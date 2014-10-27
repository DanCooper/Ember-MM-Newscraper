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

Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports NLog

<Serializable()> _
Public Class MediaInfo

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private Handle As IntPtr
    Private UseAnsi As Boolean

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

    Public Shared Function ApplyDefaults(ByVal ext As String) As Fileinfo
        Dim fi As New Fileinfo
        For Each m As Settings.MetadataPerType In Master.eSettings.MovieMetadataPerFileType
            If m.FileType = ext Then
                fi = m.MetaData
                Return fi
            End If
        Next
        Return Nothing
    End Function

    Public Shared Function ApplyTVDefaults(ByVal ext As String) As Fileinfo
        Dim fi As New Fileinfo
        For Each m As Settings.MetadataPerType In Master.eSettings.TVMetadataPerFileType
            If m.FileType = ext Then
                fi = m.MetaData
                Return fi
            End If
        Next
        Return Nothing
    End Function

    Public Shared Sub UpdateMediaInfo(ByRef miMovie As Structures.DBMovie)
        Try
            'DON'T clear it out
            'miMovie.Movie.FileInfo = New MediaInfo.Fileinfo
            Dim tinfo = New MediaInfo.Fileinfo

            Dim pExt As String = Path.GetExtension(miMovie.Filename).ToLower
            If Master.CanScanDiscImage OrElse Not (pExt = ".iso" OrElse _
               pExt = ".img" OrElse pExt = ".bin" OrElse pExt = ".cue" OrElse pExt = ".nrg" OrElse pExt = ".rar") Then
                Dim MI As New MediaInfo
                'MI.GetMIFromPath(miMovie.Movie.FileInfo, miMovie.Filename, False)
                MI.GetMIFromPath(tinfo, miMovie.Filename, False)
                If tinfo.StreamDetails.Video.Count > 0 OrElse tinfo.StreamDetails.Audio.Count > 0 OrElse tinfo.StreamDetails.Subtitle.Count > 0 Then
                    ' overwrite only if it get something from Mediainfo


                    If Master.eSettings.MovieLockLanguageV Then
                        Try
                            'sets old language setting if setting is enabled (lock language)
                            'First make sure that there is no completely new video source scanned of the movie --> if so (i.e. more streams) then update!
                            If tinfo.StreamDetails.Video.Count = miMovie.Movie.FileInfo.StreamDetails.Video.Count Then
                                For i = 0 To tinfo.StreamDetails.Video.Count - 1
                                    'only preserve if language tag is filled --> else update!
                                    If Not String.IsNullOrEmpty(miMovie.Movie.FileInfo.StreamDetails.Video.Item(i).LongLanguage) Then
                                        tinfo.StreamDetails.Video.Item(i).Language = miMovie.Movie.FileInfo.StreamDetails.Video.Item(i).Language
                                        tinfo.StreamDetails.Video.Item(i).LongLanguage = miMovie.Movie.FileInfo.StreamDetails.Video.Item(i).LongLanguage
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                    If Master.eSettings.MovieLockLanguageA Then
                        Try
                            'sets old language setting if setting is enabled (lock language)
                            'First make sure that there is no completely new audio source scanned of the movie --> if so (i.e. more streams) then update!
                            If tinfo.StreamDetails.Audio.Count = miMovie.Movie.FileInfo.StreamDetails.Audio.Count Then
                                For i = 0 To tinfo.StreamDetails.Audio.Count - 1
                                    'only preserve if language tag is filled --> else update!
                                    If Not String.IsNullOrEmpty(miMovie.Movie.FileInfo.StreamDetails.Audio.Item(i).LongLanguage) Then
                                        tinfo.StreamDetails.Audio.Item(i).Language = miMovie.Movie.FileInfo.StreamDetails.Audio.Item(i).Language
                                        tinfo.StreamDetails.Audio.Item(i).LongLanguage = miMovie.Movie.FileInfo.StreamDetails.Audio.Item(i).LongLanguage
                                    End If
                                Next
                            End If

                        Catch ex As Exception

                        End Try
                    End If
                    miMovie.Movie.FileInfo = tinfo
                End If
                If miMovie.Movie.FileInfo.StreamDetails.Video.Count > 0 AndAlso Master.eSettings.MovieScraperUseMDDuration Then
                    Dim tVid As MediaInfo.Video = NFO.GetBestVideo(miMovie.Movie.FileInfo)
                    'cocotus 29/02/2014, Added check to only save Runtime in nfo/moviedb if scraped Runtime <> 0! (=Error during Mediainfo Scan)
                    If Not String.IsNullOrEmpty(tVid.Duration) AndAlso Not tVid.Duration.Trim = "0" Then
                        miMovie.Movie.Runtime = MediaInfo.FormatDuration(tVid.Duration, Master.eSettings.MovieScraperDurationRuntimeFormat)
                    End If
                End If
                MI = Nothing
            End If
            If miMovie.Movie.FileInfo.StreamDetails.Video.Count = 0 AndAlso miMovie.Movie.FileInfo.StreamDetails.Audio.Count = 0 AndAlso miMovie.Movie.FileInfo.StreamDetails.Subtitle.Count = 0 Then
                Dim _mi As MediaInfo.Fileinfo
                _mi = MediaInfo.ApplyDefaults(pExt)
                If Not _mi Is Nothing Then miMovie.Movie.FileInfo = _mi
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Public Shared Sub UpdateTVMediaInfo(ByRef miTV As Structures.DBTV)
        Try
            'clear it out
            miTV.TVEp.FileInfo = New MediaInfo.Fileinfo

            Dim pExt As String = Path.GetExtension(miTV.Filename).ToLower
            If Master.CanScanDiscImage OrElse Not (pExt = ".iso" OrElse _
               pExt = ".img" OrElse pExt = ".bin" OrElse pExt = ".cue" OrElse pExt = ".nrg" OrElse pExt = ".rar") Then
                Dim MI As New MediaInfo
                MI.GetMIFromPath(miTV.TVEp.FileInfo, miTV.Filename, True)
                MI = Nothing
            End If
            If miTV.TVEp.FileInfo.StreamDetails.Video.Count = 0 AndAlso miTV.TVEp.FileInfo.StreamDetails.Audio.Count = 0 AndAlso miTV.TVEp.FileInfo.StreamDetails.Subtitle.Count = 0 Then
                Dim _mi As MediaInfo.Fileinfo
                _mi = MediaInfo.ApplyTVDefaults(pExt)
                If Not _mi Is Nothing Then miTV.TVEp.FileInfo = _mi
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Public Sub GetMIFromPath(ByRef fiInfo As Fileinfo, ByVal sPath As String, ByVal ForTV As Boolean)
        If Not String.IsNullOrEmpty(sPath) AndAlso File.Exists(sPath) Then
            Dim sExt As String = Path.GetExtension(sPath).ToLower
            Dim fiOut As New Fileinfo
            Dim miVideo As New Video
            Dim miAudio As New Audio
            Dim miSubtitle As New Subtitle
            Dim AudioStreams As Integer
            Dim SubtitleStreams As Integer
            Dim aLang As String = String.Empty
            Dim sLang As String = String.Empty
            Dim cDVD As New DVD

            Dim ifoVideo(2) As String
            Dim ifoAudio(2) As String

            If Master.eSettings.MovieScraperMetaDataIFOScan AndAlso (sExt = ".ifo" OrElse sExt = ".vob" OrElse sExt = ".bup") AndAlso cDVD.fctOpenIFOFile(sPath) Then
                Try
                    ifoVideo = cDVD.GetIFOVideo
                    Dim vRes() As String = ifoVideo(1).Split(Convert.ToChar("x"))
                    miVideo.Width = vRes(0)
                    miVideo.Height = vRes(1)
                    miVideo.Codec = ifoVideo(0)
                    miVideo.Duration = cDVD.GetProgramChainPlayBackTime(1)
                    miVideo.Aspect = ifoVideo(2)

                    With miVideo
                        If Not String.IsNullOrEmpty(.Codec) OrElse Not String.IsNullOrEmpty(.Duration) OrElse Not String.IsNullOrEmpty(.Aspect) OrElse _
                        Not String.IsNullOrEmpty(.Height) OrElse Not String.IsNullOrEmpty(.Width) Then
                            fiOut.StreamDetails.Video.Add(miVideo)
                        End If
                    End With

                    AudioStreams = cDVD.GetIFOAudioNumberOfTracks
                    For a As Integer = 1 To AudioStreams
                        miAudio = New MediaInfo.Audio
                        ifoAudio = cDVD.GetIFOAudio(a)
                        miAudio.Codec = ifoAudio(0)
                        miAudio.Channels = ifoAudio(2)
                        aLang = ifoAudio(1)
                        If Not String.IsNullOrEmpty(aLang) Then
                            miAudio.LongLanguage = aLang
                            If Localization.ISOLangGetCode3ByLang(miAudio.LongLanguage) <> "" Then
                                miAudio.Language = Localization.ISOLangGetCode3ByLang(miAudio.LongLanguage)
                            End If
                        End If
                        With miAudio
                            If Not String.IsNullOrEmpty(.Codec) OrElse Not String.IsNullOrEmpty(.Channels) OrElse Not String.IsNullOrEmpty(.Language) Then
                                fiOut.StreamDetails.Audio.Add(miAudio)
                            End If
                        End With
                    Next

                    SubtitleStreams = cDVD.GetIFOSubPicNumberOf
                    For s As Integer = 1 To SubtitleStreams
                        miSubtitle = New MediaInfo.Subtitle
                        sLang = cDVD.GetIFOSubPic(s)
                        If Not String.IsNullOrEmpty(sLang) Then
                            miSubtitle.LongLanguage = sLang
                            If Localization.ISOLangGetCode3ByLang(miSubtitle.LongLanguage) <> "" Then
                                miSubtitle.Language = Localization.ISOLangGetCode3ByLang(miSubtitle.LongLanguage)
                            End If
                            If Not String.IsNullOrEmpty(miSubtitle.Language) Then
                                'miSubtitle.SubsForced = Not supported(?)
                                miSubtitle.SubsType = "Embedded"
                                fiOut.StreamDetails.Subtitle.Add(miSubtitle)
                            End If
                        End If
                    Next

                    cDVD.Close()
                    cDVD = Nothing

                    fiInfo = fiOut
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name,ex)
                End Try

                'cocotus 20140118 For more accurate metadata scanning of BLURAY/DVD images use improved mediainfo scanning (ScanMI-function) -> don't hop in this branch!! 
                '  ElseIf StringUtils.IsStacked(Path.GetFileNameWithoutExtension(sPath), True) OrElse FileUtils.Common.isVideoTS(sPath) OrElse FileUtils.Common.isBDRip(sPath) Then
            ElseIf StringUtils.IsStacked(Path.GetFileNameWithoutExtension(sPath), True) Then
                Try
                    Dim oFile As String = StringUtils.CleanStackingMarkers(sPath, False)
                    Dim sFile As New List(Of String)
                    Dim bIsVTS As Boolean = False

                    If sExt = ".ifo" OrElse sExt = ".bup" OrElse sExt = ".vob" Then
                        bIsVTS = True
                    End If

                    If bIsVTS Then
                        Try
                            sFile.AddRange(Directory.GetFiles(Directory.GetParent(sPath).FullName, "VTS*.VOB"))
                        Catch
                        End Try
                    ElseIf sExt = ".m2ts" Then
                        Try
                            sFile.AddRange(Directory.GetFiles(Directory.GetParent(sPath).FullName, "*.m2ts"))
                        Catch
                        End Try
                    Else
                        Try
                            sFile.AddRange(Directory.GetFiles(Directory.GetParent(sPath).FullName, StringUtils.CleanStackingMarkers(Path.GetFileName(sPath), True)))
                        Catch
                        End Try
                    End If

                    Dim TotalDur As Integer = 0
                    Dim tInfo As New Fileinfo
                    Dim tVideo As New Video
                    Dim tAudio As New Audio

                    miVideo.Width = "0"
                    miAudio.Channels = "0"

                    For Each File As String In sFile
                        'make sure the file is actually part of the stack
                        'handles movie.cd1.ext, movie.cd2.ext and movie.extras.ext
                        'disregards movie.extras.ext in this case
                        If bIsVTS OrElse (oFile = StringUtils.CleanStackingMarkers(File, False)) Then
                            tInfo = ScanMI(File)

                            tVideo = NFO.GetBestVideo(tInfo)
                            tAudio = NFO.GetBestAudio(tInfo, ForTV)

                            If String.IsNullOrEmpty(miVideo.Codec) OrElse Not String.IsNullOrEmpty(tVideo.Codec) Then
                                If Not String.IsNullOrEmpty(tVideo.Width) AndAlso Convert.ToInt32(tVideo.Width) >= Convert.ToInt32(miVideo.Width) Then
                                    miVideo = tVideo
                                End If
                            End If

                            If String.IsNullOrEmpty(miAudio.Codec) OrElse Not String.IsNullOrEmpty(tAudio.Codec) Then
                                If Not String.IsNullOrEmpty(tAudio.Channels) AndAlso Convert.ToInt32(tAudio.Channels) >= Convert.ToInt32(miAudio.Channels) Then
                                    miAudio = tAudio
                                End If
                            End If

                            If Not String.IsNullOrEmpty(tVideo.Duration) Then TotalDur += Convert.ToInt32(DurationToSeconds(tVideo.Duration, False))

                            For Each sSub As Subtitle In tInfo.StreamDetails.Subtitle
                                If Not fiOut.StreamDetails.Subtitle.Contains(sSub) Then
                                    fiOut.StreamDetails.Subtitle.Add(sSub)
                                End If
                            Next
                        End If
                    Next

                    fiOut.StreamDetails.Video.Add(miVideo)
                    fiOut.StreamDetails.Audio.Add(miAudio)
                    fiOut.StreamDetails.Video(0).Duration = DurationToSeconds(TotalDur.ToString, True)

                    fiInfo = fiOut
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name,ex)
                End Try
            Else
                fiInfo = ScanMI(sPath)
            End If

            'finally go through all the video streams and reformat the duration
            'we do this afterwards because of scanning mediainfo from stacked files... we need total
            'duration so we need to keep a consistent duration format while scanning
            'it's easier to format at the end so we don't need to bother with creating a generic
            'conversion routine
            If Not IsNothing(fiInfo.StreamDetails) AndAlso fiInfo.StreamDetails.Video.Count > 0 Then
                For Each tVid As Video In fiInfo.StreamDetails.Video
                    tVid.Duration = DurationToSeconds(tVid.Duration, False)
                Next
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    <DllImport("Bin\MediaInfo.DLL")> _
    Private Shared Function MediaInfoA_Get(ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As UIntPtr, ByVal Parameter As IntPtr, ByVal KindOfInfo As UIntPtr, ByVal KindOfSearch As UIntPtr) As IntPtr
    End Function

    <DllImport("Bin\MediaInfo.DLL")> _
    Private Shared Function MediaInfoA_Open(ByVal Handle As IntPtr, ByVal FileName As IntPtr) As UIntPtr
    End Function

    <DllImport("Bin\MediaInfo.DLL")> _
    Private Shared Sub MediaInfo_Close(ByVal Handle As IntPtr)
    End Sub

    <DllImport("Bin\MediaInfo.DLL")> _
    Private Shared Function MediaInfo_Count_Get(ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As IntPtr) As Integer
    End Function

    <DllImport("Bin\MediaInfo.DLL")> _
    Private Shared Sub MediaInfo_Delete(ByVal Handle As IntPtr)
    End Sub

    <DllImport("Bin\MediaInfo.DLL")> _
    Private Shared Function MediaInfo_Get(ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As UIntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByVal Parameter As String, ByVal KindOfInfo As UIntPtr, ByVal KindOfSearch As UIntPtr) As IntPtr
    End Function

    <DllImport("Bin\MediaInfo.DLL")> _
    Private Shared Function MediaInfo_New() As IntPtr
    End Function

    <DllImport("Bin\MediaInfo.DLL")> _
    Private Shared Function MediaInfo_Open(ByVal Handle As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByVal FileName As String) As UIntPtr
    End Function

    Private Sub Close()
        MediaInfo_Close(Handle)
        MediaInfo_Delete(Handle)
        Handle = Nothing
    End Sub

    Private Function ConvertAFormat(ByVal sFormat As String, Optional ByVal sProfile As String = "") As String
        If Not String.IsNullOrEmpty(sFormat) Then
            Select Case sFormat.ToLower
                Case "dts", "a_dts"
                    If sProfile.ToUpper.Contains("MA") Then
                        sFormat = "dtshd_ma" 'Master Audio
                    ElseIf sProfile.ToUpper.Contains("HRA") Then
                        sFormat = "dtshd_hra" 'high resolution
                    End If
            End Select
            If sFormat.ToLower.Contains("truehd") Then
                sFormat = "truehd" 'Dolby TrueHD
            ElseIf sFormat.ToLower.Contains("vorbis") Then
                sFormat = "vorbis" 'Vorbis
            ElseIf sFormat.ToLower.Contains("eac3") Then
                sFormat = "dolbydigital" 'EAC3
            ElseIf sFormat.ToLower.Contains("flac") Then
                sFormat = "flac" 'flac
            End If
            Return clsAdvancedSettings.GetSetting(String.Concat("AudioFormatConvert:", sFormat.ToLower), sFormat.ToLower)
            'Return sFormat
            'cocotus, 2013/02 Fix2 for DTS Scan
        ElseIf Not String.IsNullOrEmpty(sProfile) Then
            If sProfile.ToUpper.Contains("MA") Then
                sFormat = "dtshd_ma" 'Master Audio
            ElseIf sProfile.ToUpper.Contains("HRA") Then
                sFormat = "dtshd_hra" 'high resolution
            ElseIf sProfile.ToLower.Contains("truehd") Then
                sFormat = "truehd" 'Dolby TrueHD
            ElseIf sFormat.ToLower.Contains("vorbis") Then
                sFormat = "vorbis" 'Vorbis
            ElseIf sFormat.ToLower.Contains("eac3") Then
                sFormat = "dolbydigital" 'EAC3
            ElseIf sFormat.ToLower.Contains("flac") Then
                sFormat = "flac" 'flac
            End If
            Return clsAdvancedSettings.GetSetting(String.Concat("AudioFormatConvert:", sFormat.ToLower), sFormat.ToLower)
            'cocotus end
        Else
            Return String.Empty
        End If
    End Function

    Private Function ConvertVFormat(ByVal sFormat As String, Optional ByVal sModifier As String = "") As String
        If Not String.IsNullOrEmpty(sFormat) Then
            Dim tFormat As String = sFormat.ToLower
            Dim myconversions As New List(Of AdvancedSettingsComplexSettingsTableItem)
            myconversions = clsAdvancedSettings.GetComplexSetting("VideoFormatConverts")
            If Not myconversions Is Nothing Then
                For Each k In myconversions
                    If Regex.IsMatch(tFormat.ToLower, k.Name) Then
                        Return k.Value
                    End If
                Next
                Return tFormat.ToLower
                'if VideoFormatConvert is not avalaible as complex setting (old)
            Else
                Return clsAdvancedSettings.GetSetting(String.Concat("VideoFormatConvert:", tFormat.ToLower), tFormat.ToLower)
            End If
        Else
            Return String.Empty
        End If
    End Function

    Public Shared Function ConvertVStereoMode(ByVal sFormat As String) As String
        If Not String.IsNullOrEmpty(sFormat) Then
            Dim tFormat As String = String.Empty
            Select Case sFormat.ToLower
                Case "side by side (left eye first)"
                    tFormat = "left_right"
                Case "top-bottom (right eye first)"
                    tFormat = "bottom_top"
                Case "top-bottom (left eye first)"
                    tFormat = "bottom_top"
                Case "checkboard (right eye first)"
                    tFormat = "checkerboard_rl"
                Case "checkboard (left eye first)"
                    tFormat = "checkerboard_lr"
                Case "row interleaved (right eye first)"
                    tFormat = "row_interleaved_rl"
                Case "row interleaved (left eye first)"
                    tFormat = "row_interleaved_lr"
                Case "column interleaved (right eye first)"
                    tFormat = "col_interleaved_rl"
                Case "column interleaved (left eye first)"
                    tFormat = "col_interleaved_lr"
                Case "anaglyph (cyan/red)"
                    tFormat = "anaglyph_cyan_red"
                Case "side by side (right eye first)"
                    tFormat = "right_left"
                Case "anaglyph (green/magenta)"
                    tFormat = "anaglyph_green_magenta"
                Case "both eyes laced in one block (left eye first)"
                    tFormat = "block_lr"
                Case "both eyes laced in one block (right eye first)"
                    tFormat = "block_rl"
            End Select

            Return tFormat
        Else
            Return String.Empty
        End If
    End Function

    Private Function Count_Get(ByVal StreamKind As StreamKind, Optional ByVal StreamNumber As UInteger = UInteger.MaxValue) As Integer
        If StreamNumber = UInteger.MaxValue Then
            Return MediaInfo_Count_Get(Handle, CType(StreamKind, UIntPtr), CType(-1, IntPtr))
        Else
            Return MediaInfo_Count_Get(Handle, CType(StreamKind, UIntPtr), CType(StreamNumber, IntPtr))
        End If
    End Function

    Public Shared Function DurationToSeconds(ByVal Duration As String, ByVal Reverse As Boolean) As String
        If Not String.IsNullOrEmpty(Duration) Then
            If Reverse Then
                Dim ts As New TimeSpan(0, 0, Convert.ToInt32(Duration))
                Return String.Format("{0}h {1}min {2}s", ts.Hours, ts.Minutes, ts.Seconds)
            Else
                Dim sDuration As Match = Regex.Match(Duration, "(([0-9]+)h)?\s?(([0-9]+)mn)?\s?(([0-9]+)s)?")
                Dim sHour As Integer = If(Not String.IsNullOrEmpty(sDuration.Groups(2).Value), (Convert.ToInt32(sDuration.Groups(2).Value)), 0)
                Dim sMin As Integer = If(Not String.IsNullOrEmpty(sDuration.Groups(4).Value), (Convert.ToInt32(sDuration.Groups(4).Value)), 0)
                Dim sSec As Integer = If(Not String.IsNullOrEmpty(sDuration.Groups(6).Value), (Convert.ToInt32(sDuration.Groups(6).Value)), 0)
                Return ((sHour * 60 * 60) + (sMin * 60) + sSec).ToString
            End If
        End If
        Return "0"
    End Function

    Private Function Get_(ByVal StreamKind As StreamKind, ByVal StreamNumber As Integer, ByVal Parameter As String, Optional ByVal KindOfInfo As InfoKind = InfoKind.Text, Optional ByVal KindOfSearch As InfoKind = InfoKind.Name) As String
        If UseAnsi Then
            Dim Parameter_Ptr As IntPtr = Marshal.StringToHGlobalAnsi(Parameter)
            Dim ToReturn As String = Marshal.PtrToStringAnsi(MediaInfoA_Get(Handle, CType(StreamKind, UIntPtr), CType(StreamNumber, UIntPtr), Parameter_Ptr, CType(KindOfInfo, UIntPtr), CType(KindOfSearch, UIntPtr)))
            Marshal.FreeHGlobal(Parameter_Ptr)
            Return ToReturn
        Else
            Return Marshal.PtrToStringUni(MediaInfo_Get(Handle, CType(StreamKind, UIntPtr), CType(StreamNumber, UIntPtr), Parameter, CType(KindOfInfo, UIntPtr), CType(KindOfSearch, UIntPtr)))
        End If
    End Function

    Private Sub Open(ByVal FileName As String)
        If UseAnsi Then
            Dim FileName_Ptr As IntPtr = Marshal.StringToHGlobalAnsi(FileName)
            MediaInfoA_Open(Handle, FileName_Ptr)
            Marshal.FreeHGlobal(FileName_Ptr)
        Else
            MediaInfo_Open(Handle, FileName)
        End If
    End Sub

    ''' <summary>
    ''' Use MediaInfo.dll Scan to get subtitle and audio Stream information of file (used for scanning IFO files)
    ''' </summary>
    ''' <returns>Mediainfo-Scanresults as MediainfoFileInfoObject</returns>
    Private Function ScanLanguage(ByVal IFOPath As String) As Fileinfo
        'The whole content of this function is a strip of of the "big" ScanMI function. It is used to scan IFO files of VIDEO_TS media to retrieve language info
        Dim fiOut As New Fileinfo

        Me.Handle = MediaInfo_New()

        If Master.isWindows Then
            UseAnsi = False
        Else
            UseAnsi = True
        End If

        Me.Open(IFOPath)


        'Audio Scan
        Dim AudioStreams As Integer
        AudioStreams = Me.Count_Get(StreamKind.Audio)
        Dim miAudio As New Audio
        Dim aLang As String = String.Empty
        Dim a_Profile As String = String.Empty

        For a As Integer = 0 To AudioStreams - 1
            miAudio = New Audio

            a_Profile = Me.Get_(StreamKind.Audio, a, "Format_Profile")
            miAudio.Codec = ConvertAFormat(Me.Get_(StreamKind.Audio, a, "CodecID/Hint"), a_Profile)
            If String.IsNullOrEmpty(miAudio.Codec) OrElse IsNumeric(miAudio.Codec) Then
                miAudio.Codec = ConvertAFormat(Me.Get_(StreamKind.Audio, a, "CodecID"), a_Profile)
                miAudio.Codec = If(IsNumeric(miAudio.Codec) OrElse String.IsNullOrEmpty(miAudio.Codec), ConvertAFormat(Me.Get_(StreamKind.Audio, a, "Format"), a_Profile), miAudio.Codec)
            End If
            miAudio.Channels = FormatAudioChannel(Me.Get_(StreamKind.Audio, a, "Channel(s)"))

            'cocotus, 2013/02 Added support for new MediaInfo-fields
            'Audio-Bitrate
            miAudio.Bitrate = FormatBitrate(Me.Get_(StreamKind.Audio, a, "BitRate/String"))
            'cocotus end

            aLang = Me.Get_(StreamKind.Audio, a, "Language/String")
            If Not String.IsNullOrEmpty(aLang) Then
                miAudio.LongLanguage = aLang
                If Localization.ISOLangGetCode3ByLang(miAudio.LongLanguage) <> "" Then
                    miAudio.Language = Localization.ISOLangGetCode3ByLang(miAudio.LongLanguage)
                End If
            End If
            fiOut.StreamDetails.Audio.Add(miAudio)
        Next

        'Subtitle Scan
        Dim SubtitleStreams As Integer
        SubtitleStreams = Me.Count_Get(StreamKind.Text)
        Dim miSubtitle As New Subtitle

        Dim sLang As String = String.Empty
        For s As Integer = 0 To SubtitleStreams - 1
            miSubtitle = New MediaInfo.Subtitle
            sLang = Me.Get_(StreamKind.Text, s, "Language/String")
            If Not String.IsNullOrEmpty(sLang) Then
                miSubtitle.LongLanguage = sLang
                If Localization.ISOLangGetCode3ByLang(miSubtitle.LongLanguage) <> "" Then
                    miSubtitle.Language = Localization.ISOLangGetCode3ByLang(miSubtitle.LongLanguage)
                End If
            End If
            If Not String.IsNullOrEmpty(miSubtitle.Language) Then
                miSubtitle.SubsForced = FormatBoolean(Me.Get_(StreamKind.Text, s, "Forced/String"))
                miSubtitle.SubsType = "Embedded"
                fiOut.StreamDetails.Subtitle.Add(miSubtitle)
            End If
        Next

        'Video Scan
        Dim miVideo As New Video
        Dim VideoStreams As Integer
        VideoStreams = Me.Count_Get(StreamKind.Visual)
        For v As Integer = 0 To VideoStreams - 1
            miVideo = New Video
            'cocotus, It's possible that duration returns empty when retrieved from videostream data - so instead use "General" section of MediaInfo.dll to read duration (is always filled!)
            'More here: http://forum.xbmc.org/showthread.php?tid=169900 
            miVideo.Duration = Me.Get_(StreamKind.Visual, v, "Duration/String1")
            If miVideo.Duration = String.Empty Then
                miVideo.Duration = Me.Get_(StreamKind.General, 0, "Duration/String1")
            End If
            fiOut.StreamDetails.Video.Add(miVideo)
        Next


        Me.Close()

        Return fiOut

    End Function

    ''' <summary>
    ''' Use MediaInfo to get/scan subtitle, audio Stream and video information of videofile
    ''' </summary>
    ''' <returns>Mediainfo-Scanresults as MediainfoFileInfoObject</returns>
    Private Function ScanMI(ByVal sPath As String) As Fileinfo
        Dim fiOut As New Fileinfo
        Dim fiIFO As New Fileinfo
        Try
            If Not String.IsNullOrEmpty(sPath) Then
                Dim miVideo As New Video
                Dim miAudio As New Audio
                Dim miSubtitle As New Subtitle
                Dim VideoStreams As Integer
                Dim AudioStreams As Integer
                Dim SubtitleStreams As Integer
                Dim vLang As String = String.Empty
                Dim aLang As String = String.Empty
                Dim sLang As String = String.Empty
                Dim a_Profile As String = String.Empty
                Dim sExt As String = Path.GetExtension(sPath).ToLower
                Dim alternativeIFOFile As String = String.Empty

                'New ISO Handling -> Use either DAEMON Tools or VitualCloneDrive to mount ISO!
                If sExt = ".iso" OrElse FileUtils.Common.isVideoTS(sPath) OrElse FileUtils.Common.isBDRip(sPath) Then

                    'ISO-File Scanning using either DAIMON Tools / VCDMount.exe to mount and read file!
                    If sExt = ".iso" Then

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
                                Functions.Run_Process(ToolPath, """" & sPath & """", False, True)
                                System.Threading.Thread.Sleep(8000)
                                'Toolpath doesn't contain virtualclonedrive.exe -> assume daemon tools with DS type drive!
                            Else
                                'Unmount
                                '   Run_Process(ToolPath, " -unmount 0", False, True)
                                'Mount
                                Functions.Run_Process(ToolPath, " -mount 0, " & """" & sPath & """", False, True)
                                System.Threading.Thread.Sleep(8000)
                            End If

                            'now check if it's bluray or dvd image/VIDEO_TS/BMDV Folder-Scanning!
                            If Directory.Exists(driveletter & "VIDEO_TS") Then
                                sPath = driveletter & "VIDEO_TS"
                                SetMediaInfoScanPaths(sPath, fiIFO, alternativeIFOFile, True)
                            ElseIf Directory.Exists(driveletter & "BDMV\STREAM") Then
                                sPath = driveletter & "BDMV\STREAM"
                                SetMediaInfoScanPaths(sPath, fiIFO, alternativeIFOFile, True)
                            End If
                        End If

                        'VIDEO_TS/BMDV Folder-Scanning!
                    Else
                        If Directory.Exists(Directory.GetParent(sPath).FullName) Then
                            SetMediaInfoScanPaths(sPath, fiIFO, alternativeIFOFile, False)
                        End If
                    End If
                End If

                If Not sPath = String.Empty Then
                    Me.Handle = MediaInfo_New()

                    If Master.isWindows Then
                        UseAnsi = False
                    Else
                        UseAnsi = True
                    End If

                    Me.Open(sPath)

                    VideoStreams = Me.Count_Get(StreamKind.Visual)
                    AudioStreams = Me.Count_Get(StreamKind.Audio)
                    SubtitleStreams = Me.Count_Get(StreamKind.Text)

                    '2014/07/05 Fix for VIDEO_TS scanning: Use second largest (=alternativeIFOFile) IFO File if largest File doesn't contain needed information (=duration)! (rare case!)
                    If sPath.ToUpper.Contains("VIDEO_TS") Then
                        miVideo = New Video
                        'IFO Scan results (used when scanning VIDEO_TS files)
                        If fiIFO.StreamDetails.Video.Count > 0 Then
                            If Not String.IsNullOrEmpty(fiIFO.StreamDetails.Video(0).Duration) Then
                                miVideo.Duration = fiIFO.StreamDetails.Video(0).Duration
                            Else
                                'cocotus, It's possible that duration returns empty when retrieved from videostream data - so instead use "General" section of MediaInfo.dll to read duration (is always filled!)
                                'More here: http://forum.xbmc.org/showthread.php?tid=169900 
                                miVideo.Duration = Me.Get_(StreamKind.Visual, 0, "Duration/String1")
                                If miVideo.Duration = String.Empty Then
                                    miVideo.Duration = Me.Get_(StreamKind.General, 0, "Duration/String1")
                                End If
                            End If
                        Else
                            'cocotus, It's possible that duration returns empty when retrieved from videostream data - so instead use "General" section of MediaInfo.dll to read duration (is always filled!)
                            'More here: http://forum.xbmc.org/showthread.php?tid=169900 
                            miVideo.Duration = Me.Get_(StreamKind.Visual, 0, "Duration/String1")
                            If miVideo.Duration = String.Empty Then
                                miVideo.Duration = Me.Get_(StreamKind.General, 0, "Duration/String1")
                            End If
                        End If
                        'if ms instead of hours or minutes than wrong IFO!
                        If miVideo.Duration.ToUpper.ToString.Contains("ms") Then
                            fiIFO = Nothing
                            fiIFO = ScanLanguage(alternativeIFOFile)
                        End If
                    End If

                End If

                For v As Integer = 0 To VideoStreams - 1
                    miVideo = New Video
                    'cocotus, 2013/02 Added support for new MediaInfo-fields
                    'Video-Bitrate
                    miVideo.Bitrate = FormatBitrate(Me.Get_(StreamKind.Visual, v, "BitRate/String"))
                    'MultiViewCount (Support for 3D Movie, If > 1 -> 3D Movie)
                    miVideo.MultiViewCount = Me.Get_(StreamKind.Visual, v, "MultiView_Count")
                    'MultiViewLayout (http://matroska.org/technical/specs/index.html#StereoMode)
                    miVideo.MultiViewLayout = Me.Get_(StreamKind.Visual, v, "MultiView_Layout")
                    'Encoder-settings
                    miVideo.EncodedSettings = Me.Get_(StreamKind.Visual, v, "Encoded_Library_Settings")
                    'cocotus end
                    miVideo.StereoMode = ConvertVStereoMode(miVideo.MultiViewLayout)

                    miVideo.Width = Me.Get_(StreamKind.Visual, v, "Width")
                    miVideo.Height = Me.Get_(StreamKind.Visual, v, "Height")
                    miVideo.Codec = ConvertVFormat(Me.Get_(StreamKind.Visual, v, "CodecID/Hint"))
                    If String.IsNullOrEmpty(miVideo.Codec) OrElse IsNumeric(miVideo.Codec) Then
                        miVideo.Codec = ConvertVFormat(Me.Get_(StreamKind.Visual, v, "CodecID"))
                        If IsNumeric(miVideo.Codec) OrElse String.IsNullOrEmpty(miVideo.Codec) Then
                            miVideo.Codec = ConvertVFormat(Me.Get_(StreamKind.Visual, v, "Format"), Me.Get_(StreamKind.Visual, v, "Format_Version"))
                        End If
                    End If


                    'IFO Scan results (used when scanning VIDEO_TS files)
                    If fiIFO.StreamDetails.Video.Count > 0 Then
                        If Not String.IsNullOrEmpty(fiIFO.StreamDetails.Video(v).Duration) Then
                            miVideo.Duration = fiIFO.StreamDetails.Video(v).Duration
                        Else
                            'cocotus, It's possible that duration returns empty when retrieved from videostream data - so instead use "General" section of MediaInfo.dll to read duration (is always filled!)
                            'More here: http://forum.xbmc.org/showthread.php?tid=169900 
                            miVideo.Duration = Me.Get_(StreamKind.Visual, v, "Duration/String1")
                            If miVideo.Duration = String.Empty Then
                                miVideo.Duration = Me.Get_(StreamKind.General, 0, "Duration/String1")
                            End If
                        End If
                    Else
                        'cocotus, It's possible that duration returns empty when retrieved from videostream data - so instead use "General" section of MediaInfo.dll to read duration (is always filled!)
                        'More here: http://forum.xbmc.org/showthread.php?tid=169900 
                        miVideo.Duration = Me.Get_(StreamKind.Visual, v, "Duration/String1")
                        If miVideo.Duration = String.Empty Then
                            miVideo.Duration = Me.Get_(StreamKind.General, 0, "Duration/String1")
                        End If
                    End If


                    miVideo.Aspect = Me.Get_(StreamKind.Visual, v, "DisplayAspectRatio")
                    miVideo.Scantype = Me.Get_(StreamKind.Visual, v, "ScanType")

                    vLang = Me.Get_(StreamKind.Visual, v, "Language/String")
                    If Not String.IsNullOrEmpty(vLang) Then
                        miVideo.LongLanguage = vLang
                        If Localization.ISOLangGetCode3ByLang(miVideo.LongLanguage) <> "" Then
                            miVideo.Language = Localization.ISOLangGetCode3ByLang(miVideo.LongLanguage)
                        End If
                    End If

                    'With miVideo
                    '    If Not String.IsNullOrEmpty(.Codec) OrElse Not String.IsNullOrEmpty(.Duration) OrElse Not String.IsNullOrEmpty(.Aspect) OrElse _
                    '    Not String.IsNullOrEmpty(.Height) OrElse Not String.IsNullOrEmpty(.Width) OrElse Not String.IsNullOrEmpty(.Scantype) Then
                    '        fiOut.StreamDetails.Video.Add(miVideo)
                    '    End If
                    'End With
                    fiOut.StreamDetails.Video.Add(miVideo)
                Next


                For a As Integer = 0 To AudioStreams - 1
                    miAudio = New Audio

                    a_Profile = Me.Get_(StreamKind.Audio, a, "Format_Profile")
                    miAudio.Codec = ConvertAFormat(Me.Get_(StreamKind.Audio, a, "CodecID/Hint"), a_Profile)
                    If String.IsNullOrEmpty(miAudio.Codec) OrElse IsNumeric(miAudio.Codec) Then
                        miAudio.Codec = ConvertAFormat(Me.Get_(StreamKind.Audio, a, "CodecID"), a_Profile)
                        miAudio.Codec = If(IsNumeric(miAudio.Codec) OrElse String.IsNullOrEmpty(miAudio.Codec), ConvertAFormat(Me.Get_(StreamKind.Audio, a, "Format"), a_Profile), miAudio.Codec)
                    End If
                    miAudio.Channels = FormatAudioChannel(Me.Get_(StreamKind.Audio, a, "Channel(s)"))

                    'cocotus, 2013/02 Added support for new MediaInfo-fields
                    'Audio-Bitrate
                    miAudio.Bitrate = FormatBitrate(Me.Get_(StreamKind.Audio, a, "BitRate/String"))
                    'cocotus end

                    aLang = Me.Get_(StreamKind.Audio, a, "Language/String")
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


                For s As Integer = 0 To SubtitleStreams - 1

                    miSubtitle = New MediaInfo.Subtitle

                    sLang = Me.Get_(StreamKind.Text, s, "Language/String")
                    If Not String.IsNullOrEmpty(sLang) Then
                        miSubtitle.LongLanguage = sLang
                        If Localization.ISOLangGetCode3ByLang(miSubtitle.LongLanguage) <> "" Then
                            miSubtitle.Language = Localization.ISOLangGetCode3ByLang(miSubtitle.LongLanguage)
                        End If
                        miSubtitle.SubsForced = True

                        'IFO Scan results (used when scanning VIDEO_TS files)
                    ElseIf fiIFO.StreamDetails.Subtitle.Count > 0 Then
                        If Not String.IsNullOrEmpty(fiIFO.StreamDetails.Subtitle(s).LongLanguage) Then
                            miSubtitle.LongLanguage = fiIFO.StreamDetails.Subtitle(s).LongLanguage
                            miSubtitle.Language = fiIFO.StreamDetails.Subtitle(s).Language
                        End If
                    End If

                    If Not String.IsNullOrEmpty(miSubtitle.Language) Then
                        miSubtitle.SubsForced = FormatBoolean(Me.Get_(StreamKind.Text, s, "Forced/String"))
                        miSubtitle.SubsType = "Embedded"
                        fiOut.StreamDetails.Subtitle.Add(miSubtitle)
                    End If
                Next

                Me.Close()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return fiOut
    End Function

    Public Shared Function FormatBitrate(ByVal rawbitrate As String) As String
        'now consider bitrate number and calculate all values in KB instead of MB/KB
        If rawbitrate.ToUpper.IndexOf(" K") > 0 Then
            rawbitrate = rawbitrate.Substring(0, rawbitrate.ToUpper.IndexOf(" K"))
            Dim mystring As String = ""
            'use regex to get rid of all letters(if that ever happens just in case) and also remove spaces
            mystring = Text.RegularExpressions.Regex.Replace(rawbitrate, "[^.0-9]", "").Trim
            rawbitrate = mystring
        ElseIf rawbitrate.ToUpper.IndexOf(" M") > 0 Then
            'can happen if video is ripped from bluray
            rawbitrate = rawbitrate.Substring(0, rawbitrate.ToUpper.IndexOf(" M"))
            Dim mystring As String = ""
            'use regex to get rid of all letters(if that ever happens just in case) and also remove spaces
            mystring = Text.RegularExpressions.Regex.Replace(rawbitrate, "[^.0-9]", "").Trim
            Try
                rawbitrate = (CDbl(mystring) * 100).ToString
            Catch ex As Exception
            End Try
        End If
        'If no bitrate is read by MediaInfo, then set default 0 - I need value here because of comparing numbers later in HTML/Javascript template!
        If rawbitrate = "" Then
            rawbitrate = "0"
        End If
        Return rawbitrate
    End Function

    ''' <summary>
    ''' Convert string "x/y" to single digit number "x" (Audio Channel conversion)
    ''' </summary>
    ''' <param name="AudioChannelstring">The channelstring (as string) to clean</param>
    ''' <returns>cleaned Channelnumber, i.e  "8/6" will return as 7 </returns>
    ''' <remarks>Inputstring "x/y" will return as "x" which is highest number, i.e 8/6 -> 8 (assume: highest number always first!)
    ''' 2014/01/22 Cocotus - Since this functionality is needed on several places in Ember, I builded new function to avoid duplicate code
    '''</remarks>
    Public Shared Function FormatAudioChannel(ByVal AudioChannelstring As String) As String
        'cocotus 20130303 ChannelInfo fix
        'Channel(s)/sNumber might contain: "8 / 6" (7.1) so we must handle backslash and spaces --> XBMC/Ember only supports Number like 2,6,8...
        If AudioChannelstring.Contains("/") Then
            Dim mystring As String = ""
            'use regex to get rid of all letters(if that ever happens just in case) and also remove spaces
            mystring = Text.RegularExpressions.Regex.Replace(AudioChannelstring, "[^/.0-9]", "").Trim
            'now get channel number
            If mystring.Length > 0 Then
                If Char.IsDigit(mystring(0)) Then
                    Try
                        'In case of "x/y" this will return x which is highest number, i.e 8/6 -> 8 (highest number always first!)
                        AudioChannelstring = CStr(mystring(0))
                    Catch ex As Exception
                    End Try
                End If
            End If
        End If
        Return AudioChannelstring
        'cocotus end
    End Function
    ''' <summary>
    ''' Converts "Yes" and "No" to boolean
    ''' </summary>
    ''' <param name="bString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FormatBoolean(ByVal bString As String) As Boolean
        If Not String.IsNullOrEmpty(bString) Then
            Select Case bString.ToLower
                Case "yes"
                    Return True
                Case "no"
                    Return False
            End Select
        End If
        Return False
    End Function

    Public Shared Function FormatDuration(ByVal tDur As String, ByVal sMask As String) As String
        Dim sDuration As Match = Regex.Match(tDur, "(([0-9]+)h)?\s?(([0-9]+)min)?\s?(([0-9]+)s)?")
        Dim sHour As Integer = If(Not String.IsNullOrEmpty(sDuration.Groups(2).Value), (Convert.ToInt32(sDuration.Groups(2).Value)), 0)
        Dim sMin As Integer = If(Not String.IsNullOrEmpty(sDuration.Groups(4).Value), (Convert.ToInt32(sDuration.Groups(4).Value)), 0)
        Dim sSec As Integer = If(Not String.IsNullOrEmpty(sDuration.Groups(6).Value), (Convert.ToInt32(sDuration.Groups(6).Value)), 0)
        'Dim sMask As String = Master.eSettings.RuntimeMask
        'Dim sRuntime As String = String.Empty

        'new handling: only seconds as tdur
        If IsNumeric(tDur) Then
            Dim ts As New TimeSpan(0, 0, Convert.ToInt32(tDur))
            sHour = ts.Hours
            sMin = ts.Minutes
            sSec = ts.Seconds
        End If

        If sMask.Contains("<h>") Then
            If sMask.Contains("<m>") OrElse sMask.Contains("<0m>") Then
                If sMask.Contains("<s>") OrElse sMask.Contains("<0s>") Then
                    Return sMask.Replace("<h>", sHour.ToString).Replace("<m>", sMin.ToString).Replace("<0m>", sMin.ToString("00")).Replace("<s>", sSec.ToString).Replace("<0s>", sSec.ToString("00"))
                Else
                    Return sMask.Replace("<h>", sHour.ToString).Replace("<m>", sMin.ToString).Replace("<0m>", sMin.ToString("00"))
                End If
            Else
                Dim tHDec As String = If(sMin > 0, Convert.ToSingle(1 / (60 / sMin)).ToString(".00"), String.Empty)
                Return sMask.Replace("<h>", String.Concat(sHour, tHDec))
            End If
        ElseIf sMask.Contains("<m>") Then
            If sMask.Contains("<s>") OrElse sMask.Contains("<0s>") Then
                Return sMask.Replace("<m>", ((sHour * 60) + sMin).ToString).Replace("<s>", sSec.ToString).Replace("<0s>", sSec.ToString("00"))
            Else
                Return sMask.Replace("<m>", ((sHour * 60) + sMin).ToString)
            End If
        ElseIf sMask.Contains("<s>") Then
            Return sMask.Replace("<s>", ((sHour * 60 * 60) + sMin * 60 + sSec).ToString)
        Else
            Return sMask
        End If
    End Function



    ''' 
    ''' <summary>
    ''' Used to set the paths of IFO/VOB (DVD) or M2ts/CLPI (BLURAY) files for Mediainfo-Scanning
    ''' </summary>
    ''' <param name="sPath">The <c>String</c>, the path to videofile (VOB/M2TS)</param>
    ''' <param name="fiIFO"><c>MediaInfo.FileInfo</c> contains the scanned Mediainfo IFO information</param>
    ''' <param name="alternativeIFOFile"><c>String</c> path to second biggest IFO File of video - alternative to default biggest IFO file</param>
    ''' <param name="ISO"><c>Boolean</c> Source: .ISO file =True, if not = False</param>
    ''' <remarks>
    ''' 2014/07/05 Cocotus - Method created to remove duplicate code and make ScanMi function easier to read
    ''' </remarks>
    Private Sub SetMediaInfoScanPaths(ByRef sPath As String, ByRef fiIFO As Fileinfo, ByRef alternativeIFOFile As String, ByVal ISO As Boolean)
        Try
            If sPath.Contains("VIDEO_TS") Then
                'DVD structure


                Dim di As New IO.DirectoryInfo(Directory.GetParent(sPath).FullName)
                If ISO Then
                    'ie. path = driveletter & "VIDEO_TS"
                    di = New DirectoryInfo(sPath)
                End If


                'Biggest IFO File! -> Get Languages out of IFO and Bitrate data out of biggest VOB file!
                Dim myFilesIFO = From file In di.GetFiles("VTS*.IFO") _
                              Order By file.Length _
                              Select file.FullName
                If Not myFilesIFO Is Nothing AndAlso myFilesIFO.Count > 0 Then
                    alternativeIFOFile = myFilesIFO(myFilesIFO.Count - 2)
                    fiIFO = ScanLanguage(myFilesIFO.Last)
                End If

                'Biggest VOB File! -> Get Languages out of IFO and Bitrate data out of biggest VOB file!
                If Not myFilesIFO Is Nothing AndAlso myFilesIFO.Count > 0 AndAlso myFilesIFO.Last.Length > 6 Then

                    Dim myFiles = From file In di.GetFiles(Path.GetFileName(myFilesIFO.Last).Substring(0, Path.GetFileName(myFilesIFO.Last).Length - 6) & "*.VOB") _
                        Order By file.Length _
                        Select file.FullName
                    If Not myFiles Is Nothing AndAlso myFiles.Count > 0 Then
                        sPath = myFiles.Last
                    Else
                        myFiles = From file In di.GetFiles("VTS*.VOB") _
                           Order By file.Length _
                           Select file.FullName
                        sPath = myFiles.Last
                    End If
                Else
                    Dim myFiles = From file In di.GetFiles("VTS*.VOB") _
                             Order By file.Length _
                             Select file.FullName
                    sPath = myFiles.Last
                End If

                'Bluray
            Else

                ' looking at the largest m2ts file within the \BDMV\STREAM folder
                Dim di As New IO.DirectoryInfo(Directory.GetParent(sPath).FullName)
                If ISO Then
                    'ie. path = driveletter & "VIDEO_TS"
                    di = New DirectoryInfo(sPath)
                End If
                Dim myFiles = From file In di.GetFiles("*.m2ts") _
                              Order By file.Length _
                              Select file.Name

                If Not myFiles Is Nothing AndAlso myFiles.Count > 0 Then
                    'Biggest file!
                    If ISO Then
                        sPath = sPath & "\" & myFiles.Last
                    Else
                        sPath = Directory.GetParent(sPath).FullName & "\" & myFiles.Last
                    End If

                End If
                Dim ISOSubtitleScanFile As String
                If myFiles.Last.Length > 5 Then
                    ISOSubtitleScanFile = myFiles.Last.Substring(0, myFiles.Last.Length - 5) & ".clpi"
                    Dim clipinfpath As String = ""

                    clipinfpath = Directory.GetParent(sPath).FullName.Replace("STREAM", "CLIPINF")

                    If IO.File.Exists(clipinfpath & "\" & ISOSubtitleScanFile) Then
                        fiIFO = ScanLanguage(clipinfpath & "\" & ISOSubtitleScanFile)
                    End If
                End If

            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try


    End Sub


#End Region 'Methods

#Region "Nested Types"

    <Serializable()> _
    Public Class Audio

#Region "Fields"

        Private _bitrate As String = String.Empty
        Private _channels As String = String.Empty
        Private _codec As String = String.Empty
        Private _haspreferred As Boolean = False
        Private _language As String = String.Empty
        Private _longlanguage As String = String.Empty

#End Region 'Fields

#Region "Properties"

        <XmlElement("bitrate")> _
        Public Property Bitrate() As String
            Get
                Return Me._bitrate.Trim()
            End Get
            Set(ByVal Value As String)
                Me._bitrate = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property BitrateSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._bitrate)
            End Get
        End Property

        <XmlElement("channels")> _
        Public Property Channels() As String
            Get
                Return Me._channels.Trim()
            End Get
            Set(ByVal Value As String)
                Me._channels = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property ChannelsSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._channels)
            End Get
        End Property

        <XmlElement("codec")> _
        Public Property Codec() As String
            Get
                Return Me._codec.Trim()
            End Get
            Set(ByVal Value As String)
                Me._codec = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property CodecSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._codec)
            End Get
        End Property

        <XmlIgnore> _
        Public Property HasPreferred() As Boolean
            Get
                Return Me._haspreferred
            End Get
            Set(ByVal value As Boolean)
                Me._haspreferred = value
            End Set
        End Property

        <XmlElement("language")> _
        Public Property Language() As String
            Get
                Return Me._language.Trim()
            End Get
            Set(ByVal Value As String)
                Me._language = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property LanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._language)
            End Get
        End Property

        <XmlElement("longlanguage")> _
        Public Property LongLanguage() As String
            Get
                Return Me._longlanguage.Trim()
            End Get
            Set(ByVal value As String)
                Me._longlanguage = value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property LongLanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._longlanguage)
            End Get
        End Property

#End Region 'Properties

    End Class

    <Serializable()> _
    <XmlRoot("fileinfo")> _
    Public Class Fileinfo

#Region "Fields"

        Private _streamdetails As New StreamData

#End Region 'Fields

#Region "Properties"

        <XmlElement("streamdetails")> _
        Property StreamDetails() As StreamData
            Get
                Return _streamdetails
            End Get
            Set(ByVal value As StreamData)
                _streamdetails = value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property StreamDetailsSpecified() As Boolean
            Get
                Return (Not IsNothing(_streamdetails.Video) AndAlso _streamdetails.Video.Count > 0) OrElse _
                (Not IsNothing(_streamdetails.Audio) AndAlso _streamdetails.Audio.Count > 0) OrElse _
                (Not IsNothing(_streamdetails.Subtitle) AndAlso _streamdetails.Subtitle.Count > 0)
            End Get
        End Property

#End Region 'Properties

    End Class

    <Serializable()> _
    <XmlRoot("streamdata")> _
    Public Class StreamData

#Region "Fields"

        Private _audio As New List(Of Audio)
        Private _subtitle As New List(Of Subtitle)
        Private _video As New List(Of Video)

#End Region 'Fields

#Region "Properties"

        <XmlElement("audio")> _
        Public Property Audio() As List(Of Audio)
            Get
                Return Me._audio
            End Get
            Set(ByVal Value As List(Of Audio))
                Me._audio = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property AudioSpecified() As Boolean
            Get
                Return Me._audio.Count > 0
            End Get
        End Property

        <XmlElement("subtitle")> _
        Public Property Subtitle() As List(Of Subtitle)
            Get
                Return Me._subtitle
            End Get
            Set(ByVal Value As List(Of Subtitle))
                Me._subtitle = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property SubtitleSpecified() As Boolean
            Get
                Return Me._subtitle.Count > 0
            End Get
        End Property

        <XmlElement("video")> _
        Public Property Video() As List(Of Video)
            Get
                Return Me._video
            End Get
            Set(ByVal Value As List(Of Video))
                Me._video = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property VideoSpecified() As Boolean
            Get
                Return Me._video.Count > 0
            End Get
        End Property

#End Region 'Properties

    End Class

    <Serializable()> _
    Public Class Subtitle

#Region "Fields"

        Private _language As String = String.Empty
        Private _longlanguage As String = String.Empty
        Private _subs_foced As Boolean = False
        Private _subs_path As String = String.Empty
        Private _subs_type As String = String.Empty
        Private _toremove As Boolean = False            'trigger to delete local/external trailer files

#End Region 'Fields

#Region "Properties"

        <XmlElement("language")> _
        Public Property Language() As String
            Get
                Return Me._language
            End Get
            Set(ByVal Value As String)
                Me._language = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property LanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._language)
            End Get
        End Property

        <XmlElement("longlanguage")> _
        Public Property LongLanguage() As String
            Get
                Return Me._longlanguage
            End Get
            Set(ByVal value As String)
                Me._longlanguage = value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property LongLanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._longlanguage)
            End Get
        End Property

        <XmlElement("forced")> _
        Public Property SubsForced() As Boolean
            Get
                Return _subs_foced
            End Get
            Set(ByVal value As Boolean)
                _subs_foced = value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property SubsForcedSpecified() As Boolean
            Get
                Return Not IsNothing(Me._subs_foced)
            End Get
        End Property

        <XmlElement("path")> _
        Public Property SubsPath() As String
            Get
                Return _subs_path
            End Get
            Set(ByVal value As String)
                _subs_path = value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property SubsPathSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._subs_path)
            End Get
        End Property

        <XmlElement("type")> _
        Public Property SubsType() As String
            Get
                Return _subs_type
            End Get
            Set(ByVal value As String)
                _subs_type = value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property SubsTypeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._subs_type)
            End Get
        End Property

        <XmlIgnore> _
        Public Property toRemove() As Boolean
            Get
                Return _toremove
            End Get
            Set(ByVal value As Boolean)
                _toremove = value
            End Set
        End Property

#End Region 'Properties

    End Class

    <Serializable()> _
    Public Class Video

#Region "Fields"

        Private _aspect As String = String.Empty
        Private _bitrate As String = String.Empty
        Private _codec As String = String.Empty
        Private _duration As String = String.Empty
        Private _encoded_Settings As String = String.Empty
        Private _height As String = String.Empty
        Private _language As String = String.Empty
        Private _longlanguage As String = String.Empty
        Private _multiview_count As String = String.Empty
        Private _multiview_layout As String = String.Empty
        Private _scantype As String = String.Empty
        'XBMC multiview layout type (http://wiki.xbmc.org/index.php?title=3D)
        Private _stereomode As String = String.Empty
        Private _width As String = String.Empty


#End Region 'Fields

#Region "Properties"

        <XmlElement("aspect")> _
        Public Property Aspect() As String
            Get
                Return Me._aspect.Trim()
            End Get
            Set(ByVal Value As String)
                Me._aspect = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property AspectSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._aspect)
            End Get
        End Property

        <XmlElement("bitrate")> _
        Public Property Bitrate() As String
            Get
                Return Me._bitrate.Trim()
            End Get
            Set(ByVal Value As String)
                Me._bitrate = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property BitrateSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._bitrate)
            End Get
        End Property

        <XmlElement("codec")> _
        Public Property Codec() As String
            Get
                Return Me._codec.Trim()
            End Get
            Set(ByVal Value As String)
                Me._codec = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property CodecSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._codec)
            End Get
        End Property

        <XmlElement("durationinseconds")> _
        Public Property Duration() As String
            Get
                Return Me._duration.Trim()
            End Get
            Set(ByVal Value As String)
                Me._duration = Value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property DurationSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._duration)
            End Get
        End Property

        <XmlElement("encodedsettings")> _
        Public Property EncodedSettings() As String
            Get
                Return Me._encoded_Settings.Trim()
            End Get
            Set(ByVal Value As String)
                Me._encoded_Settings = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property EncodedSettingsSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._encoded_Settings)
            End Get
        End Property

        <XmlElement("height")> _
        Public Property Height() As String
            Get
                Return Me._height.Trim()
            End Get
            Set(ByVal Value As String)
                Me._height = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property HeightSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._height)
            End Get
        End Property

        <XmlElement("language")> _
        Public Property Language() As String
            Get
                Return Me._language.Trim()
            End Get
            Set(ByVal Value As String)
                Me._language = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property LanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._language)
            End Get
        End Property

        <XmlElement("longlanguage")> _
        Public Property LongLanguage() As String
            Get
                Return Me._longlanguage.Trim()
            End Get
            Set(ByVal value As String)
                Me._longlanguage = value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property LongLanguageSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._longlanguage)
            End Get
        End Property

        <XmlElement("multiview_count")> _
        Public Property MultiViewCount() As String
            Get
                Return Me._multiview_count.Trim()
            End Get
            Set(ByVal Value As String)
                Me._multiview_count = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property MultiViewCountSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._multiview_count)
            End Get
        End Property

        <XmlElement("multiview_layout")> _
        Public Property MultiViewLayout() As String
            Get
                Return Me._multiview_layout.Trim()
            End Get
            Set(ByVal Value As String)
                Me._multiview_layout = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property MultiViewLayoutSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._multiview_layout)
            End Get
        End Property

        <XmlElement("scantype")> _
        Public Property Scantype() As String
            Get
                Return Me._scantype.Trim()
            End Get
            Set(ByVal Value As String)
                Me._scantype = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property ScantypeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._scantype)
            End Get
        End Property

        <XmlElement("stereomode")> _
        Public Property StereoMode() As String
            Get
                Return Me._stereomode.Trim()
            End Get
            Set(ByVal Value As String)
                Me._stereomode = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property StereoModeSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._stereomode)
            End Get
        End Property

        <XmlElement("width")> _
        Public Property Width() As String
            Get
                Return Me._width.Trim()
            End Get
            Set(ByVal Value As String)
                Me._width = Value
            End Set
        End Property

        <XmlIgnore> _
        Public ReadOnly Property WidthSpecified() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me._width)
            End Get
        End Property

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class