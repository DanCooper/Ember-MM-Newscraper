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
            'update number of tasks
        End If
    End Sub

    Private Sub bwTaskManager_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTaskManager.DoWork
        While TaskList.Count > 0
            If bwTaskManager.CancellationPending Then Return

            Dim currTask As TaskItem = TaskList.Dequeue()

            Select Case currTask.TaskType
                Case Enums.TaskManagerType.CopyBackdrops
                    CopyBackdrops(currTask)

                Case Enums.TaskManagerType.DoTitleCheck
                    Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        DoTitleCheck(currTask)
                        SQLTransaction.Commit()
                    End Using

                Case Enums.TaskManagerType.GetMissingEpisodes
                    Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        GetMissingEpisodes(currTask)
                        SQLTransaction.Commit()
                    End Using

                Case Enums.TaskManagerType.SetLanguage
                    Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        SetLanguage(currTask)
                        SQLTransaction.Commit()
                    End Using

                Case Enums.TaskManagerType.SetLockedState
                    Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        SetLockedState(currTask)
                        SQLTransaction.Commit()
                    End Using

                Case Enums.TaskManagerType.SetMarkedState
                    Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        SetMarkedState(currTask)
                        SQLTransaction.Commit()
                    End Using

                Case Enums.TaskManagerType.SetWatchedState
                    Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        SetWatchedState(currTask)
                        SQLTransaction.Commit()
                    End Using
            End Select
        End While
    End Sub

    Private Sub bwTaskManager_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwTaskManager.ProgressChanged
        Dim tProgressValue As ProgressValue = DirectCast(e.UserState, ProgressValue)
        RaiseEvent ProgressUpdate(tProgressValue)
    End Sub

    Private Sub bwTaskManager_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTaskManager.RunWorkerCompleted
        RaiseEvent ProgressUpdate(New ProgressValue With {.EventType = Enums.TaskManagerEventType.TaskManagerEnded})
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

    Private Sub DoTitleCheck(ByVal tTaskItem As TaskItem)
        Select Case tTaskItem.ContentType

            Case Enums.ContentType.Movie
                Using SQLCommand_Update As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLCommand_Update.CommandText = "UPDATE movie SET OutOfTolerance = (?) WHERE idMovie = (?);"
                    Dim par_OutOfTolerance As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_OutOfTolerance", DbType.Boolean, 0, "OutOfTolerance")
                    Dim par_idMovie As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_idMovie", DbType.Int64, 0, "idMovie")

                    Using SQLCommand_GetMovies As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_GetMovies.CommandText = String.Concat("SELECT * FROM movie;")
                        Using SQLReader_GetMovies As SQLite.SQLiteDataReader = SQLCommand_GetMovies.ExecuteReader()
                            While SQLReader_GetMovies.Read
                                Dim bLevFail_OldValue = CBool(SQLReader_GetMovies("OutOfTolerance"))

                                If Master.eSettings.MovieLevTolerance > 0 Then
                                    Dim bIsSingle As Boolean = False
                                    Dim bLevFail_NewValue As Boolean = False
                                    Dim bUseFolderName As Boolean = False

                                    bIsSingle = CBool(SQLReader_GetMovies("Type"))

                                    Using SQLCommand_Source As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                                        SQLCommand_Source.CommandText = String.Concat("SELECT * FROM moviesource WHERE idSource = ", Convert.ToInt64(SQLReader_GetMovies("idSource")), ";")
                                        Using SQLreader_GetSource As SQLite.SQLiteDataReader = SQLCommand_Source.ExecuteReader()
                                            If SQLreader_GetSource.HasRows Then
                                                SQLreader_GetSource.Read()
                                                bUseFolderName = CBool(SQLreader_GetSource("bFoldername"))
                                            Else
                                                bUseFolderName = False
                                            End If
                                        End Using
                                    End Using

                                    bLevFail_NewValue = StringUtils.ComputeLevenshtein(SQLReader_GetMovies("Title").ToString, StringUtils.FilterTitleFromPath_Movie(SQLReader_GetMovies("MoviePath").ToString, bIsSingle, bUseFolderName)) > Master.eSettings.MovieLevTolerance

                                    If Not bLevFail_OldValue = bLevFail_NewValue Then
                                        par_OutOfTolerance.Value = bLevFail_NewValue
                                        par_idMovie.Value = CLng(SQLReader_GetMovies("idMovie"))
                                        SQLCommand_Update.ExecuteNonQuery()
                                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                         .ContentType = Enums.ContentType.Movie,
                                                                         .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                                         .ID = CLng(SQLReader_GetMovies("idMovie"))})
                                    End If
                                ElseIf bLevFail_OldValue Then
                                    par_OutOfTolerance.Value = False
                                    par_idMovie.Value = CLng(SQLReader_GetMovies("idMovie"))
                                    SQLCommand_Update.ExecuteNonQuery()
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                     .ContentType = Enums.ContentType.Movie,
                                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                                     .ID = CLng(SQLReader_GetMovies("idMovie"))})
                                End If
                            End While
                        End Using
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

    Private Sub SetLanguage(ByVal tTaskItem As TaskItem)
        If String.IsNullOrEmpty(tTaskItem.CommonStringValue) Then Return

        Dim nLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Description = tTaskItem.CommonStringValue)
        If nLanguage IsNot Nothing AndAlso Not String.IsNullOrEmpty(nLanguage.Abbreviation) Then
            Dim strNewLanguage As String = nLanguage.Abbreviation

            Select Case tTaskItem.ContentType

                Case Enums.ContentType.Movie
                    For Each tID In tTaskItem.ListOfID
                        If bwTaskManager.CancellationPending Then Return
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(tID)

                        If Not tmpDBElement.Language = strNewLanguage Then
                            tmpDBElement.Language = strNewLanguage
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
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_MovieSet(tID)

                        If Not tmpDBElement.Language = strNewLanguage Then
                            tmpDBElement.Language = strNewLanguage
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                         .Message = tmpDBElement.MovieSet.Title})

                            Master.DB.Save_MovieSet(tmpDBElement, True, True, False, False)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = Enums.ContentType.MovieSet,
                                                         .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                         .ID = tmpDBElement.ID})
                        End If
                    Next

                Case Enums.ContentType.TVShow
                    For Each tID In tTaskItem.ListOfID
                        If bwTaskManager.CancellationPending Then Return
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(tID, False, False)

                        If Not tmpDBElement.Language = strNewLanguage Then
                            tmpDBElement.Language = strNewLanguage
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

        End If
    End Sub

    Private Sub SetLockedState(ByVal tTaskItem As TaskItem)
        Select Case tTaskItem.ContentType

            Case Enums.ContentType.Movie
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(tID)

                    If tTaskItem.CommonBooleanValue Then
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

                    If tTaskItem.CommonBooleanValue Then
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

                        Master.DB.Save_MovieSet(tmpDBElement, True, True, False, False)

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

                    If tTaskItem.CommonBooleanValue Then
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

                    If tTaskItem.CommonBooleanValue Then
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

                    If tTaskItem.CommonBooleanValue Then
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
                        SetLockedState(New TaskItem With {
                                       .CommonBooleanValue = tmpDBElement.IsLock,
                                       .ContentType = Enums.ContentType.TVSeason,
                                       .ListOfID = lstSeasonsIDs,
                                       .TaskType = Enums.TaskManagerType.SetLockedState})
                    End If

                    'Episode handling
                    Dim lstEpisodeIDs As New List(Of Long)
                    For Each nTVEpisode As Database.DBElement In tmpDBElement.Episodes
                        lstEpisodeIDs.Add(nTVEpisode.ID)
                    Next
                    If lstEpisodeIDs.Count > 0 Then
                        SetLockedState(New TaskItem With {
                                       .CommonBooleanValue = tmpDBElement.IsLock,
                                       .ContentType = Enums.ContentType.TVEpisode,
                                       .ListOfID = lstEpisodeIDs,
                                       .TaskType = Enums.TaskManagerType.SetLockedState})
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

                    Using SQLCommand_Update As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_Update.CommandText = "UPDATE movie SET Mark = (?) WHERE idMovie = (?);"
                        Dim par_Mark As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_Mark", DbType.Boolean, 0, "Mark")
                        Dim par_ID As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_ID", DbType.Int64, 0, "idMovie")

                        Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand_Get.CommandText = String.Format("SELECT * FROM movie WHERE idMovie = {0} AND Mark = {1};", tID, If(tTaskItem.CommonBooleanValue, 0, 1))
                            Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                                While SQLReader_Get.Read
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                                 .Message = SQLReader_Get("Title").ToString})
                                    par_ID.Value = tID
                                    par_Mark.Value = tTaskItem.CommonBooleanValue
                                    SQLCommand_Update.ExecuteNonQuery()
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .ContentType = Enums.ContentType.Movie,
                                                                 .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                                 .ID = tID})
                                End While
                            End Using
                        End Using
                    End Using
                Next

            Case Enums.ContentType.MovieSet
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return

                    Using SQLCommand_Update As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_Update.CommandText = "UPDATE sets SET Mark = (?) WHERE idSet = (?);"
                        Dim par_Mark As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_Mark", DbType.Boolean, 0, "Mark")
                        Dim par_ID As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_ID", DbType.Int64, 0, "idSet")

                        Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand_Get.CommandText = String.Format("SELECT * FROM sets WHERE idSet = {0} AND Mark = {1};", tID, If(tTaskItem.CommonBooleanValue, 0, 1))
                            Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                                While SQLReader_Get.Read
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                                 .Message = SQLReader_Get("SetName").ToString})
                                    par_ID.Value = tID
                                    par_Mark.Value = tTaskItem.CommonBooleanValue
                                    SQLCommand_Update.ExecuteNonQuery()
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .ContentType = Enums.ContentType.MovieSet,
                                                                 .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                                 .ID = tID})
                                End While
                            End Using
                        End Using
                    End Using
                Next

            Case Enums.ContentType.TVEpisode
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return

                    Using SQLCommand_Update As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_Update.CommandText = "UPDATE episode SET Mark = (?) WHERE idEpisode = (?);"
                        Dim par_Mark As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_Mark", DbType.Boolean, 0, "Mark")
                        Dim par_ID As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_ID", DbType.Int64, 0, "idEpisode")

                        Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand_Get.CommandText = String.Format("SELECT * FROM episode WHERE idEpisode = {0} AND Mark = {1};", tID, If(tTaskItem.CommonBooleanValue, 0, 1))
                            Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                                While SQLReader_Get.Read
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                                 .Message = SQLReader_Get("Title").ToString})
                                    par_ID.Value = tID
                                    par_Mark.Value = tTaskItem.CommonBooleanValue
                                    SQLCommand_Update.ExecuteNonQuery()
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .ContentType = Enums.ContentType.TVEpisode,
                                                                 .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                                 .ID = tID})
                                End While
                            End Using
                        End Using
                    End Using
                Next

            Case Enums.ContentType.TVSeason
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return

                    'first proceed all episodes in this season
                    Dim intSeason As Integer = -1
                    Dim lngShowID As Long = -1
                    Dim nTaskItem As New TaskItem With {
                        .CommonBooleanValue = tTaskItem.CommonBooleanValue,
                        .ContentType = Enums.ContentType.TVEpisode,
                        .ListOfID = New List(Of Long),
                        .TaskType = Enums.TaskManagerType.SetMarkedState}

                    Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_Get.CommandText = String.Format("SELECT idShow, Season FROM seasons WHERE idSeason = {0};", tID)
                        Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                            While SQLReader_Get.Read
                                intSeason = CInt(SQLReader_Get("Season"))
                                lngShowID = CLng(SQLReader_Get("idShow"))
                            End While
                        End Using
                    End Using

                    If Not intSeason = -1 AndAlso Not lngShowID = -1 Then
                        Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand_Get.CommandText = String.Format("SELECT idEpisode FROM episode WHERE idShow = {0} AND Season = {1};", lngShowID, intSeason)
                            Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                                While SQLReader_Get.Read
                                    nTaskItem.ListOfID.Add(CLng(SQLReader_Get("idEpisode")))
                                End While
                            End Using
                        End Using
                        SetMarkedState(nTaskItem)
                    End If

                    'now proceed the season
                    Using SQLCommand_Update As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_Update.CommandText = "UPDATE seasons SET Mark = (?) WHERE idSeason = (?);"
                        Dim par_Mark As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_Mark", DbType.Boolean, 0, "Mark")
                        Dim par_ID As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_ID", DbType.Int64, 0, "idSeason")

                        Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand_Get.CommandText = String.Format("SELECT * FROM seasons WHERE idSeason = {0} AND Mark = {1};", tID, If(tTaskItem.CommonBooleanValue, 0, 1))
                            Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                                While SQLReader_Get.Read
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                                 .Message = SQLReader_Get("SeasonText").ToString})
                                    par_ID.Value = tID
                                    par_Mark.Value = tTaskItem.CommonBooleanValue
                                    SQLCommand_Update.ExecuteNonQuery()
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .ContentType = Enums.ContentType.TVSeason,
                                                                 .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                                 .ID = tID})
                                End While
                            End Using
                        End Using
                    End Using
                Next

            Case Enums.ContentType.TVShow, Enums.ContentType.TV
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return

                    'first proceed all seasons in this tv show (episodes will be handled by seasons)
                    Dim nTaskItem As New TaskItem With {
                        .CommonBooleanValue = tTaskItem.CommonBooleanValue,
                        .ContentType = Enums.ContentType.TVSeason,
                        .ListOfID = New List(Of Long),
                        .TaskType = Enums.TaskManagerType.SetMarkedState}

                    Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_Get.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0};", tID)
                        Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                            While SQLReader_Get.Read
                                nTaskItem.ListOfID.Add(CLng(SQLReader_Get("idSeason")))
                            End While
                        End Using
                    End Using
                    SetMarkedState(nTaskItem)

                    'now proceed the tv show
                    Using SQLCommand_Update As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_Update.CommandText = "UPDATE tvshow SET Mark = (?) WHERE idShow = (?);"
                        Dim par_Mark As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_Mark", DbType.Boolean, 0, "Mark")
                        Dim par_ID As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_ID", DbType.Int64, 0, "idShow")

                        Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand_Get.CommandText = String.Format("SELECT * FROM tvshow WHERE idShow = {0} AND Mark = {1};", tID, If(tTaskItem.CommonBooleanValue, 0, 1))
                            Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                                While SQLReader_Get.Read
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                                 .Message = SQLReader_Get("Title").ToString})
                                    par_ID.Value = tID
                                    par_Mark.Value = tTaskItem.CommonBooleanValue
                                    SQLCommand_Update.ExecuteNonQuery()
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .ContentType = Enums.ContentType.TVShow,
                                                                 .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                                 .ID = tID})
                                End While
                            End Using
                        End Using
                    End Using
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

                    If tTaskItem.CommonBooleanValue Then
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

                    If tTaskItem.CommonBooleanValue Then
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

                            If tTaskItem.CommonBooleanValue Then
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

                        If tTaskItem.CommonBooleanValue Then
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
        RaiseEvent ProgressUpdate(New ProgressValue With {.EventType = Enums.TaskManagerEventType.TaskManagerStarted, .Message = "TaskManager is running"})
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

        Dim CommonBooleanValue As Boolean
        Dim CommonStringValue As String
        Dim ContentType As Enums.ContentType
        Dim ListOfID As List(Of Long)
        Dim TaskType As Enums.TaskManagerType

#End Region

    End Structure

#End Region

End Class
