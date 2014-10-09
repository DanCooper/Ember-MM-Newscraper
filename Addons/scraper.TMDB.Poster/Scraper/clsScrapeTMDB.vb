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
Imports WatTmdb
Imports NLog
Imports System.Diagnostics

Namespace TMDB

	Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
		Private _TMDBConf As V3.TmdbConfiguration
        Private _TMDBConfE As V3.TmdbConfiguration
        Private _TMDBConfA As V3.TmdbConfiguration
        Private _TMDBApi As V3.Tmdb 'preferred language
        Private _TMDBApiE As V3.Tmdb 'english language
        Private _TMDBApiA As V3.Tmdb 'all languages
        Private _MySettings As TMDB.Scraper.sMySettings_ForScraper

        Friend WithEvents bwTMDB As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Methods"

        Public Sub New(ByVal Settings As sMySettings_ForScraper)
            Try
                _TMDBApi = New WatTmdb.V3.Tmdb(Settings.APIKey, Settings.PrefLanguage)
                If IsNothing(_TMDBApi) Then
                    logger.Error(Master.eLang.GetString(938, "TheMovieDB API is missing or not valid"), _TMDBApi.Error.status_message)
                Else
                    If Not IsNothing(_TMDBApi.Error) AndAlso _TMDBApi.Error.status_message.Length > 0 Then
                        logger.Error(_TMDBApi.Error.status_message, _TMDBApi.Error.status_code.ToString())
                    End If
                End If
                _TMDBConf = _TMDBApi.GetConfiguration()
                _TMDBApiE = New WatTmdb.V3.Tmdb(Settings.APIKey)
                _TMDBConfE = _TMDBApiE.GetConfiguration()
                _TMDBApiA = New WatTmdb.V3.Tmdb(Settings.APIKey, String.Empty)
                _TMDBConfA = _TMDBApiA.GetConfiguration()

                _MySettings = Settings
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

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

        Public Function GetTMDBImages(ByVal TMDBID As String, ByVal Type As Enums.ScraperCapabilities, ByRef Settings As sMySettings_ForScraper) As List(Of MediaContainers.Image)
            Dim alPosters As New List(Of MediaContainers.Image) 'main poster list
            Dim alPostersP As New List(Of MediaContainers.Image) 'preferred language poster list
            Dim alPostersE As New List(Of MediaContainers.Image) 'english poster list
            Dim alPostersO As New List(Of MediaContainers.Image) 'all others poster list
            Dim alPostersOs As New List(Of MediaContainers.Image) 'all others sorted poster list
            Dim alPostersN As New List(Of MediaContainers.Image) 'neutral/none language poster list

            Dim images As V3.TmdbMovieImages
            Dim aW, aH As Integer

            If bwTMDB.CancellationPending Then Return Nothing

            Try
                If Not String.IsNullOrEmpty(TMDBID) Then
                    images = _TMDBApiA.GetMovieImages(CInt(TMDBID))
                    If Type = Enums.ScraperCapabilities.Poster Then
                        If IsNothing(images.posters) OrElse images.posters.Count = 0 Then
                            images = _TMDBApiE.GetMovieImages(CInt(TMDBID))
                            If IsNothing(images.posters) OrElse images.posters.Count = 0 Then
                                Return alPosters
                            End If
                        End If
                    Else
                        If IsNothing(images.backdrops) OrElse images.backdrops.Count = 0 Then
                            images = _TMDBApiE.GetMovieImages(CInt(TMDBID))
                            If IsNothing(images.backdrops) OrElse images.backdrops.Count = 0 Then
                                Return alPosters
                            End If
                        End If
                    End If

                    'If bwTMDB.WorkerReportsProgress Then
                    '    bwTMDB.ReportProgress(1)
                    'End If

                    If bwTMDB.CancellationPending Then Return Nothing

                    If Type = Enums.ScraperCapabilities.Poster Then
                        For Each tmdbI As V3.Poster In images.posters
                            'If bwTMDB.CancellationPending Then Return Nothing
                            For Each aSize In Master.eSize.poster_names
                                Select Case aSize.size
                                    Case Master.eSize.poster_names(5).description
                                        aW = tmdbI.width
                                        aH = tmdbI.height
                                    Case Else
                                        aW = aSize.width
                                        aH = CInt(aW / tmdbI.aspect_ratio)
                                End Select
                                Dim tmpPoster As New MediaContainers.Image With {.ShortLang = tmdbI.iso_639_1, .LongLang = If(String.IsNullOrEmpty(tmdbI.iso_639_1), "", Localization.ISOGetLangByCode2(tmdbI.iso_639_1)), .URL = _TMDBConf.images.base_url & aSize.size & tmdbI.file_path, .Description = aSize.description, .Width = CStr(aW), .Height = CStr(aH), .ParentID = tmdbI.file_path}
                                If tmpPoster.ShortLang = Settings.PrefLanguage Then
                                    alPostersP.Add(tmpPoster)
                                ElseIf tmpPoster.ShortLang = "en" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                        alPostersE.Add(tmpPoster)
                                    End If
                                ElseIf tmpPoster.ShortLang = "xx" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                        alPostersN.Add(tmpPoster)
                                    End If
                                ElseIf String.IsNullOrEmpty(tmpPoster.ShortLang) Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                        alPostersN.Add(tmpPoster)
                                    End If
                                Else
                                    If Not Settings.PrefLanguageOnly Then
                                        alPostersO.Add(tmpPoster)
                                    End If
                                End If
                            Next
                        Next
                        For Each xPoster As MediaContainers.Image In alPostersO.OrderBy(Function(p) (p.LongLang))
                            alPostersOs.Add(xPoster)
                        Next
                        alPosters.AddRange(alPostersP)
                        alPosters.AddRange(alPostersE)
                        alPosters.AddRange(alPostersOs)
                        alPosters.AddRange(alPostersN)
                    ElseIf Type = Enums.ScraperCapabilities.Fanart Then
                        For Each tmdbI As V3.Backdrop In images.backdrops
                            'If bwTMDB.CancellationPending Then Return Nothing
                            For Each aSize In Master.eSize.backdrop_names
                                Select Case aSize.size
                                    Case Master.eSize.backdrop_names(3).description
                                        aW = tmdbI.width
                                        aH = tmdbI.height
                                    Case Else
                                        aW = aSize.width
                                        aH = CInt(aW / tmdbI.aspect_ratio)
                                End Select
                                Dim tmpPoster As New MediaContainers.Image With {.ShortLang = tmdbI.iso_639_1, .LongLang = If(String.IsNullOrEmpty(tmdbI.iso_639_1), "", Localization.ISOGetLangByCode2(tmdbI.iso_639_1)), .URL = _TMDBConf.images.base_url & aSize.size & tmdbI.file_path, .Description = aSize.description, .Width = CStr(aW), .Height = CStr(aH), .ParentID = tmdbI.file_path}
                                alPosters.Add(tmpPoster)
                            Next
                        Next
                    End If
                End If
                'If bwTMDB.WorkerReportsProgress Then
                '    bwTMDB.ReportProgress(3)
                'End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return alPosters
        End Function

#End Region 'Methods

#Region "Nested Types"

        Private Structure Arguments

#Region "Fields"

            Dim Parameter As String
            Dim Type As Enums.ScraperCapabilities

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