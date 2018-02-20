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

Namespace TMDB

	Public Class Scraper

#Region "Fields"

        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Private _TMDBApi As TMDbLib.Client.TMDbClient
        Private _sPoster As String

        Friend WithEvents bwTMDB As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Methods"

        Public Sub New(ByVal SpecialSettings As TMDB_Image.SpecialSettings)
            Try

                _TMDBApi = New TMDbLib.Client.TMDbClient(SpecialSettings.APIKey)
                _TMDBApi.GetConfig()

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

        Public Function GetImages_Movie_MovieSet(ByVal TMDBID As String, ByVal FilteredModifiers As Structures.ScrapeModifiers, ByVal ContentType As Enums.ContentType) As MediaContainers.SearchResultsContainer
            Dim alImagesContainer As New MediaContainers.SearchResultsContainer

            If bwTMDB.CancellationPending Then Return alImagesContainer

            Try
                Dim Results As TMDbLib.Objects.General.Images = Nothing
                Dim APIResult As Task(Of TMDbLib.Objects.General.ImagesWithId)

                If ContentType = Enums.ContentType.Movie Then
                    APIResult = Task.Run(Function() _TMDBApi.GetMovieImagesAsync(CInt(TMDBID)))
                    Results = APIResult.Result
                ElseIf ContentType = Enums.ContentType.MovieSet Then
                    APIResult = Task.Run(Function() _TMDBApi.GetCollectionImagesAsync(CInt(TMDBID)))
                    Results = APIResult.Result
                End If

                If Results Is Nothing Then
                    Return alImagesContainer
                End If

                'MainFanart
                If (FilteredModifiers.MainExtrafanarts OrElse FilteredModifiers.MainExtrathumbs OrElse FilteredModifiers.MainFanart) AndAlso Results.Backdrops IsNot Nothing Then
                    For Each tImage In Results.Backdrops
                        Dim newImage As New MediaContainers.Image With {
                            .Height = tImage.Height.ToString,
                            .Likes = 0,
                            .LongLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(tImage.Iso_639_1)),
                            .Scraper = "TMDB",
                            .ShortLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                            .URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & tImage.FilePath,
                            .URLThumb = _TMDBApi.Config.Images.BaseUrl & "w300" & tImage.FilePath,
                            .VoteAverage = tImage.VoteAverage.ToString,
                            .VoteCount = tImage.VoteCount,
                            .Width = tImage.Width.ToString}

                        alImagesContainer.MainFanarts.Add(newImage)
                    Next
                End If

                'MainPoster
                If FilteredModifiers.MainPoster AndAlso Results.Posters IsNot Nothing Then
                    For Each tImage In Results.Posters
                        Dim newImage As New MediaContainers.Image With {
                                .Height = tImage.Height.ToString,
                                .Likes = 0,
                                .LongLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(tImage.Iso_639_1)),
                                .Scraper = "TMDB",
                                .ShortLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                                .URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & tImage.FilePath,
                                .URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & tImage.FilePath,
                                .VoteAverage = tImage.VoteAverage.ToString,
                                .VoteCount = tImage.VoteCount,
                                .Width = tImage.Width.ToString}

                        alImagesContainer.MainPosters.Add(newImage)
                    Next
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Return alImagesContainer
        End Function

        Public Function GetImages_TVShow(ByVal tmdbID As String, ByVal FilteredModifiers As Structures.ScrapeModifiers) As MediaContainers.SearchResultsContainer
            Dim alImagesContainer As New MediaContainers.SearchResultsContainer

            If bwTMDB.CancellationPending Then Return alImagesContainer

            Try

                Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvShow)
                APIResult = Task.Run(Function() _TMDBApi.GetTvShowAsync(CInt(tmdbID), TMDbLib.Objects.TvShows.TvShowMethods.Images))

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
                            .LongLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(tImage.Iso_639_1)),
                            .Scraper = "TMDB",
                            .ShortLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                            .URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & tImage.FilePath,
                            .URLThumb = _TMDBApi.Config.Images.BaseUrl & "w300" & tImage.FilePath,
                            .VoteAverage = tImage.VoteAverage.ToString,
                            .VoteCount = tImage.VoteCount,
                            .Width = tImage.Width.ToString}

                        alImagesContainer.MainFanarts.Add(newImage)
                    Next
                End If

                'MainPoster
                If FilteredModifiers.MainPoster AndAlso Result.Images.Posters IsNot Nothing Then
                    For Each tImage In Result.Images.Posters
                        Dim newImage As New MediaContainers.Image With {
                                .Height = tImage.Height.ToString,
                                .Likes = 0,
                                .LongLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(tImage.Iso_639_1)),
                                .Scraper = "TMDB",
                                .ShortLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                                .URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & tImage.FilePath,
                                .URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & tImage.FilePath,
                                .VoteAverage = tImage.VoteAverage.ToString,
                                .VoteCount = tImage.VoteCount,
                                .Width = tImage.Width.ToString}

                        alImagesContainer.MainPosters.Add(newImage)
                    Next
                End If

                'SeasonPoster
                If (FilteredModifiers.SeasonPoster OrElse FilteredModifiers.EpisodePoster) AndAlso Result.Seasons IsNot Nothing Then
                    For Each tSeason In Result.Seasons
                        Dim APIResult_Season As Task(Of TMDbLib.Objects.TvShows.TvSeason)
                        APIResult_Season = Task.Run(Function() _TMDBApi.GetTvSeasonAsync(CInt(tmdbID), tSeason.SeasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Images))

                        If APIResult_Season IsNot Nothing Then
                            Dim Result_Season As TMDbLib.Objects.TvShows.TvSeason = APIResult_Season.Result

                            'SeasonPoster
                            If FilteredModifiers.SeasonPoster AndAlso Result_Season.Images.Posters IsNot Nothing Then
                                For Each tImage In Result_Season.Images.Posters
                                    Dim newImage As New MediaContainers.Image With {
                                        .Height = tImage.Height.ToString,
                                        .Likes = 0,
                                        .LongLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(tImage.Iso_639_1)),
                                        .Scraper = "TMDB",
                                        .Season = tSeason.SeasonNumber,
                                        .ShortLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                                        .URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & tImage.FilePath,
                                        .URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & tImage.FilePath,
                                        .VoteAverage = tImage.VoteAverage.ToString,
                                        .VoteCount = tImage.VoteCount,
                                        .Width = tImage.Width.ToString}

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
                                            .Season = CInt(tEpisode.SeasonNumber),
                                            .URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & tEpisode.StillPath,
                                            .URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & tEpisode.StillPath}

                                        alImagesContainer.EpisodePosters.Add(newImage)

                                        '    For Each tImage In tEpisode.Images.Stills
                                        '        Dim newImage As New MediaContainers.Image With {
                                        '            .Episode = tEpisode.EpisodeNumber,
                                        '            .Height = tImage.Height.ToString,
                                        '            .Likes = 0,
                                        '            .LongLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(tImage.Iso_639_1)),
                                        '            .Scraper = "TMDB",
                                        '            .Season = CInt(tEpisode.SeasonNumber),
                                        '            .ShortLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                                        '            .URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & tImage.FilePath,
                                        '            .URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & tImage.FilePath,
                                        '            .VoteAverage = tImage.VoteAverage.ToString,
                                        '            .VoteCount = tImage.VoteCount,
                                        '            .Width = tImage.Width.ToString}

                                        '        alContainer.EpisodePosters.Add(newImage)
                                        '    Next
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

        Public Function GetImages_TVEpisode(ByVal tmdbID As String, ByVal iSeason As Integer, ByVal iEpisode As Integer, ByVal FilteredModifiers As Structures.ScrapeModifiers) As MediaContainers.SearchResultsContainer
            Dim alImagesContainer As New MediaContainers.SearchResultsContainer

            If bwTMDB.CancellationPending Then Return alImagesContainer

            Try
                Dim Results As TMDbLib.Objects.General.StillImages = Nothing
                Dim APIResult As Task(Of TMDbLib.Objects.General.StillImages)
                APIResult = Task.Run(Function() _TMDBApi.GetTvEpisodeImagesAsync(CInt(tmdbID), iSeason, iEpisode))
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
                            .LongLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(tImage.Iso_639_1)),
                            .Scraper = "TMDB",
                            .Season = iSeason,
                            .ShortLang = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                            .URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & tImage.FilePath,
                            .URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & tImage.FilePath,
                            .VoteAverage = tImage.VoteAverage.ToString,
                            .VoteCount = tImage.VoteCount,
                            .Width = tImage.Width.ToString}

                        alImagesContainer.EpisodePosters.Add(newImage)
                    Next
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Return alImagesContainer
        End Function

        Public Function GetTMDBbyIMDB(ByVal imdbID As String) As String
            Dim tmdbID As String = String.Empty

            Try
                Dim APIResult As Task(Of TMDbLib.Objects.Find.FindContainer)
                APIResult = Task.Run(Function() _TMDBApi.FindAsync(TMDbLib.Objects.Find.FindExternalSource.Imdb, imdbID))

                If APIResult IsNot Nothing AndAlso APIResult.Result IsNot Nothing AndAlso
                    APIResult.Result.TvResults IsNot Nothing AndAlso APIResult.Result.TvResults.Count > 0 Then
                    tmdbID = APIResult.Result.TvResults.Item(0).Id.ToString
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Return tmdbID
        End Function

        Public Function GetTMDBbyTVDB(ByVal tvdbID As String) As String
            Dim tmdbID As String = String.Empty

            Try
                Dim APIResult As Task(Of TMDbLib.Objects.Find.FindContainer)
                APIResult = Task.Run(Function() _TMDBApi.FindAsync(TMDbLib.Objects.Find.FindExternalSource.TvDb, tvdbID))

                If APIResult IsNot Nothing AndAlso APIResult.Result IsNot Nothing AndAlso _
                    APIResult.Result.TvResults IsNot Nothing AndAlso APIResult.Result.TvResults.Count > 0 Then
                    tmdbID = APIResult.Result.TvResults.Item(0).Id.ToString
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Return tmdbID
        End Function

#End Region 'Methods

#Region "Nested Types"

        Private Structure Arguments

#Region "Fields"

            Dim Parameter As String
            Dim Type As Enums.ModifierType

#End Region 'Fields

        End Structure

#End Region 'Nested Types

	End Class

End Namespace