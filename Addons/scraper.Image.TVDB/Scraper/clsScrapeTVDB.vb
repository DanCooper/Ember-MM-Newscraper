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

Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog
Imports System.Diagnostics

Namespace TVDBs

    Public Class Scraper

#Region "Fields"

        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Friend WithEvents bwTVDB As New System.ComponentModel.BackgroundWorker

        Private _TVDBApi As TVDB.Web.WebInterface
        Private _TVDBMirror As TVDB.Model.Mirror

#End Region 'Fields

#Region "Events"

        '		Public Event PostersDownloaded(ByVal Posters As List(Of MediaContainers.Image))

        '		Public Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Methods"

        Public Sub New(ByVal SpecialSettings As TVDB_Image.SpecialSettings)
            Try

                If Not Directory.Exists(Path.Combine(Master.TempPath, "Shows")) Then Directory.CreateDirectory(Path.Combine(Master.TempPath, "Shows"))
                _TVDBApi = New TVDB.Web.WebInterface(SpecialSettings.ApiKey, Path.Combine(Master.TempPath, "Shows"))
                _TVDBMirror = New TVDB.Model.Mirror With {.Address = "http://thetvdb.com", .ContainsBannerFile = True, .ContainsXmlFile = True, .ContainsZipFile = False}

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub
        ''' <summary>
        ''' Workaround to fix the theTVDB bug
        ''' </summary>
        ''' <param name="tvdbID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Async Function GetFullSeriesById(ByVal tvdbID As Integer) As Task(Of TVDB.Model.SeriesDetails)
            Dim Result As TVDB.Model.SeriesDetails = Await _TVDBApi.GetFullSeriesById(tvdbID, _TVDBMirror)
            Return Result
        End Function

        Public Function GetImages_TV(ByVal tvdbID As String, ByVal FilteredModifiers As Structures.ScrapeModifiers) As MediaContainers.SearchResultsContainer
            Dim alContainer As New MediaContainers.SearchResultsContainer

            Try
                Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(CInt(tvdbID)))
                If APIResult Is Nothing OrElse APIResult.Result Is Nothing Then
                    Return Nothing
                End If
                Dim Results = APIResult.Result

                If bwTVDB.CancellationPending Then Return Nothing

                If Results.Banners IsNot Nothing Then

                    'EpisodePoster
                    If FilteredModifiers.EpisodePoster AndAlso Results.Series.Episodes IsNot Nothing Then
                        For Each tEpisode As TVDB.Model.Episode In Results.Series.Episodes.Where(Function(f) Not String.IsNullOrEmpty(f.PictureFilename))
                            Dim img As New MediaContainers.Image With {
                            .Episode = tEpisode.Number,
                            .Height = CStr(tEpisode.ThumbHeight),
                            .LongLang = If(tEpisode.Language IsNot Nothing, Localization.ISOGetLangByCode2(tEpisode.Language), String.Empty),
                            .Scraper = "TVDB",
                            .Season = tEpisode.SeasonNumber,
                            .ShortLang = If(tEpisode.Language IsNot Nothing, tEpisode.Language, String.Empty),
                            .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", tEpisode.PictureFilename),
                            .URLThumb = If(Not String.IsNullOrEmpty(tEpisode.PictureFilename), String.Concat(_TVDBMirror.Address, "/banners/_cache/", tEpisode.PictureFilename), String.Empty),
                            .Width = CStr(tEpisode.ThumbWidth)}

                            alContainer.EpisodePosters.Add(img)
                        Next
                    End If

                    'MainBanner
                    If FilteredModifiers.MainBanner Then
                        For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.series)
                            Dim img As New MediaContainers.Image With {.Height = "140",
                                                                       .LongLang = If(image.Language IsNot Nothing, Localization.ISOGetLangByCode2(image.Language), String.Empty),
                                                                       .Scraper = "TVDB",
                                                                       .Season = image.Season,
                                                                       .ShortLang = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                                                       .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                                                       .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                                                       .VoteAverage = CStr(image.Rating),
                                                                       .VoteCount = image.RatingCount,
                                                                       .Width = "758"}
                            alContainer.MainBanners.Add(img)
                        Next
                    End If

                    'SeasonBanner
                    If FilteredModifiers.SeasonBanner Then
                        For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.season AndAlso f.BannerPath.Contains("seasonswide"))
                            Dim img As New MediaContainers.Image With {.Height = "140",
                                                                       .LongLang = If(image.Language IsNot Nothing, Localization.ISOGetLangByCode2(image.Language), String.Empty),
                                                                       .Scraper = "TVDB",
                                                                       .Season = image.Season,
                                                                       .ShortLang = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                                                       .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                                                       .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                                                       .VoteAverage = CStr(image.Rating),
                                                                       .VoteCount = image.RatingCount,
                                                                       .Width = "758"}
                            alContainer.SeasonBanners.Add(img)
                        Next
                    End If

                    'MainFanart
                    If FilteredModifiers.MainFanart Then
                        For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.fanart)
                            alContainer.MainFanarts.Add(New MediaContainers.Image With {.Height = StringUtils.StringToSize(image.Dimension).Height.ToString,
                                                                                        .LongLang = If(image.Language IsNot Nothing, Localization.ISOGetLangByCode2(image.Language), String.Empty),
                                                                                        .Scraper = "TVDB",
                                                                                        .Season = image.Season,
                                                                                        .ShortLang = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                                                                        .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                                                                        .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                                                                        .VoteAverage = CStr(image.Rating),
                                                                                        .VoteCount = image.RatingCount,
                                                                                        .Width = StringUtils.StringToSize(image.Dimension).Width.ToString})
                        Next
                    End If

                    'MainPoster
                    If FilteredModifiers.MainPoster Then
                        For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.poster)
                            Dim img As New MediaContainers.Image With {.Height = StringUtils.StringToSize(image.Dimension).Height.ToString,
                                                                       .LongLang = If(image.Language IsNot Nothing, Localization.ISOGetLangByCode2(image.Language), String.Empty),
                                                                       .Scraper = "TVDB",
                                                                       .Season = image.Season,
                                                                       .ShortLang = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                                                       .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                                                       .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                                                       .VoteAverage = CStr(image.Rating),
                                                                       .VoteCount = image.RatingCount,
                                                                       .Width = StringUtils.StringToSize(image.Dimension).Width.ToString}
                            alContainer.MainPosters.Add(img)
                        Next
                    End If

                    'SeasonPoster
                    If FilteredModifiers.SeasonPoster Then
                        For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.season AndAlso Not f.BannerPath.Contains("seasonswide"))
                            Dim img As New MediaContainers.Image With {.Height = "578",
                                                                       .LongLang = If(image.Language IsNot Nothing, Localization.ISOGetLangByCode2(image.Language), String.Empty),
                                                                       .Scraper = "TVDB",
                                                                       .Season = image.Season,
                                                                       .ShortLang = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                                                       .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                                                       .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                                                       .VoteAverage = CStr(image.Rating),
                                                                       .VoteCount = image.RatingCount,
                                                                       .Width = "400"}
                            alContainer.SeasonPosters.Add(img)
                        Next
                    End If

                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Return alContainer
        End Function

        Public Function GetImages_TVEpisode(ByVal tvdbID As String, ByVal iSeason As Integer, ByVal iEpisode As Integer, ByVal FilteredModifiers As Structures.ScrapeModifiers) As MediaContainers.SearchResultsContainer
            Dim alContainer As New MediaContainers.SearchResultsContainer

            Try
                Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(CInt(tvdbID)))
                If APIResult Is Nothing OrElse APIResult.Result Is Nothing Then
                    Return Nothing
                End If
                Dim Results = APIResult.Result

                If bwTVDB.CancellationPending Then Return Nothing

                'EpisodePoster
                If FilteredModifiers.EpisodePoster AndAlso Results.Series.Episodes IsNot Nothing Then
                    For Each tEpisode As TVDB.Model.Episode In Results.Series.Episodes.Where(Function(f) f.SeasonNumber = iSeason And f.Number = iEpisode)
                        Dim img As New MediaContainers.Image With {
                            .Episode = tEpisode.Number,
                            .Height = CStr(tEpisode.ThumbHeight),
                            .LongLang = If(tEpisode.Language IsNot Nothing, Localization.ISOGetLangByCode2(tEpisode.Language), String.Empty),
                            .Scraper = "TVDB",
                            .Season = tEpisode.SeasonNumber,
                            .ShortLang = If(tEpisode.Language IsNot Nothing, tEpisode.Language, String.Empty),
                            .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", tEpisode.PictureFilename),
                            .URLThumb = If(Not String.IsNullOrEmpty(tEpisode.PictureFilename), String.Concat(_TVDBMirror.Address, "/banners/_cache/", tEpisode.PictureFilename), String.Empty),
                            .Width = CStr(tEpisode.ThumbWidth)}

                        alContainer.EpisodePosters.Add(img)
                    Next
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Return alContainer
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

End Namespace

