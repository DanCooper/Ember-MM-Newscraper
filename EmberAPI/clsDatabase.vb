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
    ' NOTE: This will use another DB because: can grow alot, Don't want to stress Media DB with this stuff
    'Protected _jobsDBConn As SQLiteConnection

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.AddonEventType, ByRef _params As List(Of Object))

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property MyVideosDBConn() As SQLiteConnection
        Get
            Return _myvideosDBConn
        End Get
    End Property

#End Region 'Properties

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
        ''' <summary>
        ''' Dummy trigger, not a database column
        ''' </summary>
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
        GetYear
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
        IsSingle
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
        ''' <summary>
        ''' Dummy trigger, not a database column
        ''' </summary>
        MoviesInMovieset
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
        Role
        Runtime
        ScanRecursive
        SeasonNumber
        SortMethod
        SortTitle
        SortedTitle
        ''' <summary>
        ''' Name of the source (only available in views)
        ''' </summary>
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
        UseFolderName
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
    ''' <summary>
    ''' add or update actor
    ''' </summary>
    ''' <param name="name">actor name</param>
    ''' <param name="thumbURL">thumb URL</param>
    ''' <param name="thumb">local thumb path</param>
    ''' <param name="imdbId">IMDB ID of actor</param>
    ''' <param name="tbdmId">TMDB ID of actor</param>
    ''' <param name="isActor"><c>True</c> if adding an actor, <c>False</c> if adding a Creator, Director, Writer or something else without ID's and images to refresh if already exist in actors table</param>
    ''' <returns><c>ID</c> of actor in actors table</returns>
    ''' <remarks></remarks>
    Private Function Add_Actor(ByVal name As String, ByVal thumbURL As String, ByVal thumb As String, ByVal imdbId As String, ByVal tbdmId As String, ByVal isActor As Boolean) As Long
        Dim doesExist As Boolean = False
        Dim ID As Long = -1

        Using SQLcommand_select_actors As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_select_actors.CommandText = String.Format("SELECT idActor FROM actors WHERE strActor LIKE ?", name)
            Dim par_select_actors_strActor As SQLiteParameter = SQLcommand_select_actors.Parameters.Add("par_select_actors_strActor", DbType.String, 0, "strActor")
            par_select_actors_strActor.Value = name
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
                par_insert_actors_strActor.Value = name
                par_insert_actors_strThumb.Value = thumbURL
                par_insert_actors_strIMDB.Value = imdbId
                par_insert_actors_strTMDB.Value = tbdmId
                ID = CInt(SQLcommand_insert_actors.ExecuteScalar())
            End Using
        ElseIf isActor Then
            Using SQLcommand_update_actors As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand_update_actors.CommandText = String.Format("UPDATE actors SET strThumb=?, strIMDB=?, strTMDB=? WHERE idActor={0}", ID)
                Dim par_update_actors_strThumb As SQLiteParameter = SQLcommand_update_actors.Parameters.Add("par_actors_strThumb", DbType.String, 0, "strThumb")
                Dim par_update_actors_strIMDB As SQLiteParameter = SQLcommand_update_actors.Parameters.Add("par_actors_strIMDB", DbType.String, 0, "strIMDB")
                Dim par_update_actors_strTMDB As SQLiteParameter = SQLcommand_update_actors.Parameters.Add("par_actors_strTMDB", DbType.String, 0, "strTMDB")
                par_update_actors_strThumb.Value = thumbURL
                par_update_actors_strIMDB.Value = imdbId
                par_update_actors_strTMDB.Value = tbdmId
                SQLcommand_update_actors.ExecuteNonQuery()
            End Using
        End If

        If Not ID = -1 Then
            If Not String.IsNullOrEmpty(thumb) Then
                Set_ArtForItem(ID, "actor", "thumb", thumb)
            End If
        End If

        Return ID
    End Function

    Private Sub Add_Artist_Musicvideo(ByVal idMVideo As Long, ByVal idArtist As Long)
        Add_ToLinkTable("artistlinkmusicvideo", "idArtist", idArtist, "idMVideo", idMVideo)
    End Sub

    Private Sub Add_Cast(ByVal idMedia As Long, ByVal table As String, ByVal field As String, ByVal cast As List(Of MediaContainers.Person))
        If cast Is Nothing Then Return

        Dim iOrder As Integer = 0
        For Each actor As MediaContainers.Person In cast
            Dim idActor = Add_Actor(actor.Name, actor.URLOriginal, actor.LocalFilePath, actor.IMDbId, actor.TMDbId, True)
            Add_LinkToActor(table, idActor, field, idMedia, actor.Role, iOrder)
            iOrder += 1
        Next
    End Sub

    Private Sub Add_Creator_TVShow(ByVal idShow As Long, ByVal idActor As Long)
        Add_ToLinkTable("creatorlinktvshow", "idActor", idActor, "idShow", idShow)
    End Sub

    Private Function Add_Country(ByVal strCountry As String) As Long
        If String.IsNullOrEmpty(strCountry) Then Return -1
        Dim ID As Long = Add_ToTable("country", "idCountry", "strCountry", strCountry)
        LoadAll_Countries()
        Return ID
    End Function

    Private Sub Add_Country_Movie(ByVal idMovie As Long, ByVal idCountry As Long)
        Add_ToLinkTable("countrylinkmovie", "idCountry", idCountry, "idMovie", idMovie)
    End Sub

    Private Sub Add_Country_TVShow(ByVal idShow As Long, ByVal idCountry As Long)
        Add_ToLinkTable("countrylinktvshow", "idCountry", idCountry, "idShow", idShow)
    End Sub

    Private Sub Add_Director_Movie(ByVal idMovie As Long, ByVal idDirector As Long)
        Add_ToLinkTable("directorlinkmovie", "idDirector", idDirector, "idMovie", idMovie)
    End Sub

    Private Sub Add_Director_Musicvideo(ByVal idMVideo As Long, ByVal idDirector As Long)
        Add_ToLinkTable("directorlinkmusicvideo", "idDirector", idDirector, "idMVideo", idMVideo)
    End Sub

    Private Sub Add_Director_TVEpisode(ByVal idEpisode As Long, ByVal idDirector As Long)
        Add_ToLinkTable("directorlinkepisode", "idDirector", idDirector, "idEpisode", idEpisode)
    End Sub

    Private Sub Add_Director_TVShow(ByVal idShow As Long, ByVal idDirector As Long)
        Add_ToLinkTable("directorlinktvshow", "idDirector", idDirector, "idShow", idShow)
    End Sub

    Public Sub Add_ExcludedPath(ByVal path As String)
        Using sqlTransaction As SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using sqlCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                sqlCommand.CommandText = "INSERT OR REPLACE INTO ExcludeDir (path) VALUES (?);"
                Dim par_path As SQLiteParameter = sqlCommand.Parameters.Add("par_Dirname", DbType.String, 0, path)
                par_path.Value = path
                sqlCommand.ExecuteNonQuery()
            End Using
            sqlTransaction.Commit()
        End Using
    End Sub

    Private Function Add_Genre(ByVal genre As String) As Long
        If String.IsNullOrEmpty(genre) Then Return -1
        Dim ID As Long = Add_ToTable("genre", "idGenre", "strGenre", genre)
        LoadAll_Genres()
        Return ID
    End Function

    Private Sub Add_Genre_Movie(ByVal idMovie As Long, ByVal idGenre As Long, ByVal sorting As Integer)
        Add_ToLinkTable("genrelinkmovie", "idGenre", idGenre, "idMovie", idMovie, "sorting", sorting)
    End Sub

    Private Sub Add_Genre_Musicvideo(ByVal idMVideo As Long, ByVal idGenre As Long, ByVal sorting As Integer)
        Add_ToLinkTable("genrelinkmusicvideo", "idGenre", idGenre, "idMVideo", idMVideo, "sorting", sorting)
    End Sub

    Private Sub Add_Genre_TVShow(ByVal idShow As Long, ByVal idGenre As Long, ByVal sorting As Integer)
        Add_ToLinkTable("genrelinktvshow", "idGenre", idGenre, "idShow", idShow, "sorting", sorting)
    End Sub

    Private Sub Add_GuestStar(ByVal idMedia As Long, ByVal table As String, ByVal field As String, ByVal cast As List(Of MediaContainers.Person))
        If cast Is Nothing Then Return

        Dim iOrder As Integer = 0
        For Each actor As MediaContainers.Person In cast
            Dim idActor = Add_Actor(actor.Name, actor.URLOriginal, actor.LocalFilePath, actor.IMDbId, actor.TMDbId, True)
            Add_LinkToGuestStar(table, idActor, field, idMedia, actor.Role, iOrder)
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
    Private Function Add_LinkToActor(ByVal table As String, ByVal actorID As Long, ByVal field As String,
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
    Private Function Add_LinkToGuestStar(ByVal table As String, ByVal actorID As Long, ByVal field As String,
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

    Private Sub Add_LinkToMovieset_Movie(ByVal idMovie As Long, ByVal idSet As Long, ByVal order As Integer)
        Add_ToLinkTable("setlinkmovie", "idSet", idSet, "idMovie", idMovie, "iOrder", order)
    End Sub

    Private Function Add_Rating(ByVal idMedia As Long,
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
                par_rating_type.Value = rating.Type
                par_rating_max.Value = rating.Max
                par_rating.Value = rating.Value
                par_votes.Value = rating.Votes
                par_isDefault.Value = rating.IsDefault
                Return CLng(sqlCommand.ExecuteScalar())
            End Using
        End If
        Return -1
    End Function

    Private Function Add_Studio(ByVal strStudio As String) As Long
        If String.IsNullOrEmpty(strStudio) Then Return -1
        Dim ID As Long = Add_ToTable("studio", "idStudio", "strStudio", strStudio)
        LoadAll_Studios()
        Return ID
    End Function

    Private Sub Add_Studio_Movie(ByVal idMovie As Long, ByVal idStudio As Long)
        Add_ToLinkTable("studiolinkmovie", "idStudio", idStudio, "idMovie", idMovie)
    End Sub

    Private Sub Add_Studio_TVShow(ByVal idShow As Long, ByVal idStudio As Long)
        Add_ToLinkTable("studiolinktvshow", "idStudio", idStudio, "idShow", idShow)
    End Sub

    Private Function Add_Tag(ByVal strTag As String) As Long
        If String.IsNullOrEmpty(strTag) Then Return -1
        Return Add_ToTable("tag", "idTag", "strTag", strTag)
    End Function

    Private Function Add_ToLinkTable(ByVal table As String, ByVal firstField As String, ByVal firstID As Long, ByVal secondField As String, ByVal secondID As Long,
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

    Private Function Add_ToLinkTable(ByVal table As String, ByVal firstField As String, ByVal firstID As Long, ByVal secondField As String, ByVal secondID As Long,
                               ByVal thirdField As String, ByVal thirdValue As Integer,
                               Optional ByVal typeField As String = "", Optional ByVal type As String = "") As Boolean
        Dim doesExist As Boolean = False

        Using SQLcommand_select As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_select.CommandText = String.Format("SELECT * FROM {0} WHERE {1}={2} AND {3}={4} AND {5}={6}", table, firstField, firstID, secondField, secondID, thirdField, thirdValue)
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
                    SQLcommand_insert.CommandText = String.Format("INSERT INTO {0} ({1},{2},{3}) VALUES ({4},{5},{6})", table, firstField, secondField, thirdField, firstID, secondID, thirdValue)
                Else
                    SQLcommand_insert.CommandText = String.Format("INSERT INTO {0} ({1},{2},{3},{4}) VALUES ({5},{6},{7},'{8}')", table, firstField, secondField, thirdField, typeField, firstID, secondID, thirdValue, type)
                End If
                SQLcommand_insert.ExecuteNonQuery()
                Return True
            End Using
        Else
            Return False
        End If
    End Function

    Private Function Add_ToTable(ByVal table As String, ByVal firstField As String, ByVal secondField As String, ByVal value As String) As Long
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

    Private Sub Add_TagToItem(ByVal idMedia As Long, ByVal idTag As Long, ByVal type As String, ByVal sorting As Integer)
        If String.IsNullOrEmpty(type) Then Return
        Add_ToLinkTable("taglinks", "idTag", idTag, "idMedia", idMedia, "sorting", sorting, "media_type", type)
    End Sub

    Private Function Add_UniqueID(ByVal idMedia As Long,
                                  ByVal mediaType As String,
                                  ByVal uniqueID As MediaContainers.Uniqueid) As Long
        If Not idMedia = -1 AndAlso Not String.IsNullOrEmpty(mediaType) AndAlso uniqueID.TypeSpecified AndAlso uniqueID.ValueSpecified Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
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

    Private Sub Add_Writer_Movie(ByVal idMovie As Long, ByVal idWriter As Long)
        Add_ToLinkTable("writerlinkmovie", "idWriter", idWriter, "idMovie", idMovie)
    End Sub

    Private Sub Add_Writer_TVEpisode(ByVal idEpisode As Long, ByVal idWriter As Long)
        Add_ToLinkTable("writerlinkepisode", "idWriter", idWriter, "idEpisode", idEpisode)
    End Sub

    Public Sub Change_TVEpisode(ByVal _episode As DBElement, ByVal ListOfEpisodes As List(Of MediaContainers.MainDetails), Optional ByVal bBatchmode As Boolean = False)
        Dim newEpisodesList As New List(Of DBElement)

        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not bBatchmode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLPCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()

            'first step: remove all existing episode informations for this file and set it to "Missing"
            Delete_TVEpisode(_episode.Filename, False, True)

            'second step: create new episode DBElements and save it to database
            For Each tEpisode In ListOfEpisodes
                Dim newEpisode As New DBElement(Enums.ContentType.TVEpisode)
                newEpisode = New DBElement(Enums.ContentType.TVEpisode)
                newEpisode = CType(_episode.CloneDeep, DBElement)
                newEpisode.FilenameID = -1
                newEpisode.ID = -1
                newEpisode.MainDetails = tEpisode
                newEpisode.MainDetails.FileInfo = _episode.MainDetails.FileInfo
                newEpisode.MainDetails.VideoSource = newEpisode.VideoSource
                Save_TVEpisode(newEpisode, True, True, True, True, False)
                newEpisodesList.Add(newEpisode)
            Next

            For Each tEpisode As DBElement In newEpisodesList
                Addons.Instance.RunGeneric(Enums.AddonEventType.DuringUpdateDB_TV, Nothing, Nothing, False, tEpisode)
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
    ''' <param name="SourceIDs">Optional. If provided, only process entries from that source.</param>
    ''' <remarks></remarks>
    Public Sub Clean(ByVal CleanMovies As Boolean, ByVal CleanMovieSets As Boolean, ByVal CleanTVShows As Boolean, Optional ByVal SourceIDs As List(Of Long) = Nothing)
        Dim fInfo As FileInfo
        Dim tPath As String = String.Empty
        Dim sPath As String = String.Empty

        logger.Info("Cleaning videodatabase started")

        Using SQLtransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
            If CleanMovies Then
                logger.Info("Cleaning movies started")
                Dim MoviePaths As List(Of String) = GetAll_Paths_Movie()
                MoviePaths.Sort()

                'get a listing of sources and their recursive properties
                Dim SourceList As New List(Of DBSource)
                Dim tSource As DBSource

                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    If SourceIDs IsNot Nothing AndAlso SourceIDs.Count > 0 Then
                        Dim lstWhereClauses As List(Of String) = SourceIDs.Select(Function(f) String.Concat("idSource = ", f.ToString)).ToList
                        SQLcommand.CommandText = String.Concat("SELECT DISTINCT * FROM moviesource WHERE ", String.Join(" OR ", lstWhereClauses), ";")
                    Else
                        SQLcommand.CommandText = "SELECT * FROM moviesource;"
                    End If
                    Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                        While SQLreader.Read
                            SourceList.Add(Load_Source_Movie(Convert.ToInt64(SQLreader("idSource"))))
                        End While
                    End Using
                End Using

                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    If SourceIDs IsNot Nothing AndAlso SourceIDs.Count > 0 Then
                        Dim lstWhereClauses As List(Of String) = SourceIDs.Select(Function(f) String.Concat("idSource = ", f.ToString)).ToList
                        SQLcommand.CommandText = String.Concat("SELECT DISTINCT MoviePath, idMovie, idSource, Type FROM movie WHERE ", String.Join(" OR ", lstWhereClauses), " ORDER BY MoviePath DESC;")
                    Else
                        SQLcommand.CommandText = "SELECT MoviePath, idMovie, idSource, Type FROM movie ORDER BY MoviePath DESC;"
                    End If
                    Using SQLReader As SQLiteDataReader = SQLcommand.ExecuteReader()
                        While SQLReader.Read
                            If Not File.Exists(SQLReader("MoviePath").ToString) OrElse Not Master.eSettings.Options.FileSystem.ValidVideoExtensions.Contains(Path.GetExtension(SQLReader("MoviePath").ToString).ToLower) OrElse
                                Master.DB.GetAll_ExcludedPaths.Exists(Function(s) SQLReader("MoviePath").ToString.ToLower.StartsWith(s.ToLower)) Then
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
                    If SourceIDs IsNot Nothing AndAlso SourceIDs.Count > 0 Then
                        Dim lstWhereClauses As List(Of String) = SourceIDs.Select(Function(f) String.Concat("episode.idSource = ", f.ToString)).ToList
                        SQLcommand.CommandText = String.Concat("SELECT DISTINCT files.strFilename, episode.idEpisode FROM files INNER JOIN episode ON (files.idFile = episode.idFile) WHERE ", String.Join(" OR ", lstWhereClauses), " ORDER BY files.strFilename;")
                    Else
                        SQLcommand.CommandText = "SELECT files.strFilename, episode.idEpisode FROM files INNER JOIN episode ON (files.idFile = episode.idFile) ORDER BY files.strFilename;"
                    End If

                    Using SQLReader As SQLiteDataReader = SQLcommand.ExecuteReader()
                        While SQLReader.Read
                            If Not File.Exists(SQLReader("strFilename").ToString) OrElse Not Master.eSettings.Options.FileSystem.ValidVideoExtensions.Contains(Path.GetExtension(SQLReader("strFilename").ToString).ToLower) OrElse
                                Master.DB.GetAll_ExcludedPaths.Exists(Function(s) SQLReader("strFilename").ToString.ToLower.StartsWith(s.ToLower)) Then
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
                    If APIXML.CertificationMappings.RunMapping(tmpDBElement.MainDetails.Certifications, False) Then
                        'run merge because of the "use certification as MPAA value" settings
                        tmpDBElement.ScrapeModifiers.Information = True
                        tmpDBElement.ScrapeOptions.Certifications = True
                        Information.MergeDataScraperResults_Movie(tmpDBElement, New List(Of MediaContainers.MainDetails))
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
                    If APIXML.CertificationMappings.RunMapping(tmpDBElement.MainDetails.Certifications, False) Then
                        'run merge because of the "use certification as MPAA value" settings
                        tmpDBElement.ScrapeModifiers.Information = True
                        tmpDBElement.ScrapeOptions.Certifications = True
                        Information.MergeDataScraperResults_TV(tmpDBElement, New List(Of MediaContainers.MainDetails))
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
                    If APIXML.CountryMappings.RunMapping(tmpDBElement.MainDetails.Countries, False) Then
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
                    If APIXML.CountryMappings.RunMapping(tmpDBElement.MainDetails.Countries, False) Then
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
                    If APIXML.GenreMappings.RunMapping(tmpDBElement.MainDetails.Genres, False) Then
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
                    If APIXML.GenreMappings.RunMapping(tmpDBElement.MainDetails.Genres, False) Then
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
                    If APIXML.StatusMappings.RunMapping(tmpDBElement.MainDetails.Status, False) Then
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
                    If APIXML.StudioMappings.RunMapping(tmpDBElement.MainDetails.Studios, False) Then
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
                    If APIXML.StudioMappings.RunMapping(tmpDBElement.MainDetails.Studios, False) Then
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
    Public Sub Clear_New()
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
        Dim MyVideosDBVersion As Integer = 48

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

    Private Function Convert_ContentTypeToMediaType(contentType As Enums.ContentType) As String
        Select Case contentType
            Case Enums.ContentType.Movie
                Return "movie"
            Case Enums.ContentType.Movieset
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
        Addons.Instance.RunGeneric(Enums.AddonEventType.Remove_Movie, Nothing, Nothing, False, _movieDB)

        Try
            Dim SQLtransaction As SQLiteTransaction = Nothing
            If Not BatchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DELETE FROM movie WHERE idMovie = ", ID, ";")
                SQLcommand.ExecuteNonQuery()
            End Using
            If Not BatchMode Then SQLtransaction.Commit()

            RaiseEvent GenericEvent(Enums.AddonEventType.Remove_Movie, New List(Of Object)(New Object() {_movieDB.ID}))
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            Return False
        End Try
        Return True
    End Function
    ''' <summary>
    ''' Remove all information related to a movieset from the database.
    ''' </summary>
    ''' <param name="id">ID of the movieset to remove, as stored in the database.</param>
    ''' <param name="batchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Function Delete_MovieSet(ByVal id As Long, ByVal batchMode As Boolean) As Boolean
        Try
            'first get a list of all movies in the movieset to remove the movieset information from NFO
            Dim moviesToSave As New List(Of DBElement)

            Dim SQLtransaction As SQLiteTransaction = Nothing
            If Not batchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("SELECT idMovie FROM setlinkmovie ",
                                                       "WHERE idSet = ", id, ";")
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
                    movie.MainDetails.Sets.RemoveSet(id)
                    Addons.Instance.RunGeneric(Enums.AddonEventType.BeforeEdit_Movie, Nothing, Nothing, False, movie)
                    Addons.Instance.RunGeneric(Enums.AddonEventType.AfterEdit_Movie, Nothing, Nothing, False, movie)
                    Save_Movie(movie, batchMode, True, False, True, False)
                    RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_Movie, New List(Of Object)(New Object() {movie.ID}))
                Next
            End If

            'delete all movieset images and if this setting is enabled
            If Master.eSettings.MovieSetCleanFiles Then
                Dim MovieSet As DBElement = Master.DB.Load_Movieset(id)
                Images.Delete(MovieSet, Enums.ModifierType.MainBanner)
                Images.Delete(MovieSet, Enums.ModifierType.MainClearArt)
                Images.Delete(MovieSet, Enums.ModifierType.MainClearLogo)
                Images.Delete(MovieSet, Enums.ModifierType.MainDiscArt)
                Images.Delete(MovieSet, Enums.ModifierType.MainFanart)
                Images.Delete(MovieSet, Enums.ModifierType.MainKeyart)
                Images.Delete(MovieSet, Enums.ModifierType.MainLandscape)
                Images.Delete(MovieSet, Enums.ModifierType.MainPoster)
            End If

            'remove the movieset and still existing setlinkmovie entries
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DELETE FROM sets WHERE idSet = ", id, ";")
                SQLcommand.ExecuteNonQuery()
            End Using
            If Not batchMode Then SQLtransaction.Commit()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            Return False
        End Try
        Return True
    End Function
    ''' <summary>
    ''' Remove all information related to a TV episode from the database.
    ''' </summary>
    ''' <param name="episodeId">ID of the episode to remove, as stored in the database.</param>
    ''' <param name="batchMode">Is this function already part of a transaction?</param>
    ''' <returns><c>True</c> if has been removed, <c>False</c> if has been changed to missing</returns>
    Public Function Delete_TVEpisode(ByVal episodeId As Long, ByVal force As Boolean, ByVal cleanSeasons As Boolean, ByVal batchMode As Boolean) As Boolean
        Dim SQLtransaction As SQLiteTransaction = Nothing
        Dim doesExist As Boolean = False
        Dim bHasRemoved As Boolean = False

        Dim _tvepisodeDB As DBElement = Load_TVEpisode(episodeId, True)
        Addons.Instance.RunGeneric(Enums.AddonEventType.Remove_TVEpisode, Nothing, Nothing, False, _tvepisodeDB)

        If Not batchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT idFile, Episode, Season, idShow FROM episode WHERE idEpisode = ", episodeId, ";")
            Using SQLReader As SQLiteDataReader = SQLcommand.ExecuteReader
                While SQLReader.Read
                    Using SQLECommand As SQLiteCommand = _myvideosDBConn.CreateCommand()

                        If Not force Then
                            'check if there is another episode with same season and episode number (in this case we don't need a another "Missing" episode)
                            Using SQLcommand_select As SQLiteCommand = _myvideosDBConn.CreateCommand
                                SQLcommand_select.CommandText = String.Format("SELECT COUNT(episode.idEpisode) AS eCount FROM episode WHERE NOT idEpisode = {0} AND Season = {1} AND Episode = {2} AND idShow = {3}", episodeId, SQLReader("Season"), SQLReader("Episode"), SQLReader("idShow"))
                                Using SQLReader_select As SQLiteDataReader = SQLcommand_select.ExecuteReader
                                    While SQLReader_select.Read
                                        If CInt(SQLReader_select("eCount")) > 0 Then doesExist = True
                                    End While
                                End Using
                            End Using
                        End If

                        If force OrElse doesExist Then
                            SQLECommand.CommandText = String.Concat("DELETE FROM episode WHERE idEpisode = ", episodeId, ";")
                            SQLECommand.ExecuteNonQuery()

                            If cleanSeasons Then Master.DB.Delete_Empty_TVSeasons(Convert.ToInt64(SQLReader("idShow")), True)
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
                            SQLECommand.CommandText = String.Concat("DELETE FROM TVVStreams WHERE TVEpID = ", episodeId, ";")
                            SQLECommand.ExecuteNonQuery()
                            SQLECommand.CommandText = String.Concat("DELETE FROM TVAStreams WHERE TVEpID = ", episodeId, ";")
                            SQLECommand.ExecuteNonQuery()
                            SQLECommand.CommandText = String.Concat("DELETE FROM TVSubs WHERE TVEpID = ", episodeId, ";")
                            SQLECommand.ExecuteNonQuery()
                            SQLECommand.CommandText = String.Concat("DELETE FROM art WHERE media_id = ", episodeId, " AND media_type = 'episode';")
                            SQLECommand.ExecuteNonQuery()
                            SQLECommand.CommandText = String.Concat("UPDATE episode SET New = 0, ",
                                                                    "idFile = -1, NfoPath = '', ",
                                                                    "VideoSource = '' WHERE idEpisode = ", episodeId, ";")
                            SQLECommand.ExecuteNonQuery()
                        End If
                    End Using
                End While
            End Using
        End Using

        If Not batchMode Then
            SQLtransaction.Commit()
        End If

        Return bHasRemoved
    End Function

    Public Function Delete_TVEpisode(ByVal fullPath As String, ByVal force As Boolean, ByVal batchMode As Boolean) As Boolean
        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not batchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
        Using SQLPCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLPCommand.CommandText = String.Concat("SELECT idFile FROM files WHERE strFilename = """, fullPath, """;")
            Using SQLPReader As SQLiteDataReader = SQLPCommand.ExecuteReader
                While SQLPReader.Read
                    Using SQLCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        SQLCommand.CommandText = String.Concat("SELECT idEpisode FROM episode WHERE idFile = ", SQLPReader("idFile"), ";")
                        Using SQLReader As SQLiteDataReader = SQLCommand.ExecuteReader
                            While SQLReader.Read
                                Delete_TVEpisode(CInt(SQLReader("idEpisode")), force, False, batchMode)
                            End While
                        End Using
                    End Using
                End While
            End Using
        End Using
        If Not batchMode Then SQLtransaction.Commit()

        Return True
    End Function

    ''' <summary>
    ''' Remove all information related to a TV season from the database.
    ''' </summary>
    ''' <param name="batchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Function Delete_TVSeason(ByVal id As Long, ByVal batchMode As Boolean) As Boolean
        If id < 0 Then Throw New ArgumentOutOfRangeException("idSeason", "Value must be >= 0, was given: " & id)

        Try
            Dim SQLtransaction As SQLiteTransaction = Nothing
            If Not batchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DELETE FROM seasons WHERE idSeason = ", id, ";")
                SQLcommand.ExecuteNonQuery()
            End Using
            If Not batchMode Then SQLtransaction.Commit()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Remove all information related to a TV season from the database.
    ''' </summary>
    ''' <param name="showID">ID of the tvshow to remove, as stored in the database.</param>
    ''' <param name="batchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Function Delete_TVSeason(ByVal showID As Long, ByVal seasonNumber As Integer, ByVal batchMode As Boolean) As Boolean
        If showID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & showID)
        If seasonNumber < 0 Then Throw New ArgumentOutOfRangeException("iSeason", "Value must be >= 0, was given: " & seasonNumber)

        Try
            Dim SQLtransaction As SQLiteTransaction = Nothing
            If Not batchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DELETE FROM seasons WHERE idShow = ", showID, " AND Season = ", seasonNumber, ";")
                SQLcommand.ExecuteNonQuery()
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
    ''' <param name="id">ID of the tvshow to remove, as stored in the database.</param>
    ''' <param name="batchMode">Is this function already part of a transaction?</param>
    ''' <returns>True if successful, false if deletion failed.</returns>
    Public Function Delete_TVShow(ByVal id As Long, ByVal batchMode As Boolean) As Boolean
        If id < 0 Then Throw New ArgumentOutOfRangeException("idShow", "Value must be >= 0, was given: " & id)

        Dim _tvshowDB As DBElement = Load_TVShow_Full(id)
        Addons.Instance.RunGeneric(Enums.AddonEventType.Remove_TVShow, Nothing, Nothing, False, _tvshowDB)

        Try
            Dim SQLtransaction As SQLiteTransaction = Nothing
            If Not batchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Concat("DELETE FROM tvshow WHERE idShow = ", id, ";")
                SQLcommand.ExecuteNonQuery()
            End Using
            If Not batchMode Then SQLtransaction.Commit()

            RaiseEvent GenericEvent(Enums.AddonEventType.Remove_TVShow, New List(Of Object)(New Object() {_tvshowDB.ID}))
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Fill DataTable with data returned from the provided command
    ''' </summary>
    ''' <param name="table">DataTable to fill</param>
    ''' <param name="command">SQL Command to process</param>
    Public Sub FillDataTable(ByRef table As DataTable, ByVal command As String)
        table.Clear()
        Dim sqlDA As New SQLiteDataAdapter(command, _myvideosDBConn)
        sqlDA.Fill(table)
    End Sub

    Public Sub FillDataTable_Movie(ByRef table As DataTable, ByVal command As String)
        table = New DataTable
        FillDataTable(table, command)

        'Table manipulation
        'add column "listTitle" and create value
        'add column "Year" and create value
        'modify "SortedTitle" (add sort tokens to the end)
        table.Columns.Add("ListTitle")
        For i As Integer = 0 To table.Rows.Count - 1
            table.Rows(i).Item("ListTitle") = StringUtils.SortTokens(table.Rows(i).Item("Title").ToString)
            table.Rows(i).Item("SortedTitle") = StringUtils.SortTokens(table.Rows(i).Item("SortedTitle").ToString)
        Next
    End Sub

    Public Sub FillDataTable_Movieset(ByRef table As DataTable, ByVal command As String)
        table = New DataTable
        FillDataTable(table, command)

        'Table manipulation
        'add column "listTitle" and generate the value
        table.Columns.Add("ListTitle")
        For i As Integer = 0 To table.Rows.Count - 1
            table.Rows(i).Item("ListTitle") = StringUtils.SortTokens(table.Rows(i).Item("Title").ToString)
        Next
    End Sub

    Public Sub FillDataTable_TVEpisode(ByRef table As DataTable, ByVal command As String)
        table = New DataTable
        FillDataTable(table, command)
    End Sub

    Public Sub FillDataTable_TVSeason(ByRef table As DataTable, ByVal command As String)
        table = New DataTable
        FillDataTable(table, command)

        'Table manipulation
        'fill column "title" with generic season title if no season title has been specified
        For i = 0 To table.Rows.Count - 1
            If String.IsNullOrEmpty(table.Rows(i).Item("Title").ToString) Then
                table.Rows(i).Item("Title") = StringUtils.FormatSeasonTitle(CInt(table.Rows(i).Item("Season")))
            End If
        Next
    End Sub

    Public Sub FillDataTable_TVShow(ByRef table As DataTable, ByVal command As String)
        table = New DataTable
        FillDataTable(table, command)

        'Table manipulation
        'add column "listTitle" and generate the value
        table.Columns.Add("ListTitle")
        For i As Integer = 0 To table.Rows.Count - 1
            table.Rows(i).Item("ListTitle") = StringUtils.SortTokens(table.Rows(i).Item("Title").ToString)
            'table.Rows(i).Item(Helpers.GetColumnName(ColumnName.SortedTitle)) = StringUtils.SortTokens_TV(table.Rows(i).Item(Helpers.GetColumnName(ColumnName.SortedTitle)).ToString)
        Next
    End Sub

    Public Function GetAll_Certifications() As String()
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

    Public Function GetAll_Countries() As String()
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

    Public Function GetAll_Editions_Movie() As String()
        Dim nList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT DISTINCT edition FROM movie WHERE edition <> '' ORDER BY edition;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("edition").ToString)
                End While
            End Using
        End Using
        Return nList.ToArray
    End Function
    ''' <summary>
    ''' Get a list of excluded directories
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAll_ExcludedPaths() As List(Of String)
        Dim nList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT Dirname FROM ExcludeDir;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("Dirname").ToString)
                End While
            End Using
        End Using
        Return nList
    End Function

    Public Function GetAll_MoviesetDetails() As List(Of MediaContainers.MoviesetDetails)
        Dim nList As New List(Of MediaContainers.MoviesetDetails)
        For Each tSet In LoadAll_Moviesets()
            nList.Add(New MediaContainers.MoviesetDetails With {
                      .ID = tSet.ID,
                      .Plot = tSet.MainDetails.Plot,
                      .Title = tSet.MainDetails.Title,
                      .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.Movieset) With {.TMDbId = tSet.MainDetails.UniqueIDs.TMDbId}
                      })
        Next
        Return nList
    End Function

    Public Function GetAll_Paths_Movie() As List(Of String)
        Dim nList As New List(Of String)
        Dim mPath As String = String.Empty
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT MoviePath FROM movie;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    mPath = SQLreader("MoviePath").ToString.ToLower
                    If Master.eSettings.FileSystemNoStackExts.Contains(Path.GetExtension(mPath)) Then
                        nList.Add(mPath)
                    Else
                        nList.Add(FileUtils.Common.RemoveStackingMarkers(mPath))
                    End If
                End While
            End Using
        End Using
        Return nList
    End Function

    Public Function GetAll_Paths_TVEpisode() As List(Of String)
        Dim nList As New List(Of String)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT strFilename FROM files;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("strFilename").ToString.ToLower)
                End While
            End Using
        End Using
        Return nList
    End Function

    Public Function GetAll_Paths_TVShow() As Hashtable
        Dim nList As New Hashtable
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT idShow, TVShowPath FROM tvshow;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nList.Add(SQLreader("TVShowPath").ToString.ToLower, SQLreader("idShow"))
                End While
            End Using
        End Using
        Return nList
    End Function

    Public Function GetAll_SourceNames_Movie() As String()
        Dim nList As New List(Of String)
        For Each nSource In Master.DB.LoadAll_Sources_Movie
            nList.Add(nSource.Name)
        Next
        Return nList.ToArray
    End Function

    Public Function GetAll_SourceNames_TVShow() As String()
        Dim nList As New List(Of String)
        For Each nSource In Master.DB.LoadAll_Sources_TVShow
            nList.Add(nSource.Name)
        Next
        Return nList.ToArray
    End Function

    Public Function GetAll_Status() As String()
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

    Public Function GetAll_Studios() As String()
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

    Public Function GetAll_TVShowTitles() As List(Of String)
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

    Public Function GetAll_Tags() As String()
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

    Public Function GetAll_VideoSources_Movie() As String()
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

    Public Function GetAll_VideoSources_TVEpisode() As String()
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

    Public Function Get_ArtForItem(ByVal mediaId As Long, ByVal mediaType As String, ByVal artType As String) As String
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT url FROM art WHERE media_id={0} AND media_type='{1}' AND type='{2}'", mediaId, mediaType, artType)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Return SQLreader("url").ToString
                    Exit While
                End While
            End Using
        End Using
        Return String.Empty
    End Function

    Public Function Get_EpisodeSorting_TVShow(ByVal showID As Long) As Enums.EpisodeSorting
        Dim sEpisodeSorting As Enums.EpisodeSorting = Enums.EpisodeSorting.Episode
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT EpisodeSorting FROM tvshow WHERE idShow = ", showID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    sEpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("EpisodeSorting")), Enums.EpisodeSorting)
                End While
            End Using
        End Using
        Return sEpisodeSorting
    End Function

    Public Function Get_TVSeasonIdByEpisode(ByVal dbElement As DBElement) As Long
        Dim sID As Long = -1
        If dbElement.MainDetails IsNot Nothing Then
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0} AND Season = {1};", dbElement.ShowID, dbElement.MainDetails.Season)
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

    Public Function Get_TVSeasonIdByShowIdAndSeasonNumber(ByVal showId As Long, ByVal seasonNumber As Integer) As Long
        Dim sID As Long = -1
        If showId > -1 AndAlso seasonNumber > -1 Then
            Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                SQLcommand.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0} AND Season = {1};", showId, seasonNumber)
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

    Private Function Get_Moviesets_Movie(ByVal movieId As Long) As List(Of MediaContainers.MoviesetDetails)
        Dim lstResults As New List(Of MediaContainers.MoviesetDetails)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Concat("SELECT A.idMovie, A.idSet, A.iOrder, B.idSet, B.Plot, B.Title, B.TMDBColID FROM setlinkmovie ",
                                                   "AS A INNER JOIN sets AS B ON (A.idSet = B.idSet) WHERE A.idMovie = ", movieId, ";")
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    Dim nSet As New MediaContainers.MoviesetDetails
                    If Not DBNull.Value.Equals(SQLreader("idSet")) Then nSet.ID = Convert.ToInt64(SQLreader("idSet"))
                    If Not DBNull.Value.Equals(SQLreader("iOrder")) Then nSet.Order = CInt(SQLreader("iOrder"))
                    If Not DBNull.Value.Equals(SQLreader("Plot")) Then nSet.Plot = SQLreader("plot").ToString
                    If Not DBNull.Value.Equals(SQLreader("Title")) Then nSet.Title = SQLreader("Title").ToString
                    lstResults.Add(nSet)
                End While
            End Using
        End Using
        Return lstResults
    End Function

    Private Function Get_RatingsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As MediaContainers.RatingContainer
        Dim nResult As New MediaContainers.RatingContainer(contentType)
        Dim mediaType As String = Convert_ContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT * FROM rating WHERE media_id={0} AND media_type='{1}';", idMedia, mediaType)
                Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    While sqlReader.Read
                        nResult.Items.Add(New MediaContainers.RatingDetails With {
                                          .ID = CLng(sqlReader("idRating")),
                                          .IsDefault = CBool(sqlReader("isDefault")),
                                          .Max = CInt(sqlReader("rating_max")),
                                          .Type = sqlReader("rating_type").ToString,
                                          .Value = CDbl(sqlReader("rating")),
                                          .Votes = CInt(sqlReader("votes"))
                                          })
                    End While
                End Using
            End Using
        End If
        Return nResult
    End Function

    Public Function Get_TVShowPath(ByVal showId As Long) As String
        Dim ShowPath As String = String.Empty

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT TVShowPath FROM tvshow WHERE idShow = ", showId, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    ShowPath = SQLreader("TVShowPath").ToString
                End If
            End Using
        End Using

        Return ShowPath
    End Function

    Private Function Get_UniqueIDsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType) As MediaContainers.UniqueidContainer
        Dim nResult As New MediaContainers.UniqueidContainer(contentType)
        Dim mediaType As String = Convert_ContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("SELECT * FROM uniqueid WHERE media_id={0} AND media_type='{1}' ORDER BY isDefault=0", idMedia, mediaType)
                Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                    While SQLreader.Read
                        nResult.Items.Add(New MediaContainers.Uniqueid With {
                                               .ID = CLng(SQLreader("idUniqueID")),
                                               .IsDefault = CBool(SQLreader("isDefault")),
                                               .Type = SQLreader("type").ToString,
                                               .Value = SQLreader("value").ToString
                                               })
                    End While
                End Using
            End Using
        End If
        Return nResult
    End Function

    Public Sub LoadAll_Certifications()
        For Each aElement As String In GetAll_Certifications()
            Dim nMapping As SimpleMapping = APIXML.CertificationMappings.Mappings.FirstOrDefault(Function(f) f.Input = aElement)
            If nMapping Is Nothing Then
                'add a new mapping if aElement is not in the MappingTable
                APIXML.CertificationMappings.Mappings.Add(New SimpleMapping With {.Input = aElement, .MappedTo = aElement})
            End If
        Next
    End Sub

    Public Sub LoadAll_Countries()
        For Each aElement As String In GetAll_Countries()
            Dim nMapping As SimpleMapping = APIXML.CountryMappings.Mappings.FirstOrDefault(Function(f) f.Input = aElement)
            If nMapping Is Nothing Then
                'add a new mapping if aElement is not in the MappingTable
                APIXML.CountryMappings.Mappings.Add(New SimpleMapping With {.Input = aElement, .MappedTo = aElement})
            End If
        Next
    End Sub

    Public Sub LoadAll_Genres()
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
            Dim gMapping As GenreMapping = APIXML.GenreMappings.Mappings.FirstOrDefault(Function(f) f.SearchString = aGenre)
            If gMapping Is Nothing Then
                'check if the tGenre is already existing in Gernes list
                Dim gProperty As GenreProperty = APIXML.GenreMappings.Genres.FirstOrDefault(Function(f) f.Name = aGenre)
                If gProperty Is Nothing Then
                    APIXML.GenreMappings.Genres.Add(New GenreProperty With {.isNew = False, .Name = aGenre})
                End If
                'add a new mapping if tGenre is not in the MappingTable
                APIXML.GenreMappings.Mappings.Add(New GenreMapping With {.isNew = False, .MappedTo = New List(Of String) From {aGenre}, .SearchString = aGenre})
            End If
        Next
    End Sub

    Public Function LoadAll_Movies() As List(Of DBElement)
        Dim lstDBELement As New List(Of DBElement)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT idMovie FROM movie ORDER BY Title;"
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

    Public Function LoadAll_Moviesets() As List(Of DBElement)
        Dim lstDBELement As New List(Of DBElement)
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idSet FROM sets ORDER BY Title;"
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
    ''' <summary>
    ''' Get all movie sources from DB
    ''' </summary>
    ''' <remarks></remarks>
    Public Function LoadAll_Sources_Movie() As List(Of DBSource)
        Dim lstSources As New List(Of DBSource)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT * FROM moviesource ORDER BY strName;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Dim msource As New DBSource With {
                        .Exclude = Convert.ToBoolean(SQLreader("bExclude")),
                        .GetYear = Convert.ToBoolean(SQLreader("bGetYear")),
                        .ID = Convert.ToInt64(SQLreader("idSource")),
                        .IsSingle = Convert.ToBoolean(SQLreader("bSingle")),
                        .Language = SQLreader("strLanguage").ToString,
                        .LastScan = SQLreader("strLastScan").ToString,
                        .Name = SQLreader("strName").ToString,
                        .Path = SQLreader("strPath").ToString,
                        .ScanRecursive = Convert.ToBoolean(SQLreader("bRecursive")),
                        .UseFolderName = Convert.ToBoolean(SQLreader("bFoldername"))
                    }
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
    Public Function LoadAll_Sources_TVShow() As List(Of DBSource)
        Dim lstSources As New List(Of DBSource)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT * FROM tvshowsource ORDER BY strName;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Dim tvsource As New DBSource With {
                        .EpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("iEpisodeSorting")), Enums.EpisodeSorting),
                        .Exclude = Convert.ToBoolean(SQLreader("bExclude")),
                        .ID = Convert.ToInt64(SQLreader("idSource")),
                        .IsSingle = Convert.ToBoolean(SQLreader("bSingle")),
                        .Language = SQLreader("strLanguage").ToString,
                        .LastScan = SQLreader("strLastScan").ToString,
                        .Name = SQLreader("strName").ToString,
                        .EpisodeOrdering = DirectCast(Convert.ToInt32(SQLreader("iOrdering")), Enums.EpisodeOrdering),
                        .Path = SQLreader("strPath").ToString
                    }
                    lstSources.Add(tvsource)
                End While
            End Using
        End Using
        Return lstSources
    End Function

    Public Sub LoadAll_Status()
        For Each aElement As String In GetAll_Status()
            Dim nMapping As SimpleMapping = APIXML.StatusMappings.Mappings.FirstOrDefault(Function(f) f.Input = aElement)
            If nMapping Is Nothing Then
                'add a new mapping if aElement is not in the MappingTable
                APIXML.StatusMappings.Mappings.Add(New SimpleMapping With {.Input = aElement, .MappedTo = aElement})
            End If
        Next
    End Sub

    Public Sub LoadAll_Studios()
        For Each aElement As String In GetAll_Studios()
            Dim nMapping As SimpleMapping = APIXML.StudioMappings.Mappings.FirstOrDefault(Function(f) f.Input = aElement)
            If nMapping Is Nothing Then
                'add a new mapping if aElement is not in the MappingTable
                APIXML.StudioMappings.Mappings.Add(New SimpleMapping With {.Input = aElement, .MappedTo = aElement})
            End If
        Next
    End Sub

    Public Function LoadAll_Tags() As List(Of TagContainer)
        Dim lstTags As New List(Of TagContainer)
        'Load all tags
        Using SQLcommand_Tags As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_Tags.CommandText = "SELECT idTag, strTag FROM tag ORDER BY strTag;"
            Using SQLreader_Tags As SQLiteDataReader = SQLcommand_Tags.ExecuteReader()
                If SQLreader_Tags.HasRows Then
                    While SQLreader_Tags.Read()
                        Dim nTagContainer As New TagContainer With {
                            .Id = Convert.ToInt64(SQLreader_Tags("idTag")),
                            .Name = SQLreader_Tags("strTag").ToString
                        }
                        'search for movies
                        FillDataTable(nTagContainer.Movies, String.Format("SELECT movie.idMovie AS id, movie.Title FROM taglinks LEFT OUTER JOIN movie ON (taglinks.idMedia = movie.idMovie) WHERE taglinks.media_type = ""movie"" and taglinks.idTag = {0};", SQLreader_Tags("idTag").ToString))
                        'search for tv shows
                        FillDataTable(nTagContainer.TVShows, String.Format("SELECT tvshow.idShow AS id, tvshow.Title FROM taglinks LEFT OUTER JOIN tvshow ON (taglinks.idMedia = tvshow.idShow) WHERE taglinks.media_type = ""tvshow"" and taglinks.idTag = {0};", SQLreader_Tags("idTag").ToString))
                        'add container to the list
                        lstTags.Add(nTagContainer)
                    End While
                End If
            End Using
        End Using
        Return lstTags
    End Function

    Public Function LoadAll_TVEpisodes(ByVal showID As Long, ByVal withShow As Boolean, Optional ByVal onlySeasonNumber As Integer = -1, Optional ByVal withMissingEpisodes As Boolean = False) As List(Of DBElement)
        If showID < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & showID)

        Dim _TVEpisodesList As New List(Of DBElement)

        Using SQLCount As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            If onlySeasonNumber = -1 Then
                SQLCount.CommandText = String.Concat("SELECT COUNT(idEpisode) AS eCount FROM episode WHERE idShow = ", showID, If(withMissingEpisodes, ";", " AND NOT idFile = -1;"))
            Else
                SQLCount.CommandText = String.Concat("SELECT COUNT(idEpisode) AS eCount FROM episode WHERE idShow = ", showID, " AND Season = ", onlySeasonNumber, If(withMissingEpisodes, ";", " AND NOT idFile = -1;"))
            End If
            Using SQLRCount As SQLiteDataReader = SQLCount.ExecuteReader
                If SQLRCount.HasRows Then
                    SQLRCount.Read()
                    If Convert.ToInt32(SQLRCount("eCount")) > 0 Then
                        Using SQLCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            If onlySeasonNumber = -1 Then
                                SQLCommand.CommandText = String.Concat("SELECT * FROM episode WHERE idShow = ", showID, If(withMissingEpisodes, ";", " AND NOT idFile = -1;"))
                            Else
                                SQLCommand.CommandText = String.Concat("SELECT * FROM episode WHERE idShow = ", showID, " AND Season = ", onlySeasonNumber, If(withMissingEpisodes, ";", " AND NOT idFile = -1;"))
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

    Public Function LoadAll_TVEpisodes_ByFileId(ByVal fileId As Long, ByVal withShow As Boolean) As List(Of DBElement)
        If fileId < 0 Then Throw New ArgumentOutOfRangeException("idFile", "Value must be >= 0, was given: " & fileId)

        Dim _TVEpisodesList As New List(Of DBElement)

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT idEpisode FROM episode WHERE idFile = {0};", fileId)
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

    Public Function LoadAll_TVSeasons(ByVal showId As Long) As List(Of DBElement)
        If showId < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & showId)

        Dim _TVSeasonsList As New List(Of DBElement)

        Using SQLCount As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLCount.CommandText = String.Concat("SELECT COUNT(idSeason) AS eCount FROM seasons WHERE idShow = ", showId, ";")
            Using SQLRCount As SQLiteDataReader = SQLCount.ExecuteReader
                If SQLRCount.HasRows Then
                    SQLRCount.Read()
                    If Convert.ToInt32(SQLRCount("eCount")) > 0 Then
                        Using SQLCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand.CommandText = String.Concat("SELECT * FROM seasons WHERE idShow = ", showId, ";")
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

    Public Function LoadAll_TVShows(ByVal withseasons As Boolean, ByVal withEpisodes As Boolean, Optional ByVal withMissingEpisodes As Boolean = False) As List(Of DBElement)
        Dim lstDBELement As New List(Of DBElement)
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT idShow FROM tvshow;"
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        lstDBELement.Add(Master.DB.Load_TVShow(Convert.ToInt64(SQLreader("idShow")), withseasons, withEpisodes, withMissingEpisodes))
                    End While
                End If
            End Using
        End Using
        Return lstDBELement
    End Function
    ''' <summary>
    ''' Load all the information for a movie.
    ''' </summary>
    ''' <param name="id">ID of the movie to load, as stored in the database</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_Movie(ByVal id As Long) As DBElement
        Dim nDbElement As New DBElement(Enums.ContentType.Movie) With {
            .ID = id,
            .MainDetails = New MediaContainers.MainDetails
        }
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT * FROM movie WHERE idMovie = {0};", nDbElement.ID)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("MoviePath")) Then nDbElement.Filename = SQLreader("MoviePath").ToString
                    nDbElement.IsSingle = Convert.ToBoolean(SQLreader("Type"))
                    If Not DBNull.Value.Equals(SQLreader("TrailerPath")) Then nDbElement.Trailer.LocalFilePath = SQLreader("TrailerPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("NfoPath")) Then nDbElement.NfoPath = SQLreader("NfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("EThumbsPath")) Then nDbElement.ExtrathumbsPath = SQLreader("EThumbsPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("EFanartsPath")) Then nDbElement.ExtrafanartsPath = SQLreader("EFanartsPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("ThemePath")) Then nDbElement.Theme.LocalFilePath = SQLreader("ThemePath").ToString

                    nDbElement.Source = Load_Source_Movie(Convert.ToInt64(SQLreader("idSource")))

                    nDbElement.IsLocked = Convert.ToBoolean(SQLreader("Lock"))
                    nDbElement.IsMarked = Convert.ToBoolean(SQLreader("Mark"))
                    nDbElement.IsNew = Convert.ToBoolean(SQLreader("New"))
                    nDbElement.OutOfTolerance = Convert.ToBoolean(SQLreader("OutOfTolerance"))
                    nDbElement.IsMarkCustom1 = Convert.ToBoolean(SQLreader("MarkCustom1"))
                    nDbElement.IsMarkCustom2 = Convert.ToBoolean(SQLreader("MarkCustom2"))
                    nDbElement.IsMarkCustom3 = Convert.ToBoolean(SQLreader("MarkCustom3"))
                    nDbElement.IsMarkCustom4 = Convert.ToBoolean(SQLreader("MarkCustom4"))
                    If Not DBNull.Value.Equals(SQLreader("VideoSource")) Then nDbElement.VideoSource = SQLreader("VideoSource").ToString
                    If Not DBNull.Value.Equals(SQLreader("Language")) Then nDbElement.Language = SQLreader("Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("edition")) Then nDbElement.Edition = SQLreader("edition").ToString

                    With nDbElement.MainDetails
                        If Not DBNull.Value.Equals(SQLreader("DateAdded")) Then .DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("DateAdded"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("DateModified")) Then .DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("DateModified"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("Title")) Then .Title = SQLreader("Title").ToString
                        If Not DBNull.Value.Equals(SQLreader("OriginalTitle")) Then .OriginalTitle = SQLreader("OriginalTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("SortTitle")) Then .SortTitle = SQLreader("SortTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("MPAA")) Then .MPAA = SQLreader("MPAA").ToString
                        If Not DBNull.Value.Equals(SQLreader("Top250")) Then .Top250 = Convert.ToInt32(SQLreader("Top250"))
                        If Not DBNull.Value.Equals(SQLreader("Outline")) Then .Outline = SQLreader("Outline").ToString
                        If Not DBNull.Value.Equals(SQLreader("Plot")) Then .Plot = SQLreader("Plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("Tagline")) Then .Tagline = SQLreader("Tagline").ToString
                        If Not DBNull.Value.Equals(SQLreader("Trailer")) Then .Trailer = SQLreader("Trailer").ToString
                        If Not DBNull.Value.Equals(SQLreader("Certification")) Then .AddCertificationsFromString(SQLreader("Certification").ToString)
                        If Not DBNull.Value.Equals(SQLreader("Runtime")) Then .Runtime = SQLreader("Runtime").ToString
                        If Not DBNull.Value.Equals(SQLreader("premiered")) Then .Premiered = SQLreader("premiered").ToString
                        If Not DBNull.Value.Equals(SQLreader("PlayCount")) Then .PlayCount = Convert.ToInt32(SQLreader("PlayCount"))
                        If Not DBNull.Value.Equals(SQLreader("VideoSource")) Then .VideoSource = SQLreader("VideoSource").ToString
                        If Not DBNull.Value.Equals(SQLreader("iLastPlayed")) Then .LastPlayed = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("iLastPlayed"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("Language")) Then .Language = SQLreader("Language").ToString
                        If Not DBNull.Value.Equals(SQLreader("iUserRating")) Then .UserRating = Convert.ToInt32(SQLreader("iUserRating"))
                        If Not DBNull.Value.Equals(SQLreader("userNote")) Then .UserNote = SQLreader("userNote").ToString
                        If Not DBNull.Value.Equals(SQLreader("edition")) Then .Edition = SQLreader("edition").ToString
                        If Not DBNull.Value.Equals(SQLreader("year")) Then .Year = SQLreader("year").ToString
                    End With
                End If
            End Using
        End Using

        'Actors
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT A.strRole, B.idActor, B.strActor, B.strThumb, C.url FROM actorlinkmovie AS A ",
                        "INNER JOIN actors AS B ON (A.idActor = B.idActor) ",
                        "LEFT OUTER JOIN art AS C ON (B.idActor = C.media_id AND C.media_type = 'actor' AND C.type = 'thumb') ",
                        "WHERE A.idMovie = ", nDbElement.ID, " ",
                        "ORDER BY A.iOrder;")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim person As MediaContainers.Person
                While SQLreader.Read
                    person = New MediaContainers.Person With {
                        .ID = Convert.ToInt64(SQLreader("idActor")),
                        .Name = SQLreader("strActor").ToString,
                        .Role = SQLreader("strRole").ToString,
                        .LocalFilePath = SQLreader("url").ToString,
                        .URLOriginal = SQLreader("strThumb").ToString
                    }
                    nDbElement.MainDetails.Actors.Add(person)
                End While
            End Using
        End Using

        'Countries
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strCountry FROM countrylinkmovie ",
                                                   "AS A INNER JOIN country AS B ON (A.idCountry = B.idCountry) WHERE A.idMovie = ", nDbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strCountry")) Then nDbElement.MainDetails.Countries.Add(SQLreader("strCountry").ToString)
                End While
            End Using
        End Using

        'Credits
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strActor FROM writerlinkmovie ",
                                                   "AS A INNER JOIN actors AS B ON (A.idWriter = B.idActor) WHERE A.idMovie = ", nDbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strActor")) Then nDbElement.MainDetails.Credits.Add(SQLreader("strActor").ToString)
                End While
            End Using
        End Using

        'Directors
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strActor FROM directorlinkmovie ",
                                                   "AS A INNER JOIN actors AS B ON (A.idDirector = B.idActor) WHERE A.idMovie = ", nDbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strActor")) Then nDbElement.MainDetails.Directors.Add(SQLreader("strActor").ToString)
                End While
            End Using
        End Using

        'Genres
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strGenre FROM genrelinkmovie ",
                                                   "AS A INNER JOIN genre AS B ON (A.idGenre = B.idGenre) WHERE A.idMovie = ", nDbElement.ID, " ORDER BY sorting;")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strGenre")) Then nDbElement.MainDetails.Genres.Add(SQLreader("strGenre").ToString)
                End While
            End Using
        End Using

        'Moviesets
        nDbElement.MainDetails.Sets.AddRange(Get_Moviesets_Movie(nDbElement.ID))

        'Ratings
        nDbElement.MainDetails.Ratings = Get_RatingsForItem(nDbElement.ID, nDbElement.ContentType)

        'UniqueIDs
        nDbElement.MainDetails.UniqueIDs = Get_UniqueIDsForItem(nDbElement.ID, nDbElement.ContentType)

        'Video streams
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM MoviesVStreams WHERE MovieID = ", nDbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim video As MediaContainers.Video
                While SQLreader.Read
                    video = New MediaContainers.Video
                    If Not DBNull.Value.Equals(SQLreader("Video_Width")) Then video.Width = Convert.ToInt32(SQLreader("Video_Width"))
                    If Not DBNull.Value.Equals(SQLreader("Video_Height")) Then video.Height = Convert.ToInt32(SQLreader("Video_Height"))
                    If Not DBNull.Value.Equals(SQLreader("Video_Codec")) Then video.Codec = SQLreader("Video_Codec").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_Duration")) Then video.Duration = Convert.ToInt32(SQLreader("Video_Duration"))
                    If Not DBNull.Value.Equals(SQLreader("Video_ScanType")) Then video.Scantype = SQLreader("Video_ScanType").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_AspectDisplayRatio")) Then video.Aspect = Convert.ToDouble(SQLreader("Video_AspectDisplayRatio"))
                    If Not DBNull.Value.Equals(SQLreader("Video_Language")) Then video.Language = SQLreader("Video_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_LongLanguage")) Then video.LongLanguage = SQLreader("Video_LongLanguage").ToString
                    'If Not DBNull.Value.Equals(SQLreader("Video_Bitrate")) Then video.Bitrate = Convert.ToInt32(SQLreader("Video_Bitrate"))
                    'If Not DBNull.Value.Equals(SQLreader("Video_MultiViewCount")) Then video.MultiViewCount = Convert.ToInt32(SQLreader("Video_MultiViewCount"))
                    If Not DBNull.Value.Equals(SQLreader("Video_FileSize")) Then video.Filesize = Convert.ToInt64(SQLreader("Video_FileSize"))
                    If Not DBNull.Value.Equals(SQLreader("Video_MultiViewLayout")) Then video.MultiViewLayout = SQLreader("Video_MultiViewLayout").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_StereoMode")) Then video.StereoMode = SQLreader("Video_StereoMode").ToString
                    nDbElement.MainDetails.FileInfo.StreamDetails.Video.Add(video)
                End While
            End Using
        End Using

        'Audio streams
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM MoviesAStreams WHERE MovieID = ", nDbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim audio As MediaContainers.Audio
                While SQLreader.Read
                    audio = New MediaContainers.Audio
                    If Not DBNull.Value.Equals(SQLreader("Audio_Language")) Then audio.Language = SQLreader("Audio_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_LongLanguage")) Then audio.LongLanguage = SQLreader("Audio_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Codec")) Then audio.Codec = SQLreader("Audio_Codec").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Channel")) Then audio.Channels = Convert.ToInt32(SQLreader("Audio_Channel"))
                    'If Not DBNull.Value.Equals(SQLreader("Audio_Bitrate")) Then audio.Bitrate = Convert.ToInt32(SQLreader("Audio_Bitrate"))
                    nDbElement.MainDetails.FileInfo.StreamDetails.Audio.Add(audio)
                End While
            End Using
        End Using

        'embedded subtitles
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM MoviesSubs WHERE MovieID = ", nDbElement.ID, " AND NOT Subs_Type = 'External';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaContainers.Subtitle
                While SQLreader.Read
                    subtitle = New MediaContainers.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.Type = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.Path = SQLreader("Subs_Path").ToString
                    subtitle.Forced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    nDbElement.MainDetails.FileInfo.StreamDetails.Subtitle.Add(subtitle)
                End While
            End Using
        End Using

        'external subtitles
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM MoviesSubs WHERE MovieID = ", nDbElement.ID, " AND Subs_Type = 'External';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaContainers.Subtitle
                While SQLreader.Read
                    subtitle = New MediaContainers.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.Type = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.Path = SQLreader("Subs_Path").ToString
                    subtitle.Forced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    nDbElement.Subtitles.Add(subtitle)
                End While
            End Using
        End Using

        'Studios
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strStudio FROM studiolinkmovie ",
                                                   "AS A INNER JOIN studio AS B ON (A.idStudio = B.idStudio) WHERE A.idMovie = ", nDbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strStudio")) Then nDbElement.MainDetails.Studios.Add(SQLreader("strStudio").ToString)
                End While
            End Using
        End Using

        'Tags
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strTag FROM taglinks ",
                                                   "AS A INNER JOIN tag AS B ON (A.idTag = B.idTag) WHERE A.idMedia = ", nDbElement.ID, " AND A.media_type = 'movie' ORDER BY sorting;")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strTag")) Then nDbElement.MainDetails.Tags.Add(SQLreader("strTag").ToString)
                End While
            End Using
        End Using

        'ImagesContainer
        nDbElement.ImagesContainer.Banner.LocalFilePath = Get_ArtForItem(nDbElement.ID, "movie", "banner")
        nDbElement.ImagesContainer.ClearArt.LocalFilePath = Get_ArtForItem(nDbElement.ID, "movie", "clearart")
        nDbElement.ImagesContainer.ClearLogo.LocalFilePath = Get_ArtForItem(nDbElement.ID, "movie", "clearlogo")
        nDbElement.ImagesContainer.DiscArt.LocalFilePath = Get_ArtForItem(nDbElement.ID, "movie", "discart")
        nDbElement.ImagesContainer.Fanart.LocalFilePath = Get_ArtForItem(nDbElement.ID, "movie", "fanart")
        nDbElement.ImagesContainer.Keyart.LocalFilePath = Get_ArtForItem(nDbElement.ID, "movie", "keyart")
        nDbElement.ImagesContainer.Landscape.LocalFilePath = Get_ArtForItem(nDbElement.ID, "movie", "landscape")
        nDbElement.ImagesContainer.Poster.LocalFilePath = Get_ArtForItem(nDbElement.ID, "movie", "poster")
        If Not String.IsNullOrEmpty(nDbElement.ExtrafanartsPath) AndAlso Directory.Exists(nDbElement.ExtrafanartsPath) Then
            For Each ePath As String In Directory.GetFiles(nDbElement.ExtrafanartsPath, "*.jpg")
                nDbElement.ImagesContainer.Extrafanarts.Add(New MediaContainers.Image With {.LocalFilePath = ePath})
            Next
        End If
        If Not String.IsNullOrEmpty(nDbElement.ExtrathumbsPath) AndAlso Directory.Exists(nDbElement.ExtrathumbsPath) Then
            Dim iIndex As Integer = 0
            For Each ePath As String In Directory.GetFiles(nDbElement.ExtrathumbsPath, "thumb*.jpg")
                nDbElement.ImagesContainer.Extrathumbs.Add(New MediaContainers.Image With {.Index = iIndex, .LocalFilePath = ePath})
                iIndex += 1
            Next
        End If

        'Check if the file is available and ready to edit
        nDbElement.IsOnline = File.Exists(nDbElement.Filename)

        Return nDbElement
    End Function

    ''' <summary>
    ''' Load all the information for a movie (by movie path)
    ''' </summary>
    ''' <param name="fullPath">Full path to the movie file</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_Movie(ByVal fullPath As String) As DBElement
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            ' One more Query Better then re-write all function again
            SQLcommand.CommandText = String.Concat("SELECT idMovie FROM movie WHERE MoviePath = ", fullPath, ";")
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
    ''' <param name="id">ID of the movieset to load, as stored in the database</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_Movieset(ByVal id As Long) As DBElement
        Dim nDbElement As New DBElement(Enums.ContentType.Movieset) With {
            .ID = id,
            .MainDetails = New MediaContainers.MainDetails
        }
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM sets WHERE idSet = ", id, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("NfoPath")) Then nDbElement.NfoPath = SQLreader("NfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("Language")) Then nDbElement.Language = SQLreader("Language").ToString

                    nDbElement.IsLocked = Convert.ToBoolean(SQLreader("Lock"))
                    nDbElement.IsMarked = Convert.ToBoolean(SQLreader("Mark"))
                    nDbElement.IsNew = Convert.ToBoolean(SQLreader("New"))
                    nDbElement.SortMethod = DirectCast(Convert.ToInt32(SQLreader("SortMethod")), Enums.SortMethod_MovieSet)

                    With nDbElement.MainDetails
                        If Not DBNull.Value.Equals(SQLreader("Plot")) Then .Plot = SQLreader("Plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("Title")) Then .Title = SQLreader("Title").ToString
                        If Not DBNull.Value.Equals(SQLreader("Language")) Then .Language = SQLreader("Language").ToString
                        .Title_Old = .Title
                    End With
                End If
            End Using
        End Using

        'Movies in Set
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            If Not Master.eSettings.Movie.InformationSettings.Collection.SaveYAMJCompatible Then
                If nDbElement.SortMethod = Enums.SortMethod_MovieSet.Year Then
                    SQLcommand.CommandText = String.Concat("SELECT setlinkmovie.idMovie, setlinkmovie.iOrder FROM setlinkmovie INNER JOIN movie ON (setlinkmovie.idMovie = movie.idMovie) ",
                                                           "WHERE idSet = ", nDbElement.ID, " ORDER BY movie.Year;")
                ElseIf nDbElement.SortMethod = Enums.SortMethod_MovieSet.Title Then
                    SQLcommand.CommandText = String.Concat("SELECT setlinkmovie.idMovie, setlinkmovie.iOrder FROM setlinkmovie INNER JOIN movielist ON (setlinkmovie.idMovie = movielist.idMovie) ",
                                                           "WHERE idSet = ", nDbElement.ID, " ORDER BY movielist.SortedTitle COLLATE NOCASE;")
                End If
            Else
                SQLcommand.CommandText = String.Concat("SELECT setlinkmovie.idMovie, setlinkmovie.iOrder FROM setlinkmovie ",
                                                       "WHERE idSet = ", nDbElement.ID, " ORDER BY iOrder;")
            End If
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim i As Integer = 0
                While SQLreader.Read
                    nDbElement.MoviesInSet.Add(New MediaContainers.MovieInSet With {
                                                .DBMovie = Load_Movie(Convert.ToInt64(SQLreader("idMovie"))),
                                                .Order = i})
                    i += 1
                End While
            End Using
        End Using

        'ImagesContainer
        nDbElement.ImagesContainer.Banner.LocalFilePath = Get_ArtForItem(nDbElement.ID, "set", "banner")
        nDbElement.ImagesContainer.ClearArt.LocalFilePath = Get_ArtForItem(nDbElement.ID, "set", "clearart")
        nDbElement.ImagesContainer.ClearLogo.LocalFilePath = Get_ArtForItem(nDbElement.ID, "set", "clearlogo")
        nDbElement.ImagesContainer.DiscArt.LocalFilePath = Get_ArtForItem(nDbElement.ID, "set", "discart")
        nDbElement.ImagesContainer.Fanart.LocalFilePath = Get_ArtForItem(nDbElement.ID, "set", "fanart")
        nDbElement.ImagesContainer.Keyart.LocalFilePath = Get_ArtForItem(nDbElement.ID, "set", "keyart")
        nDbElement.ImagesContainer.Landscape.LocalFilePath = Get_ArtForItem(nDbElement.ID, "set", "landscape")
        nDbElement.ImagesContainer.Poster.LocalFilePath = Get_ArtForItem(nDbElement.ID, "set", "poster")

        'UniqueIDs
        nDbElement.MainDetails.UniqueIDs = Get_UniqueIDsForItem(nDbElement.ID, nDbElement.ContentType)

        'Check if the file is available and ready to edit
        nDbElement.IsOnline = Not nDbElement.MoviesInSet.Where(Function(f) Not f.DBMovie.IsOnline).Count > 0

        Return nDbElement
    End Function

    Public Function Load_Source_Movie(ByVal id As Long) As DBSource
        Dim _source As New DBSource With {
            .ID = id
        }
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM moviesource WHERE idSource = ", _source.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    _source.Name = SQLreader("strName").ToString
                    _source.Path = SQLreader("strPath").ToString
                    _source.ScanRecursive = Convert.ToBoolean(SQLreader("bRecursive"))
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

    Public Function Load_Source_TVShow(ByVal id As Long) As DBSource
        Dim _source As New DBSource With {
            .ID = id
        }
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM tvshowsource WHERE idSource = ", _source.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    _source.Name = SQLreader("strName").ToString
                    _source.Path = SQLreader("strPath").ToString
                    _source.Language = SQLreader("strLanguage").ToString
                    _source.EpisodeOrdering = DirectCast(Convert.ToInt32(SQLreader("iOrdering")), Enums.EpisodeOrdering)
                    _source.Exclude = Convert.ToBoolean(SQLreader("bExclude"))
                    _source.EpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("iEpisodeSorting")), Enums.EpisodeSorting)
                    _source.LastScan = SQLreader("strLastScan").ToString
                    _source.IsSingle = Convert.ToBoolean(SQLreader("bSingle"))
                End If
            End Using
        End Using

        Return _source
    End Function
    ''' <summary>
    ''' Load all the information for a movietag.
    ''' </summary>
    ''' <param name="tagId">ID of the movietag to load, as stored in the database</param>
    ''' <returns>Database.DBElementTag object</returns>
    Public Function Load_Tag_Movie(ByVal tagId As Integer) As Structures.DBMovieTag
        Dim _tagDB As New Structures.DBMovieTag With {
            .ID = tagId
        }
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM tag WHERE idTag = ", tagId, ";")
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
    ''' <summary>
    ''' Load all the information for a TV Episode
    ''' </summary>
    ''' <param name="id">Episode ID</param>
    ''' <param name="WithShow">>If <c>True</c>, also retrieve the TV Show information</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_TVEpisode(ByVal id As Long, ByVal withShow As Boolean) As DBElement
        Dim nDbElement As New DBElement(Enums.ContentType.TVEpisode) With {
            .ID = id,
            .MainDetails = New MediaContainers.MainDetails
        }
        Dim PathID As Long = -1

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM episode WHERE idEpisode = ", id, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("NfoPath")) Then nDbElement.NfoPath = SQLreader("NfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("idShow")) Then nDbElement.ShowID = Convert.ToInt64(SQLreader("idShow"))
                    If Not DBNull.Value.Equals(SQLreader("VideoSource")) Then nDbElement.VideoSource = SQLreader("VideoSource").ToString
                    PathID = Convert.ToInt64(SQLreader("idFile"))

                    nDbElement.Source = Load_Source_TVShow(Convert.ToInt64(SQLreader("idSource")))

                    nDbElement.FilenameID = PathID
                    nDbElement.IsLocked = Convert.ToBoolean(SQLreader("Lock"))
                    nDbElement.IsMarked = Convert.ToBoolean(SQLreader("Mark"))
                    nDbElement.IsNew = Convert.ToBoolean(SQLreader("New"))
                    nDbElement.ShowID = Convert.ToInt64(SQLreader("idShow"))
                    nDbElement.ShowPath = Get_TVShowPath(Convert.ToInt64(SQLreader("idShow")))

                    With nDbElement.MainDetails
                        If Not DBNull.Value.Equals(SQLreader("Title")) Then .Title = SQLreader("Title").ToString
                        If Not DBNull.Value.Equals(SQLreader("Season")) Then .Season = Convert.ToInt32(SQLreader("Season"))
                        If Not DBNull.Value.Equals(SQLreader("Episode")) Then .Episode = Convert.ToInt32(SQLreader("Episode"))
                        If Not DBNull.Value.Equals(SQLreader("DisplaySeason")) Then .DisplaySeason = Convert.ToInt32(SQLreader("DisplaySeason"))
                        If Not DBNull.Value.Equals(SQLreader("DisplayEpisode")) Then .DisplayEpisode = Convert.ToInt32(SQLreader("DisplayEpisode"))
                        If Not DBNull.Value.Equals(SQLreader("Aired")) Then .Aired = SQLreader("Aired").ToString
                        If Not DBNull.Value.Equals(SQLreader("Plot")) Then .Plot = SQLreader("Plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("Playcount")) Then .PlayCount = Convert.ToInt32(SQLreader("Playcount"))
                        If Not DBNull.Value.Equals(SQLreader("DateAdded")) Then .DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("DateAdded"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("Runtime")) Then .Runtime = SQLreader("Runtime").ToString
                        If Not DBNull.Value.Equals(SQLreader("VideoSource")) Then .VideoSource = SQLreader("VideoSource").ToString
                        If Not DBNull.Value.Equals(SQLreader("SubEpisode")) Then .SubEpisode = Convert.ToInt32(SQLreader("SubEpisode"))
                        If Not DBNull.Value.Equals(SQLreader("iLastPlayed")) Then .LastPlayed = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("iLastPlayed"))).ToString("yyyy-MM-dd HH:mm:ss")
                        If Not DBNull.Value.Equals(SQLreader("iUserRating")) Then .UserRating = Convert.ToInt32(SQLreader("iUserRating"))
                        If Not DBNull.Value.Equals(SQLreader("userNote")) Then .UserNote = SQLreader("userNote").ToString
                    End With
                End If
            End Using
        End Using

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT strFilename FROM files WHERE idFile = ", PathID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("strFilename")) Then nDbElement.Filename = SQLreader("strFilename").ToString
                End If
            End Using
        End Using

        'Actors
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT A.strRole, B.idActor, B.strActor, B.strThumb, C.url FROM actorlinkepisode AS A ",
                                                   "INNER JOIN actors AS B ON (A.idActor = B.idActor) ",
                                                   "LEFT OUTER JOIN art AS C ON (B.idActor = C.media_id AND C.media_type = 'actor' AND C.type = 'thumb') ",
                                                   "WHERE A.idEpisode = ", nDbElement.ID, " ",
                                                   "ORDER BY A.iOrder;")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim person As MediaContainers.Person
                While SQLreader.Read
                    person = New MediaContainers.Person With {
                        .ID = Convert.ToInt64(SQLreader("idActor")),
                        .Name = SQLreader("strActor").ToString,
                        .Role = SQLreader("strRole").ToString,
                        .LocalFilePath = SQLreader("url").ToString,
                        .URLOriginal = SQLreader("strThumb").ToString
                    }
                    nDbElement.MainDetails.Actors.Add(person)
                End While
            End Using
        End Using

        'Credits
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strActor FROM writerlinkepisode ",
                                                   "AS A INNER JOIN actors AS B ON (A.idWriter = B.idActor) WHERE A.idEpisode = ", nDbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strActor")) Then nDbElement.MainDetails.Credits.Add(SQLreader("strActor").ToString)
                End While
            End Using
        End Using

        'Directors
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strActor FROM directorlinkepisode ",
                                                   "AS A INNER JOIN actors AS B ON (A.idDirector = B.idActor) WHERE A.idEpisode = ", nDbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strActor")) Then nDbElement.MainDetails.Directors.Add(SQLreader("strActor").ToString)
                End While
            End Using
        End Using

        'Guest Stars
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT A.strRole, B.idActor, B.strActor, B.strThumb, C.url FROM gueststarlinkepisode AS A ",
                                                   "INNER JOIN actors AS B ON (A.idActor = B.idActor) ",
                                                   "LEFT OUTER JOIN art AS C ON (B.idActor = C.media_id AND C.media_type = 'actor' AND C.type = 'thumb') ",
                                                   "WHERE A.idEpisode = ", nDbElement.ID, " ",
                                                   "ORDER BY A.iOrder;")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim person As MediaContainers.Person
                While SQLreader.Read
                    person = New MediaContainers.Person With {
                        .ID = Convert.ToInt64(SQLreader("idActor")),
                        .Name = SQLreader("strActor").ToString,
                        .Role = SQLreader("strRole").ToString,
                        .LocalFilePath = SQLreader("url").ToString,
                        .URLOriginal = SQLreader("strThumb").ToString
                    }
                    nDbElement.MainDetails.GuestStars.Add(person)
                End While
            End Using
        End Using

        'Ratings
        nDbElement.MainDetails.Ratings = Get_RatingsForItem(nDbElement.ID, nDbElement.ContentType)

        'UniqueIDs
        nDbElement.MainDetails.UniqueIDs = Get_UniqueIDsForItem(nDbElement.ID, nDbElement.ContentType)

        'Video Streams
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM TVVStreams WHERE TVEpID = ", nDbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim video As MediaContainers.Video
                While SQLreader.Read
                    video = New MediaContainers.Video
                    If Not DBNull.Value.Equals(SQLreader("Video_Width")) Then video.Width = Convert.ToInt32(SQLreader("Video_Width"))
                    If Not DBNull.Value.Equals(SQLreader("Video_Height")) Then video.Height = Convert.ToInt32(SQLreader("Video_Height"))
                    If Not DBNull.Value.Equals(SQLreader("Video_Codec")) Then video.Codec = SQLreader("Video_Codec").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_Duration")) Then video.Duration = Convert.ToInt32(SQLreader("Video_Duration"))
                    If Not DBNull.Value.Equals(SQLreader("Video_ScanType")) Then video.Scantype = SQLreader("Video_ScanType").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_AspectDisplayRatio")) Then video.Aspect = Convert.ToDouble(SQLreader("Video_AspectDisplayRatio"))
                    If Not DBNull.Value.Equals(SQLreader("Video_Language")) Then video.Language = SQLreader("Video_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_LongLanguage")) Then video.LongLanguage = SQLreader("Video_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_Bitrate")) Then video.Bitrate = Convert.ToInt32(SQLreader("Video_Bitrate"))
                    If Not DBNull.Value.Equals(SQLreader("Video_MultiViewCount")) Then video.MultiViewCount = Convert.ToInt32(SQLreader("Video_MultiViewCount"))
                    If Not DBNull.Value.Equals(SQLreader("Video_FileSize")) Then video.Filesize = Convert.ToInt64(SQLreader("Video_FileSize"))
                    If Not DBNull.Value.Equals(SQLreader("Video_MultiViewLayout")) Then video.MultiViewLayout = SQLreader("Video_MultiViewLayout").ToString
                    If Not DBNull.Value.Equals(SQLreader("Video_StereoMode")) Then video.StereoMode = SQLreader("Video_StereoMode").ToString
                    nDbElement.MainDetails.FileInfo.StreamDetails.Video.Add(video)
                End While
            End Using
        End Using

        'Audio Streams
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM TVAStreams WHERE TVEpID = ", nDbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim audio As MediaContainers.Audio
                While SQLreader.Read
                    audio = New MediaContainers.Audio
                    If Not DBNull.Value.Equals(SQLreader("Audio_Language")) Then audio.Language = SQLreader("Audio_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_LongLanguage")) Then audio.LongLanguage = SQLreader("Audio_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Codec")) Then audio.Codec = SQLreader("Audio_Codec").ToString
                    If Not DBNull.Value.Equals(SQLreader("Audio_Channel")) Then audio.Channels = Convert.ToInt32(SQLreader("Audio_Channel"))
                    If Not DBNull.Value.Equals(SQLreader("Audio_Bitrate")) Then audio.Bitrate = Convert.ToInt32(SQLreader("Audio_Bitrate"))
                    nDbElement.MainDetails.FileInfo.StreamDetails.Audio.Add(audio)
                End While
            End Using
        End Using

        'embedded subtitles
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM TVSubs WHERE TVEpID = ", nDbElement.ID, " AND NOT Subs_Type = 'External';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaContainers.Subtitle
                While SQLreader.Read
                    subtitle = New MediaContainers.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.Type = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.Path = SQLreader("Subs_Path").ToString
                    subtitle.Forced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    nDbElement.MainDetails.FileInfo.StreamDetails.Subtitle.Add(subtitle)
                End While
            End Using
        End Using

        'external subtitles
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT * FROM TVSubs WHERE TVEpID = ", nDbElement.ID, " AND Subs_Type = 'External';")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim subtitle As MediaContainers.Subtitle
                While SQLreader.Read
                    subtitle = New MediaContainers.Subtitle
                    If Not DBNull.Value.Equals(SQLreader("Subs_Language")) Then subtitle.Language = SQLreader("Subs_Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_LongLanguage")) Then subtitle.LongLanguage = SQLreader("Subs_LongLanguage").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Type")) Then subtitle.Type = SQLreader("Subs_Type").ToString
                    If Not DBNull.Value.Equals(SQLreader("Subs_Path")) Then subtitle.Path = SQLreader("Subs_Path").ToString
                    subtitle.Forced = Convert.ToBoolean(SQLreader("Subs_Forced"))
                    nDbElement.Subtitles.Add(subtitle)
                End While
            End Using
        End Using

        'ImagesContainer
        nDbElement.ImagesContainer.Fanart.LocalFilePath = Get_ArtForItem(nDbElement.ID, "episode", "fanart")
        nDbElement.ImagesContainer.Poster.LocalFilePath = Get_ArtForItem(nDbElement.ID, "episode", "thumb")

        'Show container
        If withShow Then
            nDbElement = Master.DB.Load_TVShowInfoIntoDBElement(nDbElement)
        End If

        'Check if the file is available and ready to edit
        If File.Exists(nDbElement.Filename) Then nDbElement.IsOnline = True

        Return nDbElement
    End Function
    ''' <summary>
    ''' Load all the information for a TV Season
    ''' </summary>
    ''' <param name="id">Season ID</param>
    ''' <param name="WithShow">If <c>True</c>, also retrieve the TV Show information</param>
    ''' <returns>Database.DBElement object</returns>
    ''' <remarks></remarks>
    Public Function Load_TVSeason(ByVal id As Long, ByVal withShow As Boolean, ByVal withEpisodes As Boolean) As DBElement
        Dim nDbElement As New DBElement(Enums.ContentType.TVSeason) With {
            .ID = id,
            .MainDetails = New MediaContainers.MainDetails
        }
        Using SQLcommandTVSeason As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommandTVSeason.CommandText = String.Concat("SELECT * FROM seasons WHERE idSeason = ", nDbElement.ID, ";")
            Using SQLReader As SQLiteDataReader = SQLcommandTVSeason.ExecuteReader
                If SQLReader.HasRows Then
                    SQLReader.Read()
                    nDbElement.IsLocked = CBool(SQLReader("Lock"))
                    nDbElement.IsMarked = CBool(SQLReader("Mark"))
                    nDbElement.IsNew = Convert.ToBoolean(SQLReader("New"))
                    nDbElement.ShowID = Convert.ToInt64(SQLReader("idShow"))
                    nDbElement.ShowPath = Get_TVShowPath(Convert.ToInt64(SQLReader("idShow")))

                    With nDbElement.MainDetails
                        If Not DBNull.Value.Equals(SQLReader("strAired")) Then .Aired = CStr(SQLReader("strAired"))
                        If Not DBNull.Value.Equals(SQLReader("strPlot")) Then .Plot = CStr(SQLReader("strPlot"))
                        If Not DBNull.Value.Equals(SQLReader("Season")) Then .Season = CInt(SQLReader("Season"))
                        If Not DBNull.Value.Equals(SQLReader("Title")) Then .Title = CStr(SQLReader("Title"))
                    End With
                End If
            End Using
        End Using

        'ImagesContainer
        nDbElement.ImagesContainer.Banner.LocalFilePath = Get_ArtForItem(nDbElement.ID, "season", "banner")
        nDbElement.ImagesContainer.Fanart.LocalFilePath = Get_ArtForItem(nDbElement.ID, "season", "fanart")
        nDbElement.ImagesContainer.Landscape.LocalFilePath = Get_ArtForItem(nDbElement.ID, "season", "landscape")
        nDbElement.ImagesContainer.Poster.LocalFilePath = Get_ArtForItem(nDbElement.ID, "season", "poster")

        'UniqueIDs
        nDbElement.MainDetails.UniqueIDs = Get_UniqueIDsForItem(nDbElement.ID, nDbElement.ContentType)

        'Show container
        If withShow Then
            nDbElement = Master.DB.Load_TVShowInfoIntoDBElement(nDbElement)
        End If

        'Episodes
        If withEpisodes Then
            For Each tEpisode As DBElement In LoadAll_TVEpisodes(nDbElement.ShowID, withShow, nDbElement.MainDetails.Season)
                tEpisode = Load_TVShowInfoIntoDBElement(tEpisode, nDbElement)
                nDbElement.Episodes.Add(tEpisode)
            Next
        End If

        Return nDbElement
    End Function
    ''' <summary>
    ''' Load all the information for a TV Show
    ''' </summary>
    ''' <param name="showId">Show ID</param>
    ''' <param name="seasonNumber">Season number</param>
    ''' <param name="withShow">If <c>True</c>, also retrieve the TV Show information</param>
    ''' <returns>Database.DBElement object</returns>
    ''' <remarks></remarks>
    Public Function Load_TVSeason(ByVal showId As Long, ByVal seasonNumber As Integer, ByVal withShow As Boolean, ByVal withEpisodes As Boolean) As DBElement
        If showId < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & showId)

        Dim _TVDB As New DBElement(Enums.ContentType.TVSeason) With {
            .ShowID = showId
        }
        If withShow Then Load_TVShowInfoIntoDBElement(_TVDB)

        Using SQLcommandTVSeason As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommandTVSeason.CommandText = String.Concat("SELECT idSeason FROM seasons WHERE idShow = ", showId, " AND Season = ", seasonNumber, ";")
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
    ''' <param name="id">Show ID</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Load_TVShow(ByVal id As Long, ByVal withSeasons As Boolean, ByVal withEpisodes As Boolean, Optional ByVal withMissingEpisodes As Boolean = False) As DBElement
        If id < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & id)

        Dim nDbElement As New DBElement(Enums.ContentType.TVShow) With {
            .ID = id,
            .ShowID = id,
            .MainDetails = New MediaContainers.MainDetails
        }

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("SELECT * FROM tvshow WHERE idShow = {0};", id)
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    SQLreader.Read()
                    If Not DBNull.Value.Equals(SQLreader("EFanartsPath")) Then nDbElement.ExtrafanartsPath = SQLreader("EFanartsPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("Language")) Then nDbElement.Language = SQLreader("Language").ToString
                    If Not DBNull.Value.Equals(SQLreader("NfoPath")) Then nDbElement.NfoPath = SQLreader("NfoPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("TVShowPath")) Then nDbElement.ShowPath = SQLreader("TVShowPath").ToString
                    If Not DBNull.Value.Equals(SQLreader("ThemePath")) Then nDbElement.Theme.LocalFilePath = SQLreader("ThemePath").ToString

                    nDbElement.Source = Load_Source_TVShow(Convert.ToInt64(SQLreader("idSource")))

                    nDbElement.IsLocked = Convert.ToBoolean(SQLreader("Lock"))
                    nDbElement.IsMarked = Convert.ToBoolean(SQLreader("Mark"))
                    nDbElement.IsNew = Convert.ToBoolean(SQLreader("New"))
                    nDbElement.EpisodeOrdering = DirectCast(Convert.ToInt32(SQLreader("Ordering")), Enums.EpisodeOrdering)
                    nDbElement.EpisodeSorting = DirectCast(Convert.ToInt32(SQLreader("EpisodeSorting")), Enums.EpisodeSorting)

                    With nDbElement.MainDetails
                        If Not DBNull.Value.Equals(SQLreader("Title")) Then .Title = SQLreader("Title").ToString
                        If Not DBNull.Value.Equals(SQLreader("EpisodeGuide")) Then .EpisodeGuideURL.URL = SQLreader("EpisodeGuide").ToString
                        If Not DBNull.Value.Equals(SQLreader("Plot")) Then .Plot = SQLreader("Plot").ToString
                        If Not DBNull.Value.Equals(SQLreader("Premiered")) Then .Premiered = SQLreader("Premiered").ToString
                        If Not DBNull.Value.Equals(SQLreader("MPAA")) Then .MPAA = SQLreader("MPAA").ToString
                        If Not DBNull.Value.Equals(SQLreader("Status")) Then .Status = SQLreader("Status").ToString
                        If Not DBNull.Value.Equals(SQLreader("Runtime")) Then .Runtime = SQLreader("Runtime").ToString
                        If Not DBNull.Value.Equals(SQLreader("SortTitle")) Then .SortTitle = SQLreader("SortTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("Language")) Then .Language = SQLreader("Language").ToString
                        If Not DBNull.Value.Equals(SQLreader("strOriginalTitle")) Then .OriginalTitle = SQLreader("strOriginalTitle").ToString
                        If Not DBNull.Value.Equals(SQLreader("iUserRating")) Then .UserRating = Convert.ToInt32(SQLreader("iUserRating"))
                        If Not DBNull.Value.Equals(SQLreader("Certification")) Then .AddCertificationsFromString(SQLreader("Certification").ToString)
                        If Not DBNull.Value.Equals(SQLreader("userNote")) Then .UserNote = SQLreader("userNote").ToString
                        If Not DBNull.Value.Equals(SQLreader("Tagline")) Then .Tagline = SQLreader("Tagline").ToString
                    End With
                End If
            End Using
        End Using

        'Actors
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT A.strRole, B.idActor, B.strActor, B.strThumb, C.url FROM actorlinktvshow AS A ",
                                                   "INNER JOIN actors AS B ON (A.idActor = B.idActor) ",
                                                   "LEFT OUTER JOIN art AS C ON (B.idActor = C.media_id AND C.media_type = 'actor' AND C.type = 'thumb') ",
                                                   "WHERE A.idShow = ", nDbElement.ID, " ",
                                                   "ORDER BY A.iOrder;")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim actor As MediaContainers.Person
                While SQLreader.Read
                    actor = New MediaContainers.Person With {
                        .ID = Convert.ToInt64(SQLreader("idActor")),
                        .Name = SQLreader("strActor").ToString,
                        .Role = SQLreader("strRole").ToString,
                        .LocalFilePath = SQLreader("url").ToString,
                        .URLOriginal = SQLreader("strThumb").ToString
                    }
                    nDbElement.MainDetails.Actors.Add(actor)
                End While
            End Using
        End Using

        'Countries
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT country.strCountry ",
                                                   "FROM country ",
                                                   "INNER JOIN countrylinktvshow ON (country.idCountry = countrylinktvshow.idCountry) ",
                                                   "WHERE countrylinktvshow.idShow = ", nDbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nDbElement.MainDetails.Countries.Add(SQLreader("strCountry").ToString)
                End While
            End Using
        End Using

        'Creators
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT actors.strActor ",
                                                   "FROM actors ",
                                                   "INNER JOIN creatorlinktvshow ON (actors.idActor = creatorlinktvshow.idActor) ",
                                                   "WHERE creatorlinktvshow.idShow = ", nDbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    nDbElement.MainDetails.Creators.Add(SQLreader("strActor").ToString)
                End While
            End Using
        End Using

        'Directors
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT actors.strActor ",
                                                   "FROM actors ",
                                                   "INNER JOIN directorlinktvshow ON (actors.idActor = directorlinktvshow.idDirector) ",
                                                   "WHERE directorlinktvshow.idShow = ", nDbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strActor")) Then nDbElement.MainDetails.Directors.Add(SQLreader("strActor").ToString)
                End While
            End Using
        End Using

        'Genres
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT genre.strGenre ",
                                                   "FROM genre ",
                                                   "INNER JOIN genrelinktvshow ON (genre.idGenre = genrelinktvshow.idGenre) ",
                                                   "WHERE genrelinktvshow.idShow = ", nDbElement.ID, " ORDER BY sorting;")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strGenre")) Then nDbElement.MainDetails.Genres.Add(SQLreader("strGenre").ToString)
                End While
            End Using
        End Using

        'Ratings
        nDbElement.MainDetails.Ratings = Get_RatingsForItem(nDbElement.ID, nDbElement.ContentType)

        'Studios
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT studio.strStudio ",
                                                   "FROM studio ",
                                                   "INNER JOIN studiolinktvshow ON (studio.idStudio = studiolinktvshow.idStudio) ",
                                                   "WHERE studiolinktvshow.idShow = ", nDbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    If Not DBNull.Value.Equals(SQLreader("strStudio")) Then nDbElement.MainDetails.Studios.Add(SQLreader("strStudio").ToString)
                End While
            End Using
        End Using

        'Tags
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT B.strTag FROM taglinks ",
                                                   "AS A INNER JOIN tag AS B ON (A.idTag = B.idTag) WHERE A.idMedia = ", nDbElement.ID, " And A.media_type = 'tvshow' ORDER BY sorting;")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                Dim tag As String
                While SQLreader.Read
                    tag = String.Empty
                    If Not DBNull.Value.Equals(SQLreader("strTag")) Then tag = SQLreader("strTag").ToString
                    nDbElement.MainDetails.Tags.Add(tag)
                End While
            End Using
        End Using

        'UniqueIDs
        nDbElement.MainDetails.UniqueIDs = Get_UniqueIDsForItem(nDbElement.ID, nDbElement.ContentType)

        'ImagesContainer
        nDbElement.ImagesContainer.Banner.LocalFilePath = Get_ArtForItem(nDbElement.ID, "tvshow", "banner")
        nDbElement.ImagesContainer.CharacterArt.LocalFilePath = Get_ArtForItem(nDbElement.ID, "tvshow", "characterart")
        nDbElement.ImagesContainer.ClearArt.LocalFilePath = Get_ArtForItem(nDbElement.ID, "tvshow", "clearart")
        nDbElement.ImagesContainer.ClearLogo.LocalFilePath = Get_ArtForItem(nDbElement.ID, "tvshow", "clearlogo")
        nDbElement.ImagesContainer.Fanart.LocalFilePath = Get_ArtForItem(nDbElement.ID, "tvshow", "fanart")
        nDbElement.ImagesContainer.Keyart.LocalFilePath = Get_ArtForItem(nDbElement.ID, "tvshow", "keyart")
        nDbElement.ImagesContainer.Landscape.LocalFilePath = Get_ArtForItem(nDbElement.ID, "tvshow", "landscape")
        nDbElement.ImagesContainer.Poster.LocalFilePath = Get_ArtForItem(nDbElement.ID, "tvshow", "poster")
        If Not String.IsNullOrEmpty(nDbElement.ExtrafanartsPath) AndAlso Directory.Exists(nDbElement.ExtrafanartsPath) Then
            For Each ePath As String In Directory.GetFiles(nDbElement.ExtrafanartsPath, "*.jpg")
                nDbElement.ImagesContainer.Extrafanarts.Add(New MediaContainers.Image With {.LocalFilePath = ePath})
            Next
        End If

        'Seasons
        If withSeasons Then
            For Each tSeason As DBElement In LoadAll_TVSeasons(nDbElement.ID)
                tSeason = Load_TVShowInfoIntoDBElement(tSeason, nDbElement)
                nDbElement.Seasons.Add(tSeason)
                nDbElement.MainDetails.Seasons.Seasons.Add(tSeason.MainDetails)
            Next
            '_TVDB.TVShow.Seasons = LoadAllTVSeasonsDetailsFromDB(_TVDB.ID)
        End If

        'Episodes
        If withEpisodes Then
            For Each tEpisode As DBElement In LoadAll_TVEpisodes(nDbElement.ID, False, -1, withMissingEpisodes)
                tEpisode = Load_TVShowInfoIntoDBElement(tEpisode, nDbElement)
                nDbElement.Episodes.Add(tEpisode)
            Next
        End If

        'Check if the path is available and ready to edit
        If Directory.Exists(nDbElement.ShowPath) Then nDbElement.IsOnline = True

        Return nDbElement
    End Function
    ''' <summary>
    ''' Load all the information for a TV Show
    ''' </summary>
    ''' <param name="id">Show ID</param>
    ''' <returns>Database.DBElement object</returns>
    ''' <remarks></remarks>
    Public Function Load_TVShow_Full(ByVal id As Long) As DBElement
        If id < 0 Then Throw New ArgumentOutOfRangeException("ShowID", "Value must be >= 0, was given: " & id)
        Return Master.DB.Load_TVShow(id, True, True, True)
    End Function
    ''' <summary>
    ''' Adds TVShow informations to a Database.DBElement
    ''' </summary>
    ''' <param name="episodeOrSeasonElement">Database.DBElement container to fill with TVShow informations</param>
    ''' <param name="tvshow">Optional the TVShow informations to add to _TVDB</param>
    ''' <remarks></remarks>
    Public Function Load_TVShowInfoIntoDBElement(ByVal episodeOrSeasonElement As DBElement, Optional ByVal tvshow As DBElement = Nothing) As DBElement
        Dim nTvShow As DBElement

        If tvshow Is Nothing OrElse tvshow.MainDetails Is Nothing Then
            nTvShow = Load_TVShow(episodeOrSeasonElement.ShowID, False, False)
        Else
            nTvShow = tvshow
        End If

        episodeOrSeasonElement.EpisodeSorting = nTvShow.EpisodeSorting
        episodeOrSeasonElement.EpisodeOrdering = nTvShow.EpisodeOrdering
        episodeOrSeasonElement.Language = nTvShow.Language
        episodeOrSeasonElement.ShowID = nTvShow.ShowID
        episodeOrSeasonElement.ShowPath = nTvShow.ShowPath
        episodeOrSeasonElement.Source = nTvShow.Source
        episodeOrSeasonElement.TVShowDetails = nTvShow.MainDetails
        Return episodeOrSeasonElement
    End Function

    Public Sub Remove_ExcludedPath(ByVal path As String, ByVal batchMode As Boolean)
        If Not String.IsNullOrEmpty(path) Then
            Dim SQLtransaction As SQLiteTransaction = Nothing
            If Not batchMode Then SQLtransaction = _myvideosDBConn.BeginTransaction()
            Using sqlCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Concat("DELETE FROM ExcludeDir WHERE Dirname=(?);")
                Dim par_path As SQLiteParameter = sqlCommand.Parameters.Add("par_Dirname", DbType.String, 0, "Dirname")
                par_path.Value = path
                sqlCommand.ExecuteNonQuery()
            End Using
            If Not batchMode Then SQLtransaction.Commit()
        End If
    End Sub

    Private Sub Remove_MoviesetFromMovie(ByVal idMovie As Long)
        Remove_FromTable("setlinkmovie", "idMovie", idMovie)
    End Sub

    Private Sub Remove_RatingsFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        Remove_FromTable("rating", idMedia, contentType)
    End Sub

    Public Sub Remove_Source_Movie(ByVal ID As Long, ByVal batchMode As Boolean)
        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not batchMode Then SQLtransaction = _MyvideosDBConn.BeginTransaction()
        Using sqlCommand As SQLiteCommand = _MyvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("DELETE FROM moviesource WHERE idSource={0};", ID)
            sqlCommand.ExecuteNonQuery()
        End Using
        If Not batchMode Then SQLtransaction.Commit()
    End Sub

    Public Sub Remove_Source_TVShow(ByVal ID As Long, ByVal batchMode As Boolean)
        Dim SQLtransaction As SQLiteTransaction = Nothing
        If Not batchMode Then SQLtransaction = _MyvideosDBConn.BeginTransaction()
        Using sqlCommand As SQLiteCommand = _MyvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("DELETE FROM tvshowsource WHERE idSource={0};", ID)
            sqlCommand.ExecuteNonQuery()
        End Using
        If Not batchMode Then SQLtransaction.Commit()
    End Sub

    Private Sub Remove_UniqueIDsFromItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        Remove_FromTable("uniqueid", idMedia, contentType)
    End Sub

    Private Sub Remove_FromTable(ByVal table As String, ByVal idMedia As Long, ByVal contentType As Enums.ContentType)
        Dim mediaType As String = Convert_ContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("DELETE FROM {0} WHERE media_id={1} AND media_type='{2}';", table, idMedia, mediaType)
                sqlCommand.ExecuteNonQuery()
            End Using
        End If
    End Sub

    Private Sub Remove_FromTable(ByVal table As String, ByVal firstField As String, firstId As Long)
        If Not String.IsNullOrEmpty(table) AndAlso Not String.IsNullOrEmpty(firstField) AndAlso Not firstId = -1 Then
            Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand.CommandText = String.Format("DELETE FROM {0} WHERE {1}={2};", table, firstField, firstId)
                sqlCommand.ExecuteNonQuery()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Saves all information from a Database.DBElement object to the database
    ''' </summary>
    ''' <param name="dbElement">Media.Movie object to save to the database</param>
    ''' <param name="batchMode">Is the function already part of a transaction?</param>
    ''' <param name="toNfo">Save informations to NFO</param>
    ''' <param name="toDisk">Save Images, Themes and Trailers to disk</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Save_Movie(ByVal dbElement As DBElement, ByVal batchMode As Boolean, ByVal toNfo As Boolean, ByVal toDisk As Boolean, ByVal doSync As Boolean, ByVal forceFileCleanup As Boolean) As DBElement
        If dbElement.MainDetails Is Nothing Then Return dbElement

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "INSERT OR REPLACE INTO movie ("
            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "idMovie,")
            End If

            Dim dbFields As String() = New String() {
                "idSource",
                "MoviePath",
                "Type",
                "HasSub",
                "New",
                "Mark",
                "Imdb",
                "Lock",
                "Title",
                "OriginalTitle",
                "SortTitle",
                "Year",
                "Rating",
                "Votes",
                "MPAA",
                "Top250",
                "Outline",
                "Plot",
                "Tagline",
                "Certification",
                "Runtime",
                "premiered",
                "Playcount",
                "Trailer",
                "NfoPath",
                "TrailerPath",
                "SubPath",
                "EThumbsPath",
                "FanartURL",
                "OutOfTolerance",
                "VideoSource",
                "DateAdded",
                "EFanartsPath",
                "ThemePath",
                "TMDB",
                "TMDBColID",
                "DateModified",
                "MarkCustom1",
                "MarkCustom2",
                "MarkCustom3",
                "MarkCustom4",
                "HasSet",
                "iLastPlayed",
                "Language",
                "iUserRating",
                "userNote",
                "edition"
            }
            sqlCommand.CommandText = String.Format("{0}{1}) VALUES ({2}",
                                                   sqlCommand.CommandText,
                                                   String.Join(",", dbFields),
                                                   String.Join(",", New String("?"c, dbFields.Count).ToList)
                                                   )

            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, ",?")
                Dim par_idMovie As SQLiteParameter = sqlCommand.Parameters.Add("par_idMovie", DbType.Int64, 0, "idMovie")
                par_idMovie.Value = dbElement.ID
            End If
            sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "); SELECT LAST_INSERT_ROWID() FROM movie;")

            Dim par_idSource As SQLiteParameter = sqlCommand.Parameters.Add("par_idSource", DbType.Int64, 0, "idSource")
            Dim par_MoviePath As SQLiteParameter = sqlCommand.Parameters.Add("par_MoviePath", DbType.String, 0, "MoviePath")
            Dim par_Type As SQLiteParameter = sqlCommand.Parameters.Add("par_Type", DbType.Boolean, 0, "Type")
            Dim par_HasSub As SQLiteParameter = sqlCommand.Parameters.Add("par_HasSub", DbType.Boolean, 0, "HasSub")
            Dim par_New As SQLiteParameter = sqlCommand.Parameters.Add("par_New", DbType.Boolean, 0, "New")
            Dim par_Mark As SQLiteParameter = sqlCommand.Parameters.Add("par_Mark", DbType.Boolean, 0, "Mark")
            Dim par_Imdb As SQLiteParameter = sqlCommand.Parameters.Add("par_Imdb", DbType.String, 0, "Imdb")
            Dim par_Lock As SQLiteParameter = sqlCommand.Parameters.Add("par_Lock", DbType.Boolean, 0, "Lock")
            Dim par_Title As SQLiteParameter = sqlCommand.Parameters.Add("par_Title", DbType.String, 0, "Title")
            Dim par_OriginalTitle As SQLiteParameter = sqlCommand.Parameters.Add("par_OriginalTitle", DbType.String, 0, "OriginalTitle")
            Dim par_SortTitle As SQLiteParameter = sqlCommand.Parameters.Add("par_SortTitle", DbType.String, 0, "SortTitle")
            Dim par_Year As SQLiteParameter = sqlCommand.Parameters.Add("par_Year", DbType.String, 0, "Year")
            Dim par_Rating As SQLiteParameter = sqlCommand.Parameters.Add("par_Rating", DbType.String, 0, "Rating")
            Dim par_Votes As SQLiteParameter = sqlCommand.Parameters.Add("par_Votes", DbType.String, 0, "Votes")
            Dim par_MPAA As SQLiteParameter = sqlCommand.Parameters.Add("par_MPAA", DbType.String, 0, "MPAA")
            Dim par_Top250 As SQLiteParameter = sqlCommand.Parameters.Add("par_Top250", DbType.Int64, 0, "Top250")
            Dim par_Outline As SQLiteParameter = sqlCommand.Parameters.Add("par_Outline", DbType.String, 0, "Outline")
            Dim par_Plot As SQLiteParameter = sqlCommand.Parameters.Add("par_Plot", DbType.String, 0, "Plot")
            Dim par_Tagline As SQLiteParameter = sqlCommand.Parameters.Add("par_Tagline", DbType.String, 0, "Tagline")
            Dim par_Certification As SQLiteParameter = sqlCommand.Parameters.Add("par_Certification", DbType.String, 0, "Certification")
            Dim par_Runtime As SQLiteParameter = sqlCommand.Parameters.Add("par_Runtime", DbType.String, 0, "Runtime")
            Dim par_premiered As SQLiteParameter = sqlCommand.Parameters.Add("par_premiered", DbType.String, 0, "premiered")
            Dim par_Playcount As SQLiteParameter = sqlCommand.Parameters.Add("par_Playcount", DbType.Int64, 0, "Playcount")
            Dim par_Trailer As SQLiteParameter = sqlCommand.Parameters.Add("par_Trailer", DbType.String, 0, "Trailer")
            Dim par_NfoPath As SQLiteParameter = sqlCommand.Parameters.Add("par_NfoPath", DbType.String, 0, "NfoPath")
            Dim par_TrailerPath As SQLiteParameter = sqlCommand.Parameters.Add("par_TrailerPath", DbType.String, 0, "TrailerPath")
            Dim par_SubPath As SQLiteParameter = sqlCommand.Parameters.Add("par_SubPath", DbType.String, 0, "SubPath")
            Dim par_EThumbsPath As SQLiteParameter = sqlCommand.Parameters.Add("par_EThumbsPath", DbType.String, 0, "EThumbsPath")
            Dim par_FanartURL As SQLiteParameter = sqlCommand.Parameters.Add("par_FanartURL", DbType.String, 0, "FanartURL")
            Dim par_OutOfTolerance As SQLiteParameter = sqlCommand.Parameters.Add("par_OutOfTolerance", DbType.Boolean, 0, "OutOfTolerance")
            Dim par_VideoSource As SQLiteParameter = sqlCommand.Parameters.Add("par_VideoSource", DbType.String, 0, "VideoSource")
            Dim par_DateAdded As SQLiteParameter = sqlCommand.Parameters.Add("par_DateAdded", DbType.Int64, 0, "DateAdded")
            Dim par_EFanartsPath As SQLiteParameter = sqlCommand.Parameters.Add("par_EFanartsPath", DbType.String, 0, "EFanartsPath")
            Dim par_ThemePath As SQLiteParameter = sqlCommand.Parameters.Add("par_ThemePath", DbType.String, 0, "ThemePath")
            Dim par_TMDB As SQLiteParameter = sqlCommand.Parameters.Add("par_TMDB", DbType.String, 0, "TMDB")
            Dim par_TMDBColID As SQLiteParameter = sqlCommand.Parameters.Add("par_TMDBColID", DbType.String, 0, "TMDBColID")
            Dim par_DateModified As SQLiteParameter = sqlCommand.Parameters.Add("par_DateModified", DbType.Int64, 0, "DateModified")
            Dim par_MarkCustom1 As SQLiteParameter = sqlCommand.Parameters.Add("par_MarkCustom1", DbType.Boolean, 0, "MarkCustom1")
            Dim par_MarkCustom2 As SQLiteParameter = sqlCommand.Parameters.Add("par_MarkCustom2", DbType.Boolean, 0, "MarkCustom2")
            Dim par_MarkCustom3 As SQLiteParameter = sqlCommand.Parameters.Add("par_MarkCustom3", DbType.Boolean, 0, "MarkCustom3")
            Dim par_MarkCustom4 As SQLiteParameter = sqlCommand.Parameters.Add("par_MarkCustom4", DbType.Boolean, 0, "MarkCustom4")
            Dim par_HasSet As SQLiteParameter = sqlCommand.Parameters.Add("par_HasSet", DbType.Boolean, 0, "HasSet")
            Dim par_iLastPlayed As SQLiteParameter = sqlCommand.Parameters.Add("par_iLastPlayed", DbType.Int64, 0, "iLastPlayed")
            Dim par_Language As SQLiteParameter = sqlCommand.Parameters.Add("par_Language", DbType.String, 0, "Language")
            Dim par_iUserRating As SQLiteParameter = sqlCommand.Parameters.Add("par_iUserRating", DbType.Int64, 0, "iUserRating")
            Dim par_userNote As SQLiteParameter = sqlCommand.Parameters.Add("par_userNote", DbType.String, 0, "userNote")
            Dim par_edition As SQLiteParameter = sqlCommand.Parameters.Add("par_edition", DbType.String, 0, "edition")

            'DateAdded
            Try
                If Not Master.eSettings.GeneralDateAddedIgnoreNFO AndAlso dbElement.MainDetails.DateAddedSpecified Then
                    Dim DateTimeAdded As Date = Date.ParseExact(dbElement.MainDetails.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    par_DateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    Select Case Master.eSettings.GeneralDateTime
                        Case Enums.DateTimeStamp.Now
                            Dim DateTimeAdded As Date
                            If Date.TryParseExact(dbElement.MainDetails.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, DateTimeAdded) Then
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                            Else
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                            End If
                        Case Enums.DateTimeStamp.ctime
                            Dim ctime As Date = File.GetCreationTime(dbElement.Filename)
                            If ctime.Year > 1601 Then
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            Else
                                Dim mtime As Date = File.GetLastWriteTime(dbElement.Filename)
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            End If
                        Case Enums.DateTimeStamp.mtime
                            Dim mtime As Date = File.GetLastWriteTime(dbElement.Filename)
                            If mtime.Year > 1601 Then
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                Dim ctime As Date = File.GetCreationTime(dbElement.Filename)
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                        Case Enums.DateTimeStamp.Newer
                            Dim mtime As Date = File.GetLastWriteTime(dbElement.Filename)
                            Dim ctime As Date = File.GetCreationTime(dbElement.Filename)
                            If mtime > ctime Then
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                    End Select
                End If
                dbElement.MainDetails.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_DateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            Catch
                Dim DateTimeAdded As Date
                If Date.TryParseExact(dbElement.MainDetails.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, DateTimeAdded) Then
                    par_DateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    par_DateAdded.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                End If
                dbElement.MainDetails.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_DateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            End Try

            'DateModified
            Try
                If Not dbElement.IDSpecified AndAlso dbElement.MainDetails.DateModifiedSpecified Then
                    Dim DateTimeDateModified As Date = Date.ParseExact(dbElement.MainDetails.DateModified, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    par_DateModified.Value = Functions.ConvertToUnixTimestamp(DateTimeDateModified)
                ElseIf dbElement.IDSpecified Then
                    par_DateModified.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                End If
                If par_DateModified.Value IsNot Nothing Then
                    dbElement.MainDetails.DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_DateModified.Value)).ToString("yyyy-MM-dd HH:mm:ss")
                Else
                    dbElement.MainDetails.DateModified = String.Empty
                End If
            Catch
                par_DateModified.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                dbElement.MainDetails.DateModified = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_DateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            End Try

            'LastPlayed
            Dim DateTimeLastPlayedUnix As Double = -1
            If dbElement.MainDetails.LastPlayedSpecified Then
                Try
                    Dim DateTimeLastPlayed As Date = Date.ParseExact(dbElement.MainDetails.LastPlayed, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    DateTimeLastPlayedUnix = Functions.ConvertToUnixTimestamp(DateTimeLastPlayed)
                Catch
                    'Kodi save it only as yyyy-MM-dd, try that
                    Try
                        Dim DateTimeLastPlayed As Date = Date.ParseExact(dbElement.MainDetails.LastPlayed, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture)
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
                dbElement.MainDetails.LastPlayed = String.Empty
            End If

            'Trailer URL
            If Master.eSettings.Movie.InformationSettings.TrailerLink.SaveKodiCompatible Then
                dbElement.MainDetails.Trailer = dbElement.MainDetails.Trailer.Trim.Replace("http://www.youtube.com/watch?v=", "plugin://plugin.video.youtube/?action=play_video&videoid=")
                dbElement.MainDetails.Trailer = dbElement.MainDetails.Trailer.Replace("http://www.youtube.com/watch?hd=1&v=", "plugin://plugin.video.youtube/?action=play_video&videoid=")
            End If

            'First let's save it to NFO, even because we will need the NFO path
            'Also save Images to get ExtrafanartsPath and ExtrathumbsPath
            'art Table will be linked later
            If toNfo Then NFO.SaveToNFO_Movie(dbElement, forceFileCleanup)
            If toDisk Then
                dbElement.ImagesContainer.SaveAllImages(dbElement, forceFileCleanup)
                dbElement.MainDetails.SaveAllActorThumbs(dbElement)
                dbElement.Theme.Save(dbElement, Enums.ModifierType.MainTheme, forceFileCleanup)
                dbElement.Trailer.Save(dbElement, Enums.ModifierType.MainTrailer, forceFileCleanup)
            End If

            par_MoviePath.Value = dbElement.Filename
            par_Type.Value = dbElement.IsSingle

            par_EFanartsPath.Value = dbElement.ExtrafanartsPath
            par_EThumbsPath.Value = dbElement.ExtrathumbsPath
            par_NfoPath.Value = dbElement.NfoPath
            par_ThemePath.Value = If(Not String.IsNullOrEmpty(dbElement.Theme.LocalFilePath), dbElement.Theme.LocalFilePath, String.Empty)
            par_TrailerPath.Value = If(Not String.IsNullOrEmpty(dbElement.Trailer.LocalFilePath), dbElement.Trailer.LocalFilePath, String.Empty)

            par_HasSet.Value = dbElement.MainDetails.SetsSpecified
            If dbElement.Subtitles Is Nothing = False Then
                par_HasSub.Value = dbElement.Subtitles.Count > 0 OrElse dbElement.MainDetails.FileInfo.StreamDetails.Subtitle.Count > 0
            Else
                par_HasSub.Value = Nothing
            End If

            par_Lock.Value = dbElement.IsLocked
            par_Mark.Value = dbElement.IsMarked
            par_MarkCustom1.Value = dbElement.IsMarkCustom1
            par_MarkCustom2.Value = dbElement.IsMarkCustom2
            par_MarkCustom3.Value = dbElement.IsMarkCustom3
            par_MarkCustom4.Value = dbElement.IsMarkCustom4
            par_New.Value = Not dbElement.IDSpecified

            With dbElement.MainDetails
                par_Certification.Value = String.Join(" / ", .Certifications.ToArray)
                par_Imdb.Value = If(.UniqueIDs.IMDbIdSpecified, .UniqueIDs.IMDbId, String.Empty)
                par_iUserRating.Value = .UserRating
                par_MPAA.Value = .MPAA
                par_OriginalTitle.Value = .OriginalTitle
                par_Outline.Value = .Outline
                If .PlayCountSpecified Then 'need to be NOTHING instead of "0"
                    par_Playcount.Value = .PlayCount
                End If
                par_Plot.Value = .Plot
                par_Rating.Value = .Rating
                par_premiered.Value = NumUtils.DateToISO8601Date(.Premiered)
                par_Runtime.Value = .Runtime
                par_SortTitle.Value = .SortTitle
                par_TMDB.Value = If(.UniqueIDs.TMDbIdSpecified, .UniqueIDs.TMDbId.ToString, String.Empty)
                par_TMDBColID.Value = If(.UniqueIDs.TMDbCollectionIdSpecified, .UniqueIDs.TMDbCollectionId.ToString, String.Empty)
                par_Tagline.Value = .Tagline
                par_Title.Value = .Title
                If .Top250Specified Then 'need to be NOTHING instead of "0"
                    par_Top250.Value = .Top250
                End If
                par_Trailer.Value = .Trailer
                par_userNote.Value = .UserNote
                par_Votes.Value = .Votes
                par_Year.Value = .Year
            End With

            par_OutOfTolerance.Value = dbElement.OutOfTolerance
            par_VideoSource.Value = dbElement.VideoSource
            par_Language.Value = dbElement.Language
            par_edition.Value = dbElement.Edition

            par_idSource.Value = dbElement.Source.ID

            If Not dbElement.IDSpecified Then
                If Master.eSettings.MovieGeneralMarkNew Then
                    par_Mark.Value = True
                    dbElement.IsMarked = True
                End If
                Using rdrMovie As SQLiteDataReader = sqlCommand.ExecuteReader()
                    If rdrMovie.Read Then
                        dbElement.ID = Convert.ToInt64(rdrMovie(0))
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
                Using sqlCommand_actorlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_actorlink.CommandText = String.Format("DELETE FROM actorlinkmovie WHERE idMovie = {0};", dbElement.ID)
                    sqlCommand_actorlink.ExecuteNonQuery()
                End Using
                Add_Cast(dbElement.ID, "movie", "movie", dbElement.MainDetails.Actors)

                'Countries
                Using sqlCommand_countrylink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_countrylink.CommandText = String.Format("DELETE FROM countrylinkmovie WHERE idMovie = {0};", dbElement.ID)
                    sqlCommand_countrylink.ExecuteNonQuery()
                End Using
                For Each country As String In dbElement.MainDetails.Countries
                    Add_Country_Movie(dbElement.ID, Add_Country(country))
                Next

                'Directors
                Using sqlCommand_directorlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_directorlink.CommandText = String.Format("DELETE FROM directorlinkmovie WHERE idMovie = {0};", dbElement.ID)
                    sqlCommand_directorlink.ExecuteNonQuery()
                End Using
                For Each director As String In dbElement.MainDetails.Directors
                    Add_Director_Movie(dbElement.ID, Add_Actor(director, "", "", "", "", False))
                Next

                'Genres
                Using sqlCommand_genrelink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_genrelink.CommandText = String.Format("DELETE FROM genrelinkmovie WHERE idMovie = {0};", dbElement.ID)
                    sqlCommand_genrelink.ExecuteNonQuery()
                End Using
                Dim iGenre As Integer = 0
                For Each genre As String In dbElement.MainDetails.Genres
                    Add_Genre_Movie(dbElement.ID, Add_Genre(genre), iGenre)
                    iGenre += 1
                Next

                'Images
                Using sqlCommand_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_art.CommandText = String.Format("DELETE FROM art WHERE media_id = {0} AND media_type = 'movie';", dbElement.ID)
                    sqlCommand_art.ExecuteNonQuery()
                End Using
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Banner.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "movie", "banner", dbElement.ImagesContainer.Banner.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.ClearArt.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "movie", "clearart", dbElement.ImagesContainer.ClearArt.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.ClearLogo.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "movie", "clearlogo", dbElement.ImagesContainer.ClearLogo.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.DiscArt.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "movie", "discart", dbElement.ImagesContainer.DiscArt.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Fanart.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "movie", "fanart", dbElement.ImagesContainer.Fanart.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Keyart.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "movie", "keyart", dbElement.ImagesContainer.Keyart.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Landscape.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "movie", "landscape", dbElement.ImagesContainer.Landscape.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Poster.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "movie", "poster", dbElement.ImagesContainer.Poster.LocalFilePath)

                'Movieset
                'process moviesets and rewrite the NFO if a movieset has been changed during this process
                If Set_MoviesetsForMovie(dbElement, dbElement.MainDetails.Sets) Then NFO.SaveToNFO_Movie(dbElement, forceFileCleanup)

                'Ratings
                Set_RatingsForItem(dbElement.ID, dbElement.ContentType, dbElement.MainDetails.Ratings)

                'Studios
                Using sqlCommand_studiolink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_studiolink.CommandText = String.Format("DELETE FROM studiolinkmovie WHERE idMovie = {0};", dbElement.ID)
                    sqlCommand_studiolink.ExecuteNonQuery()
                End Using
                For Each studio As String In dbElement.MainDetails.Studios
                    Add_Studio_Movie(dbElement.ID, Add_Studio(studio))
                Next

                'Tags
                Using sqlCommand_taglinks As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_taglinks.CommandText = String.Format("DELETE FROM taglinks WHERE idMedia = {0} AND media_type = 'movie';", dbElement.ID)
                    sqlCommand_taglinks.ExecuteNonQuery()
                End Using
                Dim iTag As Integer = 0
                For Each tag As String In dbElement.MainDetails.Tags
                    Add_TagToItem(dbElement.ID, Add_Tag(tag), "movie", iTag)
                    iTag += 1
                Next

                'UniqueIDs
                Set_UniqueIDsForItem(dbElement.ID, dbElement.ContentType, dbElement.MainDetails.UniqueIDs)

                'Writers
                Using sqlCommand_writerlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_writerlink.CommandText = String.Format("DELETE FROM writerlinkmovie WHERE idMovie = {0};", dbElement.ID)
                    sqlCommand_writerlink.ExecuteNonQuery()
                End Using
                For Each writer As String In dbElement.MainDetails.Credits
                    Add_Writer_Movie(dbElement.ID, Add_Actor(writer, "", "", "", "", False))
                Next

                'Video Streams
                Using SsqlCommand_MoviesVStreams As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SsqlCommand_MoviesVStreams.CommandText = String.Format("DELETE FROM MoviesVStreams WHERE MovieID = {0};", dbElement.ID)
                    SsqlCommand_MoviesVStreams.ExecuteNonQuery()

                    'Expanded SQL Statement to INSERT/replace new fields
                    SsqlCommand_MoviesVStreams.CommandText = String.Concat("INSERT OR REPLACE INTO MoviesVStreams (",
                       "MovieID, StreamID, Video_Width,Video_Height,Video_Codec,Video_Duration, Video_ScanType, Video_AspectDisplayRatio, ",
                       "Video_Language, Video_LongLanguage, Video_Bitrate, Video_MultiViewCount, Video_FileSize, Video_MultiViewLayout, ",
                       "Video_StereoMode) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);")

                    Dim parVideo_MovieID As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_MovieID", DbType.Int64, 0, "MovieID")
                    Dim parVideo_StreamID As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_StreamID", DbType.Int64, 0, "StreamID")
                    Dim parVideo_Width As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_Width", DbType.String, 0, "Video_Width")
                    Dim parVideo_Height As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_Height", DbType.String, 0, "Video_Height")
                    Dim parVideo_Codec As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_Codec", DbType.String, 0, "Video_Codec")
                    Dim parVideo_Duration As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_Duration", DbType.String, 0, "Video_Duration")
                    Dim parVideo_ScanType As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_ScanType", DbType.String, 0, "Video_ScanType")
                    Dim parVideo_AspectDisplayRatio As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_AspectDisplayRatio", DbType.String, 0, "Video_AspectDisplayRatio")
                    Dim parVideo_Language As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_Language", DbType.String, 0, "Video_Language")
                    Dim parVideo_LongLanguage As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_LongLanguage", DbType.String, 0, "Video_LongLanguage")
                    Dim parVideo_Bitrate As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_Bitrate", DbType.String, 0, "Video_Bitrate")
                    Dim parVideo_MultiViewCount As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_MultiViewCount", DbType.String, 0, "Video_MultiViewCount")
                    Dim parVideo_FileSize As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_FileSize", DbType.Int64, 0, "Video_FileSize")
                    Dim parVideo_MultiViewLayout As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_MultiViewLayout", DbType.String, 0, "Video_MultiViewLayout")
                    Dim parVideo_StereoMode As SQLiteParameter = SsqlCommand_MoviesVStreams.Parameters.Add("parVideo_StereoMode", DbType.String, 0, "Video_StereoMode")

                    For i As Integer = 0 To dbElement.MainDetails.FileInfo.StreamDetails.Video.Count - 1
                        parVideo_MovieID.Value = dbElement.ID
                        parVideo_StreamID.Value = i
                        parVideo_Width.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Width
                        parVideo_Height.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Height
                        parVideo_Codec.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Codec
                        parVideo_Duration.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Duration
                        parVideo_ScanType.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Scantype
                        parVideo_AspectDisplayRatio.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Aspect
                        parVideo_Language.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Language
                        parVideo_LongLanguage.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).LongLanguage
                        parVideo_Bitrate.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Bitrate
                        parVideo_MultiViewCount.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).MultiViewCount
                        parVideo_MultiViewLayout.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).MultiViewLayout
                        parVideo_FileSize.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Filesize
                        parVideo_StereoMode.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).StereoMode

                        SsqlCommand_MoviesVStreams.ExecuteNonQuery()
                    Next
                End Using
                Using sqlCommand_MoviesAStreams As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_MoviesAStreams.CommandText = String.Concat("DELETE FROM MoviesAStreams WHERE MovieID = ", dbElement.ID, ";")
                    sqlCommand_MoviesAStreams.ExecuteNonQuery()

                    'Expanded SQL Statement to INSERT/replace new fields
                    sqlCommand_MoviesAStreams.CommandText = String.Concat("INSERT OR REPLACE INTO MoviesAStreams (",
                      "MovieID, StreamID, Audio_Language, Audio_LongLanguage, Audio_Codec, Audio_Channel, Audio_Bitrate",
                      ") VALUES (?,?,?,?,?,?,?);")

                    Dim parAudio_MovieID As SQLiteParameter = sqlCommand_MoviesAStreams.Parameters.Add("parAudio_MovieID", DbType.Int64, 0, "MovieID")
                    Dim parAudio_StreamID As SQLiteParameter = sqlCommand_MoviesAStreams.Parameters.Add("parAudio_StreamID", DbType.Int64, 0, "StreamID")
                    Dim parAudio_Language As SQLiteParameter = sqlCommand_MoviesAStreams.Parameters.Add("parAudio_Language", DbType.String, 0, "Audio_Language")
                    Dim parAudio_LongLanguage As SQLiteParameter = sqlCommand_MoviesAStreams.Parameters.Add("parAudio_LongLanguage", DbType.String, 0, "Audio_LongLanguage")
                    Dim parAudio_Codec As SQLiteParameter = sqlCommand_MoviesAStreams.Parameters.Add("parAudio_Codec", DbType.String, 0, "Audio_Codec")
                    Dim parAudio_Channel As SQLiteParameter = sqlCommand_MoviesAStreams.Parameters.Add("parAudio_Channel", DbType.String, 0, "Audio_Channel")
                    Dim parAudio_Bitrate As SQLiteParameter = sqlCommand_MoviesAStreams.Parameters.Add("parAudio_Bitrate", DbType.String, 0, "Audio_Bitrate")

                    For i As Integer = 0 To dbElement.MainDetails.FileInfo.StreamDetails.Audio.Count - 1
                        parAudio_MovieID.Value = dbElement.ID
                        parAudio_StreamID.Value = i
                        parAudio_Language.Value = dbElement.MainDetails.FileInfo.StreamDetails.Audio(i).Language
                        parAudio_LongLanguage.Value = dbElement.MainDetails.FileInfo.StreamDetails.Audio(i).LongLanguage
                        parAudio_Codec.Value = dbElement.MainDetails.FileInfo.StreamDetails.Audio(i).Codec
                        parAudio_Channel.Value = dbElement.MainDetails.FileInfo.StreamDetails.Audio(i).Channels
                        parAudio_Bitrate.Value = dbElement.MainDetails.FileInfo.StreamDetails.Audio(i).Bitrate

                        sqlCommand_MoviesAStreams.ExecuteNonQuery()
                    Next
                End Using

                'subtitles
                Using sqlCommand_MoviesSubs As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_MoviesSubs.CommandText = String.Concat("DELETE FROM MoviesSubs WHERE MovieID = ", dbElement.ID, ";")
                    sqlCommand_MoviesSubs.ExecuteNonQuery()

                    sqlCommand_MoviesSubs.CommandText = String.Concat("INSERT OR REPLACE INTO MoviesSubs (",
                       "MovieID, StreamID, Subs_Language, Subs_LongLanguage,Subs_Type, Subs_Path, Subs_Forced",
                       ") VALUES (?,?,?,?,?,?,?);")
                    Dim parSubs_MovieID As SQLiteParameter = sqlCommand_MoviesSubs.Parameters.Add("parSubs_MovieID", DbType.Int64, 0, "MovieID")
                    Dim parSubs_StreamID As SQLiteParameter = sqlCommand_MoviesSubs.Parameters.Add("parSubs_StreamID", DbType.Int64, 0, "StreamID")
                    Dim parSubs_Language As SQLiteParameter = sqlCommand_MoviesSubs.Parameters.Add("parSubs_Language", DbType.String, 0, "Subs_Language")
                    Dim parSubs_LongLanguage As SQLiteParameter = sqlCommand_MoviesSubs.Parameters.Add("parSubs_LongLanguage", DbType.String, 0, "Subs_LongLanguage")
                    Dim parSubs_Type As SQLiteParameter = sqlCommand_MoviesSubs.Parameters.Add("parSubs_Type", DbType.String, 0, "Subs_Type")
                    Dim parSubs_Path As SQLiteParameter = sqlCommand_MoviesSubs.Parameters.Add("parSubs_Path", DbType.String, 0, "Subs_Path")
                    Dim parSubs_Forced As SQLiteParameter = sqlCommand_MoviesSubs.Parameters.Add("parSubs_Forced", DbType.Boolean, 0, "Subs_Forced")
                    Dim iID As Integer = 0
                    'embedded subtitles
                    For i As Integer = 0 To dbElement.MainDetails.FileInfo.StreamDetails.Subtitle.Count - 1
                        parSubs_MovieID.Value = dbElement.ID
                        parSubs_StreamID.Value = iID
                        parSubs_Language.Value = dbElement.MainDetails.FileInfo.StreamDetails.Subtitle(i).Language
                        parSubs_LongLanguage.Value = dbElement.MainDetails.FileInfo.StreamDetails.Subtitle(i).LongLanguage
                        parSubs_Type.Value = dbElement.MainDetails.FileInfo.StreamDetails.Subtitle(i).Type
                        parSubs_Path.Value = dbElement.MainDetails.FileInfo.StreamDetails.Subtitle(i).Path
                        parSubs_Forced.Value = dbElement.MainDetails.FileInfo.StreamDetails.Subtitle(i).Forced
                        sqlCommand_MoviesSubs.ExecuteNonQuery()
                        iID += 1
                    Next
                    'external subtitles
                    For i As Integer = 0 To dbElement.Subtitles.Count - 1
                        parSubs_MovieID.Value = dbElement.ID
                        parSubs_StreamID.Value = iID
                        parSubs_Language.Value = dbElement.Subtitles(i).Language
                        parSubs_LongLanguage.Value = dbElement.Subtitles(i).LongLanguage
                        parSubs_Type.Value = dbElement.Subtitles(i).Type
                        parSubs_Path.Value = dbElement.Subtitles(i).Path
                        parSubs_Forced.Value = dbElement.Subtitles(i).Forced
                        sqlCommand_MoviesSubs.ExecuteNonQuery()
                        iID += 1
                    Next
                End Using
            End If
        End Using

        'YAMJ watched file
        If dbElement.MainDetails.PlayCountSpecified AndAlso Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
            For Each a In FileUtils.FileNames.Movie(dbElement, Enums.ModifierType.MainWatchedFile)
                If Not File.Exists(a) Then
                    Dim fs As FileStream = File.Create(a)
                    fs.Close()
                End If
            Next
        ElseIf Not dbElement.MainDetails.PlayCountSpecified AndAlso Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
            For Each a In FileUtils.FileNames.Movie(dbElement, Enums.ModifierType.MainWatchedFile)
                If File.Exists(a) Then
                    File.Delete(a)
                End If
            Next
        End If

        If Not batchMode Then sqlTransaction.Commit()

        If doSync Then
            Addons.Instance.RunGeneric(Enums.AddonEventType.Sync_Movie, Nothing, Nothing, False, dbElement)
        End If

        Return dbElement
    End Function
    ''' <summary>
    ''' Saves all information from a Database.DBElement object to the database
    ''' </summary>
    ''' <param name="dbElement">Media.Movie object to save to the database</param>
    ''' <param name="batchMode">Is the function already part of a transaction?</param>
    ''' <param name="toDisk">Create NFO and Images</param>
    ''' <returns>Database.DBElement object</returns>
    Public Function Save_Movieset(ByVal dbElement As DBElement, ByVal batchMode As Boolean, ByVal toNfo As Boolean, ByVal toDisk As Boolean, ByVal doSync As Boolean, ByVal updateMovies As Boolean) As DBElement
        If dbElement.MainDetails Is Nothing Then Return dbElement

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "INSERT OR REPLACE INTO sets ("
            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "idSet,")
            End If

            Dim dbFields As String() = New String() {
                "NfoPath",
                "TMDBColID",
                "Plot",
                "Title",
                "New",
                "Mark",
                "Lock",
                "SortMethod",
                "Language"
            }
            sqlCommand.CommandText = String.Format("{0}{1}) VALUES ({2}",
                                                   sqlCommand.CommandText,
                                                   String.Join(",", dbFields),
                                                   String.Join(",", New String("?"c, dbFields.Count).ToList)
                                                   )

            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, ",?")
                Dim par_idSet As SQLiteParameter = sqlCommand.Parameters.Add("par_idSet", DbType.Int64, 0, "idSet")
                par_idSet.Value = dbElement.ID
            End If
            sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "); SELECT LAST_INSERT_ROWID() FROM sets;")

            Dim par_NfoPath As SQLiteParameter = sqlCommand.Parameters.Add("par_NfoPath", DbType.String, 0, "NfoPath")
            Dim par_TMDBColID As SQLiteParameter = sqlCommand.Parameters.Add("par_TMDBColID", DbType.String, 0, "TMDBColID")
            Dim par_Plot As SQLiteParameter = sqlCommand.Parameters.Add("par_Plot", DbType.String, 0, "Plot")
            Dim par_Title As SQLiteParameter = sqlCommand.Parameters.Add("par_Title", DbType.String, 0, "Title")
            Dim par_New As SQLiteParameter = sqlCommand.Parameters.Add("par_New", DbType.Boolean, 0, "New")
            Dim par_Mark As SQLiteParameter = sqlCommand.Parameters.Add("par_Mark", DbType.Boolean, 0, "Mark")
            Dim par_Lock As SQLiteParameter = sqlCommand.Parameters.Add("par_Lock", DbType.Boolean, 0, "Lock")
            Dim par_SortMethod As SQLiteParameter = sqlCommand.Parameters.Add("par_SortMethod", DbType.Int16, 0, "SortMethod")
            Dim par_Language As SQLiteParameter = sqlCommand.Parameters.Add("par_Language", DbType.String, 0, "Language")

            'First let's save it to NFO, even because we will need the NFO path, also save Images
            'art Table be be linked later
            If toNfo Then NFO.SaveToNFO_Movieset(dbElement)
            If toDisk Then
                dbElement.ImagesContainer.SaveAllImages(dbElement, False)
            End If

            par_NfoPath.Value = dbElement.NfoPath
            par_Language.Value = dbElement.Language

            par_New.Value = Not dbElement.IDSpecified
            par_Mark.Value = dbElement.IsMarked
            par_Lock.Value = dbElement.IsLocked
            par_SortMethod.Value = dbElement.SortMethod

            With dbElement.MainDetails
                par_Plot.Value = .Plot
                par_Title.Value = .Title
                par_TMDBColID.Value = If(.UniqueIDs.TMDbIdSpecified, .UniqueIDs.TMDbId.ToString, String.Empty)
            End With

            If Not dbElement.IDSpecified Then
                If Master.eSettings.MovieSetGeneralMarkNew Then
                    par_Mark.Value = True
                    dbElement.IsMarked = True
                End If
                Using rdrMovieSet As SQLiteDataReader = sqlCommand.ExecuteReader()
                    If rdrMovieSet.Read Then
                        dbElement.ID = Convert.ToInt64(rdrMovieSet(0))
                    Else
                        logger.Error("Something very wrong here: Save_MovieSet", dbElement.ToString)
                        Return dbElement
                    End If
                End Using
            Else
                sqlCommand.ExecuteNonQuery()
            End If
        End Using

        'Images
        Using SQLcommand_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_art.CommandText = String.Format("DELETE FROM art WHERE media_id = {0} AND media_type = 'set';", dbElement.ID)
            SQLcommand_art.ExecuteNonQuery()
        End Using
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Banner.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "set", "banner", dbElement.ImagesContainer.Banner.LocalFilePath)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.ClearArt.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "set", "clearart", dbElement.ImagesContainer.ClearArt.LocalFilePath)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.ClearLogo.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "set", "clearlogo", dbElement.ImagesContainer.ClearLogo.LocalFilePath)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.DiscArt.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "set", "discart", dbElement.ImagesContainer.DiscArt.LocalFilePath)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Fanart.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "set", "fanart", dbElement.ImagesContainer.Fanart.LocalFilePath)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Keyart.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "set", "keyart", dbElement.ImagesContainer.Keyart.LocalFilePath)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Landscape.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "set", "landscape", dbElement.ImagesContainer.Landscape.LocalFilePath)
        If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Poster.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "set", "poster", dbElement.ImagesContainer.Poster.LocalFilePath)

        'UniqueIDs
        Set_UniqueIDsForItem(dbElement.ID, dbElement.ContentType, dbElement.MainDetails.UniqueIDs)

        'save set informations to movies
        If updateMovies Then
            For Each tMovie In dbElement.MoviesInSet
                tMovie.DBMovie.MainDetails.Sets.Add(New MediaContainers.MoviesetDetails With {
                                              .ID = dbElement.ID,
                                              .Order = tMovie.Order,
                                              .Plot = dbElement.MainDetails.Plot,
                                              .Title = dbElement.MainDetails.Title,
                                              .UniqueIDs = dbElement.MainDetails.UniqueIDs
                                              })
                Addons.Instance.RunGeneric(Enums.AddonEventType.BeforeEdit_Movie, Nothing, Nothing, False, tMovie.DBMovie)
                Addons.Instance.RunGeneric(Enums.AddonEventType.AfterEdit_Movie, Nothing, Nothing, False, tMovie.DBMovie)
                Save_Movie(tMovie.DBMovie, True, True, False, True, False)
                RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_Movie, New List(Of Object)(New Object() {tMovie.DBMovie.ID}))
            Next
        End If

        'remove set-information from movies which are no longer assigned to this set
        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Concat("SELECT idMovie, idSet FROM setlinkmovie ",
                                                       "WHERE idSet = ", dbElement.ID, ";")
            Using SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    Dim rMovie = dbElement.MoviesInSet.FirstOrDefault(Function(f) f.DBMovie.ID = Convert.ToInt64(SQLreader("idMovie")))
                    If rMovie Is Nothing Then
                        'movie is no longer a part of this set
                        Dim tMovie As Database.DBElement = Load_Movie(Convert.ToInt64(SQLreader("idMovie")))
                        tMovie.MainDetails.Sets.RemoveSet(dbElement.ID)
                        Addons.Instance.RunGeneric(Enums.AddonEventType.BeforeEdit_Movie, Nothing, Nothing, False, tMovie)
                        Addons.Instance.RunGeneric(Enums.AddonEventType.AfterEdit_Movie, Nothing, Nothing, False, tMovie)
                        Save_Movie(tMovie, True, True, False, True, False)
                        RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_Movie, New List(Of Object)(New Object() {tMovie.ID}))
                    End If
                End While
            End Using
        End Using

        If Not batchMode Then sqlTransaction.Commit()

        Addons.Instance.RunGeneric(Enums.AddonEventType.Sync_MovieSet, Nothing, Nothing, False, dbElement)

        Return dbElement
    End Function

    Public Function Save_Source_Movie(ByVal dbSource As DBSource) As Long
        Dim lngID As Long = -1
        Using sqlTransaction As SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using sqlCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                sqlCommand.CommandText = "INSERT OR REPLACE INTO moviesource ("
                If dbSource.IDSpecified Then
                    lngID = dbSource.ID
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

                If Not dbSource.IDSpecified Then
                    Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                        If sqlReader.Read Then
                            lngID = CLng(sqlReader(0))
                        End If
                    End Using
                Else
                    sqlCommand.ExecuteNonQuery()
                End If
            End Using
            sqlTransaction.Commit()
        End Using
        Return lngID
    End Function

    Public Function Save_Source_TVShow(ByVal dbSource As DBSource) As Long
        Dim lngID As Long = -1
        Using sqlTransaction As SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using sqlCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                sqlCommand.CommandText = "INSERT OR REPLACE INTO tvshowsource ("
                If dbSource.IDSpecified Then
                    lngID = dbSource.ID
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

                If Not dbSource.IDSpecified Then
                    Using sqlReader As SQLiteDataReader = sqlCommand.ExecuteReader()
                        If sqlReader.Read Then
                            lngID = CLng(sqlReader(0))
                        End If
                    End Using
                Else
                    sqlCommand.ExecuteNonQuery()
                End If
            End Using
            sqlTransaction.Commit()
        End Using
        Return lngID
    End Function
    ''' <summary>
    ''' Saves all episode information from a Database.DBElement object to the database
    ''' </summary>
    ''' <param name="dbElement">Database.DBElement object to save to the database</param>
    ''' <param name="doSeasonCheck">If <c>True</c> then check if it's needed to create a new season for this episode</param>
    ''' <param name="batchMode">Is the function already part of a transaction?</param>
    ''' <param name="toDisk">Create NFO and Images</param>
    Public Function Save_TVEpisode(ByVal dbElement As DBElement, ByVal batchMode As Boolean, ByVal toNfo As Boolean, ByVal toDisk As Boolean, ByVal doSeasonCheck As Boolean, ByVal doSync As Boolean, Optional ByVal forceIsNewFlag As Boolean = False) As DBElement
        If dbElement.MainDetails Is Nothing Then Return dbElement

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        'delete to remove "missing" episodes. Only "missing" episodes has to be deleted.
        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = String.Format("DELETE FROM episode WHERE idShow = {0} AND Episode = {1} AND Season = {2} AND idFile = -1;",
                                                   dbElement.ShowID,
                                                   dbElement.MainDetails.Episode,
                                                   dbElement.MainDetails.Season)
            sqlCommand.ExecuteNonQuery()
        End Using

        If dbElement.FilenameSpecified Then
            If dbElement.FilenameIDSpecified Then
                Using sqlCommand_files As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_files.CommandText = String.Concat("INSERT OR REPLACE INTO files (idFile, strFilename) VALUES (?,?);")

                    Dim par_idFile As SQLiteParameter = sqlCommand_files.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
                    Dim par_strFilename As SQLiteParameter = sqlCommand_files.Parameters.Add("par_strFilename", DbType.String, 0, "strFilename")
                    par_idFile.Value = dbElement.FilenameID
                    par_strFilename.Value = dbElement.Filename
                    sqlCommand_files.ExecuteNonQuery()
                End Using
            Else
                Using sqlCommand_files As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_files.CommandText = "SELECT idFile FROM files WHERE strFilename = (?);"

                    Dim par_strFilename As SQLiteParameter = sqlCommand_files.Parameters.Add("par_strFilename", DbType.String, 0, "strFilename")
                    par_strFilename.Value = dbElement.Filename

                    Using sqlReader_files As SQLiteDataReader = sqlCommand_files.ExecuteReader
                        If sqlReader_files.HasRows Then
                            sqlReader_files.Read()
                            dbElement.FilenameID = Convert.ToInt64(sqlReader_files("idFile"))
                        Else
                            Using sqlCommand_Files_add As SQLiteCommand = _myvideosDBConn.CreateCommand()
                                sqlCommand_Files_add.CommandText = String.Concat("INSERT INTO files (",
                                     "strFilename) VALUES (?); SELECT LAST_INSERT_ROWID() FROM files;")
                                Dim parEpPath As SQLiteParameter = sqlCommand_Files_add.Parameters.Add("parEpPath", DbType.String, 0, "strFilename")
                                parEpPath.Value = dbElement.Filename

                                dbElement.FilenameID = Convert.ToInt64(sqlCommand_Files_add.ExecuteScalar)
                            End Using
                        End If
                    End Using
                End Using
            End If
        End If

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "INSERT OR REPLACE INTO episode ("
            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "idEpisode,")
            End If

            Dim dbFields As String() = New String() {
                "idShow",
                "idFile",
                "idSource",
                "New",
                "Mark",
                "Lock",
                "Title",
                "OriginalTitle",
                "Season",
                "Episode",
                "Rating",
                "Plot",
                "Aired",
                "NfoPath",
                "Playcount",
                "DisplaySeason",
                "DisplayEpisode",
                "DateAdded",
                "Runtime",
                "Votes",
                "VideoSource",
                "HasSub",
                "SubEpisode",
                "iLastPlayed",
                "strIMDB",
                "strTMDB",
                "strTVDB",
                "iUserRating",
                "userNote"
            }
            sqlCommand.CommandText = String.Format("{0}{1}) VALUES ({2}",
                                                   sqlCommand.CommandText,
                                                   String.Join(",", dbFields),
                                                   String.Join(",", New String("?"c, dbFields.Count).ToList)
                                                   )

            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, ",?")
                Dim par_idEpisode As SQLiteParameter = sqlCommand.Parameters.Add("par_idEpisode", DbType.Int64, 0, "idEpisode")
                par_idEpisode.Value = dbElement.ID
            End If
            sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "); SELECT LAST_INSERT_ROWID() FROM episode;")

            Dim par_idShow As SQLiteParameter = sqlCommand.Parameters.Add("par_idShow", DbType.Int64, 0, "idShow")
            Dim par_idFile As SQLiteParameter = sqlCommand.Parameters.Add("par_idFile", DbType.Int64, 0, "idFile")
            Dim par_idSource As SQLiteParameter = sqlCommand.Parameters.Add("par_idSource", DbType.Int64, 0, "idSource")
            Dim par_New As SQLiteParameter = sqlCommand.Parameters.Add("par_New", DbType.Boolean, 0, "New")
            Dim par_Mark As SQLiteParameter = sqlCommand.Parameters.Add("par_Mark", DbType.Boolean, 0, "Mark")
            Dim par_Lock As SQLiteParameter = sqlCommand.Parameters.Add("par_Lock", DbType.Boolean, 0, "Lock")
            Dim par_Title As SQLiteParameter = sqlCommand.Parameters.Add("par_Title", DbType.String, 0, "Title")
            Dim par_OriginalTitle As SQLiteParameter = sqlCommand.Parameters.Add("par_OriginalTitle", DbType.String, 0, "OriginalTitle")
            Dim par_Season As SQLiteParameter = sqlCommand.Parameters.Add("par_Season", DbType.String, 0, "Season")
            Dim par_Episode As SQLiteParameter = sqlCommand.Parameters.Add("par_Episode", DbType.String, 0, "Episode")
            Dim par_Rating As SQLiteParameter = sqlCommand.Parameters.Add("par_Rating", DbType.String, 0, "Rating")
            Dim par_Plot As SQLiteParameter = sqlCommand.Parameters.Add("par_Plot", DbType.String, 0, "Plot")
            Dim par_Aired As SQLiteParameter = sqlCommand.Parameters.Add("par_Aired", DbType.String, 0, "Aired")
            Dim par_NfoPath As SQLiteParameter = sqlCommand.Parameters.Add("par_NfoPath", DbType.String, 0, "NfoPath")
            Dim par_Playcount As SQLiteParameter = sqlCommand.Parameters.Add("par_Playcount", DbType.Int64, 0, "Playcount")
            Dim par_DisplaySeason As SQLiteParameter = sqlCommand.Parameters.Add("par_DisplaySeason", DbType.String, 0, "DisplaySeason")
            Dim par_DisplayEpisode As SQLiteParameter = sqlCommand.Parameters.Add("par_DisplayEpisode", DbType.String, 0, "DisplayEpisode")
            Dim par_DateAdded As SQLiteParameter = sqlCommand.Parameters.Add("par_DateAdded", DbType.Int64, 0, "DateAdded")
            Dim par_Runtime As SQLiteParameter = sqlCommand.Parameters.Add("par_Runtime", DbType.String, 0, "Runtime")
            Dim par_Votes As SQLiteParameter = sqlCommand.Parameters.Add("par_Votes", DbType.String, 0, "Votes")
            Dim par_VideoSource As SQLiteParameter = sqlCommand.Parameters.Add("par_VideoSource", DbType.String, 0, "VideoSource")
            Dim par_HasSub As SQLiteParameter = sqlCommand.Parameters.Add("par_HasSub", DbType.Boolean, 0, "HasSub")
            Dim par_SubEpisode As SQLiteParameter = sqlCommand.Parameters.Add("par_SubEpisode", DbType.String, 0, "SubEpisode")
            Dim par_iLastPlayed As SQLiteParameter = sqlCommand.Parameters.Add("par_iLastPlayed", DbType.Int64, 0, "iLastPlayed")
            Dim par_strIMDB As SQLiteParameter = sqlCommand.Parameters.Add("par_strIMDB", DbType.String, 0, "strIMDB")
            Dim par_strTMDB As SQLiteParameter = sqlCommand.Parameters.Add("par_strTMDB", DbType.String, 0, "strTMDB")
            Dim par_strTVDB As SQLiteParameter = sqlCommand.Parameters.Add("par_strTVDB", DbType.String, 0, "strTVDB")
            Dim par_iUserRating As SQLiteParameter = sqlCommand.Parameters.Add("par_iUserRating", DbType.Int64, 0, "iUserRating")
            Dim par_userNote As SQLiteParameter = sqlCommand.Parameters.Add("par_userNote", DbType.String, 0, "userNote")

            Try
                If Not Master.eSettings.GeneralDateAddedIgnoreNFO AndAlso dbElement.MainDetails.DateAddedSpecified Then
                    Dim DateTimeAdded As Date = Date.ParseExact(dbElement.MainDetails.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    par_DateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    Select Case Master.eSettings.GeneralDateTime
                        Case Enums.DateTimeStamp.Now
                            Dim DateTimeAdded As Date
                            If Date.TryParseExact(dbElement.MainDetails.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, DateTimeAdded) Then
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                            Else
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                            End If
                        Case Enums.DateTimeStamp.ctime
                            Dim ctime As Date = File.GetCreationTime(dbElement.Filename)
                            If ctime.Year > 1601 Then
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            Else
                                Dim mtime As Date = File.GetLastWriteTime(dbElement.Filename)
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            End If
                        Case Enums.DateTimeStamp.mtime
                            Dim mtime As Date = File.GetLastWriteTime(dbElement.Filename)
                            If mtime.Year > 1601 Then
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                Dim ctime As Date = File.GetCreationTime(dbElement.Filename)
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                        Case Enums.DateTimeStamp.Newer
                            Dim mtime As Date = File.GetLastWriteTime(dbElement.Filename)
                            Dim ctime As Date = File.GetCreationTime(dbElement.Filename)
                            If mtime > ctime Then
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(mtime)
                            Else
                                par_DateAdded.Value = Functions.ConvertToUnixTimestamp(ctime)
                            End If
                    End Select
                End If
                dbElement.MainDetails.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_DateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            Catch ex As Exception
                Dim DateTimeAdded As Date
                If Date.TryParseExact(dbElement.MainDetails.DateAdded, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, DateTimeAdded) Then
                    par_DateAdded.Value = Functions.ConvertToUnixTimestamp(DateTimeAdded)
                Else
                    par_DateAdded.Value = Functions.ConvertToUnixTimestamp(Date.Now)
                End If
                dbElement.MainDetails.DateAdded = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(par_DateAdded.Value)).ToString("yyyy-MM-dd HH:mm:ss")
            End Try

            Dim DateTimeLastPlayedUnix As Double = -1
            If dbElement.MainDetails.LastPlayedSpecified Then
                Try
                    Dim DateTimeLastPlayed As Date = Date.ParseExact(dbElement.MainDetails.LastPlayed, "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                    DateTimeLastPlayedUnix = Functions.ConvertToUnixTimestamp(DateTimeLastPlayed)
                Catch
                    'Kodi save it only as yyyy-MM-dd, try that
                    Try
                        Dim DateTimeLastPlayed As Date = Date.ParseExact(dbElement.MainDetails.LastPlayed, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture)
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
                dbElement.MainDetails.LastPlayed = String.Empty
            End If

            'First let's save it to NFO, even because we will need the NFO path, also save Images
            'art Table be be linked later
            If dbElement.FilenameIDSpecified Then
                If toNfo Then NFO.SaveToNFO_TVEpisode(dbElement)
                If toDisk Then
                    dbElement.ImagesContainer.SaveAllImages(dbElement, False)
                    dbElement.MainDetails.SaveAllActorThumbs(dbElement)
                End If
            End If

            par_idShow.Value = dbElement.ShowID
            par_NfoPath.Value = dbElement.NfoPath
            par_HasSub.Value = (dbElement.Subtitles IsNot Nothing AndAlso dbElement.Subtitles.Count > 0) OrElse dbElement.MainDetails.FileInfo.StreamDetails.Subtitle.Count > 0
            par_New.Value = forceIsNewFlag OrElse Not dbElement.IDSpecified
            par_Mark.Value = dbElement.IsMarked
            par_idFile.Value = dbElement.FilenameID
            par_Lock.Value = dbElement.IsLocked
            par_idSource.Value = dbElement.Source.ID
            par_VideoSource.Value = dbElement.VideoSource

            With dbElement.MainDetails
                par_Title.Value = .Title
                par_OriginalTitle.Value = .OriginalTitle
                par_Season.Value = .Season
                par_Episode.Value = .Episode
                par_DisplaySeason.Value = .DisplaySeason
                par_DisplayEpisode.Value = .DisplayEpisode
                par_iUserRating.Value = .UserRating
                par_Rating.Value = .Rating
                par_Plot.Value = .Plot
                par_Aired.Value = NumUtils.DateToISO8601Date(.Aired)
                If .PlayCountSpecified Then 'need to be NOTHING instead of "0"
                    par_Playcount.Value = .PlayCount
                End If
                par_Runtime.Value = .Runtime
                par_Votes.Value = .Votes
                If .SubEpisodeSpecified Then
                    par_SubEpisode.Value = .SubEpisode
                End If
                par_strIMDB.Value = If(.UniqueIDs.IMDbIdSpecified, .UniqueIDs.IMDbId, String.Empty)
                par_strTMDB.Value = If(.UniqueIDs.TMDbIdSpecified, .UniqueIDs.TMDbId.ToString, String.Empty)
                par_strTVDB.Value = If(.UniqueIDs.TVDbIdSpecified, .UniqueIDs.TVDbId.ToString, String.Empty)
                par_userNote.Value = .UserNote
            End With

            If Not dbElement.IDSpecified Then
                If Master.eSettings.TVGeneralMarkNewEpisodes Then
                    par_Mark.Value = True
                    dbElement.IsMarked = True
                End If
                Using rdrTVEp As SQLiteDataReader = sqlCommand.ExecuteReader()
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
                sqlCommand.ExecuteNonQuery()
            End If

            If dbElement.IDSpecified Then

                'Actors
                Using sqlCommand_actorlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_actorlink.CommandText = String.Concat("DELETE FROM actorlinkepisode WHERE idEpisode = ", dbElement.ID, ";")
                    sqlCommand_actorlink.ExecuteNonQuery()
                End Using
                Add_Cast(dbElement.ID, "episode", "episode", dbElement.MainDetails.Actors)

                'Directors
                Using sqlCommand_directorlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_directorlink.CommandText = String.Format("DELETE FROM directorlinkepisode WHERE idEpisode = {0};", dbElement.ID)
                    sqlCommand_directorlink.ExecuteNonQuery()
                End Using
                For Each director As String In dbElement.MainDetails.Directors
                    Add_Director_TVEpisode(dbElement.ID, Add_Actor(director, "", "", "", "", False))
                Next

                'Guest Stars
                Using sqlCommand_gueststarlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_gueststarlink.CommandText = String.Concat("DELETE FROM gueststarlinkepisode WHERE idEpisode = ", dbElement.ID, ";")
                    sqlCommand_gueststarlink.ExecuteNonQuery()
                End Using
                Add_GuestStar(dbElement.ID, "episode", "episode", dbElement.MainDetails.GuestStars)

                'Images
                Using sqlCommand_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_art.CommandText = String.Concat("DELETE FROM art WHERE media_id = ", dbElement.ID, " AND media_type = 'episode';")
                    sqlCommand_art.ExecuteNonQuery()
                End Using
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Fanart.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "episode", "fanart", dbElement.ImagesContainer.Fanart.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Poster.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "episode", "thumb", dbElement.ImagesContainer.Poster.LocalFilePath)

                'Ratings
                Set_RatingsForItem(dbElement.ID, dbElement.ContentType, dbElement.MainDetails.Ratings)

                'UniqueIDs
                Set_UniqueIDsForItem(dbElement.ID, dbElement.ContentType, dbElement.MainDetails.UniqueIDs)

                'Writers
                Using sqlCommand_writerlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_writerlink.CommandText = String.Concat("DELETE FROM writerlinkepisode WHERE idEpisode = ", dbElement.ID, ";")
                    sqlCommand_writerlink.ExecuteNonQuery()
                End Using
                For Each writer As String In dbElement.MainDetails.Credits
                    Add_Writer_TVEpisode(dbElement.ID, Add_Actor(writer, "", "", "", "", False))
                Next

                Using sqlCommand_TVVStreams As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_TVVStreams.CommandText = String.Concat("DELETE FROM TVVStreams WHERE TVEpID = ", dbElement.ID, ";")
                    sqlCommand_TVVStreams.ExecuteNonQuery()
                    sqlCommand_TVVStreams.CommandText = String.Concat("INSERT OR REPLACE INTO TVVStreams (",
                       "TVEpID, StreamID, Video_Width, Video_Height, Video_Codec, Video_Duration, Video_ScanType, Video_AspectDisplayRatio,",
                       "Video_Language, Video_LongLanguage, Video_Bitrate, Video_MultiViewCount, Video_FileSize, Video_MultiViewLayout, ",
                       "Video_StereoMode) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);")

                    Dim parVideo_EpID As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_EpID", DbType.Int64, 0, "TVEpID")
                    Dim parVideo_StreamID As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_StreamID", DbType.Int64, 0, "StreamID")
                    Dim parVideo_Width As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_Width", DbType.String, 0, "Video_Width")
                    Dim parVideo_Height As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_Height", DbType.String, 0, "Video_Height")
                    Dim parVideo_Codec As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_Codec", DbType.String, 0, "Video_Codec")
                    Dim parVideo_Duration As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_Duration", DbType.String, 0, "Video_Duration")
                    Dim parVideo_ScanType As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_ScanType", DbType.String, 0, "Video_ScanType")
                    Dim parVideo_AspectDisplayRatio As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_AspectDisplayRatio", DbType.String, 0, "Video_AspectDisplayRatio")
                    Dim parVideo_Language As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_Language", DbType.String, 0, "Video_Language")
                    Dim parVideo_LongLanguage As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_LongLanguage", DbType.String, 0, "Video_LongLanguage")
                    Dim parVideo_Bitrate As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_Bitrate", DbType.String, 0, "Video_Bitrate")
                    Dim parVideo_MultiViewCount As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_MultiViewCount", DbType.String, 0, "Video_MultiViewCount")
                    Dim parVideo_FileSize As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_FileSize", DbType.Int64, 0, "Video_FileSize")
                    Dim parVideo_MultiViewLayout As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_MultiViewLayout", DbType.String, 0, "Video_MultiViewLayout")
                    Dim parVideo_StereoMode As SQLiteParameter = sqlCommand_TVVStreams.Parameters.Add("parVideo_StereoMode", DbType.String, 0, "Video_StereoMode")

                    For i As Integer = 0 To dbElement.MainDetails.FileInfo.StreamDetails.Video.Count - 1
                        parVideo_EpID.Value = dbElement.ID
                        parVideo_StreamID.Value = i
                        parVideo_Width.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Width
                        parVideo_Height.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Height
                        parVideo_Codec.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Codec
                        parVideo_Duration.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Duration
                        parVideo_ScanType.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Scantype
                        parVideo_AspectDisplayRatio.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Aspect
                        parVideo_Language.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Language
                        parVideo_LongLanguage.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).LongLanguage
                        parVideo_Bitrate.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Bitrate
                        parVideo_MultiViewCount.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).MultiViewCount
                        parVideo_MultiViewLayout.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).MultiViewLayout
                        parVideo_FileSize.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).Filesize
                        parVideo_StereoMode.Value = dbElement.MainDetails.FileInfo.StreamDetails.Video(i).StereoMode

                        sqlCommand_TVVStreams.ExecuteNonQuery()
                    Next
                End Using
                Using sqlCommand_TVAStreams As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_TVAStreams.CommandText = String.Concat("DELETE FROM TVAStreams WHERE TVEpID = ", dbElement.ID, ";")
                    sqlCommand_TVAStreams.ExecuteNonQuery()
                    sqlCommand_TVAStreams.CommandText = String.Concat("INSERT OR REPLACE INTO TVAStreams (",
                       "TVEpID, StreamID, Audio_Language, Audio_LongLanguage, Audio_Codec, Audio_Channel, Audio_Bitrate",
                       ") VALUES (?,?,?,?,?,?,?);")

                    Dim parAudio_EpID As SQLiteParameter = sqlCommand_TVAStreams.Parameters.Add("parAudio_EpID", DbType.Int64, 0, "TVEpID")
                    Dim parAudio_StreamID As SQLiteParameter = sqlCommand_TVAStreams.Parameters.Add("parAudio_StreamID", DbType.Int64, 0, "StreamID")
                    Dim parAudio_Language As SQLiteParameter = sqlCommand_TVAStreams.Parameters.Add("parAudio_Language", DbType.String, 0, "Audio_Language")
                    Dim parAudio_LongLanguage As SQLiteParameter = sqlCommand_TVAStreams.Parameters.Add("parAudio_LongLanguage", DbType.String, 0, "Audio_LongLanguage")
                    Dim parAudio_Codec As SQLiteParameter = sqlCommand_TVAStreams.Parameters.Add("parAudio_Codec", DbType.String, 0, "Audio_Codec")
                    Dim parAudio_Channel As SQLiteParameter = sqlCommand_TVAStreams.Parameters.Add("parAudio_Channel", DbType.String, 0, "Audio_Channel")
                    Dim parAudio_Bitrate As SQLiteParameter = sqlCommand_TVAStreams.Parameters.Add("parAudio_Bitrate", DbType.String, 0, "Audio_Bitrate")

                    For i As Integer = 0 To dbElement.MainDetails.FileInfo.StreamDetails.Audio.Count - 1
                        parAudio_EpID.Value = dbElement.ID
                        parAudio_StreamID.Value = i
                        parAudio_Language.Value = dbElement.MainDetails.FileInfo.StreamDetails.Audio(i).Language
                        parAudio_LongLanguage.Value = dbElement.MainDetails.FileInfo.StreamDetails.Audio(i).LongLanguage
                        parAudio_Codec.Value = dbElement.MainDetails.FileInfo.StreamDetails.Audio(i).Codec
                        parAudio_Channel.Value = dbElement.MainDetails.FileInfo.StreamDetails.Audio(i).Channels
                        parAudio_Bitrate.Value = dbElement.MainDetails.FileInfo.StreamDetails.Audio(i).Bitrate

                        sqlCommand_TVAStreams.ExecuteNonQuery()
                    Next
                End Using

                'subtitles
                Using sqlCommand_TVSubs As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_TVSubs.CommandText = String.Concat("DELETE FROM TVSubs WHERE TVEpID = ", dbElement.ID, ";")
                    sqlCommand_TVSubs.ExecuteNonQuery()

                    sqlCommand_TVSubs.CommandText = String.Concat("INSERT OR REPLACE INTO TVSubs (",
                       "TVEpID, StreamID, Subs_Language, Subs_LongLanguage, Subs_Type, Subs_Path, Subs_Forced",
                       ") VALUES (?,?,?,?,?,?,?);")
                    Dim parSubs_EpID As SQLiteParameter = sqlCommand_TVSubs.Parameters.Add("parSubs_EpID", DbType.Int64, 0, "TVEpID")
                    Dim parSubs_StreamID As SQLiteParameter = sqlCommand_TVSubs.Parameters.Add("parSubs_StreamID", DbType.Int64, 0, "StreamID")
                    Dim parSubs_Language As SQLiteParameter = sqlCommand_TVSubs.Parameters.Add("parSubs_Language", DbType.String, 0, "Subs_Language")
                    Dim parSubs_LongLanguage As SQLiteParameter = sqlCommand_TVSubs.Parameters.Add("parSubs_LongLanguage", DbType.String, 0, "Subs_LongLanguage")
                    Dim parSubs_Type As SQLiteParameter = sqlCommand_TVSubs.Parameters.Add("parSubs_Type", DbType.String, 0, "Subs_Type")
                    Dim parSubs_Path As SQLiteParameter = sqlCommand_TVSubs.Parameters.Add("parSubs_Path", DbType.String, 0, "Subs_Path")
                    Dim parSubs_Forced As SQLiteParameter = sqlCommand_TVSubs.Parameters.Add("parSubs_Forced", DbType.Boolean, 0, "Subs_Forced")
                    Dim iID As Integer = 0
                    'embedded subtitles
                    For i As Integer = 0 To dbElement.MainDetails.FileInfo.StreamDetails.Subtitle.Count - 1
                        parSubs_EpID.Value = dbElement.ID
                        parSubs_StreamID.Value = iID
                        parSubs_Language.Value = dbElement.MainDetails.FileInfo.StreamDetails.Subtitle(i).Language
                        parSubs_LongLanguage.Value = dbElement.MainDetails.FileInfo.StreamDetails.Subtitle(i).LongLanguage
                        parSubs_Type.Value = dbElement.MainDetails.FileInfo.StreamDetails.Subtitle(i).Type
                        parSubs_Path.Value = dbElement.MainDetails.FileInfo.StreamDetails.Subtitle(i).Path
                        parSubs_Forced.Value = dbElement.MainDetails.FileInfo.StreamDetails.Subtitle(i).Forced
                        sqlCommand_TVSubs.ExecuteNonQuery()
                        iID += 1
                    Next
                    'external subtitles
                    If dbElement.Subtitles IsNot Nothing Then
                        For i As Integer = 0 To dbElement.Subtitles.Count - 1
                            parSubs_EpID.Value = dbElement.ID
                            parSubs_StreamID.Value = iID
                            parSubs_Language.Value = dbElement.Subtitles(i).Language
                            parSubs_LongLanguage.Value = dbElement.Subtitles(i).LongLanguage
                            parSubs_Type.Value = dbElement.Subtitles(i).Type
                            parSubs_Path.Value = dbElement.Subtitles(i).Path
                            parSubs_Forced.Value = dbElement.Subtitles(i).Forced
                            sqlCommand_TVSubs.ExecuteNonQuery()
                            iID += 1
                        Next
                    End If
                End Using

                If doSeasonCheck Then
                    Using sqlCommand_SeasonCheck As SQLiteCommand = _myvideosDBConn.CreateCommand()
                        sqlCommand_SeasonCheck.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0} AND Season = {1}", dbElement.ShowID, dbElement.MainDetails.Season)
                        Using sqlReader As SQLiteDataReader = sqlCommand_SeasonCheck.ExecuteReader()
                            If Not sqlReader.HasRows Then
                                Dim _season As New DBElement(Enums.ContentType.TVSeason) With {.ShowID = dbElement.ShowID, .MainDetails = New MediaContainers.MainDetails With {.Season = dbElement.MainDetails.Season}}
                                Save_TVSeason(_season, True, False, True)
                            End If
                        End Using
                    End Using
                End If
            End If
        End Using
        If Not batchMode Then sqlTransaction.Commit()

        If dbElement.FilenameIDSpecified AndAlso doSync Then
            Addons.Instance.RunGeneric(Enums.AddonEventType.Sync_TVEpisode, Nothing, Nothing, False, dbElement)
        End If

        Return dbElement
    End Function
    ''' <summary>
    ''' Stores information for a single season to the database
    ''' </summary>
    ''' <param name="dbElement">Database.DBElement representing the season to be stored.</param>
    ''' <param name="batchMode"></param>
    ''' <remarks>Note that this stores the season information, not the individual episodes within that season</remarks>
    Public Function Save_TVSeason(ByRef dbElement As DBElement, ByVal batchMode As Boolean, ByVal toDisk As Boolean, ByVal doSync As Boolean) As DBElement
        If dbElement.MainDetails Is Nothing Then Return dbElement

        Dim lngID As Long = -1

        Using sqlCommand_select_seasons As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand_select_seasons.CommandText = String.Format("SELECT idSeason FROM seasons WHERE idShow = {0} AND Season = {1}", dbElement.ShowID, dbElement.MainDetails.Season)
            Using sqlRerader As SQLiteDataReader = sqlCommand_select_seasons.ExecuteReader()
                While sqlRerader.Read
                    lngID = CLng(sqlRerader("idSeason"))
                    Exit While
                End While
            End Using
        End Using

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "INSERT OR REPLACE INTO seasons ("
            If Not lngID = -1 Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "idSeason,")
            End If

            Dim dbFields As String() = New String() {
                "idShow",
                "Season",
                "Title",
                "Lock",
                "Mark",
                "New",
                "strTVDB",
                "strTMDB",
                "strAired",
                "strPlot"
            }
            sqlCommand.CommandText = String.Format("{0}{1}) VALUES ({2}",
                                                   sqlCommand.CommandText,
                                                   String.Join(",", dbFields),
                                                   String.Join(",", New String("?"c, dbFields.Count).ToList)
                                                   )

            If Not lngID = -1 Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, ",?")
                Dim par_idSeason As SQLiteParameter = sqlCommand.Parameters.Add("par_idSeason", DbType.Int64, 0, "idSeason")
                par_idSeason.Value = dbElement.ID
            End If
            sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "); SELECT LAST_INSERT_ROWID() FROM seasons;")

            Dim par_idShow As SQLiteParameter = sqlCommand.Parameters.Add("par_idShow", DbType.Int64, 0, "idShow")
            Dim par_Season As SQLiteParameter = sqlCommand.Parameters.Add("par_Season", DbType.Int64, 0, "Season")
            Dim par_Title As SQLiteParameter = sqlCommand.Parameters.Add("par_Title", DbType.String, 0, "Title")
            Dim par_Lock As SQLiteParameter = sqlCommand.Parameters.Add("par_Lock", DbType.Boolean, 0, "Lock")
            Dim par_Mark As SQLiteParameter = sqlCommand.Parameters.Add("par_Mark", DbType.Boolean, 0, "Mark")
            Dim par_New As SQLiteParameter = sqlCommand.Parameters.Add("par_New", DbType.Boolean, 0, "New")
            Dim par_strTVDB As SQLiteParameter = sqlCommand.Parameters.Add("par_strTVDB", DbType.String, 0, "strTVDB")
            Dim par_strTMDB As SQLiteParameter = sqlCommand.Parameters.Add("par_strTMDB", DbType.String, 0, "strTMDB")
            Dim par_strAired As SQLiteParameter = sqlCommand.Parameters.Add("par_strAired", DbType.String, 0, "strAired")
            Dim par_strPlot As SQLiteParameter = sqlCommand.Parameters.Add("par_strPlot", DbType.String, 0, "strPlot")

            'First let's save all images
            If toDisk Then dbElement.ImagesContainer.SaveAllImages(dbElement, False)

            par_idShow.Value = dbElement.ShowID
            par_Season.Value = dbElement.MainDetails.Season
            par_Title.Value = dbElement.MainDetails.Title
            par_Lock.Value = dbElement.IsLocked
            par_Mark.Value = dbElement.IsMarked
            par_New.Value = lngID = -1

            With dbElement.MainDetails
                par_strTVDB.Value = If(.UniqueIDs.TVDbIdSpecified, .UniqueIDs.TVDbId.ToString, String.Empty)
                par_strTMDB.Value = If(.UniqueIDs.TMDbIdSpecified, .UniqueIDs.TMDbId.ToString, String.Empty)
                par_strAired.Value = .Aired
                par_strPlot.Value = .Plot
            End With

            lngID = CInt(sqlCommand.ExecuteScalar())
        End Using

        dbElement.ID = lngID

        If dbElement.IDSpecified Then
            'Images
            Using sqlCommand_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
                sqlCommand_art.CommandText = String.Format("DELETE FROM art WHERE media_id = {0} AND media_type = 'season';", dbElement.ID)
                sqlCommand_art.ExecuteNonQuery()
            End Using
            If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Banner.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "season", "banner", dbElement.ImagesContainer.Banner.LocalFilePath)
            If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Fanart.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "season", "fanart", dbElement.ImagesContainer.Fanart.LocalFilePath)
            If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Landscape.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "season", "landscape", dbElement.ImagesContainer.Landscape.LocalFilePath)
            If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Poster.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "season", "poster", dbElement.ImagesContainer.Poster.LocalFilePath)

            'UniqueIDs
            Set_UniqueIDsForItem(dbElement.ID, dbElement.ContentType, dbElement.MainDetails.UniqueIDs)
        End If

        If Not batchMode Then sqlTransaction.Commit()

        If doSync Then
            Addons.Instance.RunGeneric(Enums.AddonEventType.Sync_TVSeason, Nothing, Nothing, False, dbElement)
        End If

        Return dbElement
    End Function
    ''' <summary>
    ''' Saves all show information from a Database.DBElement object to the database
    ''' </summary>
    ''' <param name="dbElement">Database.DBElement object to save to the database</param>
    ''' <param name="batchMode">Is the function already part of a transaction?</param>
    ''' <param name="toDisk">Create NFO and Images</param>
    Public Function Save_TVShow(ByRef dbElement As DBElement, ByVal batchMode As Boolean, ByVal toNfo As Boolean, ByVal toDisk As Boolean, ByVal withEpisodes As Boolean) As DBElement
        If dbElement.MainDetails Is Nothing Then Return dbElement

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "INSERT OR REPLACE INTO tvshow ("
            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "idShow,")
            End If

            Dim dbFields As String() = New String() {
                "idSource",
                "TVShowPath",
                "New",
                "Mark",
                "TVDB",
                "Lock",
                "EpisodeGuide",
                "Plot",
                "Premiered",
                "MPAA",
                "Rating",
                "NfoPath",
                "Language",
                "Ordering",
                "Status",
                "ThemePath",
                "EFanartsPath",
                "Runtime",
                "Title",
                "Votes",
                "EpisodeSorting",
                "SortTitle",
                "strIMDB",
                "strTMDB",
                "strOriginalTitle",
                "iUserRating",
                "Certification",
                "userNote",
                "Tagline"
            }
            sqlCommand.CommandText = String.Format("{0}{1}) VALUES ({2}",
                                                   sqlCommand.CommandText,
                                                   String.Join(",", dbFields),
                                                   String.Join(",", New String("?"c, dbFields.Count).ToList)
                                                   )

            If dbElement.IDSpecified Then
                sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, ",?")
                Dim par_idShowe As SQLiteParameter = sqlCommand.Parameters.Add("par_idShow", DbType.Int64, 0, "idShow")
                par_idShowe.Value = dbElement.ID
            End If
            sqlCommand.CommandText = String.Concat(sqlCommand.CommandText, "); SELECT LAST_INSERT_ROWID() FROM tvshow;")

            Dim par_idSource As SQLiteParameter = sqlCommand.Parameters.Add("par_idSource", DbType.Int64, 0, "idSource")
            Dim par_TVShowPath As SQLiteParameter = sqlCommand.Parameters.Add("par_TVShowPath", DbType.String, 0, "TVShowPath")
            Dim par_New As SQLiteParameter = sqlCommand.Parameters.Add("par_New", DbType.Boolean, 0, "New")
            Dim par_Mark As SQLiteParameter = sqlCommand.Parameters.Add("par_Mark", DbType.Boolean, 0, "Mark")
            Dim par_TVDB As SQLiteParameter = sqlCommand.Parameters.Add("par_TVDB", DbType.String, 0, "TVDB")
            Dim par_Lock As SQLiteParameter = sqlCommand.Parameters.Add("par_Lock", DbType.Boolean, 0, "Lock")
            Dim par_EpisodeGuide As SQLiteParameter = sqlCommand.Parameters.Add("par_EpisodeGuide", DbType.String, 0, "EpisodeGuide")
            Dim par_Plot As SQLiteParameter = sqlCommand.Parameters.Add("par_Plot", DbType.String, 0, "Plot")
            Dim par_Premiered As SQLiteParameter = sqlCommand.Parameters.Add("par_Premiered", DbType.String, 0, "Premiered")
            Dim par_MPAA As SQLiteParameter = sqlCommand.Parameters.Add("par_MPAA", DbType.String, 0, "MPAA")
            Dim par_Rating As SQLiteParameter = sqlCommand.Parameters.Add("par_Rating", DbType.String, 0, "Rating")
            Dim par_NfoPath As SQLiteParameter = sqlCommand.Parameters.Add("par_NfoPath", DbType.String, 0, "NfoPath")
            Dim par_Language As SQLiteParameter = sqlCommand.Parameters.Add("par_Language", DbType.String, 0, "Language")
            Dim par_Ordering As SQLiteParameter = sqlCommand.Parameters.Add("par_Ordering", DbType.Int16, 0, "Ordering")
            Dim par_Status As SQLiteParameter = sqlCommand.Parameters.Add("par_Status", DbType.String, 0, "Status")
            Dim par_ThemePath As SQLiteParameter = sqlCommand.Parameters.Add("par_ThemePath", DbType.String, 0, "ThemePath")
            Dim par_EFanartsPath As SQLiteParameter = sqlCommand.Parameters.Add("par_EFanartsPath", DbType.String, 0, "EFanartsPath")
            Dim par_Runtime As SQLiteParameter = sqlCommand.Parameters.Add("par_Runtime", DbType.String, 0, "Runtime")
            Dim par_Title As SQLiteParameter = sqlCommand.Parameters.Add("par_Title", DbType.String, 0, "Title")
            Dim par_Votes As SQLiteParameter = sqlCommand.Parameters.Add("par_Votes", DbType.String, 0, "Votes")
            Dim par_EpisodeSorting As SQLiteParameter = sqlCommand.Parameters.Add("par_EpisodeSorting", DbType.Int16, 0, "EpisodeSorting")
            Dim par_SortTitle As SQLiteParameter = sqlCommand.Parameters.Add("par_SortTitle", DbType.String, 0, "SortTitle")
            Dim par_strIMDB As SQLiteParameter = sqlCommand.Parameters.Add("par_strIMDB", DbType.String, 0, "strIMDB")
            Dim par_strTMDB As SQLiteParameter = sqlCommand.Parameters.Add("par_strTMDB", DbType.String, 0, "strTMDB")
            Dim par_strOriginalTitle As SQLiteParameter = sqlCommand.Parameters.Add("par_strOriginalTitle", DbType.String, 0, "strOriginalTitle")
            Dim par_iUserRating As SQLiteParameter = sqlCommand.Parameters.Add("par_iUserRating", DbType.Int64, 0, "iUserRating")
            Dim par_Certification As SQLiteParameter = sqlCommand.Parameters.Add("par_Certification", DbType.String, 0, "Certification")
            Dim par_userNote As SQLiteParameter = sqlCommand.Parameters.Add("par_userNote", DbType.String, 0, "userNote")
            Dim par_Tagline As SQLiteParameter = sqlCommand.Parameters.Add("par_Tagline", DbType.String, 0, "Tagline")

            With dbElement.MainDetails
                par_Certification.Value = String.Join(" / ", .Certifications.ToArray)
                par_iUserRating.Value = .UserRating
                par_EpisodeGuide.Value = .EpisodeGuideURL.URL
                par_strIMDB.Value = If(.UniqueIDs.IMDbIdSpecified, .UniqueIDs.IMDbId, String.Empty)
                par_MPAA.Value = .MPAA
                par_strOriginalTitle.Value = .OriginalTitle
                par_Plot.Value = .Plot
                par_Premiered.Value = NumUtils.DateToISO8601Date(.Premiered)
                par_Rating.Value = .Rating
                par_Runtime.Value = .Runtime
                par_SortTitle.Value = .SortTitle
                par_Status.Value = .Status
                par_Tagline.Value = .Tagline
                par_strTMDB.Value = If(.UniqueIDs.TMDbIdSpecified, .UniqueIDs.TMDbId.ToString, String.Empty)
                par_TVDB.Value = If(.UniqueIDs.TVDbIdSpecified, .UniqueIDs.TVDbId.ToString, String.Empty)
                par_Title.Value = .Title
                par_Votes.Value = .Votes
                par_userNote.Value = .UserNote
            End With

            'First let's save it to NFO, even because we will need the NFO path
            'Also Save Images to get ExtrafanartsPath
            'art Table be be linked later
            If toNfo Then NFO.SaveToNFO_TVShow(dbElement)
            If toDisk Then
                dbElement.ImagesContainer.SaveAllImages(dbElement, False)
                dbElement.Theme.Save(dbElement, Enums.ModifierType.MainTheme, False)
                dbElement.MainDetails.SaveAllActorThumbs(dbElement)
            End If

            par_EFanartsPath.Value = dbElement.ExtrafanartsPath
            par_NfoPath.Value = dbElement.NfoPath
            par_ThemePath.Value = If(Not String.IsNullOrEmpty(dbElement.Theme.LocalFilePath), dbElement.Theme.LocalFilePath, String.Empty)
            par_TVShowPath.Value = dbElement.ShowPath

            par_New.Value = Not dbElement.IDSpecified
            par_Mark.Value = dbElement.IsMarked
            par_Lock.Value = dbElement.IsLocked
            par_idSource.Value = dbElement.Source.ID
            par_Language.Value = dbElement.Language
            par_Ordering.Value = dbElement.EpisodeOrdering
            par_EpisodeSorting.Value = dbElement.EpisodeSorting

            If Not dbElement.IDSpecified Then
                If Master.eSettings.TVGeneralMarkNewShows Then
                    par_Mark.Value = True
                    dbElement.IsMarked = True
                End If
                Using rdrTVShow As SQLiteDataReader = sqlCommand.ExecuteReader()
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
                sqlCommand.ExecuteNonQuery()
            End If

            If Not dbElement.ID = -1 Then

                'Actors
                Using sqlCommand_actorlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_actorlink.CommandText = String.Format("DELETE FROM actorlinktvshow WHERE idShow = {0};", dbElement.ID)
                    sqlCommand_actorlink.ExecuteNonQuery()
                End Using
                Add_Cast(dbElement.ID, "tvshow", "show", dbElement.MainDetails.Actors)

                'Creators
                Using sqlCommand_creatorlink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_creatorlink.CommandText = String.Format("DELETE FROM creatorlinktvshow WHERE idShow = {0};", dbElement.ID)
                    sqlCommand_creatorlink.ExecuteNonQuery()
                End Using
                For Each creator As String In dbElement.MainDetails.Creators
                    Add_Creator_TVShow(dbElement.ID, Add_Actor(creator, "", "", "", "", False))
                Next

                'Countries
                Using sqlCommand_countrylink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_countrylink.CommandText = String.Format("DELETE FROM countrylinktvshow WHERE idShow = {0};", dbElement.ID)
                    sqlCommand_countrylink.ExecuteNonQuery()
                End Using
                For Each country As String In dbElement.MainDetails.Countries
                    Add_Country_TVShow(dbElement.ID, Add_Country(country))
                Next

                'Genres
                Using sqlCommand_genrelink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_genrelink.CommandText = String.Format("DELETE FROM genrelinktvshow WHERE idShow = {0};", dbElement.ID)
                    sqlCommand_genrelink.ExecuteNonQuery()
                End Using
                Dim iGenre As Integer = 0
                For Each genre As String In dbElement.MainDetails.Genres
                    Add_Genre_TVShow(dbElement.ID, Add_Genre(genre), iGenre)
                    iGenre += 1
                Next

                'Images
                Using sqlCommand_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_art.CommandText = String.Format("DELETE FROM art WHERE media_id = {0} AND media_type = 'tvshow';", dbElement.ID)
                    sqlCommand_art.ExecuteNonQuery()
                End Using
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Banner.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "tvshow", "banner", dbElement.ImagesContainer.Banner.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.CharacterArt.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "tvshow", "characterart", dbElement.ImagesContainer.CharacterArt.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.ClearArt.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "tvshow", "clearart", dbElement.ImagesContainer.ClearArt.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.ClearLogo.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "tvshow", "clearlogo", dbElement.ImagesContainer.ClearLogo.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Fanart.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "tvshow", "fanart", dbElement.ImagesContainer.Fanart.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Keyart.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "tvshow", "keyart", dbElement.ImagesContainer.Keyart.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Landscape.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "tvshow", "landscape", dbElement.ImagesContainer.Landscape.LocalFilePath)
                If Not String.IsNullOrEmpty(dbElement.ImagesContainer.Poster.LocalFilePath) Then Set_ArtForItem(dbElement.ID, "tvshow", "poster", dbElement.ImagesContainer.Poster.LocalFilePath)

                'Ratings
                Set_RatingsForItem(dbElement.ID, dbElement.ContentType, dbElement.MainDetails.Ratings)

                'Studios
                Using sqlCommand_studiolink As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_studiolink.CommandText = String.Format("DELETE FROM studiolinktvshow WHERE idShow = {0};", dbElement.ID)
                    sqlCommand_studiolink.ExecuteNonQuery()
                End Using
                For Each studio As String In dbElement.MainDetails.Studios
                    Add_Studio_TVShow(dbElement.ID, Add_Studio(studio))
                Next

                'Tags
                Using sqlCommand_taglinks As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    sqlCommand_taglinks.CommandText = String.Format("DELETE FROM taglinks WHERE idMedia = {0} AND media_type = 'tvshow';", dbElement.ID)
                    sqlCommand_taglinks.ExecuteNonQuery()
                End Using
                Dim iTag As Integer = 0
                For Each tag As String In dbElement.MainDetails.Tags
                    Add_TagToItem(dbElement.ID, Add_Tag(tag), "tvshow", iTag)
                    iTag += 1
                Next

                'UniqueIDs
                Set_UniqueIDsForItem(dbElement.ID, dbElement.ContentType, dbElement.MainDetails.UniqueIDs)
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
        If withEpisodes AndAlso dbElement.EpisodesSpecified Then
            For Each nEpisode As DBElement In dbElement.Episodes
                Save_TVEpisode(nEpisode, True, True, True, False, True)
            Next
            Delete_Invalid_TVEpisodes(dbElement.Episodes, dbElement.ID, True)
        End If

        'delete empty seasons after saving all known episodes
        Delete_Empty_TVSeasons(dbElement.ID, True)

        If Not batchMode Then sqlTransaction.Commit()

        Addons.Instance.RunGeneric(Enums.AddonEventType.Sync_TVShow, Nothing, Nothing, False, dbElement)

        Return dbElement
    End Function

    Private Sub Set_ArtForItem(ByVal mediaId As Long, ByVal mediaType As String, ByVal artType As String, ByVal url As String)
        Dim doesExist As Boolean = False
        Dim ID As Long = -1
        Dim oldURL As String = String.Empty

        Using SQLcommand_select_art As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand_select_art.CommandText = String.Format("SELECT art_id, url FROM art WHERE media_id={0} AND media_type='{1}' AND type='{2}'", mediaId, mediaType, artType)
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
                SQLcommand_insert_art.CommandText = String.Format("INSERT INTO art(media_id, media_type, type, url) VALUES ({0}, '{1}', '{2}', ?)", mediaId, mediaType, artType)
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

    Private Sub Set_RatingsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal ratings As MediaContainers.RatingContainer)
        Remove_RatingsFromItem(idMedia, contentType)
        Dim mediaType As String = Convert_ContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As MediaContainers.RatingDetails In ratings.Items
                entry.ID = Add_Rating(idMedia, mediaType, entry)
            Next
        End If
    End Sub

    Private Function Set_MoviesetsForMovie(ByVal dbElement As DBElement, ByRef moviesets As MediaContainers.MoviesetContainer) As Boolean
        Remove_MoviesetFromMovie(dbElement.ID)
        Dim hasChanged As Boolean = False
        If moviesets.Items.Count > 0 Then
            moviesets.Items.Sort()
            Dim iOrder As Integer = 0
            Dim lstMoviesets = GetAll_MoviesetDetails()
            For i As Integer = 0 To moviesets.Items.Count - 1
                If moviesets.Items(i).TitleSpecified Then
                    Dim Initial_Plot As String = moviesets.Items(i).Plot
                    Dim Inital_TMDbId = moviesets.Items(i).UniqueIDs.TMDbId
                    Dim Initial_Title As String = moviesets.Items(i).Title
                    If moviesets.Items(i).IDSpecified Then
                        'use existing movieset based database ID
                        moviesets.Items(i).Order = iOrder
                        Add_LinkToMovieset_Movie(dbElement.ID, moviesets.Items(i).ID, iOrder)
                    ElseIf moviesets.Items(i).UniqueIDs.TMDbIdSpecified AndAlso lstMoviesets.FirstOrDefault(Function(f) f.UniqueIDs.TMDbId = Inital_TMDbId) IsNot Nothing Then
                        'use existing movieset based on title
                        moviesets.Items(i) = lstMoviesets.FirstOrDefault(Function(f) f.UniqueIDs.TMDbId = Inital_TMDbId)
                        moviesets.Items(i).Order = iOrder
                        Add_LinkToMovieset_Movie(dbElement.ID, moviesets.Items(i).ID, iOrder)
                        If Not moviesets.Items(i).Plot = Initial_Plot OrElse
                            Not moviesets.Items(i).UniqueIDs.TMDbId = Inital_TMDbId OrElse
                            Not moviesets.Items(i).Title = Initial_Title Then hasChanged = True
                    ElseIf moviesets.Items(i).TitleSpecified AndAlso lstMoviesets.FirstOrDefault(Function(f) f.Title = Initial_Title) IsNot Nothing Then
                        'use existing movieset based on title
                        moviesets.Items(i) = lstMoviesets.FirstOrDefault(Function(f) f.Title = Initial_Title)
                        moviesets.Items(i).Order = iOrder
                        Add_LinkToMovieset_Movie(dbElement.ID, moviesets.Items(i).ID, iOrder)
                        If Not moviesets.Items(i).Plot = Initial_Plot OrElse
                            Not moviesets.Items(i).UniqueIDs.TMDbId = Inital_TMDbId OrElse
                            Not moviesets.Items(i).Title = Initial_Title Then hasChanged = True
                    Else
                        'create new movieset
                        Dim nDBMovieset As New DBElement(Enums.ContentType.Movieset) With {.MainDetails = moviesets.Items(i)}
                        Save_Movieset(nDBMovieset, True, True, False, False, False)
                        moviesets.Items(i).ID = nDBMovieset.ID
                        moviesets.Items(i).Order = iOrder
                        Add_LinkToMovieset_Movie(dbElement.ID, moviesets.Items(i).ID, iOrder)
                    End If
                    iOrder += 1
                End If
            Next
        End If
        Return hasChanged
    End Function

    Private Sub Set_UniqueIDsForItem(ByVal idMedia As Long, ByVal contentType As Enums.ContentType, ByVal uniqueids As MediaContainers.UniqueidContainer)
        Remove_UniqueIDsFromItem(idMedia, contentType)
        Dim mediaType As String = Convert_ContentTypeToMediaType(contentType)
        If Not String.IsNullOrEmpty(mediaType) Then
            For Each entry As MediaContainers.Uniqueid In uniqueids.Items
                Add_UniqueID(idMedia, mediaType, entry)
            Next
        End If
    End Sub

    Public Function View_Add(ByVal dbCommand As String) As Boolean
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

    Public Function View_Delete(ByVal viewName As String) As Boolean
        If String.IsNullOrEmpty(viewName) Then Return False
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

    Public Function View_Exists(ByVal viewName As String) As Boolean
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

    Public Function View_GetProperty(ByVal viewName As String) As SQLViewProperty
        Dim ViewProperty As New SQLViewProperty
        If Not String.IsNullOrEmpty(viewName) Then
            Try
                Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
                    SQLcommand.CommandText = String.Concat("SELECT name, sql FROM sqlite_master WHERE type ='view' AND name='", viewName, "';")
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

    Public Function View_GetList(ByVal type As Enums.ContentType) As List(Of String)
        Dim ViewList As New List(Of String)
        Dim ContentType As String = String.Empty

        Select Case type
            Case Enums.ContentType.TVEpisode
                ContentType = "episode-"
            Case Enums.ContentType.Movie
                ContentType = "movie-"
            Case Enums.ContentType.Movieset
                ContentType = "sets-"
            Case Enums.ContentType.TVSeason
                ContentType = "seasons-"
            Case Enums.ContentType.TVShow
                ContentType = "tvshow-"
        End Select

        If Not String.IsNullOrEmpty(ContentType) OrElse type = Enums.ContentType.None Then
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

    Public Function View_GetMediaCount(ByVal viewName As String, Optional episodesByView As Boolean = False) As Integer
        Dim mCount As Integer
        If Not String.IsNullOrEmpty(viewName) Then
            Try
                If Not episodesByView Then
                    Using SQLCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand.CommandText = String.Format("SELECT COUNT(*) FROM '{0}'", viewName)
                        mCount = Convert.ToInt32(SQLCommand.ExecuteScalar)
                        Return mCount
                    End Using
                Else
                    Using SQLCommand As SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand.CommandText = String.Format("SELECT COUNT(*) FROM '{0}' INNER JOIN episode ON ('{0}'.idShow = episode.idShow) WHERE NOT episode.idFile = -1", viewName)
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

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
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

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 18
                        Prepare_VotesCount("idEpisode", "episode", True)
                        Prepare_VotesCount("idMovie", "movie", True)
                        Prepare_VotesCount("idShow", "tvshow", True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 21
                        Prepare_SortTitle("tvshow", True)
                        Prepare_DisplayEpisodeSeason(True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 26
                        Prepare_EFanartsPath("idMovie", "movie", True)
                        Prepare_EThumbsPath("idMovie", "movie", True)
                        Prepare_EFanartsPath("idShow", "tvshow", True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 30
                        Prepare_Language("movie", True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 31
                        Prepare_Language("sets", True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 40
                        Prepare_IMDB(True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 41
                        Prepare_Top250(True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 42
                        Prepare_OrphanedLinks(True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 43
                        If MessageBox.Show("Locked state will now be saved in NFO. Do you want to rewrite all NFOs of locked items?", "Rewrite NFOs of locked items", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Prepare_LockedStateToNFO(True)
                        End If
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 44
                        Prepare_Sources("moviesource", True)
                        Prepare_Sources("tvshowsource", True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 45
                        Prepare_AllSeasonsEntries(True)
                End Select

                sqlTransaction.Commit()
            End Using

            Using sqlTransaction As SQLiteTransaction = _myvideosDBConn.BeginTransaction()
                Select Case Args.currVersion
                    Case Is < 48
                        Patch48_wipe_seasontitles(True)
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
    Public Sub Patch_MyVideos(ByVal cPath As String, ByVal nPath As String, ByVal cVersion As Integer, ByVal nVersion As Integer)

        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)

        bwPatchDB = New System.ComponentModel.BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = False
        }
        bwPatchDB.RunWorkerAsync(New Arguments With {.currDBPath = cPath, .currVersion = cVersion, .newDBPath = nPath, .newVersion = nVersion})

        While bwPatchDB.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub Prepare_AllSeasonsEntries(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing ""* All Seasons"" entries...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "UPDATE seasons SET Season = -1 WHERE Season = 999;"
            sqlCommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub PrepareTable_country(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Get countries...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

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
                                    Add_Country_Movie(idMedia, Add_Country(value))
                            End Select
                        Next
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub PrepareTable_director(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Get directors...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

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
                                    Add_Director_TVEpisode(idMedia, Add_Actor(value, "", "", "", "", False))
                                Case "movie"
                                    Add_Director_Movie(idMedia, Add_Actor(value, "", "", "", "", False))
                                Case "tvshow"
                                    Add_Director_TVShow(idMedia, Add_Actor(value, "", "", "", "", False))
                            End Select
                        Next
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub PrepareTable_genre(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Get genres...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

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
                                    AddGenreToMovie(idMedia, Add_Genre(value))
                                Case "tvshow"
                                    AddGenreToTvShow(idMedia, Add_Genre(value))
                            End Select
                        Next
#Enable Warning BC40000 ' The type or member is obsolete.
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub PrepareTable_studio(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Get studios...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

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
                                    Add_Studio_Movie(idMedia, Add_Studio(value))
                                Case "tvshow"
                                    Add_Studio_TVShow(idMedia, Add_Studio(value))
                            End Select
                        Next
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub PrepareTable_writer(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Get writers...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

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
                                    Add_Writer_TVEpisode(idMedia, Add_Actor(value, "", "", "", "", False))
                                Case "movie"
                                    Add_Writer_Movie(idMedia, Add_Actor(value, "", "", "", "", False))
                            End Select
                        Next
                    End If
                End While
            End Using
        End Using

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub Prepare_DisplayEpisodeSeason(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing DisplayEpisode and DisplaySeason...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "UPDATE episode SET DisplayEpisode = -1 WHERE DisplayEpisode IS NULL;"
            SQLcommand.ExecuteNonQuery()
            SQLcommand.CommandText = "UPDATE episode SET DisplaySeason = -1 WHERE DisplaySeason IS NULL;"
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub Prepare_EFanartsPath(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing Extrafanarts Paths...")
        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

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

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub Prepare_EThumbsPath(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing ExtrathumbsPaths...")
        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

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

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub Prepare_Language(ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Set all languages to ""en-US"" ...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("UPDATE {0} SET Language = 'en-US';", table)
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub Prepare_IMDB(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Cleanup all IMDB ID's ...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

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

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub Prepare_LockedStateToNFO(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Rewriting NFOs...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

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

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub Prepare_OrphanedLinks(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Removing orphaned links...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

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

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub Prepare_Playcounts(ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing Playcounts...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = String.Format("UPDATE {0} SET Playcount = NULL WHERE Playcount = 0 OR Playcount = """";", table)
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then sqlTransaction.Commit()
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

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

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

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub Prepare_Top250(ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Fixing Top250...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        Using SQLcommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            SQLcommand.CommandText = "UPDATE movie SET Top250 = NULL WHERE Top250 = 0 OR Top250 = """";"
            SQLcommand.ExecuteNonQuery()
        End Using

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub Prepare_VotesCount(ByVal idField As String, ByVal table As String, ByVal BatchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Clean Votes count...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not BatchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

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

        If Not BatchMode Then sqlTransaction.Commit()
    End Sub

    Private Sub Patch48_wipe_seasontitles(ByVal batchMode As Boolean)
        bwPatchDB.ReportProgress(-1, "Wipe all generic season titles...")

        Dim sqlTransaction As SQLiteTransaction = Nothing
        If Not batchMode Then sqlTransaction = _myvideosDBConn.BeginTransaction()

        Using sqlCommand As SQLiteCommand = _myvideosDBConn.CreateCommand()
            sqlCommand.CommandText = "SELECT idSeason, Season, Title FROM seasons;"
            Using SQLreader As SQLiteDataReader = sqlCommand.ExecuteReader()
                While SQLreader.Read
                    Dim lngID = Convert.ToInt64(SQLreader("idSeason"))
                    Dim intSeason = Convert.ToInt32(SQLreader("Season"))
                    If intSeason = -1 OrElse String.IsNullOrEmpty(StringUtils.FilterSeasonTitle(SQLreader("Title").ToString)) Then
                        Using sqlCommand_update As SQLiteCommand = _myvideosDBConn.CreateCommand()
                            sqlCommand_update.CommandText = String.Format("UPDATE seasons SET Title='' WHERE idSeason={0};", lngID)
                            sqlCommand_update.ExecuteNonQuery()
                        End Using
                    End If
                End While
            End Using
        End Using

        If Not batchMode Then sqlTransaction.Commit()
    End Sub

#End Region 'Database upgrade Methods

#Region "Deprecated Methodes"

    <Obsolete("This method is deprecated and only to use for database upgrade, use AddGenreToItem instead.")>
    Private Sub AddGenreToMovie(ByVal idMovie As Long, ByVal idGenre As Long)
        Add_ToLinkTable("genrelinkmovie", "idGenre", idGenre, "idMovie", idMovie)
    End Sub

    <Obsolete("This method is deprecated and only to use for database upgrade, use AddGenreToItem instead.")>
    Private Sub AddGenreToTvShow(ByVal idShow As Long, ByVal idGenre As Long)
        Add_ToLinkTable("genrelinktvshow", "idGenre", idGenre, "idShow", idShow)
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
                     ColumnName.MoviesInMovieset,
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
                    Return "displaySeason"
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
                Case ColumnName.GetYear
                    Return "getYear"
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
                Case ColumnName.IsSingle
                    Return "isSingle"
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
                Case ColumnName.MoviesInMovieset
                    'shortcut to "idSet"
                    Return GetColumnName(ColumnName.idSet)
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
                Case ColumnName.Role
                    Return "role"
                Case ColumnName.Runtime
                    Return "runtime"
                Case ColumnName.ScanRecursive
                    Return "scanRecursive"
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
                Case ColumnName.UseFolderName
                    Return "useFolderName"
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
                Case TableName.movieset, TableName.movieset_link
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
                Case Enums.ContentType.Source_Movie
                    Return "moviesource"
                Case Enums.ContentType.TVEpisode
                    Return "episodelist"
                Case Enums.ContentType.TVSeason
                    Return "seasonlist"
                Case Enums.ContentType.TVShow
                    Return "tvshowlist"
                Case Enums.ContentType.Source_TVShow
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

#Region "Constructors"

        Public Sub New(ByVal type As Enums.ContentType)
            ContentType = type
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property ActorThumbs As New List(Of String)

        Public ReadOnly Property ActorThumbsSpecified As Boolean
            Get
                Return ActorThumbs.Count > 0
            End Get
        End Property

        Public ReadOnly Property ContentType As Enums.ContentType

        Public Property Edition As String = String.Empty

        Public ReadOnly Property EditionSpecified As Boolean
            Get
                Return Not String.IsNullOrEmpty(Edition)
            End Get
        End Property

        Public Property Episodes As New List(Of DBElement)

        Public ReadOnly Property EpisodesSpecified() As Boolean
            Get
                Return Episodes.Count > 0
            End Get
        End Property

        Public Property EpisodeOrdering As Enums.EpisodeOrdering = Enums.EpisodeOrdering.Standard

        Public Property EpisodeSorting As Enums.EpisodeSorting = Enums.EpisodeSorting.Episode

        Public Property ExtrafanartsPath As String = String.Empty

        Public ReadOnly Property ExtrafanartsPathSpecified As Boolean
            Get
                Return Not String.IsNullOrEmpty(ExtrafanartsPath)
            End Get
        End Property

        Public Property ExtrathumbsPath As String = String.Empty

        Public ReadOnly Property ExtrathumbsPathSpecified As Boolean
            Get
                Return Not String.IsNullOrEmpty(ExtrathumbsPath)
            End Get
        End Property

        Public Property FileItem As FileItem = Nothing

        Public ReadOnly Property FileItemSpecified As Boolean
            Get
                Return FileItem IsNot Nothing AndAlso FileItem.FullPathSpecified
            End Get
        End Property

        Public Property Filename As String = String.Empty

        Public ReadOnly Property FilenameSpecified As Boolean
            Get
                Return Not String.IsNullOrEmpty(Filename)
            End Get
        End Property

        Public Property FilenameID As Long = -1

        Public ReadOnly Property FilenameIDSpecified() As Boolean
            Get
                Return Not FilenameID = -1
            End Get
        End Property

        Public Property ID As Long = -1

        Public ReadOnly Property IDSpecified As Boolean
            Get
                Return Not ID = -1
            End Get
        End Property

        Public Property ImagesContainer As New MediaContainers.ImagesContainer

        Public Property IsLocked As Boolean
            Get
                Return MainDetails.IsLocked
            End Get
            Set(ByVal value As Boolean)
                MainDetails.IsLocked = value
            End Set
        End Property

        Public Property IsMarked As Boolean = False

        Public Property IsMarkCustom1 As Boolean = False

        Public Property IsMarkCustom2 As Boolean = False

        Public Property IsMarkCustom3 As Boolean = False

        Public Property IsMarkCustom4 As Boolean = False

        Public Property IsNew As Boolean = False

        Public Property IsOnline As Boolean = False

        Public Property IsSingle As Boolean = False

        Public Property Language As String = String.Empty

        Public ReadOnly Property LanguageSpecified As Boolean
            Get
                Return Not String.IsNullOrEmpty(Language)
            End Get
        End Property

        Public ReadOnly Property Language_Main As String
            Get
                Return Regex.Replace(Language, "-.*", String.Empty).Trim
            End Get
        End Property

        Public Property MainDetails As New MediaContainers.MainDetails

        Public ReadOnly Property MainDetailsSpecified As Boolean
            Get
                Return MainDetails IsNot Nothing
            End Get
        End Property

        Public Property MoviesInSet As New List(Of MediaContainers.MovieInSet)

        Public ReadOnly Property MoviesInSetSpecified As Boolean
            Get
                Return MoviesInSet.Count > 0
            End Get
        End Property

        Public Property NfoPath As String = String.Empty

        Public ReadOnly Property NfoPathSpecified As Boolean
            Get
                Return Not String.IsNullOrEmpty(NfoPath)
            End Get
        End Property

        Public Property OutOfTolerance As Boolean = False

        Public Property ScrapeModifiers As New Structures.ScrapeModifiers

        Public Property ScrapeOptions As New Structures.ScrapeOptions

        Public Property ScrapeType As Enums.ScrapeType = Enums.ScrapeType.None

        Public Property Seasons As New List(Of DBElement)

        Public ReadOnly Property SeasonsSpecified As Boolean
            Get
                Return Seasons.Count > 0
            End Get
        End Property

        Public Property ShowID As Long = -1

        Public ReadOnly Property ShowIDSpecified As Boolean
            Get
                Return Not ShowID = -1
            End Get
        End Property

        Public Property ShowPath As String = String.Empty

        Public ReadOnly Property ShowPathSpecified As Boolean
            Get
                Return Not String.IsNullOrEmpty(ShowPath)
            End Get
        End Property

        Public Property SortMethod As Enums.SortMethod_MovieSet = Enums.SortMethod_MovieSet.Year

        Public Property Source As New DBSource

        Public ReadOnly Property SourceSpecified As Boolean
            Get
                Return Not Source.ID = -1
            End Get
        End Property

        Public Property Subtitles As New List(Of MediaContainers.Subtitle)

        Public ReadOnly Property SubtitlesSpecified As Boolean
            Get
                Return Subtitles.Count > 0
            End Get
        End Property

        Public Property Theme As New MediaContainers.MediaFile

        Public ReadOnly Property ThemeSpecified As Boolean
            Get
                Return Theme.FileOriginal IsNot Nothing AndAlso Theme.FileOriginal.HasMemoryStream
            End Get
        End Property

        Public Property Trailer As New MediaContainers.MediaFile

        Public ReadOnly Property TrailerSpecified As Boolean
            Get
                Return Trailer.FileOriginal IsNot Nothing AndAlso Trailer.FileOriginal.HasMemoryStream
            End Get
        End Property
        ''' <summary>
        ''' Only to set the TVShow informations for TVEpisodes and TVSeasons elements
        ''' </summary>
        ''' <returns></returns>
        Public Property TVShowDetails As MediaContainers.MainDetails = Nothing

        Public ReadOnly Property TVShowDetailsSpecified As Boolean
            Get
                Return TVShowDetails IsNot Nothing
            End Get
        End Property

        Public Property VideoSource As String = String.Empty

        Public ReadOnly Property VideoSourceSpecified As Boolean
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

        Public Sub Save(ByVal batchMode As Boolean, ByVal toNFO As Boolean, ByVal toDisk As Boolean, ByVal doSync As Boolean, ByVal forced As Boolean)
            Select Case ContentType
                Case Enums.ContentType.Movie
                    Master.DB.Save_Movie(Me, batchMode, toNFO, toDisk, doSync, forced)
                Case Enums.ContentType.Movieset
                Case Enums.ContentType.TVEpisode
                Case Enums.ContentType.TVSeason
                Case Enums.ContentType.TVShow
            End Select
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

    <Serializable>
    Public Class TagContainer

#Region "Constructors"

        Public Sub New()
            Movies.Columns.Add("id", GetType(Long))
            Movies.Columns.Add("Title", GetType(String))
            Movies.Columns.Add("State", GetType(String))
            Movies.Columns("State").DefaultValue = "Unchanged"
            Movies.PrimaryKey = {Movies.Columns("id")}
            TVShows.Columns.Add("id", GetType(Long))
            TVShows.Columns.Add("Title", GetType(String))
            TVShows.Columns.Add("State", GetType(String))
            TVShows.Columns("State").DefaultValue = "Unchanged"
            TVShows.PrimaryKey = {TVShows.Columns("id")}
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Id As Long = -1

        Public Property Name As String = String.Empty

        Public Property Movies As DataTable = New DataTable

        Public Property TVShows As DataTable = New DataTable

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class