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
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions

Public Class MediaInfo

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _Handle As IntPtr

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

#Region "MediaInfo.dll"

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

#End Region 'MediaInfo.dll

#Region "Methods"

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

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
    Private Function Convert_AudioChannels(ByVal channelinfo As String) As Integer
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

    Private Function Convert_AudioFormat(ByVal codecID As String,
                                         ByVal format As String,
                                         ByVal codecHint As String,
                                         ByVal profile As String,
                                         ByVal additionalFeatures As String) As String
        If format.ToLower.Contains("dts") AndAlso (profile.ToLower.Contains("hra / core") OrElse profile.ToLower.Contains("ma / core")) Then
            Return profile
        ElseIf additionalFeatures.ToLower.Contains("xll") Then
            Return String.Format("{0} {1}", format, additionalFeatures).Trim
        ElseIf format.ToLower.Contains("atmos / truehd") Then
            Return format
        ElseIf profile.ToLower.Contains("truehd+atmos") Then
            Return profile
        ElseIf profile.ToLower.Contains("e-ac-3+atmos") Then
            Return "e-ac-3+atmos"
        ElseIf Not String.IsNullOrEmpty(codecID) AndAlso Not Integer.TryParse(codecID, 0) AndAlso Not codecID.ToLower.Contains("a_pcm") AndAlso Not codecID.Contains("00001000-0000-0100-8000-00AA00389B71") Then
            Return codecID
        ElseIf Not String.IsNullOrEmpty(codecHint) Then
            Return codecHint
        ElseIf format.ToLower.Contains("mpeg") AndAlso Not String.IsNullOrEmpty(profile) Then
            Return String.Concat("mp", profile.Replace("Layer", String.Empty).Trim).Trim
        Else
            Return format
        End If
    End Function

    Private Function Convert_BitDepth(ByVal bitdepth As String) As Integer
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
    Private Function Convert_Bitrate(ByVal bitrate As String, Optional ByVal alternateBitrate As String = "") As Integer
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
    Private Function Convert_Boolean(ByVal textYesNo As String) As Boolean
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

    Private Function Convert_VideoAspectRatio(ByVal ratio As String) As Double
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
    Private Function Convert_VideoDuration(ByVal duration As String, Optional generalDuration As String = "") As Integer
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

    Private Function Convert_VideoFormat(ByVal sCodecID As String,
                                         ByVal sFormat As String,
                                         ByVal sVersion As String) As String
        If Not String.IsNullOrEmpty(sCodecID) AndAlso Not Integer.TryParse(sCodecID, 0) Then
            Return sCodecID
        ElseIf sFormat.ToLower.Contains("mpeg") AndAlso Not String.IsNullOrEmpty(sVersion) Then
            Return String.Concat("mpeg", sVersion.Replace("Version", String.Empty).Trim, "video").Trim
        Else
            Return sFormat
        End If
    End Function

    Private Function Convert_VideoMultiViewCount(ByVal multiViewCount As String) As Integer
        Dim iCount As Integer
        Integer.TryParse(multiViewCount, iCount)
        Return iCount
    End Function

    Public Shared Function Convert_VideoMultiViewLayout_To_StereoMode(ByVal sFormat As String) As String
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

    Private Function Convert_VideoWidthOrHeight(ByVal widthOrHeight As String) As Integer
        Dim iSize As Integer
        Integer.TryParse(widthOrHeight, iSize)
        Return iSize
    End Function

    Private Function Convert_VideoStereoToShort(ByVal sFormat As String) As String
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
        Return Marshal.PtrToStringUni(MediaInfo_Get(_Handle, CType(streamKind, UIntPtr), CType(streamNumber, UIntPtr), parameter, CType(kindOfInfo, UIntPtr), CType(kindOfSearch, UIntPtr)))
    End Function

    Private Sub Open(ByVal path As String)
        MediaInfo_Open(_Handle, path)
    End Sub
    ''' <summary>
    ''' Reads the metadata with MediaInfo.dll from the specified file
    ''' </summary>
    ''' <param name="path">full path to the video file</param>
    ''' <returns></returns>
    Public Function ScanPath(ByVal path As String) As MediaContainers.Fileinfo
        Dim nFileInfo As New MediaContainers.Fileinfo

        If Not String.IsNullOrEmpty(path) Then
            _Handle = MediaInfo_New()

            Open(path)

            'Audio Streams
            Dim intAudioStreamsCount = Count_Get(StreamKind.Audio)
            For i As Integer = 0 To intAudioStreamsCount - 1
                Dim nAudio As New MediaContainers.Audio
                'Audio General
                nAudio.BitDepth = Convert_BitDepth(Get_(StreamKind.Audio, i, "BitDepth"))
                nAudio.Bitrate = Convert_Bitrate(Get_(StreamKind.Audio, i, "BitRate"), Get_(StreamKind.Audio, i, "BitRate_Maximum"))
                nAudio.Channels = Convert_AudioChannels(Get_(StreamKind.Audio, i, "Channel(s)_Original"))
                If Not nAudio.ChannelsSpecified Then
                    nAudio.Channels = Convert_AudioChannels(Get_(StreamKind.Audio, i, "Channel(s)"))
                End If
                nAudio.Codec = Get_(StreamKind.Audio, i, "Format/String").ToLower
                'Audio Language
                Dim strLanguage = Get_(StreamKind.Audio, i, "Language/String")
                If Not String.IsNullOrEmpty(strLanguage) Then
                    nAudio.LongLanguage = strLanguage
                    nAudio.Language = Localization.Languages.Get_Alpha3_B_By_Name(nAudio.LongLanguage)
                End If
                nFileInfo.StreamDetails.Audio.Add(nAudio)
            Next

            'Subtitle Streams
            Dim intSubtitleStreamsCount As Integer = Count_Get(StreamKind.Text)
            For i As Integer = 0 To intSubtitleStreamsCount - 1
                Dim nSubtitle = New MediaContainers.Subtitle
                'Subtitle Language
                nSubtitle.LongLanguage = Get_(StreamKind.Text, i, "Language/String")
                If Not String.IsNullOrEmpty(nSubtitle.LongLanguage) Then
                    nSubtitle.Language = Localization.Languages.Get_Alpha3_B_By_Name(nSubtitle.LongLanguage)
                End If
                nSubtitle.Forced = Convert_Boolean(Get_(StreamKind.Text, i, "Forced/String"))
                nFileInfo.StreamDetails.Subtitle.Add(nSubtitle)
            Next

            'Video Streams
            Dim intVideoStreamsCount As Integer = Count_Get(StreamKind.Visual)
            For i As Integer = 0 To intVideoStreamsCount - 1
                Dim nVideo As New MediaContainers.Video
                'Video General
                nVideo.Aspect = Convert_VideoAspectRatio(Get_(StreamKind.Visual, i, "DisplayAspectRatio"))
                nVideo.BitDepth = Convert_BitDepth(Get_(StreamKind.Visual, i, "BitDepth"))
                nVideo.Bitrate = Convert_Bitrate(Get_(StreamKind.Visual, i, "BitRate"), Get_(StreamKind.General, i, "OverallBitRate"))
                nVideo.ChromaSubsampling = Get_(StreamKind.Visual, i, "ChromaSubsampling")
                nVideo.Codec = Convert_VideoFormat(Get_(StreamKind.Visual, i, "CodecID"),
                                                   Get_(StreamKind.Visual, i, "Format"),
                                                   Get_(StreamKind.Visual, i, "Format_Version")).ToLower
                nVideo.Duration = Convert_VideoDuration(Get_(StreamKind.Visual, i, "Duration/String3"), Get_(StreamKind.General, 0, "Duration/String3"))
                nVideo.Height = Convert_VideoWidthOrHeight(Get_(StreamKind.Visual, i, "Height"))
                nVideo.MultiViewCount = Convert_VideoMultiViewCount(Get_(StreamKind.Visual, i, "MultiView_Count"))
                nVideo.MultiViewLayout = Get_(StreamKind.Visual, i, "MultiView_Layout") 'see: http://matroska.org/technical/specs/index.html#StereoMode
                nVideo.Scantype = Get_(StreamKind.Visual, i, "ScanType")
                nVideo.Width = Convert_VideoWidthOrHeight(Get_(StreamKind.Visual, i, "Width"))
                'Video Language
                Dim strLanguage = Get_(StreamKind.Visual, i, "Language/String")
                If Not String.IsNullOrEmpty(strLanguage) Then
                    nVideo.LongLanguage = strLanguage
                    nVideo.Language = Localization.Languages.Get_Alpha3_B_By_Name(nVideo.LongLanguage)
                End If
                nFileInfo.StreamDetails.Video.Add(nVideo)
            Next
            Close()
        End If
        Return nFileInfo
    End Function

#End Region 'Methods 

End Class