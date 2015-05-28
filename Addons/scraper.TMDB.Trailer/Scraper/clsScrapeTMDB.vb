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

Namespace TMDB

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Private _TMDBApi As TMDbLib.Client.TMDbClient  'preferred language
        Private _TMDBApiE As TMDbLib.Client.TMDbClient 'english language
        Private _MySettings As sMySettings_ForScraper
        Private strPrivateAPIKey As String = String.Empty

        Friend WithEvents bwTMDB As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Methods"

        Public Sub New(ByVal Settings As sMySettings_ForScraper)
            Try
                _MySettings = Settings

                _TMDBApi = New TMDbLib.Client.TMDbClient(Settings.ApiKey)
                _TMDBApi.GetConfig()
                _TMDBApi.DefaultLanguage = _MySettings.PrefLanguage

                If _MySettings.FallBackEng Then
                    _TMDBApiE = New TMDbLib.Client.TMDbClient(Settings.ApiKey)
                    _TMDBApiE.GetConfig()
                    _TMDBApiE.DefaultLanguage = "en"
                Else
                    _TMDBApiE = _TMDBApi
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub CancelAsync()
            If bwTMDB.IsBusy Then bwTMDB.CancelAsync()

            While bwTMDB.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End Sub

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

        Public Function GetTrailers(ByVal tmdbID As String) As List(Of Trailers)
            Dim alTrailers As New List(Of Trailers)
            Dim trailers As TMDbLib.Objects.Movies.Trailers
            Dim tLink As String
            Dim tName As String
            Dim tQualitiy As String

            If String.IsNullOrEmpty(tmdbID) OrElse Not Integer.TryParse(tmdbID, 0) Then Return alTrailers

            trailers = _TMDBApi.GetMovie(CInt(tmdbID), TMDbLib.Objects.Movies.MovieMethods.Trailers).Trailers
            If trailers.Youtube Is Nothing OrElse trailers.Youtube.Count = 0 AndAlso _MySettings.FallBackEng Then
                trailers = _TMDBApiE.GetMovie(CInt(tmdbID), TMDbLib.Objects.Movies.MovieMethods.Trailers).Trailers
                If trailers.Youtube Is Nothing OrElse trailers.Youtube.Count = 0 Then
                    Return alTrailers
                End If
            End If
            If trailers IsNot Nothing AndAlso trailers.Youtube IsNot Nothing Then
                For Each YTb As TMDbLib.Objects.Movies.Youtube In trailers.Youtube
                    tLink = String.Format("http://www.youtube.com/watch?v={0}", YTb.Source)
                    tName = GetYouTubeTitle(tLink)
                    tQualitiy = YTb.Size
                    alTrailers.Add(New Trailers With {.VideoURL = tLink, .WebURL = tLink, .Description = tName, .Source = "TMDB"})
                Next
            End If

            Return alTrailers
        End Function

        Public Shared Function GetYouTubeTitle(ByVal sURL As String) As String
            Dim oTitle As String
            Dim sHTTP As New HTTP

            Dim HTML As String = sHTTP.DownloadData(String.Concat(sURL))
            sHTTP = Nothing

            oTitle = YouTube.Scraper.GetVideoTitle(HTML)

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