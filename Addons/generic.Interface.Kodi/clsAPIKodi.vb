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

Imports System.IO
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports generic.Interface.Kodi.KodiInterface
Imports NLog
Imports XBMCRPC

Namespace Kodi

    Public Class APIKodi

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()
        'current selected host, Kodi Host type already declared in EmberAPI (XML serialization) -> no MySettings declaration needed here
        Private _currenthost As New Host
        'current selected client
        Private _kodi As Client
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
        Public Sub New(ByVal host As Host)
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
            'Listen to Kodi Events
            'AddHandler _kodi.VideoLibrary.OnScanFinished, AddressOf VideoLibrary_OnScanFinished
            'AddHandler _kodi.VideoLibrary.OnCleanFinished, AddressOf VideoLibrary_OnCleanFinished
            '_kodi.StartNotificationListener()
        End Sub
        ''' <summary>
        ''' Get all movies from Kodi host
        ''' </summary>
        ''' <returns>list of kodi movies, Nothing: error</returns>
        ''' <remarks></remarks>
        Private Async Function GetAll_Movies() As Task(Of VideoLibrary.GetMoviesResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetAllMovies: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Return Await _kodi.VideoLibrary.GetMovies(Video.Fields.Movie.AllFields).ConfigureAwait(False)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Async Function GetAll_MovieSets() As Task(Of VideoLibrary.GetMovieSetsResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetAllMovieSets: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Return Await _kodi.VideoLibrary.GetMovieSets(Video.Fields.MovieSet.AllFields).ConfigureAwait(False)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function

        Private Async Function GetAll_TVEpisodes() As Task(Of VideoLibrary.GetEpisodesResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetAllTVEpisodes: No host initialized! Abort!")
                Return Nothing
            End If

            Dim response_TVShows = Await GetAll_TVShows().ConfigureAwait(False)
            If response_TVShows IsNot Nothing AndAlso response_TVShows.tvshows IsNot Nothing AndAlso response_TVShows.tvshows.Count > 0 Then
                Dim lstTVEpisodes As New VideoLibrary.GetEpisodesResponse With {.episodes = New List(Of Video.Details.Episode)}
                For Each nTVShow In response_TVShows.tvshows
                    Dim response_TVEpisodes = Await GetAll_TVEpisodes(nTVShow.tvshowid)
                    If response_TVEpisodes IsNot Nothing AndAlso response_TVEpisodes.episodes.Count > 0 Then
                        lstTVEpisodes.episodes.AddRange(response_TVEpisodes.episodes)
                    End If
                Next
                Return lstTVEpisodes
            End If
            Return Nothing
        End Function

        Private Async Function GetAll_TVEpisodes(ByVal KodiShowID As Integer) As Task(Of VideoLibrary.GetEpisodesResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetAllTVEpisodes: No host initialized! Abort!")
                Return Nothing
            End If

            Dim response_TVSeasons = Await GetAll_TVSeasons(KodiShowID).ConfigureAwait(False)
            If response_TVSeasons IsNot Nothing AndAlso response_TVSeasons.seasons IsNot Nothing AndAlso response_TVSeasons.seasons.Count > 0 Then
                Dim lstTVEpisodes As New VideoLibrary.GetEpisodesResponse With {.episodes = New List(Of Video.Details.Episode)}
                For Each nTVSeason In response_TVSeasons.seasons
                    Dim response_TVEpisodes = Await GetAll_TVEpisodes(KodiShowID, nTVSeason.season)
                    If response_TVEpisodes IsNot Nothing AndAlso response_TVEpisodes.episodes IsNot Nothing AndAlso response_TVEpisodes.episodes.Count > 0 Then
                        lstTVEpisodes.episodes.AddRange(response_TVEpisodes.episodes)
                    End If
                Next
                Return lstTVEpisodes
            End If
            Return Nothing
        End Function

        Private Async Function GetAll_TVEpisodes(ByVal KodiShowID As Integer, ByVal intSeason As Integer) As Task(Of VideoLibrary.GetEpisodesResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetAllTVEpisodes: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Return Await _kodi.VideoLibrary.GetEpisodes(KodiShowID, intSeason, Video.Fields.Episode.AllFields).ConfigureAwait(False)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function

        Private Async Function GetAll_TVSeasons(ByVal KodiShowID As Integer) As Task(Of VideoLibrary.GetSeasonsResponse)
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] GetAllTVSeasons: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Return Await _kodi.VideoLibrary.GetSeasons(KodiShowID, Video.Fields.Season.AllFields).ConfigureAwait(False)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Get all tvshows from Kodi host
        ''' </summary>
        ''' <returns>list of kodi tv shows, Nothing: error</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' Notice: No exception handling here because this function is called/nested in other functions and an exception must not be consumed (meaning a disconnect host would not be recognized at once)
        ''' </remarks>
        Private Async Function GetAll_TVShows() As Task(Of VideoLibrary.GetTVShowsResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetAllTVShows: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Return Await _kodi.VideoLibrary.GetTVShows(Video.Fields.TVShow.AllFields).ConfigureAwait(False)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function

        Public Async Function GetConnectionToHost() As Task(Of Boolean)
            Try
                _currenthost.APIVersionInfo = Await GetHostJSONVersion()
                Return True
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                logger.Error(String.Format("[APIKodi] [{0}] TestConnectionToHost | No connection to Host!", _currenthost.Label))
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Get full details of a Movie by ID
        ''' </summary>
        ''' <param name="KodiMovieID"></param>
        ''' <returns></returns>
        Private Async Function GetFullDetailsByID_Movie(ByVal KodiMovieID As Integer) As Task(Of Video.Details.Movie)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetFullDetailsByID_Movie: No host initialized! Abort!")
                Return Nothing
            End If

            If Not KodiMovieID = -1 Then
                Try
                    Dim KodiElement As VideoLibrary.GetMovieDetailsResponse = Await _kodi.VideoLibrary.GetMovieDetails(KodiMovieID, Video.Fields.Movie.AllFields).ConfigureAwait(False)
                    If KodiElement IsNot Nothing AndAlso KodiElement.moviedetails IsNot Nothing Then Return KodiElement.moviedetails
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                    Return Nothing
                End Try
            End If
            Return Nothing
        End Function
        ''' <summary>
        ''' Get full details of a MovieSet by ID
        ''' </summary>
        ''' <param name="KodiMoviesetID"></param>
        ''' <returns></returns>
        Private Async Function GetFullDetailsByID_MovieSet(ByVal KodiMoviesetID As Integer) As Task(Of Video.Details.MovieSet)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetFullDetailsByID_MovieSet: No host initialized! Abort!")
                Return Nothing
            End If

            If Not KodiMoviesetID = -1 Then
                Try
                    Dim KodiElement As VideoLibrary.GetMovieSetDetailsResponse = Await _kodi.VideoLibrary.GetMovieSetDetails(KodiMoviesetID, Video.Fields.MovieSet.AllFields).ConfigureAwait(False)
                    If KodiElement IsNot Nothing AndAlso KodiElement.setdetails IsNot Nothing Then Return KodiElement.setdetails
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                    Return Nothing
                End Try
            End If
            Return Nothing
        End Function
        ''' <summary>
        ''' Get full details of a Episode by ID
        ''' </summary>
        ''' <param name="KodiEpisodeID"></param>
        ''' <returns></returns>
        Private Async Function GetFullDetailsByID_TVEpisode(ByVal KodiEpisodeID As Integer) As Task(Of Video.Details.Episode)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetFullDetailsByID_TVEpisode: No host initialized! Abort!")
                Return Nothing
            End If

            If Not KodiEpisodeID = -1 Then
                Try
                    Dim KodiElement As VideoLibrary.GetEpisodeDetailsResponse = Await _kodi.VideoLibrary.GetEpisodeDetails(KodiEpisodeID, Video.Fields.Episode.AllFields).ConfigureAwait(False)
                    If KodiElement IsNot Nothing AndAlso KodiElement.episodedetails IsNot Nothing Then Return KodiElement.episodedetails
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                    Return Nothing
                End Try
            End If
            Return Nothing
        End Function
        ''' <summary>
        ''' Get full details of a Season by ID
        ''' </summary>
        ''' <param name="KodiSeasonID"></param>
        ''' <returns></returns>
        Private Async Function GetFullDetailsByID_TVSeason(ByVal KodiSeasonID As Integer) As Task(Of Video.Details.Season)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetFullDetailsByID_TVSeason: No host initialized! Abort!")
                Return Nothing
            End If

            If Not KodiSeasonID = -1 Then
                Try
                    Dim KodiElement As VideoLibrary.GetSeasonDetailsResponse = Await _kodi.VideoLibrary.GetSeasonDetails(KodiSeasonID, Video.Fields.Season.AllFields).ConfigureAwait(False)
                    If KodiElement IsNot Nothing AndAlso KodiElement.seasondetails IsNot Nothing Then Return KodiElement.seasondetails
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                    Return Nothing
                End Try
            End If
            Return Nothing
        End Function
        ''' <summary>
        ''' Get full details of a TV Show by ID
        ''' </summary>
        ''' <param name="KodiShowID"></param>
        ''' <returns></returns>
        Private Async Function GetFullDetailsByID_TVShow(ByVal KodiShowID As Integer) As Task(Of Video.Details.TVShow)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetFullDetailsByID_TVShow: No host initialized! Abort!")
                Return Nothing
            End If

            If Not KodiShowID = -1 Then
                Try
                    Dim KodiElement As VideoLibrary.GetTVShowDetailsResponse = Await _kodi.VideoLibrary.GetTVShowDetails(KodiShowID, Video.Fields.TVShow.AllFields).ConfigureAwait(False)
                    If KodiElement IsNot Nothing AndAlso KodiElement.tvshowdetails IsNot Nothing Then Return KodiElement.tvshowdetails
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                    Return Nothing
                End Try
            End If
            Return Nothing
        End Function
        ''' <summary>
        ''' Get JSONRPC version of host
        ''' </summary>
        ''' <param name="kHost">specific host to query</param>
        ''' <remarks>
        ''' </remarks>
        Public Shared Function GetHostJSONVersion(ByVal kHost As Host) As JSONRPC.VersionResponse
            Try
                Dim _APIKodi As New APIKodi(kHost)
                Return _APIKodi.GetHostJSONVersion.Result
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Get JSON RPC version of host
        ''' </summary>
        ''' <returns>string which contains exact JSONRPC version, Nothing: Empty string</returns>
        ''' <remarks>
        ''' </remarks>
        Private Async Function GetHostJSONVersion() As Task(Of JSONRPC.VersionResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetHostJSONVersion: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Return Await _kodi.JSONRPC.Version.ConfigureAwait(False)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Search movie ID in Kodi database
        ''' </summary>
        ''' <param name="tDBElement"></param>
        ''' <returns></returns>
        Private Async Function GetMediaID(ByVal tDBElement As Database.DBElement) As Task(Of Integer)
            Dim KodiID As Integer = -1

            Select Case tDBElement.ContentType
                Case Enums.ContentType.Movie
                    Dim KodiMovie As Video.Details.Movie = Await Search_Movie(tDBElement).ConfigureAwait(False)
                    If KodiMovie IsNot Nothing Then Return KodiMovie.movieid
                Case Enums.ContentType.Movieset
                    Dim KodiMovieset As Video.Details.MovieSet = Await Search_MovieSet(tDBElement).ConfigureAwait(False)
                    If KodiMovieset IsNot Nothing Then Return KodiMovieset.setid
                Case Enums.ContentType.TVEpisode
                    Dim KodiEpsiode As Video.Details.Episode = Await Search_TVEpisode(tDBElement).ConfigureAwait(False)
                    If Not KodiEpsiode Is Nothing Then Return KodiEpsiode.episodeid
                Case Enums.ContentType.TVSeason
                    Dim KodiSeason As Video.Details.Season = Await Search_TVSeason(tDBElement).ConfigureAwait(False)
                    If Not KodiSeason Is Nothing Then Return KodiSeason.seasonid
                Case Enums.ContentType.TVShow
                    Dim KodiTVShow As Video.Details.TVShow = Await Search_TVShow(tDBElement).ConfigureAwait(False)
                    If Not KodiTVShow Is Nothing Then Return KodiTVShow.tvshowid
            End Select

            Return -1
        End Function
        ''' <summary>
        ''' Get movie playcount from Host
        ''' </summary>
        ''' <param name="mDBElement">Movie as DBElement</param>
        ''' <returns>true=Update successfull, false=error or movie not found in KodiDB</returns>
        ''' <remarks>
        ''' </remarks>
        Public Async Function GetPlaycount_AllMovies(
                                                    ByVal GenericSubEvent As IProgress(Of GenericSubEventCallBackAsync),
                                                    ByVal GenericMainEvent As IProgress(Of GenericEventCallBackAsync)
                                                    ) As Task(Of VideoLibrary.GetMoviesResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetPlaycount_AllMovies: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] GetPlaycount_AllMovies | Start process...", _currenthost.Label))
                'get informations for all movies
                Return Await GetAll_Movies()

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Get movie playcount from Host
        ''' </summary>
        ''' <param name="mDBElement">Movie as DBElement</param>
        ''' <returns>true=Update successfull, false=error or movie not found in KodiDB</returns>
        ''' <remarks>
        ''' </remarks>
        Public Async Function GetPlaycount_AllTVEpisodes(
                                                        ByVal GenericSubEvent As IProgress(Of GenericSubEventCallBackAsync),
                                                        ByVal GenericMainEvent As IProgress(Of GenericEventCallBackAsync)
                                                        ) As Task(Of VideoLibrary.GetEpisodesResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetPlaycount_AllTVEpisodes: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] GetPlaycount_AllTVEpisodes | Start process...", _currenthost.Label))
                'get informations for all movies
                Return Await GetAll_TVEpisodes()

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Get movie playcount from Host
        ''' </summary>
        ''' <param name="mDBElement">Movie as DBElement</param>
        ''' <returns>true=Update successfull, false=error or movie not found in KodiDB</returns>
        ''' <remarks>
        ''' </remarks>
        Public Async Function GetPlaycount_Movie(
                                                ByVal mDBElement As Database.DBElement,
                                                ByVal GenericSubEvent As IProgress(Of GenericSubEventCallBackAsync),
                                                ByVal GenericMainEvent As IProgress(Of GenericEventCallBackAsync)
                                                ) As Task(Of WatchedState)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetPlaycount_Movie: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] GetPlaycount_Movie: ""{1}"" | Start process...", _currenthost.Label, mDBElement.Movie.Title))

                'search Movie ID in Kodi DB
                Dim KodiElement As Video.Details.Movie = Await GetFullDetailsByID_Movie(Await GetMediaID(mDBElement))

                If KodiElement IsNot Nothing Then
                    'check if we have to retrieve the PlayCount from Kodi
                    If Not mDBElement.Movie.PlayCount = KodiElement.playcount OrElse Not mDBElement.Movie.LastPlayed = KodiElement.lastplayed Then
                        Dim WatchedState As New WatchedState
                        WatchedState.PlayCount = KodiElement.playcount
                        WatchedState.LastPlayed = KodiElement.lastplayed
                        logger.Trace(String.Format("[APIKodi] [{0}] GetPlaycount_Movie: ""{1}"" | Synced to Ember", _currenthost.Label, mDBElement.Movie.Title))
                        Return WatchedState
                    Else
                        logger.Trace(String.Format("[APIKodi] [{0}] GetPlaycount_Movie: ""{1}"" | Nothing to sync", _currenthost.Label, mDBElement.Movie.Title))
                        Return New WatchedState With {.AlreadyInSync = True}
                    End If
                Else
                    logger.Trace(String.Format("[APIKodi] [{0}] GetPlaycount_Movie: ""{1}"" | Nothing to sync (not found on host)", _currenthost.Label, mDBElement.Movie.Title))
                    Return Nothing
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Get episode playcount from Host
        ''' </summary>
        ''' <param name="mDBElement">TVEpisode as DBElement</param>
        ''' <returns>true=Update successfull, false=error or episode not found in KodiDB</returns>
        ''' <remarks>
        ''' </remarks>
        Public Async Function GetPlaycount_TVEpisode(
                                                    ByVal mDBElement As Database.DBElement,
                                                    ByVal GenericSubEvent As IProgress(Of GenericSubEventCallBackAsync),
                                                    ByVal GenericMainEvent As IProgress(Of GenericEventCallBackAsync)
                                                    ) As Task(Of WatchedState)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] GetPlaycount_TVEpisode: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] GetPlaycount_TVEpisode: ""{1}"" | Start syncing process...", _currenthost.Label, mDBElement.TVEpisode.Title))

                'search TV Episode ID in Kodi DB
                Dim KodiElement As Video.Details.Episode = Await GetFullDetailsByID_TVEpisode(Await GetMediaID(mDBElement))

                If KodiElement IsNot Nothing Then
                    'check if we have to retrieve the PlayCount from Kodi
                    If Not mDBElement.TVEpisode.Playcount = KodiElement.playcount OrElse Not mDBElement.TVEpisode.LastPlayed = KodiElement.lastplayed Then
                        Dim WatchedState As New WatchedState
                        WatchedState.PlayCount = KodiElement.playcount
                        WatchedState.LastPlayed = KodiElement.lastplayed
                        logger.Trace(String.Format("[APIKodi] [{0}] GetPlaycount_TVEpisode: ""{1}"" | Synced to Ember", _currenthost.Label, mDBElement.TVEpisode.Title))
                        Return WatchedState
                    Else
                        logger.Trace(String.Format("[APIKodi] [{0}] GetPlaycount_TVEpisode: ""{1}"" | Nothing to sync", _currenthost.Label, mDBElement.TVEpisode.Title))
                        Return New WatchedState With {.AlreadyInSync = True}
                    End If
                Else
                    logger.Trace(String.Format("[APIKodi] [{0}] GetPlaycount_TVEpisode: ""{1}"" | Nothing to sync (not found on host)", _currenthost.Label, mDBElement.TVEpisode.Title))
                    Return Nothing
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function

        Public Shared Function GetMediaPathAndFilename(
                                                 ByVal tDBElement As Database.DBElement,
                                                 Optional ByVal ForcedContentType As Enums.ContentType = Enums.ContentType.None
                                                 ) As PathAndFilename
            Dim tPathAndFilename As New PathAndFilename
            Dim tContentType As Enums.ContentType = If(ForcedContentType = Enums.ContentType.None, tDBElement.ContentType, ForcedContentType)

            Select Case tContentType
                Case Enums.ContentType.Movie, Enums.ContentType.TVEpisode
                    If tDBElement.FileItem.bIsVideoTS Then
                        'Kodi needs the VIDEO_TS folder path and VIDEO_TS.OFI as file name
                        tPathAndFilename.Filename = tDBElement.FileItem.FileInfo.Name
                        tPathAndFilename.Path = tDBElement.FileItem.FileInfo.DirectoryName
                    ElseIf tDBElement.FileItem.bIsBDMV Then
                        'Kodi needs the BDMV folder path and index.bdmv as file name
                        tPathAndFilename.Filename = tDBElement.FileItem.FileInfo.Name
                        tPathAndFilename.Path = tDBElement.FileItem.FileInfo.DirectoryName
                    Else
                        If tDBElement.FileItem.bIsArchive OrElse
                            tDBElement.FileItem.bIsStacked Then
                            tPathAndFilename.Filename = tDBElement.FileItem.FullPath
                            tPathAndFilename.Path = tDBElement.FileItem.MainPath.FullName
                        Else
                            tPathAndFilename.Filename = Path.GetFileName(tDBElement.FileItem.FirstPathFromStack)
                            tPathAndFilename.Path = tDBElement.FileItem.MainPath.FullName
                        End If
                    End If
                Case Enums.ContentType.TVShow
                    tPathAndFilename.Path = tDBElement.ShowPath
            End Select

            If tPathAndFilename.Path.Contains(Path.DirectorySeparatorChar) AndAlso Not tPathAndFilename.Path.EndsWith(Path.DirectorySeparatorChar) Then
                tPathAndFilename.Path = String.Concat(tPathAndFilename.Path, Path.DirectorySeparatorChar)
            ElseIf tPathAndFilename.Path.Contains(Path.AltDirectorySeparatorChar) AndAlso Not tPathAndFilename.Path.EndsWith(Path.AltDirectorySeparatorChar) Then
                tPathAndFilename.Path = String.Concat(tPathAndFilename.Path, Path.AltDirectorySeparatorChar)
            End If

            Return tPathAndFilename
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="LocalPath"></param>
        ''' <returns></returns>
        ''' <remarks>ATTENTION: It's not allowed to use "Remotepath.ToLower" (Kodi can't find UNC sources with wrong case)</remarks>
        Public Function GetRemotePath(ByVal LocalPath As String) As String
            Dim strRemotePath As String = String.Empty
            Dim bRemoteIsUNC As Boolean = False

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
                If LocalPath.ToLower.StartsWith(tLocalSource.ToLower) Then
                    Dim tRemoteSource As String = String.Empty
                    If Source.RemotePath.Contains(Path.DirectorySeparatorChar) Then
                        tRemoteSource = If(Source.RemotePath.EndsWith(Path.DirectorySeparatorChar), Source.RemotePath, String.Concat(Source.RemotePath, Path.DirectorySeparatorChar)).Trim
                    ElseIf Source.RemotePath.Contains(Path.AltDirectorySeparatorChar) Then
                        tRemoteSource = If(Source.RemotePath.EndsWith(Path.AltDirectorySeparatorChar), Source.RemotePath, String.Concat(Source.RemotePath, Path.AltDirectorySeparatorChar)).Trim
                        bRemoteIsUNC = True
                    End If
                    strRemotePath = Regex.Replace(LocalPath, Regex.Escape(tLocalSource), tRemoteSource, RegexOptions.IgnoreCase)
                    If bRemoteIsUNC Then
                        strRemotePath = strRemotePath.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                    Else
                        strRemotePath = strRemotePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)
                    End If
                    Exit For
                End If
            Next

            If String.IsNullOrEmpty(strRemotePath) Then logger.Error(String.Format("[APIKodi] [{0}] GetRemotePath: ""{1}"" | Source not mapped!", _currenthost.Label, LocalPath))

            'Path encoding if needed
            If Regex.IsMatch(strRemotePath, "davs?:\/\/") OrElse
                Path.GetExtension(strRemotePath).ToLower = ".rar" OrElse
                Path.GetExtension(strRemotePath).ToLower = ".zip" Then
                'HttpUtility.UrlEncode does escape SPACE as + and not as %20, so we have to use Uri.EscapeDataString.
                'Uri.EscapeDataString use upper case for all escaped chars, Kodi use lower case. But it works, so it
                'looks like Kodi use .ToLower for comparing
                strRemotePath = Uri.EscapeDataString(strRemotePath)
            End If

            Return strRemotePath
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="LocalPath"></param>
        ''' <returns></returns>
        ''' <remarks>ATTENTION: It's not allowed to use "Remotepath.ToLower" (Kodi can't find UNC sources with wrong case)</remarks>
        Private Function GetRemotePath_MovieSet(ByVal strLocalPath As String) As String
            If String.IsNullOrEmpty(_currenthost.MovieSetArtworksPath) Then
                logger.Error(String.Format("[APIKodi] [{0}] [GetRemotePath_MovieSet]: No MovieSet Artwork path definied for this host!", _currenthost.Label))
                Return Nothing
            End If

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
                    RemotePath = Regex.Replace(strLocalPath, Regex.Escape(tLocalSource), tRemoteSource, RegexOptions.IgnoreCase)
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
        ''' <summary>
        ''' Get all video sources configured in host
        ''' </summary>
        ''' <param name="kHost">specific host to query</param>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' Called from dlgHost.vb when user hits "Populate" button to get host sources
        ''' </remarks>
        Public Shared Function GetSources(ByVal kHost As Host) As List(Of List.Items.SourcesItem)
            Try
                Dim _APIKodi As New APIKodi(kHost)
                Return _APIKodi.GetSources(Files.Media.video).Result
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
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
        Private Async Function GetSources(mediaType As Files.Media) As Task(Of List(Of List.Items.SourcesItem))
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] [GetSources] No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Dim response = Await _kodi.Files.GetSources(mediaType).ConfigureAwait(False)
                If response Is Nothing OrElse response.sources Is Nothing Then
                    Return Nothing
                Else
                    Dim tmplist = response.sources.ToList

                    'type multipath sources contain multiple paths
                    Dim lstremotesources As New List(Of List.Items.SourcesItem)
                    Dim paths As New List(Of String)
                    Const MultiPath As String = "multipath://"
                    For Each remotesource In tmplist
                        Dim newsource As New List.Items.SourcesItem
                        If remotesource.file.StartsWith(MultiPath) Then
                            logger.Trace(String.Format("[APIKodi] [{0}] [GetSources] Multipath format, try to split: {1}", _currenthost.Label, remotesource.file))
                            'remove "multipath://" from path and split on "/"
                            'i.e multipath://nfs%3a%2f%2f192.168.2.200%2fMedia_1%2fMovie%2f/nfs%3a%2f%2f192.168.2.200%2fMedia_2%2fMovie%2f/
                            For Each path As String In remotesource.file.Remove(0, MultiPath.Length).Split("/"c)
                                If Not String.IsNullOrEmpty(path) Then
                                    newsource = New List.Items.SourcesItem
                                    'URL decode each item
                                    newsource.file = Web.HttpUtility.UrlDecode(path)
                                    newsource.label = remotesource.label
                                    lstremotesources.Add(newsource)
                                    logger.Trace(String.Format("[APIKodi] [{0}] [GetSources] Added Source: {1}", _currenthost.Label, newsource.file))
                                End If
                            Next
                        Else
                            newsource.file = remotesource.file
                            newsource.label = remotesource.label
                            lstremotesources.Add(newsource)
                            logger.Trace(String.Format("[APIKodi] [{0}] [GetSources] Added Source: {1}", _currenthost.Label, remotesource.file))
                        End If
                    Next

                    Return lstremotesources
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function

        Private Async Function GetTextures(ByVal tDBElement As Database.DBElement) As Task(Of Textures.GetTexturesResponse)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] [GetTextures] No host initialized! Abort!")
                Return Nothing
            End If

            Dim lstImagesToRemove As New List(Of String)

            With tDBElement.ImagesContainer
                If .Banner.LocalFilePathSpecified Then lstImagesToRemove.Add(.Banner.LocalFilePath)
                If .CharacterArt.LocalFilePathSpecified Then lstImagesToRemove.Add(.CharacterArt.LocalFilePath)
                If .ClearArt.LocalFilePathSpecified Then lstImagesToRemove.Add(.ClearArt.LocalFilePath)
                If .ClearLogo.LocalFilePathSpecified Then lstImagesToRemove.Add(.ClearLogo.LocalFilePath)
                If .DiscArt.LocalFilePathSpecified Then lstImagesToRemove.Add(.DiscArt.LocalFilePath)
                If .Fanart.LocalFilePathSpecified Then lstImagesToRemove.Add(.Fanart.LocalFilePath)
                If .Landscape.LocalFilePathSpecified Then lstImagesToRemove.Add(.Landscape.LocalFilePath)
                If .Poster.LocalFilePathSpecified Then lstImagesToRemove.Add(.Poster.LocalFilePath)
            End With

            Select Case tDBElement.ContentType
                Case Enums.ContentType.Movie
                    For Each tActor As MediaContainers.Person In tDBElement.Movie.Actors.Where(Function(f) f.LocalFilePathSpecified)
                        lstImagesToRemove.Add(tActor.LocalFilePath)
                    Next
                Case Enums.ContentType.TVEpisode
                    For Each tActor As MediaContainers.Person In tDBElement.TVEpisode.Actors.Where(Function(f) f.LocalFilePathSpecified)
                        lstImagesToRemove.Add(tActor.LocalFilePath)
                    Next
                Case Enums.ContentType.TVShow
                    For Each tActor As MediaContainers.Person In tDBElement.TVShow.Actors.Where(Function(f) f.LocalFilePathSpecified)
                        lstImagesToRemove.Add(tActor.LocalFilePath)
                    Next
            End Select

            If lstImagesToRemove.Count > 0 Then
                Try
                    Dim filter As New List.Filter.TexturesOr With {.or = New List(Of Object)}
                    For Each tURL As String In lstImagesToRemove
                        Dim filterRule As New List.Filter.Rule.Textures With {
                            .field = List.Filter.Fields.Textures.url,
                            .Operator = List.Filter.Operators.Is,
                            .value = If(tDBElement.ContentType = Enums.ContentType.Movieset, GetRemotePath_MovieSet(tURL), GetRemotePath(tURL))
                        }
                        filter.or.Add(filterRule)
                    Next

                    'TODO: JSON is limited to 20k characters. We have to split the filter if there are to many actor thumbs
                    Dim response As Textures.GetTexturesResponse = Await _kodi.Textures.GetTextures(filter, Textures.Fields.Texture.AllFields)
                    Return response
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                    Return Nothing
                End Try
            End If
            Return Nothing
        End Function
        ''' <summary>
        ''' Scan video library of Kodi host
        ''' </summary>
        ''' <returns>string with status message, if failed: Nothing</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Async Function Host_IsScanningVideo() As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] IsScanningVideo: No host initialized! Abort!")
                Return False
            End If

            Try
                Dim response As XBMC.GetInfoBooleansResponse = Await _kodi.XBMC.GetInfoBooleans(New List(Of String)(New String() {"Library.IsScanningVideo"}))
                If response IsNot Nothing Then
                    logger.Trace(String.Format("[APIKodi] [{0}] IsScanningVideo: {1}", _currenthost.Label, response.IsScanningVideo.ToString))
                    Return response.IsScanningVideo
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] IsScanningVideo: No answer from Host", _currenthost.Label))
                    Return False
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return False
            End Try
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
        Public Async Function Host_SendMessage(ByVal Title As String, ByVal Message As String) As Task(Of String)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] SendMessage: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Return Await _kodi.GUI.ShowNotification(Title, Message, 2500).ConfigureAwait(False)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function

        Private Async Function Search_Movie(ByVal tDBElement As Database.DBElement) As Task(Of Video.Details.Movie)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] [SearchMovie] No host initialized! Abort!")
                Return Nothing
            End If

            Dim kMovies As VideoLibrary.GetMoviesResponse

            Dim tPathAndFilename As PathAndFilename = GetMediaPathAndFilename(tDBElement)
            Dim strFilename As String = String.Empty
            If tDBElement.FileItem.bIsArchive OrElse tDBElement.FileItem.bIsStacked Then
                strFilename = GetRemotePath(tPathAndFilename.Filename)
            Else
                strFilename = tPathAndFilename.Filename
            End If
            Dim strRemotePath As String = GetRemotePath(tPathAndFilename.Path)

            If Not String.IsNullOrEmpty(strRemotePath) Then
                Try
                    Dim filter As New List.Filter.MoviesAnd With {.and = New List(Of Object)}
                    Dim filterRule_Path As New List.Filter.Rule.Movies With {
                        .field = List.Filter.Fields.Movies.path,
                        .Operator = List.Filter.Operators.Is,
                        .value = strRemotePath
                    }
                    filter.and.Add(filterRule_Path)
                    Dim filterRule_Filename As New List.Filter.Rule.Movies With {
                        .field = List.Filter.Fields.Movies.filename,
                        .Operator = If(tDBElement.FileItem.bIsArchive OrElse tDBElement.FileItem.bIsStacked, List.Filter.Operators.contains, List.Filter.Operators.Is),
                        .value = strFilename
                    }
                    filter.and.Add(filterRule_Filename)

                    kMovies = Await _kodi.VideoLibrary.GetMovies(filter:=filter).ConfigureAwait(False)
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                    Return Nothing
                End Try
            Else
                logger.Error(String.Format("[APIKodi] [{0}] [SearchMovie] ""{1}"" | Source not mapped!", _currenthost.Label, tDBElement.Source.Path))
                Return Nothing
            End If

            If kMovies IsNot Nothing Then
                If kMovies.movies IsNot Nothing Then
                    If kMovies.movies.Count = 1 Then
                        logger.Trace(String.Format("[APIKodi] [{0}] [SearchMovie] ""{1}"" | OK, found in host database! [ID:{2}]", _currenthost.Label, tDBElement.FileItem.FirstPathFromStack, kMovies.movies.Item(0).movieid))
                        Return kMovies.movies.Item(0)
                    ElseIf kMovies.movies.Count > 1 Then
                        logger.Warn(String.Format("[APIKodi] [{0}] [SearchMovie] ""{1}"" | MORE THAN ONE movie found in host database!", _currenthost.Label, tDBElement.FileItem.FirstPathFromStack))
                        Return Nothing
                    Else
                        logger.Warn(String.Format("[APIKodi] [{0}] [SearchMovie] ""{1}"" | NOT found in host database!", _currenthost.Label, tDBElement.FileItem.FirstPathFromStack))
                        Return Nothing
                    End If
                Else
                    logger.Warn(String.Format("[APIKodi] [{0}] [SearchMovie] ""{1}"" | NOT found in host database!", _currenthost.Label, tDBElement.FileItem.FirstPathFromStack))
                    Return Nothing
                End If
            Else
                logger.Error(String.Format("[APIKodi] [{0}] [SearchMovie] ""{1}"" | No connection to Host!", _currenthost.Label, tDBElement.FileItem.FirstPathFromStack))
                Return Nothing
            End If
        End Function

        Private Async Function Search_MovieSet(ByVal tDBElement As Database.DBElement) As Task(Of Video.Details.MovieSet)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] SearchMovieSet: No host initialized! Abort!")
                Return Nothing
            End If

            'get a list of all moviesets saved in Kodi DB
            Dim kMovieSets As VideoLibrary.GetMovieSetsResponse = Await GetAll_MovieSets().ConfigureAwait(False)

            If kMovieSets IsNot Nothing Then
                If kMovieSets.sets IsNot Nothing Then

                    'compare by movieset title
                    For Each tMovieSet In kMovieSets.sets
                        If tMovieSet.title.ToLower = tDBElement.MovieSet.Title.ToLower Then
                            logger.Trace(String.Format("[APIKodi] [{0}] SearchMovieSetByDetails: ""{1}"" | OK, found in host database! [ID:{2}]", _currenthost.Label, tDBElement.MovieSet.Title, tMovieSet.setid))
                            Return tMovieSet
                        End If
                    Next

                    'compare by movies inside movieset
                    For Each tMovie In tDBElement.MoviesInSet
                        logger.Trace(String.Format("[APIKodi] [{0}] SearchMovieSetByDetails: ""{1}"" | NOT found in host database, trying to find the movieset by movies...", _currenthost.Label, tDBElement.MovieSet.Title))
                        'search movie ID in Kodi DB
                        Dim MovieID As Integer = -1
                        Dim KodiMovie = Await Search_Movie(tMovie.DBMovie).ConfigureAwait(False)
                        If KodiMovie IsNot Nothing Then
                            For Each tMovieSet In kMovieSets.sets
                                If tMovieSet.setid = KodiMovie.setid Then
                                    logger.Trace(String.Format("[APIKodi] [{0}] SearchMovieSetByDetails: ""{1}"" | OK, found in host database by movie ""{2}""! [ID:{3}]", _currenthost.Label, tDBElement.MovieSet.Title, KodiMovie.title, tMovieSet.setid))
                                    Return tMovieSet
                                End If
                            Next
                        End If
                    Next

                    logger.Warn(String.Format("[APIKodi] [{0}] SearchMovieSetByDetails: ""{1}"" | NOT found in host database!", _currenthost.Label, tDBElement.MovieSet.Title))
                    Return Nothing
                Else
                    logger.Warn(String.Format("[APIKodi] [{0}] SearchMovieSetByDetails: ""{1}"" | NOT found in host database!", _currenthost.Label, tDBElement.MovieSet.Title))
                    Return Nothing
                End If
            Else
                logger.Error(String.Format("[APIKodi] [{0}] SearchMovieSetByDetails: ""{1}"" | No connection to Host!", _currenthost.Label, tDBElement.MovieSet.Title))
                Return Nothing
            End If
        End Function

        Private Async Function Search_TVEpisode(ByVal tDBElement As Database.DBElement) As Task(Of Video.Details.Episode)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] SearchTVEpisode: No host initialized! Abort!")
                Return Nothing
            End If

            Dim kTVEpisodes As VideoLibrary.GetEpisodesResponse

            Dim tPathAndFilename As PathAndFilename = GetMediaPathAndFilename(tDBElement)
            Dim strFilename As String = String.Empty
            If tDBElement.FileItem.bIsArchive OrElse tDBElement.FileItem.bIsStacked Then
                strFilename = GetRemotePath(tPathAndFilename.Filename)
            Else
                strFilename = tPathAndFilename.Filename
            End If
            Dim strRemotePath As String = GetRemotePath(tPathAndFilename.Path)

            If Not String.IsNullOrEmpty(strRemotePath) Then
                Try
                    Dim filter As New List.Filter.EpisodesAnd With {.and = New List(Of Object)}
                    Dim filterRule_Path As New List.Filter.Rule.Episodes With {
                        .field = List.Filter.Fields.Episodes.path,
                        .Operator = List.Filter.Operators.Is,
                        .value = strRemotePath
                    }
                    filter.and.Add(filterRule_Path)
                    Dim filterRule_Filename As New List.Filter.Rule.Episodes With {
                        .field = List.Filter.Fields.Episodes.filename,
                        .Operator = List.Filter.Operators.Is,
                        .value = Path.GetFileName(strFilename)
                    }
                    filter.and.Add(filterRule_Filename)

                    kTVEpisodes = Await _kodi.VideoLibrary.GetEpisodes(filter:=filter).ConfigureAwait(False)
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                    Return Nothing
                End Try
            Else
                logger.Error(String.Format("[APIKodi] [{0}] SearchTVEpisode: ""{1}"" | Source not mapped!", _currenthost.Label, tDBElement.Source.Path))
                Return Nothing
            End If

            If kTVEpisodes IsNot Nothing Then
                If kTVEpisodes.episodes IsNot Nothing Then
                    If kTVEpisodes.episodes.Count = 1 Then
                        logger.Trace(String.Format("[APIKodi] [{0}] SearchTVEpisode: ""{1}"" | OK, found in host database! [ID:{2}]", _currenthost.Label, tDBElement.FileItem.FirstPathFromStack, kTVEpisodes.episodes.Item(0).episodeid))
                        Return kTVEpisodes.episodes.Item(0)
                    ElseIf kTVEpisodes.episodes.Count > 1 Then
                        'try to filter MultiEpisode files
                        Dim sEpisode = kTVEpisodes.episodes.Where(Function(f) f.episode = tDBElement.TVEpisode.Episode)
                        If sEpisode.Count = 1 Then
                            Return sEpisode(0)
                        ElseIf sEpisode.Count > 1 Then
                            logger.Warn(String.Format("[APIKodi] [{0}] SearchTVEpisode: ""{1}"" | MORE THAN ONE episode found in host database!", _currenthost.Label, tDBElement.FileItem.FirstPathFromStack))
                            Return Nothing
                        Else
                            logger.Warn(String.Format("[APIKodi] [{0}] SearchTVEpisode: ""{1}"" | NOT found in host database!", _currenthost.Label, tDBElement.FileItem.FirstPathFromStack))
                            Return Nothing
                        End If
                    Else
                        logger.Warn(String.Format("[APIKodi] [{0}] SearchTVEpisode: ""{1}"" | NOT found in host database!", _currenthost.Label, tDBElement.FileItem.FirstPathFromStack))
                        Return Nothing
                    End If
                Else
                    logger.Warn(String.Format("[APIKodi] [{0}] SearchTVEpisode: ""{1}"" | NOT found in host database!", _currenthost.Label, tDBElement.FileItem.FirstPathFromStack))
                    Return Nothing
                End If
            Else
                logger.Error(String.Format("[APIKodi] [{0}] SearchTVEpisode: ""{1}"" | No connection to Host!", _currenthost.Label, tDBElement.FileItem.FirstPathFromStack))
                Return Nothing
            End If
        End Function

        Private Async Function Search_TVSeason(ByVal tDBElement As Database.DBElement) As Task(Of Video.Details.Season)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] SearchTVSeason: No host initialized! Abort!")
                Return Nothing
            End If

            Dim KodiTVShow = Await Search_TVShow(tDBElement).ConfigureAwait(False)
            Dim ShowID As Integer = -1
            If KodiTVShow IsNot Nothing Then
                ShowID = KodiTVShow.tvshowid
            End If
            If ShowID = -1 Then
                logger.Warn(String.Format("[APIKodi] [{0}] SearchTVSeason: ""{1}: Season {2}"" | NOT found in host database!", _currenthost.Label, tDBElement.ShowPath, tDBElement.TVSeason.Season))
                Return Nothing
            End If

            'get a list of all seasons saved in Kodi DB by ShowID
            Dim kTVSeasons As VideoLibrary.GetSeasonsResponse = Await GetAll_TVSeasons(ShowID).ConfigureAwait(False)

            If kTVSeasons IsNot Nothing Then
                If kTVSeasons.seasons IsNot Nothing Then
                    Dim result = kTVSeasons.seasons.FirstOrDefault(Function(f) f.season = tDBElement.TVSeason.Season)
                    If result IsNot Nothing Then
                        logger.Trace(String.Format("[APIKodi] [{0}] SearchTVSeason: ""{1}: Season {2}"" | OK, found in host database! [ID:{3}]", _currenthost.Label, tDBElement.ShowPath, tDBElement.TVSeason.Season, result.seasonid))
                        Return result
                    Else
                        logger.Warn(String.Format("[APIKodi] [{0}] SearchTVSeason: ""{1}: Season {2}"" | NOT found in host database!", _currenthost.Label, tDBElement.ShowPath, tDBElement.TVSeason.Season))
                        Return Nothing
                    End If
                Else
                    logger.Warn(String.Format("[APIKodi] [{0}] SearchTVSeason: ""{1}: Season {2}"" | NOT found in host database!", _currenthost.Label, tDBElement.ShowPath, tDBElement.TVSeason.Season))
                    Return Nothing
                End If
            Else
                logger.Error(String.Format("[APIKodi] [{0}] SearchTVSeason: ""{1}: Season {2}"" | No connection to Host!", _currenthost.Label, tDBElement.ShowPath, tDBElement.TVSeason.Season))
                Return Nothing
            End If
        End Function

        Private Async Function Search_TVShow(ByVal tDBElement As Database.DBElement) As Task(Of Video.Details.TVShow)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] SearchTVShow: No host initialized! Abort!")
                Return Nothing
            End If

            Dim kTVShows As VideoLibrary.GetTVShowsResponse

            Dim tPathAndFilename As PathAndFilename = GetMediaPathAndFilename(tDBElement, Enums.ContentType.TVShow)
            Dim strRemotePath As String = GetRemotePath(tPathAndFilename.Path)

            If Not String.IsNullOrEmpty(strRemotePath) Then
                Try
                    Dim filter As New List.Filter.TVShowsAnd With {.and = New List(Of Object)}
                    Dim filterRule As New List.Filter.Rule.TVShows With {
                        .field = List.Filter.Fields.TVShows.path,
                        .Operator = List.Filter.Operators.Is,
                        .value = strRemotePath
                    }
                    filter.and.Add(filterRule)

                    kTVShows = Await _kodi.VideoLibrary.GetTVShows(filter:=filter).ConfigureAwait(False)

                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                    Return Nothing
                End Try
            Else
                logger.Error(String.Format("[APIKodi] [{0}] SearchTVShow: ""{1}"" | Source not mapped!", _currenthost.Label, tDBElement.Source.Path))
                Return Nothing
            End If

            If kTVShows IsNot Nothing Then
                If kTVShows.tvshows IsNot Nothing Then
                    If kTVShows.tvshows.Count = 1 Then
                        logger.Trace(String.Format("[APIKodi] [{0}] SearchTVShow: ""{1}"" | OK, found in host database! [ID:{2}]", _currenthost.Label, tDBElement.ShowPath, kTVShows.tvshows.Item(0).tvshowid))
                        Return kTVShows.tvshows.Item(0)
                    ElseIf kTVShows.tvshows.Count > 1 Then
                        logger.Warn(String.Format("[APIKodi] [{0}] SearchTVShow: ""{1}"" | MORE THAN ONE tv show found in host database!", _currenthost.Label, tDBElement.ShowPath))
                        Return Nothing
                    Else
                        logger.Warn(String.Format("[APIKodi] [{0}] SearchTVShow: ""{1}"" | NOT found in host database!", _currenthost.Label, tDBElement.ShowPath))
                        Return Nothing
                    End If
                Else
                    logger.Warn(String.Format("[APIKodi] [{0}] SearchTVShow: ""{1}"" | NOT found in host database!", _currenthost.Label, tDBElement.ShowPath))
                    Return Nothing
                End If
            Else
                logger.Error(String.Format("[APIKodi] [{0}] SearchTVShow: ""{1}"" | No connection to Host!", _currenthost.Label, tDBElement.ShowPath))
                Return Nothing
            End If
        End Function
        ''' <summary>
        ''' Update movie details at Kodi
        ''' </summary>
        ''' <param name="mDBElement">Movie as DBElement</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Update successfull, false=error or movie not found in KodiDB</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation, main code by DanCooper
        ''' updates all movie fields which are filled/set in Ember (also paths of images)
        ''' at the moment the movie to update on host is identified by searching and comparing filename of movie(special handling for DVDs/Blurays), meaning there might be problems when filename is appearing more than once in movie library
        ''' </remarks>
        Public Async Function UpdateInfo_Movie(
                                              ByVal mDBElement As Database.DBElement,
                                              ByVal SendHostNotification As Boolean,
                                              ByVal GenericSubEvent As IProgress(Of GenericSubEventCallBackAsync),
                                              ByVal GenericMainEvent As IProgress(Of GenericEventCallBackAsync)
                                              ) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] UpdateInfo_Movie: No host initialized! Abort!")
                Return False
            End If

            Dim bIsNew As Boolean = False

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] UpdateInfo_Movie: ""{1}"" | Start syncing process...", _currenthost.Label, mDBElement.Movie.Title))

                'search Movie ID in Kodi DB
                Dim KodiElement As Video.Details.Movie = Await GetFullDetailsByID_Movie(Await GetMediaID(mDBElement))

                'scan movie path
                If KodiElement Is Nothing Then
                    logger.Trace(String.Format("[APIKodi] [{0}] UpdateMovieInfo: ""{1}"" | NOT found in host database, scan directory on host...", _currenthost.Label, mDBElement.Movie.Title))
                    If Await VideoLibrary_ScanPath(mDBElement).ConfigureAwait(False) Then
                        While Await Host_IsScanningVideo()
                            Threading.Thread.Sleep(1000)
                        End While
                        KodiElement = Await GetFullDetailsByID_Movie(Await GetMediaID(mDBElement))
                        If KodiElement IsNot Nothing Then bIsNew = True
                    Else
                        logger.Error(String.Format("[APIKodi] [{0}] UpdateMovieInfo: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, mDBElement.Movie.Title))
                        Return False
                    End If
                End If

                If KodiElement IsNot Nothing Then

                    'string or string.empty
                    Dim mDateAdded As String = If(mDBElement.Movie.DateAddedSpecified, mDBElement.Movie.DateAdded, Nothing)
                    Dim mImdbnumber As String = mDBElement.Movie.UniqueIDs.IMDbId
                    Dim mLastPlayed As String = mDBElement.Movie.LastPlayed
                    Dim mMPAA As String = mDBElement.Movie.MPAA
                    Dim mOriginalTitle As String = mDBElement.Movie.OriginalTitle
                    Dim mOutline As String = mDBElement.Movie.Outline
                    Dim mPlot As String = mDBElement.Movie.Plot
                    Dim mPremiered As String = mDBElement.Movie.ReleaseDate
                    Dim mSet As String = If(mDBElement.Movie.SetsSpecified, mDBElement.Movie.Sets.Item(0).Title, String.Empty)
                    Dim mSortTitle As String = mDBElement.Movie.SortTitle
                    Dim mTagline As String = mDBElement.Movie.Tagline
                    Dim mTitle As String = mDBElement.Movie.Title
                    Dim mTrailer As String = If(Not String.IsNullOrEmpty(mDBElement.Trailer.LocalFilePath), GetRemotePath(mDBElement.Trailer.LocalFilePath), If(mDBElement.Movie.TrailerSpecified, mDBElement.Movie.Trailer, String.Empty))
                    If mTrailer Is Nothing Then mTrailer = String.Empty

                    'digit grouping symbol for Votes count
                    Dim mVotes As String = If(Not String.IsNullOrEmpty(mDBElement.Movie.Votes), mDBElement.Movie.Votes, Nothing)
                    If Master.eSettings.GeneralDigitGrpSymbolVotes Then
                        If mDBElement.Movie.VotesSpecified Then
                            Dim strVotes As String = Double.Parse(mDBElement.Movie.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                            If strVotes IsNot Nothing Then
                                mVotes = strVotes
                            End If
                        End If
                    End If

                    'integer or 0
                    Dim mPlaycount As Integer = If(mDBElement.Movie.PlayCountSpecified, mDBElement.Movie.PlayCount, 0)
                    Dim mRating As Double = If(mDBElement.Movie.RatingSpecified, CType(Double.Parse(mDBElement.Movie.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), Double), 0)
                    Dim mRuntime As Integer = 0
                    If mDBElement.Movie.RuntimeSpecified AndAlso Integer.TryParse(mDBElement.Movie.Runtime, 0) Then
                        mRuntime = CType(mDBElement.Movie.Runtime, Integer) * 60 'API requires runtime in seconds
                    End If
                    Dim mTop250 As Integer = mDBElement.Movie.Top250
                    Dim mUserRating As Integer = mDBElement.Movie.UserRating
                    Dim mYear As Integer = If(mDBElement.Movie.YearSpecified, CType(mDBElement.Movie.Year, Integer), 0)

                    'arrays
                    'Countries
                    Dim mCountryList As List(Of String) = If(mDBElement.Movie.CountriesSpecified, mDBElement.Movie.Countries, New List(Of String))

                    'Directors
                    Dim mDirectorList As List(Of String) = If(mDBElement.Movie.DirectorsSpecified, mDBElement.Movie.Directors, New List(Of String))

                    'Genres
                    Dim mGenreList As List(Of String) = If(mDBElement.Movie.GenresSpecified, mDBElement.Movie.Genres, New List(Of String))

                    'Studios
                    Dim mStudioList As List(Of String) = If(mDBElement.Movie.StudiosSpecified, mDBElement.Movie.Studios, New List(Of String))

                    'Tags
                    Dim mTagList As List(Of String) = If(mDBElement.Movie.TagsSpecified, mDBElement.Movie.Tags, New List(Of String))

                    'TVShowLinks
                    Dim mTVShowLinks As List(Of String) = If(mDBElement.Movie.ShowLinksSpecified, mDBElement.Movie.ShowLinks, New List(Of String))

                    'Writers (Credits)
                    Dim mWriterList As List(Of String) = If(mDBElement.Movie.CreditsSpecified, mDBElement.Movie.Credits, New List(Of String))


                    'string or null/nothing
                    Dim mBanner As String = If(mDBElement.ImagesContainer.Banner.LocalFilePathSpecified,
                                                  GetRemotePath(mDBElement.ImagesContainer.Banner.LocalFilePath), Nothing)
                    Dim mClearArt As String = If(mDBElement.ImagesContainer.ClearArt.LocalFilePathSpecified,
                                                  GetRemotePath(mDBElement.ImagesContainer.ClearArt.LocalFilePath), Nothing)
                    Dim mClearLogo As String = If(mDBElement.ImagesContainer.ClearLogo.LocalFilePathSpecified,
                                                  GetRemotePath(mDBElement.ImagesContainer.ClearLogo.LocalFilePath), Nothing)
                    Dim mDiscArt As String = If(mDBElement.ImagesContainer.DiscArt.LocalFilePathSpecified,
                                                  GetRemotePath(mDBElement.ImagesContainer.DiscArt.LocalFilePath), Nothing)
                    Dim mFanart As String = If(mDBElement.ImagesContainer.Fanart.LocalFilePathSpecified,
                                                 GetRemotePath(mDBElement.ImagesContainer.Fanart.LocalFilePath), Nothing)
                    Dim mKeyArt As String = If(mDBElement.ImagesContainer.KeyArt.LocalFilePathSpecified,
                                                  GetRemotePath(mDBElement.ImagesContainer.KeyArt.LocalFilePath), Nothing)
                    Dim mLandscape As String = If(mDBElement.ImagesContainer.Landscape.LocalFilePathSpecified,
                                                  GetRemotePath(mDBElement.ImagesContainer.Landscape.LocalFilePath), Nothing)
                    Dim mPoster As String = If(mDBElement.ImagesContainer.Poster.LocalFilePathSpecified,
                                                  GetRemotePath(mDBElement.ImagesContainer.Poster.LocalFilePath), Nothing)

                    'Artwork
                    Dim mArtwork As New Dictionary(Of String, String) From {
                        {"banner", mBanner},
                        {"clearart", mClearArt},
                        {"clearlogo", mClearLogo},
                        {"discart", mDiscArt},
                        {"fanart", mFanart},
                        {"keyart", mKeyArt},
                        {"landscape", mLandscape},
                        {"poster", mPoster}
                    }
                    'remove other artwork stored in Kodi's database
                    For Each oldArtwork In KodiElement.art.Where(Function(f) Not mArtwork.ContainsKey(f.Key))
                        mArtwork.Add(oldArtwork.Key, Nothing)
                    Next

                    'UniquieIDs
                    Dim mUniqueID As New Dictionary(Of String, String)
                    For Each nUniqueID In mDBElement.Movie.UniqueIDs.Items
                        mUniqueID.Add(nUniqueID.Type, nUniqueID.Value)
                    Next
                    'remove other unique ID's stored in Kodi's database
                    For Each oldUniqueID In KodiElement.uniqueid.Where(Function(f) Not mUniqueID.ContainsKey(f.Key))
                        mUniqueID.Add(oldUniqueID.Key, Nothing)
                    Next

                    'Ratings
                    Dim mRatings As New Dictionary(Of String, Video.Rating)
                    For Each nRating In mDBElement.Movie.Ratings
                        mRatings.Add(nRating.Name, New Video.Rating With {
                                     .[default] = nRating.IsDefault,
                                     .rating = nRating.Value,
                                     .votes = nRating.Votes})
                    Next
                    'remove other ratings stored in Kodi's database
                    For Each oldRating In KodiElement.ratings.Where(Function(f) Not mRatings.ContainsKey(f.Key))
                        mRatings.Add(oldRating.Key, Nothing)
                    Next

                    Dim response As String = Await _kodi.VideoLibrary.SetMovieDetails(
                        KodiElement.movieid,
                        title:=mTitle,
                        playcount:=mPlaycount,
                        runtime:=mRuntime,
                        director:=mDirectorList,
                        studio:=mStudioList,
                        year:=mYear,
                        plot:=mPlot,
                        genre:=mGenreList,
                        rating:=mRating,
                        mpaa:=mMPAA,
                        imdbnumber:=mImdbnumber,
                        votes:=mVotes,
                        lastplayed:=mLastPlayed,
                        originaltitle:=mOriginalTitle,
                        trailer:=mTrailer,
                        tagline:=mTagline,
                        plotoutline:=mOutline,
                        writer:=mWriterList,
                        country:=mCountryList,
                        top250:=mTop250,
                        sorttitle:=mSortTitle,
                        set:=mSet,
                        showlink:=mTVShowLinks,
                        tag:=mTagList,
                        art:=mArtwork,
                        userrating:=mUserRating,
                        ratings:=mRatings,
                        dateadded:=mDateAdded,
                        premiered:=mPremiered,
                        uniqueid:=mUniqueID).ConfigureAwait(False)

                    If response.Contains("error") Then
                        logger.Error(String.Format("[APIKodi] [{0}] UpdateInfo_Movie: {1}", _currenthost.Label, response))
                        Return False
                    Else
                        'Remove old textures (cache)
                        Await Remove_Textures(mDBElement)

                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await Host_SendMessage("Ember Media Manager", If(bIsNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & mDBElement.Movie.Title).ConfigureAwait(False)
                        End If

                        logger.Trace(String.Format("[APIKodi] [{0}] UpdateInfo_Movie: ""{1}"" | {2} on host", _currenthost.Label, mDBElement.Movie.Title, If(bIsNew, "Added", "Updated")))
                        Return True
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] UpdateInfo_Movie: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, mDBElement.Movie.Title))
                    Return False
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Update movieset details at Kodi
        ''' </summary>
        ''' <param name="mDBElement">MovieSet as DBElement</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Update successfull, false=error or movieset not found in KodiDB</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation, main code by DanCooper
        ''' updates all movieset fields which are filled/set in Ember (also paths of images)
        ''' </remarks>
        Public Async Function UpdateInfo_MovieSet(
                                                 ByVal mDBElement As Database.DBElement,
                                                 ByVal SendHostNotification As Boolean
                                                 ) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] UpdateInfo_MovieSet: No host initialized! Abort!")
                Return False
            End If

            Dim bIsNew As Boolean = False

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] UpdateInfo_MovieSet: ""{1}"" | Start syncing process...", _currenthost.Label, mDBElement.MovieSet.Title))

                'search MovieSet ID in Kodi DB
                Dim KodiElement As Video.Details.MovieSet = Await GetFullDetailsByID_MovieSet(Await GetMediaID(mDBElement))

                If KodiElement Is Nothing Then
                    logger.Error(String.Format("[APIKodi] [{0}] UpdateInfo_MovieSet: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, mDBElement.MovieSet.Title))
                    Return False
                    'what to do in this case?
                    'Await VideoLibrary_ScanPath(uMovieset).ConfigureAwait(False)
                    'Threading.Thread.Sleep(2000) 'TODO better solution for this?!
                    'KodiElement = Await GetFullDetailsByID_MovieSet(Await GetMediaID(uMovieset))
                    'If KodiElement IsNot Nothing Then bIsNew = True
                End If

                If KodiElement IsNot Nothing Then

                    'string or string.empty
                    Dim mTitle As String = mDBElement.MovieSet.Title
                    Dim mPlot As String = mDBElement.MovieSet.Plot

                    'string or null/nothing
                    Dim mBanner As String = If(mDBElement.ImagesContainer.Banner.LocalFilePathSpecified,
                                                  GetRemotePath_MovieSet(mDBElement.ImagesContainer.Banner.LocalFilePath), Nothing)
                    Dim mClearArt As String = If(mDBElement.ImagesContainer.ClearArt.LocalFilePathSpecified,
                                                  GetRemotePath_MovieSet(mDBElement.ImagesContainer.ClearArt.LocalFilePath), Nothing)
                    Dim mClearLogo As String = If(mDBElement.ImagesContainer.ClearLogo.LocalFilePathSpecified,
                                                  GetRemotePath_MovieSet(mDBElement.ImagesContainer.ClearLogo.LocalFilePath), Nothing)
                    Dim mDiscArt As String = If(mDBElement.ImagesContainer.DiscArt.LocalFilePathSpecified,
                                                  GetRemotePath_MovieSet(mDBElement.ImagesContainer.DiscArt.LocalFilePath), Nothing)
                    Dim mFanart As String = If(mDBElement.ImagesContainer.Fanart.LocalFilePathSpecified,
                                                 GetRemotePath_MovieSet(mDBElement.ImagesContainer.Fanart.LocalFilePath), Nothing)
                    Dim mKeyArt As String = If(mDBElement.ImagesContainer.KeyArt.LocalFilePathSpecified,
                                                  GetRemotePath_MovieSet(mDBElement.ImagesContainer.KeyArt.LocalFilePath), Nothing)
                    Dim mLandscape As String = If(mDBElement.ImagesContainer.Landscape.LocalFilePathSpecified,
                                                  GetRemotePath_MovieSet(mDBElement.ImagesContainer.Landscape.LocalFilePath), Nothing)
                    Dim mPoster As String = If(mDBElement.ImagesContainer.Poster.LocalFilePathSpecified,
                                                  GetRemotePath_MovieSet(mDBElement.ImagesContainer.Poster.LocalFilePath), Nothing)

                    'Artwork
                    Dim mArtwork As New Dictionary(Of String, String) From {
                        {"banner", mBanner},
                        {"clearart", mClearArt},
                        {"clearlogo", mClearLogo},
                        {"discart", mDiscArt},
                        {"fanart", mFanart},
                        {"keyart", mKeyArt},
                        {"landscape", mLandscape},
                        {"poster", mPoster}
                    }
                    'remove other artwork stored in Kodi's database
                    For Each oldArtwork In KodiElement.art.Where(Function(f) Not mArtwork.ContainsKey(f.Key))
                        mArtwork.Add(oldArtwork.Key, Nothing)
                    Next

                    Dim response As String = Await _kodi.VideoLibrary.SetMovieSetDetails(
                        KodiElement.setid,
                        title:=mTitle,
                        plot:=mPlot,
                        art:=mArtwork).ConfigureAwait(False)

                    If response.Contains("error") Then
                        logger.Error(String.Format("[APIKodi] [{0}] UpdateInfo_MovieSet: {1}", _currenthost.Label, response))
                        Return False
                    Else
                        'Remove old textures (cache)
                        Await Remove_Textures(mDBElement)

                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await Host_SendMessage("Ember Media Manager", If(bIsNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & mDBElement.MovieSet.Title).ConfigureAwait(False)
                        End If
                        logger.Trace(String.Format("[APIKodi] [{0}] UpdateInfo_MovieSet: ""{1}"" | {2} on host", _currenthost.Label, mDBElement.MovieSet.Title, If(bIsNew, "Added", "Updated")))
                        Return True
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] UpdateInfo_MovieSet: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, mDBElement.MovieSet.Title))
                    Return False
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Update episode details at Kodi
        ''' </summary>
        ''' <param name="mDBElement">TVEpisode as DBElement</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Update successfull, false=error or episode not found in KodiDB</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' updates all episode fields (also pathes of images)
        ''' at the moment episode on host is identified by searching and comparing filename of episode
        ''' </remarks>
        Public Async Function UpdateInfo_TVEpisode(
                                                  ByVal mDBElement As Database.DBElement,
                                                  ByVal SendHostNotification As Boolean,
                                                  ByVal GenericSubEvent As IProgress(Of GenericSubEventCallBackAsync),
                                                  ByVal GenericMainEvent As IProgress(Of GenericEventCallBackAsync)
                                                  ) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] UpdateInfo_TVEpisode: No host initialized! Abort!")
                Return False
            End If

            Dim bIsNew As Boolean = False

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] UpdateInfo_TVEpisode: ""{1}"" | Start syncing process...", _currenthost.Label, mDBElement.TVEpisode.Title))

                'search TV Episode ID in Kodi DB
                Dim KodiElement As Video.Details.Episode = Await GetFullDetailsByID_TVEpisode(Await GetMediaID(mDBElement))

                'scan tv show path
                If KodiElement Is Nothing Then
                    logger.Trace(String.Format("[APIKodi] [{0}] UpdateTVEpisodeInfo: ""{1}"" | NOT found in database, scan directory on host...", _currenthost.Label, mDBElement.TVEpisode.Title))
                    If Await VideoLibrary_ScanPath(mDBElement).ConfigureAwait(False) Then
                        While Await Host_IsScanningVideo()
                            Threading.Thread.Sleep(1000)
                        End While
                        KodiElement = Await GetFullDetailsByID_TVEpisode(Await GetMediaID(mDBElement))
                        If KodiElement IsNot Nothing Then bIsNew = True
                    Else
                        logger.Error(String.Format("[APIKodi] [{0}] UpdateTVEpisodeInfo: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, mDBElement.TVEpisode.Title))
                        Return False
                    End If
                End If

                If KodiElement IsNot Nothing Then

                    'string or string.empty
                    Dim mAired As String = mDBElement.TVEpisode.Aired
                    Dim mDateAdded As String = mDBElement.TVEpisode.DateAdded
                    Dim mLastPlayed As String = mDBElement.TVEpisode.LastPlayed
                    Dim mOriginalTitle As String = mDBElement.TVEpisode.OriginalTitle
                    Dim mPlot As String = mDBElement.TVEpisode.Plot
                    Dim mTitle As String = mDBElement.TVEpisode.Title

                    'digit grouping symbol for Votes count
                    Dim mVotes As String = If(Not String.IsNullOrEmpty(mDBElement.TVEpisode.Votes), mDBElement.TVEpisode.Votes, Nothing)
                    If Master.eSettings.GeneralDigitGrpSymbolVotes Then
                        If mDBElement.TVEpisode.VotesSpecified Then
                            Dim vote As String = Double.Parse(mDBElement.TVEpisode.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                            If vote IsNot Nothing Then
                                mVotes = vote
                            End If
                        End If
                    End If

                    'integer or 0
                    Dim mPlaycount As Integer = If(mDBElement.TVEpisode.PlaycountSpecified, CType(mDBElement.TVEpisode.Playcount, Integer), 0)
                    Dim mRating As Double = If(mDBElement.TVEpisode.RatingSpecified, CType(Double.Parse(mDBElement.TVEpisode.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), Double), 0)
                    Dim mRuntime As Integer = 0
                    If mDBElement.TVEpisode.RuntimeSpecified AndAlso Integer.TryParse(mDBElement.TVEpisode.Runtime, 0) Then
                        mRuntime = CType(mDBElement.TVEpisode.Runtime, Integer) * 60 'API requires runtime in seconds
                    End If
                    Dim mUserRating As Integer = mDBElement.TVEpisode.UserRating

                    'arrays
                    'Directors
                    Dim mDirectorList As List(Of String) = If(mDBElement.TVEpisode.DirectorsSpecified, mDBElement.TVEpisode.Directors, New List(Of String))

                    'Writers (Credits)
                    Dim mWriterList As List(Of String) = If(mDBElement.TVEpisode.CreditsSpecified, mDBElement.TVEpisode.Credits, New List(Of String))

                    'string or null/nothing
                    Dim mFanart As String = If(mDBElement.ImagesContainer.Fanart.LocalFilePathSpecified,
                                                 GetRemotePath(mDBElement.ImagesContainer.Fanart.LocalFilePath), Nothing)
                    Dim mPoster As String = If(mDBElement.ImagesContainer.Poster.LocalFilePathSpecified,
                                                  GetRemotePath(mDBElement.ImagesContainer.Poster.LocalFilePath), Nothing)

                    'Artwork
                    Dim mArtwork As New Dictionary(Of String, String) From {
                        {"fanart", mFanart},
                        {"thumb", mPoster}
                    }
                    'remove other artwork stored in Kodi's database
                    For Each oldArtwork In KodiElement.art.Where(Function(f) Not mArtwork.ContainsKey(f.Key) AndAlso Not f.Key.Contains("."))
                        mArtwork.Add(oldArtwork.Key, Nothing)
                    Next

                    'UniquieIDs
                    Dim mUniqueID As New Dictionary(Of String, String)
                    For Each nUniqueID In mDBElement.TVEpisode.UniqueIDs.Items
                        mUniqueID.Add(nUniqueID.Type, nUniqueID.Value)
                    Next
                    'remove other unique ID's stored in Kodi's database
                    For Each oldUniqueID In KodiElement.uniqueid.Where(Function(f) Not mUniqueID.ContainsKey(f.Key))
                        mUniqueID.Add(oldUniqueID.Key, Nothing)
                    Next

                    'Ratings
                    Dim mRatings As New Dictionary(Of String, Video.Rating)
                    For Each nRating In mDBElement.TVEpisode.Ratings
                        mRatings.Add(nRating.Name, New Video.Rating With {
                                     .[default] = nRating.IsDefault,
                                     .rating = nRating.Value,
                                     .votes = nRating.Votes})
                    Next
                    'remove other ratings stored in Kodi's database
                    For Each oldRating In KodiElement.ratings.Where(Function(f) Not mRatings.ContainsKey(f.Key))
                        mRatings.Add(oldRating.Key, Nothing)
                    Next

                    Dim response As String = Await _kodi.VideoLibrary.SetEpisodeDetails(
                        KodiElement.episodeid,
                        title:=mTitle,
                        playcount:=mPlaycount,
                        runtime:=mRuntime,
                        director:=mDirectorList,
                        plot:=mPlot,
                        rating:=mRating,
                        votes:=mVotes,
                        lastplayed:=mLastPlayed,
                        writer:=mWriterList,
                        art:=mArtwork,
                        firstaired:=mAired,
                        userrating:=mUserRating,
                        ratings:=mRatings,
                        dateadded:=mDateAdded,
                        uniqueid:=mUniqueID).ConfigureAwait(False)

                    If response.Contains("error") Then
                        logger.Error(String.Format("[APIKodi] [{0}] UpdateInfo_TVEpisode: {1}", _currenthost.Label, response))
                        Return False
                    Else
                        'Remove old textures (cache)
                        Await Remove_Textures(mDBElement)

                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await Host_SendMessage("Ember Media Manager", If(bIsNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & mDBElement.TVShow.Title & ": " & mDBElement.TVEpisode.Title).ConfigureAwait(False)
                        End If

                        logger.Trace(String.Format("[APIKodi] [{0}] UpdateInfo_TVEpisode: ""{1}"" | {2} on host", _currenthost.Label, mDBElement.TVEpisode.Title, If(bIsNew, "Added", "Updated")))
                        Return True
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] UpdateInfo_TVEpisode: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, mDBElement.TVEpisode.Title))
                    Return False
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Update season details at Kodi
        ''' </summary>
        ''' <param name="mDBElement">TVSeason as DBElement</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Update successfull, false=error or season not found in KodiDB</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation, main code by DanCooper
        ''' updates all movieset fields which are filled/set in Ember (also paths of images)
        ''' </remarks>
        Public Async Function UpdateInfo_TVSeason(
                                                 ByVal mDBElement As Database.DBElement,
                                                 ByVal SendHostNotification As Boolean,
                                                 ByVal GenericSubEvent As IProgress(Of GenericSubEventCallBackAsync),
                                                 ByVal GenericMainEvent As IProgress(Of GenericEventCallBackAsync)
                                                 ) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Warn("[APIKodi] UpdateInfo_TVSeason: No host initialized! Abort!")
                Return False
            End If

            Dim bIsNew As Boolean = False

            If mDBElement.TVSeason.IsAllSeasons Then
                logger.Info(String.Format("[APIKodi] [{0}] UpdateInfo_TVSeason: ""{1}: Season {2}"" | Skip syncing process (* All Seasons entry can't be synced)", _currenthost.Label, mDBElement.ShowPath, mDBElement.TVSeason.Season))
                Return True
            End If

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] UpdateInfo_TVSeason: ""{1}: Season {2}"" | Start syncing process...", _currenthost.Label, mDBElement.ShowPath, mDBElement.TVSeason.Season))

                'search TVSeason ID in Kodi DB
                Dim KodiElement As Video.Details.Season = Await GetFullDetailsByID_TVSeason(Await GetMediaID(mDBElement))

                'scan tv show path
                If KodiElement Is Nothing Then
                    logger.Trace(String.Format("[APIKodi] [{0}] UpdateTVSeasonInfo: ""{1}: Season {2}"" | NOT found in host database, scan directory on host...", _currenthost.Label, mDBElement.ShowPath, mDBElement.TVSeason.Season))
                    If Await VideoLibrary_ScanPath(mDBElement).ConfigureAwait(False) Then
                        While Await Host_IsScanningVideo()
                            Threading.Thread.Sleep(1000)
                        End While
                        KodiElement = Await GetFullDetailsByID_TVSeason(Await GetMediaID(mDBElement))
                        If KodiElement IsNot Nothing Then bIsNew = True
                    Else
                        logger.Error(String.Format("[APIKodi] [{0}] UpdateTVSeasonInfo: ""{1}: Season {2}"" | NOT found on host! Abort!", _currenthost.Label, mDBElement.ShowPath, mDBElement.TVSeason.Season))
                        Return False
                    End If
                End If

                If KodiElement IsNot Nothing Then
                    'string or string.empty
                    Dim mTitle As String = mDBElement.TVSeason.Title

                    'string or null/nothing
                    Dim mBanner As String = If(mDBElement.ImagesContainer.Banner.LocalFilePathSpecified,
                                                  GetRemotePath(mDBElement.ImagesContainer.Banner.LocalFilePath), Nothing)
                    Dim mFanart As String = If(mDBElement.ImagesContainer.Fanart.LocalFilePathSpecified,
                                                 GetRemotePath(mDBElement.ImagesContainer.Fanart.LocalFilePath), Nothing)
                    Dim mLandscape As String = If(mDBElement.ImagesContainer.Landscape.LocalFilePathSpecified,
                                                  GetRemotePath(mDBElement.ImagesContainer.Landscape.LocalFilePath), Nothing)
                    Dim mPoster As String = If(mDBElement.ImagesContainer.Poster.LocalFilePathSpecified,
                                                  GetRemotePath(mDBElement.ImagesContainer.Poster.LocalFilePath), Nothing)

                    'Artwork
                    Dim mArtwork As New Dictionary(Of String, String)
                    mArtwork.Add("banner", mBanner)
                    mArtwork.Add("fanart", mFanart)
                    mArtwork.Add("landscape", mLandscape)
                    mArtwork.Add("poster", mPoster)
                    'remove other artwork stored in Kodi's database
                    For Each oldArtwork In KodiElement.art.Where(Function(f) Not mArtwork.ContainsKey(f.Key))
                        mArtwork.Add(oldArtwork.Key, Nothing)
                    Next

                    Dim response As String = Await _kodi.VideoLibrary.SetSeasonDetails(
                        KodiElement.seasonid,
                        title:=mTitle,
                        art:=mArtwork).ConfigureAwait(False)

                    If response.Contains("error") Then
                        logger.Error(String.Format("[APIKodi] [{0}] UpdateInfo_TVSeason: {1}", _currenthost.Label, response))
                        Return False
                    Else
                        'Remove old textures (cache)
                        Await Remove_Textures(mDBElement)

                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await Host_SendMessage("Ember Media Manager", If(bIsNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & mDBElement.TVShow.Title & ": Season " & mDBElement.TVSeason.Season).ConfigureAwait(False)
                        End If

                        'Sync Episodes
                        If mDBElement.EpisodesSpecified Then
                            For Each tEpisode As Database.DBElement In mDBElement.Episodes
                                If tEpisode.TVShow Is Nothing Then Master.DB.AddTVShowInfoToDBElement(tEpisode, mDBElement)
                                Await Task.Run(Function() UpdateInfo_TVEpisode(tEpisode, SendHostNotification, GenericSubEvent, GenericMainEvent))
                            Next
                        End If

                        logger.Trace(String.Format("[APIKodi] [{0}] UpdateInfo_TVSeason: ""{1}: Season {2}"" | {3} on host", _currenthost.Label, mDBElement.ShowPath, mDBElement.TVSeason.Season, If(bIsNew, "Added", "Updated")))
                        Return True
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] UpdateInfo_TVSeason: ""{1}: Season {2}"" | NOT found on host! Abort!", _currenthost.Label, mDBElement.ShowPath, mDBElement.TVSeason.Season))
                    Return False
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Update TVShow details at Kodi
        ''' </summary>
        ''' <param name="mDBElement">TVShow as DBElement</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Update successfull, false=error or show not found in KodiDB</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' updates all TVShow fields (also paths of images)
        ''' at the moment TVShow on host is identified by searching and comparing path of TVShow
        ''' </remarks>
        Public Async Function UpdateInfo_TVShow(
                                               ByVal mDBElement As Database.DBElement,
                                               ByVal SendHostNotification As Boolean,
                                               ByVal GenericSubEvent As IProgress(Of GenericSubEventCallBackAsync),
                                               ByVal GenericMainEvent As IProgress(Of GenericEventCallBackAsync)
                                               ) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] UpdateInfo_TVShow: No host initialized! Abort!")
                Return False
            End If

            Dim bIsNew As Boolean = False

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] UpdateInfo_TVShow: ""{1}"" | Start syncing process...", _currenthost.Label, mDBElement.TVShow.Title))

                'search TVShow ID in Kodi DB
                Dim KodiElement As Video.Details.TVShow = Await GetFullDetailsByID_TVShow(Await GetMediaID(mDBElement))

                'scan tv show path
                If KodiElement Is Nothing Then
                    logger.Trace(String.Format("[APIKodi] [{0}] UpdateTVShowInfo: ""{1}"" | NOT found in host database, scan directory on host...", _currenthost.Label, mDBElement.TVShow.Title))
                    If Await VideoLibrary_ScanPath(mDBElement).ConfigureAwait(False) Then
                        While Await Host_IsScanningVideo()
                            Threading.Thread.Sleep(1000)
                        End While
                        KodiElement = Await GetFullDetailsByID_TVShow(Await GetMediaID(mDBElement))
                        If KodiElement IsNot Nothing Then bIsNew = True
                    Else
                        logger.Error(String.Format("[APIKodi] [{0}] UpdateTVShowInfo: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, mDBElement.TVShow.Title))
                        Return False
                    End If
                End If

                If KodiElement IsNot Nothing Then

                    'string or string.empty
                    Dim mDateAdded As String = If(mDBElement.TVShow.DateAddedSpecified, mDBElement.TVShow.DateAdded, Nothing)
                    Dim mEpisodeGuide As String = mDBElement.TVShow.EpisodeGuide.URL
                    Dim mImdbnumber As String = mDBElement.TVShow.UniqueIDs.TVDbId
                    Dim mMPAA As String = mDBElement.TVShow.MPAA
                    Dim mOriginalTitle As String = mDBElement.TVShow.OriginalTitle
                    Dim mPlot As String = mDBElement.TVShow.Plot
                    Dim mPremiered As String = mDBElement.TVShow.Premiered
                    Dim mSortTitle As String = mDBElement.TVShow.SortTitle
                    Dim mStatus As String = mDBElement.TVShow.Status
                    Dim mTitle As String = mDBElement.TVShow.Title

                    'digit grouping symbol for Votes count
                    Dim mVotes As String = If(Not String.IsNullOrEmpty(mDBElement.TVShow.Votes), mDBElement.TVShow.Votes, Nothing)
                    If Master.eSettings.GeneralDigitGrpSymbolVotes Then
                        If mDBElement.TVShow.VotesSpecified Then
                            Dim vote As String = Double.Parse(mDBElement.TVShow.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                            If vote IsNot Nothing Then
                                mVotes = vote
                            End If
                        End If
                    End If

                    'integer or 0
                    Dim mRating As Double = If(mDBElement.TVShow.RatingSpecified, CType(Double.Parse(mDBElement.TVShow.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), Double), 0)
                    Dim mRuntime As Integer = If(mDBElement.TVShow.RuntimeSpecified, CType(mDBElement.TVShow.Runtime, Integer), 0)
                    Dim mUserRating As Integer = mDBElement.TVShow.UserRating

                    'arrays
                    'Genres
                    Dim mGenreList As List(Of String) = If(mDBElement.TVShow.GenresSpecified, mDBElement.TVShow.Genres, New List(Of String))

                    'Studios
                    Dim mStudioList As List(Of String) = If(mDBElement.TVShow.StudiosSpecified, mDBElement.TVShow.Studios, New List(Of String))

                    'Tags
                    Dim mTagList As List(Of String) = If(mDBElement.TVShow.Tags.Count > 0, mDBElement.TVShow.Tags, New List(Of String))

                    'string or null/nothing
                    Dim mBanner As String = If(mDBElement.ImagesContainer.Banner.LocalFilePathSpecified,
                                                  GetRemotePath(mDBElement.ImagesContainer.Banner.LocalFilePath), Nothing)
                    Dim mCharacterArt As String = If(mDBElement.ImagesContainer.CharacterArt.LocalFilePathSpecified,
                                               GetRemotePath(mDBElement.ImagesContainer.CharacterArt.LocalFilePath), Nothing)
                    Dim mClearArt As String = If(mDBElement.ImagesContainer.ClearArt.LocalFilePathSpecified,
                                                GetRemotePath(mDBElement.ImagesContainer.ClearArt.LocalFilePath), Nothing)
                    Dim mClearLogo As String = If(mDBElement.ImagesContainer.ClearLogo.LocalFilePathSpecified,
                                                GetRemotePath(mDBElement.ImagesContainer.ClearLogo.LocalFilePath), Nothing)
                    Dim mFanart As String = If(mDBElement.ImagesContainer.Fanart.LocalFilePathSpecified,
                                               GetRemotePath(mDBElement.ImagesContainer.Fanart.LocalFilePath), Nothing)
                    Dim mKeyArt As String = If(mDBElement.ImagesContainer.KeyArt.LocalFilePathSpecified,
                                              GetRemotePath(mDBElement.ImagesContainer.KeyArt.LocalFilePath), Nothing)
                    Dim mLandscape As String = If(mDBElement.ImagesContainer.Landscape.LocalFilePathSpecified,
                                              GetRemotePath(mDBElement.ImagesContainer.Landscape.LocalFilePath), Nothing)
                    Dim mPoster As String = If(mDBElement.ImagesContainer.Poster.LocalFilePathSpecified,
                                                 GetRemotePath(mDBElement.ImagesContainer.Poster.LocalFilePath), Nothing)
                    Dim mIMDB As String = If(mDBElement.TVShow.UniqueIDs.IMDbIdSpecified, mDBElement.TVShow.UniqueIDs.IMDbId, Nothing)
                    Dim mTMDB As String = If(mDBElement.TVShow.UniqueIDs.TMDbIdSpecified, mDBElement.TVShow.UniqueIDs.TMDbId, Nothing)
                    Dim mTVDB As String = If(mDBElement.TVShow.UniqueIDs.TVDbIdSpecified, mDBElement.TVShow.UniqueIDs.TVDbId, Nothing)

                    'Artwork
                    Dim mArtwork As New Dictionary(Of String, String)
                    mArtwork.Add("banner", mBanner)
                    mArtwork.Add("characterart", mCharacterArt)
                    mArtwork.Add("clearart", mClearArt)
                    mArtwork.Add("clearlogo", mClearLogo)
                    mArtwork.Add("fanart", mFanart)
                    mArtwork.Add("keyart", mKeyArt)
                    mArtwork.Add("landscape", mLandscape)
                    mArtwork.Add("poster", mPoster)
                    'remove other artwork stored in Kodi's database
                    For Each oldArtwork In KodiElement.art.Where(Function(f) Not mArtwork.ContainsKey(f.Key))
                        mArtwork.Add(oldArtwork.Key, Nothing)
                    Next

                    'UniquieIDs
                    Dim mUniqueID As New Dictionary(Of String, String)
                    For Each nUniqueID In mDBElement.TVShow.UniqueIDs.Items
                        mUniqueID.Add(nUniqueID.Type, nUniqueID.Value)
                    Next
                    'remove other unique ID's stored in Kodi's database
                    For Each oldUniqueID In KodiElement.uniqueid.Where(Function(f) Not mUniqueID.ContainsKey(f.Key))
                        mUniqueID.Add(oldUniqueID.Key, Nothing)
                    Next

                    'Ratings
                    Dim mRatings As New Dictionary(Of String, Video.Rating)
                    For Each nRating In mDBElement.TVShow.Ratings
                        mRatings.Add(nRating.Name, New Video.Rating With {
                                     .[default] = nRating.IsDefault,
                                     .rating = nRating.Value,
                                     .votes = nRating.Votes})
                    Next
                    'remove other ratings stored in Kodi's database
                    For Each oldRating In KodiElement.ratings.Where(Function(f) Not mRatings.ContainsKey(f.Key))
                        mRatings.Add(oldRating.Key, Nothing)
                    Next

                    Dim response As String = Await _kodi.VideoLibrary.SetTVShowDetails(
                        KodiElement.tvshowid,
                        title:=mTitle,
                        studio:=mStudioList,
                        plot:=mPlot,
                        genre:=mGenreList,
                        rating:=mRating,
                        mpaa:=mMPAA,
                        imdbnumber:=mImdbnumber,
                        premiered:=mPremiered,
                        votes:=mVotes,
                        originaltitle:=mOriginalTitle,
                        sorttitle:=mSortTitle,
                        episodeguide:=mEpisodeGuide,
                        tag:=mTagList,
                        art:=mArtwork,
                        runtime:=mRuntime,
                        status:=mStatus,
                        uniqueid:=mUniqueID,
                        userrating:=mUserRating,
                        ratings:=mRatings,
                        dateadded:=mDateAdded
                        ).ConfigureAwait(False)

                    If response.Contains("error") Then
                        logger.Error(String.Format("[APIKodi] [{0}] UpdateInfo_TVShow: {1}", _currenthost.Label, response))
                        Return False
                    Else
                        'Remove old textures (cache)
                        Await Remove_Textures(mDBElement)

                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await Host_SendMessage("Ember Media Manager", If(bIsNew, Master.eLang.GetString(881, "Added"), Master.eLang.GetString(1408, "Updated")) & ": " & mDBElement.TVShow.Title).ConfigureAwait(False)
                        End If

                        'Sync Episodes
                        If mDBElement.EpisodesSpecified Then
                            For Each tEpisode As Database.DBElement In mDBElement.Episodes.Where(Function(f) f.FileItemSpecified)
                                If tEpisode.TVShow Is Nothing Then Master.DB.AddTVShowInfoToDBElement(tEpisode, mDBElement)
                                Await Task.Run(Function() UpdateInfo_TVEpisode(tEpisode, SendHostNotification, GenericSubEvent, GenericMainEvent))
                            Next
                        End If

                        'Sync Seasons
                        If mDBElement.SeasonsSpecified Then
                            For Each tSeason As Database.DBElement In mDBElement.Seasons
                                If tSeason.TVShow Is Nothing Then Master.DB.AddTVShowInfoToDBElement(tSeason, mDBElement)
                                Await Task.Run(Function() UpdateInfo_TVSeason(tSeason, SendHostNotification, GenericSubEvent, GenericMainEvent))
                            Next
                        End If

                        logger.Trace(String.Format("[APIKodi] [{0}] UpdateInfo_TVShow: ""{1}"" | {2} on host", _currenthost.Label, mDBElement.TVShow.Title, If(bIsNew, "Added", "Updated")))
                        Return True
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] UpdateInfo_TVShow: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, mDBElement.TVShow.Title))
                    Return False
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
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
        Public Async Function VideoLibrary_Clean() As Task(Of String)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] VideoLibrary_Clean: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Dim response As String = String.Empty
                response = Await _kodi.VideoLibrary.Clean.ConfigureAwait(False)
                logger.Trace("[APIKodi] VideoLibrary_Clean: " & _currenthost.Label)
                Return response
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Triggered as soon as cleaning of video library is finished
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>just an example for eventhandler</remarks>
        Private Sub VideoLibrary_OnCleanFinished(ByVal sender As String, ByVal data As Object)
            'Finished cleaning of video library
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), _currenthost.Label & " | " & Master.eLang.GetString(1450, "Cleaning Video Library...") & " OK!", New Bitmap(My.Resources.logo)}))
        End Sub

        ''' <summary>
        ''' Triggered as soon as video library scan (whole database not specific folder!) is finished
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>just an example for eventhandler</remarks>
        Private Sub VideoLibrary_OnScanFinished(ByVal sender As String, ByVal data As Object)
            'Finished updating video library
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), _currenthost.Label & " | " & Master.eLang.GetString(1448, "Updating Video Library...") & " OK!", New Bitmap(My.Resources.logo)}))
        End Sub
        ''' <summary>
        ''' Scan video library of Kodi host
        ''' </summary>
        ''' <returns>string with status message, if failed: Nothing</returns>
        ''' <remarks>
        ''' 2015/06/27 Cocotus - First implementation
        ''' </remarks>
        Public Async Function VideoLibrary_Scan() As Task(Of String)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] VideoLibrary_Scan: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Dim response As String = String.Empty
                response = Await _kodi.VideoLibrary.Scan.ConfigureAwait(False)
                logger.Trace("[APIKodi] VideoLibrary_Scan: " & _currenthost.Label)
                Return response
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Scan specific directory for new content
        ''' </summary>
        ''' <param name="tDBElement"></param>
        ''' <returns></returns>
        Public Async Function VideoLibrary_ScanPath(ByVal tDBElement As Database.DBElement) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] VideoLibrary_ScanPath: No host initialized! Abort!")
                Return Nothing
            End If

            Dim strLocalPath As String = String.Empty

            Select Case tDBElement.ContentType
                Case Enums.ContentType.Movie
                    strLocalPath = tDBElement.FileItem.MainPath.FullName
                Case Enums.ContentType.TVEpisode, Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                    'workaround for bug in Kodi JSON (needs DirectorySeparatorChar at the end of path to recognize new tv shows)
                    If tDBElement.ShowPath.Contains(Path.DirectorySeparatorChar) Then
                        strLocalPath = String.Concat(tDBElement.ShowPath, Path.DirectorySeparatorChar)
                    ElseIf tDBElement.ShowPath.Contains(Path.AltDirectorySeparatorChar) Then
                        strLocalPath = String.Concat(tDBElement.ShowPath, Path.AltDirectorySeparatorChar)
                    End If
                Case Else
                    logger.Warn(String.Format("[APIKodi] [{0}] VideoLibrary_ScanPath: No videotype specified! Abort!", _currenthost.Label))
                    Return False
            End Select

            If strLocalPath.Contains(Path.DirectorySeparatorChar) AndAlso Not strLocalPath.EndsWith(Path.DirectorySeparatorChar) Then
                strLocalPath = String.Concat(strLocalPath, Path.DirectorySeparatorChar)
            ElseIf strLocalPath.Contains(Path.AltDirectorySeparatorChar) AndAlso Not strLocalPath.EndsWith(Path.AltDirectorySeparatorChar) Then
                strLocalPath = String.Concat(strLocalPath, Path.AltDirectorySeparatorChar)
            End If

            Dim strRemotePath As String = GetRemotePath(strLocalPath)
            If String.IsNullOrEmpty(strRemotePath) Then
                Return False
            End If
            logger.Trace(String.Format("[APIKodi] [{0}] VideoLibrary_ScanPaths: ""{1}"" | Start scanning process...", _currenthost.Label, strRemotePath))
            Dim strResponse = Await _kodi.VideoLibrary.Scan(strRemotePath).ConfigureAwait(False)
            If strResponse.ToLower.Contains("error") Then
                logger.Trace(String.Format("[APIKodi] [{0}] VideoLibrary_ScanPath: ""{1}"" | {2}", _currenthost.Label, strRemotePath, strResponse))
                Return False
            Else
                Return True
            End If
        End Function
        ''' <summary>
        ''' Remove Movie details at Kodi
        ''' </summary>
        ''' <param name="mDBElement">Movie as DBElement</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Remove successfull, false=error or movie not found in KodiDB</returns>
        ''' <remarks>
        ''' </remarks>
        Public Async Function Remove_Movie(ByVal mDBElement As Database.DBElement,
                                           ByVal SendHostNotification As Boolean,
                                           ByVal GenericSubEvent As IProgress(Of GenericSubEventCallBackAsync),
                                           ByVal GenericMainEvent As IProgress(Of GenericEventCallBackAsync)
                                           ) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] [Remove_Movie]: No host initialized! Abort!")
                Return False
            End If

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] [Remove_Movie]: ""{1}"" | Start removing process...", _currenthost.Label, mDBElement.Movie.Title))

                'search Movie ID in Kodi DB
                Dim KodiElement As Video.Details.Movie = Await GetFullDetailsByID_Movie(Await GetMediaID(mDBElement))

                If KodiElement IsNot Nothing Then
                    Dim response = Await _kodi.VideoLibrary.RemoveMovie(KodiElement.movieid).ConfigureAwait(False)

                    If response.Contains("error") Then
                        logger.Error(String.Format("[APIKodi] [{0}] [Remove_Movie]: {1}", _currenthost.Label, response))
                        Return False
                    Else
                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await Host_SendMessage("Ember Media Manager", String.Format("{0}: {1}", Master.eLang.GetString(1024, "Removed"), mDBElement.Movie.Title)).ConfigureAwait(False)
                        End If
                        logger.Trace(String.Format("[APIKodi] [{0}] [Remove_Movie]: ""{1}"" | {2} on host", _currenthost.Label, mDBElement.Movie.Title, "Removed"))
                        Return True
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] [Remove_Movie]: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, mDBElement.Movie.Title))
                    Return False
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Remove TVEpisode details at Kodi
        ''' </summary>
        ''' <param name="mDBElement">TVEpisode as DBElement</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Remove successfull, false=error or episode not found in KodiDB</returns>
        ''' <remarks>
        ''' </remarks>
        Public Async Function Remove_TVEpisode(
                                              ByVal mDBElement As Database.DBElement,
                                              ByVal SendHostNotification As Boolean,
                                              ByVal GenericSubEvent As IProgress(Of GenericSubEventCallBackAsync),
                                              ByVal GenericMainEvent As IProgress(Of GenericEventCallBackAsync)
                                              ) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] [Remove_TVEpisode]: No host initialized! Abort!")
                Return False
            End If

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] [Remove_TVEpisode]: ""{1}"" | Start removing process...", _currenthost.Label, mDBElement.TVEpisode.Title))

                'search TV Episode ID in Kodi DB
                Dim KodiElement As Video.Details.Episode = Await GetFullDetailsByID_TVEpisode(Await GetMediaID(mDBElement))

                If KodiElement IsNot Nothing Then
                    Dim response = Await _kodi.VideoLibrary.RemoveEpisode(KodiElement.episodeid).ConfigureAwait(False)

                    If response.Contains("error") Then
                        logger.Error(String.Format("[APIKodi] [{0}] [Remove_TVEpisode]: {1}", _currenthost.Label, response))
                        Return False
                    Else
                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await Host_SendMessage("Ember Media Manager", String.Format("{0}: {1}: {2}", Master.eLang.GetString(1024, "Removed"), mDBElement.TVShow.Title, mDBElement.TVEpisode.Title)).ConfigureAwait(False)
                        End If
                        logger.Trace(String.Format("[APIKodi] [{0}] [Remove_TVEpisode]: ""{1}"" | {2} on host", _currenthost.Label, mDBElement.TVEpisode.Title, "Removed"))
                        Return True
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] [Remove_TVEpisode]: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, mDBElement.TVEpisode.Title))
                    Return False
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Remove TVShow details at Kodi
        ''' </summary>
        ''' <param name="mDBElement">TVShow as DBElement</param>
        ''' <param name="SendHostNotification">Send notification to host</param>
        ''' <returns>true=Remove successfull, false=error or tv show not found in KodiDB</returns>
        ''' <remarks>
        ''' </remarks>
        Public Async Function Remove_TVShow(
                                           ByVal mDBElement As Database.DBElement,
                                           ByVal SendHostNotification As Boolean,
                                           ByVal GenericSubEvent As IProgress(Of GenericSubEventCallBackAsync),
                                           ByVal GenericMainEvent As IProgress(Of GenericEventCallBackAsync)
                                           ) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] [Remove_TVShow]: No host initialized! Abort!")
                Return False
            End If

            Try
                logger.Trace(String.Format("[APIKodi] [{0}] [Remove_TVShow]: ""{1}"" | Start removing process...", _currenthost.Label, mDBElement.TVShow.Title))

                'search TVShow ID in Kodi DB
                Dim KodiElement As Video.Details.TVShow = Await GetFullDetailsByID_TVShow(Await GetMediaID(mDBElement))

                If KodiElement IsNot Nothing Then
                    Dim response = Await _kodi.VideoLibrary.RemoveTVShow(KodiElement.tvshowid).ConfigureAwait(False)

                    If response.Contains("error") Then
                        logger.Error(String.Format("[APIKodi] [{0}] [Remove_TVShow]: {1}", _currenthost.Label, response))
                        Return False
                    Else
                        'Remove old textures (cache)
                        Await Remove_Textures(mDBElement)

                        'Send message to Kodi?
                        If SendHostNotification = True Then
                            Await Host_SendMessage("Ember Media Manager", String.Format("{0}: {1}", Master.eLang.GetString(1024, "Removed"), mDBElement.TVShow.Title)).ConfigureAwait(False)
                        End If

                        logger.Trace(String.Format("[APIKodi] [{0}] [Remove_TVShow]: ""{1}"" | {2} on host", _currenthost.Label, mDBElement.TVShow.Title, "Removed"))
                        Return True
                    End If
                Else
                    logger.Error(String.Format("[APIKodi] [{0}] [Remove_TVShow]: ""{1}"" | NOT found on host! Abort!", _currenthost.Label, mDBElement.TVShow.Title))
                    Return False
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Remove a cached image by Texture ID
        ''' </summary>
        ''' <param name="ID">ID of Texture</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Async Function Remove_Texture(ByVal KodiTextureID As Integer) As Task(Of String)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] RemoveTexture: No host initialized! Abort!")
                Return Nothing
            End If

            Try
                Return Await _kodi.Textures.RemoveTexture(KodiTextureID)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function
        ''' <summary>
        ''' Removes all cached images by a given path
        ''' </summary>
        ''' <param name="LocalPath"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Async Function Remove_Textures(ByVal tDBElement As Database.DBElement) As Task(Of Boolean)
            If _kodi Is Nothing Then
                logger.Error("[APIKodi] RemoveTextures: No host initialized! Abort!")
                Return False
            End If

            Try
                Dim TexturesResponce = Await GetTextures(tDBElement)
                If TexturesResponce IsNot Nothing Then
                    For Each tTexture In TexturesResponce.textures
                        Await Remove_Texture(tTexture.textureid)
                    Next
                Else
                    Return False
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return False
            End Try
            Return True
        End Function

#End Region 'Methods

#Region "Nested Types"

        Public Class PathAndFilename

#Region "Properties"

            Public Property Path As String = String.Empty

            Public Property Filename As String = String.Empty

#End Region 'Properties

        End Class

        Public Class WatchedState

#Region "Properties"

            Public Property AlreadyInSync() As Boolean = False

            Public Property LastPlayed() As String = String.Empty

            Public Property PlayCount() As Integer = 0

#End Region 'Properties

        End Class

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

#Region "Methods"

        Public Function GetSocket() As ISocket Implements ISocketFactory.GetSocket
            Return New DummySocket()
        End Function

        Public Async Function ResolveHostname(hostname As String) As Task(Of String()) Implements ISocketFactory.ResolveHostname
            Return Await ResolveHostname(hostname).ConfigureAwait(False)
        End Function

#End Region 'Methods

    End Class

    Friend Class DummySocket
        Implements ISocket

#Region "Fields"

        Private _socket As Net.Sockets.Socket

#End Region 'Fields

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

    End Class

#End Region 'Client JSON Communication helper (needed for listening to notification events in Kodi)

End Namespace
