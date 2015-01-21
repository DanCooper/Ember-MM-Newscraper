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

Imports System.Text.RegularExpressions
Imports EmberAPI
Imports WatTmdb
Imports NLog

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
        Private _MySettings As sMySettings_ForScraper
        Private strPrivateAPIKey As String = String.Empty

        'Friend WithEvents bwTMDB As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Methods"

        Public Sub New(ByVal Settings As sMySettings_ForScraper)
            Try
                _TMDBApi = New WatTmdb.V3.Tmdb(Settings.ApiKey, Settings.PrefLanguage)
                If IsNothing(_TMDBApi) Then
                    logger.Error(Master.eLang.GetString(938, "TheMovieDB API is missing or not valid"), _TMDBApi.Error.status_message)
                Else
                    If Not IsNothing(_TMDBApi.Error) AndAlso _TMDBApi.Error.status_message.Length > 0 Then
                        logger.Error(_TMDBApi.Error.status_message, _TMDBApi.Error.status_code.ToString())
                    End If
                End If
                _TMDBConf = _TMDBApi.GetConfiguration()
                _TMDBApiE = New WatTmdb.V3.Tmdb(Settings.ApiKey)
                _TMDBConfE = _TMDBApiE.GetConfiguration()
                _TMDBApiA = New WatTmdb.V3.Tmdb(Settings.ApiKey, String.Empty)
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

        Public Function GetTMDBTrailers(ByVal TMDBID As String) As List(Of Trailers)
            Dim alTrailers As New List(Of Trailers)
            Dim trailers As V3.TmdbMovieTrailers
            Dim tLink As String
            Dim tName As String

            Try
                If Not String.IsNullOrEmpty(TMDBID) Then
                    trailers = _TMDBApi.GetMovieTrailers(CInt(TMDBID))
                    If IsNothing(trailers.youtube) OrElse trailers.youtube.Count = 0 AndAlso _MySettings.FallBackEng Then
                        trailers = _TMDBApiE.GetMovieTrailers(CInt(TMDBID))
                        If IsNothing(trailers.youtube) OrElse trailers.youtube.Count = 0 Then
                            Return alTrailers
                        End If
                    End If
                    For Each YTb As V3.Youtube In trailers.youtube
                        tLink = String.Format("http://www.youtube.com/watch?v={0}", YTb.source)
                        tName = GetYouTubeTitle(tLink)
                        alTrailers.Add(New Trailers With {.VideoURL = tLink, .WebURL = tLink, .Description = tName, .Source = "TMDB"})
                    Next
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return alTrailers
        End Function

        Public Shared Function GetYouTubeTitle(ByVal sURL As String) As String
            Dim oTitle As String
            Dim sHTTP As New HTTP

            Dim HTML As String = sHTTP.DownloadData(String.Concat(sURL))
            sHTTP = Nothing

            oTitle = Regex.Match(HTML, "<meta property=""og:title"" content=""(.*?)"">", RegexOptions.Singleline Or RegexOptions.IgnoreCase Or RegexOptions.Multiline).Groups(1).Value.ToString.Trim
            oTitle = HttpUtility.HtmlDecode(oTitle)

            Return oTitle
        End Function

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
            Dim Type As Enums.ScraperCapabilities_Movie_MovieSet

#End Region 'Fields

        End Structure

        Structure sMySettings_ForScraper

#Region "Fields"

            Dim ApiKey As String
            Dim FallBackEng As Boolean
            Dim PrefLanguage As String

#End Region 'Fields

        End Structure

#End Region 'Nested Types

    End Class

End Namespace