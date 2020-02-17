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

Imports Newtonsoft.Json
Imports NLog
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports System.Web

'The InternalsVisibleTo is required for unit testing the friend methods
<Assembly: InternalsVisibleTo("EmberAPI_Test")>

Namespace YouTube

    Public Class Scraper

#Region "Fields"

        Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

        Private Shared Function GetStreams(ByVal videoInfo As VideoInfo) As MediaContainers.Trailer.StreamCollection
            If videoInfo Is Nothing Then Return Nothing
            Dim nStreams As New MediaContainers.Trailer.StreamCollection
            Dim nAdaptiveFormats = SetITag(videoInfo.streamingData.adaptiveFormats)
            Dim nFormats = SetITag(videoInfo.streamingData.formats)
            nStreams.AudioStreams.AddRange(nAdaptiveFormats.AudioStreams)
            nStreams.AudioStreams.AddRange(nFormats.AudioStreams)
            nStreams.VideoStreams.AddRange(nAdaptiveFormats.VideoStreams)
            nStreams.VideoStreams.AddRange(nFormats.VideoStreams)
            nStreams.AudioStreams.Sort()
            nStreams.VideoStreams.Sort()
            Return nStreams
        End Function

        Public Shared Function GetVideoDetails(ByVal videoIdOrUrl As String) As MediaContainers.Trailer
            If String.IsNullOrEmpty(videoIdOrUrl) Then Return Nothing
            Dim strVideoId As String = String.Empty
            If UrlUtils.IsYouTubeUrl(videoIdOrUrl) Then
                UrlUtils.GetVideoIDFromURL(videoIdOrUrl, strVideoId)
            Else
                strVideoId = videoIdOrUrl
            End If
            Dim nVideoInfo = GetVideoInfo(strVideoId)
            Dim nStreams As New MediaContainers.Trailer.StreamCollection
            If nVideoInfo IsNot Nothing AndAlso nVideoInfo.playabilityStatus.status = "OK" AndAlso nVideoInfo.streamingData.HasStreams Then
                nStreams = GetStreams(nVideoInfo)
            Else
                'try parse webpage
            End If
            If nStreams IsNot Nothing AndAlso nStreams.VideoStreams.Count > 0 Then
                Return New MediaContainers.Trailer With {
                    .Duration = StringUtils.SecondsToDuration(nVideoInfo.videoDetails.lengthSeconds),
                    .Quality = nStreams.VideoStreams(0).FormatQuality,
                    .Source = "YouTube",
                    .Streams = nStreams,
                    .Title = nVideoInfo.videoDetails.title,
                    .URLWebsite = String.Concat("http://www.youtube.com/watch?v=", nVideoInfo.videoDetails.videoId)
                }
            End If
            Return Nothing
        End Function

        Public Shared Function GetVideoInfo(ByVal videoID As String) As VideoInfo
            If String.IsNullOrEmpty(videoID) Then Return Nothing
            Dim sHTTP As New HTTP
            Dim rawResult As String = HttpUtility.UrlDecode(sHTTP.DownloadData(String.Concat("https://www.youtube.com/get_video_info?&video_id=", videoID)))
            rawResult = Regex.Match(rawResult, "{.*}").Value
            rawResult = Regex.Replace(rawResult, "\u0026", "&")
            Dim videoInfo = JsonConvert.DeserializeObject(Of VideoInfo)(rawResult)
            Return videoInfo
        End Function
        ''' <summary>
        ''' Search for the given string on YouTube
        ''' </summary>
        ''' <param name="searchString"><c>String</c> to search for</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function SearchOnYouTube(ByVal searchString As String) As List(Of MediaContainers.Trailer)
            Dim tHTTP As New HTTP
            Dim tList As New List(Of MediaContainers.Trailer)

            Dim Html As String = tHTTP.DownloadData(String.Concat("http://www.youtube.com/results?search_query=", HttpUtility.UrlEncode(searchString), "&sp=EgIQAQ%253D%253D"))
            If Html.ToLower.Contains("page not found") Then
                Html = String.Empty
            End If

            Dim Pattern As String = AdvancedSettings.GetSetting("SearchOnYouTube", "<\/div><div class=""yt-lockup-content"">.*?<a href=""(?<URL>.*?)"".*?title=""(?<TITLE>.*?)"".*?true"">(?<DURATION>.*?)<")

            Try
                Dim Result As MatchCollection = Regex.Matches(Html, Pattern, RegexOptions.Singleline, TimeSpan.FromSeconds(1))
                For ctr As Integer = 0 To Result.Count - 1
                    Dim strURL = String.Concat("http://www.youtube.com", Result.Item(ctr).Groups("URL").Value)
                    Dim nTrailer = GetVideoDetails(strURL)
                    If nTrailer IsNot Nothing Then tList.Add(nTrailer)
                Next
            Catch ex As TimeoutException
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Return tList
        End Function

        Private Shared Function SetITag(ByVal streamList() As VideoInfo.Format) As MediaContainers.Trailer.StreamCollection
            Dim nStreams As New MediaContainers.Trailer.StreamCollection
            For Each tStream In streamList
                Dim aLink As MediaContainers.Trailer.AudioStream = Nothing
                Dim vLink As MediaContainers.Trailer.VideoStream = Nothing

                Select Case tStream.itag
                    Case 5 'FLV
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H263,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ240p,
                            .IsDash = False
                        }
                    Case 6 'FLV
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H263,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ240p,
                            .IsDash = False
                        }
                    Case 13 'ThreeGP
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H263,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ144p15fps,
                            .IsDash = False
                        }
                    Case 17 'ThreeGP
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ144p,
                            .IsDash = False
                        }
                    Case 18 'MP4
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ360p,
                            .IsDash = False
                        }
                    Case 22 'MP4
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.HD720p,
                            .IsDash = False
                        }
                    Case 34 'FLV
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ360p,
                            .IsDash = False
                        }
                    Case 35 'FLV
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.HQ480p,
                            .IsDash = False
                        }
                    Case 36 'ThreeGP
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ240p,
                            .IsDash = False
                        }
                    Case 37 'MP4
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.HD1080p,
                            .IsDash = False
                        }
                    Case 38 'MP4
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.HD2160p,
                            .IsDash = False
                        }
                    Case 43 'WebM
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP8,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ360p,
                            .IsDash = False
                        }
                    Case 44 'WebM
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP8,
                            .FormatQuality = Enums.TrailerVideoQuality.HQ480p,
                            .IsDash = False
                        }
                    Case 45 'WebM
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP8,
                            .FormatQuality = Enums.TrailerVideoQuality.HD720p,
                            .IsDash = False
                        }
                    Case 46 'WebM
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP8,
                            .FormatQuality = Enums.TrailerVideoQuality.HD1080p,
                            .IsDash = False
                        }
                    Case 82 'MP4
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ360p,
                            .IsDash = False
                        }
                    Case 83 'MP4
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.HQ480p,
                            .IsDash = False
                        }
                    Case 84 'MP4
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.HD720p,
                            .IsDash = False
                        }
                    Case 85 'MP4
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.HD1080p,
                            .IsDash = False
                        }
                    Case 100 'WebM
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP8,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ360p,
                            .IsDash = False
                        }
                    Case 101 'WebM
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP8,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ360p,
                            .IsDash = False
                        }
                    Case 102 'WebM
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP8,
                            .FormatQuality = Enums.TrailerVideoQuality.HD720p,
                            .IsDash = False
                        }
                    Case 133 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ240p,
                            .IsDash = True
                        }
                    Case 134 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ360p,
                            .IsDash = True
                        }
                    Case 135 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.HQ480p,
                            .IsDash = True
                        }
                    Case 136 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.HD720p,
                            .IsDash = True
                        }
                    Case 137 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.HD1080p,
                            .IsDash = True
                        }
                    Case 139 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.AAC,
                            .FormatQuality = Enums.TrailerAudioQuality.Q48kbps
                        }
                    Case 140 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.AAC,
                            .FormatQuality = Enums.TrailerAudioQuality.Q128kbps
                        }
                    Case 141 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.AAC,
                            .FormatQuality = Enums.TrailerAudioQuality.Q256kbps
                        }
                    Case 160 'DASH (video only, 15 fps)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ144p15fps,
                            .IsDash = True
                        }
                    Case 171 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.Vorbis,
                            .FormatQuality = Enums.TrailerAudioQuality.Q128kbps
                        }
                    Case 172 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.Vorbis,
                            .FormatQuality = Enums.TrailerAudioQuality.Q192kbps
                        }
                    Case 242 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP9,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ240p,
                            .IsDash = True
                        }
                    Case 243 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP9,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ360p,
                            .IsDash = True
                        }
                    Case 244 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP9,
                            .FormatQuality = Enums.TrailerVideoQuality.HQ480p,
                            .IsDash = True
                        }
                    Case 245 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP9,
                            .FormatQuality = Enums.TrailerVideoQuality.HQ480p,
                            .IsDash = True
                        }
                    Case 246 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP9,
                            .FormatQuality = Enums.TrailerVideoQuality.HQ480p,
                            .IsDash = True
                        }
                    Case 247 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP9,
                            .FormatQuality = Enums.TrailerVideoQuality.HD720p,
                            .IsDash = True
                        }
                    Case 248 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP9,
                            .FormatQuality = Enums.TrailerVideoQuality.HD1080p,
                            .IsDash = True
                        }
                    Case 249 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.Opus,
                            .FormatQuality = Enums.TrailerAudioQuality.Q48kbps
                        }
                    Case 250 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.Opus,
                            .FormatQuality = Enums.TrailerAudioQuality.Q64kbps
                        }
                    Case 251 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.Opus,
                            .FormatQuality = Enums.TrailerAudioQuality.Q128kbps
                        }
                    Case 256 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.AAC_SPATIAL,
                            .FormatQuality = Enums.TrailerAudioQuality.Q192kbps
                        }
                    Case 258 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.AAC_SPATIAL,
                            .FormatQuality = Enums.TrailerAudioQuality.Q384kbps
                        }
                    Case 264 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.HD1440p,
                            .IsDash = True
                        }
                    Case 266 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.HD2160p,
                            .IsDash = True
                        }
                    Case 271 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP9,
                            .FormatQuality = Enums.TrailerVideoQuality.HD1440p,
                            .IsDash = True
                        }
                    Case 272 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP9,
                            .FormatQuality = Enums.TrailerVideoQuality.HD2160p,
                            .IsDash = True
                        }
                    Case 278 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP9,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ144p,
                            .IsDash = True
                        }
                    Case 298 'DASH (video only, 60 fps)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.HD720p60fps,
                            .IsDash = True
                        }
                    Case 299 'DASH (video only, 60 fps)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.H264,
                            .FormatQuality = Enums.TrailerVideoQuality.HD1080p60fps,
                            .IsDash = True
                        }
                    Case 302 'DASH (video only, 60 fps)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP9,
                            .FormatQuality = Enums.TrailerVideoQuality.HD720p60fps,
                            .IsDash = True
                        }
                    Case 303 'DASH (video only, 60 FPSfps)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP9,
                            .FormatQuality = Enums.TrailerVideoQuality.HD1080p60fps,
                            .IsDash = True
                        }
                    Case 308 'DASH (video only, 60 FPSfps)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP9,
                            .FormatQuality = Enums.TrailerVideoQuality.HD1440p,
                            .IsDash = True
                        }
                    Case 313 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.VP9,
                            .FormatQuality = Enums.TrailerVideoQuality.HD1080p,
                            .IsDash = True
                        }
                    Case 325 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.DTSE_SPATIAL,
                            .FormatQuality = Enums.TrailerAudioQuality.Q384kbps
                        }
                    Case 327 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.AAC_SPATIAL,
                            .FormatQuality = Enums.TrailerAudioQuality.Q256kbps
                        }
                    Case 328 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.EC3_SPATIAL,
                            .FormatQuality = Enums.TrailerAudioQuality.Q384kbps
                        }
                    Case 380 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.AC3_SPATIAL,
                            .FormatQuality = Enums.TrailerAudioQuality.Q384kbps
                        }
                    Case 339 'DASH (audio only)
                        aLink = New MediaContainers.Trailer.AudioStream With {
                            .FormatCodec = Enums.TrailerAudioCodec.Vorbis_SPATIAL,
                            .FormatQuality = Enums.TrailerAudioQuality.Q256kbps
                        }
                    Case 394 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.AV1,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ144p,
                            .IsDash = True
                        }
                    Case 395 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.AV1,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ240p,
                            .IsDash = True
                        }
                    Case 396 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.AV1,
                            .FormatQuality = Enums.TrailerVideoQuality.SQ360p,
                            .IsDash = True
                        }
                    Case 397 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.AV1,
                            .FormatQuality = Enums.TrailerVideoQuality.HQ480p,
                            .IsDash = True
                        }
                    Case 398 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.AV1,
                            .FormatQuality = Enums.TrailerVideoQuality.HD720p,
                            .IsDash = True
                        }
                    Case 399 'DASH (video only)
                        vLink = New MediaContainers.Trailer.VideoStream With {
                            .FormatCodec = Enums.TrailerVideoCodec.AV1,
                            .FormatQuality = Enums.TrailerVideoQuality.HD1080p,
                            .IsDash = True
                        }
                    Case Else
                        _Logger.Warn(String.Format("Unknown YouTube ITAG: {0}", tStream.itag))
                End Select

                If vLink IsNot Nothing AndAlso Not String.IsNullOrEmpty(tStream.url) Then
                    vLink.URL = tStream.url
                    nStreams.VideoStreams.Add(vLink)
                ElseIf aLink IsNot Nothing AndAlso Not String.IsNullOrEmpty(tStream.url) Then
                    aLink.URL = tStream.url
                    nStreams.AudioStreams.Add(aLink)
                End If
            Next

            Return nStreams
        End Function

#End Region 'Methods

#Region "Nested Types"

        Public Class VideoInfo

#Region "Properties"

            Public Property playabilityStatus As New PlayabilityStatusItem
            Public Property streamingData As New StreamingDataItem
            Public Property videoDetails As New VideoDetailsItem

#End Region 'Properties

#Region "Nested Types"

            Public Class PlayabilityStatusItem
                Public Property status As String
                Public Property playableInEmbed As Boolean
                Public Property contextParams As String
            End Class

            Public Class Format
                Public Property itag As Integer
                Public Property url As String
                Public Property mimeType As String
                Public Property bitrate As Integer
                Public Property width As Integer
                Public Property height As Integer
                Public Property lastModified As String
                Public Property contentLength As String
                Public Property quality As String
                Public Property qualityLabel As String
                Public Property projectionType As String
                Public Property averageBitrate As Integer
                Public Property audioQuality As String
                Public Property approxDurationMs As String
                Public Property audioSampleRate As String
                Public Property audioChannels As Integer
            End Class

            Public Class Range
                Public Property start As String
                Public Property [end] As String
            End Class

            Public Class ColorInfo
                Public Property primaries As String
                Public Property transferCharacteristics As String
                Public Property matrixCoefficients As String
            End Class

            Public Class AdaptiveFormat
                Inherits Format
                Public Property initRange As New Range
                Public Property indexRange As New Range
                Public Property fps As Integer
                Public Property colorInfo As New ColorInfo
                Public Property highReplication As Boolean?
            End Class

            Public Class StreamingDataItem
                Public Property expiresInSeconds As String
                Public Property formats As Format()
                Public ReadOnly Property HasStreams As Boolean
                    Get
                        Return formats IsNot Nothing AndAlso formats.Count > 0 OrElse adaptiveFormats IsNot Nothing AndAlso adaptiveFormats.Count > 0
                    End Get
                End Property
                Public Property adaptiveFormats As AdaptiveFormat()
            End Class

            Public Class VideoDetailsItem
                Public Property videoId As String
                Public Property title As String
                Public Property lengthSeconds As String
                Public Property keywords As String()
                Public Property channelId As String
                Public Property isOwnerViewing As Boolean
                Public Property shortDescription As String
                Public Property isCrawlable As Boolean
                Public Property averageRating As Double
                Public Property allowRatings As Boolean
                Public Property viewCount As String
                Public Property author As String
                Public Property isPrivate As Boolean
                Public Property isUnpluggedCorpus As Boolean
                Public Property isLiveContent As Boolean
            End Class

#End Region 'Nested Types

        End Class

#End Region 'Nested Types

    End Class

    Public Class UrlUtils

#Region "Fields"

        Private Shared ReadOnly YouTubeUrlRegex As Regex = New Regex("youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase)

#End Region 'Fields

#Region "Methods"

        Public Shared Function GetVideoIDFromURL(ByVal url As String, ByRef id As String) As Boolean
            Dim ytMatch As Match = YouTubeUrlRegex.Match(url)
            If ytMatch.Success Then
                id = ytMatch.Groups(4).ToString
                Return True
            Else
                id = String.Empty
                Return False
            End If
        End Function

        Public Shared Function IsYouTubeUrl(ByVal url As String) As Boolean
            Dim result As Match = YouTubeUrlRegex.Match(url)
            Return result.Success
        End Function

#End Region 'Methods

    End Class

End Namespace