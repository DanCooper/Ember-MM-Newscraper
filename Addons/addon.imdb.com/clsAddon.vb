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
Imports System.Text.RegularExpressions

Public Class Scraper

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private Const _REGEX_Certifications As String = "<a href=""/search/title\?certificates=[^""]*"">([^<]*):([^<]*)</a>[^<]*(<i>([^<]*)</i>)?"
    Private Const _REGEX_IMDBID As String = "tt\d\d\d\d\d\d\d"

    Private _HtmlDocPlotSummary As HtmlDocument = Nothing
    Private _HtmlDocReleaseInfo As HtmlDocument = Nothing

#End Region 'Fields

#Region "Properties"

    Public Property Settings As New AddonSettings

    Public Property PreferredLanguage As String

#End Region 'Properties

#Region "Methods"

    Private Function BuildMovieForSearchResults(ByVal imdbID As String, ByVal lev As Integer, ByVal title As String, ByVal year As String) As MediaContainers.MainDetails
        Dim nMovie As New MediaContainers.MainDetails
        nMovie.UniqueIDs.IMDbId = imdbID
        nMovie.Lev = lev
        nMovie.Title = title
        nMovie.Premiered = year
        Return nMovie
    End Function

    Public Function GetInfo_Movie(ByVal id As String, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions, Optional ByVal getPoster As Boolean = False) As Interfaces.AddonResult
        If String.IsNullOrEmpty(id) Then Return Nothing

        Dim bIsScraperLanguage As Boolean = PreferredLanguage.ToLower.StartsWith("en")
        Dim nMainDetails As New MediaContainers.MainDetails
        Dim nTrailers As New List(Of MediaContainers.Trailer)

        Try
            'ID
            nMainDetails.UniqueIDs.IMDbId = id

            'reset all local objects
            _HtmlDocPlotSummary = Nothing
            _HtmlDocReleaseInfo = Nothing

            Dim webParsing As New HtmlWeb
            Dim htmldReference As HtmlDocument = webParsing.Load(String.Concat("http://www.imdb.com/title/", id, "/reference"))

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
                _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Originaltitle", id))
            End If

            'Actors
            If scrapeOptions.Actors Then
                Dim nActors = Parse_Actors(htmldReference)
                If nActors IsNot Nothing Then
                    nMainDetails.Actors = nActors
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Actors", id))
                End If
            End If

            'Certifications
            If scrapeOptions.Certifications Then
                Dim lstCertifications = Parse_Certifications(htmldReference)
                If lstCertifications IsNot Nothing Then
                    nMainDetails.Certifications = lstCertifications
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Certifications", id))
                End If
            End If

            'Credits
            If scrapeOptions.Credits Then
                Dim lstCredits = Parse_Credits(htmldReference)
                If lstCredits IsNot Nothing Then
                    nMainDetails.Credits = lstCredits
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Writers (Credits)", id))
                End If
            End If

            'Countries
            If scrapeOptions.Countries Then
                Dim lstCountries = Parse_Countries(htmldReference)
                If lstCountries IsNot Nothing Then
                    nMainDetails.Countries = lstCountries
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Countries", id))
                End If
            End If

            'Director
            If scrapeOptions.Directors Then
                Dim lstDirectors = Parse_Directors(htmldReference)
                If lstDirectors IsNot Nothing Then
                    nMainDetails.Directors = lstDirectors
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Directors", id))
                End If
            End If

            'Duration
            If scrapeOptions.Runtime Then
                Dim strDuration = Parse_Duration(htmldReference)
                If strDuration IsNot Nothing Then
                    nMainDetails.Runtime = strDuration
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Runtime", id))
                End If
            End If

            'Genres
            If scrapeOptions.Genres Then
                Dim lstGenres = Parse_Genres(htmldReference)
                If lstGenres IsNot Nothing Then
                    nMainDetails.Genres = lstGenres
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Genres", id))
                End If
            End If

            'MPAA
            If scrapeOptions.MPAA Then
                Dim strMPAA = Parse_MPAA(htmldReference, id)
                If id IsNot Nothing Then
                    nMainDetails.MPAA = strMPAA
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse MPAA", id))
                End If
            End If

            'Original Title
            If scrapeOptions.OriginalTitle Then
                nMainDetails.OriginalTitle = strOriginalTitle
            End If

            'Outline
            If scrapeOptions.Outline AndAlso bIsScraperLanguage Then
                Dim strOutline = Parse_Outline(htmldReference, id)
                If strOutline IsNot Nothing Then
                    nMainDetails.Outline = strOutline
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Outline", id))
                End If
            End If

            'Plot
            If scrapeOptions.Plot AndAlso bIsScraperLanguage Then
                Dim strPlot = Parse_Plot(htmldReference)
                If strPlot IsNot Nothing Then
                    nMainDetails.Plot = strPlot
                Else
                    'if "plot" isn't available then the "outline" will be used as plot
                    If nMainDetails.OutlineSpecified Then
                        nMainDetails.Plot = nMainDetails.Outline
                    Else
                        strPlot = Parse_PlotFromSummaryPage(id)
                        If Not String.IsNullOrEmpty(strPlot) Then
                            nMainDetails.Plot = strPlot
                        Else
                            _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] no result from ""plotsummary"" page for Plot", id))
                        End If
                    End If
                End If
            End If

            'Poster for search result
            If getPoster Then
                nMainDetails.ThumbPoster.URLOriginal = Parse_PosterURL(htmldReference)
            End If

            'Premiered
            If scrapeOptions.Premiered Then
                Dim dateRelease As New Date
                If Parse_ReleaseDate(htmldReference, dateRelease) Then
                    If scrapeOptions.Premiered Then nMainDetails.Premiered = dateRelease.ToString("yyyy-MM-dd")
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Premiered/Year", id))
                End If
            End If

            'Rating
            If scrapeOptions.Ratings Then
                Dim nRating = Parse_Rating(htmldReference)
                If nRating IsNot Nothing Then
                    nMainDetails.Ratings.Add(nRating)
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Rating", id))
                End If
            End If

            'Studios
            If scrapeOptions.Studios Then
                Dim lstStudios = Parse_Studios(htmldReference)
                If lstStudios IsNot Nothing Then
                    nMainDetails.Studios = lstStudios
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Studios", id))
                End If
            End If

            'Tagline
            If scrapeOptions.Tagline AndAlso bIsScraperLanguage Then
                Dim strTagline = Parse_Tagline(htmldReference)
                If strTagline IsNot Nothing Then
                    nMainDetails.Tagline = strTagline
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Tagline", id))
                End If
            End If

            'Title
            If scrapeOptions.Title Then
                If Not String.IsNullOrEmpty(Settings.ForceTitleLanguage) Then
                    nMainDetails.Title = Parse_ForcedTitle(id, strOriginalTitle)
                Else
                    nMainDetails.Title = strOriginalTitle
                End If
            End If

            'Top250
            If scrapeOptions.Top250 Then
                Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//a[@href=""/chart/top""]")
                If selNode IsNot Nothing Then
                    Dim strTop250 As String = Regex.Match(selNode.InnerText.Trim, "#([0-9]+)").Groups(1).Value
                    Dim iTop250 As Integer = 0
                    If Integer.TryParse(strTop250, iTop250) Then
                        nMainDetails.Top250 = iTop250
                    Else
                        _Logger.Trace(String.Format("[IMDB] [GetMovieInfo] [ID:""{0}""] can't parse Top250", id))
                    End If
                End If
            End If

            'Trailer
            If scrapeOptions.Trailer OrElse scrapeModifiers.MainTrailer Then
                Dim TrailerList As List(Of MediaContainers.Trailer) = IMDb.Scraper.GetMovieTrailersByIMDBID(nMainDetails.UniqueIDs.IMDbId)
                If TrailerList.Count > 0 Then
                    Dim sIMDb As New IMDb.Scraper
                    sIMDb.GetVideoLinks(TrailerList.Item(0).URLWebsite)
                    If sIMDb.VideoLinks.Count > 0 Then
                        nMainDetails.Trailer = sIMDb.VideoLinks.FirstOrDefault().Value.URL.ToString
                    End If
                End If
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            Return Nothing
        End Try

        Return New Interfaces.AddonResult With {.ScraperResult_Data = nMainDetails, .ScraperResult_Trailers = nTrailers}
    End Function

    Public Function GetInfo_TVEpisode(ByVal id As String, ByRef scrapeOptions As Structures.ScrapeOptions) As MediaContainers.MainDetails
        If String.IsNullOrEmpty(id) Then Return Nothing

        Dim bIsScraperLanguage As Boolean = PreferredLanguage.ToLower.StartsWith("en")
        Dim nMainDetails As New MediaContainers.MainDetails

        Try
            'reset all local objects
            _HtmlDocPlotSummary = Nothing
            _HtmlDocReleaseInfo = Nothing

            Dim webParsingSeasons As New HtmlWeb
            Dim htmldReference As HtmlDocument = webParsingSeasons.Load(String.Concat("http://www.imdb.com/title/", id, "/reference"))

            nMainDetails.UniqueIDs.IMDbId = id

            'get season and episode number
            Dim selSENode = htmldReference.DocumentNode.SelectSingleNode("//ul[@class=""ipl-inline-list titlereference-overview-season-episode-numbers""]")
            If selSENode IsNot Nothing Then
                Dim rSeasonEpisode As Match = Regex.Match(selSENode.InnerText,
                                                              "season (?<SEASON>\d+).*?episode (?<EPISODE>\d+)",
                                                              RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                If Not rSeasonEpisode.Success Then Return Nothing

                'Episode # Standard
                nMainDetails.Episode = CInt(rSeasonEpisode.Groups("EPISODE").Value)

                'Season # Standard
                nMainDetails.Season = CInt(rSeasonEpisode.Groups("SEASON").Value)
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
                _Logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Originaltitle", id))
            End If

            'Actors
            If scrapeOptions.Episodes.Actors Then
                Dim lstActors = Parse_Actors(htmldReference, True)
                If lstActors IsNot Nothing Then
                    nMainDetails.Actors = lstActors
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Actors", id))
                End If
            End If

            'AiredDate
            If scrapeOptions.Episodes.Aired Then
                Dim dateRelease As New Date
                If Parse_ReleaseDate(htmldReference, dateRelease) Then
                    nMainDetails.Aired = dateRelease.ToString("yyyy-MM-dd")
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse AiredDate", id))
                End If
            End If

            'Credits (writers)
            If scrapeOptions.Episodes.Credits Then
                Dim lstCredits = Parse_Credits(htmldReference)
                If lstCredits IsNot Nothing Then
                    nMainDetails.Credits = lstCredits
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Credits (Writers)", id))
                End If
            End If

            'Directors
            If scrapeOptions.Episodes.Directors Then
                Dim lstDirectors = Parse_Directors(htmldReference)
                If lstDirectors IsNot Nothing Then
                    nMainDetails.Directors = lstDirectors
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Directors", id))
                End If
            End If

            'Plot
            If scrapeOptions.Episodes.Plot AndAlso bIsScraperLanguage Then
                Dim selNodes = htmldReference.DocumentNode.SelectNodes("//section[@class=""titlereference-section-overview""]/div[2]/text()")
                If selNodes IsNot Nothing Then
                    Dim selNode = selNodes.FirstOrDefault
                    If selNode IsNot Nothing AndAlso selNode.InnerText IsNot Nothing Then
                        'get the outline, even if only a part of the outline is available
                        nMainDetails.Plot = HttpUtility.HtmlDecode(selNode.InnerText.Trim)
                        'remove the three dots to search the same text on the "plotsummary" page
                        Dim strPlot As String = Regex.Replace(nMainDetails.Plot, "\.\.\.", String.Empty)
                        If selNode.NextSibling IsNot Nothing AndAlso selNode.NextSibling.InnerText.Trim.ToLower.StartsWith("see more") Then
                            'parse the "plotsummary" page for full outline text
                            strPlot = Parse_PlotFromSummaryPage(id, strPlot)
                            If Not String.IsNullOrEmpty(strPlot) Then
                                nMainDetails.Plot = strPlot
                            Else
                                _Logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] no result from ""plotsummary"" page for Plot", id))
                            End If
                        End If
                    Else
                        _Logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Plot", id))
                    End If
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Plot", id))
                End If
            End If

            'Rating
            If scrapeOptions.Episodes.Ratings Then
                Dim nRating = Parse_Rating(htmldReference)
                If nRating IsNot Nothing Then
                    nMainDetails.Ratings.Add(nRating)
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Rating", id))
                End If
            End If

            'Title
            If scrapeOptions.Episodes.Title Then
                If Not String.IsNullOrEmpty(Settings.ForceTitleLanguage) Then
                    nMainDetails.Title = Parse_ForcedTitle(id, strOriginalTitle)
                Else
                    nMainDetails.Title = strOriginalTitle
                End If
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            Return Nothing
        End Try

        Return nMainDetails
    End Function

    Public Function GetInfo_TVEpisode(ByVal showId As String, ByVal season As Integer, ByVal episode As Integer, ByRef scrapeOptions As Structures.ScrapeOptions) As MediaContainers.MainDetails
        If String.IsNullOrEmpty(showId) OrElse season = -1 OrElse episode = -1 Then Return Nothing

        Dim webParsing As New HtmlWeb
        Dim webParsingSeasons As New HtmlWeb
        Dim htmldEpisodes As HtmlDocument = webParsingSeasons.Load(String.Concat("http://www.imdb.com/title/", showId, "/episodes?season=", season))

        Dim selNode = htmldEpisodes.DocumentNode.SelectSingleNode("//div[@class=""list detail eplist""]")
        If selNode IsNot Nothing Then
            Dim ncEpisodes = selNode.Descendants("div").Where(Function(f) Regex.Match(f.InnerText, String.Format("S{0}, Ep{1}", season, episode)).Success)
            If ncEpisodes IsNot Nothing Then
                For Each nNode In ncEpisodes
                    Dim strID = StringUtils.GetIMDBIDFromString(nNode.InnerHtml)
                    If Not String.IsNullOrEmpty(strID) Then
                        Dim nEpisode As MediaContainers.MainDetails = GetInfo_TVEpisode(strID, scrapeOptions)
                        If nEpisode IsNot Nothing Then
                            Return nEpisode
                        End If
                    End If
                Next
            End If
        End If

        Return Nothing
    End Function

    Public Sub GetInfo_TVSeason(ByRef tvshow As MediaContainers.MainDetails, ByVal showid As String, ByVal season As Integer, ByRef scrapeModifiers As Structures.ScrapeModifiers, ByRef scrapeOptions As Structures.ScrapeOptions)
        Try
            If scrapeModifiers.withEpisodes Then
                Dim webParsingSeasons As New HtmlWeb
                Dim htmldEpisodes As HtmlDocument = webParsingSeasons.Load(String.Concat("http://www.imdb.com/title/", showid, "/episodes?season=", season))

                Dim selNode = htmldEpisodes.DocumentNode.SelectSingleNode("//div[@class=""list detail eplist""]")
                If selNode IsNot Nothing Then
                    Dim ncEpisodes = selNode.ChildNodes.Where(Function(f) f.Name = "div")
                    If ncEpisodes IsNot Nothing Then
                        For Each nodeEpisode In ncEpisodes
                            Dim strIMDBID As String = StringUtils.GetIMDBIDFromString(nodeEpisode.InnerHtml)
                            Dim nEpisode As MediaContainers.MainDetails = GetInfo_TVEpisode(strIMDBID, scrapeOptions)
                            If nEpisode IsNot Nothing Then
                                tvshow.KnownEpisodes.Add(nEpisode)
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    '''  Scrape TV Show details from IMDB
    ''' </summary>
    ''' <param name="id">IMDB ID of tv show to be scraped</param>
    ''' <param name="getPoster">Scrape posters for the tv show?</param> 
    ''' <returns>True: success, false: no success</returns>
    Public Function GetInfo_TVShow(ByVal id As String, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions, Optional ByVal getPoster As Boolean = False) As MediaContainers.MainDetails
        If String.IsNullOrEmpty(id) Then Return Nothing

        Dim bIsScraperLanguage As Boolean = PreferredLanguage.ToLower.StartsWith("en")
        Dim nMainDetails As New MediaContainers.MainDetails

        Try
            nMainDetails.UniqueIDs.IMDbId = id

            'reset all local objects
            _HtmlDocPlotSummary = Nothing
            _HtmlDocReleaseInfo = Nothing

            Dim webParsing As New HtmlWeb
            Dim htmldReference As HtmlDocument = webParsing.Load(String.Concat("http://www.imdb.com/title/", id, "/reference"))

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
                _Logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Originaltitle", id))
            End If

            'Actors
            If scrapeOptions.Actors Then
                Dim lstActors = Parse_Actors(htmldReference, True)
                If lstActors IsNot Nothing Then
                    nMainDetails.Actors = lstActors
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Actors", id))
                End If
            End If

            'Certifications
            If scrapeOptions.Certifications Then
                Dim lstCertifications = Parse_Certifications(htmldReference)
                If lstCertifications IsNot Nothing Then
                    nMainDetails.Certifications = lstCertifications
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Certifications", id))
                End If
            End If

            'Countries
            If scrapeOptions.Countries Then
                Dim lstCountries = Parse_Countries(htmldReference)
                If lstCountries IsNot Nothing Then
                    nMainDetails.Countries = lstCountries
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Countries", id))
                End If
            End If

            'Creators
            If scrapeOptions.Creators Then
                Dim lstCreators = Parse_Creators(htmldReference)
                If lstCreators IsNot Nothing Then
                    nMainDetails.Creators = lstCreators
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Creators", id))
                End If
            End If

            'Genres
            If scrapeOptions.Genres Then
                Dim lstGenres = Parse_Genres(htmldReference)
                If lstGenres IsNot Nothing Then
                    nMainDetails.Genres = lstGenres
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Genres", id))
                End If
            End If

            'Original Title
            If scrapeOptions.OriginalTitle Then
                nMainDetails.OriginalTitle = strOriginalTitle
            End If

            'Plot
            If scrapeOptions.Plot AndAlso bIsScraperLanguage Then
                Dim strPlot = Parse_Plot(htmldReference)
                If strPlot IsNot Nothing Then
                    nMainDetails.Plot = strPlot
                Else
                    strPlot = Parse_PlotFromSummaryPage(id)
                    If Not String.IsNullOrEmpty(strPlot) Then
                        nMainDetails.Plot = strPlot
                    Else
                        _Logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] no result from ""plotsummary"" page for Plot", id))
                    End If
                End If
            End If

            'Poster for search result
            If getPoster Then
                nMainDetails.ThumbPoster.URLOriginal = Parse_PosterURL(htmldReference)
            End If

            'Premiered
            If scrapeOptions.Premiered Then
                Dim dateRelease As New Date
                If Parse_Premiered(id, dateRelease) Then
                    nMainDetails.Premiered = dateRelease.ToString("yyyy-MM-dd")
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Premiered", id))
                End If
            End If

            'Rating
            If scrapeOptions.Ratings Then
                Dim nRating = Parse_Rating(htmldReference)
                If nRating IsNot Nothing Then
                    nMainDetails.Ratings.Add(nRating)
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Rating", id))
                End If
            End If

            'Runtime
            If scrapeOptions.Runtime Then
                Dim strRuntime = Parse_Runtime(htmldReference)
                If strRuntime IsNot Nothing Then
                    nMainDetails.Runtime = strRuntime
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Runtime", id))
                End If
            End If

            'Studios
            If scrapeOptions.Studios Then
                Dim lstStudios = Parse_Studios(htmldReference)
                If lstStudios IsNot Nothing Then
                    nMainDetails.Studios = lstStudios
                Else
                    _Logger.Trace(String.Format("[IMDB] [GetTVShowInfo] [ID:""{0}""] can't parse Studios", id))
                End If
            End If

            'Title
            If scrapeOptions.Title Then
                If Not String.IsNullOrEmpty(Settings.ForceTitleLanguage) Then
                    nMainDetails.Title = Parse_ForcedTitle(id, strOriginalTitle)
                Else
                    nMainDetails.Title = strOriginalTitle
                End If
            End If

            'Seasons and Episodes
            If scrapeModifiers.withEpisodes OrElse scrapeModifiers.withSeasons Then
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
                    GetInfo_TVSeason(nMainDetails, nMainDetails.UniqueIDs.IMDbId, tSeason, scrapeModifiers, scrapeOptions)
                    If scrapeModifiers.withSeasons Then
                        nMainDetails.KnownSeasons.Add(New MediaContainers.MainDetails With {.Season = tSeason})
                    End If
                Next
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            Return Nothing
        End Try

        Return nMainDetails
    End Function

    Public Shared Function GetTrailers(ByVal IMDBID As String) As List(Of MediaContainers.Trailer)
        Dim nTrailers As New List(Of MediaContainers.Trailer)
        nTrailers = IMDb.Scraper.GetMovieTrailersByIMDBID(IMDBID)
        For Each tTrailer In nTrailers
            tTrailer.Source = "IMDB"
        Next
        Return nTrailers
    End Function

    Private Function Parse_Actors(ByRef htmldReference As HtmlDocument, Optional ByVal removeepisodecount As Boolean = False) As List(Of MediaContainers.Person)
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

    Private Function Parse_Certifications(ByRef htmldReference As HtmlDocument) As List(Of String)
        Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='Certification']")
        If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
            Dim rCert As MatchCollection = Regex.Matches(selNode.ParentNode.InnerHtml, _REGEX_Certifications)
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

    Private Function Parse_Countries(ByRef htmldReference As HtmlDocument) As List(Of String)
        Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='Country']")
        If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
            Dim ncCountries = selNode.ParentNode.Descendants("a")
            If ncCountries IsNot Nothing Then
                Return ncCountries.Select(Function(f) HttpUtility.HtmlDecode(f.InnerText)).Distinct.ToList
            End If
        End If
        Return Nothing
    End Function

    Private Function Parse_Creators(ByRef htmldReference As HtmlDocument) As List(Of String)
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

    Private Function Parse_Credits(ByRef htmldReference As HtmlDocument) As List(Of String)
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

    Private Function Parse_Directors(ByRef htmldReference As HtmlDocument) As List(Of String)
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

    Private Function Parse_Duration(ByRef htmldReference As HtmlDocument) As String
        Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='Runtime']")
        If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
            Dim nDuration = selNode.ParentNode.Descendants("td").Where(Function(f) Not f.InnerText = "Runtime").FirstOrDefault
            If nDuration IsNot Nothing Then
                Return nDuration.InnerText.Trim
            End If
        End If
        Return Nothing
    End Function

    Private Function Parse_ForcedTitle(ByVal id As String, ByVal originaltitle As String) As String
        If _HtmlDocReleaseInfo Is Nothing Then
            Dim webParsing As New HtmlWeb
            _HtmlDocReleaseInfo = webParsing.Load(String.Concat("http://www.imdb.com/title/", id, "/releaseinfo"))
        End If

        If _HtmlDocReleaseInfo IsNot Nothing Then
            Dim selNodes = _HtmlDocReleaseInfo.DocumentNode.SelectNodes(String.Format("//tr[@class=""ipl-zebra-list__item aka-item""]/td[contains(text(), '{0}')]", Settings.ForceTitleLanguage))
            If selNodes IsNot Nothing Then
                Dim filteredTitles = selNodes.Where(Function(f) Not HttpUtility.HtmlDecode(f.InnerText).ToLower.Contains("working title") AndAlso
                                                            Not HttpUtility.HtmlDecode(f.InnerText).ToLower.Contains("fake working title"))
                If filteredTitles IsNot Nothing AndAlso filteredTitles.Count > 0 Then
                    Return HttpUtility.HtmlDecode(filteredTitles(0).NextSibling.NextSibling.InnerText)
                End If
            End If
        End If

        'fallback
        If Settings.ForceTitleLanguageFallback Then
            Dim selFallback = _HtmlDocReleaseInfo.DocumentNode.SelectNodes("//tr[@class=""ipl-zebra-list__item aka-item""]/td")
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

    Private Function Parse_Genres(ByRef htmldReference As HtmlDocument) As List(Of String)
        Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='Genres']")
        If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
            Dim ncGenres = selNode.ParentNode.Descendants("a").Where(Function(f) Not f.InnerText.ToLower = "see more")
            If ncGenres IsNot Nothing Then
                Return ncGenres.Select(Function(f) HttpUtility.HtmlDecode(f.InnerText)).Distinct.ToList
            End If
        End If
        Return Nothing
    End Function

    Private Function Parse_MPAA(ByRef htmldReference As HtmlDocument, id As String) As String
        'try to get the full MPAA from "Parents Guide" page
        If Settings.MPAADescription Then
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
        If Settings.MPAADescription Then _Logger.Trace(String.Format("[IMDB] [ParseMPAA] [ID:""{0}""] can't parse full MPAA, try to parse the short rating", id))
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

    Private Function Parse_Outline(ByRef htmldReference As HtmlDocument, id As String) As String
        Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//section[@class=""titlereference-section-overview""]/div/text()")
        If selNode IsNot Nothing AndAlso selNode.InnerText IsNot Nothing Then
            'get the outline, even if only a part of the outline is available
            Dim strReferenceOutline = HttpUtility.HtmlDecode(selNode.InnerText.Trim)
            'remove the three dots to search the same text on the "plotsummary" page
            Dim strOutlineForSearch As String = Regex.Replace(strReferenceOutline, "\.\.\.", String.Empty)
            If selNode.NextSibling IsNot Nothing AndAlso selNode.NextSibling.InnerText.Trim.ToLower.StartsWith("see more") Then
                'parse the "plotsummary" page for full outline text
                strOutlineForSearch = Parse_PlotFromSummaryPage(id, strOutlineForSearch)
                If Not String.IsNullOrEmpty(strOutlineForSearch) Then
                    Return strOutlineForSearch
                Else
                    _Logger.Trace(String.Format("[IMDB] [ParseOutline] [ID:""{0}""] no result from ""plotsummary"" page for Outline", id))
                End If
            End If
            If Not String.IsNullOrEmpty(strReferenceOutline) Then
                Return strReferenceOutline
            End If
        End If
        Return Nothing
    End Function

    Private Function Parse_Plot(ByRef htmldReference As HtmlDocument) As String
        Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='Plot Summary']")
        If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
            Dim ndPlot = selNode.ParentNode.Descendants("p").FirstOrDefault
            If ndPlot IsNot Nothing AndAlso ndPlot.FirstChild IsNot Nothing AndAlso Not String.IsNullOrEmpty(ndPlot.FirstChild.InnerText.Trim) Then
                Return HttpUtility.HtmlDecode(ndPlot.FirstChild.InnerText.Trim)
            End If
        End If
        Return Nothing
    End Function

    Private Function Parse_PlotFromSummaryPage(ByVal id As String, Optional ByVal outline As String = "") As String
        If _HtmlDocPlotSummary Is Nothing Then
            Dim webParsing As New HtmlWeb
            _HtmlDocPlotSummary = webParsing.Load(String.Concat("http://www.imdb.com/title/", id, "/plotsummary"))
        End If
        If _HtmlDocPlotSummary IsNot Nothing Then
            Dim selNode As HtmlNode = Nothing
            If Not String.IsNullOrEmpty(outline) Then
                selNode = _HtmlDocPlotSummary.DocumentNode.SelectNodes("//p").Where(Function(f) f.InnerText IsNot Nothing AndAlso
                                                                                           HttpUtility.HtmlDecode(f.InnerText.Trim).StartsWith(outline)).FirstOrDefault
            Else
                selNode = _HtmlDocPlotSummary.DocumentNode.SelectNodes("//ul[@id=""plot-summaries-content""]/li/p").FirstOrDefault
            End If
            If selNode IsNot Nothing Then
                Return HttpUtility.HtmlDecode(selNode.InnerText.Trim)
            Else
                _Logger.Trace(String.Format("[IMDB] [ParsePlotFromSummaryPage] [ID:""{0}""] no proper result from the ""plotsummary"" page for Outline", id))
            End If
        Else
            _Logger.Trace(String.Format("[IMDB] [ParsePlotFromSummaryPage] [ID:""{0}""] can't parse the ""plotsummary"" page", id))
        End If
        Return String.Empty
    End Function

    Private Function Parse_PosterURL(ByRef htmldReference As HtmlDocument) As String
        Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//img[@class=""titlereference-primary-image""]")
        If selNode IsNot Nothing Then
            Dim attSource = selNode.Attributes("src")
            If attSource IsNot Nothing Then
                Return attSource.Value
            End If
        End If
        Return String.Empty
    End Function

    Private Function Parse_Rating(ByRef htmldReference As HtmlDocument) As MediaContainers.RatingDetails
        Dim selNodeRating = htmldReference.DocumentNode.SelectSingleNode("//span[@class=""ipl-rating-star__rating""]")
        Dim selNodeVotes = htmldReference.DocumentNode.SelectSingleNode("//span[@class=""ipl-rating-star__total-votes""]")
        If selNodeRating IsNot Nothing AndAlso selNodeVotes IsNot Nothing Then
            Dim dblRating As Double
            Dim iVotes As Integer
            If Double.TryParse(selNodeRating.InnerText.Trim, dblRating) AndAlso
                Integer.TryParse(NumUtils.CleanVotes(Regex.Match(selNodeVotes.InnerText.Trim, "[0-9,.]+").Value), iVotes) Then
                Return New MediaContainers.RatingDetails With {
                    .Max = 10,
                    .Name = "imdb",
                    .Value = dblRating,
                    .Votes = iVotes
                }
            End If
        End If
        Return Nothing
    End Function

    Private Function Parse_Runtime(ByRef htmldReference As HtmlDocument) As String
        Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='Runtime']")
        If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
            Dim nDuration = selNode.ParentNode.Descendants("td").Where(Function(f) Not f.InnerText = "Runtime").FirstOrDefault
            If nDuration IsNot Nothing Then
                Return nDuration.InnerText.Trim
            End If
        End If
        Return Nothing
    End Function

    Private Function Parse_ReleaseDate(ByRef htmldReference As HtmlDocument, ByRef releasedate As Date) As Boolean
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

    Private Function Parse_Premiered(ByVal id As String, ByRef releasedate As Date) As Boolean
        If _HtmlDocReleaseInfo Is Nothing Then
            Dim webParsing As New HtmlWeb
            _HtmlDocReleaseInfo = webParsing.Load(String.Concat("http://www.imdb.com/title/", id, "/releaseinfo"))
        End If
        If _HtmlDocReleaseInfo IsNot Nothing Then
            'get all release dates from table
            Dim selNodes = _HtmlDocReleaseInfo.DocumentNode.SelectNodes("//td[@class=""release_date""]")
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

    Private Function Parse_Studios(ByRef htmldReference As HtmlDocument) As List(Of String)
        Dim lstStudios As New List(Of String)
        Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//h4[@class=""ipl-header__content ipl-list-title""][.='Production Companies']")
        If selNode IsNot Nothing Then
            lstStudios.AddRange(selNode.ParentNode.ParentNode.NextSibling.NextSibling.Descendants("a").Select(Function(f) HttpUtility.HtmlDecode(f.InnerText)).Distinct.ToList)
        End If

        If Settings.StudiosWithDistributors Then
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

    Private Function Parse_Tagline(ByRef htmldReference As HtmlDocument) As String
        Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//tr[@class=""ipl-zebra-list__item""]/td[@class=""ipl-zebra-list__label""][.='Taglines']")
        If selNode IsNot Nothing AndAlso selNode.ParentNode IsNot Nothing Then
            Dim ndTagline = selNode.ParentNode.Descendants("td").Where(Function(f) Not f.InnerText = "Taglines").FirstOrDefault
            If ndTagline IsNot Nothing AndAlso ndTagline.FirstChild IsNot Nothing AndAlso Not String.IsNullOrEmpty(ndTagline.FirstChild.InnerText.Trim) Then
                Return HttpUtility.HtmlDecode(ndTagline.FirstChild.InnerText.Trim)
            End If
        End If
        Return Nothing
    End Function

    Public Function Search_Movie(ByVal title As String, ByVal year As Integer) As List(Of MediaContainers.MainDetails)
        Dim nSearchResults As New List(Of MediaContainers.MainDetails)

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

        If Settings.SearchTvTitles Then
            htmldResultsTvTitles = webParsing.Load(String.Concat("http://www.imdb.com/search/title?title=", HttpUtility.UrlEncode(strTitle), "&title_type=tv_movie&view=simple"))
        End If
        If Settings.SearchVideoTitles Then
            htmldResultsVideoTitles = webParsing.Load(String.Concat("http://www.imdb.com/search/title?title=", HttpUtility.UrlEncode(strTitle), "&title_type=video&view=simple"))
        End If
        If Settings.SearchShortTitles Then
            htmldResultsShortTitles = webParsing.Load(String.Concat("http://www.imdb.com/search/title?title=", HttpUtility.UrlEncode(strTitle), "&title_type=short&view=simple"))
        End If
        If Settings.SearchPartialTitles Then
            htmldResultsPartialTitles = webParsing.Load(String.Concat("http://www.imdb.com/find?q=", HttpUtility.UrlEncode(strTitle), "&s=tt&ttype=ft&ref_=fn_ft"))
        End If
        If Settings.SearchPopularTitles Then
            htmldResultsPopularTitles = webParsing.Load(String.Concat("http://www.imdb.com/find?q=", HttpUtility.UrlEncode(strTitle), "&s=tt&ttype=ft&ref_=fn_tt_pop"))
        End If

        'Check if we've been redirected straight to the movie page
        If Regex.IsMatch(strResponseUri, _REGEX_IMDBID) Then
            Return nSearchResults
        End If

        'exact titles
        If htmldResultsExact IsNot Nothing Then
            Dim searchResults = htmldResultsExact.DocumentNode.SelectNodes("//table[@class=""findList""]/tr[@class]/td[2]")
            If searchResults IsNot Nothing Then
                For Each nResult In searchResults
                    nSearchResults.Add(BuildMovieForSearchResults(
                                       StringUtils.GetIMDBIDFromString(nResult.OuterHtml),
                                       StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                       nResult.SelectSingleNode("a").InnerText,
                                       Regex.Match(nResult.InnerText, "\((\d{4})").Groups(1).Value)
                                       )
                Next
            End If
        End If

        'popular titles
        If htmldResultsPopularTitles IsNot Nothing Then
            Dim searchResults = htmldResultsPopularTitles.DocumentNode.SelectNodes("//table[@class=""findList""]/tr[@class]/td[2]")
            If searchResults IsNot Nothing Then
                For Each nResult In searchResults
                    nSearchResults.Add(BuildMovieForSearchResults(
                                       StringUtils.GetIMDBIDFromString(nResult.OuterHtml),
                                       StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                       nResult.SelectSingleNode("a").InnerText,
                                       Regex.Match(nResult.InnerText, "\((\d{4})").Groups(1).Value)
                                       )
                Next
            End If
        End If

        'partial titles
        If htmldResultsPartialTitles IsNot Nothing Then
            Dim searchResults = htmldResultsPartialTitles.DocumentNode.SelectNodes("//table[@class=""findList""]/tr[@class]/td[2]")
            If searchResults IsNot Nothing Then
                For Each nResult In searchResults
                    nSearchResults.Add(BuildMovieForSearchResults(
                                       StringUtils.GetIMDBIDFromString(nResult.OuterHtml),
                                       StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                       nResult.SelectSingleNode("a").InnerText,
                                       Regex.Match(nResult.InnerText, "\((\d{4})").Groups(1).Value)
                                       )
                Next
            End If
        End If

        'tv titles
        If htmldResultsTvTitles IsNot Nothing Then
            Dim searchResults = htmldResultsTvTitles.DocumentNode.SelectNodes("//span[@title]")
            If searchResults IsNot Nothing Then
                For Each nResult In searchResults
                    nSearchResults.Add(BuildMovieForSearchResults(
                                       StringUtils.GetIMDBIDFromString(nResult.InnerHtml),
                                       StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                       nResult.SelectSingleNode("a").InnerText,
                                       Regex.Match(nResult.SelectSingleNode("span").InnerText, "\((\d{4})").Groups(1).Value)
                                       )
                Next
            End If
        End If

        'video titles
        If htmldResultsVideoTitles IsNot Nothing Then
            Dim searchResults = htmldResultsVideoTitles.DocumentNode.SelectNodes("//span[@title]")
            If searchResults IsNot Nothing Then
                For Each nResult In searchResults
                    nSearchResults.Add(BuildMovieForSearchResults(
                                       StringUtils.GetIMDBIDFromString(nResult.InnerHtml),
                                       StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                       nResult.SelectSingleNode("a").InnerText,
                                       Regex.Match(nResult.SelectSingleNode("span").InnerText, "\((\d{4})").Groups(1).Value)
                                       )
                Next
            End If
        End If

        'short titles
        If htmldResultsShortTitles IsNot Nothing Then
            Dim searchResults = htmldResultsShortTitles.DocumentNode.SelectNodes("//span[@title]")
            If searchResults IsNot Nothing Then
                For Each nResult In searchResults
                    nSearchResults.Add(BuildMovieForSearchResults(
                                       StringUtils.GetIMDBIDFromString(nResult.InnerHtml),
                                       StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                       nResult.SelectSingleNode("a").InnerText,
                                       Regex.Match(nResult.SelectSingleNode("span").InnerText, "\((\d{4})").Groups(1).Value)
                                       )
                Next
            End If
        End If

        Return nSearchResults
    End Function

    Public Function Search_TVShow(ByVal title As String) As List(Of MediaContainers.MainDetails)
        Dim nSearchResults As New List(Of MediaContainers.MainDetails)

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
                        Dim nTVShow As New MediaContainers.MainDetails
                        nTVShow.UniqueIDs.IMDbId = attIMDBID.Value
                        nTVShow.Title = attTitle.Value
                        nSearchResults.Add(nTVShow)
                    End If
                End If
            Next
        End If

        Return nSearchResults
    End Function

#End Region 'Methods

End Class