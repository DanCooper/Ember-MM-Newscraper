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
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class Scanner

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Public _filePaths As New List(Of String)
    Public _TVShowPaths As New Hashtable

    Friend WithEvents bwPrelim As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Events"

    Public Event ProgressUpdate(ByVal eProgressValue As ProgressValue)

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property IsBusy() As Boolean
        Get
            Return bwPrelim.IsBusy
        End Get
    End Property

#End Region 'Properties

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
    ''' <param name="DBElement">Database.DBElement object</param>
    ''' <param name="bForced">Enable ALL known file naming schemas. Should only be used to search files and not to save files!</param>
    Public Sub GetFolderContents_Movie(ByRef DBElement As Database.DBElement, Optional ByVal bForced As Boolean = False)
        If Not DBElement.FileItemSpecified Then Return

        'remove all known paths
        DBElement.ActorThumbs.Clear()
        DBElement.ExtrafanartsPath = String.Empty
        DBElement.ExtrathumbsPath = String.Empty
        DBElement.ImagesContainer = New MediaContainers.ImagesContainer
        DBElement.NfoPath = String.Empty
        DBElement.Subtitles = New List(Of MediaContainers.Subtitle)
        DBElement.Theme = New MediaContainers.Theme
        DBElement.Trailer = New MediaContainers.Trailer

        'actor thumbs
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainActorThumbs, bForced)
            Dim parDir As String = Directory.GetParent(a.Replace("<placeholder>", "placeholder")).FullName
            If Directory.Exists(parDir) Then
                DBElement.ActorThumbs.AddRange(Directory.GetFiles(parDir))
            End If
        Next

        'banner
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainBanner, bForced)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainBanner)
                Exit For
            End If
        Next

        'clearart
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainClearArt, bForced)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainClearArt)
                Exit For
            End If
        Next

        'clearlogo
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainClearLogo, bForced)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainClearLogo)
                Exit For
            End If
        Next

        'discart
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainDiscArt, bForced)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainDiscArt)
                Exit For
            End If
        Next

        'extrafanarts
        Dim efList As New List(Of String)
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainExtrafanarts, bForced)
            If Directory.Exists(a) Then
                Try
                    efList.AddRange(Directory.GetFiles(a, "*.jpg"))
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
                If efList.Count > 0 Then Exit For 'scan only one path to prevent image dublicates
            End If
        Next
        For Each ePath In efList
            DBElement.ImagesContainer.Extrafanarts.Add(New MediaContainers.Image With {.LocalFilePath = ePath})
        Next
        If DBElement.ImagesContainer.Extrafanarts.Count > 0 Then
            DBElement.ExtrafanartsPath = Directory.GetParent(efList.Item(0)).FullName
        End If

        'extrathumbs
        Dim etList As New List(Of String)
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainExtrathumbs, bForced)
            If Directory.Exists(a) Then
                Try
                    etList.AddRange(Directory.GetFiles(a, "*.jpg"))
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
                If etList.Count > 0 Then Exit For 'scan only one path to prevent image dublicates
            End If
        Next
        For Each ePath In etList
            DBElement.ImagesContainer.Extrathumbs.Add(New MediaContainers.Image With {.LocalFilePath = ePath})
        Next
        If DBElement.ImagesContainer.Extrathumbs.Count > 0 Then
            DBElement.ExtrathumbsPath = Directory.GetParent(etList.Item(0)).FullName
        End If

        'fanart
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainFanart, bForced)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainFanart)
                Exit For
            End If
        Next

        'keyart
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainKeyArt, bForced)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainKeyArt)
                Exit For
            End If
        Next

        'landscape
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainLandscape, bForced)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainLandscape)
                Exit For
            End If
        Next

        'nfo
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainNFO, bForced)
            If File.Exists(a) Then
                DBElement.NfoPath = a
                Exit For
            End If
        Next

        'poster
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainPoster, bForced)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainPoster)
                Exit For
            End If
        Next

        'subtitles (external)
        Dim sList As New List(Of String)
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainSubtitle, bForced)
            If Directory.Exists(a) Then
                Try
                    sList.AddRange(Directory.GetFiles(a, String.Concat(Path.GetFileNameWithoutExtension(DBElement.FileItem.FirstPathFromStack), "*")))
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            End If
        Next
        For Each fFile As String In sList
            For Each ext In Master.eSettings.Options.FileSystem.ValidSubtitleExtensions
                If fFile.ToLower.EndsWith(ext) Then
                    Dim isForced As Boolean = Path.GetFileNameWithoutExtension(fFile).ToLower.EndsWith("forced")
                    DBElement.Subtitles.Add(New MediaContainers.Subtitle With {.Path = fFile, .Forced = isForced})
                End If
            Next
        Next

        'theme
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainTheme, bForced)
            For Each ext As String In Master.eSettings.Options.FileSystem.ValidThemeExtensions
                If File.Exists(String.Concat(a, ext)) Then
                    DBElement.Theme.LocalFilePath = String.Concat(a, ext)
                    Exit For
                End If
            Next
            If DBElement.Theme.LocalFilePathSpecified Then Exit For
        Next

        'trailer
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainTrailer, bForced)
            For Each ext As String In Master.eSettings.Options.FileSystem.ValidVideoExtensions
                If File.Exists(String.Concat(a, ext)) Then
                    DBElement.Trailer.LocalFilePath = String.Concat(a, ext)
                    Exit For
                End If
            Next
            If DBElement.Trailer.LocalFilePathSpecified Then Exit For
        Next
    End Sub

    ''' <summary>
    ''' Check if a directory contains supporting files (nfo, poster, fanart, etc)
    ''' </summary>
    ''' <param name="DBElement">MovieSetContainer object.</param>
    Public Sub GetFolderContents_MovieSet(ByRef DBElement As Database.DBElement)
        'remove all known paths
        DBElement.ImagesContainer = New MediaContainers.ImagesContainer
        DBElement.NfoPath = String.Empty

        'banner
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainBanner)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainBanner)
                Exit For
            End If
        Next

        'clearart
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainClearArt)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainClearArt)
                Exit For
            End If
        Next

        'clearlogo
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainClearLogo)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainClearLogo)
                Exit For
            End If
        Next

        'discart
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainDiscArt)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainDiscArt)
                Exit For
            End If
        Next

        'fanart
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainFanart)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainFanart)
                Exit For
            End If
        Next

        'keyart
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainKeyArt)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainKeyArt)
                Exit For
            End If
        Next

        'landscape
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainLandscape)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainLandscape)
                Exit For
            End If
        Next

        'nfo
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainNFO)
            If File.Exists(a) Then
                DBElement.NfoPath = a
                Exit For
            End If
        Next

        'poster
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainPoster)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainPoster)
                Exit For
            End If
        Next
    End Sub

    Public Sub GetFolderContents_TVEpisode(ByRef DBElement As Database.DBElement)
        If Not DBElement.FileItemSpecified Then Return

        'remove all known paths
        DBElement.ActorThumbs.Clear()
        DBElement.ImagesContainer = New MediaContainers.ImagesContainer
        DBElement.NfoPath = String.Empty
        DBElement.Subtitles = New List(Of MediaContainers.Subtitle)

        'actor thumbs
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.EpisodeActorThumbs)
            Dim parDir As String = Directory.GetParent(a.Replace("<placeholder>", "placeholder")).FullName
            If Directory.Exists(parDir) Then
                Try
                    DBElement.ActorThumbs.AddRange(Directory.GetFiles(parDir))
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            End If
        Next

        'fanart
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.EpisodeFanart)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.EpisodeFanart)
                Exit For
            End If
        Next

        'nfo
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.EpisodeNFO)
            If File.Exists(a) Then
                DBElement.NfoPath = a
                Exit For
            End If
        Next

        'poster
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.EpisodePoster)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.EpisodePoster)
                Exit For
            End If
        Next

        'subtitles (external)
        Dim sList As New List(Of String)
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.EpisodeSubtitle)
            If Directory.Exists(a) Then
                Try
                    sList.AddRange(Directory.GetFiles(a, String.Concat(Path.GetFileNameWithoutExtension(DBElement.FileItem.FirstPathFromStack), "*")))
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            End If
        Next
        For Each fFile As String In sList
            For Each ext In Master.eSettings.Options.FileSystem.ValidSubtitleExtensions
                If fFile.ToLower.EndsWith(ext) Then
                    Dim isForced As Boolean = Path.GetFileNameWithoutExtension(fFile).ToLower.EndsWith("forced")
                    DBElement.Subtitles.Add(New MediaContainers.Subtitle With {.Path = fFile, .Forced = isForced})
                End If
            Next
        Next
    End Sub

    Public Sub GetFolderContents_TVSeason(ByRef DBElement As Database.DBElement)
        Dim bIsAllSeasons As Boolean = DBElement.MainDetails.Season_IsAllSeasons

        'remove all known paths
        DBElement.ImagesContainer = New MediaContainers.ImagesContainer

        'banner
        If bIsAllSeasons Then
            'all-seasons
            For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.AllSeasonsBanner)
                If File.Exists(a) Then
                    DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.AllSeasonsBanner)
                    Exit For
                End If
            Next
        Else
            For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.SeasonBanner)
                If File.Exists(a) Then
                    DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.AllSeasonsBanner)
                    Exit For
                End If
            Next
        End If

        'fanart 
        If bIsAllSeasons Then
            'all-seasons
            For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.AllSeasonsFanart)
                If File.Exists(a) Then
                    DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.AllSeasonsFanart)
                    Exit For
                End If
            Next
        Else
            For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.SeasonFanart)
                If File.Exists(a) Then
                    DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.SeasonFanart)
                    Exit For
                End If
            Next
        End If

        'landscape
        If bIsAllSeasons Then
            'all-seasons
            For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.AllSeasonsLandscape)
                If File.Exists(a) Then
                    DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.AllSeasonsLandscape)
                    Exit For
                End If
            Next
        Else
            For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.SeasonLandscape)
                If File.Exists(a) Then
                    DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.SeasonLandscape)
                    Exit For
                End If
            Next
        End If

        'poster
        If bIsAllSeasons Then
            'all-seasons
            For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.AllSeasonsPoster)
                If File.Exists(a) Then
                    DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.AllSeasonsPoster)
                    Exit For
                End If
            Next
        Else
            For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.SeasonPoster)
                If File.Exists(a) Then
                    DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.SeasonPoster)
                    Exit For
                End If
            Next
        End If
    End Sub
    ''' <summary>
    ''' Check if a directory contains supporting files (nfo, poster, fanart)
    ''' </summary>
    ''' <param name="DBElement">TVShowContainer object.</param>
    Public Sub GetFolderContents_TVShow(ByRef DBElement As Database.DBElement)

        'remove all known paths
        DBElement.ActorThumbs.Clear()
        DBElement.ExtrafanartsPath = String.Empty
        DBElement.ImagesContainer = New MediaContainers.ImagesContainer
        DBElement.NfoPath = String.Empty
        DBElement.Theme = New MediaContainers.Theme

        'actor thumbs
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainActorThumbs)
            Dim parDir As String = Directory.GetParent(a.Replace("<placeholder>", "placeholder")).FullName
            If Directory.Exists(parDir) Then
                Try
                    DBElement.ActorThumbs.AddRange(Directory.GetFiles(parDir))
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            End If
        Next

        'banner
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainBanner)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainBanner)
                Exit For
            End If
        Next

        'characterart
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainCharacterArt)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainCharacterArt)
                Exit For
            End If
        Next

        'clearart
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainClearArt)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainClearArt)
                Exit For
            End If
        Next

        'clearlogo
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainClearLogo)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainClearLogo)
                Exit For
            End If
        Next

        'extrafanarts
        Dim efList As New List(Of String)
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainExtrafanarts)
            If Directory.Exists(a) Then
                Try
                    efList.AddRange(Directory.GetFiles(a, "*.jpg"))
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
                If efList.Count > 0 Then Exit For 'scan only one path to prevent image dublicates
            End If
        Next
        For Each ePath In efList
            DBElement.ImagesContainer.Extrafanarts.Add(New MediaContainers.Image With {.LocalFilePath = ePath})
        Next
        If DBElement.ImagesContainer.Extrafanarts.Count > 0 Then
            DBElement.ExtrafanartsPath = Directory.GetParent(efList.Item(0)).FullName
        End If

        'fanart
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainFanart)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainFanart)
                Exit For
            End If
        Next

        'keyart
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainKeyArt)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainKeyArt)
                Exit For
            End If
        Next

        'landscape
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainLandscape)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainLandscape)
                Exit For
            End If
        Next

        'nfo
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainNFO)
            If File.Exists(a) Then
                DBElement.NfoPath = a
                Exit For
            End If
        Next

        'poster
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainPoster)
            If File.Exists(a) Then
                DBElement.ImagesContainer.SetImageByType(New MediaContainers.Image With {.LocalFilePath = a}, Enums.ModifierType.MainPoster)
                Exit For
            End If
        Next

        'theme
        For Each a In FileUtils.FileNames.GetFileNames(DBElement, Enums.ModifierType.MainTheme)
            For Each ext As String In Master.eSettings.Options.FileSystem.ValidThemeExtensions
                If File.Exists(String.Concat(a, ext)) Then
                    DBElement.Theme.LocalFilePath = String.Concat(a, ext)
                    Exit For
                End If
            Next
            If DBElement.Theme.LocalFilePathSpecified Then Exit For
        Next
    End Sub

    Public Sub Load_Movie(ByRef dbElement As Database.DBElement, ByVal batchMode As Boolean)
        Dim ToNfo As Boolean = False

        GetFolderContents_Movie(dbElement)

        If dbElement.NfoPathSpecified Then
            dbElement.MainDetails = NFO.LoadFromNFO_Movie(dbElement.NfoPath, dbElement.IsSingle)
            If Not dbElement.MainDetails.FileInfoSpecified AndAlso dbElement.MainDetails.TitleSpecified AndAlso Master.eSettings.Movie.DataSettings.MetadataScan.Enabled Then
                MetaData.UpdateFileInfo(dbElement)
            End If
        Else
            dbElement.MainDetails = NFO.LoadFromNFO_Movie(dbElement.FileItem.FirstPathFromStack, dbElement.IsSingle)
            If Not dbElement.MainDetails.FileInfoSpecified AndAlso dbElement.MainDetails.TitleSpecified AndAlso Master.eSettings.Movie.DataSettings.MetadataScan.Enabled Then
                MetaData.UpdateFileInfo(dbElement)
            End If
        End If

        'Premiered
        If Not dbElement.MainDetails.PremieredSpecified AndAlso dbElement.Source.GetYear Then
            dbElement.MainDetails.Premiered = StringUtils.FilterYearFromPath_Movie(dbElement.FileItem, dbElement.IsSingle, dbElement.Source.UseFolderName).ToString
        End If

        'IMDB ID
        If Not dbElement.MainDetails.UniqueIDs.IMDbIdSpecified Then
            dbElement.MainDetails.UniqueIDs.IMDbId = StringUtils.GetIMDBIDFromString(dbElement.FileItem.FirstPathFromStack, True)
        End If

        'Title
        If Not dbElement.MainDetails.TitleSpecified Then
            'no title so assume it's an invalid nfo, clear nfo path if exists
            dbElement.NfoPath = String.Empty
            dbElement.MainDetails.Title = StringUtils.FilterTitleFromPath_Movie(dbElement.FileItem, dbElement.IsSingle, dbElement.Source.UseFolderName)
        End If

        If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
            For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.MainWatchedFile)
                If dbElement.MainDetails.PlayCountSpecified Then
                    If Not File.Exists(a) Then
                        Dim fs As FileStream = File.Create(a)
                        fs.Close()
                    End If
                Else
                    If File.Exists(a) Then
                        dbElement.MainDetails.PlayCount = 1
                        ToNfo = True
                    End If
                End If
            Next
        End If

        If dbElement.MainDetails.TitleSpecified Then
            'search local actor thumb for each actor in NFO
            If dbElement.MainDetails.ActorsSpecified AndAlso dbElement.ActorThumbsSpecified Then
                For Each actor In dbElement.MainDetails.Actors
                    actor.LocalFilePath = dbElement.ActorThumbs.FirstOrDefault(Function(s) Path.GetFileNameWithoutExtension(s).ToLower = actor.Name.Replace(" ", "_").ToLower)
                Next
            End If

            'Language
            If dbElement.MainDetails.LanguageSpecified Then
                dbElement.Language = dbElement.MainDetails.Language
            Else
                dbElement.Language = dbElement.Source.Language
                dbElement.MainDetails.Language = dbElement.Source.Language
            End If

            'Lock state
            If dbElement.MainDetails.IsLocked Then
                dbElement.IsLocked = dbElement.MainDetails.IsLocked
            End If

            'VideoSource
            Dim strVideosource = FileUtils.Common.GetVideosource(dbElement.FileItem, False)
            If Not String.IsNullOrEmpty(strVideosource) Then
                'use the value that was obtained from the file
                dbElement.VideoSource = strVideosource
                dbElement.MainDetails.VideoSource = strVideosource
            Else
                'use the value from NFO
                dbElement.VideoSource = dbElement.MainDetails.VideoSource
            End If

            'Do the Save
            If ToNfo AndAlso dbElement.NfoPathSpecified Then
                dbElement = Master.DB.Save_Movie(dbElement, batchMode, True, False, True, False)
            Else
                dbElement = Master.DB.Save_Movie(dbElement, batchMode, False, False, True, False)
            End If
        End If
    End Sub

    Public Sub Load_MovieSet(ByRef dbElement As Database.DBElement, ByVal batchMode As Boolean)
        Dim OldTitle As String = dbElement.MainDetails.Title

        GetFolderContents_MovieSet(dbElement)

        If Not dbElement.NfoPathSpecified Then
            Dim sNFO As String = NFO.GetNfoPath_MovieSet(dbElement)
            If Not String.IsNullOrEmpty(sNFO) Then
                dbElement.NfoPath = sNFO
                dbElement.MainDetails = NFO.LoadFromNFO_MovieSet(sNFO)
            End If
        Else
            dbElement.MainDetails = NFO.LoadFromNFO_MovieSet(dbElement.NfoPath)
        End If

        'Language
        If dbElement.MainDetails.LanguageSpecified Then
            dbElement.Language = dbElement.MainDetails.Language
        End If

        'Lock state
        If dbElement.MainDetails.IsLocked Then
            dbElement.IsLocked = dbElement.MainDetails.IsLocked
        End If

        dbElement = Master.DB.Save_Movieset(dbElement, batchMode, False, False, True)
    End Sub

    Public Function Load_TVEpisode(ByVal dbElement As Database.DBElement, ByVal isNew As Boolean, ByVal batchMode As Boolean, reportProgress As Boolean) As SeasonAndEpisodeItems
        Dim SeasonAndEpisodeList As New SeasonAndEpisodeItems
        Dim EpisodesToRemoveList As New List(Of EpisodeItem)

        'first we have to create a list of all already existing episode information for this file path
        If Not isNew Then
            Dim EpisodesByFilenameList As List(Of Database.DBElement) = Master.DB.Load_AllTVEpisodes_ByFileID(dbElement.FileItem.ID, False)
            For Each eEpisode As Database.DBElement In EpisodesByFilenameList
                EpisodesToRemoveList.Add(New EpisodeItem With {.Episode = eEpisode.MainDetails.Episode, .idEpisode = eEpisode.ID, .Season = eEpisode.MainDetails.Season})
            Next
        End If

        GetFolderContents_TVEpisode(dbElement)

        For Each sEpisode As EpisodeItem In RegexGetTVEpisode(dbElement.FileItem.FirstPathFromStack, dbElement.ShowID)
            Dim ToNfo As Boolean = False

            'It's a clone needed to prevent overwriting information of MultiEpisodes
            Dim cEpisode As Database.DBElement = CType(dbElement.CloneDeep, Database.DBElement)

            If cEpisode.NfoPathSpecified Then
                If sEpisode.byDate Then
                    cEpisode.MainDetails = NFO.LoadFromNFO_TVEpisode(cEpisode.NfoPath, sEpisode.Season, sEpisode.Aired)
                Else
                    cEpisode.MainDetails = NFO.LoadFromNFO_TVEpisode(cEpisode.NfoPath, sEpisode.Season, sEpisode.Episode)
                End If

                If Not cEpisode.MainDetails.FileInfoSpecified AndAlso cEpisode.MainDetails.TitleSpecified AndAlso Master.eSettings.TVScraperMetaDataScan Then
                    MetaData.UpdateFileInfo(cEpisode)
                End If
            Else
                If isNew AndAlso cEpisode.TVShowDetails.UniqueIDsSpecified AndAlso cEpisode.ShowIDSpecified Then
                    If sEpisode.byDate Then
                        If Not cEpisode.MainDetails.AiredSpecified Then cEpisode.MainDetails.Aired = sEpisode.Aired
                    Else
                        If cEpisode.MainDetails.Season = -1 Then cEpisode.MainDetails.Season = sEpisode.Season
                        If cEpisode.MainDetails.Episode = -1 Then cEpisode.MainDetails.Episode = sEpisode.Episode
                    End If

                    'Scrape episode data
                    If Master.eSettings.TVEpisode.SourceSettings.AutoScrapeOnImportEnabled Then
                        cEpisode.ScrapeOptions = Master.eSettings.DefaultScrapeOptions(Enums.ContentType.TV)
                        Scraper.Run(cEpisode)
                        If cEpisode.MainDetails.TitleSpecified Then
                            ToNfo = True

                            'if we had info for it (based on title) and mediainfo scanning is enabled
                            If Master.eSettings.TVScraperMetaDataScan Then
                                MetaData.UpdateFileInfo(cEpisode)
                            End If
                        End If
                    End If
                Else
                    cEpisode.MainDetails = New MediaContainers.MainDetails
                End If
            End If

            'Scrape episode images
            If isNew AndAlso cEpisode.TVShowDetails.UniqueIDsSpecified AndAlso cEpisode.ShowIDSpecified Then
                Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
                Dim ScrapeModifiers As New Structures.ScrapeModifiers
                If Not cEpisode.ImagesContainer.Fanart.LocalFilePathSpecified AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled Then ScrapeModifiers.EpisodeFanart = True
                If Not cEpisode.ImagesContainer.Poster.LocalFilePathSpecified AndAlso Master.eSettings.TVEpisodePosterAnyEnabled Then ScrapeModifiers.EpisodePoster = True
                If ScrapeModifiers.EpisodeFanart OrElse ScrapeModifiers.EpisodePoster Then
                    'If Not AddonsManager.Instance.ScrapeImage_TV(cEpisode, SearchResultsContainer, ScrapeModifiers, False) Then
                    '    Images.SetPreferredImages(cEpisode, SearchResultsContainer, ScrapeModifiers)
                    'End If
                End If
            End If

            If Not cEpisode.MainDetails.TitleSpecified Then
                'no title so assume it's an invalid nfo, clear nfo path if exists
                cEpisode.NfoPath = String.Empty
                'set title based on episode file
                If Master.eSettings.TVEpisode.SourceSettings.TitleFiltersEnabled AndAlso Master.eSettings.TVEpisode.SourceSettings.TitleFiltersSpecified Then cEpisode.MainDetails.Title = StringUtils.FilterTitleFromPath_TVEpisode(cEpisode.FileItem.FirstPathFromStack, cEpisode.TVShowDetails.Title)
            End If

            'search local actor thumb for each actor in NFO
            If cEpisode.MainDetails.ActorsSpecified AndAlso cEpisode.ActorThumbsSpecified Then
                For Each actor In cEpisode.MainDetails.Actors
                    actor.LocalFilePath = cEpisode.ActorThumbs.FirstOrDefault(Function(s) Path.GetFileNameWithoutExtension(s).ToLower = actor.Name.Replace(" ", "_").ToLower)
                Next
            End If

            If sEpisode.byDate Then
                If cEpisode.MainDetails.Season = -1 Then cEpisode.MainDetails.Season = sEpisode.Season
                If cEpisode.MainDetails.Episode = -1 AndAlso cEpisode.EpisodeOrdering = Enums.EpisodeOrdering.DayOfYear Then
                    Dim eDate As Date = DateTime.ParseExact(sEpisode.Aired, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture)
                    cEpisode.MainDetails.Episode = eDate.DayOfYear
                ElseIf cEpisode.MainDetails.Episode = -1 Then
                    cEpisode.MainDetails.Episode = sEpisode.Episode
                End If
                If Not cEpisode.MainDetails.AiredSpecified Then cEpisode.MainDetails.Aired = sEpisode.Aired

                If Not cEpisode.MainDetails.TitleSpecified Then
                    'nothing usable in the title after filters have runs
                    cEpisode.MainDetails.Title = String.Format("{0} {1}", cEpisode.TVShowDetails.Title, cEpisode.MainDetails.Aired)
                End If
            Else
                If cEpisode.MainDetails.Season = -1 Then cEpisode.MainDetails.Season = sEpisode.Season
                If cEpisode.MainDetails.Episode = -1 Then cEpisode.MainDetails.Episode = sEpisode.Episode
                If cEpisode.MainDetails.SubEpisode = -1 AndAlso Not sEpisode.SubEpisode = -1 Then cEpisode.MainDetails.SubEpisode = sEpisode.SubEpisode

                If Not cEpisode.MainDetails.TitleSpecified Then
                    'nothing usable in the title after filters have runs
                    cEpisode.MainDetails.Title = StringUtils.BuildGenericTitle_TVEpisode(cEpisode)
                End If
            End If

            'Lock state
            If cEpisode.MainDetails.IsLocked Then
                cEpisode.IsLocked = cEpisode.MainDetails.IsLocked
            End If

            'VideoSource
            Dim strVideosource = FileUtils.Common.GetVideosource(cEpisode.FileItem, True)
            If Not String.IsNullOrEmpty(strVideosource) Then
                'use the value that was obtained from the file
                cEpisode.VideoSource = strVideosource
                cEpisode.MainDetails.VideoSource = strVideosource
            Else
                'use the value from NFO
                cEpisode.VideoSource = cEpisode.MainDetails.VideoSource
            End If

            If Not isNew Then
                Dim EpisodeID As Long = -1

                Dim eEpisode = EpisodesToRemoveList.FirstOrDefault(Function(f) f.Episode = cEpisode.MainDetails.Episode AndAlso f.Season = cEpisode.MainDetails.Season)
                If eEpisode IsNot Nothing Then
                    'if an existing episode was found we use that idEpisode and remove the entry from the "existingEpisodeList" (remaining entries are deleted at the end)
                    EpisodeID = eEpisode.idEpisode
                    EpisodesToRemoveList.Remove(eEpisode)
                End If

                If Not EpisodeID = -1 Then
                    'old episode entry found, we re-use the idEpisode
                    cEpisode.ID = EpisodeID
                    Master.DB.Save_TVEpisode(cEpisode, batchMode, ToNfo, ToNfo, False, True)
                Else
                    'no existing episode found or the season or episode number has changed => we have to add it as new episode
                    Master.DB.Save_TVEpisode(cEpisode, batchMode, ToNfo, ToNfo, True, True)
                End If

                'add the season number to list
                SeasonAndEpisodeList.Seasons.Add(cEpisode.MainDetails.Season)
            Else
                'Do the Save, no Season check (we add a new seasons whit tv show), no Sync (we sync with tv show)
                cEpisode = Master.DB.Save_TVEpisode(cEpisode, batchMode, ToNfo, ToNfo, False, False)
                'add the season number and the new saved episode to list
                SeasonAndEpisodeList.Episodes.Add(cEpisode)
                SeasonAndEpisodeList.Seasons.Add(cEpisode.MainDetails.Season)
            End If

            If reportProgress Then bwPrelim.ReportProgress(-1, New ProgressValue With {.EventType = Enums.ScannerEventType.Added_TVEpisode, .ID = cEpisode.ID, .Message = String.Format("{0}: {1}", cEpisode.TVShowDetails.Title, cEpisode.MainDetails.Title)})
        Next

        If Not isNew Then
            For Each eEpisode As EpisodeItem In EpisodesToRemoveList
                Master.DB.Remove_TVEpisode(eEpisode.idEpisode, False, False, batchMode)
            Next
        End If

        Return SeasonAndEpisodeList
    End Function

    Public Function Load_TVShow(ByRef dbElement As Database.DBElement, ByVal isNew As Boolean, ByVal batchMode As Boolean, ByVal reportProgress As Boolean) As Enums.ScannerEventType
        Dim newEpisodesList As New List(Of Database.DBElement)
        Dim newSeasonsIndex As New List(Of Integer)

        Dim Result As Enums.ScannerEventType = Enums.ScannerEventType.None

        If dbElement.EpisodesSpecified OrElse dbElement.IDSpecified Then
            If (Not _TVShowPaths.ContainsKey(dbElement.ShowPath.ToLower) AndAlso isNew) OrElse (dbElement.IDSpecified AndAlso Not isNew) Then
                Result = If(isNew, Enums.ScannerEventType.Added_TVShow, Enums.ScannerEventType.Refresh_TVShow)

                GetFolderContents_TVShow(dbElement)

                If dbElement.NfoPathSpecified Then
                    dbElement.MainDetails = NFO.LoadFromNFO_TVShow(dbElement.NfoPath)
                Else
                    dbElement.MainDetails = New MediaContainers.MainDetails
                End If

                'IMDB ID
                If Not dbElement.MainDetails.UniqueIDs.IMDbIdSpecified Then
                    dbElement.MainDetails.UniqueIDs.IMDbId = StringUtils.GetIMDBIDFromString(dbElement.ShowPath, True)
                End If

                'Title
                If Not dbElement.MainDetails.TitleSpecified Then
                    'no title so assume it's an invalid nfo, clear nfo path if exists
                    dbElement.NfoPath = String.Empty
                    dbElement.MainDetails.Title = StringUtils.FilterTitleFromPath_TVShow(dbElement.ShowPath)
                End If

                If dbElement.MainDetails.TitleSpecified Then
                    'search local actor thumb for each actor in NFO
                    If dbElement.MainDetails.ActorsSpecified AndAlso dbElement.ActorThumbsSpecified Then
                        For Each actor In dbElement.MainDetails.Actors
                            actor.LocalFilePath = dbElement.ActorThumbs.FirstOrDefault(Function(s) Path.GetFileNameWithoutExtension(s).ToLower = actor.Name.Replace(" ", "_").ToLower)
                        Next
                    End If

                    'Language
                    If dbElement.MainDetails.LanguageSpecified Then
                        dbElement.Language = dbElement.MainDetails.Language
                    Else
                        dbElement.Language = dbElement.Source.Language
                        dbElement.MainDetails.Language = dbElement.Source.Language
                    End If

                    'Lock state
                    If dbElement.MainDetails.IsLocked Then
                        dbElement.IsLocked = dbElement.MainDetails.IsLocked
                    End If

                    Master.DB.Save_TVShow(dbElement, batchMode, False, False, False)
                End If
            Else
                Result = Enums.ScannerEventType.Refresh_TVShow

                Dim newEpisodes As List(Of Database.DBElement) = dbElement.Episodes
                dbElement = Master.DB.Load_TVShow(Convert.ToInt64(_TVShowPaths.Item(dbElement.ShowPath.ToLower)), True, False)
                dbElement.Episodes = newEpisodes
            End If

            If dbElement.ShowIDSpecified Then
                For Each DBTVEpisode As Database.DBElement In dbElement.Episodes
                    DBTVEpisode = Master.DB.AddTVShowInfoToDBElement(DBTVEpisode, dbElement)
                    If DBTVEpisode.FileItemSpecified Then
                        Dim SeasonAndEpisodesList As SeasonAndEpisodeItems = Load_TVEpisode(DBTVEpisode, isNew, batchMode, reportProgress)

                        'add new episodes
                        For Each iEpisode As Database.DBElement In SeasonAndEpisodesList.Episodes
                            newEpisodesList.Add(iEpisode)
                        Next

                        'add seasons
                        For Each iSeason As Integer In SeasonAndEpisodesList.Seasons
                            Dim tmpSeason As Database.DBElement = dbElement.Seasons.FirstOrDefault(Function(f) f.MainDetails.Season = iSeason)
                            If tmpSeason Is Nothing OrElse tmpSeason.MainDetails Is Nothing Then
                                tmpSeason = New Database.DBElement(Enums.ContentType.TVSeason)
                                tmpSeason = Master.DB.AddTVShowInfoToDBElement(tmpSeason, dbElement)
                                tmpSeason.FileItem = DBTVEpisode.FileItem 'needed to check if the episode is inside a season folder or not
                                tmpSeason.MainDetails = New MediaContainers.MainDetails With {.Season = iSeason}

                                'check if tvshow.nfo contains any season details
                                Dim nfoSeason As MediaContainers.MainDetails = dbElement.MainDetails.Seasons.Seasons.FirstOrDefault(Function(f) f.Season = iSeason)
                                If nfoSeason IsNot Nothing Then
                                    tmpSeason.MainDetails.Aired = nfoSeason.Aired
                                    tmpSeason.MainDetails.Plot = nfoSeason.Plot
                                    tmpSeason.MainDetails.Title = nfoSeason.Title
                                    tmpSeason.MainDetails.UniqueIDs.TMDbId = nfoSeason.UniqueIDs.TMDbId
                                    tmpSeason.MainDetails.UniqueIDs.TVDbId = nfoSeason.UniqueIDs.TVDbId
                                Else
                                    'Scrape season info
                                    If isNew AndAlso tmpSeason.MainDetails.UniqueIDsSpecified AndAlso tmpSeason.ShowIDSpecified Then
                                        'AddonsManager.Instance.ScrapeData_TVSeason(tmpSeason, Master.eSettings.DefaultOptions(Enums.ContentType.TV), False)
                                    End If
                                End If

                                GetFolderContents_TVSeason(tmpSeason)

                                'Scrape season images
                                If isNew AndAlso tmpSeason.MainDetails.UniqueIDsSpecified AndAlso tmpSeason.ShowIDSpecified Then
                                    Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
                                    Dim ScrapeModifiers As New Structures.ScrapeModifiers
                                    If Not tmpSeason.ImagesContainer.Banner.LocalFilePathSpecified AndAlso Master.eSettings.TVSeasonBannerAnyEnabled Then ScrapeModifiers.SeasonBanner = True
                                    If Not tmpSeason.ImagesContainer.Fanart.LocalFilePathSpecified AndAlso Master.eSettings.TVSeasonFanartAnyEnabled Then ScrapeModifiers.SeasonFanart = True
                                    If Not tmpSeason.ImagesContainer.Landscape.LocalFilePathSpecified AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled Then ScrapeModifiers.SeasonLandscape = True
                                    If Not tmpSeason.ImagesContainer.Poster.LocalFilePathSpecified AndAlso Master.eSettings.TVSeasonPosterAnyEnabled Then ScrapeModifiers.SeasonPoster = True
                                    If ScrapeModifiers.SeasonBanner OrElse ScrapeModifiers.SeasonFanart OrElse ScrapeModifiers.SeasonLandscape OrElse ScrapeModifiers.SeasonPoster Then
                                        'If Not AddonsManager.Instance.ScrapeImage_TV(tmpSeason, SearchResultsContainer, ScrapeModifiers, False) Then
                                        '    Images.SetPreferredImages(tmpSeason, SearchResultsContainer, ScrapeModifiers)
                                        'End If
                                    End If
                                End If

                                dbElement.Seasons.Add(tmpSeason)
                                newSeasonsIndex.Add(tmpSeason.MainDetails.Season)
                            End If
                        Next
                    End If
                Next
            End If

            'create the "* All Seasons" entry if needed
            If isNew Then
                Dim tmpAllSeasons As Database.DBElement = dbElement.Seasons.FirstOrDefault(Function(f) f.MainDetails.Season_IsAllSeasons)
                If tmpAllSeasons Is Nothing OrElse tmpAllSeasons.MainDetails Is Nothing Then
                    tmpAllSeasons = New Database.DBElement(Enums.ContentType.TVSeason)
                    tmpAllSeasons = Master.DB.AddTVShowInfoToDBElement(tmpAllSeasons, dbElement)
                    tmpAllSeasons.FileItem = New FileItem(Path.Combine(dbElement.ShowPath, "file.ext")) 'needed to check if the episode is inside a season folder or not
                    tmpAllSeasons.MainDetails = New MediaContainers.MainDetails With {.Season = -1}
                    GetFolderContents_TVSeason(tmpAllSeasons)
                    dbElement.Seasons.Add(tmpAllSeasons)
                    newSeasonsIndex.Add(tmpAllSeasons.MainDetails.Season)
                End If
            End If

            'save all new seasons to DB (no sync)
            For Each newSeason As Integer In newSeasonsIndex
                Dim tSeason As Database.DBElement = dbElement.Seasons.FirstOrDefault(Function(f) f.MainDetails.Season = newSeason)
                If tSeason IsNot Nothing AndAlso tSeason.MainDetails IsNot Nothing Then
                    Master.DB.Save_TVSeason(tSeason, batchMode, True, False)
                End If
            Next

            'process new episodes
            For Each nEpisode In newEpisodesList
                AddonsManager.Instance.RunGeneric(Enums.AddonEventType.DuringUpdateDB_TV, Nothing, Nothing, False, nEpisode)
            Next

            'sync new episodes
            For Each nEpisode In newEpisodesList
                Master.DB.Save_TVEpisode(nEpisode, batchMode, False, False, False, True, True)
            Next

            'sync new seasons
            For Each newSeason As Integer In newSeasonsIndex
                Dim tSeason As Database.DBElement = dbElement.Seasons.FirstOrDefault(Function(f) f.MainDetails.Season = newSeason)
                If tSeason IsNot Nothing AndAlso tSeason.MainDetails IsNot Nothing Then
                    Master.DB.Save_TVSeason(tSeason, batchMode, False, True)
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
                Dim iEpisode As Integer = 0
                If Integer.TryParse(Episode.Groups(1).Value, iEpisode) Then
                    eItem.Episode = iEpisode
                Else
                    Return False
                End If
                If Not String.IsNullOrEmpty(Episode.Groups(2).Value) Then endPattern = Episode.Groups(2).Value
            ElseIf Not String.IsNullOrEmpty(tSeason) AndAlso String.IsNullOrEmpty(tEpisode) Then
                'no episode specification -> assume defaultSeason and use the "season" group as episode number
                eItem.Season = defaultSeason
                Dim iEpisode As Integer = 0
                If Integer.TryParse(tSeason, iEpisode) Then
                    eItem.Episode = iEpisode
                Else
                    Return False
                End If
            Else
                'season and episode specified
                Dim iSeason As Integer = 0
                If Integer.TryParse(tSeason, iSeason) Then
                    eItem.Season = iSeason
                    Dim Episode As Match = Regex.Match(tEpisode, "(\d*)(.*)")
                    Dim iEpisode As Integer = 0
                    If Integer.TryParse(Episode.Groups(1).Value, iEpisode) Then
                        eItem.Episode = iEpisode
                        If Not String.IsNullOrEmpty(Episode.Groups(2).Value) Then endPattern = Episode.Groups(2).Value
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
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

    Public Shared Function RegexGetTVEpisode(ByVal path As String, ByVal showID As Long) As List(Of EpisodeItem)
        Dim retEpisodeItemsList As New List(Of EpisodeItem)

        For Each rShow In Master.eSettings.TVEpisode.SourceSettings.EpisodeMatching
            Dim reg As Regex = New Regex(rShow.RegExp, RegexOptions.IgnoreCase)

            Dim regexppos As Integer
            Dim regexp2pos As Integer

            If reg.IsMatch(path.ToLower) Then
                Dim eItem As New EpisodeItem
                Dim defaultSeason As Integer = rShow.DefaultSeason
                Dim sMatch As Match = reg.Match(path.ToLower)

                If rShow.ByDate Then
                    If Not RegexGetAiredDate(sMatch, eItem) Then Continue For
                    retEpisodeItemsList.Add(eItem)
                    _Logger.Info(String.Format("[Scanner] [RegexGetTVEpisode] Found date based match {0} ({1}) [{2}]", path, eItem.Aired, rShow.RegExp))
                Else
                    If Not RegexGetSeasonAndEpisodeNumber(sMatch, eItem, defaultSeason) Then
                        _Logger.Warn(String.Format("[Scanner] [RegexGetTVEpisode] Can't get a proper season and episode number for file {0} [{1}]", path, rShow.RegExp))
                        Continue For
                    End If
                    retEpisodeItemsList.Add(eItem)
                    _Logger.Info(String.Format("[Scanner] [RegexGetTVEpisode] Found episode match {0} (s{1}e{2}{3}) [{4}]", path, eItem.Season, eItem.Episode, If(Not eItem.SubEpisode = -1, String.Concat(".", eItem.SubEpisode), String.Empty), rShow.RegExp))
                End If

                ' Grab the remainder from first regexp run
                ' as second run might modify or empty it.
                Dim remainder As String = sMatch.Groups(3).Value.ToString

                If Master.eSettings.TVEpisode.SourceSettings.EpisodeMultiPartMatchingSpecified Then
                    Dim reg2 As Regex = New Regex(Master.eSettings.TVEpisode.SourceSettings.EpisodeMultiPartMatching.Regex, RegexOptions.IgnoreCase)

                    ' check the remainder of the string for any further episodes.
                    If Not rShow.ByDate Then
                        ' we want "long circuit" OR below so that both offsets are evaluated
                        While reg2.IsMatch(remainder) OrElse reg.IsMatch(remainder)
                            regexppos = If(reg.IsMatch(remainder), reg.Match(remainder).Index, -1)
                            regexp2pos = If(reg2.IsMatch(remainder), reg2.Match(remainder).Index, -1)
                            If (regexppos <= regexp2pos AndAlso regexppos <> -1) OrElse (regexppos >= 0 AndAlso regexp2pos = -1) Then
                                eItem = New EpisodeItem
                                RegexGetSeasonAndEpisodeNumber(reg.Match(remainder), eItem, defaultSeason)
                                retEpisodeItemsList.Add(eItem)
                                _Logger.Info(String.Format("[Scanner] [RegexGetTVEpisode] Adding new season {0}, multipart episode {1} [{2}]", eItem.Season, eItem.Episode, rShow.RegExp))
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
                                _Logger.Info(String.Format("[Scanner] [RegexGetTVEpisode] Adding multipart episode {0} [{1}]", eItem.Episode, Master.eSettings.TVEpisode.SourceSettings.EpisodeMultiPartMatching))
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
    ''' <param name="path">Full path of the directory.</param>
    ''' <param name="dbSource">Structures.Source</param>
    Public Sub ScanForFiles_Movie(ByVal path As String, ByVal dbSource As Database.DBSource)
        Dim nFileItemList As New FileItemList(path, Enums.ContentType.Movie)
        For Each nFileItem As FileItem In nFileItemList.FileItems.Where(Function(f) _
                                                                            Not f.bIsDirectory AndAlso
                                                                            Not _filePaths.Contains(f.FullPath.ToLower)
                                                                            )
            If dbSource.IsSingle AndAlso
                Not nFileItem.bIsBDMV AndAlso
                Not nFileItem.bIsVideoTS AndAlso
                _filePaths.Where(Function(f) _
                                      Not f.StartsWith("stack://") AndAlso
                                      Directory.GetParent(f).FullName.ToLower = nFileItem.MainPath.FullName.ToLower).Count > 0 OrElse
                                      dbSource.IsSingle AndAlso nFileItem.MainPath.FullName.ToLower = dbSource.Path.ToLower Then
                Continue For
            End If

            Dim nMovieContainer = New Database.DBElement(Enums.ContentType.Movie)
            nMovieContainer.FileItem = nFileItem
            nMovieContainer.IsSingle = dbSource.IsSingle
            nMovieContainer.Language = dbSource.Language
            nMovieContainer.Source = dbSource
            Load_Movie(nMovieContainer, True)
            _filePaths.Add(nFileItem.FullPath.ToLower)
            bwPrelim.ReportProgress(-1, New ProgressValue With {
                                    .EventType = Enums.ScannerEventType.Added_Movie,
                                    .ID = nMovieContainer.ID,
                                    .Message = nMovieContainer.MainDetails.Title
                                    })
        Next

        If dbSource.ScanRecursive Then
            For Each nFileItem In nFileItemList.FileItems.Where(Function(f) f.bIsDirectory)
                ScanForFiles_Movie(nFileItem.FullPath, dbSource)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Find all related files in a directory.
    ''' </summary>
    ''' <param name="dbElement">TVShowContainer object</param>
    ''' <param name="path">Path of folder contianing the episodes</param>
    Public Sub ScanForFiles_TV(ByRef dbElement As Database.DBElement, ByVal path As String)
        Dim nFileItemList As New FileItemList(path, dbElement.ContentType)
        For Each nFileItem As FileItem In nFileItemList.FileItems.Where(Function(f) _
                                                                            Not f.bIsDirectory AndAlso
                                                                            Not _filePaths.Contains(f.FullPath.ToLower)
                                                                            )
            dbElement.Episodes.Add(New Database.DBElement(Enums.ContentType.TVEpisode) With {
                                    .FileItem = nFileItem
                                    })
        Next

        For Each nFileItem In nFileItemList.FileItems.Where(Function(f) f.bIsDirectory)
            ScanForFiles_TV(dbElement, nFileItem.FullPath)
        Next
    End Sub

    ''' <summary>
    ''' Get all directories/movies in the parent directory
    ''' </summary>
    ''' <param name="dbSource"></param>
    ''' <param name="path">Specific Path to scan</param>
    Public Sub ScanSourceDirectory_Movie(ByVal dbSource As Database.DBSource, Optional ByVal path As String = "")
        If FileUtils.Common.CheckOnlineStatus(dbSource, True) Then
            Dim strScanPath As String = String.Empty

            If Not String.IsNullOrEmpty(path) Then
                strScanPath = path
            Else
                strScanPath = dbSource.Path
            End If

            If Directory.Exists(strScanPath) Then
                ScanForFiles_Movie(strScanPath, dbSource)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Get all directories in the parent directory
    ''' </summary>
    ''' <param name="dbSource"></param>
    ''' <param name="path">Specific Path to scan</param>
    Public Sub ScanSourceDirectory_TV(ByVal dbSource As Database.DBSource, Optional ByVal path As String = "", Optional forcePathAsTVShowFolder As Boolean = False)
        If FileUtils.Common.CheckOnlineStatus(dbSource, True) Then
            Dim ScanPath As String = String.Empty

            If Not String.IsNullOrEmpty(path) Then
                ScanPath = path
            Else
                ScanPath = dbSource.Path
            End If

            If Directory.Exists(ScanPath) Then
                Dim dInfo As New DirectoryInfo(ScanPath)
                Dim inList As IEnumerable(Of DirectoryInfo) = Nothing

                'tv show folder as a source
                If dbSource.IsSingle OrElse forcePathAsTVShowFolder Then
                    Dim currShowContainer As New Database.DBElement(Enums.ContentType.TVShow)
                    currShowContainer.EpisodeSorting = dbSource.EpisodeSorting
                    currShowContainer.Language = dbSource.Language
                    currShowContainer.EpisodeOrdering = dbSource.EpisodeOrdering
                    currShowContainer.ShowPath = dInfo.FullName
                    currShowContainer.Source = dbSource
                    ScanForFiles_TV(currShowContainer, dInfo.FullName)
                    Try
                        inList = dInfo.GetDirectories.Where(Function(f) FileUtils.Common.IsValidDir(f, Enums.ContentType.TVShow)).OrderBy(Function(d) d.Name)
                    Catch ex As Exception
                        _Logger.Error(ex, New StackFrame().GetMethod().Name)
                    End Try

                    For Each sDirs As DirectoryInfo In inList
                        ScanForFiles_TV(currShowContainer, sDirs.FullName)
                    Next

                    Dim Result = Load_TVShow(currShowContainer, True, True, True)
                    If Not Result = Enums.ScannerEventType.None Then
                        bwPrelim.ReportProgress(-1, New ProgressValue With {
                                                .EventType = Result,
                                                .ID = currShowContainer.ID,
                                                .Message = currShowContainer.MainDetails.Title
                                                })
                    End If
                Else
                    For Each inDir As DirectoryInfo In dInfo.GetDirectories.Where(Function(f) FileUtils.Common.IsValidDir(f, Enums.ContentType.TVShow)).OrderBy(Function(d) d.Name)
                        Dim currShowContainer As New Database.DBElement(Enums.ContentType.TVShow)
                        currShowContainer.EpisodeSorting = dbSource.EpisodeSorting
                        currShowContainer.Language = dbSource.Language
                        currShowContainer.EpisodeOrdering = dbSource.EpisodeOrdering
                        currShowContainer.ShowPath = inDir.FullName
                        currShowContainer.Source = dbSource
                        ScanForFiles_TV(currShowContainer, inDir.FullName)

                        Dim Result = Load_TVShow(currShowContainer, True, True, True)
                        If Not Result = Enums.ScannerEventType.None Then
                            bwPrelim.ReportProgress(-1, New ProgressValue With {
                                                    .EventType = Result,
                                                    .ID = currShowContainer.ID,
                                                    .Message = currShowContainer.MainDetails.Title
                                                    })
                        End If
                    Next

                End If
            End If
        End If
    End Sub

    Public Sub Start(ByVal scanOptions As ScanOrCleanOptions, ByVal idSource As Long, ByVal folderPath As String)
        bwPrelim = New System.ComponentModel.BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }
        bwPrelim.RunWorkerAsync(New Arguments With {
                                .FolderPath = folderPath,
                                .Scan = scanOptions,
                                .SourceID = idSource
                                })
    End Sub

    Private Sub bwPrelim_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwPrelim.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim bAlreadyClearedNew_Movie As Boolean
        Dim bAlreadyClearedNew_Movieset As Boolean
        Dim bAlreadyClearedNew_TV As Boolean
        _filePaths = Master.DB.GetAllFilePaths

        If Args.Scan.SpecificFolder AndAlso Not String.IsNullOrEmpty(Args.FolderPath) AndAlso Directory.Exists(Args.FolderPath) Then
            'check if FolderPath is inside of a movie source
            For Each eSource In Master.DB.Load_AllSources_Movie
                Dim tSource As String = If(eSource.Path.EndsWith(Path.DirectorySeparatorChar), eSource.Path, String.Concat(eSource.Path, Path.DirectorySeparatorChar)).ToLower.Trim
                Dim tFolder As String = If(Args.FolderPath.EndsWith(Path.DirectorySeparatorChar), Args.FolderPath, String.Concat(Args.FolderPath, Path.DirectorySeparatorChar)).ToLower.Trim

                If tFolder.StartsWith(tSource) Then
                    Args.Scan.Movies = True
                    If Master.eSettings.Movie.SourceSettings.ResetNewBeforeDBUpdate AndAlso Not bAlreadyClearedNew_Movie Then
                        Master.DB.ClearNew(Enums.ContentType.Movie)
                        bAlreadyClearedNew_Movie = True
                    End If
                    If Master.eSettings.Movieset.SourceSettings.ResetNewBeforeDBUpdate AndAlso Not bAlreadyClearedNew_Movieset Then
                        Master.DB.ClearNew(Enums.ContentType.Movieset)
                        bAlreadyClearedNew_Movieset = True
                    End If

                    Using sqlTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        Using sqlCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            ScanSourceDirectory_Movie(eSource, Args.FolderPath)
                        End Using
                        sqlTransaction.Commit()
                    End Using
                    Exit For
                End If
            Next

            'check if FolderPath is inside of a tv show source
            For Each eSource In Master.DB.Load_AllSources_TVShow
                Dim tSource As String = If(eSource.Path.EndsWith(Path.DirectorySeparatorChar), eSource.Path, String.Concat(eSource.Path, Path.DirectorySeparatorChar)).ToLower.Trim
                Dim tFolder As String = If(Args.FolderPath.EndsWith(Path.DirectorySeparatorChar), Args.FolderPath, String.Concat(Args.FolderPath, Path.DirectorySeparatorChar)).ToLower.Trim

                If tFolder.StartsWith(tSource) Then
                    Args.Scan.TV = True
                    If Not bAlreadyClearedNew_TV Then
                        If Master.eSettings.TVEpisode.SourceSettings.ResetNewBeforeDBUpdate Then
                            Master.DB.ClearNew(Enums.ContentType.TVEpisode)
                        End If
                        If Master.eSettings.TVSeason.SourceSettings.ResetNewBeforeDBUpdate Then
                            Master.DB.ClearNew(Enums.ContentType.TVSeason)
                        End If
                        If Master.eSettings.TVShow.SourceSettings.ResetNewBeforeDBUpdate Then
                            Master.DB.ClearNew(Enums.ContentType.TVShow)
                        End If
                        bAlreadyClearedNew_TV = True
                    End If

                    _TVShowPaths = Master.DB.GetAllTVShowPaths
                    If Args.FolderPath.ToLower = eSource.Path.ToLower Then
                        'Args.Folder is a tv show source folder -> scan the whole source
                        ScanSourceDirectory_TV(eSource)
                    Else
                        'Args.Folder is a tv show folder or a tv show subfolder -> get the tv show main path
                        Dim idShow As Long = -1
                        For Each hKey In _TVShowPaths.Keys
                            If String.Concat(Args.FolderPath.ToLower, Path.DirectorySeparatorChar).StartsWith(String.Concat(hKey.ToString.ToLower, Path.DirectorySeparatorChar)) Then
                                idShow = Convert.ToInt64(_TVShowPaths.Item(hKey))
                                Exit For
                            End If
                        Next

                        If Not idShow = -1 Then
                            Dim currShowContainer As Database.DBElement = Master.DB.Load_TVShow(idShow, False, False)

                            Dim inInfo As DirectoryInfo = New DirectoryInfo(currShowContainer.ShowPath)
                            Dim inList As IEnumerable(Of DirectoryInfo) = Nothing
                            Try
                                inList = inInfo.GetDirectories.Where(Function(d) FileUtils.Common.IsValidDir(d, Enums.ContentType.TVShow)).OrderBy(Function(d) d.Name)
                            Catch
                            End Try

                            ScanForFiles_TV(currShowContainer, inInfo.FullName)

                            For Each sDirs As DirectoryInfo In inList
                                ScanForFiles_TV(currShowContainer, sDirs.FullName)
                            Next

                            Dim Result = Load_TVShow(currShowContainer, True, True, True)
                            If Not Result = Enums.ScannerEventType.None Then
                                bwPrelim.ReportProgress(-1, New ProgressValue With {
                                                        .EventType = Result,
                                                        .ID = currShowContainer.ID,
                                                        .Message = currShowContainer.MainDetails.Title
                                                        })
                            End If
                        Else
                            'add a new tv show
                            Dim nDInfo = New DirectoryInfo(Args.FolderPath)
                            While nDInfo.Parent IsNot Nothing AndAlso Not nDInfo.Parent.FullName.ToLower = eSource.Path.ToLower
                                nDInfo = nDInfo.Parent
                            End While
                            If nDInfo.Parent IsNot Nothing AndAlso nDInfo.Parent.FullName.ToLower = eSource.Path.ToLower Then
                                ScanSourceDirectory_TV(eSource, nDInfo.FullName, True)
                            End If
                        End If
                    End If
                    Exit For
                End If
            Next
        End If

        If Not Args.Scan.SpecificFolder AndAlso Args.Scan.Movies Then
            If Master.eSettings.Movie.SourceSettings.ResetNewBeforeDBUpdate AndAlso Not bAlreadyClearedNew_Movie Then
                Master.DB.ClearNew(Enums.ContentType.Movie)
                bAlreadyClearedNew_Movie = True
            End If
            If Master.eSettings.Movieset.SourceSettings.ResetNewBeforeDBUpdate AndAlso Not bAlreadyClearedNew_Movieset Then
                Master.DB.ClearNew(Enums.ContentType.Movieset)
                bAlreadyClearedNew_Movieset = True
            End If
            Using sqlTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Dim dtSources As New DataTable
                Dim nSmartFilter As New SmartFilter.Filter(Enums.ContentType.Source_Movie)

                If Not Args.SourceID = -1 Then
                    nSmartFilter.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.idSource, .Operator = SmartFilter.Operators.Is, .Value = Args.SourceID})
                    dtSources = Master.DB.GetSources_Movie(nSmartFilter)
                Else
                    nSmartFilter.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.Exclude, .Operator = SmartFilter.Operators.False})
                    dtSources = Master.DB.GetSources_Movie(nSmartFilter)
                End If

                For Each nSource As DataRow In dtSources.Rows
                    Dim sSource As Database.DBSource = Master.DB.Load_Source_Movie(CLng(nSource.Item(Database.Helpers.GetMainIdName(Database.TableName.moviesource))))
                    Try
                        If Master.eSettings.Movie.SourceSettings.SortBeforeScan OrElse sSource.IsSingle Then
                            FileUtils.SortFiles(nSource.Item(Database.Helpers.GetColumnName(Database.ColumnName.Path)).ToString)
                        End If
                    Catch ex As Exception
                        _Logger.Error(ex, New StackFrame().GetMethod().Name)
                    End Try
                    bwPrelim.ReportProgress(-1, New ProgressValue With {
                                            .EventType = Enums.ScannerEventType.CurrentSource,
                                            .Message = String.Format("{0} ({1})", sSource.Name, sSource.Path)
                                            })
                    ScanSourceDirectory_Movie(sSource)

                    If bwPrelim.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If
                Next
                sqlTransaction.Commit()
            End Using
        End If

        If Not Args.Scan.SpecificFolder AndAlso Args.Scan.TV Then
            If Not bAlreadyClearedNew_TV Then
                If Master.eSettings.TVEpisode.SourceSettings.ResetNewBeforeDBUpdate Then
                    Master.DB.ClearNew(Enums.ContentType.TVEpisode)
                End If
                If Master.eSettings.TVSeason.SourceSettings.ResetNewBeforeDBUpdate Then
                    Master.DB.ClearNew(Enums.ContentType.TVSeason)
                End If
                If Master.eSettings.TVShow.SourceSettings.ResetNewBeforeDBUpdate Then
                    Master.DB.ClearNew(Enums.ContentType.TVShow)
                End If
                bAlreadyClearedNew_TV = True
            End If

            _TVShowPaths = Master.DB.GetAllTVShowPaths
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Dim dtSources As New DataTable
                Dim nSmartFilter As New SmartFilter.Filter(Enums.ContentType.Source_TVShow)

                If Not Args.SourceID = -1 Then
                    nSmartFilter.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.idSource, .Operator = SmartFilter.Operators.Is, .Value = Args.SourceID})
                    dtSources = Master.DB.GetSources_TVShow(nSmartFilter)
                Else
                    nSmartFilter.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.Exclude, .Operator = SmartFilter.Operators.False})
                    dtSources = Master.DB.GetSources_TVShow(nSmartFilter)
                End If

                For Each nSource As DataRow In dtSources.Rows
                    Dim sSource As Database.DBSource = Master.DB.Load_Source_TVShow(CLng(nSource.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshowsource))))
                    ScanSourceDirectory_TV(sSource)

                    If bwPrelim.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If
                Next
                SQLtransaction.Commit()
            End Using
        End If

        'no separate MovieSet scanning possible, so we clean MovieSets when movies were scanned
        If (Master.eSettings.Movie.SourceSettings.CleanLibraryAfterUpdate AndAlso Args.Scan.Movies) OrElse
            (Master.eSettings.MovieSetCleanDB AndAlso Args.Scan.Movies) OrElse
            (Master.eSettings.TVShow.SourceSettings.CleanLibraryAfterUpdate AndAlso Args.Scan.TV) Then
            bwPrelim.ReportProgress(-1, New ProgressValue With {.EventType = Enums.ScannerEventType.CleaningDatabase, .Message = String.Empty})
            'remove any db entries that no longer exist
            Master.DB.Clean(Master.eSettings.Movie.SourceSettings.CleanLibraryAfterUpdate AndAlso Args.Scan.Movies,
                            Master.eSettings.MovieSetCleanDB AndAlso Args.Scan.Moviesets,
                            Master.eSettings.TVShow.SourceSettings.CleanLibraryAfterUpdate AndAlso Args.Scan.TV, Args.SourceID
                            )
        End If

        e.Result = Args
    End Sub

    Private Sub bwPrelim_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwPrelim.ProgressChanged
        Dim tProgressValue As ProgressValue = DirectCast(e.UserState, ProgressValue)
        RaiseEvent ProgressUpdate(tProgressValue)

        Select Case tProgressValue.EventType
            Case Enums.ScannerEventType.Added_Movie
                AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"newmovie", 3, Master.eLang.GetString(817, "New Movie Added"), tProgressValue.Message, Nothing}))
            Case Enums.ScannerEventType.Added_TVEpisode
                AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"newep", 4, Master.eLang.GetString(818, "New Episode Added"), tProgressValue.Message, Nothing}))
        End Select
    End Sub

    Private Sub bwPrelim_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwPrelim.RunWorkerCompleted
        If Not e.Cancelled Then
            Dim Args As Arguments = DirectCast(e.Result, Arguments)
            If Args.Scan.Movies Then
                Dim params As New List(Of Object)(New Object() {False, False, False, True, Args.SourceID})
                AddonsManager.Instance.RunGeneric(Enums.AddonEventType.AfterUpdateDB_Movie, params, Nothing)
            End If
            If Args.Scan.TV Then
                Dim params As New List(Of Object)(New Object() {False, False, False, True, Args.SourceID})
                AddonsManager.Instance.RunGeneric(Enums.AddonEventType.AfterUpdateDB_TV, params, Nothing)
            End If
            RaiseEvent ProgressUpdate(New ProgressValue With {.EventType = Enums.ScannerEventType.ScannerEnded})
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim FolderPath As String
        Dim Scan As ScanOrCleanOptions
        Dim SourceID As Long

#End Region 'Fields

    End Structure

    Public Class EpisodeItem

#Region "Properties"

        Public Property Aired() As String = String.Empty

        Public Property byDate() As Boolean = False

        Public Property Episode() As Integer = -1

        Public Property idEpisode() As Long = -1

        Public Property Season() As Integer = -2

        Public Property SubEpisode() As Integer = -1

#End Region 'Properties

    End Class

    Public Structure ProgressValue

#Region "Fields"

        Dim ContentType As Enums.ContentType
        Dim EventType As Enums.ScannerEventType
        Dim ID As Long
        Dim Message As String

#End Region 'Fields

    End Structure

    Public Class ScanOrCleanOptions

#Region "Properties"

        Public Property Movies As Boolean

        Public Property Moviesets As Boolean

        Public Property SpecificFolder As Boolean

        Public Property TV As Boolean

#End Region 'Properties

    End Class

    Public Class SeasonAndEpisodeItems

#Region "Properties"

        Public Property Episodes() As List(Of Database.DBElement) = New List(Of Database.DBElement)

        Public Property Seasons() As List(Of Integer) = New List(Of Integer)

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class