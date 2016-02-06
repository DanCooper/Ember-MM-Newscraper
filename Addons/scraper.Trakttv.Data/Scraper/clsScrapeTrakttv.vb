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
        Private _Token As String
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
        ''' <param name="Scrapermode">0= movie scraper, 1= tv scraper</param>
        ''' <remarks>
        ''' 2015/11/18 Cocotus - First implementation
        ''' Constructor for trakt.tv data scraper - use this to do things that only needs to be done one time
        ''' Connect to trakt.tv and scrape personal video data (personal ratings, last played) only once to minimize amount of queries to trakt.tv server
        ''' </remarks>
        Public Sub New(ByVal SpecialSettings As Trakttv_Data.SpecialSettings, ByVal Scrapermode As Byte)
            Try
                _SpecialSettings = SpecialSettings

                _Token = TraktMethods.LoginToTrakt(_SpecialSettings.TrakttvUserName, _SpecialSettings.TrakttvPassword)
                If String.IsNullOrEmpty(_Token) Then
                    logger.Error(String.Concat("[New] Can't login to trakt.tv account!"))
                Else
                    'Movie Mode
                    If Scrapermode = 0 Then
                        'Retrieve at scraper startup all user video data on trakt.tv like ratings (only need to do this once and NOT for every scraped movie/show)
                        If _SpecialSettings.UsePersonalRatings Then
                            _traktRatedMovies = TrakttvAPI.GetRatedMovies
                            If _traktRatedMovies Is Nothing Then
                                logger.Error(String.Concat("[New] Could not scrape personal trakt.tv ratings!"))
                            End If
                        End If
                        'TV Mode
                    ElseIf Scrapermode = 1 Then
                        'Retrieve at scraper startup all user video data on trakt.tv like ratings (only need to do this once and NOT for every scraped movie/show)
                        If _SpecialSettings.UsePersonalRatings Then
                            _traktRatedEpisodes = TrakttvAPI.GetRatedEpisodes
                            If _traktRatedEpisodes Is Nothing Then
                                logger.Error(String.Concat("[New] Could not scrape personal trakt.tv ratings!"))
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        ''' <summary>
        '''  Scrape MovieDetails from trakttv
        ''' </summary>
        ''' <param name="strID">TMDBID or IMDBID (IMDB ID starts with "tt") of movie to be scraped</param>
        ''' <param name="FilteredOptions">Module settings<param>
        ''' <param name="IsSearch">Not used at moment</param>
        ''' <returns>True: success, false: no success</returns>
        ''' <remarks>
        ''' 2015/11/18 Cocotus - First implementation
        ''' For now only retrieve trakt.tv exklusive data like playcount, lastplayed, rating and votes
        ''' </remarks>
        Public Function GetMovieInfo(ByVal strID As String, ByVal FilteredOptions As Structures.ScrapeOptions, ByVal IsSearch As Boolean) As MediaContainers.Movie
            If String.IsNullOrEmpty(strID) OrElse strID.Length < 2 Then Return Nothing

            Dim nMovie As New MediaContainers.Movie
            Try
                nMovie.Scrapersource = "TRAKTTV"
                'check type of ID and set accordingly
                If strID.Substring(0, 2).ToLower = "tt" Then
                    'IMDBID
                    nMovie.IMDBID = strID
                Else
                    'TMDBID
                    nMovie.TMDBID = strID
                End If

                If String.IsNullOrEmpty(_Token) Then
                    logger.Error(String.Concat("[GetMovieInfo] Can't login to trakt.tv account! Current movie: ", strID))
                    Return Nothing
                End If

                'Rating / Votes
                If FilteredOptions.bMainRating Then
                    If Not String.IsNullOrEmpty(_Token) Then
                        'scrape community rating and votes
                        Dim traktrating As TraktAPI.Model.TraktRating = TrakttvAPI.GetMovieRating(strID)
                        If Not traktrating Is Nothing AndAlso Not traktrating.Rating Is Nothing AndAlso Not traktrating.Votes Is Nothing Then
                            nMovie.Rating = CStr(Math.Round(traktrating.Rating.Value, 1)) ' traktrating.Rating.ToString
                            nMovie.Votes = traktrating.Votes.ToString
                        Else
                            logger.Info("[GetMovieInfo] Could not scrape community rating/votes from trakt.tv! Current movie: " & strID)
                        End If
                        'instead of community rating use personal rating if available?
                        If _SpecialSettings.UsePersonalRatings Then
                            If _traktRatedMovies Is Nothing = False Then
                                For Each ratedMovie As TraktAPI.Model.TraktMovieRated In _traktRatedMovies
                                    If Not ratedMovie.Movie.Ids Is Nothing Then
                                        'Check if information is stored...
                                        If (Not String.IsNullOrEmpty(nMovie.IMDBID) AndAlso Not ratedMovie.Movie.Ids.Imdb Is Nothing AndAlso ratedMovie.Movie.Ids.Imdb = strID) OrElse (Not String.IsNullOrEmpty(nMovie.TMDBID) AndAlso Not ratedMovie.Movie.Ids.Tmdb Is Nothing AndAlso ratedMovie.Movie.Ids.Tmdb.ToString = strID) Then
                                            nMovie.Rating = CStr(ratedMovie.Rating)
                                            Exit For
                                        End If
                                    End If
                                Next
                            Else
                                logger.Info("[GetMovieInfo] No ratings of movies scraped from trakt.tv! Current movie: " & strID)
                            End If
                        End If
                    End If
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try

            Return nMovie
        End Function

        ''' <summary>
        '''  Scrape TV Show details from trakttv
        ''' </summary>
        ''' <param name="strID">IMDBID of tv show to be scraped</param>
        ''' <param name="FilteredOptions">Module settings<param>
        ''' <param name="ScrapeModifiers">More options - scrape episode/season infos?</param>
        ''' <returns>True: success, false: no success</returns>
        ''' <remarks>
        ''' 2015/11/18 Cocotus - First implementation
        ''' trakt.tv API supports ONLY IMDB
        ''' </remarks>
        Public Function GetTVShowInfo(ByVal strID As String, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByVal FilteredOptions As Structures.ScrapeOptions) As MediaContainers.TVShow
            If String.IsNullOrEmpty(strID) OrElse strID.Length < 2 Then Return Nothing

            Dim nTVShow As New MediaContainers.TVShow

            If bwTrakttv.CancellationPending Then Return Nothing

            'check type of ID and set accordingly
            If strID.Substring(0, 2).ToLower = "tt" Then
                'IMDBID
                nTVShow.IMDB = strID
            Else
                'TVDB? TMDB?
                'nShow.TVDB = strID
            End If

            nTVShow.Scrapersource = "TRAKTTV"

            'Rating
            If FilteredOptions.bMainRating Then
                Dim traktrating As TraktAPI.Model.TraktRating = TrakttvAPI.GetShowRating(strID)
                If Not traktrating Is Nothing AndAlso Not traktrating.Rating Is Nothing AndAlso Not traktrating.Votes Is Nothing Then
                    nTVShow.Rating = CStr(Math.Round(traktrating.Rating.Value, 1)) ' traktrating.Rating.ToString
                    nTVShow.Votes = traktrating.Votes.ToString
                Else
                    logger.Info("[GetTVShowInfo] Could not scrape community rating/votes from trakt.tv! Current showID: " & strID)
                End If
            End If

            If bwTrakttv.CancellationPending Then Return Nothing

            'Seasons and Episodes? No automatic scraping for now...
            'If ScrapeModifier.withEpisodes OrElse ScrapeModifier.withSeasons Then
            '    For Each aSeason As MediaContainers.SeasonDetails In nShow.Seasons.Seasons
            '        aSeason = GetTVSeasonInfo(strID, aSeason.Season, FilteredOptions)
            '    Next
            'End If

            Return nTVShow
        End Function

        ''' <summary>
        '''  Scrape TV Season details from trakttv
        ''' </summary>
        ''' <param name="strID">TVDBID ID of tv show to be scraped</param>
        ''' <param name="SeasonNumber">Number of season to scrape</param>
        ''' <param name="FilteredOptions">Module settings<param>
        ''' <returns>Season details</returns>
        ''' <remarks>
        ''' 2015/11/18 Cocotus - First implementation
        ''' Not used in moment, since theres no rating/votes field in season database!
        ''' </remarks>
        Public Function GetTVSeasonInfo(ByVal strID As String, ByVal SeasonNumber As Integer, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.SeasonDetails
            If String.IsNullOrEmpty(strID) OrElse strID.Length < 2 Then Return Nothing

            Dim nSeason As New MediaContainers.SeasonDetails
            nSeason.Scrapersource = "TRAKTTV"
            'check type of ID and set accordingly
            'TVDB?
            'nSeason.TVDB = strID
            nSeason.Season = SeasonNumber

            'Rating?
            'If FilteredOptions.bSeasonRating Then
            ''try to login to your trakt.tv account
            'Trakttv.TraktMethods.LoginToTrakt(clsAdvancedSettings.GetSetting("Username", ""), clsAdvancedSettings.GetSetting("Password", ""))
            'If String.IsNullOrEmpty(Trakttv.TraktSettings.Token) Then
            '    'EXIT
            'End If
            '    Dim traktrating As TraktAPI.Model.TraktRating = TrakttvAPI.GetSeasonRating(CStr(ShowID), SeasonNumber)
            '    If Not traktrating Is Nothing AndAlso Not traktrating.Rating Is Nothing AndAlso Not traktrating.Votes Is Nothing Then
            '        nSeason.Rating = traktrating.Rating.ToString
            '        nSeason.Votes = traktrating.Votes.ToString
            '    End If
            'End If

            Return nSeason
        End Function

        ''' <summary>
        '''  Scrape TV Epsiode details from trakttv
        ''' </summary>
        ''' <param name="strID">TVDBID ID of tv show to be scraped</param>
        ''' <param name="SeasonNumber">Number of season to scrape</param>
        ''' <param name="FilteredOptions">Module settings<param>
        ''' <returns>Season details</returns>
        ''' <remarks>
        ''' 2015/11/18 Cocotus - First implementation
        ''' Not used in moment, since theres no rating/votes field in season database!
        ''' </remarks>
        Public Function GetTVEpisodeInfo(ByVal ShowID As String, ByVal SeasonNumber As Integer, ByVal EpisodeNumber As Integer, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
            Dim nEpisode As New MediaContainers.EpisodeDetails

            nEpisode.Scrapersource = "TRAKTTV"
            'nEpisode.TVDB = ShowID
            nEpisode.Episode = EpisodeNumber
            nEpisode.Season = SeasonNumber

            If String.IsNullOrEmpty(_Token) Then
                logger.Error(String.Concat("[GetTVEpisodeInfo] Can't login to trakt.tv account! Current show: ", ShowID))
                Return Nothing
            End If

            'Rating / Votes
            If FilteredOptions.bEpisodeRating Then
                If Not String.IsNullOrEmpty(_Token) Then
                    'scrape community rating and votes
                    Dim traktrating As TraktAPI.Model.TraktRating = TrakttvAPI.GetEpisodeRating(ShowID, SeasonNumber, EpisodeNumber)
                    If Not traktrating Is Nothing AndAlso Not traktrating.Rating Is Nothing AndAlso Not traktrating.Votes Is Nothing Then
                        nEpisode.Rating = CStr(Math.Round(traktrating.Rating.Value, 1)) ' traktrating.Rating.ToString
                        nEpisode.Votes = traktrating.Votes.ToString
                    Else
                        logger.Info("[GetTVEpisodeInfo] Could not scrape community rating/votes from trakt.tv! Current show: ", ShowID)
                    End If

                    'instead of community rating use personal rating if available?
                    If _SpecialSettings.UsePersonalRatings Then
                        If _traktRatedEpisodes Is Nothing = False Then
                            For Each ratedEpisode As TraktAPI.Model.TraktEpisodeRated In _traktRatedEpisodes
                                If Not ratedEpisode Is Nothing AndAlso Not ratedEpisode.Show Is Nothing AndAlso Not ratedEpisode.Show.Ids Is Nothing AndAlso Not ratedEpisode.Episode Is Nothing Then
                                    'Check if information is stored...
                                    If (ratedEpisode.Show.Ids.Tvdb.ToString = ShowID OrElse ratedEpisode.Show.Ids.Tmdb.ToString = ShowID OrElse ratedEpisode.Show.Ids.Imdb.ToString = ShowID OrElse ratedEpisode.Show.Ids.Trakt.ToString = ShowID) AndAlso ratedEpisode.Episode.Number = EpisodeNumber AndAlso ratedEpisode.Episode.Season = SeasonNumber Then
                                        nEpisode.Rating = ratedEpisode.Rating.ToString
                                        Exit For
                                    End If
                                End If
                            Next
                        Else
                            logger.Info("[GetTVEpisodeInfo] No ratings of episodes scraped from trakt.tv! Current show: ", ShowID)
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