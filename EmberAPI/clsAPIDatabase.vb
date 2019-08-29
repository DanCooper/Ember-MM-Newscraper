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

#Region "Enumerations"

    Public Enum ColumnType As Integer
        Banner
        CharacterArt
        ClearArt
        ClearLogo
        DiscArt
        Extrafanarts
        Extrathumbs
        Fanart
        KeyArt
        Landscape
        MetaData
        Movieset
        NFO
        Poster
        Rating
        Theme
        Trailer
        Subtitle
        Unknown
        UserRating
        WatchedState
    End Enum

    Public Enum ColumnName As Integer
        ActorName
        Aired
        AudioBitrate
        AudioChannels
        AudioCodec
        AudioLanguage
        BannerPath
        Certifications
        CharacterArtPath
        ClearArtPath
        ClearLogoPath
        Countries
        Creators
        Credits
        DateAdded
        DateModified
        Directors
        DiscArtPath
        DisplayEpisode
        DisplaySeason
        EpisodeCount
        EpisodeGuideURL
        EpisodeNumber
        EpisodeOrdering
        EpisodeSorting
        Exclude
        ExtrafanartsPath
        ExtrathumbsPath
        FanartPath
        FileSize
        Genres
        HasMetaData
        HasMovieset
        HasSubtitles
        HasWatched
        idEpisode
        idMedia
        idMovie
        idPerson
        idSeason
        idSet
        idShow
        idSource
        IsMissing
        KeyArtPath
        LandscapePath
        Language
        LastPlayed
        ListTitle
        Locked
        LockedEpisodesCount
        MPAA
        Marked
        MarkedCustom1
        MarkedCustom2
        MarkedCustom3
        MarkedCustom4
        MarkedEpisodesCount
        MediaType
        MovieCount
        MovieTitles
        Name
        [New]
        NewEpisodesCount
        NfoPath
        OriginalTitle
        OutOfTolerance
        Outline
        Path
        PlayCount
        Plot
        PosterPath
        Premiered
        Ratings
        ReleaseDate
        Role
        Runtime
        SeasonNumber
        SortMethod
        SortTitle
        SortedTitle
        SourceName
        Status
        Studios
        SubEpisode
        SubtitleForced
        SubtitleLanguage
        SubtitlesPath
        Tagline
        Tags
        ThemePath
        Title
        Top250
        Trailer
        TrailerPath
        UniqueIDs
        UserRating
        VideoAspect
        VideoBitDepth
        VideoBitrate
        VideoChromaSupersampling
        VideoCodec
        VideoColorPrimaries
        VideoDuration
        VideoHeight
        VideoLanguage
        VideoMultiViewCount
        VideoMultiViewLayout
        VideoScantype
        VideoSource
        VideoStereoMode
        VideoWidth
        Year
    End Enum

    Public Enum DataType As Integer
        [Boolean]
        [Double]    'float
        [Integer]   'int32
        [Long]      'int64
        [String]
    End Enum

    Public Enum TableName As Integer
        actor_link
        art
        certification
        certification_link
        country
        country_link
        creator_link
        director_link
        episode
        excludedpath
        file
        genre
        genre_link
        gueststar_link
        movie
        movieset
        movieset_link
        moviesource
        person
        rating
        season
        streamdetail
        studio
        studio_link
        tag
        tag_link
        tvshow
        tvshow_link
        tvshowsource
        uniqueid
        writer_link
    End Enum

#End Region 'Enumerations

#Region "Methods"

    Private Sub AddArt(ByVal idMedia As Long,
                       ByVal contentType As Enums.ContentType,
                       ByVal artType As String,
                       ByVal url As String,
                       ByVal width As Integer,
                       ByVal height As Integer)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Dim doesExist As Boolean = False
            Dim ID As Long = -1
            Dim oldURL As String = String.Empty

            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT idArt, url FROM art WHERE idMedia={0} AND media_type='{1}' AND type='{2}'", idMedia, mediaType, artType)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    While SQLreader.Read
                        doesExist = True
                        ID = CInt(SQLreader("idArt"))
                        oldURL = SQLreader("url").ToString
                        Exit While
                    End While
                End Using
            End Using

            If Not doesExist Then
                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand.CommandText = String.Format("INSERT INTO art(idMedia, media_type, type, url, width, height) VALUES ({0}, '{1}', '{2}', ?, {3}, {4})", idMedia, mediaType, artType, width, height)
                    Dim par_insert_art_url As SQLiteParameter = sqlCommand.Parameters.Add("par_insert_art_url", DbType.String, 0, "url")
                    par_insert_art_url.Value = url
                    sqlCommand.ExecuteNonQuery()
                End Using
            Else
                If Not url = oldURL Then
                    Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        sqlCommand.CommandText = String.Format("UPDATE art SET url=(?), width={0}, height={1} WHERE idArt={2}", width, height, ID)
                        Dim par_update_art_url As SQLiteParameter = sqlCommand.Parameters.Add("par_update_art_url", DbType.String, 0, "url")
                        par_update_art_url.Value = url
                        sqlCommand.ExecuteNonQuery()
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

    Private Function AddFileItem(ByVal fileItem As FileItem) As Long
        If fileItem Is Nothing OrElse Not fileItem.FullPathSpecified Then Return -1

        If Not fileItem.IDSpecified Then
            'search for an existing entry with same path
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = "SELECT idFile FROM file WHERE path=(?);"
                Dim par_path As SQLiteParameter = sqlCommand.Parameters.Add("par_path", DbType.String, 0, "path")
                par_path.Value = fileItem.FullPath
                Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader
                    If sqlReader.HasRows Then
                        sqlReader.Read()
                        fileItem.ID = CLng(sqlReader("idFile"))
                    End If
                End Using
            End Using
        End If

        If fileItem.IDSpecified Then
            'update the existing entry
            'it's not allowed to change the idFile value because the entry can be an entry of a multi-episode file
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("UPDATE file SET path=(?), originalFileName=(?), filesize=(?) WHERE idFile={0}", fileItem.ID)
                Dim par_path As SQLiteParameter = sqlCommand.Parameters.Add("par_path", DbType.String, 0, "path")
                Dim par_originalFileName As SQLiteParameter = sqlCommand.Parameters.Add("par_originalFileName", DbType.String, 0, "originalFileName")
                Dim par_fileSize As SQLiteParameter = sqlCommand.Parameters.Add("par_fileSize", DbType.Int64, 0, "fileSize")
                par_path.Value = fileItem.FullPath
                par_originalFileName.Value = fileItem.OriginalFileName
                If fileItem.TotalSize > 0 Then
                    par_fileSize.Value = fileItem.TotalSize
                Else
                    par_fileSize.Value = Nothing 'need to be NOTHING instead of 0
                End If
                sqlCommand.ExecuteNonQuery()
                Return fileItem.ID
            End Using
        Else
            'create a new entry
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Concat("INSERT INTO file (path, originalFileName, fileSize) VALUES (?,?,?); Select LAST_INSERT_ROWID() FROM file;")
                Dim par_path As SQLiteParameter = sqlCommand.Parameters.Add("parEpPath", DbType.String, 0, "path")
                Dim par_originalFileName As SQLiteParameter = sqlCommand.Parameters.Add("par_originalFileName", DbType.String, 0, "originalFileName")
                Dim par_fileSize As SQLiteParameter = sqlCommand.Parameters.Add("par_filesize", DbType.Int64, 0, "fileSize")
                par_path.Value = fileItem.FullPath
                par_originalFileName.Value = fileItem.OriginalFileName
                If fileItem.TotalSize > 0 Then
                    par_fileSize.Value = fileItem.TotalSize
                Else
                    par_fileSize.Value = Nothing 'need to be NOTHING instead of 0
                End If
                Return CLng(sqlCommand.ExecuteScalar)
            End Using
        End If
    End Function

    Public Sub AddExcludedPath(ByVal path As String)
        Using sqlTransaction As SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using sqlCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                sqlCommand.CommandText = "INSERT OR REPLACE INTO excludedpath (path) VALUES (?);"
                Dim par_path As SQLiteParameter = sqlCommand.Parameters.Add("par_path", DbType.String, 0, path)
                par_path.Value = path
                sqlCommand.ExecuteNonQuery()
            End Using
            sqlTransaction.Commit()
        End Using
    End Sub

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
    Private Function AddPerson(ByVal name As String,
                               ByVal thumbURL As String,
                               ByVal thumb As String,
                               ByVal uniqueIDs As MediaContainers.UniqueidContainer,
                               ByVal isActor As Boolean) As Long
        Dim doesExist As Boolean = False
        Dim lngID As Long = -1

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT idPerson FROM person WHERE name LIKE ?", name)
            Dim par_name As SQLiteParameter = sqlCommand.Parameters.Add("par_name", DbType.String, 0, "name")
            par_name.Value = name
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    doesExist = True
                    lngID = CInt(SQLreader("idPerson"))
                    Exit While
                End While
            End Using
        End Using

        If Not doesExist Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = "INSERT INTO person (idPerson, name, thumb) VALUES (NULL,?,?); SELECT LAST_INSERT_ROWID() FROM person;"
                Dim par_name As SQLiteParameter = sqlCommand.Parameters.Add("par_name", DbType.String, 0, "name")
                Dim par_thumb As SQLiteParameter = sqlCommand.Parameters.Add("par_thumb", DbType.String, 0, "thumb")
                par_name.Value = name
                par_thumb.Value = thumbURL
                lngID = CInt(sqlCommand.ExecuteScalar())
            End Using
        ElseIf isActor Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("UPDATE person SET thumb=? WHERE idPerson={0}", lngID)
                Dim par_thumb As SQLiteParameter = sqlCommand.Parameters.Add("par_thumb", DbType.String, 0, "thumb")
                par_thumb.Value = thumbURL
                sqlCommand.ExecuteNonQuery()
            End Using
        End If

        If Not lngID = -1 Then
            If Not String.IsNullOrEmpty(thumb) Then
                AddArt(lngID, Enums.ContentType.Person, "thumb", thumb, 0, 0)
            End If
        End If

        Return lngID
    End Function

    Private Function AddRating(ByVal idMedia As Long,
                               ByVal mediaType As String,
                               ByVal rating As MediaContainers.RatingDetails) As Long
        If Not idMedia = -1 AndAlso Not String.IsNullOrEmpty(mediaType) AndAlso rating.ValueSpecified AndAlso rating.VotesSpecified Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("INSERT OR REPLACE INTO rating (idMedia, media_type, rating_type, rating_max, rating, votes, isDefault) VALUES ({0},'{1}',?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM rating;",
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

    Private Function AddStudio(ByVal studio As String) As Long
        If String.IsNullOrEmpty(studio) Then Return -1
        Return AddToTable("studio", "idStudio", "name", studio)
    End Function
    ''' <summary>
    ''' Adds a new tag name
    ''' </summary>
    ''' <param name="tag">name of the tag</param>
    ''' <returns>the ID</returns>
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
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT * FROM {0} WHERE {1}={2} AND {3}={4}", table, firstField, firstID, secondField, secondID)
                If Not String.IsNullOrEmpty(typeField) AndAlso Not String.IsNullOrEmpty(type) Then
                    sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, String.Format(" AND {0}='{1}'", typeField, type))
                End If
                Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    If sqlReader.HasRows Then Exit Sub
                End Using
            End Using

            'add a new entry
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                If String.IsNullOrEmpty(typeField) AndAlso String.IsNullOrEmpty(type) Then
                    sqlCommand.CommandText = String.Format("INSERT INTO {0} ({1},{2}) VALUES ({3},{4})",
                                                                      table,
                                                                      firstField,
                                                                      secondField,
                                                                      firstID,
                                                                      secondID)
                Else
                    sqlCommand.CommandText = String.Format("INSERT INTO {0} ({1},{2},{3}) VALUES ({4},{5},'{6}')",
                                                                      table,
                                                                      firstField,
                                                                      secondField,
                                                                      typeField,
                                                                      firstID,
                                                                      secondID,
                                                                      type)
                End If
                sqlCommand.ExecuteNonQuery()
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
                sqlCommand.CommandText = String.Format("INSERT OR REPLACE INTO {0} (idPerson, idMedia, media_type, role, cast_order) VALUES ({1},{2},'{3}',?,{4})", table, idPerson, idMedia, mediaType, order)
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
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT {0} FROM {1} WHERE {2} LIKE ?", firstField, table, secondField)
            Dim par_select_secondField As SQLiteParameter = sqlCommand.Parameters.Add("par_select_secondField", DbType.String, 0, secondField)
            par_select_secondField.Value = value
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    'use existing entry ID
                    Return CInt(SQLreader(firstField))
                    Exit While
                End While
            End Using
        End Using

        'add a new entry
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("INSERT INTO {0} ({1}, {2}) VALUES (NULL, ?); SELECT LAST_INSERT_ROWID() FROM {0};", table, firstField, secondField)
            Dim par_insert_secondField As SQLiteParameter = sqlCommand.Parameters.Add("par_insert_secondField", DbType.String, 0, secondField)
            par_insert_secondField.Value = value
            Return CInt(sqlCommand.ExecuteScalar())
        End Using
    End Function

    Private Function AddUniqueID(ByVal idMedia As Long,
                                 ByVal mediaType As String,
                                 ByVal uniqueID As MediaContainers.Uniqueid) As Long
        If Not idMedia = -1 AndAlso Not String.IsNullOrEmpty(mediaType) AndAlso uniqueID.TypeSpecified AndAlso uniqueID.ValueSpecified Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("INSERT OR REPLACE INTO uniqueid (idMedia, media_type, value, type, isDefault) VALUES ({0},'{1}',?,?,?); SELECT LAST_INSERT_ROWID() FROM uniqueid;",
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
    ''' <summary>
    ''' Iterates db entries to check if the paths to the movie or TV files are valid. 
    ''' If not, remove all entries pertaining to the movie.
    ''' </summary>
    ''' <param name="cleanMovies">If <c>True</c>, process the movie files</param>
    ''' <param name="cleanTVShows">If <c>True</c>, process the TV files</param>
    ''' <param name="idSource">Optional. If provided, only process entries from that source.</param>
    ''' <remarks></remarks>
    Public Sub Clean(ByVal cleanMovies As Boolean,
                     ByVal cleanMovieSets As Boolean,
                     ByVal cleanTVShows As Boolean,
                     Optional ByVal idSource As Long = -1)
        logger.Info("Cleaning videodatabase started")

        Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
            If cleanMovies Then
                logger.Info("Cleaning movies started")
                'get a listing of sources and their recursive properties
                Dim lstMovieSources As New List(Of DBSource)
                If idSource = -1 Then
                    lstMovieSources = Load_AllSources_Movie()
                Else
                    Dim tmpSource = Load_Source_Movie(idSource)
                    If tmpSource IsNot Nothing Then
                        lstMovieSources.Add(tmpSource)
                    End If
                End If

                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    If idSource = -1 Then
                        sqlCommand.CommandText = "SELECT file.path, movie.idMovie, movie.idSource FROM file INNER JOIN movie ON file.idFile=movie.idFile ORDER BY file.path;"
                    Else
                        sqlCommand.CommandText = String.Format("SELECT file.path, movie.idMovie, movie.idSource FROM file INNER JOIN movie ON file.idFile=movie.idFile ORDER BY file.path WHERE movie.idSource={0} ORDER BY file.path;", idSource)
                    End If
                    Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                        While sqlReader.Read
                            Dim tSource As DBSource = lstMovieSources.OrderByDescending(Function(s) s.Path).FirstOrDefault(Function(s) s.ID = CLng(sqlReader("idSource")))
                            If tSource IsNot Nothing Then
                                Dim nFileItem As New FileItem(sqlReader("path").ToString)
                                If Not nFileItem.bIsOnline OrElse Not FileUtils.Common.IsValidFile(nFileItem.FileInfo, Enums.ContentType.Movie) Then
                                    Master.DB.Delete_Movie(CLng(sqlReader("idMovie")), True)
                                End If
                            Else
                                'orphaned (no longer mapped to an existing source/idSource)
                                Master.DB.Delete_Movie(CLng(sqlReader("idMovie")), True)
                            End If
                        End While
                    End Using
                End Using
                logger.Info("Cleaning movies done")
            End If

            If cleanMovieSets Then
                logger.Info("Cleaning moviesets started")
                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand.CommandText = "SELECT movieset.idSet, COUNT(movieset_link.idMovie) AS 'mCount' FROM movieset LEFT OUTER JOIN movieset_link ON movieset.idSet=movieset_link.idSet GROUP BY movieset.idSet ORDER BY movieset.idSet COLLATE NOCASE;"
                    Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                        While SQLreader.Read
                            If Convert.ToInt64(SQLreader("mCount")) = 0 Then
                                Master.DB.Delete_MovieSet(CLng(SQLreader("idSet")), True)
                            End If
                        End While
                    End Using
                End Using
                logger.Info("Cleaning moviesets done")
            End If

            If cleanTVShows Then
                logger.Info("Cleaning tv shows started")
                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    If idSource = -1 Then
                        sqlCommand.CommandText = "SELECT file.path, episode.idEpisode FROM file INNER JOIN episode ON file.idFile=episode.idFile ORDER BY file.path;"
                    Else
                        sqlCommand.CommandText = String.Format("SELECT file.path, episode.idEpisode FROM file INNER JOIN episode ON file.idFile=episode.idFile WHERE episode.idSource={0} ORDER BY file.path;", idSource)
                    End If

                    Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                        While sqlReader.Read
                            Dim nFileItem As New FileItem(sqlReader("path").ToString)
                            If Not nFileItem.bIsOnline OrElse Not FileUtils.Common.IsValidFile(nFileItem.FileInfo, Enums.ContentType.TVEpisode) Then
                                Master.DB.Delete_TVEpisode(CLng(sqlReader("idEpisode")), False, False, True)
                            End If
                        End While
                    End Using

                    logger.Info("Removing tvshows without local episodes")
                    sqlCommand.CommandText = "SELECT tvshow.idShow FROM tvshow WHERE NOT EXISTS (SELECT episode.idShow FROM episode WHERE episode.idShow=tvshow.idShow AND NOT episode.idFile = -1);"
                    Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                        While sqlReader.Read
                            Master.DB.Delete_TVShow(CLng(sqlReader("idShow")), True)
                        End While
                    End Using
                    logger.Info("Removing seasons with no more existing tvshows")
                    sqlCommand.CommandText = "DELETE FROM season WHERE idShow NOT IN (SELECT idShow FROM tvshow);"
                    sqlCommand.ExecuteNonQuery()
                    logger.Info("Removing episodes with no more existing tvshows")
                    sqlCommand.CommandText = "DELETE FROM episode WHERE idShow NOT IN (SELECT idShow FROM tvshow);"
                    sqlCommand.ExecuteNonQuery()
                    logger.Info("Removing episodes with orphaned paths")
                    sqlCommand.CommandText = "DELETE FROM episode WHERE NOT EXISTS (SELECT file.idFile FROM file WHERE file.idFile=episode.idFile OR episode.idFile = -1);"
                    sqlCommand.ExecuteNonQuery()
                    logger.Info("Removing orphaned paths")
                    sqlCommand.CommandText = String.Concat("DELETE FROM file WHERE NOT EXISTS (SELECT episode.idFile FROM episode WHERE episode.idFile=file.idFile AND NOT episode.idFile = -1)",
                                                               " AND NOT EXISTS (SELECT movie.idFile FROM movie WHERE movie.idFile=file.idFile AND NOT movie.idFile = -1);")
                    sqlCommand.ExecuteNonQuery()
                End Using

                logger.Info("Removing seasons with no more existing episodes")
                Delete_Empty_TVSeasons(-1, True)
                logger.Info("Cleaning tv shows done")
            End If

            'global cleaning
            logger.Info("Cleaning global tables started")
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                'clean all link tables
                'actor_link
                logger.Info("Cleaning actor_link table")
                CleanLinkTable("actor_link", "episode", "idEpisode", Enums.ContentType.TVEpisode)
                CleanLinkTable("actor_link", "movie", "idMovie", Enums.ContentType.Movie)
                CleanLinkTable("actor_link", "tvshow", "idShow", Enums.ContentType.TVShow)
                'certification_link
                logger.Info("Cleaning certification_link table")
                CleanLinkTable("certification_link", "movie", "idMovie", Enums.ContentType.Movie)
                CleanLinkTable("certification_link", "tvshow", "idShow", Enums.ContentType.TVShow)
                'country_link
                logger.Info("Cleaning country_link table")
                CleanLinkTable("country_link", "movie", "idMovie", Enums.ContentType.Movie)
                CleanLinkTable("country_link", "tvshow", "idShow", Enums.ContentType.TVShow)
                'creator_link
                logger.Info("Cleaning creator_link table")
                CleanLinkTable("creator_link", "tvshow", "idShow", Enums.ContentType.TVShow)
                'director_link
                logger.Info("Cleaning director_link table")
                CleanLinkTable("director_link", "movie", "idMovie", Enums.ContentType.Movie)
                CleanLinkTable("director_link", "tvshow", "idShow", Enums.ContentType.TVShow)
                'genre_link
                logger.Info("Cleaning genre_link table")
                CleanLinkTable("genre_link", "movie", "idMovie", Enums.ContentType.Movie)
                CleanLinkTable("genre_link", "tvshow", "idShow", Enums.ContentType.TVShow)
                'gueststar_link
                logger.Info("Cleaning gueststar_link table")
                CleanLinkTable("gueststar_link", "episode", "idEpisode", Enums.ContentType.TVEpisode)
                'movieset_link
                logger.Info("Cleaning movieset_link table")
                sqlCommand.CommandText = "DELETE FROM movieset_link WHERE idMovie Not IN (SELECT 1 idMovie FROM movie);"
                sqlCommand.ExecuteNonQuery()
                'studio_link
                logger.Info("Cleaning studio_link table")
                CleanLinkTable("studio_link", "movie", "idMovie", Enums.ContentType.Movie)
                CleanLinkTable("studio_link", "tvshow", "idShow", Enums.ContentType.TVShow)
                'writer_link
                logger.Info("Cleaning writer_link table")
                CleanLinkTable("writer_link", "episode", "idEpisode", Enums.ContentType.TVEpisode)
                CleanLinkTable("writer_link", "movie", "idMovie", Enums.ContentType.Movie)

                'clean all main tables
                'certification
                logger.Info("Cleaning certification table")
                CleanMainTable("certification", "certification_link", "idCertification")
                'country
                logger.Info("Cleaning country table")
                CleanMainTable("country", "country_link", "idCountry")
                'genre
                logger.Info("Cleaning genre table")
                CleanMainTable("genre", "genre_link", "idGenre")
                'person
                logger.Info("Cleaning person table")
                CleanPersonTable()
                'studio
                logger.Info("Cleaning studio table")
                CleanMainTable("studio", "studio_link", "idStudio")
            End Using
            logger.Info("Cleaning global tables done")

            sqlTransaction.Commit()
        End Using

        logger.Info("Cleaning videodatabase done")

        ' Housekeeping - consolidate and pack database using vacuum command http://www.sqlite.org/lang_vacuum.html
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            logger.Info("Rebulding videodatabase started")
            sqlCommand.CommandText = "VACUUM;"
            sqlCommand.ExecuteNonQuery()
            logger.Info("Rebulding videodatabase done")
        End Using
    End Sub

    Private Sub CleanPersonTable()
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format(String.Concat(
                                                   "DELETE FROM {0} WHERE NOT EXISTS (SELECT 1 FROM {2} WHERE {0}.{1}={2}.{1})",
                                                   " AND NOT EXISTS (SELECT 1 FROM {3} WHERE {0}.{1}={3}.{1})",
                                                   " AND NOT EXISTS (SELECT 1 FROM {4} WHERE {0}.{1}={4}.{1})",
                                                   " AND NOT EXISTS (SELECT 1 FROM {5} WHERE {0}.{1}={5}.{1})",
                                                   " AND NOT EXISTS (SELECT 1 FROM {6} WHERE {0}.{1}={6}.{1})",
                                                   ";"),
                                                   Helpers.GetTableName(TableName.person),
                                                   Helpers.GetColumnName(ColumnName.idPerson),
                                                   Helpers.GetTableName(TableName.actor_link),
                                                   Helpers.GetTableName(TableName.creator_link),
                                                   Helpers.GetTableName(TableName.director_link),
                                                   Helpers.GetTableName(TableName.gueststar_link),
                                                   Helpers.GetTableName(TableName.writer_link)
                                                   )
            sqlCommand.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub CleanLinkTable(ByVal linkTable As String,
                               ByVal mainTable As String,
                               ByVal mainField As String,
                               ByVal contentType As Enums.ContentType)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("DELETE FROM {0} WHERE {0}.media_type='{1}' AND NOT EXISTS (SELECT 1 FROM {2} WHERE {0}.idMedia={2}.{3});",
                                                           linkTable, mediaType, mainTable, mainField)
                sqlCommand.ExecuteNonQuery()
            End Using
        End If
    End Sub

    Private Sub CleanMainTable(ByVal mainTable As String,
                               ByVal linkTable As String,
                               ByVal mainField As String)
        If Not String.IsNullOrEmpty(mainTable) AndAlso Not String.IsNullOrEmpty(linkTable) Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("DELETE FROM {0} WHERE NOT EXISTS (SELECT 1 FROM {1} WHERE {0}.{2}={1}.{2});",
                                                           mainTable, linkTable, mainField)
                sqlCommand.ExecuteNonQuery()
            End Using
        End If
    End Sub

    Private Function GetActorsForItem(ByVal idMedia As Long,
                                      ByVal contentType As Enums.ContentType) As List(Of MediaContainers.Person)
        Return GetFromPersonLinkTable("actor_link", idMedia, contentType)
    End Function

    Public Function GetArtForItem(ByVal idMedia As Long,
                                  ByVal contentType As Enums.ContentType,
                                  ByVal artType As String) As String
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT url FROM art WHERE idMedia={0} AND media_type='{1}' AND type='{2}'", idMedia, mediaType, artType)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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
        Return GetFromLinkTable("creator_link", "idPerson", "person", "idPerson", idMedia, contentType)
    End Function

    Private Function GetDirectorsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of String)
        Return GetFromLinkTable("director_link", "idPerson", "person", "idPerson", idMedia, contentType)
    End Function
    ''' <summary>
    ''' Get a list of excluded paths
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetExcludedPaths() As List(Of String)
        Dim lstResult As New List(Of String)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT path FROM excludedpath;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    lstResult.Add(SQLreader("path").ToString)
                End While
            End Using
        End Using
        Return lstResult
    End Function

    Private Function GetExternalSubtitlesForFileItem(ByVal fileItem As FileItem) As List(Of MediaContainers.Subtitle)
        Dim lstSubtitles As New List(Of MediaContainers.Subtitle)
        If fileItem IsNot Nothing AndAlso fileItem.IDSpecified Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT * FROM streamdetail WHERE idFile={0} AND streamType=2 AND subtitlePath<>''", fileItem.ID)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    Dim subtitle As MediaContainers.Subtitle
                    While SQLreader.Read
                        subtitle = New MediaContainers.Subtitle
                        If Not DBNull.Value.Equals(SQLreader("subtitleLanguage")) Then subtitle.Language = SQLreader("subtitleLanguage").ToString
                        If Not DBNull.Value.Equals(SQLreader("subtitleForced")) Then subtitle.Forced = Convert.ToBoolean(SQLreader("subtitleForced"))
                        If Not DBNull.Value.Equals(SQLreader("subtitlePath")) Then subtitle.Path = SQLreader("subtitlePath").ToString
                        lstSubtitles.Add(subtitle)
                    End While
                End Using
            End Using
        End If
        Return lstSubtitles
    End Function

    Private Function GetFileItem(ByVal idFile As Long) As FileItem
        Dim nDBFile As FileItem = Nothing
        If Not idFile = -1 Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT * FROM file WHERE idFile={0};", idFile)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    While SQLreader.Read
                        nDBFile = New FileItem(SQLreader("path").ToString)
                        With nDBFile
                            .ID = idFile
                            If Not DBNull.Value.Equals(SQLreader("originalFileName")) Then .OriginalFileName = SQLreader("originalFileName").ToString
                        End With
                    End While
                End Using
            End Using
        End If
        Return nDBFile
    End Function

    Private Function GetFileInfoForFileItem(ByVal fileItem As FileItem) As MediaContainers.FileInfo
        Dim nFileInfo As New MediaContainers.FileInfo
        If fileItem IsNot Nothing AndAlso fileItem.IDSpecified Then
            'streamdetails
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT * FROM streamdetail WHERE idFile={0};", fileItem.ID)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    While SQLreader.Read
                        Select Case CInt(SQLreader("streamType").ToString)
                            Case 0 'videostream
                                Dim video As New MediaContainers.Video
                                If Not DBNull.Value.Equals(SQLreader("videoCodec")) Then video.Codec = SQLreader("videoCodec").ToString
                                If Not DBNull.Value.Equals(SQLreader("videoAspect")) Then video.Aspect = CDbl(SQLreader("videoAspect"))
                                If Not DBNull.Value.Equals(SQLreader("videoBitrate")) Then video.Bitrate = CInt(SQLreader("videoBitrate"))
                                If Not DBNull.Value.Equals(SQLreader("videoLanguage")) Then video.Language = SQLreader("videoLanguage").ToString
                                If Not DBNull.Value.Equals(SQLreader("videoWidth")) Then video.Width = CInt(SQLreader("videoWidth"))
                                If Not DBNull.Value.Equals(SQLreader("videoHeight")) Then video.Height = CInt(SQLreader("videoHeight"))
                                If Not DBNull.Value.Equals(SQLreader("videoScanType")) Then video.Scantype = SQLreader("videoScanType").ToString
                                If Not DBNull.Value.Equals(SQLreader("videoDuration")) Then video.Duration = CInt(SQLreader("videoDuration").ToString)
                                If Not DBNull.Value.Equals(SQLreader("videoMultiViewCount")) Then video.MultiViewCount = CInt(SQLreader("videoMultiViewCount"))
                                If Not DBNull.Value.Equals(SQLreader("videoMultiViewLayout")) Then video.MultiViewLayout = SQLreader("videoMultiViewLayout").ToString
                                If Not DBNull.Value.Equals(SQLreader("videoStereoMode")) Then video.StereoMode = SQLreader("videoStereoMode").ToString
                                If Not DBNull.Value.Equals(SQLreader("videoBitDepth")) Then video.BitDepth = CInt(SQLreader("videoBitDepth"))
                                If Not DBNull.Value.Equals(SQLreader("videoChromaSubsampling")) Then video.ChromaSubsampling = SQLreader("videoChromaSubsampling").ToString
                                If Not DBNull.Value.Equals(SQLreader("videoColourPrimaries")) Then video.ColourPrimaries = SQLreader("videoColourPrimaries").ToString
                                nFileInfo.StreamDetails.Video.Add(video)
                            Case 1 'audio stream
                                Dim audio As New MediaContainers.Audio
                                If Not DBNull.Value.Equals(SQLreader("audioCodec")) Then audio.Codec = SQLreader("audioCodec").ToString
                                If Not DBNull.Value.Equals(SQLreader("audioChannels")) Then audio.Channels = CInt(SQLreader("audioChannels"))
                                If Not DBNull.Value.Equals(SQLreader("audioBitrate")) Then audio.Bitrate = CInt(SQLreader("audioBitrate"))
                                If Not DBNull.Value.Equals(SQLreader("audioLanguage")) Then audio.Language = SQLreader("audioLanguage").ToString
                                nFileInfo.StreamDetails.Audio.Add(audio)
                            Case 2 'subtitle stream
                                Dim subtitle As New MediaContainers.Subtitle
                                'only get embedded subtitles
                                If DBNull.Value.Equals(SQLreader("subtitlePath")) OrElse String.IsNullOrEmpty(SQLreader("subtitlePath").ToString) Then
                                    If Not DBNull.Value.Equals(SQLreader("subtitleLanguage")) Then subtitle.Language = SQLreader("subtitleLanguage").ToString
                                    If Not DBNull.Value.Equals(SQLreader("subtitleForced")) Then subtitle.Forced = Convert.ToBoolean(SQLreader("subtitleForced"))
                                    nFileInfo.StreamDetails.Subtitle.Add(subtitle)
                                End If
                        End Select
                    End While
                End Using
            End Using
        End If
        Return nFileInfo
    End Function

    Private Function GetGenresForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of String)
        Return GetFromLinkTable("genre_link", "idGenre", "genre", "idGenre", idMedia, contentType)
    End Function

    Private Function GetRatingsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of MediaContainers.RatingDetails)
        Dim lstResults As New List(Of MediaContainers.RatingDetails)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT * FROM rating WHERE idMedia={0} AND media_type='{1}';", idMedia, mediaType)
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

    Private Function GetGuestStarsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of MediaContainers.Person)
        Return GetFromPersonLinkTable("gueststar_link", idMedia, contentType)
    End Function

    Private Function GetMoviesetsForMovie(ByVal idMovie As Long) As List(Of MediaContainers.SetDetails)
        Dim lstResults As New List(Of MediaContainers.SetDetails)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Concat("SELECT a.idSet, a.set_order, b.plot, b.title FROM movieset_link ",
                                                           "AS a INNER JOIN movieset AS b ON (a.idSet=b.idSet) WHERE a.idMovie=", idMovie, ";")
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    Dim nSet As New MediaContainers.SetDetails
                    If Not DBNull.Value.Equals(SQLreader("idSet")) Then nSet.ID = Convert.ToInt64(SQLreader("idSet"))
                    If Not DBNull.Value.Equals(SQLreader("set_order")) Then nSet.Order = CInt(SQLreader("set_order"))
                    If Not DBNull.Value.Equals(SQLreader("plot")) Then nSet.Plot = SQLreader("plot").ToString
                    If Not DBNull.Value.Equals(SQLreader("title")) Then nSet.Title = SQLreader("title").ToString
                    lstResults.Add(nSet)
                End While
            End Using
        End Using
        Return lstResults
    End Function

    Private Function GetStudiosForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of String)
        Return GetFromLinkTable("studio_link", "idStudio", "studio", "idStudio", idMedia, contentType)
    End Function

    Private Function GetTagsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of String)
        Return GetFromLinkTable("tag_link", "idTag", "tag", "idTag", idMedia, contentType)
    End Function

    Private Function GetTVShowLinksForMovie(ByVal idMovie As Long, ByVal contentType As Enums.ContentType) As List(Of String)
        Dim lstResults As New List(Of String)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Concat("SELECT b.title FROM tvshow_link ",
                                                           "AS a INNER JOIN tvshow AS b ON (a.idShow=b.idShow) WHERE a.idMovie=", idMovie, ";")
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("title")) Then lstResults.Add(SQLreader("title").ToString)
                End While
            End Using
        End Using
        Return lstResults
    End Function

    Private Function GetWritersForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As List(Of String)
        Return GetFromLinkTable("writer_link", "idPerson", "person", "idPerson", idMedia, contentType)
    End Function

    Private Function GetUniqueIDsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As MediaContainers.UniqueidContainer
        Dim lstUniqueIDs As New MediaContainers.UniqueidContainer
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT * FROM uniqueid WHERE idMedia={0} AND media_type='{1}' ORDER BY isDefault=0", idMedia, mediaType)
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

    Private Function GetFromLinkTable(ByVal linkTable As String,
                                      ByVal linkField As String,
                                      ByVal mainTable As String,
                                      ByVal mainField As String,
                                      ByVal idMedia As Long,
                                      ByVal contentType As Enums.ContentType) As List(Of String)
        Dim lstResults As New List(Of String)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT b.name FROM {0} AS a INNER JOIN {1} AS b ON (a.{2}=b.{3}) WHERE a.idMedia={4} AND a.media_type='{5}';",
                                                           linkTable, mainTable, linkField, mainField, idMedia, mediaType)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT * FROM {0} AS a INNER JOIN person AS b ON (a.idPerson=b.idPerson) LEFT OUTER JOIN art AS c ON (b.idPerson=c.idMedia AND c.media_type='person' AND c.type='thumb') WHERE a.idMedia={1} AND a.media_type='{2}' ORDER BY a.cast_order;",
                                                           linkTable, idMedia, mediaType)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    While SQLreader.Read
                        lstResults.Add(New MediaContainers.Person With {
                                           .ID = CLng(SQLreader("idPerson")),
                                           .LocalFilePath = SQLreader("url").ToString,
                                           .Name = SQLreader("name").ToString,
                                           .Order = CInt(SQLreader("cast_order")),
                                           .Role = SQLreader("role").ToString,
                                           .UniqueIDs = GetUniqueIDsForItem(CLng(SQLreader("idPerson")), Enums.ContentType.Person),
                                           .URLOriginal = SQLreader("thumb").ToString
                                           })
                    End While
                End Using
            End Using
        End If
        Return lstResults
    End Function

    Public Sub Cleanup_Genres()
        logger.Info("[Database] [Cleanup_Genres] Started")
        Dim lstMovies As New List(Of Long)
        Dim lstTVShows As New List(Of Long)

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT DISTINCT idMedia FROM genre_link WHERE media_type='movie';"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    lstMovies.Add(Convert.ToInt64(SQLreader("idMedia")))
                End While
            End Using

            sqlCommand.CommandText = "SELECT DISTINCT idMedia FROM genre_link WHERE media_type='tvshow';"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    lstTVShows.Add(Convert.ToInt64(SQLreader("idMedia")))
                End While
            End Using
        End Using

        Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
            logger.Info("[Database] [Cleanup_Genres] Process all Movies")
            'Process all Movies, which are assigned to a genre
            For Each lMovieID In lstMovies
                Dim tmpDBElement As DBElement = Load_Movie(lMovieID)
                If tmpDBElement.IsOnline Then
                    If StringUtils.GenreFilter(tmpDBElement.Movie.Genres, False) Then
                        Save_Movie(tmpDBElement, True, True, False, True, False)
                    End If
                Else
                    logger.Warn(String.Concat("[Database] [Cleanup_Genres] Skip Movie (not online): ", tmpDBElement.FileItem.FullPath))
                End If
            Next

            'Process all TVShows, which are assigned to a genre
            logger.Info("[Database] [Cleanup_Genres] Process all TVShows")
            For Each lTVShowID In lstTVShows
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
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                logger.Info("[Database] [Cleanup_Genres] Cleaning genre table")
                sqlCommand.CommandText = String.Concat("DELETE FROM genre ",
                                                           "WHERE NOT EXISTS (SELECT 1 FROM genre_link WHERE genre_link.idGenre = genre.idGenre);")
                sqlCommand.ExecuteNonQuery()
            End Using

            sqlTransaction.Commit()
        End Using
        logger.Info("[Database] [Cleanup_Genres] Done")
    End Sub
    ''' <summary>
    ''' Remove the New flag from database entries (movies, tvshow, season, episode)
    ''' </summary>
    ''' <remarks>
    ''' 2013/12/13 Dekker500 - Check that MediaDBConn IsNot Nothing before continuing, 
    '''                        otherwise shutdown after a failed startup (before DB initialized) 
    '''                        will trow exception
    ''' </remarks>
    Public Sub ClearNew()
        If Master.DB.MyVideosDBConn IsNot Nothing Then
            Using sqlTransaction As SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand.CommandText = "UPDATE episode SET new=0;"
                    sqlCommand.ExecuteNonQuery()
                End Using
                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand.CommandText = "UPDATE movie SET new=0;"
                    sqlCommand.ExecuteNonQuery()
                End Using
                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand.CommandText = "UPDATE movieset SET new=0;"
                    sqlCommand.ExecuteNonQuery()
                End Using
                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand.CommandText = "UPDATE season SET new=0;"
                    sqlCommand.ExecuteNonQuery()
                End Using
                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand.CommandText = "UPDATE tvshow SET new=0;"
                    sqlCommand.ExecuteNonQuery()
                End Using
                sqlTransaction.Commit()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Close the databases
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Close_MyVideos()
        CloseDatabase(_myvideosDBConn)
        If _myvideosDBConn IsNot Nothing Then
            _myvideosDBConn = Nothing
        End If
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
            Case Enums.ContentType.Movieset
                Return "movieset"
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

    Private Function ConvertMediaTypeToContentType(mediaType As String) As Enums.ContentType
        Select Case mediaType
            Case "movie"
                Return Enums.ContentType.Movie
            Case "set"
                Return Enums.ContentType.Movieset
            Case "person"
                Return Enums.ContentType.Person
            Case "episode"
                Return Enums.ContentType.TVEpisode
            Case "season"
                Return Enums.ContentType.TVSeason
            Case "tvshow"
                Return Enums.ContentType.TVShow
            Case Else
                Return Enums.ContentType.None
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
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not idShow = -1 Then
                sqlCommand.CommandText = String.Format("DELETE FROM season WHERE season.idShow={0} AND NOT EXISTS (SELECT episode.season FROM episode WHERE episode.season=season.season AND episode.idShow=season.idShow) AND season.season <> -1", idShow)
            Else
                sqlCommand.CommandText = String.Format("DELETE FROM season WHERE NOT EXISTS (SELECT episode.season FROM episode WHERE episode.season=season.season AND episode.idShow=season.idShow) AND season.season <> -1")
            End If
            sqlCommand.ExecuteNonQuery()
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
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT idEpisode FROM episode WHERE idShow = {0};", idShow)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT idSeason FROM season WHERE idShow={0};", idShow)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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
        If Not idMovie = -1 Then
            Dim nDBElement As DBElement = Load_Movie(idMovie)
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Remove_Movie, Nothing, Nothing, False, nDBElement)

            Dim sqlTransaction As SQLiteTransaction = Nothing
            If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("DELETE FROM movie WHERE idMovie={0};", idMovie)
                sqlCommand.ExecuteNonQuery()
            End Using
            If Not batchMode Then sqlTransaction.Commit()

            RaiseEvent GenericEvent(Enums.ModuleEventType.Remove_Movie, New List(Of Object)(New Object() {nDBElement.ID}))
        End If
    End Sub
    ''' <summary>
    ''' Remove all information related to a movieset from the database.
    ''' </summary>
    ''' <param name="idMovieset">ID of the movieset to remove, as stored in the database.</param>
    ''' <param name="batchMode">Is this function already part of a transaction?</param>
    Public Sub Delete_MovieSet(ByVal idMovieset As Long, ByVal batchMode As Boolean)
        If Not idMovieset = -1 Then
            'first get a list of all movies in the movieset to remove the movieset information from NFO
            Dim lstMoviesToEdit As New List(Of DBElement)

            Dim sqlTransaction As SQLiteTransaction = Nothing
            If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT idMovie FROM movieset_link WHERE idSet={0};", idMovieset)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    While SQLreader.Read
                        If Not DBNull.Value.Equals(SQLreader("idMovie")) Then
                            lstMoviesToEdit.Add(Load_Movie(CLng(SQLreader("idMovie"))))
                        End If
                    End While
                End Using
            End Using

            'remove the movieset from movie and write new movie NFOs
            If lstMoviesToEdit.Count > 0 Then
                For Each movie In lstMoviesToEdit
                    movie.Movie.RemoveSet(idMovieset)
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, Nothing, False, movie)
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_Movie, Nothing, Nothing, False, movie)
                    Save_Movie(movie, batchMode, True, False, True, False)
                    RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {movie.ID}))
                Next
            End If

            'delete all movieset images and if this setting is enabled
            If Master.eSettings.MovieSetCleanFiles Then
                Dim MovieSet As DBElement = Master.DB.Load_Movieset(idMovieset)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainBanner)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainClearArt)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainClearLogo)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainDiscArt)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainFanart)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainLandscape)
                Images.Delete_MovieSet(MovieSet, Enums.ModifierType.MainPoster)
            End If

            'remove the movieset and still existing setlinkmovie entries
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("DELETE FROM movieset WHERE idSet={0};", idMovieset)
                sqlCommand.ExecuteNonQuery()
            End Using
            If Not batchMode Then sqlTransaction.Commit()
        End If
    End Sub

    ''' <summary>
    ''' Remove all information related to a tag from the database.
    ''' </summary>
    ''' <param name="idTag">Internal TagID of the tag to remove, as stored in the database.</param>
    ''' <param name="mode">1=tag of a movie, 2=tag of a show</param>
    ''' <param name="batchMode">Is this function already part of a transaction?</param>
    Public Sub Delete_Tag(ByVal idTag As Long, ByVal batchMode As Boolean)
        If Not idTag = -1 Then
            Dim lstDBElement As New List(Of DBElement)
            Dim strTagName As String = String.Empty
            Dim sqlTransaction As SQLiteTransaction = Nothing
            If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT name FROM tag WHERE idTag={0};", idTag)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    While SQLreader.Read
                        If Not DBNull.Value.Equals(SQLreader("name")) Then strTagName = CStr(SQLreader("name"))
                    End While
                End Using
            End Using

            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT idMedia, media_type FROM tag_link WHERE idTag={0};", idTag)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    While SQLreader.Read
                        Select Case ConvertMediaTypeToContentType(SQLreader("media_type").ToString)
                            Case Enums.ContentType.Movie
                                lstDBElement.Add(Load_Movie(CLng(SQLreader("idMedia"))))
                            Case Enums.ContentType.TVShow
                                lstDBElement.Add(Load_TVShow(CLng(SQLreader("idMedia")), False, False, False))
                        End Select
                    End While
                End Using
            End Using

            'remove the tag from movie and write new movie NFOs
            If lstDBElement.Count > 0 Then
                For Each nDBElement In lstDBElement
                    Select Case nDBElement.ContentType
                        Case Enums.ContentType.Movie
                            nDBElement.Movie.Tags.Remove(strTagName)
                            Save_Movie(nDBElement, batchMode, True, False, True, False)
                        Case Enums.ContentType.TVShow
                            nDBElement.TVShow.Tags.Remove(strTagName)
                            Save_TVShow(nDBElement, batchMode, True, False, False)
                    End Select
                Next
            End If

            'remove the tag entry
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("DELETE FROM tag_link WHERE idTag={0};", idTag)
                sqlCommand.ExecuteNonQuery()
            End Using
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("DELETE FROM tag WHERE idTag={0};", idTag)
                sqlCommand.ExecuteNonQuery()
            End Using
            If Not batchMode Then sqlTransaction.Commit()
        End If
    End Sub

    ''' <summary>
    ''' Remove all information related to a TV episode from the database.
    ''' </summary>
    ''' <param name="idEpisode">ID of the episode to remove, as stored in the database.</param>
    ''' <param name="batchMode">Is this function already part of a transaction?</param>
    ''' <returns><c>True</c> if has been removed, <c>False</c> if has been changed to missing</returns>
    Public Function Delete_TVEpisode(ByVal idEpisode As Long, ByVal doForce As Boolean, ByVal doCleanSeasons As Boolean, ByVal batchMode As Boolean) As Boolean
        Dim sqlTransaction As SQLiteTransaction = Nothing
        Dim doesExist As Boolean = False
        Dim bHasRemoved As Boolean = False

        Dim _tvepisodeDB As Database.DBElement = Load_TVEpisode(idEpisode, True)
        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Remove_TVEpisode, Nothing, Nothing, False, _tvepisodeDB)

        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT idFile, episode, season, idShow FROM episode WHERE idEpisode={0};", idEpisode)
            Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader
                While sqlReader.Read
                    Using sqlCommand_delete As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        If Not doForce Then
                            'check if there is another episode with same season and episode number (in this case we don't need a another "Missing" episode)
                            Using sqlCommand_select As SQLiteCommand = _myvideosDBConn.CreateCommand
                                sqlCommand_select.CommandText = String.Format("SELECT COUNT(episode.idEpisode) AS eCount FROM episode WHERE NOT idEpisode={0} AND season={1} AND episode={2} AND idShow={3}",
                                                                                  idEpisode, sqlReader("season"), sqlReader("episode"), sqlReader("idShow"))
                                Using sqlReader_select As SQLiteDataReader = sqlCommand_select.ExecuteReader
                                    While sqlReader_select.Read
                                        If CInt(sqlReader_select("eCount")) > 0 Then doesExist = True
                                    End While
                                End Using
                            End Using
                        End If

                        If doForce OrElse doesExist Then
                            sqlCommand_delete.CommandText = String.Format("DELETE FROM episode WHERE idEpisode={0};", idEpisode)
                            sqlCommand_delete.ExecuteNonQuery()

                            If doCleanSeasons Then Master.DB.Delete_Empty_TVSeasons(CLng(sqlReader("idShow")), True)
                            bHasRemoved = True
                        ElseIf Not Convert.ToInt64(sqlReader("idFile")) = -1 Then 'already marked as missing, no need for another query
                            'check if there is another episode that use the same idFile
                            Dim multiEpisode As Boolean = False
                            Using sqlCommand_select As SQLiteCommand = _myvideosDBConn.CreateCommand
                                sqlCommand_select.CommandText = String.Format("SELECT COUNT(episode.idFile) AS eCount FROM episode WHERE idFile={0}", Convert.ToInt64(sqlReader("idFile")))
                                Using sqlReader_select As SQLiteDataReader = sqlCommand_select.ExecuteReader
                                    While sqlReader_select.Read
                                        If CInt(sqlReader_select("eCount")) > 1 Then multiEpisode = True
                                    End While
                                End Using
                            End Using
                            If Not multiEpisode Then
                                sqlCommand_delete.CommandText = String.Format("DELETE FROM file WHERE idFile={0};", CLng(sqlReader("idFile")))
                                sqlCommand_delete.ExecuteNonQuery()
                            End If
                            RemoveArtFromItem(idEpisode, Enums.ContentType.TVEpisode)
                            RemoveFileInfoFromFileItem(CLng(sqlReader("idFile")))
                            sqlCommand_delete.CommandText = String.Format("UPDATE episode SET new=0, idFile=-1, nfoPath='', videoSource='' WHERE idEpisode={0};", idEpisode)
                            sqlCommand_delete.ExecuteNonQuery()
                        End If
                    End Using
                End While
            End Using
        End Using

        If Not batchMode Then sqlTransaction.Commit()

        Return bHasRemoved
    End Function

    Public Sub Delete_TVEpisode(ByVal path As String, ByVal doForce As Boolean, ByVal batchMode As Boolean)
        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT idEpisode FROM episode INNER JOIN file ON episode.idFile=file.idFile WHERE file.path=""{0}"";", path)
            Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader
                While sqlReader.Read
                    Delete_TVEpisode(CLng(sqlReader("idEpisode")), doForce, False, batchMode)
                End While
            End Using
        End Using
        If Not batchMode Then sqlTransaction.Commit()
    End Sub

    ''' <summary>
    ''' Remove all information related to a TV season from the database.
    ''' </summary>
    ''' <param name="batchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Sub Delete_TVSeason(ByVal idSeason As Long, ByVal batchMode As Boolean)
        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("DELETE FROM season WHERE idSeason={0};", idSeason)
            sqlCommand.ExecuteNonQuery()
        End Using
        If Not batchMode Then sqlTransaction.Commit()
    End Sub

    ''' <summary>
    ''' Remove all information related to a TV season from the database.
    ''' </summary>
    ''' <param name="idShow">ID of the tvshow to remove, as stored in the database.</param>
    ''' <param name="batchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Function Delete_TVSeason(ByVal idShow As Long, ByVal season As Integer, ByVal batchMode As Boolean) As Boolean
        If idShow < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & idShow)
        If season < 0 Then Throw New ArgumentOutOfRangeException("iSeason", "Value must be >= 0, was given: " & season)

        Try
            Dim SQLtransaction As SQLiteTransaction = Nothing
            If Not batchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("DELETE FROM season WHERE idShow={0} AND season={1};", idShow, season)
                sqlCommand.ExecuteNonQuery()
            End Using
            If Not batchMode Then SQLtransaction.Commit()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Remove all information related to a TV show from the database.
    ''' </summary>
    ''' <param name="idShow">ID of the tvshow to remove, as stored in the database.</param>
    ''' <param name="batchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Function Delete_TVShow(ByVal idShow As Long, ByVal batchMode As Boolean) As Boolean
        Dim _tvshowDB As DBElement = Load_TVShow_Full(idShow)
        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Remove_TVShow, Nothing, Nothing, False, _tvshowDB)

        Try
            Dim SQLtransaction As SQLiteTransaction = Nothing
            If Not batchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("DELETE FROM tvshow WHERE idShow={0};", idShow)
                sqlCommand.ExecuteNonQuery()
            End Using
            If Not batchMode Then SQLtransaction.Commit()

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
    ''' <param name="Table">DataTable to fill</param>
    ''' <param name="Filter">SmartFilter</param>
    Private Sub FillDataTable(ByRef Table As DataTable, ByVal Filter As SmartFilter.Filter)
        Table.Clear()
        Dim sqlConnection As SQLiteConnection = Nothing
        Dim sqlDA As New SQLiteDataAdapter
        Dim sqlQuery As String = String.Empty
        Select Case Filter.Type
            Case Enums.ContentType.Movie,
                 Enums.ContentType.Movieset,
                 Enums.ContentType.Moviesource,
                 Enums.ContentType.MusicVideo,
                 Enums.ContentType.TVEpisode,
                 Enums.ContentType.TVSeason,
                 Enums.ContentType.TVShow,
                 Enums.ContentType.TVShowsource
                sqlConnection = _myvideosDBConn
        End Select
        If sqlConnection IsNot Nothing Then
            sqlQuery = Filter.SqlQuery_Full
            sqlDA = New SQLiteDataAdapter(sqlQuery, sqlConnection)
            Try
                sqlDA.Fill(Table)
            Catch ex As Exception
                logger.Error(String.Format("Get error: ""{0}"" with SQLCommand: ""{1}""", ex.Message, sqlQuery))
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Fill DataTable with data returned from the provided command
    ''' </summary>
    ''' <param name="dataTable">DataTable to fill</param>
    ''' <param name="sqlCommand">SQL Command to process</param>
    Private Sub FillDataTable(ByRef dataTable As DataTable, ByVal sqlCommand As String)
        dataTable.Clear()
        Dim sqlDA As New SQLiteDataAdapter(sqlCommand, _myvideosDBConn)
        Try
            sqlDA.Fill(dataTable)
        Catch ex As Exception
            logger.Error(String.Format("Get error: ""{0}"" with SQLCommand: ""{1}""", ex.Message, sqlCommand))
        End Try
    End Sub
    ''' <summary>
    ''' Adds TVShow informations to a Database.DBElement
    ''' </summary>
    ''' <param name="episode">Database.DBElement container to fill with TVShow informations</param>
    ''' <param name="tvshow">Optional the TVShow informations to add to _TVDB</param>
    ''' <remarks></remarks>
    Public Function AddTVShowInfoToDBElement(ByVal episode As DBElement, Optional ByVal tvshow As DBElement = Nothing) As DBElement
        Dim _tmpTVDBShow As DBElement

        If tvshow Is Nothing OrElse tvshow.TVShow Is Nothing Then
            _tmpTVDBShow = Load_TVShow(episode.ShowID, False, False)
        Else
            _tmpTVDBShow = tvshow
        End If

        episode.EpisodeSorting = _tmpTVDBShow.EpisodeSorting
        episode.EpisodeOrdering = _tmpTVDBShow.EpisodeOrdering
        episode.Language = _tmpTVDBShow.Language
        episode.ShowID = _tmpTVDBShow.ShowID
        episode.ShowPath = _tmpTVDBShow.ShowPath
        episode.Source = _tmpTVDBShow.Source
        episode.TVShow = _tmpTVDBShow.TVShow
        Return episode
    End Function

    Public Function GetAllTags() As String()
        Dim nList As New List(Of String)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT name FROM tag ORDER BY name;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("name").ToString.Trim)
                End While
            End Using
        End Using
        Return nList.ToArray
    End Function

    Public Function GetTVShowEpisodeSorting(ByVal ShowID As Long) As Enums.EpisodeSorting
        Dim sEpisodeSorting As Enums.EpisodeSorting = Enums.EpisodeSorting.Episode
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Concat("SELECT EpisodeSorting FROM tvshow WHERE idShow = ", ShowID, ";")
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    sEpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("EpisodeSorting")), Enums.EpisodeSorting)
                End While
            End Using
        End Using
        Return sEpisodeSorting
    End Function

    Public Function GetAllCountries() As String()
        Dim nList As New List(Of String)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT name FROM country ORDER BY name;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("name").ToString)
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
                      .TMDbId = nSet.MovieSet.UniqueIDs.TMDbId
                      })
        Next
        Return nList
    End Function

    Public Function GetAllSourceNames_Movie() As String()
        Dim nList As New List(Of String)
        nList.AddRange(Master.DB.Load_AllSources_Movie.Select(Function(f) f.Name))
        Return nList.ToArray
    End Function

    Public Function GetAllSourceNames_TVShow() As String()
        Dim nList As New List(Of String)
        nList.AddRange(Master.DB.Load_AllSources_TVShow.Select(Function(f) f.Name))
        Return nList.ToArray
    End Function

    Public Function GetAllVideoSources_Movie() As String()
        Dim nList As New List(Of String)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT DISTINCT videoSource FROM movie WHERE videoSource IS NOT '' ORDER BY videoSource;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("videoSource").ToString)
                End While
            End Using
        End Using
        Return nList.ToArray
    End Function

    Public Function GetAllVideoSources_TVEpisode() As String()
        Dim nList As New List(Of String)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT DISTINCT videoSource FROM episode WHERE videoSource IS NOT '' ORDER BY videoSource;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("videoSource").ToString)
                End While
            End Using
        End Using
        Return nList.ToArray
    End Function

    Public Function GetAllYears_Movie() As String()
        Dim nList As New List(Of String)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT DISTINCT year FROM movie WHERE year IS NOT '' ORDER BY year;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("year").ToString)
                End While
            End Using
        End Using
        Return nList.ToArray
    End Function

    Public Sub LoadAllGenres()
        Dim gList As New List(Of String)

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT name FROM genre ORDER BY name;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    gList.Add(SQLreader("name").ToString)
                End While
            End Using
        End Using

        For Each tGenre As String In gList
            Dim gMapping As GenreMapping = APIXML.GenreXML.Mappings.FirstOrDefault(Function(f) f.SearchString = tGenre)
            If gMapping Is Nothing Then
                'check if the tGenre is already existing in Gernes list
                Dim gProperty As GenreProperty = APIXML.GenreXML.Genres.FirstOrDefault(Function(f) f.Name = tGenre)
                If gProperty Is Nothing Then
                    APIXML.GenreXML.Genres.Add(New GenreProperty With {.IsNew = False, .Name = tGenre})
                End If
                'add a new mapping if tGenre is not in the MappingTable
                APIXML.GenreXML.Mappings.Add(New GenreMapping With {.isNew = False, .MappedTo = New List(Of String) From {tGenre}, .SearchString = tGenre})
            End If
        Next
    End Sub

    Public Function GetAllFilePaths() As List(Of String)
        Dim lstPaths As New List(Of String)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT path FROM file;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    lstPaths.Add(SQLreader("path").ToString.ToLower)
                End While
            End Using
        End Using
        Return lstPaths
    End Function

    Public Function GetAllTVShowPaths() As Hashtable
        Dim tList As New Hashtable
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idShow, path FROM tvshow;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    tList.Add(SQLreader("path").ToString.ToLower, SQLreader("idShow"))
                End While
            End Using
        End Using
        Return tList
    End Function

    Public Function GetAllTVShowTitles() As List(Of String)
        Dim lstTitles As New List(Of String)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT {0} FROM {1};",
                                                   Helpers.GetColumnName(ColumnName.Title),
                                                   Helpers.GetTableName(TableName.tvshow))
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    lstTitles.Add(SQLreader(Helpers.GetColumnName(ColumnName.Title)).ToString)
                End While
            End Using
        End Using
        lstTitles.Sort()
        Return lstTitles
    End Function

    Public Function GetTVSeasonIDFromEpisode(ByVal dbElement As DBElement) As Long
        Dim sID As Long = -1
        If dbElement.TVEpisode IsNot Nothing Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT idSeason FROM season WHERE idShow={0} AND season={1};", dbElement.ShowID, dbElement.TVEpisode.Season)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT idSeason FROM season WHERE idShow = {0} AND season = {1};", idTVShow, seasonnumber)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = dbCommand
                sqlCommand.ExecuteNonQuery()
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
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Concat("DROP VIEW IF EXISTS """, viewName, """;")
                sqlCommand.ExecuteNonQuery()
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
                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand.CommandText = String.Format("SELECT name, sql FROM sqlite_master WHERE type ='view' AND name='{0}';", viewName)
                    Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT name FROM sqlite_master WHERE type ='view' AND name = '{0}';", viewName)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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
                    Using sqlCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        sqlCommand.CommandText = String.Format("SELECT COUNT(*) FROM '{0}'", viewName)
                        iCount = Convert.ToInt32(sqlCommand.ExecuteScalar)
                        Return iCount
                    End Using
                Else
                    Using sqlCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        sqlCommand.CommandText = String.Format("SELECT COUNT(*) FROM '{0}' INNER JOIN episode ON ('{0}'.idShow = episode.idShow) WHERE NOT episode.idFile = -1", viewName)
                        iCount = Convert.ToInt32(sqlCommand.ExecuteScalar)
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

    Public Function GetViewList(ByVal contentType As Enums.ContentType, ByVal onlyCustomLists As Boolean) As List(Of String)
        Dim ViewList As New List(Of String)
        Dim listType As String = String.Empty

        Select Case contentType
            Case Enums.ContentType.TVEpisode
                listType = "episode-"
            Case Enums.ContentType.Movie
                listType = "movie-"
            Case Enums.ContentType.Movieset
                listType = "movieset-"
            Case Enums.ContentType.TVSeason
                listType = "season-"
            Case Enums.ContentType.TVShow
                listType = "tvshow-"
        End Select

        If Not String.IsNullOrEmpty(listType) OrElse contentType = Enums.ContentType.None Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT name FROM sqlite_master WHERE type ='view' AND name LIKE '{0}%';", listType)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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
            If ViewList.Contains("seasonlist") Then ViewList.Remove("seasonlist")
        End If

        ViewList.Sort()
        Return ViewList
    End Function

    Public Function GetMovies(Optional ByVal filter As SmartFilter.Filter = Nothing) As DataTable
        Dim nDataTable As New DataTable
        If filter Is Nothing Then
            filter = New SmartFilter.Filter(Enums.ContentType.Movie)
        End If
        FillDataTable(nDataTable, filter)

        'Table manipulation
        'add column "listTitle" and generate the value
        nDataTable.Columns.Add(Helpers.GetColumnName(ColumnName.ListTitle))
        For i As Integer = 0 To nDataTable.Rows.Count - 1
            nDataTable.Rows(i).Item(Helpers.GetColumnName(ColumnName.ListTitle)) = StringUtils.SortTokens_Movie(nDataTable.Rows(i).Item(Helpers.GetColumnName(ColumnName.Title)).ToString)
            nDataTable.Rows(i).Item(Helpers.GetColumnName(ColumnName.SortedTitle)) = StringUtils.SortTokens_Movie(nDataTable.Rows(i).Item(Helpers.GetColumnName(ColumnName.SortedTitle)).ToString)
        Next
        Return nDataTable
    End Function

    Public Function GetMoviesets(Optional ByVal filter As SmartFilter.Filter = Nothing) As DataTable
        Dim nDataTable As New DataTable
        If filter Is Nothing Then
            filter = New SmartFilter.Filter(Enums.ContentType.Movieset)
        End If
        FillDataTable(nDataTable, filter)

        'Table manipulation
        'add column "listTitle" and generate the value
        nDataTable.Columns.Add(Helpers.GetColumnName(ColumnName.ListTitle))
        For i As Integer = 0 To nDataTable.Rows.Count - 1
            nDataTable.Rows(i).Item(Helpers.GetColumnName(ColumnName.ListTitle)) = StringUtils.SortTokens_MovieSet(nDataTable.Rows(i).Item(Helpers.GetColumnName(ColumnName.Title)).ToString)
        Next
        Return nDataTable
    End Function

    Public Function GetMoviesources(Optional ByVal filter As SmartFilter.Filter = Nothing) As DataTable
        Dim nDataTable As New DataTable
        If filter Is Nothing Then
            filter = New SmartFilter.Filter(Enums.ContentType.Moviesource)
        End If
        FillDataTable(nDataTable, filter)
        Return nDataTable
    End Function

    Public Function GetTags(Optional ByVal filter As SmartFilter.Filter = Nothing) As DataTable
        Dim nDataTable As New DataTable
        If filter Is Nothing Then
            'filter = New SmartFilter.Filter(Enums.ContentType.Movie)
        End If
        FillDataTable(nDataTable, String.Format("SELECT * FROM {0} ORDER BY {1} COLLATE NOCASE;",
                                                Helpers.GetTableName(Database.TableName.tag),
                                                Helpers.GetColumnName(Database.ColumnName.Name)
                                                ))
        Return nDataTable
    End Function

    Public Function GetTVEpisodes(Optional ByVal filter As SmartFilter.Filter = Nothing) As DataTable
        Dim nDataTable As New DataTable
        If filter Is Nothing Then
            filter = New SmartFilter.Filter(Enums.ContentType.TVEpisode)
        End If
        FillDataTable(nDataTable, filter)
        Return nDataTable
    End Function

    Public Function GetTVSeasons(Optional ByVal filter As SmartFilter.Filter = Nothing) As DataTable
        Dim nDataTable As New DataTable
        If filter Is Nothing Then
            filter = New SmartFilter.Filter(Enums.ContentType.TVSeason)
        End If
        FillDataTable(nDataTable, filter)

        'Table manipulation
        'fill column "title" with generic season title if no season title has been specified
        For i = 0 To nDataTable.Rows.Count - 1
            If String.IsNullOrEmpty(nDataTable.Rows(i).Item(Helpers.GetColumnName(ColumnName.Title)).ToString) Then
                nDataTable.Rows(i).Item(Helpers.GetColumnName(ColumnName.Title)) = StringUtils.FormatSeasonTitle(CInt(nDataTable.Rows(i).Item(Helpers.GetColumnName(ColumnName.SeasonNumber))))
            End If
        Next
        Return nDataTable
    End Function

    Public Function GetTVShows(Optional ByVal filter As SmartFilter.Filter = Nothing) As DataTable
        Dim nDataTable As New DataTable
        If filter Is Nothing Then
            filter = New SmartFilter.Filter(Enums.ContentType.TVShow)
        End If
        FillDataTable(nDataTable, filter)

        'Table manipulation
        'add column "listTitle" and generate the value
        nDataTable.Columns.Add(Helpers.GetColumnName(ColumnName.ListTitle))
        For i As Integer = 0 To nDataTable.Rows.Count - 1
            nDataTable.Rows(i).Item(Helpers.GetColumnName(ColumnName.ListTitle)) = StringUtils.SortTokens_TV(nDataTable.Rows(i).Item(Helpers.GetColumnName(ColumnName.Title)).ToString)
            'dtTVShows.Rows(i).Item(Helpers.GetColumnName(ColumnName.SortedTitle)) = StringUtils.SortTokens_TV(dtTVShows.Rows(i).Item(Helpers.GetColumnName(ColumnName.SortedTitle)).ToString)
        Next
        Return nDataTable
    End Function

    Public Function GetTVShowources(Optional ByVal filter As SmartFilter.Filter = Nothing) As DataTable
        Dim nDataTable As New DataTable
        If filter Is Nothing Then
            filter = New SmartFilter.Filter(Enums.ContentType.TVShowsource)
        End If
        FillDataTable(nDataTable, filter)
        Return nDataTable
    End Function

    Public Function Load_AllMovies() As List(Of DBElement)
        Dim lstDBELement As New List(Of DBElement)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idMovie FROM movie ORDER BY title;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        lstDBELement.Add(Master.DB.Load_Movie(Convert.ToInt64(SQLreader("idMovie"))))
                    End While
                End If
            End Using
        End Using
        Return lstDBELement
    End Function

    Public Function Load_AllMoviesets() As List(Of DBElement)
        Dim lstDBELement As New List(Of DBElement)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idSet FROM movieset ORDER BY title;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        lstDBELement.Add(Master.DB.Load_Movieset(Convert.ToInt64(SQLreader("idSet"))))
                    End While
                End If
            End Using
        End Using
        Return lstDBELement
    End Function

    Public Function Load_AllTVEpisodes(ByVal idShow As Long, ByVal withShow As Boolean, Optional ByVal onlySeason As Integer = -1, Optional ByVal withMissingEpisodes As Boolean = False) As List(Of DBElement)
        If idShow < 0 Then Throw New ArgumentOutOfRangeException("idShow", "Value must be >= 0, was given: " & idShow)

        Dim _TVEpisodesList As New List(Of DBElement)

        Using SQLCount As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            If onlySeason = -1 Then
                SQLCount.CommandText = String.Concat("SELECT COUNT(idEpisode) AS eCount FROM episode WHERE idShow = ", idShow, If(withMissingEpisodes, ";", " AND NOT idFile = -1;"))
            Else
                SQLCount.CommandText = String.Concat("SELECT COUNT(idEpisode) AS eCount FROM episode WHERE idShow = ", idShow, " AND Season = ", onlySeason, If(withMissingEpisodes, ";", " AND NOT idFile = -1;"))
            End If
            Using SQLRCount As SQLiteDataReader = SQLCount.ExecuteReader
                If SQLRCount.HasRows Then
                    SQLRCount.Read()
                    If Convert.ToInt32(SQLRCount("eCount")) > 0 Then
                        Using sqlCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            If onlySeason = -1 Then
                                sqlCommand.CommandText = String.Concat("SELECT * FROM episode WHERE idShow = ", idShow, If(withMissingEpisodes, ";", " AND NOT idFile = -1;"))
                            Else
                                sqlCommand.CommandText = String.Concat("SELECT * FROM episode WHERE idShow = ", idShow, " AND Season = ", onlySeason, If(withMissingEpisodes, ";", " AND NOT idFile = -1;"))
                            End If
                            Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader
                                While sqlReader.Read
                                    _TVEpisodesList.Add(Master.DB.Load_TVEpisode(Convert.ToInt64(sqlReader("idEpisode")), withShow))
                                End While
                            End Using
                        End Using
                    End If
                End If
            End Using
        End Using

        Return _TVEpisodesList
    End Function

    Public Function Load_AllTVEpisodes_ByFileID(ByVal idFile As Long, ByVal withShow As Boolean) As List(Of DBElement)
        If idFile < 0 Then Throw New ArgumentOutOfRangeException("idFile", "Value must be >= 0, was given: " & idFile)

        Dim _TVEpisodesList As New List(Of DBElement)

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT idEpisode FROM episode WHERE idFile={0};", idFile)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        _TVEpisodesList.Add(Master.DB.Load_TVEpisode(Convert.ToInt64(SQLreader("idEpisode")), withShow))
                    End While
                End If
            End Using
        End Using

        Return _TVEpisodesList
    End Function

    Public Function Load_AllTVSeasons(ByVal idShow As Long) As List(Of DBElement)
        If idShow < 0 Then Throw New ArgumentOutOfRangeException("idShow", "Value must be >= 0, was given: " & idShow)

        Dim _TVSeasonsList As New List(Of DBElement)

        Using sqlCommand_count As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            sqlCommand_count.CommandText = String.Format("SELECT COUNT(idSeason) AS eCount FROM season WHERE idShow={0};", idShow)
            Using SQLRCount As SQLiteDataReader = sqlCommand_count.ExecuteReader
                If SQLRCount.HasRows Then
                    SQLRCount.Read()
                    If Convert.ToInt32(SQLRCount("eCount")) > 0 Then
                        Using sqlCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            sqlCommand.CommandText = String.Format("SELECT * FROM season WHERE idShow={0};", idShow)
                            Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader
                                While sqlReader.Read
                                    _TVSeasonsList.Add(Master.DB.Load_TVSeason(Convert.ToInt64(sqlReader("idSeason")), False, False))
                                End While
                            End Using
                        End Using
                    End If
                End If
            End Using
        End Using

        Return _TVSeasonsList
    End Function

    Public Function Load_AllTVShows(ByVal withSeasons As Boolean, ByVal withEpisodes As Boolean, Optional ByVal withMissingEpisodes As Boolean = False) As List(Of DBElement)
        Dim lstDBELement As New List(Of DBElement)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idShow FROM tvshow ORDER BY title;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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
    ''' Get all movie sources from DB
    ''' </summary>
    ''' <remarks></remarks>
    Public Function Load_AllSources_Movie() As List(Of DBSource)
        Dim lstSources As New List(Of DBSource)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idSource FROM moviesource ORDER BY name;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    lstSources.Add(Load_Source_Movie(CLng(SQLreader("idSource"))))
                End While
            End Using
        End Using
        Return lstSources
    End Function
    ''' <summary>
    ''' Get all tv show sources from DB
    ''' </summary>
    ''' <remarks></remarks>
    Public Function Load_AllSources_TVShow() As List(Of DBSource)
        Dim lstSources As New List(Of DBSource)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idSource FROM tvshowsource ORDER BY name;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    lstSources.Add(Load_Source_TVShow(CLng(SQLreader("idSource"))))
                End While
            End Using
        End Using
        Return lstSources
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
                    If Not DBNull.Value.Equals(sqlReader("idFile")) Then dbElement.FileItem = GetFileItem(Convert.ToInt64(sqlReader("idFile")))
                    If Not DBNull.Value.Equals(sqlReader("dateAdded")) Then dbElement.DateAdded = Convert.ToInt64(sqlReader("dateAdded"))
                    If Not DBNull.Value.Equals(sqlReader("dateModified")) Then dbElement.DateModified = Convert.ToInt64(sqlReader("dateModified"))
                    If Not DBNull.Value.Equals(sqlReader("isSingle")) Then dbElement.IsSingle = Convert.ToBoolean(sqlReader("isSingle"))
                    If Not DBNull.Value.Equals(sqlReader("trailerPath")) Then dbElement.Trailer.LocalFilePath = sqlReader("trailerPath").ToString
                    If Not DBNull.Value.Equals(sqlReader("nfoPath")) Then dbElement.NfoPath = sqlReader("nfoPath").ToString
                    If Not DBNull.Value.Equals(sqlReader("ethumbsPath")) Then dbElement.ExtrathumbsPath = sqlReader("ethumbsPath").ToString
                    If Not DBNull.Value.Equals(sqlReader("efanartsPath")) Then dbElement.ExtrafanartsPath = sqlReader("efanartsPath").ToString
                    If Not DBNull.Value.Equals(sqlReader("themePath")) Then dbElement.Theme.LocalFilePath = sqlReader("themePath").ToString

                    dbElement.Source = Load_Source_Movie(Convert.ToInt64(sqlReader("idSource")))

                    dbElement.IsMarked = Convert.ToBoolean(sqlReader("marked"))
                    dbElement.IsLocked = Convert.ToBoolean(sqlReader("locked"))
                    dbElement.ListTitle = StringUtils.SortTokens_Movie(sqlReader("title").ToString)
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
                        If Not DBNull.Value.Equals(sqlReader("year")) Then .Year = Convert.ToInt32(sqlReader("year"))
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

        'Art
        dbElement.ImagesContainer.Banner.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "banner")
        dbElement.ImagesContainer.ClearArt.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "clearart")
        dbElement.ImagesContainer.ClearLogo.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "clearlogo")
        dbElement.ImagesContainer.DiscArt.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "discart")
        dbElement.ImagesContainer.Fanart.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "fanart")
        dbElement.ImagesContainer.KeyArt.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "keyart")
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

        'Certifications
        dbElement.Movie.Certifications = GetCertificationsForItem(dbElement.ID, dbElement.ContentType)

        'Countries
        dbElement.Movie.Countries = GetCountriesForItem(dbElement.ID, dbElement.ContentType)

        'Credits
        dbElement.Movie.Credits = GetWritersForItem(dbElement.ID, dbElement.ContentType)

        'Directors
        dbElement.Movie.Directors = GetDirectorsForItem(dbElement.ID, dbElement.ContentType)

        'External subtitles
        dbElement.Subtitles = GetExternalSubtitlesForFileItem(dbElement.FileItem)

        'FileInfo
        dbElement.Movie.FileInfo = GetFileInfoForFileItem(dbElement.FileItem)

        'Genres
        dbElement.Movie.Genres = GetGenresForItem(dbElement.ID, dbElement.ContentType)

        'Moviesets
        dbElement.Movie.Sets.AddRange(GetMoviesetsForMovie(dbElement.ID))

        'Ratings
        dbElement.Movie.Ratings = GetRatingsForItem(dbElement.ID, dbElement.ContentType)

        'Studios
        dbElement.Movie.Studios = GetStudiosForItem(dbElement.ID, dbElement.ContentType)

        'Tags
        dbElement.Movie.Tags = GetTagsForItem(dbElement.ID, dbElement.ContentType)

        'TV Show Links
        dbElement.Movie.ShowLinks = GetTVShowLinksForMovie(dbElement.ID, dbElement.ContentType)

        'UniqueIDs
        dbElement.Movie.UniqueIDs = GetUniqueIDsForItem(dbElement.ID, dbElement.ContentType)

        'Check if the file is available and ready to edit
        dbElement.IsOnline = dbElement.FileItemSpecified AndAlso dbElement.FileItem.bIsOnline

        Return dbElement
    End Function
    ''' <summary>
    ''' Load all the information for a movieset.
    ''' </summary>
    ''' <param name="idSet">ID of the movieset to load, as stored in the database</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_Movieset(ByVal idSet As Long) As DBElement
        Dim dbElement As New DBElement(Enums.ContentType.Movieset)

        dbElement.ID = idSet
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT * FROM movieset WHERE idSet={0};", idSet)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("nfoPath")) Then dbElement.NfoPath = SQLreader("nfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("language")) Then dbElement.Language = SQLreader("language").ToString

                    dbElement.IsMarked = Convert.ToBoolean(SQLreader("marked"))
                    dbElement.IsLocked = Convert.ToBoolean(SQLreader("locked"))
                    dbElement.ListTitle = StringUtils.SortTokens_MovieSet(SQLreader("title").ToString)
                    dbElement.SortMethod = DirectCast(Convert.ToInt32(SQLreader("sortMethod")), Enums.SortMethod_MovieSet)

                    With dbElement.MovieSet
                        If Not DBNull.Value.Equals(SQLreader("plot")) Then .Plot = SQLreader("plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("title")) Then .Title = SQLreader("title").ToString
                        If Not DBNull.Value.Equals(SQLreader("language")) Then .Language = SQLreader("language").ToString
                        .OldTitle = .Title
                    End With
                End If
            End Using
        End Using

        'Movies in Set
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not Master.eSettings.MovieScraperCollectionsYAMJCompatibleSets Then
                If dbElement.SortMethod = Enums.SortMethod_MovieSet.Year Then
                    sqlCommand.CommandText = String.Concat("SELECT movieset_link.idMovie, movieset_link.set_order FROM movieset_link INNER JOIN movie ON (movieset_link.idMovie=movie.idMovie) ",
                                                               "WHERE idSet=", dbElement.ID, " ORDER BY movie.year;")
                ElseIf dbElement.SortMethod = Enums.SortMethod_MovieSet.Title Then
                    sqlCommand.CommandText = String.Concat("SELECT movieset_link.idMovie, movieset_link.set_order FROM movieset_link INNER JOIN movielist ON (movieset_link.idMovie=movielist.idMovie) ",
                                                               "WHERE idSet=", dbElement.ID, " ORDER BY movielist.sortedTitle COLLATE NOCASE;")
                End If
            Else
                sqlCommand.CommandText = String.Concat("SELECT movieset_link.idMovie, movieset_link.order FROM movieset_link ",
                                                           "WHERE idSet=", dbElement.ID, " ORDER BY set_order;")
            End If
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                Dim i As Integer = 0
                While SQLreader.Read
                    dbElement.MoviesInSet.Add(New MediaContainers.MovieInSet With {
                                                    .DBMovie = Load_Movie(Convert.ToInt64(SQLreader("idMovie"))),
                                                    .Order = i})
                    i += 1
                End While
            End Using
        End Using

        'Art
        dbElement.ImagesContainer.Banner.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "banner")
        dbElement.ImagesContainer.ClearArt.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "clearart")
        dbElement.ImagesContainer.ClearLogo.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "clearlogo")
        dbElement.ImagesContainer.DiscArt.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "discart")
        dbElement.ImagesContainer.Fanart.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "fanart")
        dbElement.ImagesContainer.KeyArt.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "keyart")
        dbElement.ImagesContainer.Landscape.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "landscape")
        dbElement.ImagesContainer.Poster.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "poster")

        'UniqueIDs
        dbElement.MovieSet.UniqueIDs = GetUniqueIDsForItem(dbElement.ID, dbElement.ContentType)

        Return dbElement
    End Function

    Public Function Load_Source_Movie(ByVal idSource As Long) As DBSource
        Dim nDBSource As New DBSource

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT * FROM moviesource WHERE idSource={0};", idSource)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    nDBSource.ID = idSource
                    nDBSource.Path = SQLreader("path").ToString
                    nDBSource.Name = SQLreader("name").ToString
                    nDBSource.ScanRecursive = Convert.ToBoolean(SQLreader("scanRecursive"))
                    nDBSource.UseFolderName = Convert.ToBoolean(SQLreader("useFoldername"))
                    nDBSource.IsSingle = Convert.ToBoolean(SQLreader("isSingle"))
                    nDBSource.Exclude = Convert.ToBoolean(SQLreader("exclude"))
                    nDBSource.GetYear = Convert.ToBoolean(SQLreader("getYear"))
                    nDBSource.Language = SQLreader("language").ToString
                Else
                    Return Nothing
                End If
            End Using
        End Using

        Return nDBSource
    End Function

    Public Function Load_Source_TVShow(ByVal idSource As Long) As DBSource
        Dim nDBSource As New DBSource

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT * FROM tvshowsource WHERE idSource={0};", idSource)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    nDBSource.ID = idSource
                    nDBSource.Path = SQLreader("path").ToString
                    nDBSource.Name = SQLreader("name").ToString
                    nDBSource.Language = SQLreader("language").ToString
                    nDBSource.EpisodeOrdering = DirectCast(Convert.ToInt32(SQLreader("episodeOrdering")), Enums.EpisodeOrdering)
                    nDBSource.Exclude = Convert.ToBoolean(SQLreader("exclude"))
                    nDBSource.EpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("episodeSorting")), Enums.EpisodeSorting)
                    nDBSource.IsSingle = Convert.ToBoolean(SQLreader("isSingle"))
                Else
                    Return Nothing
                End If
            End Using
        End Using

        Return nDBSource
    End Function
    ''' <summary>
    ''' Load all the information for a movietag.
    ''' </summary>
    ''' <param name="idTag">ID of the movietag to load, as stored in the database</param>
    ''' <returns>Database.DBElementTag object</returns>
    Public Function Load_Tag_Movie(ByVal idTag As Long) As Structures.DBMovieTag
        Dim _tagDB As New Structures.DBMovieTag
        _tagDB.ID = idTag
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT * FROM tag WHERE idTag={0};", idTag)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("idTag")) Then _tagDB.ID = CInt(SQLreader("idTag"))
                    If Not DBNull.Value.Equals(SQLreader("name")) Then _tagDB.Title = SQLreader("name").ToString
                End If
            End Using
        End Using

        _tagDB.Movies = New List(Of DBElement)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT * FROM tag_link WHERE idTag={0} AND media_type='movie';", _tagDB.ID)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    _tagDB.Movies.Add(Load_Movie(Convert.ToInt64(SQLreader("idMedia"))))
                End While
            End Using
        End Using
        Return _tagDB
    End Function
    ''' <summary>
    ''' Load all the information for a TV Episode
    ''' </summary>
    ''' <param name="idEpisode">Episode ID</param>
    ''' <param name="WithShow">>If <c>True</c>, also retrieve the TV Show information</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_TVEpisode(ByVal idEpisode As Long, ByVal withShow As Boolean) As DBElement
        Dim dbElement As New DBElement(Enums.ContentType.TVEpisode)

        dbElement.ID = idEpisode
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT * FROM episode WHERE idEpisode={0};", idEpisode)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("idFile")) Then dbElement.FileItem = GetFileItem(Convert.ToInt64(SQLreader("idFile")))
                    If Not DBNull.Value.Equals(SQLreader("nfoPath")) Then dbElement.NfoPath = SQLreader("nfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("idShow")) Then dbElement.ShowID = Convert.ToInt64(SQLreader("idShow"))
                    If Not DBNull.Value.Equals(SQLreader("dateAdded")) Then dbElement.DateAdded = Convert.ToInt64(SQLreader("dateAdded"))
                    If Not DBNull.Value.Equals(SQLreader("dateModified")) Then dbElement.DateModified = Convert.ToInt64(SQLreader("dateModified"))
                    If Not DBNull.Value.Equals(SQLreader("videoSource")) Then dbElement.VideoSource = SQLreader("videoSource").ToString

                    dbElement.Source = Load_Source_TVShow(Convert.ToInt64(SQLreader("idSource")))

                    dbElement.IsMarked = Convert.ToBoolean(SQLreader("marked"))
                    dbElement.IsLocked = Convert.ToBoolean(SQLreader("locked"))
                    dbElement.ShowID = Convert.ToInt64(SQLreader("idShow"))
                    dbElement.ShowPath = Load_Path_TVShow(Convert.ToInt64(SQLreader("idShow")))

                    With dbElement.TVEpisode
                        If Not DBNull.Value.Equals(SQLreader("title")) Then .Title = SQLreader("title").ToString
                        If Not DBNull.Value.Equals(SQLreader("season")) Then .Season = Convert.ToInt32(SQLreader("season"))
                        If Not DBNull.Value.Equals(SQLreader("episode")) Then .Episode = Convert.ToInt32(SQLreader("episode"))
                        If Not DBNull.Value.Equals(SQLreader("displaySeason")) Then .DisplaySeason = Convert.ToInt32(SQLreader("displaySeason"))
                        If Not DBNull.Value.Equals(SQLreader("displayEpisode")) Then .DisplayEpisode = Convert.ToInt32(SQLreader("displayEpisode"))
                        If Not DBNull.Value.Equals(SQLreader("aired")) Then .Aired = SQLreader("aired").ToString
                        If Not DBNull.Value.Equals(SQLreader("plot")) Then .Plot = SQLreader("plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("playCount")) Then .Playcount = Convert.ToInt32(SQLreader("playCount"))
                        If Not DBNull.Value.Equals(SQLreader("dateAdded")) Then .DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("dateAdded"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("runtime")) Then .Runtime = SQLreader("runtime").ToString
                        If Not DBNull.Value.Equals(SQLreader("videoSource")) Then .VideoSource = SQLreader("videoSource").ToString
                        If Not DBNull.Value.Equals(SQLreader("subEpisode")) Then .SubEpisode = Convert.ToInt32(SQLreader("subEpisode"))
                        If Not DBNull.Value.Equals(SQLreader("lastPlayed")) Then .LastPlayed = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("lastPlayed"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("userRating")) Then .UserRating = Convert.ToInt32(SQLreader("userRating"))
                        If Not DBNull.Value.Equals(SQLreader("dateModified")) Then .DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("dateModified"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("originalTitle")) Then .OriginalTitle = SQLreader("originalTitle").ToString
                    End With
                End If
            End Using
        End Using

        'Actors
        dbElement.TVEpisode.Actors = GetActorsForItem(dbElement.ID, dbElement.ContentType)

        'Art
        dbElement.ImagesContainer.Fanart.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "fanart")
        dbElement.ImagesContainer.Poster.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "thumb")

        'Credits
        dbElement.TVEpisode.Credits = GetWritersForItem(dbElement.ID, dbElement.ContentType)

        'Directors
        dbElement.TVEpisode.Directors = GetDirectorsForItem(dbElement.ID, dbElement.ContentType)

        'External subtitles
        dbElement.Subtitles = GetExternalSubtitlesForFileItem(dbElement.FileItem)

        'FileInfo
        dbElement.TVEpisode.FileInfo = GetFileInfoForFileItem(dbElement.FileItem)

        'GuestStars
        dbElement.TVEpisode.GuestStars = GetGuestStarsForItem(dbElement.ID, dbElement.ContentType)

        'Ratings
        dbElement.TVEpisode.Ratings = GetRatingsForItem(dbElement.ID, dbElement.ContentType)

        'UniqueIDs
        dbElement.TVEpisode.UniqueIDs = GetUniqueIDsForItem(dbElement.ID, dbElement.ContentType)

        'Show container
        If withShow Then
            dbElement = Master.DB.AddTVShowInfoToDBElement(dbElement)
        End If

        'Check if the file is available and ready to edit
        dbElement.IsOnline = dbElement.FileItemSpecified AndAlso dbElement.FileItem.bIsOnline

        Return dbElement
    End Function
    ''' <summary>
    ''' Load all the information for a TV Season
    ''' </summary>
    ''' <param name="idSeason">Season ID</param>
    ''' <param name="WithShow">If <c>True</c>, also retrieve the TV Show information</param>
    ''' <returns>Database.DBElement object</returns>
    ''' <remarks></remarks>
    Public Function Load_TVSeason(ByVal idSeason As Long, ByVal withShow As Boolean, ByVal withEpisodes As Boolean) As DBElement
        Dim _TVDB As New DBElement(Enums.ContentType.TVSeason)

        _TVDB.ID = idSeason
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT * FROM season WHERE idSeason={0};", _TVDB.ID)
            Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader
                If sqlReader.HasRows Then
                    sqlReader.Read()
                    _TVDB.IsLocked = CBool(sqlReader("locked"))
                    _TVDB.IsMarked = CBool(sqlReader("marked"))
                    _TVDB.ShowID = Convert.ToInt64(sqlReader("idShow"))
                    _TVDB.ShowPath = Load_Path_TVShow(Convert.ToInt64(sqlReader("idShow")))

                    With _TVDB.TVSeason
                        If Not DBNull.Value.Equals(sqlReader("aired")) Then .Aired = CStr(sqlReader("aired"))
                        If Not DBNull.Value.Equals(sqlReader("plot")) Then .Plot = CStr(sqlReader("plot"))
                        If Not DBNull.Value.Equals(sqlReader("season")) Then .Season = CInt(sqlReader("season"))
                        If Not DBNull.Value.Equals(sqlReader("title")) Then .Title = CStr(sqlReader("title"))
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
    ''' <param name="idShow">Show ID</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_TVShow(ByVal idShow As Long, ByVal withSeasons As Boolean, ByVal withEpisodes As Boolean, Optional ByVal withMissingEpisodes As Boolean = False) As DBElement
        Dim dbElement As New DBElement(Enums.ContentType.TVShow)
        dbElement.TVShow = New MediaContainers.TVShow

        If idShow < 0 Then Throw New ArgumentOutOfRangeException("idShow", "Value must be >= 0, was given: " & idShow)

        dbElement.ID = idShow
        dbElement.ShowID = idShow
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT * FROM tvshow WHERE idShow={0};", idShow)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("efanartsPath")) Then dbElement.ExtrafanartsPath = SQLreader("efanartsPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("language")) Then dbElement.Language = SQLreader("language").ToString
                    If Not DBNull.Value.Equals(SQLreader("nfoPath")) Then dbElement.NfoPath = SQLreader("nfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("path")) Then dbElement.ShowPath = SQLreader("path").ToString
                    If Not DBNull.Value.Equals(SQLreader("themePath")) Then dbElement.Theme.LocalFilePath = SQLreader("themePath").ToString
                    If Not DBNull.Value.Equals(SQLreader("dateAdded")) Then dbElement.DateAdded = Convert.ToInt64(SQLreader("dateAdded"))
                    If Not DBNull.Value.Equals(SQLreader("dateModified")) Then dbElement.DateModified = Convert.ToInt64(SQLreader("dateModified"))

                    dbElement.Source = Load_Source_TVShow(Convert.ToInt64(SQLreader("idSource")))

                    dbElement.IsMarked = Convert.ToBoolean(SQLreader("marked"))
                    dbElement.IsLocked = Convert.ToBoolean(SQLreader("locked"))
                    dbElement.ListTitle = StringUtils.SortTokens_TV(SQLreader("title").ToString)
                    dbElement.EpisodeOrdering = DirectCast(Convert.ToInt32(SQLreader("episodeOrdering")), Enums.EpisodeOrdering)
                    dbElement.EpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("episodeSorting")), Enums.EpisodeSorting)

                    With dbElement.TVShow
                        If Not DBNull.Value.Equals(SQLreader("title")) Then .Title = SQLreader("title").ToString
                        If Not DBNull.Value.Equals(SQLreader("episodeGuide")) Then .EpisodeGuide.URL = SQLreader("episodeGuide").ToString
                        If Not DBNull.Value.Equals(SQLreader("plot")) Then .Plot = SQLreader("plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("premiered")) Then .Premiered = SQLreader("premiered").ToString
                        If Not DBNull.Value.Equals(SQLreader("mpaa")) Then .MPAA = SQLreader("mpaa").ToString
                        If Not DBNull.Value.Equals(SQLreader("status")) Then .Status = SQLreader("status").ToString
                        If Not DBNull.Value.Equals(SQLreader("runtime")) Then .Runtime = SQLreader("runtime").ToString
                        If Not DBNull.Value.Equals(SQLreader("sortTitle")) Then .SortTitle = SQLreader("sortTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("language")) Then .Language = SQLreader("language").ToString
                        If Not DBNull.Value.Equals(SQLreader("originalTitle")) Then .OriginalTitle = SQLreader("originalTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("userRating")) Then .UserRating = Convert.ToInt32(SQLreader("userRating"))
                        If Not DBNull.Value.Equals(SQLreader("dateAdded")) Then .DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("dateAdded"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("dateModified")) Then .DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("dateModified"))).ToString("yyyy-MM-dd HH:mm:ss")
                    End With
                End If
            End Using
        End Using

        'Actors
        dbElement.TVShow.Actors = GetActorsForItem(dbElement.ID, dbElement.ContentType)

        'Art
        dbElement.ImagesContainer.Banner.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "banner")
        dbElement.ImagesContainer.CharacterArt.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "characterart")
        dbElement.ImagesContainer.ClearArt.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "clearart")
        dbElement.ImagesContainer.ClearLogo.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "clearlogo")
        dbElement.ImagesContainer.Fanart.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "fanart")
        dbElement.ImagesContainer.KeyArt.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "keyart")
        dbElement.ImagesContainer.Landscape.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "landscape")
        dbElement.ImagesContainer.Poster.LocalFilePath = GetArtForItem(dbElement.ID, dbElement.ContentType, "poster")
        If Not String.IsNullOrEmpty(dbElement.ExtrafanartsPath) AndAlso Directory.Exists(dbElement.ExtrafanartsPath) Then
            For Each ePath As String In Directory.GetFiles(dbElement.ExtrafanartsPath, "*.jpg")
                dbElement.ImagesContainer.Extrafanarts.Add(New MediaContainers.Image With {.LocalFilePath = ePath})
            Next
        End If

        'Certifications
        dbElement.TVShow.Certifications = GetCertificationsForItem(dbElement.ID, dbElement.ContentType)

        'Countries
        dbElement.TVShow.Countries = GetCountriesForItem(dbElement.ID, dbElement.ContentType)

        'Creators
        dbElement.TVShow.Creators = GetCreatorsForItem(dbElement.ID, dbElement.ContentType)

        'Genres
        dbElement.TVShow.Genres = GetGenresForItem(dbElement.ID, dbElement.ContentType)

        'Ratings
        dbElement.TVShow.Ratings = GetRatingsForItem(dbElement.ID, dbElement.ContentType)

        'Studios
        dbElement.TVShow.Studios = GetStudiosForItem(dbElement.ID, dbElement.ContentType)

        'Tags
        dbElement.TVShow.Tags = GetTagsForItem(dbElement.ID, dbElement.ContentType)

        'UniqueIDs
        dbElement.TVShow.UniqueIDs = GetUniqueIDsForItem(dbElement.ID, dbElement.ContentType)

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
        dbElement.IsOnline = Directory.Exists(dbElement.ShowPath)

        Return dbElement
    End Function
    ''' <summary>
    ''' Load all the information for a TV Show
    ''' </summary>
    ''' <param name="idShow">Show ID</param>
    ''' <returns>Database.DBElement object</returns>
    ''' <remarks></remarks>
    Public Function Load_TVShow_Full(ByVal idShow As Long) As DBElement
        If idShow < 0 Then Throw New ArgumentOutOfRangeException("idShow", "Value must be >= 0, was given: " & idShow)
        Return Master.DB.Load_TVShow(idShow, True, True, True)
    End Function

    Public Function Load_Path_TVShow(ByVal ShowID As Long) As String
        Dim ShowPath As String = String.Empty

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT path FROM tvshow WHERE idShow={0};", ShowID)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    ShowPath = SQLreader("path").ToString
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

    Public Sub RemoveExcludedPath(ByVal path As String, ByVal batchMode As Boolean)
        If Not String.IsNullOrEmpty(path) Then
            Dim SQLtransaction As SQLiteTransaction = Nothing
            If Not batchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using sqlCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Concat("DELETE FROM excludedpath WHERE path=(?);")
                Dim par_path As SQLiteParameter = sqlCommand.Parameters.Add("par_path", DbType.String, 0, "path")
                par_path.Value = path
                sqlCommand.ExecuteNonQuery()
            End Using
            If Not batchMode Then SQLtransaction.Commit()
        End If
    End Sub

    Private Sub RemoveGenresFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("genre_link", idMedia, contentType)
    End Sub

    Private Sub RemoveFileInfoFromFileItem(ByVal idFile As Long)
        RemoveFromTable("streamdetail", "idFile", idFile)
    End Sub

    Private Sub RemoveGuestStarsFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("gueststar_link", idMedia, contentType)
    End Sub

    Private Sub RemoveMissingEpisode(ByVal idShow As Long, ByVal season As Integer, ByVal episode As Integer)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("DELETE FROM episode WHERE idShow={0} AND season={1} AND episode={2} AND idFile=-1;", idShow, season, episode)
            sqlCommand.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub RemoveMoviesetFromMovie(ByVal idMovie As Long)
        RemoveFromTable("movieset_link", "idMovie", idMovie)
    End Sub

    Private Sub RemoveRatingsFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        RemoveFromTable("rating", idMedia, contentType)
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
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("DELETE FROM {0} WHERE idMedia={1} AND media_type='{2}';", table, idMedia, mediaType)
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
    ''' <summary>
    ''' Saves all information from a Database.DBElement object to the database
    ''' </summary>
    ''' <param name="dbElement">Media.Movie object to save to the database</param>
    ''' <param name="batchMode">Is the function already part of a transaction?</param>
    ''' <param name="toNFO">Save informations to NFO</param>
    ''' <param name="toDisk">Save Images, Themes and Trailers to disk</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Save_Movie(ByVal dbElement As DBElement, ByVal batchMode As Boolean, ByVal toNFO As Boolean, ByVal toDisk As Boolean, ByVal doSync As Boolean, ByVal forceFileCleanup As Boolean) As DBElement
        If dbElement.Movie Is Nothing Then Return dbElement

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        'add the path as first to get the idFile value
        dbElement.FileItem.ID = AddFileItem(dbElement.FileItem)

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "INSERT OR REPLACE INTO movie ("
            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "idMovie,")
            End If
            sqlCommand.CommandText = String.Concat(sqlCommand.CommandText,
                                                           "idSource,",
                                                           "idFile,",
                                                           "isSingle,",
                                                           "hasSub,",
                                                           "new,",
                                                           "marked,",
                                                           "locked,",
                                                           "title,",
                                                           "originalTitle,",
                                                           "sortTitle,",
                                                           "year,",
                                                           "mpaa,",
                                                           "top250,",
                                                           "outline,",
                                                           "plot,",
                                                           "tagline,",
                                                           "runtime,",
                                                           "releaseDate,",
                                                           "playCount,",
                                                           "trailer,",
                                                           "nfoPath,",
                                                           "trailerPath,",
                                                           "subPath,",
                                                           "ethumbsPath,",
                                                           "outOfTolerance,",
                                                           "videoSource,",
                                                           "dateAdded,",
                                                           "efanartsPath,",
                                                           "themePath,",
                                                           "dateModified,",
                                                           "markCustom1,",
                                                           "markCustom2,",
                                                           "markCustom3,",
                                                           "markCustom4,",
                                                           "hasSet,",
                                                           "lastPlayed,",
                                                           "language,",
                                                           "userRating",
                                                           ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?")
            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, ",?")
                Dim par_idMovie As SQLiteParameter = sqlCommand.Parameters.Add("par_idMovie", DbType.Int64, 0, "idMovie")
                par_idMovie.Value = dbElement.ID
            End If
            sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "); Select LAST_INSERT_ROWID() FROM movie;")

            Dim par_idSource As SQLiteParameter = sqlCommand.Parameters.Add("par_idSource", DbType.Int64, 0, "idSource")
            Dim par_idFile As SQLiteParameter = sqlCommand.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
            Dim par_isSingle As SQLiteParameter = sqlCommand.Parameters.Add("par_isSingle", DbType.Boolean, 0, "isSingle")
            Dim par_hasSub As SQLiteParameter = sqlCommand.Parameters.Add("par_hasSub", DbType.Boolean, 0, "hasSub")
            Dim par_new As SQLiteParameter = sqlCommand.Parameters.Add("par_new", DbType.Boolean, 0, "New")
            Dim par_marked As SQLiteParameter = sqlCommand.Parameters.Add("par_marked", DbType.Boolean, 0, "marked")
            Dim par_locked As SQLiteParameter = sqlCommand.Parameters.Add("par_locked", DbType.Boolean, 0, "locked")
            Dim par_title As SQLiteParameter = sqlCommand.Parameters.Add("par_title", DbType.String, 0, "title")
            Dim par_originalTitle As SQLiteParameter = sqlCommand.Parameters.Add("par_originalTitle", DbType.String, 0, "originalTitle")
            Dim par_sortTitle As SQLiteParameter = sqlCommand.Parameters.Add("par_sortTitle", DbType.String, 0, "sortTitle")
            Dim par_year As SQLiteParameter = sqlCommand.Parameters.Add("par_year", DbType.Int32, 0, "year")
            Dim par_mpaa As SQLiteParameter = sqlCommand.Parameters.Add("par_mpaa", DbType.String, 0, "mpaa")
            Dim par_top250 As SQLiteParameter = sqlCommand.Parameters.Add("par_top250", DbType.Int64, 0, "top250")
            Dim par_outline As SQLiteParameter = sqlCommand.Parameters.Add("par_outline", DbType.String, 0, "outline")
            Dim par_plot As SQLiteParameter = sqlCommand.Parameters.Add("par_plot", DbType.String, 0, "plot")
            Dim par_tagline As SQLiteParameter = sqlCommand.Parameters.Add("par_tagline", DbType.String, 0, "tagline")
            Dim par_runtime As SQLiteParameter = sqlCommand.Parameters.Add("par_runtime", DbType.String, 0, "runtime")
            Dim par_releaseDate As SQLiteParameter = sqlCommand.Parameters.Add("par_releaseDate", DbType.String, 0, "releaseDate")
            Dim par_playCount As SQLiteParameter = sqlCommand.Parameters.Add("par_playCount", DbType.Int64, 0, "playCount")
            Dim par_trailer As SQLiteParameter = sqlCommand.Parameters.Add("par_trailer", DbType.String, 0, "trailer")
            Dim par_nfoPath As SQLiteParameter = sqlCommand.Parameters.Add("par_nfoPath", DbType.String, 0, "nfoPath")
            Dim par_trailerPath As SQLiteParameter = sqlCommand.Parameters.Add("par_trailerPath", DbType.String, 0, "trailerPath")
            Dim par_subPath As SQLiteParameter = sqlCommand.Parameters.Add("par_subPath", DbType.String, 0, "subPath")
            Dim par_ethumbsPath As SQLiteParameter = sqlCommand.Parameters.Add("par_ethumbsPath", DbType.String, 0, "ethumbsPath")
            Dim par_outOfTolerance As SQLiteParameter = sqlCommand.Parameters.Add("par_outOfTolerance", DbType.Boolean, 0, "outOfTolerance")
            Dim par_videoSource As SQLiteParameter = sqlCommand.Parameters.Add("par_videoSource", DbType.String, 0, "videoSource")
            Dim par_dateAdded As SQLiteParameter = sqlCommand.Parameters.Add("par_dateAdded", DbType.Int64, 0, "dateAdded")
            Dim par_efanartsPath As SQLiteParameter = sqlCommand.Parameters.Add("par_efanartsPath", DbType.String, 0, "efanartsPath")
            Dim par_themePath As SQLiteParameter = sqlCommand.Parameters.Add("par_themePath", DbType.String, 0, "themePath")
            Dim par_dateModified As SQLiteParameter = sqlCommand.Parameters.Add("par_dateModified", DbType.Int64, 0, "dateModified")
            Dim par_markCustom1 As SQLiteParameter = sqlCommand.Parameters.Add("par_markCustom1", DbType.Boolean, 0, "markCustom1")
            Dim par_markCustom2 As SQLiteParameter = sqlCommand.Parameters.Add("par_markCustom2", DbType.Boolean, 0, "markCustom2")
            Dim par_markCustom3 As SQLiteParameter = sqlCommand.Parameters.Add("par_markCustom3", DbType.Boolean, 0, "markCustom3")
            Dim par_markCustom4 As SQLiteParameter = sqlCommand.Parameters.Add("par_markCustom4", DbType.Boolean, 0, "markCustom4")
            Dim par_hasSet As SQLiteParameter = sqlCommand.Parameters.Add("par_hasSet", DbType.Boolean, 0, "hasSet")
            Dim par_lastPlayed As SQLiteParameter = sqlCommand.Parameters.Add("par_lastPlayed", DbType.Int64, 0, "lastPlayed")
            Dim par_language As SQLiteParameter = sqlCommand.Parameters.Add("par_language", DbType.String, 0, "language")
            Dim par_userRating As SQLiteParameter = sqlCommand.Parameters.Add("par_userRating", DbType.Int64, 0, "userRating")

            'DateAdded
            Try
                If Not Master.eSettings.GeneralDateAddedIgnoreNFO AndAlso dbElement.Movie.DateAddedSpecified Then
                    Dim DateTimeAdded As Date = Date.ParseExact(dbElement.Movie.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    par_dateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    Select Case Master.eSettings.GeneralDateTime
                        Case Enums.DateTime.Now
                            par_dateAdded.Value = If(Not dbElement.IDSpecified, Functions.ConvertToUnixTimestamp(Date.Now), dbElement.DateAdded)
                        Case Enums.DateTime.ctime
                            Dim ctime As Date = File.GetCreationTime(dbElement.FileItem.FirstPathFromStack)
                            If ctime.Year > 1601 Then
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            Else
                                Dim mtime As Date = File.GetLastWriteTime(dbElement.FileItem.FirstPathFromStack)
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            End If
                        Case Enums.DateTime.mtime
                            Dim mtime As Date = File.GetLastWriteTime(dbElement.FileItem.FirstPathFromStack)
                            If mtime.Year > 1601 Then
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                Dim ctime As Date = File.GetCreationTime(dbElement.FileItem.FirstPathFromStack)
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                        Case Enums.DateTime.Newer
                            Dim mtime As Date = File.GetLastWriteTime(dbElement.FileItem.FirstPathFromStack)
                            Dim ctime As Date = File.GetCreationTime(dbElement.FileItem.FirstPathFromStack)
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

            'DateModified
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

            'LastPlayed
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
            If toNFO Then Info.SaveToNFO_Movie(dbElement, forceFileCleanup)
            If toDisk Then
                dbElement.ImagesContainer.SaveAllImages(dbElement, forceFileCleanup)
                dbElement.Movie.SaveAllActorThumbs(dbElement)
                dbElement.Theme.SaveAllThemes(dbElement, forceFileCleanup)
                dbElement.Trailer.SaveAllTrailers(dbElement, forceFileCleanup)
            End If

            par_isSingle.Value = dbElement.IsSingle

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

            par_locked.Value = dbElement.IsLocked
            par_idFile.Value = If(dbElement.FileItemSpecified, dbElement.FileItem.ID, -1)
            par_marked.Value = dbElement.IsMarked
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
                If .PlayCountSpecified Then 'has to be NOTHING instead of "0"
                    par_playCount.Value = .PlayCount
                End If
                par_plot.Value = .Plot
                par_releaseDate.Value = NumUtils.DateToISO8601Date(.ReleaseDate)
                par_runtime.Value = .Runtime
                par_sortTitle.Value = .SortTitle
                par_tagline.Value = .Tagline
                par_title.Value = .Title
                If .Top250Specified Then 'has to be NOTHING instead of "0"
                    par_top250.Value = .Top250
                End If
                par_trailer.Value = .Trailer
                If .YearSpecified Then 'has to be NOTHING instead of "0"
                    par_year.Value = .Year
                End If
            End With

            par_outOfTolerance.Value = dbElement.OutOfTolerance
            par_videoSource.Value = dbElement.VideoSource
            par_language.Value = dbElement.Language

            par_idSource.Value = dbElement.Source.ID

            If Not dbElement.IDSpecified Then
                If Master.eSettings.MovieGeneralMarkNew Then
                    par_marked.Value = True
                    dbElement.IsMarked = True
                End If
                Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    If sqlReader.Read Then
                        dbElement.ID = CLng(sqlReader(0))
                    Else
                        logger.Error("Something very wrong here: Save_Movie", dbElement.ToString)
                        dbElement.ID = -1
                        Return dbElement
                    End If
                End Using
            Else
                sqlCommand.ExecuteNonQuery()
            End If

            If dbElement.IDSpecified Then
                'Actors
                SetActorsForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Actors)

                'Art
                RemoveArtFromItem(dbElement.ID, dbElement.ContentType)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "banner", dbElement.ImagesContainer.Banner)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "clearart", dbElement.ImagesContainer.ClearArt)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "clearlogo", dbElement.ImagesContainer.ClearLogo)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "discart", dbElement.ImagesContainer.DiscArt)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "fanart", dbElement.ImagesContainer.Fanart)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "keyart", dbElement.ImagesContainer.KeyArt)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "landscape", dbElement.ImagesContainer.Landscape)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "poster", dbElement.ImagesContainer.Poster)

                'Certifications
                SetCertificationsForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Certifications)

                'Countries
                SetCountriesForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Countries)

                'Directors
                SetDirectorsForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Directors)

                'StreamDetails and external subtitles
                SetFileInfoForFileItem(dbElement.FileItem, dbElement.Movie.FileInfo)
                SetExternalSubtitlesForFileItem(dbElement.FileItem, dbElement.Subtitles)

                'Genres
                SetGenresForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Genres)

                'Movieset
                SetMoviesetsForMovie(dbElement, dbElement.Movie.Sets)

                'Ratings
                SetRatingsForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Ratings)

                'Studios
                SetStudiosForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Studios)

                'Tags
                SetTagsForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Tags)

                'TVShow Links
                SetTVShowLinksForMovie(dbElement)

                'UniqueIDs
                SetUniqueIDsForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.UniqueIDs)

                'Writers
                SetWritersForItem(dbElement.ID, dbElement.ContentType, dbElement.Movie.Credits)
            End If
        End Using

        'YAMJ watched file
        If dbElement.Movie.PlayCountSpecified AndAlso Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
            For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.MainWatchedFile)
                If Not File.Exists(a) Then
                    Dim fs As FileStream = File.Create(a)
                    fs.Close()
                End If
            Next
        ElseIf Not dbElement.Movie.PlayCountSpecified AndAlso Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
            For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.MainWatchedFile)
                If File.Exists(a) Then
                    File.Delete(a)
                End If
            Next
        End If

        If Not batchMode Then sqlTransaction.Commit()

        If doSync Then
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

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not bBatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "INSERT OR REPLACE INTO movieset ("
            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "idSet,")
            End If
            sqlCommand.CommandText = String.Concat(sqlCommand.CommandText,
                                                   "nfoPath,",
                                                   "plot,",
                                                   "title,",
                                                   "new,",
                                                   "marked,",
                                                   "locked,",
                                                   "sortMethod,",
                                                   "language",
                                                   ") VALUES (?,?,?,?,?,?,?,?")
            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, ",?")
                Dim par_idSet As SQLiteParameter = sqlCommand.Parameters.Add("par_idSet", DbType.Int64, 0, "idSet")
                par_idSet.Value = dbElement.ID
            End If
            sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "); Select LAST_INSERT_ROWID() FROM movieset;")

            Dim par_nfoPath As SQLiteParameter = sqlCommand.Parameters.Add("par_nfoPath", DbType.String, 0, "nfoPath")
            Dim par_plot As SQLiteParameter = sqlCommand.Parameters.Add("par_plot", DbType.String, 0, "plot")
            Dim par_title As SQLiteParameter = sqlCommand.Parameters.Add("par_title", DbType.String, 0, "title")
            Dim par_new As SQLiteParameter = sqlCommand.Parameters.Add("par_new", DbType.Boolean, 0, "New")
            Dim par_marked As SQLiteParameter = sqlCommand.Parameters.Add("par_marked", DbType.Boolean, 0, "marked")
            Dim par_locked As SQLiteParameter = sqlCommand.Parameters.Add("par_locked", DbType.Boolean, 0, "locked")
            Dim par_sortMethod As SQLiteParameter = sqlCommand.Parameters.Add("par_sortMethod", DbType.Int32, 0, "sortMethod")
            Dim par_language As SQLiteParameter = sqlCommand.Parameters.Add("par_language", DbType.String, 0, "language")

            'First let's save it to NFO, even because we will need the NFO path, also save Images
            'art Table be be linked later
            If bToNFO Then Info.SaveToNFO_MovieSet(dbElement)
            If bToDisk Then
                dbElement.ImagesContainer.SaveAllImages(dbElement, False)
            End If

            par_language.Value = dbElement.Language
            par_locked.Value = dbElement.IsLocked
            par_marked.Value = dbElement.IsMarked
            par_new.Value = Not dbElement.IDSpecified
            par_nfoPath.Value = dbElement.NfoPath
            par_plot.Value = dbElement.MovieSet.Plot
            par_sortMethod.Value = dbElement.SortMethod
            par_title.Value = dbElement.MovieSet.Title

            If Not dbElement.IDSpecified Then
                If Master.eSettings.MovieSetGeneralMarkNew Then
                    par_marked.Value = True
                    dbElement.IsMarked = True
                End If
                Using rdrMovieSet As SQLiteDataReader = sqlCommand.ExecuteReader()
                    If rdrMovieSet.Read Then
                        dbElement.ID = Convert.ToInt64(rdrMovieSet(0))
                    Else
                        logger.Error("Something very wrong here: Save_MovieSet", dbElement.ToString, "Error")
                        dbElement.ListTitle = "ERROR"
                        Return dbElement
                    End If
                End Using
            Else
                sqlCommand.ExecuteNonQuery()
            End If
        End Using

        'Art
        RemoveArtFromItem(dbElement.ID, dbElement.ContentType)
        SetArtForItem(dbElement.ID, dbElement.ContentType, "banner", dbElement.ImagesContainer.Banner)
        SetArtForItem(dbElement.ID, dbElement.ContentType, "clearart", dbElement.ImagesContainer.ClearArt)
        SetArtForItem(dbElement.ID, dbElement.ContentType, "clearlogo", dbElement.ImagesContainer.ClearLogo)
        SetArtForItem(dbElement.ID, dbElement.ContentType, "discart", dbElement.ImagesContainer.DiscArt)
        SetArtForItem(dbElement.ID, dbElement.ContentType, "fanart", dbElement.ImagesContainer.Fanart)
        SetArtForItem(dbElement.ID, dbElement.ContentType, "keyart", dbElement.ImagesContainer.KeyArt)
        SetArtForItem(dbElement.ID, dbElement.ContentType, "landscape", dbElement.ImagesContainer.Landscape)
        SetArtForItem(dbElement.ID, dbElement.ContentType, "poster", dbElement.ImagesContainer.Poster)

        'UniqueIDs 
        SetUniqueIDsForItem(dbElement.ID, dbElement.ContentType, dbElement.MovieSet.UniqueIDs)

        'save set informations to movies
        For Each tMovie In dbElement.MoviesInSet
            tMovie.DBMovie.Movie.AddSet(New MediaContainers.SetDetails With {
                                            .ID = dbElement.ID,
                                            .Order = tMovie.Order,
                                            .Plot = dbElement.MovieSet.Plot,
                                            .Title = dbElement.MovieSet.Title,
                                            .TMDbId = dbElement.MovieSet.UniqueIDs.TMDbId
                                            })
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, Nothing, False, tMovie.DBMovie)
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_Movie, Nothing, Nothing, False, tMovie.DBMovie)
            Save_Movie(tMovie.DBMovie, True, True, False, True, False)
            RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {tMovie.DBMovie.ID}))
        Next

        'remove set-information from movies which are no longer assigned to this set
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT idMovie FROM movieset_link WHERE idSet={0};", dbElement.ID)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    Dim rMovie = dbElement.MoviesInSet.FirstOrDefault(Function(f) f.DBMovie.ID = Convert.ToInt64(SQLreader("idMovie")))
                    If rMovie Is Nothing Then
                        'movie is no longer a part of this set
                        Dim tMovie As DBElement = Load_Movie(Convert.ToInt64(SQLreader("idMovie")))
                        tMovie.Movie.RemoveSet(dbElement.ID)
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, Nothing, False, tMovie)
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_Movie, Nothing, Nothing, False, tMovie)
                        Save_Movie(tMovie, True, True, False, True, False)
                        RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {tMovie.ID}))
                    End If
                End While
            End Using
        End Using

        If Not bBatchMode Then sqlTransaction.Commit()

        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Sync_MovieSet, Nothing, Nothing, False, dbElement)

        Return dbElement
    End Function

    Public Sub Save_Source_Movie(ByVal dbSource As DBSource)
        Using sqlTransaction As SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using sqlCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                sqlCommand.CommandText = "INSERT OR REPLACE INTO moviesource ("
                If dbSource.IDSpecified Then
                    sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "idSource,")
                End If
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText,
                                                       "path,",
                                                       "name,",
                                                       "scanRecursive,",
                                                       "useFoldername,",
                                                       "isSingle,",
                                                       "exclude,",
                                                       "getYear,",
                                                       "language",
                                                       ") VALUES (?,?,?,?,?,?,?,?")
                If dbSource.IDSpecified Then
                    sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, ",?")
                    Dim par_idSource As SQLiteParameter = sqlCommand.Parameters.Add("par_idSource", DbType.Int64, 0, "idSource")
                    par_idSource.Value = dbSource.ID
                End If
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "); Select LAST_INSERT_ROWID() FROM moviesource;")

                Dim par_path As SQLiteParameter = sqlCommand.Parameters.Add("par_path", DbType.String, 0, "path")
                Dim par_name As SQLiteParameter = sqlCommand.Parameters.Add("par_name", DbType.String, 0, "name")
                Dim par_scanRecursive As SQLiteParameter = sqlCommand.Parameters.Add("par_scanRecursive", DbType.Boolean, 0, "scanRecursive")
                Dim par_useFoldername As SQLiteParameter = sqlCommand.Parameters.Add("par_useFoldername", DbType.Boolean, 0, "useFoldername")
                Dim par_isSingle As SQLiteParameter = sqlCommand.Parameters.Add("par_isSingle", DbType.Boolean, 0, "isSingle")
                Dim par_exclude As SQLiteParameter = sqlCommand.Parameters.Add("par_exclude", DbType.Boolean, 0, "exclude")
                Dim par_getYear As SQLiteParameter = sqlCommand.Parameters.Add("par_getYear", DbType.Boolean, 0, "getYear")
                Dim par_language As SQLiteParameter = sqlCommand.Parameters.Add("par_language", DbType.String, 0, "language")
                par_path.Value = dbSource.Path
                par_name.Value = dbSource.Name
                par_scanRecursive.Value = dbSource.ScanRecursive
                par_useFoldername.Value = dbSource.UseFolderName
                par_isSingle.Value = dbSource.IsSingle
                par_exclude.Value = dbSource.Exclude
                par_getYear.Value = dbSource.GetYear
                If dbSource.LanguageSpecified Then
                    par_language.Value = dbSource.Language
                Else
                    par_language.Value = "en-US"
                End If

                sqlCommand.ExecuteNonQuery()
            End Using
            sqlTransaction.Commit()
        End Using
    End Sub

    Public Sub Save_Source_TVShow(ByVal dbSource As DBSource)
        Using sqlTransaction As SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using sqlCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                sqlCommand.CommandText = "INSERT OR REPLACE INTO tvshowsource ("
                If dbSource.IDSpecified Then
                    sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "idSource,")
                End If
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText,
                                                       "path,",
                                                       "name,",
                                                       "language,",
                                                       "episodeOrdering,",
                                                       "exclude,",
                                                       "episodeSorting,",
                                                       "isSingle",
                                                       ") VALUES (?,?,?,?,?,?,?")
                If dbSource.IDSpecified Then
                    sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, ",?")
                    Dim par_idSource As SQLiteParameter = sqlCommand.Parameters.Add("par_idSource", DbType.Int64, 0, "idSource")
                    par_idSource.Value = dbSource.ID
                End If
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "); Select LAST_INSERT_ROWID() FROM tvshowsource;")

                Dim par_path As SQLiteParameter = sqlCommand.Parameters.Add("par_path", DbType.String, 0, "path")
                Dim par_name As SQLiteParameter = sqlCommand.Parameters.Add("par_name", DbType.String, 0, "name")
                Dim par_language As SQLiteParameter = sqlCommand.Parameters.Add("par_language", DbType.String, 0, "language")
                Dim par_episodeOrdering As SQLiteParameter = sqlCommand.Parameters.Add("par_episodeOrdering", DbType.Int32, 0, "episodeOrdering")
                Dim par_exclude As SQLiteParameter = sqlCommand.Parameters.Add("par_exclude", DbType.Boolean, 0, "exclude")
                Dim par_episodeSorting As SQLiteParameter = sqlCommand.Parameters.Add("par_episodeSorting", DbType.Int32, 0, "episodeSorting")
                Dim par_isSingle As SQLiteParameter = sqlCommand.Parameters.Add("par_isSingle", DbType.Boolean, 0, "isSingle")
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

                sqlCommand.ExecuteNonQuery()
            End Using
            sqlTransaction.Commit()
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
        'TODO: remove this Function
        If _tagDB.ID = -1 Then bIsNew = True

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not bBatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If bIsNew Then
                sqlCommand.CommandText = String.Concat("INSERT OR REPLACE INTO tag (strTag) VALUES (?); SELECT LAST_INSERT_ROWID() FROM tag;")
            Else
                sqlCommand.CommandText = String.Concat("INSERT OR REPLACE INTO tag (",
                              "idTag, strTag) VALUES (?,?); SELECT LAST_INSERT_ROWID() FROM tag;")
                Dim parTagID As SQLiteParameter = sqlCommand.Parameters.Add("parTagID", DbType.Int64, 0, "idTag")
                parTagID.Value = _tagDB.ID
            End If
            Dim parTitle As SQLiteParameter = sqlCommand.Parameters.Add("parTitle", DbType.String, 0, "strTag")

            parTitle.Value = _tagDB.Title

            If bIsNew Then
                Using rdrMovieTag As SQLiteDataReader = sqlCommand.ExecuteReader()
                    If rdrMovieTag.Read Then
                        _tagDB.ID = CInt(Convert.ToInt64(rdrMovieTag(0)))
                    Else
                        logger.Error("Something very wrong here: Save_Tag_Movie", _tagDB.ToString, "Error")
                        _tagDB.Title = "SETERROR"
                        Return _tagDB
                    End If
                End Using
            Else
                sqlCommand.ExecuteNonQuery()
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
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Concat("SELECT idMedia, idTag FROM tag_link ",
                       "WHERE idTag = ", _tagDB.ID, " AND media_type = 'movie';")

                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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

    Public Sub Change_TVEpisode(ByVal episode As DBElement, ByVal listOfEpisodes As List(Of MediaContainers.EpisodeDetails), Optional ByVal batchMode As Boolean = False)
        Dim newEpisodesList As New List(Of DBElement)

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        'first step: remove all existing episode informations for this file and set it to "Missing"
        Delete_TVEpisode(episode.FileItem.FullPath, False, True)

        'second step: create new episode DBElements and save it to database
        For Each tEpisode As MediaContainers.EpisodeDetails In listOfEpisodes
            Dim newEpisode As New DBElement(Enums.ContentType.TVEpisode)
            newEpisode = CType(episode.CloneDeep, DBElement)
            newEpisode.FileItem.ID = -1
            newEpisode.ID = -1
            newEpisode.TVEpisode = tEpisode
            newEpisode.TVEpisode.FileInfo = episode.TVEpisode.FileInfo
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
        If Not batchMode Then sqlTransaction.Commit()
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
        RemoveMissingEpisode(dbElement.ShowID, dbElement.TVEpisode.Season, dbElement.TVEpisode.Episode)

        If dbElement.FileItemSpecified Then
            'add the path as first to get the idFile value
            dbElement.FileItem.ID = AddFileItem(dbElement.FileItem)
        End If

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "INSERT OR REPLACE INTO episode ("
            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "idEpisode,")
            End If
            sqlCommand.CommandText = String.Concat(sqlCommand.CommandText,
                                                   "idShow,",
                                                   "idFile,",
                                                   "idSource,",
                                                   "new,",
                                                   "marked,",
                                                   "locked,",
                                                   "title,",
                                                   "season,",
                                                   "episode,",
                                                   "plot,",
                                                   "aired,",
                                                   "nfoPath,",
                                                   "playCount,",
                                                   "displaySeason,",
                                                   "displayEpisode,",
                                                   "dateAdded,",
                                                   "runtime,",
                                                   "videoSource,",
                                                   "hasSub,",
                                                   "subEpisode,",
                                                   "lastPlayed,",
                                                   "userRating,",
                                                   "dateModified,",
                                                   "originalTitle",
                                                   ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?")
            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, ",?")
                Dim par_idEpisode As SQLiteParameter = sqlCommand.Parameters.Add("par_idEpisode", DbType.Int64, 0, "idEpisode")
                par_idEpisode.Value = dbElement.ID
            End If
            sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "); Select LAST_INSERT_ROWID() FROM episode;")

            Dim par_idShow As SQLiteParameter = sqlCommand.Parameters.Add("par_idShow", DbType.Int64, 0, "idShow")
            Dim par_idFile As SQLiteParameter = sqlCommand.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
            Dim par_idSource As SQLiteParameter = sqlCommand.Parameters.Add("par_idSource", DbType.Int64, 0, "idSource")
            Dim par_new As SQLiteParameter = sqlCommand.Parameters.Add("par_new", DbType.Boolean, 0, "New")
            Dim par_marked As SQLiteParameter = sqlCommand.Parameters.Add("par_marked", DbType.Boolean, 0, "marked")
            Dim par_locked As SQLiteParameter = sqlCommand.Parameters.Add("par_locked", DbType.Boolean, 0, "locked")
            Dim par_title As SQLiteParameter = sqlCommand.Parameters.Add("par_title", DbType.String, 0, "title")
            Dim par_season As SQLiteParameter = sqlCommand.Parameters.Add("par_season", DbType.String, 0, "season")
            Dim par_episode As SQLiteParameter = sqlCommand.Parameters.Add("par_episode", DbType.String, 0, "episode")
            Dim par_plot As SQLiteParameter = sqlCommand.Parameters.Add("par_plot", DbType.String, 0, "plot")
            Dim par_aired As SQLiteParameter = sqlCommand.Parameters.Add("par_aired", DbType.String, 0, "aired")
            Dim par_nfoPath As SQLiteParameter = sqlCommand.Parameters.Add("par_nfoPath", DbType.String, 0, "nfoPath")
            Dim par_playCount As SQLiteParameter = sqlCommand.Parameters.Add("par_playCount", DbType.Int64, 0, "playCount")
            Dim par_displaySeason As SQLiteParameter = sqlCommand.Parameters.Add("par_displaySeason", DbType.String, 0, "displaySeason")
            Dim par_displayEpisode As SQLiteParameter = sqlCommand.Parameters.Add("par_displayEpisode", DbType.String, 0, "displayEpisode")
            Dim par_dateAdded As SQLiteParameter = sqlCommand.Parameters.Add("par_dateAdded", DbType.Int64, 0, "dateAdded")
            Dim par_runtime As SQLiteParameter = sqlCommand.Parameters.Add("par_runtime", DbType.String, 0, "runtime")
            Dim par_videoSource As SQLiteParameter = sqlCommand.Parameters.Add("par_videoSource", DbType.String, 0, "videoSource")
            Dim par_hasSub As SQLiteParameter = sqlCommand.Parameters.Add("par_hasSub", DbType.Boolean, 0, "hasSub")
            Dim par_subEpisode As SQLiteParameter = sqlCommand.Parameters.Add("par_subEpisode", DbType.String, 0, "subEpisode")
            Dim par_lastPlayed As SQLiteParameter = sqlCommand.Parameters.Add("par_lastPlayed", DbType.Int64, 0, "lastPlayed")
            Dim par_userRating As SQLiteParameter = sqlCommand.Parameters.Add("par_userRating", DbType.Int64, 0, "userRating")
            Dim par_dateModified As SQLiteParameter = sqlCommand.Parameters.Add("par_dateModified", DbType.Int64, 0, "dateModified")
            Dim par_originalTitle As SQLiteParameter = sqlCommand.Parameters.Add("par_originalTitle", DbType.String, 0, "originalTitle")

            'DateAdded
            Try
                If Not Master.eSettings.GeneralDateAddedIgnoreNFO AndAlso dbElement.TVEpisode.DateAddedSpecified Then
                    Dim DateTimeAdded As Date = Date.ParseExact(dbElement.TVEpisode.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    par_dateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    Select Case Master.eSettings.GeneralDateTime
                        Case Enums.DateTime.Now
                            par_dateAdded.Value = If(Not dbElement.IDSpecified, Functions.ConvertToUnixTimestamp(Date.Now), dbElement.DateAdded)
                        Case Enums.DateTime.ctime
                            Dim ctime As Date = File.GetCreationTime(dbElement.FileItem.FirstPathFromStack)
                            If ctime.Year > 1601 Then
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            Else
                                Dim mtime As Date = File.GetLastWriteTime(dbElement.FileItem.FirstPathFromStack)
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            End If
                        Case Enums.DateTime.mtime
                            Dim mtime As Date = File.GetLastWriteTime(dbElement.FileItem.FirstPathFromStack)
                            If mtime.Year > 1601 Then
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                Dim ctime As Date = File.GetCreationTime(dbElement.FileItem.FirstPathFromStack)
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                        Case Enums.DateTime.Newer
                            Dim mtime As Date = File.GetLastWriteTime(dbElement.FileItem.FirstPathFromStack)
                            Dim ctime As Date = File.GetCreationTime(dbElement.FileItem.FirstPathFromStack)
                            If mtime > ctime Then
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                par_dateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                    End Select
                End If
                dbElement.TVEpisode.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_dateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            Catch ex As Exception
                par_dateAdded.Value = If(Not dbElement.IDSpecified, Functions.ConvertToUnixTimestamp(Date.Now), dbElement.DateAdded)
                dbElement.TVEpisode.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_dateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            End Try

            'DateModified
            Try
                If Not dbElement.IDSpecified AndAlso dbElement.TVEpisode.DateModifiedSpecified Then
                    Dim DateTimeDateModified As Date = Date.ParseExact(dbElement.TVEpisode.DateModified, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    par_dateModified.Value = Functions.ConvertToUnixTimestamp(DateTimeDateModified)
                ElseIf dbElement.IDSpecified Then
                    par_dateModified.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                End If
                If par_dateModified.Value IsNot Nothing Then
                    dbElement.TVEpisode.DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_dateModified.Value)).ToString("yyyy-MM-dd HH:mm:ss")
                Else
                    dbElement.TVEpisode.DateModified = String.Empty
                End If
            Catch
                par_dateModified.Value = If(Not dbElement.IDSpecified, Functions.ConvertToUnixTimestamp(Date.Now), dbElement.DateModified)
                dbElement.TVEpisode.DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_dateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            End Try

            'LastPlayed
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
                par_lastPlayed.Value = DateTimeLastPlayedUnix
            Else
                par_lastPlayed.Value = Nothing 'need to be NOTHING instead of 0
                dbElement.TVEpisode.LastPlayed = String.Empty
            End If

            'First let's save it to NFO, even because we will need the NFO path, also save Images
            'art Table be be linked later
            If dbElement.FileItemSpecified Then
                If bToNFO Then Info.SaveToNFO_TVEpisode(dbElement)
                If bToDisk Then
                    dbElement.ImagesContainer.SaveAllImages(dbElement, False)
                    dbElement.TVEpisode.SaveAllActorThumbs(dbElement)
                End If
            End If

            par_idShow.Value = dbElement.ShowID
            par_nfoPath.Value = dbElement.NfoPath
            par_hasSub.Value = (dbElement.Subtitles IsNot Nothing AndAlso dbElement.Subtitles.Count > 0) OrElse dbElement.TVEpisode.FileInfo.StreamDetails.Subtitle.Count > 0
            par_new.Value = bForceIsNewFlag OrElse Not dbElement.IDSpecified
            par_marked.Value = dbElement.IsMarked
            par_idFile.Value = If(dbElement.FileItemSpecified, dbElement.FileItem.ID, -1)
            par_locked.Value = dbElement.IsLocked
            par_idSource.Value = dbElement.Source.ID
            par_videoSource.Value = dbElement.VideoSource

            With dbElement.TVEpisode
                par_title.Value = .Title
                par_season.Value = .Season
                par_episode.Value = .Episode
                par_displaySeason.Value = .DisplaySeason
                par_displayEpisode.Value = .DisplayEpisode
                par_userRating.Value = .UserRating
                par_plot.Value = .Plot
                par_aired.Value = NumUtils.DateToISO8601Date(.Aired)
                If .PlaycountSpecified Then 'need to be NOTHING instead of "0"
                    par_playCount.Value = .Playcount
                End If
                If .SubEpisodeSpecified Then
                    par_subEpisode.Value = .SubEpisode
                End If
                par_originalTitle.Value = .OriginalTitle
            End With

            If Not dbElement.IDSpecified Then
                If Master.eSettings.TVGeneralMarkNewEpisodes Then
                    par_marked.Value = True
                    dbElement.IsMarked = True
                End If
                Using rdrTVEp As SQLiteDataReader = sqlCommand.ExecuteReader()
                    If rdrTVEp.Read Then
                        dbElement.ID = Convert.ToInt64(rdrTVEp(0))
                    Else
                        logger.Error("Something very wrong here: Save_TVEpisode", dbElement.ToString, "Error")
                        dbElement.ID = -1
                        Return dbElement
                        Exit Function
                    End If
                End Using
            Else
                sqlCommand.ExecuteNonQuery()
            End If

            If dbElement.IDSpecified Then
                'Actors 
                SetActorsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVEpisode.Actors)

                'Art
                RemoveArtFromItem(dbElement.ID, dbElement.ContentType)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "fanart", dbElement.ImagesContainer.Fanart)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "thumb", dbElement.ImagesContainer.Poster)

                'Directors
                SetDirectorsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVEpisode.Directors)

                'Guest Stars
                SetGuestStarsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVEpisode.GuestStars)

                'Ratings
                SetRatingsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVEpisode.Ratings)

                'StreamDetails and external subtitles
                SetFileInfoForFileItem(dbElement.FileItem, dbElement.TVEpisode.FileInfo)
                SetExternalSubtitlesForFileItem(dbElement.FileItem, dbElement.Subtitles)


                'UniqueIDs 
                SetUniqueIDsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVEpisode.UniqueIDs)

                'Writers
                SetWritersForItem(dbElement.ID, dbElement.ContentType, dbElement.TVEpisode.Credits)

                If bDoSeasonCheck Then
                    Using SQLSeasonCheck As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        SQLSeasonCheck.CommandText = String.Format("SELECT idSeason FROM season WHERE idShow = {0} AND Season = {1};", dbElement.ShowID, dbElement.TVEpisode.Season)
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

        If dbElement.FileItemSpecified AndAlso bDoSync Then
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
        Dim idSeason As Long = -1

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT idSeason FROM season WHERE idShow = {0} AND Season = {1};", dbElement.ShowID, dbElement.TVSeason.Season)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    doesExist = True
                    idSeason = CLng(SQLreader("idSeason"))
                    Exit While
                End While
            End Using
        End Using

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not bBatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        If Not doesExist Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Concat("INSERT INTO season (",
                                                       "idSeason,",
                                                       "idShow,",
                                                       "season,",
                                                       "title,",
                                                       "locked,",
                                                       "marked,",
                                                       "new,",
                                                       "aired,",
                                                       "plot",
                                                       ") VALUES (NULL,?,?,?,?,?,?,?,?); SELECT LAST_INSERT_ROWID() FROM season;")
                Dim par_idShow As SQLiteParameter = sqlCommand.Parameters.Add("par_idShow", DbType.Int64, 0, "idShow")
                Dim par_season As SQLiteParameter = sqlCommand.Parameters.Add("par_season", DbType.Int64, 0, "season")
                Dim par_title As SQLiteParameter = sqlCommand.Parameters.Add("par_title", DbType.String, 0, "title")
                Dim par_locked As SQLiteParameter = sqlCommand.Parameters.Add("par_locked", DbType.Boolean, 0, "locked")
                Dim par_marked As SQLiteParameter = sqlCommand.Parameters.Add("par_marked", DbType.Boolean, 0, "marked")
                Dim par_new As SQLiteParameter = sqlCommand.Parameters.Add("par_new", DbType.Boolean, 0, "New")
                Dim par_aired As SQLiteParameter = sqlCommand.Parameters.Add("par_aired", DbType.String, 0, "aired")
                Dim par_plot As SQLiteParameter = sqlCommand.Parameters.Add("par_plot", DbType.String, 0, "plot")
                par_idShow.Value = dbElement.ShowID
                par_season.Value = dbElement.TVSeason.Season
                par_title.Value = dbElement.TVSeason.Title
                par_locked.Value = dbElement.IsLocked
                par_marked.Value = dbElement.IsMarked
                par_new.Value = True
                par_aired.Value = dbElement.TVSeason.Aired
                par_plot.Value = dbElement.TVSeason.Plot
                idSeason = CLng(sqlCommand.ExecuteScalar())
            End Using
        Else
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("UPDATE season SET title=?, locked=?, marked=?, New=?, aired=?, plot=? WHERE idSeason={0}", idSeason)
                Dim par_title As SQLiteParameter = sqlCommand.Parameters.Add("par_title", DbType.String, 0, "title")
                Dim par_locked As SQLiteParameter = sqlCommand.Parameters.Add("par_locked", DbType.Boolean, 0, "locked")
                Dim par_marked As SQLiteParameter = sqlCommand.Parameters.Add("par_marked", DbType.Boolean, 0, "marked")
                Dim par_new As SQLiteParameter = sqlCommand.Parameters.Add("par_new", DbType.Boolean, 0, "New")
                Dim par_aired As SQLiteParameter = sqlCommand.Parameters.Add("par_aired", DbType.String, 0, "aired")
                Dim par_plot As SQLiteParameter = sqlCommand.Parameters.Add("par_plot", DbType.String, 0, "plot")
                par_title.Value = dbElement.TVSeason.Title
                par_locked.Value = dbElement.IsLocked
                par_marked.Value = dbElement.IsMarked
                par_new.Value = False
                par_aired.Value = dbElement.TVSeason.Aired
                par_plot.Value = dbElement.TVSeason.Plot
                sqlCommand.ExecuteNonQuery()
            End Using
        End If

        dbElement.ID = idSeason


        If bToDisk Then dbElement.ImagesContainer.SaveAllImages(dbElement, False)

        'Art
        RemoveArtFromItem(dbElement.ID, dbElement.ContentType)
        SetArtForItem(dbElement.ID, dbElement.ContentType, "banner", dbElement.ImagesContainer.Banner)
        SetArtForItem(dbElement.ID, dbElement.ContentType, "fanart", dbElement.ImagesContainer.Fanart)
        SetArtForItem(dbElement.ID, dbElement.ContentType, "landscape", dbElement.ImagesContainer.Landscape)
        SetArtForItem(dbElement.ID, dbElement.ContentType, "poster", dbElement.ImagesContainer.Poster)

        'UniqueIDs 
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

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "INSERT Or REPLACE INTO tvshow ("
            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "idShow,")
            End If
            sqlCommand.CommandText = String.Concat(sqlCommand.CommandText,
                                                   "idSource,",
                                                   "path,",
                                                   "New,",
                                                   "marked,",
                                                   "locked,",
                                                   "episodeGuide,",
                                                   "plot,",
                                                   "premiered,",
                                                   "mpaa,",
                                                   "nfoPath,",
                                                   "language,",
                                                   "episodeOrdering,",
                                                   "status,",
                                                   "themePath,",
                                                   "efanartsPath,",
                                                   "runtime,",
                                                   "title,",
                                                   "episodeSorting,",
                                                   "sortTitle,",
                                                   "originalTitle,",
                                                   "userRating,",
                                                   "dateModified,",
                                                   "dateAdded",
                                                   ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?")
            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, ",?")
                Dim par_idShow As SQLiteParameter = sqlCommand.Parameters.Add("par_idShow", DbType.Int64, 0, "idShow")
                par_idShow.Value = dbElement.ID
            End If
            sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "); Select LAST_INSERT_ROWID() FROM tvshow;")

            Dim par_idSource As SQLiteParameter = sqlCommand.Parameters.Add("par_idSource", DbType.Int64, 0, "idSource")
            Dim par_path As SQLiteParameter = sqlCommand.Parameters.Add("par_path", DbType.String, 0, "path")
            Dim par_new As SQLiteParameter = sqlCommand.Parameters.Add("par_new", DbType.Boolean, 0, "New")
            Dim par_marked As SQLiteParameter = sqlCommand.Parameters.Add("par_marked", DbType.Boolean, 0, "marked")
            Dim par_locked As SQLiteParameter = sqlCommand.Parameters.Add("par_locked", DbType.Boolean, 0, "locked")
            Dim par_episodeGuide As SQLiteParameter = sqlCommand.Parameters.Add("par_episodeGuide", DbType.String, 0, "episodeGuide")
            Dim par_plot As SQLiteParameter = sqlCommand.Parameters.Add("par_plot", DbType.String, 0, "plot")
            Dim par_premiered As SQLiteParameter = sqlCommand.Parameters.Add("par_premiered", DbType.String, 0, "premiered")
            Dim par_mpaa As SQLiteParameter = sqlCommand.Parameters.Add("par_mpaa", DbType.String, 0, "mpaa")
            Dim par_nfoPath As SQLiteParameter = sqlCommand.Parameters.Add("par_nfoPath", DbType.String, 0, "nfoPath")
            Dim par_language As SQLiteParameter = sqlCommand.Parameters.Add("par_language", DbType.String, 0, "language")
            Dim par_episodeOrdering As SQLiteParameter = sqlCommand.Parameters.Add("par_episodeOrdering", DbType.Int32, 0, "episodeOrdering")
            Dim par_status As SQLiteParameter = sqlCommand.Parameters.Add("par_status", DbType.String, 0, "status")
            Dim par_themePath As SQLiteParameter = sqlCommand.Parameters.Add("par_themePath", DbType.String, 0, "themePath")
            Dim par_efanartsPath As SQLiteParameter = sqlCommand.Parameters.Add("par_efanartsPath", DbType.String, 0, "efanartsPath")
            Dim par_runtime As SQLiteParameter = sqlCommand.Parameters.Add("par_runtime", DbType.String, 0, "runtime")
            Dim par_title As SQLiteParameter = sqlCommand.Parameters.Add("par_title", DbType.String, 0, "title")
            Dim par_episodeSorting As SQLiteParameter = sqlCommand.Parameters.Add("par_episodeSorting", DbType.Int32, 0, "episodeSorting")
            Dim par_sortTitle As SQLiteParameter = sqlCommand.Parameters.Add("par_sortTitle", DbType.String, 0, "sortTitle")
            Dim par_originalTitle As SQLiteParameter = sqlCommand.Parameters.Add("par_originalTitle", DbType.String, 0, "originalTitle")
            Dim par_userRating As SQLiteParameter = sqlCommand.Parameters.Add("par_userRating", DbType.Int64, 0, "userRating")
            Dim par_dateModified As SQLiteParameter = sqlCommand.Parameters.Add("par_dateModified", DbType.Int64, 0, "dateModified")
            Dim par_dateAdded As SQLiteParameter = sqlCommand.Parameters.Add("par_dateAdded", DbType.Int64, 0, "dateAdded")

            'DateAdded
            Try
                If Not Master.eSettings.GeneralDateAddedIgnoreNFO AndAlso dbElement.TVShow.DateAddedSpecified Then
                    Dim DateTimeAdded As Date = Date.ParseExact(dbElement.TVShow.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    par_dateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    par_dateAdded.Value = If(Not dbElement.IDSpecified, Functions.ConvertToUnixTimestamp(Date.Now), dbElement.DateAdded)
                End If
                dbElement.TVShow.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_dateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            Catch
                par_dateAdded.Value = If(Not dbElement.IDSpecified, Functions.ConvertToUnixTimestamp(Date.Now), dbElement.DateAdded)
                dbElement.TVShow.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_dateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            End Try

            'DateModified
            Try
                If Not dbElement.IDSpecified AndAlso dbElement.TVShow.DateModifiedSpecified Then
                    Dim DateTimeDateModified As Date = Date.ParseExact(dbElement.TVShow.DateModified, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    par_dateModified.Value = Functions.ConvertToUnixTimestamp(DateTimeDateModified)
                ElseIf dbElement.IDSpecified Then
                    par_dateModified.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                End If
                If par_dateModified.Value IsNot Nothing Then
                    dbElement.TVShow.DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_dateModified.Value)).ToString("yyyy-MM-dd HH:mm:ss")
                Else
                    dbElement.TVShow.DateModified = String.Empty
                End If
            Catch
                par_dateModified.Value = If(Not dbElement.IDSpecified, Functions.ConvertToUnixTimestamp(Date.Now), dbElement.DateModified)
                dbElement.TVShow.DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_dateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            End Try

            With dbElement.TVShow
                par_userRating.Value = .UserRating
                par_episodeGuide.Value = .EpisodeGuide.URL
                par_mpaa.Value = .MPAA
                par_originalTitle.Value = .OriginalTitle
                par_plot.Value = .Plot
                par_premiered.Value = NumUtils.DateToISO8601Date(.Premiered)
                par_runtime.Value = .Runtime
                par_sortTitle.Value = .SortTitle
                par_status.Value = .Status
                par_title.Value = .Title
            End With

            'First let's save it to NFO, even because we will need the NFO path
            'Also Save Images to get ExtrafanartsPath
            'art Table be be linked later
            If bToNFO Then Info.SaveToNFO_TVShow(dbElement)
            If bToDisk Then
                dbElement.ImagesContainer.SaveAllImages(dbElement, False)
                dbElement.Theme.SaveAllThemes(dbElement, False)
                dbElement.TVShow.SaveAllActorThumbs(dbElement)
            End If

            par_efanartsPath.Value = dbElement.ExtrafanartsPath
            par_nfoPath.Value = dbElement.NfoPath
            par_themePath.Value = If(Not String.IsNullOrEmpty(dbElement.Theme.LocalFilePath), dbElement.Theme.LocalFilePath, String.Empty)
            par_path.Value = dbElement.ShowPath

            par_new.Value = Not dbElement.IDSpecified
            par_marked.Value = dbElement.IsMarked
            par_locked.Value = dbElement.IsLocked
            par_idSource.Value = dbElement.Source.ID
            par_language.Value = dbElement.Language
            par_episodeOrdering.Value = dbElement.EpisodeOrdering
            par_episodeSorting.Value = dbElement.EpisodeSorting

            If Not dbElement.IDSpecified Then
                If Master.eSettings.TVGeneralMarkNewShows Then
                    par_marked.Value = True
                    dbElement.IsMarked = True
                End If
                Using rdrTVShow As SQLiteDataReader = sqlCommand.ExecuteReader()
                    If rdrTVShow.Read Then
                        dbElement.ID = Convert.ToInt64(rdrTVShow(0))
                        dbElement.ShowID = dbElement.ID
                    Else
                        logger.Error("Something very wrong here: Save_TVShow", dbElement.ToString, "Error")
                        dbElement.ID = -1
                        dbElement.ShowID = dbElement.ID
                        Return dbElement
                        Exit Function
                    End If
                End Using
            Else
                sqlCommand.ExecuteNonQuery()
            End If

            If dbElement.IDSpecified Then
                'Actors
                SetActorsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.Actors)

                'Art
                RemoveArtFromItem(dbElement.ID, dbElement.ContentType)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "banner", dbElement.ImagesContainer.Banner)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "characterart", dbElement.ImagesContainer.CharacterArt)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "clearart", dbElement.ImagesContainer.ClearArt)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "clearlogo", dbElement.ImagesContainer.ClearLogo)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "fanart", dbElement.ImagesContainer.Fanart)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "keyart", dbElement.ImagesContainer.KeyArt)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "landscape", dbElement.ImagesContainer.Landscape)
                SetArtForItem(dbElement.ID, dbElement.ContentType, "poster", dbElement.ImagesContainer.Poster)

                'Certifications
                SetCertificationsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.Certifications)

                'Creators
                SetCreatorsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.Creators)

                'Countries
                SetCountriesForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.Countries)

                'Genres
                SetGenresForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.Genres)

                'Ratings
                SetRatingsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.Ratings)

                'Studios
                SetStudiosForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.Studios)

                'UniqueIDs
                SetUniqueIDsForItem(dbElement.ID, dbElement.ContentType, dbElement.TVShow.UniqueIDs)

                'Tags
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

    Private Sub SetArtForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal artType As String, ByVal image As MediaContainers.Image)
        If image.LocalFilePathSpecified Then
            AddArt(idMedia, contentType, artType, image.LocalFilePath, image.Width, image.Height)
        End If
    End Sub

    Private Sub SetActorsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal actors As List(Of MediaContainers.Person))
        RemoveActorsFromItem(idMedia, contentType)
        Dim iOrder As Integer = 0
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As MediaContainers.Person In actors
                AddToPersonLinkTable("actor_link", AddPerson(entry.Name, entry.URLOriginal, entry.LocalFilePath, entry.UniqueIDs, True), idMedia, mediaType, entry.Role, iOrder)
                iOrder += 1
            Next
        End If
    End Sub

    Private Sub SetCertificationsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal certifications As List(Of String))
        RemoveCertificationsFromItem(idMedia, contentType)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            certifications.Sort()
            For Each entry As String In certifications
                AddToLinkTable("certification_link", "idCertification", AddCertification(entry), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

    Private Sub SetCountriesForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal countries As List(Of String))
        RemoveCountriesFromItem(idMedia, contentType)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            countries.Sort()
            For Each entry As String In countries
                AddToLinkTable("country_link", "idCountry", AddCountry(entry), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

    Private Sub SetCreatorsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal creators As List(Of String))
        RemoveCreatorsFromItem(idMedia, contentType)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As String In creators
                AddToLinkTable("creator_link", "idPerson", AddPerson(entry, "", "", Nothing, False), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

    Private Sub SetDirectorsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal directors As List(Of String))
        RemoveDirectrorsFromItem(idMedia, contentType)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As String In directors
                AddToLinkTable("director_link", "idPerson", AddPerson(entry, "", "", Nothing, False), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

    Private Sub SetExternalSubtitlesForFileItem(ByVal fileItem As FileItem, ByVal subtitles As List(Of MediaContainers.Subtitle))
        If fileItem IsNot Nothing AndAlso fileItem.IDSpecified AndAlso subtitles.Count > 0 Then
            'Subtitle streams (external only)
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Concat("INSERT INTO streamdetail (",
                                                       "idFile,",
                                                       "streamType,",
                                                       "subtitleLanguage,",
                                                       "subtitleForced,",
                                                       "subtitlePath",
                                                       ") VALUES (?,?,?,?,?)")

                Dim par_idFile As SQLiteParameter = sqlCommand.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
                Dim par_streamType As SQLiteParameter = sqlCommand.Parameters.Add("par_streamType", DbType.Int32, 0, "streamType")
                Dim par_subtitleLanguage As SQLiteParameter = sqlCommand.Parameters.Add("par_subtitleLanguage", DbType.String, 0, "subtitleLanguage")
                Dim par_subtitleForced As SQLiteParameter = sqlCommand.Parameters.Add("par_subtitleForced", DbType.Boolean, 0, "subtitleForced")
                Dim par_subtitlePath As SQLiteParameter = sqlCommand.Parameters.Add("par_subtitlePath", DbType.String, 0, "subtitlePath")

                For i As Integer = 0 To subtitles.Count - 1
                    par_idFile.Value = fileItem.ID
                    par_streamType.Value = 2 'subtitle stream
                    par_subtitleForced.Value = subtitles(i).Forced
                    par_subtitleLanguage.Value = subtitles(i).Language
                    par_subtitlePath.Value = subtitles(i).Path
                    sqlCommand.ExecuteNonQuery()
                Next
            End Using
        End If
    End Sub

    Private Sub SetFileInfoForFileItem(ByVal fileItem As FileItem, ByVal fileInfo As MediaContainers.FileInfo)
        If fileItem IsNot Nothing AndAlso fileItem.IDSpecified Then
            RemoveFileInfoFromFileItem(fileItem.ID)
            If fileInfo.StreamDetailsSpecified Then
                'Video streams
                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()

                    sqlCommand.CommandText = String.Concat("INSERT INTO streamdetail (",
                                                           "idFile,",
                                                           "streamType,",
                                                           "videoCodec,",
                                                           "videoAspect,",
                                                           "videoBitrate,",
                                                           "videoLanguage,",
                                                           "videoWidth,",
                                                           "videoHeight,",
                                                           "videoScantype,",
                                                           "videoDuration,",
                                                           "videoMultiViewCount,",
                                                           "videoMultiViewLayout,",
                                                           "videoStereoMode,",
                                                           "videoBitDepth,",
                                                           "videoChromaSubsampling",
                                                           ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)")

                    Dim par_idFile As SQLiteParameter = sqlCommand.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
                    Dim par_streamType As SQLiteParameter = sqlCommand.Parameters.Add("par_streamType", DbType.Int32, 0, "streamType")
                    Dim par_videoCodec As SQLiteParameter = sqlCommand.Parameters.Add("par_videoCodec", DbType.String, 0, "videoCodec")
                    Dim par_videoAspect As SQLiteParameter = sqlCommand.Parameters.Add("par_videoAspect", DbType.Double, 0, "videoAspect")
                    Dim par_videoBitrate As SQLiteParameter = sqlCommand.Parameters.Add("par_videoBitrate", DbType.Int32, 0, "videoBitrate")
                    Dim par_videoLanguage As SQLiteParameter = sqlCommand.Parameters.Add("par_videoLanguage", DbType.String, 0, "videoLanguage")
                    Dim par_videoWidth As SQLiteParameter = sqlCommand.Parameters.Add("par_videoWidth", DbType.Int32, 0, "videoWidth")
                    Dim par_videoHeight As SQLiteParameter = sqlCommand.Parameters.Add("par_videoHeight", DbType.Int32, 0, "videoHeight")
                    Dim par_videoScantype As SQLiteParameter = sqlCommand.Parameters.Add("par_videoScantype", DbType.String, 0, "videoScantype")
                    Dim par_videoDuration As SQLiteParameter = sqlCommand.Parameters.Add("par_videoDuration", DbType.Int32, 0, "videoDuration")
                    Dim par_videoMultiViewCount As SQLiteParameter = sqlCommand.Parameters.Add("par_videoMultiViewCount", DbType.Int32, 0, "videoMultiViewCount")
                    Dim par_videoMultiViewLayout As SQLiteParameter = sqlCommand.Parameters.Add("par_videoMultiViewLayout", DbType.String, 0, "videoMultiViewLayout")
                    Dim par_videoStereoMode As SQLiteParameter = sqlCommand.Parameters.Add("par_videoStereoMode", DbType.String, 0, "videoStereoMode")
                    Dim par_videoBitDepth As SQLiteParameter = sqlCommand.Parameters.Add("par_videoBitDepth", DbType.Int32, 0, "videoBitDepth")
                    Dim par_videoChromaSubsampling As SQLiteParameter = sqlCommand.Parameters.Add("par_videoChromaSubsampling", DbType.String, 0, "videoChromaSubsampling")
                    Dim par_videoColourPrimaries As SQLiteParameter = sqlCommand.Parameters.Add("par_videoColourPrimaries", DbType.String, 0, "videoColourPrimaries")

                    For i As Integer = 0 To fileInfo.StreamDetails.Video.Count - 1
                        par_idFile.Value = fileItem.ID
                        par_streamType.Value = 0 'video stream
                        par_videoCodec.Value = fileInfo.StreamDetails.Video(i).Codec
                        par_videoLanguage.Value = fileInfo.StreamDetails.Video(i).Language
                        par_videoMultiViewLayout.Value = fileInfo.StreamDetails.Video(i).MultiViewLayout
                        par_videoScantype.Value = fileInfo.StreamDetails.Video(i).Scantype
                        par_videoStereoMode.Value = fileInfo.StreamDetails.Video(i).StereoMode
                        par_videoAspect.Value = fileInfo.StreamDetails.Video(i).Aspect
                        par_videoBitrate.Value = fileInfo.StreamDetails.Video(i).Bitrate
                        par_videoDuration.Value = fileInfo.StreamDetails.Video(i).Duration
                        par_videoHeight.Value = fileInfo.StreamDetails.Video(i).Height
                        par_videoMultiViewCount.Value = fileInfo.StreamDetails.Video(i).MultiViewCount
                        par_videoWidth.Value = fileInfo.StreamDetails.Video(i).Width
                        par_videoBitDepth.Value = fileInfo.StreamDetails.Video(i).BitDepth
                        par_videoChromaSubsampling.Value = fileInfo.StreamDetails.Video(i).ChromaSubsampling
                        par_videoColourPrimaries.Value = fileInfo.StreamDetails.Video(i).ColourPrimaries
                        sqlCommand.ExecuteNonQuery()
                    Next
                End Using

                'Audio streams
                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand.CommandText = String.Concat("INSERT INTO streamdetail (",
                                                           "idFile,",
                                                           "streamType,",
                                                           "audioCodec,",
                                                           "audioChannels,",
                                                           "audioBitrate,",
                                                           "audioLanguage",
                                                           ") VALUES (?,?,?,?,?,?)")

                    Dim par_idFile As SQLiteParameter = sqlCommand.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
                    Dim par_streamType As SQLiteParameter = sqlCommand.Parameters.Add("par_streamType", DbType.Int32, 0, "streamType")
                    Dim par_audioCodec As SQLiteParameter = sqlCommand.Parameters.Add("par_audioCodec", DbType.String, 0, "audioCodec")
                    Dim par_audioChannels As SQLiteParameter = sqlCommand.Parameters.Add("par_audioChannels", DbType.Int32, 0, "audioChannels")
                    Dim par_audioBitrate As SQLiteParameter = sqlCommand.Parameters.Add("par_audioBitrate", DbType.Int32, 0, "audioBitrate")
                    Dim par_audioLanguage As SQLiteParameter = sqlCommand.Parameters.Add("par_audioLanguage", DbType.String, 0, "audioLanguage")

                    For i As Integer = 0 To fileInfo.StreamDetails.Audio.Count - 1
                        par_idFile.Value = fileItem.ID
                        par_streamType.Value = 1 'audio stream
                        par_audioCodec.Value = fileInfo.StreamDetails.Audio(i).Codec
                        par_audioLanguage.Value = fileInfo.StreamDetails.Audio(i).Language
                        par_audioBitrate.Value = fileInfo.StreamDetails.Audio(i).Bitrate
                        par_audioChannels.Value = fileInfo.StreamDetails.Audio(i).Channels
                        sqlCommand.ExecuteNonQuery()
                    Next
                End Using

                'Subtitle streams (embedded only)
                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand.CommandText = String.Concat("INSERT INTO streamdetail (",
                                                           "idFile,",
                                                           "streamType,",
                                                           "subtitleLanguage,",
                                                           "subtitleForced,",
                                                           "subtitlePath",
                                                           ") VALUES (?,?,?,?,?)")

                    Dim par_idFile As SQLiteParameter = sqlCommand.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
                    Dim par_streamType As SQLiteParameter = sqlCommand.Parameters.Add("par_streamType", DbType.Int32, 0, "streamType")
                    Dim par_subtitleLanguage As SQLiteParameter = sqlCommand.Parameters.Add("par_subtitleLanguage", DbType.String, 0, "subtitleLanguage")
                    Dim par_subtitleForced As SQLiteParameter = sqlCommand.Parameters.Add("par_subtitleForced", DbType.Boolean, 0, "subtitleForced")
                    Dim par_subtitlePath As SQLiteParameter = sqlCommand.Parameters.Add("par_subtitlePath", DbType.String, 0, "subtitlePath")

                    For i As Integer = 0 To fileInfo.StreamDetails.Subtitle.Count - 1
                        par_idFile.Value = fileItem.ID
                        par_streamType.Value = 2 'subtitle stream
                        par_subtitleForced.Value = fileInfo.StreamDetails.Subtitle(i).Forced
                        par_subtitleLanguage.Value = fileInfo.StreamDetails.Subtitle(i).Language
                        par_subtitlePath.Value = fileInfo.StreamDetails.Subtitle(i).Path
                        sqlCommand.ExecuteNonQuery()
                    Next
                End Using
            End If
        End If
    End Sub

    Private Sub SetGenresForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal genres As List(Of String))
        RemoveGenresFromItem(idMedia, contentType)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            genres.Sort()
            For Each entry As String In genres
                AddToLinkTable("genre_link", "idGenre", AddGenre(entry), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

    Private Sub SetGuestStarsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal gueststars As List(Of MediaContainers.Person))
        RemoveGuestStarsFromItem(idMedia, contentType)
        Dim iOrder As Integer = 0
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As MediaContainers.Person In gueststars
                AddToPersonLinkTable("gueststar_link", AddPerson(entry.Name, entry.URLOriginal, entry.LocalFilePath, entry.UniqueIDs, True), idMedia, mediaType, entry.Role, iOrder)
                iOrder += 1
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
                        sqlCommand.CommandText = String.Concat("INSERT OR REPLACE INTO movieset_link (",
                                                               "idSet,",
                                                               "idMovie,",
                                                               "set_order",
                                                               ") VALUES (?,?,?);")
                        Dim par_idSet As SQLiteParameter = sqlCommand.Parameters.Add("par_idSet", DbType.Int64, 0, "idSet")
                        Dim par_idMovie As SQLiteParameter = sqlCommand.Parameters.Add("par_idMovie", DbType.Int64, 0, "idMovie")
                        Dim par_set_order As SQLiteParameter = sqlCommand.Parameters.Add("par_set_order", DbType.Int64, 0, "set_order")

                        par_idSet.Value = entry.ID
                        par_idMovie.Value = dbElement.ID
                        par_set_order.Value = entry.Order
                        sqlCommand.ExecuteNonQuery()
                    End Using
                Else
                    'first check if a movieset with the same TMDBColID is already existing
                    If entry.TMDbIdSpecified Then
                        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand.CommandText = String.Format("SELECT movieset.idSet, movieset.title, movieset.plot FROM uniqueid INNER JOIN movieset ON (uniqueid.idMedia = movieset.idSet) WHERE uniqueid.media_type = 'movieset' AND uniqueid.type = 'tmdb' AND uniqueid.value = '{0}'", entry.TMDbId)
                            Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                                If sqlReader.HasRows Then
                                    sqlReader.Read()
                                    If Not DBNull.Value.Equals(sqlReader("idSet")) Then entry.ID = CLng(sqlReader("idSet"))
                                    If Not DBNull.Value.Equals(sqlReader("title")) Then entry.Title = CStr(sqlReader("title"))
                                    If Not DBNull.Value.Equals(sqlReader("plot")) AndAlso
                                            Not String.IsNullOrEmpty(CStr(sqlReader("plot"))) Then entry.Plot = CStr(sqlReader("plot"))
                                    bIsNewSet = False
                                    Info.SaveToNFO_Movie(dbElement, False) 'to save the "new" SetName and/or SetPlot
                                Else
                                    bIsNewSet = True
                                End If
                            End Using
                        End Using
                    End If

                    If bIsNewSet Then
                        'secondly check if a movieset with the same name is already existing
                        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand.CommandText = String.Concat("SELECT idSet, plot ",
                                                                       "FROM movieset WHERE title LIKE """, entry.Title, """;")
                            Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                                If sqlReader.HasRows Then
                                    sqlReader.Read()
                                    If Not DBNull.Value.Equals(sqlReader("idSet")) Then entry.ID = CLng(sqlReader("idSet"))
                                    If Not DBNull.Value.Equals(sqlReader("plot")) AndAlso
                                                    Not String.IsNullOrEmpty(CStr(sqlReader("plot"))) Then entry.Plot = CStr(sqlReader("plot"))
                                    bIsNewSet = False
                                    Info.SaveToNFO_Movie(dbElement, False) 'to save the "new" SetName and/or SetPlot
                                Else
                                    bIsNewSet = True
                                End If
                            End Using
                        End Using
                    End If

                    If Not bIsNewSet Then
                        'create new movieset_link entry with existing idSet
                        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand.CommandText = String.Concat("INSERT OR REPLACE INTO movieset_link (",
                                                                   "idSet,",
                                                                   "idMovie,",
                                                                   "set_order",
                                                                   ") VALUES (?,?,?);")
                            Dim par_idSet As SQLiteParameter = sqlCommand.Parameters.Add("par_idSet", DbType.Int64, 0, "idSet")
                            Dim par_idMovie As SQLiteParameter = sqlCommand.Parameters.Add("par_idMovie", DbType.Int64, 0, "idMovie")
                            Dim par_set_order As SQLiteParameter = sqlCommand.Parameters.Add("par_set_order", DbType.Int64, 0, "set_order")

                            par_idSet.Value = entry.ID
                            par_idMovie.Value = dbElement.ID
                            par_set_order.Value = entry.Order
                            sqlCommand.ExecuteNonQuery()
                        End Using

                        'update existing movieset with latest TMDB Collection ID and Plot
                        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand.CommandText = String.Format("UPDATE movieset SET plot=? WHERE idSet={0}", entry.ID)
                            Dim par_plot As SQLiteParameter = sqlCommand.Parameters.Add("par_plot", DbType.String, 0, "plot")
                            par_plot.Value = entry.Plot
                            sqlCommand.ExecuteNonQuery()
                        End Using
                    Else
                        'create new movieset
                        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand.CommandText = String.Concat("INSERT Or REPLACE INTO movieset (",
                                                                   "nfoPath,",
                                                                   "plot,",
                                                                   "title,",
                                                                   "new,",
                                                                   "marked,",
                                                                   "locked,",
                                                                   "sortMethod,",
                                                                   "language",
                                                                   ") VALUES (?,?,?,?,?,?,?,?);")
                            Dim par_nfoPath As SQLiteParameter = sqlCommand.Parameters.Add("par_nfoPath", DbType.String, 0, "nfoPath")
                            Dim par_plot As SQLiteParameter = sqlCommand.Parameters.Add("par_plot", DbType.String, 0, "plot")
                            Dim par_title As SQLiteParameter = sqlCommand.Parameters.Add("par_title", DbType.String, 0, "title")
                            Dim par_new As SQLiteParameter = sqlCommand.Parameters.Add("par_new", DbType.Boolean, 0, "New")
                            Dim par_marked As SQLiteParameter = sqlCommand.Parameters.Add("par_marked", DbType.Boolean, 0, "marked")
                            Dim par_locked As SQLiteParameter = sqlCommand.Parameters.Add("par_locked", DbType.Boolean, 0, "locked")
                            Dim par_sortMethod As SQLiteParameter = sqlCommand.Parameters.Add("par_sortMethod", DbType.Int64, 0, "sortMethod")
                            Dim par_language As SQLiteParameter = sqlCommand.Parameters.Add("par_language", DbType.String, 0, "language")

                            par_title.Value = entry.Title
                            par_plot.Value = entry.Plot
                            par_nfoPath.Value = String.Empty
                            par_new.Value = True
                            par_locked.Value = False
                            par_sortMethod.Value = Enums.SortMethod_MovieSet.Year
                            par_language.Value = dbElement.Language

                            If Master.eSettings.MovieSetGeneralMarkNew Then
                                par_marked.Value = True
                            Else
                                par_marked.Value = False
                            End If
                            sqlCommand.ExecuteNonQuery()
                        End Using

                        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand.CommandText = String.Concat("SELECT idSet, title FROM movieset WHERE title Like """, entry.Title, """;")
                            Using rdrSets As SQLiteDataReader = sqlCommand.ExecuteReader()
                                If rdrSets.Read Then
                                    entry.ID = Convert.ToInt64(rdrSets(0))
                                End If
                            End Using
                        End Using

                        'create new movieset_link entry with new idSet
                        If entry.ID > 0 Then
                            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                sqlCommand.CommandText = String.Concat("INSERT Or REPLACE INTO movieset_link (",
                                                                       "idSet,",
                                                                       "idMovie,",
                                                                       "set_order",
                                                                       ") VALUES (?,?,?);")
                                Dim par_idSet As SQLiteParameter = sqlCommand.Parameters.Add("par_idSet", DbType.Int64, 0, "idSet")
                                Dim par_idMovie As SQLiteParameter = sqlCommand.Parameters.Add("par_idMovie", DbType.Int64, 0, "idMovie")
                                Dim par_set_order As SQLiteParameter = sqlCommand.Parameters.Add("par_set_order", DbType.Int64, 0, "set_order")

                                par_idSet.Value = entry.ID
                                par_idMovie.Value = dbElement.ID
                                par_set_order.Value = entry.Order
                                sqlCommand.ExecuteNonQuery()
                            End Using
                        End If
                    End If
                End If
            End If
        Next
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

    Private Sub SetStudiosForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal studios As List(Of String))
        RemoveStudiosFromItem(idMedia, contentType)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As String In studios
                AddToLinkTable("studio_link", "idStudio", AddStudio(entry), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

    Private Sub SetTagsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal tags As List(Of String))
        RemoveTagsFromItem(idMedia, contentType)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            tags.Sort()
            For Each entry As String In tags
                AddToLinkTable("tag_link", "idTag", AddTag(entry), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

    Private Sub SetTVShowLinksForMovie(ByVal dbElement As DBElement)
        RemoveFromTable(Helpers.GetTableName(TableName.tvshow_link), Helpers.GetMainIdName(TableName.movie), dbElement.ID)
        For Each strTitle In dbElement.Movie.ShowLinks
            Dim lngIDShow As Long = -1
            'search idShow by title
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT {0} FROM {1} WHERE {2}=?",
                                                       Helpers.GetMainIdName(TableName.tvshow),
                                                       Helpers.GetTableName(TableName.tvshow),
                                                       Helpers.GetColumnName(ColumnName.Title))
                Dim par_title As SQLiteParameter = sqlCommand.Parameters.Add("par_title", DbType.String, 0, Helpers.GetColumnName(ColumnName.Title))
                par_title.Value = strTitle
                Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    While sqlReader.Read
                        lngIDShow = CLng(sqlReader(Helpers.GetMainIdName(TableName.tvshow)))
                        Exit While
                    End While
                End Using
            End Using

            If Not lngIDShow = -1 Then
                AddToLinkTable(Helpers.GetTableName(TableName.tvshow_link),
                               Helpers.GetMainIdName(TableName.tvshow),
                               lngIDShow,
                               Helpers.GetMainIdName(TableName.movie),
                               dbElement.ID,
                               String.Empty,
                               String.Empty)
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

    Private Sub SetWritersForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal writers As List(Of String))
        RemoveWritersFromItem(idMedia, contentType)
        Dim mediaType As String = ConvertContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As String In writers
                AddToLinkTable("writer_link", "idPerson", AddPerson(entry, "", "", Nothing, False), "idMedia", idMedia, "media_type", mediaType)
            Next
        End If
    End Sub

#End Region 'Methods

#Region "Database upgrade Methods"

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
                    Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                        For Each _cmd As Containers.CommandsTransactionCommand In Trans.command
                            If _cmd.type = "DB" Then
                                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    sqlCommand.CommandText = _cmd.execute
                                    Try
                                        logger.Info(String.Concat(Trans.name, ":   ", _cmd.description))
                                        sqlCommand.ExecuteNonQuery()
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
                            sqlTransaction.Commit()
                            ' Housekeeping - consolidate and pack database using vacuum command http://www.sqlite.org/lang_vacuum.html
                            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                sqlCommand.CommandText = "VACUUM;"
                                sqlCommand.ExecuteNonQuery()
                            End Using
                        Else
                            logger.Trace(New StackFrame().GetMethod().Name, String.Format("Transaction {0} RollBack", Trans.name))
                            sqlTransaction.Rollback()
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
                                Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    sqlCommand.CommandText = "VACUUM;"
                                    sqlCommand.ExecuteNonQuery()
                                End Using
                            Catch ex As Exception
                                logger.Info(New StackFrame().GetMethod().Name, ex, SQLnotransaction, _cmd.description, _cmd.execute)
                            End Try
                        End Using
                    End If
                Next
            Next

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
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
                        Patch14_playCounts("episode", True)
                        Patch14_playCounts("movie", True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 18
                        Patch18_VotesCount("idEpisode", "episode", True)
                        Patch18_VotesCount("idMovie", "movie", True)
                        Patch18_VotesCount("idShow", "tvshow", True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 21
                        Patch21_SortTitle("tvshow", True)
                        Patch21_DisplayEpisodeSeason(True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 26
                        Patch26_EFanartsPath("idMovie", "movie", True)
                        Patch26_EThumbsPath("idMovie", "movie", True)
                        Patch26_EFanartsPath("idShow", "tvshow", True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 30
                        Patch30and31_Language("movie", True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 31
                        Patch30and31_Language("sets", True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 40
                        Patch40_IMDB(True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 41
                        Patch41_Top250(True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 42
                        Patch42_OrphanedLinks(True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 43
                        If MessageBox.Show("Locked state will now be saved in NFO. Do you want to rewrite all NFOs of locked items?", "Rewrite NFOs of locked items", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Patch43_LockedStateToNFO(True)
                        End If
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 44
                        Patch44_Sources("moviesource", True)
                        Patch44_Sources("tvshowsource", True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 45
                        Patch45_AllSeasonsEntries(True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 47
                        Patch47_certification_temp(True)
                        Patch47_file_temp(True)
                        Patch47_dateAdded_tvshow(True)
                        Patch47_streamdetails(True)
                        Patch47_wipe_seasontitles(True)
                        Patch47_year(True)
                        If MessageBox.Show("Ember now saves the resolution of all images in the database. Do you want to scan all images (all sources has to be mountet for this)?", "Get resolution of images", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Patch47_art(True)
                        End If
                        logger.Info("Set main tab sorting to defaults (needed for New default lists)")
                        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MainTabSorting, True)
                End Select

                sqlTransaction.Commit()
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

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT {0}, country FROM {1};", idField, table)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT {0}, director FROM {1};", idField, table)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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
                                    AddDirectorToTVEpisode(idMedia, AddPerson(value, "", "", Nothing, False))
                                Case "movie"
                                    AddDirectorToMovie(idMedia, AddPerson(value, "", "", Nothing, False))
                                Case "tvshow"
                                    AddDirectorToTvShow(idMedia, AddPerson(value, "", "", Nothing, False))
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

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT {0}, genre FROM {1};", idField, table)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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

    Private Sub Patch14_playCounts(ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing Playcounts...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("UPDATE {0} SET Playcount = NULL WHERE Playcount = 0 Or Playcount = """";", table)
            sqlCommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch14_studio(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Get studios...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT {0}, studio FROM {1};", idField, table)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT {0}, credits FROM {1};", idField, table)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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
                                    AddWriterToTVEpisode(idMedia, AddPerson(value, "", "", Nothing, False))
                                Case "movie"
                                    AddWriterToMovie(idMedia, AddPerson(value, "", "", Nothing, False))
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

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT {0}, Votes FROM {1};", idField, table)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("Votes")) AndAlso Not String.IsNullOrEmpty(SQLreader("Votes").ToString) AndAlso Not Integer.TryParse(SQLreader("Votes").ToString, 0) Then
                        Using sqlCommand_update_votes As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand_update_votes.CommandText = String.Format("UPDATE {0} SET Votes=? WHERE {1}={2}", table, idField, SQLreader(idField))
                            Dim par_update_Votes As SQLiteParameter = sqlCommand_update_votes.Parameters.Add("par_update_Votes", DbType.String, 0, "Vote")
                            par_update_Votes.Value = NumUtils.CleanVotes(SQLreader("Votes").ToString)
                            sqlCommand_update_votes.ExecuteNonQuery()
                        End Using
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch21_DisplayEpisodeSeason(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing DisplayEpisode And DisplaySeason...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "UPDATE episode SET DisplayEpisode = -1 WHERE DisplayEpisode Is NULL;"
            sqlCommand.ExecuteNonQuery()
            sqlCommand.CommandText = "UPDATE episode SET DisplaySeason = -1 WHERE DisplaySeason Is NULL;"
            sqlCommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch21_SortTitle(ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing SortTitles...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("UPDATE {0} SET SortTitle = '' WHERE SortTitle IS NULL OR SortTitle = """";", table)
            sqlCommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch26_EFanartsPath(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing Extrafanarts Paths...")
        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT * FROM {0} WHERE EFanartsPath NOT LIKE ''", table)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    Dim newExtrafanartsPath As String = String.Empty
                    If Not DBNull.Value.Equals(SQLreader("EFanartsPath")) Then newExtrafanartsPath = SQLreader("EFanartsPath").ToString
                    newExtrafanartsPath = Directory.GetParent(newExtrafanartsPath).FullName
                    Using sqlCommand_update_paths As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        sqlCommand_update_paths.CommandText = String.Format("UPDATE {0} SET EFanartsPath=? WHERE {1}={2}", table, idField, SQLreader(idField))
                        Dim par_ExtrafanartsPath As SQLiteParameter = sqlCommand_update_paths.Parameters.Add("par_EFanartsPath", DbType.String, 0, "EFanartsPath")
                        par_ExtrafanartsPath.Value = newExtrafanartsPath
                        sqlCommand_update_paths.ExecuteNonQuery()
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

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT * FROM {0} WHERE EThumbsPath NOT LIKE ''", table)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    Dim newExtrathumbsPath As String = String.Empty
                    If Not DBNull.Value.Equals(SQLreader("EThumbsPath")) Then newExtrathumbsPath = SQLreader("EThumbsPath").ToString
                    newExtrathumbsPath = Directory.GetParent(newExtrathumbsPath).FullName
                    Using sqlCommand_update_paths As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        sqlCommand_update_paths.CommandText = String.Format("UPDATE {0} SET EThumbsPath=? WHERE {1}={2}", table, idField, SQLreader(idField))
                        Dim par_ExtrathumbsPath As SQLiteParameter = sqlCommand_update_paths.Parameters.Add("par_EThumbsPath", DbType.String, 0, "EThumbsPath")
                        par_ExtrathumbsPath.Value = newExtrathumbsPath
                        sqlCommand_update_paths.ExecuteNonQuery()
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

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("UPDATE {0} SET Language = 'en-US';", table)
            sqlCommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch40_IMDB(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Cleanup all IMDB ID's ...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idMovie, Imdb FROM movie WHERE movie.Imdb <> '';"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("Imdb")) AndAlso Not String.IsNullOrEmpty(SQLreader("Imdb").ToString) AndAlso Not SQLreader("Imdb").ToString.StartsWith("tt") Then
                        Using sqlCommand_cleanup_imdb As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand_cleanup_imdb.CommandText = String.Format("UPDATE movie SET Imdb=? WHERE idMovie={0}", SQLreader("idMovie").ToString)
                            Dim par_Imdb As SQLiteParameter = sqlCommand_cleanup_imdb.Parameters.Add("par_Imdb", DbType.String, 0, "Imdb")
                            par_Imdb.Value = String.Concat("tt", SQLreader("Imdb").ToString)
                            sqlCommand_cleanup_imdb.ExecuteNonQuery()
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

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "UPDATE movie SET Top250 = NULL WHERE Top250 = 0 OR Top250 = """";"
            sqlCommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch42_OrphanedLinks(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Removing orphaned links...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            bwPatchDB.ReportProgress(-1, "Cleaning movie table")
            sqlCommand.CommandText = "DELETE FROM movie WHERE idSource NOT IN (SELECT idSource FROM moviesource);"
            sqlCommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning tvshow table")
            sqlCommand.CommandText = "DELETE FROM tvshow WHERE idSource NOT IN (SELECT idSource FROM tvshowsource);"
            sqlCommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning actorlinkmovie table")
            sqlCommand.CommandText = "DELETE FROM actorlinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            sqlCommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning art table")
            sqlCommand.CommandText = "DELETE FROM art WHERE media_id NOT IN (SELECT idMovie FROM movie) AND media_type = 'movie';"
            sqlCommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning countrylinkmovie table")
            sqlCommand.CommandText = "DELETE FROM countrylinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            sqlCommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning directorlinkmovie table")
            sqlCommand.CommandText = "DELETE FROM directorlinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            sqlCommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning genrelinkmovie table")
            sqlCommand.CommandText = "DELETE FROM genrelinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            sqlCommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning movielinktvshow table")
            sqlCommand.CommandText = "DELETE FROM movielinktvshow WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            sqlCommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning setlinkmovie table")
            sqlCommand.CommandText = "DELETE FROM setlinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            sqlCommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning studiolinkmovie table")
            sqlCommand.CommandText = "DELETE FROM studiolinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            sqlCommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning taglinks table")
            sqlCommand.CommandText = "DELETE FROM taglinks WHERE idMedia NOT IN (SELECT idMovie FROM movie) AND media_type = 'movie';"
            sqlCommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning writerlinkmovie table")
            sqlCommand.CommandText = "DELETE FROM writerlinkmovie WHERE idMovie NOT IN (SELECT idMovie FROM movie);"
            sqlCommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning MoviesAStreams table")
            sqlCommand.CommandText = "DELETE FROM MoviesAStreams WHERE MovieID NOT IN (SELECT idMovie FROM movie);"
            sqlCommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning MoviesSubs table")
            sqlCommand.CommandText = "DELETE FROM MoviesSubs WHERE MovieID NOT IN (SELECT idMovie FROM movie);"
            sqlCommand.ExecuteNonQuery()
            bwPatchDB.ReportProgress(-1, "Cleaning MoviesVStreams table")
            sqlCommand.CommandText = "DELETE FROM MoviesVStreams WHERE MovieID NOT IN (SELECT idMovie FROM movie);"
            sqlCommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch43_LockedStateToNFO(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Rewriting NFOs...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        'Movies
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idMovie FROM movie WHERE Lock = 1;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    Dim tmpDBElement As DBElement = Load_Movie(Convert.ToInt64(SQLreader("idMovie")))
                    Save_Movie(tmpDBElement, BatchMode, True, False, False, False)
                End While
            End Using
        End Using

        'TVEpisodes
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idEpisode FROM episode WHERE Lock = 1;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    Dim tmpDBElement As DBElement = Load_TVEpisode(Convert.ToInt64(SQLreader("idEpisode")), False)
                    Save_TVEpisode(tmpDBElement, BatchMode, True, False, False, False)
                End While
            End Using
        End Using

        'TVShows
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idShow FROM tvshow WHERE Lock = 1;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("SELECT idSource, strPath FROM {0};", table)
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    If Regex.Match(SQLreader("strPath").ToString, "([a-z]:|[A-Z]:)$").Success Then
                        Using sqlCommand_update_votes As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand_update_votes.CommandText = String.Format("UPDATE {0} SET strPath=""{1}\"" WHERE idSource={2}", table, SQLreader("strPath").ToString, SQLreader("idSource").ToString)
                            sqlCommand_update_votes.ExecuteNonQuery()
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

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "UPDATE seasons SET Season = -1 WHERE Season = 999;"
            sqlCommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch47_art(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Get size of all images...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idArt, url FROM art;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("url")) AndAlso Not String.IsNullOrEmpty(SQLreader("url").ToString) Then
                        Dim idArt As Long = Convert.ToInt64(SQLreader("idArt"))
                        Dim strValue As String = SQLreader("url").ToString
                        If File.Exists(strValue) Then
                            Try
                                Dim nImage As New Drawing.Bitmap(strValue)
                                Dim iWidth = nImage.Width
                                Dim iHeight = nImage.Height
                                nImage.Dispose()
                                Using sqlCommand_update As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                    sqlCommand_update.CommandText = String.Format("UPDATE art SET width={0}, height={1} WHERE idArt={2};", iWidth, iHeight, idArt)
                                    sqlCommand_update.ExecuteNonQuery()
                                End Using
                            Catch ex As Exception
                                logger.Error(String.Format("Can't read image (maybe broken): ""{0}""", strValue))
                            End Try
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

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT * FROM certification_temp;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
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
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Concat("DROP TABLE certification_temp;")
            sqlCommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Patch47_dateAdded_tvshow(ByVal batchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Create dateAdded for tv shows based on oldest episode...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT DISTINCT MIN(episode.dateAdded) AS dateAdded, tvshow.idShow FROM tvshow INNER JOIN episode ON (tvshow.idShow = episode.idShow) GROUP BY tvshow.idShow"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    Using sqlCommand_update As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        Dim iDateAdded As Integer = CInt(SQLreader("dateAdded"))
                        Dim lngID As Long = Convert.ToInt64(SQLreader("idShow"))
                        sqlCommand_update.CommandText = String.Format("UPDATE tvshow SET dateAdded={0} WHERE idShow={1};", iDateAdded, lngID)
                        sqlCommand_update.ExecuteNonQuery()
                    End Using
                End While
            End Using
        End Using

        If Not batchMode Then sqlTransaction.Commit()
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
                        Dim fileItem As FileItem = New FileItem(SQLreader("path").ToString)
                        Dim idMedia As Long = Convert.ToInt64(SQLreader("idMovie"))
                        Dim idFile = AddFileItem(fileItem)
                        Using sqlCommand_update As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand_update.CommandText = String.Format("UPDATE movie SET idFile={0} WHERE idMovie={1};", idFile, idMedia)
                            sqlCommand_update.ExecuteNonQuery()
                        End Using
                    End If
                End While
            End Using
        End Using

        'delete temporary table "file_temp"
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Concat("DROP TABLE file_temp;")
            sqlCommand.ExecuteNonQuery()
        End Using

        If Not batchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub Patch47_streamdetails(ByVal batchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Merge all stream tables to streamdetails...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        'Get data from table MoviesVStreams
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT MoviesVStreams.*, movie.idFile FROM MoviesVStreams LEFT OUTER JOIN movie ON (MoviesVStreams.MovieID = movie.idMovie);"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("idFile")) AndAlso
                            Not String.IsNullOrEmpty(SQLreader("idFile").ToString) AndAlso
                            Not CLng(SQLreader("idFile")) = -1 Then
                        Using sqlCommand_insert As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand_insert.CommandText = String.Concat("INSERT INTO streamdetail (",
                                                                          "idFile,",
                                                                          "streamType,",
                                                                          "videoCodec,",
                                                                          "videoAspect,",
                                                                          "videoBitrate,",
                                                                          "videoLanguage,",
                                                                          "videoWidth,",
                                                                          "videoHeight,",
                                                                          "videoScantype,",
                                                                          "videoDuration,",
                                                                          "videoMultiViewCount,",
                                                                          "videoMultiViewLayout,",
                                                                          "videoStereoMode",
                                                                          ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?)")
                            Dim par_idFile As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
                            Dim par_streamType As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_streamType", DbType.Int32, 0, "streamType")
                            Dim par_videoCodec As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoCodec", DbType.String, 0, "videoCodec")
                            Dim par_videoAspect As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoAspect", DbType.Double, 0, "videoAspect")
                            Dim par_videoBitrate As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoBitrate", DbType.Int32, 0, "videoBitrate")
                            Dim par_videoLanguage As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoLanguage", DbType.String, 0, "videoLanguage")
                            Dim par_videoWidth As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoWidth", DbType.Int32, 0, "videoWidth")
                            Dim par_videoHeight As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoHeight", DbType.Int32, 0, "videoHeight")
                            Dim par_videoScantype As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoScantype", DbType.String, 0, "videoScantype")
                            Dim par_videoDuration As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoDuration", DbType.Int32, 0, "videoDuration")
                            Dim par_videoMultiViewCount As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoMultiViewCount", DbType.Int32, 0, "videoMultiViewCount")
                            Dim par_videoMultiViewLayout As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoMultiViewLayout", DbType.String, 0, "videoMultiViewLayout")
                            Dim par_videoStereoMode As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoStereoMode", DbType.String, 0, "videoStereoMode")

                            'Known values
                            par_idFile.Value = CLng(SQLreader("idFile"))
                            par_streamType.Value = 0 'video stream

                            'String values
                            par_videoCodec.Value = SQLreader("Video_Codec").ToString
                            par_videoLanguage.Value = SQLreader("Video_Language").ToString
                            par_videoMultiViewLayout.Value = SQLreader("Video_MultiViewLayout").ToString
                            par_videoScantype.Value = SQLreader("Video_ScanType").ToString
                            par_videoStereoMode.Value = SQLreader("Video_StereoMode").ToString

                            'Double/Float or NULL values
                            If Double.TryParse(SQLreader("Video_AspectDisplayRatio").ToString, 0) Then
                                par_videoAspect.Value = CDbl(SQLreader("Video_AspectDisplayRatio"))
                            End If

                            'Integer or NULL values
                            If Integer.TryParse(SQLreader("Video_Bitrate").ToString, 0) Then
                                par_videoBitrate.Value = CInt(SQLreader("Video_Bitrate"))
                            End If
                            If Integer.TryParse(SQLreader("Video_Duration").ToString, 0) Then
                                par_videoDuration.Value = CInt(SQLreader("Video_Duration"))
                            End If
                            If Integer.TryParse(SQLreader("Video_Height").ToString, 0) Then
                                par_videoHeight.Value = CInt(SQLreader("Video_Height"))
                            End If
                            If Integer.TryParse(SQLreader("Video_MultiViewCount").ToString, 0) Then
                                par_videoMultiViewCount.Value = CInt(SQLreader("Video_MultiViewCount"))
                            End If
                            If Integer.TryParse(SQLreader("Video_Width").ToString, 0) Then
                                par_videoWidth.Value = CInt(SQLreader("Video_Width"))
                            End If
                            sqlCommand_insert.ExecuteNonQuery()
                        End Using
                    End If
                End While
            End Using
        End Using

        'Get data from table MoviesAStreams
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT MoviesAStreams.*, movie.idFile FROM MoviesAStreams LEFT OUTER JOIN movie ON (MoviesAStreams.MovieID = movie.idMovie);"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("idFile")) AndAlso
                            Not String.IsNullOrEmpty(SQLreader("idFile").ToString) AndAlso
                            Not CLng(SQLreader("idFile")) = -1 Then
                        Using sqlCommand_insert As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand_insert.CommandText = String.Concat("INSERT INTO streamdetail (",
                                                                          "idFile,",
                                                                          "streamType,",
                                                                          "audioCodec,",
                                                                          "audioChannels,",
                                                                          "audioBitrate,",
                                                                          "audioLanguage",
                                                                          ") VALUES (?,?,?,?,?,?)")
                            Dim par_idFile As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
                            Dim par_streamType As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_streamType", DbType.Int32, 0, "streamType")
                            Dim par_audioCodec As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_audioCodec", DbType.String, 0, "audioCodec")
                            Dim par_audioChannels As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_audioChannels", DbType.Int32, 0, "audioChannels")
                            Dim par_audioBitrate As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_audioBitrate", DbType.Int32, 0, "audioBitrate")
                            Dim par_audioLanguage As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_audioLanguage", DbType.String, 0, "audioLanguage")

                            'Known values
                            par_idFile.Value = CLng(SQLreader("idFile"))
                            par_streamType.Value = 1 'audio stream

                            'String values
                            par_audioCodec.Value = SQLreader("Audio_Codec").ToString
                            par_audioLanguage.Value = SQLreader("Audio_Codec").ToString

                            'Integer or NULL values
                            If Integer.TryParse(SQLreader("Audio_Bitrate").ToString, 0) Then
                                par_audioBitrate.Value = CInt(SQLreader("Audio_Bitrate"))
                            End If
                            If Integer.TryParse(SQLreader("Audio_Channel").ToString, 0) Then
                                par_audioChannels.Value = CInt(SQLreader("Audio_Channel"))
                            End If
                            sqlCommand_insert.ExecuteNonQuery()
                        End Using
                    End If
                End While
            End Using
        End Using

        'Get data from table MoviesSubs
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT MoviesSubs.*, movie.idFile FROM MoviesSubs LEFT OUTER JOIN movie ON (MoviesSubs.MovieID = movie.idMovie);"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("idFile")) AndAlso
                            Not String.IsNullOrEmpty(SQLreader("idFile").ToString) AndAlso
                            Not CLng(SQLreader("idFile")) = -1 Then
                        Using sqlCommand_insert As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand_insert.CommandText = String.Concat("INSERT INTO streamdetail (",
                                                                          "idFile,",
                                                                          "streamType,",
                                                                          "subtitleLanguage,",
                                                                          "subtitleForced,",
                                                                          "subtitlePath",
                                                                          ") VALUES (?,?,?,?,?)")
                            Dim par_idFile As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
                            Dim par_streamType As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_streamType", DbType.Int32, 0, "streamType")
                            Dim par_subtitleLanguage As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_subtitleLanguage", DbType.String, 0, "subtitleLanguage")
                            Dim par_subtitleForced As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_subtitleForced", DbType.Boolean, 0, "subtitleForced")
                            Dim par_subtitlePath As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_subtitlePath", DbType.String, 0, "subtitlePath")

                            'Known values
                            par_idFile.Value = CLng(SQLreader("idFile"))
                            par_streamType.Value = 2 'subtitle stream

                            'Boolean values
                            par_subtitleForced.Value = CBool(SQLreader("Subs_Forced"))

                            'String values
                            par_subtitleLanguage.Value = SQLreader("Subs_Language").ToString
                            par_subtitlePath.Value = SQLreader("Subs_Path").ToString
                            sqlCommand_insert.ExecuteNonQuery()
                        End Using
                    End If
                End While
            End Using
        End Using

        'Get data from table TVVStreams
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT TVVStreams.*, episode.idFile FROM TVVStreams LEFT OUTER JOIN episode ON (TVVStreams.TVEpID = episode.idEpisode);"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("idFile")) AndAlso
                            Not String.IsNullOrEmpty(SQLreader("idFile").ToString) AndAlso
                            Not CLng(SQLreader("idFile")) = -1 Then
                        Using sqlCommand_insert As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand_insert.CommandText = String.Concat("INSERT INTO streamdetail (",
                                                                          "idFile,",
                                                                          "streamType,",
                                                                          "videoCodec,",
                                                                          "videoAspect,",
                                                                          "videoBitrate,",
                                                                          "videoLanguage,",
                                                                          "videoWidth,",
                                                                          "videoHeight,",
                                                                          "videoScantype,",
                                                                          "videoDuration,",
                                                                          "videoMultiViewCount,",
                                                                          "videoMultiViewLayout,",
                                                                          "videoStereoMode",
                                                                          ") VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?)")
                            Dim par_idFile As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
                            Dim par_streamType As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_streamType", DbType.Int32, 0, "streamType")
                            Dim par_videoCodec As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoCodec", DbType.String, 0, "videoCodec")
                            Dim par_videoAspect As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoAspect", DbType.Double, 0, "videoAspect")
                            Dim par_videoBitrate As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoBitrate", DbType.Int32, 0, "videoBitrate")
                            Dim par_videoLanguage As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoLanguage", DbType.String, 0, "videoLanguage")
                            Dim par_videoWidth As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoWidth", DbType.Int32, 0, "videoWidth")
                            Dim par_videoHeight As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoHeight", DbType.Int32, 0, "videoHeight")
                            Dim par_videoScantype As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoScantype", DbType.String, 0, "videoScantype")
                            Dim par_videoDuration As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoDuration", DbType.Int32, 0, "videoDuration")
                            Dim par_videoMultiViewCount As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoMultiViewCount", DbType.Int32, 0, "videoMultiViewCount")
                            Dim par_videoMultiViewLayout As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoMultiViewLayout", DbType.String, 0, "videoMultiViewLayout")
                            Dim par_videoStereoMode As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_videoStereoMode", DbType.String, 0, "videoStereoMode")

                            'Known values
                            par_idFile.Value = CLng(SQLreader("idFile"))
                            par_streamType.Value = 0 'video stream

                            'String values
                            par_videoCodec.Value = SQLreader("Video_Codec").ToString
                            par_videoLanguage.Value = SQLreader("Video_Language").ToString
                            par_videoMultiViewLayout.Value = SQLreader("Video_MultiViewLayout").ToString
                            par_videoScantype.Value = SQLreader("Video_ScanType").ToString
                            par_videoStereoMode.Value = SQLreader("Video_StereoMode").ToString

                            'Double/Float or NULL values
                            If Double.TryParse(SQLreader("Video_AspectDisplayRatio").ToString, 0) Then
                                par_videoAspect.Value = CDbl(SQLreader("Video_AspectDisplayRatio"))
                            End If

                            'Integer or NULL values
                            If Integer.TryParse(SQLreader("Video_Bitrate").ToString, 0) Then
                                par_videoBitrate.Value = CInt(SQLreader("Video_Bitrate"))
                            End If
                            If Integer.TryParse(SQLreader("Video_Duration").ToString, 0) Then
                                par_videoDuration.Value = CInt(SQLreader("Video_Duration"))
                            End If
                            If Integer.TryParse(SQLreader("Video_Height").ToString, 0) Then
                                par_videoHeight.Value = CInt(SQLreader("Video_Height"))
                            End If
                            If Integer.TryParse(SQLreader("Video_MultiViewCount").ToString, 0) Then
                                par_videoMultiViewCount.Value = CInt(SQLreader("Video_MultiViewCount"))
                            End If
                            If Integer.TryParse(SQLreader("Video_Width").ToString, 0) Then
                                par_videoWidth.Value = CInt(SQLreader("Video_Width"))
                            End If
                            sqlCommand_insert.ExecuteNonQuery()
                        End Using
                    End If
                End While
            End Using
        End Using

        'Get data from table TVAStreams
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT TVAStreams.*, episode.idFile FROM TVAStreams LEFT OUTER JOIN episode ON (TVAStreams.TVEpID = episode.idEpisode);"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("idFile")) AndAlso
                            Not String.IsNullOrEmpty(SQLreader("idFile").ToString) AndAlso
                            Not CLng(SQLreader("idFile")) = -1 Then
                        Using sqlCommand_insert As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand_insert.CommandText = String.Concat("INSERT INTO streamdetail (",
                                                                          "idFile,",
                                                                          "streamType,",
                                                                          "audioCodec,",
                                                                          "audioChannels,",
                                                                          "audioBitrate,",
                                                                          "audioLanguage",
                                                                          ") VALUES (?,?,?,?,?,?)")
                            Dim par_idFile As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
                            Dim par_streamType As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_streamType", DbType.Int32, 0, "streamType")
                            Dim par_audioCodec As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_audioCodec", DbType.String, 0, "audioCodec")
                            Dim par_audioChannels As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_audioChannels", DbType.Int32, 0, "audioChannels")
                            Dim par_audioBitrate As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_audioBitrate", DbType.Int32, 0, "audioBitrate")
                            Dim par_audioLanguage As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_audioLanguage", DbType.String, 0, "audioLanguage")

                            'Known values
                            par_idFile.Value = CLng(SQLreader("idFile"))
                            par_streamType.Value = 1 'audio stream

                            'String values
                            par_audioCodec.Value = SQLreader("Audio_Codec").ToString
                            par_audioLanguage.Value = SQLreader("Audio_Codec").ToString

                            'Integer or NULL values
                            If Integer.TryParse(SQLreader("Audio_Bitrate").ToString, 0) Then
                                par_audioBitrate.Value = CInt(SQLreader("Audio_Bitrate"))
                            End If
                            If Integer.TryParse(SQLreader("Audio_Channel").ToString, 0) Then
                                par_audioChannels.Value = CInt(SQLreader("Audio_Channel"))
                            End If
                            sqlCommand_insert.ExecuteNonQuery()
                        End Using
                    End If
                End While
            End Using
        End Using

        'Get data from table TVSubs
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT TVSubs.*, episode.idFile FROM TVSubs LEFT OUTER JOIN episode ON (TVSubs.TVEpID = episode.idEpisode);"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("idFile")) AndAlso
                            Not String.IsNullOrEmpty(SQLreader("idFile").ToString) AndAlso
                            Not CLng(SQLreader("idFile")) = -1 Then
                        Using sqlCommand_insert As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand_insert.CommandText = String.Concat("INSERT INTO streamdetail (",
                                                                          "idFile,",
                                                                          "streamType,",
                                                                          "subtitleLanguage,",
                                                                          "subtitleForced,",
                                                                          "subtitlePath",
                                                                          ") VALUES (?,?,?,?,?)")
                            Dim par_idFile As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
                            Dim par_streamType As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_streamType", DbType.Int32, 0, "streamType")
                            Dim par_subtitleLanguage As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_subtitleLanguage", DbType.String, 0, "subtitleLanguage")
                            Dim par_subtitleForced As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_subtitleForced", DbType.Boolean, 0, "subtitleForced")
                            Dim par_subtitlePath As SQLiteParameter = sqlCommand_insert.Parameters.Add("par_subtitlePath", DbType.String, 0, "subtitlePath")

                            'Known values
                            par_idFile.Value = CLng(SQLreader("idFile"))
                            par_streamType.Value = 3 'subtitle stream

                            'Boolean values
                            par_subtitleForced.Value = CBool(SQLreader("Subs_Forced"))

                            'String values
                            par_subtitleLanguage.Value = SQLreader("Subs_Language").ToString
                            par_subtitlePath.Value = SQLreader("Subs_Path").ToString
                            sqlCommand_insert.ExecuteNonQuery()
                        End Using
                    End If
                End While
            End Using
        End Using

        'delete old tables
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Concat("DROP TABLE MoviesAStreams;")
            sqlCommand.ExecuteNonQuery()
            sqlCommand.CommandText = String.Concat("DROP TABLE MoviesSubs;")
            sqlCommand.ExecuteNonQuery()
            sqlCommand.CommandText = String.Concat("DROP TABLE MoviesVStreams;")
            sqlCommand.ExecuteNonQuery()
            sqlCommand.CommandText = String.Concat("DROP TABLE TVAStreams;")
            sqlCommand.ExecuteNonQuery()
            sqlCommand.CommandText = String.Concat("DROP TABLE TVSubs;")
            sqlCommand.ExecuteNonQuery()
            sqlCommand.CommandText = String.Concat("DROP TABLE TVVStreams;")
            sqlCommand.ExecuteNonQuery()
        End Using

        If Not batchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub Patch47_wipe_seasontitles(ByVal batchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Wipe all generic season titles...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idSeason, season, title FROM season;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    Dim lngID = Convert.ToInt64(SQLreader("idSeason"))
                    Dim intSeason = Convert.ToInt32(SQLreader("season"))
                    If intSeason = -1 OrElse String.IsNullOrEmpty(StringUtils.FilterSeasonTitle(SQLreader("title").ToString)) Then
                        Using sqlCommand_update As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand_update.CommandText = String.Format("UPDATE season SET title='' WHERE idSeason={0};", lngID)
                            sqlCommand_update.ExecuteNonQuery()
                        End Using
                    End If
                End While
            End Using
        End Using

        If Not batchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub Patch47_year(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing Year...")

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "UPDATE movie SET year = NULL WHERE year = 0 OR year = '';"
            sqlCommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then SQLtransaction.Commit()
    End Sub

#End Region 'Database upgrade Methods

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

    <Serializable()>
    Public Class DBElement
        Implements ICloneable

#Region "Fields"

        Private _islocked As Boolean

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal type As Enums.ContentType)
            ContentType = type
            Select Case ContentType
                Case Enums.ContentType.Movie
                    Movie = New MediaContainers.Movie
                Case Enums.ContentType.Movieset
                    MovieSet = New MediaContainers.MovieSet
                Case Enums.ContentType.TVEpisode
                    TVEpisode = New MediaContainers.EpisodeDetails
                Case Enums.ContentType.TVSeason
                    TVSeason = New MediaContainers.SeasonDetails
                Case Enums.ContentType.TVShow
                    TVShow = New MediaContainers.TVShow
            End Select
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property ActorThumbs() As List(Of String) = New List(Of String)

        Public ReadOnly Property ActorThumbsSpecified() As Boolean
            Get
                Return ActorThumbs.Count > 0
            End Get
        End Property

        Public ReadOnly Property ContentType() As Enums.ContentType

        Public Property DateAdded() As Long = -1

        Public Property DateModified() As Long = -1

        Public Property Episodes() As List(Of DBElement) = New List(Of DBElement)

        Public ReadOnly Property EpisodesSpecified() As Boolean
            Get
                Return Episodes.Count > 0
            End Get
        End Property

        Public Property EpisodeOrdering() As Enums.EpisodeOrdering = Enums.EpisodeOrdering.Standard

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

        Public Property FileItem() As FileItem = Nothing

        Public ReadOnly Property FileItemSpecified() As Boolean
            Get
                Return FileItem IsNot Nothing AndAlso FileItem.FullPathSpecified
            End Get
        End Property

        Public Property ID() As Long = -1

        Public ReadOnly Property IDSpecified() As Boolean
            Get
                Return Not ID = -1
            End Get
        End Property

        Public Property ImagesContainer() As MediaContainers.ImagesContainer = New MediaContainers.ImagesContainer

        Public Property IsLocked() As Boolean
            Get
                Return _islocked
            End Get
            Set(ByVal value As Boolean)
                _islocked = value
                Select Case ContentType
                    Case Enums.ContentType.Movie
                        If MovieSpecified Then Movie.Locked = value
                    Case Enums.ContentType.Movieset
                        If MovieSetSpecified Then MovieSet.Locked = value
                    Case Enums.ContentType.TVEpisode
                        If TVEpisodeSpecified Then TVEpisode.Locked = value
                    Case Enums.ContentType.TVSeason
                        If TVSeasonSpecified Then TVSeason.Locked = value
                    Case Enums.ContentType.TVShow
                        If TVShowSpecified Then TVShow.Locked = value
                End Select
            End Set
        End Property

        Public Property IsMarked() As Boolean = False

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

        Public Property MoviesInSet() As List(Of MediaContainers.MovieInSet) = New List(Of MediaContainers.MovieInSet)

        Public ReadOnly Property MoviesInSetSpecified() As Boolean
            Get
                Return MoviesInSet.Count > 0
            End Get
        End Property

        Public Property MovieSet() As MediaContainers.MovieSet = Nothing

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

        Public Property OutOfTolerance() As Boolean = False

        Public Property Seasons() As List(Of DBElement) = New List(Of DBElement)

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

        Public Property Source() As DBSource = New DBSource

        Public ReadOnly Property SourceSpecified() As Boolean
            Get
                Return Not Source.ID = -1
            End Get
        End Property

        Public Property Subtitles() As List(Of MediaContainers.Subtitle) = New List(Of MediaContainers.Subtitle)

        Public ReadOnly Property SubtitlesSpecified() As Boolean
            Get
                Return Subtitles.Count > 0
            End Get
        End Property

        Public Property Theme() As MediaContainers.Theme = New MediaContainers.Theme

        Public ReadOnly Property ThemeSpecified() As Boolean
            Get
                Return Theme.ThemeOriginal IsNot Nothing AndAlso Theme.ThemeOriginal.hasMemoryStream
            End Get
        End Property

        Public Property Trailer() As MediaContainers.Trailer = New MediaContainers.Trailer

        Public ReadOnly Property TrailerSpecified() As Boolean
            Get
                Return Trailer.TrailerOriginal IsNot Nothing AndAlso Trailer.TrailerOriginal.hasMemoryStream
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

        Public Property EpisodeOrdering() As Enums.EpisodeOrdering = Enums.EpisodeOrdering.Standard

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

        Public Property Name() As String = String.Empty

        Public ReadOnly Property NameSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Name)
            End Get
        End Property

        Public Property Path() As String = String.Empty

        Public ReadOnly Property PathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Path)
            End Get
        End Property

        Public Property ScanRecursive() As Boolean = False

        Public Property UseFolderName() As Boolean = False

#End Region 'Properties  

    End Class

    Public Class Helpers

#Region "Methods"

        Public Shared Function ColumnHeaderIsIcon(ByVal columnName As String) As Boolean
            If Not String.IsNullOrEmpty(columnName) Then
                Dim lstColumns As String() = New String() {
                    GetColumnName(Database.ColumnName.BannerPath),
                    GetColumnName(Database.ColumnName.CharacterArtPath),
                    GetColumnName(Database.ColumnName.ClearArtPath),
                    GetColumnName(Database.ColumnName.ClearLogoPath),
                    GetColumnName(Database.ColumnName.DiscArtPath),
                    GetColumnName(Database.ColumnName.ExtrafanartsPath),
                    GetColumnName(Database.ColumnName.ExtrathumbsPath),
                    GetColumnName(Database.ColumnName.FanartPath),
                    GetColumnName(Database.ColumnName.HasMetaData),
                    GetColumnName(Database.ColumnName.HasMovieset),
                    GetColumnName(Database.ColumnName.HasSubtitles),
                    GetColumnName(Database.ColumnName.HasWatched),
                    GetColumnName(Database.ColumnName.KeyArtPath),
                    GetColumnName(Database.ColumnName.LandscapePath),
                    GetColumnName(Database.ColumnName.LastPlayed),
                    GetColumnName(Database.ColumnName.NfoPath),
                    GetColumnName(Database.ColumnName.PlayCount),
                    GetColumnName(Database.ColumnName.PosterPath),
                    GetColumnName(Database.ColumnName.Ratings),
                    GetColumnName(Database.ColumnName.ThemePath),
                    GetColumnName(Database.ColumnName.TrailerPath),
                    GetColumnName(Database.ColumnName.UserRating)
                    }
                Return lstColumns.Contains(columnName)
            End If
            Return False
        End Function

        Public Shared Function ColumnIsBoolean(ByVal columnName As String) As Boolean
            If Not String.IsNullOrEmpty(columnName) Then
                Dim lstColumns As String() = New String() {
                    GetColumnName(Database.ColumnName.Exclude),
                    GetColumnName(Database.ColumnName.HasMetaData),
                    GetColumnName(Database.ColumnName.HasMovieset),
                    GetColumnName(Database.ColumnName.HasSubtitles),
                    GetColumnName(Database.ColumnName.HasWatched),
                    GetColumnName(Database.ColumnName.IsMissing)
                }
                Return lstColumns.Contains(columnName)
            End If
            Return False
        End Function

        Public Shared Function ColumnIsCheckbox(ByVal columnName As String) As Boolean
            If Not String.IsNullOrEmpty(columnName) Then
                Dim lstColumns As String() = New String() {
                    GetColumnName(Database.ColumnName.BannerPath),
                    GetColumnName(Database.ColumnName.CharacterArtPath),
                    GetColumnName(Database.ColumnName.ClearArtPath),
                    GetColumnName(Database.ColumnName.ClearLogoPath),
                    GetColumnName(Database.ColumnName.DiscArtPath),
                    GetColumnName(Database.ColumnName.ExtrafanartsPath),
                    GetColumnName(Database.ColumnName.ExtrathumbsPath),
                    GetColumnName(Database.ColumnName.FanartPath),
                    GetColumnName(Database.ColumnName.HasMetaData),
                    GetColumnName(Database.ColumnName.HasMovieset),
                    GetColumnName(Database.ColumnName.HasSubtitles),
                    GetColumnName(Database.ColumnName.HasWatched),
                    GetColumnName(Database.ColumnName.KeyArtPath),
                    GetColumnName(Database.ColumnName.LandscapePath),
                    GetColumnName(Database.ColumnName.LastPlayed),
                    GetColumnName(Database.ColumnName.NfoPath),
                    GetColumnName(Database.ColumnName.PlayCount),
                    GetColumnName(Database.ColumnName.PosterPath),
                    GetColumnName(Database.ColumnName.ThemePath),
                    GetColumnName(Database.ColumnName.TrailerPath)
                }
                Return lstColumns.Contains(columnName)
            End If
            Return False
        End Function

        Public Shared Function ColumnIsInMainView(ByVal columnName As ColumnName) As Boolean
            Select Case columnName
                Case ColumnName.ActorName,
                     ColumnName.AudioBitrate,
                     ColumnName.AudioChannels,
                     ColumnName.AudioCodec,
                     ColumnName.AudioLanguage,
                     ColumnName.FileSize,
                     ColumnName.Role,
                     ColumnName.SubtitleForced,
                     ColumnName.SubtitleLanguage,
                     ColumnName.SubtitlesPath,
                     ColumnName.VideoAspect,
                     ColumnName.VideoBitDepth,
                     ColumnName.VideoBitrate,
                     ColumnName.VideoChromaSupersampling,
                     ColumnName.VideoCodec,
                     ColumnName.VideoColorPrimaries,
                     ColumnName.VideoHeight,
                     ColumnName.VideoLanguage,
                     ColumnName.VideoMultiViewCount,
                     ColumnName.VideoMultiViewLayout,
                     ColumnName.VideoScantype,
                     ColumnName.VideoStereoMode,
                     ColumnName.VideoWidth
                    Return False
                Case Else
                    Return True
            End Select
        End Function

        Public Shared Function ColumnIsInteger(ByVal columnName As String) As Boolean
            If Not String.IsNullOrEmpty(columnName) Then
                Dim lstColumns As String() = New String() {
                    GetColumnName(Database.ColumnName.LockedEpisodesCount),
                    GetColumnName(Database.ColumnName.MarkedEpisodesCount),
                    GetColumnName(Database.ColumnName.MovieCount),
                    GetColumnName(Database.ColumnName.NewEpisodesCount)
                }
                Return lstColumns.Contains(columnName)
            End If
            Return False
        End Function

        Public Shared Function ColumnIsText(ByVal columnName As String) As Boolean
            If Not String.IsNullOrEmpty(columnName) Then
                Dim lstColumns As String() = New String() {
                    GetColumnName(Database.ColumnName.Aired),
                    GetColumnName(Database.ColumnName.Certifications),
                    GetColumnName(Database.ColumnName.Countries),
                    GetColumnName(Database.ColumnName.Certifications),
                    GetColumnName(Database.ColumnName.Creators),
                    GetColumnName(Database.ColumnName.Credits),
                    GetColumnName(Database.ColumnName.Directors),
                    GetColumnName(Database.ColumnName.DisplayEpisode),
                    GetColumnName(Database.ColumnName.DisplaySeason),
                    GetColumnName(Database.ColumnName.EpisodeCount),
                    GetColumnName(Database.ColumnName.EpisodeGuideURL),
                    GetColumnName(Database.ColumnName.EpisodeNumber),
                    GetColumnName(Database.ColumnName.Genres),
                    GetColumnName(Database.ColumnName.Language),
                    GetColumnName(Database.ColumnName.ListTitle),
                    GetColumnName(Database.ColumnName.MPAA),
                    GetColumnName(Database.ColumnName.MovieCount),
                    GetColumnName(Database.ColumnName.OriginalTitle),
                    GetColumnName(Database.ColumnName.Outline),
                    GetColumnName(Database.ColumnName.PlayCount),
                    GetColumnName(Database.ColumnName.Plot),
                    GetColumnName(Database.ColumnName.Premiered),
                    GetColumnName(Database.ColumnName.Ratings),
                    GetColumnName(Database.ColumnName.ReleaseDate),
                    GetColumnName(Database.ColumnName.Runtime),
                    GetColumnName(Database.ColumnName.SeasonNumber),
                    GetColumnName(Database.ColumnName.SortTitle),
                    GetColumnName(Database.ColumnName.SourceName),
                    GetColumnName(Database.ColumnName.Status),
                    GetColumnName(Database.ColumnName.Studios),
                    GetColumnName(Database.ColumnName.SubEpisode),
                    GetColumnName(Database.ColumnName.Tagline),
                    GetColumnName(Database.ColumnName.Tags),
                    GetColumnName(Database.ColumnName.Title),
                    GetColumnName(Database.ColumnName.Top250),
                    GetColumnName(Database.ColumnName.Trailer),
                    GetColumnName(Database.ColumnName.UniqueIDs),
                    GetColumnName(Database.ColumnName.UserRating),
                    GetColumnName(Database.ColumnName.VideoSource),
                    GetColumnName(Database.ColumnName.Year)
                }
                Return lstColumns.Contains(columnName)
            End If
            Return False
        End Function

        Public Shared Function ColumnIsScrapeModifier(ByVal columnName As String) As Boolean
            If Not String.IsNullOrEmpty(columnName) Then
                Dim lstColumns As String() = New String() {
                    GetColumnName(Database.ColumnName.BannerPath),
                    GetColumnName(Database.ColumnName.CharacterArtPath),
                    GetColumnName(Database.ColumnName.ClearArtPath),
                    GetColumnName(Database.ColumnName.ClearLogoPath),
                    GetColumnName(Database.ColumnName.DiscArtPath),
                    GetColumnName(Database.ColumnName.ExtrafanartsPath),
                    GetColumnName(Database.ColumnName.ExtrathumbsPath),
                    GetColumnName(Database.ColumnName.FanartPath),
                    GetColumnName(Database.ColumnName.HasMetaData),
                    GetColumnName(Database.ColumnName.HasMovieset),
                    GetColumnName(Database.ColumnName.HasSubtitles),
                    GetColumnName(Database.ColumnName.HasWatched),
                    GetColumnName(Database.ColumnName.LastPlayed),
                    GetColumnName(Database.ColumnName.KeyArtPath),
                    GetColumnName(Database.ColumnName.LandscapePath),
                    GetColumnName(Database.ColumnName.NfoPath),
                    GetColumnName(Database.ColumnName.PosterPath),
                    GetColumnName(Database.ColumnName.ThemePath),
                    GetColumnName(Database.ColumnName.TrailerPath)
                }
                Return lstColumns.Contains(columnName)
            End If
            Return False
        End Function

        Public Shared Function ColumnIsWatchedState(ByVal columnName As String) As Boolean
            If Not String.IsNullOrEmpty(columnName) Then
                Dim lstColumns As String() = New String() {
                    GetColumnName(Database.ColumnName.HasWatched),
                    GetColumnName(Database.ColumnName.LastPlayed)
                }
                Return lstColumns.Contains(columnName)
            End If
            Return False
        End Function

        Public Shared Function ConvertColumnNameToColumnType(ByVal columnName As String) As ColumnType
            If Not String.IsNullOrEmpty(columnName) Then
                Select Case columnName
                    Case GetColumnName(Database.ColumnName.BannerPath)
                        Return ColumnType.Banner
                    Case GetColumnName(Database.ColumnName.CharacterArtPath)
                        Return ColumnType.CharacterArt
                    Case GetColumnName(Database.ColumnName.ClearArtPath)
                        Return ColumnType.ClearArt
                    Case GetColumnName(Database.ColumnName.ClearLogoPath)
                        Return ColumnType.ClearLogo
                    Case GetColumnName(Database.ColumnName.DiscArtPath)
                        Return ColumnType.DiscArt
                    Case GetColumnName(Database.ColumnName.ExtrafanartsPath)
                        Return ColumnType.Extrafanarts
                    Case GetColumnName(Database.ColumnName.ExtrathumbsPath)
                        Return ColumnType.Extrathumbs
                    Case GetColumnName(Database.ColumnName.FanartPath)
                        Return ColumnType.Fanart
                    Case GetColumnName(Database.ColumnName.HasMetaData)
                        Return ColumnType.MetaData
                    Case GetColumnName(Database.ColumnName.HasMovieset)
                        Return ColumnType.Movieset
                    Case GetColumnName(Database.ColumnName.KeyArtPath)
                        Return ColumnType.KeyArt
                    Case GetColumnName(Database.ColumnName.LandscapePath)
                        Return ColumnType.Landscape
                    Case GetColumnName(Database.ColumnName.NfoPath)
                        Return ColumnType.NFO
                    Case GetColumnName(Database.ColumnName.PosterPath)
                        Return ColumnType.Poster
                    Case GetColumnName(Database.ColumnName.Ratings)
                        Return ColumnType.Rating
                    Case GetColumnName(Database.ColumnName.ThemePath)
                        Return ColumnType.Theme
                    Case GetColumnName(Database.ColumnName.HasSubtitles)
                        Return ColumnType.Subtitle
                    Case GetColumnName(Database.ColumnName.TrailerPath)
                        Return ColumnType.Trailer
                    Case GetColumnName(Database.ColumnName.UserRating)
                        Return ColumnType.UserRating
                    Case GetColumnName(Database.ColumnName.HasWatched),
                         GetColumnName(Database.ColumnName.LastPlayed),
                         GetColumnName(Database.ColumnName.PlayCount)
                        Return ColumnType.WatchedState
                End Select
            End If
            Return ColumnType.Unknown
        End Function

        Public Shared Function GetColumnName(ByVal item As ColumnName) As String
            Select Case item
                Case ColumnName.ActorName
                    'shortcut to "name"
                    Return GetColumnName(ColumnName.Name)
                Case ColumnName.Aired
                    Return "aired"
                Case ColumnName.BannerPath
                    Return "bannerPath"
                Case ColumnName.Certifications
                    Return "certification"
                Case ColumnName.CharacterArtPath
                    Return "characterartPath"
                Case ColumnName.ClearArtPath
                    Return "clearartPath"
                Case ColumnName.ClearLogoPath
                    Return "clearlogoPath"
                Case ColumnName.Countries
                    Return "country"
                Case ColumnName.Creators
                    Return "creator"
                Case ColumnName.Credits
                    Return "credits"
                Case ColumnName.DateAdded
                    Return "dateAdded"
                Case ColumnName.DateModified
                    Return "dateModified"
                Case ColumnName.Directors
                    Return "director"
                Case ColumnName.DiscArtPath
                    Return "discartPath"
                Case ColumnName.DisplayEpisode
                    Return "displayEpisode"
                Case ColumnName.DisplaySeason
                    Return "DisplaySeason"
                Case ColumnName.EpisodeGuideURL
                    Return "episodeGuide"
                Case ColumnName.EpisodeNumber
                    Return "episode"
                Case ColumnName.EpisodeOrdering
                    Return "episodeOrdering"
                Case ColumnName.EpisodeCount
                    Return "episodes"
                Case ColumnName.EpisodeSorting
                    Return "episodeSorting"
                Case ColumnName.Exclude
                    Return "exclude"
                Case ColumnName.ExtrafanartsPath
                    Return "efanartsPath"
                Case ColumnName.ExtrathumbsPath
                    Return "ethumbsPath"
                Case ColumnName.FanartPath
                    Return "fanartPath"
                Case ColumnName.Genres
                    Return "genre"
                Case ColumnName.HasMetaData
                    Return "hasMetaData"
                Case ColumnName.HasMovieset
                    Return "hasSet"
                Case ColumnName.HasSubtitles
                    Return "hasSub"
                Case ColumnName.HasWatched
                    Return "hasWatched"
                Case ColumnName.idEpisode
                    Return GetMainIdName(TableName.episode)
                Case ColumnName.idMedia
                    Return "idMedia"
                Case ColumnName.idMovie
                    Return GetMainIdName(TableName.movie)
                Case ColumnName.idPerson
                    Return GetMainIdName(TableName.person)
                Case ColumnName.idSeason
                    Return GetMainIdName(TableName.season)
                Case ColumnName.idSet
                    Return GetMainIdName(TableName.movieset)
                Case ColumnName.idShow
                    Return GetMainIdName(TableName.tvshow)
                Case ColumnName.idSource
                    Return GetMainIdName(TableName.moviesource)
                Case ColumnName.MediaType
                    Return "media_type"
                Case ColumnName.IsMissing
                    Return "missing"
                Case ColumnName.KeyArtPath
                    Return "keyartPath"
                Case ColumnName.LandscapePath
                    Return "landscapePath"
                Case ColumnName.Language
                    Return "language"
                Case ColumnName.LastPlayed
                    Return "lastPlayed"
                Case ColumnName.ListTitle
                    Return "listTitle"
                Case ColumnName.Locked
                    Return "locked"
                Case ColumnName.LockedEpisodesCount
                    Return "lockedEpisodes"
                Case ColumnName.Marked
                    Return "marked"
                Case ColumnName.MarkedCustom1
                    Return "markCustom1"
                Case ColumnName.MarkedCustom2
                    Return "markCustom2"
                Case ColumnName.MarkedCustom3
                    Return "markCustom3"
                Case ColumnName.MarkedCustom4
                    Return "markCustom4"
                Case ColumnName.MarkedEpisodesCount
                    Return "markedEpisodes"
                Case ColumnName.MovieCount
                    Return "movieCount"
                Case ColumnName.MovieTitles
                    Return "movieTitles"
                Case ColumnName.MPAA
                    Return "mpaa"
                Case ColumnName.Name
                    Return "name"
                Case ColumnName.New
                    Return "new"
                Case ColumnName.NewEpisodesCount
                    Return "newEpisodes"
                Case ColumnName.NfoPath
                    Return "nfoPath"
                Case ColumnName.OriginalTitle
                    Return "originalTitle"
                Case ColumnName.Outline
                    Return "outline"
                Case ColumnName.OutOfTolerance
                    Return "outOfTolerance"
                Case ColumnName.Path
                    Return "path"
                Case ColumnName.PlayCount
                    Return "playCount"
                Case ColumnName.Plot
                    Return "plot"
                Case ColumnName.PosterPath
                    Return "posterPath"
                Case ColumnName.Premiered
                    Return "premiered"
                Case ColumnName.ReleaseDate
                    Return "releaseDate"
                Case ColumnName.Role
                    Return "role"
                Case ColumnName.Runtime
                    Return "runtime"
                Case ColumnName.SeasonNumber
                    Return "season"
                Case ColumnName.SortedTitle
                    Return "sortedTitle"
                Case ColumnName.SortMethod
                    Return "sortMethod"
                Case ColumnName.SortTitle
                    Return "sortTitle"
                Case ColumnName.SourceName
                    Return "source"
                Case ColumnName.Status
                    Return "status"
                Case ColumnName.Studios
                    Return "studio"
                Case ColumnName.SubEpisode
                    Return "subEpisode"
                Case ColumnName.SubtitlesPath
                    Return "subPath"
                Case ColumnName.Tagline
                    Return "tagline"
                Case ColumnName.Tags
                    Return "tag"
                Case ColumnName.ThemePath
                    Return "themePath"
                Case ColumnName.Title
                    Return "title"
                Case ColumnName.Top250
                    Return "top250"
                Case ColumnName.Trailer
                    Return "trailer"
                Case ColumnName.TrailerPath
                    Return "trailerPath"
                Case ColumnName.UniqueIDs
                    Return "uniqueid"
                Case ColumnName.UserRating
                    Return "userRating"
                Case ColumnName.VideoSource
                    Return "videoSource"
                Case ColumnName.Year
                    Return "year"
                Case Else
                    Return String.Empty
            End Select
        End Function

        Public Shared Function GetColumnName(ByVal column As String) As ColumnName
            For Each item In [Enum].GetValues(GetType(ColumnName))
                If GetColumnName(DirectCast(item, ColumnName)) = column.ToLower Then Return DirectCast(item, ColumnName)
            Next
            Return Nothing
        End Function

        Public Shared Function GetDataType(ByVal item As ColumnName) As DataType
            Select Case item
                Case ColumnName.HasMetaData,
                     ColumnName.HasMovieset,
                     ColumnName.HasSubtitles,
                     ColumnName.HasWatched,
                     ColumnName.IsMissing,
                     ColumnName.Locked,
                     ColumnName.Marked,
                     ColumnName.MarkedCustom1,
                     ColumnName.MarkedCustom2,
                     ColumnName.MarkedCustom3,
                     ColumnName.MarkedCustom4,
                     ColumnName.New,
                     ColumnName.SubtitleForced
                    Return DataType.Boolean
                Case ColumnName.VideoAspect
                    Return DataType.Double
                Case ColumnName.AudioBitrate,
                     ColumnName.AudioChannels,
                     ColumnName.DateAdded,
                     ColumnName.DateModified,
                     ColumnName.DisplayEpisode,
                     ColumnName.DisplaySeason,
                     ColumnName.EpisodeCount,
                     ColumnName.EpisodeNumber,
                     ColumnName.LastPlayed,
                     ColumnName.LockedEpisodesCount,
                     ColumnName.MarkedEpisodesCount,
                     ColumnName.MovieCount,
                     ColumnName.NewEpisodesCount,
                     ColumnName.OutOfTolerance,
                     ColumnName.PlayCount,
                     ColumnName.SeasonNumber,
                     ColumnName.SubEpisode,
                     ColumnName.Top250,
                     ColumnName.UserRating,
                     ColumnName.VideoBitDepth,
                     ColumnName.VideoBitrate,
                     ColumnName.VideoDuration,
                     ColumnName.VideoHeight,
                     ColumnName.VideoMultiViewCount,
                     ColumnName.VideoWidth,
                     ColumnName.Year
                    Return DataType.Integer
                Case ColumnName.idEpisode, ColumnName.idMovie, ColumnName.idSeason, ColumnName.idSet, ColumnName.idShow
                    Return DataType.Long
                Case Else
                    Return DataType.String
            End Select
            Return Nothing
        End Function

        Public Shared Function GetMainIdName(ByVal item As TableName) As String
            Select Case item
                Case TableName.actor_link, TableName.creator_link, TableName.director_link, TableName.gueststar_link, TableName.person, TableName.writer_link
                    Return "idPerson"
                Case TableName.art
                    Return "idArt"
                Case TableName.certification
                    Return "idCertification"
                Case TableName.country
                    Return "idCountry"
                Case TableName.episode
                    Return "idEpisode"
                Case TableName.file
                    Return "idFile"
                Case TableName.genre
                    Return "idGenre"
                Case TableName.movie
                    Return "idMovie"
                Case TableName.movieset
                    Return "idSet"
                Case TableName.moviesource
                    Return "idSource"
                Case TableName.rating
                    Return "idRating"
                Case TableName.season
                    Return "idSeason"
                Case TableName.studio
                    Return "idStudio"
                Case TableName.tag
                    Return "idTag"
                Case TableName.tvshow
                    Return "idShow"
                Case TableName.tvshowsource
                    Return "idSource"
                Case TableName.uniqueid
                    Return "idUniqueID"
            End Select
            Return String.Empty
        End Function

        Public Shared Function GetMainIdName(ByVal viewName As String) As String
            Select Case viewName
                Case "movielist"
                    Return GetMainIdName(TableName.movie)
                Case "moviesetlist"
                    Return GetMainIdName(TableName.movieset)
                Case "episodelist"
                    Return GetMainIdName(TableName.episode)
                Case "seasonlist"
                    Return GetMainIdName(TableName.season)
                Case "tvshowlist"
                    Return GetMainIdName(TableName.tvshow)
            End Select
            Return String.Empty
        End Function

        Public Shared Function GetMainViewName(ByVal contentType As Enums.ContentType) As String
            Select Case contentType
                Case Enums.ContentType.Movie
                    Return "movielist"
                Case Enums.ContentType.Movieset
                    Return "moviesetlist"
                Case Enums.ContentType.Moviesource
                    Return "moviesource"
                Case Enums.ContentType.TVEpisode
                    Return "episodelist"
                Case Enums.ContentType.TVSeason
                    Return "seasonlist"
                Case Enums.ContentType.TVShow
                    Return "tvshowlist"
                Case Enums.ContentType.TVShowsource
                    Return "tvshowsource"
            End Select
            Return String.Empty
        End Function

        Public Shared Function GetTableName(ByVal tableName As TableName) As String
            Select Case tableName
                Case TableName.actor_link
                    Return "actor_link"
                Case TableName.art
                    Return "art"
                Case TableName.certification
                    Return "certification"
                Case TableName.certification_link
                    Return "certification_link"
                Case TableName.country
                    Return "country"
                Case TableName.country_link
                    Return "country_link"
                Case TableName.creator_link
                    Return "creator_link"
                Case TableName.director_link
                    Return "director_link"
                Case TableName.episode
                    Return "episode"
                Case TableName.excludedpath
                    Return "excludedpath"
                Case TableName.file
                    Return "file"
                Case TableName.genre
                    Return "genre"
                Case TableName.genre_link
                    Return "genre_link"
                Case TableName.gueststar_link
                    Return "gueststar_link"
                Case TableName.movie
                    Return "movie"
                Case TableName.movieset
                    Return "movieset"
                Case TableName.movieset_link
                    Return "movieset_link"
                Case TableName.moviesource
                    Return "moviesource"
                Case TableName.person
                    Return "person"
                Case TableName.rating
                    Return "rating"
                Case TableName.season
                    Return "season"
                Case TableName.streamdetail
                    Return "streamdetail"
                Case TableName.studio
                    Return "studio"
                Case TableName.studio_link
                    Return "studio_link"
                Case TableName.tag
                    Return "tag"
                Case TableName.tag_link
                    Return "tag_link"
                Case TableName.tvshow
                    Return "tvshow"
                Case TableName.tvshowsource
                    Return "tvshowsource"
                Case TableName.tvshow_link
                    Return "tvshow_link"
                Case TableName.uniqueid
                    Return "uniqueid"
                Case TableName.writer_link
                    Return "writer_link"
            End Select
            Return String.Empty
        End Function

#End Region 'Methods

    End Class

    Public Class SQLViewProperty

#Region "Properties"

        Public Property Name() As String = String.Empty

        Public Property Statement() As String = String.Empty

#End Region 'Properties 

    End Class

#End Region 'Nested Types

End Class