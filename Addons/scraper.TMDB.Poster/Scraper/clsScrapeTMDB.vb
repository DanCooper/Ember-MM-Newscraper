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
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Function GetImages_Movie_MovieSet(ByVal TMDBID As String, ByVal FilteredModifier As Structures.ScrapeModifier, ByVal ContentType As Enums.ContentType) As MediaContainers.SearchResultsContainer
            Dim alImagesContainer As New MediaContainers.SearchResultsContainer

            If bwTMDB.CancellationPending Then Return Nothing

            Try
                Dim Results As TMDbLib.Objects.General.Images = Nothing
                If ContentType = Enums.ContentType.Movie Then
                    Results = _TMDBApi.GetMovieImages(CInt(TMDBID))
                ElseIf ContentType = Enums.ContentType.MovieSet Then
                    Results = _TMDBApi.GetCollectionImages(CInt(TMDBID))
                End If

                If Results Is Nothing Then
                    Return Nothing
                End If

                'Fanart
                If (FilteredModifier.MainExtrafanarts OrElse FilteredModifier.MainExtrathumbs OrElse FilteredModifier.MainFanart) AndAlso Results.Backdrops IsNot Nothing Then
                    For Each image In Results.Backdrops
                        Dim tmpImage As New MediaContainers.Image With { _
                            .Height = image.Height.ToString, _
                            .Likes = 0, _
                            .LongLang = If(String.IsNullOrEmpty(image.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(image.Iso_639_1)), _
                            .Scraper = "TMDB", _
                            .ShortLang = If(String.IsNullOrEmpty(image.Iso_639_1), String.Empty, image.Iso_639_1), _
                            .URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & image.FilePath, _
                            .URLThumb = _TMDBApi.Config.Images.BaseUrl & "w300" & image.FilePath, _
                            .VoteAverage = image.VoteAverage.ToString, _
                            .VoteCount = image.VoteCount, _
                            .Width = image.Width.ToString}

                        alImagesContainer.MainFanarts.Add(tmpImage)
                    Next
                End If

                'Poster
                If FilteredModifier.MainPoster AndAlso Results.Posters IsNot Nothing Then
                    For Each image In Results.Posters
                        Dim tmpImage As New MediaContainers.Image With { _
                                .Height = image.Height.ToString, _
                                .Likes = 0, _
                                .LongLang = If(String.IsNullOrEmpty(image.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(image.Iso_639_1)), _
                                .Scraper = "TMDB", _
                                .ShortLang = If(String.IsNullOrEmpty(image.Iso_639_1), String.Empty, image.Iso_639_1), _
                                .URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & image.FilePath, _
                                .URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & image.FilePath, _
                                .VoteAverage = image.VoteAverage.ToString, _
                                .VoteCount = image.VoteCount, _
                                .Width = image.Width.ToString}

                        alImagesContainer.MainPosters.Add(tmpImage)
                    Next
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return alImagesContainer
        End Function

        Public Function GetImages_TVShow(ByVal tmdbID As String, ByVal FilteredModifier As Structures.ScrapeModifier) As MediaContainers.SearchResultsContainer
            Dim alContainer As New MediaContainers.SearchResultsContainer

            If bwTMDB.CancellationPending Then Return Nothing

            Try
                Dim Results As TMDbLib.Objects.General.ImagesWithId = Nothing
                Results = _TMDBApi.GetTvShowImages(CInt(tmdbID))

                If Results Is Nothing Then
                    Return Nothing
                End If

                'MainFanart
                If FilteredModifier.MainFanart AndAlso Results.Backdrops IsNot Nothing Then
                    For Each image In Results.Backdrops
                        Dim tmpImage As New MediaContainers.Image With { _
                            .Height = image.Height.ToString, _
                            .Likes = 0, _
                            .LongLang = If(String.IsNullOrEmpty(image.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(image.Iso_639_1)), _
                            .Scraper = "TMDB", _
                            .ShortLang = If(String.IsNullOrEmpty(image.Iso_639_1), String.Empty, image.Iso_639_1), _
                            .URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & image.FilePath, _
                            .URLThumb = _TMDBApi.Config.Images.BaseUrl & "w300" & image.FilePath, _
                            .VoteAverage = image.VoteAverage.ToString, _
                            .VoteCount = image.VoteCount, _
                            .Width = image.Width.ToString}

                        alContainer.MainFanarts.Add(tmpImage)
                    Next
                End If

                'MainPoster
                If FilteredModifier.MainPoster AndAlso Results.Posters IsNot Nothing Then
                    For Each image In Results.Posters
                        Dim tmpImage As New MediaContainers.Image With { _
                                .Height = image.Height.ToString, _
                                .Likes = 0, _
                                .LongLang = If(String.IsNullOrEmpty(image.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(image.Iso_639_1)), _
                                .Scraper = "TMDB", _
                                .ShortLang = If(String.IsNullOrEmpty(image.Iso_639_1), String.Empty, image.Iso_639_1), _
                                .URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & image.FilePath, _
                                .URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & image.FilePath, _
                                .VoteAverage = image.VoteAverage.ToString, _
                                .VoteCount = image.VoteCount, _
                                .Width = image.Width.ToString}

                        alContainer.MainPosters.Add(tmpImage)
                    Next
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return alContainer
        End Function

        Public Function GetImages_TVEpisode(ByVal tmdbID As String, ByRef iSeason As Integer, ByRef iEpisode As Integer) As MediaContainers.SearchResultsContainer
            Dim alContainer As New MediaContainers.SearchResultsContainer

            If bwTMDB.CancellationPending Then Return Nothing

            Try
                Dim Results As TMDbLib.Objects.General.StillImages = Nothing
                Results = _TMDBApi.GetTvEpisodeImages(CInt(tmdbID), iSeason, iEpisode)

                If Results Is Nothing Then
                    Return Nothing
                End If

                'EpisodePoster
                If Results.Stills IsNot Nothing Then
                    For Each image In Results.Stills
                        Dim tmpImage As New MediaContainers.Image With { _
                            .Episode = iEpisode, _
                            .Height = image.Height.ToString, _
                            .Likes = 0, _
                            .LongLang = If(String.IsNullOrEmpty(image.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(image.Iso_639_1)), _
                            .Scraper = "TMDB", _
                            .Season = iSeason, _
                            .ShortLang = If(String.IsNullOrEmpty(image.Iso_639_1), String.Empty, image.Iso_639_1), _
                            .URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & image.FilePath, _
                            .URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & image.FilePath, _
                            .VoteAverage = image.VoteAverage.ToString, _
                            .VoteCount = image.VoteCount, _
                            .Width = image.Width.ToString}

                        alContainer.EpisodePosters.Add(tmpImage)
                    Next
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return alContainer
        End Function

        Public Function GetTMDBbyTVDB(ByRef tvdbID As String) As String
            Dim tmdbID As String = String.Empty

            Try
                tmdbID = _TMDBApi.Find(TMDbLib.Objects.Find.FindExternalSource.TvDb, tvdbID).TvResults.Item(0).Id.ToString

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
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