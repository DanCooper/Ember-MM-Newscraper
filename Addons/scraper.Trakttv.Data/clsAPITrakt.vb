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
Imports System.Threading.Tasks

Public Class clsAPITrakt

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()
    Private _client As TraktClient

#End Region 'Fields

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

    Public Async Function CheckConnection() As Task(Of Boolean)
        Dim bGetError As Boolean
        Try
            Dim authorizationcheck = Await Task.Run(Function() _client.Authentication.CheckIfAuthorizationIsExpiredOrWasRevokedAsync(True))
            If authorizationcheck.First AndAlso authorizationcheck.Second IsNot Nothing Then
                _client.Authorization = authorizationcheck.Second
                RaiseEvent NewTokenCreated()
            ElseIf authorizationcheck.First OrElse _client.Authorization.IsExpired OrElse Not _client.Authorization.IsValid Then
                Await CreateAPI(New Addon.AddonSettings, _client.ClientId, _client.ClientSecret)
            End If
        Catch ex As Exception
            bGetError = True
        End Try
        If bGetError Then
            Await CreateAPI(New Addon.AddonSettings, _client.ClientId, _client.ClientSecret)
        End If
        Return Not _client.Authorization.IsExpired AndAlso _client.Authorization.IsValid
    End Function

    Public Async Function CreateAPI(ByVal tAddonSettings As Addon.AddonSettings, ByVal clientId As String, ByVal clientSecret As String) As Task
        'Default lifetime of an AccessToken is 90 days. So we set the default CreatedAt age to 91 days to get shure that the default value is to old and a new AccessToken has to be created.
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
                Await RefreshAuthorization()
            End If
        End If

        If Not _client.IsValidForUseWithAuthorization Then
            Try
                Dim strActivationURL = _client.OAuth.CreateAuthorizationUrl()
                Using dAuthorize As New frmAuthorize
                    If dAuthorize.ShowDialog(strActivationURL) = DialogResult.OK Then
                        Await _client.OAuth.GetAuthorizationAsync(dAuthorize.Result)
                        If _client.IsValidForUseWithAuthorization Then
                            RaiseEvent NewTokenCreated()
                        End If
                    End If
                End Using
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
    End Function

    Public Async Function RefreshAuthorization() As Task(Of Boolean)
        If _client.Authorization.IsRefreshPossible Then
            Dim bGetError As Boolean
            Try
                Dim authorization = Task.Run(Function() _client.OAuth.RefreshAuthorizationAsync)
                If authorization.Result.IsValid Then
                    _client.Authorization = authorization.Result
                    RaiseEvent NewTokenCreated()
                    Return True
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                bGetError = True
                _client.Authorization = New Authentication.TraktAuthorization
            End Try
            If bGetError Then
                Await CreateAPI(New Addon.AddonSettings, _client.ClientId, _client.ClientSecret)
            End If
        Else
            _client.Authorization = New Authentication.TraktAuthorization
            Await CreateAPI(New Addon.AddonSettings, String.Empty, String.Empty)
        End If
        Return False
    End Function

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
                        logger.Info(String.Format("[GetID_Trakt] Could not scrape TraktID from trakt.tv! IMDB: {0} / TMDB: {1}", tDBElement.Movie.UniqueIDs.IMDbId, tDBElement.Movie.UniqueIDs.TMDbId.ToString))
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

    Public Async Function GetInfo_Movie(ByVal uintTraktID As Task(Of UInteger), ByVal tFilteredOptions As Structures.ScrapeOptions) As Task(Of MediaContainers.Movie)
        If uintTraktID.Result = 0 Then Return Nothing

        Dim nMovie As New MediaContainers.Movie With {.Scrapersource = "Trakt.tv"}

        If _client IsNot Nothing Then
            'Rating
            If tFilteredOptions.bMainRating Then
                Dim nGlobalRating = Await _client.Movies.GetMovieRatingsAsync(CStr(uintTraktID.Result))
                If nGlobalRating IsNot Nothing AndAlso nGlobalRating.Rating IsNot Nothing AndAlso nGlobalRating.Votes IsNot Nothing Then
                    nMovie.Ratings.Add(New MediaContainers.RatingDetails With {
                                       .Max = 10,
                                       .Type = "trakt",
                                       .Value = Math.Round(nGlobalRating.Rating.Value, 1),
                                       .Votes = CInt(nGlobalRating.Votes)
                                       })
                End If
            End If

            'UserRating
            If tFilteredOptions.bMainUserRating Then
                Dim bCheck = CheckConnection()
                If bCheck.Result Then
                    Dim nPersonalRatedMovies = Await _client.Sync.GetRatingsAsync(TraktApiSharp.Enums.TraktRatingsItemType.Movie)
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
                                            Optional ByVal tPersonalRatedTVEpisodes As IEnumerable(Of Objects.Get.Ratings.TraktRatingsItem) = Nothing) As Task(Of MediaContainers.EpisodeDetails)
        If uintTVShowTraktID.Result = 0 Then Return Nothing

        Dim uintSeason As Integer
        Dim uintEpisode As Integer

        If Not Integer.TryParse(intEpisode.ToString, uintEpisode) OrElse Not Integer.TryParse(intSeason.ToString, uintSeason) Then
            Return Nothing
        End If

        Dim nTVEpisode As New MediaContainers.EpisodeDetails With {.Scrapersource = "Trakt.tv", .Episode = intEpisode, .Season = intSeason}

        If _client IsNot Nothing Then
            'Rating
            If tFilteredOptions.bEpisodeRating Then
                Dim nGlobalRating = Await _client.Episodes.GetEpisodeRatingsAsync(CStr(uintTVShowTraktID.Result), uintSeason, uintEpisode)
                If nGlobalRating IsNot Nothing AndAlso nGlobalRating.Rating IsNot Nothing AndAlso nGlobalRating.Votes IsNot Nothing Then
                    nTVEpisode.Ratings.Add(New MediaContainers.RatingDetails With {
                                           .Max = 10,
                                           .Type = "trakt",
                                           .Value = CDbl(Math.Round(nGlobalRating.Rating.Value, 1)),
                                           .Votes = CInt(nGlobalRating.Votes)
                                           })
                End If
            End If

            'UserRating
            If tFilteredOptions.bEpisodeUserRating Then
                Dim nPersonalRatedTVEpisodes = tPersonalRatedTVEpisodes
                If nPersonalRatedTVEpisodes Is Nothing Then
                    Dim bCheck = CheckConnection()
                    If bCheck.Result Then
                        nPersonalRatedTVEpisodes = Await _client.Sync.GetRatingsAsync(TraktApiSharp.Enums.TraktRatingsItemType.Episode)
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

    Public Async Function GetInfo_TVShow(ByVal uintTraktID As Task(Of UInteger), ByVal FilteredOptions As Structures.ScrapeOptions, ByVal tScrapeModifiers As Structures.ScrapeModifiers, ByVal lstEpisodes As List(Of Database.DBElement)) As Task(Of MediaContainers.TVShow)
        If uintTraktID.Result = 0 Then Return Nothing

        Dim nTVShow As New MediaContainers.TVShow With {.Scrapersource = "Trakt.tv"}

        If _client IsNot Nothing Then
            'Rating
            If FilteredOptions.bMainRating Then
                Dim nGlobalRating = Await _client.Shows.GetShowRatingsAsync(CStr(uintTraktID.Result))
                If nGlobalRating IsNot Nothing AndAlso nGlobalRating.Rating IsNot Nothing AndAlso nGlobalRating.Votes IsNot Nothing Then
                    nTVShow.Ratings.Add(New MediaContainers.RatingDetails With {
                                        .Max = 10,
                                        .Type = "trakt",
                                        .Value = CDbl(Math.Round(nGlobalRating.Rating.Value, 1)),
                                        .Votes = CInt(nGlobalRating.Votes)
                                        })
                End If
            End If

            'UserRating
            If FilteredOptions.bMainUserRating Then
                Dim bCheck = CheckConnection()
                If bCheck.Result Then
                    Dim nPersonalRatedTVShows = Await _client.Sync.GetRatingsAsync(TraktApiSharp.Enums.TraktRatingsItemType.Show)
                    If nPersonalRatedTVShows IsNot Nothing AndAlso nPersonalRatedTVShows.Count > 0 Then
                        Dim tTVShow = nPersonalRatedTVShows.FirstOrDefault(Function(f) f.Show.Ids.Trakt = uintTraktID.Result)
                        If tTVShow IsNot Nothing Then
                            nTVShow.UserRating = CInt(tTVShow.Rating)
                        End If
                    End If
                End If
            End If

            'Episodes
            If tScrapeModifiers.withEpisodes AndAlso lstEpisodes.Count > 0 AndAlso (FilteredOptions.bEpisodeRating OrElse FilteredOptions.bEpisodeUserRating) Then
                'looks like there is no way to get all episodes for a tv show. so we scrape only local existing episodes
                'reduce the API call and get all rated episodes before
                Dim nPersonalRatedTVEpisodes As IEnumerable(Of Objects.Get.Ratings.TraktRatingsItem) = Nothing
                If FilteredOptions.bEpisodeUserRating Then
                    Dim bCheck = CheckConnection()
                    If bCheck.Result Then
                        nPersonalRatedTVEpisodes = Await _client.Sync.GetRatingsAsync(TraktApiSharp.Enums.TraktRatingsItemType.Episode)
                    End If
                End If
                For Each nDBElement As Database.DBElement In lstEpisodes
                    Try
                        Dim nEpisode As MediaContainers.EpisodeDetails = Await GetInfo_TVEpisode(uintTraktID, nDBElement.TVEpisode.Season, nDBElement.TVEpisode.Episode, FilteredOptions, nPersonalRatedTVEpisodes)
                        If nEpisode IsNot Nothing Then
                            nTVShow.KnownEpisodes.Add(nEpisode)
                        End If
                    Catch ex As Exception
                        logger.Info(String.Format("[TrakttvScraper] [GetInfo_TVShow] S{0}E{1}: {2}", nDBElement.TVEpisode.Season, nDBElement.TVEpisode.Episode, ex.Message))
                    End Try
                Next
            End If
        End If

        Return nTVShow
    End Function

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class
