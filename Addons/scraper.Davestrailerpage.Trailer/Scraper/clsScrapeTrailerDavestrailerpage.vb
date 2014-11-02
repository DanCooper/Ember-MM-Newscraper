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

Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Namespace Davestrailerpage

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Private originaltitle As String
        Private _trailerlist As New List(Of Trailers)
        Private imdbid As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property TrailerList() As List(Of Trailers)
            Get
                Return _trailerlist
            End Get
            Set(ByVal value As List(Of Trailers))
                _trailerlist = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"
        Public Async Function Init(ByVal sOriginalTitle As String, ByVal sIMDBID As String) As Threading.Tasks.Task
            originaltitle = sOriginalTitle
            imdbid = sIMDBID
            Await GetMovieTrailers()
        End Function

        Private Sub Clear()
            _trailerlist = New List(Of Trailers)
        End Sub


        ''' <summary>
        ''' Scrapes avalaible trailerlinks from http://www.davestrailerpage.co.uk/
        ''' </summary>
        ''' <remarks>Try to find trailerlinks for selected movie and add results to global trailerlist
        ''' 
        ''' 2014/10/05 Cocotus - First implementation
        ''' Steps: 
        ''' 1. Look for movie on  http://www.davestrailerpage.co.uk/ using originaltitle and download HTML of site! Example: http://www.davestrailerpage.co.uk/trailers_0to9.html
        ''' 2. Extract moviesection using Originaltitle/IMDBID 
        ''' 3. Further extract trailerlinks of moviesection.
        ''' 4. Add found trailerlink to global list
        ''' </remarks>
        Private Async Function GetMovieTrailers() As Task
            Try
                If originaltitle <> "" Then
                    'constant URL-part of query
                    Dim BaseURL As String = ""
                    'dynamic URL-part of query
                    Dim SearchURL As String = ""
                    'webrequest-object of Ember used to scrape Webpages
                    Dim sHTTP As New HTTP
                    'retrieved JSON string
                    Dim sjson As String = ""
                    'downloaded HTML of a webpage
                    Dim sHtml As String = ""
                    'title to search for
                    Dim searchtitle As String = originaltitle.ToLower
                    If searchtitle.StartsWith("the ") Then
                        searchtitle = searchtitle.Replace("the ", "")
                    ElseIf searchtitle.StartsWith("an ") Then
                        searchtitle = searchtitle.Replace("an ", "")
                    ElseIf searchtitle.StartsWith("a ") Then
                        searchtitle = searchtitle.Replace("a ", "")
                    End If
                    'TODO: Optional: originaltitle may contain not supported characters which must be filtered/cleaned first!?
                    'searchtitle = StringUtils.RemovePunctuation(originaltitle)
                    'URL to moviepage which contains downloadlinks
                    Dim TrailerWEBPageURL As String = ""
                    'Step 1: Build URL to movie on http://www.davestrailerpage.co.uk/!
                    'depending on beginning letter of movietitle we need to scrape different HTML site

                    If IsNumeric(searchtitle(0)) Then
                        searchtitle = "0to9.html"
                    ElseIf searchtitle(0).ToString.ToLower = "x" OrElse searchtitle(0).ToString.ToLower = "z" OrElse searchtitle(0).ToString.ToLower = "y" Then
                        searchtitle = "xyz.html"
                    Else
                        searchtitle = (StringUtils.RemovePunctuation(searchtitle)).Replace(" ", "")(0) & ".html"
                    End If
                    'build URL
                    BaseURL = "http://www.davestrailerpage.co.uk/trailers_"
                    SearchURL = String.Concat(BaseURL, searchtitle)
                    'download HTML
                    sHTTP = New HTTP
                    sHtml = ""
                    sHtml = Await sHTTP.DownloadData(SearchURL)
                    sHTTP = Nothing

                    'Step 2: Extract moviesection on downloaded HTML
                    If sHtml <> "" Then
                        'example of returned HTML:
                        '<tr>
                        '<td><ul><li><b>X-Men Origins: Wolverine</b>
                        '	<ul><li><a href="http://www.x-menorigins.com/" target="_blank">Official Website</a> / <a href="http://www.imdb.com/title/tt0458525/" target="_blank">IMDB</a>
                        '		<li><b>Trailer 1</b>
                        '			<ul><li><a href="http://largeassets.myspacecdn.com/creative/hd/wolverine_trlA/48043679_480p.mov">480P</a>
                        '			<li><a href="http://largeassets.myspacecdn.com/creative/hd/wolverine_trlA/48043679_720p.mov">720P</a>
                        '			<li><a href="http://largeassets.myspacecdn.com/creative/hd/wolverine_trlA/48043679_1080p.mov">1080P</a>
                        '			</ul>
                        '		<li><b>Trailer 3</b>
                        '			<ul><li><a href="http://trailers.apple.com/movies/fox/wolverine/wolverine-clip_h640w.mov">SD</a>
                        '			<li><a href="http://trailers.apple.com/movies/fox/wolverine/wolverine-clip_h480p.mov">480P</a>
                        '			<li><a href="http://trailers.apple.com/movies/fox/wolverine/wolverine-clip_h720p.mov">720P</a>
                        '			<li><a href="http://trailers.apple.com/movies/fox/wolverine/wolverine-clip_h1080p.mov">1080P</a>
                        '			</ul>
                        '   </ul>
                        '</ul></td>
                        '</tr>
                        'marks the start of moviesection in downloaded HTML
                        Dim intstartposmovie As Integer = 0

                        'either use IMDBID or movietitle to search for movie on page
                        If imdbid <> "" Then
                            intstartposmovie = sHtml.IndexOf(imdbid)
                        End If
                        'if IMDBID search didn't help, look for title
                        If intstartposmovie < 1 Then
                            'search for movietitle 
                            Dim movieResults As MatchCollection = Nothing
                            Dim movietitlePattern As String = "<td><ul><li><b>(?<TITLE>.*?)</b>"
                            movieResults = Regex.Matches(sHtml, movietitlePattern, RegexOptions.Singleline)
                            If Not movieResults Is Nothing AndAlso movieResults.Count > 0 Then
                                'Go through each movietitle found on page and compare it with originaltitle using Levenshtein algorithm
                                Dim compareresult As Integer = 100
                                Dim comparestring As String = ""
                                searchtitle = originaltitle.ToLower
                                If searchtitle.StartsWith("the ") Then
                                    searchtitle = searchtitle.Replace("the ", "")
                                ElseIf searchtitle.StartsWith("an ") Then
                                    searchtitle = searchtitle.Replace("an ", "")
                                ElseIf searchtitle.StartsWith("a ") Then
                                    searchtitle = searchtitle.Replace("a ", "")
                                End If
                                searchtitle = (StringUtils.RemovePunctuation(searchtitle)).Replace("the ", "").Replace(" ", "")
                                For Each movieresult As Match In movieResults
                                    comparestring = movieresult.Groups(1).Value.ToLower
                                    comparestring = comparestring.Replace("the ", "").Replace("an ", "").Replace("a ", "").Replace(", the", "").Replace(", a", "").Replace(", an", "")
                                    compareresult = 100
                                    comparestring = (StringUtils.RemovePunctuation(comparestring)).Replace(" ", "")
                                    compareresult = StringUtils.ComputeLevenshtein(comparestring, searchtitle)

                                    If (originaltitle.Length <= 5 AndAlso compareresult = 0) OrElse (originaltitle.Length > 5 AndAlso compareresult <= 2) Then
                                        logger.Debug("[" & originaltitle & "] Davetrailer - Movie found! Title: " & movieresult.Groups(1).Value & " URL: " & SearchURL)
                                        'set startpos
                                        intstartposmovie = sHtml.IndexOf(movieresult.Groups(1).Value)
                                        Exit For
                                    End If
                                Next
                            End If
                        Else
                            logger.Debug("[" & originaltitle & "] Davetrailer - Movie found! IMDBID: " & imdbid & " URL: " & SearchURL)
                        End If

                        'check if title was found
                        If intstartposmovie > 0 Then
                            'extracted HTML which contains information of movie (title,trailerlinks)
                            Dim strmoviesection As String = ""
                            strmoviesection = (sHtml.Substring(intstartposmovie)).Remove(sHtml.Substring(intstartposmovie).IndexOf("</tr"))

                            ' Step 3: Further extract trailerlinks of moviesection
                            If strmoviesection <> "" Then
                                'now look for trailers in found moviesection

                                'moviesection can have 2 different structures
                                'Solution 1:
                                '<tr>
                                '	<td><ul><li><b>28 Hotel Rooms</b>
                                '		<ul><li><a href="http://28hotelrooms.com/" target="_blank">Official Website</a> / <a href="http://www.imdb.com/title/tt2124074/" target="_blank">IMDB</a>
                                '			<li><b>Trailer 1</b> <a href="http://trailers.apple.com/movies/independent/28hotelrooms/28hotelrooms-tsr1_h640w.mov">SD</a> | <a href="http://trailers.apple.com/movies/independent/28hotelrooms/28hotelrooms-tsr1_h480p.mov">480P</a> | <a href="http://trailers.apple.com/movies/independent/28hotelrooms/28hotelrooms-tsr1_h720p.mov">720P</a> | <a href="http://trailers.apple.com/movies/independent/28hotelrooms/28hotelrooms-tsr1_h1080p.mov">1080P</a>
                                '			<li><b>Trailer 2</b> <a href="http://trailers.apple.com/movies/independent/28hotelrooms/28hotelrooms-tlr2_h480p.mov">480P</a> | <a href="http://trailers.apple.com/movies/independent/28hotelrooms/28hotelrooms-tlr2_h720p.mov">720P</a> | <a href="http://trailers.apple.com/movies/independent/28hotelrooms/28hotelrooms-tlr2_h1080p.mov">1080P</a>
                                '		</ul>
                                '	</ul></td>
                                '</tr>
                                'Solution 2:
                                '<tr>
                                '	<td><ul><li><b>28 Days Later</b>
                                '	<ul><li><a href="http://trailers.apple.com/405/us/media/trailers/fox_searchlight/28_days_later/28_days_later_m480.mov">Teaser</a> SD </li>
                                '	<li><a href="http://trailers.apple.com/405/us/media/trailers/fox_searchlight/28_days_later/28_days_later-tlr_m480.mov">Trailer</a> SD </li>
                                '	</ul>
                                '</ul></td>
                                '</tr>

                                'solution 1 handling
                                Dim trailerResults As MatchCollection = Nothing
                                Dim titlePattern As String = "<li><b>(?<TITLE>.*?)</b>"
                                trailerResults = Regex.Matches(strmoviesection, titlePattern, RegexOptions.Singleline)
                                If Not trailerResults Is Nothing AndAlso trailerResults.Count > 0 Then
                                    For Each trailerresult As Match In trailerResults
                                        'marks the start of trailer in moviesection
                                        Dim intstartpostrailer As Integer = 0
                                        intstartpostrailer = strmoviesection.IndexOf(trailerresult.Groups(1).Value)
                                        'extracted HTML which contains information of ONE trailer (url,quality)
                                        Dim strtrailersection As String = ""
                                        strtrailersection = (strmoviesection.Substring(intstartpostrailer)).Remove(strmoviesection.Substring(intstartpostrailer).IndexOf("</ul"))
                                        If strtrailersection <> "" Then
                                            Dim trailerURLResults As MatchCollection = Nothing
                                            Dim trailerlinkPattern As String = "<a href=""(?<URL>.*?)"">(?<QUALITY>.*?)</a>"
                                            trailerURLResults = Regex.Matches(strtrailersection, trailerlinkPattern, RegexOptions.Singleline)
                                            If Not trailerURLResults Is Nothing AndAlso trailerURLResults.Count > 0 Then
                                                For Each trailerlink As Match In trailerURLResults
                                                    Dim trailer As New Trailers
                                                    'trailer description
                                                    trailer.Description = trailerresult.Groups(1).Value
                                                    'trailer URLs
                                                    trailer.URL = trailerlink.Groups(1).Value
                                                    trailer.WebURL = trailerlink.Groups(1).Value
                                                    'trailer extension
                                                    trailer.Extention = IO.Path.GetExtension(trailer.URL)
                                                    'trailer source
                                                    trailer.Source = "Davestrailer"
                                                    '..and most important: trailer quality
                                                    If trailerlink.Groups(2).Value.Contains("1080") Then
                                                        '1080p
                                                        trailer.Quality = Enums.TrailerQuality.HD1080p
                                                    ElseIf trailerlink.Groups(2).Value.Contains("720") Then
                                                        '720p
                                                        trailer.Quality = Enums.TrailerQuality.HD720p
                                                    Else
                                                        'other                     
                                                        trailer.Quality = Enums.TrailerQuality.HQ480p
                                                    End If
                                                    ' Step 4: Add scraped trailer to global list
                                                    _trailerlist.Add(trailer)
                                                Next
                                            End If
                                        End If
                                    Next
                                    'solution 2 handling
                                Else
                                    Dim trailerURLResults As MatchCollection = Nothing
                                    Dim trailerlinkPattern As String = "<a href=""(?<URL>.*?)"">(?<TITLE>.*?)</a>(?<QUALITY>.*?)</li"
                                    strmoviesection = strmoviesection.Replace("\r\n", "").Replace("\n", "").Replace("\r", "")
                                    trailerURLResults = Regex.Matches(strmoviesection, trailerlinkPattern, RegexOptions.Singleline)
                                    If trailerURLResults Is Nothing OrElse trailerURLResults.Count < 1 Then
                                        trailerlinkPattern = "<a href=""(?<URL>.*?)"">(?<TITLE>.*?)</a>"
                                        trailerURLResults = Regex.Matches(strmoviesection, trailerlinkPattern, RegexOptions.Singleline)
                                    End If
                                    If Not trailerURLResults Is Nothing AndAlso trailerURLResults.Count > 0 Then
                                        For Each trailerlink As Match In trailerURLResults

                                            If trailerlink.Groups(1).Value.ToLower.Contains(".mov") OrElse trailerlink.Groups(1).Value.ToLower.Contains(".mp4") OrElse trailerlink.Groups(1).Value.ToLower.Contains(".flv") Then
                                                Dim trailer As New Trailers
                                                'trailer description
                                                trailer.Description = trailerlink.Groups(2).Value
                                                'trailer URLs
                                                trailer.URL = trailerlink.Groups(1).Value
                                                trailer.WebURL = trailerlink.Groups(1).Value
                                                'trailer extension
                                                trailer.Extention = IO.Path.GetExtension(trailer.URL)
                                                'trailer source
                                                trailer.Source = "Davestrailer"
                                                '..and most important: trailer quality
                                                If trailerlink.Groups(3).Value <> "" Then
                                                    If trailerlink.Groups(3).Value.Contains("1080") Then
                                                        '1080p
                                                        trailer.Quality = Enums.TrailerQuality.HD1080p
                                                    ElseIf trailerlink.Groups(3).Value.Contains("720") Then
                                                        '720p
                                                        trailer.Quality = Enums.TrailerQuality.HD720p
                                                    Else
                                                        'other                     
                                                        trailer.Quality = Enums.TrailerQuality.HQ480p
                                                    End If
                                                Else
                                                    If trailerlink.Groups(1).Value.Contains("1080") Then
                                                        '1080p
                                                        trailer.Quality = Enums.TrailerQuality.HD1080p
                                                    ElseIf trailerlink.Groups(1).Value.Contains("720") Then
                                                        '720p
                                                        trailer.Quality = Enums.TrailerQuality.HD720p
                                                    Else
                                                        'other                     
                                                        trailer.Quality = Enums.TrailerQuality.HQ480p
                                                    End If
                                                End If

                                                ' Step 4: Add scraped trailer to global list
                                                _trailerlist.Add(trailer)
                                            Else
                                                logger.Debug("[" & originaltitle & "] Invalid trailerllink: " & trailerlink.Groups(1).Value)
                                            End If
                                        Next
                                    Else
                                        logger.Debug("[" & originaltitle & "] Davetrailer - No trailerlinks found on page! URL: " & SearchURL)
                                    End If
                                End If
                            Else
                                logger.Debug("[" & originaltitle & "] Davetrailer - Moviesection NOT found on page! URL: " & SearchURL)
                            End If
                        Else
                            logger.Debug("[" & originaltitle & "] Davetrailer - Movie NOT found on page! URL: " & SearchURL)
                        End If
                    Else
                        logger.Warn("[" & originaltitle & "] Davetrailer - HTML could not be downloaded! URL: " & SearchURL)
                    End If
                Else
                    logger.Warn("[" & originaltitle & "] Davetrailer - Original title is empty, no scraping of trailers possible!")
                End If

                'log scraperesults for user
                If _trailerlist.Count < 1 Then
                    logger.Info("[" & originaltitle & "] Davetrailer - NO trailer scraped!")
                Else
                    logger.Info("[" & originaltitle & "] Davetrailer - Scraped " & _trailerlist.Count & " trailer!")
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Function

#End Region 'Methods

    End Class

End Namespace
