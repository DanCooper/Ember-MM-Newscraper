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
Imports EmberAPI
Imports XBMCRPC
Imports System.IO

Namespace Kodi

    Public Class APIKodi

#Region "Fields"

        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
        'current selected host, Kodi Host type already declared in EmberAPI (XML serialization) -> no MySettings declaration needed here
        Private _currenthost As New EmberAPI.Host
        'current selected client
        Private _kodi As XBMCRPC.Client
        'helper object, needed for communication client (notification, eventhandler support)
        Private platformServices As IPlatformServices = New PlatformServices
        'Private NotificationsEnabled As Boolean

#End Region 'Fields

#Region "Methods"
        ''' <summary>
        ''' Initialize Communication Client for ONE Kodi Host
        ''' </summary>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Sub New(ByVal host As EmberAPI.Host)
            _currenthost = Nothing
            _currenthost = host
            Init()
        End Sub
        ''' <summary>
        ''' Initialize API class (host)
        ''' </summary>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Friend Sub Init()
            'dispose old client before initalizing new ones (just to be safe)
            If _kodi IsNot Nothing Then
                _kodi.Dispose()
            End If
            'now initialize new client object
            _kodi = New Client(platformServices, _currenthost.address, _currenthost.port, _currenthost.username, _currenthost.password)
        End Sub

#Region "Movie API"
        ''' <summary>
        ''' Get all movies from Kodi host
        ''' </summary>
        ''' <returns>list of kodi movies, Nothing: error</returns>
        ''' <remarks></remarks>
        Public Async Function GetAllMovies() As Task(Of VideoLibrary.GetMoviesResponse)
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] GetAllMovies: No client initialized! Abort!")
                Return Nothing
            End If
            Dim response = Await _kodi.VideoLibrary.GetMovies(Video.Fields.Movie.AllFields).ConfigureAwait(False)
            Return response
        End Function
        ''' <summary>
        ''' Get all movies by a given filename
        ''' </summary>
        ''' <param name="RemoteFilename"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Async Function GetAllMoviesByFilename(ByVal RemoteFilename As String) As Task(Of VideoLibrary.GetMoviesResponse)
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] GetAllMoviesByFilename: No client initialized! Abort!")
                Return Nothing
            End If

            If Not String.IsNullOrEmpty(RemoteFilename) Then
                Try
                    Dim filter As New XBMCRPC.List.Filter.MoviesOr With {.or = New List(Of Object)}
                    Dim filterRule As New XBMCRPC.List.Filter.Rule.Movies
                    filterRule.field = List.Filter.Fields.Movies.filename
                    filterRule.Operator = List.Filter.Operators.endswith
                    filterRule.value = RemoteFilename
                    filter.or.Add(filterRule)

                    Dim response = Await _kodi.VideoLibrary.GetMovies(filter, Video.Fields.Movie.AllFields).ConfigureAwait(False)
                    Return response
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                    Return Nothing
                End Try
            Else
                Return Nothing
            End If
        End Function
        ''' <summary>
        ''' Get all movies by a given path
        ''' </summary>
        ''' <param name="RemotePath"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Async Function GetAllMoviesByPath(ByVal RemotePath As String) As Task(Of VideoLibrary.GetMoviesResponse)
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] GetAllMoviesByPath: No client initialized! Abort!")
                Return Nothing
            End If

            If Not String.IsNullOrEmpty(RemotePath) Then
                Try
                    Dim filter As New XBMCRPC.List.Filter.MoviesOr With {.or = New List(Of Object)}
                    Dim filterRule As New XBMCRPC.List.Filter.Rule.Movies
                    filterRule.field = List.Filter.Fields.Movies.path
                    filterRule.Operator = List.Filter.Operators.startswith
                    filterRule.value = RemotePath
                    filter.or.Add(filterRule)

                    Dim response = Await _kodi.VideoLibrary.GetMovies(filter, Video.Fields.Movie.AllFields).ConfigureAwait(False)
                    Return response
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                    Return Nothing
                End Try
            Else
                Return Nothing
            End If
        End Function
        ''' <summary>
        ''' Search a movie by Filename
        ''' </summary>
        ''' <param name="Filename">Filename with extension</param>
        ''' <returns></returns>
        ''' <remarks>Don't use it with non-unique filenames like "VIDEO_TS.*"</remarks>
        Public Async Function SearchMovieByFilename(ByVal Filename As String) As Task(Of XBMCRPC.Video.Details.Movie)
            'get a list of all movies saved in Kodi DB by Filename
            Dim kMovies As VideoLibrary.GetMoviesResponse = Await GetAllMoviesByFilename(Filename).ConfigureAwait(False)

            If kMovies.movies IsNot Nothing Then
                If kMovies.movies.Count = 1 Then
                    logger.Trace(String.Concat("[APIKodi] SearchMovieByFilename: " & _currenthost.name & ": """ & Filename & """ found in host database! [ID:", kMovies.movies.Item(0).movieid, "]"))
                    Return kMovies.movies.Item(0)
                ElseIf kMovies.movies.Count > 1 Then
                    logger.Warn(String.Concat("[APIKodi] SearchMovieByFilename: " & _currenthost.name & ": """ & Filename & """ MORE THAN ONE movie found in host database!"))
                    'TODO: add some comparisons to find the correct movie like:
                    'path, IMDB ID, year ...
                    Return Nothing
                End If
            End If

            logger.Trace(String.Concat("[APIKodi] SearchMovieByFilename: " & _currenthost.name & ": """ & Filename & """ NOT found in host database!"))
            Return Nothing
        End Function
        ''' <summary>
        ''' Search a movie by path
        ''' </summary>
        ''' <param name="LocalPath">Full parent path of video file</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Async Function SearchMovieByPath(ByVal LocalPath As String) As Task(Of XBMCRPC.Video.Details.Movie)
            Dim RemotePath As String = GetRemotePath(LocalPath)

            'get a list of all movies saved in Kodi DB by Path
            Dim kMovies As VideoLibrary.GetMoviesResponse = Await GetAllMoviesByPath(RemotePath).ConfigureAwait(False)

            If kMovies.movies IsNot Nothing Then
                If kMovies.movies.Count = 1 Then
                    logger.Trace(String.Concat("[APIKodi] SearchMovieByPath: " & _currenthost.name & ": """ & LocalPath & """ found in host database! [ID:", kMovies.movies.Item(0).movieid, "]"))
                    Return kMovies.movies.Item(0)
                ElseIf kMovies.movies.Count > 1 Then
                    logger.Warn(String.Concat("[APIKodi] SearchMovieByPath: " & _currenthost.name & ": """ & LocalPath & """ MORE THAN ONE movie found in host database!"))
                    'TODO: add some comparisons to find the correct movie like:
                    'path, IMDB ID, year ...
                    'or try to search movie by filename
                    Return Nothing
                End If
            End If

            logger.Trace(String.Concat("[APIKodi] SearchMovieByPath: " & _currenthost.name & ": """ & LocalPath & """ NOT found in host database!"))
            Return Nothing
        End Function
        ''' <summary>
        ''' Update movie details at Kodi
        ''' </summary>
        ''' <param name="EmbermovieID">ID of specific movie (EmberDB)</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Update successfull, false=error or movie not found in KodiDB</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation, main code by DanCooper
        ''' updates all movie fields which are filled/set in Ember (also paths of images)
        ''' at the moment the movie to update on host is identified by searching and comparing filename of movie(special handling for DVDs/Blurays), meaning there might be problems when filename is appearing more than once in movie library
        ''' </remarks>
        Public Async Function UpdateMovieInfo(ByVal EmbermovieID As Long, ByVal SendHostNotification As Boolean) As Task(Of Boolean)
            Dim isNew As Boolean = False
            Dim uMovie As Structures.DBMovie = Master.DB.LoadMovieFromDB(EmbermovieID)
            Try
                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] UpdateMovieInfo: No client initialized! Abort!")
                    Return False
                End If
                ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Start Syncing") & ": " & uMovie.Filename, New Bitmap(My.Resources.logo)}))

                'search movie ID in Kodi DB
                Dim KodiID As Integer = -1
                Dim KodiMovie As XBMCRPC.Video.Details.Movie = Await SearchMovieByPath(Directory.GetParent(uMovie.Filename).FullName).ConfigureAwait(False)
                If KodiMovie Is Nothing Then
                    KodiMovie = Await SearchMovieByFilename(uMovie.Filename).ConfigureAwait(False)
                End If
                If KodiMovie IsNot Nothing Then
                    KodiID = KodiMovie.movieid
                End If

                'movie isn't in database of host -> scan directory
                If KodiID = -1 Then
                    logger.Warn("[APIKodi] UpdateMovieInfo: " & _currenthost.name & ": " & uMovie.Filename & ": Not found in database, scan directory...")
                    Await ScanVideoPath(EmbermovieID, Enums.Content_Type.Movie).ConfigureAwait(False)
                    'wait a bit before trying going on, as scan might take a while on Kodi...
                    Threading.Thread.Sleep(1000) 'TODO better solution for this?!
                    KodiMovie = Await SearchMovieByPath(Directory.GetParent(uMovie.Filename).FullName).ConfigureAwait(False)
                    If KodiMovie IsNot Nothing Then
                        isNew = True
                        KodiID = KodiMovie.movieid
                    End If
                End If


                If KodiID > -1 Then
                    'string or string.empty
                    Dim mDateAdded As String = If(uMovie.Movie.DateAddedSpecified, uMovie.Movie.DateAdded, Nothing)
                    Dim mImdbnumber As String = uMovie.Movie.ID
                    Dim mLastPlayed As String = If(uMovie.Movie.LastPlayedSpecified, uMovie.Movie.LastPlayed, Nothing)
                    Dim mMPAA As String = uMovie.Movie.MPAA
                    Dim mOriginalTitle As String = uMovie.Movie.OriginalTitle
                    Dim mOutline As String = uMovie.Movie.Outline
                    Dim mPlot As String = uMovie.Movie.Plot
                    Dim mSet As String = If(uMovie.Movie.Sets.Count > 0, uMovie.Movie.Sets.Item(0).Title, String.Empty)
                    Dim mSortTitle As String = uMovie.Movie.SortTitle
                    Dim mTagline As String = uMovie.Movie.Tagline
                    Dim mTitle As String = uMovie.Movie.Title
                    Dim mTrailer As String = If(Not String.IsNullOrEmpty(uMovie.TrailerPath), GetRemotePath(uMovie.TrailerPath), If(uMovie.Movie.TrailerSpecified, uMovie.Movie.Trailer, String.Empty))
                    If mTrailer Is Nothing Then
                        mTrailer = String.Empty
                    End If

                    'digit grouping symbol for Votes count
                    Dim mVotes As String = If(Not String.IsNullOrEmpty(uMovie.Movie.Votes), uMovie.Movie.Votes, Nothing)
                    If Master.eSettings.GeneralDigitGrpSymbolVotes Then
                        If uMovie.Movie.VotesSpecified Then
                            Dim vote As String = Double.Parse(uMovie.Movie.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                            If vote IsNot Nothing Then
                                mVotes = vote
                            End If
                        End If
                    End If

                    'integer or 0
                    Dim mRating As Double = If(uMovie.Movie.RatingSpecified, CType(Double.Parse(uMovie.Movie.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), Double), 0)
                    Dim mRuntime As Integer = If(uMovie.Movie.RuntimeSpecified, CType(uMovie.Movie.Runtime, Integer), 0) * 60 'API requires runtime in seconds
                    Dim mTop250 As Integer = If(uMovie.Movie.Top250Specified, CType(uMovie.Movie.Top250, Integer), 0)
                    Dim mYear As Integer = If(uMovie.Movie.YearSpecified, CType(uMovie.Movie.Year, Integer), 0)

                    'Nullable(Of Integer)types. Following values can be integer or set to Nothing if not filled (nullable integer type here because otherwise default value would always be "0" which is not correct)
                    Dim mPlaycount As Integer? = If(uMovie.Movie.PlayCountSpecified, CType(uMovie.Movie.PlayCount, Integer?), New Integer?)


                    'arrays
                    'Countries
                    Dim mCountryList As List(Of String) = If(uMovie.Movie.CountriesSpecified, uMovie.Movie.Countries, New List(Of String))

                    'Directors
                    Dim mDirectorList As List(Of String) = If(uMovie.Movie.DirectorsSpecified, uMovie.Movie.Directors, New List(Of String))

                    'Genres
                    Dim mGenreList As List(Of String) = If(uMovie.Movie.GenresSpecified, uMovie.Movie.Genres, New List(Of String))

                    'Studios
                    Dim mStudioList As List(Of String) = If(uMovie.Movie.StudiosSpecified, uMovie.Movie.Studios, New List(Of String))

                    'Tags
                    Dim mTagList As List(Of String) = If(uMovie.Movie.TagsSpecified, uMovie.Movie.Tags, New List(Of String))

                    'Writers (Credits)
                    Dim mWriterList As List(Of String) = If(uMovie.Movie.CreditsSpecified, uMovie.Movie.Credits, New List(Of String))


                    'string or null/nothing
                    Dim mBanner As String = If(Not String.IsNullOrEmpty(uMovie.BannerPath), _
                                                  GetRemotePath(uMovie.BannerPath), Nothing)
                    Dim mClearArt As String = If(Not String.IsNullOrEmpty(uMovie.ClearArtPath), _
                                                  GetRemotePath(uMovie.ClearArtPath), Nothing)
                    Dim mClearLogo As String = If(Not String.IsNullOrEmpty(uMovie.ClearLogoPath), _
                                                  GetRemotePath(uMovie.ClearLogoPath), Nothing)
                    Dim mDiscArt As String = If(Not String.IsNullOrEmpty(uMovie.DiscArtPath), _
                                                  GetRemotePath(uMovie.DiscArtPath), Nothing)
                    Dim mFanart As String = If(Not String.IsNullOrEmpty(uMovie.FanartPath), _
                                                 GetRemotePath(uMovie.FanartPath), Nothing)
                    Dim mLandscape As String = If(Not String.IsNullOrEmpty(uMovie.LandscapePath), _
                                                  GetRemotePath(uMovie.LandscapePath), Nothing)
                    Dim mPoster As String = If(Not String.IsNullOrEmpty(uMovie.PosterPath), _
                                                  GetRemotePath(uMovie.PosterPath), Nothing)

                    'all image paths will be set in artwork object
                    Dim artwork As New Media.Artwork.Set
                    artwork.banner = mBanner
                    artwork.clearart = mClearArt
                    artwork.clearlogo = mClearLogo
                    artwork.discart = mDiscArt
                    artwork.fanart = mFanart
                    artwork.landscape = mLandscape
                    artwork.poster = mPoster
                    'artwork.thumb = mPoster ' not supported in Ember?!

                    Dim response = Await _kodi.VideoLibrary.SetMovieDetails(KodiID, _
                                                                        title:=mTitle, _
                                                                        playcount:=mPlaycount, _
                                                                        runtime:=mRuntime, _
                                                                        director:=mDirectorList, _
                                                                        studio:=mStudioList, _
                                                                        year:=mYear, _
                                                                        plot:=mPlot, _
                                                                        genre:=mGenreList, _
                                                                        rating:=mRating, _
                                                                        mpaa:=mMPAA, _
                                                                        imdbnumber:=mImdbnumber, _
                                                                        votes:=mVotes, _
                                                                        lastplayed:=mLastPlayed, _
                                                                        originaltitle:=mOriginalTitle, _
                                                                        trailer:=mTrailer, _
                                                                        tagline:=mTagline, _
                                                                        plotoutline:=mOutline, _
                                                                        writer:=mWriterList, _
                                                                        country:=mCountryList, _
                                                                        top250:=mTop250, _
                                                                        sorttitle:=mSortTitle, _
                                                                        set:=mSet, _
                                                                        tag:=mTagList, _
                                                                        art:=artwork).ConfigureAwait(False)
                    'not supported right now in Ember
                    'showlink:=mshowlink, _     
                    'thumbnail:=mposter, _
                    'fanart:=mFanart, _
                    'resume:=mresume, _
                    ' dateadded:=mdateAdded, _

                    If response.Contains("error") Then
                        logger.Error("[APIKodi] UpdateMovieInfo: " & _currenthost.name & ": " & response)
                        ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & uMovie.Filename, Nothing}))
                        Return False
                    Else
                        logger.Trace("[APIKodi] UpdateMovieInfo: " & _currenthost.name & ": " & If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": """ & uMovie.Filename, """")
                        ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync OK") & ": " & uMovie.Filename, New Bitmap(My.Resources.logo)}))

                        'Remove old textures (cache)
                        Dim resTextures = Await RemoveTextures(Directory.GetParent(uMovie.NfoPath).FullName)

                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await SendMessage("Ember Media Manager", If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & uMovie.Movie.Title).ConfigureAwait(False)
                        End If
                        Return True
                    End If
                Else
                    logger.Trace("[APIKodi] UpdateMovieInfo: " & _currenthost.name & ": " & Master.eLang.GetString(1453, "Not Found On Host") & ": " & uMovie.Filename, """")
                    '   ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Not Found On Host") & ": " & uMovie.Filename, Nothing}))
                    Return False
                End If

            Catch ex As Exception
                ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & uMovie.Filename, Nothing}))
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function

#End Region 'Movie API

#Region "Movieset API"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Async Function GetAllMovieSets() As Task(Of VideoLibrary.GetMovieSetsResponse)
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] GetAllMovieSets: No client initialized! Abort!")
                Return Nothing
            End If

            Try
                Dim response = Await _kodi.VideoLibrary.GetMovieSets(Video.Fields.MovieSet.AllFields).ConfigureAwait(False)
                Return response
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="DBMovieSet"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Async Function SearchMovieSetByDetails(ByVal DBMovieSet As Structures.DBMovieSet) As Task(Of XBMCRPC.Video.Details.MovieSet)
            'get a list of all moviesets saved in Kodi DB
            Dim kMovies As VideoLibrary.GetMovieSetsResponse = Await GetAllMovieSets().ConfigureAwait(False)

            If kMovies.sets IsNot Nothing AndAlso kMovies.sets.Count > 0 Then
                'compare by movieset title
                For Each tMovieSet In kMovies.sets
                    If tMovieSet.title.ToLower = DBMovieSet.MovieSet.Title.ToLower Then
                        logger.Trace(String.Concat("[APIKodi] SearchMovieSetByDetails: " & _currenthost.name & ": """ & DBMovieSet.MovieSet.Title & """ found in host database! [ID:", tMovieSet.setid, "]"))
                        Return tMovieSet
                    End If
                Next

                'compare by movies inside movieset
                For Each tMovie In DBMovieSet.Movies
                    'search movie ID in Kodi DB
                    Dim MovieID As Integer = -1
                    Dim KodiMovie = Await SearchMovieByPath(Directory.GetParent(tMovie.Filename).FullName).ConfigureAwait(False)
                    If KodiMovie Is Nothing Then
                        KodiMovie = Await SearchMovieByFilename(tMovie.Filename).ConfigureAwait(False)
                    End If
                    If KodiMovie IsNot Nothing Then
                        For Each tMovieSet In kMovies.sets
                            If tMovieSet.setid = KodiMovie.setid Then
                                logger.Trace(String.Concat("[APIKodi] SearchMovieSetByDetails: " & _currenthost.name & ": """ & DBMovieSet.MovieSet.Title & """ found in host database by movie """, KodiMovie.title, """! [ID:", tMovieSet.setid, "]"))
                                Return tMovieSet
                            End If
                        Next
                    End If
                Next
            End If

            logger.Trace(String.Concat("[APIKodi] SearchMovieSetByDetails: " & _currenthost.name & ": """ & DBMovieSet.MovieSet.Title & """ NOT found in host database!"))
            Return Nothing
        End Function
        ''' <summary>
        ''' Update movieset details at Kodi
        ''' </summary>
        ''' <param name="EmbermoviesetID">ID of specific movieset (EmberDB)</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Update successfull, false=error or movieset not found in KodiDB</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation, main code by DanCooper
        ''' updates all movieset fields which are filled/set in Ember (also paths of images)
        ''' </remarks>
        Public Async Function UpdateMovieSetInfo(ByVal EmbermoviesetID As Long, ByVal MovieSetArtworkPath As String, ByVal SendHostNotification As Boolean) As Task(Of Boolean)
            Dim isNew As Boolean = False
            Dim uMovieset As Structures.DBMovieSet = Master.DB.LoadMovieSetFromDB(EmbermoviesetID)
            Try
                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] UpdateMovieSetInfo: No client initialized! Abort!")
                    Return False
                End If
                ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Start Syncing") & ": " & uMovie.Filename, New Bitmap(My.Resources.logo)}))

                'search movieset ID in Kodi DB
                Dim KodiMovieset As XBMCRPC.Video.Details.MovieSet = Await SearchMovieSetByDetails(uMovieset).ConfigureAwait(False)
                Dim KodiID As Integer = -1
                If Not KodiMovieset Is Nothing Then
                    KodiID = KodiMovieset.setid
                End If

                'movieset isn't in database of host -> scan directory
                If KodiID = -1 Then
                    logger.Warn("[APIKodi] UpdateMovieSetInfo: " & _currenthost.name & ": """ & uMovieset.MovieSet.Title & """ NOT found in database, abort process...")
                    'what to do in this case?
                End If

                If KodiID > -1 Then
                    'string or string.empty
                    Dim mTitle As String = uMovieset.MovieSet.Title

                    'string or null/nothing
                    Dim mBanner As String = If(Not String.IsNullOrEmpty(uMovieset.BannerPath), _
                                                  GetRemoteFilePath(uMovieset.BannerPath, MovieSetArtworkPath), Nothing)
                    Dim mClearArt As String = If(Not String.IsNullOrEmpty(uMovieset.ClearArtPath), _
                                                  GetRemoteFilePath(uMovieset.ClearArtPath, MovieSetArtworkPath), Nothing)
                    Dim mClearLogo As String = If(Not String.IsNullOrEmpty(uMovieset.ClearLogoPath), _
                                                  GetRemoteFilePath(uMovieset.ClearLogoPath, MovieSetArtworkPath), Nothing)
                    Dim mDiscArt As String = If(Not String.IsNullOrEmpty(uMovieset.DiscArtPath), _
                                                  GetRemoteFilePath(uMovieset.DiscArtPath, MovieSetArtworkPath), Nothing)
                    Dim mFanart As String = If(Not String.IsNullOrEmpty(uMovieset.FanartPath), _
                                                 GetRemoteFilePath(uMovieset.FanartPath, MovieSetArtworkPath), Nothing)
                    Dim mLandscape As String = If(Not String.IsNullOrEmpty(uMovieset.LandscapePath), _
                                                  GetRemoteFilePath(uMovieset.LandscapePath, MovieSetArtworkPath), Nothing)
                    Dim mPoster As String = If(Not String.IsNullOrEmpty(uMovieset.PosterPath), _
                                                  GetRemoteFilePath(uMovieset.PosterPath, MovieSetArtworkPath), Nothing)

                    'all image paths will be set in artwork object
                    Dim artwork As New Media.Artwork.Set
                    artwork.banner = mBanner
                    artwork.clearart = mClearArt
                    artwork.clearlogo = mClearLogo
                    artwork.discart = mDiscArt
                    artwork.fanart = mFanart
                    artwork.landscape = mLandscape
                    artwork.poster = mPoster

                    Dim response = Await _kodi.VideoLibrary.SetMovieSetDetails(KodiID, _
                                                                        title:=mTitle, _
                                                                        art:=artwork).ConfigureAwait(False)


                    If response.Contains("error") Then
                        logger.Error("[APIKodi] UpdateMovieSetInfo: " & _currenthost.name & ": " & response)
                        ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & uMovie.Filename, Nothing}))
                        Return False
                    Else
                        logger.Trace("[APIKodi] UpdateMovieSetInfo: " & _currenthost.name & ": " & If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": """ & uMovieset.MovieSet.Title, """")
                        ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync OK") & ": " & uMovie.Filename, New Bitmap(My.Resources.logo)}))

                        'Remove old textures (cache) 'TODO: Limit to images of this MovieSet
                        Dim resTextures = Await RemoveTextures(MovieSetArtworkPath)

                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await SendMessage("Ember Media Manager", If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & uMovieset.MovieSet.Title).ConfigureAwait(False)
                        End If
                        Return True
                    End If
                Else
                    logger.Trace("[APIKodi] UpdateMovieSetInfo: " & _currenthost.name & ": " & Master.eLang.GetString(1453, "Not Found On Host") & ": """ & uMovieset.MovieSet.Title, """")
                    '   ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Not Found On Host") & ": " & uMovie.Filename, Nothing}))
                    Return False
                End If

            Catch ex As Exception
                ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & uMovie.Filename, Nothing}))
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function

#End Region '"Movieset API

#Region "TVEpisode API"
        ''' <summary>
        ''' Get all episodes by various details
        ''' </summary>
        ''' <param name="RemotePath"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Async Function GetAllTVEpisodesByDetails(ByVal RemotePath As String, ByVal LocalPath_Filename As String, ByVal ShowID As Integer, ByVal Season As Integer) As Task(Of VideoLibrary.GetEpisodesResponse)
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] GetAllTVEpisodesByDetails: No client initialized! Abort!")
                Return Nothing
            End If

            If Not String.IsNullOrEmpty(RemotePath) Then
                Try
                    Dim filter As New XBMCRPC.List.Filter.EpisodesAnd With {.and = New List(Of Object)}
                    Dim filterRulePath As New XBMCRPC.List.Filter.Rule.Episodes
                    filterRulePath.field = List.Filter.Fields.Episodes.path
                    filterRulePath.Operator = List.Filter.Operators.startswith
                    filterRulePath.value = RemotePath
                    filter.and.Add(filterRulePath)
                    Dim filterRuleFilename As New XBMCRPC.List.Filter.Rule.Episodes
                    filterRuleFilename.field = List.Filter.Fields.Episodes.filename
                    filterRuleFilename.Operator = List.Filter.Operators.endswith
                    filterRuleFilename.value = Path.GetFileName(LocalPath_Filename)
                    filter.and.Add(filterRuleFilename)

                    Dim response = Await _kodi.VideoLibrary.GetEpisodes(filter, ShowID, Season, Video.Fields.Episode.AllFields).ConfigureAwait(False)
                    Return response
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                    Return Nothing
                End Try
            Else
                Return Nothing
            End If
        End Function
        ''' <summary>
        ''' Search a episode by various details
        ''' </summary>
        ''' <param name="LocalPath_TVShow">Full TVShow path</param>
        ''' <param name="LocalPath_Filename">FullPath of Filename with extension</param>
        ''' <param name="Season">Season number of episode</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Async Function SearchTVEpisodeByDetails(ByVal LocalPath_TVShow As String, ByVal LocalPath_Filename As String, ByVal Season As Integer) As Task(Of XBMCRPC.Video.Details.Episode)
            Dim KodiTVShow = Await SearchTVShowByPath(LocalPath_TVShow).ConfigureAwait(False)
            Dim ShowID As Integer = -1
            If Not KodiTVShow Is Nothing Then
                ShowID = KodiTVShow.tvshowid
            End If
            If ShowID < 1 Then
                logger.Trace(String.Concat("[APIKodi] SearchTVEpisodeByDetails: " & _currenthost.name & ": """ & LocalPath_Filename & """ NOT found in host database!"))
                Return Nothing
            End If

            Dim RemotePath As String = GetRemotePath(Directory.GetParent(LocalPath_Filename).FullName)

            'get a list of all episodes saved in Kodi DB by Path
            Dim kTVEpisodes As VideoLibrary.GetEpisodesResponse = Await GetAllTVEpisodesByDetails(RemotePath, LocalPath_Filename, ShowID, Season).ConfigureAwait(False)

            If kTVEpisodes.episodes IsNot Nothing Then
                If kTVEpisodes.episodes.Count = 1 Then
                    logger.Trace(String.Concat("[APIKodi] SearchTVEpisodeByDetails: " & _currenthost.name & ": """ & LocalPath_Filename & """ found in host database! [ID:", kTVEpisodes.episodes.Item(0).episodeid, "]"))
                    Return kTVEpisodes.episodes.Item(0)
                ElseIf kTVEpisodes.episodes.Count > 1 Then
                    logger.Warn(String.Concat("[APIKodi] SearchTVEpisodeByDetails: " & _currenthost.name & ": """ & LocalPath_Filename & """ MORE THAN ONE episode found in host database!"))
                    'TODO: add some comparisons to find the correct episode like:
                    'path, TVDB ID, year ...
                    'or try to search movie by filename
                    Return Nothing
                End If
            End If

            logger.Trace(String.Concat("[APIKodi] SearchTVEpisodeByDetails: " & _currenthost.name & ": """ & LocalPath_Filename & """ NOT found in host database!"))
            Return Nothing
        End Function
        ''' <summary>
        ''' Update episode details at Kodi
        ''' </summary>
        ''' <param name="EmberepisodeID">ID of specific episode (EmberDB)</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Update successfull, false=error or episode not found in KodiDB</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' updates all episode fields (also pathes of images)
        ''' at the moment episode on host is identified by searching and comparing filename of episode
        ''' </remarks>
        Public Async Function UpdateTVEpisodeInfo(ByVal EmberepisodeID As Long, ByVal SendHostNotification As Boolean) As Task(Of Boolean)
            Dim isNew As Boolean = False
            Dim uEpisode As Structures.DBTV = Master.DB.LoadTVEpFromDB(EmberepisodeID, True)

            Try
                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] UpdateTVEpisodeInfo: No client initialized! Abort!")
                    Return False
                End If

                'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Start Syncing") & ": " & uEpisode.Filename, New Bitmap(My.Resources.logo)}))

                'search ID in Kodi DB
                Dim KodiEpsiode As XBMCRPC.Video.Details.Episode = Await SearchTVEpisodeByDetails(uEpisode.ShowPath, uEpisode.Filename, uEpisode.TVEp.Season).ConfigureAwait(False)
                Dim KodiID As Integer = -1
                If Not KodiEpsiode Is Nothing Then
                    KodiID = KodiEpsiode.episodeid
                End If

                'episode isn't in database of host -> scan directory
                If KodiID = -1 Then
                    logger.Warn("[APIKodi] UpdateTVEpisodeInfo: " & _currenthost.name & ": """ & uEpisode.Filename & """: Not found in database, scan directory...")
                    Await ScanVideoPath(EmberepisodeID, Enums.Content_Type.TVEpisode).ConfigureAwait(False)
                    'wait a bit before trying going on, as scan might take a while on Kodi...
                    Threading.Thread.Sleep(1000) 'TODO better solution for this?!
                    KodiEpsiode = Await SearchTVEpisodeByDetails(uEpisode.ShowPath, uEpisode.Filename, uEpisode.TVEp.Season).ConfigureAwait(False)
                    If Not KodiEpsiode Is Nothing Then
                        isNew = True
                        KodiID = KodiEpsiode.episodeid
                    End If
                End If

                If KodiID > -1 Then
                    'string or string.empty
                    Dim mDateAdded As String = uEpisode.TVEp.DateAdded
                    Dim mLastPlayed As String = uEpisode.TVEp.LastPlayed
                    Dim mPlot As String = uEpisode.TVEp.Plot
                    Dim mTitle As String = uEpisode.TVEp.Title

                    'digit grouping symbol for Votes count
                    Dim mVotes As String = If(Not String.IsNullOrEmpty(uEpisode.TVEp.Votes), uEpisode.TVEp.Votes, Nothing)
                    If Master.eSettings.GeneralDigitGrpSymbolVotes Then
                        If uEpisode.TVEp.VotesSpecified Then
                            Dim vote As String = Double.Parse(uEpisode.TVEp.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                            If vote IsNot Nothing Then
                                mVotes = vote
                            End If
                        End If
                    End If

                    'integer or 0
                    Dim mRating As Double = If(uEpisode.TVEp.RatingSpecified, CType(Double.Parse(uEpisode.TVEp.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), Double), 0)
                    Dim mRuntime As Integer = If(uEpisode.TVEp.RuntimeSpecified, CType(uEpisode.TVEp.Runtime, Integer), 0) * 60 'API requires runtime in seconds

                    'Nullable(Of Integer)types. Following values can be integer or set to Nothing if not filled (nullable integer type here because otherwise default value would always be "0" which is not correct)
                    Dim mPlaycount As Integer? = If(uEpisode.TVEp.PlaycountSpecified, CType(uEpisode.TVEp.Playcount, Integer?), New Integer?)

                    'arrays
                    'Directors
                    Dim mDirectorList As List(Of String) = If(uEpisode.TVEp.DirectorSpecified, uEpisode.TVEp.Directors, New List(Of String))

                    'Writers (Credits)
                    Dim mWriterList As List(Of String) = If(uEpisode.TVEp.CreditsSpecified, uEpisode.TVEp.Credits, New List(Of String))

                    'all image paths will be set in artwork object
                    Dim artwork As New Media.Artwork.Set
                    artwork.fanart = If(Not String.IsNullOrEmpty(uEpisode.FanartPath), _
                                                  GetRemotePath(uEpisode.FanartPath), Nothing)
                    artwork.poster = If(Not String.IsNullOrEmpty(uEpisode.PosterPath), _
                                                  GetRemotePath(uEpisode.PosterPath), Nothing)
                    'artwork.thumb = mPoster ' not supported in Ember?!

                    Dim response = Await _kodi.VideoLibrary.SetEpisodeDetails(KodiID, _
                                                                        title:=mTitle, _
                                                                        playcount:=mPlaycount, _
                                                                        runtime:=mRuntime, _
                                                                        director:=mDirectorList, _
                                                                        plot:=mPlot, _
                                                                        rating:=mRating, _
                                                                        votes:=mVotes, _
                                                                        lastplayed:=mLastPlayed, _
                                                                        writer:=mWriterList, _
                                                                        art:=artwork).ConfigureAwait(False)
                    'not supported right now in Ember
                    'originaltitle:=moriginaltitle, _    
                    'firstaired:=mfirstaired, _    
                    'productioncode:=mproductioncode, _     
                    'thumbnail:=mposter, _
                    'fanart:=mFanart, _
                    'resume:=mresume, _


                    If response.Contains("error") Then
                        logger.Error("[APIKodi] UpdateTVEpisodeInfo: " & _currenthost.name & ": " & response)
                        '  ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & uEpisode.Filename, Nothing}))
                        Return False
                    Else
                        logger.Trace("[APIKodi] UpdateTVEpisodeInfo: " & _currenthost.name & ": " & If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": """ & uEpisode.Filename, """")
                        '  ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync OK") & ": " & uEpisode.Filename, New Bitmap(My.Resources.logo)}))

                        'Remove old textures (cache) 'TODO: Limit to images of this Epsiode
                        Dim resTextures = Await RemoveTextures(Directory.GetParent(uEpisode.Filename).FullName)

                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await SendMessage("Ember Media Manager", If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & uEpisode.TVShow.Title & ": " & uEpisode.TVEp.Title).ConfigureAwait(False)
                        End If
                        Return True
                    End If
                Else
                    logger.Trace("[APIKodi] UpdateTVEpisodeInfo: " & _currenthost.name & ": " & Master.eLang.GetString(1453, "Not Found On Host") & ": """ & uEpisode.Filename, """")
                    ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Not Found On Host") & ": " & uEpisode.Filename, Nothing}))
                    Return False
                End If
            Catch ex As Exception
                ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & uEpisode.Filename, Nothing}))
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function

#End Region 'TVEpisode API

#Region "TVSeason API"

        Public Async Function GetAllTVSeasonsByShowID(ByVal ShowID As Integer) As Task(Of VideoLibrary.GetSeasonsResponse)
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] GetAllMovieSets: No client initialized! Abort!")
                Return Nothing
            End If

            Try
                Dim response = Await _kodi.VideoLibrary.GetSeasons(ShowID, Video.Fields.Season.AllFields).ConfigureAwait(False)
                Return response
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function

        Public Async Function SearchTVSeasonByDetails(ByVal LocalPath_TVShow As String, ByVal Season As Integer) As Task(Of XBMCRPC.Video.Details.Season)
            Dim KodiTVShow = Await SearchTVShowByPath(LocalPath_TVShow).ConfigureAwait(False)
            Dim ShowID As Integer = -1
            If Not KodiTVShow Is Nothing Then
                ShowID = KodiTVShow.tvshowid
            End If
            If ShowID < 1 Then
                logger.Trace(String.Concat("[APIKodi] SearchTVSeasonByDetails: " & _currenthost.name & ": """ & LocalPath_TVShow & """ NOT found in host database!"))
                Return Nothing
            End If

            'get a list of all seasons saved in Kodi DB by ShowID
            Dim kTVSeasons As VideoLibrary.GetSeasonsResponse = Await GetAllTVSeasonsByShowID(ShowID).ConfigureAwait(False)

            If kTVSeasons.seasons IsNot Nothing Then
                Dim result = kTVSeasons.seasons.FirstOrDefault(Function(f) f.season = If(Season = 999, -1, Season))
                If result IsNot Nothing Then
                    logger.Trace(String.Concat("[APIKodi] SearchTVSeasonByDetails: " & _currenthost.name & ": """ & LocalPath_TVShow & """ found in host database! [ID:", result.seasonid, "]"))
                    Return result
                End If
            End If

            logger.Trace(String.Concat("[APIKodi] SearchTVSeasonByDetails: " & _currenthost.name & ": """ & LocalPath_TVShow & """ NOT found in host database!"))
            Return Nothing
        End Function
        ''' <summary>
        ''' Update season details at Kodi
        ''' </summary>
        ''' <param name="EmberseasonID">ID of specific season (EmberDB)</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Update successfull, false=error or movieset not found in KodiDB</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation, main code by DanCooper
        ''' updates all movieset fields which are filled/set in Ember (also paths of images)
        ''' </remarks>
        Public Async Function UpdateTVSeasonInfo(ByVal EmberseasonID As Long, ByVal SendHostNotification As Boolean) As Task(Of Boolean)
            Dim isNew As Boolean = False
            Dim uSeason As Structures.DBTV = Master.DB.LoadTVSeasonFromDB(EmberseasonID, True)

            Try
                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] UpdateTVSeasonInfo: No client initialized! Abort!")
                    Return False
                End If
                ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Start Syncing") & ": " & uMovie.Filename, New Bitmap(My.Resources.logo)}))

                'search season ID in Kodi DB
                Dim KodiSeason As XBMCRPC.Video.Details.Season = Await SearchTVSeasonByDetails(uSeason.ShowPath, uSeason.TVSeason.Season).ConfigureAwait(False)
                Dim KodiID As Integer = -1
                If Not KodiSeason Is Nothing Then
                    KodiID = KodiSeason.seasonid
                End If

                'season isn't in database of host -> scan show directory?
                If KodiID = -1 Then
                    logger.Warn("[APIKodi] UpdateTVSeasonInfo: " & _currenthost.name & ": Season " & uSeason.TVEp.Season & ": Not found in database, abort process...")
                    'what to do in this case?
                    Await ScanVideoPath(EmberseasonID, Enums.Content_Type.TVShow).ConfigureAwait(False)
                    'wait a bit before trying going on, as scan might take a while on Kodi...
                    Threading.Thread.Sleep(1000) 'TODO better solution for this?!
                    KodiSeason = Await SearchTVSeasonByDetails(uSeason.ShowPath, uSeason.TVSeason.Season).ConfigureAwait(False)
                    If Not KodiSeason Is Nothing Then
                        isNew = True
                        KodiID = KodiSeason.seasonid
                    End If
                End If

                If KodiID > -1 Then
                    'string or null/nothing
                    Dim mBanner As String = If(Not String.IsNullOrEmpty(uSeason.BannerPath), _
                                                  GetRemotePath(uSeason.BannerPath), Nothing)
                    Dim mFanart As String = If(Not String.IsNullOrEmpty(uSeason.FanartPath), _
                                                 GetRemotePath(uSeason.FanartPath), Nothing)
                    Dim mLandscape As String = If(Not String.IsNullOrEmpty(uSeason.LandscapePath), _
                                                  GetRemotePath(uSeason.LandscapePath), Nothing)
                    Dim mPoster As String = If(Not String.IsNullOrEmpty(uSeason.PosterPath), _
                                                  GetRemotePath(uSeason.PosterPath), Nothing)

                    'all image paths will be set in artwork object
                    Dim artwork As New Media.Artwork.Set
                    artwork.banner = mBanner
                    artwork.fanart = mFanart
                    artwork.landscape = mLandscape
                    artwork.poster = mPoster

                    Dim response = Await _kodi.VideoLibrary.SetSeasonDetails(KodiID, _
                                                                             art:=artwork).ConfigureAwait(False)

                    If response.Contains("error") Then
                        logger.Error("[APIKodi] UpdateTVSeasonInfo: " & _currenthost.name & ": " & response)
                        ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & uMovie.Filename, Nothing}))
                        Return False
                    Else
                        logger.Trace("[APIKodi] UpdateTVSeasonInfo: " & _currenthost.name & ": " & If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & uSeason.TVSeason.Season)
                        ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync OK") & ": " & uMovie.Filename, New Bitmap(My.Resources.logo)}))

                        'Remove old textures (cache) 'TODO: Limit to images of this Season
                        Dim resTextures = Await RemoveTextures(uSeason.ShowPath)

                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await SendMessage("Ember Media Manager", If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & uSeason.TVShow.Title & ": Season " & uSeason.TVSeason.Season).ConfigureAwait(False)
                        End If
                        Return True
                    End If
                Else
                    logger.Trace("[APIKodi] UpdateTVSeasonInfo: " & _currenthost.name & ": " & Master.eLang.GetString(1453, "Not Found On Host") & ": " & uSeason.TVSeason.Season)
                    '   ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Not Found On Host") & ": " & uMovie.Filename, Nothing}))
                    Return False
                End If

            Catch ex As Exception
                ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & uMovie.Filename, Nothing}))
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function

#End Region 'TVSeason API

#Region "TVShow API"
        ''' <summary>
        ''' Get all tvshows from Kodi host
        ''' </summary>
        ''' <returns>list of kodi tv shows, Nothing: error</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' Notice: No exception handling here because this function is called/nested in other functions and an exception must not be consumed (meaning a disconnect host would not be recognized at once)
        ''' </remarks>
        Public Async Function GetAllTVShows() As Task(Of List(Of Video.Details.TVShow))
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] GetAllTVShows: No client initialized! Abort!")
                Return Nothing
            End If
            Dim response = Await _kodi.VideoLibrary.GetTVShows(Video.Fields.TVShow.AllFields).ConfigureAwait(False)
            Return response.tvshows.ToList()
        End Function
        ''' <summary>
        ''' Get all tv shows by a given path
        ''' </summary>
        ''' <param name="RemotePath"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Async Function GetAllTVShowsByPath(ByVal RemotePath As String) As Task(Of VideoLibrary.GetTVShowsResponse)
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] GetAllTVShowsByPath: No client initialized! Abort!")
                Return Nothing
            End If

            If Not String.IsNullOrEmpty(RemotePath) Then
                Try
                    Dim filter As New XBMCRPC.List.Filter.TVShowsOr With {.or = New List(Of Object)}
                    Dim filterRule As New XBMCRPC.List.Filter.Rule.TVShows
                    filterRule.field = List.Filter.Fields.TVShows.path
                    filterRule.Operator = List.Filter.Operators.startswith
                    filterRule.value = RemotePath
                    filter.or.Add(filterRule)

                    Dim response = Await _kodi.VideoLibrary.GetTVShows(filter, Video.Fields.TVShow.AllFields).ConfigureAwait(False)
                    Return response
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                    Return Nothing
                End Try
            Else
                Return Nothing
            End If
        End Function
        ''' <summary>
        ''' Search a tv show by tv show path
        ''' </summary>
        ''' <param name="LocalPath">Full tv show path</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Async Function SearchTVShowByPath(ByVal LocalPath As String) As Task(Of XBMCRPC.Video.Details.TVShow)
            Dim RemotePath As String = GetRemotePath(LocalPath)

            'get a list of all tv shows saved in Kodi DB by Path
            Dim kTVShows As VideoLibrary.GetTVShowsResponse = Await GetAllTVShowsByPath(RemotePath).ConfigureAwait(False)

            If kTVShows.tvshows IsNot Nothing Then
                If kTVShows.tvshows.Count = 1 Then
                    logger.Trace(String.Concat("[APIKodi] SearchTVShowByPath: " & _currenthost.name & ": """ & LocalPath & """ found in host database! [ID:", kTVShows.tvshows.Item(0).tvshowid, "]"))
                    Return kTVShows.tvshows.Item(0)
                ElseIf kTVShows.tvshows.Count > 1 Then
                    logger.Warn(String.Concat("[APIKodi] SearchTVShowByPath: " & _currenthost.name & ": """ & LocalPath & """ MORE THAN ONE tv show found in host database!"))
                    'TODO: add some comparisons to find the correct tv show like:
                    'TMDB ID, ...
                    Return Nothing
                End If
            End If

            logger.Trace(String.Concat("[APIKodi] SearchTVShowByPath: " & _currenthost.name & ": """ & LocalPath & """ NOT found in host database!"))
            Return Nothing
        End Function
        ''' <summary>
        ''' Update TVShow details at Kodi
        ''' </summary>
        ''' <param name="EmbershowID">ID of specific tvshow (EmberDB)</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Update successfull, false=error or show not found in KodiDB</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' updates all TVShow fields (also paths of images)
        ''' at the moment TVShow on host is identified by searching and comparing path of TVShow
        ''' </remarks>
        Public Async Function UpdateTVShowInfo(ByVal EmbershowID As Long, ByVal SendHostNotification As Boolean) As Task(Of Boolean)
            Dim isNew As Boolean = False
            Dim uTVShow As Structures.DBTV = Master.DB.LoadTVShowFromDB(EmbershowID, False)
            Try

                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] UpdateTVShowInfo: No client initialized! Abort!")
                    Return False
                End If

                ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Start Syncing") & ": " & uTVShow.ShowPath, New Bitmap(My.Resources.logo)}))

                'search ID in Kodi DB
                Dim KodiTVShow As XBMCRPC.Video.Details.TVShow = Await SearchTVShowByPath(uTVShow.ShowPath).ConfigureAwait(False)
                Dim KodiID As Integer = -1
                If Not KodiTVShow Is Nothing Then
                    KodiID = KodiTVShow.tvshowid
                End If

                'TVShow isn't in database of host -> scan directory
                If KodiID = -1 Then
                    logger.Warn("[APIKodi] UpdateTVShowInfo: " & _currenthost.name & ": """ & uTVShow.ShowPath & """: Not found in database, scan directory...")
                    Await ScanVideoPath(EmbershowID, Enums.Content_Type.TVShow).ConfigureAwait(False)
                    'wait a bit before trying going on, as scan might take a while on Kodi...
                    Threading.Thread.Sleep(1000) 'TODO better solution for this?!
                    KodiTVShow = Await SearchTVShowByPath(uTVShow.ShowPath).ConfigureAwait(False)
                    If Not KodiTVShow Is Nothing Then
                        isNew = True
                        KodiID = KodiTVShow.tvshowid
                    End If
                End If

                If KodiID > -1 Then

                    'TODO missing:
                    ' Dim mPlaycount As String = If(uTVShow.TVShow.PlayCountSpecified, uTVShow.TVShow.PlayCount, "0")
                    ' Dim mLastPlayed As String = If(uEpisode.TVEp.LastPlayedSpecified, Web.HttpUtility.JavaScriptStringEncode(uEpisode.TVEp.LastPlayed, True), "null")

                    'string or string.empty
                    Dim mEpisodeGuide As String = uTVShow.TVShow.EpisodeGuide.URL
                    Dim mImdbnumber As String = uTVShow.TVShow.TVDB
                    Dim mMPAA As String = uTVShow.TVShow.MPAA
                    Dim mOriginalTitle As String = uTVShow.TVShow.OriginalTitle
                    Dim mPlot As String = uTVShow.TVShow.Plot
                    Dim mPremiered As String = uTVShow.TVShow.Premiered
                    Dim mSortTitle As String = uTVShow.TVShow.SortTitle
                    Dim mTitle As String = uTVShow.TVShow.Title

                    'digit grouping symbol for Votes count
                    Dim mVotes As String = If(Not String.IsNullOrEmpty(uTVShow.TVShow.Votes), uTVShow.TVShow.Votes, Nothing)
                    If Master.eSettings.GeneralDigitGrpSymbolVotes Then
                        If uTVShow.TVShow.VotesSpecified Then
                            Dim vote As String = Double.Parse(uTVShow.TVShow.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                            If vote IsNot Nothing Then
                                mVotes = vote
                            End If
                        End If
                    End If

                    'integer or 0
                    Dim mRating As Double = If(uTVShow.TVShow.RatingSpecified, CType(Double.Parse(uTVShow.TVShow.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), Double), 0)
                    Dim mRuntime As Integer = If(uTVShow.TVShow.RuntimeSpecified, CType(uTVShow.TVShow.Runtime, Integer), 0)

                    'arrays
                    'Genres
                    Dim mGenreList As List(Of String) = If(uTVShow.TVShow.GenresSpecified, uTVShow.TVShow.Genres, New List(Of String))

                    'Studios
                    Dim mStudioList As List(Of String) = If(uTVShow.TVShow.StudiosSpecified, uTVShow.TVShow.Studios, New List(Of String))

                    'Tags
                    Dim mTagList As List(Of String) = If(uTVShow.TVShow.Tags.Count > 0, uTVShow.TVShow.Tags, New List(Of String))

                    'string or null/nothing
                    Dim mBanner As String = If(Not String.IsNullOrEmpty(uTVShow.BannerPath), _
                                                  GetRemotePath(uTVShow.BannerPath), Nothing)
                    Dim mCharacterArt As String = If(Not String.IsNullOrEmpty(uTVShow.CharacterArtPath), _
                                               GetRemotePath(uTVShow.CharacterArtPath), Nothing)
                    Dim mClearArt As String = If(Not String.IsNullOrEmpty(uTVShow.ClearArtPath), _
                                                GetRemotePath(uTVShow.ClearArtPath), Nothing)
                    Dim mClearLogo As String = If(Not String.IsNullOrEmpty(uTVShow.ClearLogoPath), _
                                                GetRemotePath(uTVShow.ClearLogoPath), Nothing)
                    Dim mFanart As String = If(Not String.IsNullOrEmpty(uTVShow.FanartPath), _
                                               GetRemotePath(uTVShow.FanartPath), Nothing)
                    Dim mLandscape As String = If(Not String.IsNullOrEmpty(uTVShow.LandscapePath), _
                                              GetRemotePath(uTVShow.LandscapePath), Nothing)
                    Dim mPoster As String = If(Not String.IsNullOrEmpty(uTVShow.PosterPath), _
                                                 GetRemotePath(uTVShow.PosterPath), Nothing)
                    'TODO Missing Artwork:
                    'Dim mExtraThumbs As String = If(Not String.IsNullOrEmpty(uTVShow.ShowEThumbsPath), _
                    '                           Web.HttpUtility.JavaScriptStringEncode(GetRemoteFilePath(uTVShow.ShowEThumbsPath, uTVShow.Source), True), "null")
                    'Dim mThumb As String = If(Not String.IsNullOrEmpty(uTVShow.ShowThumbPath), _
                    '                           Web.HttpUtility.JavaScriptStringEncode(GetRemoteFilePath(uTVShow.ShowThumbPath, uTVShow.Source), True), "null")
                    'artwork.thumb = mPoster
                    'artwork.extrathumbs = mExtraThumbs

                    'all image paths will be set in artwork object
                    Dim artwork As New Media.Artwork.Set
                    artwork.banner = mBanner
                    artwork.characterart = mCharacterArt
                    artwork.clearart = mClearArt
                    artwork.clearlogo = mClearLogo
                    artwork.fanart = mFanart
                    artwork.landscape = mLandscape
                    artwork.poster = mPoster

                    Dim response = Await _kodi.VideoLibrary.SetTVShowDetails(KodiID, _
                                                                        title:=mTitle, _
                                                                        studio:=mStudioList, _
                                                                        plot:=mPlot, _
                                                                        genre:=mGenreList, _
                                                                        rating:=mRating, _
                                                                        mpaa:=mMPAA, _
                                                                        imdbnumber:=mImdbnumber, _
                                                                        premiered:=mPremiered, _
                                                                        votes:=mVotes, _
                                                                        originaltitle:=mOriginalTitle, _
                                                                        sorttitle:=mSortTitle, _
                                                                        episodeguide:=mEpisodeGuide, _
                                                                        tag:=mTagList, _
                                                                        art:=artwork).ConfigureAwait(False)
                    'not supported right now in Ember
                    'thumbnail:=mposter, _
                    'fanart:=mFanart, _
                    'resume:=mresume, _
                    'playcount:=mplaycount, _
                    'lastplayed:=mlastplayed, _

                    If response.Contains("error") Then
                        logger.Error("[APIKodi] UpdateTVShowInfo: " & _currenthost.name & ": " & response)
                        '   ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & uTVShow.ShowPath, Nothing}))
                        Return False
                    Else
                        logger.Trace("[APIKodi] UpdateMovieInfo: " & _currenthost.name & ": " & If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": """ & uTVShow.ShowPath, """")
                        '  ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync OK") & ": " & uTVShow.ShowPath, New Bitmap(My.Resources.logo)}))

                        'Remove old textures (cache) 'TODO: Limit to images of this Show (exkl. all season and episode images)
                        Dim resTextures = Await RemoveTextures(uTVShow.ShowPath)

                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await SendMessage("Ember Media Manager", If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & uTVShow.TVShow.Title).ConfigureAwait(False)
                        End If
                        Return True
                    End If
                Else
                    logger.Trace("[APIKodi] UpdateTVShowInfo: " & _currenthost.name & ": " & Master.eLang.GetString(1453, "Not Found On Host") & ": """ & uTVShow.ShowPath, """")
                    '   ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Not Found On Host") & ": " & uTVShow.ShowPath, Nothing}))
                    Return False
                End If
            Catch ex As Exception
                ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & uTVShow.ShowPath, Nothing}))
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function

#End Region 'TVShow API

#Region "General API"
        ''' <summary>
        ''' Sync playcount for specific episode/movie between Kodi host and EmberDB
        ''' </summary>
        ''' <param name="EmbervideofileID">ID of specific videoitem (EmberDB)</param>
        ''' <param name="EmbervideofileID">type of videoitem (EmberDB), at the moment following is supported: movie, tvshow, episode</param>
        ''' <returns>true=sync successful, false=error</returns>
        ''' Notice: No exception handling here because this function is called/nested in other functions and an exception must not be consumed (meaning a disconnect host would not be recognized at once)
        ''' <remarks>
        ''' 2015/07/09 Cocotus - First implementation
        ''' At the moment we read and save playcount/lastplayed value of Kodi video item (movie or episode) to EmberDB. We don't overwrite Kodi playcount data
        ''' </remarks>
        Public Async Function SyncPlaycount(ByVal EmbervideofileID As Long, ByVal videotype As String) As Task(Of Boolean)
            Try
                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] SyncPlaycount: No client initialized! Abort!")
                    Return False
                End If
                Select Case videotype
                    Case "movie"
                        Dim uMovie As Structures.DBMovie = Master.DB.LoadMovieFromDB(EmbervideofileID)

                        'search movie ID in Kodi DB
                        Dim KodiMovie = Await SearchMovieByPath(Directory.GetParent(uMovie.Filename).FullName).ConfigureAwait(False)
                        If KodiMovie Is Nothing Then
                            'movie isn't in database of host -> scan directory
                            logger.Warn("[APIKodi] GetPlayCount: " & _currenthost.name & ": " & uMovie.Filename & ": Not found in database, scan directory...")
                            Await ScanVideoPath(EmbervideofileID, Enums.Content_Type.Movie).ConfigureAwait(False)
                            'wait a bit before trying going on, as scan might take a while on Kodi...
                            Threading.Thread.Sleep(1000) 'TODO better solution for this?!
                            KodiMovie = Await SearchMovieByPath(Directory.GetParent(uMovie.Filename).FullName).ConfigureAwait(False)
                        End If
                        'if host information retrieved, update playcount/lastplayed in EmberDB
                        If Not KodiMovie Is Nothing Then
                            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                                uMovie.Movie.PlayCount = CStr(KodiMovie.playcount)
                                'check date format
                                'should be: 2014-09-01  09:10:11
                                Dim myDateString As String = KodiMovie.lastplayed
                                Dim myDate As DateTime
                                Dim isDate As Boolean = DateTime.TryParse(myDateString, myDate)
                                If isDate Then
                                    uMovie.Movie.LastPlayed = myDate.ToString("yyyy-MM-dd HH:mm:ss")
                                End If
                                Master.DB.SaveMovieToDB(uMovie, False, False, True)
                                SQLtransaction.Commit()
                            End Using
                            Return True
                        Else
                            Return False
                        End If
                    Case "episode"
                        Dim uEpisode As Structures.DBTV = Master.DB.LoadTVEpFromDB(EmbervideofileID, True)
                        'search movie ID in Kodi DB
                        Dim KodiEpsiode = Await SearchTVEpisodeByDetails(uEpisode.ShowPath, uEpisode.Filename, uEpisode.TVEp.Season).ConfigureAwait(False)
                        If KodiEpsiode Is Nothing Then
                            'episode isn't in database of host -> scan directory
                            logger.Warn("[APIKodi] SyncPlaycount: " & _currenthost.name & ": """ & uEpisode.Filename & """: Not found in database, scan directory...")
                            Await ScanVideoPath(EmbervideofileID, Enums.Content_Type.TVEpisode).ConfigureAwait(False)
                            'wait a bit before trying going on, as scan might take a while on Kodi...
                            Threading.Thread.Sleep(1000) 'TODO better solution for this?!
                            KodiEpsiode = Await SearchTVEpisodeByDetails(uEpisode.ShowPath, uEpisode.Filename, uEpisode.TVEp.Season).ConfigureAwait(False)
                        End If
                        'if host information retrieved, update playcount/lastplayed in EmberDB
                        If Not KodiEpsiode Is Nothing Then
                            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                                Dim tmpshow As New Structures.DBTV
                                tmpshow = Master.DB.LoadTVEpFromDB(EmbervideofileID, True)
                                tmpshow.TVEp.Playcount = CStr(KodiEpsiode.playcount)
                                'check date format
                                'should be: 2014-09-01  09:10:11
                                Dim myDateString As String = KodiEpsiode.lastplayed
                                Dim myDate As DateTime
                                Dim isDate As Boolean = DateTime.TryParse(myDateString, myDate)
                                If isDate Then
                                    tmpshow.TVEp.LastPlayed = myDate.ToString("yyyy-MM-dd HH:mm:ss")
                                End If
                                Master.DB.SaveTVEpToDB(tmpshow, False, False, True)
                                SQLtransaction.Commit()
                            End Using
                            Return True
                        Else
                            Return False
                        End If
                    Case Else
                        logger.Warn("[APIKodi] SyncPlaycount: " & _currenthost.name & ": No videotype specified, Abort!")
                        Return False
                End Select
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Clean video library of host
        ''' </summary>
        ''' <returns>string with status message, if failed: Nothing</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Async Function CleanVideoLibrary() As Task(Of String)
            Try
                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] CleanVideoLibrary: No client initialized! Abort!")
                    Return Nothing
                End If
                Dim response As String = ""
                response = Await _kodi.VideoLibrary.Clean.ConfigureAwait(False)
                logger.Trace("[APIKodi] CleanVideoLibrary: " & _currenthost.name)
                Return response
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Scan video library of Kodi host
        ''' </summary>
        ''' <returns>string with status message, if failed: Nothing</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Async Function ScanVideoLibrary() As Task(Of String)
            Try
                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] CleanVideoLibrary: No client initialized! Abort!")
                    Return Nothing
                End If
                Dim response As String = ""
                response = Await _kodi.VideoLibrary.Scan.ConfigureAwait(False)
                logger.Trace("[APIKodi] ScanVideoLibrary: " & _currenthost.name)
                Return response
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Get all sources configured in Kodi host
        ''' </summary>
        ''' <param name="mediaType">type of source (default: video)</param>
        ''' <returns>list of sources</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' 2015/07/05 Cocotus - Added multipath support, i.e nfs://192.168.2.200/Media_1/Movie/nfs://192.168.2.200/Media_2/Movie/
        ''' </remarks>
        Public Async Function GetSources(Optional mediaType As Files.Media = Files.Media.video) As Task(Of List(Of List.Items.SourcesItem))
            Try
                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] GetSources: No client initialized! Abort!")
                    Return Nothing
                End If

                Dim response = Await _kodi.Files.GetSources(mediaType).ConfigureAwait(False)
                Dim tmplist = response.sources.ToList

                'type multipath sources contain multiple paths
                Dim lstremotesources As New List(Of List.Items.SourcesItem)
                Dim paths As New List(Of String)
                Const MultiPath As String = "multipath://"
                For Each remotesource In tmplist
                    Dim newsource As New List.Items.SourcesItem
                    If remotesource.file.StartsWith(MultiPath) Then
                        logger.Warn("[APIKodi] GetSources: " & _currenthost.name & ": " & remotesource.file & " - Multipath format, try to split...")
                        'remove "multipath://" from path and split on "/"
                        'i.e multipath://nfs%3a%2f%2f192.168.2.200%2fMedia_1%2fMovie%2f/nfs%3a%2f%2f192.168.2.200%2fMedia_2%2fMovie%2f/
                        For Each path As String In remotesource.file.Remove(0, MultiPath.Length).Split("/"c)
                            If Not String.IsNullOrEmpty(path) Then
                                newsource = New List.Items.SourcesItem
                                'URL decode each item
                                newsource.file = Web.HttpUtility.UrlDecode(path)
                                newsource.label = remotesource.label
                                lstremotesources.Add(newsource)
                            End If
                        Next
                    Else
                        logger.Warn("[APIKodi] GetSources: " & _currenthost.name & ": """ & remotesource.file, """")
                        newsource.file = remotesource.file
                        newsource.label = remotesource.label
                        lstremotesources.Add(newsource)
                    End If
                Next
                Return lstremotesources
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Get JSON RPC version of host
        ''' </summary>
        ''' <returns>string which contains exact JSONRPC version, Nothing: Empty string</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Async Function GetHostJSONVersion() As Task(Of String)
            Try
                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] GetHostJSONVersion: No client initialized! Abort!")
                    Return Nothing
                End If

                Dim response = Await _kodi.JSONRPC.Version.ConfigureAwait(False)
                Dim codename As String = ""
                'see codename table here: http://kodi.wiki/view/JSON-RPC_API
                Select Case response.version.major.ToString & response.version.minor
                    Case "2"
                        codename = "Dharma "
                    Case "4"
                        codename = "Eden "
                    Case "60"
                        codename = "Frodo "
                    Case "614"
                        codename = "Gotham "
                    Case "621"
                        codename = "Helix "
                End Select
                Return codename & response.version.major.ToString & "." & response.version.minor
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return ""
            End Try
        End Function
        ''' <summary>
        ''' Scan specific directory for new content
        ''' </summary>
        ''' <param name="EmbervideofileID">ID of specific videoitem (EmberDB)</param>
        ''' <param name="EmbervideofileID">type of videoitem (EmberDB), at the moment following is supported: movie, tvshow, episode</param>
        ''' <returns>true=Update successfull, false=error</returns>
        ''' Notice: No exception handling here because this function is called/nested in other functions and an exception must not be consumed (meaning a disconnect host would not be recognized at once)
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Async Function ScanVideoPath(ByVal EmbervideofileID As Long, ByVal videotype As Enums.Content_Type) As Task(Of Boolean)
            Dim uPath As String = String.Empty
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] ScanVideoPath: No client initialized! Abort!")
                Return Nothing
            End If

            Select Case videotype
                Case Enums.Content_Type.Movie
                    Dim uMovie As Structures.DBMovie = Master.DB.LoadMovieFromDB(EmbervideofileID)
                    If FileUtils.Common.isBDRip(uMovie.Filename) Then
                        'filename must point to m2ts file! 
                        'Ember-Filepath i.e.  E:\Media_1\Movie\Horror\Europa Report\BDMV\STREAM\00000.m2ts
                        'for adding new Bluray rips scan the root folder of movie, i.e: E:\Media_1\Movie\Horror\Europa Report\
                        uPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(uMovie.Filename).FullName).FullName).FullName
                    ElseIf FileUtils.Common.isVideoTS(uMovie.Filename) Then
                        'filename must point to IFO file!
                        'Ember-Filepath i.e.  E:\Media_1\Movie\Action\Crow\VIDEO_TS\VIDEO_TS.IFO
                        'for adding new DVDs scan the root folder of movie, i.e:  E:\Media_1\Movie\Action\Crow\
                        uPath = Directory.GetParent(Directory.GetParent(uMovie.Filename).FullName).FullName
                    Else
                        If Path.GetFileNameWithoutExtension(uMovie.Filename).ToLower = "video_ts" Then
                            uPath = Directory.GetParent(Directory.GetParent(uMovie.Filename).FullName).FullName
                        Else
                            uPath = Directory.GetParent(uMovie.Filename).FullName
                        End If
                    End If
                Case Enums.Content_Type.TVShow
                    Dim uShow As Structures.DBTV = Master.DB.LoadTVShowFromDB(EmbervideofileID, False)
                    If FileUtils.Common.isBDRip(uShow.ShowPath) Then
                        'needs some testing?!
                        uPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(uShow.ShowPath).FullName).FullName).FullName
                    ElseIf FileUtils.Common.isVideoTS(uShow.ShowPath) Then
                        'needs some testing?!
                        uPath = Directory.GetParent(Directory.GetParent(uShow.ShowPath).FullName).FullName
                    Else
                        uPath = uShow.ShowPath
                    End If
                Case Enums.Content_Type.TVEpisode
                    Dim uEpisode As Structures.DBTV = Master.DB.LoadTVEpFromDB(EmbervideofileID, False)
                    If FileUtils.Common.isBDRip(uEpisode.Filename) Then
                        'needs some testing?!
                        uPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(uEpisode.Filename).FullName).FullName).FullName
                    ElseIf FileUtils.Common.isVideoTS(uEpisode.Filename) Then
                        'needs some testing?!
                        uPath = Directory.GetParent(Directory.GetParent(uEpisode.Filename).FullName).FullName
                    Else
                        uPath = Directory.GetParent(uEpisode.Filename).FullName
                    End If
                Case Else
                    logger.Warn("[APIKodi] ScanVideoPath: " & _currenthost.name & ": No videotype specified, Abort!")
                    Return False
            End Select

            uPath = GetRemotePath(uPath)
            If uPath Is Nothing Then
                Return False
            End If
            Dim response = Await _kodi.VideoLibrary.Scan(uPath).ConfigureAwait(False)
            If response.Contains("error") Then
                logger.Error(String.Concat("[APIKodi] ScanVideoPath: " & _currenthost.name & ": """ & uPath, """ ", response))
                Return False
            Else
                logger.Trace(String.Concat("[APIKodi] ScanVideoPath: " & _currenthost.name & ": """ & uPath, """ ", response))
                Return True
            End If
        End Function
        ''' <summary>
        ''' Send message to Kodi which is displayed as notification
        ''' </summary>
        ''' <param name="title">title of notification in Kodi</param>
        ''' <param name="message">message to display</param>
        ''' <returns>string with displayed message, if failed: Nothing</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Async Function SendMessage(ByVal title As String, ByVal message As String) As Task(Of String)
            Try
                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] SendMessage: No client initialized! Abort!")
                    Return Nothing
                End If
                Dim response = Await _kodi.GUI.ShowNotification(title, message, 2500).ConfigureAwait(False)
                Return response
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function

        Public Async Function GetTextures(ByVal MoviePath As String) As Task(Of XBMCRPC.Textures.GetTexturesResponse)
            Try
                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] SendMessage: No client initialized! Abort!")
                    Return Nothing
                End If

                Dim filter As New XBMCRPC.List.Filter.TexturesOr With {.or = New List(Of Object)}
                Dim filterRule As New XBMCRPC.List.Filter.Rule.Textures
                filterRule.field = List.Filter.Fields.Textures.url
                filterRule.Operator = List.Filter.Operators.startswith
                filterRule.value = MoviePath
                filter.or.Add(filterRule)

                Dim response As XBMCRPC.Textures.GetTexturesResponse = Await _kodi.Textures.GetTextures(filter, XBMCRPC.Textures.Fields.Texture.AllFields)
                Return response
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Remove a cached image by Texture ID
        ''' </summary>
        ''' <param name="ID">ID of Texture</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Async Function RemoveTexture(ByVal ID As Integer) As Task(Of String)
            Try
                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] SendMessage: No client initialized! Abort!")
                    Return Nothing
                End If
                Dim response = Await _kodi.Textures.RemoveTexture(ID)
                Return response
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Removes all cached images by a given path
        ''' </summary>
        ''' <param name="LocalPath"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Async Function RemoveTextures(ByVal LocalPath As String) As Task(Of Boolean)
            Try
                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] SendMessage: No client initialized! Abort!")
                    Return False
                End If
                Dim TexturesResponce = Await GetTextures(GetRemotePath(LocalPath))
                For Each tTexture In TexturesResponce.textures
                    Await RemoveTexture(tTexture.textureid)
                Next
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
            Return True
        End Function

#End Region 'General API

#Region "Helper functions/methods"
        ''' <summary>
        ''' Get remotepath of any file using the file's corresponding localpath in Ember
        ''' </summary>
        ''' <param name="localpath">local path to file (as defined in Ember)</param>
        ''' <returns>remote path of file (=path in Kodi), no success: Nothing</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' This might not work for all kind of paths!? 
        ''' For now its necessary that last fragment in remotesource is also part of localfilepath, so I can build the remotepath for the local file
        ''' this means root path won't work for now, i.e remotesource: "Z:\", but this will work: "Z:\Movies"
        ''' </remarks>
        Function GetRemoteFilePath(ByVal localpath As String, Optional ByVal remotesource As String = "") As String
            logger.Trace(String.Concat("[APIKodi] GetRemoteFilePath: Localpath: " & localpath))
            If remotesource = "" Then
                For Each kodisource In _currenthost.source
                    If localpath.StartsWith(kodisource.applicationpath) Then
                        logger.Trace(String.Concat("[APIKodi] GetRemoteFilePath: Found KodiSource: """ & kodisource.remotepath, """"))
                        remotesource = kodisource.remotepath
                        Exit For
                    End If
                Next
            End If

            If remotesource = String.Empty Then
                logger.Warn("[APIKodi] GetRemoteFilePath: KodiSource NOT Found! Abort!")
                Return Nothing
            End If

            'if no seperator is specified use pathseperator of current system (=Windows)
            Dim remotepathseparator = _currenthost.remotepathseparator
            If String.IsNullOrEmpty(remotepathseparator) Then remotepathseparator = Path.DirectorySeparatorChar

            'example remotesources: 
            'nfs://192.168.2.200/Media_1/
            'nfs://192.168.0.2/mnt/share/media/Video/Media_1/, 
            'sftp://name:password@192.168.0.2:22/home/media/Media_1/
            'smb://PC/Share/
            '
            'example localpath:  
            'E:\MyMovies\Media_1\Movie\Action\11.14 - Elevenfourteen\poster.jpg

            'first strip driveletter (since it will probably not be identical between Kodi and Ember (i.e. mapped drive))
            Dim tmpremotepath As String = localpath.Replace(Directory.GetDirectoryRoot(localpath), "")
            'result: MyMovies\Media_1\Movie\Action\11.14 - Elevenfourteen\poster.jpg
            tmpremotepath = tmpremotepath.Replace(Path.DirectorySeparatorChar, remotepathseparator)
            'result: MyMovies/Media_1/Movie/Action/11.14 - Elevenfourteen/poster.jpg

            'split remotesource at each seperator and check last part, as this part is used to identify correct source
            Dim pathsplitsremote As String() = remotesource.Split(New String() {remotepathseparator}, StringSplitOptions.RemoveEmptyEntries)
            Dim matchfolder As String = ""
            'should be at least 2 fragment, one= wrong seperator saved in configuration
            If pathsplitsremote.Count > 1 Then
                'example: matchfolder: Media_1
                matchfolder = pathsplitsremote(pathsplitsremote.Count - 1)
            Else
                logger.Warn(String.Concat("[APIKodi] GetRemoteFilePath: Wrong Remotepathseparator, Abort process: " & remotepathseparator))
                Return Nothing
            End If

            If matchfolder <> "" Then
                'split localpath at each seperator and check each part if its identical to matchfolder - if match is found we put new remotepath together
                Dim pathsplitslocal As String() = tmpremotepath.Split(New String() {remotepathseparator}, StringSplitOptions.RemoveEmptyEntries)
                Dim strhelper As String = ""
                For i = 0 To pathsplitslocal.Count - 1
                    If pathsplitslocal(i) = matchfolder Then
                        For z = i + 1 To pathsplitslocal.Count - 1
                            strhelper = strhelper & remotepathseparator & pathsplitslocal(z)
                        Next
                        'for above example strhelper should look like this now: /Movie/Action/11.14 - Elevenfourteen/poster.jpg
                        Exit For
                    End If
                Next
                'remove last seperator because strhelper already starts with one
                If remotesource.EndsWith(remotepathseparator) Then
                    'example: nfs://192.168.2.200/Media_1
                    remotesource = remotesource.Substring(0, remotesource.LastIndexOf(remotepathseparator))
                End If
                'the final string, example: nfs://192.168.2.200/Media_1/Movie/Action/11.14 - Elevenfourteen/poster.jpg
                tmpremotepath = remotesource & strhelper
                logger.Trace(String.Concat("[APIKodi] GetRemoteFilePath: Constructed Remotepath: " & tmpremotepath))
            End If
            Return tmpremotepath
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="LocalPath"></param>
        ''' <returns></returns>
        ''' <remarks>ATTENTION: It's not allowed to use "Remotepath.ToLower" (Kodi can't find UNC sources with wrong case)</remarks>
        Function GetRemotePath(ByVal LocalPath As String) As String
            Dim RemotePath As String = String.Empty
            Dim RemoteIsUNC As Boolean = False

            For Each Source In _currenthost.source
                Dim tLocalSource As String = String.Empty
                'add a directory separator at the end of the path to distinguish between)
                'D:\Movies
                'D:\Movies Shared
                '(needed for "LocalPath.ToLower.StartsWith(tLocalSource)"
                If Source.applicationpath.Contains(Path.DirectorySeparatorChar) Then
                    tLocalSource = If(Source.applicationpath.EndsWith(Path.DirectorySeparatorChar), Source.applicationpath, String.Concat(Source.applicationpath, Path.DirectorySeparatorChar)).Trim
                ElseIf Source.applicationpath.Contains(Path.AltDirectorySeparatorChar) Then
                    tLocalSource = If(Source.applicationpath.EndsWith(Path.AltDirectorySeparatorChar), Source.applicationpath, String.Concat(Source.applicationpath, Path.AltDirectorySeparatorChar)).Trim
                End If
                If LocalPath.ToLower.StartsWith(tLocalSource.ToLower) Then
                    Dim tRemoteSource As String = String.Empty
                    If Source.remotepath.Contains(Path.DirectorySeparatorChar) Then
                        tRemoteSource = If(Source.remotepath.EndsWith(Path.DirectorySeparatorChar), Source.remotepath, String.Concat(Source.remotepath, Path.DirectorySeparatorChar)).Trim
                    ElseIf Source.remotepath.Contains(Path.AltDirectorySeparatorChar) Then
                        tRemoteSource = If(Source.remotepath.EndsWith(Path.AltDirectorySeparatorChar), Source.remotepath, String.Concat(Source.remotepath, Path.AltDirectorySeparatorChar)).Trim
                        RemoteIsUNC = True
                    End If
                    RemotePath = LocalPath.Replace(tLocalSource, tRemoteSource)
                    If RemoteIsUNC Then
                        RemotePath = RemotePath.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                    Else
                        RemotePath = RemotePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)
                    End If
                    Exit For
                End If
            Next

            Return RemotePath
        End Function
#End Region 'Helper functions/methods

#Region "Unused code"
        ''' <summary>
        ''' Check host connection
        ''' </summary>
        ''' <returns>true: host is online, false:offline</returns>
        ''' <remarks>
        ''' 2015/06/30 Cocotus - First implementation
        ''' </remarks>
        Public Async Function IsValidConnection() As Task(Of Boolean)
            Try
                Dim response = Await _kodi.JSONRPC.Ping.ConfigureAwait(False)
                If response.Length = 0 Then
                    Return False
                End If
                If response(0).ToString = "" Then
                    ' Dim t = _kodi.StartNotificationListener()
                    't.ContinueWith(t2 => { NotificationsEnabled = !t2.IsFaulted; });
                    Return True
                Else
                    'Dim t = _kodi.StartNotificationListener()
                    't.ContinueWith(t2 => { NotificationsEnabled = !t2.IsFaulted; });
                    Return True
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' UnMute Kodi host
        ''' </summary>
        ''' <returns>true: success, false: error</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Async Function UnMute() As Task(Of Boolean)
            Try
                Return Await _kodi.Application.SetMute(False).ConfigureAwait(False)
                'Await Refresh()
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Mute Kodi host
        ''' </summary>
        ''' <returns>true: success, false: error</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Async Function Mute() As Task(Of Boolean)
            Try
                Return Await _kodi.Application.SetMute(True).ConfigureAwait(False)
                'Await Refresh()
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Set volume of Kodi host
        ''' </summary>
        ''' <returns>integer: volume level, Nothing: error</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Async Function SetVolume(ByVal volume As Integer) As Task(Of Integer)
            Try
                Return Await _kodi.Application.SetVolume(volume).ConfigureAwait(False)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Get JSON structure of Kodi host
        ''' </summary>
        ''' <returns>string: JSON structure, Nothing: error</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Async Function GetJSONHost() As Task(Of String)
            Try
                Dim response = Await _kodi.JSONRPC.Introspect().ConfigureAwait(False)
                Return response.ToString
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Get basic Kodi host information
        ''' </summary>
        ''' <returns>XBMCRPC.Application.Property.Value: object which contains specific host information, Nothing: error</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Async Function GetHostInformation() As Task(Of Application.Property.Value)
            Try
                Dim response = Await _kodi.Application.GetProperties(Application.GetProperties_properties.AllFields()).ConfigureAwait(False)
                Return response
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function

        ' Dim ret2 = Await xbmc.VideoLibrary.GetTVShows(TVShow.AllFields())
        ' Dim ret3 = Await xbmc.VideoLibrary.SetMovieDetails(3, playcount:=10)
        ' Dim ret4 = Await xbmc.Files.PrepareDownload(ret4.movies(0).thumbnail)
        ' Dim ret5 = Await xbmc.Files.GetDirectory(ret5b.files(0).file, Media.files, Files.AllFields())
        ' Dim ret6 = Await xbmc.Files.GetDirectory("C:\Archiv\Serien1\How I met your Mother\Staffel 3", Media.video)
        ' Dim ret7 = Await xbmc.Files.GetDirectory("C:\Archiv\HD2", Media.video, Files.AllFields())
        ' Dim ret8 = Await xbmc.Files.GetDirectory("C:\Users\steve_000\Music\Amazon MP3\die ärzte\auch", Media.music, Files.AllFields())
        ' Dim ret9 = Await xbmc.Playlist.GetItems(0, properties:=All.AllFields())
        ' Dim ret10 = Await xbmc.Playlist.GetPlaylists()
        ' Dim ret11 = Await xbmc.Player.GetActivePlayers()

        'Public Async Function Refresh() As Task
        '    Dim props = Await _kodi.Application.GetProperties(New GetProperties_properties() From { _
        '        Name.muted, _
        '        Name.volume _
        '    })

        '    SetProperty(_volumeLevel, props.volume, "VolumeLevel")
        '    VolumeEnabled = Not props.muted
        'End Function

#End Region 'Unused code

#End Region 'Methods

#Region "Nested Types"

        Structure MySettings
            ''' <summary>
            ''' Username for Kodi webservice. Optional. Default is kodi for Kodi hosts ( xbmc for XBMC hosts )
            ''' </summary>
            ''' <returns>Username for Kodi webservice</returns>
            ''' <history>
            ''' 9/13/2015 Cocotus created
            ''' </history>
            Dim Username As String
            ''' <summary>
            ''' Password for Kodi webservice. Optional. As configured in Kodi / XBMC Setup
            ''' </summary>
            ''' <returns>Password for Kodi webservice</returns>
            ''' <history>
            ''' 9/13/2015 Cocotus created
            ''' </history>
            Dim Password As String
            ''' <summary>
            ''' IP address or DNS host name of Kodi / XBMC media player
            ''' </summary>
            ''' <returns>Address of Kodi webservice</returns>
            ''' <history>
            ''' 9/13/2015 Cocotus created
            ''' </history>
            Dim HostIP As String
            ''' <summary>
            ''' Kodi webport.Typically 80 or 8080. As configured in Kodi / XBMC Setup
            ''' </summary>
            ''' <returns>Kodi webport</returns>
            ''' <history>
            ''' 9/13/2015 Cocotus created
            ''' </history>
            Dim WebPort As Integer
        End Structure

#End Region 'Nested Types

    End Class

#Region "Client JSON Communication helper (needed for listening to notification events in Kodi)"
    Friend Class PlatformServices
        Implements IPlatformServices
        'following class platformservices is needed for listening to notification events in Kodi
        'found and converted to vb.net from https://github.com/DerPate2010/Xbmc2ndScr

#Region "Fields"

        Private _socketfactory As ISocketFactory

#End Region

#Region "Constructors"

        Public Sub New()
            SocketFactoryCreate = New SocketFactory()
        End Sub

#End Region 'Constructors

#Region "Methods"

        Public Async Function GetRequestStream(request As Net.WebRequest) As Task(Of Stream)
            Try
                Return Await request.GetRequestStreamAsync().ConfigureAwait(False)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Async Function GetResponse(request As Net.WebRequest) As Task(Of Net.WebResponse)
            Try
                Return Await request.GetResponseAsync().ConfigureAwait(False)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region 'Methods

#Region "Properties"

        Public ReadOnly Property SocketFactory As ISocketFactory Implements IPlatformServices.SocketFactory
            Get
                Return _socketfactory
            End Get
        End Property

        Public Property SocketFactoryCreate As ISocketFactory
            Get
                Return _socketfactory
            End Get
            Private Set(value As ISocketFactory)
                _socketfactory = value
            End Set
        End Property

#End Region 'Properties

    End Class

    Friend Class SocketFactory
        Implements ISocketFactory

#Region "Fields"

#End Region 'Fields

#Region "Constructors"

#End Region 'Constructors

#Region "Methods"

        Public Function GetSocket() As ISocket Implements ISocketFactory.GetSocket
            Return New DummySocket()
        End Function

        Public Async Function ResolveHostname(hostname As String) As Task(Of String()) Implements ISocketFactory.ResolveHostname
            Return Await ResolveHostname(hostname).ConfigureAwait(False)
        End Function

#End Region 'Methods

#Region "Properties"

#End Region 'Properties

    End Class

    Friend Class DummySocket
        Implements ISocket

#Region "Fields"

        Private _socket As Net.Sockets.Socket

#End Region 'Fields

#Region "Constructors"

#End Region 'Constructors

#Region "Methods"

        Public Sub Dispose() Implements IDisposable.Dispose
        End Sub

        Public Async Function ConnectAsync(hostName As String, port As Integer) As Task Implements ISocket.ConnectAsync
            _socket = New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
            _socket.Connect(hostName, port)
        End Function

        Public Function GetInputStream() As Stream Implements ISocket.GetInputStream
            Return New Net.Sockets.NetworkStream(_socket)
        End Function

#End Region 'Methods

#Region "Properties"

#End Region 'Properties

    End Class

#End Region 'Client JSON Communication helper (needed for listening to notification events in Kodi)

End Namespace
