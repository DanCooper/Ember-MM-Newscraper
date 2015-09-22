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
        Private _currenthost As New KodiInterface.Host
        'current selected client
        Private _kodi As XBMCRPC.Client
        'helper object, needed for communication client (notification, eventhandler support)
        Private platformServices As IPlatformServices = New PlatformServices
        'Private NotificationsEnabled As Boolean
        Shared IsRunningTask As Boolean = False

#End Region 'Fields

#Region "Events"

        Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))

#End Region 'Events

#Region "Methods"
        ''' <summary>
        ''' Initialize Communication Client for ONE Kodi Host
        ''' </summary>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Sub New(ByVal host As KodiInterface.Host)
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
            _kodi = New Client(platformServices, _currenthost.Address, _currenthost.Port, _currenthost.Username, _currenthost.Password)
         
        End Sub

        ''' <summary>
        ''' Triggered as soon as video library scan (whole database not specific folder!) is finished
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>just an example for eventhandler</remarks>
        Private Sub VideoLibrary_OnScanFinished(ByVal sender As String, ByVal data As Object)
            'Finished updating video library
            RemoveHandler _kodi.VideoLibrary.OnScanFinished, AddressOf VideoLibrary_OnScanFinished
            IsRunningTask = False
            'cleanup NotificationListener disposing current communication object and make a new init (fix for crash in loop: https://github.com/DanCooper/Ember-MM-Newscraper/blob/master/KodiAPI/XBMCRPC/Client.cs#L131)
            Init()
        End Sub
        ''' <summary>
        ''' Triggered as soon as cleaning of video library is finished
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>just an example for eventhandler</remarks>
        Private Sub VideoLibrary_OnCleanFinished(ByVal sender As String, ByVal data As Object)
            'Finished cleaning of video library
            RemoveHandler _kodi.VideoLibrary.OnCleanFinished, AddressOf VideoLibrary_OnCleanFinished
            IsRunningTask = False
            'cleanup NotificationListener disposing current communication object and make a new init (fix for crash in loop: https://github.com/DanCooper/Ember-MM-Newscraper/blob/master/KodiAPI/XBMCRPC/Client.cs#L131)
            Init()
        End Sub

#Region "Movie API"
        ''' <summary>
        ''' Get all movies from Kodi host
        ''' </summary>
        ''' <returns>list of kodi movies, Nothing: error</returns>
        ''' <remarks></remarks>
        Public Async Function GetAllMovies() As Task(Of VideoLibrary.GetMoviesResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetAllMovies: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Dim response = Await _kodi.VideoLibrary.GetMovies(Video.Fields.Movie.AllFields).ConfigureAwait(False)
                Return response
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Get all movies by a given filename
        ''' </summary>
        ''' <param name="RemoteFilename"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Async Function GetAllMoviesByFilename(ByVal strFilename As String) As Task(Of VideoLibrary.GetMoviesResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetAllMoviesByFilename: No host initialized! Abort!")
                Return Nothing
            End If

            If Not String.IsNullOrEmpty(strFilename) Then
                Try
                    Dim filter As New XBMCRPC.List.Filter.MoviesOr With {.or = New List(Of Object)}
                    Dim filterRule As New XBMCRPC.List.Filter.Rule.Movies
                    filterRule.field = List.Filter.Fields.Movies.filename
                    filterRule.Operator = List.Filter.Operators.endswith
                    filterRule.value = strFilename
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
        Public Async Function GetAllMoviesByPath(ByVal strRemotePath As String) As Task(Of VideoLibrary.GetMoviesResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetAllMoviesByPath: No host initialized! Abort!")
                Return Nothing
            End If

            If Not String.IsNullOrEmpty(strRemotePath) Then
                Try
                    Dim filter As New XBMCRPC.List.Filter.MoviesOr With {.or = New List(Of Object)}
                    Dim filterRule As New XBMCRPC.List.Filter.Rule.Movies
                    filterRule.field = List.Filter.Fields.Movies.path
                    filterRule.Operator = List.Filter.Operators.startswith
                    filterRule.value = strRemotePath
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
        Public Async Function SearchMovieByFilename(ByVal strFilename As String) As Task(Of XBMCRPC.Video.Details.Movie)
            'get a list of all movies saved in Kodi DB by Filename
            Dim kMovies As VideoLibrary.GetMoviesResponse = Await GetAllMoviesByFilename(strFilename).ConfigureAwait(False)

            If kMovies IsNot Nothing Then
                If kMovies.movies IsNot Nothing Then
                    If kMovies.movies.Count = 1 Then
                        logger.Trace(String.Format("[APIKodi] [{0}] SearchMovieByFilename: ""{1}"" | OK, found in host database! [ID:{2}]", _currenthost.Label, strFilename, kMovies.movies.Item(0).movieid))
                        Return kMovies.movies.Item(0)
                    ElseIf kMovies.movies.Count > 1 Then
                        'TODO: add some comparisons to find the correct movie like:
                        'path, IMDB ID, year ...
                        logger.Warn(String.Format("[APIKodi] [{0}] SearchMovieByFilename: ""{1}"" | MORE THAN ONE movie found in host database!", _currenthost.Label, strFilename))
                        Return Nothing
                    Else
                        logger.Warn(String.Format("[APIKodi] [{0}] SearchMovieByFilename: ""{1}"" | NOT found in host database!", _currenthost.Label, strFilename))
                        Return Nothing
                    End If
                Else
                    logger.Warn(String.Format("[APIKodi] [{0}] SearchMovieByFilename: ""{1}"" | NOT found in host database!", _currenthost.Label, strFilename))
                    Return Nothing
                End If
            Else
                logger.Error(String.Format("[APIKodi] [{0}] SearchMovieByFilename: ""{1}"" | No connection to Host!", _currenthost.Label, strFilename))
                Return Nothing
            End If
        End Function
        ''' <summary>
        ''' Search a movie by path
        ''' </summary>
        ''' <param name="LocalPath">Full parent path of video file</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Async Function SearchMovieByPath(ByVal strLocalPath As String) As Task(Of XBMCRPC.Video.Details.Movie)
            Dim strRemotePath As String = GetRemotePath(strLocalPath)

            If Not String.IsNullOrEmpty(strRemotePath) Then
                'get a list of all movies saved in Kodi DB by Path
                Dim kMovies As VideoLibrary.GetMoviesResponse = Await GetAllMoviesByPath(strRemotePath).ConfigureAwait(False)

                If kMovies IsNot Nothing Then
                    If kMovies.movies IsNot Nothing Then
                        If kMovies.movies.Count = 1 Then
                            logger.Trace(String.Format("[APIKodi] [{0}] SearchMovieByPath: ""{1}"" | OK, found in host database! [ID:{2}]", _currenthost.Label, strLocalPath, kMovies.movies.Item(0).movieid))
                            Return kMovies.movies.Item(0)
                        ElseIf kMovies.movies.Count > 1 Then
                            'TODO: add some comparisons to find the correct movie like:
                            'path, IMDB ID, year ...
                            'or try to search movie by filename
                            logger.Warn(String.Format("[APIKodi] [{0}] SearchMovieByPath: ""{1}"" | MORE THAN ONE movie found in host database!", _currenthost.Label, strLocalPath))
                            Return Nothing
                        Else
                            logger.Warn(String.Format("[APIKodi] [{0}] SearchMovieByPath: ""{1}"" | NOT found in host database!", _currenthost.Label, strLocalPath))
                            Return Nothing
                        End If
                    Else
                        logger.Warn(String.Format("[APIKodi] [{0}] SearchMovieByPath: ""{1}"" | NOT found in host database!", _currenthost.Label, strLocalPath))
                        Return Nothing
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] SearchMovieByPath: ""{1}"" | No connection to Host!", _currenthost.Label, strLocalPath))
                    Return Nothing
                End If
            Else
                logger.Error(String.Format("[APIKodi] [{0}] SearchMovieByPath: ""{1}"" | Source not mapped!", _currenthost.Label, strLocalPath))
                Return Nothing
            End If
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
        Public Async Function UpdateMovieInfo(ByVal lngMovieID As Long, ByVal blnSendHostNotification As Boolean, ByVal blnSyncPlayCount As Boolean) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] UpdateMovieInfo: No host initialized! Abort!")
                Return False
            End If

            Dim blnNeedSave As Boolean = False
            Dim isNew As Boolean = False
            Dim uMovie As Database.DBElement = Master.DB.LoadMovieFromDB(lngMovieID, False)

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] UpdateMovieInfo: ""{1}"" | Start syncing process...", _currenthost.Label, uMovie.Movie.Title))

                'search movie ID in Kodi DB
                Dim KodiID As Integer = -1
                Dim KodiMovie As XBMCRPC.Video.Details.Movie = Await SearchMovieByPath(Directory.GetParent(uMovie.Filename).FullName).ConfigureAwait(False)
                If KodiMovie Is Nothing Then
                    KodiMovie = Await SearchMovieByFilename(Path.GetFileName(uMovie.Filename)).ConfigureAwait(False)
                End If
                If KodiMovie IsNot Nothing Then
                    KodiID = KodiMovie.movieid
                End If

                'movie isn't in database of host -> scan directory
                If KodiID = -1 Then
                    logger.Trace(String.Format("[APIKodi] [{0}] UpdateMovieInfo: ""{1}"" | NOT found in database, scan directory on host...", _currenthost.Label, uMovie.Movie.Title))
                    Await ScanVideoPath(lngMovieID, Enums.ContentType.Movie).ConfigureAwait(False)
                    'wait a bit before trying going on, as scan might take a while on Kodi...
                    Threading.Thread.Sleep(1000) 'TODO better solution for this?!
                    KodiMovie = Await SearchMovieByPath(Directory.GetParent(uMovie.Filename).FullName).ConfigureAwait(False)
                    If KodiMovie IsNot Nothing Then
                        isNew = True
                        KodiID = KodiMovie.movieid
                    End If
                End If


                If KodiID > -1 Then
                    'check if we have to retrieve the PlayCount from Kodi
                    If blnSyncPlayCount AndAlso Not uMovie.Movie.PlayCount = KodiMovie.playcount Then
                        uMovie.Movie.PlayCount = KodiMovie.playcount
                        uMovie.Movie.LastPlayed = KodiMovie.lastplayed
                        blnNeedSave = True
                    End If

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
                    Dim mTrailer As String = If(Not String.IsNullOrEmpty(uMovie.Trailer.LocalFilePath), GetRemotePath(uMovie.Trailer.LocalFilePath), If(uMovie.Movie.TrailerSpecified, uMovie.Movie.Trailer, String.Empty))
                    If mTrailer Is Nothing Then mTrailer = String.Empty

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
                    Dim mPlaycount As Integer = If(uMovie.Movie.PlayCountSpecified, uMovie.Movie.PlayCount, 0)
                    Dim mRating As Double = If(uMovie.Movie.RatingSpecified, CType(Double.Parse(uMovie.Movie.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), Double), 0)
                    Dim mRuntime As Integer = If(uMovie.Movie.RuntimeSpecified, CType(uMovie.Movie.Runtime, Integer), 0) * 60 'API requires runtime in seconds
                    Dim mTop250 As Integer = If(uMovie.Movie.Top250Specified, CType(uMovie.Movie.Top250, Integer), 0)
                    Dim mYear As Integer = If(uMovie.Movie.YearSpecified, CType(uMovie.Movie.Year, Integer), 0)

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
                    Dim mBanner As String = If(Not String.IsNullOrEmpty(uMovie.ImagesContainer.Banner.LocalFilePath), _
                                                  GetRemotePath(uMovie.ImagesContainer.Banner.LocalFilePath), Nothing)
                    Dim mClearArt As String = If(Not String.IsNullOrEmpty(uMovie.ImagesContainer.ClearArt.LocalFilePath), _
                                                  GetRemotePath(uMovie.ImagesContainer.ClearArt.LocalFilePath), Nothing)
                    Dim mClearLogo As String = If(Not String.IsNullOrEmpty(uMovie.ImagesContainer.ClearLogo.LocalFilePath), _
                                                  GetRemotePath(uMovie.ImagesContainer.ClearLogo.LocalFilePath), Nothing)
                    Dim mDiscArt As String = If(Not String.IsNullOrEmpty(uMovie.ImagesContainer.DiscArt.LocalFilePath), _
                                                  GetRemotePath(uMovie.ImagesContainer.DiscArt.LocalFilePath), Nothing)
                    Dim mFanart As String = If(Not String.IsNullOrEmpty(uMovie.ImagesContainer.Fanart.LocalFilePath), _
                                                 GetRemotePath(uMovie.ImagesContainer.Fanart.LocalFilePath), Nothing)
                    Dim mLandscape As String = If(Not String.IsNullOrEmpty(uMovie.ImagesContainer.Landscape.LocalFilePath), _
                                                  GetRemotePath(uMovie.ImagesContainer.Landscape.LocalFilePath), Nothing)
                    Dim mPoster As String = If(Not String.IsNullOrEmpty(uMovie.ImagesContainer.Poster.LocalFilePath), _
                                                  GetRemotePath(uMovie.ImagesContainer.Poster.LocalFilePath), Nothing)

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
                        logger.Error(String.Format("[APIKodi] [{0}] UpdateMovieInfo: {1}", _currenthost.Label, response))
                        Return False
                    Else
                        'Remove old textures (cache)
                        Dim resTextures = Await RemoveTextures(Directory.GetParent(uMovie.NfoPath).FullName)

                        'Send message to Kodi?
                        If blnSendHostNotification = True Then
                            Await SendMessage("Ember Media Manager", If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & uMovie.Movie.Title).ConfigureAwait(False)
                        End If
                        If blnNeedSave Then
                            logger.Trace(String.Format("[APIKodi] [{0}] UpdateMovieInfo: ""{1}"" | Save Playcount from host", _currenthost.Label, uMovie.Movie.Title))
                            Master.DB.SaveMovieToDB(uMovie, False, False, True, False)
                            'RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {uMovie.ID}))
                        End If
                        logger.Trace(String.Format("[APIKodi] [{0}] UpdateMovieInfo: ""{1}"" | {2} on host", _currenthost.Label, uMovie.Movie.Title, If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated"))))
                        Return True
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] UpdateMovieInfo: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, uMovie.Movie.Title))
                    Return False
                End If

            Catch ex As Exception
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
                logger.Error("[APIKodi] GetAllMovieSets: No host initialized! Abort!")
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
        Public Async Function SearchMovieSetByDetails(ByVal DBMovieSet As Database.DBElement) As Task(Of XBMCRPC.Video.Details.MovieSet)
            'get a list of all moviesets saved in Kodi DB
            Dim kMovieSets As VideoLibrary.GetMovieSetsResponse = Await GetAllMovieSets().ConfigureAwait(False)

            If kMovieSets IsNot Nothing Then
                If kMovieSets.sets IsNot Nothing Then

                    'compare by movieset title
                    For Each tMovieSet In kMovieSets.sets
                        If tMovieSet.title.ToLower = DBMovieSet.MovieSet.Title.ToLower Then
                            logger.Trace(String.Format("[APIKodi] [{0}] SearchMovieSetByDetails: ""{1}"" | OK, found in host database! [ID:{2}]", _currenthost.Label, DBMovieSet.MovieSet.Title, tMovieSet.setid))
                            Return tMovieSet
                        End If
                    Next

                    'compare by movies inside movieset
                    For Each tMovie In DBMovieSet.MovieList
                        logger.Trace(String.Format("[APIKodi] [{0}] SearchMovieSetByDetails: ""{1}"" | NOT found in database, trying to find the movieset by movies...", _currenthost.Label, DBMovieSet.MovieSet.Title))
                        'search movie ID in Kodi DB
                        Dim MovieID As Integer = -1
                        Dim KodiMovie = Await SearchMovieByPath(Directory.GetParent(tMovie.Filename).FullName).ConfigureAwait(False)
                        If KodiMovie Is Nothing Then
                            KodiMovie = Await SearchMovieByFilename(Path.GetFileName(tMovie.Filename)).ConfigureAwait(False)
                        End If
                        If KodiMovie IsNot Nothing Then
                            For Each tMovieSet In kMovieSets.sets
                                If tMovieSet.setid = KodiMovie.setid Then
                                    logger.Trace(String.Format("[APIKodi] [{0}] SearchMovieSetByDetails: ""{1}"" | OK, found in host database by movie ""{2}""! [ID:{3}]", _currenthost.Label, DBMovieSet.MovieSet.Title, KodiMovie.title, tMovieSet.setid))
                                    Return tMovieSet
                                End If
                            Next
                        End If
                    Next

                    logger.Warn(String.Format("[APIKodi] [{0}] SearchMovieSetByDetails: ""{1}"" | NOT found in host database!", _currenthost.Label, DBMovieSet.MovieSet.Title))
                    Return Nothing
                Else
                    logger.Warn(String.Format("[APIKodi] [{0}] SearchMovieSetByDetails: ""{1}"" | NOT found in host database!", _currenthost.Label, DBMovieSet.MovieSet.Title))
                    Return Nothing
                End If
            Else
                logger.Error(String.Format("[APIKodi] [{0}] SearchMovieSetByDetails: ""{1}"" | No connection to Host!", _currenthost.Label, DBMovieSet.MovieSet.Title))
                Return Nothing
            End If
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
        Public Async Function UpdateMovieSetInfo(ByVal lngMovieSetID As Long, ByVal blnSendHostNotification As Boolean) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] UpdateMovieSetInfo: No host initialized! Abort!")
                Return False
            End If

            Dim isNew As Boolean = False
            Dim uMovieset As Database.DBElement = Master.DB.LoadMovieSetFromDB(lngMovieSetID, False)

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] UpdateMovieSetInfo: ""{1}"" | Start syncing process...", _currenthost.Label, uMovieset.MovieSet.Title))

                'search movieset ID in Kodi DB
                Dim KodiMovieset As XBMCRPC.Video.Details.MovieSet = Await SearchMovieSetByDetails(uMovieset).ConfigureAwait(False)
                Dim KodiID As Integer = -1
                If KodiMovieset IsNot Nothing Then
                    KodiID = KodiMovieset.setid
                End If

                'movieset isn't in database of host -> scan directory
                If KodiID = -1 Then
                    logger.Error(String.Format("[APIKodi] [{0}] UpdateMovieSetInfo: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, uMovieset.MovieSet.Title))
                    Return False
                    'what to do in this case?
                End If

                If KodiID > -1 Then
                    'string or string.empty
                    Dim mTitle As String = uMovieset.MovieSet.Title

                    'string or null/nothing
                    Dim mBanner As String = If(Not String.IsNullOrEmpty(uMovieset.ImagesContainer.Banner.LocalFilePath), _
                                                  GetRemoteMovieSetPath(uMovieset.ImagesContainer.Banner.LocalFilePath), Nothing)
                    Dim mClearArt As String = If(Not String.IsNullOrEmpty(uMovieset.ImagesContainer.ClearArt.LocalFilePath), _
                                                  GetRemoteMovieSetPath(uMovieset.ImagesContainer.ClearArt.LocalFilePath), Nothing)
                    Dim mClearLogo As String = If(Not String.IsNullOrEmpty(uMovieset.ImagesContainer.ClearLogo.LocalFilePath), _
                                                  GetRemoteMovieSetPath(uMovieset.ImagesContainer.ClearLogo.LocalFilePath), Nothing)
                    Dim mDiscArt As String = If(Not String.IsNullOrEmpty(uMovieset.ImagesContainer.DiscArt.LocalFilePath), _
                                                  GetRemoteMovieSetPath(uMovieset.ImagesContainer.DiscArt.LocalFilePath), Nothing)
                    Dim mFanart As String = If(Not String.IsNullOrEmpty(uMovieset.ImagesContainer.Fanart.LocalFilePath), _
                                                 GetRemoteMovieSetPath(uMovieset.ImagesContainer.Fanart.LocalFilePath), Nothing)
                    Dim mLandscape As String = If(Not String.IsNullOrEmpty(uMovieset.ImagesContainer.Landscape.LocalFilePath), _
                                                  GetRemoteMovieSetPath(uMovieset.ImagesContainer.Landscape.LocalFilePath), Nothing)
                    Dim mPoster As String = If(Not String.IsNullOrEmpty(uMovieset.ImagesContainer.Poster.LocalFilePath), _
                                                  GetRemoteMovieSetPath(uMovieset.ImagesContainer.Poster.LocalFilePath), Nothing)

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
                        logger.Error(String.Format("[APIKodi] [{0}] UpdateMovieSetInfo: {1}", _currenthost.Label, response))
                        Return False
                    Else
                        'Remove old textures (cache) 'TODO: Limit to images of this MovieSet
                        Dim resTextures = Await RemoveTextures(_currenthost.MovieSetArtworksPath)

                        'Send message to Kodi?
                        If blnSendHostNotification = True Then
                            Await SendMessage("Ember Media Manager", If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & uMovieset.MovieSet.Title).ConfigureAwait(False)
                        End If
                        logger.Trace(String.Format("[APIKodi] [{0}] UpdateMovieSetInfo: ""{1}"" | {2} on host", _currenthost.Label, uMovieset.MovieSet.Title, If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated"))))
                        Return True
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] UpdateMovieSetInfo: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, uMovieset.MovieSet.Title))
                    Return False
                End If

            Catch ex As Exception
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
        Public Async Function GetAllTVEpisodesByDetails(ByVal strRemotePath As String, ByVal str As String, ByVal ShowID As Integer, ByVal intSeason As Integer, ByVal intEpisode As Integer) As Task(Of VideoLibrary.GetEpisodesResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetAllTVEpisodesByDetails: No host initialized! Abort!")
                Return Nothing
            End If

            If Not String.IsNullOrEmpty(strRemotePath) Then
                Try
                    Dim filter As New XBMCRPC.List.Filter.EpisodesAnd With {.and = New List(Of Object)}
                    Dim filterRulePath As New XBMCRPC.List.Filter.Rule.Episodes
                    filterRulePath.field = List.Filter.Fields.Episodes.path
                    filterRulePath.Operator = List.Filter.Operators.startswith
                    filterRulePath.value = strRemotePath
                    filter.and.Add(filterRulePath)
                    Dim filterRuleFilename As New XBMCRPC.List.Filter.Rule.Episodes
                    filterRuleFilename.field = List.Filter.Fields.Episodes.filename
                    filterRuleFilename.Operator = List.Filter.Operators.endswith
                    filterRuleFilename.value = Path.GetFileName(str)
                    filter.and.Add(filterRuleFilename)

                    'bug in KodiAPI, "Is" will no be changed to lower case "is" for JSON request
                    'same for "True" and "False"

                    'Dim filterRuleEpisode As New XBMCRPC.List.Filter.Rule.Episodes
                    'filterRuleEpisode.field = List.Filter.Fields.Episodes.episode
                    'filterRuleEpisode.Operator = List.Filter.Operators.is
                    'filterRuleEpisode.value = CStr(Episode)
                    'filter.and.Add(filterRuleEpisode)

                    Dim response = Await _kodi.VideoLibrary.GetEpisodes(filter, ShowID, intSeason, Video.Fields.Episode.AllFields).ConfigureAwait(False)
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
        Public Async Function SearchTVEpisodeByDetails(ByVal strLocalPath_TVShow As String, ByVal strLocalPath_Filename As String, ByVal intSeason As Integer, ByVal intEpisode As Integer) As Task(Of XBMCRPC.Video.Details.Episode)
            Dim KodiTVShow = Await SearchTVShowByPath(strLocalPath_TVShow).ConfigureAwait(False)
            Dim ShowID As Integer = -1
            If Not KodiTVShow Is Nothing Then
                ShowID = KodiTVShow.tvshowid
            End If
            If Not ShowID > -1 Then
                logger.Warn(String.Format("[APIKodi] [{0}] SearchTVEpisodeByDetails: ""{1}"" | NOT found in host database!", _currenthost.Label, strLocalPath_Filename))
                Return Nothing
            End If

            Dim strRemotePath As String = GetRemotePath(Directory.GetParent(strLocalPath_Filename).FullName)

            If Not String.IsNullOrEmpty(strRemotePath) Then
                'get a list of all episodes saved in Kodi DB by Path
                Dim kTVEpisodes As VideoLibrary.GetEpisodesResponse = Await GetAllTVEpisodesByDetails(strRemotePath, strLocalPath_Filename, ShowID, intSeason, intEpisode).ConfigureAwait(False)

                If kTVEpisodes IsNot Nothing Then
                    If kTVEpisodes.episodes IsNot Nothing Then
                        If kTVEpisodes.episodes.Count = 1 Then
                            logger.Trace(String.Format("[APIKodi] [{0}] SearchTVEpisodeByDetails: ""{1}"" | OK, found in host database! [ID:{2}]", _currenthost.Label, strLocalPath_Filename, kTVEpisodes.episodes.Item(0).episodeid))
                            Return kTVEpisodes.episodes.Item(0)
                        ElseIf kTVEpisodes.episodes.Count > 1 Then
                            'try to filter MultiEpisode files
                            Dim sEpisode = kTVEpisodes.episodes.Where(Function(f) f.episode = intEpisode)
                            If sEpisode.Count = 1 Then
                                Return sEpisode(0)
                            ElseIf sEpisode.Count > 1 Then
                                logger.Warn(String.Format("[APIKodi] [{0}] SearchTVEpisodeByDetails: ""{1}"" | MORE THAN ONE episode found in host database!", _currenthost.Label, strLocalPath_Filename))
                                Return Nothing
                            Else
                                logger.Warn(String.Format("[APIKodi] [{0}] SearchTVEpisodeByDetails: ""{1}"" | NOT found in host database!", _currenthost.Label, strLocalPath_Filename))
                                Return Nothing
                            End If
                        Else
                            logger.Warn(String.Format("[APIKodi] [{0}] SearchTVEpisodeByDetails: ""{1}"" | NOT found in host database!", _currenthost.Label, strLocalPath_Filename))
                            Return Nothing
                        End If
                    Else
                        logger.Warn(String.Format("[APIKodi] [{0}] SearchTVEpisodeByDetails: ""{1}"" | NOT found in host database!", _currenthost.Label, strLocalPath_Filename))
                        Return Nothing
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] SearchTVEpisodeByDetails: ""{1}"" | No connection to Host!", _currenthost.Label, strLocalPath_Filename))
                    Return Nothing
                End If
            Else
                logger.Error(String.Format("[APIKodi] [{0}] SearchTVEpisodeByDetails: ""{1}"" | Source not mapped!", _currenthost.Label, strLocalPath_Filename))
                Return Nothing
            End If
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
        Public Async Function UpdateTVEpisodeInfo(ByVal lngTVEpisodeID As Long, ByVal blnSendHostNotification As Boolean, ByVal blnSyncPlayCount As Boolean) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] UpdateTVEpisodeInfo: No host initialized! Abort!")
                Return False
            End If

            Dim needSave As Boolean = False
            Dim isNew As Boolean = False
            Dim uEpisode As Database.DBElement = Master.DB.LoadTVEpisodeFromDB(lngTVEpisodeID, True, False)

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] UpdateTVEpisodeInfo: ""{1}"" | Start syncing process...", _currenthost.Label, uEpisode.TVEpisode.Title))

                'search ID in Kodi DB
                Dim KodiEpsiode As XBMCRPC.Video.Details.Episode = Await SearchTVEpisodeByDetails(uEpisode.ShowPath, uEpisode.Filename, uEpisode.TVEpisode.Season, uEpisode.TVEpisode.Episode).ConfigureAwait(False)
                Dim KodiID As Integer = -1
                If Not KodiEpsiode Is Nothing Then
                    KodiID = KodiEpsiode.episodeid
                End If

                'episode isn't in database of host -> scan directory
                If KodiID = -1 Then
                    logger.Trace(String.Format("[APIKodi] [{0}] UpdateTVEpisodeInfo: ""{1}"" | NOT found in database, scan directory on host...", _currenthost.Label, uEpisode.TVEpisode.Title))
                    Await ScanVideoPath(lngTVEpisodeID, Enums.ContentType.TVEpisode).ConfigureAwait(False)
                    'wait a bit before trying going on, as scan might take a while on Kodi...
                    Threading.Thread.Sleep(1000) 'TODO better solution for this?!
                    KodiEpsiode = Await SearchTVEpisodeByDetails(uEpisode.ShowPath, uEpisode.Filename, uEpisode.TVEpisode.Season, uEpisode.TVEpisode.Episode).ConfigureAwait(False)
                    If Not KodiEpsiode Is Nothing Then
                        isNew = True
                        KodiID = KodiEpsiode.episodeid
                    End If
                End If

                If KodiID > -1 Then
                    'check if we have to retrieve the PlayCount from Kodi
                    If blnSyncPlayCount AndAlso Not uEpisode.TVEpisode.Playcount = KodiEpsiode.playcount Then
                        uEpisode.TVEpisode.Playcount = KodiEpsiode.playcount
                        uEpisode.TVEpisode.LastPlayed = KodiEpsiode.lastplayed
                        needSave = True
                    End If

                    'string or string.empty
                    Dim mDateAdded As String = uEpisode.TVEpisode.DateAdded
                    Dim mLastPlayed As String = uEpisode.TVEpisode.LastPlayed
                    Dim mPlot As String = uEpisode.TVEpisode.Plot
                    Dim mTitle As String = uEpisode.TVEpisode.Title

                    'digit grouping symbol for Votes count
                    Dim mVotes As String = If(Not String.IsNullOrEmpty(uEpisode.TVEpisode.Votes), uEpisode.TVEpisode.Votes, Nothing)
                    If Master.eSettings.GeneralDigitGrpSymbolVotes Then
                        If uEpisode.TVEpisode.VotesSpecified Then
                            Dim vote As String = Double.Parse(uEpisode.TVEpisode.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                            If vote IsNot Nothing Then
                                mVotes = vote
                            End If
                        End If
                    End If

                    'integer or 0
                    Dim mPlaycount As Integer = If(uEpisode.TVEpisode.PlaycountSpecified, CType(uEpisode.TVEpisode.Playcount, Integer), 0)
                    Dim mRating As Double = If(uEpisode.TVEpisode.RatingSpecified, CType(Double.Parse(uEpisode.TVEpisode.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), Double), 0)
                    Dim mRuntime As Integer = If(uEpisode.TVEpisode.RuntimeSpecified, CType(uEpisode.TVEpisode.Runtime, Integer), 0) * 60 'API requires runtime in seconds

                    'arrays
                    'Directors
                    Dim mDirectorList As List(Of String) = If(uEpisode.TVEpisode.DirectorsSpecified, uEpisode.TVEpisode.Directors, New List(Of String))

                    'Writers (Credits)
                    Dim mWriterList As List(Of String) = If(uEpisode.TVEpisode.CreditsSpecified, uEpisode.TVEpisode.Credits, New List(Of String))

                    'all image paths will be set in artwork object
                    Dim artwork As New Media.Artwork.Set
                    artwork.fanart = If(Not String.IsNullOrEmpty(uEpisode.ImagesContainer.Fanart.LocalFilePath), _
                                                  GetRemotePath(uEpisode.ImagesContainer.Fanart.LocalFilePath), Nothing)
                    artwork.poster = If(Not String.IsNullOrEmpty(uEpisode.ImagesContainer.Poster.LocalFilePath), _
                                                  GetRemotePath(uEpisode.ImagesContainer.Poster.LocalFilePath), Nothing)
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
                        logger.Error(String.Format("[APIKodi] [{0}] UpdateTVEpisodeInfo: {1}", _currenthost.Label, response))
                        Return False
                    Else
                        'Remove old textures (cache) 'TODO: Limit to images of this Epsiode
                        Dim resTextures = Await RemoveTextures(Directory.GetParent(uEpisode.Filename).FullName)

                        'Send message to Kodi?
                        If blnSendHostNotification = True Then
                            Await SendMessage("Ember Media Manager", If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & uEpisode.TVShow.Title & ": " & uEpisode.TVEpisode.Title).ConfigureAwait(False)
                        End If
                        If needSave Then
                            logger.Trace(String.Format("[APIKodi] [{0}] UpdateTVEpisodeInfo: ""{1}"" | Save Playcount from host", _currenthost.Label, uEpisode.TVEpisode.Title))
                            Master.DB.SaveTVEpisodeToDB(uEpisode, False, False, True, False, False)
                            'RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {uEpisode.ID}))
                        End If
                        logger.Trace(String.Format("[APIKodi] [{0}] UpdateTVEpisodeInfo: ""{1}"" | {2} on host", _currenthost.Label, uEpisode.TVEpisode.Title, If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated"))))
                        Return True
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] UpdateTVEpisodeInfo: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, uEpisode.TVEpisode.Title))
                    Return False
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function

#End Region 'TVEpisode API

#Region "TVSeason API"

        Public Async Function GetAllTVSeasonsByShowID(ByVal ShowID As Integer) As Task(Of VideoLibrary.GetSeasonsResponse)
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] GetAllMovieSets: No host initialized! Abort!")
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

        Public Async Function SearchTVSeasonByDetails(ByVal strLocalPath_TVShow As String, ByVal intSeason As Integer) As Task(Of XBMCRPC.Video.Details.Season)
            Dim KodiTVShow = Await SearchTVShowByPath(strLocalPath_TVShow).ConfigureAwait(False)
            Dim ShowID As Integer = -1
            If Not KodiTVShow Is Nothing Then
                ShowID = KodiTVShow.tvshowid
            End If
            If ShowID < 1 Then
                logger.Warn(String.Format("[APIKodi] [{0}] SearchTVSeasonByDetails: ""{1}: Season {2}"" | NOT found in host database!", _currenthost.Label, strLocalPath_TVShow, intSeason))
                Return Nothing
            End If

            'get a list of all seasons saved in Kodi DB by ShowID
            Dim kTVSeasons As VideoLibrary.GetSeasonsResponse = Await GetAllTVSeasonsByShowID(ShowID).ConfigureAwait(False)

            If kTVSeasons IsNot Nothing Then
                If kTVSeasons.seasons IsNot Nothing Then
                    Dim result = kTVSeasons.seasons.FirstOrDefault(Function(f) f.season = If(intSeason = 999, -1, intSeason))
                    If result IsNot Nothing Then
                        logger.Trace(String.Format("[APIKodi] [{0}] SearchTVSeasonByDetails: ""{1}: Season {2}"" | OK, found in host database! [ID:{3}]", _currenthost.Label, strLocalPath_TVShow, intSeason, result.seasonid))
                        Return result
                    Else
                        logger.Warn(String.Format("[APIKodi] [{0}] SearchTVSeasonByDetails: ""{1}: Season {2}"" | NOT found in host database!", _currenthost.Label, strLocalPath_TVShow, intSeason))
                        Return Nothing
                    End If
                Else
                    logger.Warn(String.Format("[APIKodi] [{0}] SearchTVSeasonByDetails: ""{1}: Season {2}"" | NOT found in host database!", _currenthost.Label, strLocalPath_TVShow, intSeason))
                    Return Nothing
                End If
            Else
                logger.Error(String.Format("[APIKodi] [{0}] SearchTVSeasonByDetails: ""{1}: Season {2}"" | No connection to Host!", _currenthost.Label, strLocalPath_TVShow, intSeason))
                Return Nothing
            End If
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
        Public Async Function UpdateTVSeasonInfo(ByVal lngTVSeasonID As Long, ByVal SendHostNotification As Boolean) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] UpdateTVSeasonInfo: No host initialized! Abort!")
                Return False
            End If

            Dim isNew As Boolean = False
            Dim uSeason As Database.DBElement = Master.DB.LoadTVSeasonFromDB(lngTVSeasonID, True, False)

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] UpdateTVSeasonInfo: ""{1}: Season {2}"" | Start syncing process...", _currenthost.Label, uSeason.ShowPath, uSeason.TVSeason.Season))

                'search season ID in Kodi DB
                Dim KodiSeason As XBMCRPC.Video.Details.Season = Await SearchTVSeasonByDetails(uSeason.ShowPath, uSeason.TVSeason.Season).ConfigureAwait(False)
                Dim KodiID As Integer = -1
                If Not KodiSeason Is Nothing Then
                    KodiID = KodiSeason.seasonid
                End If

                'season isn't in database of host -> scan show directory?
                If KodiID = -1 Then
                    logger.Trace(String.Format("[APIKodi] [{0}] UpdateTVSeasonInfo: ""{1}: Season {2}"" | NOT found in database, scan directory on host...", _currenthost.Label, uSeason.ShowPath, uSeason.TVSeason.Season))
                    'what to do in this case?
                    Await ScanVideoPath(uSeason.ShowID, Enums.ContentType.TVShow).ConfigureAwait(False)
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
                    Dim mBanner As String = If(Not String.IsNullOrEmpty(uSeason.ImagesContainer.Banner.LocalFilePath), _
                                                  GetRemotePath(uSeason.ImagesContainer.Banner.LocalFilePath), Nothing)
                    Dim mFanart As String = If(Not String.IsNullOrEmpty(uSeason.ImagesContainer.Fanart.LocalFilePath), _
                                                 GetRemotePath(uSeason.ImagesContainer.Fanart.LocalFilePath), Nothing)
                    Dim mLandscape As String = If(Not String.IsNullOrEmpty(uSeason.ImagesContainer.Landscape.LocalFilePath), _
                                                  GetRemotePath(uSeason.ImagesContainer.Landscape.LocalFilePath), Nothing)
                    Dim mPoster As String = If(Not String.IsNullOrEmpty(uSeason.ImagesContainer.Poster.LocalFilePath), _
                                                  GetRemotePath(uSeason.ImagesContainer.Poster.LocalFilePath), Nothing)

                    'all image paths will be set in artwork object
                    Dim artwork As New Media.Artwork.Set
                    artwork.banner = mBanner
                    artwork.fanart = mFanart
                    artwork.landscape = mLandscape
                    artwork.poster = mPoster

                    Dim response = Await _kodi.VideoLibrary.SetSeasonDetails(KodiID, _
                                                                             art:=artwork).ConfigureAwait(False)

                    If response.Contains("error") Then
                        logger.Error(String.Format("[APIKodi] [{0}] UpdateTVSeasonInfo: {1}", _currenthost.Label, response))
                        Return False
                    Else
                        'Remove old textures (cache) 'TODO: Limit to images of this Season
                        Dim resTextures = Await RemoveTextures(uSeason.ShowPath)

                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await SendMessage("Ember Media Manager", If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & uSeason.TVShow.Title & ": Season " & uSeason.TVSeason.Season).ConfigureAwait(False)
                        End If
                        logger.Trace(String.Format("[APIKodi] [{0}] UpdateTVSeasonInfo: ""{1}: Season {2}"" | {3} on host", _currenthost.Label, uSeason.ShowPath, uSeason.TVSeason.Season, If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated"))))
                        Return True
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] UpdateTVSeasonInfo: ""{1}: Season {2}"" | NOT found on host! Abort!", _currenthost.Label, uSeason.ShowPath, uSeason.TVSeason.Season))
                    Return False
                End If

            Catch ex As Exception
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
        Public Async Function GetAllTVShows() As Task(Of VideoLibrary.GetTVShowsResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetAllTVShows: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Dim response = Await _kodi.VideoLibrary.GetTVShows(Video.Fields.TVShow.AllFields).ConfigureAwait(False)
                Return response
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Get all tv shows by a given path
        ''' </summary>
        ''' <param name="RemotePath"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Async Function GetAllTVShowsByPath(ByVal strRemotePath As String) As Task(Of VideoLibrary.GetTVShowsResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetAllTVShowsByPath: No host initialized! Abort!")
                Return Nothing
            End If

            If Not String.IsNullOrEmpty(strRemotePath) Then
                Try
                    Dim filter As New XBMCRPC.List.Filter.TVShowsOr With {.or = New List(Of Object)}
                    Dim filterRule As New XBMCRPC.List.Filter.Rule.TVShows
                    filterRule.field = List.Filter.Fields.TVShows.path
                    filterRule.Operator = List.Filter.Operators.startswith
                    filterRule.value = strRemotePath
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
        Public Async Function SearchTVShowByPath(ByVal strLocalPath As String) As Task(Of XBMCRPC.Video.Details.TVShow)
            Dim strRemotePath As String = GetRemotePath(strLocalPath)

            If Not String.IsNullOrEmpty(strRemotePath) Then
                'get a list of all tv shows saved in Kodi DB by Path
                Dim kTVShows As VideoLibrary.GetTVShowsResponse = Await GetAllTVShowsByPath(strRemotePath).ConfigureAwait(False)

                If kTVShows IsNot Nothing Then
                    If kTVShows.tvshows IsNot Nothing Then
                        If kTVShows.tvshows.Count = 1 Then
                            logger.Trace(String.Format("[APIKodi] [{0}] SearchTVShowByPath: ""{1}"" | OK, found in host database! [ID:{2}]", _currenthost.Label, strLocalPath, kTVShows.tvshows.Item(0).tvshowid))
                            Return kTVShows.tvshows.Item(0)
                        ElseIf kTVShows.tvshows.Count > 1 Then
                            'TODO: add some comparisons to find the correct tv show like:
                            'TMDB ID, ...
                            logger.Warn(String.Format("[APIKodi] [{0}] SearchTVShowByPath: ""{1}"" | MORE THAN ONE tv show found in host database!", _currenthost.Label, strLocalPath))
                            Return Nothing
                        Else
                            logger.Warn(String.Format("[APIKodi] [{0}] SearchTVShowByPath: ""{1}"" | NOT found in host database!", _currenthost.Label, strLocalPath))
                            Return Nothing
                        End If
                    Else
                        logger.Warn(String.Format("[APIKodi] [{0}] SearchTVShowByPath: ""{1}"" | NOT found in host database!", _currenthost.Label, strLocalPath))
                        Return Nothing
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] SearchTVShowByPath: ""{1}"" | No connection to Host!", _currenthost.Label, strLocalPath))
                    Return Nothing
                End If
            Else
                logger.Error(String.Format("[APIKodi] [{0}] SearchTVShowByPath: ""{1}"" | Source not mapped!", _currenthost.Label, strLocalPath))
                Return Nothing
            End If
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
        Public Async Function UpdateTVShowInfo(ByVal lngTVShowID As Long, ByVal blnSendHostNotification As Boolean) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] UpdateTVShowInfo: No host initialized! Abort!")
                Return False
            End If

            Dim isNew As Boolean = False
            Dim uTVShow As Database.DBElement = Master.DB.LoadTVShowFromDB(lngTVShowID, False, False, False)

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] UpdateTVShowInfo: ""{1}"" | Start syncing process...", _currenthost.Label, uTVShow.TVShow.Title))

                'search ID in Kodi DB
                Dim KodiTVShow As XBMCRPC.Video.Details.TVShow = Await SearchTVShowByPath(uTVShow.ShowPath).ConfigureAwait(False)
                Dim KodiID As Integer = -1
                If Not KodiTVShow Is Nothing Then
                    KodiID = KodiTVShow.tvshowid
                End If

                'TVShow isn't in database of host -> scan directory
                If KodiID = -1 Then
                    logger.Trace(String.Format("[APIKodi] [{0}] UpdateTVShowInfo: ""{1}"" | NOT found in database, scan directory on host...", _currenthost.Label, uTVShow.TVShow.Title))
                    Await ScanVideoPath(lngTVShowID, Enums.ContentType.TVShow).ConfigureAwait(False)
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
                    Dim mBanner As String = If(Not String.IsNullOrEmpty(uTVShow.ImagesContainer.Banner.LocalFilePath), _
                                                  GetRemotePath(uTVShow.ImagesContainer.Banner.LocalFilePath), Nothing)
                    Dim mCharacterArt As String = If(Not String.IsNullOrEmpty(uTVShow.ImagesContainer.CharacterArt.LocalFilePath), _
                                               GetRemotePath(uTVShow.ImagesContainer.CharacterArt.LocalFilePath), Nothing)
                    Dim mClearArt As String = If(Not String.IsNullOrEmpty(uTVShow.ImagesContainer.ClearArt.LocalFilePath), _
                                                GetRemotePath(uTVShow.ImagesContainer.ClearArt.LocalFilePath), Nothing)
                    Dim mClearLogo As String = If(Not String.IsNullOrEmpty(uTVShow.ImagesContainer.ClearLogo.LocalFilePath), _
                                                GetRemotePath(uTVShow.ImagesContainer.ClearLogo.LocalFilePath), Nothing)
                    Dim mFanart As String = If(Not String.IsNullOrEmpty(uTVShow.ImagesContainer.Fanart.LocalFilePath), _
                                               GetRemotePath(uTVShow.ImagesContainer.Fanart.LocalFilePath), Nothing)
                    Dim mLandscape As String = If(Not String.IsNullOrEmpty(uTVShow.ImagesContainer.Landscape.LocalFilePath), _
                                              GetRemotePath(uTVShow.ImagesContainer.Landscape.LocalFilePath), Nothing)
                    Dim mPoster As String = If(Not String.IsNullOrEmpty(uTVShow.ImagesContainer.Poster.LocalFilePath), _
                                                 GetRemotePath(uTVShow.ImagesContainer.Poster.LocalFilePath), Nothing)
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
                        logger.Error(String.Format("[APIKodi] [{0}] UpdateTVShowInfo: {1}", _currenthost.Label, response))
                        Return False
                    Else
                        'Remove old textures (cache) 'TODO: Limit to images of this Show (exkl. all season and episode images)
                        Dim resTextures = Await RemoveTextures(uTVShow.ShowPath)

                        'Send message to Kodi?
                        If blnSendHostNotification = True Then
                            Await SendMessage("Ember Media Manager", If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & uTVShow.TVShow.Title).ConfigureAwait(False)
                        End If
                        logger.Trace(String.Format("[APIKodi] [{0}] UpdateTVShowInfo: ""{1}"" | {2} on host", _currenthost.Label, uTVShow.TVShow.Title, If(isNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated"))))
                        Return True
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] UpdateTVShowInfo: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, uTVShow.TVShow.Title))
                    Return False
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function

#End Region 'TVShow API

#Region "General API"
        ''' <summary>
        ''' Clean video library of host
        ''' </summary>
        ''' <returns>string with status message, if failed: Nothing</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Async Function CleanVideoLibrary() As Task(Of String)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] CleanVideoLibrary: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                'Listen to Kodi Events
                AddHandler _kodi.VideoLibrary.OnCleanFinished, AddressOf VideoLibrary_OnCleanFinished
                Await _kodi.StartNotificationListener()
                Dim response As String = String.Empty
                IsRunningTask = True

                response = Await _kodi.VideoLibrary.Clean.ConfigureAwait(False)
                While IsRunningTask = True
                    'Still cleaning library
                End While
                logger.Trace("[APIKodi] CleanVideoLibrary: " & _currenthost.Label)
                Return response
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Get JSONRPC version of host
        ''' </summary>
        ''' <param name="kHost">specific host to query</param>
        ''' <remarks>
        ''' 2015/06/29 Cocotus - First implementation
        ''' </remarks>
        Public Shared Function GetJSONHostVersion(ByVal kHost As KodiInterface.Host) As String
            Try
                Dim _APIKodi As New Kodi.APIKodi(kHost)
                Return _APIKodi.GetHostJSONVersion.Result
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return String.Empty
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
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetHostJSONVersion: No host initialized! Abort!")
                Return Nothing
            End If

            Try
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
                    Case "625"
                        codename = "Isengard "
                End Select
                Return codename & response.version.major.ToString & "." & response.version.minor
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return ""
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
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetSources: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Dim response = Await _kodi.Files.GetSources(mediaType).ConfigureAwait(False)
                Dim tmplist = response.sources.ToList

                'type multipath sources contain multiple paths
                Dim lstremotesources As New List(Of List.Items.SourcesItem)
                Dim paths As New List(Of String)
                Const MultiPath As String = "multipath://"
                For Each remotesource In tmplist
                    Dim newsource As New List.Items.SourcesItem
                    If remotesource.file.StartsWith(MultiPath) Then
                        logger.Warn("[APIKodi] GetSources: " & _currenthost.Label & ": " & remotesource.file & " - Multipath format, try to split...")
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
                        logger.Warn("[APIKodi] GetSources: " & _currenthost.Label & ": """ & remotesource.file, """")
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
        ''' Get all video sources configured in host
        ''' </summary>
        ''' <param name="kHost">specific host to query</param>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' Called from dlgHost.vb when user hits "Populate" button to get host sources
        ''' </remarks>
        Public Shared Function GetSources(ByVal kHost As KodiInterface.Host) As List(Of XBMCRPC.List.Items.SourcesItem)
            Dim listSources As New List(Of XBMCRPC.List.Items.SourcesItem)
            Try
                Dim _APIKodi As New Kodi.APIKodi(kHost)
                listSources = _APIKodi.GetSources(XBMCRPC.Files.Media.video).Result
                Return listSources
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
            Return listSources
        End Function
        ''' <summary>
        ''' Scan video library of Kodi host
        ''' </summary>
        ''' <returns>string with status message, if failed: Nothing</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Async Function ScanVideoLibrary() As Task(Of String)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] CleanVideoLibrary: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Dim response As String = String.Empty
                'Listen to Kodi Events
                AddHandler _kodi.VideoLibrary.OnScanFinished, AddressOf VideoLibrary_OnScanFinished
                Await _kodi.StartNotificationListener()
                IsRunningTask = True
                response = Await _kodi.VideoLibrary.Scan.ConfigureAwait(False)
                logger.Trace("[APIKodi] ScanVideoLibrary: " & _currenthost.Label)
                While IsRunningTask = True
                    'Still updating library
                End While
                Return response
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
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
        Public Async Function ScanVideoPath(ByVal lngEmbervideofileID As Long, ByVal ContentType As Enums.ContentType) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] ScanVideoPath: No host initialized! Abort!")
                Return Nothing
            End If

            Dim strLocalPath As String = String.Empty

            Select Case ContentType
                Case Enums.ContentType.Movie
                    Dim uMovie As Database.DBElement = Master.DB.LoadMovieFromDB(lngEmbervideofileID)
                    If FileUtils.Common.isBDRip(uMovie.Filename) Then
                        'filename must point to m2ts file! 
                        'Ember-Filepath i.e.  E:\Media_1\Movie\Horror\Europa Report\BDMV\STREAM\00000.m2ts
                        'for adding new Bluray rips scan the root folder of movie, i.e: E:\Media_1\Movie\Horror\Europa Report\
                        strLocalPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(uMovie.Filename).FullName).FullName).FullName
                    ElseIf FileUtils.Common.isVideoTS(uMovie.Filename) Then
                        'filename must point to IFO file!
                        'Ember-Filepath i.e.  E:\Media_1\Movie\Action\Crow\VIDEO_TS\VIDEO_TS.IFO
                        'for adding new DVDs scan the root folder of movie, i.e:  E:\Media_1\Movie\Action\Crow\
                        strLocalPath = Directory.GetParent(Directory.GetParent(uMovie.Filename).FullName).FullName
                    Else
                        If Path.GetFileNameWithoutExtension(uMovie.Filename).ToLower = "video_ts" Then
                            strLocalPath = Directory.GetParent(Directory.GetParent(uMovie.Filename).FullName).FullName
                        Else
                            strLocalPath = Directory.GetParent(uMovie.Filename).FullName
                        End If
                    End If
                Case Enums.ContentType.TVShow
                    Dim uShow As Database.DBElement = Master.DB.LoadTVShowFromDB(lngEmbervideofileID, False, False)
                    If FileUtils.Common.isBDRip(uShow.ShowPath) Then
                        'needs some testing?!
                        strLocalPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(uShow.ShowPath).FullName).FullName).FullName
                    ElseIf FileUtils.Common.isVideoTS(uShow.ShowPath) Then
                        'needs some testing?!
                        strLocalPath = Directory.GetParent(Directory.GetParent(uShow.ShowPath).FullName).FullName
                    Else
                        strLocalPath = uShow.ShowPath
                    End If
                Case Enums.ContentType.TVEpisode
                    Dim uEpisode As Database.DBElement = Master.DB.LoadTVEpisodeFromDB(lngEmbervideofileID, False)
                    If FileUtils.Common.isBDRip(uEpisode.Filename) Then
                        'needs some testing?!
                        strLocalPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(uEpisode.Filename).FullName).FullName).FullName
                    ElseIf FileUtils.Common.isVideoTS(uEpisode.Filename) Then
                        'needs some testing?!
                        strLocalPath = Directory.GetParent(Directory.GetParent(uEpisode.Filename).FullName).FullName
                    Else
                        strLocalPath = Directory.GetParent(uEpisode.Filename).FullName
                    End If
                Case Else
                    logger.Warn(String.Format("[APIKodi] [{0}] ScanVideoPath: No videotype specified! Abort!", _currenthost.Label))
                    Return False
            End Select

            Dim strRemotePath As String = GetRemotePath(strLocalPath)
            If strRemotePath Is Nothing Then
                Return False
            End If
            Dim strResponse = Await _kodi.VideoLibrary.Scan(strRemotePath).ConfigureAwait(False)
            If strResponse.ToLower.Contains("error") Then
                logger.Trace(String.Format("[APIKodi] [{0}] ScanVideoPath: ""{1}"" | {2}", _currenthost.Label, strRemotePath, strResponse))
                Return False
            Else
                logger.Trace(String.Format("[APIKodi] [{0}] ScanVideoPath: ""{1}"" | {2}", _currenthost.Label, strRemotePath, strResponse))
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
        Public Async Function SendMessage(ByVal strTitle As String, ByVal strMessage As String) As Task(Of String)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] SendMessage: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Dim response = Await _kodi.GUI.ShowNotification(strTitle, strMessage, 2500).ConfigureAwait(False)
                Return response
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function

        Public Async Function GetTextures(ByVal strMoviePath As String) As Task(Of XBMCRPC.Textures.GetTexturesResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] SendMessage: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Dim filter As New XBMCRPC.List.Filter.TexturesOr With {.or = New List(Of Object)}
                Dim filterRule As New XBMCRPC.List.Filter.Rule.Textures
                filterRule.field = List.Filter.Fields.Textures.url
                filterRule.Operator = List.Filter.Operators.startswith
                filterRule.value = strMoviePath
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
        Public Async Function RemoveTexture(ByVal intID As Integer) As Task(Of String)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] SendMessage: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Dim response = Await _kodi.Textures.RemoveTexture(intID)
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
        Public Async Function RemoveTextures(ByVal strLocalPath As String) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] SendMessage: No host initialized! Abort!")
                Return False
            End If

            Try
                Dim TexturesResponce = Await GetTextures(GetRemotePath(strLocalPath))
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
        ''' 
        ''' </summary>
        ''' <param name="LocalPath"></param>
        ''' <returns></returns>
        ''' <remarks>ATTENTION: It's not allowed to use "Remotepath.ToLower" (Kodi can't find UNC sources with wrong case)</remarks>
        Function GetRemotePath(ByVal strLocalPath As String) As String
            Dim RemotePath As String = String.Empty
            Dim RemoteIsUNC As Boolean = False

            For Each Source In _currenthost.Sources
                Dim tLocalSource As String = String.Empty
                'add a directory separator at the end of the path to distinguish between
                'D:\Movies
                'D:\Movies Shared
                '(needed for "LocalPath.ToLower.StartsWith(tLocalSource)"
                If Source.LocalPath.Contains(Path.DirectorySeparatorChar) Then
                    tLocalSource = If(Source.LocalPath.EndsWith(Path.DirectorySeparatorChar), Source.LocalPath, String.Concat(Source.LocalPath, Path.DirectorySeparatorChar)).Trim
                ElseIf Source.LocalPath.Contains(Path.AltDirectorySeparatorChar) Then
                    tLocalSource = If(Source.LocalPath.EndsWith(Path.AltDirectorySeparatorChar), Source.LocalPath, String.Concat(Source.LocalPath, Path.AltDirectorySeparatorChar)).Trim
                End If
                If strLocalPath.ToLower.StartsWith(tLocalSource.ToLower) Then
                    Dim tRemoteSource As String = String.Empty
                    If Source.RemotePath.Contains(Path.DirectorySeparatorChar) Then
                        tRemoteSource = If(Source.RemotePath.EndsWith(Path.DirectorySeparatorChar), Source.RemotePath, String.Concat(Source.RemotePath, Path.DirectorySeparatorChar)).Trim
                    ElseIf Source.RemotePath.Contains(Path.AltDirectorySeparatorChar) Then
                        tRemoteSource = If(Source.RemotePath.EndsWith(Path.AltDirectorySeparatorChar), Source.RemotePath, String.Concat(Source.RemotePath, Path.AltDirectorySeparatorChar)).Trim
                        RemoteIsUNC = True
                    End If
                    RemotePath = strLocalPath.Replace(tLocalSource, tRemoteSource)
                    If RemoteIsUNC Then
                        RemotePath = RemotePath.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                    Else
                        RemotePath = RemotePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)
                    End If
                    Exit For
                End If
            Next

            If String.IsNullOrEmpty(RemotePath) Then logger.Error(String.Format("[APIKodi] [{0}] GetRemotePath: ""{1}"" | Source not mapped!", _currenthost.Label, strLocalPath))

            Return RemotePath
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="LocalPath"></param>
        ''' <returns></returns>
        ''' <remarks>ATTENTION: It's not allowed to use "Remotepath.ToLower" (Kodi can't find UNC sources with wrong case)</remarks>
        Function GetRemoteMovieSetPath(ByVal strLocalPath As String) As String
            Dim HostPath As String = _currenthost.MovieSetArtworksPath
            Dim RemotePath As String = String.Empty
            Dim RemoteIsUNC As Boolean = False

            For Each Source In Master.eSettings.GetMovieSetsArtworkPaths()
                Dim tLocalSource As String = String.Empty
                'add a directory separator at the end of the path to distinguish between
                'D:\MovieSetsArtwork
                'D:\MovieSetsArtwork Shared
                '(needed for "LocalPath.ToLower.StartsWith(tLocalSource)"
                If Source.Contains(Path.DirectorySeparatorChar) Then
                    tLocalSource = If(Source.EndsWith(Path.DirectorySeparatorChar), Source, String.Concat(Source, Path.DirectorySeparatorChar)).Trim
                ElseIf Source.Contains(Path.AltDirectorySeparatorChar) Then
                    tLocalSource = If(Source.EndsWith(Path.AltDirectorySeparatorChar), Source, String.Concat(Source, Path.AltDirectorySeparatorChar)).Trim
                End If
                If strLocalPath.ToLower.StartsWith(tLocalSource.ToLower) Then
                    Dim tRemoteSource As String = String.Empty
                    If HostPath.Contains(Path.DirectorySeparatorChar) Then
                        tRemoteSource = If(HostPath.EndsWith(Path.DirectorySeparatorChar), HostPath, String.Concat(HostPath, Path.DirectorySeparatorChar)).Trim
                    ElseIf HostPath.Contains(Path.AltDirectorySeparatorChar) Then
                        tRemoteSource = If(HostPath.EndsWith(Path.AltDirectorySeparatorChar), HostPath, String.Concat(HostPath, Path.AltDirectorySeparatorChar)).Trim
                        RemoteIsUNC = True
                    End If
                    RemotePath = strLocalPath.Replace(tLocalSource, tRemoteSource)
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
