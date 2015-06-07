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
        Private _MySettings As TMDB.Scraper.sMySettings_ForScraper

        Friend WithEvents bwTMDB As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Methods"

        'Public Sub Cancel()
        '	If bwTMDB.IsBusy Then bwTMDB.CancelAsync()

        '	While bwTMDB.IsBusy
        '		Application.DoEvents()
        '		Threading.Thread.Sleep(50)
        '	End While
        'End Sub

        'Public Sub GetImagesAsync(ByVal imdbID As String, ByVal Type As Enums.ScraperCapabilities)
        '    Try
        '        If Not bwTMDB.IsBusy Then
        '            bwTMDB.WorkerSupportsCancellation = True
        '            bwTMDB.WorkerReportsProgress = True
        '            bwTMDB.RunWorkerAsync(New Arguments With {.Parameter = imdbID, .Type = Type})
        '        End If
        '    Catch ex As Exception
        '        logger.Error(New StackFrame().GetMethod().Name,ex)
        '    End Try
        'End Sub

        Public Function GetImages(ByVal TMDBID As String, ByVal Type As Enums.ScraperCapabilities_Movie_MovieSet, ByRef Settings As sMySettings_ForScraper, ByVal ContentType As Enums.Content_Type) As MediaContainers.ImagesContainer
            Dim alImagesContainer As New MediaContainers.ImagesContainer

            If bwTMDB.CancellationPending Then Return Nothing

            Try
                Dim TMDBClient = New TMDbLib.Client.TMDbClient(Settings.APIKey)
                TMDBClient.GetConfig()

                Dim Results As TMDbLib.Objects.General.Images = Nothing
                If ContentType = Enums.Content_Type.Movie Then
                    Results = TMDBClient.GetMovieImages(CInt(TMDBID))
                ElseIf ContentType = Enums.Content_Type.MovieSet Then
                    Results = TMDBClient.GetCollectionImages(CInt(TMDBID))
                End If

                If Results Is Nothing Then
                    Return Nothing
                End If

                'Fanart
                If (Type = Enums.ScraperCapabilities_Movie_MovieSet.All OrElse Type = Enums.ScraperCapabilities_Movie_MovieSet.Fanart) AndAlso Results.Backdrops IsNot Nothing Then
                    For Each image In Results.Backdrops
                        Dim tmpImage As New MediaContainers.Image With { _
                            .Height = image.Height.ToString, _
                            .Likes = 0, _
                            .LongLang = If(String.IsNullOrEmpty(image.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(image.Iso_639_1)), _
                            .ShortLang = If(String.IsNullOrEmpty(image.Iso_639_1), String.Empty, image.Iso_639_1), _
                            .ThumbURL = TMDBClient.Config.Images.BaseUrl & "w300" & image.FilePath, _
                            .URL = TMDBClient.Config.Images.BaseUrl & "original" & image.FilePath, _
                            .VoteAverage = image.VoteAverage.ToString, _
                            .VoteCount = image.VoteCount, _
                            .Width = image.Width.ToString}

                        alImagesContainer.Fanarts.Add(tmpImage)
                    Next
                End If

                'Poster
                If (Type = Enums.ScraperCapabilities_Movie_MovieSet.All OrElse Type = Enums.ScraperCapabilities_Movie_MovieSet.Poster) AndAlso Results.Posters IsNot Nothing Then
                    For Each image In Results.Posters
                        Dim tmpImage As New MediaContainers.Image With { _
                                .Height = image.Height.ToString, _
                                .Likes = 0, _
                                .LongLang = If(String.IsNullOrEmpty(image.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(image.Iso_639_1)), _
                                .ShortLang = If(String.IsNullOrEmpty(image.Iso_639_1), String.Empty, image.Iso_639_1), _
                                .ThumbURL = TMDBClient.Config.Images.BaseUrl & "w185" & image.FilePath, _
                                .URL = TMDBClient.Config.Images.BaseUrl & "original" & image.FilePath, _
                                .VoteAverage = image.VoteAverage.ToString, _
                                .VoteCount = image.VoteCount, _
                                .Width = image.Width.ToString}

                        If tmpImage.ShortLang = Settings.PrefLanguage Then
                            alImagesContainer.Posters.Add(tmpImage)
                        ElseIf tmpImage.ShortLang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alImagesContainer.Posters.Add(tmpImage)
                            End If
                        ElseIf tmpImage.ShortLang = "xx" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alImagesContainer.Posters.Add(tmpImage)
                            End If
                        ElseIf String.IsNullOrEmpty(tmpImage.ShortLang) Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alImagesContainer.Posters.Add(tmpImage)
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alImagesContainer.Posters.Add(tmpImage)
                            End If
                        End If
                    Next
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return alImagesContainer
        End Function

#End Region 'Methods

#Region "Nested Types"

        Private Structure Arguments

#Region "Fields"

            Dim Parameter As String
            Dim Type As Enums.ScraperCapabilities_Movie_MovieSet

#End Region 'Fields

        End Structure

        Structure sMySettings_ForScraper

#Region "Fields"
            Dim APIKey As String
            Dim PrefLanguage As String
            Dim PrefLanguageOnly As Boolean
            Dim GetEnglishImages As Boolean
            Dim GetBlankImages As Boolean
#End Region 'Fields

        End Structure

#End Region 'Nested Types

	End Class

End Namespace