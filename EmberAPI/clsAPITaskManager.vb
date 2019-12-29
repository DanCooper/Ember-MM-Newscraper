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

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _HasChanged As Boolean
    Private _TaskList As New Queue(Of TaskItem)

    Friend WithEvents bwTaskManager As New ComponentModel.BackgroundWorker

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
        _TaskList.Enqueue(tTaskItem)
        If Not bwTaskManager.IsBusy Then
            RunTaskManager()
        Else
            'update number of tasks
        End If
    End Sub

    Private Sub bwTaskManager_DoWork(ByVal sender As Object, ByVal e As ComponentModel.DoWorkEventArgs) Handles bwTaskManager.DoWork
        While _TaskList.Count > 0
            If bwTaskManager.CancellationPending Then Return

            Dim currTask As TaskItem = _TaskList.Dequeue()

            Select Case currTask.TypeOfTask
                Case TaskItem.TaskType.DataFields_ClearOrReplace
                    Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        DataFields_ClearOrReplace(currTask)
                        SQLTransaction.Commit()
                    End Using

                Case TaskItem.TaskType.CopyBackdrops
                    CopyBackdrops(currTask)

                Case TaskItem.TaskType.DoTitleCheck
                    Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        DoTitleCheck(currTask)
                        SQLTransaction.Commit()
                    End Using

                Case TaskItem.TaskType.GetMissingEpisodes
                    Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        GetMissingEpisodes(currTask)
                        SQLTransaction.Commit()
                    End Using

                Case TaskItem.TaskType.Scan
                    DoScan(currTask)

                Case TaskItem.TaskType.SetLanguage
                    Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        SetLanguage(currTask)
                        SQLTransaction.Commit()
                    End Using

                Case TaskItem.TaskType.SetLockedState
                    Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        SetLockedState(currTask)
                        SQLTransaction.Commit()
                    End Using

                Case TaskItem.TaskType.SetMarkedState
                    Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        SetMarkedState(currTask)
                        SQLTransaction.Commit()
                    End Using

                Case TaskItem.TaskType.SetMovieset
                    Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        SetMovieset(currTask)
                        SQLTransaction.Commit()
                    End Using

                Case TaskItem.TaskType.SetWatchedState
                    Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        SetWatchedState(currTask)
                        SQLTransaction.Commit()
                    End Using
            End Select
        End While
    End Sub

    Private Sub bwTaskManager_ProgressChanged(ByVal sender As Object, ByVal e As ComponentModel.ProgressChangedEventArgs) Handles bwTaskManager.ProgressChanged
        Dim tProgressValue As ProgressValue = DirectCast(e.UserState, ProgressValue)
        RaiseEvent ProgressUpdate(tProgressValue)
    End Sub

    Private Sub bwTaskManager_RunWorkerCompleted(ByVal sender As Object, ByVal e As ComponentModel.RunWorkerCompletedEventArgs) Handles bwTaskManager.RunWorkerCompleted
        RaiseEvent ProgressUpdate(New ProgressValue With {.EventType = EventType.TaskManagerEnded})
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

    Private Sub DataFields_ClearOrReplace(ByVal tTaskItem As TaskItem)
        Select Case tTaskItem.ContentType

            Case Enums.ContentType.Movie
                Dim nInfo As New MediaContainers.Movie
                If tTaskItem.GenericObject IsNot Nothing AndAlso
                    tTaskItem.GenericObject.GetType Is GetType(MediaContainers.Movie) Then
                    nInfo = DirectCast(tTaskItem.GenericObject, MediaContainers.Movie)
                Else
                    Return
                End If

                For Each tID In tTaskItem.ListOfID
                    _HasChanged = False
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(tID)

                    If Not tmpDBElement.IsLocked Then
                        With tTaskItem.ScrapeOptions
                            DataField_ClearList(.bMainActors, tmpDBElement.Movie.Actors)
                            DataField_CompareLists(.bMainCertifications, tmpDBElement.Movie.Certifications, nInfo.Certifications)
                            DataField_CompareLists(.bMainCountries, tmpDBElement.Movie.Countries, nInfo.Countries)
                            DataField_CompareLists(.bMainDirectors, tmpDBElement.Movie.Directors, nInfo.Directors)
                            DataField_CompareLists(.bMainGenres, tmpDBElement.Movie.Genres, nInfo.Genres)
                            DataField_CompareStrings(.bMainMPAA, tmpDBElement.Movie.MPAA, nInfo.MPAA)
                            DataField_ClearString(.bMainOriginalTitle, tmpDBElement.Movie.OriginalTitle)
                            DataField_ClearString(.bMainOutline, tmpDBElement.Movie.Outline)
                            DataField_ClearString(.bMainPlot, tmpDBElement.Movie.Plot)
                            DataField_ClearString(.bMainRatings, tmpDBElement.Movie.Rating)
                            DataField_ClearString(.bMainRatings, tmpDBElement.Movie.Votes)
                            DataField_CompareStrings(.bMainPremiered, tmpDBElement.Movie.Premiered, nInfo.Premiered)
                            DataField_ClearString(.bMainRuntime, tmpDBElement.Movie.Runtime)
                            DataField_CompareLists(.bMainStudios, tmpDBElement.Movie.Studios, nInfo.Studios)
                            DataField_CompareStrings(.bMainTagline, tmpDBElement.Movie.Tagline, nInfo.Tagline)
                            DataField_CompareLists(.bMainTags, tmpDBElement.Movie.Tags, nInfo.Tags)
                            DataField_CompareIntegers(.bMainTop250, tmpDBElement.Movie.Top250, nInfo.Top250)
                            DataField_ClearString(.bMainTrailer, tmpDBElement.Movie.Trailer)
                            DataField_CompareIntegers(.bMainUserRating, tmpDBElement.Movie.UserRating, nInfo.UserRating)
                            DataField_CompareStrings(.bMainVideoSource, tmpDBElement.VideoSource, nInfo.VideoSource)
                            tmpDBElement.Movie.VideoSource = tmpDBElement.VideoSource
                            DataField_CompareLists(.bMainCredits, tmpDBElement.Movie.Credits, nInfo.Credits)
                        End With

                        If _HasChanged Then
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = EventType.SimpleMessage,
                                                     .Message = tmpDBElement.Movie.Title})

                            Master.DB.Save_Movie(tmpDBElement, True, True, False, True, False)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                        End If
                    End If
                Next

            Case Enums.ContentType.Movieset
                For Each tID In tTaskItem.ListOfID
                    _HasChanged = False
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movieset(tID)

                    If Not tmpDBElement.IsLocked Then
                        With tTaskItem.ScrapeOptions
                            DataField_ClearString(.bMainPlot, tmpDBElement.Movieset.Plot)
                        End With

                        If _HasChanged Then
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = EventType.SimpleMessage,
                                                     .Message = tmpDBElement.Movieset.Title})

                            Master.DB.Save_Movieset(tmpDBElement, True, True, False, True)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                        End If
                    End If
                Next

            Case Enums.ContentType.TVEpisode
                Dim nInfo As New MediaContainers.EpisodeDetails
                If tTaskItem.GenericObject IsNot Nothing AndAlso
                    tTaskItem.GenericObject.GetType Is GetType(MediaContainers.EpisodeDetails) Then
                    nInfo = DirectCast(tTaskItem.GenericObject, MediaContainers.EpisodeDetails)
                Else
                    Return
                End If

                For Each tID In tTaskItem.ListOfID
                    _HasChanged = False
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVEpisode(tID, False)

                    If Not tmpDBElement.IsLocked Then
                        With tTaskItem.ScrapeOptions
                            DataField_ClearList(.bEpisodeActors, tmpDBElement.TVEpisode.Actors)
                            DataField_CompareStrings(.bEpisodeAired, tmpDBElement.TVEpisode.Aired, nInfo.Aired)
                            DataField_CompareLists(.bEpisodeCredits, tmpDBElement.TVEpisode.Credits, nInfo.Credits)
                            DataField_CompareLists(.bEpisodeDirectors, tmpDBElement.TVEpisode.Directors, nInfo.Directors)
                            DataField_ClearList(.bEpisodeGuestStars, tmpDBElement.TVEpisode.GuestStars)
                            DataField_ClearString(.bEpisodePlot, tmpDBElement.TVEpisode.Plot)
                            DataField_ClearString(.bEpisodeRating, tmpDBElement.TVEpisode.Rating)
                            DataField_ClearString(.bEpisodeRating, tmpDBElement.TVEpisode.Votes)
                            DataField_ClearString(.bEpisodeRuntime, tmpDBElement.TVEpisode.Runtime)
                            DataField_CompareIntegers(.bEpisodeUserRating, tmpDBElement.TVEpisode.UserRating, nInfo.UserRating)
                            DataField_CompareStrings(.bEpisodeVideoSource, tmpDBElement.VideoSource, nInfo.VideoSource)
                            tmpDBElement.TVEpisode.VideoSource = tmpDBElement.VideoSource
                        End With

                        If _HasChanged Then
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = EventType.SimpleMessage,
                                                     .Message = tmpDBElement.TVEpisode.Title})

                            Master.DB.Save_TVEpisode(tmpDBElement, True, True, False, False, True)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                        End If
                    End If
                Next

            Case Enums.ContentType.TVSeason
                Dim nInfo As New MediaContainers.SeasonDetails
                If tTaskItem.GenericObject IsNot Nothing AndAlso
                    tTaskItem.GenericObject.GetType Is GetType(MediaContainers.SeasonDetails) Then
                    nInfo = DirectCast(tTaskItem.GenericObject, MediaContainers.SeasonDetails)
                Else
                    Return
                End If

                For Each tID In tTaskItem.ListOfID
                    _HasChanged = False
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVSeason(tID, True, False)

                    If Not tmpDBElement.IsLocked Then

                        With tTaskItem.ScrapeOptions
                            DataField_CompareStrings(.bSeasonAired, tmpDBElement.TVSeason.Aired, nInfo.Aired)
                            DataField_ClearString(.bSeasonPlot, tmpDBElement.TVSeason.Plot)
                            DataField_ClearString(.bSeasonTitle, tmpDBElement.TVSeason.Title)
                        End With

                        If _HasChanged Then
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = EventType.SimpleMessage,
                                                     .Message = String.Format("{0}: {1} {2}", tmpDBElement.TVShow.Title, Master.eLang.GetString(650, "Season"), tmpDBElement.TVSeason.Season.ToString)})

                            Master.DB.Save_TVSeason(tmpDBElement, True, False, True)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                        End If
                    End If
                Next

            Case Enums.ContentType.TVShow
                Dim nInfo As New MediaContainers.TVShow
                If tTaskItem.GenericObject IsNot Nothing AndAlso
                    tTaskItem.GenericObject.GetType Is GetType(MediaContainers.TVShow) Then
                    nInfo = DirectCast(tTaskItem.GenericObject, MediaContainers.TVShow)
                Else
                    Return
                End If

                For Each tID In tTaskItem.ListOfID
                    _HasChanged = False
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(tID, False, False, False)

                    If Not tmpDBElement.IsLocked Then
                        With tTaskItem.ScrapeOptions
                            DataField_ClearList(.bMainActors, tmpDBElement.TVShow.Actors)
                            DataField_CompareLists(.bMainCertifications, tmpDBElement.TVShow.Certifications, nInfo.Certifications)
                            DataField_CompareLists(.bMainCountries, tmpDBElement.TVShow.Countries, nInfo.Countries)
                            DataField_CompareLists(.bMainCreators, tmpDBElement.TVShow.Creators, nInfo.Creators)
                            DataField_CompareLists(.bMainGenres, tmpDBElement.TVShow.Genres, nInfo.Genres)
                            DataField_CompareStrings(.bMainMPAA, tmpDBElement.TVShow.MPAA, nInfo.MPAA)
                            DataField_ClearString(.bMainOriginalTitle, tmpDBElement.TVShow.OriginalTitle)
                            DataField_ClearString(.bMainPlot, tmpDBElement.TVShow.Plot)
                            DataField_CompareStrings(.bMainPremiered, tmpDBElement.TVShow.Premiered, nInfo.Premiered)
                            DataField_ClearString(.bMainRatings, tmpDBElement.TVShow.Rating)
                            DataField_ClearString(.bMainRatings, tmpDBElement.TVShow.Votes)
                            DataField_ClearString(.bMainRuntime, tmpDBElement.TVShow.Runtime)
                            DataField_CompareStrings(.bMainStatus, tmpDBElement.TVShow.Status, nInfo.Status)
                            DataField_CompareLists(.bMainStudios, tmpDBElement.TVShow.Studios, nInfo.Studios)
                            DataField_CompareLists(.bMainTags, tmpDBElement.TVShow.Tags, nInfo.Tags)
                            DataField_CompareIntegers(.bMainUserRating, tmpDBElement.TVShow.UserRating, nInfo.UserRating)
                        End With

                        If _HasChanged Then
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = EventType.SimpleMessage,
                                                     .Message = tmpDBElement.TVShow.Title})

                            Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                        End If
                    End If
                Next
        End Select
    End Sub

    Private Sub DataField_ClearList(bEnabled As Boolean, ByRef currList As List(Of String))
        If bEnabled AndAlso currList.Count > 0 Then
            currList.Clear()
            _HasChanged = True
        End If
    End Sub

    Private Sub DataField_ClearList(bEnabled As Boolean, ByRef currList As List(Of MediaContainers.Person))
        If bEnabled AndAlso currList.Count > 0 Then
            currList.Clear()
            _HasChanged = True
        End If
    End Sub

    Private Sub DataField_ClearString(bEnabled As Boolean, ByRef currString As String)
        If bEnabled AndAlso Not String.IsNullOrEmpty(currString) Then
            currString = String.Empty
            _HasChanged = True
        End If
    End Sub

    Private Sub DataField_CompareIntegers(bEnabled As Boolean, ByRef currInteger As Integer, newInteger As Integer)
        If bEnabled AndAlso Not currInteger = newInteger Then
            currInteger = newInteger
            _HasChanged = True
        End If
    End Sub

    Private Sub DataField_CompareLists(bEnabled As Boolean, ByRef currList As List(Of String), newList As List(Of String))
        If bEnabled AndAlso (Not currList.Count = newList.Count OrElse Not currList.Count = 0 OrElse Not newList.Count = 0) Then
            currList = newList
            _HasChanged = True
        End If
    End Sub

    Private Sub DataField_CompareStrings(bEnabled As Boolean, ByRef currString As String, newString As String)
        If bEnabled AndAlso Not currString = newString Then
            currString = newString
            _HasChanged = True
        End If
    End Sub

    Private Sub CopyBackdrops(ByVal currTask As TaskItem)
        Select Case currTask.ContentType

            Case Enums.ContentType.Movie
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLcommand.CommandText = "SELECT listTitle, fanartPath FROM movielist WHERE fanartPath IS NOT NULL AND NOT fanartPath='' ORDER BY listTitle;"
                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        While SQLreader.Read
                            If bwTaskManager.CancellationPending Then Return
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .EventType = EventType.SimpleMessage,
                                                         .Message = SQLreader("listTitle").ToString})

                            FileUtils.Common.CopyFanartToBackdropsPath(SQLreader("fanartPath").ToString, Enums.ContentType.Movie)
                        End While
                    End Using
                End Using

        End Select
    End Sub

    Private Sub DoScan(ByVal tTaskItem As TaskItem)
        Dim fScanner As New Scanner
        For Each id In tTaskItem.ListOfID
            fScanner.Start(tTaskItem.ScanOrCleanOptions, id, tTaskItem.CommonStringValue)
        Next
        While fScanner.IsBusy
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub DoTitleCheck(ByVal tTaskItem As TaskItem)
        Select Case tTaskItem.ContentType

            Case Enums.ContentType.Movie
                Using SQLCommand_Update As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLCommand_Update.CommandText = "UPDATE movie SET outOfTolerance=(?) WHERE idMovie=(?);"
                    Dim par_outOfTolerance As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_outOfTolerance", DbType.Boolean, 0, "outOfTolerance")
                    Dim par_idMovie As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_idMovie", DbType.Int64, 0, "idMovie")

                    Using SQLCommand_GetMovies As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_GetMovies.CommandText = String.Concat("SELECT * FROM movie;")
                        Using SQLReader_GetMovies As SQLite.SQLiteDataReader = SQLCommand_GetMovies.ExecuteReader()
                            While SQLReader_GetMovies.Read
                                Dim bLevFail_OldValue = CBool(SQLReader_GetMovies("outOfTolerance"))

                                If Master.eSettings.MovieLevTolerance > 0 Then
                                    Dim bIsSingle As Boolean = False
                                    Dim bLevFail_NewValue As Boolean = False
                                    Dim bUseFolderName As Boolean = False
                                    Dim strPath As String = String.Empty

                                    bIsSingle = CBool(SQLReader_GetMovies("isSingle"))

                                    Using SQLCommand_Source As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                                        SQLCommand_Source.CommandText = String.Format("SELECT * FROM moviesource WHERE idSource={0};", Convert.ToInt64(SQLReader_GetMovies("idSource")))
                                        Using SQLreader_GetSource As SQLite.SQLiteDataReader = SQLCommand_Source.ExecuteReader()
                                            If SQLreader_GetSource.HasRows Then
                                                SQLreader_GetSource.Read()
                                                bUseFolderName = CBool(SQLreader_GetSource("useFolderName"))
                                            Else
                                                bUseFolderName = False
                                            End If
                                        End Using
                                    End Using

                                    Using sqlCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                                        sqlCommand.CommandText = String.Format("SELECT path FROM file WHERE idFile={0};", Convert.ToInt64(SQLReader_GetMovies("idFile")))
                                        Using sqlReader As SQLite.SQLiteDataReader = sqlCommand.ExecuteReader()
                                            If sqlReader.HasRows Then
                                                sqlReader.Read()
                                                strPath = sqlReader("path").ToString
                                            End If
                                        End Using
                                    End Using

                                    If Not String.IsNullOrEmpty(strPath) Then
                                        bLevFail_NewValue = StringUtils.ComputeLevenshtein(SQLReader_GetMovies("title").ToString, StringUtils.FilterTitleFromPath_Movie(New FileItem(strPath), bIsSingle, bUseFolderName)) > Master.eSettings.MovieLevTolerance
                                    End If

                                    If Not bLevFail_OldValue = bLevFail_NewValue Then
                                        par_outOfTolerance.Value = bLevFail_NewValue
                                        par_idMovie.Value = CLng(SQLReader_GetMovies("idMovie"))
                                        SQLCommand_Update.ExecuteNonQuery()
                                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                         .ContentType = tTaskItem.ContentType,
                                                                         .EventType = EventType.RefreshRow,
                                                                         .ID = CLng(SQLReader_GetMovies("idMovie"))})
                                    End If
                                ElseIf bLevFail_OldValue Then
                                    par_outOfTolerance.Value = False
                                    par_idMovie.Value = CLng(SQLReader_GetMovies("idMovie"))
                                    SQLCommand_Update.ExecuteNonQuery()
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                     .ContentType = tTaskItem.ContentType,
                                                                     .EventType = EventType.RefreshRow,
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
                                                 .EventType = EventType.SimpleMessage,
                                                 .Message = tmpDBElement.TVShow.Title})

                    Dim ScrapeModifiers As New Structures.ScrapeModifiers
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainNFO, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withEpisodes, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withSeasons, True)

                    If Not AddonsManager.Instance.ScrapeData_TVShow(tmpDBElement, ScrapeModifiers, Enums.ScrapeType.SingleScrape, Master.eSettings.DefaultOptions(Enums.ContentType.TV), True) Then
                        For Each nMissingSeason In tmpDBElement.Seasons.Where(Function(f) Not f.IDSpecified)
                            Master.DB.Save_TVSeason(nMissingSeason, True, False, False)
                            bNewSeasons = True
                        Next
                        For Each nMissingEpisode In tmpDBElement.Episodes.Where(Function(f) Not f.FileItemSpecified)
                            Master.DB.Save_TVEpisode(nMissingEpisode, True, False, False, False, False)
                        Next
                    End If

                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                 .ContentType = tTaskItem.ContentType,
                                                 .EventType = EventType.RefreshRow,
                                                 .ID = tmpDBElement.ID})

                Next

        End Select
    End Sub

    Private Sub SetLanguage(ByVal tTaskItem As TaskItem)
        If String.IsNullOrEmpty(tTaskItem.CommonStringValue) Then Return

        Dim nLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Description = tTaskItem.CommonStringValue)
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
                                                         .EventType = EventType.SimpleMessage,
                                                         .Message = tmpDBElement.Movie.Title})

                            Master.DB.Save_Movie(tmpDBElement, True, True, False, False, False)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = tTaskItem.ContentType,
                                                         .EventType = EventType.RefreshRow,
                                                         .ID = tmpDBElement.ID})
                        End If
                    Next

                Case Enums.ContentType.Movieset
                    For Each tID In tTaskItem.ListOfID
                        If bwTaskManager.CancellationPending Then Return
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movieset(tID)

                        If Not tmpDBElement.Language = strNewLanguage Then
                            tmpDBElement.Language = strNewLanguage
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .EventType = EventType.SimpleMessage,
                                                         .Message = tmpDBElement.Movieset.Title})

                            Master.DB.Save_Movieset(tmpDBElement, True, True, False, False)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = tTaskItem.ContentType,
                                                         .EventType = EventType.RefreshRow,
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
                                                         .EventType = EventType.SimpleMessage,
                                                         .Message = tmpDBElement.TVShow.Title})

                            Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = tTaskItem.ContentType,
                                                         .EventType = EventType.RefreshRow,
                                                         .ID = tmpDBElement.ID})
                        End If
                    Next

            End Select

        End If
    End Sub

    Private Sub SetLockedState(ByVal tTaskItem As TaskItem, Optional ByVal bRegressiveSync As Boolean = False)
        Select Case tTaskItem.ContentType

            Case Enums.ContentType.Movie
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(tID)

                    If tTaskItem.CommonBooleanValue Then
                        If Not tmpDBElement.IsLocked Then
                            tmpDBElement.IsLocked = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsLocked Then
                            tmpDBElement.IsLocked = False
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = EventType.SimpleMessage,
                                                     .Message = tmpDBElement.Movie.Title})

                        Master.DB.Save_Movie(tmpDBElement, True, True, False, False, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

            Case Enums.ContentType.Movieset
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movieset(tID)

                    If tTaskItem.CommonBooleanValue Then
                        If Not tmpDBElement.IsLocked Then
                            tmpDBElement.IsLocked = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsLocked Then
                            tmpDBElement.IsLocked = False
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = EventType.SimpleMessage,
                                                     .Message = tmpDBElement.Movieset.Title})

                        Master.DB.Save_Movieset(tmpDBElement, True, True, False, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

            Case Enums.ContentType.TVEpisode
                Dim lstTVSeasonIDs As New List(Of Long)
                Dim lstTVShowIDs As New List(Of Long)
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVEpisode(tID, True)

                    If tTaskItem.CommonBooleanValue Then
                        If Not tmpDBElement.IsLocked Then
                            tmpDBElement.IsLocked = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsLocked Then
                            tmpDBElement.IsLocked = False
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        lstTVSeasonIDs.Add(Master.DB.GetTVSeasonIDFromEpisode(tmpDBElement))
                        lstTVShowIDs.Add(tmpDBElement.ShowID)
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = EventType.SimpleMessage,
                                                     .Message = tmpDBElement.TVEpisode.Title})

                        Master.DB.Save_TVEpisode(tmpDBElement, True, True, False, False, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

                If Not bRegressiveSync Then
                    'refresh other rows if the seasons/tv show entries not will be refreshed by a higher task
                    lstTVSeasonIDs = lstTVSeasonIDs.Distinct.ToList
                    lstTVShowIDs = lstTVShowIDs.Distinct.ToList
                    For Each nTVSeasonID In lstTVSeasonIDs
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVSeason,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = nTVSeasonID})
                    Next
                    For Each nTVShowID In lstTVShowIDs
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVShow,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = nTVShowID})
                    Next
                End If

            Case Enums.ContentType.TVSeason
                Dim lstTVShowIDs As New List(Of Long)
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVSeason(tID, True, False)

                    If tTaskItem.CommonBooleanValue Then
                        If Not tmpDBElement.IsLocked Then
                            tmpDBElement.IsLocked = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsLocked Then
                            tmpDBElement.IsLocked = False
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        lstTVShowIDs.Add(tmpDBElement.ShowID)
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = EventType.SimpleMessage,
                                                     .Message = String.Format("{0}: {1} {2}", tmpDBElement.TVShow.Title, Master.eLang.GetString(650, "Season"), tmpDBElement.TVSeason.Season.ToString)})

                        Master.DB.Save_TVSeason(tmpDBElement, True, True, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

                If Not bRegressiveSync Then
                    'refresh other rows if the tv show entries not will be refreshed by a higher task
                    lstTVShowIDs = lstTVShowIDs.Distinct.ToList
                    For Each nTVShowID In lstTVShowIDs
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVShow,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = nTVShowID})
                    Next
                End If

            Case Enums.ContentType.TVShow
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim bHasChanged As Boolean = False
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(tID, True, True)

                    If tTaskItem.CommonBooleanValue Then
                        If Not tmpDBElement.IsLocked Then
                            tmpDBElement.IsLocked = True
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.IsLocked Then
                            tmpDBElement.IsLocked = False
                            bHasChanged = True
                        End If
                    End If

                    'Season handling
                    Dim lstSeasonsIDs As New List(Of Long)
                    For Each nTVSeason As Database.DBElement In tmpDBElement.Seasons
                        lstSeasonsIDs.Add(nTVSeason.ID)
                        'overwrite the season Locked state to save it in tvshow.nfo
                        nTVSeason.IsLocked = tmpDBElement.IsLocked
                    Next
                    If lstSeasonsIDs.Count > 0 Then
                        SetLockedState(New TaskItem(TaskItem.TaskType.SetLockedState) With {
                                       .CommonBooleanValue = tmpDBElement.IsLocked,
                                       .ContentType = Enums.ContentType.TVSeason,
                                       .ListOfID = lstSeasonsIDs
                                       }, True)
                    End If

                    'Episode handling
                    Dim lstEpisodeIDs As New List(Of Long)
                    For Each nTVEpisode As Database.DBElement In tmpDBElement.Episodes
                        lstEpisodeIDs.Add(nTVEpisode.ID)
                    Next
                    If lstEpisodeIDs.Count > 0 Then
                        SetLockedState(New TaskItem(TaskItem.TaskType.SetLockedState) With {
                                       .CommonBooleanValue = tmpDBElement.IsLocked,
                                       .ContentType = Enums.ContentType.TVEpisode,
                                       .ListOfID = lstEpisodeIDs
                                       }, True)
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = EventType.SimpleMessage,
                                                     .Message = tmpDBElement.TVShow.Title})

                        Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next
        End Select
    End Sub

    Private Sub SetMarkedState(ByVal tTaskItem As TaskItem, Optional ByVal bRegressiveSync As Boolean = False)
        Select Case tTaskItem.ContentType

            Case Enums.ContentType.Movie
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return

                    Using SQLCommand_Update As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_Update.CommandText = "UPDATE movie SET marked=(?) WHERE idMovie=(?);"
                        Dim par_marked As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_marked", DbType.Boolean, 0, "marked")
                        Dim par_id As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_id", DbType.Int64, 0, "idMovie")

                        Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand_Get.CommandText = String.Format("SELECT * FROM movie WHERE idMovie={0} AND marked={1};", tID, If(tTaskItem.CommonBooleanValue, 0, 1))
                            Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                                While SQLReader_Get.Read
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .EventType = EventType.SimpleMessage,
                                                                 .Message = SQLReader_Get("title").ToString})
                                    par_id.Value = tID
                                    par_marked.Value = tTaskItem.CommonBooleanValue
                                    SQLCommand_Update.ExecuteNonQuery()
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .ContentType = tTaskItem.ContentType,
                                                                 .EventType = EventType.RefreshRow,
                                                                 .ID = tID})
                                End While
                            End Using
                        End Using
                    End Using
                Next

            Case Enums.ContentType.Movieset
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return

                    Using SQLCommand_Update As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_Update.CommandText = "UPDATE movieset SET marked=(?) WHERE idSet=(?);"
                        Dim par_marked As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_marked", DbType.Boolean, 0, "marked")
                        Dim par_id As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_id", DbType.Int64, 0, "idSet")

                        Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand_Get.CommandText = String.Format("SELECT * FROM movieset WHERE idSet={0} AND marked={1};", tID, If(tTaskItem.CommonBooleanValue, 0, 1))
                            Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                                While SQLReader_Get.Read
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .EventType = EventType.SimpleMessage,
                                                                 .Message = SQLReader_Get("title").ToString})
                                    par_id.Value = tID
                                    par_marked.Value = tTaskItem.CommonBooleanValue
                                    SQLCommand_Update.ExecuteNonQuery()
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .ContentType = tTaskItem.ContentType,
                                                                 .EventType = EventType.RefreshRow,
                                                                 .ID = tID})
                                End While
                            End Using
                        End Using
                    End Using
                Next

            Case Enums.ContentType.TVEpisode
                Dim lstTVSeasonIDs As New List(Of Long)
                Dim lstTVShowIDs As New List(Of Long)
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return

                    Using SQLCommand_Update As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_Update.CommandText = "UPDATE episode SET marked=(?) WHERE idEpisode=(?);"
                        Dim par_marked As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_marked", DbType.Boolean, 0, "marked")
                        Dim par_id As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_id", DbType.Int64, 0, "idEpisode")

                        Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand_Get.CommandText = String.Format("SELECT * FROM episode WHERE idEpisode={0} AND marked={1};", tID, If(tTaskItem.CommonBooleanValue, 0, 1))
                            Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                                While SQLReader_Get.Read
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .EventType = EventType.SimpleMessage,
                                                                 .Message = SQLReader_Get("title").ToString})
                                    par_id.Value = tID
                                    par_marked.Value = tTaskItem.CommonBooleanValue
                                    SQLCommand_Update.ExecuteNonQuery()
                                    lstTVSeasonIDs.Add(Master.DB.GetTVSeasonIDFromShowIDAndSeasonNumber(CLng(SQLReader_Get("idShow")), CInt(SQLReader_Get("season"))))
                                    lstTVShowIDs.Add(CLng(SQLReader_Get("idShow")))
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .ContentType = tTaskItem.ContentType,
                                                                 .EventType = EventType.RefreshRow,
                                                                 .ID = tID})
                                End While
                            End Using
                        End Using
                    End Using
                Next

                If Not bRegressiveSync Then
                    'refresh other rows if the seasons/tv show entries not will be refreshed by a higher task
                    lstTVSeasonIDs = lstTVSeasonIDs.Distinct.ToList
                    lstTVShowIDs = lstTVShowIDs.Distinct.ToList
                    For Each nTVSeasonID In lstTVSeasonIDs
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVSeason,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = nTVSeasonID})
                    Next
                    For Each nTVShowID In lstTVShowIDs
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVShow,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = nTVShowID})
                    Next
                End If

            Case Enums.ContentType.TVSeason
                Dim lstTVShowIDs As New List(Of Long)
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return

                    'first proceed all episodes in this season
                    Dim intSeason As Integer = -1
                    Dim lngShowID As Long = -1
                    Dim nTaskItem As New TaskItem(TaskItem.TaskType.SetMarkedState) With {
                        .CommonBooleanValue = tTaskItem.CommonBooleanValue,
                        .ContentType = Enums.ContentType.TVEpisode,
                        .ListOfID = New List(Of Long)
                    }

                    Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_Get.CommandText = String.Format("SELECT idShow, season FROM season WHERE idSeason={0};", tID)
                        Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                            While SQLReader_Get.Read
                                intSeason = CInt(SQLReader_Get("season"))
                                lngShowID = CLng(SQLReader_Get("idShow"))
                            End While
                        End Using
                    End Using

                    If Not intSeason = -1 AndAlso Not lngShowID = -1 Then
                        Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand_Get.CommandText = String.Format("SELECT idEpisode FROM episode WHERE idShow={0} AND season={1};", lngShowID, intSeason)
                            Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                                While SQLReader_Get.Read
                                    nTaskItem.ListOfID.Add(CLng(SQLReader_Get("idEpisode")))
                                End While
                            End Using
                        End Using
                        SetMarkedState(nTaskItem, True)
                    End If

                    'now proceed the season
                    Using SQLCommand_Update As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_Update.CommandText = "UPDATE season SET marked=(?) WHERE idSeason=(?);"
                        Dim par_marked As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_marked", DbType.Boolean, 0, "marked")
                        Dim par_id As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_id", DbType.Int64, 0, "idSeason")

                        Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand_Get.CommandText = String.Format("SELECT * FROM season WHERE idSeason={0} AND marked={1};", tID, If(tTaskItem.CommonBooleanValue, 0, 1))
                            Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                                While SQLReader_Get.Read
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .EventType = EventType.SimpleMessage,
                                                                 .Message = SQLReader_Get("title").ToString})
                                    par_id.Value = tID
                                    par_marked.Value = tTaskItem.CommonBooleanValue
                                    SQLCommand_Update.ExecuteNonQuery()
                                    lstTVShowIDs.Add(CLng(SQLReader_Get("idShow")))
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .ContentType = tTaskItem.ContentType,
                                                                 .EventType = EventType.RefreshRow,
                                                                 .ID = tID})
                                End While
                            End Using
                        End Using
                    End Using
                Next

                If Not bRegressiveSync Then
                    'refresh other rows if the tv show entries not will be refreshed by a higher task
                    lstTVShowIDs = lstTVShowIDs.Distinct.ToList
                    For Each nTVShowID In lstTVShowIDs
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVShow,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = nTVShowID})
                    Next
                End If

            Case Enums.ContentType.TVShow, Enums.ContentType.TV
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return

                    'first proceed all seasons in this tv show (episodes will be handled by seasons)
                    Dim nTaskItem As New TaskItem(TaskItem.TaskType.SetMarkedState) With {
                        .CommonBooleanValue = tTaskItem.CommonBooleanValue,
                        .ContentType = Enums.ContentType.TVSeason,
                        .ListOfID = New List(Of Long)
                    }

                    Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_Get.CommandText = String.Format("SELECT idSeason FROM season WHERE idShow={0};", tID)
                        Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                            While SQLReader_Get.Read
                                nTaskItem.ListOfID.Add(CLng(SQLReader_Get("idSeason")))
                            End While
                        End Using
                    End Using
                    SetMarkedState(nTaskItem, True)

                    'now proceed the tv show
                    Using SQLCommand_Update As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand_Update.CommandText = "UPDATE tvshow SET marked=(?) WHERE idShow=(?);"
                        Dim par_marked As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_marked", DbType.Boolean, 0, "marked")
                        Dim par_id As SQLite.SQLiteParameter = SQLCommand_Update.Parameters.Add("par_id", DbType.Int64, 0, "idShow")

                        Using SQLCommand_Get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand_Get.CommandText = String.Format("SELECT * FROM tvshow WHERE idShow={0} AND marked={1};", tID, If(tTaskItem.CommonBooleanValue, 0, 1))
                            Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                                While SQLReader_Get.Read
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .EventType = EventType.SimpleMessage,
                                                                 .Message = SQLReader_Get("title").ToString})
                                    par_id.Value = tID
                                    par_marked.Value = tTaskItem.CommonBooleanValue
                                    SQLCommand_Update.ExecuteNonQuery()
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .ContentType = tTaskItem.ContentType,
                                                                 .EventType = EventType.RefreshRow,
                                                                 .ID = tID})
                                End While
                            End Using
                        End Using
                    End Using
                Next
        End Select
    End Sub

    Private Sub SetMovieset(ByVal tTaskItem As TaskItem)
        Dim nMovieset = DirectCast(tTaskItem.GenericObject, MediaContainers.SetDetails)
        Select Case tTaskItem.ContentType

            Case Enums.ContentType.Movie
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(tID)
                    If nMovieset Is Nothing AndAlso tmpDBElement.Movie.SetsSpecified OrElse
                        nMovieset IsNot Nothing AndAlso Not tmpDBElement.Movie.SetsSpecified OrElse
                        nMovieset IsNot Nothing AndAlso tmpDBElement.Movie.Sets.Count > 1 OrElse
                        nMovieset IsNot Nothing AndAlso tmpDBElement.Movie.Sets.Where(Function(f) f.ID = nMovieset.ID).Count = 0 Then
                        tmpDBElement.Movie.Sets.Clear()
                        If nMovieset IsNot Nothing Then tmpDBElement.Movie.Sets.Add(nMovieset)
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .EventType = EventType.SimpleMessage,
                                                         .Message = tmpDBElement.Movie.Title})

                        Master.DB.Save_Movie(tmpDBElement, True, True, False, True, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = tTaskItem.ContentType,
                                                         .EventType = EventType.RefreshRow,
                                                         .ID = tmpDBElement.ID})
                    End If
                Next
        End Select
    End Sub

    Private Sub SetWatchedState(ByVal tTaskItem As TaskItem, Optional ByVal bRegressiveSync As Boolean = False)
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
                                                     .EventType = EventType.SimpleMessage,
                                                     .Message = tmpDBElement.Movie.Title})

                        Master.DB.Save_Movie(tmpDBElement, True, True, False, True, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

            Case Enums.ContentType.TVEpisode
                Dim lstTVSeasonIDs As New List(Of Long)
                Dim lstTVShowIDs As New List(Of Long)
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
                        lstTVSeasonIDs.Add(Master.DB.GetTVSeasonIDFromEpisode(tmpDBElement))
                        lstTVShowIDs.Add(tmpDBElement.ShowID)
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = EventType.SimpleMessage,
                                                     .Message = tmpDBElement.TVEpisode.Title})

                        Master.DB.Save_TVEpisode(tmpDBElement, True, True, False, False, True)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

                If Not bRegressiveSync Then
                    'refresh other rows if the seasons/tv show entries not will be refreshed by a higher task
                    lstTVSeasonIDs = lstTVSeasonIDs.Distinct.ToList
                    lstTVShowIDs = lstTVShowIDs.Distinct.ToList
                    For Each nTVSeasonID In lstTVSeasonIDs
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVSeason,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = nTVSeasonID})
                    Next
                    For Each nTVShowID In lstTVShowIDs
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVShow,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = nTVShowID})
                    Next
                End If

            Case Enums.ContentType.TVSeason
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement_TVSeason As Database.DBElement = Master.DB.Load_TVSeason(tID, True, True)
                    'exclude "* All Seasons" entry from changing WatchedState
                    If Not tmpDBElement_TVSeason.TVSeason.IsAllSeasons Then
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
                                                             .EventType = EventType.SimpleMessage,
                                                             .Message = tmpDBElement.TVEpisode.Title})

                                Master.DB.Save_TVEpisode(tmpDBElement, True, True, False, False, True)

                                bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .ContentType = Enums.ContentType.TVEpisode,
                                                             .EventType = EventType.RefreshRow,
                                                             .ID = tmpDBElement.ID})
                            End If
                        Next

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = tmpDBElement_TVSeason.ID})

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVShow,
                                                     .EventType = EventType.RefreshRow,
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
                                                         .EventType = EventType.SimpleMessage,
                                                         .Message = tmpDBElement.TVEpisode.Title})

                            Master.DB.Save_TVEpisode(tmpDBElement, True, True, False, False, True)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = Enums.ContentType.TVEpisode,
                                                         .EventType = EventType.RefreshRow,
                                                         .ID = tmpDBElement.ID})
                        End If
                    Next

                    For Each tSeason In tmpDBElement_TVShow.Seasons.OrderBy(Function(f) f.TVSeason.Season)
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVSeason,
                                                     .EventType = EventType.RefreshRow,
                                                     .ID = tSeason.ID})
                    Next

                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                 .ContentType = tTaskItem.ContentType,
                                                 .EventType = EventType.RefreshRow,
                                                 .ID = tmpDBElement_TVShow.ID})
                Next
        End Select
    End Sub

    Private Sub RunTaskManager()
        While bwTaskManager.IsBusy
            Threading.Thread.Sleep(50)
        End While
        RaiseEvent ProgressUpdate(New ProgressValue With {.EventType = EventType.TaskManagerStarted, .Message = "TaskManager is running"})
        bwTaskManager = New ComponentModel.BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }
        bwTaskManager.RunWorkerAsync()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Structure ProgressValue

#Region "Fields"

        Dim ContentType As Enums.ContentType
        Dim EventType As EventType
        Dim ID As Long
        Dim Message As String

#End Region 'Fields

    End Structure

    Public Class TaskItem

#Region "Fields"

        Public ReadOnly TypeOfTask As TaskType = TaskType.None

#End Region 'Fields

#Region "Properties"

        Public Property CommonBooleanValue As Boolean

        Public Property CommonStringValue As String

        Public Property ContentType As Enums.ContentType = Enums.ContentType.None

        Public Property GenericObject As Object = Nothing

        Public Property ListOfID As List(Of Long) = New List(Of Long)

        Public Property ScanOrCleanOptions As Scanner.ScanOrCleanOptions = New Scanner.ScanOrCleanOptions

        Public Property ScrapeOptions As Structures.ScrapeOptions = New Structures.ScrapeOptions

#End Region 'Properties

#Region "Constructors"

        Public Sub New(ByVal type As TaskType)
            TypeOfTask = type
        End Sub

#End Region 'Constructors

#Region "Nested Types"

        Public Enum TaskType As Integer
            ''' <summary>
            ''' only to set nothing after init
            ''' </summary>
            None
            DataFields_ClearOrReplace
            Clean
            CopyBackdrops
            DoTitleCheck
            GetMissingEpisodes
            Reload
            RewriteContent
            Scan
            SetLanguage
            SetLockedState
            SetMarkedState
            ''' <summary>
            ''' Needs MediaContainer.SetDetails or Nothing as GenericObject. Nothing removes all assigned movie sets.
            ''' </summary>
            SetMovieset
            SetWatchedState
        End Enum

#End Region 'Nested Types

    End Class

    Public Enum EventType As Integer
        RefreshRow
        SimpleMessage
        TaskManagerEnded
        TaskManagerStarted
    End Enum

#End Region 'Nested Types

End Class