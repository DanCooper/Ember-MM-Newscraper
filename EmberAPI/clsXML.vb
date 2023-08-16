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
Imports System.Drawing
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization

Public Class APIXML

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared AudioCodecMappings As New XmlSimpleMapping("core.mapping.audiocodecs.xml")
    Public Shared CertificationMappings As New XmlSimpleMapping("core.mapping.certifications.xml")
    Public Shared CountryMappings As New XmlSimpleMapping("core.mapping.countries.xml")
    Public Shared EditionMappings As New XmlRegexMapping("core.mapping.editions.xml")
    Public Shared Flags As New List(Of Flag)
    Public Shared GenreIcons As New List(Of String)
    Public Shared GenreMappings As New XmlGenreMapping("core.mapping.genres.xml")
    Public Shared LanguageIcons As New Dictionary(Of String, String)
    Public Shared RatingXml As New XmlRatings
    Public Shared ScraperLanguages As New XmlScraperLanguages
    Public Shared StatusMappings As New XmlSimpleMapping("core.mapping.status.xml")
    Public Shared StudioIcons As New Dictionary(Of String, String)
    Public Shared StudioMappings As New XmlSimpleMapping("core.mapping.studios.xml")
    Public Shared VideoCodecMappings As New XmlSimpleMapping("core.mapping.videocodecs.xml")
    Public Shared VideoSourceByExtensionMappings As New XmlSimpleMapping("core.mapping.videosourcesbyextension.xml")
    Public Shared VideoSourceByNameMappings As New XmlRegexMapping("core.mapping.videosourcesbyname.xml")

#End Region 'Fields

#Region "Methods"

    Public Shared Sub CacheAll()
        Dim objStreamReader As StreamReader
        Try
            Dim fPath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Flags")
            If Directory.Exists(fPath) Then
                Dim cFileName As String = String.Empty
                Dim fType As FlagType = FlagType.Unknown
                Try
                    For Each lFile As String In Directory.GetFiles(fPath, "*.png")
                        cFileName = Path.GetFileNameWithoutExtension(lFile)
                        If cFileName.Contains("_") Then
                            fType = GetFlagTypeFromString(cFileName.Substring(0, cFileName.IndexOf("_")))
                            If Not fType = FlagType.Unknown Then
                                Using fsImage As New FileStream(lFile, FileMode.Open, FileAccess.Read)
                                    Flags.Add(New Flag With {.Name = cFileName.Remove(0, cFileName.IndexOf("_") + 1), .Image = Image.FromStream(fsImage), .Path = lFile, .Type = fType})
                                End Using
                            End If
                        End If
                    Next
                Catch
                End Try
            End If

            'AudioCodec mappings
            If File.Exists(AudioCodecMappings.FullName) Then
                AudioCodecMappings.Load()
            Else
                'try to load it from advancedsettings.xml, move it to the new core.mapping.audiocodecs.xml and remove it from advancedsettings.xml,
                'otherwise load the default mapping
                Dim oldAudioFormatConverts = Master.eAdvancedSettings.GetComplexSetting("AudioFormatConverts", "*EmberAPP")
                If oldAudioFormatConverts IsNot Nothing Then
                    For Each item In oldAudioFormatConverts
                        AudioCodecMappings.Mappings.Add(New SimpleMapping With {
                                                       .Input = item.Name,
                                                       .MappedTo = item.Value
                                                       })
                    Next
                    AudioCodecMappings.Save()
                    Master.eAdvancedSettings.RemoveComplexSetting("AudioFormatConverts", "*EmberAPP")
                    Master.eAdvancedSettings.Save()
                Else
                    'loads the defaults
                    AudioCodecMappings.Load()
                End If
            End If

            'Certification mappings
            CertificationMappings.Load()

            'Country mappings
            CountryMappings.Load()

            'Country/Language icons
            Dim lPath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Countries", Path.DirectorySeparatorChar, "Countries.xml")
            If Directory.Exists(Directory.GetParent(lPath).FullName) Then
                Try
                    'get all images in the main folder
                    For Each lFile As String In Directory.GetFiles(Directory.GetParent(lPath).FullName, "*.png")
                        LanguageIcons.Add(Path.GetFileNameWithoutExtension(lFile).ToLower, lFile)
                    Next
                Catch
                End Try
            End If

            'Edition mappings
            EditionMappings.Load()

            'Genre mappings
            If File.Exists(GenreMappings.FullName) Then
                GenreMappings.Load()
            ElseIf File.Exists(Path.Combine(Master.SettingsPath, "core.genres.xml")) Then
                File.Move(Path.Combine(Master.SettingsPath, "core.genres.xml"), Path.Combine(Master.SettingsPath, "core.mapping.genres.xml"))
                GenreMappings.Load()
            End If

            'Genre images
            If Directory.Exists(Directory.GetParent(String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Genres", Path.DirectorySeparatorChar)).FullName) Then
                Try
                    GenreIcons.AddRange(Directory.GetFiles(Directory.GetParent(String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Genres", Path.DirectorySeparatorChar)).FullName, "*.jpg"))
                Catch
                End Try
                GenreIcons = GenreIcons.ConvertAll(Function(s) s.ToLower)
            End If

            'Status mappings
            StatusMappings.Load()

            'Studio mappings
            StudioMappings.Load()

            'Studio icons
            Dim sPath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Studios")
            If Directory.Exists(sPath) Then
                Try
                    'get all images in the main folder
                    For Each lFile As String In Directory.GetFiles(sPath, "*.png")
                        StudioIcons.Add(Path.GetFileNameWithoutExtension(lFile).ToLower, lFile)
                    Next

                    'now get all images in sub folders
                    For Each iDir As String In Directory.GetDirectories(sPath)
                        For Each lFile As String In Directory.GetFiles(iDir, "*.png")
                            'hard code "\" here, then replace when retrieving images
                            StudioIcons.Add(String.Concat(Directory.GetParent(iDir).Name, "\", Path.GetFileNameWithoutExtension(lFile).ToLower), lFile)
                        Next
                    Next
                Catch
                End Try
            End If

            'Rating icons
            Dim rPath As String = Path.Combine(Master.SettingsPath, "Ratings.xml")
            If File.Exists(rPath) Then
                objStreamReader = New StreamReader(rPath)
                Dim xRatings As New XmlSerializer(RatingXml.GetType)

                RatingXml = CType(xRatings.Deserialize(objStreamReader), XmlRatings)
                objStreamReader.Close()
            Else
                Dim rPathD As String = FileUtils.Common.ReturnSettingsFile("Defaults", "DefaultRatings.xml")
                objStreamReader = New StreamReader(rPathD)
                Dim xRatings As New XmlSerializer(RatingXml.GetType)

                RatingXml = CType(xRatings.Deserialize(objStreamReader), XmlRatings)
                objStreamReader.Close()

                Try
                    File.Copy(rPathD, rPath)
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            End If

            'Scraper languages
            ScraperLanguages.Load()

            'VideoCodec mappings
            If File.Exists(VideoCodecMappings.FullName) Then
                VideoCodecMappings.Load()
            Else
                'try to load it from advancedsettings.xml, move it to the new core.mapping.videocodecs.xml and remove it from advancedsettings.xml,
                'otherwise load the default mapping
                Dim oldVideoFormatConverts = Master.eAdvancedSettings.GetComplexSetting("VideoFormatConverts", "*EmberAPP")
                If oldVideoFormatConverts IsNot Nothing Then
                    For Each item In oldVideoFormatConverts
                        VideoCodecMappings.Mappings.Add(New SimpleMapping With {
                                                       .Input = item.Name,
                                                       .MappedTo = item.Value
                                                       })
                    Next
                    VideoCodecMappings.Save()
                    Master.eAdvancedSettings.RemoveComplexSetting("VideoFormatConverts", "*EmberAPP")
                    Master.eAdvancedSettings.Save()
                Else
                    'loads the defaults
                    VideoCodecMappings.Load()
                End If
            End If

            'VideoSourceByExtension mappings
            If File.Exists(VideoSourceByExtensionMappings.FullName) Then
                VideoSourceByExtensionMappings.Load()
            ElseIf Master.eAdvancedSettings.Settings.Where(Function(f) f.Name.StartsWith("VideoSourceByExtension:")).Count > 0 Then
                'try to load it from advancedsettings.xml, move it to the new core.mapping.videosourcebyextension.xml and remove it from advancedsettings.xml,
                'otherwise load the default mapping
                While Master.eAdvancedSettings.Settings.Where(Function(f) f.Name.StartsWith("VideoSourceByExtension:")).Count > 0
                    VideoSourceByExtensionMappings.Mappings.Add(New SimpleMapping With {
                                                                .Input = Master.eAdvancedSettings.Settings.Where(Function(f) f.Name.StartsWith("VideoSourceByExtension:"))(0).Name.Substring(23),
                                                                .MappedTo = Master.eAdvancedSettings.Settings.Where(Function(f) f.Name.StartsWith("VideoSourceByExtension:"))(0).Value
                                                                })
                    Master.eAdvancedSettings.RemoveStringSetting(Master.eAdvancedSettings.Settings.Where(Function(f) f.Name.StartsWith("VideoSourceByExtension:"))(0).Name, "*EmberAPP")
                End While
                For Each item In VideoSourceByExtensionMappings.Mappings
                    If Not item.Input.StartsWith(".") Then item.Input = String.Concat(".", item.Input)
                Next
                VideoSourceByExtensionMappings.Save()
                Master.eAdvancedSettings.RemoveStringSetting("VideoSourceByExtension", "*EmberAPP")
                Master.eAdvancedSettings.Save()
            Else
                'loads the defaults
                VideoSourceByExtensionMappings.Load()
            End If

            'VideoSourceByName mappings
            If File.Exists(VideoSourceByNameMappings.FullName) Then
                VideoSourceByNameMappings.Load()
            Else
                'try to load it from advancedsettings.xml, move it to the new core.mapping.videosourcebyname.xml and remove it from advancedsettings.xml,
                'otherwise load the default mapping
                Dim oldVideoSourceMapping = Master.eAdvancedSettings.GetComplexSetting("VideoSourceMapping", "*EmberAPP")
                If oldVideoSourceMapping IsNot Nothing Then
                    For Each item In oldVideoSourceMapping
                        VideoSourceByNameMappings.Mappings.Add(New RegexMapping With {
                                                               .RegExp = item.Name,
                                                               .Result = item.Value
                                                               })
                    Next
                    VideoSourceByNameMappings.Save()
                    Master.eAdvancedSettings.RemoveComplexSetting("VideoSourceMapping", "*EmberAPP")
                    Master.eAdvancedSettings.Save()
                Else
                    'loads the defaults
                    VideoSourceByNameMappings.Load()
                End If
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Function GetAVImages(ByVal fiAV As MediaContainers.Fileinfo, ByVal preferredLanguage As String, ByVal contentType As Enums.ContentType, ByVal videoSource As String) As Image()
        Dim iReturn(19) As Image
        Dim tVideo As MediaContainers.Video = MetaData.GetBestVideo(fiAV)
        Dim tAudio As MediaContainers.Audio = MetaData.GetBestAudio(fiAV, preferredLanguage, contentType)

        If Flags.Count > 0 OrElse LanguageIcons.Count > 0 Then
            Try
                Dim vRes As String = NFO.GetResFromDimensions(tVideo).ToLower
                Dim vresFlag As Flag = Flags.FirstOrDefault(Function(f) f.Name = vRes AndAlso f.Type = FlagType.VideoResolution)
                If vresFlag IsNot Nothing Then
                    iReturn(0) = vresFlag.Image
                Else
                    vresFlag = Flags.FirstOrDefault(Function(f) f.Name = "unknown" AndAlso f.Type = FlagType.VideoResolution)
                    If vresFlag IsNot Nothing Then
                        iReturn(0) = vresFlag.Image
                    Else
                        iReturn(0) = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))
                    End If
                End If

                Dim vSource As String = videoSource 'GetFileSource(fName)
                Dim vSourceFlag As Flag = Flags.FirstOrDefault(Function(f) f.Name.ToLower = vSource.ToLower AndAlso f.Type = FlagType.VideoSource)
                If vSourceFlag IsNot Nothing Then
                    iReturn(1) = vSourceFlag.Image
                Else
                    vSourceFlag = Flags.FirstOrDefault(Function(f) f.Name = "unknown" AndAlso f.Type = FlagType.VideoSource)
                    If vSourceFlag IsNot Nothing Then
                        iReturn(1) = vSourceFlag.Image
                    Else
                        iReturn(1) = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))
                    End If
                End If

                Dim vcodecFlag As Flag = Flags.FirstOrDefault(Function(f) f.Name = tVideo.Codec.ToLower AndAlso f.Type = FlagType.VideoCodec)
                If vcodecFlag IsNot Nothing Then
                    iReturn(2) = vcodecFlag.Image
                Else
                    vcodecFlag = Flags.FirstOrDefault(Function(f) f.Name = "unknown" AndAlso f.Type = FlagType.VideoCodec)
                    If vcodecFlag IsNot Nothing Then
                        iReturn(2) = vcodecFlag.Image
                    Else
                        iReturn(2) = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))
                    End If
                End If

                If tVideo.MultiViewCountSpecified Then
                    Dim vchanFlag As Flag = Flags.FirstOrDefault(Function(f) f.Name = tVideo.MultiViewCount.ToString AndAlso f.Type = FlagType.VideoChan)
                    If vchanFlag IsNot Nothing Then
                        iReturn(19) = vchanFlag.Image
                    End If
                End If

                Dim acodecFlag As Flag = Flags.FirstOrDefault(Function(f) f.Name = tAudio.Codec.ToLower AndAlso f.Type = FlagType.AudioCodec)
                If acodecFlag IsNot Nothing Then
                    If tAudio.HasPreferred Then
                        Dim acodecFlagTemp As Image = acodecFlag.Image
                        iReturn(3) = ImageUtils.SetOverlay(acodecFlagTemp, 64, 44, Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "HasLanguage.png")), 4)
                    Else
                        iReturn(3) = acodecFlag.Image
                    End If
                Else
                    acodecFlag = Flags.FirstOrDefault(Function(f) f.Name = "unknown" AndAlso f.Type = FlagType.AudioCodec)
                    If acodecFlag IsNot Nothing Then
                        If tAudio.HasPreferred Then
                            Dim acodecFlagTemp As Image = acodecFlag.Image
                            iReturn(3) = ImageUtils.SetOverlay(acodecFlagTemp, 64, 44, Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "HasLanguage.png")), 4)
                        Else
                            iReturn(3) = acodecFlag.Image
                        End If
                    Else
                        If tAudio.HasPreferred Then
                            iReturn(3) = ImageUtils.SetOverlay(Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "unknown.png")), 64, 44, Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "HasLanguage.png")), 4)
                        Else
                            iReturn(3) = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))
                        End If
                    End If
                End If

                Dim achanFlag As Flag = Flags.FirstOrDefault(Function(f) f.Name = tAudio.Channels.ToString AndAlso f.Type = FlagType.AudioChan)
                If achanFlag IsNot Nothing Then
                    iReturn(4) = achanFlag.Image
                Else
                    achanFlag = Flags.FirstOrDefault(Function(f) f.Name = "unknown" AndAlso f.Type = FlagType.AudioChan)
                    If achanFlag IsNot Nothing Then
                        iReturn(4) = achanFlag.Image
                    Else
                        iReturn(4) = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))
                    End If
                End If

                If Master.eSettings.GeneralShowLangFlags Then
                    'Audio Language Flags has range iReturn(5) to iReturn(11)
                    Dim aIcon As Integer = 5
                    Dim hasMoreA As Boolean = fiAV.StreamDetails.Audio.Count > 7
                    For i = 0 To fiAV.StreamDetails.Audio.Count - 1
                        If If(hasMoreA, aIcon > 10, aIcon > 11) Then Exit For
                        If fiAV.StreamDetails.Audio(i).LanguageSpecified Then
                            Dim aLangFlag As Image = GetLanguageImage(fiAV.StreamDetails.Audio(i).Language)
                            aLangFlag.Tag = String.Format("{0}: {1}", Master.eLang.GetString(618, "Audio Stream"), i)
                            aLangFlag.Tag = String.Format("{0}{1}{2}: {3}", aLangFlag.Tag, Environment.NewLine, Master.eLang.GetString(610, "Language"), fiAV.StreamDetails.Audio(i).LongLanguage)
                            If fiAV.StreamDetails.Audio(i).CodecSpecified Then aLangFlag.Tag = String.Format("{0}{1}{2}: {3}", aLangFlag.Tag, Environment.NewLine, Master.eLang.GetString(604, "Codec"), fiAV.StreamDetails.Audio(i).Codec)
                            If fiAV.StreamDetails.Audio(i).ChannelsSpecified Then aLangFlag.Tag = String.Format("{0}{1}{2}: {3}", aLangFlag.Tag, Environment.NewLine, Master.eLang.GetString(611, "Channels"), fiAV.StreamDetails.Audio(i).Channels)
                            If fiAV.StreamDetails.Audio(i).BitrateSpecified Then aLangFlag.Tag = String.Format("{0}{1}{2}: {3}", aLangFlag.Tag, Environment.NewLine, Master.eLang.GetString(1158, "Bitrate"), fiAV.StreamDetails.Audio(i).Bitrate)
                            iReturn(aIcon) = aLangFlag
                            aIcon += 1
                        Else
                            Dim aLangFlag As Image = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "DefaultLanguage.png"))
                            aLangFlag.Tag = String.Format("{0}: {1}", Master.eLang.GetString(618, "Audio Stream"), i)
                            aLangFlag.Tag = String.Format("{0}{1}{2}: {3}", aLangFlag.Tag, Environment.NewLine, Master.eLang.GetString(610, "Language"), Master.eLang.GetString(138, "Unknown"))
                            If fiAV.StreamDetails.Audio(i).CodecSpecified Then aLangFlag.Tag = String.Format("{0}{1}{2}: {3}", aLangFlag.Tag, Environment.NewLine, Master.eLang.GetString(604, "Codec"), fiAV.StreamDetails.Audio(i).Codec)
                            If fiAV.StreamDetails.Audio(i).ChannelsSpecified Then aLangFlag.Tag = String.Format("{0}{1}{2}: {3}", aLangFlag.Tag, Environment.NewLine, Master.eLang.GetString(611, "Channels"), fiAV.StreamDetails.Audio(i).Channels)
                            If fiAV.StreamDetails.Audio(i).BitrateSpecified Then aLangFlag.Tag = String.Format("{0}{1}{2}: {3}", aLangFlag.Tag, Environment.NewLine, Master.eLang.GetString(1158, "Bitrate"), fiAV.StreamDetails.Audio(i).Bitrate)
                            iReturn(aIcon) = aLangFlag
                            aIcon += 1
                        End If
                    Next
                    'If there are more than 7 languages ​​a plus icon appears instead of the 7th language.
                    If hasMoreA Then
                        Dim pImage As Image
                        Dim pLang As String = String.Empty
                        For i = 7 To fiAV.StreamDetails.Audio.Count - 1
                            pLang = String.Concat(pLang, String.Format("{0}{1}", Environment.NewLine, fiAV.StreamDetails.Audio(i).LongLanguage))
                        Next
                        pImage = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "DefaultLanguageMore.png"))
                        pImage.Tag = pLang
                        iReturn(11) = pImage
                    End If

                    'Subtitles Language Flags has range iReturn(12) to iReturn(18)
                    Dim sIcon As Integer = 12
                    Dim hasMoreS As Boolean = fiAV.StreamDetails.Subtitle.Count > 7
                    For i = 0 To fiAV.StreamDetails.Subtitle.Count - 1
                        If If(hasMoreA, sIcon > 17, sIcon > 18) Then Exit For
                        If fiAV.StreamDetails.Subtitle(i).LanguageSpecified Then
                            Dim sLangFlag As Image = GetLanguageImage(fiAV.StreamDetails.Subtitle(i).Language)
                            sLangFlag.Tag = String.Format("{0}: {1}", Master.eLang.GetString(619, "Subtitle Stream"), i)
                            sLangFlag.Tag = String.Format("{0}{1}{2}: {3}", sLangFlag.Tag, Environment.NewLine, Master.eLang.GetString(610, "Language"), fiAV.StreamDetails.Subtitle(i).LongLanguage)
                            sLangFlag.Tag = String.Format("{0}{1}{2}: {3}", sLangFlag.Tag, Environment.NewLine, Master.eLang.GetString(1287, "Forced"),
                                                          If(fiAV.StreamDetails.Subtitle(i).Forced, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
                            iReturn(sIcon) = sLangFlag
                            sIcon += 1
                        Else
                            Dim sLangFlag As Image = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "DefaultLanguage.png"))
                            sLangFlag.Tag = String.Format("{0}: {1}", Master.eLang.GetString(619, "Subtitle Stream"), i)
                            sLangFlag.Tag = String.Format("{0}{1}{2}: {3}", sLangFlag.Tag, Environment.NewLine, Master.eLang.GetString(610, "Language"), Master.eLang.GetString(138, "Unknown"))
                            sLangFlag.Tag = String.Format("{0}{1}{2}: {3}", sLangFlag.Tag, Environment.NewLine, Master.eLang.GetString(1287, "Forced"),
                                                          If(fiAV.StreamDetails.Subtitle(i).Forced, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
                            iReturn(sIcon) = sLangFlag
                            aIcon += 1
                        End If
                    Next
                    'If there are more than 7 languages ​​a plus icon appears instead of the 7th language.
                    If hasMoreS Then
                        Dim pImage As Image
                        Dim pLang As String = String.Empty
                        For i = 7 To fiAV.StreamDetails.Subtitle.Count - 1
                            pLang = String.Concat(pLang, String.Format("{0}{1}", Environment.NewLine, fiAV.StreamDetails.Subtitle(i).LongLanguage))
                        Next
                        pImage = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "DefaultLanguageMore.png"))
                        pImage.Tag = pLang
                        iReturn(18) = pImage
                    End If
                End If

            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Else
            iReturn(0) = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))
            iReturn(1) = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))
            iReturn(2) = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))
            If tAudio.HasPreferred Then
                iReturn(3) = ImageUtils.SetOverlay(Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png")), 64, 44, Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "HasLanguage.png")), 4)
            Else
                iReturn(3) = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))
            End If
            iReturn(4) = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))
            'Audio Language Flags
            iReturn(5) = Nothing
            iReturn(6) = Nothing
            iReturn(7) = Nothing
            iReturn(8) = Nothing
            iReturn(9) = Nothing
            iReturn(10) = Nothing
            iReturn(11) = Nothing
            'Subtitle Language Flags
            iReturn(12) = Nothing
            iReturn(13) = Nothing
            iReturn(14) = Nothing
            iReturn(15) = Nothing
            iReturn(16) = Nothing
            iReturn(17) = Nothing
            iReturn(18) = Nothing
        End If

        Return iReturn
    End Function

    Public Shared Function GetLanguageImage(ByVal language As String) As Image
        Dim imgLanguage As Image = Nothing
        Dim sLang As String = String.Empty

        sLang = Localization.Languages.Get_Alpha2_By_Alpha3(language)

        If Not String.IsNullOrEmpty(sLang) Then
            If LanguageIcons.ContainsKey(sLang.ToLower) Then
                Using fsImage As New FileStream(LanguageIcons.Item(sLang.ToLower).Replace(Convert.ToChar("\"), Path.DirectorySeparatorChar), FileMode.Open, FileAccess.Read)
                    imgLanguage = Image.FromStream(fsImage)
                End Using
            End If
        End If

        If imgLanguage Is Nothing Then imgLanguage = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "DefaultLanguage.png"))

        Return imgLanguage
    End Function

    Public Shared Function GetVideoSource(ByVal filenamePath As String, ByVal isTV As Boolean) As String
        Dim fileName As String = String.Empty

        Try
            If FileUtils.Common.isVideoTS(filenamePath) Then
                Return "dvd"
            ElseIf FileUtils.Common.isBDRip(filenamePath) Then
                Return "bluray"
            ElseIf Path.GetFileName(filenamePath).ToLower = "video_ts.ifo" Then
                Return "dvd"
            Else
                If isTV Then
                    fileName = Path.GetFileName(filenamePath).ToLower
                Else
                    fileName = If(Master.eSettings.GeneralSourceFromFolder, String.Concat(Directory.GetParent(filenamePath).Name.ToLower, Path.DirectorySeparatorChar, Path.GetFileName(filenamePath).ToLower), Path.GetFileName(filenamePath).ToLower)
                End If
                Dim strResult As String = String.Empty
                'by filename or path
                If VideoSourceByNameMappings.RunRegex(fileName, strResult) Then
                    Return strResult
                End If
                'by file extension
                strResult = Path.GetExtension(fileName)
                If VideoSourceByExtensionMappings.RunMapping(strResult, False) Then
                    Return strResult
                End If
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return String.Empty
    End Function

    Public Shared Function GetGenreImage(ByVal genre As String) As Image
        Dim imgGenre As Image = Nothing
        Dim imgGenreStr As String = String.Empty
        Dim mePath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Genres")
        imgGenreStr = Path.Combine(mePath, GenreMappings.DefaultImage)

        Dim v = From e In GenreMappings.Genres.Where(Function(f) f.Name = genre)
        If v.Count > 0 Then
            imgGenreStr = Path.Combine(mePath, v(0).Image)
        End If
        Try

            If Not String.IsNullOrEmpty(imgGenreStr) AndAlso GenreIcons.Contains(imgGenreStr.ToLower) Then
                imgGenre = Image.FromFile(imgGenreStr)
            Else
                imgGenre = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "DefaultGenre.jpg"))
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return imgGenre
    End Function

    Public Shared Function GetGenreList() As String()
        Dim retGenre As New List(Of String)
        For Each mGenre In GenreMappings.Genres
            retGenre.Add(mGenre.Name)
        Next
        retGenre.Sort()
        Return retGenre.ToArray
    End Function

    Public Shared Function GetRatingFlag(ByVal ratingProvider As String) As Image
        Dim imgRating As Image = Nothing
        Dim ratingFlag = Flags.FirstOrDefault(Function(f) f.Name = ratingProvider AndAlso f.Type = FlagType.Rating)
        If ratingFlag IsNot Nothing Then
            Return ratingFlag.Image
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function GetRatingImage(ByVal rating As String) As Image
        Dim mePath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Ratings")
        Dim imgRating As Image = Nothing
        Dim imgRatingStr As String = String.Empty
        If Not rating = Master.eSettings.Movie.InformationSettings.MPAA.NotRatedValue Then
            Dim v = From e In RatingXml.Movies.Where(Function(f) f.Searchstring = rating)
            If v.Count > 0 Then
                imgRatingStr = Path.Combine(mePath, v(0).Icon)
            Else
                v = From e In RatingXml.Movies Where rating.ToLower.StartsWith(e.Searchstring.ToLower)
                If v.Count > 0 Then
                    imgRatingStr = Path.Combine(mePath, v(0).Icon)
                End If
            End If
        Else
            imgRatingStr = Path.Combine(mePath, "movienr.png")
        End If

        Try
            If Not String.IsNullOrEmpty(imgRatingStr) AndAlso File.Exists(imgRatingStr) Then
                Using fsImage As New FileStream(imgRatingStr, FileMode.Open, FileAccess.Read)
                    imgRating = Image.FromStream(fsImage)
                End Using
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return imgRating
    End Function

    Public Shared Function GetRatingList_Movie() As Object()
        Dim retRatings As New List(Of String)
        If Master.eSettings.Movie.InformationSettings.CertificationsForMPAA AndAlso Not Master.eSettings.Movie.InformationSettings.Certifications.Filter = Master.eLang.CommonWordsList.All Then
            Dim tCountry = Localization.Countries.Items.FirstOrDefault(Function(l) l.Alpha2 = Master.eSettings.Movie.InformationSettings.Certifications.Filter)
            If tCountry IsNot Nothing AndAlso Not String.IsNullOrEmpty(tCountry.Name) Then
                For Each r In RatingXml.Movies.FindAll(Function(f) f.Country.ToLower = tCountry.Name.ToLower)
                    retRatings.Add(r.Searchstring)
                Next
            End If
        Else
            For Each r In RatingXml.Movies.FindAll(Function(f) f.Country.ToLower = "usa")
                retRatings.Add(r.Searchstring)
            Next
        End If

        Return retRatings.ToArray
    End Function

    Public Shared Function GetRatingList_TV() As Object()
        Dim retRatings As New List(Of String)
        If Master.eSettings.TVScraperShowCertForMPAA AndAlso Not Master.eSettings.TVScraperShowCertCountry = Master.eLang.CommonWordsList.All Then
            Dim tCountry = Localization.Countries.Items.FirstOrDefault(Function(l) l.Alpha2 = Master.eSettings.TVScraperShowCertCountry)
            If tCountry IsNot Nothing AndAlso Not String.IsNullOrEmpty(tCountry.Name) Then
                For Each r In RatingXml.TV.FindAll(Function(f) f.Country.ToLower = tCountry.Name.ToLower)
                    retRatings.Add(r.Searchstring)
                Next
            End If
        Else
            For Each r In RatingXml.TV.FindAll(Function(f) f.Country.ToLower = "usa")
                retRatings.Add(r.Searchstring)
            Next
        End If

        Return retRatings.ToArray
    End Function

    Public Shared Function GetStudioImage(ByVal studio As String) As Image
        Dim imgStudio As Image = Nothing

        If StudioIcons.ContainsKey(studio.ToLower) Then
            Using fsImage As New FileStream(StudioIcons.Item(studio.ToLower).Replace(Convert.ToChar("\"), Path.DirectorySeparatorChar), FileMode.Open, FileAccess.Read)
                imgStudio = Image.FromStream(fsImage)
            End Using
        End If

        If imgStudio Is Nothing Then imgStudio = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))

        Return imgStudio
    End Function

    Public Shared Function GetTVRatingImage(ByVal rating As String) As Image
        Dim mePath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Ratings")
        Dim imgRating As Image = Nothing
        Dim imgRatingStr As String = String.Empty
        If Not rating = Master.eSettings.TVScraperShowMPAANotRated Then
            Dim v = From e In RatingXml.TV.Where(Function(f) f.Searchstring = rating)
            If v.Count > 0 Then
                imgRatingStr = Path.Combine(mePath, v(0).Icon)
            Else
                v = From e In RatingXml.TV Where rating.ToLower.StartsWith(e.Searchstring.ToLower)
                If v.Count > 0 Then
                    imgRatingStr = Path.Combine(mePath, v(0).Icon)
                End If
            End If
        Else
            imgRatingStr = Path.Combine(mePath, "tvnr.png")
        End If

        Try
            If Not String.IsNullOrEmpty(imgRatingStr) AndAlso File.Exists(imgRatingStr) Then
                Using fsImage As New FileStream(imgRatingStr, FileMode.Open, FileAccess.Read)
                    imgRating = Image.FromStream(fsImage)
                End Using
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return imgRating
    End Function

    Public Shared Function XmlToLowerCase(ByVal xml As String) As String
        Dim sMatches As MatchCollection = Regex.Matches(xml, "\<(.*?)\>", RegexOptions.IgnoreCase)
        For Each sMatch As Match In sMatches
            xml = xml.Replace(sMatch.Groups(1).Value, sMatch.Groups(1).Value.ToLower)
        Next
        Return xml
    End Function

    Public Shared Function GetFlagTypeFromString(ByVal type As String) As FlagType
        Select Case type
            Case "acodec"
                Return FlagType.AudioCodec
            Case "achan"
                Return FlagType.AudioChan
            Case "rating"
                Return FlagType.Rating
            Case "vchan"
                Return FlagType.VideoChan
            Case "vcodec"
                Return FlagType.VideoCodec
            Case "vres"
                Return FlagType.VideoResolution
            Case "vsource"
                Return FlagType.VideoSource
            Case Else
                Return FlagType.Unknown
        End Select
    End Function

#End Region 'Methods

#Region "Nested Types"

    Public Enum FlagType
        AudioChan
        AudioCodec
        Rating
        Unknown
        VideoChan
        VideoCodec
        VideoResolution
        VideoSource
    End Enum

    Public Class Flag

#Region "Properties"

        Public Property Name() As String = String.Empty

        Public Property Image() As Image = Nothing

        Public Property Path() As String = String.Empty

        Public Property Type() As FlagType = FlagType.Unknown

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class