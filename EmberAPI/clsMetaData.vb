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

    Private Shared Function ApplyDefaults(ByVal tDBElement As Database.DBElement) As MediaContainers.Fileinfo
        Dim nFileInfo As New MediaContainers.Fileinfo
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

    Private Shared Function CleanupFileInfo(ByVal tFileInfo As MediaContainers.Fileinfo) As MediaContainers.Fileinfo
        If tFileInfo.StreamDetailsSpecified Then
            If tFileInfo.StreamDetails.VideoSpecified Then
                For i As Integer = 0 To tFileInfo.StreamDetails.Video.Count - 1
                    tFileInfo.StreamDetails.Video(i).StereoMode = ConvertVideoStereoMode(tFileInfo.StreamDetails.Video(i).MultiViewLayout)
                Next
            End If
        End If

        Return tFileInfo
    End Function

    Private Shared Function CombineFileInfo(ByVal mainFileInfo As MediaContainers.Fileinfo, otherFileInfo As MediaContainers.Fileinfo) As MediaContainers.Fileinfo
        If mainFileInfo IsNot Nothing AndAlso mainFileInfo.StreamDetailsSpecified AndAlso otherFileInfo IsNot Nothing AndAlso otherFileInfo.StreamDetailsSpecified Then
            'check if both containers has the same count of streams
            If mainFileInfo.StreamDetails.Audio.Count = otherFileInfo.StreamDetails.Audio.Count AndAlso
                mainFileInfo.StreamDetails.Subtitle.Count = otherFileInfo.StreamDetails.Subtitle.Count AndAlso
                mainFileInfo.StreamDetails.Video.Count = otherFileInfo.StreamDetails.Video.Count Then

                'audio streams
                For i = 0 To mainFileInfo.StreamDetails.Audio.Count - 1
                    If Not mainFileInfo.StreamDetails.Audio(i).AdditionalFeaturesSpecified AndAlso otherFileInfo.StreamDetails.Audio(i).AdditionalFeaturesSpecified Then mainFileInfo.StreamDetails.Audio(i).AdditionalFeatures = otherFileInfo.StreamDetails.Audio(i).AdditionalFeatures
                    If Not mainFileInfo.StreamDetails.Audio(i).BitDepthSpecified AndAlso otherFileInfo.StreamDetails.Audio(i).BitDepthSpecified Then mainFileInfo.StreamDetails.Audio(i).BitDepth = otherFileInfo.StreamDetails.Audio(i).BitDepth
                    If Not mainFileInfo.StreamDetails.Audio(i).BitrateSpecified AndAlso otherFileInfo.StreamDetails.Audio(i).BitrateSpecified Then mainFileInfo.StreamDetails.Audio(i).Bitrate = otherFileInfo.StreamDetails.Audio(i).Bitrate
                    If Not mainFileInfo.StreamDetails.Audio(i).ChannelsSpecified AndAlso otherFileInfo.StreamDetails.Audio(i).ChannelsSpecified Then mainFileInfo.StreamDetails.Audio(i).Channels = otherFileInfo.StreamDetails.Audio(i).Channels
                    If Not mainFileInfo.StreamDetails.Audio(i).CodecSpecified AndAlso otherFileInfo.StreamDetails.Audio(i).CodecSpecified Then mainFileInfo.StreamDetails.Audio(i).Codec = otherFileInfo.StreamDetails.Audio(i).Codec
                    If Not mainFileInfo.StreamDetails.Audio(i).LanguageSpecified AndAlso otherFileInfo.StreamDetails.Audio(i).LanguageSpecified Then mainFileInfo.StreamDetails.Audio(i).Language = otherFileInfo.StreamDetails.Audio(i).Language
                    If Not mainFileInfo.StreamDetails.Audio(i).LongLanguageSpecified AndAlso otherFileInfo.StreamDetails.Audio(i).LongLanguageSpecified Then mainFileInfo.StreamDetails.Audio(i).LongLanguage = otherFileInfo.StreamDetails.Audio(i).LongLanguage
                Next

                'subtitle streams
                For i = 0 To mainFileInfo.StreamDetails.Subtitle.Count - 1
                    If Not mainFileInfo.StreamDetails.Subtitle(i).Forced AndAlso otherFileInfo.StreamDetails.Subtitle(i).Forced Then mainFileInfo.StreamDetails.Subtitle(i).Forced = otherFileInfo.StreamDetails.Subtitle(i).Forced
                    If Not mainFileInfo.StreamDetails.Subtitle(i).LanguageSpecified AndAlso otherFileInfo.StreamDetails.Subtitle(i).LanguageSpecified Then mainFileInfo.StreamDetails.Subtitle(i).Language = otherFileInfo.StreamDetails.Subtitle(i).Language
                    If Not mainFileInfo.StreamDetails.Subtitle(i).LongLanguageSpecified AndAlso otherFileInfo.StreamDetails.Subtitle(i).LongLanguageSpecified Then mainFileInfo.StreamDetails.Subtitle(i).LongLanguage = otherFileInfo.StreamDetails.Subtitle(i).LongLanguage
                Next

                'video streams
                For i = 0 To mainFileInfo.StreamDetails.Video.Count - 1
                    If Not mainFileInfo.StreamDetails.Video(i).AspectSpecified AndAlso otherFileInfo.StreamDetails.Video(i).AspectSpecified Then mainFileInfo.StreamDetails.Video(i).Aspect = otherFileInfo.StreamDetails.Video(i).Aspect
                    If Not mainFileInfo.StreamDetails.Video(i).BitDepthSpecified AndAlso otherFileInfo.StreamDetails.Video(i).BitDepthSpecified Then mainFileInfo.StreamDetails.Video(i).BitDepth = otherFileInfo.StreamDetails.Video(i).BitDepth
                    If Not mainFileInfo.StreamDetails.Video(i).BitrateSpecified AndAlso otherFileInfo.StreamDetails.Video(i).BitDepthSpecified Then mainFileInfo.StreamDetails.Video(i).Bitrate = otherFileInfo.StreamDetails.Video(i).Bitrate
                    If Not mainFileInfo.StreamDetails.Video(i).ChromaSubsamplingSpecified AndAlso otherFileInfo.StreamDetails.Video(i).ChromaSubsamplingSpecified Then mainFileInfo.StreamDetails.Video(i).ChromaSubsampling = otherFileInfo.StreamDetails.Video(i).ChromaSubsampling
                    If Not mainFileInfo.StreamDetails.Video(i).CodecSpecified AndAlso otherFileInfo.StreamDetails.Video(i).CodecSpecified Then mainFileInfo.StreamDetails.Video(i).Codec = otherFileInfo.StreamDetails.Video(i).Codec
                    If Not mainFileInfo.StreamDetails.Video(i).ColourPrimariesSpecified AndAlso otherFileInfo.StreamDetails.Video(i).ColourPrimariesSpecified Then mainFileInfo.StreamDetails.Video(i).ColourPrimaries = otherFileInfo.StreamDetails.Video(i).ColourPrimaries
                    If Not mainFileInfo.StreamDetails.Video(i).DurationSpecified AndAlso otherFileInfo.StreamDetails.Video(i).DurationSpecified Then mainFileInfo.StreamDetails.Video(i).Duration = otherFileInfo.StreamDetails.Video(i).Duration
                    If Not mainFileInfo.StreamDetails.Video(i).HeightSpecified AndAlso otherFileInfo.StreamDetails.Video(i).HeightSpecified Then mainFileInfo.StreamDetails.Video(i).Height = otherFileInfo.StreamDetails.Video(i).Height
                    If Not mainFileInfo.StreamDetails.Video(i).LanguageSpecified AndAlso otherFileInfo.StreamDetails.Video(i).LanguageSpecified Then mainFileInfo.StreamDetails.Video(i).Language = otherFileInfo.StreamDetails.Video(i).Language
                    If Not mainFileInfo.StreamDetails.Video(i).LongLanguageSpecified AndAlso otherFileInfo.StreamDetails.Video(i).LongLanguageSpecified Then mainFileInfo.StreamDetails.Video(i).LongLanguage = otherFileInfo.StreamDetails.Video(i).LongLanguage
                    If Not mainFileInfo.StreamDetails.Video(i).MultiViewCountSpecified AndAlso otherFileInfo.StreamDetails.Video(i).MultiViewCountSpecified Then mainFileInfo.StreamDetails.Video(i).MultiViewCount = otherFileInfo.StreamDetails.Video(i).MultiViewCount
                    If Not mainFileInfo.StreamDetails.Video(i).MultiViewLayoutSpecified AndAlso otherFileInfo.StreamDetails.Video(i).MultiViewLayoutSpecified Then mainFileInfo.StreamDetails.Video(i).MultiViewLayout = otherFileInfo.StreamDetails.Video(i).MultiViewLayout
                    If Not mainFileInfo.StreamDetails.Video(i).ScantypeSpecified AndAlso otherFileInfo.StreamDetails.Video(i).ScantypeSpecified Then mainFileInfo.StreamDetails.Video(i).Scantype = otherFileInfo.StreamDetails.Video(i).Scantype
                    If Not mainFileInfo.StreamDetails.Video(i).StereoModeSpecified AndAlso otherFileInfo.StreamDetails.Video(i).StereoModeSpecified Then mainFileInfo.StreamDetails.Video(i).StereoMode = otherFileInfo.StreamDetails.Video(i).StereoMode
                    If Not mainFileInfo.StreamDetails.Video(i).WidthSpecified AndAlso otherFileInfo.StreamDetails.Video(i).WidthSpecified Then mainFileInfo.StreamDetails.Video(i).Width = otherFileInfo.StreamDetails.Video(i).Width
                Next
            End If
        End If
        Return mainFileInfo
    End Function

    Private Shared Function ConvertVideoStereoMode(ByVal strFormat As String) As String
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

    Public Shared Function GetBestAudio(ByVal fileInfo As MediaContainers.Fileinfo, ByVal preferredLanguage As String, ByVal contentType As Enums.ContentType) As MediaContainers.Audio
        Dim nBestAudio As New MediaContainers.Audio
        Dim nFilteredAudio As New MediaContainers.Fileinfo
        Dim bGetPrefLanguage As Boolean = False
        Dim bHasPrefLanguage As Boolean = False
        Dim strPrefLanguage As String = String.Empty
        Dim iHighestBitrate As Integer = 0
        Dim iHighestChannels As Integer = 0

        If Not String.IsNullOrEmpty(preferredLanguage) Then
            bGetPrefLanguage = True
            strPrefLanguage = preferredLanguage.ToLower
        End If

        If bGetPrefLanguage AndAlso fileInfo.StreamDetails.Audio.Where(Function(f) f.LongLanguage.ToLower = strPrefLanguage).Count > 0 Then
            For Each Stream As MediaContainers.Audio In fileInfo.StreamDetails.Audio
                If Stream.LongLanguage.ToLower = strPrefLanguage Then
                    nFilteredAudio.StreamDetails.Audio.Add(Stream)
                End If
            Next
        Else
            nFilteredAudio.StreamDetails.Audio.AddRange(fileInfo.StreamDetails.Audio)
        End If

        For Each miAudio As MediaContainers.Audio In nFilteredAudio.StreamDetails.Audio
            If miAudio.ChannelsSpecified Then
                If miAudio.Channels >= iHighestChannels AndAlso (miAudio.Bitrate > iHighestBitrate OrElse miAudio.Bitrate = 0) Then
                    iHighestBitrate = miAudio.Bitrate
                    iHighestChannels = miAudio.Channels
                    nBestAudio.Bitrate = miAudio.Bitrate
                    nBestAudio.Channels = miAudio.Channels
                    nBestAudio.Codec = miAudio.Codec
                    nBestAudio.Language = miAudio.Language
                    nBestAudio.LongLanguage = miAudio.LongLanguage
                End If
            End If
            If bGetPrefLanguage AndAlso miAudio.LongLanguage.ToLower = strPrefLanguage Then nBestAudio.HasPreferred = True
        Next

        Return nBestAudio
    End Function

    Public Shared Function GetBestVideo(ByVal miFIV As MediaContainers.Fileinfo) As MediaContainers.Video
        Dim nBestVideo = miFIV.StreamDetails.Video.OrderBy(Function(f) f.Width).Reverse.FirstOrDefault
        If nBestVideo IsNot Nothing Then
            Return nBestVideo
        Else
            Return New MediaContainers.Video
        End If
    End Function

    Private Shared Function GetFileInfo(ByVal fileItem As FileItem, ByVal contentType As Enums.ContentType) As MediaContainers.Fileinfo
        Dim nFileInfo As New MediaContainers.Fileinfo

        If fileItem.FullPathSpecified AndAlso File.Exists(fileItem.FirstPathFromStack) Then
            Dim nStackedFiles As New List(Of MediaContainers.Fileinfo)
            Dim nMediaInfo As New MediaInfo

            'scan Main video file to get all media informations
            If fileItem.bIsDiscImage Then
                Dim nVirtualDrive As New FileUtils.VirtualCloneDrive(fileItem.FirstPathFromStack)
                If nVirtualDrive.IsReady Then
                    Dim nFileItemList As New FileItemList(nVirtualDrive.Path, contentType)
                    If nFileItemList.FileItems.Count > 0 Then
                        nFileInfo = GetFileInfo(nFileItemList.FileItems.Item(0), contentType)
                        nVirtualDrive.UnmountDiscImage()
                    End If
                End If
            ElseIf fileItem.bIsBDMV Then
                'looking at the largest ".m2ts" file within the "\BDMV\STREAM" folder
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
            ElseIf fileItem.bIsVideoTS Then
                'looking at the largest ".ifo" file within the "\VIDEO_TS" folder
                Dim diVideoTS = New DirectoryInfo(Path.Combine(fileItem.MainPath.FullName, "VIDEO_TS"))
                If Not diVideoTS.Exists Then
                    'search for "VIDEO_TS" files without a "VIDEO_TS" folder
                    diVideoTS = New DirectoryInfo(fileItem.MainPath.FullName)
                End If
                If diVideoTS.Exists Then
                    'get the the biggest ".vob" video file (real video file)
                    Dim fiVOB As IEnumerable(Of String) = diVideoTS.GetFiles("vts*.vob").OrderByDescending(Function(f) f.Length).Select(Function(f) f.FullName)
                    'get the the biggest ".ifo" information file (that holds additional data like language that's missing in ".vob" files)
                    Dim fiIFO As IEnumerable(Of String) = diVideoTS.GetFiles("vts*.ifo").OrderByDescending(Function(f) f.Length).Select(Function(f) f.FullName)
                    If fiVOB IsNot Nothing AndAlso fiVOB.Count > 0 Then
                        nFileInfo = nMediaInfo.ScanPath(fiVOB(0))
                    End If
                    Dim nAdditionalFileInfo As New MediaContainers.Fileinfo
                    If fiIFO IsNot Nothing AndAlso fiIFO.Count > 0 Then
                        nAdditionalFileInfo = nMediaInfo.ScanPath(fiIFO(0))
                    End If
                    nFileInfo = CombineFileInfo(nFileInfo, nAdditionalFileInfo)
                End If
            Else
                nFileInfo = nMediaInfo.ScanPath(fileItem.FirstPathFromStack)
            End If

            'scan all stacked video files to get the total duration
            For Each strStackedPath In fileItem.PathList.Where(Function(f) Not f.ToString = fileItem.FirstPathFromStack)
                Dim nAdditionalFileInfo = GetFileInfo(New FileItem(strStackedPath), contentType)
                If nAdditionalFileInfo IsNot Nothing AndAlso nAdditionalFileInfo.StreamDetailsSpecified Then
                    nStackedFiles.Add(nAdditionalFileInfo)
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
        Dim nSettings As New DataSpecificationItem_Metadata
        Dim currentFileInfo = dbElement.MainDetails.FileInfo
        Dim nFileInfo As New MediaContainers.Fileinfo

        Select Case dbElement.ContentType
            Case Enums.ContentType.Movie
                nSettings = Master.eSettings.Movie.InformationSettings.MetadataScan
            Case Enums.ContentType.TVEpisode
                nSettings = Master.eSettings.TVEpisode.InformationSettings.MetadataScan
            Case Else
                Exit Sub
        End Select

        If Not dbElement.FileItem.bIsArchive AndAlso Not dbElement.FileItem.bIsDiscStub Then
            nFileInfo = GetFileInfo(dbElement.FileItem, dbElement.ContentType)

            If nFileInfo.StreamDetailsSpecified Then
                If nFileInfo.StreamDetails.AudioSpecified Then
                    'do the audio codec mapping
                    For Each stream In nFileInfo.StreamDetails.Audio.Where(Function(f) f.CodecSpecified)
                        For Each mapping In Master.eSettings.Options.AVCodecMapping.Audio
                            If stream.Codec = mapping.Codec Then
                                stream.AdditionalFeatures = mapping.AdditionalFeatures
                                stream.Codec = mapping.Mapping
                            End If
                        Next
                    Next
                End If
                If nFileInfo.StreamDetails.VideoSpecified Then
                    'do the video codec mapping
                    For Each stream In nFileInfo.StreamDetails.Video.Where(Function(f) f.CodecSpecified)
                        For Each mapping In Master.eSettings.Options.AVCodecMapping.Video
                            If stream.Codec = mapping.Codec Then
                                stream.Codec = mapping.Mapping
                            End If
                        Next
                    Next
                End If
            End If

            If nFileInfo.StreamDetailsSpecified Then
                ' overwrite only if it get something from Mediainfo 
                If nSettings.LockAudioLanguage Then
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
                If nSettings.LockVideoLanguage Then
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
            If nFileInfo.StreamDetails.VideoSpecified AndAlso nSettings.DurationForRuntimeEnabled Then
                Dim tVid As MediaContainers.Video = GetBestVideo(currentFileInfo)
                If tVid.DurationSpecified Then
                    dbElement.MainDetails.Runtime = StringUtils.FormatDuration(tVid.Duration.ToString, dbElement.ContentType)
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
        dbElement.MainDetails.FileInfo = CleanupFileInfo(nFileInfo)
    End Sub

#End Region 'Methods

End Class