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

    Public Class MovieSearchResults

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
        Private _ExactMatches As New List(Of MediaContainers.Movie)
        Private _PartialMatches As New List(Of MediaContainers.Movie)
        Private _PopularTitles As New List(Of MediaContainers.Movie)
        Private _TvTitles As New List(Of MediaContainers.Movie)
        Private _VideoTitles As New List(Of MediaContainers.Movie)

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
        Private Const TD_PATTERN_1 As String = "<td\sclass=""nm"">(.*?)</td>"
        Private Const TD_PATTERN_2 As String = "(?<=<td\sclass=""char"">)(.*?)(?=</td>)(\s\(.*?\))?"
        Private Const TD_PATTERN_3 As String = "<td\sclass=""hs"">(.*?)</td>"
        Private Const TD_PATTERN_4 As String = "<td>(?<title>.*?)</td>"
        Private Const TITLE_PATTERN As String = "<a\shref=[""""'](?<url>.*?)[""""'].*?>(?<name>.*?)</a>((\s)+?(\((?<year>\d{4})(\/.*?)?\)))?((\s)+?(\((?<type>.*?)\)))?"
        Private Const TR_PATTERN As String = "<tr\sclass="".*?"">(.*?)</tr>"
        Private Const TvTITLE_PATTERN As String = "<a\shref=[""'](?<url>.*?)[""']\stitle=[""'](?<name>.*?)((\s)+?(\((?<year>\d{4})))"

        Private sPoster As String

#End Region 'Fields

#Region "Enumerations"

        Private Enum SearchType
            Movies = 0
            Details = 1
            SearchDetails = 2
        End Enum

#End Region 'Enumerations

#Region "Events"

        Public Event Exception(ByVal ex As Exception)

        Public Event SearchMovieInfoDownloaded(ByVal sPoster As String, ByVal bSuccess As Boolean)

        Public Event SearchResultsDownloaded(ByVal mResults As IMDB.MovieSearchResults)

#End Region 'Events

#Region "Methods"

        Public Sub CancelAsync()
            If bwIMDB.IsBusy Then bwIMDB.CancelAsync()

            While bwIMDB.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End Sub

        ''' <summary>
        '''  Scrape MovieDetails from TMDB
        ''' </summary>
        ''' <param name="strID">IMDBID/TMBID of movie to be scraped</param>
        ''' <param name="nMovie">Container of scraped movie data</param>
        ''' <param name="FullCrew">Module setting: Scrape full cast?</param>
        ''' <param name="GetPoster">Scrape posters for the movie?</param>
        ''' <param name="Options">Module settings<param>
        ''' <param name="IsSearch">Not used at moment</param>
        ''' <param name="WorldWideTitleFallback">Module setting: Use worldwide title for Title tag if preferred language title isnt found</param>
        ''' <param name="ForceTitleLanguage">Module setting: preferred language title</param>
        ''' <returns>True: success, false: no success</returns>
        ''' <remarks>Cocotus/Dan 2014/08/30 - Reworked structure: Scraper module should NOT use global scraper settings/locks in Ember, just scraper options of module
        ''' Instead of directly saving scraped results into DBMovie we use empty nMovie movie container to store retrieved information of scraper</remarks>
        Public Function GetMovieInfo(ByVal strID As String, ByRef nMovie As MediaContainers.Movie, ByVal FullCrew As Boolean, ByVal GetPoster As Boolean, ByVal Options As Structures.ScrapeOptions_Movie, ByVal IsSearch As Boolean, ByVal WorldWideTitleFallback As Boolean, ByVal ForceTitleLanguage As String) As Boolean
            Try
                If bwIMDB.CancellationPending Then Return Nothing

                Dim HTML As String
                Using sHTTP As New HTTP
                    HTML = sHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", strID, "/combined"))
                End Using

                If bwIMDB.CancellationPending Then Return Nothing

                Dim PlotHtml As String
                Using sPlot As New HTTP
                    PlotHtml = sPlot.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", strID, "/plotsummary"))
                End Using

                nMovie.IMDBID = strID
                nMovie.Scrapersource = "IMDB"

                If bwIMDB.CancellationPending Then Return Nothing

                Dim scrapedresult As String = ""

                Dim OriginalTitle As String = Regex.Match(HTML, MOVIE_TITLE_PATTERN).ToString


                If Options.bTitle Then
                    'MOVIE ORIGINALTITLE
                    Dim oldOTitle As String = nMovie.OriginalTitle
                    scrapedresult = CleanTitle(Web.HttpUtility.HtmlDecode(Regex.Match(OriginalTitle, ".*(?=\s\(\d+.*?\))").ToString)).Trim
                    'only update nMovie if scraped result is not empty/nothing!
                    If Not String.IsNullOrEmpty(scrapedresult) Then
                        nMovie.OriginalTitle = scrapedresult
                    End If

                    'MOVIE TITLE
                    If Not String.IsNullOrEmpty(ForceTitleLanguage) Then
                        'only update nMovie if scraped result is not empty/nothing!
                        scrapedresult = GetForcedTitle(strID, nMovie.OriginalTitle, WorldWideTitleFallback, ForceTitleLanguage)
                        If Not String.IsNullOrEmpty(scrapedresult) Then
                            nMovie.Title = scrapedresult
                        End If
                    Else
                        'only update nMovie if scraped result is not empty/nothing!
                        If Not String.IsNullOrEmpty(nMovie.OriginalTitle) Then
                            nMovie.Title = nMovie.OriginalTitle.Trim
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                If GetPoster Then
                    sPoster = Regex.Match(Regex.Match(HTML, "(?<=\b(name=""poster"")).*\b[</a>]\b").ToString, "(?<=\b(src=)).*\b(?=[</a>])").ToString.Replace("""", String.Empty).Replace("/></", String.Empty)
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'MOVIE YEAR
                If Options.bYear Then
                    scrapedresult = Regex.Match(OriginalTitle, "(?<=\()\d+(?=.*\))", RegexOptions.RightToLeft).ToString
                    'only update nMovie if scraped result is not empty/nothing!
                    If Not String.IsNullOrEmpty(scrapedresult) Then
                        nMovie.Year = scrapedresult
                    End If
                End If


                Dim D, W, tempD As Integer

                'MOVIE MPAA / Certifications
                If Options.bCert Then
                    tempD = If(HTML.IndexOf("<h5><a href=""/mpaa"">MPAA</a>:</h5>") > 0, HTML.IndexOf("<h5><a href=""/mpaa"">MPAA</a>:</h5>"), 0)
                    D = If(tempD > 0, HTML.IndexOf("<div class=""info-content"">", tempD), 0)
                    W = If(D > 0, HTML.IndexOf("</div", D), 0)
                    scrapedresult = If(D > 0 AndAlso W > 0, Web.HttpUtility.HtmlDecode(HTML.Substring(D, W - D).Remove(0, 26)).Trim(), String.Empty)
                    'only update nMovie if scraped result is not empty/nothing!
                    If Not String.IsNullOrEmpty(scrapedresult) Then
                        nMovie.MPAA = scrapedresult
                    End If

                    If bwIMDB.CancellationPending Then Return Nothing
                    ' MOVIE certifications

                    'get certifications
                    D = HTML.IndexOf("<h5>Certification:</h5>")
                    If D > 0 Then
                        W = HTML.IndexOf("</div>", D)
                        Dim rCert As MatchCollection = Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN_3)

                        If rCert.Count > 0 Then
                            Dim Cert = From M In rCert Select N = String.Format("{0}:{1}", DirectCast(M, Match).Groups(1).ToString.Trim, DirectCast(M, Match).Groups(2).ToString.Trim) Order By N Descending Where _
                                       N.Contains(APIXML.MovieCertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = Master.eSettings.MovieScraperCertLang).name)
                            If Not String.IsNullOrEmpty(Master.eSettings.MovieScraperCertLang) Then
                                If Cert.Count > 0 Then
                                    scrapedresult = Cert(0).ToString.Replace("West", String.Empty).Trim
                                    'only update nMovie if scraped result is not empty/nothing!
                                    If Not String.IsNullOrEmpty(scrapedresult) Then
                                        nMovie.Certification = scrapedresult
                                    End If
                                End If
                            Else
                                nMovie.Certification = Strings.Join(Cert.ToArray, " / ").Trim
                            End If
                        End If
                    End If
                    'nahh doesn't make any sense to store a US only MPAA setting into certification field!
                    'If String.IsNullOrEmpty(nMovie.Certification) AndAlso Not String.IsNullOrEmpty(nMovie.MPAA) Then
                    '    nMovie.Certification = nMovie.MPAA
                    'End If

                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'MOVIE RELEASEDATE
                If Options.bRelease Then
                    Dim RelDate As Date
                    Dim sRelDate As MatchCollection = Regex.Matches(HTML, "<h5>Release Date:</h5>.*?(?<DATE>\d+\s\w+\s\d\d\d\d\s)", RegexOptions.Singleline)
                    If sRelDate.Count > 0 Then
                        If Date.TryParse(sRelDate.Item(0).Groups(1).Value, RelDate) Then
                            nMovie.ReleaseDate = Strings.FormatDateTime(RelDate, DateFormat.ShortDate).ToString
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'MOVIE RATING IMDB
                If Options.bRating Then
                    Dim RegexRating As String = Regex.Match(HTML, "\b\d\W\d/\d\d").ToString
                    If String.IsNullOrEmpty(RegexRating) = False Then
                        nMovie.Rating = RegexRating.Split(Convert.ToChar("/")).First.Trim
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'IMDB trailer
                If Options.bTrailer Then
                    'Get first IMDB trailer if possible
                    Dim trailers As List(Of String) = GetTrailers(nMovie.IMDBID)
                    nMovie.Trailer = trailers.FirstOrDefault()
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'IMDB Votes
                If Options.bVotes Then nMovie.Votes = Regex.Match(HTML, "class=""tn15more"">([0-9,]+) votes</a>").Groups(1).Value.Trim
                'Votes should be rounded value
                nMovie.Votes = nMovie.Votes.Replace(".", "").Replace(",", "")


                'IMDB Top250
                'ie: <a href="/chart/top?tt0167260">Top 250: #13</a>
                If Options.bTop250 Then
                    scrapedresult = Regex.Match(HTML, String.Concat("/chart/top\?tt", nMovie.IMDBID, """>Top 250: #([0-9]+)</a>")).Groups(1).Value.Trim
                    'only update nMovie if scraped result is not empty/nothing!
                    If Not String.IsNullOrEmpty(scrapedresult) Then
                        nMovie.Top250 = scrapedresult
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'IMDB Actors
                If Options.bCast Then
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
                                If(m3.Groups("thumb").ToString.IndexOf("addtiny") > 0 OrElse m3.Groups("thumb").ToString.IndexOf("no_photo") > 0, String.Empty, Strings.Replace(Web.HttpUtility.HtmlDecode(m3.Groups("thumb").ToString.Trim), _
                                "._SX23_SY30_.jpg", String.Concat("._", ThumbsSize, "_.jpg")))) Take 999999

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

                D = 0 : W = 0

                'MOVIE TAGLINE
                If Options.bTagline Then
                    tempD = If(HTML.IndexOf("<h5>Tagline:</h5>") > 0, HTML.IndexOf("<h5>Tagline:</h5>"), 0)
                    D = If(tempD > 0, HTML.IndexOf("<div class=""info-content"">", tempD), 0)
                    Dim lHtmlIndexOf As Integer = If(D > 0, HTML.IndexOf("<a class=""tn15more inline""", D), 0)
                    Dim TagLineEnd As Integer = If(lHtmlIndexOf > 0, lHtmlIndexOf, 0)
                    If D > 0 Then W = If(TagLineEnd > 0, TagLineEnd, HTML.IndexOf("</div>", D))
                    scrapedresult = If(D > 0 AndAlso W > 0, Web.HttpUtility.HtmlDecode(HTML.Substring(D, W - D).Replace("<h5>Tagline:</h5>", String.Empty).Split(vbCrLf.ToCharArray)(1)).Trim, String.Empty)
                    'only update nMovie if scraped result is not empty/nothing!
                    If Not String.IsNullOrEmpty(scrapedresult) Then
                        nMovie.Tagline = scrapedresult
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'MOVIE DIRECTOR
                If Options.bDirector Then
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

                'MOVIE COUNTRIES
                'Get countries of the movie
                If Options.bCountry Then
                    'Get the countries
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
                            'fix for display country flag in XBMC! 
                            If Strings.Join(Cou.ToArray, " / ").Trim.ToUpper.Contains("USA") Then
                                '       nMovie.Country = "United States of America"
                                nMovie.Countries.Add("United States of America")
                            ElseIf Strings.Join(Cou.ToArray, " / ").Trim.ToUpper.Contains("UK") Then
                                ' nMovie.Country = "United Kingdom"
                                nMovie.Countries.Add("United Kingdom")
                            Else
                                ' nMovie.Country = Strings.Join(Cou.ToArray, " / ").Trim()
                                nMovie.Countries.AddRange(Cou.ToList)
                            End If

                        End If
                    End If


                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'MOVIE GENRES
                'Get genres of the movie
                If Options.bGenre Then
                    D = 0 : W = 0
                    D = HTML.IndexOf("<h5>Genre:</h5>")
                    'Check if doesnt find genres
                    If D > 0 Then
                        W = HTML.IndexOf("</div>", D)

                        If W > 0 Then
                            Dim rGenres As MatchCollection = Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN)
                            Dim Gen = From M In rGenres _
                                      Select N = Web.HttpUtility.HtmlDecode(DirectCast(M, Match).Groups("name").ToString) Where Not N.Contains("more") Take 999999
                            If Gen.Count > 0 Then
                                Dim tGenre As String = Strings.Join(Gen.ToArray, "/").Trim
                                'we don't use language filter here - instead do this in MergeScraperResult function
                                '       tGenre = StringUtils.GenreFilter(tGenre)
                                'only update nMovie if scraped result is not empty/nothing!
                                If Not String.IsNullOrEmpty(tGenre) Then
                                    '  nMovie.Genre = Strings.Join(tGenre.Split(Convert.ToChar("/")), " / ").Trim
                                    nMovie.Genres.AddRange(Gen.ToList)
                                End If
                            End If
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'MOVIE OUTLINE
                If Options.bOutline Then

                    'Get the Plot Outline
                    D = 0 : W = 0

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

mPlot:          'MOVIE PLOT
                'Get the full Plot
                If Options.bPlot Then
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

                'MOVIE DURATION
                'Get the movie duration
                If Options.bRuntime Then
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

                'MOVIE STUDIO
                'Get Production Studio
                If Options.bStudio Then
                    D = 0 : W = 0
                    If FullCrew Then
                        D = HTML.IndexOf("<b class=""blackcatheader"">Production Companies</b>")
                        If D > 0 Then W = HTML.IndexOf("</ul>", D)
                        If D > 0 AndAlso W > 0 Then
                            'only get the first one
                            Dim Ps = From P1 In Regex.Matches(HTML.Substring(D, W - D), HREF_PATTERN) _
                                     Where Not DirectCast(P1, Match).Groups("name").ToString = String.Empty _
                                     Select Studio = Web.HttpUtility.HtmlDecode(DirectCast(P1, Match).Groups("name").ToString) Take 1
                            nMovie.Studio = Ps(0).ToString.Trim
                        End If
                    Else
                        D = HTML.IndexOf("<h5>Company:</h5>")
                        If D > 0 Then W = HTML.IndexOf("</div>", D)
                        'only update nMovie if scraped result is not empty/nothing!
                        If D > 0 AndAlso W > 0 Then
                            nMovie.Studio = Web.HttpUtility.HtmlDecode(Regex.Match(HTML.Substring(D, W - D), HREF_PATTERN).Groups("name").ToString.Trim)
                        End If
                    End If
                End If

                If bwIMDB.CancellationPending Then Return Nothing

                'MOVIE WRITERS
                'Get Writers
                If Options.bWriters Then
                    D = 0 : W = 0
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

                'MOVIE OTHER
                'Get All Other Info
                If FullCrew OrElse Options.bMusicBy OrElse Options.bProducers OrElse Options.bOtherCrew Then

                    D = 0 : W = 0
                    D = HTML.IndexOf("Directed by</a></h5>")
                    If D > 0 Then W = HTML.IndexOf("</body>", D)
                    If D > 0 AndAlso W > 0 Then
                        Dim qTables As MatchCollection = Regex.Matches(HTML.Substring(D, W - D), TABLE_PATTERN)

                        For Each M As Match In qTables

                            If bwIMDB.CancellationPending Then Return Nothing

                            'Producers
                            If (Options.bProducers OrElse FullCrew) AndAlso M.ToString.Contains("Produced by</a></h5>") Then
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
                            If (Options.bMusicBy OrElse FullCrew) AndAlso M.ToString.Contains("Original Music by</a></h5>") Then
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
                    If (Options.bOtherCrew OrElse FullCrew) Then
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

        Public Function GetMovieStudios(ByVal strID As String) As List(Of String)
            Dim alStudio As New List(Of String)
            If (String.IsNullOrEmpty(strID)) Then
                logger.Warn("Attempting to GetMovieStudios with invalid ID <{0}>", strID)
                Return alStudio
            End If
            Dim HTML As String
            Using sHTTP As New HTTP
                HTML = sHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", strID, "/combined"))
            End Using
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

        Public Function GetSearchMovieInfo(ByVal sMovieName As String, ByRef dbMovie As Structures.DBMovie, ByRef nMovie As MediaContainers.Movie, ByVal iType As Enums.ScrapeType, ByVal Options As Structures.ScrapeOptions_Movie, ByVal FullCrew As Boolean, ByVal WorldWideTitleFallback As Boolean, ByVal ForceTitleLanguage As String) As MediaContainers.Movie
            Dim r As MovieSearchResults = SearchMovie(sMovieName)
            Dim b As Boolean = False


            'r.PopularTitles.Sort()
            'r.ExactMatches.Sort()
            'r.PartialMatches.Sort()

            Try
                Select Case iType
                    Case Enums.ScrapeType.FullAsk, Enums.ScrapeType.UpdateAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.MarkAsk, Enums.ScrapeType.FilterAsk

                        If r.ExactMatches.Count = 1 Then 'AndAlso r.PopularTitles.Count = 0 AndAlso r.PartialMatches.Count = 0 Then 'redirected to imdb info page
                            b = GetMovieInfo(r.ExactMatches.Item(0).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage)
                        ElseIf r.PopularTitles.Count = 1 AndAlso r.PopularTitles(0).Lev <= 5 Then
                            b = GetMovieInfo(r.PopularTitles.Item(0).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage)
                        ElseIf r.ExactMatches.Count = 1 AndAlso r.ExactMatches(0).Lev <= 5 Then
                            b = GetMovieInfo(r.ExactMatches.Item(0).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage)
                        Else
                            Master.tmpMovie.Clear()
                            Using dIMDB As New dlgIMDBSearchResults
                                If dIMDB.ShowDialog(r, sMovieName, dbMovie.Filename) = Windows.Forms.DialogResult.OK Then
                                    If String.IsNullOrEmpty(Master.tmpMovie.IMDBID) Then
                                        b = False
                                    Else
                                        b = GetMovieInfo(Master.tmpMovie.IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage)
                                    End If
                                Else
                                    b = False
                                End If
                            End Using
                        End If

                    Case Enums.ScrapeType.FilterSkip, Enums.ScrapeType.FullSkip, Enums.ScrapeType.MarkSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.UpdateSkip
                        If r.ExactMatches.Count = 1 Then
                            b = GetMovieInfo(r.ExactMatches.Item(0).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage)
                        End If

                    Case Enums.ScrapeType.FullAuto, Enums.ScrapeType.UpdateAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.SingleScrape, Enums.ScrapeType.FilterAuto

                        'check if ALL results are over lev value
                        Dim useAnyway As Boolean = False
                        If ((r.PopularTitles.Count > 0 AndAlso r.PopularTitles(0).Lev > 5) OrElse r.PopularTitles.Count = 0) AndAlso _
                        ((r.ExactMatches.Count > 0 AndAlso r.ExactMatches(0).Lev > 5) OrElse r.ExactMatches.Count = 0) AndAlso _
                        ((r.PartialMatches.Count > 0 AndAlso r.PartialMatches(0).Lev > 5) OrElse r.PartialMatches.Count = 0) Then
                            useAnyway = True
                        End If
                        Dim exactHaveYear As Integer = FindYear(dbMovie.Filename, r.ExactMatches)
                        Dim popularHaveYear As Integer = FindYear(dbMovie.Filename, r.PopularTitles)
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
                            b = GetMovieInfo(r.ExactMatches.Item(0).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage)
                        ElseIf r.ExactMatches.Count > 1 AndAlso exactHaveYear >= 0 Then
                            b = GetMovieInfo(r.ExactMatches.Item(exactHaveYear).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage)
                        ElseIf r.PopularTitles.Count > 0 AndAlso popularHaveYear >= 0 Then
                            b = GetMovieInfo(r.PopularTitles.Item(popularHaveYear).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage)
                        ElseIf r.ExactMatches.Count > 0 AndAlso (r.ExactMatches(0).Lev <= 5 OrElse useAnyway) Then
                            b = GetMovieInfo(r.ExactMatches.Item(0).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage)
                        ElseIf r.PopularTitles.Count > 0 AndAlso (r.PopularTitles(0).Lev <= 5 OrElse useAnyway) Then
                            b = GetMovieInfo(r.PopularTitles.Item(0).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage)
                        ElseIf r.PartialMatches.Count > 0 AndAlso (r.PartialMatches(0).Lev <= 5 OrElse useAnyway) Then
                            b = GetMovieInfo(r.PartialMatches.Item(0).IMDBID, nMovie, FullCrew, False, Options, True, WorldWideTitleFallback, ForceTitleLanguage)
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

        Private Function FindYear(ByVal tmpname As String, ByVal lst As List(Of MediaContainers.Movie)) As Integer
            Dim tmpyear As String = ""
            Dim i As Integer
            Dim ret As Integer = -1
            tmpname = Path.GetFileNameWithoutExtension(tmpname)
            tmpname = tmpname.Replace(".", " ").Trim.Replace("(", " ").Replace(")", "").Trim
            i = tmpname.LastIndexOf(" ")
            If i >= 0 Then
                tmpyear = tmpname.Substring(i + 1, tmpname.Length - i - 1)
                If IsNumeric(tmpyear) AndAlso Convert.ToInt32(tmpyear) > 1950 Then 'let's assume there are no movies older then 1950
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

        Public Sub GetSearchMovieInfoAsync(ByVal imdbID As String, ByVal IMDBMovie As MediaContainers.Movie, ByVal Options As Structures.ScrapeOptions_Movie)
            Try
                If Not bwIMDB.IsBusy Then
                    bwIMDB.WorkerReportsProgress = False
                    bwIMDB.WorkerSupportsCancellation = True
                    bwIMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails, _
                                           .Parameter = imdbID, .IMDBMovie = IMDBMovie, .Options = Options})
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub SearchMovieAsync(ByVal sMovie As String, ByVal filterOptions As Structures.ScrapeOptions_Movie)
            Try
                If Not bwIMDB.IsBusy Then
                    bwIMDB.WorkerReportsProgress = False
                    bwIMDB.WorkerSupportsCancellation = True
                    bwIMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.Movies, .Parameter = sMovie, .Options = filterOptions})
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Private Sub bwIMDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwIMDB.DoWork
            Dim Args As Arguments = DirectCast(e.Argument, Arguments)

            Try
                Select Case Args.Search
                    Case SearchType.Movies
                        Dim r As MovieSearchResults = SearchMovie(Args.Parameter)
                        e.Result = New Results With {.ResultType = SearchType.Movies, .Result = r}
                    Case SearchType.SearchDetails
                        Dim s As Boolean = GetMovieInfo(Args.Parameter, Args.IMDBMovie, False, True, Args.Options, True, True, "")
                        e.Result = New Results With {.ResultType = SearchType.SearchDetails, .Success = s}
                End Select
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Private Sub BW_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwIMDB.RunWorkerCompleted
            Dim Res As Results = DirectCast(e.Result, Results)

            Try
                Select Case Res.ResultType
                    Case SearchType.Movies
                        RaiseEvent SearchResultsDownloaded(DirectCast(Res.Result, MovieSearchResults))
                    Case SearchType.SearchDetails
                        Dim movieInfo As MovieSearchResults = DirectCast(Res.Result, MovieSearchResults)
                        RaiseEvent SearchMovieInfoDownloaded(sPoster, Res.Success)
                End Select
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Private Function CleanTitle(ByVal sString As String) As String
            Dim CleanString As String = sString

            Try
                If sString.StartsWith("""") Then CleanString = sString.Remove(0, 1)

                If sString.EndsWith("""") Then CleanString = CleanString.Remove(CleanString.Length - 1, 1)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
            Return CleanString
        End Function

        Private Function GetForcedTitle(ByVal strID As String, ByVal oTitle As String, ByVal WorldWideTitleFallback As Boolean, ByVal ForceTitleLanguage As String) As String
            Dim fTitle As String = oTitle

            Try
                If bwIMDB.CancellationPending Then Return Nothing
                Dim HTML As String
                Using sHTTP As New HTTP
                    HTML = sHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", strID, "/releaseinfo#akas"))
                End Using

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
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return fTitle
            End Try
        End Function

        Private Function GetMovieID(ByVal strObj As String) As String
            Return Regex.Match(strObj, IMDB_ID_REGEX).ToString.Replace("tt", String.Empty)
        End Function

        Private Function SearchMovie(ByVal sMovie As String) As MovieSearchResults
            Try

                Dim D, W As Integer
                Dim R As New MovieSearchResults

                Dim HTML As String = String.Empty
                Dim HTMLt As String = String.Empty
                Dim HTMLp As String = String.Empty
                Dim HTMLm As String = String.Empty
                Dim HTMLe As String = String.Empty
                Dim HTMLv As String = String.Empty
                Dim rUri As String = String.Empty

                Using sHTTP As New HTTP
                    HTML = sHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/find?q=", Web.HttpUtility.UrlEncode(sMovie), "&s=tt&ttype=ft"))
                    HTMLe = sHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/find?q=", Web.HttpUtility.UrlEncode(sMovie), "&s=tt&ttype=ft&exact=true&ref_=fn_tt_ex"))
                    rUri = sHTTP.ResponseUri

                    If clsAdvancedSettings.GetBooleanSetting("SearchTvTitles", False) Then
                        HTMLt = sHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/search/title?title=", Web.HttpUtility.UrlEncode(sMovie), "&title_type=tv_movie"))
                    End If
                    If clsAdvancedSettings.GetBooleanSetting("SearchVideoTitles", False) Then
                        HTMLv = sHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/search/title?title=", Web.HttpUtility.UrlEncode(sMovie), "&title_type=video"))
                    End If
                    If clsAdvancedSettings.GetBooleanSetting("SearchPartialTitles", True) Then
                        HTMLm = sHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/find?q=", Web.HttpUtility.UrlEncode(sMovie), "&s=tt&ttype=ft&ref_=fn_ft"))
                    End If
                    If clsAdvancedSettings.GetBooleanSetting("SearchPopularTitles", True) Then
                        HTMLp = sHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/find?q=", Web.HttpUtility.UrlEncode(sMovie), "&s=tt&ttype=ft&ref_=fn_tt_pop"))
                    End If
                End Using

                'Check if we've been redirected straight to the movie page
                If Regex.IsMatch(rUri, IMDB_ID_REGEX) Then
                    Dim lNewMovie As MediaContainers.Movie = New MediaContainers.Movie(Regex.Match(rUri, IMDB_ID_REGEX).ToString, _
                        StringUtils.ProperCase(sMovie), Regex.Match(Regex.Match(HTML, MOVIE_TITLE_PATTERN).ToString, "(?<=\()\d+(?=.*\))").ToString, 0)
                    R.ExactMatches.Add(lNewMovie)
                    Return R
                End If

                'popular titles
                D = HTMLp.IndexOf("</a>Titles</h3>")
                If D <= 0 Then GoTo mPartial
                W = HTMLp.IndexOf("</table>", D) + 8

                Dim Table As String = Regex.Match(HTML.Substring(D, W - D), TABLE_PATTERN).ToString

                Dim qPopular = From Mtr In Regex.Matches(Table, TITLE_PATTERN) _
                               Where Not DirectCast(Mtr, Match).Groups("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups("type").ToString.Contains("VG") _
                               Select New MediaContainers.Movie(GetMovieID(DirectCast(Mtr, Match).Groups("url").ToString), _
                                                Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString), Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(sMovie).ToLower, StringUtils.FilterYear(Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString)).ToLower))

                R.PopularTitles = qPopular.ToList

mPartial:
                D = HTMLm.IndexOf("</a>Titles</h3>")
                If D <= 0 Then GoTo mTV
                W = HTMLm.IndexOf("</table>", D) + 8

                Table = Regex.Match(HTMLm.Substring(D, W - D), TABLE_PATTERN).ToString
                Dim qpartial = From Mtr In Regex.Matches(Table, TITLE_PATTERN) _
                    Where Not DirectCast(Mtr, Match).Groups("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups("type").ToString.Contains("VG") _
                    Select New MediaContainers.Movie(GetMovieID(DirectCast(Mtr, Match).Groups("url").ToString), _
                                     Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString), Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(sMovie).ToLower, StringUtils.FilterYear(Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString)).ToLower))

                R.PartialMatches = qpartial.ToList

mTv:
                D = HTMLt.IndexOf("<table class=""results"">")
                If D <= 0 Then GoTo mVideo
                W = HTMLt.IndexOf("</table>", D) + 8

                Table = HTMLt.Substring(D, W - D).ToString
                Dim qtvmovie = From Mtr In Regex.Matches(Table, TvTITLE_PATTERN) _
                    Where Not DirectCast(Mtr, Match).Groups("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups("type").ToString.Contains("VG") _
                    Select New MediaContainers.Movie(GetMovieID(DirectCast(Mtr, Match).Groups("url").ToString), _
                                     Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString), Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(sMovie).ToLower, StringUtils.FilterYear(Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString)).ToLower))

                R.TvTitles = qtvmovie.ToList

mVideo:
                D = HTMLv.IndexOf("<table class=""results"">")
                If D <= 0 Then GoTo mExact
                W = HTMLv.IndexOf("</table>", D) + 8

                Table = HTMLv.Substring(D, W - D).ToString
                Dim qvideo = From Mtr In Regex.Matches(Table, TvTITLE_PATTERN) _
                    Where Not DirectCast(Mtr, Match).Groups("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups("type").ToString.Contains("VG") _
                    Select New MediaContainers.Movie(GetMovieID(DirectCast(Mtr, Match).Groups("url").ToString), _
                                     Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString), Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(sMovie).ToLower, StringUtils.FilterYear(Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString)).ToLower))

                R.VideoTitles = qvideo.ToList

mExact:
                D = HTMLe.IndexOf("</a>Titles</h3>")
                If D <= 0 Then GoTo mResult
                W = HTMLe.IndexOf("</table>", D) + 8

                Table = String.Empty
                Table = Regex.Match(HTMLe.Substring(D, W - D), TABLE_PATTERN).ToString

                Dim qExact = From Mtr In Regex.Matches(Table, TITLE_PATTERN) _
                               Where Not DirectCast(Mtr, Match).Groups("name").ToString.Contains("<img") AndAlso Not DirectCast(Mtr, Match).Groups("type").ToString.Contains("VG") _
                               Select New MediaContainers.Movie(GetMovieID(DirectCast(Mtr, Match).Groups("url").ToString), _
                            Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString.ToString), Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("year").ToString), StringUtils.ComputeLevenshtein(StringUtils.FilterYear(sMovie).ToLower, StringUtils.FilterYear(Web.HttpUtility.HtmlDecode(DirectCast(Mtr, Match).Groups("name").ToString)).ToLower))

                R.ExactMatches = qExact.ToList

mResult:
                Return R
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function

        Public Function GetTrailers(imdbID As String) As List(Of String)
            Dim TrailerList As New List(Of String)
            Dim TrailerNumber As Integer = 0
            Dim Links As MatchCollection
            Dim trailerPage As String
            Dim trailerUrl As String
            Dim Link As Match
            Dim currPage As Integer = 0

            Using WebPage As New HTTP
                Dim _ImdbTrailerPage As String = String.Empty

                _ImdbTrailerPage = WebPage.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", imdbID, "/videogallery/content_type-Trailer"))
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
                                    _ImdbTrailerPage = WebPage.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", imdbID, "/videogallery/content_type-Trailer?page=", i))
                                End If

                                Links = Regex.Matches(_ImdbTrailerPage, "screenplay/(vi[0-9]+)/")
                                Dim linksCollection As String() = From m As Object In Links Select CType(m, Match).Value Distinct.ToArray()

                                Links = Regex.Matches(_ImdbTrailerPage, "imdb/(vi[0-9]+)/")
                                linksCollection = linksCollection.Concat(From m As Object In Links Select CType(m, Match).Value Distinct.ToArray()).ToArray

                                For Each value As String In linksCollection
                                    If value.Contains("screenplay") Then
                                        trailerPage = WebPage.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/video/", value, "player"))
                                        trailerUrl = Web.HttpUtility.UrlDecode(Regex.Match(trailerPage, "http.+mp4").Value)
                                        If Not String.IsNullOrEmpty(trailerUrl) AndAlso WebPage.IsValidURL(trailerUrl) Then
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
            End Using

            Return TrailerList
        End Function

#End Region 'Methods

#Region "Nested Types"

        Private Structure Arguments

#Region "Fields"

            Dim FullCast As Boolean
            Dim FullCrew As Boolean
            Dim IMDBMovie As MediaContainers.Movie
            Dim Options As Structures.ScrapeOptions_Movie
            Dim Parameter As String
            Dim Search As SearchType

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

