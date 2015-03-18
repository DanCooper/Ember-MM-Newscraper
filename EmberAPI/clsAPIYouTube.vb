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
Imports NLog
Imports System.Web

'The InternalsVisibleTo is required for unit testing the friend methods
<Assembly: InternalsVisibleTo("EmberAPI_Test")> 

Namespace YouTube

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Private _youtubelinks As YouTubeLinkItemCollection

#End Region 'Fields

#Region "Events"

        Public Event Exception(ByVal ex As Exception)

        Public Event VideoLinksRetrieved(ByVal bSuccess As Boolean)

#End Region 'Events

#Region "Properties"

        Public ReadOnly Property YouTubeLinks() As YouTubeLinkItemCollection
            Get
                If _youtubelinks Is Nothing Then
                    _youtubelinks = New YouTubeLinkItemCollection
                End If
                Return _youtubelinks
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
                _youtubelinks = ParseYTFormats(url, False)

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
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
                result = HttpUtility.HtmlDecode(result)
            End If

            Return result
        End Function

        Private Function SelectYTiTag(ByVal YouTubeMatch As Match, ByVal VideoTitle As String) As YouTubeLinkItemCollection
            Dim DownloadLinks As New YouTubeLinkItemCollection
            Dim sHTTP As New HTTP

            Dim FormatMap As String = YouTubeMatch.Groups(1).Value
            Dim decoded As String = Web.HttpUtility.UrlDecode(FormatMap)
            Dim FormatArray() As String = decoded.Replace(", ", ";").Split(","c)

            Dim rurl As New Regex("url=([^\\]+)", RegexOptions.IgnoreCase)
            Dim ritag As New Regex("itag=(\d+)", RegexOptions.IgnoreCase)
            Dim rsig As New Regex("sig=([^\\]+)", RegexOptions.IgnoreCase)

            For i As Integer = 0 To FormatArray.Length - 1
                Dim yturl As String = rurl.Match(FormatArray(i)).Groups(1).Value
                Dim ytitag As String = ritag.Match(FormatArray(i)).Groups(1).Value
                Dim ytsig As String = rsig.Match(FormatArray(i)).Groups(1).Value

                'Console.WriteLine("Trailer Itag: {0}", ytitag)
                'Console.WriteLine("Trailer Url: {0}", yturl)
                'Console.WriteLine("Trailer Sig: {0}", ytsig)

                Dim aLink As New AudioLinkItem
                Dim vLink As New VideoLinkItem

                Select Case ytitag
                    ' **********************************************************************
                    ' see all itags http://en.wikipedia.org/wiki/YouTube#Quality_and_codecs
                    ' **********************************************************************
                    Case "5"
                        vLink.Description = "240p (FLV, H.263)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.FLV
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ240p
                        vLink.isDash = False
                    Case "6" 'Discontinued
                        vLink.Description = "270p (FLV, H.263)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.FLV
                        vLink.FormatQuality = Enums.TrailerVideoQuality.UNKNOWN
                        vLink.isDash = False
                    Case "13" 'Discontinued
                        vLink.Description = "144p (3GP, MPEG-4)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.v3GP
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ144p
                        vLink.isDash = False
                    Case "17"
                        vLink.Description = "144p (3GP, MPEG-4)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.v3GP
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ144p
                        vLink.isDash = False
                    Case "18"
                        vLink.Description = "360p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ360p
                        vLink.isDash = False
                    Case "22"
                        vLink.Description = "720p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD720p
                        vLink.isDash = False
                    Case "34" 'Discontinued
                        vLink.Description = "360p (FLV, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.FLV
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ360p
                        vLink.isDash = False
                    Case "35" 'Discontinued
                        vLink.Description = "480p (FLV, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.FLV
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HQ480p
                        vLink.isDash = False
                    Case "36"
                        vLink.Description = "240p (3GP, MPEG-4)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.v3GP
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ240p
                        vLink.isDash = False
                    Case "37" 'Discontinued
                        vLink.Description = "1080p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD1080p
                        vLink.isDash = False
                    Case "38" 'Discontinued
                        vLink.Description = "1536p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.UNKNOWN
                        vLink.isDash = False
                    Case "43"
                        vLink.Description = "360p (WebM, VP8)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ360p
                        vLink.isDash = False
                    Case "44" 'Discontinued
                        vLink.Description = "480p (WebM, VP8)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HQ480p
                        vLink.isDash = False
                    Case "45" 'Discontinued
                        vLink.Description = "720p (WebM, VP8)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD720p
                        vLink.isDash = False
                    Case "46" 'Discontinued
                        vLink.Description = "1080p (WebM, VP8)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD1080p
                        vLink.isDash = False
                    Case "82"
                        vLink.Description = "3D 360p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ360p
                        vLink.isDash = False
                    Case "83"
                        vLink.Description = "3D 480p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HQ480p
                        vLink.isDash = False
                    Case "84"
                        vLink.Description = "3D 720p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD720p
                        vLink.isDash = False
                    Case "85"
                        vLink.Description = "3D 520p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.UNKNOWN
                        vLink.isDash = False
                    Case "100"
                        vLink.Description = "3D 360p (WebM, VP8)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ360p
                        vLink.isDash = False
                    Case "101" 'Discontinued
                        vLink.Description = "3D 480p (WebM, VP8)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HQ480p
                        vLink.isDash = False
                    Case "102" 'Discontinued
                        vLink.Description = "3D 720p (WebM, VP8)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD720p
                        vLink.isDash = False
                    Case "133" 'DASH (video only)
                        vLink.Description = "240p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ240p
                        vLink.isDash = True
                    Case "134" 'DASH (video only)
                        vLink.Description = "360p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ360p
                        vLink.isDash = True
                    Case "135" 'DASH (video only)
                        vLink.Description = "480p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HQ480p
                        vLink.isDash = True
                    Case "136" 'DASH (video only)
                        vLink.Description = "720p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD720p
                        vLink.isDash = True
                    Case "137" 'DASH (video only)
                        vLink.Description = "1080p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD1080p
                        vLink.isDash = True
                    Case "139" 'DASH (audio only)
                        aLink.Description = "48 kbit/s (MP4, AAC)"
                        aLink.FormatCodec = Enums.TrailerAudioCodec.MP4
                        aLink.FormatQuality = Enums.TrailerAudioQuality.AAC48kbps
                    Case "140" 'DASH (audio only)
                        aLink.Description = "128 kbit/s (MP4, AAC)"
                        aLink.FormatCodec = Enums.TrailerAudioCodec.MP4
                        aLink.FormatQuality = Enums.TrailerAudioQuality.AAC128kbps
                    Case "141" 'DASH (audio only)
                        aLink.Description = "256 kbit/s (MP4, AAC)"
                        aLink.FormatCodec = Enums.TrailerAudioCodec.MP4
                        aLink.FormatQuality = Enums.TrailerAudioQuality.AAC256kbps
                    Case "160" 'DASH (video only, 15 FPS)
                        vLink.Description = "144p (MP4, H.264, 15 FPS)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ144p15fps
                        vLink.isDash = True
                    Case "171" 'DASH (audio only)
                        aLink.Description = "128 kbit/s (WebM, Vorbis)"
                        aLink.FormatCodec = Enums.TrailerAudioCodec.WebM
                        aLink.FormatQuality = Enums.TrailerAudioQuality.Vorbis128kbps
                    Case "172" 'DASH (audio only)
                        aLink.Description = "192 kbit/s (WebM, Vorbis)"
                        aLink.FormatCodec = Enums.TrailerAudioCodec.WebM
                        aLink.FormatQuality = Enums.TrailerAudioQuality.Vorbis192kbps
                    Case "242" 'DASH (video only)
                        vLink.Description = "240p (WebM, VP9)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ240p
                        vLink.isDash = True
                    Case "243" 'DASH (video only)
                        vLink.Description = "360p (WebM, VP9)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ360p
                        vLink.isDash = True
                    Case "244" 'DASH (video only)
                        vLink.Description = "480p (WebM, VP9)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HQ480p
                        vLink.isDash = True
                    Case "247" 'DASH (video only)
                        vLink.Description = "720p (WebM, VP9)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD720p
                        vLink.isDash = True
                    Case "248" 'DASH (video only)
                        vLink.Description = "1080p (WebM, VP9)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD1080p
                        vLink.isDash = True
                    Case "264" 'DASH (video only)
                        vLink.Description = "1140p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD1440p
                        vLink.isDash = True
                    Case "266" 'DASH (video only)
                        vLink.Description = "2160p (MP4, H.264)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD2160p
                        vLink.isDash = True
                    Case "271" 'DASH (video only)
                        vLink.Description = "1140p (WebM, VP9)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD1440p
                        vLink.isDash = True
                    Case "272" 'DASH (video only)
                        vLink.Description = "2160p (WebM, VP9)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD2160p
                        vLink.isDash = True
                    Case "278" 'DASH (video only)
                        vLink.Description = "144p (WebM, VP9)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.SQ144p
                        vLink.isDash = True
                    Case "298" 'DASH (video only, 60 FPS)
                        vLink.Description = "720p (MP4, H.264, 60 FPS)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD720p60fps
                        vLink.isDash = True
                    Case "299" 'DASH (video only, 60 FPS)
                        vLink.Description = "1080p (MP4, H.264, 60 FPS)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.MP4
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD1080p60fps
                        vLink.isDash = True
                    Case "302" 'DASH (video only, 60 FPS)
                        vLink.Description = "720p (WebM, VP9, 60 FPS)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD720p60fps
                        vLink.isDash = True
                    Case "303" 'DASH (video only, 60 FPS)
                        vLink.Description = "1080p (WebM, VP9, 60 FPS)"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.WebM
                        vLink.FormatQuality = Enums.TrailerVideoQuality.HD1080p60fps
                        vLink.isDash = True
                    Case Else
                        vLink.Description = "Unknown"
                        vLink.FormatCodec = Enums.TrailerVideoCodec.UNKNOWN
                        vLink.FormatQuality = Enums.TrailerVideoQuality.UNKNOWN
                        vLink.isDash = False
                End Select

                If Not String.IsNullOrEmpty(vLink.Description) Then
                    vLink.URL = Web.HttpUtility.UrlDecode(Web.HttpUtility.UrlDecode(yturl)) & "&signature=" & ytsig & "&title=" & Web.HttpUtility.UrlEncode(VideoTitle)
                    vLink.URL = vLink.URL.Replace("sig=", "signature=")

                    If Not String.IsNullOrEmpty(vLink.URL) AndAlso sHTTP.IsValidURL(vLink.URL) Then
                        DownloadLinks.VideoLinks.Add(vLink)
                    End If
                ElseIf Not String.IsNullOrEmpty(aLink.Description) Then
                    aLink.URL = Web.HttpUtility.UrlDecode(Web.HttpUtility.UrlDecode(yturl)) & "&signature=" & ytsig & "&title=" & Web.HttpUtility.UrlEncode(VideoTitle)
                    aLink.URL = aLink.URL.Replace("sig=", "signature=")

                    If Not String.IsNullOrEmpty(aLink.URL) AndAlso sHTTP.IsValidURL(aLink.URL) Then
                        DownloadLinks.AudioLinks.Add(aLink)
                    End If
                End If
            Next

            Return DownloadLinks
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
        Private Function ParseYTFormats(ByVal url As String, ByVal doProgress As Boolean) As YouTubeLinkItemCollection
            Dim DownloadLinks As New YouTubeLinkItemCollection
            Dim sHTTP As New HTTP

            Try
                Dim Html As String = sHTTP.DownloadData(url)

                If Html.ToLower.Contains("page not found") Then
                    Html = String.Empty
                End If

                Dim test As String = Web.HttpUtility.UrlDecode(Html)

                If String.IsNullOrEmpty(Html.Trim) Then Return DownloadLinks

                Dim VideoTitle As String = GetVideoTitle(Html)
                VideoTitle = Regex.Replace(VideoTitle, "['?\\:*<>]*", "")

                Dim fmtMatch As Match = Regex.Match(Html, "url_encoded_fmt_stream_map\"": \""(.*?)\"", \""", RegexOptions.IgnoreCase)
                Dim dashMatch As Match = Regex.Match(Html, "(adaptive_fmts"":"".*?"")", RegexOptions.IgnoreCase) '"adaptive_fmts\"": \""(.*?)\"", \""", RegexOptions.IgnoreCase)

                If fmtMatch.Success Then
                    Dim fmtDownloadlinks As YouTubeLinkItemCollection = SelectYTiTag(fmtMatch, VideoTitle)
                    If fmtDownloadlinks IsNot Nothing AndAlso fmtDownloadlinks.AudioLinks.Count > 0 Then
                        DownloadLinks.AudioLinks.AddRange(fmtDownloadlinks.AudioLinks)
                    End If
                    If fmtDownloadlinks IsNot Nothing AndAlso fmtDownloadlinks.VideoLinks.Count > 0 Then
                        DownloadLinks.VideoLinks.AddRange(fmtDownloadlinks.VideoLinks)
                    End If
                End If

                If dashMatch.Success Then
                    Dim dashDownloadlinks As YouTubeLinkItemCollection = SelectYTiTag(dashMatch, VideoTitle)
                    If dashDownloadlinks IsNot Nothing AndAlso dashDownloadlinks.AudioLinks.Count > 0 Then
                        DownloadLinks.AudioLinks.AddRange(dashDownloadlinks.AudioLinks)
                    End If
                    If dashDownloadlinks IsNot Nothing AndAlso dashDownloadlinks.VideoLinks.Count > 0 Then
                        DownloadLinks.VideoLinks.AddRange(dashDownloadlinks.VideoLinks)
                    End If
                End If

                DownloadLinks.AudioLinks.Sort()
                DownloadLinks.VideoLinks.Sort()

                Return DownloadLinks

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return New YouTubeLinkItemCollection
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
            Dim tLength As String = String.Empty
            Dim tLink As String = String.Empty
            Dim tName As String = String.Empty

            Dim Html As String = tHTTP.DownloadData("http://www.youtube.com/results?search_query=" & Web.HttpUtility.UrlEncode(mName))
            If Html.ToLower.Contains("page not found") Then
                Html = String.Empty
            End If

            Dim Pattern As String = "<li><div class=""yt-lockup yt-lockup-tile yt-lockup-video.*?video-time.*?>(?<TIME>.*?)<.*?<h3 class=""yt-lockup-title""><a href=""(?<LINK>.*?)"".*?title=""(?<NAME>.*?)"""

            Dim Result As MatchCollection = Regex.Matches(Html, Pattern, RegexOptions.Singleline)

            For ctr As Integer = 0 To Result.Count - 1
                tLength = Result.Item(ctr).Groups(1).Value
                tLink = String.Concat("http://www.youtube.com", Result.Item(ctr).Groups(2).Value)
                tName = Web.HttpUtility.HtmlDecode(Result.Item(ctr).Groups(3).Value)
                If Not tName = "__title__" AndAlso Not tName = "__channel_name__" Then
                    tList.Add(New Trailers With {.VideoURL = tLink, .WebURL = tLink, .Description = tName, .Duration = tLength, .Source = "YouTube"})
                End If
            Next

            Return tList
        End Function

#End Region 'Methods

    End Class

    Public Class AudioLinkItem
        Implements IComparable(Of AudioLinkItem)

#Region "Fields"

        Private _Description As String
        Private _FormatCodec As New Enums.TrailerAudioCodec
        Private _FormatQuality As New Enums.TrailerAudioQuality
        Private _URL As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

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

        Public Property FormatCodec() As Enums.TrailerAudioCodec
            Get
                Return _FormatCodec
            End Get
            Set(ByVal value As Enums.TrailerAudioCodec)
                _FormatCodec = value
            End Set
        End Property

        Public Property FormatQuality() As Enums.TrailerAudioQuality
            Get
                Return _FormatQuality
            End Get
            Set(ByVal value As Enums.TrailerAudioQuality)
                _FormatQuality = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me.Description = String.Empty
            Me._FormatCodec = Enums.TrailerAudioCodec.UNKNOWN
            Me._FormatQuality = Enums.TrailerAudioQuality.UNKNOWN
            Me._URL = String.Empty
        End Sub

        Public Function CompareTo(ByVal other As AudioLinkItem) As Integer Implements IComparable(Of AudioLinkItem).CompareTo
            Return (Me.FormatQuality).CompareTo(other.FormatQuality)
        End Function

#End Region 'Methods

    End Class

    Public Class VideoLinkItem
        Implements IComparable(Of VideoLinkItem)

#Region "Fields"

        Private _description As String
        Private _formatcodec As New Enums.TrailerVideoCodec
        Private _formatquality As New Enums.TrailerVideoQuality
        Private _isdash As Boolean
        Private _url As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        Public Property FormatCodec() As Enums.TrailerVideoCodec
            Get
                Return _formatcodec
            End Get
            Set(ByVal value As Enums.TrailerVideoCodec)
                _formatcodec = value
            End Set
        End Property

        Public Property FormatQuality() As Enums.TrailerVideoQuality
            Get
                Return _formatquality
            End Get
            Set(ByVal value As Enums.TrailerVideoQuality)
                _formatquality = value
            End Set
        End Property

        Public Property isDash() As Boolean
            Get
                Return _isdash
            End Get
            Set(ByVal value As Boolean)
                _isdash = value
            End Set
        End Property

        Public Property URL() As String
            Get
                Return _url
            End Get
            Set(ByVal value As String)
                _url = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me.Description = String.Empty
            Me._formatcodec = Enums.TrailerVideoCodec.UNKNOWN
            Me._formatquality = Enums.TrailerVideoQuality.UNKNOWN
            Me._isdash = False
            Me._url = String.Empty
        End Sub

        Public Function CompareTo(ByVal other As VideoLinkItem) As Integer Implements IComparable(Of VideoLinkItem).CompareTo
            Return (Me.FormatQuality).CompareTo(other.FormatQuality)
        End Function

#End Region 'Methods

    End Class

    Public Class YouTubeLinkItemCollection

#Region "Fields"

        Private _audiolinks As New List(Of AudioLinkItem)
        Private _videolinks As New List(Of VideoLinkItem)

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property AudioLinks() As List(Of AudioLinkItem)
            Get
                Return _audiolinks
            End Get
            Set(ByVal value As List(Of AudioLinkItem))
                _audiolinks = value
            End Set
        End Property

        Public Property VideoLinks() As List(Of VideoLinkItem)
            Get
                Return _videolinks
            End Get
            Set(ByVal value As List(Of VideoLinkItem))
                _videolinks = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._audiolinks = New List(Of AudioLinkItem)
            Me._videolinks = New List(Of VideoLinkItem)
        End Sub

#End Region 'Methods

    End Class

End Namespace
