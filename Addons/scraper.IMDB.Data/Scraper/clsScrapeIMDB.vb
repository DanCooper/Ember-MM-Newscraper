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

Imports System.Globalization
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Namespace IMDB

    Public Class SearchResults_Movie

#Region "Fields"

        Private _ExactMatches As New List(Of MediaContainers.Movie)
        Private _PartialMatches As New List(Of MediaContainers.Movie)
        Private _PopularTitles As New List(Of MediaContainers.Movie)
        Private _TvTitles As New List(Of MediaContainers.Movie)
        Private _VideoTitles As New List(Of MediaContainers.Movie)
        Private _ShortTitles As New List(Of MediaContainers.Movie)

#End Region 'Fields

#Region "Properties"

        Public Property ExactMatches() As List(Of MediaContainers.Movie)
            Get
                Return _ExactMatches
            End Get
            Set(ByVal value As List(Of MediaContainers.Movie))
                _ExactMatches = value
            End Set
        End Property

        Public Property PartialMatches() As List(Of MediaContainers.Movie)
            Get
                Return _PartialMatches
            End Get
            Set(ByVal value As List(Of MediaContainers.Movie))
                _PartialMatches = value
            End Set
        End Property

        Public Property PopularTitles() As List(Of MediaContainers.Movie)
            Get
                Return _PopularTitles
            End Get
            Set(ByVal value As List(Of MediaContainers.Movie))
                _PopularTitles = value
            End Set
        End Property

        Public Property TvTitles() As List(Of MediaContainers.Movie)
            Get
                Return _TvTitles
            End Get
            Set(ByVal value As List(Of MediaContainers.Movie))
                _TvTitles = value
            End Set
        End Property

        Public Property VideoTitles() As List(Of MediaContainers.Movie)
            Get
                Return _VideoTitles
            End Get
            Set(ByVal value As List(Of MediaContainers.Movie))
                _VideoTitles = value
            End Set
        End Property

        Public Property ShortTitles() As List(Of MediaContainers.Movie)
            Get
                Return _ShortTitles
            End Get
            Set(ByVal value As List(Of MediaContainers.Movie))
                _ShortTitles = value
            End Set
        End Property

#End Region 'Properties

    End Class

    Public Class SearchResults_TVShow

#Region "Fields"

        Private _Matches As New List(Of MediaContainers.TVShow)

#End Region 'Fields

#Region "Properties"

        Public Property Matches() As List(Of MediaContainers.TVShow)
            Get
                Return _Matches
            End Get
            Set(ByVal value As List(Of MediaContainers.TVShow))
                _Matches = value
            End Set
        End Property

#End Region 'Properties

    End Class

    Public Class Scraper

#Region "Fields"

        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Friend WithEvents bwIMDB As New System.ComponentModel.BackgroundWorker

        Private Const LINK_PATTERN As String = "<a[\s]+[^>]*?href[\s]?=[\s\""\']*(?<url>.*?)[\""\']*.*?>(?<name>[^<]+|.*?)?<\/a>"
        Private Const ACTORTABLE_PATTERN As String = "<table class=""cast"">(.*?)</table>"
        Private Const HREF_PATTERN As String = "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>"
        Private Const HREF_PATTERN_2 As String = "<a\shref=[""""'](?<url>.*?)[""""'].*?>(?<name>.*?)</a>"
        Private Const HREF_PATTERN_3 As String = "<a href=""/search/title\?certificates=[^""]*"">([^<]*):([^<]*)</a>[^<]*(<i>([^<]*)</i>)?"
        Private Const HREF_PATTERN_4 As String = "<a.*?href=[""']/(title/tt\d{7}/|name/nm\d{7}/)[""'].*?>(?<text>.*?)</a>"
        Private Const IMDB_ID_REGEX As String = "tt\d\d\d\d\d\d\d"
        Private Const IMG_PATTERN As String = "<img src=""(?<thumb>.*?)"" width=""\d{1,3}"" height=""\d{1,3}"" border="".{1,3}"">"
        Private Const MOVIE_TITLE_PATTERN As String = "(?<=<(title)>).*(?=<\/\1>)"
        Private Const TABLE_PATTERN As String = "<table.*?>\n?(.*?)</table>"
        Private Const TABLE_PATTERN_TV As String = "<table class=""results"">(.*?)</table>"
        Private Const TD_PATTERN_1 As String = "<td\sclass=""nm"">(.*?)</td>"
        Private Const TD_PATTERN_2 As String = "(?<=<td\sclass=""char"">)(.*?)(?=</td>)(\s\(.*?\))?"
        Private Const TD_PATTERN_3 As String = "<td\sclass=""hs"">(.*?)</td>"
        Private Const TD_PATTERN_4 As String = "<td>(?<title>.*?)</td>"
        Private Const TITLE_PATTERN As String = "<a\shref=[""""'](?<url>.*?)[""""'].*?>(?<name>.*?)</a>((\s)+?(\((?<year>\d{4})(\/.*?)?\)))?((\s)+?(\((?<type>.*?)\)))?"
        Private Const TR_PATTERN As String = "<tr\sclass="".*?"">(.*?)</tr>"
        Private Const TvTITLE_PATTERN As String = "<a\shref=[""'](?<url>.*?)[""']\stitle=[""'](?<name>.*?)((\s)+?(\((?<year>\d{4})))"
        Private Const TVSHOWTITLE_PATTERN As String = "<tr class.*?>.*?<a href=""\/title\/(?<IMDB>tt\d*)\/"">(?<TITLE>.*?)<\/a>.*?year_type"">\((?<YEAR>\d*).*?<\/tr>"

        Private sPoster As String
        Private intHTTP As HTTP = Nothing

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
        Public Event SearchInfoDownloaded_Movie(ByVal sPoster As String, ByVal bSuccess As Boolean)
        Public Event SearchInfoDownloaded_TV(ByVal sPoster As String, ByVal bSuccess As Boolean)
        Public Event SearchResultsDownloaded_Movie(ByVal mResults As IMDB.SearchResults_Movie)
        Public Event SearchResultsDownloaded_TV(ByVal mResults As IMDB.SearchResults_TVShow)

#End Region 'Events

#Region "Methods"

        Public Sub New(ByVal SpecialSettings As IMDB_Data.SpecialSettings)
            _SpecialSettings = SpecialSettings
        End Sub

        Public Sub CancelAsync()

            If bwIMDB.IsBusy Then
                If intHTTP IsNot Nothing Then
                    intHTTP.Cancel()
                End If
                bwIMDB.CancelAsync()
            End If

            While bwIMDB.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End Sub
        ''' <summary>
        '''  Scrape MovieDetails from IMDB
        ''' </summary>
        ''' <param name="strID">IMDBID (without "tt") of movie to be scraped</param>
        ''' <param name="nMovie">Container of scraped movie data</param>
        ''' <param name="FullCrew">Module setting: Scrape full cast?</param>
        ''' <param name="GetPoster">Scrape posters for the movie?</param>
        ''' <param name="Options">Module settings<param>
        ''' <param name="IsSearch">Not used at moment</param>
        ''' <param name="WorldWideTitleFallback">Module setting: Use worldwide title for Title tag if preferred language title isnt found</param>
        ''' <param name="ForceTitleLanguage">Module setting: preferred language title</param>
        ''' <returns>True: success, false: no success</returns>
        ''' <remarks></remarks>
        Public Function GetMovieInfo(ByVal strID As String, ByRef nMovie As MediaContainers.Movie, ByVal FullCrew As Boolean, ByVal GetPoster As Boolean, ByVal FilteredOptions As Structures.ScrapeOptions_Movie, ByVal IsSearch As Boolean, ByVal WorldWideTitleFallback As Boolean, ByVal ForceTitleLanguage As String, ByVal CountryAbbreviation As Boolean, ByVal StudiowithDistributors As Boolean) As Boolean
            Try
                If bwIMDB.CancellationPending Then Return Nothing

                'clear nMovie from search results
                nMovie.Clear()

                Dim HTML As String
                intHTTP = New HTTP
                HTML = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", strID, "/combined"))
                intHTTP.Dispose()
                intHTTP = Nothing

                If bwIMDB.CancellationPending Then Return Nothing

                Dim PlotHtml As String
                intHTTP = New HTTP
                PlotHtml = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", strID, "/plotsummary"))
                intHTTP.Dispose()
                intHTTP = Nothing

                nMovie.IMDBID = strID
                nMovie.Scrapersource = "IMDB"

                If bwIMDB.CancellationPending Then Return Nothing

                Dim scrapedresult As String = ""

                Dim OriginalTitle As String = Regex.Match(HTML, MOVIE_TITLE_PATTERN).ToString


                'Original Title
                If FilteredOptions.bOriginalTitle Then
                    nMovie.OriginalTitle = CleanTitle(Web.HttpUtility.HtmlDecode(Regex.Match(OriginalTitle, ".*(?=\s\(\d+.*?\))").ToString)).Trim
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Title
                If FilteredOptions.bTitle Then
                    If Not String.IsNullOrEmpty(ForceTitleLanguage) Then
                        nMovie.Title = GetForcedTitle(strID, nMovie.OriginalTitle, WorldWideTitleFallback, ForceTitleLanguage)
                    Else
                        nMovie.Title = CleanTitle(Web.HttpUtility.HtmlDecode(Regex.Match(OriginalTitle, ".*(?=\s\(\d+.*?\))").ToString)).Trim
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Poster for search result
                If GetPoster Then
                    sPoster = Regex.Match(Regex.Match(HTML, "(?<=\b(name=""poster"")).*\b[</a>]\b").ToString, "(?<=\b(src=)).*\b(?=[</a>])").ToString.Replace("""", String.Empty).Replace("/></", String.Empty)
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Year
                If FilteredOptions.bYear Then
                    nMovie.Year = Regex.Match(OriginalTitle, "(?<=\()\d+(?=.*\))", RegexOptions.RightToLeft).ToString
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Certifications
                If FilteredOptions.bCert Then
                    Dim D, W As Integer
                    D = HTML.IndexOf("<h5>Certification:</h5>")
                    If D > 0 Then
                        W = HTML.IndexOf("</div>", D)
                        Dim rCert As MatchCollection = Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN_3)

                        If rCert.Count > 0 Then
                            Dim Certs = From M In rCert Select N = String.Format("{0}:{1}", DirectCast(M, Match).Groups(1).ToString.Trim, DirectCast(M, Match).Groups(2).ToString.Trim) Order By N Ascending
                            For Each tCert In Certs
                                nMovie.Certifications.Add(tCert.ToString.Replace("West", String.Empty).Trim)
                            Next
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'MPAA
                If FilteredOptions.bMPAA Then
                    Dim D, W, tempD As Integer
                    tempD = If(HTML.IndexOf("<h5><a href=""/mpaa"">MPAA</a>:</h5>") > 0, HTML.IndexOf("<h5><a href=""/mpaa"">MPAA</a>:</h5>"), 0)
                    D = If(tempD > 0, HTML.IndexOf("<div class=""info-content"">", tempD), 0)
                    W = If(D > 0, HTML.IndexOf("</div", D), 0)
                    nMovie.MPAA = If(D > 0 AndAlso W > 0, Web.HttpUtility.HtmlDecode(HTML.Substring(D, W - D).Remove(0, 26)).Trim(), String.Empty)
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Release Date
                If FilteredOptions.bRelease Then
                    Dim RelDate As Date
                    Dim sRelDate As MatchCollection = Regex.Matches(HTML, "<h5>Release Date:</h5>.*?(?<DATE>\d+\s\w+\s\d\d\d\d\s)", RegexOptions.Singleline)
                    If sRelDate.Count > 0 Then
                        If Date.TryParse(sRelDate.Item(0).Groups(1).Value, RelDate) Then
                            'FormatDateTime interprets date according to the CurrentCulture of user -> different results depending on user country!
                            '  nMovie.ReleaseDate = Strings.FormatDateTime(RelDate, DateFormat.ShortDate).ToString
                            'always save date in same date format not depending on users language setting!
                            nMovie.ReleaseDate = RelDate.ToString("yyyy-MM-dd")
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Rating
                If FilteredOptions.bRating Then
                    Dim RegexRating As String = Regex.Match(HTML, "\b\d\W\d/\d\d").ToString
                    If Not String.IsNullOrEmpty(RegexRating) Then
                        nMovie.Rating = RegexRating.Split(Convert.ToChar("/")).First.Trim
                    End If
                    nMovie.Votes = Regex.Match(HTML, "class=""tn15more"">([0-9,]+) votes</a>").Groups(1).Value.Trim
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Trailer
                If FilteredOptions.bTrailer Then
                    'Get first IMDB trailer if possible
                    Dim trailers As List(Of String) = GetTrailers(nMovie.IMDBID)
                    nMovie.Trailer = trailers.FirstOrDefault()
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Top250
                'ie: <a href="/chart/top?tt0167260">Top 250: #13</a>
                If FilteredOptions.bTop250 Then
                    nMovie.Top250 = Regex.Match(HTML, String.Concat("/chart/top\?tt", nMovie.IMDBID, """>Top 250: #([0-9]+)</a>")).Groups(1).Value.Trim
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Actors
                If FilteredOptions.bCast Then
                    'Find all cast of the movie
                    'Match the table only 1 time
                    Dim ActorsTable As String = Regex.Match(HTML, ACTORTABLE_PATTERN).ToString
                    Dim ThumbsSize = clsAdvancedSettings.GetSetting("ActorThumbsSize", "SY275_SX400")

                    Dim rCast As MatchCollection = Regex.Matches(ActorsTable, TR_PATTERN)

                    Dim Cast1 = From M In rCast _
                                Let m1 = Regex.Match(Regex.Match(M.ToString, TD_PATTERN_1).ToString, HREF_PATTERN) _
                                Let m2 = Regex.Match(M.ToString, TD_PATTERN_2).ToString _
                                Let m3 = Regex.Match(Regex.Match(M.ToString, TD_PATTERN_3).ToString, IMG_PATTERN) _
                                Select New MediaContainers.Person(Web.HttpUtility.HtmlDecode(m1.Groups("name").ToString.Trim), _
                                Web.HttpUtility.HtmlDecode(m2.ToString.Trim), _
                                If(m3.Groups("thumb").ToString.IndexOf("addtiny") > 0 OrElse m3.Groups("thumb").ToString.IndexOf("no_photo") > 0, String.Empty, Web.HttpUtility.HtmlDecode(m3.Groups("thumb").ToString.Trim).Replace("._SX23_SY30_.jpg", String.Concat("._", ThumbsSize, "_.jpg")))) Take 999999

                    Dim Cast As List(Of MediaContainers.Person) = Cast1.ToList

                    'Clean up the actors list
                    For Each Ps As MediaContainers.Person In Cast
                        For Each sMatch As Match In Regex.Matches(Ps.Role, HREF_PATTERN)
                            Ps.Role = Ps.Role.Replace(sMatch.Value, sMatch.Groups("name").Value.ToString.Trim)
                        Next
                        ' Dim a_patterRegex = Regex.Match(Ps.Role, HREF_PATTERN)
                        ' If a_patterRegex.Success Then Ps.Role = a_patterRegex.Groups("name").ToString.Trim
                    Next

                    'only update nMovie if scraped result is not empty/nothing!
                    If Cast.Count > 0 Then
                        nMovie.Actors = Cast
                    End If

                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Tagline
                If FilteredOptions.bTagline Then
                    Dim D, W, tempD As Integer
                    tempD = If(HTML.IndexOf("<h5>Tagline:</h5>") > 0, HTML.IndexOf("<h5>Tagline:</h5>"), 0)
                    D = If(tempD > 0, HTML.IndexOf("<div class=""info-content"">", tempD), 0)
                    Dim lHtmlIndexOf As Integer = If(D > 0, HTML.IndexOf("<a class=""tn15more inline""", D), 0)
                    Dim TagLineEnd As Integer = If(lHtmlIndexOf > 0, lHtmlIndexOf, 0)
                    If D > 0 Then W = If(TagLineEnd > 0, TagLineEnd, HTML.IndexOf("</div>", D))
                    nMovie.Tagline = If(D > 0 AndAlso W > 0, Web.HttpUtility.HtmlDecode(HTML.Substring(D, W - D).Replace("<h5>Tagline:</h5>", String.Empty).Split(Environment.NewLine.ToCharArray)(1)).Trim, String.Empty)
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Director
                If FilteredOptions.bDirector Then
                    Dim D, W As Integer
                    'Get the directors
                    D = If(HTML.IndexOf("<h5>Director:</h5>") > 0, HTML.IndexOf("<h5>Director:</h5>"), HTML.IndexOf("<h5>Directors:</h5>"))
                    W = If(D > 0, HTML.IndexOf("</div>", D), 0)
                    'got any director(s) ?
                    If D > 0 AndAlso Not W <= 0 Then
                        'get only the first director's name
                        Dim rDir As MatchCollection = Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN)
                        Dim Dir = From M In rDir Where Not DirectCast(M, Match).Groups("name").ToString.Contains("more") _
                                  Select Web.HttpUtility.HtmlDecode(DirectCast(M, Match).Groups("name").ToString)
                        'only update nMovie if scraped result is not empty/nothing!
                        If Dir.Count > 0 Then
                            ' nMovie.Director = Strings.Join(Dir.ToArray, " / ").Trim
                            nMovie.Directors.AddRange(Dir.ToList)
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Countries
                If FilteredOptions.bCountry Then
                    Dim D, W As Integer
                    D = If(HTML.IndexOf("<h5>Country:</h5>") > 0, HTML.IndexOf("<h5>Country:</h5>"), HTML.IndexOf("<h5>Countries:</h5>"))
                    W = If(D > 0, HTML.IndexOf("</div>", D), 0)
                    'got any country ?
                    If D > 0 AndAlso Not W <= 0 Then
                        'get only the first country's name
                        Dim rCou As MatchCollection = Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN)
                        Dim Cou = From M In rCou Where Not DirectCast(M, Match).Groups("name").ToString.Contains("more") _
                                  Select Web.HttpUtility.HtmlDecode(DirectCast(M, Match).Groups("name").ToString)

                        'only update nMovie if scraped result is not empty/nothing!
                        If Cou.Count > 0 Then
                            If CountryAbbreviation = False Then
                                For Each entry In Cou
                                    entry = entry.Replace("USA", "United States of America")
                                    entry = entry.Replace("UK", "United Kingdom")
                                    nMovie.Countries.Add(entry)
                                Next
                            Else
                                nMovie.Countries.AddRange(Cou.ToList)
                            End If

                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Genres
                If FilteredOptions.bGenre Then
                    Dim D, W As Integer
                    D = HTML.IndexOf("<h5>Genre:</h5>")
                    'Check if doesnt find genres
                    If D > 0 Then
                        W = HTML.IndexOf("</div>", D)

                        If W > 0 Then
                            Dim rGenres As MatchCollection = Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN)
                            Dim Gen = From M In rGenres _
                                      Select N = Web.HttpUtility.HtmlDecode(DirectCast(M, Match).Groups("name").ToString) Where Not N.Contains("more") Take 999999
                            If Gen.Count > 0 Then
                                nMovie.Genres.AddRange(Gen.ToList)
                            End If
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Outline
                If FilteredOptions.bOutline Then
                    Dim D, W, tempD As Integer
                    Try
                        If nMovie.Title.Contains("(VG)") Then
                            D = If(HTML.IndexOf("<h5>Plot Summary:</h5>") > 0, HTML.IndexOf("<h5>Plot Summary:</h5>"), HTML.IndexOf("<h5>Tagline:</h5>"))
                            If D > 0 Then W = HTML.IndexOf("</div>", D)
                        Else
                            tempD = If(HTML.IndexOf("<h5>Plot:</h5>") > 0, HTML.IndexOf("<h5>Plot:</h5>"), HTML.IndexOf("<h5>Plot Summary:</h5>"))
                            D = If(tempD > 0, HTML.IndexOf("<div class=""info-content"">", tempD), 0)
                            If D <= 0 Then D = HTML.IndexOf("<h5>Plot Synopsis:</h5>")
                            If D > 0 Then
                                W = HTML.IndexOf("<a class=", D)
                                If W > 0 Then
                                    W = HTML.IndexOf("</div>", D)
                                Else
                                    '   IMnMovie.Outline = String.Empty
                                    GoTo mplot
                                End If
                            Else
                                'IMnMovie.Outline = String.Empty
                                GoTo mPlot 'This plot synopsis is empty
                            End If
                        End If

                        Dim PlotOutline As String = HTML.Substring(D, W - D).Remove(0, 26)

                        PlotOutline = Web.HttpUtility.HtmlDecode(If(PlotOutline.Contains("is empty") OrElse PlotOutline.Contains("View full synopsis") _
                                           , String.Empty, PlotOutline.Replace("|", String.Empty).Replace("&raquo;", String.Empty)).Trim)
                        'only update nMovie if scraped result is not empty/nothing!
                        If Not String.IsNullOrEmpty(PlotOutline) Then
                            'check if outline has links to other IMDB entry
                            For Each rMatch As Match In Regex.Matches(PlotOutline, HREF_PATTERN_4)
                                PlotOutline = PlotOutline.Replace(rMatch.Value, rMatch.Groups("text").Value.Trim)
                            Next
                            nMovie.Outline = Regex.Replace(PlotOutline, HREF_PATTERN, String.Empty).Trim
                        End If

                    Catch ex As Exception
                    End Try
                End If

                If bwIMDB.CancellationPending Then Return Nothing

mPlot:          'Plot
                If FilteredOptions.bPlot Then
                    Dim FullPlotS As String = Regex.Match(PlotHtml, "<p class=""plotSummary"">(.*?)</p>", RegexOptions.Singleline Or RegexOptions.IgnoreCase Or RegexOptions.Multiline).Groups(1).Value.ToString.Trim
                    Dim FullPlotO As String = Regex.Match(PlotHtml, "<li class=""odd"">\s*<p>(.*?)<br/>", RegexOptions.Singleline Or RegexOptions.IgnoreCase Or RegexOptions.Multiline).Groups(1).Value.ToString.Trim
                    Dim FullPlotE As String = Regex.Match(PlotHtml, "<li class=""even"">\s*<p>(.*?)<br/>", RegexOptions.Singleline Or RegexOptions.IgnoreCase Or RegexOptions.Multiline).Groups(1).Value.ToString.Trim
                    Dim FullPlot As String = If(Not String.IsNullOrEmpty(FullPlotS), FullPlotS, If(Not String.IsNullOrEmpty(FullPlotO), FullPlotO, FullPlotE))
                    FullPlot = Regex.Replace(FullPlot, "<a(.*?)>", "")
                    FullPlot = Regex.Replace(FullPlot, "</a>", "")
                    'only update nMovie if scraped result is not empty/nothing!
                    If Not String.IsNullOrEmpty(FullPlot) Then
                        nMovie.Plot = FullPlot
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Duration
                If FilteredOptions.bRuntime Then
                    scrapedresult = Web.HttpUtility.HtmlDecode(Regex.Match(HTML, "<h5>Runtime:</h5>[^0-9]*([^<]*)").Groups(1).Value.Trim)
                    'only update nMovie if scraped result is not empty/nothing!
                    If Not String.IsNullOrEmpty(scrapedresult) Then
                        'examples:
                        ' <h5>Runtime:</h5><div class="info-content">93 min </div> OR
                        ' <h5>Runtime:</h5><div class="info-content">"94 min  | USA:102 min (unrated version)</div>
                        ' <h5>Runtime:</h5><div class="info-content">Thailand: 89 min  | USA:93 min </div>
                        '  scrapedresult = Web.HttpUtility.HtmlDecode(Regex.Match(HTML, "<h5>Runtime:</h5>[^0-9]*([^<]*)").Groups(1).Value.Trim)
                        Dim Match As Match = Regex.Match(HTML, "Runtime:(\s*<((?<!>).)+)+(?<length>\d+|((?!</div|<h).)+)", RegexOptions.IgnoreCase)
                        If Match.Success Then
                            If Regex.IsMatch(Match.Groups("length").Value, "^\d+$") Then
                                scrapedresult = Match.Groups("length").Value
                            ElseIf Regex.IsMatch(Match.Groups("length").Value, "\d+") Then
                                scrapedresult = Regex.Match(Match.Groups("length").Value, "\d+").Value
                            End If
                            nMovie.Runtime = scrapedresult
                        End If
                    End If
                End If

                'Studios
                If FilteredOptions.bStudio Then
                    Dim D, W As Integer
                    D = HTML.IndexOf("<b class=""blackcatheader"">Production Companies</b>")
                    If D > 0 Then W = HTML.IndexOf("</ul>", D)
                    If D > 0 AndAlso W > 0 Then
                        'only get the first one
                        Dim Ps = From P1 In Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN) _
                                 Where Not DirectCast(P1, Match).Groups("name").ToString = String.Empty _
                                 Select Studio = Web.HttpUtility.HtmlDecode(DirectCast(P1, Match).Groups("name").ToString)
                        '  nMovie.Studio = Ps(0).ToString.Trim
                        'only update nMovie if scraped result is not empty/nothing!
                        If Ps.Count > 0 Then
                            nMovie.Studios.AddRange(Ps.ToList)
                        End If
                    End If
                    If StudiowithDistributors Then
                        D = HTML.IndexOf("<b class=""blackcatheader"">Distributors</b>")
                        If D > 0 Then W = HTML.IndexOf("</ul>", D)
                        If D > 0 AndAlso W > 0 Then
                            Dim distributor_pattern As String = "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>(?<releaseinfo>.*?)</li>"
                            'example of DISTRIBUTOR_PATTERN input string: 
                            '<li><a href="/company/co0015030/">Alfa Films</a> (2015) (Argentina) (theatrical)</li>
                            '<li><a href="/company/co0481930/">Bravos Pictures</a> (2015) (Hong Kong) (theatrical)</li><li>
                            Dim Ps = From P1 In Regex.Matches(HTML.Substring(D, W - D), distributor_pattern) _
                                     Where Not DirectCast(P1, Match).Groups("name").ToString = String.Empty AndAlso DirectCast(P1, Match).Groups("releaseinfo").ToString.Contains(ForceTitleLanguage) _
                                     Select Studio = Web.HttpUtility.HtmlDecode(DirectCast(P1, Match).Groups("name").ToString)
                            '  nMovie.Studio = Ps(0).ToString.Trim
                            'only update nMovie if scraped result is not empty/nothing!
                            If Ps.Count > 0 Then
                                For Each item In Ps.ToList
                                    If nMovie.Studios.Contains(item) = False Then
                                        nMovie.Studios.Add(item)
                                    End If
                                Next
                            End If
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Writers
                If FilteredOptions.bWriters Then
                    Dim D, W As Integer
                    D = HTML.IndexOf("<h5>Writer")
                    If D > 0 Then W = HTML.IndexOf("</div>", D)
                    If D > 0 AndAlso W > 0 Then
                        Dim q = From M In Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN) _
                                Where Not DirectCast(M, Match).Groups("name").ToString.Trim = "more" _
                                AndAlso Not DirectCast(M, Match).Groups("name").ToString.Trim = "(more)" _
                                AndAlso Not DirectCast(M, Match).Groups("name").ToString.Trim = "WGA" _
                                AndAlso Not DirectCast(M, Match).Groups("name").ToString.Trim.Contains("see more") _
                                Select Writer = Web.HttpUtility.HtmlDecode(String.Concat(DirectCast(M, Match).Groups("name").ToString, If(FullCrew, " (writer)", String.Empty)))

                        'only update nMovie if scraped result is not empty/nothing!
                        If q.Count > 0 Then
                            nMovie.Credits.AddRange(q.ToList) 'Strings.Join(q.ToArray, " / ").Trim
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Others
                'Get All Other Info
                If FullCrew OrElse FilteredOptions.bMusicBy OrElse FilteredOptions.bProducers OrElse FilteredOptions.bOtherCrew Then
                    Dim D, W As Integer
                    D = HTML.IndexOf("Directed by</a></h5>")
                    If D > 0 Then W = HTML.IndexOf("</body>", D)
                    If D > 0 AndAlso W > 0 Then
                        Dim qTables As MatchCollection = Regex.Matches(HTML.Substring(D, W - D), TABLE_PATTERN)

                        For Each M As Match In qTables

                            If bwIMDB.CancellationPending Then Return Nothing

                            'Producers
                            If (FilteredOptions.bProducers OrElse FullCrew) AndAlso M.ToString.Contains("Produced by</a></h5>") Then
                                Dim Pr = From Po In Regex.Matches(M.ToString, "<td\svalign=""top"">(.*?)</td>") _
                                Where Not Po.ToString.Contains(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/Glossary/")) _
                                Let P1 = Regex.Match(Po.ToString, HREF_PATTERN_2) _
                                Where Not String.IsNullOrEmpty(P1.Groups("name").ToString) _
                                Select Producer = Web.HttpUtility.HtmlDecode(String.Concat(P1.Groups("name").ToString, " (producer)"))
                                'only update nMovie if scraped result is not empty/nothing!
                                If Pr.Count > 0 Then
                                    'nMovie.OldCredits = String.Concat(nMovie.OldCredits, " / ", Strings.Join(Pr.ToArray, " / ").Trim)
                                    nMovie.Credits.AddRange(Pr.ToList)
                                End If
                            End If

                            'Music by
                            If (FilteredOptions.bMusicBy OrElse FullCrew) AndAlso M.ToString.Contains("Original Music by</a></h5>") Then
                                Dim Mu = From Mo In Regex.Matches(M.ToString, "<td\svalign=""top"">(.*?)</td>") _
                                Let M1 = Regex.Match(Mo.ToString, HREF_PATTERN) _
                                Where Not String.IsNullOrEmpty(M1.Groups("name").ToString) _
                                Select Musician = Web.HttpUtility.HtmlDecode(String.Concat(M1.Groups("name").ToString, " (music by)"))
                                'only update nMovie if scraped result is not empty/nothing!
                                If Mu.Count > 0 Then
                                    '   nMovie.OldCredits = String.Concat(nMovie.OldCredits, " / ", Strings.Join(Mu.ToArray, " / ").Trim)
                                    nMovie.Credits.AddRange(Mu.ToList)
                                End If
                            End If

                        Next
                    End If

                    If bwIMDB.CancellationPending Then Return Nothing

                    'Special Effects
                    If (FilteredOptions.bOtherCrew OrElse FullCrew) Then
                        D = HTML.IndexOf("<b class=""blackcatheader"">Special Effects</b>")
                        If D > 0 Then W = HTML.IndexOf("</ul>", D)
                        If D > 0 AndAlso W > 0 Then
                            Dim Ps = From P1 In Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN) _
                                     Where Not String.IsNullOrEmpty(DirectCast(P1, Match).Groups("name").ToString) _
                                     Select Studio = Web.HttpUtility.HtmlDecode(DirectCast(P1, Match).Groups("name").ToString)
                            'only update nMovie if scraped result is not empty/nothing!
                            If Ps.Count > 0 Then
                                ' nMovie.OldCredits = String.Concat(nMovie.OldCredits, " / ", Strings.Join(Ps.ToArray, " / ").Trim)
                                nMovie.Credits.AddRange(Ps.ToList)
                            End If
                        End If
                    End If
                End If

                Return True
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function
        ''' <summary>
        '''  Scrape TV Show details from TMDB
        ''' </summary>
        ''' <param name="strID">TMDB ID of tv show to be scraped</param>
        ''' <param name="nMovie">Container of scraped tv show data</param>
        ''' <param name="GetPoster">Scrape posters for the movie?</param>
        ''' <param name="Options">Module settings<param>
        ''' <param name="IsSearch">Not used at moment</param>
        ''' <returns>True: success, false: no success</returns>
        Public Function GetTVShowInfo(ByVal strID As String, ByRef nShow As MediaContainers.TVShow, ByVal GetPoster As Boolean, ByVal FilteredOptions As Structures.ScrapeOptions_TV, ByVal IsSearch As Boolean, ByVal withEpisodes As Boolean) As Boolean
            If String.IsNullOrEmpty(strID) OrElse strID.Length < 2 Then Return False

            Try
                If bwIMDB.CancellationPending Then Return Nothing

                'clear nMovie from search results
                nShow.Clear()

                Dim HTML As String
                intHTTP = New HTTP
                HTML = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/", strID, "/combined"))
                intHTTP.Dispose()
                intHTTP = Nothing

                If bwIMDB.CancellationPending Then Return Nothing

                Dim PlotHtml As String
                intHTTP = New HTTP
                PlotHtml = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/", strID, "/plotsummary"))
                intHTTP.Dispose()
                intHTTP = Nothing

                nShow.IMDB = strID
                nShow.Scrapersource = "IMDB"

                If bwIMDB.CancellationPending Then Return Nothing

                Dim scrapedresult As String = ""

                Dim OriginalTitle As String = Regex.Match(HTML, MOVIE_TITLE_PATTERN).ToString

                'Title
                If FilteredOptions.bShowTitle Then
                    nShow.Title = CleanTitle(Web.HttpUtility.HtmlDecode(Regex.Match(OriginalTitle, ".*(?=\s\(\d+.*?\))").ToString)).Trim
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Poster for search result
                If GetPoster Then
                    sPoster = Regex.Match(Regex.Match(HTML, "(?<=\b(name=""poster"")).*\b[</a>]\b").ToString, "(?<=\b(src=)).*\b(?=[</a>])").ToString.Replace("""", String.Empty).Replace("/></", String.Empty)
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Certifications
                If FilteredOptions.bShowCert Then
                    Dim D, W As Integer
                    D = HTML.IndexOf("<h5>Certification:</h5>")
                    If D > 0 Then
                        W = HTML.IndexOf("</div>", D)
                        Dim rCert As MatchCollection = Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN_3)

                        If rCert.Count > 0 Then
                            Dim Certs = From M In rCert Select N = String.Format("{0}:{1}", DirectCast(M, Match).Groups(1).ToString.Trim, DirectCast(M, Match).Groups(2).ToString.Trim) Order By N Ascending
                            For Each tCert In Certs
                                nShow.Certifications.Add(tCert.ToString.Replace("West", String.Empty).Trim)
                            Next
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'MPAA
                If FilteredOptions.bShowMPAA Then
                    Dim D, W, tempD As Integer
                    tempD = If(HTML.IndexOf("<h5><a href=""/mpaa"">MPAA</a>:</h5>") > 0, HTML.IndexOf("<h5><a href=""/mpaa"">MPAA</a>:</h5>"), 0)
                    D = If(tempD > 0, HTML.IndexOf("<div class=""info-content"">", tempD), 0)
                    W = If(D > 0, HTML.IndexOf("</div", D), 0)
                    nShow.MPAA = If(D > 0 AndAlso W > 0, Web.HttpUtility.HtmlDecode(HTML.Substring(D, W - D).Remove(0, 26)).Trim(), String.Empty)
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Release Date
                If FilteredOptions.bShowPremiered Then
                    Dim RelDate As Date
                    Dim sRelDate As MatchCollection = Regex.Matches(HTML, "<h5>Release Date:</h5>.*?(?<DATE>\d+\s\w+\s\d\d\d\d\s)", RegexOptions.Singleline)
                    If sRelDate.Count > 0 Then
                        If Date.TryParse(sRelDate.Item(0).Groups(1).Value, RelDate) Then
                            'FormatDateTime interprets date according to the CurrentCulture of user -> different results depending on user country!
                            '  nMovie.ReleaseDate = Strings.FormatDateTime(RelDate, DateFormat.ShortDate).ToString
                            'always save date in same date format not depending on users language setting!
                            nShow.Premiered = RelDate.ToString("yyyy-MM-dd")
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Rating
                If FilteredOptions.bShowRating Then
                    Dim RegexRating As String = Regex.Match(HTML, "\b\d\W\d/\d\d").ToString
                    If String.IsNullOrEmpty(RegexRating) = False Then
                        nShow.Rating = RegexRating.Split(Convert.ToChar("/")).First.Trim
                    End If
                    nShow.Votes = Regex.Match(HTML, "class=""tn15more"">([0-9,]+) votes</a>").Groups(1).Value.Trim
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Actors
                If FilteredOptions.bShowActors Then
                    'Find all cast of the tv show
                    'Match the table only 1 time
                    Dim ActorsTable As String = Regex.Match(HTML, ACTORTABLE_PATTERN).ToString
                    Dim ThumbsSize = clsAdvancedSettings.GetSetting("ActorThumbsSize", "SY275_SX400")

                    Dim rCast As MatchCollection = Regex.Matches(ActorsTable, TR_PATTERN)

                    Dim Cast1 = From M In rCast _
                                Let m1 = Regex.Match(Regex.Match(M.ToString, TD_PATTERN_1).ToString, HREF_PATTERN) _
                                Let m2 = Regex.Match(M.ToString, TD_PATTERN_2).ToString _
                                Let m3 = Regex.Match(Regex.Match(M.ToString, TD_PATTERN_3).ToString, IMG_PATTERN) _
                                Select New MediaContainers.Person(Web.HttpUtility.HtmlDecode(m1.Groups("name").ToString.Trim), _
                                Web.HttpUtility.HtmlDecode(m2.ToString.Trim), _
                                If(m3.Groups("thumb").ToString.IndexOf("addtiny") > 0 OrElse m3.Groups("thumb").ToString.IndexOf("no_photo") > 0, String.Empty, Web.HttpUtility.HtmlDecode(m3.Groups("thumb").ToString.Trim).Replace("._SX23_SY30_.jpg", String.Concat("._", ThumbsSize, "_.jpg")))) Take 999999

                    Dim Cast As List(Of MediaContainers.Person) = Cast1.ToList

                    'Clean up the actors list
                    For Each Ps As MediaContainers.Person In Cast
                        For Each sMatch As Match In Regex.Matches(Ps.Role, HREF_PATTERN)
                            Ps.Role = Ps.Role.Replace(sMatch.Value, sMatch.Groups("name").Value.ToString.Trim)
                            Ps.Role = CleanRole(Ps.Role)
                        Next
                        ' Dim a_patterRegex = Regex.Match(Ps.Role, HREF_PATTERN)
                        ' If a_patterRegex.Success Then Ps.Role = a_patterRegex.Groups("name").ToString.Trim
                    Next

                    'only update nMovie if scraped result is not empty/nothing!
                    If Cast.Count > 0 Then
                        nShow.Actors = Cast
                    End If

                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Countries
                If FilteredOptions.bShowCountry Then
                    Dim D, W As Integer
                    D = If(HTML.IndexOf("<h5>Country:</h5>") > 0, HTML.IndexOf("<h5>Country:</h5>"), HTML.IndexOf("<h5>Countries:</h5>"))
                    W = If(D > 0, HTML.IndexOf("</div>", D), 0)
                    'got any country ?
                    If D > 0 AndAlso Not W <= 0 Then
                        'get only the first country's name
                        Dim rCou As MatchCollection = Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN)
                        Dim Cou = From M In rCou Where Not DirectCast(M, Match).Groups("name").ToString.Contains("more") _
                                  Select Web.HttpUtility.HtmlDecode(DirectCast(M, Match).Groups("name").ToString)

                        If Cou.Count > 0 Then
                            'If CountryAbbreviation = False Then
                            '    For Each entry In Cou
                            '        entry = entry.Replace("USA", "United States of America")
                            '        entry = entry.Replace("UK", "United Kingdom")
                            '        nShow.Countries.Add(entry)
                            '    Next
                            'Else
                            nShow.Countries.AddRange(Cou.ToList)
                            'End If

                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Genres
                If FilteredOptions.bShowGenre Then
                    Dim D, W As Integer
                    D = HTML.IndexOf("<h5>Genre:</h5>")
                    'Check if doesnt find genres
                    If D > 0 Then
                        W = HTML.IndexOf("</div>", D)

                        If W > 0 Then
                            Dim rGenres As MatchCollection = Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN)
                            Dim Gen = From M In rGenres _
                                      Select N = Web.HttpUtility.HtmlDecode(DirectCast(M, Match).Groups("name").ToString) Where Not N.Contains("more") Take 999999
                            If Gen.Count > 0 Then
                                nShow.Genres.AddRange(Gen.ToList)
                            End If
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Plot
                If FilteredOptions.bShowPlot Then
                    Dim FullPlotS As String = Regex.Match(PlotHtml, "<p class=""plotSummary"">(.*?)</p>", RegexOptions.Singleline Or RegexOptions.IgnoreCase Or RegexOptions.Multiline).Groups(1).Value.ToString.Trim
                    Dim FullPlotO As String = Regex.Match(PlotHtml, "<li class=""odd"">\s*<p>(.*?)<br/>", RegexOptions.Singleline Or RegexOptions.IgnoreCase Or RegexOptions.Multiline).Groups(1).Value.ToString.Trim
                    Dim FullPlotE As String = Regex.Match(PlotHtml, "<li class=""even"">\s*<p>(.*?)<br/>", RegexOptions.Singleline Or RegexOptions.IgnoreCase Or RegexOptions.Multiline).Groups(1).Value.ToString.Trim
                    Dim FullPlot As String = If(Not String.IsNullOrEmpty(FullPlotS), FullPlotS, If(Not String.IsNullOrEmpty(FullPlotO), FullPlotO, FullPlotE))
                    FullPlot = Regex.Replace(FullPlot, "<a(.*?)>", "")
                    FullPlot = Regex.Replace(FullPlot, "</a>", "")

                    If Not String.IsNullOrEmpty(FullPlot) Then
                        nShow.Plot = FullPlot
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'Duration
                If FilteredOptions.bShowRuntime Then
                    scrapedresult = Web.HttpUtility.HtmlDecode(Regex.Match(HTML, "<h5>Runtime:</h5>[^0-9]*([^<]*)").Groups(1).Value.Trim)
                    'only update nMovie if scraped result is not empty/nothing!
                    If Not String.IsNullOrEmpty(scrapedresult) Then
                        'examples:
                        ' <h5>Runtime:</h5><div class="info-content">93 min </div> OR
                        ' <h5>Runtime:</h5><div class="info-content">"94 min  | USA:102 min (unrated version)</div>
                        ' <h5>Runtime:</h5><div class="info-content">Thailand: 89 min  | USA:93 min </div>
                        '  scrapedresult = Web.HttpUtility.HtmlDecode(Regex.Match(HTML, "<h5>Runtime:</h5>[^0-9]*([^<]*)").Groups(1).Value.Trim)
                        Dim Match As Match = Regex.Match(HTML, "Runtime:(\s*<((?<!>).)+)+(?<length>\d+|((?!</div|<h).)+)", RegexOptions.IgnoreCase)
                        If Match.Success Then
                            If Regex.IsMatch(Match.Groups("length").Value, "^\d+$") Then
                                scrapedresult = Match.Groups("length").Value
                            ElseIf Regex.IsMatch(Match.Groups("length").Value, "\d+") Then
                                scrapedresult = Regex.Match(Match.Groups("length").Value, "\d+").Value
                            End If
                            nShow.Runtime = scrapedresult
                        End If
                    End If
                End If

                'Studios
                If FilteredOptions.bShowStudio Then
                    Dim D, W As Integer
                    'If FullCrew Then
                    '    D = HTML.IndexOf("<b class=""blackcatheader"">Production Companies</b>")
                    '    If D > 0 Then W = HTML.IndexOf("</ul>", D)
                    '    If D > 0 AndAlso W > 0 Then
                    '        'only get the first one
                    '        Dim Ps = From P1 In Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN) _
                    '                 Where Not DirectCast(P1, Match).Groups("name").ToString = String.Empty _
                    '                 Select Studio = Web.HttpUtility.HtmlDecode(DirectCast(P1, Match).Groups("name").ToString) Take 1
                    '        '  nMovie.Studio = Ps(0).ToString.Trim
                    '        'only update nMovie if scraped result is not empty/nothing!
                    '        If Ps.Count > 0 Then
                    '            nMovie.Studios.AddRange(Ps.ToList)
                    '        End If
                    '    End If
                    'Else
                    D = HTML.IndexOf("<h5>Company:</h5>")
                    If D > 0 Then W = HTML.IndexOf("</div>", D)
                    'only update nMovie if scraped result is not empty/nothing!
                    If D > 0 AndAlso W > 0 Then
                        nShow.Studios.Add(Web.HttpUtility.HtmlDecode(Regex.Match(HTML.Substring(D, W - D), HREF_PATTERN).Groups("name").ToString.Trim))
                    End If
                    'End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                Return True
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try

            'Dim Show As TMDbLib.Objects.TvShows.TvShow
            'Dim ShowE As TMDbLib.Objects.TvShows.TvShow

            ''clear nShow from search results
            'nShow.Clear()

            'If bwTMDB.CancellationPending Then Return Nothing

            ''search movie by TMDB ID
            'Show = _TMDBApi.GetTvShow(CInt(strID), TMDbLib.Objects.TvShows.TvShowMethods.ContentRatings Or TMDbLib.Objects.TvShows.TvShowMethods.Credits Or TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds)
            'If _MySettings.FallBackEng Then
            '    ShowE = _TMDBApiE.GetTvShow(CInt(strID), TMDbLib.Objects.TvShows.TvShowMethods.ContentRatings Or TMDbLib.Objects.TvShows.TvShowMethods.Credits Or TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds)
            'Else
            '    ShowE = Show
            'End If

            'If (Show Is Nothing AndAlso Not _MySettings.FallBackEng) OrElse (Show Is Nothing AndAlso ShowE Is Nothing) OrElse _
            '    (Not Show.Id > 0 AndAlso Not _MySettings.FallBackEng) OrElse (Not Show.Id > 0 AndAlso Not ShowE.Id > 0) Then
            '    logger.Error(String.Concat("Can't scrape or movie not found: ", strID))
            '    Return False
            'End If

            'nShow.Scrapersource = "TMDB"

            ''IDs
            'nShow.TMDB = CStr(Show.Id)
            'If Show.ExternalIds.TvdbId IsNot Nothing Then nShow.ID = CStr(Show.ExternalIds.TvdbId)
            'If Show.ExternalIds.ImdbId IsNot Nothing Then nShow.IMDB = Show.ExternalIds.ImdbId

            'If bwTMDB.CancellationPending Or Show Is Nothing Then Return Nothing

            ''Cast (Actors)
            'If Options.bShowActors Then
            '    If Show.Credits IsNot Nothing AndAlso Show.Credits.Cast IsNot Nothing Then
            '        For Each aCast As TMDbLib.Objects.TvShows.Cast In Show.Credits.Cast
            '            nShow.Actors.Add(New MediaContainers.Person With {.Name = aCast.Name, _
            '                                                               .Role = aCast.Character, _
            '                                                               .ThumbURL = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_TMDBApi.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty), _
            '                                                               .TMDB = CStr(aCast.Id)})
            '        Next
            '    End If
            'End If

            'If bwTMDB.CancellationPending Then Return Nothing

            ''Certifications
            'If Options.bShowCert Then
            '    If Show.ContentRatings IsNot Nothing AndAlso Show.ContentRatings.Results IsNot Nothing AndAlso Show.ContentRatings.Results.Count > 0 Then
            '        For Each aCountry In Show.ContentRatings.Results
            '            If Not String.IsNullOrEmpty(aCountry.Rating) Then
            '                Try
            '                    Dim tCountry As String = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = aCountry.Iso_3166_1.ToLower).name
            '                    nShow.Certifications.Add(String.Concat(tCountry, ":", aCountry.Rating))
            '                Catch ex As Exception
            '                    logger.Warn("Unhandled certification language encountered: {0}", aCountry.Iso_3166_1.ToLower)
            '                End Try
            '            End If
            '        Next
            '    End If
            'End If

            'If bwTMDB.CancellationPending Then Return Nothing

            ''Countries
            'If Options.bShowCountry Then
            '    If Show.OriginCountry IsNot Nothing AndAlso Show.OriginCountry.Count > 0 Then
            '        For Each aCountry As String In Show.OriginCountry
            '            nShow.Countries.Add(aCountry)
            '        Next
            '    End If
            'End If

            'If bwTMDB.CancellationPending Then Return Nothing

            ''Created By
            'If Options.bShowCreator Then
            '    If Show.CreatedBy IsNot Nothing Then
            '        For Each aCreator As TMDbLib.Objects.People.Person In Show.CreatedBy
            '            nShow.Creators.Add(aCreator.Name)
            '        Next
            '    End If
            'End If

            'If bwTMDB.CancellationPending Then Return Nothing

            ''Genres
            'If Options.bShowGenre Then
            '    Dim aGenres As System.Collections.Generic.List(Of TMDbLib.Objects.General.Genre) = Nothing
            '    If Show.Genres Is Nothing OrElse (Show.Genres IsNot Nothing AndAlso Show.Genres.Count = 0) Then
            '        If _MySettings.FallBackEng AndAlso ShowE.Genres IsNot Nothing AndAlso ShowE.Genres.Count > 0 Then
            '            aGenres = ShowE.Genres
            '        End If
            '    Else
            '        aGenres = Show.Genres
            '    End If

            '    If aGenres IsNot Nothing Then
            '        For Each tGenre As TMDbLib.Objects.General.Genre In aGenres
            '            nShow.Genres.Add(tGenre.Name)
            '        Next
            '    End If
            'End If

            'If bwTMDB.CancellationPending Then Return Nothing

            ''OriginalTitle
            'If Options.bShowOriginalTitle Then
            '    If Show.OriginalName Is Nothing OrElse (Show.OriginalName IsNot Nothing AndAlso String.IsNullOrEmpty(Show.OriginalName)) Then
            '        If _MySettings.FallBackEng AndAlso ShowE.OriginalName IsNot Nothing AndAlso Not String.IsNullOrEmpty(ShowE.OriginalName) Then
            '            nShow.OriginalTitle = ShowE.OriginalName
            '        End If
            '    Else
            '        nShow.OriginalTitle = ShowE.OriginalName
            '    End If
            'End If

            'If bwTMDB.CancellationPending Then Return Nothing

            ''Plot
            'If Options.bShowPlot Then
            '    If Show.Overview Is Nothing OrElse (Show.Overview IsNot Nothing AndAlso String.IsNullOrEmpty(Show.Overview)) Then
            '        If _MySettings.FallBackEng AndAlso ShowE.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ShowE.Overview) Then
            '            nShow.Plot = ShowE.Overview
            '        End If
            '    Else
            '        nShow.Plot = Show.Overview
            '    End If
            'End If

            'If bwTMDB.CancellationPending Then Return Nothing

            ''Posters (only for SearchResult dialog, auto fallback to "en" by TMDB)
            'If GetPoster Then
            '    If Show.PosterPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(Show.PosterPath) Then
            '        _sPoster = _TMDBApi.Config.Images.BaseUrl & "w92" & Show.PosterPath
            '    Else
            '        _sPoster = String.Empty
            '    End If
            'End If

            'If bwTMDB.CancellationPending Then Return Nothing

            ''Premiered
            'If Options.bShowPremiered Then
            '    Dim ScrapedDate As String = String.Empty
            '    If Show.FirstAirDate Is Nothing OrElse (Show.FirstAirDate IsNot Nothing AndAlso String.IsNullOrEmpty(CStr(Show.FirstAirDate))) Then
            '        If _MySettings.FallBackEng AndAlso ShowE.FirstAirDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(ShowE.FirstAirDate)) Then
            '            ScrapedDate = CStr(ShowE.FirstAirDate)
            '        End If
            '    Else
            '        ScrapedDate = CStr(Show.FirstAirDate)
            '    End If
            '    If Not String.IsNullOrEmpty(ScrapedDate) Then
            '        Dim RelDate As Date
            '        If Date.TryParse(ScrapedDate, RelDate) Then
            '            'always save date in same date format not depending on users language setting!
            '            nShow.Premiered = RelDate.ToString("yyyy-MM-dd")
            '        Else
            '            nShow.Premiered = ScrapedDate
            '        End If
            '    End If
            'End If

            'If bwTMDB.CancellationPending Then Return Nothing

            ''Rating
            'If Options.bShowRating Then
            '    nShow.Rating = CStr(Show.VoteAverage)
            '    nShow.Votes = CStr(Show.VoteCount)
            'End If

            'If bwTMDB.CancellationPending Then Return Nothing

            ''Runtime
            'If Options.bShowRuntime Then
            '    If Show.EpisodeRunTime Is Nothing OrElse Show.EpisodeRunTime.Count = 0 Then
            '        If _MySettings.FallBackEng AndAlso ShowE.EpisodeRunTime IsNot Nothing Then
            '            nShow.Runtime = CStr(ShowE.EpisodeRunTime.Item(0))
            '        End If
            '    Else
            '        nShow.Runtime = CStr(Show.EpisodeRunTime.Item(0))
            '    End If
            'End If

            'If bwTMDB.CancellationPending Then Return Nothing

            ''Studios
            'If Options.bShowStudio Then
            '    If Show.Networks IsNot Nothing AndAlso Show.Networks.Count > 0 Then
            '        For Each aStudio In Show.Networks
            '            nShow.Studios.Add(aStudio.Name)
            '        Next
            '    End If
            'End If

            'If bwTMDB.CancellationPending Then Return Nothing

            ''Title
            'If Options.bShowTitle Then
            '    If Show.Name Is Nothing OrElse (Show.Name IsNot Nothing AndAlso String.IsNullOrEmpty(Show.Name)) Then
            '        If _MySettings.FallBackEng AndAlso ShowE.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(ShowE.Name) Then
            '            nShow.Title = ShowE.Name
            '        End If
            '    Else
            '        nShow.Title = Show.Name
            '    End If
            'End If

            'If bwTMDB.CancellationPending Then Return Nothing

            ' ''Trailer
            ''If Options.bTrailer Then
            ''    Dim aTrailers As List(Of TMDbLib.Objects.TvShows.Video) = Nothing
            ''    If Show.Videos Is Nothing OrElse (Show.Videos IsNot Nothing AndAlso Show.Videos.Results.Count = 0) Then
            ''        If _MySettings.FallBackEng AndAlso ShowE.Videos IsNot Nothing AndAlso ShowE.Videos.Results.Count > 0 Then
            ''            aTrailers = ShowE.Videos.Results
            ''        End If
            ''    Else
            ''        aTrailers = Show.Videos.Results
            ''    End If

            ''    If aTrailers IsNot Nothing AndAlso aTrailers IsNot Nothing AndAlso aTrailers.Count > 0 Then
            ''        nShow.Trailer = "http://www.youtube.com/watch?hd=1&v=" & aTrailers.Item(0).Key
            ''    End If
            ''End If

            'If bwTMDB.CancellationPending Then Return Nothing

            ''Episodes
            'If withEpisodes Then
            '    For Each aSeason As TMDbLib.Objects.TvShows.TvSeason In Show.Seasons
            '        GetTVSeasonInfo(nShow, Show.Id, aSeason.SeasonNumber, Options, withEpisodes)
            '    Next
            'End If

            Return True
        End Function

        Public Function GetMovieStudios(ByVal strID As String) As List(Of String)
            Dim alStudio As New List(Of String)
            If (String.IsNullOrEmpty(strID)) Then
                logger.Warn("Attempting to GetMovieStudios with invalid ID <{0}>", strID)
                Return alStudio
            End If
            Dim HTML As String
            intHTTP = New HTTP
            HTML = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", strID, "/combined"))
            intHTTP.Dispose()
            intHTTP = Nothing

            If (String.IsNullOrEmpty(HTML)) Then
                logger.Warn("IMDB Query returned no results for ID of <{0}>", strID)
                Return alStudio
            End If
            Dim D, W As Integer

            D = HTML.IndexOf("<b class=""blackcatheader"">Production Companies</b>")
            If D > 0 Then W = HTML.IndexOf("</ul>", D)
            If D > 0 AndAlso W > 0 Then
                Dim Ps = From P1 In Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN) _
                         Where Not DirectCast(P1, Match).Groups("name").ToString = String.Empty _
                         Select Studio = Web.HttpUtility.HtmlDecode(DirectCast(P1, Match).Groups("name").ToString)
                alStudio.AddRange(Ps.ToArray)
            End If

            Return alStudio
        End Function

        Public Function GetSearchMovieInfo(ByVal sMovieName As String, ByVal sMovieYear As String, ByRef oDBMovie As Database.DBElement, ByRef nMovie As MediaContainers.Movie, ByVal iType As Enums.ScrapeType, ByVal FilteredOptions As Structures.ScrapeOptions_Movie, ByVal FullCrew As Boolean, ByVal WorldWideTitleFallback As Boolean, ByVal ForceTitleLanguage As String, ByVal CountryAbbreviation As Boolean, ByVal StudiowithDistributors As Boolean) As MediaContainers.Movie
            Dim r As SearchResults_Movie = SearchMovie(sMovieName, sMovieYear)
            Dim b As Boolean = False

            Try
                Select Case iType
                    Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                        If r.ExactMatches.Count = 1 Then
                            b = GetMovieInfo(r.ExactMatches.Item(0).IMDBID, nMovie, FullCrew, False, FilteredOptions, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation, StudiowithDistributors)
                        ElseIf r.PopularTitles.Count = 1 AndAlso r.PopularTitles(0).Lev <= 5 Then
                            b = GetMovieInfo(r.PopularTitles.Item(0).IMDBID, nMovie, FullCrew, False, FilteredOptions, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation, StudiowithDistributors)
                        ElseIf r.ExactMatches.Count = 1 AndAlso r.ExactMatches(0).Lev <= 5 Then
                            b = GetMovieInfo(r.ExactMatches.Item(0).IMDBID, nMovie, FullCrew, False, FilteredOptions, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation, StudiowithDistributors)
                        Else
                            nMovie.Clear()
                            Using dIMDB As New dlgIMDBSearchResults_Movie(_SpecialSettings, Me)
                                If dIMDB.ShowDialog(nMovie, r, sMovieName, oDBMovie.Filename) = Windows.Forms.DialogResult.OK Then
                                    If String.IsNullOrEmpty(nMovie.IMDBID) Then
                                        b = False
                                    Else
                                        b = GetMovieInfo(nMovie.IMDBID, nMovie, FullCrew, False, FilteredOptions, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation, StudiowithDistributors)
                                    End If
                                Else
                                    b = False
                                End If
                            End Using
                        End If

                    Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                        If r.ExactMatches.Count = 1 Then
                            b = GetMovieInfo(r.ExactMatches.Item(0).IMDBID, nMovie, FullCrew, False, FilteredOptions, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation, StudiowithDistributors)
                        End If

                    Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                        'check if ALL results are over lev value
                        Dim useAnyway As Boolean = False
                        If ((r.PopularTitles.Count > 0 AndAlso r.PopularTitles(0).Lev > 5) OrElse r.PopularTitles.Count = 0) AndAlso _
                        ((r.ExactMatches.Count > 0 AndAlso r.ExactMatches(0).Lev > 5) OrElse r.ExactMatches.Count = 0) AndAlso _
                        ((r.PartialMatches.Count > 0 AndAlso r.PartialMatches(0).Lev > 5) OrElse r.PartialMatches.Count = 0) Then
                            useAnyway = True
                        End If
                        Dim exactHaveYear As Integer = FindYear(oDBMovie.Filename, r.ExactMatches)
                        Dim popularHaveYear As Integer = FindYear(oDBMovie.Filename, r.PopularTitles)
                        'it seems "popular matches" is a better result than "exact matches" ..... nope
                        'If r.ExactMatches.Count = 1 AndAlso r.PopularTitles.Count = 0 AndAlso r.PartialMatches.Count = 0 Then 'redirected to imdb info page
                        '    b = GetMovieInfo(r.ExactMatches.Item(0).IMDBID, imdbMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                        'ElseIf (popularHaveYear >= 0 OrElse exactHaveYear = -1) AndAlso r.PopularTitles.Count > 0 AndAlso (r.PopularTitles(If(popularHaveYear >= 0, popularHaveYear, 0)).Lev <= 5 OrElse useAnyway) Then
                        '    b = GetMovieInfo(r.PopularTitles.Item(If(popularHaveYear >= 0, popularHaveYear, 0)).IMDBID, imdbMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                        'ElseIf r.ExactMatches.Count > 0 AndAlso (r.ExactMatches(If(exactHaveYear >= 0, exactHaveYear, 0)).Lev <= 5 OrElse useAnyway) Then
                        '    b = GetMovieInfo(r.ExactMatches.Item(If(exactHaveYear >= 0, exactHaveYear, 0)).IMDBID, imdbMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                        'ElseIf r.PartialMatches.Count > 0 Then
                        '    b = GetMovieInfo(r.PartialMatches.Item(0).IMDBID, imdbMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                        'End If
                        If r.ExactMatches.Count = 1 Then
                            b = GetMovieInfo(r.ExactMatches.Item(0).IMDBID, nMovie, FullCrew, False, FilteredOptions, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation, StudiowithDistributors)
                        ElseIf r.ExactMatches.Count > 1 AndAlso exactHaveYear >= 0 Then
                            b = GetMovieInfo(r.ExactMatches.Item(exactHaveYear).IMDBID, nMovie, FullCrew, False, FilteredOptions, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation, StudiowithDistributors)
                        ElseIf r.PopularTitles.Count > 0 AndAlso popularHaveYear >= 0 Then
                            b = GetMovieInfo(r.PopularTitles.Item(popularHaveYear).IMDBID, nMovie, FullCrew, False, FilteredOptions, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation, StudiowithDistributors)
                        ElseIf r.ExactMatches.Count > 0 AndAlso (r.ExactMatches(0).Lev <= 5 OrElse useAnyway) Then
                            b = GetMovieInfo(r.ExactMatches.Item(0).IMDBID, nMovie, FullCrew, False, FilteredOptions, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation, StudiowithDistributors)
                        ElseIf r.PopularTitles.Count > 0 AndAlso (r.PopularTitles(0).Lev <= 5 OrElse useAnyway) Then
                            b = GetMovieInfo(r.PopularTitles.Item(0).IMDBID, nMovie, FullCrew, False, FilteredOptions, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation, StudiowithDistributors)
                        ElseIf r.PartialMatches.Count > 0 AndAlso (r.PartialMatches(0).Lev <= 5 OrElse useAnyway) Then
                            b = GetMovieInfo(r.PartialMatches.Item(0).IMDBID, nMovie, FullCrew, False, FilteredOptions, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation, StudiowithDistributors)
                        End If
                End Select

                If b Then
                    Return nMovie
                Else
                    Return New MediaContainers.Movie
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return New MediaContainers.Movie
            End Try
        End Function

        Public Function GetSearchTVShowInfo(ByVal sShowName As String, ByRef oDBTV As Database.DBElement, ByRef nShow As MediaContainers.TVShow, ByVal iType As Enums.ScrapeType, ByVal FilteredOptions As Structures.ScrapeOptions_TV) As MediaContainers.TVShow
            Dim r As SearchResults_TVShow = SearchTVShow(sShowName)
            Dim b As Boolean = False

            Select Case iType
                Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                    If r.Matches.Count = 1 Then
                        b = GetTVShowInfo(r.Matches.Item(0).IMDB, nShow, False, FilteredOptions, True, False)
                    Else
                        nShow.Clear()
                        Using dIMDB As New dlgIMDBSearchResults_TV(_SpecialSettings, Me)
                            If dIMDB.ShowDialog(nShow, r, sShowName, oDBTV.ShowPath) = Windows.Forms.DialogResult.OK Then
                                If String.IsNullOrEmpty(nShow.IMDB) Then
                                    b = False
                                Else
                                    b = GetTVShowInfo(nShow.IMDB, nShow, False, FilteredOptions, True, False)
                                End If
                            Else
                                b = False
                            End If
                        End Using
                    End If

                Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                    If r.Matches.Count = 1 Then
                        b = GetTVShowInfo(r.Matches.Item(0).IMDB, nShow, False, FilteredOptions, True, False)
                    End If

                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                    If r.Matches.Count > 0 Then
                        b = GetTVShowInfo(r.Matches.Item(0).IMDB, nShow, False, FilteredOptions, True, False)
                    End If

                    ''check if ALL results are over lev value
                    'Dim useAnyway As Boolean = False
                    'If ((r.PopularTitles.Count > 0 AndAlso r.PopularTitles(0).Lev > 5) OrElse r.PopularTitles.Count = 0) AndAlso _
                    '((r.ExactMatches.Count > 0 AndAlso r.ExactMatches(0).Lev > 5) OrElse r.ExactMatches.Count = 0) AndAlso _
                    '((r.PartialMatches.Count > 0 AndAlso r.PartialMatches(0).Lev > 5) OrElse r.PartialMatches.Count = 0) Then
                    '    useAnyway = True
                    'End If
                    'Dim exactHaveYear As Integer = FindYear(oDBMovie.Filename, r.ExactMatches)
                    'Dim popularHaveYear As Integer = FindYear(oDBMovie.Filename, r.PopularTitles)
                    ''it seems "popular matches" is a better result than "exact matches" ..... nope
                    ''If r.ExactMatches.Count = 1 AndAlso r.PopularTitles.Count = 0 AndAlso r.PartialMatches.Count = 0 Then 'redirected to imdb info page
                    ''    b = GetMovieInfo(r.ExactMatches.Item(0).IMDBID, imdbMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                    ''ElseIf (popularHaveYear >= 0 OrElse exactHaveYear = -1) AndAlso r.PopularTitles.Count > 0 AndAlso (r.PopularTitles(If(popularHaveYear >= 0, popularHaveYear, 0)).Lev <= 5 OrElse useAnyway) Then
                    ''    b = GetMovieInfo(r.PopularTitles.Item(If(popularHaveYear >= 0, popularHaveYear, 0)).IMDBID, imdbMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                    ''ElseIf r.ExactMatches.Count > 0 AndAlso (r.ExactMatches(If(exactHaveYear >= 0, exactHaveYear, 0)).Lev <= 5 OrElse useAnyway) Then
                    ''    b = GetMovieInfo(r.ExactMatches.Item(If(exactHaveYear >= 0, exactHaveYear, 0)).IMDBID, imdbMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                    ''ElseIf r.PartialMatches.Count > 0 Then
                    ''    b = GetMovieInfo(r.PartialMatches.Item(0).IMDBID, imdbMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
                    ''End If
                    'If r.ExactMatches.Count = 1 Then
                    '    b = GetMovieInfo(r.ExactMatches.Item(0).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation)
                    'ElseIf r.ExactMatches.Count > 1 AndAlso exactHaveYear >= 0 Then
                    '    b = GetMovieInfo(r.ExactMatches.Item(exactHaveYear).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation)
                    'ElseIf r.PopularTitles.Count > 0 AndAlso popularHaveYear >= 0 Then
                    '    b = GetMovieInfo(r.PopularTitles.Item(popularHaveYear).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation)
                    'ElseIf r.ExactMatches.Count > 0 AndAlso (r.ExactMatches(0).Lev <= 5 OrElse useAnyway) Then
                    '    b = GetMovieInfo(r.ExactMatches.Item(0).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation)
                    'ElseIf r.PopularTitles.Count > 0 AndAlso (r.PopularTitles(0).Lev <= 5 OrElse useAnyway) Then
                    '    b = GetMovieInfo(r.PopularTitles.Item(0).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation)
                    'ElseIf r.PartialMatches.Count > 0 AndAlso (r.PartialMatches(0).Lev <= 5 OrElse useAnyway) Then
                    '    b = GetMovieInfo(r.PartialMatches.Item(0).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage, CountryAbbreviation)
                    'End If
            End Select

            If b Then
                Return nShow
            Else
                Return New MediaContainers.TVShow
            End If
        End Function

        Private Function FindYear(ByVal tmpname As String, ByVal lst As List(Of MediaContainers.Movie)) As Integer
            Dim tmpyear As String = ""
            Dim i As Integer
            Dim ret As Integer = -1
            tmpname = Path.GetFileNameWithoutExtension(tmpname)
            tmpname = tmpname.Replace(".", " ").Trim.Replace("(", " ").Replace(")", "").Trim
            i = tmpname.LastIndexOf(" ")
            If i >= 0 Then
                tmpyear = tmpname.Substring(i + 1, tmpname.Length - i - 1)
                If Integer.TryParse(tmpyear, 0) AndAlso Convert.ToInt32(tmpyear) > 1950 Then 'let's assume there are no movies older then 1950
                    For c = 0 To lst.Count - 1
                        If lst(c).Year = tmpyear Then
                            ret = c
                            Exit For
                        End If
                    Next
                End If
            End If
            Return ret
        End Function

        Public Sub GetSearchMovieInfoAsync(ByVal imdbID As String, ByVal nMovie As MediaContainers.Movie, ByVal FilteredOptions As Structures.ScrapeOptions_Movie)
            Try
                If Not bwIMDB.IsBusy Then
                    bwIMDB.WorkerReportsProgress = False
                    bwIMDB.WorkerSupportsCancellation = True
                    bwIMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_Movie, _
                                           .Parameter = imdbID, .Movie = nMovie, .Options_Movie = FilteredOptions})
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub GetSearchTVShowInfoAsync(ByVal imdbID As String, ByVal nShow As MediaContainers.TVShow, ByVal Options As Structures.ScrapeOptions_TV)
            Try
                If Not bwIMDB.IsBusy Then
                    bwIMDB.WorkerReportsProgress = False
                    bwIMDB.WorkerSupportsCancellation = True
                    bwIMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_TVShow, _
                                           .Parameter = imdbID, .TVShow = nShow, .Options_TV = Options})
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub SearchMovieAsync(ByVal sMovieTitle As String, ByVal sMovieYear As String, ByVal filterOptions As Structures.ScrapeOptions_Movie)
            Try
                If Not bwIMDB.IsBusy Then
                    bwIMDB.WorkerReportsProgress = False
                    bwIMDB.WorkerSupportsCancellation = True
                    bwIMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.Movies, .Parameter = sMovieTitle, .Year = sMovieYear, .Options_Movie = filterOptions})
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub SearchTVShowAsync(ByVal sShow As String, ByVal filterOptions As Structures.ScrapeOptions_TV)

            If Not bwIMDB.IsBusy Then
                bwIMDB.WorkerReportsProgress = False
                bwIMDB.WorkerSupportsCancellation = True
                bwIMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.TVShows, _
                  .Parameter = sShow, .Options_TV = filterOptions, .withEpisodes = False})
            End If
        End Sub

        Private Sub bwIMDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwIMDB.DoWork
            Dim Args As Arguments = DirectCast(e.Argument, Arguments)

            Select Case Args.Search
                Case SearchType.Movies
                    Dim r As SearchResults_Movie = SearchMovie(Args.Parameter, Args.Year)
                    e.Result = New Results With {.ResultType = SearchType.Movies, .Result = r}

                Case SearchType.SearchDetails_Movie
                    Dim s As Boolean = GetMovieInfo(Args.Parameter, Args.Movie, False, True, Args.Options_Movie, True, True, "", False, False)
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails_Movie, .Success = s}

                Case SearchType.TVShows
                    Dim r As SearchResults_TVShow = SearchTVShow(Args.Parameter)
                    e.Result = New Results With {.ResultType = SearchType.TVShows, .Result = r}

                Case SearchType.SearchDetails_TVShow
                    Dim s As Boolean = GetTVShowInfo(Args.Parameter, Args.TVShow, True, Args.Options_TV, True, Args.withEpisodes)
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails_TVShow, .Success = s}
            End Select
        End Sub

        Private Sub bwIMDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwIMDB.RunWorkerCompleted
            Dim Res As Results = DirectCast(e.Result, Results)

            Select Case Res.ResultType
                Case SearchType.Movies
                    RaiseEvent SearchResultsDownloaded_Movie(DirectCast(Res.Result, SearchResults_Movie))

                Case SearchType.SearchDetails_Movie
                    Dim movieInfo As SearchResults_Movie = DirectCast(Res.Result, SearchResults_Movie)
                    RaiseEvent SearchInfoDownloaded_Movie(sPoster, Res.Success)

                Case SearchType.TVShows
                    RaiseEvent SearchResultsDownloaded_TV(DirectCast(Res.Result, SearchResults_TVShow))

                Case SearchType.SearchDetails_TVShow
                    Dim movieInfo As SearchResults_TVShow = DirectCast(Res.Result, SearchResults_TVShow)
                    RaiseEvent SearchInfoDownloaded_TV(sPoster, Res.Success)
            End Select
        End Sub

        Private Function CleanTitle(ByVal sString As String) As String
            Dim CleanString As String = sString

            If sString.StartsWith("""") Then CleanString = sString.Remove(0, 1)

            If sString.EndsWith("""") Then CleanString = CleanString.Remove(CleanString.Length - 1, 1)

            Return CleanString
        End Function

        Private Function CleanRole(ByVal strRole As String) As String
            Dim CleanString As String = strRole

            CleanString = Regex.Replace(CleanString, "\/ \.\.\..*", String.Empty).Trim
            CleanString = Regex.Replace(CleanString, "\(\d.*", String.Empty).Trim

            If Not String.IsNullOrEmpty(CleanString) Then
                Return CleanString
            Else
                Return strRole
            End If
        End Function

        Private Function GetForcedTitle(ByVal strID As String, ByVal oTitle As String, ByVal WorldWideTitleFallback As Boolean, ByVal ForceTitleLanguage As String) As String
            Dim fTitle As String = oTitle

            If bwIMDB.CancellationPending Then Return Nothing
            Dim HTML As String
            intHTTP = New HTTP
            HTML = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", strID, "/releaseinfo#akas"))
            intHTTP.Dispose()
            intHTTP = Nothing


            Dim D, W As Integer

            D = HTML.IndexOf("<h4 class=""li_group"">Also Known As (AKA)&nbsp;</h4>")

            If D > 0 Then
                W = HTML.IndexOf("</table>", D)
                Dim rTitles As MatchCollection = Regex.Matches(HTML.Substring(D, W - D), TD_PATTERN_4, RegexOptions.Multiline Or RegexOptions.IgnorePatternWhitespace)

                If rTitles.Count > 0 Then
                    For i As Integer = 0 To rTitles.Count - 1 Step 2
                        If rTitles(i).Value.ToString.Contains(ForceTitleLanguage) AndAlso Not rTitles(i).Value.ToString.Contains(String.Concat(ForceTitleLanguage, " (working title)")) AndAlso Not rTitles(i).Value.ToString.Contains(String.Concat(ForceTitleLanguage, " (fake working title)")) Then
                            fTitle = CleanTitle(Web.HttpUtility.HtmlDecode(rTitles(i + 1).Groups("title").Value.ToString.Trim))
                            Exit For
                            'if Setting WorldWide Title Fallback is enabled then instead of returning originaltitle (when force title language isn't found), use english/worldwide title instead (i.e. avoid asian original titles)
                        ElseIf WorldWideTitleFallback AndAlso (rTitles(i).Value.ToString.ToUpper.Contains("WORLD-WIDE") OrElse rTitles(i).Value.ToString.ToUpper.Contains("ENGLISH")) Then
                            fTitle = CleanTitle(Web.HttpUtility.HtmlDecode(rTitles(i + 1).Groups("title").Value.ToString.Trim))
                        End If
                    Next
                End If
            End If

            Return fTitle
        End Function

        Private Function GetMovieID(ByVal strObj As String) As String
            Return Regex.Match(strObj, IMDB_ID_REGEX).ToString.Replace("tt", String.Empty)
        End Function

        Private Function SearchMovie(ByVal sMovieTitle As String, ByVal sMovieYear As String) As SearchResults_Movie

            Dim sMovie As String = String.Concat(sMovieTitle, " ", If(Not String.IsNullOrEmpty(sMovieYear), String.Concat("(", sMovieYear, ")"), String.Empty))

            Dim D, W As Integer
            Dim R As New SearchResults_Movie

            Dim HTML As String = String.Empty
            Dim HTMLt As String = String.Empty
            Dim HTMLp As String = String.Empty
            Dim HTMLm As String = String.Empty
            Dim HTMLe As String = String.Empty
            Dim HTMLv As String = String.Empty
            Dim HTMLs As String = String.Empty
            Dim rUri As String = String.Empty

            intHTTP = New HTTP
            HTML = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/find?q=", Web.HttpUtility.UrlEncode(sMovie), "&s=tt&ttype=ft"))
            HTMLe = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/find?q=", Web.HttpUtility.UrlEncode(sMovie), "&s=tt&ttype=ft&exact=true&ref_=fn_tt_ex"))
            rUri = intHTTP.ResponseUri

            If _SpecialSettings.SearchTvTitles Then
                HTMLt = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/search/title?title=", Web.HttpUtility.UrlEncode(sMovie), "&title_type=tv_movie"))
            End If
            If _SpecialSettings.SearchVideoTitles Then
                HTMLv = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/search/title?title=", Web.HttpUtility.UrlEncode(sMovie), "&title_type=video"))
            End If
            If _SpecialSettings.SearchShortTitles Then
                HTMLs = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/search/title?title=", Web.HttpUtility.UrlEncode(sMovie), "&title_type=short"))
            End If
            If _SpecialSettings.SearchPartialTitles Then
                HTMLm = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/find?q=", Web.HttpUtility.UrlEncode(sMovie), "&s=tt&ttype=ft&ref_=fn_ft"))
            End If
            If _SpecialSettings.SearchPopularTitles Then
                HTMLp = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/find?q=", Web.HttpUtility.UrlEncode(sMovie), "&s=tt&ttype=ft&ref_=fn_tt_pop"))
            End If
            intHTTP.Dispose()
            intHTTP = Nothing

            'Check if we've been redirected straight to the movie page
            If Regex.IsMatch(rUri, IMDB_ID_REGEX) Then
                Dim lNewMovie As MediaContainers.Movie = New MediaContainers.Movie(Regex.Match(rUri, IMDB_ID_REGEX).ToString, _
                    StringUtils.ProperCase(sMovie), Regex.Match(Regex.Match(HTML, MOVIE_TITLE_PATTERN).ToString, "(?<=\()\d+(?=.*\))").ToString, 0)
                R.ExactMatches.Add(lNewMovie)
                Return R
            End If

            'popular titles
            D = HTMLp.IndexOf("</a>Titles</h3>")
            If Not D <= 0 Then
                W = HTMLp.IndexOf("</table>", D) + 8

                Dim Table As String = Regex.Match(HTML.Substring(D, W - D), TABLE_PATTERN).ToString
                Dim qPopular = From Mtr In Regex.Matches(Table, TITLE_PATTERN) _
                               Where Not DirectCast(Mtr, Match).Groups("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups("type").ToString.Contains("VG") _
                               Select New MediaContainers.Movie(GetMovieID(DirectCast(Mtr, Match).Groups("url").ToString), _
                                                Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString), Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(sMovie).ToLower, StringUtils.FilterYear(Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString)).ToLower))

                R.PopularTitles = qPopular.ToList
            End If

            'partial titles
            D = HTMLm.IndexOf("</a>Titles</h3>")
            If Not D <= 0 Then
                W = HTMLm.IndexOf("</table>", D) + 8

                Dim Table As String = Regex.Match(HTMLm.Substring(D, W - D), TABLE_PATTERN).ToString
                Dim qpartial = From Mtr In Regex.Matches(Table, TITLE_PATTERN) _
                    Where Not DirectCast(Mtr, Match).Groups("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups("type").ToString.Contains("VG") _
                    Select New MediaContainers.Movie(GetMovieID(DirectCast(Mtr, Match).Groups("url").ToString), _
                                     Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString), Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(sMovie).ToLower, StringUtils.FilterYear(Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString)).ToLower))

                R.PartialMatches = qpartial.ToList
            End If

            'tv titles
            D = HTMLt.IndexOf("<table class=""results"">")
            If Not D <= 0 Then
                W = HTMLt.IndexOf("</table>", D) + 8

                Dim Table As String = HTMLt.Substring(D, W - D).ToString
                Dim qtvmovie = From Mtr In Regex.Matches(Table, TvTITLE_PATTERN) _
                    Where Not DirectCast(Mtr, Match).Groups("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups("type").ToString.Contains("VG") _
                    Select New MediaContainers.Movie(GetMovieID(DirectCast(Mtr, Match).Groups("url").ToString), _
                                     Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString), Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(sMovie).ToLower, StringUtils.FilterYear(Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString)).ToLower))

                R.TvTitles = qtvmovie.ToList
            End If

            'video titles
            D = HTMLv.IndexOf("<table class=""results"">")
            If Not D <= 0 Then
                W = HTMLv.IndexOf("</table>", D) + 8

                Dim Table As String = HTMLv.Substring(D, W - D).ToString
                Dim qvideo = From Mtr In Regex.Matches(Table, TvTITLE_PATTERN) _
                    Where Not DirectCast(Mtr, Match).Groups("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups("type").ToString.Contains("VG") _
                    Select New MediaContainers.Movie(GetMovieID(DirectCast(Mtr, Match).Groups("url").ToString), _
                                     Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString), Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(sMovie).ToLower, StringUtils.FilterYear(Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString)).ToLower))

                R.VideoTitles = qvideo.ToList
            End If

            'short titles
            D = HTMLs.IndexOf("<table class=""results"">")
            If Not D <= 0 Then
                W = HTMLs.IndexOf("</table>", D) + 8

                Dim Table As String = HTMLs.Substring(D, W - D).ToString
                Dim qshort = From Mtr In Regex.Matches(Table, TvTITLE_PATTERN) _
                    Where Not DirectCast(Mtr, Match).Groups("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups("type").ToString.Contains("VG") _
                    Select New MediaContainers.Movie(GetMovieID(DirectCast(Mtr, Match).Groups("url").ToString), _
                                     Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString), Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(sMovie).ToLower, StringUtils.FilterYear(Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString)).ToLower))

                R.ShortTitles = qshort.ToList
            End If

            'exact titles
            D = HTMLe.IndexOf("</a>Titles</h3>")
            If Not D <= 0 Then
                W = HTMLe.IndexOf("</table>", D) + 8

                Dim Table As String = Regex.Match(HTMLe.Substring(D, W - D), TABLE_PATTERN).ToString
                Dim qExact = From Mtr In Regex.Matches(Table, TITLE_PATTERN) _
                               Where Not DirectCast(Mtr, Match).Groups("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups("type").ToString.Contains("VG") _
                               Select New MediaContainers.Movie(GetMovieID(DirectCast(Mtr, Match).Groups("url").ToString), _
                            Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString.ToString), Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(sMovie).ToLower, StringUtils.FilterYear(Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString)).ToLower))

                R.ExactMatches = qExact.ToList
            End If

            Return R
        End Function

        Private Function SearchTVShow(ByVal sShowTitle As String) As SearchResults_TVShow
            Dim sShow As String = sShowTitle

            Dim R As New SearchResults_TVShow

            Dim HTML As String = String.Empty
            Dim rUri As String = String.Empty

            intHTTP = New HTTP
            HTML = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/search/title?title=", Web.HttpUtility.UrlEncode(sShow), "&title_type=tv_series"))
            rUri = intHTTP.ResponseUri

            intHTTP.Dispose()
            intHTTP = Nothing

            'search results
            Dim Table As String = Regex.Match(HTML, TABLE_PATTERN_TV, RegexOptions.Singleline).ToString
            For Each sResult As Match In Regex.Matches(Table, TVSHOWTITLE_PATTERN, RegexOptions.Singleline)
                R.Matches.Add(New MediaContainers.TVShow With {.IMDB = sResult.Groups("IMDB").ToString, .Title = sResult.Groups("TITLE").ToString})
            Next

            Return R
        End Function

        Public Function GetTrailers(imdbID As String) As List(Of String)
            Dim TrailerList As New List(Of String)
            Dim TrailerNumber As Integer = 0
            Dim Links As MatchCollection
            Dim trailerPage As String
            Dim trailerUrl As String
            Dim Link As Match
            Dim currPage As Integer = 0

            intHTTP = New HTTP
            Dim _ImdbTrailerPage As String = String.Empty

            _ImdbTrailerPage = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", imdbID, "/videogallery/content_type-Trailer"))
            If _ImdbTrailerPage.ToLower.Contains("page not found") Then
                _ImdbTrailerPage = String.Empty
            End If

            If Not String.IsNullOrEmpty(_ImdbTrailerPage) Then
                Link = Regex.Match(_ImdbTrailerPage, "of [0-9]{1,3}")

                If Link.Success Then
                    TrailerNumber = Convert.ToInt32(Link.Value.Substring(3))

                    If TrailerNumber > 0 Then
                        currPage = Convert.ToInt32(Math.Ceiling(TrailerNumber / 10))

                        For i As Integer = 1 To currPage
                            If Not i = 1 Then
                                _ImdbTrailerPage = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", imdbID, "/videogallery/content_type-Trailer?page=", i))
                            End If

                            Links = Regex.Matches(_ImdbTrailerPage, "screenplay/(vi[0-9]+)/")
                            Dim linksCollection As String() = From m As Object In Links Select CType(m, Match).Value Distinct.ToArray()

                            Links = Regex.Matches(_ImdbTrailerPage, "imdb/(vi[0-9]+)/")
                            linksCollection = linksCollection.Concat(From m As Object In Links Select CType(m, Match).Value Distinct.ToArray()).ToArray

                            For Each value As String In linksCollection
                                If value.Contains("screenplay") Then
                                    trailerPage = intHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/video/", value, "player"))
                                    trailerUrl = Web.HttpUtility.UrlDecode(Regex.Match(trailerPage, "http.+mp4").Value)
                                    If Not String.IsNullOrEmpty(trailerUrl) AndAlso intHTTP.IsValidURL(trailerUrl) Then
                                        TrailerList.Add(trailerUrl)
                                    End If
                                Else
                                    ''480p Trailer
                                    'trailerPage = WebPage.DownloadData(String.Concat("http://", IMDBURL, "/video/", value, "player?uff=2"))
                                    'trailerUrl = Web.HttpUtility.UrlDecode(Regex.Match(trailerPage, "http.+mp4").Value)
                                    'If Not String.IsNullOrEmpty(trailerUrl) AndAlso WebPage.IsValidURL(trailerUrl) Then
                                    '    Me._TrailerList.Add(trailerUrl)
                                    'End If

                                    ''720p Trailer
                                    'trailerPage = WebPage.DownloadData(String.Concat("http://", IMDBURL, "/video/", value, "player?uff=3"))
                                    'trailerUrl = Web.HttpUtility.UrlDecode(Regex.Match(trailerPage, "http.+mp4").Value)
                                    'If Not String.IsNullOrEmpty(trailerUrl) AndAlso WebPage.IsValidURL(trailerUrl) Then
                                    '    Me._TrailerList.Add(trailerUrl)
                                    'End If
                                End If
                            Next
                        Next
                    End If
                End If
            End If
            intHTTP.Dispose()
            intHTTP = Nothing

            Return TrailerList
        End Function

#End Region 'Methods

#Region "Nested Types"

        Private Structure Arguments

#Region "Fields"

            Dim FullCast As Boolean
            Dim FullCrew As Boolean
            Dim Movie As MediaContainers.Movie
            Dim Options_Movie As Structures.ScrapeOptions_Movie
            Dim Options_TV As Structures.ScrapeOptions_TV
            Dim Parameter As String
            Dim Search As SearchType
            Dim TVShow As MediaContainers.TVShow
            Dim withEpisodes As Boolean
            Dim Year As String

#End Region 'Fields

        End Structure

        Private Structure Results

#Region "Fields"

            Dim Result As Object
            Dim ResultType As SearchType
            Dim Success As Boolean

#End Region 'Fields

        End Structure

#End Region 'Nested Types

    End Class

End Namespace

