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
            Dim uMovie As Structures.DBMovie = Master.DB.LoadMovieFromDB(EmbermovieID)
            Try
                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] UpdateMovieInfo: No client initialized! Abort!")
                    Return False
                End If
                ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Start Syncing") & ": " & uMovie.Filename, New Bitmap(My.Resources.logo)}))

                'search movie ID in Kodi DB
                Dim KodiID As Integer = Await GetHostMovieIdByFilename(uMovie.Filename).ConfigureAwait(False)

                'movie isn't in database of host -> scan directory
                If KodiID = -1 Then
                    logger.Warn("[APIKodi] UpdateMovieInfo: " & _currenthost.name & ": " & uMovie.Filename & ": Not found in database, scan directory...")
                    Await ScanVideoPath(EmbermovieID, "movie").ConfigureAwait(False)
                    'wait a bit before trying going on, as scan might take a while on Kodi...
                    Threading.Thread.Sleep(1000) 'TODO better solution for this?!
                    KodiID = Await GetHostMovieIdByFilename(uMovie.Filename).ConfigureAwait(False)
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
                    Dim mTrailer As String = If(Not String.IsNullOrEmpty(uMovie.TrailerPath), GetRemoteFilePath(uMovie.TrailerPath), If(uMovie.Movie.TrailerSpecified, uMovie.Movie.Trailer, String.Empty))

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
                                                  GetRemoteFilePath(uMovie.BannerPath), Nothing)
                    Dim mClearArt As String = If(Not String.IsNullOrEmpty(uMovie.ClearArtPath), _
                                                  GetRemoteFilePath(uMovie.ClearArtPath), Nothing)
                    Dim mClearLogo As String = If(Not String.IsNullOrEmpty(uMovie.ClearLogoPath), _
                                                  GetRemoteFilePath(uMovie.ClearLogoPath), Nothing)
                    Dim mDiscArt As String = If(Not String.IsNullOrEmpty(uMovie.DiscArtPath), _
                                                  GetRemoteFilePath(uMovie.DiscArtPath), Nothing)
                    Dim mFanart As String = If(Not String.IsNullOrEmpty(uMovie.FanartPath), _
                                                 GetRemoteFilePath(uMovie.FanartPath), Nothing)
                    Dim mLandscape As String = If(Not String.IsNullOrEmpty(uMovie.LandscapePath), _
                                                  GetRemoteFilePath(uMovie.LandscapePath), Nothing)
                    Dim mPoster As String = If(Not String.IsNullOrEmpty(uMovie.PosterPath), _
                                                  GetRemoteFilePath(uMovie.PosterPath), Nothing)

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
                        logger.Trace("[APIKodi] UpdateMovieInfo: " & _currenthost.name & ": " & Master.eLang.GetString(9999, "Updated") & ": " & uMovie.Filename)
                        ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync OK") & ": " & uMovie.Filename, New Bitmap(My.Resources.logo)}))
                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await SendMessage("Ember MediaManager", Master.eLang.GetString(9999, "Updated") & ": " & uMovie.Filename).ConfigureAwait(False)
                        End If
                        Return True
                    End If
                Else
                    logger.Trace("[APIKodi] UpdateMovieInfo: " & _currenthost.name & ": " & Master.eLang.GetString(9999, "Not Found On Host") & ": " & uMovie.Filename)
                    '   ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Not Found On Host") & ": " & uMovie.Filename, Nothing}))
                    Return False
                End If

            Catch ex As Exception
                ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & uMovie.Filename, Nothing}))
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Get movieID at Kodi host for a specific movie
        ''' </summary>
        ''' <returns>movieID of host, -1: error</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' We assume that filename/path to video is unique and can be used to find correct videofile in Host DB
        ''' Notice: No exception handling here because this function is called/nested in other functions and an exception must not be consumed (meaning a disconnect host would not be recognized at once)
        ''' </remarks>
        Public Async Function GetHostMovieIdByFilename(ByVal Filename As String) As Task(Of Integer)
            Dim movieID As Integer = -1
            If FileUtils.Common.isBDRip(Filename) Then
                'i.e.  E:\Media_1\Movie\Horror\Europa Report\BDMV\STREAM\00000.m2ts
                'since Filename is always style like xx..m2ts (filename isn't unique) for every Bluray we need special handling here...
                'idea: compare path to STREAM folder, i.e:  Media_1/Movie/Horror/Europa Report/BDMV/STREAM/00000.m2ts

                'if no seperator is specified use pathseperator of current system (=Windows)
                Dim remotepathseparator = _currenthost.remotepathseparator
                If String.IsNullOrEmpty(remotepathseparator) Then remotepathseparator = IO.Path.DirectorySeparatorChar
                'first strip driveletter (since it will probably not be identical between Kodi and Ember (i.e. mapped drive))
                Filename = Filename.Replace(IO.Directory.GetDirectoryRoot(Filename), "")
                'result i.e:  Media_1\Movie\Horror\Europa Report\BDMV\STREAM\00000.m2ts
                'now replace DirectorySeparatorChar for comparison
                Filename = Filename.Replace(IO.Path.DirectorySeparatorChar, remotepathseparator)
                'result i.e:  Media_1/Movie/Horror/Europa Report/BDMV/STREAM/00000.m2ts

            ElseIf FileUtils.Common.isVideoTS(Filename) Then
                'i.e.  E:\Media_1\Movie\Action\Crow\VIDEO_TS\VIDEO_TS.IFO
                'since Filename is always VIDEO_TS.IFO (filename isn't unique) for every DVD we need special handling here...
                'idea: compare path to VIDEO_TS folder, i.e:  Media_1/Movie/Action/Crow/VIDEO_TS/VIDEO_TS.IFO

                'if no seperator is specified use pathseperator of current system (=Windows)
                Dim remotepathseparator = _currenthost.remotepathseparator
                If String.IsNullOrEmpty(remotepathseparator) Then remotepathseparator = IO.Path.DirectorySeparatorChar
                'first strip driveletter (since it will probably not be identical between Kodi and Ember (i.e. mapped drive))
                Filename = Filename.Replace(IO.Directory.GetDirectoryRoot(Filename), "")
                'result i.e:  Media_1\Movie\Action\Crow\VIDEO_TS\VIDEO_TS.IFO
                'now replace DirectorySeparatorChar for comparison
                Filename = Filename.Replace(IO.Path.DirectorySeparatorChar, remotepathseparator)
                'result i.e:  Media_1/Movie/Action/Crow/VIDEO_TS/VIDEO_TS.IFO
            Else
                'only need filename to compare (not full path!)
                'i.e Filename: Child's Play.1988.HDTV.mkv
                Filename = IO.Path.GetFileName(Filename)
            End If

            'get a list of all movies saved in Kodi DB
            Dim kMovies As List(Of Video.Details.Movie) = Await GetAllMovies().ConfigureAwait(False)
            'compare filenames of remote and local filenames to identify movie
            If Not kMovies Is Nothing Then
                For Each kMovie In kMovies
                    'kmovie.file ie. "nfs://192.168.2.200/Media_2/Movie/Horror/Chucky 1/Child's Play.1988.HDTV.mkv"
                    'since kmovie.file represents fullpath, compare only last part (filename)
                    If kMovie.file.ToLower.EndsWith(Filename.ToLower) Then
                        logger.Trace(String.Concat("[APIKodi] GetHostMovieIdByFilename: " & _currenthost.name & ": " & Filename & " found in host database!"))
                        movieID = kMovie.movieid
                        Return movieID
                    End If
                Next
            End If
            logger.Trace(String.Concat("[APIKodi] GetHostMovieIdByFilename: " & _currenthost.name & ": " & Filename & " NOT found in host database!"))
            Return movieID
        End Function
        ''' <summary>
        ''' Get all movies from Kodi host
        ''' </summary>
        ''' <returns>list of kodi movies, Nothing: error</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' Notice: No exception handling here because this function is called/nested in other functions and an exception must not be consumed (meaning a disconnect host would not be recognized at once)
        ''' </remarks>
        Public Async Function GetAllMovies() As Task(Of List(Of Video.Details.Movie))
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] GetAllMovies: No client initialized! Abort!")
                Return Nothing
            End If
            Dim response = Await _kodi.VideoLibrary.GetMovies(Video.Fields.Movie.AllFields).ConfigureAwait(False)
            Return response.movies.ToList()
        End Function

#End Region 'Movie API

#Region "TVShow API"
        ''' <summary>
        ''' Update TVShow details at Kodi
        ''' </summary>
        ''' <param name="movieID">ID of specific tvshow (EmberDB)</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Update successfull, false=error or show not found in KodiDB</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' updates all TVShow fields (also paths of images)
        ''' at the moment TVShow on host is identified by searching and comparing path of TVShow
        ''' </remarks>
        Public Async Function UpdateTVShowInfo(ByVal showID As Long, ByVal SendHostNotification As Boolean) As Task(Of Boolean)
            Dim uTVShow As Structures.DBTV = Master.DB.LoadTVShowFromDB(showID)
            Try

                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] UpdateTVShowInfo: No client initialized! Abort!")
                    Return False
                End If

                ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Start Syncing") & ": " & uTVShow.ShowPath, New Bitmap(My.Resources.logo)}))

                'search ID in Kodi DB
                Dim KodiID As Integer = Await GetHostTVShowIdByPath(uTVShow.ShowPath).ConfigureAwait(False)

                'TVShow isn't in database of host -> scan directory
                If KodiID = -1 Then
                    logger.Warn("[APIKodi] UpdateTVShowInfo: " & _currenthost.name & ": " & uTVShow.ShowPath & ": Not found in database, scan directory...")
                    Await ScanVideoPath(showID, "tvshow").ConfigureAwait(False)
                    'wait a bit before trying going on, as scan might take a while on Kodi...
                    Threading.Thread.Sleep(1000) 'TODO better solution for this?!
                    KodiID = Await GetHostTVShowIdByPath(uTVShow.ShowPath)
                End If

                If KodiID > -1 Then

                    'TODO missing:
                    ' Dim mOriginalTitle As String = Web.HttpUtility.JavaScriptStringEncode(uTVShow.TVShow.OriginalTitle, True)
                    ' Dim mPlaycount As String = If(uTVShow.TVShow.PlayCountSpecified, uTVShow.TVShow.PlayCount, "0")
                    ' Dim mLastPlayed As String = If(uEpisode.TVEp.LastPlayedSpecified, Web.HttpUtility.JavaScriptStringEncode(uEpisode.TVEp.LastPlayed, True), "null")

                    'string or string.empty
                    Dim mEpisodeGuide As String = uTVShow.TVShow.EpisodeGuide.URL
                    Dim mImdbnumber As String = uTVShow.TVShow.TVDBID
                    Dim mMPAA As String = uTVShow.TVShow.MPAA
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
                    Dim mBanner As String = If(Not String.IsNullOrEmpty(uTVShow.ShowBannerPath), _
                                                  GetRemoteFilePath(uTVShow.ShowBannerPath), Nothing)
                    Dim mCharacterArt As String = If(Not String.IsNullOrEmpty(uTVShow.ShowCharacterArtPath), _
                                               GetRemoteFilePath(uTVShow.ShowCharacterArtPath), Nothing)
                    Dim mClearArt As String = If(Not String.IsNullOrEmpty(uTVShow.ShowClearArtPath), _
                                                GetRemoteFilePath(uTVShow.ShowClearArtPath), Nothing)
                    Dim mClearLogo As String = If(Not String.IsNullOrEmpty(uTVShow.ShowClearLogoPath), _
                                                GetRemoteFilePath(uTVShow.ShowClearLogoPath), Nothing)
                    Dim mFanart As String = If(Not String.IsNullOrEmpty(uTVShow.ShowFanartPath), _
                                               GetRemoteFilePath(uTVShow.ShowFanartPath), Nothing)
                    Dim mLandscape As String = If(Not String.IsNullOrEmpty(uTVShow.ShowLandscapePath), _
                                              GetRemoteFilePath(uTVShow.ShowLandscapePath), Nothing)
                    Dim mPoster As String = If(Not String.IsNullOrEmpty(uTVShow.ShowPosterPath), _
                                                 GetRemoteFilePath(uTVShow.ShowPosterPath), Nothing)
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
                                                                        sorttitle:=mSortTitle, _
                                                                        episodeguide:=mEpisodeGuide, _
                                                                        tag:=mTagList, _
                                                                        art:=artwork).ConfigureAwait(False)
                    'not supported right now in Ember
                    'originaltitle:=moriginaltitle, _    
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
                        logger.Trace("[APIKodi] UpdateMovieInfo: " & _currenthost.name & ": " & Master.eLang.GetString(9999, "Updated") & ": " & uTVShow.ShowPath)
                        '  ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync OK") & ": " & uTVShow.ShowPath, New Bitmap(My.Resources.logo)}))
                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await SendMessage("Ember MediaManager", Master.eLang.GetString(9999, "Updated") & ": " & uTVShow.ShowPath).ConfigureAwait(False)
                        End If
                        Return True
                    End If
                Else
                    logger.Trace("[APIKodi] UpdateTVShowInfo: " & _currenthost.name & ": " & Master.eLang.GetString(9999, "Not Found On Host") & ": " & uTVShow.ShowPath)
                    '   ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Not Found On Host") & ": " & uTVShow.ShowPath, Nothing}))
                    Return False
                End If
            Catch ex As Exception
                ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & uTVShow.ShowPath, Nothing}))
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Get TVShowID at Kodi host for a specific TVShow
        ''' </summary>
        ''' <returns>TVShowID of host, -1: error</returns>
        ''' <remarks>
        ''' 2015/06/28 Cocotus - First implementation
        ''' Notice: No exception handling here because this function is called/nested in other functions and an exception must not be consumed (meaning a disconnect host would not be recognized at once)
        ''' </remarks>
        Public Async Function GetHostTVShowIdByPath(ByVal TVShowPath As String) As Task(Of Integer)
            Dim tvshowid As Integer = -1
            'get a list of all shows saved in Kodi DB
            Dim kTVShows As List(Of Video.Details.TVShow) = Await GetAllTVShows().ConfigureAwait(False)
            'compare path of remote and local path to identify tvshow
            If Not kTVShows Is Nothing Then
                'only need directoryname of show to compare (not full path!)
                TVShowPath = IO.Path.GetFileNameWithoutExtension(TVShowPath)
                'add last seperator, because file property of kTVShow always end with backslash
                If TVShowPath.EndsWith(_currenthost.remotepathseparator) = False Then
                    TVShowPath = TVShowPath & _currenthost.remotepathseparator
                End If
                For Each kTVShow In kTVShows
                    'since kTVShow.file represents fullpath, compare only last part
                    If kTVShow.file.ToLower.EndsWith(TVShowPath.ToLower) Then
                        logger.Trace(String.Concat("[APIKodi] GetHostTVShowIdByPath: " & _currenthost.name & ": " & TVShowPath & " found in host database!"))
                        tvshowid = kTVShow.tvshowid
                        Return tvshowid
                    End If
                Next
            End If
            logger.Trace(String.Concat("[APIKodi] GetHostTVShowIdByPath: " & _currenthost.name & ": " & TVShowPath & " NOT found in host database!"))
            Return tvshowid
        End Function
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
            Dim response = Await _kodi.VideoLibrary.GetTVShows(properties:=Video.Fields.TVShow.AllFields).ConfigureAwait(False)
            Return response.tvshows.ToList()
        End Function

#End Region 'TVShow API

#Region "Episode API"
        ''' <summary>
        ''' Update episode details at Kodi
        ''' </summary>
        ''' <param name="movieID">ID of specific episode (EmberDB)</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Update successfull, false=error or episode not found in KodiDB</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' updates all episode fields (also pathes of images)
        ''' at the moment episode on host is identified by searching and comparing filename of episode
        ''' </remarks>
        Public Async Function UpdateEpisodeInfo(ByVal episodeID As Long, ByVal SendHostNotification As Boolean) As Task(Of Boolean)
            Dim uEpisode As Structures.DBTV = Master.DB.LoadTVEpFromDB(episodeID, True)

            Try

                If _kodi Is Nothing Then
                    logger.Warn("[APIKodi] UpdateTVShowInfo: No client initialized! Abort!")
                    Return False
                End If

                'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Start Syncing") & ": " & uEpisode.Filename, New Bitmap(My.Resources.logo)}))

                'search ID in Kodi DB
                Dim KodiID As Integer = Await GetHostEpisodeIdByFilename(uEpisode.Filename, uEpisode.ShowPath, uEpisode.TVEp.Season).ConfigureAwait(False)

                'episode isn't in database of host -> scan directory
                If KodiID = -1 Then
                    logger.Warn("[APIKodi] UpdateEpisodeInfo: " & _currenthost.name & ": " & uEpisode.Filename & ": Not found in database, scan directory...")
                    Await ScanVideoPath(episodeID, "episode").ConfigureAwait(False)
                    'wait a bit before trying going on, as scan might take a while on Kodi...
                    Threading.Thread.Sleep(1000) 'TODO better solution for this?!
                    KodiID = Await GetHostEpisodeIdByFilename(uEpisode.Filename, uEpisode.ShowPath, uEpisode.TVEp.Season).ConfigureAwait(False)
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
                    artwork.fanart = If(Not String.IsNullOrEmpty(uEpisode.EpFanartPath), _
                                                  GetRemoteFilePath(uEpisode.EpFanartPath), Nothing)
                    artwork.poster = If(Not String.IsNullOrEmpty(uEpisode.EpPosterPath), _
                                                  GetRemoteFilePath(uEpisode.EpPosterPath), Nothing)
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
                        logger.Error("[APIKodi] UpdateEpisodeInfo: " & _currenthost.name & ": " & response)
                        '  ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & uEpisode.Filename, Nothing}))
                        Return False
                    Else
                        logger.Trace("[APIKodi] UpdateMovieInfo: " & _currenthost.name & ": " & Master.eLang.GetString(9999, "Updated") & ": " & uEpisode.Filename)
                        '  ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync OK") & ": " & uEpisode.Filename, New Bitmap(My.Resources.logo)}))
                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await SendMessage("Ember MediaManager", Master.eLang.GetString(9999, "Updated") & ": " & uEpisode.Filename).ConfigureAwait(False)
                        End If
                        Return True
                    End If
                Else
                    logger.Trace("[APIKodi] UpdateEpisodeInfo: " & _currenthost.name & ": " & Master.eLang.GetString(9999, "Not Found On Host") & ": " & uEpisode.Filename)
                    ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Not Found On Host") & ": " & uEpisode.Filename, Nothing}))
                    Return False
                End If
            Catch ex As Exception
                ' ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", _currenthost.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & uEpisode.Filename, Nothing}))
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Get episodeID at Kodi host for a specific episode
        ''' </summary>
        ''' <returns>episodeID of host, -1: error</returns>
        ''' <remarks>
        ''' 2015/06/28 Cocotus - First implementation
        ''' Notice: No exception handling here because this function is called/nested in other functions and an exception must not be consumed (meaning a disconnect host would not be recognized at once)
        ''' </remarks>
        Public Async Function GetHostEpisodeIdByFilename(ByVal Filename As String, ByVal TVShowPath As String, ByVal Season As Integer) As Task(Of Integer)
            Dim episodeID As Integer = -1

            Dim ShowID As Integer = Await GetHostTVShowIdByPath(TVShowPath).ConfigureAwait(False)
            If ShowID < 1 Then
                logger.Trace(String.Concat("[APIKodi] GetHostEpisodeIdByFilename: " & _currenthost.name & ": " & Filename & " NOT found in host database!"))
                Return -1
            End If
            'get a list of all episodes saved in Kodi DB
            Dim kEpisodes As List(Of Video.Details.Episode) = Await GetAllEpisodes(ShowID, Season).ConfigureAwait(False)


            'only need filename to compare (not full path!)
            Filename = IO.Path.GetFileName(Filename)
            'compare filenames of remote and local filenames to identify episode
            If Not kEpisodes Is Nothing Then
                For Each kEpisode In kEpisodes
                    If kEpisode.file.ToLower.EndsWith(Filename.ToLower) Then
                        'since kEpisode.file represents fullpath, compare only last part (filename)
                        logger.Trace(String.Concat("[APIKodi] GetHostEpisodeIdByFilename: " & _currenthost.name & ": " & Filename & " found in host database!"))
                        episodeID = kEpisode.episodeid
                        Return episodeID
                    End If
                Next
            End If
            logger.Trace(String.Concat("[APIKodi] GetHostEpisodeIdByFilename: " & _currenthost.name & ": " & Filename & " NOT found in host database!"))
            Return episodeID
        End Function
        ''' <summary>
        ''' Get all episodes from Kodi host
        ''' </summary>
        ''' <returns>list of kodi episodes, Nothing: error</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' Notice: No exception handling here because this function is called/nested in other functions and an exception must not be consumed (meaning a disconnect host would not be recognized at once) 
        ''' </remarks>
        Public Async Function GetAllEpisodes(ByVal ShowID As Integer, ByVal Season As Integer) As Task(Of List(Of Video.Details.Episode))
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] GetAllEpisodes: No client initialized! Abort!")
                Return Nothing
            End If
            Dim response = Await _kodi.VideoLibrary.GetEpisodes(ShowID, Season, properties:=Video.Fields.Episode.AllFields).ConfigureAwait(False)
            Return response.episodes.ToList()
        End Function

#End Region 'Episode API

#Region "General API"
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
                        logger.Warn("[APIKodi] GetSources: " & _currenthost.name & ": " & remotesource.file)
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
        Public Async Function ScanVideoPath(ByVal EmbervideofileID As Long, ByVal videotype As String) As Task(Of Boolean)
            Dim uPath As String = String.Empty
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] ScanVideoPath: No client initialized! Abort!")
                Return Nothing
            End If
            Select Case videotype
                Case "movie"
                    Dim uMovie As Structures.DBMovie = Master.DB.LoadMovieFromDB(EmbervideofileID)
                    If FileUtils.Common.isBDRip(uMovie.Filename) Then
                        'filename must point to m2ts file! 
                        'Ember-Filepath i.e.  E:\Media_1\Movie\Horror\Europa Report\BDMV\STREAM\00000.m2ts
                        'for adding new Bluray rips scan the root folder of movie, i.e: E:\Media_1\Movie\Horror\Europa Report\
                        uPath = IO.Directory.GetParent(IO.Directory.GetParent(IO.Directory.GetParent(uMovie.Filename).FullName).FullName).FullName
                    ElseIf FileUtils.Common.isVideoTS(uMovie.Filename) Then
                        'filename must point to IFO file!
                        'Ember-Filepath i.e.  E:\Media_1\Movie\Action\Crow\VIDEO_TS\VIDEO_TS.IFO
                        'for adding new DVDs scan the root folder of movie, i.e:  E:\Media_1\Movie\Action\Crow\
                        uPath = IO.Directory.GetParent(IO.Directory.GetParent(uMovie.Filename).FullName).FullName
                    Else
                        uPath = IO.Directory.GetParent(uMovie.Filename).FullName
                    End If
                Case "tvshow"
                    Dim uShow As Structures.DBTV = Master.DB.LoadTVShowFromDB(EmbervideofileID)
                    If FileUtils.Common.isBDRip(uShow.ShowPath) Then
                        'needs some testing?!
                        uPath = IO.Directory.GetParent(IO.Directory.GetParent(IO.Directory.GetParent(uShow.ShowPath).FullName).FullName).FullName
                    ElseIf FileUtils.Common.isVideoTS(uShow.ShowPath) Then
                        'needs some testing?!
                        uPath = IO.Directory.GetParent(IO.Directory.GetParent(uShow.ShowPath).FullName).FullName
                    Else
                        uPath = uShow.ShowPath
                    End If
                Case "episode"
                    Dim uEpisode As Structures.DBTV = Master.DB.LoadTVEpFromDB(EmbervideofileID, False)
                    If FileUtils.Common.isBDRip(uEpisode.Filename) Then
                        'needs some testing?!
                        uPath = IO.Directory.GetParent(IO.Directory.GetParent(IO.Directory.GetParent(uEpisode.Filename).FullName).FullName).FullName
                    ElseIf FileUtils.Common.isVideoTS(uEpisode.Filename) Then
                        'needs some testing?!
                        uPath = IO.Directory.GetParent(IO.Directory.GetParent(uEpisode.Filename).FullName).FullName
                    Else
                        uPath = IO.Directory.GetParent(uEpisode.Filename).FullName
                    End If
                Case Else
                    logger.Warn("[APIKodi] ScanVideoPath: " & _currenthost.name & ": No videotype specified, Abort!")
                    Return False
            End Select

            uPath = GetRemoteFilePath(uPath)
            Dim response = Await _kodi.VideoLibrary.Scan(uPath).ConfigureAwait(False)
            If response.Contains("error") Then
                logger.Error(String.Concat("[APIKodi] ScanVideoPath: " & _currenthost.name & ": " & uPath, response))
                Return False
            Else
                logger.Trace(String.Concat("[APIKodi] ScanVideoPath: " & _currenthost.name & ": " & uPath, response))
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
        Function GetRemoteFilePath(ByVal localpath As String) As String
            Dim remotesource As String = ""
            logger.Trace(String.Concat("[APIKodi] GetRemoteFilePath: Localpath: " & localpath))
            For Each kodisource In _currenthost.source
                If localpath.StartsWith(kodisource.applicationpath) Then
                    logger.Trace(String.Concat("[APIKodi] GetRemoteFilePath: Found KodiSource: " & kodisource.remotepath))
                    remotesource = kodisource.remotepath
                    Exit For
                End If
            Next

            If remotesource = String.Empty Then
                logger.Warn("[APIKodi] GetRemoteFilePath: KodiSource NOT Found! Abort!")
                Return ""
            End If

            'if no seperator is specified use pathseperator of current system (=Windows)
            Dim remotepathseparator = _currenthost.remotepathseparator
            If String.IsNullOrEmpty(remotepathseparator) Then remotepathseparator = IO.Path.DirectorySeparatorChar

            'example remotesources: 
            'nfs://192.168.2.200/Media_1/
            'nfs://192.168.0.2/mnt/share/media/Video/Media_1/, 
            'sftp://name:password@192.168.0.2:22/home/media/Media_1/
            '
            'example localpath:  
            'E:\MyMovies\Media_1\Movie\Action\11.14 - Elevenfourteen\poster.jpg

            'first strip driveletter (since it will probably not be identical between Kodi and Ember (i.e. mapped drive))
            Dim tmpremotepath As String = localpath.Replace(IO.Directory.GetDirectoryRoot(localpath), "")
            'result: MyMovies\Media_1\Movie\Action\11.14 - Elevenfourteen\poster.jpg
            tmpremotepath = tmpremotepath.Replace(IO.Path.DirectorySeparatorChar, remotepathseparator)
            'result: MyMovies/Media_1/Movie/Action/11.14 - Elevenfourteen/poster.jpg

            'split remotesource at each seperator and check last part, as this part is used to identify correct source
            Dim pathsplitsremote As String() = remotesource.Split(New String() {remotepathseparator}, StringSplitOptions.RemoveEmptyEntries)
            Dim matchfolder As String = ""
            'should be at least 2 fragment, one= wrong seperator saved in configuration
            If pathsplitsremote.Count > 1 Then
                'example: matchfolder: Media_1
                matchfolder = pathsplitsremote(pathsplitsremote.Count - 1)
            Else
                logger.Warn(String.Concat("[APIKodi] GetRemoteFilePath: Wrong Remotepathseparator: " & remotepathseparator))
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

        Public Async Function GetRequestStream(request As Net.WebRequest) As Task(Of IO.Stream)
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

        Public Function GetInputStream() As IO.Stream Implements ISocket.GetInputStream
            Return New Net.Sockets.NetworkStream(_socket)
        End Function

#End Region 'Methods

#Region "Properties"

#End Region 'Properties

    End Class

#End Region 'Client JSON Communication helper (needed for listening to notification events in Kodi)

End Namespace
