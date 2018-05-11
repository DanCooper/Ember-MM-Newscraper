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
Imports System.Data.SQLite
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports System.Xml.Serialization

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

#End Region

#Region "Methods"

    Private Sub AddArt(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal artType As String, ByVal url As String, ByVal width As Integer, ByVal height As Integer)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Dim doesExist As Boolean = False
            Dim ID As Long = -1
            Dim oldURL As String = String.Empty

            Using SQLcommand_select_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand_select_art.CommandText = String.Format("SELECT idArt, url FROM art WHERE media_id={0} AND media_type='{1}' AND type='{2}'", idMedia, mediaType, artType)
                Using SQLreader As SQLiteDataReader = SQLcommand_select_art.ExecuteReader()
                    While SQLreader.Read
                        doesExist = True
                        ID = CInt(SQLreader("idArt"))
                        oldURL = SQLreader("url").ToString
                        Exit While
                    End While
                End Using
            End Using

            If Not doesExist Then
                Using SQLcommand_insert_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_insert_art.CommandText = String.Format("INSERT INTO art(media_id, media_type, type, url, width, height) VALUES ({0}, '{1}', '{2}', ?, {3}, {4})", idMedia, mediaType, artType, width, height)
                    Dim par_insert_art_url As SQLiteParameter = SQLcommand_insert_art.Parameters.Add("par_insert_art_url", DbType.String, 0, "url")
                    par_insert_art_url.Value = url
                    SQLcommand_insert_art.ExecuteNonQuery()
                End Using
            Else
                If Not url = oldURL Then
                    Using SQLcommand_update_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        SQLcommand_update_art.CommandText = String.Format("UPDATE art SET url=(?), width={0}, height={1} WHERE idArt={2}", width, height, ID)
                        Dim par_update_art_url As SQLiteParameter = SQLcommand_update_art.Parameters.Add("par_update_art_url", DbType.String, 0, "url")
                        par_update_art_url.Value = url
                        SQLcommand_update_art.ExecuteNonQuery()
                    End Using
                End If
            End If
        End If
    End Sub

    Private Function AddCertification(ByVal certification As String) As Long
        If String.IsNullOrEmpty(certification) Then Return -1
        Return AddToTable("certification", "idCertification", "name", certification)
    End Function

    Private Function AddCountry(ByVal country As String) As Long
        If String.IsNullOrEmpty(country) Then Return -1
        Return AddToTable("country", "idCountry", "name", country)
    End Function

    Private Function AddFileinfo(ByVal idFile As Long, ByVal path As String, ByVal filesize As Integer) As Long
        If String.IsNullOrEmpty(path) Then Return -1

        If idFile = -1 Then
            'search for an existing entry with same path
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = "SELECT idFile FROM file WHERE path = (?);"
                Dim par_path As SQLiteParameter = sqlCommand.Parameters.Add("par_path", DbType.String, 0, "path")
                par_path.Value = path
                Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader
                    If sqlReader.HasRows Then
                        sqlReader.Read()
                        idFile = CLng(sqlReader("idFile"))
                    End If
                End Using
            End Using
        End If

        If Not idFile = -1 Then
            'update the existing entry
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("UPDATE file SET path=(?), filesize=(?) WHERE idFile={0}", idFile)
                Dim par_path As SQLiteParameter = sqlCommand.Parameters.Add("par_path", DbType.String, 0, "path")
                Dim par_filesize As SQLiteParameter = sqlCommand.Parameters.Add("par_filesize", DbType.Int16, 0, "filesize")
                par_path.Value = path
                If filesize > 0 Then
                    par_filesize.Value = filesize
                Else
                    par_filesize.Value = Nothing 'need to be NOTHING instead of 0
                End If
                sqlCommand.ExecuteNonQuery()
                Return idFile
            End Using
        Else
            'create a new entry
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Concat("INSERT INTO file (path, filesize) VALUES (?,?); Select LAST_INSERT_ROWID() FROM file;")
                Dim par_path As SQLiteParameter = sqlCommand.Parameters.Add("parEpPath", DbType.String, 0, "path")
                Dim par_filesize As SQLiteParameter = sqlCommand.Parameters.Add("par_filesize", DbType.Int16, 0, "filesize")
                par_path.Value = path
                If filesize > 0 Then
                    par_filesize.Value = filesize
                Else
                    par_filesize.Value = Nothing 'need to be NOTHING instead of 0
                End If
                Return CLng(sqlCommand.ExecuteScalar)
            End Using
        End If
    End Function

    Private Function AddGenre(ByVal genre As String) As Long
        If String.IsNullOrEmpty(genre) Then Return -1
        Dim ID As Long = AddToTable("genre", "idGenre", "name", genre)
        LoadAllGenres()
        Return ID
    End Function
    ''' <summary>
    ''' add or update actor
    ''' </summary>
    ''' <param name="name">actor name</param>
    ''' <param name="thumbURL">thumb URL</param>
    ''' <param name="thumb">local thumb path</param>
    ''' <param name="imdbID">IMDB ID of actor</param>
    ''' <param name="tmdbID">TMDB ID of actor</param>
    ''' <param name="isActor"><c>True</c> if adding an actor, <c>False</c> if adding a Creator, Director, Writer or something else without ID's and images to refresh if already exist in actors table</param>
    ''' <returns><c>ID</c> of person in table person</returns>
    ''' <remarks></remarks>
    Private Function AddPerson(ByVal name As String, ByVal thumbURL As String, ByVal thumb As String, ByVal imdbID As String, ByVal tmdbID As String, ByVal isActor As Boolean) As Long
        Dim doesExist As Boolean = False
        Dim ID As Long = -1

        Using SQLcommand_select_actors As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_select_actors.CommandText = String.Format("SELECT idPerson FROM person WHERE name LIKE ?", name)
            Dim par_name As SQLiteParameter = SQLcommand_select_actors.Parameters.Add("par_name", DbType.String, 0, "name")
            par_name.Value = name
            Using SQLreader As SQLiteDataReader = SQLcommand_select_actors.ExecuteReader()
                While SQLreader.Read
                    doesExist = True
                    ID = CInt(SQLreader("idPerson"))
                    Exit While
                End While
            End Using
        End Using

        If Not doesExist Then
            Using SQLcommand_insert_actors As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand_insert_actors.CommandText = "INSERT INTO person (idPerson, name, thumb, strIMDB, strTMDB) VALUES (NULL,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM person;"
                Dim par_name As SQLiteParameter = SQLcommand_insert_actors.Parameters.Add("par_name", DbType.String, 0, "name")
                Dim par_thumb As SQLiteParameter = SQLcommand_insert_actors.Parameters.Add("par_thumb", DbType.String, 0, "thumb")
                Dim par_imdb As SQLiteParameter = SQLcommand_insert_actors.Parameters.Add("par_strIMDB", DbType.String, 0, "strIMDB")
                Dim par_tmdb As SQLiteParameter = SQLcommand_insert_actors.Parameters.Add("par_strTMDB", DbType.String, 0, "strTMDB")
                par_name.Value = name
                par_thumb.Value = thumbURL
                par_imdb.Value = imdbID
                par_tmdb.Value = tmdbID
                ID = CInt(SQLcommand_insert_actors.ExecuteScalar())
            End Using
        ElseIf isActor Then
            Using SQLcommand_update_actors As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand_update_actors.CommandText = String.Format("UPDATE person SET thumb=?, strIMDB=?, strTMDB=? WHERE idPerson={0}", ID)
                Dim par_thumb As SQLiteParameter = SQLcommand_update_actors.Parameters.Add("par_thumb", DbType.String, 0, "thumb")
                Dim par_update_actors_strIMDB As SQLiteParameter = SQLcommand_update_actors.Parameters.Add("par_imdb", DbType.String, 0, "strIMDB")
                Dim par_update_actors_strTMDB As SQLiteParameter = SQLcommand_update_actors.Parameters.Add("par_tmdb", DbType.String, 0, "strTMDB")
                par_thumb.Value = thumbURL
                par_update_actors_strIMDB.Value = imdbID
                par_update_actors_strTMDB.Value = tmdbID
                SQLcommand_update_actors.ExecuteNonQuery()
            End Using
        End If

        If Not ID = -1 Then
            If Not String.IsNullOrEmpty(thumb) Then
                AddArt(ID, Enums.ContentType.Person, "thumb", thumb, 0, 0)
            End If
        End If

        Return ID
    End Function

    Private Function AddSet(ByVal title As String) As Long
        If String.IsNullOrEmpty(title) Then Return -1
        Return AddToTable("set", "idSet", "name", title)
    End Function

    Private Function AddStudio(ByVal studio As String) As Long
        If String.IsNullOrEmpty(studio) Then Return -1
        Return AddToTable("studio", "idStudio", "name", studio)
    End Function

    Private Function AddTag(ByVal tag As String) As Long
        If String.IsNullOrEmpty(tag) Then Return -1
        Return AddToTable("tag", "idTag", "name", tag)
    End Function

    Private Sub AddToLinkTable(ByVal table As String,
                               ByVal firstField As String,
                               ByVal firstID As Long,
                               ByVal secondField As String,
                               ByVal secondID As Long,
                               ByVal typeField As String,
                               ByVal type As String)
        If Not firstID = -1 AndAlso Not secondID = -1 Then
            Using SQLcommand_select As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand_select.CommandText = String.Format("Select * FROM {0} WHERE {1}={2} And {3}={4}", table, firstField, firstID, secondField, secondID)
                If Not String.IsNullOrEmpty(typeField) AndAlso Not String.IsNullOrEmpty(type) Then
                    SQLcommand_select.CommandText = String.Concat(SQLcommand_select.CommandText, String.Format(" And {0}='{1}'", typeField, type))
                End If
                Using SQLreader As SQLiteDataReader = SQLcommand_select.ExecuteReader()
                    If SQLreader.HasRows Then Exit Sub
                End Using
            End Using

            'add a new entry
            Using SQLcommand_insert As SQLiteCommand = _myvideosDBConn.CreateCommand()
                If String.IsNullOrEmpty(typeField) AndAlso String.IsNullOrEmpty(type) Then
                    SQLcommand_insert.CommandText = String.Format("INSERT INTO {0} ({1},{2}) VALUES ({3},{4})",
                                                                  table,
                                                                  firstField,
                                                                  secondField,
                                                                  firstID,
                                                                  secondID)
                Else
                    SQLcommand_insert.CommandText = String.Format("INSERT INTO {0} ({1},{2},{3}) VALUES ({4},{5},'{6}')",
                                                                  table,
                                                                  firstField,
                                                                  secondField,
                                                                  typeField,
                                                                  firstID,
                                                                  secondID,
                                                                  type)
                End If
                SQLcommand_insert.ExecuteNonQuery()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="table"><cactor_link></c> or <c>gueststar_link</c></param>
    ''' <param name="idPerson">ID from table person</param>
    ''' <param name="idMedia">ID of media</param>
    ''' <param name="mediaType"></param>
    ''' <param name="role"></param>
    ''' <param name="order"></param>
    Private Sub AddToPersonLinkTable(ByVal table As String,
                               ByVal idPerson As Long,
                               ByVal idMedia As Long,
                               ByVal mediaType As String,
                               ByVal role As String,
                               ByVal order As Integer)
        If Not idPerson = -1 AndAlso Not idMedia = -1 Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("INSERT OR REPLACE INTO {0} (idPerson, idMedia, media_type, role, cast_order) VALUES ({1},'{2}',?,{3})", table, idPerson, mediaType, order)
                Dim par_role As SQLiteParameter = sqlCommand.Parameters.Add("par_role", DbType.String, 0, "role")
                par_role.Value = role
                sqlCommand.ExecuteNonQuery()
            End Using
        End If
    End Sub

    Private Function AddToTable(ByVal table As String,
                                ByVal firstField As String,
                                ByVal secondField As String,
                                ByVal value As String) As Long
        'search for an already existing entry
        Using SQLcommand_select As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_select.CommandText = String.Format("SELECT {0} FROM {1} WHERE {2} LIKE ?", firstField, table, secondField)
            Dim par_select_secondField As SQLiteParameter = SQLcommand_select.Parameters.Add("par_select_secondField", DbType.String, 0, secondField)
            par_select_secondField.Value = value
            Using SQLreader As SQLiteDataReader = SQLcommand_select.ExecuteReader()
                While SQLreader.Read
                    'use existing entry ID
                    Return CInt(SQLreader(firstField))
                    Exit While
                End While
            End Using
        End Using

        'add a new entry
        Using SQLcommand_insert As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_insert.CommandText = String.Format("INSERT INTO {0} ({1}, {2}) VALUES (NULL, ?); SELECT LAST_INSERT_ROWID() FROM {0};", table, firstField, secondField)
            Dim par_insert_secondField As SQLiteParameter = SQLcommand_insert.Parameters.Add("par_insert_secondField", DbType.String, 0, secondField)
            par_insert_secondField.Value = value
            Return CInt(SQLcommand_insert.ExecuteScalar())
        End Using
    End Function

    Private Function AddUniqueID(ByVal idMedia As Long,
                            ByVal mediaType As String,
                            ByVal uniqueID As MediaContainers.Uniqueid) As Long
        If Not idMedia = -1 AndAlso Not String.IsNullOrEmpty(mediaType) AndAlso uniqueID.TypeSpecified AndAlso uniqueID.ValueSpecified Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("INSERT OR REPLACE INTO uniqueid (idMedia, media_type, value, type, isDefault) VALUES ({0},'{1}',?,{2},{3}); SELECT LAST_INSERT_ROWID() FROM uniqueid;",
                                                       idMedia,
                                                       mediaType,
                                                       uniqueID.Type,
                                                       uniqueID.IsDefault)
                Dim par_value As SQLiteParameter = sqlCommand.Parameters.Add("par_value", DbType.String, 0, "value")
                par_value.Value = uniqueID.Value
                Return CLng(sqlCommand.ExecuteScalar())
            End Using
        End If
        Return -1
    End Function

    Private Function GetActorsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of MediaContainers.Person)
        Return GetFromPersonLinkTable("actor_link", idMedia, contentType)
    End Function

    Public Function GetArtForItem(ByVal idMedia As Long,
                                  ByVal contentType As Enums.ContentType,
                                  ByVal artType As String) As String
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("SELECT url FROM art WHERE idMedia={0} AND media_type='{1}' AND type='{2}'", idMedia, mediaType, artType)
                Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        Return SQLreader("url").ToString
                        Exit While
                    End While
                End Using
            End Using
        End If
        Return String.Empty
    End Function

    Private Function GetCertificationsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of String)
        Return GetFromLinkTable("certification_link", "idCertification", "certification", "idCertification", idMedia, contentType)
    End Function

    Private Function GetCountriesForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of String)
        Return GetFromLinkTable("country_link", "idCountry", "country", "idCountry", idMedia, contentType)
    End Function

    Private Function GetCreatorsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of String)
        Return GetFromLinkTable("creatorlink", "idPerson", "person", "idPerson", idMedia, contentType)
    End Function

    Private Function GetDirectorsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of String)
        Return GetFromLinkTable("director_link", "idPerson", "person", "idPerson", idMedia, contentType)
    End Function

    Private Function GetFileInfoForItem(ByVal idMedia As Long, ByVal idFile As Long) As MediaContainers.Fileinfo
        Return New MediaContainers.Fileinfo
    End Function

    Private Function GetGenresForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of String)
        Return GetFromLinkTable("genre_link", "idGenre", "genre", "idGenre", idMedia, contentType)
    End Function

    Private Function GetGuestStarsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of MediaContainers.Person)
        Return GetFromPersonLinkTable("gueststar_link", idMedia, contentType)
    End Function

    Private Function GetStudiosForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of String)
        Return GetFromLinkTable("studio_link", "idStudio", "studio", "idStudio", idMedia, contentType)
    End Function

    Private Function GetTagsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of String)
        Return GetFromLinkTable("tag_link", "idTag", "tag", "idTag", idMedia, contentType)
    End Function

    Private Function GetWritersForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of String)
        Return GetFromLinkTable("writer_link", "idPerson", "person", "idPerson", idMedia, contentType)
    End Function

    Private Function GetUniqueIDsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of MediaContainers.Uniqueid)
        Dim lstUniqueIDs As New List(Of MediaContainers.Uniqueid)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("SELECT * FROM uniqueid WHERE idMedia={0} AND media_type='{1}' ORDER BY isDefault=0", idMedia, mediaType)
                Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        lstUniqueIDs.Add(New MediaContainers.Uniqueid With {
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

    Private Function GetFromLinkTable(ByVal linkTable As String,
                                      ByVal linkField As String,
                                      ByVal mainTable As String,
                                      ByVal mainField As String,
                                      ByVal idMedia As Long,
                                      ByVal contentType As Enums.ContentType) As List(Of String)
        Dim lstResults As New List(Of String)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("SELECT B.name FROM {0} AS A INNER JOIN {1} AS B ON (A.{2} = B.{3}) WHERE A.idMedia = {4} AND A.media_type='{5}'",
                                                       linkTable, mainTable, linkField, mainField, idMedia, mediaType)
                Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        lstResults.Add(SQLreader("name").ToString)
                    End While
                End Using
            End Using
        End If
        Return lstResults
    End Function

    Private Function GetFromPersonLinkTable(ByVal linkTable As String,
                                            ByVal idMedia As Long,
                                            ByVal contentType As Enums.ContentType) As List(Of MediaContainers.Person)
        Dim lstResults As New List(Of MediaContainers.Person)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("SELECT * FROM {0} AS a INNER JOIN person AS b ON (a.idPerson=b.idPerson) LEFT OUTER JOIN art AS c ON (b.idPerson=c.idMedia AND c.media_type='person' AND c.type='thumb') WHERE a.idMedia={1} AND a.media_type='{2}' ORDER BY a.cast_order;",
                                                       linkTable, idMedia, mediaType)
                Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        lstResults.Add(New MediaContainers.Person With {
                                       .ID = CLng(SQLreader("idPerson")),
                                       .Name = SQLreader("name").ToString,
                                       .Order = CInt(SQLreader("cast_order")),
                                       .Role = SQLreader("role").ToString})
                    End While
                End Using
            End Using
        End If
        Return lstResults
    End Function

    ''' <summary>
    ''' Iterates db entries to check if the paths to the movie or TV files are valid. 
    ''' If not, remove all entries pertaining to the movie.
    ''' </summary>
    ''' <param name="cleanMovies">If <c>True</c>, process the movie files</param>
    ''' <param name="cleanTVShows">If <c>True</c>, process the TV files</param>
    ''' <param name="idSource">Optional. If provided, only process entries from that source.</param>
    ''' <remarks></remarks>
    Public Sub Clean(ByVal cleanMovies As Boolean, ByVal cleanMovieSets As Boolean, ByVal cleanTVShows As Boolean, Optional ByVal idSource As Long = -1)
        Dim fInfo As FileInfo
        Dim tPath As String = String.Empty
        Dim sPath As String = String.Empty

        logger.Info("Cleaning videodatabase started")

        Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
            If cleanMovies Then
                logger.Info("Cleaning movies started")
                Dim MoviePaths As List(Of String) = GetAllMoviePaths()
                MoviePaths.Sort()

                'get a listing of sources and their recursive properties
                Dim SourceList As New List(Of DBSource)
                Dim tSource As DBSource

                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    If idSource = -1 Then
                        SQLcommand.CommandText = "SELECT * FROM moviesource;"
                    Else
                        SQLcommand.CommandText = String.Format("SELECT * FROM moviesource WHERE idSource = {0}", idSource)
                    End If
                    Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                        While SQLreader.Read
                            SourceList.Add(Load_Source_Movie(Convert.ToInt64(SQLreader("idSource"))))
                        End While
                    End Using
                End Using

                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    If idSource = -1 Then
                        SQLcommand.CommandText = "SELECT MoviePath, idMovie, idSource, Type FROM movie ORDER BY MoviePath DESC;"
                    Else
                        SQLcommand.CommandText = String.Format("SELECT MoviePath, idMovie, idSource, Type FROM movie WHERE idSource = {0} ORDER BY MoviePath DESC;", idSource)
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
                                    If Not tSource.ScanRecursive AndAlso tPath.Length > tSource.Path.Length AndAlso If(sPath = "video_ts" OrElse sPath = "bdmv", tPath.Substring(tSource.Path.Length).Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar).Count > 2, tPath.Substring(tSource.Path.Length).Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar).Count > 1) Then
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

            If cleanMovieSets Then
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

            If cleanTVShows Then
                logger.Info("Cleaning tv shows started")
                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    If idSource = -1 Then
                        SQLcommand.CommandText = "SELECT files.strFilename, episode.idEpisode FROM files INNER JOIN episode ON (files.idFile = episode.idFile) ORDER BY files.strFilename;"
                    Else
                        SQLcommand.CommandText = String.Format("SELECT files.strFilename, episode.idEpisode FROM files INNER JOIN episode ON (files.idFile = episode.idFile) WHERE episode.idSource = {0} ORDER BY files.strFilename;", idSource)
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

    Public Sub Cleanup_Genres()
        logger.Info("[Database] [Cleanup_Genres] Started")
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
                    If StringUtils.GenreFilter(tmpDBElement.Movie.Genres, False) Then
                        Save_Movie(tmpDBElement, True, True, False, True, False)
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
                    If StringUtils.GenreFilter(tmpDBElement.TVShow.Genres, False) Then
                        Save_TVShow(tmpDBElement, True, True, False, False)
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
        logger.Info("[Database] [Cleanup_Genres] Done")
    End Sub
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
            Case Enums.ContentType.Person
                Return "person"
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
    ''' <param name="idShow">Show ID</param>
    ''' <param name="batchMode">If <c>False</c>, the action is wrapped in a transaction</param>
    ''' <remarks></remarks>
    Private Sub Delete_Empty_TVSeasons(ByVal idShow As Long, ByVal batchMode As Boolean)
        Dim SQLtransaction As SQLiteTransaction = Nothing

        If Not batchMode Then SQLtransaction = Master.DB.MyVideosDBConn.BeginTransaction()
        Using SQLCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not idShow = -1 Then
                SQLCommand.CommandText = String.Format("DELETE FROM seasons WHERE seasons.idShow = {0} AND NOT EXISTS (SELECT episode.Season FROM episode WHERE episode.Season = seasons.Season AND episode.idShow = seasons.idShow) AND seasons.Season <> -1", idShow)
            Else
                SQLCommand.CommandText = String.Format("DELETE FROM seasons WHERE NOT EXISTS (SELECT episode.Season FROM episode WHERE episode.Season = seasons.Season AND episode.idShow = seasons.idShow) AND seasons.Season <> -1")
            End If
            SQLCommand.ExecuteNonQuery()
        End Using
        If Not batchMode Then SQLtransaction.Commit()
        SQLtransaction = Nothing

        If SQLtransaction IsNot Nothing Then SQLtransaction.Dispose()
    End Sub
    ''' <summary>
    ''' Remove all TV Episodes they are no longer valid (not in <c>ValidEpisodes</c> list)
    ''' </summary>
    ''' <param name="batchMode">If <c>False</c>, the action is wrapped in a transaction</param>
    ''' <remarks></remarks>
    Private Sub Delete_Invalid_TVEpisodes(ByVal validEpisodes As List(Of DBElement), ByVal idShow As Long, ByVal batchMode As Boolean)
        Dim SQLtransaction As SQLiteTransaction = Nothing

        If Not batchMode Then SQLtransaction = Master.DB.MyVideosDBConn.BeginTransaction()
        Using SQLCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLCommand.CommandText = String.Format("SELECT idEpisode FROM episode WHERE idShow = {0};", idShow)
            Using SQLreader As SQLiteDataReader = SQLCommand.ExecuteReader()
                While SQLreader.Read
                    If validEpisodes.Where(Function(f) f.ID = Convert.ToInt64(SQLreader("idEpisode"))).Count = 0 Then
                        Delete_TVEpisode(Convert.ToInt64(SQLreader("idEpisode")), True, False, True)
                    End If
                End While
            End Using
        End Using
        If Not batchMode Then SQLtransaction.Commit()
        SQLtransaction = Nothing

        If SQLtransaction IsNot Nothing Then SQLtransaction.Dispose()
    End Sub
    ''' <summary>
    ''' Remove all TV Seasons they are no longer valid (not in <c>ValidSeasons</c> list)
    ''' </summary>
    ''' <param name="batchMode">If <c>False</c>, the action is wrapped in a transaction</param>
    ''' <remarks></remarks>
    Private Sub Delete_Invalid_TVSeasons(ByVal validSeasons As List(Of DBElement), ByVal idShow As Long, ByVal batchMode As Boolean)
        Dim SQLtransaction As SQLiteTransaction = Nothing

        If Not batchMode Then SQLtransaction = Master.DB.MyVideosDBConn.BeginTransaction()
        Using SQLCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLCommand.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0};", idShow)
            Using SQLreader As SQLiteDataReader = SQLCommand.ExecuteReader()
                While SQLreader.Read
                    If validSeasons.Where(Function(f) f.ID = Convert.ToInt64(SQLreader("idSeason"))).Count = 0 Then
                        Delete_TVSeason(Convert.ToInt64(SQLreader("idSeason")), True)
                    End If
                End While
            End Using
        End Using
        If Not batchMode Then SQLtransaction.Commit()
        SQLtransaction = Nothing

        If SQLtransaction IsNot Nothing Then SQLtransaction.Dispose()
    End Sub

    ''' <summary>
    ''' Remove all information related to a movie from the database.
    ''' </summary>
    ''' <param name="idMovie">ID of the movie to remove, as stored in the database.</param>
    ''' <param name="batchMode">Is this function already part of a transaction?</param>
    Public Sub Delete_Movie(ByVal idMovie As Long, ByVal batchMode As Boolean)
        If idMovie < 0 Then Throw New ArgumentOutOfRangeException("idMovie", "Value must be >= 0, was given: " & idMovie)

        Dim _movieDB As DBElement = Load_Movie(idMovie)
        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Remove_Movie, Nothing, Nothing, False, _movieDB)

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not batchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("DELETE FROM movie WHERE idMovie = ", idMovie, ";")
            SQLcommand.ExecuteNonQuery()
        End Using
        If Not batchMode Then SQLtransaction.Commit()

        RaiseEvent GenericEvent(Enums.ModuleEventType.Remove_Movie, New List(Of Object)(New Object() {_movieDB.ID}))
    End Sub
    ''' <summary>
    ''' Remove all information related to a movieset from the database.
    ''' </summary>
    ''' <param name="idMovieset">ID of the movieset to remove, as stored in the database.</param>
    ''' <param name="batchMode">Is this function already part of a transaction?</param>
    Public Sub Delete_MovieSet(ByVal idMovieset As Long, ByVal batchMode As Boolean)
        'first get a list of all movies in the movieset to remove the movieset information from NFO
        Dim moviesToSave As New List(Of DBElement)

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not batchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT idMovie FROM setlinkmovie ",
                                                   "WHERE idSet = ", idMovieset, ";")
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
                movie.Movie.RemoveSet(idMovieset)
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, Nothing, False, movie)
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_Movie, Nothing, Nothing, False, movie)
                Save_Movie(movie, batchMode, True, False, True, False)
                RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {movie.ID}))
            Next
        End If

        'delete all movieset images and if this setting is enabled
        If Master.eSettings.MovieSetCleanFiles Then
            Dim MovieSet As DBElement = Master.DB.Load_MovieSet(idMovieset)
            Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainBanner)
            Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainClearArt)
            Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainClearLogo)
            Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainDiscArt)
            Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainFanart)
            Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainLandscape)
            Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainPoster)
        End If

        'remove the movieset and still existing setlinkmovie entries
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("DELETE FROM sets WHERE idSet = ", idMovieset, ";")
            SQLcommand.ExecuteNonQuery()
        End Using
        If Not batchMode Then SQLtransaction.Commit()
    End Sub

    ''' <summary>
    ''' Remove all information related to a tag from the database.
    ''' </summary>
    ''' <param name="idTag">Internal TagID of the tag to remove, as stored in the database.</param>
    ''' <param name="mode">1=tag of a movie, 2=tag of a show</param>
    ''' <param name="batchMode">Is this function already part of a transaction?</param>
    Public Sub Delete_Tag(ByVal idTag As Long, ByVal mode As Integer, ByVal batchMode As Boolean)
        'first get a list of all movies in the tag to remove the tag information from NFO
        Dim moviesToSave As New List(Of DBElement)
        Dim SQLtransaction As SQLiteTransaction = Nothing
        Dim strName As String = String.Empty
        If Not batchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT name FROM tag ",
                                                   "WHERE idTag = ", idTag, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("name")) Then strName = CStr(SQLreader("name"))
                End While
            End Using
        End Using

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT idMedia FROM tag_link ",
                                                   "WHERE idTag = ", idTag, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If mode = 1 Then
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
                movie.Movie.Tags.Remove(strName)
                Save_Movie(movie, batchMode, True, False, True, False)
            Next
        End If

        'remove the tag entry
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("DELETE FROM tag WHERE idTag = ", idTag, ";")
            SQLcommand.ExecuteNonQuery()
        End Using
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("DELETE FROM tag_link WHERE idTag = ", idTag, ";")
            SQLcommand.ExecuteNonQuery()
        End Using
        If Not batchMode Then SQLtransaction.Commit()
    End Sub

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
        Try
            sqlDA.Fill(dTable)
        Catch ex As Exception
            logger.Error(String.Format("Get error: ""{0}"" with SQLCommand: ""{1}""", ex.Message, Command))
        End Try
    End Sub
    ''' <summary>
    ''' Adds TVShow informations to a Database.DBElement
    ''' </summary>
    ''' <param name="_TVDB">Database.DBElement container to fill with TVShow informations</param>
    ''' <param name="_TVDBShow">Optional the TVShow informations to add to _TVDB</param>
    ''' <remarks></remarks>
    Public Function AddTVShowInfoToDBElement(ByVal _TVDB As DBElement, Optional ByVal _TVDBShow As DBElement = Nothing) As DBElement
        Dim _tmpTVDBShow As DBElement

        If _TVDBShow Is Nothing OrElse _TVDBShow.TVShow Is Nothing Then
            _tmpTVDBShow = Load_TVShow(_TVDB.ShowID, False, False)
        Else
            _tmpTVDBShow = _TVDBShow
        End If

        _TVDB.EpisodeSorting = _tmpTVDBShow.EpisodeSorting
        _TVDB.Ordering = _tmpTVDBShow.Ordering
        _TVDB.Language = _tmpTVDBShow.Language
        _TVDB.ShowID = _tmpTVDBShow.ShowID
        _TVDB.ShowPath = _tmpTVDBShow.ShowPath
        _TVDB.Source = _tmpTVDBShow.Source
        _TVDB.TVShow = _tmpTVDBShow.TVShow
        Return _TVDB
    End Function

    Public Function GetAllTags() As String()
        Dim nList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT name FROM tag ORDER BY name;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("name").ToString.Trim)
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

    Public Function GetAllCountries() As String()
        Dim nList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT name FROM country ORDER BY name;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("name").ToString)
                End While
            End Using
        End Using
        Return nList.ToArray
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

    Public Function GetAllVideoSources_Movie() As String()
        Dim nList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT DISTINCT videoSource FROM movie WHERE videoSource IS NOT '' ORDER BY videoSource;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("videoSource").ToString)
                End While
            End Using
        End Using
        Return nList.ToArray
    End Function

    Public Function GetAllYears_Movie() As String()
        Dim nList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT DISTINCT year FROM movie WHERE year IS NOT '' ORDER BY year;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("year").ToString)
                End While
            End Using
        End Using
        Return nList.ToArray
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

    Public Sub LoadAllGenres()
        Dim gList As New List(Of String)

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT name FROM genre ORDER BY name;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    gList.Add(SQLreader("name").ToString)
                End While
            End Using
        End Using

        For Each tGenre As String In gList
            Dim gMapping As genreMapping = APIXML.GenreXML.Mappings.FirstOrDefault(Function(f) f.SearchString = tGenre)
            If gMapping Is Nothing Then
                'check if the tGenre is already existing in Gernes list
                Dim gProperty As genreProperty = APIXML.GenreXML.Genres.FirstOrDefault(Function(f) f.Name = tGenre)
                If gProperty Is Nothing Then
                    APIXML.GenreXML.Genres.Add(New genreProperty With {.isNew = False, .Name = tGenre})
                End If
                'add a new mapping if tGenre is not in the MappingTable
                APIXML.GenreXML.Mappings.Add(New genreMapping With {.isNew = False, .MappedTo = New List(Of String) From {tGenre}, .SearchString = tGenre})
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
            SQLcommand.CommandText = "SELECT * FROM moviesource ORDER BY name;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    lstSources.Add(New DBSource With {
                                   .ID = Convert.ToInt64(SQLreader("idSource")),
                                   .Exclude = Convert.ToBoolean(SQLreader("exclude")),
                                   .GetYear = Convert.ToBoolean(SQLreader("getYear")),
                                   .IsSingle = Convert.ToBoolean(SQLreader("isSingle")),
                                   .Language = SQLreader("language").ToString,
                                   .LastScan = SQLreader("lastScan").ToString,
                                   .Name = SQLreader("name").ToString,
                                   .Path = SQLreader("path").ToString,
                                   .ScanRecursive = Convert.ToBoolean(SQLreader("scanRecursive")),
                                   .UseFolderName = Convert.ToBoolean(SQLreader("UseFolderName"))
                                   })
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
            SQLcommand.CommandText = "SELECT * FROM tvshowsource ORDER BY name;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    lstSources.Add(New DBSource With {
                                   .EpisodeOrdering = DirectCast(Convert.ToInt32(SQLreader("episodeOrdering")), Enums.EpisodeOrdering),
                                   .EpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("episodeSorting")), Enums.EpisodeSorting),
                                   .Exclude = Convert.ToBoolean(SQLreader("exclude")),
                                   .ID = Convert.ToInt64(SQLreader("idSource")),
                                   .IsSingle = Convert.ToBoolean(SQLreader("isSingle")),
                                   .Language = SQLreader("language").ToString,
                                   .LastScan = SQLreader("lastScan").ToString,
                                   .Name = SQLreader("name").ToString,
                                   .Path = SQLreader("path").ToString
                                   })
                End While
            End Using
        End Using
        Return lstSources
    End Function

    Public Function GetTVSeasonIDFromEpisode(ByVal dbElement As DBElement) As Long
        Dim sID As Long = -1
        If dbElement.TVEpisode IsNot Nothing Then
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0} AND Season = {1};", dbElement.ShowID, dbElement.TVEpisode.Season)
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

    Public Function GetTVSeasonIDFromShowIDAndSeasonNumber(ByVal idTVShow As Long, ByVal seasonnumber As Integer) As Long
        Dim sID As Long = -1
        If idTVShow > -1 AndAlso seasonnumber > -1 Then
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0} AND Season = {1};", idTVShow, seasonnumber)
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

    Public Function DeleteView(ByVal viewName As String) As Boolean
        If String.IsNullOrEmpty(viewName) Then Return False
        If viewName = "episodelist" OrElse
            viewName = "movielist" OrElse
            viewName = "moviesetlist" OrElse
            viewName = "seasonlist" OrElse
            viewName = "tvshowlist" Then
            logger.Error("It's not allowed to delete a default list")
            Return False
        End If

        Try
            Dim SQLtransaction As SQLiteTransaction = Nothing
            SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DROP VIEW IF EXISTS """, viewName, """;")
                SQLcommand.ExecuteNonQuery()
            End Using
            SQLtransaction.Commit()
            Return True
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            Return False
        End Try
    End Function

    Public Function GetViewDetails(ByVal viewName As String) As SQLViewProperty
        Dim ViewProperty As New SQLViewProperty
        If Not String.IsNullOrEmpty(viewName) Then
            Try
                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand.CommandText = String.Format("SELECT name, sql FROM sqlite_master WHERE type ='view' AND name='{0}';", viewName)
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

    Public Function ViewExists(ByVal viewName As String) As Boolean
        If Not String.IsNullOrEmpty(viewName) Then
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("SELECT name FROM sqlite_master WHERE type ='view' AND name = '{0}';", viewName)
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

    Public Function GetViewMediaCount(ByVal viewName As String, Optional episodesByView As Boolean = False) As Integer
        Dim iCount As Integer
        If Not String.IsNullOrEmpty(viewName) Then
            Try
                If Not episodesByView Then
                    Using SQLCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand.CommandText = String.Format("SELECT COUNT(*) FROM '{0}'", viewName)
                        iCount = Convert.ToInt32(SQLCommand.ExecuteScalar)
                        Return iCount
                    End Using
                Else
                    Using SQLCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand.CommandText = String.Format("SELECT COUNT(*) FROM '{0}' INNER JOIN episode ON ('{0}'.idShow = episode.idShow) WHERE NOT episode.idFile = -1", viewName)
                        iCount = Convert.ToInt32(SQLCommand.ExecuteScalar)
                        Return iCount
                    End Using
                End If
            Catch ex As Exception
                logger.Error(String.Format("SQL error in ""{0}"": {1}", viewName, ex.Message))
                Return -1
            End Try
        Else
            Return iCount
        End If
    End Function

    Public Function GetViewList(ByVal Type As Enums.ContentType, ByVal onlyCustomLists As Boolean) As List(Of String)
        Dim ViewList As New List(Of String)
        Dim ContentType As String = String.Empty

        Select Case Type
            Case Enums.ContentType.TVEpisode
                ContentType = "episode-"
            Case Enums.ContentType.Movie
                ContentType = "movie-"
            Case Enums.ContentType.MovieSet
                ContentType = "movieset-"
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

            If onlyCustomLists Then
                'remove default lists
                If ViewList.Contains("movielist") Then ViewList.Remove("movielist")
                If ViewList.Contains("moviesetlist") Then ViewList.Remove("moviesetlist")
                If ViewList.Contains("tvshowlist") Then ViewList.Remove("tvshowlist")
            End If

            'remove default lists that are currently not supported to add or customize
            If ViewList.Contains("episodelist") Then ViewList.Remove("episodelist")
            If ViewList.Contains("seasonslist") Then ViewList.Remove("seasonslist")
        End If

        ViewList.Sort()
        Return ViewList
    End Function

    Public Function GetMovies() As List(Of DBElement)
        Dim lstDBELement As New List(Of DBElement)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT idMovie FROM movie ORDER BY listTitle;"
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

    Public Function GetTVShows(ByVal withSeasons As Boolean, ByVal withEpisodes As Boolean, Optional ByVal withMissingEpisodes As Boolean = False) As List(Of DBElement)
        Dim lstDBELement As New List(Of DBElement)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT idShow FROM tvshow;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        lstDBELement.Add(Master.DB.Load_TVShow(Convert.ToInt64(SQLreader("idShow")), withSeasons, withEpisodes, withMissingEpisodes))
                    End While
                End If
            End Using
        End Using
        Return lstDBELement
    End Function
    ''' <summary>
    ''' Load all the information for a movie.
    ''' </summary>
    ''' <param name="idMovie">ID of the movie to load, as stored in the database</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_Movie(ByVal idMovie As Long) As DBElement
        Dim dbElement As New DBElement(Enums.ContentType.Movie)

        dbElement.ID = idMovie
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT * FROM movie WHERE idMovie={0};", dbElement.ID)
            Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                If sqlReader.HasRows Then
                    sqlReader.Read()
                    If Not DBNull.Value.Equals(sqlReader("dateAdded")) Then dbElement.DateAdded = Convert.ToInt64(sqlReader("dateAdded"))
                    If Not DBNull.Value.Equals(sqlReader("dateModified")) Then dbElement.DateModified = Convert.ToInt64(sqlReader("dateModified"))
                    If Not DBNull.Value.Equals(sqlReader("listTitle")) Then dbElement.ListTitle = sqlReader("listTitle").ToString
                    If Not DBNull.Value.Equals(sqlReader("isSingle")) Then dbElement.IsSingle = Convert.ToBoolean(sqlReader("isSingle"))
                    If Not DBNull.Value.Equals(sqlReader("trailerPath")) Then dbElement.Trailer.LocalFilePath = sqlReader("trailerPath").ToString
                    If Not DBNull.Value.Equals(sqlReader("nfoPath")) Then dbElement.NfoPath = sqlReader("nfoPath").ToString
                    If Not DBNull.Value.Equals(sqlReader("ethumbsPath")) Then dbElement.ExtrathumbsPath = sqlReader("ethumbsPath").ToString
                    If Not DBNull.Value.Equals(sqlReader("efanartsPath")) Then dbElement.ExtrafanartsPath = sqlReader("efanartsPath").ToString
                    If Not DBNull.Value.Equals(sqlReader("themePath")) Then dbElement.Theme.LocalFilePath = sqlReader("themePath").ToString

                    dbElement.Source = Load_Source_Movie(Convert.ToInt64(sqlReader("idSource")))

                    dbElement.IsMark = Convert.ToBoolean(sqlReader("marked"))
                    dbElement.IsLock = Convert.ToBoolean(sqlReader("locked"))
                    dbElement.OutOfTolerance = Convert.ToBoolean(sqlReader("outOfTolerance"))
                    dbElement.IsMarkCustom1 = Convert.ToBoolean(sqlReader("markCustom1"))
                    dbElement.IsMarkCustom2 = Convert.ToBoolean(sqlReader("markCustom2"))
                    dbElement.IsMarkCustom3 = Convert.ToBoolean(sqlReader("markCustom3"))
                    dbElement.IsMarkCustom4 = Convert.ToBoolean(sqlReader("markCustom4"))
                    If Not DBNull.Value.Equals(sqlReader("videoSource")) Then dbElement.VideoSource = sqlReader("videoSource").ToString
                    If Not DBNull.Value.Equals(sqlReader("language")) Then dbElement.Language = sqlReader("language").ToString

                    With dbElement.Movie
                        If Not DBNull.Value.Equals(sqlReader("dateAdded")) Then .DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(sqlReader("dateAdded"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(sqlReader("dateModified")) Then .DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(sqlReader("dateModified"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(sqlReader("title")) Then .Title = sqlReader("title").ToString
                        If Not DBNull.Value.Equals(sqlReader("originalTitle")) Then .OriginalTitle = sqlReader("originalTitle").ToString
                        If Not DBNull.Value.Equals(sqlReader("sortTitle")) Then .SortTitle = sqlReader("sortTitle").ToString
                        If Not DBNull.Value.Equals(sqlReader("year")) Then .Year = sqlReader("year").ToString
                        If Not DBNull.Value.Equals(sqlReader("mpaa")) Then .MPAA = sqlReader("mpaa").ToString
                        If Not DBNull.Value.Equals(sqlReader("top250")) Then .Top250 = Convert.ToInt32(sqlReader("top250"))
                        If Not DBNull.Value.Equals(sqlReader("outline")) Then .Outline = sqlReader("outline").ToString
                        If Not DBNull.Value.Equals(sqlReader("plot")) Then .Plot = sqlReader("plot").ToString
                        If Not DBNull.Value.Equals(sqlReader("tagline")) Then .Tagline = sqlReader("tagline").ToString
                        If Not DBNull.Value.Equals(sqlReader("trailer")) Then .Trailer = sqlReader("trailer").ToString
                        If Not DBNull.Value.Equals(sqlReader("runtime")) Then .Runtime = sqlReader("runtime").ToString
                        If Not DBNull.Value.Equals(sqlReader("releaseDate")) Then .ReleaseDate = sqlReader("releaseDate").ToString
                        If Not DBNull.Value.Equals(sqlReader("playCount")) Then .PlayCount = Convert.ToInt32(sqlReader("playCount"))
                        If Not DBNull.Value.Equals(sqlReader("videoSource")) Then .VideoSource = sqlReader("videoSource").ToString
                        If Not DBNull.Value.Equals(sqlReader("lastPlayed")) Then .LastPlayed = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(sqlReader("lastPlayed"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(sqlReader("language")) Then .Language = sqlReader("language").ToString
                        If Not DBNull.Value.Equals(sqlReader("userRating")) Then .UserRating = Convert.ToInt32(sqlReader("userRating"))
                    End With
                End If
            End Using
        End Using

        'Actors
        dbElement.Movie.Actors = GetActorsForItem(dbElement.ID, dbElement.ContentType)

        'Certifications
        dbElement.Movie.Certifications = GetCertificationsForItem(dbElement.ID, dbElement.ContentType)

        'Countries
        dbElement.Movie.Countries = GetCountriesForItem(dbElement.ID, dbElement.ContentType)

        'Credits
        dbElement.Movie.Credits = GetWritersForItem(dbElement.ID, dbElement.ContentType)

        'Directors
        dbElement.Movie.Directors = GetDirectorsForItem(dbElement.ID, dbElement.ContentType)

        'FileInfo
        dbElement.Movie.FileInfo = GetFileInfoForItem(dbElement.ID, dbElement.ContentType)

        'Genres
        dbElement.Movie.Genres = GetGenresForItem(dbElement.ID, dbElement.ContentType)

        'Video streams
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM MoviesVStreams WHERE MovieID = ", dbElement.ID, ";")
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
                    dbElement.Movie.FileInfo.StreamDetails.Video.Add(video)
                End While
            End Using
        End Using

        'Audio streams
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM MoviesAStreams WHERE MovieID = ", dbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim audio As MediaContainers.Audio
                While SQLreader.Read
                    audio = New MediaContainers.Audio
                    If Not DBNull.Value.Equals(SQLreader("Audio_Language")) Then audio.Language = SQLreader("Audio_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_LongLanguage")) Then audio.LongLanguage = SQLreader("Audio_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Codec")) Then audio.Codec = SQLreader("Audio_Codec").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Channel")) Then audio.Channels = SQLreader("Audio_Channel").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Bitrate")) Then audio.Bitrate = SQLreader("Audio_Bitrate").ToString
                    dbElement.Movie.FileInfo.StreamDetails.Audio.Add(audio)
                End While
            End Using
        End Using

        'embedded subtitles
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM MoviesSubs WHERE MovieID = ", dbElement.ID, " AND NOT Subs_Type = 'External';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaContainers.Subtitle
                While SQLreader.Read
                    subtitle = New MediaContainers.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.SubsType = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.SubsPath = SQLreader("Subs_Path").ToString
                    subtitle.SubsForced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    dbElement.Movie.FileInfo.StreamDetails.Subtitle.Add(subtitle)
                End While
            End Using
        End Using

        'external subtitles
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM MoviesSubs WHERE MovieID = ", dbElement.ID, " AND Subs_Type = 'External';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaContainers.Subtitle
                While SQLreader.Read
                    subtitle = New MediaContainers.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.SubsType = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.SubsPath = SQLreader("Subs_Path").ToString
                    subtitle.SubsForced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    dbElement.Subtitles.Add(subtitle)
                End While
            End Using
        End Using

        'Moviesets
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            'TODO: add missing uniqueid from movieset
            SQLcommand.CommandText = String.Concat("SELECT A.idMovie, A.idSet, A.'order', B.idSet, B.plot, B.title FROM movieset_link ",
                                                   "AS A INNER JOIN movieset AS B ON (A.idSet = B.idSet) WHERE A.idMovie = ", dbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Dim tSet As New MediaContainers.SetDetails
                    If Not DBNull.Value.Equals(SQLreader("idSet")) Then tSet.ID = Convert.ToInt64(SQLreader("idSet"))
                    If Not DBNull.Value.Equals(SQLreader("order")) Then tSet.Order = CInt(SQLreader("order"))
                    If Not DBNull.Value.Equals(SQLreader("plot")) Then tSet.Plot = SQLreader("plot").ToString
                    If Not DBNull.Value.Equals(SQLreader("title")) Then tSet.Title = SQLreader("title").ToString
                    'If Not DBNull.Value.Equals(SQLreader("TMDBColID")) Then tSet.TMDB = SQLreader("TMDBColID").ToString
                    dbElement.Movie.Sets.Add(tSet)
                End While
            End Using
        End Using

        'Studios
        dbElement.Movie.Studios = GetStudiosForItem(dbElement.ID, dbElement.ContentType)

        'Tags
        dbElement.Movie.Tags = GetTagsForItem(dbElement.ID, dbElement.ContentType)

        'UniqueIDs
        dbElement.Movie.UniqueIDs = GetUniqueIDsForItem(dbElement.ID, dbElement.ContentType)

        'ImagesContainer
        dbElement.ImagesContainer.Banner.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "banner")
        dbElement.ImagesContainer.ClearArt.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "clearart")
        dbElement.ImagesContainer.ClearLogo.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "clearlogo")
        dbElement.ImagesContainer.DiscArt.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "discart")
        dbElement.ImagesContainer.Fanart.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "fanart")
        dbElement.ImagesContainer.Landscape.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "landscape")
        dbElement.ImagesContainer.Poster.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "poster")
        If Not String.IsNullOrEmpty(dbElement.ExtrafanartsPath) AndAlso Directory.Exists(dbElement.ExtrafanartsPath) Then
            For Each ePath As String In Directory.GetFiles(dbElement.ExtrafanartsPath, "*.jpg")
                dbElement.ImagesContainer.Extrafanarts.Add(New MediaContainers.Image With {.LocalFilePath = ePath})
            Next
        End If
        If Not String.IsNullOrEmpty(dbElement.ExtrathumbsPath) AndAlso Directory.Exists(dbElement.ExtrathumbsPath) Then
            Dim iIndex As Integer = 0
            For Each ePath As String In Directory.GetFiles(dbElement.ExtrathumbsPath, "thumb*.jpg")
                dbElement.ImagesContainer.Extrathumbs.Add(New MediaContainers.Image With {.Index = iIndex, .LocalFilePath = ePath})
                iIndex += 1
            Next
        End If

        'Check if the file is available and ready to edit
        If File.Exists(dbElement.Filename) Then dbElement.IsOnline = True

        Return dbElement
    End Function
    ''' <summary>
    ''' Load all the information for a movieset.
    ''' </summary>
    ''' <param name="MovieSetID">ID of the movieset to load, as stored in the database</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_MovieSet(ByVal MovieSetID As Long) As DBElement
        Dim _moviesetDB As New DBElement(Enums.ContentType.MovieSet)

        _moviesetDB.ID = MovieSetID
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM movieset WHERE idSet = ", MovieSetID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("listTitle")) Then _moviesetDB.ListTitle = SQLreader("listTitle").ToString
                    If Not DBNull.Value.Equals(SQLreader("nfoPath")) Then _moviesetDB.NfoPath = SQLreader("nfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("language")) Then _moviesetDB.Language = SQLreader("language").ToString

                    _moviesetDB.IsMark = Convert.ToBoolean(SQLreader("marked"))
                    _moviesetDB.IsLock = Convert.ToBoolean(SQLreader("locked"))
                    _moviesetDB.SortMethod = DirectCast(Convert.ToInt32(SQLreader("sortMethod")), Enums.SortMethod_MovieSet)

                    With _moviesetDB.MovieSet
                        If Not DBNull.Value.Equals(SQLreader("plot")) Then .Plot = SQLreader("plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("title")) Then .Title = SQLreader("title").ToString
                        If Not DBNull.Value.Equals(SQLreader("language")) Then .Language = SQLreader("language").ToString
                        .OldTitle = .Title
                    End With
                End If
            End Using
        End Using

        'Movies in Set
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not Master.eSettings.MovieScraperCollectionsYAMJCompatibleSets Then
                If _moviesetDB.SortMethod = Enums.SortMethod_MovieSet.Year Then
                    SQLcommand.CommandText = String.Concat("SELECT movieset_link.idMovie, movieset_link.order FROM movieset_link INNER JOIN movie ON (movieset_link.idMovie = movie.idMovie) ",
                                                           "WHERE idSet = ", _moviesetDB.ID, " ORDER BY movie.year;")
                ElseIf _moviesetDB.SortMethod = Enums.SortMethod_MovieSet.Title Then
                    SQLcommand.CommandText = String.Concat("SELECT movieset_link.idMovie, movieset_link.order FROM movieset_link INNER JOIN movielist ON (movieset_link.idMovie = movielist.idMovie) ",
                                                           "WHERE idSet = ", _moviesetDB.ID, " ORDER BY movielist.sortedTitle COLLATE NOCASE;")
                End If
            Else
                SQLcommand.CommandText = String.Concat("SELECT movieset_link.idMovie, movieset_link.order FROM movieset_link ",
                                                       "WHERE idSet = ", _moviesetDB.ID, " ORDER BY order;")
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
        _moviesetDB.ImagesContainer.Banner.LocalFilePath = GetArtForItem(_moviesetDB.ID, _moviesetDB.ContentType, "banner")
        _moviesetDB.ImagesContainer.ClearArt.LocalFilePath = GetArtForItem(_moviesetDB.ID, _moviesetDB.ContentType, "clearart")
        _moviesetDB.ImagesContainer.ClearLogo.LocalFilePath = GetArtForItem(_moviesetDB.ID, _moviesetDB.ContentType, "clearlogo")
        _moviesetDB.ImagesContainer.DiscArt.LocalFilePath = GetArtForItem(_moviesetDB.ID, _moviesetDB.ContentType, "discart")
        _moviesetDB.ImagesContainer.Fanart.LocalFilePath = GetArtForItem(_moviesetDB.ID, _moviesetDB.ContentType, "fanart")
        _moviesetDB.ImagesContainer.Landscape.LocalFilePath = GetArtForItem(_moviesetDB.ID, _moviesetDB.ContentType, "landscape")
        _moviesetDB.ImagesContainer.Poster.LocalFilePath = GetArtForItem(_moviesetDB.ID, _moviesetDB.ContentType, "poster")

        Return _moviesetDB
    End Function

    Public Function Load_Source_Movie(ByVal idSource As Long) As DBSource
        Dim nDBSource As New DBSource

        nDBSource.ID = idSource
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM moviesource WHERE idSource = ", nDBSource.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    nDBSource.Path = SQLreader("path").ToString
                    nDBSource.Name = SQLreader("name").ToString
                    nDBSource.ScanRecursive = Convert.ToBoolean(SQLreader("scanRecursive"))
                    nDBSource.UseFolderName = Convert.ToBoolean(SQLreader("useFoldername"))
                    nDBSource.IsSingle = Convert.ToBoolean(SQLreader("isSingle"))
                    nDBSource.Exclude = Convert.ToBoolean(SQLreader("exclude"))
                    nDBSource.GetYear = Convert.ToBoolean(SQLreader("getYear"))
                    nDBSource.Language = SQLreader("language").ToString
                    nDBSource.LastScan = SQLreader("lastScan").ToString
                End If
            End Using
        End Using

        Return nDBSource
    End Function

    Public Function Load_Source_TVShow(ByVal idSource As Long) As DBSource
        Dim nDBSource As New DBSource

        nDBSource.ID = idSource
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM tvshowsource WHERE idSource = ", nDBSource.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    nDBSource.Path = SQLreader("path").ToString
                    nDBSource.Name = SQLreader("name").ToString
                    nDBSource.Language = SQLreader("language").ToString
                    nDBSource.EpisodeOrdering = DirectCast(Convert.ToInt32(SQLreader("ordering")), Enums.EpisodeOrdering)
                    nDBSource.Exclude = Convert.ToBoolean(SQLreader("exclude"))
                    nDBSource.EpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("episodeSorting")), Enums.EpisodeSorting)
                    nDBSource.LastScan = SQLreader("lastScan").ToString
                    nDBSource.IsSingle = Convert.ToBoolean(SQLreader("isSingle"))
                End If
            End Using
        End Using

        Return nDBSource
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
                    If Not DBNull.Value.Equals(SQLreader("name")) Then _tagDB.Title = SQLreader("name").ToString
                    If Not DBNull.Value.Equals(SQLreader("idTag")) Then _tagDB.ID = CInt(SQLreader("idTag"))
                End If
            End Using
        End Using

        _tagDB.Movies = New List(Of DBElement)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM tag_link ",
                        "WHERE idTag = ", _tagDB.ID, " AND media_type = 'movie';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    _tagDB.Movies.Add(Load_Movie(Convert.ToInt64(SQLreader("idMedia"))))
                End While
            End Using
        End Using
        Return _tagDB
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
                    If Not DBNull.Value.Equals(SQLReader("strTMDB")) Then nSeason.TMDB = CStr(SQLReader("strTMDB"))
                    If Not DBNull.Value.Equals(SQLReader("strTVDB")) Then nSeason.TVDB = CStr(SQLReader("strTVDB"))
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
        Dim dbElement As New DBElement(Enums.ContentType.TVEpisode)
        Dim PathID As Long = -1

        dbElement.ID = EpisodeID
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM episode WHERE idEpisode = ", EpisodeID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("NfoPath")) Then dbElement.NfoPath = SQLreader("NfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("idShow")) Then dbElement.ShowID = Convert.ToInt64(SQLreader("idShow"))
                    If Not DBNull.Value.Equals(SQLreader("DateAdded")) Then dbElement.DateAdded = Convert.ToInt64(SQLreader("DateAdded"))
                    If Not DBNull.Value.Equals(SQLreader("VideoSource")) Then dbElement.VideoSource = SQLreader("VideoSource").ToString
                    PathID = Convert.ToInt64(SQLreader("idFile"))

                    dbElement.Source = Load_Source_TVShow(Convert.ToInt64(SQLreader("idSource")))

                    dbElement.FilenameID = PathID
                    dbElement.IsMark = Convert.ToBoolean(SQLreader("Mark"))
                    dbElement.IsLock = Convert.ToBoolean(SQLreader("Lock"))
                    dbElement.ShowID = Convert.ToInt64(SQLreader("idShow"))
                    dbElement.ShowPath = Load_Path_TVShow(Convert.ToInt64(SQLreader("idShow")))

                    With dbElement.TVEpisode
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
                        If Not DBNull.Value.Equals(SQLreader("strIMDB")) Then .IMDB = SQLreader("strIMDB").ToString
                        If Not DBNull.Value.Equals(SQLreader("strTMDB")) Then .TMDB = SQLreader("strTMDB").ToString
                        If Not DBNull.Value.Equals(SQLreader("strTVDB")) Then .TVDB = SQLreader("strTVDB").ToString
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
                    If Not DBNull.Value.Equals(SQLreader("strFilename")) Then dbElement.Filename = SQLreader("strFilename").ToString
                End If
            End Using
        End Using

        'Actors
        dbElement.TVEpisode.Actors = GetActorsForItem(dbElement.ID, dbElement.ContentType)

        'Credits
        dbElement.TVEpisode.Credits = GetWritersForItem(dbElement.ID, dbElement.ContentType)

        'Directors
        dbElement.TVEpisode.Directors = GetDirectorsForItem(dbElement.ID, dbElement.ContentType)

        'FileInfo
        dbElement.TVEpisode.FileInfo = GetFileInfoForItem(dbElement.ID, dbElement.ContentType)

        'Guest Stars
        dbElement.TVEpisode.GuestStars = GetGuestStarsForItem(dbElement.ID, dbElement.ContentType)

        'Video Streams
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM TVVStreams WHERE TVEpID = ", dbElement.ID, ";")
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
                    dbElement.TVEpisode.FileInfo.StreamDetails.Video.Add(video)
                End While
            End Using
        End Using

        'Audio Streams
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM TVAStreams WHERE TVEpID = ", dbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim audio As MediaContainers.Audio
                While SQLreader.Read
                    audio = New MediaContainers.Audio
                    If Not DBNull.Value.Equals(SQLreader("Audio_Language")) Then audio.Language = SQLreader("Audio_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_LongLanguage")) Then audio.LongLanguage = SQLreader("Audio_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Codec")) Then audio.Codec = SQLreader("Audio_Codec").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Channel")) Then audio.Channels = SQLreader("Audio_Channel").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Bitrate")) Then audio.Bitrate = SQLreader("Audio_Bitrate").ToString
                    dbElement.TVEpisode.FileInfo.StreamDetails.Audio.Add(audio)
                End While
            End Using
        End Using

        'embedded subtitles
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM TVSubs WHERE TVEpID = ", dbElement.ID, " AND NOT Subs_Type = 'External';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaContainers.Subtitle
                While SQLreader.Read
                    subtitle = New MediaContainers.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.SubsType = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.SubsPath = SQLreader("Subs_Path").ToString
                    subtitle.SubsForced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    dbElement.TVEpisode.FileInfo.StreamDetails.Subtitle.Add(subtitle)
                End While
            End Using
        End Using

        'external subtitles
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM TVSubs WHERE TVEpID = ", dbElement.ID, " AND Subs_Type = 'External';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaContainers.Subtitle
                While SQLreader.Read
                    subtitle = New MediaContainers.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.SubsType = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.SubsPath = SQLreader("Subs_Path").ToString
                    subtitle.SubsForced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    dbElement.Subtitles.Add(subtitle)
                End While
            End Using
        End Using

        'UniqueIDs
        dbElement.TVEpisode.UniqueIDs = GetUniqueIDsForItem(dbElement.ID, dbElement.ContentType)

        'ImagesContainer
        dbElement.ImagesContainer.Fanart.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "fanart")
        dbElement.ImagesContainer.Poster.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "thumb")

        'Show container
        If withShow Then
            dbElement = Master.DB.AddTVShowInfoToDBElement(dbElement)
        End If

        'Check if the file is available and ready to edit
        If File.Exists(dbElement.Filename) Then dbElement.IsOnline = True

        Return dbElement
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
        Dim _TVDB As New DBElement(Enums.ContentType.TVSeason)

        _TVDB.ID = SeasonID
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
                        If Not DBNull.Value.Equals(SQLReader("strTMDB")) Then .TMDB = CStr(SQLReader("strTMDB"))
                        If Not DBNull.Value.Equals(SQLReader("strTVDB")) Then .TVDB = CStr(SQLReader("strTVDB"))
                        If Not DBNull.Value.Equals(SQLReader("SeasonText")) Then .Title = CStr(SQLReader("SeasonText"))
                    End With
                End If
            End Using
        End Using

        'ImagesContainer
        _TVDB.ImagesContainer.Banner.LocalFilePath = GetArtForItem(_TVDB.ID, _TVDB.ContentType, "banner")
        _TVDB.ImagesContainer.Fanart.LocalFilePath = GetArtForItem(_TVDB.ID, _TVDB.ContentType, "fanart")
        _TVDB.ImagesContainer.Landscape.LocalFilePath = GetArtForItem(_TVDB.ID, _TVDB.ContentType, "landscape")
        _TVDB.ImagesContainer.Poster.LocalFilePath = GetArtForItem(_TVDB.ID, _TVDB.ContentType, "poster")

        'Show container
        If withShow Then
            _TVDB = Master.DB.AddTVShowInfoToDBElement(_TVDB)
        End If

        'Episodes
        If withEpisodes Then
            For Each tEpisode As DBElement In Load_AllTVEpisodes(_TVDB.ShowID, withShow, _TVDB.TVSeason.Season)
                tEpisode = AddTVShowInfoToDBElement(tEpisode, _TVDB)
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
        Dim _TVDB As New DBElement(Enums.ContentType.TVSeason)

        If ShowID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & ShowID)

        _TVDB.ShowID = ShowID
        If withShow Then AddTVShowInfoToDBElement(_TVDB)

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
        Dim dbElement As New DBElement(Enums.ContentType.TVShow)
        dbElement.TVShow = New MediaContainers.TVShow

        If ShowID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & ShowID)

        dbElement.ID = ShowID
        dbElement.ShowID = ShowID
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM tvshow WHERE idShow = ", ShowID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("ListTitle")) Then dbElement.ListTitle = SQLreader("ListTitle").ToString
                    If Not DBNull.Value.Equals(SQLreader("EFanartsPath")) Then dbElement.ExtrafanartsPath = SQLreader("EFanartsPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("Language")) Then dbElement.Language = SQLreader("Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("NfoPath")) Then dbElement.NfoPath = SQLreader("NfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("TVShowPath")) Then dbElement.ShowPath = SQLreader("TVShowPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("ThemePath")) Then dbElement.Theme.LocalFilePath = SQLreader("ThemePath").ToString

                    dbElement.Source = Load_Source_TVShow(Convert.ToInt64(SQLreader("idSource")))

                    dbElement.IsMark = Convert.ToBoolean(SQLreader("Mark"))
                    dbElement.IsLock = Convert.ToBoolean(SQLreader("Lock"))
                    dbElement.Ordering = DirectCast(Convert.ToInt32(SQLreader("Ordering")), Enums.EpisodeOrdering)
                    dbElement.EpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("EpisodeSorting")), Enums.EpisodeSorting)

                    With dbElement.TVShow
                        If Not DBNull.Value.Equals(SQLreader("Title")) Then .Title = SQLreader("Title").ToString
                        If Not DBNull.Value.Equals(SQLreader("TVDB")) Then .TVDB = SQLreader("TVDB").ToString
                        If Not DBNull.Value.Equals(SQLreader("EpisodeGuide")) Then .EpisodeGuide.URL = SQLreader("EpisodeGuide").ToString
                        If Not DBNull.Value.Equals(SQLreader("Plot")) Then .Plot = SQLreader("Plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("Premiered")) Then .Premiered = SQLreader("Premiered").ToString
                        If Not DBNull.Value.Equals(SQLreader("MPAA")) Then .MPAA = SQLreader("MPAA").ToString
                        If Not DBNull.Value.Equals(SQLreader("Rating")) Then .Rating = SQLreader("Rating").ToString
                        If Not DBNull.Value.Equals(SQLreader("Status")) Then .Status = SQLreader("Status").ToString
                        If Not DBNull.Value.Equals(SQLreader("Runtime")) Then .Runtime = SQLreader("Runtime").ToString
                        If Not DBNull.Value.Equals(SQLreader("Votes")) Then .Votes = SQLreader("Votes").ToString
                        If Not DBNull.Value.Equals(SQLreader("SortTitle")) Then .SortTitle = SQLreader("SortTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("strIMDB")) Then .IMDB = SQLreader("strIMDB").ToString
                        If Not DBNull.Value.Equals(SQLreader("strTMDB")) Then .TMDB = SQLreader("strTMDB").ToString
                        If Not DBNull.Value.Equals(SQLreader("Language")) Then .Language = SQLreader("Language").ToString
                        If Not DBNull.Value.Equals(SQLreader("strOriginalTitle")) Then .OriginalTitle = SQLreader("strOriginalTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("iUserRating")) Then .UserRating = Convert.ToInt32(SQLreader("iUserRating"))
                    End With
                End If
            End Using
        End Using

        'Actors
        dbElement.TVShow.Actors = GetActorsForItem(dbElement.ID, dbElement.ContentType)
        'Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
        '    SQLcommand.CommandText = String.Concat("SELECT A.strRole, B.idActor, B.strActor, B.strThumb, C.url FROM actor_link AS A ",
        '                                           "INNER JOIN actors AS B ON (A.idActor = B.idActor) ",
        '                                           "LEFT OUTER JOIN art AS C ON (B.idActor = C.media_id AND C.media_type = 'actor' AND C.type = 'thumb') ",
        '                                           "WHERE A.idShow = ", _TVDB.ID, " ",
        '                                           "ORDER BY A.iOrder;")
        '    Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
        '        Dim actor As MediaContainers.Person
        '        While SQLreader.Read
        '            actor = New MediaContainers.Person
        '            actor.ID = Convert.ToInt64(SQLreader("idActor"))
        '            actor.Name = SQLreader("strActor").ToString
        '            actor.Role = SQLreader("strRole").ToString
        '            actor.LocalFilePath = SQLreader("url").ToString
        '            actor.URLOriginal = SQLreader("strThumb").ToString
        '            _TVDB.TVShow.Actors.Add(actor)
        '        End While
        '    End Using
        'End Using

        'Certifications
        dbElement.TVShow.Certifications = GetCertificationsForItem(dbElement.ID, dbElement.ContentType)

        'Countries
        dbElement.TVShow.Countries = GetCountriesForItem(dbElement.ID, dbElement.ContentType)

        'Creators
        dbElement.TVShow.Creators = GetCreatorsForItem(dbElement.ID, dbElement.ContentType)

        'Genres
        dbElement.TVShow.Genres = GetGenresForItem(dbElement.ID, dbElement.ContentType)

        'Studios
        dbElement.TVShow.Studios = GetStudiosForItem(dbElement.ID, dbElement.ContentType)

        'Tags
        dbElement.TVShow.Tags = GetTagsForItem(dbElement.ID, dbElement.ContentType)

        'UniqueIDs
        dbElement.TVShow.UniqueIDs = GetUniqueIDsForItem(dbElement.ID, dbElement.ContentType)

        'ImagesContainer
        dbElement.ImagesContainer.Banner.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "banner")
        dbElement.ImagesContainer.CharacterArt.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "characterart")
        dbElement.ImagesContainer.ClearArt.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "clearart")
        dbElement.ImagesContainer.ClearLogo.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "clearlogo")
        dbElement.ImagesContainer.Fanart.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "fanart")
        dbElement.ImagesContainer.Landscape.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "landscape")
        dbElement.ImagesContainer.Poster.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "poster")
        If Not String.IsNullOrEmpty(dbElement.ExtrafanartsPath) AndAlso Directory.Exists(dbElement.ExtrafanartsPath) Then
            For Each ePath As String In Directory.GetFiles(dbElement.ExtrafanartsPath, "*.jpg")
                dbElement.ImagesContainer.Extrafanarts.Add(New MediaContainers.Image With {.LocalFilePath = ePath})
            Next
        End If

        'Seasons
        If withSeasons Then
            For Each tSeason As DBElement In Load_AllTVSeasons(dbElement.ID)
                tSeason = AddTVShowInfoToDBElement(tSeason, dbElement)
                dbElement.Seasons.Add(tSeason)
                dbElement.TVShow.Seasons.Seasons.Add(tSeason.TVSeason)
            Next
            '_TVDB.TVShow.Seasons = LoadAllTVSeasonsDetailsFromDB(_TVDB.ID)
        End If

        'Episodes
        If withEpisodes Then
            For Each tEpisode As DBElement In Load_AllTVEpisodes(dbElement.ID, False, -1, withMissingEpisodes)
                tEpisode = AddTVShowInfoToDBElement(tEpisode, dbElement)
                dbElement.Episodes.Add(tEpisode)
            Next
        End If

        'Check if the path is available and ready to edit
        If Directory.Exists(dbElement.ShowPath) Then dbElement.IsOnline = True

        Return dbElement
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

    Private Sub RemoveActorsFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("actor_link", idMedia, contentType)
    End Sub

    Private Sub RemoveArtFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("art", idMedia, contentType)
    End Sub

    Private Sub RemoveCertificationsFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("certification_link", idMedia, contentType)
    End Sub

    Private Sub RemoveCountriesFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("country_link", idMedia, contentType)
    End Sub

    Private Sub RemoveCreatorsFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("creator_link", idMedia, contentType)
    End Sub

    Private Sub RemoveDirectrorsFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("director_link", idMedia, contentType)
    End Sub

    Private Sub RemoveGenresFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("genre_link", idMedia, contentType)
    End Sub

    Private Sub RemoveGuestStarsFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("gueststar_link", idMedia, contentType)
    End Sub

    Private Sub RemoveStudiosFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("studio_link", idMedia, contentType)
    End Sub

    Private Sub RemoveTagsFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("tag_link", idMedia, contentType)
    End Sub

    Private Sub RemoveWritersFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("writer_link", idMedia, contentType)
    End Sub

    Private Sub RemoveUniqueIDsFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("uniqueid", idMedia, contentType)
    End Sub

    Private Sub RemoveFromTable(ByVal table As String, ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("DELETE FROM {0} WHERE idMedia={1} AND media_type={2};", table, idMedia, mediaType)
                SQLcommand.ExecuteNonQuery()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Saves all information from a Database.DBElement object to the database
    ''' </summary>
    ''' <param name="dbElement">Media.Movie object to save to the database</param>
    ''' <param name="bBatchMode">Is the function already part of a transaction?</param>
    ''' <param name="bToNFO">Save informations to NFO</param>
    ''' <param name="bToDisk">Save Images, Themes and Trailers to disk</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Save_Movie(ByVal dbElement As DBElement, ByVal bBatchMode As Boolean, ByVal bToNFO As Boolean, ByVal bToDisk As Boolean, ByVal bDoSync As Boolean, ByVal bForceFileCleanup As Boolean) As DBElement
        If dbElement.Movie Is Nothing Then Return dbElement

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not bBatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLcommand_movie As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not dbElement.IDSpecified Then
                SQLcommand_movie.CommandText = String.Concat("INSERT OR REPLACE INTO movie (",
                 "idSource, idFile, isSingle, listTitle, hasSub, new, marked, locked, ",
                 "title, originalTitle, sortTitle, year, mpaa, top250, outline, plot, tagline, ",
                 "runtime, releaseDate, playcount, trailer, ",
                 "nfoPath, trailerPath, subPath, ethumbsPath, outOfTolerance, videoSource, ",
                 "dateAdded, efanartsPath, themePath, ",
                 "dateModified, markCustom1, markCustom2, markCustom3, markCustom4, hasSet, lastPlayed, language, userRating",
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM movie;")
            Else
                SQLcommand_movie.CommandText = String.Concat("INSERT OR REPLACE INTO movie (",
                 "idMovie, idSource, idFile, isSingle, listTitle, hasSub, new, marked, lock, ",
                 "title, originalTitle, sortTitle, year, mpaa, top250, outline, plot, tagline, ",
                 "Runtime, ReleaseDate, Playcount, Trailer, ",
                 "nfoPath, trailerPath, subPath, ethumbsPath, outOfTolerance, videoSource, ",
                 "dateAdded, efanartsPath, themePath, ",
                 "dateModified, markCustom1, markCustom2, markCustom3, markCustom4, hasSet, lastPlayed, language, userRating",
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM movie;")
                Dim parMovieID As SQLiteParameter = SQLcommand_movie.Parameters.Add("paridMovie", DbType.Int64, 0, "idMovie")
                parMovieID.Value = dbElement.ID
            End If
            Dim par_idSource As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_idSource", DbType.Int64, 0, "idSource")
            Dim par_idFile As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
            Dim par_isSingle As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_isSingle", DbType.Boolean, 0, "isSingle")
            Dim par_listTitle As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_listTitle", DbType.String, 0, "listTitle")
            Dim par_hasSub As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_hasSub", DbType.Boolean, 0, "hasSub")
            Dim par_new As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_new", DbType.Boolean, 0, "new")
            Dim par_marked As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_marked", DbType.Boolean, 0, "marked")
            Dim par_locked As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_locked", DbType.Boolean, 0, "locked")
            Dim par_title As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_title", DbType.String, 0, "title")
            Dim par_originalTitle As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_originalTitle", DbType.String, 0, "originalTitle")
            Dim par_sortTitle As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_sortTitle", DbType.String, 0, "sortTitle")
            Dim par_year As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_year", DbType.String, 0, "year")
            Dim par_mpaa As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_mpaa", DbType.String, 0, "mpaa")
            Dim par_top250 As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_top250", DbType.Int64, 0, "top250")
            Dim par_outline As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_outline", DbType.String, 0, "outline")
            Dim par_plot As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_plot", DbType.String, 0, "plot")
            Dim par_tagline As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_tagline", DbType.String, 0, "tagline")
            Dim par_runtime As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_runtime", DbType.String, 0, "runtime")
            Dim par_releaseDate As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_releaseDate", DbType.String, 0, "releaseDate")
            Dim par_playcount As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_playcount", DbType.Int64, 0, "playcount")
            Dim par_trailer As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_trailer", DbType.String, 0, "trailer")
            Dim par_nfoPath As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_nfoPath", DbType.String, 0, "nfoPath")
            Dim par_trailerPath As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_trailerPath", DbType.String, 0, "trailerPath")
            Dim par_subPath As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_subPath", DbType.String, 0, "subPath")
            Dim par_ethumbsPath As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_ethumbsPath", DbType.String, 0, "ethumbsPath")
            Dim par_outOfTolerance As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_outOfTolerance", DbType.Boolean, 0, "outOfTolerance")
            Dim par_videoSource As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_videoSource", DbType.String, 0, "videoSource")
            Dim par_dateAdded As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_dateAdded", DbType.Int64, 0, "dateAdded")
            Dim par_efanartsPath As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_efanartsPath", DbType.String, 0, "efanartsPath")
            Dim par_themePath As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_themePath", DbType.String, 0, "themePath")
            Dim par_dateModified As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_dateModified", DbType.Int64, 0, "dateModified")
            Dim par_markCustom1 As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_markCustom1", DbType.Boolean, 0, "markCustom1")
            Dim par_markCustom2 As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_markCustom2", DbType.Boolean, 0, "markCustom2")
            Dim par_markCustom3 As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_markCustom3", DbType.Boolean, 0, "markCustom3")
            Dim par_markCustom4 As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_markCustom4", DbType.Boolean, 0, "markCustom4")
            Dim par_hasSet As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_hasSet", DbType.Boolean, 0, "hasSet")
            Dim par_lastPlayed As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_lastPlayed", DbType.Int64, 0, "lastPlayed")
            Dim par_language As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_language", DbType.String, 0, "language")
            Dim par_userRating As SQLiteParameter = SQLcommand_movie.Parameters.Add("par_userRating", DbType.Int64, 0, "userRating")

            Try
                If Not Master.eSettings.GeneralDateAddedIgnoreNFO AndAlso Not String.IsNullOrEmpty(dbElement.Movie.DateAdded) Then
                    Dim DateTimeAdded As Date = Date.ParseExact(dbElement.Movie.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    par_dateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    Select Case Master.eSettings.GeneralDateTime
                        Case Enums.DateTime.Now
                            par_dateAdded.Value = If(Not dbElement.IDSpecified, Functions.ConvertToUnixTimestamp(Date.Now), dbElement.DateAdded)
                        Case Enums.DateTime.ctime
                            Dim ctime As Date = File.GetCreationTime(dbElement.Filename)
                            If ctime.Year > 1601 Then
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            Else
                                Dim mtime As Date = File.GetLastWriteTime(dbElement.Filename)
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            End If
                        Case Enums.DateTime.mtime
                            Dim mtime As Date = File.GetLastWriteTime(dbElement.Filename)
                            If mtime.Year > 1601 Then
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                Dim ctime As Date = File.GetCreationTime(dbElement.Filename)
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                        Case Enums.DateTime.Newer
                            Dim mtime As Date = File.GetLastWriteTime(dbElement.Filename)
                            Dim ctime As Date = File.GetCreationTime(dbElement.Filename)
                            If mtime > ctime Then
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                    End Select
                End If
                dbElement.Movie.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_dateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            Catch
                par_dateAdded.Value = If(Not dbElement.IDSpecified, Functions.ConvertToUnixTimestamp(Date.Now), dbElement.DateAdded)
                dbElement.Movie.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_dateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            End Try

            Try
                If Not dbElement.IDSpecified AndAlso dbElement.Movie.DateModifiedSpecified Then
                    Dim DateTimeDateModified As Date = Date.ParseExact(dbElement.Movie.DateModified, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    par_dateModified.Value = Functions.ConvertToUnixTimestamp(DateTimeDateModified)
                ElseIf dbElement.IDSpecified Then
                    par_dateModified.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                End If
                If par_dateModified.Value IsNot Nothing Then
                    dbElement.Movie.DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_dateModified.Value)).ToString("yyyy-MM-dd HH:mm:ss")
                Else
                    dbElement.Movie.DateModified = String.Empty
                End If
            Catch
                par_dateModified.Value = If(Not dbElement.IDSpecified, Functions.ConvertToUnixTimestamp(Date.Now), dbElement.DateModified)
                dbElement.Movie.DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_dateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            End Try

            Dim DateTimeLastPlayedUnix As Double = -1
            If dbElement.Movie.LastPlayedSpecified Then
                Try
                    Dim DateTimeLastPlayed As Date = Date.ParseExact(dbElement.Movie.LastPlayed, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    DateTimeLastPlayedUnix = Functions.ConvertToUnixTimestamp(DateTimeLastPlayed)
                Catch
                    'Kodi save it only as yyyy-MM-dd, try that
                    Try
                        Dim DateTimeLastPlayed As Date = Date.ParseExact(dbElement.Movie.LastPlayed, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture)
                        DateTimeLastPlayedUnix = Functions.ConvertToUnixTimestamp(DateTimeLastPlayed)
                    Catch
                        DateTimeLastPlayedUnix = -1
                    End Try
                End Try
            End If
            If DateTimeLastPlayedUnix >= 0 Then
                par_lastPlayed.Value = DateTimeLastPlayedUnix
            Else
                par_lastPlayed.Value = Nothing 'need to be NOTHING instead of 0
                dbElement.Movie.LastPlayed = String.Empty
            End If

            'Trailer URL
            If Master.eSettings.MovieScraperXBMCTrailerFormat Then
                dbElement.Movie.Trailer = dbElement.Movie.Trailer.Trim.Replace("http://www.youtube.com/watch?v=", "plugin://plugin.video.youtube/?action=play_video&videoid=")
                dbElement.Movie.Trailer = dbElement.Movie.Trailer.Replace("http://www.youtube.com/watch?hd=1&v=", "plugin://plugin.video.youtube/?action=play_video&videoid=")
            End If

            'First let's save it to NFO, even because we will need the NFO path
            'Also save Images to get ExtrafanartsPath and ExtrathumbsPath
            'art Table will be linked later
            If bToNFO Then NFO.SaveToNFO_Movie(dbElement, bForceFileCleanup)
            If bToDisk Then
                dbElement.ImagesContainer.SaveAllImages(dbElement, bForceFileCleanup)
                dbElement.Movie.SaveAllActorThumbs(dbElement)
                dbElement.Theme.SaveAllThemes(dbElement, bForceFileCleanup)
                dbElement.Trailer.SaveAllTrailers(dbElement, bForceFileCleanup)
            End If

            par_isSingle.Value = dbElement.IsSingle
            par_listTitle.Value = dbElement.ListTitle

            par_efanartsPath.Value = dbElement.ExtrafanartsPath
            par_ethumbsPath.Value = dbElement.ExtrathumbsPath
            par_nfoPath.Value = dbElement.NfoPath
            par_themePath.Value = If(Not String.IsNullOrEmpty(dbElement.Theme.LocalFilePath), dbElement.Theme.LocalFilePath, String.Empty)
            par_trailerPath.Value = If(Not String.IsNullOrEmpty(dbElement.Trailer.LocalFilePath), dbElement.Trailer.LocalFilePath, String.Empty)

            par_hasSet.Value = dbElement.Movie.SetsSpecified
            If dbElement.Subtitles Is Nothing = False Then
                par_hasSub.Value = dbElement.Subtitles.Count > 0 OrElse dbElement.Movie.FileInfo.StreamDetails.Subtitle.Count > 0
            Else
                par_hasSub.Value = Nothing
            End If

            par_locked.Value = dbElement.IsLock
            par_marked.Value = dbElement.IsMark
            par_markCustom1.Value = dbElement.IsMarkCustom1
            par_markCustom2.Value = dbElement.IsMarkCustom2
            par_markCustom3.Value = dbElement.IsMarkCustom3
            par_markCustom4.Value = dbElement.IsMarkCustom4
            par_new.Value = Not dbElement.IDSpecified

            With dbElement.Movie
                par_userRating.Value = .UserRating
                par_mpaa.Value = .MPAA
                par_originalTitle.Value = .OriginalTitle
                par_outline.Value = .Outline
                If .PlayCountSpecified Then 'need to be NOTHING instead of "0"
                    par_playcount.Value = .PlayCount
                End If
                par_plot.Value = .Plot
                par_releaseDate.Value = NumUtils.DateToISO8601Date(.ReleaseDate)
                par_runtime.Value = .Runtime
                par_sortTitle.Value = .SortTitle
                par_tagline.Value = .Tagline
                par_title.Value = .Title
                If .Top250Specified Then 'need to be NOTHING instead of "0"
                    par_top250.Value = .Top250
                End If
                par_trailer.Value = .Trailer
                par_year.Value = .Year
            End With

            par_outOfTolerance.Value = dbElement.OutOfTolerance
            par_videoSource.Value = dbElement.VideoSource
            par_language.Value = dbElement.Language

            par_idSource.Value = dbElement.Source.ID

            If Not dbElement.IDSpecified Then
                If Master.eSettings.MovieGeneralMarkNew Then
                    par_marked.Value = True
                    dbElement.IsMark = True
                End If
                Using rdrMovie As SQLiteDataReader = SQLcommand_movie.ExecuteReader()
                    If rdrMovie.Read Then
                        dbElement.ID = Convert.ToInt64(rdrMovie(0))
                    Else
                        logger.Error("Something very wrong here: SaveMovieToDB", dbElement.ToString)
                        dbElement.ID = -1
                        Return dbElement
                    End If
                End Using
            Else
                SQLcommand_movie.ExecuteNonQuery()
            End If

            If dbElement.IDSpecified Then
                'Actors
                RemoveActorsFromItem(dbElement.ID, dbElement.ContentType)
                SetActorsForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Actors)

                'Art
                RemoveArtFromItem(dbElement.ID, dbElement.ContentType)
                If dbElement.ImagesContainer.Banner.LocalFilePathSpecified Then SetArtForItem(dbElement.ID, dbElement.ContentType, "banner", dbElement.ImagesContainer.Banner)
                If dbElement.ImagesContainer.ClearArt.LocalFilePathSpecified Then SetArtForItem(dbElement.ID, dbElement.ContentType, "clearart", dbElement.ImagesContainer.ClearArt)
                If dbElement.ImagesContainer.ClearLogo.LocalFilePathSpecified Then SetArtForItem(dbElement.ID, dbElement.ContentType, "clearlogo", dbElement.ImagesContainer.ClearLogo)
                If dbElement.ImagesContainer.DiscArt.LocalFilePathSpecified Then SetArtForItem(dbElement.ID, dbElement.ContentType, "discart", dbElement.ImagesContainer.DiscArt)
                If dbElement.ImagesContainer.Fanart.LocalFilePathSpecified Then SetArtForItem(dbElement.ID, dbElement.ContentType, "fanart", dbElement.ImagesContainer.Fanart)
                If dbElement.ImagesContainer.Landscape.LocalFilePathSpecified Then SetArtForItem(dbElement.ID, dbElement.ContentType, "landscape", dbElement.ImagesContainer.Landscape)
                If dbElement.ImagesContainer.Poster.LocalFilePathSpecified Then SetArtForItem(dbElement.ID, dbElement.ContentType, "poster", dbElement.ImagesContainer.Poster)

                'Certifications
                RemoveCertificationsFromItem(dbElement.ID, dbElement.ContentType)
                SetCertificationsForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Certifications)

                'Countries
                RemoveCountriesFromItem(dbElement.ID, dbElement.ContentType)
                SetCountriesForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Countries)

                'Directors
                RemoveDirectrorsFromItem(dbElement.ID, dbElement.ContentType)
                SetDirectorsForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Directors)

                'Genres
                RemoveGenresFromItem(dbElement.ID, dbElement.ContentType)
                SetGenresForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Genres)

                'Studios
                RemoveStudiosFromItem(dbElement.ID, dbElement.ContentType)
                SetStudiosForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Studios)

                'Tags
                RemoveTagsFromItem(dbElement.ID, dbElement.ContentType)
                SetTagsForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Tags)

                'UniqueIDs
                RemoveUniqueIDsFromItem(dbElement.ID, dbElement.ContentType)
                SetUniqueIDsForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.UniqueIDs)

                'Writers
                RemoveWritersFromItem(dbElement.ID, dbElement.ContentType)
                SetWritersForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Credits)

                'Video Streams
                Using SQLcommandMoviesVStreams As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandMoviesVStreams.CommandText = String.Format("DELETE FROM MoviesVStreams WHERE MovieID = {0};", dbElement.ID)
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

                    For i As Integer = 0 To dbElement.Movie.FileInfo.StreamDetails.Video.Count - 1
                        parVideo_MovieID.Value = dbElement.ID
                        parVideo_StreamID.Value = i
                        parVideo_Width.Value = dbElement.Movie.FileInfo.StreamDetails.Video(i).Width
                        parVideo_Height.Value = dbElement.Movie.FileInfo.StreamDetails.Video(i).Height
                        parVideo_Codec.Value = dbElement.Movie.FileInfo.StreamDetails.Video(i).Codec
                        parVideo_Duration.Value = dbElement.Movie.FileInfo.StreamDetails.Video(i).Duration
                        parVideo_ScanType.Value = dbElement.Movie.FileInfo.StreamDetails.Video(i).Scantype
                        parVideo_AspectDisplayRatio.Value = dbElement.Movie.FileInfo.StreamDetails.Video(i).Aspect
                        parVideo_Language.Value = dbElement.Movie.FileInfo.StreamDetails.Video(i).Language
                        parVideo_LongLanguage.Value = dbElement.Movie.FileInfo.StreamDetails.Video(i).LongLanguage
                        parVideo_Bitrate.Value = dbElement.Movie.FileInfo.StreamDetails.Video(i).Bitrate
                        parVideo_MultiViewCount.Value = dbElement.Movie.FileInfo.StreamDetails.Video(i).MultiViewCount
                        parVideo_MultiViewLayout.Value = dbElement.Movie.FileInfo.StreamDetails.Video(i).MultiViewLayout
                        parVideo_FileSize.Value = dbElement.Movie.FileInfo.StreamDetails.Video(i).Filesize
                        parVideo_StereoMode.Value = dbElement.Movie.FileInfo.StreamDetails.Video(i).StereoMode

                        SQLcommandMoviesVStreams.ExecuteNonQuery()
                    Next
                End Using
                Using SQLcommandMoviesAStreams As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandMoviesAStreams.CommandText = String.Concat("DELETE FROM MoviesAStreams WHERE MovieID = ", dbElement.ID, ";")
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

                    For i As Integer = 0 To dbElement.Movie.FileInfo.StreamDetails.Audio.Count - 1
                        parAudio_MovieID.Value = dbElement.ID
                        parAudio_StreamID.Value = i
                        parAudio_Language.Value = dbElement.Movie.FileInfo.StreamDetails.Audio(i).Language
                        parAudio_LongLanguage.Value = dbElement.Movie.FileInfo.StreamDetails.Audio(i).LongLanguage
                        parAudio_Codec.Value = dbElement.Movie.FileInfo.StreamDetails.Audio(i).Codec
                        parAudio_Channel.Value = dbElement.Movie.FileInfo.StreamDetails.Audio(i).Channels
                        parAudio_Bitrate.Value = dbElement.Movie.FileInfo.StreamDetails.Audio(i).Bitrate

                        SQLcommandMoviesAStreams.ExecuteNonQuery()
                    Next
                End Using

                'subtitles
                Using SQLcommandMoviesSubs As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandMoviesSubs.CommandText = String.Concat("DELETE FROM MoviesSubs WHERE MovieID = ", dbElement.ID, ";")
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
                    For i As Integer = 0 To dbElement.Movie.FileInfo.StreamDetails.Subtitle.Count - 1
                        parSubs_MovieID.Value = dbElement.ID
                        parSubs_StreamID.Value = iID
                        parSubs_Language.Value = dbElement.Movie.FileInfo.StreamDetails.Subtitle(i).Language
                        parSubs_LongLanguage.Value = dbElement.Movie.FileInfo.StreamDetails.Subtitle(i).LongLanguage
                        parSubs_Type.Value = dbElement.Movie.FileInfo.StreamDetails.Subtitle(i).SubsType
                        parSubs_Path.Value = dbElement.Movie.FileInfo.StreamDetails.Subtitle(i).SubsPath
                        parSubs_Forced.Value = dbElement.Movie.FileInfo.StreamDetails.Subtitle(i).SubsForced
                        SQLcommandMoviesSubs.ExecuteNonQuery()
                        iID += 1
                    Next
                    'external subtitles
                    For i As Integer = 0 To dbElement.Subtitles.Count - 1
                        parSubs_MovieID.Value = dbElement.ID
                        parSubs_StreamID.Value = iID
                        parSubs_Language.Value = dbElement.Subtitles(i).Language
                        parSubs_LongLanguage.Value = dbElement.Subtitles(i).LongLanguage
                        parSubs_Type.Value = dbElement.Subtitles(i).SubsType
                        parSubs_Path.Value = dbElement.Subtitles(i).SubsPath
                        parSubs_Forced.Value = dbElement.Subtitles(i).SubsForced
                        SQLcommandMoviesSubs.ExecuteNonQuery()
                        iID += 1
                    Next
                End Using

                'MovieSets part
                Using SQLcommand_setlinkmovie As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand_setlinkmovie.CommandText = String.Concat("DELETE FROM setlinkmovie WHERE idMovie = ", dbElement.ID, ";")
                    SQLcommand_setlinkmovie.ExecuteNonQuery()
                End Using

                Dim bIsNewSet As Boolean
                For Each s As MediaContainers.SetDetails In dbElement.Movie.Sets
                    If s.TitleSpecified Then
                        bIsNewSet = s.ID = -1
                        If Not bIsNewSet Then
                            Using SQLcommand_setlinkmovie As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                SQLcommand_setlinkmovie.CommandText = String.Concat("INSERT OR REPLACE INTO setlinkmovie (",
                             "idMovie, idSet, iOrder",
                             ") VALUES (?,?,?);")
                                Dim par_setlinkmovie_idMovie As SQLiteParameter = SQLcommand_setlinkmovie.Parameters.Add("parSets_idMovie", DbType.Int64, 0, "idMovie")
                                Dim par_setlinkmovie_idSet As SQLiteParameter = SQLcommand_setlinkmovie.Parameters.Add("parSets_idSet", DbType.Int64, 0, "idSet")
                                Dim par_setlinkmovie_iOrder As SQLiteParameter = SQLcommand_setlinkmovie.Parameters.Add("parSets_iOrder", DbType.Int64, 0, "iOrder")

                                par_setlinkmovie_idMovie.Value = dbElement.ID
                                par_setlinkmovie_idSet.Value = s.ID
                                par_setlinkmovie_iOrder.Value = s.Order
                                SQLcommand_setlinkmovie.ExecuteNonQuery()
                            End Using
                        Else
                            'first check if a Set with same TMDBColID is already existing
                            If s.TMDBSpecified Then
                                Using SQLcommand_sets As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    SQLcommand_sets.CommandText = String.Concat("SELECT idSet, SetName, Plot ",
                                                                               "FROM sets WHERE TMDBColID LIKE """, s.TMDB, """;")
                                    Using SQLreader As SQLiteDataReader = SQLcommand_sets.ExecuteReader()
                                        If SQLreader.HasRows Then
                                            SQLreader.Read()
                                            If Not DBNull.Value.Equals(SQLreader("idSet")) Then s.ID = CInt(SQLreader("idSet"))
                                            If Not DBNull.Value.Equals(SQLreader("SetName")) Then s.Title = CStr(SQLreader("SetName"))
                                            If Not DBNull.Value.Equals(SQLreader("Plot")) AndAlso
                                                Not String.IsNullOrEmpty(CStr(SQLreader("Plot"))) Then s.Plot = CStr(SQLreader("Plot"))
                                            bIsNewSet = False
                                            NFO.SaveToNFO_Movie(dbElement, False) 'to save the "new" SetName and/or SetPlot
                                        Else
                                            bIsNewSet = True
                                        End If
                                    End Using
                                End Using
                            End If

                            If bIsNewSet Then
                                'secondly check if a Set with same name is already existing
                                Using SQLcommand_sets As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    SQLcommand_sets.CommandText = String.Concat("SELECT idSet, Plot ",
                                                                               "FROM sets WHERE SetName LIKE """, s.Title, """;")
                                    Using SQLreader As SQLiteDataReader = SQLcommand_sets.ExecuteReader()
                                        If SQLreader.HasRows Then
                                            SQLreader.Read()
                                            If Not DBNull.Value.Equals(SQLreader("idSet")) Then s.ID = CInt(SQLreader("idSet"))
                                            If Not DBNull.Value.Equals(SQLreader("Plot")) AndAlso
                                                Not String.IsNullOrEmpty(CStr(SQLreader("Plot"))) Then s.Plot = CStr(SQLreader("Plot"))
                                            bIsNewSet = False
                                            NFO.SaveToNFO_Movie(dbElement, False) 'to save the "new" SetName and/or SetPlot
                                        Else
                                            bIsNewSet = True
                                        End If
                                    End Using
                                End Using
                            End If

                            If Not bIsNewSet Then
                                'create new setlinkmovie with existing SetID
                                Using SQLcommand_setlinkmovie As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    SQLcommand_setlinkmovie.CommandText = String.Concat("INSERT OR REPLACE INTO setlinkmovie (",
                                                                                     "idMovie, idSet, iOrder",
                                                                                     ") VALUES (?,?,?);")
                                    Dim par_setlinkmovie_idMovie As SQLiteParameter = SQLcommand_setlinkmovie.Parameters.Add("parSets_idMovie", DbType.Int64, 0, "idMovie")
                                    Dim par_setlinkmovie_idSet As SQLiteParameter = SQLcommand_setlinkmovie.Parameters.Add("parSets_idSet", DbType.Int64, 0, "idSet")
                                    Dim par_setlinkmovie_iOrder As SQLiteParameter = SQLcommand_setlinkmovie.Parameters.Add("parSets_iOrder", DbType.Int64, 0, "iOrder")

                                    par_setlinkmovie_idMovie.Value = dbElement.ID
                                    par_setlinkmovie_idSet.Value = s.ID
                                    par_setlinkmovie_iOrder.Value = s.Order
                                    SQLcommand_setlinkmovie.ExecuteNonQuery()
                                End Using

                                'update existing set with latest TMDB Collection ID and SetPlot
                                Using SQLcommand_sets As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    SQLcommand_sets.CommandText = String.Format("UPDATE sets SET TMDBColID=?, Plot=? WHERE idSet={0}", s.ID)
                                    Dim par_sets_TMDBColID As SQLiteParameter = SQLcommand_sets.Parameters.Add("parSets_TMDBColID", DbType.String, 0, "TMDBColID")
                                    Dim par_sets_Plot As SQLiteParameter = SQLcommand_sets.Parameters.Add("parSets_Plot", DbType.String, 0, "Plot")
                                    par_sets_TMDBColID.Value = s.TMDB
                                    par_sets_Plot.Value = s.Plot
                                    SQLcommand_sets.ExecuteNonQuery()
                                End Using
                            Else
                                'create new Set
                                Using SQLcommand_sets As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    SQLcommand_sets.CommandText = String.Concat("INSERT OR REPLACE INTO sets (",
                                                                                     "ListTitle, NfoPath, TMDBColID, Plot, SetName, ",
                                                                                     "New, Mark, Lock, SortMethod, Language",
                                                                                     ") VALUES (?,?,?,?,?,?,?,?,?,?);")
                                    Dim par_sets_ListTitle As SQLiteParameter = SQLcommand_sets.Parameters.Add("parSets_ListTitle", DbType.String, 0, "ListTitle")
                                    Dim par_sets_NfoPath As SQLiteParameter = SQLcommand_sets.Parameters.Add("parSets_NfoPath", DbType.String, 0, "NfoPath")
                                    Dim par_sets_TMDBColID As SQLiteParameter = SQLcommand_sets.Parameters.Add("parSets_TMDBColID", DbType.String, 0, "TMDBColID")
                                    Dim par_sets_Plot As SQLiteParameter = SQLcommand_sets.Parameters.Add("parSets_Plot", DbType.String, 0, "Plot")
                                    Dim par_sets_SetName As SQLiteParameter = SQLcommand_sets.Parameters.Add("parSets_SetName", DbType.String, 0, "SetName")
                                    Dim par_sets_New As SQLiteParameter = SQLcommand_sets.Parameters.Add("parSets_New", DbType.Boolean, 0, "New")
                                    Dim par_sets_Mark As SQLiteParameter = SQLcommand_sets.Parameters.Add("parSets_Mark", DbType.Boolean, 0, "Mark")
                                    Dim par_sets_Lock As SQLiteParameter = SQLcommand_sets.Parameters.Add("parSets_Lock", DbType.Boolean, 0, "Lock")
                                    Dim par_sets_SortMethod As SQLiteParameter = SQLcommand_sets.Parameters.Add("parSets_SortMethod", DbType.Int64, 0, "SortMethod")
                                    Dim par_sets_Language As SQLiteParameter = SQLcommand_sets.Parameters.Add("parSets_Language", DbType.String, 0, "Language")

                                    par_sets_SetName.Value = s.Title
                                    par_sets_ListTitle.Value = StringUtils.SortTokens_MovieSet(s.Title)
                                    par_sets_TMDBColID.Value = s.TMDB
                                    par_sets_Plot.Value = s.Plot
                                    par_sets_NfoPath.Value = String.Empty
                                    par_sets_New.Value = True
                                    par_sets_Lock.Value = False
                                    par_sets_SortMethod.Value = Enums.SortMethod_MovieSet.Year
                                    par_sets_Language.Value = dbElement.Language

                                    If Master.eSettings.MovieSetGeneralMarkNew Then
                                        par_sets_Mark.Value = True
                                    Else
                                        par_sets_Mark.Value = False
                                    End If
                                    SQLcommand_sets.ExecuteNonQuery()
                                End Using

                                Using SQLcommand_sets As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    SQLcommand_sets.CommandText = String.Concat("SELECT idSet, SetName FROM sets WHERE SetName LIKE """, s.Title, """;")
                                    Using rdrSets As SQLiteDataReader = SQLcommand_sets.ExecuteReader()
                                        If rdrSets.Read Then
                                            s.ID = Convert.ToInt64(rdrSets(0))
                                        End If
                                    End Using
                                End Using

                                'create new setlinkmovie with new SetID
                                If s.ID > 0 Then
                                    Using SQLcommand_setlinkmovie As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                        SQLcommand_setlinkmovie.CommandText = String.Concat("INSERT OR REPLACE INTO setlinkmovie (",
                                                                                         "idMovie, idSet, iOrder",
                                                                                         ") VALUES (?,?,?);")
                                        Dim par_setlinkmovie_idMovie As SQLiteParameter = SQLcommand_setlinkmovie.Parameters.Add("parSets_idMovie", DbType.Int64, 0, "idMovie")
                                        Dim par_setlinkmovie_idSet As SQLiteParameter = SQLcommand_setlinkmovie.Parameters.Add("parSets_idSet", DbType.Int64, 0, "idSet")
                                        Dim par_setlinkmovie_iOrder As SQLiteParameter = SQLcommand_setlinkmovie.Parameters.Add("parSets_iOrder", DbType.Int64, 0, "iOrder")

                                        par_setlinkmovie_idMovie.Value = dbElement.ID
                                        par_setlinkmovie_idSet.Value = s.ID
                                        par_setlinkmovie_iOrder.Value = s.Order
                                        SQLcommand_setlinkmovie.ExecuteNonQuery()
                                    End Using
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        End Using

        'YAMJ watched file
        If dbElement.Movie.PlayCountSpecified AndAlso Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
            For Each a In FileUtils.GetFilenameList.Movie(dbElement, Enums.ModifierType.MainWatchedFile)
                If Not File.Exists(a) Then
                    Dim fs As FileStream = File.Create(a)
                    fs.Close()
                End If
            Next
        ElseIf Not dbElement.Movie.PlayCountSpecified AndAlso Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
            For Each a In FileUtils.GetFilenameList.Movie(dbElement, Enums.ModifierType.MainWatchedFile)
                If File.Exists(a) Then
                    File.Delete(a)
                End If
            Next
        End If

        If Not bBatchMode Then SQLtransaction.Commit()

        If bDoSync Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Sync_Movie, Nothing, Nothing, False, dbElement)
        End If

        Return dbElement
    End Function
    ''' <summary>
    ''' Saves all information from a Database.DBElement object to the database
    ''' </summary>
    ''' <param name="dbElement">object to save to the database</param>
    ''' <param name="bBatchMode">Is the function already part of a transaction?</param>
    ''' <param name="bToDisk">Create NFO and Images</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Save_MovieSet(ByVal dbElement As DBElement, ByVal bBatchMode As Boolean, ByVal bToNFO As Boolean, ByVal bToDisk As Boolean, ByVal bDoSync As Boolean) As DBElement
        If dbElement.MovieSet Is Nothing Then Return dbElement

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not bBatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not dbElement.IDSpecified Then
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO movieset (",
                 "listTitle, nfoPath, plot, title, new, marked, locked, sortMethod, language",
                 ") VALUES (?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM movieset;")
            Else
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO movieset (",
                 "idSet, listTitle, nfoPath, plot, title, new, marked, locked, sortMethod, language",
                 ") VALUES (?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM movieset;")
                Dim par_idSet As SQLiteParameter = SQLcommand.Parameters.Add("par_idSet", DbType.Int64, 0, "idSet")
                par_idSet.Value = dbElement.ID
            End If
            Dim par_listTitle As SQLiteParameter = SQLcommand.Parameters.Add("par_listTitle", DbType.String, 0, "listTitle")
            Dim par_nfoPath As SQLiteParameter = SQLcommand.Parameters.Add("par_nfoPath", DbType.String, 0, "nfoPath")
            Dim par_plot As SQLiteParameter = SQLcommand.Parameters.Add("par_plot", DbType.String, 0, "plot")
            Dim par_title As SQLiteParameter = SQLcommand.Parameters.Add("par_title", DbType.String, 0, "title")
            Dim par_new As SQLiteParameter = SQLcommand.Parameters.Add("par_new", DbType.Boolean, 0, "new")
            Dim par_marked As SQLiteParameter = SQLcommand.Parameters.Add("par_marked", DbType.Boolean, 0, "marked")
            Dim par_locked As SQLiteParameter = SQLcommand.Parameters.Add("par_locked", DbType.Boolean, 0, "lcoked")
            Dim par_sortMethod As SQLiteParameter = SQLcommand.Parameters.Add("par_sortMethod", DbType.Int16, 0, "sortMethod")
            Dim par_language As SQLiteParameter = SQLcommand.Parameters.Add("par_language", DbType.String, 0, "language")

            'First let's save it to NFO, even because we will need the NFO path, also save Images
            'art Table be be linked later
            If bToNFO Then NFO.SaveToNFO_MovieSet(dbElement)
            If bToDisk Then
                dbElement.ImagesContainer.SaveAllImages(dbElement, False)
            End If

            par_nfoPath.Value = dbElement.NfoPath
            par_language.Value = dbElement.Language

            par_new.Value = Not dbElement.IDSpecified
            par_marked.Value = dbElement.IsMark
            par_locked.Value = dbElement.IsLock
            par_sortMethod.Value = dbElement.SortMethod

            par_listTitle.Value = dbElement.ListTitle
            par_title.Value = dbElement.MovieSet.Title
            par_plot.Value = dbElement.MovieSet.Plot

            If Not dbElement.IDSpecified Then
                If Master.eSettings.MovieSetGeneralMarkNew Then
                    par_marked.Value = True
                    dbElement.IsMark = True
                End If
                Using rdrMovieSet As SQLiteDataReader = SQLcommand.ExecuteReader()
                    If rdrMovieSet.Read Then
                        dbElement.ID = Convert.ToInt64(rdrMovieSet(0))
                    Else
                        logger.Error("Something very wrong here: SaveMovieSetToDB", dbElement.ToString, "Error")
                        dbElement.ListTitle = "SETERROR"
                        Return dbElement
                    End If
                End Using
            Else
                SQLcommand.ExecuteNonQuery()
            End If
        End Using

        'Art
        RemoveArtFromItem(dbElement.ID, dbElement.ContentType)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Banner.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "banner", dbElement.ImagesContainer.Banner)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.ClearArt.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "clearart", dbElement.ImagesContainer.ClearArt)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.ClearLogo.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "clearlogo", dbElement.ImagesContainer.ClearLogo)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.DiscArt.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "discart", dbElement.ImagesContainer.DiscArt)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Fanart.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "fanart", dbElement.ImagesContainer.Fanart)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Landscape.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "landscape", dbElement.ImagesContainer.Landscape)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Poster.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "poster", dbElement.ImagesContainer.Poster)

        'UniqueIDs
        RemoveUniqueIDsFromItem(dbElement.ID, dbElement.ContentType)
        SetUniqueIDsForItem(dbElement.ID, dbElement.ContentType, dbElement.MovieSet.UniqueIDs)

        'save set informations to movies
        For Each tMovie In dbElement.MoviesInSet
            tMovie.DBMovie.Movie.AddSet(New MediaContainers.SetDetails With {
                                                .ID = dbElement.ID,
                                                .Order = tMovie.Order,
                                                .Plot = dbElement.MovieSet.Plot,
                                                .Title = dbElement.MovieSet.Title,
                                                .TMDB = dbElement.MovieSet.TMDB})
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, Nothing, False, tMovie.DBMovie)
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_Movie, Nothing, Nothing, False, tMovie.DBMovie)
            Save_Movie(tMovie.DBMovie, True, True, False, True, False)
            RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {tMovie.DBMovie.ID}))
        Next

        'remove set-information from movies which are no longer assigned to this set
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT idMovie, idSet FROM movieset_link WHERE idSet={0};", dbElement.ID)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Dim rMovie = dbElement.MoviesInSet.FirstOrDefault(Function(f) f.DBMovie.ID = Convert.ToInt64(SQLreader("idMovie")))
                    If rMovie Is Nothing Then
                        'movie is no longer a part of this set
                        Dim tMovie As Database.DBElement = Load_Movie(Convert.ToInt64(SQLreader("idMovie")))
                        tMovie.Movie.RemoveSet(dbElement.ID)
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, Nothing, False, tMovie)
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_Movie, Nothing, Nothing, False, tMovie)
                        Save_Movie(tMovie, True, True, False, True, False)
                        RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {tMovie.ID}))
                    End If
                End While
            End Using
        End Using

        If Not bBatchMode Then SQLtransaction.Commit()

        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Sync_MovieSet, Nothing, Nothing, False, dbElement)

        Return dbElement
    End Function

    Public Sub Save_Source_Movie(ByVal dbSource As DBSource)
        Using SQLtransaction As SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                If dbSource.IDSpecified Then
                    SQLcommand.CommandText = String.Format("UPDATE moviesource SET path=(?), name=(?), scanRecursive=(?), useFoldername=(?), isSingle=(?), lastScan=(?), exclude=(?), getYear=(?) , language=(?) WHERE idSource ={0};", dbSource.ID)
                Else
                    SQLcommand.CommandText = "INSERT OR REPLACE INTO moviesource (path, name, scanRecursive, useFoldername, isSingle, lastScan, exclude, getYear, language) VALUES (?,?,?,?,?,?,?,?,?);"
                End If
                Dim par_path As SQLiteParameter = SQLcommand.Parameters.Add("par_path", DbType.String, 0, "path")
                Dim par_name As SQLiteParameter = SQLcommand.Parameters.Add("par_name", DbType.String, 0, "name")
                Dim par_scanRecursive As SQLiteParameter = SQLcommand.Parameters.Add("par_scanRecursive", DbType.Boolean, 0, "scanRecursive")
                Dim par_useFoldername As SQLiteParameter = SQLcommand.Parameters.Add("par_useFoldername", DbType.Boolean, 0, "useFoldername")
                Dim par_isSingle As SQLiteParameter = SQLcommand.Parameters.Add("par_isSingle", DbType.Boolean, 0, "isSingle")
                Dim par_lastScan As SQLiteParameter = SQLcommand.Parameters.Add("par_lastScan", DbType.String, 0, "lastScan")
                Dim par_exclude As SQLiteParameter = SQLcommand.Parameters.Add("par_exclude", DbType.Boolean, 0, "exclude")
                Dim par_getYear As SQLiteParameter = SQLcommand.Parameters.Add("par_getYear", DbType.Boolean, 0, "getYear")
                Dim par_language As SQLiteParameter = SQLcommand.Parameters.Add("par_language", DbType.String, 0, "language")
                par_path.Value = dbSource.Path
                par_name.Value = dbSource.Name
                par_scanRecursive.Value = dbSource.ScanRecursive
                par_useFoldername.Value = dbSource.UseFolderName
                par_isSingle.Value = dbSource.IsSingle
                par_lastScan.Value = DateTime.Now
                par_exclude.Value = dbSource.Exclude
                par_getYear.Value = dbSource.GetYear
                If dbSource.LanguageSpecified Then
                    par_language.Value = dbSource.Language
                Else
                    par_language.Value = "en-US"
                End If

                SQLcommand.ExecuteNonQuery()
            End Using
            SQLtransaction.Commit()
        End Using
    End Sub

    Public Sub Save_Source_TVShow(ByVal dbSource As DBSource)
        Using SQLtransaction As SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                If dbSource.IDSpecified Then
                    SQLcommand.CommandText = String.Format("UPDATE tvshowsource SET path=(?), name=(?), language=(?), episodeOrdering=(?), exclude=(?), episodeSorting=(?) , isSingle=(?) WHERE idSource={0};", dbSource.ID)
                Else
                    SQLcommand.CommandText = "INSERT OR REPLACE INTO tvshowsource (path, name, language, episodeOrdering, exclude, episodeSorting, isSingle) VALUES (?,?,?,?,?,?,?);"
                End If
                Dim par_path As SQLiteParameter = SQLcommand.Parameters.Add("par_path", DbType.String, 0, "path")
                Dim par_name As SQLiteParameter = SQLcommand.Parameters.Add("par_name", DbType.String, 0, "name")
                Dim par_language As SQLiteParameter = SQLcommand.Parameters.Add("par_language", DbType.String, 0, "language")
                Dim par_episodeOrdering As SQLiteParameter = SQLcommand.Parameters.Add("par_episodeOrdering", DbType.Int16, 0, "episodeOrdering")
                Dim par_exclude As SQLiteParameter = SQLcommand.Parameters.Add("par_exclude", DbType.Boolean, 0, "exclude")
                Dim par_episodeSorting As SQLiteParameter = SQLcommand.Parameters.Add("par_episodeSorting", DbType.Int16, 0, "episodeSorting")
                Dim par_isSingle As SQLiteParameter = SQLcommand.Parameters.Add("par_isSingle", DbType.Boolean, 0, "isSingle")
                par_path.Value = dbSource.Path
                par_name.Value = dbSource.Name
                par_exclude.Value = dbSource.Exclude
                par_isSingle.Value = dbSource.IsSingle
                If dbSource.LanguageSpecified Then
                    par_language.Value = dbSource.Language
                Else
                    par_language.Value = "en-US"
                End If
                par_episodeOrdering.Value = dbSource.EpisodeOrdering
                par_episodeSorting.Value = dbSource.EpisodeSorting

                SQLcommand.ExecuteNonQuery()
            End Using
            SQLtransaction.Commit()
        End Using
    End Sub

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
                SQLcommand.CommandText = String.Concat("SELECT idMedia, idTag FROM tag_link ",
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
                    If MoviesInTagOld(i).Movie.IMDB = movienew.Movie.IMDB Then
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
    ''' Saves all episode information from a Database.DBElement object to the database
    ''' </summary>
    ''' <param name="dbElement">Database.DBElement object to save to the database</param>
    ''' <param name="bDoSeasonCheck">If <c>True</c> then check if it's needed to create a new season for this episode</param>
    ''' <param name="bBatchMode">Is the function already part of a transaction?</param>
    ''' <param name="bToDisk">Create NFO and Images</param>
    Public Function Save_TVEpisode(ByVal dbElement As DBElement, ByVal bBatchMode As Boolean, ByVal bToNFO As Boolean, ByVal bToDisk As Boolean, ByVal bDoSeasonCheck As Boolean, ByVal bDoSync As Boolean, Optional ByVal bForceIsNewFlag As Boolean = False) As DBElement
        If dbElement.TVEpisode Is Nothing Then Return dbElement

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not bBatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        'delete so it will remove if there is a "missing" episode entry already. Only "missing" episodes must be deleted.
        Using SQLCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLCommand.CommandText = String.Concat("DELETE FROM episode WHERE idShow = ", dbElement.ShowID, " AND Episode = ", dbElement.TVEpisode.Episode, " AND Season = ", dbElement.TVEpisode.Season, " AND idFile = -1;")
            SQLCommand.ExecuteNonQuery()
        End Using

        If dbElement.FilenameSpecified Then
            If dbElement.FilenameIDSpecified Then
                Using SQLpathcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLpathcommand.CommandText = String.Concat("INSERT OR REPLACE INTO files (idFile, strFilename) VALUES (?,?);")

                    Dim parID As SQLiteParameter = SQLpathcommand.Parameters.Add("parFileID", DbType.Int64, 0, "idFile")
                    Dim parFilename As SQLiteParameter = SQLpathcommand.Parameters.Add("parFilename", DbType.String, 0, "strFilename")
                    parID.Value = dbElement.FilenameID
                    parFilename.Value = dbElement.Filename
                    SQLpathcommand.ExecuteNonQuery()
                End Using
            Else
                Using SQLpathcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLpathcommand.CommandText = "SELECT idFile FROM files WHERE strFilename = (?);"

                    Dim parPath As SQLiteParameter = SQLpathcommand.Parameters.Add("parFilename", DbType.String, 0, "strFilename")
                    parPath.Value = dbElement.Filename

                    Using SQLreader As SQLiteDataReader = SQLpathcommand.ExecuteReader
                        If SQLreader.HasRows Then
                            SQLreader.Read()
                            dbElement.FilenameID = Convert.ToInt64(SQLreader("idFile"))
                        Else
                            Using SQLpcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                SQLpcommand.CommandText = String.Concat("INSERT INTO files (",
                                     "strFilename) VALUES (?); SELECT LAST_INSERT_ROWID() FROM files;")
                                Dim parEpPath As SQLiteParameter = SQLpcommand.Parameters.Add("parEpPath", DbType.String, 0, "strFilename")
                                parEpPath.Value = dbElement.Filename

                                dbElement.FilenameID = Convert.ToInt64(SQLpcommand.ExecuteScalar)
                            End Using
                        End If
                    End Using
                End Using
            End If
        End If

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not dbElement.IDSpecified Then
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO episode (",
                 "idShow, idFile, idSource, New, Mark, Lock, Title, Season, Episode, ",
                 "Rating, Plot, Aired, NfoPath, Playcount, ",
                 "DisplaySeason, DisplayEpisode, DateAdded, Runtime, Votes, VideoSource, HasSub, SubEpisode, ",
                 "iLastPlayed, strIMDB, strTMDB, strTVDB, iUserRating",
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM episode;")

            Else
                SQLcommand.CommandText = String.Concat("INSERT OR REPLACE INTO episode (",
                 "idEpisode, idShow, idFile, idSource, New, Mark, Lock, Title, Season, Episode, ",
                 "Rating, Plot, Aired, NfoPath, Playcount, ",
                 "DisplaySeason, DisplayEpisode, DateAdded, Runtime, Votes, VideoSource, HasSub, SubEpisode, ",
                 "iLastPlayed, strIMDB, strTMDB, strTVDB, iUserRating",
                 ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM episode;")

                Dim parTVEpisodeID As SQLiteParameter = SQLcommand.Parameters.Add("parTVEpisodeID", DbType.Int64, 0, "idEpisode")
                parTVEpisodeID.Value = dbElement.ID
            End If

            Dim parTVShowID As SQLiteParameter = SQLcommand.Parameters.Add("parTVShowID", DbType.Int64, 0, "idShow")
            Dim parTVFileID As SQLiteParameter = SQLcommand.Parameters.Add("parTVFileID", DbType.Int64, 0, "idFile")
            Dim parSourceID As SQLiteParameter = SQLcommand.Parameters.Add("parSourceID", DbType.Int64, 0, "idSource")
            Dim parNew As SQLiteParameter = SQLcommand.Parameters.Add("parNew", DbType.Boolean, 0, "new")
            Dim parMark As SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
            Dim parLock As SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "lock")
            Dim parTitle As SQLiteParameter = SQLcommand.Parameters.Add("parTitle", DbType.String, 0, "Title")
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
                If Not Master.eSettings.GeneralDateAddedIgnoreNFO AndAlso Not String.IsNullOrEmpty(dbElement.TVEpisode.DateAdded) Then
                    Dim DateTimeAdded As Date = Date.ParseExact(dbElement.TVEpisode.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    parDateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    Select Case Master.eSettings.GeneralDateTime
                        Case Enums.DateTime.Now
                            parDateAdded.Value = If(Not dbElement.IDSpecified, Functions.ConvertToUnixTimestamp(Date.Now), dbElement.DateAdded)
                        Case Enums.DateTime.ctime
                            Dim ctime As Date = File.GetCreationTime(dbElement.Filename)
                            If ctime.Year > 1601 Then
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            Else
                                Dim mtime As Date = File.GetLastWriteTime(dbElement.Filename)
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            End If
                        Case Enums.DateTime.mtime
                            Dim mtime As Date = File.GetLastWriteTime(dbElement.Filename)
                            If mtime.Year > 1601 Then
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                Dim ctime As Date = File.GetCreationTime(dbElement.Filename)
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                        Case Enums.DateTime.Newer
                            Dim mtime As Date = File.GetLastWriteTime(dbElement.Filename)
                            Dim ctime As Date = File.GetCreationTime(dbElement.Filename)
                            If mtime > ctime Then
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                parDateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                    End Select
                End If
                dbElement.TVEpisode.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(parDateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            Catch ex As Exception
                parDateAdded.Value = If(Not dbElement.IDSpecified, Functions.ConvertToUnixTimestamp(Date.Now), dbElement.DateAdded)
                dbElement.TVEpisode.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(parDateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            End Try

            Dim DateTimeLastPlayedUnix As Double = -1
            If dbElement.TVEpisode.LastPlayedSpecified Then
                Try
                    Dim DateTimeLastPlayed As Date = Date.ParseExact(dbElement.TVEpisode.LastPlayed, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    DateTimeLastPlayedUnix = Functions.ConvertToUnixTimestamp(DateTimeLastPlayed)
                Catch
                    'Kodi save it only as yyyy-MM-dd, try that
                    Try
                        Dim DateTimeLastPlayed As Date = Date.ParseExact(dbElement.TVEpisode.LastPlayed, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture)
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
                dbElement.TVEpisode.LastPlayed = String.Empty
            End If

            'First let's save it to NFO, even because we will need the NFO path, also save Images
            'art Table be be linked later
            If dbElement.FilenameIDSpecified Then
                If bToNFO Then NFO.SaveToNFO_TVEpisode(dbElement)
                If bToDisk Then
                    dbElement.ImagesContainer.SaveAllImages(dbElement, False)
                    dbElement.TVEpisode.SaveAllActorThumbs(dbElement)
                End If
            End If

            parTVShowID.Value = dbElement.ShowID
            parNfoPath.Value = dbElement.NfoPath
            parHasSub.Value = (dbElement.Subtitles IsNot Nothing AndAlso dbElement.Subtitles.Count > 0) OrElse dbElement.TVEpisode.FileInfo.StreamDetails.Subtitle.Count > 0
            parNew.Value = bForceIsNewFlag OrElse Not dbElement.IDSpecified
            parMark.Value = dbElement.IsMark
            parTVFileID.Value = dbElement.FilenameID
            parLock.Value = dbElement.IsLock
            parSourceID.Value = dbElement.Source.ID
            parVideoSource.Value = dbElement.VideoSource

            With dbElement.TVEpisode
                parTitle.Value = .Title
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
                par_strIMDB.Value = .IMDB
                par_strTMDB.Value = .TMDB
                par_strTVDB.Value = .TVDB
            End With

            If Not dbElement.IDSpecified Then
                If Master.eSettings.TVGeneralMarkNewEpisodes Then
                    parMark.Value = True
                    dbElement.IsMark = True
                End If
                Using rdrTVEp As SQLiteDataReader = SQLcommand.ExecuteReader()
                    If rdrTVEp.Read Then
                        dbElement.ID = Convert.ToInt64(rdrTVEp(0))
                    Else
                        logger.Error("Something very wrong here: SaveTVEpToDB", dbElement.ToString, "Error")
                        dbElement.ID = -1
                        Return dbElement
                        Exit Function
                    End If
                End Using
            Else
                SQLcommand.ExecuteNonQuery()
            End If

            If dbElement.IDSpecified Then

                'Actors
                RemoveActorsFromItem(dbElement.ID, dbElement.ContentType)
                SetActorsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVEpisode.Actors)

                'Art
                RemoveArtFromItem(dbElement.ID, dbElement.ContentType)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Fanart.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "fanart", dbElement.ImagesContainer.Fanart)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Poster.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "thumb", dbElement.ImagesContainer.Poster)

                'Directors
                RemoveDirectrorsFromItem(dbElement.ID, dbElement.ContentType)
                SetDirectorsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVEpisode.Directors)

                'Guest Stars
                RemoveGuestStarsFromItem(dbElement.ID, dbElement.ContentType)
                SetGuestStarsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVEpisode.GuestStars)

                'UniqueIDs
                RemoveUniqueIDsFromItem(dbElement.ID, dbElement.ContentType)
                SetUniqueIDsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVEpisode.UniqueIDs)

                'Writers
                RemoveWritersFromItem(dbElement.ID, dbElement.ContentType)
                SetWritersForItem(dbElement.ID, dbElement.ContentType, dbElement.TVEpisode.Credits)

                Using SQLcommandTVVStreams As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandTVVStreams.CommandText = String.Concat("DELETE FROM TVVStreams WHERE TVEpID = ", dbElement.ID, ";")
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

                    For i As Integer = 0 To dbElement.TVEpisode.FileInfo.StreamDetails.Video.Count - 1
                        parVideo_EpID.Value = dbElement.ID
                        parVideo_StreamID.Value = i
                        parVideo_Width.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Video(i).Width
                        parVideo_Height.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Video(i).Height
                        parVideo_Codec.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Video(i).Codec
                        parVideo_Duration.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Video(i).Duration
                        parVideo_ScanType.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Video(i).Scantype
                        parVideo_AspectDisplayRatio.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Video(i).Aspect
                        parVideo_Language.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Video(i).Language
                        parVideo_LongLanguage.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Video(i).LongLanguage
                        parVideo_Bitrate.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Video(i).Bitrate
                        parVideo_MultiViewCount.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Video(i).MultiViewCount
                        parVideo_MultiViewLayout.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Video(i).MultiViewLayout
                        parVideo_FileSize.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Video(i).Filesize
                        parVideo_StereoMode.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Video(i).StereoMode

                        SQLcommandTVVStreams.ExecuteNonQuery()
                    Next
                End Using
                Using SQLcommandTVAStreams As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandTVAStreams.CommandText = String.Concat("DELETE FROM TVAStreams WHERE TVEpID = ", dbElement.ID, ";")
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

                    For i As Integer = 0 To dbElement.TVEpisode.FileInfo.StreamDetails.Audio.Count - 1
                        parAudio_EpID.Value = dbElement.ID
                        parAudio_StreamID.Value = i
                        parAudio_Language.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Audio(i).Language
                        parAudio_LongLanguage.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Audio(i).LongLanguage
                        parAudio_Codec.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Audio(i).Codec
                        parAudio_Channel.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Audio(i).Channels
                        parAudio_Bitrate.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Audio(i).Bitrate

                        SQLcommandTVAStreams.ExecuteNonQuery()
                    Next
                End Using

                'subtitles
                Using SQLcommandTVSubs As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommandTVSubs.CommandText = String.Concat("DELETE FROM TVSubs WHERE TVEpID = ", dbElement.ID, ";")
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
                    For i As Integer = 0 To dbElement.TVEpisode.FileInfo.StreamDetails.Subtitle.Count - 1
                        parSubs_EpID.Value = dbElement.ID
                        parSubs_StreamID.Value = iID
                        parSubs_Language.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Subtitle(i).Language
                        parSubs_LongLanguage.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Subtitle(i).LongLanguage
                        parSubs_Type.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Subtitle(i).SubsType
                        parSubs_Path.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Subtitle(i).SubsPath
                        parSubs_Forced.Value = dbElement.TVEpisode.FileInfo.StreamDetails.Subtitle(i).SubsForced
                        SQLcommandTVSubs.ExecuteNonQuery()
                        iID += 1
                    Next
                    'external subtitles
                    If dbElement.Subtitles IsNot Nothing Then
                        For i As Integer = 0 To dbElement.Subtitles.Count - 1
                            parSubs_EpID.Value = dbElement.ID
                            parSubs_StreamID.Value = iID
                            parSubs_Language.Value = dbElement.Subtitles(i).Language
                            parSubs_LongLanguage.Value = dbElement.Subtitles(i).LongLanguage
                            parSubs_Type.Value = dbElement.Subtitles(i).SubsType
                            parSubs_Path.Value = dbElement.Subtitles(i).SubsPath
                            parSubs_Forced.Value = dbElement.Subtitles(i).SubsForced
                            SQLcommandTVSubs.ExecuteNonQuery()
                            iID += 1
                        Next
                    End If
                End Using

                If bDoSeasonCheck Then
                    Using SQLSeasonCheck As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        SQLSeasonCheck.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0} AND Season = {1}", dbElement.ShowID, dbElement.TVEpisode.Season)
                        Using SQLreader As SQLiteDataReader = SQLSeasonCheck.ExecuteReader()
                            If Not SQLreader.HasRows Then
                                Dim _season As New DBElement(Enums.ContentType.TVSeason) With {.ShowID = dbElement.ShowID, .TVSeason = New MediaContainers.SeasonDetails With {.Season = dbElement.TVEpisode.Season}}
                                Save_TVSeason(_season, True, False, True)
                            End If
                        End Using
                    End Using
                End If
            End If
        End Using
        If Not bBatchMode Then SQLtransaction.Commit()

        If dbElement.FilenameIDSpecified AndAlso bDoSync Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Sync_TVEpisode, Nothing, Nothing, False, dbElement)
        End If

        Return dbElement
    End Function
    ''' <summary>
    ''' Stores information for a single season to the database
    ''' </summary>
    ''' <param name="dbElement">Database.DBElement representing the season to be stored.</param>
    ''' <param name="bBatchMode"></param>
    ''' <remarks>Note that this stores the season information, not the individual episodes within that season</remarks>
    Public Function Save_TVSeason(ByRef dbElement As DBElement, ByVal bBatchMode As Boolean, ByVal bToDisk As Boolean, ByVal bDoSync As Boolean) As DBElement
        If dbElement.TVSeason Is Nothing Then Return dbElement

        Dim doesExist As Boolean = False
        Dim ID As Long = -1

        Using SQLcommand_select_seasons As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_select_seasons.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0} AND Season = {1}", dbElement.ShowID, dbElement.TVSeason.Season)
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
                par_seasons_idShow.Value = dbElement.ShowID
                par_seasons_Season.Value = dbElement.TVSeason.Season
                par_seasons_SeasonText.Value = If(dbElement.TVSeason.TitleSpecified, dbElement.TVSeason.Title, StringUtils.FormatSeasonText(dbElement.TVSeason.Season))
                par_seasons_Lock.Value = dbElement.IsLock
                par_seasons_Mark.Value = dbElement.IsMark
                par_seasons_New.Value = True
                par_seasons_strTVDB.Value = dbElement.TVSeason.TVDB
                par_seasons_strTMDB.Value = dbElement.TVSeason.TMDB
                par_seasons_strAired.Value = dbElement.TVSeason.Aired
                par_seasons_strPlot.Value = dbElement.TVSeason.Plot
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
                par_seasons_SeasonText.Value = If(dbElement.TVSeason.TitleSpecified, dbElement.TVSeason.Title, StringUtils.FormatSeasonText(dbElement.TVSeason.Season))
                par_seasons_Lock.Value = dbElement.IsLock
                par_seasons_Mark.Value = dbElement.IsMark
                par_seasons_New.Value = False
                par_seasons_strTVDB.Value = dbElement.TVSeason.TVDB
                par_seasons_strTMDB.Value = dbElement.TVSeason.TMDB
                par_seasons_strAired.Value = dbElement.TVSeason.Aired
                par_seasons_strPlot.Value = dbElement.TVSeason.Plot
                SQLcommand_update_seasons.ExecuteNonQuery()
            End Using
        End If

        dbElement.ID = ID


        If bToDisk Then dbElement.ImagesContainer.SaveAllImages(dbElement, False)

        'Art
        RemoveArtFromItem(dbElement.ID, dbElement.ContentType)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Banner.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "banner", dbElement.ImagesContainer.Banner)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Fanart.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "fanart", dbElement.ImagesContainer.Fanart)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Landscape.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "landscape", dbElement.ImagesContainer.Landscape)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Poster.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "poster", dbElement.ImagesContainer.Poster)

        'UniqueIDs
        RemoveUniqueIDsFromItem(dbElement.ID, dbElement.ContentType)
        SetUniqueIDsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVSeason.UniqueIDs)

        If Not bBatchMode Then SQLtransaction.Commit()

        If bDoSync Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Sync_TVSeason, Nothing, Nothing, False, dbElement)
        End If

        Return dbElement
    End Function
    ''' <summary>
    ''' Saves all show information from a Database.DBElement object to the database
    ''' </summary>
    ''' <param name="dbElement">Database.DBElement object to save to the database</param>
    ''' <param name="bBatchMode">Is the function already part of a transaction?</param>
    ''' <param name="bToDisk">Create NFO and Images</param>
    Public Function Save_TVShow(ByRef dbElement As DBElement, ByVal bBatchMode As Boolean, ByVal bToNFO As Boolean, ByVal bToDisk As Boolean, ByVal bWithEpisodes As Boolean) As DBElement
        If dbElement.TVShow Is Nothing Then Return dbElement

        Dim SQLtransaction As SQLiteTransaction = Nothing

        If Not bBatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not dbElement.IDSpecified Then
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
                par_lngTVShowID.Value = dbElement.ID
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

            With dbElement.TVShow
                par_iUserRating.Value = .UserRating
                par_strEpisodeGuide.Value = .EpisodeGuide.URL
                par_strIMDB.Value = .IMDB
                par_strMPAA.Value = .MPAA
                par_strOriginalTitle.Value = .OriginalTitle
                par_strPlot.Value = .Plot
                par_strPremiered.Value = NumUtils.DateToISO8601Date(.Premiered)
                par_strRating.Value = .Rating
                par_strRuntime.Value = .Runtime
                par_strSortTitle.Value = .SortTitle
                par_strStatus.Value = .Status
                par_strTMDB.Value = .TMDB
                par_strTVDB.Value = .TVDB
                par_strTitle.Value = .Title
                par_strVotes.Value = .Votes
            End With

            'First let's save it to NFO, even because we will need the NFO path
            'Also Save Images to get ExtrafanartsPath
            'art Table be be linked later
            If bToNFO Then NFO.SaveToNFO_TVShow(dbElement)
            If bToDisk Then
                dbElement.ImagesContainer.SaveAllImages(dbElement, False)
                dbElement.Theme.SaveAllThemes(dbElement, False)
                dbElement.TVShow.SaveAllActorThumbs(dbElement)
            End If

            par_strExtrafanartsPath.Value = dbElement.ExtrafanartsPath
            par_strNfoPath.Value = dbElement.NfoPath
            par_strThemePath.Value = If(Not String.IsNullOrEmpty(dbElement.Theme.LocalFilePath), dbElement.Theme.LocalFilePath, String.Empty)
            par_strTVShowPath.Value = dbElement.ShowPath

            par_bNew.Value = Not dbElement.IDSpecified
            par_strListTitle.Value = dbElement.ListTitle
            par_bMark.Value = dbElement.IsMark
            par_bLock.Value = dbElement.IsLock
            par_lngTVSourceID.Value = dbElement.Source.ID
            par_strLanguage.Value = dbElement.Language
            par_iOrdering.Value = dbElement.Ordering
            par_iEpisodeSorting.Value = dbElement.EpisodeSorting

            If Not dbElement.IDSpecified Then
                If Master.eSettings.TVGeneralMarkNewShows Then
                    par_bMark.Value = True
                    dbElement.IsMark = True
                End If
                Using rdrTVShow As SQLiteDataReader = SQLcommand.ExecuteReader()
                    If rdrTVShow.Read Then
                        dbElement.ID = Convert.ToInt64(rdrTVShow(0))
                        dbElement.ShowID = dbElement.ID
                    Else
                        logger.Error("Something very wrong here: SaveTVShowToDB", dbElement.ToString, "Error")
                        dbElement.ID = -1
                        dbElement.ShowID = dbElement.ID
                        Return dbElement
                        Exit Function
                    End If
                End Using
            Else
                SQLcommand.ExecuteNonQuery()
            End If

            If Not dbElement.ID = -1 Then

                'Actors
                RemoveActorsFromItem(dbElement.ID, dbElement.ContentType)
                SetActorsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.Actors)

                'Art
                RemoveArtFromItem(dbElement.ID, dbElement.ContentType)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Banner.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "banner", dbElement.ImagesContainer.Banner)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.CharacterArt.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "characterart", dbElement.ImagesContainer.CharacterArt)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.ClearArt.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "clearart", dbElement.ImagesContainer.ClearArt)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.ClearLogo.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "clearlogo", dbElement.ImagesContainer.ClearLogo)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Fanart.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "fanart", dbElement.ImagesContainer.Fanart)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Landscape.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "landscape", dbElement.ImagesContainer.Landscape)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Poster.LocalFilePath) Then SetArtForItem(dbElement.ID, dbElement.ContentType, "poster", dbElement.ImagesContainer.Poster)

                'Certifications
                RemoveCertificationsFromItem(dbElement.ID, dbElement.ContentType)
                SetCertificationsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.Certifications)

                'Creators
                RemoveCreatorsFromItem(dbElement.ID, dbElement.ContentType)
                SetCreatorsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.Creators)

                'Countries
                RemoveCountriesFromItem(dbElement.ID, dbElement.ContentType)
                SetCountriesForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.Countries)

                'Genres
                RemoveGenresFromItem(dbElement.ID, dbElement.ContentType)
                SetGenresForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.Genres)

                'Studios
                RemoveStudiosFromItem(dbElement.ID, dbElement.ContentType)
                SetStudiosForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.Studios)

                'UniqueIDs
                RemoveUniqueIDsFromItem(dbElement.ID, dbElement.ContentType)
                SetUniqueIDsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.UniqueIDs)

                'Tags
                RemoveTagsFromItem(dbElement.ID, dbElement.ContentType)
                SetTagsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.Tags)
            End If
        End Using

        'save season informations
        If dbElement.SeasonsSpecified Then
            For Each nSeason As DBElement In dbElement.Seasons
                Save_TVSeason(nSeason, True, True, True)
            Next
            Delete_Invalid_TVSeasons(dbElement.Seasons, dbElement.ID, True)
        End If

        'save episode informations
        If bWithEpisodes AndAlso dbElement.EpisodesSpecified Then
            For Each nEpisode As DBElement In dbElement.Episodes
                Save_TVEpisode(nEpisode, True, True, True, False, True)
            Next
            Delete_Invalid_TVEpisodes(dbElement.Episodes, dbElement.ID, True)
        End If

        'delete empty seasons after saving all known episodes
        Delete_Empty_TVSeasons(dbElement.ID, True)

        If Not bBatchMode Then SQLtransaction.Commit()

        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Sync_TVShow, Nothing, Nothing, False, dbElement)

        Return dbElement
    End Function

    Private Sub SetArtForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal artType As String, ByVal imagecontainer As MediaContainers.Image)
        If imagecontainer.LocalFilePathSpecified Then
            Dim iHeight As Integer
            Dim iWidth As Integer
            Integer.TryParse(imagecontainer.Height, iHeight)
            Integer.TryParse(imagecontainer.Width, iWidth)
            AddArt(idMedia, contentType, artType, imagecontainer.LocalFilePath, iWidth, iHeight)
        End If
    End Sub

    Private Sub SetActorsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal actors As List(Of MediaContainers.Person))
        Dim iOrder As Integer = 0
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As MediaContainers.Person In actors
                AddToPersonLinkTable("actor_link", AddPerson(entry.Name, entry.URLOriginal, entry.LocalFilePath, entry.IMDB, entry.TMDB, True), idMedia, mediaType, entry.Role, iOrder)
                iOrder += 1
            Next
        End If
    End Sub

    Private Sub SetCertificationsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal certifications As List(Of String))
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As String In certifications
                AddToLinkTable("certification_link", "idCertification", AddCertification(entry), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

    Private Sub SetCountriesForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal countries As List(Of String))
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As String In countries
                AddToLinkTable("country_link", "idCountry", AddCountry(entry), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

    Private Sub SetCreatorsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal creators As List(Of String))
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As String In creators
                AddToLinkTable("creator_link", "idActor", AddPerson(entry, "", "", "", "", False), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

    Private Sub SetDirectorsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal directors As List(Of String))
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As String In directors
                AddToLinkTable("director_link", "idActor", AddPerson(entry, "", "", "", "", False), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

    Private Sub SetGenresForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal genres As List(Of String))
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As String In genres
                AddToLinkTable("genre_link", "idGenre", AddGenre(entry), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

    Private Sub SetGuestStarsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal gueststars As List(Of MediaContainers.Person))
        Dim iOrder As Integer = 0
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As MediaContainers.Person In gueststars
                AddToPersonLinkTable("gueststar_link", AddPerson(entry.Name, entry.URLOriginal, entry.LocalFilePath, entry.IMDB, entry.TMDB, True), idMedia, mediaType, entry.Role, iOrder)
                iOrder += 1
            Next
        End If
    End Sub

    Private Sub SetStudiosForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal studios As List(Of String))
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As String In studios
                AddToLinkTable("studio_link", "idStudio", AddStudio(entry), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

    Private Sub SetTagsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal tags As List(Of String))
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As String In tags
                AddToLinkTable("tag_link", "idTag", AddTag(entry), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

    Private Sub SetUniqueIDsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal uniqueids As List(Of MediaContainers.Uniqueid))
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As MediaContainers.Uniqueid In uniqueids
                AddUniqueID(idMedia, mediaType, entry)
            Next
        End If
    End Sub

    Private Sub SetWritersForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal writers As List(Of String))
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As String In writers
                AddToLinkTable("writer_link", "idActor", AddPerson(entry, "", "", "", "", False), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

#End Region 'Methods

#Region "Database upgrade Methodes"

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
                                        logger.Info(String.Concat(Trans.name, ":   ", _cmd.description))
                                        SQLcommand.ExecuteNonQuery()
                                    Catch ex As Exception
                                        TransOk = False
                                        logger.Error(New StackFrame().GetMethod().Name, ex, Trans.name, _cmd.description)
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
                        Patch14_country("idMovie", "movie", True)
                        Patch14_director("idEpisode", "episode", True)
                        Patch14_director("idMovie", "movie", True)
                        Patch14_genre("idMovie", "movie", True)
                        Patch14_genre("idShow", "tvshow", True)
                        Patch14_studio("idMovie", "movie", True)
                        Patch14_studio("idShow", "tvshow", True)
                        Patch14_writer("idEpisode", "episode", True)
                        Patch14_writer("idMovie", "movie", True)
                        Patch14_playcounts("episode", True)
                        Patch14_playcounts("movie", True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 18
                        Patch18_VotesCount("idEpisode", "episode", True)
                        Patch18_VotesCount("idMovie", "movie", True)
                        Patch18_VotesCount("idShow", "tvshow", True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 21
                        Patch21_SortTitle("tvshow", True)
                        Patch21_DisplayEpisodeSeason(True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 26
                        Patch26_EFanartsPath("idMovie", "movie", True)
                        Patch26_EThumbsPath("idMovie", "movie", True)
                        Patch26_EFanartsPath("idShow", "tvshow", True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 30
                        Patch30and31_Language("movie", True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 31
                        Patch30and31_Language("sets", True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 40
                        Patch40_IMDB(True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 41
                        Patch41_Top250(True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 42
                        Patch42_OrphanedLinks(True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 43
                        If MessageBox.Show("Locked state will now be saved in NFO. Do you want to rewrite all NFOs of locked items?", "Rewrite NFOs of locked items", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Patch43_LockedStateToNFO(True)
                        End If
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 44
                        Patch44_Sources("moviesource", True)
                        Patch44_Sources("tvshowsource", True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 45
                        Patch45_AllSeasonsEntries(True)
                End Select

                SQLtransaction.Commit()
            End Using

            Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 47
                        Patch47_certification_temp(True)
                        Patch47_file_temp(True)
                        If MessageBox.Show("Ember now saves the resolution of all images in the database. Do you want to scan all images (all sources has to be mountet for this)?", "Get resolution of images", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Patch47_art(True)
                        End If
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
    Private Sub Patch_MyVideos(ByVal cPath As String, ByVal nPath As String, ByVal cVersion As Integer, ByVal nVersion As Integer)

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

    Private Sub Patch14_country(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
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

#Disable Warning BC40000 ' The type or member is obsolete.
                        For Each value As String In valuelist
                            Select Case table
                                Case "movie"
                                    AddCountryToMovie(idMedia, AddCountry(value))
                            End Select
                        Next
#Enable Warning BC40000 ' The type or member is obsolete.
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch14_director(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
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

#Disable Warning BC40000 ' The type or member is obsolete.
                        For Each value As String In valuelist
                            Select Case table
                                Case "episode"
                                    AddDirectorToTVEpisode(idMedia, AddPerson(value, "", "", "", "", False))
                                Case "movie"
                                    AddDirectorToMovie(idMedia, AddPerson(value, "", "", "", "", False))
                                Case "tvshow"
                                    AddDirectorToTvShow(idMedia, AddPerson(value, "", "", "", "", False))
                            End Select
                        Next
#Enable Warning BC40000 ' The type or member is obsolete.
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch14_genre(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
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

#Disable Warning BC40000 ' The type or member is obsolete.
                        For Each value As String In valuelist
                            Select Case table
                                Case "movie"
                                    AddGenreToMovie(idMedia, AddGenre(value))
                                Case "tvshow"
                                    AddGenreToTvShow(idMedia, AddGenre(value))
                            End Select
                        Next
#Enable Warning BC40000 ' The type or member is obsolete.
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch14_playcounts(ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing Playcounts...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("UPDATE {0} SET Playcount = NULL WHERE Playcount = 0 OR Playcount = """";", table)
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch14_studio(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
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

#Disable Warning BC40000 ' The type or member is obsolete.
                        For Each value As String In valuelist
                            Select Case table
                                Case "movie"
                                    AddStudioToMovie(idMedia, AddStudio(value))
                                Case "tvshow"
                                    AddStudioToTvShow(idMedia, AddStudio(value))
                            End Select
                        Next
#Enable Warning BC40000 ' The type or member is obsolete.
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch14_writer(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
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

#Disable Warning BC40000 ' The type or member is obsolete.
                        For Each value As String In valuelist
                            Select Case table
                                Case "episode"
                                    AddWriterToTVEpisode(idMedia, AddPerson(value, "", "", "", "", False))
                                Case "movie"
                                    AddWriterToMovie(idMedia, AddPerson(value, "", "", "", "", False))
                            End Select
                        Next
#Enable Warning BC40000 ' The type or member is obsolete.
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch18_VotesCount(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
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

    Private Sub Patch21_DisplayEpisodeSeason(ByVal BatchMode As Boolean)
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

    Private Sub Patch21_SortTitle(ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing SortTitles...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("UPDATE {0} SET SortTitle = '' WHERE SortTitle IS NULL OR SortTitle = """";", table)
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch26_EFanartsPath(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
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

    Private Sub Patch26_EThumbsPath(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
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

    Private Sub Patch30and31_Language(ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Set all languages to ""en-US"" ...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("UPDATE {0} SET Language = 'en-US';", table)
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch40_IMDB(ByVal BatchMode As Boolean)
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

    Private Sub Patch41_Top250(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing Top250...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "UPDATE movie SET Top250 = NULL WHERE Top250 = 0 OR Top250 = """";"
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch42_OrphanedLinks(ByVal BatchMode As Boolean)
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

    Private Sub Patch43_LockedStateToNFO(ByVal BatchMode As Boolean)
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

        'TVEpisodes
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

    Private Sub Patch44_Sources(ByVal table As String, ByVal BatchMode As Boolean)
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

    Private Sub Patch45_AllSeasonsEntries(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing ""* All Seasons"" entries...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "UPDATE seasons SET Season = -1 WHERE Season = 999;"
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch47_art(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Get size of all images...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT idArt, url FROM art;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("url")) AndAlso Not String.IsNullOrEmpty(SQLreader("url").ToString) Then
                        Dim idArt As Long = Convert.ToInt64(SQLreader("idArt"))
                        Dim strValue As String = SQLreader("url").ToString
                        If File.Exists(strValue) Then
                            Dim nImage As New Drawing.Bitmap(strValue)
                            Dim iWidth = nImage.Width
                            Dim iHeight = nImage.Height
                            nImage.Dispose()
                            Using sqlCommand_update As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                sqlCommand_update.CommandText = String.Format("UPDATE art SET width={0}, height={1} WHERE idArt={2};", iWidth, iHeight, idArt)
                                sqlCommand_update.ExecuteNonQuery()
                            End Using
                        End If
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch47_certification_temp(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Move certifications...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT * FROM certification_temp;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("name")) AndAlso Not String.IsNullOrEmpty(SQLreader("name").ToString) Then
                        Dim valuelist As New List(Of String)
                        Dim strValue As String = SQLreader("name").ToString
                        Dim idMedia As Long = Convert.ToInt64(SQLreader("idMedia"))

                        If strValue.Contains(" / ") Then
                            Dim values As String() = Regex.Split(strValue, " / ")
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
                        SetCertificationsForItem(idMedia, Enums.ContentType.Movie, valuelist)
                    End If
                End While
            End Using
        End Using

        'delete temporary table "certification_temp"
        Using SQLCommand_drop_table As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLCommand_drop_table.CommandText = String.Concat("DROP TABLE certification_temp;")
            SQLCommand_drop_table.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch47_file_temp(ByVal batchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Move movie paths to table file...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT * FROM file_temp;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("path")) AndAlso Not String.IsNullOrEmpty(SQLreader("path").ToString) Then
                        Dim strPath As String = SQLreader("path").ToString
                        Dim idMedia As Long = Convert.ToInt64(SQLreader("idMovie"))
                        Dim idFile = AddFileinfo(-1, strPath, 0)
                        Using sqlCommand_update As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand_update.CommandText = String.Format("UPDATE movie SET idFile={0} WHERE idMovie={1};", idFile, idMedia)
                            sqlCommand_update.ExecuteNonQuery()
                        End Using
                    End If
                End While
            End Using
        End Using

        'delete temporary table "file_temp"
        Using sqlCommand_drop_table As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand_drop_table.CommandText = String.Concat("DROP TABLE file_temp;")
            sqlCommand_drop_table.ExecuteNonQuery()
        End Using

        If Not batchMode Then sqlTransaction.Commit()
    End Sub

#End Region 'Database upgrade Methodes

#Region "Deprecated Methodes"

    <Obsolete("This method is deprecated and only to use for database upgrade, use AddCountryToItem instead.")>
    Private Sub AddCountryToMovie(ByVal idMovie As Long, ByVal idCountry As Long)
        AddToLinkTable("countrylinkmovie", "idCountry", idCountry, "idMovie", idMovie, "", "")
    End Sub

    <Obsolete("This method is deprecated and only to use for database upgrade, use AddDirectorToItem instead.")>
    Private Sub AddDirectorToMovie(ByVal idMovie As Long, ByVal idDirector As Long)
        AddToLinkTable("directorlinkmovie", "idDirector", idDirector, "idMovie", idMovie, "", "")
    End Sub

    <Obsolete("This method is deprecated and only to use for database upgrade, use AddDirectorToItem instead.")>
    Private Sub AddDirectorToTVEpisode(ByVal idEpisode As Long, ByVal idDirector As Long)
        AddToLinkTable("directorlinkepisode", "idDirector", idDirector, "idEpisode", idEpisode, "", "")
    End Sub

    <Obsolete("This method is deprecated and only to use for database upgrade, use AddDirectorToItem instead.")>
    Private Sub AddDirectorToTvShow(ByVal idShow As Long, ByVal idDirector As Long)
        AddToLinkTable("directorlinktvshow", "idDirector", idDirector, "idShow", idShow, "", "")
    End Sub

    <Obsolete("This method is deprecated and only to use for database upgrade, use AddGenreToItem instead.")>
    Private Sub AddGenreToMovie(ByVal idMovie As Long, ByVal idGenre As Long)
        AddToLinkTable("genrelinkmovie", "idGenre", idGenre, "idMovie", idMovie, "", "")
    End Sub

    <Obsolete("This method is deprecated and only to use for database upgrade, use AddGenreToItem instead.")>
    Private Sub AddGenreToTvShow(ByVal idShow As Long, ByVal idGenre As Long)
        AddToLinkTable("genrelinktvshow", "idGenre", idGenre, "idShow", idShow, "", "")
    End Sub

    <Obsolete("This method is deprecated and only to use for database upgrade, use AddStudioToItem instead.")>
    Private Sub AddStudioToMovie(ByVal idMovie As Long, ByVal idStudio As Long)
        AddToLinkTable("studiolinkmovie", "idStudio", idStudio, "idMovie", idMovie, "", "")
    End Sub

    <Obsolete("This method is deprecated and only to use for database upgrade, use AddStudioToItem instead.")>
    Private Sub AddStudioToTvShow(ByVal idShow As Long, ByVal idStudio As Long)
        AddToLinkTable("studiolinktvshow", "idStudio", idStudio, "idShow", idShow, "", "")
    End Sub

    <Obsolete("This method is deprecated and only to use for database upgrade, use AddWriterToItem instead.")>
    Private Sub AddWriterToMovie(ByVal idMovie As Long, ByVal idWriter As Long)
        AddToLinkTable("writerlinkmovie", "idWriter", idWriter, "idMovie", idMovie, "", "")
    End Sub

    <Obsolete("This method is deprecated and only to use for database upgrade, use AddWriterToItem instead.")>
    Private Sub AddWriterToTVEpisode(ByVal idEpisode As Long, ByVal idWriter As Long)
        AddToLinkTable("writerlinkepisode", "idWriter", idWriter, "idEpisode", idEpisode, "", "")
    End Sub

#End Region 'Deprecated Methodes

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

        Private _actorthumbs As New List(Of String)
        Private _contenttype As Enums.ContentType
        Private _dateadded As Long
        Private _datemodified As Long
        Private _episodes As New List(Of DBElement)
        Private _episodesorting As Enums.EpisodeSorting
        Private _extrafanartspath As String
        Private _extrathumbspath As String
        Private _filename As String
        Private _filenameid As Long
        Private _id As Long
        Private _imagescontainer As New MediaContainers.ImagesContainer
        Private _islock As Boolean
        Private _ismark As Boolean
        Private _ismarkcustom1 As Boolean
        Private _ismarkcustom2 As Boolean
        Private _ismarkcustom3 As Boolean
        Private _ismarkcustom4 As Boolean
        Private _isonline As Boolean
        Private _issingle As Boolean
        Private _language As String
        Private _listtitle As String
        Private _movie As MediaContainers.Movie
        Private _movieset As MediaContainers.MovieSet
        Private _moviesinset As List(Of MediaContainers.MovieInSet)
        Private _nfopath As String
        Private _ordering As Enums.EpisodeOrdering
        Private _outoftolerance As Boolean
        Private _seasons As New List(Of DBElement)
        Private _showid As Long
        Private _showpath As String
        Private _sortmethod As Enums.SortMethod_MovieSet
        Private _source As New DBSource
        Private _subtitles As New List(Of MediaContainers.Subtitle)
        Private _theme As New MediaContainers.Theme
        Private _trailer As New MediaContainers.Trailer
        Private _tvepisode As MediaContainers.EpisodeDetails
        Private _tvseason As MediaContainers.SeasonDetails
        Private _tvshow As MediaContainers.TVShow
        Private _videosource As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal ContentType As Enums.ContentType)
            Clear()
            _contenttype = ContentType
            Select Case ContentType
                Case Enums.ContentType.Movie
                    _movie = New MediaContainers.Movie
                Case Enums.ContentType.MovieSet
                    _movieset = New MediaContainers.MovieSet
                Case Enums.ContentType.TVEpisode
                    _tvepisode = New MediaContainers.EpisodeDetails
                Case Enums.ContentType.TVSeason
                    _tvseason = New MediaContainers.SeasonDetails
                Case Enums.ContentType.TVShow
                    _tvshow = New MediaContainers.TVShow
            End Select
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property ActorThumbs() As List(Of String)
            Get
                Return _actorthumbs
            End Get
            Set(ByVal value As List(Of String))
                _actorthumbs = value
            End Set
        End Property

        Public ReadOnly Property ActorThumbsSpecified() As Boolean
            Get
                Return _actorthumbs.Count > 0
            End Get
        End Property

        Public ReadOnly Property ContentType() As Enums.ContentType
            Get
                Return _contenttype
            End Get
        End Property

        Public Property DateAdded() As Long
            Get
                Return _dateadded
            End Get
            Set(ByVal value As Long)
                _dateadded = value
            End Set
        End Property

        Public Property DateModified() As Long
            Get
                Return _datemodified
            End Get
            Set(ByVal value As Long)
                _datemodified = value
            End Set
        End Property

        Public Property Episodes() As List(Of DBElement)
            Get
                Return _episodes
            End Get
            Set(ByVal value As List(Of DBElement))
                _episodes = value
            End Set
        End Property

        Public ReadOnly Property EpisodesSpecified() As Boolean
            Get
                Return _episodes.Count > 0
            End Get
        End Property

        Public Property EpisodeSorting() As Enums.EpisodeSorting
            Get
                Return _episodesorting
            End Get
            Set(ByVal value As Enums.EpisodeSorting)
                _episodesorting = value
            End Set
        End Property

        Public Property ExtrafanartsPath() As String
            Get
                Return _extrafanartspath
            End Get
            Set(ByVal value As String)
                _extrafanartspath = value
            End Set
        End Property

        Public ReadOnly Property ExtrafanartsPathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_extrafanartspath)
            End Get
        End Property

        Public Property ExtrathumbsPath() As String
            Get
                Return _extrathumbspath
            End Get
            Set(ByVal value As String)
                _extrathumbspath = value
            End Set
        End Property

        Public ReadOnly Property ExtrathumbsPathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_extrathumbspath)
            End Get
        End Property

        Public Property Filename() As String
            Get
                Return _filename
            End Get
            Set(ByVal value As String)
                _filename = value
            End Set
        End Property

        Public ReadOnly Property FilenameSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_filename)
            End Get
        End Property

        Public Property FilenameID() As Long
            Get
                Return _filenameid
            End Get
            Set(ByVal value As Long)
                _filenameid = value
            End Set
        End Property

        Public ReadOnly Property FilenameIDSpecified() As Boolean
            Get
                Return Not _filenameid = -1
            End Get
        End Property

        Public Property ID() As Long
            Get
                Return _id
            End Get
            Set(ByVal value As Long)
                _id = value
            End Set
        End Property

        Public ReadOnly Property IDSpecified() As Boolean
            Get
                Return Not _id = -1
            End Get
        End Property

        Public Property ImagesContainer() As MediaContainers.ImagesContainer
            Get
                Return _imagescontainer
            End Get
            Set(ByVal value As MediaContainers.ImagesContainer)
                _imagescontainer = value
            End Set
        End Property

        Public Property IsLock() As Boolean
            Get
                Return _islock
            End Get
            Set(ByVal value As Boolean)
                _islock = value
                Select Case _contenttype
                    Case Enums.ContentType.Movie
                        If _movie IsNot Nothing Then _movie.Locked = value
                    Case Enums.ContentType.MovieSet
                        If _movieset IsNot Nothing Then _movieset.Locked = value
                    Case Enums.ContentType.TVEpisode
                        If _tvepisode IsNot Nothing Then _tvepisode.Locked = value
                    Case Enums.ContentType.TVSeason
                        If _tvseason IsNot Nothing Then _tvseason.Locked = value
                    Case Enums.ContentType.TVShow
                        If _tvshow IsNot Nothing Then _tvshow.Locked = value
                End Select
            End Set
        End Property

        Public Property IsMark() As Boolean
            Get
                Return _ismark
            End Get
            Set(ByVal value As Boolean)
                _ismark = value
            End Set
        End Property

        Public Property IsMarkCustom1() As Boolean
            Get
                Return _ismarkcustom1
            End Get
            Set(ByVal value As Boolean)
                _ismarkcustom1 = value
            End Set
        End Property

        Public Property IsMarkCustom2() As Boolean
            Get
                Return _ismarkcustom2
            End Get
            Set(ByVal value As Boolean)
                _ismarkcustom2 = value
            End Set
        End Property

        Public Property IsMarkCustom3() As Boolean
            Get
                Return _ismarkcustom3
            End Get
            Set(ByVal value As Boolean)
                _ismarkcustom3 = value
            End Set
        End Property

        Public Property IsMarkCustom4() As Boolean
            Get
                Return _ismarkcustom4
            End Get
            Set(ByVal value As Boolean)
                _ismarkcustom4 = value
            End Set
        End Property

        Public Property IsOnline() As Boolean
            Get
                Return _isonline
            End Get
            Set(ByVal value As Boolean)
                _isonline = value
            End Set
        End Property

        Public Property IsSingle() As Boolean
            Get
                Return _issingle
            End Get
            Set(ByVal value As Boolean)
                _issingle = value
            End Set
        End Property

        Public Property Language() As String
            Get
                Return _language
            End Get
            Set(ByVal value As String)
                _language = value
            End Set
        End Property

        Public ReadOnly Property LanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_language)
            End Get
        End Property

        Public ReadOnly Property Language_Main() As String
            Get
                Return Regex.Replace(_language, "-.*", String.Empty).Trim
            End Get
        End Property

        Public Property ListTitle() As String
            Get
                Return _listtitle
            End Get
            Set(ByVal value As String)
                _listtitle = value
            End Set
        End Property

        Public ReadOnly Property ListTitleSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_listtitle)
            End Get
        End Property

        Public Property Movie() As MediaContainers.Movie
            Get
                Return _movie
            End Get
            Set(ByVal value As MediaContainers.Movie)
                _movie = value
            End Set
        End Property

        Public ReadOnly Property MovieSpecified() As Boolean
            Get
                Return _movie IsNot Nothing
            End Get
        End Property

        Public Property MoviesInSet() As List(Of MediaContainers.MovieInSet)
            Get
                Return _moviesinset
            End Get
            Set(ByVal value As List(Of MediaContainers.MovieInSet))
                _moviesinset = value
            End Set
        End Property

        Public ReadOnly Property MoviesInSetSpecified() As Boolean
            Get
                Return _moviesinset.Count > 0
            End Get
        End Property

        Public Property MovieSet() As MediaContainers.MovieSet
            Get
                Return _movieset
            End Get
            Set(ByVal value As MediaContainers.MovieSet)
                _movieset = value
            End Set
        End Property

        Public ReadOnly Property MovieSetSpecified() As Boolean
            Get
                Return _movieset IsNot Nothing
            End Get
        End Property

        Public Property NfoPath() As String
            Get
                Return _nfopath
            End Get
            Set(ByVal value As String)
                _nfopath = value
            End Set
        End Property

        Public ReadOnly Property NfoPathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_nfopath)
            End Get
        End Property

        Public Property Ordering() As Enums.EpisodeOrdering
            Get
                Return _ordering
            End Get
            Set(ByVal value As Enums.EpisodeOrdering)
                _ordering = value
            End Set
        End Property

        Public Property OutOfTolerance() As Boolean
            Get
                Return _outoftolerance
            End Get
            Set(ByVal value As Boolean)
                _outoftolerance = value
            End Set
        End Property

        Public Property Seasons() As List(Of DBElement)
            Get
                Return _seasons
            End Get
            Set(ByVal value As List(Of DBElement))
                _seasons = value
            End Set
        End Property

        Public ReadOnly Property SeasonsSpecified() As Boolean
            Get
                Return _seasons.Count > 0
            End Get
        End Property

        Public Property ShowID() As Long
            Get
                Return _showid
            End Get
            Set(ByVal value As Long)
                _showid = value
            End Set
        End Property

        Public ReadOnly Property ShowIDSpecified() As Boolean
            Get
                Return Not _showid = -1
            End Get
        End Property

        Public Property ShowPath() As String
            Get
                Return _showpath
            End Get
            Set(ByVal value As String)
                _showpath = value
            End Set
        End Property

        Public ReadOnly Property ShowPathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_showpath)
            End Get
        End Property

        Public Property SortMethod() As Enums.SortMethod_MovieSet
            Get
                Return _sortmethod
            End Get
            Set(ByVal value As Enums.SortMethod_MovieSet)
                _sortmethod = value
            End Set
        End Property

        Public Property Source() As DBSource
            Get
                Return _source
            End Get
            Set(ByVal value As DBSource)
                _source = value
            End Set
        End Property

        Public ReadOnly Property SourceSpecified() As Boolean
            Get
                Return Not _source.ID = -1
            End Get
        End Property

        Public Property Subtitles() As List(Of MediaContainers.Subtitle)
            Get
                Return _subtitles
            End Get
            Set(ByVal value As List(Of MediaContainers.Subtitle))
                _subtitles = value
            End Set
        End Property

        Public ReadOnly Property SubtitlesSpecified() As Boolean
            Get
                Return _subtitles.Count > 0
            End Get
        End Property

        Public Property Theme() As MediaContainers.Theme
            Get
                Return _theme
            End Get
            Set(ByVal value As MediaContainers.Theme)
                _theme = value
            End Set
        End Property

        Public ReadOnly Property ThemeSpecified() As Boolean
            Get
                Return _theme.ThemeOriginal IsNot Nothing AndAlso _theme.ThemeOriginal.hasMemoryStream
            End Get
        End Property

        Public Property Trailer() As MediaContainers.Trailer
            Get
                Return _trailer
            End Get
            Set(ByVal value As MediaContainers.Trailer)
                _trailer = value
            End Set
        End Property

        Public ReadOnly Property TrailerSpecified() As Boolean
            Get
                Return _trailer.TrailerOriginal IsNot Nothing AndAlso _trailer.TrailerOriginal.hasMemoryStream
            End Get
        End Property

        Public Property TVEpisode() As MediaContainers.EpisodeDetails
            Get
                Return _tvepisode
            End Get
            Set(ByVal value As MediaContainers.EpisodeDetails)
                _tvepisode = value
            End Set
        End Property

        Public ReadOnly Property TVEpisodeSpecified() As Boolean
            Get
                Return _tvepisode IsNot Nothing
            End Get
        End Property

        Public Property TVSeason() As MediaContainers.SeasonDetails
            Get
                Return _tvseason
            End Get
            Set(ByVal value As MediaContainers.SeasonDetails)
                _tvseason = value
            End Set
        End Property

        Public ReadOnly Property TVSeasonSpecified() As Boolean
            Get
                Return _tvseason IsNot Nothing
            End Get
        End Property

        Public Property TVShow() As MediaContainers.TVShow
            Get
                Return _tvshow
            End Get
            Set(ByVal value As MediaContainers.TVShow)
                _tvshow = value
            End Set
        End Property

        Public ReadOnly Property TVShowSpecified() As Boolean
            Get
                Return _tvshow IsNot Nothing
            End Get
        End Property

        Public Property VideoSource() As String
            Get
                Return _videosource
            End Get
            Set(ByVal value As String)
                _videosource = value
            End Set
        End Property

        Public ReadOnly Property VideoSourceSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_videosource)
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _actorthumbs = New List(Of String)
            _dateadded = -1
            _datemodified = -1
            _episodes = New List(Of DBElement)
            _episodesorting = Enums.EpisodeSorting.Episode
            _extrafanartspath = String.Empty
            _extrathumbspath = String.Empty
            _filename = String.Empty
            _filenameid = -1
            _id = -1
            _imagescontainer = New MediaContainers.ImagesContainer
            _islock = False
            _ismark = False
            _isonline = False
            _issingle = False
            _language = String.Empty
            _listtitle = String.Empty
            _movie = Nothing
            _movieset = Nothing
            _moviesinset = New List(Of MediaContainers.MovieInSet)
            _nfopath = String.Empty
            _ordering = Enums.EpisodeOrdering.Standard
            _outoftolerance = False
            _seasons = New List(Of DBElement)
            _showid = -1
            _showpath = String.Empty
            _sortmethod = Enums.SortMethod_MovieSet.Year
            _source = New DBSource
            _subtitles = New List(Of MediaContainers.Subtitle)
            _theme = New MediaContainers.Theme
            _trailer = New MediaContainers.Trailer
            _tvepisode = Nothing
            _tvseason = Nothing
            _tvshow = Nothing
            _videosource = String.Empty
        End Sub

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

#Region "Fields"

        Private _episodeordering As Enums.EpisodeOrdering
        Private _episodesorting As Enums.EpisodeSorting
        Private _exclude As Boolean
        Private _getyear As Boolean
        Private _id As Long
        Private _issingle As Boolean
        Private _language As String
        Private _lastscan As String
        Private _name As String
        Private _path As String
        Private _scanrecursive As Boolean
        Private _usefoldername As Boolean

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property EpisodeOrdering() As Enums.EpisodeOrdering
            Get
                Return _episodeordering
            End Get
            Set(ByVal value As Enums.EpisodeOrdering)
                _episodeordering = value
            End Set
        End Property

        Public Property EpisodeSorting() As Enums.EpisodeSorting
            Get
                Return _episodesorting
            End Get
            Set(ByVal value As Enums.EpisodeSorting)
                _episodesorting = value
            End Set
        End Property

        Public Property Exclude() As Boolean
            Get
                Return _exclude
            End Get
            Set(ByVal value As Boolean)
                _exclude = value
            End Set
        End Property

        Public Property GetYear() As Boolean
            Get
                Return _getyear
            End Get
            Set(ByVal value As Boolean)
                _getyear = value
            End Set
        End Property

        Public Property ID() As Long
            Get
                Return _id
            End Get
            Set(ByVal value As Long)
                _id = value
            End Set
        End Property

        Public ReadOnly Property IDSpecified() As Boolean
            Get
                Return Not _id = -1
            End Get
        End Property

        Public Property IsSingle() As Boolean
            Get
                Return _issingle
            End Get
            Set(ByVal value As Boolean)
                _issingle = value
            End Set
        End Property

        Public Property Language() As String
            Get
                Return _language
            End Get
            Set(ByVal value As String)
                _language = value
            End Set
        End Property

        Public ReadOnly Property LanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_language)
            End Get
        End Property

        Public Property LastScan() As String
            Get
                Return _lastscan
            End Get
            Set(ByVal value As String)
                _lastscan = value
            End Set
        End Property

        Public ReadOnly Property LastScanSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_lastscan)
            End Get
        End Property

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public ReadOnly Property NameSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_name)
            End Get
        End Property

        Public Property Path() As String
            Get
                Return _path
            End Get
            Set(ByVal value As String)
                _path = value
            End Set
        End Property

        Public ReadOnly Property PathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(_path)
            End Get
        End Property

        Public Property ScanRecursive() As Boolean
            Get
                Return _scanrecursive
            End Get
            Set(ByVal value As Boolean)
                _scanrecursive = value
            End Set
        End Property

        Public Property UseFolderName() As Boolean
            Get
                Return _usefoldername
            End Get
            Set(ByVal value As Boolean)
                _usefoldername = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _episodeordering = Enums.EpisodeOrdering.Standard
            _episodesorting = Enums.EpisodeSorting.Episode
            _exclude = False
            _getyear = False
            _id = -1
            _issingle = False
            _language = String.Empty
            _lastscan = String.Empty
            _name = String.Empty
            _path = String.Empty
            _scanrecursive = False
            _usefoldername = False
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class