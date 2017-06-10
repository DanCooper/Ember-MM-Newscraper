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
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports NLog

Public Class Scanner

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Public MoviePaths As New List(Of String)
    Public SourceLastScan As New DateTime
    Public TVEpisodePaths As New List(Of String)
    Public TVShowPaths As New Hashtable

    Friend WithEvents bwPrelim As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Events"

    Public Event ProgressUpdate(ByVal eProgressValue As ProgressValue)

#End Region 'Events

#Region "Methods"

    Public Sub Cancel()
        If bwPrelim.IsBusy Then bwPrelim.CancelAsync()
    End Sub

    Public Sub CancelAndWait()
        If bwPrelim.IsBusy Then bwPrelim.CancelAsync()
        While bwPrelim.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub
    ''' <summary>
    ''' Check if a directory contains supporting files (nfo, poster, fanart, etc)
    ''' </summary>
    ''' <param name="tDBElement">Database.DBElement object</param>
    ''' <param name="bForced">Enable ALL known file naming schemas. Should only be used to search files and not to save files!</param>
    Public Sub GetFolderContents_Movie(ByRef tDBElement As Database.DBElement, Optional ByVal bForced As Boolean = False)
        If String.IsNullOrEmpty(tDBElement.Filename) Then Return

        Dim currname As String = String.Empty
        Dim atList As New List(Of String)   'actor thumbs list
        Dim efList As New List(Of String)   'extrafanart list
        Dim etList As New List(Of String)   'extrathumbs list
        Dim sList As New List(Of String)    'external subtitles files list
        Dim tList As New List(Of String)    'theme files list
        Dim fList As New List(Of String)    'all other files list
        Dim fName As String = String.Empty

        Dim parPath As String = String.Empty

        Dim fileName As String = Path.GetFileNameWithoutExtension(tDBElement.Filename)
        Dim fileNameStack As String = Path.GetFileNameWithoutExtension(FileUtils.Common.RemoveStackingMarkers(tDBElement.Filename))
        Dim filePath As String = Path.Combine(Directory.GetParent(tDBElement.Filename).FullName, fileName)
        Dim filePathStack As String = Path.Combine(Directory.GetParent(tDBElement.Filename).FullName, fileNameStack)
        Dim fileParPath As String = Directory.GetParent(filePath).FullName

        'remove all known paths
        tDBElement.ActorThumbs.Clear()
        tDBElement.ExtrafanartsPath = String.Empty
        tDBElement.ExtrathumbsPath = String.Empty
        tDBElement.ImagesContainer = New MediaContainers.ImagesContainer
        tDBElement.NfoPath = String.Empty
        tDBElement.Subtitles = New List(Of MediaContainers.Subtitle)
        tDBElement.Theme = New MediaContainers.Theme
        tDBElement.Trailer = New MediaContainers.Trailer

        'first add files to filelists
        If FileUtils.Common.isVideoTS(tDBElement.Filename) Then
            parPath = FileUtils.Common.GetMainPath(tDBElement.Filename).FullName

            Try
                fList.AddRange(Directory.GetFiles(Directory.GetParent(tDBElement.Filename).FullName))
                fList.AddRange(Directory.GetFiles(parPath))
                If Master.eSettings.MovieUseNMJ Then
                    fList.AddRange(Directory.GetFiles(Directory.GetParent(parPath).FullName))
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        ElseIf FileUtils.Common.isBDRip(tDBElement.Filename) Then
            parPath = FileUtils.Common.GetMainPath(tDBElement.Filename).FullName

            Try
                fList.AddRange(Directory.GetFiles(Directory.GetParent(Directory.GetParent(tDBElement.Filename).FullName).FullName))
                fList.AddRange(Directory.GetFiles(parPath))
                If Master.eSettings.MovieUseNMJ Then
                    fList.AddRange(Directory.GetFiles(Directory.GetParent(parPath).FullName))
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Else
            parPath = FileUtils.Common.GetMainPath(tDBElement.Filename).FullName

            If tDBElement.IsSingle Then
                fList.AddRange(Directory.GetFiles(parPath))
            Else
                Try
                    Dim sName As String = Path.GetFileNameWithoutExtension(FileUtils.Common.RemoveStackingMarkers(tDBElement.Filename))
                    fList.AddRange(Directory.GetFiles(parPath, String.Concat(sName, "*")))
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            End If
        End If

        'secondly add files from special folders to filelists
        If tDBElement.IsSingle Then
            For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainActorThumbs, bForced)
                Dim parDir As String = Directory.GetParent(a.Replace("<placeholder>", "placeholder")).FullName
                If Directory.Exists(parDir) Then
                    atList.AddRange(Directory.GetFiles(parDir))
                End If
            Next
            For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainExtrafanarts, bForced)
                If Directory.Exists(a) Then
                    efList.AddRange(Directory.GetFiles(a, "*.jpg"))
                    If efList.Count > 0 Then Exit For 'scan only one path to prevent image dublicates
                End If
            Next
            For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainExtrathumbs, bForced)
                If Directory.Exists(a) Then
                    etList.AddRange(Directory.GetFiles(a, "*.jpg"))
                    If etList.Count > 0 Then Exit For 'scan only one path to prevent image dublicates
                End If
            Next
            For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainSubtitle, bForced)
                If Directory.Exists(a) Then
                    sList.AddRange(Directory.GetFiles(a))
                End If
            Next
            For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainTheme, bForced)
                If Directory.Exists(Directory.GetParent(a).FullName) Then
                    tList.AddRange(Directory.GetFiles(Directory.GetParent(a).FullName))
                End If
            Next
        End If

        'actor thumbs
        If atList.Count > 0 Then
            tDBElement.ActorThumbs.AddRange(atList)
        End If

        'banner
        For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainBanner, bForced)
            tDBElement.ImagesContainer.Banner.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.Banner.LocalFilePathSpecified Then Exit For
        Next

        'clearart
        For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainClearArt, bForced)
            tDBElement.ImagesContainer.ClearArt.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.ClearArt.LocalFilePathSpecified Then Exit For
        Next

        'clearlogo
        For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainClearLogo, bForced)
            tDBElement.ImagesContainer.ClearLogo.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.ClearLogo.LocalFilePathSpecified Then Exit For
        Next

        'discart
        For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainDiscArt, bForced)
            tDBElement.ImagesContainer.DiscArt.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.DiscArt.LocalFilePathSpecified Then Exit For
        Next

        'extrafanarts
        For Each ePath In efList
            tDBElement.ImagesContainer.Extrafanarts.Add(New MediaContainers.Image With {.LocalFilePath = ePath})
        Next
        If tDBElement.ImagesContainer.Extrafanarts.Count > 0 Then
            tDBElement.ExtrafanartsPath = Directory.GetParent(efList.Item(0)).FullName
        End If

        'extrathumbs
        For Each ePath In etList
            tDBElement.ImagesContainer.Extrathumbs.Add(New MediaContainers.Image With {.LocalFilePath = ePath})
        Next
        If tDBElement.ImagesContainer.Extrathumbs.Count > 0 Then
            tDBElement.ExtrathumbsPath = Directory.GetParent(etList.Item(0)).FullName
        End If

        'fanart
        For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainFanart, bForced)
            tDBElement.ImagesContainer.Fanart.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.Fanart.LocalFilePathSpecified Then Exit For
        Next

        'landscape
        For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainLandscape, bForced)
            tDBElement.ImagesContainer.Landscape.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.Landscape.LocalFilePathSpecified Then Exit For
        Next

        'nfo
        For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainNFO, bForced)
            tDBElement.NfoPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.NfoPathSpecified Then Exit For
        Next

        'poster
        For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainPoster, bForced)
            tDBElement.ImagesContainer.Poster.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.Poster.LocalFilePathSpecified Then Exit For
        Next

        'subtitles (external)
        For Each fFile As String In sList
            For Each ext In Master.eSettings.FileSystemValidSubtitlesExts
                If fFile.ToLower.EndsWith(ext) Then
                    Dim isForced As Boolean = Path.GetFileNameWithoutExtension(fFile).ToLower.EndsWith("forced")
                    tDBElement.Subtitles.Add(New MediaContainers.Subtitle With {.SubsPath = fFile, .SubsType = "External", .SubsForced = isForced})
                End If
            Next
        Next

        'theme
        If String.IsNullOrEmpty(tDBElement.Theme.LocalFilePath) Then
            For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainTheme, bForced)
                For Each t As String In Master.eSettings.FileSystemValidThemeExts
                    tDBElement.Theme.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = String.Concat(a.ToLower, t.ToLower))
                    If tDBElement.Theme.LocalFilePathSpecified Then Exit For
                Next
                If tDBElement.Theme.LocalFilePathSpecified Then Exit For
            Next
        End If

        'trailer
        If String.IsNullOrEmpty(tDBElement.Trailer.LocalFilePath) Then
            For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainTrailer, bForced)
                For Each t As String In Master.eSettings.FileSystemValidExts
                    tDBElement.Trailer.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = String.Concat(a.ToLower, t.ToLower))
                    If tDBElement.Trailer.LocalFilePathSpecified Then Exit For
                Next
                If tDBElement.Trailer.LocalFilePathSpecified Then Exit For
            Next
        End If
    End Sub

    ''' <summary>
    ''' Check if a directory contains supporting files (nfo, poster, fanart, etc)
    ''' </summary>
    ''' <param name="tDBElement">MovieSetContainer object.</param>
    Public Sub GetFolderContents_MovieSet(ByRef tDBElement As Database.DBElement)
        'remove all known paths
        tDBElement.ImagesContainer = New MediaContainers.ImagesContainer
        tDBElement.NfoPath = String.Empty

        'banner
        For Each a In FileUtils.GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainBanner)
            If File.Exists(a) Then
                tDBElement.ImagesContainer.Banner.LocalFilePath = a
                Exit For
            End If
        Next

        'clearart
        For Each a In FileUtils.GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainClearArt)
            If File.Exists(a) Then
                tDBElement.ImagesContainer.ClearArt.LocalFilePath = a
                Exit For
            End If
        Next

        'clearlogo
        For Each a In FileUtils.GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainClearLogo)
            If File.Exists(a) Then
                tDBElement.ImagesContainer.ClearLogo.LocalFilePath = a
                Exit For
            End If
        Next

        'discart
        For Each a In FileUtils.GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainDiscArt)
            If File.Exists(a) Then
                tDBElement.ImagesContainer.DiscArt.LocalFilePath = a
                Exit For
            End If
        Next

        'fanart
        For Each a In FileUtils.GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainFanart)
            If File.Exists(a) Then
                tDBElement.ImagesContainer.Fanart.LocalFilePath = a
                Exit For
            End If
        Next

        'landscape
        For Each a In FileUtils.GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainLandscape)
            If File.Exists(a) Then
                tDBElement.ImagesContainer.Landscape.LocalFilePath = a
                Exit For
            End If
        Next

        'nfo
        For Each a In FileUtils.GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainNFO)
            If File.Exists(a) Then
                tDBElement.NfoPath = a
                Exit For
            End If
        Next

        'poster
        For Each a In FileUtils.GetFilenameList.MovieSet(tDBElement, Enums.ModifierType.MainPoster)
            If File.Exists(a) Then
                tDBElement.ImagesContainer.Poster.LocalFilePath = a
                Exit For
            End If
        Next
    End Sub

    Public Sub GetFolderContents_TVEpisode(ByRef tDBElement As Database.DBElement)
        If String.IsNullOrEmpty(tDBElement.Filename) Then Return

        Dim fName As String = String.Empty
        Dim fList As New List(Of String)

        'remove all known paths
        tDBElement.ActorThumbs.Clear()
        tDBElement.ImagesContainer = New MediaContainers.ImagesContainer
        tDBElement.NfoPath = String.Empty
        tDBElement.Subtitles = New List(Of MediaContainers.Subtitle)

        Try
            fList.AddRange(Directory.GetFiles(Directory.GetParent(tDBElement.Filename).FullName, String.Concat(Path.GetFileNameWithoutExtension(tDBElement.Filename), "*.*")))
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        'episode actor thumbs
        For Each a In FileUtils.GetFilenameList.TVEpisode(tDBElement, Enums.ModifierType.EpisodeActorThumbs)
            Dim parDir As String = Directory.GetParent(a.Replace("<placeholder>", "placeholder")).FullName
            If Directory.Exists(parDir) Then
                tDBElement.ActorThumbs.AddRange(Directory.GetFiles(parDir))
            End If
        Next

        'episode fanart
        For Each a In FileUtils.GetFilenameList.TVEpisode(tDBElement, Enums.ModifierType.EpisodeFanart)
            tDBElement.ImagesContainer.Fanart.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.Fanart.LocalFilePathSpecified Then Exit For
        Next

        'episode poster
        For Each a In FileUtils.GetFilenameList.TVEpisode(tDBElement, Enums.ModifierType.EpisodePoster)
            tDBElement.ImagesContainer.Poster.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.Poster.LocalFilePathSpecified Then Exit For
        Next

        'episode NFO
        For Each a In FileUtils.GetFilenameList.TVEpisode(tDBElement, Enums.ModifierType.EpisodeNFO)
            tDBElement.NfoPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.NfoPathSpecified Then Exit For
        Next

        'subtitles (external)
        For Each fFile As String In fList
            For Each ext In Master.eSettings.FileSystemValidSubtitlesExts
                Dim FullFilePathWithoutExt As String = Path.Combine(Directory.GetParent(tDBElement.Filename).FullName, Path.GetFileNameWithoutExtension(tDBElement.Filename)).ToLower
                If fFile.ToLower.StartsWith(FullFilePathWithoutExt) AndAlso fFile.ToLower.EndsWith(ext) Then
                    Dim isForced As Boolean = Path.GetFileNameWithoutExtension(fFile).ToLower.EndsWith("forced")
                    tDBElement.Subtitles.Add(New MediaContainers.Subtitle With {.SubsPath = fFile, .SubsType = "External", .SubsForced = isForced})
                End If
            Next
        Next
    End Sub

    Public Sub GetFolderContents_TVSeason(ByRef tDBElement As Database.DBElement)
        Dim iSeason As Integer = tDBElement.TVSeason.Season
        Dim strSeasonPath As String = String.Empty
        Dim strShowPath As String = tDBElement.ShowPath
        Dim bInside As Boolean = False
        Dim fList As New List(Of String)

        'remove all known paths
        tDBElement.ImagesContainer = New MediaContainers.ImagesContainer

        'check if there is a season directory
        strSeasonPath = Functions.GetSeasonDirectoryFromShowPath(strShowPath, iSeason)
        If Not String.IsNullOrEmpty(strSeasonPath) Then 'If Functions.IsSeasonDirectory(Directory.GetParent(TVDB.Filename).FullName) Then
            bInside = True
        End If

        Try
            If bInside Then
                fList.AddRange(Directory.GetFiles(strSeasonPath))
                fList.AddRange(Directory.GetFiles(strShowPath))
            Else
                fList.AddRange(Directory.GetFiles(strShowPath))
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        'season banner
        If iSeason = 999 Then 'all-seasons
            For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.AllSeasonsBanner)
                tDBElement.ImagesContainer.Banner.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                If tDBElement.ImagesContainer.Banner.LocalFilePathSpecified Then Exit For
            Next
        Else
            For Each a In FileUtils.GetFilenameList.TVSeason(tDBElement, Enums.ModifierType.SeasonBanner)
                tDBElement.ImagesContainer.Banner.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                If tDBElement.ImagesContainer.Banner.LocalFilePathSpecified Then Exit For
            Next
        End If

        'season fanart 
        If iSeason = 999 Then 'all-seasons
            For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.AllSeasonsFanart)
                tDBElement.ImagesContainer.Fanart.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                If tDBElement.ImagesContainer.Fanart.LocalFilePathSpecified Then Exit For
            Next
        Else
            For Each a In FileUtils.GetFilenameList.TVSeason(tDBElement, Enums.ModifierType.SeasonFanart)
                tDBElement.ImagesContainer.Fanart.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                If tDBElement.ImagesContainer.Fanart.LocalFilePathSpecified Then Exit For
            Next
        End If

        'season landscape
        If iSeason = 999 Then 'all-seasons
            For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.AllSeasonsLandscape)
                tDBElement.ImagesContainer.Landscape.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                If tDBElement.ImagesContainer.Landscape.LocalFilePathSpecified Then Exit For
            Next
        Else
            For Each a In FileUtils.GetFilenameList.TVSeason(tDBElement, Enums.ModifierType.SeasonLandscape)
                tDBElement.ImagesContainer.Landscape.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                If tDBElement.ImagesContainer.Landscape.LocalFilePathSpecified Then Exit For
            Next
        End If

        'season poster
        If iSeason = 999 Then 'all-seasons
            For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.AllSeasonsPoster)
                tDBElement.ImagesContainer.Poster.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                If tDBElement.ImagesContainer.Poster.LocalFilePathSpecified Then Exit For
            Next
        Else
            For Each a In FileUtils.GetFilenameList.TVSeason(tDBElement, Enums.ModifierType.SeasonPoster)
                tDBElement.ImagesContainer.Poster.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                If tDBElement.ImagesContainer.Poster.LocalFilePathSpecified Then Exit For
            Next
        End If
    End Sub
    ''' <summary>
    ''' Check if a directory contains supporting files (nfo, poster, fanart)
    ''' </summary>
    ''' <param name="tDBElement">TVShowContainer object.</param>
    Public Sub GetFolderContents_TVShow(ByRef tDBElement As Database.DBElement)
        Dim ShowPath As String = tDBElement.ShowPath
        Dim efList As New List(Of String)
        Dim fList As New List(Of String)

        'remove all known paths
        tDBElement.ActorThumbs.Clear()
        tDBElement.ExtrafanartsPath = String.Empty
        tDBElement.ImagesContainer = New MediaContainers.ImagesContainer
        tDBElement.NfoPath = String.Empty
        tDBElement.Theme = New MediaContainers.Theme

        Try
            fList.AddRange(Directory.GetFiles(tDBElement.ShowPath))

            For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.MainExtrafanarts)
                If Directory.Exists(a) Then
                    efList.AddRange(Directory.GetFiles(a, "*.jpg"))
                    If efList.Count > 0 Then Exit For 'scan only one path to prevent image dublicates
                End If
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        'show actor thumbs
        For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.MainActorThumbs)
            Dim parDir As String = Directory.GetParent(a.Replace("<placeholder>", "placeholder")).FullName
            If Directory.Exists(parDir) Then
                tDBElement.ActorThumbs.AddRange(Directory.GetFiles(parDir))
            End If
        Next

        'show banner
        For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.MainBanner)
            tDBElement.ImagesContainer.Banner.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.Banner.LocalFilePathSpecified Then Exit For
        Next

        'show characterart
        For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.MainCharacterArt)
            tDBElement.ImagesContainer.CharacterArt.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.CharacterArt.LocalFilePathSpecified Then Exit For
        Next

        'show clearart
        For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.MainClearArt)
            tDBElement.ImagesContainer.ClearArt.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.ClearArt.LocalFilePathSpecified Then Exit For
        Next

        'show clearlogo
        For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.MainClearLogo)
            tDBElement.ImagesContainer.ClearLogo.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.ClearLogo.LocalFilePathSpecified Then Exit For
        Next

        'show extrafanarts
        For Each ePath In efList
            tDBElement.ImagesContainer.Extrafanarts.Add(New MediaContainers.Image With {.LocalFilePath = ePath})
        Next
        If tDBElement.ImagesContainer.Extrafanarts.Count > 0 Then
            tDBElement.ExtrafanartsPath = Directory.GetParent(efList.Item(0)).FullName
        End If

        'show fanart
        For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.MainFanart)
            tDBElement.ImagesContainer.Fanart.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.Fanart.LocalFilePathSpecified Then Exit For
        Next

        'show landscape
        For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.MainLandscape)
            tDBElement.ImagesContainer.Landscape.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.Landscape.LocalFilePathSpecified Then Exit For
        Next

        'show NFO
        For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.MainNFO)
            tDBElement.NfoPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.NfoPathSpecified Then Exit For
        Next

        'show poster
        For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.MainPoster)
            tDBElement.ImagesContainer.Poster.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
            If tDBElement.ImagesContainer.Poster.LocalFilePathSpecified Then Exit For
        Next

        'show theme
        For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.MainTheme)
            For Each t As String In Master.eSettings.FileSystemValidThemeExts
                tDBElement.Theme.LocalFilePath = fList.FirstOrDefault(Function(s) s.ToLower = String.Concat(a.ToLower, t.ToLower))
                If tDBElement.Theme.LocalFilePathSpecified Then Exit For
            Next
            If tDBElement.Theme.LocalFilePathSpecified Then Exit For
        Next
    End Sub

    Public Function IsBusy() As Boolean
        Return bwPrelim.IsBusy
    End Function

    ''' <summary>
    ''' Check if we should scan the directory.
    ''' </summary>
    ''' <param name="dInfo">Full path of the directory to check</param>
    ''' <returns>True if directory is valid, false if not.</returns>
    Public Function IsValidDir(ByVal dInfo As DirectoryInfo, ByVal bIsTV As Boolean) As Boolean
        Try
            For Each s As String In Master.ExcludeDirs
                If dInfo.FullName.ToLower = s.ToLower Then
                    logger.Info(String.Format("[Sanner] [IsValidDir] [ExcludeDirs] Path ""{0}"" has been skipped (path is in ""exclude directory"" list)", dInfo.FullName, s))
                    Return False
                End If
            Next
            If (Not bIsTV AndAlso dInfo.Name.ToLower = "extras") OrElse
            If(dInfo.FullName.IndexOf("\") >= 0, dInfo.FullName.Remove(0, dInfo.FullName.IndexOf("\")).Contains(":"), False) Then
                Return False
            End If
            For Each s As String In AdvancedSettings.GetSetting("NotValidDirIs", ".actors|extrafanarts|extrathumbs|video_ts|bdmv|audio_ts|recycler|subs|subtitles|.trashes").Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)
                If dInfo.Name.ToLower = s.ToLower Then
                    logger.Info(String.Format("[Sanner] [IsValidDir] [NotValidDirIs] Path ""{0}"" has been skipped (path name is ""{1}"")", dInfo.FullName, s))
                    Return False
                End If
            Next
            For Each s As String In AdvancedSettings.GetSetting("NotValidDirContains", "-trailer|[trailer|temporary files|(noscan)|$recycle.bin|lost+found|system volume information|sample").Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)
                If dInfo.Name.ToLower.Contains(s.ToLower) Then
                    logger.Info(String.Format("[Sanner] [IsValidDir] [NotValidDirContains] Path ""{0}"" has been skipped (path contains ""{1}"")", dInfo.FullName, s))
                    Return False
                End If
            Next

        Catch ex As Exception
            logger.Error(String.Format("[Sanner] [IsValidDir] Path ""{0}"" has been skipped ({1})", dInfo.Name, ex.Message))
            Return False
        End Try
        Return True 'This is the Else
    End Function

    Public Sub Load_Movie(ByRef DBMovie As Database.DBElement, ByVal Batchmode As Boolean)
        Dim ToNfo As Boolean = False

        GetFolderContents_Movie(DBMovie)

        If DBMovie.NfoPathSpecified Then
            DBMovie.Movie = NFO.LoadFromNFO_Movie(DBMovie.NfoPath, DBMovie.IsSingle)
            If Not DBMovie.Movie.FileInfoSpecified AndAlso DBMovie.Movie.TitleSpecified AndAlso Master.eSettings.MovieScraperMetaDataScan Then
                MediaInfo.UpdateMediaInfo(DBMovie)
            End If
        Else
            DBMovie.Movie = NFO.LoadFromNFO_Movie(DBMovie.Filename, DBMovie.IsSingle)
            If Not DBMovie.Movie.FileInfoSpecified AndAlso DBMovie.Movie.TitleSpecified AndAlso Master.eSettings.MovieScraperMetaDataScan Then
                MediaInfo.UpdateMediaInfo(DBMovie)
            End If
        End If

        'Year
        If Not DBMovie.Movie.YearSpecified AndAlso DBMovie.Source.GetYear Then
            DBMovie.Movie.Year = StringUtils.FilterYearFromPath_Movie(DBMovie.Filename, DBMovie.IsSingle, DBMovie.Source.UseFolderName)
        End If

        'IMDB ID
        If Not DBMovie.Movie.IMDBSpecified Then
            DBMovie.Movie.IMDB = StringUtils.FilterIMDBIDFromPath(DBMovie.Filename, True)
        End If

        'Title
        If Not DBMovie.Movie.TitleSpecified Then
            'no title so assume it's an invalid nfo, clear nfo path if exists
            DBMovie.NfoPath = String.Empty
            DBMovie.Movie.Title = StringUtils.FilterTitleFromPath_Movie(DBMovie.Filename, DBMovie.IsSingle, DBMovie.Source.UseFolderName)
        End If

        'ListTitle
        Dim tTitle As String = StringUtils.SortTokens_Movie(DBMovie.Movie.Title)
        If Master.eSettings.MovieDisplayYear AndAlso Not String.IsNullOrEmpty(DBMovie.Movie.Year) Then
            DBMovie.ListTitle = String.Format("{0} ({1})", tTitle, DBMovie.Movie.Year)
        Else
            DBMovie.ListTitle = tTitle
        End If

        If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
            For Each a In FileUtils.GetFilenameList.Movie(DBMovie, Enums.ModifierType.MainWatchedFile)
                If DBMovie.Movie.PlayCountSpecified Then
                    If Not File.Exists(a) Then
                        Dim fs As FileStream = File.Create(a)
                        fs.Close()
                    End If
                Else
                    If File.Exists(a) Then
                        DBMovie.Movie.PlayCount = 1
                        ToNfo = True
                    End If
                End If
            Next
        End If

        If DBMovie.ListTitleSpecified Then
            'search local actor thumb for each actor in NFO
            If DBMovie.Movie.ActorsSpecified AndAlso DBMovie.ActorThumbsSpecified Then
                For Each actor In DBMovie.Movie.Actors
                    actor.LocalFilePath = DBMovie.ActorThumbs.FirstOrDefault(Function(s) Path.GetFileNameWithoutExtension(s).ToLower = actor.Name.Replace(" ", "_").ToLower)
                Next
            End If

            'Language
            If DBMovie.Movie.LanguageSpecified Then
                DBMovie.Language = DBMovie.Movie.Language
            Else
                DBMovie.Language = DBMovie.Source.Language
                DBMovie.Movie.Language = DBMovie.Source.Language
            End If

            'Lock state
            If DBMovie.Movie.Locked Then
                DBMovie.IsLock = DBMovie.Movie.Locked
            End If

            'VideoSource
            Dim vSource As String = APIXML.GetVideoSource(DBMovie.Filename, False)
            DBMovie.VideoSource = String.Empty
            If Not String.IsNullOrEmpty(vSource) Then
                DBMovie.VideoSource = vSource
                DBMovie.Movie.VideoSource = vSource
            ElseIf Not DBMovie.VideoSourceSpecified AndAlso AdvancedSettings.GetBooleanSetting("VideoSourceByExtension", False, "*EmberAPP") Then
                vSource = AdvancedSettings.GetSetting(String.Concat("VideoSourceByExtension:", Path.GetExtension(DBMovie.Filename)), String.Empty, "*EmberAPP")
                If Not String.IsNullOrEmpty(vSource) Then
                    DBMovie.VideoSource = vSource
                    DBMovie.Movie.VideoSource = vSource
                End If
            End If
            If Not DBMovie.VideoSourceSpecified AndAlso DBMovie.Movie.VideoSourceSpecified Then
                DBMovie.VideoSource = DBMovie.Movie.VideoSource
            End If

            'Do the Save
            If ToNfo AndAlso DBMovie.NfoPathSpecified Then
                DBMovie = Master.DB.Save_Movie(DBMovie, Batchmode, True, False, True, False)
            Else
                DBMovie = Master.DB.Save_Movie(DBMovie, Batchmode, False, False, True, False)
            End If
        End If
    End Sub

    Public Sub Load_MovieSet(ByRef DBMovieSet As Database.DBElement, ByVal Batchmode As Boolean)
        Dim OldTitle As String = DBMovieSet.MovieSet.Title

        GetFolderContents_MovieSet(DBMovieSet)

        If Not DBMovieSet.NfoPathSpecified Then
            Dim sNFO As String = NFO.GetNfoPath_MovieSet(DBMovieSet)
            If Not String.IsNullOrEmpty(sNFO) Then
                DBMovieSet.NfoPath = sNFO
                DBMovieSet.MovieSet = NFO.LoadFromNFO_MovieSet(sNFO)
            End If
        Else
            DBMovieSet.MovieSet = NFO.LoadFromNFO_MovieSet(DBMovieSet.NfoPath)
        End If

        'ListTitle
        Dim tTitle As String = StringUtils.SortTokens_MovieSet(DBMovieSet.MovieSet.Title)
        If Not String.IsNullOrEmpty(tTitle) Then
            DBMovieSet.ListTitle = tTitle
        Else
            DBMovieSet.ListTitle = OldTitle
        End If

        'Language
        If DBMovieSet.MovieSet.LanguageSpecified Then
            DBMovieSet.Language = DBMovieSet.MovieSet.Language
        End If

        'Lock state
        If DBMovieSet.MovieSet.Locked Then
            DBMovieSet.IsLock = DBMovieSet.MovieSet.Locked
        End If

        DBMovieSet = Master.DB.Save_MovieSet(DBMovieSet, Batchmode, False, False, True)
    End Sub

    Public Function Load_TVEpisode(ByVal DBTVEpisode As Database.DBElement, ByVal isNew As Boolean, ByVal Batchmode As Boolean, ReportProgress As Boolean) As SeasonAndEpisodeItems
        Dim SeasonAndEpisodeList As New SeasonAndEpisodeItems
        Dim EpisodesToRemoveList As New List(Of EpisodeItem)

        'first we have to create a list of all already existing episode information for this file path
        If Not isNew Then
            Dim EpisodesByFilenameList As List(Of Database.DBElement) = Master.DB.Load_AllTVEpisodes_ByFileID(DBTVEpisode.FilenameID, False)
            For Each eEpisode As Database.DBElement In EpisodesByFilenameList
                EpisodesToRemoveList.Add(New EpisodeItem With {.Episode = eEpisode.TVEpisode.Episode, .idEpisode = eEpisode.ID, .Season = eEpisode.TVEpisode.Season})
            Next
        End If

        GetFolderContents_TVEpisode(DBTVEpisode)

        For Each sEpisode As EpisodeItem In RegexGetTVEpisode(DBTVEpisode.Filename, DBTVEpisode.ShowID)
            Dim ToNfo As Boolean = False

            'It's a clone needed to prevent overwriting information of MultiEpisodes
            Dim cEpisode As Database.DBElement = CType(DBTVEpisode.CloneDeep, Database.DBElement)

            If cEpisode.NfoPathSpecified Then
                If sEpisode.byDate Then
                    cEpisode.TVEpisode = NFO.LoadFromNFO_TVEpisode(cEpisode.NfoPath, sEpisode.Season, sEpisode.Aired)
                Else
                    cEpisode.TVEpisode = NFO.LoadFromNFO_TVEpisode(cEpisode.NfoPath, sEpisode.Season, sEpisode.Episode)
                End If

                If Not cEpisode.TVEpisode.FileInfoSpecified AndAlso cEpisode.TVEpisode.TitleSpecified AndAlso Master.eSettings.TVScraperMetaDataScan Then
                    MediaInfo.UpdateTVMediaInfo(cEpisode)
                End If
            Else
                If isNew AndAlso cEpisode.TVShow.AnyUniqueIDSpecified AndAlso cEpisode.ShowIDSpecified Then
                    If sEpisode.byDate Then
                        If Not cEpisode.TVEpisode.AiredSpecified Then cEpisode.TVEpisode.Aired = sEpisode.Aired
                    Else
                        If cEpisode.TVEpisode.Season = -1 Then cEpisode.TVEpisode.Season = sEpisode.Season
                        If cEpisode.TVEpisode.Episode = -1 Then cEpisode.TVEpisode.Episode = sEpisode.Episode
                    End If

                    'Scrape episode data
                    If Not ModulesManager.Instance.ScrapeData_TVEpisode(cEpisode, Master.DefaultOptions_TV, False) Then
                        If cEpisode.TVEpisode.TitleSpecified Then
                            ToNfo = True

                            'if we had info for it (based on title) and mediainfo scanning is enabled
                            If Master.eSettings.TVScraperMetaDataScan Then
                                MediaInfo.UpdateTVMediaInfo(cEpisode)
                            End If
                        End If
                    End If
                Else
                    cEpisode.TVEpisode = New MediaContainers.EpisodeDetails
                End If
            End If

            'Scrape episode images
            If isNew AndAlso cEpisode.TVShow.AnyUniqueIDSpecified AndAlso cEpisode.ShowIDSpecified Then
                Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
                Dim ScrapeModifiers As New Structures.ScrapeModifiers
                If Not cEpisode.ImagesContainer.Fanart.LocalFilePathSpecified AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled Then ScrapeModifiers.EpisodeFanart = True
                If Not cEpisode.ImagesContainer.Poster.LocalFilePathSpecified AndAlso Master.eSettings.TVEpisodePosterAnyEnabled Then ScrapeModifiers.EpisodePoster = True
                If ScrapeModifiers.EpisodeFanart OrElse ScrapeModifiers.EpisodePoster Then
                    If Not ModulesManager.Instance.ScrapeImage_TV(cEpisode, SearchResultsContainer, ScrapeModifiers, False) Then
                        Images.SetPreferredImages(cEpisode, SearchResultsContainer, ScrapeModifiers)
                    End If
                End If
            End If

            If Not cEpisode.TVEpisode.TitleSpecified Then
                'no title so assume it's an invalid nfo, clear nfo path if exists
                cEpisode.NfoPath = String.Empty
                'set title based on episode file
                If Not Master.eSettings.TVEpisodeNoFilter Then cEpisode.TVEpisode.Title = StringUtils.FilterTitleFromPath_TVEpisode(cEpisode.Filename, cEpisode.TVShow.Title)
            End If

            'search local actor thumb for each actor in NFO
            If cEpisode.TVEpisode.ActorsSpecified AndAlso cEpisode.ActorThumbsSpecified Then
                For Each actor In cEpisode.TVEpisode.Actors
                    actor.LocalFilePath = cEpisode.ActorThumbs.FirstOrDefault(Function(s) Path.GetFileNameWithoutExtension(s).ToLower = actor.Name.Replace(" ", "_").ToLower)
                Next
            End If

            If sEpisode.byDate Then
                If cEpisode.TVEpisode.Season = -1 Then cEpisode.TVEpisode.Season = sEpisode.Season
                If cEpisode.TVEpisode.Episode = -1 AndAlso cEpisode.Ordering = Enums.EpisodeOrdering.DayOfYear Then
                    Dim eDate As Date = DateTime.ParseExact(sEpisode.Aired, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture)
                    cEpisode.TVEpisode.Episode = eDate.DayOfYear
                ElseIf cEpisode.TVEpisode.Episode = -1 Then
                    cEpisode.TVEpisode.Episode = sEpisode.Episode
                End If
                If Not cEpisode.TVEpisode.AiredSpecified Then cEpisode.TVEpisode.Aired = sEpisode.Aired

                If Not cEpisode.TVEpisode.TitleSpecified Then
                    'nothing usable in the title after filters have runs
                    cEpisode.TVEpisode.Title = String.Format("{0} {1}", cEpisode.TVShow.Title, cEpisode.TVEpisode.Aired)
                End If
            Else
                If cEpisode.TVEpisode.Season = -1 Then cEpisode.TVEpisode.Season = sEpisode.Season
                If cEpisode.TVEpisode.Episode = -1 Then cEpisode.TVEpisode.Episode = sEpisode.Episode
                If cEpisode.TVEpisode.SubEpisode = -1 AndAlso Not sEpisode.SubEpisode = -1 Then cEpisode.TVEpisode.SubEpisode = sEpisode.SubEpisode

                If Not cEpisode.TVEpisode.TitleSpecified Then
                    'nothing usable in the title after filters have runs
                    cEpisode.TVEpisode.Title = String.Format("{0} S{1}E{2}{3}", cEpisode.TVShow.Title, cEpisode.TVEpisode.Season.ToString.PadLeft(2, Convert.ToChar("0")),
                                                       cEpisode.TVEpisode.Episode.ToString.PadLeft(2, Convert.ToChar("0")),
                                                       If(cEpisode.TVEpisode.SubEpisodeSpecified, String.Concat(".", cEpisode.TVEpisode.SubEpisode), String.Empty))
                End If
            End If

            'Lock state
            If cEpisode.TVEpisode.Locked Then
                cEpisode.IsLock = cEpisode.TVEpisode.Locked
            End If

            'VideoSource
            Dim vSource As String = APIXML.GetVideoSource(cEpisode.Filename, True)
            cEpisode.VideoSource = String.Empty
            If Not String.IsNullOrEmpty(vSource) Then
                cEpisode.VideoSource = vSource
                cEpisode.TVEpisode.VideoSource = vSource
            ElseIf Not cEpisode.VideoSourceSpecified AndAlso AdvancedSettings.GetBooleanSetting("MediaSourcesByExtension", False, "*EmberAPP") Then
                vSource = AdvancedSettings.GetSetting(String.Concat("MediaSourcesByExtension:", Path.GetExtension(cEpisode.Filename)), String.Empty, "*EmberAPP")
                If Not String.IsNullOrEmpty(vSource) Then
                    cEpisode.VideoSource = vSource
                    cEpisode.TVEpisode.VideoSource = vSource
                End If
            End If
            If Not cEpisode.VideoSourceSpecified AndAlso cEpisode.TVEpisode.VideoSourceSpecified Then
                cEpisode.VideoSource = cEpisode.TVEpisode.VideoSource
            End If

            If Not isNew Then
                Dim EpisodeID As Long = -1

                Dim eEpisode = EpisodesToRemoveList.FirstOrDefault(Function(f) f.Episode = cEpisode.TVEpisode.Episode AndAlso f.Season = cEpisode.TVEpisode.Season)
                If eEpisode IsNot Nothing Then
                    'if an existing episode was found we use that idEpisode and remove the entry from the "existingEpisodeList" (remaining entries are deleted at the end)
                    EpisodeID = eEpisode.idEpisode
                    EpisodesToRemoveList.Remove(eEpisode)
                End If

                If Not EpisodeID = -1 Then
                    'old episode entry found, we re-use the idEpisode
                    cEpisode.ID = EpisodeID
                    Master.DB.Save_TVEpisode(cEpisode, Batchmode, ToNfo, ToNfo, False, True)
                Else
                    'no existing episode found or the season or episode number has changed => we have to add it as new episode
                    Master.DB.Save_TVEpisode(cEpisode, Batchmode, ToNfo, ToNfo, True, True)
                End If

                'add the season number to list
                SeasonAndEpisodeList.Seasons.Add(cEpisode.TVEpisode.Season)
            Else
                'Do the Save, no Season check (we add a new seasons whit tv show), no Sync (we sync with tv show)
                cEpisode = Master.DB.Save_TVEpisode(cEpisode, Batchmode, ToNfo, ToNfo, False, False)
                'add the season number and the new saved episode to list
                SeasonAndEpisodeList.Episodes.Add(cEpisode)
                SeasonAndEpisodeList.Seasons.Add(cEpisode.TVEpisode.Season)
            End If

            If ReportProgress Then bwPrelim.ReportProgress(-1, New ProgressValue With {.EventType = Enums.ScannerEventType.Added_TVEpisode, .ID = cEpisode.ID, .Message = String.Format("{0}: {1}", cEpisode.TVShow.Title, cEpisode.TVEpisode.Title)})
        Next

        If Not isNew Then
            For Each eEpisode As EpisodeItem In EpisodesToRemoveList
                Master.DB.Delete_TVEpisode(eEpisode.idEpisode, False, False, Batchmode)
            Next
        End If

        Return SeasonAndEpisodeList
    End Function

    Public Function Load_TVShow(ByRef DBTVShow As Database.DBElement, ByVal isNew As Boolean, ByVal Batchmode As Boolean, ByVal ReportProgress As Boolean) As Enums.ScannerEventType
        Dim newEpisodesList As New List(Of Database.DBElement)
        Dim newSeasonsIndex As New List(Of Integer)

        Dim Result As Enums.ScannerEventType = Enums.ScannerEventType.None

        If DBTVShow.EpisodesSpecified OrElse DBTVShow.IDSpecified Then
            If (Not TVShowPaths.ContainsKey(DBTVShow.ShowPath.ToLower) AndAlso isNew) OrElse (DBTVShow.IDSpecified AndAlso Not isNew) Then
                Result = If(isNew, Enums.ScannerEventType.Added_TVShow, Enums.ScannerEventType.Refresh_TVShow)

                GetFolderContents_TVShow(DBTVShow)

                If DBTVShow.NfoPathSpecified Then
                    DBTVShow.TVShow = NFO.LoadFromNFO_TVShow(DBTVShow.NfoPath)
                Else
                    DBTVShow.TVShow = New MediaContainers.TVShow
                End If

                'IMDB ID
                If Not DBTVShow.TVShow.IMDBSpecified Then
                    DBTVShow.TVShow.IMDB = StringUtils.FilterIMDBIDFromPath(DBTVShow.ShowPath, True)
                End If

                'Title
                If Not DBTVShow.TVShow.TitleSpecified Then
                    'no title so assume it's an invalid nfo, clear nfo path if exists
                    DBTVShow.NfoPath = String.Empty
                    DBTVShow.TVShow.Title = StringUtils.FilterTitleFromPath_TVShow(DBTVShow.ShowPath)
                End If

                'ListTitle
                Dim tTitle As String = StringUtils.SortTokens_TV(DBTVShow.TVShow.Title)
                If Master.eSettings.TVDisplayStatus AndAlso Not String.IsNullOrEmpty(DBTVShow.TVShow.Status) Then
                    DBTVShow.ListTitle = String.Format("{0} ({1})", tTitle, DBTVShow.TVShow.Status)
                Else
                    DBTVShow.ListTitle = tTitle
                End If

                If DBTVShow.ListTitleSpecified Then
                    'search local actor thumb for each actor in NFO
                    If DBTVShow.TVShow.ActorsSpecified AndAlso DBTVShow.ActorThumbsSpecified Then
                        For Each actor In DBTVShow.TVShow.Actors
                            actor.LocalFilePath = DBTVShow.ActorThumbs.FirstOrDefault(Function(s) Path.GetFileNameWithoutExtension(s).ToLower = actor.Name.Replace(" ", "_").ToLower)
                        Next
                    End If

                    'Language
                    If DBTVShow.TVShow.LanguageSpecified Then
                        DBTVShow.Language = DBTVShow.TVShow.Language
                    Else
                        DBTVShow.Language = DBTVShow.Source.Language
                        DBTVShow.TVShow.Language = DBTVShow.Source.Language
                    End If

                    'Lock state
                    If DBTVShow.TVShow.Locked Then
                        DBTVShow.IsLock = DBTVShow.TVShow.Locked
                    End If

                    Master.DB.Save_TVShow(DBTVShow, Batchmode, False, False, False)
                End If
            Else
                Result = Enums.ScannerEventType.Refresh_TVShow

                Dim newEpisodes As List(Of Database.DBElement) = DBTVShow.Episodes
                DBTVShow = Master.DB.Load_TVShow(Convert.ToInt64(TVShowPaths.Item(DBTVShow.ShowPath.ToLower)), True, False)
                DBTVShow.Episodes = newEpisodes
            End If

            If DBTVShow.ShowIDSpecified Then
                For Each DBTVEpisode As Database.DBElement In DBTVShow.Episodes
                    DBTVEpisode = Master.DB.AddTVShowInfoToDBElement(DBTVEpisode, DBTVShow)
                    If DBTVEpisode.FilenameSpecified Then
                        Dim SeasonAndEpisodesList As SeasonAndEpisodeItems = Load_TVEpisode(DBTVEpisode, isNew, Batchmode, ReportProgress)

                        'add new episodes
                        For Each iEpisode As Database.DBElement In SeasonAndEpisodesList.Episodes
                            newEpisodesList.Add(iEpisode)
                        Next

                        'add seasons
                        For Each iSeason As Integer In SeasonAndEpisodesList.Seasons
                            Dim tmpSeason As Database.DBElement = DBTVShow.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = iSeason)
                            If tmpSeason Is Nothing OrElse tmpSeason.TVSeason Is Nothing Then
                                tmpSeason = New Database.DBElement(Enums.ContentType.TVSeason)
                                tmpSeason = Master.DB.AddTVShowInfoToDBElement(tmpSeason, DBTVShow)
                                tmpSeason.Filename = DBTVEpisode.Filename 'needed to check if the episode is inside a season folder or not
                                tmpSeason.TVSeason = New MediaContainers.SeasonDetails With {.Season = iSeason}

                                'check if tvshow.nfo contains any season details
                                Dim nfoSeason As MediaContainers.SeasonDetails = DBTVShow.TVShow.Seasons.Seasons.FirstOrDefault(Function(f) f.Season = iSeason)
                                If nfoSeason IsNot Nothing Then
                                    tmpSeason.TVSeason.Aired = nfoSeason.Aired
                                    tmpSeason.TVSeason.Plot = nfoSeason.Plot
                                    tmpSeason.TVSeason.Title = nfoSeason.Title
                                    tmpSeason.TVSeason.TMDB = nfoSeason.TMDB
                                    tmpSeason.TVSeason.TVDB = nfoSeason.TVDB
                                Else
                                    'Scrape season info
                                    If isNew AndAlso tmpSeason.TVShow.AnyUniqueIDSpecified AndAlso tmpSeason.ShowIDSpecified Then
                                        ModulesManager.Instance.ScrapeData_TVSeason(tmpSeason, Master.DefaultOptions_TV, False)
                                    End If
                                End If

                                GetFolderContents_TVSeason(tmpSeason)

                                'Scrape season images
                                If isNew AndAlso tmpSeason.TVShow.AnyUniqueIDSpecified AndAlso tmpSeason.ShowIDSpecified Then
                                    Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
                                    Dim ScrapeModifiers As New Structures.ScrapeModifiers
                                    If Not tmpSeason.ImagesContainer.Banner.LocalFilePathSpecified AndAlso Master.eSettings.TVSeasonBannerAnyEnabled Then ScrapeModifiers.SeasonBanner = True
                                    If Not tmpSeason.ImagesContainer.Fanart.LocalFilePathSpecified AndAlso Master.eSettings.TVSeasonFanartAnyEnabled Then ScrapeModifiers.SeasonFanart = True
                                    If Not tmpSeason.ImagesContainer.Landscape.LocalFilePathSpecified AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled Then ScrapeModifiers.SeasonLandscape = True
                                    If Not tmpSeason.ImagesContainer.Poster.LocalFilePathSpecified AndAlso Master.eSettings.TVSeasonPosterAnyEnabled Then ScrapeModifiers.SeasonPoster = True
                                    If ScrapeModifiers.SeasonBanner OrElse ScrapeModifiers.SeasonFanart OrElse ScrapeModifiers.SeasonLandscape OrElse ScrapeModifiers.SeasonPoster Then
                                        If Not ModulesManager.Instance.ScrapeImage_TV(tmpSeason, SearchResultsContainer, ScrapeModifiers, False) Then
                                            Images.SetPreferredImages(tmpSeason, SearchResultsContainer, ScrapeModifiers)
                                        End If
                                    End If
                                End If

                                DBTVShow.Seasons.Add(tmpSeason)
                                newSeasonsIndex.Add(tmpSeason.TVSeason.Season)
                            End If
                        Next
                    End If
                Next
            End If

            'create the "* All Seasons" entry if needed
            If isNew Then
                Dim tmpAllSeasons As Database.DBElement = DBTVShow.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = 999)
                If tmpAllSeasons Is Nothing OrElse tmpAllSeasons.TVSeason Is Nothing Then
                    tmpAllSeasons = New Database.DBElement(Enums.ContentType.TVSeason)
                    tmpAllSeasons = Master.DB.AddTVShowInfoToDBElement(tmpAllSeasons, DBTVShow)
                    tmpAllSeasons.Filename = Path.Combine(DBTVShow.ShowPath, "file.ext")
                    tmpAllSeasons.TVSeason = New MediaContainers.SeasonDetails With {.Season = 999}
                    GetFolderContents_TVSeason(tmpAllSeasons)
                    DBTVShow.Seasons.Add(tmpAllSeasons)
                    newSeasonsIndex.Add(tmpAllSeasons.TVSeason.Season)
                End If
            End If

            'save all new seasons to DB (no sync)
            For Each newSeason As Integer In newSeasonsIndex
                Dim tSeason As Database.DBElement = DBTVShow.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = newSeason)
                If tSeason IsNot Nothing AndAlso tSeason.TVSeason IsNot Nothing Then
                    Master.DB.Save_TVSeason(tSeason, Batchmode, True, False)
                End If
            Next

            'process new episodes
            For Each nEpisode In newEpisodesList
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.DuringUpdateDB_TV, Nothing, Nothing, False, nEpisode)
            Next

            'sync new episodes
            For Each nEpisode In newEpisodesList
                Master.DB.Save_TVEpisode(nEpisode, Batchmode, False, False, False, True, True)
            Next

            'sync new seasons
            For Each newSeason As Integer In newSeasonsIndex
                Dim tSeason As Database.DBElement = DBTVShow.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = newSeason)
                If tSeason IsNot Nothing AndAlso tSeason.TVSeason IsNot Nothing Then
                    Master.DB.Save_TVSeason(tSeason, Batchmode, False, True)
                End If
            Next
        End If

        Return Result
    End Function

    Private Shared Function RegexGetAiredDate(ByVal reg As Match, ByRef eItem As EpisodeItem) As Boolean
        Dim param1 As String = reg.Groups(1).Value
        Dim param2 As String = reg.Groups(2).Value
        Dim param3 As String = reg.Groups(3).Value

        If Not String.IsNullOrEmpty(param1) AndAlso Not String.IsNullOrEmpty(param2) AndAlso Not String.IsNullOrEmpty(param3) Then
            Dim len1 As Integer = param1.Length
            Dim len2 As Integer = param2.Length
            Dim len3 As Integer = param3.Length

            If len1 = 4 AndAlso len2 = 2 AndAlso len3 = 2 Then
                ' yyyy-MM-dd format
                eItem.byDate = True
                eItem.Episode = -1
                eItem.Season = CInt(param1)
                eItem.Aired = String.Concat(param1.ToString, "-", param2.ToString, "-", param3.ToString)
                Return True
            ElseIf len1 = 2 AndAlso len2 = 2 AndAlso len3 = 4 Then
                ' dd-MM-yyyy format
                eItem.byDate = True
                eItem.Episode = -1
                eItem.Season = CInt(param3)
                eItem.Aired = String.Concat(param3.ToString, "-", param2.ToString, "-", param1.ToString)
                Return True
            End If
        End If
        Return False
    End Function

    Private Shared Function RegexGetSeasonAndEpisodeNumber(ByVal reg As Match, ByRef eItem As EpisodeItem, ByVal defaultSeason As Integer) As Boolean
        Dim tSeason As String = reg.Groups(1).Value
        Dim tEpisode As String = reg.Groups(2).Value

        If Not String.IsNullOrEmpty(tSeason) OrElse Not String.IsNullOrEmpty(tEpisode) Then
            Dim endPattern As String = String.Empty
            If String.IsNullOrEmpty(tSeason) AndAlso Not String.IsNullOrEmpty(tEpisode) Then
                'no season specified -> assume defaultSeason
                eItem.Season = defaultSeason
                Dim Episode As Match = Regex.Match(tEpisode, "(\d*)(.*)")
                eItem.Episode = CInt(Episode.Groups(1).Value)
                If Not String.IsNullOrEmpty(Episode.Groups(2).Value) Then endPattern = Episode.Groups(2).Value
            ElseIf Not String.IsNullOrEmpty(tSeason) AndAlso String.IsNullOrEmpty(tEpisode) Then
                'no episode specification -> assume defaultSeason
                eItem.Season = defaultSeason
                eItem.Episode = CInt(tSeason)
            Else
                'season and episode specified
                eItem.Season = CInt(tSeason)
                Dim Episode As Match = Regex.Match(tEpisode, "(\d*)(.*)")
                eItem.Episode = CInt(Episode.Groups(1).Value)
                If Not String.IsNullOrEmpty(Episode.Groups(2).Value) Then endPattern = Episode.Groups(2).Value
            End If
            eItem.byDate = False

            If Not String.IsNullOrEmpty(endPattern) Then
                If Regex.IsMatch(endPattern.ToLower, "[a-i]", RegexOptions.IgnoreCase) Then
                    Dim count As Integer = 1
                    For Each c As Char In "abcdefghi".ToCharArray()
                        If c = endPattern.ToLower Then
                            eItem.SubEpisode = count
                            Exit For
                        End If
                        count += 1
                    Next
                ElseIf endPattern.StartsWith(".") Then
                    eItem.SubEpisode = CInt(endPattern.Substring(1))
                End If
            End If
            Return True
        End If

        Return False
    End Function

    Public Shared Function RegexGetTVEpisode(ByVal sPath As String, ByVal ShowID As Long) As List(Of EpisodeItem)
        Dim retEpisodeItemsList As New List(Of EpisodeItem)

        For Each rShow As Settings.regexp In Master.eSettings.TVShowMatching
            Dim reg As Regex = New Regex(rShow.Regexp, RegexOptions.IgnoreCase)

            Dim regexppos As Integer
            Dim regexp2pos As Integer

            If reg.IsMatch(sPath.ToLower) Then
                Dim eItem As New EpisodeItem
                Dim defaultSeason As Integer = rShow.defaultSeason
                Dim sMatch As Match = reg.Match(sPath.ToLower)

                If rShow.byDate Then
                    If Not RegexGetAiredDate(sMatch, eItem) Then Continue For
                    retEpisodeItemsList.Add(eItem)
                    logger.Info(String.Format("[Scanner] [RegexGetTVEpisode] Found date based match {0} ({1}) [{2}]", sPath, eItem.Aired, rShow.Regexp))
                Else
                    If Not RegexGetSeasonAndEpisodeNumber(sMatch, eItem, defaultSeason) Then Continue For
                    retEpisodeItemsList.Add(eItem)
                    logger.Info(String.Format("[Scanner] [RegexGetTVEpisode] Found episode match {0} (s{1}e{2}{3}) [{4}]", sPath, eItem.Season, eItem.Episode, If(Not eItem.SubEpisode = -1, String.Concat(".", eItem.SubEpisode), String.Empty), rShow.Regexp))
                End If

                ' Grab the remainder from first regexp run
                ' as second run might modify or empty it.
                Dim remainder As String = sMatch.Groups(3).Value.ToString

                If Not String.IsNullOrEmpty(Master.eSettings.TVMultiPartMatching) Then
                    Dim reg2 As Regex = New Regex(Master.eSettings.TVMultiPartMatching, RegexOptions.IgnoreCase)

                    ' check the remainder of the string for any further episodes.
                    If Not rShow.byDate Then
                        ' we want "long circuit" OR below so that both offsets are evaluated
                        While reg2.IsMatch(remainder) OrElse reg.IsMatch(remainder)
                            regexppos = If(reg.IsMatch(remainder), reg.Match(remainder).Index, -1)
                            regexp2pos = If(reg2.IsMatch(remainder), reg2.Match(remainder).Index, -1)
                            If (regexppos <= regexp2pos AndAlso regexppos <> -1) OrElse (regexppos >= 0 AndAlso regexp2pos = -1) Then
                                eItem = New EpisodeItem
                                RegexGetSeasonAndEpisodeNumber(reg.Match(remainder), eItem, defaultSeason)
                                retEpisodeItemsList.Add(eItem)
                                logger.Info(String.Format("[Scanner] [RegexGetTVEpisode] Adding new season {0}, multipart episode {1} [{2}]", eItem.Season, eItem.Episode, rShow.Regexp))
                                remainder = reg.Match(remainder).Groups(3).Value
                            ElseIf (regexp2pos < regexppos AndAlso regexp2pos <> -1) OrElse (regexp2pos >= 0 AndAlso regexppos = -1) Then
                                Dim endPattern As String = String.Empty
                                eItem = New EpisodeItem With {.Season = eItem.Season}
                                Dim Episode As Match = Regex.Match(reg2.Match(remainder).Groups(1).Value, "(\d*)(.*)")
                                eItem.Episode = CInt(Episode.Groups(1).Value)
                                If Not String.IsNullOrEmpty(Episode.Groups(2).Value) Then endPattern = Episode.Groups(2).Value

                                If Not String.IsNullOrEmpty(endPattern) Then
                                    If Regex.IsMatch(endPattern.ToLower, "[a-i]", RegexOptions.IgnoreCase) Then
                                        Dim count As Integer = 1
                                        For Each c As Char In "abcdefghi".ToCharArray()
                                            If c = endPattern.ToLower Then
                                                eItem.SubEpisode = count
                                                Exit For
                                            End If
                                            count += 1
                                        Next
                                    ElseIf endPattern.StartsWith(".") Then
                                        eItem.SubEpisode = CInt(endPattern.Substring(1))
                                    End If
                                End If

                                retEpisodeItemsList.Add(eItem)
                                logger.Info(String.Format("[Scanner] [RegexGetTVEpisode] Adding multipart episode {0} [{1}]", eItem.Episode, Master.eSettings.TVMultiPartMatching))
                                remainder = remainder.Substring(reg2.Match(remainder).Length)
                            End If
                        End While
                    End If
                End If

                Exit For
            End If
        Next

        Return retEpisodeItemsList
    End Function

    ''' <summary>
    ''' Find all related files in a directory.
    ''' </summary>
    ''' <param name="strPath">Full path of the directory.</param>
    ''' <param name="sSource">Structures.Source</param>
    Public Sub ScanForFiles_Movie(ByVal strPath As String, ByVal sSource As Database.DBSource)
        Dim currMovieContainer As Database.DBElement
        Dim di As DirectoryInfo
        Dim lFi As New List(Of FileInfo)
        Dim fList As New List(Of String)
        Dim SkipStack As Boolean = False
        Dim vtsSingle As Boolean = False
        Dim bdmvSingle As Boolean = False
        Dim tFile As String = String.Empty
        Dim autoCheck As Boolean = False

        Try
            If Directory.Exists(Path.Combine(strPath, "VIDEO_TS")) Then
                di = New DirectoryInfo(Path.Combine(strPath, "VIDEO_TS"))
                sSource.IsSingle = True
            ElseIf Directory.Exists(Path.Combine(strPath, String.Concat("BDMV", Path.DirectorySeparatorChar, "STREAM"))) Then
                di = New DirectoryInfo(Path.Combine(strPath, String.Concat("BDMV", Path.DirectorySeparatorChar, "STREAM")))
                sSource.IsSingle = True
            Else
                di = New DirectoryInfo(strPath)
                autoCheck = True
            End If

            Try
                lFi.AddRange(di.GetFiles)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            If lFi.Count > 0 Then

                If Master.eSettings.MovieRecognizeVTSExpertVTS AndAlso autoCheck Then
                    If lFi.Where(Function(s) s.Name.ToLower = "index.bdmv").Count > 0 Then
                        bdmvSingle = True
                        tFile = FileUtils.Common.GetLongestFromRip(strPath, True)
                        If bwPrelim.CancellationPending Then Return
                    End If
                End If

                If Not bdmvSingle AndAlso Master.eSettings.MovieRecognizeVTSExpertVTS AndAlso autoCheck Then
                    Dim hasIfo As Integer = 0
                    Dim hasVob As Integer = 0
                    Dim hasBup As Integer = 0
                    For Each lfile As FileInfo In lFi
                        If lfile.Extension.ToLower = ".ifo" Then hasIfo = 1
                        If lfile.Extension.ToLower = ".vob" Then hasVob = 1
                        If lfile.Extension.ToLower = ".bup" Then hasBup = 1
                        If lfile.Name.ToLower = "video_ts.ifo" Then
                            'video_ts.vob takes precedence
                            tFile = lfile.FullName
                        ElseIf String.IsNullOrEmpty(tFile) AndAlso lfile.Extension.ToLower = ".vob" Then
                            'get any vob
                            tFile = lfile.FullName
                        End If
                        vtsSingle = (hasIfo + hasVob + hasBup) > 1
                        If vtsSingle AndAlso Path.GetFileName(tFile).ToLower = "video_ts.ifo" Then Exit For
                        If bwPrelim.CancellationPending Then Return
                    Next
                End If

                If (vtsSingle OrElse bdmvSingle) AndAlso Not String.IsNullOrEmpty(tFile) Then
                    If Not MoviePaths.Contains(FileUtils.Common.RemoveStackingMarkers(tFile.ToLower)) Then
                        If Master.eSettings.FileSystemNoStackExts.Contains(Path.GetExtension(tFile).ToLower) Then
                            MoviePaths.Add(tFile.ToLower)
                        Else
                            MoviePaths.Add(FileUtils.Common.RemoveStackingMarkers(tFile).ToLower)
                        End If
                        currMovieContainer = New Database.DBElement(Enums.ContentType.Movie)
                        currMovieContainer.ActorThumbs = New List(Of String)
                        currMovieContainer.Filename = tFile
                        currMovieContainer.IsSingle = True
                        currMovieContainer.Language = sSource.Language
                        currMovieContainer.Source = sSource
                        currMovieContainer.Subtitles = New List(Of MediaContainers.Subtitle)
                        Load_Movie(currMovieContainer, True)
                        bwPrelim.ReportProgress(-1, New ProgressValue With {.EventType = Enums.ScannerEventType.Added_Movie, .ID = currMovieContainer.ID, .Message = currMovieContainer.Movie.Title})
                    End If

                Else
                    Dim HasFile As Boolean = False
                    Dim tList As IOrderedEnumerable(Of FileInfo) = lFi.Where(Function(f) Master.eSettings.FileSystemValidExts.Contains(f.Extension.ToLower) AndAlso
                             Not Regex.IsMatch(f.Name, String.Concat("[^\w\s]\s?(", AdvancedSettings.GetSetting("NotValidFileContains", "trailer|sample"), ")"), RegexOptions.IgnoreCase) AndAlso ((Master.eSettings.MovieSkipStackedSizeCheck AndAlso
                            FileUtils.Common.isStacked(f.FullName)) OrElse (Not Convert.ToInt32(Master.eSettings.MovieSkipLessThan) > 0 OrElse f.Length >= Master.eSettings.MovieSkipLessThan * 1048576))).OrderBy(Function(f) f.FullName)

                    If tList.Count > 1 AndAlso sSource.IsSingle Then
                        'check if we already have a movie from this folder
                        If MoviePaths.Where(Function(f) tList.Where(Function(l) FileUtils.Common.RemoveStackingMarkers(l.FullName).ToLower = f).Count > 0).Count > 0 Then
                            HasFile = True
                        End If
                    End If

                    If Not HasFile Then
                        For Each lFile As FileInfo In tList

                            If Not MoviePaths.Contains(FileUtils.Common.RemoveStackingMarkers(lFile.FullName).ToLower) Then
                                If Master.eSettings.FileSystemNoStackExts.Contains(lFile.Extension.ToLower) Then
                                    MoviePaths.Add(lFile.FullName.ToLower)
                                    SkipStack = True
                                Else
                                    MoviePaths.Add(FileUtils.Common.RemoveStackingMarkers(lFile.FullName).ToLower)
                                End If
                                fList.Add(lFile.FullName)
                            End If
                            If sSource.IsSingle AndAlso Not SkipStack Then Exit For
                            If bwPrelim.CancellationPending Then Return
                        Next
                    End If

                    For Each s As String In fList
                        currMovieContainer = New Database.DBElement(Enums.ContentType.Movie)
                        currMovieContainer.ActorThumbs = New List(Of String)
                        currMovieContainer.Filename = s
                        currMovieContainer.IsSingle = sSource.IsSingle
                        currMovieContainer.Language = sSource.Language
                        currMovieContainer.Source = sSource
                        currMovieContainer.Subtitles = New List(Of MediaContainers.Subtitle)
                        Load_Movie(currMovieContainer, True)
                        bwPrelim.ReportProgress(-1, New ProgressValue With {.EventType = Enums.ScannerEventType.Added_Movie, .ID = currMovieContainer.ID, .Message = currMovieContainer.Movie.Title})
                    Next
                End If

            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    ''' <summary>
    ''' Find all related files in a directory.
    ''' </summary>
    ''' <param name="tShow">TVShowContainer object</param>
    ''' <param name="sPath">Path of folder contianing the episodes</param>
    Public Sub ScanForFiles_TV(ByRef tShow As Database.DBElement, ByVal sPath As String)
        Dim di As New DirectoryInfo(sPath)

        For Each lFile As FileInfo In di.GetFiles.OrderBy(Function(s) s.Name)
            Try
                If Not TVEpisodePaths.Contains(lFile.FullName.ToLower) AndAlso Master.eSettings.FileSystemValidExts.Contains(lFile.Extension.ToLower) AndAlso
                    Not Regex.IsMatch(lFile.Name, String.Concat("[^\w\s]\s?(", AdvancedSettings.GetSetting("NotValidFileContains", "trailer|sample"), ")"), RegexOptions.IgnoreCase) AndAlso
                    (Not Convert.ToInt32(Master.eSettings.TVSkipLessThan) > 0 OrElse lFile.Length >= Master.eSettings.TVSkipLessThan * 1048576) Then
                    tShow.Episodes.Add(New Database.DBElement(Enums.ContentType.TVEpisode) With {.Filename = lFile.FullName, .TVEpisode = New MediaContainers.EpisodeDetails})
                ElseIf Regex.IsMatch(lFile.Name, String.Concat("[^\w\s]\s?(", AdvancedSettings.GetSetting("NotValidFileContains", "trailer|sample"), ")"), RegexOptions.IgnoreCase) AndAlso Master.eSettings.FileSystemValidExts.Contains(lFile.Extension.ToLower) Then
                    logger.Info(String.Format("[Sanner] [ScanForFiles_TV] File ""{0}"" has been ignored (ignore list)", lFile.FullName))
                End If
            Catch ex As Exception
                logger.Error(String.Format("[Sanner] [ScanForFiles_TV] File ""{0}"" has been skipped ({1})", lFile.Name, ex.Message))
            End Try
        Next
    End Sub

    ''' <summary>
    ''' Get all directories/movies in the parent directory
    ''' </summary>
    ''' <param name="sSource"></param>
    ''' <param name="strPath">Specific Path to scan</param>
    Public Sub ScanSourceDirectory_Movie(ByVal sSource As Database.DBSource, ByVal doScan As Boolean, Optional ByVal strPath As String = "")
        Dim strScanPath As String = String.Empty

        If Not String.IsNullOrEmpty(strPath) Then
            strScanPath = strPath
        Else
            strScanPath = sSource.Path
        End If

        If Directory.Exists(strScanPath) Then
            Dim strMoviePath As String = String.Empty

            Dim dInfo As New DirectoryInfo(strScanPath)
            Dim dList As IEnumerable(Of DirectoryInfo) = Nothing

            Try

                'check if there are any movies in this directory
                If doScan Then ScanForFiles_Movie(strScanPath, sSource)

                If Master.eSettings.MovieScanOrderModify Then
                    Try
                        dList = dInfo.GetDirectories.Where(Function(s) (Master.eSettings.MovieGeneralIgnoreLastScan OrElse sSource.Recursive OrElse s.LastWriteTime > SourceLastScan) AndAlso IsValidDir(s, False)).OrderBy(Function(d) d.LastWriteTime)
                    Catch ex As Exception
                        logger.Error(ex, New StackFrame().GetMethod().Name)
                    End Try
                Else
                    Try
                        dList = dInfo.GetDirectories.Where(Function(s) (Master.eSettings.MovieGeneralIgnoreLastScan OrElse sSource.Recursive OrElse s.LastWriteTime > SourceLastScan) AndAlso IsValidDir(s, False)).OrderBy(Function(d) d.Name)
                    Catch ex As Exception
                        logger.Error(ex, New StackFrame().GetMethod().Name)
                    End Try
                End If

                For Each inDir As DirectoryInfo In dList
                    If bwPrelim.CancellationPending Then Return
                    ScanForFiles_Movie(inDir.FullName, sSource)
                    If sSource.Recursive Then
                        ScanSourceDirectory_Movie(sSource, False, inDir.FullName)
                    End If
                Next

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Get all directories in the parent directory
    ''' </summary>
    ''' <param name="sSource"></param>
    ''' <param name="sPath">Specific Path to scan</param>
    Public Sub ScanSourceDirectory_TV(ByVal sSource As Database.DBSource, Optional ByVal sPath As String = "")
        Dim ScanPath As String = String.Empty

        If Not String.IsNullOrEmpty(sPath) Then
            ScanPath = sPath
        Else
            ScanPath = sSource.Path
        End If

        If Directory.Exists(ScanPath) Then

            Dim currShowContainer As Database.DBElement
            Dim dInfo As New DirectoryInfo(ScanPath)
            Dim inInfo As DirectoryInfo
            Dim inList As IEnumerable(Of DirectoryInfo) = Nothing

            'tv show folder as a source
            If sSource.IsSingle Then
                currShowContainer = New Database.DBElement(Enums.ContentType.TVShow)
                currShowContainer.EpisodeSorting = sSource.EpisodeSorting
                currShowContainer.Language = sSource.Language
                currShowContainer.Ordering = sSource.Ordering
                currShowContainer.ShowPath = dInfo.FullName
                currShowContainer.Source = sSource
                ScanForFiles_TV(currShowContainer, dInfo.FullName)

                If Master.eSettings.TVScanOrderModify Then
                    Try
                        inList = dInfo.GetDirectories.Where(Function(d) (Master.eSettings.TVGeneralIgnoreLastScan OrElse d.LastWriteTime > SourceLastScan) AndAlso IsValidDir(d, True)).OrderBy(Function(d) d.LastWriteTime)
                    Catch ex As Exception
                        logger.Error(ex, New StackFrame().GetMethod().Name)
                    End Try
                Else
                    Try
                        inList = dInfo.GetDirectories.Where(Function(d) (Master.eSettings.TVGeneralIgnoreLastScan OrElse d.LastWriteTime > SourceLastScan) AndAlso IsValidDir(d, True)).OrderBy(Function(d) d.Name)
                    Catch ex As Exception
                        logger.Error(ex, New StackFrame().GetMethod().Name)
                    End Try
                End If

                For Each sDirs As DirectoryInfo In inList
                    ScanForFiles_TV(currShowContainer, sDirs.FullName)
                    ScanSubDirectory_TV(currShowContainer, sDirs.FullName)
                Next

                Dim Result = Load_TVShow(currShowContainer, True, True, True)
                If Not Result = Enums.ScannerEventType.None Then
                    bwPrelim.ReportProgress(-1, New ProgressValue With {.EventType = Result, .ID = currShowContainer.ID, .Message = currShowContainer.TVShow.Title})
                End If
            Else
                For Each inDir As DirectoryInfo In dInfo.GetDirectories.Where(Function(d) IsValidDir(d, True)).OrderBy(Function(d) d.Name)
                    currShowContainer = New Database.DBElement(Enums.ContentType.TVShow)
                    currShowContainer.EpisodeSorting = sSource.EpisodeSorting
                    currShowContainer.Language = sSource.Language
                    currShowContainer.Ordering = sSource.Ordering
                    currShowContainer.ShowPath = inDir.FullName
                    currShowContainer.Source = sSource
                    ScanForFiles_TV(currShowContainer, inDir.FullName)

                    inInfo = New DirectoryInfo(inDir.FullName)

                    If Master.eSettings.TVScanOrderModify Then
                        Try
                            inList = inInfo.GetDirectories.Where(Function(d) (Master.eSettings.TVGeneralIgnoreLastScan OrElse d.LastWriteTime > SourceLastScan) AndAlso IsValidDir(d, True)).OrderBy(Function(d) d.LastWriteTime)
                        Catch
                        End Try
                    Else
                        Try
                            inList = inInfo.GetDirectories.Where(Function(d) (Master.eSettings.TVGeneralIgnoreLastScan OrElse d.LastWriteTime > SourceLastScan) AndAlso IsValidDir(d, True)).OrderBy(Function(d) d.Name)
                        Catch
                        End Try
                    End If

                    For Each sDirs As DirectoryInfo In inList
                        ScanForFiles_TV(currShowContainer, sDirs.FullName)
                        ScanSubDirectory_TV(currShowContainer, sDirs.FullName)
                    Next

                    Dim Result = Load_TVShow(currShowContainer, True, True, True)
                    If Not Result = Enums.ScannerEventType.None Then
                        bwPrelim.ReportProgress(-1, New ProgressValue With {.EventType = Result, .ID = currShowContainer.ID, .Message = currShowContainer.TVShow.Title})
                    End If
                Next

            End If
        End If
    End Sub

    ''' <summary>
    ''' Check if a path contains movies.
    ''' </summary>
    ''' <param name="inDir">DirectoryInfo object of directory to scan</param>
    ''' <returns>True if directory contains movie files.</returns>
    Public Function ScanSubDirectory_Movie(ByVal inDir As DirectoryInfo) As Boolean
        Try

            If inDir.GetFiles.Where(Function(s) Master.eSettings.FileSystemValidExts.Contains(s.Extension.ToLower) AndAlso
                                                      Not s.Name.ToLower.Contains("-trailer") AndAlso Not s.Name.ToLower.Contains("[trailer") AndAlso
                                                      Not s.Name.ToLower.Contains("sample")).OrderBy(Function(s) s.Name).Count > 0 Then Return True

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return False
    End Function

    Private Sub ScanSubDirectory_TV(ByRef tShow As Database.DBElement, ByVal strPath As String)
        Dim inInfo As DirectoryInfo
        Dim inList As IEnumerable(Of DirectoryInfo) = Nothing

        inInfo = New DirectoryInfo(strPath)
        inList = inInfo.GetDirectories.Where(Function(d) IsValidDir(d, True)).OrderBy(Function(d) d.Name)

        For Each sDirs As DirectoryInfo In inList
            ScanForFiles_TV(tShow, sDirs.FullName)
            ScanSubDirectory_TV(tShow, sDirs.FullName)
        Next
    End Sub

    Public Sub Start(ByVal Scan As Structures.ScanOrClean, ByVal SourceID As Long, ByVal Folder As String)
        bwPrelim = New System.ComponentModel.BackgroundWorker
        bwPrelim.WorkerReportsProgress = True
        bwPrelim.WorkerSupportsCancellation = True
        bwPrelim.RunWorkerAsync(New Arguments With {.Scan = Scan, .SourceID = SourceID, .Folder = Folder})
    End Sub

    ''' <summary>
    ''' Check if there are movies in the subdirectorys of a path.
    ''' </summary>
    ''' <param name="MovieDir">DirectoryInfo object of directory to scan.</param>
    ''' <returns>True if the path's subdirectories contain movie files, else false.</returns>
    Public Function SubDirsHaveMovies(ByVal MovieDir As DirectoryInfo) As Boolean
        Try
            If Directory.Exists(MovieDir.FullName) Then

                For Each inDir As DirectoryInfo In MovieDir.GetDirectories
                    If IsValidDir(inDir, False) Then
                        If ScanSubDirectory_Movie(inDir) Then Return True
                        SubDirsHaveMovies(inDir)
                    End If
                Next

            End If
            Return False
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            Return False
        End Try
    End Function

    Private Sub bwPrelim_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwPrelim.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Master.DB.ClearNew()

        If Args.Scan.SpecificFolder AndAlso Not String.IsNullOrEmpty(Args.Folder) AndAlso Directory.Exists(Args.Folder) Then

            For Each eSource In Master.MovieSources
                Dim tSource As String = If(eSource.Path.EndsWith(Path.DirectorySeparatorChar), eSource.Path, String.Concat(eSource.Path, Path.DirectorySeparatorChar)).ToLower.Trim
                Dim tFolder As String = If(Args.Folder.EndsWith(Path.DirectorySeparatorChar), Args.Folder, String.Concat(Args.Folder, Path.DirectorySeparatorChar)).ToLower.Trim

                If tFolder.StartsWith(tSource) Then
                    MoviePaths = Master.DB.GetAllMoviePaths
                    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            ScanSourceDirectory_Movie(eSource, True, Args.Folder)
                        End Using
                        SQLtransaction.Commit()
                    End Using
                    Args.Scan.Movies = True
                    Exit For
                End If
            Next

            For Each eSource In Master.TVShowSources
                Dim tSource As String = If(eSource.Path.EndsWith(Path.DirectorySeparatorChar), eSource.Path, String.Concat(eSource.Path, Path.DirectorySeparatorChar)).ToLower.Trim
                Dim tFolder As String = If(Args.Folder.EndsWith(Path.DirectorySeparatorChar), Args.Folder, String.Concat(Args.Folder, Path.DirectorySeparatorChar)).ToLower.Trim

                If tFolder.StartsWith(tSource) Then
                    TVEpisodePaths = Master.DB.GetAllTVEpisodePaths
                    TVShowPaths = Master.DB.GetAllTVShowPaths

                    If Args.Folder.ToLower = eSource.Path.ToLower Then
                        'Args.Folder is a tv show source folder -> scan the whole source
                        ScanSourceDirectory_TV(eSource)
                    Else
                        'Args.Folder is a tv show folder or a tv show subfolder -> get the tv show main path
                        Dim ShowID As Long = -1
                        For Each hKey In TVShowPaths.Keys
                            If String.Concat(Args.Folder.ToLower, Path.DirectorySeparatorChar).StartsWith(String.Concat(hKey.ToString.ToLower, Path.DirectorySeparatorChar)) Then
                                ShowID = Convert.ToInt64(TVShowPaths.Item(hKey))
                                Exit For
                            End If
                        Next

                        If Not ShowID = -1 Then
                            Dim currShowContainer As Database.DBElement = Master.DB.Load_TVShow(ShowID, False, False)

                            Dim inInfo As DirectoryInfo = New DirectoryInfo(currShowContainer.ShowPath)
                            Dim inList As IEnumerable(Of DirectoryInfo) = Nothing
                            Try
                                inList = inInfo.GetDirectories.Where(Function(d) (Master.eSettings.TVGeneralIgnoreLastScan OrElse d.LastWriteTime > SourceLastScan) AndAlso IsValidDir(d, True)).OrderBy(Function(d) d.Name)
                            Catch
                            End Try

                            ScanForFiles_TV(currShowContainer, inInfo.FullName)

                            For Each sDirs As DirectoryInfo In inList
                                ScanForFiles_TV(currShowContainer, sDirs.FullName)
                            Next

                            Dim Result = Load_TVShow(currShowContainer, True, True, True)
                            If Not Result = Enums.ScannerEventType.None Then
                                bwPrelim.ReportProgress(-1, New ProgressValue With {.EventType = Result, .ID = currShowContainer.ID, .Message = currShowContainer.TVShow.Title})
                            End If
                        End If
                    End If
                    Args.Scan.TV = True
                    Exit For
                End If
            Next
        End If

        If Not Args.Scan.SpecificFolder AndAlso Args.Scan.Movies Then
            MoviePaths = Master.DB.GetAllMoviePaths

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    If Not Args.SourceID = -1 Then
                        SQLcommand.CommandText = String.Format("SELECT * FROM moviesource WHERE idSource = {0};", Args.SourceID)
                    Else
                        SQLcommand.CommandText = "SELECT * FROM moviesource WHERE bExclude = 0;"
                    End If

                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        Using SQLUpdatecommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLUpdatecommand.CommandText = "UPDATE moviesource SET strLastScan = (?) WHERE idSource = (?);"
                            Dim parLastScan As SQLite.SQLiteParameter = SQLUpdatecommand.Parameters.Add("parLastScan", DbType.String, 0, "strLastScan")
                            Dim parID As SQLite.SQLiteParameter = SQLUpdatecommand.Parameters.Add("parID", DbType.Int64, 0, "idSource")
                            While SQLreader.Read
                                Dim sSource As Database.DBSource = Master.DB.Load_Source_Movie(Convert.ToInt64(SQLreader("idSource")))
                                Try
                                    SourceLastScan = If(sSource.LastScanSpecified, Convert.ToDateTime(sSource.LastScan), DateTime.Now)
                                Catch ex As Exception
                                    SourceLastScan = DateTime.Now
                                End Try
                                If sSource.Recursive OrElse (Master.eSettings.MovieGeneralIgnoreLastScan OrElse Directory.GetLastWriteTime(sSource.Path) > SourceLastScan) Then
                                    'save the scan time back to the db
                                    parLastScan.Value = DateTime.Now
                                    parID.Value = sSource.ID
                                    SQLUpdatecommand.ExecuteNonQuery()
                                    Try
                                        If Master.eSettings.MovieSortBeforeScan OrElse sSource.IsSingle Then
                                            FileUtils.SortFiles(SQLreader("strPath").ToString)
                                        End If
                                    Catch ex As Exception
                                        logger.Error(ex, New StackFrame().GetMethod().Name)
                                    End Try
                                    ScanSourceDirectory_Movie(sSource, True)
                                End If
                                If bwPrelim.CancellationPending Then
                                    e.Cancel = True
                                    Return
                                End If
                            End While
                        End Using
                    End Using
                End Using
                SQLtransaction.Commit()
            End Using
        End If

        If Not Args.Scan.SpecificFolder AndAlso Args.Scan.TV Then
            TVEpisodePaths = Master.DB.GetAllTVEpisodePaths
            TVShowPaths = Master.DB.GetAllTVShowPaths

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    If Not Args.SourceID = -1 Then
                        SQLcommand.CommandText = String.Format("SELECT * FROM tvshowsource WHERE idSource = {0};", Args.SourceID)
                    Else
                        SQLcommand.CommandText = "SELECT * FROM tvshowsource WHERE bExclude = 0;"
                    End If

                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        Using SQLUpdatecommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLUpdatecommand.CommandText = "UPDATE tvshowsource SET strLastScan = (?) WHERE idSource = (?);"
                            Dim parLastScan As SQLite.SQLiteParameter = SQLUpdatecommand.Parameters.Add("parLastScan", DbType.String, 0, "strLastScan")
                            Dim parID As SQLite.SQLiteParameter = SQLUpdatecommand.Parameters.Add("parID", DbType.Int64, 0, "idSource")
                            While SQLreader.Read
                                Dim sSource As Database.DBSource = Master.DB.Load_Source_TVShow(Convert.ToInt64(SQLreader("idSource")))
                                Try
                                    SourceLastScan = If(sSource.LastScanSpecified, Convert.ToDateTime(sSource.LastScan), DateTime.Now)
                                Catch ex As Exception
                                    SourceLastScan = DateTime.Now
                                End Try
                                'save the scan time back to the db
                                parLastScan.Value = DateTime.Now
                                parID.Value = sSource.ID
                                SQLUpdatecommand.ExecuteNonQuery()
                                ScanSourceDirectory_TV(sSource)
                                If bwPrelim.CancellationPending Then
                                    e.Cancel = True
                                    Return
                                End If
                            End While
                        End Using
                    End Using
                End Using
                SQLtransaction.Commit()
            End Using
        End If

        'no separate MovieSet scanning possible, so we clean MovieSets when movies were scanned
        If (Master.eSettings.MovieCleanDB AndAlso Args.Scan.Movies) OrElse (Master.eSettings.MovieSetCleanDB AndAlso Args.Scan.Movies) OrElse (Master.eSettings.TVCleanDB AndAlso Args.Scan.TV) Then
            bwPrelim.ReportProgress(-1, New ProgressValue With {.EventType = Enums.ScannerEventType.CleaningDatabase, .Message = String.Empty})
            'remove any db entries that no longer exist
            Master.DB.Clean(Master.eSettings.MovieCleanDB AndAlso Args.Scan.Movies, Master.eSettings.MovieSetCleanDB AndAlso Args.Scan.MovieSets, Master.eSettings.TVCleanDB AndAlso Args.Scan.TV, Args.SourceID)
        End If

        e.Result = Args
    End Sub

    Private Sub bwPrelim_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwPrelim.ProgressChanged
        Dim tProgressValue As ProgressValue = DirectCast(e.UserState, ProgressValue)
        RaiseEvent ProgressUpdate(tProgressValue)

        Select Case tProgressValue.EventType
            Case Enums.ScannerEventType.Added_Movie
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"newmovie", 3, Master.eLang.GetString(817, "New Movie Added"), tProgressValue.Message, Nothing}))
            Case Enums.ScannerEventType.Added_TVEpisode
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"newep", 4, Master.eLang.GetString(818, "New Episode Added"), tProgressValue.Message, Nothing}))
        End Select
    End Sub

    Private Sub bwPrelim_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwPrelim.RunWorkerCompleted
        If Not e.Cancelled Then
            Dim Args As Arguments = DirectCast(e.Result, Arguments)
            If Args.Scan.Movies Then
                Dim params As New List(Of Object)(New Object() {False, False, False, True, Args.SourceID})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterUpdateDB_Movie, params, Nothing)
            End If
            If Args.Scan.TV Then
                Dim params As New List(Of Object)(New Object() {False, False, False, True, Args.SourceID})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterUpdateDB_TV, params, Nothing)
            End If
            RaiseEvent ProgressUpdate(New ProgressValue With {.EventType = Enums.ScannerEventType.ScannerEnded})
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim Folder As String
        Dim Scan As Structures.ScanOrClean
        Dim SourceID As Long

#End Region 'Fields

    End Structure

    Public Structure ProgressValue

#Region "Fields"

        Dim ContentType As Enums.ContentType
        Dim EventType As Enums.ScannerEventType
        Dim ID As Long
        Dim Message As String

#End Region 'Fields

    End Structure

    Public Class EpisodeItem

#Region "Fields"

        Private _aired As String
        Private _bydate As Boolean
        Private _episode As Integer
        Private _idepisode As Long
        Private _season As Integer
        Private _subepisode As Integer

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Aired() As String
            Get
                Return _aired
            End Get
            Set(ByVal value As String)
                _aired = value
            End Set
        End Property

        Public Property byDate() As Boolean
            Get
                Return _bydate
            End Get
            Set(ByVal value As Boolean)
                _bydate = value
            End Set
        End Property

        Public Property Episode() As Integer
            Get
                Return _episode
            End Get
            Set(ByVal value As Integer)
                _episode = value
            End Set
        End Property

        Public Property idEpisode() As Long
            Get
                Return _idepisode
            End Get
            Set(ByVal value As Long)
                _idepisode = value
            End Set
        End Property

        Public Property Season() As Integer
            Get
                Return _season
            End Get
            Set(ByVal value As Integer)
                _season = value
            End Set
        End Property

        Public Property SubEpisode() As Integer
            Get
                Return _subepisode
            End Get
            Set(ByVal value As Integer)
                _subepisode = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _aired = String.Empty
            _bydate = False
            _episode = -1
            _idepisode = -1
            _season = -1
            _subepisode = -1
        End Sub

#End Region 'Methods

    End Class

    Public Class SeasonAndEpisodeItems

#Region "Fields"

        Private _episodes As List(Of Database.DBElement)
        Private _seasons As List(Of Integer)

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Episodes() As List(Of Database.DBElement)
            Get
                Return _episodes
            End Get
            Set(ByVal value As List(Of Database.DBElement))
                _episodes = value
            End Set
        End Property

        Public Property Seasons() As List(Of Integer)
            Get
                Return _seasons
            End Get
            Set(ByVal value As List(Of Integer))
                _seasons = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _episodes = New List(Of Database.DBElement)
            _seasons = New List(Of Integer)
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class