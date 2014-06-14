﻿'################################################################################
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

    Public htTVShows As New Hashtable
    Public MoviePaths As New List(Of String)
    Public ShowPath As String = String.Empty
    Public SourceLastScan As New DateTime
    Public TVPaths As New List(Of String)

    Friend WithEvents bwPrelim As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Events"

    Public Event ScannerUpdated(ByVal iType As Integer, ByVal sText As String)

    Public Event ScanningCompleted()

#End Region 'Events

#Region "Methods"

    Public Shared Function GetTVSeasons(ByVal sPath As String, ByVal ShowID As Long, ByVal MinEp As Integer) As List(Of Seasons)
        Dim retSeason As New List(Of Seasons)
        Dim cSeason As Seasons

        For Each rShow As Settings.TVShowRegEx In Master.eSettings.TVShowRegexes
            Try
                For Each sMatch As Match In Regex.Matches(If(rShow.SeasonFromDirectory, Directory.GetParent(sPath).Name, Path.GetFileNameWithoutExtension(sPath)), rShow.SeasonRegex, RegexOptions.IgnoreCase)
                    Try
                        cSeason = New Seasons

                        If Not String.IsNullOrEmpty(sMatch.Groups("season").Value) Then
                            If IsNumeric(sMatch.Groups("season").Value) Then
                                cSeason.Season = Convert.ToInt32(sMatch.Groups("season").Value)
                            ElseIf Regex.IsMatch(sMatch.Groups("season").Value, "specials?", RegexOptions.IgnoreCase) Then
                                cSeason.Season = 0
                            Else
                                For Each sShow As Settings.TVShowRegEx In Master.eSettings.TVShowRegexes.Where(Function(r) r.SeasonFromDirectory = True)
                                    For Each sfMatch As Match In Regex.Matches(Directory.GetParent(sPath).Name, sShow.SeasonRegex, RegexOptions.IgnoreCase)
                                        If IsNumeric(sfMatch.Groups("season").Value) Then
                                            cSeason.Season = Convert.ToInt32(sfMatch.Groups("season").Value)
                                        ElseIf Regex.IsMatch(sfMatch.Groups("season").Value, "specials?", RegexOptions.IgnoreCase) Then
                                            cSeason.Season = 0
                                        Else
                                            cSeason.Season = -1
                                        End If
                                    Next
                                Next
                            End If
                        Else
                            For Each sShow As Settings.TVShowRegEx In Master.eSettings.TVShowRegexes.Where(Function(r) r.SeasonFromDirectory = True)
                                For Each sfMatch As Match In Regex.Matches(Directory.GetParent(sPath).Name, sShow.SeasonRegex, RegexOptions.IgnoreCase)
                                    If IsNumeric(sfMatch.Groups("season").Value) Then
                                        cSeason.Season = Convert.ToInt32(sfMatch.Groups("season").Value)
                                    ElseIf Regex.IsMatch(sfMatch.Groups("season").Value, "specials?", RegexOptions.IgnoreCase) Then
                                        cSeason.Season = 0
                                    Else
                                        cSeason.Season = -1
                                    End If
                                Next
                            Next
                        End If

                        Select Case rShow.EpisodeRetrieve
                            Case Settings.EpRetrieve.FromDirectory
                                For Each eMatch As Match In Regex.Matches(Directory.GetParent(sPath).Name, rShow.EpisodeRegex, RegexOptions.IgnoreCase)
                                    If Not String.IsNullOrEmpty(eMatch.Groups("episode").Value) Then cSeason.Episodes.Add(Convert.ToInt32(eMatch.Groups("episode").Value))
                                Next
                            Case Settings.EpRetrieve.FromFilename
                                For Each eMatch As Match In Regex.Matches(Path.GetFileNameWithoutExtension(sPath), rShow.EpisodeRegex, RegexOptions.IgnoreCase)
                                    If Not String.IsNullOrEmpty(eMatch.Groups("episode").Value) Then cSeason.Episodes.Add(Convert.ToInt32(eMatch.Groups("episode").Value))
                                Next
                            Case Settings.EpRetrieve.FromSeasonResult
                                If Not String.IsNullOrEmpty(sMatch.Groups("season").Value) Then
                                    For Each eMatch As Match In Regex.Matches(sMatch.Value, rShow.EpisodeRegex, RegexOptions.IgnoreCase)
                                        If Not String.IsNullOrEmpty(eMatch.Groups("episode").Value) Then cSeason.Episodes.Add(Convert.ToInt32(eMatch.Groups("episode").Value))
                                    Next
                                End If
                        End Select


                        If cSeason.Episodes.Count = 0 Then
                            cSeason.Episodes.Add(MinEp)
                            MinEp += -1
                        End If

                        retSeason.Add(cSeason)
                    Catch ex As Exception
                        logger.ErrorException(New StackFrame().GetMethod().Name, ex)
                    End Try
                Next

                If retSeason.Count > 0 Then
                    'clean entries

                    'first check if we have at least one "real" season with "real" episodes
                    If retSeason.Where(Function(s) s.Season >= 0 AndAlso s.Episodes.Where(Function(e) e >= 0).Count > 0).Count > 0 Then
                        'there is at least one season, so lets clean out all the unknown seasons or seasons with unknown episodes
                        For i As Integer = retSeason.Count - 1 To 0 Step -1
                            'remove any unknown season or seasons where all episodes are unknown
                            If retSeason(i).Season < 0 OrElse retSeason(i).Episodes.Where(Function(e) e < 0).Count = retSeason(i).Episodes.Count Then retSeason.Remove(retSeason(i))
                        Next
                    End If

                    'if we still have something left, lets use it
                    If retSeason.Count > 0 Then Return retSeason
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
                Continue For
            End Try
        Next

        'nothing found
        cSeason = New Seasons
        cSeason.Season = -1
        cSeason.Episodes.Add(MinEp)
        retSeason.Add(cSeason)

        Return retSeason
    End Function

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

    Public Sub GetTVEpisodeFolderContents(ByRef Episode As EpisodeContainer)
        Dim tmpName As String = String.Empty
        Dim fName As String = String.Empty
        Dim fList As New List(Of String)
        Dim EpisodePath As String = Episode.Filename

        Try
            Try
                fList.AddRange(Directory.GetFiles(Directory.GetParent(Episode.Filename).FullName, String.Concat(Path.GetFileNameWithoutExtension(Episode.Filename), "*.*")))
            Catch
            End Try

            'episode fanart
            If String.IsNullOrEmpty(Episode.Fanart) Then
                For Each a In FileUtils.GetFilenameList.TVEpisode(EpisodePath, Enums.TVModType.EpisodeFanart)
                    Episode.Fanart = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Episode.Fanart) Then Exit For
                Next
            End If

            'episode poster
            If String.IsNullOrEmpty(Episode.Poster) Then
                For Each a In FileUtils.GetFilenameList.TVEpisode(EpisodePath, Enums.TVModType.EpisodePoster)
                    Episode.Poster = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Episode.Poster) Then Exit For
                Next
            End If

            'episode NFO
            tmpName = Path.Combine(Directory.GetParent(Episode.Filename).FullName, Path.GetFileNameWithoutExtension(Episode.Filename))
            fName = String.Concat(tmpName, ".nfo")
            Episode.Nfo = fList.FirstOrDefault(Function(s) s.ToLower = fName.ToLower)

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try

        fList = Nothing
    End Sub

    ''' <summary>
    ''' Check if a directory contains supporting files (nfo, poster, fanart, etc)
    ''' </summary>
    ''' <param name="Movie">MovieContainer object.</param>
    Public Sub GetMovieFolderContents(ByRef Movie As MovieContainer)
        Dim currname As String = String.Empty
        Dim efList As New List(Of String)   'extrafanart list
        Dim etList As New List(Of String)   'extrathumbs list
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

                If Movie.isSingle Then
                    For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.EFanarts)
                        If Directory.Exists(a) Then
                            efList.AddRange(Directory.GetFiles(a))
                        End If
                    Next
                End If

                If Movie.isSingle Then
                    For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.EThumbs)
                        If Directory.Exists(a) Then
                            etList.AddRange(Directory.GetFiles(a))
                        End If
                    Next
                End If

                If Movie.isSingle Then
                    For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.Theme)
                        If Directory.Exists(Directory.GetParent(a).FullName) Then
                            tList.AddRange(Directory.GetFiles(Directory.GetParent(a).FullName))
                        End If
                    Next
                End If
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

                If Movie.isSingle Then
                    For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.EFanarts)
                        If Directory.Exists(a) Then
                            efList.AddRange(Directory.GetFiles(a))
                        End If
                    Next
                End If

                If Movie.isSingle Then
                    For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.EThumbs)
                        If Directory.Exists(a) Then
                            etList.AddRange(Directory.GetFiles(a))
                        End If
                    Next
                End If

                If Movie.isSingle Then
                    For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.Theme)
                        If Directory.Exists(Directory.GetParent(a).FullName) Then
                            tList.AddRange(Directory.GetFiles(Directory.GetParent(a).FullName))
                        End If
                    Next
                End If
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

                If Movie.isSingle Then
                    For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.EFanarts)
                        If Directory.Exists(a) Then
                            efList.AddRange(Directory.GetFiles(a))
                        End If
                    Next
                End If

                If Movie.isSingle Then
                    For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.EThumbs)
                        If Directory.Exists(a) Then
                            etList.AddRange(Directory.GetFiles(a))
                        End If
                    Next
                End If

                If Movie.isSingle Then
                    For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.Theme)
                        If Directory.Exists(Directory.GetParent(a).FullName) Then
                            tList.AddRange(Directory.GetFiles(Directory.GetParent(a).FullName))
                        End If
                    Next
                End If
            End If

            'banner
            If String.IsNullOrEmpty(Movie.Banner) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.Banner)
                    Movie.Banner = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.Banner) Then Exit For
                Next
            End If

            'clearart
            If String.IsNullOrEmpty(Movie.ClearArt) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.ClearArt)
                    Movie.ClearArt = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.ClearArt) Then Exit For
                Next
            End If

            'clearlogo
            If String.IsNullOrEmpty(Movie.ClearLogo) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.ClearLogo)
                    Movie.ClearLogo = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.ClearLogo) Then Exit For
                Next
            End If

            'discart
            If String.IsNullOrEmpty(Movie.DiscArt) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.DiscArt)
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
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.Fanart)
                    Movie.Fanart = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.Fanart) Then Exit For
                Next
            End If

            'landscape
            If String.IsNullOrEmpty(Movie.Landscape) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.Landscape)
                    Movie.Landscape = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.Landscape) Then Exit For
                Next
            End If

            'poster
            If String.IsNullOrEmpty(Movie.Poster) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.Poster)
                    Movie.Poster = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.Poster) Then Exit For
                Next
            End If

            'nfo
            If String.IsNullOrEmpty(Movie.Nfo) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.NFO)
                    Movie.Nfo = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(Movie.Nfo) Then Exit For
                Next
            End If

            'theme
            If String.IsNullOrEmpty(Movie.Theme) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.Theme)
                    For Each t As String In Master.eSettings.FileSystemValidThemeExts
                        Movie.Theme = tList.FirstOrDefault(Function(s) s.ToLower = (String.Concat(a.ToLower, t)))
                        If Not String.IsNullOrEmpty(Movie.Theme) Then Exit For
                    Next
                Next
            End If

            'trailer
            If String.IsNullOrEmpty(Movie.Trailer) Then
                For Each a In FileUtils.GetFilenameList.Movie(Movie.Filename, Movie.isSingle, Enums.MovieModType.Trailer)
                    For Each t As String In Master.eSettings.FileSystemValidExts
                        Movie.Trailer = fList.FirstOrDefault(Function(s) s.ToLower = (String.Concat(a.ToLower, t)))
                        If Not String.IsNullOrEmpty(Movie.Trailer) Then Exit For
                    Next
                Next
            End If

            For Each fFile As String In fList

                'subs
                If String.IsNullOrEmpty(Movie.Subs) Then
                    If Regex.IsMatch(fFile.ToLower, String.Concat("^", Regex.Escape(filePath), AdvancedSettings.GetSetting("SubtitleExtension", ".*\.(sst|srt|sub|ssa|aqt|smi|sami|jss|mpl|rt|idx|ass)$")), RegexOptions.IgnoreCase) OrElse _
                                Regex.IsMatch(fFile.ToLower, String.Concat("^", Regex.Escape(filePathStack), AdvancedSettings.GetSetting("SubtitleExtension", ".*\.(sst|srt|sub|ssa|aqt|smi|sami|jss|mpl|rt|idx|ass)$")), RegexOptions.IgnoreCase) Then
                        Movie.Subs = fFile
                        Continue For
                    End If
                End If

                If Not String.IsNullOrEmpty(Movie.Subs) Then Exit For
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try

        efList = Nothing
        etList = Nothing
        fList = Nothing
    End Sub

    ''' <summary>
    ''' Check if a directory contains supporting files (nfo, poster, fanart, etc)
    ''' </summary>
    ''' <param name="MovieSet">MovieSetContainer object.</param>
    Public Sub GetMovieSetFolderContents(ByRef MovieSet As MovieSetContainer)
        Dim fList As New List(Of String)    'all other files list
        Dim fPath As String = Master.eSettings.MovieMoviesetsPath

        Try
            'first add files to filelists
            If Not String.IsNullOrEmpty(fPath) AndAlso Directory.Exists(fPath) Then
                fList.AddRange(Directory.GetFiles(fPath))
            End If

            'banner
            If String.IsNullOrEmpty(MovieSet.Banner) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.MovieModType.Banner)
                    MovieSet.Banner = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.Banner) Then Exit For
                Next
            End If

            'clearart
            If String.IsNullOrEmpty(MovieSet.ClearArt) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.MovieModType.ClearArt)
                    MovieSet.ClearArt = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.ClearArt) Then Exit For
                Next
            End If

            'clearlogo
            If String.IsNullOrEmpty(MovieSet.ClearLogo) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.MovieModType.ClearLogo)
                    MovieSet.ClearLogo = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.ClearLogo) Then Exit For
                Next
            End If

            'discart
            If String.IsNullOrEmpty(MovieSet.DiscArt) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.MovieModType.DiscArt)
                    MovieSet.DiscArt = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.DiscArt) Then Exit For
                Next
            End If

            'fanart
            If String.IsNullOrEmpty(MovieSet.Fanart) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.MovieModType.Fanart)
                    MovieSet.Fanart = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.Fanart) Then Exit For
                Next
            End If

            'landscape
            If String.IsNullOrEmpty(MovieSet.Landscape) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.MovieModType.Landscape)
                    MovieSet.Landscape = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.Landscape) Then Exit For
                Next
            End If

            'poster
            If String.IsNullOrEmpty(MovieSet.Poster) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.MovieModType.Poster)
                    MovieSet.Poster = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.Poster) Then Exit For
                Next
            End If

            'nfo
            If String.IsNullOrEmpty(MovieSet.Nfo) Then
                For Each a In FileUtils.GetFilenameList.MovieSet(MovieSet.SetName, Enums.MovieModType.NFO)
                    MovieSet.Nfo = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(MovieSet.Nfo) Then Exit For
                Next
            End If

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try

        fList = Nothing
    End Sub

    Public Sub GetTVSeasonImages(ByRef TVDB As Structures.DBTV, ByVal sSeason As Integer)
        Dim Season As Integer = sSeason
        Dim SeasonFirstEpisodePath As String = String.Empty
        Dim SeasonPath As String = String.Empty
        Dim ShowPath As String = TVDB.ShowPath
        Dim bInside As Boolean = False
        Dim fList As New List(Of String)
        Dim fName As String = String.Empty

        TVDB.SeasonBannerPath = String.Empty
        TVDB.SeasonFanartPath = String.Empty
        TVDB.SeasonLandscapePath = String.Empty
        TVDB.SeasonPosterPath = String.Empty

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
                Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM TVEps INNER JOIN TVEpPaths ON (TVEpPaths.ID = TVEpPathid) WHERE TVShowID = ", TVDB.ShowID, " AND Season = ", TVDB.TVEp.Season, " ORDER BY Episode;"))
                If dtEpisodes.Rows.Count > 0 Then
                    SeasonFirstEpisodePath = dtEpisodes.Rows(0).Item("TVEpPath").ToString
                End If
            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            If String.IsNullOrEmpty(SeasonFirstEpisodePath) Then
                SeasonFirstEpisodePath = TVDB.Filename
            End If

            'season banner
            If String.IsNullOrEmpty(TVDB.SeasonBannerPath) Then
                For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.TVModType.SeasonBanner)
                    TVDB.SeasonBannerPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(TVDB.SeasonBannerPath) Then Exit For
                Next
            End If

            'season fanart
            If String.IsNullOrEmpty(TVDB.SeasonFanartPath) Then
                For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.TVModType.SeasonFanart)
                    TVDB.SeasonFanartPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(TVDB.SeasonFanartPath) Then Exit For
                Next
            End If

            'season landscape
            If String.IsNullOrEmpty(TVDB.SeasonLandscapePath) Then
                For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.TVModType.SeasonLandscape)
                    TVDB.SeasonLandscapePath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(TVDB.SeasonLandscapePath) Then Exit For
                Next
            End If

            'season poster
            If String.IsNullOrEmpty(TVDB.SeasonPosterPath) Then
                For Each a In FileUtils.GetFilenameList.TVSeason(ShowPath, SeasonPath, Season, SeasonFirstEpisodePath, Enums.TVModType.SeasonPoster)
                    TVDB.SeasonPosterPath = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(TVDB.SeasonPosterPath) Then Exit For
                Next
            End If

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try

        fList = Nothing
    End Sub

    ''' <summary>
    ''' Check if a directory contains supporting files (nfo, poster, fanart)
    ''' </summary>
    ''' <param name="tShow">TVShowContainer object.</param>
    Public Sub GetTVShowFolderContents(ByRef tShow As TVShowContainer, Optional ByVal ID As Long = 0)
        Dim ShowPath As String = tShow.ShowPath
        Dim efList As New List(Of String)
        Dim fList As New List(Of String)
        Dim fName As String = String.Empty

        Try
            Try
                fList.AddRange(Directory.GetFiles(tShow.ShowPath))

                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowEFanarts)
                    If Directory.Exists(a) Then
                        efList.AddRange(Directory.GetFiles(a))
                    End If
                Next
            Catch
            End Try

            'all-season banner
            If String.IsNullOrEmpty(tShow.AllSeasonsBanner) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.AllSeasonsBanner)
                    tShow.AllSeasonsBanner = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.AllSeasonsBanner) Then Exit For
                Next
            End If

            'all-season fanart
            If String.IsNullOrEmpty(tShow.AllSeasonsFanart) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.AllSeasonsFanart)
                    tShow.AllSeasonsFanart = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.AllSeasonsFanart) Then Exit For
                Next
            End If

            'all-season landscape
            If String.IsNullOrEmpty(tShow.AllSeasonsLandscape) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.AllSeasonsLandscape)
                    tShow.AllSeasonsLandscape = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.AllSeasonsLandscape) Then Exit For
                Next
            End If

            'all-season poster
            If String.IsNullOrEmpty(tShow.AllSeasonsPoster) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.AllSeasonsPoster)
                    tShow.AllSeasonsPoster = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.AllSeasonsPoster) Then Exit For
                Next
            End If

            'show banner
            If String.IsNullOrEmpty(tShow.ShowBanner) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowBanner)
                    tShow.ShowBanner = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.ShowBanner) Then Exit For
                Next
            End If

            'show characterart
            If String.IsNullOrEmpty(tShow.ShowCharacterArt) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowCharacterArt)
                    tShow.ShowCharacterArt = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.ShowCharacterArt) Then Exit For
                Next
            End If

            'show clearart
            If String.IsNullOrEmpty(tShow.ShowClearArt) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowClearArt)
                    tShow.ShowClearArt = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.ShowClearArt) Then Exit For
                Next
            End If

            'show clearlogo
            If String.IsNullOrEmpty(tShow.ShowClearLogo) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowClearLogo)
                    tShow.ShowClearLogo = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.ShowClearLogo) Then Exit For
                Next
            End If

            'extrafanart
            If String.IsNullOrEmpty(tShow.ShowEFanarts) Then
                If eflist.Count > 0 Then
                    tShow.ShowEFanarts = efList.Item(0).ToString
                End If
            End If

            'show fanart
            If String.IsNullOrEmpty(tShow.ShowFanart) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowFanart)
                    tShow.ShowFanart = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.ShowFanart) Then Exit For
                Next
            End If

            'show landscape
            If String.IsNullOrEmpty(tShow.ShowLandscape) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowLandscape)
                    tShow.ShowLandscape = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.ShowLandscape) Then Exit For
                Next
            End If

            'show NFO
            If String.IsNullOrEmpty(tShow.ShowNfo) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowNfo)
                    tShow.ShowNfo = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.ShowNfo) Then Exit For
                Next
            End If

            'show poster
            If String.IsNullOrEmpty(tShow.ShowPoster) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowPoster)
                    tShow.ShowPoster = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.ShowPoster) Then Exit For
                Next
            End If

            'show theme
            If String.IsNullOrEmpty(tShow.ShowTheme) Then
                For Each a In FileUtils.GetFilenameList.TVShow(ShowPath, Enums.TVModType.ShowTheme)
                    tShow.ShowTheme = fList.FirstOrDefault(Function(s) s.ToLower = a.ToLower)
                    If Not String.IsNullOrEmpty(tShow.ShowTheme) Then Exit For
                Next
            End If

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try

        fList = Nothing
    End Sub

    ''' <summary>
    ''' Get the full path to a trailer, if it exists.
    ''' </summary>
    ''' <param name="sPath">Full path to a movie file for which you are trying to find the accompanying trailer.</param>
    ''' <returns>Full path of trailer file.</returns>
    Public Function GetMovieTrailerPath(ByVal sPath As String) As String
        Dim tFile As String = String.Empty

        Dim parPath As String = Directory.GetParent(sPath).FullName
        Dim fileName As String = Path.Combine(parPath, StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(sPath)))
        Dim tmpNameNoStack As String = Path.Combine(parPath, Path.GetFileNameWithoutExtension(sPath))
        For Each t As String In Master.eSettings.FileSystemValidExts
            If File.Exists(String.Concat(fileName, "-trailer", t)) Then
                tFile = String.Concat(fileName, "-trailer", t)
                Exit For
            ElseIf File.Exists(String.Concat(fileName, "[trailer]", t)) Then
                tFile = String.Concat(fileName, "[trailer]", t)
                Exit For
            ElseIf File.Exists(String.Concat(tmpNameNoStack, "-trailer", t)) Then
                tFile = String.Concat(tmpNameNoStack, "-trailer", t)
                Exit For
            ElseIf File.Exists(String.Concat(tmpNameNoStack, "[trailer]", t)) Then
                tFile = String.Concat(tmpNameNoStack, "[trailer]", t)
                Exit For
            End If
        Next

        Return tFile
    End Function

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

            If (Not isTV AndAlso dInfo.Name.ToLower = "extras") OrElse _
            If(dInfo.FullName.IndexOf("\") >= 0, dInfo.FullName.Remove(0, dInfo.FullName.IndexOf("\")).Contains(":"), False) Then
                Return False
            End If
            For Each s As String In AdvancedSettings.GetSetting("NotValidDirIs", "extrathumbs|video_ts|bdmv|audio_ts|recycler|subs|subtitles|.trashes").Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)
                If dInfo.Name.ToLower = s Then Return False
            Next
            For Each s As String In AdvancedSettings.GetSetting("NotValidDirContains", "-trailer|[trailer|temporary files|(noscan)|$recycle.bin|lost+found|system volume information|sample").Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)
                If dInfo.Name.ToLower.Contains(s) Then Return False
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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

            If String.IsNullOrEmpty(tmpMovieDB.Movie.Title) Then
                'no title so assume it's an invalid nfo, clear nfo path if exists
                mContainer.Nfo = String.Empty

                If FileUtils.Common.isVideoTS(mContainer.Filename) Then
                    tmpMovieDB.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).Name)
                    tmpMovieDB.Movie.Title = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).Name, False)
                    If String.IsNullOrEmpty(tmpMovieDB.Movie.Title) Then
                        tmpMovieDB.ListTitle = Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).Name
                        tmpMovieDB.Movie.Title = Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).Name
                    End If
                ElseIf FileUtils.Common.isBDRip(mContainer.Filename) Then
                    tmpMovieDB.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).FullName).Name)
                    tmpMovieDB.Movie.Title = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).FullName).Name, False)
                    If String.IsNullOrEmpty(tmpMovieDB.Movie.Title) Then
                        tmpMovieDB.ListTitle = Directory.GetParent(Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).FullName).Name
                        tmpMovieDB.Movie.Title = Directory.GetParent(Directory.GetParent(Directory.GetParent(mContainer.Filename).FullName).FullName).Name
                    End If
                Else
                    If mContainer.UseFolder AndAlso mContainer.isSingle Then
                        tmpMovieDB.ListTitle = StringUtils.FilterName(Directory.GetParent(mContainer.Filename).Name)
                        tmpMovieDB.Movie.Title = StringUtils.FilterName(Directory.GetParent(mContainer.Filename).Name, False)
                        If String.IsNullOrEmpty(tmpMovieDB.Movie.Title) Then
                            tmpMovieDB.ListTitle = Directory.GetParent(mContainer.Filename).Name
                            tmpMovieDB.Movie.Title = Directory.GetParent(mContainer.Filename).Name
                        End If
                    Else
                        tmpMovieDB.ListTitle = StringUtils.FilterName(Path.GetFileNameWithoutExtension(mContainer.Filename))
                        tmpMovieDB.Movie.Title = StringUtils.FilterName(Path.GetFileNameWithoutExtension(mContainer.Filename), False)
                        If String.IsNullOrEmpty(tmpMovieDB.Movie.Title) Then
                            tmpMovieDB.ListTitle = Path.GetFileNameWithoutExtension(mContainer.Filename)
                            tmpMovieDB.Movie.Title = Path.GetFileNameWithoutExtension(mContainer.Filename)
                        End If
                    End If
                End If

                If String.IsNullOrEmpty(tmpMovieDB.Movie.SortTitle) Then tmpMovieDB.Movie.SortTitle = tmpMovieDB.ListTitle

            Else
                Dim tTitle As String = StringUtils.FilterTokens(tmpMovieDB.Movie.Title)
                If String.IsNullOrEmpty(tmpMovieDB.Movie.SortTitle) Then tmpMovieDB.Movie.SortTitle = tTitle
                If Master.eSettings.MovieDisplayYear AndAlso Not String.IsNullOrEmpty(tmpMovieDB.Movie.Year) Then
                    tmpMovieDB.ListTitle = String.Format("{0} ({1})", tTitle, tmpMovieDB.Movie.Year)
                Else
                    tmpMovieDB.ListTitle = StringUtils.FilterTokens(tmpMovieDB.Movie.Title)
                End If
            End If

            If String.IsNullOrEmpty(tmpMovieDB.Movie.Year) Then
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

            If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
                For Each a In FileUtils.GetFilenameList.Movie(mContainer.Filename, False, Enums.MovieModType.WatchedFile)
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
                tmpMovieDB.SubPath = mContainer.Subs
                tmpMovieDB.ThemePath = mContainer.Theme
                tmpMovieDB.TrailerPath = mContainer.Trailer
                tmpMovieDB.UseFolder = mContainer.UseFolder
                tmpMovieDB.isSingle = mContainer.isSingle
                Dim fSource As String = APIXML.GetFileSource(mContainer.Filename)
                If Not String.IsNullOrEmpty(fSource) Then
                    tmpMovieDB.FileSource = fSource
                ElseIf String.IsNullOrEmpty(tmpMovieDB.FileSource) AndAlso AdvancedSettings.GetBooleanSetting("MediaSourcesByExtension", False, "*EmberAPP") Then
                    tmpMovieDB.FileSource = AdvancedSettings.GetSetting(String.Concat("MediaSourcesByExtension:", Path.GetExtension(tmpMovieDB.Filename)), String.Empty, "*EmberAPP")
                End If
                If String.IsNullOrEmpty(tmpMovieDB.FileSource) AndAlso Not String.IsNullOrEmpty(tmpMovieDB.Movie.VideoSource) Then
                    tmpMovieDB.FileSource = tmpMovieDB.Movie.VideoSource
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
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Find all related files in a directory.
    ''' </summary>
    ''' <param name="sPath">Full path of the directory.</param>
    ''' <param name="sSource">Name of source.</param>
    ''' <param name="bUseFolder">Use the folder name for initial title? (else uses file name)</param>
    ''' <param name="bSingle">Only detect one movie from each folder?</param>
    Public Sub ScanForMovieFiles(ByVal sPath As String, ByVal sSource As String, ByVal bUseFolder As Boolean, ByVal bSingle As Boolean)
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
                        LoadMovie(New MovieContainer With {.Filename = s, .Source = sSource, .isSingle = bSingle, .UseFolder = If(bSingle OrElse fList.Count = 1, bUseFolder, False)})
                    Next
                End If

            End If
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
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
    Public Sub ScanForTVFiles(ByRef tShow As TVShowContainer, ByVal sPath As String)
        Dim di As New DirectoryInfo(sPath)
        Try
            For Each lFile As FileInfo In di.GetFiles.Where(Function(f) Not TVPaths.Contains(f.FullName.ToLower) AndAlso Master.eSettings.FileSystemValidExts.Contains(f.Extension.ToLower) AndAlso _
                    Not Regex.IsMatch(f.Name, "[^\w\s]\s?(trailer|sample)", RegexOptions.IgnoreCase) AndAlso _
                    (Not Convert.ToInt32(Master.eSettings.TVSkipLessThan) > 0 OrElse f.Length >= Master.eSettings.TVSkipLessThan * 1048576)).OrderBy(Function(s) s.Name)
                tShow.Episodes.Add(New EpisodeContainer With {.Filename = lFile.FullName, .Source = tShow.Source})
            Next

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try
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
    Public Sub ScanMovieSourceDir(ByVal sSource As String, ByVal sPath As String, ByVal bRecur As Boolean, ByVal bUseFolder As Boolean, ByVal bSingle As Boolean, ByVal doScan As Boolean)
        If Directory.Exists(sPath) Then
            Dim sMoviePath As String = String.Empty

            Dim dInfo As New DirectoryInfo(sPath)
            Dim dList As IEnumerable(Of DirectoryInfo) = Nothing

            Try

                'check if there are any movies in the parent folder
                If doScan Then ScanForMovieFiles(sPath, sSource, bUseFolder, bSingle)

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
                    ScanForMovieFiles(inDir.FullName, sSource, bUseFolder, bSingle)
                    If bRecur Then
                        ScanMovieSourceDir(sSource, inDir.FullName, bRecur, bUseFolder, bSingle, False)
                    End If
                Next

            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try

            dInfo = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Check if a path contains movies.
    ''' </summary>
    ''' <param name="inDir">DirectoryInfo object of directory to scan</param>
    ''' <returns>True if directory contains movie files.</returns>
    Public Function ScanSubs(ByVal inDir As DirectoryInfo) As Boolean
        Try

            If inDir.GetFiles.Where(Function(s) Master.eSettings.FileSystemValidExts.Contains(s.Extension.ToLower) AndAlso _
                                                      Not s.Name.ToLower.Contains("-trailer") AndAlso Not s.Name.ToLower.Contains("[trailer") AndAlso _
                                                      Not s.Name.ToLower.Contains("sample")).OrderBy(Function(s) s.Name).Count > 0 Then Return True

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Get all directories in the parent directory
    ''' </summary>
    ''' <param name="sSource">Name of source.</param>
    ''' <param name="sPath">Path of source.</param>
    Public Sub ScanTVSourceDir(ByVal sSource As String, ByVal sPath As String, Optional ByVal isInner As Boolean = False)
        If Directory.Exists(sPath) Then

            Dim currShowContainer As TVShowContainer
            Dim dInfo As New DirectoryInfo(sPath)
            Dim inInfo As DirectoryInfo
            Dim inList As IEnumerable(Of DirectoryInfo) = Nothing

            Try
                'first check if user added a show folder as a source
                If (dInfo.GetDirectories.Count = 0 AndAlso dInfo.GetFiles.Count > 0) OrElse dInfo.GetDirectories.Where(Function(s) Not Functions.IsSeasonDirectory(s.FullName)).Count = 0 Then
                    'only files in the folder or all folders match the season regex... assume it's a single show folder
                    currShowContainer = New TVShowContainer
                    currShowContainer.ShowPath = dInfo.FullName
                    currShowContainer.Source = sSource
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

                        currShowContainer = New TVShowContainer
                        currShowContainer.ShowPath = inDir.FullName
                        currShowContainer.Source = sSource
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

            Catch ex As Exception
                logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            End Try
            dInfo = Nothing
            inInfo = Nothing
        End If
    End Sub

    Public Sub Start(ByVal Scan As Structures.Scans, ByVal SourceName As String)
        Me.bwPrelim = New System.ComponentModel.BackgroundWorker
        Me.bwPrelim.WorkerReportsProgress = True
        Me.bwPrelim.WorkerSupportsCancellation = True
        Me.bwPrelim.RunWorkerAsync(New Arguments With {.Scan = Scan, .SourceName = SourceName})
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
                        If ScanSubs(inDir) Then Return True
                        SubDirsHaveMovies(inDir)
                    End If
                Next

            End If
            Return False
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            Return False
        End Try
    End Function

    Private Sub bwPrelim_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwPrelim.DoWork
        Try
            Dim Args As Arguments = DirectCast(e.Argument, Arguments)
            Master.DB.ClearNew()

            If Args.Scan.Movies Then
                Me.MoviePaths = Master.DB.GetMoviePaths
                Using SQLTrans As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        If Not String.IsNullOrEmpty(Args.SourceName) Then
                            SQLcommand.CommandText = String.Format("SELECT ID, Name, path, Recursive, Foldername, Single, LastScan FROM sources WHERE Name = ""{0}"";", Args.SourceName)
                        Else
                            SQLcommand.CommandText = "SELECT ID, Name, path, Recursive, Foldername, Single, LastScan FROM sources;"
                        End If

                        Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                            Using SQLUpdatecommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                                SQLUpdatecommand.CommandText = "UPDATE sources SET LastScan = (?) WHERE ID = (?);"
                                Dim parLastScan As SQLite.SQLiteParameter = SQLUpdatecommand.Parameters.Add("parLastScan", DbType.String, 0, "LastScan")
                                Dim parID As SQLite.SQLiteParameter = SQLUpdatecommand.Parameters.Add("parID", DbType.Int32, 0, "ID")
                                While SQLreader.Read
                                    Try
                                        SourceLastScan = If(DBNull.Value.Equals(SQLreader("LastScan")), Now, Convert.ToDateTime(SQLreader("LastScan").ToString))
                                    Catch ex As Exception
                                        SourceLastScan = Now
                                    End Try
                                    If Convert.ToBoolean(SQLreader("Recursive")) OrElse (Master.eSettings.MovieGeneralIgnoreLastScan OrElse Directory.GetLastWriteTime(SQLreader("Path").ToString) > SourceLastScan) Then
                                        'save the scan time back to the db
                                        parLastScan.Value = Now
                                        parID.Value = SQLreader("ID")
                                        SQLUpdatecommand.ExecuteNonQuery()
                                        Try
                                            If Master.eSettings.MovieSortBeforeScan Then
                                                FileUtils.FileSorter.SortFiles(SQLreader("Path").ToString)
                                            End If
                                        Catch ex As Exception
                                            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
                                        End Try
                                        ScanMovieSourceDir(SQLreader("Name").ToString, SQLreader("Path").ToString, Convert.ToBoolean(SQLreader("Recursive")), Convert.ToBoolean(SQLreader("Foldername")), Convert.ToBoolean(SQLreader("Single")), True)
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

            If Args.Scan.TV Then
                bwPrelim.ReportProgress(2, New ProgressValue With {.Type = -1, .Message = String.Empty})

                htTVShows.Clear()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLcommand.CommandText = "SELECT ID, TVShowPath FROM TVShows;"
                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        While SQLreader.Read
                            htTVShows.Add(SQLreader("TVShowPath").ToString.ToLower, SQLreader("ID"))
                            If Me.bwPrelim.CancellationPending Then
                                e.Cancel = True
                                Return
                            End If
                        End While
                    End Using
                End Using

                TVPaths.Clear()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLcommand.CommandText = "SELECT TVEpPath FROM TVEpPaths;"
                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        While SQLreader.Read
                            TVPaths.Add(SQLreader("TVEpPath").ToString.ToLower)
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
                            SQLcommand.CommandText = String.Format("SELECT ID, Name, path, LastScan FROM TVSources WHERE Name = ""{0}"";", Args.SourceName)
                        Else
                            SQLcommand.CommandText = "SELECT ID, Name, path, LastScan FROM TVSources;"
                        End If

                        Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                            Using SQLUpdatecommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                                SQLUpdatecommand.CommandText = "UPDATE TVSources SET LastScan = (?) WHERE ID = (?);"
                                Dim parLastScan As SQLite.SQLiteParameter = SQLUpdatecommand.Parameters.Add("parLastScan", DbType.String, 0, "LastScan")
                                Dim parID As SQLite.SQLiteParameter = SQLUpdatecommand.Parameters.Add("parID", DbType.Int32, 0, "ID")
                                While SQLreader.Read
                                    Try
                                        SourceLastScan = If(DBNull.Value.Equals(SQLreader("LastScan")), Now, Convert.ToDateTime(SQLreader("LastScan").ToString))
                                    Catch ex As Exception
                                        SourceLastScan = Now
                                    End Try
                                    'save the scan time back to the db
                                    parLastScan.Value = Now
                                    parID.Value = SQLreader("ID")
                                    SQLUpdatecommand.ExecuteNonQuery()
                                    ScanTVSourceDir(SQLreader("Name").ToString, SQLreader("Path").ToString)
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

            If (Master.eSettings.MovieCleanDB AndAlso Args.Scan.Movies) OrElse (Master.eSettings.TVCleanDB AndAlso Args.Scan.TV) Then
                Me.bwPrelim.ReportProgress(3, New ProgressValue With {.Type = -1, .Message = String.Empty})
                'remove any db entries that no longer exist
                Master.DB.Clean(Master.eSettings.MovieCleanDB AndAlso Args.Scan.Movies, Master.eSettings.TVCleanDB AndAlso Args.Scan.TV, Args.SourceName)
            End If

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            e.Cancel = True
        End Try
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
            RaiseEvent ScanningCompleted()
        End If
    End Sub

    Private Sub LoadTVShow(ByVal TVContainer As TVShowContainer)
        Dim tmpTVDB As New Structures.DBTV
        Dim toNfo As Boolean = False
        Dim tRes As Object
        Dim tEp As Integer = -1

        Try
            If TVContainer.Episodes.Count > 0 Then
                If Not htTVShows.ContainsKey(TVContainer.ShowPath.ToLower) Then
                    GetTVShowFolderContents(TVContainer)

                    If Not String.IsNullOrEmpty(TVContainer.ShowNfo) Then
                        tmpTVDB.TVShow = NFO.LoadTVShowFromNFO(TVContainer.ShowNfo)
                    Else
                        tmpTVDB.TVShow = New MediaContainers.TVShow
                    End If

                    If String.IsNullOrEmpty(tmpTVDB.TVShow.Title) Then
                        'no title so assume it's an invalid nfo, clear nfo path if exists
                        TVContainer.ShowNfo = String.Empty
                        tmpTVDB.TVShow.Title = StringUtils.FilterTVShowName(FileUtils.Common.GetDirectory(TVContainer.ShowPath))

                        'everything was filtered out... just set to directory name
                        If String.IsNullOrEmpty(tmpTVDB.TVShow.Title) Then tmpTVDB.TVShow.Title = FileUtils.Common.GetDirectory(TVContainer.ShowPath)
                    End If

                    tmpTVDB.ShowBannerPath = TVContainer.ShowBanner
                    tmpTVDB.ShowCharacterArtPath = TVContainer.ShowCharacterArt
                    tmpTVDB.ShowClearArtPath = TVContainer.ShowClearArt
                    tmpTVDB.ShowClearLogoPath = TVContainer.ShowClearLogo
                    tmpTVDB.ShowFanartPath = TVContainer.ShowFanart
                    tmpTVDB.ShowLandscapePath = TVContainer.ShowLandscape
                    tmpTVDB.ShowNfoPath = TVContainer.ShowNfo
                    tmpTVDB.ShowPath = TVContainer.ShowPath
                    tmpTVDB.ShowPosterPath = TVContainer.ShowPoster
                    tmpTVDB.ShowThemePath = TVContainer.ShowTheme
                    tmpTVDB.IsLockShow = False
                    tmpTVDB.IsMarkShow = False
                    tmpTVDB.Source = TVContainer.Source
                    tmpTVDB.Ordering = Master.eSettings.TVScraperOptionsOrdering
                    'get the install wizard selected language for initial scan
                    Dim ShowLang As String = AdvancedSettings.GetSetting("TVDBLanguage", String.Empty, "scraper.TVDB")
                    If Not String.IsNullOrEmpty(ShowLang) Then
                        tmpTVDB.ShowLanguage = ShowLang
                    ElseIf Not String.IsNullOrEmpty(Master.eSettings.TVGeneralLanguage) Then
                        tmpTVDB.ShowLanguage = Master.eSettings.TVGeneralLanguage
                    Else
                        tmpTVDB.ShowLanguage = "en"
                    End If

                    Master.DB.SaveTVShowToDB(tmpTVDB, True, True)

                Else
                    tmpTVDB = Master.DB.LoadTVFullShowFromDB(Convert.ToInt64(htTVShows.Item(TVContainer.ShowPath.ToLower)))
                End If
                If tmpTVDB.ShowID > -1 Then
                    For Each Episode In TVContainer.Episodes
                        If Not String.IsNullOrEmpty(Episode.Filename) Then
                            GetTVEpisodeFolderContents(Episode)

                            tmpTVDB.EpNfoPath = Episode.Nfo
                            tmpTVDB.EpPosterPath = Episode.Poster
                            tmpTVDB.Source = Episode.Source
                            tmpTVDB.IsLockEp = False
                            tmpTVDB.IsMarkEp = False
                            tmpTVDB.IsLockSeason = False
                            tmpTVDB.IsMarkSeason = False

                            Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                                SQLCommand.CommandText = String.Concat("SELECT MIN(Episode) AS MinE FROM TVEps WHERE TVShowID = ", tmpTVDB.ShowID, ";")
                                tRes = SQLCommand.ExecuteScalar
                                If Not TypeOf tRes Is DBNull Then
                                    tEp = Convert.ToInt32(tRes)
                                    If tEp > -1 Then
                                        tEp = -1
                                    Else
                                        tEp += -1
                                    End If
                                Else
                                    tEp = -1
                                End If
                            End Using

                            For Each sSeasons As Seasons In GetTVSeasons(Episode.Filename, tmpTVDB.ShowID, tEp)
                                For Each i As Integer In sSeasons.Episodes

                                    toNfo = False

                                    tmpTVDB.Filename = Episode.Filename

                                    If Not String.IsNullOrEmpty(Episode.Nfo) Then
                                        tmpTVDB.TVEp = NFO.LoadTVEpFromNFO(Episode.Nfo, sSeasons.Season, i)
                                        If Not tmpTVDB.TVEp.FileInfoSpecified AndAlso Not String.IsNullOrEmpty(tmpTVDB.TVEp.Title) AndAlso Master.eSettings.TVScraperMetaDataScan Then
                                            MediaInfo.UpdateTVMediaInfo(tmpTVDB)
                                        End If
                                    Else
                                        If Not String.IsNullOrEmpty(tmpTVDB.TVShow.ID) AndAlso tmpTVDB.ShowID >= 0 Then
                                            tmpTVDB.TVEp = ModulesManager.Instance.GetSingleEpisode(Convert.ToInt32(tmpTVDB.ShowID), tmpTVDB.TVShow.ID, sSeasons.Season, i, tmpTVDB.ShowLanguage, tmpTVDB.Ordering, Master.DefaultTVOptions)

                                            If Not String.IsNullOrEmpty(tmpTVDB.TVEp.Title) Then
                                                toNfo = True

                                                'if we had info for it (based on title) and mediainfo scanning is enabled
                                                If Master.eSettings.TVScraperMetaDataScan Then
                                                    MediaInfo.UpdateTVMediaInfo(tmpTVDB)
                                                End If
                                            End If

                                            If String.IsNullOrEmpty(tmpTVDB.EpPosterPath) Then
                                                If Not String.IsNullOrEmpty(tmpTVDB.TVEp.LocalFile) AndAlso File.Exists(tmpTVDB.TVEp.LocalFile) Then
                                                    tmpTVDB.TVEp.Poster.FromFile(tmpTVDB.TVEp.LocalFile)
                                                    If Not IsNothing(tmpTVDB.TVEp.Poster.Image) Then
                                                        tmpTVDB.EpPosterPath = tmpTVDB.TVEp.Poster.SaveAsTVEpisodePoster(tmpTVDB)
                                                    End If
                                                ElseIf Not String.IsNullOrEmpty(tmpTVDB.TVEp.PosterURL) Then
                                                    tmpTVDB.TVEp.Poster.FromWeb(tmpTVDB.TVEp.PosterURL)
                                                    If Not IsNothing(tmpTVDB.TVEp.Poster.Image) Then
                                                        Directory.CreateDirectory(Directory.GetParent(tmpTVDB.TVEp.LocalFile).FullName)
                                                        tmpTVDB.TVEp.Poster.Save(tmpTVDB.TVEp.LocalFile)
                                                        tmpTVDB.EpPosterPath = tmpTVDB.TVEp.Poster.SaveAsTVEpisodePoster(tmpTVDB)
                                                    End If
                                                End If
                                            End If
                                        Else
                                            tmpTVDB.TVEp = New MediaContainers.EpisodeDetails
                                        End If
                                    End If

                                    If String.IsNullOrEmpty(tmpTVDB.TVEp.Title) Then
                                        'no title so assume it's an invalid nfo, clear nfo path if exists
                                        Episode.Nfo = String.Empty
                                        'set title based on episode file
                                        If Not Master.eSettings.TVEpisodeNoFilter Then tmpTVDB.TVEp.Title = StringUtils.FilterTVEpName(Path.GetFileNameWithoutExtension(Episode.Filename), tmpTVDB.TVShow.Title)
                                    End If

                                    If tmpTVDB.TVEp.Season = -999 Then tmpTVDB.TVEp.Season = sSeasons.Season
                                    If tmpTVDB.TVEp.Episode = -999 Then tmpTVDB.TVEp.Episode = i

                                    If String.IsNullOrEmpty(tmpTVDB.TVEp.Title) Then
                                        'nothing usable in the title after filters have runs
                                        tmpTVDB.TVEp.Title = String.Format("{0} S{1}E{2}", tmpTVDB.TVShow.Title, tmpTVDB.TVEp.Season.ToString.PadLeft(2, Convert.ToChar("0")), tmpTVDB.TVEp.Episode.ToString.PadLeft(2, Convert.ToChar("0")))
                                    End If

                                    Me.GetTVSeasonImages(tmpTVDB, tmpTVDB.TVEp.Season)

                                    'Do the Save
                                    Master.DB.SaveTVEpToDB(tmpTVDB, True, True, True, toNfo)

                                    'Save the All Seasons entry
                                    Master.DB.SaveTVSeasonToDB(New Structures.DBTV With {.ShowID = tmpTVDB.ShowID, .SeasonBannerPath = TVContainer.AllSeasonsBanner, .SeasonFanartPath = TVContainer.AllSeasonsFanart, .SeasonLandscapePath = TVContainer.AllSeasonsLandscape, .SeasonPosterPath = TVContainer.AllSeasonsPoster, .TVEp = New MediaContainers.EpisodeDetails With {.Season = 999}}, True, True)

                                    Me.bwPrelim.ReportProgress(1, New ProgressValue With {.Type = 1, .Message = String.Format("{0}: {1}", tmpTVDB.TVShow.Title, tmpTVDB.TVEp.Title)})
                                Next
                            Next
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

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

    Public Class EpisodeContainer

#Region "Fields"

        Private _fanart As String
        Private _filename As String
        Private _nfo As String
        Private _poster As String
        Private _source As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

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

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _filename = String.Empty
            _source = String.Empty
            _poster = String.Empty
            _fanart = String.Empty
            _nfo = String.Empty
        End Sub

#End Region 'Methods

    End Class

    Public Class MovieContainer

#Region "Fields"

        Private _banner As String
        Private _clearart As String
        Private _clearlogo As String
        Private _discart As String
        Private _efanarts As String
        Private _ethumbs As String
        Private _fanart As String
        Private _filename As String
        Private _landscape As String
        Private _nfo As String
        Private _poster As String
        Private _single As Boolean
        Private _source As String
        Private _subs As String
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

        Public Property Subs() As String
            Get
                Return _subs
            End Get
            Set(ByVal value As String)
                _subs = value
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
            _banner = String.Empty
            _clearart = String.Empty
            _clearlogo = String.Empty
            _discart = String.Empty
            _filename = String.Empty
            _source = String.Empty
            _single = False
            _usefolder = False
            _landscape = String.Empty
            _poster = String.Empty
            _fanart = String.Empty
            _nfo = String.Empty
            _ethumbs = String.Empty
            _efanarts = String.Empty
            _theme = String.Empty
            _trailer = String.Empty
            _subs = String.Empty
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

    Public Class Seasons

#Region "Fields"

        Private _episodes As List(Of Integer)
        Private _season As Integer

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property Episodes() As List(Of Integer)
            Get
                Return _episodes
            End Get
            Set(ByVal value As List(Of Integer))
                _episodes = value
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

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._season = -1
            Me._episodes = New List(Of Integer)
        End Sub

#End Region 'Methods

    End Class

    Public Class TVShowContainer

#Region "Fields"

        Private _allseasonsbanner As String
        Private _allseasonsfanart As String
        Private _allseasonslandscape As String
        Private _allseasonsposter As String
        Private _episodes As New List(Of EpisodeContainer)
        Private _showbanner As String
        Private _showcharacterart As String
        Private _showclearart As String
        Private _showclearlogo As String
        Private _showefanarts As String
        Private _showfanart As String
        Private _showlandscape As String
        Private _shownfo As String
        Private _showposter As String
        Private _showpath As String
        Private _showtheme As String
        Private _source As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property AllSeasonsBanner() As String
            Get
                Return Me._allseasonsbanner
            End Get
            Set(ByVal value As String)
                Me._allseasonsbanner = value
            End Set
        End Property

        Public Property AllSeasonsFanart() As String
            Get
                Return Me._allseasonsfanart
            End Get
            Set(ByVal value As String)
                Me._allseasonsfanart = value
            End Set
        End Property

        Public Property AllSeasonsLandscape() As String
            Get
                Return Me._allseasonslandscape
            End Get
            Set(ByVal value As String)
                Me._allseasonslandscape = value
            End Set
        End Property

        Public Property AllSeasonsPoster() As String
            Get
                Return Me._allseasonsposter
            End Get
            Set(ByVal value As String)
                Me._allseasonsposter = value
            End Set
        End Property

        Public Property Episodes() As List(Of EpisodeContainer)
            Get
                Return Me._episodes
            End Get
            Set(ByVal value As List(Of EpisodeContainer))
                Me._episodes = value
            End Set
        End Property

        Public Property ShowBanner() As String
            Get
                Return Me._showbanner
            End Get
            Set(ByVal value As String)
                Me._showbanner = value
            End Set
        End Property

        Public Property ShowCharacterArt() As String
            Get
                Return Me._showcharacterart
            End Get
            Set(ByVal value As String)
                Me._showcharacterart = value
            End Set
        End Property

        Public Property ShowClearArt() As String
            Get
                Return Me._showclearart
            End Get
            Set(ByVal value As String)
                Me._showclearart = value
            End Set
        End Property

        Public Property ShowClearLogo() As String
            Get
                Return Me._showclearlogo
            End Get
            Set(ByVal value As String)
                Me._showclearlogo = value
            End Set
        End Property

        Public Property ShowEFanarts() As String
            Get
                Return _showefanarts
            End Get
            Set(ByVal value As String)
                _showefanarts = value
            End Set
        End Property

        Public Property ShowFanart() As String
            Get
                Return Me._showfanart
            End Get
            Set(ByVal value As String)
                Me._showfanart = value
            End Set
        End Property

        Public Property ShowLandscape() As String
            Get
                Return Me._showlandscape
            End Get
            Set(ByVal value As String)
                Me._showlandscape = value
            End Set
        End Property

        Public Property ShowNfo() As String
            Get
                Return Me._shownfo
            End Get
            Set(ByVal value As String)
                Me._shownfo = value
            End Set
        End Property

        Public Property ShowPoster() As String
            Get
                Return Me._showposter
            End Get
            Set(ByVal value As String)
                Me._showposter = value
            End Set
        End Property

        Public Property ShowPath() As String
            Get
                Return Me._showpath
            End Get
            Set(ByVal value As String)
                Me._showpath = value
            End Set
        End Property

        Public Property ShowTheme() As String
            Get
                Return Me._showtheme
            End Get
            Set(ByVal value As String)
                Me._showtheme = value
            End Set
        End Property

        Public Property Source() As String
            Get
                Return Me._source
            End Get
            Set(ByVal value As String)
                Me._source = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._allseasonsbanner = String.Empty
            Me._allseasonsfanart = String.Empty
            Me._allseasonslandscape = String.Empty
            Me._allseasonsposter = String.Empty
            Me._episodes.Clear()
            Me._showbanner = String.Empty
            Me._showcharacterart = String.Empty
            Me._showclearart = String.Empty
            Me._showclearlogo = String.Empty
            Me._showefanarts = String.Empty
            Me._showfanart = String.Empty
            Me._showlandscape = String.Empty
            Me._shownfo = String.Empty
            Me._showposter = String.Empty
            Me._showpath = String.Empty
            Me._showtheme = String.Empty
            Me._source = String.Empty
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class