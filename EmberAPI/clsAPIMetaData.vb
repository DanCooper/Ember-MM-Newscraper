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

Public Class MetaData

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

    Private Shared Function ApplyDefaults(ByVal tDBElement As Database.DBElement) As MediaContainers.FileInfo
        Dim nFileInfo As New MediaContainers.FileInfo
        Dim nMetadataList As New List(Of Settings.MetadataPerType)

        Select Case tDBElement.ContentType
            Case Enums.ContentType.Movie
                nMetadataList = Master.eSettings.MovieMetadataPerFileType
            Case Enums.ContentType.TVEpisode
                nMetadataList = Master.eSettings.TVMetadataPerFileType
        End Select

        For Each m As Settings.MetadataPerType In nMetadataList
            If m.FileType.ToLower = tDBElement.FileItem.FileInfo.Extension.ToLower Then
                nFileInfo = m.MetaData
                Return nFileInfo
            End If
        Next
        Return Nothing
    End Function

    Private Shared Function CleanupFileInfo(ByVal tFileInfo As MediaContainers.FileInfo) As MediaContainers.FileInfo
        If tFileInfo.StreamDetailsSpecified Then
            If tFileInfo.StreamDetails.VideoSpecified Then
                For i As Integer = 0 To tFileInfo.StreamDetails.Video.Count - 1
                    tFileInfo.StreamDetails.Video(i).StereoMode = ConvertVideoStereoMode(tFileInfo.StreamDetails.Video(i).MultiViewLayout)
                Next
            End If
        End If

        Return tFileInfo
    End Function

    Public Shared Function ConvertVideoStereoMode(ByVal strFormat As String) As String
        Select Case strFormat.ToLower
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

    Private Shared Function FormatVideoDurationFromSeconds(ByVal strDurationInSeconds As String, ByVal strMask As String) As String
        Dim intHours As Integer = 0
        Dim intMinutes As Integer = 0
        Dim intSeconds As Integer = 0

        'new handling: only seconds as tdur
        If Integer.TryParse(strDurationInSeconds, 0) Then
            Dim ts As New TimeSpan(0, 0, Convert.ToInt32(strDurationInSeconds))
            intHours = ts.Hours
            intMinutes = ts.Minutes
            intSeconds = ts.Seconds
        End If

        If strMask.Contains("<h>") Then
            If strMask.Contains("<m>") OrElse strMask.Contains("<0m>") Then
                If strMask.Contains("<s>") OrElse strMask.Contains("<0s>") Then
                    Return strMask.Replace("<h>", intHours.ToString).Replace("<m>", intMinutes.ToString).Replace("<0m>", intMinutes.ToString("00")).Replace("<s>", intSeconds.ToString).Replace("<0s>", intSeconds.ToString("00"))
                Else
                    Return strMask.Replace("<h>", intHours.ToString).Replace("<m>", intMinutes.ToString).Replace("<0m>", intMinutes.ToString("00"))
                End If
            Else
                Dim tHDec As String = If(intMinutes > 0, Convert.ToSingle(1 / (60 / intMinutes)).ToString(".00"), String.Empty)
                Return strMask.Replace("<h>", String.Concat(intHours, tHDec))
            End If
        ElseIf strMask.Contains("<m>") Then
            If strMask.Contains("<s>") OrElse strMask.Contains("<0s>") Then
                Return strMask.Replace("<m>", ((intHours * 60) + intMinutes).ToString).Replace("<s>", intSeconds.ToString).Replace("<0s>", intSeconds.ToString("00"))
            Else
                Return strMask.Replace("<m>", ((intHours * 60) + intMinutes).ToString)
            End If
        ElseIf strMask.Contains("<s>") Then
            Return strMask.Replace("<s>", ((intHours * 60 * 60) + intMinutes * 60 + intSeconds).ToString)
        Else
            Return strMask
        End If
    End Function

    Private Shared Function GetFileInfo(ByVal fileItem As FileItem, ByVal contentType As Enums.ContentType) As MediaContainers.FileInfo
        Dim nFileInfo As New MediaContainers.FileInfo

        If fileItem.FullPathSpecified AndAlso File.Exists(fileItem.FirstStackedPath) Then
            Dim nStackedFiles As New List(Of MediaContainers.FileInfo)
            Dim nMediaInfo As New MediaInfo

            'scan Main video file to get all media informations
            If fileItem.bIsDiscImage Then
                Dim nVirtualDrive As New FileUtils.VirtualDrive(fileItem.FirstStackedPath)
                If nVirtualDrive.IsLoaded Then
                    Dim nFileItemList As New FileItemList(nVirtualDrive.Path, contentType)
                    If nFileItemList.FileItems.Count > 0 Then
                        nFileInfo = GetFileInfo(nFileItemList.FileItems.Item(0), contentType)
                        nVirtualDrive.UnmountDiscImage()
                    End If
                End If
            ElseIf fileItem.bIsBDMV Then
                'looking at the largest m2ts file within the \BDMV\STREAM folder
                Dim diStream = New DirectoryInfo(Path.Combine(fileItem.MainPath.FullName, "BDMV", "STREAM"))
                If diStream.Exists Then
                    Dim fiStream As IEnumerable(Of String) = diStream.GetFiles("*.m2ts").OrderByDescending(Function(f) f.Length).Select(Function(f) f.FullName)
                    If fiStream IsNot Nothing AndAlso fiStream.Count > 0 Then
                        'get the ".clpi" file of the biggest ".m2ts" video file (that holds all needed data)
                        'the files are stored in "\BDMV\CLIPINF\*.clpi"
                        Dim fiClpi = New FileInfo(fiStream(0).Replace("STREAM", "CLIPINF").Replace("m2ts", "clpi"))
                        If fiClpi.Exists Then
                            nFileInfo = nMediaInfo.ScanPath(fiClpi.FullName)
                        Else
                            'fallback to the ".m2ts" file to get the basic data
                            nFileInfo = GetFileInfo(New FileItem(fiStream(0)), contentType)
                        End If
                    End If
                End If
            Else
                nFileInfo = nMediaInfo.ScanPath(fileItem.FirstStackedPath)
            End If

            'scan all stacked video files to get the total duration
            For Each strStackedPath In fileItem.PathList.Where(Function(f) Not f.ToString = fileItem.FirstStackedPath)
                If File.Exists(strStackedPath) Then
                    If fileItem.bIsDiscImage Then
                        Dim nVirtualDrive As New FileUtils.VirtualDrive(strStackedPath)
                        If nVirtualDrive.IsLoaded Then
                            Dim nAdditionalFileInfo = nMediaInfo.ScanPath(nVirtualDrive.Path)
                            If nAdditionalFileInfo IsNot Nothing AndAlso nAdditionalFileInfo.StreamDetailsSpecified Then
                                nStackedFiles.Add(nAdditionalFileInfo)
                            End If
                            nVirtualDrive.UnmountDiscImage()
                        End If
                    Else
                        Dim nAdditionalFileInfo = nMediaInfo.ScanPath(strStackedPath)
                        If nAdditionalFileInfo IsNot Nothing AndAlso nAdditionalFileInfo.StreamDetailsSpecified Then
                            nStackedFiles.Add(nAdditionalFileInfo)
                        End If
                    End If
                Else
                    logger.Error(String.Format("[MetaData] [ScanFileItem] Stacked file not found: {0} ", strStackedPath))
                End If
            Next

            'Set TotalDuration of stacked files
            If nStackedFiles.Count > 0 Then
                Dim iTotalRuntime As Integer = nFileInfo.StreamDetails.Video(0).Duration
                For Each fileinfo In nStackedFiles
                    iTotalRuntime += fileinfo.StreamDetails.Video(0).Duration
                Next
                nFileInfo.StreamDetails.Video(0).Duration = iTotalRuntime
            End If
        End If

        Return nFileInfo
    End Function

    Public Shared Sub UpdateFileInfo(ByRef dbElement As Database.DBElement)
        Dim bLockAudioLanguages As Boolean
        Dim bLockVideoLanguages As Boolean
        Dim bUseRuntimeFormat As Boolean
        Dim currentFileInfo As New MediaContainers.FileInfo
        Dim nFileInfo As New MediaContainers.FileInfo

        Select Case dbElement.ContentType
            Case Enums.ContentType.Movie
                bLockAudioLanguages = Master.eSettings.MovieLockLanguageA
                bLockVideoLanguages = Master.eSettings.MovieLockLanguageV
                bUseRuntimeFormat = Master.eSettings.MovieScraperUseMDDuration
                currentFileInfo = dbElement.Movie.FileInfo
            Case Enums.ContentType.TVEpisode
                bLockAudioLanguages = Master.eSettings.TVLockEpisodeLanguageA
                bLockVideoLanguages = Master.eSettings.TVLockEpisodeLanguageV
                bUseRuntimeFormat = Master.eSettings.TVScraperUseMDDuration
                currentFileInfo = dbElement.TVEpisode.FileInfo
            Case Else
                Exit Sub
        End Select

        If Not dbElement.FileItem.bIsArchive AndAlso Not dbElement.FileItem.bIsDiscStub Then
            nFileInfo = GetFileInfo(dbElement.FileItem, dbElement.ContentType)

            If nFileInfo.StreamDetailsSpecified Then
                If nFileInfo.StreamDetails.AudioSpecified Then
                    'do the audio codec mapping
                    For Each stream In nFileInfo.StreamDetails.Audio.Where(Function(f) f.CodecSpecified)
                        Dim lstMappings As New List(Of AdvancedSettingsComplexSettingsTableItem)
                        lstMappings = AdvancedSettings.GetComplexSetting("AudioFormatConverts")
                        If Not lstMappings Is Nothing Then
                            For Each k In lstMappings
                                If stream.Codec.ToLower = k.Name.ToLower Then
                                    stream.Codec = k.Value
                                End If
                            Next
                        End If
                    Next
                End If
                If nFileInfo.StreamDetails.VideoSpecified Then
                    'do the video codec mapping
                    For Each stream In nFileInfo.StreamDetails.Video.Where(Function(f) f.CodecSpecified)
                        Dim lstMappings As New List(Of AdvancedSettingsComplexSettingsTableItem)
                        lstMappings = AdvancedSettings.GetComplexSetting("VideoFormatConverts")
                        If Not lstMappings Is Nothing Then
                            For Each k In lstMappings
                                If stream.Codec.ToLower = k.Name.ToLower Then
                                    stream.Codec = k.Value
                                End If
                            Next
                        End If
                    Next
                End If
            End If

            If nFileInfo.StreamDetailsSpecified Then
                ' overwrite only if it get something from Mediainfo 
                If bLockAudioLanguages Then
                    'sets old language setting if setting is enabled (lock language)
                    'First make sure that there is no completely new audio source scanned of the movie --> if so (i.e. more streams) then update!
                    If nFileInfo.StreamDetails.Audio.Count = currentFileInfo.StreamDetails.Audio.Count Then
                        For i = 0 To nFileInfo.StreamDetails.Audio.Count - 1
                            'only preserve if language tag is filled --> else update!
                            If currentFileInfo.StreamDetails.Audio.Item(i).LongLanguageSpecified Then
                                nFileInfo.StreamDetails.Audio.Item(i).Language = currentFileInfo.StreamDetails.Audio.Item(i).Language
                                nFileInfo.StreamDetails.Audio.Item(i).LongLanguage = currentFileInfo.StreamDetails.Audio.Item(i).LongLanguage
                            End If
                        Next
                    End If
                End If
                If bLockVideoLanguages Then
                    'sets old language setting if setting is enabled (lock language)
                    'First make sure that there is no completely new video source scanned of the movie --> if so (i.e. more streams) then update!
                    If nFileInfo.StreamDetails.Video.Count = currentFileInfo.StreamDetails.Video.Count Then
                        For i = 0 To nFileInfo.StreamDetails.Video.Count - 1
                            'only preserve if language tag is filled --> else update!
                            If currentFileInfo.StreamDetails.Video.Item(i).LongLanguageSpecified Then
                                nFileInfo.StreamDetails.Video.Item(i).Language = currentFileInfo.StreamDetails.Video.Item(i).Language
                                nFileInfo.StreamDetails.Video.Item(i).LongLanguage = currentFileInfo.StreamDetails.Video.Item(i).LongLanguage
                            End If
                        Next
                    End If
                End If
            End If
            If nFileInfo.StreamDetails.VideoSpecified AndAlso bUseRuntimeFormat Then
                Dim tVid As MediaContainers.Video = NFO.GetBestVideo(currentFileInfo)
                If tVid.DurationSpecified Then
                    Select Case dbElement.ContentType
                        Case Enums.ContentType.Movie
                            dbElement.Movie.Runtime = StringUtils.FormatDuration(tVid.Duration.ToString, dbElement.ContentType)
                        Case Enums.ContentType.TVEpisode
                            dbElement.TVEpisode.Runtime = StringUtils.FormatDuration(tVid.Duration.ToString, dbElement.ContentType)
                    End Select
                End If
            End If
        End If

        'load defaults for this file extension if no FileInfo has been readed
        If Not nFileInfo.StreamDetailsSpecified Then
            Dim nFileInfoByExtension = ApplyDefaults(dbElement)
            If nFileInfoByExtension IsNot Nothing Then
                nFileInfo = nFileInfoByExtension
            End If
        End If

        'set the new FileInfo for the dbElement
        Select Case dbElement.ContentType
            Case Enums.ContentType.Movie
                dbElement.Movie.FileInfo = CleanupFileInfo(nFileInfo)
            Case Enums.ContentType.TVEpisode
                dbElement.TVEpisode.FileInfo = CleanupFileInfo(nFileInfo)
        End Select
    End Sub

#End Region 'Methods

End Class
