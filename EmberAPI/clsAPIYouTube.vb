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

Imports NLog
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports System.Web

'The InternalsVisibleTo is required for unit testing the friend methods
<Assembly: InternalsVisibleTo("EmberAPI_Test")>

Namespace YouTube

    Public Class Scraper

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

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
        ''' <param name="strURL"><c>String</c> representation of the URL to query</param>
        ''' <remarks>If the <paramref name="strURL">URL</paramref> leads to a YouTube video page, this method will parse
        ''' the page to extract the various video stream links, and store them in the internal <c>VideoLinks</c> collection.
        ''' </remarks>
        Public Sub GetVideoLinks(ByVal strURL As String)
            Try
                _youtubelinks = ParseYTFormats(strURL)

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Shared Function GetVideoDetails(ByVal strVideoID As String) As String
            If String.IsNullOrEmpty(strVideoID) Then Return String.Empty
            Dim sHTTP As New HTTP
            Dim result As String = sHTTP.DownloadData(String.Concat("https://www.youtube.com/get_video_info?&video_id=", strVideoID))
            Return result
        End Function
        ''' <summary>
        ''' Extract and return the title of the video from the supplied YouTUbe URL.
        ''' </summary>
        ''' <param name="strURL">The text to parse for the title</param>
        Public Shared Function GetVideoTitle(ByRef strURL As String) As String
            If UrlUtils.IsYouTubeURL(strURL) Then
                Dim raw_video_info As String = GetVideoDetails(UrlUtils.GetVideoID(strURL))
                Dim rawAllData As Dictionary(Of String, String) = ToStringTable(raw_video_info).ToDictionary(Function(entry) entry(0), Function(entry) entry(1))
                Try
                    Return HttpUtility.UrlDecode(rawAllData("title"))
                Catch ex As Exception
                    Return HttpUtility.UrlDecode(rawAllData("reason"))
                End Try
            End If
            Return String.Empty
        End Function

        Public Shared Function IsAvailable(ByVal strURL As String) As Boolean
            If UrlUtils.IsYouTubeURL(strURL) Then
                Dim raw_video_info As String = GetVideoDetails(UrlUtils.GetVideoID(strURL))
                Dim rawAllData As Dictionary(Of String, String) = ToStringTable(raw_video_info).ToDictionary(Function(entry) entry(0), Function(entry) entry(1))
                If Not rawAllData.ContainsKey("errorcode") Then
                    Return True
                End If
            End If
            Return False
        End Function

        Private Shared Function ToStringTable(s As String) As IEnumerable(Of String())
            Return s.Split("&"c).[Select](Function(entry) entry.Split("="c))
        End Function

        ''' <summary>
        ''' Fetches the list of valid video links for the given URL
        ''' </summary>
        ''' <param name="strURL"><c>String</c> representation of the URL to query</param>
        ''' <remarks>Note that most callers should use <c>GetVideoLinks(ByVal url As String)</c> instead of this <c>Private</c> function.
        ''' If the <paramref name="url">URL</paramref> leads to a YouTube video page, this method will parse
        ''' the page to extract the various video stream links, and store them in the internal <c>VideoLinks</c> collection.
        ''' Note that only one link of each <c>Enums.TrailerQuality</c> will be kept.
        ''' </remarks>
        Private Function ParseYTFormats(ByVal strURL As String) As YouTubeLinkItemCollection
            Dim DownloadLinks As New YouTubeLinkItemCollection

            If UrlUtils.IsYouTubeURL(strURL) Then
                Dim rawVideo_Info As String = GetVideoDetails(UrlUtils.GetVideoID(strURL))
                Dim rawStream_Map As String = String.Empty
                Dim rawDash_Map As String = String.Empty

                Dim rawAllData As Dictionary(Of String, String) = ToStringTable(rawVideo_Info).ToDictionary(Function(entry) entry(0), Function(entry) entry(1))

                Try
                    rawStream_Map = HttpUtility.UrlDecode(rawAllData("url_encoded_fmt_stream_map"))
                    rawDash_Map = HttpUtility.UrlDecode(rawAllData("adaptive_fmts"))
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try

                Try
                    Dim StreamList = rawStream_Map.Split(","c).[Select](Function(data) StreamInfo.FromStringTable(ToStringTable(data))).ToArray()
                    Dim DashList = rawDash_Map.Split(","c).[Select](Function(data) StreamInfo.FromStringTable(ToStringTable(data))).ToArray()

                    If StreamList.Count > 0 Then
                        Dim fmtDownloadlinks As YouTubeLinkItemCollection = SelectYTiTag(StreamList)
                        If fmtDownloadlinks IsNot Nothing AndAlso fmtDownloadlinks.AudioLinks.Count > 0 Then
                            DownloadLinks.AudioLinks.AddRange(fmtDownloadlinks.AudioLinks)
                        End If
                        If fmtDownloadlinks IsNot Nothing AndAlso fmtDownloadlinks.VideoLinks.Count > 0 Then
                            DownloadLinks.VideoLinks.AddRange(fmtDownloadlinks.VideoLinks)
                        End If
                    End If

                    If DashList.Count > 0 Then
                        Dim dashDownloadlinks As YouTubeLinkItemCollection = SelectYTiTag(DashList)
                        If dashDownloadlinks IsNot Nothing AndAlso dashDownloadlinks.AudioLinks.Count > 0 Then
                            DownloadLinks.AudioLinks.AddRange(dashDownloadlinks.AudioLinks)
                        End If
                        If dashDownloadlinks IsNot Nothing AndAlso dashDownloadlinks.VideoLinks.Count > 0 Then
                            DownloadLinks.VideoLinks.AddRange(dashDownloadlinks.VideoLinks)
                        End If
                    End If

                    DownloadLinks.AudioLinks.Sort()
                    DownloadLinks.VideoLinks.Sort()

                    DownloadLinks.BestQuality = DownloadLinks.VideoLinks.Item(0).FormatQuality
                    DownloadLinks.Title = HttpUtility.UrlDecode(rawAllData("title"))
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            End If

            Return DownloadLinks
        End Function
        ''' <summary>
        ''' Search for the given string on YouTube
        ''' </summary>
        ''' <param name="mName"><c>String</c> to search for</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function SearchOnYouTube(ByVal mName As String) As List(Of MediaContainers.Trailer)
            Dim tHTTP As New HTTP
            Dim tList As New List(Of MediaContainers.Trailer)
            Dim tLength As String = String.Empty
            Dim tLink As String = String.Empty
            Dim tName As String = String.Empty

            Dim Html As String = tHTTP.DownloadData("http://www.youtube.com/results?search_query=" & Web.HttpUtility.UrlEncode(mName))
            If Html.ToLower.Contains("page not found") Then
                Html = String.Empty
            End If

            Dim Pattern As String = clsAdvancedSettings.GetSetting("SearchOnYouTube", "<\/div><span class=""video-time.*?>(?<TIME>.*?)<.*?<a href=""(?<LINK>.*?)"".*?title=""(?<NAME>.*?)""")

            Try
                Dim Result As MatchCollection = Regex.Matches(Html, Pattern, RegexOptions.Singleline, TimeSpan.FromSeconds(1))

                For ctr As Integer = 0 To Result.Count - 1
                    tLength = Result.Item(ctr).Groups(1).Value
                    tLink = String.Concat("http://www.youtube.com", Result.Item(ctr).Groups(2).Value)
                    tName = HttpUtility.HtmlDecode(Result.Item(ctr).Groups(3).Value)
                    If Not tName = "__title__" AndAlso Not tName = "__channel_name__" Then
                        tList.Add(New MediaContainers.Trailer With {.URLWebsite = tLink, .Title = tName, .Duration = tLength, .Source = "YouTube"})
                    End If
                Next
            Catch ex As TimeoutException
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return tList
        End Function

        Private Function SelectYTiTag(ByVal StreamList() As StreamInfo) As YouTubeLinkItemCollection
            Dim DownloadLinks As New YouTubeLinkItemCollection
            For Each tStream As StreamInfo In StreamList
                Dim aLink As New AudioLinkItem
                Dim vLink As New VideoLinkItem

                Select Case tStream.ITag.ToString
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
                    If vLink.isDash Then
                        vLink.URL = tStream.DownloadUrlDash
                    Else
                        vLink.URL = tStream.DownloadUrl
                    End If

                    If Not String.IsNullOrEmpty(vLink.URL) Then
                        DownloadLinks.VideoLinks.Add(vLink)
                    End If
                ElseIf Not String.IsNullOrEmpty(aLink.Description) Then
                    aLink.URL = tStream.DownloadUrlDash

                    If Not String.IsNullOrEmpty(aLink.URL) Then
                        DownloadLinks.AudioLinks.Add(aLink)
                    End If
                End If
            Next

            Return DownloadLinks
        End Function

#End Region 'Methods

    End Class

    Public Class StreamInfo

#Region "Fields"

        Private m_Bitrate As String
        Private m_Clen As String
        Private m_Fallback_Host As String
        Private m_FPS As String
        Private m_ITag As Integer
        Private m_Index As String
        Private m_Init As String
        Private m_LMT As String
        Private m_ProjectionType As String
        Private m_Quality As String
        Private m_QualityLabel As String
        Private m_Sig As String
        Private m_Size As String
        Private m_Stereo3D As String
        Private m_Type As String = String.Empty
        Private m_URL As String = String.Empty

#End Region 'Fields

#Region "Properties"

        Public Property Bitrate() As String
            Get
                Return m_Bitrate
            End Get
            Set(value As String)
                m_Bitrate = value
            End Set
        End Property

        Public Property Clen() As String
            Get
                Return m_Clen
            End Get
            Set(value As String)
                m_Clen = value
            End Set
        End Property

        Public ReadOnly Property DownloadUrl() As String
            Get
                Return String.Format("{0}&fallback_host={1}&signature={2}", Url, Fallback_Host, Sig)
            End Get
        End Property

        Public ReadOnly Property DownloadUrlDash() As String
            Get
                Return Url
            End Get
        End Property

        Public Property Fallback_Host() As String
            Get
                Return m_Fallback_Host
            End Get
            Set(value As String)
                m_Fallback_Host = value
            End Set
        End Property

        Public Property FPS() As String
            Get
                Return m_FPS
            End Get
            Set(value As String)
                m_FPS = value
            End Set
        End Property

        Public ReadOnly Property FileType() As String
            Get
                Return Type.Split(";"c)(0).Split("/"c)(1).Replace("x-", "")
            End Get
        End Property

        Public Property ITag() As Integer
            Get
                Return m_ITag
            End Get
            Set(value As Integer)
                m_ITag = value
            End Set
        End Property

        Public Property Index() As String
            Get
                Return m_Index
            End Get
            Set(value As String)
                m_Index = value
            End Set
        End Property

        Public Property Init() As String
            Get
                Return m_Init
            End Get
            Set(value As String)
                m_Init = value
            End Set
        End Property

        Public Property LMT() As String
            Get
                Return m_LMT
            End Get
            Set(value As String)
                m_LMT = value
            End Set
        End Property

        Public Property Projection_Type() As String
            Get
                Return m_ProjectionType
            End Get
            Set(value As String)
                m_ProjectionType = value
            End Set
        End Property

        Public Property Quality() As String
            Get
                Return m_Quality
            End Get
            Set(value As String)
                m_Quality = value
            End Set
        End Property

        Public Property Quality_Label() As String
            Get
                Return m_QualityLabel
            End Get
            Set(value As String)
                m_QualityLabel = value
            End Set
        End Property

        Public Property Sig() As String
            Get
                Return m_Sig
            End Get
            Set(value As String)
                m_Sig = value
            End Set
        End Property

        Public Property Size() As String
            Get
                Return m_Size
            End Get
            Set(value As String)
                m_Size = value
            End Set
        End Property

        Public Property Stereo3D() As String
            Get
                Return m_Stereo3D
            End Get
            Set(value As String)
                m_Stereo3D = value
            End Set
        End Property

        Public Property Type() As String
            Get
                Return m_Type
            End Get
            Set(value As String)
                m_Type = HttpUtility.UrlDecode(value)
            End Set
        End Property

        Public Property Url() As String
            Get
                Return m_URL
            End Get
            Set(value As String)
                m_URL = HttpUtility.UrlDecode(value)
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Shared Function FromStringTable(table As IEnumerable(Of String())) As StreamInfo
            Dim info As New StreamInfo()
            Dim infoType As Type = GetType(StreamInfo)

            Dim properties = infoType.GetProperties().ToDictionary(Function(item) item.Name.ToLower())

            For Each entry In table
                properties(entry(0).ToLower()).SetValue(info, Convert.ChangeType(entry(1), properties(entry(0).ToLower()).PropertyType))
            Next

            Return info
        End Function

#End Region 'Methods

    End Class

    Public Class UrlUtils

#Region "Fields"

        Private Shared ReadOnly YouTubeURLRegex As Regex = New Regex("youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase)

#End Region 'Fields

#Region "Methods"

        Public Shared Function GetVideoID(ByVal strURL As String) As String
            Dim ytMatch As Match = YouTubeURLRegex.Match(strURL)
            If ytMatch.Success Then
                Return ytMatch.Groups(4).ToString
            Else
                Return String.Empty
            End If
        End Function

        Public Shared Function IsYouTubeURL(ByVal strURL As String) As Boolean
            Dim result As Match = YouTubeURLRegex.Match(strURL)
            Return result.Success
        End Function

#End Region 'Methods

    End Class

    Public Class AudioLinkItem
        Implements IComparable(Of AudioLinkItem)

#Region "Fields"

        Private _description As String
        Private _formatcodec As New Enums.TrailerAudioCodec
        Private _formatquality As New Enums.TrailerAudioQuality
        Private _url As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
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

        Public Property URL() As String
            Get
                Return _url
            End Get
            Set(ByVal value As String)
                _url = value
            End Set
        End Property

        Public Property FormatCodec() As Enums.TrailerAudioCodec
            Get
                Return _formatcodec
            End Get
            Set(ByVal value As Enums.TrailerAudioCodec)
                _formatcodec = value
            End Set
        End Property

        Public Property FormatQuality() As Enums.TrailerAudioQuality
            Get
                Return _formatquality
            End Get
            Set(ByVal value As Enums.TrailerAudioQuality)
                _formatquality = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _description = String.Empty
            _formatcodec = Enums.TrailerAudioCodec.UNKNOWN
            _formatquality = Enums.TrailerAudioQuality.UNKNOWN
            _url = String.Empty
        End Sub

        Public Function CompareTo(ByVal other As AudioLinkItem) As Integer Implements IComparable(Of AudioLinkItem).CompareTo
            Return (FormatQuality).CompareTo(other.FormatQuality)
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
            Clear()
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
            _description = String.Empty
            _formatcodec = Enums.TrailerVideoCodec.UNKNOWN
            _formatquality = Enums.TrailerVideoQuality.UNKNOWN
            _isdash = False
            _url = String.Empty
        End Sub

        Public Function CompareTo(ByVal other As VideoLinkItem) As Integer Implements IComparable(Of VideoLinkItem).CompareTo
            Return (FormatQuality).CompareTo(other.FormatQuality)
        End Function

#End Region 'Methods

    End Class

    Public Class YouTubeLinkItemCollection

#Region "Fields"

        Private _audiolinks As New List(Of AudioLinkItem)
        Private _bestqualitiy As Enums.TrailerVideoQuality
        Private _title As String
        Private _videolinks As New List(Of VideoLinkItem)

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
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

        Public Property BestQuality() As Enums.TrailerVideoQuality
            Get
                Return _bestqualitiy
            End Get
            Set(ByVal value As Enums.TrailerVideoQuality)
                _bestqualitiy = value
            End Set
        End Property

        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
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
            _audiolinks = New List(Of AudioLinkItem)
            _bestqualitiy = Enums.TrailerVideoQuality.Any
            _title = String.Empty
            _videolinks = New List(Of VideoLinkItem)
        End Sub

#End Region 'Methods

    End Class

End Namespace
