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

Imports System
Imports System.Windows.Forms
Imports System.IO
Imports System.Xml.Serialization
Imports System.Data.SQLite
Imports System.Xml.Linq
Imports NLog

''' <summary>
''' Class defining and implementing the interface to the database
''' </summary>
''' <remarks></remarks>
Public Class Database

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    ReadOnly _connStringTemplate As String = "Data Source=""{0}"";Version=3;Compress=True"
    Protected _myvideosDBConn As SQLiteConnection
    ' NOTE: This will use another DB because: can grow alot, Don't want to stress Media DB with this stuff
    'Protected _jobsDBConn As SQLiteConnection

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property MyVideosDBConn() As SQLiteConnection
        Get
            Return _myvideosDBConn
        End Get
    End Property

    'Public ReadOnly Property JobsDBConn() As SQLiteConnection
    '    Get
    '        Return _jobsDBConn
    '    End Get
    'End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Iterates db entries to check if the paths to the movie or TV files are valid. 
    ''' If not, remove all entries pertaining to the movie.
    ''' </summary>
    ''' <param name="CleanMovies">If <c>True</c>, process the movie files</param>
    ''' <param name="CleanTV">If <c>True</c>, process the TV files</param>
    ''' <param name="source">Optional. If provided, only process entries from that named source.</param>
    ''' <remarks></remarks>
    Public Sub Clean(ByVal CleanMovies As Boolean, ByVal CleanMovieSets As Boolean, ByVal CleanTV As Boolean, Optional ByVal source As String = "")
        Dim fInfo As FileInfo
        Dim tPath As String = String.Empty
        Dim sPath As String = String.Empty
        Try
            Using SQLtransaction As SQLite.SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                If CleanMovies Then

                    Dim MoviePaths As List(Of String) = GetMoviePaths()
                    MoviePaths.Sort()

                    'get a listing of sources and their recursive properties
                    Dim SourceList As New List(Of SourceHolder)
                    Dim tSource As SourceHolder

                    Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                        If source = String.Empty Then
                            SQLcommand.CommandText = "SELECT Path, Name, Recursive, Single FROM sources;"
                        Else
                            SQLcommand.CommandText = String.Format("SELECT Path, Name, Recursive, Single FROM sources WHERE Name=""{0}""", source)
                        End If
                        Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                            While SQLreader.Read
                                SourceList.Add(New SourceHolder With {.Name = SQLreader("Name").ToString, .Path = SQLreader("Path").ToString, .Recursive = Convert.ToBoolean(SQLreader("Recursive")), .isSingle = Convert.ToBoolean(SQLreader("Single"))})
                            End While
                        End Using
                    End Using

                    Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                        If source = String.Empty Then
                            SQLcommand.CommandText = "SELECT MoviePath, idMovie, Source, Type FROM movie ORDER BY MoviePath DESC;"
                        Else
                            SQLcommand.CommandText = String.Format("SELECT MoviePath, idMovie, Source, Type FROM movie WHERE Source = ""{0}"" ORDER BY MoviePath DESC;", source)
                        End If
                        Using SQLReader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                            While SQLReader.Read
                                If Not File.Exists(SQLReader("MoviePath").ToString) OrElse Not Master.eSettings.FileSystemValidExts.Contains(Path.GetExtension(SQLReader("MoviePath").ToString).ToLower) OrElse _
                                    Master.ExcludeDirs.Exists(Function(s) SQLReader("MoviePath").ToString.ToLower.StartsWith(s.ToLower)) Then
                                    MoviePaths.Remove(SQLReader("MoviePath").ToString)
                                    Master.DB.DeleteMovieFromDB(Convert.ToInt64(SQLReader("ID")), True)
                                ElseIf Master.eSettings.MovieSkipLessThan > 0 Then
                                    fInfo = New FileInfo(SQLReader("MoviePath").ToString)
                                    If ((Not Master.eSettings.MovieSkipStackedSizeCheck OrElse Not StringUtils.IsStacked(fInfo.Name)) AndAlso fInfo.Length < Master.eSettings.MovieSkipLessThan * 1048576) Then
                                        MoviePaths.Remove(SQLReader("MoviePath").ToString)
                                        Master.DB.DeleteMovieFromDB(Convert.ToInt64(SQLReader("ID")), True)
                                    End If
                                Else
                                    tSource = SourceList.OrderByDescending(Function(s) s.Path).FirstOrDefault(Function(s) s.Name = SQLReader("Source").ToString)
                                    If Not IsNothing(tSource) Then
                                        If Directory.GetParent(Directory.GetParent(SQLReader("MoviePath").ToString).FullName).Name.ToLower = "bdmv" Then
                                            tPath = Directory.GetParent(Directory.GetParent(SQLReader("MoviePath").ToString).FullName).FullName
                                        Else
                                            tPath = Directory.GetParent(SQLReader("MoviePath").ToString).FullName
                                        End If
                                        sPath = FileUtils.Common.GetDirectory(tPath).ToLower
                                        If tSource.Recursive = False AndAlso tPath.Length > tSource.Path.Length AndAlso If(sPath = "video_ts" OrElse sPath = "bdmv", tPath.Substring(tSource.Path.Length).Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar).Count > 2, tPath.Substring(tSource.Path.Length).Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar).Count > 1) Then
                                            MoviePaths.Remove(SQLReader("MoviePath").ToString)
                                            Master.DB.DeleteMovieFromDB(Convert.ToInt64(SQLReader("ID")), True)
                                        ElseIf Not Convert.ToBoolean(SQLReader("Type")) AndAlso tSource.isSingle AndAlso Not MoviePaths.Where(Function(s) SQLReader("MoviePath").ToString.ToLower.StartsWith(tSource.Path.ToLower)).Count = 1 Then
                                            MoviePaths.Remove(SQLReader("MoviePath").ToString)
                                            Master.DB.DeleteMovieFromDB(Convert.ToInt64(SQLReader("ID")), True)
                                        End If
                                    Else
                                        'orphaned
                                        MoviePaths.Remove(SQLReader("MoviePath").ToString)
                                        Master.DB.DeleteMovieFromDB(Convert.ToInt64(SQLReader("ID")), True)
                                    End If
                                End If
                            End While
                        End Using
                    End Using
                End If

                If CleanMovieSets Then
                    Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                        SQLcommand.CommandText = "SELECT Sets.ID, COUNT(MoviesSets.MovieID) AS 'Count' FROM Sets LEFT OUTER JOIN MoviesSets ON Sets.ID = MoviesSets.SetID GROUP BY Sets.ID ORDER BY Sets.ID COLLATE NOCASE;"
                        Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                            While SQLreader.Read
                                If Convert.ToInt64(SQLreader("Count")) = 0 Then
                                    Master.DB.DeleteMovieSetFromDB(Convert.ToInt64(SQLreader("ID")), True)
                                End If
                            End While
                        End Using
                    End Using
                End If

                If CleanTV Then
                    Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                        If String.IsNullOrEmpty(source) Then
                            SQLcommand.CommandText = "SELECT TVEpPath FROM TVEpPaths;"
                        Else
                            SQLcommand.CommandText = String.Format("SELECT TVEpPath FROM TVEpPaths INNER JOIN episode ON TVEpPaths.ID = episode.TVEpPathID WHERE episode.Source =""{0}"";", source)
                        End If

                        Using SQLReader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                            While SQLReader.Read
                                If Not File.Exists(SQLReader("TVEpPath").ToString) OrElse Not Master.eSettings.FileSystemValidExts.Contains(Path.GetExtension(SQLReader("TVEpPath").ToString).ToLower) OrElse _
                                    Master.ExcludeDirs.Exists(Function(s) SQLReader("TVEpPath").ToString.ToLower.StartsWith(s.ToLower)) Then
                                    Master.DB.DeleteTVEpFromDBByPath(SQLReader("TVEpPath").ToString, False, True)
                                End If
                            End While
                        End Using
                        'tvshows with no more real episodes
                        SQLcommand.CommandText = "DELETE FROM tvshow WHERE NOT EXISTS (SELECT episode.idShow FROM episode WHERE episode.idShow = tvshow.idShow AND episode.Missing = 0)"
                        SQLcommand.ExecuteNonQuery()
                        SQLcommand.CommandText = String.Concat("DELETE FROM tvshow WHERE idShow NOT IN (SELECT idShow FROM episode);")
                        SQLcommand.ExecuteNonQuery()
                        SQLcommand.CommandText = String.Concat("DELETE FROM actorlinktvshow WHERE idShow NOT IN (SELECT idShow FROM tvshow);")
                        SQLcommand.ExecuteNonQuery()
                        SQLcommand.CommandText = "DELETE FROM episode WHERE idShow NOT IN (SELECT idShow FROM tvshow);"
                        SQLcommand.ExecuteNonQuery()
                        'orphaned paths
                        SQLcommand.CommandText = "DELETE FROM TVEpPaths WHERE NOT EXISTS (SELECT episode.TVEpPathID FROM episode WHERE episode.TVEpPathID = TVEpPaths.ID AND episode.Missing = 0)"
                        SQLcommand.ExecuteNonQuery()
                    End Using

                    CleanSeasons(True)
                End If

                SQLtransaction.Commit()
            End Using

            ' Housekeeping - consolidate and pack database using vacuum command http://www.sqlite.org/lang_vacuum.html
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = "VACUUM;"
                SQLcommand.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Remove from the database the TV seasons for which there are no episodes defined
    ''' </summary>
    ''' <param name="BatchMode">If <c>False</c>, the action is wrapped in a transaction</param>
    ''' <remarks></remarks>
    Public Sub CleanSeasons(Optional ByVal BatchMode As Boolean = False)
        Dim SQLTrans As SQLite.SQLiteTransaction = Nothing
        Try
            If Not BatchMode Then SQLTrans = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLCommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLCommand.CommandText = "DELETE FROM TVSeason WHERE NOT EXISTS (SELECT episode.Season FROM episode WHERE episode.Season = TVSeason.Season AND episode.idShow = TVSeason.TVShowID) AND TVSeason.Season <> 999"
                SQLCommand.ExecuteNonQuery()
            End Using
            If Not BatchMode Then SQLTrans.Commit()
            SQLTrans = Nothing
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        If SQLTrans IsNot Nothing Then SQLTrans.Dispose()
    End Sub
    ''' <summary>
    ''' Remove the New flag from database entries (movies, TVShows, TVSeason, TVEps)
    ''' </summary>
    ''' <remarks>
    ''' 2013/12/13 Dekker500 - Check that MediaDBConn IsNot Nothing before continuing, 
    '''                        otherwise shutdown after a failed startup (before DB initialized) 
    '''                        will trow exception
    ''' </remarks>
    Public Sub ClearNew()
        If (Master.DB.MyVideosDBConn IsNot Nothing) Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand.CommandText = "UPDATE movie SET New = (?);"
                    Dim parNew As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parNew", DbType.Boolean, 0, "New")
                    parNew.Value = False
                    SQLcommand.ExecuteNonQuery()
                End Using
                Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand.CommandText = "UPDATE Sets SET New = (?);"
                    Dim parNew As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parNew", DbType.Boolean, 0, "New")
                    parNew.Value = False
                    SQLcommand.ExecuteNonQuery()
                End Using
                Using SQLShowcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLShowcommand.CommandText = "UPDATE tvshow SET New = (?);"
                    Dim parShowNew As SQLite.SQLiteParameter = SQLShowcommand.Parameters.Add("parShowNew", DbType.Boolean, 0, "New")
                    parShowNew.Value = False
                    SQLShowcommand.ExecuteNonQuery()
                End Using
                Using SQLSeasoncommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLSeasoncommand.CommandText = "UPDATE TVSeason SET New = (?);"
                    Dim parSeasonNew As SQLite.SQLiteParameter = SQLSeasoncommand.Parameters.Add("parSeasonNew", DbType.Boolean, 0, "New")
                    parSeasonNew.Value = False
                    SQLSeasoncommand.ExecuteNonQuery()
                End Using
                Using SQLEpcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLEpcommand.CommandText = "UPDATE episode SET New = (?);"
                    Dim parEpNew As SQLite.SQLiteParameter = SQLEpcommand.Parameters.Add("parEpNew", DbType.Boolean, 0, "New")
                    parEpNew.Value = False
                    SQLEpcommand.ExecuteNonQuery()
                End Using
                SQLtransaction.Commit()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Close the databases
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CloseMyVideosDB()
        CloseDatabase(_myvideosDBConn)
        'CloseDatabase(_jobsDBConn)

        If Not IsNothing(_myvideosDBConn) Then
            _myvideosDBConn = Nothing
        End If
        'If Not IsNothing(_jobsDBConn) Then
        '    _jobsDBConn = Nothing
        'End If
    End Sub
    ''' <summary>
    ''' Perform the actual closing of the given database connection
    ''' </summary>
    ''' <param name="connection">Database connection on which to perform closing activities</param>
    ''' <remarks></remarks>
    Protected Sub CloseDatabase(ByRef connection As SQLiteConnection)
        If IsNothing(connection) Then
            Return
        End If

        Try
            ' Housekeeping - consolidate and pack database using vacuum command http://www.sqlite.org/lang_vacuum.html
            Using command As SQLiteCommand = connection.CreateCommand()
                command.CommandText = "VACUUM;"
                command.ExecuteNonQuery()
            End Using

            connection.Close()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & vbTab & "There was a problem closing the media database.", ex)
        Finally
            connection.Dispose()
        End Try
    End Sub
    ''' <summary>
    ''' Creates the connection to the MediaDB database
    ''' </summary>
    ''' <returns><c>True</c> if the database needed to be created (is new), <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Function ConnectMyVideosDB() As Boolean

        'set database version
        Dim MyVideosDBVersion As Integer = 14

        'set database filename
        Dim MyVideosDB As String = String.Format("MyVideos{0}.emm", MyVideosDBVersion)

        'TODO Warning - This method should be marked as Protected and references re-directed to Connect() above
        If Not IsNothing(_myvideosDBConn) Then
            Return False
            'Throw New InvalidOperationException("A database connection is already open, can't open another.")
        End If

        Dim MyVideosDBFile As String = FileUtils.Common.ReturnSettingsFile("Settings", MyVideosDB)

        'check if an older DB version still exist
        If Not File.Exists(MyVideosDBFile) Then
            For i As Integer = MyVideosDBVersion - 1 To 2 Step -1
                Dim oldMyVideosDB As String = String.Format("MyVideos{0}.emm", i)
                Dim oldMyVideosDBFile As String = FileUtils.Common.ReturnSettingsFile("Settings", oldMyVideosDB)
                If File.Exists(oldMyVideosDBFile) Then
                    PatchDatabase(oldMyVideosDBFile, MyVideosDBFile, i, MyVideosDBVersion)
                    Exit For
                End If
            Next
        End If

        Dim isNew As Boolean = Not File.Exists(MyVideosDBFile)

        Try
            _myvideosDBConn = New SQLiteConnection(String.Format(_connStringTemplate, MyVideosDBFile))
            _myvideosDBConn.Open()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & vbTab & "Unable to open media database connection.", ex)
        End Try

        Try
            If isNew Then
                Dim sqlCommand As String = File.ReadAllText(FileUtils.Common.ReturnSettingsFile("DB", String.Format("MyVideosDBSQL_v{0}.txt", MyVideosDBVersion)))

                Using transaction As SQLite.SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                    Using command As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                        command.CommandText = sqlCommand
                        command.ExecuteNonQuery()
                    End Using
                    transaction.Commit()
                End Using
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & vbTab & "Error creating database", ex)
            File.Delete(MyVideosDBFile)
        End Try
        Return isNew
    End Function

    ''' <summary>
    ''' Remove all information related to a movie from the database.
    ''' </summary>
    ''' <param name="ID">ID of the movie to remove, as stored in the database.</param>
    ''' <param name="BatchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Function DeleteMovieFromDB(ByVal ID As Long, Optional ByVal BatchMode As Boolean = False) As Boolean
        Try
            Dim SQLtransaction As SQLite.SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DELETE FROM movie WHERE idMovie = ", ID, ";")
                SQLcommand.ExecuteNonQuery()
            End Using
            If Not BatchMode Then SQLtransaction.Commit()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Return False
        End Try
        Return True
    End Function
    ''' <summary>
    ''' Remove all information related to a movieset from the database.
    ''' </summary>
    ''' <param name="ID">ID of the movieset to remove, as stored in the database.</param>
    ''' <param name="BatchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Function DeleteMovieSetFromDB(ByVal ID As Long, Optional ByVal BatchMode As Boolean = False) As Boolean
        Try
            'first get a list of all movies in the movieset to remove the movieset information from NFO
            Dim moviesToSave As New List(Of Structures.DBMovie)

            Dim SQLtransaction As SQLite.SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT MovieID FROM MoviesSets ", _
                                                       "WHERE SetID = ", ID, ";")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        Dim movie As New Structures.DBMovie
                        If Not DBNull.Value.Equals(SQLreader("MovieID")) Then movie = LoadMovieFromDB(Convert.ToInt64(SQLreader("MovieID")))
                        moviesToSave.Add(movie)
                    End While
                End Using
            End Using

            'remove the movieset from movie and write new movie NFOs
            If moviesToSave.Count > 0 Then
                For Each movie In moviesToSave
                    movie.Movie.RemoveSet(ID)
                    SaveMovieToDB(movie, False, BatchMode, True)
                Next
            End If

            'delete all movieset images and if this setting is enabled
            If Master.eSettings.MovieSetCleanFiles Then
                Dim tmpImage As New Images
                Dim MovieSet As Structures.DBMovieSet = Master.DB.LoadMovieSetFromDB(ID)
                tmpImage.DeleteMovieSetBanner(MovieSet)
                tmpImage.DeleteMovieSetClearArt(MovieSet)
                tmpImage.DeleteMovieSetClearLogo(MovieSet)
                tmpImage.DeleteMovieSetDiscArt(MovieSet)
                tmpImage.DeleteMovieSetFanart(MovieSet)
                tmpImage.DeleteMovieSetLandscape(MovieSet)
                tmpImage.DeleteMovieSetPoster(MovieSet)
            End If

            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT MovieID FROM MoviesSets ", _
                                                       "WHERE SetID = ", ID, ";")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        Dim movie As New Structures.DBMovie
                        If Not DBNull.Value.Equals(SQLreader("MovieID")) Then movie = LoadMovieFromDB(Convert.ToInt64(SQLreader("MovieID")))
                        moviesToSave.Add(movie)
                    End While
                End Using
            End Using

            'remove the movieset and still existing moviessets entries
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DELETE FROM sets WHERE idSet = ", ID, ";")
                SQLcommand.ExecuteNonQuery()
            End Using
            If Not BatchMode Then SQLtransaction.Commit()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Remove all information related to a TV episode from the database.
    ''' </summary>
    ''' <param name="ID">ID of the episode to remove, as stored in the database.</param>
    ''' <param name="BatchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Function DeleteTVEpFromDB(ByVal ID As Long, ByVal Force As Boolean, ByVal DoCleanSeasons As Boolean, Optional ByVal BatchMode As Boolean = False) As Boolean
        Try
            Dim SQLtransaction As SQLite.SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT TVEpPathID, Missing FROM episode WHERE idEpisode = ", ID, ";")
                Using SQLReader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader
                    While SQLReader.Read
                        Using SQLECommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                            If Force Then
                                SQLECommand.CommandText = String.Concat("DELETE FROM episode WHERE idEpisode = ", ID, ";")
                                SQLECommand.ExecuteNonQuery()

                                If DoCleanSeasons Then Master.DB.CleanSeasons(True)
                            ElseIf Not Convert.ToBoolean(SQLReader("Missing")) Then 'already marked as missing, no need for another query
                                SQLECommand.CommandText = String.Concat("DELETE FROM TVEpPaths WHERE ID = ", Convert.ToInt32(SQLReader("TVEpPathID")), ";")
                                SQLECommand.ExecuteNonQuery()
                                SQLECommand.CommandText = String.Concat("DELETE FROM TVVStreams WHERE TVEpID = ", ID, ";")
                                SQLECommand.ExecuteNonQuery()
                                SQLECommand.CommandText = String.Concat("DELETE FROM TVAStreams WHERE TVEpID = ", ID, ";")
                                SQLECommand.ExecuteNonQuery()
                                SQLECommand.CommandText = String.Concat("DELETE FROM TVSubs WHERE TVEpID = ", ID, ";")
                                SQLECommand.ExecuteNonQuery()
                                SQLECommand.CommandText = String.Concat("UPDATE episode SET HasPoster = 0, HasFanart = 0, HasNfo = 0, New = 0, New = 0, ", _
                                                                        "TVEpPathID = -1, PosterPath = '', FanartPath = '', NfoPath = '', ", _
                                                                        "Missing = 1, VideoSource = '' WHERE idEpisode = ", ID, ";")
                                SQLECommand.ExecuteNonQuery()
                            End If
                        End Using
                    End While
                End Using
            End Using
            If Not BatchMode Then
                SQLtransaction.Commit()
                Master.DB.CleanSeasons()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Return False
        End Try
        Return True
    End Function

    Public Function DeleteTVEpFromDBByPath(ByVal sPath As String, ByVal Force As Boolean, Optional ByVal BatchMode As Boolean = False) As Boolean
        Try
            Dim SQLtransaction As SQLite.SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLPCommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLPCommand.CommandText = String.Concat("SELECT ID FROM TVEpPaths WHERE TVEpPath = """, sPath, """;")
                Using SQLPReader As SQLite.SQLiteDataReader = SQLPCommand.ExecuteReader
                    While SQLPReader.Read
                        Using SQLCommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                            SQLCommand.CommandText = String.Concat("SELECT idEpisode, idShow, Season, Missing FROM episode WHERE TVEpPathID = ", SQLPReader("ID"), ";")
                            Using SQLReader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader
                                While SQLReader.Read
                                    Using SQLECommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                        If Not Master.eSettings.TVDisplayMissingEpisodes OrElse Force Then
                                            SQLECommand.CommandText = String.Concat("DELETE FROM episode WHERE idEpisode = ", SQLReader("idEpisode"), ";")
                                            'SQLECommand.ExecuteNonQuery()
                                            'SQLECommand.CommandText = String.Concat("DELETE FROM TVEpActors WHERE TVEpID = ", SQLReader("ID"), ";")
                                            'SQLECommand.ExecuteNonQuery()
                                            'SQLECommand.CommandText = String.Concat("DELETE FROM TVVStreams WHERE TVEpID = ", SQLReader("ID"), ";")
                                            'SQLECommand.ExecuteNonQuery()
                                            'SQLECommand.CommandText = String.Concat("DELETE FROM TVAStreams WHERE TVEpID = ", SQLReader("ID"), ";")
                                            'SQLECommand.ExecuteNonQuery()
                                            'SQLECommand.CommandText = String.Concat("DELETE FROM TVSubs WHERE TVEpID = ", SQLReader("ID"), ";")
                                            'SQLECommand.ExecuteNonQuery()

                                            SQLECommand.CommandText = String.Concat("SELECT idEpisode FROM episode WHERE idShow = ", SQLReader("idShow"), " AND Season = ", SQLReader("Season"), ";")
                                            Using SQLSeasonReader As SQLite.SQLiteDataReader = SQLECommand.ExecuteReader
                                                If Not SQLSeasonReader.HasRows Then
                                                    'no more episodes for this season, delete the season
                                                    Using SQLSeasonCommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                                        SQLSeasonCommand.CommandText = String.Concat("DELETE FROM TVSeason WHERE TVShowID = ", SQLReader("idShow"), " AND Season = ", SQLReader("Season"), ";")
                                                        SQLSeasonCommand.ExecuteNonQuery()
                                                    End Using
                                                End If
                                            End Using
                                        ElseIf Not Convert.ToBoolean(SQLReader("Missing")) Then
                                            SQLECommand.CommandText = String.Concat("UPDATE episode SET Missing = 1, TVEpPathID = -1 WHERE idEpisode = ", SQLReader("idEpisode"), ";")
                                            SQLECommand.ExecuteNonQuery()
                                        End If

                                        SQLECommand.CommandText = String.Concat("DELETE FROM TVEpPaths WHERE ID = ", SQLPReader("idEpisode"), ";")
                                        SQLECommand.ExecuteNonQuery()
                                    End Using
                                End While
                            End Using
                        End Using
                    End While
                End Using
            End Using
            If Not BatchMode Then SQLtransaction.Commit()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Remove all information related to a TV season from the database.
    ''' </summary>
    ''' <param name="ShowID">ID of the tvshow to remove, as stored in the database.</param>
    ''' <param name="BatchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Function DeleteTVSeasonFromDB(ByVal ShowID As Long, ByVal iSeason As Integer, Optional ByVal BatchMode As Boolean = False) As Boolean
        If ShowID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & ShowID)
        If iSeason < 0 Then Throw New ArgumentOutOfRangeException("iSeason", "Value must be >= 0, was given: " & iSeason)

        Try
            Dim SQLtransaction As SQLite.SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT idEpisode FROM episode WHERE idShow = ", ShowID, " AND Season = ", iSeason, ";")
                Using SQLReader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLReader.Read
                        DeleteTVEpFromDB(Convert.ToInt64(SQLReader("idEpisode")), True, False, True)
                    End While
                End Using
                SQLcommand.CommandText = String.Concat("DELETE FROM TVSeason WHERE TVShowID = ", ShowID, " AND Season = ", iSeason, ";")
                SQLcommand.ExecuteNonQuery()
            End Using

            CleanSeasons(True)

            If Not BatchMode Then SQLtransaction.Commit()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Remove all information related to a TV show from the database.
    ''' </summary>
    ''' <param name="ID">ID of the tvshow to remove, as stored in the database.</param>
    ''' <param name="BatchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Function DeleteTVShowFromDB(ByVal ID As Long, Optional ByVal BatchMode As Boolean = False) As Boolean
        Try
            Dim SQLtransaction As SQLite.SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                'SQLcommand.CommandText = String.Concat("SELECT idEpisode FROM episode WHERE idShow = ", ID, ";")
                'Using SQLReader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                '    While SQLReader.Read
                '        DeleteTVEpFromDB(Convert.ToInt64(SQLReader("idEpisode")), True, False, True)
                '    End While
                'End Using
                SQLcommand.CommandText = String.Concat("DELETE FROM tvshow WHERE idShow = ", ID, ";")
                SQLcommand.ExecuteNonQuery()
                'SQLcommand.CommandText = String.Concat("DELETE FROM actorlinktvshow WHERE idShow = ", ID, ";")
                'SQLcommand.ExecuteNonQuery()
                'SQLcommand.CommandText = String.Concat("DELETE FROM TVSeason WHERE TVShowID = ", ID, ";")
                'SQLcommand.ExecuteNonQuery()
            End Using

            CleanSeasons(True)

            If Not BatchMode Then SQLtransaction.Commit()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Fill DataTable with data returned from the provided command
    ''' </summary>
    ''' <param name="dTable">DataTable to fill</param>
    ''' <param name="Command">SQL Command to process</param>
    Public Sub FillDataTable(ByRef dTable As DataTable, ByVal Command As String)
        dTable.Clear()
        Dim sqlDA As New SQLite.SQLiteDataAdapter(Command, _myvideosDBConn)
        Dim sqlCB As New SQLite.SQLiteCommandBuilder(sqlDA)
        sqlDA.Fill(dTable)
    End Sub
    ''' <summary>
    ''' Retrieve a TV Show's information for a particular season.
    ''' </summary>
    ''' <param name="_TVDB">Structure in which to return the information. NOTE: _TVDB.ShowID must be valid.</param>
    ''' <param name="iSeason"></param>
    ''' <remarks></remarks>
    Public Sub FillTVSeasonFromDB(ByRef _TVDB As Structures.DBTV, ByVal iSeason As Integer)
        Dim _tmpTVDB As New Structures.DBTV
        _tmpTVDB = LoadTVSeasonFromDB(_TVDB.ShowID, iSeason, False)

        _TVDB.IsLockSeason = _tmpTVDB.IsLockSeason
        _TVDB.IsMarkSeason = _tmpTVDB.IsMarkSeason
        _TVDB.SeasonBannerPath = _tmpTVDB.SeasonBannerPath
        _TVDB.SeasonFanartPath = _tmpTVDB.SeasonFanartPath
        _TVDB.SeasonLandscapePath = _tmpTVDB.SeasonLandscapePath
        _TVDB.SeasonPosterPath = _tmpTVDB.SeasonPosterPath
    End Sub

    ''' <summary>
    ''' Load all the information for a TV Show.
    ''' </summary>
    ''' <param name="_TVDB">Structures.DBTV container to fill</param>
    Public Sub FillTVShowFromDB(ByRef _TVDB As Structures.DBTV)
        Dim _tmpTVDB As New Structures.DBTV
        _tmpTVDB = LoadTVShowFromDB(_TVDB.ShowID)

        _TVDB.EpisodeSorting = _tmpTVDB.EpisodeSorting
        _TVDB.IsLockShow = _tmpTVDB.IsLockShow
        _TVDB.IsMarkShow = _tmpTVDB.IsMarkShow
        _TVDB.ListTitle = _tmpTVDB.ListTitle
        _TVDB.Ordering = _tmpTVDB.Ordering
        _TVDB.ShowBannerPath = _tmpTVDB.ShowBannerPath
        _TVDB.ShowCharacterArtPath = _tmpTVDB.ShowCharacterArtPath
        _TVDB.ShowClearArtPath = _tmpTVDB.ShowClearArtPath
        _TVDB.ShowClearLogoPath = _tmpTVDB.ShowClearLogoPath
        _TVDB.ShowEFanartsPath = _tmpTVDB.ShowEFanartsPath
        _TVDB.ShowFanartPath = _tmpTVDB.ShowFanartPath
        _TVDB.ShowLandscapePath = _tmpTVDB.ShowLandscapePath
        _TVDB.ShowLanguage = _tmpTVDB.ShowLanguage
        _TVDB.ShowNeedsSave = _tmpTVDB.ShowNeedsSave
        _TVDB.ShowNfoPath = _tmpTVDB.ShowNfoPath
        _TVDB.ShowPath = _tmpTVDB.ShowPath
        _TVDB.ShowPosterPath = _tmpTVDB.ShowPosterPath
        _TVDB.ShowThemePath = _tmpTVDB.ShowThemePath
        _TVDB.Source = _tmpTVDB.Source
        _TVDB.TVShow = _tmpTVDB.TVShow
    End Sub

    Public Function GetTVShowEpisodeSorting(ByVal ShowID As Long) As Enums.EpisodeSorting
        Dim sEpisodeSorting As Enums.EpisodeSorting = Enums.EpisodeSorting.Episode

        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT EpisodeSorting FROM tvshow WHERE idShow = ", ShowID, ";")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    sEpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("EpisodeSorting")), Enums.EpisodeSorting)
                End While
            End Using
        End Using

        Return sEpisodeSorting
    End Function

    Public Function GetMovieCountries() As String()
        Dim cList As New List(Of String)
        Dim mCountry As String

        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT Country FROM movie;"
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    mCountry = SQLreader("Country").ToString
                    If mCountry.Contains("/") Then
                        Dim values As String() = mCountry.Split(New [Char]() {"/"c})
                        For Each country As String In values
                            country = country.Trim
                            If Not cList.Contains(country) Then
                                cList.Add(country)
                            End If
                        Next
                    Else
                        If Not String.IsNullOrEmpty(mCountry) Then
                            If Not cList.Contains(mCountry) Then
                                cList.Add(mCountry.Trim)
                            End If
                        End If
                    End If
                End While
            End Using
        End Using

        cList.Sort()
        Return cList.ToArray
    End Function

    Public Function GetMoviePaths() As List(Of String)
        Dim tList As New List(Of String)
        Dim mPath As String = String.Empty

        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT MoviePath FROM movie;"
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    mPath = SQLreader("MoviePath").ToString.ToLower
                    If Master.eSettings.FileSystemNoStackExts.Contains(Path.GetExtension(mPath)) Then
                        tList.Add(mPath)
                    Else
                        tList.Add(StringUtils.CleanStackingMarkers(mPath))
                    End If
                End While
            End Using
        End Using

        Return tList
    End Function

    Public Function GetTVSourceLanguage(ByVal sName As String) As String
        Dim sLang As String = String.Empty

        Try
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT Language FROM TVSources WHERE Name = """, sName, """;")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        SQLreader.Read()
                        sLang = SQLreader("Language").ToString
                    End If
                End Using
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return sLang
    End Function

    ''' <summary>
    ''' Load all the information for a movie.
    ''' </summary>
    ''' <param name="MovieID">ID of the movie to load, as stored in the database</param>
    ''' <returns>Structures.DBMovie object</returns>
    Public Function LoadMovieFromDB(ByVal MovieID As Long) As Structures.DBMovie
        Dim _movieDB As New Structures.DBMovie

        _movieDB.ID = MovieID
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT idMovie, MoviePath, Type, ListTitle, HasPoster, HasFanart, HasNfo, HasTrailer, HasSub, ", _
                                                   "HasEThumbs, New, Mark, Source, Imdb, Lock, Title, OriginalTitle, Year, Rating, Votes, MPAA, ", _
                                                   "Top250, Country, Outline, Plot, Tagline, Certification, Genre, Studio, Runtime, ReleaseDate, ", _
                                                   "Director, Credits, Playcount, HasWatched, Trailer, PosterPath, FanartPath, EThumbsPath, NfoPath, ", _
                                                   "TrailerPath, SubPath, FanartURL, UseFolder, OutOfTolerance, VideoSource, NeedsSave, SortTitle, ", _
                                                   "DateAdded, HasEFanarts, EFanartsPath, HasBanner, BannerPath, HasLandscape, LandscapePath, HasTheme, ", _
                                                   "ThemePath, HasDiscArt, DiscArtPath, HasClearLogo, ClearLogoPath, HasClearArt, ClearArtPath, TMDB, ", _
                                                   "TMDBColID, DateModified, MarkCustom1, MarkCustom2, MarkCustom3, MarkCustom4, HasSet FROM movie WHERE idMovie = ", _movieDB.ID, ";")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("DateAdded")) Then _movieDB.DateAdded = Convert.ToInt64(SQLreader("DateAdded"))
                    If Not DBNull.Value.Equals(SQLreader("DateModified")) Then _movieDB.DateModified = Convert.ToInt64(SQLreader("DateModified"))
                    If Not DBNull.Value.Equals(SQLreader("ListTitle")) Then _movieDB.ListTitle = SQLreader("ListTitle").ToString
                    If Not DBNull.Value.Equals(SQLreader("MoviePath")) Then _movieDB.Filename = SQLreader("MoviePath").ToString
                    _movieDB.IsSingle = Convert.ToBoolean(SQLreader("type"))
                    If Not DBNull.Value.Equals(SQLreader("FanartPath")) Then _movieDB.FanartPath = SQLreader("FanartPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("PosterPath")) Then _movieDB.PosterPath = SQLreader("PosterPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("TrailerPath")) Then _movieDB.TrailerPath = SQLreader("TrailerPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("NfoPath")) Then _movieDB.NfoPath = SQLreader("NfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("SubPath")) Then _movieDB.SubPath = SQLreader("SubPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("EThumbsPath")) Then _movieDB.EThumbsPath = SQLreader("EThumbsPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("EFanartsPath")) Then _movieDB.EFanartsPath = SQLreader("EFanartsPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("BannerPath")) Then _movieDB.BannerPath = SQLreader("BannerPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("LandscapePath")) Then _movieDB.LandscapePath = SQLreader("LandscapePath").ToString
                    If Not DBNull.Value.Equals(SQLreader("ThemePath")) Then _movieDB.ThemePath = SQLreader("ThemePath").ToString
                    If Not DBNull.Value.Equals(SQLreader("DiscArtPath")) Then _movieDB.DiscArtPath = SQLreader("DiscArtPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("ClearLogoPath")) Then _movieDB.ClearLogoPath = SQLreader("ClearLogoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("ClearArtPath")) Then _movieDB.ClearArtPath = SQLreader("ClearArtPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("Source")) Then _movieDB.Source = SQLreader("Source").ToString
                    _movieDB.IsMark = Convert.ToBoolean(SQLreader("Mark"))
                    _movieDB.IsLock = Convert.ToBoolean(SQLreader("Lock"))
                    _movieDB.UseFolder = Convert.ToBoolean(SQLreader("UseFolder"))
                    _movieDB.OutOfTolerance = Convert.ToBoolean(SQLreader("OutOfTolerance"))
                    _movieDB.NeedsSave = Convert.ToBoolean(SQLreader("NeedsSave"))
                    _movieDB.IsMarkCustom1 = Convert.ToBoolean(SQLreader("MarkCustom1"))
                    _movieDB.IsMarkCustom2 = Convert.ToBoolean(SQLreader("MarkCustom2"))
                    _movieDB.IsMarkCustom3 = Convert.ToBoolean(SQLreader("MarkCustom3"))
                    _movieDB.IsMarkCustom4 = Convert.ToBoolean(SQLreader("MarkCustom4"))
                    If Not DBNull.Value.Equals(SQLreader("VideoSource")) Then _movieDB.VideoSource = SQLreader("VideoSource").ToString
                    _movieDB.Movie = New MediaContainers.Movie
                    With _movieDB.Movie
                        If Not DBNull.Value.Equals(SQLreader("DateAdded")) Then .DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("DateAdded"))).ToString("yyyy-MM-d HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("DateModified")) Then .DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("DateModified"))).ToString("yyyy-MM-d HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("IMDB")) Then .ID = SQLreader("IMDB").ToString
                        If Not DBNull.Value.Equals(SQLreader("Title")) Then .Title = SQLreader("Title").ToString
                        If Not DBNull.Value.Equals(SQLreader("OriginalTitle")) Then .OriginalTitle = SQLreader("OriginalTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("SortTitle")) Then .SortTitle = SQLreader("SortTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("Year")) Then .Year = SQLreader("Year").ToString
                        If Not DBNull.Value.Equals(SQLreader("Rating")) Then .Rating = SQLreader("Rating").ToString
                        If Not DBNull.Value.Equals(SQLreader("Votes")) Then .Votes = SQLreader("Votes").ToString
                        If Not DBNull.Value.Equals(SQLreader("MPAA")) Then .MPAA = SQLreader("MPAA").ToString
                        If Not DBNull.Value.Equals(SQLreader("Top250")) Then .Top250 = SQLreader("Top250").ToString
                        If Not DBNull.Value.Equals(SQLreader("Country")) Then .Country = SQLreader("Country").ToString
                        If Not DBNull.Value.Equals(SQLreader("Outline")) Then .Outline = SQLreader("Outline").ToString
                        If Not DBNull.Value.Equals(SQLreader("Plot")) Then .Plot = SQLreader("Plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("Tagline")) Then .Tagline = SQLreader("Tagline").ToString
                        If Not DBNull.Value.Equals(SQLreader("Trailer")) Then .Trailer = SQLreader("Trailer").ToString
                        If Not DBNull.Value.Equals(SQLreader("Certification")) Then .Certification = SQLreader("Certification").ToString
                        If Not DBNull.Value.Equals(SQLreader("Genre")) Then .Genre = SQLreader("Genre").ToString
                        If Not DBNull.Value.Equals(SQLreader("Runtime")) Then .Runtime = SQLreader("Runtime").ToString
                        If Not DBNull.Value.Equals(SQLreader("ReleaseDate")) Then .ReleaseDate = SQLreader("ReleaseDate").ToString
                        If Not DBNull.Value.Equals(SQLreader("Studio")) Then .Studio = SQLreader("Studio").ToString
                        If Not DBNull.Value.Equals(SQLreader("Director")) Then .Director = SQLreader("Director").ToString
                        If Not DBNull.Value.Equals(SQLreader("Credits")) Then .OldCredits = SQLreader("Credits").ToString
                        If Not DBNull.Value.Equals(SQLreader("PlayCount")) Then .PlayCount = SQLreader("PlayCount").ToString
                        If Not DBNull.Value.Equals(SQLreader("FanartURL")) AndAlso Not Master.eSettings.MovieNoSaveImagesToNfo Then .Fanart.URL = SQLreader("FanartURL").ToString
                        If Not DBNull.Value.Equals(SQLreader("VideoSource")) Then .VideoSource = SQLreader("VideoSource").ToString
                        If Not DBNull.Value.Equals(SQLreader("TMDB")) Then .TMDBID = SQLreader("TMDB").ToString
                        If Not DBNull.Value.Equals(SQLreader("TMDBColID")) Then .TMDBColID = SQLreader("TMDBColID").ToString
                    End With
                End If
            End Using
        End Using

        'Actors
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT A.strRole, B.strActor, B.strThumb FROM actorlinkmovie AS A ", _
                        "INNER JOIN actors AS B ON (A.idActor = B.idActor) WHERE A.idMovie = ", _movieDB.ID, " ORDER BY A.iOrder;")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim person As MediaContainers.Person
                While SQLreader.Read
                    person = New MediaContainers.Person
                    person.Name = SQLreader("strActor").ToString
                    person.Role = SQLreader("strRole").ToString
                    person.Thumb = SQLreader("strThumb").ToString
                    _movieDB.Movie.Actors.Add(person)
                End While
            End Using
        End Using

        'Video streams
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT MovieID, StreamID, Video_Width, Video_Height, Video_Codec, Video_Duration, Video_ScanType, ", _
                                                   "Video_AspectDisplayRatio, Video_Language, Video_LongLanguage, Video_Bitrate, Video_MultiViewCount, ", _
                                                   "Video_EncodedSettings, Video_MultiViewLayout, Video_StereoMode FROM MoviesVStreams WHERE MovieID = ", _movieDB.ID, ";")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim video As MediaInfo.Video
                While SQLreader.Read
                    video = New MediaInfo.Video
                    If Not DBNull.Value.Equals(SQLreader("Video_Width")) Then video.Width = SQLreader("Video_Width").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_Height")) Then video.Height = SQLreader("Video_Height").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_Codec")) Then video.Codec = SQLreader("Video_Codec").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_Duration")) Then video.Duration = SQLreader("Video_Duration").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_ScanType")) Then video.Scantype = SQLreader("Video_ScanType").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_AspectDisplayRatio")) Then video.Aspect = SQLreader("Video_AspectDisplayRatio").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_Language")) Then video.Language = SQLreader("Video_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_LongLanguage")) Then video.LongLanguage = SQLreader("Video_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_Bitrate")) Then video.Bitrate = SQLreader("Video_Bitrate").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_MultiViewCount")) Then video.MultiViewCount = SQLreader("Video_MultiViewCount").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_EncodedSettings")) Then video.EncodedSettings = SQLreader("Video_EncodedSettings").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_MultiViewLayout")) Then video.MultiViewLayout = SQLreader("Video_MultiViewLayout").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_StereoMode")) Then video.StereoMode = SQLreader("Video_StereoMode").ToString
                    _movieDB.Movie.FileInfo.StreamDetails.Video.Add(video)
                End While
            End Using
        End Using

        'Audio streams
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT MovieID, StreamID, Audio_Language, Audio_LongLanguage, Audio_Codec, Audio_Channel, Audio_Bitrate FROM MoviesAStreams WHERE MovieID = ", _movieDB.ID, ";")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim audio As MediaInfo.Audio
                While SQLreader.Read
                    audio = New MediaInfo.Audio
                    If Not DBNull.Value.Equals(SQLreader("Audio_Language")) Then audio.Language = SQLreader("Audio_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_LongLanguage")) Then audio.LongLanguage = SQLreader("Audio_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Codec")) Then audio.Codec = SQLreader("Audio_Codec").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Channel")) Then audio.Channels = SQLreader("Audio_Channel").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Bitrate")) Then audio.Bitrate = SQLreader("Audio_Bitrate").ToString
                    _movieDB.Movie.FileInfo.StreamDetails.Audio.Add(audio)
                End While
            End Using
        End Using

        'embedded subtitles
        _movieDB.Subtitles = New List(Of MediaInfo.Subtitle)
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT MovieID, StreamID, Subs_Language, Subs_LongLanguage, Subs_Type, Subs_Path, Subs_Forced FROM MoviesSubs WHERE MovieID = ", _movieDB.ID, " AND NOT Subs_Type = 'External';")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaInfo.Subtitle
                While SQLreader.Read
                    subtitle = New MediaInfo.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.SubsType = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.SubsPath = SQLreader("Subs_Path").ToString
                    subtitle.SubsForced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    _movieDB.Movie.FileInfo.StreamDetails.Subtitle.Add(subtitle)
                End While
            End Using
        End Using

        'external subtitles
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT MovieID, StreamID, Subs_Language, Subs_LongLanguage, Subs_Type, Subs_Path, Subs_Forced FROM MoviesSubs WHERE MovieID = ", _movieDB.ID, " AND Subs_Type = 'External';")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaInfo.Subtitle
                While SQLreader.Read
                    subtitle = New MediaInfo.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.SubsType = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.SubsPath = SQLreader("Subs_Path").ToString
                    subtitle.SubsForced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    _movieDB.Subtitles.Add(subtitle)
                End While
            End Using
        End Using

        'MovieSets
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT A.MovieID, A.SetID, A.SetOrder, B.idSet, B.SetName FROM MoviesSets ", _
                                                   "AS A INNER JOIN sets AS B ON (A.SetID = B.idSet) WHERE A.MovieID = ", _movieDB.ID, ";")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim sets As MediaContainers.Set
                While SQLreader.Read
                    sets = New MediaContainers.Set
                    If Not DBNull.Value.Equals(SQLreader("SetID")) Then sets.ID = Convert.ToInt64(SQLreader("SetID"))
                    If Not DBNull.Value.Equals(SQLreader("SetOrder")) Then sets.Order = SQLreader("SetOrder").ToString
                    If Not DBNull.Value.Equals(SQLreader("SetName")) Then sets.Title = SQLreader("SetName").ToString
                    _movieDB.Movie.Sets.Add(sets)
                End While
            End Using
        End Using

        'Tags
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strTag FROM taglinks ", _
                                                   "AS A INNER JOIN tag AS B ON (A.idTag = B.idTag) WHERE A.idMedia = ", _movieDB.ID, " AND A.media_type = 'movie';")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim tag As String
                While SQLreader.Read
                    tag = String.Empty
                    If Not DBNull.Value.Equals(SQLreader("strTag")) Then tag = SQLreader("strTag").ToString
                    _movieDB.Movie.Tags.Add(tag)
                End While
            End Using
        End Using

        'NFO image links
        If Not Master.eSettings.MovieNoSaveImagesToNfo Then
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT ID, MovieID, preview, thumbs FROM MoviesFanart WHERE MovieID = ", _movieDB.ID, ";")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    Dim thumb As MediaContainers.Thumb
                    While SQLreader.Read
                        thumb = New MediaContainers.Thumb
                        If Not DBNull.Value.Equals(SQLreader("preview")) Then thumb.Preview = SQLreader("preview").ToString
                        If Not DBNull.Value.Equals(SQLreader("thumbs")) Then thumb.Text = SQLreader("thumbs").ToString
                        _movieDB.Movie.Fanart.Thumb.Add(thumb)
                    End While
                End Using
            End Using
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT ID, MovieID, thumbs FROM MoviesPosters WHERE MovieID = ", _movieDB.ID, ";")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        If Not DBNull.Value.Equals(SQLreader("thumbs")) Then _movieDB.Movie.Thumb.Add(SQLreader("thumbs").ToString)
                    End While
                End Using
            End Using
        End If

        Return _movieDB
    End Function

    ''' <summary>
    ''' Load all the information for a movie (by movie path)
    ''' </summary>
    ''' <param name="sPath">Full path to the movie file</param>
    ''' <returns>Structures.DBMovie object</returns>
    Public Function LoadMovieFromDB(ByVal sPath As String) As Structures.DBMovie
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            ' One more Query Better then re-write all function again
            SQLcommand.CommandText = String.Concat("SELECT idMovie FROM movie WHERE MoviePath = ", sPath, ";")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.Read Then
                    Return LoadMovieFromDB(Convert.ToInt64(SQLreader("idMovie")))
                Else
                    Return New Structures.DBMovie With {.Id = -1} ' No Movie Found
                End If
            End Using
        End Using

        Return New Structures.DBMovie With {.Id = -1}
    End Function

    ''' <summary>
    ''' Load all the information for a movieset.
    ''' </summary>
    ''' <param name="MovieSetID">ID of the movieset to load, as stored in the database</param>
    ''' <returns>Structures.DBMovie object</returns>
    Public Function LoadMovieSetFromDB(ByVal MovieSetID As Long) As Structures.DBMovieSet
        Dim _moviesetDB As New Structures.DBMovieSet

        Try
            _moviesetDB.ID = MovieSetID
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT idSet, ListTitle, HasNfo, NfoPath, HasPoster, PosterPath, HasFanart, ", _
                                                       "FanartPath, HasBanner, BannerPath, HasLandscape, LandscapePath, ", _
                                                       "HasDiscArt, DiscArtPath, HasClearLogo, ClearLogoPath, HasClearArt, ", _
                                                       "ClearArtPath, TMDBColID, Plot, SetName, New, Mark, Lock FROM sets WHERE idSet = ", MovieSetID, ";")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        SQLreader.Read()
                        If Not DBNull.Value.Equals(SQLreader("ListTitle")) Then _moviesetDB.ListTitle = SQLreader("ListTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("NfoPath")) Then _moviesetDB.NfoPath = SQLreader("NfoPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("PosterPath")) Then _moviesetDB.PosterPath = SQLreader("PosterPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("FanartPath")) Then _moviesetDB.FanartPath = SQLreader("FanartPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("BannerPath")) Then _moviesetDB.BannerPath = SQLreader("BannerPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("LandscapePath")) Then _moviesetDB.LandscapePath = SQLreader("LandscapePath").ToString
                        If Not DBNull.Value.Equals(SQLreader("DiscArtPath")) Then _moviesetDB.DiscArtPath = SQLreader("DiscArtPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("ClearLogoPath")) Then _moviesetDB.ClearLogoPath = SQLreader("ClearLogoPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("ClearArtPath")) Then _moviesetDB.ClearArtPath = SQLreader("ClearArtPath").ToString
                        _moviesetDB.IsMark = Convert.ToBoolean(SQLreader("Mark"))
                        _moviesetDB.IsLock = Convert.ToBoolean(SQLreader("Lock"))
                        _moviesetDB.MovieSet = New MediaContainers.MovieSet
                        With _moviesetDB.MovieSet
                            If Not DBNull.Value.Equals(SQLreader("TMDBColID")) Then .ID = SQLreader("TMDBColID").ToString
                            If Not DBNull.Value.Equals(SQLreader("Plot")) Then .Plot = SQLreader("Plot").ToString
                            If Not DBNull.Value.Equals(SQLreader("SetName")) Then .Title = SQLreader("SetName").ToString
                        End With
                    End If
                End Using
            End Using

            _moviesetDB.Movies = New List(Of Structures.DBMovie)
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT MovieID, SetID , SetOrder FROM MoviesSets ", _
                            "WHERE SetID = ", _moviesetDB.ID, " ORDER BY SetOrder;")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    Dim movie As Structures.DBMovie
                    While SQLreader.Read
                        movie = New Structures.DBMovie
                        movie = LoadMovieFromDB(Convert.ToInt64(SQLreader("MovieID")))
                        _moviesetDB.Movies.Add(movie)
                    End While
                End Using
            End Using

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return _moviesetDB
    End Function

    ''' <summary>
    ''' Get the posterpath for the all seasons entry.
    ''' </summary>
    ''' <param name="ShowID">ID of the show to load, as stored in the database</param>
    ''' <param name="WithShow">If <c>True</c>, also retrieve base show information</param>
    ''' <returns>Structures.DBTV object</returns>
    Public Function LoadTVAllSeasonFromDB(ByVal ShowID As Long, Optional ByVal WithShow As Boolean = False) As Structures.DBTV
        'TODO This method seems mis-named, and may not be performing its intended role
        'DanCooper: Works correctly. AllSeasonPosters don't have existing episodes, so it needs to be fetched manually from DB.
        If ShowID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & ShowID)

        Dim _TVDB As New Structures.DBTV
        Try
            _TVDB.ShowID = ShowID
            _TVDB.TVEp = New MediaContainers.EpisodeDetails With {.Season = 999}

            Using SQLcommandTVSeason As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommandTVSeason.CommandText = String.Concat("SELECT TVShowID, SeasonText, Season, HasPoster, HasFanart, PosterPath, FanartPath, Lock, Mark, ", _
                                                               "New, HasBanner, BannerPath, HasLandscape, LandscapePath FROM TVSeason WHERE TVShowID = ", ShowID, " AND Season = 999;")
                Using SQLReader As SQLite.SQLiteDataReader = SQLcommandTVSeason.ExecuteReader
                    If SQLReader.HasRows Then
                        SQLReader.Read()
                        If Not DBNull.Value.Equals(SQLReader("BannerPath")) Then _TVDB.SeasonBannerPath = SQLReader("BannerPath").ToString
                        If Not DBNull.Value.Equals(SQLReader("FanartPath")) Then _TVDB.SeasonFanartPath = SQLReader("FanartPath").ToString
                        If Not DBNull.Value.Equals(SQLReader("LandscapePath")) Then _TVDB.SeasonLandscapePath = SQLReader("LandscapePath").ToString
                        If Not DBNull.Value.Equals(SQLReader("PosterPath")) Then _TVDB.SeasonPosterPath = SQLReader("PosterPath").ToString
                    End If
                End Using
            End Using

            If WithShow Then Master.DB.FillTVShowFromDB(_TVDB)

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return _TVDB
    End Function

    ''' <summary>
    ''' Load all the information for a TV Episode.
    ''' </summary>
    ''' <param name="EpID">ID of the episode to load, as stored in the database</param>
    ''' <returns>Structures.DBTV object</returns>
    Public Function LoadTVEpFromDB(ByVal EpID As Long, ByVal WithShow As Boolean) As Structures.DBTV
        Dim _TVDB As New Structures.DBTV
        Dim PathID As Long = -1

        _TVDB.EpID = EpID
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT idEpisode, idShow, Episode, Title, HasPoster, HasFanart, HasNfo, New, Mark, TVEpPathID, Source, Lock, ", _
                                                   "Season, Rating, Plot, Aired, Director, Credits, PosterPath, FanartPath, NfoPath, NeedsSave, Missing, Playcount, ", _
                                                   "HasWatched, DisplaySeason, DisplayEpisode, DateAdded, Runtime, Votes, VideoSource, HasSub FROM episode WHERE idEpisode = ", EpID, ";")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("PosterPath")) Then _TVDB.EpPosterPath = SQLreader("PosterPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("FanartPath")) Then _TVDB.EpFanartPath = SQLreader("FanartPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("NfoPath")) Then _TVDB.EpNfoPath = SQLreader("NfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("Source")) Then _TVDB.Source = SQLreader("Source").ToString
                    If Not DBNull.Value.Equals(SQLreader("idShow")) Then _TVDB.ShowID = Convert.ToInt64(SQLreader("idShow"))
                    If Not DBNull.Value.Equals(SQLreader("DateAdded")) Then _TVDB.DateAdded = Convert.ToInt64(SQLreader("DateAdded"))
                    If Not DBNull.Value.Equals(SQLreader("VideoSource")) Then _TVDB.VideoSource = SQLreader("VideoSource").ToString
                    PathID = Convert.ToInt64(SQLreader("TVEpPathid"))
                    _TVDB.FilenameID = PathID
                    _TVDB.IsMarkEp = Convert.ToBoolean(SQLreader("Mark"))
                    _TVDB.IsLockEp = Convert.ToBoolean(SQLreader("Lock"))
                    _TVDB.EpNeedsSave = Convert.ToBoolean(SQLreader("NeedsSave"))
                    _TVDB.TVEp = New MediaContainers.EpisodeDetails
                    With _TVDB.TVEp
                        ' add display season and episode - mh
                        If Not DBNull.Value.Equals(SQLreader("DisplaySeason")) Then
                            .DisplaySeason = Convert.ToInt32(SQLreader("Season"))
                            .displaySEset = True
                        End If
                        If Not DBNull.Value.Equals(SQLreader("DisplayEpisode")) Then
                            .DisplayEpisode = Convert.ToInt32(SQLreader("Episode"))
                            .displaySEset = True
                        End If
                        If Not DBNull.Value.Equals(SQLreader("Title")) Then .Title = SQLreader("Title").ToString
                        If Not DBNull.Value.Equals(SQLreader("Season")) Then .Season = Convert.ToInt32(SQLreader("Season"))
                        If Not DBNull.Value.Equals(SQLreader("Episode")) Then .Episode = Convert.ToInt32(SQLreader("Episode"))
                        If Not DBNull.Value.Equals(SQLreader("Aired")) Then .Aired = SQLreader("Aired").ToString
                        If Not DBNull.Value.Equals(SQLreader("Rating")) Then .Rating = SQLreader("Rating").ToString
                        If Not DBNull.Value.Equals(SQLreader("Plot")) Then .Plot = SQLreader("Plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("Director")) Then .Director = SQLreader("Director").ToString
                        If Not DBNull.Value.Equals(SQLreader("Credits")) Then .OldCredits = SQLreader("Credits").ToString
                        If Not DBNull.Value.Equals(SQLreader("Playcount")) Then .Playcount = SQLreader("Playcount").ToString
                        If Not DBNull.Value.Equals(SQLreader("DateAdded")) Then .DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("DateAdded"))).ToString("yyyy-MM-d HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("Runtime")) Then .Runtime = SQLreader("Runtime").ToString
                        If Not DBNull.Value.Equals(SQLreader("Votes")) Then .Votes = SQLreader("Votes").ToString
                        If Not DBNull.Value.Equals(SQLreader("VideoSource")) Then .VideoSource = SQLreader("VideoSource").ToString
                    End With
                End If
            End Using
        End Using

        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT TVEpPath FROM TVEpPaths WHERE ID = ", PathID, ";")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("TVEpPath")) Then _TVDB.Filename = SQLreader("TVEpPath").ToString
                End If
            End Using
        End Using

        'Actors
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT A.strRole, B.strActor, B.strThumb FROM actorlinkepisode AS A ", _
                        "INNER JOIN actors AS B ON (A.idActor = B.idActor) WHERE A.idEpisode = ", _TVDB.EpID, " ORDER BY A.iOrder;")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim person As MediaContainers.Person
                While SQLreader.Read
                    person = New MediaContainers.Person
                    person.Name = SQLreader("strActor").ToString
                    person.Role = SQLreader("strRole").ToString
                    person.Thumb = SQLreader("strThumb").ToString
                    _TVDB.TVEp.Actors.Add(person)
                End While
            End Using
        End Using

        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT TVEpID, StreamID, Video_Width, Video_Height, Video_Codec, Video_Duration, Video_ScanType, ", _
                                                   "Video_AspectDisplayRatio, Video_Language, Video_LongLanguage, Video_Bitrate, Video_MultiViewCount, ", _
                                                   "Video_EncodedSettings, Video_MultiViewLayout, Video_StereoMode FROM TVVStreams WHERE TVEpID = ", _TVDB.EpID, ";")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim video As MediaInfo.Video
                While SQLreader.Read
                    video = New MediaInfo.Video
                    If Not DBNull.Value.Equals(SQLreader("Video_Width")) Then video.Width = SQLreader("Video_Width").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_Height")) Then video.Height = SQLreader("Video_Height").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_Codec")) Then video.Codec = SQLreader("Video_Codec").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_Duration")) Then video.Duration = SQLreader("Video_Duration").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_ScanType")) Then video.Scantype = SQLreader("Video_ScanType").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_AspectDisplayRatio")) Then video.Aspect = SQLreader("Video_AspectDisplayRatio").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_Language")) Then video.Language = SQLreader("Video_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_LongLanguage")) Then video.LongLanguage = SQLreader("Video_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_Bitrate")) Then video.Bitrate = SQLreader("Video_Bitrate").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_MultiViewCount")) Then video.MultiViewCount = SQLreader("Video_MultiViewCount").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_EncodedSettings")) Then video.EncodedSettings = SQLreader("Video_EncodedSettings").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_MultiViewLayout")) Then video.MultiViewLayout = SQLreader("Video_MultiViewLayout").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_StereoMode")) Then video.StereoMode = SQLreader("Video_StereoMode").ToString
                    _TVDB.TVEp.FileInfo.StreamDetails.Video.Add(video)
                End While
            End Using
        End Using

        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT TVEpID, StreamID, Audio_Language, Audio_LongLanguage, Audio_Codec, Audio_Channel, Audio_Bitrate FROM TVAStreams WHERE TVEpID = ", _TVDB.EpID, ";")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim audio As MediaInfo.Audio
                While SQLreader.Read
                    audio = New MediaInfo.Audio
                    If Not DBNull.Value.Equals(SQLreader("Audio_Language")) Then audio.Language = SQLreader("Audio_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_LongLanguage")) Then audio.LongLanguage = SQLreader("Audio_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Codec")) Then audio.Codec = SQLreader("Audio_Codec").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Channel")) Then audio.Channels = SQLreader("Audio_Channel").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Bitrate")) Then audio.Bitrate = SQLreader("Audio_Bitrate").ToString
                    _TVDB.TVEp.FileInfo.StreamDetails.Audio.Add(audio)
                End While
            End Using
        End Using

        'embedded subtitles
        _TVDB.EpSubtitles = New List(Of MediaInfo.Subtitle)
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT TVEpID, StreamID, Subs_Language, Subs_LongLanguage, Subs_Type, Subs_Path, Subs_Forced FROM TVSubs WHERE TVEpID = ", _TVDB.EpID, " AND NOT Subs_Type = 'External';")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaInfo.Subtitle
                While SQLreader.Read
                    subtitle = New MediaInfo.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.SubsType = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.SubsPath = SQLreader("Subs_Path").ToString
                    subtitle.SubsForced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    _TVDB.TVEp.FileInfo.StreamDetails.Subtitle.Add(subtitle)
                End While
            End Using
        End Using

        'external subtitles
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT TVEpID, StreamID, Subs_Language, Subs_LongLanguage, Subs_Type, Subs_Path, Subs_Forced FROM TVSubs WHERE TVEpID = ", _TVDB.EpID, " AND Subs_Type = 'External';")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaInfo.Subtitle
                While SQLreader.Read
                    subtitle = New MediaInfo.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.SubsType = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.SubsPath = SQLreader("Subs_Path").ToString
                    subtitle.SubsForced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    _TVDB.EpSubtitles.Add(subtitle)
                End While
            End Using
        End Using

        If _TVDB.ShowID > -1 AndAlso WithShow Then
            FillTVShowFromDB(_TVDB)
            FillTVSeasonFromDB(_TVDB, _TVDB.TVEp.Season)
        End If

        Return _TVDB
    End Function
    ''' <summary>
    ''' Load all the information for a TV Episode (by episode path)
    ''' </summary>
    ''' <param name="sPath">Full path to the episode file</param>
    ''' <returns>Structures.DBTV object</returns>
    Public Function LoadTVEpFromDB(ByVal sPath As String, ByVal WithShow As Boolean) As Structures.DBTV
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            ' One more Query Better then re-write all function again
            SQLcommand.CommandText = String.Concat("SELECT ID FROM TVEpPaths WHERE TVEpPath = ", sPath, ";")
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.Read Then
                    Return LoadTVEpFromDB(Convert.ToInt64(SQLreader("ID")), WithShow)
                Else
                    Return New Structures.DBTV With {.EpID = -1} ' No Movie Found
                End If
            End Using
        End Using

        Return New Structures.DBTV With {.EpID = -1}
    End Function
    ''' <summary>
    ''' Retrieve details for the given ShowID. If AllSeasonPosterEnabled is <c>True</c>, also retrieve the AllSeasonsPoster
    ''' </summary>
    ''' <param name="ShowID">Show ID to retrieve</param>
    ''' <returns>Structures.DBDV filled with show information.</returns>
    ''' <remarks></remarks>
    Public Function LoadTVFullShowFromDB(ByVal ShowID As Long) As Structures.DBTV
        If ShowID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & ShowID)

        If Master.eSettings.TVASPosterAnyEnabled Then
            Return Master.DB.LoadTVAllSeasonFromDB(ShowID, True)
        Else
            Return Master.DB.LoadTVShowFromDB(ShowID)
        End If
    End Function
    ''' <summary>
    ''' Load all the information for a TV Season.
    ''' </summary>
    ''' <param name="ShowID">ID of the show to load, as stored in the database</param>
    ''' <param name="iSeason">Number of the season to load, as stored in the database</param>
    ''' <param name="WithShow">If <c>True</c>, also retrieve the TV Show information</param>
    ''' <returns>Structures.DBTV object</returns>
    ''' <remarks></remarks>
    Public Function LoadTVSeasonFromDB(ByVal ShowID As Long, ByVal iSeason As Integer, ByVal WithShow As Boolean) As Structures.DBTV
        Dim _TVDB As New Structures.DBTV

        If ShowID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & ShowID)

        Try
            _TVDB.ShowID = ShowID
            If WithShow Then FillTVShowFromDB(_TVDB)
            _TVDB.TVEp = New MediaContainers.EpisodeDetails
            _TVDB.TVEp.Season = iSeason

            Using SQLcommandTVSeason As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommandTVSeason.CommandText = String.Concat("SELECT TVShowID, SeasonText, Season, HasPoster, HasFanart, PosterPath, FanartPath, Lock , Mark , New, HasBanner, BannerPath, HasLandscape, LandscapePath FROM TVSeason WHERE TVShowID = ", ShowID, " AND Season = ", iSeason, ";")
                Using SQLReader As SQLite.SQLiteDataReader = SQLcommandTVSeason.ExecuteReader
                    If SQLReader.HasRows Then
                        SQLReader.Read()
                        If Not DBNull.Value.Equals(SQLReader("BannerPath")) Then _TVDB.SeasonBannerPath = SQLReader("BannerPath").ToString
                        If Not DBNull.Value.Equals(SQLReader("FanartPath")) Then _TVDB.SeasonFanartPath = SQLReader("FanartPath").ToString
                        If Not DBNull.Value.Equals(SQLReader("LandscapePath")) Then _TVDB.SeasonLandscapePath = SQLReader("LandscapePath").ToString
                        If Not DBNull.Value.Equals(SQLReader("PosterPath")) Then _TVDB.SeasonPosterPath = SQLReader("PosterPath").ToString
                        _TVDB.IsLockSeason = Convert.ToBoolean(SQLReader("Lock"))
                        _TVDB.IsMarkSeason = Convert.ToBoolean(SQLReader("Mark"))
                    End If
                End Using
            End Using

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return _TVDB
    End Function
    ''' <summary>
    ''' Load all the information for a TV Show.
    ''' </summary>
    ''' <param name="ShowID">ID of the show to load, as stored in the database</param>
    ''' <returns>Structures.DBTV object</returns>
    Public Function LoadTVShowFromDB(ByVal ShowID As Long) As Structures.DBTV
        Dim _TVDB As New Structures.DBTV

        If ShowID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & ShowID)

        Try
            _TVDB.ShowID = ShowID
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT idShow, ListTitle, HasPoster, HasFanart, HasNfo, New, Mark, TVShowPath, Source, TVDB, Lock, EpisodeGuide, ", _
                                                       "Plot, Genre, Premiered, Studio, MPAA, Rating, PosterPath, FanartPath, NfoPath, NeedsSave, Language, Ordering, ", _
                                                       "HasBanner, BannerPath, HasLandscape, LandscapePath, Status, HasTheme, ThemePath, HasCharacterArt, CharacterArtPath, ", _
                                                       "HasClearLogo, ClearLogoPath, HasClearArt, ClearArtPath, HasEFanarts, EFanartsPath, Runtime, Title, Votes, EpisodeSorting ", _
                                                       "FROM tvshow WHERE idShow = ", ShowID, ";")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        SQLreader.Read()
                        If Not DBNull.Value.Equals(SQLreader("ListTitle")) Then _TVDB.ListTitle = SQLreader("ListTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("CharacterArtPath")) Then _TVDB.ShowCharacterArtPath = SQLreader("CharacterArtPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("ClearArtPath")) Then _TVDB.ShowClearArtPath = SQLreader("ClearArtPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("ClearLogoPath")) Then _TVDB.ShowClearLogoPath = SQLreader("ClearLogoPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("BannerPath")) Then _TVDB.ShowBannerPath = SQLreader("BannerPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("EFanartsPath")) Then _TVDB.ShowEFanartsPath = SQLreader("EFanartsPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("FanartPath")) Then _TVDB.ShowFanartPath = SQLreader("FanartPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("LandscapePath")) Then _TVDB.ShowLandscapePath = SQLreader("LandscapePath").ToString
                        If Not DBNull.Value.Equals(SQLreader("Language")) Then _TVDB.ShowLanguage = SQLreader("Language").ToString
                        If Not DBNull.Value.Equals(SQLreader("NfoPath")) Then _TVDB.ShowNfoPath = SQLreader("NfoPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("PosterPath")) Then _TVDB.ShowPosterPath = SQLreader("PosterPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("Source")) Then _TVDB.Source = SQLreader("Source").ToString
                        If Not DBNull.Value.Equals(SQLreader("TVShowPath")) Then _TVDB.ShowPath = SQLreader("TVShowPath").ToString
                        If Not DBNull.Value.Equals(SQLreader("ThemePath")) Then _TVDB.ShowThemePath = SQLreader("ThemePath").ToString
                        _TVDB.IsMarkShow = Convert.ToBoolean(SQLreader("Mark"))
                        _TVDB.IsLockShow = Convert.ToBoolean(SQLreader("Lock"))
                        _TVDB.ShowNeedsSave = Convert.ToBoolean(SQLreader("NeedsSave"))
                        _TVDB.Ordering = DirectCast(Convert.ToInt32(SQLreader("Ordering")), Enums.Ordering)
                        _TVDB.EpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("EpisodeSorting")), Enums.EpisodeSorting)
                        _TVDB.TVShow = New MediaContainers.TVShow
                        With _TVDB.TVShow
                            If Not DBNull.Value.Equals(SQLreader("Title")) Then .Title = SQLreader("Title").ToString
                            If Not DBNull.Value.Equals(SQLreader("TVDB")) Then .ID = SQLreader("TVDB").ToString
                            If Not DBNull.Value.Equals(SQLreader("EpisodeGuide")) Then .EpisodeGuide.URL = SQLreader("EpisodeGuide").ToString
                            If Not DBNull.Value.Equals(SQLreader("Plot")) Then .Plot = SQLreader("Plot").ToString
                            If Not DBNull.Value.Equals(SQLreader("Genre")) Then .Genre = SQLreader("Genre").ToString
                            If Not DBNull.Value.Equals(SQLreader("Premiered")) Then .Premiered = SQLreader("Premiered").ToString
                            If Not DBNull.Value.Equals(SQLreader("Studio")) Then .Studio = SQLreader("Studio").ToString
                            If Not DBNull.Value.Equals(SQLreader("MPAA")) Then .MPAA = SQLreader("MPAA").ToString
                            If Not DBNull.Value.Equals(SQLreader("Rating")) Then .Rating = SQLreader("Rating").ToString
                            If Not DBNull.Value.Equals(SQLreader("Status")) Then .Status = SQLreader("Status").ToString
                            If Not DBNull.Value.Equals(SQLreader("Runtime")) Then .Runtime = SQLreader("Runtime").ToString
                            If Not DBNull.Value.Equals(SQLreader("Votes")) Then .Votes = SQLreader("Votes").ToString
                        End With
                    End If
                End Using
            End Using

            'Actors
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT A.strRole, B.strActor, B.strThumb FROM actorlinktvshow AS A ", _
                            "INNER JOIN actors AS B ON (A.idActor = B.idActor) WHERE A.idShow = ", _TVDB.ShowID, " ORDER BY A.iOrder;")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    Dim person As MediaContainers.Person
                    While SQLreader.Read
                        person = New MediaContainers.Person
                        person.Name = SQLreader("strActor").ToString
                        person.Role = SQLreader("strRole").ToString
                        person.Thumb = SQLreader("strThumb").ToString
                        _TVDB.TVShow.Actors.Add(person)
                    End While
                End Using
            End Using

            'Tags
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT B.strTag FROM taglinks ", _
                                                       "AS A INNER JOIN tag AS B ON (A.idTag = B.idTag) WHERE A.idMedia = ", _TVDB.ShowID, " AND A.media_type = 'tvshow';")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    Dim tag As String
                    While SQLreader.Read
                        tag = String.Empty
                        If Not DBNull.Value.Equals(SQLreader("strTag")) Then tag = SQLreader("strTag").ToString
                        _TVDB.TVShow.Tags.Add(tag)
                    End While
                End Using
            End Using

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            _TVDB.ShowID = -1
        End Try
        Return _TVDB
    End Function
    ''' <summary>
    ''' Execute arbitrary SQL commands against the database. Commands are retrieved from fname. 
    ''' Commands are serialized Containers.InstallCommands. Only commands marked as CommandType DB are executed.
    ''' </summary>
    ''' <param name="cPath">path to current DB</param>
    ''' <param name="nPath">path for new DB</param>
    ''' <param name="cVersion">current version of DB to patch</param>
    ''' <param name="nVersion">lastest version of DB</param>
    ''' <remarks></remarks>
    Public Sub PatchDatabase(ByVal cPath As String, ByVal nPath As String, ByVal cVersion As Integer, ByVal nVersion As Integer)
        Dim xmlSer As XmlSerializer
        Dim _cmds As New Containers.InstallCommands
        Dim TransOk As Boolean
        Dim tempName As String = String.Empty

        tempName = String.Concat(nPath, "_tmp")
        If File.Exists(tempName) Then
            File.Delete(tempName)
        End If
        File.Copy(cPath, tempName)

        Try
            _myvideosDBConn = New SQLiteConnection(String.Format(_connStringTemplate, tempName))
            _myvideosDBConn.Open()

            For i As Integer = cVersion To nVersion - 1

                Dim patchpath As String = FileUtils.Common.ReturnSettingsFile("DB", String.Format("MyVideosDBSQL_v{0}_Patch.xml", i))

                xmlSer = New XmlSerializer(GetType(Containers.InstallCommands))
                Using xmlSW As New StreamReader(Path.Combine(Functions.AppPath, patchpath))
                    _cmds = DirectCast(xmlSer.Deserialize(xmlSW), Containers.InstallCommands)
                End Using

                For Each Trans In _cmds.transaction
                    TransOk = True
                    Using SQLtransaction As SQLite.SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                        For Each _cmd As Containers.CommandsTransactionCommand In Trans.command
                            If _cmd.type = "DB" Then
                                Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    SQLcommand.CommandText = _cmd.execute
                                    Try
                                        SQLcommand.ExecuteNonQuery()
                                    Catch ex As Exception
                                        logger.Info(New StackFrame().GetMethod().Name, ex, Trans.name, _cmd.execute)
                                        TransOk = False
                                        Exit For
                                    End Try
                                End Using
                            End If
                        Next
                        If TransOk Then
                            logger.Trace(New StackFrame().GetMethod().Name, String.Format("Transaction {0} Commit", Trans.name))
                            SQLtransaction.Commit()
                            ' Housekeeping - consolidate and pack database using vacuum command http://www.sqlite.org/lang_vacuum.html
                            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                SQLcommand.CommandText = "VACUUM;"
                                SQLcommand.ExecuteNonQuery()
                            End Using
                        Else
                            logger.Trace(New StackFrame().GetMethod().Name, String.Format("Transaction {0} RollBack", Trans.name))
                            SQLtransaction.Rollback()
                        End If
                    End Using
                Next
                For Each _cmd As Containers.CommandsNoTransactionCommand In _cmds.noTransaction
                    If _cmd.type = "DB" Then
                        Using SQLnotransaction As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                            SQLnotransaction.CommandText = _cmd.execute
                            Try
                                SQLnotransaction.ExecuteNonQuery()
                                ' Housekeeping - consolidate and pack database using vacuum command http://www.sqlite.org/lang_vacuum.html
                                Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    SQLcommand.CommandText = "VACUUM;"
                                    SQLcommand.ExecuteNonQuery()
                                End Using
                            Catch ex As Exception
                                logger.Info(New StackFrame().GetMethod().Name, ex, SQLnotransaction, _cmd.description, _cmd.execute)
                            End Try
                        End Using
                    End If
                Next

            Next
            _myvideosDBConn.Close()
            File.Move(tempName, nPath)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & vbTab & "Unable to open media database connection.", ex)
            _myvideosDBConn.Close()
        End Try
    End Sub

    '  Public Function CheckEssentials() As Boolean
    'Dim needUpdate As Boolean = False
    'Dim lhttp As New HTTP
    'If Not File.Exists(Path.Combine(Functions.AppPath, "Media.emm")) Then
    '	System.IO.File.Copy(Path.Combine(Path.Combine(Functions.AppPath, "Resources"), "commands_base.xml"), Path.Combine(Functions.AppPath, "InstallTasks.xml"))
    '	'lhttp.DownloadFile(String.Format("http://pcjco.dommel.be/emm-r/{0}/commands_base.xml", If(Functions.IsBetaEnabled(), "updatesbeta", "updates")), Path.Combine(Functions.AppPath, "InstallTasks.xml"), False, "other")
    'End If
    'If File.Exists(Path.Combine(Functions.AppPath, "InstallTasks.xml")) Then
    '	Master.DB.PatchDatabase("InstallTasks.xml")
    '	File.Delete(Path.Combine(Functions.AppPath, "InstallTasks.xml"))
    '	needUpdate = True
    'End If
    'If File.Exists(Path.Combine(Functions.AppPath, "UpdateTasks.xml")) Then
    '	Master.DB.PatchDatabase("UpdateTasks.xml")
    '	File.Delete(Path.Combine(Functions.AppPath, "UpdateTasks.xml"))
    '	needUpdate = True
    'End If
    'Return needUpdate
    '  End Function

    ''' <summary>
    ''' Saves all information from a Structures.DBMovie object to the database
    ''' </summary>
    ''' <param name="_movieDB">Media.Movie object to save to the database</param>
    ''' <param name="IsNew">Is this a new movie (not already present in database)?</param>
    ''' <param name="BatchMode">Is the function already part of a transaction?</param>
    ''' <param name="ToNfo">Save the information to an nfo file?</param>
    ''' <returns>Structures.DBMovie object</returns>
    Public Function SaveMovieToDB(ByVal _movieDB As Structures.DBMovie, ByVal IsNew As Boolean, Optional ByVal BatchMode As Boolean = False, Optional ByVal ToNfo As Boolean = False) As Structures.DBMovie
        'TODO Must add parameter checking. Needs thought to ensure calling routines are not broken if exception thrown. 
        'TODO Break this method into smaller chunks. Too important to be this complex

        Dim SQLtransaction As SQLite.SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            If IsNew Then
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO movie (", _
                 "MoviePath, Type, ListTitle, HasPoster, HasFanart, HasNfo, HasTrailer, HasSub, HasEThumbs, New, Mark, Source, Imdb, Lock, ", _
                 "Title, OriginalTitle, SortTitle, Year, Rating, Votes, MPAA, Top250, Country, Outline, Plot, Tagline, Certification, Genre, ", _
                 "Studio, Runtime, ReleaseDate, Director, Credits, Playcount, HasWatched, Trailer, ", _
                 "PosterPath, FanartPath, NfoPath, TrailerPath, SubPath, EThumbsPath, FanartURL, UseFolder, OutOfTolerance, VideoSource, NeedsSave, ", _
                 "DateAdded, HasEFanarts, EFanartsPath, HasBanner, BannerPath, HasLandscape, LandscapePath, HasTheme, ThemePath, HasDiscArt, DiscArtPath, ", _
                 "HasClearLogo, ClearLogoPath, HasClearArt, ClearArtPath, TMDB, TMDBColID, DateModified, MarkCustom1, MarkCustom2, MarkCustom3, MarkCustom4, HasSet", _
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM movie;")
            Else
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO movie (", _
                 "idMovie, MoviePath, Type, ListTitle, HasPoster, HasFanart, HasNfo, HasTrailer, HasSub, HasEThumbs, New, Mark, Source, Imdb, Lock, ", _
                 "Title, OriginalTitle, SortTitle, Year, Rating, Votes, MPAA, Top250, Country, Outline, Plot, Tagline, Certification, Genre, ", _
                 "Studio, Runtime, ReleaseDate, Director, Credits, Playcount, HasWatched, Trailer, ", _
                 "PosterPath, FanartPath, NfoPath, TrailerPath, SubPath, EThumbsPath, FanartURL, UseFolder, OutOfTolerance, VideoSource, NeedsSave, ", _
                 "DateAdded, HasEFanarts, EFanartsPath, HasBanner, BannerPath, HasLandscape, LandscapePath, HasTheme, ThemePath, HasDiscArt, DiscArtPath, ", _
                 "HasClearLogo, ClearLogoPath, HasClearArt, ClearArtPath, TMDB, TMDBColID, DateModified, MarkCustom1, MarkCustom2, MarkCustom3, MarkCustom4, HasSet", _
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM movie;")
                Dim parMovieID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("paridMovie", DbType.Int32, 0, "idMovie")
                parMovieID.Value = _movieDB.ID
            End If
            Dim parMoviePath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMoviePath", DbType.String, 0, "MoviePath")
            Dim parType As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parType", DbType.Boolean, 0, "Type")
            Dim parListTitle As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parListTitle", DbType.String, 0, "ListTitle")
            Dim parHasPoster As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasPoster", DbType.Boolean, 0, "HasPoster")
            Dim parHasFanart As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasFanart", DbType.Boolean, 0, "HasFanart")
            Dim parHasNfo As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasInfo", DbType.Boolean, 0, "HasNfo")
            Dim parHasTrailer As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasTrailer", DbType.Boolean, 0, "HasTrailer")
            Dim parHasSub As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasSub", DbType.Boolean, 0, "HasSub")
            Dim parHasEThumbs As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasEThumbs", DbType.Boolean, 0, "HasEThumbs")
            Dim parNew As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parNew", DbType.Boolean, 0, "New")
            Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "Mark")
            Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.String, 0, "Source")
            Dim parIMDB As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parIMDB", DbType.String, 0, "Imdb")
            Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "Lock")
            Dim parTitle As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTitle", DbType.String, 0, "Title")
            Dim parOriginalTitle As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parOriginalTitle", DbType.String, 0, "OriginalTitle")
            Dim parSortTitle As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSortTitle", DbType.String, 0, "SortTitle")
            Dim parYear As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parYear", DbType.String, 0, "Year")
            Dim parRating As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parRating", DbType.String, 0, "Rating")
            Dim parVotes As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parVotes", DbType.String, 0, "Votes")
            Dim parMPAA As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMPAA", DbType.String, 0, "MPAA")
            Dim parTop250 As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTop250", DbType.String, 0, "Top250")
            Dim parCountry As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parCountry", DbType.String, 0, "Country")
            Dim parOutline As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parOutline", DbType.String, 0, "Outline")
            Dim parPlot As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPlot", DbType.String, 0, "Plot")
            Dim parTagline As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTagline", DbType.String, 0, "Tagline")
            Dim parCertification As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parCertification", DbType.String, 0, "Certification")
            Dim parGenre As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parGenre", DbType.String, 0, "Genre")
            Dim parStudio As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parStudio", DbType.String, 0, "Studio")
            Dim parRuntime As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parRuntime", DbType.String, 0, "Runtime")
            Dim parReleaseDate As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parReleaseDate", DbType.String, 0, "ReleaseDate")
            Dim parDirector As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parDirector", DbType.String, 0, "Director")
            Dim parCredits As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parCredits", DbType.String, 0, "Credits")
            Dim parPlaycount As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPlaycount", DbType.String, 0, "Playcount")
            Dim parHasWatched As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasWatched", DbType.Boolean, 0, "HasWatched")
            Dim parTrailer As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTrailer", DbType.String, 0, "Trailer")
            Dim parPosterPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPosterPath", DbType.String, 0, "PosterPath")
            Dim parFanartPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parFanartPath", DbType.String, 0, "FanartPath")
            Dim parNfoPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parNfoPath", DbType.String, 0, "NfoPath")
            Dim parTrailerPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTrailerPath", DbType.String, 0, "TrailerPath")
            Dim parSubPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSubPath", DbType.String, 0, "SubPath")
            Dim parEThumbsPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parEThumbsPath", DbType.String, 0, "EThumbsPath")
            Dim parFanartURL As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parFanartURL", DbType.String, 0, "FanartURL")
            Dim parUseFolder As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parUseFolder", DbType.Boolean, 0, "UseFolder")
            Dim parOutOfTolerance As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parOutOfTolerance", DbType.Boolean, 0, "OutOfTolerance")
            Dim parVideoSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parVideoSource", DbType.String, 0, "VideoSource")
            Dim parNeedsSave As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parNeedsSave", DbType.Boolean, 0, "NeedsSave")
            Dim parDateAdded As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parDateAdded", DbType.Int32, 0, "DateAdded")
            Dim parHasEFanarts As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasEFanarts", DbType.Boolean, 0, "HasEFanarts")
            Dim parEFanartsPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parEFanartsPath", DbType.String, 0, "EFanartsPath")
            Dim parHasBanner As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasBanner", DbType.Boolean, 0, "HasBanner")
            Dim parBannerPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parBannerPath", DbType.String, 0, "BannerPath")
            Dim parHasLandscape As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasLandscape", DbType.Boolean, 0, "HasLandscape")
            Dim parLandscapePath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLandscapePath", DbType.String, 0, "LandscapePath")
            Dim parHasTheme As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasTheme", DbType.Boolean, 0, "HasTheme")
            Dim parThemePath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parThemePath", DbType.String, 0, "ThemePath")
            Dim parHasDiscArt As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasDiscArt", DbType.Boolean, 0, "HasDiscArt")
            Dim parDiscArtPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parDiscArtPath", DbType.String, 0, "DiscArtPath")
            Dim parHasClearLogo As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasClearLogo", DbType.Boolean, 0, "HasClearLogo")
            Dim parClearLogoPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parClearLogoPath", DbType.String, 0, "ClearLogoPath")
            Dim parHasClearArt As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasClearArt", DbType.Boolean, 0, "HasClearArt")
            Dim parClearArtPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parClearArtPath", DbType.String, 0, "ClearArtPath")
            Dim parTMDB As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTMDB", DbType.String, 0, "TMDB")
            Dim parTMDBColID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTMDBColID", DbType.String, 0, "TMDBColID")
            Dim parDateModified As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parDateModified", DbType.Int32, 0, "DateModified")
            Dim parMarkCustom1 As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom1", DbType.Boolean, 0, "MarkCustom1")
            Dim parMarkCustom2 As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom2", DbType.Boolean, 0, "MarkCustom2")
            Dim parMarkCustom3 As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom3", DbType.Boolean, 0, "MarkCustom3")
            Dim parMarkCustom4 As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom4", DbType.Boolean, 0, "MarkCustom4")
            Dim parHasSet As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasSet", DbType.Boolean, 0, "HasSet")

            Try
                If Not Master.eSettings.GeneralDateAddedIgnoreNFO AndAlso Not String.IsNullOrEmpty(_movieDB.Movie.DateAdded) Then
                    Dim DateTimeAdded As DateTime = DateTime.ParseExact(_movieDB.Movie.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    parDateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    Select Case Master.eSettings.GeneralDateTime
                        Case Enums.DateTime.Now
                            parDateAdded.Value = If(IsNew, Functions.ConvertToUnixTimestamp(Now), _movieDB.DateAdded)
                        Case Enums.DateTime.mtime
                            Dim mtime As Date = System.IO.File.GetLastWriteTime(_movieDB.Filename)
                            If mtime.Year > 1601 Then
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                Dim ctime As Date = System.IO.File.GetCreationTime(_movieDB.Filename)
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                        Case Enums.DateTime.Newer
                            Dim mtime As Date = System.IO.File.GetLastWriteTime(_movieDB.Filename)
                            Dim ctime As Date = System.IO.File.GetCreationTime(_movieDB.Filename)
                            If mtime > ctime Then
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                    End Select
                End If
                _movieDB.Movie.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(parDateAdded.Value)).ToString("yyyy-MM-d HH:mm:ss")
            Catch ex As Exception
                parDateAdded.Value = If(IsNew, Functions.ConvertToUnixTimestamp(Now), _movieDB.DateAdded)
                _movieDB.Movie.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(parDateAdded.Value)).ToString("yyyy-MM-d HH:mm:ss")
            End Try

            If IsNew AndAlso Not String.IsNullOrEmpty(_movieDB.Movie.DateModified) Then
                Dim DateTimeDateModified As DateTime = DateTime.ParseExact(_movieDB.Movie.DateModified, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                parDateModified.Value = Functions.ConvertToUnixTimestamp(DateTimeDateModified)
            ElseIf Not IsNew Then
                parDateModified.Value = Functions.ConvertToUnixTimestamp(Now)
            End If
            If Not IsNothing(parDateModified.Value) Then
                _movieDB.Movie.DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(parDateModified.Value)).ToString("yyyy-MM-d HH:mm:ss")
            Else
                _movieDB.Movie.DateModified = String.Empty
            End If

            ' First let's save it to NFO, even because we will need the NFO path
            If ToNfo Then NFO.SaveMovieToNFO(_movieDB)

            parMoviePath.Value = _movieDB.Filename
            parType.Value = _movieDB.IsSingle
            parListTitle.Value = _movieDB.ListTitle

            parBannerPath.Value = _movieDB.BannerPath
            parClearArtPath.Value = _movieDB.ClearArtPath
            parClearLogoPath.Value = _movieDB.ClearLogoPath
            parDiscArtPath.Value = _movieDB.DiscArtPath
            parEFanartsPath.Value = _movieDB.EFanartsPath
            parEThumbsPath.Value = _movieDB.EThumbsPath
            parFanartPath.Value = _movieDB.FanartPath
            parLandscapePath.Value = _movieDB.LandscapePath
            parNfoPath.Value = _movieDB.NfoPath
            parPosterPath.Value = _movieDB.PosterPath
            parSubPath.Value = _movieDB.SubPath
            parThemePath.Value = _movieDB.ThemePath
            parTrailerPath.Value = _movieDB.TrailerPath

            If Not Master.eSettings.MovieNoSaveImagesToNfo Then
                parFanartURL.Value = _movieDB.Movie.Fanart.URL
            Else
                parFanartURL.Value = String.Empty
            End If

            parHasBanner.Value = Not String.IsNullOrEmpty(_movieDB.BannerPath)
            parHasClearArt.Value = Not String.IsNullOrEmpty(_movieDB.ClearArtPath)
            parHasClearLogo.Value = Not String.IsNullOrEmpty(_movieDB.ClearLogoPath)
            parHasDiscArt.Value = Not String.IsNullOrEmpty(_movieDB.DiscArtPath)
            parHasEFanarts.Value = Not String.IsNullOrEmpty(_movieDB.EFanartsPath)
            parHasEThumbs.Value = Not String.IsNullOrEmpty(_movieDB.EThumbsPath)
            parHasFanart.Value = Not String.IsNullOrEmpty(_movieDB.FanartPath)
            parHasLandscape.Value = Not String.IsNullOrEmpty(_movieDB.LandscapePath)
            parHasNfo.Value = Not String.IsNullOrEmpty(_movieDB.NfoPath)
            parHasPoster.Value = Not String.IsNullOrEmpty(_movieDB.PosterPath)
            parHasSub.Value = _movieDB.Subtitles.Count > 0 OrElse _movieDB.Movie.FileInfo.StreamDetails.Subtitle.Count > 0
            parHasTheme.Value = Not String.IsNullOrEmpty(_movieDB.ThemePath)
            parHasTrailer.Value = Not String.IsNullOrEmpty(_movieDB.TrailerPath)
            parHasWatched.Value = Not String.IsNullOrEmpty(_movieDB.Movie.PlayCount) AndAlso Not _movieDB.Movie.PlayCount = "0"
            parHasSet.Value = _movieDB.Movie.Sets.Count > 0

            parNew.Value = IsNew
            parMark.Value = _movieDB.IsMark
            parMarkCustom1.Value = _movieDB.IsMarkCustom1
            parMarkCustom2.Value = _movieDB.IsMarkCustom2
            parMarkCustom3.Value = _movieDB.IsMarkCustom3
            parMarkCustom4.Value = _movieDB.IsMarkCustom4
            parLock.Value = _movieDB.IsLock

            parIMDB.Value = _movieDB.Movie.IMDBID
            parTitle.Value = _movieDB.Movie.Title
            parOriginalTitle.Value = _movieDB.Movie.OriginalTitle
            parSortTitle.Value = _movieDB.Movie.SortTitle
            parYear.Value = _movieDB.Movie.Year
            parRating.Value = _movieDB.Movie.Rating
            parVotes.Value = _movieDB.Movie.Votes
            parMPAA.Value = _movieDB.Movie.MPAA
            parTop250.Value = _movieDB.Movie.Top250
            parCountry.Value = _movieDB.Movie.Country
            parOutline.Value = _movieDB.Movie.Outline
            parPlot.Value = _movieDB.Movie.Plot
            parTagline.Value = _movieDB.Movie.Tagline
            parCertification.Value = _movieDB.Movie.Certification
            parGenre.Value = _movieDB.Movie.Genre
            parStudio.Value = _movieDB.Movie.Studio
            parRuntime.Value = _movieDB.Movie.Runtime
            parReleaseDate.Value = _movieDB.Movie.ReleaseDate
            parDirector.Value = _movieDB.Movie.Director
            parCredits.Value = _movieDB.Movie.OldCredits
            parPlaycount.Value = _movieDB.Movie.PlayCount
            parTrailer.Value = _movieDB.Movie.Trailer
            parTMDB.Value = _movieDB.Movie.TMDBID
            parTMDBColID.Value = _movieDB.Movie.TMDBColID

            parUseFolder.Value = _movieDB.UseFolder
            parOutOfTolerance.Value = _movieDB.OutOfTolerance
            parVideoSource.Value = _movieDB.VideoSource
            parNeedsSave.Value = _movieDB.NeedsSave

            parSource.Value = _movieDB.Source

            If IsNew Then
                If Master.eSettings.MovieGeneralMarkNew Then
                    parMark.Value = True
                Else
                    parMark.Value = False
                End If
                Using rdrMovie As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    If rdrMovie.Read Then
                        _movieDB.ID = Convert.ToInt64(rdrMovie(0))
                    Else
                        logger.Error("Something very wrong here: SaveMovieToDB", _movieDB.ToString)
                        _movieDB.ID = -1
                        Return _movieDB
                    End If
                End Using
            Else
                SQLcommand.ExecuteNonQuery()
            End If

            If Not _movieDB.ID = -1 Then
                Using SQLcommand_actor As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_actor.CommandText = String.Concat("DELETE FROM actorlinkmovie WHERE idMovie = ", _movieDB.ID, ";")
                    SQLcommand_actor.ExecuteNonQuery()

                    SQLcommand_actor.CommandText = String.Concat("INSERT OR REPLACE INTO actor (strActor, strThumb) VALUES (?,?)")
                    Dim par_actor_strActor As SQLite.SQLiteParameter = SQLcommand_actor.Parameters.Add("par_actor_strActor", DbType.String, 0, "strActor")
                    Dim par_actor_strThumb As SQLite.SQLiteParameter = SQLcommand_actor.Parameters.Add("par_actor_strThumb", DbType.String, 0, "strThumb")
                    For Each actor As MediaContainers.Person In _movieDB.Movie.Actors
                        par_actor_strActor.Value = actor.Name
                        par_actor_strThumb.Value = actor.Thumb
                        SQLcommand_actor.ExecuteNonQuery()

                        'Using SQLcommandSets As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                        '    SQLcommandSets.CommandText = String.Concat("SELECT idActor, strThumb FROM actor WHERE strActor LIKE """, actor.Name, """;")
                        '    Using rdrSets As SQLite.SQLiteDataReader = SQLcommandSets.ExecuteReader()
                        '        If rdrSets.Read Then
                        '            actor.ID = CInt(rdrSets(0))
                        '            actor.Thumb = CStr(rdrSets(1))
                        '        End If
                        '    End Using
                        'End Using

                        If Not actor.ID = -1 Then
                            Using SQLcommand_actorlinkmovie As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                SQLcommand_actorlinkmovie.CommandText = String.Concat("INSERT OR REPLACE INTO actorlinkmovie (idActor, idMovie, strRole) VALUES (?,?,?);")
                                Dim par_actorlinkmovie_idActor As SQLite.SQLiteParameter = SQLcommand_actorlinkmovie.Parameters.Add("par_actorlinkmovie_idActor", DbType.UInt64, 0, "idActor")
                                Dim par_actorlinkmovie_idMovie As SQLite.SQLiteParameter = SQLcommand_actorlinkmovie.Parameters.Add("par_actorlinkmovie_idMovie", DbType.UInt64, 0, "idMovie")
                                Dim par_actorlinkmovie_strRole As SQLite.SQLiteParameter = SQLcommand_actorlinkmovie.Parameters.Add("par_actorlinkmovie_strRole", DbType.String, 0, "strRole")
                                par_actorlinkmovie_idActor.Value = actor.ID
                                par_actorlinkmovie_idMovie.Value = _movieDB.ID
                                par_actorlinkmovie_strRole.Value = actor.Role
                                SQLcommand_actorlinkmovie.ExecuteNonQuery()
                            End Using
                        End If
                    Next
                End Using
                Using SQLcommandMoviesVStreams As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandMoviesVStreams.CommandText = String.Concat("DELETE FROM MoviesVStreams WHERE MovieID = ", _movieDB.ID, ";")
                    SQLcommandMoviesVStreams.ExecuteNonQuery()

                    'Expanded SQL Statement to INSERT/replace new fields
                    SQLcommandMoviesVStreams.CommandText = String.Concat("INSERT OR REPLACE INTO MoviesVStreams (", _
                       "MovieID, StreamID, Video_Width,Video_Height,Video_Codec,Video_Duration, Video_ScanType, Video_AspectDisplayRatio, ", _
                       "Video_Language, Video_LongLanguage, Video_Bitrate, Video_MultiViewCount, Video_EncodedSettings, Video_MultiViewLayout, ", _
                       "Video_StereoMode) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);")

                    Dim parVideo_MovieID As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_MovieID", DbType.UInt64, 0, "MovieID")
                    Dim parVideo_StreamID As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_StreamID", DbType.UInt64, 0, "StreamID")
                    Dim parVideo_Width As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_Width", DbType.String, 0, "Video_Width")
                    Dim parVideo_Height As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_Height", DbType.String, 0, "Video_Height")
                    Dim parVideo_Codec As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_Codec", DbType.String, 0, "Video_Codec")
                    Dim parVideo_Duration As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_Duration", DbType.String, 0, "Video_Duration")
                    Dim parVideo_ScanType As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_ScanType", DbType.String, 0, "Video_ScanType")
                    Dim parVideo_AspectDisplayRatio As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_AspectDisplayRatio", DbType.String, 0, "Video_AspectDisplayRatio")
                    Dim parVideo_Language As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_Language", DbType.String, 0, "Video_Language")
                    Dim parVideo_LongLanguage As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_LongLanguage", DbType.String, 0, "Video_LongLanguage")
                    Dim parVideo_Bitrate As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_Bitrate", DbType.String, 0, "Video_Bitrate")
                    Dim parVideo_MultiViewCount As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_MultiViewCount", DbType.String, 0, "Video_MultiViewCount")
                    Dim parVideo_EncodedSettings As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_EncodedSettings", DbType.String, 0, "Video_EncodedSettings")
                    Dim parVideo_MultiViewLayout As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_MultiViewLayout", DbType.String, 0, "Video_MultiViewLayout")
                    Dim parVideo_StereoMode As SQLite.SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_StereoMode", DbType.String, 0, "Video_StereoMode")

                    For i As Integer = 0 To _movieDB.Movie.FileInfo.StreamDetails.Video.Count - 1
                        parVideo_MovieID.Value = _movieDB.ID
                        parVideo_StreamID.Value = i
                        parVideo_Width.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).Width
                        parVideo_Height.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).Height
                        parVideo_Codec.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).Codec
                        parVideo_Duration.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).Duration
                        parVideo_ScanType.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).Scantype
                        parVideo_AspectDisplayRatio.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).Aspect
                        parVideo_Language.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).Language
                        parVideo_LongLanguage.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).LongLanguage
                        parVideo_Bitrate.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).Bitrate
                        parVideo_MultiViewCount.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).MultiViewCount
                        parVideo_MultiViewLayout.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).MultiViewLayout
                        parVideo_EncodedSettings.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).EncodedSettings
                        parVideo_StereoMode.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).StereoMode

                        SQLcommandMoviesVStreams.ExecuteNonQuery()
                    Next
                End Using
                Using SQLcommandMoviesAStreams As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandMoviesAStreams.CommandText = String.Concat("DELETE FROM MoviesAStreams WHERE MovieID = ", _movieDB.ID, ";")
                    SQLcommandMoviesAStreams.ExecuteNonQuery()

                    'Expanded SQL Statement to INSERT/replace new fields
                    SQLcommandMoviesAStreams.CommandText = String.Concat("INSERT OR REPLACE INTO MoviesAStreams (", _
                      "MovieID, StreamID, Audio_Language, Audio_LongLanguage, Audio_Codec, Audio_Channel, Audio_Bitrate", _
                      ") VALUES (?,?,?,?,?,?,?);")

                    Dim parAudio_MovieID As SQLite.SQLiteParameter = SQLcommandMoviesAStreams.Parameters.Add("parAudio_MovieID", DbType.UInt64, 0, "MovieID")
                    Dim parAudio_StreamID As SQLite.SQLiteParameter = SQLcommandMoviesAStreams.Parameters.Add("parAudio_StreamID", DbType.UInt64, 0, "StreamID")
                    Dim parAudio_Language As SQLite.SQLiteParameter = SQLcommandMoviesAStreams.Parameters.Add("parAudio_Language", DbType.String, 0, "Audio_Language")
                    Dim parAudio_LongLanguage As SQLite.SQLiteParameter = SQLcommandMoviesAStreams.Parameters.Add("parAudio_LongLanguage", DbType.String, 0, "Audio_LongLanguage")
                    Dim parAudio_Codec As SQLite.SQLiteParameter = SQLcommandMoviesAStreams.Parameters.Add("parAudio_Codec", DbType.String, 0, "Audio_Codec")
                    Dim parAudio_Channel As SQLite.SQLiteParameter = SQLcommandMoviesAStreams.Parameters.Add("parAudio_Channel", DbType.String, 0, "Audio_Channel")
                    Dim parAudio_Bitrate As SQLite.SQLiteParameter = SQLcommandMoviesAStreams.Parameters.Add("parAudio_Bitrate", DbType.String, 0, "Audio_Bitrate")

                    For i As Integer = 0 To _movieDB.Movie.FileInfo.StreamDetails.Audio.Count - 1
                        parAudio_MovieID.Value = _movieDB.ID
                        parAudio_StreamID.Value = i
                        parAudio_Language.Value = _movieDB.Movie.FileInfo.StreamDetails.Audio(i).Language
                        parAudio_LongLanguage.Value = _movieDB.Movie.FileInfo.StreamDetails.Audio(i).LongLanguage
                        parAudio_Codec.Value = _movieDB.Movie.FileInfo.StreamDetails.Audio(i).Codec
                        parAudio_Channel.Value = _movieDB.Movie.FileInfo.StreamDetails.Audio(i).Channels
                        parAudio_Bitrate.Value = _movieDB.Movie.FileInfo.StreamDetails.Audio(i).Bitrate

                        SQLcommandMoviesAStreams.ExecuteNonQuery()
                    Next
                End Using

                'subtitles
                Using SQLcommandMoviesSubs As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandMoviesSubs.CommandText = String.Concat("DELETE FROM MoviesSubs WHERE MovieID = ", _movieDB.ID, ";")
                    SQLcommandMoviesSubs.ExecuteNonQuery()

                    SQLcommandMoviesSubs.CommandText = String.Concat("INSERT OR REPLACE INTO MoviesSubs (", _
                       "MovieID, StreamID, Subs_Language, Subs_LongLanguage,Subs_Type, Subs_Path, Subs_Forced", _
                       ") VALUES (?,?,?,?,?,?,?);")
                    Dim parSubs_MovieID As SQLite.SQLiteParameter = SQLcommandMoviesSubs.Parameters.Add("parSubs_MovieID", DbType.UInt64, 0, "MovieID")
                    Dim parSubs_StreamID As SQLite.SQLiteParameter = SQLcommandMoviesSubs.Parameters.Add("parSubs_StreamID", DbType.UInt64, 0, "StreamID")
                    Dim parSubs_Language As SQLite.SQLiteParameter = SQLcommandMoviesSubs.Parameters.Add("parSubs_Language", DbType.String, 0, "Subs_Language")
                    Dim parSubs_LongLanguage As SQLite.SQLiteParameter = SQLcommandMoviesSubs.Parameters.Add("parSubs_LongLanguage", DbType.String, 0, "Subs_LongLanguage")
                    Dim parSubs_Type As SQLite.SQLiteParameter = SQLcommandMoviesSubs.Parameters.Add("parSubs_Type", DbType.String, 0, "Subs_Type")
                    Dim parSubs_Path As SQLite.SQLiteParameter = SQLcommandMoviesSubs.Parameters.Add("parSubs_Path", DbType.String, 0, "Subs_Path")
                    Dim parSubs_Forced As SQLite.SQLiteParameter = SQLcommandMoviesSubs.Parameters.Add("parSubs_Forced", DbType.Boolean, 0, "Subs_Forced")
                    Dim iID As Integer = 0
                    'embedded subtitles
                    For i As Integer = 0 To _movieDB.Movie.FileInfo.StreamDetails.Subtitle.Count - 1
                        parSubs_MovieID.Value = _movieDB.ID
                        parSubs_StreamID.Value = iID
                        parSubs_Language.Value = _movieDB.Movie.FileInfo.StreamDetails.Subtitle(i).Language
                        parSubs_LongLanguage.Value = _movieDB.Movie.FileInfo.StreamDetails.Subtitle(i).LongLanguage
                        parSubs_Type.Value = _movieDB.Movie.FileInfo.StreamDetails.Subtitle(i).SubsType
                        parSubs_Path.Value = _movieDB.Movie.FileInfo.StreamDetails.Subtitle(i).SubsPath
                        parSubs_Forced.Value = _movieDB.Movie.FileInfo.StreamDetails.Subtitle(i).SubsForced
                        SQLcommandMoviesSubs.ExecuteNonQuery()
                        iID += 1
                    Next
                    'external subtitles
                    For i As Integer = 0 To _movieDB.Subtitles.Count - 1
                        parSubs_MovieID.Value = _movieDB.ID
                        parSubs_StreamID.Value = iID
                        parSubs_Language.Value = _movieDB.Subtitles(i).Language
                        parSubs_LongLanguage.Value = _movieDB.Subtitles(i).LongLanguage
                        parSubs_Type.Value = _movieDB.Subtitles(i).SubsType
                        parSubs_Path.Value = _movieDB.Subtitles(i).SubsPath
                        parSubs_Forced.Value = _movieDB.Subtitles(i).SubsForced
                        SQLcommandMoviesSubs.ExecuteNonQuery()
                        iID += 1
                    Next
                End Using

                ' For what i understand this is used from Poster/Fanart Modules... will not be read/wrtire directly when load/save Movie
                Using SQLcommandMoviesPosters As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandMoviesPosters.CommandText = String.Concat("DELETE FROM MoviesPosters WHERE MovieID = ", _movieDB.ID, ";")
                    SQLcommandMoviesPosters.ExecuteNonQuery()

                    If Not Master.eSettings.MovieNoSaveImagesToNfo Then
                        SQLcommandMoviesPosters.CommandText = String.Concat("INSERT OR REPLACE INTO MoviesPosters (", _
                           "MovieID, thumbs) VALUES (?,?);")
                        Dim parPosters_MovieID As SQLite.SQLiteParameter = SQLcommandMoviesPosters.Parameters.Add("parPosters_MovieID", DbType.UInt64, 0, "MovieID")
                        Dim parPosters_thumb As SQLite.SQLiteParameter = SQLcommandMoviesPosters.Parameters.Add("parPosters_thumb", DbType.String, 0, "thumbs")
                        For Each p As String In _movieDB.Movie.Thumb
                            parPosters_MovieID.Value = _movieDB.ID
                            parPosters_thumb.Value = p
                            SQLcommandMoviesPosters.ExecuteNonQuery()
                        Next
                    End If
                End Using
                Using SQLcommandMoviesFanart As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandMoviesFanart.CommandText = String.Concat("DELETE FROM MoviesFanart WHERE MovieID = ", _movieDB.ID, ";")
                    SQLcommandMoviesFanart.ExecuteNonQuery()

                    If Not Master.eSettings.MovieNoSaveImagesToNfo Then
                        SQLcommandMoviesFanart.CommandText = String.Concat("INSERT OR REPLACE INTO MoviesFanart (", _
                             "MovieID, preview, thumbs", _
                             ") VALUES (?,?,?);")
                        Dim parFanart_MovieID As SQLite.SQLiteParameter = SQLcommandMoviesFanart.Parameters.Add("parFanart_MovieID", DbType.UInt64, 0, "MovieID")
                        Dim parFanart_Preview As SQLite.SQLiteParameter = SQLcommandMoviesFanart.Parameters.Add("parFanart_Preview", DbType.String, 0, "Preview")
                        Dim parFanart_thumb As SQLite.SQLiteParameter = SQLcommandMoviesFanart.Parameters.Add("parFanart_thumb", DbType.String, 0, "thumb")
                        For Each p As MediaContainers.Thumb In _movieDB.Movie.Fanart.Thumb
                            parFanart_MovieID.Value = _movieDB.ID
                            parFanart_Preview.Value = p.Preview
                            parFanart_thumb.Value = p.Text
                            SQLcommandMoviesFanart.ExecuteNonQuery()
                        Next
                    End If
                End Using

                'Tags part
                Using SQLcommand_taglinks As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_taglinks.CommandText = String.Concat("DELETE FROM taglinks WHERE idMedia = ", _movieDB.ID, " AND mediatype = 'movie';")
                    SQLcommand_taglinks.ExecuteNonQuery()
                End Using

                'MovieSets part
                Using SQLcommandMovieSets As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandMovieSets.CommandText = String.Concat("DELETE FROM MoviesSets WHERE MovieID = ", _movieDB.ID, ";")
                    SQLcommandMovieSets.ExecuteNonQuery()
                End Using

                Dim IsNewSet As Boolean
                For Each s As MediaContainers.Set In _movieDB.Movie.Sets
                    If s.TitleSpecified Then
                        IsNewSet = Not s.ID > 0
                        If Not IsNewSet Then
                            Using SQLcommandMoviesSets As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                SQLcommandMoviesSets.CommandText = String.Concat("INSERT OR REPLACE INTO MoviesSets (", _
                             "MovieID, SetID, SetOrder", _
                             ") VALUES (?,?,?);")
                                Dim parMoviesSets_MovieID As SQLite.SQLiteParameter = SQLcommandMoviesSets.Parameters.Add("parSets_MovieID", DbType.Int64, 0, "MovieID")
                                Dim parMoviesSets_SetID As SQLite.SQLiteParameter = SQLcommandMoviesSets.Parameters.Add("parSets_SetID", DbType.Int64, 0, "SetID")
                                Dim parMoviesSets_SetOrder As SQLite.SQLiteParameter = SQLcommandMoviesSets.Parameters.Add("parSets_SetOrder", DbType.String, 0, "SetOrder")

                                parMoviesSets_MovieID.Value = _movieDB.ID
                                parMoviesSets_SetID.Value = s.ID
                                parMoviesSets_SetOrder.Value = s.Order
                                SQLcommandMoviesSets.ExecuteNonQuery()
                            End Using
                        Else
                            'first check if a Set with same TMDBColID is already existing
                            If Not String.IsNullOrEmpty(s.TMDBColID) Then
                                Using SQLcommandSets As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    SQLcommandSets.CommandText = String.Concat("SELECT ID, ListTitle, HasNfo, NfoPath, HasPoster, PosterPath, HasFanart, ", _
                                                                           "FanartPath, HasBanner, BannerPath, HasLandscape, LandscapePath, ", _
                                                                           "HasDiscArt, DiscArtPath, HasClearLogo, ClearLogoPath, HasClearArt, ", _
                                                                           "ClearArtPath, TMDBColID, Plot, SetName, New, Mark, Lock FROM Sets WHERE TMDBColID LIKE """, s.TMDBColID, """;")
                                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommandSets.ExecuteReader()
                                        If SQLreader.HasRows Then
                                            SQLreader.Read()
                                            If Not DBNull.Value.Equals(SQLreader("ID")) Then s.ID = CInt(SQLreader("ID"))
                                            If Not DBNull.Value.Equals(SQLreader("SetName")) Then s.Title = CStr(SQLreader("SetName"))
                                            IsNewSet = False
                                            NFO.SaveMovieToNFO(_movieDB) 'to save the "new" SetName
                                        Else
                                            IsNewSet = True
                                        End If
                                    End Using
                                End Using
                            End If

                            If IsNewSet Then
                                'secondly check if a Set with same name is already existing
                                Using SQLcommandSets As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    SQLcommandSets.CommandText = String.Concat("SELECT ID, ListTitle, HasNfo, NfoPath, HasPoster, PosterPath, HasFanart, ", _
                                                                           "FanartPath, HasBanner, BannerPath, HasLandscape, LandscapePath, ", _
                                                                           "HasDiscArt, DiscArtPath, HasClearLogo, ClearLogoPath, HasClearArt, ", _
                                                                           "ClearArtPath, TMDBColID, Plot, SetName, New, Mark, Lock FROM Sets WHERE SetName LIKE """, s.Title, """;")
                                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommandSets.ExecuteReader()
                                        If SQLreader.HasRows Then
                                            SQLreader.Read()
                                            If Not DBNull.Value.Equals(SQLreader("ID")) Then s.ID = CInt(SQLreader("ID"))
                                            IsNewSet = False
                                        Else
                                            IsNewSet = True
                                        End If
                                    End Using
                                End Using
                            End If

                            If Not IsNewSet Then
                                'create new MoviesSets with existing SetID
                                Using SQLcommandMoviesSets As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    SQLcommandMoviesSets.CommandText = String.Concat("INSERT OR REPLACE INTO MoviesSets (", _
                                                                                     "MovieID, SetID, SetOrder", _
                                                                                     ") VALUES (?,?,?);")
                                    Dim parMoviesSets_MovieID As SQLite.SQLiteParameter = SQLcommandMoviesSets.Parameters.Add("parSets_MovieID", DbType.Int64, 0, "MovieID")
                                    Dim parMoviesSets_SetID As SQLite.SQLiteParameter = SQLcommandMoviesSets.Parameters.Add("parSets_SetID", DbType.Int64, 0, "SetID")
                                    Dim parMoviesSets_SetOrder As SQLite.SQLiteParameter = SQLcommandMoviesSets.Parameters.Add("parSets_SetOrder", DbType.String, 0, "SetOrder")

                                    parMoviesSets_MovieID.Value = _movieDB.ID
                                    parMoviesSets_SetID.Value = s.ID
                                    parMoviesSets_SetOrder.Value = s.Order
                                    SQLcommandMoviesSets.ExecuteNonQuery()
                                End Using
                            Else
                                'create new Set
                                Using SQLcommandSets As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    SQLcommandSets.CommandText = String.Concat("INSERT OR REPLACE INTO Sets (", _
                                                                                     "ListTitle, HasNfo, NfoPath, HasPoster, PosterPath, HasFanart, ", _
                                                                                     "FanartPath, HasBanner, BannerPath, HasLandscape, LandscapePath, ", _
                                                                                     "HasDiscArt, DiscArtPath, HasClearLogo, ClearLogoPath, HasClearArt, ", _
                                                                                     "ClearArtPath, TMDBColID, Plot, SetName, New, Mark, Lock", _
                                                                                     ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);")
                                    Dim parSets_ListTitle As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_ListTitle", DbType.String, 0, "ListTitle")
                                    Dim parSets_HasNfo As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_HasInfo", DbType.Boolean, 0, "HasNfo")
                                    Dim parSets_NfoPath As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_NfoPath", DbType.String, 0, "NfoPath")
                                    Dim parSets_HasPoster As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_HasPoster", DbType.Boolean, 0, "HasPoster")
                                    Dim parSets_PosterPath As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_PosterPath", DbType.String, 0, "PosterPath")
                                    Dim parSets_HasFanart As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_HasFanart", DbType.Boolean, 0, "HasFanart")
                                    Dim parSets_FanartPath As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_FanartPath", DbType.String, 0, "FanartPath")
                                    Dim parSets_HasBanner As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_HasBanner", DbType.Boolean, 0, "HasBanner")
                                    Dim parSets_BannerPath As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_BannerPath", DbType.String, 0, "BannerPath")
                                    Dim parSets_HasLandscape As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_HasLandscape", DbType.Boolean, 0, "HasLandscape")
                                    Dim parSets_LandscapePath As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_LandscapePath", DbType.String, 0, "LandscapePath")
                                    Dim parSets_HasDiscArt As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_HasDiscArt", DbType.Boolean, 0, "HasDiscArt")
                                    Dim parSets_DiscArtPath As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_DiscArtPath", DbType.String, 0, "DiscArtPath")
                                    Dim parSets_HasClearLogo As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_HasClearLogo", DbType.Boolean, 0, "HasClearLogo")
                                    Dim parSets_ClearLogoPath As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_ClearLogoPath", DbType.String, 0, "ClearLogoPath")
                                    Dim parSets_HasClearArt As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_HasClearArt", DbType.Boolean, 0, "HasClearArt")
                                    Dim parSets_ClearArtPath As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_ClearArtPath", DbType.String, 0, "ClearArtPath")
                                    Dim parSets_TMDBColID As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_TMDBColID", DbType.String, 0, "TMDBColID")
                                    Dim parSets_Plot As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_Plot", DbType.String, 0, "Plot")
                                    Dim parSets_SetName As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_SetName", DbType.String, 0, "SetName")
                                    Dim parSets_New As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_New", DbType.Boolean, 0, "New")
                                    Dim parSets_Mark As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_Mark", DbType.Boolean, 0, "Mark")
                                    Dim parSets_Lock As SQLite.SQLiteParameter = SQLcommandSets.Parameters.Add("parSets_Lock", DbType.Boolean, 0, "Lock")

                                    parSets_SetName.Value = s.Title
                                    parSets_ListTitle.Value = StringUtils.FilterTokens_MovieSet(s.Title)
                                    parSets_TMDBColID.Value = s.TMDBColID
                                    parSets_Plot.Value = String.Empty
                                    parSets_BannerPath.Value = String.Empty
                                    parSets_ClearArtPath.Value = String.Empty
                                    parSets_ClearLogoPath.Value = String.Empty
                                    parSets_DiscArtPath.Value = String.Empty
                                    parSets_FanartPath.Value = String.Empty
                                    parSets_LandscapePath.Value = String.Empty
                                    parSets_NfoPath.Value = String.Empty
                                    parSets_PosterPath.Value = String.Empty
                                    parSets_HasBanner.Value = False
                                    parSets_HasClearArt.Value = False
                                    parSets_HasClearLogo.Value = False
                                    parSets_HasDiscArt.Value = False
                                    parSets_HasFanart.Value = False
                                    parSets_HasLandscape.Value = False
                                    parSets_HasNfo.Value = False
                                    parSets_HasPoster.Value = False
                                    parSets_New.Value = True
                                    parSets_Lock.Value = False

                                    If Master.eSettings.MovieSetGeneralMarkNew Then
                                        parSets_Mark.Value = True
                                    Else
                                        parSets_Mark.Value = False
                                    End If
                                    SQLcommandSets.ExecuteNonQuery()
                                End Using

                                Using SQLcommandSets As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    SQLcommandSets.CommandText = String.Concat("SELECT ID, SetName FROM Sets WHERE SetName LIKE """, s.Title, """;")
                                    Using rdrSets As SQLite.SQLiteDataReader = SQLcommandSets.ExecuteReader()
                                        If rdrSets.Read Then
                                            s.ID = Convert.ToInt64(rdrSets(0))
                                        End If
                                    End Using
                                End Using

                                'create new MoviesSets with new SetID
                                If s.ID > 0 Then
                                    Using SQLcommandMoviesSets As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                        SQLcommandMoviesSets.CommandText = String.Concat("INSERT OR REPLACE INTO MoviesSets (", _
                                                                                         "MovieID, SetID, SetOrder", _
                                                                                         ") VALUES (?,?,?);")
                                        Dim parMoviesSets_MovieID As SQLite.SQLiteParameter = SQLcommandMoviesSets.Parameters.Add("parSets_MovieID", DbType.Int64, 0, "MovieID")
                                        Dim parMoviesSets_SetID As SQLite.SQLiteParameter = SQLcommandMoviesSets.Parameters.Add("parSets_SetID", DbType.Int64, 0, "SetID")
                                        Dim parMoviesSets_SetOrder As SQLite.SQLiteParameter = SQLcommandMoviesSets.Parameters.Add("parSets_SetOrder", DbType.String, 0, "SetOrder")

                                        parMoviesSets_MovieID.Value = _movieDB.ID
                                        parMoviesSets_SetID.Value = s.ID
                                        parMoviesSets_SetOrder.Value = s.Order
                                        SQLcommandMoviesSets.ExecuteNonQuery()
                                    End Using
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        End Using
        If Not BatchMode Then SQLtransaction.Commit()

        Return _movieDB
    End Function
    ''' <summary>
    ''' Saves all information from a Structures.DBMovieSet object to the database
    ''' </summary>
    ''' <param name="_moviesetDB">Media.Movie object to save to the database</param>
    ''' <param name="IsNew">Is this a new movieset (not already present in database)?</param>
    ''' <param name="BatchMode">Is the function already part of a transaction?</param>
    ''' <param name="ToNfo">Save the information to an nfo file?</param>
    ''' <param name="withMovies">Save the information also to all linked movies?</param>
    ''' <returns>Structures.DBMovieSet object</returns>
    Public Function SaveMovieSetToDB(ByVal _moviesetDB As Structures.DBMovieSet, ByVal IsNew As Boolean, Optional ByVal BatchMode As Boolean = False, Optional ByVal ToNfo As Boolean = False, Optional ByVal withMovies As Boolean = False) As Structures.DBMovieSet
        'TODO Must add parameter checking. Needs thought to ensure calling routines are not broken if exception thrown. 
        'TODO Break this method into smaller chunks. Too important to be this complex
        Try
            If _moviesetDB.ID = -1 Then IsNew = True

            Dim SQLtransaction As SQLite.SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                If IsNew Then
                    SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO sets (", _
                     "ListTitle, HasNfo, NfoPath, HasPoster, PosterPath, HasFanart, ", _
                     "FanartPath, HasBanner, BannerPath, HasLandscape, LandscapePath, ", _
                     "HasDiscArt, DiscArtPath, HasClearLogo, ClearLogoPath, HasClearArt, ", _
                     "ClearArtPath, TMDBColID, Plot, SetName, New, Mark, Lock", _
                     ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM sets;")
                Else
                    SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO sets (", _
                     "idSet, ListTitle, HasNfo, NfoPath, HasPoster, PosterPath, HasFanart, ", _
                     "FanartPath, HasBanner, BannerPath, HasLandscape, LandscapePath, ", _
                     "HasDiscArt, DiscArtPath, HasClearLogo, ClearLogoPath, HasClearArt, ", _
                     "ClearArtPath, TMDBColID, Plot, SetName, New, Mark, Lock", _
                     ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM sets;")
                    Dim parMovieSetID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMovieSetID", DbType.Int32, 0, "idSet")
                    parMovieSetID.Value = _moviesetDB.ID
                End If
                Dim parListTitle As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parListTitle", DbType.String, 0, "ListTitle")
                Dim parHasNfo As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasInfo", DbType.Boolean, 0, "HasNfo")
                Dim parNfoPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parNfoPath", DbType.String, 0, "NfoPath")
                Dim parHasPoster As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasPoster", DbType.Boolean, 0, "HasPoster")
                Dim parPosterPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPosterPath", DbType.String, 0, "PosterPath")
                Dim parHasFanart As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasFanart", DbType.Boolean, 0, "HasFanart")
                Dim parFanartPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parFanartPath", DbType.String, 0, "FanartPath")
                Dim parHasBanner As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasBanner", DbType.Boolean, 0, "HasBanner")
                Dim parBannerPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parBannerPath", DbType.String, 0, "BannerPath")
                Dim parHasLandscape As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasLandscape", DbType.Boolean, 0, "HasLandscape")
                Dim parLandscapePath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLandscapePath", DbType.String, 0, "LandscapePath")
                Dim parHasDiscArt As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasDiscArt", DbType.Boolean, 0, "HasDiscArt")
                Dim parDiscArtPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parDiscArtPath", DbType.String, 0, "DiscArtPath")
                Dim parHasClearLogo As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasClearLogo", DbType.Boolean, 0, "HasClearLogo")
                Dim parClearLogoPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parClearLogoPath", DbType.String, 0, "ClearLogoPath")
                Dim parHasClearArt As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasClearArt", DbType.Boolean, 0, "HasClearArt")
                Dim parClearArtPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parClearArtPath", DbType.String, 0, "ClearArtPath")
                Dim parTMDBColID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTMDBColID", DbType.String, 0, "TMDBColID")
                Dim parPlot As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPlot", DbType.String, 0, "Plot")
                Dim parSetName As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSetName", DbType.String, 0, "SetName")
                Dim parNew As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parNew", DbType.Boolean, 0, "New")
                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "Mark")
                Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "Lock")

                ' First let's save it to NFO, even because we will need the NFO path
                'If ToNfo AndAlso Not String.IsNullOrEmpty(_movieDB.Movie.IMDBID) Then NFO.SaveMovieToNFO(_movieDB)
                'Why do we need IMDB to save to NFO?
                If ToNfo Then NFO.SaveMovieSetToNFO(_moviesetDB)

                parBannerPath.Value = _moviesetDB.BannerPath
                parClearArtPath.Value = _moviesetDB.ClearArtPath
                parClearLogoPath.Value = _moviesetDB.ClearLogoPath
                parDiscArtPath.Value = _moviesetDB.DiscArtPath
                parFanartPath.Value = _moviesetDB.FanartPath
                parLandscapePath.Value = _moviesetDB.LandscapePath
                parNfoPath.Value = _moviesetDB.NfoPath
                parPosterPath.Value = _moviesetDB.PosterPath

                parHasBanner.Value = Not String.IsNullOrEmpty(_moviesetDB.BannerPath)
                parHasClearArt.Value = Not String.IsNullOrEmpty(_moviesetDB.ClearArtPath)
                parHasClearLogo.Value = Not String.IsNullOrEmpty(_moviesetDB.ClearLogoPath)
                parHasDiscArt.Value = Not String.IsNullOrEmpty(_moviesetDB.DiscArtPath)
                parHasFanart.Value = Not String.IsNullOrEmpty(_moviesetDB.FanartPath)
                parHasLandscape.Value = Not String.IsNullOrEmpty(_moviesetDB.LandscapePath)
                parHasNfo.Value = Not String.IsNullOrEmpty(_moviesetDB.NfoPath)
                parHasPoster.Value = Not String.IsNullOrEmpty(_moviesetDB.PosterPath)

                parNew.Value = IsNew
                parMark.Value = _moviesetDB.IsMark
                parLock.Value = _moviesetDB.IsLock

                parListTitle.Value = _moviesetDB.ListTitle
                parSetName.Value = _moviesetDB.MovieSet.Title
                parTMDBColID.Value = _moviesetDB.MovieSet.ID
                parPlot.Value = _moviesetDB.MovieSet.Plot

                If IsNew Then
                    Using rdrMovieSet As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        If rdrMovieSet.Read Then
                            _moviesetDB.ID = Convert.ToInt64(rdrMovieSet(0))
                        Else
                            logger.Error("Something very wrong here: SaveMovieSetToDB", _moviesetDB.ToString, "Error")
                            _moviesetDB.ListTitle = "SETERROR"
                            Return _moviesetDB
                        End If
                    End Using
                Else
                    SQLcommand.ExecuteNonQuery()
                End If
            End Using

            If withMovies Then
                Dim MoviesInSet As New List(Of MovieInSet)

                'get all movies linked to this MovieSet
                Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand.CommandText = String.Concat("SELECT MovieID, SetID, SetOrder FROM MoviesSets ", _
                                                           "WHERE SetID = ", _moviesetDB.ID, ";")
                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        While SQLreader.Read
                            Dim movie As New Structures.DBMovie
                            Dim movieToSave As New MovieInSet
                            If Not DBNull.Value.Equals(SQLreader("MovieID")) Then movie = LoadMovieFromDB(Convert.ToInt64(SQLreader("MovieID")))
                            movieToSave.DBMovie = movie
                            If Not DBNull.Value.Equals(SQLreader("SetOrder")) Then movieToSave.Order = If(Not String.IsNullOrEmpty(SQLreader("SetOrder").ToString), CInt(SQLreader("SetOrder").ToString), 0)
                            MoviesInSet.Add(movieToSave)
                        End While
                    End Using
                End Using

                'write new movie NFOs
                If MoviesInSet.Count > 0 Then
                    For Each tMovie In MoviesInSet
                        tMovie.DBMovie.Movie.AddSet(_moviesetDB.ID, _moviesetDB.MovieSet.Title, tMovie.Order, _moviesetDB.MovieSet.ID)
                        Master.DB.SaveMovieToDB(tMovie.DBMovie, False, BatchMode, True)
                    Next
                End If
            End If

            If Not BatchMode Then SQLtransaction.Commit()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return _moviesetDB
    End Function

    ''' <summary>
    ''' Saves all episode information from a Structures.DBTV object to the database
    ''' </summary>
    ''' <param name="_TVEpDB">Structures.DBTV object to save to the database</param>
    ''' <param name="IsNew">Is this a new episode (not already present in database)?</param>
    ''' <param name="WithSeason">If <c>True</c>, also save season information</param>
    ''' <param name="BatchMode">Is the function already part of a transaction?</param>
    ''' <param name="ToNfo">Save the information to an nfo file?</param>
    Public Sub SaveTVEpToDB(ByVal _TVEpDB As Structures.DBTV, ByVal IsNew As Boolean, ByVal WithSeason As Boolean, Optional ByVal BatchMode As Boolean = False, Optional ByVal ToNfo As Boolean = False)
        'TODO Must add parameter checking. Needs thought to ensure calling routines are not broken if exception thrown. 
        'TODO Break this method into smaller chunks. Too important to be this complex

        Dim SQLtransaction As SQLite.SQLiteTransaction = Nothing
        Dim PathID As Long = -1
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        'Copy fileinfo duration over to runtime var for xbmc to pick up episode runtime.
        NFO.LoadTVEpDuration(_TVEpDB)

        'delete so it will remove if there is a "missing" episode entry already. Only "missing" episodes must be deleted.
        Using SQLCommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLCommand.CommandText = String.Concat("DELETE FROM episode WHERE idShow = ", _TVEpDB.ShowID, " AND Episode = ", _TVEpDB.TVEp.Episode, " AND Season = ", _TVEpDB.TVEp.Season, " AND Missing = 1;")
            SQLCommand.ExecuteNonQuery()
        End Using

        If Not String.IsNullOrEmpty(_TVEpDB.Filename) Then
            If _TVEpDB.FilenameID > -1 Then
                Using SQLpathcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLpathcommand.CommandText = String.Concat("INSERT OR REPLACE INTO TVEpPaths (ID, TVEpPath) VALUES (?,?);")

                    Dim parID As SQLite.SQLiteParameter = SQLpathcommand.Parameters.Add("parID", DbType.UInt64, 0, "ID")
                    Dim parTVEpPath As SQLite.SQLiteParameter = SQLpathcommand.Parameters.Add("parTVEpPath", DbType.String, 0, "TVEpPath")
                    parID.Value = _TVEpDB.FilenameID
                    parTVEpPath.Value = _TVEpDB.Filename
                    PathID = _TVEpDB.FilenameID
                    SQLpathcommand.ExecuteNonQuery()
                End Using
            Else
                Using SQLpathcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLpathcommand.CommandText = "SELECT ID FROM TVEpPaths WHERE TVEpPath = (?);"

                    Dim parPath As SQLite.SQLiteParameter = SQLpathcommand.Parameters.Add("parPath", DbType.String, 0, "TVEpPath")
                    parPath.Value = _TVEpDB.Filename

                    Using SQLreader As SQLite.SQLiteDataReader = SQLpathcommand.ExecuteReader
                        If SQLreader.HasRows Then
                            SQLreader.Read()
                            PathID = Convert.ToInt64(SQLreader("ID"))
                        Else
                            Using SQLpcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                SQLpcommand.CommandText = String.Concat("INSERT INTO TVEpPaths (", _
                                     "TVEpPath) VALUES (?); SELECT LAST_INSERT_ROWID() FROM TVEpPaths;")
                                Dim parEpPath As SQLite.SQLiteParameter = SQLpcommand.Parameters.Add("parEpPath", DbType.String, 0, "TVEpPath")
                                parEpPath.Value = _TVEpDB.Filename

                                PathID = Convert.ToInt64(SQLpcommand.ExecuteScalar)
                            End Using
                        End If
                    End Using
                End Using
            End If
        End If

        Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
            If IsNew Then
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO episode (", _
                 "idShow, HasPoster, HasFanart, HasNfo, New, Mark, TVEpPathID, Source, Lock, Title, Season, Episode,", _
                 "Rating, Plot, Aired, Director, Credits, PosterPath, FanartPath, NfoPath, NeedsSave, Missing, Playcount,", _
                 "HasWatched, DisplaySeason, DisplayEpisode, DateAdded, Runtime, Votes, VideoSource, HasSub", _
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM episode;")

            Else
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO episode (", _
                 "idEpisode, idShow, HasPoster, HasFanart, HasNfo, New, Mark, TVEpPathID, Source, Lock, Title, Season, Episode,", _
                 "Rating, Plot, Aired, Director, Credits, PosterPath, FanartPath, NfoPath, NeedsSave, Missing, Playcount,", _
                 "HasWatched, DisplaySeason, DisplayEpisode, DateAdded, Runtime, Votes, VideoSource, HasSub", _
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM episode;")

                Dim parTVEpID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTVEpID", DbType.UInt64, 0, "idEpisode")
                parTVEpID.Value = _TVEpDB.EpID
            End If

            Dim parTVShowID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTVShowID", DbType.UInt64, 0, "idShow")
            Dim parHasPoster As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasPoster", DbType.Boolean, 0, "HasPoster")
            Dim parHasFanart As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasFanart", DbType.Boolean, 0, "HasFanart")
            Dim parHasNfo As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasInfo", DbType.Boolean, 0, "HasNfo")
            Dim parNew As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parNew", DbType.Boolean, 0, "new")
            Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
            Dim parTVEpPathID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTVEpPathID", DbType.Int64, 0, "TVEpPathID")
            Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.String, 0, "source")
            Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "lock")
            Dim parTitle As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTitle", DbType.String, 0, "Title")
            Dim parSeason As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSeason", DbType.String, 0, "Season")
            Dim parEpisode As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parEpisode", DbType.String, 0, "Episode")
            Dim parRating As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parRating", DbType.String, 0, "Rating")
            Dim parPlot As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPlot", DbType.String, 0, "Plot")
            Dim parAired As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parAired", DbType.String, 0, "Aired")
            Dim parDirector As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parDirector", DbType.String, 0, "Director")
            Dim parCredits As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parCredits", DbType.String, 0, "Credits")
            Dim parPosterPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPosterPath", DbType.String, 0, "PosterPath")
            Dim parFanartPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parFanartPath", DbType.String, 0, "FanartPath")
            Dim parNfoPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parNfoPath", DbType.String, 0, "NfoPath")
            Dim parNeedsSave As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parNeedsSave", DbType.Boolean, 0, "NeedsSave")
            Dim parTVEpMissing As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTVEpMissing", DbType.Boolean, 0, "Missing")
            Dim parPlaycount As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPlaycount", DbType.String, 0, "Playcount")
            Dim parHasWatched As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasWatched", DbType.Boolean, 0, "HasWatched")
            Dim parDisplaySeason As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parDisplaySeason", DbType.String, 0, "DisplaySeason")
            Dim parDisplayEpisode As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parDisplayEpisode", DbType.String, 0, "DisplayEpisode")
            Dim parDateAdded As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parDateAdded", DbType.Int32, 0, "DateAdded")
            Dim parRuntime As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parRuntime", DbType.String, 0, "Runtime")
            Dim parVotes As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parVotes", DbType.String, 0, "Votes")
            Dim parVideoSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parVideoSource", DbType.String, 0, "VideoSource")
            Dim parHasSub As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasSub", DbType.Boolean, 0, "HasSub")

            Try
                If Not Master.eSettings.GeneralDateAddedIgnoreNFO AndAlso Not String.IsNullOrEmpty(_TVEpDB.TVEp.DateAdded) Then
                    Dim DateTimeAdded As DateTime = DateTime.ParseExact(_TVEpDB.TVEp.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    parDateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    Select Case Master.eSettings.GeneralDateTime
                        Case Enums.DateTime.Now
                            parDateAdded.Value = If(IsNew, Functions.ConvertToUnixTimestamp(Now), _TVEpDB.DateAdded)
                        Case Enums.DateTime.mtime
                            Dim mtime As Date = System.IO.File.GetLastWriteTime(_TVEpDB.Filename)
                            If mtime.Year > 1601 Then
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                Dim ctime As Date = System.IO.File.GetCreationTime(_TVEpDB.Filename)
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                        Case Enums.DateTime.Newer
                            Dim mtime As Date = System.IO.File.GetLastWriteTime(_TVEpDB.Filename)
                            Dim ctime As Date = System.IO.File.GetCreationTime(_TVEpDB.Filename)
                            If mtime > ctime Then
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                    End Select
                End If
                _TVEpDB.TVEp.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(parDateAdded.Value)).ToString("yyyy-MM-d HH:mm:ss")
            Catch ex As Exception
                parDateAdded.Value = If(IsNew, Functions.ConvertToUnixTimestamp(Now), _TVEpDB.DateAdded)
                _TVEpDB.TVEp.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(parDateAdded.Value)).ToString("yyyy-MM-d HH:mm:ss")
            End Try

            ' First let's save it to NFO, even because we will need the NFO path
            If ToNfo Then NFO.SaveTVEpToNFO(_TVEpDB)

            parTVShowID.Value = _TVEpDB.ShowID
            parPosterPath.Value = _TVEpDB.EpPosterPath
            parFanartPath.Value = _TVEpDB.EpFanartPath
            parNfoPath.Value = _TVEpDB.EpNfoPath
            parHasPoster.Value = Not String.IsNullOrEmpty(_TVEpDB.EpPosterPath)
            parHasFanart.Value = Not String.IsNullOrEmpty(_TVEpDB.EpFanartPath)
            parHasNfo.Value = Not String.IsNullOrEmpty(_TVEpDB.EpNfoPath)
            parHasSub.Value = _TVEpDB.EpSubtitles.Count > 0 OrElse _TVEpDB.TVEp.FileInfo.StreamDetails.Subtitle.Count > 0
            parHasWatched.Value = Not String.IsNullOrEmpty(_TVEpDB.TVEp.Playcount) AndAlso Not _TVEpDB.TVEp.Playcount = "0"
            parNew.Value = IsNew
            parMark.Value = _TVEpDB.IsMarkEp
            parTVEpPathID.Value = PathID
            parLock.Value = _TVEpDB.IsLockEp
            parSource.Value = _TVEpDB.Source
            parNeedsSave.Value = _TVEpDB.EpNeedsSave
            parTVEpMissing.Value = PathID < 0
            parVideoSource.Value = _TVEpDB.VideoSource

            With _TVEpDB.TVEp
                parTitle.Value = .Title
                parSeason.Value = .Season
                parEpisode.Value = .Episode
                parRating.Value = .Rating
                parPlot.Value = .Plot
                parAired.Value = .Aired
                parDirector.Value = .Director
                parCredits.Value = .OldCredits
                parPlaycount.Value = .Playcount
                parRuntime.Value = .Runtime
                parVotes.Value = .Votes
                If .displaySEset Then
                    parDisplaySeason.Value = .DisplaySeason
                    parDisplayEpisode.Value = .DisplayEpisode
                End If
            End With

            If IsNew Then
                If Master.eSettings.TVGeneralMarkNewEpisodes Then
                    parMark.Value = True
                Else
                    parMark.Value = False
                End If
                Using rdrTVEp As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    If rdrTVEp.Read Then
                        _TVEpDB.EpID = Convert.ToInt64(rdrTVEp(0))
                    Else
                        logger.Error("Something very wrong here: SaveTVEpToDB", _TVEpDB.ToString, "Error")
                        _TVEpDB.EpID = -1
                        Exit Sub
                    End If
                End Using
            Else
                SQLcommand.ExecuteNonQuery()
            End If

            If Not _TVEpDB.EpID = -1 Then

                'Actors
                Using SQLcommand_actorlinkepisode As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_actorlinkepisode.CommandText = String.Concat("DELETE FROM actorlinkepisode WHERE idEpisode = ", _TVEpDB.EpID, ";")
                    SQLcommand_actorlinkepisode.ExecuteNonQuery()

                    For Each actor As MediaContainers.Person In _TVEpDB.TVEp.Actors
                        If actor.ID > -1 Then
                            Using SQLcommand_actor As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                SQLcommand_actor.CommandText = String.Concat("INSERT OR REPLACE INTO actors (idActor, strActor, strThumb) VALUES (?,?,?);")

                                Dim par_actor_idActor As SQLite.SQLiteParameter = SQLcommand_actor.Parameters.Add("par_actor_idActor", DbType.UInt64, 0, "idActor")
                                Dim par_actor_strActor As SQLite.SQLiteParameter = SQLcommand_actor.Parameters.Add("par_actor_strActor", DbType.String, 0, "strActor")
                                Dim par_actor_strThumb As SQLite.SQLiteParameter = SQLcommand_actor.Parameters.Add("par_actor_strThumb", DbType.String, 0, "strThumb")
                                par_actor_idActor.Value = actor.ID
                                par_actor_strActor.Value = actor.Name
                                par_actor_strThumb.Value = actor.Thumb
                                SQLcommand_actor.ExecuteNonQuery()
                            End Using
                        Else
                            Using SQLcommand_actor As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                SQLcommand_actor.CommandText = "SELECT idActor FROM actors WHERE strActor = (?);"

                                Dim par_actor_strActor As SQLite.SQLiteParameter = SQLcommand_actor.Parameters.Add("par_actor_strActor", DbType.String, 0, "strActor")
                                Dim par_actor_strThumb As SQLite.SQLiteParameter = SQLcommand_actor.Parameters.Add("par_actor_strThumb", DbType.String, 0, "strThumb")
                                par_actor_strActor.Value = actor.Name
                                par_actor_strThumb.Value = actor.Thumb

                                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand_actor.ExecuteReader
                                    If SQLreader.HasRows Then
                                        SQLreader.Read()
                                        actor.ID = Convert.ToInt64(SQLreader("idActor"))
                                    Else
                                        Using SQLpcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                            SQLpcommand.CommandText = String.Concat("INSERT INTO actors (", _
                                                 "strActor, strThumb) VALUES (?,?); SELECT LAST_INSERT_ROWID() FROM actors;")
                                            par_actor_strActor = SQLcommand_actor.Parameters.Add("par_actor_strActor", DbType.String, 0, "strActor")
                                            par_actor_strThumb = SQLcommand_actor.Parameters.Add("par_actor_strThumb", DbType.String, 0, "strThumb")
                                            par_actor_strActor.Value = actor.Name
                                            par_actor_strThumb.Value = actor.Thumb

                                            actor.ID = Convert.ToInt64(SQLpcommand.ExecuteScalar)
                                        End Using
                                    End If
                                End Using
                            End Using
                        End If

                        Using SQLcommandTVEpActors As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                            SQLcommandTVEpActors.CommandText = String.Concat("INSERT OR REPLACE INTO actorlinkepisode (idActor, idEpisode, strRole, iOrder) VALUES (?,?,?,?);")
                            Dim par_actorlinkepisode_idActor As SQLite.SQLiteParameter = SQLcommandTVEpActors.Parameters.Add("par_actorlinkepisode_idActor", DbType.UInt64, 0, "idActor")
                            Dim par_actorlinkepisode_idEpisode As SQLite.SQLiteParameter = SQLcommandTVEpActors.Parameters.Add("par_actorlinkepisode_idEpisode", DbType.UInt64, 0, "idEpisode")
                            Dim par_actorlinkepisode_strRole As SQLite.SQLiteParameter = SQLcommandTVEpActors.Parameters.Add("par_actorlinkepisode_strRole", DbType.String, 0, "strRole")
                            Dim par_actorlinkepisode_iOrder As SQLite.SQLiteParameter = SQLcommandTVEpActors.Parameters.Add("par_actorlinkepisode_iOrder", DbType.UInt64, 0, "iOrder")
                            par_actorlinkepisode_idActor.Value = actor.ID
                            par_actorlinkepisode_idEpisode.Value = _TVEpDB.EpID
                            par_actorlinkepisode_strRole.Value = actor.Role
                            'par_actorlinkepisode_iOrder.Value = actor.
                            SQLcommandTVEpActors.ExecuteNonQuery()
                        End Using
                    Next
                End Using

                'Using SQLcommandActor As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                '    SQLcommandActor.CommandText = String.Concat("DELETE FROM actorlinkepisode WHERE idEpisode = ", _TVEpDB.EpID, ";")
                '    SQLcommandActor.ExecuteNonQuery()

                '    SQLcommandActor.CommandText = "INSERT OR REPLACE INTO actor (strActor, strThumb) VALUES (?,?)"
                '    Dim parActorName As SQLite.SQLiteParameter = SQLcommandActor.Parameters.Add("parActorName", DbType.String, 0, "strActor")
                '    Dim parActorThumb As SQLite.SQLiteParameter = SQLcommandActor.Parameters.Add("parActorThumb", DbType.String, 0, "strThumb")
                '    For Each actor As MediaContainers.Person In _TVEpDB.TVEp.Actors
                '        parActorName.Value = actor.Name
                '        parActorThumb.Value = actor.Thumb
                '        SQLcommandActor.ExecuteNonQuery()
                '        Using SQLcommandTVEpActors As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                '            SQLcommandTVEpActors.CommandText = String.Concat("INSERT OR REPLACE INTO actorlinkepisode (idActor, idEpisode, strRole) VALUES (?,?,?);")
                '            Dim parTVEpActorsEpID As SQLite.SQLiteParameter = SQLcommandTVEpActors.Parameters.Add("parTVEpActorsEpID", DbType.UInt64, 0, "idActor")
                '            Dim parTVEpActorsEpID As SQLite.SQLiteParameter = SQLcommandTVEpActors.Parameters.Add("parTVEpActorsEpID", DbType.UInt64, 0, "idEpisode")
                '            Dim parTVEpActorsActorRole As SQLite.SQLiteParameter = SQLcommandTVEpActors.Parameters.Add("parTVEpActorsActorRole", DbType.String, 0, "strRole")
                '            parTVEpActorsEpID.Value = _TVEpDB.EpID
                '            parTVEpActorsActorRole.Value = actor.Role
                '            SQLcommandTVEpActors.ExecuteNonQuery()
                '        End Using
                '    Next
                'End Using
                Using SQLcommandTVVStreams As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandTVVStreams.CommandText = String.Concat("DELETE FROM TVVStreams WHERE TVEpID = ", _TVEpDB.EpID, ";")
                    SQLcommandTVVStreams.ExecuteNonQuery()
                    SQLcommandTVVStreams.CommandText = String.Concat("INSERT OR REPLACE INTO TVVStreams (", _
                       "TVEpID, StreamID, Video_Width, Video_Height, Video_Codec, Video_Duration, Video_ScanType, Video_AspectDisplayRatio,", _
                       "Video_Language, Video_LongLanguage, Video_Bitrate, Video_MultiViewCount, Video_EncodedSettings, Video_MultiViewLayout, ", _
                       "Video_StereoMode) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);")

                    Dim parVideo_EpID As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_EpID", DbType.UInt64, 0, "TVEpID")
                    Dim parVideo_StreamID As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_StreamID", DbType.UInt64, 0, "StreamID")
                    Dim parVideo_Width As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_Width", DbType.String, 0, "Video_Width")
                    Dim parVideo_Height As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_Height", DbType.String, 0, "Video_Height")
                    Dim parVideo_Codec As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_Codec", DbType.String, 0, "Video_Codec")
                    Dim parVideo_Duration As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_Duration", DbType.String, 0, "Video_Duration")
                    Dim parVideo_ScanType As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_ScanType", DbType.String, 0, "Video_ScanType")
                    Dim parVideo_AspectDisplayRatio As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_AspectDisplayRatio", DbType.String, 0, "Video_AspectDisplayRatio")
                    Dim parVideo_Language As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_Language", DbType.String, 0, "Video_Language")
                    Dim parVideo_LongLanguage As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_LongLanguage", DbType.String, 0, "Video_LongLanguage")
                    Dim parVideo_Bitrate As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_Bitrate", DbType.String, 0, "Video_Bitrate")
                    Dim parVideo_MultiViewCount As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_MultiViewCount", DbType.String, 0, "Video_MultiViewCount")
                    Dim parVideo_EncodedSettings As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_EncodedSettings", DbType.String, 0, "Video_EncodedSettings")
                    Dim parVideo_MultiViewLayout As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_MultiViewLayout", DbType.String, 0, "Video_MultiViewLayout")
                    Dim parVideo_StereoMode As SQLite.SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_StereoMode", DbType.String, 0, "Video_StereoMode")

                    For i As Integer = 0 To _TVEpDB.TVEp.FileInfo.StreamDetails.Video.Count - 1
                        parVideo_EpID.Value = _TVEpDB.EpID
                        parVideo_StreamID.Value = i
                        parVideo_Width.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Video(i).Width
                        parVideo_Height.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Video(i).Height
                        parVideo_Codec.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Video(i).Codec
                        parVideo_Duration.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Video(i).Duration
                        parVideo_ScanType.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Video(i).Scantype
                        parVideo_AspectDisplayRatio.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Video(i).Aspect
                        parVideo_Language.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Video(i).Language
                        parVideo_LongLanguage.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Video(i).LongLanguage
                        parVideo_Bitrate.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Video(i).Bitrate
                        parVideo_MultiViewCount.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Video(i).MultiViewCount
                        parVideo_MultiViewLayout.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Video(i).MultiViewLayout
                        parVideo_EncodedSettings.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Video(i).EncodedSettings
                        parVideo_StereoMode.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Video(i).StereoMode

                        SQLcommandTVVStreams.ExecuteNonQuery()
                    Next
                End Using
                Using SQLcommandTVAStreams As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandTVAStreams.CommandText = String.Concat("DELETE FROM TVAStreams WHERE TVEpID = ", _TVEpDB.EpID, ";")
                    SQLcommandTVAStreams.ExecuteNonQuery()
                    SQLcommandTVAStreams.CommandText = String.Concat("INSERT OR REPLACE INTO TVAStreams (", _
                       "TVEpID, StreamID, Audio_Language, Audio_LongLanguage, Audio_Codec, Audio_Channel, Audio_Bitrate", _
                       ") VALUES (?,?,?,?,?,?,?);")

                    Dim parAudio_EpID As SQLite.SQLiteParameter = SQLcommandTVAStreams.Parameters.Add("parAudio_EpID", DbType.UInt64, 0, "TVEpID")
                    Dim parAudio_StreamID As SQLite.SQLiteParameter = SQLcommandTVAStreams.Parameters.Add("parAudio_StreamID", DbType.UInt64, 0, "StreamID")
                    Dim parAudio_Language As SQLite.SQLiteParameter = SQLcommandTVAStreams.Parameters.Add("parAudio_Language", DbType.String, 0, "Audio_Language")
                    Dim parAudio_LongLanguage As SQLite.SQLiteParameter = SQLcommandTVAStreams.Parameters.Add("parAudio_LongLanguage", DbType.String, 0, "Audio_LongLanguage")
                    Dim parAudio_Codec As SQLite.SQLiteParameter = SQLcommandTVAStreams.Parameters.Add("parAudio_Codec", DbType.String, 0, "Audio_Codec")
                    Dim parAudio_Channel As SQLite.SQLiteParameter = SQLcommandTVAStreams.Parameters.Add("parAudio_Channel", DbType.String, 0, "Audio_Channel")
                    Dim parAudio_Bitrate As SQLite.SQLiteParameter = SQLcommandTVAStreams.Parameters.Add("parAudio_Bitrate", DbType.String, 0, "Audio_Bitrate")

                    For i As Integer = 0 To _TVEpDB.TVEp.FileInfo.StreamDetails.Audio.Count - 1
                        parAudio_EpID.Value = _TVEpDB.EpID
                        parAudio_StreamID.Value = i
                        parAudio_Language.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Audio(i).Language
                        parAudio_LongLanguage.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Audio(i).LongLanguage
                        parAudio_Codec.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Audio(i).Codec
                        parAudio_Channel.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Audio(i).Channels
                        parAudio_Bitrate.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Audio(i).Bitrate

                        SQLcommandTVAStreams.ExecuteNonQuery()
                    Next
                End Using

                'subtitles
                Using SQLcommandTVSubs As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandTVSubs.CommandText = String.Concat("DELETE FROM TVSubs WHERE TVEpID = ", _TVEpDB.EpID, ";")
                    SQLcommandTVSubs.ExecuteNonQuery()

                    SQLcommandTVSubs.CommandText = String.Concat("INSERT OR REPLACE INTO TVSubs (", _
                       "TVEpID, StreamID, Subs_Language, Subs_LongLanguage, Subs_Type, Subs_Path, Subs_Forced", _
                       ") VALUES (?,?,?,?,?,?,?);")
                    Dim parSubs_EpID As SQLite.SQLiteParameter = SQLcommandTVSubs.Parameters.Add("parSubs_EpID", DbType.UInt64, 0, "TVEpID")
                    Dim parSubs_StreamID As SQLite.SQLiteParameter = SQLcommandTVSubs.Parameters.Add("parSubs_StreamID", DbType.UInt64, 0, "StreamID")
                    Dim parSubs_Language As SQLite.SQLiteParameter = SQLcommandTVSubs.Parameters.Add("parSubs_Language", DbType.String, 0, "Subs_Language")
                    Dim parSubs_LongLanguage As SQLite.SQLiteParameter = SQLcommandTVSubs.Parameters.Add("parSubs_LongLanguage", DbType.String, 0, "Subs_LongLanguage")
                    Dim parSubs_Type As SQLite.SQLiteParameter = SQLcommandTVSubs.Parameters.Add("parSubs_Type", DbType.String, 0, "Subs_Type")
                    Dim parSubs_Path As SQLite.SQLiteParameter = SQLcommandTVSubs.Parameters.Add("parSubs_Path", DbType.String, 0, "Subs_Path")
                    Dim parSubs_Forced As SQLite.SQLiteParameter = SQLcommandTVSubs.Parameters.Add("parSubs_Forced", DbType.Boolean, 0, "Subs_Forced")
                    Dim iID As Integer = 0
                    'embedded subtitles
                    For i As Integer = 0 To _TVEpDB.TVEp.FileInfo.StreamDetails.Subtitle.Count - 1
                        parSubs_EpID.Value = _TVEpDB.EpID
                        parSubs_StreamID.Value = iID
                        parSubs_Language.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Subtitle(i).Language
                        parSubs_LongLanguage.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Subtitle(i).LongLanguage
                        parSubs_Type.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Subtitle(i).SubsType
                        parSubs_Path.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Subtitle(i).SubsPath
                        parSubs_Forced.Value = _TVEpDB.TVEp.FileInfo.StreamDetails.Subtitle(i).SubsForced
                        SQLcommandTVSubs.ExecuteNonQuery()
                        iID += 1
                    Next
                    'external subtitles
                    For i As Integer = 0 To _TVEpDB.EpSubtitles.Count - 1
                        parSubs_EpID.Value = _TVEpDB.EpID
                        parSubs_StreamID.Value = iID
                        parSubs_Language.Value = _TVEpDB.EpSubtitles(i).Language
                        parSubs_LongLanguage.Value = _TVEpDB.EpSubtitles(i).LongLanguage
                        parSubs_Type.Value = _TVEpDB.EpSubtitles(i).SubsType
                        parSubs_Path.Value = _TVEpDB.EpSubtitles(i).SubsPath
                        parSubs_Forced.Value = _TVEpDB.EpSubtitles(i).SubsForced
                        SQLcommandTVSubs.ExecuteNonQuery()
                        iID += 1
                    Next
                End Using

                If WithSeason Then SaveTVSeasonToDB(_TVEpDB, IsNew, True)
            End If
        End Using
        If Not BatchMode Then SQLtransaction.Commit()
    End Sub
    ''' <summary>
    ''' Stores information for a single season to the database
    ''' </summary>
    ''' <param name="_TVSeasonDB">Structures.DBTV representing the season to be stored.</param>
    ''' <param name="IsNew"></param>
    ''' <param name="BatchMode"></param>
    ''' <remarks>Note that this stores the season information, not the individual episodes within that season</remarks>
    Public Sub SaveTVSeasonToDB(ByRef _TVSeasonDB As Structures.DBTV, ByVal IsNew As Boolean, Optional ByVal BatchMode As Boolean = False)
        'TODO Must add parameter checking. Needs thought to ensure calling routines are not broken if exception thrown. 
        'TODO Break this method into smaller chunks. Too important to be this complex
        Try
            Dim SQLtransaction As SQLite.SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

            Using SQLcommandTVSeason As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommandTVSeason.CommandText = String.Concat("INSERT OR ", If(IsNew, "IGNORE", "REPLACE"), " INTO TVSeason (", _
                  "TVShowID, SeasonText, Season, HasPoster, HasFanart, PosterPath, FanartPath, Lock, Mark, New, HasBanner, BannerPath, HasLandscape, LandscapePath", _
                  ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?);")
                Dim parSeasonShowID As SQLite.SQLiteParameter = SQLcommandTVSeason.Parameters.Add("parSeasonShowID", DbType.UInt64, 0, "TVShowID")
                Dim parSeasonSeasonText As SQLite.SQLiteParameter = SQLcommandTVSeason.Parameters.Add("parSeasonSeasonText", DbType.String, 0, "SeasonText")
                Dim parSeasonSeason As SQLite.SQLiteParameter = SQLcommandTVSeason.Parameters.Add("parSeasonSeason", DbType.Int32, 0, "Season")
                Dim parSeasonHasPoster As SQLite.SQLiteParameter = SQLcommandTVSeason.Parameters.Add("parSeasonHasPoster", DbType.Boolean, 0, "HasPoster")
                Dim parSeasonHasFanart As SQLite.SQLiteParameter = SQLcommandTVSeason.Parameters.Add("parSeasonHasFanart", DbType.Boolean, 0, "HasFanart")
                Dim parSeasonPosterPath As SQLite.SQLiteParameter = SQLcommandTVSeason.Parameters.Add("parSeasonPosterPath", DbType.String, 0, "PosterPath")
                Dim parSeasonFanartPath As SQLite.SQLiteParameter = SQLcommandTVSeason.Parameters.Add("parSeasonFanartPath", DbType.String, 0, "FanartPath")
                Dim parSeasonLock As SQLite.SQLiteParameter = SQLcommandTVSeason.Parameters.Add("parSeasonLock", DbType.Boolean, 0, "Lock")
                Dim parSeasonMark As SQLite.SQLiteParameter = SQLcommandTVSeason.Parameters.Add("parSeasonMark", DbType.Boolean, 0, "Mark")
                Dim parSeasonNew As SQLite.SQLiteParameter = SQLcommandTVSeason.Parameters.Add("parSeasonNew", DbType.Boolean, 0, "New")
                Dim parSeasonHasBanner As SQLite.SQLiteParameter = SQLcommandTVSeason.Parameters.Add("parSeasonHasBanner", DbType.Boolean, 0, "HasBanner")
                Dim parSeasonBannerPath As SQLite.SQLiteParameter = SQLcommandTVSeason.Parameters.Add("parSeasonBannerPath", DbType.String, 0, "BannerPath")
                Dim parSeasonHasLandscape As SQLite.SQLiteParameter = SQLcommandTVSeason.Parameters.Add("parSeasonHasLandscape", DbType.Boolean, 0, "HasLandscape")
                Dim parSeasonLandscapePath As SQLite.SQLiteParameter = SQLcommandTVSeason.Parameters.Add("parSeasonLandscapePath", DbType.String, 0, "LandscapePath")
                parSeasonShowID.Value = _TVSeasonDB.ShowID
                parSeasonSeasonText.Value = StringUtils.FormatSeasonText(_TVSeasonDB.TVEp.Season)
                parSeasonSeason.Value = _TVSeasonDB.TVEp.Season
                parSeasonHasPoster.Value = Not String.IsNullOrEmpty(_TVSeasonDB.SeasonPosterPath)
                parSeasonHasBanner.Value = Not String.IsNullOrEmpty(_TVSeasonDB.SeasonBannerPath)
                parSeasonHasFanart.Value = Not String.IsNullOrEmpty(_TVSeasonDB.SeasonFanartPath)
                parSeasonHasLandscape.Value = Not String.IsNullOrEmpty(_TVSeasonDB.SeasonLandscapePath)
                parSeasonPosterPath.Value = _TVSeasonDB.SeasonPosterPath
                parSeasonBannerPath.Value = _TVSeasonDB.SeasonBannerPath
                parSeasonFanartPath.Value = _TVSeasonDB.SeasonFanartPath
                parSeasonLandscapePath.Value = _TVSeasonDB.SeasonLandscapePath
                parSeasonLock.Value = _TVSeasonDB.IsLockSeason
                parSeasonMark.Value = _TVSeasonDB.IsMarkSeason
                parSeasonNew.Value = IsNew
                SQLcommandTVSeason.ExecuteNonQuery()
            End Using

            If Not BatchMode Then SQLtransaction.Commit()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Saves all show information from a Structures.DBTV object to the database
    ''' </summary>
    ''' <param name="_TVShowDB">Structures.DBTV object to save to the database</param>
    ''' <param name="IsNew">Is this a new show (not already present in database)?</param>
    ''' <param name="BatchMode">Is the function already part of a transaction?</param>
    ''' <param name="ToNfo">Save the information to an nfo file?</param>
    Public Sub SaveTVShowToDB(ByRef _TVShowDB As Structures.DBTV, ByVal IsNew As Boolean, Optional ByVal BatchMode As Boolean = False, Optional ByVal ToNfo As Boolean = False)
        Try
            Dim SQLtransaction As SQLite.SQLiteTransaction = Nothing

            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                If IsNew Then
                    SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO tvshow (", _
                     "TVShowPath, HasPoster, HasFanart, HasNfo, New, Mark, Source, TVDB, Lock, ListTitle, EpisodeGuide, ", _
                     "Plot, Genre, Premiered, Studio, MPAA, Rating, PosterPath, FanartPath, NfoPath, NeedsSave, Language, Ordering, ", _
                     "HasBanner, BannerPath, HasLandscape, LandscapePath, Status, HasTheme, ThemePath, HasCharacterArt, CharacterArtPath, ", _
                     "HasClearLogo, ClearLogoPath, HasClearArt, ClearArtPath, HasEFanarts, EFanartsPath, Runtime, Title, Votes, EpisodeSorting", _
                     ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM tvshow;")
                Else
                    SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO tvshow (", _
                     "idShow, TVShowPath, HasPoster, HasFanart, HasNfo, New, Mark, Source, TVDB, Lock, ListTitle, EpisodeGuide, ", _
                     "Plot, Genre, Premiered, Studio, MPAA, Rating, PosterPath, FanartPath, NfoPath, NeedsSave, Language, Ordering, ", _
                     "HasBanner, BannerPath, HasLandscape, LandscapePath, Status, HasTheme, ThemePath, HasCharacterArt, CharacterArtPath, ", _
                     "HasClearLogo, ClearLogoPath, HasClearArt, ClearArtPath, HasEFanarts, EFanartsPath, Runtime, Title, Votes, EpisodeSorting", _
                     ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM tvshow;")
                    Dim parTVShowID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTVShowID", DbType.UInt64, 0, "idShow")
                    parTVShowID.Value = _TVShowDB.ShowID
                End If

                Dim parTVShowPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTVShowPath", DbType.String, 0, "TVShowPath")
                Dim parHasPoster As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasPoster", DbType.Boolean, 0, "HasPoster")
                Dim parHasFanart As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasFanart", DbType.Boolean, 0, "HasFanart")
                Dim parHasNfo As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasNfo", DbType.Boolean, 0, "HasNfo")
                Dim parNew As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parNew", DbType.Boolean, 0, "new")
                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
                Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.String, 0, "source")
                Dim parTVDB As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTVDB", DbType.String, 0, "TVDB")
                Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "lock")
                Dim parListTitle As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parListTitle", DbType.String, 0, "ListTitle")
                Dim parEpisodeGuide As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parEpisodeGuide", DbType.String, 0, "EpisodeGuide")
                Dim parPlot As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPlot", DbType.String, 0, "Plot")
                Dim parGenre As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parGenre", DbType.String, 0, "Genre")
                Dim parPremiered As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPremiered", DbType.String, 0, "Premiered")
                Dim parStudio As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parStudio", DbType.String, 0, "Studio")
                Dim parMPAA As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMPAA", DbType.String, 0, "MPAA")
                Dim parRating As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parRating", DbType.String, 0, "Rating")
                Dim parPosterPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parPosterPath", DbType.String, 0, "PosterPath")
                Dim parFanartPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parFanartPath", DbType.String, 0, "FanartPath")
                Dim parNfoPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parNfoPath", DbType.String, 0, "NfoPath")
                Dim parNeedsSave As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parNeedsSave", DbType.Boolean, 0, "NeedsSave")
                Dim parLanguage As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLanguage", DbType.String, 0, "Language")
                Dim parOrdering As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parOrdering", DbType.Int16, 0, "Ordering")
                Dim parHasBanner As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasBanner", DbType.Boolean, 0, "HasBanner")
                Dim parBannerPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parBannerPath", DbType.String, 0, "BannerPath")
                Dim parHasLandscape As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasLandscape", DbType.Boolean, 0, "HasLandscape")
                Dim parLandscapePath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLandscapePath", DbType.String, 0, "LandscapePath")
                Dim parStatus As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parStatus", DbType.String, 0, "Status")
                Dim parHasTheme As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasTheme", DbType.Boolean, 0, "HasTheme")
                Dim parThemePath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parThemePath", DbType.String, 0, "ThemePath")
                Dim parHasCharacterArt As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasCharacterArt", DbType.Boolean, 0, "HasCharacterArt")
                Dim parCharacterArtPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parCharacterArtPath", DbType.String, 0, "CharacterArtPath")
                Dim parHasClearLogo As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasClearLogo", DbType.Boolean, 0, "HasClearLogo")
                Dim parClearLogoPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parClearLogoPath", DbType.String, 0, "ClearLogoPath")
                Dim parHasClearArt As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasClearArt", DbType.Boolean, 0, "HasClearArt")
                Dim parClearArtPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parClearArtPath", DbType.String, 0, "ClearArtPath")
                Dim parHasEFanarts As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parHasEFanarts", DbType.Boolean, 0, "HasEFanarts")
                Dim parEFanartsPath As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parEFanartsPath", DbType.String, 0, "EFanartsPath")
                Dim parRuntime As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parRuntime", DbType.String, 0, "Runtime")
                Dim parTitle As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parTitle", DbType.String, 0, "Title")
                Dim parVotes As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parVotes", DbType.String, 0, "Votes")
                Dim parEpisodeSorting As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parEpisodeSorting", DbType.Int16, 0, "EpisodeSorting")

                With _TVShowDB.TVShow
                    parTVDB.Value = .ID
                    parTitle.Value = .Title
                    parEpisodeGuide.Value = .EpisodeGuide.URL
                    parPlot.Value = .Plot
                    parGenre.Value = .Genre
                    parPremiered.Value = .Premiered
                    parStudio.Value = .Studio
                    parMPAA.Value = .MPAA
                    parRating.Value = .Rating
                    parStatus.Value = .Status
                    parRuntime.Value = .Runtime
                    parVotes.Value = .Votes
                End With

                ' First let's save it to NFO, even because we will need the NFO path
                If ToNfo Then NFO.SaveTVShowToNFO(_TVShowDB)

                parBannerPath.Value = _TVShowDB.ShowBannerPath
                parCharacterArtPath.Value = _TVShowDB.ShowCharacterArtPath
                parClearArtPath.Value = _TVShowDB.ShowClearArtPath
                parClearLogoPath.Value = _TVShowDB.ShowClearLogoPath
                parEFanartsPath.Value = _TVShowDB.ShowEFanartsPath
                parFanartPath.Value = _TVShowDB.ShowFanartPath
                parLandscapePath.Value = _TVShowDB.ShowLandscapePath
                parNfoPath.Value = _TVShowDB.ShowNfoPath
                parPosterPath.Value = _TVShowDB.ShowPosterPath
                parTVShowPath.Value = _TVShowDB.ShowPath
                parThemePath.Value = _TVShowDB.ShowThemePath
                parHasBanner.Value = Not String.IsNullOrEmpty(_TVShowDB.ShowBannerPath)
                parHasCharacterArt.Value = Not String.IsNullOrEmpty(_TVShowDB.ShowCharacterArtPath)
                parHasClearArt.Value = Not String.IsNullOrEmpty(_TVShowDB.ShowClearArtPath)
                parHasClearLogo.Value = Not String.IsNullOrEmpty(_TVShowDB.ShowClearLogoPath)
                parHasEFanarts.Value = Not String.IsNullOrEmpty(_TVShowDB.ShowEFanartsPath)
                parHasFanart.Value = Not String.IsNullOrEmpty(_TVShowDB.ShowFanartPath)
                parHasLandscape.Value = Not String.IsNullOrEmpty(_TVShowDB.ShowLandscapePath)
                parHasNfo.Value = Not String.IsNullOrEmpty(_TVShowDB.ShowNfoPath)
                parHasPoster.Value = Not String.IsNullOrEmpty(_TVShowDB.ShowPosterPath)
                parHasTheme.Value = Not String.IsNullOrEmpty(_TVShowDB.ShowThemePath)

                parNew.Value = IsNew
                parListTitle.Value = _TVShowDB.ListTitle
                parMark.Value = _TVShowDB.IsMarkShow
                parLock.Value = _TVShowDB.IsLockShow
                parSource.Value = _TVShowDB.Source
                parNeedsSave.Value = _TVShowDB.ShowNeedsSave
                parLanguage.Value = If(String.IsNullOrEmpty(_TVShowDB.ShowLanguage), Master.DB.GetTVSourceLanguage(_TVShowDB.Source), _TVShowDB.ShowLanguage)
                parOrdering.Value = _TVShowDB.Ordering
                parEpisodeSorting.Value = _TVShowDB.EpisodeSorting



                If IsNew Then
                    If Master.eSettings.TVGeneralMarkNewShows Then
                        parMark.Value = True
                    Else
                        parMark.Value = False
                    End If
                    Using rdrTVShow As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        If rdrTVShow.Read Then
                            _TVShowDB.ShowID = Convert.ToInt64(rdrTVShow(0))
                        Else
                            logger.Error("Something very wrong here: SaveTVShowToDB", _TVShowDB.ToString, "Error")
                            _TVShowDB.ShowID = -1
                            Exit Sub
                        End If
                    End Using
                Else
                    SQLcommand.ExecuteNonQuery()
                End If

                If Not _TVShowDB.ShowID = -1 Then
                    Using SQLcommandActor As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                        SQLcommandActor.CommandText = String.Concat("DELETE FROM TVShowActors WHERE TVShowID = ", _TVShowDB.EpID, ";")
                        SQLcommandActor.ExecuteNonQuery()

                        SQLcommandActor.CommandText = String.Concat("INSERT OR REPLACE INTO Actors (Name,thumb) VALUES (?,?)")
                        Dim parActorName As SQLite.SQLiteParameter = SQLcommandActor.Parameters.Add("parActorName", DbType.String, 0, "Name")
                        Dim parActorThumb As SQLite.SQLiteParameter = SQLcommandActor.Parameters.Add("parActorThumb", DbType.String, 0, "thumb")
                        For Each actor As MediaContainers.Person In _TVShowDB.TVShow.Actors
                            parActorName.Value = actor.Name
                            parActorThumb.Value = actor.Thumb
                            SQLcommandActor.ExecuteNonQuery()
                            Using SQLcommandTVShowActors As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                                SQLcommandTVShowActors.CommandText = String.Concat("INSERT OR REPLACE INTO TVShowActors (TVShowID,ActorName,Role) VALUES (?,?,?);")
                                Dim parTVShowActorsShowID As SQLite.SQLiteParameter = SQLcommandTVShowActors.Parameters.Add("parTVShowActorsEpID", DbType.UInt64, 0, "TVShowID")
                                Dim parTVShowActorsActorName As SQLite.SQLiteParameter = SQLcommandTVShowActors.Parameters.Add("parTVShowActorsActorName", DbType.String, 0, "ActorName")
                                Dim parTVShowActorsActorRole As SQLite.SQLiteParameter = SQLcommandTVShowActors.Parameters.Add("parTVShowActorsActorRole", DbType.String, 0, "Role")
                                parTVShowActorsShowID.Value = _TVShowDB.ShowID
                                parTVShowActorsActorName.Value = actor.Name
                                parTVShowActorsActorRole.Value = actor.Role
                                SQLcommandTVShowActors.ExecuteNonQuery()
                            End Using
                        Next
                    End Using
                End If
            End Using
            If Not BatchMode Then SQLtransaction.Commit()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load TV Sources from the DB. This populates the Master.TVSources list of TV Sources
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadTVSourcesFromDB()
        Master.TVSources.Clear()
        Try
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = "SELECT ID, Name, path, LastScan, Language, Ordering, Exclude, EpisodeSorting FROM TVSources;"
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        Try ' Parsing database entry may fail. If it does, log the error and ignore the entry but continue processing
                            Dim tvsource As New Structures.TVSource
                            tvsource.id = SQLreader("ID").ToString
                            tvsource.Name = SQLreader("Name").ToString
                            tvsource.Path = SQLreader("Path").ToString
                            tvsource.Language = SQLreader("Language").ToString
                            tvsource.Ordering = DirectCast(Convert.ToInt32(SQLreader("Ordering")), Enums.Ordering)
                            tvsource.Exclude = Convert.ToBoolean(SQLreader("Exclude"))
                            tvsource.EpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("EpisodeSorting")), Enums.EpisodeSorting)
                            Master.TVSources.Add(tvsource)
                        Catch ex As Exception
                            logger.Error(New StackFrame().GetMethod().Name, ex)
                        End Try
                    End While
                End Using
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load Movie Sources from the DB. This populates the Master.MovieSources list of movie Sources
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadMovieSourcesFromDB()
        Master.MovieSources.Clear()
        Try
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = "SELECT ID, Name, Path, Recursive, Foldername, Single, LastScan, Exclude FROM Sources;"
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        Try ' Parsing database entry may fail. If it does, log the error and ignore the entry but continue processing
                            Dim msource As New Structures.MovieSource
                            msource.id = SQLreader("ID").ToString
                            msource.Name = SQLreader("Name").ToString
                            msource.Path = SQLreader("Path").ToString
                            msource.Recursive = Convert.ToBoolean(SQLreader("Recursive"))
                            msource.UseFolderName = Convert.ToBoolean(SQLreader("Foldername"))
                            msource.IsSingle = Convert.ToBoolean(SQLreader("Single"))
                            msource.Exclude = Convert.ToBoolean(SQLreader("Exclude"))
                            Master.MovieSources.Add(msource)
                        Catch ex As Exception
                            logger.Error(New StackFrame().GetMethod().Name, ex)
                        End Try
                    End While
                End Using
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load Movie Sources from the DB. This populates the Master.MovieSources list of movie Sources
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadExcludeDirsFromDB()
        Master.ExcludeDirs.Clear()
        Try
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = "SELECT Dirname FROM ExcludeDir;"
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        Try ' Parsing database entry may fail. If it does, log the error and ignore the entry but continue processing
                            Dim eDir As String = String.Empty
                            eDir = SQLreader("Dirname").ToString
                            Master.ExcludeDirs.Add(eDir)
                        Catch ex As Exception
                            logger.Error(New StackFrame().GetMethod().Name, ex)
                        End Try
                    End While
                End Using
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Retrieve movie paths.
    ''' </summary>
    ''' <param name="source">If supplied, paths returned will be restricted to the given source. No validation is done
    ''' on this source to ensure it is valid. If it is not valid, expect the returned paths to be empty.</param>
    ''' <returns>List of String movie paths</returns>
    ''' <remarks></remarks>
    Public Function GetMoviePathsBySource(Optional ByVal source As String = "") As List(Of String)
        Dim Paths As New List(Of String)
        Try
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("SELECT MoviePath, Source FROM movie {0};", If(source = String.Empty, String.Empty, String.Format("INNER JOIN Sources ON movie.Source=Sources.Name Where Sources.Path=""{0}""", source)))
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        Try ' Parsing database entry may fail. If it does, log the error and ignore the entry but continue processing
                            Paths.Add(SQLreader("MoviePath").ToString)
                        Catch ex As Exception
                            logger.Error(New StackFrame().GetMethod().Name, ex)
                        End Try
                    End While
                End Using
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return Paths
    End Function


    '''''''''''''''''''''''''''''''''''''''''''
    'Protected Sub ConnectJobsDB()
    '    If Not IsNothing(_myvideosDBConn) Then
    '        Return
    '        'Throw New InvalidOperationException("A database connection is already open, can't open another.")
    '    End If

    '    Dim jobsDBFile As String = Path.Combine(Functions.AppPath, "JobLogs.emm")
    '    Dim isNew As Boolean = (Not File.Exists(jobsDBFile))

    '    Try
    '        _jobsDBConn = New SQLiteConnection(String.Format(_connStringTemplate, jobsDBFile))
    '        _jobsDBConn.Open()
    '    Catch ex As Exception
    '        logger.Error(GetType(Database),ex.ToString, _
    '                                    ex.StackTrace, _
    '                                    "Unable to open media database connection.")
    '    End Try

    '    If isNew Then
    '        Dim sqlCommand As String = My.Resources.JobsDatabaseSQL_v1
    '        Using transaction As SQLite.SQLiteTransaction = _jobsDBConn.BeginTransaction()
    '            Using command As SQLite.SQLiteCommand = _jobsDBConn.CreateCommand()
    '                command.CommandText = sqlCommand
    '                command.ExecuteNonQuery()
    '            End Using
    '            transaction.Commit()
    '        End Using
    '    End If
    'End Sub

    ''' <summary>
    ''' Verify whether the given Addon is installed
    ''' </summary>
    ''' <param name="AddonID">The AddonID to be verified.</param>
    ''' <returns>Version of the addon, if it is installed, or zero (0) otherwise</returns>
    ''' <remarks></remarks>
    Public Function IsAddonInstalled(ByVal AddonID As Integer) As Single
        If AddonID < 0 Then
            logger.Error(New StackFrame().GetMethod().Name, Environment.StackTrace, "Invalid AddonID: {0}" & AddonID)
            'Throw New ArgumentOutOfRangeException("AddonID", "Must be a positive integer")
        End If

        Try
            Using SQLCommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLCommand.CommandText = String.Concat("SELECT Version FROM Addons WHERE AddonID = ", AddonID, ";")
                Dim tES As Object = SQLCommand.ExecuteScalar
                If Not IsNothing(tES) Then
                    Dim tSing As Single = 0
                    If Single.TryParse(tES.ToString, tSing) Then
                        Return tSing
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return 0
    End Function
    ''' <summary>
    ''' Removes the referenced Addon from the list of installed Addons
    ''' </summary>
    ''' <param name="AddonID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UninstallAddon(ByVal AddonID As Integer) As Boolean
        If AddonID < 0 Then
            logger.Error(New StackFrame().GetMethod().Name, Environment.StackTrace, "Invalid AddonID: {0}" & AddonID)
            'Throw New ArgumentOutOfRangeException("AddonID", "Must be a positive integer")
        End If

        Dim needRestart As Boolean = False
        Try
            Dim _cmds As Containers.InstallCommands = Containers.InstallCommands.Load(Path.Combine(Functions.AppPath, "InstallTasks.xml"))
            Using SQLtransaction As SQLite.SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Using SQLCommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLCommand.CommandText = String.Concat("SELECT FilePath FROM AddonFiles WHERE AddonID = ", AddonID, ";")
                    Using SQLReader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader
                        While SQLReader.Read
                            Try
                                File.Delete(SQLReader("FilePath").ToString)
                            Catch
                                _cmds.noTransaction.Add(New Containers.CommandsNoTransactionCommand With {.type = "FILE.Delete", .execute = SQLReader("FilePath").ToString})
                                needRestart = True
                            End Try
                        End While
                        If needRestart Then _cmds.Save(Path.Combine(Functions.AppPath, "InstallTasks.xml"))
                    End Using
                    SQLCommand.CommandText = String.Concat("DELETE FROM Addons WHERE AddonID = ", AddonID, ";")
                    SQLCommand.ExecuteNonQuery()
                    SQLCommand.CommandText = String.Concat("DELETE FROM AddonFiles WHERE AddonID = ", AddonID, ";")
                    SQLCommand.ExecuteNonQuery()
                End Using
                SQLtransaction.Commit()
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return Not needRestart
    End Function

    ''' <summary>
    ''' Saves/installs the supplied Addon to the database
    ''' </summary>
    ''' <param name="Addon">Addon to be saved</param>
    ''' <remarks></remarks>
    Public Sub SaveAddonToDB(ByVal Addon As Containers.Addon)
        'TODO Need to add validation on Addon.ID, especially if it is passed in the parameter
        If Addon Is Nothing Then
            logger.Error(New StackFrame().GetMethod().Name, Environment.StackTrace, "Invalid AddonID: empty")
        End If
        Try
            Using SQLtransaction As SQLite.SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Using SQLCommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLCommand.CommandText = String.Concat("INSERT OR REPLACE INTO Addons (", _
                      "AddonID, Version) VALUES (?,?);")
                    Dim parAddonID As SQLite.SQLiteParameter = SQLCommand.Parameters.Add("parAddonID", DbType.Int32, 0, "AddonID")
                    Dim parVersion As SQLite.SQLiteParameter = SQLCommand.Parameters.Add("parVersion", DbType.String, 0, "Version")

                    parAddonID.Value = Addon.ID
                    parVersion.Value = Addon.Version.ToString

                    SQLCommand.ExecuteNonQuery()

                    SQLCommand.CommandText = String.Concat("DELETE FROM AddonFiles WHERE AddonID = ", Addon.ID, ";")
                    SQLCommand.ExecuteNonQuery()

                    Using SQLFileCommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                        SQLFileCommand.CommandText = String.Concat("INSERT INTO AddonFiles (AddonID, FilePath) VALUES (?,?);")
                        Dim parFileAddonID As SQLite.SQLiteParameter = SQLFileCommand.Parameters.Add("parFileAddonID", DbType.Int32, 0, "AddonID")
                        Dim parFilePath As SQLite.SQLiteParameter = SQLFileCommand.Parameters.Add("parFilePath", DbType.String, 0, "FilePath")
                        parFileAddonID.Value = Addon.ID
                        For Each fFile As KeyValuePair(Of String, String) In Addon.Files
                            parFilePath.Value = Path.Combine(Functions.AppPath, fFile.Key.Replace("/", Path.DirectorySeparatorChar))
                            SQLFileCommand.ExecuteNonQuery()
                        Next
                    End Using
                End Using
                SQLtransaction.Commit()
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Save the PlayCount Tag for watched movie  into Ember database /NFO if not already set
    ''' </summary>
    ''' <param name="WatchedMovieData">The watched movie as Keypair</param>
    ''' <remarks>    
    ''' cocotus 2013/02 Trakt.tv syncing - Movies
    ''' not using loop here, only do one movie a time (call function repeatedly!)! -> only deliver keypair instead of whole dictionary
    ''' Old-> Public Sub SaveMoviePlayCountInDatabase(ByVal myWatchedMovies As Dictionary(Of String, KeyValuePair(Of String, String)))
    ''' </remarks>
    Public Sub SaveMoviePlayCountInDatabase(ByVal WatchedMovieData As KeyValuePair(Of String, KeyValuePair(Of String, Integer)))
        Try
            Dim PlaycountStored As Boolean = True
            Dim _movieDB As New Structures.DBMovie
            _movieDB.Movie = New MediaContainers.Movie
            ''not using Loop here, only do one movie a time (call function repeatedly!)!
            '  For Each watchedMovieData In myWatchedMovies

            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                'TODO: This statement (directly filter IMDB) doesn't work ?! This is bad, cause right now I have to get all movies and search through them!
                '         SQLcommand.CommandText = String.Concat("SELECT * FROM movies WHERE imdb = ", watchedMovieIMDBID.Value, ";")
                SQLcommand.CommandText = String.Concat("SELECT idMovie, MoviePath, Type, ListTitle, HasPoster, HasFanart, HasNfo, HasTrailer, HasSub, HasEThumbs, New, Mark, ", _
                                                       "Source, Imdb, Lock, Title, OriginalTitle, Year, Rating, Votes, MPAA, Top250, Country, Outline, Plot, Tagline, ", _
                                                       "Certification, Genre, Studio, Runtime, ReleaseDate, Director, Credits, Playcount, HasWatched, Trailer, PosterPath, ", _
                                                       "FanartPath, EThumbsPath, NfoPath, TrailerPath, SubPath, FanartURL, UseFolder, OutOfTolerance, VideoSource, NeedsSave, ", _
                                                       "SortTitle, DateAdded, HasEFanarts, EFanartsPath, HasBanner, BannerPath, HasLandscape, LandscapePath, HasTheme, ThemePath FROM movie;")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        If Not DBNull.Value.Equals(SQLreader("IMDB")) Then
                            If SQLreader("IMDB").ToString.Equals(WatchedMovieData.Value.Key) Then
                                _movieDB.ID = CLng(SQLreader("idMovie").ToString)
                                _movieDB.Movie.IMDBID = SQLreader("IMDB").ToString
                                If Not DBNull.Value.Equals(SQLreader("MoviePath")) Then _movieDB.Filename = SQLreader("MoviePath").ToString
                                If Not DBNull.Value.Equals(SQLreader("Playcount")) Then _movieDB.Movie.PlayCount = SQLreader("Playcount").ToString
                                If DBNull.Value.Equals(SQLreader("Playcount")) Or SQLreader("Playcount").Equals("0") Or SQLreader("Playcount").Equals("") Or Not (SQLreader("Playcount").Equals(WatchedMovieData.Value.Value.ToString)) Then

                                    PlaycountStored = False
                                End If
                                Exit While
                            End If
                        End If
                    End While
                End Using
            End Using

            If PlaycountStored = False Then
                Using SQLTrans As SQLite.SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                    Using SQLUpdatecommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                        Dim parPlaycount As SQLite.SQLiteParameter = SQLUpdatecommand.Parameters.Add("parPlaycount", DbType.String, 0, "Playcount")
                        SQLUpdatecommand.CommandText = String.Concat("UPDATE movie SET Playcount = (?) WHERE idMovie = ", _movieDB.ID, ";")
                        parPlaycount.Value = WatchedMovieData.Value.Value.ToString
                        SQLUpdatecommand.ExecuteNonQuery()
                    End Using
                    SQLTrans.Commit()
                End Using
                'Save to NFO!
                Dim _movieSavetoNFO As New Structures.DBMovie
                _movieSavetoNFO = Master.DB.LoadMovieFromDB(_movieDB.ID)
                Master.DB.SaveMovieToDB(_movieSavetoNFO, False, False, True)
                'create .watched files
                If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
                    For Each a In FileUtils.GetFilenameList.Movie(_movieSavetoNFO.Filename, False, Enums.ModType_Movie.WatchedFile)
                        If Not File.Exists(a) Then
                            Dim fs As FileStream = File.Create(a)
                            fs.Close()
                        End If
                    Next
                End If
            End If
            ''not using Loop here, only do one movie a time (call function repeatedly!)!
            '    Next
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Savethe PlayCount Tag for watched episode into Ember database /NFO if not already set
    ''' </summary>
    ''' <param name="TVDBID">TVDBID for TV Show identification</param>
    ''' <param name="Season">Season Number</param>
    ''' <param name="episode">Episode Number</param>
    ''' <remarks>
    ''' cocotus 2013/03 Trakt.tv syncing - Episodes
    ''' not using loop here, only do one episode a time (call function repeatedly!)!
    '''</remarks>
    Public Sub SaveEpisodePlayCountInDatabase(ByVal TVDBID As String, ByVal Season As String, ByVal episode As String)
        'TODO PlaycountStored is set to False if db value is 0 or not set. What conditions might cause that to happen? If DB Version issues, should be resolved elsewhere first!
        Try
            Dim PlaycountStored As Boolean = True
            Dim _TVEpDB As New Structures.DBTV
            Dim tempTVDBID As String = ""
            Dim tempPlaycount As String = ""

            'First get the internal ID of TVSHOW using the TVDBID info
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT idShow, Title, HasPoster, HasFanart, HasNfo, New, Mark, TVShowPath, Source, TVDB, Lock, EpisodeGuide, Plot, ", _
                                                       "Genre, Premiered, Studio, MPAA, Rating, PosterPath, FanartPath, NfoPath, NeedsSave, Language, Ordering FROM tvshow WHERE tvdb = ", TVDBID, ";")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        If Not DBNull.Value.Equals(SQLreader("id")) Then
                            tempTVDBID = SQLreader("id").ToString
                            Exit While
                        End If
                    End While
                End Using
            End Using

            'No ID --> TV Show doesn't Exist in Ember --> Exit no updates!
            If String.IsNullOrEmpty(tempTVDBID) Then Exit Sub
            'Now we search episodes of the found TV Show
            Using SQLcommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT idEpisode, idShow, Episode, Title, HasPoster, HasFanart, HasNfo, New, Mark, TVEpPathID, Source, Lock, Season, ", _
                                                       "Rating, Plot, Aired, Director, Credits, PosterPath, FanartPath, NfoPath, NeedsSave, Missing, Playcount, HasWatched, ", _
                                                       "DisplaySeason, DisplayEpisode FROM episode WHERE idShow = ", tempTVDBID, ";")

                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        If SQLreader("Season").ToString.Equals(Season) AndAlso SQLreader("Episode").ToString.Equals(episode) Then
                            _TVEpDB.EpID = CLng(SQLreader("id").ToString)
                            'Only if playcount is not set we update
                            If DBNull.Value.Equals(SQLreader("Playcount")) Or SQLreader("Playcount").Equals("0") Or SQLreader("Playcount").Equals("") Then
                                PlaycountStored = False
                            Else
                                tempPlaycount = SQLreader("playcount").ToString
                            End If
                            Exit While
                        End If
                    End While
                End Using
            End Using
            'Updating Playcount in database
            If PlaycountStored = False Then
                Using SQLTrans As SQLite.SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                    Using SQLUpdatecommand As SQLite.SQLiteCommand = _myvideosDBConn.CreateCommand()
                        Dim parPlaycount As SQLite.SQLiteParameter = SQLUpdatecommand.Parameters.Add("parPlaycount", DbType.String, 0, "Playcount")
                        If Not String.IsNullOrEmpty(CStr(_TVEpDB.EpID)) Then
                            SQLUpdatecommand.CommandText = String.Concat("UPDATE episode SET Playcount = (?) WHERE idEpisode = ", _TVEpDB.EpID, ";")
                            parPlaycount.Value = "1"
                            SQLUpdatecommand.ExecuteNonQuery()
                        Else
                            Exit Sub
                        End If
                    End Using
                    SQLTrans.Commit()
                End Using
                'Save to NFO!
                Dim _episodeSavetoNFO As New Structures.DBTV
                _episodeSavetoNFO = Master.DB.LoadTVEpFromDB(_TVEpDB.EpID, True)
                SaveTVEpToDB(_episodeSavetoNFO, False, False, False, True)

            End If

        Catch ex As Exception

        End Try
    End Sub

#End Region 'Methods

#Region "Nested Types"
    ''' <summary>
    ''' Class representing a media Source
    ''' </summary>
    ''' <remarks></remarks>
    Private Class SourceHolder

#Region "Fields"

        Private _name As String
        Private _path As String
        Private _recursive As Boolean   ' Scanned recursively? I.e. should sub-directories be scanned as well?
        Private _single As Boolean      ' Does this path contains a single video/media?

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"
        ''' <summary>
        ''' Does this path contains a single video/media?
        ''' </summary>
        ''' <value>Whether this folder contains a single video</value>
        Public Property isSingle() As Boolean
            Get
                Return Me._single
            End Get
            Set(ByVal value As Boolean)
                Me._single = value
            End Set
        End Property
        ''' <summary>
        ''' Name given to this source by the user
        ''' </summary>
        ''' <value>Name given to this source by the user</value>
        Public Property Name() As String
            Get
                Return Me._name
            End Get
            Set(ByVal value As String)
                Me._name = value
            End Set
        End Property
        Public Property Path() As String
            Get
                Return Me._path
            End Get
            Set(ByVal value As String)
                Me._path = value
            End Set
        End Property
        ''' <summary>
        ''' Scanned recursively? I.e. should sub-directories be scanned as well?
        ''' </summary>
        ''' <value>Whether sub-directories should be scanned</value>
        Public Property Recursive() As Boolean
            Get
                Return Me._recursive
            End Get
            Set(ByVal value As Boolean)
                Me._recursive = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._name = String.Empty
            Me._path = String.Empty
            Me._recursive = False
            Me._single = False
        End Sub

#End Region 'Methods

    End Class

    Friend Class MovieInSet
        Implements IComparable(Of MovieInSet)

#Region "Fields"

        Private _dbmovie As Structures.DBMovie
        Private _id As Long
        Private _listtitle As String
        Private _order As Integer

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property DBMovie() As Structures.DBMovie
            Get
                Return Me._dbmovie
            End Get
            Set(ByVal value As Structures.DBMovie)
                Me._dbmovie = value
            End Set
        End Property

        Public Property ID() As Long
            Get
                Return Me._id
            End Get
            Set(ByVal value As Long)
                Me._id = value
            End Set
        End Property

        Public Property ListTitle() As String
            Get
                Return Me._listtitle
            End Get
            Set(ByVal value As String)
                Me._listtitle = value
            End Set
        End Property

        Public Property Order() As Integer
            Get
                Return Me._order
            End Get
            Set(ByVal value As Integer)
                Me._order = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._dbmovie = New Structures.DBMovie
            Me._id = -1
            Me._order = 0
            Me._listtitle = String.Empty
        End Sub

        Public Function CompareTo(ByVal other As MovieInSet) As Integer Implements IComparable(Of MovieInSet).CompareTo
            Return (Me.Order).CompareTo(other.Order)
        End Function

#End Region 'Methods

    End Class


#End Region 'Nested Types

End Class