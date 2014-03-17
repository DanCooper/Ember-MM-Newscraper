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
Imports System.Runtime.CompilerServices

'The InternalsVisibleTo is required for unit testing the friend methods
<Assembly: InternalsVisibleTo("EmberAPI_Test")> 

Namespace YouTube

    Public Class Scraper

#Region "Fields"

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

        ''' <summary>
        ''' Fetches the list of valid video links for the given URL
        ''' </summary>
        ''' <param name="url"><c>String</c> representation of the URL to query</param>
        ''' <remarks>If the <paramref name="url">URL</paramref> leads to a YouTube video page, this method will parse
        ''' the page to extract the various video stream links, and store them in the internal <c>VideoLinks</c> collection.
        ''' Note that only one link of each <c>Enums.TrailerQuality</c> will be kept.</remarks>
        Public Sub GetVideoLinks(ByVal url As String)
            Try
                _VideoLinks = ParseYTFormats(url, False)

            Catch ex As Exception
                Master.eLog.Error(GetType(Scraper), ex.Message, ex.StackTrace, "Error")
            End Try
        End Sub
        ''' <summary>
        ''' Extract and return the title of the video from the supplied HTML.
        ''' </summary>
        ''' <param name="HTML">The text to parse for the title</param>
        ''' <returns><c>String</c> representing the title of the video, as extracted from the YouTube page.</returns>
        ''' <remarks>This method looks in the page's metadata, looking for the meta name="title" tag, and 
        ''' fetching the content attribute.</remarks>
        Private Function GetVideoTitle(ByVal HTML As String) As String
            Dim result As String = ""
            'Dim KeyPattern As String = "'VIDEO_TITLE':\s*'([^']*?)'"
            Dim KeyPattern As String = "meta name=\""title\"" content=\s*\""([^']*?)\"""
            'meta name="title" content=
            If Regex.IsMatch(HTML, KeyPattern) Then
                result = Regex.Match(HTML, KeyPattern).Groups(1).Value
            End If

            Return result
        End Function
        ''' <summary>
        ''' Fetches the list of valid video links for the given URL
        ''' </summary>
        ''' <param name="url"><c>String</c> representation of the URL to query</param>
        ''' <param name="doProgress"><c>Boolean</c> representing whether an event should be raised. Note that this functionality
        ''' has not yet been implemented.</param>
        ''' <remarks>Note that most callers should use <c>GetVideoLinks(ByVal url As String)</c> instead of this <c>Private</c> function.
        ''' If the <paramref name="url">URL</paramref> leads to a YouTube video page, this method will parse
        ''' the page to extract the various video stream links, and store them in the internal <c>VideoLinks</c> collection.
        ''' Note that only one link of each <c>Enums.TrailerQuality</c> will be kept.
        ''' </remarks>
        Private Function ParseYTFormats(ByVal url As String, ByVal doProgress As Boolean) As VideoLinkItemCollection
            Dim DownloadLinks As New VideoLinkItemCollection
            Dim sHTTP As New HTTP

            Try
                Dim Html As String = sHTTP.DownloadData(url)

                If Html.ToLower.Contains("page not found") Then
                    Html = String.Empty
                End If

                If String.IsNullOrEmpty(Html.Trim) Then Return DownloadLinks

                Dim VideoTitle As String = GetVideoTitle(Html)
                VideoTitle = Regex.Replace(VideoTitle, "['?\\:*<>]*", "")

                Dim fmtMatch As Match = Regex.Match(Html, "url_encoded_fmt_stream_map\"": \""(.*?)\"", \""", RegexOptions.IgnoreCase)
                If fmtMatch.Success Then
                    Dim FormatMap As String = fmtMatch.Groups(1).Value
                    Dim decoded As String = Web.HttpUtility.UrlDecode(FormatMap)
                    Dim FormatArray() As String = Split(decoded.Replace(", ", ";"), ",")

                    Dim rurl As New Regex("url=([^\\]+)", RegexOptions.IgnoreCase)
                    Dim rsig As New Regex("sig=([^\\]+)", RegexOptions.IgnoreCase)
                    Dim ritag As New Regex("itag=(\d+)", RegexOptions.IgnoreCase)

                    For i As Integer = 0 To FormatArray.Length - 1
                        Dim yturl As String = rurl.Match(FormatArray(i)).Groups(1).Value
                        Dim ytitag As String = ritag.Match(FormatArray(i)).Groups(1).Value
                        Dim ytsig As String = rsig.Match(FormatArray(i)).Groups(1).Value

                        'Console.WriteLine("Trailer Itag: {0}", ytitag)
                        'Console.WriteLine("Trailer Url: {0}", yturl)
                        'Console.WriteLine("Trailer Sig: {0}", ytsig)

                        Dim Link As New VideoLinkItem
                        Select Case ytitag
                            ' **********************************************************************
                            ' see all itags http://en.wikipedia.org/wiki/YouTube#Quality_and_codecs
                            ' **********************************************************************
                            Case "5"
                                Link.Description = "240p (FLV, H.263)"
                                Link.FormatCodec = Enums.TrailerCodec.FLV
                                Link.FormatQuality = Enums.TrailerQuality.SQ240p
                            Case "6"
                                Link.Description = "270p (FLV, H.263)"
                                Link.FormatCodec = Enums.TrailerCodec.FLV
                                Link.FormatQuality = Enums.TrailerQuality.OTHERS
                            Case "13"
                                Link.Description = "144p (3GP, MPEG-4)"
                                Link.FormatCodec = Enums.TrailerCodec.v3GP
                                Link.FormatQuality = Enums.TrailerQuality.SQ144p
                            Case "17"
                                Link.Description = "144p (3GP, MPEG-4)"
                                Link.FormatCodec = Enums.TrailerCodec.v3GP
                                Link.FormatQuality = Enums.TrailerQuality.SQ144p
                            Case "18"
                                Link.Description = "360p (MP4, H.264)"
                                Link.FormatCodec = Enums.TrailerCodec.MP4
                                Link.FormatQuality = Enums.TrailerQuality.SQ360p
                            Case "22"
                                Link.Description = "720p (MP4, H.264)"
                                Link.FormatCodec = Enums.TrailerCodec.MP4
                                Link.FormatQuality = Enums.TrailerQuality.HD720p
                            Case "34"
                                Link.Description = "360p (FLV, H.264)"
                                Link.FormatCodec = Enums.TrailerCodec.FLV
                                Link.FormatQuality = Enums.TrailerQuality.SQ360p
                            Case "35"
                                Link.Description = "480p (FLV, H.264)"
                                Link.FormatCodec = Enums.TrailerCodec.FLV
                                Link.FormatQuality = Enums.TrailerQuality.HQ480p
                            Case "36"
                                Link.Description = "240p (3GP, MPEG-4)"
                                Link.FormatCodec = Enums.TrailerCodec.v3GP
                                Link.FormatQuality = Enums.TrailerQuality.SQ240p
                            Case "37"
                                Link.Description = "1080p (MP4, H.264)"
                                Link.FormatCodec = Enums.TrailerCodec.MP4
                                Link.FormatQuality = Enums.TrailerQuality.HD1080p
                            Case "38"
                                Link.Description = "1536p (MP4, H.264)"
                                Link.FormatCodec = Enums.TrailerCodec.MP4
                                Link.FormatQuality = Enums.TrailerQuality.OTHERS
                            Case "43"
                                Link.Description = "360p (WebM, VP8)"
                                Link.FormatCodec = Enums.TrailerCodec.WebM
                                Link.FormatQuality = Enums.TrailerQuality.SQ360p
                            Case "44"
                                Link.Description = "480p (WebM, VP8)"
                                Link.FormatCodec = Enums.TrailerCodec.WebM
                                Link.FormatQuality = Enums.TrailerQuality.HQ480p
                            Case "45"
                                Link.Description = "720p (WebM, VP8)"
                                Link.FormatCodec = Enums.TrailerCodec.WebM
                                Link.FormatQuality = Enums.TrailerQuality.HD720p
                            Case "46"
                                Link.Description = "1080p (WebM, VP8)"
                                Link.FormatCodec = Enums.TrailerCodec.WebM
                                Link.FormatQuality = Enums.TrailerQuality.HD1080p
                            Case "82"
                                Link.Description = "3D 360p (MP4, H.264)"
                                Link.FormatCodec = Enums.TrailerCodec.MP4
                                Link.FormatQuality = Enums.TrailerQuality.SQ360p
                            Case "83"
                                Link.Description = "3D 480p (MP4, H.264)"
                                Link.FormatCodec = Enums.TrailerCodec.MP4
                                Link.FormatQuality = Enums.TrailerQuality.HQ480p
                            Case "84"
                                Link.Description = "3D 720p (MP4, H.264)"
                                Link.FormatCodec = Enums.TrailerCodec.MP4
                                Link.FormatQuality = Enums.TrailerQuality.HD720p
                            Case "85"
                                Link.Description = "3D 520p (MP4, H.264)"
                                Link.FormatCodec = Enums.TrailerCodec.MP4
                                Link.FormatQuality = Enums.TrailerQuality.OTHERS
                            Case "100"
                                Link.Description = "3D 360p (WebM, VP8)"
                                Link.FormatCodec = Enums.TrailerCodec.WebM
                                Link.FormatQuality = Enums.TrailerQuality.SQ360p
                            Case "101"
                                Link.Description = "3D 480p (WebM, VP8)"
                                Link.FormatCodec = Enums.TrailerCodec.WebM
                                Link.FormatQuality = Enums.TrailerQuality.HQ480p
                            Case "102"
                                Link.Description = "3D 720p (WebM, VP8)"
                                Link.FormatCodec = Enums.TrailerCodec.WebM
                                Link.FormatQuality = Enums.TrailerQuality.HD720p
                            Case "120"
                                Link.Description = "720p LiveStream (FLV, AVC)"
                                Link.FormatCodec = Enums.TrailerCodec.FLV
                                Link.FormatQuality = Enums.TrailerQuality.HD720p
                            Case "133"
                                Link.Description = "240p ? (MP4, ?)"
                                Link.FormatCodec = Enums.TrailerCodec.MP4
                                Link.FormatQuality = Enums.TrailerQuality.SQ240p
                            Case "134"
                                Link.Description = "360p ? (MP4, ?)"
                                Link.FormatCodec = Enums.TrailerCodec.MP4
                                Link.FormatQuality = Enums.TrailerQuality.SQ360p
                            Case "135"
                                Link.Description = "480p ? (MP4, ?)"
                                Link.FormatCodec = Enums.TrailerCodec.MP4
                                Link.FormatQuality = Enums.TrailerQuality.HQ480p
                            Case "136"
                                Link.Description = "720p ? (MP4, ?)"
                                Link.FormatCodec = Enums.TrailerCodec.MP4
                                Link.FormatQuality = Enums.TrailerQuality.HD720p
                            Case "137"
                                Link.Description = "1080p ? (MP4, ?)"
                                Link.FormatCodec = Enums.TrailerCodec.MP4
                                Link.FormatQuality = Enums.TrailerQuality.HD1080p
                            Case Else
                                Link.Description = "Other"
                                Link.FormatCodec = Enums.TrailerCodec.OTHERS
                                Link.FormatQuality = Enums.TrailerQuality.OTHERS
                                'Continue For
                        End Select

                        Link.URL = Web.HttpUtility.UrlDecode(Web.HttpUtility.UrlDecode(yturl)) & "&signature=" & ytsig & "&title=" & Web.HttpUtility.UrlEncode(VideoTitle)
                        Link.URL = Link.URL.Replace("sig=", "signature=") ' sig= returns HTTP 403 //oldstyle, keep it
                        'Console.WriteLine("Trailer Url decoded: {0}", Link.URL)

                        If Not String.IsNullOrEmpty(Link.URL) AndAlso sHTTP.IsValidURL(Link.URL) Then
                            DownloadLinks.Add(Link)
                        End If

                    Next

                End If
                Return DownloadLinks

            Catch ex As Exception
                Master.eLog.Error(GetType(Scraper), ex.Message, ex.StackTrace, "Error")
                Return New VideoLinkItemCollection
            Finally
                sHTTP = Nothing
            End Try
        End Function
        ''' <summary>
        ''' Search for the given string on YouTube
        ''' </summary>
        ''' <param name="mName"><c>String</c> to search for</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function SearchOnYouTube(ByVal mName As String) As List(Of Trailers)
            Dim tHTTP As New HTTP
            Dim tList As New List(Of Trailers)
            Dim tLink As String = String.Empty
            Dim tName As String = String.Empty

            Dim Html As String = tHTTP.DownloadData("http://www.youtube.com/results?search_query=" & Web.HttpUtility.UrlEncode(mName))
            If Html.ToLower.Contains("page not found") Then
                Html = String.Empty
            End If

            Dim Pattern As String = "<div class=""yt-lockup-content"">.*?title=""(?<NAME>.*?)"".*?href=""(?<LINK>.*?)"""

            Dim Result As MatchCollection = Regex.Matches(Html, Pattern, RegexOptions.Singleline)

            For ctr As Integer = 0 To Result.Count - 1
                tName = Web.HttpUtility.HtmlDecode(Result.Item(ctr).Groups(1).Value)
                tLink = String.Concat("http://www.youtube.com/", Result.Item(ctr).Groups(2).Value)
                tList.Add(New Trailers With {.URL = tLink, .Description = tName})
            Next

            Return tList
        End Function

#End Region 'Methods

    End Class

    Public Class VideoLinkItem

#Region "Fields"

        Private _Description As String
        Private _FormatCodec As Enums.TrailerCodec
        Private _FormatQuality As Enums.TrailerQuality
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

        Friend Property FormatCodec() As Enums.TrailerCodec
            Get
                Return _FormatCodec
            End Get
            Set(ByVal value As Enums.TrailerCodec)
                _FormatCodec = value
            End Set
        End Property

        Friend Property FormatQuality() As Enums.TrailerQuality
            Get
                Return _FormatQuality
            End Get
            Set(ByVal value As Enums.TrailerQuality)
                _FormatQuality = value
            End Set
        End Property

#End Region 'Properties

    End Class

    Public Class VideoLinkItemCollection
        Inherits Generic.SortedList(Of Enums.TrailerQuality, VideoLinkItem)

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
