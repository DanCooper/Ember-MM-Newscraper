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
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization

Public Class MediaExporter

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public Delegate Function ShowProgress(ByVal title As String, ByVal operation As String) As Boolean

    Private _iCounter_Global As Integer
    Private _iCounter_TVEpisode As Integer
    Private _iCounter_TVSeason As Integer
    Private _iTotal_Movies As Integer
    Private _iTotal_TVEpisodes As Integer
    Private _iTotal_TVShows As Integer
    Private _strBuildPath As String = Path.Combine(Master.TempPath, "Export")
    Private _tExportSettings As New ExportSettings
    Private _tMovieList As List(Of Database.DBElement)
    Private _tTVShowList As List(Of Database.DBElement)

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
                part = New ContentPart With {.isLooped = False}
                part.Content = strPattern.Substring(0, iNextPart)
                tContentPartMap.Add(part)
                strPattern = strPattern.Substring(iNextPart)
                iNextPart = strPattern.IndexOf("<$$")
            ElseIf iNextPart = -1 Then
                'no more sections found, add the remaining html code
                part = New ContentPart With {.isLooped = False}
                part.Content = strPattern
                tContentPartMap.Add(part)
                strPattern = String.Empty
            Else
                If strPattern.IndexOf("<$$MOVIE>") = 0 Then
                    'create the looped section for movies
                    iEndPart = strPattern.IndexOf("</$$MOVIE>")
                    part = New ContentPart With {.ContentType = Enums.ContentType.Movie, .isLooped = True}
                    part.Content = strPattern.Substring(9, iEndPart - 9)
                    tContentPartMap.Add(part)
                    strPattern = strPattern.Substring(iEndPart + 10)
                    iNextPart = strPattern.IndexOf("<$$")
                ElseIf strPattern.IndexOf("<$$TVSHOW>") = 0 Then
                    'create the looped section for tv shows
                    iEndPart = strPattern.IndexOf("</$$TVSHOW>")
                    part = New ContentPart With {.ContentType = Enums.ContentType.TVShow, .isLooped = True}
                    part.Content = strPattern.Substring(10, iEndPart - 10)

                    'check if we have an InnerContentPart
                    If Not part.Content.IndexOf("<$$") = -1 Then
                        part.innerContentPartMap = Build_ContentPartMap(part.Content)
                        part.Content = String.Empty
                    End If

                    tContentPartMap.Add(part)
                    strPattern = strPattern.Substring(iEndPart + 11)
                    iNextPart = strPattern.IndexOf("<$$")
                ElseIf strPattern.IndexOf("<$$TVSEASON>") = 0 Then
                    'create the looped section for tv seasons
                    iEndPart = strPattern.IndexOf("</$$TVSEASON>")
                    part = New ContentPart With {.ContentType = Enums.ContentType.TVSeason, .isLooped = True}
                    part.Content = strPattern.Substring(12, iEndPart - 12)

                    'check if we have an InnerContentPart
                    If Not part.Content.IndexOf("<$$") = -1 Then
                        part.innerContentPartMap = Build_ContentPartMap(part.Content)
                        part.Content = String.Empty
                    End If

                    tContentPartMap.Add(part)
                    strPattern = strPattern.Substring(iEndPart + 13)
                    iNextPart = strPattern.IndexOf("<$$")
                ElseIf strPattern.IndexOf("<$$TVEPISODE>") = 0 Then
                    'create the looped section for tv episodes
                    iEndPart = strPattern.IndexOf("</$$TVEPISODE>")
                    part = New ContentPart With {.ContentType = Enums.ContentType.TVEpisode, .isLooped = True}
                    part.Content = strPattern.Substring(13, iEndPart - 13)

                    'check if we have an InnerContentPart
                    If Not part.Content.IndexOf("<$$") = -1 Then
                        part.innerContentPartMap = Build_ContentPartMap(part.Content)
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
            If part.isLooped Then
                'Movies
                If part.ContentType = Enums.ContentType.Movie Then
                    For Each tMovie As Database.DBElement In _tMovieList
                        If Not sfunction Is Nothing Then
                            If Not sfunction(tMovie.Movie.Title, String.Empty) Then Return Nothing
                        End If
                        HTMLBodyPart.Append(ProcessPattern_Movie(part, sfunction, tMovie))
                        _iCounter_Global += 1
                    Next

                    'TV Shows
                ElseIf part.ContentType = Enums.ContentType.TVShow Then
                    For Each tShow As Database.DBElement In _tTVShowList
                        If Not sfunction Is Nothing Then
                            If Not sfunction(tShow.TVShow.Title, String.Empty) Then Return Nothing
                        End If
                        HTMLBodyPart.Append(ProcessPattern_TVShow(part, tShow, sfunction))
                        _iCounter_Global += 1
                    Next

                    'TV Seasons
                ElseIf part.ContentType = Enums.ContentType.TVSeason Then
                    If tTVShow IsNot Nothing Then
                        _iCounter_TVSeason = 1
                        For Each tSeason As Database.DBElement In tTVShow.Seasons.Where(Function(f) Not f.TVSeason.IsAllSeasons)
                            If Not sfunction Is Nothing Then
                                If Not sfunction(tTVShow.TVShow.Title, tSeason.TVSeason.Title) Then Return Nothing
                            End If
                            HTMLBodyPart.Append(ProcessPattern_TVSeason(part, sfunction, tTVShow, tSeason))
                            _iCounter_TVSeason += 1
                        Next
                    End If

                    'TV Episodes
                ElseIf part.ContentType = Enums.ContentType.TVEpisode Then
                    If tTVShow IsNot Nothing AndAlso tTVSeason IsNot Nothing Then
                        _iCounter_TVEpisode = 1
                        For Each tEpisode As Database.DBElement In tTVShow.Episodes.Where(Function(f) f.TVEpisode.Season = tTVSeason.TVSeason.Season)
                            If Not sfunction Is Nothing Then
                                If Not sfunction(tTVShow.TVShow.Title, tEpisode.TVEpisode.Title) Then Return Nothing
                            End If
                            HTMLBodyPart.Append(ProcessPattern_TVEpisode(part, sfunction, tTVShow, tEpisode))
                            _iCounter_TVEpisode += 1
                        Next
                    End If
                End If
            Else
                HTMLBodyPart.Append(ProcessPattern_General(part))
            End If
        Next

        Return HTMLBodyPart
    End Function

    Public Function CreateTemplate(ByVal strTemplatePath As String, ByVal MovieList As List(Of Database.DBElement), ByVal TVShowList As List(Of Database.DBElement), Optional ByVal BuildPath As String = "", Optional ByVal sfunction As ShowProgress = Nothing) As String
        _tMovieList = MovieList
        _tTVShowList = TVShowList

        _iTotal_Movies = _tMovieList.Count
        _iTotal_TVShows = _tTVShowList.Count
        For Each tShow As Database.DBElement In _tTVShowList
            _iTotal_TVEpisodes += tShow.Episodes.Count
        Next

        If Not String.IsNullOrEmpty(BuildPath) Then
            _strBuildPath = BuildPath
        Else
            _strBuildPath = Path.Combine(Master.TempPath, "Export")
        End If

        FileUtils.Delete.DeleteDirectory(_strBuildPath)
        Directory.CreateDirectory(_strBuildPath)

        If strTemplatePath IsNot Nothing Then
            Dim htmlPath As String = Path.Combine(strTemplatePath, String.Concat(Master.eSettings.GeneralLanguage, ".html"))
            If Not File.Exists(htmlPath) Then
                htmlPath = Path.Combine(strTemplatePath, String.Concat("English_(en_US).html"))
            End If
            If Not File.Exists(htmlPath) Then
                logger.Warn(String.Concat("Can't find template """, htmlPath, """"))
                Return String.Empty
            End If

            Dim pattern As String = String.Empty
            pattern = File.ReadAllText(htmlPath)
            pattern = ProcessPattern_Settings(pattern)

            Dim ContentPartMap As List(Of ContentPart) = Build_ContentPartMap(pattern)

            _iCounter_Global = 1
            _iCounter_TVEpisode = 1
            _iCounter_TVSeason = 1
            Dim HTMLBody As New StringBuilder
            HTMLBody = Build_HTML(ContentPartMap, sfunction)

            If HTMLBody IsNot Nothing Then
                Return SaveAll(Directory.GetParent(htmlPath).FullName, HTMLBody)
            Else
                Return String.Empty
            End If
        Else
            logger.Warn(String.Concat("Can't find any template"))
            Return String.Empty
        End If
    End Function

    Private Function ExportImage(ByVal tImage As MediaContainers.Image, ByVal tImageType As Enums.ModifierType) As String
        Dim strPath As String = String.Empty

        If File.Exists(tImage.LocalFilePath) Then
            Select Case tImageType
                Case Enums.ModifierType.EpisodeFanart
                    If _tExportSettings.EpisodeFanarts Then
                        If Not _tExportSettings.EpisodeFanarts_MaxHeight = -1 OrElse Not _tExportSettings.EpisodeFanarts_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _tExportSettings.EpisodeFanarts_MaxWidth, _tExportSettings.EpisodeFanarts_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}/{1}/{2}-fanart.jpg", _iCounter_Global, _iCounter_TVSeason, _iCounter_TVEpisode)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_strBuildPath, strPath))
                                tImage.ImageOriginal.Clear()  'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}/{1}/{2}-fanart.jpg", _iCounter_Global, _iCounter_TVSeason, _iCounter_TVEpisode)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.EpisodePoster
                    If _tExportSettings.EpisodePosters Then
                        If Not _tExportSettings.EpisodePosters_MaxHeight = -1 OrElse Not _tExportSettings.EpisodePosters_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _tExportSettings.EpisodePosters_MaxWidth, _tExportSettings.EpisodePosters_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}/{1}/{2}-poster.jpg", _iCounter_Global, _iCounter_TVSeason, _iCounter_TVEpisode)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_strBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}/{1}/{2}-poster.jpg", _iCounter_Global, _iCounter_TVSeason, _iCounter_TVEpisode)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.MainBanner
                    If _tExportSettings.MainBanners Then
                        If Not _tExportSettings.MainBanners_MaxHeight = -1 OrElse Not _tExportSettings.MainBanners_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.Movie, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _tExportSettings.MainBanners_MaxWidth, _tExportSettings.MainBanners_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}-banner.jpg", _iCounter_Global)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_strBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}-banner.jpg", _iCounter_Global)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.MainCharacterArt
                    If _tExportSettings.MainCharacterarts Then
                        strPath = String.Format("exportdata/{0}-characterart.png", _iCounter_Global)
                        File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                    End If
                Case Enums.ModifierType.MainClearArt
                    If _tExportSettings.MainClearArts Then
                        strPath = String.Format("exportdata/{0}-clearart.png", _iCounter_Global)
                        File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                    End If
                Case Enums.ModifierType.MainClearLogo
                    If _tExportSettings.MainClearLogos Then
                        strPath = String.Format("exportdata/{0}-clearlogo.png", _iCounter_Global)
                        File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                    End If
                Case Enums.ModifierType.MainDiscArt
                    If _tExportSettings.MainClearArts Then
                        strPath = String.Format("exportdata/{0}-discart.png", _iCounter_Global)
                        File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                    End If
                Case Enums.ModifierType.MainFanart
                    If _tExportSettings.MainFanarts Then
                        If Not _tExportSettings.MainFanarts_MaxHeight = -1 OrElse Not _tExportSettings.MainFanarts_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.Movie, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _tExportSettings.MainFanarts_MaxWidth, _tExportSettings.MainFanarts_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}-fanart.jpg", _iCounter_Global)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_strBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}-fanart.jpg", _iCounter_Global)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.MainKeyart
                    If _tExportSettings.MainKeyarts Then
                        If Not _tExportSettings.MainKeyarts_MaxHeight = -1 OrElse Not _tExportSettings.MainKeyarts_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _tExportSettings.MainKeyarts_MaxWidth, _tExportSettings.MainKeyarts_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}-keyart.jpg", _iCounter_Global)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_strBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}-keyart.jpg", _iCounter_Global)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.MainLandscape
                    If _tExportSettings.MainLandscapes Then
                        If Not _tExportSettings.MainLandscapes_MaxHeight = -1 OrElse Not _tExportSettings.MainLandscapes_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.Movie, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _tExportSettings.MainLandscapes_MaxWidth, _tExportSettings.MainLandscapes_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}-landscape.jpg", _iCounter_Global)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_strBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}-landscape.jpg", _iCounter_Global)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.MainPoster
                    If _tExportSettings.MainPosters Then
                        If Not _tExportSettings.MainPosters_MaxHeight = -1 OrElse Not _tExportSettings.MainPosters_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _tExportSettings.MainPosters_MaxWidth, _tExportSettings.MainPosters_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}-poster.jpg", _iCounter_Global)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_strBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}-poster.jpg", _iCounter_Global)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.SeasonBanner
                    If _tExportSettings.SeasonBanners Then
                        If Not _tExportSettings.SeasonBanners_MaxHeight = -1 OrElse Not _tExportSettings.SeasonBanners_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _tExportSettings.SeasonBanners_MaxWidth, _tExportSettings.SeasonBanners_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}/{1}-banner.jpg", _iCounter_Global, _iCounter_TVSeason)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_strBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}/{1}-banner.jpg", _iCounter_Global, _iCounter_TVSeason)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.SeasonFanart
                    If _tExportSettings.SeasonFanarts Then
                        If Not _tExportSettings.SeasonFanarts_MaxHeight = -1 OrElse Not _tExportSettings.SeasonFanarts_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _tExportSettings.SeasonFanarts_MaxWidth, _tExportSettings.SeasonFanarts_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}/{1}-fanart.jpg", _iCounter_Global, _iCounter_TVSeason)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_strBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}/{1}-fanart.jpg", _iCounter_Global, _iCounter_TVSeason)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.SeasonLandscape
                    If _tExportSettings.SeasonLandscapes Then
                        If Not _tExportSettings.SeasonLandscapes_MaxHeight = -1 OrElse Not _tExportSettings.SeasonLandscapes_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _tExportSettings.SeasonLandscapes_MaxWidth, _tExportSettings.SeasonLandscapes_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}/{1}-landscape.jpg", _iCounter_Global, _iCounter_TVSeason)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_strBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}/{1}-landscape.jpg", _iCounter_Global, _iCounter_TVSeason)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                        End If
                    End If
                Case Enums.ModifierType.SeasonPoster
                    If _tExportSettings.SeasonPosters Then
                        If Not _tExportSettings.SeasonPosters_MaxHeight = -1 OrElse Not _tExportSettings.SeasonPosters_MaxWidth = -1 Then
                            If tImage.LoadAndCache(Enums.ContentType.TV, True, True) Then
                                Dim nImg As Image = tImage.ImageOriginal.Image
                                ImageUtils.ResizeImage(nImg, _tExportSettings.SeasonPosters_MaxWidth, _tExportSettings.SeasonPosters_MaxHeight)
                                tImage.ImageOriginal.UpdateMSfromImg(nImg)
                                strPath = String.Format("exportdata/{0}/{1}-poster.jpg", _iCounter_Global, _iCounter_TVSeason)
                                tImage.ImageOriginal.SaveToFile(Path.Combine(_strBuildPath, strPath))
                                tImage.ImageOriginal.Clear() 'Dispose to save memory
                            End If
                        Else
                            strPath = String.Format("exportdata/{0}/{1}-poster.jpg", _iCounter_Global, _iCounter_TVSeason)
                            File.Copy(tImage.LocalFilePath, Path.Combine(_strBuildPath, strPath), True)
                        End If
                    End If
            End Select
        End If

        Return strPath
    End Function

    Private Function GetAVSInfo(ByVal fInfo As MediaContainers.Fileinfo) As AVSInfo
        Dim nInfo As New AVSInfo

        Dim tVid As New MediaContainers.Video
        Dim tAud As New MediaContainers.Audio
        Dim tRes As String = String.Empty


        If fInfo IsNot Nothing Then
            If fInfo.StreamDetails.Video.Count > 0 Then
                tVid = NFO.GetBestVideo(fInfo)
                tRes = NFO.GetResFromDimensions(tVid)

                nInfo.vidBitrate = tVid.Bitrate
                nInfo.vidFileSize = CStr(tVid.Filesize)
                nInfo.vidMultiViewCount = tVid.MultiViewCount
                nInfo.vidMultiViewLayout = tVid.MultiViewLayout
                nInfo.vidAspect = tVid.Aspect
                nInfo.vidDuration = tVid.Duration
                nInfo.vidHeight = tVid.Height
                nInfo.vidLanguage = tVid.Language
                nInfo.vidLongLanguage = tVid.LongLanguage
                nInfo.vidScantype = tVid.Scantype
                nInfo.vidStereoMode = tVid.StereoMode
                nInfo.vidWidth = tVid.Width
                nInfo.vidDetails = String.Format("{0} / {1}", If(String.IsNullOrEmpty(tRes), Master.eLang.GetString(138, "Unknown"), tRes), If(String.IsNullOrEmpty(tVid.Codec), Master.eLang.GetString(138, "Unknown"), tVid.Codec)).ToUpper
                nInfo.vidDimensions = NFO.GetDimensionsFromVideo(tVid)
            End If

            If fInfo.StreamDetails.Audio.Count > 0 Then
                tAud = NFO.GetBestAudio(fInfo, False)

                nInfo.audBitrate = tAud.Bitrate
                nInfo.audChannels = tAud.Channels
                nInfo.audLanguage = tAud.Language
                nInfo.audLongLanguage = tAud.LongLanguage
                nInfo.audDetails = String.Format("{0}ch / {1}", If(String.IsNullOrEmpty(tAud.Channels), Master.eLang.GetString(138, "Unknown"), tAud.Channels), If(String.IsNullOrEmpty(tAud.Codec), Master.eLang.GetString(138, "Unknown"), tAud.Codec)).ToUpper
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
                        If Not String.IsNullOrEmpty(subtitleinfo.Type) Then
                            nInfo.subType = nInfo.subType & ";" & subtitleinfo.Type
                        End If
                    End If
                Next
            End If
        End If

        Return nInfo
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
                        nInfo.audDetails = If(audioinfo.CodecSpecified, nInfo.audDetails & ";" & String.Format("{0}ch / {1}", If(String.IsNullOrEmpty(audioinfo.Channels), Master.eLang.GetString(138, "Unknown"), audioinfo.Channels), If(String.IsNullOrEmpty(audioinfo.Codec), Master.eLang.GetString(138, "Unknown"), audioinfo.Codec)).ToUpper, nInfo.audDetails)
                    End If
                Next
            End If
        End If
        Return nInfo
    End Function

    Private Function GetFileSize(ByVal fPath As String) As String
        Dim fSize As Long = 0

        If Not String.IsNullOrEmpty(fPath) Then
            If FileUtils.Common.isStacked(fPath) OrElse FileUtils.Common.isVideoTS(fPath) OrElse FileUtils.Common.isBDRip(fPath) Then
                Try
                    Dim sExt As String = Path.GetExtension(fPath).ToLower
                    Dim oFile As String = FileUtils.Common.RemoveStackingMarkers(fPath)
                    Dim sFile As New List(Of String)
                    Dim bIsVTS As Boolean = False

                    If sExt = ".ifo" OrElse sExt = ".bup" OrElse sExt = ".vob" Then
                        bIsVTS = True
                    End If

                    If bIsVTS Then
                        Try
                            sFile.AddRange(Directory.GetFiles(Directory.GetParent(fPath).FullName, "VTS*.VOB"))
                        Catch
                        End Try
                    ElseIf sExt = ".m2ts" Then
                        Try
                            sFile.AddRange(Directory.GetFiles(Directory.GetParent(fPath).FullName, "*.m2ts"))
                        Catch
                        End Try
                    Else
                        Try
                            Dim strName As String = Path.GetFileNameWithoutExtension(FileUtils.Common.RemoveStackingMarkers(fPath))
                            sFile.AddRange(Directory.GetFiles(Directory.GetParent(fPath).FullName, String.Concat(strName, "*")))
                        Catch
                        End Try
                    End If

                    For Each tFile As String In sFile
                        fSize += New FileInfo(tFile).Length
                    Next
                Catch ex As Exception
                End Try
            Else
                fSize = New FileInfo(fPath).Length
            End If

            Select Case fSize
                Case 0 To 1023
                    Return fSize & " Bytes"
                Case 1024 To 1048575
                    Return String.Concat((fSize / 1024).ToString("###0.00"), " KB")
                Case 1048576 To 1043741824
                    Return String.Concat((fSize / 1024 ^ 2).ToString("###0.00"), " MB")
                Case Is > 1043741824
                    Return String.Concat((fSize / 1024 ^ 3).ToString("###0.00"), " GB")
            End Select
        Else
            Return String.Empty
        End If

        Return String.Empty
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
        If APIXML.Flags.Count > 0 Then
            Dim fiAV As New MediaContainers.Fileinfo
            Select Case tContentType
                Case Enums.ContentType.Movie
                    fiAV = tDBElement.Movie.FileInfo
                Case Enums.ContentType.TVEpisode
                    fiAV = tDBElement.TVEpisode.FileInfo
            End Select
            Dim tVideo As MediaContainers.Video = NFO.GetBestVideo(fiAV)
            Dim tAudio As MediaContainers.Audio = NFO.GetBestAudio(fiAV, False)

            Dim vresFlag As APIXML.Flag = APIXML.Flags.FirstOrDefault(Function(f) f.Name = NFO.GetResFromDimensions(tVideo).ToLower AndAlso f.Type = APIXML.FlagType.VideoResolution)
            If vresFlag IsNot Nothing Then
                strRow = strRow.Replace("<$FLAG_VRES>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vresFlag.Path))).Replace("\", "/")
            Else
                vresFlag = APIXML.Flags.FirstOrDefault(Function(f) f.Name = "defaultscreen" AndAlso f.Type = APIXML.FlagType.VideoResolution)
                If vresFlag IsNot Nothing Then
                    strRow = strRow.Replace("<$FLAG_VRES>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vresFlag.Path))).Replace("\", "/")
                End If
            End If

            'Dim vsourceFlag As APIXML.Flag = APIXML.lFlags.FirstOrDefault(Function(f) f.Name = APIXML.GetFileSource(AVMovie.Filename) AndAlso f.Type = APIXML.FlagType.VideoSource)
            Dim vsourceFlag As APIXML.Flag = APIXML.Flags.FirstOrDefault(Function(f) f.Name.ToLower = tDBElement.VideoSource AndAlso f.Type = APIXML.FlagType.VideoSource)
            If vsourceFlag IsNot Nothing Then
                strRow = strRow.Replace("<$FLAG_VSOURCE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vsourceFlag.Path))).Replace("\", "/")
            Else
                vsourceFlag = APIXML.Flags.FirstOrDefault(Function(f) f.Name = "defaultscreen" AndAlso f.Type = APIXML.FlagType.VideoSource)
                If vsourceFlag IsNot Nothing Then
                    strRow = strRow.Replace("<$FLAG_VSOURCE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vsourceFlag.Path))).Replace("\", "/")
                End If
            End If

            Dim vcodecFlag As APIXML.Flag = APIXML.Flags.FirstOrDefault(Function(f) f.Name = tVideo.Codec.ToLower AndAlso f.Type = APIXML.FlagType.VideoCodec)
            If vcodecFlag IsNot Nothing Then
                strRow = strRow.Replace("<$FLAG_VTYPE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vcodecFlag.Path))).Replace("\", "/")
            Else
                vcodecFlag = APIXML.Flags.FirstOrDefault(Function(f) f.Name = "defaultscreen" AndAlso f.Type = APIXML.FlagType.VideoCodec)
                If vcodecFlag IsNot Nothing Then
                    strRow = strRow.Replace("<$FLAG_VTYPE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(vcodecFlag.Path))).Replace("\", "/")
                End If
            End If

            Dim acodecFlag As APIXML.Flag = APIXML.Flags.FirstOrDefault(Function(f) f.Name = tAudio.Codec.ToLower AndAlso f.Type = APIXML.FlagType.AudioCodec)
            If acodecFlag IsNot Nothing Then
                strRow = strRow.Replace("<$FLAG_ATYPE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(acodecFlag.Path))).Replace("\", "/")
            Else
                acodecFlag = APIXML.Flags.FirstOrDefault(Function(f) f.Name = "defaultaudio" AndAlso f.Type = APIXML.FlagType.AudioCodec)
                If acodecFlag IsNot Nothing Then
                    strRow = strRow.Replace("<$FLAG_ATYPE>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(acodecFlag.Path))).Replace("\", "/")
                End If
            End If

            Dim achanFlag As APIXML.Flag = APIXML.Flags.FirstOrDefault(Function(f) f.Name = tAudio.Channels AndAlso f.Type = APIXML.FlagType.AudioChan)
            If achanFlag IsNot Nothing Then
                strRow = strRow.Replace("<$FLAG_ACHAN>", String.Concat("flags", Path.DirectorySeparatorChar, Path.GetFileName(achanFlag.Path))).Replace("\", "/")
            Else
                achanFlag = APIXML.Flags.FirstOrDefault(Function(f) f.Name = "defaultaudio" AndAlso f.Type = APIXML.FlagType.AudioChan)
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
        strRow = strRow.Replace("<$NOW>", Date.Now.ToLongDateString) 'Save Build Date. might be useful info!
        strRow = strRow.Replace("<$TOTAL_MOVIES>", _iTotal_Movies.ToString)
        strRow = strRow.Replace("<$TOTAL_TVEPISODES>", _iTotal_TVEpisodes.ToString)
        strRow = strRow.Replace("<$TOTAL_TVSHOWS>", _iTotal_TVShows.ToString)

        Return strRow
    End Function

    Private Function ProcessPattern_Movie(ByVal tContentPart As ContentPart, ByVal sfunction As ShowProgress, ByVal tMovie As Database.DBElement) As String
        Dim strRow As String = tContentPart.Content

        'Special Strings
        strRow = strRow.Replace("<$COUNT>", _iCounter_Global.ToString)
        strRow = strRow.Replace("<$DIRNAME>", StringUtils.HtmlEncode(Path.GetDirectoryName(tMovie.Filename)))
        strRow = strRow.Replace("<$FILENAME>", StringUtils.HtmlEncode(Path.GetFileName(tMovie.Filename)))
        strRow = strRow.Replace("<$FILESIZE>", StringUtils.HtmlEncode(GetFileSize(tMovie.Filename)))
        strRow = strRow.Replace("<$NOW>", Date.Now.ToLongDateString) 'Save Build Date. might be useful info!
        strRow = strRow.Replace("<$PATH>", StringUtils.HtmlEncode(tMovie.Filename))

        'Images
        With tMovie.ImagesContainer
            strRow = strRow.Replace("<$BANNER>", ExportImage(.Banner, Enums.ModifierType.MainBanner))
            strRow = strRow.Replace("<$CLEARART>", ExportImage(.ClearArt, Enums.ModifierType.MainClearArt))
            strRow = strRow.Replace("<$CLEARLOGO>", ExportImage(.ClearLogo, Enums.ModifierType.MainClearLogo))
            strRow = strRow.Replace("<$DISCART>", ExportImage(.DiscArt, Enums.ModifierType.MainDiscArt))
            strRow = strRow.Replace("<$FANART>", ExportImage(.Fanart, Enums.ModifierType.MainFanart))
            strRow = strRow.Replace("<$KEYART>", ExportImage(.Keyart, Enums.ModifierType.MainKeyart))
            strRow = strRow.Replace("<$LANDSCAPE>", ExportImage(.Landscape, Enums.ModifierType.MainLandscape))
            strRow = strRow.Replace("<$POSTER>", ExportImage(.Poster, Enums.ModifierType.MainPoster))
        End With

        'Actors
        Dim ActorsList As New List(Of String)
        For Each tActor As MediaContainers.Person In tMovie.Movie.Actors
            ActorsList.Add(tActor.Name)
        Next

        'NFO Fields
        strRow = strRow.Replace("<$ACTORS>", StringUtils.HtmlEncode(String.Join(", ", ActorsList.ToArray)))
        strRow = strRow.Replace("<$CERTIFICATIONS>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.Movie.Certifications.ToArray)))
        strRow = strRow.Replace("<$COUNTRIES>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.Movie.Countries.ToArray)))
        strRow = strRow.Replace("<$CREDITS>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.Movie.Credits.ToArray)))
        strRow = strRow.Replace("<$DATEADDED>", StringUtils.HtmlEncode(tMovie.Movie.DateAdded))
        strRow = strRow.Replace("<$DATEMODIFIED>", StringUtils.HtmlEncode(tMovie.Movie.DateModified))
        strRow = strRow.Replace("<$DIRECTORS>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.Movie.Directors.ToArray)))
        strRow = strRow.Replace("<$EDITION>", StringUtils.HtmlEncode(tMovie.Movie.Edition))
        strRow = strRow.Replace("<$GENRES>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.Movie.Genres.ToArray)))
        strRow = strRow.Replace("<$IMDBID>", StringUtils.HtmlEncode(tMovie.Movie.UniqueIDs.IMDbId))
        strRow = strRow.Replace("<$LANGUAGE>", StringUtils.HtmlEncode(tMovie.Movie.Language))
        strRow = strRow.Replace("<$LASTPLAYED>", StringUtils.HtmlEncode(tMovie.Movie.LastPlayed))
        strRow = strRow.Replace("<$LISTTITLE>", StringUtils.HtmlEncode(StringUtils.SortTokens(tMovie.Movie.Title)))
        strRow = strRow.Replace("<$MPAA>", StringUtils.HtmlEncode(tMovie.Movie.MPAA))
        strRow = strRow.Replace("<$ORIGINALTITLE>", StringUtils.HtmlEncode(tMovie.Movie.OriginalTitle))
        strRow = strRow.Replace("<$OUTLINE>", StringUtils.HtmlEncode(tMovie.Movie.Outline))
        strRow = strRow.Replace("<$PLAYCOUNT>", CStr(tMovie.Movie.PlayCount))
        strRow = strRow.Replace("<$PLOT>", StringUtils.HtmlEncode(tMovie.Movie.Plot))
        strRow = strRow.Replace("<$PREMIERED>", StringUtils.HtmlEncode(tMovie.Movie.Premiered))
        strRow = strRow.Replace("<$RATING>", StringUtils.HtmlEncode(If(tMovie.Movie.RatingSpecified, Double.Parse(tMovie.Movie.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), String.Empty)))
        strRow = strRow.Replace("<$RUNTIME>", StringUtils.HtmlEncode(tMovie.Movie.Runtime))
        strRow = strRow.Replace("<$STUDIOS>", StringUtils.HtmlEncode(String.Join(" / ", tMovie.Movie.Studios.ToArray)))
        strRow = strRow.Replace("<$TAGLINE>", StringUtils.HtmlEncode(tMovie.Movie.Tagline))
        strRow = strRow.Replace("<$TAGS>", If(tMovie.Movie.TagsSpecified, StringUtils.HtmlEncode((String.Join(" / ", tMovie.Movie.Tags.ToArray))), String.Empty))
        strRow = strRow.Replace("<$TITLE>", StringUtils.HtmlEncode(tMovie.Movie.Title))
        strRow = strRow.Replace("<$TMDBCOLID>", StringUtils.HtmlEncode(tMovie.Movie.UniqueIDs.TMDbCollectionId.ToString))
        strRow = strRow.Replace("<$TMDBID>", StringUtils.HtmlEncode(tMovie.Movie.UniqueIDs.TMDbId.ToString))
        strRow = strRow.Replace("<$TOP250>", StringUtils.HtmlEncode(tMovie.Movie.Top250.ToString))
        strRow = strRow.Replace("<$TRAILER>", StringUtils.HtmlEncode(tMovie.Movie.Trailer))
        strRow = strRow.Replace("<$VIDEOSOURCE>", StringUtils.HtmlEncode(tMovie.Movie.VideoSource))
        strRow = strRow.Replace("<$VOTES>", StringUtils.HtmlEncode(If(tMovie.Movie.VotesSpecified, Integer.Parse(tMovie.Movie.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture), String.Empty)))
        strRow = strRow.Replace("<$YEAR>", tMovie.Movie.Year)

        'FileInfo
        Dim fInfo As AVSInfo = GetAVSInfo(tMovie.Movie.FileInfo)

        'Audio
        strRow = strRow.Replace("<$AUDIO>", fInfo.audDetails)
        strRow = strRow.Replace("<$AUDIOBITRATE>", fInfo.audBitrate)
        strRow = strRow.Replace("<$AUDIOCHANNELS>", fInfo.audChannels)
        strRow = strRow.Replace("<$AUDIOLANGUAGE>", fInfo.audLanguage)
        strRow = strRow.Replace("<$AUDIOLONGLANGUAGE>", fInfo.audLongLanguage)

        Dim aInfo As AVSInfo = GetDetailAudioInfo(tMovie.Movie.FileInfo)
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
        strRow = strRow.Replace("<$VIDEOFILESIZE>", fInfo.vidFileSize)
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
        _tExportSettings = New ExportSettings

        Dim regSettings As Match = Regex.Match(strPattern, "<exportsettings>.*?</exportsettings>", RegexOptions.Singleline)
        If regSettings.Success Then
            Dim xmlSer As XmlSerializer = Nothing
            Using xmlSR As TextReader = New StringReader(regSettings.Value)
                xmlSer = New XmlSerializer(GetType(ExportSettings))
                _tExportSettings = DirectCast(xmlSer.Deserialize(xmlSR), ExportSettings)
            End Using
            strPattern = strPattern.Replace(regSettings.Value, String.Empty)
        End If

        Return strPattern
    End Function

    Private Function ProcessPattern_TVEpisode(ByVal tContentPart As ContentPart, ByVal sfunction As ShowProgress, ByVal tShow As Database.DBElement, ByVal tEpisode As Database.DBElement) As String
        Dim strRow As String = tContentPart.Content

        'Special Strings
        strRow = strRow.Replace("<$COUNT>", _iCounter_Global.ToString)
        strRow = strRow.Replace("<$COUNT_TVSEASON>", _iCounter_TVSeason.ToString)
        strRow = strRow.Replace("<$COUNT_TVEPISODE>", _iCounter_TVEpisode.ToString)
        strRow = strRow.Replace("<$FILENAME>", StringUtils.HtmlEncode(Path.GetFileName(tEpisode.Filename)))
        strRow = strRow.Replace("<$FILESIZE>", StringUtils.HtmlEncode(GetFileSize(tEpisode.Filename)))
        strRow = strRow.Replace("<$MISSING>", If(tEpisode.FilenameID = -1, "true", "false"))
        strRow = strRow.Replace("<$PATH>", StringUtils.HtmlEncode(tEpisode.Filename))

        'Images
        With tEpisode.ImagesContainer
            strRow = strRow.Replace("<$FANART>", ExportImage(.Fanart, Enums.ModifierType.EpisodeFanart))
            strRow = strRow.Replace("<$POSTER>", ExportImage(.Poster, Enums.ModifierType.EpisodePoster))
        End With

        'Actors
        Dim ActorsList_Episode As New List(Of String)
        For Each tActor As MediaContainers.Person In tEpisode.TVEpisode.Actors
            ActorsList_Episode.Add(tActor.Name)
        Next

        'Guest Stars
        Dim GuestStarsList_Episode As New List(Of String)
        For Each tActor As MediaContainers.Person In tEpisode.TVEpisode.GuestStars
            GuestStarsList_Episode.Add(tActor.Name)
        Next

        'NFO Fields
        strRow = strRow.Replace("<$ACTORS>", StringUtils.HtmlEncode(String.Join(", ", ActorsList_Episode.ToArray)))
        strRow = strRow.Replace("<$AIRED>", StringUtils.HtmlEncode(tEpisode.TVEpisode.Aired))
        strRow = strRow.Replace("<$CREDITS>", StringUtils.HtmlEncode(String.Join(" / ", tEpisode.TVEpisode.Credits.ToArray)))
        strRow = strRow.Replace("<$DATEADDED>", StringUtils.HtmlEncode(tEpisode.TVEpisode.DateAdded))
        strRow = strRow.Replace("<$DIRECTORS>", StringUtils.HtmlEncode(String.Join(" / ", tEpisode.TVEpisode.Directors.ToArray)))
        strRow = strRow.Replace("<$EPISODE>", StringUtils.HtmlEncode(CStr(tEpisode.TVEpisode.Episode)))
        strRow = strRow.Replace("<$GUESTSTARS>", StringUtils.HtmlEncode(String.Join(" / ", GuestStarsList_Episode.ToArray)))
        strRow = strRow.Replace("<$IMDBID>", StringUtils.HtmlEncode(tEpisode.TVEpisode.UniqueIDs.IMDbId.ToString))
        strRow = strRow.Replace("<$LASTPLAYED>", StringUtils.HtmlEncode(tEpisode.TVEpisode.LastPlayed))
        strRow = strRow.Replace("<$PLOT>", StringUtils.HtmlEncode(tEpisode.TVEpisode.Plot))
        strRow = strRow.Replace("<$RATING>", StringUtils.HtmlEncode(If(tEpisode.TVEpisode.RatingSpecified, Double.Parse(tEpisode.TVEpisode.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), String.Empty)))
        strRow = strRow.Replace("<$RUNTIME>", StringUtils.HtmlEncode(tEpisode.TVEpisode.Runtime))
        strRow = strRow.Replace("<$SEASON>", StringUtils.HtmlEncode(CStr(tEpisode.TVEpisode.Season)))
        strRow = strRow.Replace("<$TITLE>", StringUtils.HtmlEncode(tEpisode.TVEpisode.Title))
        strRow = strRow.Replace("<$TMDBID>", StringUtils.HtmlEncode(tEpisode.TVEpisode.UniqueIDs.TMDbId.ToString))
        strRow = strRow.Replace("<$TVDBID>", StringUtils.HtmlEncode(tEpisode.TVEpisode.UniqueIDs.TVDbId.ToString))
        strRow = strRow.Replace("<$VIDEOSOURCE>", StringUtils.HtmlEncode(tEpisode.TVEpisode.VideoSource))
        strRow = strRow.Replace("<$VOTES>", StringUtils.HtmlEncode(If(tEpisode.TVEpisode.VotesSpecified, Integer.Parse(tEpisode.TVEpisode.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture), String.Empty)))

        'FileInfo
        Dim fInfo As AVSInfo = GetAVSInfo(tEpisode.TVEpisode.FileInfo)

        'Audio
        strRow = strRow.Replace("<$AUDIO>", fInfo.audDetails)
        strRow = strRow.Replace("<$AUDIOBITRATE>", fInfo.audBitrate)
        strRow = strRow.Replace("<$AUDIOCHANNELS>", fInfo.audChannels)
        strRow = strRow.Replace("<$AUDIOLANGUAGE>", fInfo.audLanguage)
        strRow = strRow.Replace("<$AUDIOLONGLANGUAGE>", fInfo.audLongLanguage)

        Dim aInfo As AVSInfo = GetDetailAudioInfo(tEpisode.TVEpisode.FileInfo)
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
        strRow = strRow.Replace("<$VIDEOFILESIZE>", fInfo.vidFileSize)
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

        If tContentPart.innerContentPartMap IsNot Nothing Then
            strRow = Build_HTML(DirectCast(tContentPart.innerContentPartMap, List(Of ContentPart)), sfunction, tShow, tSeason).ToString
        End If

        'Special Strings
        strRow = strRow.Replace("<$COUNT>", _iCounter_Global.ToString)
        strRow = strRow.Replace("<$COUNT_TVSEASON>", _iCounter_TVSeason.ToString)
        strRow = strRow.Replace("<$EPISODES>", StringUtils.HtmlEncode(CStr(tShow.Episodes.Where(Function(f) f.TVEpisode.Season = tSeason.TVSeason.Season).Count)))

        'Images
        With tSeason.ImagesContainer
            strRow = strRow.Replace("<$BANNER>", ExportImage(.Banner, Enums.ModifierType.SeasonBanner))
            strRow = strRow.Replace("<$FANART>", ExportImage(.Fanart, Enums.ModifierType.SeasonFanart))
            strRow = strRow.Replace("<$LANDSCAPE>", ExportImage(.Landscape, Enums.ModifierType.SeasonLandscape))
            strRow = strRow.Replace("<$POSTER>", ExportImage(.Poster, Enums.ModifierType.SeasonPoster))
        End With

        'NFO Fields
        strRow = strRow.Replace("<$AIRED>", StringUtils.HtmlEncode(tSeason.TVSeason.Aired))
        strRow = strRow.Replace("<$PLOT>", StringUtils.HtmlEncode(tSeason.TVSeason.Plot))
        strRow = strRow.Replace("<$SEASON>", StringUtils.HtmlEncode(CStr(tSeason.TVSeason.Season)))
        strRow = strRow.Replace("<$TITLE>", StringUtils.HtmlEncode(tSeason.TVSeason.Title))
        strRow = strRow.Replace("<$TMDBID>", StringUtils.HtmlEncode(tSeason.TVSeason.UniqueIDs.TMDbId.ToString))
        strRow = strRow.Replace("<$TVDBID>", StringUtils.HtmlEncode(tSeason.TVSeason.UniqueIDs.TVDbId.ToString))

        Return strRow
    End Function

    Private Function ProcessPattern_TVShow(ByVal tContentPart As ContentPart, ByVal tShow As Database.DBElement, ByVal sfunction As ShowProgress) As String
        Dim strRow As String = tContentPart.Content

        If tContentPart.innerContentPartMap IsNot Nothing Then
            strRow = Build_HTML(DirectCast(tContentPart.innerContentPartMap, List(Of ContentPart)), sfunction, tShow).ToString
        End If

        'Special Strings
        strRow = strRow.Replace("<$COUNT>", _iCounter_Global.ToString)
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
            strRow = strRow.Replace("<$KEYART>", ExportImage(.Keyart, Enums.ModifierType.MainKeyart))
            strRow = strRow.Replace("<$LANDSCAPE>", ExportImage(.Landscape, Enums.ModifierType.MainLandscape))
            strRow = strRow.Replace("<$POSTER>", ExportImage(.Poster, Enums.ModifierType.MainPoster))
        End With

        'Actors
        Dim ActorsList As New List(Of String)
        For Each tActor As MediaContainers.Person In tShow.TVShow.Actors
            ActorsList.Add(tActor.Name)
        Next

        'NFO Fields
        strRow = strRow.Replace("<$ACTORS>", StringUtils.HtmlEncode(String.Join(", ", ActorsList.ToArray)))
        strRow = strRow.Replace("<$CERTIFICATIONS>", StringUtils.HtmlEncode(String.Join(" / ", tShow.TVShow.Certifications.ToArray)))
        strRow = strRow.Replace("<$COUNTRIES>", StringUtils.HtmlEncode(String.Join(" / ", tShow.TVShow.Countries.ToArray)))
        strRow = strRow.Replace("<$CREATORS>", StringUtils.HtmlEncode(String.Join(" / ", tShow.TVShow.Creators.ToArray)))
        'strRow = strRow.Replace("<$DATEADDED>", StringUtils.HtmlEncode(tShow.TVShow.DateAdded))
        'strRow = strRow.Replace("<$DATEMODIFIED>", StringUtils.HtmlEncode(tShow.TVShow.DateModified))
        strRow = strRow.Replace("<$DIRECTORS>", StringUtils.HtmlEncode(String.Join(" / ", tShow.TVShow.Directors.ToArray)))
        strRow = strRow.Replace("<$GENRES>", StringUtils.HtmlEncode(String.Join(" / ", tShow.TVShow.Genres.ToArray)))
        strRow = strRow.Replace("<$IMDBID>", StringUtils.HtmlEncode(tShow.TVShow.UniqueIDs.IMDbId))
        strRow = strRow.Replace("<$LANGUAGE>", StringUtils.HtmlEncode(tShow.TVShow.Language))
        strRow = strRow.Replace("<$LISTTITLE>", StringUtils.HtmlEncode(StringUtils.SortTokens(tShow.TVShow.Title)))
        strRow = strRow.Replace("<$MPAA>", StringUtils.HtmlEncode(tShow.TVShow.MPAA))
        strRow = strRow.Replace("<$ORIGINALTITLE>", StringUtils.HtmlEncode(tShow.TVShow.OriginalTitle))
        strRow = strRow.Replace("<$PLOT>", StringUtils.HtmlEncode(tShow.TVShow.Plot))
        strRow = strRow.Replace("<$PREMIERED>", StringUtils.HtmlEncode(tShow.TVShow.Premiered))
        strRow = strRow.Replace("<$RATING>", StringUtils.HtmlEncode(If(tShow.TVShow.RatingSpecified, Double.Parse(tShow.TVShow.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture), String.Empty)))
        strRow = strRow.Replace("<$RUNTIME>", StringUtils.HtmlEncode(tShow.TVShow.Runtime))
        strRow = strRow.Replace("<$STATUS>", StringUtils.HtmlEncode(tShow.TVShow.Status))
        strRow = strRow.Replace("<$STUDIOS>", StringUtils.HtmlEncode(String.Join(" / ", tShow.TVShow.Studios.ToArray)))
        strRow = strRow.Replace("<$TAGS>", If(tShow.TVShow.TagsSpecified, StringUtils.HtmlEncode((String.Join(" / ", tShow.TVShow.Tags.ToArray))), String.Empty))
        strRow = strRow.Replace("<$TITLE>", StringUtils.HtmlEncode(tShow.TVShow.Title))
        strRow = strRow.Replace("<$TMDBID>", StringUtils.HtmlEncode(tShow.TVShow.UniqueIDs.TMDbId.ToString))
        strRow = strRow.Replace("<$TVDBID>", tShow.TVShow.UniqueIDs.TVDbId.ToString)
        strRow = strRow.Replace("<$VOTES>", StringUtils.HtmlEncode(If(tShow.TVShow.VotesSpecified, Integer.Parse(tShow.TVShow.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture), String.Empty)))

        Return strRow
    End Function

    Private Function SaveAll(ByVal TemplateSource As String, ByVal HTMLBody As StringBuilder) As String
        Try
            CopyDirectory(TemplateSource, _strBuildPath, True)

            If _tExportSettings.Flags Then
                Directory.CreateDirectory(Path.Combine(_strBuildPath, "exportdata", "flags"))
                Dim FlagSource As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Flags", Path.DirectorySeparatorChar)
                CopyDirectory(FlagSource, Path.Combine(_strBuildPath, "exportdata", "flags"), True)
            End If

            Dim strIndexFile As String = Path.Combine(_strBuildPath, If(Not String.IsNullOrEmpty(_tExportSettings.Filename), _tExportSettings.Filename, "index.html"))
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
            logger.Error(ex, New StackFrame().GetMethod().Name)
            Return String.Empty
        End Try
    End Function

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments
        Dim destPath As String
        Dim resizePoster As Integer
        Dim srcPath As String
    End Structure

    Private Structure ContentPart
        Dim Content As String
        Dim ContentType As Enums.ContentType
        Dim innerContentPartMap As Object
        Dim isLooped As Boolean
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

        Public Property vidFileSize() As String = String.Empty

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

        <XmlElement("mainkeyarts")>
        Public Property MainKeyarts() As Boolean = False

        <XmlElement("mainkeyarts_maxheight")>
        Public Property MainKeyarts_MaxHeight() As Integer = -1

        <XmlElement("mainkeyarts_maxwidth")>
        Public Property MainKeyarts_MaxWidth() As Integer = -1

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

#End Region 'Nested Types

End Class