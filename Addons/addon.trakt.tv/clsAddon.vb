﻿' ################################################################################
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
Imports System.Threading.Tasks

Public Class Scraper

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _Client As TraktClient

#End Region 'Fields

#Region "Properties"

    ReadOnly Property AccessToken() As String
        Get
            Return _Client.Authorization.AccessToken
        End Get
    End Property

    ReadOnly Property Created() As Date
        Get
            Return _Client.Authorization.Created
        End Get
    End Property

    ReadOnly Property ExpiresInSeconds() As Integer
        Get
            Return _Client.Authorization.ExpiresInSeconds
        End Get
    End Property

    ReadOnly Property RefreshToken() As String
        Get
            Return _Client.Authorization.RefreshToken
        End Get
    End Property


#End Region 'Properties

#Region "Delegates"

    Public Delegate Function ShowProgress(ByVal iProgress As Integer, ByVal strMessage As String) As Boolean

#End Region 'Delegates

#Region "Events"

    Public Event NewTokenCreated()

#End Region 'Events 

#Region "Methods"

    Public Async Function CheckConnection() As Task(Of Boolean)
        Dim bGetError As Boolean
        Try
            Dim authorizationcheck = Await Task.Run(Function() _Client.Authentication.CheckIfAuthorizationIsExpiredOrWasRevokedAsync(True))
            If authorizationcheck.First AndAlso authorizationcheck.Second IsNot Nothing Then
                _Client.Authorization = authorizationcheck.Second
                RaiseEvent NewTokenCreated()
            ElseIf authorizationcheck.First OrElse _Client.Authorization.IsExpired OrElse Not _Client.Authorization.IsValid Then
                Await CreateAPI(New AddonSettings, _Client.ClientId, _Client.ClientSecret)
            End If
        Catch ex As Exception
            bGetError = True
        End Try
        If bGetError Then
            Await CreateAPI(New AddonSettings, _Client.ClientId, _Client.ClientSecret)
        End If
        Return Not _Client.Authorization.IsExpired AndAlso _Client.Authorization.IsValid
    End Function

    Public Async Function CreateAPI(ByVal tAddonSettings As AddonSettings, ByVal clientId As String, ByVal clientSecret As String) As Task
        'Default lifetime of an AccessToken is 90 days. 
        'So we set the default CreatedAt age to 91 days to get shure that the default value Is to old And a New AccessToken has to be created.
        Dim dCreatedAt As Date = Date.Today.AddDays(-91)
        Dim iCreatedAt As Long = 0
        Dim iExpiresIn As Integer = 0

        Integer.TryParse(tAddonSettings.APIExpiresInSeconds, iExpiresIn)
        If Long.TryParse(tAddonSettings.APICreated, iCreatedAt) Then
            dCreatedAt = Functions.ConvertFromUnixTimestamp(iCreatedAt)
        End If

        _Client = New TraktClient(clientId, clientSecret) With {
            .Authorization = Authentication.TraktAuthorization.CreateWith(dCreatedAt, iExpiresIn, tAddonSettings.APIAccessToken, tAddonSettings.APIRefreshToken)
        }

        If Not _Client.IsValidForUseWithAuthorization Then
            If _Client.Authorization.IsRefreshPossible Then
                Await RefreshAuthorization()
            End If
        End If

        If Not _Client.IsValidForUseWithAuthorization Then
            Try
                Dim strActivationURL = _Client.OAuth.CreateAuthorizationUrl()
                Using dAuthorize As New frmAuthorize
                    If dAuthorize.ShowDialog(strActivationURL) = DialogResult.OK Then
                        Await _Client.OAuth.GetAuthorizationAsync(dAuthorize.Result)
                        If _Client.IsValidForUseWithAuthorization Then
                            RaiseEvent NewTokenCreated()
                        End If
                    End If
                End Using
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
    End Function

    Public Async Function RefreshAuthorization() As Task(Of Boolean)
        If _Client.Authorization.IsRefreshPossible Then
            Dim bGetError As Boolean
            Try
                Dim authorization = Task.Run(Function() _Client.OAuth.RefreshAuthorizationAsync)
                If authorization.Result.IsValid Then
                    _Client.Authorization = authorization.Result
                    RaiseEvent NewTokenCreated()
                    Return True
                End If
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
                bGetError = True
                _Client.Authorization = New Authentication.TraktAuthorization
            End Try
            If bGetError Then
                Await CreateAPI(New AddonSettings, _Client.ClientId, _Client.ClientSecret)
            End If
        Else
            _Client.Authorization = New Authentication.TraktAuthorization
            Await CreateAPI(New AddonSettings, String.Empty, String.Empty)
        End If
        Return False
    End Function

    Public Async Function GetID_Trakt(ByVal tDBElement As Database.DBElement, Optional bForceTVShowID As Boolean = False) As Task(Of UInteger)
        Dim nSearchResults As Objects.Basic.TraktPaginationListResult(Of Objects.Basic.TraktSearchResult) = Nothing
        Dim nContentType As Enums.ContentType = If(bForceTVShowID, Enums.ContentType.TVShow, tDBElement.ContentType)

        If _Client IsNot Nothing Then
            Select Case nContentType
                Case Enums.ContentType.Movie
                    'search by IMDB ID
                    If tDBElement.MainDetails.UniqueIDs.IMDbIdSpecified Then
                        nSearchResults = Await _Client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.ImDB, tDBElement.MainDetails.UniqueIDs.IMDbId, TraktApiSharp.Enums.TraktSearchResultType.Movie)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    'search by TMDB ID
                    If (nSearchResults Is Nothing OrElse nSearchResults.ItemCount = 0) AndAlso tDBElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                        nSearchResults = Await _Client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TmDB, tDBElement.MainDetails.UniqueIDs.TMDbId.ToString, TraktApiSharp.Enums.TraktSearchResultType.Movie)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    If nSearchResults IsNot Nothing AndAlso nSearchResults.ItemCount = 1 AndAlso nSearchResults(0).Movie IsNot Nothing Then
                        Return nSearchResults(0).Movie.Ids.Trakt
                    Else
                        _Logger.Info(String.Format("[GetID_Trakt] Could not scrape TraktID from trakt.tv! IMDB: {0} / TMDB: {1}", tDBElement.MainDetails.UniqueIDs.IMDbId, tDBElement.MainDetails.UniqueIDs.TMDbId))
                    End If
                Case Enums.ContentType.TVEpisode
                    'search by TVDB ID
                    If tDBElement.MainDetails.UniqueIDs.TVDbIdSpecified Then
                        nSearchResults = Await _Client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TvDB, tDBElement.MainDetails.UniqueIDs.TVDbId.ToString, TraktApiSharp.Enums.TraktSearchResultType.Episode)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    'search by IMDB ID
                    If (nSearchResults Is Nothing OrElse nSearchResults.ItemCount = 0) AndAlso tDBElement.MainDetails.UniqueIDs.IMDbIdSpecified Then
                        nSearchResults = Await _Client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.ImDB, tDBElement.MainDetails.UniqueIDs.IMDbId, TraktApiSharp.Enums.TraktSearchResultType.Episode)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    'search by TMDB ID
                    If (nSearchResults Is Nothing OrElse nSearchResults.ItemCount = 0) AndAlso tDBElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                        nSearchResults = Await _Client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TmDB, tDBElement.MainDetails.UniqueIDs.TMDbId.ToString, TraktApiSharp.Enums.TraktSearchResultType.Episode)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    If nSearchResults IsNot Nothing AndAlso nSearchResults.ItemCount = 1 AndAlso nSearchResults(0).Episode IsNot Nothing Then
                        Return nSearchResults(0).Episode.Ids.Trakt
                    Else
                        _Logger.Info(String.Format("[GetID_Trakt] Could not scrape TraktID from trakt.tv! TVDB: {0} / IMDB: {1} / TMDB: {2}", tDBElement.MainDetails.UniqueIDs.TVDbId, tDBElement.MainDetails.UniqueIDs.IMDbId, tDBElement.MainDetails.UniqueIDs.TMDbId))
                    End If
                Case Enums.ContentType.TVShow
                    'search by TVDB ID
                    If tDBElement.MainDetails.UniqueIDs.TVDbIdSpecified Then
                        nSearchResults = Await _Client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TvDB, tDBElement.MainDetails.UniqueIDs.TVDbId.ToString, TraktApiSharp.Enums.TraktSearchResultType.Show)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    'search by IMDB ID
                    If (nSearchResults Is Nothing OrElse nSearchResults.ItemCount = 0) AndAlso tDBElement.MainDetails.UniqueIDs.IMDbIdSpecified Then
                        nSearchResults = Await _Client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.ImDB, tDBElement.MainDetails.UniqueIDs.IMDbId, TraktApiSharp.Enums.TraktSearchResultType.Show)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    'search by TMDB ID
                    If (nSearchResults Is Nothing OrElse nSearchResults.ItemCount = 0) AndAlso tDBElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                        nSearchResults = Await _Client.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TmDB, tDBElement.MainDetails.UniqueIDs.TMDbId.ToString, TraktApiSharp.Enums.TraktSearchResultType.Show)
                        'If nSearchResults.Exception IsNot Nothing Then Return 0
                    End If
                    If nSearchResults IsNot Nothing AndAlso nSearchResults.ItemCount = 1 AndAlso nSearchResults(0).Show IsNot Nothing Then
                        Return nSearchResults(0).Show.Ids.Trakt
                    Else
                        _Logger.Info(String.Format("[GetID_Trakt] Could not scrape TraktID from trakt.tv! TVDB: {0} / IMDB: {1} / TMDB: {2}", tDBElement.MainDetails.UniqueIDs.TVDbId, tDBElement.MainDetails.UniqueIDs.IMDbId, tDBElement.MainDetails.UniqueIDs.TMDbId))
                    End If
            End Select
        End If

        Return 0
    End Function

    Public Async Function GetInfo_Movie(ByVal uintTraktID As Task(Of UInteger), ByVal tFilteredOptions As Structures.ScrapeOptions) As Task(Of MediaContainers.MainDetails)
        If uintTraktID.Result = 0 Then Return Nothing

        Dim nMovie As New MediaContainers.MainDetails With {.Scrapersource = "Trakt.tv"}

        If _Client IsNot Nothing Then
            'Rating
            If tFilteredOptions.Ratings Then
                Dim nGlobalRating = Await _Client.Movies.GetMovieRatingsAsync(CStr(uintTraktID.Result))
                Dim dblRating As Double
                Dim iVotes As Integer
                If nGlobalRating IsNot Nothing AndAlso
                    nGlobalRating.Rating IsNot Nothing AndAlso Double.TryParse(nGlobalRating.Rating.Value.ToString, dblRating) AndAlso
                    nGlobalRating.Votes IsNot Nothing AndAlso Integer.TryParse(nGlobalRating.Votes.Value.ToString, iVotes) Then
                    nMovie.Ratings.Add(New MediaContainers.RatingDetails With {
                                       .Max = 10,
                                       .Name = "trakt",
                                       .Value = Math.Round(dblRating, 1),
                                       .Votes = iVotes})
                End If
            End If

            'UserRating
            If tFilteredOptions.UserRating Then
                Dim bCheck = CheckConnection()
                If bCheck.Result Then
                    Dim nPersonalRatedMovies = Await _Client.Sync.GetRatingsAsync(TraktApiSharp.Enums.TraktRatingsItemType.Movie)
                    If nPersonalRatedMovies IsNot Nothing AndAlso nPersonalRatedMovies.Count > 0 Then
                        Dim tMovie = nPersonalRatedMovies.FirstOrDefault(Function(f) f.Movie.Ids.Trakt = uintTraktID.Result)
                        If tMovie IsNot Nothing Then
                            nMovie.UserRating = CInt(tMovie.Rating)
                        End If
                    End If
                End If
            End If
        End If

        Return nMovie
    End Function

    Public Async Function GetInfo_TVEpisode(ByVal uintTVShowTraktID As Task(Of UInteger),
                                            ByVal intSeason As Integer,
                                            ByVal intEpisode As Integer,
                                            ByVal tFilteredOptions As Structures.ScrapeOptions,
                                            Optional ByVal tPersonalRatedTVEpisodes As IEnumerable(Of Objects.Get.Ratings.TraktRatingsItem) = Nothing) As Task(Of MediaContainers.MainDetails)
        If uintTVShowTraktID.Result = 0 Then Return Nothing

        Dim uintSeason As Integer
        Dim uintEpisode As Integer

        If Not Integer.TryParse(intEpisode.ToString, uintEpisode) OrElse Not Integer.TryParse(intSeason.ToString, uintSeason) Then
            Return Nothing
        End If

        Dim nTVEpisode As New MediaContainers.MainDetails With {.Scrapersource = "Trakt.tv", .Episode = intEpisode, .Season = intSeason}

        If _Client IsNot Nothing Then
            'Rating
            If tFilteredOptions.Episodes.Ratings Then
                Dim nGlobalRating = Await _Client.Episodes.GetEpisodeRatingsAsync(CStr(uintTVShowTraktID.Result), uintSeason, uintEpisode)
                Dim dblRating As Double
                Dim iVotes As Integer
                If nGlobalRating IsNot Nothing AndAlso
                    nGlobalRating.Rating IsNot Nothing AndAlso Double.TryParse(nGlobalRating.Rating.Value.ToString, dblRating) AndAlso
                    nGlobalRating.Votes IsNot Nothing AndAlso Integer.TryParse(nGlobalRating.Votes.Value.ToString, iVotes) Then
                    nTVEpisode.Ratings.Add(New MediaContainers.RatingDetails With {
                                           .Max = 10,
                                           .Name = "trakt",
                                           .Value = Math.Round(dblRating, 1),
                                           .Votes = iVotes})
                End If
            End If

            'UserRating
            If tFilteredOptions.Episodes.UserRating Then
                Dim nPersonalRatedTVEpisodes = tPersonalRatedTVEpisodes
                If nPersonalRatedTVEpisodes Is Nothing Then
                    Dim bCheck = CheckConnection()
                    If bCheck.Result Then
                        nPersonalRatedTVEpisodes = Await _Client.Sync.GetRatingsAsync(TraktApiSharp.Enums.TraktRatingsItemType.Episode)
                    End If
                End If
                If nPersonalRatedTVEpisodes IsNot Nothing AndAlso nPersonalRatedTVEpisodes.Count > 0 Then
                    Dim tTVEpisode = nPersonalRatedTVEpisodes.FirstOrDefault(Function(f) f.Show.Ids.Trakt = uintTVShowTraktID.Result AndAlso
                                                                                 f.Episode.Number IsNot Nothing AndAlso
                                                                                 CInt(f.Episode.Number) = intEpisode AndAlso
                                                                                 f.Episode.SeasonNumber IsNot Nothing AndAlso
                                                                                 CInt(f.Episode.SeasonNumber) = intSeason)
                    If tTVEpisode IsNot Nothing Then
                        nTVEpisode.UserRating = CInt(tTVEpisode.Rating)
                    End If
                End If
            End If
        End If

        Return nTVEpisode
    End Function

    Public Async Function GetInfo_TVShow(ByVal uintTraktID As Task(Of UInteger), ByVal FilteredOptions As Structures.ScrapeOptions, ByVal tScrapeModifiers As Structures.ScrapeModifiers, ByVal lstEpisodes As List(Of Database.DBElement)) As Task(Of MediaContainers.MainDetails)
        If uintTraktID.Result = 0 Then Return Nothing

        Dim nTVShow As New MediaContainers.MainDetails With {.Scrapersource = "Trakt.tv"}

        If _Client IsNot Nothing Then
            'Rating
            If FilteredOptions.Ratings Then
                Dim nGlobalRating = Await _Client.Shows.GetShowRatingsAsync(CStr(uintTraktID.Result))
                Dim dblRating As Double
                Dim iVotes As Integer
                If nGlobalRating IsNot Nothing AndAlso
                    nGlobalRating.Rating IsNot Nothing AndAlso Double.TryParse(nGlobalRating.Rating.Value.ToString, dblRating) AndAlso
                    nGlobalRating.Votes IsNot Nothing AndAlso Integer.TryParse(nGlobalRating.Votes.Value.ToString, iVotes) Then
                    nTVShow.Ratings.Add(New MediaContainers.RatingDetails With {
                                         .Max = 10,
                                         .Name = "trakt",
                                         .Value = Math.Round(dblRating, 1),
                                         .Votes = iVotes})
                End If
            End If

            'UserRating
            If FilteredOptions.UserRating Then
                Dim bCheck = CheckConnection()
                If bCheck.Result Then
                    Dim nPersonalRatedTVShows = Await _Client.Sync.GetRatingsAsync(TraktApiSharp.Enums.TraktRatingsItemType.Show)
                    If nPersonalRatedTVShows IsNot Nothing AndAlso nPersonalRatedTVShows.Count > 0 Then
                        Dim tTVShow = nPersonalRatedTVShows.FirstOrDefault(Function(f) f.Show.Ids.Trakt = uintTraktID.Result)
                        If tTVShow IsNot Nothing Then
                            nTVShow.UserRating = CInt(tTVShow.Rating)
                        End If
                    End If
                End If
            End If

            'Episodes
            If tScrapeModifiers.withEpisodes AndAlso lstEpisodes.Count > 0 AndAlso (FilteredOptions.Episodes.Ratings OrElse FilteredOptions.Episodes.UserRating) Then
                'looks like there is no way to get all episodes for a tv show. so we scrape only local existing episodes
                'reduce the API call and get all rated episodes before
                Dim nPersonalRatedTVEpisodes As IEnumerable(Of Objects.Get.Ratings.TraktRatingsItem) = Nothing
                If FilteredOptions.Episodes.UserRating Then
                    Dim bCheck = CheckConnection()
                    If bCheck.Result Then
                        nPersonalRatedTVEpisodes = Await _Client.Sync.GetRatingsAsync(TraktApiSharp.Enums.TraktRatingsItemType.Episode)
                    End If
                End If
                For Each nDBElement As Database.DBElement In lstEpisodes
                    Try
                        Dim nEpisode As MediaContainers.MainDetails = Await GetInfo_TVEpisode(uintTraktID, nDBElement.MainDetails.Season, nDBElement.MainDetails.Episode, FilteredOptions, nPersonalRatedTVEpisodes)
                        If nEpisode IsNot Nothing Then
                            nTVShow.KnownEpisodes.Add(nEpisode)
                        End If
                    Catch ex As Exception
                        _Logger.Info(String.Format("[TrakttvScraper] [GetInfo_TVShow] S{0}E{1}: {2}", nDBElement.MainDetails.Season, nDBElement.MainDetails.Episode, ex.Message))
                    End Try
                Next
            End If
        End If

        Return nTVShow
    End Function

    Public Function GetWatched_Movies() As IEnumerable(Of Objects.Get.Watched.TraktWatchedMovie)
        Dim bCheck = CheckConnection()
        If bCheck.Result Then
            Dim APIResult = Task.Run(Function() _Client.Sync.GetWatchedMoviesAsync())
            If APIResult.Exception Is Nothing Then
                Return APIResult.Result
            End If
        End If
        Return Nothing
    End Function

    Public Function GetWatched_TVShows(Optional ByVal bGetFullInformation As Boolean = False) As IEnumerable(Of Objects.Get.Watched.TraktWatchedShow)
        Dim bCheck = CheckConnection()
        If bCheck.Result Then
            Dim nOptions As Requests.Params.TraktExtendedInfo = Nothing
            If bGetFullInformation Then
                nOptions = New Requests.Params.TraktExtendedInfo With {.Full = True}
            End If
            Dim APIResult = Task.Run(Function() _Client.Sync.GetWatchedShowsAsync(nOptions))
            If APIResult.Exception Is Nothing Then
                Return APIResult.Result
            End If
        End If
        Return Nothing
    End Function

    Public Function GetWatchedState_Movie(ByRef tDBElement As Database.DBElement,
                                          Optional ByRef watchedmovies As IEnumerable(Of Objects.Get.Watched.TraktWatchedMovie) = Nothing) As Boolean
        If Not tDBElement.MainDetails.UniqueIDsSpecified Then Return False

        Dim strIMDBID As String = tDBElement.MainDetails.UniqueIDs.IMDbId
        Dim intTMDBID As Integer = tDBElement.MainDetails.UniqueIDs.TMDbId

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
                If Not tDBElement.MainDetails.LastPlayed = strLastPlayed OrElse Not tDBElement.MainDetails.PlayCount = iPlayCount Then
                    tDBElement.MainDetails.LastPlayed = strLastPlayed
                    tDBElement.MainDetails.PlayCount = iPlayCount
                    Return True
                End If
            End If
        End If

        Return False
    End Function

    Public Function GetWatchedState_TVEpisode(ByRef tDBElement As Database.DBElement,
                                              Optional ByRef watchedshows As IEnumerable(Of Objects.Get.Watched.TraktWatchedShow) = Nothing) As Boolean
        If tDBElement.TVShowDetails Is Nothing OrElse Not tDBElement.TVShowDetails.UniqueIDsSpecified Then Return False

        Dim strShowIMDBID As String = tDBElement.TVShowDetails.UniqueIDs.IMDbId
        Dim intShowTMDBID As Integer = tDBElement.TVShowDetails.UniqueIDs.TMDbId
        Dim intShowTVDBID As Integer = tDBElement.TVShowDetails.UniqueIDs.TVDbId

        Dim lstWatchedShows As IEnumerable(Of Objects.Get.Watched.TraktWatchedShow)
        If watchedshows Is Nothing Then
            lstWatchedShows = GetWatched_TVShows()
        Else
            lstWatchedShows = watchedshows
        End If

        If lstWatchedShows IsNot Nothing AndAlso lstWatchedShows.Count > 0 Then
            'search tv show
            Dim nTVShow = lstWatchedShows.FirstOrDefault(Function(f) (f.Show.Ids.Tvdb IsNot Nothing AndAlso CInt(f.Show.Ids.Tvdb) = intShowTVDBID) OrElse
                                                             (f.Show.Ids.Imdb IsNot Nothing AndAlso f.Show.Ids.Imdb = strShowIMDBID) OrElse
                                                             (f.Show.Ids.Tmdb IsNot Nothing AndAlso CInt(f.Show.Ids.Tmdb) = intShowTMDBID))
            If nTVShow IsNot Nothing Then
                Select Case tDBElement.ContentType
                    Case Enums.ContentType.TVEpisode
                        Dim intEpisode = tDBElement.MainDetails.Episode
                        Dim intSeason = tDBElement.MainDetails.Season

                        Dim nWatchedSeason = nTVShow.Seasons.Where(Function(f) CInt(f.Number) = intSeason).FirstOrDefault
                        If nWatchedSeason IsNot Nothing Then
                            Dim nWatchedEpisode = nWatchedSeason.Episodes.FirstOrDefault(Function(f) CInt(f.Number) = intEpisode)
                            If nWatchedEpisode IsNot Nothing Then
                                Dim strLastPlayed = Functions.ConvertToProperDateTime(nWatchedEpisode.LastWatchedAt.Value.ToLocalTime.ToString)
                                Dim iPlayCount = nWatchedEpisode.Plays.Value
                                If Not tDBElement.MainDetails.LastPlayed = strLastPlayed OrElse Not tDBElement.MainDetails.PlayCount = iPlayCount Then
                                    tDBElement.MainDetails.LastPlayed = strLastPlayed
                                    tDBElement.MainDetails.PlayCount = iPlayCount
                                    Return True
                                End If
                            End If
                        End If
                    Case Enums.ContentType.TVShow
                        For Each nTVEpisode In tDBElement.Episodes.Where(Function(f) f.FileItemSpecified)
                            Dim intEpisode = nTVEpisode.MainDetails.Episode
                            Dim intSeason = nTVEpisode.MainDetails.Season

                            Dim nWatchedSeason = nTVShow.Seasons.Where(Function(f) CInt(f.Number) = intSeason).FirstOrDefault
                            If nWatchedSeason IsNot Nothing Then
                                Dim nWatchedEpisode = nWatchedSeason.Episodes.FirstOrDefault(Function(f) CInt(f.Number) = intEpisode)
                                If nWatchedEpisode IsNot Nothing Then
                                    Dim strLastPlayed = Functions.ConvertToProperDateTime(nWatchedEpisode.LastWatchedAt.Value.ToLocalTime.ToString)
                                    Dim iPlayCount = nWatchedEpisode.Plays.Value
                                    If Not nTVEpisode.MainDetails.LastPlayed = strLastPlayed OrElse Not nTVEpisode.MainDetails.PlayCount = iPlayCount Then
                                        nTVEpisode.MainDetails.LastPlayed = strLastPlayed
                                        nTVEpisode.MainDetails.PlayCount = iPlayCount
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