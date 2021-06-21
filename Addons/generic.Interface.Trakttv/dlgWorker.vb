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
Imports TraktApiSharp

Public Class dlgWorker

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _client As clsAPITrakt
    Private _contenttype As Enums.ContentType

    Friend WithEvents bwGetWatchedState As New ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))

#End Region 'Events

#Region "Methods"

    Public Sub New(ByRef apitrakt As clsAPITrakt, ByVal contenttype As Enums.ContentType)
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
        _client = apitrakt
        _contenttype = contenttype
        SetUp()
    End Sub

    Private Sub btnCancel_Click() Handles btnCancel.Click
        If bwGetWatchedState.IsBusy Then
            bwGetWatchedState.CancelAsync()
        Else
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        btnStart.Enabled = False
        bwGetWatchedState = New ComponentModel.BackgroundWorker
        bwGetWatchedState.WorkerSupportsCancellation = True
        bwGetWatchedState.WorkerReportsProgress = True
        bwGetWatchedState.RunWorkerAsync()
    End Sub

    Private Sub bwGetWatchedState_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwGetWatchedState.DoWork
        Dim iItemsSynced As Integer
        Select Case _contenttype
            Case Enums.ContentType.Movie
                Dim lstWatchedMovies As IEnumerable(Of Objects.Get.Watched.TraktWatchedMovie) = Nothing
                bwGetWatchedState.ReportProgress(1)
                lstWatchedMovies = _client.GetWatched_Movies
                If bwGetWatchedState.CancellationPending Then
                    e.Result = New Results With {.Cancelled = bwGetWatchedState.CancellationPending}
                    Return
                End If
                If lstWatchedMovies IsNot Nothing Then
                    If lstWatchedMovies.Count > 0 Then
                        bwGetWatchedState.ReportProgress(2, lstWatchedMovies.Count)
                        Dim lstMoviesInDB = Master.DB.LoadAll_Movies
                        If bwGetWatchedState.CancellationPending Then
                            e.Result = New Results With {.Cancelled = bwGetWatchedState.CancellationPending}
                            Return
                        End If
                        For Each nWatchedMovie In lstWatchedMovies
                            If bwGetWatchedState.CancellationPending Then
                                e.Result = New Results With {.Cancelled = bwGetWatchedState.CancellationPending}
                                Return
                            End If
                            bwGetWatchedState.ReportProgress(3, nWatchedMovie.Movie.Title)
                            Dim lstDBElement = lstMoviesInDB.Where(Function(f) (nWatchedMovie.Movie.Ids.Imdb IsNot Nothing AndAlso f.Movie.UniqueIDs.IMDbId = nWatchedMovie.Movie.Ids.Imdb) OrElse
                                                                       (nWatchedMovie.Movie.Ids.Tmdb IsNot Nothing AndAlso f.Movie.UniqueIDs.TMDbId = CInt(nWatchedMovie.Movie.Ids.Tmdb)))
                            If lstDBElement IsNot Nothing Then
                                Dim strLastPlayed = Functions.ConvertToProperDateTime(nWatchedMovie.LastWatchedAt.Value.ToLocalTime.ToString)
                                Dim iPlayCount = nWatchedMovie.Plays.Value
                                For Each tDBElement In lstDBElement.Where(Function(f) Not f.Movie.LastPlayed = strLastPlayed OrElse
                                                                              Not f.Movie.PlayCount = iPlayCount)
                                    If tDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(tDBElement, False) Then
                                        tDBElement.Movie.LastPlayed = strLastPlayed
                                        tDBElement.Movie.PlayCount = iPlayCount
                                        Master.DB.Save_Movie(tDBElement, False, True, False, True, False)
                                        bwGetWatchedState.ReportProgress(4, tDBElement.ID)
                                        iItemsSynced += 1
                                        logger.Trace(String.Format("[TraktWorker] GetPlaycount_AllMovies: ""{0}"" | Synced to Ember", tDBElement.Movie.Title))
                                    Else
                                        logger.Warn(String.Format("[TraktWorker] GetPlaycount_AllMovies: ""{0}"" | NOT Synced to Ember, media was offline", tDBElement.Movie.Title))
                                    End If
                                Next
                            End If
                        Next
                        bwGetWatchedState.ReportProgress(6, iItemsSynced)
                    Else
                        bwGetWatchedState.ReportProgress(7)
                    End If
                Else
                    bwGetWatchedState.ReportProgress(9)
                End If
            Case Enums.ContentType.TVEpisode
                Dim lstWatchedShows As IEnumerable(Of Objects.Get.Watched.TraktWatchedShow) = Nothing
                bwGetWatchedState.ReportProgress(1)
                lstWatchedShows = _client.GetWatched_TVShows
                If bwGetWatchedState.CancellationPending Then
                    e.Result = New Results With {.Cancelled = bwGetWatchedState.CancellationPending}
                    Return
                End If
                If lstWatchedShows IsNot Nothing Then
                    If lstWatchedShows.Count > 0 Then
                        bwGetWatchedState.ReportProgress(2, lstWatchedShows.Count)
                        Dim lstTVShowsInDB = Master.DB.LoadAll_TVShows(False, True)
                        If bwGetWatchedState.CancellationPending Then
                            e.Result = New Results With {.Cancelled = bwGetWatchedState.CancellationPending}
                            Return
                        End If
                        For Each nWatchedShow In lstWatchedShows
                            Dim lstTVShowIDsToRefresh As New List(Of Long)
                            bwGetWatchedState.ReportProgress(3, nWatchedShow.Show.Title)
                            Dim lstDBTVShow = lstTVShowsInDB.Where(Function(f) (nWatchedShow.Show.Ids.Imdb IsNot Nothing AndAlso f.TVShow.UniqueIDs.IMDbId = nWatchedShow.Show.Ids.Imdb) OrElse
                                                                       (nWatchedShow.Show.Ids.Tmdb IsNot Nothing AndAlso f.TVShow.UniqueIDs.TMDbId = CInt(nWatchedShow.Show.Ids.Tmdb)) OrElse
                                                                       (nWatchedShow.Show.Ids.Tvdb IsNot Nothing AndAlso f.TVShow.UniqueIDs.TVDbId = CInt(nWatchedShow.Show.Ids.Tvdb)))
                            If lstDBTVShow IsNot Nothing Then
                                For Each nWatchedSeason In nWatchedShow.Seasons
                                    For Each nWatchedEpisode In nWatchedSeason.Episodes
                                        If bwGetWatchedState.CancellationPending Then
                                            e.Result = New Results With {.Cancelled = bwGetWatchedState.CancellationPending}
                                            Return
                                        End If
                                        Dim strLastPlayed = Functions.ConvertToProperDateTime(nWatchedEpisode.LastWatchedAt.Value.ToLocalTime.ToString)
                                        Dim iPlayCount = nWatchedEpisode.Plays.Value
                                        For Each nTVShow In lstDBTVShow
                                            If nTVShow.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(nTVShow, False) Then
                                                For Each tDBTVEpisode In nTVShow.Episodes.Where(Function(f) (Not f.TVEpisode.LastPlayed = strLastPlayed OrElse
                                                                                                    Not f.TVEpisode.Playcount = iPlayCount) AndAlso
                                                                                                    (nWatchedSeason.Number IsNot Nothing AndAlso f.TVEpisode.Season = CInt(nWatchedSeason.Number)) AndAlso
                                                                                                    (nWatchedEpisode.Number IsNot Nothing AndAlso f.TVEpisode.Episode = CInt(nWatchedEpisode.Number)))
                                                    If tDBTVEpisode.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(tDBTVEpisode, False) Then
                                                        tDBTVEpisode.TVEpisode.LastPlayed = strLastPlayed
                                                        tDBTVEpisode.TVEpisode.Playcount = iPlayCount
                                                        Master.DB.Save_TVEpisode(tDBTVEpisode, False, True, False, False, True, False)
                                                        bwGetWatchedState.ReportProgress(4, tDBTVEpisode.ID)
                                                        lstTVShowIDsToRefresh.Add(nTVShow.ID)
                                                        iItemsSynced += 1
                                                        logger.Trace(String.Format("[TraktWorker] GetPlaycount_AllTVEpisodes: ""{0}: S{1}E{2} - {3}"" | Synced to Ember",
                                                                                   tDBTVEpisode.TVShow.Title,
                                                                                   tDBTVEpisode.TVEpisode.Season,
                                                                                   tDBTVEpisode.TVEpisode.Episode,
                                                                                   tDBTVEpisode.TVEpisode.Title))
                                                    Else
                                                        logger.Warn(String.Format("[TraktWorker] GetPlaycount_AllTVEpisodes: ""{0}: S{1}E{2} - {3}"" | NOT Synced to Ember, media was offline",
                                                                                   tDBTVEpisode.TVShow.Title,
                                                                                   tDBTVEpisode.TVEpisode.Season,
                                                                                   tDBTVEpisode.TVEpisode.Episode,
                                                                                   tDBTVEpisode.TVEpisode.Title))
                                                    End If
                                                Next
                                            Else
                                                logger.Warn(String.Format("[TraktWorker] GetPlaycount_AllTVEpisodes: ""{0}"" | NOT Synced to Ember, tv show was offline", nTVShow.TVShow.Title))
                                            End If
                                        Next
                                    Next
                                Next
                            End If
                            If lstTVShowIDsToRefresh.Count > 0 Then
                                bwGetWatchedState.ReportProgress(5, lstTVShowIDsToRefresh.Distinct.ToList)
                            End If
                        Next
                        bwGetWatchedState.ReportProgress(6, iItemsSynced)
                    Else
                        bwGetWatchedState.ReportProgress(7)
                    End If
                Else
                    bwGetWatchedState.ReportProgress(9)
                End If
        End Select
        e.Result = New Results With {.Cancelled = bwGetWatchedState.CancellationPending}
    End Sub

    Private Sub bwGetWatchedState_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwGetWatchedState.ProgressChanged
        Select Case e.ProgressPercentage
            Case 1
                'State 2: Load from Trakt  started
                lblOverallStateInfo.Text = "Get watched media from Trakt"
                prgbOverallState.Maximum = 4
                prgbOverallState.Value = 1
                prgbCurrentItem.Value = 0
            Case 2
                'State 2: Load from Trakt  done, report count of watched media on Trakt, load all media from database
                'e.UserState = count of watched media on Trakt  
                lblOverallStateInfo.Text = "Load media from database"
                prgbCurrentItem.Maximum = CInt(e.UserState)
                prgbOverallState.Value = 2
            Case 3
                'State 3: Sync Ember with Trakt started, report progress
                'e.UserState = media Title
                lblCurrentItemInfo.Text = CStr(e.UserState)
                lblOverallStateInfo.Text = "Sync Ember with Trakt"
                prgbCurrentItem.Value = prgbCurrentItem.Value + 1
                prgbOverallState.Value = 3
            Case 4
                'update entry in media list
                'e.UserState = media ID
                Select Case _contenttype
                    Case Enums.ContentType.Movie
                        RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {CLng(e.UserState)}))
                    Case Enums.ContentType.TVEpisode
                        RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {CLng(e.UserState)}))
                End Select
            Case 5
                'update tv show entry in media list
                'e.UserState = tv show IDs as "List(Of Long)"
                For Each nID In CType(e.UserState, List(Of Long))
                    RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVShow, New List(Of Object)(New Object() {nID}))
                Next
            Case 6
                'State 5: finished
                lblCurrentItemInfo.Text = String.Format("{0}: {1} item(s) synced", Master.eLang.GetString(362, "Done"), CInt(e.UserState))
                lblOverallStateInfo.Text = Master.eLang.GetString(362, "Done")
                prgbOverallState.Value = 4
                btnCancel.Text = Master.eLang.GetString(19, "Close")
            Case 7
                'no watched media found in Trakt
                lblCurrentItemInfo.Text = "No watched media found on Trakt"
                lblOverallStateInfo.Text = Master.eLang.GetString(362, "Done")
                prgbCurrentItem.Maximum = 100
                prgbCurrentItem.Value = 100
                prgbOverallState.Value = 4
            Case 9
                'Error
        End Select
    End Sub

    Private Sub bwGetWatchedState_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwGetWatchedState.RunWorkerCompleted
        Dim Result As Results = DirectCast(e.Result, Results)
        If Result.Cancelled Then
            lblCurrentItemInfo.Text = String.Empty
            lblOverallStateInfo.Text = Master.eLang.GetString(396, "Cancelled")
            prgbCurrentItem.Value = 0
            prgbOverallState.Value = 0
            btnStart.Enabled = True
            btnStart.Text = Master.eLang.GetString(1443, "Start Syncing")
        End If
    End Sub

    Private Sub SetUp()
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        btnStart.Text = Master.eLang.GetString(1443, "Start Syncing")
        lblCurrentItemInfo.Text = String.Empty
        lblOverallStateInfo.Text = String.Empty
        Select Case _contenttype
            Case Enums.ContentType.Movie
                lblTitle.Text = "Trakt movie watched state syncing"
            Case Enums.ContentType.TVEpisode
                lblTitle.Text = "Trakt episode watched state syncing"
        End Select
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Results

#Region "Fields"

        Dim Cancelled As Boolean

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class