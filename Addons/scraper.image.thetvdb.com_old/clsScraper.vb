' ################################################################################
' #                             EMBER MEDIA MANAGER                              #
' ################################################################################
' ################################################################################
' # This file is part of Ember Media Manager.                                    #
' #                                                                              #
' # Ember Media Manager is free software: you can redistribute it and/or modify  #
' # it under the terms of the GNU General Public License as published by         #
' # the Free Software Foundation, either version 3 of the License, or            #
' # (at your option) any later version.                                          #
' #                                                                              #
' # Ember Media Manager is distributed in the hope that it will be useful,       #
' # but WITHOUT ANY WARRANTY; without even the implied warranty of               #
' # MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                #
' # GNU General Public License for more details.                                 #
' #                                                                              #
' # You should have received a copy of the GNU General Public License            #
' # along with Ember Media Manager.  If not, see <http://www.gnu.org/licenses/>. #
' ################################################################################

Imports EmberAPI
Imports NLog
Imports System.IO

Public Class Scraper

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwTVDB As New ComponentModel.BackgroundWorker

    Private _TVDBApi As TVDB.Web.WebInterface
    Private _TVDBMirror As TVDB.Model.Mirror

#End Region 'Fields

#Region "Methods"

    Public Sub New(ByVal SpecialSettings As Addon.AddonSettings)
        Try

            If Not Directory.Exists(Path.Combine(Master.TempPath, "Shows")) Then Directory.CreateDirectory(Path.Combine(Master.TempPath, "Shows"))
            _TVDBApi = New TVDB.Web.WebInterface(SpecialSettings.ApiKey, Path.Combine(Master.TempPath, "Shows"))
            _TVDBMirror = New TVDB.Model.Mirror With {.Address = "http://thetvdb.com", .ContainsBannerFile = True, .ContainsXmlFile = True, .ContainsZipFile = False}

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Function GetAllImages_TV(ByVal tvdbId As Integer, ByVal filteredModifiers As Structures.ScrapeModifiers, ByVal language As String) As MediaContainers.SearchResultsContainer
        'get images for the content language
        Dim alImagesContainer As New MediaContainers.SearchResultsContainer
        alImagesContainer = GetImages_TV(tvdbId, filteredModifiers, language)
        'get images english tagged images
        Dim lstEnglishImages = GetImages_TV(tvdbId, filteredModifiers, "en")
        alImagesContainer.EpisodePosters.AddRange(lstEnglishImages.EpisodePosters)
        alImagesContainer.MainBanners.AddRange(lstEnglishImages.MainBanners)
        alImagesContainer.MainFanarts.AddRange(lstEnglishImages.MainFanarts)
        alImagesContainer.MainPosters.AddRange(lstEnglishImages.MainPosters)
        alImagesContainer.SeasonBanners.AddRange(lstEnglishImages.SeasonBanners)
        alImagesContainer.SeasonPosters.AddRange(lstEnglishImages.SeasonPosters)
        Return alImagesContainer
    End Function
    ''' <summary>
    ''' Workaround to fix the theTVDB bug
    ''' </summary>
    ''' <param name="tvdbId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function GetFullSeriesById(ByVal tvdbId As Integer, ByVal language As String) As Task(Of TVDB.Model.SeriesDetails)
        Dim Result As TVDB.Model.SeriesDetails = Await _TVDBApi.GetFullSeriesById(tvdbId, language, _TVDBMirror)
        Return Result
    End Function

    Private Function GetImages_TV(ByVal tvdbId As Integer, ByVal filteredModifiers As Structures.ScrapeModifiers, ByVal language As String) As MediaContainers.SearchResultsContainer
        Dim alImagesContainer As New MediaContainers.SearchResultsContainer

        Try
            Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(tvdbId, language))
            If APIResult Is Nothing OrElse APIResult.Result Is Nothing Then
                Return alImagesContainer
            End If
            Dim Results = APIResult.Result

            If bwTVDB.CancellationPending Then Return alImagesContainer

            If Results.Banners IsNot Nothing Then

                'EpisodePoster
                If filteredModifiers.EpisodePoster AndAlso Results.Series.Episodes IsNot Nothing Then
                    For Each tEpisode As TVDB.Model.Episode In Results.Series.Episodes.Where(Function(f) f.PictureFilename IsNot Nothing)
                        Dim img As New MediaContainers.Image With {
                                .Episode = tEpisode.Number,
                                .Height = CStr(tEpisode.ThumbHeight),
                                .LongLang = If(tEpisode.Language IsNot Nothing, Localization.Languages.Get_Name_By_Alpha2(tEpisode.Language), String.Empty),
                                .Scraper = "TVDB",
                                .Season = tEpisode.SeasonNumber,
                                .ShortLang = If(tEpisode.Language IsNot Nothing, tEpisode.Language, String.Empty),
                                .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", tEpisode.PictureFilename),
                                .URLThumb = If(Not String.IsNullOrEmpty(tEpisode.PictureFilename), String.Concat(_TVDBMirror.Address, "/banners/_cache/", tEpisode.PictureFilename), String.Empty),
                                .Width = CStr(tEpisode.ThumbWidth)
                            }
                        alImagesContainer.EpisodePosters.Add(img)
                    Next
                End If

                'MainBanner
                If filteredModifiers.MainBanner Then
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.series)
                        Dim img As New MediaContainers.Image With {
                                .Height = "140",
                                .LongLang = If(image.Language IsNot Nothing, Localization.Languages.Get_Name_By_Alpha2(image.Language), String.Empty),
                                .Scraper = "TVDB",
                                .Season = image.Season,
                                .ShortLang = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                .VoteAverage = CStr(image.Rating),
                                .VoteCount = image.RatingCount,
                                .Width = "758"
                            }
                        alImagesContainer.MainBanners.Add(img)
                    Next
                End If

                'SeasonBanner
                If filteredModifiers.SeasonBanner Then
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.season AndAlso f.BannerPath.Contains("seasonswide"))
                        Dim img As New MediaContainers.Image With {
                                .Height = "140",
                                .LongLang = If(image.Language IsNot Nothing, Localization.Languages.Get_Name_By_Alpha2(image.Language), String.Empty),
                                .Scraper = "TVDB",
                                .Season = image.Season,
                                .ShortLang = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                .VoteAverage = CStr(image.Rating),
                                .VoteCount = image.RatingCount,
                                .Width = "758"
                            }
                        alImagesContainer.SeasonBanners.Add(img)
                    Next
                End If

                'MainFanart
                If filteredModifiers.MainFanart Then
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.fanart)
                        alImagesContainer.MainFanarts.Add(New MediaContainers.Image With {
                                                              .Height = StringUtils.StringToSize(image.Dimension).Height.ToString,
                                                              .LongLang = If(image.Language IsNot Nothing, Localization.Languages.Get_Name_By_Alpha2(image.Language), String.Empty),
                                                              .Scraper = "TVDB",
                                                              .Season = image.Season,
                                                              .ShortLang = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                                              .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                                              .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                                              .VoteAverage = CStr(image.Rating),
                                                              .VoteCount = image.RatingCount,
                                                              .Width = StringUtils.StringToSize(image.Dimension).Width.ToString
                                                              })
                    Next
                End If

                'MainPoster / MainKeyart
                If filteredModifiers.MainPoster OrElse filteredModifiers.MainKeyart Then
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.poster)
                        Dim img As New MediaContainers.Image With {
                                .Height = StringUtils.StringToSize(image.Dimension).Height.ToString,
                                .LongLang = If(image.Language IsNot Nothing, Localization.Languages.Get_Name_By_Alpha2(image.Language), String.Empty),
                                .Scraper = "TVDB",
                                .Season = image.Season,
                                .ShortLang = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                .VoteAverage = CStr(image.Rating),
                                .VoteCount = image.RatingCount,
                                .Width = StringUtils.StringToSize(image.Dimension).Width.ToString
                            }
                        alImagesContainer.MainPosters.Add(img)
                    Next
                End If

                'SeasonPoster
                If filteredModifiers.SeasonPoster Then
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.season AndAlso Not f.BannerPath.Contains("seasonswide"))
                        Dim img As New MediaContainers.Image With {
                                .Height = "578",
                                .LongLang = If(image.Language IsNot Nothing, Localization.Languages.Get_Name_By_Alpha2(image.Language), String.Empty),
                                .Scraper = "TVDB",
                                .Season = image.Season,
                                .ShortLang = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                .VoteAverage = CStr(image.Rating),
                                .VoteCount = image.RatingCount,
                                .Width = "400"
                            }
                        alImagesContainer.SeasonPosters.Add(img)
                    Next
                End If

            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return alImagesContainer
    End Function

    Public Function GetImages_TVEpisode(ByVal tvdbId As Integer, ByVal iSeason As Integer, ByVal iEpisode As Integer, ByVal tEpisodeOrdering As Enums.EpisodeOrdering, ByVal filteredModifiers As Structures.ScrapeModifiers) As MediaContainers.SearchResultsContainer
        Dim alImagesContainer As New MediaContainers.SearchResultsContainer

        Try
            Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(tvdbId, "en"))
            If APIResult Is Nothing OrElse APIResult.Result Is Nothing Then
                Return alImagesContainer
            End If
            Dim Results = APIResult.Result

            If bwTVDB.CancellationPending Then Return alImagesContainer

            'EpisodePoster
            If filteredModifiers.EpisodePoster AndAlso Results.Series.Episodes IsNot Nothing Then
                Dim ieEpisodes As IEnumerable(Of TVDB.Model.Episode) = Nothing
                Select Case tEpisodeOrdering
                    Case Enums.EpisodeOrdering.Absolute
                        ieEpisodes = Results.Series.Episodes.Where(Function(f) f.AbsoluteNumber = iEpisode)
                    Case Enums.EpisodeOrdering.DVD
                        ieEpisodes = Results.Series.Episodes.Where(Function(f) f.DVDSeason = iSeason And f.DVDEpisodeNumber = iEpisode)
                    Case Enums.EpisodeOrdering.Standard
                        ieEpisodes = Results.Series.Episodes.Where(Function(f) f.SeasonNumber = iSeason And f.Number = iEpisode)
                End Select

                If ieEpisodes IsNot Nothing Then
                    For Each tEpisode As TVDB.Model.Episode In ieEpisodes.Where(Function(f) f.PictureFilename IsNot Nothing)
                        Dim img As New MediaContainers.Image With {
                                .Episode = iEpisode,
                                .Height = CStr(tEpisode.ThumbHeight),
                                .LongLang = If(tEpisode.Language IsNot Nothing, Localization.Languages.Get_Name_By_Alpha2(tEpisode.Language), String.Empty),
                                .Scraper = "TVDB",
                                .Season = iSeason,
                                .ShortLang = If(tEpisode.Language IsNot Nothing, tEpisode.Language, String.Empty),
                                .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", tEpisode.PictureFilename),
                                .URLThumb = If(Not String.IsNullOrEmpty(tEpisode.PictureFilename), String.Concat(_TVDBMirror.Address, "/banners/_cache/", tEpisode.PictureFilename), String.Empty),
                                .Width = CStr(tEpisode.ThumbWidth)
                            }
                        alImagesContainer.EpisodePosters.Add(img)
                    Next
                End If
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return alImagesContainer
    End Function


#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim Parameter As String
        Dim sType As String

#End Region 'Fields

    End Structure

    Private Structure Results

#Region "Fields"

        Dim Result As Object
        Dim ResultList As List(Of MediaContainers.Image)

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class