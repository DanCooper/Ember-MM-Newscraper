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
Imports System.IO.Compression
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog
Imports Trakttv

Namespace TrakttvScraper

    Public Class Scraper

#Region "Fields"

        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
        Friend WithEvents bwTrakttv As New System.ComponentModel.BackgroundWorker
        'special settings of trakt.tv module like "User personal ratings"
        Private _SpecialSettings As Trakttv_Data.SpecialSettings
        'Token generated after successfull login to trakt.tv account - without a token no scraping is possible
        Private _Token As String = String.Empty
        'Notice: IMDBID or queried traktID of scraped show/movie is needed! - without ID scraping is not possible
        '(at the moment IMDBID or traktID of show(movie is required to query stats on trakt.tv -> may change in future)
        'TRAKTID of current scraped DBElement
        Private _TrakttvDBElementTRAKTID As String = String.Empty
        'IMDBID of current scraped DBElement
        Private _TrakttvDBElementIMDBID As String = String.Empty
        'TMDBID of current scraped DBElement
        Private _TrakttvDBElementTMDBID As String = String.Empty
        'collection of all rated movies on trakt.tv
        Private _traktRatedMovies As IEnumerable(Of TraktAPI.Model.TraktMovieRated) = Nothing
        'collection of all rated epsiodes on trakt.tv
        Private _traktRatedEpisodes As IEnumerable(Of TraktAPI.Model.TraktEpisodeRated) = Nothing

#End Region 'Fields

#Region "Methods"

        ''' <summary>
        '''  Login to trakttv and retrieve personal video data
        ''' </summary>
        ''' <param name="SpecialSettings">Special settings of trakttv scraper module</param>
        ''' <param name="oDBElement">DBElement which is going to be scraped</param>
        ''' <remarks>
        ''' 2015/11/18 Cocotus - First implementation
        ''' Constructor for trakt.tv data scraper - use this to do things that only needs to be done one time
        ''' Connect to trakt.tv and scrape personal video data (personal ratings, last played) only once to minimize amount of queries to trakt.tv server
        ''' </remarks>
        Public Sub New(ByVal SpecialSettings As Trakttv_Data.SpecialSettings, ByVal oDBElement As Database.DBElement)
            Try
                _SpecialSettings = SpecialSettings
                _Token = TraktMethods.LoginToTrakt(_SpecialSettings.TrakttvUserName, _SpecialSettings.TrakttvPassword)
                _TrakttvDBElementIMDBID = String.Empty
                _TrakttvDBElementTMDBID = String.Empty
                _TrakttvDBElementTRAKTID = String.Empty
                If String.IsNullOrEmpty(_Token) Then
                    logger.Error(String.Format("[New] Can't login to trakt.tv account. Could not scrape trakt.tv data for DBElement: {0}", oDBElement.ListTitle))
                Else
                    Dim TraktResult As New TraktAPI.Model.TraktSearchResult
                    'Movie Mode
                    Select Case oDBElement.ContentType
                        Case Enums.ContentType.Movie
                            If oDBElement IsNot Nothing Then
                                'make sure we set correct ID of DBElement for trakt.tv queries (need IMDBID oder traktID)
                                If Not String.IsNullOrEmpty(oDBElement.Movie.IMDB) Then
                                    If oDBElement.Movie.IMDB.StartsWith("tt") = False Then
                                        _TrakttvDBElementIMDBID = "tt" & oDBElement.Movie.IMDB
                                    Else
                                        _TrakttvDBElementIMDBID = oDBElement.Movie.IMDB
                                    End If
                                ElseIf oDBElement.Movie.TMDBSpecified Then
                                    _TrakttvDBElementTMDBID = oDBElement.Movie.TMDB
                                    TraktResult = GetIDs(oDBElement.Movie.TMDB, "tmdb")
                                    If TraktResult IsNot Nothing AndAlso TraktResult.Movie IsNot Nothing AndAlso TraktResult.Movie.Ids IsNot Nothing AndAlso TraktResult.Movie.Ids.Trakt IsNot Nothing Then
                                        _TrakttvDBElementTRAKTID = CStr(TraktResult.Movie.Ids.Trakt)
                                        If TraktResult.Movie.Ids.Imdb IsNot Nothing Then
                                            _TrakttvDBElementIMDBID = TraktResult.Movie.Ids.Imdb
                                        End If
                                    End If
                                End If
                                If String.IsNullOrEmpty(_TrakttvDBElementIMDBID) OrElse String.IsNullOrEmpty(_TrakttvDBElementTRAKTID) Then
                                    logger.Error(String.Format("[New] No IMDBID/TraktID is available for element: {0}. Leave trakt.tv scraper!", oDBElement.ListTitle))
                                    Return
                                End If
                            End If
                            'Retrieve at scraper startup all user video data on trakt.tv like ratings (only need to do this once and NOT for every scraped movie/show)
                            If _SpecialSettings.UsePersonalRatings Then
                                _traktRatedMovies = TrakttvAPI.GetRatedMovies
                                If _traktRatedMovies Is Nothing Then
                                    logger.Error(String.Format("[New] Could not scrape personal trakt.tv ratings for element: {0}. Leave trakt.tv scraper!", oDBElement.ListTitle))
                                End If
                            End If

                            'TV Mode
                        Case Enums.ContentType.TV, Enums.ContentType.TVEpisode, Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                            If oDBElement IsNot Nothing Then
                                'at the moment IMDBID of show is required to query show stats on trakt.tv -> may change in future!
                                'API: http://docs.trakt.apiary.io/#reference/shows/ratings/get-show-ratings
                                If Not String.IsNullOrEmpty(oDBElement.TVShow.IMDB) Then
                                    If Not oDBElement.TVShow.IMDB.StartsWith("tt") Then
                                        _TrakttvDBElementIMDBID = "tt" & oDBElement.TVShow.IMDB
                                    Else
                                        _TrakttvDBElementIMDBID = oDBElement.TVShow.IMDB
                                    End If
                                ElseIf oDBElement.TVShow.TMDBSpecified Then
                                    _TrakttvDBElementTMDBID = oDBElement.TVShow.TMDB
                                    TraktResult = GetIDs(oDBElement.TVShow.TMDB, "tmdb")
                                    If TraktResult IsNot Nothing AndAlso TraktResult.Show IsNot Nothing AndAlso TraktResult.Show.Ids IsNot Nothing AndAlso TraktResult.Show.Ids.Trakt IsNot Nothing Then
                                        _TrakttvDBElementTRAKTID = CStr(TraktResult.Show.Ids.Trakt)
                                        If TraktResult.Show.Ids.Imdb IsNot Nothing Then
                                            _TrakttvDBElementIMDBID = TraktResult.Show.Ids.Imdb
                                        End If
                                    End If
                                ElseIf oDBElement.TVShow.TVDBSpecified Then
                                    TraktResult = GetIDs(oDBElement.TVShow.TVDB, "tvdb")
                                    If TraktResult IsNot Nothing AndAlso TraktResult.Show IsNot Nothing AndAlso TraktResult.Show.Ids IsNot Nothing AndAlso TraktResult.Show.Ids.Trakt IsNot Nothing Then
                                        _TrakttvDBElementTRAKTID = CStr(TraktResult.Show.Ids.Trakt)
                                        If TraktResult.Show.Ids.Imdb IsNot Nothing Then
                                            _TrakttvDBElementIMDBID = TraktResult.Show.Ids.Imdb
                                        End If
                                        If TraktResult.Show.Ids.Tmdb IsNot Nothing Then
                                            _TrakttvDBElementTMDBID = CStr(TraktResult.Show.Ids.Tmdb)
                                        End If
                                    End If
                                End If
                                If String.IsNullOrEmpty(_TrakttvDBElementIMDBID) AndAlso String.IsNullOrEmpty(_TrakttvDBElementTRAKTID) Then
                                    logger.Error(String.Format("[New] No IMDBID/TraktID is available for element: {0}. Leave trakt.tv scraper!", oDBElement.ListTitle))
                                    Return
                                End If
                            End If
                            'Retrieve at scraper startup all user video data on trakt.tv like ratings (only need to do this once and NOT for every scraped movie/show)
                            If _SpecialSettings.UsePersonalRatings Then
                                _traktRatedEpisodes = TrakttvAPI.GetRatedEpisodes
                                If _traktRatedEpisodes Is Nothing Then
                                    logger.Error(String.Format("[New] Could not scrape personal trakt.tv ratings for element: {0}. Leave trakt.tv scraper!", oDBElement.ListTitle))
                                End If
                            End If
                    End Select
                End If
            Catch ex As Exception
                logger.Error(ex)
            End Try
        End Sub

        ''' <summary>
        '''  Scrape MovieDetails from trakttv
        ''' </summary>
        ''' <param name="FilteredOptions">Module settings<param>
        ''' <param name="IsSearch">Not used at moment</param>
        ''' <returns>True: success, false: no success</returns>
        ''' <remarks>
        ''' 2015/11/18 Cocotus - First implementation
        ''' For now only retrieve trakt.tv exklusive data like playcount, lastplayed, rating and votes
        ''' IMDBID/TraktID of movie to be scraped is needed!
        ''' </remarks>
        Public Function GetMovieInfo(ByVal FilteredOptions As Structures.ScrapeOptions, ByVal IsSearch As Boolean) As MediaContainers.Movie
            If String.IsNullOrEmpty(_TrakttvDBElementIMDBID) OrElse String.IsNullOrEmpty(_TrakttvDBElementTRAKTID) Then Return Nothing

            Dim nMovie As New MediaContainers.Movie
            Try
                nMovie.Scrapersource = "TRAKTTV"
                nMovie.IMDB = _TrakttvDBElementIMDBID
                nMovie.TMDB = _TrakttvDBElementTMDBID

                If String.IsNullOrEmpty(_Token) Then
                    logger.Error(String.Concat("[GetMovieInfo] Can't login to trakt.tv account! Current movieID: ", If(String.IsNullOrEmpty(_TrakttvDBElementTRAKTID), _TrakttvDBElementIMDBID, _TrakttvDBElementTRAKTID)))
                    Return Nothing
                End If

                'Rating / Votes
                If FilteredOptions.bMainRating Then
                    If Not String.IsNullOrEmpty(_Token) Then
                        'scrape community rating and votes
                        Dim traktrating As TraktAPI.Model.TraktRating = TrakttvAPI.GetMovieRating(If(String.IsNullOrEmpty(_TrakttvDBElementTRAKTID), _TrakttvDBElementIMDBID, _TrakttvDBElementTRAKTID))
                        If Not traktrating Is Nothing AndAlso Not traktrating.Rating Is Nothing AndAlso Not traktrating.Votes Is Nothing Then
                            nMovie.Rating = CStr(Math.Round(traktrating.Rating.Value, 1)) ' traktrating.Rating.ToString
                            nMovie.Votes = traktrating.Votes.ToString
                        Else
                            logger.Info("[GetMovieInfo] Could not scrape community rating/votes from trakt.tv! Current movieID: " & If(String.IsNullOrEmpty(_TrakttvDBElementTRAKTID), _TrakttvDBElementIMDBID, _TrakttvDBElementTRAKTID))
                        End If
                        'instead of community rating use personal rating if available?
                        If _SpecialSettings.UsePersonalRatings Then
                            If _traktRatedMovies Is Nothing = False Then
                                For Each ratedMovie As TraktAPI.Model.TraktMovieRated In _traktRatedMovies
                                    If Not ratedMovie.Movie.Ids Is Nothing Then
                                        'Check if information is stored...
                                        If (Not String.IsNullOrEmpty(nMovie.IMDB) AndAlso Not ratedMovie.Movie.Ids.Imdb Is Nothing AndAlso ((ratedMovie.Movie.Ids.Imdb = _TrakttvDBElementIMDBID) OrElse (ratedMovie.Movie.Ids.Trakt.ToString = _TrakttvDBElementTRAKTID))) Then
                                            nMovie.Rating = CStr(ratedMovie.Rating)
                                            Exit For
                                        End If
                                    End If
                                Next
                            Else
                                logger.Info("[GetMovieInfo] No ratings of movies scraped from trakt.tv! Current movieID: " & If(String.IsNullOrEmpty(_TrakttvDBElementTRAKTID), _TrakttvDBElementIMDBID, _TrakttvDBElementTRAKTID))
                            End If
                        End If
                    End If
                End If

            Catch ex As Exception
                logger.Error(ex)
                Return Nothing
            End Try

            Return nMovie
        End Function

        ''' <summary>
        '''  Scrape TV Show details from trakttv
        ''' </summary>
        ''' <param name="FilteredOptions">Module settings<param>
        ''' <param name="ScrapeModifiers">More options - scrape episode/season infos?</param>
        ''' <param name="oDBElement">DBElement/Show which is going to be scraped</param>
        ''' <returns>True: success, false: no success</returns>
        ''' <remarks>
        ''' 2015/11/18 Cocotus - First implementation
        ''' trakt.tv API supports ONLY IMDB
        ''' IMDBID/TraktID of tv show to be scraped is needed!
        ''' </remarks>
        Public Function GetTVShowInfo(ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByVal FilteredOptions As Structures.ScrapeOptions, ByVal oDBElement As Database.DBElement) As MediaContainers.TVShow
            If String.IsNullOrEmpty(_TrakttvDBElementIMDBID) AndAlso String.IsNullOrEmpty(_TrakttvDBElementTRAKTID) Then Return Nothing

            Dim nTVShow As New MediaContainers.TVShow

            If bwTrakttv.CancellationPending Then Return Nothing
            nTVShow.IMDB = _TrakttvDBElementIMDBID
            nTVShow.TMDB = _TrakttvDBElementTMDBID
            nTVShow.Scrapersource = "TRAKTTV"

            'Rating
            If FilteredOptions.bMainRating Then
                Dim traktrating As TraktAPI.Model.TraktRating = TrakttvAPI.GetShowRating(If(String.IsNullOrEmpty(_TrakttvDBElementTRAKTID), _TrakttvDBElementIMDBID, _TrakttvDBElementTRAKTID))
                If Not traktrating Is Nothing AndAlso Not traktrating.Rating Is Nothing AndAlso Not traktrating.Votes Is Nothing Then
                    nTVShow.Rating = CStr(Math.Round(traktrating.Rating.Value, 1)) ' traktrating.Rating.ToString
                    nTVShow.Votes = traktrating.Votes.ToString
                Else
                    logger.Info("[GetTVShowInfo] Could not scrape community rating/votes from trakt.tv! Current showID: " & If(String.IsNullOrEmpty(_TrakttvDBElementTRAKTID), _TrakttvDBElementIMDBID, _TrakttvDBElementTRAKTID))
                End If
            End If

            If bwTrakttv.CancellationPending Then Return Nothing

            'Seasons and Episodes?
            If ScrapeModifiers.withEpisodes OrElse ScrapeModifiers.withSeasons Then
                'since we don't scrape any episode info with trakt.tv scraper, copy episodes info of current DBElement to scrape for trakt.tv ratings
                Dim nlistEpisode As New List(Of Integer)
                For Each aSeason As MediaContainers.SeasonDetails In oDBElement.TVShow.Seasons.Seasons
                    nlistEpisode.Clear()
                    For Each episode In oDBElement.Episodes
                        If episode.TVEpisode.Season = aSeason.Season Then
                            nlistEpisode.Add(episode.TVEpisode.Episode)
                        End If
                    Next
                    GetTVSeasonInfo(nTVShow, aSeason.Season, nlistEpisode, ScrapeModifiers, FilteredOptions)
                Next
            End If

            Return nTVShow
        End Function
        Public Sub GetTVSeasonInfo(ByRef nTVShow As MediaContainers.TVShow, ByVal SeasonNumber As Integer, ByVal Episodes As List(Of Integer), ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef FilteredOptions As Structures.ScrapeOptions)
            If String.IsNullOrEmpty(_TrakttvDBElementIMDBID) AndAlso String.IsNullOrEmpty(_TrakttvDBElementTRAKTID) Then Exit Sub
            If ScrapeModifiers.withSeasons Then
                Dim nSeason As New MediaContainers.SeasonDetails
                nSeason.TMDB = _TrakttvDBElementTMDBID
                'nSeason.IMDB = _TrakttvDBElementIMDBID
                nSeason.Scrapersource = "TRAKTTV"
                nTVShow.KnownSeasons.Add(nSeason)
            End If
            If ScrapeModifiers.withEpisodes Then
                For Each episode In Episodes
                    nTVShow.KnownEpisodes.Add(GetTVEpisodeInfo(SeasonNumber, episode, FilteredOptions))
                Next
            End If
        End Sub

        ''' <summary>
        '''  Scrape TV Season details from trakttv
        ''' </summary>
        ''' <param name="SeasonNumber">Number of season to scrape</param>
        ''' <param name="FilteredOptions">Module settings<param>
        ''' <returns>Season details</returns>
        ''' <remarks>
        ''' 2015/11/18 Cocotus - First implementation
        ''' Not used in moment, since theres no rating/votes field in season database!
        ''' IMDBID/TraktID of tv show to be scraped is needed!
        ''' </remarks>
        Public Function GetTVSeasonInfo(ByVal SeasonNumber As Integer, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.SeasonDetails
            If String.IsNullOrEmpty(_TrakttvDBElementIMDBID) AndAlso String.IsNullOrEmpty(_TrakttvDBElementTRAKTID) Then Return Nothing
            Dim nSeason As New MediaContainers.SeasonDetails
            nSeason.TMDB = _TrakttvDBElementTMDBID
            'nSeason.IMDB = _TrakttvDBElementIMDBID
            nSeason.Scrapersource = "TRAKTTV"
            Return nSeason
        End Function

        ''' <summary>
        '''  Scrape TV Epsiode details from trakttv
        ''' </summary>
        ''' <param name="SeasonNumber">Number of season to scrape</param>
        ''' <param name="EpisodeNumber">Number of episode to scrape</param>
        ''' <param name="FilteredOptions">Module settings<param>
        ''' <returns>Season details</returns>
        ''' <remarks>
        ''' 2015/11/18 Cocotus - First implementation
        ''' IMDBID/TraktID of tv show to be scraped is needed!
        ''' </remarks>
        Public Function GetTVEpisodeInfo(ByVal SeasonNumber As Integer, ByVal EpisodeNumber As Integer, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
            If String.IsNullOrEmpty(_TrakttvDBElementIMDBID) AndAlso String.IsNullOrEmpty(_TrakttvDBElementTRAKTID) Then Return Nothing
            Dim nEpisode As New MediaContainers.EpisodeDetails

            nEpisode.Scrapersource = "TRAKTTV"
            nEpisode.IMDB = _TrakttvDBElementIMDBID
            nEpisode.TMDB = _TrakttvDBElementTMDBID
            nEpisode.Episode = EpisodeNumber
            nEpisode.Season = SeasonNumber

            If String.IsNullOrEmpty(_Token) Then
                logger.Error(String.Concat("[GetTVEpisodeInfo] Can't login to trakt.tv account! Current showID: " & If(String.IsNullOrEmpty(_TrakttvDBElementTRAKTID), _TrakttvDBElementIMDBID, _TrakttvDBElementTRAKTID)))
                Return Nothing
            End If

            'Rating / Votes
            If FilteredOptions.bEpisodeRating Then
                If Not String.IsNullOrEmpty(_Token) Then
                    'scrape community rating and votes
                    Dim traktrating As TraktAPI.Model.TraktRating = TrakttvAPI.GetEpisodeRating(If(String.IsNullOrEmpty(_TrakttvDBElementTRAKTID), _TrakttvDBElementIMDBID, _TrakttvDBElementTRAKTID), SeasonNumber, EpisodeNumber)
                    If Not traktrating Is Nothing AndAlso Not traktrating.Rating Is Nothing AndAlso Not traktrating.Votes Is Nothing Then
                        nEpisode.Rating = CStr(Math.Round(traktrating.Rating.Value, 1)) ' traktrating.Rating.ToString
                        nEpisode.Votes = traktrating.Votes.ToString
                    Else
                        logger.Info("[GetTVEpisodeInfo] Could not scrape community rating/votes from trakt.tv! Current showID: " & If(String.IsNullOrEmpty(_TrakttvDBElementTRAKTID), _TrakttvDBElementIMDBID, _TrakttvDBElementTRAKTID))
                    End If

                    'instead of community rating use personal rating if available?
                    If _SpecialSettings.UsePersonalRatings Then
                        If _traktRatedEpisodes Is Nothing = False Then
                            For Each ratedEpisode As TraktAPI.Model.TraktEpisodeRated In _traktRatedEpisodes
                                If Not ratedEpisode Is Nothing AndAlso Not ratedEpisode.Show Is Nothing AndAlso Not ratedEpisode.Show.Ids Is Nothing AndAlso Not ratedEpisode.Episode Is Nothing Then
                                    'Check if information is stored...
                                    If (ratedEpisode.Show.Ids.Imdb.ToString = _TrakttvDBElementIMDBID OrElse ratedEpisode.Show.Ids.Trakt.ToString = _TrakttvDBElementTRAKTID) AndAlso ratedEpisode.Episode.Number = EpisodeNumber AndAlso ratedEpisode.Episode.Season = SeasonNumber Then
                                        nEpisode.Rating = ratedEpisode.Rating.ToString
                                        Exit For
                                    End If
                                End If
                            Next
                        Else
                            logger.Info("[GetTVEpisodeInfo] No ratings of episodes scraped from trakt.tv! Current showID: ", If(String.IsNullOrEmpty(_TrakttvDBElementTRAKTID), _TrakttvDBElementIMDBID, _TrakttvDBElementTRAKTID))
                        End If
                    End If
                Else
                    Return Nothing
                End If
            End If
            Return nEpisode
        End Function

        ''' <summary>
        '''  Scrape IDs(IDs of TVDB TMDB IMDB TraktID and TVRage) for item(movie/episode/show)
        ''' </summary>
        ''' <param name="ID">TVDBID/IMDBID/TMDBID/TraktID of item</param>
        ''' <param name="IDType">Type of ID to lookup. Possible values: trakt-movie , trakt-show , trakt-episode , imdb , tmdb , tvdb , tvrage</param>
        ''' <returns>SearchResult-Container which contains Ids of TVDB TMDB IMDB TraktID and TVRage</returns>
        ''' <remarks>
        ''' 2016/02/02 Cocotus - First implementation
        ''' </remarks>
        Public Function GetIDs(ByVal ID As String, ByVal IDType As String) As TraktAPI.Model.TraktSearchResult
            If Not String.IsNullOrEmpty(ID) Then
                If String.IsNullOrEmpty(_Token) Then
                    logger.Error(String.Concat("[GetIDs] Can't login to trakt.tv account! Current ID: ", ID))
                    Return Nothing
                End If
                'search on trakt.tv
                Dim traktsearchresult = TrakttvAPI.SearchById(IDType, ID)
                If Not traktsearchresult Is Nothing AndAlso Not traktsearchresult.Count = 1 Then
                    Return traktsearchresult(0)
                Else
                    logger.Info("[GetIDs] Could not scrape TraktID from trakt.tv! ID: ", ID)
                End If
            End If
            Return Nothing
        End Function


#End Region 'Methods

#Region "Nested Types"
#End Region 'Nested Types

    End Class

End Namespace