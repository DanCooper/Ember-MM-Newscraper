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
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization

Public Class NFO

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

    Public Shared Function CleanNFO_Movies(ByVal mNFO As MediaContainers.MainDetails) As MediaContainers.MainDetails
        If mNFO IsNot Nothing Then
            mNFO.Outline = mNFO.Outline.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
            mNFO.Plot = mNFO.Plot.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
            mNFO.Premiered = NumUtils.DateToISO8601Date(mNFO.Premiered)
            mNFO.Votes = NumUtils.CleanVotes(mNFO.Votes)
            If mNFO.FileInfoSpecified Then
                If mNFO.FileInfo.StreamDetails.AudioSpecified Then
                    For Each aStream In mNFO.FileInfo.StreamDetails.Audio.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                        aStream.LongLanguage = Localization.Languages.Get_Name_By_Alpha3(aStream.Language)
                    Next
                End If
                If mNFO.FileInfo.StreamDetails.SubtitleSpecified Then
                    For Each sStream In mNFO.FileInfo.StreamDetails.Subtitle.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                        sStream.LongLanguage = Localization.Languages.Get_Name_By_Alpha3(sStream.Language)
                    Next
                End If
            End If
            If mNFO.SetsSpecified Then
                For i = mNFO.Sets.Items.Count - 1 To 0 Step -1
                    If Not mNFO.Sets.Items(i).TitleSpecified Then
                        mNFO.Sets.Items.RemoveAt(i)
                    End If
                Next
            End If

            'changes a LongLanguage to Alpha2 code
            If mNFO.LanguageSpecified Then
                Dim Language = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Name = mNFO.Language)
                If Language IsNot Nothing Then
                    mNFO.Language = Language.Abbreviation
                Else
                    'check if it's a valid Alpha2 code or remove the information the use the source default language
                    Dim ShortLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = mNFO.Language)
                    If ShortLanguage Is Nothing Then
                        mNFO.Language = String.Empty
                    End If
                End If
            End If

            Return mNFO
        Else
            Return mNFO
        End If
    End Function

    Public Shared Function CleanNFO_TVEpisodes(ByVal eNFO As MediaContainers.MainDetails) As MediaContainers.MainDetails
        If eNFO IsNot Nothing Then
            eNFO.Aired = NumUtils.DateToISO8601Date(eNFO.Aired)
            eNFO.Votes = NumUtils.CleanVotes(eNFO.Votes)
            If eNFO.FileInfoSpecified Then
                If eNFO.FileInfo.StreamDetails.AudioSpecified Then
                    For Each aStream In eNFO.FileInfo.StreamDetails.Audio.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                        aStream.LongLanguage = Localization.Languages.Get_Name_By_Alpha3(aStream.Language)
                    Next
                End If
                If eNFO.FileInfo.StreamDetails.SubtitleSpecified Then
                    For Each sStream In eNFO.FileInfo.StreamDetails.Subtitle.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                        sStream.LongLanguage = Localization.Languages.Get_Name_By_Alpha3(sStream.Language)
                    Next
                End If
            End If
            Return eNFO
        Else
            Return eNFO
        End If
    End Function

    Public Shared Function CleanNFO_TVShow(ByVal mNFO As MediaContainers.MainDetails) As MediaContainers.MainDetails
        If mNFO IsNot Nothing Then
            mNFO.Plot = mNFO.Plot.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
            mNFO.Premiered = NumUtils.DateToISO8601Date(mNFO.Premiered)
            mNFO.Votes = NumUtils.CleanVotes(mNFO.Votes)

            'changes a LongLanguage to Alpha2 code
            If mNFO.LanguageSpecified Then
                Dim Language = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Name = mNFO.Language)
                If Language IsNot Nothing Then
                    mNFO.Language = Language.Abbreviation
                Else
                    'check if it's a valid Alpha2 code or remove the information the use the source default language
                    Dim ShortLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = mNFO.Language)
                    If ShortLanguage Is Nothing Then
                        mNFO.Language = String.Empty
                    End If
                End If
            End If

            'TODO: Boxee support
            'If Master.eSettings.TVUseBoxee Then
            '    If mNFO.BoxeeTvDbSpecified AndAlso Not mNFO.UniqueIDs.TVDbIdSpecified Then
            '        mNFO.UniqueIDs.TVDbId = mNFO.BoxeeTVDb
            '        mNFO.BlankBoxeeId()
            '    End If
            'End If

            Return mNFO
        Else
            Return mNFO
        End If
    End Function
    ''' <summary>
    ''' Sets the "ElementName" in an XML for different NFO types
    ''' </summary>
    ''' <param name="contentType"></param>
    ''' <returns></returns>
    Private Shared Function CreateOverrider(ByVal contentType As Enums.ContentType) As XmlSerializer
        Dim attrs As New XmlAttributes
        Dim xOver As New XmlAttributeOverrides
        Dim xRoot As New XmlRootAttribute

        Select Case contentType
            Case Enums.ContentType.Movie
                xRoot.ElementName = "movie"
            Case Enums.ContentType.MovieSet
                xRoot.ElementName = "movieset"
            Case Enums.ContentType.TVEpisode
                xRoot.ElementName = "episodedetails"
            Case Enums.ContentType.TVShow
                xRoot.ElementName = "tvshow"
        End Select
        attrs.XmlRoot = xRoot
        xOver.Add(GetType(MediaContainers.MainDetails), attrs)
        Return New XmlSerializer(GetType(MediaContainers.MainDetails), xOver)
    End Function
    ''' <summary>
    ''' Delete all movie NFOs
    ''' </summary>
    ''' <param name="dbElement"></param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteNFO_Movie(ByVal dbElement As Database.DBElement, ByVal forceFileCleanup As Boolean)
        If Not dbElement.FilenameSpecified Then Return

        Try
            For Each a In FileUtils.FileNames.Movie(dbElement, Enums.ModifierType.MainNFO, forceFileCleanup)
                If File.Exists(a) Then
                    File.Delete(a)
                End If
            Next
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & dbElement.Filename & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Delete all movie NFOs
    ''' </summary>
    ''' <param name="dbElement"></param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteNFO_MovieSet(ByVal dbElement As Database.DBElement, ByVal forceFileCleanup As Boolean, Optional forceOldTitle As Boolean = False)
        If Not dbElement.MainDetails.TitleSpecified Then Return

        Try
            For Each a In FileUtils.FileNames.Movieset(dbElement, Enums.ModifierType.MainNFO, forceOldTitle)
                If File.Exists(a) Then
                    File.Delete(a)
                End If
            Next
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & dbElement.Filename & ">")
        End Try
    End Sub

    Public Shared Function FIToString(ByVal miFI As MediaContainers.Fileinfo, ByVal isTV As Boolean) As String
        '//
        ' Convert Fileinfo into a string to be displayed in the GUI
        '\\

        Dim strOutput As New StringBuilder
        Dim iVS As Integer = 1
        Dim iAS As Integer = 1
        Dim iSS As Integer = 1

        Try
            If miFI IsNot Nothing Then

                If miFI.StreamDetails IsNot Nothing Then
                    If miFI.StreamDetails.VideoSpecified Then strOutput.AppendFormat("{0}: {1}{2}", Master.eLang.GetString(595, "Video Streams"), miFI.StreamDetails.Video.Count.ToString, Environment.NewLine)
                    If miFI.StreamDetails.AudioSpecified Then strOutput.AppendFormat("{0}: {1}{2}", Master.eLang.GetString(596, "Audio Streams"), miFI.StreamDetails.Audio.Count.ToString, Environment.NewLine)
                    If miFI.StreamDetails.SubtitleSpecified Then strOutput.AppendFormat("{0}: {1}{2}", Master.eLang.GetString(597, "Subtitle  Streams"), miFI.StreamDetails.Subtitle.Count.ToString, Environment.NewLine)
                    For Each miVideo As MediaContainers.Video In miFI.StreamDetails.Video
                        strOutput.AppendFormat("{0}{1} {2}{0}", Environment.NewLine, Master.eLang.GetString(617, "Video Stream"), iVS)
                        If miVideo.WidthSpecified AndAlso miVideo.HeightSpecified Then strOutput.AppendFormat("- {0}{1}", String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), miVideo.Width, miVideo.Height), Environment.NewLine)
                        If miVideo.AspectSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(614, "Aspect Ratio"), miVideo.Aspect, Environment.NewLine)
                        If miVideo.ScantypeSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(605, "Scan Type"), miVideo.Scantype, Environment.NewLine)
                        If miVideo.CodecSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(604, "Codec"), miVideo.Codec, Environment.NewLine)
                        If miVideo.BitrateSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", "Bitrate", miVideo.Bitrate, Environment.NewLine)
                        If miVideo.DurationSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(609, "Duration"), miVideo.Duration, Environment.NewLine)
                        'for now return filesize in mbytes instead of bytes(default)
                        If miVideo.Filesize > 0 Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(1455, "Filesize [MB]"), CStr(NumUtils.ConvertBytesTo(CLng(miVideo.Filesize), NumUtils.FileSizeUnit.Megabyte, 0)), Environment.NewLine)
                        If miVideo.LongLanguageSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(610, "Language"), miVideo.LongLanguage, Environment.NewLine)
                        If miVideo.MultiViewCountSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(1156, "MultiView Count"), miVideo.MultiViewCount, Environment.NewLine)
                        If miVideo.MultiViewLayoutSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(1157, "MultiView Layout"), miVideo.MultiViewLayout, Environment.NewLine)
                        If miVideo.StereoModeSpecified Then strOutput.AppendFormat("- {0}: {1} ({2})", Master.eLang.GetString(1286, "StereoMode"), miVideo.StereoMode, miVideo.ShortStereoMode)
                        iVS += 1
                    Next

                    strOutput.Append(Environment.NewLine)

                    For Each miAudio As MediaContainers.Audio In miFI.StreamDetails.Audio
                        'audio
                        strOutput.AppendFormat("{0}{1} {2}{0}", Environment.NewLine, Master.eLang.GetString(618, "Audio Stream"), iAS.ToString)
                        If miAudio.CodecSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(604, "Codec"), miAudio.Codec, Environment.NewLine)
                        If miAudio.ChannelsSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(611, "Channels"), miAudio.Channels, Environment.NewLine)
                        If miAudio.BitrateSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", "Bitrate", miAudio.Bitrate, Environment.NewLine)
                        If miAudio.LongLanguageSpecified Then strOutput.AppendFormat("- {0}: {1}", Master.eLang.GetString(610, "Language"), miAudio.LongLanguage)
                        iAS += 1
                    Next

                    strOutput.Append(Environment.NewLine)

                    For Each miSub As MediaContainers.Subtitle In miFI.StreamDetails.Subtitle
                        'subtitles
                        strOutput.AppendFormat("{0}{1} {2}{0}", Environment.NewLine, Master.eLang.GetString(619, "Subtitle Stream"), iSS.ToString)
                        If miSub.LongLanguageSpecified Then strOutput.AppendFormat("- {0}: {1}", Master.eLang.GetString(610, "Language"), miSub.LongLanguage)
                        iSS += 1
                    Next
                End If
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        If strOutput.ToString.Trim.Length > 0 Then
            Return strOutput.ToString
        Else
            If isTV Then
                Return Master.eLang.GetString(504, "Meta Data is not available for this episode. Try rescanning.")
            Else
                Return Master.eLang.GetString(419, "Meta Data is not available for this movie. Try rescanning.")
            End If
        End If
    End Function

    Public Shared Function GetBestVideo(ByVal miFIV As MediaContainers.Fileinfo) As MediaContainers.Video
        '//
        ' Get the highest values from file info
        '\\

        Dim fivOut As New MediaContainers.Video
        Try
            Dim iWidest As Integer = 0
            Dim iWidth As Integer = 0

            For Each miVideo As MediaContainers.Video In miFIV.StreamDetails.Video
                If miVideo.WidthSpecified Then
                    iWidth = miVideo.Width
                    If iWidth > iWidest Then
                        iWidest = iWidth
                        fivOut.Width = miVideo.Width
                        fivOut.Height = miVideo.Height
                        fivOut.Aspect = miVideo.Aspect
                        fivOut.Codec = miVideo.Codec
                        fivOut.Duration = miVideo.Duration
                        fivOut.Scantype = miVideo.Scantype
                        fivOut.Language = miVideo.Language

                        'cocotus, 2013/02 Added support for new MediaInfo-fields

                        'MultiViewCount (3D) handling, simply map field
                        fivOut.MultiViewCount = miVideo.MultiViewCount

                        'MultiViewLayout (3D) handling, simply map field
                        fivOut.MultiViewLayout = miVideo.MultiViewLayout

                        'FileSize handling, simply map field
                        fivOut.Filesize = miVideo.Filesize

                        'Bitrate handling, simply map field
                        fivOut.Bitrate = miVideo.Bitrate
                        'cocotus end

                    End If
                End If
            Next

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return fivOut
    End Function

    Public Shared Function GetDimensionsFromVideo(ByVal fiRes As MediaContainers.Video) As String
        '//
        ' Get the dimension values of the video from the information provided by MediaInfo.dll
        '\\

        Dim result As String = String.Empty
        Try
            If fiRes.WidthSpecified AndAlso fiRes.HeightSpecified AndAlso fiRes.AspectSpecified Then
                Dim iWidth As Integer = fiRes.Width
                Dim iHeight As Integer = fiRes.Height
                Dim sinADR As Single = CSng(fiRes.Aspect)

                result = String.Format("{0}x{1} ({2})", iWidth, iHeight, sinADR.ToString("0.00"))
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return result
    End Function

    Public Shared Function GetIMDBFromNonConf(ByVal sPath As String, ByVal isSingle As Boolean) As NonConf
        Dim tNonConf As New NonConf
        Dim dirPath As String = Directory.GetParent(sPath).FullName
        Dim lFiles As New List(Of String)

        If isSingle Then
            Try
                lFiles.AddRange(Directory.GetFiles(dirPath, "*.nfo"))
            Catch
            End Try
            Try
                lFiles.AddRange(Directory.GetFiles(dirPath, "*.info"))
            Catch
            End Try
        Else
            Dim fName As String = Path.GetFileNameWithoutExtension(FileUtils.Common.RemoveStackingMarkers(sPath)).ToLower
            Dim oName As String = Path.GetFileNameWithoutExtension(sPath)
            fName = If(fName.EndsWith("*"), fName, String.Concat(fName, "*"))
            oName = If(oName.EndsWith("*"), oName, String.Concat(oName, "*"))

            Try
                lFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(fName, ".nfo")))
            Catch
            End Try
            Try
                lFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(oName, ".nfo")))
            Catch
            End Try
            Try
                lFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(fName, ".info")))
            Catch
            End Try
            Try
                lFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(oName, ".info")))
            Catch
            End Try
        End If

        For Each sFile As String In lFiles
            Using srInfo As New StreamReader(sFile)
                Dim sInfo As String = srInfo.ReadToEnd
                Dim sIMDBID As String = Regex.Match(sInfo, "tt\d\d\d\d\d\d\d*", RegexOptions.Multiline Or RegexOptions.Singleline Or RegexOptions.IgnoreCase).ToString

                If Not String.IsNullOrEmpty(sIMDBID) Then
                    tNonConf.IMDBID = sIMDBID
                    'now lets try to see if the rest of the file is a proper nfo
                    If sInfo.ToLower.Contains("</movie>") Then
                        tNonConf.Text = APIXML.XmlToLowerCase(sInfo.Substring(0, sInfo.ToLower.IndexOf("</movie>") + 8))
                    End If
                    Exit For
                Else
                    sIMDBID = Regex.Match(sPath, "tt\d\d\d\d\d\d\d*", RegexOptions.Multiline Or RegexOptions.Singleline Or RegexOptions.IgnoreCase).ToString
                    If Not String.IsNullOrEmpty(sIMDBID) Then
                        tNonConf.IMDBID = sIMDBID
                    End If
                End If
            End Using
        Next
        Return tNonConf
    End Function

    Public Shared Function GetNfoPath_MovieSet(ByVal dbElement As Database.DBElement) As String
        For Each a In FileUtils.FileNames.Movieset(dbElement, Enums.ModifierType.MainNFO)
            If File.Exists(a) Then
                Return a
            End If
        Next

        Return String.Empty
    End Function
    ''' <summary>
    ''' Get the resolution of the video from the dimensions provided by MediaInfo.dll
    ''' </summary>
    ''' <param name="video"></param>
    ''' <returns></returns>
    Public Shared Function GetResFromDimensions(ByVal video As MediaContainers.Video) As String
        Dim iWidth As Integer = video.Width
        Dim iHeight As Integer = video.Height
        Dim strResolution As String = String.Empty

        Select Case True
            'exact
            Case iWidth = 7680 AndAlso iHeight = 4320   'UHD 8K
                strResolution = "4320"
            Case iWidth = 4096 AndAlso iHeight = 2160   'UHD 4K (cinema)
                strResolution = "2160"
            Case iWidth = 3840 AndAlso iHeight = 2160   'UHD 4K
                strResolution = "2160"
            Case iWidth = 2560 AndAlso iHeight = 1600   'WQXGA (16:10)
                strResolution = "1600"
            Case iWidth = 2560 AndAlso iHeight = 1440   'WQHD (16:9)
                strResolution = "1440"
            Case iWidth = 1920 AndAlso iHeight = 1200   'WUXGA (16:10)
                strResolution = "1200"
            Case iWidth = 1920 AndAlso iHeight = 1080   'HD1080 (16:9)
                strResolution = "1080"
            Case iWidth = 1680 AndAlso iHeight = 1050   'WSXGA+ (16:10)
                strResolution = "1050"
            Case iWidth = 1600 AndAlso iHeight = 900    'HD+ (16:9)
                strResolution = "900"
            Case iWidth = 1280 AndAlso iHeight = 720    'HD720 / WXGA (16:9)
                strResolution = "720"
            Case iWidth = 800 AndAlso iHeight = 480     'Rec. 601 plus a quarter (5:3)
                strResolution = "480"
            Case iWidth = 768 AndAlso iHeight = 576     'PAL
                strResolution = "576"
            Case iWidth = 720 AndAlso iHeight = 480     'Rec. 601 (3:2)
                strResolution = "480"
            Case iWidth = 720 AndAlso iHeight = 576     'PAL (DVD)
                strResolution = "576"
            Case iWidth = 720 AndAlso iHeight = 540     'half of 1080p (16:9)
                strResolution = "540"
            Case iWidth = 640 AndAlso iHeight = 480     'VGA (4:3)
                strResolution = "480"
            Case iWidth = 640 AndAlso iHeight = 360     'Wide 360p (16:9)
                strResolution = "360"
            Case iWidth = 480 AndAlso iHeight = 360     '360p (4:3, uncommon)
                strResolution = "360"
            Case iWidth = 426 AndAlso iHeight = 240     'NTSC widescreen (16:9)
                strResolution = "240"
            Case iWidth = 352 AndAlso iHeight = 240     'NTSC-standard VCD / super-long-play DVD (4:3)
                strResolution = "240"
            Case iWidth = 320 AndAlso iHeight = 240     'CGA / NTSC square pixel (4:3)
                strResolution = "240"
            Case iWidth = 256 AndAlso iHeight = 144     'One tenth of 1440p (16:9)
                strResolution = "144"
            Case Else
                '
                ' MAM: simple version, totally sufficient. Add new res at the end of the list if they become available (before "99999999" of course!)
                ' Warning: this list needs to be sorted from lowest to highes resolution, else the search routine will go nuts!
                '
                Dim aVres() = New Dictionary(Of Integer, String) From
                        {
                        {426, "240"},
                        {480, "360"},
                        {640, "480"},
                        {720, "576"},
                        {1280, "720"},
                        {1920, "1080"},
                        {4096, "2160"},
                        {7680, "4320"},
                        {99999999, String.Empty}
                    }.ToArray
                '
                ' search appropriate horizontal resolution
                ' Note: Array's last entry must be a ridiculous high number, else this loop will surely crash!
                '
                Dim i As Integer
                While (aVres(i).Key < iWidth)
                    i = i + 1
                End While
                strResolution = aVres(i).Value
        End Select

        If Not String.IsNullOrEmpty(strResolution) AndAlso video.ScantypeSpecified Then
            Return String.Concat(strResolution, If(video.Scantype.ToLower = "progressive", "p", "i"))
        End If

        Return strResolution
    End Function

    Public Shared Function IsConformingNFO_Movie(ByVal sPath As String) As Boolean
        Dim testSer As XmlSerializer = Nothing

        Try
            If (Path.GetExtension(sPath) = ".nfo" OrElse Path.GetExtension(sPath) = ".info") AndAlso File.Exists(sPath) Then
                Using testSR As StreamReader = New StreamReader(sPath)
                    testSer = CreateOverrider(Enums.ContentType.Movie)
                    Dim testMovie As MediaContainers.MainDetails = DirectCast(testSer.Deserialize(testSR), MediaContainers.MainDetails)
                    testMovie = Nothing
                    testSer = Nothing
                End Using
                Return True
            Else
                Return False
            End If
        Catch
            If testSer IsNot Nothing Then
                testSer = Nothing
            End If
            Return False
        End Try
    End Function

    Public Shared Function IsConformingNFO_TVEpisode(ByVal sPath As String) As Boolean
        Dim testSer = CreateOverrider(Enums.ContentType.TVEpisode)
        Dim testEp As New MediaContainers.MainDetails

        Try
            If (Path.GetExtension(sPath) = ".nfo" OrElse Path.GetExtension(sPath) = ".info") AndAlso File.Exists(sPath) Then
                Using xmlSR As StreamReader = New StreamReader(sPath)
                    Dim xmlStr As String = xmlSR.ReadToEnd
                    Dim rMatches As MatchCollection = Regex.Matches(xmlStr, "<episodedetails.*?>.*?</episodedetails>", RegexOptions.IgnoreCase Or RegexOptions.Singleline Or RegexOptions.IgnorePatternWhitespace)
                    If rMatches.Count = 1 Then
                        Using xmlRead As StringReader = New StringReader(rMatches(0).Value)
                            testEp = DirectCast(testSer.Deserialize(xmlRead), MediaContainers.MainDetails)
                            testSer = Nothing
                            testEp = Nothing
                            Return True
                        End Using
                    ElseIf rMatches.Count > 1 Then
                        'read them all... if one fails, the entire nfo is non conforming
                        For Each xmlReg As Match In rMatches
                            Using xmlRead As StringReader = New StringReader(xmlReg.Value)
                                testEp = DirectCast(testSer.Deserialize(xmlRead), MediaContainers.MainDetails)
                                testEp = Nothing
                            End Using
                        Next
                        testSer = Nothing
                        Return True
                    Else
                        testSer = Nothing
                        If testEp IsNot Nothing Then
                            testEp = Nothing
                        End If
                        Return False
                    End If
                End Using
            Else
                testSer = Nothing
                testEp = Nothing
                Return False
            End If
        Catch
            If testSer IsNot Nothing Then
                testSer = Nothing
            End If
            If testEp IsNot Nothing Then
                testEp = Nothing
            End If
            Return False
        End Try
    End Function

    Public Shared Function IsConformingNFO_TVShow(ByVal sPath As String) As Boolean
        Dim testSer As XmlSerializer = Nothing

        Try
            If (Path.GetExtension(sPath) = ".nfo" OrElse Path.GetExtension(sPath) = ".info") AndAlso File.Exists(sPath) Then
                Using testSR As StreamReader = New StreamReader(sPath)
                    testSer = CreateOverrider(Enums.ContentType.TVShow)
                    Dim testShow As MediaContainers.MainDetails = DirectCast(testSer.Deserialize(testSR), MediaContainers.MainDetails)
                    testShow = Nothing
                    testSer = Nothing
                End Using
                Return True
            Else
                Return False
            End If
        Catch
            If testSer IsNot Nothing Then
                testSer = Nothing
            End If

            Return False
        End Try
    End Function

    Public Shared Function LoadFromNFO_Movie(ByVal sPath As String, ByVal isSingle As Boolean) As MediaContainers.MainDetails
        Dim xmlSer As XmlSerializer = Nothing
        Dim xmlMov As New MediaContainers.MainDetails

        If Not String.IsNullOrEmpty(sPath) Then
            Try
                If File.Exists(sPath) AndAlso Path.GetExtension(sPath).ToLower = ".nfo" Then
                    Using xmlSR As StreamReader = New StreamReader(sPath)
                        xmlSer = CreateOverrider(Enums.ContentType.Movie)
                        xmlMov = DirectCast(xmlSer.Deserialize(xmlSR), MediaContainers.MainDetails)
                        xmlMov = CleanNFO_Movies(xmlMov)
                    End Using
                Else
                    If Not String.IsNullOrEmpty(sPath) Then
                        Dim sReturn As New NonConf
                        sReturn = GetIMDBFromNonConf(sPath, isSingle)
                        xmlMov.UniqueIDs.IMDbId = sReturn.IMDBID
                        Try
                            If Not String.IsNullOrEmpty(sReturn.Text) Then
                                Using xmlSTR As StringReader = New StringReader(sReturn.Text)
                                    xmlSer = CreateOverrider(Enums.ContentType.Movie)
                                    xmlMov = DirectCast(xmlSer.Deserialize(xmlSTR), MediaContainers.MainDetails)
                                    xmlMov.UniqueIDs.IMDbId = sReturn.IMDBID
                                    xmlMov = CleanNFO_Movies(xmlMov)
                                End Using
                            End If
                        Catch
                        End Try
                    End If
                End If

            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)

                xmlMov = New MediaContainers.MainDetails
                If Not String.IsNullOrEmpty(sPath) Then

                    'go ahead and rename it now, will still be picked up in getimdbfromnonconf
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNFO_Movie(sPath, True)
                    End If

                    Dim sReturn As New NonConf
                    sReturn = GetIMDBFromNonConf(sPath, isSingle)
                    xmlMov.UniqueIDs.IMDbId = sReturn.IMDBID
                    Try
                        If Not String.IsNullOrEmpty(sReturn.Text) Then
                            Using xmlSTR As StringReader = New StringReader(sReturn.Text)
                                xmlSer = New XmlSerializer(GetType(MediaContainers.MainDetails))
                                xmlMov = DirectCast(xmlSer.Deserialize(xmlSTR), MediaContainers.MainDetails)
                                xmlMov.UniqueIDs.IMDbId = sReturn.IMDBID
                                xmlMov = CleanNFO_Movies(xmlMov)
                            End Using
                        End If
                    Catch
                    End Try
                End If
            End Try

            If xmlSer IsNot Nothing Then
                xmlSer = Nothing
            End If
        End If

        Return xmlMov
    End Function

    Public Shared Function LoadFromNFO_Movieset(ByVal sPath As String) As MediaContainers.MainDetails
        Dim xmlSer As XmlSerializer = Nothing
        Dim xmlMovSet As New MediaContainers.MainDetails

        If Not String.IsNullOrEmpty(sPath) Then
            Try
                If File.Exists(sPath) AndAlso Path.GetExtension(sPath).ToLower = ".nfo" Then
                    Using xmlSR As StreamReader = New StreamReader(sPath)
                        xmlSer = CreateOverrider(Enums.ContentType.MovieSet)
                        xmlMovSet = DirectCast(xmlSer.Deserialize(xmlSR), MediaContainers.MainDetails)
                        xmlMovSet.Plot = xmlMovSet.Plot.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
                    End Using
                End If

            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
                xmlMovSet = New MediaContainers.MainDetails
            End Try

            If xmlSer IsNot Nothing Then
                xmlSer = Nothing
            End If
        End If

        Return xmlMovSet
    End Function

    Public Shared Function LoadFromNFO_TVEpisode(ByVal sPath As String, ByVal season As Integer, ByVal episode As Integer) As MediaContainers.MainDetails
        Dim xmlSer = CreateOverrider(Enums.ContentType.TVEpisode)
        Dim xmlEp As New MediaContainers.MainDetails

        If Not String.IsNullOrEmpty(sPath) AndAlso season >= -1 Then
            Try
                If File.Exists(sPath) AndAlso Path.GetExtension(sPath).ToLower = ".nfo" Then
                    'better way to read multi-root xml??
                    Using xmlSR As StreamReader = New StreamReader(sPath)
                        Dim xmlStr As String = xmlSR.ReadToEnd
                        Dim rMatches As MatchCollection = Regex.Matches(xmlStr, "<episodedetails.*?>.*?</episodedetails>", RegexOptions.IgnoreCase Or RegexOptions.Singleline Or RegexOptions.IgnorePatternWhitespace)
                        If rMatches.Count = 1 Then
                            'only one episodedetail... assume it's the proper one
                            Using xmlRead As StringReader = New StringReader(rMatches(0).Value)
                                xmlEp = DirectCast(xmlSer.Deserialize(xmlRead), MediaContainers.MainDetails)
                                xmlEp = CleanNFO_TVEpisodes(xmlEp)
                                xmlSer = Nothing
                                If xmlEp.FileInfoSpecified Then
                                    If xmlEp.FileInfo.StreamDetails.AudioSpecified Then
                                        For Each aStream In xmlEp.FileInfo.StreamDetails.Audio.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                                            aStream.LongLanguage = Localization.Languages.Get_Name_By_Alpha3(aStream.Language)
                                        Next
                                    End If
                                    If xmlEp.FileInfo.StreamDetails.SubtitleSpecified Then
                                        For Each sStream In xmlEp.FileInfo.StreamDetails.Subtitle.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                                            sStream.LongLanguage = Localization.Languages.Get_Name_By_Alpha3(sStream.Language)
                                        Next
                                    End If
                                End If
                                Return xmlEp
                            End Using
                        ElseIf rMatches.Count > 1 Then
                            For Each xmlReg As Match In rMatches
                                Using xmlRead As StringReader = New StringReader(xmlReg.Value)
                                    xmlEp = DirectCast(xmlSer.Deserialize(xmlRead), MediaContainers.MainDetails)
                                    xmlEp = CleanNFO_TVEpisodes(xmlEp)
                                    If xmlEp.Episode = episode AndAlso xmlEp.Season = season Then
                                        xmlSer = Nothing
                                        Return xmlEp
                                    End If
                                End Using
                            Next
                        End If
                    End Using

                Else
                    'not really anything else to do with non-conforming nfos aside from rename them
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNFO_TVEpisode(sPath, True)
                    End If
                End If

            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)

                'not really anything else to do with non-conforming nfos aside from rename them
                If Not Master.eSettings.GeneralOverwriteNfo Then
                    RenameNonConfNFO_TVEpisode(sPath, True)
                End If
            End Try
        End If

        Return New MediaContainers.MainDetails
    End Function

    Public Shared Function LoadFromNFO_TVEpisode(ByVal sPath As String, ByVal season As Integer, ByVal aired As String) As MediaContainers.MainDetails
        Dim xmlSer = CreateOverrider(Enums.ContentType.TVEpisode)
        Dim xmlEp As New MediaContainers.MainDetails

        If Not String.IsNullOrEmpty(sPath) AndAlso season >= -1 Then
            Try
                If File.Exists(sPath) AndAlso Path.GetExtension(sPath).ToLower = ".nfo" Then
                    'better way to read multi-root xml??
                    Using xmlSR As StreamReader = New StreamReader(sPath)
                        Dim xmlStr As String = xmlSR.ReadToEnd
                        Dim rMatches As MatchCollection = Regex.Matches(xmlStr, "<episodedetails.*?>.*?</episodedetails>", RegexOptions.IgnoreCase Or RegexOptions.Singleline Or RegexOptions.IgnorePatternWhitespace)
                        If rMatches.Count = 1 Then
                            'only one episodedetail... assume it's the proper one
                            Using xmlRead As StringReader = New StringReader(rMatches(0).Value)
                                xmlEp = DirectCast(xmlSer.Deserialize(xmlRead), MediaContainers.MainDetails)
                                xmlEp = CleanNFO_TVEpisodes(xmlEp)
                                xmlSer = Nothing
                                If xmlEp.FileInfoSpecified Then
                                    If xmlEp.FileInfo.StreamDetails.AudioSpecified Then
                                        For Each aStream In xmlEp.FileInfo.StreamDetails.Audio.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                                            aStream.LongLanguage = Localization.Languages.Get_Name_By_Alpha3(aStream.Language)
                                        Next
                                    End If
                                    If xmlEp.FileInfo.StreamDetails.SubtitleSpecified Then
                                        For Each sStream In xmlEp.FileInfo.StreamDetails.Subtitle.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                                            sStream.LongLanguage = Localization.Languages.Get_Name_By_Alpha3(sStream.Language)
                                        Next
                                    End If
                                End If
                                Return xmlEp
                            End Using
                        ElseIf rMatches.Count > 1 Then
                            For Each xmlReg As Match In rMatches
                                Using xmlRead As StringReader = New StringReader(xmlReg.Value)
                                    xmlEp = DirectCast(xmlSer.Deserialize(xmlRead), MediaContainers.MainDetails)
                                    xmlEp = CleanNFO_TVEpisodes(xmlEp)
                                    If xmlEp.Aired = aired AndAlso xmlEp.Season = season Then
                                        xmlSer = Nothing
                                        Return xmlEp
                                    End If
                                End Using
                            Next
                        End If
                    End Using

                Else
                    'not really anything else to do with non-conforming nfos aside from rename them
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNFO_TVEpisode(sPath, True)
                    End If
                End If

            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)

                'not really anything else to do with non-conforming nfos aside from rename them
                If Not Master.eSettings.GeneralOverwriteNfo Then
                    RenameNonConfNFO_TVEpisode(sPath, True)
                End If
            End Try
        End If

        Return New MediaContainers.MainDetails
    End Function

    Public Shared Function LoadFromNFO_TVShow(ByVal sPath As String) As MediaContainers.MainDetails
        Dim xmlSer As XmlSerializer = Nothing
        Dim xmlShow As New MediaContainers.MainDetails

        If Not String.IsNullOrEmpty(sPath) Then
            Try
                If File.Exists(sPath) AndAlso Path.GetExtension(sPath).ToLower = ".nfo" Then
                    Using xmlSR As StreamReader = New StreamReader(sPath)
                        xmlSer = CreateOverrider(Enums.ContentType.TVShow)
                        xmlShow = DirectCast(xmlSer.Deserialize(xmlSR), MediaContainers.MainDetails)
                        xmlShow = CleanNFO_TVShow(xmlShow)
                    End Using
                Else
                    'not really anything else to do with non-conforming nfos aside from rename them
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNFO_TVShow(sPath)
                    End If
                End If

            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)

                'not really anything else to do with non-conforming nfos aside from rename them
                If Not Master.eSettings.GeneralOverwriteNfo Then
                    RenameNonConfNFO_TVShow(sPath)
                End If
            End Try

            Try
                Dim params As New List(Of Object)(New Object() {xmlShow})
                Dim doContinue As Boolean = True
                Addons.Instance.RunGeneric(Enums.AddonEventType.OnNFORead_TVShow, params, doContinue, False)

            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            If xmlSer IsNot Nothing Then
                xmlSer = Nothing
            End If
        End If

        Return xmlShow
    End Function

    Private Shared Sub RenameNonConfNFO_Movie(ByVal sPath As String, ByVal isChecked As Boolean)
        Try
            If isChecked OrElse Not IsConformingNFO_Movie(sPath) Then
                If isChecked OrElse File.Exists(sPath) Then
                    RenameToInfo(sPath)
                End If
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Shared Sub RenameNonConfNFO_TVEpisode(ByVal sPath As String, ByVal isChecked As Boolean)
        Try
            If File.Exists(sPath) AndAlso Not IsConformingNFO_TVEpisode(sPath) Then
                RenameToInfo(sPath)
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Shared Sub RenameNonConfNFO_TVShow(ByVal sPath As String)
        Try
            If File.Exists(sPath) AndAlso Not IsConformingNFO_TVShow(sPath) Then
                RenameToInfo(sPath)
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Shared Sub RenameToInfo(ByVal sPath As String)
        Try
            Dim i As Integer = 1
            Dim strNewName As String = String.Concat(FileUtils.Common.RemoveExtFromPath(sPath), ".info")
            'in case there is already a .info file
            If File.Exists(strNewName) Then
                Do
                    strNewName = String.Format("{0}({1}).info", FileUtils.Common.RemoveExtFromPath(sPath), i)
                    i += 1
                Loop While File.Exists(strNewName)
                strNewName = String.Format("{0}({1}).info", FileUtils.Common.RemoveExtFromPath(sPath), i)
            End If
            My.Computer.FileSystem.RenameFile(sPath, Path.GetFileName(strNewName))
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Sub SaveToNFO_Movie(ByRef dbElement As Database.DBElement, ByVal ForceFileCleanup As Boolean)
        Try
            Try
                Dim params As New List(Of Object)(New Object() {dbElement})
                Dim doContinue As Boolean = True
                Addons.Instance.RunGeneric(Enums.AddonEventType.OnNFOSave_Movie, params, doContinue, False)
                If Not doContinue Then Return
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            If dbElement.FilenameSpecified Then
                'cleanup old NFOs if needed
                If ForceFileCleanup Then DeleteNFO_Movie(dbElement, ForceFileCleanup)

                'Create a clone of MediaContainer to prevent changes on database data that only needed in NFO
                Dim tMovie As MediaContainers.MainDetails = CType(dbElement.MainDetails.CloneDeep, MediaContainers.MainDetails)

                Dim xmlSer = CreateOverrider(Enums.ContentType.Movie)
                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True

                'YAMJ support
                If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieNFOYAMJ Then
                    If tMovie.UniqueIDs.TMDbIdSpecified Then
                        tMovie.UniqueIDs.TMDbId = -1
                    End If
                End If

                'digit grouping symbol for Votes count
                If Master.eSettings.Options.Global.DigitGrpSymbolVotesEnabled Then
                    If tMovie.VotesSpecified Then
                        Dim vote As String = Double.Parse(tMovie.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                        If vote IsNot Nothing Then tMovie.Votes = vote
                    End If
                End If

                For Each a In FileUtils.FileNames.Movie(dbElement, Enums.ModifierType.MainNFO)
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNFO_Movie(a, False)
                    End If

                    doesExist = File.Exists(a)
                    If Not doesExist OrElse (Not CBool(File.GetAttributes(a) And FileAttributes.ReadOnly)) Then
                        If doesExist Then
                            fAtt = File.GetAttributes(a)
                            Try
                                File.SetAttributes(a, FileAttributes.Normal)
                            Catch ex As Exception
                                fAttWritable = False
                            End Try
                        End If
                        Using xmlSW As New StreamWriter(a)
                            dbElement.NfoPath = a
                            xmlSer.Serialize(xmlSW, tMovie)
                        End Using
                        If doesExist And fAttWritable Then File.SetAttributes(a, fAtt)
                    End If
                Next
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Sub SaveToNFO_Movieset(ByRef dbElement As Database.DBElement)
        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {moviesetToSave})
            '    Dim doContinue As Boolean = True
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieSetNFOSave, params, doContinue, False)
            '    If Not doContinue Then Return
            'Catch ex As Exception
            'End Try

            If Not String.IsNullOrEmpty(dbElement.MainDetails.Title) Then
                If dbElement.MainDetails.Title_HasChanged Then DeleteNFO_MovieSet(dbElement, False, True)

                Dim xmlSer = CreateOverrider(Enums.ContentType.MovieSet)
                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True

                For Each a In FileUtils.FileNames.Movieset(dbElement, Enums.ModifierType.MainNFO)
                    'If Not Master.eSettings.GeneralOverwriteNfo Then
                    '    RenameNonConfNfo(a, False)
                    'End If

                    doesExist = File.Exists(a)
                    If Not doesExist OrElse (Not CBool(File.GetAttributes(a) And FileAttributes.ReadOnly)) Then
                        If doesExist Then
                            fAtt = File.GetAttributes(a)
                            Try
                                File.SetAttributes(a, FileAttributes.Normal)
                            Catch ex As Exception
                                fAttWritable = False
                            End Try
                        End If
                        Using xmlSW As New StreamWriter(a)
                            dbElement.NfoPath = a
                            xmlSer.Serialize(xmlSW, dbElement.MainDetails)
                        End Using
                        If doesExist And fAttWritable Then File.SetAttributes(a, fAtt)
                    End If
                Next
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Sub SaveToNFO_TVEpisode(ByRef dbElement As Database.DBElement)
        Try
            If dbElement.FilenameSpecified Then
                'Create a clone of MediaContainer to prevent changes on database data that only needed in NFO
                Dim tTVEpisode As MediaContainers.MainDetails = CType(dbElement.MainDetails.CloneDeep, MediaContainers.MainDetails)

                Dim xmlSer = CreateOverrider(Enums.ContentType.TVEpisode)

                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True
                Dim EpList As New List(Of MediaContainers.MainDetails)
                Dim sBuilder As New StringBuilder

                For Each a In FileUtils.FileNames.TVEpisode(dbElement, Enums.ModifierType.EpisodeNFO)
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNFO_TVEpisode(a, False)
                    End If

                    doesExist = File.Exists(a)
                    If Not doesExist OrElse (Not CBool(File.GetAttributes(a) And FileAttributes.ReadOnly)) Then

                        If doesExist Then
                            fAtt = File.GetAttributes(a)
                            Try
                                File.SetAttributes(a, FileAttributes.Normal)
                            Catch ex As Exception
                                fAttWritable = False
                            End Try
                        End If

                        Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLCommand.CommandText = "SELECT idEpisode FROM episode WHERE idEpisode <> (?) AND idFile IN (SELECT idFile FROM files WHERE strFilename = (?)) ORDER BY Episode"
                            Dim parID As SQLite.SQLiteParameter = SQLCommand.Parameters.Add("parID", DbType.Int64, 0, "idEpisode")
                            Dim parFilename As SQLite.SQLiteParameter = SQLCommand.Parameters.Add("parFilename", DbType.String, 0, "strFilename")

                            parID.Value = dbElement.ID
                            parFilename.Value = dbElement.Filename

                            Using SQLreader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader
                                While SQLreader.Read
                                    EpList.Add(Master.DB.Load_TVEpisode(Convert.ToInt64(SQLreader("idEpisode")), False).MainDetails)
                                End While
                            End Using

                            EpList.Add(tTVEpisode)

                            Dim NS As New XmlSerializerNamespaces
                            NS.Add(String.Empty, String.Empty)

                            For Each tvEp In EpList.OrderBy(Function(s) s.Season).OrderBy(Function(e) e.Episode)

                                'digit grouping symbol for Votes count
                                If Master.eSettings.Options.Global.DigitGrpSymbolVotesEnabled Then
                                    If tvEp.VotesSpecified Then
                                        Dim vote As String = Double.Parse(tvEp.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                                        If vote IsNot Nothing Then tvEp.Votes = vote
                                    End If
                                End If

                                'removing <displayepisode> and <displayseason> if disabled
                                If Not Master.eSettings.TVScraperUseDisplaySeasonEpisode Then
                                    tvEp.DisplayEpisode = -1
                                    tvEp.DisplaySeason = -1
                                End If

                                Using xmlSW As New Utf8StringWriter
                                    xmlSer.Serialize(xmlSW, tvEp, NS)
                                    If sBuilder.Length > 0 Then
                                        sBuilder.Append(Environment.NewLine)
                                        xmlSW.GetStringBuilder.Remove(0, xmlSW.GetStringBuilder.ToString.IndexOf(Environment.NewLine) + 1)
                                    End If
                                    sBuilder.Append(xmlSW.ToString)
                                End Using
                            Next

                            dbElement.NfoPath = a

                            If sBuilder.Length > 0 Then
                                Using fSW As New StreamWriter(a)
                                    fSW.Write(sBuilder.ToString)
                                End Using
                            End If
                        End Using
                        If doesExist And fAttWritable Then File.SetAttributes(a, fAtt)
                    End If
                Next
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Sub SaveToNFO_TVShow(ByRef dbElement As Database.DBElement)
        Try
            Dim params As New List(Of Object)(New Object() {dbElement})
            Dim doContinue As Boolean = True
            Addons.Instance.RunGeneric(Enums.AddonEventType.OnNFOSave_TVShow, params, doContinue, False)
            If Not doContinue Then Return
        Catch ex As Exception
        End Try

        Try
            If dbElement.ShowPathSpecified Then
                'Create a clone of MediaContainer to prevent changes on database data that only needed in NFO
                Dim tTVShow As MediaContainers.MainDetails = CType(dbElement.MainDetails.CloneDeep, MediaContainers.MainDetails)

                Dim xmlSer = CreateOverrider(Enums.ContentType.Movie)
                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True

                'TODO: Boxee support
                'If Master.eSettings.TVUseBoxee Then
                '    If tTVShow.UniqueIDs.TVDbIdSpecified() Then
                '        tTVShow.BoxeeTVDb = tTVShow.UniqueIDs.TVDbId
                '        tTVShow.BlankId()
                '    End If
                'End If

                'digit grouping symbol for Votes count
                If Master.eSettings.Options.Global.DigitGrpSymbolVotesEnabled Then
                    If tTVShow.VotesSpecified Then
                        Dim vote As String = Double.Parse(tTVShow.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                        If vote IsNot Nothing Then tTVShow.Votes = vote
                    End If
                End If

                For Each a In FileUtils.FileNames.TVShow(dbElement, Enums.ModifierType.MainNFO)
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNFO_TVShow(a)
                    End If

                    doesExist = File.Exists(a)
                    If Not doesExist OrElse (Not CBool(File.GetAttributes(a) And FileAttributes.ReadOnly)) Then

                        If doesExist Then
                            fAtt = File.GetAttributes(a)
                            Try
                                File.SetAttributes(a, FileAttributes.Normal)
                            Catch ex As Exception
                                fAttWritable = False
                            End Try
                        End If

                        Using xmlSW As New StreamWriter(a)
                            dbElement.NfoPath = a
                            xmlSer.Serialize(xmlSW, tTVShow)
                        End Using

                        If doesExist And fAttWritable Then File.SetAttributes(a, fAtt)
                    End If
                Next
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Class NonConf

#Region "Properties"

        Public Property IMDBID() As String = String.Empty

        Public Property Text() As String = String.Empty

#End Region 'Properties

    End Class

    Public NotInheritable Class Utf8StringWriter
        Inherits StringWriter
        Public Overloads Overrides ReadOnly Property Encoding() As Encoding
            Get
                Return Encoding.UTF8
            End Get
        End Property
    End Class

#End Region 'Nested Types

End Class