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
Imports HtmlAgilityPack
Imports NLog
Imports System.IO
Imports System.Text.RegularExpressions

Namespace IMDB

    Public Class SearchResults_Movie

#Region "Properties"

        Public Property ExactMatches() As New List(Of MediaContainers.Movie)

        Public Property PartialMatches() As New List(Of MediaContainers.Movie)

        Public Property PopularTitles() As New List(Of MediaContainers.Movie)

        Public Property TvTitles() As New List(Of MediaContainers.Movie)

        Public Property VideoTitles() As New List(Of MediaContainers.Movie)

        Public Property ShortTitles() As New List(Of MediaContainers.Movie)

#End Region 'Properties

    End Class

    Public Class SearchResults_TVShow

#Region "Properties"

        Public Property Matches() As New List(Of MediaContainers.TVShow)

#End Region 'Properties

    End Class

    Public Class Scraper

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Friend WithEvents bwIMDB As New ComponentModel.BackgroundWorker

        Private Const REGEX_Certifications As String = "<a href=""/search/title\?certificates=[^""]*"">([^<]*):([^<]*)</a>[^<]*(<i>([^<]*)</i>)?"
        Private Const REGEX_IMDBID As String = "tt\d\d\d\d\d\d\d"

        Private htmldPlotSummary As HtmlDocument = Nothing
        Private htmldReleaseInfo As HtmlDocument = Nothing
        Private strPosterURL As String = String.Empty

        Private _SpecialSettings As IMDB_Data.SpecialSettings

#End Region 'Fields

#Region "Enumerations"

        Private Enum SearchType
            Details = 0
            Movies = 1
            SearchDetails_Movie = 2
            SearchDetails_TVShow = 3
            TVShows = 4
        End Enum

#End Region 'Enumerations

#Region "Events"

        Public Event Exception(ByVal ex As Exception)

        Public Event SearchInfoDownloaded_Movie(ByVal sPoster As String, ByVal sInfo As MediaContainers.Movie)
        Public Event SearchInfoDownloaded_TV(ByVal sPoster As String, ByVal sInfo As MediaContainers.TVShow)

        Public Event SearchResultsDownloaded_Movie(ByVal mResults As SearchResults_Movie)
        Public Event SearchResultsDownloaded_TV(ByVal mResults As SearchResults_TVShow)

#End Region 'Events

#Region "Methods"

        Public Sub New(ByVal SpecialSettings As IMDB_Data.SpecialSettings)
            _SpecialSettings = SpecialSettings
        End Sub

        Public Sub CancelAsync()

            If bwIMDB.IsBusy Then
                bwIMDB.CancelAsync()
            End If

            While bwIMDB.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End Sub

        Private Function BuildMovieForSearchResults(ByVal imdbID As String, ByVal lev As Integer, ByVal title As String, ByVal year As String) As MediaContainers.Movie
            Dim iYear As Integer
            Integer.TryParse(year, iYear)
            Dim nMovie As New MediaContainers.Movie
            nMovie.UniqueIDs.IMDbId = imdbID
            nMovie.Lev = lev
            nMovie.Title = title
            nMovie.Year = iYear
            Return nMovie
        End Function

        Private Sub bwIMDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwIMDB.DoWork
            Dim Args As Arguments = DirectCast(e.Argument, Arguments)

            Select Case Args.Search
                Case SearchType.Movies
                    Dim r As SearchResults_Movie = SearchMovie(Args.Parameter, Args.Year)
                    e.Result = New Results With {.ResultType = SearchType.Movies, .Result = r}

                Case SearchType.TVShows
                    Dim r As SearchResults_TVShow = SearchTVShow(Args.Parameter)
                    e.Result = New Results With {.ResultType = SearchType.TVShows, .Result = r}

                Case SearchType.SearchDetails_Movie
                    Dim r As MediaContainers.Movie = GetMovieInfo(Args.Parameter, True, Args.Options_Movie)
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails_Movie, .Result = r}

                Case SearchType.SearchDetails_TVShow
                    Dim r As MediaContainers.TVShow = GetTVShowInfo(Args.Parameter, Args.ScrapeModifiers, Args.Options_TV, True)
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails_TVShow, .Result = r}
            End Select
        End Sub

        Private Sub bwIMDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwIMDB.RunWorkerCompleted
            Dim Res As Results = DirectCast(e.Result, Results)

            Select Case Res.ResultType
                Case SearchType.Movies
                    RaiseEvent SearchResultsDownloaded_Movie(DirectCast(Res.Result, SearchResults_Movie))

                Case SearchType.TVShows
                    RaiseEvent SearchResultsDownloaded_TV(DirectCast(Res.Result, SearchResults_TVShow))

                Case SearchType.SearchDetails_Movie
                    Dim movieInfo As MediaContainers.Movie = DirectCast(Res.Result, MediaContainers.Movie)
                    RaiseEvent SearchInfoDownloaded_Movie(strPosterURL, movieInfo)

                Case SearchType.SearchDetails_TVShow
                    Dim showInfo As MediaContainers.TVShow = DirectCast(Res.Result, MediaContainers.TVShow)
                    RaiseEvent SearchInfoDownloaded_TV(strPosterURL, showInfo)
            End Select
        End Sub

        Private Function FindYear(ByVal tmpname As String, ByVal movies As List(Of MediaContainers.Movie)) As Integer
            Dim tmpyear As String = String.Empty
            Dim i As Integer
            Dim ret As Integer = -1
            tmpname = Path.GetFileNameWithoutExtension(tmpname)
            tmpname = tmpname.Replace(".", " ").Trim.Replace("(", " ").Replace(")", "").Trim
            i = tmpname.LastIndexOf(" ")
            If i >= 0 Then
                tmpyear = tmpname.Substring(i + 1, tmpname.Length - i - 1)
                If Integer.TryParse(tmpyear, 0) AndAlso Convert.ToInt32(tmpyear) > 1950 Then 'let's assume there are no movies older then 1950
                    For c = 0 To movies.Count - 1
                        Dim iYear As Integer
                        If Integer.TryParse(tmpyear, iYear) AndAlso movies(c).Year = iYear Then
                            ret = c
                            Exit For
                        End If
                    Next
                End If
            End If
            Return ret
        End Function
        ''' <summary>
        '''  Scrape MovieDetails from IMDB
        ''' </summary>
        ''' <param name="id">IMDBID of movie to be scraped</param>
        ''' <param name="FullCrew">Module setting: Scrape full cast?</param>
        ''' <param name="getposter">Scrape posters for the movie?</param>
        ''' <param name="Options">Module settings<param>
        ''' <param name="IsSearch">Not used at moment</param>
        ''' <returns>True: success, false: no success</returns>
        ''' <remarks></remarks>
        Public Function GetMovieInfo(ByVal id As String, ByVal getposter As Boolean, ByVal filteredoptions As Structures.ScrapeOptions) As MediaContainers.Movie
            If String.IsNullOrEmpty(id) Then Return Nothing

            Try
                If bwIMDB.CancellationPending Then Return Nothing

                Dim bIsScraperLanguage As Boolean = _SpecialSettings.PrefLanguage.ToLower.StartsWith("en")

                strPosterURL = String.Empty
                Dim nMovie As New MediaContainers.Movie With {.Scrapersource = "IMDB"}

                'ID
                nMovie.UniqueIDs.IMDbId = id

                'reset all local objects
                htmldPlotSummary = Nothing
                htmldReleaseInfo = Nothing

                Dim webParsing As New HtmlWeb
                Dim htmldReference As HtmlDocument = webParsing.Load(String.Concat("http://www.imdb.com/title/", id, "/reference"))

                If bwIMDB.CancellationPending Then Return Nothing

                'get clean OriginalTitle
                Dim strOriginalTitle As String = String.Empty
                Dim ndOriginalTitle = htmldReference.DocumentNode.SelectSingleNode("//h3[@itemprop=""name""]/text()")
                Dim ndOriginalTitleLbl = htmldReference.DocumentNode.SelectSingleNode("//span[@class=""titlereference-original-title-label""]")

                'first check if Original Title is country localized
                If ndOriginalTitleLbl IsNot Nothing Then
                    'get text before span object for Original Title
                    strOriginalTitle = HttpUtility.HtmlDecode(ndOriginalTitleLbl.PreviousSibling.InnerText.Trim)
                ElseIf ndOriginalTitle IsNot Nothing Then
                    'remove year in brakets
                    strOriginalTitle = HttpUtility.HtmlDecode(ndOriginalTitle.InnerText.Trim)
                Else
                    logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Originaltitle", id))
                End If

                'Actors
                If filteredoptions.bMainActors Then
                    Dim nActors = ParseActors(htmldReference)
                    If nActors IsNot Nothing Then
                        nMovie.Actors = nActors
                    Else
                        logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Actors", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Certifications
                If filteredoptions.bMainCertifications Then
                    Dim lstCertifications = ParseCertifications(htmldReference)
                    If lstCertifications IsNot Nothing Then
                        nMovie.Certifications = lstCertifications
                    Else
                        logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Certifications", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Countries
                If filteredoptions.bMainCountries Then
                    Dim lstCountries = ParseCountries(htmldReference)
                    If lstCountries IsNot Nothing Then
                        nMovie.Countries = lstCountries
                    Else
                        logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Countries", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Director
                If filteredoptions.bMainDirectors Then
                    Dim lstDirectors = ParseDirectors(htmldReference)
                    If lstDirectors IsNot Nothing Then
                        nMovie.Directors = lstDirectors
                    Else
                        logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Directors", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Duration
                If filteredoptions.bMainRuntime Then
                    Dim strDuration = ParseDuration(htmldReference)
                    If strDuration IsNot Nothing Then
                        nMovie.Runtime = strDuration
                    Else
                        logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Runtime", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Genres
                If filteredoptions.bMainGenres Then
                    Dim lstGenres = ParseGenres(htmldReference)
                    If lstGenres IsNot Nothing Then
                        nMovie.Genres = lstGenres
                    Else
                        logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Genres", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'MPAA
                If filteredoptions.bMainMPAA Then
                    Dim strMPAA = ParseMPAA(htmldReference, id)
                    If id IsNot Nothing Then
                        nMovie.MPAA = strMPAA
                    Else
                        logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse MPAA", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Original Title
                If filteredoptions.bMainOriginalTitle Then
                    nMovie.OriginalTitle = strOriginalTitle
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Outline
                If filteredoptions.bMainOutline AndAlso bIsScraperLanguage Then
                    Dim strOutline = ParseOutline(htmldReference, id)
                    If strOutline IsNot Nothing Then
                        nMovie.Outline = strOutline
                    Else
                        logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Outline", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Plot
                If filteredoptions.bMainPlot AndAlso bIsScraperLanguage Then
                    Dim strPlot = ParsePlot(htmldReference)
                    If strPlot IsNot Nothing Then
                        nMovie.Plot = strPlot
                    Else
                        'if "plot" isn't available then the "outline" will be used as plot
                        If nMovie.OutlineSpecified Then
                            nMovie.Plot = nMovie.Outline
                        Else
                            strPlot = ParsePlotFromSummaryPage(id)
                            If Not String.IsNullOrEmpty(strPlot) Then
                                nMovie.Plot = strPlot
                            Else
                                logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] no result from ""plotsummary"" page for Plot", id))
                            End If
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Poster for search result
                If getposter Then
                    ParsePosterURL(htmldReference)
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Rating
                If filteredoptions.bMainRating Then
                    Dim nRating = ParseRating(htmldReference)
                    If nRating IsNot Nothing Then
                        Dim dblRating As Double
                        Dim iVotes As Integer
                        If Double.TryParse(nRating.strRating, dblRating) AndAlso Integer.TryParse(NumUtils.CleanVotes(nRating.strVotes), iVotes) Then
                            nMovie.Ratings.Add(New MediaContainers.RatingDetails With {.Max = 10, .Name = "imdb", .Value = dblRating, .Votes = iVotes})
                        End If
                    Else
                        logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Rating", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'ReleaseDate / Year
                If filteredoptions.bMainRelease OrElse filteredoptions.bMainYear Then
                    Dim dateRelease As New Date
                    If ParseReleaseDate(htmldReference, dateRelease) Then
                        If filteredoptions.bMainRelease Then nMovie.ReleaseDate = dateRelease.ToString("yyyy-MM-dd")
                        If filteredoptions.bMainYear Then nMovie.Year = dateRelease.Year
                    Else
                        logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse ReleaseDate/Year", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Studios
                If filteredoptions.bMainStudios Then
                    Dim lstStudios = ParseStudios(htmldReference)
                    If lstStudios IsNot Nothing Then
                        nMovie.Studios = lstStudios
                    Else
                        logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Studios", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Tagline
                If filteredoptions.bMainTagline AndAlso bIsScraperLanguage Then
                    Dim strTagline = ParseTagline(htmldReference)
                    If strTagline IsNot Nothing Then
                        nMovie.Tagline = strTagline
                    Else
                        logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Tagline", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Title
                If filteredoptions.bMainTitle Then
                    If Not String.IsNullOrEmpty(_SpecialSettings.ForceTitleLanguage) Then
                        nMovie.Title = ParseForcedTitle(id, strOriginalTitle)
                    Else
                        nMovie.Title = strOriginalTitle
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Top250
                If filteredoptions.bMainTop250 Then
                    Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//a[@href=""/chart/top""]")
                    If selNode IsNot Nothing Then
                        Dim strTop250 As String = Regex.Match(selNode.InnerText.Trim, "#([0-9]+)").Groups(1).Value
                        Dim iTop250 As Integer = 0
                        If Integer.TryParse(strTop250, iTop250) Then
                            nMovie.Top250 = iTop250
                        Else
                            logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Top250", id))
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Trailer
                If filteredoptions.bMainTrailer Then
                    'Get first IMDB trailer if possible
                    Dim TrailerList As List(Of MediaContainers.Trailer) = EmberAPI.IMDb.Scraper.GetMovieTrailersByIMDBID(nMovie.UniqueIDs.IMDbId)
                    If TrailerList.Count > 0 Then
                        Dim sIMDb As New EmberAPI.IMDb.Scraper
                        sIMDb.GetVideoLinks(TrailerList.Item(0).URLWebsite)
                        If sIMDb.VideoLinks.Count > 0 Then
                            nMovie.Trailer = sIMDb.VideoLinks.FirstOrDefault().Value.URL.ToString
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Writers
                If filteredoptions.bMainWriters Then
                    Dim lstCredits = ParseCredits(htmldReference)
                    If lstCredits IsNot Nothing Then
                        nMovie.Credits = lstCredits
                    Else
                        logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Writers (Credits)", id))
                    End If
                End If

                'Year (fallback if ReleaseDate can't be parsed)
                If filteredoptions.bMainYear AndAlso Not nMovie.YearSpecified Then
                    Dim iYear As Integer
                    Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//span[@class=""titlereference-title-year""]/a")
                    If selNode IsNot Nothing AndAlso Integer.TryParse(selNode.InnerText, iYear) Then
                        nMovie.Year = iYear
                    Else
                        logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Year (fallback)", id))
                    End If
                End If

                Return nMovie
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function

        Public Function GetTVEpisodeInfo(ByVal id As String, ByRef filteredoptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
            If String.IsNullOrEmpty(id) Then Return Nothing

            Try
                If String.IsNullOrEmpty(id) Then Return Nothing

                Dim bIsScraperLanguage As Boolean = _SpecialSettings.PrefLanguage.ToLower.StartsWith("en")

                'reset all local objects
                htmldPlotSummary = Nothing
                htmldReleaseInfo = Nothing

                Dim webParsingSeasons As New HtmlWeb
                Dim htmldReference As HtmlDocument = webParsingSeasons.Load(String.Concat("http://www.imdb.com/title/", id, "/reference"))

                If bwIMDB.CancellationPending Then Return Nothing

                strPosterURL = String.Empty
                Dim nTVEpisode As New MediaContainers.EpisodeDetails With {.Scrapersource = "IMDB"}
                nTVEpisode.UniqueIDs.IMDbId = id

                'get season and episode number
                Dim selSENode = htmldReference.DocumentNode.SelectSingleNode("//ul[@class=""ipl-inline-list titlereference-overview-season-episode-numbers""]")
                If selSENode IsNot Nothing Then
                    Dim rSeasonEpisode As Match = Regex.Match(selSENode.InnerText,
                                                              "season (?<SEASON>\d+).*?episode (?<EPISODE>\d+)",
                                                              RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                    If Not rSeasonEpisode.Success Then Return Nothing

                    'Episode # Standard
                    nTVEpisode.Episode = CInt(rSeasonEpisode.Groups("EPISODE").Value)

                    'Season # Standard
                    nTVEpisode.Season = CInt(rSeasonEpisode.Groups("SEASON").Value)
                Else
                    Return Nothing
                End If

                'get clean OriginalTitle
                Dim strOriginalTitle As String = String.Empty
                Dim ndOriginalTitle = htmldReference.DocumentNode.SelectSingleNode("//h3[@itemprop=""name""]/text()")
                Dim ndOriginalTitleLbl = htmldReference.DocumentNode.SelectSingleNode("//span[@class=""titlereference-original-title-label""]")

                'first check if Original Title is country localized
                If ndOriginalTitleLbl IsNot Nothing Then
                    'get text before span object for Original Title
                    strOriginalTitle = HttpUtility.HtmlDecode(ndOriginalTitleLbl.PreviousSibling.InnerText.Trim)
                ElseIf ndOriginalTitle IsNot Nothing Then
                    'remove year in brakets
                    strOriginalTitle = HttpUtility.HtmlDecode(ndOriginalTitle.InnerText.Trim)
                Else
                    logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Originaltitle", id))
                End If

                'Actors
                If filteredoptions.bEpisodeActors Then
                    Dim lstActors = ParseActors(htmldReference, True)
                    If lstActors IsNot Nothing Then
                        nTVEpisode.Actors = lstActors
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Actors", id))
                    End If
                End If

                'AiredDate
                If filteredoptions.bEpisodeAired Then
                    Dim dateRelease As New Date
                    If ParseReleaseDate(htmldReference, dateRelease) Then
                        nTVEpisode.Aired = dateRelease.ToString("yyyy-MM-dd")
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse AiredDate", id))
                    End If
                End If

                'Credits (writers)
                If filteredoptions.bEpisodeCredits Then
                    Dim lstCredits = ParseCredits(htmldReference)
                    If lstCredits IsNot Nothing Then
                        nTVEpisode.Credits = lstCredits
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Credits (Writers)", id))
                    End If
                End If

                'Directors
                If filteredoptions.bEpisodeDirectors Then
                    Dim lstDirectors = ParseDirectors(htmldReference)
                    If lstDirectors IsNot Nothing Then
                        nTVEpisode.Directors = lstDirectors
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Directors", id))
                    End If
                End If

                'Plot
                If filteredoptions.bEpisodePlot AndAlso bIsScraperLanguage Then
                    Dim selNodes = htmldReference.DocumentNode.SelectNodes("//section[@class=""titlereference-section-overview""]/div[2]/text()")
                    If selNodes IsNot Nothing Then
                        Dim selNode = selNodes.FirstOrDefault
                        If selNode IsNot Nothing AndAlso selNode.InnerText IsNot Nothing Then
                            'get the outline, even if only a part of the outline is available
                            nTVEpisode.Plot = HttpUtility.HtmlDecode(selNode.InnerText.Trim)
                            'remove the three dots to search the same text on the "plotsummary" page
                            Dim strPlot As String = Regex.Replace(nTVEpisode.Plot, "\.\.\.", String.Empty)
                            If selNode.NextSibling IsNot Nothing AndAlso selNode.NextSibling.InnerText.Trim.ToLower.StartsWith("see more") Then
                                'parse the "plotsummary" page for full outline text
                                strPlot = ParsePlotFromSummaryPage(id, strPlot)
                                If Not String.IsNullOrEmpty(strPlot) Then
                                    nTVEpisode.Plot = strPlot
                                Else
                                    logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] no result from ""plotsummary"" page for Plot", id))
                                End If
                            End If
                        Else
                            logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Plot", id))
                        End If
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Plot", id))
                    End If
                End If

                'Rating
                If filteredoptions.bEpisodeRating Then
                    Dim nRating = ParseRating(htmldReference)
                    If nRating IsNot Nothing Then
                        Dim dblRating As Double
                        Dim iVotes As Integer
                        If Double.TryParse(nRating.strRating, dblRating) AndAlso Integer.TryParse(NumUtils.CleanVotes(nRating.strVotes), iVotes) Then
                            nTVEpisode.Ratings.Add(New MediaContainers.RatingDetails With {.Max = 10, .Name = "imdb", .Value = dblRating, .Votes = iVotes})
                        End If
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Rating", id))
                    End If
                End If

                'Title
                If filteredoptions.bEpisodeTitle Then
                    If Not String.IsNullOrEmpty(_SpecialSettings.ForceTitleLanguage) Then
                        nTVEpisode.Title = ParseForcedTitle(id, strOriginalTitle)
                    Else
                        nTVEpisode.Title = strOriginalTitle
                    End If
                End If

                Return nTVEpisode
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function

        Public Function GetTVEpisodeInfo(ByVal showid As String, ByVal season As Integer, ByVal episode As Integer, ByRef filteredoptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
            If String.IsNullOrEmpty(showid) OrElse season = -1 OrElse episode = -1 Then Return Nothing

            Dim webParsing As New HtmlWeb
            Dim webParsingSeasons As New HtmlWeb
            Dim htmldEpisodes As HtmlDocument = webParsingSeasons.Load(String.Concat("http://www.imdb.com/title/", showid, "/episodes?season=", season))

            Dim selNode = htmldEpisodes.DocumentNode.SelectSingleNode("//div[@class=""list detail eplist""]")
            If selNode IsNot Nothing Then
                Dim ncEpisodes = selNode.Descendants("div").Where(Function(f) Regex.Match(f.InnerText, String.Format("S{0}, Ep{1}", season, episode)).Success)
                If ncEpisodes IsNot Nothing Then
                    For Each nNode In ncEpisodes
                        Dim strID = StringUtils.GetIMDBIDFromString(nNode.InnerHtml)
                        If Not String.IsNullOrEmpty(strID) Then
                            Dim nEpisode As MediaContainers.EpisodeDetails = GetTVEpisodeInfo(strID, filteredoptions)
                            If nEpisode IsNot Nothing Then
                                Return nEpisode
                            End If
                        End If
                    Next
                End If
            End If

            Return Nothing
        End Function

        Public Sub GetTVSeasonInfo(ByRef nTVShow As MediaContainers.TVShow, ByVal showid As String, ByVal season As Integer, ByRef scrapemodifiers As Structures.ScrapeModifiers, ByRef filteredoptions As Structures.ScrapeOptions)
            Try
                If scrapemodifiers.withEpisodes Then
                    Dim webParsingSeasons As New HtmlWeb
                    Dim htmldEpisodes As HtmlDocument = webParsingSeasons.Load(String.Concat("http://www.imdb.com/title/", showid, "/episodes?season=", season))

                    Dim selNode = htmldEpisodes.DocumentNode.SelectSingleNode("//div[@class=""list detail eplist""]")
                    If selNode IsNot Nothing Then
                        Dim ncEpisodes = selNode.ChildNodes.Where(Function(f) f.Name = "div")
                        If ncEpisodes IsNot Nothing Then
                            For Each nodeEpisode In ncEpisodes
                                Dim strIMDBID As String = StringUtils.GetIMDBIDFromString(nodeEpisode.InnerHtml)
                                Dim nEpisode As MediaContainers.EpisodeDetails = GetTVEpisodeInfo(strIMDBID, filteredoptions)
                                If nEpisode IsNot Nothing Then
                                    nTVShow.KnownEpisodes.Add(nEpisode)
                                End If
                            Next
                        End If
                    End If
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub
        ''' <summary>
        '''  Scrape TV Show details from IMDB
        ''' </summary>
        ''' <param name="id">IMDB ID of tv show to be scraped</param>
        ''' <param name="getposter">Scrape posters for the tv show?</param> 
        ''' <returns>True: success, false: no success</returns>
        Public Function GetTVShowInfo(ByVal id As String, ByVal scrapemodifier As Structures.ScrapeModifiers, ByVal filteredoptions As Structures.ScrapeOptions, ByVal getposter As Boolean) As MediaContainers.TVShow
            If String.IsNullOrEmpty(id) Then Return Nothing

            Try
                If bwIMDB.CancellationPending Then Return Nothing

                Dim bIsScraperLanguage As Boolean = _SpecialSettings.PrefLanguage.ToLower.StartsWith("en")

                strPosterURL = String.Empty
                Dim nTVShow As New MediaContainers.TVShow With {.Scrapersource = "IMDB"}
                nTVShow.UniqueIDs.IMDbId = id

                'reset all local objects
                htmldPlotSummary = Nothing
                htmldReleaseInfo = Nothing

                Dim webParsing As New HtmlWeb
                Dim htmldReference As HtmlDocument = webParsing.Load(String.Concat("http://www.imdb.com/title/", id, "/reference"))

                If bwIMDB.CancellationPending Then Return Nothing

                'get clean OriginalTitle
                Dim strOriginalTitle As String = String.Empty
                Dim ndOriginalTitle = htmldReference.DocumentNode.SelectSingleNode("//h3[@itemprop=""name""]/text()")
                Dim ndOriginalTitleLbl = htmldReference.DocumentNode.SelectSingleNode("//span[@class=""titlereference-original-title-label""]")

                'first check if Original Title is country localized
                If ndOriginalTitleLbl IsNot Nothing Then
                    'get text before span object for Original Title
                    strOriginalTitle = HttpUtility.HtmlDecode(ndOriginalTitleLbl.PreviousSibling.InnerText.Trim)
                ElseIf ndOriginalTitle IsNot Nothing Then
                    'remove year in brakets
                    strOriginalTitle = HttpUtility.HtmlDecode(ndOriginalTitle.InnerText.Trim)
                Else
                    logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Originaltitle", id))
                End If

                'Actors
                If filteredoptions.bMainActors Then
                    Dim lstActors = ParseActors(htmldReference, True)
                    If lstActors IsNot Nothing Then
                        nTVShow.Actors = lstActors
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Actors", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Certifications
                If filteredoptions.bMainCertifications Then
                    Dim lstCertifications = ParseCertifications(htmldReference)
                    If lstCertifications IsNot Nothing Then
                        nTVShow.Certifications = lstCertifications
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Certifications", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Countries
                If filteredoptions.bMainCountries Then
                    Dim lstCountries = ParseCountries(htmldReference)
                    If lstCountries IsNot Nothing Then
                        nTVShow.Countries = lstCountries
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Countries", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Creators
                If filteredoptions.bMainCreators Then
                    Dim lstCreators = ParseCreators(htmldReference)
                    If lstCreators IsNot Nothing Then
                        nTVShow.Creators = lstCreators
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Creators", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Genres
                If filteredoptions.bMainGenres Then
                    Dim lstGenres = ParseGenres(htmldReference)
                    If lstGenres IsNot Nothing Then
                        nTVShow.Genres = lstGenres
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Genres", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Original Title
                If filteredoptions.bMainOriginalTitle Then
                    nTVShow.OriginalTitle = strOriginalTitle
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Plot
                If filteredoptions.bMainPlot AndAlso bIsScraperLanguage Then
                    Dim strPlot = ParsePlot(htmldReference)
                    If strPlot IsNot Nothing Then
                        nTVShow.Plot = strPlot
                    Else
                        strPlot = ParsePlotFromSummaryPage(id)
                        If Not String.IsNullOrEmpty(strPlot) Then
                            nTVShow.Plot = strPlot
                        Else
                            logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] no result from ""plotsummary"" page for Plot", id))
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Poster for search result
                If getposter Then
                    ParsePosterURL(htmldReference)
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Premiered
                If filteredoptions.bMainPremiered Then
                    Dim dateRelease As New Date
                    If ParsePremiered(id, dateRelease) Then
                        nTVShow.Premiered = dateRelease.ToString("yyyy-MM-dd")
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Premiered", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Rating
                If filteredoptions.bMainRating Then
                    Dim nRating As Rating = ParseRating(htmldReference)
                    If nRating IsNot Nothing Then
                        Dim dblRating As Double
                        Dim iVotes As Integer
                        If Double.TryParse(nRating.strRating, dblRating) AndAlso Integer.TryParse(NumUtils.CleanVotes(nRating.strVotes), iVotes) Then
                            nTVShow.Ratings.Add(New MediaContainers.RatingDetails With {.Max = 10, .Name = "imdb", .Value = dblRating, .Votes = iVotes})
                        End If
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Rating", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Runtime
                If filteredoptions.bMainRuntime Then
                    Dim strRuntime = ParseRuntime(htmldReference)
                    If strRuntime IsNot Nothing Then
                        nTVShow.Runtime = strRuntime
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Runtime", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Studios
                If filteredoptions.bMainStudios Then
                    Dim lstStudios = ParseStudios(htmldReference)
                    If lstStudios IsNot Nothing Then
                        nTVShow.Studios = lstStudios
                    Else
                        logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Studios", id))
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Title
                If filteredoptions.bMainTitle Then
                    If Not String.IsNullOrEmpty(_SpecialSettings.ForceTitleLanguage) Then
                        nTVShow.Title = ParseForcedTitle(id, strOriginalTitle)
                    Else
                        nTVShow.Title = strOriginalTitle
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Seasons and Episodes
                If scrapemodifier.withEpisodes OrElse scrapemodifier.withSeasons Then
                    Dim webParsingSeasons As New HtmlWeb
                    Dim htmldEpisodes As HtmlDocument = webParsingSeasons.Load(String.Concat("http://www.imdb.com/title/", id, "/episodes"))

                    'parse the "Season:" dropdown menu to get all season numbers
                    Dim lstSeasons As New List(Of Integer)
                    Dim selNodes = htmldEpisodes.DocumentNode.SelectNodes("//select[@id=""bySeason""]/option")
                    If selNodes IsNot Nothing Then
                        For Each nNode In selNodes
                            Dim iSeasonsNr As Integer = -1
                            If Integer.TryParse(nNode.Attributes("value").Value, iSeasonsNr) Then
                                lstSeasons.Add(iSeasonsNr)
                            End If
                        Next
                    End If

                    For Each tSeason In lstSeasons
                        If bwIMDB.CancellationPending Then Return Nothing
                        GetTVSeasonInfo(nTVShow, nTVShow.UniqueIDs.IMDbId, tSeason, scrapemodifier, filteredoptions)
                        If scrapemodifier.withSeasons Then
                            nTVShow.KnownSeasons.Add(New MediaContainers.SeasonDetails With {.Season = tSeason})
                        End If
                    Next
                End If

                Return nTVShow
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function

        Public Function GetMovieStudios(ByVal id As String) As List(Of String)
            Dim webParsingSeasons As New HtmlWeb
            Dim htmldReference As HtmlDocument = webParsingSeasons.Load(String.Concat("http://www.imdb.com/title/", id, "/reference"))
            If htmldReference IsNot Nothing Then
                Dim lstStudios = ParseStudios(htmldReference)
                If lstStudios IsNot Nothing Then
                    Return lstStudios
                End If
            End If
            Return New List(Of String)
        End Function

        Public Function GetSearchMovieInfo(ByVal title As String, ByVal year As Integer, ByRef oDBElement As Database.DBElement, ByVal scrapetype As Enums.ScrapeType, ByVal filteredoptions As Structures.ScrapeOptions) As MediaContainers.Movie
            Dim r As SearchResults_Movie = SearchMovie(title, year)

            Try
                Select Case scrapetype
                    Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                        If r.ExactMatches.Count = 1 Then
                            Return GetMovieInfo(r.ExactMatches.Item(0).UniqueIDs.IMDbId, False, filteredoptions)
                        ElseIf r.PopularTitles.Count = 1 AndAlso r.PopularTitles(0).Lev <= 5 Then
                            Return GetMovieInfo(r.PopularTitles.Item(0).UniqueIDs.IMDbId, False, filteredoptions)
                        ElseIf r.ExactMatches.Count = 1 AndAlso r.ExactMatches(0).Lev <= 5 Then
                            Return GetMovieInfo(r.ExactMatches.Item(0).UniqueIDs.IMDbId, False, filteredoptions)
                        Else
                            Using dlgSearch As New dlgIMDBSearchResults_Movie(_SpecialSettings, Me)
                                If dlgSearch.ShowDialog(r, title, oDBElement.FileItem.FirstPathFromStack) = DialogResult.OK Then
                                    If Not String.IsNullOrEmpty(dlgSearch.Result.UniqueIDs.IMDbId) Then
                                        Return GetMovieInfo(dlgSearch.Result.UniqueIDs.IMDbId, False, filteredoptions)
                                    End If
                                End If
                            End Using
                        End If

                    Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                        If r.ExactMatches.Count = 1 Then
                            Return GetMovieInfo(r.ExactMatches.Item(0).UniqueIDs.IMDbId, False, filteredoptions)
                        End If

                    Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                        'check if ALL results are over lev value
                        Dim useAnyway As Boolean = False
                        If ((r.PopularTitles.Count > 0 AndAlso r.PopularTitles(0).Lev > 5) OrElse r.PopularTitles.Count = 0) AndAlso
                        ((r.ExactMatches.Count > 0 AndAlso r.ExactMatches(0).Lev > 5) OrElse r.ExactMatches.Count = 0) AndAlso
                        ((r.PartialMatches.Count > 0 AndAlso r.PartialMatches(0).Lev > 5) OrElse r.PartialMatches.Count = 0) Then
                            useAnyway = True
                        End If
                        Dim exactHaveYear As Integer = FindYear(oDBElement.FileItem.FirstPathFromStack, r.ExactMatches)
                        Dim popularHaveYear As Integer = FindYear(oDBElement.FileItem.FirstPathFromStack, r.PopularTitles)
                        If r.ExactMatches.Count = 1 Then
                            Return GetMovieInfo(r.ExactMatches.Item(0).UniqueIDs.IMDbId, False, filteredoptions)
                        ElseIf r.ExactMatches.Count > 1 AndAlso exactHaveYear >= 0 Then
                            Return GetMovieInfo(r.ExactMatches.Item(exactHaveYear).UniqueIDs.IMDbId, False, filteredoptions)
                        ElseIf r.PopularTitles.Count > 0 AndAlso popularHaveYear >= 0 Then
                            Return GetMovieInfo(r.PopularTitles.Item(popularHaveYear).UniqueIDs.IMDbId, False, filteredoptions)
                        ElseIf r.ExactMatches.Count > 0 AndAlso (r.ExactMatches(0).Lev <= 5 OrElse useAnyway) Then
                            Return GetMovieInfo(r.ExactMatches.Item(0).UniqueIDs.IMDbId, False, filteredoptions)
                        ElseIf r.PopularTitles.Count > 0 AndAlso (r.PopularTitles(0).Lev <= 5 OrElse useAnyway) Then
                            Return GetMovieInfo(r.PopularTitles.Item(0).UniqueIDs.IMDbId, False, filteredoptions)
                        ElseIf r.PartialMatches.Count > 0 AndAlso (r.PartialMatches(0).Lev <= 5 OrElse useAnyway) Then
                            Return GetMovieInfo(r.PartialMatches.Item(0).UniqueIDs.IMDbId, False, filteredoptions)
                        End If
                End Select

                Return Nothing
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function

        Public Sub GetSearchMovieInfoAsync(ByVal imdbID As String, ByVal FilteredOptions As Structures.ScrapeOptions)
            Try
                If Not bwIMDB.IsBusy Then
                    bwIMDB.WorkerReportsProgress = False
                    bwIMDB.WorkerSupportsCancellation = True
                    bwIMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_Movie,
                                           .Parameter = imdbID, .Options_Movie = FilteredOptions})
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

        Public Function GetSearchTVShowInfo(ByVal title As String, ByRef oDBElement As Database.DBElement, ByVal scrapetype As Enums.ScrapeType, ByVal scrapemodifier As Structures.ScrapeModifiers, ByVal FilteredOptions As Structures.ScrapeOptions) As MediaContainers.TVShow
            Dim r As SearchResults_TVShow = SearchTVShow(title)

            Select Case scrapetype
                Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                    If r.Matches.Count = 1 Then
                        Return GetTVShowInfo(r.Matches.Item(0).UniqueIDs.IMDbId, scrapemodifier, FilteredOptions, False)
                    Else
                        Using dlgSearch As New dlgIMDBSearchResults_TV(_SpecialSettings, Me)
                            If dlgSearch.ShowDialog(r, title, oDBElement.ShowPath) = DialogResult.OK Then
                                If Not String.IsNullOrEmpty(dlgSearch.Result.UniqueIDs.IMDbId) Then
                                    Return GetTVShowInfo(dlgSearch.Result.UniqueIDs.IMDbId, scrapemodifier, FilteredOptions, False)
                                End If
                            End If
                        End Using
                    End If

                Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                    If r.Matches.Count = 1 Then
                        Return GetTVShowInfo(r.Matches.Item(0).UniqueIDs.IMDbId, scrapemodifier, FilteredOptions, False)
                    End If

                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                    If r.Matches.Count > 0 Then
                        Return GetTVShowInfo(r.Matches.Item(0).UniqueIDs.IMDbId, scrapemodifier, FilteredOptions, False)
                    End If
            End Select

            Return Nothing
        End Function

        Public Sub GetSearchTVShowInfoAsync(ByVal id As String, ByVal options As Structures.ScrapeOptions)
            Try
                If Not bwIMDB.IsBusy Then
                    bwIMDB.WorkerReportsProgress = False
                    bwIMDB.WorkerSupportsCancellation = True
                    bwIMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_TVShow,
                                           .Parameter = id, .Options_TV = options})
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

        Private Function ParseActors(ByRef htmldReference As HtmlDocument, Optional ByVal removeepisodecount As Boolean = False) As List(Of MediaContainers.Person)
            Dim nActors As New List(Of MediaContainers.Person)
            Dim strThumbsSize = AdvancedSettings.GetSetting("ActorThumbsSize", "SX1000_SY1000")
            Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//table[@class=""cast_list""]")
            If selNode IsNot Nothing Then
                Dim ncCast = selNode.Descendants("tr")
                If ncCast IsNot Nothing Then
                    For Each nCast In ncCast.Where(Function(f) _
                                                       f.Attributes IsNot Nothing AndAlso
                                                       f.Attributes.Where(Function(a) a.Name = "class" AndAlso a.Value = "odd" OrElse a.Value = "even").Any
                                                       )
                        Dim nActor As New MediaContainers.Person
                        'get actor thumb
                        Dim ndActorthumb = nCast.Descendants("img").FirstOrDefault
                        If ndActorthumb IsNot Nothing AndAlso
                        ndActorthumb.Attributes("loadlate") IsNot Nothing AndAlso
                        Not String.IsNullOrEmpty(ndActorthumb.Attributes("loadlate").Value) AndAlso
                        Not ndActorthumb.Attributes("loadlate").Value.Contains("no_image") AndAlso
                        Not ndActorthumb.Attributes("loadlate").Value.Contains("no_photo") AndAlso
                        Not ndActorthumb.Attributes("loadlate").Value.Contains("thumb") AndAlso
                        Not ndActorthumb.Attributes("loadlate").Value.Contains("addtiny") Then
                            Dim rToReplace = Regex.Match(ndActorthumb.Attributes("loadlate").Value, ".*\.(.+)\.jpg$")
                            If rToReplace.Success Then
                                nActor.Thumb.URLOriginal = ndActorthumb.Attributes("loadlate").Value.Replace(rToReplace.Groups(1).Value, strThumbsSize)
                            End If
                        End If
                        'get actor name
                        Dim ndName = nCast.Descendants("td").Where(Function(f) f.Attributes.Where(Function(a) a.Name = "itemprop" AndAlso a.Value = "actor").Any).FirstOrDefault
                        'get character name
                        Dim ndCharacter = nCast.Descendants("td").Where(Function(f) f.Attributes.Where(Function(a) a.Name = "class" AndAlso a.Value = "character").Any).FirstOrDefault
                        'character cleanup (removes whitespaces)
                        Dim strCharacter As String = Regex.Replace(HttpUtility.HtmlDecode(ndCharacter.InnerText.Trim), "\s{2,}", " ").Trim
                        'character cleanup (removes "/ ...")
                        strCharacter = Regex.Replace(strCharacter, "\/ \.\.\.", String.Empty).Trim
                        If removeepisodecount Then
                            'character cleanup (removes episode info like "(49 episodes, 2011-2017)")
                            strCharacter = Regex.Replace(strCharacter, "\(\d+.*\)", String.Empty).Trim
                        End If
                        If ndName IsNot Nothing AndAlso ndCharacter IsNot Nothing Then
                            nActor.Name = HttpUtility.HtmlDecode(ndName.InnerText.Trim)
                            nActor.Role = strCharacter
                            nActors.Add(nActor)
                        End If
                    Next
                    Return nActors
                End If
            End If
            Return Nothing
        End Function

        Private Function ParseCertifications(ByRef htmldReference As HtmlDocument) As List(Of String)
            Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='Certification']")
            If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
                Dim rCert As MatchCollection = Regex.Matches(selNode.ParentNode.InnerHtml, REGEX_Certifications)
                If rCert.Count > 0 Then
                    Dim Certs = From M In rCert Select N = String.Format("{0}:{1}", DirectCast(M, Match).Groups(1).ToString.Trim, DirectCast(M, Match).Groups(2).ToString.Trim) Order By N Ascending
                    Dim lstCertifications As New List(Of String)
                    For Each tCert In Certs
                        tCert = tCert.Replace("United Kingdom", "UK")
                        tCert = tCert.Replace("United States", "USA")
                        tCert = tCert.Replace("West", String.Empty)
                        lstCertifications.Add(HttpUtility.HtmlDecode(tCert).Trim)
                    Next
                    lstCertifications = lstCertifications.Distinct.ToList
                    lstCertifications.Sort()
                    Return lstCertifications
                End If
            End If
            Return Nothing
        End Function

        Private Function ParseCountries(ByRef htmldReference As HtmlDocument) As List(Of String)
            Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='Country']")
            If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
                Dim ncCountries = selNode.ParentNode.Descendants("a")
                If ncCountries IsNot Nothing Then
                    Dim lstCountries = ncCountries.Select(Function(f) HttpUtility.HtmlDecode(f.InnerText)).Distinct.ToList
                    If _SpecialSettings.CountryAbbreviation Then
                        For i As Integer = 0 To lstCountries.Count - 1
                            lstCountries(i) = lstCountries(i).Replace("United States", "USA")
                            lstCountries(i) = lstCountries(i).Replace("United States of America", "USA")
                            lstCountries(i) = lstCountries(i).Replace("United Kingdom", "UK")
                        Next
                    Else
                        For i As Integer = 0 To lstCountries.Count - 1
                            lstCountries(i) = lstCountries(i).Replace("United States", "United States of America")
                            lstCountries(i) = lstCountries(i).Replace("USA", "United States of America")
                            lstCountries(i) = lstCountries(i).Replace("UK", "United Kingdom")
                        Next
                    End If
                    Return lstCountries
                End If
            End If
            Return Nothing
        End Function

        Private Function ParseCreators(ByRef htmldReference As HtmlDocument) As List(Of String)
            Dim selNodes = htmldReference.DocumentNode.SelectNodes("//div[@class=""titlereference-overview-section""]")
            If selNodes IsNot Nothing Then
                Dim selNode = selNodes.Where(Function(f) f.InnerText.Trim.StartsWith("Creator")).FirstOrDefault
                If selNode IsNot Nothing Then
                    Dim nCreators = selNode.Descendants("a").Where(Function(f) Not f.InnerText.ToLower = "see more")
                    If nCreators IsNot Nothing Then
                        Return nCreators.Select(Function(f) HttpUtility.HtmlDecode(f.InnerText)).Distinct.ToList
                    End If
                End If
            End If
            Return Nothing
        End Function

        Private Function ParseCredits(ByRef htmldReference As HtmlDocument) As List(Of String)
            Dim selNodes = htmldReference.DocumentNode.SelectNodes("//div[@class=""titlereference-overview-section""]")
            If selNodes IsNot Nothing Then
                Dim selNode = selNodes.Where(Function(f) f.InnerText.Trim.StartsWith("Writer")).FirstOrDefault
                If selNode IsNot Nothing Then
                    Dim nCredits = selNode.Descendants("a").Where(Function(f) Not f.InnerText.ToLower = "see more")
                    If nCredits IsNot Nothing Then
                        Return nCredits.Select(Function(f) HttpUtility.HtmlDecode(f.InnerText.Trim)).Distinct.ToList.Where(Function(f) Not f.ToLower.StartsWith("see more")).ToList
                    End If
                End If
            End If
            Return Nothing
        End Function

        Private Function ParseDirectors(ByRef htmldReference As HtmlDocument) As List(Of String)
            Dim selNodes = htmldReference.DocumentNode.SelectNodes("//div[@class=""titlereference-overview-section""]")
            If selNodes IsNot Nothing Then
                Dim selNode = selNodes.Where(Function(f) f.InnerText.Trim.StartsWith("Director")).FirstOrDefault
                If selNode IsNot Nothing Then
                    Dim nDirectors = selNode.Descendants("a").Where(Function(f) Not f.InnerText.ToLower = "see more")
                    If nDirectors IsNot Nothing Then
                        Return nDirectors.Select(Function(f) HttpUtility.HtmlDecode(f.InnerText)).Distinct.ToList
                    End If
                End If
            End If
            Return Nothing
        End Function

        Private Function ParseDuration(ByRef htmldReference As HtmlDocument) As String
            Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='Runtime']")
            If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
                Dim nDuration = selNode.ParentNode.Descendants("td").Where(Function(f) Not f.InnerText = "Runtime").FirstOrDefault
                If nDuration IsNot Nothing Then
                    Return nDuration.InnerText.Trim
                End If
            End If
            Return Nothing
        End Function

        Private Function ParseForcedTitle(ByVal id As String, ByVal originaltitle As String) As String
            If htmldReleaseInfo Is Nothing Then
                Dim webParsing As New HtmlWeb
                htmldReleaseInfo = webParsing.Load(String.Concat("http://www.imdb.com/title/", id, "/releaseinfo"))
            End If

            If htmldReleaseInfo IsNot Nothing Then
                Dim selNodes = htmldReleaseInfo.DocumentNode.SelectNodes(String.Format("//table[@id=""akas""]/tr/td[contains(text(), '{0}')]", _SpecialSettings.ForceTitleLanguage))
                If selNodes IsNot Nothing Then
                    Dim filteredTitles = selNodes.Where(Function(f) Not HttpUtility.HtmlDecode(f.InnerText).ToLower.Contains("working title") AndAlso
                                                            Not HttpUtility.HtmlDecode(f.InnerText).ToLower.Contains("fake working title"))
                    If filteredTitles IsNot Nothing AndAlso filteredTitles.Count > 0 Then
                        Return HttpUtility.HtmlDecode(filteredTitles(0).NextSibling.NextSibling.InnerText)
                    End If
                End If
            End If

            'fallback
            If _SpecialSettings.FallBackWorldwide Then
                Dim selFallback = htmldReleaseInfo.DocumentNode.SelectNodes("//table[@id=""akas""]/tr/td")
                If selFallback IsNot Nothing Then
                    For Each nNode In selFallback
                        If HttpUtility.HtmlDecode(nNode.InnerText).ToLower.Contains("world-wide") OrElse
                            HttpUtility.HtmlDecode(nNode.InnerText).ToLower.Contains("english") Then
                            Return HttpUtility.HtmlDecode(nNode.NextSibling.NextSibling.InnerText)
                        End If
                    Next
                End If
            End If

            'return OriginalTitle if no forced and no fallback title has been found
            Return originaltitle
        End Function

        Private Function ParseGenres(ByRef htmldReference As HtmlDocument) As List(Of String)
            Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='Genres']")
            If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
                Dim ncGenres = selNode.ParentNode.Descendants("a").Where(Function(f) Not f.InnerText.ToLower = "see more")
                If ncGenres IsNot Nothing Then
                    Return ncGenres.Select(Function(f) HttpUtility.HtmlDecode(f.InnerText)).Distinct.ToList
                End If
            End If
            Return Nothing
        End Function

        Private Function ParseMPAA(ByRef htmldReference As HtmlDocument, id As String) As String
            'try to get the full MPAA from "Parents Guide" page
            If _SpecialSettings.MPAADescription Then
                Dim webParsing As New HtmlWeb
                Dim htmldParentsGuide As HtmlDocument = webParsing.Load(String.Concat("http://www.imdb.com/title/", id, "/parentalguide"))
                Dim selNode = htmldParentsGuide.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='MPAA']")
                If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
                    Dim ncMPAA = selNode.ParentNode.Descendants("td").Where(Function(f) Not f.InnerText = "MPAA").FirstOrDefault
                    If ncMPAA IsNot Nothing AndAlso Not String.IsNullOrEmpty(ncMPAA.InnerText.Trim) Then
                        Return ncMPAA.InnerText.Trim
                    End If
                End If
            End If
            If _SpecialSettings.MPAADescription Then logger.Trace(String.Format("[IMDB] [ParseMPAA] [ID:""{0}""] can't parse full MPAA, try to parse the short rating", id))
            'try to get short Rating from "reference" page
            Dim selNodeTitleHeader = htmldReference.DocumentNode.SelectSingleNode("//div[@class=""titlereference-header""]")
            If selNodeTitleHeader IsNot Nothing Then
                Dim selNode = selNodeTitleHeader.Descendants("li").FirstOrDefault
                If selNode IsNot Nothing AndAlso
                        selNode.InnerText.Trim = "G" OrElse
                        selNode.InnerText.Trim = "PG" OrElse
                        selNode.InnerText.Trim = "PG-13" OrElse
                        selNode.InnerText.Trim = "R" OrElse
                        selNode.InnerText.Trim = "NC-17" Then
                    Return String.Format("Rated {0}", HttpUtility.HtmlDecode(selNode.InnerText.Trim))
                End If
            End If
            Return Nothing
        End Function

        Private Function ParseOutline(ByRef htmldReference As HtmlDocument, id As String) As String
            Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//section[@class=""titlereference-section-overview""]/div/text()")
            If selNode IsNot Nothing AndAlso selNode.InnerText IsNot Nothing Then
                'get the outline, even if only a part of the outline is available
                Dim strReferenceOutline = HttpUtility.HtmlDecode(selNode.InnerText.Trim)
                'remove the three dots to search the same text on the "plotsummary" page
                Dim strOutlineForSearch As String = Regex.Replace(strReferenceOutline, "\.\.\.", String.Empty)
                If selNode.NextSibling IsNot Nothing AndAlso selNode.NextSibling.InnerText.Trim.ToLower.StartsWith("see more") Then
                    'parse the "plotsummary" page for full outline text
                    strOutlineForSearch = ParsePlotFromSummaryPage(id, strOutlineForSearch)
                    If Not String.IsNullOrEmpty(strOutlineForSearch) Then
                        Return strOutlineForSearch
                    Else
                        logger.Trace(String.Format("[IMDB] [ParseOutline] [ID:""{0}""] no result from ""plotsummary"" page for Outline", id))
                    End If
                End If
                If Not String.IsNullOrEmpty(strReferenceOutline) Then
                    Return strReferenceOutline
                End If
            End If
            Return Nothing
        End Function

        Private Function ParsePlot(ByRef htmldReference As HtmlDocument) As String
            Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='Plot Summary']")
            If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
                Dim ndPlot = selNode.ParentNode.Descendants("p").FirstOrDefault
                If ndPlot IsNot Nothing AndAlso ndPlot.FirstChild IsNot Nothing AndAlso Not String.IsNullOrEmpty(ndPlot.FirstChild.InnerText.Trim) Then
                    Return HttpUtility.HtmlDecode(ndPlot.FirstChild.InnerText.Trim)
                End If
            End If
            Return Nothing
        End Function

        Private Function ParsePlotFromSummaryPage(ByVal id As String, Optional ByVal outline As String = "") As String
            If htmldPlotSummary Is Nothing Then
                Dim webParsing As New HtmlWeb
                htmldPlotSummary = webParsing.Load(String.Concat("http://www.imdb.com/title/", id, "/plotsummary"))
            End If
            If htmldPlotSummary IsNot Nothing Then
                Dim selNode As HtmlNode = Nothing
                If Not String.IsNullOrEmpty(outline) Then
                    selNode = htmldPlotSummary.DocumentNode.SelectNodes("//p").Where(Function(f) f.InnerText IsNot Nothing AndAlso
                                                                                           HttpUtility.HtmlDecode(f.InnerText.Trim).StartsWith(outline)).FirstOrDefault
                Else
                    selNode = htmldPlotSummary.DocumentNode.SelectNodes("//ul[@id=""plot-summaries-content""]/li/p").FirstOrDefault
                End If
                If selNode IsNot Nothing Then
                    Return HttpUtility.HtmlDecode(selNode.InnerText.Trim)
                Else
                    logger.Trace(String.Format("[IMDB] [ParsePlotFromSummaryPage] [ID:""{0}""] no proper result from the ""plotsummary"" page for Outline", id))
                End If
            Else
                logger.Trace(String.Format("[IMDB] [ParsePlotFromSummaryPage] [ID:""{0}""] can't parse the ""plotsummary"" page", id))
            End If
            Return String.Empty
        End Function

        Private Sub ParsePosterURL(ByRef htmldReference As HtmlDocument)
            Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//img[@class=""titlereference-primary-image""]")
            If selNode IsNot Nothing Then
                Dim attSource = selNode.Attributes("src")
                If attSource IsNot Nothing Then
                    strPosterURL = attSource.Value
                End If
            End If
        End Sub

        Private Function ParseRating(ByRef htmldReference As HtmlDocument) As Rating
            Dim selNodeRating = htmldReference.DocumentNode.SelectSingleNode("//span[@class=""ipl-rating-star__rating""]")
            Dim selNodeVotes = htmldReference.DocumentNode.SelectSingleNode("//span[@class=""ipl-rating-star__total-votes""]")
            If selNodeRating IsNot Nothing AndAlso
                selNodeVotes IsNot Nothing Then
                Return New Rating With {
                    .strRating = selNodeRating.InnerText.Trim,
                    .strVotes = Regex.Match(selNodeVotes.InnerText.Trim, "[0-9,.]+").Value
                }
            End If
            Return Nothing
        End Function

        Private Function ParseRuntime(ByRef htmldReference As HtmlDocument) As String
            Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='Runtime']")
            If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
                Dim nDuration = selNode.ParentNode.Descendants("td").Where(Function(f) Not f.InnerText = "Runtime").FirstOrDefault
                If nDuration IsNot Nothing Then
                    Return nDuration.InnerText.Trim
                End If
            End If
            Return Nothing
        End Function

        Private Function ParseReleaseDate(ByRef htmldReference As HtmlDocument, ByRef releasedate As Date) As Boolean
            Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//div[@class=""titlereference-header""]")
            If selNode IsNot Nothing Then
                Dim test = selNode.Descendants("a").Where(Function(f) f.Attributes.Where(Function(a) a.Value.Contains("releaseinfo")).Any)
                Dim nReleaseNode = selNode.Descendants("a").Where(Function(f) f.Attributes.Where(Function(a) a.Value.Contains("releaseinfo")).Any).FirstOrDefault
                If nReleaseNode IsNot Nothing Then
                    'find a date like "17 Apr 2017"
                    Dim mRelDate = Regex.Match(
                        nReleaseNode.InnerText,
                        "\d+\s\w+\s\d\d\d\d"
                        )
                    If mRelDate.Success Then
                        If Date.TryParse(mRelDate.Value, releasedate) Then
                            Return True
                        End If
                    End If
                End If
            End If
            Return False
        End Function

        Private Function ParsePremiered(ByVal id As String, ByRef releasedate As Date) As Boolean
            If htmldReleaseInfo Is Nothing Then
                Dim webParsing As New HtmlWeb
                htmldReleaseInfo = webParsing.Load(String.Concat("http://www.imdb.com/title/", id, "/releaseinfo"))
            End If
            If htmldReleaseInfo IsNot Nothing Then
                'get all release dates from table
                Dim selNodes = htmldReleaseInfo.DocumentNode.SelectNodes("//td[@class=""release_date""]")
                If selNodes IsNot Nothing Then
                    Dim lstReleaseDates As New List(Of Date)
                    For Each ndReleaseDate In selNodes
                        Dim nDate As New Date
                        If Date.TryParse(ndReleaseDate.InnerText.Trim, nDate) Then
                            lstReleaseDates.Add(nDate)
                        End If
                    Next
                    'remove douplicates
                    lstReleaseDates = lstReleaseDates.Distinct.ToList
                    'sort list by date to get to oldest at the first position
                    lstReleaseDates.Sort()
                    If lstReleaseDates.Count > 0 Then
                        releasedate = lstReleaseDates.First
                        Return True
                    End If
                End If
            End If
            Return False
        End Function

        Private Function ParseStudios(ByRef htmldReference As HtmlDocument) As List(Of String)
            Dim lstStudios As New List(Of String)
            Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//h4[@class=""ipl-header__content ipl-list-title""][.='Production Companies']")
            If selNode IsNot Nothing Then
                lstStudios.AddRange(selNode.ParentNode.ParentNode.NextSibling.NextSibling.Descendants("a").Select(Function(f) HttpUtility.HtmlDecode(f.InnerText)).Distinct.ToList)
            End If

            If _SpecialSettings.StudiowithDistributors Then
                Dim selNodeD = htmldReference.DocumentNode.SelectSingleNode("//h4[@class=""ipl-header__content ipl-list-title""][.='Distributors']")
                If selNodeD IsNot Nothing Then
                    lstStudios.AddRange(selNodeD.ParentNode.ParentNode.NextSibling.NextSibling.Descendants("a").Select(Function(f) HttpUtility.HtmlDecode(f.InnerText)).Distinct.ToList)
                    lstStudios = lstStudios.Distinct.ToList
                End If
            End If

            If lstStudios.Count > 0 Then
                Return lstStudios
            Else
                Return Nothing
            End If
        End Function

        Private Function ParseTagline(ByRef htmldReference As HtmlDocument) As String
            Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='Taglines']")
            If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
                Dim ndTagline = selNode.ParentNode.Descendants("td").Where(Function(f) Not f.InnerText = "Taglines").FirstOrDefault
                If ndTagline IsNot Nothing AndAlso ndTagline.FirstChild IsNot Nothing AndAlso Not String.IsNullOrEmpty(ndTagline.FirstChild.InnerText.Trim) Then
                    Return HttpUtility.HtmlDecode(ndTagline.FirstChild.InnerText.Trim)
                End If
            End If
            Return Nothing
        End Function

        Private Function SearchMovie(ByVal title As String, ByVal year As Integer) As SearchResults_Movie
            Dim R As New SearchResults_Movie

            Dim strTitle As String = String.Concat(title, " ", If(Not year = 0, String.Concat("(", year, ")"), String.Empty)).Trim

            Dim htmldResultsPartialTitles As HtmlDocument = Nothing
            Dim htmldResultsPopularTitles As HtmlDocument = Nothing
            Dim htmldResultsShortTitles As HtmlDocument = Nothing
            Dim htmldResultsTvTitles As HtmlDocument = Nothing
            Dim htmldResultsVideoTitles As HtmlDocument = Nothing

            Dim webParsing As New HtmlWeb
            Dim htmldResultStandard As HtmlDocument = webParsing.Load(String.Concat("http://www.imdb.com/find?q=", HttpUtility.UrlEncode(strTitle), "&s=tt&ttype=ft"))
            Dim htmldResultsExact As HtmlDocument = webParsing.Load(String.Concat("http://www.imdb.com/find?q=", HttpUtility.UrlEncode(strTitle), "&s=tt&ttype=ft&exact=true&ref_=fn_tt_ex"))
            Dim strResponseUri = webParsing.ResponseUri.ToString

            If _SpecialSettings.SearchTvTitles Then
                htmldResultsTvTitles = webParsing.Load(String.Concat("http://www.imdb.com/search/title?title=", HttpUtility.UrlEncode(strTitle), "&title_type=tv_movie&view=simple"))
            End If
            If _SpecialSettings.SearchVideoTitles Then
                htmldResultsVideoTitles = webParsing.Load(String.Concat("http://www.imdb.com/search/title?title=", HttpUtility.UrlEncode(strTitle), "&title_type=video&view=simple"))
            End If
            If _SpecialSettings.SearchShortTitles Then
                htmldResultsShortTitles = webParsing.Load(String.Concat("http://www.imdb.com/search/title?title=", HttpUtility.UrlEncode(strTitle), "&title_type=short&view=simple"))
            End If
            If _SpecialSettings.SearchPartialTitles Then
                htmldResultsPartialTitles = webParsing.Load(String.Concat("http://www.imdb.com/find?q=", HttpUtility.UrlEncode(strTitle), "&s=tt&ttype=ft&ref_=fn_ft"))
            End If
            If _SpecialSettings.SearchPopularTitles Then
                htmldResultsPopularTitles = webParsing.Load(String.Concat("http://www.imdb.com/find?q=", HttpUtility.UrlEncode(strTitle), "&s=tt&ttype=ft&ref_=fn_tt_pop"))
            End If

            'Check if we've been redirected straight to the movie page
            If Regex.IsMatch(strResponseUri, REGEX_IMDBID) Then
                Return R
            End If

            'popular titles
            If htmldResultsPopularTitles IsNot Nothing Then
                Dim searchResults = htmldResultsPopularTitles.DocumentNode.SelectNodes("//table[@class=""findList""]/tr[@class]/td[2]")
                If searchResults IsNot Nothing Then
                    For Each nResult In searchResults
                        R.PopularTitles.Add(BuildMovieForSearchResults(
                                            StringUtils.GetIMDBIDFromString(nResult.OuterHtml),
                                            StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                            nResult.SelectSingleNode("a").InnerText,
                                            Regex.Match(nResult.InnerText, "\((\d{4})").Groups(1).Value))
                    Next
                End If
            End If

            'partial titles
            If htmldResultsPartialTitles IsNot Nothing Then
                Dim searchResults = htmldResultsPartialTitles.DocumentNode.SelectNodes("//table[@class=""findList""]/tr[@class]/td[2]")
                If searchResults IsNot Nothing Then
                    For Each nResult In searchResults
                        R.PartialMatches.Add(BuildMovieForSearchResults(
                                             StringUtils.GetIMDBIDFromString(nResult.OuterHtml),
                                             StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                             nResult.SelectSingleNode("a").InnerText,
                                             Regex.Match(nResult.InnerText, "\((\d{4})").Groups(1).Value))
                    Next
                End If
            End If

            'tv titles
            If htmldResultsTvTitles IsNot Nothing Then
                Dim searchResults = htmldResultsTvTitles.DocumentNode.SelectNodes("//span[@title]")
                If searchResults IsNot Nothing Then
                    For Each nResult In searchResults
                        R.TvTitles.Add(BuildMovieForSearchResults(
                                       StringUtils.GetIMDBIDFromString(nResult.InnerHtml),
                                       StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                       nResult.SelectSingleNode("a").InnerText,
                                       Regex.Match(nResult.SelectSingleNode("span").InnerText, "\((\d{4})").Groups(1).Value))
                    Next
                End If
            End If

            'video titles
            If htmldResultsVideoTitles IsNot Nothing Then
                Dim searchResults = htmldResultsVideoTitles.DocumentNode.SelectNodes("//span[@title]")
                If searchResults IsNot Nothing Then
                    For Each nResult In searchResults
                        R.VideoTitles.Add(BuildMovieForSearchResults(
                                          StringUtils.GetIMDBIDFromString(nResult.InnerHtml),
                                          StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                          nResult.SelectSingleNode("a").InnerText,
                                          Regex.Match(nResult.SelectSingleNode("span").InnerText, "\((\d{4})").Groups(1).Value))
                    Next
                End If
            End If

            'short titles
            If htmldResultsShortTitles IsNot Nothing Then
                Dim searchResults = htmldResultsShortTitles.DocumentNode.SelectNodes("//span[@title]")
                If searchResults IsNot Nothing Then
                    For Each nResult In searchResults
                        R.ShortTitles.Add(BuildMovieForSearchResults(
                                          StringUtils.GetIMDBIDFromString(nResult.InnerHtml),
                                          StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                          nResult.SelectSingleNode("a").InnerText,
                                          Regex.Match(nResult.SelectSingleNode("span").InnerText, "\((\d{4})").Groups(1).Value))
                    Next
                End If
            End If

            'exact titles
            If htmldResultsExact IsNot Nothing Then
                Dim searchResults = htmldResultsExact.DocumentNode.SelectNodes("//table[@class=""findList""]/tr[@class]/td[2]")
                If searchResults IsNot Nothing Then
                    For Each nResult In searchResults
                        R.ExactMatches.Add(BuildMovieForSearchResults(
                                           StringUtils.GetIMDBIDFromString(nResult.OuterHtml),
                                           StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                           nResult.SelectSingleNode("a").InnerText,
                                           Regex.Match(nResult.InnerText, "\((\d{4})").Groups(1).Value))
                    Next
                End If
            End If

            Return R
        End Function

        Public Sub SearchMovieAsync(ByVal title As String, ByVal year As Integer, ByVal filteredoptions As Structures.ScrapeOptions)
            Try
                If Not bwIMDB.IsBusy Then
                    bwIMDB.WorkerReportsProgress = False
                    bwIMDB.WorkerSupportsCancellation = True
                    bwIMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.Movies, .Parameter = title, .Year = year, .Options_Movie = filteredoptions})
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

        Private Function SearchTVShow(ByVal title As String) As SearchResults_TVShow
            Dim R As New SearchResults_TVShow

            Dim webParsing As New HtmlWeb
            Dim htmldSearchResults As HtmlDocument = webParsing.Load(String.Concat("http://www.imdb.com/search/title?title=",
                                                                               HttpUtility.UrlEncode(title),
                                                                               "&title_type=tv_series&view=simple"))

            Dim selNodes = htmldSearchResults.DocumentNode.SelectNodes("//div[@class=""lister-item mode-simple""]")
            If selNodes IsNot Nothing Then
                For Each nResult In selNodes
                    Dim ndInfo = nResult.Descendants("img").FirstOrDefault
                    If ndInfo IsNot Nothing Then
                        Dim attIMDBID = ndInfo.Attributes.Where(Function(f) f.Name = "data-tconst").FirstOrDefault
                        Dim attTitle = ndInfo.Attributes.Where(Function(f) f.Name = "alt").FirstOrDefault
                        If attIMDBID IsNot Nothing AndAlso attTitle IsNot Nothing Then
                            Dim nTVShow As New MediaContainers.TVShow
                            nTVShow.UniqueIDs.IMDbId = attIMDBID.Value
                            nTVShow.Title = attTitle.Value
                            R.Matches.Add(nTVShow)
                        End If
                    End If
                Next
            End If

            Return R
        End Function

        Public Sub SearchTVShowAsync(ByVal title As String, ByVal scrapemodifiers As Structures.ScrapeModifiers, ByVal filteredoptions As Structures.ScrapeOptions)

            If Not bwIMDB.IsBusy Then
                bwIMDB.WorkerReportsProgress = False
                bwIMDB.WorkerSupportsCancellation = True
                bwIMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.TVShows,
                  .Parameter = title, .Options_TV = filteredoptions, .ScrapeModifiers = scrapemodifiers})
            End If
        End Sub

#End Region 'Methods

#Region "Nested Types"

        Private Structure Arguments

#Region "Fields"

            Dim FullCast As Boolean
            Dim FullCrew As Boolean
            Dim Options_Movie As Structures.ScrapeOptions
            Dim Options_TV As Structures.ScrapeOptions
            Dim Parameter As String
            Dim ScrapeModifiers As Structures.ScrapeModifiers
            Dim Search As SearchType
            Dim Year As Integer

#End Region 'Fields

        End Structure

        Private Class Rating

#Region "Fields"

            Public strRating As String
            Public strVotes As String

#End Region 'Fields

        End Class

        Private Structure Results

#Region "Fields"

            Dim Result As Object
            Dim ResultType As SearchType

#End Region 'Fields

        End Structure

#End Region 'Nested Types

    End Class

End Namespace

