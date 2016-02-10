﻿Imports System.Text
Imports NLog
Imports System.Globalization
Imports System.IO
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json


Namespace FFmpeg

    ''' <summary>
    ''' Contains functionality for interacting with FFmpeg.exe. For now its used to generate thumbnail images for video
    ''' In future uses this to scan metadata in Ember. Maybe use ffprobe.exe too....
    ''' </summary>
    Public Class FFmpeg
#Region "Fields"
        Private ReadOnly _output As StringBuilder
        Shared logger As Logger = LogManager.GetCurrentClassLogger()
#End Region

#Region "Constructors"
        Private Sub New(_FFmpegTask As FFmpegTask)
            CurrentFFmpegTask = _FFmpegTask
            _output = New StringBuilder()
        End Sub
#End Region

#Region "Properties"
        ''' <summary>
        ''' Get or set the ffmpeg settings used to process the videofile
        ''' </summary>
        Private Property CurrentFFmpegTask() As FFmpegTask

        ''' <summary>
        ''' Current output of running ffmpeg process
        ''' </summary>
        Protected ReadOnly Property Output() As String
            Get
                SyncLock _output
                    Return _output.ToString()
                End SyncLock
            End Get
        End Property
#End Region

#Region "FFmpeg Methods"
        ''' <summary>
        ''' Generates and saves thumbnail(s) for a specific DBElement. Returns generated thumbs in ImageContainer. 
        ''' The generated thumbnail(s) won't have black bars!
        ''' </summary>
        ''' <param name="DBElement">Movie/Show/Episode for which thumbnails should be created</param>
        ''' <param name="ThumbCount">Number of thumbnails to generate</param>
        ''' <param name="VideoFileDuration">Optional: The duration of videofile in seconds. Used to calculate the timeframe between thumbs. If not specified, duration will be calculated automatically</param>
        ''' <param name="NoSpoilers">true: Don't create thumbs for second half of video. Defaults to false if not specified (create tumbnails over whole movie)</param>
        ''' <param name="Timeout">The timeout to apply to FFmpeg, in milliseconds. Defaults to 20 seconds if not specified</param>
        ''' <returns>Returns imagecontainer which contains generated thumbnails</returns>
        ''' <remarks>
        ''' 2016/01/22 Cocotus - First implementation
        ''' Steps:
        ''' 1. Step (optional): Retrieve duration of videofile if not specified as parameter as its used to calculate timespan between thumbs
        ''' 2. Use calculated duration to set timespan between thumbs (we generate thumbcount * 2 first) 
        ''' 3. Retrieve correct thumb resolution based on video source (remove black bars)
        ''' 4. Use all above information and execute ffmpeg statement based on "intelligent" thumbnail extracting of ffmpeg discussed here:  http://superuser.com/questions/538112/meaningful-thumbnails-for-a-video-using-ffmpeg
        ''' 5. Save created thumbs under Ember temp folder and return specified amount of thumbs (prefer larger thumbs) as list of imagecontainer
        ''' </remarks>
        Public Shared Function GenerateThumbnailsWithoutBars(ByVal DBElement As Database.DBElement, ByVal ThumbCount As Integer, Optional ByVal VideoFileDuration As Integer = 0, Optional ByVal NoSpoilers As Boolean = False, Optional ByVal Timeout As Integer = 20000) As List(Of MediaContainers.Image)
            'set TEMP folder as savepath for thumbs
            Dim thumbPath As String = Path.Combine(Master.TempPath, "extrathumbs")
            'Retrieve the full file path to the source video file
            Dim ScanPath = GetVideoFileScanPath(DBElement)
            Dim lstThumbContainer As New List(Of MediaContainers.Image)


            If String.IsNullOrEmpty(ScanPath) Then
                logger.Warn(String.Format(("[FFmpeg] GenerateThumbnailsWithoutBars: Could not set ScanPath. Abort creation of thumbnails! File: {0}"), DBElement.Filename))
                Return lstThumbContainer
            End If

            'Step 1: First get the duration if necessary since it is needed to calculate the timespan between thumbs
            If VideoFileDuration = 0 Then
                'using FFmpeg...
                Dim s As String = GetMediaInfoByFFmpeg(DBElement, ScanPath)
                If s.Contains("Duration: ") Then
                    Dim sTime As String = Regex.Match(s, "Duration: (?<dur>.*?),").Groups("dur").ToString
                    If Not sTime = "N/A" Then
                        Dim ts As TimeSpan = CDate(CDate(String.Format("{0} {1}", DateTime.Today.ToString("d"), sTime))).Subtract(CDate(DateTime.Today))
                        VideoFileDuration = ((ts.Hours * 60) + ts.Minutes) * 60 + ts.Seconds
                    End If
                End If
                'using ffprobe...
                'Dim JsonOutput = FFmpeg.FFmpeg.GetMediaInfoByFFProbe(_DBElement, videofilepath)
                'If Not String.IsNullOrEmpty(JsonOutput) Then
                '    Dim tmpMediainfo = FFmpeg.FFmpeg.ParseMediaInfoByFFProbe(JsonOutput)
                '    If tmpMediainfo.StreamDetailsSpecified AndAlso tmpMediainfo.StreamDetails.VideoSpecified AndAlso tmpMediainfo.StreamDetails.Video(0).DurationSpecified Then
                '        Integer.TryParse(tmpMediainfo.StreamDetails.Video(0).Duration, intSeconds)
                '    End If
                'End If
            End If

            'Step 2: Calculate timespan between thumbs
            Dim secondsbetweenThumbs As Integer = 0
            Dim videoThumbnailPositionStr As String = String.Empty
            Dim output As String = String.Empty
            'video should be long enough to avoid opening credits! (=5min)
            If VideoFileDuration < (301 + (ThumbCount * 2)) Then
                'videofile should be at least 5min + thumbcount*2 long, otherwise exit and return empty imagecontainer
                logger.Warn(String.Format(("[FFmpeg] GenerateThumbnailsWithoutBars: Duration of file is shorter than required minimum: {0} seconds. Abort automatic creation of thumbnails! ScanPath: {1}"), 300 + (ThumbCount * 2), ScanPath))
                Return lstThumbContainer
            End If
            'calculate timeintervall for thumbnails (we always create double images, sort them later and only save the best ones)
            If NoSpoilers AndAlso VideoFileDuration > (600 + (ThumbCount * 2)) Then
                'We don't want to see spoilers in thumbs -> don't create images of second half of video, also avoid ending credits (5min)
                secondsbetweenThumbs = CInt(((VideoFileDuration / 2) - 300) / (ThumbCount * 2))
            ElseIf VideoFileDuration > (600 + (ThumbCount * 2)) Then
                'don't avoid spoilers but avoid (possible) ending credits
                secondsbetweenThumbs = CInt((VideoFileDuration - 600) / (ThumbCount * 2))
            Else
                'not possible to avoid spoilers and to avoid (possible) ending credits
                secondsbetweenThumbs = CInt((VideoFileDuration - 300) / (ThumbCount * 2))
            End If

            'Step 3: (Optional) Analyze video and retrieve real screensize without black bars
            Dim cropsize As String = ""
            If Master.eSettings.MovieExtrathumbsCreatorNoBlackBars Then
                cropsize = GetScreenSizeWithoutBars(DBElement, VideoFileDuration, ScanPath, Timeout)
            End If

            'Step 4: Build FFmpeg argument, which will generate the thumbs
            Dim args As String = String.Empty

            If Not Directory.Exists(thumbPath) Then
                Directory.CreateDirectory(thumbPath)
            Else
                FileUtils.Delete.DeleteDirectory(thumbPath)
                Directory.CreateDirectory(thumbPath)
            End If
            'explanations of parameters:
            'ss x: start x seconds from beginning
            '-y: Override any existing output file
            '-frames x: Tell ffmpeg that output from this command is/are x image(s)
            '-q:v x: Output quality, 0 is the best but also largest
            '-vf select= That's where all the magic happens. This is the selector function for video filter (like the value for crop we calculated earlier & autothumbnail(scene...) & mod..)
            ' not(mod(n\,x)): Select one frame every x frames see the documentation.+
            'create x images at specific timeframe starting from x second  onwards at calculcated intervall and crop them
            Dim tmpSpan As New TimeSpan(0, 0, (secondsbetweenThumbs))
            For i = 1 To (ThumbCount * 2)
                'first screenshot should be taken around 5 minutes from start to avoid credits/openening scenes
                If i = 1 Then
                    tmpSpan = tmpSpan + TimeSpan.FromSeconds(300)
                Else
                    tmpSpan = tmpSpan + TimeSpan.FromSeconds(secondsbetweenThumbs)
                End If
                videoThumbnailPositionStr = tmpSpan.ToString("hh\:mm\:ss")
                '  args = String.Format(CultureInfo.InvariantCulture, "-ss {0} -i ""{1}"" -vf {2} -frames:v 1 -vsync vfr -q:v 0 ""{3}"" -y", videoThumbnailPositionStr, ScanPath, cropsize, Path.Combine(thumbPath, "thumb" & i & ".jpg"))
                If String.IsNullOrEmpty(cropsize) Then
                    args = String.Format(CultureInfo.InvariantCulture, "-ss {0} -i ""{1}"" -frames:v 1 -q:v 0 ""{2}"" -y", videoThumbnailPositionStr, ScanPath, Path.Combine(thumbPath, "thumb" & i & ".jpg"))
                Else
                    args = String.Format(CultureInfo.InvariantCulture, "-ss {0} -i ""{1}"" -vf {2} -frames:v 1 -q:v 0 ""{3}"" -y", videoThumbnailPositionStr, ScanPath, cropsize, Path.Combine(thumbPath, "thumb" & i & ".jpg"))
                End If
                'logger.Info(String.Format(("[FFmpeg] GenerateThumbnailsWithoutBars: Args: {0}"), args))
                output = output & ExecuteFFmpeg(args:=args, timeout:=Timeout, dbelement:=DBElement)
            Next

            'Step 5: To find most interesting thumbs and to avoid black/white thumbs we sort all generated thumbs after size and pick the largest images (because those will contain more dynamic content)
            If Not Directory.Exists(thumbPath) Then
                Directory.CreateDirectory(thumbPath)
            End If
            Dim sortedThumbs = Directory.GetFiles(thumbPath, "*.jpg").OrderByDescending(Function(f) New FileInfo(f).Length).ToList
            If sortedThumbs.Count > 0 Then
                logger.Info(String.Format(("[FFmpeg] GenerateThumbnailsWithoutBars: {0} thumbs created. File: {1}"), sortedThumbs.Count, ScanPath))
                'remove the old ones
                ' Images.Delete_Movie(DBElement, Enums.ModifierType.MainExtrathumbs)
                'Step 3: load all valid extrathumbs into imagecontainer
                For i = 1 To sortedThumbs.Count
                    'need no more than thumbcount images
                    If lstThumbContainer.Count < ThumbCount Then
                        Dim eImg As New MediaContainers.Image
                        'not needed to load it here, just set localpath
                        'eImg.ImageOriginal.FromFile(sortedThumbs(i - 1), True)
                        eImg.LocalFilePath = sortedThumbs(i - 1)
                        lstThumbContainer.Add(eImg)
                    Else
                        Exit For
                    End If
                Next
                'Don't save here - just return filled imagecontainers and let other processes do the saving task!
                'For Each eImg As MediaContainers.Image In DBElement.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
                '    If eImg.LoadAndCache(DBElement.ContentType, True) Then
                '        eImg.ImageOriginal.SaveAsMovieExtrathumb(DBElement)
                '    End If
                'Next
            Else
                logger.Warn(String.Format(("[FFmpeg] GenerateThumbnailsWithoutBars: No thumbs created for {0}"), ScanPath))
            End If
            Return lstThumbContainer
        End Function


        ''' <summary>
        ''' Calculates the "real" screensize by neglecting black bars which might be part of video
        ''' </summary>
        ''' <param name="DBElement">Movie/Show/Episode for which thumbnails should be created</param>
        ''' <param name="Duration">Duration of video in seconds</param>
        ''' <param name="ScanPath">The full file path to the source video file which should be processed. If not specified ScanPath will be retrieved automatically</param>
        ''' <param name="Timeout">The timeout to apply to FFmpeg, in milliseconds. Defaults to 20 seconds if not specified</param>
        ''' <returns>Returns the cropsize of videofile, i.e "crop=1920:800:0:136". Returns empty string if error ocurred.</returns>
        ''' <remarks>
        ''' 2016/01/22 Cocotus - First implementation
        ''' Notice: Used cropdetect filter of ffmpeg to detect black bars in videofile
        ''' We analyze the file 3 times and later pick the largest screen size as "real" resolution. Scan multiple times to to avoid false values which can happen wenn whole screen is black (like creadits)!
        ''' Can be used to calculate "real" aspect ratio of videofiles, which might be important for all beamer owners out there
        ''' </remarks>
        Public Shared Function GetScreenSizeWithoutBars(ByVal DBElement As Database.DBElement, ByVal Duration As Integer, Optional ByVal ScanPath As String = "", Optional ByVal Timeout As Integer = 20000) As String
            If Duration < 4 Then
                logger.Warn(String.Format(("[FFmpeg] GetScreenSizeWithoutBars: Duration of videofile [s]: {0} Thats not a valid duration! Detection of black bars not possible!"), Duration))
            End If

            'Retrieve the full file path to the source video file (if necessary)
            If ScanPath = "" Then
                ScanPath = GetVideoFileScanPath(DBElement)
            End If

            Dim cropscanresult As String = String.Empty
            'save scannend resolution (=string, not sortable) together with highest resolution in tuple to sort for highest resolution and return that value
            Dim sortcrops As New List(Of Tuple(Of String, Double))
            Dim mc As MatchCollection
            Dim cropvalue As Double = 0
            ' analyze videofile 3times for 2 seconds to detect black bars
            ' we use duration of the videofile to determine the scanning points
            ' i.e 120min movie -> 120min/ 4 = 30min -> 1.Scan: 30min, 2.Scan: 60min, 3.Scan: 90min


            ' from beginning of video because often black bars are non existent in first seconds of a movie
            For i = 1 To 3
                mc = Nothing
                cropscanresult = ExecuteFFmpeg(String.Format("-ss {0} -i ""{1}"" -t {2} -vf cropdetect -f null NUL", (CInt(Duration / 4) * i), ScanPath, 2), DBElement, Timeout)
                'Example Output:
                '[Parsed_cropdetect_0 @ 054d2a20] x1:0 x2:1919 y1:136 y2:935 w:1920 h:800 x:0 y:1
                '36 pts:122291 t:1.358789 crop=1920:800:0:136
                'frame=    7 fps=0.0 q=0.0 Lsize=N/A time=00:00:01.00 bitrate=N/A
                'this means: crop=1920:800:0:136 is real resolution --> Aspect Ratio: 2:40
                'put all detected cropvalues in collection
                If Not String.IsNullOrEmpty(cropscanresult) Then
                    mc = System.Text.RegularExpressions.Regex.Matches(cropscanresult, ".*(crop=\d+:\d+:\d+:\d+).*")
                    If mc IsNot Nothing AndAlso mc.Count > 0 Then
                        For j = 0 To mc.Count - 1
                            'i.e. saving: [crop=1920:800:0:136,1920]
                            Dim tmpcropvalues As String() = mc(j).Groups(mc(j).Groups.Count - 1).Value.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                            If tmpcropvalues.Length = 4 Then
                                tmpcropvalues(0) = tmpcropvalues(0).Replace("crop=", "")
                                'save width+height as Double to use for sorting later
                                Double.TryParse(tmpcropvalues(0) & tmpcropvalues(1), cropvalue)
                            End If
                            sortcrops.Add((Tuple.Create(mc(j).Groups(mc(j).Groups.Count - 1).Value, cropvalue)))
                        Next
                    Else
                        logger.Info(String.Format(("[FFmpeg] GetScreenSizeWithoutBars: Result does not contain any cropvalues? Args: {0} Output: {1}"), String.Format("-ss {0} -i ""{1}"" -t {2} -vf cropdetect -f null NUL", (CInt(Duration / 4) * i), ScanPath, 2), cropscanresult))
                    End If
                Else
                    logger.Warn(String.Format(("[FFmpeg] GetScreenSizeWithoutBars: Failure Scan! File: {0} Args: {1}"), DBElement.Filename, String.Format("-ss {0} -i ""{1}"" -t {2} -vf cropdetect -f null NUL", (CInt(Duration / 4) * i), ScanPath, 2)))
                End If
            Next

            If sortcrops.Count < 1 Then
                logger.Warn("[FFmpeg] GetScreenSizeWithoutBars: Resolution not found!" & " File: " & DBElement.Filename)
                Return String.Empty
            Else
                'sort list, highest resolution on top -> this one will be returned!
                sortcrops = sortcrops.OrderByDescending(Function(X) X.Item2).ToList
                logger.Info(String.Format(("[FFmpeg] GetScreenSizeWithoutBars: Resolution: {0} File: {1}"), sortcrops(0).Item1, DBElement.Filename))
                Return sortcrops(0).Item1
            End If
        End Function

        ''' <summary>
        ''' Returns the output from the execution of FFmpeg against a specific videofile
        ''' </summary>
        ''' <param name="DBElement">Movie/Show/Episode for whichshould be scanned</param>
        ''' <param name="ScanPath">The full file path to the source video file</param>
        ''' <param name="Timeout">The timeout to apply to FFmpeg, in milliseconds. Defaults to 20 seconds if not specified</param>
        ''' <returns>Returns the text output from the execution of FFmpeg</returns>
        ''' <remarks>
        ''' 2016/01/22 Cocotus - First implementation
        ''' Just for some simple tests
        ''' Better use FFProbe to get videofile information since it returns JSON format which can be parsed easier
        ''' </remarks>
        Public Shared Function GetMediaInfoByFFmpeg(ByVal DBElement As Database.DBElement, ScanPath As String, Optional ByVal Timeout As Integer = 20000) As String
            Dim args As String = String.Format(CultureInfo.InvariantCulture, "-i ""{0}""", ScanPath)
            Return ExecuteFFmpeg(args:=args, timeout:=Timeout, dbelement:=DBElement)
        End Function


#End Region

#Region "FFProbe Methods"

        ''' <summary>
        ''' Returns the output from the execution of FFProbe against a specific videofile
        ''' </summary>
        ''' <param name="dbelement">Movie/Show/Episode for whichshould be scanned</param>
        ''' <param name="videofilepath">The full file path to the source video file</param>
        ''' <param name="timeout">The timeout to apply to FFProbe, in milliseconds. Defaults to 20 seconds if not specified</param>
        ''' <returns>Returns the JSON output from the execution of FFProbe</returns>
        ''' <remarks>
        ''' 2016/01/22 Cocotus - First implementation
        ''' </remarks>
        Public Shared Function GetMediaInfoByFFProbe(ByVal dbelement As Database.DBElement, videoFilePath As String, Optional ByVal timeout As Integer = 20000) As String
            Dim args As String = String.Format(CultureInfo.InvariantCulture, "-v quiet -print_format json -show_streams -show_format ""{0}""", videoFilePath)
            Return ExecuteFFmpeg(args:=args, timeout:=timeout, dbelement:=dbelement, UseFFProbe:=True)
        End Function

        ''' <summary>
        ''' Parses videofile using FFprobe and return parsed information as MediaInfo object
        ''' </summary>
        ''' <param name="jsonOutput">JSON which was created by FFProbe and should be parsed</param>
        ''' <returns>Mediainfo object containing all retrieved mediadetails</returns>
        ''' <remarks>
        ''' 2016/01/23 Cocotus - First implementation
        ''' Use FFProbe instead of FFmpeg because ffprobe supports output as JSON which makes parsing much easier using free Newtonsoft.Json library
        ''' Notice: Implemented a first version of ffprobe mediainfo scanning that may be used in future as alternative for MediaInfo scanning
        ''' Its not used right now
        ''' </remarks>
        Public Shared Function ParseMediaInfoByFFProbe(ByVal jsonOutput As String) As MediaInfo.Fileinfo

            'deserialize JSON
            Dim ffprobeResults As FFProbeResults = JsonConvert.DeserializeObject(Of FFProbeResults)(jsonOutput)
            Dim MediaInfo As New MediaInfo.Fileinfo
            Dim VideoInfo As New MediaInfo.Video
            Dim AudioInfo As New MediaInfo.Audio
            Dim SubtitleInfo As New MediaInfo.Subtitle

            'VideoInformation/AudioInformation(s)
            For Each stream As FFProbeStream In ffprobeResults.streams
                ' Skip invalid streams
                If stream.codec_type Is Nothing Then
                    Continue For
                End If

                ' Process the video stream (skip MJPEG streams) and use only the first Video stream with a width (ignore subsequent ones)
                If (stream.codec_type.Trim().ToLower() = "video") AndAlso (stream.codec_name.Trim().ToLower() <> "mjpeg") Then
                    VideoInfo = New MediaInfo.Video


                    'FileSize
                    If Not Double.TryParse(ffprobeResults.format.size, VideoInfo.Filesize) Then
                        logger.Warn("[FFmpeg] GetMediaInfoByFFProbe: Invalid Size: " & ffprobeResults.format.size)
                    End If
                    'Bitrate
                    Dim tmpnumber As Integer = 0
                    If Not Integer.TryParse(stream.bit_rate, tmpnumber) Then
                        ' Video bitrate, sometimes it's a N/A, use overall bitrate instead
                        VideoInfo.Bitrate = ffprobeResults.format.bit_rate
                    Else
                        VideoInfo.Bitrate = stream.bit_rate
                    End If
                    'Duration
                    If Not Integer.TryParse(ffprobeResults.format.duration, tmpnumber) Then
                        If ffprobeResults.format.duration.Contains(".") Then
                            VideoInfo.Duration = ffprobeResults.format.duration.Substring(0, ffprobeResults.format.duration.IndexOf("."))
                        Else
                            VideoInfo.Duration = ffprobeResults.format.duration
                        End If
                    Else
                        VideoInfo.Duration = CStr(tmpnumber)
                    End If

                    'DAR , that display aspect ratio
                    VideoInfo.Aspect = stream.display_aspect_ratio
                    ' Height
                    VideoInfo.Height = CStr((If(stream.height Is Nothing, 0, CInt(stream.height))))
                    ' Width
                    VideoInfo.Width = CStr((If(stream.width Is Nothing, 0, CInt(stream.width))))
                    ' Video codec name
                    VideoInfo.Codec = stream.codec_name
                    'Language
                    VideoInfo.Language = (If(stream.tags Is Nothing, "", stream.tags.language))

                    ' Not supported anymore:
                    'VideoInfo.Scantype = ???
                    'VideoInfo.StereoMode = ???
                    'VideoInfo.MultiViewCount = ???

                    'Currently not supported in existingn MEDIAINFO-structure of Ember:
                    'Color space pixel format
                    'VideoInfo.Format = stream.pix_fmt
                    'FPS does not exist in moment
                    'VideoInfo.FPS = stream.avg_frame_rate
                    'PID
                    'VideoInfo.PID = (If(String.IsNullOrWhiteSpace(stream.id), -1, Convert.ToInt32(stream.id.Replace("0x", ""), 16)))
                    'SAR
                    'VideoInfo.SAR = stream.sample_aspect_ratio

                    'Finally add Videoinformation to Ember MediaInfo object
                    MediaInfo.StreamDetails.Video.Add(VideoInfo)

                ElseIf stream.codec_type.Trim().ToLower() = "audio" Then
                    ' Create a new Audio object for each stream we find
                    AudioInfo = New MediaInfo.Audio

                    ' Audio codec name
                    AudioInfo.Codec = stream.codec_name
                    ' Audio Bitrate
                    AudioInfo.Bitrate = stream.bit_rate
                    ' Store the channel information
                    AudioInfo.Channels = CStr((If(stream.channels Is Nothing, 0, CInt(stream.channels))))
                    'Language
                    AudioInfo.Language = (If(stream.tags Is Nothing, "", stream.tags.language))

                    'Currently not supported in existingn MEDIAINFO-structure of Ember:
                    'PID
                    'AudioInfo.PID = (If(String.IsNullOrWhiteSpace(stream.id), -1, Convert.ToInt32(stream.id.Replace("0x", ""), 16)))
                    'Hz
                    'Integer.TryParse(stream.sample_rate, AudioInfo.Rate)
                    'Bits per sample
                    'AudioInfo.SamplingBits = (If(stream.bits_per_sample Is Nothing, -1, CInt(stream.bits_per_sample)))


                    'Finally add Videoinformation to Ember MediaInfo object
                    MediaInfo.StreamDetails.Audio.Add(AudioInfo)

                ElseIf stream.codec_type.Trim().ToLower() = "subtitle" Then
                    ' Create a new Subtitle object for each stream we find
                    SubtitleInfo = New MediaInfo.Subtitle
                    'Language
                    SubtitleInfo.Language = (If(stream.tags Is Nothing, "", stream.tags.language))

                    'Currently not supported in existingn MEDIAINFO-structure of Ember:
                    ' Subtitle Codec name
                    'SubtitleInfo.Name = stream.codec_name
                    ' PId
                    'SubtitleInfo.PID = (If(String.IsNullOrWhiteSpace(stream.id), -1, Convert.ToInt32(stream.id.Replace("0x", ""), 16)))


                    'Finally add Videoinformation to Ember MediaInfo object
                    MediaInfo.StreamDetails.Subtitle.Add(SubtitleInfo)
                End If
            Next

            Return MediaInfo
        End Function

#End Region

#Region "Private functions"
        ''' <summary>
        ''' Execute the FFmpeg or FFprobe with the given <paramref name="arguments"/> and return the text output generated by it
        ''' A default timeout value of 20 seconds is used, which can be overridden with the <paramref name="timeout" /> parameter
        ''' </summary>
        ''' <param name="args">The argument values to pass to FFmpeg/FFprobe</param>
        ''' <param name="dbelement">Movie/Show/Episode that should be scanned</param>
        ''' <param name="timeout">The timeout to apply to FFmpeg/FFprobe, in milliseconds. Defaults to 20 seconds if not specified</param>
        ''' <param name="UseFFProbe">Use FFmpeg or FFprobe for query? By default FFmpeg is used to execute commands</param>
        ''' <returns>Returns the text output from the execution of FFmpeg/FFprobe</returns>
        ''' <remarks>
        ''' 2016/01/22 Cocotus - First implementation
        ''' </remarks>
        Private Shared Function ExecuteFFmpeg(args As String, ByVal dbelement As Database.DBElement, Optional timeout As Integer = 20000, Optional UseFFProbe As Boolean = False) As String
            Dim FFmpegTaskSettings = New FFmpegTask() With {.DBElement = dbelement, .Timeout = timeout, .FFmpegArgs = args, .FFmpegOutput = String.Empty}
            If UseFFProbe Then
                FFmpegTaskSettings.FFmpegPath = Functions.GetFFProbe
            Else
                FFmpegTaskSettings.FFmpegPath = Functions.GetFFMpeg
            End If
            Return ExecuteFFmpeg(FFmpegTaskSettings)
        End Function

        ''' <summary>
        ''' Execute the FFmpeg or FFprobe with the given <paramref name="arguments"/> and return the text output generated by it
        ''' </summary>
        ''' <param name="_settings">The ffmpeg task settings.</param>
        ''' <returns>Returns the text output from the execution of FFmpeg/FFprobe</returns>
        ''' <remarks>
        ''' 2016/01/22 Cocotus - First implementation
        ''' </remarks>
        Private Shared Function ExecuteFFmpeg(_settings As FFmpegTask) As String
            Dim ffmpeg As New FFmpeg(_settings)
            ffmpeg.Execute()
            _settings.FFmpegOutput = ffmpeg.Output
            Return _settings.FFmpegOutput
        End Function

        ''' <summary>
        ''' Run the FFmpeg/FFprobe executable with the specified command line arguments
        ''' </summary>
        ''' <remarks>
        ''' 2016/01/22 Cocotus - First implementation
        ''' </remarks>
        Private Sub Execute()
            Dim processCompletedSuccessfully As Boolean = False

            Dim ffmpeg As New ProcessStartInfo
            ffmpeg.FileName = CurrentFFmpegTask.FFmpegPath
            ffmpeg.Arguments = CurrentFFmpegTask.FFmpegArgs
            ffmpeg.UseShellExecute = False
            ffmpeg.CreateNoWindow = True
            ffmpeg.RedirectStandardError = True
            ffmpeg.RedirectStandardOutput = True

            'log the arguments/input
            '_output.AppendLine("Argument String:")
            '_output.AppendLine(CurrentFFmpegTask.FFmpegArgs)

            Using p As New Process()
                Try
                    p.StartInfo = ffmpeg
                    'For some reason, FFmpeg sends data to the ErrorDataReceived event rather than OutputDataReceived -> Listen to ErrorDataReceived
                    If ffmpeg.FileName.ToLower.EndsWith("ffmpeg.exe") Then
                        AddHandler p.ErrorDataReceived, AddressOf ErrorDataReceived
                        p.Start()
                        p.BeginErrorReadLine()
                    Else
                        AddHandler p.OutputDataReceived, AddressOf OutputDataReceived
                        p.Start()
                        p.BeginOutputReadLine()
                    End If
                    processCompletedSuccessfully = p.WaitForExit(CurrentFFmpegTask.Timeout)
                    If Not processCompletedSuccessfully Then
                        p.Kill()
                    End If
                    p.WaitForExit()
                    'Delete result if there are problems?!
                    'If Not processCompletedSuccessfully OrElse CurrentFFmpegTask.CancelToken.IsCancellationRequested Then
                    '    File.Delete(CurrentFFmpegTask.TargetPath)
                    'End If
                Catch ex As Exception
                    If Not ex.Data.Contains("args") Then
                        ex.Data.Add("args", CurrentFFmpegTask.FFmpegArgs)
                    End If
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            End Using

            If Not processCompletedSuccessfully Then
                logger.Warn(String.Format("Timed out while processing the file. Maybe increase timeout? Currently set to {0} ms", CurrentFFmpegTask.Timeout))
                logger.Warn(String.Format("Arguments: ", CurrentFFmpegTask.FFmpegArgs))
                logger.Warn(String.Format("Output: ", CurrentFFmpegTask.FFmpegOutput))
            End If
        End Sub

        ''' <summary>
        ''' The Data Received Event of current FFmpeg task
        ''' </summary>
        ''' <param name="sender">The instance containing the event data</param>
        ''' <param name="e">The instance containing the event data</param>
        ''' <remarks>
        ''' 2016/01/22 Cocotus - First implementation
        ''' </remarks>
        Private Sub ErrorDataReceived(sender As Object, e As DataReceivedEventArgs)
            'write to output
            _output.AppendLine(e.Data)
            CancelIfRequested(TryCast(sender, Process))
        End Sub

        ''' <summary>
        ''' The Data Received Event of current FFmpeg task
        ''' </summary>
        ''' <param name="sender">The instance containing the event data</param>
        ''' <param name="e">The instance containing the event data</param>
        ''' <remarks>
        ''' 2016/01/22 Cocotus - First implementation
        ''' </remarks>
        Private Sub OutputDataReceived(sender As Object, e As DataReceivedEventArgs)
            'write to output
            _output.AppendLine(e.Data)
            CancelIfRequested(TryCast(sender, Process))
        End Sub
        ''' <summary>
        ''' Kill the running process if requested
        ''' </summary>
        ''' <param name="process">The process running FFmpeg/FFprobe</param>
        ''' <remarks>
        ''' 2016/01/22 Cocotus - First implementation
        ''' </remarks>
        Private Sub CancelIfRequested(process As Process)
            Dim ct As Threading.CancellationToken = CurrentFFmpegTask.CancelToken
            If ct.IsCancellationRequested Then
                If process IsNot Nothing Then
                    Try
                        process.Kill()
                        process.WaitForExit()
                    Catch generatedExceptionName As System.ComponentModel.Win32Exception
                    Catch generatedExceptionName As SystemException
                    End Try
                End If
            End If
        End Sub

        ''' <summary>
        ''' Get the correct file which should be processed by MediaInfo/FFmpeg/FFprobe.
        ''' Supports also .ISO/.BIN files!
        ''' </summary>
        ''' <param name="dbelement">Movie/Show/Episode which should be scanned</param>
        ''' <returns>Returns full file path to the source video file which should be scanned by MediaInfo/FFmpeg/FFprobe</returns>
        ''' <remarks>
        ''' 2016/01/22 Cocotus - First implementation
        ''' Code below is also part of clsMediaInfo(ScanMI) but now put in a separate function because ffmpeg/ffprobe needs this too!
        ''' </remarks>
        Private Shared Function GetVideoFileScanPath(ByVal DBElement As Database.DBElement) As String
            Dim videofilepath As String = String.Empty
            Dim videofileExt As String = Path.GetExtension(DBElement.Filename).ToLower
            If videofileExt = ".rar" AndAlso Not videofileExt = ".img" AndAlso Not videofileExt = ".cue" Then
                'not supported?!
            End If
            Select Case DBElement.ContentType
                Case Enums.ContentType.Movie
                    If FileUtils.Common.isBDRip(DBElement.Filename) Then
                        'filename points to largest m2ts file, i.e:
                        'E:\Media_1\Movie\Horror\Europa Report\BDMV\STREAM\00000.m2ts
                        videofilepath = FileUtils.Common.GetLongestFromRip(DBElement.Filename)
                    ElseIf FileUtils.Common.isVideoTS(DBElement.Filename) Then
                        'filename points to largest VOB  file
                        videofilepath = FileUtils.Common.GetLongestFromRip(DBElement.Filename)
                    ElseIf videofileExt = ".iso" OrElse videofileExt = ".bin" Then
                        Dim driveletter As String = Master.eSettings.GeneralDaemonDrive ' i.e. "F:\"
                        'Toolpath either VCDMOUNT.exe or DTLite.exe!
                        Dim ToolPath As String = Master.eSettings.GeneralDaemonPath
                        'Now only use DAEMON Tools to mount ISO if installed on user system
                        If Not String.IsNullOrEmpty(driveletter) AndAlso Not String.IsNullOrEmpty(ToolPath) Then
                            'Either DAEMONToolsLite or VirtualCloneDrive (http://www.slysoft.com/en/virtual-clonedrive.html)
                            If ToolPath.ToUpper.Contains("VCDMOUNT") Then
                                'First unmount, i.e "C:\Program Files\Elaborate Bytes\VirtualCloneDrive\VCDMount.exe" /u
                                '  Run_Process(ToolPath, " /u", False, True)
                                'Mount ISO on virtual drive, i.e c:\Program Files (x86)\Elaborate Bytes\VirtualCloneDrive\vcdmount.exe U:\isotest\test2iso.ISO
                                Functions.Run_Process(ToolPath, """" & videofilepath & """", False, True)
                                System.Threading.Thread.Sleep(8000)
                                'Toolpath doesn't contain virtualclonedrive.exe -> assume daemon tools with DS type drive!
                            Else
                                'Unmount
                                '   Run_Process(ToolPath, " -unmount 0", False, True)
                                'Mount
                                Functions.Run_Process(ToolPath, " -mount 0, " & """" & videofilepath & """", False, True)
                                System.Threading.Thread.Sleep(8000)
                            End If
                            'now check if it's bluray or dvd image/VIDEO_TS/BMDV Folder-Scanning!
                            If Directory.Exists(driveletter & "VIDEO_TS") Then
                                'get biggest VOB file for thumbscraping
                                Dim lFileList As New List(Of FileInfo)
                                lFileList.AddRange(New DirectoryInfo(driveletter & "VIDEO_TS").GetFiles("*.vob"))
                                videofilepath = lFileList.OrderByDescending(Function(a) a.Length).Select(Function(a) a.FullName).FirstOrDefault()
                            ElseIf Directory.Exists(driveletter & "BDMV\STREAM") Then
                                'get biggest m2ts file for thumbscraping
                                Dim lFileList As New List(Of FileInfo)
                                lFileList.AddRange(New DirectoryInfo(driveletter & "BDMV\STREAM").GetFiles("*.m2ts"))
                                videofilepath = lFileList.OrderByDescending(Function(b) b.Length).Select(Function(b) b.FullName).FirstOrDefault()
                            End If
                        End If
                        'default case
                    Else
                        videofilepath = DBElement.Filename
                    End If
                Case Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                    logger.Warn(String.Format(("[FFmpeg] GetVideoFileScanPath: Current DBElement is not a movie - not supported! File: {0}"), DBElement.Filename))
                    If FileUtils.Common.isBDRip(DBElement.ShowPath) Then
                        'no tv support for now...
                    ElseIf FileUtils.Common.isVideoTS(DBElement.ShowPath) Then
                        'no tv support for now...
                    Else
                        'no tv support for now...
                    End If
                Case Enums.ContentType.TVEpisode
                    'no tv support for now...
            End Select
            Return videofilepath
        End Function

#End Region

    End Class


    ''' <summary>
    ''' Every information for a single ffmpeg task is stored in this object
    ''' </summary>
    ''' <remarks>
    ''' 2016/01/22 Cocotus - First implementation
    ''' Use this class to store all necessary information for ffmpeg task
    ''' The properties defined here will be used to create argument(s) for ffmpeg/ffprobe
    ''' </remarks>
    Public Class FFmpegTask
        ''' <summary>
        ''' Either path to ffmpeg.exe or ffprobe.exe
        ''' </summary>
        Public Property FFmpegPath() As String = Functions.GetFFMpeg

        ''' <summary>
        ''' The (console) output that FFmpeg generates
        ''' </summary>
        Public Property FFmpegOutput() As String = String.Empty

        ''' <summary>
        ''' Arguments to provide for FFmpeg conversion . 
        ''' May contains replacement tokens like {VideofilePath}
        ''' </summary>
        Public Property FFmpegArgs() As String = String.Empty

        ''' <summary>
        ''' Timeout to apply for conversion [ms]
        ''' 20 seconds as default value
        ''' </summary>
        Public Property Timeout() As Integer = 20000

        ''' <summary>
        ''' Current database element (movie/show/episode..)
        ''' </summary>
        Public Property DBElement() As Database.DBElement = Nothing

        ''' <summary>
        ''' Can be used to cancel the conversion process when whole conversion is running asynchronously
        ''' </summary>
        Public Property CancelToken() As Threading.CancellationToken
    End Class

    ''' <summary>
    ''' Helper class for storing JSON output from FFprobe
    ''' </summary>
    ''' <remarks>
    ''' 2016/01/22 Cocotus - First implementation
    ''' Use this class to store all necessary information for a ffprobe scrape
    ''' The properties defined here will be used to create argument(s) for ffmpeg/ffprobe
    ''' </remarks>
    Public Class FFProbeResults
        Property programs() As List(Of FFProbeProgram)
        Property streams() As List(Of FFProbeStream)
        Property format() As FFProbeFormat
    End Class
    Public Class FFProbeInterlaceDetection
        Public TFF As Long
        Public BFF As Long
        Public Progressive As Long
        Public Undetermined As Long

        Public Overrides Function ToString() As String
            Dim ret As String = ""

            ret += "Top Frame First -> " + TFF.ToString() + vbCr & vbLf
            ret += "Bottom Frame First -> " + BFF.ToString() + vbCr & vbLf
            ret += "Progressive Frames -> " + Progressive.ToString() + vbCr & vbLf
            ret += "Undetermined Frames -> " + Undetermined.ToString() + vbCr & vbLf

            Return ret
        End Function
    End Class

    Public Class FFProbeDisposition
        Public Property [default]() As Integer
        Public Property dub() As Integer
        Public Property original() As Integer
        Public Property comment() As Integer
        Public Property lyrics() As Integer
        Public Property karaoke() As Integer
        Public Property forced() As Integer
        Public Property hearing_impaired() As Integer
        Public Property visual_impaired() As Integer
        Public Property clean_effects() As Integer
        Public Property attached_pic() As Integer
    End Class

    Public Class FFProbeTags
        Public Property language() As String
        Public Property title() As String
    End Class

    Public Class FFProbeStream
        Public Property index() As Integer
        Public Property codec_type() As String
        Public Property codec_time_base() As String
        Public Property codec_tag_string() As String
        Public Property codec_tag() As String
        Public Property id() As String
        Public Property r_frame_rate() As String
        Public Property avg_frame_rate() As String
        Public Property time_base() As String
        Public Property start_pts() As Long
        Public Property start_time() As String
        Public Property duration_ts() As Long
        Public Property duration() As String
        Public Property disposition() As FFProbeDisposition
        Public Property codec_name() As String
        Public Property codec_long_name() As String
        Public Property sample_fmt() As String
        Public Property sample_rate() As String
        Public Property channels() As System.Nullable(Of Integer)
        Public Property channel_layout() As String
        Public Property bits_per_sample() As System.Nullable(Of Integer)
        Public Property dmix_mode() As String
        Public Property ltrt_cmixlev() As String
        Public Property ltrt_surmixlev() As String
        Public Property loro_cmixlev() As String
        Public Property loro_surmixlev() As String
        Public Property bit_rate() As String
        Public Property tags() As FFProbeTags
        Public Property profile() As String
        Public Property width() As System.Nullable(Of Integer)
        Public Property height() As System.Nullable(Of Integer)
        Public Property has_b_frames() As System.Nullable(Of Integer)
        Public Property sample_aspect_ratio() As String
        Public Property display_aspect_ratio() As String
        Public Property pix_fmt() As String
        Public Property level() As System.Nullable(Of Integer)
        Public Property timecode() As String
    End Class

    Public Class FFProbeFormat
        Public Property filename() As String
        Public Property nb_streams() As Integer
        Public Property nb_programs() As Integer
        Public Property format_name() As String
        Public Property format_long_name() As String
        Public Property start_time() As String
        Public Property duration() As String
        Public Property size() As String
        Public Property bit_rate() As String
        Public Property probe_score() As Integer
        Public Property tags() As Object
    End Class

    Public Class FFProbeTagsP
        Public Property service_name() As String
        Public Property service_provider() As String
    End Class

    Public Class FFProbeProgram
        Public Property program_id() As Integer
        Public Property program_num() As Integer
        Public Property nb_streams() As Integer
        Public Property pmt_pid() As Integer
        Public Property pcr_pid() As Integer
        Public Property start_pts() As Long
        Public Property start_time() As String
        Public Property end_pts() As Long
        Public Property end_time() As String
        Public Property tags() As FFProbeTagsP
        Public Property streams() As List(Of FFProbeStream)
    End Class

End Namespace