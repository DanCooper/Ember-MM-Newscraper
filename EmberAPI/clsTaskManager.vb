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

                Case TaskItem.TaskType.Scrape
                    DoScrape(currTask)

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

    Private Sub DataFields_ClearOrReplace(ByVal tTaskItem As TaskItem)
        Select Case tTaskItem.ContentType

            Case Enums.ContentType.Movie
                Dim nInfo As New MediaContainers.MainDetails
                If tTaskItem.GenericObject IsNot Nothing AndAlso
                    tTaskItem.GenericObject.GetType Is GetType(MediaContainers.MainDetails) Then
                    nInfo = DirectCast(tTaskItem.GenericObject, MediaContainers.MainDetails)
                Else
                    Return
                End If

                For Each tID In tTaskItem.ListOfID
                    _HasChanged = False
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(tID)

                    If Not tmpDBElement.IsLocked Then
                        With tTaskItem.ScrapeOptions
                            DataField_ClearList(.Actors, tmpDBElement.MainDetails.Actors)
                            DataField_CompareLists(.Certifications, tmpDBElement.MainDetails.Certifications, nInfo.Certifications)
                            DataField_CompareLists(.Countries, tmpDBElement.MainDetails.Countries, nInfo.Countries)
                            DataField_CompareLists(.Credits, tmpDBElement.MainDetails.Credits, nInfo.Credits)
                            DataField_CompareLists(.Directors, tmpDBElement.MainDetails.Directors, nInfo.Directors)
                            DataField_CompareStrings(.Edition, tmpDBElement.Edition, nInfo.Edition)
                            tmpDBElement.MainDetails.Edition = tmpDBElement.Edition
                            DataField_CompareLists(.Genres, tmpDBElement.MainDetails.Genres, nInfo.Genres)
                            DataField_CompareStrings(.MPAA, tmpDBElement.MainDetails.MPAA, nInfo.MPAA)
                            DataField_ClearString(.OriginalTitle, tmpDBElement.MainDetails.OriginalTitle)
                            DataField_ClearString(.Outline, tmpDBElement.MainDetails.Outline)
                            DataField_ClearString(.Plot, tmpDBElement.MainDetails.Plot)
                            DataField_CompareStrings(.Premiered, tmpDBElement.MainDetails.Premiered, nInfo.Premiered)
                            DataField_ClearString(.Ratings, tmpDBElement.MainDetails.Rating)
                            DataField_ClearString(.Ratings, tmpDBElement.MainDetails.Votes)
                            DataField_ClearString(.Runtime, tmpDBElement.MainDetails.Runtime)
                            DataField_CompareLists(.Studios, tmpDBElement.MainDetails.Studios, nInfo.Studios)
                            DataField_CompareStrings(.Tagline, tmpDBElement.MainDetails.Tagline, nInfo.Tagline)
                            DataField_CompareLists(.Tags, tmpDBElement.MainDetails.Tags, nInfo.Tags)
                            DataField_CompareIntegers(.Top250, tmpDBElement.MainDetails.Top250, nInfo.Top250)
                            DataField_ClearString(.TrailerLink, tmpDBElement.MainDetails.Trailer)
                            DataField_CompareIntegers(.UserRating, tmpDBElement.MainDetails.UserRating, nInfo.UserRating)
                            DataField_CompareStrings(.VideoSource, tmpDBElement.VideoSource, nInfo.VideoSource)
                            tmpDBElement.MainDetails.VideoSource = tmpDBElement.VideoSource
                        End With

                        If _HasChanged Then
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                     .Message = tmpDBElement.MainDetails.Title})

                            Master.DB.Save_Movie(tmpDBElement, True, True, False, True, False)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                        End If
                    End If
                Next

            Case Enums.ContentType.MovieSet
                For Each tID In tTaskItem.ListOfID
                    _HasChanged = False
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movieset(tID)

                    If Not tmpDBElement.IsLocked Then
                        With tTaskItem.ScrapeOptions
                            DataField_ClearString(.Plot, tmpDBElement.MainDetails.Plot)
                        End With

                        If _HasChanged Then
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                     .Message = tmpDBElement.MainDetails.Title})

                            Master.DB.Save_Movieset(tmpDBElement, True, True, False, True, True)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                        End If
                    End If
                Next

            Case Enums.ContentType.TVEpisode
                Dim nInfo As New MediaContainers.MainDetails
                If tTaskItem.GenericObject IsNot Nothing AndAlso
                    tTaskItem.GenericObject.GetType Is GetType(MediaContainers.MainDetails) Then
                    nInfo = DirectCast(tTaskItem.GenericObject, MediaContainers.MainDetails)
                Else
                    Return
                End If

                For Each tID In tTaskItem.ListOfID
                    _HasChanged = False
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVEpisode(tID, False)

                    If Not tmpDBElement.IsLocked Then
                        With tTaskItem.ScrapeOptions
                            DataField_ClearList(.Episodes.Actors, tmpDBElement.MainDetails.Actors)
                            DataField_CompareStrings(.Episodes.Aired, tmpDBElement.MainDetails.Aired, nInfo.Aired)
                            DataField_CompareLists(.Episodes.Credits, tmpDBElement.MainDetails.Credits, nInfo.Credits)
                            DataField_CompareLists(.Episodes.Directors, tmpDBElement.MainDetails.Directors, nInfo.Directors)
                            DataField_ClearList(.Episodes.GuestStars, tmpDBElement.MainDetails.GuestStars)
                            DataField_ClearString(.Episodes.Plot, tmpDBElement.MainDetails.Plot)
                            DataField_ClearString(.Episodes.Ratings, tmpDBElement.MainDetails.Rating)
                            DataField_ClearString(.Episodes.Ratings, tmpDBElement.MainDetails.Votes)
                            DataField_ClearString(.Episodes.Runtime, tmpDBElement.MainDetails.Runtime)
                            DataField_CompareIntegers(.Episodes.UserRating, tmpDBElement.MainDetails.UserRating, nInfo.UserRating)
                            DataField_CompareStrings(.Episodes.VideoSource, tmpDBElement.VideoSource, nInfo.VideoSource)
                            tmpDBElement.MainDetails.VideoSource = tmpDBElement.VideoSource
                        End With

                        If _HasChanged Then
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                     .Message = tmpDBElement.MainDetails.Title})

                            Master.DB.Save_TVEpisode(tmpDBElement, True, True, False, False, True)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                        End If
                    End If
                Next

            Case Enums.ContentType.TVSeason
                Dim nInfo As New MediaContainers.MainDetails
                If tTaskItem.GenericObject IsNot Nothing AndAlso
                    tTaskItem.GenericObject.GetType Is GetType(MediaContainers.MainDetails) Then
                    nInfo = DirectCast(tTaskItem.GenericObject, MediaContainers.MainDetails)
                Else
                    Return
                End If

                For Each tID In tTaskItem.ListOfID
                    _HasChanged = False
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVSeason(tID, True, False)

                    If Not tmpDBElement.IsLocked Then

                        With tTaskItem.ScrapeOptions
                            DataField_CompareStrings(.Seasons.Aired, tmpDBElement.MainDetails.Aired, nInfo.Aired)
                            DataField_ClearString(.Seasons.Plot, tmpDBElement.MainDetails.Plot)
                            DataField_ClearString(.Seasons.Title, tmpDBElement.MainDetails.Title)
                        End With

                        If _HasChanged Then
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                     .Message = String.Format("{0}: {1} {2}", tmpDBElement.TVShowDetails.Title, Master.eLang.GetString(650, "Season"), tmpDBElement.MainDetails.Season.ToString)})

                            Master.DB.Save_TVSeason(tmpDBElement, True, False, True)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                        End If
                    End If
                Next

            Case Enums.ContentType.TVShow
                Dim nInfo As New MediaContainers.MainDetails
                If tTaskItem.GenericObject IsNot Nothing AndAlso
                    tTaskItem.GenericObject.GetType Is GetType(MediaContainers.MainDetails) Then
                    nInfo = DirectCast(tTaskItem.GenericObject, MediaContainers.MainDetails)
                Else
                    Return
                End If

                For Each tID In tTaskItem.ListOfID
                    _HasChanged = False
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(tID, False, False, False)

                    If Not tmpDBElement.IsLocked Then
                        With tTaskItem.ScrapeOptions
                            DataField_ClearList(.Actors, tmpDBElement.MainDetails.Actors)
                            DataField_CompareLists(.Certifications, tmpDBElement.MainDetails.Certifications, nInfo.Certifications)
                            DataField_CompareLists(.Countries, tmpDBElement.MainDetails.Countries, nInfo.Countries)
                            DataField_CompareLists(.Creators, tmpDBElement.MainDetails.Creators, nInfo.Creators)
                            DataField_CompareLists(.Directors, tmpDBElement.MainDetails.Directors, nInfo.Directors)
                            DataField_CompareLists(.Genres, tmpDBElement.MainDetails.Genres, nInfo.Genres)
                            DataField_CompareStrings(.MPAA, tmpDBElement.MainDetails.MPAA, nInfo.MPAA)
                            DataField_ClearString(.OriginalTitle, tmpDBElement.MainDetails.OriginalTitle)
                            DataField_ClearString(.Plot, tmpDBElement.MainDetails.Plot)
                            DataField_CompareStrings(.Premiered, tmpDBElement.MainDetails.Premiered, nInfo.Premiered)
                            DataField_ClearString(.Ratings, tmpDBElement.MainDetails.Rating)
                            DataField_ClearString(.Ratings, tmpDBElement.MainDetails.Votes)
                            DataField_ClearString(.Runtime, tmpDBElement.MainDetails.Runtime)
                            DataField_CompareStrings(.Status, tmpDBElement.MainDetails.Status, nInfo.Status)
                            DataField_CompareLists(.Studios, tmpDBElement.MainDetails.Studios, nInfo.Studios)
                            DataField_CompareStrings(.Tagline, tmpDBElement.MainDetails.Tagline, nInfo.Tagline)
                            DataField_CompareLists(.Tags, tmpDBElement.MainDetails.Tags, nInfo.Tags)
                            DataField_CompareIntegers(.UserRating, tmpDBElement.MainDetails.UserRating, nInfo.UserRating)
                        End With

                        If _HasChanged Then
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                     .Message = tmpDBElement.MainDetails.Title})

                            Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
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

    Private Sub DoScrape(ByVal tTaskItem As TaskItem)
        Select Case tTaskItem.ContentType
            Case Enums.ContentType.Movie
                If tTaskItem.SelectionType = Enums.SelectionType.All OrElse
                        tTaskItem.SelectionType = Enums.SelectionType.New Then
                    tTaskItem.ListOfID = (From movies In Master.DB.LoadAll_Movies Select movies.ID).ToList
                End If
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement = Master.DB.Load_Movie(tID)
                    If tmpDBElement.IsOnline Then
                        'Update information bevore scraping to prevent overwriting of later added content
                        Dim nScanner As New Scanner
                        nScanner.GetFolderContents_Movie(tmpDBElement)

                        'Define ScrapeModifiers
                        Dim ActorThumbsAllowed As Boolean = Master.eSettings.MovieActorthumbsAnyEnabled
                        Dim BannerAllowed As Boolean = Master.eSettings.MovieBannerAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
                        Dim ClearArtAllowed As Boolean = Master.eSettings.MovieClearArtAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt)
                        Dim ClearLogoAllowed As Boolean = Master.eSettings.MovieClearLogoAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo)
                        Dim DiscArtAllowed As Boolean = Master.eSettings.MovieDiscArtAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt)
                        Dim ExtrafanartsAllowed As Boolean = Master.eSettings.MovieExtrafanartsAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                        Dim ExtrathumbsAllowed As Boolean = Master.eSettings.MovieExtrathumbsAnyEnabled AndAlso (Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)) 'OrElse Master.eSettings.MovieExtrathumbsVideoExtraction)
                        Dim FanartAllowed As Boolean = Master.eSettings.MovieFanartAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                        Dim KeyArtAllowed As Boolean = Master.eSettings.MovieKeyartAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
                        Dim LandscapeAllowed As Boolean = Master.eSettings.MovieLandscapeAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape)
                        Dim PosterAllowed As Boolean = Master.eSettings.MoviePosterAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
                        Dim ThemeAllowed As Boolean = Master.eSettings.MovieThemeAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie()
                        Dim TrailerAllowed As Boolean = Master.eSettings.MovieTrailerAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Trailer_Movie()
                        Dim scrapeModifier As New Structures.ScrapeModifiers With {
                            .DoSearch = tTaskItem.ScrapeModifiers.DoSearch,
                            .Actorthumbs = tTaskItem.ScrapeModifiers.Actorthumbs AndAlso ActorThumbsAllowed,
                            .Banner = tTaskItem.ScrapeModifiers.Banner AndAlso BannerAllowed,
                            .Clearart = tTaskItem.ScrapeModifiers.Clearart AndAlso ClearArtAllowed,
                            .Clearlogo = tTaskItem.ScrapeModifiers.Clearlogo AndAlso ClearLogoAllowed,
                            .Discart = tTaskItem.ScrapeModifiers.Discart AndAlso DiscArtAllowed,
                            .Extrafanarts = tTaskItem.ScrapeModifiers.Extrafanarts AndAlso ExtrafanartsAllowed,
                            .Extrathumbs = tTaskItem.ScrapeModifiers.Extrathumbs AndAlso ExtrathumbsAllowed,
                            .Fanart = tTaskItem.ScrapeModifiers.Fanart AndAlso FanartAllowed,
                            .Keyart = tTaskItem.ScrapeModifiers.Keyart AndAlso KeyArtAllowed,
                            .Landscape = tTaskItem.ScrapeModifiers.Landscape AndAlso LandscapeAllowed,
                            .Metadata = tTaskItem.ScrapeModifiers.Metadata,
                            .Information = tTaskItem.ScrapeModifiers.Information,
                            .Poster = tTaskItem.ScrapeModifiers.Poster AndAlso PosterAllowed,
                            .Theme = tTaskItem.ScrapeModifiers.Theme AndAlso ThemeAllowed,
                            .Trailer = tTaskItem.ScrapeModifiers.Trailer AndAlso TrailerAllowed
                        }

                        Select Case tTaskItem.SelectionType
                            Case Enums.SelectionType.[New]
                                If Not tmpDBElement.IsNew Then Continue For
                            Case Enums.SelectionType.Marked
                                If Not tmpDBElement.IsMarked Then Continue For
                            Case Enums.SelectionType.MissingContent
                                If tmpDBElement.ImagesContainer.BannerSpecified Then scrapeModifier.Banner = False
                                If tmpDBElement.ImagesContainer.ClearArtSpecified Then scrapeModifier.Clearart = False
                                If tmpDBElement.ImagesContainer.ClearLogoSpecified Then scrapeModifier.Clearlogo = False
                                If tmpDBElement.ImagesContainer.DiscArtSpecified Then scrapeModifier.Discart = False
                                If tmpDBElement.ImagesContainer.ExtrafanartsSpecified Then scrapeModifier.Extrafanarts = False
                                If tmpDBElement.ImagesContainer.ExtrathumbsSpecified Then scrapeModifier.Extrathumbs = False
                                If tmpDBElement.ImagesContainer.FanartSpecified Then scrapeModifier.Fanart = False
                                If tmpDBElement.ImagesContainer.KeyartSpecified Then scrapeModifier.Keyart = False
                                If tmpDBElement.ImagesContainer.LandscapeSpecified Then scrapeModifier.Landscape = False
                                If tmpDBElement.ImagesContainer.PosterSpecified Then scrapeModifier.Poster = False
                                If tmpDBElement.NfoPathSpecified Then scrapeModifier.Information = False
                                If tmpDBElement.ThemeSpecified Then scrapeModifier.Theme = False
                                If tmpDBElement.TrailerSpecified Then scrapeModifier.Trailer = False
                        End Select
                        If Functions.ScrapeModifiersAnyEnabled(scrapeModifier) Then
                            tmpDBElement.ScrapeModifiers = scrapeModifier
                            tmpDBElement.ScrapeOptions = tTaskItem.ScrapeOptions
                            tmpDBElement.ScrapeType = tTaskItem.ScrapeType
                            Scraper.Run(tmpDBElement)
                        End If
                    End If
                Next
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
                                                                         .ContentType = tTaskItem.ContentType,
                                                                         .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                                         .ID = CLng(SQLReader_GetMovies("idMovie"))})
                                    End If
                                ElseIf bLevFail_OldValue Then
                                    par_OutOfTolerance.Value = False
                                    par_idMovie.Value = CLng(SQLReader_GetMovies("idMovie"))
                                    SQLCommand_Update.ExecuteNonQuery()
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                     .ContentType = tTaskItem.ContentType,
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
                                                 .Message = tmpDBElement.MainDetails.Title})

                    Dim ScrapeModifiers As New Structures.ScrapeModifiers
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainNFO, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withEpisodes, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withSeasons, True)

                    'If Addons.Instance.ScrapeData_TVShow(tmpDBElement, ScrapeModifiers, Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, True) Then
                    '    For Each nMissingSeason In tmpDBElement.Seasons.Where(Function(f) Not f.IDSpecified)
                    '        Master.DB.Save_TVSeason(nMissingSeason, True, False, False)
                    '        bNewSeasons = True
                    '    Next
                    '    For Each nMissingEpisode In tmpDBElement.Episodes.Where(Function(f) Not f.FilenameSpecified)
                    '        Master.DB.Save_TVEpisode(nMissingEpisode, True, False, False, False, False)
                    '    Next
                    'End If

                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                 .ContentType = tTaskItem.ContentType,
                                                 .EventType = Enums.TaskManagerEventType.RefreshRow,
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
                                                         .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                         .Message = tmpDBElement.MainDetails.Title})

                            Master.DB.Save_Movie(tmpDBElement, True, True, False, False, False)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = tTaskItem.ContentType,
                                                         .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                         .ID = tmpDBElement.ID})
                        End If
                    Next

                Case Enums.ContentType.MovieSet
                    For Each tID In tTaskItem.ListOfID
                        If bwTaskManager.CancellationPending Then Return
                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movieset(tID)

                        If Not tmpDBElement.Language = strNewLanguage Then
                            tmpDBElement.Language = strNewLanguage
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                         .Message = tmpDBElement.MainDetails.Title})

                            Master.DB.Save_Movieset(tmpDBElement, True, True, False, False, False)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = tTaskItem.ContentType,
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
                                                         .Message = tmpDBElement.MainDetails.Title})

                            Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = tTaskItem.ContentType,
                                                         .EventType = Enums.TaskManagerEventType.RefreshRow,
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
                                                     .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                     .Message = tmpDBElement.MainDetails.Title})

                        Master.DB.Save_Movie(tmpDBElement, True, True, False, False, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

            Case Enums.ContentType.MovieSet
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
                                                     .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                     .Message = tmpDBElement.MainDetails.Title})

                        Master.DB.Save_Movieset(tmpDBElement, True, True, False, False, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
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
                        lstTVSeasonIDs.Add(Master.DB.Get_TVSeasonIdByEpisode(tmpDBElement))
                        lstTVShowIDs.Add(tmpDBElement.ShowID)
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                     .Message = tmpDBElement.MainDetails.Title})

                        Master.DB.Save_TVEpisode(tmpDBElement, True, True, False, False, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
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
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = nTVSeasonID})
                    Next
                    For Each nTVShowID In lstTVShowIDs
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVShow,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
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
                                                     .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                     .Message = String.Format("{0}: {1} {2}", tmpDBElement.TVShowDetails.Title, Master.eLang.GetString(650, "Season"), tmpDBElement.MainDetails.Season.ToString)})

                        Master.DB.Save_TVSeason(tmpDBElement, True, True, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tmpDBElement.ID})
                    End If
                Next

                If Not bRegressiveSync Then
                    'refresh other rows if the tv show entries not will be refreshed by a higher task
                    lstTVShowIDs = lstTVShowIDs.Distinct.ToList
                    For Each nTVShowID In lstTVShowIDs
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVShow,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
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
                                                     .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                     .Message = tmpDBElement.MainDetails.Title})

                        Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
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
                                                                 .ContentType = tTaskItem.ContentType,
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
                                                                 .Message = SQLReader_Get("Title").ToString})
                                    par_ID.Value = tID
                                    par_Mark.Value = tTaskItem.CommonBooleanValue
                                    SQLCommand_Update.ExecuteNonQuery()
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .ContentType = tTaskItem.ContentType,
                                                                 .EventType = Enums.TaskManagerEventType.RefreshRow,
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
                                    lstTVSeasonIDs.Add(Master.DB.Get_TVSeasonIdByShowIdAndSeasonNumber(CLng(SQLReader_Get("idShow")), CInt(SQLReader_Get("Season"))))
                                    lstTVShowIDs.Add(CLng(SQLReader_Get("idShow")))
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .ContentType = tTaskItem.ContentType,
                                                                 .EventType = Enums.TaskManagerEventType.RefreshRow,
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
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = nTVSeasonID})
                    Next
                    For Each nTVShowID In lstTVShowIDs
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVShow,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
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
                        SetMarkedState(nTaskItem, True)
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
                                                                 .Message = SQLReader_Get("Title").ToString})
                                    par_ID.Value = tID
                                    par_Mark.Value = tTaskItem.CommonBooleanValue
                                    SQLCommand_Update.ExecuteNonQuery()
                                    lstTVShowIDs.Add(CLng(SQLReader_Get("idShow")))
                                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                                 .ContentType = tTaskItem.ContentType,
                                                                 .EventType = Enums.TaskManagerEventType.RefreshRow,
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
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
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
                        SQLCommand_Get.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0};", tID)
                        Using SQLReader_Get As SQLite.SQLiteDataReader = SQLCommand_Get.ExecuteReader()
                            While SQLReader_Get.Read
                                nTaskItem.ListOfID.Add(CLng(SQLReader_Get("idSeason")))
                            End While
                        End Using
                    End Using
                    SetMarkedState(nTaskItem, True)

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
                                                                 .ContentType = tTaskItem.ContentType,
                                                                 .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                                 .ID = tID})
                                End While
                            End Using
                        End Using
                    End Using
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
                        If Not tmpDBElement.MainDetails.LastPlayedSpecified OrElse Not tmpDBElement.MainDetails.PlayCountSpecified Then
                            tmpDBElement.MainDetails.LastPlayed = If(tmpDBElement.MainDetails.LastPlayedSpecified, tmpDBElement.MainDetails.LastPlayed, Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                            tmpDBElement.MainDetails.PlayCount = If(tmpDBElement.MainDetails.PlayCountSpecified, tmpDBElement.MainDetails.PlayCount, 1)
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.MainDetails.LastPlayedSpecified OrElse tmpDBElement.MainDetails.PlayCountSpecified Then
                            tmpDBElement.MainDetails.LastPlayed = String.Empty
                            tmpDBElement.MainDetails.PlayCount = 0
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                     .Message = tmpDBElement.MainDetails.Title})

                        Master.DB.Save_Movie(tmpDBElement, True, True, False, True, False)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
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
                        If Not tmpDBElement.MainDetails.LastPlayedSpecified OrElse Not tmpDBElement.MainDetails.PlayCountSpecified Then
                            tmpDBElement.MainDetails.LastPlayed = If(tmpDBElement.MainDetails.LastPlayedSpecified, tmpDBElement.MainDetails.LastPlayed, Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                            tmpDBElement.MainDetails.PlayCount = If(tmpDBElement.MainDetails.PlayCountSpecified, tmpDBElement.MainDetails.PlayCount, 1)
                            bHasChanged = True
                        End If
                    Else
                        If tmpDBElement.MainDetails.LastPlayedSpecified OrElse tmpDBElement.MainDetails.PlayCountSpecified Then
                            tmpDBElement.MainDetails.LastPlayed = String.Empty
                            tmpDBElement.MainDetails.PlayCount = 0
                            bHasChanged = True
                        End If
                    End If

                    If bHasChanged Then
                        lstTVSeasonIDs.Add(Master.DB.Get_TVSeasonIdByEpisode(tmpDBElement))
                        lstTVShowIDs.Add(tmpDBElement.ShowID)
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                     .Message = tmpDBElement.MainDetails.Title})

                        Master.DB.Save_TVEpisode(tmpDBElement, True, True, False, False, True)

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
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
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = nTVSeasonID})
                    Next
                    For Each nTVShowID In lstTVShowIDs
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVShow,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = nTVShowID})
                    Next
                End If

            Case Enums.ContentType.TVSeason
                For Each tID In tTaskItem.ListOfID
                    If bwTaskManager.CancellationPending Then Return
                    Dim tmpDBElement_TVSeason As Database.DBElement = Master.DB.Load_TVSeason(tID, True, True)
                    'exclude "* All Seasons" entry from changing WatchedState
                    If Not tmpDBElement_TVSeason.MainDetails.Season_IsAllSeasons Then
                        For Each tEpisode As Database.DBElement In tmpDBElement_TVSeason.Episodes.OrderBy(Function(f) f.MainDetails.Episode)
                            If bwTaskManager.CancellationPending Then Exit For
                            Dim bHasChanged As Boolean = False

                            If tTaskItem.CommonBooleanValue Then
                                If Not tEpisode.MainDetails.LastPlayedSpecified OrElse Not tEpisode.MainDetails.PlayCountSpecified Then
                                    tEpisode.MainDetails.LastPlayed = If(tEpisode.MainDetails.LastPlayedSpecified, tEpisode.MainDetails.LastPlayed, Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                    tEpisode.MainDetails.PlayCount = If(tEpisode.MainDetails.PlayCountSpecified, tEpisode.MainDetails.PlayCount, 1)
                                    bHasChanged = True
                                End If
                            Else
                                If tEpisode.MainDetails.LastPlayedSpecified OrElse tEpisode.MainDetails.PlayCountSpecified Then
                                    tEpisode.MainDetails.LastPlayed = String.Empty
                                    tEpisode.MainDetails.PlayCount = 0
                                    bHasChanged = True
                                End If
                            End If

                            If bHasChanged Then
                                bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                             .Message = tEpisode.MainDetails.Title})

                                Master.DB.Save_TVEpisode(tEpisode, True, True, False, False, True)

                                bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                             .ContentType = Enums.ContentType.TVEpisode,
                                                             .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                             .ID = tEpisode.ID})
                            End If
                        Next

                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = tTaskItem.ContentType,
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
                    For Each tEpisode As Database.DBElement In tmpDBElement_TVShow.Episodes.OrderBy(Function(f) f.MainDetails.Season).OrderBy(Function(f) f.MainDetails.Episode)
                        If bwTaskManager.CancellationPending Then Exit For
                        Dim bHasChanged As Boolean = False

                        If tTaskItem.CommonBooleanValue Then
                            If Not tEpisode.MainDetails.LastPlayedSpecified OrElse Not tEpisode.MainDetails.PlayCountSpecified Then
                                tEpisode.MainDetails.LastPlayed = If(tEpisode.MainDetails.LastPlayedSpecified, tEpisode.MainDetails.LastPlayed, Date.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                tEpisode.MainDetails.PlayCount = If(tEpisode.MainDetails.PlayCountSpecified, tEpisode.MainDetails.PlayCount, 1)
                                bHasChanged = True
                            End If
                        Else
                            If tEpisode.MainDetails.LastPlayedSpecified OrElse tEpisode.MainDetails.PlayCountSpecified Then
                                tEpisode.MainDetails.LastPlayed = String.Empty
                                tEpisode.MainDetails.PlayCount = 0
                                bHasChanged = True
                            End If
                        End If

                        If bHasChanged Then
                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .EventType = Enums.TaskManagerEventType.SimpleMessage,
                                                         .Message = tEpisode.MainDetails.Title})

                            Master.DB.Save_TVEpisode(tEpisode, True, True, False, False, True)

                            bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                         .ContentType = Enums.ContentType.TVEpisode,
                                                         .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                         .ID = tEpisode.ID})
                        End If
                    Next

                    For Each tSeason In tmpDBElement_TVShow.Seasons.OrderBy(Function(f) f.MainDetails.Season)
                        bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                     .ContentType = Enums.ContentType.TVSeason,
                                                     .EventType = Enums.TaskManagerEventType.RefreshRow,
                                                     .ID = tSeason.ID})
                    Next

                    bwTaskManager.ReportProgress(-1, New ProgressValue With {
                                                 .ContentType = tTaskItem.ContentType,
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
        Dim EventType As Enums.TaskManagerEventType
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

        Public Property ListOfID As New List(Of Long)

        Public Property ScanOrCleanOptions As New Scanner.ScanOrCleanOptions

        Public Property ScrapeModifiers As New Structures.ScrapeModifiers

        Public Property ScrapeOptions As New Structures.ScrapeOptions

        Public Property ScrapeType As Enums.ScrapeType

        Public Property SelectionType As Enums.SelectionType

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
            Scrape
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

#End Region

End Class