'################################################################################
'#                             EMBER MEDIA MANAGER                              #
'################################################################################
'################################################################################
'# This file is part of Ember Media Manager.                                    #
'#                                                                              #
'# Ember Media Manager is free software: you can redistribute it and/or modify  #
'# it under the terms of the GNU General Public License as published by         #
'# the Free Software Foundation, either version 3 of the License, or            #
'# (at your option) any later version.                                          #
'#                                                                              #
'# Ember Media Manager is distributed in the hope that it will be useful,       #
'# but WITHOUT ANY WARRANTY; without even the implied warranty of               #
'# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                #
'# GNU General Public License for more details.                                 #
'#                                                                              #
'# You should have received a copy of the GNU General Public License            #
'# along with Ember Media Manager.  If not, see <http://www.gnu.org/licenses/>. #
'################################################################################

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

    Public Event ScannerUpdated(ByVal iType As Integer, ByVal sText As String)

    Public Event ScanningCompleted()

#End Region 'Events

#Region "Methods"

    Public Sub Cancel()
        If Me.bwPrelim.IsBusy Then Me.bwPrelim.CancelAsync()
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
    ''' <param name="Movie">MovieContainer object.</param>
    Public Sub GetMovieFolderContents(ByRef Movie As MovieContainer)
        Dim currname As String = String.Empty
        Dim atList As New List(Of String)   'actor thumbs list
        Dim efList As New List(Of String)   'extrafanart list
        Dim etList As New List(Of String)   'extrathumbs list
        Dim sList As New List(Of String)    'external subtitles files list
        Dim tList As New List(Of String)    'theme files list
        Dim fList As New List(Of String)    'all other files list
        Dim fName As String = String.Empty

        Dim parPath As String = String.Empty

        Dim fileName As String = Path.GetFileNameWithoutExtension(Movie.Filename)
        Dim fileNameStack As String = StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(Movie.Filename))
        Dim filePath As String = Path.Combine(Directory.GetParent(Movie.Filename).FullName, fileName)
        Dim filePathStack As String = Path.Combine(Directory.GetParent(Movie.Filename).FullName, fileNameStack)
        Dim fileParPath As String = Directory.GetParent(filePath).FullName

        Try
            'first add files to filelists
            If FileUtils.Common.isVideoTS(Movie.Filename) Then
                parPath = Directory.GetParent(Directory.GetParent(Movie.Filename).FullName).FullName

                Try
                    fList.AddRange(Directory.GetFiles(Directory.GetParent(Movie.Filename).FullName))
                    fList.AddRange(Directory.GetFiles(parPath))
                    If Master.eSettings.MovieUseNMJ Then
                        fList.AddRange(Directory.GetFiles(Directory.GetParent(parPath).FullName))
                    End If
                Catch
                End Try
            ElseIf FileUtils.Common.isBDRip(Movie.Filename) Then
                parPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Movie.Filename).FullName).FullName).FullName

                Try
                    fList.AddRange(Directory.GetFiles(Directory.GetParent(Directory.GetParent(Movie.Filename).FullName).FullName))
                    fList.AddRange(Directory.GetFiles(parPath))
                    If Master.eSettings.MovieUseNMJ Then
                        fList.AddRange(Directory.GetFiles(Directory.GetParent(parPath).FullName))
                    End If
                Catch
                End Try
            Else
                parPath = Directory.GetParent(Movie.Filename).FullName

                If Movie.isSingle Then
                    fList.AddRange(Directory.GetFiles(parPath))
                Else
                    Try
                        Dim sName As String = StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(Movie.Filename), True)
                        fList.AddRange(Directory.GetFiles(parPath, If(sName.EndsWith("*"), sName, String.Concat(sName, "*"))))
                    Catch
                    End Try
                End If
            End If

            'secondly add files from special folders to filelists
            If Movie.isSingle Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainActorThumbs)
                    Dim parDir As String = Directory.GetParent(a.Replace("<placeholder>", "placeholder")).FullName
                    If Directory.Exists(parDir) Then
                        atList.AddRange(Directory.GetFiles(parDir))
                    End If
                Next
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainEFanarts)
                    If Directory.Exists(a) Then
                        efList.AddRange(Directory.GetFiles(a))
                    End If
                Next
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainEThumbs)
                    If Directory.Exists(a) Then
                        etList.AddRange(Directory.GetFiles(a))
                    End If
                Next
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainSubtitle)
                    If Directory.Exists(a) Then
                        sList.AddRange(Directory.GetFiles(a))
                    End If
                Next
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainTheme)
                    If Directory.Exists(Directory.GetParent(a).FullName) Then
                        tList.AddRange(Directory.GetFiles(Directory.GetParent(a).FullName))
                    End If
                Next
            End If

            'actor thumbs
            If atList.Count > 0 Then
                Movie.ActorThumbs.AddRange(atList)
            End If

            'banner
            If String.IsNullOrEmpty(Movie.Banner) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainBanner)
                    Movie.Banner = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.Banner) Then Exit For
                Next
            End If

            'clearart
            If String.IsNullOrEmpty(Movie.ClearArt) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainClearArt)
                    Movie.ClearArt = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.ClearArt) Then Exit For
                Next
            End If

            'clearlogo
            If String.IsNullOrEmpty(Movie.ClearLogo) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainClearLogo)
                    Movie.ClearLogo = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.ClearLogo) Then Exit For
                Next
            End If

            'discart
            If String.IsNullOrEmpty(Movie.DiscArt) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainDiscArt)
                    Movie.DiscArt = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.DiscArt) Then Exit For
                Next
            End If

            'extrafanart
            If String.IsNullOrEmpty(Movie.EFanarts) Then
                If eflist.Count > 0 Then
                    Movie.EFanarts = efList.Item(0).ToString
                End If
            End If

            'extrathumbs
            If String.IsNullOrEmpty(Movie.EThumbs) Then
                If etList.Count > 0 Then
                    Movie.EThumbs = etList.Item(0).ToString
                End If
            End If

            'fanart
            If String.IsNullOrEmpty(Movie.Fanart) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainFanart)
                    Movie.Fanart = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.Fanart) Then Exit For
                Next
            End If

            'landscape
            If String.IsNullOrEmpty(Movie.Landscape) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainLandscape)
                    Movie.Landscape = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.Landscape) Then Exit For
                Next
            End If

            'nfo
            If String.IsNullOrEmpty(Movie.Nfo) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainNFO)
                    Movie.Nfo = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.Nfo) Then Exit For
                Next
            End If

            'poster
            If String.IsNullOrEmpty(Movie.Poster) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainPoster)
                    Movie.Poster = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.Poster) Then Exit For
                Next
            End If

            'subtitles (external)
            For Each fFile As String In sList
                For Each ext In Master.eSettings.FileSystemValidSubtitlesExts
                    If fFile.ToLower.EndsWith(ext) Then
                        Dim isForced As Boolean = Path.GetFileNameWithoutExtension(fFile).ToLower.EndsWith("forced")
                        Movie.Subtitles.Add(New MediaInfo.Subtitle With {.SubsPath = fFile, .SubsType = "External", .SubsForced = isForced})
                    End If
                Next
            Next

            'theme
            If String.IsNullOrEmpty(Movie.Theme) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainTheme)
                    For Each t As String In Master.eSettings.FileSystemValidThemeExts
                        Movie.Theme = tList.FirstOrDefault(Function(s) s.ToLower = String.Concat(a.ToLower, t.ToLower))
                        If Not String.IsNullOrEmpty(Movie.Theme) Then Exit For
                    Next
                    If Not String.IsNullOrEmpty(Movie.Theme) Then Exit For
                Next
            End If

            'trailer
            If String.IsNullOrEmpty(Movie.Trailer) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.ModifierType.MainTrailer)
                    For Each t As String In Master.eSettings.FileSystemValidExts
                        Movie.Trailer = fList.FirstOrDefault(Function(s) s.ToLower = String.Concat(a.ToLower, t.ToLower))
                        If Not String.IsNullOrEmpty(Movie.Trailer) Then Exit For
                    Next
                    If Not String.IsNullOrEmpty(Movie.Trailer) Then Exit For
                Next
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        efList = Nothing
        etList = Nothing
        fList = Nothing
        sList = Nothing
    End Sub

    ''' <summary>
    ''' Check if a directory contains supporting files (nfo, poster, fanart, etc)
    ''' </summary>
    ''' <param name="MovieSet">MovieSetContainer object.</param>
    Public Sub GetMovieSetFolderContents(ByRef MovieSet As MovieSetContainer)
        Dim fList As New List(Of String)    'all other files list
        Dim fPath As String = Master.eSettings.MovieSetPathMSAA

        Try
            'first add files to filelists
            If Not String.IsNullOrEmpty(fPath) AndAlso Directory.Exists(fPath) Then
                fList.AddRange(Directory.GetFiles(fPath))
            End If

            'banner
            If String.IsNullOrEmpty(MovieSet.Banner) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.ModifierType.MainBanner)
                    MovieSet.Banner = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.Banner) Then Exit For
                Next
            End If

            'clearart
            If String.IsNullOrEmpty(MovieSet.ClearArt) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.ModifierType.MainClearArt)
                    MovieSet.ClearArt = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.ClearArt) Then Exit For
                Next
            End If

            'clearlogo
            If String.IsNullOrEmpty(MovieSet.ClearLogo) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.ModifierType.MainClearLogo)
                    MovieSet.ClearLogo = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.ClearLogo) Then Exit For
                Next
            End If

            'discart
            If String.IsNullOrEmpty(MovieSet.DiscArt) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.ModifierType.MainDiscArt)
                    MovieSet.DiscArt = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.DiscArt) Then Exit For
                Next
            End If

            'fanart
            If String.IsNullOrEmpty(MovieSet.Fanart) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.ModifierType.MainFanart)
                    MovieSet.Fanart = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.Fanart) Then Exit For
                Next
            End If

            'landscape
            If String.IsNullOrEmpty(MovieSet.Landscape) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.ModifierType.MainLandscape)
                    MovieSet.Landscape = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.Landscape) Then Exit For
                Next
            End If

            'poster
            If String.IsNullOrEmpty(MovieSet.Poster) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.ModifierType.MainPoster)
                    MovieSet.Poster = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.Poster) Then Exit For
                Next
            End If

            'nfo
            If String.IsNullOrEmpty(MovieSet.Nfo) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.ModifierType.MainNFO)
                    MovieSet.Nfo = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.Nfo) Then Exit For
                Next
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        fList = Nothing
    End Sub

    Public Sub GetTVEpisodeFolderContents(ByRef Episode As Structures.DBTV)
        Dim tmpName As String = String.Empty
        Dim fName As String = String.Empty
        Dim fList As New List(Of String)
        Dim EpisodePath As String = Episode.Filename

        Try
            Try
                fList.AddRange(Directory.GetFiles(Directory.GetParent(Episode.Filename).FullName, String.Concat(Path.GetFileNameWithoutExtension(Episode.Filename), "*.*")))
            Catch
            End Try

            'episode actor thumbs
            For Each a In FileUtils.GetFilenameList.TVEpisode(Episode.Filename, Enums.ModifierType.EpisodeActorThumbs)
                Dim parDir As String = Directory.GetParent(a.Replace("<placeholder>", "placeholder")).FullName
                If Directory.Exists(parDir) Then
                    Episode.ActorThumbs.AddRange(Directory.GetFiles(parDir))
                End If
            Next

            'episode fanart
            If String.IsNullOrEmpty(Episode.FanartPath) Then
                For Each a In FileUtils.GetFilenameList.TVEpisode(EpisodePath, Enums.ModifierType.EpisodeFanart)
                    Episode.FanartPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Episode.FanartPath) Then Exit For
                Next
            End If

            'episode poster
            If String.IsNullOrEmpty(Episode.PosterPath) Then
                For Each a In FileUtils.GetFilenameList.TVEpisode(EpisodePath, Enums.ModifierType.EpisodePoster)
                    Episode.PosterPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Episode.PosterPath) Then Exit For
                Next
            End If

            'episode NFO
            If String.IsNullOrEmpty(Episode.NfoPath) Then
                For Each a In FileUtils.GetFilenameList.TVEpisode(EpisodePath, Enums.ModifierType.EpisodeNfo)
                    Episode.NfoPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Episode.NfoPath) Then Exit For
                Next
            End If

            'subtitles (external)
            For Each fFile As String In fList
                For Each ext In Master.eSettings.FileSystemValidSubtitlesExts
                    If fFile.ToLower.StartsWith(tmpName.ToLower) AndAlso fFile.ToLower.EndsWith(ext) Then
                        Dim isForced As Boolean = Path.GetFileNameWithoutExtension(fFile).ToLower.EndsWith("forced")
                        Episode.Subtitles.Add(New MediaInfo.Subtitle With {.SubsPath = fFile, .SubsType = "External", .SubsForced = isForced})
                    End If
                Next
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        fList = Nothing
    End Sub

    Public Sub GetTVSeasonFolderContents(ByRef TVDB As Structures.DBTV, ByVal sSeason As Integer)
        Dim Season As Integer = sSeason
        Dim SeasonFirstEpisodePath As String = String.Empty
        Dim SeasonPath As String = String.Empty
        Dim ShowPath As String = TVDB.ShowPath
        Dim bInside As Boolean = False
        Dim fList As New List(Of String)
        Dim fName As String = String.Empty

        TVDB.BannerPath = String.Empty
        TVDB.FanartPath = String.Empty
        TVDB.LandscapePath = String.Empty
        TVDB.PosterPath = String.Empty

        Try
            If Functions.IsSeasonDirectory(Directory.GetParent(TVDB.Filename).FullName) Then
                SeasonPath = Directory.GetParent(TVDB.Filename).FullName
                bInside = True
            Else
                SeasonPath = Directory.GetParent(TVDB.Filename).FullName
            End If

            Try
                If bInside Then
                    fList.AddRange(Directory.GetFiles(SeasonPath))
                    fList.AddRange(Directory.GetFiles(ShowPath))
                Else
                    fList.AddRange(Directory.GetFiles(SeasonPath))
                End If
            Catch
            End Try

            'get first episode of season (YAMJ need that for epsiodes without separate season folders)
            Try
                Dim dtEpisodes As New DataTable
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM episode INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE idShow = ", TVDB.ShowID, " AND Season = ", TVDB.TVSeason.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            'season banner
            If String.IsNullOrEmpty(TVDB.BannerPath) Then
                'all-seasons
                If Season = 999 Then
                    For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.AllSeasonsBanner)
                        TVDB.BannerPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                        If Not String.IsNullOrEmpty(TVDB.BannerPath) Then Exit For
                    Next
                Else
                    For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.ModifierType.SeasonBanner)
                        TVDB.BannerPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                        If Not String.IsNullOrEmpty(TVDB.BannerPath) Then Exit For
                    Next
                End If
            End If

            'season fanart
            If String.IsNullOrEmpty(TVDB.FanartPath) Then
                'all-seasons
                If Season = 999 Then
                    For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.AllSeasonsFanart)
                        TVDB.FanartPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                        If Not String.IsNullOrEmpty(TVDB.FanartPath) Then Exit For
                    Next
                Else
                    For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.ModifierType.SeasonFanart)
                        TVDB.FanartPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                        If Not String.IsNullOrEmpty(TVDB.FanartPath) Then Exit For
                    Next
                End If
            End If

            'season landscape
            If String.IsNullOrEmpty(TVDB.LandscapePath) Then
                'all-seasons
                If Season = 999 Then
                    For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.AllSeasonsLandscape)
                        TVDB.LandscapePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                        If Not String.IsNullOrEmpty(TVDB.LandscapePath) Then Exit For
                    Next
                Else
                    For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.ModifierType.SeasonLandscape)
                        TVDB.LandscapePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                        If Not String.IsNullOrEmpty(TVDB.LandscapePath) Then Exit For
                    Next
                End If
            End If

            'season poster
            If String.IsNullOrEmpty(TVDB.PosterPath) Then
                'all-seasons
                If Season = 999 Then
                    For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.AllSeasonsPoster)
                        TVDB.PosterPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                        If Not String.IsNullOrEmpty(TVDB.PosterPath) Then Exit For
                    Next
                Else
                    For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.ModifierType.SeasonPoster)
                        TVDB.PosterPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                        If Not String.IsNullOrEmpty(TVDB.PosterPath) Then Exit For
                    Next
                End If
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        fList = Nothing
    End Sub
    ''' <summary>
    ''' Check if a directory contains supporting files (nfo, poster, fanart)
    ''' </summary>
    ''' <param name="tShow">TVShowContainer object.</param>
    Public Sub GetTVShowFolderContents(ByRef tShow As Structures.DBTV, Optional ByVal ID As Long = 0)
        Dim ShowPath As String = tShow.ShowPath
        Dim efList As New List(Of String)
        Dim fList As New List(Of String)
        Dim fName As String = String.Empty

        Try
            Try
                fList.AddRange(Directory.GetFiles(tShow.ShowPath))

                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainEFanarts)
                    If Directory.Exists(a) Then
                        efList.AddRange(Directory.GetFiles(a))
                    End If
                Next
            Catch
            End Try

            'show actor thumbs
            For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainActorThumbs)
                Dim parDir As String = Directory.GetParent(a.Replace("<placeholder>", "placeholder")).FullName
                If Directory.Exists(parDir) Then
                    tShow.ActorThumbs.AddRange(Directory.GetFiles(parDir))
                End If
            Next

            'show banner
            If String.IsNullOrEmpty(tShow.BannerPath) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainBanner)
                    tShow.BannerPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.BannerPath) Then Exit For
                Next
            End If

            'show characterart
            If String.IsNullOrEmpty(tShow.CharacterArtPath) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainCharacterArt)
                    tShow.CharacterArtPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.CharacterArtPath) Then Exit For
                Next
            End If

            'show clearart
            If String.IsNullOrEmpty(tShow.ClearArtPath) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainClearArt)
                    tShow.ClearArtPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.ClearArtPath) Then Exit For
                Next
            End If

            'show clearlogo
            If String.IsNullOrEmpty(tShow.ClearLogoPath) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainClearLogo)
                    tShow.ClearLogoPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.ClearLogoPath) Then Exit For
                Next
            End If

            'extrafanart
            If String.IsNullOrEmpty(tShow.EFanartsPath) Then
                If efList.Count > 0 Then
                    tShow.EFanartsPath = efList.Item(0).ToString
                End If
            End If

            'show fanart
            If String.IsNullOrEmpty(tShow.FanartPath) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainFanart)
                    tShow.FanartPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.FanartPath) Then Exit For
                Next
            End If

            'show landscape
            If String.IsNullOrEmpty(tShow.LandscapePath) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainLandscape)
                    tShow.LandscapePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.LandscapePath) Then Exit For
                Next
            End If

            'show NFO
            If String.IsNullOrEmpty(tShow.NfoPath) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainNFO)
                    tShow.NfoPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.NfoPath) Then Exit For
                Next
            End If

            'show poster
            If String.IsNullOrEmpty(tShow.PosterPath) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainPoster)
                    tShow.PosterPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.PosterPath) Then Exit For
                Next
            End If

            'show theme
            If String.IsNullOrEmpty(tShow.ThemePath) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.ModifierType.MainTheme)
                    For Each t As String In Master.eSettings.FileSystemValidThemeExts
                        tShow.ThemePath = fList.FirstOrDefault(Function(s) s.ToLower = String.Concat(a.ToLower, t.ToLower))
                        If Not String.IsNullOrEmpty(tShow.ThemePath) Then Exit For
                    Next
                    If Not String.IsNullOrEmpty(tShow.ThemePath) Then Exit For
                Next
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        fList = Nothing
    End Sub

    Public Function IsBusy() As Boolean
        Return bwPrelim.IsBusy
    End Function

    ''' <summary>
    ''' Check if we should scan the directory.
    ''' </summary>
    ''' <param name="dInfo">Full path of the directory to check</param>
    ''' <returns>True if directory is valid, false if not.</returns>
    Public Function isValidDir(ByVal dInfo As DirectoryInfo, ByVal isTV As Boolean) As Boolean
        Try
            For Each s As String In Master.ExcludeDirs
                If dInfo.FullName.ToLower = s.ToLower Then Return False
            Next
            If (Not isTV AndAlso dInfo.Name.ToLower = "extras") OrElse _
            If(dInfo.FullName.IndexOf("\") >= 0, dInfo.FullName.Remove(0, dInfo.FullName.IndexOf("\")).Contains(":"), False) Then
                Return False
            End If
            For Each s As String In clsAdvancedSettings.GetSetting("NotValidDirIs", "extrathumbs|video_ts|bdmv|audio_ts|recycler|subs|subtitles|.trashes").Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)
                If dInfo.Name.ToLower = s Then Return False
            Next
            For Each s As String In clsAdvancedSettings.GetSetting("NotValidDirContains", "-trailer|[trailer|temporary files|(noscan)|$recycle.bin|lost+found|system volume information|sample").Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)
                If dInfo.Name.ToLower.Contains(s) Then Return False
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Return False
        End Try
        Return True 'This is the Else
    End Function

    Public Sub LoadMovie(ByVal mContainer As MovieContainer)
        Dim tmpMovieDB As New Structures.DBMovie
        Dim ToNfo As Boolean = False
        Try
            'first, lets get the contents
            GetMovieFolderContents(mContainer)

            'add filename to tmpMovie for UpdateMediaInfo
            tmpMovieDB.Filename = mContainer.Filename

            If Not String.IsNullOrEmpty(mContainer.Nfo) Then
                tmpMovieDB.Movie = NFO.LoadMovieFromNFO(mContainer.Nfo, mContainer.isSingle)
                If Not tmpMovieDB.Movie.FileInfoSpecified AndAlso Not String.IsNullOrEmpty(tmpMovieDB.Movie.Title) AndAlso Master.eSettings.MovieScraperMetaDataScan Then
                    MediaInfo.UpdateMediaInfo(tmpMovieDB)
                End If
            Else
                tmpMovieDB.Movie = NFO.LoadMovieFromNFO(mContainer.Filename, mContainer.isSingle)
                If Not tmpMovieDB.Movie.FileInfoSpecified AndAlso Not String.IsNullOrEmpty(tmpMovieDB.Movie.Title) AndAlso Master.eSettings.MovieScraperMetaDataScan Then
                    MediaInfo.UpdateMediaInfo(tmpMovieDB)
                End If
            End If

            'Year
            If String.IsNullOrEmpty(tmpMovieDB.Movie.Year) AndAlso mContainer.getYear Then
                If FileUtils.Common.isVideoTS(mContainer.Filename) Then
                    tmpMovieDB.Movie.Year = StringUtils.GetYear(Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).Name)
                ElseIf FileUtils.Common.isBDRip(mContainer.Filename) Then
                    tmpMovieDB.Movie.Year = StringUtils.GetYear(Directory.GetParent(Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).FullName).Name)
                Else
                    If mContainer.UseFolder AndAlso mContainer.isSingle Then
                        tmpMovieDB.Movie.Year = StringUtils.GetYear(Directory.GetParent(mContainer.Filename).Name)
                    Else
                        tmpMovieDB.Movie.Year = StringUtils.GetYear(Path.GetFileNameWithoutExtension(mContainer.Filename))
                    End If
                End If
            End If

            'IMDB ID
            If String.IsNullOrEmpty(tmpMovieDB.Movie.IMDBID) Then
                If FileUtils.Common.isVideoTS(mContainer.Filename) Then
                    tmpMovieDB.Movie.IMDBID = StringUtils.GetIMDBID(Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).Name)
                ElseIf FileUtils.Common.isBDRip(mContainer.Filename) Then
                    tmpMovieDB.Movie.IMDBID = StringUtils.GetIMDBID(Directory.GetParent(Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).FullName).Name)
                Else
                    If mContainer.UseFolder AndAlso mContainer.isSingle Then
                        tmpMovieDB.Movie.IMDBID = StringUtils.GetIMDBID(Directory.GetParent(mContainer.Filename).Name)
                    Else
                        tmpMovieDB.Movie.IMDBID = StringUtils.GetIMDBID(Path.GetFileNameWithoutExtension(mContainer.Filename))
                    End If
                End If
            End If

            'Title
            If String.IsNullOrEmpty(tmpMovieDB.Movie.Title) Then
                'no title so assume it's an invalid nfo, clear nfo path if exists
                mContainer.Nfo = String.Empty

                If FileUtils.Common.isVideoTS(mContainer.Filename) Then
                    tmpMovieDB.Movie.Title = StringUtils.FilterName_Movie(Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).Name, False)
                    If String.IsNullOrEmpty(tmpMovieDB.Movie.Title) Then
                        tmpMovieDB.Movie.Title = Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).Name
                    End If
                ElseIf FileUtils.Common.isBDRip(mContainer.Filename) Then
                    tmpMovieDB.Movie.Title = StringUtils.FilterName_Movie(Directory.GetParent(Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).FullName).Name, False)
                    If String.IsNullOrEmpty(tmpMovieDB.Movie.Title) Then
                        tmpMovieDB.Movie.Title = Directory.GetParent(Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).FullName).Name
                    End If
                Else
                    If mContainer.UseFolder AndAlso mContainer.isSingle Then
                        tmpMovieDB.Movie.Title = StringUtils.FilterName_Movie(Directory.GetParent(mContainer.Filename).Name, False)
                        If String.IsNullOrEmpty(tmpMovieDB.Movie.Title) Then
                            tmpMovieDB.Movie.Title = Directory.GetParent(mContainer.Filename).Name
                        End If
                    Else
                        tmpMovieDB.Movie.Title = StringUtils.FilterName_Movie(Path.GetFileNameWithoutExtension(mContainer.Filename), False)
                        If String.IsNullOrEmpty(tmpMovieDB.Movie.Title) Then
                            tmpMovieDB.Movie.Title = Path.GetFileNameWithoutExtension(mContainer.Filename)
                        End If
                    End If
                End If
            End If

            'ListTitle
            Dim tTitle As String = StringUtils.SortTokens_Movie(tmpMovieDB.Movie.Title)
            If Master.eSettings.MovieDisplayYear AndAlso Not String.IsNullOrEmpty(tmpMovieDB.Movie.Year) Then
                tmpMovieDB.ListTitle = String.Format("{0} ({1})", tTitle, tmpMovieDB.Movie.Year)
            Else
                tmpMovieDB.ListTitle = tTitle
            End If

            If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
                For Each a In FileUtils.GetFilenameList.Movie(mContainer.Filename, False, Enums.ModifierType.MainWatchedFile)
                    If Not String.IsNullOrEmpty(tmpMovieDB.Movie.PlayCount) AndAlso Not tmpMovieDB.Movie.PlayCount = "0" Then
                        If Not File.Exists(a) Then
                            Dim fs As FileStream = File.Create(a)
                            fs.Close()
                        End If
                    Else
                        If File.Exists(a) Then
                            tmpMovieDB.Movie.PlayCount = "1"
                            ToNfo = True
                        End If
                    End If
                Next
            End If

            If Not String.IsNullOrEmpty(tmpMovieDB.ListTitle) Then

                'search local actor thumb for each actor in NFO
                If tmpMovieDB.Movie.Actors.Count > 0 AndAlso mContainer.ActorThumbs.Count > 0 Then
                    For Each actor In tmpMovieDB.Movie.Actors
                        actor.ThumbPath = mContainer.ActorThumbs.FirstOrDefault(Function(s) Path.GetFileNameWithoutExtension(s).ToLower = actor.Name.Replace(" ", "_").ToLower)
                    Next
                End If

                tmpMovieDB.BannerPath = mContainer.Banner
                tmpMovieDB.ClearArtPath = mContainer.ClearArt
                tmpMovieDB.ClearLogoPath = mContainer.ClearLogo
                tmpMovieDB.DiscArtPath = mContainer.DiscArt
                tmpMovieDB.EFanartsPath = mContainer.EFanarts
                tmpMovieDB.EThumbsPath = mContainer.EThumbs
                tmpMovieDB.FanartPath = mContainer.Fanart
                tmpMovieDB.Filename = mContainer.Filename
                tmpMovieDB.LandscapePath = mContainer.Landscape
                tmpMovieDB.NfoPath = mContainer.Nfo
                tmpMovieDB.PosterPath = mContainer.Poster
                tmpMovieDB.Source = mContainer.Source
                tmpMovieDB.Subtitles = mContainer.Subtitles
                'tmpMovieDB.SubPath = mContainer.Subs
                tmpMovieDB.ThemePath = mContainer.Theme
                tmpMovieDB.TrailerPath = mContainer.Trailer
                tmpMovieDB.UseFolder = mContainer.UseFolder
                tmpMovieDB.IsSingle = mContainer.isSingle
                Dim vSource As String = APIXML.GetVideoSource(mContainer.Filename, False)
                If Not String.IsNullOrEmpty(vSource) Then
                    tmpMovieDB.VideoSource = vSource
                    tmpMovieDB.Movie.VideoSource = tmpMovieDB.VideoSource
                ElseIf String.IsNullOrEmpty(tmpMovieDB.VideoSource) AndAlso clsAdvancedSettings.GetBooleanSetting("MediaSourcesByExtension", False, "*EmberAPP") Then
                    tmpMovieDB.VideoSource = clsAdvancedSettings.GetSetting(String.Concat("MediaSourcesByExtension:", Path.GetExtension(tmpMovieDB.Filename)), String.Empty, "*EmberAPP")
                    tmpMovieDB.Movie.VideoSource = tmpMovieDB.VideoSource
                ElseIf Not String.IsNullOrEmpty(tmpMovieDB.Movie.VideoSource) Then
                    tmpMovieDB.VideoSource = tmpMovieDB.Movie.VideoSource
                End If
                tmpMovieDB.IsLock = False
                tmpMovieDB.IsMark = Master.eSettings.MovieGeneralMarkNew
                'Do the Save
                If ToNfo AndAlso Not String.IsNullOrEmpty(tmpMovieDB.NfoPath) Then
                    tmpMovieDB = Master.DB.SaveMovieToDB(tmpMovieDB, True, True, True)
                Else
                    tmpMovieDB = Master.DB.SaveMovieToDB(tmpMovieDB, True, True)
                End If

                Me.bwPrelim.ReportProgress(0, New ProgressValue With {.Type = 0, .Message = tmpMovieDB.Movie.Title})
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub LoadTVShow(ByVal DBTVShow As Structures.DBTV)
        Dim newSeasonsIndex As New List(Of Integer)
        Dim toNfo As Boolean = False

        If DBTVShow.Episodes.Count > 0 Then
            If Not TVShowPaths.ContainsKey(DBTVShow.ShowPath.ToLower) Then
                GetTVShowFolderContents(DBTVShow)
                DBTVShow.IsLock = False
                DBTVShow.IsMark = False

                If Not String.IsNullOrEmpty(DBTVShow.NfoPath) Then
                    DBTVShow.TVShow = NFO.LoadTVShowFromNFO(DBTVShow.NfoPath)
                Else
                    DBTVShow.TVShow = New MediaContainers.TVShow
                End If

                If String.IsNullOrEmpty(DBTVShow.TVShow.Title) Then
                    'no title so assume it's an invalid nfo, clear nfo path if exists
                    DBTVShow.NfoPath = String.Empty

                    DBTVShow.ListTitle = StringUtils.FilterName_TVShow(FileUtils.Common.GetDirectory(DBTVShow.ShowPath))
                    DBTVShow.TVShow.Title = StringUtils.FilterName_TVShow(FileUtils.Common.GetDirectory(DBTVShow.ShowPath), False)

                    'everything was filtered out... just set to directory name
                    If String.IsNullOrEmpty(DBTVShow.TVShow.Title) Then
                        DBTVShow.ListTitle = FileUtils.Common.GetDirectory(DBTVShow.ShowPath)
                        DBTVShow.TVShow.Title = FileUtils.Common.GetDirectory(DBTVShow.ShowPath)
                    End If
                Else
                    Dim tTitle As String = StringUtils.SortTokens_TV(DBTVShow.TVShow.Title)
                    If Master.eSettings.TVDisplayStatus AndAlso Not String.IsNullOrEmpty(DBTVShow.TVShow.Status) Then
                        DBTVShow.ListTitle = String.Format("{0} ({1})", tTitle, DBTVShow.TVShow.Status)
                    Else
                        DBTVShow.ListTitle = tTitle
                    End If
                End If

                'search local actor thumb for each actor in NFO
                If DBTVShow.TVShow.Actors.Count > 0 AndAlso DBTVShow.ActorThumbs.Count > 0 Then
                    For Each actor In DBTVShow.TVShow.Actors
                        actor.ThumbPath = DBTVShow.ActorThumbs.FirstOrDefault(Function(s) Path.GetFileNameWithoutExtension(s).ToLower = actor.Name.Replace(" ", "_").ToLower)
                    Next
                End If

                If Not String.IsNullOrEmpty(DBTVShow.ListTitle) Then
                    Master.DB.SaveTVShowToDB(DBTVShow, True, False, True)
                End If
            Else
                Dim newEpisodes As List(Of Structures.DBTV) = DBTVShow.Episodes
                DBTVShow = Master.DB.LoadTVShowFromDB(Convert.ToInt64(TVShowPaths.Item(DBTVShow.ShowPath.ToLower)), False, False)
                DBTVShow.Episodes = newEpisodes
            End If

            If DBTVShow.ShowID > -1 Then
                For Each DBTVEpisode As Structures.DBTV In DBTVShow.Episodes
                    DBTVEpisode = Master.DB.FillTVShowFromDB(DBTVEpisode, DBTVShow)
                    If Not String.IsNullOrEmpty(DBTVEpisode.Filename) Then
                        GetTVEpisodeFolderContents(DBTVEpisode)
                        DBTVEpisode.IsLock = False
                        DBTVEpisode.IsMark = False

                        For Each sEpisode As EpisodeItem In RegexGetTVEpisode(DBTVEpisode.Filename, DBTVEpisode.ShowID)
                            If sEpisode.byDate Then

                                toNfo = False

                                If Not String.IsNullOrEmpty(DBTVEpisode.NfoPath) Then
                                    DBTVEpisode.TVEp = NFO.LoadTVEpFromNFO(DBTVEpisode.NfoPath, sEpisode.Season, sEpisode.Aired)
                                    If Not DBTVEpisode.TVEp.FileInfoSpecified AndAlso Not String.IsNullOrEmpty(DBTVEpisode.TVEp.Title) AndAlso Master.eSettings.TVScraperMetaDataScan Then
                                        MediaInfo.UpdateTVMediaInfo(DBTVEpisode)
                                    End If
                                Else
                                    If Not String.IsNullOrEmpty(DBTVEpisode.TVShow.TVDB) AndAlso DBTVEpisode.ShowID >= 0 Then
                                        If String.IsNullOrEmpty(DBTVEpisode.TVEp.Aired) Then DBTVEpisode.TVEp.Aired = sEpisode.Aired
                                        If Not ModulesManager.Instance.ScrapeData_TVEpisode(DBTVEpisode, Master.DefaultOptions_TV, False) Then
                                            If Not String.IsNullOrEmpty(DBTVEpisode.TVEp.Title) Then
                                                toNfo = True

                                                'if we had info for it (based on title) and mediainfo scanning is enabled
                                                If Master.eSettings.TVScraperMetaDataScan Then
                                                    MediaInfo.UpdateTVMediaInfo(DBTVEpisode)
                                                End If
                                            End If

                                            If String.IsNullOrEmpty(DBTVEpisode.PosterPath) Then
                                                If Not String.IsNullOrEmpty(DBTVEpisode.TVEp.LocalFile) AndAlso File.Exists(DBTVEpisode.TVEp.LocalFile) Then
                                                    DBTVEpisode.TVEp.Poster.WebImage.FromFile(DBTVEpisode.TVEp.LocalFile)
                                                    If Not IsNothing(DBTVEpisode.TVEp.Poster.WebImage.Image) Then
                                                        DBTVEpisode.PosterPath = DBTVEpisode.TVEp.Poster.WebImage.SaveAsTVEpisodePoster(DBTVEpisode)
                                                    End If
                                                ElseIf Not String.IsNullOrEmpty(DBTVEpisode.TVEp.PosterURL) Then
                                                    DBTVEpisode.TVEp.Poster.WebImage.FromWeb(DBTVEpisode.TVEp.PosterURL)
                                                    If Not IsNothing(DBTVEpisode.TVEp.Poster.WebImage.Image) Then
                                                        Directory.CreateDirectory(Directory.GetParent(DBTVEpisode.TVEp.LocalFile).FullName)
                                                        DBTVEpisode.TVEp.Poster.WebImage.Save(DBTVEpisode.TVEp.LocalFile)
                                                        DBTVEpisode.PosterPath = DBTVEpisode.TVEp.Poster.WebImage.SaveAsTVEpisodePoster(DBTVEpisode)
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Else
                                        DBTVEpisode.TVEp = New MediaContainers.EpisodeDetails
                                    End If
                                End If

                                If String.IsNullOrEmpty(DBTVEpisode.TVEp.Title) Then
                                    'no title so assume it's an invalid nfo, clear nfo path if exists
                                    DBTVEpisode.NfoPath = String.Empty
                                    'set title based on episode file
                                    If Not Master.eSettings.TVEpisodeNoFilter Then DBTVEpisode.TVEp.Title = StringUtils.FilterName_TVEp(Path.GetFileNameWithoutExtension(DBTVEpisode.Filename), DBTVEpisode.TVShow.Title)
                                End If

                                'search local actor thumb for each actor in NFO
                                If DBTVEpisode.TVEp.Actors.Count > 0 AndAlso DBTVEpisode.ActorThumbs.Count > 0 Then
                                    For Each actor In DBTVEpisode.TVEp.Actors
                                        actor.ThumbPath = DBTVEpisode.ActorThumbs.FirstOrDefault(Function(s) Path.GetFileNameWithoutExtension(s).ToLower = actor.Name.Replace(" ", "_").ToLower)
                                    Next
                                End If

                                If DBTVEpisode.TVEp.Season = -1 Then DBTVEpisode.TVEp.Season = sEpisode.Season
                                If DBTVEpisode.TVEp.Episode = -1 AndAlso DBTVEpisode.Ordering = Enums.Ordering.DayOfYear Then
                                    Dim eDate As Date = DateTime.ParseExact(sEpisode.Aired, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture)
                                    DBTVEpisode.TVEp.Episode = eDate.DayOfYear
                                ElseIf DBTVEpisode.TVEp.Episode = -1 Then
                                    DBTVEpisode.TVEp.Episode = sEpisode.Episode
                                End If
                                If String.IsNullOrEmpty(DBTVEpisode.TVEp.Aired) Then DBTVEpisode.TVEp.Aired = sEpisode.Aired

                                If String.IsNullOrEmpty(DBTVEpisode.TVEp.Title) Then
                                    'nothing usable in the title after filters have runs
                                    DBTVEpisode.TVEp.Title = String.Format("{0} {1}", DBTVEpisode.TVShow.Title, DBTVEpisode.TVEp.Aired)
                                End If

                                Dim vSource As String = APIXML.GetVideoSource(DBTVEpisode.Filename, True)
                                If Not String.IsNullOrEmpty(vSource) Then
                                    DBTVEpisode.VideoSource = vSource
                                    DBTVEpisode.TVEp.VideoSource = DBTVEpisode.VideoSource
                                ElseIf String.IsNullOrEmpty(DBTVEpisode.VideoSource) AndAlso clsAdvancedSettings.GetBooleanSetting("MediaSourcesByExtension", False, "*EmberAPP") Then
                                    DBTVEpisode.VideoSource = clsAdvancedSettings.GetSetting(String.Concat("MediaSourcesByExtension:", Path.GetExtension(DBTVEpisode.Filename)), String.Empty, "*EmberAPP")
                                    DBTVEpisode.TVEp.VideoSource = DBTVEpisode.VideoSource
                                ElseIf Not String.IsNullOrEmpty(DBTVEpisode.TVEp.VideoSource) Then
                                    DBTVEpisode.VideoSource = DBTVEpisode.TVEp.VideoSource
                                End If

                                'Do the Save
                                Master.DB.SaveTVEpToDB(DBTVEpisode, True, True, True, toNfo)

                                Me.bwPrelim.ReportProgress(1, New ProgressValue With {.Type = 1, .Message = String.Format("{0}: {1}", DBTVEpisode.TVShow.Title, DBTVEpisode.TVEp.Title)})
                            Else
                                Dim iEpisode As Integer = CInt(sEpisode.Episode)
                                Dim iSeason As Integer = CInt(sEpisode.Season)
                                toNfo = False

                                If Not String.IsNullOrEmpty(DBTVEpisode.NfoPath) Then
                                    DBTVEpisode.TVEp = NFO.LoadTVEpFromNFO(DBTVEpisode.NfoPath, iSeason, iEpisode)
                                    If Not DBTVEpisode.TVEp.FileInfoSpecified AndAlso Not String.IsNullOrEmpty(DBTVEpisode.TVEp.Title) AndAlso Master.eSettings.TVScraperMetaDataScan Then
                                        MediaInfo.UpdateTVMediaInfo(DBTVEpisode)
                                    End If
                                Else
                                    If Not String.IsNullOrEmpty(DBTVEpisode.TVShow.TVDB) AndAlso DBTVEpisode.ShowID >= 0 Then
                                        If DBTVEpisode.TVEp.Season = -1 Then DBTVEpisode.TVEp.Season = iSeason
                                        If DBTVEpisode.TVEp.Episode = -1 Then DBTVEpisode.TVEp.Episode = iEpisode
                                        If Not ModulesManager.Instance.ScrapeData_TVEpisode(DBTVEpisode, Master.DefaultOptions_TV, False) Then
                                            If Not String.IsNullOrEmpty(DBTVEpisode.TVEp.Title) Then
                                                toNfo = True

                                                'if we had info for it (based on title) and mediainfo scanning is enabled
                                                If Master.eSettings.TVScraperMetaDataScan Then
                                                    MediaInfo.UpdateTVMediaInfo(DBTVEpisode)
                                                End If
                                            End If

                                            If String.IsNullOrEmpty(DBTVEpisode.PosterPath) Then
                                                If Not String.IsNullOrEmpty(DBTVEpisode.TVEp.LocalFile) AndAlso File.Exists(DBTVEpisode.TVEp.LocalFile) Then
                                                    DBTVEpisode.TVEp.Poster.WebImage.FromFile(DBTVEpisode.TVEp.LocalFile)
                                                    If Not IsNothing(DBTVEpisode.TVEp.Poster.WebImage.Image) Then
                                                        DBTVEpisode.PosterPath = DBTVEpisode.TVEp.Poster.WebImage.SaveAsTVEpisodePoster(DBTVEpisode)
                                                    End If
                                                ElseIf Not String.IsNullOrEmpty(DBTVEpisode.TVEp.PosterURL) Then
                                                    DBTVEpisode.TVEp.Poster.WebImage.FromWeb(DBTVEpisode.TVEp.PosterURL)
                                                    If Not IsNothing(DBTVEpisode.TVEp.Poster.WebImage.Image) Then
                                                        Directory.CreateDirectory(Directory.GetParent(DBTVEpisode.TVEp.LocalFile).FullName)
                                                        DBTVEpisode.TVEp.Poster.WebImage.Save(DBTVEpisode.TVEp.LocalFile)
                                                        DBTVEpisode.PosterPath = DBTVEpisode.TVEp.Poster.WebImage.SaveAsTVEpisodePoster(DBTVEpisode)
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Else
                                        DBTVEpisode.TVEp = New MediaContainers.EpisodeDetails
                                    End If
                                End If

                                If String.IsNullOrEmpty(DBTVEpisode.TVEp.Title) Then
                                    'no title so assume it's an invalid nfo, clear nfo path if exists
                                    DBTVEpisode.NfoPath = String.Empty
                                    'set title based on episode file
                                    If Not Master.eSettings.TVEpisodeNoFilter Then DBTVEpisode.TVEp.Title = StringUtils.FilterName_TVEp(Path.GetFileNameWithoutExtension(DBTVEpisode.Filename), DBTVEpisode.TVShow.Title)
                                End If

                                'search local actor thumb for each actor in NFO
                                If DBTVEpisode.TVEp.Actors.Count > 0 AndAlso DBTVEpisode.ActorThumbs.Count > 0 Then
                                    For Each actor In DBTVEpisode.TVEp.Actors
                                        actor.ThumbPath = DBTVEpisode.ActorThumbs.FirstOrDefault(Function(s) Path.GetFileNameWithoutExtension(s).ToLower = actor.Name.Replace(" ", "_").ToLower)
                                    Next
                                End If

                                If DBTVEpisode.TVEp.Season = -1 Then DBTVEpisode.TVEp.Season = iSeason
                                If DBTVEpisode.TVEp.Episode = -1 Then DBTVEpisode.TVEp.Episode = iEpisode
                                If DBTVEpisode.TVEp.SubEpisode = -1 AndAlso Not sEpisode.SubEpisode = -1 Then DBTVEpisode.TVEp.SubEpisode = sEpisode.SubEpisode

                                If String.IsNullOrEmpty(DBTVEpisode.TVEp.Title) Then
                                    'nothing usable in the title after filters have runs
                                    DBTVEpisode.TVEp.Title = String.Format("{0} S{1}E{2}{3}", DBTVEpisode.TVShow.Title, DBTVEpisode.TVEp.Season.ToString.PadLeft(2, Convert.ToChar("0")), _
                                                                       DBTVEpisode.TVEp.Episode.ToString.PadLeft(2, Convert.ToChar("0")), If(DBTVEpisode.TVEp.SubEpisodeSpecified, String.Concat(".", DBTVEpisode.TVEp.SubEpisode), String.Empty))
                                End If

                                Dim vSource As String = APIXML.GetVideoSource(DBTVEpisode.Filename, True)
                                If Not String.IsNullOrEmpty(vSource) Then
                                    DBTVEpisode.VideoSource = vSource
                                    DBTVEpisode.TVEp.VideoSource = DBTVEpisode.VideoSource
                                ElseIf String.IsNullOrEmpty(DBTVEpisode.VideoSource) AndAlso clsAdvancedSettings.GetBooleanSetting("MediaSourcesByExtension", False, "*EmberAPP") Then
                                    DBTVEpisode.VideoSource = clsAdvancedSettings.GetSetting(String.Concat("MediaSourcesByExtension:", Path.GetExtension(DBTVEpisode.Filename)), String.Empty, "*EmberAPP")
                                    DBTVEpisode.TVEp.VideoSource = DBTVEpisode.VideoSource
                                ElseIf Not String.IsNullOrEmpty(DBTVEpisode.TVEp.VideoSource) Then
                                    DBTVEpisode.VideoSource = DBTVEpisode.TVEp.VideoSource
                                End If

                                'Do the Save
                                Master.DB.SaveTVEpToDB(DBTVEpisode, True, False, True, toNfo)

                                Me.bwPrelim.ReportProgress(1, New ProgressValue With {.Type = 1, .Message = String.Format("{0}: {1}", DBTVEpisode.TVShow.Title, DBTVEpisode.TVEp.Title)})
                            End If

                            'add seasons
                            If DBTVShow.Seasons Is Nothing Then
                                DBTVShow.Seasons = New List(Of Structures.DBTV)
                            End If

                            Dim tmpSeason As Structures.DBTV = DBTVShow.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = DBTVEpisode.TVEp.Season)
                            If tmpSeason.TVSeason Is Nothing Then
                                tmpSeason = New Structures.DBTV
                                tmpSeason.Filename = DBTVEpisode.Filename
                                tmpSeason.ID = -1
                                tmpSeason.TVSeason = New MediaContainers.SeasonDetails With {.Season = DBTVEpisode.TVEp.Season}
                                tmpSeason = Master.DB.FillTVShowFromDB(tmpSeason, DBTVEpisode)
                                GetTVSeasonFolderContents(tmpSeason, tmpSeason.TVSeason.Season)
                                DBTVShow.Seasons.Add(tmpSeason)
                                newSeasonsIndex.Add(tmpSeason.TVSeason.Season)
                            End If
                        Next
                    End If
                Next
            End If

            'create the All Seasons entry if needed
            Dim tmpAllSeasons As Structures.DBTV = DBTVShow.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = 999)
            If tmpAllSeasons.TVSeason Is Nothing Then
                tmpAllSeasons = New Structures.DBTV
                tmpAllSeasons.Filename = Path.Combine(DBTVShow.ShowPath, "file.ext")
                tmpAllSeasons.ID = -1
                tmpAllSeasons.TVSeason = New MediaContainers.SeasonDetails With {.Season = 999}
                tmpAllSeasons = Master.DB.FillTVShowFromDB(tmpAllSeasons, DBTVShow)
                GetTVSeasonFolderContents(tmpAllSeasons, tmpAllSeasons.TVSeason.Season)
                DBTVShow.Seasons.Add(tmpAllSeasons)
                newSeasonsIndex.Add(tmpAllSeasons.TVSeason.Season)
            End If

            'save all new seasons to DB
            For Each newSeason As Integer In newSeasonsIndex
                Dim tSeason As Structures.DBTV = DBTVShow.Seasons.FirstOrDefault(Function(f) f.TVSeason.Season = newSeason)
                If tSeason.TVSeason IsNot Nothing Then
                    Master.DB.SaveTVSeasonToDB(tSeason, True)
                End If
            Next
        End If
    End Sub

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
                    logger.Info(String.Format("VideoInfoScanner: Found date based match {0} ({1}) [{2}]", sPath, eItem.Aired, rShow.Regexp))
                Else
                    If Not RegexGetSeasonAndEpisodeNumber(sMatch, eItem, defaultSeason) Then Continue For
                    retEpisodeItemsList.Add(eItem)
                    logger.Info(String.Format("VideoInfoScanner: Found episode match {0} (s{1}e{2}) [{3}]", sPath, eItem.Season, eItem.Episode, rShow.Regexp))
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
                                logger.Info(String.Format("VideoInfoScanner: Adding new season {0}, multipart episode {1} [{2}]", eItem.Season, eItem.Episode, rShow.Regexp))
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
                                logger.Info(String.Format("VideoInfoScanner: Adding multipart episode {0} [{1}]", eItem.Episode, Master.eSettings.TVMultiPartMatching))
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
    ''' <param name="sPath">Full path of the directory.</param>
    ''' <param name="sSource">Name of source.</param>
    ''' <param name="bUseFolder">Use the folder name for initial title? (else uses file name)</param>
    ''' <param name="bSingle">Only detect one movie from each folder?</param>
    Public Sub ScanForMovieFiles(ByVal sPath As String, ByVal sSource As String, ByVal bUseFolder As Boolean, ByVal bSingle As Boolean, ByVal bGetYear As Boolean)
        Dim di As DirectoryInfo
        Dim lFi As New List(Of FileInfo)
        Dim fList As New List(Of String)
        Dim SkipStack As Boolean = False
        Dim vtsSingle As Boolean = False
        Dim bdmvSingle As Boolean = False
        Dim tFile As String = String.Empty
        Dim autoCheck As Boolean = False

        Try

            If Directory.Exists(Path.Combine(sPath, "VIDEO_TS")) Then
                di = New DirectoryInfo(Path.Combine(sPath, "VIDEO_TS"))
                bSingle = True
            ElseIf Directory.Exists(Path.Combine(sPath, String.Concat("BDMV", Path.DirectorySeparatorChar, "STREAM"))) Then
                di = New DirectoryInfo(Path.Combine(sPath, String.Concat("BDMV", Path.DirectorySeparatorChar, "STREAM")))
                bSingle = True
            Else
                di = New DirectoryInfo(sPath)
                autoCheck = True
            End If

            Try
                lFi.AddRange(di.GetFiles)
            Catch
            End Try

            If lFi.Count > 0 Then

                If Master.eSettings.MovieRecognizeVTSExpertVTS AndAlso autoCheck Then
                    If lFi.Where(Function(s) s.Name.ToLower = "index.bdmv").Count > 0 Then
                        bdmvSingle = True
                        tFile = FileUtils.Common.GetLongestFromRip(sPath, True)
                        If Me.bwPrelim.CancellationPending Then Return
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
                        If Me.bwPrelim.CancellationPending Then Return
                    Next
                End If

                If (vtsSingle OrElse bdmvSingle) AndAlso Not String.IsNullOrEmpty(tFile) Then
                    If Not MoviePaths.Contains(StringUtils.CleanStackingMarkers(tFile.ToLower)) Then
                        If Master.eSettings.FileSystemNoStackExts.Contains(Path.GetExtension(tFile).ToLower) Then
                            MoviePaths.Add(tFile.ToLower)
                        Else
                            MoviePaths.Add(StringUtils.CleanStackingMarkers(tFile).ToLower)
                        End If
                        LoadMovie(New MovieContainer With {.Filename = tFile, .Source = sSource, .isSingle = True, .UseFolder = True})
                    End If

                Else
                    Dim HasFile As Boolean = False
                    Dim tList As IOrderedEnumerable(Of FileInfo) = lFi.Where(Function(f) Master.eSettings.FileSystemValidExts.Contains(f.Extension.ToLower) AndAlso _
                             Not Regex.IsMatch(f.Name, "[^\w\s]\s?(trailer|sample)", RegexOptions.IgnoreCase) AndAlso ((Master.eSettings.MovieSkipStackedSizeCheck AndAlso _
                            StringUtils.IsStacked(f.Name)) OrElse (Not Convert.ToInt32(Master.eSettings.MovieSkipLessThan) > 0 OrElse f.Length >= Master.eSettings.MovieSkipLessThan * 1048576))).OrderBy(Function(f) f.FullName)

                    If tList.Count > 1 AndAlso bSingle Then
                        'check if we already have a movie from this folder
                        If MoviePaths.Where(Function(f) tList.Where(Function(l) StringUtils.CleanStackingMarkers(l.FullName).ToLower = f).Count > 0).Count > 0 Then
                            HasFile = True
                        End If
                    End If

                    If Not HasFile Then
                        For Each lFile As FileInfo In tList

                            If Not MoviePaths.Contains(StringUtils.CleanStackingMarkers(lFile.FullName).ToLower) Then
                                If Master.eSettings.FileSystemNoStackExts.Contains(lFile.Extension.ToLower) Then
                                    MoviePaths.Add(lFile.FullName.ToLower)
                                    SkipStack = True
                                Else
                                    MoviePaths.Add(StringUtils.CleanStackingMarkers(lFile.FullName).ToLower)
                                End If
                                fList.Add(lFile.FullName)
                            End If
                            If bSingle AndAlso Not SkipStack Then Exit For
                            If Me.bwPrelim.CancellationPending Then Return
                        Next
                    End If

                    For Each s As String In fList
                        LoadMovie(New MovieContainer With {.Filename = s, .Source = sSource, .isSingle = bSingle, .UseFolder = If(bSingle OrElse fList.Count = 1, bUseFolder, False), .GetYear = bGetYear})
                    Next
                End If

            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        di = Nothing
        lFi = Nothing
        fList = Nothing
    End Sub

    ''' <summary>
    ''' Find all related files in a directory.
    ''' </summary>
    ''' <param name="tShow">TVShowContainer object</param>
    ''' <param name="sPath">Path of folder contianing the episodes</param>
    Public Sub ScanForTVFiles(ByRef tShow As Structures.DBTV, ByVal sPath As String)
        Dim di As New DirectoryInfo(sPath)

        For Each lFile As FileInfo In di.GetFiles.OrderBy(Function(s) s.Name)
            If Not TVEpisodePaths.Contains(lFile.FullName.ToLower) AndAlso Master.eSettings.FileSystemValidExts.Contains(lFile.Extension.ToLower) AndAlso _
                Not Regex.IsMatch(lFile.Name, "[^\w\s]\s?(trailer|sample)", RegexOptions.IgnoreCase) AndAlso _
                (Not Convert.ToInt32(Master.eSettings.TVSkipLessThan) > 0 OrElse lFile.Length >= Master.eSettings.TVSkipLessThan * 1048576) Then
                tShow.Episodes.Add(New Structures.DBTV With {.ActorThumbs = New List(Of String), .Filename = lFile.FullName, .FilenameID = -1, .Subtitles = New List(Of MediaInfo.Subtitle), .TVEp = New MediaContainers.EpisodeDetails})
            ElseIf Regex.IsMatch(lFile.Name, "[^\w\s]\s?(trailer|sample)", RegexOptions.IgnoreCase) AndAlso Master.eSettings.FileSystemValidExts.Contains(lFile.Extension.ToLower) Then
                logger.Info(String.Format("VideoInfoScanner: file {0} has been ignored (trailer or sample file)", lFile.FullName))
            End If
        Next

        di = Nothing
    End Sub

    ''' <summary>
    ''' Get all directories/movies in the parent directory
    ''' </summary>
    ''' <param name="sSource">Name of source.</param>
    ''' <param name="sPath">Path of source.</param>
    ''' <param name="bRecur">Scan directory recursively?</param>
    ''' <param name="bUseFolder">Use the folder name for initial title? (else uses file name)</param>
    ''' <param name="bSingle">Only detect one movie from each folder?</param>
    Public Sub ScanMovieSourceDir(ByVal sSource As String, ByVal sPath As String, ByVal bRecur As Boolean, ByVal bUseFolder As Boolean, ByVal bSingle As Boolean, ByVal bGetYear As Boolean, ByVal doScan As Boolean)
        If Directory.Exists(sPath) Then
            Dim sMoviePath As String = String.Empty

            Dim dInfo As New DirectoryInfo(sPath)
            Dim dList As IEnumerable(Of DirectoryInfo) = Nothing

            Try

                'check if there are any movies in the parent folder
                If doScan Then ScanForMovieFiles(sPath, sSource, bUseFolder, bSingle, bGetYear)

                If Master.eSettings.MovieScanOrderModify Then
                    Try
                        dList = dInfo.GetDirectories.Where(Function(s) (Master.eSettings.MovieGeneralIgnoreLastScan OrElse bRecur OrElse s.LastWriteTime > SourceLastScan) AndAlso isValidDir(s, False)).OrderBy(Function(d) d.LastWriteTime)
                    Catch
                    End Try
                Else
                    Try
                        dList = dInfo.GetDirectories.Where(Function(s) (Master.eSettings.MovieGeneralIgnoreLastScan OrElse bRecur OrElse s.LastWriteTime > SourceLastScan) AndAlso isValidDir(s, False)).OrderBy(Function(d) d.Name)
                    Catch
                    End Try
                End If

                For Each inDir As DirectoryInfo In dList

                    If Me.bwPrelim.CancellationPending Then Return
                    ScanForMovieFiles(inDir.FullName, sSource, bUseFolder, bSingle, bGetYear)
                    If bRecur Then
                        ScanMovieSourceDir(sSource, inDir.FullName, bRecur, bUseFolder, bSingle, bGetYear, False)
                    End If
                Next

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            dInfo = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Check if a path contains movies.
    ''' </summary>
    ''' <param name="inDir">DirectoryInfo object of directory to scan</param>
    ''' <returns>True if directory contains movie files.</returns>
    Public Function ScanMovieSubDirectory(ByVal inDir As DirectoryInfo) As Boolean
        Try

            If inDir.GetFiles.Where(Function(s) Master.eSettings.FileSystemValidExts.Contains(s.Extension.ToLower) AndAlso _
                                                      Not s.Name.ToLower.Contains("-trailer") AndAlso Not s.Name.ToLower.Contains("[trailer") AndAlso _
                                                      Not s.Name.ToLower.Contains("sample")).OrderBy(Function(s) s.Name).Count > 0 Then Return True

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Get all directories in the parent directory
    ''' </summary>
    ''' <param name="sSource">Name of source.</param>
    ''' <param name="sPath">Path of source.</param>
    Public Sub ScanTVSourceDir(ByVal sSource As String, ByVal sPath As String, ByVal sLang As String, ByVal sOrdering As Enums.Ordering, ByVal sEpisodeSorting As Enums.EpisodeSorting)
        If Directory.Exists(sPath) Then

            Dim currShowContainer As Structures.DBTV
            Dim dInfo As New DirectoryInfo(sPath)
            Dim inInfo As DirectoryInfo
            Dim inList As IEnumerable(Of DirectoryInfo) = Nothing

            'first check if user added a show folder as a source
            If (dInfo.GetDirectories.Count = 0 AndAlso dInfo.GetFiles.Count > 0) OrElse dInfo.GetDirectories.Where(Function(s) Not Functions.IsSeasonDirectory(s.FullName)).Count = 0 Then
                'only files in the folder or all folders match the season regex... assume it's a single show folder
                currShowContainer = New Structures.DBTV
                currShowContainer.ActorThumbs = New List(Of String)
                currShowContainer.Episodes = New List(Of Structures.DBTV)
                currShowContainer.EpisodeSorting = sEpisodeSorting
                currShowContainer.Language = sLang
                currShowContainer.Ordering = sOrdering
                currShowContainer.Seasons = New List(Of Structures.DBTV)
                currShowContainer.ShowID = -1
                currShowContainer.ShowPath = dInfo.FullName
                currShowContainer.Source = sSource
                currShowContainer.Subtitles = New List(Of MediaInfo.Subtitle)
                Me.ScanForTVFiles(currShowContainer, dInfo.FullName)

                If Master.eSettings.TVScanOrderModify Then
                    Try
                        inList = dInfo.GetDirectories.Where(Function(d) Functions.IsSeasonDirectory(d.FullName) AndAlso (Master.eSettings.TVGeneralIgnoreLastScan OrElse d.LastWriteTime > SourceLastScan) AndAlso isValidDir(d, True)).OrderBy(Function(d) d.LastWriteTime)
                    Catch
                    End Try
                Else
                    Try
                        inList = dInfo.GetDirectories.Where(Function(d) Functions.IsSeasonDirectory(d.FullName) AndAlso (Master.eSettings.TVGeneralIgnoreLastScan OrElse d.LastWriteTime > SourceLastScan) AndAlso isValidDir(d, True)).OrderBy(Function(d) d.Name)
                    Catch
                    End Try
                End If

                For Each sDirs As DirectoryInfo In inList
                    Me.ScanForTVFiles(currShowContainer, sDirs.FullName)
                Next

                LoadTVShow(currShowContainer)
            Else
                For Each inDir As DirectoryInfo In dInfo.GetDirectories.Where(Function(d) isValidDir(d, True)).OrderBy(Function(d) d.Name)

                    currShowContainer = New Structures.DBTV
                    currShowContainer.ActorThumbs = New List(Of String)
                    currShowContainer.Episodes = New List(Of Structures.DBTV)
                    currShowContainer.EpisodeSorting = sEpisodeSorting
                    currShowContainer.Language = sLang
                    currShowContainer.Ordering = sOrdering
                    currShowContainer.Seasons = New List(Of Structures.DBTV)
                    currShowContainer.ShowID = -1
                    currShowContainer.ShowPath = inDir.FullName
                    currShowContainer.Source = sSource
                    currShowContainer.Subtitles = New List(Of MediaInfo.Subtitle)
                    Me.ScanForTVFiles(currShowContainer, inDir.FullName)

                    inInfo = New DirectoryInfo(inDir.FullName)

                    If Master.eSettings.TVScanOrderModify Then
                        Try
                            inList = inInfo.GetDirectories.Where(Function(d) (Master.eSettings.TVGeneralIgnoreLastScan OrElse d.LastWriteTime > SourceLastScan) AndAlso isValidDir(d, True)).OrderBy(Function(d) d.LastWriteTime)
                        Catch
                        End Try
                    Else
                        Try
                            inList = inInfo.GetDirectories.Where(Function(d) (Master.eSettings.TVGeneralIgnoreLastScan OrElse d.LastWriteTime > SourceLastScan) AndAlso isValidDir(d, True)).OrderBy(Function(d) d.Name)
                        Catch
                        End Try
                    End If

                    For Each sDirs As DirectoryInfo In inList
                        Me.ScanForTVFiles(currShowContainer, sDirs.FullName)
                    Next

                    LoadTVShow(currShowContainer)
                Next

            End If

            dInfo = Nothing
            inInfo = Nothing
        End If
    End Sub

    Public Sub Start(ByVal Scan As Structures.Scans, ByVal SourceName As String, ByVal Folder As String)
        Me.bwPrelim = New System.ComponentModel.BackgroundWorker
        Me.bwPrelim.WorkerReportsProgress = True
        Me.bwPrelim.WorkerSupportsCancellation = True
        Me.bwPrelim.RunWorkerAsync(New Arguments With {.Scan = Scan, .SourceName = SourceName, .Folder = Folder})
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
                    If isValidDir(inDir, False) Then
                        If ScanMovieSubDirectory(inDir) Then Return True
                        SubDirsHaveMovies(inDir)
                    End If
                Next

            End If
            Return False
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
                    Me.MoviePaths = Master.DB.GetMoviePaths
                    Using SQLTrans As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            ScanMovieSourceDir(eSource.Name, Args.Folder, eSource.Recursive, eSource.UseFolderName, eSource.IsSingle, eSource.GetYear, True)
                        End Using
                        SQLTrans.Commit()
                    End Using
                    Exit For
                End If
            Next

            For Each eSource In Master.TVSources
                Dim tSource As String = If(eSource.Path.EndsWith(Path.DirectorySeparatorChar), eSource.Path, String.Concat(eSource.Path, Path.DirectorySeparatorChar)).ToLower.Trim
                Dim tFolder As String = If(Args.Folder.EndsWith(Path.DirectorySeparatorChar), Args.Folder, String.Concat(Args.Folder, Path.DirectorySeparatorChar)).ToLower.Trim
                If tFolder.StartsWith(tSource) Then
                    bwPrelim.ReportProgress(2, New ProgressValue With {.Type = -1, .Message = String.Empty})

                    TVShowPaths.Clear()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLcommand.CommandText = "SELECT idShow, TVShowPath FROM tvshow;"
                        Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                            While SQLreader.Read
                                TVShowPaths.Add(SQLreader("TVShowPath").ToString.ToLower, SQLreader("idShow"))
                                If Me.bwPrelim.CancellationPending Then
                                    e.Cancel = True
                                    Return
                                End If
                            End While
                        End Using
                    End Using

                    TVEpisodePaths.Clear()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLcommand.CommandText = "SELECT TVEpPath FROM TVEpPaths;"
                        Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                            While SQLreader.Read
                                TVEpisodePaths.Add(SQLreader("TVEpPath").ToString.ToLower)
                                If Me.bwPrelim.CancellationPending Then
                                    e.Cancel = True
                                    Return
                                End If
                            End While
                        End Using
                    End Using

                    If Args.Folder.ToLower = eSource.Path.ToLower Then
                        'Args.Folder is a tv show source folder -> scan the whole source
                        ScanTVSourceDir(eSource.Name, eSource.Path, eSource.Language, eSource.Ordering, eSource.EpisodeSorting)
                    Else
                        'Args.Folder is a tv show folder or a tv show subfolder -> get the tv show main path
                        Dim ShowPath As String = String.Empty
                        For Each hKey In Me.TVShowPaths.Keys
                            If String.Concat(Args.Folder.ToLower, Path.DirectorySeparatorChar).StartsWith(String.Concat(hKey.ToString.ToLower, Path.DirectorySeparatorChar)) Then
                                ShowPath = hKey.ToString
                                Exit For
                            End If
                        Next

                        If Not String.IsNullOrEmpty(ShowPath) AndAlso Directory.Exists(ShowPath) Then
                            Dim currShowContainer As New Structures.DBTV
                            currShowContainer.ActorThumbs = New List(Of String)
                            currShowContainer.Episodes = New List(Of Structures.DBTV)
                            currShowContainer.EpisodeSorting = eSource.EpisodeSorting
                            currShowContainer.Language = eSource.Language
                            currShowContainer.Ordering = eSource.Ordering
                            currShowContainer.Seasons = New List(Of Structures.DBTV)
                            currShowContainer.ShowID = -1
                            currShowContainer.ShowPath = ShowPath
                            currShowContainer.Source = eSource.Name
                            currShowContainer.Subtitles = New List(Of MediaInfo.Subtitle)

                            Dim inInfo As DirectoryInfo = New DirectoryInfo(ShowPath)
                            Dim inList As IEnumerable(Of DirectoryInfo) = Nothing
                            Try
                                inList = inInfo.GetDirectories.Where(Function(d) (Master.eSettings.TVGeneralIgnoreLastScan OrElse d.LastWriteTime > SourceLastScan) AndAlso isValidDir(d, True)).OrderBy(Function(d) d.Name)
                            Catch
                            End Try

                            For Each sDirs As DirectoryInfo In inList
                                Me.ScanForTVFiles(currShowContainer, sDirs.FullName)
                            Next

                            LoadTVShow(currShowContainer)
                        End If
                    End If
                    Exit For
                End If
            Next
        End If

        If Not Args.Scan.SpecificFolder AndAlso Args.Scan.Movies Then
            Me.MoviePaths = Master.DB.GetMoviePaths
            Using SQLTrans As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    If Not String.IsNullOrEmpty(Args.SourceName) Then
                        SQLcommand.CommandText = String.Format("SELECT ID, Name, Path, Recursive, Foldername, Single, LastScan, GetYear FROM Sources WHERE Name = ""{0}"";", Args.SourceName)
                    Else
                        SQLcommand.CommandText = "SELECT ID, Name, Path, Recursive, Foldername, Single, LastScan, GetYear FROM Sources WHERE Exclude = 0;"
                    End If

                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        Using SQLUpdatecommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLUpdatecommand.CommandText = "UPDATE sources SET LastScan = (?) WHERE ID = (?);"
                            Dim parLastScan As SQLite.SQLiteParameter = SQLUpdatecommand.Parameters.Add("parLastScan", DbType.String, 0, "LastScan")
                            Dim parID As SQLite.SQLiteParameter = SQLUpdatecommand.Parameters.Add("parID", DbType.Int32, 0, "ID")
                            While SQLreader.Read
                                Try
                                    SourceLastScan = If(DBNull.Value.Equals(SQLreader("LastScan")), DateTime.Now, Convert.ToDateTime(SQLreader("LastScan").ToString))
                                Catch ex As Exception
                                    SourceLastScan = DateTime.Now
                                End Try
                                If Convert.ToBoolean(SQLreader("Recursive")) OrElse (Master.eSettings.MovieGeneralIgnoreLastScan OrElse Directory.GetLastWriteTime(SQLreader("Path").ToString) > SourceLastScan) Then
                                    'save the scan time back to the db
                                    parLastScan.Value = DateTime.Now
                                    parID.Value = SQLreader("ID")
                                    SQLUpdatecommand.ExecuteNonQuery()
                                    Try
                                        If Master.eSettings.MovieSortBeforeScan Then
                                            FileUtils.FileSorter.SortFiles(SQLreader("Path").ToString)
                                        End If
                                    Catch ex As Exception
                                        logger.Error(New StackFrame().GetMethod().Name, ex)
                                    End Try
                                    ScanMovieSourceDir(SQLreader("Name").ToString, SQLreader("Path").ToString, Convert.ToBoolean(SQLreader("Recursive")), Convert.ToBoolean(SQLreader("Foldername")), Convert.ToBoolean(SQLreader("Single")), Convert.ToBoolean(SQLreader("GetYear")), True)
                                End If
                                If Me.bwPrelim.CancellationPending Then
                                    e.Cancel = True
                                    Return
                                End If
                            End While
                        End Using
                    End Using
                End Using
                SQLTrans.Commit()
            End Using
        End If

        If Not Args.Scan.SpecificFolder AndAlso Args.Scan.TV Then
            bwPrelim.ReportProgress(2, New ProgressValue With {.Type = -1, .Message = String.Empty})

            TVShowPaths.Clear()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = "SELECT idShow, TVShowPath FROM tvshow;"
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        TVShowPaths.Add(SQLreader("TVShowPath").ToString.ToLower, SQLreader("idShow"))
                        If Me.bwPrelim.CancellationPending Then
                            e.Cancel = True
                            Return
                        End If
                    End While
                End Using
            End Using

            TVEpisodePaths.Clear()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = "SELECT TVEpPath FROM TVEpPaths;"
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    While SQLreader.Read
                        TVEpisodePaths.Add(SQLreader("TVEpPath").ToString.ToLower)
                        If Me.bwPrelim.CancellationPending Then
                            e.Cancel = True
                            Return
                        End If
                    End While
                End Using
            End Using

            Using SQLTrans As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    If Not String.IsNullOrEmpty(Args.SourceName) Then
                        SQLcommand.CommandText = String.Format("SELECT ID, Name, Path, LastScan, Language, Ordering, Exclude, EpisodeSorting FROM TVSources WHERE Name = ""{0}"";", Args.SourceName)
                    Else
                        SQLcommand.CommandText = "SELECT ID, Name, Path, LastScan, Language, Ordering, Exclude, EpisodeSorting FROM TVSources WHERE Exclude = 0;"
                    End If

                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        Using SQLUpdatecommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            SQLUpdatecommand.CommandText = "UPDATE TVSources SET LastScan = (?) WHERE ID = (?);"
                            Dim parLastScan As SQLite.SQLiteParameter = SQLUpdatecommand.Parameters.Add("parLastScan", DbType.String, 0, "LastScan")
                            Dim parID As SQLite.SQLiteParameter = SQLUpdatecommand.Parameters.Add("parID", DbType.Int32, 0, "ID")
                            While SQLreader.Read
                                Try
                                    SourceLastScan = If(DBNull.Value.Equals(SQLreader("LastScan")), DateTime.Now, Convert.ToDateTime(SQLreader("LastScan").ToString))
                                Catch ex As Exception
                                    SourceLastScan = DateTime.Now
                                End Try
                                'save the scan time back to the db
                                parLastScan.Value = DateTime.Now
                                parID.Value = SQLreader("ID")
                                SQLUpdatecommand.ExecuteNonQuery()
                                ScanTVSourceDir(SQLreader("Name").ToString, SQLreader("Path").ToString, SQLreader("Language").ToString, DirectCast(Convert.ToInt32(SQLreader("Ordering")), Enums.Ordering), DirectCast(Convert.ToInt32(SQLreader("EpisodeSorting")), Enums.EpisodeSorting))
                                If Me.bwPrelim.CancellationPending Then
                                    e.Cancel = True
                                    Return
                                End If
                            End While
                        End Using
                    End Using
                End Using
                SQLTrans.Commit()
            End Using
        End If

        'no separate MovieSet scanning possible, so we clean MovieSets when movies were scanned
        If (Master.eSettings.MovieCleanDB AndAlso Args.Scan.Movies) OrElse (Master.eSettings.MovieSetCleanDB AndAlso Args.Scan.Movies) OrElse (Master.eSettings.TVCleanDB AndAlso Args.Scan.TV) Then
            Me.bwPrelim.ReportProgress(3, New ProgressValue With {.Type = -1, .Message = String.Empty})
            'remove any db entries that no longer exist
            Master.DB.Clean(Master.eSettings.MovieCleanDB AndAlso Args.Scan.Movies, Master.eSettings.MovieSetCleanDB AndAlso Args.Scan.MovieSets, Master.eSettings.TVCleanDB AndAlso Args.Scan.TV, Args.SourceName)
        End If

        e.Result = Args
    End Sub

    Private Sub bwPrelim_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwPrelim.ProgressChanged
        Dim tProgressValue As ProgressValue = DirectCast(e.UserState, ProgressValue)
        RaiseEvent ScannerUpdated(e.ProgressPercentage, tProgressValue.Message)

        Select Case tProgressValue.Type
            Case 0
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"newmovie", 3, Master.eLang.GetString(817, "New Movie Added"), tProgressValue.Message, Nothing}))
            Case 1
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"newep", 4, Master.eLang.GetString(818, "New Episode Added"), tProgressValue.Message, Nothing}))
        End Select
    End Sub

    Private Sub bwPrelim_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwPrelim.RunWorkerCompleted
        If Not e.Cancelled Then
            Dim Args As Arguments = DirectCast(e.Result, Arguments)
            If Args.Scan.Movies Then
                Dim params As New List(Of Object)(New Object() {False, False, False, True, Args.SourceName})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterUpdateDB_Movie, params, Nothing)
            End If
            If Args.Scan.TV Then
                Dim params As New List(Of Object)(New Object() {False, False, False, True, Args.SourceName})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterUpdateDB_TV, params, Nothing)
            End If
            RaiseEvent ScanningCompleted()
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim Folder As String
        Dim Scan As Structures.Scans
        Dim SourceName As String

#End Region 'Fields

    End Structure

    Private Structure ProgressValue

#Region "Fields"

        Dim Message As String
        Dim Type As Integer

#End Region 'Fields

    End Structure

    Public Class MovieContainer

#Region "Fields"

        Private _actorthumbs As New List(Of String)
        Private _banner As String
        Private _clearart As String
        Private _clearlogo As String
        Private _discart As String
        Private _efanarts As String
        Private _ethumbs As String
        Private _fanart As String
        Private _filename As String
        Private _getyear As Boolean
        Private _landscape As String
        Private _nfo As String
        Private _poster As String
        Private _single As Boolean
        Private _source As String
        Private _subtitles As New List(Of MediaInfo.Subtitle)
        Private _theme As String
        Private _trailer As String
        Private _usefolder As Boolean

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property ActorThumbs() As List(Of String)
            Get
                Return _actorthumbs
            End Get
            Set(ByVal value As List(Of String))
                _actorthumbs = value
            End Set
        End Property

        Public Property Banner() As String
            Get
                Return _banner
            End Get
            Set(ByVal value As String)
                _banner = value
            End Set
        End Property

        Public Property ClearArt() As String
            Get
                Return _clearart
            End Get
            Set(ByVal value As String)
                _clearart = value
            End Set
        End Property

        Public Property ClearLogo() As String
            Get
                Return _clearlogo
            End Get
            Set(ByVal value As String)
                _clearlogo = value
            End Set
        End Property

        Public Property DiscArt() As String
            Get
                Return _discart
            End Get
            Set(ByVal value As String)
                _discart = value
            End Set
        End Property

        Public Property EFanarts() As String
            Get
                Return _efanarts
            End Get
            Set(ByVal value As String)
                _efanarts = value
            End Set
        End Property

        Public Property EThumbs() As String
            Get
                Return _ethumbs
            End Get
            Set(ByVal value As String)
                _ethumbs = value
            End Set
        End Property

        Public Property Fanart() As String
            Get
                Return _fanart
            End Get
            Set(ByVal value As String)
                _fanart = value
            End Set
        End Property

        Public Property Filename() As String
            Get
                Return _filename
            End Get
            Set(ByVal value As String)
                _filename = value
            End Set
        End Property

        Public Property getYear() As Boolean
            Get
                Return _getyear
            End Get
            Set(ByVal value As Boolean)
                _getyear = value
            End Set
        End Property

        Public Property isSingle() As Boolean
            Get
                Return _single
            End Get
            Set(ByVal value As Boolean)
                _single = value
            End Set
        End Property

        Public Property Landscape() As String
            Get
                Return _landscape
            End Get
            Set(ByVal value As String)
                _landscape = value
            End Set
        End Property

        Public Property Nfo() As String
            Get
                Return _nfo
            End Get
            Set(ByVal value As String)
                _nfo = value
            End Set
        End Property

        Public Property Poster() As String
            Get
                Return _poster
            End Get
            Set(ByVal value As String)
                _poster = value
            End Set
        End Property

        Public Property Source() As String
            Get
                Return _source
            End Get
            Set(ByVal value As String)
                _source = value
            End Set
        End Property

        Public Property Subtitles() As List(Of MediaInfo.Subtitle)
            Get
                Return _subtitles
            End Get
            Set(ByVal value As List(Of MediaInfo.Subtitle))
                _subtitles = value
            End Set
        End Property

        Public Property Theme() As String
            Get
                Return _theme
            End Get
            Set(ByVal value As String)
                _theme = value
            End Set
        End Property

        Public Property Trailer() As String
            Get
                Return _trailer
            End Get
            Set(ByVal value As String)
                _trailer = value
            End Set
        End Property

        Public Property UseFolder() As Boolean
            Get
                Return _usefolder
            End Get
            Set(ByVal value As Boolean)
                _usefolder = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _actorthumbs.Clear()
            _banner = String.Empty
            _clearart = String.Empty
            _clearlogo = String.Empty
            _discart = String.Empty
            _efanarts = String.Empty
            _ethumbs = String.Empty
            _fanart = String.Empty
            _filename = String.Empty
            _getyear = True
            _landscape = String.Empty
            _nfo = String.Empty
            _poster = String.Empty
            _single = False
            _source = String.Empty
            _subtitles.Clear()
            _theme = String.Empty
            _trailer = String.Empty
            _usefolder = False
        End Sub

#End Region 'Methods

    End Class

    Public Class MovieSetContainer

#Region "Fields"

        Private _banner As String
        Private _clearart As String
        Private _clearlogo As String
        Private _discart As String
        Private _fanart As String
        Private _landscape As String
        Private _nfo As String
        Private _poster As String
        Private _setname As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Banner() As String
            Get
                Return _banner
            End Get
            Set(ByVal value As String)
                _banner = value
            End Set
        End Property

        Public Property ClearArt() As String
            Get
                Return _clearart
            End Get
            Set(ByVal value As String)
                _clearart = value
            End Set
        End Property

        Public Property ClearLogo() As String
            Get
                Return _clearlogo
            End Get
            Set(ByVal value As String)
                _clearlogo = value
            End Set
        End Property

        Public Property DiscArt() As String
            Get
                Return _discart
            End Get
            Set(ByVal value As String)
                _discart = value
            End Set
        End Property

        Public Property Fanart() As String
            Get
                Return _fanart
            End Get
            Set(ByVal value As String)
                _fanart = value
            End Set
        End Property

        Public Property Landscape() As String
            Get
                Return _landscape
            End Get
            Set(ByVal value As String)
                _landscape = value
            End Set
        End Property

        Public Property Nfo() As String
            Get
                Return _nfo
            End Get
            Set(ByVal value As String)
                _nfo = value
            End Set
        End Property

        Public Property Poster() As String
            Get
                Return _poster
            End Get
            Set(ByVal value As String)
                _poster = value
            End Set
        End Property

        Public Property SetName() As String
            Get
                Return _setname
            End Get
            Set(ByVal value As String)
                _setname = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _banner = String.Empty
            _clearart = String.Empty
            _clearlogo = String.Empty
            _discart = String.Empty
            _landscape = String.Empty
            _poster = String.Empty
            _fanart = String.Empty
            _nfo = String.Empty
            _setname = String.Empty
        End Sub

#End Region 'Methods

    End Class

    Public Class EpisodeItem

#Region "Fields"

        Private _aired As String
        Private _bydate As Boolean
        Private _episode As Integer
        Private _season As Integer
        Private _subepisode As Integer

#End Region

#Region "Constructors"

        Public Sub New()
            Me.Clear()
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
            Me._aired = String.Empty
            Me._bydate = False
            Me._episode = -1
            Me._season = -1
            Me._subepisode = -1
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class