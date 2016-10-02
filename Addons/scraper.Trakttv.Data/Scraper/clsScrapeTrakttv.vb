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

Imports System.Threading.Tasks
Imports EmberAPI
Imports NLog
Imports TraktApiSharp

Namespace TrakttvScraper

    Public Class Scraper

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _apiTrakt As New TraktClient("80a5418f493f058bc6fdfdc6d0a154731dea3fc628241e3dee29846c59f5d0f0", "e097b8c0b24ffddffb165b260166f9d7f7cd8e1617964bb51b393478772728e5")
        Private _SpecialSettings As Trakttv_Data.SpecialSettings
        Private _newTokenCreated As Boolean

#End Region 'Fields

#Region "Properties"

        ReadOnly Property AccessToken() As String
            Get
                Return _SpecialSettings.AccessToken
            End Get
        End Property

        ReadOnly Property NewTokenCreated() As Boolean
            Get
                Return _newTokenCreated
            End Get
        End Property

        ReadOnly Property RefreshToken() As String
            Get
                Return _SpecialSettings.RefreshToken
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub New(ByRef tSpecialSettings As Trakttv_Data.SpecialSettings)
            _SpecialSettings = tSpecialSettings
            Try
                CreateAPI()
                tSpecialSettings.AccessToken = _SpecialSettings.AccessToken
                tSpecialSettings.CreatedAt = _SpecialSettings.CreatedAt
                tSpecialSettings.ExpiresIn = _SpecialSettings.ExpiresIn
                tSpecialSettings.RefreshToken = _SpecialSettings.RefreshToken
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

        Public Function CheckConnection() As Boolean
            If _apiTrakt.AccessToken Is Nothing OrElse String.IsNullOrEmpty(_apiTrakt.AccessToken) OrElse _apiTrakt.Authorization.IsExpired Then
                CreateAPI()
            End If
            If _apiTrakt.AccessToken IsNot Nothing AndAlso Not String.IsNullOrEmpty(_apiTrakt.AccessToken) AndAlso Not _apiTrakt.Authorization.IsExpired Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Sub CreateAPI()
            Dim bIsExpired As Boolean = True
            'Default lifetime of an AccessToken is 90 days. So we set the default CreatedAt age to 91 days to get shure that the default value is to old and a new AccessToken has to be created.
            Dim dCreatedAt As Date = Date.Today.AddDays(-91)
            Dim iCreatedAt As Long = 0
            Dim iExpiresIn As Integer = 0

            Integer.TryParse(_SpecialSettings.ExpiresIn, iExpiresIn)
            If Long.TryParse(_SpecialSettings.CreatedAt, iCreatedAt) Then
                dCreatedAt = Functions.ConvertFromUnixTimestamp(iCreatedAt)
            End If

            'calculation actual ExiresIn value
            bIsExpired = dCreatedAt.AddSeconds(iExpiresIn) <= Date.Today

            _apiTrakt.AccessToken = _SpecialSettings.AccessToken
            _apiTrakt.Authorization.RefreshToken = _SpecialSettings.RefreshToken

            If (bIsExpired OrElse String.IsNullOrEmpty(_apiTrakt.AccessToken)) AndAlso Not String.IsNullOrEmpty(_apiTrakt.Authorization.RefreshToken) Then
                _apiTrakt.AccessToken = String.Empty
                _apiTrakt.OAuth.RefreshAuthorizationAsync()
                _newTokenCreated = True
                While _apiTrakt.AccessToken Is Nothing OrElse String.IsNullOrEmpty(_apiTrakt.AccessToken)
                    Threading.Thread.Sleep(100)
                End While
                _SpecialSettings.AccessToken = _apiTrakt.AccessToken
                _SpecialSettings.CreatedAt = Functions.ConvertToUnixTimestamp(_apiTrakt.Authorization.Created.Date).ToString
                _SpecialSettings.ExpiresIn = _apiTrakt.Authorization.ExpiresIn.ToString
                _SpecialSettings.RefreshToken = _apiTrakt.Authorization.RefreshToken
            End If

            If String.IsNullOrEmpty(_apiTrakt.AccessToken) Then
                Dim strActivationURL = _apiTrakt.OAuth.CreateAuthorizationUrl()
                Using dAuthorize As New frmAuthorize
                    If dAuthorize.ShowDialog(strActivationURL) = DialogResult.OK Then
                        _apiTrakt.OAuth.GetAuthorizationAsync(dAuthorize.Result)
                        _newTokenCreated = True
                        While _apiTrakt.AccessToken Is Nothing OrElse String.IsNullOrEmpty(_apiTrakt.AccessToken)
                            Threading.Thread.Sleep(100)
                        End While
                        _SpecialSettings.AccessToken = _apiTrakt.AccessToken
                        _SpecialSettings.CreatedAt = Functions.ConvertToUnixTimestamp(_apiTrakt.Authorization.Created.Date).ToString
                        _SpecialSettings.ExpiresIn = _apiTrakt.Authorization.ExpiresIn.ToString
                        _SpecialSettings.RefreshToken = _apiTrakt.Authorization.RefreshToken
                    End If
                End Using
            End If
        End Sub

        Public Function GetInfo_Movie(ByVal uintTraktID As UInteger, ByVal tFilteredOptions As Structures.ScrapeOptions) As MediaContainers.Movie
            If uintTraktID = 0 Then Return Nothing

            Dim nMovie As New MediaContainers.Movie
            nMovie.Scrapersource = "TRAKTTV"

            If CheckConnection() Then
                If tFilteredOptions.bMainRating Then
                    If _SpecialSettings.UsePersonalRating Then
                        Dim nPersonalRatedMovies = Task.Run(Function() _apiTrakt.Sync.GetRatingsAsync(TraktApiSharp.Enums.TraktRatingsItemType.Movie))
                        If nPersonalRatedMovies IsNot Nothing AndAlso nPersonalRatedMovies.Result IsNot Nothing AndAlso nPersonalRatedMovies.Result.Count > 0 Then
                            Dim tMovie = nPersonalRatedMovies.Result.FirstOrDefault(Function(f) f.Movie.Ids.Trakt = uintTraktID)
                            If tMovie IsNot Nothing Then
                                nMovie.Rating = CStr(tMovie.Rating)
                                nMovie.Votes = "1"
                                Return nMovie
                            End If
                        End If
                    End If

                    If _SpecialSettings.FallbackToGlobalRating OrElse Not _SpecialSettings.UsePersonalRating Then
                        Dim nGlobalRating = Task.Run(Function() _apiTrakt.Movies.GetMovieRatingsAsync(CStr(uintTraktID)))
                        If nGlobalRating IsNot Nothing AndAlso nGlobalRating.Result IsNot Nothing AndAlso nGlobalRating.Result.Rating IsNot Nothing AndAlso nGlobalRating.Result.Votes IsNot Nothing Then
                            nMovie.Rating = CStr(Math.Round(nGlobalRating.Result.Rating.Value, 1))
                            nMovie.Votes = CStr(nGlobalRating.Result.Votes)
                            Return nMovie
                        Else
                            logger.Info(String.Format("[GetMovieInfo] Could not scrape community rating/votes from trakt.tv! Current TraktID: {0}", uintTraktID))
                        End If
                    End If
                End If
            End If

            Return Nothing
        End Function

        Public Function GetInfo_TVEpisode(ByVal uintTVShowTraktID As UInteger, ByVal intSeason As Integer, ByVal intEpisode As Integer, ByVal tFilteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
            If uintTVShowTraktID = 0 Then Return Nothing

            Dim nTVEpisode As New MediaContainers.EpisodeDetails
            nTVEpisode.Scrapersource = "TRAKTTV"
            nTVEpisode.Episode = intEpisode
            nTVEpisode.Season = intSeason

            If CheckConnection() Then
                If tFilteredOptions.bEpisodeRating Then
                    If _SpecialSettings.UsePersonalRating Then
                        Dim nPersonalRatedTVEpisodes = Task.Run(Function() _apiTrakt.Sync.GetRatingsAsync(TraktApiSharp.Enums.TraktRatingsItemType.Episode))
                        If nPersonalRatedTVEpisodes IsNot Nothing AndAlso nPersonalRatedTVEpisodes.Result IsNot Nothing AndAlso nPersonalRatedTVEpisodes.Result.Count > 0 Then
                            Dim tTVEpisode = nPersonalRatedTVEpisodes.Result.FirstOrDefault(Function(f) f.Show.Ids.Trakt = uintTVShowTraktID AndAlso
                                                                                                f.Episode.Number IsNot Nothing AndAlso
                                                                                                CInt(f.Episode.Number) = intEpisode AndAlso
                                                                                                f.Episode.SeasonNumber IsNot Nothing AndAlso
                                                                                                CInt(f.Episode.SeasonNumber) = intSeason)
                            If tTVEpisode IsNot Nothing Then
                                nTVEpisode.Rating = CStr(tTVEpisode.Rating)
                                nTVEpisode.Votes = "1"
                                Return nTVEpisode
                            End If
                        End If
                    End If

                    If _SpecialSettings.FallbackToGlobalRating OrElse Not _SpecialSettings.UsePersonalRating Then
                        If Not uintTVShowTraktID = 0 Then
                            Dim nGlobalRating = Task.Run(Function() _apiTrakt.Episodes.GetEpisodeRatingsAsync(CStr(uintTVShowTraktID), intSeason, intEpisode))
                            If nGlobalRating IsNot Nothing AndAlso nGlobalRating.Result IsNot Nothing AndAlso nGlobalRating.Result.Rating IsNot Nothing AndAlso nGlobalRating.Result.Votes IsNot Nothing Then
                                nTVEpisode.Rating = CStr(Math.Round(nGlobalRating.Result.Rating.Value, 1)) ' traktrating.Rating.ToString
                                nTVEpisode.Votes = CStr(nGlobalRating.Result.Votes)
                                Return nTVEpisode
                            Else
                                logger.Info(String.Format("[GetInfo_TVEpisode] Could not scrape community rating/votes from trakt.tv! Current TraktID: {0} S{1}E{2}", uintTVShowTraktID, intSeason, intEpisode))
                            End If
                        End If
                    End If
                End If
            End If

            Return Nothing
        End Function

        Public Function GetInfo_TVShow(ByVal uintTraktID As UInteger, ByVal tScrapeModifiers As Structures.ScrapeModifiers, ByVal FilteredOptions As Structures.ScrapeOptions, ByRef lstEpisodes As List(Of Database.DBElement)) As MediaContainers.TVShow
            If uintTraktID = 0 Then Return Nothing

            Dim nTVShow As New MediaContainers.TVShow
            nTVShow.Scrapersource = "TRAKTTV"

            If CheckConnection() Then
                If FilteredOptions.bMainRating Then
                    Dim bRated_TVShow As Boolean = False

                    If _SpecialSettings.UsePersonalRating Then
                        Dim nPersonalRatedTVShows = Task.Run(Function() _apiTrakt.Sync.GetRatingsAsync(TraktApiSharp.Enums.TraktRatingsItemType.Show))
                        If nPersonalRatedTVShows IsNot Nothing AndAlso nPersonalRatedTVShows.Result IsNot Nothing AndAlso nPersonalRatedTVShows.Result.Count > 0 Then
                            Dim tTVShow = nPersonalRatedTVShows.Result.FirstOrDefault(Function(f) f.Show.Ids.Trakt = uintTraktID)
                            If tTVShow IsNot Nothing Then
                                nTVShow.Rating = CStr(tTVShow.Rating)
                                nTVShow.Votes = "1"
                                bRated_TVShow = True
                            End If
                        End If
                    End If

                    If Not bRated_TVShow AndAlso (_SpecialSettings.FallbackToGlobalRating OrElse Not _SpecialSettings.UsePersonalRating) Then
                        Dim nGlobalRating = Task.Run(Function() _apiTrakt.Shows.GetShowRatingsAsync(CStr(uintTraktID)))
                        If nGlobalRating IsNot Nothing AndAlso nGlobalRating.Result IsNot Nothing AndAlso nGlobalRating.Result.Rating IsNot Nothing AndAlso nGlobalRating.Result.Votes IsNot Nothing Then
                            nTVShow.Rating = CStr(Math.Round(nGlobalRating.Result.Rating.Value, 1)) ' traktrating.Rating.ToString
                            nTVShow.Votes = CStr(nGlobalRating.Result.Votes)
                        Else
                            logger.Info(String.Format("[GetInfo_TVShow] Could not scrape community rating/votes from trakt.tv! Current TraktID: {0}", uintTraktID))
                        End If
                    End If
                End If

                If tScrapeModifiers.withEpisodes AndAlso FilteredOptions.bEpisodeRating AndAlso lstEpisodes.Count > 0 Then
                    'looks like there is no way to get all episodes for a tv show. so we scrape only local existing episodes
                    For Each nDBElement As Database.DBElement In lstEpisodes
                        Dim nEpisode As MediaContainers.EpisodeDetails = GetInfo_TVEpisode(uintTraktID, nDBElement.TVEpisode.Season, nDBElement.TVEpisode.Episode, FilteredOptions)
                        If nEpisode IsNot Nothing Then
                            nTVShow.KnownEpisodes.Add(nEpisode)
                        End If
                    Next
                End If

                Return nTVShow
            End If

            Return Nothing
        End Function

        Public Function GetTraktID(ByVal tDBElement As Database.DBElement, Optional bForceTVShowID As Boolean = False) As UInteger
            Dim nSearchResults As Task(Of Objects.Basic.TraktPaginationListResult(Of Objects.Basic.TraktSearchResult)) = Nothing
            Dim nContentType As Enums.ContentType = If(bForceTVShowID, Enums.ContentType.TVShow, tDBElement.ContentType)

            If CheckConnection() Then
                Select Case nContentType
                    Case Enums.ContentType.Movie
                        'search by IMDB ID
                        If tDBElement.Movie.IMDBSpecified Then
                            nSearchResults = Task.Run(Function() _apiTrakt.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.ImDB, tDBElement.Movie.IMDB, TraktApiSharp.Enums.TraktSearchResultType.Movie))
                        End If
                        'search by TMDB ID
                        If nSearchResults.Result.Items.Count = 0 AndAlso tDBElement.Movie.TMDBSpecified Then
                            nSearchResults = Task.Run(Function() _apiTrakt.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TmDB, tDBElement.Movie.TMDB, TraktApiSharp.Enums.TraktSearchResultType.Movie))
                        End If
                        If nSearchResults.Result.Items.Count = 1 Then
                            Return nSearchResults.Result.Items(0).Movie.Ids.Trakt
                        Else
                            logger.Info(String.Format("[GetIDs] Could not scrape TraktID from trakt.tv! IMDB: {0} / TMDB: {1}", tDBElement.Movie.IMDB, tDBElement.Movie.TMDB))
                        End If
                    Case Enums.ContentType.TVEpisode
                        'search by TVDB ID
                        If tDBElement.TVEpisode.TVDBSpecified Then
                            nSearchResults = Task.Run(Function() _apiTrakt.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TvDB, tDBElement.TVEpisode.TVDB, TraktApiSharp.Enums.TraktSearchResultType.Episode))
                        End If
                        'search by IMDB ID
                        If nSearchResults.Result.Items.Count = 0 AndAlso tDBElement.TVEpisode.IMDBSpecified Then
                            nSearchResults = Task.Run(Function() _apiTrakt.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.ImDB, tDBElement.TVEpisode.IMDB, TraktApiSharp.Enums.TraktSearchResultType.Episode))
                        End If
                        'search by TMDB ID
                        If nSearchResults.Result.Items.Count = 0 AndAlso tDBElement.TVEpisode.TMDBSpecified Then
                            nSearchResults = Task.Run(Function() _apiTrakt.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TmDB, tDBElement.TVEpisode.TMDB, TraktApiSharp.Enums.TraktSearchResultType.Episode))
                        End If
                        If nSearchResults.Result.Items.Count = 1 Then
                            Return nSearchResults.Result.Items(0).Episode.Ids.Trakt
                        Else
                            logger.Info(String.Format("[GetIDs] Could not scrape TraktID from trakt.tv! TVDB: {0} / IMDB: {1} / TMDB: {2}", tDBElement.TVEpisode.TVDB, tDBElement.TVEpisode.IMDB, tDBElement.TVEpisode.TMDB))
                        End If
                    Case Enums.ContentType.TVShow
                        'search by TVDB ID
                        If tDBElement.TVShow.TVDBSpecified Then
                            nSearchResults = Task.Run(Function() _apiTrakt.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TvDB, tDBElement.TVShow.TVDB, TraktApiSharp.Enums.TraktSearchResultType.Show))
                        End If
                        'search by IMDB ID
                        If nSearchResults.Result.Items.Count = 0 AndAlso tDBElement.TVShow.IMDBSpecified Then
                            nSearchResults = Task.Run(Function() _apiTrakt.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.ImDB, tDBElement.TVShow.IMDB, TraktApiSharp.Enums.TraktSearchResultType.Show))
                        End If
                        'search by TMDB ID
                        If nSearchResults.Result.Items.Count = 0 AndAlso tDBElement.TVShow.TMDBSpecified Then
                            nSearchResults = Task.Run(Function() _apiTrakt.Search.GetIdLookupResultsAsync(TraktApiSharp.Enums.TraktSearchIdType.TmDB, tDBElement.TVShow.TMDB, TraktApiSharp.Enums.TraktSearchResultType.Show))
                        End If
                        If nSearchResults.Result.Items.Count = 1 Then
                            Return nSearchResults.Result.Items(0).Show.Ids.Trakt
                        Else
                            logger.Info(String.Format("[GetIDs] Could not scrape TraktID from trakt.tv! TVDB: {0} / IMDB: {1} / TMDB: {2}", tDBElement.TVShow.TVDB, tDBElement.TVShow.IMDB, tDBElement.TVShow.TMDB))
                        End If
                End Select
            End If

            Return 0
        End Function

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

    End Class

End Namespace