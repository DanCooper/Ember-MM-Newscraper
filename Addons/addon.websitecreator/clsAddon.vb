﻿' ################################################################################
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
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization

Public Class WebsiteCreator

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Public Delegate Function ShowProgress(ByVal title As String, ByVal operation As String) As Boolean

    Private Shared _AudioVideoFlags As List(Of Flag) = New List(Of Flag)
    Private _IntCounter_Global As Integer
    Private _IntCounter_TVEpisode As Integer
    Private _IntCounter_TVSeason As Integer
    Private _IntTotal_Movies As Integer
    Private _IntTotal_TVEpisodes As Integer
    Private _IntTotal_TVShows As Integer
    Private _StrBuildPath As String = Path.Combine(Master.TempPath, "Export")
    Private _ExportSettings As New ExportSettings
    Private _LstMovieList As List(Of Database.DBElement)
    Private _LstTVShowList As List(Of Database.DBElement)

#End Region 'Fields

#Region "Methods"

    Private Function Build_ContentPartMap(ByVal strPattern As String) As List(Of ContentPart)
        'here we transform the main pattern string into a content map
        Dim tContentPartMap As New List(Of ContentPart)
        Dim part As ContentPart
        Dim iNextPart = strPattern.IndexOf("<$$")
        Dim iEndPart As Integer

        While strPattern.Length > 0
            If iNextPart > 0 Then
                'create the non-looped section
                part = New ContentPart With {.IsLooped = False}
                part.Content = strPattern.Substring(0, iNextPart)
                tContentPartMap.Add(part)
                strPattern = strPattern.Substring(iNextPart)
                iNextPart = strPattern.IndexOf("<$$")
            ElseIf iNextPart = -1 Then
                'no more sections found, add the remaining html code
                part = New ContentPart With {.IsLooped = False}
                part.Content = strPattern
                tContentPartMap.Add(part)
                strPattern = String.Empty
            Else
                If strPattern.IndexOf("<$$MOVIE>") = 0 Then
                    'create the looped section for movies
                    iEndPart = strPattern.IndexOf("</$$MOVIE>")
                    part = New ContentPart With {.ContentType = Enums.ContentType.Movie, .IsLooped = True}
                    part.Content = strPattern.Substring(9, iEndPart - 9)
                    tContentPartMap.Add(part)
                    strPattern = strPattern.Substring(iEndPart + 10)
                    iNextPart = strPattern.IndexOf("<$$")
                ElseIf strPattern.IndexOf("<$$TVSHOW>") = 0 Then
                    'create the looped section for tv shows
                    iEndPart = strPattern.IndexOf("</$$TVSHOW>")
                    part = New ContentPart With {.ContentType = Enums.ContentType.TVShow, .IsLooped = True}
                    part.Content = strPattern.Substring(10, iEndPart - 10)

                    'check if we have an InnerContentPart
                    If Not part.Content.IndexOf("<$$") = -1 Then
                        part.InnerContentPartMap = Build_ContentPartMap(part.Content)
                        part.Content = String.Empty
                    End If

                    tContentPartMap.Add(part)
                    strPattern = strPattern.Substring(iEndPart + 11)
                    iNextPart = strPattern.IndexOf("<$$")
                ElseIf strPattern.IndexOf("<$$TVSEASON>") = 0 Then
                    'create the looped section for tv seasons
                    iEndPart = strPattern.IndexOf("</$$TVSEASON>")
                    part = New ContentPart With {.ContentType = Enums.ContentType.TVSeason, .IsLooped = True}
                    part.Content = strPattern.Substring(12, iEndPart - 12)

                    'check if we have an InnerContentPart
                    If Not part.Content.IndexOf("<$$") = -1 Then
                        part.InnerContentPartMap = Build_ContentPartMap(part.Content)
                        part.Content = String.Empty
                    End If

                    tContentPartMap.Add(part)
                    strPattern = strPattern.Substring(iEndPart + 13)
                    iNextPart = strPattern.IndexOf("<$$")
                ElseIf strPattern.IndexOf("<$$TVEPISODE>") = 0 Then
                    'create the looped section for tv episodes
                    iEndPart = strPattern.IndexOf("</$$TVEPISODE>")
                    part = New ContentPart With {.ContentType = Enums.ContentType.TVEpisode, .IsLooped = True}
                    part.Content = strPattern.Substring(13, iEndPart - 13)

                    'check if we have an InnerContentPart
                    If Not part.Content.IndexOf("<$$") = -1 Then
                        part.InnerContentPartMap = Build_ContentPartMap(part.Content)
                        part.Content = String.Empty
                    End If

                    tContentPartMap.Add(part)
                    strPattern = strPattern.Substring(iEndPart + 14)
                    iNextPart = strPattern.IndexOf("<$$")
                End If
            End If
        End While

        Return tContentPartMap
    End Function

    Private Function Build_HTML(ByVal tContentPartMap As List(Of ContentPart),
                                ByVal sfunction As ShowProgress,
                                Optional ByVal tTVShow As Database.DBElement = Nothing,
                                Optional ByVal tTVSeason As Database.DBElement = Nothing) As StringBuilder

        Dim HTMLBodyPart As New StringBuilder

        For Each part In tContentPartMap
            If part.IsLooped Then
                'Movies
                If part.ContentType = Enums.ContentType.Movie Then
                    For Each tMovie As Database.DBElement In _LstMovieList
                        If Not sfunction Is Nothing Then
                            If Not sfunction(tMovie.MainDetails.Title, String.Empty) Then Return Nothing
                        End If
                        HTMLBodyPart.Append(ProcessPattern_Movie(part, sfunction, tMovie))
                        _IntCounter_Global += 1
                    Next

                    'TV Shows
                ElseIf part.ContentType = Enums.ContentType.TVShow Then
                    For Each tShow As Database.DBElement In _LstTVShowList
                        If Not sfunction Is Nothing Then
                            If Not sfunction(tShow.MainDetails.Title, String.Empty) Then Return Nothing
                        End If
                        HTMLBodyPart.Append(ProcessPattern_TVShow(part, tShow, sfunction))
                        _IntCounter_Global += 1
                    Next

                    'TV Seasons
                ElseIf part.ContentType = Enums.ContentType.TVSeason Then
                    If tTVShow IsNot Nothing Then
                        _IntCounter_TVSeason = 1
                        For Each tSeason As Database.DBElement In tTVShow.Seasons.Where(Function(f) Not f.MainDetails.Season_IsAllSeasons)
                            If Not sfunction Is Nothing Then
                                If Not sfunction(tTVShow.MainDetails.Title, tSeason.MainDetails.Title) Then Return Nothing
                            End If
                            HTMLBodyPart.Append(ProcessPattern_TVSeason(part, sfunction, tTVShow, tSeason))
                            _IntCounter_TVSeason += 1
                        Next
                    End If

                    'TV Episodes
                ElseIf part.ContentType = Enums.ContentType.TVEpisode Then
                    If tTVShow IsNot Nothing AndAlso tTVSeason IsNot Nothing Then
                        _IntCounter_TVEpisode = 1
                        For Each tEpisode As Database.DBElement In tTVShow.Episodes.Where(Function(f) f.MainDetails.Season = tTVSeason.MainDetails.Season)
                            If Not sfunction Is Nothing Then
                                If Not sfunction(tTVShow.MainDetails.Title, tEpisode.MainDetails.Title) Then Return Nothing
                            End If
                            HTMLBodyPart.Append(ProcessPattern_TVEpisode(part, sfunction, tTVShow, tEpisode))
                            _IntCounter_TVEpisode += 1
                        Next
                    End If
                End If
            Else
                HTMLBodyPart.Append(ProcessPattern_General(part))
            End If
        Next

        Return HTMLBodyPart
    End Function

    Public Shared Sub CacheFlags()
        'Audio/Video flags
        Dim fPath As String = Path.Combine(Functions.AppPath, "Addons\addon.websitecreator\Images\Flags")
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
                                _AudioVideoFlags.Add(New Flag With {.Name = cFileName.Remove(0, cFileName.IndexOf("_") + 1), .Image = Image.FromStream(fsImage), .Path = lFile, .Type = fType})
                            End Using
                        End If
                    End If
                Next
            Catch
            End Try
        End If
    End Sub

    Public Function CreateTemplate(ByVal strTemplatePath As String, ByVal MovieList As List(Of Database.DBElement), ByVal TVShowList As List(Of Database.DBElement), Optional ByVal BuildPath As String = "", Optional ByVal sfunction As ShowProgress = Nothing) As String
        CacheFlags()
        _LstMovieList = MovieList
        _LstTVShowList = TVShowList

        _IntTotal_Movies = _LstMovieList.Count
        _IntTotal_TVShows = _LstTVShowList.Count
        For Each tShow As Database.DBElement In _LstTVShowList
            _IntTotal_TVEpisodes += tShow.Episodes.Count
        Next

        If Not String.IsNullOrEmpty(BuildPath) Then
            _StrBuildPath = BuildPath
        Else
            _StrBuildPath = Path.Combine(Master.TempPath, "Export")
        End If

        FileUtils.Delete.DeleteDirectory(_StrBuildPath)
        Directory.CreateDirectory(_StrBuildPath)

        If strTemplatePath IsNot Nothing Then
            Dim htmlPath As String = Path.Combine(strTemplatePath, String.Concat(Master.eSettings.Options.Global.Language, ".html"))
            If Not File.Exists(htmlPath) Then
                htmlPath = Path.Combine(strTemplatePath, String.Concat("English_(en_US).html"))
            End If
            If Not File.Exists(htmlPath) Then
                _Logger.Warn(String.Concat("Can't find template """, htmlPath, """"))
                Return String.Empty
            End If

            Dim pattern As String = String.Empty
            pattern = File.ReadAllText(htmlPath)
            pattern = ProcessPattern_Settings(pattern)

            Dim ContentPartMap As List(Of ContentPart) = Build_ContentPartMap(pattern)

            _IntCounter_Global = 1
            _IntCounter_TVEpisode = 1
            _IntCounter_TVSeason = 1
            Dim HTMLBody As New StringBuilder
            HTMLBody = Build_HTML(ContentPartMap, sfunction)

            If HTMLBody IsNot Nothing Then
                Return SaveAll(Directory.GetParent(htmlPath).FullName, HTMLBody)
            Else
                Return String.Empty
            End If
        Else
            _Logger.Warn(String.Concat("Can't find any template"))
            Return String.Empty
        End If
    End Function

    Private Function ExportImage(ByVal tImage As MediaContainers.Image, ByVal tImageType As Enums.ModifierType) As String
        Dim strPath As String = String.Empty

        If tImage IsNot Nothing AndAlso File.Exists(tImage.LocalFilePath) Then
            Select Case tImageType
                Case Enums.ModifierType.EpisodeFanart
                    If _ExportSettings.EpisodeFanarts Then
                        If Not _ExportSettings.EpisodeFanarts_MaxHeight = -1 OrElse Not _ExportSettings.EpisodeFanarts_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _ExportSettings.EpisodeFanarts_MaxWidth, _ExportSettings.EpisodeFanarts_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}/{1}/{2}-fanart.jpg", _IntCounter_Global, _IntCounter_TVSeason, _IntCounter_TVEpisode)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_StrBuildPath, strPath))
                                tImage.ImageOriginal.Clear()  'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}/{1}/{2}-fanart.jpg", _IntCounter_Global, _IntCounter_TVSeason, _IntCounter_TVEpisode)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_StrBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.EpisodePoster
                    If _ExportSettings.EpisodePosters Then
                        If Not _ExportSettings.EpisodePosters_MaxHeight = -1 OrElse Not _ExportSettings.EpisodePosters_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _ExportSettings.EpisodePosters_MaxWidth, _ExportSettings.EpisodePosters_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}/{1}/{2}-poster.jpg", _IntCounter_Global, _IntCounter_TVSeason, _IntCounter_TVEpisode)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_StrBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}/{1}/{2}-poster.jpg", _IntCounter_Global, _IntCounter_TVSeason, _IntCounter_TVEpisode)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_StrBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.MainBanner
                    If _ExportSettings.MainBanners Then
                        If Not _ExportSettings.MainBanners_MaxHeight = -1 OrElse Not _ExportSettings.MainBanners_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.Movie, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _ExportSettings.MainBanners_MaxWidth, _ExportSettings.MainBanners_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}-banner.jpg", _IntCounter_Global)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_StrBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}-banner.jpg", _IntCounter_Global)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_StrBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.MainCharacterArt
                    If _ExportSettings.MainCharacterarts Then
                        strPath = String.Format("exportdata/{0}-characterart.png", _IntCounter_Global)
                        File.Copy(tImage.LocalFilePath, Path.Combine(_StrBuildPath, strPath), True)
                    End If
                Case Enums.ModifierType.MainClearArt
                    If _ExportSettings.MainClearArts Then
                        strPath = String.Format("exportdata/{0}-clearart.png", _IntCounter_Global)
                        File.Copy(tImage.LocalFilePath, Path.Combine(_StrBuildPath, strPath), True)
                    End If
                Case Enums.ModifierType.MainClearLogo
                    If _ExportSettings.MainClearLogos Then
                        strPath = String.Format("exportdata/{0}-clearlogo.png", _IntCounter_Global)
                        File.Copy(tImage.LocalFilePath, Path.Combine(_StrBuildPath, strPath), True)
                    End If
                Case Enums.ModifierType.MainDiscArt
                    If _ExportSettings.MainClearArts Then
                        strPath = String.Format("exportdata/{0}-discart.png", _IntCounter_Global)
                        File.Copy(tImage.LocalFilePath, Path.Combine(_StrBuildPath, strPath), True)
                    End If
                Case Enums.ModifierType.MainFanart
                    If _ExportSettings.MainFanarts Then
                        If Not _ExportSettings.MainFanarts_MaxHeight = -1 OrElse Not _ExportSettings.MainFanarts_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.Movie, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _ExportSettings.MainFanarts_MaxWidth, _ExportSettings.MainFanarts_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}-fanart.jpg", _IntCounter_Global)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_StrBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}-fanart.jpg", _IntCounter_Global)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_StrBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.MainLandscape
                    If _ExportSettings.MainLandscapes Then
                        If Not _ExportSettings.MainLandscapes_MaxHeight = -1 OrElse Not _ExportSettings.MainLandscapes_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.Movie, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _ExportSettings.MainLandscapes_MaxWidth, _ExportSettings.MainLandscapes_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}-landscape.jpg", _IntCounter_Global)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_StrBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}-landscape.jpg", _IntCounter_Global)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_StrBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.MainPoster
                    If _ExportSettings.MainPosters Then
                        If Not _ExportSettings.MainPosters_MaxHeight = -1 OrElse Not _ExportSettings.MainPosters_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _ExportSettings.MainPosters_MaxWidth, _ExportSettings.MainPosters_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}-poster.jpg", _IntCounter_Global)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_StrBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}-poster.jpg", _IntCounter_Global)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_StrBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.SeasonBanner
                    If _ExportSettings.SeasonBanners Then
                        If Not _ExportSettings.SeasonBanners_MaxHeight = -1 OrElse Not _ExportSettings.SeasonBanners_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _ExportSettings.SeasonBanners_MaxWidth, _ExportSettings.SeasonBanners_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}/{1}-banner.jpg", _IntCounter_Global, _IntCounter_TVSeason)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_StrBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}/{1}-banner.jpg", _IntCounter_Global, _IntCounter_TVSeason)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_StrBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.SeasonFanart
                    If _ExportSettings.SeasonFanarts Then
                        If Not _ExportSettings.SeasonFanarts_MaxHeight = -1 OrElse Not _ExportSettings.SeasonFanarts_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _ExportSettings.SeasonFanarts_MaxWidth, _ExportSettings.SeasonFanarts_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}/{1}-fanart.jpg", _IntCounter_Global, _IntCounter_TVSeason)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_StrBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}/{1}-fanart.jpg", _IntCounter_Global, _IntCounter_TVSeason)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_StrBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.SeasonLandscape
                    If _ExportSettings.SeasonLandscapes Then
                        If Not _ExportSettings.SeasonLandscapes_MaxHeight = -1 OrElse Not _ExportSettings.SeasonLandscapes_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _ExportSettings.SeasonLandscapes_MaxWidth, _ExportSettings.SeasonLandscapes_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}/{1}-landscape.jpg", _IntCounter_Global, _IntCounter_TVSeason)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_StrBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}/{1}-landscape.jpg", _IntCounter_Global, _IntCounter_TVSeason)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_StrBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.SeasonPoster
                    If _ExportSettings.SeasonPosters Then
                        If Not _ExportSettings.SeasonPosters_MaxHeight = -1 OrElse Not _ExportSettings.SeasonPosters_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _ExportSettings.SeasonPosters_MaxWidth, _ExportSettings.SeasonPosters_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}/{1}-poster.jpg", _IntCounter_Global, _IntCounter_TVSeason)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_StrBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}/{1}-poster.jpg", _IntCounter_Global, _IntCounter_TVSeason)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_StrBuildPath, strPath), True)
                        End If
                    End If
            End Select
        End If

        Return strPath
    End Function

    Private Function GetAVSInfo(ByVal fInfo As MediaContainers.Fileinfo, ByVal contentType As Enums.ContentType) As AVSInfo
        Dim nInfo As New AVSInfo

        Dim tVid As New MediaContainers.Video
        Dim tAud As New MediaContainers.Audio
        Dim tRes As String = String.Empty


        If fInfo IsNot Nothing Then
            If fInfo.StreamDetails.Video.Count > 0 Then
                tVid = Information.GetBestVideo(fInfo)
                tRes = Information.GetResolutionFromDimensions(tVid)

                nInfo.vidBitrate = tVid.Bitrate.ToString
                nInfo.vidMultiViewCount = tVid.MultiViewCount.ToString
                nInfo.vidMultiViewLayout = tVid.MultiViewLayout
                nInfo.vidAspect = tVid.Aspect.ToString
                nInfo.vidDuration = tVid.Duration.ToString
                nInfo.vidHeight = tVid.Height.ToString
                nInfo.vidLanguage = tVid.Language
                nInfo.vidLongLanguage = tVid.LongLanguage
                nInfo.vidScantype = tVid.Scantype
                nInfo.vidStereoMode = tVid.StereoMode
                nInfo.vidWidth = tVid.Width.ToString
                nInfo.vidDetails = String.Format("{0} / {1}", If(String.IsNullOrEmpty(tRes), Master.eLang.GetString(138, "Unknown"), tRes), If(String.IsNullOrEmpty(tVid.Codec), Master.eLang.GetString(138, "Unknown"), tVid.Codec)).ToUpper
                nInfo.vidDimensions = Information.GetDimensionsFromVideo(tVid)
            End If

            If fInfo.StreamDetails.Audio.Count > 0 Then
                tAud = MetaData.GetBestAudio(fInfo, String.Empty, contentType)

                nInfo.audBitrate = tAud.Bitrate.ToString
                nInfo.audChannels = tAud.Channels.ToString
                nInfo.audLanguage = tAud.Language
                nInfo.audLongLanguage = tAud.LongLanguage
                nInfo.audDetails = String.Format("{0} / {1}", If(Not tAud.ChannelsSpecified, Master.eLang.GetString(138, "Unknown"), String.Concat(tAud.Channels.ToString, "ch")), If(String.IsNullOrEmpty(tAud.Codec), Master.eLang.GetString(138, "Unknown"), tAud.Codec)).ToUpper
            End If

            If fInfo.StreamDetails.Subtitle.Count > 0 Then
                Dim subtitleinfo As MediaContainers.Subtitle
                For c = 0 To fInfo.StreamDetails.Subtitle.Count - 1
                    subtitleinfo = fInfo.StreamDetails.Subtitle(c)
                    If Not subtitleinfo Is Nothing Then
                        If Not String.IsNullOrEmpty(subtitleinfo.Language) Then
                            nInfo.subLanguage = nInfo.subLanguage & ";" & subtitleinfo.Language
                        End If
                        If Not String.IsNullOrEmpty(subtitleinfo.LongLanguage) Then
                            nInfo.subLongLanguage = nInfo.subLongLanguage & ";" & subtitleinfo.LongLanguage
                        End If
                        If Not String.IsNullOrEmpty(subtitleinfo.SubsType) Then
                            nInfo.subType = nInfo.subType & ";" & subtitleinfo.SubsType
                        End If
                    End If
                Next
            End If
        End If

        Return nInfo
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

    Private Function GetDetailAudioInfo(ByVal fInfo As MediaContainers.Fileinfo) As AVSInfo
        Dim nInfo As New AVSInfo
        Dim tAud As New MediaContainers.Audio
        If fInfo IsNot Nothing Then
            If fInfo.StreamDetails.Audio.Count > 0 Then
                Dim audioinfo As MediaContainers.Audio
                For c = 0 To fInfo.StreamDetails.Audio.Count - 1
                    audioinfo = fInfo.StreamDetails.Audio(c)
                    If audioinfo IsNot Nothing Then
                        nInfo.audBitrate = If(audioinfo.BitrateSpecified, nInfo.audBitrate & ";" & audioinfo.Bitrate, Master.eLang.GetString(138, "Unknown"))
                        nInfo.audChannels = If(audioinfo.ChannelsSpecified, nInfo.audChannels & ";" & audioinfo.Channels, Master.eLang.GetString(138, "Unknown"))
                        nInfo.audLanguage = If(audioinfo.LanguageSpecified, nInfo.audLanguage & ";" & audioinfo.Language, Master.eLang.GetString(138, "Unknown"))
                        nInfo.audLongLanguage = If(audioinfo.LongLanguageSpecified, nInfo.audLongLanguage & ";" & audioinfo.LongLanguage, Master.eLang.GetString(138, "Unknown"))
                        nInfo.audDetails = If(audioinfo.CodecSpecified, nInfo.audDetails & ";" & String.Format("{0}ch / {1}", If(audioinfo.ChannelsSpecified, Master.eLang.GetString(138, "Unknown"), audioinfo.Channels.ToString), If(String.IsNullOrEmpty(audioinfo.Codec), Master.eLang.GetString(138, "Unknown"), audioinfo.Codec)).ToUpper, nInfo.audDetails)
                    End If
                Next
            End If
        End If
        Return nInfo
    End Function

    Private Shared Sub CopyDirectory(ByVal SourcePath As String, ByVal DestPath As String, Optional ByVal Overwrite As Boolean = False)
        Dim SourceDir As DirectoryInfo = New DirectoryInfo(SourcePath)
        Dim DestDir As DirectoryInfo = New DirectoryInfo(DestPath)
        Dim IsRoot As Boolean = False

        ' the source directory must exist, otherwise throw an exception
        If SourceDir.Exists Then

            'is this a root directory?
            If DestDir.Root.FullName = DestDir.FullName Then
                IsRoot = True
            End If

            ' if destination SubDir's parent SubDir does not exist throw an exception (also check it isn't the root)
            If Not IsRoot AndAlso Not DestDir.Parent.Exists Then
                Throw New DirectoryNotFoundException _
                    ("Destination directory does not exist: " + DestDir.Parent.FullName)
            End If

            If Not DestDir.Exists Then
                DestDir.Create()
            End If

            ' copy all the files of the current directory
            Dim ChildFile As FileInfo
            For Each ChildFile In SourceDir.GetFiles()
                If (ChildFile.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden OrElse Path.GetExtension(ChildFile.FullName) = ".html" Then Continue For
                If Overwrite Then
                    ChildFile.CopyTo(Path.Combine(DestDir.FullName, ChildFile.Name), True)
                Else
                    ' if Overwrite = false, copy the file only if it does not exist
                    ' this is done to avoid an IOException if a file already exists
                    ' this way the other files can be copied anyway...
                    If Not File.Exists(Path.Combine(DestDir.FullName, ChildFile.Name)) Then
                        ChildFile.CopyTo(Path.Combine(DestDir.FullName, ChildFile.Name), False)
                    End If
                End If
            Next

            ' copy all the sub-directories by recursively calling this same routine
            Dim SubDir As DirectoryInfo
            For Each SubDir In SourceDir.GetDirectories()
                If (SubDir.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then Continue For
                CopyDirectory(SubDir.FullName, Path.Combine(DestDir.FullName,
                    SubDir.Name), Overwrite)
            Next
        Else
            Throw New DirectoryNotFoundException("Source directory does not exist: " + SourceDir.FullName)
        End If
    End Sub

    Private Function ProcessPattern_Flags(ByVal tDBElement As Database.DBElement, ByVal strRow As String, ByVal tContentType As Enums.ContentType) As String
        If _AudioVideoFlags.Count > 0 Then
            Dim fiAV = tDBElement.MainDetails.FileInfo
            Dim tVideo As MediaContainers.Video = Information.GetBestVideo(fiAV)
            Dim tAudio As MediaContainers.Audio = MetaData.GetBestAudio(fiAV, String.Empty, tContentType)

            'VideoResolution flags
            Dim vresFlag As Flag = _AudioVideoFlags.FirstOrDefault(Function(f) f.Name = Information.GetResolutionFromDimensions(tVideo).ToLower AndAlso f.Type = Flag.FlagType.VideoResolution)
            If vresFlag IsNot Nothing Then
                strRow = strRow.Replace("<$FLAG_VRES>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vresFlag.Path))).Replace("\", "/")
            Else
                vresFlag = _AudioVideoFlags.FirstOrDefault(Function(f) f.Name = "defaultscreen" AndAlso f.Type = Flag.FlagType.VideoResolution)
                If vresFlag IsNot Nothing Then
                    strRow = strRow.Replace("<$FLAG_VRES>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vresFlag.Path))).Replace("\", "/")
                End If
            End If

            'VideoSource flags
            Dim vsourceFlag As Flag = _AudioVideoFlags.FirstOrDefault(Function(f) f.Name.ToLower = tDBElement.VideoSource AndAlso f.Type = Flag.FlagType.VideoSource)
            If vsourceFlag IsNot Nothing Then
                strRow = strRow.Replace("<$FLAG_VSOURCE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vsourceFlag.Path))).Replace("\", "/")
            Else
                vsourceFlag = _AudioVideoFlags.FirstOrDefault(Function(f) f.Name = "defaultscreen" AndAlso f.Type = Flag.FlagType.VideoSource)
                If vsourceFlag IsNot Nothing Then
                    strRow = strRow.Replace("<$FLAG_VSOURCE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vsourceFlag.Path))).Replace("\", "/")
                End If
            End If

            'VideoCodec flags
            Dim vcodecFlag As Flag = _AudioVideoFlags.FirstOrDefault(Function(f) f.Name = tVideo.Codec.ToLower AndAlso f.Type = Flag.FlagType.VideoCodec)
            If vcodecFlag IsNot Nothing Then
                strRow = strRow.Replace("<$FLAG_VTYPE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vcodecFlag.Path))).Replace("\", "/")
            Else
                vcodecFlag = _AudioVideoFlags.FirstOrDefault(Function(f) f.Name = "defaultscreen" AndAlso f.Type = Flag.FlagType.VideoCodec)
                If vcodecFlag IsNot Nothing Then
                    strRow = strRow.Replace("<$FLAG_VTYPE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vcodecFlag.Path))).Replace("\", "/")
                End If
            End If

            'AudioCodec flags
            Dim acodecFlag As Flag = _AudioVideoFlags.FirstOrDefault(Function(f) f.Name = tAudio.Codec.ToLower AndAlso f.Type = Flag.FlagType.AudioCodec)
            If acodecFlag IsNot Nothing Then
                strRow = strRow.Replace("<$FLAG_ATYPE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(acodecFlag.Path))).Replace("\", "/")
            Else
                acodecFlag = _AudioVideoFlags.FirstOrDefault(Function(f) f.Name = "defaultaudio" AndAlso f.Type = Flag.FlagType.AudioCodec)
                If acodecFlag IsNot Nothing Then
                    strRow = strRow.Replace("<$FLAG_ATYPE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(acodecFlag.Path))).Replace("\", "/")
                End If
            End If

            'AudioChannels flags
            Dim achanFlag As Flag = _AudioVideoFlags.FirstOrDefault(Function(f) f.Name = tAudio.Channels.ToString AndAlso f.Type = Flag.FlagType.AudioChan)
            If achanFlag IsNot Nothing Then
                strRow = strRow.Replace("<$FLAG_ACHAN>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(achanFlag.Path))).Replace("\", "/")
            Else
                achanFlag = _AudioVideoFlags.FirstOrDefault(Function(f) f.Name = "defaultaudio" AndAlso f.Type = Flag.FlagType.AudioChan)
                If achanFlag IsNot Nothing Then
                    strRow = strRow.Replace("<$FLAG_ACHAN>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(achanFlag.Path))).Replace("\", "/")
                End If
            End If
        End If
        Return strRow
    End Function

    Private Function ProcessPattern_General(ByVal tContentPart As ContentPart) As String
        Dim strRow As String = tContentPart.Content

        'Special Strings
        strRow = strRow.Replace("<$TOTAL_MOVIES>", _IntTotal_Movies.ToString)
        strRow = strRow.Replace("<$TOTAL_TVEPISODES>", _IntTotal_TVEpisodes.ToString)
        strRow = strRow.Replace("<$TOTAL_TVSHOWS>", _IntTotal_TVShows.ToString)

        Return strRow
    End Function

    Private Function ProcessPattern_Movie(ByVal tContentPart As ContentPart, ByVal sfunction As ShowProgress, ByVal tMovie As Database.DBElement) As String
        Dim strRow As String = tContentPart.Content

        'Special Strings
        strRow = strRow.Replace("<$COUNT>", _IntCounter_Global.ToString)
        strRow = strRow.Replace("<$DIRNAME>", StringUtils.HtmlEncode(Path.GetDirectoryName(tMovie.FileItem.MainPath.FullName)))
        strRow = strRow.Replace("<$FILENAME>", StringUtils.HtmlEncode(Path.GetFileName(tMovie.FileItem.FirstPathFromStack)))
        strRow = strRow.Replace("<$FILESIZE>", StringUtils.HtmlEncode(tMovie.FileItem.TotalSizeAsReadableString))
        strRow = strRow.Replace("<$NOW>", Date.Now.ToLongDateString) 'Save Build Date. might be useful info!
        strRow = strRow.Replace("<$PATH>", StringUtils.HtmlEncode(tMovie.FileItem.MainPath.FullName))

        'Images
        With tMovie.ImagesContainer
            strRow = strRow.Replace("<$BANNER>", ExportImage(.Banner, Enums.ModifierType.MainBanner))
            strRow = strRow.Replace("<$CLEARART>", ExportImage(.ClearArt, Enums.ModifierType.MainClearArt))
            strRow = strRow.Replace("<$CLEARLOGO>", ExportImage(.ClearLogo, Enums.ModifierType.MainClearLogo))
            strRow = strRow.Replace("<$DISCART>", ExportImage(.DiscArt, Enums.ModifierType.MainDiscArt))
            strRow = strRow.Replace("<$FANART>", ExportImage(.Fanart, Enums.ModifierType.MainFanart))
            strRow = strRow.Replace("<$LANDSCAPE>", ExportImage(.Landscape, Enums.ModifierType.MainLandscape))
            strRow = strRow.Replace("<$POSTER>", ExportImage(.Poster, Enums.ModifierType.MainPoster))
        End With

        'Title
        Dim Title As String = tMovie.MainDetails.Title

        'Actors
        Dim ActorsList As New List(Of String)
        For Each tActor As MediaContainers.Person In tMovie.MainDetails.Actors
            ActorsList.Add(tActor.Name)
        Next

        'NFO Fields
        strRow = strRow.Replace("<$ACTORS>", StringUtils.HtmlEncode(String.Join(", ", ActorsList.ToArray)))
        strRow = strRow.Replace("<$CERTIFICATIONS>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.MainDetails.Certifications.ToArray)))
        strRow = strRow.Replace("<$COUNTRIES>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.MainDetails.Countries.ToArray)))
        strRow = strRow.Replace("<$CREDITS>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.MainDetails.Credits.ToArray)))
        strRow = strRow.Replace("<$DATEADDED>", StringUtils.HtmlEncode(Functions.ConvertFromUnixTimestamp(tMovie.DateAdded).ToString("dd.MM.yyyy")))
        strRow = strRow.Replace("<$DATEMODIFIED>", StringUtils.HtmlEncode(Functions.ConvertFromUnixTimestamp(tMovie.DateModified).ToString("dd.MM.yyyy")))
        strRow = strRow.Replace("<$DIRECTORS>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.MainDetails.Directors.ToArray)))
        strRow = strRow.Replace("<$GENRES>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.MainDetails.Genres.ToArray)))
        strRow = strRow.Replace("<$IMDBID>", StringUtils.HtmlEncode(tMovie.MainDetails.UniqueIDs.IMDbId))
        strRow = strRow.Replace("<$LANGUAGE>", StringUtils.HtmlEncode(tMovie.MainDetails.Language))
        strRow = strRow.Replace("<$LASTPLAYED>", StringUtils.HtmlEncode(tMovie.MainDetails.LastPlayed))
        strRow = strRow.Replace("<$LISTTITLE>", StringUtils.HtmlEncode(StringUtils.SortTokens(tMovie.MainDetails.Title)))
        strRow = strRow.Replace("<$MPAA>", StringUtils.HtmlEncode(tMovie.MainDetails.MPAA))
        strRow = strRow.Replace("<$ORIGINALTITLE>", StringUtils.HtmlEncode(tMovie.MainDetails.OriginalTitle))
        strRow = strRow.Replace("<$OUTLINE>", StringUtils.HtmlEncode(tMovie.MainDetails.Outline))
        strRow = strRow.Replace("<$PLAYCOUNT>", CStr(tMovie.MainDetails.PlayCount))
        strRow = strRow.Replace("<$PLOT>", StringUtils.HtmlEncode(tMovie.MainDetails.Plot))
        strRow = strRow.Replace("<$RATING>", StringUtils.HtmlEncode(If(tMovie.MainDetails.RatingSpecified, Double.Parse(tMovie.MainDetails.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), String.Empty)))
        strRow = strRow.Replace("<$PREMIERED>", StringUtils.HtmlEncode(tMovie.MainDetails.Premiered))
        strRow = strRow.Replace("<$RUNTIME>", StringUtils.HtmlEncode(tMovie.MainDetails.Runtime))
        strRow = strRow.Replace("<$STUDIOS>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.MainDetails.Studios.ToArray)))
        strRow = strRow.Replace("<$TAGLINE>", StringUtils.HtmlEncode(tMovie.MainDetails.Tagline))
        strRow = strRow.Replace("<$TAGS>", If(tMovie.MainDetails.TagsSpecified, StringUtils.HtmlEncode((String.Join(" / ", tMovie.MainDetails.Tags.ToArray))), String.Empty))
        strRow = strRow.Replace("<$TITLE>", StringUtils.HtmlEncode(Title))
        strRow = strRow.Replace("<$TMDBCOLID>", StringUtils.HtmlEncode(CStr(tMovie.MainDetails.UniqueIDs.TMDbCollectionId)))
        strRow = strRow.Replace("<$TMDBID>", StringUtils.HtmlEncode(CStr(tMovie.MainDetails.UniqueIDs.TMDbId)))
        strRow = strRow.Replace("<$TOP250>", StringUtils.HtmlEncode(tMovie.MainDetails.Top250.ToString))
        strRow = strRow.Replace("<$TRAILER>", StringUtils.HtmlEncode(tMovie.MainDetails.Trailer))
        strRow = strRow.Replace("<$VIDEOSOURCE>", StringUtils.HtmlEncode(tMovie.MainDetails.VideoSource))
        strRow = strRow.Replace("<$VOTES>", StringUtils.HtmlEncode(If(tMovie.MainDetails.VotesSpecified, Double.Parse(tMovie.MainDetails.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture), String.Empty)))
        strRow = strRow.Replace("<$YEAR>", tMovie.MainDetails.Year.ToString)

        'FileInfo
        Dim fInfo As AVSInfo = GetAVSInfo(tMovie.MainDetails.FileInfo, tMovie.ContentType)

        'Audio
        strRow = strRow.Replace("<$AUDIO>", fInfo.audDetails)
        strRow = strRow.Replace("<$AUDIOBITRATE>", fInfo.audBitrate)
        strRow = strRow.Replace("<$AUDIOCHANNELS>", fInfo.audChannels)
        strRow = strRow.Replace("<$AUDIOLANGUAGE>", fInfo.audLanguage)
        strRow = strRow.Replace("<$AUDIOLONGLANGUAGE>", fInfo.audLongLanguage)

        Dim aInfo As AVSInfo = GetDetailAudioInfo(tMovie.MainDetails.FileInfo)
        strRow = strRow.Replace("<$DETAILALLAUDIO>", aInfo.audDetails)
        strRow = strRow.Replace("<$AUDIOALLBITRATE>", aInfo.audBitrate)
        strRow = strRow.Replace("<$AUDIOALLCHANNELS>", aInfo.audChannels)
        strRow = strRow.Replace("<$AUDIOALLLANGUAGE>", aInfo.audLanguage)
        strRow = strRow.Replace("<$AUDIOALLLONGLANGUAGE>", aInfo.audLongLanguage)

        'Subtitle
        strRow = strRow.Replace("<$SUBTITLELANGUAGE>", fInfo.subLanguage)
        strRow = strRow.Replace("<$SUBTITLELONGLANGUAGE>", fInfo.subLongLanguage)
        strRow = strRow.Replace("<$SUBTITLETYPE>", fInfo.subType)

        'Video
        strRow = strRow.Replace("<$VIDEO>", fInfo.vidDetails)
        strRow = strRow.Replace("<$VIDEOASPECT>", fInfo.vidAspect)
        strRow = strRow.Replace("<$VIDEOBITRATE>", fInfo.vidBitrate)
        strRow = strRow.Replace("<$VIDEODIMENSIONS>", fInfo.vidDimensions)
        strRow = strRow.Replace("<$VIDEODURATION>", fInfo.vidDuration)
        strRow = strRow.Replace("<$VIDEOHEIGHT>", fInfo.vidHeight)
        strRow = strRow.Replace("<$VIDEOLANGUAGE>", fInfo.vidLanguage)
        strRow = strRow.Replace("<$VIDEOLONGLANGUAGE>", fInfo.vidLongLanguage)
        strRow = strRow.Replace("<$VIDEOMULTIVIEW>", fInfo.vidMultiViewCount)
        strRow = strRow.Replace("<$VIDEOSCANTYPE>", fInfo.vidScantype)
        strRow = strRow.Replace("<$VIDEOSTEREOMODE>", fInfo.vidStereoMode)
        strRow = strRow.Replace("<$VIDEOWIDTH>", fInfo.vidWidth)

        'Flags
        strRow = ProcessPattern_Flags(tMovie, strRow, Enums.ContentType.Movie)

        Return strRow
    End Function

    Private Function ProcessPattern_Settings(ByVal strPattern As String) As String
        _ExportSettings = New ExportSettings

        Dim regSettings As Match = Regex.Match(strPattern, "<exportsettings>.*?</exportsettings>", RegexOptions.Singleline)
        If regSettings.Success Then
            Dim xmlSer As XmlSerializer = Nothing
            Using xmlSR As TextReader = New StringReader(regSettings.Value)
                xmlSer = New XmlSerializer(GetType(ExportSettings))
                _ExportSettings = DirectCast(xmlSer.Deserialize(xmlSR), ExportSettings)
            End Using
            strPattern = strPattern.Replace(regSettings.Value, String.Empty)
        End If

        Return strPattern
    End Function

    Private Function ProcessPattern_TVEpisode(ByVal tContentPart As ContentPart, ByVal sfunction As ShowProgress, ByVal tShow As Database.DBElement, ByVal tEpisode As Database.DBElement) As String
        Dim strRow As String = tContentPart.Content

        'Special Strings
        strRow = strRow.Replace("<$COUNT>", _IntCounter_Global.ToString)
        strRow = strRow.Replace("<$COUNT_TVSEASON>", _IntCounter_TVSeason.ToString)
        strRow = strRow.Replace("<$COUNT_TVEPISODE>", _IntCounter_TVEpisode.ToString)
        strRow = strRow.Replace("<$FILENAME>", StringUtils.HtmlEncode(Path.GetFileName(tEpisode.FileItem.FirstPathFromStack)))
        strRow = strRow.Replace("<$FILESIZE>", StringUtils.HtmlEncode(tEpisode.FileItem.TotalSizeAsReadableString))
        strRow = strRow.Replace("<$MISSING>", If(tEpisode.FileItem.IDSpecified, "false", "true"))
        strRow = strRow.Replace("<$PATH>", StringUtils.HtmlEncode(tEpisode.FileItem.MainPath.FullName))

        'Images
        With tEpisode.ImagesContainer
            strRow = strRow.Replace("<$FANART>", ExportImage(.Fanart, Enums.ModifierType.EpisodeFanart))
            strRow = strRow.Replace("<$POSTER>", ExportImage(.Poster, Enums.ModifierType.EpisodePoster))
        End With

        'Actors
        Dim ActorsList_Episode As New List(Of String)
        For Each tActor As MediaContainers.Person In tEpisode.MainDetails.Actors
            ActorsList_Episode.Add(tActor.Name)
        Next

        'Guest Stars
        Dim GuestStarsList_Episode As New List(Of String)
        For Each tActor As MediaContainers.Person In tEpisode.MainDetails.GuestStars
            GuestStarsList_Episode.Add(tActor.Name)
        Next

        'NFO Fields
        strRow = strRow.Replace("<$ACTORS>", StringUtils.HtmlEncode(String.Join(", ", ActorsList_Episode.ToArray)))
        strRow = strRow.Replace("<$AIRED>", StringUtils.HtmlEncode(tEpisode.MainDetails.Aired))
        strRow = strRow.Replace("<$CREDITS>", StringUtils.HtmlEncode(String.Join(" / ", tEpisode.MainDetails.Credits.ToArray)))
        strRow = strRow.Replace("<$DATEADDED>", StringUtils.HtmlEncode(Functions.ConvertFromUnixTimestamp(tEpisode.DateAdded).ToString("dd.MM.yyyy")))
        strRow = strRow.Replace("<$DIRECTORS>", StringUtils.HtmlEncode(String.Join(" / ", tEpisode.MainDetails.Directors.ToArray)))
        strRow = strRow.Replace("<$EPISODE>", StringUtils.HtmlEncode(CStr(tEpisode.MainDetails.Episode)))
        strRow = strRow.Replace("<$GUESTSTARS>", StringUtils.HtmlEncode(String.Join(" / ", GuestStarsList_Episode.ToArray)))
        strRow = strRow.Replace("<$IMDBID>", StringUtils.HtmlEncode(tEpisode.MainDetails.UniqueIDs.IMDbId))
        strRow = strRow.Replace("<$LASTPLAYED>", StringUtils.HtmlEncode(tEpisode.MainDetails.LastPlayed))
        strRow = strRow.Replace("<$PLOT>", StringUtils.HtmlEncode(tEpisode.MainDetails.Plot))
        strRow = strRow.Replace("<$RATING>", StringUtils.HtmlEncode(If(tEpisode.MainDetails.RatingSpecified, Double.Parse(tEpisode.MainDetails.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), String.Empty)))
        strRow = strRow.Replace("<$RUNTIME>", StringUtils.HtmlEncode(tEpisode.MainDetails.Runtime))
        strRow = strRow.Replace("<$SEASON>", StringUtils.HtmlEncode(CStr(tEpisode.MainDetails.Season)))
        strRow = strRow.Replace("<$TITLE>", StringUtils.HtmlEncode(tEpisode.MainDetails.Title))
        strRow = strRow.Replace("<$TMDBID>", StringUtils.HtmlEncode(CStr(tEpisode.MainDetails.UniqueIDs.TMDbId)))
        strRow = strRow.Replace("<$TVDBID>", StringUtils.HtmlEncode(CStr(tEpisode.MainDetails.UniqueIDs.TVDbId)))
        strRow = strRow.Replace("<$VIDEOSOURCE>", StringUtils.HtmlEncode(tEpisode.MainDetails.VideoSource))
        strRow = strRow.Replace("<$VOTES>", StringUtils.HtmlEncode(If(tEpisode.MainDetails.VotesSpecified, Double.Parse(tEpisode.MainDetails.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture), String.Empty)))

        'FileInfo
        Dim fInfo As AVSInfo = GetAVSInfo(tEpisode.MainDetails.FileInfo, tEpisode.ContentType)

        'Audio
        strRow = strRow.Replace("<$AUDIO>", fInfo.audDetails)
        strRow = strRow.Replace("<$AUDIOBITRATE>", fInfo.audBitrate)
        strRow = strRow.Replace("<$AUDIOCHANNELS>", fInfo.audChannels)
        strRow = strRow.Replace("<$AUDIOLANGUAGE>", fInfo.audLanguage)
        strRow = strRow.Replace("<$AUDIOLONGLANGUAGE>", fInfo.audLongLanguage)

        Dim aInfo As AVSInfo = GetDetailAudioInfo(tEpisode.MainDetails.FileInfo)
        strRow = strRow.Replace("<$DETAILALLAUDIO>", aInfo.audDetails)
        strRow = strRow.Replace("<$AUDIOALLBITRATE>", aInfo.audBitrate)
        strRow = strRow.Replace("<$AUDIOALLCHANNELS>", aInfo.audChannels)
        strRow = strRow.Replace("<$AUDIOALLLANGUAGE>", aInfo.audLanguage)
        strRow = strRow.Replace("<$AUDIOALLLONGLANGUAGE>", aInfo.audLongLanguage)

        'Subtitle
        strRow = strRow.Replace("<$SUBTITLELANGUAGE>", fInfo.subLanguage)
        strRow = strRow.Replace("<$SUBTITLELONGLANGUAGE>", fInfo.subLongLanguage)
        strRow = strRow.Replace("<$SUBTITLETYPE>", fInfo.subType)

        'Video
        strRow = strRow.Replace("<$VIDEO>", fInfo.vidDetails)
        strRow = strRow.Replace("<$VIDEOASPECT>", fInfo.vidAspect)
        strRow = strRow.Replace("<$VIDEOBITRATE>", fInfo.vidBitrate)
        strRow = strRow.Replace("<$VIDEODIMENSIONS>", fInfo.vidDimensions)
        strRow = strRow.Replace("<$VIDEODURATION>", fInfo.vidDuration)
        strRow = strRow.Replace("<$VIDEOHEIGHT>", fInfo.vidHeight)
        strRow = strRow.Replace("<$VIDEOLANGUAGE>", fInfo.vidLanguage)
        strRow = strRow.Replace("<$VIDEOLONGLANGUAGE>", fInfo.vidLongLanguage)
        strRow = strRow.Replace("<$VIDEOMULTIVIEW>", fInfo.vidMultiViewCount)
        strRow = strRow.Replace("<$VIDEOSCANTYPE>", fInfo.vidScantype)
        strRow = strRow.Replace("<$VIDEOSTEREOMODE>", fInfo.vidStereoMode)
        strRow = strRow.Replace("<$VIDEOWIDTH>", fInfo.vidWidth)

        'Flags
        strRow = ProcessPattern_Flags(tEpisode, strRow, Enums.ContentType.TVEpisode)

        Return strRow
    End Function

    Private Function ProcessPattern_TVSeason(ByVal tContentPart As ContentPart, ByVal sfunction As ShowProgress, ByVal tShow As Database.DBElement, ByVal tSeason As Database.DBElement) As String
        Dim strRow As String = tContentPart.Content

        If tContentPart.InnerContentPartMap IsNot Nothing Then
            strRow = Build_HTML(DirectCast(tContentPart.InnerContentPartMap, List(Of ContentPart)), sfunction, tShow, tSeason).ToString
        End If

        'Special Strings
        strRow = strRow.Replace("<$COUNT>", _IntCounter_Global.ToString)
        strRow = strRow.Replace("<$COUNT_TVSEASON>", _IntCounter_TVSeason.ToString)
        strRow = strRow.Replace("<$EPISODES>", StringUtils.HtmlEncode(CStr(tShow.Episodes.Where(Function(f) f.MainDetails.Season = tSeason.MainDetails.Season).Count)))

        'Images
        With tSeason.ImagesContainer
            strRow = strRow.Replace("<$BANNER>", ExportImage(.Banner, Enums.ModifierType.SeasonBanner))
            strRow = strRow.Replace("<$FANART>", ExportImage(.Fanart, Enums.ModifierType.SeasonFanart))
            strRow = strRow.Replace("<$LANDSCAPE>", ExportImage(.Landscape, Enums.ModifierType.SeasonLandscape))
            strRow = strRow.Replace("<$POSTER>", ExportImage(.Poster, Enums.ModifierType.SeasonPoster))
        End With

        'NFO Fields
        strRow = strRow.Replace("<$AIRED>", StringUtils.HtmlEncode(tSeason.MainDetails.Aired))
        strRow = strRow.Replace("<$PLOT>", StringUtils.HtmlEncode(tSeason.MainDetails.Plot))
        strRow = strRow.Replace("<$SEASON>", StringUtils.HtmlEncode(CStr(tSeason.MainDetails.Season)))
        strRow = strRow.Replace("<$TITLE>", StringUtils.HtmlEncode(tSeason.MainDetails.Title))
        strRow = strRow.Replace("<$TMDBID>", StringUtils.HtmlEncode(CStr(tSeason.MainDetails.UniqueIDs.TMDbId)))
        strRow = strRow.Replace("<$TVDBID>", StringUtils.HtmlEncode(CStr(tSeason.MainDetails.UniqueIDs.TVDbId)))

        Return strRow
    End Function

    Private Function ProcessPattern_TVShow(ByVal tContentPart As ContentPart, ByVal tShow As Database.DBElement, ByVal sfunction As ShowProgress) As String
        Dim strRow As String = tContentPart.Content

        If tContentPart.InnerContentPartMap IsNot Nothing Then
            strRow = Build_HTML(DirectCast(tContentPart.InnerContentPartMap, List(Of ContentPart)), sfunction, tShow).ToString
        End If

        'Special Strings
        strRow = strRow.Replace("<$COUNT>", _IntCounter_Global.ToString)
        strRow = strRow.Replace("<$EPISODES>", StringUtils.HtmlEncode(CStr(tShow.Episodes.Count)))
        strRow = strRow.Replace("<$NOW>", Date.Now.ToLongDateString) 'Save Build Date. might be useful info!
        strRow = strRow.Replace("<$PATH>", StringUtils.HtmlEncode(tShow.ShowPath))

        'Images
        With tShow.ImagesContainer
            strRow = strRow.Replace("<$BANNER>", ExportImage(.Banner, Enums.ModifierType.MainBanner))
            strRow = strRow.Replace("<$CHARACTERART>", ExportImage(.CharacterArt, Enums.ModifierType.MainCharacterArt))
            strRow = strRow.Replace("<$CLEARART>", ExportImage(.ClearArt, Enums.ModifierType.MainClearArt))
            strRow = strRow.Replace("<$CLEARLOGO>", ExportImage(.ClearLogo, Enums.ModifierType.MainClearLogo))
            strRow = strRow.Replace("<$FANART>", ExportImage(.Fanart, Enums.ModifierType.MainFanart))
            strRow = strRow.Replace("<$LANDSCAPE>", ExportImage(.Landscape, Enums.ModifierType.MainLandscape))
            strRow = strRow.Replace("<$POSTER>", ExportImage(.Poster, Enums.ModifierType.MainPoster))
        End With

        'Title
        Dim Title As String = tShow.MainDetails.Title

        'Actors
        Dim ActorsList As New List(Of String)
        For Each tActor As MediaContainers.Person In tShow.MainDetails.Actors
            ActorsList.Add(tActor.Name)
        Next

        'NFO Fields
        strRow = strRow.Replace("<$ACTORS>", StringUtils.HtmlEncode(String.Join(", ", ActorsList.ToArray)))
        strRow = strRow.Replace("<$CERTIFICATIONS>", StringUtils.HtmlEncode(String.Join(" / ", tShow.MainDetails.Certifications.ToArray)))
        strRow = strRow.Replace("<$COUNTRIES>", StringUtils.HtmlEncode(String.Join(" / ", tShow.MainDetails.Countries.ToArray)))
        strRow = strRow.Replace("<$CREATORS>", StringUtils.HtmlEncode(String.Join(" / ", tShow.MainDetails.Creators.ToArray)))
        strRow = strRow.Replace("<$DATEADDED>", StringUtils.HtmlEncode(Functions.ConvertFromUnixTimestamp(tShow.DateAdded).ToString("dd.MM.yyyy")))
        strRow = strRow.Replace("<$DATEMODIFIED>", StringUtils.HtmlEncode(Functions.ConvertFromUnixTimestamp(tShow.DateModified).ToString("dd.MM.yyyy")))
        strRow = strRow.Replace("<$GENRES>", StringUtils.HtmlEncode(String.Join(" / ", tShow.MainDetails.Genres.ToArray)))
        strRow = strRow.Replace("<$IMDBID>", StringUtils.HtmlEncode(tShow.MainDetails.UniqueIDs.IMDbId))
        strRow = strRow.Replace("<$LANGUAGE>", StringUtils.HtmlEncode(tShow.MainDetails.Language))
        strRow = strRow.Replace("<$LISTTITLE>", StringUtils.HtmlEncode(StringUtils.SortTokens(tShow.MainDetails.Title)))
        strRow = strRow.Replace("<$MPAA>", StringUtils.HtmlEncode(tShow.MainDetails.MPAA))
        strRow = strRow.Replace("<$ORIGINALTITLE>", StringUtils.HtmlEncode(tShow.MainDetails.OriginalTitle))
        strRow = strRow.Replace("<$PLOT>", StringUtils.HtmlEncode(tShow.MainDetails.Plot))
        strRow = strRow.Replace("<$PREMIERED>", StringUtils.HtmlEncode(tShow.MainDetails.Premiered))
        strRow = strRow.Replace("<$RATING>", StringUtils.HtmlEncode(If(tShow.MainDetails.RatingSpecified, Double.Parse(tShow.MainDetails.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), String.Empty)))
        strRow = strRow.Replace("<$RUNTIME>", StringUtils.HtmlEncode(tShow.MainDetails.Runtime))
        strRow = strRow.Replace("<$STATUS>", StringUtils.HtmlEncode(tShow.MainDetails.Status))
        strRow = strRow.Replace("<$STUDIOS>", StringUtils.HtmlEncode(String.Join(" / ", tShow.MainDetails.Studios.ToArray)))
        strRow = strRow.Replace("<$TAGS>", If(tShow.MainDetails.TagsSpecified, StringUtils.HtmlEncode((String.Join(" / ", tShow.MainDetails.Tags.ToArray))), String.Empty))
        strRow = strRow.Replace("<$TITLE>", StringUtils.HtmlEncode(Title))
        strRow = strRow.Replace("<$TMDBID>", StringUtils.HtmlEncode(CStr(tShow.MainDetails.UniqueIDs.TMDbId)))
        strRow = strRow.Replace("<$TVDBID>", StringUtils.HtmlEncode(CStr(tShow.MainDetails.UniqueIDs.TVDbId)))
        strRow = strRow.Replace("<$VOTES>", StringUtils.HtmlEncode(If(tShow.MainDetails.VotesSpecified, Double.Parse(tShow.MainDetails.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture), String.Empty)))

        Return strRow
    End Function

    Private Function SaveAll(ByVal TemplateSource As String, ByVal HTMLBody As StringBuilder) As String
        Try
            CopyDirectory(TemplateSource, _StrBuildPath, True)

            If _ExportSettings.Flags Then
                Directory.CreateDirectory(Path.Combine(_StrBuildPath, "flags"))
                Dim FlagSource As String = Path.Combine(Functions.AppPath, "Addons\addon.websitecreator\Images\Flags")
                CopyDirectory(FlagSource, Path.Combine(_StrBuildPath, "flags"), True)
            End If

            Dim strIndexFile As String = Path.Combine(_StrBuildPath, If(Not String.IsNullOrEmpty(_ExportSettings.Filename), _ExportSettings.Filename, "index.html"))
            If File.Exists(strIndexFile) Then
                File.Delete(strIndexFile)
            End If
            Dim myStream As Stream = File.OpenWrite(strIndexFile)
            If myStream IsNot Nothing Then
                myStream.Write(Encoding.ASCII.GetBytes(HTMLBody.ToString), 0, HTMLBody.ToString.Length)
                myStream.Close()
            End If
            Return strIndexFile
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            Return String.Empty
        End Try
    End Function

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments
        Dim DestinationPath As String
        Dim ResizePoster As Integer
        Dim SourcePath As String
    End Structure

    Private Structure ContentPart
        Dim Content As String
        Dim ContentType As Enums.ContentType
        Dim InnerContentPartMap As Object
        Dim IsLooped As Boolean
    End Structure

    Private Class AVSInfo

#Region "Properties"

        Public Property audBitrate() As String = String.Empty

        Public Property audChannels() As String = String.Empty

        Public Property audDetails() As String = String.Empty

        Public Property audLanguage() As String = String.Empty

        Public Property audLongLanguage() As String = String.Empty

        Public Property subLanguage() As String = String.Empty

        Public Property subLongLanguage() As String = String.Empty

        Public Property subType() As String = String.Empty

        Public Property vidAspect() As String = String.Empty

        Public Property vidBitrate() As String = String.Empty

        Public Property vidDetails() As String = String.Empty

        Public Property vidDimensions() As String = String.Empty

        Public Property vidDuration() As String = String.Empty

        Public Property vidHeight() As String = String.Empty

        Public Property vidLanguage() As String = String.Empty

        Public Property vidLongLanguage() As String = String.Empty

        Public Property vidMultiViewCount() As String = String.Empty

        Public Property vidMultiViewLayout() As String = String.Empty

        Public Property vidPrivateensions() As String = String.Empty

        Public Property vidScantype() As String = String.Empty

        Public Property vidStereoMode() As String = String.Empty

        Public Property vidWidth() As String = String.Empty

#End Region 'Properties

    End Class

    <Serializable()>
    <XmlRoot("exportsettings")>
    Public Class ExportSettings

#Region "Properties"

        <XmlElement("episodefanarts")>
        Public Property EpisodeFanarts() As Boolean = False

        <XmlElement("episodefanarts_maxheight")>
        Public Property EpisodeFanarts_MaxHeight() As Integer = -1

        <XmlElement("episodefanarts_maxwidth")>
        Public Property EpisodeFanarts_MaxWidth() As Integer = -1

        <XmlElement("episodeposters")>
        Public Property EpisodePosters() As Boolean = False

        <XmlElement("episodeposters_maxheight")>
        Public Property EpisodePosters_MaxHeight() As Integer = -1

        <XmlElement("episodeposters_maxwidth")>
        Public Property EpisodePosters_MaxWidth() As Integer = -1

        <XmlElement("filename")>
        Public Property Filename() As String = "index.html"

        <XmlElement("flags")>
        Public Property Flags() As Boolean = False

        <XmlElement("info")>
        Public Property Info() As String = String.Empty

        <XmlElement("mainbanners")>
        Public Property MainBanners() As Boolean = False

        <XmlElement("mainbanners_maxheight")>
        Public Property MainBanners_MaxHeight() As Integer = -1

        <XmlElement("mainbanners_maxwidth")>
        Public Property MainBanners_MaxWidth() As Integer = -1

        <XmlElement("maincharacterarts")>
        Public Property MainCharacterarts() As Boolean = False

        <XmlElement("maincleararts")>
        Public Property MainClearArts() As Boolean = False

        <XmlElement("mainclearlogos")>
        Public Property MainClearLogos() As Boolean = False

        <XmlElement("maindiscarts")>
        Public Property MainDiscArts() As Boolean = False

        <XmlElement("mainfanarts")>
        Public Property MainFanarts() As Boolean = False

        <XmlElement("mainfanarts_maxheight")>
        Public Property MainFanarts_MaxHeight() As Integer = -1

        <XmlElement("mainfanarts_maxwidth")>
        Public Property MainFanarts_MaxWidth() As Integer = -1

        <XmlElement("mainlandscapes")>
        Public Property MainLandscapes() As Boolean = False

        <XmlElement("mainlandscapes_maxheight")>
        Public Property MainLandscapes_MaxHeight() As Integer = -1

        <XmlElement("mainlandscapes_maxwidth")>
        Public Property MainLandscapes_MaxWidth() As Integer = -1

        <XmlElement("mainposters")>
        Public Property MainPosters() As Boolean = False

        <XmlElement("mainposters_maxheight")>
        Public Property MainPosters_MaxHeight() As Integer = -1

        <XmlElement("mainposters_maxwidth")>
        Public Property MainPosters_MaxWidth() As Integer = -1

        <XmlElement("seasonbanners")>
        Public Property SeasonBanners() As Boolean = False

        <XmlElement("seasonbanners_maxheight")>
        Public Property SeasonBanners_MaxHeight() As Integer = -1

        <XmlElement("seasonbanners_maxwidth")>
        Public Property SeasonBanners_MaxWidth() As Integer = -1

        <XmlElement("seasonfanarts")>
        Public Property SeasonFanarts() As Boolean = False

        <XmlElement("seasonfanarts_maxheight")>
        Public Property SeasonFanarts_MaxHeight() As Integer = -1

        <XmlElement("seasonfanarts_maxwidth")>
        Public Property SeasonFanarts_MaxWidth() As Integer = -1

        <XmlElement("seasonlandscapes")>
        Public Property SeasonLandscapes() As Boolean = False

        <XmlElement("seasonlandscapes_maxheight")>
        Public Property SeasonLandscapes_MaxHeight() As Integer = -1

        <XmlElement("seasonlandscapes_maxwidth")>
        Public Property SeasonLandscapes_MaxWidth() As Integer = -1

        <XmlElement("seasonposters")>
        Public Property SeasonPosters() As Boolean = False

        <XmlElement("seasonposters_maxheight")>
        Public Property SeasonPosters_MaxHeight() As Integer = -1

        <XmlElement("seasonposters_maxwidth")>
        Public Property SeasonPosters_MaxWidth() As Integer = -1

#End Region 'Properties 

    End Class

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

#End Region 'Nested Types

End Class