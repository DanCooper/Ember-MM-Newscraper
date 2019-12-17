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

Imports EmberAPI
Imports NLog
Imports System.IO
Imports System.Xml.Serialization

Public Class MediaFlags

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private Shared _Flags_Countries As New Dictionary(Of String, String)
    Private Shared _Flags_Genres As New List(Of String)
    Private Shared _Flags_Ratings As New XMLRatings
    Private Shared _Flags_Studios As New Dictionary(Of String, String)

#End Region 'Fields

#Region "Properties"

    Public Shared ReadOnly Property AudioVideoFlags As List(Of Flag) = New List(Of Flag)

#End Region 'Properties

#Region "Methods"

    Public Shared Sub CacheFlags()
        Dim objStreamReader As StreamReader

        'Audio/Video flags
        Dim fPath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Flags")
        If Directory.Exists(fPath) Then
            Dim cFileName As String = String.Empty
            Dim fType As Flag.FlagType = Flag.FlagType.Unknown
            Try
                For Each lFile As String In Directory.GetFiles(fPath, "*.png")
                    cFileName = Path.GetFileNameWithoutExtension(lFile)
                    If cFileName.Contains("_") Then
                        fType = GetFlagTypeFromString(cFileName.Substring(0, cFileName.IndexOf("_")))
                        If Not fType = Flag.FlagType.Unknown Then
                            Using fsImage As New FileStream(lFile, FileMode.Open, FileAccess.Read)
                                AudioVideoFlags.Add(New Flag With {.Name = cFileName.Remove(0, cFileName.IndexOf("_") + 1), .Image = Image.FromStream(fsImage), .Path = lFile, .Type = fType})
                            End Using
                        End If
                    End If
                Next
            Catch
            End Try

            'Genre flags
            If Directory.Exists(Directory.GetParent(String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Genres", Path.DirectorySeparatorChar)).FullName) Then
                Try
                    _Flags_Genres.AddRange(Directory.GetFiles(Directory.GetParent(String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Genres", Path.DirectorySeparatorChar)).FullName, "*.jpg"))
                Catch
                End Try
                _Flags_Genres = _Flags_Genres.ConvertAll(Function(s) s.ToLower)
            End If

            'Language flags
            Dim lPath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Countries", Path.DirectorySeparatorChar, "Countries.xml")
            If Directory.Exists(Directory.GetParent(lPath).FullName) Then
                Try
                    'get all images in the main folder
                    For Each lFile As String In Directory.GetFiles(Directory.GetParent(lPath).FullName, "*.png")
                        _Flags_Countries.Add(Path.GetFileNameWithoutExtension(lFile).ToLower, lFile)
                    Next
                Catch
                End Try
            End If

            'Rating flags
            Dim rPath As String = Path.Combine(Master.SettingsPath, "Ratings.xml")
            If File.Exists(rPath) Then
                objStreamReader = New StreamReader(rPath)
                Dim xRatings As New XmlSerializer(_Flags_Ratings.GetType)

                _Flags_Ratings = CType(xRatings.Deserialize(objStreamReader), XMLRatings)
                objStreamReader.Close()
            Else
                Dim rPathD As String = FileUtils.Common.ReturnSettingsFile("Defaults", "DefaultRatings.xml")
                objStreamReader = New StreamReader(rPathD)
                Dim xRatings As New XmlSerializer(_Flags_Ratings.GetType)

                _Flags_Ratings = CType(xRatings.Deserialize(objStreamReader), XMLRatings)
                objStreamReader.Close()

                Try
                    File.Copy(rPathD, rPath)
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            End If

            'Studio flags
            Dim sPath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Studios", Path.DirectorySeparatorChar, "Studios.xml")
            If Directory.Exists(Directory.GetParent(sPath).FullName) Then
                Try
                    'get all images in the main folder
                    For Each lFile As String In Directory.GetFiles(Directory.GetParent(sPath).FullName, "*.png")
                        _Flags_Studios.Add(Path.GetFileNameWithoutExtension(lFile).ToLower, lFile)
                    Next

                    'now get all images in sub folders
                    For Each iDir As String In Directory.GetDirectories(Directory.GetParent(sPath).FullName)
                        For Each lFile As String In Directory.GetFiles(iDir, "*.png")
                            'hard code "\" here, then resplace when retrieving images
                            _Flags_Studios.Add(String.Concat(Directory.GetParent(iDir).Name, "\", Path.GetFileNameWithoutExtension(lFile).ToLower), lFile)
                        Next
                    Next
                Catch
                End Try
            End If
        End If
    End Sub

    Public Shared Function GetAVImages(ByVal fiAV As MediaContainers.FileInfo, ByVal PreferredLanguage As String, ByVal contentType As Enums.ContentType, ByVal videoSource As String) As Image()
        Dim iReturn(19) As Image
        Dim tVideo As MediaContainers.Video = Info.GetBestVideo(fiAV)
        Dim tAudio As MediaContainers.Audio = MetaData.GetBestAudio(fiAV, PreferredLanguage, contentType)

        If AudioVideoFlags.Count > 0 OrElse _Flags_Countries.Count > 0 Then
            Try
                'VideoResolution flags
                Dim vRes As String = Info.GetResolutionFromDimensions(tVideo).ToLower
                Dim vresFlag As Flag = AudioVideoFlags.FirstOrDefault(Function(f) f.Name = vRes AndAlso f.Type = Flag.FlagType.VideoResolution)
                If vresFlag IsNot Nothing Then
                    iReturn(0) = vresFlag.Image
                Else
                    vresFlag = AudioVideoFlags.FirstOrDefault(Function(f) f.Name = "unknown" AndAlso f.Type = Flag.FlagType.VideoResolution)
                    If vresFlag IsNot Nothing Then
                        iReturn(0) = vresFlag.Image
                    Else
                        iReturn(0) = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))
                    End If
                End If

                'VideoSource flags
                Dim vSource As String = videoSource
                Dim vSourceFlag As Flag = AudioVideoFlags.FirstOrDefault(Function(f) f.Name.ToLower = vSource.ToLower AndAlso f.Type = Flag.FlagType.VideoSource)
                If vSourceFlag IsNot Nothing Then
                    iReturn(1) = vSourceFlag.Image
                Else
                    vSourceFlag = AudioVideoFlags.FirstOrDefault(Function(f) f.Name = "unknown" AndAlso f.Type = Flag.FlagType.VideoSource)
                    If vSourceFlag IsNot Nothing Then
                        iReturn(1) = vSourceFlag.Image
                    Else
                        iReturn(1) = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))
                    End If
                End If

                'VideoCodec flags
                Dim vcodecFlag As Flag = AudioVideoFlags.FirstOrDefault(Function(f) f.Name = tVideo.Codec.ToLower AndAlso f.Type = Flag.FlagType.VideoCodec)
                If vcodecFlag IsNot Nothing Then
                    iReturn(2) = vcodecFlag.Image
                Else
                    vcodecFlag = AudioVideoFlags.FirstOrDefault(Function(f) f.Name = "unknown" AndAlso f.Type = Flag.FlagType.VideoCodec)
                    If vcodecFlag IsNot Nothing Then
                        iReturn(2) = vcodecFlag.Image
                    Else
                        iReturn(2) = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))
                    End If
                End If

                '3D flag
                If tVideo.MultiViewCountSpecified Then
                    Dim vchanFlag As Flag = AudioVideoFlags.FirstOrDefault(Function(f) f.Name = tVideo.MultiViewCount.ToString AndAlso f.Type = Flag.FlagType.VideoChan)
                    If vchanFlag IsNot Nothing Then
                        iReturn(19) = vchanFlag.Image
                    End If
                End If

                'AudioCodec flags
                Dim acodecFlag As Flag = AudioVideoFlags.FirstOrDefault(Function(f) f.Name = tAudio.Codec.ToLower AndAlso f.Type = Flag.FlagType.AudioCodec)
                If acodecFlag IsNot Nothing Then
                    If tAudio.HasPreferred Then
                        Dim acodecFlagTemp As Image = acodecFlag.Image
                        iReturn(3) = ImageUtils.SetOverlay(acodecFlagTemp, 64, 44, Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "HasLanguage.png")), 4)
                    Else
                        iReturn(3) = acodecFlag.Image
                    End If
                Else
                    acodecFlag = AudioVideoFlags.FirstOrDefault(Function(f) f.Name = "unknown" AndAlso f.Type = Flag.FlagType.AudioCodec)
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

                'AudioChannels flags
                Dim achanFlag As Flag = AudioVideoFlags.FirstOrDefault(Function(f) f.Name = tAudio.Channels.ToString AndAlso f.Type = Flag.FlagType.AudioChan)
                If achanFlag IsNot Nothing Then
                    iReturn(4) = achanFlag.Image
                Else
                    achanFlag = AudioVideoFlags.FirstOrDefault(Function(f) f.Name = "unknown" AndAlso f.Type = Flag.FlagType.AudioChan)
                    If achanFlag IsNot Nothing Then
                        iReturn(4) = achanFlag.Image
                    Else
                        iReturn(4) = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))
                    End If
                End If

                If True Then
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

    Public Shared Function GetFlagTypeFromString(ByVal sType As String) As Flag.FlagType
        Select Case sType
            Case "vchan"
                Return Flag.FlagType.VideoChan
            Case "vcodec"
                Return Flag.FlagType.VideoCodec
            Case "vres"
                Return Flag.FlagType.VideoResolution
            Case "vsource"
                Return Flag.FlagType.VideoSource
            Case "acodec"
                Return Flag.FlagType.AudioCodec
            Case "achan"
                Return Flag.FlagType.AudioChan
            Case Else
                Return Flag.FlagType.Unknown
        End Select
    End Function

    Public Shared Function GetGenreImage(ByVal strGenre As String) As Image
        Dim imgGenre As Image = Nothing
        Dim imgGenreStr As String = String.Empty
        Dim mePath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Genres")
        imgGenreStr = Path.Combine(mePath, APIXML.GenreMapping.DefaultImage)

        Dim v = From e In APIXML.GenreMapping.Genres.Where(Function(f) f.Name = strGenre)
        If v.Count > 0 Then
            imgGenreStr = Path.Combine(mePath, v(0).Image)
        End If
        Try

            If Not String.IsNullOrEmpty(imgGenreStr) AndAlso _Flags_Genres.Contains(imgGenreStr.ToLower) Then
                imgGenre = Image.FromFile(imgGenreStr)
            Else
                imgGenre = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "DefaultGenre.jpg"))
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return imgGenre
    End Function

    Public Shared Function GetLanguageImage(ByVal strLanguage As String) As Image
        Dim imgLanguage As Image = Nothing
        Dim sLang As String = String.Empty

        sLang = Localization.ISOLangGetCode2ByCode3(strLanguage)

        If Not String.IsNullOrEmpty(sLang) Then
            If _Flags_Countries.ContainsKey(sLang.ToLower) Then
                Using fsImage As New FileStream(_Flags_Countries.Item(sLang.ToLower).Replace(Convert.ToChar("\"), Path.DirectorySeparatorChar), FileMode.Open, FileAccess.Read)
                    imgLanguage = Image.FromStream(fsImage)
                End Using
            End If
        End If

        If imgLanguage Is Nothing Then imgLanguage = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "DefaultLanguage.png"))

        Return imgLanguage
    End Function

    Public Shared Function GetRatingImage(ByVal strRating As String) As Image
        Dim mePath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Ratings")
        Dim imgRating As Image = Nothing
        Dim imgRatingStr As String = String.Empty
        If Not strRating = Master.eSettings.MovieScraperMPAANotRated Then
            Dim v = From e In _FLags_Ratings.movies.Where(Function(f) f.searchstring = strRating)
            If v.Count > 0 Then
                imgRatingStr = Path.Combine(mePath, v(0).icon)
            Else
                v = From e In _FLags_Ratings.movies Where strRating.ToLower.StartsWith(e.searchstring.ToLower)
                If v.Count > 0 Then
                    imgRatingStr = Path.Combine(mePath, v(0).icon)
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
        If Master.eSettings.MovieScraperCertForMPAA AndAlso Not Master.eSettings.MovieScraperCertLang = Master.eLang.All Then
            Dim tCountry = APIXML.CertificationLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = Master.eSettings.MovieScraperCertLang)
            If tCountry IsNot Nothing AndAlso Not String.IsNullOrEmpty(tCountry.name) Then
                For Each r In _FLags_Ratings.movies.FindAll(Function(f) f.country.ToLower = tCountry.name.ToLower)
                    retRatings.Add(r.searchstring)
                Next
            End If
        Else
            For Each r In _FLags_Ratings.movies.FindAll(Function(f) f.country.ToLower = "usa")
                retRatings.Add(r.searchstring)
            Next
        End If

        Return retRatings.ToArray
    End Function

    Public Shared Function GetRatingList_TV() As Object()
        Dim retRatings As New List(Of String)
        If Master.eSettings.TVScraperShowCertForMPAA AndAlso Not Master.eSettings.TVScraperShowCertLang = Master.eLang.All Then
            Dim tCountry = APIXML.CertificationLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = Master.eSettings.TVScraperShowCertLang)
            If tCountry IsNot Nothing AndAlso Not String.IsNullOrEmpty(tCountry.name) Then
                For Each r In _FLags_Ratings.tv.FindAll(Function(f) f.country.ToLower = tCountry.name.ToLower)
                    retRatings.Add(r.searchstring)
                Next
            End If
        Else
            For Each r In _FLags_Ratings.tv.FindAll(Function(f) f.country.ToLower = "usa")
                retRatings.Add(r.searchstring)
            Next
        End If

        Return retRatings.ToArray
    End Function

    Public Shared Function GetStudioImage(ByVal strStudio As String) As Image
        Dim imgStudio As Image = Nothing

        If _Flags_Studios.ContainsKey(strStudio.ToLower) Then
            Using fsImage As New FileStream(_Flags_Studios.Item(strStudio.ToLower).Replace(Convert.ToChar("\"), Path.DirectorySeparatorChar), FileMode.Open, FileAccess.Read)
                imgStudio = Image.FromStream(fsImage)
            End Using
        End If

        If imgStudio Is Nothing Then imgStudio = Image.FromFile(FileUtils.Common.ReturnSettingsFile("Images\Defaults", "questionmark.png"))

        Return imgStudio
    End Function

    Public Shared Function GetTVRatingImage(ByVal strRating As String) As Image
        Dim mePath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Ratings")
        Dim imgRating As Image = Nothing
        Dim imgRatingStr As String = String.Empty
        If Not strRating = Master.eSettings.TVScraperShowMPAANotRated Then
            Dim v = From e In _FLags_Ratings.tv.Where(Function(f) f.searchstring = strRating)
            If v.Count > 0 Then
                imgRatingStr = Path.Combine(mePath, v(0).icon)
            Else
                v = From e In _FLags_Ratings.tv Where strRating.ToLower.StartsWith(e.searchstring.ToLower)
                If v.Count > 0 Then
                    imgRatingStr = Path.Combine(mePath, v(0).icon)
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

#End Region 'Methods

#Region "Nested Types"

    Public Class Flag

#Region "Properties"

        Public Property Name() As String = String.Empty

        Public Property Image() As Image = Nothing

        Public Property Path() As String = String.Empty

        Public Property Type() As FlagType = FlagType.VideoCodec

#End Region 'Properties

#Region "Nested Types"

        Public Enum FlagType
            AudioChan
            AudioCodec
            Unknown
            VideoChan
            VideoCodec
            VideoResolution
            VideoSource
        End Enum

#End Region 'Nested Types

    End Class

    <Serializable>
    <XmlRoot("ratings")>
    Public Class XMLRatings

#Region "Properties"

        <XmlArrayItem("movies")>
        Public Property Movies As List(Of Rating) = New List(Of Rating)

        <XmlArrayItem("tv")>
        Public Property TV As List(Of Rating) = New List(Of Rating)

#End Region 'Properties

#Region "Nested Types"

        <Serializable>
        Public Class Rating

#Region "Properties"

            <XmlElement("country")>
            Public Property Country As String = String.Empty

            <XmlElement("icon")>
            Public Property Icon As String = String.Empty

            <XmlAttribute("searchstring")>
            Public Property SearchString As String = String.Empty

#End Region 'Properties

        End Class

#End Region 'Nested Types

    End Class

#End Region 'Nested Types

End Class