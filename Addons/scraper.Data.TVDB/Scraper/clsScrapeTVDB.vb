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

#End Region 'Fields

#Region "Events"

        '		Public Event PostersDownloaded(ByVal Posters As List(Of MediaContainers.Image))

        '		Public Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Methods"

        Public Function GetTVShowInfo(ByVal strID As String, ByRef nShow As MediaContainers.TVShow, ByVal GetPoster As Boolean, ByVal Options As Structures.ScrapeOptions_TV, ByVal IsSearch As Boolean, ByRef Settings As MySettings) As Boolean
            If String.IsNullOrEmpty(strID) OrElse strID.Length < 2 Then Return False

            'clear nShow from search results
            nShow.Clear()

            Dim tvdbAPI = New TVDB.Web.WebInterface(Settings.ApiKey)
            Dim tvdbMirror As New TVDB.Model.Mirror With {.Address = "http://thetvdb.com", .ContainsBannerFile = True, .ContainsXmlFile = True, .ContainsZipFile = False}

            Dim Results As TVDB.Model.SeriesDetails = tvdbAPI.GetFullSeriesById(CInt(strID), Settings.Language, tvdbMirror).Result
            If Results Is Nothing Then
                Return Nothing
            End If

            nShow.Scrapersource = "TVDB"
            nShow.ID = CStr(Results.Series.Id)
            nShow.IMDB = CStr(Results.Series.IMDBId)

            'Cast (Actors)
            If Options.bShowActors Then
                If Results.Actors IsNot Nothing Then
                    For Each aCast As TVDB.Model.Actor In Results.Actors.OrderBy(Function(f) f.SortOrder)
                        nShow.Actors.Add(New MediaContainers.Person With {.Name = aCast.Name, _
                                                                          .Order = aCast.SortOrder, _
                                                                          .Role = aCast.Role, _
                                                                          .ThumbURL = If(Not String.IsNullOrEmpty(aCast.ImagePath), String.Format("{0}/banners/{1}", tvdbMirror.Address, aCast.ImagePath), String.Empty), _
                                                                          .TMDB = CStr(aCast.Id)})
                    Next
                End If
            End If

            'Genres
            If Options.bShowGenre Then
                Dim aGenres As List(Of String) = Nothing
                If Results.Series.Genre IsNot Nothing Then
                    aGenres = Results.Series.Genre.Split(CChar(",")).ToList
                End If

                If aGenres IsNot Nothing Then
                    For Each tGenre As String In aGenres
                        nShow.Genres.Add(tGenre.Trim)
                    Next
                End If
            End If

            'MPAA
            If Options.bShowMPAA Then
                nShow.MPAA = Results.Series.ContentRating
            End If

            'Plot
            If Options.bShowPlot Then
                If Results.Series.Overview IsNot Nothing Then
                    nShow.Plot = Results.Series.Overview
                End If
            End If

            'Posters (only for SearchResult dialog, auto fallback to "en" by TMDB)
            If GetPoster Then
                'Dim Images As TMDbLib.Objects.General.Images
                'Images = _TMDBApi.GetMovieImages(Movie.Id)
                'If Images IsNot Nothing AndAlso Images.Posters IsNot Nothing Then
                '    If Images.Posters.Count > 0 Then
                '        _sPoster = _TMDBApi.Config.Images.BaseUrl & "w92" & Images.Posters(0).FilePath
                '    Else
                '        _sPoster = String.Empty
                '    End If
                'End If
            End If

            'Premiered
            If Options.bShowPremiered Then
                nShow.Premiered = CStr(Results.Series.FirstAired)
            End If

            'Rating
            If Options.bShowRating Then
                nShow.Rating = CStr(Results.Series.Rating)
            End If

            'Runtime
            If Options.bShowRuntime Then
                nShow.Runtime = CStr(Results.Series.Runtime)
            End If

            'Status
            If Options.bShowStatus Then
                nShow.Status = Results.Series.Status
            End If

            'Studios
            If Options.bShowStudio Then
                nShow.Studios.Add(Results.Series.Network)
            End If

            'Title
            If Options.bShowTitle Then
                nShow.Title = Results.Series.Name
            End If

            'Votes
            If Options.bShowVotes Then
                nShow.Votes = CStr(Results.Series.RatingCount)
            End If

            Return True
        End Function


        Public Function GetImages_TV(ByVal tvdbID As String, ByVal Type As Enums.ScraperCapabilities_TV, ByRef Settings As MySettings) As MediaContainers.ImagesContainer_TV
            Dim alContainer As New MediaContainers.ImagesContainer_TV

            Try
                Dim tvdbAPI = New TVDB.Web.WebInterface(Settings.ApiKey)
                Dim tvdbMirror As New TVDB.Model.Mirror With {.Address = "http://thetvdb.com", .ContainsBannerFile = True, .ContainsXmlFile = True, .ContainsZipFile = False}

                Dim Results As TVDB.Model.SeriesDetails = tvdbAPI.GetFullSeriesById(CInt(tvdbID), tvdbMirror).Result
                If Results Is Nothing Then
                    Return Nothing
                End If

                If bwTVDB.CancellationPending Then Return Nothing

                If Results.Banners IsNot Nothing Then

                    'Banner Show
                    If Type = Enums.ScraperCapabilities_TV.All OrElse Type = Enums.ScraperCapabilities_TV.ShowBanner Then
                        For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.series)
                            Dim img As New MediaContainers.Image With {.Height = "140", _
                                                                       .LongLang = Localization.ISOGetLangByCode2(image.Language), _
                                                                       .Season = image.Season, _
                                                                       .ShortLang = image.Language, _
                                                                       .ThumbURL = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(tvdbMirror.Address, "/banners/", image.ThumbnailPath), String.Empty), _
                                                                       .URL = String.Concat(tvdbMirror.Address, "/banners/", image.BannerPath), _
                                                                       .VoteAverage = CStr(image.Rating), _
                                                                       .VoteCount = image.RatingCount, _
                                                                       .Width = "758"}
                            alContainer.ShowBanners.Add(img)
                        Next
                    End If

                    'Banner Season
                    If Type = Enums.ScraperCapabilities_TV.All OrElse Type = Enums.ScraperCapabilities_TV.SeasonBanner Then
                        For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.season AndAlso f.BannerPath.Contains("seasonswide"))
                            Dim img As New MediaContainers.Image With {.Height = "140", _
                                                                       .LongLang = Localization.ISOGetLangByCode2(image.Language), _
                                                                       .Season = image.Season, _
                                                                       .ShortLang = image.Language, _
                                                                       .ThumbURL = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(tvdbMirror.Address, "/banners/", image.ThumbnailPath), String.Empty), _
                                                                       .URL = String.Concat(tvdbMirror.Address, "/banners/", image.BannerPath), _
                                                                       .VoteAverage = CStr(image.Rating), _
                                                                       .VoteCount = image.RatingCount, _
                                                                       .Width = "758"}
                            alContainer.SeasonBanners.Add(img)
                        Next
                    End If

                    'Fanart Show
                    If Type = Enums.ScraperCapabilities_TV.All OrElse Type = Enums.ScraperCapabilities_TV.ShowFanart Then
                        For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.fanart)
                            alContainer.ShowFanarts.Add(New MediaContainers.Image With {.Height = StringUtils.StringToSize(image.Dimension).Height.ToString, _
                                                                          .LongLang = Localization.ISOGetLangByCode2(image.Language), _
                                                                          .Season = image.Season, _
                                                                          .ShortLang = image.Language, _
                                                                          .ThumbURL = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(tvdbMirror.Address, "/banners/", image.ThumbnailPath), String.Empty), _
                                                                          .URL = String.Concat(tvdbMirror.Address, "/banners/", image.BannerPath), _
                                                                          .VoteAverage = CStr(image.Rating), _
                                                                          .VoteCount = image.RatingCount, _
                                                                          .Width = StringUtils.StringToSize(image.Dimension).Width.ToString})
                        Next
                    End If

                    'Poster Show
                    If Type = Enums.ScraperCapabilities_TV.All OrElse Type = Enums.ScraperCapabilities_TV.ShowPoster Then
                        For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.poster)
                            Dim img As New MediaContainers.Image With {.Height = StringUtils.StringToSize(image.Dimension).Height.ToString, _
                                                                          .LongLang = Localization.ISOGetLangByCode2(image.Language), _
                                                                          .Season = image.Season, _
                                                                          .ShortLang = image.Language, _
                                                                          .ThumbURL = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(tvdbMirror.Address, "/banners/", image.ThumbnailPath), String.Empty), _
                                                                          .URL = String.Concat(tvdbMirror.Address, "/banners/", image.BannerPath), _
                                                                          .VoteAverage = CStr(image.Rating), _
                                                                          .VoteCount = image.RatingCount, _
                                                                          .Width = StringUtils.StringToSize(image.Dimension).Width.ToString}
                            alContainer.ShowPosters.Add(img)
                        Next
                    End If

                    'Poster Season
                    If Type = Enums.ScraperCapabilities_TV.All OrElse Type = Enums.ScraperCapabilities_TV.SeasonPoster Then
                        For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.season AndAlso Not f.BannerPath.Contains("seasonswide"))
                            Dim img As New MediaContainers.Image With {.Height = "578", _
                                                                       .LongLang = Localization.ISOGetLangByCode2(image.Language), _
                                                                       .Season = image.Season, _
                                                                       .ShortLang = image.Language, _
                                                                       .ThumbURL = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(tvdbMirror.Address, "/banners/", image.ThumbnailPath), String.Empty), _
                                                                       .URL = String.Concat(tvdbMirror.Address, "/banners/", image.BannerPath), _
                                                                       .VoteAverage = CStr(image.Rating), _
                                                                       .VoteCount = image.RatingCount, _
                                                                       .Width = "400"}
                            alContainer.SeasonPosters.Add(img)
                        Next
                    End If

                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
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

        Structure MySettings

#Region "Fields"

            Dim ApiKey As String
            Dim Language As String

#End Region 'Fields

        End Structure

#End Region 'Nested Types

    End Class

End Namespace

