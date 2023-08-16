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
Imports System.Diagnostics

Public Class clsScraper

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _client As TMDbLib.Client.TMDbClient

#End Region 'Fields

#Region "Methods"

    Public Async Function CreateAPI(ByVal specialSettings As Addon.AddonSettings) As Task
        Try
            _client = New TMDbLib.Client.TMDbClient(specialSettings.ApiKey)
            Await _client.GetConfigAsync()
            _client.MaxRetryCount = 2
            logger.Trace("[TMDB_Image] [CreateAPI] Client created")
        Catch ex As Exception
            logger.Error(String.Format("[TMDB_Image] [CreateAPI] [Error] {0}", ex.Message))
        End Try
    End Function

    Public Function GetImages_Movie_MovieSet(ByVal tmdbId As Integer, ByVal filteredModifiers As Structures.ScrapeModifiers, ByVal contentType As Enums.ContentType) As MediaContainers.SearchResultsContainer
        Dim alImagesContainer As New MediaContainers.SearchResultsContainer

        Try
            Dim Results As TMDbLib.Objects.General.Images = Nothing
            Dim APIResult As Task(Of TMDbLib.Objects.General.ImagesWithId)

            If contentType = Enums.ContentType.Movie Then
                APIResult = Task.Run(Function() _client.GetMovieImagesAsync(tmdbId))
                Results = APIResult.Result
            ElseIf contentType = Enums.ContentType.MovieSet Then
                APIResult = Task.Run(Function() _client.GetCollectionImagesAsync(tmdbId))
                Results = APIResult.Result
            End If

            If Results Is Nothing Then
                Return alImagesContainer
            End If

            'MainFanart
            If (filteredModifiers.MainExtrafanarts OrElse filteredModifiers.MainExtrathumbs OrElse filteredModifiers.MainFanart) AndAlso Results.Backdrops IsNot Nothing Then
                For Each tImage In Results.Backdrops
                    Dim newImage As New MediaContainers.Image With {
                        .Height = tImage.Height.ToString,
                        .Likes = 0,
                        .LongLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, Localization.Languages.Get_Name_By_Alpha2(tImage.Iso_639_1)),
                        .Scraper = "TMDB",
                        .ShortLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                        .URLOriginal = _client.Config.Images.BaseUrl & "original" & tImage.FilePath,
                        .URLThumb = _client.Config.Images.BaseUrl & "w300" & tImage.FilePath,
                        .VoteAverage = tImage.VoteAverage.ToString,
                        .VoteCount = tImage.VoteCount,
                        .Width = tImage.Width.ToString
                    }
                    alImagesContainer.MainFanarts.Add(newImage)
                Next
            End If

            'MainPoster / MainKeyart
            If (filteredModifiers.MainPoster OrElse filteredModifiers.MainKeyart) AndAlso Results.Posters IsNot Nothing Then
                For Each tImage In Results.Posters
                    Dim newImage As New MediaContainers.Image With {
                        .Height = tImage.Height.ToString,
                        .Likes = 0,
                        .LongLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, Localization.Languages.Get_Name_By_Alpha2(tImage.Iso_639_1)),
                        .Scraper = "TMDB",
                        .ShortLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                        .URLOriginal = _client.Config.Images.BaseUrl & "original" & tImage.FilePath,
                        .URLThumb = _client.Config.Images.BaseUrl & "w185" & tImage.FilePath,
                        .VoteAverage = tImage.VoteAverage.ToString,
                        .VoteCount = tImage.VoteCount,
                        .Width = tImage.Width.ToString
                    }
                    alImagesContainer.MainPosters.Add(newImage)
                Next
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return alImagesContainer
    End Function

    Public Function GetImages_TVShow(ByVal tmdbID As Integer, ByVal FilteredModifiers As Structures.ScrapeModifiers) As MediaContainers.SearchResultsContainer
        Dim alImagesContainer As New MediaContainers.SearchResultsContainer

        Try
            Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvShow)
            APIResult = Task.Run(Function() _client.GetTvShowAsync(tmdbID, TMDbLib.Objects.TvShows.TvShowMethods.Images))

            If APIResult Is Nothing Then
                Return alImagesContainer
            End If

            Dim Result As TMDbLib.Objects.TvShows.TvShow = APIResult.Result

            'MainFanart
            If FilteredModifiers.MainFanart AndAlso Result.Images.Backdrops IsNot Nothing Then
                For Each tImage In Result.Images.Backdrops
                    Dim newImage As New MediaContainers.Image With {
                        .Height = tImage.Height.ToString,
                        .Likes = 0,
                        .LongLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, Localization.Languages.Get_Name_By_Alpha2(tImage.Iso_639_1)),
                        .Scraper = "TMDB",
                        .ShortLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                        .URLOriginal = _client.Config.Images.BaseUrl & "original" & tImage.FilePath,
                        .URLThumb = _client.Config.Images.BaseUrl & "w300" & tImage.FilePath,
                        .VoteAverage = tImage.VoteAverage.ToString,
                        .VoteCount = tImage.VoteCount,
                        .Width = tImage.Width.ToString
                    }
                    alImagesContainer.MainFanarts.Add(newImage)
                Next
            End If

            'MainPoster / MainKeyart
            If (FilteredModifiers.MainPoster OrElse FilteredModifiers.MainKeyart) AndAlso Result.Images.Posters IsNot Nothing Then
                For Each tImage In Result.Images.Posters
                    Dim newImage As New MediaContainers.Image With {
                        .Height = tImage.Height.ToString,
                        .Likes = 0,
                        .LongLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, Localization.Languages.Get_Name_By_Alpha2(tImage.Iso_639_1)),
                        .Scraper = "TMDB",
                        .ShortLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                        .URLOriginal = _client.Config.Images.BaseUrl & "original" & tImage.FilePath,
                        .URLThumb = _client.Config.Images.BaseUrl & "w185" & tImage.FilePath,
                        .VoteAverage = tImage.VoteAverage.ToString,
                        .VoteCount = tImage.VoteCount,
                        .Width = tImage.Width.ToString
                    }
                    alImagesContainer.MainPosters.Add(newImage)
                Next
            End If

            'SeasonPoster
            If (FilteredModifiers.SeasonPoster OrElse FilteredModifiers.EpisodePoster) AndAlso Result.Seasons IsNot Nothing Then
                For Each tSeason In Result.Seasons
                    Dim APIResult_Season As Task(Of TMDbLib.Objects.TvShows.TvSeason)
                    APIResult_Season = Task.Run(Function() _client.GetTvSeasonAsync(tmdbID, tSeason.SeasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Images))

                    If APIResult_Season IsNot Nothing Then
                        Dim Result_Season As TMDbLib.Objects.TvShows.TvSeason = APIResult_Season.Result

                        'SeasonPoster
                        If FilteredModifiers.SeasonPoster AndAlso Result_Season.Images.Posters IsNot Nothing Then
                            For Each tImage In Result_Season.Images.Posters
                                Dim newImage As New MediaContainers.Image With {
                                    .Height = tImage.Height.ToString,
                                    .Likes = 0,
                                    .LongLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, Localization.Languages.Get_Name_By_Alpha2(tImage.Iso_639_1)),
                                    .Scraper = "TMDB",
                                    .Season = tSeason.SeasonNumber,
                                    .ShortLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                                    .URLOriginal = _client.Config.Images.BaseUrl & "original" & tImage.FilePath,
                                    .URLThumb = _client.Config.Images.BaseUrl & "w185" & tImage.FilePath,
                                    .VoteAverage = tImage.VoteAverage.ToString,
                                    .VoteCount = tImage.VoteCount,
                                    .Width = tImage.Width.ToString
                                }
                                alImagesContainer.SeasonPosters.Add(newImage)
                            Next
                        End If

                        If FilteredModifiers.EpisodePoster AndAlso Result_Season.Episodes IsNot Nothing Then
                            For Each tEpisode In Result_Season.Episodes

                                'EpisodePoster
                                If FilteredModifiers.EpisodePoster AndAlso tEpisode.StillPath IsNot Nothing Then

                                    Dim newImage As New MediaContainers.Image With {
                                        .Episode = tEpisode.EpisodeNumber,
                                        .Scraper = "TMDB",
                                        .Season = tEpisode.SeasonNumber,
                                        .URLOriginal = _client.Config.Images.BaseUrl & "original" & tEpisode.StillPath,
                                        .URLThumb = _client.Config.Images.BaseUrl & "w185" & tEpisode.StillPath
                                    }
                                    alImagesContainer.EpisodePosters.Add(newImage)
                                End If
                            Next
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return alImagesContainer
    End Function

    Public Function GetImages_TVEpisode(ByVal tmdbID As Integer, ByVal iSeason As Integer, ByVal iEpisode As Integer, ByVal FilteredModifiers As Structures.ScrapeModifiers) As MediaContainers.SearchResultsContainer
        Dim alImagesContainer As New MediaContainers.SearchResultsContainer

        Try
            Dim Results As TMDbLib.Objects.General.StillImages = Nothing
            Dim APIResult As Task(Of TMDbLib.Objects.General.StillImages)
            APIResult = Task.Run(Function() _client.GetTvEpisodeImagesAsync(tmdbID, iSeason, iEpisode))
            Results = APIResult.Result

            If Results Is Nothing Then
                Return alImagesContainer
            End If

            'EpisodePoster
            If FilteredModifiers.EpisodePoster AndAlso Results.Stills IsNot Nothing Then
                For Each tImage In Results.Stills
                    Dim newImage As New MediaContainers.Image With {
                        .Episode = iEpisode,
                        .Height = tImage.Height.ToString,
                        .Likes = 0,
                        .LongLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, Localization.Languages.Get_Name_By_Alpha2(tImage.Iso_639_1)),
                        .Scraper = "TMDB",
                        .Season = iSeason,
                        .ShortLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                        .URLOriginal = _client.Config.Images.BaseUrl & "original" & tImage.FilePath,
                        .URLThumb = _client.Config.Images.BaseUrl & "w185" & tImage.FilePath,
                        .VoteAverage = tImage.VoteAverage.ToString,
                        .VoteCount = tImage.VoteCount,
                        .Width = tImage.Width.ToString
                    }
                    alImagesContainer.EpisodePosters.Add(newImage)
                Next
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return alImagesContainer
    End Function

    Public Function GetTMDBbyIMDB(ByVal imdbID As String) As Integer
        Try
            Dim APIResult As Task(Of TMDbLib.Objects.Find.FindContainer)
            APIResult = Task.Run(Function() _client.FindAsync(TMDbLib.Objects.Find.FindExternalSource.Imdb, imdbID))

            If APIResult IsNot Nothing AndAlso APIResult.Result IsNot Nothing AndAlso
                    APIResult.Result.TvResults IsNot Nothing AndAlso APIResult.Result.TvResults.Count > 0 Then
                Return APIResult.Result.TvResults.Item(0).Id
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return -1
    End Function

    Public Function GetTMDBbyTVDB(ByVal tvdbId As Integer) As Integer
        Try
            Dim APIResult As Task(Of TMDbLib.Objects.Find.FindContainer)
            APIResult = Task.Run(Function() _client.FindAsync(TMDbLib.Objects.Find.FindExternalSource.TvDb, tvdbId.ToString))

            If APIResult IsNot Nothing AndAlso APIResult.Result IsNot Nothing AndAlso
                    APIResult.Result.TvResults IsNot Nothing AndAlso APIResult.Result.TvResults.Count > 0 Then
                Return APIResult.Result.TvResults.Item(0).Id
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return -1
    End Function

#End Region 'Methods

End Class