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

Imports NLog

Public Class TaskManager

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private TaskList As New Queue(Of TaskItem)

    Friend WithEvents bwTaskManager As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Events"

    Public Event ProgressUpdate(ByVal eProgressValue As ProgressValue)
    Public Event TaskManagerDone()

#End Region 'Events

#Region "Properties"

    ReadOnly Property IsBusy() As Boolean
        Get
            Return bwTaskManager.IsBusy
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub AddTask(ByVal tTaskItem As TaskItem)
        TaskList.Enqueue(tTaskItem)
        If Not bwTaskManager.IsBusy Then
            RunTaskManager()
        Else
            'ChangeTaskManagerStatus(lblTaskManagerStatus, String.Concat("Pending Tasks: ", (TaskList.Count + 1).ToString))
        End If
    End Sub

    Private Sub bwTaskManager_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTaskManager.DoWork
        While TaskList.Count > 0
            If bwTaskManager.CancellationPending Then Return

            Dim currTask As TaskItem = TaskList.Dequeue()

            Select Case currTask.TaskType
                Case Enums.TaskManagerType.CopyBackdrops
                    CopyBackdrops(currTask)

                Case Enums.TaskManagerType.GetMissingEpisodes
                    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        GetMissingEpisodes(currTask)
                        SQLtransaction.Commit()
                    End Using

                Case Enums.TaskManagerType.SetLockedState
                    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        SetLockedState(currTask)
                        SQLtransaction.Commit()
                    End Using

                Case Enums.TaskManagerType.SetMarkedState
                    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        SetMarkedState(currTask)
                        SQLtransaction.Commit()
                    End Using

                Case Enums.TaskManagerType.SetWatchedState
                    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        SetWatchedState(currTask)
                        SQLtransaction.Commit()
                    End Using
            End Select
        End While
    End Sub

    Private Sub bwTaskManager_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwTaskManager.ProgressChanged
        Dim tProgressValue As ProgressValue = DirectCast(e.UserState, ProgressValue)
        RaiseEvent ProgressUpdate(tProgressValue)
    End Sub

    Private Sub bwTaskManager_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTaskManager.RunWorkerCompleted
        RaiseEvent TaskManagerDone()
    End Sub

    Public Sub Cancel()
        If bwTaskManager.IsBusy Then bwTaskManager.CancelAsync()
    End Sub

    Public Sub CancelAndWait()
        If bwTaskManager.IsBusy Then bwTaskManager.CancelAsync()
        While bwTaskManager.IsBusy
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub CopyBackdrops(ByVal currTask As TaskItem)
        Select Case currTask.ContentType
            Case Enums.ContentType.Movie
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLcommand.CommandText = "SELECT ListTitle, FanartPath FROM movielist WHERE FanartPath IS NOT NULL AND NOT FanartPath='' ORDER BY ListTitle;"
                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        While SQLreader.Read
                            If bwTaskManager.CancellationPending Then Return
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                             .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                             .Message = SQLreader("ListTitle").ToString})

                            FileUtils.Common.CopyFanartToBackdropsPath(SQLreader("FanartPath").ToString, Enums.ContentType.Movie)
                        End While
                    End Using
                End Using
        End Select
    End Sub

    Private Sub GetMissingEpisodes(ByVal tTaskItem As TaskItem)
        Select Case tTaskItem.ContentType
            Case Enums.ContentType.TVShow
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bNewSeasons As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(tID, True, True, True)

                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                 .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                 .Message = tmpDBElement.TVShow.Title})

                    Dim ScrapeModifiers As New Structures.ScrapeModifiers
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainNFO, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withEpisodes, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withSeasons, True)

                    If Not ModulesManager.Instance.ScrapeData_TVShow(tmpDBElement, ScrapeModifiers, Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, True) Then
                        For Each nMissingSeason In tmpDBElement.Seasons.Where(Function(f) Not f.IDSpecified)
                            Master.DB.Save_TVSeason(nMissingSeason, True, False, False)
                            bNewSeasons = True
                        Next
                        For Each nMissingEpisode In tmpDBElement.Episodes.Where(Function(f) Not f.FilenameSpecified)
                            Master.DB.Save_TVEpisode(nMissingEpisode, True, False, False, False, False)
                        Next
                    End If

                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                 .ContentType = Enums.ContentType.TVShow,
                                                 .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                 .ID = tmpDBElement.ID})

                Next
        End Select
    End Sub

    Private Sub SetLockedState(ByVal tTaskItem As TaskItem)
        Select Case tTaskItem.ContentType

            Case Enums.ContentType.Movie
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(tID)

                    If tTaskItem.CommonBoolean Then
                        If Not tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = False
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                             .Message = tmpDBElement.Movie.Title})

                        Master.DB.Save_Movie(tmpDBElement, True, True, False, False, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.Movie,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

            Case Enums.ContentType.MovieSet
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_MovieSet(tID)

                    If tTaskItem.CommonBoolean Then
                        If Not tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = False
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                             .Message = tmpDBElement.MovieSet.Title})

                        Master.DB.Save_MovieSet(tmpDBElement, True, True, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.MovieSet,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

            Case Enums.ContentType.TVEpisode
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVEpisode(tID, True)

                    If tTaskItem.CommonBoolean Then
                        If Not tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = False
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                             .Message = tmpDBElement.TVEpisode.Title})

                        Master.DB.Save_TVEpisode(tmpDBElement, True, True, False, False, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVEpisode,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

            Case Enums.ContentType.TVSeason
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVSeason(tID, True, False)

                    If tTaskItem.CommonBoolean Then
                        If Not tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = False
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                             .Message = String.Format("{0}: {1} {2}", tmpDBElement.TVShow.Title, Master.eLang.GetString(650, "Season"), tmpDBElement.TVSeason.Season.ToString)})

                        Master.DB.Save_TVSeason(tmpDBElement, True, True, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVSeason,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

            Case Enums.ContentType.TVShow
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(tID, True, True)

                    If tTaskItem.CommonBoolean Then
                        If Not tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = False
                            bHasChanged = True
                        End If
                    End If

                    'Season handling
                    Dim lstSeasonsIDs As New List(Of Long)
                    For Each nTVSeason As Database.DBElement In tmpDBElement.Seasons
                        lstSeasonsIDs.Add(nTVSeason.ID)
                        'overwrite the season Locked state to save it in tvshow.nfo
                        nTVSeason.IsLock = tmpDBElement.IsLock
                    Next
                    If lstSeasonsIDs.Count > 0 Then
                        SetLockedState(New TaskItem With {.CommonBoolean = tmpDBElement.IsLock, .ContentType = Enums.ContentType.TVSeason, .ListOfID = lstSeasonsIDs, .TaskType = Enums.TaskManagerType.SetLockedState})
                    End If

                    'Episode handling
                    Dim lstEpisodeIDs As New List(Of Long)
                    For Each nTVEpisode As Database.DBElement In tmpDBElement.Episodes
                        lstEpisodeIDs.Add(nTVEpisode.ID)
                    Next
                    If lstEpisodeIDs.Count > 0 Then
                        SetLockedState(New TaskItem With {.CommonBoolean = tmpDBElement.IsLock, .ContentType = Enums.ContentType.TVEpisode, .ListOfID = lstEpisodeIDs, .TaskType = Enums.TaskManagerType.SetLockedState})
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                             .Message = tmpDBElement.TVShow.Title})

                        Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVShow,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

        End Select
    End Sub

    Private Sub SetMarkedState(ByVal tTaskItem As TaskItem)
        Select Case tTaskItem.ContentType

            Case Enums.ContentType.Movie
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(tID)


                    'Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    '    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    '        Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "Mark")
                    '        Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, "idMovie")
                    '        SQLcommand.CommandText = "UPDATE movie SET Mark = (?) WHERE idMovie = (?);"
                    '        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    '            parMark.Value = If(dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("Mark").Value))
                    '            parID.Value = sRow.Cells("idMovie").Value
                    '            SQLcommand.ExecuteNonQuery()
                    '            sRow.Cells("Mark").Value = parMark.Value
                    '        Next
                    '    End Using
                    '    SQLtransaction.Commit()
                    'End Using




                    If tTaskItem.CommonBoolean Then
                        If Not tmpDBElement.IsMark Then
                            tmpDBElement.IsMark = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsMark Then
                            tmpDBElement.IsMark = False
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                             .Message = tmpDBElement.Movie.Title})

                        Master.DB.Save_Movie(tmpDBElement, True, False, False, False, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.Movie,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

            Case Enums.ContentType.MovieSet
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_MovieSet(tID)

                    If tTaskItem.CommonBoolean Then
                        If Not tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = False
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                             .Message = tmpDBElement.MovieSet.Title})

                        Master.DB.Save_MovieSet(tmpDBElement, True, True, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.MovieSet,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

            Case Enums.ContentType.TVEpisode
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVEpisode(tID, True)

                    If tTaskItem.CommonBoolean Then
                        If Not tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = False
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                             .Message = tmpDBElement.TVEpisode.Title})

                        Master.DB.Save_TVEpisode(tmpDBElement, True, True, False, False, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVEpisode,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

            Case Enums.ContentType.TVSeason
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVSeason(tID, True, False)

                    If tTaskItem.CommonBoolean Then
                        If Not tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = False
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                             .Message = String.Format("{0}: {1} {2}", tmpDBElement.TVShow.Title, Master.eLang.GetString(650, "Season"), tmpDBElement.TVSeason.Season.ToString)})

                        Master.DB.Save_TVSeason(tmpDBElement, True, True, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVSeason,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

            Case Enums.ContentType.TVShow
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(tID, True, True)

                    If tTaskItem.CommonBoolean Then
                        If Not tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsLock Then
                            tmpDBElement.IsLock = False
                            bHasChanged = True
                        End If
                    End If

                    'Season handling
                    Dim lstSeasonsIDs As New List(Of Long)
                    For Each nTVSeason As Database.DBElement In tmpDBElement.Seasons
                        lstSeasonsIDs.Add(nTVSeason.ID)
                        'overwrite the season Locked state to save it in tvshow.nfo
                        nTVSeason.IsLock = tmpDBElement.IsLock
                    Next
                    If lstSeasonsIDs.Count > 0 Then
                        SetLockedState(New TaskItem With {.CommonBoolean = tmpDBElement.IsLock, .ContentType = Enums.ContentType.TVSeason, .ListOfID = lstSeasonsIDs, .TaskType = Enums.TaskManagerType.SetLockedState})
                    End If

                    'Episode handling
                    Dim lstEpisodeIDs As New List(Of Long)
                    For Each nTVEpisode As Database.DBElement In tmpDBElement.Episodes
                        lstEpisodeIDs.Add(nTVEpisode.ID)
                    Next
                    If lstEpisodeIDs.Count > 0 Then
                        SetLockedState(New TaskItem With {.CommonBoolean = tmpDBElement.IsLock, .ContentType = Enums.ContentType.TVEpisode, .ListOfID = lstEpisodeIDs, .TaskType = Enums.TaskManagerType.SetLockedState})
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                             .Message = tmpDBElement.TVShow.Title})

                        Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVShow,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

        End Select
    End Sub

    Private Sub SetWatchedState(ByVal tTaskItem As TaskItem)
        Select Case tTaskItem.ContentType

            Case Enums.ContentType.Movie
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(tID)

                    If tTaskItem.CommonBoolean Then
                        If Not tmpDBElement.Movie.LastPlayedSpecified OrElse Not tmpDBElement.Movie.PlayCountSpecified Then
                            tmpDBElement.Movie.LastPlayed = If(tmpDBElement.Movie.LastPlayedSpecified, tmpDBElement.Movie.LastPlayed, Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                            tmpDBElement.Movie.PlayCount = If(tmpDBElement.Movie.PlayCountSpecified, tmpDBElement.Movie.PlayCount, 1)
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.Movie.LastPlayedSpecified OrElse tmpDBElement.Movie.PlayCountSpecified Then
                            tmpDBElement.Movie.LastPlayed = String.Empty
                            tmpDBElement.Movie.PlayCount = 0
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                         .Message = tmpDBElement.Movie.Title})

                        Master.DB.Save_Movie(tmpDBElement, True, True, False, True, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.Movie,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

            Case Enums.ContentType.TVEpisode
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVEpisode(tID, True)

                    If tTaskItem.CommonBoolean Then
                        If Not tmpDBElement.TVEpisode.LastPlayedSpecified OrElse Not tmpDBElement.TVEpisode.PlaycountSpecified Then
                            tmpDBElement.TVEpisode.LastPlayed = If(tmpDBElement.TVEpisode.LastPlayedSpecified, tmpDBElement.TVEpisode.LastPlayed, Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                            tmpDBElement.TVEpisode.Playcount = If(tmpDBElement.TVEpisode.PlaycountSpecified, tmpDBElement.TVEpisode.Playcount, 1)
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.TVEpisode.LastPlayedSpecified OrElse tmpDBElement.TVEpisode.PlaycountSpecified Then
                            tmpDBElement.TVEpisode.LastPlayed = String.Empty
                            tmpDBElement.TVEpisode.Playcount = 0
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                             .Message = tmpDBElement.TVEpisode.Title})

                        Master.DB.Save_TVEpisode(tmpDBElement, True, True, False, False, True)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVEpisode,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

            Case Enums.ContentType.TVSeason
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement_TVSeason As Database.DBElement = Master.DB.Load_TVSeason(tID, True, True)
                    'exclude "* All Seasons" entry from changing WatchedState
                    If Not tmpDBElement_TVSeason.TVSeason.Season = 999 Then
                        For Each tmpDBElement As Database.DBElement In tmpDBElement_TVSeason.Episodes.OrderBy(Function(f) f.TVEpisode.Episode)
                            If bwTaskManager.CancellationPending Then Exit For
                            Dim bHasChanged As Boolean = False

                            If tTaskItem.CommonBoolean Then
                                If Not tmpDBElement.TVEpisode.LastPlayedSpecified OrElse Not tmpDBElement.TVEpisode.PlaycountSpecified Then
                                    tmpDBElement.TVEpisode.LastPlayed = If(tmpDBElement.TVEpisode.LastPlayedSpecified, tmpDBElement.TVEpisode.LastPlayed, Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                    tmpDBElement.TVEpisode.Playcount = If(tmpDBElement.TVEpisode.PlaycountSpecified, tmpDBElement.TVEpisode.Playcount, 1)
                                    bHasChanged = True
                                End If
                            Else
                                If tmpDBElement.TVEpisode.LastPlayedSpecified OrElse tmpDBElement.TVEpisode.PlaycountSpecified Then
                                    tmpDBElement.TVEpisode.LastPlayed = String.Empty
                                    tmpDBElement.TVEpisode.Playcount = 0
                                    bHasChanged = True
                                End If
                            End If

                            If bHasChanged Then
                                bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                             .Message = tmpDBElement.TVEpisode.Title})

                                Master.DB.Save_TVEpisode(tmpDBElement, True, True, False, False, True)

                                bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = Enums.ContentType.TVEpisode,
                                                         .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                         .ID = tmpDBElement.ID})
                            End If
                        Next

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = Enums.ContentType.TVSeason,
                                                         .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                         .ID = tmpDBElement_TVSeason.ID})

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = Enums.ContentType.TVShow,
                                                         .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                         .ID = tmpDBElement_TVSeason.ShowID})
                    End If
                Next

            Case Enums.ContentType.TVShow
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement_TVShow As Database.DBElement = Master.DB.Load_TVShow(tID, True, True)
                    For Each tmpDBElement As Database.DBElement In tmpDBElement_TVShow.Episodes.OrderBy(Function(f) f.TVEpisode.Season).OrderBy(Function(f) f.TVEpisode.Episode)
                        If bwTaskManager.CancellationPending Then Exit For
                        Dim bHasChanged As Boolean = False

                        If tTaskItem.CommonBoolean Then
                            If Not tmpDBElement.TVEpisode.LastPlayedSpecified OrElse Not tmpDBElement.TVEpisode.PlaycountSpecified Then
                                tmpDBElement.TVEpisode.LastPlayed = If(tmpDBElement.TVEpisode.LastPlayedSpecified, tmpDBElement.TVEpisode.LastPlayed, Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                tmpDBElement.TVEpisode.Playcount = If(tmpDBElement.TVEpisode.PlaycountSpecified, tmpDBElement.TVEpisode.Playcount, 1)
                                bHasChanged = True
                            End If
                        Else
                            If tmpDBElement.TVEpisode.LastPlayedSpecified OrElse tmpDBElement.TVEpisode.PlaycountSpecified Then
                                tmpDBElement.TVEpisode.LastPlayed = String.Empty
                                tmpDBElement.TVEpisode.Playcount = 0
                                bHasChanged = True
                            End If
                        End If

                        If bHasChanged Then
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                         .Message = tmpDBElement.TVEpisode.Title})

                            Master.DB.Save_TVEpisode(tmpDBElement, True, True, False, False, True)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = Enums.ContentType.TVEpisode,
                                                         .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                         .ID = tmpDBElement.ID})
                        End If
                    Next

                    For Each tSeason In tmpDBElement_TVShow.Seasons.OrderBy(Function(f) f.TVSeason.Season)
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = Enums.ContentType.TVSeason,
                                                         .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                         .ID = tSeason.ID})
                    Next

                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVShow,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement_TVShow.ID})
                Next

        End Select
    End Sub

    Private Sub RunTaskManager()
        While bwTaskManager.IsBusy
            Threading.Thread.Sleep(50)
        End While
        bwTaskManager = New System.ComponentModel.BackgroundWorker
        bwTaskManager.WorkerReportsProgress = True
        bwTaskManager.WorkerSupportsCancellation = True
        bwTaskManager.RunWorkerAsync()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Structure ProgressValue

#Region "Fields"

        Dim ContentType As Enums.ContentType
        Dim EventType As Enums.TaskManagerEventType
        Dim ID As Long
        Dim Message As String

#End Region 'Fields

    End Structure

    Public Structure TaskItem

#Region "Fields"

        Dim CommonBoolean As Boolean
        Dim ContentType As Enums.ContentType
        Dim ListOfID As List(Of Long)
        Dim TaskType As Enums.TaskManagerType

#End Region

    End Structure

#End Region

End Class
