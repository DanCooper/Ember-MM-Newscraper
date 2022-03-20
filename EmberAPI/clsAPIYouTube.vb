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
Imports VideoLibrary
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Threading.Tasks

'The InternalsVisibleTo is required for unit testing the friend methods
<Assembly: InternalsVisibleTo("EmberAPI_Test")>

Namespace YouTube

    Public Class Scraper

#Region "Fields"

        Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

        Private Shared _Client As New VideoLibrary.YouTube

#End Region 'Fields

#Region "Methods"

        Private Shared Function ConvertAudioBitrate(ByVal bitrate As Integer) As Enums.AudioBitrate
            Select Case bitrate
                Case 48
                    Return Enums.AudioBitrate.Q48kbps
                Case 64
                    Return Enums.AudioBitrate.Q64kbps
                Case 128
                    Return Enums.AudioBitrate.Q128kbps
                Case 192
                    Return Enums.AudioBitrate.Q192kbps
                Case 256
                    Return Enums.AudioBitrate.Q256kbps
                Case 384
                    Return Enums.AudioBitrate.Q384kbps
                Case 512
                    Return Enums.AudioBitrate.Q512kbps
                Case Else
                    Return Enums.AudioBitrate.UNKNOWN
            End Select
        End Function

        Private Shared Function ConvertAudioCodec(ByVal codec As AudioFormat) As Enums.AudioCodec
            Select Case codec
                Case AudioFormat.Aac
                    Return Enums.AudioCodec.AAC
                Case AudioFormat.Opus
                    Return Enums.AudioCodec.Opus
                Case AudioFormat.Vorbis
                    Return Enums.AudioCodec.Vorbis
                Case Else
                    Return Enums.AudioCodec.UNKNOWN
            End Select
        End Function

        Public Shared Function GetVideoDetails(ByVal videoIdOrUrl As String) As MediaContainers.MediaFile
            If String.IsNullOrEmpty(videoIdOrUrl) Then Return Nothing

            Dim strVideoId As String = String.Empty
            If UrlUtils.IsYouTubeUrl(videoIdOrUrl) Then
                UrlUtils.GetVideoIDFromURL(videoIdOrUrl, strVideoId)
            Else
                strVideoId = videoIdOrUrl
            End If

            Try
                Dim nResults = _Client.GetAllVideosAsync(String.Concat("https://www.youtube.com/watch?v=", strVideoId)).Result
                If nResults IsNot Nothing AndAlso nResults.Count > 0 Then
                    Dim nStreams = SetInformationByITag(nResults)
                    If nStreams IsNot Nothing Then
                        Return New MediaContainers.MediaFile With {
                            .Duration = StringUtils.SecondsToDuration(nResults(0).Info.LengthSeconds.ToString),
                            .Source = "YouTube",
                            .Streams = nStreams,
                            .Title = nResults(0).Title,
                            .UrlForNfo = String.Concat("http://www.youtube.com/watch?v=", strVideoId),
                            .UrlWebsite = String.Concat("http://www.youtube.com/watch?v=", strVideoId)
                        }
                    End If
                End If
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
            Return Nothing
        End Function

        'Private Shared Function GetVideoInfo(ByVal videoID As String) As VideoInfo
        '    If String.IsNullOrEmpty(videoID) Then Return Nothing
        '    Dim sHTTP As New HTTP
        '    Dim rawResult As String = HttpUtility.UrlDecode(sHTTP.DownloadData(String.Concat("https://www.youtube.com/get_video_info?&video_id=", videoID)))
        '    rawResult = Regex.Match(rawResult, "{.*}").Value
        '    rawResult = Regex.Replace(rawResult, "\u0026", "&")
        '    Dim videoInfo = JsonConvert.DeserializeObject(Of VideoInfo)(rawResult)
        '    Return videoInfo
        'End Function
        ''' <summary>
        ''' Search for the given string on YouTube
        ''' </summary>
        ''' <param name="searchString"><c>String</c> to search for</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function SearchOnYouTube(ByVal searchString As String, ByVal audioStreamsOnly As Boolean) As List(Of MediaContainers.MediaFile)
            Dim tHTTP As New HTTP
            Dim nVideoList As New List(Of MediaContainers.MediaFile)

            '&sp=EgIQAQ%253D%253D sets the filter to "video" only (no playlists, channels etc.)
            Dim nHtml As String = tHTTP.DownloadData(String.Concat("http://www.youtube.com/results?search_query=", HttpUtility.UrlEncode(searchString), "&sp=EgIQAQ%253D%253D"))
            If nHtml.ToLower.Contains("page not found") Then Return nVideoList

            Dim strSearchResultsArea As String = "{""responseContext"":{.*?};"
            Dim strVideoIdPattern As String = "{""videoId"":""(.*?)"""

            Try
                Dim regSearchResultsArea As MatchCollection = Regex.Matches(nHtml, strSearchResultsArea, RegexOptions.Singleline, TimeSpan.FromSeconds(1))
                If regSearchResultsArea.Count = 1 Then
                    Dim regSearchResults As MatchCollection = Regex.Matches(regSearchResultsArea.Item(0).Value, strVideoIdPattern, RegexOptions.Singleline, TimeSpan.FromSeconds(1))
                    Dim lstVideoIds As New List(Of String)
                    For ctr = 0 To regSearchResults.Count - 1
                        Dim tId As String
                        tId = regSearchResults.Item(ctr).Groups(1).Value
                        'prevent duplicate entries
                        If (Not lstVideoIds.Contains(tId)) Then lstVideoIds.Add(tId)
                    Next
                    Dim nVideoDict As New SortedDictionary(Of Long, MediaContainers.MediaFile)
                    Dim parallelOptions = New ParallelOptions With {
                        .MaxDegreeOfParallelism = 10
                    }
                    Dim nVideoDictLock As New Object
                    Parallel.ForEach(lstVideoIds, parallelOptions,
                                     Sub(tId As String, loopstate As ParallelLoopState, index As Long)
                                         Dim nVideoDetails = GetVideoDetails(tId)

                                         If nVideoDetails IsNot Nothing Then
                                             SyncLock nVideoDictLock
                                                 nVideoDict.Add(index, nVideoDetails)
                                             End SyncLock
                                         End If
                                     End Sub)
                    nVideoList = nVideoDict.Values.ToList()
                    For Each tVideo In nVideoList
                        tVideo.Streams.BuildStreamVariants(audioStreamsOnly)
                        tVideo.Scraper = "Search"
                    Next
                End If
            Catch ex As TimeoutException
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Return nVideoList
        End Function

        Private Shared Function SetInformationByITag(ByVal streamList As IEnumerable(Of YouTubeVideo)) As MediaContainers.MediaFile.StreamCollection
            Dim nStreams As New MediaContainers.MediaFile.StreamCollection
            For Each tStream In streamList
                Dim audioStream As MediaContainers.MediaFile.AudioStream = Nothing
                Dim videoStream As MediaContainers.MediaFile.VideoStream = Nothing

                Select Case tStream.FormatCode
                    Case 5 'FLV
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H263,
                            .Resolution = Enums.VideoResolution.SQ240p
                        }
                    Case 6 'FLV
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H263,
                            .Resolution = Enums.VideoResolution.SQ240p
                        }
                    Case 13 'ThreeGP
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H263,
                            .Resolution = Enums.VideoResolution.SQ144p15fps
                        }
                    Case 17 'ThreeGP
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.SQ144p
                        }
                    Case 18 'MP4
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.SQ360p
                        }
                    Case 22 'MP4
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.HD720p
                        }
                    Case 34 'FLV
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.SQ360p
                        }
                    Case 35 'FLV
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.HQ480p
                        }
                    Case 36 'ThreeGP
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.SQ240p
                        }
                    Case 37 'MP4
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.HD1080p
                        }
                    Case 38 'MP4
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.HD2160p
                        }
                    Case 43 'WebM
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP8,
                            .Resolution = Enums.VideoResolution.SQ360p
                        }
                    Case 44 'WebM
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP8,
                            .Resolution = Enums.VideoResolution.HQ480p
                        }
                    Case 45 'WebM
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP8,
                            .Resolution = Enums.VideoResolution.HD720p
                        }
                    Case 46 'WebM
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP8,
                            .Resolution = Enums.VideoResolution.HD1080p
                        }
                    Case 82 'MP4
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.SQ360p
                        }
                    Case 83 'MP4
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.HQ480p
                        }
                    Case 84 'MP4
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.HD720p
                        }
                    Case 85 'MP4
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.HD1080p
                        }
                    Case 100 'WebM
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP8,
                            .Resolution = Enums.VideoResolution.SQ360p
                        }
                    Case 101 'WebM
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP8,
                            .Resolution = Enums.VideoResolution.SQ360p
                        }
                    Case 102 'WebM
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP8,
                            .Resolution = Enums.VideoResolution.HD720p
                        }
                    Case 133 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.SQ240p
                        }
                    Case 134 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.SQ360p
                        }
                    Case 135 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.HQ480p
                        }
                    Case 136 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.HD720p
                        }
                    Case 137 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.HD1080p
                        }
                    Case 139 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.AAC,
                            .Bitrate = Enums.AudioBitrate.Q48kbps
                        }
                    Case 140 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.AAC,
                            .Bitrate = Enums.AudioBitrate.Q128kbps
                        }
                    Case 141 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.AAC,
                            .Bitrate = Enums.AudioBitrate.Q256kbps
                        }
                    Case 160 'DASH (video only, 15 fps)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.SQ144p15fps
                        }
                    Case 167 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP8,
                            .Resolution = Enums.VideoResolution.SQ360p
                        }
                    Case 168 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP8,
                            .Resolution = Enums.VideoResolution.HQ480p
                        }
                    Case 171 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.Vorbis,
                            .Bitrate = Enums.AudioBitrate.Q128kbps
                        }
                    Case 172 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.Vorbis,
                            .Bitrate = Enums.AudioBitrate.Q192kbps
                        }
                    Case 218 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP8,
                            .Resolution = Enums.VideoResolution.HQ480p
                        }
                    Case 219 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP8,
                            .Resolution = Enums.VideoResolution.HQ480p
                        }
                    Case 242 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP9,
                            .Resolution = Enums.VideoResolution.SQ240p
                        }
                    Case 243 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP9,
                            .Resolution = Enums.VideoResolution.SQ360p
                        }
                    Case 244 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP9,
                            .Resolution = Enums.VideoResolution.HQ480p
                        }
                    Case 245 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP9,
                            .Resolution = Enums.VideoResolution.HQ480p
                        }
                    Case 246 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP9,
                            .Resolution = Enums.VideoResolution.HQ480p
                        }
                    Case 247 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP9,
                            .Resolution = Enums.VideoResolution.HD720p
                        }
                    Case 248 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP9,
                            .Resolution = Enums.VideoResolution.HD1080p
                        }
                    Case 249 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.Opus,
                            .Bitrate = Enums.AudioBitrate.Q48kbps
                        }
                    Case 250 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.Opus,
                            .Bitrate = Enums.AudioBitrate.Q64kbps
                        }
                    Case 251 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.Opus,
                            .Bitrate = Enums.AudioBitrate.Q128kbps
                        }
                    Case 256 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.AAC_SPATIAL,
                            .Bitrate = Enums.AudioBitrate.Q192kbps
                        }
                    Case 258 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.AAC_SPATIAL,
                            .Bitrate = Enums.AudioBitrate.Q384kbps
                        }
                    Case 264 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.HD1440p
                        }
                    Case 266 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.HD2160p
                        }
                    Case 271 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP9,
                            .Resolution = Enums.VideoResolution.HD1440p
                        }
                    Case 272 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP9,
                            .Resolution = Enums.VideoResolution.HD2160p
                        }
                    Case 278 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP9,
                            .Resolution = Enums.VideoResolution.SQ144p
                        }
                    Case 298 'DASH (video only, 60 fps)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.HD720p60fps
                        }
                    Case 299 'DASH (video only, 60 fps)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.H264,
                            .Resolution = Enums.VideoResolution.HD1080p60fps
                        }
                    Case 302 'DASH (video only, 60 fps)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP9,
                            .Resolution = Enums.VideoResolution.HD720p60fps
                        }
                    Case 303 'DASH (video only, 60 FPSfps)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP9,
                            .Resolution = Enums.VideoResolution.HD1080p60fps
                        }
                    Case 308 'DASH (video only, 60 FPSfps)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP9,
                            .Resolution = Enums.VideoResolution.HD1440p
                        }
                    Case 313 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.VP9,
                            .Resolution = Enums.VideoResolution.HD1080p
                        }
                    Case 325 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.DTSE_SPATIAL,
                            .Bitrate = Enums.AudioBitrate.Q384kbps
                        }
                    Case 327 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.AAC_SPATIAL,
                            .Bitrate = Enums.AudioBitrate.Q256kbps
                        }
                    Case 328 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.EC3_SPATIAL,
                            .Bitrate = Enums.AudioBitrate.Q384kbps
                        }
                    Case 380 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.AC3_SPATIAL,
                            .Bitrate = Enums.AudioBitrate.Q384kbps
                        }
                    Case 339 'DASH (audio only)
                        audioStream = New MediaContainers.MediaFile.AudioStream With {
                            .Codec = Enums.AudioCodec.Vorbis_SPATIAL,
                            .Bitrate = Enums.AudioBitrate.Q256kbps
                        }
                    Case 394 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.AV1,
                            .Resolution = Enums.VideoResolution.SQ144p
                        }
                    Case 395 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.AV1,
                            .Resolution = Enums.VideoResolution.SQ240p
                        }
                    Case 396 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.AV1,
                            .Resolution = Enums.VideoResolution.SQ360p
                        }
                    Case 397 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.AV1,
                            .Resolution = Enums.VideoResolution.HQ480p
                        }
                    Case 398 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.AV1,
                            .Resolution = Enums.VideoResolution.HD720p
                        }
                    Case 399 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.AV1,
                            .Resolution = Enums.VideoResolution.HD1080p
                        }
                    Case 400 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.AV1,
                            .Resolution = Enums.VideoResolution.HD1440p
                        }
                    Case 401 'DASH (video only)
                        videoStream = New MediaContainers.MediaFile.VideoStream With {
                            .Codec = Enums.VideoCodec.AV1,
                            .Resolution = Enums.VideoResolution.HD2160p
                        }
                    Case Else
                        _Logger.Warn(String.Format("Unknown YouTube ITAG: {0}, URL: {1}", tStream.FormatCode, tStream.Uri))
                End Select

                If videoStream IsNot Nothing Then
                    videoStream.FileExtension = tStream.FileExtension
                    If tStream.IsEncrypted Then
                        videoStream.YouTubeContainer = tStream
                        nStreams.VideoStreams.Add(videoStream)
                    Else
                        videoStream.StreamUrl = tStream.Uri
                        nStreams.VideoStreams.Add(videoStream)
                    End If
                    If tStream.IsAdaptive Then
                        videoStream.IsAdaptive = True
                    Else
                        videoStream.AudioBitrate = ConvertAudioBitrate(tStream.AudioBitrate)
                        videoStream.AudioCodec = ConvertAudioCodec(tStream.AudioFormat)
                    End If
                ElseIf audioStream IsNot Nothing Then
                    audioStream.FileExtension = tStream.FileExtension
                    If tStream.IsEncrypted Then
                        audioStream.YouTubeContainer = tStream
                        nStreams.AudioStreams.Add(audioStream)
                    Else
                        audioStream.StreamUrl = tStream.Uri
                        nStreams.AudioStreams.Add(audioStream)
                    End If
                End If
            Next
            nStreams.AudioStreams.Sort()
            nStreams.VideoStreams.Sort()
            Return nStreams
        End Function

#End Region 'Methods

        '#Region "Nested Types"

        '        Public Class VideoInfo

        '#Region "Properties"

        '            Public Property playabilityStatus As New PlayabilityStatusItem
        '            Public Property streamingData As New StreamingDataItem
        '            Public Property videoDetails As New VideoDetailsItem

        '#End Region 'Properties

        '#Region "Nested Types"

        '            Public Class PlayabilityStatusItem
        '                Public Property status As String
        '                Public Property playableInEmbed As Boolean
        '                Public Property contextParams As String
        '            End Class

        '            Public Class Format
        '                Public Property itag As Integer
        '                Public Property url As String
        '                Public Property mimeType As String
        '                Public Property bitrate As Integer
        '                Public Property width As Integer
        '                Public Property height As Integer
        '                Public Property lastModified As String
        '                Public Property contentLength As String
        '                Public Property quality As String
        '                Public Property qualityLabel As String
        '                Public Property projectionType As String
        '                Public Property averageBitrate As Integer
        '                Public Property audioQuality As String
        '                Public Property approxDurationMs As String
        '                Public Property audioSampleRate As String
        '                Public Property audioChannels As Integer
        '                Public Property signatureCipher As String
        '            End Class

        '            Public Class Range
        '                Public Property start As String
        '                Public Property [end] As String
        '            End Class

        '            Public Class ColorInfo
        '                Public Property primaries As String
        '                Public Property transferCharacteristics As String
        '                Public Property matrixCoefficients As String
        '            End Class

        '            Public Class AdaptiveFormat
        '                Inherits Format
        '                Public Property initRange As New Range
        '                Public Property indexRange As New Range
        '                Public Property fps As Integer
        '                Public Property colorInfo As New ColorInfo
        '                Public Property highReplication As Boolean?
        '            End Class

        '            Public Class StreamingDataItem
        '                Public Property expiresInSeconds As String
        '                Public Property formats As Format()
        '                Public ReadOnly Property HasStreams As Boolean
        '                    Get
        '                        Return formats IsNot Nothing AndAlso formats.Count > 0 OrElse adaptiveFormats IsNot Nothing AndAlso adaptiveFormats.Count > 0
        '                    End Get
        '                End Property
        '                Public Property adaptiveFormats As AdaptiveFormat()
        '            End Class

        '            Public Class VideoDetailsItem
        '                Public Property videoId As String
        '                Public Property title As String
        '                Public Property lengthSeconds As String
        '                Public Property keywords As String()
        '                Public Property channelId As String
        '                Public Property isOwnerViewing As Boolean
        '                Public Property shortDescription As String
        '                Public Property isCrawlable As Boolean
        '                Public Property averageRating As Double
        '                Public Property allowRatings As Boolean
        '                Public Property viewCount As String
        '                Public Property author As String
        '                Public Property isPrivate As Boolean
        '                Public Property isUnpluggedCorpus As Boolean
        '                Public Property isLiveContent As Boolean
        '            End Class

        '#End Region 'Nested Types

        '        End Class

        '#End Region 'Nested Types

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