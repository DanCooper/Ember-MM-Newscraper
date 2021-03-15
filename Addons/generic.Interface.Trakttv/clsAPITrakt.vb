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

Imports EmberAPI
Imports NLog
Imports TraktApiSharp
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks

Public Class clsAPITrakt

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()
    Private _client As TraktClient

#End Region 'Fields

#Region "Delegates"

    Public Delegate Function ShowProgress(ByVal iProgress As Integer, ByVal strMessage As String) As Boolean

#End Region 'Delegates

#Region "Properties"

    ReadOnly Property AccessToken() As String
        Get
            Return _client.Authorization.AccessToken
        End Get
    End Property

    ReadOnly Property Created() As Date
        Get
            Return _client.Authorization.Created
        End Get
    End Property

    ReadOnly Property ExpiresInSeconds() As Integer
        Get
            Return _client.Authorization.ExpiresInSeconds
        End Get
    End Property

    ReadOnly Property RefreshToken() As String
        Get
            Return _client.Authorization.RefreshToken
        End Get
    End Property

#End Region 'Properties

#Region "Events"

    Public Event NewTokenCreated()

#End Region 'Events 

#Region "Methods"

    Public Function CheckConnection() As Boolean
        Try
            Dim APIResult = Task.Run(Function() _client.Authentication.CheckIfAuthorizationIsExpiredOrWasRevokedAsync(True))
            If APIResult.Result.First AndAlso APIResult.Result.Second IsNot Nothing Then
                _client.Authorization = APIResult.Result.Second
                RaiseEvent NewTokenCreated()
            ElseIf APIResult.Result.First OrElse _client.Authorization.IsExpired OrElse Not _client.Authorization.IsValid Then
                CreateAPI(New Addon.AddonSettings, _client.ClientId, _client.ClientSecret)
            End If
        Catch ex As Exception
            CreateAPI(New Addon.AddonSettings, _client.ClientId, _client.ClientSecret)
        End Try
        Return Not _client.Authorization.IsExpired AndAlso _client.Authorization.IsValid
    End Function

    Public Sub CreateAPI(ByVal tAddonSettings As Addon.AddonSettings, ByVal clientId As String, ByVal clientSecret As String)
        'Default lifetime of an AccessToken is 90 days. 
        'So we set the default CreatedAt age to 91 days to get shure that the default value Is to old And a New AccessToken has to be created.
        Dim dCreatedAt As Date = Date.Today.AddDays(-91)
        Dim iCreatedAt As Long = 0
        Dim iExpiresIn As Integer = 0

        Integer.TryParse(tAddonSettings.APIExpiresInSeconds, iExpiresIn)
        If Long.TryParse(tAddonSettings.APICreated, iCreatedAt) Then
            dCreatedAt = Functions.ConvertFromUnixTimestamp(iCreatedAt)
        End If

        _client = New TraktClient(clientId, clientSecret)
        _client.Authorization = Authentication.TraktAuthorization.CreateWith(dCreatedAt, iExpiresIn, tAddonSettings.APIAccessToken, tAddonSettings.APIRefreshToken)

        If Not _client.IsValidForUseWithAuthorization Then
            If _client.Authorization.IsRefreshPossible Then
                RefreshAuthorization()
            End If
        End If

        If Not _client.IsValidForUseWithAuthorization Then
            Try
                Dim strActivationURL = _client.OAuth.CreateAuthorizationUrl()
                Using dAuthorize As New frmAuthorize
                    If dAuthorize.ShowDialog(strActivationURL) = DialogResult.OK Then
                        Dim APIResult = Task.Run(Function() _client.OAuth.GetAuthorizationAsync(dAuthorize.Result))
                        APIResult.Wait()
                        If _client.IsValidForUseWithAuthorization Then
                            RaiseEvent NewTokenCreated()
                        End If
                    End If
                End Using
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
    End Sub

    Public Function RefreshAuthorization() As Boolean
        If _client.Authorization.IsRefreshPossible Then
            Try
                Dim APIResult = Task.Run(Function() _client.OAuth.RefreshAuthorizationAsync)
                If APIResult.Result.IsValid Then
                    _client.Authorization = APIResult.Result
                    RaiseEvent NewTokenCreated()
                    Return True
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                _client.Authorization = New Authentication.TraktAuthorization
                CreateAPI(New Addon.AddonSettings, _client.ClientId, _client.ClientSecret)
            End Try
        Else
            _client.Authorization = New Authentication.TraktAuthorization
            CreateAPI(New Addon.AddonSettings, String.Empty, String.Empty)
        End If
        Return False
    End Function

    'Public Function AddToCollection(ByVal tCollectionItems As Objects.Post.Syncs.Collection.TraktSyncCollectionPost) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionPostResponse
    '    If CheckConnection() Then
    '        Dim APIResult = Task.Run(Function() _client.Sync.AddCollectionItemsAsync(tCollectionItems))
    '        If APIResult.Exception Is Nothing Then
    '            logger.Info(String.Format("[APITrakt] [AddToCollection] [Added] Episodes: {0} | Movies: {1}",
    '                                          APIResult.Result.Value.Added.Episodes.Value,
    '                                          APIResult.Result.Value.Added.Movies.Value))
    '            logger.Info(String.Format("[APITrakt] [AddToCollection] [Not Found] Episodes: {0} | Movies: {1}",
    '                                          APIResult.Result.Value.NotFound.Episodes.Count,
    '                                          APIResult.Result.Value.NotFound.Movies.Count))
    '            Return APIResult.Result.Value
    '        End If
    '    End If
    '    Return Nothing
    'End Function

    'Public Function AddToCollection_Movie(ByVal tTraktMovie As Objects.Get.Movies.Implementations.TraktMovie, Optional collectedAt As Date = Nothing) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionPostResponse
    '    Dim nCollectionItems As New Objects.Post.Syncs.Collection.TraktSyncCollectionPostBuilder
    '    nCollectionItems.AddMovie(tTraktMovie, collectedAt)
    '    Return AddToCollection(nCollectionItems.Build)
    'End Function

    'Public Function AddToCollection_Movie(ByVal tDBElement As Database.DBElement, Optional ByVal bUseDateNow As Boolean = False) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionPostResponse
    '    Dim nITraktMovie = GetMovie(tDBElement)
    '    Dim nTraktMovie As New Objects.Get.Movies.Implementations.TraktMovie
    '    nTraktMovie.Ids = nITraktMovie.Ids
    '    Dim nCollectionItems As New Objects.Post.Syncs.Collection.TraktSyncCollectionPostBuilder
    '    nCollectionItems.AddMovie(nTraktMovie, If(bUseDateNow, Nothing, Functions.ConvertFromUnixTimestamp(tDBElement.DateAdded)))
    '    Return AddToCollection(nCollectionItems.Build)
    'End Function

    'Public Function AddToCollection_Movies(ByVal tTraktMovies As List(Of Objects.Get.Movies.Implementations.TraktMovie)) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionPostResponse
    '    Dim nCollectionItems As New Objects.Post.Syncs.Collection.TraktSyncCollectionPostBuilder
    '    For Each nMovie In tTraktMovies
    '        nCollectionItems.AddMovie(nMovie)
    '    Next
    '    Return AddToCollection(nCollectionItems.Build)
    'End Function

    'Public Function AddToCollection_TVEpisode(ByVal tTraktEpisode As Objects.Get.Episodes.Implementations.TraktEpisode, Optional dCollectedAt As Date = Nothing) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionPostResponse
    '    Dim nCollectionItems As New Objects.Post.Syncs.Collection.TraktSyncCollectionPostBuilder
    '    nCollectionItems.AddEpisode(tTraktEpisode, dCollectedAt)
    '    Return AddToCollection(nCollectionItems.Build)
    'End Function

    'Public Function AddToCollection_TVEpisodes(ByVal tTraktEpisodes As List(Of Objects.Get.Episodes.Implementations.TraktEpisode)) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionPostResponse
    '    Dim nCollectionItems As New Objects.Post.Syncs.Collection.TraktSyncCollectionPostBuilder
    '    For Each nTVEpisode In tTraktEpisodes
    '        nCollectionItems.AddEpisode(nTVEpisode)
    '    Next
    '    Return AddToCollection(nCollectionItems.Build)
    'End Function

    'Public Function AddToCollection_TVShow(ByVal tTraktShow As Objects.Get.Shows.Implementations.TraktShow, Optional dCollectedAt As Date = Nothing) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionPostResponse
    '    Dim nCollectionItems As New Objects.Post.Syncs.Collection.TraktSyncCollectionPostBuilder
    '    nCollectionItems.AddShow(tTraktShow, dCollectedAt)
    '    Return AddToCollection(nCollectionItems.Build)
    'End Function

    'Public Function AddToCollection_TVShows(ByVal tTraktShows As List(Of Objects.Get.Shows.Implementations.TraktShow)) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionPostResponse
    '    Dim nCollectionItems As New Objects.Post.Syncs.Collection.TraktSyncCollectionPostBuilder
    '    For Each nTVShow In tTraktShows
    '        nCollectionItems.AddShow(nTVShow)
    '    Next
    '    Return AddToCollection(nCollectionItems.Build)
    'End Function

    'Public Function AddToWatchedHistory(ByVal tCollectionItems As Objects.Post.Syncs.History.TraktSyncHistoryPost) As Objects.Post.Syncs.History.Responses.ITraktSyncHistoryPostResponse
    '    If CheckConnection() Then
    '        Dim APIResult = _client.Sync.AddWatchedHistoryItemsAsync(tCollectionItems).Result
    '        If APIResult.Exception Is Nothing Then
    '            logger.Info(String.Format("[APITrakt] [AddToWatchedHistory] [Added] Episodes: {0} | Movies: {1}",
    '                                          APIResult.Value.Added.Episodes.Value,
    '                                          APIResult.Value.Added.Movies.Value))
    '            logger.Info(String.Format("[APITrakt] [AddToWatchedHistory] [Not Found] Episodes: {0} | Movies: {1}",
    '                                          APIResult.Value.NotFound.Episodes.Count,
    '                                          APIResult.Value.NotFound.Movies.Count))
    '            Return APIResult.Value
    '        End If
    '    End If
    '    Return Nothing
    'End Function
    '''' <summary>
    '''' Adds a single movie to the user watched history
    '''' </summary>
    '''' <param name="tTraktMovie"></param>
    '''' <param name="dCollectedAt">Has to be UTC DateTime</param>
    '''' <returns>Response</returns>
    'Public Function AddToWatchedHistory_Movie(ByVal tTraktMovie As Objects.Get.Movies.Implementations.TraktMovie, Optional dCollectedAt As Date = Nothing) As Objects.Post.Syncs.History.Responses.ITraktSyncHistoryPostResponse
    '    Dim nHistoryItems As New Objects.Post.Syncs.History.TraktSyncHistoryPostBuilder
    '    nHistoryItems.AddMovie(tTraktMovie, dCollectedAt)
    '    Return AddToWatchedHistory(nHistoryItems.Build)
    'End Function

    'Public Function AddToWatchedHistory_Movie(ByVal tDBElement As Database.DBElement, Optional ByVal bUseDateNow As Boolean = False) As Objects.Post.Syncs.History.Responses.ITraktSyncHistoryPostResponse
    '    Dim nTraktMovie = GetMovie(tDBElement)
    '    Dim nHistoryItems As New Objects.Post.Syncs.History.TraktSyncHistoryPostBuilder
    '    'nHistoryItems.AddMovie(nTraktMovie, If(bUseDateNow, Nothing, Functions.ConvertFromUnixTimestamp(tDBElement.DateAdded).ToUniversalTime))
    '    Return AddToWatchedHistory(nHistoryItems.Build)
    'End Function

    'Public Function GetCollection_Movies() As IEnumerable(Of Objects.Get.Collections.ITraktCollectionMovie)
    '    If CheckConnection() Then
    '        Dim APIResult = Task.Run(Function() _client.Sync.GetCollectionMoviesAsync())
    '        If APIResult.Result.Exception Is Nothing Then
    '            Return APIResult.Result
    '        End If
    '    End If
    '    Return Nothing
    'End Function

    'Public Function GetCollection_TVShows() As IEnumerable(Of Objects.Get.Collections.ITraktCollectionShow)
    '    If CheckConnection() Then
    '        Dim APIResult = Task.Run(Function() _client.Sync.GetCollectionShowsAsync())
    '        If APIResult.Result.Exception Is Nothing Then
    '            Return APIResult.Result
    '        End If
    '    End If
    '    Return Nothing
    'End Function

    Public Async Function GetID_Trakt(ByVal tDBElement As Database.DBElement, Optional bForceTVShowID As Boolean = False) As Task(Of UInteger)
        Dim nSearchResults As Objects.Basic.TraktPaginationListResult(Of Objects.Basic.TraktSearchResult) = Nothing
        Dim nContentType As Enums.ContentType = If(bForceTVShowID, Enums.ContentType.TVShow, tDBElement.ContentType)

        If _client IsNot Nothing Then
            Select Case nContentType
                Case Enums.ContentType.Movie
                    'search by IMDB ID
                    If tDBElement.Movie.UniqueIDs.IMDbIdSpecified Then
                        nSearchResults = Await _client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.ImDB, tDBElement.Movie.UniqueIDs.IMDbId, TraktApiSharp.Enums.TraktSearchResultType.Movie)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    'search by TMDB ID
                    If (nSearchResults Is Nothing OrElse nSearchResults.ItemCount = 0) AndAlso tDBElement.Movie.UniqueIDs.TMDbIdSpecified Then
                        nSearchResults = Await _client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TmDB, tDBElement.Movie.UniqueIDs.TMDbId.ToString, TraktApiSharp.Enums.TraktSearchResultType.Movie)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    If nSearchResults IsNot Nothing AndAlso nSearchResults.ItemCount = 1 AndAlso nSearchResults(0).Movie IsNot Nothing Then
                        Return nSearchResults(0).Movie.Ids.Trakt
                    Else
                        logger.Info(String.Format("[GetID_Trakt] Could not scrape TraktID from trakt.tv! IMDB: {0} / TMDB: {1}", tDBElement.Movie.UniqueIDs.IMDbId, tDBElement.Movie.UniqueIDs.TMDbId))
                    End If
                Case Enums.ContentType.TVEpisode
                    'search by TVDB ID
                    If tDBElement.TVEpisode.UniqueIDs.TVDbIdSpecified Then
                        nSearchResults = Await _client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TvDB, tDBElement.TVEpisode.UniqueIDs.TVDbId.ToString, TraktApiSharp.Enums.TraktSearchResultType.Episode)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    'search by IMDB ID
                    If (nSearchResults Is Nothing OrElse nSearchResults.ItemCount = 0) AndAlso tDBElement.TVEpisode.UniqueIDs.IMDbIdSpecified Then
                        nSearchResults = Await _client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.ImDB, tDBElement.TVEpisode.UniqueIDs.IMDbId, TraktApiSharp.Enums.TraktSearchResultType.Episode)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    'search by TMDB ID
                    If (nSearchResults Is Nothing OrElse nSearchResults.ItemCount = 0) AndAlso tDBElement.TVEpisode.UniqueIDs.TMDbIdSpecified Then
                        nSearchResults = Await _client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TmDB, tDBElement.TVEpisode.UniqueIDs.TMDbId.ToString, TraktApiSharp.Enums.TraktSearchResultType.Episode)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    If nSearchResults IsNot Nothing AndAlso nSearchResults.ItemCount = 1 AndAlso nSearchResults(0).Episode IsNot Nothing Then
                        Return nSearchResults(0).Episode.Ids.Trakt
                    Else
                        logger.Info(String.Format("[GetID_Trakt] Could not scrape TraktID from trakt.tv! TVDB: {0} / IMDB: {1} / TMDB: {2}", tDBElement.TVEpisode.UniqueIDs.TVDbId, tDBElement.TVEpisode.UniqueIDs.IMDbId, tDBElement.TVEpisode.UniqueIDs.TMDbId))
                    End If
                Case Enums.ContentType.TVShow
                    'search by TVDB ID
                    If tDBElement.TVShow.UniqueIDs.TVDbIdSpecified Then
                        nSearchResults = Await _client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TvDB, tDBElement.TVShow.UniqueIDs.TVDbId.ToString, TraktApiSharp.Enums.TraktSearchResultType.Show)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    'search by IMDB ID
                    If (nSearchResults Is Nothing OrElse nSearchResults.ItemCount = 0) AndAlso tDBElement.TVShow.UniqueIDs.IMDbIdSpecified Then
                        nSearchResults = Await _client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.ImDB, tDBElement.TVShow.UniqueIDs.IMDbId, TraktApiSharp.Enums.TraktSearchResultType.Show)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    'search by TMDB ID
                    If (nSearchResults Is Nothing OrElse nSearchResults.ItemCount = 0) AndAlso tDBElement.TVShow.UniqueIDs.TMDbIdSpecified Then
                        nSearchResults = Await _client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TmDB, tDBElement.TVShow.UniqueIDs.TMDbId.ToString, TraktApiSharp.Enums.TraktSearchResultType.Show)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    If nSearchResults IsNot Nothing AndAlso nSearchResults.ItemCount = 1 AndAlso nSearchResults(0).Show IsNot Nothing Then
                        Return nSearchResults(0).Show.Ids.Trakt
                    Else
                        logger.Info(String.Format("[GetID_Trakt] Could not scrape TraktID from trakt.tv! TVDB: {0} / IMDB: {1} / TMDB: {2}", tDBElement.TVShow.UniqueIDs.TVDbId, tDBElement.TVShow.UniqueIDs.IMDbId, tDBElement.TVShow.UniqueIDs.TMDbId))
                    End If
            End Select
        End If

        Return 0
    End Function

    'Public Function GetMovie(ByVal strTraktIDOrSlug As String) As Objects.Get.Movies.TraktMovie
    '    If String.IsNullOrEmpty(strTraktIDOrSlug) OrElse strTraktIDOrSlug = "0" Then Return Nothing
    '    Dim nOptions As New Requests.Params.TraktExtendedInfo
    '    Dim APIResult = Task.Run(Function() _client.Movies.GetMovieAsync(strTraktIDOrSlug))
    '    If APIResult.Exception Is Nothing Then
    '        Return APIResult.Result
    '    End If
    '    Return Nothing
    'End Function

    'Public Function GetMovie(ByVal tDBElement As Database.DBElement) As Objects.Get.Movies.ITraktMovie
    '    Return GetMovie(GetID_Trakt(tDBElement).ToString)
    'End Function

    'Public Function GetTVShow(ByVal strTraktIDOrSlug As String) As Objects.Get.Shows.ITraktShow
    '    If String.IsNullOrEmpty(strTraktIDOrSlug) OrElse strTraktIDOrSlug = "0" Then Return Nothing

    '    If CheckConnection() Then
    '        Dim APIResult = Task.Run(Function() _client.Shows.GetShowAsync(strTraktIDOrSlug))
    '        If APIResult.Result.Exception Is Nothing Then
    '            Return APIResult.Result.Value
    '        End If
    '    End If
    '    Return Nothing
    'End Function

    'Public Function GetProgress_TVShows(ByVal strTraktIDOrSlug As String) As Objects.Get.Shows.ITraktShowWatchedProgress
    '    If String.IsNullOrEmpty(strTraktIDOrSlug) OrElse strTraktIDOrSlug = "0" Then Return Nothing

    '    If CheckConnection() Then
    '        Dim APIResult = Task.Run(Function() _client.Shows.GetShowWatchedProgressAsync(strTraktIDOrSlug, True, True, True))
    '        If APIResult.Result.Exception Is Nothing Then
    '            Return APIResult.Result.Value
    '        End If
    '    End If
    '    Return Nothing
    'End Function

    'Public Function GetRated_Movies() As IEnumerable(Of Objects.Get.Ratings.ITraktRatingsItem)
    '    If CheckConnection() Then
    '        Dim APIResult = Task.Run(Function() _client.Sync.GetRatingsAsync(TraktApiSharp.Enums.TraktRatingsItemType.Movie))
    '        If APIResult.Result.Exception Is Nothing Then
    '            Return APIResult.Result
    '        End If
    '    End If
    '    Return Nothing
    'End Function

    'Public Function GetRated_TVEpisodes() As IEnumerable(Of Objects.Get.Ratings.ITraktRatingsItem)
    '    If CheckConnection() Then
    '        Dim APIResult = Task.Run(Function() _client.Sync.GetRatingsAsync(TraktApiSharp.Enums.TraktRatingsItemType.Episode))
    '        If APIResult.Result.Exception Is Nothing Then
    '            Return APIResult.Result
    '        End If
    '    End If
    '    Return Nothing
    'End Function

    'Public Function GetRated_TVShows() As IEnumerable(Of Objects.Get.Ratings.ITraktRatingsItem)
    '    If CheckConnection() Then
    '        Dim APIResult = Task.Run(Function() _client.Sync.GetRatingsAsync(TraktApiSharp.Enums.TraktRatingsItemType.Show))
    '        If APIResult.Exception Is Nothing Then
    '            Return APIResult.Result
    '        End If
    '    End If
    '    Return Nothing
    'End Function

    Public Function GetWatched_Movies() As IEnumerable(Of Objects.Get.Watched.TraktWatchedMovie)
        If CheckConnection() Then
            Dim APIResult = Task.Run(Function() _client.Sync.GetWatchedMoviesAsync())
            If APIResult.Exception Is Nothing Then
                Return APIResult.Result
            End If
        End If
        Return Nothing
    End Function

    Public Function GetWatched_TVShows(Optional ByVal bGetFullInformation As Boolean = False) As IEnumerable(Of Objects.Get.Watched.TraktWatchedShow)
        If CheckConnection() Then
            Dim nOptions As Requests.Params.TraktExtendedInfo = Nothing
            If bGetFullInformation Then
                nOptions = New Requests.Params.TraktExtendedInfo With {.Full = True}
            End If
            Dim APIResult = Task.Run(Function() _client.Sync.GetWatchedShowsAsync(nOptions))
            If APIResult.Exception Is Nothing Then
                Return APIResult.Result
            End If
        End If
        Return Nothing
    End Function

    'Public Function GetWatchedAndRated_Movies() As List(Of WatchedAndRatedMovie)
    '    Return GetWatchedAndRated_Movies(GetWatched_Movies(), GetRated_Movies())
    'End Function

    'Public Function GetWatchedAndRated_Movies(ByVal tWatchedMovies As IEnumerable(Of Objects.Get.Watched.ITraktWatchedMovie), ByVal tRatedMovies As IEnumerable(Of Objects.Get.Ratings.ITraktRatingsItem)) As List(Of WatchedAndRatedMovie)
    '    If tWatchedMovies Is Nothing AndAlso tRatedMovies Is Nothing Then Return Nothing

    '    Dim lstWatchedAndRatedMovies As New List(Of WatchedAndRatedMovie)

    '    'add a watched movies and search for personal rating
    '    For Each nWatched In tWatchedMovies
    '        Dim nWatchedAndRatedMovie As New WatchedAndRatedMovie
    '        nWatchedAndRatedMovie.LastWatchedAt = nWatched.LastWatchedAt
    '        nWatchedAndRatedMovie.Movie = nWatched.Movie
    '        nWatchedAndRatedMovie.Plays = CInt(nWatched.Plays)

    '        'search rating for this movie
    '        Dim nRated = tRatedMovies.FirstOrDefault(Function(f) (f.Movie.Ids.Trakt = nWatched.Movie.Ids.Trakt))
    '        If nRated IsNot Nothing Then
    '            nWatchedAndRatedMovie.RatedAt = nRated.RatedAt
    '            nWatchedAndRatedMovie.Rating = CInt(nRated.Rating)
    '        End If

    '        lstWatchedAndRatedMovies.Add(nWatchedAndRatedMovie)
    '    Next

    '    'add movies that has been rated but not watched
    '    For Each nRated In tRatedMovies
    '        Dim nMovie = lstWatchedAndRatedMovies.FirstOrDefault(Function(f) f.Movie.Ids.Trakt = nRated.Movie.Ids.Trakt)
    '        If nMovie Is Nothing Then
    '            lstWatchedAndRatedMovies.Add(New WatchedAndRatedMovie With {
    '                                    .Movie = nRated.Movie,
    '                                    .RatedAt = nRated.RatedAt,
    '                                    .Rating = CInt(nRated.Rating)})
    '        End If
    '    Next

    '    Return lstWatchedAndRatedMovies
    'End Function

    'Public Function GetWatchedAndRated_TVShows() As List(Of WatchedAndRatedTVShow)
    '    Return GetWatchedAndRated_TVShows(GetWatched_TVShows(True), GetRated_TVShows())
    'End Function

    'Public Function GetWatchedAndRated_TVShows(ByVal tWatchedTVShows As IEnumerable(Of Objects.Get.Watched.ITraktWatchedShow), ByVal tRatedTVShows As IEnumerable(Of Objects.Get.Ratings.ITraktRatingsItem)) As List(Of WatchedAndRatedTVShow)
    '    If tWatchedTVShows Is Nothing AndAlso tRatedTVShows Is Nothing Then Return Nothing

    '    Dim lstWatchedAndRatedTVShows As New List(Of WatchedAndRatedTVShow)

    '    'add a watched tv shows and search for personal rating
    '    For Each nWatched In tWatchedTVShows
    '        Dim nWatchedAndRatedTVShow As New WatchedAndRatedTVShow
    '        nWatchedAndRatedTVShow.LastWatchedAt = nWatched.LastWatchedAt
    '        nWatchedAndRatedTVShow.Seasons = nWatched.Seasons
    '        nWatchedAndRatedTVShow.Show = nWatched.Show
    '        nWatchedAndRatedTVShow.Plays = CInt(nWatched.Plays)

    '        'search rating for this tv show
    '        Dim nRated = tRatedTVShows.FirstOrDefault(Function(f) (f.Show.Ids.Trakt = nWatched.Show.Ids.Trakt))
    '        If nRated IsNot Nothing Then
    '            nWatchedAndRatedTVShow.RatedAt = nRated.RatedAt
    '            nWatchedAndRatedTVShow.Rating = CInt(nRated.Rating)
    '        End If

    '        lstWatchedAndRatedTVShows.Add(nWatchedAndRatedTVShow)
    '    Next

    '    'add tv shows that has been rated but not watched
    '    For Each nRated In tRatedTVShows
    '        Dim nTVShow = lstWatchedAndRatedTVShows.FirstOrDefault(Function(f) f.Show.Ids.Trakt = nRated.Show.Ids.Trakt)
    '        If nTVShow Is Nothing Then
    '            lstWatchedAndRatedTVShows.Add(New WatchedAndRatedTVShow With {
    '                                    .RatedAt = nRated.RatedAt,
    '                                    .Rating = CInt(nRated.Rating),
    '                                    .Show = nRated.Show})
    '        End If
    '    Next

    '    'calculate watched episodes
    '    For Each nTVShow In lstWatchedAndRatedTVShows
    '        Dim iWatched As Integer = 0
    '        For Each nSeason In nTVShow.Seasons
    '            For Each nEpisode In nSeason.Episodes
    '                'If nEpisode.Plays IsNot Nothing AndAlso nEpisode.Plays > 0 Then
    '                'iWatched += 1
    '                'End If
    '            Next
    '        Next
    '        nTVShow.WatchedEpisodes = iWatched
    '    Next

    '    Return lstWatchedAndRatedTVShows
    'End Function

    'Public Function GetWatchedHistory_Movie(ByVal uintTraktID As UInteger) As IEnumerable(Of Objects.Get.History.ITraktHistoryItem)
    '    If CheckConnection() Then
    '        Dim APIResult = Task.Run(Function() _client.Sync.GetWatchedHistoryAsync(TraktApiSharp.Enums.TraktSyncItemType.Movie, uintTraktID))
    '        If APIResult.Exception Is Nothing Then
    '            Return APIResult.Result
    '        End If
    '    End If
    '    Return Nothing
    'End Function

    Public Function GetWatchedState_Movie(ByRef tDBElement As Database.DBElement,
                                          Optional ByRef watchedmovies As IEnumerable(Of Objects.Get.Watched.TraktWatchedMovie) = Nothing) As Boolean
        If Not tDBElement.Movie.UniqueIDsSpecified Then Return False

        Dim strIMDBID As String = tDBElement.Movie.UniqueIDs.IMDbId
        Dim intTMDBID As Integer = tDBElement.Movie.UniqueIDs.TMDbId

        Dim lstWatchedMovies As IEnumerable(Of Objects.Get.Watched.TraktWatchedMovie)
        If watchedmovies Is Nothing Then
            lstWatchedMovies = GetWatched_Movies()
        Else
            lstWatchedMovies = watchedmovies
        End If

        If lstWatchedMovies IsNot Nothing AndAlso lstWatchedMovies.Count > 0 Then
            Dim nWatchedMovie = lstWatchedMovies.FirstOrDefault(Function(f) (f.Movie.Ids.Imdb IsNot Nothing AndAlso f.Movie.Ids.Imdb = strIMDBID) OrElse
                                                      (f.Movie.Ids.Tmdb IsNot Nothing AndAlso CInt(f.Movie.Ids.Tmdb) = intTMDBID))
            If nWatchedMovie IsNot Nothing Then
                Dim strLastPlayed = Functions.ConvertToProperDateTime(nWatchedMovie.LastWatchedAt.Value.ToLocalTime.ToString)
                Dim iPlayCount = nWatchedMovie.Plays.Value
                If Not tDBElement.Movie.LastPlayed = strLastPlayed OrElse Not tDBElement.Movie.PlayCount = iPlayCount Then
                    tDBElement.Movie.LastPlayed = strLastPlayed
                    tDBElement.Movie.PlayCount = iPlayCount
                    Return True
                End If
            End If
        End If

        Return False
    End Function

    Public Function GetWatchedState_TVEpisode(ByRef tDBElement As Database.DBElement,
                                              Optional ByRef watchedshows As IEnumerable(Of Objects.Get.Watched.TraktWatchedShow) = Nothing) As Boolean
        If tDBElement.TVShow Is Nothing OrElse Not tDBElement.TVShow.UniqueIDsSpecified Then Return False

        Dim strShowIMDbId As String = tDBElement.TVShow.UniqueIDs.IMDbId
        Dim intShowTMDbId As Integer = tDBElement.TVShow.UniqueIDs.TMDbId
        Dim intShowTVDbId As Integer = tDBElement.TVShow.UniqueIDs.TVDbId

        Dim lstWatchedShows As IEnumerable(Of Objects.Get.Watched.TraktWatchedShow)
        If watchedshows Is Nothing Then
            lstWatchedShows = GetWatched_TVShows()
        Else
            lstWatchedShows = watchedshows
        End If

        If lstWatchedShows IsNot Nothing AndAlso lstWatchedShows.Count > 0 Then
            'search tv show
            Dim nTVShow = lstWatchedShows.FirstOrDefault(Function(f) (f.Show.Ids.Tvdb IsNot Nothing AndAlso CInt(f.Show.Ids.Tvdb) = intShowTVDbId) OrElse
                                                             (f.Show.Ids.Imdb IsNot Nothing AndAlso f.Show.Ids.Imdb = strShowIMDbId) OrElse
                                                             (f.Show.Ids.Tmdb IsNot Nothing AndAlso CInt(f.Show.Ids.Tmdb) = intShowTMDbId))
            If nTVShow IsNot Nothing Then
                Select Case tDBElement.ContentType
                    Case Enums.ContentType.TVEpisode
                        Dim intEpisode = tDBElement.TVEpisode.Episode
                        Dim intSeason = tDBElement.TVEpisode.Season

                        Dim nWatchedSeason = nTVShow.Seasons.Where(Function(f) CInt(f.Number) = intSeason).FirstOrDefault
                        If nWatchedSeason IsNot Nothing Then
                            Dim nWatchedEpisode = nWatchedSeason.Episodes.FirstOrDefault(Function(f) CInt(f.Number) = intEpisode)
                            If nWatchedEpisode IsNot Nothing Then
                                Dim strLastPlayed = Functions.ConvertToProperDateTime(nWatchedEpisode.LastWatchedAt.Value.ToLocalTime.ToString)
                                Dim iPlayCount = nWatchedEpisode.Plays.Value
                                If Not tDBElement.TVEpisode.LastPlayed = strLastPlayed OrElse Not tDBElement.TVEpisode.Playcount = iPlayCount Then
                                    tDBElement.TVEpisode.LastPlayed = strLastPlayed
                                    tDBElement.TVEpisode.Playcount = iPlayCount
                                    Return True
                                End If
                            End If
                        End If
                    Case Enums.ContentType.TVShow
                        For Each nTVEpisode In tDBElement.Episodes.Where(Function(f) f.FilenameSpecified)
                            Dim intEpisode = nTVEpisode.TVEpisode.Episode
                            Dim intSeason = nTVEpisode.TVEpisode.Season

                            Dim nWatchedSeason = nTVShow.Seasons.Where(Function(f) CInt(f.Number) = intSeason).FirstOrDefault
                            If nWatchedSeason IsNot Nothing Then
                                Dim nWatchedEpisode = nWatchedSeason.Episodes.FirstOrDefault(Function(f) CInt(f.Number) = intEpisode)
                                If nWatchedEpisode IsNot Nothing Then
                                    Dim strLastPlayed = Functions.ConvertToProperDateTime(nWatchedEpisode.LastWatchedAt.Value.ToLocalTime.ToString)
                                    Dim iPlayCount = nWatchedEpisode.Plays.Value
                                    If Not nTVEpisode.TVEpisode.LastPlayed = strLastPlayed OrElse Not nTVEpisode.TVEpisode.Playcount = iPlayCount Then
                                        nTVEpisode.TVEpisode.LastPlayed = strLastPlayed
                                        nTVEpisode.TVEpisode.Playcount = iPlayCount
                                    End If
                                End If
                            End If
                        Next
                        Return True
                End Select
            End If
        End If

        Return False
    End Function

    'Public Function RemoveFromCollection(ByVal tCollectionItems As Objects.Post.Syncs.Collection.TraktSyncCollectionPost) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionRemovePostResponse
    '    If CheckConnection() Then
    '        Dim APIResult = Task.Run(Function() _client.Sync.RemoveCollectionItemsAsync(tCollectionItems))
    '        If APIResult.Exception Is Nothing Then
    '            Return APIResult.Result.Value
    '        End If
    '    End If
    '    Return Nothing
    'End Function

    'Public Function RemoveFromCollection_Movie(ByVal tTraktMovie As Objects.Get.Movies.Implementations.TraktMovie) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionRemovePostResponse
    '    Dim tCollectionItems As New Objects.Post.Syncs.Collection.TraktSyncCollectionPostBuilder
    '    tCollectionItems.AddMovie(tTraktMovie)
    '    Return RemoveFromCollection(tCollectionItems.Build)
    'End Function

    'Public Function RemoveFromCollection_Movie(ByVal tDBElement As Database.DBElement) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionRemovePostResponse
    '    Dim nTraktMovie = GetMovie(tDBElement)
    '    Dim tCollectionItems As New Objects.Post.Syncs.Collection.TraktSyncCollectionPostBuilder
    '    'tCollectionItems.AddMovie(nTraktMovie)
    '    Return RemoveFromCollection(tCollectionItems.Build)
    'End Function

    'Public Function RemoveFromCollection_Movies(ByVal tTraktMovies As List(Of Objects.Get.Movies.Implementations.TraktMovie)) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionRemovePostResponse
    '    Dim tCollectionItems As New Objects.Post.Syncs.Collection.TraktSyncCollectionPostBuilder
    '    For Each nMovie In tTraktMovies
    '        tCollectionItems.AddMovie(nMovie)
    '    Next
    '    Return RemoveFromCollection(tCollectionItems.Build)
    'End Function

    'Public Function RemoveFromCollection_TVEpisode(ByVal tTraktEpisode As Objects.Get.Episodes.ITraktEpisode) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionRemovePostResponse
    '    Dim tCollectionItems As New Objects.Post.Syncs.Collection.TraktSyncCollectionPostBuilder
    '    'tCollectionItems.AddEpisode(tTraktEpisode)
    '    Return RemoveFromCollection(tCollectionItems.Build)
    'End Function

    'Public Function RemoveFromCollection_TVEpisodes(ByVal tTraktEpisodes As List(Of Objects.Get.Episodes.ITraktEpisode)) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionRemovePostResponse
    '    Dim tCollectionItems As New Objects.Post.Syncs.Collection.TraktSyncCollectionPostBuilder
    '    For Each nTVEpisode In tTraktEpisodes
    '        'tCollectionItems.AddEpisode(nTVEpisode)
    '    Next
    '    Return RemoveFromCollection(tCollectionItems.Build)
    'End Function

    'Public Function RemoveFromCollection_TVShow(ByVal tTraktShow As Objects.Get.Shows.ITraktShow) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionRemovePostResponse
    '    Dim tCollectionItems As New Objects.Post.Syncs.Collection.TraktSyncCollectionPostBuilder
    '    'tCollectionItems.AddShow(tTraktShow)
    '    Return RemoveFromCollection(tCollectionItems.Build)
    'End Function

    'Public Function RemoveFromCollection_TVShows(ByVal tTraktShows As List(Of Objects.Get.Shows.ITraktShow)) As Objects.Post.Syncs.Collection.Responses.ITraktSyncCollectionRemovePostResponse
    '    Dim tCollectionItems As New Objects.Post.Syncs.Collection.TraktSyncCollectionPostBuilder
    '    For Each nTVShow In tTraktShows
    '        'tCollectionItems.AddShow(nTVShow)
    '    Next
    '    Return RemoveFromCollection(tCollectionItems.Build)
    'End Function

    'Public Function RemoveFromWatchedHistory(ByVal tHistoryItems As Objects.Post.Syncs.History.TraktSyncHistoryRemovePost) As Objects.Post.Syncs.History.Responses.ITraktSyncHistoryRemovePostResponse
    '    If CheckConnection() Then
    '        Dim APIResult = Task.Run(Function() _client.Sync.RemoveWatchedHistoryItemsAsync(tHistoryItems))
    '        If APIResult.Exception Is Nothing Then
    '            logger.Info(String.Format("[APITrakt] [RemoveFromWatchedHistory] [Removed] Episodes: {0} | Movies: {1}",
    '                                      APIResult.Result.Value.Deleted.Episodes.Value,
    '                                      APIResult.Result.Value.Deleted.Movies.Value))
    '            logger.Info(String.Format("[APITrakt] [RemoveFromWatchedHistory] [Not Found] Episodes: {0} | Movies: {1}",
    '                                      APIResult.Result.Value.NotFound.Episodes.Count,
    '                                      APIResult.Result.Value.NotFound.Movies.Count))
    '            Return APIResult.Result.Value
    '        End If
    '    End If
    '    Return Nothing
    'End Function

    'Public Function RemoveFromWatchedHistory_Movie(ByVal tTraktMovie As Objects.Get.Movies.ITraktMovie) As Objects.Post.Syncs.History.Responses.ITraktSyncHistoryRemovePostResponse
    '    Dim tWatchedHistoryItems As New Objects.Post.Syncs.History.TraktSyncHistoryRemovePostBuilder
    '    'tWatchedHistoryItems.AddMovie(tTraktMovie)
    '    Return RemoveFromWatchedHistory(tWatchedHistoryItems.Build)
    'End Function

    'Public Sub SyncToEmber_All(Optional ByVal sfunction As ShowProgress = Nothing)
    '    SyncToEmber_Movies(sfunction)
    '    SyncToEmber_TVEpisodes(sfunction)
    'End Sub

    'Public Sub SyncToEmber_Movies(Optional ByVal sfunction As ShowProgress = Nothing)
    '    Dim WatchedMovies = GetWatched_Movies()
    '    If WatchedMovies IsNot Nothing Then
    '        SaveWatchedStateToEmber_Movies(WatchedMovies, sfunction)
    '    End If
    'End Sub

    'Public Sub SyncToEmber_TVEpisodes(Optional ByVal sfunction As ShowProgress = Nothing)
    '    'Dim WatchedTVEpisodes = GetWatched_TVShows()
    '    'If WatchedTVEpisodes IsNot Nothing Then
    '    '    SaveWatchedStateToEmber_TVEpisodes(WatchedTVEpisodes, sfunction)
    '    'End If
    'End Sub

    'Public Sub SyncTo_Trakt(ByVal tDBElement As Database.DBElement)
    '    Select Case tDBElement.ContentType
    '        Case Enums.ContentType.Movie
    '            SyncToTrakt_LastPlayed(tDBElement)
    '    End Select
    'End Sub

    'Public Sub SyncToTrakt_LastPlayed(ByVal tDBElement As Database.DBElement, Optional tTraktItem As Objects.Get.Movies.ITraktMovie = Nothing)
    '    Dim nTraktItem As Objects.Get.Movies.ITraktMovie
    '    If tTraktItem IsNot Nothing Then
    '        'use submitted TraktMovie
    '        nTraktItem = tTraktItem
    '    Else
    '        'search on Trakt
    '        nTraktItem = GetMovie(tDBElement)
    '    End If

    '    If nTraktItem IsNot Nothing Then
    '        If tDBElement.Movie.LastPlayedSpecified Then
    '            Dim dLastPlayed As New Date
    '            If Date.TryParse(tDBElement.Movie.LastPlayed, dLastPlayed) Then
    '                'Trakt always use UTC
    '                dLastPlayed = dLastPlayed.ToUniversalTime
    '                'get watched history from Trakt
    '                Dim nHistoryItems = GetWatchedHistory_Movie(nTraktItem.Ids.Trakt)
    '                If nHistoryItems IsNot Nothing Then
    '                    'check if the date already exist in the history
    '                    Dim bAlreadyInHistory As Boolean = nHistoryItems.Where(Function(f) f.WatchedAt IsNot Nothing AndAlso
    '                                                                               CDate(f.WatchedAt) = dLastPlayed).Count > 0
    '                    If Not bAlreadyInHistory Then
    '                        'add to Trakt watched history
    '                        'AddToWatchedHistory_Movie(nTraktItem, dLastPlayed)
    '                    End If
    '                End If
    '            End If
    '        Else
    '            'remove from Trakt watched history
    '            RemoveFromWatchedHistory_Movie(nTraktItem)
    '        End If
    '    End If
    'End Sub







    'Public Function AddToWatchedHistory_Movies(ByVal tMovies As TraktAPI.Model.TraktSyncMoviesWatched) As TraktAPI.Model.TraktResponse
    '    If CheckConnection() Then
    '        Return TrakttvAPI.AddMoviesToWatchedHistory(tMovies)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function AddToWatchedHistoryEx_TVShows(ByVal tTVShows As TraktAPI.Model.TraktSyncShowsWatchedEx) As TraktAPI.Model.TraktResponse
    '    If CheckConnection() Then
    '        Return TrakttvAPI.AddShowsToWatchedHistoryEx(tTVShows)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function AddToWatchlist_Movies(ByVal tMovies As TraktAPI.Model.TraktSyncMovies) As TraktAPI.Model.TraktResponse
    '    If CheckConnection() Then
    '        Return TrakttvAPI.RemoveMoviesFromWatchlist(tMovies)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function Comment_AddMovie(ByVal tMovie As TraktAPI.Model.TraktCommentMovie) As TraktAPI.Model.TraktComment
    '    If CheckConnection() Then
    '        Return TrakttvAPI.AddCommentForMovie(tMovie)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function Comment_GetComments(ByVal strCommentType As String, ByVal strType As String) As IEnumerable(Of TraktAPI.Model.TraktCommentItem)
    '    If CheckConnection() Then
    '        Return TrakttvAPI.GetComments(Username, strCommentType, strType)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function Comment_Remove(ByVal iCommentID As Integer) As Boolean
    '    If CheckConnection() Then
    '        Return TrakttvAPI.RemoveCommentOrReply(iCommentID)
    '    Else
    '        Return False
    '    End If
    'End Function

    'Public Function Comment_Update(ByVal iCommentID As String, ByVal st As TraktAPI.Model.TraktCommentBase) As TraktAPI.Model.TraktComment
    '    If CheckConnection() Then
    '        Return TrakttvAPI.UpdateComment(iCommentID, st)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function GetNetworkFriends() As IEnumerable(Of TraktAPI.Model.TraktNetworkFriend)
    '    If CheckConnection() Then
    '        Return TrakttvAPI.GetNetworkFriends()
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function GetNetworkFollowing() As IEnumerable(Of TraktAPI.Model.TraktNetworkUser)
    '    If CheckConnection() Then
    '        Return TrakttvAPI.GetNetworkFollowing()
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function GetProcess_TVShow(ByVal strTraktID As String) As TraktAPI.Model.TraktShowProgress
    '    If String.IsNullOrEmpty(strTraktID) Then Return Nothing

    '    If CheckConnection() Then
    '        Return TrakttvAPI.GetProgressShow(strTraktID)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function GetRated_Movies() As IEnumerable(Of TraktAPI.Model.TraktMovieRated)
    '    If CheckConnection() Then
    '        Return TrakttvAPI.GetRatedMovies
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function GetRated_TVEpisodes() As IEnumerable(Of TraktAPI.Model.TraktEpisodeRated)
    '    If CheckConnection() Then
    '        Return TrakttvAPI.GetRatedEpisodes
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function GetWatched_Movies() As IEnumerable(Of TraktAPI.Model.TraktMovieWatched)
    '    If CheckConnection() Then
    '        Return TrakttvAPI.GetWatchedMovies
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function GetWatched_TVEpisodes() As IEnumerable(Of TraktAPI.Model.TraktEpisodeWatched)
    '    If CheckConnection() Then
    '        Return TrakttvAPI.GetWatchedEpisodes
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function GetWatchedHistory_Movies() As List(Of TraktAPI.Model.TraktMovieHistory)
    '    If CheckConnection() Then
    '        Return TrakttvAPI.GetUsersMovieWatchedHistory(Username)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function GetWatchList_Movies() As IEnumerable(Of TraktAPI.Model.TraktMovieWatchList)
    '    If CheckConnection() Then
    '        Return TrakttvAPI.GetWatchListMovies(Username)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function GetWatchedProcess_TVShows() As List(Of TraktAPI.Model.TraktShowWatchedProgress)
    '    Return GetWatchedProgress_TVShows(GetWatched_TVEpisodes())
    'End Function

    'Public Function GetWatchedProgress_TVShows(ByVal WatchedTVEpisodes As IEnumerable(Of TraktAPI.Model.TraktEpisodeWatched)) As List(Of TraktAPI.Model.TraktShowWatchedProgress)
    '    If WatchedTVEpisodes Is Nothing Then Return Nothing

    '    Dim lWatchedProgressTVShows As New List(Of TraktAPI.Model.TraktShowWatchedProgress)

    '    For Each tWatchedTVShow In WatchedTVEpisodes
    '        Dim nWatchedProgressTVShow As New TraktAPI.Model.TraktShowWatchedProgress
    '        nWatchedProgressTVShow.LastWatchedEpisode = Functions.ConvertToProperDateTime(tWatchedTVShow.WatchedAt)
    '        nWatchedProgressTVShow.EpisodePlaycount = tWatchedTVShow.Plays
    '        nWatchedProgressTVShow.ShowID = tWatchedTVShow.Show.Ids.Trakt.ToString
    '        nWatchedProgressTVShow.ShowTitle = tWatchedTVShow.Show.Title

    '        'get progress
    '        If _SpecialSettings.GetShowProgress Then
    '            Dim nProgressTVShow As TraktAPI.Model.TraktShowProgress = GetProcess_TVShow(nWatchedProgressTVShow.ShowID)
    '            If nProgressTVShow IsNot Nothing Then
    '                nWatchedProgressTVShow.EpisodesAired = nProgressTVShow.Aired
    '                nWatchedProgressTVShow.EpisodesWatched = nProgressTVShow.Completed
    '            Else
    '                nWatchedProgressTVShow.EpisodesAired = 0
    '                nWatchedProgressTVShow.EpisodesWatched = 0
    '            End If
    '        Else
    '            nWatchedProgressTVShow.EpisodesAired = 0
    '            nWatchedProgressTVShow.EpisodesWatched = 0
    '        End If

    '        lWatchedProgressTVShows.Add(nWatchedProgressTVShow)
    '    Next

    '    Return lWatchedProgressTVShows
    'End Function

    'Public Function GetWatchedRated_Movies() As List(Of TraktAPI.Model.TraktMovieWatchedRated)
    '    Return GetWatchedRated_Movies(GetWatched_Movies(), GetRated_Movies())
    'End Function

    'Public Function Rating_AddMovies(ByVal tMovies As TraktAPI.Model.TraktSyncMoviesRated) As TraktAPI.Model.TraktResponse
    '    If CheckConnection() Then
    '        Return TrakttvAPI.AddMoviesToRatings(tMovies)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function Rating_RemoveMovies(ByVal tMovies As TraktAPI.Model.TraktSyncMovies) As TraktAPI.Model.TraktResponse
    '    If CheckConnection() Then
    '        Return TrakttvAPI.RemoveMoviesFromRatings(tMovies)
    '        Return Nothing
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function RemoveFromCollection_Movie(ByVal tDBElement As Database.DBElement) As Boolean
    '    If tDBElement Is Nothing OrElse Not tDBElement.ContentType = Enums.ContentType.Movie Then Return False

    '    Dim tmpMovie As New TraktAPI.Model.TraktMovie With {.Ids = New TraktAPI.Model.TraktMovieBase}
    '    tmpMovie.Ids.Imdb = tDBElement.Movie.IMDB
    '    tmpMovie.Ids.Tmdb = If(tDBElement.Movie.TMDBSpecified, CInt(tDBElement.Movie.TMDB), Nothing)
    '    tmpMovie.Title = tDBElement.Movie.Title
    '    tmpMovie.Year = If(tDBElement.Movie.YearSpecified, CInt(tDBElement.Movie.Year), Nothing)

    '    Dim traktResponse = TrakttvAPI.RemoveMovieFromCollection(tmpMovie)
    '    If traktResponse IsNot Nothing Then
    '        If traktResponse.Deleted.Movies = 1 Then
    '            logger.Info(String.Concat("Removed Item on Trakt.tv: ", tmpMovie.Title))
    '        ElseIf traktResponse.NotFound.Movies.Count = 1 Then
    '            logger.Info(String.Concat("Item not found on Trakt.tv: ", traktResponse.NotFound.Movies.Item(0).Title, " / ", traktResponse.NotFound.Movies.Item(0).Year, " / ", traktResponse.NotFound.Movies.Item(0).Ids.Imdb))
    '        Else
    '            logger.Info(String.Concat("Item was not in your collection on Trakt.tv: ", tmpMovie.Title))
    '        End If
    '    Else
    '        logger.Error(Master.eLang.GetString(1134, "Error!"))
    '    End If
    'End Function

    'Public Function RemoveFromWatchedHistory_ByHistoryID(ByVal tHistoryID As TraktAPI.Model.TraktSyncHistoryID) As TraktAPI.Model.TraktResponse
    '    If CheckConnection() Then
    '        Return TrakttvAPI.RemoveHistoryIDFromWatchedHistory(tHistoryID)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function RemoveFromWatchedHistory_Movies(ByVal tMovies As TraktAPI.Model.TraktSyncMovies) As TraktAPI.Model.TraktResponse
    '    If CheckConnection() Then
    '        Return TrakttvAPI.RemoveMoviesFromWatchedHistory(tMovies)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function RemoveFromWatchlist_Movies(ByVal tMovies As TraktAPI.Model.TraktSyncMovies) As TraktAPI.Model.TraktResponse
    '    If CheckConnection() Then
    '        Return TrakttvAPI.RemoveMoviesFromWatchlist(tMovies)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function GetWatchedRated_Movies(ByVal WatchedMovies As IEnumerable(Of TraktAPI.Model.TraktMovieWatched), ByVal RatedMovies As IEnumerable(Of TraktAPI.Model.TraktMovieRated)) As List(Of TraktAPI.Model.TraktMovieWatchedRated)
    '    If WatchedMovies Is Nothing Then Return Nothing

    '    Dim lWatchedRatedMovies As New List(Of TraktAPI.Model.TraktMovieWatchedRated)

    '    For Each tWatchedMovie In WatchedMovies
    '        Dim nWatchedRatedMovie As New TraktAPI.Model.TraktMovieWatchedRated
    '        nWatchedRatedMovie.LastWatchedAt = Functions.ConvertToProperDateTime(tWatchedMovie.LastWatchedAt)
    '        nWatchedRatedMovie.Movie = tWatchedMovie.Movie
    '        nWatchedRatedMovie.Plays = tWatchedMovie.Plays

    '        'get rating
    '        If RatedMovies IsNot Nothing AndAlso RatedMovies.Count > 0 Then
    '            Dim nRatedMovie As TraktAPI.Model.TraktMovieRated = RatedMovies.FirstOrDefault(Function(f) (f.Movie.Ids.Imdb IsNot Nothing AndAlso f.Movie.Ids.Imdb = tWatchedMovie.Movie.Ids.Imdb) OrElse
    '                                                                                           (f.Movie.Ids.Tmdb IsNot Nothing AndAlso CInt(f.Movie.Ids.Tmdb) = CInt(tWatchedMovie.Movie.Ids.Tmdb)))
    '            If nRatedMovie IsNot Nothing Then
    '                nWatchedRatedMovie.RatedAt = Functions.ConvertToProperDateTime(nRatedMovie.RatedAt)
    '                nWatchedRatedMovie.Rating = nRatedMovie.Rating
    '            End If
    '        End If

    '        lWatchedRatedMovies.Add(nWatchedRatedMovie)
    '    Next

    '    Return lWatchedRatedMovies
    'End Function

    'Public Sub SaveWatchedStateToEmber_Movies(ByVal mywatchedmovies As IEnumerable(Of TraktAPI.Model.TraktMovieWatched), Optional ByVal sfunction As ShowProgress = Nothing)
    '    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
    '        Dim i As Integer = 0
    '        'filter watched movies at trakt.tv to movies with an Unique ID only
    '        For Each watchedMovie In mywatchedmovies.Where(Function(f) f.Movie.Ids.Imdb IsNot Nothing OrElse
    '                                                              f.Movie.Ids.Tmdb IsNot Nothing)
    '            Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
    '                Dim DateTimeLastPlayedUnix As Double = -1
    '                Try
    '                    Dim DateTimeLastPlayed As Date = Date.ParseExact(Functions.ConvertToProperDateTime(watchedMovie.LastWatchedAt), "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
    '                    DateTimeLastPlayedUnix = Functions.ConvertToUnixTimestamp(DateTimeLastPlayed)
    '                Catch ex As Exception
    '                    DateTimeLastPlayedUnix = -1
    '                End Try

    '                'build query, search only with known Unique IDs
    '                Dim UniqueIDs As New List(Of String)
    '                If watchedMovie.Movie.Ids.Imdb IsNot Nothing Then UniqueIDs.Add(String.Format("IMDB = {0}", Regex.Replace(watchedMovie.Movie.Ids.Imdb, "tt", String.Empty).Trim))
    '                If watchedMovie.Movie.Ids.Tmdb IsNot Nothing Then UniqueIDs.Add(String.Format("TMDB = {0}", watchedMovie.Movie.Ids.Tmdb))

    '                SQLCommand.CommandText = String.Format("SELECT DISTINCT idMovie FROM movie WHERE ((Playcount IS NULL OR NOT Playcount = {0}) OR (iLastPlayed IS NULL OR NOT iLastPlayed = {1})) AND ({2});", watchedMovie.Plays, DateTimeLastPlayedUnix, String.Join(" OR ", UniqueIDs.ToArray))
    '                Using SQLreader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader()
    '                    While SQLreader.Read
    '                        Dim tmpMovie As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(SQLreader("idMovie")))
    '                        tmpMovie.Movie.PlayCount = watchedMovie.Plays
    '                        tmpMovie.Movie.LastPlayed = Functions.ConvertToProperDateTime(watchedMovie.LastWatchedAt)
    '                        Master.DB.Save_Movie(tmpMovie, True, True, False, True, False)
    '                    End While
    '                End Using
    '            End Using
    '            i += 1
    '            If sfunction IsNot Nothing Then
    '                sfunction(i, watchedMovie.Movie.Title)
    '            End If
    '        Next
    '        SQLtransaction.Commit()
    '    End Using
    'End Sub

    'Public Sub SaveWatchedStateToEmber_TVEpisodes(ByVal myWatchedEpisodes As IEnumerable(Of TraktAPI.Model.TraktEpisodeWatched), Optional ByVal sfunction As ShowProgress = Nothing)
    '    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
    '        Dim i As Integer = 0
    '        'filter watched tv shows at trakt.tv to tv shows with an Unique ID only
    '        For Each watchedTVShow In myWatchedEpisodes.Where(Function(f) f.Show.Ids.Imdb IsNot Nothing OrElse
    '                                                              f.Show.Ids.Tmdb IsNot Nothing OrElse
    '                                                              f.Show.Ids.Tvdb IsNot Nothing)
    '            For Each watchedTVSeason In watchedTVShow.Seasons
    '                For Each watchedTVEpisode In watchedTVSeason.Episodes
    '                    Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
    '                        Dim DateTimeLastPlayedUnix As Double = -1
    '                        Try
    '                            Dim DateTimeLastPlayed As Date = Date.ParseExact(Functions.ConvertToProperDateTime(watchedTVEpisode.WatchedAt), "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
    '                            DateTimeLastPlayedUnix = Functions.ConvertToUnixTimestamp(DateTimeLastPlayed)
    '                        Catch ex As Exception
    '                            DateTimeLastPlayedUnix = -1
    '                        End Try

    '                        'build query, search only with known Unique IDs
    '                        Dim UniqueIDs As New List(Of String)
    '                        If watchedTVShow.Show.Ids.Tvdb IsNot Nothing Then UniqueIDs.Add(String.Format("tvshow.TVDB = {0}", watchedTVShow.Show.Ids.Tvdb))
    '                        If watchedTVShow.Show.Ids.Imdb IsNot Nothing Then UniqueIDs.Add(String.Format("tvshow.strIMDB = '{0}'", watchedTVShow.Show.Ids.Imdb))
    '                        If watchedTVShow.Show.Ids.Tmdb IsNot Nothing Then UniqueIDs.Add(String.Format("tvshow.strTMDB = {0}", watchedTVShow.Show.Ids.Tmdb))

    '                        SQLCommand.CommandText = String.Concat("SELECT DISTINCT episode.idEpisode FROM episode INNER JOIN tvshow ON (episode.idShow = tvshow.idShow) ",
    '                                                               "WHERE NOT idFile = -1 ",
    '                                                               "AND (episode.Season = ", watchedTVSeason.Number, " AND episode.Episode = ", watchedTVEpisode.Number, ") ",
    '                                                               "AND ((episode.Playcount IS NULL OR NOT episode.Playcount = ", watchedTVEpisode.Plays, ") ",
    '                                                               "OR (episode.iLastPlayed IS NULL OR NOT episode.iLastPlayed = ", DateTimeLastPlayedUnix, ")) ",
    '                                                               "AND (", String.Join(" OR ", UniqueIDs.ToArray), ");")

    '                        Using SQLreader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader()
    '                            While SQLreader.Read
    '                                Dim tmpTVEpisode As Database.DBElement = Master.DB.Load_TVEpisode(Convert.ToInt64(SQLreader("idEpisode")), True)
    '                                tmpTVEpisode.TVEpisode.Playcount = watchedTVEpisode.Plays
    '                                tmpTVEpisode.TVEpisode.LastPlayed = Functions.ConvertToProperDateTime(watchedTVEpisode.WatchedAt)
    '                                Master.DB.Save_TVEpisode(tmpTVEpisode, True, True, False, False, True)
    '                            End While
    '                        End Using
    '                    End Using
    '                Next
    '            Next
    '            i += 1
    '            If sfunction IsNot Nothing Then
    '                sfunction(i, watchedTVShow.Show.Title)
    '            End If
    '        Next
    '        SQLtransaction.Commit()
    '    End Using
    'End Sub

    'Public Function SetWatchedState_Movie(ByRef tDBElement As Database.DBElement) As Boolean
    '    If Not tDBElement.Movie.AnyUniqueIDSpecified Then Return False

    '    If CheckConnection() Then
    '        Dim strIMDBID As String = tDBElement.Movie.IMDB
    '        Dim intTMDBID As Integer = -1
    '        Integer.TryParse(tDBElement.Movie.TMDB, intTMDBID)

    '        Dim lWatchedMovies As IEnumerable(Of TraktAPI.Model.TraktMovieWatched) = GetWatched_Movies()
    '        If lWatchedMovies IsNot Nothing AndAlso lWatchedMovies.Count > 0 Then
    '            Dim tMovie = lWatchedMovies.FirstOrDefault(Function(f) (f.Movie.Ids.Imdb IsNot Nothing AndAlso f.Movie.Ids.Imdb = strIMDBID) OrElse
    '                                              (f.Movie.Ids.Tmdb IsNot Nothing AndAlso CInt(f.Movie.Ids.Tmdb) = intTMDBID))
    '            If tMovie IsNot Nothing Then
    '                tDBElement.Movie.LastPlayed = Functions.ConvertToProperDateTime(tMovie.LastWatchedAt)
    '                tDBElement.Movie.PlayCount = tMovie.Plays
    '                Return True
    '            End If
    '        End If
    '    End If

    '    Return False
    'End Function

    'Public Function SetWatchedState_TVEpisode(ByRef tDBElement As Database.DBElement) As Boolean
    '    If Not tDBElement.TVShow.AnyUniqueIDSpecified Then Return False

    '    If CheckConnection() Then
    '        Dim strIMDBID As String = tDBElement.TVShow.IMDB
    '        Dim intTMDBID As Integer = -1
    '        Dim intTVDBID As Integer = -1
    '        Integer.TryParse(tDBElement.TVShow.TMDB, intTMDBID)
    '        Integer.TryParse(tDBElement.TVShow.TVDB, intTVDBID)

    '        Dim lWatchedTVEpisodes As IEnumerable(Of TraktAPI.Model.TraktEpisodeWatched) = GetWatched_TVEpisodes()
    '        If lWatchedTVEpisodes IsNot Nothing AndAlso lWatchedTVEpisodes.Count > 0 Then
    '            Dim tTVShow = lWatchedTVEpisodes.FirstOrDefault(Function(f) (f.Show.Ids.Tvdb IsNot Nothing AndAlso CInt(f.Show.Ids.Tvdb) = intTVDBID) OrElse
    '                                                               (f.Show.Ids.Imdb IsNot Nothing AndAlso f.Show.Ids.Imdb = strIMDBID) OrElse
    '                                                               (f.Show.Ids.Tmdb IsNot Nothing AndAlso CInt(f.Show.Ids.Tmdb) = intTMDBID))
    '            If tTVShow IsNot Nothing Then
    '                Select Case tDBElement.ContentType
    '                    Case Enums.ContentType.TVEpisode
    '                        Dim intEpisode = tDBElement.TVEpisode.Episode
    '                        Dim intSeason = tDBElement.TVEpisode.Season

    '                        Dim tTVEpisode = tTVShow.Seasons.FirstOrDefault(Function(f) f.Number = intSeason).Episodes.FirstOrDefault(Function(f) f.Number = intEpisode)
    '                        If tTVEpisode IsNot Nothing Then
    '                            tDBElement.TVEpisode.LastPlayed = Functions.ConvertToProperDateTime(tTVEpisode.WatchedAt)
    '                            tDBElement.TVEpisode.Playcount = tTVEpisode.Plays
    '                            Return True
    '                        End If
    '                    Case Enums.ContentType.TVShow
    '                        For Each tEpisode As Database.DBElement In tDBElement.Episodes.Where(Function(f) f.FilenameSpecified)
    '                            Dim intEpisode = tEpisode.TVEpisode.Episode
    '                            Dim intSeason = tEpisode.TVEpisode.Season

    '                            Dim tTVEpisode = tTVShow.Seasons.FirstOrDefault(Function(f) f.Number = intSeason).Episodes.FirstOrDefault(Function(f) f.Number = intEpisode)
    '                            If tTVEpisode IsNot Nothing Then
    '                                tEpisode.TVEpisode.LastPlayed = Functions.ConvertToProperDateTime(tTVEpisode.WatchedAt)
    '                                tEpisode.TVEpisode.Playcount = tTVEpisode.Plays
    '                            End If
    '                        Next
    '                        Return True
    '                End Select
    '            End If
    '        End If
    '    End If

    '    Return False
    'End Function

    'Public Sub SyncToEmber_All(Optional ByVal sfunction As ShowProgress = Nothing)
    '    SyncToEmber_Movies(sfunction)
    '    SyncToEmber_TVEpisodes(sfunction)
    'End Sub

    'Public Sub SyncToEmber_Movies(Optional ByVal sfunction As ShowProgress = Nothing)
    '    Dim WatchedMovies = GetWatched_Movies()
    '    If WatchedMovies IsNot Nothing Then
    '        SaveWatchedStateToEmber_Movies(WatchedMovies, sfunction)
    '    End If
    'End Sub

    'Public Sub SyncToEmber_TVEpisodes(Optional ByVal sfunction As ShowProgress = Nothing)
    '    Dim WatchedTVEpisodes = GetWatched_TVEpisodes()
    '    If WatchedTVEpisodes IsNot Nothing Then
    '        SaveWatchedStateToEmber_TVEpisodes(WatchedTVEpisodes, sfunction)
    '    End If
    'End Sub

    'Public Function UserList_AddList(ByVal tList As TraktAPI.Model.TraktList) As TraktAPI.Model.TraktListDetail
    '    If CheckConnection() Then
    '        Return TrakttvAPI.AddUserList(tList, Username)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function UserList_AddItems(ByVal strSlug As String, ByVal tList As TraktAPI.Model.TraktSynchronize) As TraktAPI.Model.TraktResponse
    '    If CheckConnection() Then
    '        Return TrakttvAPI.AddItemsToList(Username, strSlug, tList)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function UserList_GetItems(ByVal strList As String) As IEnumerable(Of TraktAPI.Model.TraktListItem)
    '    If CheckConnection() Then
    '        Return TrakttvAPI.GetUserListItems(Username, strList)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function UserList_GetItems(ByVal strUsername As String, ByVal strList As String, ByVal strParam As String) As IEnumerable(Of TraktAPI.Model.TraktListItem)
    '    If CheckConnection() Then
    '        Return TrakttvAPI.GetUserListItems(strUsername, strList, strParam)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function UserList_GetLists() As IEnumerable(Of TraktAPI.Model.TraktListDetail)
    '    Return UserList_GetLists(Username)
    'End Function

    'Public Function UserList_GetLists(ByVal strUsername As String) As IEnumerable(Of TraktAPI.Model.TraktListDetail)
    '    If CheckConnection() Then
    '        Return TrakttvAPI.GetUserLists(strUsername)
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    'Public Function UserList_RemoveList(ByVal strSlug As String) As Boolean
    '    If CheckConnection() Then
    '        Return TrakttvAPI.RemoveUserList(Username, strSlug)
    '    Else
    '        Return False
    '    End If
    'End Function

#End Region 'Methods

#Region "Nested Types"

    Public Class WatchedAndRatedMovie
        Public LastWatchedAt As Date?
        Public Movie As Objects.Get.Movies.TraktMovie
        Public Plays As Integer
        Public RatedAt As Date?
        Public Rating As Integer
    End Class

    Public Class WatchedAndRatedTVEpisode
        Public LastWatchedAt As Date?
        Public Episode As Objects.Get.Shows.Episodes.TraktEpisode
        Public Plays As Integer
        Public RatedAt As Date?
        Public Rating As Integer
    End Class

    Public Class WatchedAndRatedTVShow
        Public LastWatchedAt As Date?
        Public Seasons As IEnumerable(Of Objects.Get.Shows.Seasons.TraktSeason)
        Public Show As Objects.Get.Shows.TraktShow
        Public Plays As Integer
        Public RatedAt As Date?
        Public Rating As Integer
        Public WatchedEpisodes As Integer
    End Class

#End Region 'Nested Types

End Class
