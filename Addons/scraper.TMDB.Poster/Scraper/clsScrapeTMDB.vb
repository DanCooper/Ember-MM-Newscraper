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
		Private _TMDBApi As V3.Tmdb
        Private _TMDBApiE As V3.Tmdb
        Private _TMDBApiA As V3.Tmdb
        Private _MySettings As TMDB_Image.sMySettings

        'Friend WithEvents bwTMDB As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Methods"

        Public Sub New(ByRef tTMDBConf As V3.TmdbConfiguration, ByRef tTMDBConfE As V3.TmdbConfiguration, ByRef tTMDBApi As V3.Tmdb, ByRef tTMDBApiE As V3.Tmdb, ByRef tTMDBApiA As V3.Tmdb, ByRef tMySettings As TMDB_Image.sMySettings)
            _TMDBConf = tTMDBConf
            _TMDBConfE = tTMDBConfE
            _TMDBApi = tTMDBApi
            _TMDBApiE = tTMDBApiE
            _TMDBApiA = tTMDBApiA
            _MySettings = tMySettings
            ' v3 does not have description anymore
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

            ';If bwTMDB.CancellationPending Then Return Nothing
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

                    'If bwTMDB.CancellationPending Then Return Nothing

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
                                If tmpPoster.ShortLang = Settings.TMDBLanguage Then
                                    alPostersP.Add(tmpPoster)
                                ElseIf tmpPoster.ShortLang = "en" Then
                                    If Settings.FallBackEng Then
                                        alPostersE.Add(tmpPoster)
                                    End If
                                ElseIf tmpPoster.ShortLang = "xx" Then
                                    alPostersN.Add(tmpPoster)
                                ElseIf String.IsNullOrEmpty(tmpPoster.ShortLang) Then
                                    alPostersN.Add(tmpPoster)
                                Else
                                    If Not Settings.TMDBLanguagePrefOnly Then
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


        'Public Function GetTMDBCollectionImages(ByVal CollectionID As String, ByVal Type As Enums.ScraperCapabilities) As List(Of MediaContainers.Image)
        '    Dim alPosters As New List(Of MediaContainers.Image) 'main poster list
        '    Dim alPostersP As New List(Of MediaContainers.Image) 'preferred language poster list
        '    Dim alPostersE As New List(Of MediaContainers.Image) 'english poster list
        '    Dim alPostersO As New List(Of MediaContainers.Image) 'all others poster list
        '    Dim alPostersOs As New List(Of MediaContainers.Image) 'all others sorted poster list
        '    Dim alPostersN As New List(Of MediaContainers.Image) 'neutral/none language poster list

        '    Dim images As V3.TmdbCollectionImages
        '    Dim aW, aH As Integer

        '    ';If bwTMDB.CancellationPending Then Return Nothing
        '    Try
        '        images = _TMDBApiA.GetCollectionImages(CInt(CollectionID))
        '        If Type = Enums.ScraperCapabilities.Poster Then
        '            If IsNothing(images.posters) OrElse images.posters.Count = 0 Then
        '                images = _TMDBApiE.GetCollectionImages(CInt(CollectionID))
        '                If IsNothing(images.posters) OrElse images.posters.Count = 0 Then
        '                    Return alPosters
        '                End If
        '            End If
        '        Else
        '            If IsNothing(images.backdrops) OrElse images.backdrops.Count = 0 Then
        '                images = _TMDBApiE.GetCollectionImages(CInt(CollectionID))
        '                If IsNothing(images.backdrops) OrElse images.backdrops.Count = 0 Then
        '                    Return alPosters
        '                End If
        '            End If
        '        End If

        '        'If bwTMDB.WorkerReportsProgress Then
        '        '    bwTMDB.ReportProgress(1)
        '        'End If

        '        'If bwTMDB.CancellationPending Then Return Nothing

        '        If Type = Enums.ScraperCapabilities.Poster Then
        '            For Each tmdbI As V3.CollectionPoster In images.posters
        '                'If bwTMDB.CancellationPending Then Return Nothing
        '                For Each aSize In Master.eSize.poster_names
        '                    Select Case aSize.size
        '                        Case Master.eSize.poster_names(5).description
        '                            aW = tmdbI.width
        '                            aH = tmdbI.height
        '                        Case Else
        '                            aW = aSize.width
        '                            aH = CInt(aW / tmdbI.aspect_ratio)
        '                    End Select
        '                    Dim tmpPoster As New MediaContainers.Image With {.ShortLang = tmdbI.iso_639_1, .LongLang = If(String.IsNullOrEmpty(tmdbI.iso_639_1), "", Localization.ISOGetLangByCode2(tmdbI.iso_639_1)), .URL = _TMDBConf.images.base_url & aSize.size & tmdbI.file_path, .Description = aSize.description, .Width = CStr(aW), .Height = CStr(aH), .ParentID = tmdbI.file_path}
        '                    If tmpPoster.ShortLang = _MySettings.TMDBLanguage Then
        '                        alPostersP.Add(tmpPoster)
        '                    ElseIf tmpPoster.ShortLang = "en" Then
        '                        alPostersE.Add(tmpPoster)
        '                    ElseIf tmpPoster.ShortLang = "xx" Then
        '                        alPostersN.Add(tmpPoster)
        '                    ElseIf String.IsNullOrEmpty(tmpPoster.ShortLang) Then
        '                        alPostersN.Add(tmpPoster)
        '                    Else
        '                        alPostersO.Add(tmpPoster)
        '                    End If
        '                Next
        '            Next
        '            For Each xPoster As MediaContainers.Image In alPostersO.OrderBy(Function(p) (p.LongLang))
        '                alPostersOs.Add(xPoster)
        '            Next
        '            alPosters.AddRange(alPostersP)
        '            alPosters.AddRange(alPostersE)
        '            alPosters.AddRange(alPostersOs)
        '            alPosters.AddRange(alPostersN)
        '        ElseIf Type = Enums.ScraperCapabilities.Fanart Then
        '            For Each tmdbI As V3.CollectionBackdrop In images.backdrops
        '                'If bwTMDB.CancellationPending Then Return Nothing
        '                For Each aSize In Master.eSize.backdrop_names
        '                    Select Case aSize.size
        '                        Case Master.eSize.backdrop_names(3).description
        '                            aW = tmdbI.width
        '                            aH = tmdbI.height
        '                        Case Else
        '                            aW = aSize.width
        '                            aH = CInt(aW / tmdbI.aspect_ratio)
        '                    End Select
        '                    Dim tmpPoster As New MediaContainers.Image With {.ShortLang = tmdbI.iso_639_1, .LongLang = If(String.IsNullOrEmpty(tmdbI.iso_639_1), "", Localization.ISOGetLangByCode2(tmdbI.iso_639_1)), .URL = _TMDBConf.images.base_url & aSize.size & tmdbI.file_path, .Description = aSize.description, .Width = CStr(aW), .Height = CStr(aH), .ParentID = tmdbI.file_path}
        '                    alPosters.Add(tmpPoster)
        '                Next
        '            Next
        '        End If

        '        'If bwTMDB.WorkerReportsProgress Then
        '        '    bwTMDB.ReportProgress(3)
        '        'End If

        '    Catch ex As Exception
        '        logger.Error(New StackFrame().GetMethod().Name, ex)
        '    End Try

        '    Return alPosters
        'End Function

        '      Private Sub bwTMDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTMDB.DoWork
        '          Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        '          Try
        '              e.Result = GetTMDBImages(Args.Parameter, Args.Type)
        '          Catch ex As Exception
        '              logger.Error(New StackFrame().GetMethod().Name,ex)
        '              e.Result = Nothing
        '          End Try
        '      End Sub

        'Private Sub bwTMDB_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwTMDB.ProgressChanged
        '	If Not bwTMDB.CancellationPending Then
        '		RaiseEvent ProgressUpdated(e.ProgressPercentage)
        '	End If
        'End Sub

        'Private Sub bwTMDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTMDB.RunWorkerCompleted
        '	If Not IsNothing(e.Result) Then
        '		RaiseEvent PostersDownloaded(DirectCast(e.Result, List(Of MediaContainers.Image)))
        '	End If
        'End Sub

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
            Dim TMDBAPIKey As String
            Dim TMDBLanguage As String
            Dim TMDBLanguagePrefOnly As Boolean
            Dim FallBackEng As Boolean
#End Region 'Fields

        End Structure

#End Region 'Nested Types

	End Class

End Namespace