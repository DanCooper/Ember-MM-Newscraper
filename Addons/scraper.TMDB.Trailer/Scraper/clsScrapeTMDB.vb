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
        Private _MySettings As TMDB_Trailer.SpecialSettings

        Friend WithEvents bwTMDB As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Methods"

        Public Sub New(ByVal Settings As TMDB_Trailer.SpecialSettings)
            Try
                _MySettings = Settings

                _TMDBApi = New TMDbLib.Client.TMDbClient(_MySettings.APIKey)
                _TMDBApi.GetConfig()
                _TMDBApi.DefaultLanguage = _MySettings.PrefLanguage

                If _MySettings.FallBackEng Then
                    _TMDBApiE = New TMDbLib.Client.TMDbClient(_MySettings.APIKey)
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

        Public Function GetTrailers(ByVal tmdbID As String) As List(Of MediaContainers.Trailer)
            Dim alTrailers As New List(Of MediaContainers.Trailer)
            Dim trailers As TMDbLib.Objects.General.ResultContainer(Of TMDbLib.Objects.General.Video)

            If String.IsNullOrEmpty(tmdbID) OrElse Not Integer.TryParse(tmdbID, 0) Then Return alTrailers

            Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)
            APIResult = Task.Run(Function() _TMDBApi.GetMovie(CInt(tmdbID), TMDbLib.Objects.Movies.MovieMethods.Videos))

            trailers = APIResult.Result.Videos
            If trailers.Results Is Nothing OrElse trailers.Results.Count = 0 AndAlso _MySettings.FallBackEng Then
                APIResult = Task.Run(Function() _TMDBApiE.GetMovie(CInt(tmdbID), TMDbLib.Objects.Movies.MovieMethods.Videos))
                trailers = APIResult.Result.Videos
                If trailers.Results Is Nothing OrElse trailers.Results.Count = 0 Then
                    Return alTrailers
                End If
            End If
            If trailers IsNot Nothing AndAlso trailers.Results IsNot Nothing Then
                For Each Video As TMDbLib.Objects.General.Video In trailers.Results.Where(Function(f) f.Site = "YouTube")
                    Dim tLink As String = String.Format("http://www.youtube.com/watch?v={0}", Video.Key)
                    If YouTube.Scraper.IsAvailable(tLink) Then
                        Dim tName As String = YouTube.Scraper.GetVideoTitle(tLink)
                        alTrailers.Add(New MediaContainers.Trailer With {
                                           .LongLang = If(String.IsNullOrEmpty(Video.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(Video.Iso_639_1)),
                                           .Quality = GetVideoQuality(Video.Size),
                                           .Scraper = "TMDB",
                                           .ShortLang = If(String.IsNullOrEmpty(Video.Iso_639_1), String.Empty, Video.Iso_639_1),
                                           .Source = Video.Site,
                                           .Title = tName,
                                           .Type = GetVideoType(Video.Type),
                                           .URLWebsite = tLink})
                    End If
                Next
            End If

            Return alTrailers
        End Function

        Private Function GetVideoQuality(ByRef Size As Integer) As Enums.TrailerVideoQuality
            If Size = 0 Then Return Enums.TrailerVideoQuality.Any

            Select Case Size
                Case 1080
                    Return Enums.TrailerVideoQuality.HD1080p
                Case 720
                    Return Enums.TrailerVideoQuality.HD720p
                Case 480
                    Return Enums.TrailerVideoQuality.HQ480p
            End Select

            Return Enums.TrailerVideoQuality.Any
        End Function

        Private Function GetVideoType(ByRef Type As String) As Enums.TrailerType
            If String.IsNullOrEmpty(Type) Then Return Enums.TrailerType.Any

            Select Case Type.ToLower
                Case "clip"
                    Return Enums.TrailerType.Clip
                Case "featurette"
                    Return Enums.TrailerType.Featurette
                Case "teaser"
                    Return Enums.TrailerType.Teaser
                Case "trailer"
                    Return Enums.TrailerType.Trailer
            End Select

            Return Enums.TrailerType.Any
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
            Dim Type As Enums.ModifierType

#End Region 'Fields

        End Structure

#End Region 'Nested Types

    End Class

End Namespace