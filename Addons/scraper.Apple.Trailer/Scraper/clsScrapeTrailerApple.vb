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
Imports System.Net
Imports System.Web.Script.Serialization
Imports System.Dynamic

Namespace Apple

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Private originaltitle As String
        Private _trailerlist As New List(Of MediaContainers.Trailer)
        Private imdbid As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal sOriginalTitle As String, ByVal sIMDBID As String)
            Clear()
            originaltitle = sOriginalTitle
            imdbid = sIMDBID
            GetMovieTrailers()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property TrailerList() As List(Of MediaContainers.Trailer)
            Get
                Return _trailerlist
            End Get
            Set(ByVal value As List(Of MediaContainers.Trailer))
                _trailerlist = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Private Sub Clear()
            _trailerlist = New List(Of MediaContainers.Trailer)
        End Sub


        ''' <summary>
        ''' Scrapes avalaible trailerlinks from Apple
        ''' </summary>
        ''' <remarks>Try to find trailerlinks for selected movie and add results to global trailerlist
        ''' 
        ''' 2014/10/04 Cocotus - Instead of using google to search for apple trailers (google might block webrequest due spam!) we use API of Apple
        ''' Steps: 
        ''' 1. Use Apple quicksearch which will return json response! 'Example of query URL: http://trailers.apple.com/trailers/home/scripts/quickfind.php?q=Wrath of the Titans
        ''' 2. Parse Json response and get Url to moviepage! Example of returned json: ' {"error":false,"results":[{"title":"Wrath of the Titans","releasedate":"Fri, 30 Mar 2012 00:00:00 -0700","studio":"Warner Bros. Pictures","poster":"\/trailers\/wb\/wrathofthetitans\/images\/poster.jpg","moviesite":"http:\/\/www.wrathofthetitans.com","location":"\/trailers\/wb\/wrathofthetitans\/","urltype":"html","director":"Array","rating":"PG-13","genre":["Action and Adventure","Science Fiction","Fantasy"],"actors":["Danny Huston","Bill Nighy","Toby Kebbell","Sam Worthington","Liam Neeson","Edgar Ramirez","Ralph Fiennes","Rosamund Pike"],"trailers":[{"type":"Kronos Feature","postdate":"Thu, 29 Mar 2012 00:00:00 -0700","exclusive":false,"hd":true},{"type":"Cyclops Feature","postdate":"Wed, 28 Mar 2012 00:00:00 -0700","exclusive":false,"hd":true},{"type":"Minotaur Feature","postdate":"Tue, 27 Mar 2012 00:00:00 -0700","exclusive":false,"hd":true},{"type":"Makhai Feature","postdate":"Thu, 22 Mar 2012 00:00:00 -0700","exclusive":false,"hd":true},{"type":"Chimera Feature","postdate":"Tue, 20 Mar 2012 00:00:00 -0700","exclusive":false,"hd":true},{"type":"Trailer 2","postdate":"Thu, 23 Feb 2012 00:00:00 -0800","exclusive":false,"hd":true},{"type":"Trailer","postdate":"Mon, 19 Dec 2011 00:00:00 -0800","exclusive":false,"hd":true}]}]}
        ''''   For now we only extract "location" field of json - it contains the URL to trailerpage! Example: "\/trailers\/wb\/wrathofthetitans\/"
        ''''   Build complete trailer-URL: "http://trailers.apple.com/" & jsonlocationfield  Example: http://trailers.apple.com/trailers/wb/wrathofthetitans/
        ''''   If no json was downloaded, use Google to search for trailer! Example: "http://www.google.ch/search?q=apple+trailer+Transformers%3a+Age+of+Extinction"
        ''' 3. If URL to movie was found on http://trailers.apple.com/, download HTML and search for trailerlinks! Example: http://trailers.apple.com/trailers/paramount/transformersageofextinction/includes/large.html
        ''' 4. Add found trailerlink to global list
        ''' </remarks>
        Private Sub GetMovieTrailers()
            Try
                If Not String.IsNullOrEmpty(originaltitle) Then
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
                    Dim searchtitle As String = originaltitle
                    'TODO: Optional: originaltitle may contain not supported characters which must be filtered/cleaned first!?
                    'searchtitle = StringUtils.RemovePunctuation(originaltitle)
                    'URL to moviepage which contains downloadlinks
                    Dim TrailerWEBPageURL As String = ""

                    ' Step 1: Use Apple quicksearch which will return json response!
                    BaseURL = "http://trailers.apple.com/trailers/home/scripts/quickfind.php?q="
                    SearchURL = String.Concat(BaseURL, searchtitle)
                    sjson = sHTTP.DownloadData(SearchURL)
                    sHTTP = Nothing

                    ' Step 2: If json returned -> parse Json response and get Url to moviepage, else use google to search for trailer on Apple
                    If sjson = "" OrElse sjson.Length < 50 Then

                        logger.Debug("[" & originaltitle & "] Apple Trailer - Movie NOT found on http://trailers.apple.com/ - URL: " & String.Format("http://trailers.apple.com/trailers/home/scripts/quickfind.php?q={0}", searchtitle))
                        'no json -> Google search
                        'Warning: will be blocked by Google when spamming webrequests!
                        BaseURL = "http://www.google.ch/search?q=apple+trailer+"
                        searchtitle = Web.HttpUtility.UrlEncode(originaltitle)
                        SearchURL = String.Concat(BaseURL, searchtitle)
                        'performing google search to find avalaible links on apple trailers
                        sHTTP = New HTTP
                        sHtml = ""
                        sHtml = sHTTP.DownloadData(SearchURL)
                        sHTTP = Nothing
                        If sHtml <> "" Then
                            'Now extract links from googleresults
                            Dim trailerWEBURLResults As MatchCollection = Nothing
                            Dim googleresultPattern As String = "<a href=""/url\?q=(?<RESULTS>.*?)\/&amp;" 'Google search results
                            trailerWEBURLResults = Regex.Matches(sHtml, googleresultPattern, RegexOptions.Singleline)
                            'Go through each searchresults from google and compare found URL with originaltitle using Levenshtein algorithm
                            'check if something was found
                            If Not trailerWEBURLResults Is Nothing AndAlso trailerWEBURLResults.Count > 0 Then
                                Dim compareresult As Integer = 100
                                Dim comparestring As String = ""
                                searchtitle = (StringUtils.RemovePunctuation(originaltitle.ToLower)).Replace(" ", "")
                                For Each searchresult As Match In trailerWEBURLResults
                                    TrailerWEBPageURL = ""
                                    TrailerWEBPageURL = searchresult.Groups(1).Value
                                    If Not String.IsNullOrEmpty(TrailerWEBPageURL) Then
                                        Dim resultcompore As Integer = 0
                                        If TrailerWEBPageURL.Contains("/") Then
                                            comparestring = TrailerWEBPageURL.Remove(0, TrailerWEBPageURL.LastIndexOf("/") + 1)
                                            compareresult = 100
                                            comparestring = (StringUtils.RemovePunctuation(comparestring.ToLower)).Replace(" ", "")
                                            compareresult = StringUtils.ComputeLevenshtein(comparestring, searchtitle)
                                            If (originaltitle.Length <= 5 AndAlso compareresult = 0) OrElse (originaltitle.Length > 5 AndAlso compareresult <= 2) Then
                                                logger.Debug("[" & originaltitle & "] Apple Trailer - Google: Movie found! URL: " & TrailerWEBPageURL)
                                                Exit For
                                            Else
                                                logger.Debug("[" & originaltitle & "] Apple Trailer - Google: wrong result! URL: " & TrailerWEBPageURL)
                                                TrailerWEBPageURL = ""
                                            End If
                                        End If
                                    End If
                                Next
                            Else
                                logger.Debug("[" & originaltitle & "] Apple Trailer - Movie NOT found on http://trailers.apple.com/ using google! URL: " & SearchURL)
                            End If
                        Else
                            logger.Debug("[" & originaltitle & "] Apple Trailer - Movie NOT found on http://trailers.apple.com/ using google! URL: " & SearchURL)
                        End If

                    Else
                        Dim serializer = New JavaScriptSerializer
                        Dim jsonresult = serializer.Deserialize(Of AppleTrailerQuery)(sjson)

                        If Not jsonresult Is Nothing AndAlso Not jsonresult.results Is Nothing AndAlso jsonresult.results.Count > 0 Then
                            'TODO: maybe use less heavy URL: '"http://trailers.apple.com/" & jsonresult.results(0).location & "includes/playlists/itunes.inc"
                            Dim tmpurl As String = ("trailers.apple.com/" & jsonresult.results(0).location).Replace("//", "/")
                            TrailerWEBPageURL = "http://" & tmpurl
                            If String.IsNullOrEmpty(jsonresult.results(0).location) Then
                                logger.Debug("[" & originaltitle & "] Apple Trailer - Movie NOT found on http://trailers.apple.com/ - URL: " & TrailerWEBPageURL)
                            Else
                                logger.Debug("[" & originaltitle & "] Apple Trailer - Movie found on http://trailers.apple.com/ - URL: " & TrailerWEBPageURL)
                            End If
                        Else
                            logger.Debug("[" & originaltitle & "] Apple Trailer - Movie NOT found on http://trailers.apple.com/ - URL: " & String.Format("http://trailers.apple.com/trailers/home/scripts/quickfind.php?q={0}", searchtitle))
                        End If
                    End If

                    'Step 3: If URL to movie was found on http://trailers.apple.com/, download HTML of moviepage and search for trailerlinks
                    If Not String.IsNullOrEmpty(TrailerWEBPageURL) Then
                        logger.Info("[" & originaltitle & "] Apple Trailer - Movie found! Download URL: " & TrailerWEBPageURL)
                        Dim tDownloadURL As String = String.Empty
                        Dim tDescription As New List(Of String)
                        'get preferred quality setting
                        Dim prevQual As String = "480p"

                        prevQual = Master.eSettings.MovieTrailerPrefVideoQual.ToString

                        'Since prevQual value will be used to build trailerurl "All" should not be used at all!
                        If prevQual.Contains("All") Then
                            prevQual = Master.eSettings.MovieTrailerMinVideoQual.ToString
                            If prevQual.Contains("All") Then
                                prevQual = "480p"
                            End If
                        End If

                        If prevQual.Contains("720") Then
                            prevQual = "720p"
                        ElseIf prevQual.Contains("1080") Then
                            prevQual = "1080p"
                        End If
                        Dim urlHD As String = "/includes/extralarge.html"
                        Dim urlHQ As String = "/includes/large.html"

                        Dim TrailerSiteURL = String.Concat(TrailerWEBPageURL, urlHQ)
                        'i.e http://trailers.apple.com/trailers/paramount/transformersageofextinction/includes/large.html

                        'find avalaible links on apple movie site
                        sHTTP = New HTTP
                        sHtml = ""
                        sHtml = sHTTP.DownloadData(TrailerSiteURL)
                        sHTTP = Nothing

                        'looking for trailerlinks
                        ' Dim tPattern As String = "<a href=""(?<URL>.*?)"">(?<TITLE>.*?)</a>"
                        ' Dim lPattern As String = "<li><a href=""includes/(?<URL>.*?)#"
                        Dim uPattern As String = "<a class=""movieLink"" href=""(?<URL>.*?)\?"
                        Dim zPattern As String = "<li.*?<a href=""(?<LINK>.*?)#.*?<h3 title="".*?>(?<TITLE>.*?)</h3>.*?duration"">(?<DURATION>.*?)</span>*.?</li>"

                        Dim zResult As MatchCollection = Regex.Matches(sHtml, zPattern, RegexOptions.Singleline)
                        If Not zResult Is Nothing AndAlso zResult.Count > 0 Then
                            For ctr As Integer = 0 To zResult.Count - 1
                                ' Step 4: Add scraped trailer to global list
                                _trailerlist.Add(New MediaContainers.Trailer With {.URLVideoStream = zResult.Item(ctr).Groups(1).Value, .Title = zResult.Item(ctr).Groups(2).Value, .Duration = zResult.Item(ctr).Groups(3).Value})
                            Next

                            For Each trailer In _trailerlist
                                Dim DLURL As String = String.Empty
                                If TrailerWEBPageURL.EndsWith("/") = False Then TrailerWEBPageURL = TrailerWEBPageURL & "/"
                                Dim TrailerSiteLink As String = String.Concat(TrailerWEBPageURL, trailer.URLVideoStream)
                                'i.e http://trailers.apple.com/trailers/wb/wrathofthetitans/includes/kronosfeature/large.html

                                sHTTP = New HTTP
                                Dim zHtml As String = sHTTP.DownloadData(TrailerSiteLink)
                                sHTTP = Nothing

                                Dim yResult As MatchCollection = Regex.Matches(zHtml, uPattern, RegexOptions.Singleline)

                                If yResult.Count > 0 Then
                                    tDownloadURL = Web.HttpUtility.HtmlDecode(yResult.Item(0).Groups(1).Value)
                                    tDownloadURL = tDownloadURL.Replace("480p", prevQual)
                                    trailer.URLWebsite = tDownloadURL
                                    tDownloadURL = tDownloadURL.Replace("1080p", "h1080p")
                                    tDownloadURL = tDownloadURL.Replace("720p", "h720p")
                                    tDownloadURL = tDownloadURL.Replace("480p", "h480p")
                                    trailer.URLVideoStream = tDownloadURL
                                    trailer.Source = "Apple"
                                    Select Case prevQual
                                        Case "1080p"
                                            trailer.Quality = Enums.TrailerVideoQuality.HD1080p
                                        Case "720p"
                                            trailer.Quality = Enums.TrailerVideoQuality.HD720p
                                        Case "480p"
                                            trailer.Quality = Enums.TrailerVideoQuality.HQ480p
                                    End Select
                                    'set trailer extension
                                    trailer.TrailerOriginal.Extention = IO.Path.GetExtension(trailer.URLVideoStream)
                                End If
                            Next
                        End If
                    Else
                        logger.Info("[" & originaltitle & "] Apple Trailer - Movie NOT found")
                    End If
                Else
                    logger.Warn("[" & originaltitle & "] Apple Trailer - Original title is empty, no scraping of trailers possible!")
                End If

                'log scraperesults for user
                If _trailerlist.Count < 1 Then
                    logger.Info("[" & originaltitle & "] Apple Trailer - NO trailer scraped!")
                Else
                    logger.Info("[" & originaltitle & "] Apple Trailer - Scraped " & _trailerlist.Count & " trailer!")
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

        End Sub

#End Region 'Methods

    End Class

End Namespace

#Region "AppleTrailerJSONStructure"
Public Class AppleTrailerQuery
    Private m_results As AppleTrailerQueryResult()
    'TODO error is protected keyword in VB -> how to mask it?!
    ' Public Property errors As Boolean
    Public Property results As AppleTrailerQueryResult()
        Get
            Return m_results
        End Get
        Set(value As AppleTrailerQueryResult())
            m_results = value
        End Set
    End Property
End Class

Public Class AppleTrailerQueryResult
    Private m_releasedate As String
    Private m_location As String
    Public Property releasedate As String
        Get
            Return m_releasedate
        End Get
        Set(value As String)
            m_releasedate = value
        End Set
    End Property
    Public Property location As String
        Get
            Return m_location
        End Get
        Set(value As String)
            m_location = value
        End Set
    End Property


    Public Property title As String
    Public Property studio As String
    Public Property poster As String
    Public Property moviesite As String
    Public Property urltype As String
    Public Property director As String
    Public Property rating As String
    Public Property genre As String()
    Public Property actors As String()


    Private m_trailers As AppleTrailerQuality()
    Public Property trailers As AppleTrailerQuality()

        Get
            Return m_trailers
        End Get
        Set(value As AppleTrailerQuality())
            m_trailers = value
        End Set
    End Property

End Class
Public Class AppleTrailerQuality
    Public Property type As String
    Public Property postdate As String
    Public Property exclusive As Boolean
    Public Property hd As Boolean
End Class

#End Region
