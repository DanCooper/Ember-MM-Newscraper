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
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions

Public Class MediaInfo

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _Handle As IntPtr
    Private _UseAnsi As Boolean

#End Region 'Fields

#Region "Enumerations"

    Public Enum InfoKind As UInteger
        Name
        Text
    End Enum

    Public Enum StreamKind As UInteger
        General
        Visual
        Audio
        Text
    End Enum

#End Region 'Enumerations

#Region "Methods"

    'Private Sub GetFileInfoFromPath(ByRef fiInfo As MediaContainers.FileInfo, ByVal sPath As String, ByVal contentType As Enums.ContentType)
    '    If Not String.IsNullOrEmpty(sPath) AndAlso File.Exists(sPath) Then
    '        Dim sExt As String = Path.GetExtension(sPath).ToLower
    '        Dim fiOut As New MediaContainers.FileInfo
    '        Dim miVideo As New MediaContainers.Video
    '        Dim miAudio As New MediaContainers.Audio
    '        Dim miSubtitle As New MediaContainers.Subtitle
    '        Dim AudioStreams As Integer
    '        Dim SubtitleStreams As Integer
    '        Dim aLang As String = String.Empty
    '        Dim sLang As String = String.Empty
    '        Dim cDVD As New DVD

    '        Dim ifoVideo(2) As String
    '        Dim ifoAudio(2) As String

    '        If Master.eSettings.MovieScraperMetaDataIFOScan AndAlso (sExt = ".ifo" OrElse sExt = ".vob" OrElse sExt = ".bup") AndAlso cDVD.fctOpenIFOFile(sPath) Then
    '            Try
    '                ifoVideo = cDVD.GetIFOVideo
    '                Dim vRes() As String = ifoVideo(1).Split(Convert.ToChar("x"))
    '                miVideo.Width = ConvertVideoWidthOrHeight(vRes(0))
    '                miVideo.Height = ConvertVideoWidthOrHeight(vRes(1))
    '                miVideo.Codec = ifoVideo(0)
    '                miVideo.Duration = ConvertVideoDuration(cDVD.GetProgramChainPlayBackTime(1))
    '                miVideo.Aspect = ConvertVideoAspectRatio(ifoVideo(2))

    '                With miVideo
    '                    If .CodecSpecified OrElse
    '                        .DurationSpecified OrElse
    '                        .AspectSpecified OrElse
    '                        .HeightSpecified OrElse
    '                        .WidthSpecified Then
    '                        fiOut.StreamDetails.Video.Add(miVideo)
    '                    End If
    '                End With

    '                AudioStreams = cDVD.GetIFOAudioNumberOfTracks
    '                For a As Integer = 1 To AudioStreams
    '                    miAudio = New MediaContainers.Audio
    '                    ifoAudio = cDVD.GetIFOAudio(a)
    '                    miAudio.Codec = ifoAudio(0)
    '                    miAudio.Channels = ConvertAudioChannels(ifoAudio(2))
    '                    aLang = ifoAudio(1)
    '                    If Not String.IsNullOrEmpty(aLang) Then
    '                        miAudio.LongLanguage = aLang
    '                        If Not String.IsNullOrEmpty(Localization.ISOLangGetCode3ByLang(miAudio.LongLanguage)) Then
    '                            miAudio.Language = Localization.ISOLangGetCode3ByLang(miAudio.LongLanguage)
    '                        End If
    '                    End If
    '                    With miAudio
    '                        If .CodecSpecified OrElse .ChannelsSpecified OrElse .LanguageSpecified Then
    '                            fiOut.StreamDetails.Audio.Add(miAudio)
    '                        End If
    '                    End With
    '                Next

    '                SubtitleStreams = cDVD.GetIFOSubPicNumberOf
    '                For s As Integer = 1 To SubtitleStreams
    '                    miSubtitle = New MediaContainers.Subtitle
    '                    sLang = cDVD.GetIFOSubPic(s)
    '                    If Not String.IsNullOrEmpty(sLang) Then
    '                        miSubtitle.LongLanguage = sLang
    '                        Dim strLanguage = Localization.ISOLangGetCode3ByLang(miSubtitle.LongLanguage)
    '                        If Not String.IsNullOrEmpty(strLanguage) Then
    '                            miSubtitle.Language = strLanguage
    '                        End If
    '                        If Not String.IsNullOrEmpty(miSubtitle.Language) Then
    '                            'miSubtitle.SubsForced = Not supported(?)
    '                            fiOut.StreamDetails.Subtitle.Add(miSubtitle)
    '                        End If
    '                    End If
    '                Next

    '                cDVD.Close()
    '                cDVD = Nothing

    '                fiInfo = fiOut
    '            Catch ex As Exception
    '                logger.Error(ex, New StackFrame().GetMethod().Name)
    '            End Try

    '            'cocotus 20140118 For more accurate metadata scanning of BLURAY/DVD images use improved mediainfo scanning (ScanMI-function) -> don't hop in this branch!! 
    '            '  ElseIf StringUtils.IsStacked(Path.GetFileNameWithoutExtension(sPath), True) OrElse FileUtils.Common.isVideoTS(sPath) OrElse FileUtils.Common.isBDRip(sPath) Then
    '        ElseIf FileUtils.Common.isStacked(sPath) Then
    '            Try
    '                Dim oFile As String = FileUtils.Common.RemoveStackingMarkers(sPath)
    '                Dim sFile As New List(Of String)
    '                Dim bIsVTS As Boolean = False

    '                If sExt = ".ifo" OrElse sExt = ".bup" OrElse sExt = ".vob" Then
    '                    bIsVTS = True
    '                End If

    '                If bIsVTS Then
    '                    Try
    '                        sFile.AddRange(Directory.GetFiles(Directory.GetParent(sPath).FullName, "VTS*.VOB"))
    '                    Catch
    '                    End Try
    '                ElseIf sExt = ".m2ts" Then
    '                    Try
    '                        sFile.AddRange(Directory.GetFiles(Directory.GetParent(sPath).FullName, "*.m2ts"))
    '                    Catch
    '                    End Try
    '                Else
    '                    Try
    '                        sFile.AddRange(Directory.GetFiles(Directory.GetParent(sPath).FullName, String.Concat(Path.GetFileNameWithoutExtension(FileUtils.Common.RemoveStackingMarkers(sPath)), "*")))
    '                    Catch
    '                    End Try
    '                End If

    '                Dim TotalDur As Integer = 0
    '                Dim tInfo As New MediaContainers.FileInfo
    '                Dim tVideo As New MediaContainers.Video
    '                Dim tAudio As New MediaContainers.Audio

    '                miVideo.Width = 0
    '                miAudio.Channels = 0

    '                For Each File As String In sFile
    '                    'make sure the file is actually part of the stack
    '                    'handles movie.cd1.ext, movie.cd2.ext and movie.extras.ext
    '                    'disregards movie.extras.ext in this case
    '                    If bIsVTS OrElse (oFile = FileUtils.Common.RemoveStackingMarkers(File)) Then
    '                        tInfo = ScanFileInfo(File)

    '                        tVideo = NFO.GetBestVideo(tInfo)
    '                        tAudio = NFO.GetBestAudio(tInfo, contentType)

    '                        If Not miVideo.CodecSpecified OrElse tVideo.CodecSpecified Then
    '                            If tVideo.WidthSpecified AndAlso tVideo.Width >= miVideo.Width Then
    '                                miVideo = tVideo
    '                            End If
    '                        End If

    '                        If Not miAudio.CodecSpecified OrElse tAudio.CodecSpecified Then
    '                            If tAudio.ChannelsSpecified AndAlso tAudio.Channels >= miAudio.Channels Then
    '                                miAudio = tAudio
    '                            End If
    '                        End If

    '                        If tVideo.DurationSpecified Then TotalDur += tVideo.Duration

    '                        For Each sSub As MediaContainers.Subtitle In tInfo.StreamDetails.Subtitle
    '                            If Not fiOut.StreamDetails.Subtitle.Contains(sSub) Then
    '                                fiOut.StreamDetails.Subtitle.Add(sSub)
    '                            End If
    '                        Next
    '                    End If
    '                Next

    '                fiOut.StreamDetails.Video.Add(miVideo)
    '                fiOut.StreamDetails.Audio.Add(miAudio)
    '                fiOut.StreamDetails.Video(0).Duration = TotalDur

    '                fiInfo = fiOut
    '            Catch ex As Exception
    '                logger.Error(ex, New StackFrame().GetMethod().Name)
    '            End Try
    '        Else
    '            fiInfo = ScanFileInfo(sPath)
    '        End If
    '    End If
    'End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    <DllImport("Bin\MediaInfo.DLL")>
    Private Shared Function MediaInfoA_Get(ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As UIntPtr, ByVal Parameter As IntPtr, ByVal KindOfInfo As UIntPtr, ByVal KindOfSearch As UIntPtr) As IntPtr
    End Function

    <DllImport("Bin\MediaInfo.DLL")>
    Private Shared Function MediaInfoA_Open(ByVal Handle As IntPtr, ByVal FileName As IntPtr) As UIntPtr
    End Function

    <DllImport("Bin\MediaInfo.DLL")>
    Private Shared Sub MediaInfo_Close(ByVal Handle As IntPtr)
    End Sub

    <DllImport("Bin\MediaInfo.DLL")>
    Private Shared Function MediaInfo_Count_Get(ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As IntPtr) As Integer
    End Function

    <DllImport("Bin\MediaInfo.DLL")>
    Private Shared Sub MediaInfo_Delete(ByVal Handle As IntPtr)
    End Sub

    <DllImport("Bin\MediaInfo.DLL")>
    Private Shared Function MediaInfo_Get(ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As UIntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByVal Parameter As String, ByVal KindOfInfo As UIntPtr, ByVal KindOfSearch As UIntPtr) As IntPtr
    End Function

    <DllImport("Bin\MediaInfo.DLL")>
    Private Shared Function MediaInfo_New() As IntPtr
    End Function

    <DllImport("Bin\MediaInfo.DLL")>
    Private Shared Function MediaInfo_Open(ByVal Handle As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByVal FileName As String) As UIntPtr
    End Function

    Private Sub Close()
        MediaInfo_Close(_Handle)
        MediaInfo_Delete(_Handle)
        _Handle = Nothing
    End Sub

    ''' <summary>
    ''' Convert string "x/y" to single digit number "x" (Audio Channel conversion)
    ''' </summary>
    ''' <param name="channelinfo">The channelstring (as string) to clean</param>
    ''' <returns>cleaned Channelnumber, i.e  "Object Based / 8 / 6" will return a 8 </returns>
    ''' <remarks>Inputstring "x/y" will return as "x" which is highest number, i.e 8/6 -> 8 (assume: highest number always first!)
    '''</remarks>
    Private Function ConvertAudioChannels(ByVal channelinfo As String) As Integer
        'for channel information like "15 objects / 6"
        'returns only the number of channels
        Dim rMatch = Regex.Match(channelinfo.ToLower, "(\d+) objects \/? (\d+)")
        If rMatch.Success Then
            Return CInt(rMatch.Groups(2).Value)
        End If
        'for channel information like "Object Based / 8 / 6" or "8 / 6" or "8"
        'returns the highest number
        Dim rMatches = Regex.Matches(channelinfo.ToLower, "\d+")
        If rMatches.Count > 0 Then
            Dim lstNumber As New List(Of Integer)
            For i As Integer = 0 To rMatches.Count - 1
                lstNumber.Add(CInt(rMatches(i).Value))
            Next
            lstNumber.Sort()
            lstNumber.Reverse()
            Return lstNumber(0)
        End If
        Return 0
    End Function

    Private Function ConvertAudioFormat(ByVal sCodecID As String, ByVal sFormat As String, ByVal sCodecHint As String, ByVal sProfile As String) As String
        Dim strCodec As String = String.Empty
        If sFormat.ToLower.Contains("dts") AndAlso (sProfile.ToLower.Contains("hra / core") OrElse sProfile.ToLower.Contains("ma / core")) Then
            strCodec = sProfile
        ElseIf sFormat.ToLower.Contains("atmos / truehd") Then
            strCodec = sFormat
        ElseIf sProfile.ToLower.Contains("truehd+atmos") Then
            strCodec = sProfile
        ElseIf sProfile.ToLower.Contains("e-ac-3+atmos") Then
            strCodec = "e-ac-3+atmos"
        ElseIf Not String.IsNullOrEmpty(sCodecID) AndAlso Not Integer.TryParse(sCodecID, 0) AndAlso Not sCodecID.ToLower.Contains("a_pcm") AndAlso Not sCodecID.Contains("00001000-0000-0100-8000-00AA00389B71") Then
            strCodec = sCodecID
        ElseIf Not String.IsNullOrEmpty(sCodecHint) Then
            strCodec = sCodecHint
        ElseIf sFormat.ToLower.Contains("mpeg") AndAlso Not String.IsNullOrEmpty(sProfile) Then
            strCodec = String.Concat("mp", sProfile.Replace("Layer", String.Empty).Trim).Trim
        ElseIf Not String.IsNullOrEmpty(sFormat) Then
            strCodec = sFormat
        End If
        Return strCodec.ToLower
    End Function

    Private Function ConvertBitDepth(ByVal bitdepth As String) As Integer
        Dim iBitdepth As Integer
        Integer.TryParse(bitdepth, iBitdepth)
        Return iBitdepth
    End Function
    ''' <summary>
    ''' Convert the bitrate of a audio or video stream into kb/s
    ''' </summary>
    ''' <param name="bitrate">audio or video bitrate "Bitrate" in bits per second</param>
    ''' <param name="alternateBitrate">"BitRate_Maximum" of a vbr audio stream or "OverallBitRate" of a general video stream</param>
    ''' <returns>Bitrate in kilobits per second</returns>
    Private Function ConvertBitrate(ByVal bitrate As String, Optional ByVal alternateBitrate As String = "") As Integer
        'for bitrate information like "3018000 / 1509000"
        'returns the highest number
        Dim rMatches = Regex.Matches(If(Not String.IsNullOrEmpty(bitrate), bitrate, alternateBitrate), "\d+")
        If rMatches.Count > 0 Then
            Dim lstNumber As New List(Of Integer)
            For i As Integer = 0 To rMatches.Count - 1
                lstNumber.Add(CInt(rMatches(i).Value))
            Next
            lstNumber.Sort()
            lstNumber.Reverse()
            Return CInt(lstNumber(0) / 1000)
        End If
        Return 0
    End Function
    ''' <summary>
    ''' Converts "Yes" and "No" to boolean
    ''' </summary>
    ''' <param name="textYesNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertBoolean(ByVal textYesNo As String) As Boolean
        If Not String.IsNullOrEmpty(textYesNo) Then
            Select Case textYesNo.ToLower
                Case "yes"
                    Return True
                Case "no"
                    Return False
            End Select
        End If
        Return False
    End Function

    Private Function ConvertVideoAspectRatio(ByVal ratio As String) As Double
        Dim dblRatio As Double
        Double.TryParse(ratio, dblRatio)
        Return dblRatio
    End Function
    ''' <summary>
    ''' Convert the duration of video stream into seconds
    ''' </summary>
    ''' <param name="duration">video duration as "Duration/String3" like HH:MM:SS.MMM</param>
    ''' <param name="generalDuration">general duration as "Duration/String3" like HH:MM:SS.MMM</param>
    ''' <returns>duration in seconds</returns>
    Private Function ConvertVideoDuration(ByVal duration As String, Optional generalDuration As String = "") As Integer
        'It's possible that duration returns empty when retrieved from videostream data.
        'So instead use "General" section of MediaInfo.dll to read duration (it's always filled) 
        Dim tsDuration As TimeSpan
        If TimeSpan.TryParse(duration, tsDuration) Then
            Return CInt(tsDuration.TotalSeconds)
        ElseIf TimeSpan.TryParse(generalDuration, tsDuration) Then
            Return CInt(tsDuration.TotalSeconds)
        End If
        Return 0
    End Function

    Private Function ConvertVideoFormat(ByVal sCodecID As String, ByVal sFormat As String, ByVal sVersion As String) As String
        Dim strCodec As String = String.Empty
        If Not String.IsNullOrEmpty(sCodecID) AndAlso Not Integer.TryParse(sCodecID, 0) Then
            strCodec = sCodecID
        ElseIf sFormat.ToLower.Contains("mpeg") AndAlso Not String.IsNullOrEmpty(sVersion) Then
            strCodec = String.Concat("mpeg", sVersion.Replace("Version", String.Empty).Trim, "video").Trim
        ElseIf Not String.IsNullOrEmpty(sFormat) Then
            strCodec = sFormat
        End If
        Return strCodec.ToLower
    End Function

    Private Function ConvertVideoMultiViewCount(ByVal multiViewCount As String) As Integer
        Dim iCount As Integer
        Integer.TryParse(multiViewCount, iCount)
        Return iCount
    End Function

    Public Shared Function ConvertVideoMultiViewLayoutToStereoMode(ByVal sFormat As String) As String
        'MultiViewLayout (http://matroska.org/technical/specs/index.html#StereoMode) 
        Select Case sFormat.ToLower
            Case "side by side (left eye first)"
                Return "left_right"
            Case "top-bottom (right eye first)"
                Return "bottom_top"
            Case "top-bottom (left eye first)"
                Return "bottom_top"
            Case "checkboard (right eye first)"
                Return "checkerboard_rl"
            Case "checkboard (left eye first)"
                Return "checkerboard_lr"
            Case "row interleaved (right eye first)"
                Return "row_interleaved_rl"
            Case "row interleaved (left eye first)"
                Return "row_interleaved_lr"
            Case "column interleaved (right eye first)"
                Return "col_interleaved_rl"
            Case "column interleaved (left eye first)"
                Return "col_interleaved_lr"
            Case "anaglyph (cyan/red)"
                Return "anaglyph_cyan_red"
            Case "side by side (right eye first)"
                Return "right_left"
            Case "anaglyph (green/magenta)"
                Return "anaglyph_green_magenta"
            Case "both eyes laced in one block (left eye first)"
                Return "block_lr"
            Case "both eyes laced in one block (right eye first)"
                Return "block_rl"
            Case Else
                Return String.Empty
        End Select
    End Function

    Private Function ConvertVideoWidthOrHeight(ByVal widthOrHeight As String) As Integer
        Dim iSize As Integer
        Integer.TryParse(widthOrHeight, iSize)
        Return iSize
    End Function

    Private Function ConvertVideoStereoToShort(ByVal sFormat As String) As String
        If Not String.IsNullOrEmpty(sFormat) Then
            Select Case sFormat.ToLower
                Case "bottom_top"
                    Return "tab"
                Case "left_right", "right_left"
                    Return "sbs"
                Case Else
                    Return "unknown"
            End Select
        End If
        Return String.Empty
    End Function

    Private Function Count_Get(ByVal streamKind As StreamKind, Optional ByVal streamNumber As UInteger = UInteger.MaxValue) As Integer
        If streamNumber = UInteger.MaxValue Then
            Return MediaInfo_Count_Get(_Handle, CType(streamKind, UIntPtr), CType(-1, IntPtr))
        Else
            Return MediaInfo_Count_Get(_Handle, CType(streamKind, UIntPtr), CType(streamNumber, IntPtr))
        End If
    End Function

    Private Function Get_(ByVal streamKind As StreamKind, ByVal streamNumber As Integer, ByVal parameter As String, Optional ByVal kindOfInfo As InfoKind = InfoKind.Text, Optional ByVal kindOfSearch As InfoKind = InfoKind.Name) As String
        If _UseAnsi Then
            Dim Parameter_Ptr As IntPtr = Marshal.StringToHGlobalAnsi(parameter)
            Dim ToReturn As String = Marshal.PtrToStringAnsi(MediaInfoA_Get(_Handle, CType(streamKind, UIntPtr), CType(streamNumber, UIntPtr), Parameter_Ptr, CType(kindOfInfo, UIntPtr), CType(kindOfSearch, UIntPtr)))
            Marshal.FreeHGlobal(Parameter_Ptr)
            Return ToReturn
        Else
            Return Marshal.PtrToStringUni(MediaInfo_Get(_Handle, CType(streamKind, UIntPtr), CType(streamNumber, UIntPtr), parameter, CType(kindOfInfo, UIntPtr), CType(kindOfSearch, UIntPtr)))
        End If
    End Function

    Private Sub Open(ByVal path As String)
        If _UseAnsi Then
            Dim FileName_Ptr As IntPtr = Marshal.StringToHGlobalAnsi(path)
            MediaInfoA_Open(_Handle, FileName_Ptr)
            Marshal.FreeHGlobal(FileName_Ptr)
        Else
            MediaInfo_Open(_Handle, path)
        End If
    End Sub

    ''' <summary>
    ''' Use MediaInfo to get/scan subtitle, audio Stream and video information of videofile
    ''' </summary>
    ''' <returns>Mediainfo-Scanresults as MediainfoFileInfoObject</returns>
    Private Function ScanFileInfo(ByVal path As String) As MediaContainers.FileInfo
        Dim fiOut As New MediaContainers.FileInfo
        Dim fiIFO As New MediaContainers.FileInfo
        Try
            If Not String.IsNullOrEmpty(path) Then
                Dim nVirtualDrive As FileUtils.VirtualDrive = Nothing
                Dim miVideo As New MediaContainers.Video
                Dim miAudio As New MediaContainers.Audio
                Dim miSubtitle As New MediaContainers.Subtitle
                Dim a_Profile As String = String.Empty
                Dim sExt As String = IO.Path.GetExtension(path).ToLower
                Dim alternativeIFOFile As String = String.Empty

                'New ISO Handling -> Use VitualCloneDrive to mount ISO!
                If FileUtils.Common.isDiscImage(path) Then ' OrElse
                    'FileUtils.Common.isVideoTS(path) OrElse
                    'FileUtils.Common.isBDRip(path) Then

                    'ISO-File Scanning using VCDMount.exe to mount and read file!
                    If FileUtils.Common.isDiscImage(path) Then
                        nVirtualDrive = New FileUtils.VirtualDrive(path)
                        If nVirtualDrive.IsLoaded Then
                            'now check if it's bluray or dvd image/VIDEO_TS/BMDV Folder-Scanning!
                            If Directory.Exists(String.Concat(nVirtualDrive.Path, "VIDEO_TS")) Then
                                path = String.Concat(nVirtualDrive.Path, "VIDEO_TS")
                                SetMediaInfoScanPaths(path, fiIFO, alternativeIFOFile, True)
                                'get foldersize information
                            ElseIf Directory.Exists(nVirtualDrive.Path & "BDMV\STREAM") Then
                                path = nVirtualDrive.Path & "BDMV\STREAM"
                                SetMediaInfoScanPaths(path, fiIFO, alternativeIFOFile, True)
                            End If
                        End If
                    Else
                        'VIDEO_TS/BMDV Folder-Scanning!
                        If Directory.Exists(Directory.GetParent(path).FullName) Then
                            SetMediaInfoScanPaths(path, fiIFO, alternativeIFOFile, False)
                        End If
                    End If
                End If

                If Not String.IsNullOrEmpty(path) Then
                    _Handle = MediaInfo_New()

                    If Master.isWindows Then
                        _UseAnsi = False
                    Else
                        _UseAnsi = True
                    End If

                    Open(path)

                    Dim iVideoStreamsCount = Count_Get(StreamKind.Visual)
                    Dim iAudioStreamsCount = Count_Get(StreamKind.Audio)
                    Dim iSubtitleStreamsCount = Count_Get(StreamKind.Text)

                    '2014/07/05 Fix for VIDEO_TS scanning: Use second largest (=alternativeIFOFile) IFO File if largest File doesn't contain needed information (=duration)! (rare case!)
                    If path.ToUpper.Contains("VIDEO_TS") Then
                        miVideo = New MediaContainers.Video
                        'IFO Scan results (used when scanning VIDEO_TS files)
                        If fiIFO.StreamDetails.Video.Count > 0 Then
                            If fiIFO.StreamDetails.Video(0).DurationSpecified Then
                                miVideo.Duration = fiIFO.StreamDetails.Video(0).Duration
                            Else
                                miVideo.Duration = ConvertVideoDuration(Get_(StreamKind.Visual, 0, "Duration/String1"), Get_(StreamKind.General, 0, "Duration/String1"))
                            End If
                        Else
                            'It's possible that duration returns empty when retrieved from videostream data.
                            'So instead use "General" section of MediaInfo.dll to read duration (it's always filled)
                            miVideo.Duration = ConvertVideoDuration(Get_(StreamKind.Visual, 0, "Duration/String1"), Get_(StreamKind.General, 0, "Duration/String1"))
                        End If
                        'if ms instead of hours or minutes than wrong IFO!
                        If miVideo.Duration = 0 Then
                            fiIFO = Nothing
                            fiIFO = ScanLanguage(alternativeIFOFile)
                        End If
                    End If

                    For v As Integer = 0 To iVideoStreamsCount - 1
                        miVideo = New MediaContainers.Video
                        miVideo.Bitrate = ConvertBitrate(Get_(StreamKind.Visual, v, "BitRate/String"))
                        miVideo.MultiViewCount = ConvertVideoMultiViewCount(Get_(StreamKind.Visual, v, "MultiView_Count"))
                        miVideo.StereoMode = ConvertVideoMultiViewLayoutToStereoMode(miVideo.MultiViewLayout)
                        miVideo.Width = ConvertVideoWidthOrHeight(Get_(StreamKind.Visual, v, "Width"))
                        miVideo.Height = ConvertVideoWidthOrHeight(Get_(StreamKind.Visual, v, "Height"))
                        miVideo.Codec = ConvertVideoFormat(Get_(StreamKind.Visual, v, "CodecID"), Get_(StreamKind.Visual, v, "Format"),
                                                   Get_(StreamKind.Visual, v, "Format_Version"))

                        'IFO Scan results (used when scanning VIDEO_TS files)
                        If fiIFO.StreamDetails.Video.Count > 0 Then
                            If fiIFO.StreamDetails.Video(v).DurationSpecified Then
                                miVideo.Duration = fiIFO.StreamDetails.Video(v).Duration
                            Else
                                miVideo.Duration = ConvertVideoDuration(Get_(StreamKind.Visual, v, "Duration/String1"), Get_(StreamKind.General, 0, "Duration/String1"))
                            End If
                        Else
                            miVideo.Duration = ConvertVideoDuration(Get_(StreamKind.Visual, v, "Duration/String1"), Get_(StreamKind.General, 0, "Duration/String1"))
                        End If

                        Dim dblAspect As Double
                        Dim strAspect = Get_(StreamKind.Visual, v, "DisplayAspectRatio")
                        Double.TryParse(strAspect, dblAspect)
                        miVideo.Aspect = dblAspect
                        miVideo.Scantype = Get_(StreamKind.Visual, v, "ScanType")

                        Dim vLang = Get_(StreamKind.Visual, v, "Language/String")
                        If Not String.IsNullOrEmpty(vLang) Then
                            miVideo.LongLanguage = vLang
                            Dim strLanguage = Localization.ISOLangGetCode3ByLang(miVideo.LongLanguage)
                            If Not String.IsNullOrEmpty(strLanguage) Then
                                miVideo.Language = strLanguage
                            End If
                        End If

                        fiOut.StreamDetails.Video.Add(miVideo)
                    Next


                    For a As Integer = 0 To iAudioStreamsCount - 1
                        miAudio = New MediaContainers.Audio
                        miAudio.Codec = ConvertAudioFormat(Get_(StreamKind.Audio, a, "CodecID"),
                                                       Get_(StreamKind.Audio, a, "Format"),
                                                       Get_(StreamKind.Audio, a, "CodecID/Hint"),
                                                       Get_(StreamKind.Audio, a, "Format_Profile"))
                        miAudio.Channels = ConvertAudioChannels(Get_(StreamKind.Audio, a, "Channel(s)_Original"))
                        If Not miAudio.ChannelsSpecified Then
                            miAudio.Channels = ConvertAudioChannels(Get_(StreamKind.Audio, a, "Channel(s)"))
                        End If

                        miAudio.Bitrate = ConvertBitrate(Get_(StreamKind.Audio, a, "BitRate/String"))

                        Dim aLang = Get_(StreamKind.Audio, a, "Language/String")
                        If Not String.IsNullOrEmpty(aLang) Then
                            miAudio.LongLanguage = aLang
                            If Localization.ISOLangGetCode3ByLang(miAudio.LongLanguage) <> "" Then
                                miAudio.Language = Localization.ISOLangGetCode3ByLang(miAudio.LongLanguage)
                            End If
                            'IFO Scan results (used when scanning VIDEO_TS files)
                        ElseIf fiIFO.StreamDetails.Audio.Count > 0 Then
                            If Not String.IsNullOrEmpty(fiIFO.StreamDetails.Audio(a).LongLanguage) Then
                                miAudio.LongLanguage = fiIFO.StreamDetails.Audio(a).LongLanguage
                                miAudio.Language = fiIFO.StreamDetails.Audio(a).Language
                            End If
                        End If

                        'With miAudio
                        '    If Not String.IsNullOrEmpty(.Codec) OrElse Not String.IsNullOrEmpty(.Channels) OrElse Not String.IsNullOrEmpty(.Language) Then
                        '        fiOut.StreamDetails.Audio.Add(miAudio)
                        '    End If
                        'End With
                        fiOut.StreamDetails.Audio.Add(miAudio)
                    Next


                    For s As Integer = 0 To iSubtitleStreamsCount - 1

                        miSubtitle = New MediaContainers.Subtitle

                        Dim sLang = Get_(StreamKind.Text, s, "Language/String")
                        If Not String.IsNullOrEmpty(sLang) Then
                            miSubtitle.LongLanguage = sLang
                            If Localization.ISOLangGetCode3ByLang(miSubtitle.LongLanguage) <> "" Then
                                miSubtitle.Language = Localization.ISOLangGetCode3ByLang(miSubtitle.LongLanguage)
                            End If
                            miSubtitle.Forced = True

                            'IFO Scan results (used when scanning VIDEO_TS files)
                        ElseIf fiIFO.StreamDetails.Subtitle.Count > 0 Then
                            If Not String.IsNullOrEmpty(fiIFO.StreamDetails.Subtitle(s).LongLanguage) Then
                                miSubtitle.LongLanguage = fiIFO.StreamDetails.Subtitle(s).LongLanguage
                                miSubtitle.Language = fiIFO.StreamDetails.Subtitle(s).Language
                            End If
                        End If

                        If Not String.IsNullOrEmpty(miSubtitle.Language) Then
                            miSubtitle.Forced = ConvertBoolean(Get_(StreamKind.Text, s, "Forced/String"))
                            fiOut.StreamDetails.Subtitle.Add(miSubtitle)
                        End If
                    Next
                End If

                If nVirtualDrive IsNot Nothing AndAlso nVirtualDrive.IsLoaded Then
                    nVirtualDrive.UnmountDiscImage()
                End If

                Close()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return fiOut
    End Function

    ''' <summary>
    ''' Use MediaInfo.dll Scan to get subtitle and audio Stream information of file (used for scanning IFO files)
    ''' </summary>
    ''' <returns>Mediainfo-Scanresults as MediainfoFileInfoObject</returns>
    Private Function ScanLanguage(ByVal ifoPath As String) As MediaContainers.FileInfo
        'The whole content of this function is a strip of of the "big" ScanMI function. It is used to scan IFO files of VIDEO_TS media to retrieve language info
        Dim fiOut As New MediaContainers.FileInfo

        _Handle = MediaInfo_New()

        If Master.isWindows Then
            _UseAnsi = False
        Else
            _UseAnsi = True
        End If

        Open(ifoPath)

        'Audio Scan
        Dim iAudioStreams As Integer = Count_Get(StreamKind.Audio)
        For i As Integer = 0 To iAudioStreams - 1
            Dim miAudio As New MediaContainers.Audio
            miAudio.Codec = ConvertAudioFormat(Get_(StreamKind.Audio, i, "CodecID"), Get_(StreamKind.Audio, i, "Format"),
                                           Get_(StreamKind.Audio, i, "CodecID/Hint"), Get_(StreamKind.Audio, i, "Format_Profile"))
            miAudio.Channels = ConvertAudioChannels(Get_(StreamKind.Audio, i, "Channel(s)"))
            miAudio.Bitrate = ConvertBitrate(Get_(StreamKind.Audio, i, "BitRate"))

            Dim strLanguage As String = Get_(StreamKind.Audio, i, "Language/String")
            If Not String.IsNullOrEmpty(strLanguage) Then
                miAudio.LongLanguage = strLanguage
                If Not String.IsNullOrEmpty(Localization.ISOLangGetCode3ByLang(miAudio.LongLanguage)) Then
                    miAudio.Language = Localization.ISOLangGetCode3ByLang(miAudio.LongLanguage)
                End If
            End If
            fiOut.StreamDetails.Audio.Add(miAudio)
        Next

        'Subtitle Scan
        Dim iSubtitleStreams As Integer = Count_Get(StreamKind.Text)
        For i As Integer = 0 To iSubtitleStreams - 1
            Dim miSubtitle As New MediaContainers.Subtitle
            Dim sLang As String = Get_(StreamKind.Text, i, "Language/String")
            If Not String.IsNullOrEmpty(sLang) Then
                miSubtitle.LongLanguage = sLang
                If Not String.IsNullOrEmpty(Localization.ISOLangGetCode3ByLang(miSubtitle.LongLanguage)) Then
                    miSubtitle.Language = Localization.ISOLangGetCode3ByLang(miSubtitle.LongLanguage)
                End If
            End If
            If Not String.IsNullOrEmpty(miSubtitle.Language) Then
                miSubtitle.Forced = ConvertBoolean(Get_(StreamKind.Text, i, "Forced/String"))
                fiOut.StreamDetails.Subtitle.Add(miSubtitle)
            End If
        Next

        'Video Scan
        Dim iVideoStreams As Integer = Count_Get(StreamKind.Visual)
        For i As Integer = 0 To iVideoStreams - 1
            Dim miVideo As New MediaContainers.Video
            miVideo.Duration = ConvertVideoDuration(Get_(StreamKind.Visual, i, "Duration/String3"), Get_(StreamKind.General, 0, "Duration/String3"))
            fiOut.StreamDetails.Video.Add(miVideo)
        Next

        Close()

        Return fiOut
    End Function
    ''' <summary>
    ''' Use MediaInfo.dll to get/scan audio, stubtitle and video streams of videofile
    ''' </summary>
    ''' <returns>Mediainfo-Scanresults as MediaContainers.Fileinfo</returns>
    Public Function ScanPath(ByVal strPath As String) As MediaContainers.Fileinfo
        Dim nFileInfo As New MediaContainers.Fileinfo

        If Not String.IsNullOrEmpty(strPath) Then
            _Handle = MediaInfo_New()
            _UseAnsi = Not Master.isWindows

            Open(strPath)

            'Audio Streams
            Dim intAudioStreamsCount = Count_Get(StreamKind.Audio)
            For i As Integer = 0 To intAudioStreamsCount - 1
                Dim nAudio As New MediaContainers.Audio
                'Audio General
                nAudio.Bitrate = ConvertBitrate(Get_(StreamKind.Audio, i, "BitRate"), Get_(StreamKind.Audio, i, "BitRate_Maximum"))
                nAudio.Channels = ConvertAudioChannels(Get_(StreamKind.Audio, i, "Channel(s)_Original"))
                If Not nAudio.ChannelsSpecified Then
                    nAudio.Channels = ConvertAudioChannels(Get_(StreamKind.Audio, i, "Channel(s)"))
                End If
                nAudio.Codec = ConvertAudioFormat(Get_(StreamKind.Audio, i, "CodecID"),
                                                  Get_(StreamKind.Audio, i, "Format"),
                                                  Get_(StreamKind.Audio, i, "CodecID/Hint"),
                                                  Get_(StreamKind.Audio, i, "Format_Profile"))
                'Audio Language
                Dim strLanguage = Get_(StreamKind.Audio, i, "Language/String")
                If Not String.IsNullOrEmpty(strLanguage) Then
                    nAudio.LongLanguage = strLanguage
                    nAudio.Language = Localization.ISOLangGetCode3ByLang(nAudio.LongLanguage)
                End If
                nFileInfo.StreamDetails.Audio.Add(nAudio)
            Next

            'Subtitle Streams
            Dim intSubtitleStreamsCount As Integer = Count_Get(StreamKind.Text)
            For i As Integer = 0 To intSubtitleStreamsCount - 1
                Dim nSubtitle = New MediaContainers.Subtitle
                'Subtitle Language
                nSubtitle.LongLanguage = Get_(StreamKind.Text, i, "Language/String")
                nSubtitle.Language = Localization.ISOLangGetCode3ByLang(nSubtitle.LongLanguage)
                nSubtitle.Forced = ConvertBoolean(Get_(StreamKind.Text, i, "Forced/String"))
                nFileInfo.StreamDetails.Subtitle.Add(nSubtitle)
            Next

            'Video Streams
            Dim intVideoStreamsCount As Integer = Count_Get(StreamKind.Visual)
            For i As Integer = 0 To intVideoStreamsCount - 1
                Dim nVideo As New MediaContainers.Video
                'Video General
                nVideo.Aspect = ConvertVideoAspectRatio(Get_(StreamKind.Visual, i, "DisplayAspectRatio"))
                nVideo.BitDepth = ConvertBitDepth(Get_(StreamKind.Visual, i, "BitDepth"))
                nVideo.Bitrate = ConvertBitrate(Get_(StreamKind.Visual, i, "BitRate"), Get_(StreamKind.General, i, "OverallBitRate"))
                nVideo.ChromaSubsampling = Get_(StreamKind.Visual, i, "ChromaSubsampling")
                nVideo.Codec = ConvertVideoFormat(Get_(StreamKind.Visual, i, "CodecID"),
                                                  Get_(StreamKind.Visual, i, "Format"),
                                                  Get_(StreamKind.Visual, i, "Format_Version"))
                nVideo.Duration = ConvertVideoDuration(Get_(StreamKind.Visual, i, "Duration/String3"), Get_(StreamKind.General, 0, "Duration/String3"))
                nVideo.Height = ConvertVideoWidthOrHeight(Get_(StreamKind.Visual, i, "Height"))
                nVideo.MultiViewCount = ConvertVideoMultiViewCount(Get_(StreamKind.Visual, i, "MultiView_Count"))
                nVideo.MultiViewLayout = Get_(StreamKind.Visual, i, "MultiView_Layout") 'see: http://matroska.org/technical/specs/index.html#StereoMode
                nVideo.Scantype = Get_(StreamKind.Visual, i, "ScanType")
                nVideo.Width = ConvertVideoWidthOrHeight(Get_(StreamKind.Visual, i, "Width"))
                'Video Language
                Dim strLanguage = Get_(StreamKind.Visual, i, "Language/String")
                If Not String.IsNullOrEmpty(strLanguage) Then
                    nVideo.LongLanguage = strLanguage
                    nVideo.Language = Localization.ISOLangGetCode3ByLang(nVideo.LongLanguage)
                End If
                nFileInfo.StreamDetails.Video.Add(nVideo)
            Next
            Close()
        End If
        Return nFileInfo
    End Function
    ''' 
    ''' <summary>
    ''' Used to set the paths of IFO/VOB (DVD) or M2ts/CLPI (BLURAY) files for Mediainfo-Scanning
    ''' </summary>
    ''' <param name="path">The <c>String</c>, the path to videofile (VOB/M2TS)</param>
    ''' <param name="fiIFO"><c>MediaInfo.FileInfo</c> contains the scanned Mediainfo IFO information</param>
    ''' <param name="alternativeIFOFile"><c>String</c> path to second biggest IFO File of video - alternative to default biggest IFO file</param>
    ''' <param name="ISO"><c>Boolean</c> Source: .ISO file =True, if not = False</param>
    ''' <remarks>
    ''' 2014/07/05 Cocotus - Method created to remove duplicate code and make ScanMi function easier to read
    ''' </remarks>
    Private Sub SetMediaInfoScanPaths(ByRef path As String, ByRef fiIFO As MediaContainers.FileInfo, ByRef alternativeIFOFile As String, ByVal ISO As Boolean)
        Try
            If path.Contains("VIDEO_TS") Then
                'DVD structure

                Dim di As New DirectoryInfo(Directory.GetParent(path).FullName)
                If ISO Then
                    'ie. path = driveletter & "VIDEO_TS"
                    di = New DirectoryInfo(path)
                End If


                'Biggest IFO File! -> Get Languages out of IFO and Bitrate data out of biggest VOB file!
                Dim myFilesIFO = From file In di.GetFiles("VTS*.IFO")
                                 Order By file.Length
                                 Select file.FullName
                If Not myFilesIFO Is Nothing AndAlso myFilesIFO.Count > 0 Then
                    alternativeIFOFile = myFilesIFO(myFilesIFO.Count - 2)
                    fiIFO = ScanLanguage(myFilesIFO.Last)
                End If

                'Biggest VOB File! -> Get Languages out of IFO and Bitrate data out of biggest VOB file!
                If Not myFilesIFO Is Nothing AndAlso myFilesIFO.Count > 0 AndAlso myFilesIFO.Last.Length > 6 Then

                    Dim myFiles = From file In di.GetFiles(IO.Path.GetFileName(myFilesIFO.Last).Substring(0, IO.Path.GetFileName(myFilesIFO.Last).Length - 6) & "*.VOB")
                                  Order By file.Length
                                  Select file.FullName
                    If Not myFiles Is Nothing AndAlso myFiles.Count > 0 Then
                        path = myFiles.Last
                    Else
                        myFiles = From file In di.GetFiles("VTS*.VOB")
                                  Order By file.Length
                                  Select file.FullName
                        path = myFiles.Last
                    End If
                Else
                    Dim myFiles = From file In di.GetFiles("VTS*.VOB")
                                  Order By file.Length
                                  Select file.FullName
                    path = myFiles.Last
                End If

                'Bluray
            Else

                'looking at the largest m2ts file within the \BDMV\STREAM folder
                Dim di As New DirectoryInfo(Directory.GetParent(path).FullName)
                If ISO Then
                    'ie. path = driveletter & "VIDEO_TS"
                    di = New DirectoryInfo(path)
                End If
                Dim myFiles = From file In di.GetFiles("*.m2ts")
                              Order By file.Length
                              Select file.Name

                If Not myFiles Is Nothing AndAlso myFiles.Count > 0 Then
                    'Biggest file!
                    If ISO Then
                        path = path & "\" & myFiles.Last
                    Else
                        path = Directory.GetParent(path).FullName & "\" & myFiles.Last
                    End If

                End If

                If myFiles.Last.Length > 5 Then
                    Dim ISOSubtitleScanFile As String = myFiles.Last.Substring(0, myFiles.Last.Length - 5) & ".clpi"
                    Dim clipinfpath As String = Directory.GetParent(path).FullName.Replace("STREAM", "CLIPINF")
                    If File.Exists(clipinfpath & "\" & ISOSubtitleScanFile) Then
                        fiIFO = ScanLanguage(clipinfpath & "\" & ISOSubtitleScanFile)
                    End If
                End If

            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub


#End Region 'Methods 

End Class