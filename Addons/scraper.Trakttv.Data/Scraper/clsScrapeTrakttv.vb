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
Imports Trakttv

Namespace TrakttvScraper

    Public Class Scraper

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _SpecialSettings As Trakttv_Data.SpecialSettings

#End Region 'Fields

#Region "Properties"

        ReadOnly Property Token() As String
            Get
                Return _SpecialSettings.Token
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub New(ByRef SpecialSettings As Trakttv_Data.SpecialSettings)
            _SpecialSettings = SpecialSettings
            Try
                CreateToken(_SpecialSettings.Username, _SpecialSettings.Password, _SpecialSettings.Token)
                SpecialSettings.Token = _SpecialSettings.Token
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

        Public Function CheckConnection() As Boolean
            If String.IsNullOrEmpty(TraktSettings.Token) Then
                CreateToken(_SpecialSettings.Username, _SpecialSettings.Password, String.Empty)
            End If
            If Not String.IsNullOrEmpty(TraktSettings.Token) Then
                Return True
            Else
                Return False
            End If
        End Function
        ''' <summary>
        '''  Trakt-Login process for using v2 API (Token based authentification) 
        ''' </summary>
        ''' <param name="strUsername">trakt.tv Username</param>
        ''' <param name="strUsername">trakt.tv Password</param>
        ''' <param name="strToken">trakt.tv Token, may be empty (then it will be generated)</param>
        ''' <returns>(new) trakt.tv Token</returns>
        ''' <remarks>
        ''' 2015/01/17 Cocotus - First implementation of new V2 Authentification process for trakt.tv API
        ''' </remarks>
        Private Function CreateToken(ByVal strUsername As String, ByVal strPassword As String, ByRef strToken As String) As String
            ' Use Trakttv wrapper
            Dim account As New TraktAPI.Model.TraktAuthentication
            account.Username = strUsername
            account.Password = strPassword
            TraktSettings.Password = strPassword
            TraktSettings.Username = strUsername
            TraktSettings.Token = strToken

            If String.IsNullOrEmpty(strToken) Then
                Dim response = TraktMethods.LoginToAccount(account)
                strToken = TraktSettings.Token
            End If

            Return TraktSettings.Token
        End Function

        Public Function GetInfo_Movie(ByVal intTraktID As Integer, ByVal tFilteredOptions As Structures.ScrapeOptions) As MediaContainers.Movie
            If intTraktID = -1 Then Return Nothing

            Dim nMovie As New MediaContainers.Movie
            nMovie.Scrapersource = "TRAKTTV"

            If CheckConnection() Then
                If tFilteredOptions.bMainRating Then
                    If _SpecialSettings.UsePersonalRating Then
                        Dim nPersonalRatedMovies As IEnumerable(Of TraktAPI.Model.TraktMovieRated) = TrakttvAPI.GetRatedMovies
                        If nPersonalRatedMovies IsNot Nothing AndAlso nPersonalRatedMovies.Count > 0 Then
                            Dim tMovie = nPersonalRatedMovies.FirstOrDefault(Function(f) CInt(f.Movie.Ids.Trakt) = intTraktID)
                            If tMovie IsNot Nothing Then
                                nMovie.Rating = CStr(tMovie.Rating)
                                nMovie.Votes = "1"
                                Return nMovie
                            End If
                        End If
                    End If

                    If _SpecialSettings.FallbackToGlobalRating OrElse Not _SpecialSettings.UsePersonalRating Then
                        Dim nGlobalRating As TraktAPI.Model.TraktRating = TrakttvAPI.GetMovieRating(CStr(intTraktID))
                        If Not nGlobalRating Is Nothing AndAlso Not nGlobalRating.Rating Is Nothing AndAlso Not nGlobalRating.Votes Is Nothing Then
                            nMovie.Rating = CStr(Math.Round(nGlobalRating.Rating.Value, 1)) ' traktrating.Rating.ToString
                            nMovie.Votes = CStr(nGlobalRating.Votes)
                            Return nMovie
                        Else
                            logger.Info(String.Format("[GetMovieInfo] Could not scrape community rating/votes from trakt.tv! Current TraktID: {0}", intTraktID))
                        End If
                    End If
                End If
            End If

            Return Nothing
        End Function

        Public Function GetInfo_TVEpisode(ByVal intTVShowTraktID As Integer, ByVal intSeason As Integer, ByVal intEpisode As Integer, ByVal tFilteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
            If intTVShowTraktID = -1 Then Return Nothing

            Dim nTVEpisode As New MediaContainers.EpisodeDetails
            nTVEpisode.Scrapersource = "TRAKTTV"
            nTVEpisode.Episode = intEpisode
            nTVEpisode.Season = intSeason

            If CheckConnection() Then
                If tFilteredOptions.bEpisodeRating Then
                    If _SpecialSettings.UsePersonalRating Then
                        Dim nPersonalRatedTVEpisodes As IEnumerable(Of TraktAPI.Model.TraktEpisodeRated) = TrakttvAPI.GetRatedEpisodes
                        If nPersonalRatedTVEpisodes IsNot Nothing AndAlso nPersonalRatedTVEpisodes.Count > 0 Then
                            Dim tTVEpisode = nPersonalRatedTVEpisodes.FirstOrDefault(Function(f) CInt(f.Show.Ids.Trakt) = intTVShowTraktID AndAlso
                                                                                         f.Episode.Number = intEpisode AndAlso
                                                                                         f.Episode.Season = intSeason)
                            If tTVEpisode IsNot Nothing Then
                                nTVEpisode.Rating = CStr(tTVEpisode.Rating)
                                nTVEpisode.Votes = "1"
                                Return nTVEpisode
                            End If
                        End If
                    End If

                    If _SpecialSettings.FallbackToGlobalRating OrElse Not _SpecialSettings.UsePersonalRating Then
                        If Not intTVShowTraktID = 0 Then
                            Dim nGlobalRating As TraktAPI.Model.TraktRating = TrakttvAPI.GetEpisodeRating(CStr(intTVShowTraktID), intSeason, intEpisode)
                            If Not nGlobalRating Is Nothing AndAlso Not nGlobalRating.Rating Is Nothing AndAlso Not nGlobalRating.Votes Is Nothing Then
                                nTVEpisode.Rating = CStr(Math.Round(nGlobalRating.Rating.Value, 1)) ' traktrating.Rating.ToString
                                nTVEpisode.Votes = CStr(nGlobalRating.Votes)
                                Return nTVEpisode
                            Else
                                logger.Info(String.Format("[GetInfo_TVEpisode] Could not scrape community rating/votes from trakt.tv! Current TraktID: {0} S{1}E{2}", intTVShowTraktID, intSeason, intEpisode))
                            End If
                        End If
                    End If
                End If
            End If

            Return Nothing
        End Function

        Public Function GetInfo_TVShow(ByVal intTraktID As Integer, ByVal tScrapeModifiers As Structures.ScrapeModifiers, ByVal FilteredOptions As Structures.ScrapeOptions, ByRef lstEpisodes As List(Of Database.DBElement)) As MediaContainers.TVShow
            If intTraktID = -1 Then Return Nothing

            Dim nTVShow As New MediaContainers.TVShow
            nTVShow.Scrapersource = "TRAKTTV"

            If CheckConnection() Then
                If FilteredOptions.bMainRating Then
                    Dim bRated_TVShow As Boolean = False

                    If _SpecialSettings.UsePersonalRating Then
                        Dim nPersonalRatedTVShows As IEnumerable(Of TraktAPI.Model.TraktShowRated) = TrakttvAPI.GetRatedShows
                        If nPersonalRatedTVShows IsNot Nothing AndAlso nPersonalRatedTVShows.Count > 0 Then
                            Dim tTVShow = nPersonalRatedTVShows.FirstOrDefault(Function(f) (CInt(f.Show.Ids.Trakt) = intTraktID))
                            If tTVShow IsNot Nothing Then
                                nTVShow.Rating = CStr(tTVShow.Rating)
                                nTVShow.Votes = "1"
                                bRated_TVShow = True
                            End If
                        End If
                    End If

                    If Not bRated_TVShow AndAlso (_SpecialSettings.FallbackToGlobalRating OrElse Not _SpecialSettings.UsePersonalRating) Then
                        Dim nGlobalRating As TraktAPI.Model.TraktRating = TrakttvAPI.GetShowRating(CStr(intTraktID))
                        If Not nGlobalRating Is Nothing AndAlso Not nGlobalRating.Rating Is Nothing AndAlso Not nGlobalRating.Votes Is Nothing Then
                            nTVShow.Rating = CStr(Math.Round(nGlobalRating.Rating.Value, 1)) ' traktrating.Rating.ToString
                            nTVShow.Votes = CStr(nGlobalRating.Votes)
                        Else
                            logger.Info(String.Format("[GetInfo_TVShow] Could not scrape community rating/votes from trakt.tv! Current TraktID: {0}", intTraktID))
                        End If
                    End If
                End If

                If tScrapeModifiers.withEpisodes AndAlso FilteredOptions.bEpisodeRating AndAlso lstEpisodes.Count > 0 Then
                    'looks like there is no way to get all episodes for a tv show. so we scrape only local existing episodes
                    For Each nDBElement As Database.DBElement In lstEpisodes
                        Dim nEpisode As MediaContainers.EpisodeDetails = GetInfo_TVEpisode(intTraktID, nDBElement.TVEpisode.Season, nDBElement.TVEpisode.Episode, FilteredOptions)
                        If nEpisode IsNot Nothing Then
                            nTVShow.KnownEpisodes.Add(nEpisode)
                        End If
                    Next
                End If

                Return nTVShow
            End If

            Return Nothing
        End Function

        Public Function GetTraktID(ByVal tDBElement As Database.DBElement, Optional bForceTVShowID As Boolean = False) As Integer
            Dim nSearchResults As IEnumerable(Of TraktAPI.Model.TraktSearchResult) = Nothing
            Dim nContentType As Enums.ContentType = If(bForceTVShowID, Enums.ContentType.TVShow, tDBElement.ContentType)

            If CheckConnection() Then
                Select Case nContentType
                    Case Enums.ContentType.Movie
                        If tDBElement.Movie.IMDBSpecified Then
                            nSearchResults = TrakttvAPI.SearchById("imdb", tDBElement.Movie.IMDB)
                        End If
                        If nSearchResults Is Nothing OrElse nSearchResults.Where(Function(f) f.Type = "movie").Count = 0 AndAlso tDBElement.Movie.TMDBSpecified Then
                            nSearchResults = TrakttvAPI.SearchById("tmdb", tDBElement.Movie.TMDB)
                        End If
                        If nSearchResults IsNot Nothing AndAlso nSearchResults.Where(Function(f) f.Type = "movie").Count = 1 Then
                            Return CInt(nSearchResults.Where(Function(f) f.Type = "movie")(0).Movie.Ids.Trakt)
                        Else
                            logger.Info(String.Format("[GetIDs] Could not scrape TraktID from trakt.tv! IMDB: {0} / TMDB: {1}", tDBElement.Movie.IMDB, tDBElement.Movie.TMDB))
                        End If
                    Case Enums.ContentType.TVEpisode
                        If tDBElement.TVEpisode.IMDBSpecified Then
                            nSearchResults = TrakttvAPI.SearchById("imdb", tDBElement.TVEpisode.IMDB)
                        End If
                        If nSearchResults Is Nothing OrElse nSearchResults.Where(Function(f) f.Type = "episode").Count = 0 AndAlso tDBElement.TVEpisode.TMDBSpecified Then
                            nSearchResults = TrakttvAPI.SearchById("tmdb", tDBElement.TVEpisode.TMDB)
                        End If
                        If nSearchResults Is Nothing OrElse nSearchResults.Where(Function(f) f.Type = "episode").Count = 0 AndAlso tDBElement.TVEpisode.TVDBSpecified Then
                            nSearchResults = TrakttvAPI.SearchById("tvdb", tDBElement.TVEpisode.TVDB)
                        End If
                        If nSearchResults IsNot Nothing AndAlso nSearchResults.Where(Function(f) f.Type = "episode").Count = 1 Then
                            Return CInt(nSearchResults.Where(Function(f) f.Type = "episode")(0).Episode.Ids.Trakt)
                        Else
                            logger.Info(String.Format("[GetIDs] Could not scrape TraktID from trakt.tv! IMDB: {0} / TMDB: {1} / TVDB: {2}", tDBElement.TVEpisode.IMDB, tDBElement.TVEpisode.TMDB, tDBElement.TVEpisode.TVDB))
                        End If
                    Case Enums.ContentType.TVShow
                        If tDBElement.TVShow.IMDBSpecified Then
                            nSearchResults = TrakttvAPI.SearchById("imdb", tDBElement.TVShow.IMDB)
                        End If
                        If nSearchResults Is Nothing OrElse nSearchResults.Where(Function(f) f.Type = "show").Count = 0 AndAlso tDBElement.TVShow.TMDBSpecified Then
                            nSearchResults = TrakttvAPI.SearchById("tmdb", tDBElement.TVShow.TMDB)
                        End If
                        If nSearchResults Is Nothing OrElse nSearchResults.Where(Function(f) f.Type = "show").Count = 0 AndAlso tDBElement.TVShow.TVDBSpecified Then
                            nSearchResults = TrakttvAPI.SearchById("tvdb", tDBElement.TVShow.TVDB)
                        End If
                        If nSearchResults IsNot Nothing AndAlso nSearchResults.Where(Function(f) f.Type = "show").Count = 1 Then
                            Return CInt(nSearchResults.Where(Function(f) f.Type = "show")(0).Show.Ids.Trakt)
                        Else
                            logger.Info(String.Format("[GetIDs] Could not scrape TraktID from trakt.tv! IMDB: {0} / TMDB: {1} / TVDB: {2}", tDBElement.TVShow.IMDB, tDBElement.TVShow.TMDB, tDBElement.TVShow.TVDB))
                        End If
                End Select
            End If

            Return -1
        End Function

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

    End Class

End Namespace