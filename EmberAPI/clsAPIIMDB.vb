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
Imports System.Runtime.CompilerServices
Imports NLog

'The InternalsVisibleTo is required for unit testing the friend methods
<Assembly: InternalsVisibleTo("EmberAPI_Test")>

Namespace IMDb

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Private _VideoLinks As VideoLinkItemCollection

#End Region 'Fields

#Region "Events"

        Public Event Exception(ByVal ex As Exception)

        Public Event VideoLinksRetrieved(ByVal bSuccess As Boolean)

#End Region 'Events

#Region "Properties"

        Public ReadOnly Property VideoLinks() As VideoLinkItemCollection
            Get
                If _VideoLinks Is Nothing Then
                    _VideoLinks = New VideoLinkItemCollection
                End If
                Return _VideoLinks
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Shared Function GetMovieTrailersByIMDBID(ByVal strIMDBID As String) As List(Of MediaContainers.Trailer)
            Dim TrailerList As New List(Of MediaContainers.Trailer)

            Dim BaseURL As String = "http://www.imdb.com"
            Dim SearchURL As String

            Dim ImdbTrailerPage As String = String.Empty
            Dim Trailers As MatchCollection
            Dim TrailerDetails As Match
            Dim TrailerDetailsPage As String
            Dim TrailerDuration As String
            Dim TrailerTitle As String
            Dim sHTTP As New HTTP

            Try
                If Not String.IsNullOrEmpty(strIMDBID) Then
                    Dim aPattern As String = "<div class=""article"">.*?<div class=""article"" id=""see_also"">"                                'Video Gallery results
                    Dim tPattern As String = "imdb\/(vi[0-9]+)"                                                                                 'Specific trailer website
                    Dim dPattern As String = "class=""vp-video-name"">(?<TITLE>.*?)<.*?class=""duration title-hover"">\((?<DURATION>.*?)\)"     'Trailer title and duration

                    SearchURL = String.Concat(BaseURL, "/title/", strIMDBID, "/videogallery/content_type-trailer") 'IMDb trailer website of a specific movie, filtered by trailers only

                    'download trailer website
                    ImdbTrailerPage = sHTTP.DownloadData(SearchURL)
                    sHTTP = Nothing

                    If ImdbTrailerPage.ToLower.Contains("page not found") Then
                        ImdbTrailerPage = String.Empty
                    End If

                    If Not String.IsNullOrEmpty(ImdbTrailerPage) Then
                        'filter HTML to Video Gallery only
                        Dim VideoGallery = Regex.Match(ImdbTrailerPage, aPattern, RegexOptions.IgnoreCase Or RegexOptions.Singleline)

                        If VideoGallery.Success Then
                            'search all trailer on trailer website
                            Dim test = VideoGallery.Groups.Item(0).ToString
                            Trailers = Regex.Matches(VideoGallery.Groups.Item(0).ToString, tPattern)
                            Dim linksCollection As String() = From m As Object In Trailers Select CType(m, Match).Value Distinct.ToArray()

                            For Each trailer As String In linksCollection
                                'go to specific trailer website
                                Dim URLWebsite As String = String.Concat(BaseURL, "/video/", trailer)
                                sHTTP = New HTTP
                                TrailerDetailsPage = sHTTP.DownloadData(String.Concat(URLWebsite, "/imdb/single"))
                                sHTTP = Nothing
                                TrailerDetails = Regex.Match(TrailerDetailsPage, dPattern, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                                TrailerTitle = TrailerDetails.Groups("TITLE").Value.ToString.Trim
                                TrailerDuration = TrailerDetails.Groups("DURATION").Value.ToString.Trim
                                TrailerList.Add(New MediaContainers.Trailer With {.Title = TrailerTitle, .URLWebsite = URLWebsite, .Duration = TrailerDuration, .Scraper = "IMDB", .Source = "IMDB"})
                            Next
                        End If
                    End If
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Return TrailerList
        End Function

        ''' <summary>
        ''' Fetches the list of valid video links for the given URL
        ''' </summary>
        ''' <param name="url"><c>String</c> representation of the URL to query, like "http://www.imdb.com/video/imdb/vi3138759961/"</param>
        ''' <remarks>If the <paramref name="url">URL</paramref> leads to a IMDb video page, this method will parse
        ''' the page to extract the various video stream links, and store them in the internal <c>VideoLinks</c> collection.
        ''' Note that only one link of each <c>Enums.TrailerQuality</c> will be kept.</remarks>
        Public Sub GetVideoLinks(ByVal url As String)
            Try
                _VideoLinks = ParseIMDbFormats(url, False)

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub
        ''' <summary>
        ''' Extract and return the title of the video from the supplied HTML.
        ''' </summary>
        ''' <param name="HTML">The text to parse for the title</param>
        ''' <returns><c>String</c> representing the title of the video, as extracted from the IMDb page.</returns>
        ''' <remarks>This method looks in the page's metadata, looking for the meta title tag, and 
        ''' fetching the content attribute.</remarks>
        Public Function GetVideoTitle(ByVal HTML As String) As String
            Dim result As String = ""
            Dim KeyPattern As String = "meta name=\""title\"" content=\s*\""([^']*?)\"""
            Dim nPattern As String = "<title>.*?\((?<TITLE>.*?)\).*?</title>"   'Trailer title inside brackets
            Dim mPattern As String = "<title>(?<TITLE>.*?)</title>"             'Trailer title without brackets
            If Regex.IsMatch(HTML, KeyPattern) Then
                result = Regex.Match(HTML, KeyPattern).Groups(1).Value
            End If

            Return result
        End Function
        ''' <summary>
        ''' Fetches the list of valid video links for the given URL
        ''' </summary>
        ''' <param name="url"><c>String</c> representation of the URL to query, like "http://www.imdb.com/video/imdb/vi3138759961/"</param>
        ''' <param name="doProgress"><c>Boolean</c> representing whether an event should be raised. Note that this functionality
        ''' has not yet been implemented.</param>
        ''' <remarks>Note that most callers should use <c>GetVideoLinks(ByVal url As String)</c> instead of this <c>Private</c> function.
        ''' If the <paramref name="url">URL</paramref> leads to a YouTube video page, this method will parse
        ''' the page to extract the various video stream links, and store them in the internal <c>VideoLinks</c> collection.
        ''' Note that only one link of each <c>Enums.TrailerQuality</c> will be kept.
        ''' </remarks>
        Private Function ParseIMDbFormats(ByVal url As String, ByVal doProgress As Boolean) As VideoLinkItemCollection
            Dim DownloadLinks As New VideoLinkItemCollection
            Dim sHTTP As New HTTP
            'Dim trailerTitle As String
            Dim Qualities As MatchCollection

            Dim cPattern As String = "<div class=""vp-selected"">(?<CURRENT>.*?)<\/div>"                        'Current Trailer quality
            Dim qPattern As String = "(http:\/\/www.imdb.com\/video\/imdb\/{0}\/imdb\/single\?format=.*?)&"     'other Trailer qualities
            Dim vPattern As String = "ffname"":""(?<QUAL>.*?)"".*?,""height.*?videoUrl"":""(?<LINK>.*?)"""

            Try
                'get trailer ID
                Dim vID As String = Regex.Match(url, "vi[0-9]+").Groups(0).Value.ToString.Trim

                If Not String.IsNullOrEmpty(url) Then
                    Dim Html As String = sHTTP.DownloadData(String.Concat(url, "/imdb/single"))

                    If Html.ToLower.Contains("page not found") Then
                        Html = String.Empty
                    End If

                    If String.IsNullOrEmpty(Html.Trim) Then Return DownloadLinks

                    Dim trailerCollection As New List(Of String)

                    'add current quality
                    trailerCollection.Add(String.Concat(url, "/imdb/single?format=", Regex.Match(Html, cPattern).Groups(1).Value.ToString))

                    'get other qualities of a specific trailer
                    Qualities = Regex.Matches(Html, String.Format(qPattern, vID))
                    trailerCollection.AddRange(From m As Object In Qualities Select CType(m, Match).Groups(1).Value Distinct.ToList)


                    'get all download URLs of a specific trailer
                    For Each qual As String In trailerCollection
                        Dim Link As New VideoLinkItem

                        sHTTP = New HTTP
                        Dim QualityPage As String = sHTTP.DownloadData(qual)
                        sHTTP = Nothing
                        Dim QualLink As Match = Regex.Match(QualityPage, "videoPlayerObject.*?viconst")
                        Dim tDetails As MatchCollection = Regex.Matches(QualityPage, vPattern, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                        If tDetails.Count > 0 Then
                            Dim vQuality As String = tDetails.Item(0).Groups("QUAL").Value.ToString.Trim

                            Link.URL = tDetails.Item(0).Groups("LINK").Value.ToString.Trim

                            Select Case vQuality
                                Case "SD"
                                    Link.Description = "240p (MP4)"
                                    Link.FormatCodec = Enums.TrailerVideoCodec.MP4
                                    Link.FormatQuality = Enums.TrailerVideoQuality.SQ240p
                                Case "480p"
                                    Link.Description = "480p (MP4)"
                                    Link.FormatCodec = Enums.TrailerVideoCodec.MP4
                                    Link.FormatQuality = Enums.TrailerVideoQuality.HQ480p
                                Case "720p"
                                    Link.Description = "720p (MP4)"
                                    Link.FormatCodec = Enums.TrailerVideoCodec.MP4
                                    Link.FormatQuality = Enums.TrailerVideoQuality.HD720p
                                Case Else
                                    Link.FormatQuality = Enums.TrailerVideoQuality.UNKNOWN
                            End Select

                            If Not String.IsNullOrEmpty(Link.URL) Then
                                DownloadLinks.Add(Link)
                            End If
                        End If
                    Next
                End If

                Return DownloadLinks

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return New VideoLinkItemCollection
            Finally
                sHTTP = Nothing
            End Try
        End Function

#End Region 'Methods

    End Class

    Public Class VideoLinkItem

#Region "Fields"

        Private _Description As String
        Private _FormatCodec As Enums.TrailerVideoCodec
        Private _FormatQuality As Enums.TrailerVideoQuality
        Private _URL As String

#End Region 'Fields

#Region "Properties"

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Public Property URL() As String
            Get
                Return _URL
            End Get
            Set(ByVal value As String)
                _URL = value
            End Set
        End Property

        Friend Property FormatCodec() As Enums.TrailerVideoCodec
            Get
                Return _FormatCodec
            End Get
            Set(ByVal value As Enums.TrailerVideoCodec)
                _FormatCodec = value
            End Set
        End Property

        Friend Property FormatQuality() As Enums.TrailerVideoQuality
            Get
                Return _FormatQuality
            End Get
            Set(ByVal value As Enums.TrailerVideoQuality)
                _FormatQuality = value
            End Set
        End Property

#End Region 'Properties

    End Class

    Public Class VideoLinkItemCollection
        Inherits Generic.SortedList(Of Enums.TrailerVideoQuality, VideoLinkItem)

#Region "Methods"
        ''' <summary>
        ''' Adds the provided <c>VideoLinkItem</c> to the collection. 
        ''' </summary>
        ''' <param name="Link">The <c>VideoLinkItem</c> to be added. Nothing values or existing values will be ignored</param>
        ''' <remarks>NOTE that the collection is arranged to allow only one
        ''' of VideoLink of each <c>Enums.TrailerQuality</c>. This means that attempting
        ''' to add a second <c>VideoLinkItem</c> with with an identical <c>Enums.TrailerQuality</c>
        ''' will silently fail, while leaving the original untouched.
        ''' 
        ''' 2013/11/07 Dekker500 - Added parameter validation
        ''' </remarks>
        Public Shadows Sub Add(ByVal Link As VideoLinkItem)
            If Link IsNot Nothing AndAlso Not MyBase.ContainsKey(Link.FormatQuality) Then
                MyBase.Add(Link.FormatQuality, Link)
            End If

        End Sub

#End Region 'Methods

    End Class

End Namespace
