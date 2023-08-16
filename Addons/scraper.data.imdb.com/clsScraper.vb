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

Public Class Scraper
    Implements Interfaces.IScraper_Search

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private Const REGEX_Certifications As String = "<a href=""/search/title\?certificates=[^""]*"">([^<]*):([^<]*)</a>[^<]*(<i>([^<]*)</i>)?"
    Private Const REGEX_IMDBID As String = "tt\d\d\d\d\d\d\d"

    Private htmldPlotSummary As HtmlDocument = Nothing
    Private htmldReleaseInfo As HtmlDocument = Nothing

    Private _addonSettings As Addon.AddonSettings

    Friend WithEvents _backgroundWorker As New ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property SearchResults_Movie() As New List(Of MediaContainers.Movie) Implements Interfaces.IScraper_Search.SearchResults_Movie

    Public ReadOnly Property SearchResults_Movieset() As New List(Of MediaContainers.Movieset) Implements Interfaces.IScraper_Search.SearchResults_Movieset

    Public ReadOnly Property SearchResult_TVShow() As New List(Of MediaContainers.TVShow) Implements Interfaces.IScraper_Search.SearchResult_TVShow

#End Region 'Properties

#Region "Enumerations"

    Private Enum TaskType
        GetInfo_Movie
        GetInfo_Movieset
        GetInfo_TVShow
        Search_By_Title_Movie
        Search_By_Title_Movieset
        Search_By_Title_TVShow
        Search_By_UniqueId_Movie
        Search_By_UniqueId_Movieset
        Search_By_UniqueId_TVShow
    End Enum

#End Region 'Enumerations

#Region "Events"

    Public Event GetInfoFinished_Movie(ByVal mainInfo As MediaContainers.Movie) Implements Interfaces.IScraper_Search.GetInfoFinished_Movie
    Public Event GetInfoFinished_Movieset(ByVal mainInfo As MediaContainers.Movieset) Implements Interfaces.IScraper_Search.GetInfoFinished_Movieset
    Public Event GetInfoFinished_TVShow(ByVal mainInfo As MediaContainers.TVShow) Implements Interfaces.IScraper_Search.GetInfoFinished_TVShow

    Public Event SearchFinished_Movie(ByVal searchResults As List(Of MediaContainers.Movie)) Implements Interfaces.IScraper_Search.SearchFinished_Movie
    Public Event SearchFinished_Movieset(ByVal searchResults As List(Of MediaContainers.Movieset)) Implements Interfaces.IScraper_Search.SearchFinished_Movieset
    Public Event SearchFinished_TVShow(ByVal searchResults As List(Of MediaContainers.TVShow)) Implements Interfaces.IScraper_Search.SearchFinished_TVShow

#End Region 'Events

#Region "Methods"

    Public Sub New(ByVal addonSettings As Addon.AddonSettings)
        _addonSettings = addonSettings
    End Sub

    Public Sub CancelAsync() Implements Interfaces.IScraper_Search.CancelAsync
        If _backgroundWorker.IsBusy Then _backgroundWorker.CancelAsync()

        While _backgroundWorker.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub BackgroundWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles _backgroundWorker.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)

        Select Case Args.TaskType
            Case TaskType.GetInfo_Movie
                e.Result = New Results With {
                    .Result = GetInfo_Movie(Args.Parameter, Args.ScrapeOptions),
                    .TaskType = Args.TaskType
                }

            Case TaskType.GetInfo_Movieset
                e.Result = New Results With {
                    .Result = Nothing,
                    .TaskType = Args.TaskType
                }

            Case TaskType.GetInfo_TVShow
                e.Result = New Results With {
                    .Result = GetInfo_TVShow(Args.Parameter, Args.ScrapeOptions, Args.ScrapeModifiers),
                    .TaskType = Args.TaskType
                }

            Case TaskType.Search_By_Title_Movie
                e.Result = New Results With {
                    .Result = Search_By_Title_Movie(Args.Parameter, Args.Year),
                    .TaskType = Args.TaskType
                }

            Case TaskType.Search_By_Title_Movieset
                e.Result = New Results With {
                    .Result = Nothing,
                    .TaskType = Args.TaskType
                }

            Case TaskType.Search_By_Title_TVShow
                e.Result = New Results With {
                    .Result = Search_By_Title_TVShow(Args.Parameter),
                    .TaskType = Args.TaskType
                }
        End Select
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles _backgroundWorker.RunWorkerCompleted
        Dim Result As Results = DirectCast(e.Result, Results)

        Select Case Result.TaskType
            Case TaskType.GetInfo_Movie
                RaiseEvent GetInfoFinished_Movie(DirectCast(Result.Result, MediaContainers.Movie))

            Case TaskType.GetInfo_Movieset
                RaiseEvent GetInfoFinished_Movieset(DirectCast(Result.Result, MediaContainers.Movieset))

            Case TaskType.GetInfo_TVShow
                RaiseEvent GetInfoFinished_TVShow(DirectCast(Result.Result, MediaContainers.TVShow))

            Case TaskType.Search_By_Title_Movie, TaskType.Search_By_UniqueId_Movie
                RaiseEvent SearchFinished_Movie(DirectCast(Result.Result, List(Of MediaContainers.Movie)))

            Case TaskType.Search_By_Title_Movieset, TaskType.Search_By_UniqueId_Movieset
                RaiseEvent SearchFinished_Movieset(DirectCast(Result.Result, List(Of MediaContainers.Movieset)))

            Case TaskType.Search_By_Title_TVShow, TaskType.Search_By_UniqueId_TVShow
                RaiseEvent SearchFinished_TVShow(DirectCast(Result.Result, List(Of MediaContainers.TVShow)))
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
                    If movies(c).Year = tmpyear Then
                        ret = c
                        Exit For
                    End If
                Next
            End If
        End If
        Return ret
    End Function

    Public Function GetInfo_Movie(ByVal imdbId As String,
                                  ByVal filteredOptions As Structures.ScrapeOptions
                                  ) As MediaContainers.Movie Implements Interfaces.IScraper_Search.GetInfo_Movie
        If String.IsNullOrEmpty(imdbId.Trim) Then Return Nothing

        Try
            If _backgroundWorker.CancellationPending Then Return Nothing

            Dim bIsScraperLanguage As Boolean = _addonSettings.PrefLanguage.ToLower.StartsWith("en")

            Dim nResult As New MediaContainers.Movie With {.Scrapersource = "IMDb"}

            'reset all local objects
            htmldPlotSummary = Nothing
            htmldReleaseInfo = Nothing

            Dim webParsing As New HtmlWeb
            Dim htmldReference As HtmlDocument = webParsing.Load(String.Concat("http://www.imdb.com/title/", imdbId, "/reference"))

            If _backgroundWorker.CancellationPending Then Return Nothing

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
                _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Originaltitle", imdbId))
            End If

            'IDs
            nResult.UniqueIDs.IMDbId = imdbId

            'Actors
            If filteredOptions.bMainActors Then
                Dim nActors = Parse_Actors(htmldReference)
                If nActors IsNot Nothing Then
                    nResult.Actors = nActors
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Actors", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Certifications
            If filteredOptions.bMainCertifications Then
                Dim lstCertifications = Parse_Certifications(htmldReference)
                If lstCertifications IsNot Nothing Then
                    nResult.Certifications = lstCertifications
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Certifications", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Countries
            If filteredOptions.bMainCountries Then
                Dim lstCountries = Parse_Countries(htmldReference)
                If lstCountries IsNot Nothing Then
                    nResult.Countries = lstCountries
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Countries", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Director
            If filteredOptions.bMainDirectors Then
                Dim lstDirectors = Parse_Directors(htmldReference)
                If lstDirectors IsNot Nothing Then
                    nResult.Directors = lstDirectors
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Directors", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Duration
            If filteredOptions.bMainRuntime Then
                Dim strDuration = Parse_Duration(htmldReference)
                If strDuration IsNot Nothing Then
                    nResult.Runtime = strDuration
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Runtime", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Genres
            If filteredOptions.bMainGenres Then
                Dim lstGenres = Parse_Genres(htmldReference)
                If lstGenres IsNot Nothing Then
                    nResult.Genres = lstGenres
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Genres", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'MPAA
            If filteredOptions.bMainMPAA Then
                Dim strMPAA = Parse_MPAA(htmldReference, imdbId)
                If imdbId IsNot Nothing Then
                    nResult.MPAA = strMPAA
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse MPAA", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Original Title
            If filteredOptions.bMainOriginalTitle Then
                nResult.OriginalTitle = strOriginalTitle
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Outline
            If filteredOptions.bMainOutline AndAlso bIsScraperLanguage Then
                Dim strOutline = Parse_Outline(htmldReference, imdbId)
                If strOutline IsNot Nothing Then
                    nResult.Outline = strOutline
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Outline", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Plot
            If filteredOptions.bMainPlot AndAlso bIsScraperLanguage Then
                Dim strPlot = Parse_Plot(htmldReference)
                If strPlot IsNot Nothing Then
                    nResult.Plot = strPlot
                Else
                    'if "plot" isn't available then the "outline" will be used as plot
                    If nResult.OutlineSpecified Then
                        nResult.Plot = nResult.Outline
                    Else
                        strPlot = Parse_PlotFromSummaryPage(imdbId)
                        If Not String.IsNullOrEmpty(strPlot) Then
                            nResult.Plot = strPlot
                        Else
                            _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] no result from ""plotsummary"" page for Plot", imdbId))
                        End If
                    End If
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Poster (used only for SearchResults dialog)
            nResult.ThumbPoster = Parse_ThumbPoster(htmldReference)

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Premiered
            If filteredOptions.bMainPremiered Then
                Dim datePremiered As New Date
                If Parse_Premiered(htmldReference, datePremiered) Then
                    If filteredOptions.bMainPremiered Then nResult.Premiered = datePremiered.ToString("yyyy-MM-dd")
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Premiered/Year", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Rating
            If filteredOptions.bMainRating Then
                Dim nRating = Parse_Rating(htmldReference)
                If nRating IsNot Nothing Then
                    nResult.Ratings.Add(nRating)
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Rating", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Studios
            If filteredOptions.bMainStudios Then
                Dim lstStudios = Parse_Studios(htmldReference)
                If lstStudios IsNot Nothing Then
                    nResult.Studios = lstStudios
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Studios", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Tagline
            If filteredOptions.bMainTagline AndAlso bIsScraperLanguage Then
                Dim strTagline = Parse_Tagline(htmldReference)
                If strTagline IsNot Nothing Then
                    nResult.Tagline = strTagline
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Tagline", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Title
            If filteredOptions.bMainTitle Then
                If Not String.IsNullOrEmpty(_addonSettings.ForceTitleLanguage) Then
                    nResult.Title = Parse_ForcedTitle(imdbId, strOriginalTitle)
                Else
                    nResult.Title = strOriginalTitle
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Top250
            If filteredOptions.bMainTop250 Then
                Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//a[@href=""/chart/top""]")
                If selNode IsNot Nothing Then
                    Dim strTop250 As String = Regex.Match(selNode.InnerText.Trim, "#([0-9]+)").Groups(1).Value
                    Dim iTop250 As Integer = 0
                    If Integer.TryParse(strTop250, iTop250) Then
                        nResult.Top250 = iTop250
                    Else
                        _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Top250", imdbId))
                    End If
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Writers
            If filteredOptions.bMainWriters Then
                Dim lstCredits = Parse_Credits(htmldReference)
                If lstCredits IsNot Nothing Then
                    nResult.Credits = lstCredits
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Writers (Credits)", imdbId))
                End If
            End If

            ''Year (fallback if ReleaseDate can't be parsed)
            'If filteredoptions.bMainYear AndAlso Not nMovie.YearSpecified Then
            '    Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//span[@class=""titlereference-title-year""]/a")
            '    If selNode IsNot Nothing Then
            '        nMovie.Year = selNode.InnerText
            '    Else
            '        logger.Trace(String.Format("[IMDb] [GetInfo_Movie] [ID:""{0}""] can't parse Year (fallback)", id))
            '    End If
            'End If

            Return nResult
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            Return Nothing
        End Try
    End Function

    Public Function GetInfo_Movieset(ByVal uniqueId As String,
                                     ByVal filteredOptions As Structures.ScrapeOptions
                                     ) As MediaContainers.Movieset Implements Interfaces.IScraper_Search.GetInfo_Movieset
        Return Nothing
    End Function

    Public Function GetInfo_TVEpisode(ByVal imdbId As String,
                                      ByVal filteredOptions As Structures.ScrapeOptions
                                      ) As MediaContainers.EpisodeDetails
        If String.IsNullOrEmpty(imdbId.Trim) Then Return Nothing

        Try
            Dim bIsScraperLanguage As Boolean = _addonSettings.PrefLanguage.ToLower.StartsWith("en")

            'reset all local objects
            htmldPlotSummary = Nothing
            htmldReleaseInfo = Nothing

            Dim webParsingSeasons As New HtmlWeb
            Dim htmldReference As HtmlDocument = webParsingSeasons.Load(String.Concat("http://www.imdb.com/title/", imdbId, "/reference"))

            If _backgroundWorker.CancellationPending Then Return Nothing

            Dim nResult As New MediaContainers.EpisodeDetails With {
                .Scrapersource = "IMDb",
                .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.TVEpisode) With {.IMDbId = imdbId}
            }

            'get season and episode number
            Dim selSENode = htmldReference.DocumentNode.SelectSingleNode("//ul[@class=""ipl-inline-list titlereference-overview-season-episode-numbers""]")
            If selSENode IsNot Nothing Then
                Dim rSeasonEpisode As Match = Regex.Match(selSENode.InnerText,
                                                              "season (?<SEASON>\d+).*?episode (?<EPISODE>\d+)",
                                                              RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                If Not rSeasonEpisode.Success Then Return Nothing

                'Episode # Standard
                nResult.Episode = CInt(rSeasonEpisode.Groups("EPISODE").Value)

                'Season # Standard
                nResult.Season = CInt(rSeasonEpisode.Groups("SEASON").Value)
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
                _Logger.Trace(String.Format("[IMDb] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Originaltitle", imdbId))
            End If

            'Actors
            If filteredOptions.bEpisodeActors Then
                Dim lstActors = Parse_Actors(htmldReference, True)
                If lstActors IsNot Nothing Then
                    nResult.Actors = lstActors
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Actors", imdbId))
                End If
            End If

            'AiredDate
            If filteredOptions.bEpisodeAired Then
                Dim dateRelease As New Date
                If Parse_Premiered(htmldReference, dateRelease) Then
                    nResult.Aired = dateRelease.ToString("yyyy-MM-dd")
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetTVEpisodeInfo] [ID:""{0}""] can't parse AiredDate", imdbId))
                End If
            End If

            'Credits (writers)
            If filteredOptions.bEpisodeCredits Then
                Dim lstCredits = Parse_Credits(htmldReference)
                If lstCredits IsNot Nothing Then
                    nResult.Credits = lstCredits
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Credits (Writers)", imdbId))
                End If
            End If

            'Directors
            If filteredOptions.bEpisodeDirectors Then
                Dim lstDirectors = Parse_Directors(htmldReference)
                If lstDirectors IsNot Nothing Then
                    nResult.Directors = lstDirectors
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Directors", imdbId))
                End If
            End If

            'Plot
            If filteredOptions.bEpisodePlot AndAlso bIsScraperLanguage Then
                Dim selNodes = htmldReference.DocumentNode.SelectNodes("//section[@class=""titlereference-section-overview""]/div[2]/text()")
                If selNodes IsNot Nothing Then
                    Dim selNode = selNodes.FirstOrDefault
                    If selNode IsNot Nothing AndAlso selNode.InnerText IsNot Nothing Then
                        'get the outline, even if only a part of the outline is available
                        nResult.Plot = HttpUtility.HtmlDecode(selNode.InnerText.Trim)
                        'remove the three dots to search the same text on the "plotsummary" page
                        Dim strPlot As String = Regex.Replace(nResult.Plot, "\.\.\.", String.Empty)
                        If selNode.NextSibling IsNot Nothing AndAlso selNode.NextSibling.InnerText.Trim.ToLower.StartsWith("see more") Then
                            'parse the "plotsummary" page for full outline text
                            strPlot = Parse_PlotFromSummaryPage(imdbId, strPlot)
                            If Not String.IsNullOrEmpty(strPlot) Then
                                nResult.Plot = strPlot
                            Else
                                _Logger.Trace(String.Format("[IMDb] [GetTVEpisodeInfo] [ID:""{0}""] no result from ""plotsummary"" page for Plot", imdbId))
                            End If
                        End If
                    Else
                        _Logger.Trace(String.Format("[IMDb] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Plot", imdbId))
                    End If
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Plot", imdbId))
                End If
            End If

            'Rating
            If filteredOptions.bEpisodeRating Then
                Dim nRating = Parse_Rating(htmldReference)
                If nRating IsNot Nothing Then
                    nResult.Ratings.Add(nRating)
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetTVEpisodeInfo] [ID:""{0}""] can't parse Rating", imdbId))
                End If
            End If

            'Title
            If filteredOptions.bEpisodeTitle Then
                If Not String.IsNullOrEmpty(_addonSettings.ForceTitleLanguage) Then
                    nResult.Title = Parse_ForcedTitle(imdbId, strOriginalTitle)
                Else
                    nResult.Title = strOriginalTitle
                End If
            End If

            Return nResult
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            Return Nothing
        End Try
    End Function

    Public Function GetInfo_TVEpisode(ByVal showId As String,
                                      ByVal season As Integer,
                                      ByVal episode As Integer,
                                      ByRef filteredOptions As Structures.ScrapeOptions
                                      ) As MediaContainers.EpisodeDetails
        If String.IsNullOrEmpty(showId) OrElse season = -1 OrElse episode = -1 Then Return Nothing

        Dim webParsing As New HtmlWeb
        Dim webParsingSeasons As New HtmlWeb
        Dim htmldEpisodes As HtmlDocument = webParsingSeasons.Load(String.Concat("http://www.imdb.com/title/", showId, "/episodes?season=", season))

        Dim selNode = htmldEpisodes.DocumentNode.SelectSingleNode("//div[@class=""list detail eplist""]")
        If selNode IsNot Nothing Then
            Dim ncEpisodes = selNode.Descendants("div").Where(Function(f) Regex.Match(f.InnerText, String.Format("S{0}, Ep{1}", season, episode)).Success)
            If ncEpisodes IsNot Nothing Then
                For Each nNode In ncEpisodes
                    Dim strID = StringUtils.GetIMDbIdFromString(nNode.InnerHtml)
                    If Not String.IsNullOrEmpty(strID) Then
                        Dim nEpisode As MediaContainers.EpisodeDetails = GetInfo_TVEpisode(strID, filteredOptions)
                        If nEpisode IsNot Nothing Then
                            Return nEpisode
                        End If
                    End If
                Next
            End If
        End If

        Return Nothing
    End Function

    Public Sub GetInfo_TVSeason(ByRef nTVShow As MediaContainers.TVShow,
                                ByVal showId As String,
                                ByVal season As Integer,
                                ByRef filteredOptions As Structures.ScrapeOptions,
                                ByRef scrapeModifiers As Structures.ScrapeModifiers
                                )
        Try
            If scrapeModifiers.withEpisodes Then
                Dim webParsingSeasons As New HtmlWeb
                Dim htmldEpisodes As HtmlDocument = webParsingSeasons.Load(String.Concat("http://www.imdb.com/title/", showId, "/episodes?season=", season))

                Dim selNode = htmldEpisodes.DocumentNode.SelectSingleNode("//div[@class=""list detail eplist""]")
                If selNode IsNot Nothing Then
                    Dim ncEpisodes = selNode.ChildNodes.Where(Function(f) f.Name = "div")
                    If ncEpisodes IsNot Nothing Then
                        For Each nodeEpisode In ncEpisodes
                            Dim strIMDbId As String = StringUtils.GetIMDbIdFromString(nodeEpisode.InnerHtml)
                            Dim nEpisode As MediaContainers.EpisodeDetails = GetInfo_TVEpisode(strIMDbId, filteredOptions)
                            If nEpisode IsNot Nothing Then
                                nTVShow.KnownEpisodes.Add(nEpisode)
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Function GetInfo_TVShow(ByVal imdbId As String,
                                   ByVal filteredoptions As Structures.ScrapeOptions,
                                   ByVal scrapemodifier As Structures.ScrapeModifiers
                                   ) As MediaContainers.TVShow Implements Interfaces.IScraper_Search.GetInfo_TVShow
        If String.IsNullOrEmpty(imdbId) Then Return Nothing

        Try
            If _backgroundWorker.CancellationPending Then Return Nothing

            Dim bIsScraperLanguage As Boolean = _addonSettings.PrefLanguage.ToLower.StartsWith("en")

            Dim nResult As New MediaContainers.TVShow With {
                .Scrapersource = "IMDb",
                .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.TVShow) With {.IMDbId = imdbId}
            }

            'reset all local objects
            htmldPlotSummary = Nothing
            htmldReleaseInfo = Nothing

            Dim webParsing As New HtmlWeb
            Dim htmldReference As HtmlDocument = webParsing.Load(String.Concat("http://www.imdb.com/title/", imdbId, "/reference"))

            If _backgroundWorker.CancellationPending Then Return Nothing

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
                _Logger.Trace(String.Format("[IMDb] [GetInfo_TVShow] [ID:""{0}""] can't parse Originaltitle", imdbId))
            End If

            'Actors
            If filteredoptions.bMainActors Then
                Dim lstActors = Parse_Actors(htmldReference, True)
                If lstActors IsNot Nothing Then
                    nResult.Actors = lstActors
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_TVShow] [ID:""{0}""] can't parse Actors", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Certifications
            If filteredoptions.bMainCertifications Then
                Dim lstCertifications = Parse_Certifications(htmldReference)
                If lstCertifications IsNot Nothing Then
                    nResult.Certifications = lstCertifications
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_TVShow] [ID:""{0}""] can't parse Certifications", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Countries
            If filteredoptions.bMainCountries Then
                Dim lstCountries = Parse_Countries(htmldReference)
                If lstCountries IsNot Nothing Then
                    nResult.Countries = lstCountries
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_TVShow] [ID:""{0}""] can't parse Countries", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Creators
            If filteredoptions.bMainCreators Then
                Dim lstCreators = ParseCreators(htmldReference)
                If lstCreators IsNot Nothing Then
                    nResult.Creators = lstCreators
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_TVShow] [ID:""{0}""] can't parse Creators", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Genres
            If filteredoptions.bMainGenres Then
                Dim lstGenres = Parse_Genres(htmldReference)
                If lstGenres IsNot Nothing Then
                    nResult.Genres = lstGenres
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_TVShow] [ID:""{0}""] can't parse Genres", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Original Title
            If filteredoptions.bMainOriginalTitle Then
                nResult.OriginalTitle = strOriginalTitle
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Plot
            If filteredoptions.bMainPlot AndAlso bIsScraperLanguage Then
                Dim strPlot = Parse_Plot(htmldReference)
                If strPlot IsNot Nothing Then
                    nResult.Plot = strPlot
                Else
                    strPlot = Parse_PlotFromSummaryPage(imdbId)
                    If Not String.IsNullOrEmpty(strPlot) Then
                        nResult.Plot = strPlot
                    Else
                        _Logger.Trace(String.Format("[IMDb] [GetInfo_TVShow] [ID:""{0}""] no result from ""plotsummary"" page for Plot", imdbId))
                    End If
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Poster (used only for SearchResults dialog)
            nResult.ThumbPoster = Parse_ThumbPoster(htmldReference)

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Premiered
            If filteredoptions.bMainPremiered Then
                Dim dateRelease As New Date
                If ParsePremiered(imdbId, dateRelease) Then
                    nResult.Premiered = dateRelease.ToString("yyyy-MM-dd")
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_TVShow] [ID:""{0}""] can't parse Premiered", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Rating
            If filteredoptions.bMainRating Then
                Dim nRating = Parse_Rating(htmldReference)
                If nRating IsNot Nothing Then
                    nResult.Ratings.Add(nRating)
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_TVShow] [ID:""{0}""] can't parse Rating", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Runtime
            If filteredoptions.bMainRuntime Then
                Dim strRuntime = ParseRuntime(htmldReference)
                If strRuntime IsNot Nothing Then
                    nResult.Runtime = strRuntime
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_TVShow] [ID:""{0}""] can't parse Runtime", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Studios
            If filteredoptions.bMainStudios Then
                Dim lstStudios = Parse_Studios(htmldReference)
                If lstStudios IsNot Nothing Then
                    nResult.Studios = lstStudios
                Else
                    _Logger.Trace(String.Format("[IMDb] [GetInfo_TVShow] [ID:""{0}""] can't parse Studios", imdbId))
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Title
            If filteredoptions.bMainTitle Then
                If Not String.IsNullOrEmpty(_addonSettings.ForceTitleLanguage) Then
                    nResult.Title = Parse_ForcedTitle(imdbId, strOriginalTitle)
                Else
                    nResult.Title = strOriginalTitle
                End If
            End If

            If _backgroundWorker.CancellationPending Then Return Nothing

            'Seasons and Episodes
            If scrapemodifier.withEpisodes OrElse scrapemodifier.withSeasons Then
                Dim webParsingSeasons As New HtmlWeb
                Dim htmldEpisodes As HtmlDocument = webParsingSeasons.Load(String.Concat("http://www.imdb.com/title/", imdbId, "/episodes"))

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
                    If _backgroundWorker.CancellationPending Then Return Nothing
                    GetInfo_TVSeason(nResult, nResult.UniqueIDs.IMDbId, tSeason, filteredoptions, scrapemodifier)
                    If scrapemodifier.withSeasons Then
                        nResult.KnownSeasons.Add(New MediaContainers.SeasonDetails With {.Season = tSeason})
                    End If
                Next
            End If

            Return nResult
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            Return Nothing
        End Try
    End Function

    Public Sub GetInfoAsync_Movie(ByVal imdbId As String,
                                  ByRef filteredOptions As Structures.ScrapeOptions
                                  ) Implements Interfaces.IScraper_Search.GetInfoAsync_Movie
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.GetInfo_Movie,
                                             .Parameter = imdbId,
                                             .ScrapeOptions = filteredOptions
                                             })
        End If
    End Sub

    Public Sub GetInfoAsync_Movieset(ByVal imdbId As String,
                                     ByRef filteredOptions As Structures.ScrapeOptions
                                     ) Implements Interfaces.IScraper_Search.GetInfoAsync_Movieset
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.GetInfo_Movieset,
                                             .Parameter = imdbId,
                                             .ScrapeOptions = filteredOptions
                                             })
        End If
    End Sub

    Public Sub GetInfoAsync_TVShow(ByVal imdbId As String,
                                   ByRef filteredOptions As Structures.ScrapeOptions
                                   ) Implements Interfaces.IScraper_Search.GetInfoAsync_TVShow
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.GetInfo_TVShow,
                                             .Parameter = imdbId,
                                             .ScrapeOptions = filteredOptions
                                             })
        End If
    End Sub

    Public Function GetMovieStudios(ByVal id As String) As List(Of String)
        Dim webParsingSeasons As New HtmlWeb
        Dim htmldReference As HtmlDocument = webParsingSeasons.Load(String.Concat("http://www.imdb.com/title/", id, "/reference"))
        If htmldReference IsNot Nothing Then
            Dim lstStudios = Parse_Studios(htmldReference)
            If lstStudios IsNot Nothing Then
                Return lstStudios
            End If
        End If
        Return New List(Of String)
    End Function

    Private Function Parse_Actors(ByRef htmldReference As HtmlDocument, Optional ByVal removeepisodecount As Boolean = False) As List(Of MediaContainers.Person)
        Dim nActors As New List(Of MediaContainers.Person)
        Dim strThumbsSize = Master.eAdvancedSettings.GetSetting("ActorThumbsSize", "SX1000_SY1000")
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
        If htmldReleaseInfo Is Nothing Then
            Dim webParsing As New HtmlWeb
            htmldReleaseInfo = webParsing.Load(String.Concat("http://www.imdb.com/title/", id, "/releaseinfo"))
        End If

        If htmldReleaseInfo IsNot Nothing Then
            Dim selNodes = htmldReleaseInfo.DocumentNode.SelectNodes(String.Format("//table[@id=""akas""]/tr/td[contains(text(), '{0}')]", _addonSettings.ForceTitleLanguage))
            If selNodes IsNot Nothing Then
                Dim filteredTitles = selNodes.Where(Function(f) Not HttpUtility.HtmlDecode(f.InnerText).ToLower.Contains("working title") AndAlso
                                                            Not HttpUtility.HtmlDecode(f.InnerText).ToLower.Contains("fake working title"))
                If filteredTitles IsNot Nothing AndAlso filteredTitles.Count > 0 Then
                    Return HttpUtility.HtmlDecode(filteredTitles(0).NextSibling.NextSibling.InnerText)
                End If
            End If
        End If

        'fallback
        If _addonSettings.FallBackWorldwide Then
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
        If _addonSettings.MPAADescription Then
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
        If _addonSettings.MPAADescription Then _Logger.Trace(String.Format("[IMDb] [ParseMPAA] [ID:""{0}""] can't parse full MPAA, try to parse the short rating", id))
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
                    _Logger.Trace(String.Format("[IMDb] [ParseOutline] [ID:""{0}""] no result from ""plotsummary"" page for Outline", id))
                End If
            End If
            If Not String.IsNullOrEmpty(strReferenceOutline) AndAlso Not strReferenceOutline.ToLower = "know what this is about?" Then
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
            If selNode IsNot Nothing AndAlso Not selNode.InnerText.Trim.ToLower.StartsWith("it looks like we don't have any plot summaries") Then
                Return HttpUtility.HtmlDecode(selNode.InnerText.Trim)
            Else
                _Logger.Trace(String.Format("[IMDb] [ParsePlotFromSummaryPage] [ID:""{0}""] no proper result from the ""plotsummary"" page for Outline", id))
            End If
        Else
            _Logger.Trace(String.Format("[IMDb] [ParsePlotFromSummaryPage] [ID:""{0}""] can't parse the ""plotsummary"" page", id))
        End If
        Return String.Empty
    End Function

    Private Function Parse_Rating(ByRef htmldReference As HtmlDocument) As MediaContainers.RatingDetails
        Dim selNodeRating = htmldReference.DocumentNode.SelectSingleNode("//span[@class=""ipl-rating-star__rating""]")
        Dim selNodeVotes = htmldReference.DocumentNode.SelectSingleNode("//span[@class=""ipl-rating-star__total-votes""]")
        If selNodeRating IsNot Nothing AndAlso selNodeVotes IsNot Nothing Then
            Dim dblRating As Double
            Dim iVotes As Integer
            If Double.TryParse(selNodeRating.InnerText.Trim, Globalization.NumberStyles.AllowDecimalPoint, Globalization.CultureInfo.InvariantCulture, dblRating) AndAlso
                Integer.TryParse(NumUtils.CleanVotes(Regex.Match(selNodeVotes.InnerText.Trim, "[0-9,.]+").Value), iVotes) Then
                Return New MediaContainers.RatingDetails With {
                    .Max = 10,
                    .Type = "imdb",
                    .Value = dblRating,
                    .Votes = iVotes
                }
            End If
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

    Private Function Parse_Premiered(ByRef htmldReference As HtmlDocument, ByRef releasedate As Date) As Boolean
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
            Dim selNodes = htmldReleaseInfo.DocumentNode.SelectNodes("//td[@class=""release-date-item__date""]")
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

        If _addonSettings.StudiowithDistributors Then
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

    Private Function Parse_ThumbPoster(ByRef htmldReference As HtmlDocument) As MediaContainers.Image
        Dim selNode = htmldReference.DocumentNode.SelectSingleNode("//img[@class=""titlereference-primary-image""]")
        If selNode IsNot Nothing Then
            Dim attSource = selNode.Attributes("src")
            If attSource IsNot Nothing Then
                Return New MediaContainers.Image With {.URLOriginal = attSource.Value, .URLThumb = attSource.Value}
            End If
        End If
        Return New MediaContainers.Image
    End Function

    Public Function Process_SearchResults_Movie(ByVal title As String,
                                                ByRef oDbElement As Database.DBElement,
                                                ByVal type As Enums.ScrapeType,
                                                ByVal filteredOptions As Structures.ScrapeOptions
                                                ) As MediaContainers.Movie
        Dim SearchResults = Search_By_Title_Movie(title, CInt(If(oDbElement.Movie.YearSpecified, oDbElement.Movie.Year, Nothing)))

        Try
            Select Case type
                Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                    If SearchResults.ExactMatches.Count = 1 Then
                        Return GetInfo_Movie(SearchResults.ExactMatches.Item(0).UniqueIDs.IMDbId, filteredOptions)
                    ElseIf SearchResults.PopularTitles.Count = 1 AndAlso SearchResults.PopularTitles(0).Lev <= 5 Then
                        Return GetInfo_Movie(SearchResults.PopularTitles.Item(0).UniqueIDs.IMDbId, filteredOptions)
                    ElseIf SearchResults.ExactMatches.Count = 1 AndAlso SearchResults.ExactMatches(0).Lev <= 5 Then
                        Return GetInfo_Movie(SearchResults.ExactMatches.Item(0).UniqueIDs.IMDbId, filteredOptions)
                    Else
                        Using dlgSearch As New dlgSearchResults(Me, "imdb", New List(Of String) From {"IMDb"}, Enums.ContentType.Movie)
                            Select Case dlgSearch.ShowDialog(title, oDbElement.Filename, SearchResults)
                                Case DialogResult.OK
                                    If dlgSearch.Result_Movie.UniqueIDs.IMDbIdSpecified Then
                                        Return GetInfo_Movie(dlgSearch.Result_Movie.UniqueIDs.IMDbId.ToString, filteredOptions)
                                    End If
                                Case DialogResult.Retry
                                Case DialogResult.Cancel
                            End Select
                        End Using
                    End If

                Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                    If SearchResults.ExactMatches.Count = 1 Then
                        Return GetInfo_Movie(SearchResults.ExactMatches.Item(0).UniqueIDs.IMDbId, filteredOptions)
                    End If

                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                    'check if ALL results are over lev value
                    Dim useAnyway As Boolean = False
                    If ((SearchResults.PopularTitles.Count > 0 AndAlso SearchResults.PopularTitles(0).Lev > 5) OrElse SearchResults.PopularTitles.Count = 0) AndAlso
                        ((SearchResults.ExactMatches.Count > 0 AndAlso SearchResults.ExactMatches(0).Lev > 5) OrElse SearchResults.ExactMatches.Count = 0) AndAlso
                        ((SearchResults.PartialMatches.Count > 0 AndAlso SearchResults.PartialMatches(0).Lev > 5) OrElse SearchResults.PartialMatches.Count = 0) Then
                        useAnyway = True
                    End If
                    Dim exactHaveYear As Integer = FindYear(oDbElement.Filename, SearchResults.ExactMatches)
                    Dim popularHaveYear As Integer = FindYear(oDbElement.Filename, SearchResults.PopularTitles)
                    If SearchResults.ExactMatches.Count = 1 Then
                        Return GetInfo_Movie(SearchResults.ExactMatches.Item(0).UniqueIDs.IMDbId, filteredOptions)
                    ElseIf SearchResults.ExactMatches.Count > 1 AndAlso exactHaveYear >= 0 Then
                        Return GetInfo_Movie(SearchResults.ExactMatches.Item(exactHaveYear).UniqueIDs.IMDbId, filteredOptions)
                    ElseIf SearchResults.PopularTitles.Count > 0 AndAlso popularHaveYear >= 0 Then
                        Return GetInfo_Movie(SearchResults.PopularTitles.Item(popularHaveYear).UniqueIDs.IMDbId, filteredOptions)
                    ElseIf SearchResults.ExactMatches.Count > 0 AndAlso (SearchResults.ExactMatches(0).Lev <= 5 OrElse useAnyway) Then
                        Return GetInfo_Movie(SearchResults.ExactMatches.Item(0).UniqueIDs.IMDbId, filteredOptions)
                    ElseIf SearchResults.PopularTitles.Count > 0 AndAlso (SearchResults.PopularTitles(0).Lev <= 5 OrElse useAnyway) Then
                        Return GetInfo_Movie(SearchResults.PopularTitles.Item(0).UniqueIDs.IMDbId, filteredOptions)
                    ElseIf SearchResults.PartialMatches.Count > 0 AndAlso (SearchResults.PartialMatches(0).Lev <= 5 OrElse useAnyway) Then
                        Return GetInfo_Movie(SearchResults.PartialMatches.Item(0).UniqueIDs.IMDbId, filteredOptions)
                    End If
            End Select

            Return Nothing
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            Return Nothing
        End Try
    End Function

    Public Function Process_SearchResults_TVShow(ByVal title As String,
                                                 ByRef oDbElement As Database.DBElement,
                                                 ByVal type As Enums.ScrapeType,
                                                 ByVal filteredOptions As Structures.ScrapeOptions,
                                                 ByVal scrapeModifiers As Structures.ScrapeModifiers
                                                 ) As MediaContainers.TVShow
        Dim SearchResults = Search_By_Title_TVShow(title)

        Select Case type
            Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                If SearchResults.Matches.Count = 1 Then
                    Return GetInfo_TVShow(SearchResults.Matches.Item(0).UniqueIDs.IMDbId, filteredOptions, scrapeModifiers)
                Else
                    Using dlgSearch As New dlgSearchResults(Me, "imdb", New List(Of String) From {"IMDb"}, Enums.ContentType.TVShow)
                        If dlgSearch.ShowDialog(title, oDbElement.ShowPath, SearchResults) = DialogResult.OK Then
                            If dlgSearch.Result_TVShow.UniqueIDs.IMDbIdSpecified Then
                                Return GetInfo_TVShow(dlgSearch.Result_TVShow.UniqueIDs.IMDbId, filteredOptions, scrapeModifiers)
                            End If
                        End If
                    End Using
                End If

            Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                If SearchResults.Matches.Count = 1 Then
                    Return GetInfo_TVShow(SearchResults.Matches.Item(0).UniqueIDs.IMDbId, filteredOptions, scrapeModifiers)
                End If

            Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                If SearchResults.Matches.Count > 0 Then
                    Return GetInfo_TVShow(SearchResults.Matches.Item(0).UniqueIDs.IMDbId, filteredOptions, scrapeModifiers)
                End If
        End Select

        Return Nothing
    End Function

    Private Function Search_By_Title_Movie(ByVal title As String, Optional ByVal year As Integer = 0) As List(Of MediaContainers.Movie)
        Dim SearchResults As New List(Of MediaContainers.Movie)

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

        If _addonSettings.SearchTvTitles Then
            htmldResultsTvTitles = webParsing.Load(String.Concat("http://www.imdb.com/search/title?title=", HttpUtility.UrlEncode(strTitle), "&title_type=tv_movie&view=simple"))
        End If
        If _addonSettings.SearchVideoTitles Then
            htmldResultsVideoTitles = webParsing.Load(String.Concat("http://www.imdb.com/search/title?title=", HttpUtility.UrlEncode(strTitle), "&title_type=video&view=simple"))
        End If
        If _addonSettings.SearchShortTitles Then
            htmldResultsShortTitles = webParsing.Load(String.Concat("http://www.imdb.com/search/title?title=", HttpUtility.UrlEncode(strTitle), "&title_type=short&view=simple"))
        End If
        If _addonSettings.SearchPartialTitles Then
            htmldResultsPartialTitles = webParsing.Load(String.Concat("http://www.imdb.com/find?q=", HttpUtility.UrlEncode(strTitle), "&s=tt&ttype=ft&ref_=fn_ft"))
        End If
        If _addonSettings.SearchPopularTitles Then
            htmldResultsPopularTitles = webParsing.Load(String.Concat("http://www.imdb.com/find?q=", HttpUtility.UrlEncode(strTitle), "&s=tt&ttype=ft&ref_=fn_tt_pop"))
        End If

        'Check if we've been redirected straight to the movie page
        If Regex.IsMatch(strResponseUri, REGEX_IMDBID) Then
            Return SearchResults
        End If

        'popular titles
        If htmldResultsPopularTitles IsNot Nothing Then
            Dim ApiResult = htmldResultsPopularTitles.DocumentNode.SelectNodes("//table[@class=""findList""]/tr[@class]/td[2]")
            If ApiResult IsNot Nothing Then
                For Each nResult In ApiResult
                    SearchResults.PopularTitles.Add(New MediaContainers.Movie With {
                                        .Lev = StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                        .Title = nResult.SelectSingleNode("a").InnerText,
                                        .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.Movie) With {.IMDbId = StringUtils.GetIMDbIdFromString(nResult.OuterHtml)},
                                        .Year = Regex.Match(nResult.InnerText, "\((\d{4})").Groups(1).Value
                                        })
                Next
            End If
        End If

        'partial titles
        If htmldResultsPartialTitles IsNot Nothing Then
            Dim ApiResult = htmldResultsPartialTitles.DocumentNode.SelectNodes("//table[@class=""findList""]/tr[@class]/td[2]")
            If ApiResult IsNot Nothing Then
                For Each nResult In ApiResult
                    SearchResults.PartialMatches.Add(New MediaContainers.Movie With {
                                         .Lev = StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                         .Title = nResult.SelectSingleNode("a").InnerText,
                                         .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.Movie) With {.IMDbId = StringUtils.GetIMDbIdFromString(nResult.OuterHtml)},
                                         .Year = Regex.Match(nResult.InnerText, "\((\d{4})").Groups(1).Value
                                         })
                Next
            End If
        End If

        'tv titles
        If htmldResultsTvTitles IsNot Nothing Then
            Dim ApiResult = htmldResultsTvTitles.DocumentNode.SelectNodes("//span[@title]")
            If ApiResult IsNot Nothing Then
                For Each nResult In ApiResult
                    SearchResults.TvTitles.Add(New MediaContainers.Movie With {
                                   .Lev = StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                   .Title = nResult.SelectSingleNode("a").InnerText,
                                   .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.Movie) With {.IMDbId = StringUtils.GetIMDbIdFromString(nResult.InnerHtml)},
                                   .Year = Regex.Match(nResult.SelectSingleNode("span").InnerText, "\((\d{4})").Groups(1).Value
                                   })
                Next
            End If
        End If

        'video titles
        If htmldResultsVideoTitles IsNot Nothing Then
            Dim ApiResult = htmldResultsVideoTitles.DocumentNode.SelectNodes("//span[@title]")
            If ApiResult IsNot Nothing Then
                For Each nResult In ApiResult
                    SearchResults.VideoTitles.Add(New MediaContainers.Movie With {
                                      .Lev = StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                      .Title = nResult.SelectSingleNode("a").InnerText,
                                      .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.Movie) With {.IMDbId = StringUtils.GetIMDbIdFromString(nResult.InnerHtml)},
                                      .Year = Regex.Match(nResult.SelectSingleNode("span").InnerText, "\((\d{4})").Groups(1).Value
                                      })
                Next
            End If
        End If

        'short titles
        If htmldResultsShortTitles IsNot Nothing Then
            Dim ApiResult = htmldResultsShortTitles.DocumentNode.SelectNodes("//span[@title]")
            If ApiResult IsNot Nothing Then
                For Each nResult In ApiResult
                    SearchResults.ShortTitles.Add(New MediaContainers.Movie With {
                                      .Lev = StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                      .Title = nResult.SelectSingleNode("a").InnerText,
                                      .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.Movie) With {.IMDbId = StringUtils.GetIMDbIdFromString(nResult.InnerHtml)},
                                      .Year = Regex.Match(nResult.SelectSingleNode("span").InnerText, "\((\d{4})").Groups(1).Value
                                      })
                Next
            End If
        End If

        'exact titles
        If htmldResultsExact IsNot Nothing Then
            Dim ApiResult = htmldResultsExact.DocumentNode.SelectNodes("//table[@class=""findList""]/tr[@class]/td[2]")
            If ApiResult IsNot Nothing Then
                For Each nResult In ApiResult
                    SearchResults.ExactMatches.Add(New MediaContainers.Movie With {
                                           .Lev = StringUtils.ComputeLevenshtein(StringUtils.FilterYear(strTitle).ToLower, nResult.SelectSingleNode("a").InnerText),
                                           .Title = nResult.SelectSingleNode("a").InnerText,
                                           .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.Movie) With {.IMDbId = StringUtils.GetIMDbIdFromString(nResult.InnerHtml)},
                                           .Year = Regex.Match(nResult.InnerText, "\((\d{4})").Groups(1).Value
                                           })
                Next
            End If
        End If

        Return SearchResults
    End Function

    Private Function Search_By_Title_TVShow(ByVal title As String) As List(Of MediaContainers.TVShow)
        Dim SearchResults As New List(Of MediaContainers.TVShow)

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
                        SearchResults.Add(New MediaContainers.TVShow With {
                                          .Title = attTitle.Value,
                                          .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.TVShow) With {.IMDbId = attIMDBID.Value}
                                          })
                    End If
                End If
            Next
        End If

        Return SearchResults
    End Function

    Public Sub SearchAsync_By_Title_Movie(ByVal title As String,
                                          Optional ByVal year As Integer = Nothing
                                          ) Implements Interfaces.IScraper_Search.SearchAsync_By_Title_Movie
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.Search_By_Title_Movie,
                                             .Parameter = title,
                                             .Year = year
                                             })
        End If
    End Sub

    Public Sub SearchAsync_By_Title_Movieset(ByVal title As String) Implements Interfaces.IScraper_Search.SearchAsync_By_Title_Movieset
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.Search_By_Title_Movieset,
                                             .Parameter = title
                                             })
        End If
    End Sub

    Public Sub SearchAsync_By_Title_TVShow(ByVal title As String,
                                           Optional ByVal year As Integer = Nothing
                                           ) Implements Interfaces.IScraper_Search.SearchAsync_By_Title_TVShow
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.Search_By_Title_TVShow,
                                             .Parameter = title,
                                             .Year = year
                                             })
        End If
    End Sub

    Public Sub SearchAsync_By_UniqueId_Movie(ByVal uniqueId As String) Implements Interfaces.IScraper_Search.SearchAsync_By_UniqueId_Movie
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.Search_By_UniqueId_Movie,
                                             .Parameter = uniqueId
                                             })
        End If
    End Sub

    Public Sub SearchAsync_By_UniqueId_Movieset(ByVal uniqueId As String) Implements Interfaces.IScraper_Search.SearchAsync_By_UniqueId_Movieset
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.Search_By_UniqueId_Movieset,
                                             .Parameter = uniqueId
                                             })
        End If
    End Sub

    Public Sub SearchAsync_By_UniqueId_TVShow(ByVal uniqueId As String) Implements Interfaces.IScraper_Search.SearchAsync_By_UniqueId_TVShow
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.Search_By_UniqueId_TVShow,
                                             .Parameter = uniqueId
                                             })
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim Parameter As String
        Dim ScrapeModifiers As Structures.ScrapeModifiers
        Dim ScrapeOptions As Structures.ScrapeOptions
        Dim TaskType As TaskType
        Dim Year As Integer

#End Region 'Fields

    End Structure

    Private Structure Results

#Region "Fields"

        Dim Result As Object
        Dim TaskType As TaskType

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class