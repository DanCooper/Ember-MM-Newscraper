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

Public Class IMDBTrailer

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private IMDBID As String
    Private _trailerlist As New List(Of Trailers)

#End Region 'Fields

#Region "Constructors"

    Public Sub New(ByVal sIMDBID As String)
        Clear()
        IMDBID = sIMDBID
        GetMovieTrailers()
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

    Private Sub Clear()
        _trailerlist = New List(Of Trailers)
    End Sub

    Private Sub GetMovieTrailers()



        Dim TrailerNumber As Integer = 0
        Dim Trailers As MatchCollection
        Dim Qualities As MatchCollection
        Dim trailerPage As String
        Dim trailerUrl As String
        Dim trailerTitle As String
        Dim Link As Match
        Dim currPage As Integer = 0

        Dim WebPage As New HTTP
        Dim _ImdbTrailerPage As String = String.Empty

        Try
            If Not String.IsNullOrEmpty(IMDBID) Then
                _ImdbTrailerPage = WebPage.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", IMDBID, "/videogallery/content_type-Trailer"))
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
                                    _ImdbTrailerPage = WebPage.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", IMDBID, "/videogallery/content_type-Trailer?page=", i))
                                End If

                                Trailers = Regex.Matches(_ImdbTrailerPage, "imdb/(vi[0-9]+)/")
                                Dim linksCollection As String() = From m As Object In Trailers Select CType(m, Match).Value Distinct.ToArray()

                                For Each trailer As String In linksCollection

                                    trailerPage = WebPage.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/video/", trailer))
                                    trailerTitle = Regex.Match(trailerPage, "<title>.*?\((?<TITLE>.*?)\).*?</title>").Groups(1).Value.ToString.Trim
                                    If String.IsNullOrEmpty(trailerTitle) Then
                                        trailerTitle = Regex.Match(trailerPage, "<title>(?<TITLE>.*?)</title>").Groups(1).Value.ToString.Trim
                                        trailerTitle = trailerTitle.Replace("- IMDb", String.Empty).Trim
                                    End If
                                    Qualities = Regex.Matches(trailerPage, String.Concat("http://www.imdb.com/video/", trailer, "imdb/single\?format=([0-9]+)p"))
                                    Dim trailerCollection As String() = From m As Object In Qualities Select CType(m, Match).Value Distinct.ToArray()

                                    For Each qual As String In trailerCollection
                                        Dim QualityPage As String = WebPage.DownloadData(qual)
                                        Dim QualLink As Match = Regex.Match(QualityPage, "videoPlayerObject.*?viconst")
                                        Dim dowloadURL As MatchCollection = Regex.Matches(QualLink.Value, "url"":""(?<LINK>.*?)""")
                                        Dim Resolution As String = Regex.Match(qual, "[0-9]+p").Value
                                        Dim Res As Enums.TrailerQuality

                                        Select Case Resolution
                                            Case "240p"
                                                Res = Enums.TrailerQuality.SQ240p
                                            Case "480p"
                                                Res = Enums.TrailerQuality.HQ480p
                                            Case "720p"
                                                Res = Enums.TrailerQuality.HD720p
                                            Case Else
                                                Res = Enums.TrailerQuality.OTHERS
                                        End Select

                                        trailerUrl = dowloadURL.Item(0).Groups(1).Value
                                        _trailerlist.Add(New Trailers With {.URL = trailerUrl, .Description = trailerTitle, .WebURL = String.Concat("http://", Master.eSettings.MovieIMDBURL, "/video/", trailer), .Resolution = Res})
                                    Next
                                Next
                            Next
                        End If

                    End If
                End If
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try





        Dim BaseURL As String = "http://www.google.ch/search?q=apple+trailer+"
        Dim DownloadURL As String = "http://trailers.apple.com/trailers/"
        Dim prevQual As String = AdvancedSettings.GetSetting("TrailerPrefQual", "1080p")
        Dim urlHD As String = "/includes/extralarge.html"
        Dim urlHQ As String = "/includes/large.html"
        Dim SearchTitle As String
        Dim SearchURL As String

        If Not String.IsNullOrEmpty(IMDBID) Then
            SearchTitle = Web.HttpUtility.UrlEncode("")
            SearchURL = String.Concat(BaseURL, SearchTitle)
        Else
            SearchURL = String.Empty
        End If

        Try
            If Not String.IsNullOrEmpty(SearchURL) Then
                Dim tDownloadURL As String = String.Empty
                Dim tDescription As New List(Of String)

                'get the first search result from Google
                Dim sHTTP As New HTTP
                Dim Html As String = sHTTP.DownloadData(SearchURL)
                sHTTP = Nothing

                Dim rPattern As String = "<a href=""/url\?q=(?<RESULTS>.*?)\/&amp;" 'Google search results
                Dim tPattern As String = "<a href=""(?<URL>.*?)"">(?<TITLE>.*?)</a>"
                Dim lPattern As String = "<li><a href=""includes/(?<URL>.*?)#"
                Dim uPattern As String = "<a class=""movieLink"" href=""(?<URL>.*?)\?"
                Dim zPattern As String = "<li.*?<a href=""(?<LINK>.*?)#.*?<h3 title="".*?>(?<TITLE>.*?)</h3>.*?duration"">(?<DURATION>.*?)</span>*.?</li>"

                Dim rResult As MatchCollection = Regex.Matches(Html, rPattern, RegexOptions.Singleline)

                If rResult.Count > 0 Then
                    Dim TrailerBaseURL As String = rResult.Item(0).Groups(1).Value
                    Dim TrailerSiteURL = String.Concat(TrailerBaseURL, urlHD)

                    If Not String.IsNullOrEmpty(TrailerBaseURL) Then
                        sHTTP = New HTTP
                        Dim sHtml As String = sHTTP.DownloadData(TrailerSiteURL)
                        sHTTP = Nothing

                        Dim zResult As MatchCollection = Regex.Matches(sHtml, zPattern, RegexOptions.Singleline)

                        For ctr As Integer = 0 To zResult.Count - 1
                            _trailerlist.Add(New Trailers With {.URL = zResult.Item(ctr).Groups(1).Value, .Description = zResult.Item(ctr).Groups(2).Value, .Lenght = zResult.Item(ctr).Groups(3).Value})
                        Next

                        For Each trailer In _trailerlist
                            Dim DLURL As String = String.Empty
                            Dim TrailerSiteLink As String = String.Concat(TrailerBaseURL, "/", trailer.URL)

                            sHTTP = New HTTP
                            Dim zHtml As String = sHTTP.DownloadData(TrailerSiteLink)
                            sHTTP = Nothing

                            Dim yResult As MatchCollection = Regex.Matches(zHtml, uPattern, RegexOptions.Singleline)

                            If yResult.Count > 0 Then
                                tDownloadURL = Web.HttpUtility.HtmlDecode(yResult.Item(0).Groups(1).Value)
                                tDownloadURL = tDownloadURL.Replace("720p", prevQual)
                                trailer.WebURL = tDownloadURL
                                tDownloadURL = tDownloadURL.Replace("1080p", "h1080p")
                                tDownloadURL = tDownloadURL.Replace("720p", "h720p")
                                tDownloadURL = tDownloadURL.Replace("480p", "h480p")
                                trailer.URL = tDownloadURL
                                Select Case prevQual
                                    Case "1080p"
                                        trailer.Resolution = Enums.TrailerQuality.HD1080p
                                    Case "720p"
                                        trailer.Resolution = Enums.TrailerQuality.HD720p
                                    Case "480p"
                                        trailer.Resolution = Enums.TrailerQuality.HQ480p
                                End Select
                            End If
                        Next
                    End If
                End If
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

    End Sub

#End Region 'Methods

End Class
