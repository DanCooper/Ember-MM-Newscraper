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

Imports System.Windows.Forms
Imports System.IO
Imports System.Xml.Serialization
Imports System.Data.SQLite
Imports NLog
Imports System.Text.RegularExpressions

''' <summary>
''' Class defining and implementing the interface to the database
''' </summary>
''' <remarks></remarks>
Public Class Database

#Region "Fields"
    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwPatchDB As New System.ComponentModel.BackgroundWorker

    ReadOnly _connStringTemplate As String = "Data Source=""{0}"";Version=3;Compress=True"
    Protected _myvideosDBConn As SQLiteConnection
    ' NOTE: This will use another DB because: can grow alot, Don't want to stress Media DB with this stuff
    'Protected _jobsDBConn As SQLiteConnection

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))

#End Region 'Events

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
    ''' add or update actor
    ''' </summary>
    ''' <param name="strActor">actor name</param>
    ''' <param name="thumbURL">thumb URL</param>
    ''' <param name="thumb">local thumb path</param>
    ''' <param name="strIMDB">IMDB ID of actor</param>
    ''' <param name="strTMDB">TMDB ID of actor</param>
    ''' <param name="isActor"><c>True</c> if adding an actor, <c>False</c> if adding a Creator, Director, Writer or something else without ID's and images to refresh if already exist in actors table</param>
    ''' <returns><c>ID</c> of actor in actors table</returns>
    ''' <remarks></remarks>
    Private Function AddActor(ByVal strActor As String, ByVal thumbURL As String, ByVal thumb As String, ByVal strIMDB As String, ByVal strTMDB As String, ByVal isActor As Boolean) As Long
        Dim doesExist As Boolean = False
        Dim ID As Long = -1

        Using SQLcommand_select_actors As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_select_actors.CommandText = String.Format("SELECT idActor FROM actors WHERE strActor LIKE ?", strActor)
            Dim par_select_actors_strActor As SQLiteParameter = SQLcommand_select_actors.Parameters.Add("par_select_actors_strActor", DbType.String, 0, "strActor")
            par_select_actors_strActor.Value = strActor
            Using SQLreader As SQLiteDataReader = SQLcommand_select_actors.ExecuteReader()
                While SQLreader.Read
                    doesExist = True
                    ID = CInt(SQLreader("idActor"))
                    Exit While
                End While
            End Using
        End Using

        If Not doesExist Then
            Using SQLcommand_insert_actors As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand_insert_actors.CommandText = "INSERT INTO actors (idActor, strActor, strThumb, strIMDB, strTMDB) VALUES (NULL,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM actors;"
                Dim par_insert_actors_strActor As SQLiteParameter = SQLcommand_insert_actors.Parameters.Add("par_actors_strActor", DbType.String, 0, "strActor")
                Dim par_insert_actors_strThumb As SQLiteParameter = SQLcommand_insert_actors.Parameters.Add("par_actors_strThumb", DbType.String, 0, "strThumb")
                Dim par_insert_actors_strIMDB As SQLiteParameter = SQLcommand_insert_actors.Parameters.Add("par_actors_strIMDB", DbType.String, 0, "strIMDB")
                Dim par_insert_actors_strTMDB As SQLiteParameter = SQLcommand_insert_actors.Parameters.Add("par_actors_strTMDB", DbType.String, 0, "strTMDB")
                par_insert_actors_strActor.Value = strActor
                par_insert_actors_strThumb.Value = thumbURL
                par_insert_actors_strIMDB.Value = strIMDB
                par_insert_actors_strTMDB.Value = strTMDB
                ID = CInt(SQLcommand_insert_actors.ExecuteScalar())
            End Using
        ElseIf isActor Then
            Using SQLcommand_update_actors As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand_update_actors.CommandText = String.Format("UPDATE actors SET strThumb=?, strIMDB=?, strTMDB=? WHERE idActor={0}", ID)
                Dim par_update_actors_strThumb As SQLiteParameter = SQLcommand_update_actors.Parameters.Add("par_actors_strThumb", DbType.String, 0, "strThumb")
                Dim par_update_actors_strIMDB As SQLiteParameter = SQLcommand_update_actors.Parameters.Add("par_actors_strIMDB", DbType.String, 0, "strIMDB")
                Dim par_update_actors_strTMDB As SQLiteParameter = SQLcommand_update_actors.Parameters.Add("par_actors_strTMDB", DbType.String, 0, "strTMDB")
                par_update_actors_strThumb.Value = thumbURL
                par_update_actors_strIMDB.Value = strIMDB
                par_update_actors_strTMDB.Value = strTMDB
                SQLcommand_update_actors.ExecuteNonQuery()
            End Using
        End If

        If Not ID = -1 Then
            If Not String.IsNullOrEmpty(thumb) Then
                SetArtForItem(ID, "actor", "thumb", thumb)
            End If
        End If

        Return ID
    End Function

    Private Sub AddArtistToMusicVideo(ByVal idMVideo As Long, ByVal idArtist As Long)
        AddToLinkTable("artistlinkmusicvideo", "idArtist", idArtist, "idMVideo", idMVideo)
    End Sub

    Private Sub AddCast(ByVal idMedia As Long, ByVal table As String, ByVal field As String, ByVal cast As List(Of MediaContainers.Person))
        If cast Is Nothing Then Return

        Dim iOrder As Integer = 0
        For Each actor As MediaContainers.Person In cast
            Dim idActor = AddActor(actor.Name, actor.URLOriginal, actor.LocalFilePath, actor.IMDB, actor.TMDB, True)
            AddLinkToActor(table, idActor, field, idMedia, actor.Role, iOrder)
            iOrder += 1
        Next
    End Sub

    Private Sub AddCreatorToTvShow(ByVal idShow As Long, ByVal idActor As Long)
        AddToLinkTable("creatorlinktvshow", "idActor", idActor, "idShow", idShow)
    End Sub

    Private Function AddCountry(ByVal strCountry As String) As Long
        If String.IsNullOrEmpty(strCountry) Then Return -1
        Dim ID As Long = AddToTable("country", "idCountry", "strCountry", strCountry)
        LoadAllCountries()
        Return ID
    End Function

    Private Sub AddCountryToMovie(ByVal idMovie As Long, ByVal idCountry As Long)
        AddToLinkTable("countrylinkmovie", "idCountry", idCountry, "idMovie", idMovie)
    End Sub

    Private Sub AddDirectorToEpisode(ByVal idEpisode As Long, ByVal idDirector As Long)
        AddToLinkTable("directorlinkepisode", "idDirector", idDirector, "idEpisode", idEpisode)
    End Sub

    Private Sub AddCountryToTVShow(ByVal idShow As Long, ByVal idCountry As Long)
        AddToLinkTable("countrylinktvshow", "idCountry", idCountry, "idShow", idShow)
    End Sub

    Private Sub AddDirectorToMovie(ByVal idMovie As Long, ByVal idDirector As Long)
        AddToLinkTable("directorlinkmovie", "idDirector", idDirector, "idMovie", idMovie)
    End Sub

    Private Sub AddDirectorToMusicVideo(ByVal idMVideo As Long, ByVal idDirector As Long)
        AddToLinkTable("directorlinkmusicvideo", "idDirector", idDirector, "idMVideo", idMVideo)
    End Sub

    Private Sub AddDirectorToTvShow(ByVal idShow As Long, ByVal idDirector As Long)
        AddToLinkTable("directorlinktvshow", "idDirector", idDirector, "idShow", idShow)
    End Sub

    Private Function AddGenre(ByVal strGenre As String) As Long
        If String.IsNullOrEmpty(strGenre) Then Return -1
        Dim ID As Long = AddToTable("genre", "idGenre", "strGenre", strGenre)
        LoadAllGenres()
        Return ID
    End Function

    Private Sub AddGenreToMovie(ByVal idMovie As Long, ByVal idGenre As Long)
        AddToLinkTable("genrelinkmovie", "idGenre", idGenre, "idMovie", idMovie)
    End Sub

    Private Sub AddGenreToMusicVideo(ByVal idMVideo As Long, ByVal idGenre As Long)
        AddToLinkTable("genrelinkmusicvideo", "idGenre", idGenre, "idMVideo", idMVideo)
    End Sub

    Private Sub AddGenreToTvShow(ByVal idShow As Long, ByVal idGenre As Long)
        AddToLinkTable("genrelinktvshow", "idGenre", idGenre, "idShow", idShow)
    End Sub

    Private Sub AddGuestStar(ByVal idMedia As Long, ByVal table As String, ByVal field As String, ByVal cast As List(Of MediaContainers.Person))
        If cast Is Nothing Then Return

        Dim iOrder As Integer = 0
        For Each actor As MediaContainers.Person In cast
            Dim idActor = AddActor(actor.Name, actor.URLOriginal, actor.LocalFilePath, actor.IMDB, actor.TMDB, True)
            AddLinkToGuestStar(table, idActor, field, idMedia, actor.Role, iOrder)
            iOrder += 1
        Next
    End Sub
    ''' <summary>
    ''' add an actor to an actorlink* table
    ''' </summary>
    ''' <param name="table">link table name without "actorlink" prefix("episode", "movie" or "tvshow")</param>
    ''' <param name="actorID">ID of actor in table actors</param>
    ''' <param name="field">field name in <c>table</c> without "id" prefix("Episode", "Movie" or "Show")</param>
    ''' <param name="secondID">ID of <c>field</c> </param>
    ''' <param name="role">actors role</param>
    ''' <param name="order">actors order</param>
    ''' <returns><c>True</c> if the actor link has been created, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Private Function AddLinkToActor(ByVal table As String, ByVal actorID As Long, ByVal field As String,
                                    ByVal secondID As Long, ByVal role As String,
                                    ByVal order As Long) As Boolean
        Dim doesExist As Boolean = False

        Using SQLcommand_select_actorlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_select_actorlink.CommandText = String.Format("SELECT * FROM actorlink{0} WHERE idActor={1} AND id{2}={3};", table, actorID, field, secondID)
            Using SQLreader As SQLiteDataReader = SQLcommand_select_actorlink.ExecuteReader()
                While SQLreader.Read
                    doesExist = True
                    Exit While
                End While
            End Using
        End Using

        If Not doesExist Then
            Using SQLcommand_insert_actorlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand_insert_actorlink.CommandText = String.Format("INSERT INTO actorlink{0} (idActor, id{1}, strRole, iOrder) VALUES ({2},{3},?,{4})", table, field, actorID, secondID, order)
                Dim par_insert_actors_strRole As SQLiteParameter = SQLcommand_insert_actorlink.Parameters.Add("par_insert_actors_strRole", DbType.String, 0, "strRole")
                par_insert_actors_strRole.Value = role
                SQLcommand_insert_actorlink.ExecuteNonQuery()
            End Using
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' add an actor to an actorlink* table
    ''' </summary>
    ''' <param name="table">link table name without "actorlink" prefix("episode", "movie" or "tvshow")</param>
    ''' <param name="actorID">ID of actor in table actors</param>
    ''' <param name="field">field name in <c>table</c> without "id" prefix("Episode", "Movie" or "Show")</param>
    ''' <param name="secondID">ID of <c>field</c> </param>
    ''' <param name="role">actors role</param>
    ''' <param name="order">actors order</param>
    ''' <returns><c>True</c> if the actor link has been created, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Private Function AddLinkToGuestStar(ByVal table As String, ByVal actorID As Long, ByVal field As String,
                                    ByVal secondID As Long, ByVal role As String,
                                    ByVal order As Long) As Boolean
        Dim doesExist As Boolean = False

        Using SQLcommand_select_gueststarlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_select_gueststarlink.CommandText = String.Format("SELECT * FROM gueststarlink{0} WHERE idActor={1} AND id{2}={3};", table, actorID, field, secondID)
            Using SQLreader As SQLiteDataReader = SQLcommand_select_gueststarlink.ExecuteReader()
                While SQLreader.Read
                    doesExist = True
                    Exit While
                End While
            End Using
        End Using

        If Not doesExist Then
            Using SQLcommand_insert_gueststarlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand_insert_gueststarlink.CommandText = String.Format("INSERT INTO gueststarlink{0} (idActor, id{1}, strRole, iOrder) VALUES ({2},{3},?,{4})", table, field, actorID, secondID, order)
                Dim par_insert_gueststar_strRole As SQLiteParameter = SQLcommand_insert_gueststarlink.Parameters.Add("par_insert_gueststar_strRole", DbType.String, 0, "strRole")
                par_insert_gueststar_strRole.Value = role
                SQLcommand_insert_gueststarlink.ExecuteNonQuery()
            End Using
            Return True
        Else
            Return False
        End If
    End Function

    Private Function AddRating(ByVal idMedia As Long,
                               ByVal mediaType As String,
                               ByVal rating As MediaContainers.RatingDetails) As Long
        If Not idMedia = -1 AndAlso Not String.IsNullOrEmpty(mediaType) AndAlso rating.ValueSpecified Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("INSERT OR REPLACE INTO rating (media_id, media_type, rating_type, rating_max, rating, votes, isDefault) VALUES ({0},'{1}',?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM rating;",
                                                           idMedia, mediaType)
                Dim par_rating_type As SQLiteParameter = sqlCommand.Parameters.Add("par_rating_type", DbType.String, 0, "rating_type")
                Dim par_rating_max As SQLiteParameter = sqlCommand.Parameters.Add("par_rating_max", DbType.Int32, 0, "rating_max")
                Dim par_rating As SQLiteParameter = sqlCommand.Parameters.Add("par_rating", DbType.Double, 0, "rating")
                Dim par_votes As SQLiteParameter = sqlCommand.Parameters.Add("par_votes", DbType.Int32, 0, "votes")
                Dim par_isDefault As SQLiteParameter = sqlCommand.Parameters.Add("par_isDefault", DbType.Boolean, 0, "isDefault")
                par_rating_type.Value = rating.Name
                par_rating_max.Value = rating.Max
                par_rating.Value = rating.Value
                par_votes.Value = rating.Votes
                par_isDefault.Value = rating.IsDefault
                Return CLng(sqlCommand.ExecuteScalar())
            End Using
        End If
        Return -1
    End Function

    Private Function AddSet(ByVal strSet As String) As Long
        If String.IsNullOrEmpty(strSet) Then Return -1
        Return AddToTable("sets", "idSet", "strSet", strSet)
    End Function

    Private Function AddStudio(ByVal strStudio As String) As Long
        If String.IsNullOrEmpty(strStudio) Then Return -1
        Dim ID As Long = AddToTable("studio", "idStudio", "strStudio", strStudio)
        LoadAllStudios()
        Return ID
    End Function

    Private Sub AddStudioToMovie(ByVal idMovie As Long, ByVal idStudio As Long)
        AddToLinkTable("studiolinkmovie", "idStudio", idStudio, "idMovie", idMovie)
    End Sub

    Private Sub AddStudioToTvShow(ByVal idShow As Long, ByVal idStudio As Long)
        AddToLinkTable("studiolinktvshow", "idStudio", idStudio, "idShow", idShow)
    End Sub

    Private Function AddTag(ByVal strTag As String) As Long
        If String.IsNullOrEmpty(strTag) Then Return -1
        Return AddToTable("tag", "idTag", "strTag", strTag)
    End Function

    Private Function AddToLinkTable(ByVal table As String, ByVal firstField As String, ByVal firstID As Long, ByVal secondField As String, ByVal secondID As Long,
                               Optional ByVal typeField As String = "", Optional ByVal type As String = "") As Boolean
        Dim doesExist As Boolean = False

        Using SQLcommand_select As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_select.CommandText = String.Format("SELECT * FROM {0} WHERE {1}={2} AND {3}={4}", table, firstField, firstID, secondField, secondID)
            If Not String.IsNullOrEmpty(typeField) AndAlso Not String.IsNullOrEmpty(type) Then
                SQLcommand_select.CommandText = String.Concat(SQLcommand_select.CommandText, String.Format(" AND {0}='{1}'", typeField, type))
            End If
            Using SQLreader As SQLiteDataReader = SQLcommand_select.ExecuteReader()
                While SQLreader.Read
                    doesExist = True
                    Exit While
                End While
            End Using
        End Using

        If Not doesExist Then
            Using SQLcommand_insert As SQLiteCommand = _myvideosDBConn.CreateCommand()
                If String.IsNullOrEmpty(typeField) AndAlso String.IsNullOrEmpty(type) Then
                    SQLcommand_insert.CommandText = String.Format("INSERT INTO {0} ({1},{2}) VALUES ({3},{4})", table, firstField, secondField, firstID, secondID)
                Else
                    SQLcommand_insert.CommandText = String.Format("INSERT INTO {0} ({1},{2},{3}) VALUES ({4},{5},'{6}')", table, firstField, secondField, typeField, firstID, secondID, type)
                End If
                SQLcommand_insert.ExecuteNonQuery()
                Return True
            End Using
        Else
            Return False
        End If
    End Function

    Private Function AddToTable(ByVal table As String, ByVal firstField As String, ByVal secondField As String, ByVal value As String) As Long
        Dim doesExist As Boolean = False
        Dim ID As Long = -1

        Using SQLcommand_select As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_select.CommandText = String.Format("SELECT {0} FROM {1} WHERE {2} LIKE ?", firstField, table, secondField)
            Dim par_select_secondField As SQLiteParameter = SQLcommand_select.Parameters.Add("par_select_secondField", DbType.String, 0, secondField)
            par_select_secondField.Value = value
            Using SQLreader As SQLiteDataReader = SQLcommand_select.ExecuteReader()
                While SQLreader.Read
                    doesExist = True
                    ID = CInt(SQLreader(firstField))
                    Exit While
                End While
            End Using
        End Using

        If Not doesExist Then
            Using SQLcommand_insert As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand_insert.CommandText = String.Format("INSERT INTO {0} ({1}, {2}) VALUES (NULL, ?); SELECT LAST_INSERT_ROWID() FROM {0};", table, firstField, secondField)
                Dim par_insert_secondField As SQLiteParameter = SQLcommand_insert.Parameters.Add("par_insert_secondField", DbType.String, 0, secondField)
                par_insert_secondField.Value = value
                ID = CInt(SQLcommand_insert.ExecuteScalar())
            End Using
        End If

        Return ID
    End Function

    Private Sub AddTagToItem(ByVal idMedia As Long, ByVal idTag As Long, ByVal type As String)
        If String.IsNullOrEmpty(type) Then Return
        AddToLinkTable("taglinks", "idTag", idTag, "idMedia", idMedia, "media_type", type)
    End Sub

    Private Function AddUniqueID(ByVal idMedia As Long,
                                 ByVal mediaType As String,
                                 ByVal uniqueID As MediaContainers.Uniqueid) As Long
        If Not idMedia = -1 AndAlso Not String.IsNullOrEmpty(mediaType) AndAlso uniqueID.TypeSpecified AndAlso uniqueID.ValueSpecified Then
            Using sqlCommand As SQLiteCommand = _MyvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("INSERT OR REPLACE INTO uniqueid (media_id, media_type, value, type, isDefault) VALUES ({0},'{1}',?,?,?); SELECT LAST_INSERT_ROWID() FROM uniqueid;",
                                                           idMedia, mediaType)
                Dim par_value As SQLiteParameter = sqlCommand.Parameters.Add("par_value", DbType.String, 0, "value")
                Dim par_type As SQLiteParameter = sqlCommand.Parameters.Add("par_type", DbType.String, 0, "type")
                Dim par_isDefault As SQLiteParameter = sqlCommand.Parameters.Add("par_isDefault", DbType.String, 0, "isDefault")
                par_value.Value = uniqueID.Value
                par_type.Value = uniqueID.Type
                par_isDefault.Value = uniqueID.IsDefault
                Return CLng(sqlCommand.ExecuteScalar())
            End Using
        End If
        Return -1
    End Function

    Private Sub AddWriterToEpisode(ByVal idEpisode As Long, ByVal idWriter As Long)
        AddToLinkTable("writerlinkepisode", "idWriter", idWriter, "idEpisode", idEpisode)
    End Sub

    Private Sub AddWriterToMovie(ByVal idMovie As Long, ByVal idWriter As Long)
        AddToLinkTable("writerlinkmovie", "idWriter", idWriter, "idMovie", idMovie)
    End Sub

    Public Sub Change_TVEpisode(ByVal _episode As DBElement, ByVal ListOfEpisodes As List(Of MediaContainers.EpisodeDetails), Optional ByVal bBatchmode As Boolean = False)
        Dim newEpisodesList As New List(Of DBElement)

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not bBatchmode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLPCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()

            'first step: remove all existing episode informations for this file and set it to "Missing"
            Delete_TVEpisode(_episode.Filename, False, True)

            'second step: create new episode DBElements and save it to database
            For Each tEpisode As MediaContainers.EpisodeDetails In ListOfEpisodes
                Dim newEpisode As New DBElement(Enums.ContentType.TVEpisode)
                newEpisode = New DBElement(Enums.ContentType.TVEpisode)
                newEpisode = CType(_episode.CloneDeep, DBElement)
                newEpisode.FilenameID = -1
                newEpisode.ID = -1
                newEpisode.TVEpisode = tEpisode
                newEpisode.TVEpisode.FileInfo = _episode.TVEpisode.FileInfo
                newEpisode.TVEpisode.VideoSource = newEpisode.VideoSource
                Save_TVEpisode(newEpisode, True, True, True, True, False)
                newEpisodesList.Add(newEpisode)
            Next

            For Each tEpisode As DBElement In newEpisodesList
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.DuringUpdateDB_TV, Nothing, Nothing, False, tEpisode)
            Next

            For Each tEpisode As DBElement In newEpisodesList
                Save_TVEpisode(tEpisode, True, False, False, False, True, True)
            Next
        End Using
        If Not bBatchmode Then SQLtransaction.Commit()
    End Sub
    ''' <summary>
    ''' Iterates db entries to check if the paths to the movie or TV files are valid. 
    ''' If not, remove all entries pertaining to the movie.
    ''' </summary>
    ''' <param name="CleanMovies">If <c>True</c>, process the movie files</param>
    ''' <param name="CleanTVShows">If <c>True</c>, process the TV files</param>
    ''' <param name="SourceID">Optional. If provided, only process entries from that source.</param>
    ''' <remarks></remarks>
    Public Sub Clean(ByVal CleanMovies As Boolean, ByVal CleanMovieSets As Boolean, ByVal CleanTVShows As Boolean, Optional ByVal SourceID As Long = -1)
        Dim fInfo As FileInfo
        Dim tPath As String = String.Empty
        Dim sPath As String = String.Empty

        logger.Info("Cleaning videodatabase started")

        Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
            If CleanMovies Then
                logger.Info("Cleaning movies started")
                Dim MoviePaths As List(Of String) = GetAllMoviePaths()
                MoviePaths.Sort()

                'get a listing of sources and their recursive properties
                Dim SourceList As New List(Of DBSource)
                Dim tSource As DBSource

                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    If SourceID = -1 Then
                        SQLcommand.CommandText = "SELECT * FROM moviesource;"
                    Else
                        SQLcommand.CommandText = String.Format("SELECT * FROM moviesource WHERE idSource = {0}", SourceID)
                    End If
                    Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                        While SQLreader.Read
                            SourceList.Add(Load_Source_Movie(Convert.ToInt64(SQLreader("idSource"))))
                        End While
                    End Using
                End Using

                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    If SourceID = -1 Then
                        SQLcommand.CommandText = "SELECT MoviePath, idMovie, idSource, Type FROM movie ORDER BY MoviePath DESC;"
                    Else
                        SQLcommand.CommandText = String.Format("SELECT MoviePath, idMovie, idSource, Type FROM movie WHERE idSource = {0} ORDER BY MoviePath DESC;", SourceID)
                    End If
                    Using SQLReader As SQLiteDataReader = SQLcommand.ExecuteReader()
                        While SQLReader.Read
                            If Not File.Exists(SQLReader("MoviePath").ToString) OrElse Not Master.eSettings.FileSystemValidExts.Contains(Path.GetExtension(SQLReader("MoviePath").ToString).ToLower) OrElse
                                Master.DB.GetExcludedDirs.Exists(Function(s) SQLReader("MoviePath").ToString.ToLower.StartsWith(s.ToLower)) Then
                                MoviePaths.Remove(SQLReader("MoviePath").ToString)
                                Master.DB.Delete_Movie(Convert.ToInt64(SQLReader("idMovie")), True)
                            ElseIf Master.eSettings.MovieSkipLessThan > 0 Then
                                fInfo = New FileInfo(SQLReader("MoviePath").ToString)
                                If ((Not Master.eSettings.MovieSkipStackedSizeCheck OrElse Not FileUtils.Common.isStacked(fInfo.FullName)) AndAlso fInfo.Length < Master.eSettings.MovieSkipLessThan * 1048576) Then
                                    MoviePaths.Remove(SQLReader("MoviePath").ToString)
                                    Master.DB.Delete_Movie(Convert.ToInt64(SQLReader("idMovie")), True)
                                End If
                            Else
                                tSource = SourceList.OrderByDescending(Function(s) s.Path).FirstOrDefault(Function(s) s.ID = Convert.ToInt64(SQLReader("idSource")))
                                If tSource IsNot Nothing Then
                                    If Directory.GetParent(Directory.GetParent(SQLReader("MoviePath").ToString).FullName).Name.ToLower = "bdmv" Then
                                        tPath = Directory.GetParent(Directory.GetParent(SQLReader("MoviePath").ToString).FullName).FullName
                                    Else
                                        tPath = Directory.GetParent(SQLReader("MoviePath").ToString).FullName
                                    End If
                                    sPath = FileUtils.Common.GetDirectory(tPath).ToLower
                                    If Not tSource.Recursive AndAlso tPath.Length > tSource.Path.Length AndAlso If(sPath = "video_ts" OrElse sPath = "bdmv", tPath.Substring(tSource.Path.Length).Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar).Count > 2, tPath.Substring(tSource.Path.Length).Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar).Count > 1) Then
                                        MoviePaths.Remove(SQLReader("MoviePath").ToString)
                                        Master.DB.Delete_Movie(Convert.ToInt64(SQLReader("idMovie")), True)
                                    ElseIf Not Convert.ToBoolean(SQLReader("Type")) AndAlso tSource.IsSingle AndAlso Not MoviePaths.Where(Function(s) SQLReader("MoviePath").ToString.ToLower.StartsWith(tSource.Path.ToLower)).Count = 1 Then
                                        MoviePaths.Remove(SQLReader("MoviePath").ToString)
                                        Master.DB.Delete_Movie(Convert.ToInt64(SQLReader("idMovie")), True)
                                    End If
                                Else
                                    'orphaned
                                    MoviePaths.Remove(SQLReader("MoviePath").ToString)
                                    Master.DB.Delete_Movie(Convert.ToInt64(SQLReader("idMovie")), True)
                                End If
                            End If
                        End While
                    End Using
                End Using
                logger.Info("Cleaning movies done")
            End If

            If CleanMovieSets Then
                logger.Info("Cleaning moviesets started")
                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand.CommandText = "SELECT sets.idSet, COUNT(setlinkmovie.idMovie) AS 'Count' FROM sets LEFT OUTER JOIN setlinkmovie ON sets.idSet = setlinkmovie.idSet GROUP BY sets.idSet ORDER BY sets.idSet COLLATE NOCASE;"
                    Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                        While SQLreader.Read
                            If Convert.ToInt64(SQLreader("Count")) = 0 Then
                                Master.DB.Delete_MovieSet(Convert.ToInt64(SQLreader("idSet")), True)
                            End If
                        End While
                    End Using
                End Using
                logger.Info("Cleaning moviesets done")
            End If

            If CleanTVShows Then
                logger.Info("Cleaning tv shows started")
                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    If SourceID = -1 Then
                        SQLcommand.CommandText = "SELECT files.strFilename, episode.idEpisode FROM files INNER JOIN episode ON (files.idFile = episode.idFile) ORDER BY files.strFilename;"
                    Else
                        SQLcommand.CommandText = String.Format("SELECT files.strFilename, episode.idEpisode FROM files INNER JOIN episode ON (files.idFile = episode.idFile) WHERE episode.idSource = {0} ORDER BY files.strFilename;", SourceID)
                    End If

                    Using SQLReader As SQLiteDataReader = SQLcommand.ExecuteReader()
                        While SQLReader.Read
                            If Not File.Exists(SQLReader("strFilename").ToString) OrElse Not Master.eSettings.FileSystemValidExts.Contains(Path.GetExtension(SQLReader("strFilename").ToString).ToLower) OrElse
                                Master.DB.GetExcludedDirs.Exists(Function(s) SQLReader("strFilename").ToString.ToLower.StartsWith(s.ToLower)) Then
                                Master.DB.Delete_TVEpisode(Convert.ToInt64(SQLReader("idEpisode")), False, False, True)
                            End If
                        End While
                    End Using

                    logger.Info("Removing tvshows with no more existing local episodes")
                    SQLcommand.CommandText = "DELETE FROM tvshow WHERE NOT EXISTS (SELECT episode.idShow FROM episode WHERE episode.idShow = tvshow.idShow AND NOT episode.idFile = -1);"
                    SQLcommand.ExecuteNonQuery()
                    logger.Info("Removing seasons with no more existing tvshows")
                    SQLcommand.CommandText = "DELETE FROM seasons WHERE idShow NOT IN (SELECT idShow FROM tvshow);"
                    SQLcommand.ExecuteNonQuery()
                    logger.Info("Removing episodes with no more existing tvshows")
                    SQLcommand.CommandText = "DELETE FROM episode WHERE idShow NOT IN (SELECT idShow FROM tvshow);"
                    SQLcommand.ExecuteNonQuery()
                    logger.Info("Removing episodes with orphaned paths")
                    SQLcommand.CommandText = "DELETE FROM episode WHERE NOT EXISTS (SELECT files.idFile FROM files WHERE files.idFile = episode.idFile OR episode.idFile = -1)"
                    SQLcommand.ExecuteNonQuery()
                    logger.Info("Removing orphaned paths")
                    SQLcommand.CommandText = "DELETE FROM files WHERE NOT EXISTS (SELECT episode.idFile FROM episode WHERE episode.idFile = files.idFile AND NOT episode.idFile = -1)"
                    SQLcommand.ExecuteNonQuery()
                End Using

                logger.Info("Removing seasons with no more existing episodes")
                Delete_Empty_TVSeasons(-1, True)
                logger.Info("Cleaning tv shows done")
            End If

            'global cleaning
            logger.Info("Cleaning global tables started")
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                'clean all link tables
                logger.Info("Cleaning actorlinkepisode table")
                SQLcommand.CommandText = "DELETE FROM actorlinkepisode WHERE NOT EXISTS (SELECT 1 FROM episode WHERE episode.idEpisode = actorlinkepisode.idEpisode)"
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning actorlinkmovie table")
                SQLcommand.CommandText = "DELETE FROM actorlinkmovie WHERE NOT EXISTS (SELECT 1 FROM movie WHERE movie.idMovie = actorlinkmovie.idMovie)"
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning actorlinktvshow table")
                SQLcommand.CommandText = "DELETE FROM actorlinktvshow WHERE NOT EXISTS (SELECT 1 FROM tvshow WHERE tvshow.idShow = actorlinktvshow.idShow)"
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning countrylinkmovie table")
                SQLcommand.CommandText = "DELETE FROM countrylinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning directorlinkepisode table")
                SQLcommand.CommandText = "DELETE FROM directorlinkepisode WHERE idEpisode NOT IN (SELECT idEpisode FROM episode);"
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning directorlinkmovie table")
                SQLcommand.CommandText = "DELETE FROM directorlinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning directorlinktvshow table")
                SQLcommand.CommandText = "DELETE FROM directorlinktvshow WHERE idShow NOT IN (SELECT idShow FROM tvshow);"
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning genrelinkmovie table")
                SQLcommand.CommandText = "DELETE FROM genrelinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning genrelinktvshow table")
                SQLcommand.CommandText = "DELETE FROM genrelinktvshow WHERE idShow NOT IN (SELECT idShow FROM tvshow);"
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning setlinkmovie table")
                SQLcommand.CommandText = "DELETE FROM setlinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning studiolinkmovie table")
                SQLcommand.CommandText = "DELETE FROM studiolinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning studiolinktvshow table")
                SQLcommand.CommandText = "DELETE FROM studiolinktvshow WHERE idShow NOT IN (SELECT idShow FROM tvshow);"
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning writerlinkepisode table")
                SQLcommand.CommandText = "DELETE FROM writerlinkepisode WHERE idEpisode NOT IN (SELECT idEpisode FROM episode);"
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning writerlinkmovie table")
                SQLcommand.CommandText = "DELETE FROM writerlinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
                SQLcommand.ExecuteNonQuery()
                'clean all main tables
                logger.Info("Cleaning genre table")
                SQLcommand.CommandText = String.Concat("DELETE FROM genre ",
                                                       "WHERE NOT EXISTS (SELECT 1 FROM genrelinkmovie WHERE genrelinkmovie.idGenre = genre.idGenre) ",
                                                         "AND NOT EXISTS (SELECT 1 FROM genrelinktvshow WHERE genrelinktvshow.idGenre = genre.idGenre)")
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning actor table of actors, directors and writers")
                SQLcommand.CommandText = String.Concat("DELETE FROM actors ",
                                                       "WHERE NOT EXISTS (SELECT 1 FROM actorlinkmovie WHERE actorlinkmovie.idActor = actors.idActor) ",
                                                         "AND NOT EXISTS (SELECT 1 FROM directorlinkmovie WHERE directorlinkmovie.idDirector = actors.idActor) ",
                                                         "AND NOT EXISTS (SELECT 1 FROM writerlinkmovie WHERE writerlinkmovie.idWriter = actors.idActor) ",
                                                         "AND NOT EXISTS (SELECT 1 FROM actorlinktvshow WHERE actorlinktvshow.idActor = actors.idActor) ",
                                                         "AND NOT EXISTS (SELECT 1 FROM actorlinkepisode WHERE actorlinkepisode.idActor = actors.idActor) ",
                                                         "AND NOT EXISTS (SELECT 1 FROM directorlinktvshow WHERE directorlinktvshow.idDirector = actors.idActor) ",
                                                         "AND NOT EXISTS (SELECT 1 FROM directorlinkepisode WHERE directorlinkepisode.idDirector = actors.idActor) ",
                                                         "AND NOT EXISTS (SELECT 1 FROM writerlinkepisode WHERE writerlinkepisode.idWriter = actors.idActor)")
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning country table")
                SQLcommand.CommandText = "DELETE FROM country WHERE NOT EXISTS (SELECT 1 FROM countrylinkmovie WHERE countrylinkmovie.idCountry = country.idCountry)"
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning genre table")
                SQLcommand.CommandText = String.Concat("DELETE FROM genre ",
                                                       "WHERE NOT EXISTS (SELECT 1 FROM genrelinkmovie WHERE genrelinkmovie.idGenre = genre.idGenre) ",
                                                         "AND NOT EXISTS (SELECT 1 FROM genrelinktvshow WHERE genrelinktvshow.idGenre = genre.idGenre)")
                SQLcommand.ExecuteNonQuery()
                logger.Info("Cleaning studio table")
                SQLcommand.CommandText = String.Concat("DELETE FROM studio ",
                                                       "WHERE NOT EXISTS (SELECT 1 FROM studiolinkmovie WHERE studiolinkmovie.idStudio = studio.idStudio) ",
                                                         "AND NOT EXISTS (SELECT 1 FROM studiolinktvshow WHERE studiolinktvshow.idStudio = studio.idStudio)")
                SQLcommand.ExecuteNonQuery()
            End Using
            logger.Info("Cleaning global tables done")

            SQLtransaction.Commit()
        End Using

        logger.Info("Cleaning videodatabase done")

        ' Housekeeping - consolidate and pack database using vacuum command http://www.sqlite.org/lang_vacuum.html
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            logger.Info("Rebulding videodatabase started")
            SQLcommand.CommandText = "VACUUM;"
            SQLcommand.ExecuteNonQuery()
            logger.Info("Rebulding videodatabase done")
        End Using
    End Sub

    Public Function Cleanup_Certifications() As Integer
        logger.Info("[Database] [Cleanup_Certifications] Started")
        Dim iCounter As Integer
        Dim MovieList As New List(Of Long)
        Dim TVShowList As New List(Of Long)

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT DISTINCT idMovie FROM movie;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    MovieList.Add(Convert.ToInt64(SQLreader("idMovie")))
                End While
            End Using

            SQLcommand.CommandText = "SELECT DISTINCT idShow FROM tvshow;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    TVShowList.Add(Convert.ToInt64(SQLreader("idShow")))
                End While
            End Using
        End Using

        Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
            logger.Info("[Database] [Cleanup_Certifications] Process all Movies")
            'Process all Movies
            For Each lMovieID In MovieList
                Dim tmpDBElement As DBElement = Load_Movie(lMovieID)
                If tmpDBElement.IsOnline Then
                    If APIXML.CertificationMapping.RunMapping(tmpDBElement.Movie.Certifications, False) Then
                        'run merge because of the "use certification as MPAA value" settings
                        NFO.MergeDataScraperResults_Movie(tmpDBElement,
                                                          New List(Of MediaContainers.Movie),
                                                          Enums.ScrapeType.SingleField,
                                                          New Structures.ScrapeOptions With {.bMainCertifications = True})
                        Save_Movie(tmpDBElement, True, True, False, True, False)
                        iCounter += 1
                    End If
                Else
                    logger.Warn(String.Concat("[Database] [Cleanup_Certifications] Skip Movie (not online): ", tmpDBElement.Filename))
                End If
            Next

            'Process all TVShows
            logger.Info("[Database] [Cleanup_Certifications] Process all TVShows")
            For Each lTVShowID In TVShowList
                Dim tmpDBElement As DBElement = Load_TVShow(lTVShowID, False, False)
                If tmpDBElement.IsOnline Then
                    If APIXML.CertificationMapping.RunMapping(tmpDBElement.TVShow.Certifications, False) Then
                        'run merge because of the "use certification as MPAA value" settings
                        NFO.MergeDataScraperResults_TV(tmpDBElement,
                                                       New List(Of MediaContainers.TVShow),
                                                       Enums.ScrapeType.SingleField,
                                                       New Structures.ScrapeOptions With {.bMainCertifications = True},
                                                       False)
                        Save_TVShow(tmpDBElement, True, True, False, False)
                        iCounter += 1
                    End If
                Else
                    logger.Warn(String.Concat("[Database] [Cleanup_Certifications] Skip TV Show (not online): ", tmpDBElement.ShowPath))
                End If
            Next

            SQLtransaction.Commit()
        End Using
        logger.Info(String.Format("[Database] [Cleanup_Certifications] {0} items changed", iCounter))
        logger.Info("[Database] [Cleanup_Certifications] Done")
        Return iCounter
    End Function

    Public Function Cleanup_Countries() As Integer
        logger.Info("[Database] [Cleanup_Countries] Started")
        Dim iCounter As Integer
        Dim MovieList As New List(Of Long)
        Dim TVShowList As New List(Of Long)

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT DISTINCT idMovie FROM countrylinkmovie;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    MovieList.Add(Convert.ToInt64(SQLreader("idMovie")))
                End While
            End Using

            SQLcommand.CommandText = "SELECT DISTINCT idShow FROM countrylinktvshow;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    TVShowList.Add(Convert.ToInt64(SQLreader("idShow")))
                End While
            End Using
        End Using

        Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
            logger.Info("[Database] [Cleanup_Countries] Process all Movies")
            'Process all Movies, which are assigned to a country
            For Each lMovieID In MovieList
                Dim tmpDBElement As DBElement = Load_Movie(lMovieID)
                If tmpDBElement.IsOnline Then
                    If APIXML.CountryMapping.RunMapping(tmpDBElement.Movie.Countries, False) Then
                        Save_Movie(tmpDBElement, True, True, False, True, False)
                        iCounter += 1
                    End If
                Else
                    logger.Warn(String.Concat("[Database] [Cleanup_Countries] Skip Movie (not online): ", tmpDBElement.Filename))
                End If
            Next

            'Process all TVShows, which are assigned to a country
            logger.Info("[Database] [Cleanup_Countries] Process all TVShows")
            For Each lTVShowID In TVShowList
                Dim tmpDBElement As DBElement = Load_TVShow(lTVShowID, False, False)
                If tmpDBElement.IsOnline Then
                    If APIXML.CountryMapping.RunMapping(tmpDBElement.TVShow.Countries, False) Then
                        Save_TVShow(tmpDBElement, True, True, False, False)
                        iCounter += 1
                    End If
                Else
                    logger.Warn(String.Concat("[Database] [Cleanup_Countries] Skip TV Show (not online): ", tmpDBElement.ShowPath))
                End If
            Next

            'Cleanup country table
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                logger.Info("[Database] [Cleanup_Countries] Cleaning country table")
                SQLcommand.CommandText = String.Concat("DELETE FROM country ",
                                                       "WHERE NOT EXISTS (SELECT 1 FROM countrylinkmovie WHERE countrylinkmovie.idCountry = country.idCountry) ",
                                                         "AND NOT EXISTS (SELECT 1 FROM countrylinktvshow WHERE countrylinktvshow.idCountry = country.idCountry)")
                SQLcommand.ExecuteNonQuery()
            End Using

            SQLtransaction.Commit()
        End Using
        logger.Info(String.Format("[Database] [Cleanup_Countries] {0} items changed", iCounter))
        logger.Info("[Database] [Cleanup_Countries] Done")
        Return iCounter
    End Function

    Public Function Cleanup_Genres() As Integer
        logger.Info("[Database] [Cleanup_Genres] Started")
        Dim iCounter As Integer
        Dim MovieList As New List(Of Long)
        Dim TVShowList As New List(Of Long)

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT DISTINCT idMovie FROM genrelinkmovie;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    MovieList.Add(Convert.ToInt64(SQLreader("idMovie")))
                End While
            End Using

            SQLcommand.CommandText = "SELECT DISTINCT idShow FROM genrelinktvshow;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    TVShowList.Add(Convert.ToInt64(SQLreader("idShow")))
                End While
            End Using
        End Using

        Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
            logger.Info("[Database] [Cleanup_Genres] Process all Movies")
            'Process all Movies, which are assigned to a genre
            For Each lMovieID In MovieList
                Dim tmpDBElement As DBElement = Load_Movie(lMovieID)
                If tmpDBElement.IsOnline Then
                    If APIXML.GenreMapping.RunMapping(tmpDBElement.Movie.Genres, False) Then
                        Save_Movie(tmpDBElement, True, True, False, True, False)
                        iCounter += 1
                    End If
                Else
                    logger.Warn(String.Concat("[Database] [Cleanup_Genres] Skip Movie (not online): ", tmpDBElement.Filename))
                End If
            Next

            'Process all TVShows, which are assigned to a genre
            logger.Info("[Database] [Cleanup_Genres] Process all TVShows")
            For Each lTVShowID In TVShowList
                Dim tmpDBElement As DBElement = Load_TVShow(lTVShowID, False, False)
                If tmpDBElement.IsOnline Then
                    If APIXML.GenreMapping.RunMapping(tmpDBElement.TVShow.Genres, False) Then
                        Save_TVShow(tmpDBElement, True, True, False, False)
                        iCounter += 1
                    End If
                Else
                    logger.Warn(String.Concat("[Database] [Cleanup_Genres] Skip TV Show (not online): ", tmpDBElement.ShowPath))
                End If
            Next

            'Cleanup genre table
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                logger.Info("[Database] [Cleanup_Genres] Cleaning genre table")
                SQLcommand.CommandText = String.Concat("DELETE FROM genre ",
                                                       "WHERE NOT EXISTS (SELECT 1 FROM genrelinkmovie WHERE genrelinkmovie.idGenre = genre.idGenre) ",
                                                         "AND NOT EXISTS (SELECT 1 FROM genrelinktvshow WHERE genrelinktvshow.idGenre = genre.idGenre)")
                SQLcommand.ExecuteNonQuery()
            End Using

            SQLtransaction.Commit()
        End Using
        logger.Info(String.Format("[Database] [Cleanup_Genres] {0} items changed", iCounter))
        logger.Info("[Database] [Cleanup_Genres] Done")
        Return iCounter
    End Function

    Public Function Cleanup_Status() As Integer
        logger.Info("[Database] [Cleanup_Status] Started")
        Dim iCounter As Integer
        Dim TVShowList As New List(Of Long)

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT DISTINCT idShow FROM tvshow"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    TVShowList.Add(Convert.ToInt64(SQLreader("idShow")))
                End While
            End Using
        End Using

        Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
            'Process all TVShows, which are assigned to a studio
            logger.Info("[Database] [Cleanup_Status] Process all TVShows")
            For Each lTVShowID In TVShowList
                Dim tmpDBElement As DBElement = Load_TVShow(lTVShowID, False, False)
                If tmpDBElement.IsOnline Then
                    If APIXML.StatusMapping.RunMapping(tmpDBElement.TVShow.Status, False) Then
                        Save_TVShow(tmpDBElement, True, True, False, False)
                        iCounter += 1
                    End If
                Else
                    logger.Warn(String.Concat("[Database] [Cleanup_Status] Skip TV Show (not online): ", tmpDBElement.ShowPath))
                End If
            Next
            SQLtransaction.Commit()
        End Using
        logger.Info(String.Format("[Database] [Cleanup_Status] {0} items changed", iCounter))
        logger.Info("[Database] [Cleanup_Status] Done")
        Return iCounter
    End Function

    Public Function Cleanup_Studios() As Integer
        logger.Info("[Database] [Cleanup_Studios] Started")
        Dim iCounter As Integer
        Dim MovieList As New List(Of Long)
        Dim TVShowList As New List(Of Long)

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT DISTINCT idMovie FROM studiolinkmovie;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    MovieList.Add(Convert.ToInt64(SQLreader("idMovie")))
                End While
            End Using

            SQLcommand.CommandText = "SELECT DISTINCT idShow FROM studiolinktvshow;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    TVShowList.Add(Convert.ToInt64(SQLreader("idShow")))
                End While
            End Using
        End Using

        Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
            logger.Info("[Database] [Cleanup_Studios] Process all Movies")
            'Process all Movies, which are assigned to a studio
            For Each lMovieID In MovieList
                Dim tmpDBElement As DBElement = Load_Movie(lMovieID)
                If tmpDBElement.IsOnline Then
                    If APIXML.StudioMapping.RunMapping(tmpDBElement.Movie.Studios, False) Then
                        Save_Movie(tmpDBElement, True, True, False, True, False)
                        iCounter += 1
                    End If
                Else
                    logger.Warn(String.Concat("[Database] [Cleanup_Studios] Skip Movie (not online): ", tmpDBElement.Filename))
                End If
            Next

            'Process all TVShows, which are assigned to a studio
            logger.Info("[Database] [Cleanup_Studios] Process all TVShows")
            For Each lTVShowID In TVShowList
                Dim tmpDBElement As DBElement = Load_TVShow(lTVShowID, False, False)
                If tmpDBElement.IsOnline Then
                    If APIXML.StudioMapping.RunMapping(tmpDBElement.TVShow.Studios, False) Then
                        Save_TVShow(tmpDBElement, True, True, False, False)
                        iCounter += 1
                    End If
                Else
                    logger.Warn(String.Concat("[Database] [Cleanup_Studios] Skip TV Show (not online): ", tmpDBElement.ShowPath))
                End If
            Next

            'Cleanup studio table
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                logger.Info("[Database] [Cleanup_Studios] Cleaning studio table")
                SQLcommand.CommandText = String.Concat("DELETE FROM studio ",
                                                       "WHERE NOT EXISTS (SELECT 1 FROM studiolinkmovie WHERE studiolinkmovie.idStudio = studio.idStudio) ",
                                                         "AND NOT EXISTS (SELECT 1 FROM studiolinktvshow WHERE studiolinktvshow.idStudio = studio.idStudio)")
                SQLcommand.ExecuteNonQuery()
            End Using

            SQLtransaction.Commit()
        End Using
        logger.Info(String.Format("[Database] [Cleanup_Studios] {0} items changed", iCounter))
        logger.Info("[Database] [Cleanup_Studios] Done")
        Return iCounter
    End Function
    ''' <summary>
    ''' Remove the New flag from database entries (movies, tvshow, seasons, episode)
    ''' </summary>
    ''' <remarks>
    ''' 2013/12/13 Dekker500 - Check that MediaDBConn IsNot Nothing before continuing, 
    '''                        otherwise shutdown after a failed startup (before DB initialized) 
    '''                        will trow exception
    ''' </remarks>
    Public Sub ClearNew()
        If (Master.DB.MyVideosDBConn IsNot Nothing) Then
            Using SQLtransaction As SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand.CommandText = "UPDATE movie SET New = (?);"
                    Dim parNew As SQLiteParameter = SQLcommand.Parameters.Add("parNew", DbType.Boolean, 0, "New")
                    parNew.Value = False
                    SQLcommand.ExecuteNonQuery()
                End Using
                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand.CommandText = "UPDATE sets SET New = (?);"
                    Dim parNew As SQLiteParameter = SQLcommand.Parameters.Add("parNew", DbType.Boolean, 0, "New")
                    parNew.Value = False
                    SQLcommand.ExecuteNonQuery()
                End Using
                Using SQLShowcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLShowcommand.CommandText = "UPDATE tvshow SET New = (?);"
                    Dim parShowNew As SQLiteParameter = SQLShowcommand.Parameters.Add("parShowNew", DbType.Boolean, 0, "New")
                    parShowNew.Value = False
                    SQLShowcommand.ExecuteNonQuery()
                End Using
                Using SQLSeasoncommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLSeasoncommand.CommandText = "UPDATE seasons SET New = (?);"
                    Dim parSeasonNew As SQLiteParameter = SQLSeasoncommand.Parameters.Add("parSeasonNew", DbType.Boolean, 0, "New")
                    parSeasonNew.Value = False
                    SQLSeasoncommand.ExecuteNonQuery()
                End Using
                Using SQLEpcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLEpcommand.CommandText = "UPDATE episode SET New = (?);"
                    Dim parEpNew As SQLiteParameter = SQLEpcommand.Parameters.Add("parEpNew", DbType.Boolean, 0, "New")
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
    Public Sub Close_MyVideos()
        CloseDatabase(_myvideosDBConn)
        'CloseDatabase(_jobsDBConn)

        If _myvideosDBConn IsNot Nothing Then
            _myvideosDBConn = Nothing
        End If
        'If _jobsDBConn IsNot Nothing Then
        '    _jobsDBConn = Nothing
        'End If
    End Sub
    ''' <summary>
    ''' Perform the actual closing of the given database connection
    ''' </summary>
    ''' <param name="connection">Database connection on which to perform closing activities</param>
    ''' <remarks></remarks>
    Protected Sub CloseDatabase(ByRef connection As SQLiteConnection)
        If connection Is Nothing Then
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
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "There was a problem closing the media database.")
        Finally
            connection.Dispose()
        End Try
    End Sub
    ''' <summary>
    ''' Creates the connection to the MediaDB database
    ''' </summary>
    ''' <returns><c>True</c> if the database needed to be created (is new), <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Function Connect_MyVideos() As Boolean

        'set database version
        Dim MyVideosDBVersion As Integer = 47

        'set database filename
        Dim MyVideosDB As String = String.Format("MyVideos{0}.emm", MyVideosDBVersion)

        'TODO Warning - This method should be marked as Protected and references re-directed to Connect() above
        If _myvideosDBConn IsNot Nothing Then
            Return False
            'Throw New InvalidOperationException("A database connection is already open, can't open another.")
        End If

        Dim MyVideosDBFile As String = Path.Combine(Master.SettingsPath, MyVideosDB)

        'check if an older DB version still exist
        If Not File.Exists(MyVideosDBFile) Then
            For i As Integer = MyVideosDBVersion - 1 To 2 Step -1
                Dim oldMyVideosDB As String = String.Format("MyVideos{0}.emm", i)
                Dim oldMyVideosDBFile As String = Path.Combine(Master.SettingsPath, oldMyVideosDB)
                If File.Exists(oldMyVideosDBFile) Then
                    Master.fLoading.SetLoadingMesg(Master.eLang.GetString(1356, "Upgrading database..."))
                    Patch_MyVideos(oldMyVideosDBFile, MyVideosDBFile, i, MyVideosDBVersion)
                    Exit For
                End If
            Next
        End If

        Dim isNew As Boolean = Not File.Exists(MyVideosDBFile)

        Try
            _myvideosDBConn = New SQLiteConnection(String.Format(_connStringTemplate, MyVideosDBFile))
            _myvideosDBConn.Open()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Unable to open media database connection.")
        End Try

        Try
            If isNew Then
                Dim sqlCommand As String = File.ReadAllText(FileUtils.Common.ReturnSettingsFile("DB", "MyVideosDBSQL.txt"))

                Using transaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                    Using command As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        command.CommandText = sqlCommand
                        command.ExecuteNonQuery()
                    End Using
                    transaction.Commit()
                End Using
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Error creating database")
            Close_MyVideos()
            File.Delete(MyVideosDBFile)
        End Try
        Return isNew
    End Function

    Private Function ConvertContentTypeToMediaType(contentType As Enums.ContentType) As String
        Select Case contentType
            Case Enums.ContentType.Movie
                Return "movie"
            Case Enums.ContentType.MovieSet
                Return "set"
            'Case Enums.ContentType.Person
            '    Return "person"
            Case Enums.ContentType.TVEpisode
                Return "episode"
            Case Enums.ContentType.TVSeason
                Return "season"
            Case Enums.ContentType.TVShow
                Return "tvshow"
            Case Else
                Return String.Empty
        End Select
    End Function
    ''' <summary>
    ''' Remove all empty TV Seasons there has no episodes defined
    ''' </summary>
    ''' <param name="lShowID">Show ID</param>
    ''' <param name="BatchMode">If <c>False</c>, the action is wrapped in a transaction</param>
    ''' <remarks></remarks>
    Public Sub Delete_Empty_TVSeasons(ByVal lShowID As Long, ByVal BatchMode As Boolean)
        Dim SQLtransaction As SQLiteTransaction = Nothing

        If Not BatchMode Then SQLtransaction = Master.DB.MyVideosDBConn.BeginTransaction()
        Using SQLCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not lShowID = -1 Then
                SQLCommand.CommandText = String.Format("DELETE FROM seasons WHERE seasons.idShow = {0} AND NOT EXISTS (SELECT episode.Season FROM episode WHERE episode.Season = seasons.Season AND episode.idShow = seasons.idShow) AND seasons.Season <> -1", lShowID)
            Else
                SQLCommand.CommandText = String.Format("DELETE FROM seasons WHERE NOT EXISTS (SELECT episode.Season FROM episode WHERE episode.Season = seasons.Season AND episode.idShow = seasons.idShow) AND seasons.Season <> -1")
            End If
            SQLCommand.ExecuteNonQuery()
        End Using
        If Not BatchMode Then SQLtransaction.Commit()
        SQLtransaction = Nothing

        If SQLtransaction IsNot Nothing Then SQLtransaction.Dispose()
    End Sub
    ''' <summary>
    ''' Remove all TV Episodes they are no longer valid (not in <c>ValidEpisodes</c> list)
    ''' </summary>
    ''' <param name="BatchMode">If <c>False</c>, the action is wrapped in a transaction</param>
    ''' <remarks></remarks>
    Public Sub Delete_Invalid_TVEpisodes(ByVal ValidEpisodes As List(Of DBElement), ByVal ShowID As Long, ByVal BatchMode As Boolean)
        Dim SQLtransaction As SQLiteTransaction = Nothing

        If Not BatchMode Then SQLtransaction = Master.DB.MyVideosDBConn.BeginTransaction()
        Using SQLCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLCommand.CommandText = String.Format("SELECT idEpisode FROM episode WHERE idShow = {0};", ShowID)
            Using SQLreader As SQLiteDataReader = SQLCommand.ExecuteReader()
                While SQLreader.Read
                    If ValidEpisodes.Where(Function(f) f.ID = Convert.ToInt64(SQLreader("idEpisode"))).Count = 0 Then
                        Delete_TVEpisode(Convert.ToInt64(SQLreader("idEpisode")), True, False, True)
                    End If
                End While
            End Using
        End Using
        If Not BatchMode Then SQLtransaction.Commit()
        SQLtransaction = Nothing

        If SQLtransaction IsNot Nothing Then SQLtransaction.Dispose()
    End Sub
    ''' <summary>
    ''' Remove all TV Seasons they are no longer valid (not in <c>ValidSeasons</c> list)
    ''' </summary>
    ''' <param name="BatchMode">If <c>False</c>, the action is wrapped in a transaction</param>
    ''' <remarks></remarks>
    Public Sub Delete_Invalid_TVSeasons(ByVal ValidSeasons As List(Of DBElement), ByVal ShowID As Long, ByVal BatchMode As Boolean)
        Dim SQLtransaction As SQLiteTransaction = Nothing

        If Not BatchMode Then SQLtransaction = Master.DB.MyVideosDBConn.BeginTransaction()
        Using SQLCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLCommand.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0};", ShowID)
            Using SQLreader As SQLiteDataReader = SQLCommand.ExecuteReader()
                While SQLreader.Read
                    If ValidSeasons.Where(Function(f) f.ID = Convert.ToInt64(SQLreader("idSeason"))).Count = 0 Then
                        Delete_TVSeason(Convert.ToInt64(SQLreader("idSeason")), True)
                    End If
                End While
            End Using
        End Using
        If Not BatchMode Then SQLtransaction.Commit()
        SQLtransaction = Nothing

        If SQLtransaction IsNot Nothing Then SQLtransaction.Dispose()
    End Sub

    ''' <summary>
    ''' Remove all information related to a movie from the database.
    ''' </summary>
    ''' <param name="ID">ID of the movie to remove, as stored in the database.</param>
    ''' <param name="BatchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Function Delete_Movie(ByVal ID As Long, ByVal BatchMode As Boolean) As Boolean
        If ID < 0 Then Throw New ArgumentOutOfRangeException("idMovie", "Value must be >= 0, was given: " & ID)

        Dim _movieDB As DBElement = Load_Movie(ID)
        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Remove_Movie, Nothing, Nothing, False, _movieDB)

        Try
            Dim SQLtransaction As SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DELETE FROM movie WHERE idMovie = ", ID, ";")
                SQLcommand.ExecuteNonQuery()
            End Using
            If Not BatchMode Then SQLtransaction.Commit()

            RaiseEvent GenericEvent(Enums.ModuleEventType.Remove_Movie, New List(Of Object)(New Object() {_movieDB.ID}))
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
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
    Public Function Delete_MovieSet(ByVal ID As Long, ByVal BatchMode As Boolean) As Boolean
        Try
            'first get a list of all movies in the movieset to remove the movieset information from NFO
            Dim moviesToSave As New List(Of DBElement)

            Dim SQLtransaction As SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT idMovie FROM setlinkmovie ",
                                                       "WHERE idSet = ", ID, ";")
                Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        If Not DBNull.Value.Equals(SQLreader("idMovie")) Then
                            moviesToSave.Add(Load_Movie(Convert.ToInt64(SQLreader("idMovie"))))
                        End If
                    End While
                End Using
            End Using

            'remove the movieset from movie and write new movie NFOs
            If moviesToSave.Count > 0 Then
                For Each movie In moviesToSave
                    movie.Movie.RemoveSet(ID)
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, Nothing, False, movie)
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_Movie, Nothing, Nothing, False, movie)
                    Save_Movie(movie, BatchMode, True, False, True, False)
                    RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {movie.ID}))
                Next
            End If

            'delete all movieset images and if this setting is enabled
            If Master.eSettings.MovieSetCleanFiles Then
                Dim MovieSet As DBElement = Master.DB.Load_MovieSet(ID)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainBanner)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainClearArt)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainClearLogo)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainDiscArt)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainFanart)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainKeyart)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainLandscape)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainPoster)
            End If

            'remove the movieset and still existing setlinkmovie entries
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DELETE FROM sets WHERE idSet = ", ID, ";")
                SQLcommand.ExecuteNonQuery()
            End Using
            If Not BatchMode Then SQLtransaction.Commit()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Remove all information related to a tag from the database.
    ''' </summary>
    ''' <param name="ID">Internal TagID of the tag to remove, as stored in the database.</param>
    ''' <param name="Mode">1=tag of a movie, 2=tag of a show</param>
    ''' <param name="BatchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Function Delete_Tag(ByVal ID As Long, ByVal Mode As Integer, ByVal BatchMode As Boolean) As Boolean
        Try
            'first get a list of all movies in the tag to remove the tag information from NFO
            Dim moviesToSave As New List(Of DBElement)
            Dim SQLtransaction As SQLiteTransaction = Nothing
            Dim tagName As String = String.Empty
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT strTag FROM tag ",
                                                       "WHERE idTag = ", ID, ";")
                Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        If Not DBNull.Value.Equals(SQLreader("strTag")) Then tagName = CStr(SQLreader("strTag"))
                    End While
                End Using
            End Using

            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT idMedia FROM taglinks ",
                                                       "WHERE idTag = ", ID, ";")
                Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        If Mode = 1 Then
                            'tag is for movie
                            If Not DBNull.Value.Equals(SQLreader("idMedia")) Then
                                moviesToSave.Add(Load_Movie(Convert.ToInt64(SQLreader("idMedia"))))
                            End If
                        End If
                    End While
                End Using
            End Using

            'remove the tag from movie and write new movie NFOs
            If moviesToSave.Count > 0 Then
                For Each movie In moviesToSave
                    movie.Movie.Tags.Remove(tagName)
                    Save_Movie(movie, BatchMode, True, False, True, False)
                Next
            End If

            'remove the tag entry
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DELETE FROM tag WHERE idTag = ", ID, ";")
                SQLcommand.ExecuteNonQuery()
            End Using
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DELETE FROM taglinks WHERE idTag = ", ID, ";")
                SQLcommand.ExecuteNonQuery()
            End Using
            If Not BatchMode Then SQLtransaction.Commit()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Remove all information related to a TV episode from the database.
    ''' </summary>
    ''' <param name="lTVEpisodeID">ID of the episode to remove, as stored in the database.</param>
    ''' <param name="BatchMode">Is this function already part of a transaction?</param>
    ''' <returns><c>True</c> if has been removed, <c>False</c> if has been changed to missing</returns>
    Public Function Delete_TVEpisode(ByVal lTVEpisodeID As Long, ByVal Force As Boolean, ByVal DoCleanSeasons As Boolean, ByVal BatchMode As Boolean) As Boolean
        Dim SQLtransaction As SQLiteTransaction = Nothing
        Dim doesExist As Boolean = False
        Dim bHasRemoved As Boolean = False

        Dim _tvepisodeDB As Database.DBElement = Load_TVEpisode(lTVEpisodeID, True)
        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Remove_TVEpisode, Nothing, Nothing, False, _tvepisodeDB)

        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT idFile, Episode, Season, idShow FROM episode WHERE idEpisode = ", lTVEpisodeID, ";")
            Using SQLReader As SQLiteDataReader = SQLcommand.ExecuteReader
                While SQLReader.Read
                    Using SQLECommand As SQLiteCommand = _myvideosDBConn.CreateCommand()

                        If Not Force Then
                            'check if there is another episode with same season and episode number (in this case we don't need a another "Missing" episode)
                            Using SQLcommand_select As SQLiteCommand = _myvideosDBConn.CreateCommand
                                SQLcommand_select.CommandText = String.Format("SELECT COUNT(episode.idEpisode) AS eCount FROM episode WHERE NOT idEpisode = {0} AND Season = {1} AND Episode = {2} AND idShow = {3}", lTVEpisodeID, SQLReader("Season"), SQLReader("Episode"), SQLReader("idShow"))
                                Using SQLReader_select As SQLiteDataReader = SQLcommand_select.ExecuteReader
                                    While SQLReader_select.Read
                                        If CInt(SQLReader_select("eCount")) > 0 Then doesExist = True
                                    End While
                                End Using
                            End Using
                        End If

                        If Force OrElse doesExist Then
                            SQLECommand.CommandText = String.Concat("DELETE FROM episode WHERE idEpisode = ", lTVEpisodeID, ";")
                            SQLECommand.ExecuteNonQuery()

                            If DoCleanSeasons Then Master.DB.Delete_Empty_TVSeasons(Convert.ToInt64(SQLReader("idShow")), True)
                            bHasRemoved = True
                        ElseIf Not Convert.ToInt64(SQLReader("idFile")) = -1 Then 'already marked as missing, no need for another query
                            'check if there is another episode that use the same idFile
                            Dim multiEpisode As Boolean = False
                            Using SQLcommand_select As SQLiteCommand = _myvideosDBConn.CreateCommand
                                SQLcommand_select.CommandText = String.Format("SELECT COUNT(episode.idFile) AS eCount FROM episode WHERE idFile = {0}", Convert.ToInt64(SQLReader("idFile")))
                                Using SQLReader_select As SQLiteDataReader = SQLcommand_select.ExecuteReader
                                    While SQLReader_select.Read
                                        If CInt(SQLReader_select("eCount")) > 1 Then multiEpisode = True
                                    End While
                                End Using
                            End Using
                            If Not multiEpisode Then
                                SQLECommand.CommandText = String.Concat("DELETE FROM files WHERE idFile = ", Convert.ToInt64(SQLReader("idFile")), ";")
                                SQLECommand.ExecuteNonQuery()
                            End If
                            SQLECommand.CommandText = String.Concat("DELETE FROM TVVStreams WHERE TVEpID = ", lTVEpisodeID, ";")
                            SQLECommand.ExecuteNonQuery()
                            SQLECommand.CommandText = String.Concat("DELETE FROM TVAStreams WHERE TVEpID = ", lTVEpisodeID, ";")
                            SQLECommand.ExecuteNonQuery()
                            SQLECommand.CommandText = String.Concat("DELETE FROM TVSubs WHERE TVEpID = ", lTVEpisodeID, ";")
                            SQLECommand.ExecuteNonQuery()
                            SQLECommand.CommandText = String.Concat("DELETE FROM art WHERE media_id = ", lTVEpisodeID, " AND media_type = 'episode';")
                            SQLECommand.ExecuteNonQuery()
                            SQLECommand.CommandText = String.Concat("UPDATE episode SET New = 0, ",
                                                                    "idFile = -1, NfoPath = '', ",
                                                                    "VideoSource = '' WHERE idEpisode = ", lTVEpisodeID, ";")
                            SQLECommand.ExecuteNonQuery()
                        End If
                    End Using
                End While
            End Using
        End Using

        If Not BatchMode Then
            SQLtransaction.Commit()
        End If

        Return bHasRemoved
    End Function

    Public Function Delete_TVEpisode(ByVal strPath As String, ByVal Force As Boolean, ByVal BatchMode As Boolean) As Boolean
        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLPCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLPCommand.CommandText = String.Concat("SELECT idFile FROM files WHERE strFilename = """, strPath, """;")
            Using SQLPReader As SQLiteDataReader = SQLPCommand.ExecuteReader
                While SQLPReader.Read
                    Using SQLCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        SQLCommand.CommandText = String.Concat("SELECT idEpisode FROM episode WHERE idFile = ", SQLPReader("idFile"), ";")
                        Using SQLReader As SQLiteDataReader = SQLCommand.ExecuteReader
                            While SQLReader.Read
                                Delete_TVEpisode(CInt(SQLReader("idEpisode")), Force, False, BatchMode)
                            End While
                        End Using
                    End Using
                End While
            End Using
        End Using
        If Not BatchMode Then SQLtransaction.Commit()

        Return True
    End Function

    ''' <summary>
    ''' Remove all information related to a TV season from the database.
    ''' </summary>
    ''' <param name="BatchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Function Delete_TVSeason(ByVal ID As Long, ByVal BatchMode As Boolean) As Boolean
        If ID < 0 Then Throw New ArgumentOutOfRangeException("idSeason", "Value must be >= 0, was given: " & ID)

        Try
            Dim SQLtransaction As SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DELETE FROM seasons WHERE idSeason = ", ID, ";")
                SQLcommand.ExecuteNonQuery()
            End Using
            If Not BatchMode Then SQLtransaction.Commit()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
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
    Public Function Delete_TVSeason(ByVal ShowID As Long, ByVal iSeason As Integer, ByVal BatchMode As Boolean) As Boolean
        If ShowID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & ShowID)
        If iSeason < 0 Then Throw New ArgumentOutOfRangeException("iSeason", "Value must be >= 0, was given: " & iSeason)

        Try
            Dim SQLtransaction As SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DELETE FROM seasons WHERE idShow = ", ShowID, " AND Season = ", iSeason, ";")
                SQLcommand.ExecuteNonQuery()
            End Using
            If Not BatchMode Then SQLtransaction.Commit()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
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
    Public Function Delete_TVShow(ByVal ID As Long, ByVal BatchMode As Boolean) As Boolean
        If ID < 0 Then Throw New ArgumentOutOfRangeException("idShow", "Value must be >= 0, was given: " & ID)

        Dim _tvshowDB As Database.DBElement = Load_TVShow_Full(ID)
        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Remove_TVShow, Nothing, Nothing, False, _tvshowDB)

        Try
            Dim SQLtransaction As SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DELETE FROM tvshow WHERE idShow = ", ID, ";")
                SQLcommand.ExecuteNonQuery()
            End Using
            If Not BatchMode Then SQLtransaction.Commit()

            RaiseEvent GenericEvent(Enums.ModuleEventType.Remove_TVShow, New List(Of Object)(New Object() {_tvshowDB.ID}))
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
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
        Dim sqlDA As New SQLiteDataAdapter(Command, _myvideosDBConn)
        sqlDA.Fill(dTable)
    End Sub

    Public Function GetAllTags() As String()
        Dim nList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT strTag FROM tag ORDER BY strTag;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("strTag").ToString.Trim)
                End While
            End Using
        End Using
        Return nList.ToArray
    End Function

    Public Function GetTVShowEpisodeSorting(ByVal ShowID As Long) As Enums.EpisodeSorting
        Dim sEpisodeSorting As Enums.EpisodeSorting = Enums.EpisodeSorting.Episode
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT EpisodeSorting FROM tvshow WHERE idShow = ", ShowID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    sEpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("EpisodeSorting")), Enums.EpisodeSorting)
                End While
            End Using
        End Using
        Return sEpisodeSorting
    End Function

    Public Function GetAllCertifications() As String()
        Dim nList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT Certification FROM movie WHERE Certification <> '';"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If SQLreader("Certification").ToString.Contains(" / ") Then
                        Dim values As String() = Regex.Split(SQLreader("Certification").ToString, " / ")
                        For Each certification As String In values
                            certification = certification.Trim
                            If Not nList.Contains(certification) Then
                                nList.Add(certification)
                            End If
                        Next
                    Else
                        If Not nList.Contains(SQLreader("Certification").ToString.Trim) Then
                            nList.Add(SQLreader("Certification").ToString.Trim)
                        End If
                    End If
                End While
            End Using
        End Using
        Return nList.ToArray
    End Function

    Public Function GetAllCountries() As String()
        Dim nList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT strCountry FROM country ORDER BY strCountry;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("strCountry").ToString)
                End While
            End Using
        End Using
        Return nList.ToArray
    End Function

    Public Function GetAllMovieSetDetails() As List(Of MediaContainers.SetDetails)
        Dim nList As New List(Of MediaContainers.SetDetails)
        For Each nSet In Load_AllMoviesets()
            nList.Add(New MediaContainers.SetDetails With {
                      .ID = nSet.ID,
                      .Plot = nSet.MovieSet.Plot,
                      .Title = nSet.MovieSet.Title,
                      .UniqueIDs = New MediaContainers.UniqueidContainer With {.TMDbId = nSet.MovieSet.UniqueIDs.TMDbId}
                      })
        Next
        Return nList
    End Function

    Public Function GetAllSources_Movie() As String()
        Dim nList As New List(Of String)
        For Each nSource In Master.DB.GetSources_Movie
            nList.Add(nSource.Name)
        Next
        Return nList.ToArray
    End Function

    Public Function GetAllSources_TVShow() As String()
        Dim nList As New List(Of String)
        For Each nSource In Master.DB.GetSources_TVShow
            nList.Add(nSource.Name)
        Next
        Return nList.ToArray
    End Function

    Public Function GetAllStatus() As String()
        Dim nList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT DISTINCT Status FROM tvshow WHERE Status <> '' ORDER BY Status;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("Status").ToString)
                End While
            End Using
        End Using
        Return nList.ToArray
    End Function

    Public Function GetAllStudios() As String()
        Dim nList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT strStudio FROM studio ORDER BY strStudio;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("strStudio").ToString)
                End While
            End Using
        End Using
        Return nList.ToArray
    End Function

    Public Function GetAllTVShowTitles() As List(Of String)
        Dim lstTitles As New List(Of String)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT Title FROM tvshow ORDER BY Title;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    lstTitles.Add(SQLreader("Title").ToString)
                End While
            End Using
        End Using
        lstTitles.Sort()
        Return lstTitles
    End Function

    Public Function GetAllVideoSources_Movie() As String()
        Dim nList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT DISTINCT VideoSource FROM movie WHERE VideoSource <> '' ORDER BY VideoSource;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("VideoSource").ToString)
                End While
            End Using
        End Using
        Return nList.ToArray
    End Function

    Public Function GetAllVideoSources_TVEpisode() As String()
        Dim nList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT DISTINCT VideoSource FROM episode WHERE VideoSource <> '' ORDER BY VideoSource;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("VideoSource").ToString)
                End While
            End Using
        End Using
        Return nList.ToArray
    End Function

    Public Function GetArtForItem(ByVal mediaId As Long, ByVal MediaType As String, ByVal artType As String) As String
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT url FROM art WHERE media_id={0} AND media_type='{1}' AND type='{2}'", mediaId, MediaType, artType)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Return SQLreader("url").ToString
                    Exit While
                End While
            End Using
        End Using
        Return String.Empty
    End Function
    ''' <summary>
    ''' Get a list of excluded directories
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetExcludedDirs() As List(Of String)
        Dim lstPaths As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT Dirname FROM ExcludeDir;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    lstPaths.Add(SQLreader("Dirname").ToString)
                End While
            End Using
        End Using
        Return lstPaths
    End Function

    Private Function GetMoviesetsForMovie(ByVal idMovie As Long) As List(Of MediaContainers.SetDetails)
        Dim lstResults As New List(Of MediaContainers.SetDetails)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Concat("SELECT A.idMovie, A.idSet, A.iOrder, B.idSet, B.Plot, B.SetName, B.TMDBColID FROM setlinkmovie ",
                                                   "AS A INNER JOIN sets AS B ON (A.idSet = B.idSet) WHERE A.idMovie = ", idMovie, ";")
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    Dim nSet As New MediaContainers.SetDetails
                    If Not DBNull.Value.Equals(SQLreader("idSet")) Then nSet.ID = Convert.ToInt64(SQLreader("idSet"))
                    If Not DBNull.Value.Equals(SQLreader("iOrder")) Then nSet.Order = CInt(SQLreader("iOrder"))
                    If Not DBNull.Value.Equals(SQLreader("Plot")) Then nSet.Plot = SQLreader("plot").ToString
                    If Not DBNull.Value.Equals(SQLreader("SetName")) Then nSet.Title = SQLreader("SetName").ToString
                    lstResults.Add(nSet)
                End While
            End Using
        End Using
        Return lstResults
    End Function

    Private Function GetRatingsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of MediaContainers.RatingDetails)
        Dim lstResults As New List(Of MediaContainers.RatingDetails)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT * FROM rating WHERE media_id={0} AND media_type='{1}';", idMedia, mediaType)
                Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    While sqlReader.Read
                        lstResults.Add(New MediaContainers.RatingDetails With {
                                           .ID = CLng(sqlReader("idRating")),
                                           .IsDefault = CBool(sqlReader("isDefault")),
                                           .Max = CInt(sqlReader("rating_max")),
                                           .Name = sqlReader("rating_type").ToString,
                                           .Value = CDbl(sqlReader("rating")),
                                           .Votes = CInt(sqlReader("votes"))
                                           })
                    End While
                End Using
            End Using
        End If
        Return lstResults
    End Function

    Private Function GetUniqueIDsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As MediaContainers.UniqueidContainer
        Dim lstUniqueIDs As New MediaContainers.UniqueidContainer
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT * FROM uniqueid WHERE media_id={0} AND media_type='{1}' ORDER BY isDefault=0", idMedia, mediaType)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    While SQLreader.Read
                        lstUniqueIDs.Items.Add(New MediaContainers.Uniqueid With {
                                               .ID = CLng(SQLreader("idUniqueID")),
                                               .IsDefault = CBool(SQLreader("isDefault")),
                                               .Type = SQLreader("type").ToString,
                                               .Value = SQLreader("value").ToString
                                               })
                    End While
                End Using
            End Using
        End If
        Return lstUniqueIDs
    End Function

    Public Sub LoadAllCertifications()
        For Each aElement As String In GetAllCertifications()
            Dim nMapping As SimpleMapping = APIXML.CertificationMapping.Mappings.FirstOrDefault(Function(f) f.Input = aElement)
            If nMapping Is Nothing Then
                'add a new mapping if aElement is not in the MappingTable
                APIXML.CertificationMapping.Mappings.Add(New SimpleMapping With {.Input = aElement, .MappedTo = aElement})
            End If
        Next
    End Sub

    Public Sub LoadAllCountries()
        For Each aElement As String In GetAllCountries()
            Dim nMapping As SimpleMapping = APIXML.CountryMapping.Mappings.FirstOrDefault(Function(f) f.Input = aElement)
            If nMapping Is Nothing Then
                'add a new mapping if aElement is not in the MappingTable
                APIXML.CountryMapping.Mappings.Add(New SimpleMapping With {.Input = aElement, .MappedTo = aElement})
            End If
        Next
    End Sub

    Public Sub LoadAllGenres()
        Dim nList As New List(Of String)

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT strGenre FROM genre ORDER BY strGenre;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("strGenre").ToString)
                End While
            End Using
        End Using

        For Each aGenre As String In nList
            Dim gMapping As GenreMapping = APIXML.GenreMapping.Mappings.FirstOrDefault(Function(f) f.SearchString = aGenre)
            If gMapping Is Nothing Then
                'check if the tGenre is already existing in Gernes list
                Dim gProperty As GenreProperty = APIXML.GenreMapping.Genres.FirstOrDefault(Function(f) f.Name = aGenre)
                If gProperty Is Nothing Then
                    APIXML.GenreMapping.Genres.Add(New GenreProperty With {.isNew = False, .Name = aGenre})
                End If
                'add a new mapping if tGenre is not in the MappingTable
                APIXML.GenreMapping.Mappings.Add(New GenreMapping With {.isNew = False, .MappedTo = New List(Of String) From {aGenre}, .SearchString = aGenre})
            End If
        Next
    End Sub

    Public Sub LoadAllStatus()
        For Each aElement As String In GetAllStatus()
            Dim nMapping As SimpleMapping = APIXML.StatusMapping.Mappings.FirstOrDefault(Function(f) f.Input = aElement)
            If nMapping Is Nothing Then
                'add a new mapping if aElement is not in the MappingTable
                APIXML.StatusMapping.Mappings.Add(New SimpleMapping With {.Input = aElement, .MappedTo = aElement})
            End If
        Next
    End Sub

    Public Sub LoadAllStudios()
        For Each aElement As String In GetAllStudios()
            Dim nMapping As SimpleMapping = APIXML.StudioMapping.Mappings.FirstOrDefault(Function(f) f.Input = aElement)
            If nMapping Is Nothing Then
                'add a new mapping if aElement is not in the MappingTable
                APIXML.StudioMapping.Mappings.Add(New SimpleMapping With {.Input = aElement, .MappedTo = aElement})
            End If
        Next
    End Sub

    Public Function GetAllMoviePaths() As List(Of String)
        Dim tList As New List(Of String)
        Dim mPath As String = String.Empty
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT MoviePath FROM movie;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    mPath = SQLreader("MoviePath").ToString.ToLower
                    If Master.eSettings.FileSystemNoStackExts.Contains(Path.GetExtension(mPath)) Then
                        tList.Add(mPath)
                    Else
                        tList.Add(FileUtils.Common.RemoveStackingMarkers(mPath))
                    End If
                End While
            End Using
        End Using
        Return tList
    End Function

    Public Function GetAllTVEpisodePaths() As List(Of String)
        Dim tList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT strFilename FROM files;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    tList.Add(SQLreader("strFilename").ToString.ToLower)
                End While
            End Using
        End Using
        Return tList
    End Function

    Public Function GetAllTVShowPaths() As Hashtable
        Dim tList As New Hashtable
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT idShow, TVShowPath FROM tvshow;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    tList.Add(SQLreader("TVShowPath").ToString.ToLower, SQLreader("idShow"))
                End While
            End Using
        End Using
        Return tList
    End Function
    ''' <summary>
    ''' Get all movie sources from DB
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSources_Movie() As List(Of DBSource)
        Dim lstSources As New List(Of DBSource)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT * FROM moviesource ORDER BY strName;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Dim msource As New DBSource
                    msource.ID = Convert.ToInt64(SQLreader("idSource"))
                    msource.Name = SQLreader("strName").ToString
                    msource.Path = SQLreader("strPath").ToString
                    msource.Recursive = Convert.ToBoolean(SQLreader("bRecursive"))
                    msource.UseFolderName = Convert.ToBoolean(SQLreader("bFoldername"))
                    msource.IsSingle = Convert.ToBoolean(SQLreader("bSingle"))
                    msource.Exclude = Convert.ToBoolean(SQLreader("bExclude"))
                    msource.GetYear = Convert.ToBoolean(SQLreader("bGetYear"))
                    msource.Language = SQLreader("strLanguage").ToString
                    msource.LastScan = SQLreader("strLastScan").ToString
                    lstSources.Add(msource)
                End While
            End Using
        End Using
        Return lstSources
    End Function
    ''' <summary>
    ''' Get all tv show sources from DB
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSources_TVShow() As List(Of DBSource)
        Dim lstSources As New List(Of DBSource)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT * FROM tvshowsource ORDER BY strName;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Dim tvsource As New DBSource
                    tvsource.ID = Convert.ToInt64(SQLreader("idSource"))
                    tvsource.Name = SQLreader("strName").ToString
                    tvsource.Path = SQLreader("strPath").ToString
                    tvsource.Language = SQLreader("strLanguage").ToString
                    tvsource.Ordering = DirectCast(Convert.ToInt32(SQLreader("iOrdering")), Enums.EpisodeOrdering)
                    tvsource.Exclude = Convert.ToBoolean(SQLreader("bExclude"))
                    tvsource.EpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("iEpisodeSorting")), Enums.EpisodeSorting)
                    tvsource.LastScan = SQLreader("strLastScan").ToString
                    tvsource.IsSingle = Convert.ToBoolean(SQLreader("bSingle"))
                    lstSources.Add(tvsource)
                End While
            End Using
        End Using
        Return lstSources
    End Function

    Public Function GetTVSeasonIDFromEpisode(ByVal DBElement As DBElement) As Long
        Dim sID As Long = -1
        If DBElement.TVEpisode IsNot Nothing Then
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0} AND Season = {1};", DBElement.ShowID, DBElement.TVEpisode.Season)
                Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        SQLreader.Read()
                        sID = CLng(SQLreader.Item("idSeason"))
                        Return sID
                    Else
                        Return sID
                    End If
                End Using
            End Using
        Else
            Return sID
        End If
    End Function

    Public Function GetTVSeasonIDFromShowIDAndSeasonNumber(ByVal lngTVShowID As Long, ByVal iSeason As Integer) As Long
        Dim sID As Long = -1
        If lngTVShowID > -1 AndAlso iSeason > -1 Then
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0} AND Season = {1};", lngTVShowID, iSeason)
                Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        SQLreader.Read()
                        sID = CLng(SQLreader.Item("idSeason"))
                        Return sID
                    Else
                        Return sID
                    End If
                End Using
            End Using
        Else
            Return sID
        End If
    End Function

    Public Function AddView(ByVal dbCommand As String) As Boolean
        Try
            Dim SQLtransaction As SQLiteTransaction = Nothing
            SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand_view_add As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand_view_add.CommandText = dbCommand
                SQLcommand_view_add.ExecuteNonQuery()
            End Using
            SQLtransaction.Commit()
            Return True
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            Return False
        End Try
    End Function

    Public Function DeleteView(ByVal ViewName As String) As Boolean
        If String.IsNullOrEmpty(ViewName) Then Return False
        Try
            Dim SQLtransaction As SQLiteTransaction = Nothing
            SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DROP VIEW IF EXISTS """, ViewName, """;")
                SQLcommand.ExecuteNonQuery()
            End Using
            SQLtransaction.Commit()
            Return True
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            Return False
        End Try
    End Function

    Public Function GetViewDetails(ByVal ViewName As String) As SQLViewProperty
        Dim ViewProperty As New SQLViewProperty
        If Not String.IsNullOrEmpty(ViewName) Then
            Try
                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand.CommandText = String.Concat("SELECT name, sql FROM sqlite_master WHERE type ='view' AND name='", ViewName, "';")
                    Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                        While SQLreader.Read
                            ViewProperty.Name = SQLreader("name").ToString
                            ViewProperty.Statement = SQLreader("sql").ToString
                        End While
                    End Using
                End Using
                Return ViewProperty
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
        Return ViewProperty
    End Function

    Public Function ViewExists(ByVal ViewName As String) As Boolean
        If Not String.IsNullOrEmpty(ViewName) Then
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("SELECT name FROM sqlite_master WHERE type ='view' AND name = '{0}';", ViewName)
                Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        Return True
                    Else
                        Return False
                    End If
                End Using
            End Using
        Else
            Return False
        End If
    End Function

    Public Function GetViewMediaCount(ByVal ViewName As String, Optional EpisodesByView As Boolean = False) As Integer
        Dim mCount As Integer
        If Not String.IsNullOrEmpty(ViewName) Then
            Try
                If Not EpisodesByView Then
                    Using SQLCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand.CommandText = String.Format("SELECT COUNT(*) FROM '{0}'", ViewName)
                        mCount = Convert.ToInt32(SQLCommand.ExecuteScalar)
                        Return mCount
                    End Using
                Else
                    Using SQLCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand.CommandText = String.Format("SELECT COUNT(*) FROM '{0}' INNER JOIN episode ON ('{0}'.idShow = episode.idShow) WHERE NOT episode.idFile = -1", ViewName)
                        mCount = Convert.ToInt32(SQLCommand.ExecuteScalar)
                        Return mCount
                    End Using
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return -1
            End Try
        Else
            Return mCount
        End If
    End Function

    Public Function GetViewList(ByVal Type As Enums.ContentType) As List(Of String)
        Dim ViewList As New List(Of String)
        Dim ContentType As String = String.Empty

        Select Case Type
            Case Enums.ContentType.TVEpisode
                ContentType = "episode-"
            Case Enums.ContentType.Movie
                ContentType = "movie-"
            Case Enums.ContentType.MovieSet
                ContentType = "sets-"
            Case Enums.ContentType.TVSeason
                ContentType = "seasons-"
            Case Enums.ContentType.TVShow
                ContentType = "tvshow-"
        End Select

        If Not String.IsNullOrEmpty(ContentType) OrElse Type = Enums.ContentType.None Then
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("SELECT name FROM sqlite_master WHERE type ='view' AND name LIKE '{0}%';", ContentType)
                Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        ViewList.Add(SQLreader("name").ToString)
                    End While
                End Using
            End Using

            'remove default lists
            If ViewList.Contains("episodelist") Then ViewList.Remove("episodelist")
            If ViewList.Contains("movielist") Then ViewList.Remove("movielist")
            If ViewList.Contains("seasonslist") Then ViewList.Remove("seasonslist")
            If ViewList.Contains("setslist") Then ViewList.Remove("setslist")
            If ViewList.Contains("tvshowlist") Then ViewList.Remove("tvshowlist")
        End If

        Return ViewList
    End Function

    Public Function GetMovies() As List(Of DBElement)
        Dim lstDBELement As New List(Of DBElement)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT idMovie FROM movie ORDER BY ListTitle;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        lstDBELement.Add(Master.DB.Load_Movie(Convert.ToInt64(SQLreader("idMovie"))))
                    End While
                End If
            End Using
        End Using
        Return lstDBELement
    End Function

    Public Function GetTVShows(ByVal withseasons As Boolean, ByVal withepisodes As Boolean, Optional ByVal withmissingepisodes As Boolean = False) As List(Of DBElement)
        Dim lstDBELement As New List(Of DBElement)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT idShow FROM tvshow;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        lstDBELement.Add(Master.DB.Load_TVShow(Convert.ToInt64(SQLreader("idShow")), withseasons, withepisodes, withmissingepisodes))
                    End While
                End If
            End Using
        End Using
        Return lstDBELement
    End Function
    ''' <summary>
    ''' Load all the information for a movie.
    ''' </summary>
    ''' <param name="MovieID">ID of the movie to load, as stored in the database</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_Movie(ByVal MovieID As Long) As DBElement
        Dim _movieDB As New DBElement(Enums.ContentType.Movie) With {
            .ID = MovieID,
            .Movie = New MediaContainers.Movie
        }
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM movie WHERE idMovie = ", _movieDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("ListTitle")) Then _movieDB.ListTitle = SQLreader("ListTitle").ToString
                    If Not DBNull.Value.Equals(SQLreader("MoviePath")) Then _movieDB.Filename = SQLreader("MoviePath").ToString
                    _movieDB.IsSingle = Convert.ToBoolean(SQLreader("Type"))
                    If Not DBNull.Value.Equals(SQLreader("TrailerPath")) Then _movieDB.Trailer.LocalFilePath = SQLreader("TrailerPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("NfoPath")) Then _movieDB.NfoPath = SQLreader("NfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("EThumbsPath")) Then _movieDB.ExtrathumbsPath = SQLreader("EThumbsPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("EFanartsPath")) Then _movieDB.ExtrafanartsPath = SQLreader("EFanartsPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("ThemePath")) Then _movieDB.Theme.LocalFilePath = SQLreader("ThemePath").ToString

                    _movieDB.Source = Load_Source_Movie(Convert.ToInt64(SQLreader("idSource")))

                    _movieDB.IsMark = Convert.ToBoolean(SQLreader("Mark"))
                    _movieDB.IsLock = Convert.ToBoolean(SQLreader("Lock"))
                    _movieDB.OutOfTolerance = Convert.ToBoolean(SQLreader("OutOfTolerance"))
                    _movieDB.IsMarkCustom1 = Convert.ToBoolean(SQLreader("MarkCustom1"))
                    _movieDB.IsMarkCustom2 = Convert.ToBoolean(SQLreader("MarkCustom2"))
                    _movieDB.IsMarkCustom3 = Convert.ToBoolean(SQLreader("MarkCustom3"))
                    _movieDB.IsMarkCustom4 = Convert.ToBoolean(SQLreader("MarkCustom4"))
                    If Not DBNull.Value.Equals(SQLreader("VideoSource")) Then _movieDB.VideoSource = SQLreader("VideoSource").ToString
                    If Not DBNull.Value.Equals(SQLreader("Language")) Then _movieDB.Language = SQLreader("Language").ToString

                    With _movieDB.Movie
                        If Not DBNull.Value.Equals(SQLreader("DateAdded")) Then .DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("DateAdded"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("DateModified")) Then .DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("DateModified"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("Title")) Then .Title = SQLreader("Title").ToString
                        If Not DBNull.Value.Equals(SQLreader("OriginalTitle")) Then .OriginalTitle = SQLreader("OriginalTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("SortTitle")) Then .SortTitle = SQLreader("SortTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("Year")) Then .Year = SQLreader("Year").ToString
                        If Not DBNull.Value.Equals(SQLreader("Rating")) Then .Rating = SQLreader("Rating").ToString
                        If Not DBNull.Value.Equals(SQLreader("Votes")) Then .Votes = SQLreader("Votes").ToString
                        If Not DBNull.Value.Equals(SQLreader("MPAA")) Then .MPAA = SQLreader("MPAA").ToString
                        If Not DBNull.Value.Equals(SQLreader("Top250")) Then .Top250 = Convert.ToInt32(SQLreader("Top250"))
                        If Not DBNull.Value.Equals(SQLreader("Outline")) Then .Outline = SQLreader("Outline").ToString
                        If Not DBNull.Value.Equals(SQLreader("Plot")) Then .Plot = SQLreader("Plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("Tagline")) Then .Tagline = SQLreader("Tagline").ToString
                        If Not DBNull.Value.Equals(SQLreader("Trailer")) Then .Trailer = SQLreader("Trailer").ToString
                        If Not DBNull.Value.Equals(SQLreader("Certification")) Then .AddCertificationsFromString(SQLreader("Certification").ToString)
                        If Not DBNull.Value.Equals(SQLreader("Runtime")) Then .Runtime = SQLreader("Runtime").ToString
                        If Not DBNull.Value.Equals(SQLreader("ReleaseDate")) Then .Premiered = SQLreader("ReleaseDate").ToString
                        If Not DBNull.Value.Equals(SQLreader("PlayCount")) Then .PlayCount = Convert.ToInt32(SQLreader("PlayCount"))
                        If Not DBNull.Value.Equals(SQLreader("FanartURL")) AndAlso Not Master.eSettings.MovieImagesNotSaveURLToNfo Then .Fanart.URL = SQLreader("FanartURL").ToString
                        If Not DBNull.Value.Equals(SQLreader("VideoSource")) Then .VideoSource = SQLreader("VideoSource").ToString
                        If Not DBNull.Value.Equals(SQLreader("iLastPlayed")) Then .LastPlayed = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("iLastPlayed"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("Language")) Then .Language = SQLreader("Language").ToString
                        If Not DBNull.Value.Equals(SQLreader("iUserRating")) Then .UserRating = Convert.ToInt32(SQLreader("iUserRating"))
                    End With
                End If
            End Using
        End Using

        'Actors
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT A.strRole, B.idActor, B.strActor, B.strThumb, C.url FROM actorlinkmovie AS A ",
                        "INNER JOIN actors AS B ON (A.idActor = B.idActor) ",
                        "LEFT OUTER JOIN art AS C ON (B.idActor = C.media_id AND C.media_type = 'actor' AND C.type = 'thumb') ",
                        "WHERE A.idMovie = ", _movieDB.ID, " ",
                        "ORDER BY A.iOrder;")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim person As MediaContainers.Person
                While SQLreader.Read
                    person = New MediaContainers.Person
                    person.ID = Convert.ToInt64(SQLreader("idActor"))
                    person.Name = SQLreader("strActor").ToString
                    person.Role = SQLreader("strRole").ToString
                    person.LocalFilePath = SQLreader("url").ToString
                    person.URLOriginal = SQLreader("strThumb").ToString
                    _movieDB.Movie.Actors.Add(person)
                End While
            End Using
        End Using

        'Countries
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strCountry FROM countrylinkmovie ",
                                                   "AS A INNER JOIN country AS B ON (A.idCountry = B.idCountry) WHERE A.idMovie = ", _movieDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strCountry")) Then _movieDB.Movie.Countries.Add(SQLreader("strCountry").ToString)
                End While
            End Using
        End Using

        'Credits
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strActor FROM writerlinkmovie ",
                                                   "AS A INNER JOIN actors AS B ON (A.idWriter = B.idActor) WHERE A.idMovie = ", _movieDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strActor")) Then _movieDB.Movie.Credits.Add(SQLreader("strActor").ToString)
                End While
            End Using
        End Using

        'Directors
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strActor FROM directorlinkmovie ",
                                                   "AS A INNER JOIN actors AS B ON (A.idDirector = B.idActor) WHERE A.idMovie = ", _movieDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strActor")) Then _movieDB.Movie.Directors.Add(SQLreader("strActor").ToString)
                End While
            End Using
        End Using

        'Genres
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strGenre FROM genrelinkmovie ",
                                                   "AS A INNER JOIN genre AS B ON (A.idGenre = B.idGenre) WHERE A.idMovie = ", _movieDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strGenre")) Then _movieDB.Movie.Genres.Add(SQLreader("strGenre").ToString)
                End While
            End Using
        End Using

        'Moviesets
        _movieDB.Movie.Sets.AddRange(GetMoviesetsForMovie(_movieDB.ID))

        'Ratings
        _movieDB.Movie.Ratings = GetRatingsForItem(_movieDB.ID, _movieDB.ContentType)

        'UniqueIDs
        _movieDB.Movie.UniqueIDs = GetUniqueIDsForItem(_movieDB.ID, _movieDB.ContentType)

        'Video streams
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM MoviesVStreams WHERE MovieID = ", _movieDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim video As MediaContainers.Video
                While SQLreader.Read
                    video = New MediaContainers.Video
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
                    If Not DBNull.Value.Equals(SQLreader("Video_FileSize")) Then video.Filesize = Convert.ToInt64(SQLreader("Video_FileSize"))
                    If Not DBNull.Value.Equals(SQLreader("Video_MultiViewLayout")) Then video.MultiViewLayout = SQLreader("Video_MultiViewLayout").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_StereoMode")) Then video.StereoMode = SQLreader("Video_StereoMode").ToString
                    _movieDB.Movie.FileInfo.StreamDetails.Video.Add(video)
                End While
            End Using
        End Using

        'Audio streams
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM MoviesAStreams WHERE MovieID = ", _movieDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim audio As MediaContainers.Audio
                While SQLreader.Read
                    audio = New MediaContainers.Audio
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
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM MoviesSubs WHERE MovieID = ", _movieDB.ID, " AND NOT Subs_Type = 'External';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaContainers.Subtitle
                While SQLreader.Read
                    subtitle = New MediaContainers.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.Type = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.Path = SQLreader("Subs_Path").ToString
                    subtitle.Forced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    _movieDB.Movie.FileInfo.StreamDetails.Subtitle.Add(subtitle)
                End While
            End Using
        End Using

        'external subtitles
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM MoviesSubs WHERE MovieID = ", _movieDB.ID, " AND Subs_Type = 'External';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaContainers.Subtitle
                While SQLreader.Read
                    subtitle = New MediaContainers.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.Type = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.Path = SQLreader("Subs_Path").ToString
                    subtitle.Forced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    _movieDB.Subtitles.Add(subtitle)
                End While
            End Using
        End Using

        'Studios
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strStudio FROM studiolinkmovie ",
                                                   "AS A INNER JOIN studio AS B ON (A.idStudio = B.idStudio) WHERE A.idMovie = ", _movieDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strStudio")) Then _movieDB.Movie.Studios.Add(SQLreader("strStudio").ToString)
                End While
            End Using
        End Using

        'Tags
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strTag FROM taglinks ",
                                                   "AS A INNER JOIN tag AS B ON (A.idTag = B.idTag) WHERE A.idMedia = ", _movieDB.ID, " AND A.media_type = 'movie';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strTag")) Then _movieDB.Movie.Tags.Add(SQLreader("strTag").ToString)
                End While
            End Using
        End Using

        'ImagesContainer
        _movieDB.ImagesContainer.Banner.LocalFilePath = GetArtForItem(_movieDB.ID, "movie", "banner")
        _movieDB.ImagesContainer.ClearArt.LocalFilePath = GetArtForItem(_movieDB.ID, "movie", "clearart")
        _movieDB.ImagesContainer.ClearLogo.LocalFilePath = GetArtForItem(_movieDB.ID, "movie", "clearlogo")
        _movieDB.ImagesContainer.DiscArt.LocalFilePath = GetArtForItem(_movieDB.ID, "movie", "discart")
        _movieDB.ImagesContainer.Fanart.LocalFilePath = GetArtForItem(_movieDB.ID, "movie", "fanart")
        _movieDB.ImagesContainer.Keyart.LocalFilePath = GetArtForItem(_movieDB.ID, "movie", "keyart")
        _movieDB.ImagesContainer.Landscape.LocalFilePath = GetArtForItem(_movieDB.ID, "movie", "landscape")
        _movieDB.ImagesContainer.Poster.LocalFilePath = GetArtForItem(_movieDB.ID, "movie", "poster")
        If Not String.IsNullOrEmpty(_movieDB.ExtrafanartsPath) AndAlso Directory.Exists(_movieDB.ExtrafanartsPath) Then
            For Each ePath As String In Directory.GetFiles(_movieDB.ExtrafanartsPath, "*.jpg")
                _movieDB.ImagesContainer.Extrafanarts.Add(New MediaContainers.Image With {.LocalFilePath = ePath})
            Next
        End If
        If Not String.IsNullOrEmpty(_movieDB.ExtrathumbsPath) AndAlso Directory.Exists(_movieDB.ExtrathumbsPath) Then
            Dim iIndex As Integer = 0
            For Each ePath As String In Directory.GetFiles(_movieDB.ExtrathumbsPath, "thumb*.jpg")
                _movieDB.ImagesContainer.Extrathumbs.Add(New MediaContainers.Image With {.Index = iIndex, .LocalFilePath = ePath})
                iIndex += 1
            Next
        End If

        'Check if the file is available and ready to edit
        _movieDB.IsOnline = File.Exists(_movieDB.Filename)

        Return _movieDB
    End Function

    ''' <summary>
    ''' Load all the information for a movie (by movie path)
    ''' </summary>
    ''' <param name="sPath">Full path to the movie file</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_Movie(ByVal sPath As String) As DBElement
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            ' One more Query Better then re-write all function again
            SQLcommand.CommandText = String.Concat("SELECT idMovie FROM movie WHERE MoviePath = ", sPath, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.Read Then
                    Return Load_Movie(Convert.ToInt64(SQLreader("idMovie")))
                End If
            End Using
        End Using

        Return New DBElement(Enums.ContentType.Movie)
    End Function

    ''' <summary>
    ''' Load all the information for a movieset.
    ''' </summary>
    ''' <param name="MovieSetID">ID of the movieset to load, as stored in the database</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_MovieSet(ByVal MovieSetID As Long) As DBElement
        Dim _moviesetDB As New DBElement(Enums.ContentType.MovieSet) With {
            .ID = MovieSetID,
            .MovieSet = New MediaContainers.Movieset
        }
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM sets WHERE idSet = ", MovieSetID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("ListTitle")) Then _moviesetDB.ListTitle = SQLreader("ListTitle").ToString
                    If Not DBNull.Value.Equals(SQLreader("NfoPath")) Then _moviesetDB.NfoPath = SQLreader("NfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("Language")) Then _moviesetDB.Language = SQLreader("Language").ToString

                    _moviesetDB.IsMark = Convert.ToBoolean(SQLreader("Mark"))
                    _moviesetDB.IsLock = Convert.ToBoolean(SQLreader("Lock"))
                    _moviesetDB.SortMethod = DirectCast(Convert.ToInt32(SQLreader("SortMethod")), Enums.SortMethod_MovieSet)

                    With _moviesetDB.MovieSet
                        If Not DBNull.Value.Equals(SQLreader("Plot")) Then .Plot = SQLreader("Plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("SetName")) Then .Title = SQLreader("SetName").ToString
                        If Not DBNull.Value.Equals(SQLreader("Language")) Then .Language = SQLreader("Language").ToString
                        .OldTitle = .Title
                    End With
                End If
            End Using
        End Using

        'Movies in Set
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not Master.eSettings.MovieScraperCollectionsYAMJCompatibleSets Then
                If _moviesetDB.SortMethod = Enums.SortMethod_MovieSet.Year Then
                    SQLcommand.CommandText = String.Concat("SELECT setlinkmovie.idMovie, setlinkmovie.iOrder FROM setlinkmovie INNER JOIN movie ON (setlinkmovie.idMovie = movie.idMovie) ",
                                                           "WHERE idSet = ", _moviesetDB.ID, " ORDER BY movie.Year;")
                ElseIf _moviesetDB.SortMethod = Enums.SortMethod_MovieSet.Title Then
                    SQLcommand.CommandText = String.Concat("SELECT setlinkmovie.idMovie, setlinkmovie.iOrder FROM setlinkmovie INNER JOIN movielist ON (setlinkmovie.idMovie = movielist.idMovie) ",
                                                           "WHERE idSet = ", _moviesetDB.ID, " ORDER BY movielist.SortedTitle COLLATE NOCASE;")
                End If
            Else
                SQLcommand.CommandText = String.Concat("SELECT setlinkmovie.idMovie, setlinkmovie.iOrder FROM setlinkmovie ",
                                                       "WHERE idSet = ", _moviesetDB.ID, " ORDER BY iOrder;")
            End If
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim i As Integer = 0
                While SQLreader.Read
                    _moviesetDB.MoviesInSet.Add(New MediaContainers.MovieInSet With {
                                                .DBMovie = Load_Movie(Convert.ToInt64(SQLreader("idMovie"))),
                                                .Order = i})
                    i += 1
                End While
            End Using
        End Using

        'ImagesContainer
        _moviesetDB.ImagesContainer.Banner.LocalFilePath = GetArtForItem(_moviesetDB.ID, "set", "banner")
        _moviesetDB.ImagesContainer.ClearArt.LocalFilePath = GetArtForItem(_moviesetDB.ID, "set", "clearart")
        _moviesetDB.ImagesContainer.ClearLogo.LocalFilePath = GetArtForItem(_moviesetDB.ID, "set", "clearlogo")
        _moviesetDB.ImagesContainer.DiscArt.LocalFilePath = GetArtForItem(_moviesetDB.ID, "set", "discart")
        _moviesetDB.ImagesContainer.Fanart.LocalFilePath = GetArtForItem(_moviesetDB.ID, "set", "fanart")
        _moviesetDB.ImagesContainer.Keyart.LocalFilePath = GetArtForItem(_moviesetDB.ID, "set", "keyart")
        _moviesetDB.ImagesContainer.Landscape.LocalFilePath = GetArtForItem(_moviesetDB.ID, "set", "landscape")
        _moviesetDB.ImagesContainer.Poster.LocalFilePath = GetArtForItem(_moviesetDB.ID, "set", "poster")

        'UniqueIDs
        _moviesetDB.MovieSet.UniqueIDs = GetUniqueIDsForItem(_moviesetDB.ID, _moviesetDB.ContentType)

        For Each tMovie In _moviesetDB.MoviesInSet
            If tMovie.DBMovie.IsOnline Then
                _moviesetDB.IsOnline = True
            Else
                _moviesetDB.IsOnline = False
                Exit For
            End If
        Next

        Return _moviesetDB
    End Function

    Public Function Load_Source_Movie(ByVal SourceID As Long) As DBSource
        Dim _source As New DBSource With {
            .ID = SourceID
        }
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM moviesource WHERE idSource = ", _source.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    _source.Name = SQLreader("strName").ToString
                    _source.Path = SQLreader("strPath").ToString
                    _source.Recursive = Convert.ToBoolean(SQLreader("bRecursive"))
                    _source.UseFolderName = Convert.ToBoolean(SQLreader("bFoldername"))
                    _source.IsSingle = Convert.ToBoolean(SQLreader("bSingle"))
                    _source.Exclude = Convert.ToBoolean(SQLreader("bExclude"))
                    _source.GetYear = Convert.ToBoolean(SQLreader("bGetYear"))
                    _source.Language = SQLreader("strLanguage").ToString
                    _source.LastScan = SQLreader("strLastScan").ToString
                End If
            End Using
        End Using

        Return _source
    End Function

    ''' <summary>
    ''' Load all the information for a movietag.
    ''' </summary>
    ''' <param name="TagID">ID of the movietag to load, as stored in the database</param>
    ''' <returns>Database.DBElementTag object</returns>
    Public Function Load_Tag_Movie(ByVal TagID As Integer) As Structures.DBMovieTag
        Dim _tagDB As New Structures.DBMovieTag
        _tagDB.ID = TagID
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM tag WHERE idTag = ", TagID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("strTag")) Then _tagDB.Title = SQLreader("strTag").ToString
                    If Not DBNull.Value.Equals(SQLreader("idTag")) Then _tagDB.ID = CInt(SQLreader("idTag"))
                End If
            End Using
        End Using

        _tagDB.Movies = New List(Of DBElement)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM taglinks ",
                        "WHERE idTag = ", _tagDB.ID, " AND media_type = 'movie';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    _tagDB.Movies.Add(Load_Movie(Convert.ToInt64(SQLreader("idMedia"))))
                End While
            End Using
        End Using
        Return _tagDB
    End Function

    Public Function Load_AllMoviesets() As List(Of DBElement)
        Dim lstDBELement As New List(Of DBElement)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idSet FROM sets ORDER BY SetName;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        lstDBELement.Add(Master.DB.Load_MovieSet(Convert.ToInt64(SQLreader("idSet"))))
                    End While
                End If
            End Using
        End Using
        Return lstDBELement
    End Function

    Public Function Load_AllTVEpisodes(ByVal ShowID As Long, ByVal withShow As Boolean, Optional ByVal OnlySeason As Integer = -1, Optional ByVal withMissingEpisodes As Boolean = False) As List(Of DBElement)
        If ShowID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & ShowID)

        Dim _TVEpisodesList As New List(Of DBElement)

        Using SQLCount As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            If OnlySeason = -1 Then
                SQLCount.CommandText = String.Concat("SELECT COUNT(idEpisode) AS eCount FROM episode WHERE idShow = ", ShowID, If(withMissingEpisodes, ";", " AND NOT idFile = -1;"))
            Else
                SQLCount.CommandText = String.Concat("SELECT COUNT(idEpisode) AS eCount FROM episode WHERE idShow = ", ShowID, " AND Season = ", OnlySeason, If(withMissingEpisodes, ";", " AND NOT idFile = -1;"))
            End If
            Using SQLRCount As SQLiteDataReader = SQLCount.ExecuteReader
                If SQLRCount.HasRows Then
                    SQLRCount.Read()
                    If Convert.ToInt32(SQLRCount("eCount")) > 0 Then
                        Using SQLCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            If OnlySeason = -1 Then
                                SQLCommand.CommandText = String.Concat("SELECT * FROM episode WHERE idShow = ", ShowID, If(withMissingEpisodes, ";", " AND NOT idFile = -1;"))
                            Else
                                SQLCommand.CommandText = String.Concat("SELECT * FROM episode WHERE idShow = ", ShowID, " AND Season = ", OnlySeason, If(withMissingEpisodes, ";", " AND NOT idFile = -1;"))
                            End If
                            Using SQLReader As SQLiteDataReader = SQLCommand.ExecuteReader
                                While SQLReader.Read
                                    _TVEpisodesList.Add(Master.DB.Load_TVEpisode(Convert.ToInt64(SQLReader("idEpisode")), withShow))
                                End While
                            End Using
                        End Using
                    End If
                End If
            End Using
        End Using

        Return _TVEpisodesList
    End Function

    Public Function Load_AllTVEpisodes_ByFileID(ByVal FileID As Long, ByVal withShow As Boolean) As List(Of DBElement)
        If FileID < 0 Then Throw New ArgumentOutOfRangeException("idFile", "Value must be >= 0, was given: " & FileID)

        Dim _TVEpisodesList As New List(Of DBElement)

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT idEpisode FROM episode WHERE idFile = {0};", FileID)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        _TVEpisodesList.Add(Master.DB.Load_TVEpisode(Convert.ToInt64(SQLreader("idEpisode")), withShow))
                    End While
                End If
            End Using
        End Using

        Return _TVEpisodesList
    End Function

    Public Function Load_AllTVSeasons(ByVal ShowID As Long) As List(Of DBElement)
        If ShowID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & ShowID)

        Dim _TVSeasonsList As New List(Of DBElement)

        Using SQLCount As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLCount.CommandText = String.Concat("SELECT COUNT(idSeason) AS eCount FROM seasons WHERE idShow = ", ShowID, ";")
            Using SQLRCount As SQLiteDataReader = SQLCount.ExecuteReader
                If SQLRCount.HasRows Then
                    SQLRCount.Read()
                    If Convert.ToInt32(SQLRCount("eCount")) > 0 Then
                        Using SQLCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand.CommandText = String.Concat("SELECT * FROM seasons WHERE idShow = ", ShowID, ";")
                            Using SQLReader As SQLiteDataReader = SQLCommand.ExecuteReader
                                While SQLReader.Read
                                    _TVSeasonsList.Add(Master.DB.Load_TVSeason(Convert.ToInt64(SQLReader("idSeason")), False, False))
                                End While
                            End Using
                        End Using
                    End If
                End If
            End Using
        End Using

        Return _TVSeasonsList
    End Function
    ''' <summary>
    ''' Load all the information for a TV Season by ShowID and Season #.
    ''' </summary>
    ''' <param name="ShowID">ID of the show to load, as stored in the database</param>
    ''' <returns>MediaContainers.SeasonDetails object</returns>
    ''' <remarks></remarks>
    Public Function Load_AllTVSeasonsDetails(ByVal ShowID As Long) As MediaContainers.Seasons
        If ShowID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & ShowID)

        Dim _SeasonList As New MediaContainers.Seasons

        Using SQLcommandTVSeason As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommandTVSeason.CommandText = String.Concat("SELECT * FROM seasons WHERE idShow = ", ShowID, " ORDER BY Season;")
            Using SQLReader As SQLiteDataReader = SQLcommandTVSeason.ExecuteReader
                While SQLReader.Read
                    Dim nSeason As New MediaContainers.SeasonDetails
                    If Not DBNull.Value.Equals(SQLReader("strAired")) Then nSeason.Aired = CStr(SQLReader("strAired"))
                    If Not DBNull.Value.Equals(SQLReader("strPlot")) Then nSeason.Plot = CStr(SQLReader("strPlot"))
                    If Not DBNull.Value.Equals(SQLReader("Season")) Then nSeason.Season = CInt(SQLReader("Season"))
                    If Not DBNull.Value.Equals(SQLReader("SeasonText")) Then nSeason.Title = CStr(SQLReader("SeasonText"))
                    If Not DBNull.Value.Equals(SQLReader("strTMDB")) Then nSeason.TMDbId = CStr(SQLReader("strTMDB"))
                    If Not DBNull.Value.Equals(SQLReader("strTVDB")) Then nSeason.TVDbId = CStr(SQLReader("strTVDB"))
                    _SeasonList.Seasons.Add(nSeason)
                End While
            End Using
        End Using

        Return _SeasonList
    End Function
    ''' <summary>
    ''' Load all the information for a TV Episode
    ''' </summary>
    ''' <param name="EpisodeID">Episode ID</param>
    ''' <param name="WithShow">>If <c>True</c>, also retrieve the TV Show information</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_TVEpisode(ByVal EpisodeID As Long, ByVal withShow As Boolean) As DBElement
        Dim _TVDB As New DBElement(Enums.ContentType.TVEpisode) With {
            .ID = EpisodeID,
            .TVEpisode = New MediaContainers.EpisodeDetails
        }
        Dim PathID As Long = -1

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM episode WHERE idEpisode = ", EpisodeID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("NfoPath")) Then _TVDB.NfoPath = SQLreader("NfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("idShow")) Then _TVDB.ShowID = Convert.ToInt64(SQLreader("idShow"))
                    If Not DBNull.Value.Equals(SQLreader("VideoSource")) Then _TVDB.VideoSource = SQLreader("VideoSource").ToString
                    PathID = Convert.ToInt64(SQLreader("idFile"))

                    _TVDB.Source = Load_Source_TVShow(Convert.ToInt64(SQLreader("idSource")))

                    _TVDB.FilenameID = PathID
                    _TVDB.IsMark = Convert.ToBoolean(SQLreader("Mark"))
                    _TVDB.IsLock = Convert.ToBoolean(SQLreader("Lock"))
                    _TVDB.ShowID = Convert.ToInt64(SQLreader("idShow"))
                    _TVDB.ShowPath = Load_Path_TVShow(Convert.ToInt64(SQLreader("idShow")))

                    With _TVDB.TVEpisode
                        If Not DBNull.Value.Equals(SQLreader("Title")) Then .Title = SQLreader("Title").ToString
                        If Not DBNull.Value.Equals(SQLreader("Season")) Then .Season = Convert.ToInt32(SQLreader("Season"))
                        If Not DBNull.Value.Equals(SQLreader("Episode")) Then .Episode = Convert.ToInt32(SQLreader("Episode"))
                        If Not DBNull.Value.Equals(SQLreader("DisplaySeason")) Then .DisplaySeason = Convert.ToInt32(SQLreader("DisplaySeason"))
                        If Not DBNull.Value.Equals(SQLreader("DisplayEpisode")) Then .DisplayEpisode = Convert.ToInt32(SQLreader("DisplayEpisode"))
                        If Not DBNull.Value.Equals(SQLreader("Aired")) Then .Aired = SQLreader("Aired").ToString
                        If Not DBNull.Value.Equals(SQLreader("Rating")) Then .Rating = SQLreader("Rating").ToString
                        If Not DBNull.Value.Equals(SQLreader("Plot")) Then .Plot = SQLreader("Plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("Playcount")) Then .Playcount = Convert.ToInt32(SQLreader("Playcount"))
                        If Not DBNull.Value.Equals(SQLreader("DateAdded")) Then .DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("DateAdded"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("Runtime")) Then .Runtime = SQLreader("Runtime").ToString
                        If Not DBNull.Value.Equals(SQLreader("Votes")) Then .Votes = SQLreader("Votes").ToString
                        If Not DBNull.Value.Equals(SQLreader("VideoSource")) Then .VideoSource = SQLreader("VideoSource").ToString
                        If Not DBNull.Value.Equals(SQLreader("SubEpisode")) Then .SubEpisode = Convert.ToInt32(SQLreader("SubEpisode"))
                        If Not DBNull.Value.Equals(SQLreader("iLastPlayed")) Then .LastPlayed = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("iLastPlayed"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("iUserRating")) Then .UserRating = Convert.ToInt32(SQLreader("iUserRating"))
                    End With
                End If
            End Using
        End Using

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT strFilename FROM files WHERE idFile = ", PathID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("strFilename")) Then _TVDB.Filename = SQLreader("strFilename").ToString
                End If
            End Using
        End Using

        'Actors
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT A.strRole, B.idActor, B.strActor, B.strThumb, C.url FROM actorlinkepisode AS A ",
                                                   "INNER JOIN actors AS B ON (A.idActor = B.idActor) ",
                                                   "LEFT OUTER JOIN art AS C ON (B.idActor = C.media_id AND C.media_type = 'actor' AND C.type = 'thumb') ",
                                                   "WHERE A.idEpisode = ", _TVDB.ID, " ",
                                                   "ORDER BY A.iOrder;")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim person As MediaContainers.Person
                While SQLreader.Read
                    person = New MediaContainers.Person
                    person.ID = Convert.ToInt64(SQLreader("idActor"))
                    person.Name = SQLreader("strActor").ToString
                    person.Role = SQLreader("strRole").ToString
                    person.LocalFilePath = SQLreader("url").ToString
                    person.URLOriginal = SQLreader("strThumb").ToString
                    _TVDB.TVEpisode.Actors.Add(person)
                End While
            End Using
        End Using

        'Credits
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strActor FROM writerlinkepisode ",
                                                   "AS A INNER JOIN actors AS B ON (A.idWriter = B.idActor) WHERE A.idEpisode = ", _TVDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strActor")) Then _TVDB.TVEpisode.Credits.Add(SQLreader("strActor").ToString)
                End While
            End Using
        End Using

        'Directors
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strActor FROM directorlinkepisode ",
                                                   "AS A INNER JOIN actors AS B ON (A.idDirector = B.idActor) WHERE A.idEpisode = ", _TVDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strActor")) Then _TVDB.TVEpisode.Directors.Add(SQLreader("strActor").ToString)
                End While
            End Using
        End Using

        'Guest Stars
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT A.strRole, B.idActor, B.strActor, B.strThumb, C.url FROM gueststarlinkepisode AS A ",
                                                   "INNER JOIN actors AS B ON (A.idActor = B.idActor) ",
                                                   "LEFT OUTER JOIN art AS C ON (B.idActor = C.media_id AND C.media_type = 'actor' AND C.type = 'thumb') ",
                                                   "WHERE A.idEpisode = ", _TVDB.ID, " ",
                                                   "ORDER BY A.iOrder;")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim person As MediaContainers.Person
                While SQLreader.Read
                    person = New MediaContainers.Person
                    person.ID = Convert.ToInt64(SQLreader("idActor"))
                    person.Name = SQLreader("strActor").ToString
                    person.Role = SQLreader("strRole").ToString
                    person.LocalFilePath = SQLreader("url").ToString
                    person.URLOriginal = SQLreader("strThumb").ToString
                    _TVDB.TVEpisode.GuestStars.Add(person)
                End While
            End Using
        End Using

        'Ratings
        _TVDB.TVEpisode.Ratings = GetRatingsForItem(_TVDB.ID, _TVDB.ContentType)

        'UniqueIDs
        _TVDB.TVEpisode.UniqueIDs = GetUniqueIDsForItem(_TVDB.ID, _TVDB.ContentType)

        'Video Streams
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM TVVStreams WHERE TVEpID = ", _TVDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim video As MediaContainers.Video
                While SQLreader.Read
                    video = New MediaContainers.Video
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
                    If Not DBNull.Value.Equals(SQLreader("Video_FileSize")) Then video.Filesize = Convert.ToInt64(SQLreader("Video_FileSize"))
                    If Not DBNull.Value.Equals(SQLreader("Video_MultiViewLayout")) Then video.MultiViewLayout = SQLreader("Video_MultiViewLayout").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_StereoMode")) Then video.StereoMode = SQLreader("Video_StereoMode").ToString
                    _TVDB.TVEpisode.FileInfo.StreamDetails.Video.Add(video)
                End While
            End Using
        End Using

        'Audio Streams
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM TVAStreams WHERE TVEpID = ", _TVDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim audio As MediaContainers.Audio
                While SQLreader.Read
                    audio = New MediaContainers.Audio
                    If Not DBNull.Value.Equals(SQLreader("Audio_Language")) Then audio.Language = SQLreader("Audio_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_LongLanguage")) Then audio.LongLanguage = SQLreader("Audio_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Codec")) Then audio.Codec = SQLreader("Audio_Codec").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Channel")) Then audio.Channels = SQLreader("Audio_Channel").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Bitrate")) Then audio.Bitrate = SQLreader("Audio_Bitrate").ToString
                    _TVDB.TVEpisode.FileInfo.StreamDetails.Audio.Add(audio)
                End While
            End Using
        End Using

        'embedded subtitles
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM TVSubs WHERE TVEpID = ", _TVDB.ID, " AND NOT Subs_Type = 'External';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaContainers.Subtitle
                While SQLreader.Read
                    subtitle = New MediaContainers.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.Type = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.Path = SQLreader("Subs_Path").ToString
                    subtitle.Forced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    _TVDB.TVEpisode.FileInfo.StreamDetails.Subtitle.Add(subtitle)
                End While
            End Using
        End Using

        'external subtitles
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM TVSubs WHERE TVEpID = ", _TVDB.ID, " AND Subs_Type = 'External';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaContainers.Subtitle
                While SQLreader.Read
                    subtitle = New MediaContainers.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.Type = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.Path = SQLreader("Subs_Path").ToString
                    subtitle.Forced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    _TVDB.Subtitles.Add(subtitle)
                End While
            End Using
        End Using

        'ImagesContainer
        _TVDB.ImagesContainer.Fanart.LocalFilePath = GetArtForItem(_TVDB.ID, "episode", "fanart")
        _TVDB.ImagesContainer.Poster.LocalFilePath = GetArtForItem(_TVDB.ID, "episode", "thumb")

        'Show container
        If withShow Then
            _TVDB = Master.DB.Load_TVShowInfoIntoDBElement(_TVDB)
        End If

        'Check if the file is available and ready to edit
        If File.Exists(_TVDB.Filename) Then _TVDB.IsOnline = True

        Return _TVDB
    End Function
    ''' <summary>
    ''' Load all the information for a TV Show
    ''' </summary>
    ''' <param name="ShowID">Show ID</param>
    ''' <returns>Database.DBElement object</returns>
    ''' <remarks></remarks>
    Public Function Load_TVShow_Full(ByVal ShowID As Long) As DBElement
        If ShowID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & ShowID)
        Return Master.DB.Load_TVShow(ShowID, True, True, True)
    End Function
    ''' <summary>
    ''' Load all the information for a TV Season
    ''' </summary>
    ''' <param name="SeasonID">Season ID</param>
    ''' <param name="WithShow">If <c>True</c>, also retrieve the TV Show information</param>
    ''' <returns>Database.DBElement object</returns>
    ''' <remarks></remarks>
    Public Function Load_TVSeason(ByVal SeasonID As Long, ByVal withShow As Boolean, ByVal withEpisodes As Boolean) As DBElement
        Dim _TVDB As New DBElement(Enums.ContentType.TVSeason) With {
            .ID = SeasonID,
            .TVSeason = New MediaContainers.SeasonDetails
        }
        Using SQLcommandTVSeason As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommandTVSeason.CommandText = String.Concat("SELECT * FROM seasons WHERE idSeason = ", _TVDB.ID, ";")
            Using SQLReader As SQLiteDataReader = SQLcommandTVSeason.ExecuteReader
                If SQLReader.HasRows Then
                    SQLReader.Read()
                    _TVDB.IsLock = CBool(SQLReader("Lock"))
                    _TVDB.IsMark = CBool(SQLReader("Mark"))
                    _TVDB.ShowID = Convert.ToInt64(SQLReader("idShow"))
                    _TVDB.ShowPath = Load_Path_TVShow(Convert.ToInt64(SQLReader("idShow")))

                    With _TVDB.TVSeason
                        If Not DBNull.Value.Equals(SQLReader("strAired")) Then .Aired = CStr(SQLReader("strAired"))
                        If Not DBNull.Value.Equals(SQLReader("strPlot")) Then .Plot = CStr(SQLReader("strPlot"))
                        If Not DBNull.Value.Equals(SQLReader("Season")) Then .Season = CInt(SQLReader("Season"))
                        If Not DBNull.Value.Equals(SQLReader("SeasonText")) Then .Title = CStr(SQLReader("SeasonText"))
                    End With
                End If
            End Using
        End Using

        'ImagesContainer
        _TVDB.ImagesContainer.Banner.LocalFilePath = GetArtForItem(_TVDB.ID, "season", "banner")
        _TVDB.ImagesContainer.Fanart.LocalFilePath = GetArtForItem(_TVDB.ID, "season", "fanart")
        _TVDB.ImagesContainer.Landscape.LocalFilePath = GetArtForItem(_TVDB.ID, "season", "landscape")
        _TVDB.ImagesContainer.Poster.LocalFilePath = GetArtForItem(_TVDB.ID, "season", "poster")

        'UniqueIDs
        _TVDB.TVSeason.UniqueIDs = GetUniqueIDsForItem(_TVDB.ID, _TVDB.ContentType)

        'Show container
        If withShow Then
            _TVDB = Master.DB.Load_TVShowInfoIntoDBElement(_TVDB)
        End If

        'Episodes
        If withEpisodes Then
            For Each tEpisode As DBElement In Load_AllTVEpisodes(_TVDB.ShowID, withShow, _TVDB.TVSeason.Season)
                tEpisode = Load_TVShowInfoIntoDBElement(tEpisode, _TVDB)
                _TVDB.Episodes.Add(tEpisode)
            Next
        End If

        Return _TVDB
    End Function
    ''' <summary>
    ''' Load all the information for a TV Show
    ''' </summary>
    ''' <param name="ShowID">Show ID</param>
    ''' <param name="iSeason">Season number</param>
    ''' <param name="withShow">If <c>True</c>, also retrieve the TV Show information</param>
    ''' <returns>Database.DBElement object</returns>
    ''' <remarks></remarks>
    Public Function Load_TVSeason(ByVal ShowID As Long, ByVal iSeason As Integer, ByVal withShow As Boolean, ByVal withEpisodes As Boolean) As DBElement
        If ShowID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & ShowID)

        Dim _TVDB As New DBElement(Enums.ContentType.TVSeason) With {
            .ShowID = ShowID
        }
        If withShow Then Load_TVShowInfoIntoDBElement(_TVDB)

        Using SQLcommandTVSeason As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommandTVSeason.CommandText = String.Concat("SELECT idSeason FROM seasons WHERE idShow = ", ShowID, " AND Season = ", iSeason, ";")
            Using SQLReader As SQLiteDataReader = SQLcommandTVSeason.ExecuteReader
                If SQLReader.HasRows Then
                    SQLReader.Read()
                    _TVDB = Load_TVSeason(CInt(SQLReader("idSeason")), withShow, withEpisodes)
                End If
            End Using
        End Using

        Return _TVDB
    End Function
    ''' <summary>
    ''' Load all the information for a TV Show
    ''' </summary>
    ''' <param name="ShowID">Show ID</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_TVShow(ByVal ShowID As Long, ByVal withSeasons As Boolean, ByVal withEpisodes As Boolean, Optional ByVal withMissingEpisodes As Boolean = False) As DBElement
        If ShowID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & ShowID)

        Dim _TVDB As New DBElement(Enums.ContentType.TVShow) With {
            .ID = ShowID,
            .ShowID = ShowID,
            .TVShow = New MediaContainers.TVShow
        }

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM tvshow WHERE idShow = ", ShowID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("ListTitle")) Then _TVDB.ListTitle = SQLreader("ListTitle").ToString
                    If Not DBNull.Value.Equals(SQLreader("EFanartsPath")) Then _TVDB.ExtrafanartsPath = SQLreader("EFanartsPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("Language")) Then _TVDB.Language = SQLreader("Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("NfoPath")) Then _TVDB.NfoPath = SQLreader("NfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("TVShowPath")) Then _TVDB.ShowPath = SQLreader("TVShowPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("ThemePath")) Then _TVDB.Theme.LocalFilePath = SQLreader("ThemePath").ToString

                    _TVDB.Source = Load_Source_TVShow(Convert.ToInt64(SQLreader("idSource")))

                    _TVDB.IsMark = Convert.ToBoolean(SQLreader("Mark"))
                    _TVDB.IsLock = Convert.ToBoolean(SQLreader("Lock"))
                    _TVDB.Ordering = DirectCast(Convert.ToInt32(SQLreader("Ordering")), Enums.EpisodeOrdering)
                    _TVDB.EpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("EpisodeSorting")), Enums.EpisodeSorting)

                    With _TVDB.TVShow
                        If Not DBNull.Value.Equals(SQLreader("Title")) Then .Title = SQLreader("Title").ToString
                        If Not DBNull.Value.Equals(SQLreader("EpisodeGuide")) Then .EpisodeGuide.URL = SQLreader("EpisodeGuide").ToString
                        If Not DBNull.Value.Equals(SQLreader("Plot")) Then .Plot = SQLreader("Plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("Premiered")) Then .Premiered = SQLreader("Premiered").ToString
                        If Not DBNull.Value.Equals(SQLreader("MPAA")) Then .MPAA = SQLreader("MPAA").ToString
                        If Not DBNull.Value.Equals(SQLreader("Rating")) Then .Rating = SQLreader("Rating").ToString
                        If Not DBNull.Value.Equals(SQLreader("Status")) Then .Status = SQLreader("Status").ToString
                        If Not DBNull.Value.Equals(SQLreader("Runtime")) Then .Runtime = SQLreader("Runtime").ToString
                        If Not DBNull.Value.Equals(SQLreader("Votes")) Then .Votes = SQLreader("Votes").ToString
                        If Not DBNull.Value.Equals(SQLreader("SortTitle")) Then .SortTitle = SQLreader("SortTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("Language")) Then .Language = SQLreader("Language").ToString
                        If Not DBNull.Value.Equals(SQLreader("strOriginalTitle")) Then .OriginalTitle = SQLreader("strOriginalTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("iUserRating")) Then .UserRating = Convert.ToInt32(SQLreader("iUserRating"))
                    End With
                End If
            End Using
        End Using

        'Actors
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT A.strRole, B.idActor, B.strActor, B.strThumb, C.url FROM actorlinktvshow AS A ",
                                                   "INNER JOIN actors AS B ON (A.idActor = B.idActor) ",
                                                   "LEFT OUTER JOIN art AS C ON (B.idActor = C.media_id AND C.media_type = 'actor' AND C.type = 'thumb') ",
                                                   "WHERE A.idShow = ", _TVDB.ID, " ",
                                                   "ORDER BY A.iOrder;")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim actor As MediaContainers.Person
                While SQLreader.Read
                    actor = New MediaContainers.Person
                    actor.ID = Convert.ToInt64(SQLreader("idActor"))
                    actor.Name = SQLreader("strActor").ToString
                    actor.Role = SQLreader("strRole").ToString
                    actor.LocalFilePath = SQLreader("url").ToString
                    actor.URLOriginal = SQLreader("strThumb").ToString
                    _TVDB.TVShow.Actors.Add(actor)
                End While
            End Using
        End Using

        'Countries
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT country.strCountry ",
                                                   "FROM country ",
                                                   "INNER JOIN countrylinktvshow ON (country.idCountry = countrylinktvshow.idCountry) ",
                                                   "WHERE countrylinktvshow.idShow = ", _TVDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    _TVDB.TVShow.Countries.Add(SQLreader("strCountry").ToString)
                End While
            End Using
        End Using

        'Creators
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT actors.strActor ",
                                                   "FROM actors ",
                                                   "INNER JOIN creatorlinktvshow ON (actors.idActor = creatorlinktvshow.idActor) ",
                                                   "WHERE creatorlinktvshow.idShow = ", _TVDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    _TVDB.TVShow.Creators.Add(SQLreader("strActor").ToString)
                End While
            End Using
        End Using

        'Directors
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT actors.strActor ",
                                                   "FROM actors ",
                                                   "INNER JOIN directorlinktvshow ON (actors.idActor = directorlinktvshow.idDirector) ",
                                                   "WHERE directorlinktvshow.idShow = ", _TVDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strActor")) Then _TVDB.TVShow.Directors.Add(SQLreader("strActor").ToString)
                End While
            End Using
        End Using

        'Genres
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT genre.strGenre ",
                                                   "FROM genre ",
                                                   "INNER JOIN genrelinktvshow ON (genre.idGenre = genrelinktvshow.idGenre) ",
                                                   "WHERE genrelinktvshow.idShow = ", _TVDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strGenre")) Then _TVDB.TVShow.Genres.Add(SQLreader("strGenre").ToString)
                End While
            End Using
        End Using

        'Ratings
        _TVDB.TVShow.Ratings = GetRatingsForItem(_TVDB.ID, _TVDB.ContentType)

        'Studios
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT studio.strStudio ",
                                                   "FROM studio ",
                                                   "INNER JOIN studiolinktvshow ON (studio.idStudio = studiolinktvshow.idStudio) ",
                                                   "WHERE studiolinktvshow.idShow = ", _TVDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strStudio")) Then _TVDB.TVShow.Studios.Add(SQLreader("strStudio").ToString)
                End While
            End Using
        End Using

        'Tags
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strTag FROM taglinks ",
                                                   "AS A INNER JOIN tag AS B ON (A.idTag = B.idTag) WHERE A.idMedia = ", _TVDB.ID, " And A.media_type = 'tvshow';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim tag As String
                While SQLreader.Read
                    tag = String.Empty
                    If Not DBNull.Value.Equals(SQLreader("strTag")) Then tag = SQLreader("strTag").ToString
                    _TVDB.TVShow.Tags.Add(tag)
                End While
            End Using
        End Using

        'UniqueIDs
        _TVDB.TVShow.UniqueIDs = GetUniqueIDsForItem(_TVDB.ID, _TVDB.ContentType)

        'ImagesContainer
        _TVDB.ImagesContainer.Banner.LocalFilePath = GetArtForItem(_TVDB.ID, "tvshow", "banner")
        _TVDB.ImagesContainer.CharacterArt.LocalFilePath = GetArtForItem(_TVDB.ID, "tvshow", "characterart")
        _TVDB.ImagesContainer.ClearArt.LocalFilePath = GetArtForItem(_TVDB.ID, "tvshow", "clearart")
        _TVDB.ImagesContainer.ClearLogo.LocalFilePath = GetArtForItem(_TVDB.ID, "tvshow", "clearlogo")
        _TVDB.ImagesContainer.Fanart.LocalFilePath = GetArtForItem(_TVDB.ID, "tvshow", "fanart")
        _TVDB.ImagesContainer.Keyart.LocalFilePath = GetArtForItem(_TVDB.ID, "tvshow", "keyart")
        _TVDB.ImagesContainer.Landscape.LocalFilePath = GetArtForItem(_TVDB.ID, "tvshow", "landscape")
        _TVDB.ImagesContainer.Poster.LocalFilePath = GetArtForItem(_TVDB.ID, "tvshow", "poster")
        If Not String.IsNullOrEmpty(_TVDB.ExtrafanartsPath) AndAlso Directory.Exists(_TVDB.ExtrafanartsPath) Then
            For Each ePath As String In Directory.GetFiles(_TVDB.ExtrafanartsPath, "*.jpg")
                _TVDB.ImagesContainer.Extrafanarts.Add(New MediaContainers.Image With {.LocalFilePath = ePath})
            Next
        End If

        'Seasons
        If withSeasons Then
            For Each tSeason As DBElement In Load_AllTVSeasons(_TVDB.ID)
                tSeason = Load_TVShowInfoIntoDBElement(tSeason, _TVDB)
                _TVDB.Seasons.Add(tSeason)
                _TVDB.TVShow.Seasons.Seasons.Add(tSeason.TVSeason)
            Next
            '_TVDB.TVShow.Seasons = LoadAllTVSeasonsDetailsFromDB(_TVDB.ID)
        End If

        'Episodes
        If withEpisodes Then
            For Each tEpisode As DBElement In Load_AllTVEpisodes(_TVDB.ID, False, -1, withMissingEpisodes)
                tEpisode = Load_TVShowInfoIntoDBElement(tEpisode, _TVDB)
                _TVDB.Episodes.Add(tEpisode)
            Next
        End If

        'Check if the path is available and ready to edit
        If Directory.Exists(_TVDB.ShowPath) Then _TVDB.IsOnline = True

        Return _TVDB
    End Function
    ''' <summary>
    ''' Adds TVShow informations to a Database.DBElement
    ''' </summary>
    ''' <param name="episodeOrSeasonElement">Database.DBElement container to fill with TVShow informations</param>
    ''' <param name="tvshow">Optional the TVShow informations to add to _TVDB</param>
    ''' <remarks></remarks>
    Public Function Load_TVShowInfoIntoDBElement(ByVal episodeOrSeasonElement As DBElement, Optional ByVal tvshow As DBElement = Nothing) As DBElement
        Dim nTvShow As DBElement

        If tvshow Is Nothing OrElse tvshow.TVShow Is Nothing Then
            nTvShow = Load_TVShow(episodeOrSeasonElement.ShowID, False, False)
        Else
            nTvShow = tvshow
        End If

        episodeOrSeasonElement.EpisodeSorting = nTvShow.EpisodeSorting
        episodeOrSeasonElement.Ordering = nTvShow.Ordering
        episodeOrSeasonElement.Language = nTvShow.Language
        episodeOrSeasonElement.ShowID = nTvShow.ShowID
        episodeOrSeasonElement.ShowPath = nTvShow.ShowPath
        episodeOrSeasonElement.Source = nTvShow.Source
        episodeOrSeasonElement.TVShow = nTvShow.TVShow
        Return episodeOrSeasonElement
    End Function

    Public Function Load_Path_TVShow(ByVal ShowID As Long) As String
        Dim ShowPath As String = String.Empty

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT TVShowPath FROM tvshow WHERE idShow = ", ShowID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    ShowPath = SQLreader("TVShowPath").ToString
                End If
            End Using
        End Using

        Return ShowPath
    End Function

    Public Function Load_Source_TVShow(ByVal SourceID As Long) As DBSource
        Dim _source As New DBSource

        _source.ID = SourceID
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM tvshowsource WHERE idSource = ", _source.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    _source.Name = SQLreader("strName").ToString
                    _source.Path = SQLreader("strPath").ToString
                    _source.Language = SQLreader("strLanguage").ToString
                    _source.Ordering = DirectCast(Convert.ToInt32(SQLreader("iOrdering")), Enums.EpisodeOrdering)
                    _source.Exclude = Convert.ToBoolean(SQLreader("bExclude"))
                    _source.EpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("iEpisodeSorting")), Enums.EpisodeSorting)
                    _source.LastScan = SQLreader("strLastScan").ToString
                    _source.IsSingle = Convert.ToBoolean(SQLreader("bSingle"))
                End If
            End Using
        End Using

        Return _source
    End Function

    Private Sub bwPatchDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwPatchDB.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)

        Dim xmlSer As XmlSerializer
        Dim _cmds As New Containers.InstallCommands
        Dim TransOk As Boolean
        Dim tempName As String = String.Empty

        tempName = String.Concat(Args.newDBPath, "_tmp")
        If File.Exists(tempName) Then
            File.Delete(tempName)
        End If
        File.Copy(Args.currDBPath, tempName)

        Try
            _myvideosDBConn = New SQLiteConnection(String.Format(_connStringTemplate, tempName))
            _myvideosDBConn.Open()

            For i As Integer = Args.currVersion To Args.newVersion - 1

                Dim patchpath As String = FileUtils.Common.ReturnSettingsFile("DB", String.Format("MyVideosDBSQL_v{0}_Patch.xml", i))

                xmlSer = New XmlSerializer(GetType(Containers.InstallCommands))
                Using xmlSW As New StreamReader(Path.Combine(Functions.AppPath, patchpath))
                    _cmds = DirectCast(xmlSer.Deserialize(xmlSW), Containers.InstallCommands)
                End Using

                For Each Trans In _cmds.transaction
                    TransOk = True
                    Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                        For Each _cmd As Containers.CommandsTransactionCommand In Trans.command
                            If _cmd.type = "DB" Then
                                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    SQLcommand.CommandText = _cmd.execute
                                    Try
                                        SQLcommand.ExecuteNonQuery()
                                        logger.Info(String.Concat(Trans.name, ": ", _cmd.description))
                                    Catch ex As Exception
                                        logger.Error(New StackFrame().GetMethod().Name, ex, Trans.name, _cmd.description)
                                        TransOk = False
                                        Exit For
                                    End Try
                                End Using
                            End If
                        Next
                        If TransOk Then
                            logger.Trace(String.Format("Transaction {0} Commit Done", Trans.name))
                            SQLtransaction.Commit()
                            ' Housekeeping - consolidate and pack database using vacuum command http://www.sqlite.org/lang_vacuum.html
                            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
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
                        Using SQLnotransaction As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            SQLnotransaction.CommandText = _cmd.execute
                            Try
                                SQLnotransaction.ExecuteNonQuery()
                                ' Housekeeping - consolidate and pack database using vacuum command http://www.sqlite.org/lang_vacuum.html
                                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
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

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 14
                        PrepareTable_country("idMovie", "movie", True)
                        PrepareTable_director("idEpisode", "episode", True)
                        PrepareTable_director("idMovie", "movie", True)
                        PrepareTable_genre("idMovie", "movie", True)
                        PrepareTable_genre("idShow", "tvshow", True)
                        PrepareTable_studio("idMovie", "movie", True)
                        PrepareTable_studio("idShow", "tvshow", True)
                        PrepareTable_writer("idEpisode", "episode", True)
                        PrepareTable_writer("idMovie", "movie", True)
                        Prepare_Playcounts("episode", True)
                        Prepare_Playcounts("movie", True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 18
                        Prepare_VotesCount("idEpisode", "episode", True)
                        Prepare_VotesCount("idMovie", "movie", True)
                        Prepare_VotesCount("idShow", "tvshow", True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 21
                        Prepare_SortTitle("tvshow", True)
                        Prepare_DisplayEpisodeSeason(True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 26
                        Prepare_EFanartsPath("idMovie", "movie", True)
                        Prepare_EThumbsPath("idMovie", "movie", True)
                        Prepare_EFanartsPath("idShow", "tvshow", True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 30
                        Prepare_Language("movie", True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 31
                        Prepare_Language("sets", True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 40
                        Prepare_IMDB(True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 41
                        Prepare_Top250(True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 42
                        Prepare_OrphanedLinks(True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 43
                        If MessageBox.Show("Locked state will now be saved in NFO. Do you want to rewrite all NFOs of locked items?", "Rewrite NFOs of locked items", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Prepare_LockedStateToNFO(True)
                        End If
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 44
                        Prepare_Sources("moviesource", True)
                        Prepare_Sources("tvshowsource", True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 45
                        Prepare_AllSeasonsEntries(True)
                End Select

                SQLtransaction.Commit()
            End Using

            _myvideosDBConn.Close()
            File.Move(tempName, Args.newDBPath)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Unable to open media database connection.")
            _myvideosDBConn.Close()
        End Try
    End Sub

    Private Sub bwPatchDB_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwPatchDB.ProgressChanged
        If e.ProgressPercentage = -1 Then
            logger.Info(e.UserState.ToString)
            Master.fLoading.SetLoadingMesg(e.UserState.ToString)
        End If
    End Sub

    Private Sub bwPatchDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwPatchDB.RunWorkerCompleted
        Return
    End Sub
    ''' <summary>
    ''' Execute arbitrary SQL commands against the database. Commands are retrieved from fname. 
    ''' Commands are serialized Containers.InstallCommands. Only commands marked as CommandType DB are executed.
    ''' </summary>
    ''' <param name="cPath">path to current DB</param>
    ''' <param name="nPath">path for new DB</param>
    ''' <param name="cVersion">current version of DB to patch</param>
    ''' <param name="nVersion">lastest version of DB</param>
    ''' <remarks></remarks>
    Public Sub Patch_MyVideos(ByVal cPath As String, ByVal nPath As String, ByVal cVersion As Integer, ByVal nVersion As Integer)

        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)

        bwPatchDB = New System.ComponentModel.BackgroundWorker
        bwPatchDB.WorkerReportsProgress = True
        bwPatchDB.WorkerSupportsCancellation = False
        bwPatchDB.RunWorkerAsync(New Arguments With {.currDBPath = cPath, .currVersion = cVersion, .newDBPath = nPath, .newVersion = nVersion})

        While bwPatchDB.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub Prepare_AllSeasonsEntries(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing ""* All Seasons"" entries...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "UPDATE seasons SET Season = -1 WHERE Season = 999;"
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub PrepareTable_country(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Get countries...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT {0}, country FROM {1};", idField, table)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("country")) AndAlso Not String.IsNullOrEmpty(SQLreader("country").ToString) Then
                        Dim valuelist As New List(Of String)
                        Dim strValue As String = SQLreader("country").ToString
                        Dim idMedia As Long = Convert.ToInt64(SQLreader(idField))

                        If strValue.Contains("/") Then
                            Dim values As String() = strValue.Split(New [Char]() {"/"c})
                            For Each value As String In values
                                value = value.Trim
                                If Not valuelist.Contains(value) Then
                                    valuelist.Add(value)
                                End If
                            Next
                        Else
                            strValue = strValue.Trim
                            If Not valuelist.Contains(strValue) Then
                                valuelist.Add(strValue.Trim)
                            End If
                        End If

                        For Each value As String In valuelist
                            Select Case table
                                Case "movie"
                                    AddCountryToMovie(idMedia, AddCountry(value))
                            End Select
                        Next
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub PrepareTable_director(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Get directors...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT {0}, director FROM {1};", idField, table)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("director")) AndAlso Not String.IsNullOrEmpty(SQLreader("director").ToString) Then
                        Dim valuelist As New List(Of String)
                        Dim strValue As String = SQLreader("director").ToString
                        Dim idMedia As Long = Convert.ToInt64(SQLreader(idField))

                        If strValue.Contains("/") Then
                            Dim values As String() = strValue.Split(New [Char]() {"/"c})
                            For Each value As String In values
                                value = value.Trim
                                If Not valuelist.Contains(value) Then
                                    valuelist.Add(value)
                                End If
                            Next
                        Else
                            strValue = strValue.Trim
                            If Not valuelist.Contains(strValue) Then
                                valuelist.Add(strValue.Trim)
                            End If
                        End If

                        For Each value As String In valuelist
                            Select Case table
                                Case "episode"
                                    AddDirectorToEpisode(idMedia, AddActor(value, "", "", "", "", False))
                                Case "movie"
                                    AddDirectorToMovie(idMedia, AddActor(value, "", "", "", "", False))
                                Case "tvshow"
                                    AddDirectorToTvShow(idMedia, AddActor(value, "", "", "", "", False))
                            End Select
                        Next
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub PrepareTable_genre(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Get genres...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT {0}, genre FROM {1};", idField, table)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("genre")) AndAlso Not String.IsNullOrEmpty(SQLreader("genre").ToString) Then
                        Dim valuelist As New List(Of String)
                        Dim strValue As String = SQLreader("genre").ToString
                        Dim idMedia As Long = Convert.ToInt64(SQLreader(idField))

                        If strValue.Contains("/") Then
                            Dim values As String() = strValue.Split(New [Char]() {"/"c})
                            For Each value As String In values
                                value = value.Trim
                                If Not valuelist.Contains(value) Then
                                    valuelist.Add(value)
                                End If
                            Next
                        Else
                            strValue = strValue.Trim
                            If Not valuelist.Contains(strValue) Then
                                valuelist.Add(strValue.Trim)
                            End If
                        End If

                        For Each value As String In valuelist
                            Select Case table
                                Case "movie"
                                    AddGenreToMovie(idMedia, AddGenre(value))
                                Case "tvshow"
                                    AddGenreToTvShow(idMedia, AddGenre(value))
                            End Select
                        Next
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub PrepareTable_studio(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Get studios...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT {0}, studio FROM {1};", idField, table)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("studio")) AndAlso Not String.IsNullOrEmpty(SQLreader("studio").ToString) Then
                        Dim valuelist As New List(Of String)
                        Dim strValue As String = SQLreader("studio").ToString
                        Dim idMedia As Long = Convert.ToInt64(SQLreader(idField))

                        If strValue.Contains("/") Then
                            Dim values As String() = strValue.Split(New [Char]() {"/"c})
                            For Each value As String In values
                                value = value.Trim
                                If Not valuelist.Contains(value) Then
                                    valuelist.Add(value)
                                End If
                            Next
                        Else
                            strValue = strValue.Trim
                            If Not valuelist.Contains(strValue) Then
                                valuelist.Add(strValue.Trim)
                            End If
                        End If

                        For Each value As String In valuelist
                            Select Case table
                                Case "movie"
                                    AddStudioToMovie(idMedia, AddStudio(value))
                                Case "tvshow"
                                    AddStudioToTvShow(idMedia, AddStudio(value))
                            End Select
                        Next
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub PrepareTable_writer(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Get writers...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT {0}, credits FROM {1};", idField, table)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("credits")) AndAlso Not String.IsNullOrEmpty(SQLreader("credits").ToString) Then
                        Dim valuelist As New List(Of String)
                        Dim strValue As String = SQLreader("credits").ToString
                        Dim idMedia As Long = Convert.ToInt64(SQLreader(idField))

                        If strValue.Contains("/") Then
                            Dim values As String() = strValue.Split(New [Char]() {"/"c})
                            For Each value As String In values
                                value = value.Trim
                                If Not valuelist.Contains(value) Then
                                    valuelist.Add(value)
                                End If
                            Next
                        Else
                            strValue = strValue.Trim
                            If Not valuelist.Contains(strValue) Then
                                valuelist.Add(strValue.Trim)
                            End If
                        End If

                        For Each value As String In valuelist
                            Select Case table
                                Case "episode"
                                    AddWriterToEpisode(idMedia, AddActor(value, "", "", "", "", False))
                                Case "movie"
                                    AddWriterToMovie(idMedia, AddActor(value, "", "", "", "", False))
                            End Select
                        Next
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Prepare_DisplayEpisodeSeason(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing DisplayEpisode and DisplaySeason...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "UPDATE episode SET DisplayEpisode = -1 WHERE DisplayEpisode IS NULL;"
            SQLcommand.ExecuteNonQuery()
            SQLcommand.CommandText = "UPDATE episode SET DisplaySeason = -1 WHERE DisplaySeason IS NULL;"
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Prepare_EFanartsPath(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing Extrafanarts Paths...")
        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT * FROM {0} WHERE EFanartsPath NOT LIKE ''", table)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Dim newExtrafanartsPath As String = String.Empty
                    If Not DBNull.Value.Equals(SQLreader("EFanartsPath")) Then newExtrafanartsPath = SQLreader("EFanartsPath").ToString
                    newExtrafanartsPath = Directory.GetParent(newExtrafanartsPath).FullName
                    Using SQLcommand_update_paths As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        SQLcommand_update_paths.CommandText = String.Format("UPDATE {0} SET EFanartsPath=? WHERE {1}={2}", table, idField, SQLreader(idField))
                        Dim par_ExtrafanartsPath As SQLiteParameter = SQLcommand_update_paths.Parameters.Add("par_EFanartsPath", DbType.String, 0, "EFanartsPath")
                        par_ExtrafanartsPath.Value = newExtrafanartsPath
                        SQLcommand_update_paths.ExecuteNonQuery()
                    End Using
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Prepare_EThumbsPath(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing ExtrathumbsPaths...")
        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT * FROM {0} WHERE EThumbsPath NOT LIKE ''", table)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Dim newExtrathumbsPath As String = String.Empty
                    If Not DBNull.Value.Equals(SQLreader("EThumbsPath")) Then newExtrathumbsPath = SQLreader("EThumbsPath").ToString
                    newExtrathumbsPath = Directory.GetParent(newExtrathumbsPath).FullName
                    Using SQLcommand_update_paths As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        SQLcommand_update_paths.CommandText = String.Format("UPDATE {0} SET EThumbsPath=? WHERE {1}={2}", table, idField, SQLreader(idField))
                        Dim par_ExtrathumbsPath As SQLiteParameter = SQLcommand_update_paths.Parameters.Add("par_EThumbsPath", DbType.String, 0, "EThumbsPath")
                        par_ExtrathumbsPath.Value = newExtrathumbsPath
                        SQLcommand_update_paths.ExecuteNonQuery()
                    End Using
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Prepare_Language(ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Set all languages to ""en-US"" ...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("UPDATE {0} SET Language = 'en-US';", table)
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Prepare_IMDB(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Cleanup all IMDB ID's ...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT idMovie, Imdb FROM movie WHERE movie.Imdb <> '';"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("Imdb")) AndAlso Not String.IsNullOrEmpty(SQLreader("Imdb").ToString) AndAlso Not SQLreader("Imdb").ToString.StartsWith("tt") Then
                        Using SQLcommand_cleanup_imdb As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            SQLcommand_cleanup_imdb.CommandText = String.Format("UPDATE movie SET Imdb=? WHERE idMovie={0}", SQLreader("idMovie").ToString)
                            Dim par_Imdb As SQLiteParameter = SQLcommand_cleanup_imdb.Parameters.Add("par_Imdb", DbType.String, 0, "Imdb")
                            par_Imdb.Value = String.Concat("tt", SQLreader("Imdb").ToString)
                            SQLcommand_cleanup_imdb.ExecuteNonQuery()
                        End Using
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Prepare_LockedStateToNFO(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Rewriting NFOs...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        'Movies
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT idMovie FROM movie WHERE Lock = 1;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Dim tmpDBElement As DBElement = Load_Movie(Convert.ToInt64(SQLreader("idMovie")))
                    Save_Movie(tmpDBElement, BatchMode, True, False, False, False)
                End While
            End Using
        End Using

        'TVEpsiodes
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT idEpisode FROM episode WHERE Lock = 1;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Dim tmpDBElement As DBElement = Load_TVEpisode(Convert.ToInt64(SQLreader("idEpisode")), False)
                    Save_TVEpisode(tmpDBElement, BatchMode, True, False, False, False)
                End While
            End Using
        End Using

        'TVShows
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT idShow FROM tvshow WHERE Lock = 1;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Dim tmpDBElement As DBElement = Load_TVShow(Convert.ToInt64(SQLreader("idShow")), False, False)
                    Save_TVShow(tmpDBElement, BatchMode, True, False, False)
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Prepare_OrphanedLinks(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Removing orphaned links...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            bwPatchDB.ReportProgress(-1, "Cleaning movie table")
            SQLcommand.CommandText = "DELETE FROM movie WHERE idSource NOT IN (SELECT idSource FROM moviesource);"
            SQLcommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning tvshow table")
            SQLcommand.CommandText = "DELETE FROM tvshow WHERE idSource NOT IN (SELECT idSource FROM tvshowsource);"
            SQLcommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning actorlinkmovie table")
            SQLcommand.CommandText = "DELETE FROM actorlinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            SQLcommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning art table")
            SQLcommand.CommandText = "DELETE FROM art WHERE media_id NOT IN (SELECT idMovie FROM movie) AND media_type = 'movie';"
            SQLcommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning countrylinkmovie table")
            SQLcommand.CommandText = "DELETE FROM countrylinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            SQLcommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning directorlinkmovie table")
            SQLcommand.CommandText = "DELETE FROM directorlinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            SQLcommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning genrelinkmovie table")
            SQLcommand.CommandText = "DELETE FROM genrelinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            SQLcommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning movielinktvshow table")
            SQLcommand.CommandText = "DELETE FROM movielinktvshow WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            SQLcommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning setlinkmovie table")
            SQLcommand.CommandText = "DELETE FROM setlinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            SQLcommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning studiolinkmovie table")
            SQLcommand.CommandText = "DELETE FROM studiolinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            SQLcommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning taglinks table")
            SQLcommand.CommandText = "DELETE FROM taglinks WHERE idMedia NOT IN (SELECT idMovie FROM movie) AND media_type = 'movie';"
            SQLcommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning writerlinkmovie table")
            SQLcommand.CommandText = "DELETE FROM writerlinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            SQLcommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning MoviesAStreams table")
            SQLcommand.CommandText = "DELETE FROM MoviesAStreams WHERE MovieID NOT IN (SELECT idMovie FROM movie);"
            SQLcommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning MoviesSubs table")
            SQLcommand.CommandText = "DELETE FROM MoviesSubs WHERE MovieID NOT IN (SELECT idMovie FROM movie);"
            SQLcommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning MoviesVStreams table")
            SQLcommand.CommandText = "DELETE FROM MoviesVStreams WHERE MovieID NOT IN (SELECT idMovie FROM movie);"
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Prepare_Playcounts(ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing Playcounts...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("UPDATE {0} SET Playcount = NULL WHERE Playcount = 0 OR Playcount = """";", table)
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Prepare_SortTitle(ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing SortTitles...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("UPDATE {0} SET SortTitle = '' WHERE SortTitle IS NULL OR SortTitle = """";", table)
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Prepare_Sources(ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing sources...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT idSource, strPath FROM {0};", table)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Regex.Match(SQLreader("strPath").ToString, "([a-z]:|[A-Z]:)$").Success Then
                        Using SQLcommand_update_votes As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            SQLcommand_update_votes.CommandText = String.Format("UPDATE {0} SET strPath=""{1}\"" WHERE idSource={2}", table, SQLreader("strPath").ToString, SQLreader("idSource").ToString)
                            SQLcommand_update_votes.ExecuteNonQuery()
                        End Using
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Prepare_Top250(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing Top250...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "UPDATE movie SET Top250 = NULL WHERE Top250 = 0 OR Top250 = """";"
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Prepare_VotesCount(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Clean Votes count...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT {0}, Votes FROM {1};", idField, table)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("Votes")) AndAlso Not String.IsNullOrEmpty(SQLreader("Votes").ToString) AndAlso Not Integer.TryParse(SQLreader("Votes").ToString, 0) Then
                        Using SQLcommand_update_votes As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            SQLcommand_update_votes.CommandText = String.Format("UPDATE {0} SET Votes=? WHERE {1}={2}", table, idField, SQLreader(idField))
                            Dim par_update_Votes As SQLiteParameter = SQLcommand_update_votes.Parameters.Add("par_update_Votes", DbType.String, 0, "Vote")
                            par_update_Votes.Value = NumUtils.CleanVotes(SQLreader("Votes").ToString)
                            SQLcommand_update_votes.ExecuteNonQuery()
                        End Using
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub RemoveMoviesetFromMovie(ByVal idMovie As Long)
        RemoveFromTable("setlinkmovie", "idMovie", idMovie)
    End Sub

    Private Sub RemoveRatingsFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("rating", idMedia, contentType)
    End Sub

    Private Sub RemoveUniqueIDsFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("uniqueid", idMedia, contentType)
    End Sub

    Private Sub RemoveFromTable(ByVal table As String, ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("DELETE FROM {0} WHERE media_id={1} AND media_type='{2}';", table, idMedia, mediaType)
                sqlCommand.ExecuteNonQuery()
            End Using
        End If
    End Sub

    Private Sub RemoveFromTable(ByVal table As String, ByVal firstField As String, firstID As Long)
        If Not String.IsNullOrEmpty(table) AndAlso Not String.IsNullOrEmpty(firstField) AndAlso Not firstID = -1 Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("DELETE FROM {0} WHERE {1}={2};", table, firstField, firstID)
                sqlCommand.ExecuteNonQuery()
            End Using
        End If
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
    ''' Saves all information from a Database.DBElement object to the database
    ''' </summary>
    ''' <param name="_movieDB">Media.Movie object to save to the database</param>
    ''' <param name="bBatchMode">Is the function already part of a transaction?</param>
    ''' <param name="bToNFO">Save informations to NFO</param>
    ''' <param name="bToDisk">Save Images, Themes and Trailers to disk</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Save_Movie(ByVal _movieDB As DBElement, ByVal bBatchMode As Boolean, ByVal bToNFO As Boolean, ByVal bToDisk As Boolean, ByVal bDoSync As Boolean, ByVal bForceFileCleanup As Boolean) As DBElement
        If _movieDB.Movie Is Nothing Then Return _movieDB

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not bBatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLcommand_movie As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not _movieDB.IDSpecified Then
                SQLcommand_movie.CommandText = String.Concat("INSERT OR REPLACE INTO movie (",
                 "idSource, MoviePath, Type, ListTitle, HasSub, New, Mark, Imdb, Lock, ",
                 "Title, OriginalTitle, SortTitle, Year, Rating, Votes, MPAA, Top250, Outline, Plot, Tagline, Certification, ",
                 "Runtime, ReleaseDate, Playcount, Trailer, ",
                 "NfoPath, TrailerPath, SubPath, EThumbsPath, FanartURL, OutOfTolerance, VideoSource, ",
                 "DateAdded, EFanartsPath, ThemePath, ",
                 "TMDB, TMDBColID, DateModified, MarkCustom1, MarkCustom2, MarkCustom3, MarkCustom4, HasSet, iLastPlayed, Language, iUserRating",
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM movie;")
            Else
                SQLcommand_movie.CommandText = String.Concat("INSERT OR REPLACE INTO movie (",
                 "idMovie, idSource, MoviePath, Type, ListTitle, HasSub, New, Mark, Imdb, Lock, ",
                 "Title, OriginalTitle, SortTitle, Year, Rating, Votes, MPAA, Top250, Outline, Plot, Tagline, Certification, ",
                 "Runtime, ReleaseDate, Playcount, Trailer, ",
                 "NfoPath, TrailerPath, SubPath, EThumbsPath, FanartURL, OutOfTolerance, VideoSource, ",
                 "DateAdded, EFanartsPath, ThemePath, ",
                 "TMDB, TMDBColID, DateModified, MarkCustom1, MarkCustom2, MarkCustom3, MarkCustom4, HasSet, iLastPlayed, Language, iUserRating",
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM movie;")
                Dim parMovieID As SQLiteParameter = SQLcommand_movie.Parameters.Add("paridMovie", DbType.Int64, 0, "idMovie")
                parMovieID.Value = _movieDB.ID
            End If
            Dim par_movie_idSource As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_idSource", DbType.Int64, 0, "idSource")
            Dim par_movie_MoviePath As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_MoviePath", DbType.String, 0, "MoviePath")
            Dim par_movie_Type As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Type", DbType.Boolean, 0, "Type")
            Dim par_movie_ListTitle As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_ListTitle", DbType.String, 0, "ListTitle")
            Dim par_movie_HasSub As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_HasSub", DbType.Boolean, 0, "HasSub")
            Dim par_movie_New As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_New", DbType.Boolean, 0, "New")
            Dim par_movie_Mark As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Mark", DbType.Boolean, 0, "Mark")
            Dim par_movie_Imdb As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Imdb", DbType.String, 0, "Imdb")
            Dim par_movie_Lock As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Lock", DbType.Boolean, 0, "Lock")
            Dim par_movie_Title As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Title", DbType.String, 0, "Title")
            Dim par_movie_OriginalTitle As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_OriginalTitle", DbType.String, 0, "OriginalTitle")
            Dim par_movie_SortTitle As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_SortTitle", DbType.String, 0, "SortTitle")
            Dim par_movie_Year As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Year", DbType.String, 0, "Year")
            Dim par_movie_Rating As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Rating", DbType.String, 0, "Rating")
            Dim par_movie_Votes As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Votes", DbType.String, 0, "Votes")
            Dim par_movie_MPAA As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_MPAA", DbType.String, 0, "MPAA")
            Dim par_movie_Top250 As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Top250", DbType.Int64, 0, "Top250")
            Dim par_movie_Outline As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Outline", DbType.String, 0, "Outline")
            Dim par_movie_Plot As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Plot", DbType.String, 0, "Plot")
            Dim par_movie_Tagline As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Tagline", DbType.String, 0, "Tagline")
            Dim par_movie_Certification As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Certification", DbType.String, 0, "Certification")
            Dim par_movie_Runtime As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Runtime", DbType.String, 0, "Runtime")
            Dim par_movie_ReleaseDate As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_ReleaseDate", DbType.String, 0, "ReleaseDate")
            Dim par_movie_Playcount As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Playcount", DbType.Int64, 0, "Playcount")
            Dim par_movie_Trailer As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Trailer", DbType.String, 0, "Trailer")
            Dim par_movie_NfoPath As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_NfoPath", DbType.String, 0, "NfoPath")
            Dim par_movie_TrailerPath As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_TrailerPath", DbType.String, 0, "TrailerPath")
            Dim par_movie_SubPath As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_SubPath", DbType.String, 0, "SubPath")
            Dim par_movie_ExtrathumbsPath As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_EThumbsPath", DbType.String, 0, "EThumbsPath")
            Dim par_movie_FanartURL As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_FanartURL", DbType.String, 0, "FanartURL")
            Dim par_movie_OutOfTolerance As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_OutOfTolerance", DbType.Boolean, 0, "OutOfTolerance")
            Dim par_movie_VideoSource As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_VideoSource", DbType.String, 0, "VideoSource")
            Dim par_movie_DateAdded As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_DateAdded", DbType.Int64, 0, "DateAdded")
            Dim par_movie_ExtrafanartsPath As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_EFanartsPath", DbType.String, 0, "EFanartsPath")
            Dim par_movie_ThemePath As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_ThemePath", DbType.String, 0, "ThemePath")
            Dim par_movie_TMDB As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_TMDB", DbType.String, 0, "TMDB")
            Dim par_movie_TMDBColID As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_TMDBColID", DbType.String, 0, "TMDBColID")
            Dim par_movie_DateModified As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_DateModified", DbType.Int64, 0, "DateModified")
            Dim par_movie_MarkCustom1 As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_MarkCustom1", DbType.Boolean, 0, "MarkCustom1")
            Dim par_movie_MarkCustom2 As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_MarkCustom2", DbType.Boolean, 0, "MarkCustom2")
            Dim par_movie_MarkCustom3 As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_MarkCustom3", DbType.Boolean, 0, "MarkCustom3")
            Dim par_movie_MarkCustom4 As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_MarkCustom4", DbType.Boolean, 0, "MarkCustom4")
            Dim par_movie_HasSet As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_HasSet", DbType.Boolean, 0, "HasSet")
            Dim par_movie_iLastPlayed As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_iLastPlayed", DbType.Int64, 0, "iLastPlayed")
            Dim par_movie_Language As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_Language", DbType.String, 0, "Language")
            Dim par_movie_iUserRating As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_movie_iUserRating", DbType.Int64, 0, "iUserRating")

            Try
                If Not Master.eSettings.GeneralDateAddedIgnoreNFO AndAlso _movieDB.Movie.DateAddedSpecified Then
                    Dim DateTimeAdded As Date = Date.ParseExact(_movieDB.Movie.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    par_movie_DateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    Select Case Master.eSettings.GeneralDateTime
                        Case Enums.DateTime.Now
                            Dim DateTimeAdded As Date
                            If Date.TryParseExact(_movieDB.Movie.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, DateTimeAdded) Then
                                par_movie_DateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                            Else
                                par_movie_DateAdded.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                            End If
                        Case Enums.DateTime.ctime
                            Dim ctime As Date = File.GetCreationTime(_movieDB.Filename)
                            If ctime.Year > 1601 Then
                                par_movie_DateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            Else
                                Dim mtime As Date = File.GetLastWriteTime(_movieDB.Filename)
                                par_movie_DateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            End If
                        Case Enums.DateTime.mtime
                            Dim mtime As Date = File.GetLastWriteTime(_movieDB.Filename)
                            If mtime.Year > 1601 Then
                                par_movie_DateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                Dim ctime As Date = File.GetCreationTime(_movieDB.Filename)
                                par_movie_DateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                        Case Enums.DateTime.Newer
                            Dim mtime As Date = File.GetLastWriteTime(_movieDB.Filename)
                            Dim ctime As Date = File.GetCreationTime(_movieDB.Filename)
                            If mtime > ctime Then
                                par_movie_DateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                par_movie_DateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                    End Select
                End If
                _movieDB.Movie.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_movie_DateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            Catch
                Dim DateTimeAdded As Date
                If Date.TryParseExact(_movieDB.Movie.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, DateTimeAdded) Then
                    par_movie_DateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    par_movie_DateAdded.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                End If
                _movieDB.Movie.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_movie_DateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            End Try

            Try
                If Not _movieDB.IDSpecified AndAlso _movieDB.Movie.DateModifiedSpecified Then
                    Dim DateTimeDateModified As Date = Date.ParseExact(_movieDB.Movie.DateModified, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    par_movie_DateModified.Value = Functions.ConvertToUnixTimestamp(DateTimeDateModified)
                ElseIf _movieDB.IDSpecified Then
                    par_movie_DateModified.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                End If
                If par_movie_DateModified.Value IsNot Nothing Then
                    _movieDB.Movie.DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_movie_DateModified.Value)).ToString("yyyy-MM-dd HH:mm:ss")
                Else
                    _movieDB.Movie.DateModified = String.Empty
                End If
            Catch
                par_movie_DateModified.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                _movieDB.Movie.DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_movie_DateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            End Try

            Dim DateTimeLastPlayedUnix As Double = -1
            If _movieDB.Movie.LastPlayedSpecified Then
                Try
                    Dim DateTimeLastPlayed As Date = Date.ParseExact(_movieDB.Movie.LastPlayed, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    DateTimeLastPlayedUnix = Functions.ConvertToUnixTimestamp(DateTimeLastPlayed)
                Catch
                    'Kodi save it only as yyyy-MM-dd, try that
                    Try
                        Dim DateTimeLastPlayed As Date = Date.ParseExact(_movieDB.Movie.LastPlayed, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture)
                        DateTimeLastPlayedUnix = Functions.ConvertToUnixTimestamp(DateTimeLastPlayed)
                    Catch
                        DateTimeLastPlayedUnix = -1
                    End Try
                End Try
            End If
            If DateTimeLastPlayedUnix >= 0 Then
                par_movie_iLastPlayed.Value = DateTimeLastPlayedUnix
            Else
                par_movie_iLastPlayed.Value = Nothing 'need to be NOTHING instead of 0
                _movieDB.Movie.LastPlayed = String.Empty
            End If

            'Trailer URL
            If Master.eSettings.MovieScraperXBMCTrailerFormat Then
                _movieDB.Movie.Trailer = _movieDB.Movie.Trailer.Trim.Replace("http://www.youtube.com/watch?v=", "plugin://plugin.video.youtube/?action=play_video&videoid=")
                _movieDB.Movie.Trailer = _movieDB.Movie.Trailer.Replace("http://www.youtube.com/watch?hd=1&v=", "plugin://plugin.video.youtube/?action=play_video&videoid=")
            End If

            'First let's save it to NFO, even because we will need the NFO path
            'Also save Images to get ExtrafanartsPath and ExtrathumbsPath
            'art Table will be linked later
            If bToNFO Then NFO.SaveToNFO_Movie(_movieDB, bForceFileCleanup)
            If bToDisk Then
                _movieDB.ImagesContainer.SaveAllImages(_movieDB, bForceFileCleanup)
                _movieDB.Movie.SaveAllActorThumbs(_movieDB)
                _movieDB.Theme.Save(_movieDB, Enums.ModifierType.MainTheme, bForceFileCleanup)
                _movieDB.Trailer.Save(_movieDB, Enums.ModifierType.MainTrailer, bForceFileCleanup)
            End If

            par_movie_MoviePath.Value = _movieDB.Filename
            par_movie_Type.Value = _movieDB.IsSingle
            par_movie_ListTitle.Value = _movieDB.ListTitle

            par_movie_ExtrafanartsPath.Value = _movieDB.ExtrafanartsPath
            par_movie_ExtrathumbsPath.Value = _movieDB.ExtrathumbsPath
            par_movie_NfoPath.Value = _movieDB.NfoPath
            par_movie_ThemePath.Value = If(Not String.IsNullOrEmpty(_movieDB.Theme.LocalFilePath), _movieDB.Theme.LocalFilePath, String.Empty)
            par_movie_TrailerPath.Value = If(Not String.IsNullOrEmpty(_movieDB.Trailer.LocalFilePath), _movieDB.Trailer.LocalFilePath, String.Empty)

            If Not Master.eSettings.MovieImagesNotSaveURLToNfo Then
                par_movie_FanartURL.Value = _movieDB.Movie.Fanart.URL
            Else
                par_movie_FanartURL.Value = String.Empty
            End If

            par_movie_HasSet.Value = _movieDB.Movie.SetsSpecified
            If _movieDB.Subtitles Is Nothing = False Then
                par_movie_HasSub.Value = _movieDB.Subtitles.Count > 0 OrElse _movieDB.Movie.FileInfo.StreamDetails.Subtitle.Count > 0
            Else
                par_movie_HasSub.Value = Nothing
            End If

            par_movie_Lock.Value = _movieDB.IsLock
            par_movie_Mark.Value = _movieDB.IsMark
            par_movie_MarkCustom1.Value = _movieDB.IsMarkCustom1
            par_movie_MarkCustom2.Value = _movieDB.IsMarkCustom2
            par_movie_MarkCustom3.Value = _movieDB.IsMarkCustom3
            par_movie_MarkCustom4.Value = _movieDB.IsMarkCustom4
            par_movie_New.Value = Not _movieDB.IDSpecified

            With _movieDB.Movie
                par_movie_Certification.Value = String.Join(" / ", .Certifications.ToArray)
                par_movie_Imdb.Value = .UniqueIDs.IMDbId
                par_movie_iUserRating.Value = .UserRating
                par_movie_MPAA.Value = .MPAA
                par_movie_OriginalTitle.Value = .OriginalTitle
                par_movie_Outline.Value = .Outline
                If .PlayCountSpecified Then 'need to be NOTHING instead of "0"
                    par_movie_Playcount.Value = .PlayCount
                End If
                par_movie_Plot.Value = .Plot
                par_movie_Rating.Value = .Rating
                par_movie_ReleaseDate.Value = NumUtils.DateToISO8601Date(.Premiered)
                par_movie_Runtime.Value = .Runtime
                par_movie_SortTitle.Value = .SortTitle
                par_movie_TMDB.Value = .UniqueIDs.TMDbId
                par_movie_TMDBColID.Value = .UniqueIDs.TMDbCollectionId
                par_movie_Tagline.Value = .Tagline
                par_movie_Title.Value = .Title
                If .Top250Specified Then 'need to be NOTHING instead of "0"
                    par_movie_Top250.Value = .Top250
                End If
                par_movie_Trailer.Value = .Trailer
                par_movie_Votes.Value = .Votes
                par_movie_Year.Value = .Year
            End With

            par_movie_OutOfTolerance.Value = _movieDB.OutOfTolerance
            par_movie_VideoSource.Value = _movieDB.VideoSource
            par_movie_Language.Value = _movieDB.Language

            par_movie_idSource.Value = _movieDB.Source.ID

            If Not _movieDB.IDSpecified Then
                If Master.eSettings.MovieGeneralMarkNew Then
                    par_movie_Mark.Value = True
                    _movieDB.IsMark = True
                End If
                Using rdrMovie As SQLiteDataReader = SQLcommand_movie.ExecuteReader()
                    If rdrMovie.Read Then
                        _movieDB.ID = Convert.ToInt64(rdrMovie(0))
                    Else
                        logger.Error("Something very wrong here: SaveMovieToDB", _movieDB.ToString)
                        _movieDB.ID = -1
                        Return _movieDB
                    End If
                End Using
            Else
                SQLcommand_movie.ExecuteNonQuery()
            End If

            If _movieDB.IDSpecified Then

                'Actors
                Using SQLcommand_actorlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_actorlink.CommandText = String.Format("DELETE FROM actorlinkmovie WHERE idMovie = {0};", _movieDB.ID)
                    SQLcommand_actorlink.ExecuteNonQuery()
                End Using
                AddCast(_movieDB.ID, "movie", "movie", _movieDB.Movie.Actors)

                'Countries
                Using SQLcommand_countrylink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_countrylink.CommandText = String.Format("DELETE FROM countrylinkmovie WHERE idMovie = {0};", _movieDB.ID)
                    SQLcommand_countrylink.ExecuteNonQuery()
                End Using
                For Each country As String In _movieDB.Movie.Countries
                    AddCountryToMovie(_movieDB.ID, AddCountry(country))
                Next

                'Directors
                Using SQLcommand_directorlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_directorlink.CommandText = String.Format("DELETE FROM directorlinkmovie WHERE idMovie = {0};", _movieDB.ID)
                    SQLcommand_directorlink.ExecuteNonQuery()
                End Using
                For Each director As String In _movieDB.Movie.Directors
                    AddDirectorToMovie(_movieDB.ID, AddActor(director, "", "", "", "", False))
                Next

                'Genres
                Using SQLcommand_genrelink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_genrelink.CommandText = String.Format("DELETE FROM genrelinkmovie WHERE idMovie = {0};", _movieDB.ID)
                    SQLcommand_genrelink.ExecuteNonQuery()
                End Using
                For Each genre As String In _movieDB.Movie.Genres
                    AddGenreToMovie(_movieDB.ID, AddGenre(genre))
                Next

                'Images
                Using SQLcommand_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_art.CommandText = String.Format("DELETE FROM art WHERE media_id = {0} AND media_type = 'movie';", _movieDB.ID)
                    SQLcommand_art.ExecuteNonQuery()
                End Using
                If Not String.IsNullOrEmpty(_movieDB.ImagesContainer.Banner.LocalFilePath) Then SetArtForItem(_movieDB.ID, "movie", "banner", _movieDB.ImagesContainer.Banner.LocalFilePath)
                If Not String.IsNullOrEmpty(_movieDB.ImagesContainer.ClearArt.LocalFilePath) Then SetArtForItem(_movieDB.ID, "movie", "clearart", _movieDB.ImagesContainer.ClearArt.LocalFilePath)
                If Not String.IsNullOrEmpty(_movieDB.ImagesContainer.ClearLogo.LocalFilePath) Then SetArtForItem(_movieDB.ID, "movie", "clearlogo", _movieDB.ImagesContainer.ClearLogo.LocalFilePath)
                If Not String.IsNullOrEmpty(_movieDB.ImagesContainer.DiscArt.LocalFilePath) Then SetArtForItem(_movieDB.ID, "movie", "discart", _movieDB.ImagesContainer.DiscArt.LocalFilePath)
                If Not String.IsNullOrEmpty(_movieDB.ImagesContainer.Fanart.LocalFilePath) Then SetArtForItem(_movieDB.ID, "movie", "fanart", _movieDB.ImagesContainer.Fanart.LocalFilePath)
                If Not String.IsNullOrEmpty(_movieDB.ImagesContainer.Keyart.LocalFilePath) Then SetArtForItem(_movieDB.ID, "movie", "keyart", _movieDB.ImagesContainer.Keyart.LocalFilePath)
                If Not String.IsNullOrEmpty(_movieDB.ImagesContainer.Landscape.LocalFilePath) Then SetArtForItem(_movieDB.ID, "movie", "landscape", _movieDB.ImagesContainer.Landscape.LocalFilePath)
                If Not String.IsNullOrEmpty(_movieDB.ImagesContainer.Poster.LocalFilePath) Then SetArtForItem(_movieDB.ID, "movie", "poster", _movieDB.ImagesContainer.Poster.LocalFilePath)

                'Movieset
                SetMoviesetsForMovie(_movieDB, _movieDB.Movie.Sets)

                'Ratings
                SetRatingsForItem(_movieDB.ID, _movieDB.ContentType, _movieDB.Movie.Ratings)

                'Studios
                Using SQLcommand_studiolink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_studiolink.CommandText = String.Format("DELETE FROM studiolinkmovie WHERE idMovie = {0};", _movieDB.ID)
                    SQLcommand_studiolink.ExecuteNonQuery()
                End Using
                For Each studio As String In _movieDB.Movie.Studios
                    AddStudioToMovie(_movieDB.ID, AddStudio(studio))
                Next

                'Tags
                Using SQLcommand_taglinks As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_taglinks.CommandText = String.Format("DELETE FROM taglinks WHERE idMedia = {0} AND media_type = 'movie';", _movieDB.ID)
                    SQLcommand_taglinks.ExecuteNonQuery()
                End Using
                For Each tag As String In _movieDB.Movie.Tags
                    AddTagToItem(_movieDB.ID, AddTag(tag), "movie")
                Next

                'UniqueIDs
                SetUniqueIDsForItem(_movieDB.ID, _movieDB.ContentType, _movieDB.Movie.UniqueIDs)

                'Writers
                Using SQLcommand_writerlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_writerlink.CommandText = String.Format("DELETE FROM writerlinkmovie WHERE idMovie = {0};", _movieDB.ID)
                    SQLcommand_writerlink.ExecuteNonQuery()
                End Using
                For Each writer As String In _movieDB.Movie.Credits
                    AddWriterToMovie(_movieDB.ID, AddActor(writer, "", "", "", "", False))
                Next

                'Video Streams
                Using SQLcommandMoviesVStreams As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandMoviesVStreams.CommandText = String.Format("DELETE FROM MoviesVStreams WHERE MovieID = {0};", _movieDB.ID)
                    SQLcommandMoviesVStreams.ExecuteNonQuery()

                    'Expanded SQL Statement to INSERT/replace new fields
                    SQLcommandMoviesVStreams.CommandText = String.Concat("INSERT OR REPLACE INTO MoviesVStreams (",
                       "MovieID, StreamID, Video_Width,Video_Height,Video_Codec,Video_Duration, Video_ScanType, Video_AspectDisplayRatio, ",
                       "Video_Language, Video_LongLanguage, Video_Bitrate, Video_MultiViewCount, Video_FileSize, Video_MultiViewLayout, ",
                       "Video_StereoMode) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);")

                    Dim parVideo_MovieID As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_MovieID", DbType.Int64, 0, "MovieID")
                    Dim parVideo_StreamID As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_StreamID", DbType.Int64, 0, "StreamID")
                    Dim parVideo_Width As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_Width", DbType.String, 0, "Video_Width")
                    Dim parVideo_Height As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_Height", DbType.String, 0, "Video_Height")
                    Dim parVideo_Codec As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_Codec", DbType.String, 0, "Video_Codec")
                    Dim parVideo_Duration As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_Duration", DbType.String, 0, "Video_Duration")
                    Dim parVideo_ScanType As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_ScanType", DbType.String, 0, "Video_ScanType")
                    Dim parVideo_AspectDisplayRatio As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_AspectDisplayRatio", DbType.String, 0, "Video_AspectDisplayRatio")
                    Dim parVideo_Language As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_Language", DbType.String, 0, "Video_Language")
                    Dim parVideo_LongLanguage As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_LongLanguage", DbType.String, 0, "Video_LongLanguage")
                    Dim parVideo_Bitrate As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_Bitrate", DbType.String, 0, "Video_Bitrate")
                    Dim parVideo_MultiViewCount As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_MultiViewCount", DbType.String, 0, "Video_MultiViewCount")
                    Dim parVideo_FileSize As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_FileSize", DbType.Int64, 0, "Video_FileSize")
                    Dim parVideo_MultiViewLayout As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_MultiViewLayout", DbType.String, 0, "Video_MultiViewLayout")
                    Dim parVideo_StereoMode As SQLiteParameter = SQLcommandMoviesVStreams.Parameters.Add("parVideo_StereoMode", DbType.String, 0, "Video_StereoMode")

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
                        parVideo_FileSize.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).Filesize
                        parVideo_StereoMode.Value = _movieDB.Movie.FileInfo.StreamDetails.Video(i).StereoMode

                        SQLcommandMoviesVStreams.ExecuteNonQuery()
                    Next
                End Using
                Using SQLcommandMoviesAStreams As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandMoviesAStreams.CommandText = String.Concat("DELETE FROM MoviesAStreams WHERE MovieID = ", _movieDB.ID, ";")
                    SQLcommandMoviesAStreams.ExecuteNonQuery()

                    'Expanded SQL Statement to INSERT/replace new fields
                    SQLcommandMoviesAStreams.CommandText = String.Concat("INSERT OR REPLACE INTO MoviesAStreams (",
                      "MovieID, StreamID, Audio_Language, Audio_LongLanguage, Audio_Codec, Audio_Channel, Audio_Bitrate",
                      ") VALUES (?,?,?,?,?,?,?);")

                    Dim parAudio_MovieID As SQLiteParameter = SQLcommandMoviesAStreams.Parameters.Add("parAudio_MovieID", DbType.Int64, 0, "MovieID")
                    Dim parAudio_StreamID As SQLiteParameter = SQLcommandMoviesAStreams.Parameters.Add("parAudio_StreamID", DbType.Int64, 0, "StreamID")
                    Dim parAudio_Language As SQLiteParameter = SQLcommandMoviesAStreams.Parameters.Add("parAudio_Language", DbType.String, 0, "Audio_Language")
                    Dim parAudio_LongLanguage As SQLiteParameter = SQLcommandMoviesAStreams.Parameters.Add("parAudio_LongLanguage", DbType.String, 0, "Audio_LongLanguage")
                    Dim parAudio_Codec As SQLiteParameter = SQLcommandMoviesAStreams.Parameters.Add("parAudio_Codec", DbType.String, 0, "Audio_Codec")
                    Dim parAudio_Channel As SQLiteParameter = SQLcommandMoviesAStreams.Parameters.Add("parAudio_Channel", DbType.String, 0, "Audio_Channel")
                    Dim parAudio_Bitrate As SQLiteParameter = SQLcommandMoviesAStreams.Parameters.Add("parAudio_Bitrate", DbType.String, 0, "Audio_Bitrate")

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
                Using SQLcommandMoviesSubs As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandMoviesSubs.CommandText = String.Concat("DELETE FROM MoviesSubs WHERE MovieID = ", _movieDB.ID, ";")
                    SQLcommandMoviesSubs.ExecuteNonQuery()

                    SQLcommandMoviesSubs.CommandText = String.Concat("INSERT OR REPLACE INTO MoviesSubs (",
                       "MovieID, StreamID, Subs_Language, Subs_LongLanguage,Subs_Type, Subs_Path, Subs_Forced",
                       ") VALUES (?,?,?,?,?,?,?);")
                    Dim parSubs_MovieID As SQLiteParameter = SQLcommandMoviesSubs.Parameters.Add("parSubs_MovieID", DbType.Int64, 0, "MovieID")
                    Dim parSubs_StreamID As SQLiteParameter = SQLcommandMoviesSubs.Parameters.Add("parSubs_StreamID", DbType.Int64, 0, "StreamID")
                    Dim parSubs_Language As SQLiteParameter = SQLcommandMoviesSubs.Parameters.Add("parSubs_Language", DbType.String, 0, "Subs_Language")
                    Dim parSubs_LongLanguage As SQLiteParameter = SQLcommandMoviesSubs.Parameters.Add("parSubs_LongLanguage", DbType.String, 0, "Subs_LongLanguage")
                    Dim parSubs_Type As SQLiteParameter = SQLcommandMoviesSubs.Parameters.Add("parSubs_Type", DbType.String, 0, "Subs_Type")
                    Dim parSubs_Path As SQLiteParameter = SQLcommandMoviesSubs.Parameters.Add("parSubs_Path", DbType.String, 0, "Subs_Path")
                    Dim parSubs_Forced As SQLiteParameter = SQLcommandMoviesSubs.Parameters.Add("parSubs_Forced", DbType.Boolean, 0, "Subs_Forced")
                    Dim iID As Integer = 0
                    'embedded subtitles
                    For i As Integer = 0 To _movieDB.Movie.FileInfo.StreamDetails.Subtitle.Count - 1
                        parSubs_MovieID.Value = _movieDB.ID
                        parSubs_StreamID.Value = iID
                        parSubs_Language.Value = _movieDB.Movie.FileInfo.StreamDetails.Subtitle(i).Language
                        parSubs_LongLanguage.Value = _movieDB.Movie.FileInfo.StreamDetails.Subtitle(i).LongLanguage
                        parSubs_Type.Value = _movieDB.Movie.FileInfo.StreamDetails.Subtitle(i).Type
                        parSubs_Path.Value = _movieDB.Movie.FileInfo.StreamDetails.Subtitle(i).Path
                        parSubs_Forced.Value = _movieDB.Movie.FileInfo.StreamDetails.Subtitle(i).Forced
                        SQLcommandMoviesSubs.ExecuteNonQuery()
                        iID += 1
                    Next
                    'external subtitles
                    For i As Integer = 0 To _movieDB.Subtitles.Count - 1
                        parSubs_MovieID.Value = _movieDB.ID
                        parSubs_StreamID.Value = iID
                        parSubs_Language.Value = _movieDB.Subtitles(i).Language
                        parSubs_LongLanguage.Value = _movieDB.Subtitles(i).LongLanguage
                        parSubs_Type.Value = _movieDB.Subtitles(i).Type
                        parSubs_Path.Value = _movieDB.Subtitles(i).Path
                        parSubs_Forced.Value = _movieDB.Subtitles(i).Forced
                        SQLcommandMoviesSubs.ExecuteNonQuery()
                        iID += 1
                    Next
                End Using
            End If
        End Using

        'YAMJ watched file
        If _movieDB.Movie.PlayCountSpecified AndAlso Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
            For Each a In FileUtils.GetFilenameList.Movie(_movieDB, Enums.ModifierType.MainWatchedFile)
                If Not File.Exists(a) Then
                    Dim fs As FileStream = File.Create(a)
                    fs.Close()
                End If
            Next
        ElseIf Not _movieDB.Movie.PlayCountSpecified AndAlso Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
            For Each a In FileUtils.GetFilenameList.Movie(_movieDB, Enums.ModifierType.MainWatchedFile)
                If File.Exists(a) Then
                    File.Delete(a)
                End If
            Next
        End If

        If Not bBatchMode Then SQLtransaction.Commit()

        If bDoSync Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Sync_Movie, Nothing, Nothing, False, _movieDB)
        End If

        Return _movieDB
    End Function
    ''' <summary>
    ''' Saves all information from a Database.DBElement object to the database
    ''' </summary>
    ''' <param name="_moviesetDB">Media.Movie object to save to the database</param>
    ''' <param name="bBatchMode">Is the function already part of a transaction?</param>
    ''' <param name="bToDisk">Create NFO and Images</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Save_MovieSet(ByVal _moviesetDB As DBElement, ByVal bBatchMode As Boolean, ByVal bToNFO As Boolean, ByVal bToDisk As Boolean, ByVal bDoSync As Boolean) As DBElement
        If _moviesetDB.MovieSet Is Nothing Then Return _moviesetDB

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not bBatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not _moviesetDB.IDSpecified Then
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO sets (",
                 "ListTitle, NfoPath, TMDBColID, Plot, SetName, New, Mark, Lock, SortMethod, Language",
                 ") VALUES (?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM sets;")
            Else
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO sets (",
                 "idSet, ListTitle, NfoPath, TMDBColID, Plot, SetName, New, Mark, Lock, SortMethod, Language",
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM sets;")
                Dim parMovieSetID As SQLiteParameter = SQLcommand.Parameters.Add("parMovieSetID", DbType.Int64, 0, "idSet")
                parMovieSetID.Value = _moviesetDB.ID
            End If
            Dim parListTitle As SQLiteParameter = SQLcommand.Parameters.Add("parListTitle", DbType.String, 0, "ListTitle")
            Dim parNfoPath As SQLiteParameter = SQLcommand.Parameters.Add("parNfoPath", DbType.String, 0, "NfoPath")
            Dim parTMDBColID As SQLiteParameter = SQLcommand.Parameters.Add("parTMDBColID", DbType.String, 0, "TMDBColID")
            Dim parPlot As SQLiteParameter = SQLcommand.Parameters.Add("parPlot", DbType.String, 0, "Plot")
            Dim parSetName As SQLiteParameter = SQLcommand.Parameters.Add("parSetName", DbType.String, 0, "SetName")
            Dim parNew As SQLiteParameter = SQLcommand.Parameters.Add("parNew", DbType.Boolean, 0, "New")
            Dim parMark As SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "Mark")
            Dim parLock As SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "Lock")
            Dim parSortMethod As SQLiteParameter = SQLcommand.Parameters.Add("parSortMethod", DbType.Int16, 0, "SortMethod")
            Dim parLanguage As SQLiteParameter = SQLcommand.Parameters.Add("parLanguage", DbType.String, 0, "Language")

            'First let's save it to NFO, even because we will need the NFO path, also save Images
            'art Table be be linked later
            If bToNFO Then NFO.SaveToNFO_MovieSet(_moviesetDB)
            If bToDisk Then
                _moviesetDB.ImagesContainer.SaveAllImages(_moviesetDB, False)
            End If

            parNfoPath.Value = _moviesetDB.NfoPath
            parLanguage.Value = _moviesetDB.Language

            parNew.Value = Not _moviesetDB.IDSpecified
            parMark.Value = _moviesetDB.IsMark
            parLock.Value = _moviesetDB.IsLock
            parSortMethod.Value = _moviesetDB.SortMethod

            parListTitle.Value = _moviesetDB.ListTitle
            parSetName.Value = _moviesetDB.MovieSet.Title
            parTMDBColID.Value = _moviesetDB.MovieSet.UniqueIDs.TMDbId
            parPlot.Value = _moviesetDB.MovieSet.Plot

            If Not _moviesetDB.IDSpecified Then
                If Master.eSettings.MovieSetGeneralMarkNew Then
                    parMark.Value = True
                    _moviesetDB.IsMark = True
                End If
                Using rdrMovieSet As SQLiteDataReader = SQLcommand.ExecuteReader()
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

        'Images
        Using SQLcommand_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_art.CommandText = String.Concat("DELETE FROM art WHERE media_id = ", _moviesetDB.ID, " AND media_type = 'set';")
            SQLcommand_art.ExecuteNonQuery()
        End Using
        If Not String.IsNullOrEmpty(_moviesetDB.ImagesContainer.Banner.LocalFilePath) Then SetArtForItem(_moviesetDB.ID, "set", "banner", _moviesetDB.ImagesContainer.Banner.LocalFilePath)
        If Not String.IsNullOrEmpty(_moviesetDB.ImagesContainer.ClearArt.LocalFilePath) Then SetArtForItem(_moviesetDB.ID, "set", "clearart", _moviesetDB.ImagesContainer.ClearArt.LocalFilePath)
        If Not String.IsNullOrEmpty(_moviesetDB.ImagesContainer.ClearLogo.LocalFilePath) Then SetArtForItem(_moviesetDB.ID, "set", "clearlogo", _moviesetDB.ImagesContainer.ClearLogo.LocalFilePath)
        If Not String.IsNullOrEmpty(_moviesetDB.ImagesContainer.DiscArt.LocalFilePath) Then SetArtForItem(_moviesetDB.ID, "set", "discart", _moviesetDB.ImagesContainer.DiscArt.LocalFilePath)
        If Not String.IsNullOrEmpty(_moviesetDB.ImagesContainer.Fanart.LocalFilePath) Then SetArtForItem(_moviesetDB.ID, "set", "fanart", _moviesetDB.ImagesContainer.Fanart.LocalFilePath)
        If Not String.IsNullOrEmpty(_moviesetDB.ImagesContainer.Keyart.LocalFilePath) Then SetArtForItem(_moviesetDB.ID, "set", "keyart", _moviesetDB.ImagesContainer.Keyart.LocalFilePath)
        If Not String.IsNullOrEmpty(_moviesetDB.ImagesContainer.Landscape.LocalFilePath) Then SetArtForItem(_moviesetDB.ID, "set", "landscape", _moviesetDB.ImagesContainer.Landscape.LocalFilePath)
        If Not String.IsNullOrEmpty(_moviesetDB.ImagesContainer.Poster.LocalFilePath) Then SetArtForItem(_moviesetDB.ID, "set", "poster", _moviesetDB.ImagesContainer.Poster.LocalFilePath)

        'UniqueIDs
        SetUniqueIDsForItem(_moviesetDB.ID, _moviesetDB.ContentType, _moviesetDB.MovieSet.UniqueIDs)

        'save set informations to movies
        For Each tMovie In _moviesetDB.MoviesInSet
            tMovie.DBMovie.Movie.AddSet(New MediaContainers.SetDetails With {
                                        .ID = _moviesetDB.ID,
                                        .Order = tMovie.Order,
                                        .Plot = _moviesetDB.MovieSet.Plot,
                                        .Title = _moviesetDB.MovieSet.Title,
                                        .UniqueIDs = _moviesetDB.MovieSet.UniqueIDs
                                        })
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, Nothing, False, tMovie.DBMovie)
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_Movie, Nothing, Nothing, False, tMovie.DBMovie)
            Save_Movie(tMovie.DBMovie, True, True, False, True, False)
            RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {tMovie.DBMovie.ID}))
        Next

        'remove set-information from movies which are no longer assigned to this set
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT idMovie, idSet FROM setlinkmovie ",
                                                       "WHERE idSet = ", _moviesetDB.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Dim rMovie = _moviesetDB.MoviesInSet.FirstOrDefault(Function(f) f.DBMovie.ID = Convert.ToInt64(SQLreader("idMovie")))
                    If rMovie Is Nothing Then
                        'movie is no longer a part of this set
                        Dim tMovie As Database.DBElement = Load_Movie(Convert.ToInt64(SQLreader("idMovie")))
                        tMovie.Movie.RemoveSet(_moviesetDB.ID)
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, Nothing, False, tMovie)
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_Movie, Nothing, Nothing, False, tMovie)
                        Save_Movie(tMovie, True, True, False, True, False)
                        RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {tMovie.ID}))
                    End If
                End While
            End Using
        End Using

        If Not bBatchMode Then SQLtransaction.Commit()

        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Sync_MovieSet, Nothing, Nothing, False, _moviesetDB)

        Return _moviesetDB
    End Function

    ''' <summary>
    ''' Saves all information from a Database.DBElement object to the database
    ''' </summary>
    ''' <param name="_tagDB">Media.Movie object to save to the database</param>
    ''' <param name="bIsNew">Is this a new movieset (not already present in database)?</param>
    ''' <param name="bBatchMode">Is the function already part of a transaction?</param>
    ''' <param name="bToNfo">Save the information to an nfo file?</param>
    ''' <param name="bWithMovies">Save the information also to all linked movies?</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Save_Tag_Movie(ByVal _tagDB As Structures.DBMovieTag, ByVal bIsNew As Boolean, ByVal bBatchMode As Boolean, ByVal bToNfo As Boolean, ByVal bWithMovies As Boolean) As Structures.DBMovieTag
        If _tagDB.ID = -1 Then bIsNew = True

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not bBatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If bIsNew Then
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO tag (strTag) VALUES (?); SELECT LAST_INSERT_ROWID() FROM tag;")
            Else
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO tag (",
                          "idTag, strTag) VALUES (?,?); SELECT LAST_INSERT_ROWID() FROM tag;")
                Dim parTagID As SQLiteParameter = SQLcommand.Parameters.Add("parTagID", DbType.Int64, 0, "idTag")
                parTagID.Value = _tagDB.ID
            End If
            Dim parTitle As SQLiteParameter = SQLcommand.Parameters.Add("parTitle", DbType.String, 0, "strTag")

            parTitle.Value = _tagDB.Title

            If bIsNew Then
                Using rdrMovieTag As SQLiteDataReader = SQLcommand.ExecuteReader()
                    If rdrMovieTag.Read Then
                        _tagDB.ID = CInt(Convert.ToInt64(rdrMovieTag(0)))
                    Else
                        logger.Error("Something very wrong here: SaveMovieSetToDB", _tagDB.ToString, "Error")
                        _tagDB.Title = "SETERROR"
                        Return _tagDB
                    End If
                End Using
            Else
                SQLcommand.ExecuteNonQuery()
            End If
        End Using


        If bWithMovies Then
            'Update all movies for this tag: if there are movies in linktag-table which aren't in current tag.movies object then remove movie-tag link from linktable and nfo for those movies

            'old state of tag in database
            Dim MoviesInTagOld As New List(Of DBElement)
            'new/updatend state of tag
            Dim MoviesInTagNew As New List(Of DBElement)
            MoviesInTagNew.AddRange(_tagDB.Movies.ToArray)





            'get all movies linked to this tag from database (old state)
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT idMedia, idTag FROM taglinks ",
                   "WHERE idTag = ", _tagDB.ID, " AND media_type = 'movie';")

                Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        If Not DBNull.Value.Equals(SQLreader("idMedia")) Then
                            MoviesInTagOld.Add(Load_Movie(Convert.ToInt64(SQLreader("idMedia"))))
                        End If
                    End While
                End Using
            End Using

            'check if there are movies in linktable which aren't in current tag - those are old entries which meed to be removed from linktag table and nfo of movies
            For i = MoviesInTagOld.Count - 1 To 0 Step -1
                For Each movienew In MoviesInTagNew
                    If MoviesInTagOld(i).Movie.UniqueIDs.IMDbId = movienew.Movie.UniqueIDs.IMDbId Then
                        MoviesInTagOld.RemoveAt(i)
                        Exit For
                    End If
                Next
            Next

            'write tag information into nfo (add tag)
            If MoviesInTagNew.Count > 0 Then
                For Each tMovie In MoviesInTagNew
                    Dim mMovie As DBElement = Load_Movie(tMovie.ID) 'TODO: check why we load mMovie to overwrite tMovie with himself
                    tMovie = mMovie
                    mMovie.Movie.AddTag(_tagDB.Title)
                    Master.DB.Save_Movie(mMovie, bBatchMode, True, False, True, False)
                Next
            End If
            'clean nfo of movies who aren't part of tag anymore (remove tag)
            If MoviesInTagOld.Count > 0 Then
                For Each tMovie In MoviesInTagOld
                    Dim mMovie As DBElement = Load_Movie(tMovie.ID) 'TODO: check why we load mMovie to overwrite tMovie with himself
                    tMovie = mMovie
                    mMovie.Movie.Tags.Remove(_tagDB.Title)
                    Master.DB.Save_Movie(mMovie, bBatchMode, True, False, True, False)
                Next
            End If
        End If

        If Not bBatchMode Then SQLtransaction.Commit()

        Return _tagDB
    End Function
    ''' <summary>
    ''' Saves all episode information from a Database.DBElement object to the database
    ''' </summary>
    ''' <param name="_episode">Database.DBElement object to save to the database</param>
    ''' <param name="bDoSeasonCheck">If <c>True</c> then check if it's needed to create a new season for this episode</param>
    ''' <param name="bBatchMode">Is the function already part of a transaction?</param>
    ''' <param name="bToDisk">Create NFO and Images</param>
    Public Function Save_TVEpisode(ByVal _episode As DBElement, ByVal bBatchMode As Boolean, ByVal bToNFO As Boolean, ByVal bToDisk As Boolean, ByVal bDoSeasonCheck As Boolean, ByVal bDoSync As Boolean, Optional ByVal bForceIsNewFlag As Boolean = False) As DBElement
        If _episode.TVEpisode Is Nothing Then Return _episode

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not bBatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        'delete so it will remove if there is a "missing" episode entry already. Only "missing" episodes must be deleted.
        Using SQLCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLCommand.CommandText = String.Concat("DELETE FROM episode WHERE idShow = ", _episode.ShowID, " AND Episode = ", _episode.TVEpisode.Episode, " AND Season = ", _episode.TVEpisode.Season, " AND idFile = -1;")
            SQLCommand.ExecuteNonQuery()
        End Using

        If _episode.FilenameSpecified Then
            If _episode.FilenameIDSpecified Then
                Using SQLpathcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLpathcommand.CommandText = String.Concat("INSERT OR REPLACE INTO files (idFile, strFilename) VALUES (?,?);")

                    Dim parID As SQLiteParameter = SQLpathcommand.Parameters.Add("parFileID", DbType.Int64, 0, "idFile")
                    Dim parFilename As SQLiteParameter = SQLpathcommand.Parameters.Add("parFilename", DbType.String, 0, "strFilename")
                    parID.Value = _episode.FilenameID
                    parFilename.Value = _episode.Filename
                    SQLpathcommand.ExecuteNonQuery()
                End Using
            Else
                Using SQLpathcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLpathcommand.CommandText = "SELECT idFile FROM files WHERE strFilename = (?);"

                    Dim parPath As SQLiteParameter = SQLpathcommand.Parameters.Add("parFilename", DbType.String, 0, "strFilename")
                    parPath.Value = _episode.Filename

                    Using SQLreader As SQLiteDataReader = SQLpathcommand.ExecuteReader
                        If SQLreader.HasRows Then
                            SQLreader.Read()
                            _episode.FilenameID = Convert.ToInt64(SQLreader("idFile"))
                        Else
                            Using SQLpcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                SQLpcommand.CommandText = String.Concat("INSERT INTO files (",
                                     "strFilename) VALUES (?); SELECT LAST_INSERT_ROWID() FROM files;")
                                Dim parEpPath As SQLiteParameter = SQLpcommand.Parameters.Add("parEpPath", DbType.String, 0, "strFilename")
                                parEpPath.Value = _episode.Filename

                                _episode.FilenameID = Convert.ToInt64(SQLpcommand.ExecuteScalar)
                            End Using
                        End If
                    End Using
                End Using
            End If
        End If

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not _episode.IDSpecified Then
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO episode (",
                 "idShow, idFile, idSource, New, Mark, Lock, Title, OriginalTitle, Season, Episode, ",
                 "Rating, Plot, Aired, NfoPath, Playcount, ",
                 "DisplaySeason, DisplayEpisode, DateAdded, Runtime, Votes, VideoSource, HasSub, SubEpisode, ",
                 "iLastPlayed, strIMDB, strTMDB, strTVDB, iUserRating",
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM episode;")

            Else
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO episode (",
                 "idEpisode, idShow, idFile, idSource, New, Mark, Lock, Title, OriginalTitle, Season, Episode, ",
                 "Rating, Plot, Aired, NfoPath, Playcount, ",
                 "DisplaySeason, DisplayEpisode, DateAdded, Runtime, Votes, VideoSource, HasSub, SubEpisode, ",
                 "iLastPlayed, strIMDB, strTMDB, strTVDB, iUserRating",
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM episode;")

                Dim parTVEpisodeID As SQLiteParameter = SQLcommand.Parameters.Add("parTVEpisodeID", DbType.Int64, 0, "idEpisode")
                parTVEpisodeID.Value = _episode.ID
            End If

            Dim parTVShowID As SQLiteParameter = SQLcommand.Parameters.Add("parTVShowID", DbType.Int64, 0, "idShow")
            Dim parTVFileID As SQLiteParameter = SQLcommand.Parameters.Add("parTVFileID", DbType.Int64, 0, "idFile")
            Dim parSourceID As SQLiteParameter = SQLcommand.Parameters.Add("parSourceID", DbType.Int64, 0, "idSource")
            Dim parNew As SQLiteParameter = SQLcommand.Parameters.Add("parNew", DbType.Boolean, 0, "new")
            Dim parMark As SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
            Dim parLock As SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "lock")
            Dim parTitle As SQLiteParameter = SQLcommand.Parameters.Add("parTitle", DbType.String, 0, "Title")
            Dim parOriginalTitle As SQLiteParameter = SQLcommand.Parameters.Add("parOriginalTitle", DbType.String, 0, "OriginalTitle")
            Dim parSeason As SQLiteParameter = SQLcommand.Parameters.Add("parSeason", DbType.String, 0, "Season")
            Dim parEpisode As SQLiteParameter = SQLcommand.Parameters.Add("parEpisode", DbType.String, 0, "Episode")
            Dim parRating As SQLiteParameter = SQLcommand.Parameters.Add("parRating", DbType.String, 0, "Rating")
            Dim parPlot As SQLiteParameter = SQLcommand.Parameters.Add("parPlot", DbType.String, 0, "Plot")
            Dim parAired As SQLiteParameter = SQLcommand.Parameters.Add("parAired", DbType.String, 0, "Aired")
            Dim parNfoPath As SQLiteParameter = SQLcommand.Parameters.Add("parNfoPath", DbType.String, 0, "NfoPath")
            Dim parPlaycount As SQLiteParameter = SQLcommand.Parameters.Add("parPlaycount", DbType.Int64, 0, "Playcount")
            Dim parDisplaySeason As SQLiteParameter = SQLcommand.Parameters.Add("parDisplaySeason", DbType.String, 0, "DisplaySeason")
            Dim parDisplayEpisode As SQLiteParameter = SQLcommand.Parameters.Add("parDisplayEpisode", DbType.String, 0, "DisplayEpisode")
            Dim parDateAdded As SQLiteParameter = SQLcommand.Parameters.Add("parDateAdded", DbType.Int64, 0, "DateAdded")
            Dim parRuntime As SQLiteParameter = SQLcommand.Parameters.Add("parRuntime", DbType.String, 0, "Runtime")
            Dim parVotes As SQLiteParameter = SQLcommand.Parameters.Add("parVotes", DbType.String, 0, "Votes")
            Dim parVideoSource As SQLiteParameter = SQLcommand.Parameters.Add("parVideoSource", DbType.String, 0, "VideoSource")
            Dim parHasSub As SQLiteParameter = SQLcommand.Parameters.Add("parHasSub", DbType.Boolean, 0, "HasSub")
            Dim parSubEpisode As SQLiteParameter = SQLcommand.Parameters.Add("parSubEpisode", DbType.String, 0, "SubEpisode")
            Dim par_iLastPlayed As SQLiteParameter = SQLcommand.Parameters.Add("par_iLastPlayed", DbType.Int64, 0, "iLastPlayed")
            Dim par_strIMDB As SQLiteParameter = SQLcommand.Parameters.Add("par_strIMDB", DbType.String, 0, "strIMDB")
            Dim par_strTMDB As SQLiteParameter = SQLcommand.Parameters.Add("par_strTMDB", DbType.String, 0, "strTMDB")
            Dim par_strTVDB As SQLiteParameter = SQLcommand.Parameters.Add("par_strTVDB", DbType.String, 0, "strTVDB")
            Dim par_iUserRating As SQLiteParameter = SQLcommand.Parameters.Add("par_iUserRating", DbType.Int64, 0, "iUserRating")

            Try
                If Not Master.eSettings.GeneralDateAddedIgnoreNFO AndAlso _episode.TVEpisode.DateAddedSpecified Then
                    Dim DateTimeAdded As Date = Date.ParseExact(_episode.TVEpisode.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    parDateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    Select Case Master.eSettings.GeneralDateTime
                        Case Enums.DateTime.Now
                            Dim DateTimeAdded As Date
                            If Date.TryParseExact(_episode.TVEpisode.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, DateTimeAdded) Then
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                            Else
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                            End If
                        Case Enums.DateTime.ctime
                            Dim ctime As Date = File.GetCreationTime(_episode.Filename)
                            If ctime.Year > 1601 Then
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            Else
                                Dim mtime As Date = File.GetLastWriteTime(_episode.Filename)
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            End If
                        Case Enums.DateTime.mtime
                            Dim mtime As Date = File.GetLastWriteTime(_episode.Filename)
                            If mtime.Year > 1601 Then
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                Dim ctime As Date = File.GetCreationTime(_episode.Filename)
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                        Case Enums.DateTime.Newer
                            Dim mtime As Date = File.GetLastWriteTime(_episode.Filename)
                            Dim ctime As Date = File.GetCreationTime(_episode.Filename)
                            If mtime > ctime Then
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                    End Select
                End If
                _episode.TVEpisode.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(parDateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            Catch ex As Exception
                Dim DateTimeAdded As Date
                If Date.TryParseExact(_episode.TVEpisode.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, DateTimeAdded) Then
                    parDateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    parDateAdded.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                End If
                _episode.TVEpisode.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(parDateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            End Try

            Dim DateTimeLastPlayedUnix As Double = -1
            If _episode.TVEpisode.LastPlayedSpecified Then
                Try
                    Dim DateTimeLastPlayed As Date = Date.ParseExact(_episode.TVEpisode.LastPlayed, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    DateTimeLastPlayedUnix = Functions.ConvertToUnixTimestamp(DateTimeLastPlayed)
                Catch
                    'Kodi save it only as yyyy-MM-dd, try that
                    Try
                        Dim DateTimeLastPlayed As Date = Date.ParseExact(_episode.TVEpisode.LastPlayed, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture)
                        DateTimeLastPlayedUnix = Functions.ConvertToUnixTimestamp(DateTimeLastPlayed)
                    Catch
                        DateTimeLastPlayedUnix = -1
                    End Try
                End Try
            End If
            If DateTimeLastPlayedUnix >= 0 Then
                par_iLastPlayed.Value = DateTimeLastPlayedUnix
            Else
                par_iLastPlayed.Value = Nothing 'need to be NOTHING instead of 0
                _episode.TVEpisode.LastPlayed = String.Empty
            End If

            'First let's save it to NFO, even because we will need the NFO path, also save Images
            'art Table be be linked later
            If _episode.FilenameIDSpecified Then
                If bToNFO Then NFO.SaveToNFO_TVEpisode(_episode)
                If bToDisk Then
                    _episode.ImagesContainer.SaveAllImages(_episode, False)
                    _episode.TVEpisode.SaveAllActorThumbs(_episode)
                End If
            End If

            parTVShowID.Value = _episode.ShowID
            parNfoPath.Value = _episode.NfoPath
            parHasSub.Value = (_episode.Subtitles IsNot Nothing AndAlso _episode.Subtitles.Count > 0) OrElse _episode.TVEpisode.FileInfo.StreamDetails.Subtitle.Count > 0
            parNew.Value = bForceIsNewFlag OrElse Not _episode.IDSpecified
            parMark.Value = _episode.IsMark
            parTVFileID.Value = _episode.FilenameID
            parLock.Value = _episode.IsLock
            parSourceID.Value = _episode.Source.ID
            parVideoSource.Value = _episode.VideoSource

            With _episode.TVEpisode
                parTitle.Value = .Title
                parOriginalTitle.Value = .OriginalTitle
                parSeason.Value = .Season
                parEpisode.Value = .Episode
                parDisplaySeason.Value = .DisplaySeason
                parDisplayEpisode.Value = .DisplayEpisode
                par_iUserRating.Value = .UserRating
                parRating.Value = .Rating
                parPlot.Value = .Plot
                parAired.Value = NumUtils.DateToISO8601Date(.Aired)
                If .PlaycountSpecified Then 'need to be NOTHING instead of "0"
                    parPlaycount.Value = .Playcount
                End If
                parRuntime.Value = .Runtime
                parVotes.Value = .Votes
                If .SubEpisodeSpecified Then
                    parSubEpisode.Value = .SubEpisode
                End If
                par_strIMDB.Value = .UniqueIDs.IMDbId
                par_strTMDB.Value = .UniqueIDs.TMDbId
                par_strTVDB.Value = .UniqueIDs.TVDbId
            End With

            If Not _episode.IDSpecified Then
                If Master.eSettings.TVGeneralMarkNewEpisodes Then
                    parMark.Value = True
                    _episode.IsMark = True
                End If
                Using rdrTVEp As SQLiteDataReader = SQLcommand.ExecuteReader()
                    If rdrTVEp.Read Then
                        _episode.ID = Convert.ToInt64(rdrTVEp(0))
                    Else
                        logger.Error("Something very wrong here: SaveTVEpToDB", _episode.ToString, "Error")
                        _episode.ID = -1
                        Return _episode
                        Exit Function
                    End If
                End Using
            Else
                SQLcommand.ExecuteNonQuery()
            End If

            If _episode.IDSpecified Then

                'Actors
                Using SQLcommand_actorlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_actorlink.CommandText = String.Concat("DELETE FROM actorlinkepisode WHERE idEpisode = ", _episode.ID, ";")
                    SQLcommand_actorlink.ExecuteNonQuery()
                End Using
                AddCast(_episode.ID, "episode", "episode", _episode.TVEpisode.Actors)

                'Directors
                Using SQLcommand_directorlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_directorlink.CommandText = String.Format("DELETE FROM directorlinkepisode WHERE idEpisode = {0};", _episode.ID)
                    SQLcommand_directorlink.ExecuteNonQuery()
                End Using
                For Each director As String In _episode.TVEpisode.Directors
                    AddDirectorToEpisode(_episode.ID, AddActor(director, "", "", "", "", False))
                Next

                'Guest Stars
                Using SQLcommand_gueststarlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_gueststarlink.CommandText = String.Concat("DELETE FROM gueststarlinkepisode WHERE idEpisode = ", _episode.ID, ";")
                    SQLcommand_gueststarlink.ExecuteNonQuery()
                End Using
                AddGuestStar(_episode.ID, "episode", "episode", _episode.TVEpisode.GuestStars)

                'Images
                Using SQLcommand_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_art.CommandText = String.Concat("DELETE FROM art WHERE media_id = ", _episode.ID, " AND media_type = 'episode';")
                    SQLcommand_art.ExecuteNonQuery()
                End Using
                If Not String.IsNullOrEmpty(_episode.ImagesContainer.Fanart.LocalFilePath) Then SetArtForItem(_episode.ID, "episode", "fanart", _episode.ImagesContainer.Fanart.LocalFilePath)
                If Not String.IsNullOrEmpty(_episode.ImagesContainer.Poster.LocalFilePath) Then SetArtForItem(_episode.ID, "episode", "thumb", _episode.ImagesContainer.Poster.LocalFilePath)

                'Ratings
                SetRatingsForItem(_episode.ID, _episode.ContentType, _episode.TVEpisode.Ratings)

                'UniqueIDs
                SetUniqueIDsForItem(_episode.ID, _episode.ContentType, _episode.TVEpisode.UniqueIDs)

                'Writers
                Using SQLcommand_writerlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_writerlink.CommandText = String.Concat("DELETE FROM writerlinkepisode WHERE idEpisode = ", _episode.ID, ";")
                    SQLcommand_writerlink.ExecuteNonQuery()
                End Using
                For Each writer As String In _episode.TVEpisode.Credits
                    AddWriterToEpisode(_episode.ID, AddActor(writer, "", "", "", "", False))
                Next

                Using SQLcommandTVVStreams As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandTVVStreams.CommandText = String.Concat("DELETE FROM TVVStreams WHERE TVEpID = ", _episode.ID, ";")
                    SQLcommandTVVStreams.ExecuteNonQuery()
                    SQLcommandTVVStreams.CommandText = String.Concat("INSERT OR REPLACE INTO TVVStreams (",
                       "TVEpID, StreamID, Video_Width, Video_Height, Video_Codec, Video_Duration, Video_ScanType, Video_AspectDisplayRatio,",
                       "Video_Language, Video_LongLanguage, Video_Bitrate, Video_MultiViewCount, Video_FileSize, Video_MultiViewLayout, ",
                       "Video_StereoMode) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);")

                    Dim parVideo_EpID As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_EpID", DbType.Int64, 0, "TVEpID")
                    Dim parVideo_StreamID As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_StreamID", DbType.Int64, 0, "StreamID")
                    Dim parVideo_Width As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_Width", DbType.String, 0, "Video_Width")
                    Dim parVideo_Height As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_Height", DbType.String, 0, "Video_Height")
                    Dim parVideo_Codec As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_Codec", DbType.String, 0, "Video_Codec")
                    Dim parVideo_Duration As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_Duration", DbType.String, 0, "Video_Duration")
                    Dim parVideo_ScanType As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_ScanType", DbType.String, 0, "Video_ScanType")
                    Dim parVideo_AspectDisplayRatio As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_AspectDisplayRatio", DbType.String, 0, "Video_AspectDisplayRatio")
                    Dim parVideo_Language As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_Language", DbType.String, 0, "Video_Language")
                    Dim parVideo_LongLanguage As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_LongLanguage", DbType.String, 0, "Video_LongLanguage")
                    Dim parVideo_Bitrate As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_Bitrate", DbType.String, 0, "Video_Bitrate")
                    Dim parVideo_MultiViewCount As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_MultiViewCount", DbType.String, 0, "Video_MultiViewCount")
                    Dim parVideo_FileSize As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_FileSize", DbType.Int64, 0, "Video_FileSize")
                    Dim parVideo_MultiViewLayout As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_MultiViewLayout", DbType.String, 0, "Video_MultiViewLayout")
                    Dim parVideo_StereoMode As SQLiteParameter = SQLcommandTVVStreams.Parameters.Add("parVideo_StereoMode", DbType.String, 0, "Video_StereoMode")

                    For i As Integer = 0 To _episode.TVEpisode.FileInfo.StreamDetails.Video.Count - 1
                        parVideo_EpID.Value = _episode.ID
                        parVideo_StreamID.Value = i
                        parVideo_Width.Value = _episode.TVEpisode.FileInfo.StreamDetails.Video(i).Width
                        parVideo_Height.Value = _episode.TVEpisode.FileInfo.StreamDetails.Video(i).Height
                        parVideo_Codec.Value = _episode.TVEpisode.FileInfo.StreamDetails.Video(i).Codec
                        parVideo_Duration.Value = _episode.TVEpisode.FileInfo.StreamDetails.Video(i).Duration
                        parVideo_ScanType.Value = _episode.TVEpisode.FileInfo.StreamDetails.Video(i).Scantype
                        parVideo_AspectDisplayRatio.Value = _episode.TVEpisode.FileInfo.StreamDetails.Video(i).Aspect
                        parVideo_Language.Value = _episode.TVEpisode.FileInfo.StreamDetails.Video(i).Language
                        parVideo_LongLanguage.Value = _episode.TVEpisode.FileInfo.StreamDetails.Video(i).LongLanguage
                        parVideo_Bitrate.Value = _episode.TVEpisode.FileInfo.StreamDetails.Video(i).Bitrate
                        parVideo_MultiViewCount.Value = _episode.TVEpisode.FileInfo.StreamDetails.Video(i).MultiViewCount
                        parVideo_MultiViewLayout.Value = _episode.TVEpisode.FileInfo.StreamDetails.Video(i).MultiViewLayout
                        parVideo_FileSize.Value = _episode.TVEpisode.FileInfo.StreamDetails.Video(i).Filesize
                        parVideo_StereoMode.Value = _episode.TVEpisode.FileInfo.StreamDetails.Video(i).StereoMode

                        SQLcommandTVVStreams.ExecuteNonQuery()
                    Next
                End Using
                Using SQLcommandTVAStreams As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandTVAStreams.CommandText = String.Concat("DELETE FROM TVAStreams WHERE TVEpID = ", _episode.ID, ";")
                    SQLcommandTVAStreams.ExecuteNonQuery()
                    SQLcommandTVAStreams.CommandText = String.Concat("INSERT OR REPLACE INTO TVAStreams (",
                       "TVEpID, StreamID, Audio_Language, Audio_LongLanguage, Audio_Codec, Audio_Channel, Audio_Bitrate",
                       ") VALUES (?,?,?,?,?,?,?);")

                    Dim parAudio_EpID As SQLiteParameter = SQLcommandTVAStreams.Parameters.Add("parAudio_EpID", DbType.Int64, 0, "TVEpID")
                    Dim parAudio_StreamID As SQLiteParameter = SQLcommandTVAStreams.Parameters.Add("parAudio_StreamID", DbType.Int64, 0, "StreamID")
                    Dim parAudio_Language As SQLiteParameter = SQLcommandTVAStreams.Parameters.Add("parAudio_Language", DbType.String, 0, "Audio_Language")
                    Dim parAudio_LongLanguage As SQLiteParameter = SQLcommandTVAStreams.Parameters.Add("parAudio_LongLanguage", DbType.String, 0, "Audio_LongLanguage")
                    Dim parAudio_Codec As SQLiteParameter = SQLcommandTVAStreams.Parameters.Add("parAudio_Codec", DbType.String, 0, "Audio_Codec")
                    Dim parAudio_Channel As SQLiteParameter = SQLcommandTVAStreams.Parameters.Add("parAudio_Channel", DbType.String, 0, "Audio_Channel")
                    Dim parAudio_Bitrate As SQLiteParameter = SQLcommandTVAStreams.Parameters.Add("parAudio_Bitrate", DbType.String, 0, "Audio_Bitrate")

                    For i As Integer = 0 To _episode.TVEpisode.FileInfo.StreamDetails.Audio.Count - 1
                        parAudio_EpID.Value = _episode.ID
                        parAudio_StreamID.Value = i
                        parAudio_Language.Value = _episode.TVEpisode.FileInfo.StreamDetails.Audio(i).Language
                        parAudio_LongLanguage.Value = _episode.TVEpisode.FileInfo.StreamDetails.Audio(i).LongLanguage
                        parAudio_Codec.Value = _episode.TVEpisode.FileInfo.StreamDetails.Audio(i).Codec
                        parAudio_Channel.Value = _episode.TVEpisode.FileInfo.StreamDetails.Audio(i).Channels
                        parAudio_Bitrate.Value = _episode.TVEpisode.FileInfo.StreamDetails.Audio(i).Bitrate

                        SQLcommandTVAStreams.ExecuteNonQuery()
                    Next
                End Using

                'subtitles
                Using SQLcommandTVSubs As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandTVSubs.CommandText = String.Concat("DELETE FROM TVSubs WHERE TVEpID = ", _episode.ID, ";")
                    SQLcommandTVSubs.ExecuteNonQuery()

                    SQLcommandTVSubs.CommandText = String.Concat("INSERT OR REPLACE INTO TVSubs (",
                       "TVEpID, StreamID, Subs_Language, Subs_LongLanguage, Subs_Type, Subs_Path, Subs_Forced",
                       ") VALUES (?,?,?,?,?,?,?);")
                    Dim parSubs_EpID As SQLiteParameter = SQLcommandTVSubs.Parameters.Add("parSubs_EpID", DbType.Int64, 0, "TVEpID")
                    Dim parSubs_StreamID As SQLiteParameter = SQLcommandTVSubs.Parameters.Add("parSubs_StreamID", DbType.Int64, 0, "StreamID")
                    Dim parSubs_Language As SQLiteParameter = SQLcommandTVSubs.Parameters.Add("parSubs_Language", DbType.String, 0, "Subs_Language")
                    Dim parSubs_LongLanguage As SQLiteParameter = SQLcommandTVSubs.Parameters.Add("parSubs_LongLanguage", DbType.String, 0, "Subs_LongLanguage")
                    Dim parSubs_Type As SQLiteParameter = SQLcommandTVSubs.Parameters.Add("parSubs_Type", DbType.String, 0, "Subs_Type")
                    Dim parSubs_Path As SQLiteParameter = SQLcommandTVSubs.Parameters.Add("parSubs_Path", DbType.String, 0, "Subs_Path")
                    Dim parSubs_Forced As SQLiteParameter = SQLcommandTVSubs.Parameters.Add("parSubs_Forced", DbType.Boolean, 0, "Subs_Forced")
                    Dim iID As Integer = 0
                    'embedded subtitles
                    For i As Integer = 0 To _episode.TVEpisode.FileInfo.StreamDetails.Subtitle.Count - 1
                        parSubs_EpID.Value = _episode.ID
                        parSubs_StreamID.Value = iID
                        parSubs_Language.Value = _episode.TVEpisode.FileInfo.StreamDetails.Subtitle(i).Language
                        parSubs_LongLanguage.Value = _episode.TVEpisode.FileInfo.StreamDetails.Subtitle(i).LongLanguage
                        parSubs_Type.Value = _episode.TVEpisode.FileInfo.StreamDetails.Subtitle(i).Type
                        parSubs_Path.Value = _episode.TVEpisode.FileInfo.StreamDetails.Subtitle(i).Path
                        parSubs_Forced.Value = _episode.TVEpisode.FileInfo.StreamDetails.Subtitle(i).Forced
                        SQLcommandTVSubs.ExecuteNonQuery()
                        iID += 1
                    Next
                    'external subtitles
                    If _episode.Subtitles IsNot Nothing Then
                        For i As Integer = 0 To _episode.Subtitles.Count - 1
                            parSubs_EpID.Value = _episode.ID
                            parSubs_StreamID.Value = iID
                            parSubs_Language.Value = _episode.Subtitles(i).Language
                            parSubs_LongLanguage.Value = _episode.Subtitles(i).LongLanguage
                            parSubs_Type.Value = _episode.Subtitles(i).Type
                            parSubs_Path.Value = _episode.Subtitles(i).Path
                            parSubs_Forced.Value = _episode.Subtitles(i).Forced
                            SQLcommandTVSubs.ExecuteNonQuery()
                            iID += 1
                        Next
                    End If
                End Using

                If bDoSeasonCheck Then
                    Using SQLSeasonCheck As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        SQLSeasonCheck.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0} AND Season = {1}", _episode.ShowID, _episode.TVEpisode.Season)
                        Using SQLreader As SQLiteDataReader = SQLSeasonCheck.ExecuteReader()
                            If Not SQLreader.HasRows Then
                                Dim _season As New DBElement(Enums.ContentType.TVSeason) With {.ShowID = _episode.ShowID, .TVSeason = New MediaContainers.SeasonDetails With {.Season = _episode.TVEpisode.Season}}
                                Save_TVSeason(_season, True, False, True)
                            End If
                        End Using
                    End Using
                End If
            End If
        End Using
        If Not bBatchMode Then SQLtransaction.Commit()

        If _episode.FilenameIDSpecified AndAlso bDoSync Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Sync_TVEpisode, Nothing, Nothing, False, _episode)
        End If

        Return _episode
    End Function
    ''' <summary>
    ''' Stores information for a single season to the database
    ''' </summary>
    ''' <param name="_season">Database.DBElement representing the season to be stored.</param>
    ''' <param name="bBatchMode"></param>
    ''' <remarks>Note that this stores the season information, not the individual episodes within that season</remarks>
    Public Function Save_TVSeason(ByRef _season As DBElement, ByVal bBatchMode As Boolean, ByVal bToDisk As Boolean, ByVal bDoSync As Boolean) As DBElement
        If _season.TVSeason Is Nothing Then Return _season

        Dim doesExist As Boolean = False
        Dim ID As Long = -1

        Using SQLcommand_select_seasons As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_select_seasons.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0} AND Season = {1}", _season.ShowID, _season.TVSeason.Season)
            Using SQLreader As SQLiteDataReader = SQLcommand_select_seasons.ExecuteReader()
                While SQLreader.Read
                    doesExist = True
                    ID = CInt(SQLreader("idSeason"))
                    Exit While
                End While
            End Using
        End Using

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not bBatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        If Not doesExist Then
            Using SQLcommand_insert_seasons As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand_insert_seasons.CommandText = String.Concat("INSERT INTO seasons (",
                                                                      "idSeason, idShow, Season, SeasonText, Lock, Mark, New, strTVDB, strTMDB, strAired, strPlot",
                                                                      ") VALUES (NULL,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM seasons;")
                Dim par_seasons_idShow As SQLiteParameter = SQLcommand_insert_seasons.Parameters.Add("par_seasons_idShow", DbType.Int64, 0, "idShow")
                Dim par_seasons_Season As SQLiteParameter = SQLcommand_insert_seasons.Parameters.Add("par_seasons_Season", DbType.Int64, 0, "Season")
                Dim par_seasons_SeasonText As SQLiteParameter = SQLcommand_insert_seasons.Parameters.Add("par_seasons_SeasonText", DbType.String, 0, "SeasonText")
                Dim par_seasons_Lock As SQLiteParameter = SQLcommand_insert_seasons.Parameters.Add("par_seasons_Lock", DbType.Boolean, 0, "Lock")
                Dim par_seasons_Mark As SQLiteParameter = SQLcommand_insert_seasons.Parameters.Add("par_seasons_Mark", DbType.Boolean, 0, "Mark")
                Dim par_seasons_New As SQLiteParameter = SQLcommand_insert_seasons.Parameters.Add("par_seasons_New", DbType.Boolean, 0, "New")
                Dim par_seasons_strTVDB As SQLiteParameter = SQLcommand_insert_seasons.Parameters.Add("par_seasons_strTVDB", DbType.String, 0, "strTVDB")
                Dim par_seasons_strTMDB As SQLiteParameter = SQLcommand_insert_seasons.Parameters.Add("par_seasons_strTMDB", DbType.String, 0, "strTMDB")
                Dim par_seasons_strAired As SQLiteParameter = SQLcommand_insert_seasons.Parameters.Add("par_seasons_strAired", DbType.String, 0, "strAired")
                Dim par_seasons_strPlot As SQLiteParameter = SQLcommand_insert_seasons.Parameters.Add("par_seasons_strPlot", DbType.String, 0, "strPlot")
                par_seasons_idShow.Value = _season.ShowID
                par_seasons_Season.Value = _season.TVSeason.Season
                par_seasons_SeasonText.Value = If(_season.TVSeason.TitleSpecified, _season.TVSeason.Title, StringUtils.FormatSeasonText(_season.TVSeason.Season))
                par_seasons_Lock.Value = _season.IsLock
                par_seasons_Mark.Value = _season.IsMark
                par_seasons_New.Value = True
                par_seasons_strTVDB.Value = _season.TVSeason.UniqueIDs.TVDbId
                par_seasons_strTMDB.Value = _season.TVSeason.UniqueIDs.TMDbId
                par_seasons_strAired.Value = _season.TVSeason.Aired
                par_seasons_strPlot.Value = _season.TVSeason.Plot
                ID = CInt(SQLcommand_insert_seasons.ExecuteScalar())
            End Using
        Else
            Using SQLcommand_update_seasons As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand_update_seasons.CommandText = String.Format("UPDATE seasons SET SeasonText=?, Lock=?, Mark=?, New=?, strTVDB=?, strTMDB=?, strAired=?, strPlot=? WHERE idSeason={0}", ID)
                Dim par_seasons_SeasonText As SQLiteParameter = SQLcommand_update_seasons.Parameters.Add("par_seasons_SeasonText", DbType.String, 0, "SeasonText")
                Dim par_seasons_Lock As SQLiteParameter = SQLcommand_update_seasons.Parameters.Add("par_seasons_Lock", DbType.Boolean, 0, "Lock")
                Dim par_seasons_Mark As SQLiteParameter = SQLcommand_update_seasons.Parameters.Add("par_seasons_Mark", DbType.Boolean, 0, "Mark")
                Dim par_seasons_New As SQLiteParameter = SQLcommand_update_seasons.Parameters.Add("par_seasons_New", DbType.Boolean, 0, "New")
                Dim par_seasons_strTVDB As SQLiteParameter = SQLcommand_update_seasons.Parameters.Add("par_seasons_strTVDB", DbType.String, 0, "strTVDB")
                Dim par_seasons_strTMDB As SQLiteParameter = SQLcommand_update_seasons.Parameters.Add("par_seasons_strTMDB", DbType.String, 0, "strTMDB")
                Dim par_seasons_strAired As SQLiteParameter = SQLcommand_update_seasons.Parameters.Add("par_seasons_strAired", DbType.String, 0, "strAired")
                Dim par_seasons_strPlot As SQLiteParameter = SQLcommand_update_seasons.Parameters.Add("par_seasons_strPlot", DbType.String, 0, "strPlot")
                par_seasons_SeasonText.Value = If(_season.TVSeason.TitleSpecified, _season.TVSeason.Title, StringUtils.FormatSeasonText(_season.TVSeason.Season))
                par_seasons_Lock.Value = _season.IsLock
                par_seasons_Mark.Value = _season.IsMark
                par_seasons_New.Value = False
                par_seasons_strTVDB.Value = _season.TVSeason.UniqueIDs.TVDbId
                par_seasons_strTMDB.Value = _season.TVSeason.UniqueIDs.TMDbId
                par_seasons_strAired.Value = _season.TVSeason.Aired
                par_seasons_strPlot.Value = _season.TVSeason.Plot
                SQLcommand_update_seasons.ExecuteNonQuery()
            End Using
        End If

        _season.ID = ID

        'Images
        If bToDisk Then _season.ImagesContainer.SaveAllImages(_season, False)

        Using SQLcommand_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_art.CommandText = String.Concat("DELETE FROM art WHERE media_id = ", _season.ID, " AND media_type = 'season';")
            SQLcommand_art.ExecuteNonQuery()
        End Using
        If Not String.IsNullOrEmpty(_season.ImagesContainer.Banner.LocalFilePath) Then SetArtForItem(_season.ID, "season", "banner", _season.ImagesContainer.Banner.LocalFilePath)
        If Not String.IsNullOrEmpty(_season.ImagesContainer.Fanart.LocalFilePath) Then SetArtForItem(_season.ID, "season", "fanart", _season.ImagesContainer.Fanart.LocalFilePath)
        If Not String.IsNullOrEmpty(_season.ImagesContainer.Landscape.LocalFilePath) Then SetArtForItem(_season.ID, "season", "landscape", _season.ImagesContainer.Landscape.LocalFilePath)
        If Not String.IsNullOrEmpty(_season.ImagesContainer.Poster.LocalFilePath) Then SetArtForItem(_season.ID, "season", "poster", _season.ImagesContainer.Poster.LocalFilePath)

        'UniqueIDs
        SetUniqueIDsForItem(_season.ID, _season.ContentType, _season.TVSeason.UniqueIDs)

        If Not bBatchMode Then SQLtransaction.Commit()

        If bDoSync Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Sync_TVSeason, Nothing, Nothing, False, _season)
        End If

        Return _season
    End Function
    ''' <summary>
    ''' Saves all show information from a Database.DBElement object to the database
    ''' </summary>
    ''' <param name="_show">Database.DBElement object to save to the database</param>
    ''' <param name="bBatchMode">Is the function already part of a transaction?</param>
    ''' <param name="bToDisk">Create NFO and Images</param>
    Public Function Save_TVShow(ByRef _show As DBElement, ByVal bBatchMode As Boolean, ByVal bToNFO As Boolean, ByVal bToDisk As Boolean, ByVal bWithEpisodes As Boolean) As DBElement
        If _show.TVShow Is Nothing Then Return _show

        Dim SQLtransaction As SQLiteTransaction = Nothing

        If Not bBatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not _show.IDSpecified Then
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO tvshow (",
                 "idSource, TVShowPath, New, Mark, TVDB, Lock, ListTitle, EpisodeGuide, ",
                 "Plot, Premiered, MPAA, Rating, NfoPath, Language, Ordering, ",
                 "Status, ThemePath, EFanartsPath, Runtime, Title, Votes, EpisodeSorting, SortTitle, ",
                 "strIMDB, strTMDB, strOriginalTitle, iUserRating",
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM tvshow;")
            Else
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO tvshow (",
                 "idShow, idSource, TVShowPath, New, Mark, TVDB, Lock, ListTitle, EpisodeGuide, ",
                 "Plot, Premiered, MPAA, Rating, NfoPath, Language, Ordering, ",
                 "Status, ThemePath, EFanartsPath, Runtime, Title, Votes, EpisodeSorting, SortTitle, ",
                 "strIMDB, strTMDB, strOriginalTitle, iUserRating",
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM tvshow;")
                Dim par_lngTVShowID As SQLiteParameter = SQLcommand.Parameters.Add("parTVShowID", DbType.Int64, 0, "idShow")
                par_lngTVShowID.Value = _show.ID
            End If

            Dim par_lngTVSourceID As SQLiteParameter = SQLcommand.Parameters.Add("parTVSourceID", DbType.Int64, 0, "idSource")
            Dim par_strTVShowPath As SQLiteParameter = SQLcommand.Parameters.Add("parTVShowPath", DbType.String, 0, "TVShowPath")
            Dim par_bNew As SQLiteParameter = SQLcommand.Parameters.Add("parNew", DbType.Boolean, 0, "new")
            Dim par_bMark As SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
            Dim par_strTVDB As SQLiteParameter = SQLcommand.Parameters.Add("parTVDB", DbType.String, 0, "TVDB")
            Dim par_bLock As SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "lock")
            Dim par_strListTitle As SQLiteParameter = SQLcommand.Parameters.Add("parListTitle", DbType.String, 0, "ListTitle")
            Dim par_strEpisodeGuide As SQLiteParameter = SQLcommand.Parameters.Add("parEpisodeGuide", DbType.String, 0, "EpisodeGuide")
            Dim par_strPlot As SQLiteParameter = SQLcommand.Parameters.Add("parPlot", DbType.String, 0, "Plot")
            Dim par_strPremiered As SQLiteParameter = SQLcommand.Parameters.Add("parPremiered", DbType.String, 0, "Premiered")
            Dim par_strMPAA As SQLiteParameter = SQLcommand.Parameters.Add("parMPAA", DbType.String, 0, "MPAA")
            Dim par_strRating As SQLiteParameter = SQLcommand.Parameters.Add("parRating", DbType.String, 0, "Rating")
            Dim par_strNfoPath As SQLiteParameter = SQLcommand.Parameters.Add("parNfoPath", DbType.String, 0, "NfoPath")
            Dim par_strLanguage As SQLiteParameter = SQLcommand.Parameters.Add("parLanguage", DbType.String, 0, "Language")
            Dim par_iOrdering As SQLiteParameter = SQLcommand.Parameters.Add("parOrdering", DbType.Int16, 0, "Ordering")
            Dim par_strStatus As SQLiteParameter = SQLcommand.Parameters.Add("parStatus", DbType.String, 0, "Status")
            Dim par_strThemePath As SQLiteParameter = SQLcommand.Parameters.Add("parThemePath", DbType.String, 0, "ThemePath")
            Dim par_strExtrafanartsPath As SQLiteParameter = SQLcommand.Parameters.Add("parEFanartsPath", DbType.String, 0, "EFanartsPath")
            Dim par_strRuntime As SQLiteParameter = SQLcommand.Parameters.Add("parRuntime", DbType.String, 0, "Runtime")
            Dim par_strTitle As SQLiteParameter = SQLcommand.Parameters.Add("parTitle", DbType.String, 0, "Title")
            Dim par_strVotes As SQLiteParameter = SQLcommand.Parameters.Add("parVotes", DbType.String, 0, "Votes")
            Dim par_iEpisodeSorting As SQLiteParameter = SQLcommand.Parameters.Add("parEpisodeSorting", DbType.Int16, 0, "EpisodeSorting")
            Dim par_strSortTitle As SQLiteParameter = SQLcommand.Parameters.Add("parSortTitle", DbType.String, 0, "SortTitle")
            Dim par_strIMDB As SQLiteParameter = SQLcommand.Parameters.Add("par_strIMDB", DbType.String, 0, "strIMDB")
            Dim par_strTMDB As SQLiteParameter = SQLcommand.Parameters.Add("par_strTMDB", DbType.String, 0, "strTMDB")
            Dim par_strOriginalTitle As SQLiteParameter = SQLcommand.Parameters.Add("par_strOriginalTitle", DbType.String, 0, "strOriginalTitle")
            Dim par_iUserRating As SQLiteParameter = SQLcommand.Parameters.Add("par_iUserRating", DbType.Int64, 0, "iUserRating")

            With _show.TVShow
                par_iUserRating.Value = .UserRating
                par_strEpisodeGuide.Value = .EpisodeGuide.URL
                par_strIMDB.Value = .UniqueIDs.IMDbId
                par_strMPAA.Value = .MPAA
                par_strOriginalTitle.Value = .OriginalTitle
                par_strPlot.Value = .Plot
                par_strPremiered.Value = NumUtils.DateToISO8601Date(.Premiered)
                par_strRating.Value = .Rating
                par_strRuntime.Value = .Runtime
                par_strSortTitle.Value = .SortTitle
                par_strStatus.Value = .Status
                par_strTMDB.Value = .UniqueIDs.TMDbId
                par_strTVDB.Value = .UniqueIDs.TVDbId
                par_strTitle.Value = .Title
                par_strVotes.Value = .Votes
            End With

            'First let's save it to NFO, even because we will need the NFO path
            'Also Save Images to get ExtrafanartsPath
            'art Table be be linked later
            If bToNFO Then NFO.SaveToNFO_TVShow(_show)
            If bToDisk Then
                _show.ImagesContainer.SaveAllImages(_show, False)
                _show.Theme.Save(_show, Enums.ModifierType.MainTheme, False)
                _show.TVShow.SaveAllActorThumbs(_show)
            End If

            par_strExtrafanartsPath.Value = _show.ExtrafanartsPath
            par_strNfoPath.Value = _show.NfoPath
            par_strThemePath.Value = If(Not String.IsNullOrEmpty(_show.Theme.LocalFilePath), _show.Theme.LocalFilePath, String.Empty)
            par_strTVShowPath.Value = _show.ShowPath

            par_bNew.Value = Not _show.IDSpecified
            par_strListTitle.Value = _show.ListTitle
            par_bMark.Value = _show.IsMark
            par_bLock.Value = _show.IsLock
            par_lngTVSourceID.Value = _show.Source.ID
            par_strLanguage.Value = _show.Language
            par_iOrdering.Value = _show.Ordering
            par_iEpisodeSorting.Value = _show.EpisodeSorting

            If Not _show.IDSpecified Then
                If Master.eSettings.TVGeneralMarkNewShows Then
                    par_bMark.Value = True
                    _show.IsMark = True
                End If
                Using rdrTVShow As SQLiteDataReader = SQLcommand.ExecuteReader()
                    If rdrTVShow.Read Then
                        _show.ID = Convert.ToInt64(rdrTVShow(0))
                        _show.ShowID = _show.ID
                    Else
                        logger.Error("Something very wrong here: SaveTVShowToDB", _show.ToString, "Error")
                        _show.ID = -1
                        _show.ShowID = _show.ID
                        Return _show
                        Exit Function
                    End If
                End Using
            Else
                SQLcommand.ExecuteNonQuery()
            End If

            If Not _show.ID = -1 Then

                'Actors
                Using SQLcommand_actorlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_actorlink.CommandText = String.Format("DELETE FROM actorlinktvshow WHERE idShow = {0};", _show.ID)
                    SQLcommand_actorlink.ExecuteNonQuery()
                End Using
                AddCast(_show.ID, "tvshow", "show", _show.TVShow.Actors)

                'Creators
                Using SQLcommand_creatorlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_creatorlink.CommandText = String.Format("DELETE FROM creatorlinktvshow WHERE idShow = {0};", _show.ID)
                    SQLcommand_creatorlink.ExecuteNonQuery()
                End Using
                For Each creator As String In _show.TVShow.Creators
                    AddCreatorToTvShow(_show.ID, AddActor(creator, "", "", "", "", False))
                Next

                'Countries
                Using SQLcommand_countrylink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_countrylink.CommandText = String.Format("DELETE FROM countrylinktvshow WHERE idShow = {0};", _show.ID)
                    SQLcommand_countrylink.ExecuteNonQuery()
                End Using
                For Each country As String In _show.TVShow.Countries
                    AddCountryToTVShow(_show.ID, AddCountry(country))
                Next

                'Genres
                Using SQLcommand_genrelink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_genrelink.CommandText = String.Format("DELETE FROM genrelinktvshow WHERE idShow = {0};", _show.ID)
                    SQLcommand_genrelink.ExecuteNonQuery()
                End Using
                For Each genre As String In _show.TVShow.Genres
                    AddGenreToTvShow(_show.ID, AddGenre(genre))
                Next

                'Images
                Using SQLcommand_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_art.CommandText = String.Format("DELETE FROM art WHERE media_id = {0} AND media_type = 'tvshow';", _show.ID)
                    SQLcommand_art.ExecuteNonQuery()
                End Using
                If Not String.IsNullOrEmpty(_show.ImagesContainer.Banner.LocalFilePath) Then SetArtForItem(_show.ID, "tvshow", "banner", _show.ImagesContainer.Banner.LocalFilePath)
                If Not String.IsNullOrEmpty(_show.ImagesContainer.CharacterArt.LocalFilePath) Then SetArtForItem(_show.ID, "tvshow", "characterart", _show.ImagesContainer.CharacterArt.LocalFilePath)
                If Not String.IsNullOrEmpty(_show.ImagesContainer.ClearArt.LocalFilePath) Then SetArtForItem(_show.ID, "tvshow", "clearart", _show.ImagesContainer.ClearArt.LocalFilePath)
                If Not String.IsNullOrEmpty(_show.ImagesContainer.ClearLogo.LocalFilePath) Then SetArtForItem(_show.ID, "tvshow", "clearlogo", _show.ImagesContainer.ClearLogo.LocalFilePath)
                If Not String.IsNullOrEmpty(_show.ImagesContainer.Fanart.LocalFilePath) Then SetArtForItem(_show.ID, "tvshow", "fanart", _show.ImagesContainer.Fanart.LocalFilePath)
                If Not String.IsNullOrEmpty(_show.ImagesContainer.Keyart.LocalFilePath) Then SetArtForItem(_show.ID, "tvshow", "keyart", _show.ImagesContainer.Keyart.LocalFilePath)
                If Not String.IsNullOrEmpty(_show.ImagesContainer.Landscape.LocalFilePath) Then SetArtForItem(_show.ID, "tvshow", "landscape", _show.ImagesContainer.Landscape.LocalFilePath)
                If Not String.IsNullOrEmpty(_show.ImagesContainer.Poster.LocalFilePath) Then SetArtForItem(_show.ID, "tvshow", "poster", _show.ImagesContainer.Poster.LocalFilePath)

                'Ratings
                SetRatingsForItem(_show.ID, _show.ContentType, _show.TVShow.Ratings)

                'Studios
                Using SQLcommand_studiolink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_studiolink.CommandText = String.Format("DELETE FROM studiolinktvshow WHERE idShow = {0};", _show.ID)
                    SQLcommand_studiolink.ExecuteNonQuery()
                End Using
                For Each studio As String In _show.TVShow.Studios
                    AddStudioToTvShow(_show.ID, AddStudio(studio))
                Next

                'Tags
                Using SQLcommand_taglinks As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_taglinks.CommandText = String.Format("DELETE FROM taglinks WHERE idMedia = {0} AND media_type = 'tvshow';", _show.ID)
                    SQLcommand_taglinks.ExecuteNonQuery()
                End Using
                For Each tag As String In _show.TVShow.Tags
                    AddTagToItem(_show.ID, AddTag(tag), "tvshow")
                Next

                'UniqueIDs
                SetUniqueIDsForItem(_show.ID, _show.ContentType, _show.TVShow.UniqueIDs)
            End If
        End Using

        'save season informations
        If _show.SeasonsSpecified Then
            For Each nSeason As DBElement In _show.Seasons
                Save_TVSeason(nSeason, True, True, True)
            Next
            Delete_Invalid_TVSeasons(_show.Seasons, _show.ID, True)
        End If

        'save episode informations
        If bWithEpisodes AndAlso _show.EpisodesSpecified Then
            For Each nEpisode As DBElement In _show.Episodes
                Save_TVEpisode(nEpisode, True, True, True, False, True)
            Next
            Delete_Invalid_TVEpisodes(_show.Episodes, _show.ID, True)
        End If

        'delete empty seasons after saving all known episodes
        Delete_Empty_TVSeasons(_show.ID, True)

        If Not bBatchMode Then SQLtransaction.Commit()

        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Sync_TVShow, Nothing, Nothing, False, _show)

        Return _show
    End Function

    Private Sub SetArtForItem(ByVal mediaId As Long, ByVal MediaType As String, ByVal artType As String, ByVal url As String)
        Dim doesExist As Boolean = False
        Dim ID As Long = -1
        Dim oldURL As String = String.Empty

        Using SQLcommand_select_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_select_art.CommandText = String.Format("SELECT art_id, url FROM art WHERE media_id={0} AND media_type='{1}' AND type='{2}'", mediaId, MediaType, artType)
            Using SQLreader As SQLiteDataReader = SQLcommand_select_art.ExecuteReader()
                While SQLreader.Read
                    doesExist = True
                    ID = CInt(SQLreader("art_id"))
                    oldURL = SQLreader("url").ToString
                    Exit While
                End While
            End Using
        End Using

        If Not doesExist Then
            Using SQLcommand_insert_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand_insert_art.CommandText = String.Format("INSERT INTO art(media_id, media_type, type, url) VALUES ({0}, '{1}', '{2}', ?)", mediaId, MediaType, artType)
                Dim par_insert_art_url As SQLiteParameter = SQLcommand_insert_art.Parameters.Add("par_insert_art_url", DbType.String, 0, "url")
                par_insert_art_url.Value = url
                SQLcommand_insert_art.ExecuteNonQuery()
            End Using
        Else
            If Not url = oldURL Then
                Using SQLcommand_update_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_update_art.CommandText = String.Format("UPDATE art SET url=(?) WHERE art_id={0}", ID)
                    Dim par_update_art_url As SQLiteParameter = SQLcommand_update_art.Parameters.Add("par_update_art_url", DbType.String, 0, "url")
                    par_update_art_url.Value = url
                    SQLcommand_update_art.ExecuteNonQuery()
                End Using
            End If
        End If
    End Sub

    Private Sub SetRatingsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal ratings As List(Of MediaContainers.RatingDetails))
        RemoveRatingsFromItem(idMedia, contentType)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry In ratings
                entry.ID = AddRating(idMedia, mediaType, entry)
            Next
        End If
    End Sub

    Private Sub SetMoviesetsForMovie(ByVal dbElement As DBElement, ByRef moviesets As List(Of MediaContainers.SetDetails))
        RemoveMoviesetFromMovie(dbElement.ID)
        For Each entry As MediaContainers.SetDetails In moviesets
            Dim bIsNewSet As Boolean = Not entry.IDSpecified
            If entry.TitleSpecified Then
                If Not bIsNewSet Then
                    Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        sqlCommand.CommandText = String.Concat("INSERT OR REPLACE INTO setlinkmovie (",
                                                               "idSet,",
                                                               "idMovie,",
                                                               "iOrder",
                                                               ") VALUES (?,?,?);")
                        Dim par_idSet As SQLiteParameter = sqlCommand.Parameters.Add("par_idSet", DbType.Int64, 0, "idSet")
                        Dim par_idMovie As SQLiteParameter = sqlCommand.Parameters.Add("par_idMovie", DbType.Int64, 0, "idMovie")
                        Dim par_iOrder As SQLiteParameter = sqlCommand.Parameters.Add("par_iOrder", DbType.Int64, 0, "iOrder")

                        par_idSet.Value = entry.ID
                        par_idMovie.Value = dbElement.ID
                        par_iOrder.Value = entry.Order
                        sqlCommand.ExecuteNonQuery()
                    End Using
                Else
                    'first check if a movieset with the same TMDBColID is already existing
                    If entry.UniqueIDs.TMDbIdSpecified Then
                        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand.CommandText = String.Format("SELECT sets.idSet, sets.SetName, sets.Plot FROM uniqueid INNER JOIN sets ON (uniqueid.media_id = sets.idSet) WHERE uniqueid.media_type = 'movieset' AND uniqueid.type = 'tmdb' AND uniqueid.value = '{0}'", entry.UniqueIDs.TMDbId)
                            Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                                If sqlReader.HasRows Then
                                    sqlReader.Read()
                                    If Not DBNull.Value.Equals(sqlReader("idSet")) Then entry.ID = CLng(sqlReader("idSet"))
                                    If Not DBNull.Value.Equals(sqlReader("SetName")) Then entry.Title = CStr(sqlReader("SetName"))
                                    If Not DBNull.Value.Equals(sqlReader("Plot")) AndAlso
                                            Not String.IsNullOrEmpty(CStr(sqlReader("Plot"))) Then entry.Plot = CStr(sqlReader("Plot"))
                                    bIsNewSet = False
                                    NFO.SaveToNFO_Movie(dbElement, False) 'to save the "new" SetName and/or SetPlot
                                Else
                                    bIsNewSet = True
                                End If
                            End Using
                        End Using
                    End If

                    If bIsNewSet Then
                        'secondly check if a movieset with the same name is already existing
                        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand.CommandText = String.Concat("SELECT idSet, Plot ",
                                                                       "FROM sets WHERE SetName LIKE """, entry.Title, """;")
                            Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                                If sqlReader.HasRows Then
                                    sqlReader.Read()
                                    If Not DBNull.Value.Equals(sqlReader("idSet")) Then entry.ID = CLng(sqlReader("idSet"))
                                    If Not DBNull.Value.Equals(sqlReader("Plot")) AndAlso
                                                    Not String.IsNullOrEmpty(CStr(sqlReader("Plot"))) Then entry.Plot = CStr(sqlReader("Plot"))
                                    bIsNewSet = False
                                    NFO.SaveToNFO_Movie(dbElement, False) 'to save the "new" SetName and/or SetPlot
                                Else
                                    bIsNewSet = True
                                End If
                            End Using
                        End Using
                    End If

                    If Not bIsNewSet Then
                        'create new movieset_link entry with existing idSet
                        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand.CommandText = String.Concat("INSERT OR REPLACE INTO setlinkmovie (",
                                                                   "idSet,",
                                                                   "idMovie,",
                                                                   "iOrder",
                                                                   ") VALUES (?,?,?);")
                            Dim par_idSet As SQLiteParameter = sqlCommand.Parameters.Add("par_idSet", DbType.Int64, 0, "idSet")
                            Dim par_idMovie As SQLiteParameter = sqlCommand.Parameters.Add("par_idMovie", DbType.Int64, 0, "idMovie")
                            Dim par_iOrder As SQLiteParameter = sqlCommand.Parameters.Add("par_iOrder", DbType.Int64, 0, "iOrder")

                            par_idSet.Value = entry.ID
                            par_idMovie.Value = dbElement.ID
                            par_iOrder.Value = entry.Order
                            sqlCommand.ExecuteNonQuery()
                        End Using

                        'update existing movieset with latest TMDB Collection ID and Plot
                        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand.CommandText = String.Format("UPDATE sets SET Plot=? WHERE idSet={0}", entry.ID)
                            Dim par_Plot As SQLiteParameter = sqlCommand.Parameters.Add("par_Plot", DbType.String, 0, "Plot")
                            par_Plot.Value = entry.Plot
                            sqlCommand.ExecuteNonQuery()
                        End Using
                    Else
                        'create new movieset
                        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand.CommandText = String.Concat("INSERT OR REPLACE INTO sets (",
                                                                   "ListTitle,",
                                                                   "NfoPath,",
                                                                   "TMDBColID,",
                                                                   "Plot,",
                                                                   "SetName,",
                                                                   "New,",
                                                                   "Mark,",
                                                                   "Lock,",
                                                                   "SortMethod,",
                                                                   "Language",
                                                                   ") VALUES (?,?,?,?,?,?,?,?,?,?);")
                            Dim par_ListTitle As SQLiteParameter = sqlCommand.Parameters.Add("par_ListTitle", DbType.String, 0, "ListTitle")
                            Dim par_NfoPath As SQLiteParameter = sqlCommand.Parameters.Add("par_NfoPath", DbType.String, 0, "NfoPath")
                            Dim par_TMDBColID As SQLiteParameter = sqlCommand.Parameters.Add("par_TMDBColID", DbType.String, 0, "TMDBColID")
                            Dim par_Plot As SQLiteParameter = sqlCommand.Parameters.Add("par_Plot", DbType.String, 0, "Plot")
                            Dim par_SetName As SQLiteParameter = sqlCommand.Parameters.Add("par_SetName", DbType.String, 0, "SetName")
                            Dim par_New As SQLiteParameter = sqlCommand.Parameters.Add("par_New", DbType.Boolean, 0, "New")
                            Dim par_Mark As SQLiteParameter = sqlCommand.Parameters.Add("par_Mark", DbType.Boolean, 0, "Mark")
                            Dim par_Lock As SQLiteParameter = sqlCommand.Parameters.Add("par_Lock", DbType.Boolean, 0, "Lock")
                            Dim par_SortMethod As SQLiteParameter = sqlCommand.Parameters.Add("par_SortMethod", DbType.Int64, 0, "SortMethod")
                            Dim par_Language As SQLiteParameter = sqlCommand.Parameters.Add("par_Language", DbType.String, 0, "Language")

                            par_SetName.Value = entry.Title
                            par_ListTitle.Value = StringUtils.SortTokens_MovieSet(entry.Title)
                            par_TMDBColID.Value = If(entry.UniqueIDs.TMDbIdSpecified, entry.UniqueIDs.TMDbId.ToString, String.Empty)
                            par_Plot.Value = entry.Plot
                            par_NfoPath.Value = String.Empty
                            par_New.Value = True
                            par_Lock.Value = False
                            par_SortMethod.Value = Enums.SortMethod_MovieSet.Year
                            par_Language.Value = dbElement.Language

                            If Master.eSettings.MovieSetGeneralMarkNew Then
                                par_Mark.Value = True
                            Else
                                par_Mark.Value = False
                            End If
                            sqlCommand.ExecuteNonQuery()
                        End Using

                        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand.CommandText = String.Concat("SELECT idSet, SetName FROM sets WHERE SetName Like """, entry.Title, """;")
                            Using rdrSets As SQLiteDataReader = sqlCommand.ExecuteReader()
                                If rdrSets.Read Then
                                    entry.ID = Convert.ToInt64(rdrSets(0))
                                End If
                            End Using
                        End Using

                        'create new movieset_link entry with new idSet
                        If entry.ID > 0 Then
                            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                sqlCommand.CommandText = String.Concat("INSERT OR REPLACE INTO setlinkmovie (",
                                                                       "idSet,",
                                                                       "idMovie,",
                                                                       "iOrder",
                                                                       ") VALUES (?,?,?);")
                                Dim par_idSet As SQLiteParameter = sqlCommand.Parameters.Add("par_idSet", DbType.Int64, 0, "idSet")
                                Dim par_idMovie As SQLiteParameter = sqlCommand.Parameters.Add("par_idMovie", DbType.Int64, 0, "idMovie")
                                Dim par_iOrder As SQLiteParameter = sqlCommand.Parameters.Add("par_iOrder", DbType.Int64, 0, "iOrder")

                                par_idSet.Value = entry.ID
                                par_idMovie.Value = dbElement.ID
                                par_iOrder.Value = entry.Order
                                sqlCommand.ExecuteNonQuery()
                            End Using
                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub SetUniqueIDsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal uniqueids As MediaContainers.UniqueidContainer)
        RemoveUniqueIDsFromItem(idMedia, contentType)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As MediaContainers.Uniqueid In uniqueids.Items
                AddUniqueID(idMedia, mediaType, entry)
            Next
        End If
    End Sub


    '''''''''''''''''''''''''''''''''''''''''''
    'Protected Sub ConnectJobsDB()
    '    If _myvideosDBConn IsNot Nothing Then
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
            Using SQLCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLCommand.CommandText = String.Concat("SELECT Version FROM Addons WHERE AddonID = ", AddonID, ";")
                Dim tES As Object = SQLCommand.ExecuteScalar
                If tES IsNot Nothing Then
                    Dim tSing As Single = 0
                    If Single.TryParse(tES.ToString, tSing) Then
                        Return tSing
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
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
            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Using SQLCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLCommand.CommandText = String.Concat("SELECT FilePath FROM AddonFiles WHERE AddonID = ", AddonID, ";")
                    Using SQLReader As SQLiteDataReader = SQLCommand.ExecuteReader
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
            logger.Error(ex, New StackFrame().GetMethod().Name)
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
            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Using SQLCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLCommand.CommandText = String.Concat("INSERT OR REPLACE INTO Addons (",
                      "AddonID, Version) VALUES (?,?);")
                    Dim parAddonID As SQLiteParameter = SQLCommand.Parameters.Add("parAddonID", DbType.Int64, 0, "AddonID")
                    Dim parVersion As SQLiteParameter = SQLCommand.Parameters.Add("parVersion", DbType.String, 0, "Version")

                    parAddonID.Value = Addon.ID
                    parVersion.Value = Addon.Version.ToString

                    SQLCommand.ExecuteNonQuery()

                    SQLCommand.CommandText = String.Concat("DELETE FROM AddonFiles WHERE AddonID = ", Addon.ID, ";")
                    SQLCommand.ExecuteNonQuery()

                    Using SQLFileCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        SQLFileCommand.CommandText = String.Concat("INSERT INTO AddonFiles (AddonID, FilePath) VALUES (?,?);")
                        Dim parFileAddonID As SQLiteParameter = SQLFileCommand.Parameters.Add("parFileAddonID", DbType.Int64, 0, "AddonID")
                        Dim parFilePath As SQLiteParameter = SQLFileCommand.Parameters.Add("parFilePath", DbType.String, 0, "FilePath")
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
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Check if provided querystring is valid SQL
    ''' </summary>
    ''' <param name="Query">The SQL query to check</param>
    ''' <returns>true: valid query, false: invalid sql (check log!)</returns>
    ''' <remarks>    
    ''' cocotus 2015/03/07 Check for valid sql syntax, used for custom filter module
    ''' </remarks>
    Public Function IsValid_SQL(ByVal Query As String) As Boolean
        Try
            Using SQLcommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = Query
                Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                End Using
            End Using
            Return True
        Catch ex As Exception
            Dim response As String = String.Empty
            response = Master.eLang.GetString(1386, "Invalid SQL!") & Environment.NewLine & ex.Message
            MessageBox.Show(ex.Message, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
            Return False
        End Try
    End Function

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim currDBPath As String
        Dim currVersion As Integer
        Dim newDBPath As String
        Dim newVersion As Integer

#End Region 'Fields

    End Structure

    Public Class SQLViewProperty

#Region "Fields"

        Private _name As String
        Private _statement As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property
        Public Property Statement() As String
            Get
                Return _statement
            End Get
            Set(ByVal value As String)
                _statement = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _name = String.Empty
            _statement = String.Empty
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class DBElement
        Implements ICloneable

#Region "Fields"

        Private _islock As Boolean

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal type As Enums.ContentType)
            ContentType = type
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property ActorThumbs() As New List(Of String)

        Public ReadOnly Property ActorThumbsSpecified() As Boolean
            Get
                Return ActorThumbs.Count > 0
            End Get
        End Property

        Public ReadOnly Property ContentType() As Enums.ContentType

        Public Property Episodes() As New List(Of DBElement)

        Public ReadOnly Property EpisodesSpecified() As Boolean
            Get
                Return Episodes.Count > 0
            End Get
        End Property

        Public Property EpisodeSorting() As Enums.EpisodeSorting = Enums.EpisodeSorting.Episode

        Public Property ExtrafanartsPath() As String = String.Empty

        Public ReadOnly Property ExtrafanartsPathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(ExtrafanartsPath)
            End Get
        End Property

        Public Property ExtrathumbsPath() As String = String.Empty

        Public ReadOnly Property ExtrathumbsPathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(ExtrathumbsPath)
            End Get
        End Property

        Public Property Filename() As String = String.Empty

        Public ReadOnly Property FilenameSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Filename)
            End Get
        End Property

        Public Property FilenameID() As Long = -1

        Public ReadOnly Property FilenameIDSpecified() As Boolean
            Get
                Return Not FilenameID = -1
            End Get
        End Property

        Public Property ID() As Long = -1

        Public ReadOnly Property IDSpecified() As Boolean
            Get
                Return Not ID = -1
            End Get
        End Property

        Public Property ImagesContainer() As New MediaContainers.ImagesContainer

        Public Property IsLock() As Boolean
            Get
                Return _islock
            End Get
            Set(ByVal value As Boolean)
                _islock = value
                Select Case _contenttype
                    Case Enums.ContentType.Movie
                        If Movie IsNot Nothing Then Movie.Locked = value
                    Case Enums.ContentType.MovieSet
                        If MovieSet IsNot Nothing Then MovieSet.Locked = value
                    Case Enums.ContentType.TVEpisode
                        If TVEpisode IsNot Nothing Then TVEpisode.Locked = value
                    Case Enums.ContentType.TVSeason
                        If TVSeason IsNot Nothing Then TVSeason.Locked = value
                    Case Enums.ContentType.TVShow
                        If TVShow IsNot Nothing Then TVShow.Locked = value
                End Select
            End Set
        End Property

        Public Property IsMark() As Boolean = False

        Public Property IsMarkCustom1() As Boolean = False

        Public Property IsMarkCustom2() As Boolean = False

        Public Property IsMarkCustom3() As Boolean = False

        Public Property IsMarkCustom4() As Boolean = False

        Public Property IsOnline() As Boolean = False

        Public Property IsSingle() As Boolean = False

        Public Property Language() As String = String.Empty

        Public ReadOnly Property LanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Language)
            End Get
        End Property

        Public ReadOnly Property Language_Main() As String
            Get
                Return Regex.Replace(Language, "-.*", String.Empty).Trim
            End Get
        End Property

        Public Property ListTitle() As String = String.Empty

        Public ReadOnly Property ListTitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(ListTitle)
            End Get
        End Property

        Public Property Movie() As MediaContainers.Movie = Nothing

        Public ReadOnly Property MovieSpecified() As Boolean
            Get
                Return Movie IsNot Nothing
            End Get
        End Property

        Public Property MoviesInSet() As New List(Of MediaContainers.MovieInSet)

        Public ReadOnly Property MoviesInSetSpecified() As Boolean
            Get
                Return MoviesInSet.Count > 0
            End Get
        End Property

        Public Property MovieSet() As MediaContainers.Movieset = Nothing

        Public ReadOnly Property MovieSetSpecified() As Boolean
            Get
                Return MovieSet IsNot Nothing
            End Get
        End Property

        Public Property NfoPath() As String = String.Empty

        Public ReadOnly Property NfoPathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(NfoPath)
            End Get
        End Property

        Public Property Ordering() As Enums.EpisodeOrdering = Enums.EpisodeOrdering.Standard

        Public Property OutOfTolerance() As Boolean = False

        Public Property Seasons() As New List(Of DBElement)

        Public ReadOnly Property SeasonsSpecified() As Boolean
            Get
                Return Seasons.Count > 0
            End Get
        End Property

        Public Property ShowID() As Long = -1

        Public ReadOnly Property ShowIDSpecified() As Boolean
            Get
                Return Not ShowID = -1
            End Get
        End Property

        Public Property ShowPath() As String = String.Empty

        Public ReadOnly Property ShowPathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(ShowPath)
            End Get
        End Property

        Public Property SortMethod() As Enums.SortMethod_MovieSet = Enums.SortMethod_MovieSet.Year

        Public Property Source() As New DBSource

        Public ReadOnly Property SourceSpecified() As Boolean
            Get
                Return Not Source.ID = -1
            End Get
        End Property

        Public Property Subtitles() As New List(Of MediaContainers.Subtitle)

        Public ReadOnly Property SubtitlesSpecified() As Boolean
            Get
                Return Subtitles.Count > 0
            End Get
        End Property

        Public Property Theme() As New MediaContainers.MediaFile

        Public ReadOnly Property ThemeSpecified() As Boolean
            Get
                Return Theme.FileOriginal IsNot Nothing AndAlso Theme.FileOriginal.HasMemoryStream
            End Get
        End Property

        Public Property Trailer() As New MediaContainers.MediaFile

        Public ReadOnly Property TrailerSpecified() As Boolean
            Get
                Return Trailer.FileOriginal IsNot Nothing AndAlso Trailer.FileOriginal.HasMemoryStream
            End Get
        End Property

        Public Property TVEpisode() As MediaContainers.EpisodeDetails = Nothing

        Public ReadOnly Property TVEpisodeSpecified() As Boolean
            Get
                Return TVEpisode IsNot Nothing
            End Get
        End Property

        Public Property TVSeason() As MediaContainers.SeasonDetails = Nothing

        Public ReadOnly Property TVSeasonSpecified() As Boolean
            Get
                Return TVSeason IsNot Nothing
            End Get
        End Property

        Public Property TVShow() As MediaContainers.TVShow = Nothing

        Public ReadOnly Property TVShowSpecified() As Boolean
            Get
                Return TVShow IsNot Nothing
            End Get
        End Property

        Public Property VideoSource() As String = String.Empty

        Public ReadOnly Property VideoSourceSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(VideoSource)
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Function CloneDeep() As Object Implements ICloneable.Clone
            Dim Stream As New MemoryStream(50000)
            Dim Formatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            ' Serialisierung über alle Objekte hinweg in einen Stream 
            Formatter.Serialize(Stream, Me)
            ' Zurück zum Anfang des Streams und... 
            Stream.Seek(0, SeekOrigin.Begin)
            ' ...aus dem Stream in ein Objekt deserialisieren 
            CloneDeep = Formatter.Deserialize(Stream)
            Stream.Close()
        End Function

        Public Sub LoadAllImages(ByVal LoadBitmap As Boolean, ByVal withExtraImages As Boolean)
            ImagesContainer.LoadAllImages(ContentType, LoadBitmap, withExtraImages)
        End Sub

#End Region 'Methods

    End Class

    <Serializable()>
    Public Class DBSource

#Region "Properties"

        Public Property EpisodeSorting() As Enums.EpisodeSorting = Enums.EpisodeSorting.Episode

        Public Property Exclude() As Boolean = False

        Public Property GetYear() As Boolean = False

        Public Property ID() As Long = -1

        Public ReadOnly Property IDSpecified() As Boolean
            Get
                Return Not ID = -1
            End Get
        End Property

        Public Property IsSingle() As Boolean = False

        Public Property Language() As String = String.Empty

        Public ReadOnly Property LanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Language)
            End Get
        End Property

        Public Property LastScan() As String = String.Empty

        Public ReadOnly Property LastScanSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(LastScan)
            End Get
        End Property

        Public Property Name() As String = String.Empty

        Public ReadOnly Property NameSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Name)
            End Get
        End Property

        Public Property Ordering() As Enums.EpisodeOrdering = Enums.EpisodeOrdering.Standard

        Public Property Path() As String = String.Empty

        Public ReadOnly Property PathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Path)
            End Get
        End Property

        Public Property Recursive() As Boolean = False

        Public Property UseFolderName() As Boolean = False

#End Region 'Properties 

    End Class

#End Region 'Nested Types

End Class