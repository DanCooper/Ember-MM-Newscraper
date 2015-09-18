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

Imports System
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports RestSharp
Imports NLog
Imports System.Xml.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Public Class frmMain

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwCheckVersion As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwCleanDB As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwDownloadPic As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadEpInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadMovieInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadMovieSetInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadMovieSetPosters As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadSeasonInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadShowInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwMetaData As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwMovieScraper As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwMovieSetScraper As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwNonScrape As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwReload_Movies As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwReload_MovieSets As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwReload_TVShows As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwRewrite_Movies As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwTVScraper As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwTVEpisodeScraper As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwTVSeasonScraper As New System.ComponentModel.BackgroundWorker

    Public fCommandLine As New CommandLine

    Private TaskList As New List(Of Task)
    Private TasksDone As Boolean = True

    Private alActors As New List(Of String)
    Private FilterRaise_Movies As Boolean = False
    Private FilterRaise_MovieSets As Boolean = False
    Private FilterRaise_Shows As Boolean = False
    Private aniRaise As Boolean = False
    Private MovieInfoPanelState As Integer = 0 '0 = down, 1 = mid, 2 = up
    Private MovieSetInfoPanelState As Integer = 0 '0 = down, 1 = mid, 2 = up
    Private TVShowInfoPanelState As Integer = 0 '0 = down, 1 = mid, 2 = up

    Private bsMovies As New BindingSource
    Private bsMovieSets As New BindingSource
    Private bsTVEpisodes As New BindingSource
    Private bsTVSeasons As New BindingSource
    Private bsTVShows As New BindingSource

    Private dtMovies As New DataTable
    Private dtMovieSets As New DataTable
    Private dtTVEpisodes As New DataTable
    Private dtTVSeasons As New DataTable
    Private dtTVShows As New DataTable
    Private dScrapeRow As DataRow = Nothing

    Private fScanner As New Scanner
    Private GenreImage As Image
    Private InfoCleared As Boolean = False
    Private LoadingDone As Boolean = False
    Private MainActors As New Images
    Private MainBanner As New Images
    Private MainCharacterArt As New Images
    Private MainClearArt As New Images
    Private MainClearLogo As New Images
    Private MainDiscArt As New Images
    Private MainFanart As New Images
    Private MainPoster As New Images
    Private MainFanartSmall As New Images
    Private MainLandscape As New Images
    Private pbGenre() As PictureBox = Nothing
    Private pnlGenre() As Panel = Nothing
    Private ReportDownloadPercent As Boolean = False
    Private ScraperDone As Boolean = False
    Private sHTTP As New EmberAPI.HTTP

    'Loading Delays
    Private currRow_Movie As Integer = -1
    Private currRow_MovieSet As Integer = -1
    Private currRow_TVEpisode As Integer = -1
    Private currRow_TVSeason As Integer = -1
    Private currRow_TVShow As Integer = -1
    Private currList As Integer = 0
    Private currThemeType As Theming.ThemeType
    Private prevRow_TVEpisode As Integer = -1
    Private prevRow_Movie As Integer = -1
    Private prevRow_MovieSet As Integer = -1
    Private prevRow_TVSeason As Integer = -1
    Private prevRow_TVShow As Integer = -1

    'list movies
    Private currList_Movies As String = "movielist" 'default movie list SQLite view
    Private listViews_Movies As New Dictionary(Of String, String)

    'list moviesets
    Private currList_MovieSets As String = "setslist" 'default moviesets list SQLite view
    Private listViews_MovieSets As New Dictionary(Of String, String)

    'list shows
    Private currList_Shows As String = "tvshowlist" 'default tv show list SQLite view
    Private listViews_Shows As New Dictionary(Of String, String)

    'filter movies
    Private bDoingSearch_Movies As Boolean = False
    Private FilterArray_Movies As New List(Of String)
    Private filDataField_Movies As String = String.Empty
    Private filSearch_Movies As String = String.Empty
    Private filSource_Movies As String = String.Empty
    Private filYear_Movies As String = String.Empty
    Private filGenre_Movies As String = String.Empty
    Private filCountry_Movies As String = String.Empty
    Private filMissing_Movies As String = String.Empty
    Private currTextSearch_Movies As String = String.Empty
    Private prevTextSearch_Movies As String = String.Empty

    'filter moviesets
    Private bDoingSearch_MovieSets As Boolean = False
    Private FilterArray_MovieSets As New List(Of String)
    Private filSearch_MovieSets As String = String.Empty
    Private filMissing_MovieSets As String = String.Empty
    Private currTextSearch_MovieSets As String = String.Empty
    Private prevTextSearch_MovieSets As String = String.Empty

    'filter shows
    Private bDoingSearch_Shows As Boolean = False
    Private FilterArray_Shows As New List(Of String)
    Private filSearch_Shows As String = String.Empty
    Private filSource_Shows As String = String.Empty
    Private filGenre_Shows As String = String.Empty
    Private filMissing_Shows As String = String.Empty
    Private currTextSearch_Shows As String = String.Empty
    Private prevTextSearch_Shows As String = String.Empty

    'Theme Information
    Private _bannermaxheight As Integer = 160
    Private _bannermaxwidth As Integer = 285
    Private _characterartmaxheight As Integer = 160
    Private _characterartmaxwidth As Integer = 160
    Private _clearartmaxheight As Integer = 160
    Private _clearartmaxwidth As Integer = 285
    Private _clearlogomaxheight As Integer = 160
    Private _clearlogomaxwidth As Integer = 285
    Private _discartmaxheight As Integer = 160
    Private _discartmaxwidth As Integer = 160
    Private _postermaxheight As Integer = 160
    Private _postermaxwidth As Integer = 160
    Private _fanartsmallmaxheight As Integer = 160
    Private _fanartsmallmaxwidth As Integer = 285
    Private _landscapemaxheight As Integer = 160
    Private _landscapemaxwidth As Integer = 285
    Private tTheme As New Theming
    Private _genrepanelcolor As Color = Color.Gainsboro
    Private _ipmid As Integer = 280
    Private _ipup As Integer = 500
    Private CloseApp As Boolean = False

    Private _SelectedScrapeType As String = String.Empty
    Private _SelectedScrapeTypeMode As String = String.Empty
    Private _SelectedContentType As String = String.Empty

    Private oldStatus As String = String.Empty

    Private KeyBuffer As String = String.Empty

    Private currMovie As Database.DBElement
    Private currMovieSet As Database.DBElement
    Private currTV As Database.DBElement

#End Region 'Fields

#Region "Delegates"

    Delegate Sub DelegateEvent_Movie(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
    Delegate Sub DelegateEvent_MovieSet(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)
    Delegate Sub DelegateEvent_TVShow(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object)

    Delegate Sub MydtListUpdate(ByVal drow As DataRow, ByVal v As DataRow)

    Delegate Sub MySettingsShow(ByVal dlg As dlgSettings)

#End Region 'Delegates

#Region "Properties"

    Public Property GenrePanelColor() As Color
        Get
            Return _genrepanelcolor
        End Get
        Set(ByVal value As Color)
            _genrepanelcolor = value
        End Set
    End Property

    Public Property IPMid() As Integer
        Get
            Return _ipmid
        End Get
        Set(ByVal value As Integer)
            _ipmid = value
        End Set
    End Property

    Public Property IPUp() As Integer
        Get
            Return _ipup
        End Get
        Set(ByVal value As Integer)
            _ipup = value
        End Set
    End Property

    Public Property BannerMaxHeight() As Integer
        Get
            Return _bannermaxheight
        End Get
        Set(ByVal value As Integer)
            _bannermaxheight = value
        End Set
    End Property

    Public Property BannerMaxWidth() As Integer
        Get
            Return _bannermaxwidth
        End Get
        Set(ByVal value As Integer)
            _bannermaxwidth = value
        End Set
    End Property

    Public Property CharacterArtMaxHeight() As Integer
        Get
            Return _characterartmaxheight
        End Get
        Set(ByVal value As Integer)
            _characterartmaxheight = value
        End Set
    End Property

    Public Property CharacterArtMaxWidth() As Integer
        Get
            Return _characterartmaxwidth
        End Get
        Set(ByVal value As Integer)
            _characterartmaxwidth = value
        End Set
    End Property

    Public Property ClearArtMaxHeight() As Integer
        Get
            Return _clearartmaxheight
        End Get
        Set(ByVal value As Integer)
            _clearartmaxheight = value
        End Set
    End Property

    Public Property ClearArtMaxWidth() As Integer
        Get
            Return _clearartmaxwidth
        End Get
        Set(ByVal value As Integer)
            _clearartmaxwidth = value
        End Set
    End Property

    Public Property ClearLogoMaxHeight() As Integer
        Get
            Return _clearlogomaxheight
        End Get
        Set(ByVal value As Integer)
            _clearlogomaxheight = value
        End Set
    End Property

    Public Property ClearLogoMaxWidth() As Integer
        Get
            Return _clearlogomaxwidth
        End Get
        Set(ByVal value As Integer)
            _clearlogomaxwidth = value
        End Set
    End Property

    Public Property DiscArtMaxHeight() As Integer
        Get
            Return _discartmaxheight
        End Get
        Set(ByVal value As Integer)
            _discartmaxheight = value
        End Set
    End Property

    Public Property DiscArtMaxWidth() As Integer
        Get
            Return _discartmaxwidth
        End Get
        Set(ByVal value As Integer)
            _discartmaxwidth = value
        End Set
    End Property

    Public Property PosterMaxHeight() As Integer
        Get
            Return _postermaxheight
        End Get
        Set(ByVal value As Integer)
            _postermaxheight = value
        End Set
    End Property

    Public Property PosterMaxWidth() As Integer
        Get
            Return _postermaxwidth
        End Get
        Set(ByVal value As Integer)
            _postermaxwidth = value
        End Set
    End Property

    Public Property FanartSmallMaxHeight() As Integer
        Get
            Return _fanartsmallmaxheight
        End Get
        Set(ByVal value As Integer)
            _fanartsmallmaxheight = value
        End Set
    End Property

    Public Property FanartSmallMaxWidth() As Integer
        Get
            Return _fanartsmallmaxwidth
        End Get
        Set(ByVal value As Integer)
            _fanartsmallmaxwidth = value
        End Set
    End Property

    Public Property LandscapeMaxHeight() As Integer
        Get
            Return _landscapemaxheight
        End Get
        Set(ByVal value As Integer)
            _landscapemaxheight = value
        End Set
    End Property

    Public Property LandscapeMaxWidth() As Integer
        Get
            Return _landscapemaxwidth
        End Get
        Set(ByVal value As Integer)
            _landscapemaxwidth = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub ClearInfo()

        Try
            With Me
                .InfoCleared = True

                If .bwDownloadPic.IsBusy Then .bwDownloadPic.CancelAsync()
                If .bwLoadMovieInfo.IsBusy Then .bwLoadMovieInfo.CancelAsync()
                If .bwLoadMovieSetInfo.IsBusy Then .bwLoadMovieSetInfo.CancelAsync()
                If .bwLoadMovieSetPosters.IsBusy Then .bwLoadMovieSetPosters.CancelAsync()
                If .bwLoadShowInfo.IsBusy Then .bwLoadShowInfo.CancelAsync()
                If .bwLoadSeasonInfo.IsBusy Then .bwLoadSeasonInfo.CancelAsync()
                If .bwLoadEpInfo.IsBusy Then .bwLoadEpInfo.CancelAsync()

                While .bwDownloadPic.IsBusy OrElse .bwLoadMovieInfo.IsBusy OrElse .bwLoadMovieSetInfo.IsBusy OrElse _
                    .bwLoadShowInfo.IsBusy OrElse .bwLoadSeasonInfo.IsBusy OrElse .bwLoadEpInfo.IsBusy OrElse _
                    .bwLoadMovieSetPosters.IsBusy
                    Application.DoEvents()
                    Threading.Thread.Sleep(50)
                End While

                If .pbFanart.Image IsNot Nothing Then
                    .pbFanart.Image.Dispose()
                    .pbFanart.Image = Nothing
                End If
                .MainFanart.Clear()

                If .pbBanner.Image IsNot Nothing Then
                    .pbBanner.Image.Dispose()
                    .pbBanner.Image = Nothing
                End If
                .pnlBanner.Visible = False
                .MainBanner.Clear()

                If .pbCharacterArt.Image IsNot Nothing Then
                    .pbCharacterArt.Image.Dispose()
                    .pbCharacterArt.Image = Nothing
                End If
                .pnlCharacterArt.Visible = False
                .MainCharacterArt.Clear()

                If .pbClearArt.Image IsNot Nothing Then
                    .pbClearArt.Image.Dispose()
                    .pbClearArt.Image = Nothing
                End If
                .pnlClearArt.Visible = False
                .MainClearArt.Clear()

                If .pbClearLogo.Image IsNot Nothing Then
                    .pbClearLogo.Image.Dispose()
                    .pbClearLogo.Image = Nothing
                End If
                .pnlClearLogo.Visible = False
                .MainClearLogo.Clear()

                If .pbPoster.Image IsNot Nothing Then
                    .pbPoster.Image.Dispose()
                    .pbPoster.Image = Nothing
                End If
                .pnlPoster.Visible = False
                .MainPoster.Clear()

                If .pbFanartSmall.Image IsNot Nothing Then
                    .pbFanartSmall.Image.Dispose()
                    .pbFanartSmall.Image = Nothing
                End If
                .pnlFanartSmall.Visible = False
                .MainFanartSmall.Clear()

                If .pbLandscape.Image IsNot Nothing Then
                    .pbLandscape.Image.Dispose()
                    .pbLandscape.Image = Nothing
                End If
                .pnlLandscape.Visible = False
                .MainLandscape.Clear()

                If .pbDiscArt.Image IsNot Nothing Then
                    .pbDiscArt.Image.Dispose()
                    .pbDiscArt.Image = Nothing
                End If
                .pnlDiscArt.Visible = False
                .MainDiscArt.Clear()

                'remove all the current genres
                Try
                    For iDel As Integer = 0 To .pnlGenre.Count - 1
                        .scMain.Panel2.Controls.Remove(.pbGenre(iDel))
                        .scMain.Panel2.Controls.Remove(.pnlGenre(iDel))
                    Next
                Catch
                End Try

                If .pbMPAA.Image IsNot Nothing Then
                    .pbMPAA.Image = Nothing
                End If
                .pnlMPAA.Visible = False

                .lblFanartSmallSize.Text = String.Empty
                .lblTitle.Text = String.Empty
                .lblOriginalTitle.Text = String.Empty
                .lblPosterSize.Text = String.Empty
                .lblRating.Text = String.Empty
                .lblRuntime.Text = String.Empty
                .lblStudio.Text = String.Empty
                .pnlTop250.Visible = False
                .lblTop250.Text = String.Empty
                .pbStar1.Image = Nothing
                .pbStar2.Image = Nothing
                .pbStar3.Image = Nothing
                .pbStar4.Image = Nothing
                .pbStar5.Image = Nothing
                .pbStar6.Image = Nothing
                .pbStar7.Image = Nothing
                .pbStar8.Image = Nothing
                .pbStar9.Image = Nothing
                .pbStar10.Image = Nothing
                ToolTips.SetToolTip(pbStar1, "")
                ToolTips.SetToolTip(pbStar2, "")
                ToolTips.SetToolTip(pbStar3, "")
                ToolTips.SetToolTip(pbStar4, "")
                ToolTips.SetToolTip(pbStar5, "")
                ToolTips.SetToolTip(pbStar6, "")
                ToolTips.SetToolTip(pbStar7, "")
                ToolTips.SetToolTip(pbStar8, "")
                ToolTips.SetToolTip(pbStar9, "")
                ToolTips.SetToolTip(pbStar10, "")

                .lstActors.Items.Clear()
                If .alActors IsNot Nothing Then
                    .alActors.Clear()
                    .alActors = Nothing
                End If
                If .pbActors.Image IsNot Nothing Then
                    .pbActors.Image.Dispose()
                    .pbActors.Image = Nothing
                End If
                .MainActors.Clear()
                .lblDirector.Text = String.Empty
                .lblReleaseDate.Text = String.Empty
                .txtCerts.Text = String.Empty
                .txtIMDBID.Text = String.Empty
                .txtFilePath.Text = String.Empty
                .txtOutline.Text = String.Empty
                .txtPlot.Text = String.Empty
                .txtTMDBID.Text = String.Empty
                .lblTagline.Text = String.Empty
                If .pbMPAA.Image IsNot Nothing Then
                    .pbMPAA.Image.Dispose()
                    .pbMPAA.Image = Nothing
                End If
                .pbStudio.Image = Nothing
                .pbVideo.Image = Nothing
                .pbVType.Image = Nothing
                .pbAudio.Image = Nothing
                .pbResolution.Image = Nothing
                .pbChannels.Image = Nothing
                .pbAudioLang0.Image = Nothing
                .pbAudioLang1.Image = Nothing
                .pbAudioLang2.Image = Nothing
                .pbAudioLang3.Image = Nothing
                .pbAudioLang4.Image = Nothing
                .pbAudioLang5.Image = Nothing
                .pbAudioLang6.Image = Nothing
                ToolTips.SetToolTip(.pbAudioLang0, "")
                ToolTips.SetToolTip(.pbAudioLang1, "")
                ToolTips.SetToolTip(.pbAudioLang2, "")
                ToolTips.SetToolTip(.pbAudioLang3, "")
                ToolTips.SetToolTip(.pbAudioLang4, "")
                ToolTips.SetToolTip(.pbAudioLang5, "")
                ToolTips.SetToolTip(.pbAudioLang6, "")
                .pbSubtitleLang0.Image = Nothing
                .pbSubtitleLang1.Image = Nothing
                .pbSubtitleLang2.Image = Nothing
                .pbSubtitleLang3.Image = Nothing
                .pbSubtitleLang4.Image = Nothing
                .pbSubtitleLang5.Image = Nothing
                .pbSubtitleLang6.Image = Nothing
                ToolTips.SetToolTip(.pbSubtitleLang0, "")
                ToolTips.SetToolTip(.pbSubtitleLang1, "")
                ToolTips.SetToolTip(.pbSubtitleLang2, "")
                ToolTips.SetToolTip(.pbSubtitleLang3, "")
                ToolTips.SetToolTip(.pbSubtitleLang4, "")
                ToolTips.SetToolTip(.pbSubtitleLang5, "")
                ToolTips.SetToolTip(.pbSubtitleLang6, "")

                .txtMetaData.Text = String.Empty
                .pnlTop.Visible = False
                '.tslStatus.Text = String.Empty

                .lvMoviesInSet.Items.Clear()
                .ilMoviesInSet.Images.Clear()

                Application.DoEvents()
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Function CheckColumnHide_Movies(ByVal ColumnName As String) As Boolean
        Dim lsColumn As Settings.ListSorting = Master.eSettings.MovieGeneralMediaListSorting.FirstOrDefault(Function(l) l.Column = ColumnName)
        Return If(lsColumn Is Nothing, True, lsColumn.Hide)
    End Function

    Private Function CheckColumnHide_MovieSets(ByVal ColumnName As String) As Boolean
        Dim lsColumn As Settings.ListSorting = Master.eSettings.MovieSetGeneralMediaListSorting.FirstOrDefault(Function(l) l.Column = ColumnName)
        Return If(lsColumn Is Nothing, True, lsColumn.Hide)
    End Function

    Private Function CheckColumnHide_TVEpisodes(ByVal ColumnName As String) As Boolean
        Dim lsColumn As Settings.ListSorting = Master.eSettings.TVGeneralEpisodeListSorting.FirstOrDefault(Function(l) l.Column = ColumnName)
        Return If(lsColumn Is Nothing, True, lsColumn.Hide)
    End Function

    Private Function CheckColumnHide_TVSeasons(ByVal ColumnName As String) As Boolean
        Dim lsColumn As Settings.ListSorting = Master.eSettings.TVGeneralSeasonListSorting.FirstOrDefault(Function(l) l.Column = ColumnName)
        Return If(lsColumn Is Nothing, True, lsColumn.Hide)
    End Function

    Private Function CheckColumnHide_TVShows(ByVal ColumnName As String) As Boolean
        Dim lsColumn As Settings.ListSorting = Master.eSettings.TVGeneralShowListSorting.FirstOrDefault(Function(l) l.Column = ColumnName)
        Return If(lsColumn Is Nothing, True, lsColumn.Hide)
    End Function

    Public Sub LoadMedia(ByVal Scan As Structures.Scans, Optional ByVal SourceName As String = "", Optional ByVal Folder As String = "")
        Try
            Me.SetStatus(Master.eLang.GetString(116, "Performing Preliminary Tasks (Gathering Data)..."))
            Me.tspbLoading.ProgressBar.Style = ProgressBarStyle.Marquee
            Me.tspbLoading.Visible = True

            Application.DoEvents()

            Me.ClearInfo()
            Me.ClearFilters_Movies()
            Me.ClearFilters_MovieSets()
            Me.ClearFilters_Shows()
            Me.EnableFilters_Movies(False)
            Me.EnableFilters_MovieSets(False)
            Me.EnableFilters_Shows(False)

            Me.SetControlsEnabled(False)
            Me.txtSearchMovies.Text = String.Empty
            Me.txtSearchMovieSets.Text = String.Empty
            Me.txtSearchShows.Text = String.Empty

            Me.fScanner.CancelAndWait()

            If Scan.Movies Then
                Me.prevRow_Movie = -1
                Me.dgvMovies.DataSource = Nothing
            End If

            If Scan.MovieSets Then
                Me.prevRow_MovieSet = -1
                Me.dgvMovieSets.DataSource = Nothing
            End If

            If Scan.TV Then
                Me.currList = 0
                Me.prevRow_TVShow = -1
                Me.prevRow_TVSeason = -1
                Me.prevRow_TVEpisode = -1
                Me.dgvTVShows.DataSource = Nothing
                Me.dgvTVSeasons.DataSource = Nothing
                Me.dgvTVEpisodes.DataSource = Nothing
            End If

            Me.fScanner.Start(Scan, SourceName, Folder)

        Catch ex As Exception
            Me.LoadingDone = True
            Me.EnableFilters_Movies(True)
            Me.EnableFilters_MovieSets(True)
            Me.EnableFilters_Shows(True)
            Me.SetControlsEnabled(True)
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpAbout.Click
        Using dAbout As New dlgAbout
            dAbout.ShowDialog()
        End Using
    End Sub

    Private Sub mnuMainToolsExportMovies_Click(sender As Object, e As EventArgs) Handles mnuMainToolsExportMovies.Click
        Try
            Dim table As New DataTable
            Dim ds As New DataSet
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = "SELECT * FROM movie INNER JOIN MoviesVStreams ON (MoviesVStreams.MovieID = movie.idMovie) INNER JOIN MoviesAStreams ON (MoviesAStreams.MovieID = movie.idMovie);"
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    ds.Tables.Add(table)
                    ds.EnforceConstraints = False
                    table.Load(SQLreader)
                End Using
            End Using

            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.FileName = "export_movies" + ".xml"
            saveFileDialog1.Filter = "xml files (*.xml)|*.xml"
            saveFileDialog1.FilterIndex = 2
            saveFileDialog1.RestoreDirectory = True

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                table.WriteXml(saveFileDialog1.FileName)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub mnuMainToolsExportTvShows_Click(sender As Object, e As EventArgs) Handles mnuMainToolsExportTvShows.Click
        Try
            Dim table As New DataTable
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = "Select * from tvshow;"
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    'Load the SqlDataReader object to the DataTable object as follows. 
                    table.Load(SQLreader)
                End Using
            End Using

            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.FileName = "export_tvshows" + ".xml"
            saveFileDialog1.Filter = "xml files (*.xml)|*.xml"
            saveFileDialog1.FilterIndex = 2
            saveFileDialog1.RestoreDirectory = True

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                table.WriteXml(saveFileDialog1.FileName)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub ApplyTheme(ByVal tType As Theming.ThemeType)
        Me.pnlInfoPanel.SuspendLayout()

        Me.currThemeType = tType

        tTheme.ApplyTheme(tType)

        Me.tmrAni.Stop()

        Dim currMainTabTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)

        Select Case If(currMainTabTag.ContentType = Enums.ContentType.Movie, MovieInfoPanelState, If(currMainTabTag.ContentType = Enums.ContentType.MovieSet, MovieSetInfoPanelState, TVShowInfoPanelState))
            Case 1
                If Me.btnMid.Visible Then
                    Me.pnlInfoPanel.Height = Me._ipmid
                    Me.btnUp.Enabled = True
                    Me.btnMid.Enabled = False
                    Me.btnDown.Enabled = True
                ElseIf Me.btnUp.Visible Then
                    Me.pnlInfoPanel.Height = Me._ipup
                    If currMainTabTag.ContentType = Enums.ContentType.Movie Then
                        MovieInfoPanelState = 2
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
                        MovieSetInfoPanelState = 2
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
                        TVShowInfoPanelState = 2
                    End If
                    Me.btnUp.Enabled = False
                    Me.btnMid.Enabled = True
                    Me.btnDown.Enabled = True
                Else
                    Me.pnlInfoPanel.Height = 25
                    If currMainTabTag.ContentType = Enums.ContentType.Movie Then
                        MovieInfoPanelState = 0
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
                        MovieSetInfoPanelState = 0
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
                        TVShowInfoPanelState = 0
                    End If
                    Me.btnUp.Enabled = True
                    Me.btnMid.Enabled = True
                    Me.btnDown.Enabled = False
                End If
            Case 2
                If Me.btnUp.Visible Then
                    Me.pnlInfoPanel.Height = Me._ipup
                    Me.btnUp.Enabled = False
                    Me.btnMid.Enabled = True
                    Me.btnDown.Enabled = True
                ElseIf Me.btnMid.Visible Then
                    Me.pnlInfoPanel.Height = Me._ipmid

                    If currMainTabTag.ContentType = Enums.ContentType.Movie Then
                        MovieInfoPanelState = 1
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
                        MovieSetInfoPanelState = 1
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
                        TVShowInfoPanelState = 1
                    End If

                    Me.btnUp.Enabled = True
                    Me.btnMid.Enabled = False
                    Me.btnDown.Enabled = True
                Else
                    Me.pnlInfoPanel.Height = 25
                    If currMainTabTag.ContentType = Enums.ContentType.Movie Then
                        MovieInfoPanelState = 0
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
                        MovieSetInfoPanelState = 0
                    ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
                        TVShowInfoPanelState = 0
                    End If
                    Me.btnUp.Enabled = True
                    Me.btnMid.Enabled = True
                    Me.btnDown.Enabled = False
                End If
            Case Else
                Me.pnlInfoPanel.Height = 25
                If currMainTabTag.ContentType = Enums.ContentType.Movie Then
                    MovieInfoPanelState = 0
                ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
                    MovieSetInfoPanelState = 0
                ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
                    TVShowInfoPanelState = 0
                End If

                Me.btnUp.Enabled = True
                Me.btnMid.Enabled = True
                Me.btnDown.Enabled = False
        End Select

        Me.pbActLoad.Visible = False
        Me.pbActors.Image = My.Resources.actor_silhouette
        Me.pbMILoading.Visible = False

        Me.pnlInfoPanel.ResumeLayout()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancel.Visible = False
        lblCanceling.Visible = True
        prbCanceling.Visible = True

        If Me.bwMovieScraper.IsBusy Then Me.bwMovieScraper.CancelAsync()
        If Me.bwMovieSetScraper.IsBusy Then Me.bwMovieSetScraper.CancelAsync()
        If Me.bwReload_Movies.IsBusy Then Me.bwReload_Movies.CancelAsync()
        If Me.bwReload_MovieSets.IsBusy Then Me.bwReload_MovieSets.CancelAsync()
        If Me.bwReload_TVShows.IsBusy Then Me.bwReload_TVShows.CancelAsync()
        If Me.bwRewrite_Movies.IsBusy Then Me.bwRewrite_Movies.CancelAsync()
        If Me.bwNonScrape.IsBusy Then Me.bwNonScrape.CancelAsync()
        If Me.bwTVScraper.IsBusy Then Me.bwTVScraper.CancelAsync()
        While Me.bwMovieScraper.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse Me.bwMovieSetScraper.IsBusy OrElse Me.bwReload_MovieSets.IsBusy OrElse _
            Me.bwNonScrape.IsBusy OrElse Me.bwReload_TVShows.IsBusy OrElse Me.bwRewrite_Movies.IsBusy OrElse Me.bwTVScraper.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub btnClearFilters_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearFilters_Movies.Click
        Me.ClearFilters_Movies(True)
    End Sub

    Private Sub btnClearFilters_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearFilters_MovieSets.Click
        Me.ClearFilters_MovieSets(True)
    End Sub

    Private Sub btnClearFilters_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearFilters_Shows.Click
        Me.ClearFilters_Shows(True)
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim currMainTabTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
        Me.tcMain.Focus()
        If currMainTabTag.ContentType = Enums.ContentType.Movie Then
            Me.MovieInfoPanelState = 0
        ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
            Me.MovieSetInfoPanelState = 0
        ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
            Me.TVShowInfoPanelState = 0
        End If
        Me.aniRaise = False
        Me.tmrAni.Start()
    End Sub

    Private Sub btnFilterDown_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDown_Movies.Click
        Me.FilterRaise_Movies = False
        FilterMovement_Movies()
    End Sub

    Private Sub btnFilterDown_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDown_MovieSets.Click
        Me.FilterRaise_MovieSets = False
        FilterMovement_MovieSets()
    End Sub

    Private Sub btnFilterDown_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterDown_Shows.Click
        Me.FilterRaise_Shows = False
        FilterMovement_Shows()
    End Sub

    Private Sub btnFilterUp_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterUp_Movies.Click
        Me.FilterRaise_Movies = True
        FilterMovement_Movies()
    End Sub

    Private Sub btnFilterUp_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterUp_MovieSets.Click
       
        Me.FilterRaise_MovieSets = True
        FilterMovement_MovieSets()
    End Sub

    Private Sub btnFilterUp_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterUp_Shows.Click
        Me.FilterRaise_Shows = True
        FilterMovement_Shows()
    End Sub



    Private Sub btnMarkAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarkAll.Click
        Try
            Dim MarkAll As Boolean = Not btnMarkAll.Text = Master.eLang.GetString(105, "Unmark All")
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
                    SQLcommand.CommandText = "UPDATE movie SET mark = (?);"
                    parMark.Value = MarkAll
                    SQLcommand.ExecuteNonQuery()
                End Using
                SQLtransaction.Commit()
            End Using
            For Each drvRow As DataRow In dtMovies.Rows
                drvRow.Item("Mark") = MarkAll
            Next
            dgvMovies.Refresh()
            btnMarkAll.Text = If(Not MarkAll, Master.eLang.GetString(35, "Mark All"), Master.eLang.GetString(105, "Unmark All"))
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnMid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMid.Click
        Dim currMainTabTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
        Me.tcMain.Focus()
        If Me.pnlInfoPanel.Height = Me.IPUp Then
            Me.aniRaise = False
        Else
            Me.aniRaise = True
        End If

        If currMainTabTag.ContentType = Enums.ContentType.Movie Then
            Me.MovieInfoPanelState = 1
        ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
            Me.MovieSetInfoPanelState = 1
        ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
            Me.TVShowInfoPanelState = 1
        End If

        Me.tmrAni.Start()
    End Sub

    Private Sub btnMIRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMetaDataRefresh.Click
        Dim currMainTabTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)

        If currMainTabTag.ContentType = Enums.ContentType.Movie Then
            If Not String.IsNullOrEmpty(Me.currMovie.Filename) AndAlso Me.dgvMovies.SelectedRows.Count > 0 Then
                Me.LoadInfo_Movie(Convert.ToInt32(Me.currMovie.ID), Me.currMovie.Filename, False, True, True)
            End If
        ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
            'no NFO support for MovieSets
        ElseIf currMainTabTag.ContentType = Enums.ContentType.TV AndAlso Not String.IsNullOrEmpty(Me.currTV.Filename) AndAlso Me.dgvTVEpisodes.SelectedRows.Count > 0 Then
            Me.SetControlsEnabled(False, True)

            If Me.bwMetaData.IsBusy Then Me.bwMetaData.CancelAsync()

            Me.txtMetaData.Clear()
            Me.pbMILoading.Visible = True

            Me.bwMetaData = New System.ComponentModel.BackgroundWorker
            Me.bwMetaData.WorkerSupportsCancellation = True
            Me.bwMetaData.RunWorkerAsync(New Arguments With {.DBElement = Me.currTV, .IsTV = True, .setEnabled = True})
        End If
    End Sub
    ''' <summary>
    ''' Launch video using system default player
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlay.Click
        Functions.Launch(Me.txtFilePath.Text, True)
        'Try
        '    If Not String.IsNullOrEmpty(Me.txtFilePath.Text) Then
        '        If File.Exists(Me.txtFilePath.Text) Then
        '            If Master.isWindows Then
        '                Process.Start(String.Concat("""", Me.txtFilePath.Text, """"))
        '            Else
        '                Using Explorer As New Process
        '                    Explorer.StartInfo.FileName = "xdg-open"
        '                    Explorer.StartInfo.Arguments = String.Format("""{0}""", Me.txtFilePath.Text)
        '                    Explorer.Start()
        '                End Using
        '            End If

        '        End If
        '    End If
        'Catch ex As Exception
        '    logger.Error(New StackFrame().GetMethod().Name, ex)
        'End Try
    End Sub
    ''' <summary>
    ''' sorts the movielist by adding date
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>this filter is inverted (DESC first) to get the newest title on the top of the list</remarks>
    Private Sub btnFilterSortDateAdded_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterSortDateAdded_Movies.Click
        If Me.dgvMovies.RowCount > 0 Then
            Me.btnFilterSortRating_Movies.Tag = String.Empty
            Me.btnFilterSortRating_Movies.Image = Nothing
            Me.btnFilterSortDateModified_Movies.Tag = String.Empty
            Me.btnFilterSortDateModified_Movies.Image = Nothing
            Me.btnFilterSortTitle_Movies.Tag = String.Empty
            Me.btnFilterSortTitle_Movies.Image = Nothing
            Me.btnFilterSortYear_Movies.Tag = String.Empty
            Me.btnFilterSortYear_Movies.Image = Nothing
            If Me.btnFilterSortDateAdded_Movies.Tag.ToString = "DESC" Then
                Me.btnFilterSortDateAdded_Movies.Tag = "ASC"
                Me.btnFilterSortDateAdded_Movies.Image = My.Resources.asc
                Me.dgvMovies.Sort(Me.dgvMovies.Columns("DateAdded"), ComponentModel.ListSortDirection.Ascending)
            Else
                Me.btnFilterSortDateAdded_Movies.Tag = "DESC"
                Me.btnFilterSortDateAdded_Movies.Image = My.Resources.desc
                Me.dgvMovies.Sort(Me.dgvMovies.Columns("DateAdded"), ComponentModel.ListSortDirection.Descending)
            End If

            Me.SaveFilter_Movies()
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by last modification date
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>this filter is inverted (DESC first) to get the latest modified title on the top of the list</remarks>
    Private Sub btnFilterSortDateModified_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterSortDateModified_Movies.Click
        If Me.dgvMovies.RowCount > 0 Then
            Me.btnFilterSortDateAdded_Movies.Tag = String.Empty
            Me.btnFilterSortDateAdded_Movies.Image = Nothing
            Me.btnFilterSortRating_Movies.Tag = String.Empty
            Me.btnFilterSortRating_Movies.Image = Nothing
            Me.btnFilterSortTitle_Movies.Tag = String.Empty
            Me.btnFilterSortTitle_Movies.Image = Nothing
            Me.btnFilterSortYear_Movies.Tag = String.Empty
            Me.btnFilterSortYear_Movies.Image = Nothing
            If Me.btnFilterSortDateModified_Movies.Tag.ToString = "DESC" Then
                Me.btnFilterSortDateModified_Movies.Tag = "ASC"
                Me.btnFilterSortDateModified_Movies.Image = My.Resources.asc
                Me.dgvMovies.Sort(Me.dgvMovies.Columns("DateModified"), ComponentModel.ListSortDirection.Ascending)
            Else
                Me.btnFilterSortDateModified_Movies.Tag = "DESC"
                Me.btnFilterSortDateModified_Movies.Image = My.Resources.desc
                Me.dgvMovies.Sort(Me.dgvMovies.Columns("DateModified"), ComponentModel.ListSortDirection.Descending)
            End If

            Me.SaveFilter_Movies()
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by sort title
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFilterSortTitle_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterSortTitle_Movies.Click
        If Me.dgvMovies.RowCount > 0 Then
            Me.btnFilterSortDateAdded_Movies.Tag = String.Empty
            Me.btnFilterSortDateAdded_Movies.Image = Nothing
            Me.btnFilterSortDateModified_Movies.Tag = String.Empty
            Me.btnFilterSortDateModified_Movies.Image = Nothing
            Me.btnFilterSortRating_Movies.Tag = String.Empty
            Me.btnFilterSortRating_Movies.Image = Nothing
            Me.btnFilterSortYear_Movies.Tag = String.Empty
            Me.btnFilterSortYear_Movies.Image = Nothing
            If Me.btnFilterSortTitle_Movies.Tag.ToString = "ASC" Then
                Me.btnFilterSortTitle_Movies.Tag = "DSC"
                Me.btnFilterSortTitle_Movies.Image = My.Resources.desc
                Me.dgvMovies.Sort(Me.dgvMovies.Columns("SortedTitle"), ComponentModel.ListSortDirection.Descending)
            Else
                Me.btnFilterSortTitle_Movies.Tag = "ASC"
                Me.btnFilterSortTitle_Movies.Image = My.Resources.asc
                Me.dgvMovies.Sort(Me.dgvMovies.Columns("SortedTitle"), ComponentModel.ListSortDirection.Ascending)
            End If

            Me.SaveFilter_Movies()
        End If
    End Sub
    ''' <summary>
    ''' sorts the tvshowlist by sort title
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFilterSortTitle_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterSortTitle_Shows.Click
        If Me.dgvTVShows.RowCount > 0 Then
            'Me.btnFilterSortDateAdded_Shows.Tag = String.Empty
            'Me.btnFilterSortDateAdded_Shows.Image = Nothing
            'Me.btnFilterSortDateModified_Shows.Tag = String.Empty
            'Me.btnFilterSortDateModified_Shows.Image = Nothing
            'Me.btnFilterSortRating_Shows.Tag = String.Empty
            'Me.btnFilterSortRating_Shows.Image = Nothing
            'Me.btnFilterSortYear_Shows.Tag = String.Empty
            'Me.btnFilterSortYear_Shows.Image = Nothing
            If Me.btnFilterSortTitle_Shows.Tag.ToString = "ASC" Then
                Me.btnFilterSortTitle_Shows.Tag = "DSC"
                Me.btnFilterSortTitle_Shows.Image = My.Resources.desc
                Me.dgvTVShows.Sort(Me.dgvTVShows.Columns("SortedTitle"), ComponentModel.ListSortDirection.Descending)
            Else
                Me.btnFilterSortTitle_Shows.Tag = "ASC"
                Me.btnFilterSortTitle_Shows.Image = My.Resources.asc
                Me.dgvTVShows.Sort(Me.dgvTVShows.Columns("SortedTitle"), ComponentModel.ListSortDirection.Ascending)
            End If

            Me.SaveFilter_Shows()
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by rating
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>this filter is inverted (DESC first) to get the highest rated title on the top of the list</remarks>
    Private Sub btnFilterSortRating_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterSortRating_Movies.Click
        If Me.dgvMovies.RowCount > 0 Then
            Me.btnFilterSortDateAdded_Movies.Tag = String.Empty
            Me.btnFilterSortDateAdded_Movies.Image = Nothing
            Me.btnFilterSortDateModified_Movies.Tag = String.Empty
            Me.btnFilterSortDateModified_Movies.Image = Nothing
            Me.btnFilterSortTitle_Movies.Tag = String.Empty
            Me.btnFilterSortTitle_Movies.Image = Nothing
            Me.btnFilterSortYear_Movies.Tag = String.Empty
            Me.btnFilterSortYear_Movies.Image = Nothing
            If Me.btnFilterSortRating_Movies.Tag.ToString = "DESC" Then
                Me.btnFilterSortRating_Movies.Tag = "ASC"
                Me.btnFilterSortRating_Movies.Image = My.Resources.asc
                Me.dgvMovies.Sort(Me.dgvMovies.Columns("Rating"), ComponentModel.ListSortDirection.Ascending)
            Else
                Me.btnFilterSortRating_Movies.Tag = "DESC"
                Me.btnFilterSortRating_Movies.Image = My.Resources.desc
                Me.dgvMovies.Sort(Me.dgvMovies.Columns("Rating"), ComponentModel.ListSortDirection.Descending)
            End If

            Me.SaveFilter_Movies()
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by year
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>this filter is inverted (DESC first) to get the highest year title on the top of the list</remarks>
    Private Sub btnFilterSortYear_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilterSortYear_Movies.Click
        If Me.dgvMovies.RowCount > 0 Then
            Me.btnFilterSortDateAdded_Movies.Tag = String.Empty
            Me.btnFilterSortDateAdded_Movies.Image = Nothing
            Me.btnFilterSortDateModified_Movies.Tag = String.Empty
            Me.btnFilterSortDateModified_Movies.Image = Nothing
            Me.btnFilterSortRating_Movies.Tag = String.Empty
            Me.btnFilterSortRating_Movies.Image = Nothing
            Me.btnFilterSortTitle_Movies.Tag = String.Empty
            Me.btnFilterSortTitle_Movies.Image = Nothing
            If Me.btnFilterSortYear_Movies.Tag.ToString = "DESC" Then
                Me.btnFilterSortYear_Movies.Tag = "ASC"
                Me.btnFilterSortYear_Movies.Image = My.Resources.asc
                Me.dgvMovies.Sort(Me.dgvMovies.Columns("Year"), ComponentModel.ListSortDirection.Ascending)
            Else
                Me.btnFilterSortYear_Movies.Tag = "DESC"
                Me.btnFilterSortYear_Movies.Image = My.Resources.desc
                Me.dgvMovies.Sort(Me.dgvMovies.Columns("Year"), ComponentModel.ListSortDirection.Descending)
            End If

            Me.SaveFilter_Movies()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim currMainTabTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
        Me.tcMain.Focus()
        If currMainTabTag.ContentType = Enums.ContentType.Movie Then
            Me.MovieInfoPanelState = 2
        ElseIf currMainTabTag.ContentType = Enums.ContentType.MovieSet Then
            Me.MovieSetInfoPanelState = 2
        ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
            Me.TVShowInfoPanelState = 2
        End If
        Me.aniRaise = True
        Me.tmrAni.Start()
    End Sub

    Private Sub BuildStars(ByVal sinRating As Single)
        '//
        ' Convert # rating to star images
        '\\

        Try
            With Me
                .pbStar1.Image = Nothing
                .pbStar2.Image = Nothing
                .pbStar3.Image = Nothing
                .pbStar4.Image = Nothing
                .pbStar5.Image = Nothing
                .pbStar6.Image = Nothing
                .pbStar7.Image = Nothing
                .pbStar8.Image = Nothing
                .pbStar9.Image = Nothing
                .pbStar10.Image = Nothing

                Dim tTip As String = String.Concat(Master.eLang.GetString(245, "Rating:"), String.Format(" {0:N}", sinRating))
                ToolTips.SetToolTip(.pbStar1, tTip)
                ToolTips.SetToolTip(.pbStar2, tTip)
                ToolTips.SetToolTip(.pbStar3, tTip)
                ToolTips.SetToolTip(.pbStar4, tTip)
                ToolTips.SetToolTip(.pbStar5, tTip)
                ToolTips.SetToolTip(.pbStar6, tTip)
                ToolTips.SetToolTip(.pbStar7, tTip)
                ToolTips.SetToolTip(.pbStar8, tTip)
                ToolTips.SetToolTip(.pbStar9, tTip)
                ToolTips.SetToolTip(.pbStar10, tTip)

                If sinRating >= 0.5 Then ' if rating is less than .5 out of ten, consider it a 0
                    Select Case (sinRating)
                        Case Is <= 0.5
                            .pbStar1.Image = My.Resources.starhalf
                            .pbStar2.Image = My.Resources.starempty
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 1
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.starempty
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 1.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.starhalf
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 2
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 2.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.starhalf
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 3
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 3.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.starhalf
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 4
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 4.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.starhalf
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 5.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.starhalf
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 6
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 6.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.starhalf
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 7
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 7.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.starhalf
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 8
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 8.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.starhalf
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 9
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.star
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 9.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.star
                            .pbStar10.Image = My.Resources.starhalf
                        Case Else
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.star
                            .pbStar10.Image = My.Resources.star
                    End Select
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwCleanDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCleanDB.DoWork
        Master.DB.Clean(True, True, True)
    End Sub

    Private Sub bwCleanDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCleanDB.RunWorkerCompleted
        Me.SetStatus(String.Empty)
        Me.tspbLoading.Visible = False

        Me.FillList(True, True, True)
    End Sub

    Private Sub bwDownloadPic_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadPic.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Try

            sHTTP.StartDownloadImage(Args.pURL)

            While sHTTP.IsDownloading
                Application.DoEvents()
                If Me.bwDownloadPic.CancellationPending Then
                    e.Cancel = True
                    sHTTP.Cancel()
                    Return
                End If
                Threading.Thread.Sleep(50)
            End While

            e.Result = New Results With {.Result = sHTTP.Image}
        Catch ex As Exception
            e.Result = New Results With {.Result = Nothing}
            e.Cancel = True
        End Try
    End Sub

    Private Sub bwDownloadPic_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwDownloadPic.RunWorkerCompleted
        '//
        ' Thread finished: display pic if it was able to get one
        '\\

        Me.pbActLoad.Visible = False

        If e.Cancelled Then
            Me.pbActors.Image = My.Resources.actor_silhouette
        Else
            Dim Res As Results = DirectCast(e.Result, Results)

            If Res.Result IsNot Nothing Then
                Me.pbActors.Image = Res.Result
            Else
                Me.pbActors.Image = My.Resources.actor_silhouette
            End If
        End If
    End Sub

    Private Sub bwLoadEpInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadEpInfo.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Me.MainActors.Clear()
        Me.MainBanner.Clear()
        Me.MainCharacterArt.Clear()
        Me.MainClearArt.Clear()
        Me.MainClearLogo.Clear()
        Me.MainDiscArt.Clear()
        Me.MainFanart.Clear()
        Me.MainFanartSmall.Clear()
        Me.MainLandscape.Clear()
        Me.MainPoster.Clear()

        If bwLoadEpInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        Me.currTV = Master.DB.LoadTVEpisodeFromDB(Args.ID, True)

        If bwLoadEpInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Not Master.eSettings.GeneralHideFanartSmall Then Me.MainFanartSmall = Me.currTV.ImagesContainer.Fanart.ImageOriginal
        If Not Master.eSettings.GeneralHidePoster Then Me.MainPoster = Me.currTV.ImagesContainer.Poster.ImageOriginal

        If bwLoadEpInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Not Master.eSettings.GeneralHideFanart Then
            Dim NeedsGS As Boolean = False
            If Me.currTV.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing Then
                Me.MainFanart = Me.currTV.ImagesContainer.Fanart.ImageOriginal
            Else
                Dim SeasonID As Long = Master.DB.GetTVSeasonIDFromEpisode(Me.currTV)
                Dim TVSeasonFanart As String = Master.DB.GetArtForItem(SeasonID, "season", "fanart")
                If Not String.IsNullOrEmpty(TVSeasonFanart) Then
                    Me.MainFanart.FromFile(TVSeasonFanart)
                    NeedsGS = True
                Else
                    Dim TVShowFanart As String = Master.DB.GetArtForItem(Me.currTV.ShowID, "tvshow", "fanart")
                    If Not String.IsNullOrEmpty(TVShowFanart) Then
                        Me.MainFanart.FromFile(TVShowFanart)
                        NeedsGS = True
                    End If
                End If
            End If

            If Me.MainFanart.Image IsNot Nothing Then
                If String.IsNullOrEmpty(Me.currTV.Filename) Then
                    Me.MainFanart = ImageUtils.AddMissingStamp(Me.MainFanart)
                ElseIf NeedsGS Then
                    Me.MainFanart = ImageUtils.GrayScale(Me.MainFanart)
                End If
            End If
        End If

        'wait for mediainfo to update the nfo
        While bwMetaData.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        If bwLoadEpInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub bwLoadEpInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadEpInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                Me.FillScreenInfoWith_TVEpisode()
            Else
                Me.SetControlsEnabled(True)
            End If

            Me.dgvTVEpisodes.ResumeLayout()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwLoadMovieInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadMovieInfo.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Me.MainActors.Clear()
        Me.MainBanner.Clear()
        Me.MainCharacterArt.Clear()
        Me.MainClearArt.Clear()
        Me.MainClearLogo.Clear()
        Me.MainDiscArt.Clear()
        Me.MainFanart.Clear()
        Me.MainFanartSmall.Clear()
        Me.MainLandscape.Clear()
        Me.MainPoster.Clear()

        If bwLoadMovieInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        Me.currMovie = Master.DB.LoadMovieFromDB(Args.ID, True, True)

        If bwLoadMovieInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Not Master.eSettings.GeneralHideBanner Then Me.MainBanner = Me.currMovie.ImagesContainer.Banner.ImageOriginal
        If Not Master.eSettings.GeneralHideClearArt Then Me.MainClearArt = Me.currMovie.ImagesContainer.ClearArt.ImageOriginal
        If Not Master.eSettings.GeneralHideClearLogo Then Me.MainClearLogo = Me.currMovie.ImagesContainer.ClearLogo.ImageOriginal
        If Not Master.eSettings.GeneralHideDiscArt Then Me.MainDiscArt = Me.currMovie.ImagesContainer.DiscArt.ImageOriginal
        If Not Master.eSettings.GeneralHideFanart Then Me.MainFanart = Me.currMovie.ImagesContainer.Fanart.ImageOriginal
        If Not Master.eSettings.GeneralHideFanartSmall Then Me.MainFanartSmall = Me.currMovie.ImagesContainer.Fanart.ImageOriginal
        If Not Master.eSettings.GeneralHideLandscape Then Me.MainLandscape = Me.currMovie.ImagesContainer.Landscape.ImageOriginal
        If Not Master.eSettings.GeneralHidePoster Then Me.MainPoster = Me.currMovie.ImagesContainer.Poster.ImageOriginal
        'read nfo if it's there

        'wait for mediainfo to update the nfo
        While bwMetaData.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        If bwLoadMovieInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub bwLoadMovieInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadMovieInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                Me.FillScreenInfoWith_Movie()
            Else
                If Not bwMovieScraper.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso Not bwRewrite_Movies.IsBusy AndAlso Not bwCleanDB.IsBusy AndAlso Not bwNonScrape.IsBusy Then
                    Me.SetControlsEnabled(True)
                    Me.EnableFilters_Movies(True)
                Else
                    Me.dgvMovies.Enabled = True
                End If
            End If
            Me.dgvMovies.ResumeLayout()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwLoadMovieSetInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadMovieSetInfo.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Me.MainActors.Clear()
        Me.MainBanner.Clear()
        Me.MainCharacterArt.Clear()
        Me.MainClearArt.Clear()
        Me.MainClearLogo.Clear()
        Me.MainDiscArt.Clear()
        Me.MainFanart.Clear()
        Me.MainFanartSmall.Clear()
        Me.MainLandscape.Clear()
        Me.MainPoster.Clear()

        If bwLoadMovieSetInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        Me.currMovieSet = Master.DB.LoadMovieSetFromDB(Args.ID)

        If bwLoadMovieSetInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Not Master.eSettings.GeneralHideBanner Then Me.MainBanner = Me.currMovieSet.ImagesContainer.Banner.ImageOriginal
        If Not Master.eSettings.GeneralHideClearArt Then Me.MainClearArt = Me.currMovieSet.ImagesContainer.ClearArt.ImageOriginal
        If Not Master.eSettings.GeneralHideClearLogo Then Me.MainClearLogo = Me.currMovieSet.ImagesContainer.ClearLogo.ImageOriginal
        If Not Master.eSettings.GeneralHideDiscArt Then Me.MainDiscArt = Me.currMovieSet.ImagesContainer.DiscArt.ImageOriginal
        If Not Master.eSettings.GeneralHideFanart Then Me.MainFanart = Me.currMovieSet.ImagesContainer.Fanart.ImageOriginal
        If Not Master.eSettings.GeneralHideFanartSmall Then Me.MainFanartSmall = Me.currMovieSet.ImagesContainer.Fanart.ImageOriginal
        If Not Master.eSettings.GeneralHideLandscape Then Me.MainLandscape = Me.currMovieSet.ImagesContainer.Landscape.ImageOriginal
        If Not Master.eSettings.GeneralHidePoster Then Me.MainPoster = Me.currMovieSet.ImagesContainer.Poster.ImageOriginal
        'read nfo if it's there

        If bwLoadMovieSetInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub bwLoadMovieSetInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadMovieSetInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                Me.FillScreenInfoWith_MovieSet()
            Else
                If Not bwMovieSetScraper.IsBusy AndAlso Not bwReload_MovieSets.IsBusy AndAlso Not bwCleanDB.IsBusy AndAlso Not bwNonScrape.IsBusy Then
                    Me.SetControlsEnabled(True)
                    Me.EnableFilters_MovieSets(True)
                Else
                    Me.dgvMovieSets.Enabled = True
                End If
            End If
            Me.dgvMovieSets.ResumeLayout()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwLoadMovieSetPosters_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadMovieSetPosters.DoWork
        Dim Posters As New List(Of MovieInSetPoster)

        Try
            If Me.currMovieSet.MovieList IsNot Nothing AndAlso Me.currMovieSet.MovieList.Count > 0 Then
                Try
                    For Each Movie As Database.DBElement In Me.currMovieSet.MovieList
                        If bwLoadMovieSetPosters.CancellationPending Then
                            e.Cancel = True
                            Return
                        End If

                        Dim ResImg As Image
                        If Movie.ImagesContainer.Poster.ImageOriginal.Image Is Nothing AndAlso Not String.IsNullOrEmpty(Movie.ImagesContainer.Poster.LocalFilePath) Then
                            Movie.ImagesContainer.Poster.Download(Enums.ContentType.Movie, True)
                        End If
                        If Movie.ImagesContainer.Poster.ImageOriginal.Image IsNot Nothing Then
                            ResImg = CType(Movie.ImagesContainer.Poster.ImageOriginal.Image.Clone(), Image)
                            ImageUtils.ResizeImage(ResImg, 59, 88, True, Color.White.ToArgb())
                            Posters.Add(New MovieInSetPoster With {.MoviePoster = ResImg, .MovieTitle = Movie.Movie.Title, .MovieYear = Movie.Movie.Year})
                        Else
                            Posters.Add(New MovieInSetPoster With {.MoviePoster = My.Resources.noposter, .MovieTitle = Movie.Movie.Title, .MovieYear = Movie.Movie.Year})
                        End If
                    Next
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                    e.Result = New Results With {.MovieInSetPosters = Nothing}
                    e.Cancel = True
                End Try
            End If

            e.Result = New Results With {.MovieinSetPosters = Posters}
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            e.Result = New Results With {.MovieInSetPosters = Nothing}
            e.Cancel = True
        End Try
    End Sub

    Private Sub bwLoadMovieSetPosters_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadMovieSetPosters.RunWorkerCompleted
        Me.lvMoviesInSet.Clear()
        Me.ilMoviesInSet.Images.Clear()
        Me.ilMoviesInSet.ImageSize = New Size(59, 88)
        Me.ilMoviesInSet.ColorDepth = ColorDepth.Depth32Bit
        Me.lvMoviesInSet.Visible = False

        If Not e.Cancelled Then
            Try
                Dim Res As Results = DirectCast(e.Result, Results)

                If Res.MovieInSetPosters IsNot Nothing AndAlso Res.MovieInSetPosters.Count > 0 Then
                    Me.lvMoviesInSet.BeginUpdate()
                    For Each tPoster As MovieInSetPoster In Res.MovieInSetPosters
                        If tPoster IsNot Nothing Then
                            Me.ilMoviesInSet.Images.Add(tPoster.MoviePoster)
                            Me.lvMoviesInSet.Items.Add(String.Concat(tPoster.MovieTitle, Environment.NewLine, "(", tPoster.MovieYear, ")"), Me.ilMoviesInSet.Images.Count - 1)
                        End If
                    Next
                    Me.lvMoviesInSet.EndUpdate()
                    Me.lvMoviesInSet.Visible = True
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End If
    End Sub

    Private Sub bwLoadSeasonInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadSeasonInfo.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Me.MainActors.Clear()
        Me.MainBanner.Clear()
        Me.MainCharacterArt.Clear()
        Me.MainClearArt.Clear()
        Me.MainClearLogo.Clear()
        Me.MainDiscArt.Clear()
        Me.MainFanart.Clear()
        Me.MainFanartSmall.Clear()
        Me.MainLandscape.Clear()
        Me.MainPoster.Clear()

        Me.currTV = Master.DB.LoadTVSeasonFromDB(Args.ID, True)

        If bwLoadSeasonInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Not Master.eSettings.GeneralHideBanner Then Me.MainBanner = Me.currTV.ImagesContainer.Banner.ImageOriginal
        If Not Master.eSettings.GeneralHideFanartSmall Then Me.MainFanartSmall = Me.currTV.ImagesContainer.Fanart.ImageOriginal
        If Not Master.eSettings.GeneralHideLandscape Then Me.MainLandscape = Me.currTV.ImagesContainer.Landscape.ImageOriginal
        If Not Master.eSettings.GeneralHidePoster Then Me.MainPoster = Me.currTV.ImagesContainer.Poster.ImageOriginal

        If bwLoadSeasonInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Not Master.eSettings.GeneralHideFanart Then
            Dim NeedsGS As Boolean = False
            If Me.currTV.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing Then
                Me.MainFanart = Me.currTV.ImagesContainer.Fanart.ImageOriginal
            Else
                Dim TVShowFanart As String = Master.DB.GetArtForItem(Me.currTV.ShowID, "tvshow", "fanart")
                If Not String.IsNullOrEmpty(TVShowFanart) Then
                    Me.MainFanart.FromFile(TVShowFanart)
                    NeedsGS = True
                End If
            End If

            If Me.MainFanart.Image IsNot Nothing AndAlso NeedsGS Then
                Me.MainFanart = ImageUtils.GrayScale(Me.MainFanart)
            End If
        End If

        If bwLoadSeasonInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub bwLoadSeasonInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadSeasonInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                Me.FillScreenInfoWith_TVSeason()
            Else
                Me.SetControlsEnabled(True)
            End If
            Me.dgvTVSeasons.ResumeLayout()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwLoadShowInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadShowInfo.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Me.MainActors.Clear()
        Me.MainBanner.Clear()
        Me.MainCharacterArt.Clear()
        Me.MainClearArt.Clear()
        Me.MainClearLogo.Clear()
        Me.MainDiscArt.Clear()
        Me.MainFanart.Clear()
        Me.MainFanartSmall.Clear()
        Me.MainLandscape.Clear()
        Me.MainPoster.Clear()

        If bwLoadShowInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        Me.currTV = Master.DB.LoadTVShowFromDB(Args.ID, False, False, True, True)

        If bwLoadShowInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Not Master.eSettings.GeneralHideBanner Then Me.MainBanner = Me.currTV.ImagesContainer.Banner.ImageOriginal
        If Not Master.eSettings.GeneralHideCharacterArt Then Me.MainCharacterArt = Me.currTV.ImagesContainer.CharacterArt.ImageOriginal
        If Not Master.eSettings.GeneralHideClearArt Then Me.MainClearArt = Me.currTV.ImagesContainer.ClearArt.ImageOriginal
        If Not Master.eSettings.GeneralHideClearLogo Then Me.MainClearLogo = Me.currTV.ImagesContainer.ClearLogo.ImageOriginal
        If Not Master.eSettings.GeneralHideFanart Then Me.MainFanart = Me.currTV.ImagesContainer.Fanart.ImageOriginal
        If Not Master.eSettings.GeneralHideFanartSmall Then Me.MainFanartSmall = Me.currTV.ImagesContainer.Fanart.ImageOriginal
        If Not Master.eSettings.GeneralHideLandscape Then Me.MainLandscape = Me.currTV.ImagesContainer.Landscape.ImageOriginal
        If Not Master.eSettings.GeneralHidePoster Then Me.MainPoster = Me.currTV.ImagesContainer.Poster.ImageOriginal

        If bwLoadShowInfo.CancellationPending Then
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub bwLoadShowInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadShowInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                Me.FillScreenInfoWith_TVShow()
            Else
                Me.SetControlsEnabled(True)
                Me.EnableFilters_Shows(True)
            End If
            Me.dgvTVShows.ResumeLayout()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwMetaData_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwMetaData.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)

        Try
            If Args.IsTV Then
                MediaInfo.UpdateTVMediaInfo(Args.DBElement)
                Master.DB.SaveTVEpisodeToDB(Args.DBElement, False, False, True, False, False)
                e.Result = New Results With {.fileinfo = NFO.FIToString(Args.DBElement.TVEpisode.FileInfo, True), .DBElement = Args.DBElement, .IsTV = True, .setEnabled = Args.setEnabled}
            Else
                MediaInfo.UpdateMediaInfo(Args.DBElement)
                Master.DB.SaveMovieToDB(Args.DBElement, False, False, True, False)
                e.Result = New Results With {.fileinfo = NFO.FIToString(Args.DBElement.Movie.FileInfo, False), .setEnabled = Args.setEnabled, .Path = Args.Path, .DBElement = Args.DBElement}
            End If

            If Me.bwMetaData.CancellationPending Then
                e.Cancel = True
                Return
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            e.Result = New Results With {.fileinfo = "error", .setEnabled = Args.setEnabled}
            e.Cancel = True
        End Try
    End Sub

    Private Sub bwbwMetaData_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwMetaData.RunWorkerCompleted
        '//
        ' Thread finished: fill textbox with result
        '\\

        If Not e.Cancelled Then
            Dim Res As Results = DirectCast(e.Result, Results)

            Try
                If Not Res.fileInfo = "error" Then
                    Me.pbMILoading.Visible = False
                    Me.txtMetaData.Text = Res.fileInfo

                    If Res.IsTV Then
                        If Master.eSettings.TVScraperMetaDataScan Then
                            Me.SetAVImages(APIXML.GetAVImages(Res.DBElement.TVEpisode.FileInfo, Res.DBElement.Filename, True, ""))
                            Me.pnlInfoIcons.Width = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + pbStudio.Width + 6
                            Me.pbStudio.Left = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + 5
                        Else
                            Me.pnlInfoIcons.Width = pbStudio.Width + 1
                            Me.pbStudio.Left = 0
                        End If
                    Else
                        If Master.eSettings.MovieScraperMetaDataScan Then
                            Me.SetAVImages(APIXML.GetAVImages(Res.DBElement.Movie.FileInfo, Res.DBElement.Filename, False, Res.DBElement.Movie.VideoSource))
                            Me.pnlInfoIcons.Width = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + pbStudio.Width + 6
                            Me.pbStudio.Left = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + 5
                        Else
                            Me.pnlInfoIcons.Width = pbStudio.Width + 1
                            Me.pbStudio.Left = 0
                        End If

                        If Master.eSettings.MovieScraperUseMDDuration Then
                            If Not String.IsNullOrEmpty(Res.DBElement.Movie.Runtime) Then
                                Me.lblRuntime.Text = String.Format(Master.eLang.GetString(112, "Runtime: {0}"), Res.DBElement.Movie.Runtime)
                            End If
                        End If
                    End If
                    Me.btnMetaDataRefresh.Focus()
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            If Res.setEnabled Then
                Dim currMainTabTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
                Me.tcMain.Enabled = True
                Me.mnuUpdate.Enabled = True
                Me.cmnuTrayUpdate.Enabled = True
                If (currMainTabTag.ContentType = Enums.ContentType.Movie AndAlso Me.dgvMovies.RowCount > 0) OrElse _
                    (currMainTabTag.ContentType = Enums.ContentType.MovieSet AndAlso Me.dgvMovieSets.RowCount > 0) OrElse _
                    (currMainTabTag.ContentType = Enums.ContentType.TV AndAlso Me.dgvTVShows.RowCount > 0) Then
                    Me.SetControlsEnabled(True)
                End If
            End If
        End If
    End Sub

    Private Sub bwMovieScraper_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwMovieScraper.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        If Master.isCL Then
            Me.ScraperDone = True
        End If

        If Res.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Not Res.Cancelled Then
            Me.InfoDownloaded_Movie(Res.DBElement)
        ElseIf Res.Cancelled Then
            'Reload last partially scraped Movie from disk to get clean informations in DB
            Me.Reload_Movie(Res.DBElement.ID, False, True)
            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
            Me.btnCancel.Visible = False
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = False
            Me.SetControlsEnabled(True)
        Else
            Me.FillList(False, True, False)
            If Me.dgvMovies.SelectedRows.Count > 0 Then
                Me.SelectRow_Movie(Me.dgvMovies.SelectedRows(0).Index)
            Else
                Me.ClearInfo()
            End If
            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
            Me.btnCancel.Visible = False
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = False
            Me.SetControlsEnabled(True)
        End If
    End Sub

    Private Sub bwMovieScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwMovieScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeMovie As New Database.DBElement

        logger.Trace("Starting MOVIE scrape")

        For Each tScrapeItem As ScrapeItem In Args.ScrapeList
            Dim Theme As New MediaContainers.Theme
            Dim tURL As String = String.Empty
            Dim aUrlList As New List(Of MediaContainers.Trailer)
            Dim tUrlList As New List(Of Themes)
            Dim OldListTitle As String = String.Empty
            Dim NewListTitle As String = String.Empty

            Cancelled = False

            If bwMovieScraper.CancellationPending Then Exit For
            OldListTitle = tScrapeItem.DataRow.Item("ListTitle").ToString
            bwMovieScraper.ReportProgress(1, OldListTitle)

            dScrapeRow = tScrapeItem.DataRow

            logger.Trace(String.Concat("Start scraping: ", OldListTitle))

            DBScrapeMovie = Master.DB.LoadMovieFromDB(Convert.ToInt64(tScrapeItem.DataRow.Item("idMovie")))
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, Nothing, False, DBScrapeMovie)

            If tScrapeItem.ScrapeModifier.MainNFO Then
                bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(253, "Scraping Data"), ":"))
                If ModulesManager.Instance.ScrapeData_Movie(DBScrapeMovie, tScrapeItem.ScrapeModifier, Args.ScrapeType, Args.Options_Movie, Args.ScrapeList.Count = 1) Then
                    Cancelled = True
                    If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                        logger.Trace(String.Concat("Canceled scraping: ", OldListTitle))
                        bwMovieScraper.CancelAsync()
                    End If
                End If
            Else
                ' if we do not have the movie ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
                If String.IsNullOrEmpty(DBScrapeMovie.Movie.ID) AndAlso (tScrapeItem.ScrapeModifier.MainActorthumbs Or tScrapeItem.ScrapeModifier.MainBanner Or tScrapeItem.ScrapeModifier.MainClearArt Or _
                                                                         tScrapeItem.ScrapeModifier.MainClearLogo Or tScrapeItem.ScrapeModifier.MainDiscArt Or tScrapeItem.ScrapeModifier.MainExtrafanarts Or _
                                                                         tScrapeItem.ScrapeModifier.MainExtrathumbs Or tScrapeItem.ScrapeModifier.MainFanart Or tScrapeItem.ScrapeModifier.MainLandscape Or _
                                                                         tScrapeItem.ScrapeModifier.MainPoster Or tScrapeItem.ScrapeModifier.MainTheme Or tScrapeItem.ScrapeModifier.MainTrailer) Then
                    Dim tModifier As New Structures.ScrapeModifier With {.MainNFO = True}
                    Dim tOptions As New Structures.ScrapeOptions_Movie 'set all values to false to not override any field. ID's are always determined.
                    If ModulesManager.Instance.ScrapeData_Movie(DBScrapeMovie, tModifier, Args.ScrapeType, tOptions, Args.ScrapeList.Count = 1) Then
                        Cancelled = True
                        If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            logger.Trace(String.Concat("Canceled scraping: ", OldListTitle))
                            bwMovieScraper.CancelAsync()
                        End If
                    End If
                End If
            End If

            If bwMovieScraper.CancellationPending Then Exit For

            If Not Cancelled Then
                If Master.eSettings.MovieScraperMetaDataScan AndAlso tScrapeItem.ScrapeModifier.MainMeta Then
                    MediaInfo.UpdateMediaInfo(DBScrapeMovie)
                End If
                If bwMovieScraper.CancellationPending Then Exit For

                NewListTitle = DBScrapeMovie.ListTitle

                If Not NewListTitle = OldListTitle Then
                    bwMovieScraper.ReportProgress(0, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle))
                End If

                'get all images 
                If tScrapeItem.ScrapeModifier.MainBanner OrElse _
                    tScrapeItem.ScrapeModifier.MainClearArt OrElse _
                    tScrapeItem.ScrapeModifier.MainClearLogo OrElse _
                    tScrapeItem.ScrapeModifier.MainDiscArt OrElse _
                    tScrapeItem.ScrapeModifier.MainExtrafanarts OrElse _
                    tScrapeItem.ScrapeModifier.MainExtrathumbs OrElse _
                    tScrapeItem.ScrapeModifier.MainFanart OrElse _
                    tScrapeItem.ScrapeModifier.MainLandscape OrElse _
                    tScrapeItem.ScrapeModifier.MainPoster Then

                    Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
                    bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(254, "Scraping Images"), ":"))
                    If Not ModulesManager.Instance.ScrapeImage_Movie(DBScrapeMovie, SearchResultsContainer, tScrapeItem.ScrapeModifier, Args.ScrapeList.Count = 1) Then
                        If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.MovieImagesDisplayImageSelect Then
                            Using dImgSelect As New dlgImgSelect
                                If dImgSelect.ShowDialog(DBScrapeMovie, SearchResultsContainer, tScrapeItem.ScrapeModifier, Enums.ContentType.Movie, True) = Windows.Forms.DialogResult.OK Then
                                    DBScrapeMovie = dImgSelect.Result
                                End If
                            End Using

                            'autoscraping
                        ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            Dim newPreferredImages As New MediaContainers.ImagesContainer
                            Images.SetDefaultImages(DBScrapeMovie, newPreferredImages, SearchResultsContainer, tScrapeItem.ScrapeModifier, Enums.ContentType.Movie)
                            DBScrapeMovie.ImagesContainer = newPreferredImages
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'Theme
                If tScrapeItem.ScrapeModifier.MainTheme Then
                    bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(266, "Scraping Themes"), ":"))
                    If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                        tURL = String.Empty
                        If Theme.WebTheme.IsAllowedToDownload(DBScrapeMovie) Then
                            If Not ModulesManager.Instance.ScrapeTheme_Movie(DBScrapeMovie, tUrlList) Then
                                If tUrlList.Count > 0 Then
                                    If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                                        Theme.WebTheme.FromWeb(tUrlList.Item(0).URL, tUrlList.Item(0).WebURL)
                                        If Theme.WebTheme IsNot Nothing Then 'TODO: fix check
                                            tURL = Theme.WebTheme.SaveAsMovieTheme(DBScrapeMovie)
                                            If Not String.IsNullOrEmpty(tURL) Then
                                                DBScrapeMovie.ThemePath = tURL
                                            End If
                                        End If
                                        'ElseIf Args.scrapeType = Enums.ScrapeType.SingleScrape OrElse Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk  OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        '    If Args.scrapeType = Enums.ScrapeType.FullAsk OrElse Args.scrapeType = Enums.ScrapeType.NewAsk OrElse Args.scrapeType = Enums.ScrapeType.MarkAsk  OrElse Args.scrapeType = Enums.ScrapeType.UpdateAsk Then
                                        '        MsgBox(Master.eLang.GetString(930, "Trailer of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(929, "No Preferred Size:"))
                                        '    End If
                                        '    Using dThemeSelect As New dlgThemeSelect()
                                        '        tURL = dThemeSelect.ShowDialog(DBScrapeMovie, tUrlList)
                                        '        If Not String.IsNullOrEmpty(tURL) Then
                                        '            DBScrapeMovie.ThemePath = tURL
                                        '            MovieScraperEvent(Enums.MovieScraperEventType.ThemeItem, DBScrapeMovie.ThemePath )
                                        '        End If
                                        '    End Using
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'Trailer
                If tScrapeItem.ScrapeModifier.MainTrailer Then
                    bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(574, "Scraping Trailers"), ":"))
                    Dim SearchResults As New List(Of MediaContainers.Trailer)
                    If Not ModulesManager.Instance.ScrapeTrailer_Movie(DBScrapeMovie, Enums.ModifierType.MainTrailer, SearchResults) Then
                        If Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            Using dTrailerSelect As New dlgTrailerSelect
                                If dTrailerSelect.ShowDialog(DBScrapeMovie, SearchResults, False, True, False) = DialogResult.OK Then
                                    DBScrapeMovie.Trailer = dTrailerSelect.Result
                                End If
                            End Using

                            'autoscraping
                        ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            Dim newPreferredTrailer As New MediaContainers.Trailer
                            If Trailers.GetPreferredMovieTrailer(SearchResults, newPreferredTrailer) Then
                                DBScrapeMovie.Trailer = newPreferredTrailer
                            End If
                        End If
                    End If

                    ''If Not (Args.scrapeType = Enums.ScrapeType.SingleScrape) Then
                    'tURL = String.Empty
                    'If DBScrapeMovie.Trailer.TrailerOriginal.IsAllowedToDownload(DBScrapeMovie) Then
                    '    If Not ModulesManager.Instance.ScrapeTrailer_Movie(DBScrapeMovie, Enums.ModifierType.MainTrailer, aUrlList) Then
                    '        If aUrlList.Count > 0 Then
                    '            logger.Warn("[" & DBScrapeMovie.Movie.Title & "] Avalaible trailers: " & aUrlList.Count)
                    '        Else
                    '            logger.Warn("[" & DBScrapeMovie.Movie.Title & "] NO trailers avalaible!")
                    '        End If
                    '        If aUrlList.Count > 0 Then
                    '            If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) AndAlso Trailers.GetPreferredTrailer(aUrlList, DBScrapeMovie.Trailer) Then


                    '                'Cocotus 2014/09/26 After going thourgh GetPreferredTrailers aUrlList is now sorted/filtered - any trailer on this list is ok and can be downloaded!
                    '                For Each _trailer As MediaContainers.Trailer In aUrlList
                    '                    'trailer URL shoud never be empty at this point anyway, might as well remove check
                    '                    If Not String.IsNullOrEmpty(_trailer.URLVideoStream) Then
                    '                        'this will download the trailer and save it temporarly as "dummy.ext"
                    '                        DBScrapeMovie.Trailer.TrailerOriginal.FromWeb(_trailer)
                    '                        'If trailer was downloaded, Trailer.WebTrailer.URL and Trailer.WebTrailer.Extension are not empty anymore. We use this as a check!
                    '                        If Not String.IsNullOrEmpty(DBScrapeMovie.Trailer.TrailerOriginal.Extention) Then
                    '                            'now rename dummy.ext to trailer and save it in movie folder
                    '                            tURL = DBScrapeMovie.Trailer.TrailerOriginal.SaveAsMovieTrailer(DBScrapeMovie)
                    '                            If Not String.IsNullOrEmpty(tURL) Then
                    '                                DBScrapeMovie.Trailer.LocalFilePath = tURL
                    '                                logger.Info("[" & DBScrapeMovie.Movie.Title & "] " & _trailer.Quality & " Downloaded trailer: " & _trailer.URLVideoStream)
                    '                                'since trailer was downloaded we can leave loop, all good!
                    '                                Exit For
                    '                            Else
                    '                                logger.Warn("[" & DBScrapeMovie.Movie.Title & "] Saving of downloaded trailer failed: " & _trailer.URLVideoStream)
                    '                            End If
                    '                        Else
                    '                            logger.Debug("[" & DBScrapeMovie.Movie.Title & "] Download of trailer failed: " & _trailer.URLVideoStream)
                    '                        End If
                    '                    Else
                    '                        logger.Debug("[" & DBScrapeMovie.Movie.Title & "] No trailer link to download!")
                    '                    End If
                    '                Next
                    '            ElseIf Args.ScrapeType = Enums.ScrapeType.SingleScrape OrElse Args.ScrapeType = Enums.ScrapeType.AllAsk OrElse Args.ScrapeType = Enums.ScrapeType.NewAsk OrElse Args.ScrapeType = Enums.ScrapeType.MarkedAsk OrElse Args.ScrapeType = Enums.ScrapeType.MissingAsk Then
                    '                If Args.ScrapeType = Enums.ScrapeType.AllAsk OrElse Args.ScrapeType = Enums.ScrapeType.NewAsk OrElse Args.ScrapeType = Enums.ScrapeType.MarkedAsk OrElse Args.ScrapeType = Enums.ScrapeType.MissingAsk Then
                    '                    MessageBox.Show(Master.eLang.GetString(930, "Trailer of your preferred size could not be found. Please choose another."), Master.eLang.GetString(929, "No Preferred Size:"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                End If
                    '                Using dTrailerSelect As New dlgTrailerSelect
                    '                    If dTrailerSelect.ShowDialog(DBScrapeMovie, aUrlList, False, True, False) = DialogResult.OK Then
                    '                        DBScrapeMovie.Trailer = dTrailerSelect.Result
                    '                        If Not String.IsNullOrEmpty(DBScrapeMovie.Trailer.URLVideoStream) Then
                    '                            tURL = DBScrapeMovie.Trailer.TrailerOriginal.SaveAsMovieTrailer(DBScrapeMovie)
                    '                            If Not String.IsNullOrEmpty(tURL) Then
                    '                                DBScrapeMovie.Trailer.LocalFilePath = tURL
                    '                            End If
                    '                        End If
                    '                    End If
                    '                End Using
                    '            End If
                    '        Else
                    '            logger.Warn("[" & DBScrapeMovie.Movie.Title & "] No trailer links scraped!")
                    '        End If
                    '    End If
                    'End If
                    'End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                'ActorThumbs
                If tScrapeItem.ScrapeModifier.MainActorthumbs AndAlso (Master.eSettings.MovieActorThumbsFrodo OrElse Master.eSettings.MovieActorThumbsEden) Then
                    If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                        For Each act As MediaContainers.Person In DBScrapeMovie.Movie.Actors
                            Dim img As New Images
                            img.FromWeb(act.ThumbURL)
                            If img.Image IsNot Nothing Then
                                act.ThumbPath = img.SaveAsMovieActorThumb(act, Directory.GetParent(DBScrapeMovie.Filename).FullName, DBScrapeMovie)
                            End If
                        Next
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.ScraperMulti_Movie, Nothing, Nothing, False, DBScrapeMovie)
                    bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
                    Master.DB.SaveMovieToDB(DBScrapeMovie, False, False, tScrapeItem.ScrapeModifier.MainNFO, True)
                    bwMovieScraper.ReportProgress(-2, DBScrapeMovie.ID)
                    bwMovieScraper.ReportProgress(-1, If(Not OldListTitle = NewListTitle, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle), NewListTitle))
                End If
                logger.Trace(String.Concat("Ended scraping: ", OldListTitle))
            Else
                logger.Trace(String.Concat("Canceled scraping: ", OldListTitle))
            End If
        Next

        e.Result = New Results With {.DBElement = DBScrapeMovie, .ScrapeType = Args.ScrapeType, .Cancelled = bwMovieScraper.CancellationPending}
        logger.Trace("Ended MOVIE scrape")
    End Sub

    Private Sub bwMovieScraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwMovieScraper.ProgressChanged
        If e.ProgressPercentage = -1 Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"moviescraped", 3, Master.eLang.GetString(813, "Movie Scraped"), e.UserState.ToString, Nothing}))
        ElseIf e.ProgressPercentage = -2 Then
            RefreshRow_Movie(CLng(e.UserState))
        ElseIf e.ProgressPercentage = -3 Then
            Me.tslLoading.Text = e.UserState.ToString
        Else
            Me.tspbLoading.Value += e.ProgressPercentage
            Me.SetStatus(e.UserState.ToString)
        End If
    End Sub

    Private Sub bwMovieSetScraper_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwMovieSetScraper.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        If Master.isCL Then
            Me.ScraperDone = True
        End If

        If Res.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Not Res.Cancelled Then
            Me.InfoDownloaded_MovieSet(Res.DBElement)
        ElseIf Res.Cancelled Then
            'Reload last partially scraped MovieSet from disk to get clean informations in DB
            Me.Reload_MovieSet(Res.DBElement.ID)
            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
            Me.btnCancel.Visible = False
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = False
            Me.SetControlsEnabled(True)
        Else
            If Me.dgvMovieSets.SelectedRows.Count > 0 Then
                Me.SelectRow_MovieSet(Me.dgvMovieSets.SelectedRows(0).Index)
            Else
                Me.ClearInfo()
            End If
            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
            Me.btnCancel.Visible = False
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = False
            Me.SetControlsEnabled(True)
        End If
    End Sub

    Private Sub bwMovieSetScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwMovieSetScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeMovieSet As New Database.DBElement
        Dim cloneMovieSet As New Database.DBElement

        logger.Trace("Starting MOVIE SET scrape")

        For Each tScrapeItem As ScrapeItem In Args.ScrapeList
            Dim aContainer As New MediaContainers.SearchResultsContainer
            Dim NewListTitle As String = String.Empty
            Dim NewTMDBColID As String = String.Empty
            Dim NewTitle As String = String.Empty
            Dim OldListTitle As String = String.Empty
            Dim OldTMDBColID As String = String.Empty
            Dim OldTitle As String = String.Empty
            Dim efList As New List(Of String)
            Dim etList As New List(Of String)
            Dim tURL As String = String.Empty

            If bwMovieSetScraper.CancellationPending Then Exit For
            OldListTitle = tScrapeItem.DataRow.Item("ListTitle").ToString
            OldTitle = tScrapeItem.DataRow.Item("SetName").ToString
            OldTMDBColID = tScrapeItem.DataRow.Item("TMDBColID").ToString
            bwMovieSetScraper.ReportProgress(1, OldListTitle)

            dScrapeRow = tScrapeItem.DataRow

            DBScrapeMovieSet = Master.DB.LoadMovieSetFromDB(Convert.ToInt64(tScrapeItem.DataRow.Item("idSet")))

            'clone the existing MovieSet with old paths and title to remove old images if the title is changed during the scraping
            cloneMovieSet.ImagesContainer.Banner.LocalFilePath = DBScrapeMovieSet.ImagesContainer.Banner.LocalFilePath
            cloneMovieSet.ImagesContainer.ClearArt.LocalFilePath = DBScrapeMovieSet.ImagesContainer.ClearArt.LocalFilePath
            cloneMovieSet.ImagesContainer.ClearLogo.LocalFilePath = DBScrapeMovieSet.ImagesContainer.ClearLogo.LocalFilePath
            cloneMovieSet.ImagesContainer.DiscArt.LocalFilePath = DBScrapeMovieSet.ImagesContainer.DiscArt.LocalFilePath
            cloneMovieSet.ImagesContainer.Fanart.LocalFilePath = DBScrapeMovieSet.ImagesContainer.Fanart.LocalFilePath
            cloneMovieSet.ImagesContainer.Landscape.LocalFilePath = DBScrapeMovieSet.ImagesContainer.Landscape.LocalFilePath
            cloneMovieSet.ImagesContainer.Poster.LocalFilePath = DBScrapeMovieSet.ImagesContainer.Poster.LocalFilePath
            cloneMovieSet.MovieSet = New MediaContainers.MovieSet
            cloneMovieSet.MovieSet.Title = DBScrapeMovieSet.MovieSet.Title

            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEditMovieSet, Nothing, DBScrapeMovieSet)

            If tScrapeItem.ScrapeModifier.MainNFO Then
                bwMovieSetScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(253, "Scraping Data"), ":"))
                If ModulesManager.Instance.ScrapeData_MovieSet(DBScrapeMovieSet, tScrapeItem.ScrapeModifier, Args.ScrapeType, Args.Options_MovieSet, Args.ScrapeList.Count = 1) Then
                    Cancelled = True
                    If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                        logger.Trace(String.Concat("Canceled scraping: ", OldListTitle))
                        bwMovieSetScraper.CancelAsync()
                    End If
                End If
            Else
                ' if we do not have the movie set ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
                If String.IsNullOrEmpty(DBScrapeMovieSet.MovieSet.TMDB) AndAlso (tScrapeItem.ScrapeModifier.MainBanner Or tScrapeItem.ScrapeModifier.MainClearArt Or _
                                                                         tScrapeItem.ScrapeModifier.MainClearLogo Or tScrapeItem.ScrapeModifier.MainDiscArt Or _
                                                                         tScrapeItem.ScrapeModifier.MainFanart Or tScrapeItem.ScrapeModifier.MainLandscape Or _
                                                                         tScrapeItem.ScrapeModifier.MainPoster) Then
                    Dim tOpt As New Structures.ScrapeOptions_MovieSet 'all false value not to override any field
                    If ModulesManager.Instance.ScrapeData_MovieSet(DBScrapeMovieSet, tScrapeItem.ScrapeModifier, Args.ScrapeType, tOpt, Args.ScrapeList.Count = 1) Then
                        Exit For
                    End If
                End If
            End If

            If bwMovieSetScraper.CancellationPending Then Exit For

            If Not Cancelled Then

                NewListTitle = DBScrapeMovieSet.ListTitle
                NewTitle = DBScrapeMovieSet.MovieSet.Title
                NewTMDBColID = DBScrapeMovieSet.MovieSet.TMDB

                If Not NewListTitle = OldListTitle Then
                    bwMovieSetScraper.ReportProgress(0, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle))
                End If

                'rename old images with no longer valid <title>-imagetype.* file names to new MovieSet title
                If Not NewTitle = OldTitle AndAlso Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                    'load all old images to memorystream
                    'save old images with new MovieSet title
                    'If Not String.IsNullOrEmpty(cloneMovieSet.ImagesContainer.Banner.LocalFile) AndAlso File.Exists(cloneMovieSet.ImagesContainer.Banner.LocalFile) Then
                    '    Banner.WebImage.FromFile(cloneMovieSet.ImagesContainer.Banner.LocalFile)
                    '    DBScrapeMovieSet.ImagesContainer.Banner.LocalFile = Banner.WebImage.SaveAsMovieSetBanner(DBScrapeMovieSet)
                    'End If
                    'If Not String.IsNullOrEmpty(cloneMovieSet.ImagesContainer.ClearArt.LocalFile) AndAlso File.Exists(cloneMovieSet.ImagesContainer.ClearArt.LocalFile) Then
                    '    ClearArt.WebImage.FromFile(cloneMovieSet.ImagesContainer.ClearArt.LocalFile)
                    '    DBScrapeMovieSet.ImagesContainer.ClearArt.LocalFile = ClearArt.WebImage.SaveAsMovieSetClearArt(DBScrapeMovieSet)
                    'End If
                    'If Not String.IsNullOrEmpty(cloneMovieSet.ImagesContainer.ClearLogo.LocalFile) AndAlso File.Exists(cloneMovieSet.ImagesContainer.ClearLogo.LocalFile) Then
                    '    ClearLogo.WebImage.FromFile(cloneMovieSet.ImagesContainer.ClearLogo.LocalFile)
                    '    DBScrapeMovieSet.ImagesContainer.ClearLogo.LocalFile = ClearLogo.WebImage.SaveAsMovieSetClearLogo(DBScrapeMovieSet)
                    'End If
                    'If Not String.IsNullOrEmpty(cloneMovieSet.ImagesContainer.DiscArt.LocalFile) AndAlso File.Exists(cloneMovieSet.ImagesContainer.DiscArt.LocalFile) Then
                    '    DiscArt.WebImage.FromFile(cloneMovieSet.ImagesContainer.DiscArt.LocalFile)
                    '    DBScrapeMovieSet.ImagesContainer.DiscArt.LocalFile = DiscArt.WebImage.SaveAsMovieSetDiscArt(DBScrapeMovieSet)
                    'End If
                    'If Not String.IsNullOrEmpty(cloneMovieSet.ImagesContainer.Fanart.LocalFile) AndAlso File.Exists(cloneMovieSet.ImagesContainer.Fanart.LocalFile) Then
                    '    Fanart.WebImage.FromFile(cloneMovieSet.ImagesContainer.Fanart.LocalFile)
                    '    DBScrapeMovieSet.ImagesContainer.Fanart.LocalFile = Fanart.WebImage.SaveAsMovieSetFanart(DBScrapeMovieSet)
                    'End If
                    'If Not String.IsNullOrEmpty(cloneMovieSet.ImagesContainer.Landscape.LocalFile) AndAlso File.Exists(cloneMovieSet.ImagesContainer.Landscape.LocalFile) Then
                    '    Landscape.WebImage.FromFile(cloneMovieSet.ImagesContainer.Landscape.LocalFile)
                    '    DBScrapeMovieSet.ImagesContainer.Landscape.LocalFile = Landscape.WebImage.SaveAsMovieSetLandscape(DBScrapeMovieSet)
                    'End If
                    'If Not String.IsNullOrEmpty(cloneMovieSet.ImagesContainer.Poster.LocalFile) AndAlso File.Exists(cloneMovieSet.ImagesContainer.Poster.LocalFile) Then
                    '    Poster.WebImage.FromFile(cloneMovieSet.ImagesContainer.Poster.LocalFile)
                    '    DBScrapeMovieSet.ImagesContainer.Poster.LocalFile = Poster.WebImage.SaveAsMovieSetPoster(DBScrapeMovieSet)
                    'End If

                    ''delete old images
                    'Images.DeleteMovieSetBanner(cloneMovieSet)
                    'Images.DeleteMovieSetClearArt(cloneMovieSet)
                    'Images.DeleteMovieSetClearLogo(cloneMovieSet)
                    'Images.DeleteMovieSetDiscArt(cloneMovieSet)
                    'Images.DeleteMovieSetFanart(cloneMovieSet)
                    'Images.DeleteMovieSetLandscape(cloneMovieSet)
                    'Images.DeleteMovieSetPoster(cloneMovieSet)
                End If

                'get all images
                If tScrapeItem.ScrapeModifier.MainBanner OrElse _
                    tScrapeItem.ScrapeModifier.MainClearArt OrElse _
                    tScrapeItem.ScrapeModifier.MainClearLogo OrElse _
                    tScrapeItem.ScrapeModifier.MainDiscArt OrElse _
                    tScrapeItem.ScrapeModifier.MainExtrafanarts OrElse _
                    tScrapeItem.ScrapeModifier.MainFanart OrElse _
                    tScrapeItem.ScrapeModifier.MainLandscape OrElse _
                    tScrapeItem.ScrapeModifier.MainPoster Then

                    Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
                    bwMovieSetScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(254, "Scraping Images"), ":"))
                    If Not ModulesManager.Instance.ScrapeImage_MovieSet(DBScrapeMovieSet, SearchResultsContainer, tScrapeItem.ScrapeModifier) Then
                        If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.MovieImagesDisplayImageSelect Then
                            Using dImgSelect As New dlgImgSelect
                                If dImgSelect.ShowDialog(DBScrapeMovieSet, SearchResultsContainer, tScrapeItem.ScrapeModifier, Enums.ContentType.Movie, True) = DialogResult.OK Then
                                    DBScrapeMovieSet = dImgSelect.Result
                                End If
                            End Using

                            'autoscraping
                        ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            Dim newPreferredImages As New MediaContainers.ImagesContainer
                            Images.SetDefaultImages(DBScrapeMovieSet, newPreferredImages, SearchResultsContainer, tScrapeItem.ScrapeModifier, Enums.ContentType.MovieSet)
                            DBScrapeMovieSet.ImagesContainer = newPreferredImages
                        End If
                    End If
                End If

                If bwMovieScraper.CancellationPending Then Exit For

                If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                    bwMovieSetScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
                    If Not OldTitle = NewTitle OrElse Not OldTMDBColID = NewTMDBColID Then
                        Master.DB.SaveMovieSetToDB(DBScrapeMovieSet, False, False, True, True)
                    Else
                        Master.DB.SaveMovieSetToDB(DBScrapeMovieSet, False, False, True)
                    End If
                    bwMovieSetScraper.ReportProgress(-2, DBScrapeMovieSet.ID)
                    bwMovieSetScraper.ReportProgress(-1, If(Not OldListTitle = NewListTitle, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle), NewListTitle))
                End If
            End If
        Next

        e.Result = New Results With {.DBElement = DBScrapeMovieSet, .ScrapeType = Args.ScrapeType, .Cancelled = bwMovieSetScraper.CancellationPending}
        logger.Trace("Ended MOVIESET scrape")
    End Sub

    Private Sub bwMovieSetScraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwMovieSetScraper.ProgressChanged
        If e.ProgressPercentage = -1 Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"moviesetscraped", 3, Master.eLang.GetString(1204, "MovieSet Scraped"), e.UserState.ToString, Nothing}))
        ElseIf e.ProgressPercentage = -2 Then
            RefreshRow_MovieSet(CLng(e.UserState))
        ElseIf e.ProgressPercentage = -3 Then
            Me.tslLoading.Text = e.UserState.ToString
        Else
            Me.tspbLoading.Value += e.ProgressPercentage
            Me.SetStatus(e.UserState.ToString)
        End If
    End Sub

    Private Sub bwTVScraper_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTVScraper.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        If Master.isCL Then
            Me.ScraperDone = True
        End If

        If Res.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Not Res.Cancelled Then
            Me.InfoDownloaded_TV(Res.DBElement)
        ElseIf Res.Cancelled Then
            'Reload last partially scraped TVShow from disk to get clean informations in DB
            Me.Reload_TVShow(Res.DBElement.ID, False, True, True)
            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
            Me.btnCancel.Visible = False
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = False
            Me.SetControlsEnabled(True)
        Else
            'Me.FillList(False, False, False)
            If Me.dgvTVShows.SelectedRows.Count > 0 Then
                Me.SelectRow_TVShow(Me.dgvTVShows.SelectedRows(0).Index)
            Else
                Me.ClearInfo()
            End If
            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
            Me.btnCancel.Visible = False
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = False
            Me.SetControlsEnabled(True)
        End If
    End Sub

    Private Sub bwTVScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTVScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeShow As New Database.DBElement

        logger.Trace("Starting TV Show scrape")

        For Each tScrapeItem As ScrapeItem In Args.ScrapeList
            Dim ShowTheme As New MediaContainers.Theme
            Dim tURL As String = String.Empty
            Dim tUrlList As New List(Of Themes)
            Dim OldListTitle As String = String.Empty
            Dim NewListTitle As String = String.Empty

            Cancelled = False

            If bwTVScraper.CancellationPending Then Exit For
            OldListTitle = tScrapeItem.DataRow.Item("ListTitle").ToString
            bwTVScraper.ReportProgress(1, OldListTitle)

            dScrapeRow = tScrapeItem.DataRow

            logger.Trace(String.Concat("Start scraping: ", OldListTitle))

            DBScrapeShow = Master.DB.LoadTVFullShowFromDB(Convert.ToInt64(tScrapeItem.DataRow.Item("idShow")))
            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, DBScrapeMovie)

            If tScrapeItem.ScrapeModifier.MainNFO Then
                bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(253, "Scraping Data"), ":"))
                If ModulesManager.Instance.ScrapeData_TVShow(DBScrapeShow, tScrapeItem.ScrapeModifier, Args.ScrapeType, Args.Options_TV, Args.ScrapeList.Count = 1) Then
                    Cancelled = True
                    If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                        logger.Trace(String.Concat("Canceled scraping: ", OldListTitle))
                        bwTVScraper.CancelAsync()
                    End If
                End If
            Else
                ' if we do not have the tvshow ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
                If String.IsNullOrEmpty(DBScrapeShow.TVShow.TVDB) AndAlso (tScrapeItem.ScrapeModifier.MainActorthumbs Or tScrapeItem.ScrapeModifier.MainBanner Or tScrapeItem.ScrapeModifier.MainCharacterArt Or _
                                                                           tScrapeItem.ScrapeModifier.MainClearArt Or tScrapeItem.ScrapeModifier.MainClearLogo Or tScrapeItem.ScrapeModifier.MainExtrafanarts Or _
                                                                           tScrapeItem.ScrapeModifier.MainFanart Or tScrapeItem.ScrapeModifier.MainLandscape Or tScrapeItem.ScrapeModifier.MainPoster Or _
                                                                           tScrapeItem.ScrapeModifier.MainTheme) Then
                    Dim tOpt As New Structures.ScrapeOptions_TV 'all false value not to override any field
                    If ModulesManager.Instance.ScrapeData_TVShow(DBScrapeShow, tScrapeItem.ScrapeModifier, Args.ScrapeType, tOpt, Args.ScrapeList.Count = 1) Then
                        Exit For
                    End If
                End If
            End If

            If bwTVScraper.CancellationPending Then Exit For

            If Not Cancelled Then
                NewListTitle = DBScrapeShow.ListTitle

                If Not NewListTitle = OldListTitle Then
                    bwTVScraper.ReportProgress(0, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle))
                End If

                'get all images
                If tScrapeItem.ScrapeModifier.MainBanner OrElse _
                    tScrapeItem.ScrapeModifier.MainCharacterArt OrElse _
                    tScrapeItem.ScrapeModifier.MainClearArt OrElse _
                    tScrapeItem.ScrapeModifier.MainClearLogo OrElse _
                    tScrapeItem.ScrapeModifier.MainDiscArt OrElse _
                    tScrapeItem.ScrapeModifier.MainExtrafanarts OrElse _
                    tScrapeItem.ScrapeModifier.MainFanart OrElse _
                    tScrapeItem.ScrapeModifier.MainLandscape OrElse _
                    tScrapeItem.ScrapeModifier.MainPoster Then

                    Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
                    bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(254, "Scraping Images"), ":"))
                    If Not ModulesManager.Instance.ScrapeImage_TV(DBScrapeShow, SearchResultsContainer, tScrapeItem.ScrapeModifier, Args.ScrapeList.Count = 1) Then
                        If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.TVImagesDisplayImageSelect Then
                            Using dImgSelect As New dlgImgSelect
                                If dImgSelect.ShowDialog(DBScrapeShow, SearchResultsContainer, tScrapeItem.ScrapeModifier, Enums.ContentType.TV, True) = DialogResult.OK Then
                                    DBScrapeShow = dImgSelect.Result
                                End If
                            End Using

                            'autoscraping
                        ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            Dim newPreferredImages As New MediaContainers.ImagesContainer
                            Dim newPreferredEpisodeImages As New List(Of MediaContainers.EpisodeOrSeasonImagesContainer)
                            Dim newPreferredSeasonImages As New List(Of MediaContainers.EpisodeOrSeasonImagesContainer)
                            Images.SetDefaultImages(DBScrapeShow, newPreferredImages, SearchResultsContainer, tScrapeItem.ScrapeModifier, Enums.ContentType.TV, newPreferredSeasonImages, newPreferredEpisodeImages)
                            DBScrapeShow.ImagesContainer = newPreferredImages
                        End If
                    End If
                End If

                If tScrapeItem.ScrapeModifier.withEpisodes Then
                    bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(265, "Scraping Episode Images"), ":"))
                    For Each tEpisode In DBScrapeShow.Episodes.Where(Function(f) Not String.IsNullOrEmpty(f.Filename))
                        If Master.eSettings.TVScraperMetaDataScan AndAlso tScrapeItem.ScrapeModifier.EpisodeMeta Then
                            MediaInfo.UpdateTVMediaInfo(tEpisode)
                        End If
                        If tScrapeItem.ScrapeModifier.EpisodeFanart OrElse tScrapeItem.ScrapeModifier.EpisodePoster Then
                            Dim epImagesContainer As New MediaContainers.SearchResultsContainer
                            Dim imgResult As MediaContainers.Image = Nothing
                            ModulesManager.Instance.ScrapeImage_TV(tEpisode, epImagesContainer, tScrapeItem.ScrapeModifier, False)
                            Images.GetPreferredTVEpisodePoster(epImagesContainer.EpisodePosters, imgResult, tEpisode.TVEpisode.Season, tEpisode.TVEpisode.Episode)
                            tEpisode.ImagesContainer.Poster = imgResult
                        End If
                    Next
                End If

                If bwTVScraper.CancellationPending Then Exit For

                'Theme
                If tScrapeItem.ScrapeModifier.MainTheme Then
                    bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(266, "Scraping Themes"), ":"))
                End If

                If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.ScraperMulti_TVShow, Nothing, Nothing, False, DBScrapeShow)
                    bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
                    Master.DB.SaveTVShowToDB(DBScrapeShow, False, False, tScrapeItem.ScrapeModifier.MainNFO, True, tScrapeItem.ScrapeModifier.withEpisodes)
                    bwTVScraper.ReportProgress(-2, DBScrapeShow.ID)
                    bwTVScraper.ReportProgress(-1, If(Not OldListTitle = NewListTitle, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle), NewListTitle))
                End If
            End If

            logger.Trace(String.Concat("Ended scraping: ", OldListTitle))
        Next

        e.Result = New Results With {.DBElement = DBScrapeShow, .ScrapeType = Args.ScrapeType, .Cancelled = bwTVScraper.CancellationPending}
        logger.Trace("Ended TV SHOW scrape")
    End Sub

    Private Sub bwTVScraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwTVScraper.ProgressChanged
        If e.ProgressPercentage = -1 Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"tvshowscraped", 3, Master.eLang.GetString(248, "Show Scraped"), e.UserState.ToString, Nothing}))
        ElseIf e.ProgressPercentage = -2 Then
            RefreshRow_TVShow(CLng(e.UserState))
        ElseIf e.ProgressPercentage = -3 Then
            Me.tslLoading.Text = e.UserState.ToString
        Else
            Me.tspbLoading.Value += e.ProgressPercentage
            Me.SetStatus(e.UserState.ToString)
        End If
    End Sub

    Private Sub bwTVEpisodeScraper_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTVEpisodeScraper.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        If Master.isCL Then
            Me.ScraperDone = True
        End If

        If Res.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Not Res.Cancelled Then
            Me.InfoDownloaded_TVEpisode(Res.DBElement)
        ElseIf Res.Cancelled Then
            'Reload last partially scraped Episode from disk to get clean informations in DB
            Me.Reload_TVEpisode(Res.DBElement.ID, False, True)
            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
            Me.btnCancel.Visible = False
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = False
            Me.SetControlsEnabled(True)
        Else
            'Me.FillList(False, False, False)
            If Me.dgvTVEpisodes.SelectedRows.Count > 0 Then
                Me.SelectRow_TVEpisode(Me.dgvTVShows.SelectedRows(0).Index)
            Else
                Me.ClearInfo()
            End If
            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
            Me.btnCancel.Visible = False
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = False
            Me.SetControlsEnabled(True)
        End If
    End Sub

    Private Sub bwTVEpisodeScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTVEpisodeScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeEpisode As New Database.DBElement

        logger.Trace("Starting EPISODE scrape")

        For Each tScrapeItem As ScrapeItem In Args.ScrapeList
            Dim OldEpisodeTitle As String = String.Empty
            Dim NewEpisodeTitle As String = String.Empty

            Cancelled = False

            If bwTVEpisodeScraper.CancellationPending Then Exit For
            OldEpisodeTitle = tScrapeItem.DataRow.Item("Title").ToString
            bwTVEpisodeScraper.ReportProgress(1, OldEpisodeTitle)

            dScrapeRow = tScrapeItem.DataRow

            logger.Trace(String.Concat("Start scraping: ", OldEpisodeTitle))

            DBScrapeEpisode = Master.DB.LoadTVEpisodeFromDB(Convert.ToInt64(tScrapeItem.DataRow.Item("idEpisode")), True)
            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, DBScrapeMovie)

            If tScrapeItem.ScrapeModifier.EpisodeNFO Then
                bwTVEpisodeScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(253, "Scraping Data"), ":"))
                If ModulesManager.Instance.ScrapeData_TVEpisode(DBScrapeEpisode, Args.Options_TV, Args.ScrapeList.Count = 1) Then
                    Cancelled = True
                    If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                        logger.Trace(String.Concat("Canceled scraping: ", OldEpisodeTitle))
                        bwTVEpisodeScraper.CancelAsync()
                    End If
                End If
            Else
                ' if we do not have the episode ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
                If String.IsNullOrEmpty(DBScrapeEpisode.TVEpisode.TVDB) AndAlso (tScrapeItem.ScrapeModifier.MainActorthumbs Or tScrapeItem.ScrapeModifier.MainBanner Or tScrapeItem.ScrapeModifier.MainCharacterArt Or _
                                                                         tScrapeItem.ScrapeModifier.MainClearArt Or tScrapeItem.ScrapeModifier.MainClearLogo Or tScrapeItem.ScrapeModifier.MainExtrafanarts Or _
                                                                         tScrapeItem.ScrapeModifier.MainFanart Or tScrapeItem.ScrapeModifier.MainLandscape Or tScrapeItem.ScrapeModifier.MainPoster Or _
                                                                         tScrapeItem.ScrapeModifier.MainTheme) Then
                    Dim tOpt As New Structures.ScrapeOptions_TV 'all false value not to override any field
                    If ModulesManager.Instance.ScrapeData_TVEpisode(DBScrapeEpisode, tOpt, Args.ScrapeList.Count = 1) Then
                        Exit For
                    End If
                End If
            End If

            If bwTVEpisodeScraper.CancellationPending Then Exit For

            If Not Cancelled Then
                If Master.eSettings.TVScraperMetaDataScan AndAlso tScrapeItem.ScrapeModifier.EpisodeMeta Then
                    MediaInfo.UpdateTVMediaInfo(DBScrapeEpisode)
                End If
                If bwTVEpisodeScraper.CancellationPending Then Exit For

                NewEpisodeTitle = DBScrapeEpisode.TVEpisode.Title

                If Not NewEpisodeTitle = OldEpisodeTitle Then
                    bwTVEpisodeScraper.ReportProgress(0, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldEpisodeTitle, NewEpisodeTitle))
                End If

                'get all images
                If tScrapeItem.ScrapeModifier.EpisodeFanart OrElse _
                    tScrapeItem.ScrapeModifier.EpisodePoster Then
                    Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
                    bwTVEpisodeScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(265, "Scraping Episode Images"), ":"))
                    If Not ModulesManager.Instance.ScrapeImage_TV(DBScrapeEpisode, SearchResultsContainer, tScrapeItem.ScrapeModifier, Args.ScrapeList.Count = 1) Then
                        If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.TVImagesDisplayImageSelect Then
                            Using dImgSelect As New dlgImgSelect
                                If dImgSelect.ShowDialog(DBScrapeEpisode, SearchResultsContainer, tScrapeItem.ScrapeModifier, Enums.ContentType.TVEpisode, True) = DialogResult.OK Then
                                    DBScrapeEpisode = dImgSelect.Result
                                End If
                            End Using

                            'autoscraping
                        ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            Dim newPreferredImages As New MediaContainers.ImagesContainer
                            Images.SetDefaultImages(DBScrapeEpisode, newPreferredImages, SearchResultsContainer, tScrapeItem.ScrapeModifier, Enums.ContentType.TVEpisode)
                            DBScrapeEpisode.ImagesContainer = newPreferredImages
                        End If
                    End If
                End If

                If bwTVEpisodeScraper.CancellationPending Then Exit For

                If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.ScraperMulti_TVEpisode, Nothing, Nothing, False, DBScrapeEpisode)
                    bwTVEpisodeScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
                    Master.DB.SaveTVEpisodeToDB(DBScrapeEpisode, False, False, tScrapeItem.ScrapeModifier.EpisodeNFO, True, True)
                    bwTVEpisodeScraper.ReportProgress(-2, DBScrapeEpisode.ID)
                    bwTVEpisodeScraper.ReportProgress(-1, If(Not OldEpisodeTitle = NewEpisodeTitle, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldEpisodeTitle, NewEpisodeTitle), NewEpisodeTitle))
                End If
            End If

            logger.Trace(String.Concat("Ended scraping: ", OldEpisodeTitle))
        Next

        e.Result = New Results With {.DBElement = DBScrapeEpisode, .ScrapeType = Args.ScrapeType, .Cancelled = bwTVEpisodeScraper.CancellationPending}
        logger.Trace("Ended EPISODE scrape")
    End Sub

    Private Sub bwTVEpisodeScraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwTVEpisodeScraper.ProgressChanged
        If e.ProgressPercentage = -1 Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"tvepisodescraped", 3, Master.eLang.GetString(883, "Episode Scraped"), e.UserState.ToString, Nothing}))
        ElseIf e.ProgressPercentage = -2 Then
            RefreshRow_TVEpisode(CLng(e.UserState))
        ElseIf e.ProgressPercentage = -3 Then
            Me.tslLoading.Text = e.UserState.ToString
        Else
            Me.tspbLoading.Value += e.ProgressPercentage
            Me.SetStatus(e.UserState.ToString)
        End If
    End Sub

    Private Sub bwTVSeasonScraper_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTVSeasonScraper.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        If Master.isCL Then
            Me.ScraperDone = True
        End If

        If Res.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Not Res.Cancelled Then
            Me.InfoDownloaded_TVSeason(Res.DBElement)
        ElseIf Res.Cancelled Then
            'Reload last partially scraped TVSeason from disk to get clean informations in DB
            Me.Reload_TVSeason(Res.DBElement.ID, False, True, False)
            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
            Me.btnCancel.Visible = False
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = False
            Me.SetControlsEnabled(True)
        Else
            'Me.FillList(False, False, False)
            If Me.dgvTVSeasons.SelectedRows.Count > 0 Then
                Me.SelectRow_TVSeason(Me.dgvTVSeasons.SelectedRows(0).Index)
            Else
                Me.ClearInfo()
            End If
            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
            Me.btnCancel.Visible = False
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = False
            Me.SetControlsEnabled(True)
        End If
    End Sub

    Private Sub bwTVSeasonScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTVSeasonScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeSeason As New Database.DBElement

        logger.Trace("Starting TV Season scrape")

        For Each tScrapeItem As ScrapeItem In Args.ScrapeList
            Dim tURL As String = String.Empty
            Dim tUrlList As New List(Of Themes)

            Cancelled = False

            If bwTVSeasonScraper.CancellationPending Then Exit For

            dScrapeRow = tScrapeItem.DataRow

            DBScrapeSeason = Master.DB.LoadTVSeasonFromDB(Convert.ToInt64(tScrapeItem.DataRow.Item("idSeason")), True)
            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, DBScrapeMovie)

            logger.Trace(String.Format("Start scraping: {0}: Season {1}", DBScrapeSeason.TVShow.Title, DBScrapeSeason.TVSeason.Season))

            If tScrapeItem.ScrapeModifier.SeasonNFO Then
                bwTVSeasonScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(253, "Scraping Data"), ":"))
                If ModulesManager.Instance.ScrapeData_TVShow(DBScrapeSeason, tScrapeItem.ScrapeModifier, Args.ScrapeType, Args.Options_TV, Args.ScrapeList.Count = 1) Then
                    Cancelled = True
                    If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                        logger.Trace(String.Concat("Canceled scraping: {0}: Season {1}", DBScrapeSeason.TVShow.Title, DBScrapeSeason.TVSeason.Season))
                        bwTVSeasonScraper.CancelAsync()
                    End If
                End If
            Else
                ' if we do not have the tvshow ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
                If String.IsNullOrEmpty(DBScrapeSeason.TVSeason.TVDB) AndAlso (tScrapeItem.ScrapeModifier.SeasonBanner Or tScrapeItem.ScrapeModifier.SeasonFanart Or _
                                                                               tScrapeItem.ScrapeModifier.SeasonLandscape Or tScrapeItem.ScrapeModifier.SeasonPoster) Then
                    Dim tOpt As New Structures.ScrapeOptions_TV 'all false value not to override any field
                    If ModulesManager.Instance.ScrapeData_TVShow(DBScrapeSeason, tScrapeItem.ScrapeModifier, Args.ScrapeType, tOpt, Args.ScrapeList.Count = 1) Then
                        Exit For
                    End If
                End If
            End If

            If bwTVSeasonScraper.CancellationPending Then Exit For

            If Not Cancelled Then
                'get all images
                If tScrapeItem.ScrapeModifier.AllSeasonsBanner OrElse _
                    tScrapeItem.ScrapeModifier.AllSeasonsFanart OrElse _
                    tScrapeItem.ScrapeModifier.AllSeasonsLandscape OrElse _
                    tScrapeItem.ScrapeModifier.AllSeasonsPoster OrElse _
                    tScrapeItem.ScrapeModifier.SeasonBanner OrElse _
                    tScrapeItem.ScrapeModifier.SeasonFanart OrElse _
                    tScrapeItem.ScrapeModifier.SeasonLandscape OrElse _
                    tScrapeItem.ScrapeModifier.SeasonPoster Then

                    Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
                    bwTVSeasonScraper.ReportProgress(-3, "Scraping Season Images:")
                    If Not ModulesManager.Instance.ScrapeImage_TV(DBScrapeSeason, SearchResultsContainer, tScrapeItem.ScrapeModifier, Args.ScrapeList.Count = 1) Then
                        If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.TVImagesDisplayImageSelect Then
                            Using dImgSelect As New dlgImgSelect
                                If dImgSelect.ShowDialog(DBScrapeSeason, SearchResultsContainer, tScrapeItem.ScrapeModifier, Enums.ContentType.TVSeason, True) = DialogResult.OK Then
                                    DBScrapeSeason = dImgSelect.Result
                                End If
                            End Using

                            'autoscraping
                        ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
                            Dim newPreferredImages As New MediaContainers.ImagesContainer
                            Dim newPreferredEpisodeImages As New List(Of MediaContainers.EpisodeOrSeasonImagesContainer)
                            Dim newPreferredSeasonImages As New List(Of MediaContainers.EpisodeOrSeasonImagesContainer)
                            Images.SetDefaultImages(DBScrapeSeason, newPreferredImages, SearchResultsContainer, tScrapeItem.ScrapeModifier, Enums.ContentType.TVSeason, newPreferredSeasonImages, newPreferredEpisodeImages)
                            DBScrapeSeason.ImagesContainer = newPreferredImages
                        End If
                    End If
                End If

                If bwTVSeasonScraper.CancellationPending Then Exit For

                If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.ScraperMulti_TVSeason, Nothing, Nothing, False, DBScrapeSeason)
                    bwTVSeasonScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
                    Master.DB.SaveTVSeasonToDB(DBScrapeSeason, False, True)
                    bwTVSeasonScraper.ReportProgress(-2, DBScrapeSeason.ID)
                End If
            End If

            logger.Trace(String.Format("Ended scraping: {0}: Season {1}", DBScrapeSeason.TVShow.Title, DBScrapeSeason.TVSeason.Season))
        Next

        e.Result = New Results With {.DBElement = DBScrapeSeason, .ScrapeType = Args.ScrapeType, .Cancelled = bwTVSeasonScraper.CancellationPending}
        logger.Trace("Ended TV Season scrape")
    End Sub

    Private Sub bwTVSeasonScraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwTVSeasonScraper.ProgressChanged
        If e.ProgressPercentage = -1 Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"tvseasonscraped", 3, Master.eLang.GetString(247, "Season Scraped"), e.UserState.ToString, Nothing}))
        ElseIf e.ProgressPercentage = -2 Then
            RefreshRow_TVSeason(CLng(e.UserState))
        ElseIf e.ProgressPercentage = -3 Then
            Me.tslLoading.Text = e.UserState.ToString
        Else
            Me.tspbLoading.Value += e.ProgressPercentage
            Me.SetStatus(e.UserState.ToString)
        End If
    End Sub

    Private Sub bwNonScrape_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwNonScrape.RunWorkerCompleted
        Dim configpath As String = FileUtils.Common.ReturnSettingsFile("Settings", "ScraperStatus.xml")
        If File.Exists(configpath) Then
            File.Delete(configpath)
        End If
        Me.tslLoading.Visible = False
        Me.tspbLoading.Visible = False
        Me.btnCancel.Visible = False
        Me.lblCanceling.Visible = False
        Me.prbCanceling.Visible = False
        Me.pnlCancel.Visible = False
        Me.SetControlsEnabled(True)
        Me.EnableFilters_Movies(True)
        Me.EnableFilters_MovieSets(True)
        Me.EnableFilters_Shows(True)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub bwNonScrape_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwNonScrape.DoWork
        Dim scrapeMovie As Database.DBElement
        Dim iCount As Integer = 0
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            If Me.dtMovies.Rows.Count > 0 Then

                Select Case Args.ScrapeType
                    Case Enums.ScrapeType.CleanFolders
                        Dim fDeleter As New FileUtils.Delete
                        For Each drvRow As DataRow In Me.dtMovies.Rows
                            Try
                                Me.bwNonScrape.ReportProgress(iCount, drvRow.Item("Title"))
                                iCount += 1
                                If Convert.ToBoolean(drvRow.Item("Lock")) Then Continue For

                                If Me.bwNonScrape.CancellationPending Then GoTo doCancel

                                scrapeMovie = Master.DB.LoadMovieFromDB(Convert.ToInt64(drvRow.Item("idMovie")))

                                fDeleter.GetItemsToDelete(True, scrapeMovie)

                                Me.Reload_Movie(Convert.ToInt64(drvRow.Item("idMovie")), True, False)

                                Me.bwNonScrape.ReportProgress(iCount, String.Format("[[{0}]]", drvRow.Item("idMovie").ToString))
                            Catch ex As Exception
                                logger.Error(New StackFrame().GetMethod().Name, ex)
                            End Try
                        Next
                    Case Enums.ScrapeType.CopyBackdrops 'TODO: check MovieBackdropsPath and VIDEO_TS parent
                        Dim sPath As String = String.Empty
                        For Each drvRow As DataRow In Me.dtMovies.Rows
                            Me.bwNonScrape.ReportProgress(iCount, drvRow.Item("Title").ToString)
                            iCount += 1

                            If Me.bwNonScrape.CancellationPending Then GoTo doCancel
                            sPath = drvRow.Item("FanartPath").ToString
                            If Not String.IsNullOrEmpty(sPath) Then
                                If FileUtils.Common.isVideoTS(sPath) Then
                                    'If Master.eSettings.VideoTSParent Then
                                    '    FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName, Directory.GetParent(Directory.GetParent(sPath).FullName).Name), "-fanart.jpg")))
                                    'Else
                                    If Path.GetFileName(sPath).ToLower = "fanart.jpg" Then
                                        FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Directory.GetParent(Directory.GetParent(sPath).FullName).Name, "-fanart.jpg")))
                                    Else
                                        FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, Path.GetFileName(sPath)))
                                    End If
                                    'End If
                                ElseIf FileUtils.Common.isBDRip(sPath) Then
                                    'If Master.eSettings.VideoTSParent Then
                                    '    FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName).FullName, Directory.GetParent(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName).Name), "-fanart.jpg")))
                                    'Else
                                    If Path.GetFileName(sPath).ToLower = "fanart.jpg" Then
                                        FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Directory.GetParent(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName).Name, "-fanart.jpg")))
                                    Else
                                        FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, Path.GetFileName(sPath)))
                                    End If
                                    'End If
                                Else
                                    If Path.GetFileName(sPath).ToLower = "fanart.jpg" Then
                                        FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, String.Concat(Path.GetFileNameWithoutExtension(drvRow.Item("MoviePath").ToString), "-fanart.jpg")))
                                    Else
                                        FileUtils.Common.MoveFileWithStream(sPath, Path.Combine(Master.eSettings.MovieBackdropsPath, Path.GetFileName(sPath)))
                                    End If

                                End If
                            End If
                        Next
                End Select
doCancel:
                If Not Args.ScrapeType = Enums.ScrapeType.CopyBackdrops Then
                    SQLtransaction.Commit()
                End If
            End If
        End Using
    End Sub

    Private Sub bwNonScrape_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwNonScrape.ProgressChanged
        If Not Master.isCL Then
            If Regex.IsMatch(e.UserState.ToString, "\[\[[0-9]+\]\]") AndAlso Me.dgvMovies.SelectedRows.Count > 0 Then
                Try
                    If Me.dgvMovies.SelectedRows(0).Cells("idMovie").Value.ToString = e.UserState.ToString.Replace("[[", String.Empty).Replace("]]", String.Empty).Trim Then
                        Me.SelectRow_Movie(Me.dgvMovies.SelectedRows(0).Index)
                    End If
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            Else
                Me.SetStatus(e.UserState.ToString)
                Me.tspbLoading.Value = e.ProgressPercentage
            End If
        End If

        Me.dgvMovies.Invalidate()
    End Sub

    Private Sub bwReload_Movies_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwReload_Movies.DoWork
        Dim iCount As Integer = 0
        Dim MovieIDs As New Dictionary(Of Long, String)
        Dim doFill As Boolean = False

        For Each sRow As DataRow In Me.dtMovies.Rows
            MovieIDs.Add(Convert.ToInt64(sRow.Item("idMovie")), sRow.Item("ListTitle").ToString)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each KVP As KeyValuePair(Of Long, String) In MovieIDs
                If Me.bwReload_Movies.CancellationPending Then Return
                Me.bwReload_Movies.ReportProgress(iCount, KVP.Value)
                If Me.Reload_Movie(KVP.Key, True, False) Then
                    doFill = True
                Else
                    Me.bwReload_Movies.ReportProgress(-1, KVP.Key)
                End If
                iCount += 1
            Next
            SQLtransaction.Commit()
        End Using
        e.Result = New Results With {.doFill = doFill}
    End Sub

    Private Sub bwReload_Movies_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwReload_Movies.ProgressChanged
        If e.ProgressPercentage = -1 Then
            RefreshRow_Movie(CLng(e.UserState))
        Else
            Me.SetStatus(e.UserState.ToString)
            Me.tspbLoading.Value = e.ProgressPercentage
        End If
    End Sub

    Private Sub bwReload_Movies_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwReload_Movies.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        Me.tslLoading.Text = String.Empty
        Me.tspbLoading.Visible = False
        Me.tslLoading.Visible = False

        If Res.doFill Then
            FillList(True, True, False)
        Else
            DoTitleCheck()
            Me.SetControlsEnabled(True)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub bwReload_MovieSets_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwReload_MovieSets.DoWork
        Dim iCount As Integer = 0
        Dim MovieSetIDs As New Dictionary(Of Long, String)
        Dim doFill As Boolean = False

        For Each sRow As DataRow In Me.dtMovieSets.Rows
            MovieSetIDs.Add(Convert.ToInt64(sRow.Item("idSet")), sRow.Item("ListTitle").ToString)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each KVP As KeyValuePair(Of Long, String) In MovieSetIDs
                If Me.bwReload_MovieSets.CancellationPending Then Return
                Me.bwReload_MovieSets.ReportProgress(iCount, KVP.Value)
                If Me.Reload_MovieSet(KVP.Key, True) Then
                    doFill = True
                Else
                    Me.bwReload_MovieSets.ReportProgress(-1, KVP.Key)
                End If
                iCount += 1
            Next
            SQLtransaction.Commit()
        End Using
        e.Result = New Results With {.doFill = doFill}
    End Sub

    Private Sub bwReload_MovieSets_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwReload_MovieSets.ProgressChanged
        If e.ProgressPercentage = -1 Then
            RefreshRow_MovieSet(CLng(e.UserState))
        Else
            Me.SetStatus(e.UserState.ToString)
            Me.tspbLoading.Value = e.ProgressPercentage
        End If
    End Sub

    Private Sub bwReload_MovieSets_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwReload_MovieSets.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        Me.tslLoading.Text = String.Empty
        Me.tspbLoading.Visible = False
        Me.tslLoading.Visible = False

        If Res.doFill Then
            FillList(False, True, False)
        Else
            DoTitleCheck()
            Me.SetControlsEnabled(True)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub bwReload_TVShows_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwReload_TVShows.DoWork
        Dim iCount As Integer = 0
        Dim ShowIDs As New Dictionary(Of Long, String)
        Dim doFill As Boolean = False

        For Each sRow As DataRow In Me.dtTVShows.Rows
            ShowIDs.Add(Convert.ToInt64(sRow.Item("idShow")), sRow.Item("ListTitle").ToString)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each KVP As KeyValuePair(Of Long, String) In ShowIDs
                If Me.bwReload_TVShows.CancellationPending Then Return
                Me.bwReload_TVShows.ReportProgress(iCount, KVP.Value)
                If Me.Reload_TVShow(KVP.Key, True, False, True) Then
                    doFill = True
                Else
                    Me.bwReload_TVShows.ReportProgress(-1, KVP.Key)
                End If
                iCount += 1
            Next
            SQLtransaction.Commit()
        End Using
        e.Result = New Results With {.doFill = doFill}
    End Sub

    Private Sub bwReload_TVShows_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwReload_TVShows.ProgressChanged
        If e.ProgressPercentage = -1 Then
            RefreshRow_TVShow(CLng(e.UserState))
        Else
            Me.SetStatus(e.UserState.ToString)
            Me.tspbLoading.Value = e.ProgressPercentage
        End If
    End Sub

    Private Sub bwReload_TVShows_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwReload_TVShows.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        Me.tslLoading.Text = String.Empty
        Me.tspbLoading.Visible = False
        Me.tslLoading.Visible = False

        If Res.doFill Then
            FillList(False, False, True)
        Else
            Me.SetControlsEnabled(True)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub bwRewrite_Movies_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwRewrite_Movies.DoWork
        Dim iCount As Integer = 0
        Dim MovieIDs As New Dictionary(Of Long, String)

        For Each sRow As DataRow In Me.dtMovies.Rows
            MovieIDs.Add(Convert.ToInt64(sRow.Item("idMovie")), sRow.Item("ListTitle").ToString)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each KVP As KeyValuePair(Of Long, String) In MovieIDs
                If Me.bwRewrite_Movies.CancellationPending Then Return
                Me.bwRewrite_Movies.ReportProgress(iCount, KVP.Value)
                Me.RewriteMovie(KVP.Key, True)
                iCount += 1
            Next
            SQLtransaction.Commit()
        End Using
    End Sub

    Private Sub bwRewrite_Movies_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwRewrite_Movies.ProgressChanged
        Me.SetStatus(e.UserState.ToString)
        Me.tspbLoading.Value = e.ProgressPercentage
    End Sub

    Private Sub bwRewrite_Movies_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwRewrite_Movies.RunWorkerCompleted
        Me.tslLoading.Text = String.Empty
        Me.tslLoading.Visible = False
        Me.tspbLoading.Visible = False
        Me.btnCancel.Visible = False
        Me.lblCanceling.Visible = False
        Me.prbCanceling.Visible = False
        Me.pnlCancel.Visible = False

        Me.FillList(True, True, True)
    End Sub

    Private Sub cbFilterVideoSource_Movies_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterVideoSource_Movies.SelectedIndexChanged
        Try
            While Me.fScanner.IsBusy OrElse Me.bwMetaData.IsBusy OrElse Me.bwLoadMovieInfo.IsBusy OrElse Me.bwDownloadPic.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse Me.bwRewrite_Movies.IsBusy OrElse Me.bwCleanDB.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            For i As Integer = Me.FilterArray_Movies.Count - 1 To 0 Step -1
                If Me.FilterArray_Movies(i).ToString.StartsWith("VideoSource =") Then
                    Me.FilterArray_Movies.RemoveAt(i)
                End If
            Next

            If Not cbFilterVideoSource_Movies.Text = Master.eLang.All Then
                Me.FilterArray_Movies.Add(String.Format("VideoSource = '{0}'", If(cbFilterVideoSource_Movies.Text = Master.eLang.None, String.Empty, cbFilterVideoSource_Movies.Text)))
            End If

            Me.RunFilter_Movies()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cbFilterLists_Movies_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilterLists_Movies.SelectedIndexChanged
        While Me.fScanner.IsBusy OrElse Me.bwMetaData.IsBusy OrElse Me.bwLoadMovieInfo.IsBusy OrElse Me.bwDownloadPic.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse Me.bwRewrite_Movies.IsBusy OrElse Me.bwCleanDB.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
        If Not Me.currList_Movies = CType(Me.cbFilterLists_Movies.SelectedItem, KeyValuePair(Of String, String)).Value Then
            Me.currList_Movies = CType(Me.cbFilterLists_Movies.SelectedItem, KeyValuePair(Of String, String)).Value
            ModulesManager.Instance.RuntimeObjects.ListMovies = Me.currList_Movies
            FillList(True, False, False)
        End If
    End Sub

    Private Sub cbFilterLists_MovieSets_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilterLists_MovieSets.SelectedIndexChanged
        While Me.fScanner.IsBusy OrElse Me.bwMetaData.IsBusy OrElse Me.bwLoadMovieSetInfo.IsBusy OrElse Me.bwDownloadPic.IsBusy OrElse Me.bwMovieSetScraper.IsBusy OrElse Me.bwReload_MovieSets.IsBusy OrElse Me.bwRewrite_Movies.IsBusy OrElse Me.bwCleanDB.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
        If Not Me.currList_MovieSets = CType(Me.cbFilterLists_MovieSets.SelectedItem, KeyValuePair(Of String, String)).Value Then
            Me.currList_MovieSets = CType(Me.cbFilterLists_MovieSets.SelectedItem, KeyValuePair(Of String, String)).Value
            ModulesManager.Instance.RuntimeObjects.ListMovieSets = Me.currList_MovieSets
            FillList(False, True, False)
        End If
    End Sub

    Private Sub cbFilterLists_Shows_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilterLists_Shows.SelectedIndexChanged
        While Me.fScanner.IsBusy OrElse Me.bwMetaData.IsBusy OrElse Me.bwLoadMovieInfo.IsBusy OrElse Me.bwDownloadPic.IsBusy OrElse Me.bwReload_TVShows.IsBusy OrElse Me.bwCleanDB.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
        If Not Me.currList_Shows = CType(Me.cbFilterLists_Shows.SelectedItem, KeyValuePair(Of String, String)).Value Then
            Me.currList_Shows = CType(Me.cbFilterLists_Shows.SelectedItem, KeyValuePair(Of String, String)).Value
            ModulesManager.Instance.RuntimeObjects.ListShows = Me.currList_Shows
            FillList(False, False, True)
        End If
    End Sub

    Private Sub SetFilterMissing_Movies()
        Dim MissingFilter As New List(Of String)
        Me.FilterArray_Movies.Remove(filMissing_Movies)
        If Me.chkFilterMissing_Movies.Checked Then
            With Master.eSettings
                If .MovieMissingBanner Then MissingFilter.Add("BannerPath IS NULL OR BannerPath=''")
                If .MovieMissingClearArt Then MissingFilter.Add("ClearArtPath IS NULL OR ClearArtPath=''")
                If .MovieMissingClearLogo Then MissingFilter.Add("ClearLogoPath IS NULL OR ClearLogoPath=''")
                If .MovieMissingDiscArt Then MissingFilter.Add("DiscArtPath IS NULL OR DiscArtPath=''")
                If .MovieMissingEFanarts Then MissingFilter.Add("EFanartsPath IS NULL OR EFanartsPath=''")
                If .MovieMissingEThumbs Then MissingFilter.Add("EThumbsPath IS NULL OR EThumbsPath=''")
                If .MovieMissingFanart Then MissingFilter.Add("FanartPath IS NULL OR FanartPath=''")
                If .MovieMissingLandscape Then MissingFilter.Add("LandscapePath IS NULL OR LandscapePath=''")
                If .MovieMissingNFO Then MissingFilter.Add("NfoPath IS NULL OR NfoPath=''")
                If .MovieMissingPoster Then MissingFilter.Add("PosterPath IS NULL OR PosterPath=''")
                If .MovieMissingSubtitles Then MissingFilter.Add("HasSub = 0")
                If .MovieMissingTheme Then MissingFilter.Add("ThemePath IS NULL OR ThemePath=''")
                If .MovieMissingTrailer Then MissingFilter.Add("TrailerPath IS NULL OR TrailerPath=''")
            End With
            filMissing_Movies = Microsoft.VisualBasic.Strings.Join(MissingFilter.ToArray, " OR ")
            If filMissing_Movies IsNot Nothing Then Me.FilterArray_Movies.Add(filMissing_Movies)
        End If
        Me.RunFilter_Movies()
    End Sub

    Private Sub SetFilterMissing_MovieSets()
        Dim MissingFilter As New List(Of String)
        Me.FilterArray_MovieSets.Remove(filMissing_MovieSets)
        If Me.chkFilterMissing_MovieSets.Checked Then
            With Master.eSettings
                If .MovieSetMissingBanner Then MissingFilter.Add("BannerPath IS NULL OR BannerPath=''")
                If .MovieSetMissingClearArt Then MissingFilter.Add("ClearArtPath IS NULL OR ClearArtPath=''")
                If .MovieSetMissingClearLogo Then MissingFilter.Add("ClearLogoPath IS NULL OR ClearLogoPath=''")
                If .MovieSetMissingDiscArt Then MissingFilter.Add("DiscArtPath IS NULL OR DiscArtPath=''")
                If .MovieSetMissingFanart Then MissingFilter.Add("FanartPath IS NULL OR FanartPath=''")
                If .MovieSetMissingLandscape Then MissingFilter.Add("LandscapePath IS NULL OR LandscapePath=''")
                If .MovieSetMissingNFO Then MissingFilter.Add("NfoPath IS NULL OR NfoPath=''")
                If .MovieSetMissingPoster Then MissingFilter.Add("PosterPath IS NULL OR PosterPath=''")
            End With
            filMissing_MovieSets = Microsoft.VisualBasic.Strings.Join(MissingFilter.ToArray, " OR ")
            If filMissing_MovieSets IsNot Nothing Then Me.FilterArray_MovieSets.Add(filMissing_MovieSets)
        End If
        Me.RunFilter_MovieSets()
    End Sub

    Private Sub SetFilterMissing_Shows()
        Dim MissingFilter As New List(Of String)
        Me.FilterArray_Shows.Remove(filMissing_Shows)
        If Me.chkFilterMissing_Shows.Checked Then
            With Master.eSettings
                If .TVShowMissingBanner Then MissingFilter.Add("BannerPath IS NULL OR BannerPath=''")
                If .TVShowMissingCharacterArt Then MissingFilter.Add("CharacterArtPath IS NULL OR CharacterArtPath=''")
                If .TVShowMissingClearArt Then MissingFilter.Add("ClearArtPath IS NULL OR ClearArtPath=''")
                If .TVShowMissingClearLogo Then MissingFilter.Add("ClearLogoPath IS NULL OR ClearLogoPath=''")
                If .TVShowMissingEFanarts Then MissingFilter.Add("EFanartsPath IS NULL OR EFanartsPath=''")
                If .TVShowMissingFanart Then MissingFilter.Add("FanartPath IS NULL OR FanartPath=''")
                If .TVShowMissingLandscape Then MissingFilter.Add("LandscapePath IS NULL OR LandscapePath=''")
                If .TVShowMissingNFO Then MissingFilter.Add("NfoPath IS NULL OR NfoPath=''")
                If .TVShowMissingPoster Then MissingFilter.Add("PosterPath IS NULL OR PosterPath=''")
                If .TVShowMissingTheme Then MissingFilter.Add("ThemePath IS NULL OR ThemePath=''")
            End With
            filMissing_Shows = Microsoft.VisualBasic.Strings.Join(MissingFilter.ToArray, " OR ")
            If filMissing_Shows IsNot Nothing Then Me.FilterArray_Shows.Add(filMissing_Shows)
        End If
        Me.RunFilter_Shows()
    End Sub


    Private Sub SetFilterYear_Movies()
        Try
            If Not String.IsNullOrEmpty(cbFilterYearFrom_Movies.Text) AndAlso Not cbFilterYearFrom_Movies.Text = Master.eLang.All Then

                Me.FilterArray_Movies.Remove(Me.filYear_Movies)
                Me.filYear_Movies = String.Empty

                Select Case cbFilterYearModFrom_Movies.Text
                    Case ">="
                        RemoveHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged
                        cbFilterYearModTo_Movies.Enabled = True
                        AddHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged

                        RemoveHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged
                        cbFilterYearTo_Movies.Enabled = True
                        AddHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged

                        If Not String.IsNullOrEmpty(cbFilterYearTo_Movies.Text) AndAlso Not cbFilterYearTo_Movies.Text = Master.eLang.All Then
                            Me.filYear_Movies = String.Concat("Year ", cbFilterYearModFrom_Movies.Text, " '", cbFilterYearFrom_Movies.Text, _
                                                              "' AND Year ", cbFilterYearModTo_Movies.Text, " '", cbFilterYearTo_Movies.Text, "'")
                        Else
                            Me.filYear_Movies = String.Concat("Year ", cbFilterYearModFrom_Movies.Text, " '", cbFilterYearFrom_Movies.Text, "'")
                        End If

                    Case ">"
                        RemoveHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged
                        cbFilterYearModTo_Movies.Enabled = True
                        AddHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged

                        RemoveHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged
                        cbFilterYearTo_Movies.Enabled = True
                        AddHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged

                        If Not String.IsNullOrEmpty(cbFilterYearTo_Movies.Text) AndAlso Not cbFilterYearTo_Movies.Text = Master.eLang.All Then
                            Me.filYear_Movies = String.Concat("Year ", cbFilterYearModFrom_Movies.Text, " '", cbFilterYearFrom_Movies.Text, _
                                                              "' AND Year ", cbFilterYearModTo_Movies.Text, " '", cbFilterYearTo_Movies.Text, "'")
                        Else
                            Me.filYear_Movies = String.Concat("Year ", cbFilterYearModFrom_Movies.Text, " '", cbFilterYearFrom_Movies.Text, "'")
                        End If

                    Case Else
                        RemoveHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged
                        cbFilterYearModTo_Movies.Enabled = False
                        cbFilterYearModTo_Movies.SelectedIndex = 0
                        AddHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged

                        RemoveHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged
                        cbFilterYearTo_Movies.Enabled = False
                        cbFilterYearTo_Movies.SelectedIndex = 0
                        AddHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged

                        Me.filYear_Movies = String.Concat("Year ", cbFilterYearModFrom_Movies.Text, " '", cbFilterYearFrom_Movies.Text, "'")
                End Select

                Me.FilterArray_Movies.Add(Me.filYear_Movies)
                Me.RunFilter_Movies()
            Else
                RemoveHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged
                cbFilterYearModTo_Movies.Enabled = False
                cbFilterYearModTo_Movies.SelectedIndex = 0
                AddHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged

                RemoveHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged
                cbFilterYearTo_Movies.Enabled = False
                cbFilterYearTo_Movies.SelectedIndex = 0
                AddHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged

                If Not String.IsNullOrEmpty(Me.filYear_Movies) Then
                    Me.FilterArray_Movies.Remove(Me.filYear_Movies)
                    Me.filYear_Movies = String.Empty
                    Me.RunFilter_Movies()
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cbFilterYearModFrom_Movies_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterYearModFrom_Movies.SelectedIndexChanged
        SetFilterYear_Movies()
    End Sub

    Private Sub cbFilterYearModTo_Movies_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterYearModTo_Movies.SelectedIndexChanged
        SetFilterYear_Movies()
    End Sub

    Private Sub cbFilterYearFrom_Movies_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterYearFrom_Movies.SelectedIndexChanged
        SetFilterYear_Movies()
    End Sub

    Private Sub cbFilterYearTo_Movies_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterYearTo_Movies.SelectedIndexChanged
        SetFilterYear_Movies()
    End Sub

    Private Sub cbSearchMovies_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSearchMovies.SelectedIndexChanged
        Me.currTextSearch_Movies = Me.txtSearchMovies.Text

        Me.tmrSearchWait_Movies.Enabled = False
        Me.tmrSearch_Movies.Enabled = False
        Me.tmrSearchWait_Movies.Enabled = True
    End Sub

    Private Sub cbSearchMovieSets_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSearchMovieSets.SelectedIndexChanged
        Me.currTextSearch_MovieSets = Me.txtSearchMovieSets.Text

        Me.tmrSearchWait_MovieSets.Enabled = False
        Me.tmrSearch_MovieSets.Enabled = False
        Me.tmrSearchWait_MovieSets.Enabled = True
    End Sub

    Private Sub cbSearchShows_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSearchShows.SelectedIndexChanged
        Me.currTextSearch_Shows = Me.txtSearchShows.Text

        Me.tmrSearchWait_Shows.Enabled = False
        Me.tmrSearch_Shows.Enabled = False
        Me.tmrSearchWait_Shows.Enabled = True
    End Sub

    Private Sub chkFilterDuplicates_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterDuplicates_Movies.Click
        Try
            Me.RunFilter_Movies(True)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub chkFilterEmpty_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterEmpty_MovieSets.Click
        If Me.chkFilterEmpty_MovieSets.Checked Then
            Me.FilterArray_MovieSets.Add("Count = 0")
        Else
            Me.FilterArray_MovieSets.Remove("Count = 0")
        End If
        Me.RunFilter_MovieSets()
    End Sub

    Private Sub chkFilterMultiple_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMultiple_MovieSets.Click
        If Me.chkFilterMultiple_MovieSets.Checked Then
            Me.FilterArray_MovieSets.Add("Count > 1")
        Else
            Me.FilterArray_MovieSets.Remove("Count > 1")
        End If
        Me.RunFilter_MovieSets()
    End Sub

    Private Sub chkFilterOne_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterOne_MovieSets.Click
        If Me.chkFilterOne_MovieSets.Checked Then
            Me.FilterArray_MovieSets.Add("Count = 1")
        Else
            Me.FilterArray_MovieSets.Remove("Count = 1")
        End If
        Me.RunFilter_MovieSets()
    End Sub

    Private Sub chkFilterLock_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterLock_Movies.Click
        If Me.chkFilterLock_Movies.Checked Then
            Me.FilterArray_Movies.Add("Lock = 1")
        Else
            Me.FilterArray_Movies.Remove("Lock = 1")
        End If
        Me.RunFilter_Movies()
    End Sub

    Private Sub chkFilterLock_MovieSets_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterLock_MovieSets.Click
        If Me.chkFilterLock_MovieSets.Checked Then
            Me.FilterArray_MovieSets.Add("Lock = 1")
        Else
            Me.FilterArray_MovieSets.Remove("Lock = 1")
        End If
        Me.RunFilter_MovieSets()
    End Sub

    Private Sub chkFilterLock_Shows_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterLock_Shows.Click
        If Me.chkFilterLock_Shows.Checked Then
            Me.FilterArray_Shows.Add("Lock = 1")
        Else
            Me.FilterArray_Shows.Remove("Lock = 1")
        End If
        Me.RunFilter_Shows()
    End Sub

    Private Sub chkFilterMark_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMark_Movies.Click
        If Me.chkFilterMark_Movies.Checked Then
            Me.FilterArray_Movies.Add("mark = 1")
        Else
            Me.FilterArray_Movies.Remove("mark = 1")
        End If
        Me.RunFilter_Movies()
    End Sub

    Private Sub chkFilterMark_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMark_MovieSets.Click
        If Me.chkFilterMark_MovieSets.Checked Then
            Me.FilterArray_MovieSets.Add("mark = 1")
        Else
            Me.FilterArray_MovieSets.Remove("mark = 1")
        End If
        Me.RunFilter_MovieSets()
    End Sub

    Private Sub chkFilterMark_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMark_Shows.Click
        If Me.chkFilterMark_Shows.Checked Then
            Me.FilterArray_Shows.Add("mark = 1")
        Else
            Me.FilterArray_Shows.Remove("mark = 1")
        End If
        Me.RunFilter_Shows()
    End Sub

    Private Sub chkFilterMarkCustom1_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMarkCustom1_Movies.Click
        If Me.chkFilterMarkCustom1_Movies.Checked Then
            Me.FilterArray_Movies.Add("markcustom1 = 1")
        Else
            Me.FilterArray_Movies.Remove("markcustom1 = 1")
        End If
        Me.RunFilter_Movies()
    End Sub

    Private Sub chkFilterMarkCustom2_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMarkCustom2_Movies.Click
        If Me.chkFilterMarkCustom2_Movies.Checked Then
            Me.FilterArray_Movies.Add("markcustom2 = 1")
        Else
            Me.FilterArray_Movies.Remove("markcustom2 = 1")
        End If
        Me.RunFilter_Movies()
    End Sub

    Private Sub chkFilterMarkCustom3_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMarkCustom3_Movies.Click
        If Me.chkFilterMarkCustom3_Movies.Checked Then
            Me.FilterArray_Movies.Add("markcustom3 = 1")
        Else
            Me.FilterArray_Movies.Remove("markcustom3 = 1")
        End If
        Me.RunFilter_Movies()
    End Sub

    Private Sub chkFilterMarkCustom4_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterMarkCustom4_Movies.Click
        If Me.chkFilterMarkCustom4_Movies.Checked Then
            Me.FilterArray_Movies.Add("markcustom4 = 1")
        Else
            Me.FilterArray_Movies.Remove("markcustom4 = 1")
        End If
        Me.RunFilter_Movies()
    End Sub

    Private Sub chkFilterMissing_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterMissing_Movies.Click
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkFilterMissing_MovieSets_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterMissing_MovieSets.Click
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkFilterMissing_Shows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterMissing_Shows.Click
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkFilterNew_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterNew_Movies.Click
        If Me.chkFilterNew_Movies.Checked Then
            Me.FilterArray_Movies.Add("new = 1")
        Else
            Me.FilterArray_Movies.Remove("new = 1")
        End If
        Me.RunFilter_Movies()
    End Sub

    Private Sub chkFilterNew_Moviesets_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterNew_MovieSets.Click
        If Me.chkFilterNew_MovieSets.Checked Then
            Me.FilterArray_MovieSets.Add("new = 1")
        Else
            Me.FilterArray_MovieSets.Remove("new = 1")
        End If
        Me.RunFilter_MovieSets()
    End Sub

    Private Sub chkFilterNewEpisodes_Shows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterNewEpisodes_Shows.Click
        If Me.chkFilterNewEpisodes_Shows.Checked Then
            Me.FilterArray_Shows.Add("NewEpisodes > 0")
        Else
            Me.FilterArray_Shows.Remove("NewEpisodes > 0")
        End If
        Me.RunFilter_Shows()
    End Sub

    Private Sub chkFilterNewShows_Shows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterNewShows_Shows.Click
        If Me.chkFilterNewShows_Shows.Checked Then
            Me.FilterArray_Shows.Add("new = 1")
        Else
            Me.FilterArray_Shows.Remove("new = 1")
        End If
        Me.RunFilter_Shows()
    End Sub

    Private Sub chkFilterTolerance_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilterTolerance_Movies.Click
        If Me.chkFilterTolerance_Movies.Checked Then
            Me.FilterArray_Movies.Add("OutOfTolerance = 1")
        Else
            Me.FilterArray_Movies.Remove("OutOfTolerance = 1")
        End If
        Me.RunFilter_Movies()
    End Sub

    Private Sub chkMovieMissingBanner_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingBanner.CheckedChanged
        Master.eSettings.MovieMissingBanner = Me.chkMovieMissingBanner.Checked
        Me.chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        Me.chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingClearArt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingClearArt.CheckedChanged
        Master.eSettings.MovieMissingClearArt = Me.chkMovieMissingClearArt.Checked
        Me.chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        Me.chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingClearLogo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingClearLogo.CheckedChanged
        Master.eSettings.MovieMissingClearLogo = Me.chkMovieMissingClearLogo.Checked
        Me.chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        Me.chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingDiscArt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingDiscArt.CheckedChanged
        Master.eSettings.MovieMissingDiscArt = Me.chkMovieMissingDiscArt.Checked
        Me.chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        Me.chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingEFanarts_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingEFanarts.CheckedChanged
        Master.eSettings.MovieMissingEFanarts = Me.chkMovieMissingEFanarts.Checked
        Me.chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        Me.chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingEThumbs_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingEThumbs.CheckedChanged
        Master.eSettings.MovieMissingEThumbs = Me.chkMovieMissingEThumbs.Checked
        Me.chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        Me.chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingFanart_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingFanart.CheckedChanged
        Master.eSettings.MovieMissingFanart = Me.chkMovieMissingFanart.Checked
        Me.chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        Me.chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingLandscape_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingLandscape.CheckedChanged
        Master.eSettings.MovieMissingLandscape = Me.chkMovieMissingLandscape.Checked
        Me.chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        Me.chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingNFO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingNFO.CheckedChanged
        Master.eSettings.MovieMissingNFO = Me.chkMovieMissingNFO.Checked
        Me.chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        Me.chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingPoster_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingPoster.CheckedChanged
        Master.eSettings.MovieMissingPoster = Me.chkMovieMissingPoster.Checked
        Me.chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        Me.chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingSubtitles_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingSubtitles.CheckedChanged
        Master.eSettings.MovieMissingSubtitles = Me.chkMovieMissingSubtitles.Checked
        Me.chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        Me.chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingTheme_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingTheme.CheckedChanged
        Master.eSettings.MovieMissingTheme = Me.chkMovieMissingTheme.Checked
        Me.chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        Me.chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieMissingTrailer_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieMissingTrailer.CheckedChanged
        Master.eSettings.MovieMissingTrailer = Me.chkMovieMissingTrailer.Checked
        Me.chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        Me.chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        SetFilterMissing_Movies()
    End Sub

    Private Sub chkMovieSetMissingBanner_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingBanner.CheckedChanged
        Master.eSettings.MovieSetMissingBanner = Me.chkMovieSetMissingBanner.Checked
        Me.chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Me.chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkMovieSetMissingClearArt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingClearArt.CheckedChanged
        Master.eSettings.MovieSetMissingClearArt = Me.chkMovieSetMissingClearArt.Checked
        Me.chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Me.chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkMovieSetMissingClearLogo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingClearLogo.CheckedChanged
        Master.eSettings.MovieSetMissingClearLogo = Me.chkMovieSetMissingClearLogo.Checked
        Me.chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Me.chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkMovieSetMissingDiscArt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingDiscArt.CheckedChanged
        Master.eSettings.MovieSetMissingDiscArt = Me.chkMovieSetMissingDiscArt.Checked
        Me.chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Me.chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkMovieSetMissingFanart_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingFanart.CheckedChanged
        Master.eSettings.MovieSetMissingFanart = Me.chkMovieSetMissingFanart.Checked
        Me.chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Me.chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkMovieSetMissingLandscape_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingLandscape.CheckedChanged
        Master.eSettings.MovieSetMissingLandscape = Me.chkMovieSetMissingLandscape.Checked
        Me.chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Me.chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkMovieSetMissingNFO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingNFO.CheckedChanged
        Master.eSettings.MovieSetMissingNFO = Me.chkMovieSetMissingNFO.Checked
        Me.chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Me.chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkMovieSetMissingPoster_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMovieSetMissingPoster.CheckedChanged
        Master.eSettings.MovieSetMissingPoster = Me.chkMovieSetMissingPoster.Checked
        Me.chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Me.chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        SetFilterMissing_MovieSets()
    End Sub

    Private Sub chkShowMissingBanner_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingBanner.CheckedChanged
        Master.eSettings.TVShowMissingBanner = Me.chkShowMissingBanner.Checked
        Me.chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        Me.chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingCharacterArt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingCharacterArt.CheckedChanged
        Master.eSettings.TVShowMissingCharacterArt = Me.chkShowMissingCharacterArt.Checked
        Me.chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        Me.chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingClearArt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingClearArt.CheckedChanged
        Master.eSettings.TVShowMissingClearArt = Me.chkShowMissingClearArt.Checked
        Me.chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        Me.chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingClearLogo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingClearLogo.CheckedChanged
        Master.eSettings.TVShowMissingClearLogo = Me.chkShowMissingClearLogo.Checked
        Me.chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        Me.chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingEFanarts_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingEFanarts.CheckedChanged
        Master.eSettings.TVShowMissingEFanarts = Me.chkShowMissingEFanarts.Checked
        Me.chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        Me.chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingFanart_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingFanart.CheckedChanged
        Master.eSettings.TVShowMissingFanart = Me.chkShowMissingFanart.Checked
        Me.chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        Me.chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingLandscape_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingLandscape.CheckedChanged
        Master.eSettings.TVShowMissingLandscape = Me.chkShowMissingLandscape.Checked
        Me.chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        Me.chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingNFO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingNFO.CheckedChanged
        Master.eSettings.TVShowMissingNFO = Me.chkShowMissingNFO.Checked
        Me.chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        Me.chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingPoster_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingPoster.CheckedChanged
        Master.eSettings.TVShowMissingPoster = Me.chkShowMissingPoster.Checked
        Me.chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        Me.chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub chkShowMissingTheme_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowMissingTheme.CheckedChanged
        Master.eSettings.TVShowMissingTheme = Me.chkShowMissingTheme.Checked
        Me.chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        Me.chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        SetFilterMissing_Shows()
    End Sub

    Private Sub clbFilterGenres_Movies_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbFilterGenres_Movies.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbFilterGenres_Movies.Items.Count - 1
                Me.clbFilterGenres_Movies.SetItemChecked(i, False)
            Next
        Else
            Me.clbFilterGenres_Movies.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub clbFilterGenres_Shows_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbFilterGenres_Shows.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbFilterGenres_Shows.Items.Count - 1
                Me.clbFilterGenres_Shows.SetItemChecked(i, False)
            Next
        Else
            Me.clbFilterGenres_Shows.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub clbFilterCountries_Movies_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbFilterCountries_Movies.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbFilterCountries_Movies.Items.Count - 1
                Me.clbFilterCountries_Movies.SetItemChecked(i, False)
            Next
        Else
            Me.clbFilterCountries_Movies.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub clbFilterGenres_Movies_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles clbFilterGenres_Movies.LostFocus
        Try
            Me.pnlFilterGenres_Movies.Visible = False
            Me.pnlFilterGenres_Movies.Tag = "NO"

            If clbFilterGenres_Movies.CheckedItems.Count > 0 Then
                Me.txtFilterGenre_Movies.Text = String.Empty
                Me.FilterArray_Movies.Remove(Me.filGenre_Movies)

                Dim alGenres As New List(Of String)
                alGenres.AddRange(clbFilterGenres_Movies.CheckedItems.OfType(Of String).ToList)

                If rbFilterAnd_Movies.Checked Then
                    Me.txtFilterGenre_Movies.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")
                Else
                    Me.txtFilterGenre_Movies.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")
                End If

                For i As Integer = 0 To alGenres.Count - 1
                    If alGenres.Item(i) = Master.eLang.None Then
                        alGenres.Item(i) = "Genre LIKE ''"
                    Else
                        alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
                    End If
                Next

                If rbFilterAnd_Movies.Checked Then
                    Me.filGenre_Movies = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND "))
                Else
                    Me.filGenre_Movies = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR "))
                End If

                Me.FilterArray_Movies.Add(Me.filGenre_Movies)
                Me.RunFilter_Movies()
            Else
                If Not String.IsNullOrEmpty(Me.filGenre_Movies) Then
                    Me.txtFilterGenre_Movies.Text = String.Empty
                    Me.FilterArray_Movies.Remove(Me.filGenre_Movies)
                    Me.filGenre_Movies = String.Empty
                    Me.RunFilter_Movies()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub clbFilterGenres_Shows_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles clbFilterGenres_Shows.LostFocus
        Try
            Me.pnlFilterGenres_Shows.Visible = False
            Me.pnlFilterGenres_Shows.Tag = "NO"

            If clbFilterGenres_Shows.CheckedItems.Count > 0 Then
                Me.txtFilterGenre_Shows.Text = String.Empty
                Me.FilterArray_Shows.Remove(Me.filGenre_Shows)

                Dim alGenres As New List(Of String)
                alGenres.AddRange(clbFilterGenres_Shows.CheckedItems.OfType(Of String).ToList)

                If rbFilterAnd_Shows.Checked Then
                    Me.txtFilterGenre_Shows.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")
                Else
                    Me.txtFilterGenre_Shows.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")
                End If

                For i As Integer = 0 To alGenres.Count - 1
                    If alGenres.Item(i) = Master.eLang.None Then
                        alGenres.Item(i) = "Genre LIKE ''"
                    Else
                        alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
                    End If
                Next

                If rbFilterAnd_Shows.Checked Then
                    Me.filGenre_Shows = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND "))
                Else
                    Me.filGenre_Shows = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR "))
                End If

                Me.FilterArray_Shows.Add(Me.filGenre_Shows)
                Me.RunFilter_Shows()
            Else
                If Not String.IsNullOrEmpty(Me.filGenre_Shows) Then
                    Me.txtFilterGenre_Shows.Text = String.Empty
                    Me.FilterArray_Shows.Remove(Me.filGenre_Shows)
                    Me.filGenre_Shows = String.Empty
                    Me.RunFilter_Shows()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub clbFilterCountries_Movies_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles clbFilterCountries_Movies.LostFocus
        Try
            Me.pnlFilterCountries_Movies.Visible = False
            Me.pnlFilterCountries_Movies.Tag = "NO"

            If clbFilterCountries_Movies.CheckedItems.Count > 0 Then
                Me.txtFilterCountry_Movies.Text = String.Empty
                Me.FilterArray_Movies.Remove(Me.filCountry_Movies)

                Dim alCountries As New List(Of String)
                alCountries.AddRange(clbFilterCountries_Movies.CheckedItems.OfType(Of String).ToList)

                If rbFilterAnd_Movies.Checked Then
                    Me.txtFilterCountry_Movies.Text = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " AND ")
                Else
                    Me.txtFilterCountry_Movies.Text = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " OR ")
                End If

                For i As Integer = 0 To alCountries.Count - 1
                    If alCountries.Item(i) = Master.eLang.None Then
                        alCountries.Item(i) = "Country LIKE ''"
                    Else
                        alCountries.Item(i) = String.Format("Country LIKE '%{0}%'", alCountries.Item(i))
                    End If
                Next

                If rbFilterAnd_Movies.Checked Then
                    Me.filCountry_Movies = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " AND "))
                Else
                    Me.filCountry_Movies = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " OR "))
                End If

                Me.FilterArray_Movies.Add(Me.filCountry_Movies)
                Me.RunFilter_Movies()
            Else
                If Not String.IsNullOrEmpty(Me.filCountry_Movies) Then
                    Me.txtFilterCountry_Movies.Text = String.Empty
                    Me.FilterArray_Movies.Remove(Me.filCountry_Movies)
                    Me.filCountry_Movies = String.Empty
                    Me.RunFilter_Movies()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub clbFilterDataFields_Movies_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles clbFilterDataFields_Movies.LostFocus, cbFilterDataField_Movies.SelectedIndexChanged
        Try
            Me.pnlFilterDataFields_Movies.Visible = False
            Me.pnlFilterDataFields_Movies.Tag = "NO"

            If clbFilterDataFields_Movies.CheckedItems.Count > 0 Then
                Me.txtFilterDataField_Movies.Text = String.Empty
                Me.FilterArray_Movies.Remove(Me.filDataField_Movies)

                Dim alDataFields As New List(Of String)
                alDataFields.AddRange(clbFilterDataFields_Movies.CheckedItems.OfType(Of String).ToList)

                If rbFilterAnd_Movies.Checked Then
                    Me.txtFilterDataField_Movies.Text = Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " AND ")
                Else
                    Me.txtFilterDataField_Movies.Text = Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " OR ")
                End If

                For i As Integer = 0 To alDataFields.Count - 1
                    If Me.cbFilterDataField_Movies.SelectedIndex = 0 Then
                        alDataFields.Item(i) = String.Format("{0} LIKE ''", alDataFields.Item(i))
                    Else
                        alDataFields.Item(i) = String.Format("{0} NOT LIKE ''", alDataFields.Item(i))
                    End If
                Next

                If rbFilterAnd_Movies.Checked Then
                    Me.filDataField_Movies = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " AND "))
                Else
                    Me.filDataField_Movies = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " OR "))
                End If

                Me.FilterArray_Movies.Add(Me.filDataField_Movies)
                Me.RunFilter_Movies()
            Else
                If Not String.IsNullOrEmpty(Me.filDataField_Movies) Then
                    Me.txtFilterDataField_Movies.Text = String.Empty
                    Me.FilterArray_Movies.Remove(Me.filDataField_Movies)
                    Me.filDataField_Movies = String.Empty
                    Me.RunFilter_Movies()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub clbFilterSource_Movies_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles clbFilterSources_Movies.LostFocus
        Try
            Me.pnlFilterSources_Movies.Visible = False
            Me.pnlFilterSources_Movies.Tag = "NO"

            If clbFilterSources_Movies.CheckedItems.Count > 0 Then
                Me.txtFilterSource_Movies.Text = String.Empty
                Me.FilterArray_Movies.Remove(Me.filSource_Movies)

                Dim alSource As New List(Of String)
                alSource.AddRange(clbFilterSources_Movies.CheckedItems.OfType(Of String).ToList)

                Me.txtFilterSource_Movies.Text = Microsoft.VisualBasic.Strings.Join(alSource.ToArray, " | ")

                For i As Integer = 0 To alSource.Count - 1
                    alSource.Item(i) = String.Format("Source = '{0}'", alSource.Item(i))
                Next

                Me.filSource_Movies = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alSource.ToArray, " OR "))

                Me.FilterArray_Movies.Add(Me.filSource_Movies)
                Me.RunFilter_Movies()
            Else
                If Not String.IsNullOrEmpty(Me.filSource_Movies) Then
                    Me.txtFilterSource_Movies.Text = String.Empty
                    Me.FilterArray_Movies.Remove(Me.filSource_Movies)
                    Me.filSource_Movies = String.Empty
                    Me.RunFilter_Movies()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub clbFilterSource_Shows_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles clbFilterSource_Shows.LostFocus
        Try
            Me.pnlFilterSources_Shows.Visible = False
            Me.pnlFilterSources_Shows.Tag = "NO"

            If clbFilterSource_Shows.CheckedItems.Count > 0 Then
                Me.txtFilterSource_Shows.Text = String.Empty
                Me.FilterArray_Shows.Remove(Me.filSource_Shows)

                Dim alSource As New List(Of String)
                alSource.AddRange(clbFilterSource_Shows.CheckedItems.OfType(Of String).ToList)

                Me.txtFilterSource_Shows.Text = Microsoft.VisualBasic.Strings.Join(alSource.ToArray, " | ")

                For i As Integer = 0 To alSource.Count - 1
                    alSource.Item(i) = String.Format("Source = '{0}'", alSource.Item(i))
                Next

                Me.filSource_Shows = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alSource.ToArray, " OR "))

                Me.FilterArray_Shows.Add(Me.filSource_Shows)
                Me.RunFilter_Shows()
            Else
                If Not String.IsNullOrEmpty(Me.filSource_Shows) Then
                    Me.txtFilterSource_Shows.Text = String.Empty
                    Me.FilterArray_Shows.Remove(Me.filSource_Shows)
                    Me.filSource_Shows = String.Empty
                    Me.RunFilter_Shows()
                End If
            End If
        Catch
        End Try
    End Sub

    Private Sub mnuMainToolsCleanDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsCleanDB.Click, cmnuTrayToolsCleanDB.Click
        CleanDB()
    End Sub

    Private Sub CleanDB()
        Me.SetControlsEnabled(False, True)
        Me.tspbLoading.Style = ProgressBarStyle.Marquee
        Me.EnableFilters_Movies(False)
        Me.EnableFilters_MovieSets(False)
        Me.EnableFilters_Shows(False)

        Me.SetStatus(Master.eLang.GetString(644, "Cleaning Database..."))
        Me.tspbLoading.Visible = True

        Me.bwCleanDB.WorkerSupportsCancellation = True
        Me.bwCleanDB.RunWorkerAsync()
    End Sub

    Private Sub CleanFiles()
        Try
            Dim sWarning As String = String.Empty
            Dim sWarningFile As String = String.Empty
            With Master.eSettings
                If .FileSystemExpertCleaner Then
                    sWarning = String.Concat(Master.eLang.GetString(102, "WARNING: If you continue, all non-whitelisted file types will be deleted!"), Environment.NewLine, Environment.NewLine, Master.eLang.GetString(101, "Are you sure you want to continue?"))
                Else
                    If .CleanDotFanartJPG Then sWarningFile += String.Concat("<movie>.fanart.jpg", Environment.NewLine)
                    If .CleanFanartJPG Then sWarningFile += String.Concat("fanart.jpg", Environment.NewLine)
                    If .CleanFolderJPG Then sWarningFile += String.Concat("folder.jpg", Environment.NewLine)
                    If .CleanMovieFanartJPG Then sWarningFile += String.Concat("<movie>-fanart.jpg", Environment.NewLine)
                    If .CleanMovieJPG Then sWarningFile += String.Concat("movie.jpg", Environment.NewLine)
                    If .CleanMovieNameJPG Then sWarningFile += String.Concat("<movie>.jpg", Environment.NewLine)
                    If .CleanMovieNFO Then sWarningFile += String.Concat("movie.nfo", Environment.NewLine)
                    If .CleanMovieNFOB Then sWarningFile += String.Concat("<movie>.nfo", Environment.NewLine)
                    If .CleanMovieTBN Then sWarningFile += String.Concat("movie.tbn", Environment.NewLine)
                    If .CleanMovieTBNB Then sWarningFile += String.Concat("<movie>.tbn", Environment.NewLine)
                    If .CleanPosterJPG Then sWarningFile += String.Concat("poster.jpg", Environment.NewLine)
                    If .CleanPosterTBN Then sWarningFile += String.Concat("poster.tbn", Environment.NewLine)
                    If .CleanExtrathumbs Then sWarningFile += String.Concat("/extrathumbs/", Environment.NewLine)
                    sWarning = String.Concat(Master.eLang.GetString(103, "WARNING: If you continue, all files of the following types will be permanently deleted:"), Environment.NewLine, Environment.NewLine, sWarningFile, Environment.NewLine, Master.eLang.GetString(101, "Are you sure you want to continue?"))
                End If
            End With
            If MessageBox.Show(sWarning, Master.eLang.GetString(104, "Are you sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                Me.NonScrape(Enums.ScrapeType.CleanFolders, Nothing)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub mnuMainToolsCleanFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsCleanFiles.Click, cmnuTrayToolsCleanFiles.Click
        Me.CleanFiles()
    End Sub

    Private Sub mnuMainToolsClearCache_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsClearCache.Click, cmnuTrayToolsClearCache.Click
        FileUtils.Delete.Cache_All()
    End Sub

    Private Sub ClearFilters_Movies(Optional ByVal Reload As Boolean = False)
        Try
            Me.bsMovies.RemoveFilter()
            Me.FilterArray_Movies.Clear()
            Me.filSearch_Movies = String.Empty
            Me.filGenre_Movies = String.Empty
            Me.filYear_Movies = String.Empty
            Me.filSource_Movies = String.Empty

            RemoveHandler txtSearchMovies.TextChanged, AddressOf txtSearchMovies_TextChanged
            Me.txtSearchMovies.Text = String.Empty
            AddHandler txtSearchMovies.TextChanged, AddressOf txtSearchMovies_TextChanged
            If Me.cbSearchMovies.Items.Count > 0 Then
                Me.cbSearchMovies.SelectedIndex = 0
            End If

            Me.chkFilterDuplicates_Movies.Checked = False
            Me.chkFilterTolerance_Movies.Checked = False
            Me.chkFilterMissing_Movies.Checked = False
            Me.chkFilterMark_Movies.Checked = False
            Me.chkFilterMarkCustom1_Movies.Checked = False
            Me.chkFilterMarkCustom2_Movies.Checked = False
            Me.chkFilterMarkCustom3_Movies.Checked = False
            Me.chkFilterMarkCustom4_Movies.Checked = False
            Me.chkFilterNew_Movies.Checked = False
            Me.chkFilterLock_Movies.Checked = False
            Me.pnlFilterMissingItems_Movies.Visible = False
            Me.rbFilterOr_Movies.Checked = False
            Me.rbFilterAnd_Movies.Checked = True
            Me.txtFilterGenre_Movies.Text = String.Empty
            For i As Integer = 0 To Me.clbFilterGenres_Movies.Items.Count - 1
                Me.clbFilterGenres_Movies.SetItemChecked(i, False)
            Next
            Me.txtFilterCountry_Movies.Text = String.Empty
            For i As Integer = 0 To Me.clbFilterCountries_Movies.Items.Count - 1
                Me.clbFilterCountries_Movies.SetItemChecked(i, False)
            Next
            Me.txtFilterDataField_Movies.Text = String.Empty
            For i As Integer = 0 To Me.clbFilterDataFields_Movies.Items.Count - 1
                Me.clbFilterDataFields_Movies.SetItemChecked(i, False)
            Next
            Me.txtFilterSource_Movies.Text = String.Empty
            For i As Integer = 0 To Me.clbFilterSources_Movies.Items.Count - 1
                Me.clbFilterSources_Movies.SetItemChecked(i, False)
            Next

            RemoveHandler cbFilterDataField_Movies.SelectedIndexChanged, AddressOf clbFilterDataFields_Movies_LostFocus
            If Me.cbFilterDataField_Movies.Items.Count > 0 Then
                Me.cbFilterDataField_Movies.SelectedIndex = 0
            End If
            AddHandler cbFilterDataField_Movies.SelectedIndexChanged, AddressOf clbFilterDataFields_Movies_LostFocus

            RemoveHandler cbFilterYearFrom_Movies.SelectedIndexChanged, AddressOf cbFilterYearFrom_Movies_SelectedIndexChanged
            If Me.cbFilterYearFrom_Movies.Items.Count > 0 Then
                Me.cbFilterYearFrom_Movies.SelectedIndex = 0
            End If
            AddHandler cbFilterYearFrom_Movies.SelectedIndexChanged, AddressOf cbFilterYearFrom_Movies_SelectedIndexChanged

            RemoveHandler cbFilterYearModFrom_Movies.SelectedIndexChanged, AddressOf cbFilterYearModFrom_Movies_SelectedIndexChanged
            If Me.cbFilterYearModFrom_Movies.Items.Count > 0 Then
                Me.cbFilterYearModFrom_Movies.SelectedIndex = 0
            End If
            AddHandler cbFilterYearModFrom_Movies.SelectedIndexChanged, AddressOf cbFilterYearModFrom_Movies_SelectedIndexChanged

            RemoveHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged
            If Me.cbFilterYearTo_Movies.Items.Count > 0 Then
                Me.cbFilterYearTo_Movies.SelectedIndex = 0
            End If
            Me.cbFilterYearTo_Movies.Enabled = False
            AddHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearTo_Movies_SelectedIndexChanged

            RemoveHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged
            If Me.cbFilterYearModTo_Movies.Items.Count > 0 Then
                Me.cbFilterYearModTo_Movies.SelectedIndex = 0
            End If
            Me.cbFilterYearModTo_Movies.Enabled = False
            AddHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf cbFilterYearModTo_Movies_SelectedIndexChanged

            RemoveHandler cbFilterVideoSource_Movies.SelectedIndexChanged, AddressOf cbFilterVideoSource_Movies_SelectedIndexChanged
            If Me.cbFilterVideoSource_Movies.Items.Count > 0 Then
                Me.cbFilterVideoSource_Movies.SelectedIndex = 0
            End If
            AddHandler cbFilterVideoSource_Movies.SelectedIndexChanged, AddressOf cbFilterVideoSource_Movies_SelectedIndexChanged

            If Reload Then Me.FillList(True, False, False)

            ModulesManager.Instance.RuntimeObjects.FilterMovies = String.Empty
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub ClearFilters_MovieSets(Optional ByVal Reload As Boolean = False)
        Try
            Me.bsMovieSets.RemoveFilter()
            Me.FilterArray_MovieSets.Clear()
            Me.filSearch_MovieSets = String.Empty
            'Me.filGenre_Moviesets = String.Empty
            'Me.filYear_Moviesets = String.Empty
            'Me.filSource_Moviesets = String.Empty

            RemoveHandler txtSearchMovieSets.TextChanged, AddressOf txtSearchMovieSets_TextChanged
            Me.txtSearchMovieSets.Text = String.Empty
            AddHandler txtSearchMovieSets.TextChanged, AddressOf txtSearchMovieSets_TextChanged
            If Me.cbSearchMovieSets.Items.Count > 0 Then
                Me.cbSearchMovieSets.SelectedIndex = 0
            End If

            'Me.chkFilterDuplicates.Checked = False
            'Me.chkFilterTolerance.Checked = False
            Me.chkFilterEmpty_MovieSets.Checked = False
            Me.chkFilterMissing_MovieSets.Checked = False
            Me.chkFilterMark_MovieSets.Checked = False
            'Me.chkFilterMarkCustom1.Checked = False
            'Me.chkFilterMarkCustom2.Checked = False
            'Me.chkFilterMarkCustom3.Checked = False
            'Me.chkFilterMarkCustom4.Checked = False
            Me.chkFilterMultiple_MovieSets.Checked = False
            Me.chkFilterNew_MovieSets.Checked = False
            Me.chkFilterLock_MovieSets.Checked = False
            Me.chkFilterOne_MovieSets.Checked = False
            Me.pnlFilterMissingItems_MovieSets.Visible = False
            Me.rbFilterOr_MovieSets.Checked = False
            Me.rbFilterAnd_MovieSets.Checked = True
            'Me.txtFilterGenre.Text = String.Empty
            'For i As Integer = 0 To Me.clbFilterGenres.Items.Count - 1
            '    Me.clbFilterGenres.SetItemChecked(i, False)
            'Next
            'Me.txtFilterCountry.Text = String.Empty
            'For i As Integer = 0 To Me.clbFilterCountries.Items.Count - 1
            '    Me.clbFilterCountries.SetItemChecked(i, False)
            'Next
            'Me.txtFilterSource.Text = String.Empty
            'For i As Integer = 0 To Me.clbFilterSource.Items.Count - 1
            '    Me.clbFilterSource.SetItemChecked(i, False)
            'Next

            'RemoveHandler cbFilterYear.SelectedIndexChanged, AddressOf cbFilterYear_SelectedIndexChanged
            'If Me.cbFilterYear.Items.Count > 0 Then
            '    Me.cbFilterYear.SelectedIndex = 0
            'End If
            'AddHandler cbFilterYear.SelectedIndexChanged, AddressOf cbFilterYear_SelectedIndexChanged

            'RemoveHandler cbFilterYearMod.SelectedIndexChanged, AddressOf cbFilterYearMod_SelectedIndexChanged
            'If Me.cbFilterYearMod.Items.Count > 0 Then
            '    Me.cbFilterYearMod.SelectedIndex = 0
            'End If
            'AddHandler cbFilterYearMod.SelectedIndexChanged, AddressOf cbFilterYearMod_SelectedIndexChanged

            'RemoveHandler cbFilterFileSource.SelectedIndexChanged, AddressOf cbFilterFileSource_SelectedIndexChanged
            'If Me.cbFilterFileSource.Items.Count > 0 Then
            '    Me.cbFilterFileSource.SelectedIndex = 0
            'End If
            'AddHandler cbFilterFileSource.SelectedIndexChanged, AddressOf cbFilterFileSource_SelectedIndexChanged

            If Reload Then Me.FillList(False, True, False)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub ClearFilters_Shows(Optional ByVal Reload As Boolean = False)
        Try
            Me.bsTVShows.RemoveFilter()
            Me.FilterArray_Shows.Clear()
            Me.filSearch_Shows = String.Empty
            Me.filGenre_Shows = String.Empty
            'Me.filYear_Shows = String.Empty
            Me.filSource_Shows = String.Empty

            RemoveHandler txtSearchShows.TextChanged, AddressOf txtSearchShows_TextChanged
            Me.txtSearchShows.Text = String.Empty
            AddHandler txtSearchShows.TextChanged, AddressOf txtSearchShows_TextChanged
            If Me.cbSearchShows.Items.Count > 0 Then
                Me.cbSearchShows.SelectedIndex = 0
            End If

            'Me.chkFilterDuplicates.Checked = False
            'Me.chkFilterTolerance.Checked = False
            Me.chkFilterMissing_Shows.Checked = False
            Me.chkFilterMark_Shows.Checked = False
            'Me.chkFilterMarkCustom1.Checked = False
            'Me.chkFilterMarkCustom2.Checked = False
            'Me.chkFilterMarkCustom3.Checked = False
            'Me.chkFilterMarkCustom4.Checked = False
            Me.chkFilterNewEpisodes_Shows.Checked = False
            Me.chkFilterNewShows_Shows.Checked = False
            Me.chkFilterLock_Shows.Checked = False
            Me.pnlFilterMissingItems_Shows.Visible = False
            Me.rbFilterOr_Shows.Checked = False
            Me.rbFilterAnd_Shows.Checked = True
            Me.txtFilterGenre_Shows.Text = String.Empty
            For i As Integer = 0 To Me.clbFilterGenres_Shows.Items.Count - 1
                Me.clbFilterGenres_Shows.SetItemChecked(i, False)
            Next
            'Me.txtFilterCountry.Text = String.Empty
            'For i As Integer = 0 To Me.clbFilterCountries.Items.Count - 1
            '    Me.clbFilterCountries.SetItemChecked(i, False)
            'Next
            Me.txtFilterSource_Shows.Text = String.Empty
            For i As Integer = 0 To Me.clbFilterSource_Shows.Items.Count - 1
                Me.clbFilterSource_Shows.SetItemChecked(i, False)
            Next

            'RemoveHandler cbFilterYear.SelectedIndexChanged, AddressOf cbFilterYear_SelectedIndexChanged
            'If Me.cbFilterYear.Items.Count > 0 Then
            '    Me.cbFilterYear.SelectedIndex = 0
            'End If
            'AddHandler cbFilterYear.SelectedIndexChanged, AddressOf cbFilterYear_SelectedIndexChanged

            'RemoveHandler cbFilterYearMod.SelectedIndexChanged, AddressOf cbFilterYearMod_SelectedIndexChanged
            'If Me.cbFilterYearMod.Items.Count > 0 Then
            '    Me.cbFilterYearMod.SelectedIndex = 0
            'End If
            'AddHandler cbFilterYearMod.SelectedIndexChanged, AddressOf cbFilterYearMod_SelectedIndexChanged

            'RemoveHandler cbFilterFileSource.SelectedIndexChanged, AddressOf cbFilterFileSource_SelectedIndexChanged
            'If Me.cbFilterFileSource.Items.Count > 0 Then
            '    Me.cbFilterFileSource.SelectedIndex = 0
            'End If
            'AddHandler cbFilterFileSource.SelectedIndexChanged, AddressOf cbFilterFileSource_SelectedIndexChanged

            If Reload Then Me.FillList(False, False, True)

            ModulesManager.Instance.RuntimeObjects.FilterShows = String.Empty
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuShowOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowOpenFolder.Click
        If Me.dgvTVShows.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If Me.dgvTVShows.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), Me.dgvTVShows.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then doOpen = False
            End If

            If doOpen Then
                For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                    Using Explorer As New Diagnostics.Process

                        If Master.isWindows Then
                            Explorer.StartInfo.FileName = "explorer.exe"
                            Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", sRow.Cells("TVShowPath").Value.ToString)
                        Else
                            Explorer.StartInfo.FileName = "xdg-open"
                            Explorer.StartInfo.Arguments = String.Format("""{0}""", sRow.Cells("TVShowPath").Value.ToString)
                        End If
                        Explorer.Start()
                    End Using
                Next
            End If
        End If
    End Sub

    Private Sub cmnuShowClearCacheDataAndImages_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuShowClearCacheDataAndImages.Click
        Dim idList As New List(Of String)
        For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
            idList.Add(sRow.Cells("TVDB").Value.ToString)
        Next
        FileUtils.Delete.Cache_Show(idList, True, True)
    End Sub

    Private Sub cmnuShowClearCacheDataOnly_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuShowClearCacheDataOnly.Click
        Dim idList As New List(Of String)
        For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
            idList.Add(sRow.Cells("TVDB").Value.ToString)
        Next
        FileUtils.Delete.Cache_Show(idList, True, False)
    End Sub

    Private Sub cmnuShowClearCacheImagesOnly_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuShowClearCacheImagesOnly.Click
        Dim idList As New List(Of String)
        For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
            idList.Add(sRow.Cells("TVDB").Value.ToString)
        Next
        FileUtils.Delete.Cache_Show(idList, False, True)
    End Sub

    Private Sub cmnuEpisodeChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeChange.Click
        Dim indX As Integer = Me.dgvTVEpisodes.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(Me.dgvTVEpisodes.Item("idEpisode", indX).Value)
        Dim ShowID As Long = Convert.ToInt64(Me.dgvTVEpisodes.Item("idShow", indX).Value)

        Me.SetControlsEnabled(False, True)

        Dim tmpEpisode As Database.DBElement = Master.DB.LoadTVEpisodeFromDB(ID, True, False)
        Dim tmpShow As Database.DBElement = Master.DB.LoadTVShowFromDB(ShowID, False, False, False)

        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.withEpisodes, True)

        If Not ModulesManager.Instance.ScrapeData_TVShow(tmpShow, ScrapeModifier, Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, True) Then
            If tmpShow.Episodes.Count > 0 Then
                Dim dlgChangeEp As New dlgTVChangeEp(tmpShow)
                If dlgChangeEp.ShowDialog = Windows.Forms.DialogResult.OK Then
                    If dlgChangeEp.Result.Count > 0 Then
                        Master.DB.ChangeTVEpisode(tmpEpisode, dlgChangeEp.Result, False)
                    End If
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(943, "There are no known episodes for this show. Scrape the show, season, or episode and try again."), Master.eLang.GetString(944, "No Known Episodes"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        RefreshRow_TVShow(ShowID, True)

        Me.SetControlsEnabled(True)
    End Sub

    Private Sub cmnuShowChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuShowChange.Click
        If Me.dgvTVShows.SelectedRows.Count = 1 Then
            Dim ScrapeModifier As New Structures.ScrapeModifier
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.DoSearch, True)
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.withEpisodes, True)
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.withSeasons, True)
            Me.CreateScrapeList_TV(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, ScrapeModifier)
        End If
    End Sub

    Private Sub cmnuSeasonRemoveFromDisk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuSeasonRemoveFromDisk.Click
        Try

            Dim SeasonsToDelete As New Dictionary(Of Long, Long)
            Dim ShowId As Long = -1
            Dim SeasonNum As Integer = -1

            For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                ShowId = Convert.ToInt64(sRow.Cells("idShow").Value)
                SeasonNum = Convert.ToInt32(sRow.Cells("Season").Value)
                'seasonnum first... showid can't be key or else only one season will be deleted
                If Not SeasonsToDelete.ContainsKey(SeasonNum) Then
                    SeasonsToDelete.Add(SeasonNum, ShowId)
                End If
            Next

            If SeasonsToDelete.Count > 0 Then
                Using dlg As New dlgDeleteConfirm
                    If dlg.ShowDialog(SeasonsToDelete, Enums.DelType.Seasons) = Windows.Forms.DialogResult.OK Then
                        Me.FillSeasons(Convert.ToInt32(Me.dgvTVSeasons.Item("idShow", Me.currRow_TVSeason).Value))
                        Me.SetTVCount()
                    End If
                End Using
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuEpisodeRemoveFromDisk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeRemoveFromDisk.Click
        Try

            Dim EpsToDelete As New Dictionary(Of Long, Long)
            Dim EpId As Long = -1

            For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                EpId = Convert.ToInt64(sRow.Cells("idEpisode").Value)
                If Not EpsToDelete.ContainsKey(EpId) Then
                    EpsToDelete.Add(EpId, 0)
                End If
            Next

            If EpsToDelete.Count > 0 Then
                Using dlg As New dlgDeleteConfirm
                    If dlg.ShowDialog(EpsToDelete, Enums.DelType.Episodes) = Windows.Forms.DialogResult.OK Then
                        Me.FillEpisodes(Convert.ToInt32(Me.dgvTVSeasons.Item("idShow", Me.currRow_TVSeason).Value), Convert.ToInt32(Me.dgvTVSeasons.Item("Season", Me.currRow_TVSeason).Value))
                        Me.SetTVCount()
                    End If
                End Using
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

    End Sub

    Private Sub cmnuShowRemoveFromDisk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowRemoveFromDisk.Click
        Try

            Dim ShowsToDelete As New Dictionary(Of Long, Long)
            Dim ShowId As Long = -1

            For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                ShowId = Convert.ToInt64(sRow.Cells("idShow").Value)
                If Not ShowsToDelete.ContainsKey(ShowId) Then
                    ShowsToDelete.Add(ShowId, 0)
                End If
            Next

            If ShowsToDelete.Count > 0 Then
                Using dlg As New dlgDeleteConfirm
                    If dlg.ShowDialog(ShowsToDelete, Enums.DelType.Shows) = Windows.Forms.DialogResult.OK Then
                        Me.FillList(False, False, True)
                    End If
                End Using
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuEpisodeEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeEdit.Click
        If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then Return

        Dim indX As Integer = Me.dgvTVEpisodes.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(Me.dgvTVEpisodes.Item("idEpisode", indX).Value)
        Dim tmpDBTVEpisode As Database.DBElement = Master.DB.LoadTVEpisodeFromDB(ID, True)
        Edit_TVEpisode(tmpDBTVEpisode)
    End Sub

    Private Sub cmnuMovieEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieEdit.Click
        If Me.dgvMovies.SelectedRows.Count > 1 Then Return

        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item("idMovie", indX).Value)
        Dim tmpDBMovie As Database.DBElement = Master.DB.LoadMovieFromDB(ID)
        Edit_Movie(tmpDBMovie)
    End Sub

    Private Sub cmnuShowEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowEdit.Click
        If Me.dgvTVShows.SelectedRows.Count > 1 Then Return

        Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(Me.dgvTVShows.Item("idShow", indX).Value)
        Dim tmpDBMTVShow As Database.DBElement = Master.DB.LoadTVShowFromDB(ID, True, False)
        Edit_TVShow(tmpDBMTVShow)
    End Sub

    Private Sub cmnuEpisodeOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeOpenFolder.Click
        If Me.dgvTVEpisodes.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            Dim ePath As String = String.Empty

            If Me.dgvTVEpisodes.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), Me.dgvTVEpisodes.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then doOpen = False
            End If

            If doOpen Then
                Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                        If Not Convert.ToInt64(sRow.Cells("idFile").Value) = -1 Then
                            SQLCommand.CommandText = String.Concat("SELECT strFilename FROM files WHERE idFile = ", sRow.Cells("idFile").Value.ToString, ";")
                            ePath = SQLCommand.ExecuteScalar.ToString

                            If Not String.IsNullOrEmpty(ePath) Then
                                Using Explorer As New Diagnostics.Process

                                    If Master.isWindows Then
                                        Explorer.StartInfo.FileName = "explorer.exe"
                                        Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", ePath)
                                    Else
                                        Explorer.StartInfo.FileName = "xdg-open"
                                        Explorer.StartInfo.Arguments = String.Format("""{0}""", ePath)
                                    End If
                                    Explorer.Start()
                                End Using
                            End If
                        End If
                    Next
                End Using
            End If
        End If
    End Sub

    Private Sub cmnuMovieWatched_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieWatched.Click
        SetWatchedStatus_Movie()
    End Sub

    Private Sub cmnuEpisodeWatched_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeWatched.Click
        SetWatchedStatus_TVEpisode()
    End Sub

    Private Sub cmnuHasWatchedSeason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonWatched.Click
        SetWatchedStatus_TVSeason()
    End Sub

    Private Sub cmnuShowWatched_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowWatched.Click
        SetWatchedStatus_TVShow()
    End Sub

    Private Sub cmnuEpisodeLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeLock.Click
        Try
            Dim setLock As Boolean = False
            If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                    'if any one item is set as unlocked, set menu to lock
                    'else they are all locked so set menu to unlock
                    If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                        setLock = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "Lock")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idShow")
                    SQLcommand.CommandText = "UPDATE tvshow SET Lock = (?) WHERE idShow = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                        parLock.Value = If(Me.dgvTVEpisodes.SelectedRows.Count > 1, setLock, Not Convert.ToBoolean(sRow.Cells("Lock").Value))
                        parID.Value = sRow.Cells("idEpisode").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Lock").Value = parLock.Value
                    Next
                End Using

                'now check the status of all episodes in the season so we can update the season lock flag if needed
                Dim LockCount As Integer = 0
                Dim NotLockCount As Integer = 0
                For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                    If Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                        LockCount += 1
                    Else
                        NotLockCount += 1
                    End If
                Next

                If LockCount = 0 OrElse NotLockCount = 0 Then
                    Using SQLSeacommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSeaLock As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeaLock", DbType.Boolean, 0, "Lock")
                        Dim parSeaID As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeaID", DbType.Int32, 0, "idShow")
                        Dim parSeason As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeason", DbType.Int32, 0, "Season")
                        SQLSeacommand.CommandText = "UPDATE seasons SET Lock = (?) WHERE idShow = (?) AND Season = (?);"
                        If LockCount = 0 Then
                            parSeaLock.Value = False
                        ElseIf NotLockCount = 0 Then
                            parSeaLock.Value = True
                        End If
                        parSeaID.Value = Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells("idShow").Value)
                        parSeason.Value = Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells("Season").Value)
                        SQLSeacommand.ExecuteNonQuery()
                        Me.dgvTVSeasons.SelectedRows(0).Cells("Lock").Value = parSeaLock.Value
                    End Using
                End If

                SQLtransaction.Commit()
            End Using

            Me.dgvTVEpisodes.Invalidate()
            Me.dgvTVSeasons.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuSeasonLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonLock.Click
        Try
            Dim setLock As Boolean = False
            If Me.dgvTVSeasons.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                    If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                        setLock = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "mark")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idShow")
                    Dim parSeason As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSeason", DbType.Int32, 0, "Season")
                    SQLcommand.CommandText = "UPDATE seasons SET Lock = (?) WHERE idShow = (?) AND Season = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                        parLock.Value = If(Me.dgvTVSeasons.SelectedRows.Count > 1, setLock, Not Convert.ToBoolean(sRow.Cells("Lock").Value))
                        parID.Value = sRow.Cells("idShow").Value
                        parSeason.Value = sRow.Cells("Season").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Lock").Value = parLock.Value

                        Using SQLECommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parELock As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parELock", DbType.Boolean, 0, "mark")
                            Dim parEID As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEID", DbType.Int32, 0, "idShow")
                            Dim parESeason As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parESeason", DbType.Int32, 0, "Season")
                            SQLECommand.CommandText = "UPDATE episode SET Lock = (?) WHERE idShow = (?) AND Season = (?);"
                            parELock.Value = parLock.Value
                            parEID.Value = parID.Value
                            parESeason.Value = parSeason.Value
                            SQLECommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                                eRow.Cells("Lock").Value = parLock.Value
                            Next
                        End Using
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            Me.dgvTVSeasons.Invalidate()
            Me.dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuShowLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowLock.Click
        Try
            Dim setLock As Boolean = False
            If Me.dgvTVShows.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                    'if any one item is set as unlocked, set menu to lock
                    'else they are all locked so set menu to unlock
                    If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                        setLock = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "lock")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idShow")
                    SQLcommand.CommandText = "UPDATE tvshow SET lock = (?) WHERE idShow = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                        parLock.Value = If(Me.dgvTVShows.SelectedRows.Count > 1, setLock, Not Convert.ToBoolean(sRow.Cells("Lock").Value))
                        parID.Value = sRow.Cells("idShow").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Lock").Value = parLock.Value

                        Using SQLSeaCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parSeaLock As SQLite.SQLiteParameter = SQLSeaCommand.Parameters.Add("parSeaLock", DbType.Boolean, 0, "lock")
                            Dim parSeaID As SQLite.SQLiteParameter = SQLSeaCommand.Parameters.Add("parSeaID", DbType.Int32, 0, "idShow")
                            SQLSeaCommand.CommandText = "UPDATE seasons SET lock = (?) WHERE idShow = (?);"
                            parSeaLock.Value = parLock.Value
                            parSeaID.Value = parID.Value
                            SQLSeaCommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In Me.dgvTVSeasons.Rows
                                eRow.Cells("Lock").Value = parLock.Value
                            Next
                        End Using

                        Using SQLECommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parELock As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parELock", DbType.Boolean, 0, "lock")
                            Dim parEID As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEID", DbType.Int32, 0, "idShow")
                            SQLECommand.CommandText = "UPDATE episode SET lock = (?) WHERE idShow = (?);"
                            parELock.Value = parLock.Value
                            parEID.Value = parID.Value
                            SQLECommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                                eRow.Cells("Lock").Value = parLock.Value
                            Next
                        End Using
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            If Me.chkFilterLock_Shows.Checked Then
                Me.dgvTVShows.ClearSelection()
                Me.dgvTVShows.CurrentCell = Nothing
                If Me.dgvTVShows.RowCount <= 0 Then
                    Me.ClearInfo()
                    Me.dgvTVSeasons.DataSource = Nothing
                    Me.dgvTVEpisodes.DataSource = Nothing
                End If
            End If

            Me.dgvTVShows.Invalidate()
            Me.dgvTVSeasons.Invalidate()
            Me.dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieLock.Click
        Try
            Dim setLock As Boolean = False
            If Me.dgvMovies.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    'if any one item is set as unlocked, set menu to lock
                    'else they are all locked so set menu to unlock
                    If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                        setLock = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "lock")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idMovie")
                    SQLcommand.CommandText = "UPDATE movie SET lock = (?) WHERE idMovie = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                        parLock.Value = If(Me.dgvMovies.SelectedRows.Count > 1, setLock, Not Convert.ToBoolean(sRow.Cells("Lock").Value))
                        parID.Value = sRow.Cells("idMovie").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Lock").Value = parLock.Value
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            If Me.chkFilterLock_Movies.Checked Then
                Me.dgvMovies.ClearSelection()
                Me.dgvMovies.CurrentCell = Nothing
                If Me.dgvMovies.RowCount <= 0 Then Me.ClearInfo()
            End If

            Me.dgvMovies.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieSetLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetLock.Click
        Try
            Dim setLock As Boolean = False
            If Me.dgvMovieSets.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvMovieSets.SelectedRows
                    'if any one item is set as unlocked, set menu to lock
                    'else they are all locked so set menu to unlock
                    If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                        setLock = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parLock As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parLock", DbType.Boolean, 0, "lock")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idSet")
                    SQLcommand.CommandText = "UPDATE sets SET Lock = (?) WHERE idSet = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvMovieSets.SelectedRows
                        parLock.Value = If(Me.dgvMovieSets.SelectedRows.Count > 1, setLock, Not Convert.ToBoolean(sRow.Cells("Lock").Value))
                        parID.Value = sRow.Cells("idSet").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Lock").Value = parLock.Value
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            If Me.chkFilterLock_MovieSets.Checked Then
                Me.dgvMovieSets.ClearSelection()
                Me.dgvMovieSets.CurrentCell = Nothing
                If Me.dgvMovieSets.RowCount <= 0 Then Me.ClearInfo()
            End If

            Me.dgvMovieSets.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuEpisodeMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeMark.Click
        Try
            Dim setMark As Boolean = False
            If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                    'if any one item is set as unmarked, set menu to mark
                    'else they are all marked, so set menu to unmark
                    If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                        setMark = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idEpisode")
                    SQLcommand.CommandText = "UPDATE episode SET mark = (?) WHERE idEpisode = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                        parMark.Value = If(Me.dgvTVEpisodes.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("Mark").Value))
                        parID.Value = sRow.Cells("idEpisode").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Mark").Value = parMark.Value
                    Next
                End Using

                'now check the status of all episodes in the season so we can update the season mark flag if needed
                Dim MarkCount As Integer = 0
                Dim NotMarkCount As Integer = 0
                For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                    If Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                        MarkCount += 1
                    Else
                        NotMarkCount += 1
                    End If
                Next

                If MarkCount = 0 OrElse NotMarkCount = 0 Then
                    Using SQLSeacommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSeaMark As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeaMark", DbType.Boolean, 0, "Mark")
                        Dim parSeaID As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeaID", DbType.Int32, 0, "idShow")
                        Dim parSeason As SQLite.SQLiteParameter = SQLSeacommand.Parameters.Add("parSeason", DbType.Int32, 0, "Season")
                        SQLSeacommand.CommandText = "UPDATE seasons SET Mark = (?) WHERE idShow = (?) AND Season = (?);"
                        If MarkCount = 0 Then
                            parSeaMark.Value = False
                        ElseIf NotMarkCount = 0 Then
                            parSeaMark.Value = True
                        End If
                        parSeaID.Value = Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells("idShow").Value)
                        parSeason.Value = Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells("Season").Value)
                        SQLSeacommand.ExecuteNonQuery()
                        Me.dgvTVSeasons.SelectedRows(0).Cells("Mark").Value = parSeaMark.Value
                    End Using
                End If

                SQLtransaction.Commit()
            End Using

            Me.dgvTVSeasons.Invalidate()
            Me.dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuSeasonMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonMark.Click
        Try
            Dim setMark As Boolean = False
            If Me.dgvTVSeasons.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                    If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                        setMark = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idShow")
                    Dim parSeason As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSeason", DbType.Int32, 0, "Season")
                    SQLcommand.CommandText = "UPDATE seasons SET mark = (?) WHERE idShow = (?) AND Season = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                        parMark.Value = If(Me.dgvTVSeasons.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("Mark").Value))
                        parID.Value = sRow.Cells("idShow").Value
                        parSeason.Value = sRow.Cells("Season").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Mark").Value = parMark.Value

                        Using SQLECommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parEMark As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEMark", DbType.Boolean, 0, "mark")
                            Dim parEID As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEID", DbType.Int32, 0, "idShow")
                            Dim parESeason As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parESeason", DbType.Int32, 0, "Season")
                            SQLECommand.CommandText = "UPDATE episode SET mark = (?) WHERE idShow = (?) AND Season = (?);"
                            parEMark.Value = parMark.Value
                            parEID.Value = parID.Value
                            parESeason.Value = parSeason.Value
                            SQLECommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                                eRow.Cells("Mark").Value = parMark.Value
                            Next
                        End Using
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            Me.dgvTVSeasons.Invalidate()
            Me.dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuShowMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowMark.Click
        Try
            Dim setMark As Boolean = False
            If Me.dgvTVShows.SelectedRows.Count > 1 Then
                For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                    'if any one item is set as unmarked, set menu to mark
                    'else they are all marked, so set menu to unmark
                    If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                        setMark = True
                        Exit For
                    End If
                Next
            End If

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "mark")
                    Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idShow")
                    SQLcommand.CommandText = "UPDATE tvshow SET mark = (?) WHERE idShow = (?);"
                    For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                        parMark.Value = If(Me.dgvTVShows.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("Mark").Value))
                        parID.Value = sRow.Cells("idShow").Value
                        SQLcommand.ExecuteNonQuery()
                        sRow.Cells("Mark").Value = parMark.Value

                        Using SQLSeaCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parSeaMark As SQLite.SQLiteParameter = SQLSeaCommand.Parameters.Add("parSeaMark", DbType.Boolean, 0, "mark")
                            Dim parSeaID As SQLite.SQLiteParameter = SQLSeaCommand.Parameters.Add("parSeaID", DbType.Int32, 0, "idShow")
                            SQLSeaCommand.CommandText = "UPDATE seasons SET mark = (?) WHERE idShow = (?);"
                            parSeaMark.Value = parMark.Value
                            parSeaID.Value = parID.Value
                            SQLSeaCommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In Me.dgvTVSeasons.Rows
                                eRow.Cells("Mark").Value = parMark.Value
                            Next
                        End Using

                        Using SQLECommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parEMark As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEMark", DbType.Boolean, 0, "mark")
                            Dim parEID As SQLite.SQLiteParameter = SQLECommand.Parameters.Add("parEID", DbType.Int32, 0, "idShow")
                            SQLECommand.CommandText = "UPDATE episode SET mark = (?) WHERE idShow = (?);"
                            parEMark.Value = parMark.Value
                            parEID.Value = parID.Value
                            SQLECommand.ExecuteNonQuery()

                            For Each eRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                                eRow.Cells("Mark").Value = parMark.Value
                            Next
                        End Using
                    Next
                End Using
                SQLtransaction.Commit()
            End Using

            If Me.chkFilterMark_Shows.Checked Then
                Me.dgvTVShows.ClearSelection()
                Me.dgvTVShows.CurrentCell = Nothing
                If Me.dgvTVShows.RowCount <= 0 Then
                    Me.ClearInfo()
                    Me.dgvTVSeasons.DataSource = Nothing
                    Me.dgvTVEpisodes.DataSource = Nothing
                End If
            End If

            Me.dgvTVShows.Invalidate()
            Me.dgvTVSeasons.Invalidate()
            Me.dgvTVEpisodes.Invalidate()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMark.Click
        Dim setMark As Boolean = False
        If Me.dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                'if any one item is set as unmarked, set menu to mark
                'else they are all marked, so set menu to unmark
                If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                    setMark = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "Mark")
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idMovie")
                SQLcommand.CommandText = "UPDATE movie SET Mark = (?) WHERE idMovie = (?);"
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    parMark.Value = If(Me.dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("Mark").Value))
                    parID.Value = sRow.Cells("idMovie").Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("Mark").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        setMark = False
        For Each sRow As DataGridViewRow In Me.dgvMovies.Rows
            If Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                setMark = True
                Exit For
            End If
        Next
        Me.btnMarkAll.Text = If(setMark, Master.eLang.GetString(105, "Unmark All"), Master.eLang.GetString(35, "Mark All"))

        If Me.chkFilterMark_Movies.Checked Then
            Me.dgvMovies.ClearSelection()
            Me.dgvMovies.CurrentCell = Nothing
            If Me.dgvMovies.RowCount <= 0 Then Me.ClearInfo()
        End If

        Me.dgvMovies.Invalidate()
    End Sub

    Private Sub cmnuMovieSetMark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetMark.Click
        Dim setMark As Boolean = False
        If Me.dgvMovieSets.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In Me.dgvMovieSets.SelectedRows
                'if any one item is set as unmarked, set menu to mark
                'else they are all marked, so set menu to unmark
                If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                    setMark = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMark", DbType.Boolean, 0, "Mark")
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idSet")
                SQLcommand.CommandText = "UPDATE sets SET Mark = (?) WHERE idSet = (?);"
                For Each sRow As DataGridViewRow In Me.dgvMovieSets.SelectedRows
                    parMark.Value = If(Me.dgvMovieSets.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("Mark").Value))
                    parID.Value = sRow.Cells("idSet").Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("Mark").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        setMark = False
        For Each sRow As DataGridViewRow In Me.dgvMovieSets.Rows
            If Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                setMark = True
                Exit For
            End If
        Next
        'Me.btnMarkAll.Text = If(setMark, Master.eLang.GetString(105, "Unmark All"), Master.eLang.GetString(35, "Mark All"))

        If Me.chkFilterMark_MovieSets.Checked Then
            Me.dgvMovieSets.ClearSelection()
            Me.dgvMovieSets.CurrentCell = Nothing
            If Me.dgvMovieSets.RowCount <= 0 Then Me.ClearInfo()
        End If

        Me.dgvMovieSets.Invalidate()
    End Sub

    Private Sub cmnuMovieMarkAsCustom1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMarkAsCustom1.Click
        Dim setMark As Boolean = False
        If Me.dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                'if any one item is set as unmarked, set menu to mark
                'else they are all marked, so set menu to unmark
                If Not Convert.ToBoolean(sRow.Cells("MarkCustom1").Value) Then
                    setMark = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom1", DbType.Boolean, 0, "MarkCustom1")
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idMovie")
                SQLcommand.CommandText = "UPDATE movie SET MarkCustom1 = (?) WHERE idMovie = (?);"
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    parMark.Value = If(Me.dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("MarkCustom1").Value))
                    parID.Value = sRow.Cells("idMovie").Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("MarkCustom1").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        'setMark = False
        'For Each sRow As DataGridViewRow In Me.dgvMovies.Rows
        '    If Convert.ToBoolean(sRow.Cells(66).Value) Then
        '        setMark = True
        '        Exit For
        '    End If
        'Next
        'Me.btnMarkAll.Text = If(setMark, Master.eLang.GetString(105, "Unmark All"), Master.eLang.GetString(35, "Mark All"))

        If Me.chkFilterMarkCustom1_Movies.Checked Then
            Me.dgvMovies.ClearSelection()
            Me.dgvMovies.CurrentCell = Nothing
            If Me.dgvMovies.RowCount <= 0 Then Me.ClearInfo()
        End If

        Me.dgvMovies.Invalidate()
    End Sub

    Private Sub cmnuMovieMarkAsCustom2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMarkAsCustom2.Click
        Dim setMark As Boolean = False
        If Me.dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                'if any one item is set as unmarked, set menu to mark
                'else they are all marked, so set menu to unmark
                If Not Convert.ToBoolean(sRow.Cells("MarkCustom2").Value) Then
                    setMark = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom2", DbType.Boolean, 0, "MarkCustom2")
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idMovie")
                SQLcommand.CommandText = "UPDATE movie SET MarkCustom2 = (?) WHERE idMovie = (?);"
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    parMark.Value = If(Me.dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("MarkCustom2").Value))
                    parID.Value = sRow.Cells("idMovie").Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("MarkCustom2").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        'setMark = False
        'For Each sRow As DataGridViewRow In Me.dgvMovies.Rows
        '    If Convert.ToBoolean(sRow.Cells(66).Value) Then
        '        setMark = True
        '        Exit For
        '    End If
        'Next
        'Me.btnMarkAll.Text = If(setMark, Master.eLang.GetString(105, "Unmark All"), Master.eLang.GetString(35, "Mark All"))

        If Me.chkFilterMarkCustom2_Movies.Checked Then
            Me.dgvMovies.ClearSelection()
            Me.dgvMovies.CurrentCell = Nothing
            If Me.dgvMovies.RowCount <= 0 Then Me.ClearInfo()
        End If

        Me.dgvMovies.Invalidate()
    End Sub

    Private Sub cmnuMovieMarkAsCustom3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMarkAsCustom3.Click
        Dim setMark As Boolean = False
        If Me.dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                'if any one item is set as unmarked, set menu to mark
                'else they are all marked, so set menu to unmark
                If Not Convert.ToBoolean(sRow.Cells("MarkCustom3").Value) Then
                    setMark = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom3", DbType.Boolean, 0, "MarkCustom3")
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idMovie")
                SQLcommand.CommandText = "UPDATE movie SET MarkCustom3 = (?) WHERE idMovie = (?);"
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    parMark.Value = If(Me.dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("MarkCustom3").Value))
                    parID.Value = sRow.Cells("idMovie").Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("MarkCustom3").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        'setMark = False
        'For Each sRow As DataGridViewRow In Me.dgvMovies.Rows
        '    If Convert.ToBoolean(sRow.Cells(66).Value) Then
        '        setMark = True
        '        Exit For
        '    End If
        'Next
        'Me.btnMarkAll.Text = If(setMark, Master.eLang.GetString(105, "Unmark All"), Master.eLang.GetString(35, "Mark All"))

        If Me.chkFilterMarkCustom3_Movies.Checked Then
            Me.dgvMovies.ClearSelection()
            Me.dgvMovies.CurrentCell = Nothing
            If Me.dgvMovies.RowCount <= 0 Then Me.ClearInfo()
        End If

        Me.dgvMovies.Invalidate()
    End Sub

    Private Sub cmnuMovieMarkAsCustom4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieMarkAsCustom4.Click
        Dim setMark As Boolean = False
        If Me.dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                'if any one item is set as unmarked, set menu to mark
                'else they are all marked, so set menu to unmark
                If Not Convert.ToBoolean(sRow.Cells("MarkCustom4").Value) Then
                    setMark = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim parMark As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parMarkCustom4", DbType.Boolean, 0, "MarkCustom4")
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int32, 0, "idMovie")
                SQLcommand.CommandText = "UPDATE movie SET MarkCustom4 = (?) WHERE idMovie = (?);"
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    parMark.Value = If(Me.dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("MarkCustom4").Value))
                    parID.Value = sRow.Cells("idMovie").Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("MarkCustom4").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        'setMark = False
        'For Each sRow As DataGridViewRow In Me.dgvMovies.Rows
        '    If Convert.ToBoolean(sRow.Cells(66).Value) Then
        '        setMark = True
        '        Exit For
        '    End If
        'Next
        'Me.btnMarkAll.Text = If(setMark, Master.eLang.GetString(105, "Unmark All"), Master.eLang.GetString(35, "Mark All"))

        If Me.chkFilterMarkCustom4_Movies.Checked Then
            Me.dgvMovies.ClearSelection()
            Me.dgvMovies.CurrentCell = Nothing
            If Me.dgvMovies.RowCount <= 0 Then Me.ClearInfo()
        End If

        Me.dgvMovies.Invalidate()
    End Sub

    Private Sub cmnuMovieEditMetaData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieEditMetaData.Click
        If Me.dgvMovies.SelectedRows.Count > 1 Then Return
        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item("idMovie", indX).Value)
        Dim DBElement As Database.DBElement = Master.DB.LoadMovieFromDB(ID, False)
        Using dEditMeta As New dlgFileInfo(DBElement, False)
            Select Case dEditMeta.ShowDialog()
                Case Windows.Forms.DialogResult.OK
                    Me.RefreshRow_Movie(ID)
            End Select
        End Using
    End Sub

    Private Sub cmnuMovieReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieReload.Click
        Me.dgvMovies.Cursor = Cursors.WaitCursor
        Me.SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        Dim showMessages As Boolean = Me.dgvMovies.SelectedRows.Count = 1

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                If Me.Reload_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value), True, showMessages) Then
                    doFill = True
                Else
                    RefreshRow_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
                End If
            Next
            SQLtransaction.Commit()
        End Using

        Me.dgvMovies.Cursor = Cursors.Default
        Me.SetControlsEnabled(True)

        If doFill Then
            FillList(True, True, False)
        Else
            DoTitleCheck()
        End If
    End Sub

    Private Sub cmnuMovieSetEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetEdit.Click
        If Me.dgvMovieSets.SelectedRows.Count > 1 Then Return

        Dim indX As Integer = Me.dgvMovieSets.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(Me.dgvMovieSets.Item("idSet", indX).Value)
        Dim tmpDBMovieSet As Database.DBElement = Master.DB.LoadMovieSetFromDB(ID)
        Edit_MovieSet(tmpDBMovieSet)
    End Sub

    Private Sub cmnuMovieSetNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetNew.Click
        Me.dgvMovieSets.ClearSelection()

        Dim tmpDBMovieSet = New Database.DBElement
        tmpDBMovieSet.MovieSet = New MediaContainers.MovieSet
        tmpDBMovieSet.ID = -1

        Edit_MovieSet(tmpDBMovieSet)
    End Sub

    Private Sub cmnuMovieSetReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetReload.Click
        Me.dgvMovieSets.Cursor = Cursors.WaitCursor
        Me.SetControlsEnabled(False, True)

        Dim doFill As Boolean = False
        Dim tFill As Boolean = False

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvMovieSets.SelectedRows
                tFill = Me.Reload_MovieSet(Convert.ToInt64(sRow.Cells("idSet").Value), True)
                If tFill Then doFill = True
            Next
            SQLtransaction.Commit()
        End Using

        Me.dgvMovieSets.Cursor = Cursors.Default
        Me.SetControlsEnabled(True)

        If doFill Then FillList(False, True, False)
    End Sub

    Private Sub cmnuMovieSetRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetRemove.Click
        Me.ClearInfo()
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()

            For Each sRow As DataGridViewRow In Me.dgvMovieSets.SelectedRows
                Master.DB.DeleteMovieSetFromDB(Convert.ToInt64(sRow.Cells("idSet").Value), True)
            Next

            SQLtransaction.Commit()
        End Using

        Me.FillList(True, True, False)
    End Sub

    Private Sub cmnuEpisodeReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeReload.Click
        Me.dgvTVShows.Cursor = Cursors.WaitCursor
        Me.dgvTVSeasons.Cursor = Cursors.WaitCursor
        Me.dgvTVEpisodes.Cursor = Cursors.WaitCursor
        Me.SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If Me.dgvTVEpisodes.SelectedRows.Count > 0 Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                    If Not Convert.ToInt64(sRow.Cells("idFile").Value) = -1 Then 'skipping missing episodes
                        If Me.Reload_TVEpisode(Convert.ToInt64(sRow.Cells("idEpisode").Value), True, Me.dgvTVEpisodes.SelectedRows.Count = 1) Then
                            doFill = True
                        Else
                            RefreshRow_TVEpisode(Convert.ToInt64(sRow.Cells("idEpisode").Value))
                        End If
                    End If
                Next
                SQLtransaction.Commit()
            End Using
        End If

        Me.dgvTVShows.Cursor = Cursors.Default
        Me.dgvTVSeasons.Cursor = Cursors.Default
        Me.dgvTVEpisodes.Cursor = Cursors.Default
        Me.SetControlsEnabled(True)

        If doFill Then FillEpisodes(Convert.ToInt32(Me.dgvTVEpisodes.SelectedRows(0).Cells("idEpisode").Value), Convert.ToInt32(Me.dgvTVEpisodes.SelectedRows(0).Cells("Season").Value))
    End Sub

    Private Sub cmnuSeasonReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonReload.Click
        Me.dgvTVShows.Cursor = Cursors.WaitCursor
        Me.dgvTVSeasons.Cursor = Cursors.WaitCursor
        Me.dgvTVEpisodes.Cursor = Cursors.WaitCursor
        Me.SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If Me.dgvTVSeasons.SelectedRows.Count > 0 Then
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.tspbLoading.Value = 0
            Me.tspbLoading.Maximum = Me.dgvTVSeasons.SelectedRows.Count

            Me.tslLoading.Text = String.Concat(Master.eLang.GetString(563, "Reloading Season"), ":")
            Me.tslLoading.Visible = True
            Me.tspbLoading.Visible = True
            Application.DoEvents()

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                    Me.tspbLoading.Value += 1
                    If Me.Reload_TVSeason(Convert.ToInt64(sRow.Cells("idSeason").Value), True, Me.dgvTVSeasons.SelectedRows.Count = 1, False) Then
                        doFill = True
                    Else
                        RefreshRow_TVSeason(Convert.ToInt64(sRow.Cells("idSeason").Value))
                    End If
                Next
                SQLtransaction.Commit()
            End Using

            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
        End If

        Me.dgvTVShows.Cursor = Cursors.Default
        Me.dgvTVSeasons.Cursor = Cursors.Default
        Me.dgvTVEpisodes.Cursor = Cursors.Default
        Me.SetControlsEnabled(True)

        If doFill Then Me.FillSeasons(Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells("idShow").Value))
    End Sub

    Private Sub cmnuSeasonReloadFull_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonReloadFull.Click
        Me.dgvTVShows.Cursor = Cursors.WaitCursor
        Me.dgvTVSeasons.Cursor = Cursors.WaitCursor
        Me.dgvTVEpisodes.Cursor = Cursors.WaitCursor
        Me.SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If Me.dgvTVSeasons.SelectedRows.Count > 0 Then
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.tspbLoading.Value = 0
            Me.tspbLoading.Maximum = Me.dgvTVSeasons.SelectedRows.Count

            Me.tslLoading.Text = String.Concat(Master.eLang.GetString(563, "Reloading Season"), ":")
            Me.tslLoading.Visible = True
            Me.tspbLoading.Visible = True
            Application.DoEvents()

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                    Me.tspbLoading.Value += 1
                    If Me.Reload_TVSeason(Convert.ToInt64(sRow.Cells("idSeason").Value), True, Me.dgvTVSeasons.SelectedRows.Count = 1, False) Then
                        doFill = True
                    Else
                        RefreshRow_TVSeason(Convert.ToInt64(sRow.Cells("idSeason").Value))
                    End If
                Next
                SQLtransaction.Commit()
            End Using

            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
        End If

        Me.dgvTVShows.Cursor = Cursors.Default
        Me.dgvTVSeasons.Cursor = Cursors.Default
        Me.dgvTVEpisodes.Cursor = Cursors.Default
        Me.SetControlsEnabled(True)

        If doFill Then Me.FillSeasons(Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells("idShow").Value))
    End Sub

    Private Sub cmnuShowReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowReload.Click
        Me.dgvTVShows.Cursor = Cursors.WaitCursor
        Me.dgvTVSeasons.Cursor = Cursors.WaitCursor
        Me.dgvTVEpisodes.Cursor = Cursors.WaitCursor
        Me.SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If Me.dgvTVShows.SelectedRows.Count > 1 Then
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.tspbLoading.Value = 0
            Me.tspbLoading.Maximum = Me.dgvTVShows.SelectedRows.Count

            Me.tslLoading.Text = String.Concat(Master.eLang.GetString(562, "Reloading Show"), ":")
            Me.tslLoading.Visible = True
            Me.tspbLoading.Visible = True
            Application.DoEvents()

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                    Me.tspbLoading.Value += 1
                    If Me.Reload_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), True, Me.dgvTVShows.SelectedRows.Count = 1, False) Then
                        doFill = True
                    Else
                        RefreshRow_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value))
                    End If
                Next
                SQLtransaction.Commit()
            End Using

            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
        ElseIf Me.dgvTVShows.SelectedRows.Count = 1 Then
            If Me.Reload_TVShow(Convert.ToInt64(Me.dgvTVShows.SelectedRows(0).Cells("idShow").Value), False, True, False) Then
                doFill = True
            Else
                RefreshRow_TVShow(Convert.ToInt64(Me.dgvTVShows.SelectedRows(0).Cells("idShow").Value))
            End If
        End If

        Me.dgvTVShows.Cursor = Cursors.Default
        Me.dgvTVSeasons.Cursor = Cursors.Default
        Me.dgvTVEpisodes.Cursor = Cursors.Default
        Me.SetControlsEnabled(True)

        If doFill Then FillList(False, False, True)
    End Sub

    Private Sub cmnuShowReloadFull_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowReloadFull.Click
        Me.dgvTVShows.Cursor = Cursors.WaitCursor
        Me.dgvTVSeasons.Cursor = Cursors.WaitCursor
        Me.dgvTVEpisodes.Cursor = Cursors.WaitCursor
        Me.SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If Me.dgvTVShows.SelectedRows.Count > 1 Then
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.tspbLoading.Value = 0
            Me.tspbLoading.Maximum = Me.dgvTVShows.SelectedRows.Count

            Me.tslLoading.Text = String.Concat(Master.eLang.GetString(562, "Reloading Show"), ":")
            Me.tslLoading.Visible = True
            Me.tspbLoading.Visible = True
            Application.DoEvents()

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                    Me.tspbLoading.Value += 1
                    If Me.Reload_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), True, Me.dgvTVShows.SelectedRows.Count = 1, True) Then
                        doFill = True
                    Else
                        RefreshRow_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value))
                    End If
                Next
                SQLtransaction.Commit()
            End Using

            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
        ElseIf Me.dgvTVShows.SelectedRows.Count = 1 Then
            If Me.Reload_TVShow(Convert.ToInt64(Me.dgvTVShows.SelectedRows(0).Cells("idShow").Value), False, True, True) Then
                doFill = True
            Else
                RefreshRow_TVShow(Convert.ToInt64(Me.dgvTVShows.SelectedRows(0).Cells("idShow").Value))
            End If
        End If

        Me.dgvTVShows.Cursor = Cursors.Default
        Me.dgvTVSeasons.Cursor = Cursors.Default
        Me.dgvTVEpisodes.Cursor = Cursors.Default
        Me.SetControlsEnabled(True)

        If doFill Then FillList(False, False, True)
    End Sub

    Private Sub cmnuSeasonRemoveFromDB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuSeasonRemoveFromDB.Click
        Me.ClearInfo()

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Dim idShow As Integer = CInt(Me.dgvTVSeasons.SelectedRows(0).Cells("idShow").Value)
            For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                If Not CInt(sRow.Cells("Season").Value) = 999 Then
                    Master.DB.DeleteTVSeasonFromDB(Convert.ToInt32(sRow.Cells("idSeason").Value), True)
                End If
            Next
            Me.Reload_TVShow(idShow, True, True, False)
            SQLtransaction.Commit()
        End Using

        If Me.dgvTVSeasons.RowCount > 0 Then
            Me.FillSeasons(Convert.ToInt32(Me.dgvTVSeasons.SelectedRows(0).Cells("idShow").Value))
        End If

        Me.SetTVCount()
    End Sub

    Private Sub cmnuEpisodeRemoveFromDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeRemoveFromDB.Click
        Dim SeasonsList As New List(Of Integer)
        Me.ClearInfo()

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Dim idShow As Integer = CInt(Me.dgvTVEpisodes.SelectedRows(0).Cells("idShow").Value)
            For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                If Not SeasonsList.Contains(CInt(sRow.Cells("Season").Value)) Then SeasonsList.Add(CInt(sRow.Cells("Season").Value))
                If Not Convert.ToInt64(sRow.Cells("idFile").Value) = -1 Then
                    Master.DB.DeleteTVEpFromDB(Convert.ToInt32(sRow.Cells("idEpisode").Value), False, False, True) 'set the episode as "missing episode"
                Else
                    Master.DB.DeleteTVEpFromDB(Convert.ToInt32(sRow.Cells("idEpisode").Value), True, False, True) 'remove the "missing episode" from DB
                End If
            Next

            'Master.DB.CleanSeasons(True)

            For Each iSeason In SeasonsList
                Me.RefreshRow_TVSeason(idShow, iSeason)
            Next
            Me.RefreshRow_TVShow(idShow)

            SQLtransaction.Commit()
        End Using

        Dim cSeas As Integer = 0

        If Not Me.currRow_TVSeason = -1 Then
            cSeas = Me.currRow_TVSeason
        End If

        If Me.dgvTVEpisodes.RowCount > 0 Then
            Me.FillEpisodes(Convert.ToInt32(Me.dgvTVSeasons.Item("idShow", cSeas).Value), Convert.ToInt32(Me.dgvTVSeasons.Item("Season", cSeas).Value))
        End If

        Me.SetTVCount()
    End Sub

    Private Sub cmnuShowRemoveFromDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowRemoveFromDB.Click
        Me.ClearInfo()

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                Master.DB.DeleteTVShowFromDB(Convert.ToInt32(sRow.Cells("idShow").Value), True)
            Next
            SQLtransaction.Commit()
        End Using

        Me.FillList(False, False, True)
    End Sub

    Private Sub cmnuEpisodeRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuEpisodeRescrape.Click
        If Me.dgvTVEpisodes.SelectedRows.Count = 1 Then
            Dim ScrapeModifier As New Structures.ScrapeModifier
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
            Me.CreateScrapeList_TVEpisode(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, ScrapeModifier)
        End If
    End Sub

    Private Sub cmnuShowRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowRefresh.Click
        Me.SetControlsEnabled(False, True)
        'RefreshData_TVShow()
    End Sub

    Private Sub cmnuMovieRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieRescrape.Click
        If Me.dgvMovies.SelectedRows.Count = 1 Then
            Dim ScrapeModifier As New Structures.ScrapeModifier
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
            Me.CreateScrapeList_Movie(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_Movie, ScrapeModifier)
        End If
    End Sub

    Private Sub cmnuMovieSetRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetRescrape.Click
        If Me.dgvMovieSets.SelectedRows.Count = 1 Then
            Dim ScrapeModifier As New Structures.ScrapeModifier
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
            Me.CreateScrapeList_MovieSet(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_MovieSet, ScrapeModifier)
        End If
    End Sub

    Private Sub cmnuShowRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowRescrape.Click
        If Me.dgvTVShows.SelectedRows.Count > 0 Then
            Dim ScrapeModifier As New Structures.ScrapeModifier
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.withEpisodes, True)
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.withSeasons, True)
            Me.CreateScrapeList_TV(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, ScrapeModifier)
        End If
    End Sub
    ''' <summary>
    ''' User has selected "Change Movie" from the context menu. This will re-validate the movie title with the user,
    ''' and initiate a new manually scrape of the movie.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmnuMovieChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieChange.Click
        If Me.dgvMovies.SelectedRows.Count <> 1 Then Return 'This method is only valid for when exactly one movie is selected
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.DoSearch, True)
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
        Me.CreateScrapeList_Movie(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_Movie, ScrapeModifier)
    End Sub
    ''' <summary>
    ''' User has selected "Change Movie" from the context menu. This will re-validate the movie title with the user,
    ''' and initiate a new auto scrape of the movie.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmnuMovieChangeAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieChangeAuto.Click
        If Me.dgvMovies.SelectedRows.Count <> 1 Then Return 'This method is only valid for when exactly one movie is selected
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.DoSearch, True)
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
        Me.CreateScrapeList_Movie(Enums.ScrapeType.SingleAuto, Master.DefaultOptions_Movie, ScrapeModifier)
    End Sub

    Private Sub cmnuSeasonEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuSeasonEdit.Click
        Dim indX As Integer = Me.dgvTVSeasons.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item("idSeason", indX).Value)
        Dim tmpDBTVSeason As Database.DBElement = Master.DB.LoadTVSeasonFromDB(ID, True)
        Edit_TVSeason(tmpDBTVSeason)
    End Sub

    Private Sub cmnuSeasonOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuSeasonOpenFolder.Click
        If Me.dgvTVSeasons.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            Dim SeasonPath As String = String.Empty

            If Me.dgvTVSeasons.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), Me.dgvTVSeasons.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then doOpen = False
            End If

            If doOpen Then
                For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                    SeasonPath = Functions.GetSeasonDirectoryFromShowPath(Me.currTV.ShowPath, Convert.ToInt32(sRow.Cells("Season").Value))

                    Using Explorer As New Diagnostics.Process
                        If Master.isWindows Then
                            Explorer.StartInfo.FileName = "explorer.exe"
                            If String.IsNullOrEmpty(SeasonPath) Then
                                Explorer.StartInfo.Arguments = String.Format("/root,""{0}""", Me.currTV.ShowPath)
                            Else
                                Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", SeasonPath)
                            End If

                        Else
                            Explorer.StartInfo.FileName = "xdg-open"
                            If String.IsNullOrEmpty(SeasonPath) Then
                                Explorer.StartInfo.Arguments = String.Format("""{0}""", Me.currTV.ShowPath)
                            Else
                                Explorer.StartInfo.Arguments = String.Format("""{0}""", SeasonPath)
                            End If
                        End If
                        Explorer.Start()
                    End Using
                Next
            End If
        End If
    End Sub

    Private Sub cmnuSeasonRescrape_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuSeasonRescrape.Click
        If Me.dgvTVSeasons.SelectedRows.Count > 0 Then
            Dim ScrapeModifier As New Structures.ScrapeModifier
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
            Me.CreateScrapeList_TVSeason(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, ScrapeModifier)
        End If
    End Sub

    Private Sub mnuMainToolsSortFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsSortFiles.Click, cmnuTrayToolsSortFiles.Click
        Me.SetControlsEnabled(False)
        Using dSortFiles As New dlgSortFiles
            If dSortFiles.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.LoadMedia(New Structures.Scans With {.Movies = True})
            Else
                Me.SetControlsEnabled(True)
            End If
        End Using
    End Sub

    Private Sub mnuMainToolsBackdrops_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsBackdrops.Click, cmnuTrayToolsBackdrops.Click
        Me.NonScrape(Enums.ScrapeType.CopyBackdrops, Nothing)
    End Sub
    ''' <summary>
    ''' Populate the form's Genre panel and picture box arrays with the 
    ''' appropriate genre images and (conditionally) labels 
    ''' </summary>
    ''' <param name="genres"><c>List (Of String)</c> holding genre names</param>
    ''' <remarks>If any individual genre is invalid or generates an error, 
    ''' the remaining genres are still processed, however the placement/spacing
    ''' of the remaining genres may show gaps where the erronious genres should have been</remarks>
    Private Sub createGenreThumbs(ByVal genres As List(Of String))
        If ((genres Is Nothing) OrElse (genres.Count = 0)) Then Return

        For i As Integer = 0 To genres.Count - 1
            Try
                ReDim Preserve Me.pnlGenre(i)
                ReDim Preserve Me.pbGenre(i)
                Me.pnlGenre(i) = New Panel()
                Me.pnlGenre(i).Visible = False
                Me.pbGenre(i) = New PictureBox()
                Me.pbGenre(i).Name = genres(i).Trim.ToUpper
                Me.pnlGenre(i).Size = New Size(68, 100)
                Me.pbGenre(i).Size = New Size(62, 94)
                Me.pnlGenre(i).BackColor = Me.GenrePanelColor
                Me.pbGenre(i).BackColor = Me.GenrePanelColor
                Me.pnlGenre(i).BorderStyle = BorderStyle.FixedSingle
                Me.pbGenre(i).SizeMode = PictureBoxSizeMode.StretchImage
                Me.pbGenre(i).Image = APIXML.GetGenreImage(genres(i).Trim)
                Me.pnlGenre(i).Left = ((Me.pnlInfoPanel.Right) - (i * 73)) - 73
                Me.pbGenre(i).Left = 2
                Me.pnlGenre(i).Top = Me.pnlInfoPanel.Top - 105
                Me.pbGenre(i).Top = 2
                Me.scMain.Panel2.Controls.Add(Me.pnlGenre(i))
                Me.pnlGenre(i).Controls.Add(Me.pbGenre(i))
                Me.pnlGenre(i).BringToFront()
                AddHandler Me.pbGenre(i).MouseEnter, AddressOf pbGenre_MouseEnter
                AddHandler Me.pbGenre(i).MouseLeave, AddressOf pbGenre_MouseLeave
                If Master.eSettings.GeneralShowGenresText Then
                    pbGenre(i).Image = ImageUtils.AddGenreString(pbGenre(i).Image, pbGenre(i).Name)
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
    End Sub

    Private Sub mnuMovieCustom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuScrapeSubmenuCustom.Click
        Me.SetControlsEnabled(False)
        Using dUpdate As New dlgCustomScraperMovie
            Dim CustomUpdater As Structures.CustomUpdaterStruct = Nothing
            CustomUpdater = dUpdate.ShowDialog()
            If Not CustomUpdater.Canceled Then
                Me.CreateScrapeList_Movie(CustomUpdater.ScrapeType, CustomUpdater.Options, CustomUpdater.ScrapeModifier)
            Else
                Me.SetControlsEnabled(True)
            End If
        End Using
    End Sub


    Private Sub cmnuMovieRemoveFromDisk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieRemoveFromDisk.Click
        Try
            Dim MoviesToDelete As New Dictionary(Of Long, Long)
            Dim MovieId As Int64 = -1

            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                MovieId = Convert.ToInt64(sRow.Cells("idMovie").Value)
                If Not MoviesToDelete.ContainsKey(MovieId) Then
                    MoviesToDelete.Add(MovieId, 0)
                End If
            Next

            If MoviesToDelete.Count > 0 Then
                Using dlg As New dlgDeleteConfirm
                    If dlg.ShowDialog(MoviesToDelete, Enums.DelType.Movies) = Windows.Forms.DialogResult.OK Then
                        Me.FillList(True, True, False)
                    End If
                End Using
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvMovies_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim colName As String = Me.dgvMovies.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If colName = "ListTitle" OrElse colName = "Playcount" OrElse Not Master.eSettings.MovieClickScrape Then
            If Not colName = "Playcount" Then
                If Me.dgvMovies.SelectedRows.Count > 0 Then
                    If Me.dgvMovies.RowCount > 0 Then
                        If Me.dgvMovies.SelectedRows.Count > 1 Then
                            Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvMovies.SelectedRows.Count))
                        ElseIf Me.dgvMovies.SelectedRows.Count = 1 Then
                            Me.SetStatus(Me.dgvMovies.SelectedRows(0).Cells("MoviePath").Value.ToString)
                        End If
                    End If
                    Me.currRow_Movie = Me.dgvMovies.SelectedRows(0).Index
                End If
            Else
                SetWatchedStatus_Movie()
            End If

        ElseIf Master.eSettings.MovieClickScrape AndAlso colName = "HasSet" AndAlso Not bwMovieScraper.IsBusy Then
            Dim movie As Int32 = CType(Me.dgvMovies.Rows(e.RowIndex).Cells("idMovie").Value, Int32)
            Dim objCell As DataGridViewCell = CType(Me.dgvMovies.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCell)

            Me.dgvMovies.ClearSelection()
            Me.dgvMovies.Rows(objCell.RowIndex).Selected = True
            Me.currRow_Movie = objCell.RowIndex

            Dim scrapeOptions As New Structures.ScrapeOptions_Movie
            scrapeOptions.bCollectionID = True
            Dim ScrapeModifier As New Structures.ScrapeModifier
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
            CreateScrapeList_Movie(Enums.ScrapeType.SingleField, scrapeOptions, ScrapeModifier)

        ElseIf Master.eSettings.MovieClickScrape AndAlso _
            (colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse _
            colName = "DiscArtPath" OrElse colName = "EFanartsPath" OrElse colName = "EThumbsPath" OrElse _
            colName = "FanartPath" OrElse colName = "LandscapePath" OrElse colName = "NfoPath" OrElse _
            colName = "PosterPath" OrElse colName = "ThemePath" OrElse colName = "TrailerPath") AndAlso _
            Not bwMovieScraper.IsBusy Then
            Dim movie As Int32 = CType(Me.dgvMovies.Rows(e.RowIndex).Cells("idMovie").Value, Int32)
            Dim objCell As DataGridViewCell = CType(Me.dgvMovies.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCell)

            'EMM not able to scrape subtitles yet.
            'So don't set status for it, but leave the option open for the future.
            Me.dgvMovies.ClearSelection()
            Me.dgvMovies.Rows(objCell.RowIndex).Selected = True
            Me.currRow_Movie = objCell.RowIndex

            Dim ScrapeModifier As New Structures.ScrapeModifier
            Select Case colName
                Case "BannerPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainBanner, True)
                Case "ClearArtPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearArt, True)
                Case "ClearLogoPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearLogo, True)
                Case "DiscArtPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainDiscArt, True)
                Case "EFanartsPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainExtrafanarts, True)
                Case "EThumbsPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainExtrathumbs, True)
                Case "FanartPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainFanart, True)
                Case "LandscapePath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainLandscape, True)
                Case "NfoPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
                Case "PosterPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainPoster, True)
                Case "ThemePath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainTheme, True)
                Case "TrailerPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainTrailer, True)
                Case "HasSub"
                    'Functions.SetScraperMod(Enums.ModType.Subtitles, True)
                Case "MetaData" 'Metadata - need to add this column to the view.
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainMeta, True)
            End Select
            If Master.eSettings.MovieClickScrapeAsk Then
                CreateScrapeList_Movie(Enums.ScrapeType.SelectedAsk, Master.DefaultOptions_Movie, ScrapeModifier)
            Else
                CreateScrapeList_Movie(Enums.ScrapeType.SelectedAuto, Master.DefaultOptions_Movie, ScrapeModifier)
            End If
        End If
    End Sub

    Private Sub dgvMovies_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub

        If Me.fScanner.IsBusy OrElse Me.bwMetaData.IsBusy OrElse Me.bwLoadMovieInfo.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse Me.bwRewrite_Movies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item("idMovie", indX).Value)
        Dim tmpDBMovie As Database.DBElement = Master.DB.LoadMovieFromDB(ID)
        Edit_Movie(tmpDBMovie)
    End Sub

    Private Sub dgvMovies_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellEnter
        Dim currMainTabTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
        If Not currMainTabTag.ContentType = Enums.ContentType.Movie Then Return

        Me.tmrWait_TVShow.Stop()
        Me.tmrWait_TVSeason.Stop()
        Me.tmrWait_TVEpisode.Stop()
        Me.tmrWait_MovieSet.Stop()
        Me.tmrWait_Movie.Stop()
        Me.tmrLoad_TVShow.Stop()
        Me.tmrLoad_TVSeason.Stop()
        Me.tmrLoad_TVEpisode.Stop()
        Me.tmrLoad_MovieSet.Stop()
        Me.tmrLoad_Movie.Stop()

        Me.currRow_Movie = e.RowIndex
        Me.tmrWait_Movie.Start()
    End Sub

    Private Sub dgvMovies_CellMouseDown(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvMovies.CellMouseDown
        If e.Button = Windows.Forms.MouseButtons.Right And Me.dgvMovies.RowCount > 0 Then
            If bwCleanDB.IsBusy OrElse bwMovieScraper.IsBusy OrElse bwNonScrape.IsBusy Then
                Me.cmnuMovieTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
                Return
            End If

            Me.cmnuMovie.Enabled = False


            If e.RowIndex >= 0 AndAlso dgvMovies.SelectedRows.Count > 0 Then

                Me.cmnuMovie.Enabled = True
                Me.cmnuMovieChange.Visible = False
                Me.cmnuMovieChangeAuto.Visible = False
                Me.cmnuMovieEdit.Visible = False
                Me.cmnuMovieEditMetaData.Visible = False
                Me.cmnuMovieRescrapeSelected.Visible = True
                Me.cmnuMovieRescrape.Visible = False
                Me.cmnuMovieUpSel.Visible = True
                'Me.cmuRenamer.Visible = False
                Me.cmnuMovieSep4.Visible = False

                If Me.dgvMovies.SelectedRows.Count > 1 AndAlso Me.dgvMovies.Rows(e.RowIndex).Selected Then
                    Dim setMark As Boolean = False
                    Dim setLock As Boolean = False
                    Dim setWatched As Boolean = False
                    Me.cmnuMovieSep4.Visible = True

                    Me.cmnuMovieTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                    For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                        'if any one item is set as unmarked, set menu to mark
                        'else they are all marked, so set menu to unmark
                        If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                            setMark = True
                            If setLock AndAlso setWatched Then Exit For
                        End If
                        'if any one item is set as unlocked, set menu to lock
                        'else they are all locked so set menu to unlock
                        If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                            setLock = True
                            If setMark AndAlso setWatched Then Exit For
                        End If
                        'if any one item is set as unwatched, set menu to watched
                        'else they are all watched so set menu to not watched
                        If String.IsNullOrEmpty(sRow.Cells("Playcount").Value.ToString) OrElse sRow.Cells("Playcount").Value.ToString = "0" Then
                            setWatched = True
                            If setLock AndAlso setMark Then Exit For
                        End If
                    Next

                    Me.cmnuMovieMark.Text = If(setMark, Master.eLang.GetString(23, "Mark"), Master.eLang.GetString(107, "Unmark"))
                    Me.cmnuMovieLock.Text = If(setLock, Master.eLang.GetString(24, "Lock"), Master.eLang.GetString(108, "Unlock"))
                    Me.cmnuMovieWatched.Text = If(setWatched, Master.eLang.GetString(981, "Watched"), Master.eLang.GetString(980, "Not Watched"))

                    Me.cmnuMovieGenresGenre.Items.Insert(0, Master.eLang.GetString(98, "Select Genre..."))
                    Me.cmnuMovieGenresGenre.SelectedItem = Master.eLang.GetString(98, "Select Genre...")
                    Me.cmnuMovieGenresAdd.Enabled = False
                    Me.cmnuMovieGenresSet.Enabled = False
                    Me.cmnuMovieGenresRemove.Enabled = False

                    Me.cmnuMovieLanguageLanguages.Items.Insert(0, Master.eLang.GetString(1199, "Select Language..."))
                    Me.cmnuMovieLanguageLanguages.SelectedItem = Master.eLang.GetString(1199, "Select Language...")
                    Me.cmnuMovieLanguageSet.Enabled = False
                Else
                    Me.cmnuMovieChange.Visible = True
                    Me.cmnuMovieChangeAuto.Visible = True
                    Me.cmnuMovieEdit.Visible = True
                    Me.cmnuMovieEditMetaData.Visible = True
                    Me.cmnuMovieRescrapeSelected.Visible = True
                    Me.cmnuMovieRescrape.Visible = True
                    Me.cmnuMovieUpSel.Visible = True
                    Me.cmnuMovieSep3.Visible = True
                    Me.cmnuMovieSep4.Visible = True

                    cmnuMovieTitle.Text = String.Concat(">> ", Me.dgvMovies.Item("Title", e.RowIndex).Value, " <<")

                    If Not Me.dgvMovies.Rows(e.RowIndex).Selected Then
                        Me.prevRow_Movie = -1
                        Me.dgvMovies.CurrentCell = Nothing
                        Me.dgvMovies.ClearSelection()
                        Me.dgvMovies.Rows(e.RowIndex).Selected = True
                        Me.dgvMovies.CurrentCell = Me.dgvMovies.Item("ListTitle", e.RowIndex)
                    Else
                        Me.cmnuMovie.Enabled = True
                    End If

                    Me.cmnuMovieMark.Text = If(Convert.ToBoolean(Me.dgvMovies.Item("Mark", e.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))
                    Me.cmnuMovieLock.Text = If(Convert.ToBoolean(Me.dgvMovies.Item("Lock", e.RowIndex).Value), Master.eLang.GetString(108, "Unlock"), Master.eLang.GetString(24, "Lock"))
                    Me.cmnuMovieWatched.Text = If(Not String.IsNullOrEmpty(Me.dgvMovies.Item("Playcount", e.RowIndex).Value.ToString) AndAlso Not Me.dgvMovies.Item("Playcount", e.RowIndex).Value.ToString = "0", Master.eLang.GetString(980, "Not Watched"), Master.eLang.GetString(981, "Watched"))

                    Me.cmnuMovieGenresGenre.Tag = Me.dgvMovies.Item("Genre", e.RowIndex).Value
                    Me.cmnuMovieGenresGenre.Items.Insert(0, Master.eLang.GetString(98, "Select Genre..."))
                    Me.cmnuMovieGenresGenre.SelectedItem = Master.eLang.GetString(98, "Select Genre...")
                    Me.cmnuMovieGenresAdd.Enabled = False
                    Me.cmnuMovieGenresSet.Enabled = False
                    Me.cmnuMovieGenresRemove.Enabled = False

                    Dim Lang As String = CStr(Me.dgvMovies.Item("Language", e.RowIndex).Value)
                    Me.cmnuMovieLanguageLanguages.Text = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = Lang).name
                    Me.cmnuMovieLanguageSet.Enabled = False
                End If
            Else
                Me.cmnuMovie.Enabled = False
                Me.cmnuMovieTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
            End If
        End If
    End Sub

    Private Sub dgvMovies_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellMouseEnter
        Dim colName As String = Me.dgvMovies.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        Me.dgvMovies.ShowCellToolTips = True

        If colName = "Playcount" AndAlso e.RowIndex >= 0 Then
            oldStatus = GetStatus()
            Me.SetStatus(Master.eLang.GetString(885, "Change Watched Status"))
        ElseIf (colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse _
            colName = "DiscArtPath" OrElse colName = "EFanartsPath" OrElse colName = "EThumbsPath" OrElse _
            colName = "FanartPath" OrElse colName = "LandscapePath" OrElse colName = "NfoPath" OrElse _
            colName = "PosterPath" OrElse colName = "ThemePath" OrElse colName = "TrailerPath" OrElse _
            colName = "HasSet" OrElse colName = "HasSub") AndAlso e.RowIndex >= 0 Then
            Me.dgvMovies.ShowCellToolTips = False

            If Master.eSettings.MovieClickScrape AndAlso Not bwMovieScraper.IsBusy Then
                oldStatus = GetStatus()
                Dim movieTitle As String = Me.dgvMovies.Rows(e.RowIndex).Cells("Title").Value.ToString
                Dim scrapeFor As String = String.Empty
                Dim scrapeType As String = String.Empty
                Select Case colName
                    Case "BannerPath"
                        scrapeFor = Master.eLang.GetString(1060, "Banner Only")
                    Case "ClearArtPath"
                        scrapeFor = Master.eLang.GetString(1122, "ClearArt Only")
                    Case "ClearLogoPath"
                        scrapeFor = Master.eLang.GetString(1123, "ClearLogo Only")
                    Case "DiscArtPath"
                        scrapeFor = Master.eLang.GetString(1124, "DiscArt Only")
                    Case "EFanartsPath"
                        scrapeFor = Master.eLang.GetString(975, "Extrafanarts Only")
                    Case "EThumbsPath"
                        scrapeFor = Master.eLang.GetString(74, "Extrathumbs Only")
                    Case "FanartPath"
                        scrapeFor = Master.eLang.GetString(73, "Fanart Only")
                    Case "LandscapePath"
                        scrapeFor = Master.eLang.GetString(1061, "Landscape Only")
                    Case "NfoPath"
                        scrapeFor = Master.eLang.GetString(71, "NFO Only")
                    Case "MetaData"
                        scrapeFor = Master.eLang.GetString(76, "Meta Data Only")
                    Case "PosterPath"
                        scrapeFor = Master.eLang.GetString(72, "Poster Only")
                    Case "ThemePath"
                        scrapeFor = Master.eLang.GetString(1125, "Theme Only")
                    Case "TrailerPath"
                        scrapeFor = Master.eLang.GetString(75, "Trailer Only")
                    Case "HasSet"
                        scrapeFor = Master.eLang.GetString(1354, "MovieSet Informations Only")
                    Case "HasSub"
                        scrapeFor = Master.eLang.GetString(1355, "Subtitles Only")
                End Select

                If Master.eSettings.MovieClickScrapeAsk Then
                    scrapeType = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
                Else
                    scrapeType = Master.eLang.GetString(69, "Automatic (Force Best Match)")
                End If

                Me.SetStatus(String.Format("Scrape ""{0}"" for {1} - {2}", movieTitle, scrapeFor, scrapeType))
            Else
                oldStatus = String.Empty
            End If
        Else
            oldStatus = String.Empty
        End If
    End Sub

    Private Sub dgvMovies_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovies.CellMouseLeave
        If Not String.IsNullOrEmpty(oldStatus) Then Me.SetStatus(oldStatus)
    End Sub

    Private Sub dgvMovies_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvMovies.CellPainting
        Dim colName As String = Me.dgvMovies.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not Me.dgvMovies.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If

        'icons for column header
        If (colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse _
            colName = "DiscArtPath" OrElse colName = "EFanartsPath" OrElse colName = "EThumbsPath" OrElse _
            colName = "FanartPath" OrElse colName = "LandscapePath" OrElse colName = "NfoPath" OrElse _
            colName = "PosterPath" OrElse colName = "ThemePath" OrElse colName = "TrailerPath" OrElse _
            colName = "HasSet" OrElse colName = "HasSub" OrElse colName = "Playcount") AndAlso e.RowIndex = -1 Then
            e.PaintBackground(e.ClipBounds, False)

            Dim pt As Point = e.CellBounds.Location
            Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

            pt.X += offset
            pt.Y = 3

            If colName = "BannerPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 2)
            ElseIf colName = "ClearArtPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 4)
            ElseIf colName = "ClearLogoPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 5)
            ElseIf colName = "DiscArtPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 6)
            ElseIf colName = "EFanartsPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 7)
            ElseIf colName = "EThumbsPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 8)
            ElseIf colName = "FanartPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 9)
            ElseIf colName = "LandscapePath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 10)
            ElseIf colName = "NfoPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 11)
            ElseIf colName = "PosterPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 12)
            ElseIf colName = "ThemePath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 15)
            ElseIf colName = "TrailerPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 16)
            ElseIf colName = "HasSet" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 13)
            ElseIf colName = "HasSub" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 14)
            ElseIf colName = "Playcount" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 17)
            End If

            e.Handled = True

        End If

        'text fields
        If (colName = "Imdb" OrElse colName = "ListTitle" OrElse colName = "MPAA" OrElse colName = "OriginalTitle" OrElse _
            colName = "Rating" OrElse colName = "TMDB" OrElse colName = "Year") AndAlso e.RowIndex >= 0 Then
            If Convert.ToBoolean(Me.dgvMovies.Item("Mark", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Crimson
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Crimson
            ElseIf Convert.ToBoolean(Me.dgvMovies.Item("New", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Green
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Green
            ElseIf Convert.ToBoolean(Me.dgvMovies.Item("MarkCustom1", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker1Color)
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker1Color)
            ElseIf Convert.ToBoolean(Me.dgvMovies.Item("MarkCustom2", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker2Color)
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker2Color)
            ElseIf Convert.ToBoolean(Me.dgvMovies.Item("MarkCustom3", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker3Color)
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker3Color)
            ElseIf Convert.ToBoolean(Me.dgvMovies.Item("MarkCustom4", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker4Color)
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker4Color)
            Else
                e.CellStyle.ForeColor = Color.Black
                e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
            End If
        End If

        If e.ColumnIndex >= 2 AndAlso e.RowIndex >= 0 Then

            'background
            If Convert.ToBoolean(Me.dgvMovies.Item("Lock", e.RowIndex).Value) Then
                e.CellStyle.BackColor = Color.LightSteelBlue
                e.CellStyle.SelectionBackColor = Color.DarkTurquoise
            ElseIf Convert.ToBoolean(Me.dgvMovies.Item("OutOfTolerance", e.RowIndex).Value) Then
                e.CellStyle.BackColor = Color.MistyRose
                e.CellStyle.SelectionBackColor = Color.DarkMagenta
            Else
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
            End If

            'path fields
            If colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse _
                colName = "DiscArtPath" OrElse colName = "EFanartsPath" OrElse colName = "EThumbsPath" OrElse _
                colName = "FanartPath" OrElse colName = "LandscapePath" OrElse colName = "NfoPath" OrElse _
                colName = "PosterPath" OrElse colName = "ThemePath" OrElse colName = "TrailerPath" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                Me.ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString), 0, 1))
                e.Handled = True
            End If

            'playcount field
            If colName = "Playcount" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                Me.ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString) AndAlso Not e.Value.ToString = "0", 0, 1))
                e.Handled = True
            End If

            'boolean fields
            If colName = "HasSet" OrElse colName = "HasSub" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                Me.ilColumnIcons.Draw(e.Graphics, pt, If(Convert.ToBoolean(e.Value), 0, 1))
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgvMovies_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvMovies.KeyDown
        'stop enter key from selecting next list item
        e.Handled = (e.KeyCode = Keys.Enter)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.S Then txtSearchMovies.Focus()
    End Sub

    Private Sub dgvMovies_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvMovies.KeyPress
        Try
            If StringUtils.AlphaNumericOnly(e.KeyChar) OrElse e.KeyChar = Convert.ToChar(Keys.Space) Then
                KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
                tmrKeyBuffer.Start()
                For Each drvRow As DataGridViewRow In Me.dgvMovies.Rows
                    If drvRow.Cells("ListTitle").Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                        drvRow.Selected = True
                        Me.dgvMovies.CurrentCell = drvRow.Cells("ListTitle")
                        Exit For
                    End If
                Next
            ElseIf e.KeyChar = Convert.ToChar(Keys.Enter) Then
                If Me.fScanner.IsBusy OrElse Me.bwMetaData.IsBusy OrElse Me.bwLoadMovieInfo.IsBusy OrElse _
                Me.bwDownloadPic.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwReload_Movies.IsBusy _
                OrElse Me.bwCleanDB.IsBusy OrElse Me.bwRewrite_Movies.IsBusy Then Return

                Me.SetStatus(Me.currMovie.Filename)

                If Me.dgvMovies.SelectedRows.Count > 1 Then Return

                Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
                Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item("idMovie", indX).Value)
                Dim tmpDBMovie As Database.DBElement = Master.DB.LoadMovieFromDB(ID)
                Edit_Movie(tmpDBMovie)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvMovies_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovies.Resize
        ResizeMoviesList()
    End Sub

    Private Sub dgvMovies_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgvMovies.RowsRemoved
        Me.SetMovieCount()
    End Sub

    Private Sub dgvMovies_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgvMovies.RowsAdded
        Me.SetMovieCount()
    End Sub

    Private Sub dgvMovies_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovies.Sorted
        Me.prevRow_Movie = -1
        If Me.dgvMovies.RowCount > 0 Then
            Me.dgvMovies.CurrentCell = Nothing
            Me.dgvMovies.ClearSelection()
            Me.dgvMovies.Rows(0).Selected = True
            Me.dgvMovies.CurrentCell = Me.dgvMovies.Rows(0).Cells("ListTitle")
        End If

        If Me.dgvMovies.SortedColumn.HeaderCell.Value.ToString = "Year" AndAlso Me.dgvMovies.SortOrder = 1 Then
            Me.btnFilterSortYear_Movies.Tag = "ASC"
            Me.btnFilterSortYear_Movies.Image = My.Resources.asc
        ElseIf Me.dgvMovies.SortedColumn.HeaderCell.Value.ToString = "Year" AndAlso Me.dgvMovies.SortOrder = 2 Then
            Me.btnFilterSortYear_Movies.Tag = "DESC"
            Me.btnFilterSortYear_Movies.Image = My.Resources.desc
        Else
            Me.btnFilterSortYear_Movies.Tag = String.Empty
            Me.btnFilterSortYear_Movies.Image = Nothing
        End If

        If Me.dgvMovies.SortedColumn.HeaderCell.Value.ToString = "Rating" AndAlso Me.dgvMovies.SortOrder = 1 Then
            Me.btnFilterSortRating_Movies.Tag = "ASC"
            Me.btnFilterSortRating_Movies.Image = My.Resources.asc
        ElseIf Me.dgvMovies.SortedColumn.HeaderCell.Value.ToString = "Rating" AndAlso Me.dgvMovies.SortOrder = 2 Then
            Me.btnFilterSortRating_Movies.Tag = "DESC"
            Me.btnFilterSortRating_Movies.Image = My.Resources.desc
        Else
            Me.btnFilterSortRating_Movies.Tag = String.Empty
            Me.btnFilterSortRating_Movies.Image = Nothing
        End If

        If Me.dgvMovies.SortedColumn.HeaderCell.Value.ToString = "SortedTitle" AndAlso Me.dgvMovies.SortOrder = 1 Then
            Me.btnFilterSortTitle_Movies.Tag = "ASC"
            Me.btnFilterSortTitle_Movies.Image = My.Resources.asc
        ElseIf Me.dgvMovies.SortedColumn.HeaderCell.Value.ToString = "SortedTitle" AndAlso Me.dgvMovies.SortOrder = 2 Then
            Me.btnFilterSortTitle_Movies.Tag = "DESC"
            Me.btnFilterSortTitle_Movies.Image = My.Resources.desc
        Else
            Me.btnFilterSortTitle_Movies.Tag = String.Empty
            Me.btnFilterSortTitle_Movies.Image = Nothing
        End If

        If Me.dgvMovies.SortedColumn.HeaderCell.Value.ToString = "DateAdded" AndAlso Me.dgvMovies.SortOrder = 1 Then
            Me.btnFilterSortDateAdded_Movies.Tag = "ASC"
            Me.btnFilterSortDateAdded_Movies.Image = My.Resources.asc
        ElseIf Me.dgvMovies.SortedColumn.HeaderCell.Value.ToString = "DateAdded" AndAlso Me.dgvMovies.SortOrder = 2 Then
            Me.btnFilterSortDateAdded_Movies.Tag = "DESC"
            Me.btnFilterSortDateAdded_Movies.Image = My.Resources.desc
        Else
            Me.btnFilterSortDateAdded_Movies.Tag = String.Empty
            Me.btnFilterSortDateAdded_Movies.Image = Nothing
        End If

        If Me.dgvMovies.SortedColumn.HeaderCell.Value.ToString = "DateModified" AndAlso Me.dgvMovies.SortOrder = 1 Then
            Me.btnFilterSortDateModified_Movies.Tag = "ASC"
            Me.btnFilterSortDateModified_Movies.Image = My.Resources.asc
        ElseIf Me.dgvMovies.SortedColumn.HeaderCell.Value.ToString = "DateModified" AndAlso Me.dgvMovies.SortOrder = 2 Then
            Me.btnFilterSortDateModified_Movies.Tag = "DESC"
            Me.btnFilterSortDateModified_Movies.Image = My.Resources.desc
        Else
            Me.btnFilterSortDateModified_Movies.Tag = String.Empty
            Me.btnFilterSortDateModified_Movies.Image = Nothing
        End If

        Me.SaveFilter_Movies()
    End Sub

    Private Sub dgvMovieSets_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim colName As String = Me.dgvMovieSets.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If colName = "ListTitle" OrElse Not Master.eSettings.MovieSetClickScrape Then
            If Me.dgvMovieSets.SelectedRows.Count > 0 Then
                If Me.dgvMovieSets.RowCount > 0 Then
                    If Me.dgvMovieSets.SelectedRows.Count > 1 Then
                        Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvMovieSets.SelectedRows.Count))
                    ElseIf Me.dgvMovieSets.SelectedRows.Count = 1 Then
                        Me.SetStatus(Me.dgvMovieSets.SelectedRows(0).Cells("SetName").Value.ToString)
                    End If
                End If
                Me.currRow_MovieSet = Me.dgvMovieSets.SelectedRows(0).Index
            End If

        ElseIf Master.eSettings.MovieSetClickScrape AndAlso _
            (colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse colName = "DiscArtPath" OrElse _
             colName = "FanartPath" OrElse colName = "LandscapePath" OrElse colName = "NfoPath" OrElse colName = "PosterPath") AndAlso Not bwMovieSetScraper.IsBusy Then
            Dim movieset As Int32 = CType(Me.dgvMovieSets.Rows(e.RowIndex).Cells("idSet").Value, Int32)
            Dim objCell As DataGridViewCell = CType(Me.dgvMovieSets.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCell)

            Me.dgvMovieSets.ClearSelection()
            Me.dgvMovieSets.Rows(objCell.RowIndex).Selected = True
            Me.currRow_MovieSet = objCell.RowIndex
            Dim ScrapeModifier As New Structures.ScrapeModifier
            Select Case colName
                Case "BannerPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainBanner, True)
                Case "ClearArtPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearArt, True)
                Case "ClearLogoPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearLogo, True)
                Case "DiscArtPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainDiscArt, True)
                Case "FanartPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainFanart, True)
                Case "LandscapePath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainLandscape, True)
                Case "NfoPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
                Case "PosterPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainPoster, True)
            End Select
            If Master.eSettings.MovieSetClickScrapeAsk Then
                CreateScrapeList_MovieSet(Enums.ScrapeType.SelectedAsk, Master.DefaultOptions_MovieSet, ScrapeModifier)
            Else
                CreateScrapeList_MovieSet(Enums.ScrapeType.SelectedAuto, Master.DefaultOptions_MovieSet, ScrapeModifier)
            End If
        End If
    End Sub

    Private Sub dgvMovieSets_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub

        If Me.fScanner.IsBusy OrElse Me.bwLoadMovieSetInfo.IsBusy OrElse Me.bwReload_MovieSets.IsBusy OrElse Me.bwMovieSetScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

        Dim indX As Integer = Me.dgvMovieSets.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(Me.dgvMovieSets.Item("idSet", indX).Value)
        Dim tmpDBMovieSet As Database.DBElement = Master.DB.LoadMovieSetFromDB(ID)
        Edit_MovieSet(tmpDBMovieSet)
    End Sub

    Private Sub dgvMovieSets_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellEnter
        Dim currMainTabTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
        If Not currMainTabTag.ContentType = Enums.ContentType.MovieSet Then Return

        Me.tmrWait_TVShow.Stop()
        Me.tmrWait_TVSeason.Stop()
        Me.tmrWait_TVEpisode.Stop()
        Me.tmrWait_Movie.Stop()
        Me.tmrWait_MovieSet.Stop()
        Me.tmrLoad_TVShow.Stop()
        Me.tmrLoad_TVSeason.Stop()
        Me.tmrLoad_TVEpisode.Stop()
        Me.tmrLoad_Movie.Stop()
        Me.tmrLoad_MovieSet.Stop()

        Me.currRow_MovieSet = e.RowIndex
        Me.tmrWait_MovieSet.Start()
    End Sub

    Private Sub dgvMovieSets_CellMouseDown(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvMovieSets.CellMouseDown
        If e.Button = Windows.Forms.MouseButtons.Right And Me.dgvMovieSets.RowCount > 0 Then
            If bwCleanDB.IsBusy OrElse bwMovieSetScraper.IsBusy OrElse bwNonScrape.IsBusy Then
                Me.cmnuMovieSetTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
                Return
            End If

            Me.cmnuMovieSet.Enabled = False


            If e.RowIndex >= 0 AndAlso dgvMovieSets.SelectedRows.Count > 0 Then

                Me.cmnuMovieSet.Enabled = True
                Me.cmnuMovieSetReload.Visible = True
                Me.cmnuMovieSetSep3.Visible = True
                Me.cmnuMovieSetEdit.Visible = False
                Me.cmnuMovieSetRemove.Visible = True
                Me.cmnuMovieSetSep3.Visible = False
                Me.cmnuMovieSetRescrape.Visible = False

                If Me.dgvMovieSets.SelectedRows.Count > 1 AndAlso Me.dgvMovieSets.Rows(e.RowIndex).Selected Then
                    Dim setMark As Boolean = False
                    Dim setLock As Boolean = False
                    'Dim setWatched As Boolean = False

                    Me.cmnuMovieSetTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                    For Each sRow As DataGridViewRow In Me.dgvMovieSets.SelectedRows
                        'if any one item is set as unmarked, set menu to mark
                        'else they are all marked, so set menu to unmark
                        If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                            setMark = True
                            If setLock Then Exit For
                        End If
                        'if any one item is set as unlocked, set menu to lock
                        'else they are all locked so set menu to unlock
                        If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                            setLock = True
                            If setMark Then Exit For
                        End If
                        ''if any one item is set as unwatched, set menu to watched
                        ''else they are all watched so set menu to not watched
                        'If Not Convert.ToBoolean(sRow.Cells("HasWatched").Value) Then
                        '    setWatched = True
                        '    If setLock AndAlso setMark Then Exit For
                        'End If
                    Next

                    Me.cmnuMovieSetMark.Text = If(setMark, Master.eLang.GetString(23, "Mark"), Master.eLang.GetString(107, "Unmark"))
                    Me.cmnuMovieSetLock.Text = If(setLock, Master.eLang.GetString(24, "Lock"), Master.eLang.GetString(108, "Unlock"))

                    Me.cmnuMovieSetSortMethodMethods.SelectedIndex = -1
                    Me.cmnuMovieSetSortMethodSet.Enabled = False

                Else
                    Me.cmnuMovieSetReload.Visible = True
                    Me.cmnuMovieSetMark.Visible = True
                    Me.cmnuMovieSetLock.Visible = True
                    Me.cmnuMovieSetSep2.Visible = True
                    Me.cmnuMovieSetNew.Visible = True
                    Me.cmnuMovieSetEdit.Visible = True
                    Me.cmnuMovieSetRemove.Visible = True
                    Me.cmnuMovieSetSep3.Visible = True
                    Me.cmnuMovieSetRescrape.Visible = True

                    cmnuMovieSetTitle.Text = String.Concat(">> ", Me.dgvMovieSets.Item("SetName", e.RowIndex).Value, " <<")

                    If Not Me.dgvMovieSets.Rows(e.RowIndex).Selected Then
                        Me.prevRow_MovieSet = -1
                        Me.dgvMovieSets.CurrentCell = Nothing
                        Me.dgvMovieSets.ClearSelection()
                        Me.dgvMovieSets.Rows(e.RowIndex).Selected = True
                        Me.dgvMovieSets.CurrentCell = Me.dgvMovieSets.Item("ListTitle", e.RowIndex)
                    Else
                        Me.cmnuMovieSet.Enabled = True
                    End If

                    Me.cmnuMovieSetMark.Text = If(Convert.ToBoolean(Me.dgvMovieSets.Item("Mark", e.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))
                    Me.cmnuMovieSetLock.Text = If(Convert.ToBoolean(Me.dgvMovieSets.Item("Lock", e.RowIndex).Value), Master.eLang.GetString(108, "Unlock"), Master.eLang.GetString(24, "Lock"))

                    Dim SortMethod As Integer = CInt(Me.dgvMovieSets.Item("SortMethod", e.RowIndex).Value)
                    Me.cmnuMovieSetSortMethodMethods.Text = DirectCast(CInt(Me.dgvMovieSets.Item("SortMethod", e.RowIndex).Value), Enums.SortMethod_MovieSet).ToString
                    Me.cmnuMovieSetSortMethodSet.Enabled = False
                End If
            Else
                Me.cmnuMovieSet.Enabled = True
                Me.cmnuMovieSetReload.Visible = False
                Me.cmnuMovieSetMark.Visible = False
                Me.cmnuMovieSetLock.Visible = False
                Me.cmnuMovieSetSep2.Visible = False
                Me.cmnuMovieSetNew.Visible = True
                Me.cmnuMovieSetEdit.Visible = False
                Me.cmnuMovieSetRemove.Visible = False
                Me.cmnuMovieSetSep3.Visible = False
                Me.cmnuMovieSetRescrape.Visible = False
                Me.cmnuMovieSetTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
            End If
        End If
    End Sub

    Private Sub dgvMovieSets_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellMouseEnter
        Dim colName As String = Me.dgvMovieSets.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        Me.dgvMovieSets.ShowCellToolTips = True

        If (colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse colName = "DiscArtPath" OrElse _
             colName = "FanartPath" OrElse colName = "LandscapePath" OrElse colName = "NfoPath" OrElse colName = "PosterPath") AndAlso e.RowIndex >= 0 Then
            Me.dgvMovieSets.ShowCellToolTips = False

            If Master.eSettings.MovieSetClickScrape AndAlso Not bwMovieSetScraper.IsBusy Then
                oldStatus = GetStatus()
                Dim movieSetName As String = Me.dgvMovieSets.Rows(e.RowIndex).Cells("SetName").Value.ToString
                Dim scrapeFor As String = String.Empty
                Dim scrapeType As String = String.Empty
                Select Case colName
                    Case "BannerPath"
                        scrapeFor = Master.eLang.GetString(1060, "Banner Only")
                    Case "ClearArtPath"
                        scrapeFor = Master.eLang.GetString(1122, "ClearArt Only")
                    Case "ClearLogoPath"
                        scrapeFor = Master.eLang.GetString(1123, "ClearLogo Only")
                    Case "DiscArtPath"
                        scrapeFor = Master.eLang.GetString(1124, "DiscArt Only")
                    Case "FanartPath"
                        scrapeFor = Master.eLang.GetString(73, "Fanart Only")
                    Case "LandscapePath"
                        scrapeFor = Master.eLang.GetString(1061, "Landscape Only")
                    Case "NfoPath"
                        scrapeFor = Master.eLang.GetString(71, "NFO Only")
                    Case "PosterPath"
                        scrapeFor = Master.eLang.GetString(72, "Poster Only")
                End Select
                If Master.eSettings.MovieSetClickScrapeAsk Then
                    scrapeType = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
                Else
                    scrapeType = Master.eLang.GetString(69, "Automatic (Force Best Match)")
                End If
                Me.SetStatus(String.Format("Scrape ""{0}"" for {1} - {2}", movieSetName, scrapeFor, scrapeType))
            Else
                oldStatus = String.Empty
            End If
        Else
            oldStatus = String.Empty
        End If
    End Sub

    Private Sub dgvMovieSets_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMovieSets.CellMouseLeave
        If Not String.IsNullOrEmpty(oldStatus) Then Me.SetStatus(oldStatus)
    End Sub

    Private Sub dgvMovieSets_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvMovieSets.CellPainting
        Dim colName As String = Me.dgvMovieSets.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not Me.dgvMovieSets.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If

        'icons for column header
        If (colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse _
            colName = "DiscArtPath" OrElse colName = "FanartPath" OrElse colName = "LandscapePath" OrElse _
            colName = "NfoPath" OrElse colName = "PosterPath") AndAlso e.RowIndex = -1 Then
            e.PaintBackground(e.ClipBounds, False)

            Dim pt As Point = e.CellBounds.Location
            Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

            pt.X += offset
            pt.Y = 3

            If colName = "BannerPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 2)
            ElseIf colName = "ClearArtPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 4)
            ElseIf colName = "ClearLogoPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 5)
            ElseIf colName = "DiscArtPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 6)
            ElseIf colName = "FanartPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 9)
            ElseIf colName = "LandscapePath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 10)
            ElseIf colName = "NfoPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 11)
            ElseIf colName = "PosterPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 12)
            End If

            e.Handled = True

        End If

        'text fields
        If (colName = "ListTitle") AndAlso e.RowIndex >= 0 Then
            If Convert.ToBoolean(Me.dgvMovieSets.Item("Mark", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Crimson
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Crimson
            ElseIf Convert.ToBoolean(Me.dgvMovieSets.Item("New", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Green
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Green
            Else
                e.CellStyle.ForeColor = Color.Black
                e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
            End If
        End If

        If e.ColumnIndex >= 1 AndAlso e.RowIndex >= 0 Then

            'background
            If Convert.ToBoolean(Me.dgvMovieSets.Item("Lock", e.RowIndex).Value) Then
                e.CellStyle.BackColor = Color.LightSteelBlue
                e.CellStyle.SelectionBackColor = Color.DarkTurquoise
            Else
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
            End If

            'path fields
            If colName = "BannerPath" OrElse colName = "ClearArtPath" OrElse colName = "ClearLogoPath" OrElse _
                colName = "DiscArtPath" OrElse colName = "FanartPath" OrElse colName = "LandscapePath" OrElse _
                colName = "NfoPath" OrElse colName = "PosterPath" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                Me.ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString), 0, 1))
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgvMovieSets_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvMovieSets.KeyDown
        'stop enter key from selecting next list item
        e.Handled = (e.KeyCode = Keys.Enter)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.S Then txtSearchMovieSets.Focus()
    End Sub

    Private Sub dgvMovieSets_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvMovieSets.KeyPress
        If StringUtils.AlphaNumericOnly(e.KeyChar) OrElse e.KeyChar = Convert.ToChar(Keys.Space) Then
            KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
            tmrKeyBuffer.Start()
            For Each drvRow As DataGridViewRow In Me.dgvMovieSets.Rows
                If drvRow.Cells("ListTitle").Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                    drvRow.Selected = True
                    Me.dgvMovieSets.CurrentCell = drvRow.Cells("ListTitle")
                    Exit For
                End If
            Next
        ElseIf e.KeyChar = Convert.ToChar(Keys.Enter) Then
            If Me.fScanner.IsBusy OrElse Me.bwLoadMovieSetInfo.IsBusy OrElse _
            Me.bwLoadMovieSetPosters.IsBusy OrElse Me.bwMovieSetScraper.IsBusy OrElse Me.bwReload_MovieSets.IsBusy OrElse _
            Me.bwCleanDB.IsBusy Then Return

            Dim indX As Integer = Me.dgvMovieSets.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(Me.dgvMovieSets.Item("idSet", indX).Value)
            Me.currMovieSet = Master.DB.LoadMovieSetFromDB(ID)
            Me.SetStatus(Me.currMovieSet.ListTitle)
            Dim tmpDBMovieSet As Database.DBElement = Master.DB.LoadMovieSetFromDB(ID)
            Edit_MovieSet(tmpDBMovieSet)
        End If
    End Sub

    Private Sub dgvMovieSets_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovieSets.Resize
        ResizeMovieSetsList()
    End Sub

    Private Sub dgvMovieSets_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgvMovieSets.RowsRemoved
        Me.SetMovieSetCount()
    End Sub

    Private Sub dgvMovieSets_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgvMovieSets.RowsAdded
        Me.SetMovieSetCount()
    End Sub

    Private Sub dgvMovieSets_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovieSets.Sorted
        Me.prevRow_MovieSet = -1
        If Me.dgvMovieSets.RowCount > 0 Then
            Me.dgvMovieSets.CurrentCell = Nothing
            Me.dgvMovieSets.ClearSelection()
            Me.dgvMovieSets.Rows(0).Selected = True
            Me.dgvMovieSets.CurrentCell = Me.dgvMovieSets.Rows(0).Cells("ListTitle")
        End If

        Me.SaveFilter_MovieSets()
    End Sub

    Private Sub dgvTVEpisodes_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVEpisodes.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim colName As String = Me.dgvTVEpisodes.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If colName = "Title" OrElse colName = "Playcount" OrElse Not Master.eSettings.TVGeneralClickScrape Then
            If Not colName = "Playcount" Then
                If Me.dgvTVEpisodes.SelectedRows.Count > 0 Then
                    If Me.dgvTVEpisodes.RowCount > 0 Then
                        If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then
                            Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvTVEpisodes.SelectedRows.Count))
                        ElseIf Me.dgvTVEpisodes.SelectedRows.Count = 1 Then
                            Me.SetStatus(Me.dgvTVEpisodes.SelectedRows(0).Cells("Title").Value.ToString)
                        End If
                    End If
                    Me.currRow_TVEpisode = Me.dgvTVEpisodes.SelectedRows(0).Index
                    If Not Me.currList = 2 Then
                        Me.currList = 2
                        Me.prevRow_TVEpisode = -1
                        Me.SelectRow_TVEpisode(Me.dgvTVEpisodes.SelectedRows(0).Index)
                    End If
                End If
            Else
                SetWatchedStatus_TVEpisode()
            End If

        ElseIf Master.eSettings.TVGeneralClickScrape AndAlso _
            (colName = "FanartPath" OrElse colName = "NfoPath" OrElse colName = "PosterPath") AndAlso _
            Not bwTVEpisodeScraper.IsBusy Then
            Dim episode As Int32 = CType(Me.dgvTVEpisodes.Rows(e.RowIndex).Cells("idEpisode").Value, Int32)
            Dim objCell As DataGridViewCell = CType(Me.dgvTVEpisodes.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCell)

            'EMM not able to scrape subtitles yet.
            'So don't set status for it, but leave the option open for the future.
            Me.dgvTVEpisodes.ClearSelection()
            Me.dgvTVEpisodes.Rows(objCell.RowIndex).Selected = True
            Me.currRow_TVEpisode = objCell.RowIndex

            Dim ScrapeModifier As New Structures.ScrapeModifier
            Select Case colName
                Case "FanartPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.EpisodeFanart, True)
                Case "NfoPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.EpisodeNFO, True)
                Case "PosterPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.EpisodePoster, True)
                Case "HasSub"
                    'Functions.SetScraperMod(Enums.ModType.Subtitles, True)
                Case "MetaData" 'Metadata - need to add this column to the view.
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.EpisodeMeta, True)
            End Select
            If Master.eSettings.TVGeneralClickScrapeAsk Then
                CreateScrapeList_TVEpisode(Enums.ScrapeType.SelectedAsk, Master.DefaultOptions_TV, ScrapeModifier)
            Else
                CreateScrapeList_TVEpisode(Enums.ScrapeType.SelectedAuto, Master.DefaultOptions_TV, ScrapeModifier)
            End If
        End If
    End Sub

    Private Sub dgvTVEpisodes_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVEpisodes.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub

        If Me.fScanner.IsBusy OrElse Me.bwMetaData.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwLoadEpInfo.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse Me.bwReload_MovieSets.IsBusy _
            OrElse Me.bwRewrite_Movies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwMovieSetScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

        Dim indX As Integer = Me.dgvTVEpisodes.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(Me.dgvTVEpisodes.Item("idEpisode", indX).Value)
        Dim tmpDBTVEpisode As Database.DBElement = Master.DB.LoadTVEpisodeFromDB(ID, True)
        Edit_TVEpisode(tmpDBTVEpisode)
    End Sub

    Private Sub dgvTVEpisodes_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVEpisodes.CellEnter
        Dim currTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
        If Not currTag.ContentType = Enums.ContentType.TV OrElse Not Me.currList = 2 Then Return

        Me.tmrWait_TVShow.Stop()
        Me.tmrWait_TVSeason.Stop()
        Me.tmrWait_Movie.Stop()
        Me.tmrWait_MovieSet.Stop()
        Me.tmrWait_TVEpisode.Stop()
        Me.tmrLoad_TVShow.Stop()
        Me.tmrLoad_TVSeason.Stop()
        Me.tmrLoad_Movie.Stop()
        Me.tmrLoad_MovieSet.Stop()
        Me.tmrLoad_TVEpisode.Stop()

        Me.currRow_TVEpisode = e.RowIndex
        Me.tmrWait_TVEpisode.Start()
    End Sub

    Private Sub dgvTVEpisodes_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVEpisodes.CellMouseEnter
        Dim colName As String = Me.dgvTVEpisodes.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        Me.dgvTVEpisodes.ShowCellToolTips = True

        If colName = "Playcount" AndAlso e.RowIndex >= 0 Then
            oldStatus = GetStatus()
            Me.SetStatus(Master.eLang.GetString(885, "Change Watched Status"))
        ElseIf (colName = "FanartPath" OrElse colName = "NfoPath" OrElse _
            colName = "PosterPath" OrElse colName = "HasSub") AndAlso e.RowIndex >= 0 Then
            Me.dgvTVEpisodes.ShowCellToolTips = False

            If Master.eSettings.TVGeneralClickScrape AndAlso Not bwTVEpisodeScraper.IsBusy Then
                oldStatus = GetStatus()
                Dim episodeTitle As String = Me.dgvTVEpisodes.Rows(e.RowIndex).Cells("Title").Value.ToString
                Dim scrapeFor As String = String.Empty
                Dim scrapeType As String = String.Empty
                Select Case colName
                    Case "FanartPath"
                        scrapeFor = Master.eLang.GetString(73, "Fanart Only")
                    Case "NfoPath"
                        scrapeFor = Master.eLang.GetString(71, "NFO Only")
                    Case "MetaData"
                        scrapeFor = Master.eLang.GetString(76, "Meta Data Only")
                    Case "PosterPath"
                        scrapeFor = Master.eLang.GetString(72, "Poster Only")
                    Case "HasSub"
                        scrapeFor = Master.eLang.GetString(1355, "Subtitles Only")
                End Select

                If Master.eSettings.TVGeneralClickScrapeAsk Then
                    scrapeType = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
                Else
                    scrapeType = Master.eLang.GetString(69, "Automatic (Force Best Match)")
                End If

                Me.SetStatus(String.Format("Scrape ""{0}"" for {1} - {2}", episodeTitle, scrapeFor, scrapeType))
            Else
                oldStatus = String.Empty
            End If
        Else
            oldStatus = String.Empty
        End If
    End Sub

    Private Sub dgvTVEpisodes_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVEpisodes.CellMouseLeave
        If Not String.IsNullOrEmpty(oldStatus) Then Me.SetStatus(oldStatus)
    End Sub

    Private Sub dgvTVEpisodes_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvTVEpisodes.CellPainting
        Dim colName As String = Me.dgvTVEpisodes.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not Me.dgvTVEpisodes.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If

        'icons for column header
        If (colName = "FanartPath" OrElse colName = "NfoPath" OrElse colName = "PosterPath" OrElse _
            colName = "HasSub" OrElse colName = "Playcount") AndAlso e.RowIndex = -1 Then
            e.PaintBackground(e.ClipBounds, False)

            Dim pt As Point = e.CellBounds.Location
            Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

            pt.X += offset
            pt.Y = 3

            If colName = "FanartPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 9)
            ElseIf colName = "NfoPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 11)
            ElseIf colName = "PosterPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 12)
            ElseIf colName = "HasSub" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 14)
            ElseIf colName = "Playcount" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 17)
            End If

            e.Handled = True

        End If

        'text fields
        If (colName = "Aired" OrElse colName = "Episode" OrElse colName = "Season" OrElse _
            colName = "Title") AndAlso e.RowIndex >= 0 Then
            If Convert.ToInt64(Me.dgvTVEpisodes.Item("idFile", e.RowIndex).Value) = -1 Then
                e.CellStyle.ForeColor = Color.Gray
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.LightGray
            ElseIf Convert.ToBoolean(Me.dgvTVEpisodes.Item("Mark", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Crimson
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Crimson
            ElseIf Convert.ToBoolean(Me.dgvTVEpisodes.Item("New", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Green
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Green
            Else
                e.CellStyle.ForeColor = Color.Black
                e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
            End If
        End If

        If e.ColumnIndex >= 2 AndAlso e.RowIndex >= 0 Then

            'background
            If Convert.ToInt64(Me.dgvTVEpisodes.Item("idFile", e.RowIndex).Value) = -1 Then
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.DarkGray
            ElseIf Convert.ToBoolean(Me.dgvTVEpisodes.Item("Lock", e.RowIndex).Value) Then
                e.CellStyle.BackColor = Color.LightSteelBlue
                e.CellStyle.SelectionBackColor = Color.DarkTurquoise
            Else
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
            End If

            'path fields
            If colName = "FanartPath" OrElse colName = "NfoPath" OrElse colName = "PosterPath" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                Me.ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString), 0, 1))
                e.Handled = True
            End If

            'playcount field
            If colName = "Playcount" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                Me.ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString) AndAlso Not e.Value.ToString = "0", 0, 1))
                e.Handled = True
            End If

            'boolean fields
            If colName = "HasSub" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                Me.ilColumnIcons.Draw(e.Graphics, pt, If(Convert.ToBoolean(e.Value), 0, 1))
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgvTVEpisodes_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvTVEpisodes.KeyDown
        'stop enter key from selecting next list item
        e.Handled = e.KeyCode = Keys.Enter
    End Sub

    Private Sub dgvTVEpisodes_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvTVEpisodes.KeyPress
        If StringUtils.AlphaNumericOnly(e.KeyChar) OrElse e.KeyChar = Convert.ToChar(Keys.Space) Then
            KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
            tmrKeyBuffer.Start()
            For Each drvRow As DataGridViewRow In Me.dgvTVEpisodes.Rows
                If drvRow.Cells("Title").Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                    drvRow.Selected = True
                    Me.dgvTVEpisodes.CurrentCell = drvRow.Cells("Title")
                    Exit For
                End If
            Next
        ElseIf e.KeyChar = Convert.ToChar(Keys.Enter) Then
            If Me.fScanner.IsBusy OrElse Me.bwMetaData.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwLoadEpInfo.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

            Dim indX As Integer = Me.dgvTVEpisodes.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(Me.dgvTVEpisodes.Item("idEpisode", indX).Value)
            Dim tmpDBTVEpisode As Database.DBElement = Master.DB.LoadTVEpisodeFromDB(ID, True)
            Edit_TVEpisode(tmpDBTVEpisode)
        End If
    End Sub

    Private Sub ShowEpisodeMenuItems(ByVal Visible As Boolean)
        Dim cMnu As ToolStripMenuItem
        Dim cSep As ToolStripSeparator

        If Visible Then
            For Each cMnuItem As Object In Me.cmnuEpisode.Items
                If TypeOf cMnuItem Is ToolStripMenuItem Then
                    DirectCast(cMnuItem, ToolStripMenuItem).Visible = True
                ElseIf TypeOf cMnuItem Is ToolStripSeparator Then
                    DirectCast(cMnuItem, ToolStripSeparator).Visible = True
                End If
            Next
            Me.cmnuEpisodeRemoveFromDisk.Visible = True
        Else
            For Each cMnuItem As Object In Me.cmnuEpisode.Items
                If TypeOf cMnuItem Is ToolStripMenuItem Then
                    cMnu = DirectCast(cMnuItem, ToolStripMenuItem)
                    If Not cMnu.Name = "RemoveEpToolStripMenuItem" AndAlso Not cMnu.Name = "cmnuEpTitle" Then
                        cMnu.Visible = False
                    End If
                ElseIf TypeOf cMnuItem Is ToolStripSeparator Then
                    cSep = DirectCast(cMnuItem, ToolStripSeparator)
                    If Not cSep.Name = "ToolStripSeparator6" Then
                        cSep.Visible = False
                    End If
                End If
                Me.cmnuEpisodeRemoveFromDisk.Visible = False
            Next
        End If
    End Sub

    Private Sub dgvTVEpisodes_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvTVEpisodes.MouseDown
        Dim hasMissing As Boolean = False

        If e.Button = Windows.Forms.MouseButtons.Right And Me.dgvTVEpisodes.RowCount > 0 Then
            Me.cmnuEpisode.Enabled = False

            Dim dgvHTI As DataGridView.HitTestInfo = dgvTVEpisodes.HitTest(e.X, e.Y)
            If dgvHTI.Type = DataGridViewHitTestType.Cell Then

                If Me.dgvTVEpisodes.SelectedRows.Count > 1 AndAlso Me.dgvTVEpisodes.Rows(dgvHTI.RowIndex).Selected Then
                    Me.cmnuEpisode.Enabled = True

                    For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                        If Convert.ToInt64(sRow.Cells("idFile").Value) = -1 Then
                            hasMissing = True
                            Exit For
                        End If
                    Next

                    Me.cmnuEpisodeTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                    If hasMissing Then
                        Me.ShowEpisodeMenuItems(False)
                    Else
                        Dim setMark As Boolean = False
                        Dim setLock As Boolean = False
                        Dim setWatched As Boolean = False

                        Me.ShowEpisodeMenuItems(True)

                        Me.ToolStripSeparator9.Visible = False
                        Me.cmnuEpisodeEdit.Visible = False
                        Me.ToolStripSeparator10.Visible = False
                        Me.cmnuEpisodeRescrape.Visible = False
                        Me.cmnuEpisodeChange.Visible = False
                        Me.ToolStripSeparator12.Visible = False
                        Me.cmnuEpisodeOpenFolder.Visible = False

                        For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                            'if any one item is set as unmarked, set menu to mark
                            'else they are all marked, so set menu to unmark
                            If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                                setMark = True
                                If setLock AndAlso setWatched Then Exit For
                            End If
                            'if any one item is set as unlocked, set menu to lock
                            'else they are all locked so set menu to unlock
                            If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                                setLock = True
                                If setMark AndAlso setWatched Then Exit For
                            End If
                            'if any one item is set as unwatched, set menu to watched
                            'else they are all watched so set menu to not watched
                            If String.IsNullOrEmpty(sRow.Cells("Playcount").Value.ToString) AndAlso sRow.Cells("Playcount").Value.ToString = "0" Then
                                setWatched = True
                                If setLock AndAlso setMark Then Exit For
                            End If
                        Next

                        Me.cmnuEpisodeMark.Text = If(setMark, Master.eLang.GetString(23, "Mark"), Master.eLang.GetString(107, "Unmark"))
                        Me.cmnuEpisodeLock.Text = If(setLock, Master.eLang.GetString(24, "Lock"), Master.eLang.GetString(108, "Unlock"))
                        Me.cmnuEpisodeWatched.Text = If(setWatched, Master.eLang.GetString(981, "Watched"), Master.eLang.GetString(980, "Not Watched"))
                    End If
                Else
                    cmnuEpisodeTitle.Text = String.Concat(">> ", Me.dgvTVEpisodes.Item("Title", dgvHTI.RowIndex).Value, " <<")

                    If Not Me.dgvTVEpisodes.Rows(dgvHTI.RowIndex).Selected OrElse Not Me.currList = 2 Then
                        Me.prevRow_TVEpisode = -1
                        Me.currList = 2
                        Me.dgvTVEpisodes.CurrentCell = Nothing
                        Me.dgvTVEpisodes.ClearSelection()
                        Me.dgvTVEpisodes.Rows(dgvHTI.RowIndex).Selected = True
                        Me.dgvTVEpisodes.CurrentCell = Me.dgvTVEpisodes.Item("Title", dgvHTI.RowIndex)
                    Else
                        Me.cmnuEpisode.Enabled = True
                    End If

                    If Convert.ToInt64(Me.dgvTVEpisodes.Item("idFile", dgvHTI.RowIndex).Value) = -1 Then hasMissing = True

                    If hasMissing Then
                        Me.ShowEpisodeMenuItems(False)
                    Else
                        Me.ShowEpisodeMenuItems(True)

                        Me.ToolStripSeparator9.Visible = True
                        Me.cmnuEpisodeEdit.Visible = True
                        Me.ToolStripSeparator10.Visible = True
                        Me.cmnuEpisodeRescrape.Visible = True
                        Me.cmnuEpisodeChange.Visible = True
                        Me.ToolStripSeparator12.Visible = True
                        Me.cmnuEpisodeOpenFolder.Visible = True

                        Me.cmnuEpisodeMark.Text = If(Convert.ToBoolean(Me.dgvTVEpisodes.Item("Mark", dgvHTI.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))
                        Me.cmnuEpisodeLock.Text = If(Convert.ToBoolean(Me.dgvTVEpisodes.Item("Lock", dgvHTI.RowIndex).Value), Master.eLang.GetString(108, "Unlock"), Master.eLang.GetString(24, "Lock"))
                        Me.cmnuEpisodeWatched.Text = If(Not String.IsNullOrEmpty(Me.dgvTVEpisodes.Item("Playcount", dgvHTI.RowIndex).Value.ToString) AndAlso Not Me.dgvTVEpisodes.Item("Playcount", dgvHTI.RowIndex).Value.ToString = "0", Master.eLang.GetString(980, "Not Watched"), Master.eLang.GetString(981, "Watched"))
                    End If

                End If
            Else
                Me.cmnuEpisode.Enabled = False
                Me.cmnuEpisodeTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
            End If
        End If
    End Sub

    Private Sub dgvTVEpisodes_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVEpisodes.Resize
        ResizeTVLists(3)
    End Sub

    Private Sub dgvTVEpisodes_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVEpisodes.Sorted
        Me.prevRow_TVEpisode = -1
        If Me.dgvTVEpisodes.RowCount > 0 Then
            Me.dgvTVEpisodes.CurrentCell = Nothing
            Me.dgvTVEpisodes.ClearSelection()
            Me.dgvTVEpisodes.Rows(0).Selected = True
            Me.dgvTVEpisodes.CurrentCell = Me.dgvTVEpisodes.Rows(0).Cells("Title")
        End If
    End Sub

    Private Sub dgvTVSeasons_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVSeasons.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim colName As String = Me.dgvTVSeasons.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If colName = "SeasonText" OrElse colName = "HasWatched" OrElse Not Master.eSettings.TVGeneralClickScrape Then
            If Not colName = "HasWatched" Then
                If Me.dgvTVSeasons.SelectedRows.Count > 0 Then
                    If Me.dgvTVSeasons.RowCount > 0 Then
                        If Me.dgvTVSeasons.SelectedRows.Count > 1 Then
                            Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvTVSeasons.SelectedRows.Count))
                        ElseIf Me.dgvTVSeasons.SelectedRows.Count = 1 Then
                            Me.SetStatus(Me.dgvTVSeasons.SelectedRows(0).Cells("SeasonText").Value.ToString)
                        End If
                    End If
                    Me.currRow_TVSeason = Me.dgvTVSeasons.SelectedRows(0).Index
                    If Not Me.currList = 1 Then
                        Me.currList = 1
                        Me.prevRow_TVSeason = -1
                        Me.SelectRow_TVSeason(Me.dgvTVSeasons.SelectedRows(0).Index)
                    End If
                End If
            Else
                SetWatchedStatus_TVSeason()
            End If

        ElseIf Master.eSettings.TVGeneralClickScrape AndAlso _
            (colName = "BannerPath" OrElse colName = "FanartPath" OrElse _
             colName = "LandscapePath" OrElse colName = "PosterPath") AndAlso _
            Not bwTVSeasonScraper.IsBusy Then
            Dim season As Int32 = CType(Me.dgvTVSeasons.Rows(e.RowIndex).Cells("idSeason").Value, Int32)
            Dim objCell As DataGridViewCell = CType(Me.dgvTVSeasons.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCell)

            'EMM not able to scrape subtitles yet.
            'So don't set status for it, but leave the option open for the future.
            Me.dgvTVSeasons.ClearSelection()
            Me.dgvTVSeasons.Rows(objCell.RowIndex).Selected = True
            Me.currRow_TVSeason = objCell.RowIndex

            Dim ScrapeModifier As New Structures.ScrapeModifier
            Select Case colName
                Case "BannerPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonBanner, True)
                Case "FanartPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonFanart, True)
                Case "LandscapePath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonLandscape, True)
                Case "PosterPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonPoster, True)
            End Select
            If Master.eSettings.TVGeneralClickScrapeAsk Then
                CreateScrapeList_TVSeason(Enums.ScrapeType.SelectedAsk, Master.DefaultOptions_TV, ScrapeModifier)
            Else
                CreateScrapeList_TVSeason(Enums.ScrapeType.SelectedAuto, Master.DefaultOptions_TV, ScrapeModifier)
            End If
        End If
    End Sub

    Private Sub dgvTVSeasons_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVSeasons.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub

        If Me.fScanner.IsBusy OrElse Me.bwMetaData.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy OrElse Me.bwLoadEpInfo.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

        Dim indX As Integer = Me.dgvTVSeasons.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item("idSeason", indX).Value)
        Dim tmpDBTVSeason As Database.DBElement = Master.DB.LoadTVSeasonFromDB(ID, True)
        Edit_TVSeason(tmpDBTVSeason)
    End Sub

    Private Sub dgvTVSeasons_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVSeasons.CellEnter
        Dim currTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
        If Not currTag.ContentType = Enums.ContentType.TV OrElse Not Me.currList = 1 Then Return

        Me.tmrWait_TVShow.Stop()
        Me.tmrWait_Movie.Stop()
        Me.tmrWait_MovieSet.Stop()
        Me.tmrWait_TVEpisode.Stop()
        Me.tmrWait_TVSeason.Stop()
        Me.tmrLoad_TVShow.Stop()
        Me.tmrLoad_Movie.Stop()
        Me.tmrLoad_MovieSet.Stop()
        Me.tmrLoad_TVEpisode.Stop()
        Me.tmrLoad_TVSeason.Stop()

        Me.currRow_TVSeason = e.RowIndex
        Me.tmrWait_TVSeason.Start()
    End Sub

    Private Sub dgvTVSeasons_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVSeasons.CellMouseEnter
        Dim colName As String = Me.dgvTVSeasons.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        Me.dgvTVSeasons.ShowCellToolTips = True

        If colName = "HasWatched" AndAlso e.RowIndex >= 0 AndAlso Not CInt(Me.dgvTVSeasons.Rows(e.RowIndex).Cells("Season").Value) = 999 Then
            oldStatus = GetStatus()
            Me.SetStatus(Master.eLang.GetString(885, "Change Watched Status"))
        ElseIf (colName = "BannerPath" OrElse colName = "FanartPath" OrElse _
            colName = "LandscapePath" OrElse colName = "PosterPath") AndAlso e.RowIndex >= 0 Then
            Me.dgvTVSeasons.ShowCellToolTips = False

            If Master.eSettings.TVGeneralClickScrape AndAlso Not bwTVSeasonScraper.IsBusy Then
                oldStatus = GetStatus()
                Dim seasonTitle As String = Me.dgvTVSeasons.Rows(e.RowIndex).Cells("SeasonText").Value.ToString
                Dim scrapeFor As String = String.Empty
                Dim scrapeType As String = String.Empty
                Select Case colName
                    Case "BannerPath"
                        scrapeFor = Master.eLang.GetString(1060, "Banner Only")
                    Case "FanartPath"
                        scrapeFor = Master.eLang.GetString(73, "Fanart Only")
                    Case "LandscapePath"
                        scrapeFor = Master.eLang.GetString(1061, "Landscape Only")
                    Case "PosterPath"
                        scrapeFor = Master.eLang.GetString(72, "Poster Only")
                End Select

                If Master.eSettings.TVGeneralClickScrapeAsk Then
                    scrapeType = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
                Else
                    scrapeType = Master.eLang.GetString(69, "Automatic (Force Best Match)")
                End If

                Me.SetStatus(String.Format("Scrape ""{0}"" for {1} - {2}", seasonTitle, scrapeFor, scrapeType))
            Else
                oldStatus = String.Empty
            End If
        Else
            oldStatus = String.Empty
        End If
    End Sub

    Private Sub dgvTVSeasons_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVSeasons.CellMouseLeave
        If Not String.IsNullOrEmpty(oldStatus) Then Me.SetStatus(oldStatus)
    End Sub

    Private Sub dgvTVSeasons_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvTVSeasons.CellPainting
        Dim colName As String = Me.dgvTVSeasons.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not Me.dgvTVSeasons.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If

        'icons for column header
        If (colName = "BannerPath" OrElse colName = "FanartPath" OrElse colName = "LandscapePath" OrElse _
            colName = "PosterPath" OrElse colName = "HasWatched") AndAlso e.RowIndex = -1 Then
            e.PaintBackground(e.ClipBounds, False)

            Dim pt As Point = e.CellBounds.Location
            Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

            pt.X += offset
            pt.Y = 3

            If colName = "BannerPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 2)
            ElseIf colName = "FanartPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 9)
            ElseIf colName = "LandscapePath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 10)
            ElseIf colName = "PosterPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 12)
            ElseIf colName = "HasWatched" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 17)
            End If

            e.Handled = True

        End If

        'text fields
        If (colName = "SeasonText" OrElse colName = "Episodes") AndAlso e.RowIndex >= 0 Then
            If Convert.ToBoolean(Me.dgvTVSeasons.Item("Missing", e.RowIndex).Value) AndAlso Not CInt(Me.dgvTVSeasons.Item("Season", e.RowIndex).Value) = 999 Then
                e.CellStyle.ForeColor = Color.Gray
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.LightGray
            ElseIf Convert.ToBoolean(Me.dgvTVSeasons.Item("Mark", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Crimson
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Crimson
            ElseIf Convert.ToBoolean(Me.dgvTVSeasons.Item("New", e.RowIndex).Value) OrElse _
                Not String.IsNullOrEmpty(Me.dgvTVSeasons.Item("NewEpisodes", e.RowIndex).Value.ToString) AndAlso CInt(Me.dgvTVSeasons.Item("NewEpisodes", e.RowIndex).Value) > 0 Then
                e.CellStyle.ForeColor = Color.Green
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Green
            Else
                e.CellStyle.ForeColor = Color.Black
                e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
            End If
        End If

        If e.ColumnIndex >= 3 AndAlso e.RowIndex >= 0 Then

            'background
            If Convert.ToBoolean(Me.dgvTVSeasons.Item("Lock", e.RowIndex).Value) Then
                e.CellStyle.BackColor = Color.LightSteelBlue
                e.CellStyle.SelectionBackColor = Color.DarkTurquoise
            Else
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
            End If

            'path fields
            If colName = "BannerPath" OrElse colName = "FanartPath" OrElse colName = "LandscapePath" OrElse _
                colName = "NfoPath" OrElse colName = "PosterPath" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                Me.ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString), 0, 1))
                e.Handled = True
            End If

            'boolean fields
            If colName = "HasWatched" AndAlso Not CInt(Me.dgvTVSeasons.Item("Season", e.RowIndex).Value) = 999 Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                Me.ilColumnIcons.Draw(e.Graphics, pt, If(Convert.ToBoolean(e.Value), 0, 1))
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgvTVSeasons_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvTVSeasons.KeyDown
        'stop enter key from selecting next list item
        e.Handled = e.KeyCode = Keys.Enter
    End Sub

    Private Sub dgvTVSeasons_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvTVSeasons.KeyPress
        If StringUtils.AlphaNumericOnly(e.KeyChar) OrElse e.KeyChar = Convert.ToChar(Keys.Space) Then
            KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
            tmrKeyBuffer.Start()
            For Each drvRow As DataGridViewRow In Me.dgvTVSeasons.Rows
                If drvRow.Cells("SeasonText").Value.ToString.StartsWith(KeyBuffer) Then
                    drvRow.Selected = True
                    Me.dgvTVSeasons.CurrentCell = drvRow.Cells("SeasonText")
                    Exit For
                End If
            Next
        ElseIf e.KeyChar = Convert.ToChar(Keys.Enter) Then
            If Me.fScanner.IsBusy OrElse Me.bwMetaData.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy OrElse Me.bwLoadEpInfo.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

            Dim indX As Integer = Me.dgvTVSeasons.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item("idSeason", indX).Value)
            Dim tmpDBTVSeason As Database.DBElement = Master.DB.LoadTVSeasonFromDB(ID, True)
            Edit_TVSeason(tmpDBTVSeason)
        End If
    End Sub

    Private Sub dgvTVSeasons_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvTVSeasons.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right And Me.dgvTVSeasons.RowCount > 0 Then

            Me.cmnuSeason.Enabled = False

            Dim dgvHTI As DataGridView.HitTestInfo = dgvTVSeasons.HitTest(e.X, e.Y)
            If dgvHTI.Type = DataGridViewHitTestType.Cell Then

                If Me.dgvTVSeasons.SelectedRows.Count > 1 AndAlso Me.dgvTVSeasons.Rows(dgvHTI.RowIndex).Selected Then
                    Dim setMark As Boolean = False
                    Dim setLock As Boolean = False
                    Dim setWatched As Boolean = False

                    Me.cmnuSeason.Enabled = True
                    Me.cmnuSeasonTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")
                    Me.ToolStripSeparator16.Visible = False
                    Me.cmnuSeasonEdit.Visible = False
                    Me.ToolStripSeparator14.Visible = False
                    Me.cmnuSeasonRescrape.Visible = False
                    Me.ToolStripSeparator15.Visible = False
                    Me.cmnuSeasonOpenFolder.Visible = False

                    For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                        'if any one item is set as unmarked, set menu to mark
                        'else they are all marked, so set menu to unmark
                        If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                            setMark = True
                            If setLock AndAlso setWatched Then Exit For
                        End If
                        'if any one item is set as unlocked, set menu to lock
                        'else they are all locked so set menu to unlock
                        If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                            setLock = True
                            If setMark AndAlso setWatched Then Exit For
                        End If
                        'if any one item is set as unwatched, set menu to watched
                        'else they are all watched so set menu to not watched
                        If Not CInt(sRow.Cells("Season").Value) = 999 AndAlso Not Convert.ToBoolean(sRow.Cells("HasWatched").Value) Then
                            setWatched = True
                            If setLock AndAlso setMark Then Exit For
                        End If
                    Next

                    Me.cmnuSeasonMark.Text = If(setMark, Master.eLang.GetString(23, "Mark"), Master.eLang.GetString(107, "Unmark"))
                    Me.cmnuSeasonLock.Text = If(setLock, Master.eLang.GetString(24, "Lock"), Master.eLang.GetString(108, "Unlock"))
                    Me.cmnuSeasonWatched.Text = If(setWatched, Master.eLang.GetString(981, "Watched"), Master.eLang.GetString(980, "Not Watched"))

                Else
                    Me.ToolStripSeparator16.Visible = True
                    Me.cmnuSeasonEdit.Visible = True
                    Me.ToolStripSeparator14.Visible = True
                    Me.cmnuSeasonRescrape.Visible = True
                    Me.ToolStripSeparator15.Visible = True
                    Me.cmnuSeasonOpenFolder.Visible = True
                    If CInt(dgvTVSeasons.Item("Season", dgvHTI.RowIndex).Value) = 999 Then
                        Me.cmnuSeasonWatched.Enabled = False
                    Else
                        Me.cmnuSeasonWatched.Enabled = True
                    End If

                    Me.cmnuSeasonTitle.Text = String.Concat(">> ", Me.dgvTVSeasons.Item("SeasonText", dgvHTI.RowIndex).Value, " <<")
                    Me.cmnuSeasonMark.Text = If(Convert.ToBoolean(Me.dgvTVSeasons.Item("Mark", dgvHTI.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))
                    Me.cmnuSeasonLock.Text = If(Convert.ToBoolean(Me.dgvTVSeasons.Item("Lock", dgvHTI.RowIndex).Value), Master.eLang.GetString(108, "Unlock"), Master.eLang.GetString(24, "Lock"))
                    If Not CInt(dgvTVSeasons.Item("Season", dgvHTI.RowIndex).Value) = 999 Then Me.cmnuSeasonWatched.Text = If(Convert.ToBoolean(Me.dgvTVSeasons.Item("HasWatched", dgvHTI.RowIndex).Value), Master.eLang.GetString(980, "Not Watched"), Master.eLang.GetString(981, "Watched"))
                    Me.cmnuSeasonEdit.Enabled = Convert.ToInt32(Me.dgvTVSeasons.Item("Season", dgvHTI.RowIndex).Value) >= 0

                    If Not Me.dgvTVSeasons.Rows(dgvHTI.RowIndex).Selected OrElse Not Me.currList = 1 Then
                        Me.prevRow_TVSeason = -1
                        Me.currList = 1
                        Me.dgvTVSeasons.CurrentCell = Nothing
                        Me.dgvTVSeasons.ClearSelection()
                        Me.dgvTVSeasons.Rows(dgvHTI.RowIndex).Selected = True
                        Me.dgvTVSeasons.CurrentCell = Me.dgvTVSeasons.Item("SeasonText", dgvHTI.RowIndex)
                    Else
                        Me.cmnuSeason.Enabled = True
                    End If
                End If
            Else
                Me.cmnuSeason.Enabled = False
                Me.cmnuSeasonTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
            End If
        End If
    End Sub

    Private Sub dgvTVSeasons_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVSeasons.Sorted
        Me.prevRow_TVSeason = -1
        If Me.dgvTVSeasons.RowCount > 0 Then
            Me.dgvTVSeasons.CurrentCell = Nothing
            Me.dgvTVSeasons.ClearSelection()
            Me.dgvTVSeasons.Rows(0).Selected = True
            Me.dgvTVSeasons.CurrentCell = Me.dgvTVSeasons.Rows(0).Cells("SeasonText")
        End If
    End Sub

    Private Sub dgvTVSeason_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVSeasons.Resize
        ResizeTVLists(2)
    End Sub

    Private Sub dgvTVShows_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVShows.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim colName As String = Me.dgvTVShows.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If colName = "ListTitle" OrElse colName = "HasWatched" OrElse Not Master.eSettings.TVGeneralClickScrape Then
            If Not colName = "HasWatched" Then
                If Me.dgvTVShows.SelectedRows.Count > 0 Then
                    If Me.dgvTVShows.RowCount > 0 Then
                        If Me.dgvTVShows.SelectedRows.Count > 1 Then
                            Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvTVShows.SelectedRows.Count))
                        ElseIf Me.dgvTVShows.SelectedRows.Count = 1 Then
                            Me.SetStatus(Me.dgvTVShows.SelectedRows(0).Cells("TVShowPath").Value.ToString)
                        End If
                    End If

                    Me.currRow_TVShow = Me.dgvTVShows.SelectedRows(0).Index
                    If Not Me.currList = 0 Then
                        Me.currList = 0
                        Me.prevRow_TVShow = -1
                        Me.SelectRow_TVShow(Me.dgvTVShows.SelectedRows(0).Index)
                    End If
                End If
            Else
                SetWatchedStatus_TVShow()
            End If

        ElseIf Master.eSettings.TVGeneralClickScrape AndAlso _
            (colName = "BannerPath" OrElse colName = "CharacterArtPath" OrElse colName = "ClearArtPath" OrElse _
            colName = "ClearLogoPath" OrElse colName = "EFanartsPath" OrElse colName = "FanartPath" OrElse _
            colName = "LandscapePath" OrElse colName = "NfoPath" OrElse colName = "PosterPath" OrElse _
            colName = "ThemePath") AndAlso Not bwTVScraper.IsBusy Then
            Dim tvshow As Int32 = CType(Me.dgvTVShows.Rows(e.RowIndex).Cells("idShow").Value, Int32)
            Dim objCell As DataGridViewCell = CType(Me.dgvTVShows.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCell)

            'EMM not able to scrape subtitles yet.
            'So don't set status for it, but leave the option open for the future.
            Me.dgvTVShows.ClearSelection()
            Me.dgvTVShows.Rows(objCell.RowIndex).Selected = True
            Me.currRow_TVShow = objCell.RowIndex

            Dim ScrapeModifier As New Structures.ScrapeModifier
            Select Case colName
                Case "BannerPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainBanner, True)
                Case "CharacterArtPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainCharacterArt, True)
                Case "ClearArtPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearArt, True)
                Case "ClearLogoPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearLogo, True)
                Case "EFanartsPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainExtrafanarts, True)
                Case "FanartPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainFanart, True)
                Case "LandscapePath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainLandscape, True)
                Case "NfoPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
                Case "PosterPath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainPoster, True)
                Case "ThemePath"
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainTheme, True)
            End Select
            If Master.eSettings.TVGeneralClickScrapeAsk Then
                CreateScrapeList_TV(Enums.ScrapeType.SelectedAsk, Master.DefaultOptions_TV, ScrapeModifier)
            Else
                CreateScrapeList_TV(Enums.ScrapeType.SelectedAuto, Master.DefaultOptions_TV, ScrapeModifier)
            End If
        End If
    End Sub

    Private Sub dgvTVShows_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVShows.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub

        If Me.fScanner.IsBusy OrElse Me.bwMetaData.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy OrElse Me.bwLoadEpInfo.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

        Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(Me.dgvTVShows.Item("idShow", indX).Value)
        Dim tmpDBTVShow As Database.DBElement = Master.DB.LoadTVShowFromDB(ID, True, False)
        Edit_TVShow(tmpDBTVShow)
    End Sub

    Private Sub dgvTVShows_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVShows.CellEnter
        Dim currTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
        If Not currTag.ContentType = Enums.ContentType.TV OrElse Not Me.currList = 0 Then Return

        Me.tmrWait_Movie.Stop()
        Me.tmrWait_MovieSet.Stop()
        Me.tmrWait_TVSeason.Stop()
        Me.tmrWait_TVEpisode.Stop()
        Me.tmrWait_TVShow.Stop()
        Me.tmrLoad_Movie.Stop()
        Me.tmrLoad_MovieSet.Stop()
        Me.tmrLoad_TVSeason.Stop()
        Me.tmrLoad_TVEpisode.Stop()
        Me.tmrLoad_TVShow.Stop()

        Me.currRow_TVShow = e.RowIndex
        Me.tmrWait_TVShow.Start()
    End Sub

    Private Sub dgvTVShows_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVShows.CellMouseEnter
        Dim colName As String = Me.dgvTVShows.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        Me.dgvTVShows.ShowCellToolTips = True

        If colName = "HasWatched" AndAlso e.RowIndex >= 0 Then
            oldStatus = GetStatus()
            Me.SetStatus(Master.eLang.GetString(885, "Change Watched Status"))
        ElseIf (colName = "BannerPath" OrElse colName = "CharacterArtPath" OrElse colName = "ClearArtPath" OrElse _
            colName = "ClearLogoPath" OrElse colName = "EFanartsPath" OrElse colName = "FanartPath" OrElse _
            colName = "LandscapePath" OrElse colName = "NfoPath" OrElse colName = "PosterPath" OrElse _
            colName = "ThemePath") AndAlso e.RowIndex >= 0 Then
            Me.dgvTVShows.ShowCellToolTips = False

            If Master.eSettings.TVGeneralClickScrape AndAlso Not bwTVScraper.IsBusy Then
                oldStatus = GetStatus()
                Dim tvshowTitle As String = Me.dgvTVShows.Rows(e.RowIndex).Cells("Title").Value.ToString
                Dim scrapeFor As String = String.Empty
                Dim scrapeType As String = String.Empty
                Select Case colName
                    Case "BannerPath"
                        scrapeFor = Master.eLang.GetString(1060, "Banner Only")
                    Case "CharacterArtPath"
                        scrapeFor = Master.eLang.GetString(1121, "CharacterArt Only")
                    Case "ClearArtPath"
                        scrapeFor = Master.eLang.GetString(1122, "ClearArt Only")
                    Case "ClearLogoPath"
                        scrapeFor = Master.eLang.GetString(1123, "ClearLogo Only")
                    Case "EFanartsPath"
                        scrapeFor = Master.eLang.GetString(975, "Extrafanarts Only")
                    Case "FanartPath"
                        scrapeFor = Master.eLang.GetString(73, "Fanart Only")
                    Case "LandscapePath"
                        scrapeFor = Master.eLang.GetString(1061, "Landscape Only")
                    Case "NfoPath"
                        scrapeFor = Master.eLang.GetString(71, "NFO Only")
                    Case "PosterPath"
                        scrapeFor = Master.eLang.GetString(72, "Poster Only")
                    Case "ThemePath"
                        scrapeFor = Master.eLang.GetString(1125, "Theme Only")
                End Select

                If Master.eSettings.TVGeneralClickScrapeAsk Then
                    scrapeType = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
                Else
                    scrapeType = Master.eLang.GetString(69, "Automatic (Force Best Match)")
                End If

                Me.SetStatus(String.Format("Scrape ""{0}"" for {1} - {2}", tvshowTitle, scrapeFor, scrapeType))
            Else
                oldStatus = String.Empty
            End If
        Else
            oldStatus = String.Empty
        End If
    End Sub

    Private Sub dgvTVShows_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTVShows.CellMouseLeave
        If Not String.IsNullOrEmpty(oldStatus) Then Me.SetStatus(oldStatus)
    End Sub

    Private Sub dgvTVShows_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvTVShows.CellPainting
        Dim colName As String = Me.dgvTVShows.Columns(e.ColumnIndex).Name
        If String.IsNullOrEmpty(colName) Then
            Return
        End If

        If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not Me.dgvTVShows.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If

        'icons for column header
        If (colName = "BannerPath" OrElse colName = "CharacterArtPath" OrElse colName = "ClearArtPath" OrElse _
            colName = "ClearLogoPath" OrElse colName = "EFanartsPath" OrElse colName = "FanartPath" OrElse _
            colName = "LandscapePath" OrElse colName = "NfoPath" OrElse colName = "PosterPath" OrElse _
            colName = "ThemePath" OrElse colName = "HasWatched") AndAlso e.RowIndex = -1 Then
            e.PaintBackground(e.ClipBounds, False)

            Dim pt As Point = e.CellBounds.Location
            Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

            pt.X += offset
            pt.Y = 3

            If colName = "BannerPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 2)
            ElseIf colName = "CharacterArtPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 3)
            ElseIf colName = "ClearArtPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 4)
            ElseIf colName = "ClearLogoPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 5)
            ElseIf colName = "EFanartsPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 7)
            ElseIf colName = "FanartPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 9)
            ElseIf colName = "LandscapePath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 10)
            ElseIf colName = "NfoPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 11)
            ElseIf colName = "PosterPath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 12)
            ElseIf colName = "ThemePath" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 15)
            ElseIf colName = "HasWatched" Then
                Me.ilColumnIcons.Draw(e.Graphics, pt, 17)
            End If

            e.Handled = True

        End If

        'text fields
        If (colName = "ListTitle" OrElse colName = "Status") AndAlso e.RowIndex >= 0 Then
            If Convert.ToBoolean(Me.dgvTVShows.Item("Mark", e.RowIndex).Value) Then
                e.CellStyle.ForeColor = Color.Crimson
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Crimson
            ElseIf Convert.ToBoolean(Me.dgvTVShows.Item("New", e.RowIndex).Value) OrElse _
                Not String.IsNullOrEmpty(Me.dgvTVShows.Item("NewEpisodes", e.RowIndex).Value.ToString) AndAlso CInt(Me.dgvTVShows.Item("NewEpisodes", e.RowIndex).Value) > 0 Then
                e.CellStyle.ForeColor = Color.Green
                e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                e.CellStyle.SelectionForeColor = Color.Green
            Else
                e.CellStyle.ForeColor = Color.Black
                e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
            End If
        End If

        If e.ColumnIndex >= 1 AndAlso e.RowIndex >= 0 Then

            'background
            If Convert.ToBoolean(Me.dgvTVShows.Item("Lock", e.RowIndex).Value) Then
                e.CellStyle.BackColor = Color.LightSteelBlue
                e.CellStyle.SelectionBackColor = Color.DarkTurquoise
            Else
                e.CellStyle.BackColor = Color.White
                e.CellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight)
            End If

            'path fields
            If colName = "BannerPath" OrElse colName = "CharacterArtPath" OrElse colName = "ClearArtPath" OrElse _
                colName = "ClearLogoPath" OrElse colName = "EFanartsPath" OrElse colName = "FanartPath" OrElse _
                colName = "LandscapePath" OrElse colName = "NfoPath" OrElse colName = "PosterPath" OrElse _
                colName = "ThemePath" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                Me.ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString), 0, 1))
                e.Handled = True
            End If

            'boolean fields
            If colName = "HasWatched" Then
                e.PaintBackground(e.ClipBounds, True)

                Dim pt As Point = e.CellBounds.Location
                Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - Me.ilColumnIcons.ImageSize.Width) / 2)

                pt.X += offset
                pt.Y = e.CellBounds.Top + 3
                Me.ilColumnIcons.Draw(e.Graphics, pt, If(Convert.ToBoolean(e.Value), 0, 1))
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgvTVShows_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvTVShows.KeyDown
        'stop enter key from selecting next list item
        e.Handled = (e.KeyCode = Keys.Enter)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.S Then txtSearchShows.Focus()
    End Sub

    Private Sub dgvTVShows_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvTVShows.KeyPress
        If StringUtils.AlphaNumericOnly(e.KeyChar) OrElse e.KeyChar = Convert.ToChar(Keys.Space) Then
            KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
            tmrKeyBuffer.Start()
            For Each drvRow As DataGridViewRow In Me.dgvTVShows.Rows
                If drvRow.Cells("ListTitle").Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                    drvRow.Selected = True
                    Me.dgvTVShows.CurrentCell = drvRow.Cells("ListTitle")
                    Exit For
                End If
            Next
        ElseIf e.KeyChar = Convert.ToChar(Keys.Enter) Then
            If Me.fScanner.IsBusy OrElse Me.bwMetaData.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy OrElse Me.bwLoadEpInfo.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwCleanDB.IsBusy Then Return

            Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(Me.dgvTVShows.Item("idShow", indX).Value)
            Dim tmpDBTVShow As Database.DBElement = Master.DB.LoadTVShowFromDB(ID, True, False)
            Edit_TVShow(tmpDBTVShow)
        End If
    End Sub

    Private Sub dgvTVShows_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgvTVShows.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right And Me.dgvTVShows.RowCount > 0 Then

            Me.cmnuShow.Enabled = False

            Dim dgvHTI As DataGridView.HitTestInfo = dgvTVShows.HitTest(e.X, e.Y)
            If dgvHTI.Type = DataGridViewHitTestType.Cell Then

                If Me.dgvTVShows.SelectedRows.Count > 1 AndAlso Me.dgvTVShows.Rows(dgvHTI.RowIndex).Selected Then
                    Dim setMark As Boolean = False
                    Dim setLock As Boolean = False
                    Dim setWatched As Boolean = False

                    Me.cmnuShow.Enabled = True
                    Me.cmnuShowTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")
                    Me.ToolStripSeparator8.Visible = False
                    Me.cmnuShowEdit.Visible = False
                    Me.ToolStripSeparator7.Visible = False
                    ' Me.cmnuRescrapeShow.Visible = False
                    Me.cmnuShowChange.Visible = False
                    Me.cmnuShowOpenFolder.Visible = False
                    Me.ToolStripSeparator20.Visible = False

                    For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                        'if any one item is set as unmarked, set menu to mark
                        'else they are all marked, so set menu to unmark
                        If Not Convert.ToBoolean(sRow.Cells("Mark").Value) Then
                            setMark = True
                            If setLock AndAlso setWatched Then Exit For
                        End If
                        'if any one item is set as unlocked, set menu to lock
                        'else they are all locked so set menu to unlock
                        If Not Convert.ToBoolean(sRow.Cells("Lock").Value) Then
                            setLock = True
                            If setMark AndAlso setWatched Then Exit For
                        End If
                        'if any one item is set as unwatched, set menu to watched
                        'else they are all watched so set menu to not watched
                        If Not Convert.ToBoolean(sRow.Cells("HasWatched").Value) Then
                            setWatched = True
                            If setLock AndAlso setMark Then Exit For
                        End If
                    Next

                    Me.cmnuShowMark.Text = If(setMark, Master.eLang.GetString(23, "Mark"), Master.eLang.GetString(107, "Unmark"))
                    Me.cmnuShowLock.Text = If(setLock, Master.eLang.GetString(24, "Lock"), Master.eLang.GetString(108, "Unlock"))
                    Me.cmnuShowWatched.Text = If(setWatched, Master.eLang.GetString(981, "Watched"), Master.eLang.GetString(980, "Not Watched"))

                    Me.cmnuShowLanguageLanguages.Items.Insert(0, Master.eLang.GetString(1199, "Select Language..."))
                    Me.cmnuShowLanguageLanguages.SelectedItem = Master.eLang.GetString(1199, "Select Language...")
                    Me.cmnuShowLanguageSet.Enabled = False
                Else
                    Me.ToolStripSeparator8.Visible = True
                    Me.cmnuShowEdit.Visible = True
                    Me.ToolStripSeparator7.Visible = True
                    Me.cmnuShowRefresh.Visible = True
                    Me.cmnuShowRescrape.Visible = True
                    Me.cmnuShowChange.Visible = True
                    Me.cmnuShowOpenFolder.Visible = True
                    Me.ToolStripSeparator20.Visible = True

                    Me.cmnuShowTitle.Text = String.Concat(">> ", Me.dgvTVShows.Item("ListTitle", dgvHTI.RowIndex).Value, " <<")
                    Me.cmnuShowMark.Text = If(Convert.ToBoolean(Me.dgvTVShows.Item("Mark", dgvHTI.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))
                    Me.cmnuShowLock.Text = If(Convert.ToBoolean(Me.dgvTVShows.Item("Lock", dgvHTI.RowIndex).Value), Master.eLang.GetString(108, "Unlock"), Master.eLang.GetString(24, "Lock"))
                    Me.cmnuShowWatched.Text = If(Convert.ToBoolean(Me.dgvTVShows.Item("HasWatched", dgvHTI.RowIndex).Value), Master.eLang.GetString(980, "Not Watched"), Master.eLang.GetString(981, "Watched"))

                    If Not Me.dgvTVShows.Rows(dgvHTI.RowIndex).Selected OrElse Not Me.currList = 0 Then
                        Me.prevRow_TVShow = -1
                        Me.currList = 0
                        Me.dgvTVShows.CurrentCell = Nothing
                        Me.dgvTVShows.ClearSelection()
                        Me.dgvTVShows.Rows(dgvHTI.RowIndex).Selected = True
                        Me.dgvTVShows.CurrentCell = Me.dgvTVShows.Item("ListTitle", dgvHTI.RowIndex)
                    Else
                        Me.cmnuShow.Enabled = True
                    End If

                    Dim Lang As String = CStr(Me.dgvTVShows.Item("Language", dgvHTI.RowIndex).Value)
                    Me.cmnuShowLanguageLanguages.Text = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = Lang).name
                    Me.cmnuShowLanguageSet.Enabled = False
                End If
            Else
                Me.cmnuShow.Enabled = False
                Me.cmnuShowTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
            End If
        End If
    End Sub

    Private Sub dgvTVShows_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVShows.Resize
        ResizeTVLists(1)
    End Sub

    Private Sub dgvTVShows_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgvTVShows.RowsRemoved
        Me.SetTVCount()
    End Sub

    Private Sub dgvTVShows_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgvTVShows.RowsAdded
        Me.SetTVCount()
    End Sub

    Private Sub dgvTVShows_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTVShows.Sorted
        Me.prevRow_TVShow = -1
        If Me.dgvTVShows.RowCount > 0 Then
            Me.dgvTVShows.CurrentCell = Nothing
            Me.dgvTVShows.ClearSelection()
            Me.dgvTVShows.Rows(0).Selected = True
            Me.dgvTVShows.CurrentCell = Me.dgvTVShows.Rows(0).Cells("ListTitle")
        End If

        'If Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "Year" AndAlso Me.dgvTVShows.SortOrder = 1 Then
        '    Me.btnFilterSortYear_Shows.Tag = "ASC"
        '    Me.btnFilterSortYear_Shows.Image = My.Resources.asc
        'ElseIf Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "Year" AndAlso Me.dgvTVShows.SortOrder = 2 Then
        '    Me.btnFilterSortYear_Shows.Tag = "DESC"
        '    Me.btnFilterSortYear_Shows.Image = My.Resources.desc
        'Else
        '    Me.btnFilterSortYear_Shows.Tag = String.Empty
        '    Me.btnFilterSortYear_Shows.Image = Nothing
        'End If

        'If Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "Rating" AndAlso Me.dgvTVShows.SortOrder = 1 Then
        '    Me.btnFilterSortRating_Shows.Tag = "ASC"
        '    Me.btnFilterSortRating_Shows.Image = My.Resources.asc
        'ElseIf Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "Rating" AndAlso Me.dgvTVShows.SortOrder = 2 Then
        '    Me.btnFilterSortRating_Shows.Tag = "DESC"
        '    Me.btnFilterSortRating_Shows.Image = My.Resources.desc
        'Else
        '    Me.btnFilterSortRating_Shows.Tag = String.Empty
        '    Me.btnFilterSortRating_Shows.Image = Nothing
        'End If

        If Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "SortedTitle" AndAlso Me.dgvTVShows.SortOrder = 1 Then
            Me.btnFilterSortTitle_Shows.Tag = "ASC"
            Me.btnFilterSortTitle_Shows.Image = My.Resources.asc
        ElseIf Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "SortedTitle" AndAlso Me.dgvTVShows.SortOrder = 2 Then
            Me.btnFilterSortTitle_Shows.Tag = "DESC"
            Me.btnFilterSortTitle_Shows.Image = My.Resources.desc
        Else
            Me.btnFilterSortTitle_Shows.Tag = String.Empty
            Me.btnFilterSortTitle_Shows.Image = Nothing
        End If

        'If Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "DateAdded" AndAlso Me.dgvTVShows.SortOrder = 1 Then
        '    Me.btnFilterSortDateAdded_Shows.Tag = "ASC"
        '    Me.btnFilterSortDateAdded_Shows.Image = My.Resources.asc
        'ElseIf Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "DateAdded" AndAlso Me.dgvTVShows.SortOrder = 2 Then
        '    Me.btnFilterSortDateAdded_Shows.Tag = "DESC"
        '    Me.btnFilterSortDateAdded_Shows.Image = My.Resources.desc
        'Else
        '    Me.btnFilterSortDateAdded_Shows.Tag = String.Empty
        '    Me.btnFilterSortDateAdded_Shows.Image = Nothing
        'End If

        'If Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "DateModified" AndAlso Me.dgvTVShows.SortOrder = 1 Then
        '    Me.btnFilterSortDateModified_Shows.Tag = "ASC"
        '    Me.btnFilterSortDateModified_Shows.Image = My.Resources.asc
        'ElseIf Me.dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "DateModified" AndAlso Me.dgvTVShows.SortOrder = 2 Then
        '    Me.btnFilterSortDateModified_Shows.Tag = "DESC"
        '    Me.btnFilterSortDateModified_Shows.Image = My.Resources.desc
        'Else
        '    Me.btnFilterSortDateModified_Shows.Tag = String.Empty
        '    Me.btnFilterSortDateModified_Shows.Image = Nothing
        'End If

        Me.SaveFilter_Shows()
    End Sub

    Private Sub DonateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainDonate.Click
        If Master.isWindows Then
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=VWVJCUV3KAUX2&lc=CH&item_name=Ember%2dTeam%3a%20DanCooper%2c%20m%2esavazzi%20%26%20Cocotus&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted")
        Else
            Using Explorer As New Process
                Explorer.StartInfo.FileName = "xdg-open"
                Explorer.StartInfo.Arguments = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=VWVJCUV3KAUX2&lc=CH&item_name=Ember%2dTeam%3a%20DanCooper%2c%20m%2esavazzi%20%26%20Cocotus&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted"
                Explorer.Start()
            End Using
        End If
    End Sub

    Private Sub DoTitleCheck()
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = "UPDATE movie SET OutOfTolerance = (?) WHERE idMovie = (?);"
                Dim parOutOfTolerance As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parOutOfTolerance", DbType.Boolean, 0, "OutOfTolerance")
                Dim par_idMovie As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("par_idMovie", DbType.Int32, 0, "idMovie")
                Dim LevFail As Boolean = False
                Dim pTitle As String = String.Empty
                For Each drvRow As DataGridViewRow In Me.dgvMovies.Rows

                    If Master.eSettings.MovieLevTolerance > 0 Then
                        If FileUtils.Common.isVideoTS(drvRow.Cells("MoviePath").Value.ToString) Then
                            pTitle = Directory.GetParent(Directory.GetParent(drvRow.Cells("MoviePath").Value.ToString).FullName).Name
                        ElseIf FileUtils.Common.isBDRip(drvRow.Cells("MoviePath").Value.ToString) Then
                            pTitle = Directory.GetParent(Directory.GetParent(Directory.GetParent(drvRow.Cells("MoviePath").Value.ToString).FullName).FullName).Name
                        Else
                            If Convert.ToBoolean(drvRow.Cells("UseFolder").FormattedValue) AndAlso Convert.ToBoolean(drvRow.Cells("Type").FormattedValue) Then
                                pTitle = Directory.GetParent(drvRow.Cells("MoviePath").Value.ToString).Name
                            Else
                                pTitle = Path.GetFileNameWithoutExtension(drvRow.Cells("MoviePath").Value.ToString)
                            End If
                        End If

                        LevFail = StringUtils.ComputeLevenshtein(StringUtils.FilterName_Movie(drvRow.Cells("Title").Value.ToString, False, True).ToLower, StringUtils.FilterName_Movie(pTitle, False, True).ToLower) > Master.eSettings.MovieLevTolerance

                        parOutOfTolerance.Value = LevFail
                        drvRow.Cells("OutOfTolerance").Value = LevFail
                        par_idMovie.Value = drvRow.Cells("idMovie").Value
                    Else
                        parOutOfTolerance.Value = False
                        drvRow.Cells("OutOfTolerance").Value = False
                        par_idMovie.Value = drvRow.Cells("idMovie").Value
                    End If
                    SQLcommand.ExecuteNonQuery()
                Next
            End Using

            SQLtransaction.Commit()
        End Using

        Me.dgvMovies.Invalidate()
    End Sub

    Sub dtListUpdate(ByVal drow As DataRow, ByVal v As DataRow)
        drow.ItemArray = v.ItemArray
    End Sub

    Private Sub Edit_Movie(ByRef DBMovie As Database.DBElement)
        Me.SetControlsEnabled(False)
        If DBMovie.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBMovie, True) Then
            Using dEditMovie As New dlgEditMovie
                AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
                Select Case dEditMovie.ShowDialog(DBMovie)
                    Case Windows.Forms.DialogResult.OK
                        DBMovie = dEditMovie.Result
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_Movie, Nothing, Nothing, False, DBMovie)
                        Me.tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                        Master.DB.SaveMovieToDB(DBMovie, False, False, True, True)
                        RefreshRow_Movie(DBMovie.ID)
                    Case Windows.Forms.DialogResult.Retry
                        Dim ScrapeModifier As New Structures.ScrapeModifier
                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
                        Me.CreateScrapeList_Movie(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_Movie, ScrapeModifier)
                    Case Windows.Forms.DialogResult.Abort
                        Dim ScrapeModifier As New Structures.ScrapeModifier
                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.DoSearch, True)
                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
                        Me.CreateScrapeList_Movie(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_Movie, ScrapeModifier)
                    Case Else
                        If Me.InfoCleared Then Me.LoadInfo_Movie(CInt(DBMovie.ID), DBMovie.Filename, True, False)
                End Select
                RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
            End Using
        End If
        Me.SetControlsEnabled(True)
    End Sub

    Private Sub Edit_MovieSet(ByRef DBMovieSet As Database.DBElement)
        Me.SetControlsEnabled(False)
        'If DBMovieSet.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBMovieSet, True) Then
        Using dEditMovieSet As New dlgEditMovieSet
            'AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
            Select Case dEditMovieSet.ShowDialog(DBMovieSet)
                Case Windows.Forms.DialogResult.OK
                    DBMovieSet = dEditMovieSet.Result
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_MovieSet, Nothing, Nothing, False, DBMovieSet)
                    Me.tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                    Master.DB.SaveMovieSetToDB(DBMovieSet, False, False, True)
                    RefreshRow_MovieSet(DBMovieSet.ID)
                Case Windows.Forms.DialogResult.Retry
                    Dim ScrapeModifier As New Structures.ScrapeModifier
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
                    Me.CreateScrapeList_MovieSet(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_MovieSet, ScrapeModifier)
                Case Windows.Forms.DialogResult.Abort
                    Dim ScrapeModifier As New Structures.ScrapeModifier
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.DoSearch, True)
                    Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
                    Me.CreateScrapeList_MovieSet(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_MovieSet, ScrapeModifier)
                Case Else
                    If Me.InfoCleared Then Me.LoadInfo_MovieSet(CInt(DBMovieSet.ID), False)
            End Select
            'RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditMovie.GenericRunCallBack
        End Using
        'End If
        Me.SetControlsEnabled(True)
    End Sub

    Private Sub Edit_TVEpisode(ByRef DBTVEpisode As Database.DBElement)
        Me.SetControlsEnabled(False)
        If DBTVEpisode.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(DBTVEpisode, True) Then
            Using dEditTVEpisode As New dlgEditTVEpisode
                AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditTVEpisode.GenericRunCallBack
                Select Case dEditTVEpisode.ShowDialog(DBTVEpisode)
                    Case Windows.Forms.DialogResult.OK
                        DBTVEpisode = dEditTVEpisode.Result
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_TVEpisode, Nothing, Nothing, False, DBTVEpisode)
                        Me.tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                        Master.DB.SaveTVEpisodeToDB(DBTVEpisode, False, False, True, True, True)
                        RefreshRow_TVEpisode(DBTVEpisode.ID)
                    Case Else
                        If Me.InfoCleared Then Me.LoadInfo_TVEpisode(CInt(DBTVEpisode.ID))
                End Select
                RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditTVEpisode.GenericRunCallBack
            End Using
        End If
        Me.SetControlsEnabled(True)
    End Sub

    Private Sub Edit_TVSeason(ByRef DBTVSeason As Database.DBElement)
        Me.SetControlsEnabled(False)
        If DBTVSeason.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBTVSeason, True) Then
            Using dEditTVSeason As New dlgEditTVSeason
                'AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditTVSeason.GenericRunCallBack
                Select Case dEditTVSeason.ShowDialog(DBTVSeason)
                    Case Windows.Forms.DialogResult.OK
                        DBTVSeason = dEditTVSeason.Result
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_TVSeason, Nothing, Nothing, False, DBTVSeason)
                        Me.tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                        Master.DB.SaveTVSeasonToDB(DBTVSeason, False, True)
                        RefreshRow_TVSeason(DBTVSeason.ID)
                    Case Else
                        'If Me.InfoCleared Then Me.LoadInfo_TVSeason(CInt(DBTVSeason.ID)) 'TODO: 
                End Select
                'RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditTVSeason.GenericRunCallBack
            End Using
        End If
        Me.SetControlsEnabled(True)
    End Sub

    Private Sub Edit_TVShow(ByRef DBTVShow As Database.DBElement)
        Me.SetControlsEnabled(False)
        If DBTVShow.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBTVShow, True) Then
            Using dEditTVShow As New dlgEditTVShow
                'AddHandler ModulesManager.Instance.GenericEvent, AddressOf dEditTVShow.GenericRunCallBack
                Select Case dEditTVShow.ShowDialog(DBTVShow)
                    Case Windows.Forms.DialogResult.OK
                        DBTVShow = dEditTVShow.Result
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_TVShow, Nothing, Nothing, False, DBTVShow)
                        Me.tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                        Master.DB.SaveTVShowToDB(DBTVShow, False, False, True, True, True)
                        RefreshRow_TVShow(DBTVShow.ID)
                    Case Windows.Forms.DialogResult.Retry
                        Dim ScrapeModifier As New Structures.ScrapeModifier
                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
                        Me.CreateScrapeList_TV(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, ScrapeModifier)
                    Case Windows.Forms.DialogResult.Abort
                        Dim ScrapeModifier As New Structures.ScrapeModifier
                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.DoSearch, True)
                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
                        Me.CreateScrapeList_TV(Enums.ScrapeType.SingleScrape, Master.DefaultOptions_TV, ScrapeModifier)
                    Case Else
                        If Me.InfoCleared Then Me.LoadInfo_TVShow(CInt(DBTVShow.ID))
                End Select
                'RemoveHandler ModulesManager.Instance.GenericEvent, AddressOf dEditTVShow.GenericRunCallBack
            End Using
        End If
        Me.SetControlsEnabled(True)
    End Sub

    Private Sub EnableFilters_Movies(ByVal isEnabled As Boolean)
        Me.btnClearFilters_Movies.Enabled = isEnabled
        Me.btnFilterMissing_Movies.Enabled = isEnabled
        Me.btnFilterSortDateAdded_Movies.Enabled = isEnabled
        Me.btnFilterSortDateModified_Movies.Enabled = isEnabled
        Me.btnFilterSortRating_Movies.Enabled = isEnabled
        Me.btnFilterSortTitle_Movies.Enabled = isEnabled
        Me.btnFilterSortYear_Movies.Enabled = isEnabled
        Me.cbFilterDataField_Movies.Enabled = isEnabled
        Me.cbFilterVideoSource_Movies.Enabled = isEnabled
        Me.cbFilterLists_Movies.Enabled = isEnabled
        Me.cbFilterLists_MovieSets.Enabled = isEnabled
        Me.cbFilterLists_Shows.Enabled = isEnabled
        Me.cbFilterYearFrom_Movies.Enabled = isEnabled
        Me.cbFilterYearModFrom_Movies.Enabled = isEnabled
        Me.cbSearchMovies.Enabled = isEnabled
        Me.chkFilterDuplicates_Movies.Enabled = isEnabled
        Me.chkFilterLock_Movies.Enabled = isEnabled
        Me.chkFilterMark_Movies.Enabled = isEnabled
        Me.chkFilterMarkCustom1_Movies.Enabled = isEnabled
        Me.chkFilterMarkCustom2_Movies.Enabled = isEnabled
        Me.chkFilterMarkCustom3_Movies.Enabled = isEnabled
        Me.chkFilterMarkCustom4_Movies.Enabled = isEnabled
        Me.chkFilterMissing_Movies.Enabled = If(Master.eSettings.MovieMissingItemsAnyEnabled, isEnabled, False)
        Me.chkFilterNew_Movies.Enabled = isEnabled
        Me.chkFilterTolerance_Movies.Enabled = If(Master.eSettings.MovieLevTolerance > 0, isEnabled, False)
        Me.pnlFilterMissingItems_Movies.Visible = If(Not isEnabled, False, Me.pnlFilterMissingItems_Movies.Visible)
        Me.rbFilterAnd_Movies.Enabled = isEnabled
        Me.rbFilterOr_Movies.Enabled = isEnabled
        Me.txtFilterCountry_Movies.Enabled = isEnabled
        Me.txtFilterGenre_Movies.Enabled = isEnabled
        Me.txtFilterDataField_Movies.Enabled = isEnabled
        Me.txtFilterSource_Movies.Enabled = isEnabled
    End Sub

    Private Sub EnableFilters_MovieSets(ByVal isEnabled As Boolean)
        Me.btnClearFilters_MovieSets.Enabled = isEnabled
        Me.btnFilterMissing_MovieSets.Enabled = isEnabled
        'Me.btnSortDate.Enabled = isEnabled
        'Me.btnIMDBRating.Enabled = isEnabled
        'Me.btnSortTitle.Enabled = isEnabled
        'Me.cbFilterFileSource.Enabled = isEnabled
        'Me.cbFilterYear.Enabled = isEnabled
        'Me.cbFilterYearMod.Enabled = isEnabled
        Me.cbSearchMovieSets.Enabled = isEnabled
        'Me.chkFilterDupe.Enabled = isEnabled
        Me.chkFilterEmpty_MovieSets.Enabled = isEnabled
        Me.chkFilterLock_MovieSets.Enabled = isEnabled
        Me.chkFilterMark_MovieSets.Enabled = isEnabled
        'Me.chkFilterMarkCustom1.Enabled = isEnabled
        'Me.chkFilterMarkCustom2.Enabled = isEnabled
        'Me.chkFilterMarkCustom3.Enabled = isEnabled
        'Me.chkFilterMarkCustom4.Enabled = isEnabled
        Me.chkFilterMissing_MovieSets.Enabled = If(Master.eSettings.MovieSetMissingItemsAnyEnabled, isEnabled, False)
        Me.chkFilterMultiple_MovieSets.Enabled = isEnabled
        Me.chkFilterNew_MovieSets.Enabled = isEnabled
        Me.chkFilterOne_MovieSets.Enabled = isEnabled
        'Me.chkFilterTolerance.Enabled = If(Master.eSettings.MovieLevTolerance > 0, isEnabled, False)
        Me.pnlFilterMissingItems_MovieSets.Visible = If(Not isEnabled, False, Me.pnlFilterMissingItems_MovieSets.Visible)
        Me.rbFilterAnd_MovieSets.Enabled = isEnabled
        Me.rbFilterOr_MovieSets.Enabled = isEnabled
        'Me.txtFilterCountry.Enabled = isEnabled
        'Me.txtFilterGenre.Enabled = isEnabled
        'Me.txtFilterSource.Enabled = isEnabled
    End Sub

    Private Sub EnableFilters_Shows(ByVal isEnabled As Boolean)
        Me.btnClearFilters_Shows.Enabled = isEnabled
        Me.btnFilterMissing_Shows.Enabled = isEnabled
        'Me.btnSortDate.Enabled = isEnabled
        'Me.btnIMDBRating.Enabled = isEnabled
        Me.btnFilterSortTitle_Shows.Enabled = isEnabled
        'Me.cbFilterFileSource.Enabled = isEnabled
        'Me.cbFilterYear.Enabled = isEnabled
        'Me.cbFilterYearMod.Enabled = isEnabled
        Me.cbSearchShows.Enabled = isEnabled
        'Me.chkFilterDuplicates.Enabled = isEnabled
        Me.chkFilterLock_Shows.Enabled = isEnabled
        Me.chkFilterMark_Shows.Enabled = isEnabled
        'Me.chkFilterMarkCustom1.Enabled = isEnabled
        'Me.chkFilterMarkCustom2.Enabled = isEnabled
        'Me.chkFilterMarkCustom3.Enabled = isEnabled
        'Me.chkFilterMarkCustom4.Enabled = isEnabled
        Me.chkFilterMissing_Shows.Enabled = If(Master.eSettings.TVShowMissingItemsAnyEnabled, isEnabled, False)
        Me.chkFilterNewEpisodes_Shows.Enabled = isEnabled
        Me.chkFilterNewShows_Shows.Enabled = isEnabled
        'Me.chkFilterTolerance.Enabled = If(Master.eSettings.MovieLevTolerance > 0, isEnabled, False)
        Me.pnlFilterMissingItems_Shows.Visible = If(Not isEnabled, False, Me.pnlFilterMissingItems_Shows.Visible)
        Me.rbFilterAnd_Shows.Enabled = isEnabled
        Me.rbFilterOr_Shows.Enabled = isEnabled
        'Me.txtFilterCountry.Enabled = isEnabled
        'Me.txtFilterGenre.Enabled = isEnabled
        Me.txtFilterSource_Shows.Enabled = isEnabled
    End Sub

    Private Sub ErrorOccurred()
        Me.mnuMainError.Visible = True
        If dlgErrorViewer.Visible Then dlgErrorViewer.UpdateLog()
    End Sub

    Private Sub ErrorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainError.Click
        dlgErrorViewer.Show(Me)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainFileExit.Click, cmnuTrayExit.Click
        If Master.isCL Then
            'fLoading.SetLoadingMesg("Canceling ...")
            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(370, "Canceling Load..."))
            If Me.bwMovieScraper.IsBusy Then Me.bwMovieScraper.CancelAsync()
            If Me.bwReload_Movies.IsBusy Then Me.bwReload_Movies.CancelAsync()
            While Me.bwMovieScraper.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse Me.bwMovieScraper.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        Else
            Me.Close()
            Application.Exit()
        End If
    End Sub

    Private Sub FillEpisodes(ByVal ShowID As Integer, ByVal Season As Integer)
        Dim sEpisodeSorting As Enums.EpisodeSorting = Master.DB.GetTVShowEpisodeSorting(ShowID)

        Me.bsTVEpisodes.DataSource = Nothing
        Me.dgvTVEpisodes.DataSource = Nothing

        Application.DoEvents()

        Me.dgvTVEpisodes.Enabled = False

        If Season = 999 Then
            Master.DB.FillDataTable(Me.dtTVEpisodes, String.Concat("SELECT * FROM episodelist WHERE idShow = ", ShowID, If(Master.eSettings.TVDisplayMissingEpisodes, String.Empty, " AND Missing = 0"), " ORDER BY Season, Episode;"))
        Else
            Master.DB.FillDataTable(Me.dtTVEpisodes, String.Concat("SELECT * FROM episodelist WHERE idShow = ", ShowID, " AND Season = ", Season, If(Master.eSettings.TVDisplayMissingEpisodes, String.Empty, " AND Missing = 0"), " ORDER BY Episode;"))
        End If

        If Me.dtTVEpisodes.Rows.Count > 0 Then

            With Me
                .bsTVEpisodes.DataSource = .dtTVEpisodes
                .dgvTVEpisodes.DataSource = .bsTVEpisodes

                Try
                    If Master.eSettings.TVGeneralEpisodeListSorting.Count > 0 Then
                        For Each mColumn In Master.eSettings.TVGeneralEpisodeListSorting
                            Me.dgvTVEpisodes.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                        Next
                    End If
                Catch ex As Exception
                    logger.Warn("default list for episode list sorting has been loaded")
                    Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVEpisodeListSorting, True)
                    If Master.eSettings.TVGeneralEpisodeListSorting.Count > 0 Then
                        For Each mColumn In Master.eSettings.TVGeneralEpisodeListSorting
                            Me.dgvTVEpisodes.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                        Next
                    End If
                End Try

                Me.dgvTVEpisodes.Columns("Season").DisplayIndex = 0
                Me.dgvTVEpisodes.Columns("Episode").DisplayIndex = 1
                Me.dgvTVEpisodes.Columns("Aired").DisplayIndex = 2

                For i As Integer = 0 To .dgvTVEpisodes.Columns.Count - 1
                    .dgvTVEpisodes.Columns(i).Visible = False
                Next

                .dgvTVEpisodes.Columns("Aired").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("Aired").Width = 80
                .dgvTVEpisodes.Columns("Aired").ReadOnly = True
                .dgvTVEpisodes.Columns("Aired").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("Aired").Visible = sEpisodeSorting = Enums.EpisodeSorting.Aired
                .dgvTVEpisodes.Columns("Aired").ToolTipText = Master.eLang.GetString(728, "Aired")
                .dgvTVEpisodes.Columns("Aired").HeaderText = Master.eLang.GetString(728, "Aired")
                .dgvTVEpisodes.Columns("Episode").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("Episode").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
                .dgvTVEpisodes.Columns("Episode").ReadOnly = True
                .dgvTVEpisodes.Columns("Episode").MinimumWidth = If(Season = 999, 35, 70)
                .dgvTVEpisodes.Columns("Episode").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("Episode").Visible = Not sEpisodeSorting = Enums.EpisodeSorting.Aired
                .dgvTVEpisodes.Columns("Episode").ToolTipText = Master.eLang.GetString(755, "Episode #")
                .dgvTVEpisodes.Columns("Episode").HeaderText = "#"
                .dgvTVEpisodes.Columns("Episode").DefaultCellStyle.Format = "00"
                .dgvTVEpisodes.Columns("FanartPath").Width = 20
                .dgvTVEpisodes.Columns("FanartPath").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("FanartPath").ReadOnly = True
                .dgvTVEpisodes.Columns("FanartPath").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("FanartPath").Visible = Not CheckColumnHide_TVEpisodes("FanartPath")
                .dgvTVEpisodes.Columns("FanartPath").ToolTipText = Master.eLang.GetString(149, "Fanart")
                .dgvTVEpisodes.Columns("HasSub").Width = 20
                .dgvTVEpisodes.Columns("HasSub").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("HasSub").ReadOnly = True
                .dgvTVEpisodes.Columns("HasSub").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("HasSub").Visible = Not CheckColumnHide_TVEpisodes("HasSub")
                .dgvTVEpisodes.Columns("HasSub").ToolTipText = Master.eLang.GetString(152, "Subtitles")
                .dgvTVEpisodes.Columns("NfoPath").Width = 20
                .dgvTVEpisodes.Columns("NfoPath").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("NfoPath").ReadOnly = True
                .dgvTVEpisodes.Columns("NfoPath").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("NfoPath").Visible = Not CheckColumnHide_TVEpisodes("NfoPath")
                .dgvTVEpisodes.Columns("NfoPath").ToolTipText = Master.eLang.GetString(150, "Nfo")
                .dgvTVEpisodes.Columns("Playcount").Width = 20
                .dgvTVEpisodes.Columns("Playcount").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("Playcount").ReadOnly = True
                .dgvTVEpisodes.Columns("Playcount").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("Playcount").Visible = Not CheckColumnHide_TVEpisodes("Playcount")
                .dgvTVEpisodes.Columns("Playcount").ToolTipText = Master.eLang.GetString(981, "Watched")
                .dgvTVEpisodes.Columns("PosterPath").Width = 20
                .dgvTVEpisodes.Columns("PosterPath").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("PosterPath").ReadOnly = True
                .dgvTVEpisodes.Columns("PosterPath").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("PosterPath").Visible = Not CheckColumnHide_TVEpisodes("PosterPath")
                .dgvTVEpisodes.Columns("PosterPath").ToolTipText = Master.eLang.GetString(148, "Poster")
                .dgvTVEpisodes.Columns("Season").MinimumWidth = 35
                .dgvTVEpisodes.Columns("Season").Resizable = DataGridViewTriState.False
                .dgvTVEpisodes.Columns("Season").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
                .dgvTVEpisodes.Columns("Season").ReadOnly = True
                .dgvTVEpisodes.Columns("Season").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("Season").Visible = Season = 999
                .dgvTVEpisodes.Columns("Season").ToolTipText = Master.eLang.GetString(659, "Season #")
                .dgvTVEpisodes.Columns("Season").HeaderText = "#"
                .dgvTVEpisodes.Columns("Season").DefaultCellStyle.Format = "00"
                .dgvTVEpisodes.Columns("Title").Resizable = DataGridViewTriState.True
                .dgvTVEpisodes.Columns("Title").ReadOnly = True
                .dgvTVEpisodes.Columns("Title").MinimumWidth = 83
                .dgvTVEpisodes.Columns("Title").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVEpisodes.Columns("Title").Visible = True
                .dgvTVEpisodes.Columns("Title").ToolTipText = Master.eLang.GetString(21, "Title")
                .dgvTVEpisodes.Columns("Title").HeaderText = Master.eLang.GetString(21, "Title")

                .dgvTVEpisodes.Columns("idEpisode").ValueType = GetType(Int32)
                .dgvTVEpisodes.Columns("idShow").ValueType = GetType(Int32)
                .dgvTVEpisodes.Columns("Episode").ValueType = GetType(Int32)
                .dgvTVEpisodes.Columns("Season").ValueType = GetType(Int32)

                If Master.isWindows Then .dgvTVEpisodes.Columns("Title").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                ResizeTVLists(.dgvTVEpisodes.Columns("Title").Index)

                .dgvTVEpisodes.ClearSelection()
                .dgvTVEpisodes.CurrentCell = Nothing

            End With
        End If

        Me.dgvTVEpisodes.Enabled = True
    End Sub
    ''' <summary>
    ''' Reloads the DB and refresh the lists
    ''' </summary>
    ''' <param name="doMovies">reload movies</param>
    ''' <param name="doMovieSets">reload moviesets</param>
    ''' <param name="doTVShows">reload tv shows</param>
    ''' <remarks></remarks>
    Private Sub FillList(ByVal doMovies As Boolean, ByVal doMovieSets As Boolean, ByVal doTVShows As Boolean)
        If doMovies Then
            Me.bsMovies.DataSource = Nothing
            Me.dgvMovies.DataSource = Nothing
            Me.ClearInfo()

            If Not String.IsNullOrEmpty(Me.filSearch_Movies) AndAlso Me.cbSearchMovies.Text = Master.eLang.GetString(100, "Actor") Then
                Master.DB.FillDataTable(Me.dtMovies, String.Concat("SELECT DISTINCT '", Me.currList_Movies, "'.* FROM actors ", _
                                                                   "LEFT OUTER JOIN actorlinkmovie ON (actors.idActor = actorlinkmovie.idActor) ", _
                                                                   "INNER JOIN '", Me.currList_Movies, "' ON (actorlinkmovie.idMovie = '", Me.currList_Movies, "'.idMovie) ", _
                                                                   "WHERE actors.strActor LIKE '%", Me.filSearch_Movies, "%' ", _
                                                                   "ORDER BY '", Me.currList_Movies, "'.ListTitle COLLATE NOCASE;"))
            ElseIf Not String.IsNullOrEmpty(Me.filSearch_Movies) AndAlso Me.cbSearchMovies.Text = Master.eLang.GetString(233, "Role") Then
                Master.DB.FillDataTable(Me.dtMovies, String.Concat("SELECT DISTINCT '", Me.currList_Movies, "'.* FROM actorlinkmovie ", _
                                                                   "INNER JOIN '", Me.currList_Movies, "' ON (actorlinkmovie.idMovie = '", Me.currList_Movies, "'.idMovie) ", _
                                                                   "WHERE actorlinkmovie.strRole LIKE '%", Me.filSearch_Movies, "%' ", _
                                                                   "ORDER BY '", Me.currList_Movies, "'.ListTitle COLLATE NOCASE;"))
            Else
                If Me.chkFilterDuplicates_Movies.Checked Then
                    Master.DB.FillDataTable(Me.dtMovies, String.Concat("SELECT * FROM '", Me.currList_Movies, "' ", _
                                                                       "WHERE imdb IN (SELECT imdb FROM '", Me.currList_Movies, "' WHERE imdb IS NOT NULL AND LENGTH(imdb) > 0 GROUP BY imdb HAVING ( COUNT(imdb) > 1 )) ", _
                                                                       "ORDER BY ListTitle COLLATE NOCASE;"))
                Else
                    Master.DB.FillDataTable(Me.dtMovies, String.Concat("SELECT * FROM '", Me.currList_Movies, "' ", _
                                                                       "ORDER BY ListTitle COLLATE NOCASE;"))
                End If
            End If
        End If

        If doMovieSets Then
            Me.bsMovieSets.DataSource = Nothing
            Me.dgvMovieSets.DataSource = Nothing
            Me.ClearInfo()
            Master.DB.FillDataTable(Me.dtMovieSets, String.Concat("SELECT * FROM '", Me.currList_MovieSets, "' ", _
                                                                  "ORDER BY ListTitle COLLATE NOCASE;"))
        End If

        If doTVShows Then
            Me.bsTVShows.DataSource = Nothing
            Me.dgvTVShows.DataSource = Nothing
            Me.bsTVSeasons.DataSource = Nothing
            Me.dgvTVSeasons.DataSource = Nothing
            Me.bsTVEpisodes.DataSource = Nothing
            Me.dgvTVEpisodes.DataSource = Nothing
            Me.ClearInfo()
            Master.DB.FillDataTable(Me.dtTVShows, String.Concat("SELECT * FROM '", Me.currList_Shows, "' ", _
                                                              "ORDER BY ListTitle COLLATE NOCASE;"))
        End If


        If Master.isCL Then
            Me.LoadingDone = True
        Else
            If doMovies Then
                Me.prevRow_Movie = -2
                If Me.dtMovies.Rows.Count > 0 Then
                    With Me
                        .bsMovies.DataSource = .dtMovies
                        .dgvMovies.DataSource = .bsMovies

                        Try
                            If Master.eSettings.MovieGeneralMediaListSorting.Count > 0 Then
                                For Each mColumn In Master.eSettings.MovieGeneralMediaListSorting
                                    Me.dgvMovies.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                                Next
                            End If
                        Catch ex As Exception
                            logger.Warn("default list for movie list sorting has been loaded")
                            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieListSorting, True)
                            If Master.eSettings.MovieGeneralMediaListSorting.Count > 0 Then
                                For Each mColumn In Master.eSettings.MovieGeneralMediaListSorting
                                    Me.dgvMovies.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                                Next
                            End If
                        End Try

                        For i As Integer = 0 To .dgvMovies.Columns.Count - 1
                            .dgvMovies.Columns(i).Visible = False
                        Next

                        .dgvMovies.Columns("BannerPath").Width = 20
                        .dgvMovies.Columns("BannerPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("BannerPath").ReadOnly = True
                        .dgvMovies.Columns("BannerPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("BannerPath").Visible = Not CheckColumnHide_Movies("BannerPath")
                        .dgvMovies.Columns("BannerPath").ToolTipText = Master.eLang.GetString(838, "Banner")
                        .dgvMovies.Columns("ClearArtPath").Width = 20
                        .dgvMovies.Columns("ClearArtPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("ClearArtPath").ReadOnly = True
                        .dgvMovies.Columns("ClearArtPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("ClearArtPath").Visible = Not CheckColumnHide_Movies("ClearArtPath")
                        .dgvMovies.Columns("ClearArtPath").ToolTipText = Master.eLang.GetString(1096, "ClearArt")
                        .dgvMovies.Columns("ClearLogoPath").Width = 20
                        .dgvMovies.Columns("ClearLogoPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("ClearLogoPath").ReadOnly = True
                        .dgvMovies.Columns("ClearLogoPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("ClearLogoPath").Visible = Not CheckColumnHide_Movies("ClearLogoPath")
                        .dgvMovies.Columns("ClearLogoPath").ToolTipText = Master.eLang.GetString(1097, "ClearLogo")
                        .dgvMovies.Columns("DiscArtPath").Width = 20
                        .dgvMovies.Columns("DiscArtPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("DiscArtPath").ReadOnly = True
                        .dgvMovies.Columns("DiscArtPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("DiscArtPath").Visible = Not CheckColumnHide_Movies("DiscArtPath")
                        .dgvMovies.Columns("DiscArtPath").ToolTipText = Master.eLang.GetString(1098, "DiscArt")
                        .dgvMovies.Columns("EFanartsPath").Width = 20
                        .dgvMovies.Columns("EFanartsPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("EFanartsPath").ReadOnly = True
                        .dgvMovies.Columns("EFanartsPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("EFanartsPath").Visible = Not CheckColumnHide_Movies("EFanartsPath")
                        .dgvMovies.Columns("EFanartsPath").ToolTipText = Master.eLang.GetString(992, "Extrafanarts")
                        .dgvMovies.Columns("EThumbsPath").Width = 20
                        .dgvMovies.Columns("EThumbsPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("EThumbsPath").ReadOnly = True
                        .dgvMovies.Columns("EThumbsPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("EThumbsPath").Visible = Not CheckColumnHide_Movies("EThumbsPath")
                        .dgvMovies.Columns("EThumbsPath").ToolTipText = Master.eLang.GetString(153, "Extrathumbs")
                        .dgvMovies.Columns("FanartPath").Width = 20
                        .dgvMovies.Columns("FanartPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("FanartPath").ReadOnly = True
                        .dgvMovies.Columns("FanartPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("FanartPath").Visible = Not CheckColumnHide_Movies("FanartPath")
                        .dgvMovies.Columns("FanartPath").ToolTipText = Master.eLang.GetString(149, "Fanart")
                        .dgvMovies.Columns("HasSet").Width = 20
                        .dgvMovies.Columns("HasSet").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("HasSet").ReadOnly = True
                        .dgvMovies.Columns("HasSet").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("HasSet").Visible = Not CheckColumnHide_Movies("HasSet")
                        .dgvMovies.Columns("HasSet").ToolTipText = Master.eLang.GetString(1295, "Part of a MovieSet")
                        .dgvMovies.Columns("HasSub").Width = 20
                        .dgvMovies.Columns("HasSub").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("HasSub").ReadOnly = True
                        .dgvMovies.Columns("HasSub").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("HasSub").Visible = Not CheckColumnHide_Movies("HasSub")
                        .dgvMovies.Columns("HasSub").ToolTipText = Master.eLang.GetString(152, "Subtitles")
                        .dgvMovies.Columns("Imdb").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("Imdb").ReadOnly = True
                        .dgvMovies.Columns("Imdb").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("Imdb").Visible = Not CheckColumnHide_Movies("Imdb")
                        .dgvMovies.Columns("Imdb").ToolTipText = Master.eLang.GetString(61, "IMDB ID")
                        .dgvMovies.Columns("Imdb").HeaderText = Master.eLang.GetString(61, "IMDB ID")
                        .dgvMovies.Columns("Imdb").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                        .dgvMovies.Columns("LandscapePath").Width = 20
                        .dgvMovies.Columns("LandscapePath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("LandscapePath").ReadOnly = True
                        .dgvMovies.Columns("LandscapePath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("LandscapePath").Visible = Not CheckColumnHide_Movies("LandscapePath")
                        .dgvMovies.Columns("LandscapePath").ToolTipText = Master.eLang.GetString(1035, "Landscape")
                        .dgvMovies.Columns("ListTitle").Resizable = DataGridViewTriState.True
                        .dgvMovies.Columns("ListTitle").ReadOnly = True
                        .dgvMovies.Columns("ListTitle").MinimumWidth = 83
                        .dgvMovies.Columns("ListTitle").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("ListTitle").Visible = True
                        .dgvMovies.Columns("ListTitle").ToolTipText = Master.eLang.GetString(21, "Title")
                        .dgvMovies.Columns("ListTitle").HeaderText = Master.eLang.GetString(21, "Title")
                        .dgvMovies.Columns("MPAA").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("MPAA").Width = 70
                        .dgvMovies.Columns("MPAA").ReadOnly = True
                        .dgvMovies.Columns("MPAA").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("MPAA").Visible = Not CheckColumnHide_Movies("MPAA")
                        .dgvMovies.Columns("MPAA").ToolTipText = Master.eLang.GetString(401, "MPAA")
                        .dgvMovies.Columns("MPAA").HeaderText = Master.eLang.GetString(401, "MPAA")
                        '.dgvMovies.Columns("MPAA").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                        .dgvMovies.Columns("NfoPath").Width = 20
                        .dgvMovies.Columns("NfoPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("NfoPath").ReadOnly = True
                        .dgvMovies.Columns("NfoPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("NfoPath").Visible = Not CheckColumnHide_Movies("NfoPath")
                        .dgvMovies.Columns("NfoPath").ToolTipText = Master.eLang.GetString(150, "Nfo")
                        .dgvMovies.Columns("OriginalTitle").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("OriginalTitle").ReadOnly = True
                        .dgvMovies.Columns("OriginalTitle").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("OriginalTitle").Visible = Not CheckColumnHide_Movies("OriginalTitle")
                        .dgvMovies.Columns("OriginalTitle").ToolTipText = Master.eLang.GetString(302, "Original Title")
                        .dgvMovies.Columns("OriginalTitle").HeaderText = Master.eLang.GetString(302, "Original Title")
                        .dgvMovies.Columns("OriginalTitle").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                        .dgvMovies.Columns("Playcount").Width = 20
                        .dgvMovies.Columns("Playcount").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("Playcount").ReadOnly = True
                        .dgvMovies.Columns("Playcount").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("Playcount").Visible = Not CheckColumnHide_Movies("Playcount")
                        .dgvMovies.Columns("Playcount").ToolTipText = Master.eLang.GetString(981, "Watched")
                        .dgvMovies.Columns("PosterPath").Width = 20
                        .dgvMovies.Columns("PosterPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("PosterPath").ReadOnly = True
                        .dgvMovies.Columns("PosterPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("PosterPath").Visible = Not CheckColumnHide_Movies("PosterPath")
                        .dgvMovies.Columns("PosterPath").ToolTipText = Master.eLang.GetString(148, "Poster")
                        .dgvMovies.Columns("Rating").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("Rating").ReadOnly = True
                        .dgvMovies.Columns("Rating").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("Rating").Visible = Not CheckColumnHide_Movies("Rating")
                        .dgvMovies.Columns("Rating").ToolTipText = Master.eLang.GetString(400, "Rating")
                        .dgvMovies.Columns("Rating").HeaderText = Master.eLang.GetString(400, "Rating")
                        .dgvMovies.Columns("Rating").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                        .dgvMovies.Columns("ThemePath").Width = 20
                        .dgvMovies.Columns("ThemePath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("ThemePath").ReadOnly = True
                        .dgvMovies.Columns("ThemePath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("ThemePath").Visible = Not CheckColumnHide_Movies("ThemePath")
                        .dgvMovies.Columns("ThemePath").ToolTipText = Master.eLang.GetString(1118, "Theme")
                        .dgvMovies.Columns("TMDB").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("TMDB").ReadOnly = True
                        .dgvMovies.Columns("TMDB").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("TMDB").Visible = Not CheckColumnHide_Movies("TMDB")
                        .dgvMovies.Columns("TMDB").ToolTipText = Master.eLang.GetString(933, "TMDB ID")
                        .dgvMovies.Columns("TMDB").HeaderText = Master.eLang.GetString(933, "TMDB ID")
                        .dgvMovies.Columns("TMDB").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                        .dgvMovies.Columns("TrailerPath").Width = 20
                        .dgvMovies.Columns("TrailerPath").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("TrailerPath").ReadOnly = True
                        .dgvMovies.Columns("TrailerPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("TrailerPath").Visible = Not CheckColumnHide_Movies("TrailerPath")
                        .dgvMovies.Columns("TrailerPath").ToolTipText = Master.eLang.GetString(151, "Trailer")
                        .dgvMovies.Columns("Year").Resizable = DataGridViewTriState.False
                        .dgvMovies.Columns("Year").ReadOnly = True
                        .dgvMovies.Columns("Year").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovies.Columns("Year").Visible = Not CheckColumnHide_Movies("Year")
                        .dgvMovies.Columns("Year").ToolTipText = Master.eLang.GetString(278, "Year")
                        .dgvMovies.Columns("Year").HeaderText = Master.eLang.GetString(278, "Year")
                        .dgvMovies.Columns("Year").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

                        .dgvMovies.Columns("idMovie").ValueType = GetType(Int32)

                        If Master.isWindows Then .dgvMovies.Columns("ListTitle").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        ResizeMoviesList()
                    End With
                End If
            End If

            If doMovieSets Then
                Me.prevRow_MovieSet = -2
                Me.dgvMovieSets.Enabled = False
                If Me.dtMovieSets.Rows.Count > 0 Then
                    With Me
                        .bsMovieSets.DataSource = .dtMovieSets
                        .dgvMovieSets.DataSource = .bsMovieSets

                        Try
                            If Master.eSettings.MovieSetGeneralMediaListSorting.Count > 0 Then
                                For Each mColumn In Master.eSettings.MovieSetGeneralMediaListSorting
                                    Me.dgvMovieSets.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                                Next
                            End If
                        Catch ex As Exception
                            logger.Warn("default list for movieset list sorting has been loaded")
                            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MovieSetListSorting, True)
                            If Master.eSettings.MovieSetGeneralMediaListSorting.Count > 0 Then
                                For Each mColumn In Master.eSettings.MovieSetGeneralMediaListSorting
                                    Me.dgvMovieSets.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                                Next
                            End If
                        End Try

                        For i As Integer = 0 To .dgvMovieSets.Columns.Count - 1
                            .dgvMovieSets.Columns(i).Visible = False
                        Next

                        .dgvMovieSets.Columns("BannerPath").Width = 20
                        .dgvMovieSets.Columns("BannerPath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("BannerPath").ReadOnly = True
                        .dgvMovieSets.Columns("BannerPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("BannerPath").Visible = Not CheckColumnHide_MovieSets("BannerPath")
                        .dgvMovieSets.Columns("BannerPath").ToolTipText = Master.eLang.GetString(838, "Banner")
                        .dgvMovieSets.Columns("ClearArtPath").Width = 20
                        .dgvMovieSets.Columns("ClearArtPath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("ClearArtPath").ReadOnly = True
                        .dgvMovieSets.Columns("ClearArtPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("ClearArtPath").Visible = Not CheckColumnHide_MovieSets("ClearArtPath")
                        .dgvMovieSets.Columns("ClearArtPath").ToolTipText = Master.eLang.GetString(1096, "ClearArt")
                        .dgvMovieSets.Columns("ClearLogoPath").Width = 20
                        .dgvMovieSets.Columns("ClearLogoPath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("ClearLogoPath").ReadOnly = True
                        .dgvMovieSets.Columns("ClearLogoPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("ClearLogoPath").Visible = Not CheckColumnHide_MovieSets("ClearLogoPath")
                        .dgvMovieSets.Columns("ClearLogoPath").ToolTipText = Master.eLang.GetString(1097, "ClearLogo")
                        .dgvMovieSets.Columns("DiscArtPath").Width = 20
                        .dgvMovieSets.Columns("DiscArtPath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("DiscArtPath").ReadOnly = True
                        .dgvMovieSets.Columns("DiscArtPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("DiscArtPath").Visible = Not CheckColumnHide_MovieSets("DiscArtPath")
                        .dgvMovieSets.Columns("DiscArtPath").ToolTipText = Master.eLang.GetString(1098, "DiscArt")
                        .dgvMovieSets.Columns("FanartPath").Width = 20
                        .dgvMovieSets.Columns("FanartPath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("FanartPath").ReadOnly = True
                        .dgvMovieSets.Columns("FanartPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("FanartPath").Visible = Not CheckColumnHide_MovieSets("FanartPath")
                        .dgvMovieSets.Columns("FanartPath").ToolTipText = Master.eLang.GetString(149, "Fanart")
                        .dgvMovieSets.Columns("LandscapePath").Width = 20
                        .dgvMovieSets.Columns("LandscapePath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("LandscapePath").ReadOnly = True
                        .dgvMovieSets.Columns("LandscapePath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("LandscapePath").Visible = Not CheckColumnHide_MovieSets("LandscapePath")
                        .dgvMovieSets.Columns("LandscapePath").ToolTipText = Master.eLang.GetString(1035, "Landscape")
                        .dgvMovieSets.Columns("ListTitle").Resizable = DataGridViewTriState.True
                        .dgvMovieSets.Columns("ListTitle").ReadOnly = True
                        .dgvMovieSets.Columns("ListTitle").MinimumWidth = 83
                        .dgvMovieSets.Columns("ListTitle").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("ListTitle").Visible = True
                        .dgvMovieSets.Columns("ListTitle").ToolTipText = Master.eLang.GetString(21, "Title")
                        .dgvMovieSets.Columns("ListTitle").HeaderText = Master.eLang.GetString(21, "Title")
                        .dgvMovieSets.Columns("NfoPath").Width = 20
                        .dgvMovieSets.Columns("NfoPath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("NfoPath").ReadOnly = True
                        .dgvMovieSets.Columns("NfoPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("NfoPath").Visible = Not CheckColumnHide_MovieSets("NfoPath")
                        .dgvMovieSets.Columns("NfoPath").ToolTipText = Master.eLang.GetString(150, "Nfo")
                        .dgvMovieSets.Columns("PosterPath").Width = 20
                        .dgvMovieSets.Columns("PosterPath").Resizable = DataGridViewTriState.False
                        .dgvMovieSets.Columns("PosterPath").ReadOnly = True
                        .dgvMovieSets.Columns("PosterPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvMovieSets.Columns("PosterPath").Visible = Not CheckColumnHide_MovieSets("PosterPath")
                        .dgvMovieSets.Columns("PosterPath").ToolTipText = Master.eLang.GetString(148, "Poster")

                        .dgvMovieSets.Columns("idSet").ValueType = GetType(Int32)

                        If Master.isWindows Then .dgvMovieSets.Columns("ListTitle").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        ResizeMovieSetsList()
                    End With

                    Me.dgvMovieSets.Enabled = True
                End If
            End If

            If doTVShows Then
                Me.currList = 0
                Me.prevRow_TVEpisode = -2
                Me.prevRow_TVSeason = -2
                Me.prevRow_TVShow = -2
                Me.dgvTVShows.Enabled = False
                If Me.dtTVShows.Rows.Count > 0 Then
                    With Me
                        .bsTVShows.DataSource = .dtTVShows
                        .dgvTVShows.DataSource = .bsTVShows

                        Try
                            If Master.eSettings.TVGeneralShowListSorting.Count > 0 Then
                                For Each mColumn In Master.eSettings.TVGeneralShowListSorting
                                    Me.dgvTVShows.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                                Next
                            End If
                        Catch ex As Exception
                            logger.Warn("default list for tv show list sorting has been loaded")
                            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVShowListSorting, True)
                            If Master.eSettings.TVGeneralShowListSorting.Count > 0 Then
                                For Each mColumn In Master.eSettings.TVGeneralShowListSorting
                                    Me.dgvTVShows.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                                Next
                            End If
                        End Try

                        For i As Integer = 0 To .dgvTVShows.Columns.Count - 1
                            .dgvTVShows.Columns(i).Visible = False
                        Next

                        .dgvTVShows.Columns("BannerPath").Width = 20
                        .dgvTVShows.Columns("BannerPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("BannerPath").ReadOnly = True
                        .dgvTVShows.Columns("BannerPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("BannerPath").Visible = Not CheckColumnHide_TVShows("BannerPath")
                        .dgvTVShows.Columns("BannerPath").ToolTipText = Master.eLang.GetString(838, "Banner")
                        .dgvTVShows.Columns("CharacterArtPath").Width = 20
                        .dgvTVShows.Columns("CharacterArtPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("CharacterArtPath").ReadOnly = True
                        .dgvTVShows.Columns("CharacterArtPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("CharacterArtPath").Visible = Not CheckColumnHide_TVShows("CharacterArtPath")
                        .dgvTVShows.Columns("CharacterArtPath").ToolTipText = Master.eLang.GetString(1140, "CharacterArt")
                        .dgvTVShows.Columns("ClearArtPath").Width = 20
                        .dgvTVShows.Columns("ClearArtPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("ClearArtPath").ReadOnly = True
                        .dgvTVShows.Columns("ClearArtPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("ClearArtPath").Visible = Not CheckColumnHide_TVShows("ClearArtPath")
                        .dgvTVShows.Columns("ClearArtPath").ToolTipText = Master.eLang.GetString(1096, "ClearArt")
                        .dgvTVShows.Columns("ClearLogoPath").Width = 20
                        .dgvTVShows.Columns("ClearLogoPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("ClearLogoPath").ReadOnly = True
                        .dgvTVShows.Columns("ClearLogoPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("ClearLogoPath").Visible = Not CheckColumnHide_TVShows("ClearLogoPath")
                        .dgvTVShows.Columns("ClearLogoPath").ToolTipText = Master.eLang.GetString(1097, "ClearLogo")
                        .dgvTVShows.Columns("EFanartsPath").Width = 20
                        .dgvTVShows.Columns("EFanartsPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("EFanartsPath").ReadOnly = True
                        .dgvTVShows.Columns("EFanartsPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("EFanartsPath").Visible = Not CheckColumnHide_TVShows("EFanartsPath")
                        .dgvTVShows.Columns("EFanartsPath").ToolTipText = Master.eLang.GetString(992, "Extrafanarts")
                        .dgvTVShows.Columns("Episodes").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
                        .dgvTVShows.Columns("Episodes").MinimumWidth = 30
                        .dgvTVShows.Columns("Episodes").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        .dgvTVShows.Columns("Episodes").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("Episodes").ReadOnly = True
                        .dgvTVShows.Columns("Episodes").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("Episodes").Visible = Not CheckColumnHide_TVShows("Episodes")
                        .dgvTVShows.Columns("Episodes").ToolTipText = Master.eLang.GetString(682, "Episodes")
                        .dgvTVShows.Columns("Episodes").HeaderText = String.Empty
                        .dgvTVShows.Columns("FanartPath").Width = 20
                        .dgvTVShows.Columns("FanartPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("FanartPath").ReadOnly = True
                        .dgvTVShows.Columns("FanartPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("FanartPath").Visible = Not CheckColumnHide_TVShows("FanartPath")
                        .dgvTVShows.Columns("FanartPath").ToolTipText = Master.eLang.GetString(149, "Fanart")
                        .dgvTVShows.Columns("HasWatched").Width = 20
                        .dgvTVShows.Columns("HasWatched").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("HasWatched").ReadOnly = True
                        .dgvTVShows.Columns("HasWatched").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("HasWatched").Visible = Not CheckColumnHide_TVShows("HasWatched")
                        .dgvTVShows.Columns("HasWatched").ToolTipText = Master.eLang.GetString(981, "Watched")
                        .dgvTVShows.Columns("LandscapePath").Width = 20
                        .dgvTVShows.Columns("LandscapePath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("LandscapePath").ReadOnly = True
                        .dgvTVShows.Columns("LandscapePath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("LandscapePath").Visible = Not CheckColumnHide_TVShows("LandscapePath")
                        .dgvTVShows.Columns("LandscapePath").ToolTipText = Master.eLang.GetString(1035, "Landscape")
                        .dgvTVShows.Columns("ListTitle").Resizable = DataGridViewTriState.True
                        .dgvTVShows.Columns("ListTitle").ReadOnly = True
                        .dgvTVShows.Columns("ListTitle").MinimumWidth = 83
                        .dgvTVShows.Columns("ListTitle").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("ListTitle").Visible = True
                        .dgvTVShows.Columns("ListTitle").ToolTipText = Master.eLang.GetString(21, "Title")
                        .dgvTVShows.Columns("ListTitle").HeaderText = Master.eLang.GetString(21, "Title")
                        .dgvTVShows.Columns("NfoPath").Width = 20
                        .dgvTVShows.Columns("NfoPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("NfoPath").ReadOnly = True
                        .dgvTVShows.Columns("NfoPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("NfoPath").Visible = Not CheckColumnHide_TVShows("NfoPath")
                        .dgvTVShows.Columns("NfoPath").ToolTipText = Master.eLang.GetString(150, "Nfo")
                        .dgvTVShows.Columns("PosterPath").Width = 20
                        .dgvTVShows.Columns("PosterPath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("PosterPath").ReadOnly = True
                        .dgvTVShows.Columns("PosterPath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("PosterPath").Visible = Not CheckColumnHide_TVShows("PosterPath")
                        .dgvTVShows.Columns("PosterPath").ToolTipText = Master.eLang.GetString(148, "Poster")
                        .dgvTVShows.Columns("Status").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("Status").ReadOnly = True
                        .dgvTVShows.Columns("Status").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("Status").Visible = Not CheckColumnHide_TVShows("Status")
                        .dgvTVShows.Columns("Status").ToolTipText = Master.eLang.GetString(215, "Status")
                        .dgvTVShows.Columns("Status").HeaderText = Master.eLang.GetString(215, "Status")
                        .dgvTVShows.Columns("Status").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                        .dgvTVShows.Columns("ThemePath").Width = 20
                        .dgvTVShows.Columns("ThemePath").Resizable = DataGridViewTriState.False
                        .dgvTVShows.Columns("ThemePath").ReadOnly = True
                        .dgvTVShows.Columns("ThemePath").SortMode = DataGridViewColumnSortMode.Automatic
                        .dgvTVShows.Columns("ThemePath").Visible = Not CheckColumnHide_TVShows("ThemePath")
                        .dgvTVShows.Columns("ThemePath").ToolTipText = Master.eLang.GetString(1118, "Theme")

                        .dgvTVShows.Columns("idShow").ValueType = GetType(Int32)

                        If Master.isWindows Then .dgvTVShows.Columns("ListTitle").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        ResizeTVLists(.dgvTVShows.Columns("ListTitle").Index)
                    End With

                    Me.dgvTVShows.Enabled = True
                End If
            End If

            If Me.dgvMovies.RowCount > 0 OrElse Me.dgvMovieSets.RowCount > 0 OrElse Me.dgvTVShows.RowCount > 0 Then
                Me.SetControlsEnabled(True)
            Else
                Me.SetControlsEnabled(False, False, False)
                Me.SetStatus(String.Empty)
                Me.ClearInfo()
            End If
        End If

        If Not Master.isCL Then
            Me.mnuUpdate.Enabled = True
            Me.cmnuTrayExit.Enabled = True
            Me.cmnuTraySettings.Enabled = True
            Me.mnuMainEdit.Enabled = True
            Me.cmnuTrayUpdate.Enabled = True
            Me.mnuMainHelp.Enabled = True
            Me.tslLoading.Visible = False
            Me.tspbLoading.Visible = False
            Me.tspbLoading.Value = 0
            Me.tcMain.Enabled = True
            Me.DoTitleCheck()
            Me.EnableFilters_Movies(True)
            Me.EnableFilters_MovieSets(True)
            Me.EnableFilters_Shows(True)
            If doMovies Then
                Me.RestoreFilter_Movies()
            End If
            If doMovieSets Then
                Me.RestoreFilter_MovieSets()
            End If
            If doTVShows Then
                Me.RestoreFilter_Shows()
            End If
            If doMovies AndAlso doMovieSets AndAlso doTVShows Then
                Me.UpdateMainTabCounts()
            End If
        End If
    End Sub

    Private Sub FillScreenInfoWithImages()
        Dim g As Graphics
        Dim strSize As String
        Dim lenSize As Integer
        Dim rect As Rectangle

        If Me.MainPoster.Image IsNot Nothing Then
            Me.lblPosterSize.Text = String.Format("{0} x {1}", Me.MainPoster.Image.Width, Me.MainPoster.Image.Height)
            Me.pbPosterCache.Image = Me.MainPoster.Image
            ImageUtils.ResizePB(Me.pbPoster, Me.pbPosterCache, Me.PosterMaxHeight, Me.PosterMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbPoster)

            If Master.eSettings.GeneralShowImgDims Then
                Me.lblPosterSize.Visible = True
            Else
                Me.lblPosterSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                Me.lblPosterTitle.Visible = True
            Else
                Me.lblPosterTitle.Visible = False
            End If
        Else
            If Me.pbPoster.Image IsNot Nothing Then
                Me.pbPoster.Image.Dispose()
                Me.pbPoster.Image = Nothing
            End If
        End If

        If Me.MainFanartSmall.Image IsNot Nothing Then
            Me.lblFanartSmallSize.Text = String.Format("{0} x {1}", Me.MainFanartSmall.Image.Width, Me.MainFanartSmall.Image.Height)
            Me.pbFanartSmallCache.Image = Me.MainFanartSmall.Image
            ImageUtils.ResizePB(Me.pbFanartSmall, Me.pbFanartSmallCache, Me.FanartSmallMaxHeight, Me.FanartSmallMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbFanartSmall)
            'Me.pnlFanartSmall.Location = New Point(Me.pnlPoster.Location.X + Me.pnlPoster.Width + 5, Me.pnlPoster.Location.Y)    TODO: move the Location to theme settings
            Me.pnlFanartSmall.Location = New Point(124, 130)

            If Master.eSettings.GeneralShowImgDims Then
                Me.lblFanartSmallSize.Visible = True
            Else
                Me.lblFanartSmallSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                Me.lblFanartSmallTitle.Visible = True
            Else
                Me.lblFanartSmallTitle.Visible = False
            End If
        Else
            If Me.pbFanartSmall.Image IsNot Nothing Then
                Me.pbFanartSmall.Image.Dispose()
                Me.pbFanartSmall.Image = Nothing
            End If
        End If

        If Me.MainLandscape.Image IsNot Nothing Then
            Me.lblLandscapeSize.Text = String.Format("{0} x {1}", Me.MainLandscape.Image.Width, Me.MainLandscape.Image.Height)
            Me.pbLandscapeCache.Image = Me.MainLandscape.Image
            ImageUtils.ResizePB(Me.pbLandscape, Me.pbLandscapeCache, Me.LandscapeMaxHeight, Me.LandscapeMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbLandscape)
            'Me.pnlLandscape.Location = New Point(Me.pnlFanartSmall.Location.X + Me.pnlFanartSmall.Width + 5, Me.pnlFanartSmall.Location.Y)
            Me.pnlLandscape.Location = New Point(419, 130)

            If Master.eSettings.GeneralShowImgDims Then
                Me.lblLandscapeSize.Visible = True
            Else
                Me.lblLandscapeSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                Me.lblLandscapeTitle.Visible = True
            Else
                Me.lblLandscapeTitle.Visible = False
            End If
        Else
            If Me.pbLandscape.Image IsNot Nothing Then
                Me.pbLandscape.Image.Dispose()
                Me.pbLandscape.Image = Nothing
            End If
        End If

        If Me.MainClearArt.Image IsNot Nothing Then
            Me.lblClearArtSize.Text = String.Format("{0} x {1}", Me.MainClearArt.Image.Width, Me.MainClearArt.Image.Height)
            Me.pbClearArtCache.Image = Me.MainClearArt.Image
            ImageUtils.ResizePB(Me.pbClearArt, Me.pbClearArtCache, Me.ClearArtMaxHeight, Me.ClearArtMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbClearArt)
            'Me.pnlClearArt.Location = New Point(Me.pnlLandscape.Location.X + Me.pnlLandscape.Width + 5, Me.pnlLandscape.Location.Y)
            Me.pnlClearArt.Location = New Point(715, 130)

            If Master.eSettings.GeneralShowImgDims Then
                Me.lblClearArtSize.Visible = True
            Else
                Me.lblClearArtSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                Me.lblClearArtTitle.Visible = True
            Else
                Me.lblClearArtTitle.Visible = False
            End If
        Else
            If Me.pbClearArt.Image IsNot Nothing Then
                Me.pbClearArt.Image.Dispose()
                Me.pbClearArt.Image = Nothing
            End If
        End If

        If Me.MainCharacterArt.Image IsNot Nothing Then
            Me.lblCharacterArtSize.Text = String.Format("{0} x {1}", Me.MainCharacterArt.Image.Width, Me.MainCharacterArt.Image.Height)
            Me.pbCharacterArtCache.Image = Me.MainCharacterArt.Image
            ImageUtils.ResizePB(Me.pbCharacterArt, Me.pbCharacterArtCache, Me.CharacterArtMaxHeight, Me.CharacterArtMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbCharacterArt)
            'Me.pnlCharacterArt.Location = New Point(Me.pnlClearArt.Location.X + Me.pnlClearArt.Width + 5, Me.pnlClearArt.Location.Y)
            Me.pnlCharacterArt.Location = New Point(1011, 130)

            If Master.eSettings.GeneralShowImgDims Then
                Me.lblCharacterArtSize.Visible = True
            Else
                Me.lblCharacterArtSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                Me.lblCharacterArtTitle.Visible = True
            Else
                Me.lblCharacterArtTitle.Visible = False
            End If
        Else
            If Me.pbCharacterArt.Image IsNot Nothing Then
                Me.pbCharacterArt.Image.Dispose()
                Me.pbCharacterArt.Image = Nothing
            End If
        End If

        If Me.MainDiscArt.Image IsNot Nothing Then
            Me.lblDiscArtSize.Text = String.Format("{0} x {1}", Me.MainDiscArt.Image.Width, Me.MainDiscArt.Image.Height)
            Me.pbDiscArtCache.Image = Me.MainDiscArt.Image
            ImageUtils.ResizePB(Me.pbDiscArt, Me.pbDiscArtCache, Me.DiscArtMaxHeight, Me.DiscArtMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbDiscArt)
            'Me.pnlDiscArt.Location = New Point(Me.pnlClearArt.Location.X + Me.pnlClearArt.Width + 5, Me.pnlClearArt.Location.Y)
            Me.pnlDiscArt.Location = New Point(1011, 130)

            If Master.eSettings.GeneralShowImgDims Then
                Me.lblDiscArtSize.Visible = True
            Else
                Me.lblDiscArtSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                Me.lblDiscArtTitle.Visible = True
            Else
                Me.lblDiscArtTitle.Visible = False
            End If
        Else
            If Me.pbDiscArt.Image IsNot Nothing Then
                Me.pbDiscArt.Image.Dispose()
                Me.pbDiscArt.Image = Nothing
            End If
        End If

        If Me.MainBanner.Image IsNot Nothing Then
            Me.lblBannerSize.Text = String.Format("{0} x {1}", Me.MainBanner.Image.Width, Me.MainBanner.Image.Height)
            Me.pbBannerCache.Image = Me.MainBanner.Image
            ImageUtils.ResizePB(Me.pbBanner, Me.pbBannerCache, Me.BannerMaxHeight, Me.BannerMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbBanner)
            'Me.pnlBanner.Location = New Point(Me.pnlFanartSmall.Location.X, Me.pnlFanartSmall.Location.Y + Me.pnlFanartSmall.Height + 5)
            Me.pnlBanner.Location = New Point(124, 327)

            If Master.eSettings.GeneralShowImgDims Then
                Me.lblBannerSize.Visible = True
            Else
                Me.lblBannerSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                Me.lblBannerTitle.Visible = True
            Else
                Me.lblBannerTitle.Visible = False
            End If
        Else
            If Me.pbBanner.Image IsNot Nothing Then
                Me.pbBanner.Image.Dispose()
                Me.pbBanner.Image = Nothing
            End If
        End If

        If Me.MainClearLogo.Image IsNot Nothing Then
            Me.lblClearLogoSize.Text = String.Format("{0} x {1}", Me.MainClearLogo.Image.Width, Me.MainClearLogo.Image.Height)
            Me.pbClearLogoCache.Image = Me.MainClearLogo.Image
            ImageUtils.ResizePB(Me.pbClearLogo, Me.pbClearLogoCache, Me.ClearLogoMaxHeight, Me.ClearLogoMaxWidth)
            If Master.eSettings.GeneralImagesGlassOverlay Then ImageUtils.SetGlassOverlay(Me.pbClearLogo)
            'Me.pnlClearLogo.Location = New Point(Me.pnlLandscape.Location.X, Me.pnlLandscape.Location.Y + Me.pnlLandscape.Height + 5)
            Me.pnlClearLogo.Location = New Point(419, 327)

            If Master.eSettings.GeneralShowImgDims Then
                Me.lblClearLogoSize.Visible = True
            Else
                Me.lblClearLogoSize.Visible = False
            End If

            If Master.eSettings.GeneralShowImgNames Then
                Me.lblClearLogoTitle.Visible = True
            Else
                Me.lblClearLogoTitle.Visible = False
            End If
        Else
            If Me.pbClearLogo.Image IsNot Nothing Then
                Me.pbClearLogo.Image.Dispose()
                Me.pbClearLogo.Image = Nothing
            End If
        End If

        If Me.MainFanart.Image IsNot Nothing Then
            Me.pbFanartCache.Image = Me.MainFanart.Image

            ImageUtils.ResizePB(Me.pbFanart, Me.pbFanartCache, Me.scMain.Panel2.Height - 90, Me.scMain.Panel2.Width)
            Me.pbFanart.Left = Convert.ToInt32((Me.scMain.Panel2.Width - Me.pbFanart.Width) / 2)

            If pbFanart.Image IsNot Nothing AndAlso Master.eSettings.GeneralShowImgDims Then
                g = Graphics.FromImage(pbFanart.Image)
                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                strSize = String.Format("{0} x {1}", Me.MainFanart.Image.Width, Me.MainFanart.Image.Height)
                lenSize = Convert.ToInt32(g.MeasureString(strSize, New Font("Arial", 8, FontStyle.Bold)).Width)
                rect = New Rectangle(Convert.ToInt32((pbFanart.Image.Width - lenSize) / 2 - 15), Me.pbFanart.Height - 25, lenSize + 30, 25)
                ImageUtils.DrawGradEllipse(g, rect, Color.FromArgb(250, 120, 120, 120), Color.FromArgb(0, 255, 255, 255))
                g.DrawString(strSize, New Font("Arial", 8, FontStyle.Bold), New SolidBrush(Color.White), Convert.ToInt32((Me.pbFanart.Image.Width - lenSize) / 2), Me.pbFanart.Height - 20)
            End If
        Else
            If Me.pbFanartCache.Image IsNot Nothing Then
                Me.pbFanartCache.Image.Dispose()
                Me.pbFanartCache.Image = Nothing
            End If
            If Me.pbFanart.Image IsNot Nothing Then
                Me.pbFanart.Image.Dispose()
                Me.pbFanart.Image = Nothing
            End If
        End If
    End Sub

    Private Sub FillScreenInfoWith_Movie()
        Try
            Me.SuspendLayout()
            If Me.currMovie.Movie.TitleSpecified AndAlso Me.currMovie.Movie.YearSpecified Then
                Me.lblTitle.Text = String.Format("{0} ({1})", Me.currMovie.Movie.Title, Me.currMovie.Movie.Year)
            ElseIf Me.currMovie.Movie.TitleSpecified AndAlso Not Me.currMovie.Movie.YearSpecified Then
                Me.lblTitle.Text = Me.currMovie.Movie.Title
            ElseIf Not Me.currMovie.Movie.TitleSpecified AndAlso Me.currMovie.Movie.YearSpecified Then
                Me.lblTitle.Text = String.Format(Master.eLang.GetString(117, "Unknown Movie ({0})"), Me.currMovie.Movie.Year)
            End If

            If Me.currMovie.Movie.OriginalTitleSpecified AndAlso Me.currMovie.Movie.OriginalTitle <> Me.currMovie.Movie.Title Then
                Me.lblOriginalTitle.Text = String.Format(String.Concat(Master.eLang.GetString(302, "Original Title"), ": {0}"), Me.currMovie.Movie.OriginalTitle)
            Else
                Me.lblOriginalTitle.Text = String.Empty
            End If

            Try
                If Not String.IsNullOrEmpty(Me.currMovie.Movie.Rating) Then
                    If Not String.IsNullOrEmpty(Me.currMovie.Movie.Votes) Then
                        Dim strRating As String = Double.Parse(Me.currMovie.Movie.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                        Dim strVotes As String = Double.Parse(Me.currMovie.Movie.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                        Me.lblRating.Text = String.Concat(strRating, "/10 (", String.Format(Master.eLang.GetString(118, "{0} Votes"), strVotes), ")")
                    Else
                        Dim strRating As String = Double.Parse(Me.currMovie.Movie.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                        Me.lblRating.Text = String.Concat(strRating, "/10")
                    End If
                End If
            Catch ex As Exception
                logger.Error(String.Concat("Error: Not valid Rating or Votes (", Me.currMovie.Movie.Rating, " / ", Me.currMovie.Movie.Votes, ")"))
                Me.lblRating.Text = "Error: Please rescrape Rating and Votes"
            End Try

            If Me.currMovie.Movie.RuntimeSpecified Then
                Me.lblRuntime.Text = String.Format(Master.eLang.GetString(112, "Runtime: {0}"), If(Me.currMovie.Movie.Runtime.Contains("|"), Microsoft.VisualBasic.Strings.Left(Me.currMovie.Movie.Runtime, Me.currMovie.Movie.Runtime.IndexOf("|")), Me.currMovie.Movie.Runtime)).Trim
            End If

            If Me.currMovie.Movie.Top250Specified AndAlso Integer.TryParse(Me.currMovie.Movie.Top250, 0) AndAlso (Integer.TryParse(Me.currMovie.Movie.Top250, 0) AndAlso Convert.ToInt32(Me.currMovie.Movie.Top250) > 0) Then
                Me.pnlTop250.Visible = True
                Me.lblTop250.Text = Me.currMovie.Movie.Top250
            Else
                Me.pnlTop250.Visible = False
            End If

            Me.txtOutline.Text = Me.currMovie.Movie.Outline
            Me.txtPlot.Text = Me.currMovie.Movie.Plot
            Me.lblTagline.Text = Me.currMovie.Movie.Tagline

            Me.alActors = New List(Of String)

            If Me.currMovie.Movie.ActorsSpecified Then
                Me.pbActors.Image = My.Resources.actor_silhouette
                For Each imdbAct As MediaContainers.Person In Me.currMovie.Movie.Actors
                    If Not String.IsNullOrEmpty(imdbAct.ThumbPath) AndAlso File.Exists(imdbAct.ThumbPath) Then
                        If Not imdbAct.ThumbURL.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.ThumbURL.ToLower.IndexOf("no_photo") > 0 Then
                            Me.alActors.Add(imdbAct.ThumbPath)
                        Else
                            Me.alActors.Add("none")
                        End If
                    ElseIf Not String.IsNullOrEmpty(imdbAct.ThumbURL) Then
                        If Not imdbAct.ThumbURL.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.ThumbURL.ToLower.IndexOf("no_photo") > 0 Then
                            Me.alActors.Add(imdbAct.ThumbURL)
                        Else
                            Me.alActors.Add("none")
                        End If
                    Else
                        Me.alActors.Add("none")
                    End If

                    If String.IsNullOrEmpty(imdbAct.Role.Trim) Then
                        Me.lstActors.Items.Add(imdbAct.Name.Trim)
                    Else
                        Me.lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), imdbAct.Name.Trim, imdbAct.Role.Trim))
                    End If
                Next
                Me.lstActors.SelectedIndex = 0
            End If

            If Me.currMovie.Movie.MPAASpecified Then
                Dim tmpRatingImg As Image = APIXML.GetRatingImage(Me.currMovie.Movie.MPAA)
                If tmpRatingImg IsNot Nothing Then
                    Me.pbMPAA.Image = tmpRatingImg
                    Me.MoveMPAA()
                End If
            End If

            Dim tmpRating As Single = NumUtils.ConvertToSingle(Me.currMovie.Movie.Rating)
            If tmpRating > 0 Then
                Me.BuildStars(tmpRating)
            End If

            If Me.currMovie.Movie.GenresSpecified Then
                Me.createGenreThumbs(Me.currMovie.Movie.Genres)
            End If

            If Me.currMovie.Movie.StudiosSpecified Then
                Me.pbStudio.Image = APIXML.GetStudioImage(Me.currMovie.Movie.Studios.Item(0).ToLower) 'ByDef all images file a lower case
                Me.pbStudio.Tag = Me.currMovie.Movie.Studios.Item(0)
            Else
                Me.pbStudio.Image = APIXML.GetStudioImage("####")
                Me.pbStudio.Tag = String.Empty
            End If

            'If Not String.IsNullOrEmpty(Master.currMovie.Movie.Studio) Then
            '    Me.pbStudio.Image = APIXML.GetStudioImage(Master.currMovie.Movie.Studio.ToLower) 'ByDef all images file a lower case
            '    Me.pbStudio.Tag = Master.currMovie.Movie.Studio
            'Else
            '    Me.pbStudio.Image = APIXML.GetStudioImage("####")
            '    Me.pbStudio.Tag = String.Empty
            'End If
            If clsAdvancedSettings.GetBooleanSetting("StudioTagAlwaysOn", False) Then
                lblStudio.Text = pbStudio.Tag.ToString
            End If
            If Master.eSettings.MovieScraperMetaDataScan Then
                'Me.SetAVImages(APIXML.GetAVImages(Master.currMovie.Movie.FileInfo, Master.currMovie.Filename, False))
                Me.SetAVImages(APIXML.GetAVImages(Me.currMovie.Movie.FileInfo, Me.currMovie.Filename, False, Me.currMovie.Movie.VideoSource))
                Me.pnlInfoIcons.Width = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + pbStudio.Width + 6
                Me.pbStudio.Left = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + 5
            Else
                Me.pnlInfoIcons.Width = pbStudio.Width + 1
                Me.pbStudio.Left = 0
            End If

            Me.lblDirector.Text = String.Join(" / ", Me.currMovie.Movie.Directors.ToArray)

            Me.txtIMDBID.Text = Me.currMovie.Movie.IMDBID
            Me.txtTMDBID.Text = Me.currMovie.Movie.TMDBID

            Me.txtFilePath.Text = Me.currMovie.Filename

            Me.lblReleaseDate.Text = Me.currMovie.Movie.ReleaseDate
            Me.txtCerts.Text = String.Join(" / ", Me.currMovie.Movie.Certifications.ToArray)

            Me.txtMetaData.Text = NFO.FIToString(Me.currMovie.Movie.FileInfo, False)

            FillScreenInfoWithImages()

            Me.InfoCleared = False

            If Not bwMovieScraper.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso Not bwCleanDB.IsBusy AndAlso Not Me.bwNonScrape.IsBusy Then
                Me.SetControlsEnabled(True)
                Me.EnableFilters_Movies(True)
            Else
                Me.dgvMovies.Enabled = True
            End If

            If bDoingSearch_Movies Then
                Me.txtSearchMovies.Focus()
                bDoingSearch_Movies = False
            Else
                Me.dgvMovies.Focus()
            End If


            Application.DoEvents()

            Me.pnlTop.Visible = True
            If Me.pbBanner.Image IsNot Nothing Then Me.pnlBanner.Visible = True
            If Me.pbClearArt.Image IsNot Nothing Then Me.pnlClearArt.Visible = True
            If Me.pbClearLogo.Image IsNot Nothing Then Me.pnlClearLogo.Visible = True
            If Me.pbDiscArt.Image IsNot Nothing Then Me.pnlDiscArt.Visible = True
            If Me.pbFanartSmall.Image IsNot Nothing Then Me.pnlFanartSmall.Visible = True
            If Me.pbLandscape.Image IsNot Nothing Then Me.pnlLandscape.Visible = True
            If Me.pbPoster.Image IsNot Nothing Then Me.pnlPoster.Visible = True
            If Me.pbMPAA.Image IsNot Nothing Then Me.pnlMPAA.Visible = True
            For i As Integer = 0 To Me.pnlGenre.Count - 1
                Me.pnlGenre(i).Visible = True
            Next

            Me.SetStatus(Me.currMovie.Filename)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Me.ResumeLayout()
    End Sub

    Private Sub FillScreenInfoWith_MovieSet()
        Try
            Me.SuspendLayout()
            If Me.currMovieSet.MovieSet.TitleSpecified AndAlso Me.currMovieSet.MovieList IsNot Nothing AndAlso Me.currMovieSet.MovieList.Count > 0 Then
                Me.lblTitle.Text = String.Format("{0} ({1})", Me.currMovieSet.MovieSet.Title, Me.currMovieSet.MovieList.Count)
            ElseIf Me.currMovieSet.MovieSet.TitleSpecified Then
                Me.lblTitle.Text = Me.currMovieSet.MovieSet.Title
            Else
                Me.lblTitle.Text = String.Empty
            End If

            Me.txtPlot.Text = Me.currMovieSet.MovieSet.Plot

            If Me.currMovieSet.MovieList IsNot Nothing AndAlso Me.currMovieSet.MovieList.Count > 0 Then
                Me.bwLoadMovieSetPosters.WorkerSupportsCancellation = True
                Me.bwLoadMovieSetPosters.RunWorkerAsync()
            End If

            FillScreenInfoWithImages()

            Me.InfoCleared = False

            If Not bwMovieSetScraper.IsBusy AndAlso Not bwReload_MovieSets.IsBusy AndAlso Not bwCleanDB.IsBusy AndAlso Not Me.bwNonScrape.IsBusy Then
                Me.SetControlsEnabled(True)
                Me.EnableFilters_MovieSets(True)
            Else
                Me.dgvMovieSets.Enabled = True
            End If

            If bDoingSearch_MovieSets Then
                Me.txtSearchMovieSets.Focus()
                bDoingSearch_MovieSets = False
            Else
                Me.dgvMovieSets.Focus()
            End If


            Application.DoEvents()

            Me.pnlTop.Visible = True
            If Me.pbBanner.Image IsNot Nothing Then Me.pnlBanner.Visible = True
            If Me.pbClearArt.Image IsNot Nothing Then Me.pnlClearArt.Visible = True
            If Me.pbClearLogo.Image IsNot Nothing Then Me.pnlClearLogo.Visible = True
            If Me.pbDiscArt.Image IsNot Nothing Then Me.pnlDiscArt.Visible = True
            If Me.pbFanartSmall.Image IsNot Nothing Then Me.pnlFanartSmall.Visible = True
            If Me.pbLandscape.Image IsNot Nothing Then Me.pnlLandscape.Visible = True
            If Me.pbPoster.Image IsNot Nothing Then Me.pnlPoster.Visible = True
            If Me.pbMPAA.Image IsNot Nothing Then Me.pnlMPAA.Visible = True
            For i As Integer = 0 To Me.pnlGenre.Count - 1
                Me.pnlGenre(i).Visible = True
            Next
            'Me.SetStatus(Master.currMovie.Filename)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Me.ResumeLayout()
    End Sub

    Private Sub FillScreenInfoWith_TVEpisode()
        Try
            Me.SuspendLayout()
            Me.lblTitle.Text = If(String.IsNullOrEmpty(Me.currTV.Filename), String.Concat(Me.currTV.TVEpisode.Title, " ", Master.eLang.GetString(689, "[MISSING]")), Me.currTV.TVEpisode.Title)
            Me.txtPlot.Text = Me.currTV.TVEpisode.Plot
            Me.lblDirector.Text = String.Join(" / ", Me.currTV.TVEpisode.Directors.ToArray)
            Me.txtFilePath.Text = Me.currTV.Filename
            Me.lblRuntime.Text = String.Format(Master.eLang.GetString(647, "Aired: {0}"), If(String.IsNullOrEmpty(Me.currTV.TVEpisode.Aired), "?", Me.currTV.TVEpisode.Aired))

            Try
                If Me.currTV.TVEpisode.RatingSpecified Then
                    If Me.currTV.TVEpisode.VotesSpecified Then
                        Dim strRating As String = Double.Parse(Me.currTV.TVEpisode.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                        Dim strVotes As String = Double.Parse(Me.currTV.TVEpisode.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                        Me.lblRating.Text = String.Concat(strRating, "/10 (", String.Format(Master.eLang.GetString(118, "{0} Votes"), strVotes), ")")
                    Else
                        Dim strRating As String = Double.Parse(Me.currTV.TVEpisode.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                        Me.lblRating.Text = String.Concat(strRating, "/10")
                    End If
                End If
            Catch ex As Exception
                logger.Error(String.Concat("Error: Not valid Rating or Votes (", Me.currTV.TVEpisode.Rating, " / ", Me.currTV.TVEpisode.Votes, ")"))
                Me.lblRating.Text = "Error: Please rescrape Rating and Votes"
            End Try

            Me.lblTagline.Text = String.Format(Master.eLang.GetString(648, "Season: {0}, Episode: {1}"), _
                            If(String.IsNullOrEmpty(Me.currTV.TVEpisode.Season.ToString), "?", Me.currTV.TVEpisode.Season.ToString), _
                            If(String.IsNullOrEmpty(Me.currTV.TVEpisode.Episode.ToString), "?", Me.currTV.TVEpisode.Episode.ToString))

            Me.alActors = New List(Of String)

            If Me.currTV.TVEpisode.ActorsSpecified Then
                Me.pbActors.Image = My.Resources.actor_silhouette
                For Each imdbAct As MediaContainers.Person In Me.currTV.TVEpisode.Actors
                    If Not String.IsNullOrEmpty(imdbAct.ThumbPath) AndAlso File.Exists(imdbAct.ThumbPath) Then
                        If Not imdbAct.ThumbURL.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.ThumbURL.ToLower.IndexOf("no_photo") > 0 Then
                            Me.alActors.Add(imdbAct.ThumbPath)
                        Else
                            Me.alActors.Add("none")
                        End If
                    ElseIf Not String.IsNullOrEmpty(imdbAct.ThumbURL) Then
                        If Not imdbAct.ThumbURL.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.ThumbURL.ToLower.IndexOf("no_photo") > 0 Then
                            Me.alActors.Add(imdbAct.ThumbURL)
                        Else
                            Me.alActors.Add("none")
                        End If
                    Else
                        Me.alActors.Add("none")
                    End If

                    If String.IsNullOrEmpty(imdbAct.Role.Trim) Then
                        Me.lstActors.Items.Add(imdbAct.Name.Trim)
                    Else
                        Me.lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), imdbAct.Name.Trim, imdbAct.Role.Trim))
                    End If
                Next
                Me.lstActors.SelectedIndex = 0
            End If

            If Me.currTV.TVShow.MPAASpecified Then
                Dim tmpRatingImg As Image = APIXML.GetTVRatingImage(Me.currTV.TVShow.MPAA)
                If tmpRatingImg IsNot Nothing Then
                    Me.pbMPAA.Image = tmpRatingImg
                    Me.MoveMPAA()
                End If
            End If

            Dim tmpRating As Single = NumUtils.ConvertToSingle(Me.currTV.TVEpisode.Rating)
            If tmpRating > 0 Then
                Me.BuildStars(tmpRating)
            End If

            If Me.currTV.TVShow.GenresSpecified Then
                Me.createGenreThumbs(Me.currTV.TVShow.Genres)
            End If

            If Me.currTV.TVShow.StudiosSpecified Then
                Me.pbStudio.Image = APIXML.GetStudioImage(Me.currTV.TVShow.Studio.ToLower) 'ByDef all images file a lower case
                Me.pbStudio.Tag = Me.currTV.TVShow.Studio
            Else
                Me.pbStudio.Image = APIXML.GetStudioImage("####")
                Me.pbStudio.Tag = String.Empty
            End If
            If clsAdvancedSettings.GetBooleanSetting("StudioTagAlwaysOn", False) Then
                lblStudio.Text = pbStudio.Tag.ToString
            End If
            If Master.eSettings.TVScraperMetaDataScan AndAlso Not String.IsNullOrEmpty(Me.currTV.Filename) Then
                Me.SetAVImages(APIXML.GetAVImages(Me.currTV.TVEpisode.FileInfo, Me.currTV.Filename, True, Me.currTV.TVEpisode.VideoSource))
                Me.pnlInfoIcons.Width = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + pbStudio.Width + 6
                Me.pbStudio.Left = pbVideo.Width + pbVType.Width + pbResolution.Width + pbAudio.Width + pbChannels.Width + 5
            Else
                Me.pnlInfoIcons.Width = pbStudio.Width + 1
                Me.pbStudio.Left = 0
            End If

            Me.txtMetaData.Text = NFO.FIToString(Me.currTV.TVEpisode.FileInfo, True)

            FillScreenInfoWithImages()

            Me.InfoCleared = False

            If Not bwMovieScraper.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                Me.SetControlsEnabled(True)
                Me.dgvTVEpisodes.Focus()
            Else
                Me.dgvTVEpisodes.Enabled = True
                Me.dgvTVSeasons.Enabled = True
                Me.dgvTVShows.Enabled = True
                Me.dgvTVEpisodes.Focus()
            End If

            Application.DoEvents()

            Me.pnlTop.Visible = True
            If Me.pbFanartSmall.Image IsNot Nothing Then Me.pnlFanartSmall.Visible = True
            If Me.pbPoster.Image IsNot Nothing Then Me.pnlPoster.Visible = True
            If Me.pbMPAA.Image IsNot Nothing Then Me.pnlMPAA.Visible = True
            For i As Integer = 0 To Me.pnlGenre.Count - 1
                Me.pnlGenre(i).Visible = True
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Me.ResumeLayout()
    End Sub

    Private Sub FillScreenInfoWith_TVSeason()
        Me.SuspendLayout()
        If Me.currTV.TVShow.TitleSpecified Then
            Me.lblTitle.Text = Me.currTV.TVShow.Title
        End If

        Me.txtPlot.Text = Me.currTV.TVSeason.Plot
        Me.lblRuntime.Text = String.Format(Master.eLang.GetString(645, "Premiered: {0}"), If(Me.currTV.TVShow.PremieredSpecified, Me.currTV.TVShow.Premiered, "?"))

        Try
            If Me.currTV.TVShow.RatingSpecified Then
                If Me.currTV.TVShow.VotesSpecified Then
                    Dim strRating As String = Double.Parse(Me.currTV.TVShow.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                    Dim strVotes As String = Double.Parse(Me.currTV.TVShow.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                    Me.lblRating.Text = String.Concat(strRating, "/10 (", String.Format(Master.eLang.GetString(118, "{0} Votes"), strVotes), ")")
                Else
                    Dim strRating As String = Double.Parse(Me.currTV.TVShow.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                    Me.lblRating.Text = String.Concat(strRating, "/10")
                End If
            End If
        Catch ex As Exception
            logger.Error(String.Concat("Error: Not valid Rating or Votes (", Me.currTV.TVShow.Rating, " / ", Me.currTV.TVShow.Votes, ")"))
            Me.lblRating.Text = "Error: Please rescrape Rating and Votes"
        End Try

        Me.alActors = New List(Of String)

        If Me.currTV.TVShow.ActorsSpecified Then
            Me.pbActors.Image = My.Resources.actor_silhouette
            For Each imdbAct As MediaContainers.Person In Me.currTV.TVShow.Actors
                If Not String.IsNullOrEmpty(imdbAct.ThumbPath) AndAlso File.Exists(imdbAct.ThumbPath) Then
                    If Not imdbAct.ThumbURL.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.ThumbURL.ToLower.IndexOf("no_photo") > 0 Then
                        Me.alActors.Add(imdbAct.ThumbPath)
                    Else
                        Me.alActors.Add("none")
                    End If
                ElseIf Not String.IsNullOrEmpty(imdbAct.ThumbURL) Then
                    If Not imdbAct.ThumbURL.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.ThumbURL.ToLower.IndexOf("no_photo") > 0 Then
                        Me.alActors.Add(imdbAct.ThumbURL)
                    Else
                        Me.alActors.Add("none")
                    End If
                Else
                    Me.alActors.Add("none")
                End If

                If String.IsNullOrEmpty(imdbAct.Role.Trim) Then
                    Me.lstActors.Items.Add(imdbAct.Name.Trim)
                Else
                    Me.lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), imdbAct.Name.Trim, imdbAct.Role.Trim))
                End If
            Next
            Me.lstActors.SelectedIndex = 0
        End If

        If Me.currTV.TVShow.MPAASpecified Then
            Dim tmpRatingImg As Image = APIXML.GetTVRatingImage(Me.currTV.TVShow.MPAA)
            If tmpRatingImg IsNot Nothing Then
                Me.pbMPAA.Image = tmpRatingImg
                Me.MoveMPAA()
            End If
        End If

        Dim tmpRating As Single = NumUtils.ConvertToSingle(Me.currTV.TVShow.Rating)
        If tmpRating > 0 Then
            Me.BuildStars(tmpRating)
        End If

        If Me.currTV.TVShow.Genres.Count > 0 Then
            Me.createGenreThumbs(Me.currTV.TVShow.Genres)
        End If

        If Me.currTV.TVShow.StudiosSpecified Then
            Me.pbStudio.Image = APIXML.GetStudioImage(Me.currTV.TVShow.Studio.ToLower) 'ByDef all images file a lower case
            Me.pbStudio.Tag = Me.currTV.TVShow.Studio
        Else
            Me.pbStudio.Image = APIXML.GetStudioImage("####")
            Me.pbStudio.Tag = String.Empty
        End If

        Me.pnlInfoIcons.Width = pbStudio.Width + 1
        Me.pbStudio.Left = 0

        FillScreenInfoWithImages()

        Me.InfoCleared = False

        If Not bwMovieScraper.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso Not bwCleanDB.IsBusy Then
            Me.SetControlsEnabled(True)
            Me.dgvTVSeasons.Focus()
        Else
            Me.dgvTVEpisodes.Enabled = True
            Me.dgvTVSeasons.Enabled = True
            Me.dgvTVShows.Enabled = True
            Me.dgvTVSeasons.Focus()
        End If

        Application.DoEvents()

        Me.pnlTop.Visible = True
        If Me.pbBanner.Image IsNot Nothing Then Me.pnlBanner.Visible = True
        If Me.pbFanartSmall.Image IsNot Nothing Then Me.pnlFanartSmall.Visible = True
        If Me.pbLandscape.Image IsNot Nothing Then Me.pnlLandscape.Visible = True
        If Me.pbPoster.Image IsNot Nothing Then Me.pnlPoster.Visible = True
        If Me.pbMPAA.Image IsNot Nothing Then Me.pnlMPAA.Visible = True
        For i As Integer = 0 To Me.pnlGenre.Count - 1
            Me.pnlGenre(i).Visible = True
        Next

        Me.ResumeLayout()
    End Sub

    Private Sub FillScreenInfoWith_TVShow()
        Try
            Me.SuspendLayout()
            If Me.currTV.TVShow.TitleSpecified Then
                Me.lblTitle.Text = Me.currTV.TVShow.Title
            End If

            Me.lblOriginalTitle.Text = String.Empty
            Me.txtPlot.Text = Me.currTV.TVShow.Plot
            Me.lblRuntime.Text = String.Format(Master.eLang.GetString(645, "Premiered: {0}"), If(String.IsNullOrEmpty(Me.currTV.TVShow.Premiered), "?", Me.currTV.TVShow.Premiered))

            Try
                If Me.currTV.TVShow.RatingSpecified Then
                    If Me.currTV.TVShow.VotesSpecified Then
                        Dim strRating As String = Double.Parse(Me.currTV.TVShow.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                        Dim strVotes As String = Double.Parse(Me.currTV.TVShow.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                        Me.lblRating.Text = String.Concat(strRating, "/10 (", String.Format(Master.eLang.GetString(118, "{0} Votes"), strVotes), ")")
                    Else
                        Dim strRating As String = Double.Parse(Me.currTV.TVShow.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                        Me.lblRating.Text = String.Concat(strRating, "/10")
                    End If
                End If
            Catch ex As Exception
                logger.Error(String.Concat("Error: Not valid Rating or Votes (", Me.currTV.TVShow.Rating, " / ", Me.currTV.TVShow.Votes, ")"))
                Me.lblRating.Text = "Error: Please rescrape Rating and Votes"
            End Try

            Me.alActors = New List(Of String)

            If Me.currTV.TVShow.ActorsSpecified Then
                Me.pbActors.Image = My.Resources.actor_silhouette
                For Each imdbAct As MediaContainers.Person In Me.currTV.TVShow.Actors
                    If Not String.IsNullOrEmpty(imdbAct.ThumbPath) AndAlso File.Exists(imdbAct.ThumbPath) Then
                        If Not imdbAct.ThumbURL.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.ThumbURL.ToLower.IndexOf("no_photo") > 0 Then
                            Me.alActors.Add(imdbAct.ThumbPath)
                        Else
                            Me.alActors.Add("none")
                        End If
                    ElseIf Not String.IsNullOrEmpty(imdbAct.ThumbURL) Then
                        If Not imdbAct.ThumbURL.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not imdbAct.ThumbURL.ToLower.IndexOf("no_photo") > 0 Then
                            Me.alActors.Add(imdbAct.ThumbURL)
                        Else
                            Me.alActors.Add("none")
                        End If
                    Else
                        Me.alActors.Add("none")
                    End If

                    If String.IsNullOrEmpty(imdbAct.Role.Trim) Then
                        Me.lstActors.Items.Add(imdbAct.Name.Trim)
                    Else
                        Me.lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), imdbAct.Name.Trim, imdbAct.Role.Trim))
                    End If
                Next
                Me.lstActors.SelectedIndex = 0
            End If

            If Me.currTV.TVShow.MPAASpecified Then
                Dim tmpRatingImg As Image = APIXML.GetTVRatingImage(Me.currTV.TVShow.MPAA)
                If tmpRatingImg IsNot Nothing Then
                    Me.pbMPAA.Image = tmpRatingImg
                    Me.MoveMPAA()
                End If
            End If

            Dim tmpRating As Single = NumUtils.ConvertToSingle(Me.currTV.TVShow.Rating)
            If tmpRating > 0 Then
                Me.BuildStars(tmpRating)
            End If

            If Me.currTV.TVShow.Genres.Count > 0 Then
                Me.createGenreThumbs(Me.currTV.TVShow.Genres)
            End If

            If Me.currTV.TVShow.StudiosSpecified Then
                Me.pbStudio.Image = APIXML.GetStudioImage(Me.currTV.TVShow.Studio.ToLower) 'ByDef all images file a lower case
                Me.pbStudio.Tag = Me.currTV.TVShow.Studio
            Else
                Me.pbStudio.Image = APIXML.GetStudioImage("####")
                Me.pbStudio.Tag = String.Empty
            End If

            Me.pnlInfoIcons.Width = pbStudio.Width + 1
            Me.pbStudio.Left = 0

            FillScreenInfoWithImages()

            Me.InfoCleared = False

            If Not bwMovieScraper.IsBusy AndAlso Not bwReload_Movies.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                Me.SetControlsEnabled(True)
                Me.EnableFilters_Shows(True)
            Else
                Me.dgvTVEpisodes.Enabled = True
                Me.dgvTVSeasons.Enabled = True
                Me.dgvTVShows.Enabled = True
            End If

            If bDoingSearch_Shows Then
                Me.txtSearchShows.Focus()
                bDoingSearch_Shows = False
            Else
                Me.dgvTVShows.Focus()
            End If

            Application.DoEvents()

            Me.pnlTop.Visible = True
            If Me.pbBanner.Image IsNot Nothing Then Me.pnlBanner.Visible = True
            If Me.pbCharacterArt.Image IsNot Nothing Then Me.pnlCharacterArt.Visible = True
            If Me.pbClearArt.Image IsNot Nothing Then Me.pnlClearArt.Visible = True
            If Me.pbClearLogo.Image IsNot Nothing Then Me.pnlClearLogo.Visible = True
            If Me.pbFanartSmall.Image IsNot Nothing Then Me.pnlFanartSmall.Visible = True
            If Me.pbLandscape.Image IsNot Nothing Then Me.pnlLandscape.Visible = True
            If Me.pbPoster.Image IsNot Nothing Then Me.pnlPoster.Visible = True
            If Me.pbMPAA.Image IsNot Nothing Then Me.pnlMPAA.Visible = True
            For i As Integer = 0 To Me.pnlGenre.Count - 1
                Me.pnlGenre(i).Visible = True
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Me.ResumeLayout()
    End Sub

    Private Sub FillSeasons(ByVal ShowID As Integer)
        Me.bsTVSeasons.DataSource = Nothing
        Me.dgvTVSeasons.DataSource = Nothing
        Me.bsTVEpisodes.DataSource = Nothing
        Me.dgvTVEpisodes.DataSource = Nothing

        Application.DoEvents()

        Me.dgvTVSeasons.Enabled = False

        If Master.eSettings.TVDisplayMissingEpisodes Then
            Master.DB.FillDataTable(Me.dtTVSeasons, String.Concat("SELECT * FROM seasonslist WHERE idShow = ", ShowID, " ORDER BY Season;"))
        Else
            Master.DB.FillDataTable(Me.dtTVSeasons, String.Concat("SELECT DISTINCT seasonslist.* ", _
                                                                "FROM seasonslist ", _
                                                                "LEFT OUTER JOIN episodelist ON (seasonslist.idShow = episodelist.idShow) AND (seasonslist.Season = episodelist.Season) ", _
                                                                "WHERE seasonslist.idShow = ", ShowID, " AND (episodelist.Missing = 0 OR seasonslist.Season = 999) ", _
                                                                "ORDER BY seasonslist.Season;"))
        End If

        If Me.dtTVSeasons.Rows.Count > 0 Then

            With Me
                .bsTVSeasons.DataSource = .dtTVSeasons
                .dgvTVSeasons.DataSource = .bsTVSeasons

                If Me.dgvTVSeasons.Columns.Count > 0 Then
                    Try
                        If Master.eSettings.TVGeneralSeasonListSorting.Count > 0 Then
                            For Each mColumn In Master.eSettings.TVGeneralSeasonListSorting
                                Me.dgvTVSeasons.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                            Next
                        End If
                    Catch ex As Exception
                        logger.Warn("default list for season list sorting has been loaded")
                        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVSeasonListSorting, True)
                        If Master.eSettings.TVGeneralSeasonListSorting.Count > 0 Then
                            For Each mColumn In Master.eSettings.TVGeneralSeasonListSorting
                                Me.dgvTVSeasons.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                            Next
                        End If
                    End Try
                End If

                For i As Integer = 0 To .dgvTVSeasons.Columns.Count - 1
                    .dgvTVSeasons.Columns(i).Visible = False
                Next

                .dgvTVSeasons.Columns("BannerPath").Width = 20
                .dgvTVSeasons.Columns("BannerPath").Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns("BannerPath").ReadOnly = True
                .dgvTVSeasons.Columns("BannerPath").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns("BannerPath").Visible = Not CheckColumnHide_TVSeasons("BannerPath")
                .dgvTVSeasons.Columns("BannerPath").ToolTipText = Master.eLang.GetString(838, "Banner")
                .dgvTVSeasons.Columns("Episodes").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
                .dgvTVSeasons.Columns("Episodes").MinimumWidth = 30
                .dgvTVSeasons.Columns("Episodes").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .dgvTVSeasons.Columns("Episodes").Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns("Episodes").ReadOnly = True
                .dgvTVSeasons.Columns("Episodes").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns("Episodes").Visible = Not CheckColumnHide_TVSeasons("Episodes")
                .dgvTVSeasons.Columns("Episodes").ToolTipText = Master.eLang.GetString(682, "Episodes")
                .dgvTVSeasons.Columns("Episodes").HeaderText = String.Empty
                .dgvTVSeasons.Columns("FanartPath").Width = 20
                .dgvTVSeasons.Columns("FanartPath").Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns("FanartPath").ReadOnly = True
                .dgvTVSeasons.Columns("FanartPath").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns("FanartPath").Visible = Not CheckColumnHide_TVSeasons("FanartPath")
                .dgvTVSeasons.Columns("FanartPath").ToolTipText = Master.eLang.GetString(149, "Fanart")
                .dgvTVSeasons.Columns("HasWatched").Width = 20
                .dgvTVSeasons.Columns("HasWatched").Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns("HasWatched").ReadOnly = True
                .dgvTVSeasons.Columns("HasWatched").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns("HasWatched").Visible = Not CheckColumnHide_TVSeasons("HasWatched")
                .dgvTVSeasons.Columns("HasWatched").ToolTipText = Master.eLang.GetString(981, "Watched")
                .dgvTVSeasons.Columns("LandscapePath").Width = 20
                .dgvTVSeasons.Columns("LandscapePath").Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns("LandscapePath").ReadOnly = True
                .dgvTVSeasons.Columns("LandscapePath").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns("LandscapePath").Visible = Not CheckColumnHide_TVSeasons("LandscapePath")
                .dgvTVSeasons.Columns("LandscapePath").ToolTipText = Master.eLang.GetString(1035, "Landscape")
                .dgvTVSeasons.Columns("PosterPath").Width = 20
                .dgvTVSeasons.Columns("PosterPath").Resizable = DataGridViewTriState.False
                .dgvTVSeasons.Columns("PosterPath").ReadOnly = True
                .dgvTVSeasons.Columns("PosterPath").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns("PosterPath").Visible = Not CheckColumnHide_TVSeasons("PosterPath")
                .dgvTVSeasons.Columns("PosterPath").ToolTipText = Master.eLang.GetString(148, "Poster")
                .dgvTVSeasons.Columns("SeasonText").Resizable = DataGridViewTriState.True
                .dgvTVSeasons.Columns("SeasonText").ReadOnly = True
                .dgvTVSeasons.Columns("SeasonText").MinimumWidth = 83
                .dgvTVSeasons.Columns("SeasonText").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvTVSeasons.Columns("SeasonText").Visible = True
                .dgvTVSeasons.Columns("SeasonText").ToolTipText = Master.eLang.GetString(650, "Season")
                .dgvTVSeasons.Columns("SeasonText").HeaderText = Master.eLang.GetString(650, "Season")

                .dgvTVSeasons.Columns("idSeason").ValueType = GetType(Int32)
                .dgvTVSeasons.Columns("idShow").ValueType = GetType(Int32)

                If Master.isWindows Then .dgvTVSeasons.Columns("SeasonText").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                ResizeTVLists(.dgvTVSeasons.Columns("SeasonText").Index)

                .dgvTVSeasons.Sort(.dgvTVSeasons.Columns("SeasonText"), ComponentModel.ListSortDirection.Ascending)

                Me.FillEpisodes(ShowID, Convert.ToInt32(.dgvTVSeasons.Item("Season", 0).Value))
            End With
        End If

        Me.dgvTVSeasons.Enabled = True
    End Sub

    Private Sub frmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        logger.Info("====Ember Media Manager exiting====")
    End Sub
    ''' <summary>
    ''' The FormClosing event has been called, so prepare the form to shut down
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            Dim doSave As Boolean = True

            Me.SetControlsEnabled(False, True)
            Me.EnableFilters_Movies(False)
            Me.EnableFilters_MovieSets(False)
            Me.EnableFilters_Shows(False)

            Master.eSettings.Version = String.Format("r{0}", My.Application.Info.Version.Revision)

            If Me.fScanner.IsBusy OrElse Master.isCL Then
                doSave = False
            End If

            If Me.fScanner.IsBusy Then Me.fScanner.Cancel()
            If Me.bwMetaData.IsBusy Then Me.bwMetaData.CancelAsync()
            If Me.bwLoadMovieInfo.IsBusy Then Me.bwLoadMovieInfo.CancelAsync()
            If Me.bwLoadMovieSetInfo.IsBusy Then Me.bwLoadMovieSetInfo.CancelAsync()
            If Me.bwLoadMovieSetPosters.IsBusy Then Me.bwLoadMovieSetPosters.CancelAsync()
            If Me.bwLoadShowInfo.IsBusy Then Me.bwLoadShowInfo.CancelAsync()
            If Me.bwLoadSeasonInfo.IsBusy Then Me.bwLoadSeasonInfo.CancelAsync()
            If Me.bwLoadEpInfo.IsBusy Then Me.bwLoadEpInfo.CancelAsync()
            If Me.bwDownloadPic.IsBusy Then Me.bwDownloadPic.CancelAsync()
            If Me.bwReload_Movies.IsBusy Then Me.bwReload_Movies.CancelAsync()
            If Me.bwCleanDB.IsBusy Then Me.bwCleanDB.CancelAsync()
            If Me.bwMovieScraper.IsBusy Then Me.bwMovieScraper.CancelAsync()

            lblCanceling.Text = Master.eLang.GetString(99, "Canceling All Processes...")
            btnCancel.Visible = False
            lblCanceling.Visible = True
            prbCanceling.Visible = True
            pnlCancel.Visible = True
            Me.Refresh()

            While Me.fScanner.IsBusy OrElse Me.bwMetaData.IsBusy OrElse Me.bwLoadMovieInfo.IsBusy _
            OrElse Me.bwLoadMovieSetInfo.IsBusy OrElse Me.bwDownloadPic.IsBusy OrElse Me.bwMovieScraper.IsBusy _
            OrElse Me.bwReload_Movies.IsBusy OrElse Me.bwReload_MovieSets.IsBusy OrElse Me.bwCleanDB.IsBusy _
            OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwLoadEpInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy _
            OrElse Me.bwLoadMovieSetPosters.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            If doSave Then Master.DB.ClearNew()

            If Not Master.isCL Then
                Master.DB.CloseMyVideosDB()
            End If

            If Not Master.isCL Then
                Master.eSettings.GeneralWindowLoc = Me.Location
                Master.eSettings.GeneralWindowSize = Me.Size
                Master.eSettings.GeneralWindowState = Me.WindowState
                Master.eSettings.GeneralMovieInfoPanelState = Me.MovieInfoPanelState
                Master.eSettings.GeneralMovieSetInfoPanelState = Me.MovieSetInfoPanelState
                Master.eSettings.GeneralTVShowInfoPanelState = Me.TVShowInfoPanelState
                Master.eSettings.GeneralFilterPanelStateMovie = Me.FilterRaise_Movies
                Master.eSettings.GeneralFilterPanelStateMovieSet = Me.FilterRaise_MovieSets
                Master.eSettings.GeneralFilterPanelStateShow = Me.FilterRaise_Shows
                Master.eSettings.GeneralMainSplitterPanelState = Me.scMain.SplitterDistance
                Me.pnlFilter_Movies.Visible = False
                Me.pnlFilter_MovieSets.Visible = False
                Me.pnlFilter_Shows.Visible = False
                Master.eSettings.GeneralSeasonSplitterPanelState = Me.scTVSeasonsEpisodes.SplitterDistance
                Master.eSettings.GeneralShowSplitterPanelState = Me.scTV.SplitterDistance
            End If
            If Not Me.WindowState = FormWindowState.Minimized Then Master.eSettings.Save()

        Catch ex As Exception
            ' If we got here, then some of the above not run. Application.Exit can not be used. 
            ' Because Exit will dispose object that are in use by BackgroundWorkers
            ' If any BackgroundWorker still running will raise exception 
            ' "Collection was modified; enumeration operation may not execute."
            ' Application.Exit()
        End Try
    End Sub
    ''' <summary>
    ''' The form is loading. This method occurs before the form is actually visible.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Visible = False
            logger.Info(New StackFrame().GetMethod().Name, "Ember startup")

            If Master.isWindows Then 'Dam mono on MacOSX don't have trayicon implemented yet
                Me.TrayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
                Me.TrayIcon.Icon = Me.Icon
                Me.TrayIcon.ContextMenuStrip = Me.cmnuTray
                Me.TrayIcon.Text = "Ember Media Manager"
                Me.TrayIcon.Visible = True
            End If

            Me.bwCheckVersion.RunWorkerAsync()

            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(854, "Basic setup"))

            Dim currentDomain As AppDomain = AppDomain.CurrentDomain
            ModulesManager.AssemblyList.Add(New ModulesManager.AssemblyListItem With {.AssemblyName = "EmberAPI", _
              .Assembly = Assembly.LoadFile(Path.Combine(Functions.AppPath, "EmberAPI.dll"))})
            'Cocotus 2014/01/25 By switching Ember to NET4.5 dependency , the LoadFile method is not supported anymore
            '  http://msdn.microsoft.com/de-de/library/system.reflection.assembly.loadfile(v=vs.110).aspx
            '.Assembly = Assembly.LoadFile(Path.Combine(Functions.AppPath, "EmberAPI.dll"), Assembly.GetExecutingAssembly().Evidence)})    
            AddHandler currentDomain.AssemblyResolve, AddressOf MyResolveEventHandler

            clsAdvancedSettings.Start()

            'Create Modules Folders
            Dim sPath = String.Concat(Functions.AppPath, "Modules")
            If Not Directory.Exists(sPath) Then
                Directory.CreateDirectory(sPath)
            End If

            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(855, "Creating default options..."))
            Functions.CreateDefaultOptions()
            '//
            ' Add our handlers, load settings, set form colors, and try to load movies at startup
            '\\
            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(856, "Loading modules..."))
            'Setup/Load Modules Manager and set runtime objects (ember application) so they can be exposed to modules
            'ExternalModulesManager = New ModulesManager

            Me.tpMovies.Tag = New Structures.MainTabType With {.ContentName = Master.eLang.GetString(36, "Movies"), .ContentType = Enums.ContentType.Movie, .DefaultList = "movielist"}
            Me.tpMovieSets.Tag = New Structures.MainTabType With {.ContentName = Master.eLang.GetString(366, "Sets"), .ContentType = Enums.ContentType.MovieSet, .DefaultList = "setslist"}
            Me.tpTVShows.Tag = New Structures.MainTabType With {.ContentName = Master.eLang.GetString(653, "TV Shows"), .ContentType = Enums.ContentType.TV, .DefaultList = "tvshowlist"}
            ModulesManager.Instance.RuntimeObjects.MediaTabSelected = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)

            ModulesManager.Instance.RuntimeObjects.DelegateLoadMedia(AddressOf LoadMedia)
            ModulesManager.Instance.RuntimeObjects.DelegateOpenImageViewer(AddressOf OpenImageViewer)
            ModulesManager.Instance.RuntimeObjects.MainTabControl = Me.tcMain
            ModulesManager.Instance.RuntimeObjects.MainTool = Me.tsMain
            ModulesManager.Instance.RuntimeObjects.MediaListMovies = Me.dgvMovies
            ModulesManager.Instance.RuntimeObjects.MediaListMovieSets = Me.dgvMovieSets
            ModulesManager.Instance.RuntimeObjects.MediaListTVEpisodes = Me.dgvTVEpisodes
            ModulesManager.Instance.RuntimeObjects.MediaListTVSeasons = Me.dgvTVSeasons
            ModulesManager.Instance.RuntimeObjects.MediaListTVShows = Me.dgvTVShows
            ModulesManager.Instance.RuntimeObjects.MenuMovieList = Me.cmnuMovie
            ModulesManager.Instance.RuntimeObjects.MenuMovieSetList = Me.cmnuMovieSet
            ModulesManager.Instance.RuntimeObjects.MenuTVEpisodeList = Me.cmnuEpisode
            ModulesManager.Instance.RuntimeObjects.MenuTVSeasonList = Me.cmnuSeason
            ModulesManager.Instance.RuntimeObjects.MenuTVShowList = Me.cmnuShow
            ModulesManager.Instance.RuntimeObjects.TopMenu = Me.mnuMain
            ModulesManager.Instance.RuntimeObjects.TrayMenu = Me.cmnuTray
            ModulesManager.Instance.LoadAllModules()

            If Not Master.isCL Then
                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(857, "Creating GUI..."))
            End If
            'setup some dummies so we don't get exceptions when resizing form/info panel
            ReDim Preserve Me.pnlGenre(0)
            ReDim Preserve Me.pbGenre(0)
            Me.pnlGenre(0) = New Panel()
            Me.pbGenre(0) = New PictureBox()

            AddHandler fScanner.ScannerUpdated, AddressOf ScannerUpdated
            AddHandler fScanner.ScanningCompleted, AddressOf ScanningCompleted
            AddHandler ModulesManager.Instance.GenericEvent, AddressOf Me.GenericRunCallBack
            AddHandler fCommandLine.TaskEvent, AddressOf Me.TaskRunCallBack

            Functions.DGVDoubleBuffer(Me.dgvMovies)
            Functions.DGVDoubleBuffer(Me.dgvMovieSets)
            Functions.DGVDoubleBuffer(Me.dgvTVShows)
            Functions.DGVDoubleBuffer(Me.dgvTVSeasons)
            Functions.DGVDoubleBuffer(Me.dgvTVEpisodes)
            SetStyle(ControlStyles.DoubleBuffer, True)
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            SetStyle(ControlStyles.UserPaint, True)

            If TypeOf tsMain.Renderer Is ToolStripProfessionalRenderer Then
                CType(tsMain.Renderer, ToolStripProfessionalRenderer).RoundedEdges = False
            End If

            If Not Directory.Exists(Master.TempPath) Then Directory.CreateDirectory(Master.TempPath)

            If Master.isCL Then ' Command Line
                LoadWithCommandLine(Master.appArgs)
            Else 'Regular Run (GUI)
                LoadWithGUI()
            End If
            While Not ModulesManager.Instance.ModulesLoaded()
                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(856, "Loading modules..."))
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
            Master.fLoading.Close()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.Close()
        End Try
    End Sub
    ''' <summary>
    ''' Performs startup routines specific to being initiated by the command line
    ''' </summary>
    ''' <param name="appArgs">Command line arguments. Must NOT be empty!</param>
    ''' <remarks></remarks>
    Private Sub LoadWithCommandLine(ByVal appArgs As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs)
        Dim Args() As String = appArgs.CommandLine.ToArray

        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(858, "Loading database..."))
        Master.DB.ConnectMyVideosDB()
        Master.DB.LoadMovieSourcesFromDB()
        Master.DB.LoadTVSourcesFromDB()
        Master.DB.LoadExcludeDirsFromDB()

        RemoveHandler dgvMovies.RowsAdded, AddressOf dgvMovies_RowsAdded
        RemoveHandler dgvMovieSets.RowsAdded, AddressOf dgvMovieSets_RowsAdded
        RemoveHandler dgvTVShows.RowsAdded, AddressOf dgvTVShows_RowsAdded
        Me.FillList(True, True, True)
        AddHandler dgvMovies.RowsAdded, AddressOf dgvMovies_RowsAdded
        AddHandler dgvMovieSets.RowsAdded, AddressOf dgvMovieSets_RowsAdded
        AddHandler dgvTVShows.RowsAdded, AddressOf dgvTVShows_RowsAdded

        fCommandLine.RunCommandLine(Args)

        While Not Me.TaskList.Count = 0 OrElse Not Me.TasksDone
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Master.fLoading.Close()
        Me.Close()

        'Try
        '    logger.Trace("LoadWithCommandLine()")

        '    Dim MoviePath As String = String.Empty
        '    Dim isSingle As Boolean = False
        '    Dim hasSpec As Boolean = False
        '    Dim clScrapeType As Enums.ScrapeType_Movie = Enums.ScrapeType_Movie.None
        '    Dim clExport As Boolean = False
        '    Dim clExportResizePoster As Integer = 0
        '    Dim clExportTemplate As String = "template"
        '    Dim clAsk As Boolean = False
        '    Dim nowindow As Boolean = False
        '    Dim RunModule As Boolean = False
        '    Dim ModuleName As String = String.Empty
        '    Dim UpdateTVShows As Boolean = False
        '    For i As Integer = 0 To Args.Count - 1

        '        Select Case Args(i).ToLower
        '            Case "-fullask"
        '                clScrapeType = Enums.ScrapeType_Movie.FullAsk
        '                clAsk = True
        '            Case "-fullauto"
        '                clScrapeType = Enums.ScrapeType_Movie.FullAuto
        '                clAsk = False
        '            Case "-fullskip"
        '                clScrapeType = Enums.ScrapeType_Movie.FullSkip
        '                clAsk = False
        '            Case "-missask"
        '                clScrapeType = Enums.ScrapeType_Movie.MissAsk
        '                clAsk = True
        '            Case "-missauto"
        '                clScrapeType = Enums.ScrapeType_Movie.MissAuto
        '                clAsk = False
        '            Case "-missskip"
        '                clScrapeType = Enums.ScrapeType_Movie.MissSkip
        '                clAsk = True
        '            Case "-newask"
        '                clScrapeType = Enums.ScrapeType_Movie.NewAsk
        '                clAsk = True
        '            Case "-newauto"
        '                clScrapeType = Enums.ScrapeType_Movie.NewAuto
        '                clAsk = False
        '            Case "-newskip"
        '                clScrapeType = Enums.ScrapeType_Movie.NewSkip
        '                clAsk = False
        '            Case "-markask"
        '                clScrapeType = Enums.ScrapeType_Movie.MarkAsk
        '                clAsk = True
        '            Case "-markauto"
        '                clScrapeType = Enums.ScrapeType_Movie.MarkAuto
        '                clAsk = False
        '            Case "-markskip"
        '                clScrapeType = Enums.ScrapeType_Movie.MarkSkip
        '                clAsk = True
        '            Case "-file"
        '                If Args.Count - 1 > i Then
        '                    isSingle = False
        '                    hasSpec = True
        '                    clScrapeType = Enums.ScrapeType_Movie.SingleScrape
        '                    If File.Exists(Args(i + 1).Replace("""", String.Empty)) Then
        '                        MoviePath = Args(i + 1).Replace("""", String.Empty)
        '                        i += 1
        '                    End If
        '                Else
        '                    Exit For
        '                End If
        '            Case "-folder"
        '                If Args.Count - 1 > i Then
        '                    isSingle = True
        '                    hasSpec = True
        '                    clScrapeType = Enums.ScrapeType_Movie.SingleScrape
        '                    If File.Exists(Args(i + 1).Replace("""", String.Empty)) Then
        '                        MoviePath = Args(i + 1).Replace("""", String.Empty)
        '                        i += 1
        '                    End If
        '                Else
        '                    Exit For
        '                End If
        '            Case "-export"
        '                If Args.Count - 1 > i Then
        '                    MoviePath = Args(i + 1).Replace("""", String.Empty)
        '                    clExport = True
        '                Else
        '                    Exit For
        '                End If
        '            Case "-template"
        '                If Args.Count - 1 > i Then
        '                    clExportTemplate = Args(i + 1).Replace("""", String.Empty)
        '                Else
        '                    Exit For
        '                End If
        '            Case "-resize"
        '                If Args.Count - 1 > i Then
        '                    clExportResizePoster = Convert.ToUInt16(Args(i + 1).Replace("""", String.Empty))
        '                Else
        '                    Exit For
        '                End If
        '            Case "-all"
        '                Functions.SetScraperMod(Enums.ModType.All, True)
        '            Case "-banner"
        '                Functions.SetScraperMod(Enums.ModType.Banner, True)
        '            Case "-clearart"
        '                Functions.SetScraperMod(Enums.ModType.ClearArt, True)
        '            Case "-clearlogo"
        '                Functions.SetScraperMod(Enums.ModType.ClearLogo, True)
        '            Case "-discart"
        '                Functions.SetScraperMod(Enums.ModType.DiscArt, True)
        '            Case "-efanarts"
        '                Functions.SetScraperMod(Enums.ModType.EFanarts, True)
        '            Case "-ethumbs"
        '                Functions.SetScraperMod(Enums.ModType.EThumbs, True)
        '            Case "-fanart"
        '                Functions.SetScraperMod(Enums.ModType.Fanart, True)
        '            Case "-landscape"
        '                Functions.SetScraperMod(Enums.ModType.Landscape, True)
        '            Case "-nfo"
        '                Functions.SetScraperMod(Enums.ModType.NFO, True)
        '            Case "-poster"
        '                Functions.SetScraperMod(Enums.ModType.Poster, True)
        '            Case "-theme"
        '                Functions.SetScraperMod(Enums.ModType.Theme, True)
        '            Case "-trailer"
        '                Functions.SetScraperMod(Enums.ModType.Trailer, True)
        '            Case "--verbose"
        '                clAsk = True
        '            Case "-nowindow"
        '                nowindow = True
        '            Case "-run"
        '                If Args.Count - 1 > i Then
        '                    ModuleName = Args(i + 1).Replace("""", String.Empty)
        '                    RunModule = True
        '                Else
        '                    Exit For
        '                End If
        '            Case "-tvupdate"
        '                UpdateTVShows = True
        '            Case Else
        '                'If File.Exists(Args(2).Replace("""", String.Empty)) Then
        '                'MoviePath = Args(2).Replace("""", String.Empty)
        '                'End If
        '        End Select
        '    Next
        '    If nowindow Then Master.fLoading.Hide()
        '    APIXML.CacheXMLs()
        '    Master.fLoading.SetLoadingMesg(Master.eLang.GetString(858, "Loading database..."))
        '    If Master.DB.ConnectMyVideosDB() Then
        '        Me.LoadMedia(New Structures.Scans With {.Movies = True, .TV = True})
        '    End If
        '    Master.DB.LoadMovieSourcesFromDB()
        '    Master.DB.LoadTVSourcesFromDB()
        '    Master.DB.LoadExcludeDirsFromDB()
        '    If RunModule Then
        '        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
        '        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(859, "Running Module..."))
        '        Dim gModule As ModulesManager._externalGenericModuleClass = ModulesManager.Instance.externalProcessorModules.FirstOrDefault(Function(y) y.ProcessorModule.ModuleName = ModuleName)
        '        If gModule IsNot Nothing Then
        '            gModule.ProcessorModule.RunGeneric(Enums.ModuleEventType.CommandLine, Nothing, Nothing, Nothing, Nothing)
        '        End If
        '    End If
        '    If clExport = True Then
        '        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {MoviePath, clExportTemplate, clExportResizePoster}))
        '        'dlgExportMovies.CLExport(MoviePath, clExportTemplate, clExportResizePoster)
        '    End If

        '    If Not clScrapeType = Enums.ScrapeType_Movie.None Then
        '        Me.cmnuTrayExit.Enabled = True
        '        Me.cmnuTray.Enabled = True
        '        If Functions.HasModifier AndAlso Not clScrapeType = Enums.ScrapeType_Movie.SingleScrape Then
        '            Try
        '                Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
        '                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(860, "Loading Media..."))
        '                LoadMedia(New Structures.Scans With {.Movies = True})
        '                While Not Me.LoadingDone
        '                    Application.DoEvents()
        '                    Threading.Thread.Sleep(50)
        '                End While
        '                Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
        '                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(861, "Command Line Scraping..."))
        '                MovieScrapeData(False, clScrapeType, Master.DefaultMovieOptions)
        '            Catch ex As Exception
        '                logger.Error(New StackFrame().GetMethod().Name, ex)
        '            End Try
        '        Else
        '            Try
        '                If Not String.IsNullOrEmpty(MoviePath) AndAlso hasSpec Then
        '                    Master.currMovie = Master.DB.LoadMovieFromDB(MoviePath)
        '                    Dim tmpTitle As String = String.Empty
        '                    If FileUtils.Common.isVideoTS(MoviePath) Then
        '                        tmpTitle = StringUtils.FilterName_Movie(Directory.GetParent(Directory.GetParent(MoviePath).FullName).Name, False)
        '                    ElseIf FileUtils.Common.isBDRip(MoviePath) Then
        '                        tmpTitle = StringUtils.FilterName_Movie(Directory.GetParent(Directory.GetParent(Directory.GetParent(MoviePath).FullName).FullName).Name, False)
        '                    Else
        '                        tmpTitle = StringUtils.FilterName_Movie(If(isSingle, Directory.GetParent(MoviePath).Name, Path.GetFileNameWithoutExtension(MoviePath)))
        '                    End If
        '                    If Master.currMovie.Movie Is Nothing Then
        '                        Master.currMovie.Movie = New MediaContainers.Movie
        '                        Master.currMovie.Movie.Title = tmpTitle
        '                        Dim sFile As New Scanner.MovieContainer
        '                        sFile.Filename = MoviePath
        '                        sFile.isSingle = isSingle
        '                        sFile.UseFolder = If(isSingle, True, False)
        '                        fScanner.GetMovieFolderContents(sFile)
        '                        If Not String.IsNullOrEmpty(sFile.Nfo) Then
        '                            Master.currMovie.Movie = NFO.LoadMovieFromNFO(sFile.Nfo, sFile.isSingle)
        '                        Else
        '                            Master.currMovie.Movie = NFO.LoadMovieFromNFO(sFile.Filename, sFile.isSingle)
        '                        End If
        '                        If String.IsNullOrEmpty(Master.currMovie.Movie.Title) Then
        '                            'no title so assume it's an invalid nfo, clear nfo path if exists
        '                            sFile.Nfo = String.Empty
        '                            If FileUtils.Common.isVideoTS(sFile.Filename) Then
        '                                Master.currMovie.ListTitle = StringUtils.FilterName_Movie(Directory.GetParent(Directory.GetParent(sFile.Filename).FullName).Name)
        '                            ElseIf FileUtils.Common.isBDRip(sFile.Filename) Then
        '                                Master.currMovie.ListTitle = StringUtils.FilterName_Movie(Directory.GetParent(Directory.GetParent(Directory.GetParent(sFile.Filename).FullName).FullName).Name)
        '                            Else
        '                                If sFile.UseFolder AndAlso sFile.isSingle Then
        '                                    Master.currMovie.ListTitle = StringUtils.FilterName_Movie(Directory.GetParent(sFile.Filename).Name)
        '                                Else
        '                                    Master.currMovie.ListTitle = StringUtils.FilterName_Movie(Path.GetFileNameWithoutExtension(sFile.Filename))
        '                                End If
        '                            End If
        '                        Else
        '                            Dim tTitle As String = StringUtils.SortTokens_Movie(Master.currMovie.Movie.Title)
        '                            If Master.eSettings.MovieDisplayYear AndAlso Not String.IsNullOrEmpty(Master.currMovie.Movie.Year) Then
        '                                Master.currMovie.ListTitle = String.Format("{0} ({1})", tTitle, Master.currMovie.Movie.Year)
        '                            Else
        '                                Master.currMovie.ListTitle = tTitle
        '                            End If
        '                        End If

        '                        If Not String.IsNullOrEmpty(Master.currMovie.ListTitle) Then
        '                            Master.currMovie.BannerPath = sFile.Banner
        '                            Master.currMovie.ClearArtPath = sFile.ClearArt
        '                            Master.currMovie.ClearLogoPath = sFile.ClearLogo
        '                            Master.currMovie.DiscArtPath = sFile.DiscArt
        '                            Master.currMovie.EFanartsPath = sFile.EFanarts
        '                            Master.currMovie.EThumbsPath = sFile.EThumbs
        '                            Master.currMovie.FanartPath = sFile.Fanart
        '                            Master.currMovie.Filename = sFile.Filename
        '                            Master.currMovie.LandscapePath = sFile.Landscape
        '                            Master.currMovie.NfoPath = sFile.Nfo
        '                            Master.currMovie.PosterPath = sFile.Poster
        '                            Master.currMovie.Source = sFile.Source
        '                            Master.currMovie.Subtitles = sFile.Subtitles
        '                            'Master.currMovie.SubPath = sFile.Subs
        '                            Master.currMovie.ThemePath = sFile.Theme
        '                            Master.currMovie.TrailerPath = sFile.Trailer
        '                            Master.currMovie.UseFolder = sFile.UseFolder
        '                            Master.currMovie.IsSingle = sFile.isSingle

        '                            'search local actor thumb for each actor in NFO
        '                            If Master.currMovie.Movie.Actors.Count > 0 AndAlso sFile.ActorThumbs.Count > 0 Then
        '                                For Each actor In Master.currMovie.Movie.Actors
        '                                    actor.ThumbPath = sFile.ActorThumbs.FirstOrDefault(Function(s) Path.GetFileNameWithoutExtension(s).ToLower = actor.Name.Replace(" ", "_").ToLower)
        '                                Next
        '                            End If
        '                        End If
        '                        Master.tmpMovie = Master.currMovie.Movie
        '                    End If
        '                    Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
        '                    Master.fLoading.SetLoadingMesg(Master.eLang.GetString(861, "Command Line Scraping..."))
        '                    MovieScrapeData(False, Enums.ScrapeType_Movie.SingleScrape, Master.DefaultMovieOptions)
        '                Else
        '                    Me.ScraperDone = True
        '                End If
        '            Catch ex As Exception
        '                Me.ScraperDone = True
        '                logger.Error(New StackFrame().GetMethod().Name, ex)
        '            End Try
        '        End If

        '        While Not Me.ScraperDone
        '            Application.DoEvents()
        '            Threading.Thread.Sleep(50)
        '        End While
        '    End If

        '    If UpdateTVShows Then
        '        Try
        '            Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
        '            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(860, "Loading Media..."))
        '            LoadMedia(New Structures.Scans With {.TV = True})
        '            While Not Me.LoadingDone
        '                Application.DoEvents()
        '                Threading.Thread.Sleep(50)
        '            End While
        '            Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
        '            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(861, "Command Line Scraping..."))
        '            MovieScrapeData(False, clScrapeType, Master.DefaultMovieOptions)
        '        Catch ex As Exception
        '            logger.Error(New StackFrame().GetMethod().Name, ex)
        '        End Try
        '    End If

        '    Master.fLoading.Close()
        '    Me.Close()
        'Catch ex As Exception
        'End Try

    End Sub
    ''' <summary>
    ''' Performs startup routines specific to being initiated as a GUI application (user interaction intended)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadWithGUI()
        Try
            logger.Trace("LoadWithGUI()")
            'If Master.eSettings.CheckUpdates Then
            '    If Functions.CheckNeedUpdate() Then
            '        Using dNewVer As New dlgNewVersion
            '            fLoading.Hide()
            '            If dNewVer.ShowDialog() = Windows.Forms.DialogResult.Abort Then
            '                tmrAppExit.Enabled = True
            '                CloseApp = True
            '            End If
            '        End Using
            '    End If
            'End If

            ' Not localized as is the Assembly file version
            'Dim VersionNumberO As String = System.String.Format("{0}.{1}.{2}.{3}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)

            If Not CloseApp Then

                'moved to frmMain_Load
                'Me.tpMovies.Tag = New Structures.MainTabType With {.ContentName = Master.eLang.GetString(36, "Movies"), .ContentType = Enums.Content_Type.Movie, .DefaultList = "movielist"}
                'Me.tpMovieSets.Tag = New Structures.MainTabType With {.ContentName = Master.eLang.GetString(366, "Sets"), .ContentType = Enums.Content_Type.MovieSet, .DefaultList = "setslist"}
                'Me.tpTVShows.Tag = New Structures.MainTabType With {.ContentName = Master.eLang.GetString(653, "TV Shows"), .ContentType = Enums.Content_Type.TV, .DefaultList = "tvshowlist"}
                'ModulesManager.Instance.RuntimeObjects.MediaTabSelected = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)

                Me.SetUp(True)

                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(863, "Positioning controls..."))
                Me.Location = Master.eSettings.GeneralWindowLoc
                Me.Size = Master.eSettings.GeneralWindowSize
                Me.WindowState = Master.eSettings.GeneralWindowState
                If Not Me.WindowState = FormWindowState.Minimized Then
                    Master.AppPos = Me.Bounds
                End If

                Me.MovieInfoPanelState = Master.eSettings.GeneralMovieInfoPanelState
                Select Case Me.MovieInfoPanelState
                    Case 0
                        Me.pnlInfoPanel.Height = 25
                        Me.btnDown.Enabled = False
                        Me.btnMid.Enabled = True
                        Me.btnUp.Enabled = True
                    Case 1
                        Me.pnlInfoPanel.Height = Me.IPMid
                        Me.btnMid.Enabled = False
                        Me.btnDown.Enabled = True
                        Me.btnUp.Enabled = True
                    Case 2
                        Me.pnlInfoPanel.Height = Me.IPUp
                        Me.btnUp.Enabled = False
                        Me.btnDown.Enabled = True
                        Me.btnMid.Enabled = True
                End Select

                Me.MovieSetInfoPanelState = Master.eSettings.GeneralMovieSetInfoPanelState

                Me.TVShowInfoPanelState = Master.eSettings.GeneralTVShowInfoPanelState

                Me.FilterRaise_Movies = Master.eSettings.GeneralFilterPanelStateMovie
                If Me.FilterRaise_Movies Then
                    'Me.pnlFilter_Movies.Height = Functions.Quantize(Me.gbFilterSpecific_Movies.Height + Me.lblFilter_Movies.Height + 15, 5)
                    Me.pnlFilter_Movies.AutoSize = True
                    Me.btnFilterDown_Movies.Enabled = True
                    Me.btnFilterUp_Movies.Enabled = False
                Else
                    'Me.pnlFilter_Movies.Height = 25
                    Me.pnlFilter_Movies.AutoSize = False
                    Me.pnlFilter_Movies.Height = Me.pnlFilterTop_Movies.Height
                    Me.btnFilterDown_Movies.Enabled = False
                    Me.btnFilterUp_Movies.Enabled = True
                End If

                Me.FilterRaise_MovieSets = Master.eSettings.GeneralFilterPanelStateMovieSet
                If Me.FilterRaise_MovieSets Then
                    'Me.pnlFilter_MovieSets.Height = Functions.Quantize(Me.gbFilterSpecific_MovieSets.Height + Me.lblFilter_MovieSets.Height + 15, 5)
                    Me.pnlFilter_MovieSets.AutoSize = True
                    Me.btnFilterDown_MovieSets.Enabled = True
                    Me.btnFilterUp_MovieSets.Enabled = False
                Else
                    'Me.pnlFilter_MovieSets.Height = 25
                    Me.pnlFilter_MovieSets.AutoSize = False
                    Me.pnlFilter_MovieSets.Height = Me.pnlFilterTop_MovieSets.Height
                    Me.btnFilterDown_MovieSets.Enabled = False
                    Me.btnFilterUp_MovieSets.Enabled = True
                End If

                Me.FilterRaise_Shows = Master.eSettings.GeneralFilterPanelStateShow
                If Me.FilterRaise_Shows Then
                    'Me.pnlFilter_Shows.Height = Functions.Quantize(Me.gbFilterSpecific_Shows.Height + Me.lblFilter_Shows.Height + 15, 5)
                    Me.pnlFilter_Shows.AutoSize = True
                    Me.btnFilterDown_Shows.Enabled = True
                    Me.btnFilterUp_Shows.Enabled = False
                Else
                    'Me.pnlFilter_Shows.Height = 25
                    Me.pnlFilter_Shows.AutoSize = False
                    Me.pnlFilter_Shows.Height = Me.pnlFilterTop_Shows.Height
                    Me.btnFilterDown_Shows.Enabled = False
                    Me.btnFilterUp_Shows.Enabled = True
                End If

                Try ' On error just ignore this a let it use default
                    Me.scMain.SplitterDistance = Master.eSettings.GeneralMainSplitterPanelState
                    Me.scTV.SplitterDistance = Master.eSettings.GeneralShowSplitterPanelState
                    Me.scTVSeasonsEpisodes.SplitterDistance = Master.eSettings.GeneralSeasonSplitterPanelState
                Catch ex As Exception
                End Try

                Me.pnlFilter_Movies.Visible = True
                Me.pnlFilter_MovieSets.Visible = False
                Me.pnlFilter_Shows.Visible = False

                'MenuItem Tags for better Enable/Disable handling
                Me.mnuMainToolsCleanDB.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfNoMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfNoMoviesets = True, .IfNoTVShows = True, .IfTabTVShows = True}
                Me.mnuMainToolsClearCache.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfNoMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfNoMoviesets = True, .IfNoTVShows = True, .IfTabTVShows = True}
                Me.mnuMainToolsOfflineHolder.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .IfNoMovies = True}
                Me.mnuMainToolsReloadMovies.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}
                Me.mnuMainToolsReloadMovieSets.Tag = New Structures.ModulesMenus With {.ForMovieSets = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}
                Me.mnuMainToolsReloadTVShows.Tag = New Structures.ModulesMenus With {.ForTVShows = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}
                Me.mnuMainToolsRewriteMovieContent.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}
                Me.mnuMainToolsSortFiles.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfNoMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}

                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(1165, "Initializing Main Form. Please wait..."))
                Me.ClearInfo()

                Application.DoEvents()
                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(858, "Loading database..."))

                RemoveHandler dgvMovies.RowsAdded, AddressOf dgvMovies_RowsAdded
                RemoveHandler dgvMovieSets.RowsAdded, AddressOf dgvMovieSets_RowsAdded
                RemoveHandler dgvTVShows.RowsAdded, AddressOf dgvTVShows_RowsAdded

                If Not String.IsNullOrEmpty(Master.eSettings.Version) Then 'If Master.eSettings.Version = String.Format("r{0}", My.Application.Info.Version.Revision) Then
                    If Master.DB.ConnectMyVideosDB() Then
                        Me.LoadMedia(New Structures.Scans With {.Movies = True, .MovieSets = True, .TV = True})
                    End If
                    Me.FillList(True, True, True)
                    Me.Visible = True
                Else
                    If Master.DB.ConnectMyVideosDB() Then
                        Me.LoadMedia(New Structures.Scans With {.Movies = True, .MovieSets = True, .TV = True})
                    End If
                    'If dlgWizard.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    '    Application.DoEvents()
                    '    Me.SetUp(False) 'just in case user changed languages
                    '    Me.Visible = True
                    '    Me.LoadMedia(New Structures.Scans With {.Movies = True, .MovieSets = True, .TV = True})
                    'Else
                    Me.FillList(True, True, True)
                    Me.Visible = True
                    'End If
                End If

                AddHandler dgvMovies.RowsAdded, AddressOf dgvMovies_RowsAdded
                AddHandler dgvMovieSets.RowsAdded, AddressOf dgvMovieSets_RowsAdded
                AddHandler dgvTVShows.RowsAdded, AddressOf dgvTVShows_RowsAdded

                Master.DB.LoadMovieSourcesFromDB()
                Master.DB.LoadTVSourcesFromDB()
                Master.DB.LoadExcludeDirsFromDB()

                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(864, "Setting menus..."))
                Me.SetMenus(True)
                Functions.GetListOfSources()
                Me.cmnuTrayExit.Enabled = True
                Me.cmnuTraySettings.Enabled = True
                Me.mnuMainEdit.Enabled = True
                If tsbMediaCenters.DropDownItems.Count > 0 Then tsbMediaCenters.Enabled = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub frmMain_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        If Not Me.WindowState = FormWindowState.Minimized Then
            Master.AppPos = Me.Bounds
        End If
    End Sub
    ''' <summary>
    ''' The form has been resized, so re-position those controls that need to be re-located
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Created Then
            If Not Me.WindowState = FormWindowState.Minimized Then
                Master.AppPos = Me.Bounds
            End If
            Me.MoveMPAA()
            Me.MoveGenres()
            ImageUtils.ResizePB(Me.pbFanart, Me.pbFanartCache, Me.scMain.Panel2.Height - 90, Me.scMain.Panel2.Width)
            Me.pbFanart.Left = Convert.ToInt32((Me.scMain.Panel2.Width - Me.pbFanart.Width) / 2)
            Me.pnlNoInfo.Location = New Point(Convert.ToInt32((Me.scMain.Panel2.Width - Me.pnlNoInfo.Width) / 2), Convert.ToInt32((Me.scMain.Panel2.Height - Me.pnlNoInfo.Height) / 2))
            Me.pnlCancel.Location = New Point(Convert.ToInt32((Me.scMain.Panel2.Width - Me.pnlNoInfo.Width) / 2), 124)
            Me.pnlFilterCountries_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterSpecific_Movies.Left + Me.tblFilterSpecific_Movies.Left + Me.tblFilterSpecificData_Movies.Left + Me.txtFilterCountry_Movies.Left + 1, _
                                                              (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterSpecific_Movies.Top + Me.tblFilterSpecific_Movies.Top + Me.tblFilterSpecificData_Movies.Top + Me.txtFilterCountry_Movies.Top) - Me.pnlFilterCountries_Movies.Height)
            Me.pnlFilterGenres_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterSpecific_Movies.Left + Me.tblFilterSpecific_Movies.Left + Me.tblFilterSpecificData_Movies.Left + Me.txtFilterGenre_Movies.Left + 1, _
                                                           (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterSpecific_Movies.Top + Me.tblFilterSpecific_Movies.Top + Me.tblFilterSpecificData_Movies.Top + Me.txtFilterGenre_Movies.Top) - Me.pnlFilterGenres_Movies.Height)
            Me.pnlFilterGenres_Shows.Location = New Point(Me.pnlFilter_Shows.Left + Me.tblFilter_Shows.Left + Me.gbFilterSpecific_Shows.Left + Me.tblFilterSpecific_Shows.Left + Me.tblFilterSpecificData_Shows.Left + Me.txtFilterGenre_Shows.Left + 1, _
                                                          (Me.pnlFilter_Shows.Top + Me.tblFilter_Shows.Top + Me.gbFilterSpecific_Shows.Top + Me.tblFilterSpecific_Shows.Top + Me.tblFilterSpecificData_Shows.Top + Me.txtFilterGenre_Shows.Top) - Me.pnlFilterGenres_Shows.Height)
            Me.pnlFilterDataFields_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterSpecific_Movies.Left + Me.tblFilterSpecific_Movies.Left + Me.tblFilterSpecificData_Movies.Left + Me.gbFilterDataField_Movies.Left + Me.tblFilterDataField_Movies.Left + Me.txtFilterDataField_Movies.Left + 1, _
                                                               (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterSpecific_Movies.Top + Me.tblFilterSpecific_Movies.Top + Me.tblFilterSpecificData_Movies.Top + Me.gbFilterDataField_Movies.Top + Me.tblFilterDataField_Movies.Top + Me.txtFilterDataField_Movies.Top) - Me.pnlFilterDataFields_Movies.Height)
            Me.pnlFilterMissingItems_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterGeneral_Movies.Left + Me.tblFilterGeneral_Movies.Left + Me.btnFilterMissing_Movies.Left + 1, _
                                                                 (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterGeneral_Movies.Top + Me.tblFilterGeneral_Movies.Top + Me.btnFilterMissing_Movies.Top) - Me.pnlFilterMissingItems_Movies.Height)
            Me.pnlFilterMissingItems_MovieSets.Location = New Point(Me.pnlFilter_MovieSets.Left + Me.tblFilter_MovieSets.Left + Me.gbFilterGeneral_MovieSets.Left + Me.tblFilterGeneral_MovieSets.Left + Me.btnFilterMissing_MovieSets.Left + 1, _
                                                                 (Me.pnlFilter_MovieSets.Top + Me.tblFilter_MovieSets.Top + Me.gbFilterGeneral_MovieSets.Top + Me.tblFilterGeneral_MovieSets.Top + Me.btnFilterMissing_MovieSets.Top) - Me.pnlFilterMissingItems_MovieSets.Height)
            Me.pnlFilterMissingItems_Shows.Location = New Point(Me.pnlFilter_Shows.Left + Me.tblFilter_Shows.Left + Me.gbFilterGeneral_Shows.Left + Me.tblFilterGeneral_Shows.Left + Me.btnFilterMissing_Shows.Left + 1, _
                                                                 (Me.pnlFilter_Shows.Top + Me.tblFilter_Shows.Top + Me.gbFilterGeneral_Shows.Top + Me.tblFilterGeneral_Shows.Top + Me.btnFilterMissing_Shows.Top) - Me.pnlFilterMissingItems_Shows.Height)
            Me.pnlFilterSources_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterSpecific_Movies.Left + Me.tblFilterSpecific_Movies.Left + Me.tblFilterSpecificData_Movies.Left + Me.txtFilterSource_Movies.Left + 1, _
                                                            (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterSpecific_Movies.Top + Me.tblFilterSpecific_Movies.Top + Me.tblFilterSpecificData_Movies.Top + Me.txtFilterSource_Movies.Top) - Me.pnlFilterSources_Movies.Height)
            Me.pnlFilterSources_Shows.Location = New Point(Me.pnlFilter_Shows.Left + Me.tblFilter_Shows.Left + Me.gbFilterSpecific_Shows.Left + Me.tblFilterSpecific_Shows.Left + Me.tblFilterSpecificData_Shows.Left + Me.txtFilterSource_Shows.Left + 1, _
                                                           (Me.pnlFilter_Shows.Top + Me.tblFilter_Shows.Top + Me.gbFilterSpecific_Shows.Top + Me.tblFilterSpecific_Shows.Top + Me.tblFilterSpecificData_Shows.Top + Me.txtFilterSource_Shows.Top) - Me.pnlFilterSources_Shows.Height)
            Me.pnlLoadSettings.Location = New Point(Convert.ToInt32((Me.Width - Me.pnlLoadSettings.Width) / 2), Convert.ToInt32((Me.Height - Me.pnlLoadSettings.Height) / 2))
        End If
    End Sub

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Not CloseApp Then
            Me.BringToFront()
            Me.Activate()
            Me.cmnuTray.Enabled = True
            If Not Functions.CheckIfWindows Then Mono_Shown()
        End If
    End Sub

    Private Sub TaskRunCallBack(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        TaskList.Add(New Task With {.mType = mType, .Params = _params})
        If TasksDone Then
            Me.tmrRunTasks.Start()
            Me.TasksDone = False
        End If
    End Sub
    ''' <summary>
    ''' This is a generic callback function.
    ''' </summary>
    ''' <param name="mType"></param>
    ''' <param name="_params"></param>
    ''' <remarks></remarks>
    Private Sub GenericRunCallBack(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        Select Case mType

            Case Enums.ModuleEventType.CommandLine
                Select Case _params(0).ToString
                    Case "addmoviesource"
                        Using dSource As New dlgMovieSource
                            If dSource.ShowDialog(CStr(_params(1)), CStr(_params(1))) = Windows.Forms.DialogResult.OK Then
                                Master.DB.LoadMovieSourcesFromDB()
                                Me.SetMenus(True)
                            End If
                        End Using
                    Case "addtvshowsource"
                        Using dSource As New dlgTVSource
                            If dSource.ShowDialog(CStr(_params(1)), CStr(_params(1))) = Windows.Forms.DialogResult.OK Then
                                Master.DB.LoadTVSourcesFromDB()
                                Me.SetMenus(True)
                            End If
                        End Using
                    Case "cleanvideodb"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(644, "Cleaning Database..."))
                        Me.CleanDB()
                        While Me.bwCleanDB.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                    Case "loadmedia"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(860, "Loading Media..."))
                        Me.LoadingDone = False
                        Me.LoadMedia(CType(_params(1), Structures.Scans), CStr(_params(2)), CStr(_params(3)))
                        While Not Me.LoadingDone
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                    Case "scrapemovies"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(861, "Command Line Scraping..."))
                        Dim ScrapeModifier As Structures.ScrapeModifier = CType(_params(2), Structures.ScrapeModifier)
                        CreateScrapeList_Movie(CType(_params(1), Enums.ScrapeType), Master.DefaultOptions_Movie, ScrapeModifier)
                        While bwMovieScraper.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                    Case "scrapetvshows"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(861, "Command Line Scraping..."))
                        Dim ScrapeModifier As Structures.ScrapeModifier = CType(_params(2), Structures.ScrapeModifier)
                        CreateScrapeList_TV(CType(_params(1), Enums.ScrapeType), Master.DefaultOptions_TV, ScrapeModifier)
                        While bwTVScraper.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                End Select

            Case Enums.ModuleEventType.Generic
                Select Case _params(0).ToString
                    Case "controlsenabled"
                        Me.SetControlsEnabled(Convert.ToBoolean(_params(1)), If(_params.Count = 3, Convert.ToBoolean(_params(2)), False))
                    Case "filllist"
                        Me.FillList(CBool(_params(1)), CBool(_params(2)), CBool(_params(3)))
                End Select
            Case Enums.ModuleEventType.Notification
                Select Case _params(0).ToString
                    Case "error"
                        dlgErrorViewer.Show(Me)
                    Case Else
                        Me.Activate()
                End Select

            Case Enums.ModuleEventType.AfterEdit_Movie
                Me.RefreshRow_Movie(Convert.ToInt64(_params(0)))

            Case Enums.ModuleEventType.AfterEdit_TVEpisode
                Me.RefreshRow_TVEpisode(Convert.ToInt64(_params(0)))

            Case Enums.ModuleEventType.AfterEdit_TVShow
                Me.RefreshRow_TVShow(Convert.ToInt64(_params(0)))

            Case Else
                logger.Warn("Callback for <{0}> with no handler.", mType)
        End Select
    End Sub

    Private Sub cmnuMovieGenresGenre_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuMovieGenresGenre.DropDown
        Me.cmnuMovieGenresGenre.Items.Remove(Master.eLang.GetString(98, "Select Genre..."))
    End Sub

    Private Sub cmnuMovieGenresGenre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuMovieGenresGenre.SelectedIndexChanged
        If dgvMovies.SelectedRows.Count > 1 Then
            cmnuMovieGenresRemove.Enabled = True
            cmnuMovieGenresAdd.Enabled = True
        Else
            cmnuMovieGenresRemove.Enabled = cmnuMovieGenresGenre.Tag.ToString.Contains(cmnuMovieGenresGenre.Text)
            cmnuMovieGenresAdd.Enabled = Not cmnuMovieGenresGenre.Tag.ToString.Contains(cmnuMovieGenresGenre.Text)
        End If
        cmnuMovieGenresSet.Enabled = True
    End Sub

    Private Sub cmnumovieLanguageLanguages_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuMovieLanguageLanguages.DropDown
        Me.cmnuMovieLanguageLanguages.Items.Remove(Master.eLang.GetString(1199, "Select Language..."))
    End Sub

    Private Sub cmnumovieLanguageLanguages_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuMovieLanguageLanguages.SelectedIndexChanged
        Me.cmnuMovieLanguageSet.Enabled = True
    End Sub

    Private Sub cmnuShowLanguageLanguages_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuShowLanguageLanguages.DropDown
        Me.cmnuShowLanguageLanguages.Items.Remove(Master.eLang.GetString(1199, "Select Language..."))
    End Sub

    Private Sub cmnuShowLanguageLanguages_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuShowLanguageLanguages.SelectedIndexChanged
        Me.cmnuShowLanguageSet.Enabled = True
    End Sub

    Private Sub cmnuMovieSetSortMethodMethods_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuMovieSetSortMethodMethods.SelectedIndexChanged
        Me.cmnuMovieSetSortMethodSet.Enabled = True
    End Sub

    Private Sub lblFilterGenreClose_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterGenresClose_Movies.Click
        Me.txtFilterGenre_Movies.Focus()
        Me.pnlFilterGenres_Movies.Tag = String.Empty
    End Sub

    Private Sub lblFilterGenresClose_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterGenresClose_Shows.Click
        Me.txtFilterGenre_Shows.Focus()
        Me.pnlFilterGenres_Shows.Tag = String.Empty
    End Sub

    Private Sub lblFilterCountryClose_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterCountriesClose_Movies.Click
        Me.txtFilterCountry_Movies.Focus()
        Me.pnlFilterCountries_Movies.Tag = String.Empty
    End Sub

    Private Sub lblFilterDataFieldsClose_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterDataFieldsClose_Movies.Click
        Me.txtFilterDataField_Movies.Focus()
        Me.pnlFilterDataFields_Movies.Tag = String.Empty
    End Sub

    Private Sub lblFilterMissingItemsClose_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterMissingItemsClose_Movies.Click
        Me.pnlFilterMissingItems_Movies.Visible = False
    End Sub

    Private Sub lblFilterMissingItemsClose_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterMissingItemsClose_MovieSets.Click
        Me.pnlFilterMissingItems_MovieSets.Visible = False
    End Sub

    Private Sub lblFilterMissingItemsClose_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterMissingItemsClose_Shows.Click
        Me.pnlFilterMissingItems_Shows.Visible = False
    End Sub

    Private Sub lblFilterSourceClose_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterSourcesClose_Movies.Click
        Me.txtFilterSource_Movies.Focus()
        Me.pnlFilterSources_Movies.Tag = String.Empty
    End Sub

    Private Sub lblFilterSourceClose_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFilterSourcesClose_Shows.Click
        Me.txtFilterSource_Shows.Focus()
        Me.pnlFilterSources_Shows.Tag = String.Empty
    End Sub

    Private Sub LoadInfo_Movie(ByVal ID As Integer, ByVal sPath As String, ByVal doInfo As Boolean, ByVal doMI As Boolean, Optional ByVal setEnabled As Boolean = False)
        Me.dgvMovies.SuspendLayout()
        Me.SetControlsEnabled(False)
        Me.ShowNoInfo(False)

        If doMI Then
            If Me.bwMetaData.IsBusy Then Me.bwMetaData.CancelAsync()

            Me.txtMetaData.Clear()
            Me.pbMILoading.Visible = True

            Me.bwMetaData = New System.ComponentModel.BackgroundWorker
            Me.bwMetaData.WorkerSupportsCancellation = True
            Me.bwMetaData.RunWorkerAsync(New Arguments With {.setEnabled = setEnabled, .Path = sPath, .DBElement = Me.currMovie})
        End If

        If doInfo Then
            Me.ClearInfo()

            If Me.bwLoadMovieInfo.IsBusy AndAlso Not Me.bwLoadMovieInfo.CancellationPending Then
                Me.bwLoadMovieInfo.CancelAsync()
            End If

            While Me.bwLoadMovieInfo.IsBusy
                Application.DoEvents()
            End While

            Me.bwLoadMovieInfo = New System.ComponentModel.BackgroundWorker
            Me.bwLoadMovieInfo.WorkerSupportsCancellation = True
            Me.bwLoadMovieInfo.RunWorkerAsync(New Arguments With {.ID = ID})
        End If
    End Sub

    Private Sub LoadInfo_MovieSet(ByVal ID As Integer, ByVal doInfo As Boolean)
        Me.dgvMovieSets.SuspendLayout()
        Me.SetControlsEnabled(False)
        Me.ShowNoInfo(False)

        If doInfo Then
            Me.ClearInfo()

            If Me.bwLoadMovieSetInfo.IsBusy AndAlso Not Me.bwLoadMovieSetInfo.CancellationPending Then
                Me.bwLoadMovieSetInfo.CancelAsync()
            End If

            While Me.bwLoadMovieSetInfo.IsBusy
                Application.DoEvents()
            End While

            Me.bwLoadMovieSetInfo = New System.ComponentModel.BackgroundWorker
            Me.bwLoadMovieSetInfo.WorkerSupportsCancellation = True
            Me.bwLoadMovieSetInfo.RunWorkerAsync(New Arguments With {.ID = ID})
        End If
    End Sub

    Private Sub LoadInfo_TVEpisode(ByVal ID As Integer)
        Me.dgvTVEpisodes.SuspendLayout()
        Me.SetControlsEnabled(False)
        Me.ShowNoInfo(False)

        If Not Me.currThemeType = Theming.ThemeType.Episode Then Me.ApplyTheme(Theming.ThemeType.Episode)

        Me.ClearInfo()

        Me.bwLoadEpInfo = New System.ComponentModel.BackgroundWorker
        Me.bwLoadEpInfo.WorkerSupportsCancellation = True
        Me.bwLoadEpInfo.RunWorkerAsync(New Arguments With {.ID = ID})
    End Sub

    Private Sub LoadInfo_TVSeason(ByVal SeasonID As Integer, Optional ByVal isMissing As Boolean = False)
        Me.dgvTVSeasons.SuspendLayout()
        Me.SetControlsEnabled(False)
        Me.ShowNoInfo(False)

        If Not Me.currThemeType = Theming.ThemeType.Show Then
            Me.ApplyTheme(Theming.ThemeType.Show)
        End If

        Me.ClearInfo()

        Me.bwLoadSeasonInfo = New System.ComponentModel.BackgroundWorker
        Me.bwLoadSeasonInfo.WorkerSupportsCancellation = True
        Me.bwLoadSeasonInfo.RunWorkerAsync(New Arguments With {.ID = SeasonID, .setEnabled = Not isMissing})
    End Sub

    Private Sub LoadInfo_TVShow(ByVal ID As Integer)
        Me.dgvTVShows.SuspendLayout()
        Me.SetControlsEnabled(False)
        Me.ShowNoInfo(False)

        If Not Me.currThemeType = Theming.ThemeType.Show Then Me.ApplyTheme(Theming.ThemeType.Show)

        Me.ClearInfo()

        Me.bwLoadShowInfo = New System.ComponentModel.BackgroundWorker
        Me.bwLoadShowInfo.WorkerSupportsCancellation = True
        Me.bwLoadShowInfo.RunWorkerAsync(New Arguments With {.ID = ID})

        Me.FillSeasons(ID)
    End Sub

    Private Sub lstActors_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstActors.SelectedValueChanged
        If Me.lstActors.Items.Count > 0 AndAlso Me.lstActors.SelectedItems.Count > 0 AndAlso Me.alActors.Item(Me.lstActors.SelectedIndex) IsNot Nothing AndAlso Not Me.alActors.Item(Me.lstActors.SelectedIndex).ToString = "none" Then

            If Me.pbActors.Image IsNot Nothing Then
                Me.pbActors.Image.Dispose()
                Me.pbActors.Image = Nothing
            End If

            If Not Me.alActors.Item(Me.lstActors.SelectedIndex).ToString.Trim.StartsWith("http") Then
                Me.MainActors.FromFile(Me.alActors.Item(Me.lstActors.SelectedIndex).ToString)

                If Me.MainActors.Image IsNot Nothing Then
                    Me.pbActors.Image = Me.MainActors.Image
                Else
                    Me.pbActors.Image = My.Resources.actor_silhouette
                End If
            Else
                Me.pbActLoad.Visible = True

                If Me.bwDownloadPic.IsBusy Then
                    Me.bwDownloadPic.CancelAsync()
                    While Me.bwDownloadPic.IsBusy
                        Application.DoEvents()
                        Threading.Thread.Sleep(50)
                    End While
                End If

                Me.bwDownloadPic = New System.ComponentModel.BackgroundWorker
                Me.bwDownloadPic.WorkerSupportsCancellation = True
                Me.bwDownloadPic.RunWorkerAsync(New Arguments With {.pURL = Me.alActors.Item(Me.lstActors.SelectedIndex).ToString})
            End If

        Else
            Me.pbActors.Image = My.Resources.actor_silhouette
        End If
    End Sub

    Private Sub mnuScrapeModifier_Opened(sender As Object, e As EventArgs) Handles mnuScrapeModifier.Opened
        _SelectedScrapeTypeMode = mnuScrapeModifier.OwnerItem.Tag.ToString

        With Master.eSettings
            Select Case _SelectedContentType
                Case "movie"
                    mnuScrapeModifierActorthumbs.Enabled = .MovieActorThumbsAnyEnabled
                    mnuScrapeModifierActorthumbs.Visible = True
                    mnuScrapeModifierBanner.Enabled = .MovieBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
                    mnuScrapeModifierBanner.Visible = True
                    mnuScrapeModifierCharacterArt.Enabled = False
                    mnuScrapeModifierCharacterArt.Visible = False
                    mnuScrapeModifierClearArt.Enabled = .MovieClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt)
                    mnuScrapeModifierClearArt.Visible = True
                    mnuScrapeModifierClearLogo.Enabled = .MovieClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo)
                    mnuScrapeModifierClearLogo.Visible = True
                    mnuScrapeModifierDiscArt.Enabled = .MovieDiscArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt)
                    mnuScrapeModifierDiscArt.Visible = True
                    mnuScrapeModifierExtrafanarts.Enabled = .MovieExtrafanartsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainExtrafanarts)
                    mnuScrapeModifierExtrafanarts.Visible = True
                    mnuScrapeModifierExtrathumbs.Enabled = .MovieExtrathumbsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainExtrathumbs)
                    mnuScrapeModifierExtrathumbs.Visible = True
                    mnuScrapeModifierFanart.Enabled = .MovieFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierLandscape.Enabled = .MovieLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape)
                    mnuScrapeModifierLandscape.Visible = True
                    mnuScrapeModifierMetaData.Enabled = .MovieScraperMetaDataScan
                    mnuScrapeModifierMetaData.Visible = True
                    mnuScrapeModifierNFO.Enabled = True
                    mnuScrapeModifierNFO.Visible = True
                    mnuScrapeModifierPoster.Enabled = .MoviePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
                    mnuScrapeModifierPoster.Visible = True
                    mnuScrapeModifierTheme.Enabled = .MovieThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie(Enums.ModifierType.MainTheme)
                    mnuScrapeModifierTheme.Visible = True
                    mnuScrapeModifierTrailer.Enabled = .MovieTrailerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Trailer_Movie(Enums.ModifierType.MainTrailer)
                    mnuScrapeModifierTrailer.Visible = True
                Case "movieset"
                    mnuScrapeModifierActorthumbs.Enabled = False
                    mnuScrapeModifierActorthumbs.Visible = False
                    mnuScrapeModifierBanner.Enabled = .MovieSetBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainBanner)
                    mnuScrapeModifierBanner.Visible = True
                    mnuScrapeModifierCharacterArt.Enabled = False
                    mnuScrapeModifierCharacterArt.Visible = False
                    mnuScrapeModifierClearArt.Enabled = .MovieSetClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearArt)
                    mnuScrapeModifierClearArt.Visible = True
                    mnuScrapeModifierClearLogo.Enabled = .MovieSetClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearLogo)
                    mnuScrapeModifierClearLogo.Visible = True
                    mnuScrapeModifierDiscArt.Enabled = .MovieSetDiscArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainDiscArt)
                    mnuScrapeModifierDiscArt.Visible = True
                    mnuScrapeModifierExtrafanarts.Enabled = False
                    mnuScrapeModifierExtrafanarts.Visible = False
                    mnuScrapeModifierExtrathumbs.Enabled = False
                    mnuScrapeModifierExtrathumbs.Visible = False
                    mnuScrapeModifierFanart.Enabled = .MovieSetFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierLandscape.Enabled = .MovieSetLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainLandscape)
                    mnuScrapeModifierLandscape.Visible = True
                    mnuScrapeModifierMetaData.Enabled = False
                    mnuScrapeModifierMetaData.Visible = False
                    mnuScrapeModifierNFO.Enabled = True
                    mnuScrapeModifierNFO.Visible = True
                    mnuScrapeModifierPoster.Enabled = .MovieSetPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainPoster)
                    mnuScrapeModifierPoster.Visible = True
                    mnuScrapeModifierTheme.Enabled = False
                    mnuScrapeModifierTheme.Visible = False
                    mnuScrapeModifierTrailer.Enabled = False
                    mnuScrapeModifierTrailer.Visible = False
                Case "tvepisode"
                    mnuScrapeModifierActorthumbs.Enabled = .TVEpisodeActorThumbsAnyEnabled
                    mnuScrapeModifierActorthumbs.Visible = True
                    mnuScrapeModifierBanner.Enabled = False
                    mnuScrapeModifierBanner.Visible = False
                    mnuScrapeModifierCharacterArt.Enabled = False
                    mnuScrapeModifierCharacterArt.Visible = False
                    mnuScrapeModifierClearArt.Enabled = False
                    mnuScrapeModifierClearArt.Visible = False
                    mnuScrapeModifierClearLogo.Enabled = False
                    mnuScrapeModifierClearLogo.Visible = False
                    mnuScrapeModifierDiscArt.Enabled = False
                    mnuScrapeModifierDiscArt.Visible = False
                    mnuScrapeModifierExtrafanarts.Enabled = False
                    mnuScrapeModifierExtrafanarts.Visible = False
                    mnuScrapeModifierExtrathumbs.Enabled = False
                    mnuScrapeModifierExtrathumbs.Visible = False
                    mnuScrapeModifierFanart.Enabled = .TVEpisodeFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierLandscape.Enabled = False
                    mnuScrapeModifierLandscape.Visible = False
                    mnuScrapeModifierMetaData.Enabled = .TVScraperMetaDataScan
                    mnuScrapeModifierMetaData.Visible = True
                    mnuScrapeModifierNFO.Enabled = True
                    mnuScrapeModifierNFO.Visible = True
                    mnuScrapeModifierPoster.Enabled = .TVEpisodePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster)
                    mnuScrapeModifierPoster.Visible = True
                    mnuScrapeModifierTheme.Enabled = False
                    mnuScrapeModifierTheme.Visible = False
                    mnuScrapeModifierTrailer.Enabled = False
                    mnuScrapeModifierTrailer.Visible = False
                Case "tvseason"
                    mnuScrapeModifierActorthumbs.Enabled = False
                    mnuScrapeModifierActorthumbs.Visible = False
                    mnuScrapeModifierBanner.Enabled = .TVSeasonBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)
                    mnuScrapeModifierBanner.Visible = True
                    mnuScrapeModifierCharacterArt.Enabled = False
                    mnuScrapeModifierCharacterArt.Visible = False
                    mnuScrapeModifierClearArt.Enabled = False
                    mnuScrapeModifierClearArt.Visible = False
                    mnuScrapeModifierClearLogo.Enabled = False
                    mnuScrapeModifierClearLogo.Visible = False
                    mnuScrapeModifierDiscArt.Enabled = False
                    mnuScrapeModifierDiscArt.Visible = False
                    mnuScrapeModifierExtrafanarts.Enabled = False
                    mnuScrapeModifierExtrafanarts.Visible = False
                    mnuScrapeModifierExtrathumbs.Enabled = False
                    mnuScrapeModifierExtrathumbs.Visible = False
                    mnuScrapeModifierFanart.Enabled = .TVSeasonFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierLandscape.Enabled = .TVSeasonLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)
                    mnuScrapeModifierLandscape.Visible = True
                    mnuScrapeModifierMetaData.Enabled = False
                    mnuScrapeModifierMetaData.Visible = False
                    mnuScrapeModifierNFO.Enabled = False
                    mnuScrapeModifierNFO.Visible = False
                    mnuScrapeModifierPoster.Enabled = .TVSeasonPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)
                    mnuScrapeModifierPoster.Visible = True
                    mnuScrapeModifierTheme.Enabled = False
                    mnuScrapeModifierTheme.Visible = False
                    mnuScrapeModifierTrailer.Enabled = False
                    mnuScrapeModifierTrailer.Visible = False
                Case "tvshow"
                    mnuScrapeModifierActorthumbs.Enabled = .TVShowActorThumbsAnyEnabled
                    mnuScrapeModifierActorthumbs.Visible = True
                    mnuScrapeModifierBanner.Enabled = .TVShowBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)
                    mnuScrapeModifierBanner.Visible = True
                    mnuScrapeModifierCharacterArt.Enabled = .TVShowCharacterArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
                    mnuScrapeModifierCharacterArt.Visible = True
                    mnuScrapeModifierClearArt.Enabled = .TVShowClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
                    mnuScrapeModifierClearArt.Visible = True
                    mnuScrapeModifierClearLogo.Enabled = .TVShowClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
                    mnuScrapeModifierClearLogo.Visible = True
                    mnuScrapeModifierDiscArt.Enabled = False
                    mnuScrapeModifierDiscArt.Visible = False
                    mnuScrapeModifierExtrafanarts.Enabled = .TVShowExtrafanartsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainExtrafanarts)
                    mnuScrapeModifierExtrafanarts.Visible = True
                    mnuScrapeModifierExtrathumbs.Enabled = False
                    mnuScrapeModifierExtrathumbs.Visible = False
                    mnuScrapeModifierFanart.Enabled = .TVShowFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierLandscape.Enabled = .TVShowLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
                    mnuScrapeModifierLandscape.Visible = True
                    mnuScrapeModifierMetaData.Enabled = False
                    mnuScrapeModifierMetaData.Visible = False
                    mnuScrapeModifierNFO.Enabled = True
                    mnuScrapeModifierNFO.Visible = True
                    mnuScrapeModifierPoster.Enabled = .TVShowPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
                    mnuScrapeModifierPoster.Visible = True
                    mnuScrapeModifierTheme.Enabled = .TvShowThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_TV(Enums.ModifierType.MainTheme)
                    mnuScrapeModifierTheme.Visible = True
                    mnuScrapeModifierTrailer.Enabled = False
                    mnuScrapeModifierTrailer.Visible = False
            End Select
        End With
    End Sub

    Private Sub mnuScrapeType_Opened(sender As Object, e As EventArgs) Handles mnuScrapeType.Opened
        If mnuScrapeType.OwnerItem.OwnerItem IsNot Nothing Then
            _SelectedScrapeType = mnuScrapeType.OwnerItem.Tag.ToString
            _SelectedContentType = mnuScrapeType.OwnerItem.OwnerItem.Tag.ToString
        Else
            _SelectedScrapeType = "selected"
            _SelectedContentType = mnuScrapeType.OwnerItem.Tag.ToString
        End If
    End Sub

    Private Sub Autoscraper(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        mnuScrapeModifierActorthumbs.Click, _
        mnuScrapeModifierAll.Click, _
        mnuScrapeModifierBanner.Click, _
        mnuScrapeModifierCharacterArt.Click, _
        mnuScrapeModifierClearArt.Click, _
        mnuScrapeModifierClearLogo.Click, _
        mnuScrapeModifierDiscArt.Click, _
        mnuScrapeModifierExtrafanarts.Click, _
        mnuScrapeModifierExtrathumbs.Click, _
        mnuScrapeModifierFanart.Click, _
        mnuScrapeModifierLandscape.Click, _
        mnuScrapeModifierMetaData.Click, _
        mnuScrapeModifierNFO.Click, _
        mnuScrapeModifierPoster.Click, _
        mnuScrapeModifierTheme.Click, _
        mnuScrapeModifierTrailer.Click

        Dim ContentType As String = String.Empty
        Dim ModifierType As String = String.Empty
        Dim ScrapeType As String = String.Empty
        Dim Type As Enums.ScrapeType
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Dim Menu As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        ModifierType = Menu.Tag.ToString
        ScrapeType = String.Concat(_SelectedScrapeType, "_", _SelectedScrapeTypeMode)
        ContentType = _SelectedContentType

        Select Case ModifierType
            Case "all"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)
            Case "actorthumbs"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainActorThumbs, True)
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.EpisodeActorThumbs, True)
            Case "banner"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainBanner, True)
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.AllSeasonsBanner, True)
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonBanner, True)
            Case "characterart"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainCharacterArt, True)
            Case "clearart"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearArt, True)
            Case "clearlogo"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearLogo, True)
            Case "discart"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainDiscArt, True)
            Case "extrafanarts"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainExtrafanarts, True)
            Case "extrathumbs"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainExtrathumbs, True)
            Case "fanart"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainFanart, True)
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.AllSeasonsFanart, True)
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.EpisodeFanart, True)
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonFanart, True)
            Case "landscape"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainLandscape, True)
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.AllSeasonsLandscape, True)
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonLandscape, True)
            Case "metadata"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainMeta, True)
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.EpisodeMeta, True)
            Case "nfo"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.EpisodeNFO, True)
            Case "poster"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainPoster, True)
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.AllSeasonsPoster, True)
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.EpisodePoster, True)
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonPoster, True)
            Case "subtitle"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainSubtitle, True)
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.EpisodeSubtitle, True)
            Case "theme"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainTheme, True)
            Case "trailer"
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainTrailer, True)
        End Select

        Select Case ScrapeType
            Case "all_ask"
                Type = Enums.ScrapeType.AllAsk
            Case "all_auto"
                Type = Enums.ScrapeType.AllAuto
            Case "all_skip"
                Type = Enums.ScrapeType.AllSkip
            Case "filter_ask"
                Type = Enums.ScrapeType.FilterAsk
            Case "filter_auto"
                Type = Enums.ScrapeType.FilterAuto
            Case "filter_skip"
                Type = Enums.ScrapeType.FilterSkip
            Case "marked_ask"
                Type = Enums.ScrapeType.MarkedAsk
            Case "marked_auto"
                Type = Enums.ScrapeType.MarkedAuto
            Case "marked_skip"
                Type = Enums.ScrapeType.MarkedSkip
            Case "missing_ask"
                Type = Enums.ScrapeType.MissingAsk
            Case "missing_auto"
                Type = Enums.ScrapeType.MissingAuto
            Case "missing_skip"
                Type = Enums.ScrapeType.MissingSkip
            Case "new_ask"
                Type = Enums.ScrapeType.NewAsk
            Case "new_auto"
                Type = Enums.ScrapeType.NewAuto
            Case "new_skip"
                Type = Enums.ScrapeType.NewSkip
            Case "selected_ask"
                Type = Enums.ScrapeType.SelectedAsk
            Case "selected_auto"
                Type = Enums.ScrapeType.SelectedAuto
            Case "selected_skip"
                Type = Enums.ScrapeType.SelectedSkip
        End Select

        Select Case ContentType
            Case "movie"
                Me.CreateScrapeList_Movie(Type, Master.DefaultOptions_Movie, ScrapeModifier)
            Case "movieset"
                Me.CreateScrapeList_MovieSet(Type, Master.DefaultOptions_MovieSet, ScrapeModifier)
            Case "tvepisode"
                Me.CreateScrapeList_TVEpisode(Type, Master.DefaultOptions_TV, ScrapeModifier)
            Case "tvseason"
                Me.CreateScrapeList_TVSeason(Type, Master.DefaultOptions_TV, ScrapeModifier)
            Case "tvshow"
                Me.CreateScrapeList_TV(Type, Master.DefaultOptions_TV, ScrapeModifier)
        End Select
    End Sub

    Private Sub Mono_Shown()
        Me.pnlNoInfo.Location = New Point(Convert.ToInt32((Me.scMain.Panel2.Width - Me.pnlNoInfo.Width) / 2), Convert.ToInt32((Me.scMain.Panel2.Height - Me.pnlNoInfo.Height) / 2))
    End Sub
    ''' <summary>
    ''' Slide the genre images along with the panel and move with form resizing
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MoveGenres()
        Try
            For i As Integer = 0 To Me.pnlGenre.Count - 1
                Me.pnlGenre(i).Left = ((Me.pnlInfoPanel.Right) - (i * 73)) - 73
                Me.pnlGenre(i).Top = Me.pnlInfoPanel.Top - 105
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Slide the MPAA image along with the panel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MoveMPAA()
        Me.pnlMPAA.BringToFront()
        Me.pnlMPAA.Size = New Size(Me.pbMPAA.Width + 10, Me.pbMPAA.Height + 10)
        Me.pbMPAA.Location = New Point(4, 4)
        Me.pnlMPAA.Top = Me.pnlInfoPanel.Top - (Me.pnlMPAA.Height + 10)
    End Sub

    Private Sub InfoDownloaded_Movie(ByRef DBMovie As Database.DBElement)
        If Not String.IsNullOrEmpty(DBMovie.Movie.Title) Then
            Me.tslLoading.Text = Master.eLang.GetString(576, "Verifying Movie Details:")
            Application.DoEvents()

            Edit_Movie(DBMovie)
        End If

        Me.pnlCancel.Visible = False
        Me.tslLoading.Visible = False
        Me.tspbLoading.Visible = False
        Me.SetStatus(String.Empty)
        Me.SetControlsEnabled(True)
        Me.EnableFilters_Movies(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_Movie(ByVal iPercent As Integer)
        If Me.ReportDownloadPercent = True Then
            Me.tspbLoading.Value = iPercent
            Me.Refresh()
        End If
    End Sub

    Private Sub CreateScrapeList_Movie(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions_Movie, ByVal ScrapeModifier As Structures.ScrapeModifier)
        Dim DataRowList As New List(Of DataRow)
        Dim ScrapeList As New List(Of ScrapeItem)

        Select Case sType
            Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip, _
                Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
                'create snapshoot list of selected movies
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
                Next
            Case Else
                For Each sRow As DataRow In Me.dtMovies.Rows
                    DataRowList.Add(sRow)
                Next
        End Select

        Dim ActorThumbsAllowed As Boolean = Master.eSettings.MovieActorThumbsAnyEnabled
        Dim BannerAllowed As Boolean = Master.eSettings.MovieBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
        Dim ClearArtAllowed As Boolean = Master.eSettings.MovieClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt)
        Dim ClearLogoAllowed As Boolean = Master.eSettings.MovieClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo)
        Dim DiscArtAllowed As Boolean = Master.eSettings.MovieDiscArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt)
        Dim EFanartsAllowed As Boolean = Master.eSettings.MovieExtrafanartsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
        Dim EThumbsAllowed As Boolean = Master.eSettings.MovieExtrathumbsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
        Dim FanartAllowed As Boolean = Master.eSettings.MovieFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
        Dim LandscapeAllowed As Boolean = Master.eSettings.MovieLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape)
        Dim PosterAllowed As Boolean = Master.eSettings.MoviePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
        Dim ThemeAllowed As Boolean = Master.eSettings.MovieThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie(Enums.ModifierType.MainTheme)
        Dim TrailerAllowed As Boolean = Master.eSettings.MovieTrailerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Trailer_Movie(Enums.ModifierType.MainTrailer)

        'create ScrapeList of movies acording to scrapetype
        For Each drvRow As DataRow In DataRowList
            If Convert.ToBoolean(drvRow.Item("Lock")) Then Continue For

            Dim sModifier As New Structures.ScrapeModifier
            sModifier.DoSearch = ScrapeModifier.DoSearch
            sModifier.MainActorthumbs = ScrapeModifier.MainActorthumbs AndAlso ActorThumbsAllowed
            sModifier.MainBanner = ScrapeModifier.MainBanner AndAlso BannerAllowed
            sModifier.MainClearArt = ScrapeModifier.MainClearArt AndAlso ClearArtAllowed
            sModifier.MainClearLogo = ScrapeModifier.MainClearLogo AndAlso ClearLogoAllowed
            sModifier.MainDiscArt = ScrapeModifier.MainDiscArt AndAlso DiscArtAllowed
            sModifier.MainExtrafanarts = ScrapeModifier.MainExtrafanarts AndAlso EFanartsAllowed
            sModifier.MainExtrathumbs = ScrapeModifier.MainExtrathumbs AndAlso EThumbsAllowed
            sModifier.MainFanart = ScrapeModifier.MainFanart AndAlso FanartAllowed
            sModifier.MainLandscape = ScrapeModifier.MainLandscape AndAlso LandscapeAllowed
            sModifier.MainMeta = ScrapeModifier.MainMeta
            sModifier.MainNFO = ScrapeModifier.MainNFO
            sModifier.MainPoster = ScrapeModifier.MainPoster AndAlso PosterAllowed
            'sModifier.MainSubtitles = ScrapeModifier.MainSubtitles AndAlso SubtitlesAllowed
            sModifier.MainTheme = ScrapeModifier.MainTheme AndAlso ThemeAllowed
            sModifier.MainTrailer = ScrapeModifier.MainTrailer AndAlso TrailerAllowed

            Select Case sType
                Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
                    If Not Convert.ToBoolean(drvRow.Item("New")) Then Continue For
                Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
                    If Not Convert.ToBoolean(drvRow.Item("Mark")) Then Continue For
                Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
                    Dim index As Integer = Me.bsMovies.Find("idMovie", drvRow.Item(0))
                    If Not index >= 0 Then Continue For
                Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
                    If Not String.IsNullOrEmpty(drvRow.Item("BannerPath").ToString) Then sModifier.MainBanner = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ClearArtPath").ToString) Then sModifier.MainClearArt = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ClearLogoPath").ToString) Then sModifier.MainClearLogo = False
                    If Not String.IsNullOrEmpty(drvRow.Item("DiscArtPath").ToString) Then sModifier.MainDiscArt = False
                    If Not String.IsNullOrEmpty(drvRow.Item("EFanartsPath").ToString) Then sModifier.MainExtrafanarts = False
                    If Not String.IsNullOrEmpty(drvRow.Item("EThumbsPath").ToString) Then sModifier.MainExtrathumbs = False
                    If Not String.IsNullOrEmpty(drvRow.Item("FanartPath").ToString) Then sModifier.MainFanart = False
                    If Not String.IsNullOrEmpty(drvRow.Item("LandscapePath").ToString) Then sModifier.MainLandscape = False
                    If Not String.IsNullOrEmpty(drvRow.Item("NfoPath").ToString) Then sModifier.MainNFO = False
                    If Not String.IsNullOrEmpty(drvRow.Item("PosterPath").ToString) Then sModifier.MainPoster = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ThemePath").ToString) Then sModifier.MainTheme = False
                    If Not String.IsNullOrEmpty(drvRow.Item("TrailerPath").ToString) Then sModifier.MainTrailer = False
            End Select
            ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifier = sModifier})
        Next

        Me.SetControlsEnabled(False)

        Me.tspbLoading.Value = 0
        If ScrapeList.Count > 1 Then
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.tspbLoading.Maximum = ScrapeList.Count
        Else
            Me.tspbLoading.Maximum = 100
            Me.tspbLoading.Style = ProgressBarStyle.Marquee
        End If

        Select Case sType
            Case Enums.ScrapeType.AllAsk
                Me.tslLoading.Text = Master.eLang.GetString(127, "Scraping Media (All Movies - Ask):")
            Case Enums.ScrapeType.AllAuto
                Me.tslLoading.Text = Master.eLang.GetString(128, "Scraping Media (All Movies - Auto):")
            Case Enums.ScrapeType.AllSkip
                Me.tslLoading.Text = Master.eLang.GetString(853, "Scraping Media (All Movies - Skip):")
            Case Enums.ScrapeType.MissingAuto
                Me.tslLoading.Text = Master.eLang.GetString(132, "Scraping Media (Movies Missing Items - Auto):")
            Case Enums.ScrapeType.MissingAsk
                Me.tslLoading.Text = Master.eLang.GetString(133, "Scraping Media (Movies Missing Items - Ask):")
            Case Enums.ScrapeType.MissingSkip
                Me.tslLoading.Text = Master.eLang.GetString(1042, "Scraping Media (Movies Missing Items - Skip):")
            Case Enums.ScrapeType.NewAsk
                Me.tslLoading.Text = Master.eLang.GetString(134, "Scraping Media (New Movies - Ask):")
            Case Enums.ScrapeType.NewAuto
                Me.tslLoading.Text = Master.eLang.GetString(135, "Scraping Media (New Movies - Auto):")
            Case Enums.ScrapeType.NewSkip
                Me.tslLoading.Text = Master.eLang.GetString(1043, "Scraping Media (New Movies - Skip):")
            Case Enums.ScrapeType.MarkedAsk
                Me.tslLoading.Text = Master.eLang.GetString(136, "Scraping Media (Marked Movies - Ask):")
            Case Enums.ScrapeType.MarkedAuto
                Me.tslLoading.Text = Master.eLang.GetString(137, "Scraping Media (Marked Movies - Auto):")
            Case Enums.ScrapeType.MarkedSkip
                Me.tslLoading.Text = Master.eLang.GetString(1044, "Scraping Media (Marked Movies - Skip):")
            Case Enums.ScrapeType.FilterAsk
                Me.tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
            Case Enums.ScrapeType.FilterAuto
                Me.tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
            Case Enums.ScrapeType.FilterAuto
                Me.tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
            Case Enums.ScrapeType.SelectedAsk
                Me.tslLoading.Text = Master.eLang.GetString(1128, "Scraping Media (Selected Movies - Ask):")
            Case Enums.ScrapeType.SelectedAuto
                Me.tslLoading.Text = Master.eLang.GetString(1129, "Scraping Media (Selected Movies - Auto):")
            Case Enums.ScrapeType.SelectedSkip
                Me.tslLoading.Text = Master.eLang.GetString(1130, "Scraping Media (Selected Movies - Skip):")
            Case Enums.ScrapeType.SingleField
                Me.tslLoading.Text = Master.eLang.GetString(1127, "Scraping Media (Selected Movies - Single Field):")
            Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
                Me.tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
        End Select

        If Not sType = Enums.ScrapeType.SingleScrape Then
            Me.btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
            Me.lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
            Me.btnCancel.Visible = True
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = True
        End If

        Me.tslLoading.Visible = True
        Me.tspbLoading.Visible = True
        Application.DoEvents()
        bwMovieScraper.WorkerSupportsCancellation = True
        bwMovieScraper.WorkerReportsProgress = True
        bwMovieScraper.RunWorkerAsync(New Arguments With {.Options_Movie = ScrapeOptions, .ScrapeList = ScrapeList, .ScrapeType = sType})
    End Sub

    Private Sub InfoDownloaded_MovieSet(ByRef DBMovieSet As Database.DBElement)
        If Not String.IsNullOrEmpty(DBMovieSet.ListTitle) Then
            Me.tslLoading.Text = Master.eLang.GetString(1205, "Verifying MovieSet Details:")
            Application.DoEvents()

            Edit_MovieSet(DBMovieSet)
        End If

        Me.pnlCancel.Visible = False
        Me.tslLoading.Visible = False
        Me.tspbLoading.Visible = False
        Me.SetStatus(String.Empty)
        Me.SetControlsEnabled(True)
        Me.EnableFilters_MovieSets(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_MovieSet(ByVal iPercent As Integer)
        If Me.ReportDownloadPercent = True Then
            Me.tspbLoading.Value = iPercent
            Me.Refresh()
        End If
    End Sub

    Private Sub CreateScrapeList_MovieSet(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions_MovieSet, ByVal ScrapeModifier As Structures.ScrapeModifier)
        Dim DataRowList As New List(Of DataRow)
        Dim ScrapeList As New List(Of ScrapeItem)

        Select Case sType
            Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip, _
                Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
                'create snapshoot list of selected moviesets
                For Each sRow As DataGridViewRow In Me.dgvMovieSets.SelectedRows
                    DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
                Next
            Case Else
                For Each sRow As DataRow In Me.dtMovieSets.Rows
                    DataRowList.Add(sRow)
                Next
        End Select

        Dim BannerAllowed As Boolean = Master.eSettings.MovieSetBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainBanner)
        Dim ClearArtAllowed As Boolean = Master.eSettings.MovieSetClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearArt)
        Dim ClearLogoAllowed As Boolean = Master.eSettings.MovieSetClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearLogo)
        Dim DiscArtAllowed As Boolean = Master.eSettings.MovieSetDiscArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainDiscArt)
        Dim FanartAllowed As Boolean = Master.eSettings.MovieSetFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainFanart)
        Dim LandscapeAllowed As Boolean = Master.eSettings.MovieSetLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainLandscape)
        Dim PosterAllowed As Boolean = Master.eSettings.MovieSetPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainPoster)

        'create ScrapeList of moviesets acording to scrapetype
        For Each drvRow As DataRow In DataRowList
            If Convert.ToBoolean(drvRow.Item("Lock")) Then Continue For

            Dim sModifier As New Structures.ScrapeModifier
            sModifier.DoSearch = ScrapeModifier.DoSearch
            sModifier.MainBanner = ScrapeModifier.MainBanner AndAlso BannerAllowed
            sModifier.MainClearArt = ScrapeModifier.MainClearArt AndAlso ClearArtAllowed
            sModifier.MainClearLogo = ScrapeModifier.MainClearLogo AndAlso ClearLogoAllowed
            sModifier.MainDiscArt = ScrapeModifier.MainDiscArt AndAlso DiscArtAllowed
            sModifier.MainFanart = ScrapeModifier.MainFanart AndAlso FanartAllowed
            sModifier.MainLandscape = ScrapeModifier.MainLandscape AndAlso LandscapeAllowed
            sModifier.MainNFO = ScrapeModifier.MainNFO
            sModifier.MainPoster = ScrapeModifier.MainPoster AndAlso PosterAllowed

            Select Case sType
                Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
                    If Not Convert.ToBoolean(drvRow.Item("New")) Then Continue For
                Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
                    If Not Convert.ToBoolean(drvRow.Item("Mark")) Then Continue For
                Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
                    Dim index As Integer = Me.bsMovieSets.Find("idSet", drvRow.Item(0))
                    If Not index >= 0 Then Continue For
                Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
                    If Not String.IsNullOrEmpty(drvRow.Item("BannerPath").ToString) Then sModifier.MainBanner = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ClearArtPath").ToString) Then sModifier.MainClearArt = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ClearLogoPath").ToString) Then sModifier.MainClearLogo = False
                    If Not String.IsNullOrEmpty(drvRow.Item("DiscArtPath").ToString) Then sModifier.MainDiscArt = False
                    If Not String.IsNullOrEmpty(drvRow.Item("FanartPath").ToString) Then sModifier.MainFanart = False
                    If Not String.IsNullOrEmpty(drvRow.Item("LandscapePath").ToString) Then sModifier.MainLandscape = False
                    If Not String.IsNullOrEmpty(drvRow.Item("NfoPath").ToString) Then sModifier.MainNFO = False
                    If Not String.IsNullOrEmpty(drvRow.Item("PosterPath").ToString) Then sModifier.MainPoster = False
            End Select
            ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifier = sModifier})
        Next

        Me.SetControlsEnabled(False)

        Me.tspbLoading.Value = 0
        If ScrapeList.Count > 1 Then
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.tspbLoading.Maximum = ScrapeList.Count
        Else
            Me.tspbLoading.Maximum = 100
            Me.tspbLoading.Style = ProgressBarStyle.Marquee
        End If

        Select Case sType
            Case Enums.ScrapeType.AllAsk
                Me.tslLoading.Text = Master.eLang.GetString(1215, "Scraping Media (All MovieSets - Ask):")
            Case Enums.ScrapeType.AllAuto
                Me.tslLoading.Text = Master.eLang.GetString(1216, "Scraping Media (All MovieSets - Auto):")
            Case Enums.ScrapeType.AllSkip
                Me.tslLoading.Text = Master.eLang.GetString(1217, "Scraping Media (All MovieSets - Skip):")
            Case Enums.ScrapeType.MissingAuto
                Me.tslLoading.Text = Master.eLang.GetString(1218, "Scraping Media (MovieSets Missing Items - Auto):")
            Case Enums.ScrapeType.MissingAsk
                Me.tslLoading.Text = Master.eLang.GetString(1219, "Scraping Media (MovieSets Missing Items - Ask):")
            Case Enums.ScrapeType.MissingSkip
                Me.tslLoading.Text = Master.eLang.GetString(1220, "Scraping Media (MovieSets Missing Items - Skip):")
            Case Enums.ScrapeType.NewAsk
                Me.tslLoading.Text = Master.eLang.GetString(1221, "Scraping Media (New MovieSets - Ask):")
            Case Enums.ScrapeType.NewAuto
                Me.tslLoading.Text = Master.eLang.GetString(1222, "Scraping Media (New MovieSets - Auto):")
            Case Enums.ScrapeType.NewSkip
                Me.tslLoading.Text = Master.eLang.GetString(1223, "Scraping Media (New MovieSets - Skip):")
            Case Enums.ScrapeType.MarkedAsk
                Me.tslLoading.Text = Master.eLang.GetString(1224, "Scraping Media (Marked MovieSets - Ask):")
            Case Enums.ScrapeType.MarkedAuto
                Me.tslLoading.Text = Master.eLang.GetString(1225, "Scraping Media (Marked MovieSets - Auto):")
            Case Enums.ScrapeType.MarkedSkip
                Me.tslLoading.Text = Master.eLang.GetString(1226, "Scraping Media (Marked MovieSets - Skip):")
            Case Enums.ScrapeType.FilterAsk
                Me.tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
            Case Enums.ScrapeType.FilterAuto
                Me.tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
            Case Enums.ScrapeType.FilterAuto
                Me.tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
            Case Enums.ScrapeType.AllAsk
                Me.tslLoading.Text = Master.eLang.GetString(1358, "Scraping Media (Selected MovieSets - Ask):")
            Case Enums.ScrapeType.AllAuto
                Me.tslLoading.Text = Master.eLang.GetString(1359, "Scraping Media (Selected MovieSets - Auto):")
            Case Enums.ScrapeType.AllSkip
                Me.tslLoading.Text = Master.eLang.GetString(1360, "Scraping Media (Selected MovieSets - Skip):")
            Case Enums.ScrapeType.SingleField
                Me.tslLoading.Text = Master.eLang.GetString(1357, "Scraping Media (Selected MovieSets - Single Field):")
            Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
                Me.tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
        End Select


        If Not sType = Enums.ScrapeType.SingleScrape Then
            Me.btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
            Me.lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
            Me.btnCancel.Visible = True
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = True
        End If

        Me.tslLoading.Visible = True
        Me.tspbLoading.Visible = True
        Application.DoEvents()
        bwMovieSetScraper.WorkerSupportsCancellation = True
        bwMovieSetScraper.WorkerReportsProgress = True
        bwMovieSetScraper.RunWorkerAsync(New Arguments With {.Options_MovieSet = ScrapeOptions, .ScrapeList = ScrapeList, .scrapeType = sType})
    End Sub

    Private Sub InfoDownloaded_TV(ByRef DBTVShow As Database.DBElement)
        If Not String.IsNullOrEmpty(DBTVShow.TVShow.Title) Then
            Me.tslLoading.Text = Master.eLang.GetString(761, "Verifying TV Show Details:")
            Application.DoEvents()

            Edit_TVShow(DBTVShow)
        End If

        Me.pnlCancel.Visible = False
        Me.tslLoading.Visible = False
        Me.tspbLoading.Visible = False
        Me.SetStatus(String.Empty)
        Me.SetControlsEnabled(True)
        Me.EnableFilters_Shows(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_TV(ByVal iPercent As Integer)
        If Me.ReportDownloadPercent = True Then
            Me.tspbLoading.Value = iPercent
            Me.Refresh()
        End If
    End Sub

    Private Sub CreateScrapeList_TV(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions_TV, ByVal ScrapeModifier As Structures.ScrapeModifier)
        Dim DataRowList As New List(Of DataRow)
        Dim ScrapeList As New List(Of ScrapeItem)

        Select Case sType
            Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip, _
                Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
                'create snapshoot list of selected tv show
                For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                    DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
                Next
            Case Else
                For Each sRow As DataRow In Me.dtTVShows.Rows
                    DataRowList.Add(sRow)
                Next
        End Select

        Dim AllSeasonsBannerAllowed As Boolean = Master.eSettings.TVAllSeasonsBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsBanner)
        Dim AllSeasonsFanartAllowed As Boolean = Master.eSettings.TVAllSeasonsFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsFanart)
        Dim AllSeasonsLandscapeAllowed As Boolean = Master.eSettings.TVAllSeasonsLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsLandscape)
        Dim AllSeasonsPosterAllowed As Boolean = Master.eSettings.TVAllSeasonsPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsPoster)
        Dim EpisodeActorThumbsAllowed As Boolean = Master.eSettings.TVEpisodeActorThumbsAnyEnabled
        Dim EpisodeFanartAllowed As Boolean = Master.eSettings.TVEpisodeFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart)
        Dim EpisodeMetaAllowed As Boolean = Master.eSettings.TVScraperMetaDataScan
        Dim EpisodePosterAllowed As Boolean = Master.eSettings.TVEpisodePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster)
        Dim MainActorThumbsAllowed As Boolean = Master.eSettings.TVShowActorThumbsAnyEnabled
        Dim MainBannerAllowed As Boolean = Master.eSettings.TVShowBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)
        Dim MainCharacterArtAllowed As Boolean = Master.eSettings.TVShowCharacterArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
        Dim MainClearArtAllowed As Boolean = Master.eSettings.TVShowClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
        Dim MainClearLogoAllowed As Boolean = Master.eSettings.TVShowClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
        Dim MainEFanartsAllowed As Boolean = Master.eSettings.TVShowExtrafanartsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
        Dim MainFanartAllowed As Boolean = Master.eSettings.TVShowFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
        Dim MainLandscapeAllowed As Boolean = Master.eSettings.TVShowLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
        Dim MainPosterAllowed As Boolean = Master.eSettings.TVShowPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
        Dim MainThemeAllowed As Boolean = Master.eSettings.TvShowThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_TV(Enums.ModifierType.MainTheme)
        Dim SeasonBannerAllowed As Boolean = Master.eSettings.TVSeasonBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)
        Dim SeasonFanartAllowed As Boolean = Master.eSettings.TVSeasonFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart)
        Dim SeasonLandscapeAllowed As Boolean = Master.eSettings.TVSeasonLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)
        Dim SeasonPosterAllowed As Boolean = Master.eSettings.TVSeasonPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)

        'create ScrapeList of tv shows acording to scrapetype
        For Each drvRow As DataRow In DataRowList
            If Convert.ToBoolean(drvRow.Item("Lock")) Then Continue For

            Dim sModifier As New Structures.ScrapeModifier
            sModifier.DoSearch = ScrapeModifier.DoSearch
            sModifier.AllSeasonsBanner = ScrapeModifier.AllSeasonsBanner AndAlso AllSeasonsBannerAllowed
            sModifier.AllSeasonsFanart = ScrapeModifier.AllSeasonsFanart AndAlso AllSeasonsFanartAllowed
            sModifier.AllSeasonsLandscape = ScrapeModifier.AllSeasonsLandscape AndAlso AllSeasonsLandscapeAllowed
            sModifier.AllSeasonsPoster = ScrapeModifier.AllSeasonsPoster AndAlso AllSeasonsPosterAllowed
            sModifier.EpisodeActorThumbs = ScrapeModifier.EpisodeActorThumbs AndAlso EpisodeActorThumbsAllowed
            sModifier.EpisodeFanart = ScrapeModifier.EpisodeFanart AndAlso EpisodeFanartAllowed
            sModifier.EpisodeMeta = ScrapeModifier.EpisodeMeta AndAlso EpisodeMetaAllowed
            sModifier.EpisodeNFO = ScrapeModifier.EpisodeNFO
            sModifier.EpisodePoster = ScrapeModifier.EpisodePoster AndAlso EpisodePosterAllowed
            sModifier.MainActorthumbs = ScrapeModifier.MainActorthumbs AndAlso MainActorThumbsAllowed
            sModifier.MainBanner = ScrapeModifier.MainBanner AndAlso MainBannerAllowed
            sModifier.MainCharacterArt = ScrapeModifier.MainCharacterArt AndAlso MainCharacterArtAllowed
            sModifier.MainClearArt = ScrapeModifier.MainClearArt AndAlso MainClearArtAllowed
            sModifier.MainClearLogo = ScrapeModifier.MainClearLogo AndAlso MainClearLogoAllowed
            sModifier.MainExtrafanarts = ScrapeModifier.MainExtrafanarts AndAlso MainEFanartsAllowed
            sModifier.MainFanart = ScrapeModifier.MainFanart AndAlso MainFanartAllowed
            sModifier.MainLandscape = ScrapeModifier.MainLandscape AndAlso MainLandscapeAllowed
            sModifier.MainNFO = ScrapeModifier.MainNFO
            sModifier.MainPoster = ScrapeModifier.MainPoster AndAlso MainPosterAllowed
            sModifier.MainTheme = ScrapeModifier.MainTheme AndAlso MainThemeAllowed
            sModifier.SeasonBanner = ScrapeModifier.SeasonBanner AndAlso SeasonBannerAllowed
            sModifier.SeasonFanart = ScrapeModifier.SeasonFanart AndAlso SeasonFanartAllowed
            sModifier.SeasonLandscape = ScrapeModifier.SeasonLandscape AndAlso SeasonLandscapeAllowed
            sModifier.SeasonPoster = ScrapeModifier.SeasonPoster AndAlso SeasonPosterAllowed
            sModifier.withEpisodes = ScrapeModifier.withEpisodes
            sModifier.withSeasons = ScrapeModifier.withSeasons

            Select Case sType
                Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
                    If Not Convert.ToBoolean(drvRow.Item("New")) Then Continue For
                Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
                    If Not Convert.ToBoolean(drvRow.Item("Mark")) Then Continue For
                Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
                    Dim index As Integer = Me.bsTVShows.Find("idShow", drvRow.Item(0))
                    If Not index >= 0 Then Continue For
                Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
                    If Not String.IsNullOrEmpty(drvRow.Item("BannerPath").ToString) Then sModifier.MainBanner = False
                    If Not String.IsNullOrEmpty(drvRow.Item("CharacterArtPath").ToString) Then sModifier.MainCharacterArt = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ClearArtPath").ToString) Then sModifier.MainClearArt = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ClearLogoPath").ToString) Then sModifier.MainClearLogo = False
                    If Not String.IsNullOrEmpty(drvRow.Item("EFanartsPath").ToString) Then sModifier.MainExtrafanarts = False
                    If Not String.IsNullOrEmpty(drvRow.Item("FanartPath").ToString) Then sModifier.MainFanart = False
                    If Not String.IsNullOrEmpty(drvRow.Item("LandscapePath").ToString) Then sModifier.MainLandscape = False
                    If Not String.IsNullOrEmpty(drvRow.Item("NfoPath").ToString) Then sModifier.MainNFO = False
                    If Not String.IsNullOrEmpty(drvRow.Item("PosterPath").ToString) Then sModifier.MainPoster = False
                    If Not String.IsNullOrEmpty(drvRow.Item("ThemePath").ToString) Then sModifier.MainTheme = False
            End Select
            ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifier = sModifier})
        Next

        Me.SetControlsEnabled(False)

        Me.tspbLoading.Value = 0
        If ScrapeList.Count > 1 Then
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.tspbLoading.Maximum = ScrapeList.Count
        Else
            Me.tspbLoading.Maximum = 100
            Me.tspbLoading.Style = ProgressBarStyle.Marquee
        End If

        Select Case sType
            Case Enums.ScrapeType.AllAsk
                Me.tslLoading.Text = Master.eLang.GetString(127, "Scraping Media (All Movies - Ask):")
            Case Enums.ScrapeType.AllAuto
                Me.tslLoading.Text = Master.eLang.GetString(128, "Scraping Media (All Movies - Auto):")
            Case Enums.ScrapeType.AllSkip
                Me.tslLoading.Text = Master.eLang.GetString(853, "Scraping Media (All Movies - Skip):")
            Case Enums.ScrapeType.MissingAuto
                Me.tslLoading.Text = Master.eLang.GetString(132, "Scraping Media (Movies Missing Items - Auto):")
            Case Enums.ScrapeType.MissingAsk
                Me.tslLoading.Text = Master.eLang.GetString(133, "Scraping Media (Movies Missing Items - Ask):")
            Case Enums.ScrapeType.MissingSkip
                Me.tslLoading.Text = Master.eLang.GetString(1042, "Scraping Media (Movies Missing Items - Skip):")
            Case Enums.ScrapeType.NewAsk
                Me.tslLoading.Text = Master.eLang.GetString(134, "Scraping Media (New Movies - Ask):")
            Case Enums.ScrapeType.NewAuto
                Me.tslLoading.Text = Master.eLang.GetString(135, "Scraping Media (New Movies - Auto):")
            Case Enums.ScrapeType.NewSkip
                Me.tslLoading.Text = Master.eLang.GetString(1043, "Scraping Media (New Movies - Skip):")
            Case Enums.ScrapeType.MarkedAsk
                Me.tslLoading.Text = Master.eLang.GetString(136, "Scraping Media (Marked Movies - Ask):")
            Case Enums.ScrapeType.MarkedAuto
                Me.tslLoading.Text = Master.eLang.GetString(137, "Scraping Media (Marked Movies - Auto):")
            Case Enums.ScrapeType.MarkedSkip
                Me.tslLoading.Text = Master.eLang.GetString(1044, "Scraping Media (Marked Movies - Skip):")
            Case Enums.ScrapeType.FilterAsk
                Me.tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
            Case Enums.ScrapeType.FilterAuto
                Me.tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
            Case Enums.ScrapeType.FilterAuto
                Me.tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
            Case Enums.ScrapeType.SelectedAsk
                Me.tslLoading.Text = Master.eLang.GetString(1128, "Scraping Media (Selected Movies - Ask):")
            Case Enums.ScrapeType.SelectedAuto
                Me.tslLoading.Text = Master.eLang.GetString(1129, "Scraping Media (Selected Movies - Auto):")
            Case Enums.ScrapeType.SelectedSkip
                Me.tslLoading.Text = Master.eLang.GetString(1130, "Scraping Media (Selected Movies - Skip):")
            Case Enums.ScrapeType.SingleField
                Me.tslLoading.Text = Master.eLang.GetString(1127, "Scraping Media (Selected Movies - Single Field):")
            Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
                Me.tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
        End Select

        If Not sType = Enums.ScrapeType.SingleScrape Then
            Me.btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
            Me.lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
            Me.btnCancel.Visible = True
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = True
        End If

        Me.tslLoading.Visible = True
        Me.tspbLoading.Visible = True
        Application.DoEvents()
        bwTVScraper.WorkerSupportsCancellation = True
        bwTVScraper.WorkerReportsProgress = True
        bwTVScraper.RunWorkerAsync(New Arguments With {.Options_TV = ScrapeOptions, .ScrapeList = ScrapeList, .scrapeType = sType})
    End Sub

    Private Sub InfoDownloaded_TVEpisode(ByRef DBTVEpisode As Database.DBElement)
        If Not String.IsNullOrEmpty(DBTVEpisode.TVEpisode.Title) Then
            Me.tslLoading.Text = Master.eLang.GetString(762, "Verifying TV Episode Details:")
            Application.DoEvents()

            Edit_TVEpisode(DBTVEpisode)
        End If

        Me.pnlCancel.Visible = False
        Me.tslLoading.Visible = False
        Me.tspbLoading.Visible = False
        Me.SetStatus(String.Empty)
        Me.SetControlsEnabled(True)
        Me.EnableFilters_Shows(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_TVEpisode(ByVal iPercent As Integer)
        If Me.ReportDownloadPercent = True Then
            Me.tspbLoading.Value = iPercent
            Me.Refresh()
        End If
    End Sub

    Private Sub CreateScrapeList_TVEpisode(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions_TV, ByVal ScrapeModifier As Structures.ScrapeModifier)
        Dim DataRowList As New List(Of DataRow)
        Dim ScrapeList As New List(Of ScrapeItem)

        Select Case sType
            Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip, _
                Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
                'create snapshoot list of selected tv episode
                For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                    DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
                Next
            Case Else
                For Each sRow As DataRow In Me.dtTVEpisodes.Rows
                    DataRowList.Add(sRow)
                Next
        End Select

        Dim ActorThumbsAllowed As Boolean = Master.eSettings.TVEpisodeActorThumbsAnyEnabled
        Dim FanartAllowed As Boolean = Master.eSettings.TVEpisodeFanartAnyEnabled AndAlso (ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart) OrElse _
                                                                                           ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart))
        Dim PosterAllowed As Boolean = Master.eSettings.TVEpisodePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster)

        'create ScrapeList of episodes acording to scrapetype
        For Each drvRow As DataRow In DataRowList
            If Convert.ToBoolean(drvRow.Item("Lock")) Then Continue For

            Dim sModifier As New Structures.ScrapeModifier
            sModifier.DoSearch = ScrapeModifier.DoSearch
            sModifier.EpisodeActorThumbs = ScrapeModifier.EpisodeActorThumbs AndAlso ActorThumbsAllowed
            sModifier.EpisodeFanart = ScrapeModifier.EpisodeFanart AndAlso FanartAllowed
            sModifier.EpisodeMeta = ScrapeModifier.EpisodeMeta
            sModifier.EpisodeNFO = ScrapeModifier.EpisodeNFO
            sModifier.EpisodePoster = ScrapeModifier.EpisodePoster AndAlso PosterAllowed

            Select Case sType
                Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
                    If Not Convert.ToBoolean(drvRow.Item("New")) Then Continue For
                Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
                    If Not Convert.ToBoolean(drvRow.Item("Mark")) Then Continue For
                Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
                    Dim index As Integer = Me.bsTVEpisodes.Find("idEpisode", drvRow.Item(0))
                    If Not index >= 0 Then Continue For
                Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
                    If Not String.IsNullOrEmpty(drvRow.Item("FanartPath").ToString) Then sModifier.EpisodeFanart = False
                    If Not String.IsNullOrEmpty(drvRow.Item("NfoPath").ToString) Then sModifier.EpisodeNFO = False
                    If Not String.IsNullOrEmpty(drvRow.Item("PosterPath").ToString) Then sModifier.EpisodePoster = False
            End Select
            ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifier = sModifier})
        Next

        Me.SetControlsEnabled(False)

        Me.tspbLoading.Value = 0
        If ScrapeList.Count > 1 Then
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.tspbLoading.Maximum = ScrapeList.Count
        Else
            Me.tspbLoading.Maximum = 100
            Me.tspbLoading.Style = ProgressBarStyle.Marquee
        End If

        Select Case sType
            Case Enums.ScrapeType.AllAsk
                Me.tslLoading.Text = Master.eLang.GetString(127, "Scraping Media (All Movies - Ask):")
            Case Enums.ScrapeType.AllAuto
                Me.tslLoading.Text = Master.eLang.GetString(128, "Scraping Media (All Movies - Auto):")
            Case Enums.ScrapeType.AllSkip
                Me.tslLoading.Text = Master.eLang.GetString(853, "Scraping Media (All Movies - Skip):")
            Case Enums.ScrapeType.MissingAuto
                Me.tslLoading.Text = Master.eLang.GetString(132, "Scraping Media (Movies Missing Items - Auto):")
            Case Enums.ScrapeType.MissingAsk
                Me.tslLoading.Text = Master.eLang.GetString(133, "Scraping Media (Movies Missing Items - Ask):")
            Case Enums.ScrapeType.MissingSkip
                Me.tslLoading.Text = Master.eLang.GetString(1042, "Scraping Media (Movies Missing Items - Skip):")
            Case Enums.ScrapeType.NewAsk
                Me.tslLoading.Text = Master.eLang.GetString(134, "Scraping Media (New Movies - Ask):")
            Case Enums.ScrapeType.NewAuto
                Me.tslLoading.Text = Master.eLang.GetString(135, "Scraping Media (New Movies - Auto):")
            Case Enums.ScrapeType.NewSkip
                Me.tslLoading.Text = Master.eLang.GetString(1043, "Scraping Media (New Movies - Skip):")
            Case Enums.ScrapeType.MarkedAsk
                Me.tslLoading.Text = Master.eLang.GetString(136, "Scraping Media (Marked Movies - Ask):")
            Case Enums.ScrapeType.MarkedAuto
                Me.tslLoading.Text = Master.eLang.GetString(137, "Scraping Media (Marked Movies - Auto):")
            Case Enums.ScrapeType.MarkedSkip
                Me.tslLoading.Text = Master.eLang.GetString(1044, "Scraping Media (Marked Movies - Skip):")
            Case Enums.ScrapeType.FilterAsk
                Me.tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
            Case Enums.ScrapeType.FilterAuto
                Me.tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
            Case Enums.ScrapeType.FilterAuto
                Me.tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
            Case Enums.ScrapeType.AllAsk
                Me.tslLoading.Text = Master.eLang.GetString(1128, "Scraping Media (Selected Movies - Ask):")
            Case Enums.ScrapeType.AllAuto
                Me.tslLoading.Text = Master.eLang.GetString(1129, "Scraping Media (Selected Movies - Auto):")
            Case Enums.ScrapeType.AllSkip
                Me.tslLoading.Text = Master.eLang.GetString(1130, "Scraping Media (Selected Movies - Skip):")
            Case Enums.ScrapeType.SingleField
                Me.tslLoading.Text = Master.eLang.GetString(1127, "Scraping Media (Selected Movies - Single Field):")
            Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
                Me.tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
        End Select

        If Not sType = Enums.ScrapeType.SingleScrape Then
            Me.btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
            Me.lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
            Me.btnCancel.Visible = True
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = True
        End If

        Me.tslLoading.Visible = True
        Me.tspbLoading.Visible = True
        Application.DoEvents()
        bwTVEpisodeScraper.WorkerSupportsCancellation = True
        bwTVEpisodeScraper.WorkerReportsProgress = True
        bwTVEpisodeScraper.RunWorkerAsync(New Arguments With {.Options_TV = ScrapeOptions, .ScrapeList = ScrapeList, .scrapeType = sType})
    End Sub

    Private Sub InfoDownloaded_TVSeason(ByRef DBTVSeason As Database.DBElement)
        If Not String.IsNullOrEmpty(DBTVSeason.TVShow.Title) Then
            Me.tslLoading.Text = Master.eLang.GetString(80, "Verifying TV Season Details:")
            Application.DoEvents()

            Edit_TVSeason(DBTVSeason)
        End If

        Me.pnlCancel.Visible = False
        Me.tslLoading.Visible = False
        Me.tspbLoading.Visible = False
        Me.SetStatus(String.Empty)
        Me.SetControlsEnabled(True)
        Me.EnableFilters_Shows(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_TVSeason(ByVal iPercent As Integer)
        If Me.ReportDownloadPercent = True Then
            Me.tspbLoading.Value = iPercent
            Me.Refresh()
        End If
    End Sub

    Private Sub CreateScrapeList_TVSeason(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions_TV, ByVal ScrapeModifier As Structures.ScrapeModifier)
        Dim DataRowList As New List(Of DataRow)
        Dim ScrapeList As New List(Of ScrapeItem)

        Select Case sType
            Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip, _
                Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
                'create snapshoot list of selected tv season
                For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                    DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
                Next
            Case Else
                For Each sRow As DataRow In Me.dtTVSeasons.Rows
                    DataRowList.Add(sRow)
                Next
        End Select

        Dim AllSeasonsBannerAllowed As Boolean = Master.eSettings.TVAllSeasonsBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsBanner)
        Dim AllSeasonsFanartAllowed As Boolean = Master.eSettings.TVAllSeasonsFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsFanart)
        Dim AllSeasonsLandscapeAllowed As Boolean = Master.eSettings.TVAllSeasonsLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsLandscape)
        Dim AllSeasonsPosterAllowed As Boolean = Master.eSettings.TVAllSeasonsPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsPoster)
        Dim SeasonBannerAllowed As Boolean = Master.eSettings.TVSeasonBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)
        Dim SeasonFanartAllowed As Boolean = Master.eSettings.TVSeasonFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart)
        Dim SeasonLandscapeAllowed As Boolean = Master.eSettings.TVSeasonLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)
        Dim SeasonPosterAllowed As Boolean = Master.eSettings.TVSeasonPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)

        'create ScrapeList of tv seasons acording to scrapetype
        For Each drvRow As DataRow In DataRowList
            If Convert.ToBoolean(drvRow.Item("Lock")) Then Continue For

            Dim sModifier As New Structures.ScrapeModifier
            sModifier.DoSearch = ScrapeModifier.DoSearch
            sModifier.AllSeasonsBanner = ScrapeModifier.AllSeasonsBanner AndAlso AllSeasonsBannerAllowed AndAlso CInt(drvRow.Item("Season")) = 999
            sModifier.AllSeasonsFanart = ScrapeModifier.AllSeasonsFanart AndAlso AllSeasonsFanartAllowed AndAlso CInt(drvRow.Item("Season")) = 999
            sModifier.AllSeasonsLandscape = ScrapeModifier.AllSeasonsLandscape AndAlso AllSeasonsLandscapeAllowed AndAlso CInt(drvRow.Item("Season")) = 999
            sModifier.AllSeasonsPoster = ScrapeModifier.AllSeasonsPoster AndAlso AllSeasonsPosterAllowed AndAlso CInt(drvRow.Item("Season")) = 999
            sModifier.SeasonBanner = ScrapeModifier.SeasonBanner AndAlso SeasonBannerAllowed AndAlso Not CInt(drvRow.Item("Season")) = 999
            sModifier.SeasonFanart = ScrapeModifier.SeasonFanart AndAlso SeasonFanartAllowed AndAlso Not CInt(drvRow.Item("Season")) = 999
            sModifier.SeasonLandscape = ScrapeModifier.SeasonLandscape AndAlso SeasonLandscapeAllowed AndAlso Not CInt(drvRow.Item("Season")) = 999
            sModifier.SeasonPoster = ScrapeModifier.SeasonPoster AndAlso SeasonPosterAllowed AndAlso Not CInt(drvRow.Item("Season")) = 999

            Select Case sType
                Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
                    If Not Convert.ToBoolean(drvRow.Item("New")) Then Continue For
                Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
                    If Not Convert.ToBoolean(drvRow.Item("Mark")) Then Continue For
                Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
                    Dim index As Integer = Me.bsTVShows.Find("idShow", drvRow.Item(0))
                    If Not index >= 0 Then Continue For
                Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
                    If Not String.IsNullOrEmpty(drvRow.Item("BannerPath").ToString) Then sModifier.SeasonBanner = False
                    If Not String.IsNullOrEmpty(drvRow.Item("FanartPath").ToString) Then sModifier.SeasonFanart = False
                    If Not String.IsNullOrEmpty(drvRow.Item("LandscapePath").ToString) Then sModifier.SeasonLandscape = False
                    If Not String.IsNullOrEmpty(drvRow.Item("PosterPath").ToString) Then sModifier.SeasonPoster = False
            End Select
            ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifier = sModifier})
        Next

        Me.SetControlsEnabled(False)

        Me.tspbLoading.Value = 0
        If ScrapeList.Count > 1 Then
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.tspbLoading.Maximum = ScrapeList.Count
        Else
            Me.tspbLoading.Maximum = 100
            Me.tspbLoading.Style = ProgressBarStyle.Marquee
        End If

        Select Case sType
            Case Enums.ScrapeType.AllAsk
                Me.tslLoading.Text = Master.eLang.GetString(127, "Scraping Media (All Movies - Ask):")
            Case Enums.ScrapeType.AllAuto
                Me.tslLoading.Text = Master.eLang.GetString(128, "Scraping Media (All Movies - Auto):")
            Case Enums.ScrapeType.AllSkip
                Me.tslLoading.Text = Master.eLang.GetString(853, "Scraping Media (All Movies - Skip):")
            Case Enums.ScrapeType.MissingAuto
                Me.tslLoading.Text = Master.eLang.GetString(132, "Scraping Media (Movies Missing Items - Auto):")
            Case Enums.ScrapeType.MissingAsk
                Me.tslLoading.Text = Master.eLang.GetString(133, "Scraping Media (Movies Missing Items - Ask):")
            Case Enums.ScrapeType.MissingSkip
                Me.tslLoading.Text = Master.eLang.GetString(1042, "Scraping Media (Movies Missing Items - Skip):")
            Case Enums.ScrapeType.NewAsk
                Me.tslLoading.Text = Master.eLang.GetString(134, "Scraping Media (New Movies - Ask):")
            Case Enums.ScrapeType.NewAuto
                Me.tslLoading.Text = Master.eLang.GetString(135, "Scraping Media (New Movies - Auto):")
            Case Enums.ScrapeType.NewSkip
                Me.tslLoading.Text = Master.eLang.GetString(1043, "Scraping Media (New Movies - Skip):")
            Case Enums.ScrapeType.MarkedAsk
                Me.tslLoading.Text = Master.eLang.GetString(136, "Scraping Media (Marked Movies - Ask):")
            Case Enums.ScrapeType.MarkedAuto
                Me.tslLoading.Text = Master.eLang.GetString(137, "Scraping Media (Marked Movies - Auto):")
            Case Enums.ScrapeType.MarkedSkip
                Me.tslLoading.Text = Master.eLang.GetString(1044, "Scraping Media (Marked Movies - Skip):")
            Case Enums.ScrapeType.FilterAsk
                Me.tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
            Case Enums.ScrapeType.FilterAuto
                Me.tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
            Case Enums.ScrapeType.FilterAuto
                Me.tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
            Case Enums.ScrapeType.SelectedAsk
                Me.tslLoading.Text = Master.eLang.GetString(1128, "Scraping Media (Selected Movies - Ask):")
            Case Enums.ScrapeType.SelectedAuto
                Me.tslLoading.Text = Master.eLang.GetString(1129, "Scraping Media (Selected Movies - Auto):")
            Case Enums.ScrapeType.SelectedSkip
                Me.tslLoading.Text = Master.eLang.GetString(1130, "Scraping Media (Selected Movies - Skip):")
            Case Enums.ScrapeType.SingleField
                Me.tslLoading.Text = Master.eLang.GetString(1127, "Scraping Media (Selected Movies - Single Field):")
            Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
                Me.tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
        End Select

        If Not sType = Enums.ScrapeType.SingleScrape Then
            Me.btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
            Me.lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
            Me.btnCancel.Visible = True
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = True
        End If

        Me.tslLoading.Visible = True
        Me.tspbLoading.Visible = True
        Application.DoEvents()
        bwTVSeasonScraper.WorkerSupportsCancellation = True
        bwTVSeasonScraper.WorkerReportsProgress = True
        bwTVSeasonScraper.RunWorkerAsync(New Arguments With {.Options_TV = ScrapeOptions, .ScrapeList = ScrapeList, .scrapeType = sType})
    End Sub

    Function MyResolveEventHandler(ByVal sender As Object, ByVal args As ResolveEventArgs) As [Assembly]
        Dim name As String = args.Name.Split(Convert.ToChar(","))(0)
        Dim asm As Assembly = ModulesManager.AssemblyList.FirstOrDefault(Function(y) y.AssemblyName = name).Assembly
        If asm Is Nothing Then
            asm = ModulesManager.AssemblyList.FirstOrDefault(Function(y) y.AssemblyName = name.Split(Convert.ToChar("."))(0)).Assembly
        End If
        Return asm
    End Function

    Private Sub NonScrape(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions_Movie)
        Me.Cursor = Cursors.WaitCursor

        Select Case sType
            Case Enums.ScrapeType.CleanFolders
                Me.btnCancel.Text = Master.eLang.GetString(120, "Cancel Cleaner")
                Me.lblCanceling.Text = Master.eLang.GetString(119, "Canceling File Cleaner...")
                Me.tslLoading.Text = Master.eLang.GetString(129, "Cleaning Files:")
            Case Enums.ScrapeType.CopyBackdrops
                Me.btnCancel.Text = Master.eLang.GetString(122, "Cancel Copy")
                Me.lblCanceling.Text = Master.eLang.GetString(121, "Canceling Backdrop Copy...")
                Me.tslLoading.Text = Master.eLang.GetString(130, "Copying Fanart to Backdrops Folder:")
            Case Else
                logger.Warn("Invalid sType: <{0}>", sType)
        End Select

        btnCancel.Visible = True
        lblCanceling.Visible = False
        prbCanceling.Visible = False
        Me.pnlCancel.Visible = True
        Me.tslLoading.Visible = True
        Me.tspbLoading.Value = 0
        Me.tspbLoading.Maximum = Me.dtMovies.Rows.Count
        Me.tspbLoading.Visible = True
        Me.SetControlsEnabled(False, True)
        Me.EnableFilters_Movies(False)
        Me.EnableFilters_MovieSets(False)
        Me.EnableFilters_Shows(False)

        bwNonScrape.WorkerReportsProgress = True
        bwNonScrape.WorkerSupportsCancellation = True
        bwNonScrape.RunWorkerAsync(New Arguments With {.ScrapeType = sType, .Options_Movie = ScrapeOptions})
    End Sub

    Private Sub cmnuMovieOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieOpenFolder.Click
        If Me.dgvMovies.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If Me.dgvMovies.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), Me.dgvMovies.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then doOpen = False
            End If

            If doOpen Then
                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                    Using Explorer As New Diagnostics.Process

                        If Master.isWindows Then
                            Explorer.StartInfo.FileName = "explorer.exe"
                            Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", sRow.Cells("MoviePath").Value)
                        Else
                            Explorer.StartInfo.FileName = "xdg-open"
                            Explorer.StartInfo.Arguments = String.Format("""{0}""", Path.GetDirectoryName(sRow.Cells("MoviePath").Value.ToString))
                        End If
                        Explorer.Start()
                    End Using
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Disable TMDB/IMDB menutitem if selected movies don't have TMDBID/IMDBID
    ''' </summary>
    ''' <param name="sender">movielist contextmenu</param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' 
    ''' 
    ''' 2014/10/10 Cocotus - First implementation: This is used to disable TMDB/IMDB menuitem(s) if not a single movie of selected movies has IMDBID or TMDBID
    ''' </remarks>
    ''' 
    Private Sub cmnuMovie_Opened(sender As Object, e As EventArgs) Handles cmnuMovie.Opened
        If Me.dgvMovies.SelectedRows.Count > 0 Then
            Dim enableIMDB As Boolean = False
            Dim enableTMDB As Boolean = False
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                If Not String.IsNullOrEmpty(sRow.Cells("Imdb").Value.ToString) Then
                    enableIMDB = True
                End If
                If Not String.IsNullOrEmpty(sRow.Cells("TMDB").Value.ToString) Then
                    enableTMDB = True
                End If
            Next
            cmnuMovieBrowseIMDB.Enabled = enableIMDB
            cmnuMovieBrowseTMDB.Enabled = enableTMDB
        End If
    End Sub

    ''' <summary>
    ''' Open IMDB-Page of selected movie(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender">Browse to... menuitem</param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' 
    ''' 
    ''' 2014/10/10 Cocotus - First implementation
    ''' </remarks>
    Private Sub cmnuMovieBrowseIMDB_Click(sender As Object, e As EventArgs) Handles cmnuMovieBrowseIMDB.Click
        Try
            If Me.dgvMovies.SelectedRows.Count > 0 Then
                Dim doOpen As Boolean = True
                If Me.dgvMovies.SelectedRows.Count > 10 Then
                    If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), Me.dgvMovies.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then doOpen = False
                End If

                If doOpen Then
                    Dim tmpstring As String = String.Empty
                    For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                        If Not String.IsNullOrEmpty(sRow.Cells("Imdb").Value.ToString) Then
                            tmpstring = sRow.Cells("Imdb").Value.ToString.Replace("tt", String.Empty)
                            If Not My.Resources.urlIMDB.EndsWith("/") Then
                                Functions.Launch(My.Resources.urlIMDB & "/title/tt" & tmpstring)
                            Else
                                Functions.Launch(My.Resources.urlIMDB & "title/tt" & tmpstring)
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

    End Sub
    ''' <summary>
    '''Open TMDB-Page of selected movie(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender">Browse to... menuitem</param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' 
    ''' 
    ''' 2014/10/10 Cocotus - First implementation
    ''' </remarks>
    Private Sub cmnuMovieBrowseTMDB_Click(sender As Object, e As EventArgs) Handles cmnuMovieBrowseTMDB.Click
        Try
            If Me.dgvMovies.SelectedRows.Count > 0 Then
                Dim doOpen As Boolean = True
                If Me.dgvMovies.SelectedRows.Count > 10 Then
                    If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), Me.dgvMovies.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then doOpen = False
                End If
                If doOpen Then
                    For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                        If Not String.IsNullOrEmpty(sRow.Cells("TMDB").Value.ToString) Then
                            If Not My.Resources.urlTheMovieDb.EndsWith("/") Then
                                Functions.Launch(My.Resources.urlTheMovieDb & "/movie/" & sRow.Cells("TMDB").Value.ToString)
                            Else
                                Functions.Launch(My.Resources.urlTheMovieDb & "movie/" & sRow.Cells("TMDB").Value.ToString)
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

    End Sub

    Private Sub OpenImageViewer(ByVal _Image As Image)
        Using dImgView As New dlgImgView
            dImgView.ShowDialog(_Image)
        End Using
    End Sub
    ''' <summary>
    ''' Draw genre text over the image when mouse hovers
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbGenre_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        If Master.eSettings.GeneralShowGenresText Then Return 'Because Image already has genre text displayed
        Me.GenreImage = DirectCast(sender, PictureBox).Image    'Store the image for later retrieval
        DirectCast(sender, PictureBox).Image = ImageUtils.AddGenreString(DirectCast(sender, PictureBox).Image, DirectCast(sender, PictureBox).Name.ToString)
    End Sub
    ''' <summary>
    ''' Reset genre image when mouse leaves to "clear" the text
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbGenre_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        If Master.eSettings.GeneralShowGenresText Then Return
        DirectCast(sender, PictureBox).Image = GenreImage
    End Sub

    Private Sub pbBanner_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbBanner.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If Me.pbBannerCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(Me.pbBannerCache.Image)
                    End Using
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then
                Select Case tcMain.SelectedIndex
                    Case 0 'Movies list
                        If Me.dgvMovies.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item("idMovie", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.LoadMovieFromDB(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifier As New Structures.ScrapeModifier

                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainBanner, True)
                        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifier, True) Then
                            If aContainer.MainBanners.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
                                    Master.DB.SaveMovieToDB(tmpDBElement, False, False, False, True)
                                    Me.RefreshRow_Movie(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 1 'MovieSets list
                        If Me.dgvMovieSets.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovieSets.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovieSets.Item("idSet", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.LoadMovieSetFromDB(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifier As New Structures.ScrapeModifier

                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainBanner, True)
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifier) Then
                            If aContainer.MainBanners.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.MovieSet) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
                                    Master.DB.SaveMovieSetToDB(tmpDBElement, False, False, True)
                                    Me.RefreshRow_MovieSet(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 2 'TV list
                        'TV Show list
                        If Me.dgvTVShows.Focused Then
                            If Me.dgvTVShows.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
                            Dim ID As Integer = Convert.ToInt32(Me.dgvTVShows.Item("idShow", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.LoadTVShowFromDB(ID, False, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifier As New Structures.ScrapeModifier

                            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainBanner, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifier, True) Then
                                If aContainer.MainBanners.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVShow) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
                                        Master.DB.SaveTVShowToDB(tmpDBElement, False, False, False, True, False)
                                        Me.RefreshRow_TVShow(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Season list
                        ElseIf Me.dgvTVSeasons.Focused Then
                            If Me.dgvTVSeasons.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim indX As Integer = Me.dgvTVSeasons.SelectedRows(0).Index
                            Dim ID As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item("idSeason", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.LoadTVSeasonFromDB(ID, True, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifier As New Structures.ScrapeModifier

                            If tmpDBElement.TVSeason.Season = 999 Then
                                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.AllSeasonsBanner, True)
                            Else
                                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonBanner, True)
                            End If
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifier, True) Then
                                If aContainer.SeasonBanners.Count > 0 OrElse (tmpDBElement.TVSeason.Season = 999 AndAlso aContainer.MainBanners.Count > 0) Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVSeason) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
                                        Master.DB.SaveTVSeasonToDB(tmpDBElement, False, True)
                                        Me.RefreshRow_TVSeason(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Episode list
                        ElseIf Me.dgvTVEpisodes.Focused Then
                            Return
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbCharacterArt_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbCharacterArt.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If Me.pbCharacterArtCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(Me.pbCharacterArtCache.Image)
                    End Using
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then
                Select Case tcMain.SelectedIndex
                    Case 0 'Movies list
                        Return
                    Case 1 'MovieSets list
                        Return
                    Case 2 'TV list
                        'TV Show list
                        If Me.dgvTVShows.Focused Then
                            If Me.dgvTVShows.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
                            Dim ID As Integer = Convert.ToInt32(Me.dgvTVShows.Item("idShow", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.LoadTVShowFromDB(ID, False, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifier As New Structures.ScrapeModifier

                            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainCharacterArt, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifier, True) Then
                                If aContainer.MainCharacterArts.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVShow) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.CharacterArt = dlgImgS.Result.ImagesContainer.CharacterArt
                                        Master.DB.SaveTVShowToDB(tmpDBElement, False, False, False, True, False)
                                        Me.RefreshRow_TVShow(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Season list
                        ElseIf Me.dgvTVSeasons.Focused Then
                            Return

                            'TV Episode list
                        ElseIf Me.dgvTVEpisodes.Focused Then
                            Return
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbClearArt_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbClearArt.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If Me.pbClearArtCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(Me.pbClearArtCache.Image)
                    End Using
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then
                Select Case tcMain.SelectedIndex
                    Case 0 'Movies list
                        If Me.dgvMovies.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item("idMovie", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.LoadMovieFromDB(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifier As New Structures.ScrapeModifier

                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearArt, True)
                        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifier, True) Then
                            If aContainer.MainClearArts.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.ClearArt = dlgImgS.Result.ImagesContainer.ClearArt
                                    Master.DB.SaveMovieToDB(tmpDBElement, False, False, False, True)
                                    Me.RefreshRow_Movie(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1102, "No ClearArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 1 'MovieSets list
                        If Me.dgvMovieSets.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovieSets.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovieSets.Item("idSet", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.LoadMovieSetFromDB(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifier As New Structures.ScrapeModifier

                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearArt, True)
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifier) Then
                            If aContainer.MainClearArts.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.MovieSet) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.ClearArt = dlgImgS.Result.ImagesContainer.ClearArt
                                    Master.DB.SaveMovieSetToDB(tmpDBElement, False, False, True)
                                    Me.RefreshRow_MovieSet(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1102, "No ClearArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 2 'TV list
                        'TV Show list
                        If Me.dgvTVShows.Focused Then
                            If Me.dgvTVShows.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
                            Dim ID As Integer = Convert.ToInt32(Me.dgvTVShows.Item("idShow", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.LoadTVShowFromDB(ID, False, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifier As New Structures.ScrapeModifier

                            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearArt, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifier, True) Then
                                If aContainer.MainClearArts.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVShow) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.ClearArt = dlgImgS.Result.ImagesContainer.ClearArt
                                        Master.DB.SaveTVShowToDB(tmpDBElement, False, False, False, True, False)
                                        Me.RefreshRow_TVShow(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(1102, "No ClearArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Season list
                        ElseIf Me.dgvTVSeasons.Focused Then
                            Return

                            'TV Episode list
                        ElseIf Me.dgvTVEpisodes.Focused Then
                            Return
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbClearLogo_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbClearLogo.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If Me.pbClearLogoCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(Me.pbClearLogoCache.Image)
                    End Using
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then
                Select Case tcMain.SelectedIndex
                    Case 0 'Movies list
                        If Me.dgvMovies.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item("idMovie", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.LoadMovieFromDB(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifier As New Structures.ScrapeModifier

                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearLogo, True)
                        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifier, True) Then
                            If aContainer.MainClearLogos.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.ClearLogo = dlgImgS.Result.ImagesContainer.ClearLogo
                                    Master.DB.SaveMovieToDB(tmpDBElement, False, False, False, True)
                                    Me.RefreshRow_Movie(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1103, "No ClearLogos found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 1 'MovieSets list
                        If Me.dgvMovieSets.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovieSets.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovieSets.Item("idSet", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.LoadMovieSetFromDB(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifier As New Structures.ScrapeModifier

                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearLogo, True)
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifier) Then
                            If aContainer.MainClearLogos.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.MovieSet) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.ClearLogo = dlgImgS.Result.ImagesContainer.ClearLogo
                                    Master.DB.SaveMovieSetToDB(tmpDBElement, False, False, True)
                                    Me.RefreshRow_MovieSet(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1103, "No ClearLogos found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 2 'TV list
                        'TV Show list
                        If Me.dgvTVShows.Focused Then
                            If Me.dgvTVShows.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
                            Dim ID As Integer = Convert.ToInt32(Me.dgvTVShows.Item("idShow", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.LoadTVShowFromDB(ID, False, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifier As New Structures.ScrapeModifier

                            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearLogo, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifier, True) Then
                                If aContainer.MainClearLogos.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVShow) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.ClearLogo = dlgImgS.Result.ImagesContainer.ClearLogo
                                        Master.DB.SaveTVShowToDB(tmpDBElement, False, False, False, True, False)
                                        Me.RefreshRow_TVShow(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(1103, "No ClearLogos found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Season list
                        ElseIf Me.dgvTVSeasons.Focused Then
                            Return

                            'TV Episode list
                        ElseIf Me.dgvTVEpisodes.Focused Then
                            Return
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbDiscArt_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbDiscArt.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If Me.pbDiscArtCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(Me.pbDiscArtCache.Image)
                    End Using
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then
                Select Case tcMain.SelectedIndex
                    Case 0 'Movies list
                        If Me.dgvMovies.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item("idMovie", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.LoadMovieFromDB(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifier As New Structures.ScrapeModifier

                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainDiscArt, True)
                        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifier, True) Then
                            If aContainer.MainDiscArts.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.DiscArt = dlgImgS.Result.ImagesContainer.DiscArt
                                    Master.DB.SaveMovieToDB(tmpDBElement, False, False, False, True)
                                    Me.RefreshRow_Movie(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1104, "No DiscArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 1 'MovieSets list
                        If Me.dgvMovieSets.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovieSets.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovieSets.Item("idSet", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.LoadMovieSetFromDB(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifier As New Structures.ScrapeModifier

                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainDiscArt, True)
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifier) Then
                            If aContainer.MainDiscArts.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.MovieSet) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.DiscArt = dlgImgS.Result.ImagesContainer.DiscArt
                                    Master.DB.SaveMovieSetToDB(tmpDBElement, False, False, True)
                                    Me.RefreshRow_MovieSet(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1104, "No DiscArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 2 'TV list
                        'TV Show list
                        If Me.dgvTVShows.Focused Then
                            Return

                            'TV Season list
                        ElseIf Me.dgvTVSeasons.Focused Then
                            Return

                            'TV Episode list
                        ElseIf Me.dgvTVEpisodes.Focused Then
                            Return
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.SetControlsEnabled(True)
        End Try
    End Sub
    ''' <summary>
    ''' Show the Fanart in the Image Viewer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbFanart_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbFanart.MouseDoubleClick, pbFanartSmall.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If Me.pbFanartCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(Me.pbFanartCache.Image)
                    End Using
                ElseIf Me.pbFanartSmallCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(Me.pbFanartSmallCache.Image)
                    End Using
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then
                Select Case tcMain.SelectedIndex
                    Case 0 'Movies list
                        If Me.dgvMovies.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item("idMovie", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.LoadMovieFromDB(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifier As New Structures.ScrapeModifier

                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainFanart, True)
                        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifier, True) Then
                            If aContainer.MainFanarts.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
                                    Master.DB.SaveMovieToDB(tmpDBElement, False, False, False, True)
                                    Me.RefreshRow_Movie(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 1 'MovieSets list
                        If Me.dgvMovieSets.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovieSets.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovieSets.Item("idSet", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.LoadMovieSetFromDB(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifier As New Structures.ScrapeModifier

                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainFanart, True)
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifier) Then
                            If aContainer.MainFanarts.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.MovieSet) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
                                    Master.DB.SaveMovieSetToDB(tmpDBElement, False, False, True)
                                    Me.RefreshRow_MovieSet(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 2 'TV list
                        'TV Show list
                        If Me.dgvTVShows.Focused Then
                            If Me.dgvTVShows.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
                            Dim ID As Integer = Convert.ToInt32(Me.dgvTVShows.Item("idShow", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.LoadTVShowFromDB(ID, False, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifier As New Structures.ScrapeModifier

                            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainFanart, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifier, True) Then
                                If aContainer.MainFanarts.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVShow) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
                                        Master.DB.SaveTVShowToDB(tmpDBElement, False, False, False, True, False)
                                        Me.RefreshRow_TVShow(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If

                            'TV Season list
                        ElseIf Me.dgvTVSeasons.Focused Then
                            If Me.dgvTVSeasons.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim indX As Integer = Me.dgvTVSeasons.SelectedRows(0).Index
                            Dim ID As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item("idSeason", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.LoadTVSeasonFromDB(ID, True, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifier As New Structures.ScrapeModifier

                            If tmpDBElement.TVSeason.Season = 999 Then
                                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.AllSeasonsFanart, True)
                            Else
                                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonFanart, True)
                            End If
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifier, True) Then
                                If aContainer.SeasonFanarts.Count > 0 OrElse aContainer.MainFanarts.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVSeason) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
                                        Master.DB.SaveTVSeasonToDB(tmpDBElement, False, True)
                                        Me.RefreshRow_TVSeason(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Episode list
                        ElseIf Me.dgvTVEpisodes.Focused Then
                            If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim indX As Integer = Me.dgvTVEpisodes.SelectedRows(0).Index
                            Dim ID As Integer = Convert.ToInt32(Me.dgvTVEpisodes.Item("idEpisode", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.LoadTVEpisodeFromDB(ID, True)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifier As New Structures.ScrapeModifier

                            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.EpisodeFanart, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifier, True) Then
                                If aContainer.EpisodeFanarts.Count > 0 OrElse aContainer.MainFanarts.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVEpisode) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
                                        Master.DB.SaveTVEpisodeToDB(tmpDBElement, False, False, False, True, False)
                                        Me.RefreshRow_TVEpisode(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            Me.SetControlsEnabled(True)
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbLandscape_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbLandscape.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If Me.pbLandscapeCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(Me.pbLandscapeCache.Image)
                    End Using
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then
                Select Case tcMain.SelectedIndex
                    Case 0 'Movies list
                        If Me.dgvMovies.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item("idMovie", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.LoadMovieFromDB(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifier As New Structures.ScrapeModifier

                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainLandscape, True)
                        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifier, True) Then
                            If aContainer.MainLandscapes.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
                                    Master.DB.SaveMovieToDB(tmpDBElement, False, False, False, True)
                                    Me.RefreshRow_Movie(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1197, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 1 'MovieSets list
                        If Me.dgvMovieSets.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovieSets.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovieSets.Item("idSet", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.LoadMovieSetFromDB(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifier As New Structures.ScrapeModifier

                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainLandscape, True)
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifier) Then
                            If aContainer.MainLandscapes.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.MovieSet) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
                                    Master.DB.SaveMovieSetToDB(tmpDBElement, False, False, True)
                                    Me.RefreshRow_MovieSet(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(1197, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 2 'TV list
                        'TV Show list
                        If Me.dgvTVShows.Focused Then
                            If Me.dgvTVShows.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
                            Dim ID As Integer = Convert.ToInt32(Me.dgvTVShows.Item("idShow", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.LoadTVShowFromDB(ID, False, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifier As New Structures.ScrapeModifier

                            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainLandscape, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifier, True) Then
                                If aContainer.MainLandscapes.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVShow) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
                                        Master.DB.SaveTVShowToDB(tmpDBElement, False, False, False, True, False)
                                        Me.RefreshRow_TVShow(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(1197, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Season list
                        ElseIf Me.dgvTVSeasons.Focused Then
                            If Me.dgvTVSeasons.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim indX As Integer = Me.dgvTVSeasons.SelectedRows(0).Index
                            Dim ID As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item("idSeason", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.LoadTVSeasonFromDB(ID, True)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifier As New Structures.ScrapeModifier

                            If tmpDBElement.TVSeason.Season = 999 Then
                                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.AllSeasonsLandscape, True)
                            Else
                                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonLandscape, True)
                            End If
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifier, True) Then
                                If aContainer.SeasonLandscapes.Count > 0 OrElse (tmpDBElement.TVSeason.Season = 999 AndAlso aContainer.MainLandscapes.Count > 0) Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVSeason) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
                                        Master.DB.SaveTVSeasonToDB(tmpDBElement, False, True)
                                        Me.RefreshRow_TVSeason(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(1197, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Episode list
                        ElseIf Me.dgvTVEpisodes.Focused Then
                            Return
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub pbPoster_DoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbPoster.MouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Left OrElse Not Master.eSettings.GeneralDoubleClickScrape Then
                If Me.pbPosterCache.Image IsNot Nothing Then
                    Using dImgView As New dlgImgView
                        dImgView.ShowDialog(Me.pbPosterCache.Image)
                    End Using
                End If
            ElseIf e.Button = Windows.Forms.MouseButtons.Right AndAlso Master.eSettings.GeneralDoubleClickScrape Then
                Select Case tcMain.SelectedIndex
                    Case 0 'Movies list
                        If Me.dgvMovies.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovies.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovies.Item("idMovie", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.LoadMovieFromDB(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifier As New Structures.ScrapeModifier

                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainPoster, True)
                        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifier, True) Then
                            If aContainer.MainPosters.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
                                    Master.DB.SaveMovieToDB(tmpDBElement, False, False, False, True)
                                    Me.RefreshRow_Movie(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 1 'MovieSets list
                        If Me.dgvMovieSets.SelectedRows.Count > 1 Then Return
                        Me.SetControlsEnabled(False)

                        Dim indX As Integer = Me.dgvMovieSets.SelectedRows(0).Index
                        Dim ID As Integer = Convert.ToInt32(Me.dgvMovieSets.Item("idSet", indX).Value)
                        Dim tmpDBElement As Database.DBElement = Master.DB.LoadMovieSetFromDB(ID)

                        Dim aContainer As New MediaContainers.SearchResultsContainer
                        Dim ScrapeModifier As New Structures.ScrapeModifier

                        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainPoster, True)
                        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifier) Then
                            If aContainer.MainPosters.Count > 0 Then
                                Dim dlgImgS As New dlgImgSelect()
                                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.MovieSet) = DialogResult.OK Then
                                    tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
                                    Master.DB.SaveMovieSetToDB(tmpDBElement, False, False, True)
                                    Me.RefreshRow_MovieSet(ID)
                                End If
                            Else
                                MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                        Me.SetControlsEnabled(True)
                    Case 2 'TV list
                        'TV Show list
                        If Me.dgvTVShows.Focused Then
                            If Me.dgvTVShows.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim indX As Integer = Me.dgvTVShows.SelectedRows(0).Index
                            Dim ID As Integer = Convert.ToInt32(Me.dgvTVShows.Item("idShow", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.LoadTVShowFromDB(ID, False, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifier As New Structures.ScrapeModifier

                            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainPoster, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifier, True) Then
                                If aContainer.MainPosters.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVShow) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
                                        Master.DB.SaveTVShowToDB(tmpDBElement, False, False, False, True, False)
                                        Me.RefreshRow_TVShow(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Season list
                        ElseIf Me.dgvTVSeasons.Focused Then
                            If Me.dgvTVSeasons.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim indX As Integer = Me.dgvTVSeasons.SelectedRows(0).Index
                            Dim ID As Integer = Convert.ToInt32(Me.dgvTVSeasons.Item("idSeason", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.LoadTVSeasonFromDB(ID, True, False)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifier As New Structures.ScrapeModifier

                            If tmpDBElement.TVSeason.Season = 999 Then
                                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.AllSeasonsPoster, True)
                            Else
                                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonPoster, True)
                            End If
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifier, True) Then
                                If aContainer.SeasonPosters.Count > 0 OrElse (tmpDBElement.TVSeason.Season = 999 AndAlso aContainer.MainPosters.Count > 0) Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVSeason) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
                                        Master.DB.SaveTVSeasonToDB(tmpDBElement, False, True)
                                        Me.RefreshRow_TVSeason(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            Me.SetControlsEnabled(True)

                            'TV Episode list
                        ElseIf Me.dgvTVEpisodes.Focused Then
                            If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then Return
                            Me.SetControlsEnabled(False)

                            Dim indX As Integer = Me.dgvTVEpisodes.SelectedRows(0).Index
                            Dim ID As Integer = Convert.ToInt32(Me.dgvTVEpisodes.Item("idEpisode", indX).Value)
                            Dim tmpDBElement As Database.DBElement = Master.DB.LoadTVEpisodeFromDB(ID, True)

                            Dim aContainer As New MediaContainers.SearchResultsContainer
                            Dim ScrapeModifier As New Structures.ScrapeModifier

                            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.EpisodePoster, True)
                            If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifier, True) Then
                                If aContainer.EpisodePosters.Count > 0 Then
                                    Dim dlgImgS As New dlgImgSelect()
                                    If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVEpisode) = DialogResult.OK Then
                                        tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
                                        Master.DB.SaveTVEpisodeToDB(tmpDBElement, False, False, False, True, False)
                                        Me.RefreshRow_TVEpisode(ID)
                                    End If
                                Else
                                    MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                            Me.SetControlsEnabled(True)
                        End If
                End Select
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Me.SetControlsEnabled(True)
        End Try
    End Sub

    Private Sub rbFilterAnd_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilterAnd_Movies.Click
        If clbFilterGenres_Movies.CheckedItems.Count > 0 Then
            Me.txtFilterGenre_Movies.Text = String.Empty
            Me.FilterArray_Movies.Remove(Me.filGenre_Movies)

            Dim alGenres As New List(Of String)
            alGenres.AddRange(clbFilterGenres_Movies.CheckedItems.OfType(Of String).ToList)

            Me.txtFilterGenre_Movies.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")

            For i As Integer = 0 To alGenres.Count - 1
                If alGenres.Item(i) = Master.eLang.None Then
                    alGenres.Item(i) = "Genre LIKE ''"
                Else
                    alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
                End If
            Next

            Me.filGenre_Movies = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")

            Me.FilterArray_Movies.Add(Me.filGenre_Movies)
        End If

        If clbFilterCountries_Movies.CheckedItems.Count > 0 Then
            Me.txtFilterCountry_Movies.Text = String.Empty
            Me.FilterArray_Movies.Remove(Me.filCountry_Movies)

            Dim alCountries As New List(Of String)
            alCountries.AddRange(clbFilterCountries_Movies.CheckedItems.OfType(Of String).ToList)

            Me.txtFilterCountry_Movies.Text = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " AND ")

            For i As Integer = 0 To alCountries.Count - 1
                If alCountries.Item(i) = Master.eLang.None Then
                    alCountries.Item(i) = "Country LIKE ''"
                Else
                    alCountries.Item(i) = String.Format("Country LIKE '%{0}%'", alCountries.Item(i))
                End If
            Next

            Me.filCountry_Movies = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " AND ")

            Me.FilterArray_Movies.Add(Me.filCountry_Movies)
        End If

        If clbFilterDataFields_Movies.CheckedItems.Count > 0 Then
            Me.txtFilterDataField_Movies.Text = String.Empty
            Me.FilterArray_Movies.Remove(Me.filDataField_Movies)

            Dim alDataFields As New List(Of String)
            alDataFields.AddRange(clbFilterDataFields_Movies.CheckedItems.OfType(Of String).ToList)

            Me.txtFilterDataField_Movies.Text = Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " AND ")

            For i As Integer = 0 To alDataFields.Count - 1
                If Me.cbFilterDataField_Movies.SelectedIndex = 0 Then
                    alDataFields.Item(i) = String.Format("{0} LIKE ''", alDataFields.Item(i))
                Else
                    alDataFields.Item(i) = String.Format("{0} NOT LIKE ''", alDataFields.Item(i))
                End If
            Next

            Me.filDataField_Movies = Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " AND ")

            Me.FilterArray_Movies.Add(Me.filDataField_Movies)
        End If

        If (Not String.IsNullOrEmpty(Me.cbFilterYearFrom_Movies.Text) AndAlso Not Me.cbFilterYearFrom_Movies.Text = Master.eLang.All) OrElse _
            (Not String.IsNullOrEmpty(Me.cbFilterYearTo_Movies.Text) AndAlso Not Me.cbFilterYearTo_Movies.Text = Master.eLang.All) OrElse _
            Me.clbFilterGenres_Movies.CheckedItems.Count > 0 OrElse Me.clbFilterCountries_Movies.CheckedItems.Count > 0 OrElse _
            Me.clbFilterCountries_Movies.CheckedItems.Count > 0 OrElse Me.chkFilterMark_Movies.Checked OrElse _
            Me.chkFilterMarkCustom1_Movies.Checked OrElse Me.chkFilterMarkCustom2_Movies.Checked OrElse Me.chkFilterMarkCustom3_Movies.Checked OrElse _
            Me.chkFilterMarkCustom4_Movies.Checked OrElse Me.chkFilterNew_Movies.Checked OrElse Me.chkFilterLock_Movies.Checked OrElse _
            Not Me.clbFilterSources_Movies.CheckedItems.Count > 0 OrElse Me.chkFilterDuplicates_Movies.Checked OrElse _
            Me.chkFilterMissing_Movies.Checked OrElse Me.chkFilterTolerance_Movies.Checked OrElse Not Me.cbFilterVideoSource_Movies.Text = Master.eLang.All Then Me.RunFilter_Movies()
    End Sub

    Private Sub rbFilterAnd_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilterAnd_MovieSets.Click
        If Me.chkFilterEmpty_MovieSets.Checked OrElse Me.chkFilterMark_MovieSets.Checked OrElse Me.chkFilterNew_MovieSets.Checked OrElse Me.chkFilterLock_MovieSets.Checked OrElse _
            Me.chkFilterMissing_MovieSets.Checked OrElse Me.chkFilterMultiple_MovieSets.Checked OrElse Me.chkFilterOne_MovieSets.Checked Then Me.RunFilter_MovieSets()
    End Sub

    Private Sub rbFilterAnd_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilterAnd_Shows.Click
        If clbFilterGenres_Shows.CheckedItems.Count > 0 Then
            Me.txtFilterGenre_Shows.Text = String.Empty
            Me.FilterArray_Shows.Remove(Me.filGenre_Shows)

            Dim alGenres As New List(Of String)
            alGenres.AddRange(clbFilterGenres_Shows.CheckedItems.OfType(Of String).ToList)

            Me.txtFilterGenre_Shows.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")

            For i As Integer = 0 To alGenres.Count - 1
                If alGenres.Item(i) = Master.eLang.None Then
                    alGenres.Item(i) = "Genre LIKE ''"
                Else
                    alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
                End If
            Next

            Me.filGenre_Shows = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " AND ")

            Me.FilterArray_Shows.Add(Me.filGenre_Shows)
        End If

        'If clbFilterCountries_Movies.CheckedItems.Count > 0 Then
        '    Me.txtFilterCountry_Movies.Text = String.Empty
        '    Me.FilterArray_Movies.Remove(Me.filCountry_Movies)

        '    Dim alCountries As New List(Of String)
        '    alCountries.AddRange(clbFilterCountries_Movies.CheckedItems.OfType(Of String).ToList)

        '    Me.txtFilterCountry_Movies.Text = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " AND ")

        '    For i As Integer = 0 To alCountries.Count - 1
        '        If alCountries.Item(i) = Master.eLang.None Then
        '            alCountries.Item(i) = "Country LIKE ''"
        '        Else
        '            alCountries.Item(i) = String.Format("Country LIKE '%{0}%'", alCountries.Item(i))
        '        End If
        '    Next

        '    Me.filCountry_Movies = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " AND ")

        '    Me.FilterArray_Movies.Add(Me.filCountry_Movies)
        'End If

        If Me.clbFilterGenres_Shows.CheckedItems.Count > 0 OrElse Me.chkFilterMark_Shows.Checked OrElse Me.chkFilterNewEpisodes_Shows.Checked OrElse _
            Me.chkFilterNewShows_Shows.Checked OrElse Me.chkFilterLock_Shows.Checked OrElse Not Me.clbFilterSource_Shows.CheckedItems.Count > 0 OrElse _
            Me.chkFilterMissing_Shows.Checked Then Me.RunFilter_Shows()
    End Sub

    Private Sub rbFilterOr_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilterOr_Movies.Click
        If clbFilterGenres_Movies.CheckedItems.Count > 0 Then
            Me.txtFilterGenre_Movies.Text = String.Empty
            Me.FilterArray_Movies.Remove(Me.filGenre_Movies)

            Dim alGenres As New List(Of String)
            alGenres.AddRange(clbFilterGenres_Movies.CheckedItems.OfType(Of String).ToList)

            Me.txtFilterGenre_Movies.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")

            For i As Integer = 0 To alGenres.Count - 1
                If alGenres.Item(i) = Master.eLang.None Then
                    alGenres.Item(i) = "Genre LIKE ''"
                Else
                    alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
                End If
            Next

            Me.filGenre_Movies = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")

            Me.FilterArray_Movies.Add(Me.filGenre_Movies)
        End If

        If clbFilterCountries_Movies.CheckedItems.Count > 0 Then
            Me.txtFilterCountry_Movies.Text = String.Empty
            Me.FilterArray_Movies.Remove(Me.filCountry_Movies)

            Dim alCountries As New List(Of String)
            alCountries.AddRange(clbFilterCountries_Movies.CheckedItems.OfType(Of String).ToList)

            Me.txtFilterCountry_Movies.Text = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " OR ")

            For i As Integer = 0 To alCountries.Count - 1
                If alCountries.Item(i) = Master.eLang.None Then
                    alCountries.Item(i) = "Country LIKE ''"
                Else
                    alCountries.Item(i) = String.Format("Country LIKE '%{0}%'", alCountries.Item(i))
                End If
            Next

            Me.filCountry_Movies = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " OR ")

            Me.FilterArray_Movies.Add(Me.filCountry_Movies)
        End If

        If clbFilterDataFields_Movies.CheckedItems.Count > 0 Then
            Me.txtFilterDataField_Movies.Text = String.Empty
            Me.FilterArray_Movies.Remove(Me.filDataField_Movies)

            Dim alDataFields As New List(Of String)
            alDataFields.AddRange(clbFilterDataFields_Movies.CheckedItems.OfType(Of String).ToList)

            Me.txtFilterDataField_Movies.Text = Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " OR ")

            For i As Integer = 0 To alDataFields.Count - 1
                If Me.cbFilterDataField_Movies.SelectedIndex = 0 Then
                    alDataFields.Item(i) = String.Format("{0} LIKE ''", alDataFields.Item(i))
                Else
                    alDataFields.Item(i) = String.Format("{0} NOT LIKE ''", alDataFields.Item(i))
                End If
            Next

            Me.filDataField_Movies = Microsoft.VisualBasic.Strings.Join(alDataFields.ToArray, " OR ")

            Me.FilterArray_Movies.Add(Me.filDataField_Movies)
        End If

        If (Not String.IsNullOrEmpty(Me.cbFilterYearFrom_Movies.Text) AndAlso Not Me.cbFilterYearFrom_Movies.Text = Master.eLang.All) OrElse _
            (Not String.IsNullOrEmpty(Me.cbFilterYearTo_Movies.Text) AndAlso Not Me.cbFilterYearTo_Movies.Text = Master.eLang.All) OrElse _
            Me.clbFilterGenres_Movies.CheckedItems.Count > 0 OrElse Me.clbFilterCountries_Movies.CheckedItems.Count > 0 OrElse _
            Me.clbFilterCountries_Movies.CheckedItems.Count > 0 OrElse Me.chkFilterMark_Movies.Checked OrElse _
            Me.chkFilterMarkCustom1_Movies.Checked OrElse Me.chkFilterMarkCustom2_Movies.Checked OrElse Me.chkFilterMarkCustom3_Movies.Checked OrElse _
            Me.chkFilterMarkCustom4_Movies.Checked OrElse Me.chkFilterNew_Movies.Checked OrElse Me.chkFilterLock_Movies.Checked OrElse _
            Not Me.clbFilterSources_Movies.CheckedItems.Count > 0 OrElse Me.chkFilterDuplicates_Movies.Checked OrElse _
            Me.chkFilterMissing_Movies.Checked OrElse Me.chkFilterTolerance_Movies.Checked OrElse Not Me.cbFilterVideoSource_Movies.Text = Master.eLang.All Then Me.RunFilter_Movies()
    End Sub

    Private Sub rbFilterOr_MovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilterOr_MovieSets.Click
        If Me.chkFilterEmpty_MovieSets.Checked OrElse Me.chkFilterMark_MovieSets.Checked OrElse Me.chkFilterNew_MovieSets.Checked OrElse Me.chkFilterLock_MovieSets.Checked OrElse _
            Me.chkFilterMissing_MovieSets.Checked OrElse Me.chkFilterMultiple_MovieSets.Checked OrElse Me.chkFilterOne_MovieSets.Checked Then Me.RunFilter_MovieSets()
    End Sub

    Private Sub rbFilterOr_Shows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFilterOr_Shows.Click
        If clbFilterGenres_Shows.CheckedItems.Count > 0 Then
            Me.txtFilterGenre_Shows.Text = String.Empty
            Me.FilterArray_Shows.Remove(Me.filGenre_Shows)

            Dim alGenres As New List(Of String)
            alGenres.AddRange(clbFilterGenres_Shows.CheckedItems.OfType(Of String).ToList)

            Me.txtFilterGenre_Shows.Text = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")

            For i As Integer = 0 To alGenres.Count - 1
                If alGenres.Item(i) = Master.eLang.None Then
                    alGenres.Item(i) = "Genre LIKE ''"
                Else
                    alGenres.Item(i) = String.Format("Genre LIKE '%{0}%'", alGenres.Item(i))
                End If
            Next

            Me.filGenre_Shows = Microsoft.VisualBasic.Strings.Join(alGenres.ToArray, " OR ")

            Me.FilterArray_Shows.Add(Me.filGenre_Shows)
        End If

        'If clbFilterCountries_Movies.CheckedItems.Count > 0 Then
        '    Me.txtFilterCountry_Movies.Text = String.Empty
        '    Me.FilterArray_Movies.Remove(Me.filCountry_Movies)

        '    Dim alCountries As New List(Of String)
        '    alCountries.AddRange(clbFilterCountries_Movies.CheckedItems.OfType(Of String).ToList)

        '    Me.txtFilterCountry_Movies.Text = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " OR ")

        '    For i As Integer = 0 To alCountries.Count - 1
        '        If alCountries.Item(i) = Master.eLang.None Then
        '            alCountries.Item(i) = "Country LIKE ''"
        '        Else
        '            alCountries.Item(i) = String.Format("Country LIKE '%{0}%'", alCountries.Item(i))
        '        End If
        '    Next

        '    Me.filCountry_Movies = Microsoft.VisualBasic.Strings.Join(alCountries.ToArray, " OR ")

        '    Me.FilterArray_Movies.Add(Me.filCountry_Movies)
        'End If

        If Me.clbFilterGenres_Shows.CheckedItems.Count > 0 OrElse Me.chkFilterMark_Shows.Checked OrElse Me.chkFilterNewEpisodes_Shows.Checked OrElse _
            Me.chkFilterNewShows_Shows.Checked OrElse Me.chkFilterLock_Shows.Checked OrElse Not Me.clbFilterSource_Shows.CheckedItems.Count > 0 OrElse _
            Me.chkFilterMissing_Shows.Checked Then Me.RunFilter_Shows()
    End Sub

    Private Sub ReloadAll_Movie()
        If Me.dtMovies.Rows.Count > 0 Then
            Me.Cursor = Cursors.WaitCursor
            Me.SetControlsEnabled(False, True)
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.EnableFilters_Movies(False)
            Me.EnableFilters_MovieSets(False)
            Me.EnableFilters_Shows(False)

            Me.tspbLoading.Maximum = Me.dtMovies.Rows.Count + 1
            Me.tspbLoading.Value = 0
            Me.tslLoading.Text = String.Concat(Master.eLang.GetString(110, "Refreshing Media"), ":")
            Me.tspbLoading.Visible = True
            Me.tslLoading.Visible = True
            Application.DoEvents()
            Me.bwReload_Movies.WorkerReportsProgress = True
            Me.bwReload_Movies.WorkerSupportsCancellation = True
            Me.bwReload_Movies.RunWorkerAsync()
        Else
            Me.SetControlsEnabled(True)
        End If
    End Sub

    Private Sub ReloadAll_MovieSet()
        If Me.dtMovieSets.Rows.Count > 0 Then
            Me.Cursor = Cursors.WaitCursor
            Me.SetControlsEnabled(False, True)
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.EnableFilters_Movies(False)
            Me.EnableFilters_MovieSets(False)
            Me.EnableFilters_Shows(False)

            Me.tspbLoading.Maximum = Me.dtMovieSets.Rows.Count + 1
            Me.tspbLoading.Value = 0
            Me.tslLoading.Text = String.Concat(Master.eLang.GetString(110, "Refreshing Media"), ":")
            Me.tspbLoading.Visible = True
            Me.tslLoading.Visible = True
            Application.DoEvents()
            Me.bwReload_MovieSets.WorkerReportsProgress = True
            Me.bwReload_MovieSets.WorkerSupportsCancellation = True
            Me.bwReload_MovieSets.RunWorkerAsync()
        Else
            Me.SetControlsEnabled(True)
        End If
    End Sub

    Private Sub ReloadAll_TVShow()
        If Me.dtTVShows.Rows.Count > 0 Then
            Me.Cursor = Cursors.WaitCursor
            Me.SetControlsEnabled(False, True)
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.EnableFilters_Movies(False)
            Me.EnableFilters_MovieSets(False)
            Me.EnableFilters_Shows(False)

            Me.tspbLoading.Maximum = Me.dtTVShows.Rows.Count + 1
            Me.tspbLoading.Value = 0
            Me.tslLoading.Text = String.Concat(Master.eLang.GetString(110, "Refreshing Media"), ":")
            Me.tspbLoading.Visible = True
            Me.tslLoading.Visible = True
            Application.DoEvents()
            Me.bwReload_TVShows.WorkerReportsProgress = True
            Me.bwReload_TVShows.WorkerSupportsCancellation = True
            Me.bwReload_TVShows.RunWorkerAsync()
        Else
            Me.SetControlsEnabled(True)
        End If
    End Sub

    Private Sub RewriteAll_Movie()
        If Me.dtMovies.Rows.Count > 0 Then
            Me.SetControlsEnabled(False)
            Me.tspbLoading.Style = ProgressBarStyle.Continuous
            Me.EnableFilters_Movies(False)
            Me.EnableFilters_MovieSets(False)
            Me.EnableFilters_Shows(False)

            Me.btnCancel.Text = Master.eLang.GetString(1299, "Cancel Rewriting")
            Me.lblCanceling.Text = Master.eLang.GetString(1300, "Canceling Rewriting...")
            Me.btnCancel.Visible = True
            Me.lblCanceling.Visible = False
            Me.prbCanceling.Visible = False
            Me.pnlCancel.Visible = True

            Me.tspbLoading.Maximum = Me.dtMovies.Rows.Count + 1
            Me.tspbLoading.Value = 0
            Me.tslLoading.Text = Master.eLang.GetString(1297, "Rewriting Media:")
            Me.tspbLoading.Visible = True
            Me.tslLoading.Visible = True
            Application.DoEvents()
            Me.bwRewrite_Movies.WorkerReportsProgress = True
            Me.bwRewrite_Movies.WorkerSupportsCancellation = True
            Me.bwRewrite_Movies.RunWorkerAsync()
        Else
            Me.SetControlsEnabled(True)
        End If
    End Sub

    Private Sub mnuMainToolsReloadMovies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsReloadMovies.Click, cmnuTrayToolsReloadMovies.Click
        ReloadAll_Movie()
    End Sub

    Private Sub mnuMainToolsReloadMovieSets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsReloadMovieSets.Click
        ReloadAll_MovieSet()
    End Sub

    Private Sub mnuMainToolsReloadTVShows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsReloadTVShows.Click
        ReloadAll_TVShow()
    End Sub

    Private Sub mnuMainToolsRewriteMovieContent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsRewriteMovieContent.Click
        RewriteAll_Movie()
    End Sub
    ''' <summary>
    ''' Refresh a single Movie row with informations from DB
    ''' </summary>
    ''' <param name="MovieID"></param>
    ''' <remarks></remarks>
    Private Sub RefreshRow_Movie(ByVal MovieID As Long)
        Dim myDelegate As New MydtListUpdate(AddressOf dtListUpdate)
        Dim newRow As DataRow = Nothing
        Dim newTable As New DataTable

        Master.DB.FillDataTable(newTable, String.Format("SELECT * FROM movielist WHERE idMovie={0}", MovieID))
        If newTable.Rows.Count > 0 Then
            newRow = newTable.Rows.Item(0)
        End If

        Dim dRow = From drvRow In dtMovies.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idMovie")) = MovieID Select drvRow

        If dRow(0) IsNot Nothing AndAlso newRow IsNot Nothing Then
            If Me.InvokeRequired Then
                Me.Invoke(myDelegate, New Object() {dRow(0), newRow})
            Else
                DirectCast(dRow(0), DataRow).ItemArray = newRow.ItemArray
            End If
        End If

        If Me.dgvMovies.SelectedRows.Count > 0 AndAlso CInt(Me.dgvMovies.SelectedRows(0).Cells("idMovie").Value) = MovieID Then
            Me.SelectRow_Movie(Me.dgvMovies.SelectedRows(0).Index)
        End If
    End Sub
    ''' <summary>
    ''' Refresh a single MovieSet row with informations from DB
    ''' </summary>
    ''' <param name="MovieSetID"></param>
    ''' <remarks></remarks>
    Private Sub RefreshRow_MovieSet(ByVal MovieSetID As Long)
        Dim myDelegate As New MydtListUpdate(AddressOf dtListUpdate)
        Dim newRow As DataRow = Nothing
        Dim newTable As New DataTable

        Master.DB.FillDataTable(newTable, String.Format("SELECT * FROM setslist WHERE idSet={0}", MovieSetID))
        If newTable.Rows.Count > 0 Then
            newRow = newTable.Rows.Item(0)
        End If

        Dim dRow = From drvRow In dtMovieSets.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idSet")) = MovieSetID Select drvRow

        If dRow(0) IsNot Nothing AndAlso newRow IsNot Nothing Then
            If Me.InvokeRequired Then
                Me.Invoke(myDelegate, New Object() {dRow(0), newRow})
            Else
                DirectCast(dRow(0), DataRow).ItemArray = newRow.ItemArray
            End If
        End If

        If Me.dgvMovieSets.SelectedRows.Count > 0 AndAlso CInt(Me.dgvMovieSets.SelectedRows(0).Cells("idSet").Value) = MovieSetID Then
            Me.SelectRow_MovieSet(Me.dgvMovieSets.SelectedRows(0).Index)
        End If
    End Sub
    ''' <summary>
    ''' Refresh a single TVEpsiode row with informations from DB
    ''' </summary>
    ''' <param name="EpisodeID"></param>
    ''' <remarks></remarks>
    Private Sub RefreshRow_TVEpisode(ByVal EpisodeID As Long)
        Dim myDelegate As New MydtListUpdate(AddressOf dtListUpdate)
        Dim newRow As DataRow = Nothing
        Dim newTable As New DataTable

        Master.DB.FillDataTable(newTable, String.Format("SELECT * FROM episodelist WHERE idEpisode={0}", EpisodeID))
        If newTable.Rows.Count > 0 Then
            newRow = newTable.Rows.Item(0)
        End If

        Dim dRow = From drvRow In dtTVEpisodes.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idEpisode")) = EpisodeID Select drvRow

        If dRow(0) IsNot Nothing AndAlso newRow IsNot Nothing Then
            If Me.InvokeRequired Then
                Me.Invoke(myDelegate, New Object() {dRow(0), newRow})
            Else
                DirectCast(dRow(0), DataRow).ItemArray = newRow.ItemArray
            End If
        End If

        If Me.dgvTVEpisodes.SelectedRows.Count > 0 AndAlso CInt(Me.dgvTVEpisodes.SelectedRows(0).Cells("idEpisode").Value) = EpisodeID AndAlso Me.currList = 2 Then
            Me.SelectRow_TVEpisode(Me.dgvTVEpisodes.SelectedRows(0).Index)
        End If
    End Sub
    ''' <summary>
    ''' Refresh a single TVSeason row with informations from DB
    ''' </summary>
    ''' <param name="SeasonID"></param>
    ''' <remarks></remarks>
    Private Sub RefreshRow_TVSeason(ByVal SeasonID As Long)
        Dim myDelegate As New MydtListUpdate(AddressOf dtListUpdate)
        Dim newRow As DataRow = Nothing
        Dim newTable As New DataTable

        Master.DB.FillDataTable(newTable, String.Format("SELECT * FROM seasonslist WHERE idSeason={0}", SeasonID))
        If newTable.Rows.Count > 0 Then
            newRow = newTable.Rows.Item(0)
        End If

        Dim dRow = From drvRow In dtTVSeasons.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idSeason")) = SeasonID Select drvRow

        If dRow(0) IsNot Nothing AndAlso newRow IsNot Nothing Then
            If Me.InvokeRequired Then
                Me.Invoke(myDelegate, New Object() {dRow(0), newRow})
            Else
                DirectCast(dRow(0), DataRow).ItemArray = newRow.ItemArray
            End If
        End If

        If Me.dgvTVSeasons.SelectedRows.Count > 0 AndAlso CInt(Me.dgvTVSeasons.SelectedRows(0).Cells("idSeason").Value) = SeasonID AndAlso Me.currList = 1 Then
            Me.SelectRow_TVSeason(Me.dgvTVSeasons.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub RefreshRow_TVSeason(ByVal ShowID As Long, ByVal iSeason As Integer)
        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLNewcommand.CommandText = String.Concat("SELECT idSeason FROM seasons WHERE idShow = ", ShowID, " AND Season = ", iSeason, ";")
            Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                SQLreader.Read()
                If SQLreader.HasRows Then
                    RefreshRow_TVSeason(Convert.ToInt32(SQLreader("idSeason")))
                End If
            End Using
        End Using
    End Sub
    ''' <summary>
    ''' Refresh a single TVShow row with informations from DB
    ''' </summary>
    ''' <param name="ShowID"></param>
    ''' <remarks></remarks>
    Private Sub RefreshRow_TVShow(ByVal ShowID As Long, Optional ByVal Force As Boolean = False)
        Dim myDelegate As New MydtListUpdate(AddressOf dtListUpdate)
        Dim newRow As DataRow = Nothing
        Dim newTable As New DataTable

        Master.DB.FillDataTable(newTable, String.Format("SELECT * FROM tvshowlist WHERE idShow={0}", ShowID))
        If newTable.Rows.Count > 0 Then
            newRow = newTable.Rows.Item(0)
        End If

        Dim dRow = From drvRow In dtTVShows.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idShow")) = ShowID Select drvRow

        If dRow(0) IsNot Nothing AndAlso newRow IsNot Nothing Then
            If Me.InvokeRequired Then
                Me.Invoke(myDelegate, New Object() {dRow(0), newRow})
            Else
                DirectCast(dRow(0), DataRow).ItemArray = newRow.ItemArray
            End If
        End If

        If Me.dgvTVShows.SelectedRows.Count > 0 AndAlso CInt(Me.dgvTVShows.SelectedRows(0).Cells("idShow").Value) = ShowID AndAlso (Me.currList = 0 OrElse Force) Then
            Me.SelectRow_TVShow(Me.dgvTVShows.SelectedRows(0).Index)
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <param name="BatchMode"></param>
    ''' <returns>Reload movie from disc</returns>
    ''' <remarks></remarks>
    Private Function Reload_Movie(ByVal ID As Long, ByVal BatchMode As Boolean, ByVal showMessage As Boolean) As Boolean
        Dim DBMovie As New Database.DBElement

        DBMovie = Master.DB.LoadMovieFromDB(ID, False)

        If DBMovie.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBMovie, Not showMessage) Then
            fScanner.LoadMovie(DBMovie, False, BatchMode)
            If Not BatchMode Then RefreshRow_Movie(DBMovie.ID)
        Else
            If showMessage AndAlso MessageBox.Show(String.Concat(Master.eLang.GetString(587, "This file is no longer available"), ".", Environment.NewLine, _
                                                         Master.eLang.GetString(703, "Whould you like to remove it from the library?")), _
                                                     Master.eLang.GetString(654, "Remove movie from library"), _
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Master.DB.DeleteMovieFromDB(ID, BatchMode)
                Return True
            Else
                Return False
            End If
        End If

        If Not BatchMode Then
            Me.DoTitleCheck()
        End If

        Return False
    End Function
    ''' <summary>
    ''' Reload movieset from disc
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <param name="BatchMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Reload_MovieSet(ByVal ID As Long, Optional ByVal BatchMode As Boolean = False) As Boolean
        Dim DBMovieSet As New Database.DBElement

        DBMovieSet = Master.DB.LoadMovieSetFromDB(ID, False)

        fScanner.LoadMovieSet(DBMovieSet, False, BatchMode)
        If Not BatchMode Then RefreshRow_MovieSet(DBMovieSet.ID)

        Return False
    End Function
    ''' <summary>
    ''' Reload tv episode from disc
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <param name="BatchMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Reload_TVEpisode(ByVal ID As Long, ByVal BatchMode As Boolean, ByVal showMessage As Boolean) As Boolean
        Dim DBTVEpisode As New Database.DBElement
        Dim epCount As Integer = 0

        DBTVEpisode = Master.DB.LoadTVEpisodeFromDB(ID, True, False)

        If DBTVEpisode.FilenameID = -1 Then Return False 'skipping missing episodes

        If DBTVEpisode.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(DBTVEpisode, showMessage) Then
            fScanner.LoadTVEpisode(DBTVEpisode, False, BatchMode, False)
            If Not BatchMode Then RefreshRow_TVEpisode(DBTVEpisode.ID)
        Else
            If showMessage AndAlso MessageBox.Show(String.Concat(Master.eLang.GetString(587, "This file is no longer available"), ".", Environment.NewLine, _
                                                         Master.eLang.GetString(703, "Whould you like to remove it from the library?")), _
                                                     Master.eLang.GetString(738, "Remove episode from library"), _
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Master.DB.DeleteTVEpFromDBByPath(DBTVEpisode.Filename, False, BatchMode)
                Return True
            Else
                Return False
            End If
        End If

        Return False
    End Function
    ''' <summary>
    ''' Reload tv season from disc
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <param name="BatchMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Reload_TVSeason(ByVal ID As Long, ByVal BatchMode As Boolean, ByVal showMessage As Boolean, reloadFull As Boolean) As Boolean
        Dim DBTVSeason As New Database.DBElement

        DBTVSeason = Master.DB.LoadTVSeasonFromDB(ID, True, False)

        If DBTVSeason.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBTVSeason, showMessage) Then
            fScanner.GetTVSeasonFolderContents(DBTVSeason)
            Master.DB.SaveTVSeasonToDB(DBTVSeason, BatchMode, False)
            If Not BatchMode Then RefreshRow_TVSeason(DBTVSeason.ID)
        Else
            Return False
        End If

        Return False
    End Function
    ''' <summary>
    ''' Reload tv show from disc
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <param name="BatchMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Reload_TVShow(ByVal ID As Long, ByVal BatchMode As Boolean, ByVal showMessage As Boolean, ByVal reloadFull As Boolean) As Boolean
        Dim DBTVShow As New Database.DBElement

        DBTVShow = Master.DB.LoadTVShowFromDB(ID, reloadFull, reloadFull, False)

        If DBTVShow.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBTVShow, showMessage) Then
            fScanner.LoadTVShow(DBTVShow, False, BatchMode, False)
            If Not BatchMode Then RefreshRow_TVShow(DBTVShow.ID)
        Else
            If showMessage AndAlso MessageBox.Show(String.Concat(Master.eLang.GetString(719, "This path is no longer available"), ".", Environment.NewLine, _
                                                         Master.eLang.GetString(703, "Whould you like to remove it from the library?")), _
                                                     Master.eLang.GetString(776, "Remove tv show from library"), _
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Master.DB.DeleteTVShowFromDB(ID, BatchMode)
                Return True
            Else
                Return False
            End If
        End If

        Return False
    End Function
    ''' <summary>
    ''' Load existing movie content and save it again with all selected filenames
    ''' </summary>
    ''' <param name="ID">Movie ID</param>
    ''' <returns>reload list from database?</returns>
    ''' <remarks></remarks>
    Private Function RewriteMovie(ByVal ID As Long, ByVal BatchMode As Boolean) As Boolean
        Dim tmpMovieDB As New Database.DBElement

        tmpMovieDB = Master.DB.LoadMovieFromDB(ID)

        If tmpMovieDB.IsOnline Then
            Master.DB.SaveMovieToDB(tmpMovieDB, False, BatchMode, True, True)
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub cmnuMovieRemoveFromDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieRemoveFromDB.Click
        Me.ClearInfo()

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                Master.DB.DeleteMovieFromDB(Convert.ToInt64(sRow.Cells("idMovie").Value), True)
            Next
            SQLtransaction.Commit()
        End Using

        Me.FillList(True, True, False)
    End Sub

    Private Sub ResizeMoviesList()
        If Not Master.isWindows Then
            If Me.dgvMovies.ColumnCount > 0 Then
                Me.dgvMovies.Columns(3).Width = Me.dgvMovies.Width - _
                If(CheckColumnHide_Movies("Year"), dgvMovies.Columns(17).Width, 0) - _
                If(CheckColumnHide_Movies("BannerPath"), 20, 0) - _
                If(CheckColumnHide_Movies("ClearArtPath"), 20, 0) - _
                If(CheckColumnHide_Movies("ClearLogoPath"), 20, 0) - _
                If(CheckColumnHide_Movies("DiscArtPath"), 20, 0) - _
                If(CheckColumnHide_Movies("EFanartsPath"), 20, 0) - _
                If(CheckColumnHide_Movies("EThumbsPath"), 20, 0) - _
                If(CheckColumnHide_Movies("FanartPath"), 20, 0) - _
                If(CheckColumnHide_Movies("LandscapePath"), 20, 0) - _
                If(CheckColumnHide_Movies("NfoPath"), 20, 0) - _
                If(CheckColumnHide_Movies("PosterPath"), 20, 0) - _
                If(CheckColumnHide_Movies("HasSet"), 20, 0) - _
                If(CheckColumnHide_Movies("HasSub"), 20, 0) - _
                If(CheckColumnHide_Movies("ThemePath"), 20, 0) - _
                If(CheckColumnHide_Movies("TrailerPath"), 20, 0) - _
                If(CheckColumnHide_Movies("Playcount"), 20, 0) - _
                If(Me.dgvMovies.DisplayRectangle.Height > Me.dgvMovies.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If
        End If
    End Sub

    Private Sub ResizeMovieSetsList()
        If Not Master.isWindows Then
            If Me.dgvMovieSets.ColumnCount > 0 Then
                Me.dgvMovieSets.Columns(0).Width = Me.dgvMovieSets.Width - _
                If(CheckColumnHide_MovieSets("NfoPath"), 20, 0) - _
                If(CheckColumnHide_MovieSets("PosterPath"), 20, 0) - _
                If(CheckColumnHide_MovieSets("FanartPath"), 20, 0) - _
                If(CheckColumnHide_MovieSets("BannerPath"), 20, 0) - _
                If(CheckColumnHide_MovieSets("LandscapePath"), 20, 0) - _
                If(CheckColumnHide_MovieSets("DiscArtPath"), 20, 0) - _
                If(CheckColumnHide_MovieSets("ClearLogoPath"), 20, 0) - _
                If(CheckColumnHide_MovieSets("ClearArtPath"), 20, 0) - _
                If(Me.dgvMovieSets.DisplayRectangle.Height > Me.dgvMovieSets.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If
        End If
    End Sub

    Private Sub ResizeTVLists(ByVal iType As Integer)
        '0 = all.... needed???

        If Not Master.isWindows Then
            If (iType = 0 OrElse iType = 1) AndAlso Me.dgvTVShows.ColumnCount > 0 Then
                Me.dgvTVShows.Columns(1).Width = Me.dgvTVShows.Width - _
                If(CheckColumnHide_TVShows("BannerPath"), 20, 0) - _
                If(CheckColumnHide_TVShows("CharacterArtPath"), 20, 0) - _
                If(CheckColumnHide_TVShows("ClearArtPath"), 20, 0) - _
                If(CheckColumnHide_TVShows("ClearLogoPath"), 20, 0) - _
                If(CheckColumnHide_TVShows("EFanartsPath"), 20, 0) - _
                If(CheckColumnHide_TVShows("FanartPath"), 20, 0) - _
                If(CheckColumnHide_TVShows("LandscapePath"), 20, 0) - _
                If(CheckColumnHide_TVShows("NfoPath"), 20, 0) - _
                If(CheckColumnHide_TVShows("PosterPath"), 20, 0) - _
                If(CheckColumnHide_TVShows("ThemePath"), 20, 0) - _
                If(CheckColumnHide_TVShows("Playcount"), 20, 0) - _
                If(Me.dgvTVShows.DisplayRectangle.Height > Me.dgvTVShows.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If

            If (iType = 0 OrElse iType = 2) AndAlso Me.dgvTVSeasons.ColumnCount > 0 Then
                Me.dgvTVSeasons.Columns(1).Width = Me.dgvTVSeasons.Width - _
                If(CheckColumnHide_TVSeasons("BannerPath"), 20, 0) - _
                If(CheckColumnHide_TVSeasons("FanartPath"), 20, 0) - _
                If(CheckColumnHide_TVSeasons("LandscapePath"), 20, 0) - _
                If(CheckColumnHide_TVSeasons("PosterPath"), 20, 0) - _
                If(CheckColumnHide_TVSeasons("Playcount"), 20, 0) - _
                If(Me.dgvTVSeasons.DisplayRectangle.Height > Me.dgvTVSeasons.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If

            If (iType = 0 OrElse iType = 3) AndAlso Me.dgvTVEpisodes.ColumnCount > 0 Then
                Me.dgvTVEpisodes.Columns(2).Width = Me.dgvTVEpisodes.Width - 40 - _
                If(CheckColumnHide_TVEpisodes("FanartPath"), 20, 0) - _
                If(CheckColumnHide_TVEpisodes("NfoPath"), 20, 0) - _
                If(CheckColumnHide_TVEpisodes("PosterPath"), 20, 0) - _
                If(CheckColumnHide_TVEpisodes("HasSub"), 20, 0) - _
                If(CheckColumnHide_TVEpisodes("Playcount"), 20, 0) - _
                If(Me.dgvTVEpisodes.DisplayRectangle.Height > Me.dgvTVEpisodes.ClientRectangle.Height, 0, SystemInformation.VerticalScrollBarWidth)
            End If

        End If
    End Sub

    Private Sub RunFilter_MovieCustom(ByVal CustomFilterString As String)
        Try
            If Me.Visible Then
                Dim table As New DataTable
                Dim ds As New DataSet
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLcommand.CommandText = CustomFilterString
                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                        ds.Tables.Add(table)
                        ds.EnforceConstraints = False
                        table.Load(SQLreader)
                    End Using
                End Using
                Dim filterstring As String = ""
                Dim idColumnname As String = ""
                If table.Columns.Contains("idMovie") Then
                    idColumnname = "idMovie"
                ElseIf table.Columns.Contains("idMedia") Then
                    idColumnname = "idMedia"
                End If
                If Not String.IsNullOrEmpty(idColumnname) Then
                    For Each resultrow As DataRow In table.Rows
                        If Not String.IsNullOrEmpty(resultrow.Item(idColumnname).ToString) Then
                            If String.IsNullOrEmpty(filterstring) Then
                                filterstring = filterstring & "idMovie = " & resultrow.Item(idColumnname).ToString
                            Else
                                filterstring = filterstring & " OR " & "idMovie = " & resultrow.Item(idColumnname).ToString
                            End If

                        End If
                    Next
                Else
                    logger.Warn("[RunFilter_MovieCustom] Query: " & CustomFilterString & " doesn't return idMovie/idMedia field!")
                End If

                Me.ClearInfo()
                Me.prevRow_Movie = -2
                Me.currRow_Movie = -1
                Me.dgvMovies.ClearSelection()
                Me.dgvMovies.CurrentCell = Nothing
                'in case there are no results for custom filter, don't display any movies by creating dummy filter
                If String.IsNullOrEmpty(filterstring) Then
                    filterstring = "Title LIKE '%Oh my nothing found, but Ember rocks anyway!%'"
                End If
                bsMovies.Filter = filterstring
                ModulesManager.Instance.RuntimeObjects.FilterMovies = filterstring
                Me.txtSearchMovies.Focus()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub RunFilter_Movies(Optional ByVal doFill As Boolean = False)
        If Me.Visible Then

            Me.ClearInfo()

            Me.prevRow_Movie = -2
            Me.currRow_Movie = -1
            Me.dgvMovies.ClearSelection()
            Me.dgvMovies.CurrentCell = Nothing

            If FilterArray_Movies.Count > 0 Then
                Dim FilterString As String = String.Empty

                If rbFilterAnd_Movies.Checked Then
                    FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray_Movies.ToArray, " AND ")
                Else
                    FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray_Movies.ToArray, " OR ")
                End If

                bsMovies.Filter = FilterString
                ModulesManager.Instance.RuntimeObjects.FilterMovies = FilterString
            Else
                bsMovies.RemoveFilter()
                ModulesManager.Instance.RuntimeObjects.FilterMovies = String.Empty
            End If

            If doFill Then
                Me.FillList(True, False, False)
                ModulesManager.Instance.RuntimeObjects.FilterMoviesSearch = Me.txtSearchMovies.Text
                ModulesManager.Instance.RuntimeObjects.FilterMoviesType = Me.cbSearchMovies.Text
            Else
                Me.txtSearchMovies.Focus()
            End If
        End If
    End Sub

    Private Sub RunFilter_MovieSets(Optional ByVal doFill As Boolean = False)
        If Me.Visible Then

            Me.ClearInfo()

            Me.prevRow_MovieSet = -2
            Me.currRow_MovieSet = -1
            Me.dgvMovieSets.ClearSelection()
            Me.dgvMovieSets.CurrentCell = Nothing

            If FilterArray_MovieSets.Count > 0 Then
                Dim FilterString As String = String.Empty

                If rbFilterAnd_MovieSets.Checked Then
                    FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray_MovieSets.ToArray, " AND ")
                Else
                    FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray_MovieSets.ToArray, " OR ")
                End If

                bsMovieSets.Filter = FilterString
            Else
                bsMovieSets.RemoveFilter()
            End If

            If doFill Then
                Me.FillList(False, True, False)
            Else
                Me.txtSearchMovieSets.Focus()
            End If
        End If
    End Sub

    Private Sub RunFilter_Shows(Optional ByVal doFill As Boolean = False)
        If Me.Visible Then

            Me.ClearInfo()

            Me.prevRow_TVShow = -2
            Me.currRow_TVShow = -1
            Me.currList = 0
            Me.dgvTVShows.ClearSelection()
            Me.dgvTVShows.CurrentCell = Nothing

            Me.dgvTVSeasons.ClearSelection()
            Me.dgvTVSeasons.CurrentCell = Nothing
            Me.bsTVSeasons.DataSource = Nothing
            Me.dgvTVEpisodes.DataSource = Nothing

            Me.dgvTVEpisodes.ClearSelection()
            Me.dgvTVEpisodes.CurrentCell = Nothing
            Me.bsTVEpisodes.DataSource = Nothing
            Me.dgvTVEpisodes.DataSource = Nothing

            If FilterArray_Shows.Count > 0 Then
                Dim FilterString As String = String.Empty

                If rbFilterAnd_Shows.Checked Then
                    FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray_Shows.ToArray, " AND ")
                Else
                    FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray_Shows.ToArray, " OR ")
                End If

                bsTVShows.Filter = FilterString
                ModulesManager.Instance.RuntimeObjects.FilterShows = FilterString
            Else
                bsTVShows.RemoveFilter()
                ModulesManager.Instance.RuntimeObjects.FilterShows = String.Empty
            End If

            If doFill Then
                Me.FillList(False, False, True)
                ModulesManager.Instance.RuntimeObjects.FilterShowsSearch = Me.txtSearchShows.Text
                ModulesManager.Instance.RuntimeObjects.FilterShowsType = Me.cbSearchShows.Text
            Else
                Me.txtSearchShows.Focus()
            End If
        End If
    End Sub

    Private Sub RestoreFilter_Movies()
        With Master.eSettings
            If .GeneralMainFilterSortColumn_Movies = 0 AndAlso .GeneralMainFilterSortOrder_Movies = 0 Then
                .GeneralMainFilterSortColumn_Movies = 3         'ListTitle in movielist
                .GeneralMainFilterSortOrder_Movies = 0          'ASC
            End If

            If Me.dgvMovies.DataSource IsNot Nothing Then
                Me.dgvMovies.Sort(Me.dgvMovies.Columns(.GeneralMainFilterSortColumn_Movies), CType(.GeneralMainFilterSortOrder_Movies, ComponentModel.ListSortDirection))
            End If
        End With
    End Sub

    Private Sub RestoreFilter_MovieSets()
        With Master.eSettings
            If .GeneralMainFilterSortColumn_MovieSets = 0 AndAlso .GeneralMainFilterSortOrder_Movies = 0 Then
                .GeneralMainFilterSortColumn_MovieSets = 3         'ListTitle in movielist
                .GeneralMainFilterSortOrder_MovieSets = 0          'ASC
            End If

            If Me.dgvMovieSets.DataSource IsNot Nothing Then
                Me.dgvMovieSets.Sort(Me.dgvMovieSets.Columns(.GeneralMainFilterSortColumn_MovieSets), CType(.GeneralMainFilterSortOrder_MovieSets, ComponentModel.ListSortDirection))
            End If
        End With
    End Sub

    Private Sub RestoreFilter_Shows()
        With Master.eSettings
            If .GeneralMainFilterSortColumn_Shows = 0 AndAlso .GeneralMainFilterSortOrder_Shows = 0 Then
                .GeneralMainFilterSortColumn_Shows = 1         'ListTitle in tvshowlist
                .GeneralMainFilterSortOrder_Shows = 0          'ASC
            End If

            If Me.dgvTVShows.DataSource IsNot Nothing Then
                Me.dgvTVShows.Sort(Me.dgvTVShows.Columns(.GeneralMainFilterSortColumn_Shows), CType(.GeneralMainFilterSortOrder_Shows, ComponentModel.ListSortDirection))
            End If
        End With
    End Sub

    Private Sub SaveFilter_Movies()
        Dim Order As Integer
        If Me.dgvMovies.SortOrder = SortOrder.None Then Order = 0 'ComponentModel.ListSortDirection has only ASC and DESC. So set [None] to ASC
        If Me.dgvMovies.SortOrder = SortOrder.Ascending Then Order = 0
        If Me.dgvMovies.SortOrder = SortOrder.Descending Then Order = 1

        Master.eSettings.GeneralMainFilterSortColumn_Movies = Me.dgvMovies.SortedColumn.Index
        Master.eSettings.GeneralMainFilterSortOrder_Movies = Order
    End Sub

    Private Sub SaveFilter_MovieSets()
        Dim Order As Integer
        If Me.dgvMovieSets.SortOrder = SortOrder.None Then Order = 0 'ComponentModel.ListSortDirection has only ASC and DESC. So set [None] to ASC
        If Me.dgvMovieSets.SortOrder = SortOrder.Ascending Then Order = 0
        If Me.dgvMovieSets.SortOrder = SortOrder.Descending Then Order = 1

        Master.eSettings.GeneralMainFilterSortColumn_MovieSets = Me.dgvMovieSets.SortedColumn.Index
        Master.eSettings.GeneralMainFilterSortOrder_MovieSets = Order
    End Sub

    Private Sub SaveFilter_Shows()
        Dim Order As Integer
        If Me.dgvTVShows.SortOrder = SortOrder.None Then Order = 0 'ComponentModel.ListSortDirection has only ASC and DESC. So set [None] to ASC
        If Me.dgvTVShows.SortOrder = SortOrder.Ascending Then Order = 0
        If Me.dgvTVShows.SortOrder = SortOrder.Descending Then Order = 1

        Master.eSettings.GeneralMainFilterSortColumn_Shows = Me.dgvTVShows.SortedColumn.Index
        Master.eSettings.GeneralMainFilterSortOrder_Shows = Order
    End Sub

    Private Sub ScannerUpdated(ByVal iType As Integer, ByVal sText As String)
        Select Case iType
            Case 1
                Me.SetStatus(String.Concat(Master.eLang.GetString(814, "Added Episode:"), " " & sText))
            Case 2
                Me.SetStatus(Master.eLang.GetString(116, "Performing Preliminary Tasks (Gathering Data)..."))
            Case 3
                Me.SetStatus(Master.eLang.GetString(644, "Cleaning Database..."))
            Case Else
                Me.SetStatus(String.Concat(Master.eLang.GetString(815, "Added Movie:"), " " & sText))
        End Select
    End Sub

    Private Sub ScanningCompleted()
        If Not Master.isCL Then
            Me.SetStatus(String.Empty)
            Me.FillList(True, True, True)
            Me.tspbLoading.Visible = False
            Me.tslLoading.Visible = False
            LoadingDone = True
        Else
            Me.FillList(True, True, True)
            LoadingDone = True
        End If
    End Sub

    Private Sub scMain_SplitterMoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles scMain.SplitterMoved
        Try
            If Me.Created Then
                Me.SuspendLayout()
                Me.MoveMPAA()
                Me.MoveGenres()

                ImageUtils.ResizePB(Me.pbFanart, Me.pbFanartCache, Me.scMain.Panel2.Height - 90, Me.scMain.Panel2.Width)
                Me.pbFanart.Left = Convert.ToInt32((Me.scMain.Panel2.Width - Me.pbFanart.Width) / 2)
                Me.pnlNoInfo.Location = New Point(Convert.ToInt32((Me.scMain.Panel2.Width - Me.pnlNoInfo.Width) / 2), Convert.ToInt32((Me.scMain.Panel2.Height - Me.pnlNoInfo.Height) / 2))
                Me.pnlCancel.Location = New Point(Convert.ToInt32((Me.scMain.Panel2.Width - Me.pnlNoInfo.Width) / 2), 124)
                Me.pnlFilterCountries_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterSpecific_Movies.Left + Me.tblFilterSpecific_Movies.Left + Me.tblFilterSpecificData_Movies.Left + Me.txtFilterCountry_Movies.Left + 1, _
                                                                  (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterSpecific_Movies.Top + Me.tblFilterSpecific_Movies.Top + Me.tblFilterSpecificData_Movies.Top + Me.txtFilterCountry_Movies.Top) - Me.pnlFilterCountries_Movies.Height)
                Me.pnlFilterGenres_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterSpecific_Movies.Left + Me.tblFilterSpecific_Movies.Left + Me.tblFilterSpecificData_Movies.Left + Me.txtFilterGenre_Movies.Left + 1, _
                                                               (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterSpecific_Movies.Top + Me.tblFilterSpecific_Movies.Top + Me.tblFilterSpecificData_Movies.Top + Me.txtFilterGenre_Movies.Top) - Me.pnlFilterGenres_Movies.Height)
                Me.pnlFilterGenres_Shows.Location = New Point(Me.pnlFilter_Shows.Left + Me.tblFilter_Shows.Left + Me.gbFilterSpecific_Shows.Left + Me.tblFilterSpecific_Shows.Left + Me.tblFilterSpecificData_Shows.Left + Me.txtFilterGenre_Shows.Left + 1, _
                                                              (Me.pnlFilter_Shows.Top + Me.tblFilter_Shows.Top + Me.gbFilterSpecific_Shows.Top + Me.tblFilterSpecific_Shows.Top + Me.tblFilterSpecificData_Shows.Top + Me.txtFilterGenre_Shows.Top) - Me.pnlFilterGenres_Shows.Height)
                Me.pnlFilterDataFields_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterSpecific_Movies.Left + Me.tblFilterSpecific_Movies.Left + Me.tblFilterSpecificData_Movies.Left + Me.gbFilterDataField_Movies.Left + Me.tblFilterDataField_Movies.Left + Me.txtFilterDataField_Movies.Left + 1, _
                                                                   (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterSpecific_Movies.Top + Me.tblFilterSpecific_Movies.Top + Me.tblFilterSpecificData_Movies.Top + Me.gbFilterDataField_Movies.Top + Me.tblFilterDataField_Movies.Top + Me.txtFilterDataField_Movies.Top) - Me.pnlFilterDataFields_Movies.Height)
                Me.pnlFilterMissingItems_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterGeneral_Movies.Left + Me.tblFilterGeneral_Movies.Left + Me.btnFilterMissing_Movies.Left + 1, _
                                                                     (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterGeneral_Movies.Top + Me.tblFilterGeneral_Movies.Top + Me.btnFilterMissing_Movies.Top) - Me.pnlFilterMissingItems_Movies.Height)
                Me.pnlFilterMissingItems_MovieSets.Location = New Point(Me.pnlFilter_MovieSets.Left + Me.tblFilter_MovieSets.Left + Me.gbFilterGeneral_MovieSets.Left + Me.tblFilterGeneral_MovieSets.Left + Me.btnFilterMissing_MovieSets.Left + 1, _
                                                                     (Me.pnlFilter_MovieSets.Top + Me.tblFilter_MovieSets.Top + Me.gbFilterGeneral_MovieSets.Top + Me.tblFilterGeneral_MovieSets.Top + Me.btnFilterMissing_MovieSets.Top) - Me.pnlFilterMissingItems_MovieSets.Height)
                Me.pnlFilterMissingItems_Shows.Location = New Point(Me.pnlFilter_Shows.Left + Me.tblFilter_Shows.Left + Me.gbFilterGeneral_Shows.Left + Me.tblFilterGeneral_Shows.Left + Me.btnFilterMissing_Shows.Left + 1, _
                                                                     (Me.pnlFilter_Shows.Top + Me.tblFilter_Shows.Top + Me.gbFilterGeneral_Shows.Top + Me.tblFilterGeneral_Shows.Top + Me.btnFilterMissing_Shows.Top) - Me.pnlFilterMissingItems_Shows.Height)
                Me.pnlFilterSources_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterSpecific_Movies.Left + Me.tblFilterSpecific_Movies.Left + Me.tblFilterSpecificData_Movies.Left + Me.txtFilterSource_Movies.Left + 1, _
                                                                (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterSpecific_Movies.Top + Me.tblFilterSpecific_Movies.Top + Me.tblFilterSpecificData_Movies.Top + Me.txtFilterSource_Movies.Top) - Me.pnlFilterSources_Movies.Height)
                Me.pnlFilterSources_Shows.Location = New Point(Me.pnlFilter_Shows.Left + Me.tblFilter_Shows.Left + Me.gbFilterSpecific_Shows.Left + Me.tblFilterSpecific_Shows.Left + Me.tblFilterSpecificData_Shows.Left + Me.txtFilterSource_Shows.Left + 1, _
                                                               (Me.pnlFilter_Shows.Top + Me.tblFilter_Shows.Top + Me.gbFilterSpecific_Shows.Top + Me.tblFilterSpecific_Shows.Top + Me.tblFilterSpecificData_Shows.Top + Me.txtFilterSource_Shows.Top) - Me.pnlFilterSources_Shows.Height)

                Select Case Me.tcMain.SelectedIndex
                    Case 0
                        Me.dgvMovies.Focus()
                    Case 1
                        Me.dgvMovieSets.Focus()
                    Case 2
                        Me.dgvTVShows.Focus()
                End Select

                Me.ResumeLayout(True)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub cmnuMovieUpSelActors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelActors.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bCast = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelCert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelCert.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bCert = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelCollectionID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelCollectionID.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bCollectionID = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelCountry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelCountry.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bCountry = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelDirector_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelDirector.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bDirector = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelGenre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelGenre.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bGenre = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelMPAA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelMPAA.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bMPAA = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelOriginalTitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelOriginalTitle.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bOriginalTitle = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelOutline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelOutline.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bOutline = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelPlot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelPlot.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bPlot = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelProducers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelProducers.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bProducers = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelRating_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelRating.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bRating = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelRelease.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bRelease = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelRuntime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelRuntime.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bRuntime = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelStudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelStudio.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bStudio = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelTagline_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelTagline.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bTagline = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelTitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelTitle.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bTitle = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelTop250_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelTop250.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bTop250 = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelTrailer.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bTrailer = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelWriters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelWriter.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bWriters = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub

    Private Sub cmnuMovieUpSelYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieUpSelYear.Click
        Dim cScrapeOptions As New Structures.ScrapeOptions_Movie
        cScrapeOptions.bYear = True
        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainNFO, True)
        CreateScrapeList_Movie(Enums.ScrapeType.SingleField, cScrapeOptions, ScrapeModifier)
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the movie selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"><c>Integer</c> row which is currently selected</param>
    ''' <remarks></remarks>
    Private Sub SelectRow_Movie(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Me.ClearInfo()

        If Me.dgvMovies.Rows.Count >= iRow Then
            If String.IsNullOrEmpty(Me.dgvMovies.Item("BannerPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvMovies.Item("ClearArtPath", iRow).Value.ToString) AndAlso _
                String.IsNullOrEmpty(Me.dgvMovies.Item("ClearLogoPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvMovies.Item("DiscArtPath", iRow).Value.ToString) AndAlso _
                String.IsNullOrEmpty(Me.dgvMovies.Item("EFanartsPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvMovies.Item("EThumbsPath", iRow).Value.ToString) AndAlso _
                String.IsNullOrEmpty(Me.dgvMovies.Item("FanartPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvMovies.Item("LandscapePath", iRow).Value.ToString) AndAlso _
                String.IsNullOrEmpty(Me.dgvMovies.Item("NfoPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvMovies.Item("PosterPath", iRow).Value.ToString) Then
                Me.ShowNoInfo(True, 0)
                Me.currMovie = Master.DB.LoadMovieFromDB(Convert.ToInt64(Me.dgvMovies.Item("idMovie", iRow).Value))
                Me.FillScreenInfoWith_Movie()

                If Not Me.bwMovieScraper.IsBusy AndAlso Not Me.bwMovieSetScraper.IsBusy AndAlso Not Me.bwNonScrape.IsBusy AndAlso Not Me.fScanner.IsBusy AndAlso Not Me.bwMetaData.IsBusy AndAlso Not Me.bwLoadMovieInfo.IsBusy AndAlso Not Me.bwLoadShowInfo.IsBusy AndAlso Not Me.bwLoadSeasonInfo.IsBusy AndAlso Not Me.bwLoadEpInfo.IsBusy AndAlso Not Me.bwReload_Movies.IsBusy AndAlso Not Me.bwReload_MovieSets.IsBusy AndAlso Not Me.bwCleanDB.IsBusy Then
                    Me.cmnuMovie.Enabled = True
                End If
            Else
                Me.LoadInfo_Movie(Convert.ToInt32(Me.dgvMovies.Item("idMovie", iRow).Value), Me.dgvMovies.Item("MoviePath", iRow).Value.ToString, True, False)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the movie selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"><c>Integer</c> row which is currently selected</param>
    ''' <remarks></remarks>
    Private Sub SelectRow_MovieSet(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Me.ClearInfo()

        If Me.dgvMovieSets.Rows.Count >= iRow Then
            If String.IsNullOrEmpty(Me.dgvMovieSets.Item("BannerPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvMovieSets.Item("ClearArtPath", iRow).Value.ToString) AndAlso _
                String.IsNullOrEmpty(Me.dgvMovieSets.Item("ClearLogoPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvMovieSets.Item("DiscArtPath", iRow).Value.ToString) AndAlso _
                String.IsNullOrEmpty(Me.dgvMovieSets.Item("FanartPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvMovieSets.Item("LandscapePath", iRow).Value.ToString) AndAlso _
                String.IsNullOrEmpty(Me.dgvMovieSets.Item("NfoPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvMovieSets.Item("PosterPath", iRow).Value.ToString) Then
                Me.ShowNoInfo(True, Enums.ContentType.MovieSet)

                Me.currMovieSet = Master.DB.LoadMovieSetFromDB(Convert.ToInt64(Me.dgvMovieSets.Item("idSet", iRow).Value))
                Me.FillScreenInfoWith_MovieSet()

                If Not Me.bwMovieScraper.IsBusy AndAlso Not Me.bwMovieSetScraper.IsBusy AndAlso Not Me.bwNonScrape.IsBusy AndAlso Not Me.fScanner.IsBusy AndAlso Not Me.bwMetaData.IsBusy AndAlso Not Me.bwLoadMovieInfo.IsBusy AndAlso Not Me.bwLoadShowInfo.IsBusy AndAlso Not Me.bwLoadSeasonInfo.IsBusy AndAlso Not Me.bwLoadEpInfo.IsBusy AndAlso Not Me.bwReload_Movies.IsBusy AndAlso Not Me.bwReload_MovieSets.IsBusy AndAlso Not Me.bwCleanDB.IsBusy Then
                    Me.cmnuMovie.Enabled = True
                End If
            Else
                Me.LoadInfo_MovieSet(Convert.ToInt32(Me.dgvMovieSets.Item("idSet", iRow).Value), True)
            End If
        End If
    End Sub

    Private Sub SelectRow_TVEpisode(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Me.ClearInfo()

        If Me.dgvTVEpisodes.Rows.Count >= iRow Then
            If String.IsNullOrEmpty(Me.dgvTVEpisodes.Item("FanartPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvTVEpisodes.Item("NfoPath", iRow).Value.ToString) AndAlso _
                String.IsNullOrEmpty(Me.dgvTVEpisodes.Item("PosterPath", iRow).Value.ToString) AndAlso Not Convert.ToInt64(Me.dgvTVEpisodes.Item("idFile", iRow).Value) = -1 Then
                Me.ShowNoInfo(True, Enums.ContentType.TVEpisode)

                Me.currTV = Master.DB.LoadTVEpisodeFromDB(Convert.ToInt32(Me.dgvTVEpisodes.Item("idEpisode", iRow).Value), True)
                Me.FillScreenInfoWith_TVEpisode()

                If Not Convert.ToInt64(Me.dgvTVEpisodes.Item("idFile", iRow).Value) = -1 AndAlso Not Me.fScanner.IsBusy AndAlso Not Me.bwMetaData.IsBusy AndAlso Not Me.bwLoadMovieInfo.IsBusy AndAlso Not Me.bwLoadMovieSetInfo.IsBusy AndAlso Not Me.bwLoadShowInfo.IsBusy AndAlso Not Me.bwLoadSeasonInfo.IsBusy AndAlso Not Me.bwLoadEpInfo.IsBusy AndAlso Not Me.bwReload_Movies.IsBusy AndAlso Not Me.bwReload_MovieSets.IsBusy AndAlso Not Me.bwCleanDB.IsBusy Then
                    Me.cmnuEpisode.Enabled = True
                End If
            Else
                Me.LoadInfo_TVEpisode(Convert.ToInt32(Me.dgvTVEpisodes.SelectedRows(0).Cells("idEpisode").Value))
            End If
        End If
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the TV Season selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"></param>
    ''' <remarks></remarks>
    Private Sub SelectRow_TVSeason(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Me.ClearInfo()

        If Me.dgvTVSeasons.Rows.Count >= iRow Then
            If String.IsNullOrEmpty(Me.dgvTVSeasons.Item("BannerPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvTVSeasons.Item("FanartPath", iRow).Value.ToString) AndAlso _
                String.IsNullOrEmpty(Me.dgvTVSeasons.Item("LandscapePath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvTVSeasons.Item("PosterPath", iRow).Value.ToString) AndAlso _
                Not Convert.ToBoolean(Me.dgvTVSeasons.Item("Missing", iRow).Value) Then
                If Not Me.currThemeType = Theming.ThemeType.Show Then Me.ApplyTheme(Theming.ThemeType.Show)
                Me.ShowNoInfo(True, Enums.ContentType.TVSeason)

                Me.currTV = Master.DB.LoadTVSeasonFromDB(Convert.ToInt32(Me.dgvTVSeasons.Item("idSeason", iRow).Value), True)
                Me.FillEpisodes(Convert.ToInt32(Me.dgvTVSeasons.Item("idShow", iRow).Value), Convert.ToInt32(Me.dgvTVSeasons.Item("Season", iRow).Value))

                If Not Me.fScanner.IsBusy AndAlso Not Me.bwMetaData.IsBusy AndAlso Not Me.bwLoadMovieInfo.IsBusy AndAlso Not Me.bwLoadMovieSetInfo.IsBusy AndAlso _
                    Not Me.bwLoadShowInfo.IsBusy AndAlso Not Me.bwLoadSeasonInfo.IsBusy AndAlso Not Me.bwLoadEpInfo.IsBusy AndAlso Not Me.bwReload_Movies.IsBusy AndAlso _
                    Not Me.bwReload_MovieSets.IsBusy AndAlso Not Me.bwCleanDB.IsBusy Then
                    Me.cmnuSeason.Enabled = True
                End If
            Else
                Me.LoadInfo_TVSeason(Convert.ToInt32(Me.dgvTVSeasons.Item("idSeason", iRow).Value), _
                                  If(CInt(Me.dgvTVSeasons.Item("Season", iRow).Value) = 999, False, CBool(Me.dgvTVSeasons.Item("Missing", iRow).Value)))
                Me.FillEpisodes(Convert.ToInt32(Me.dgvTVSeasons.Item("idShow", iRow).Value), Convert.ToInt32(Me.dgvTVSeasons.Item("Season", iRow).Value))
            End If
        End If
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the TV Show selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"></param>
    ''' <remarks></remarks>
    Private Sub SelectRow_TVShow(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Me.ClearInfo()

        If Me.dgvTVShows.Rows.Count >= iRow Then
            If String.IsNullOrEmpty(Me.dgvTVShows.Item("BannerPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvTVShows.Item("CharacterArtPath", iRow).Value.ToString) AndAlso _
                String.IsNullOrEmpty(Me.dgvTVShows.Item("ClearArtPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvTVShows.Item("ClearLogoPath", iRow).Value.ToString) AndAlso _
                String.IsNullOrEmpty(Me.dgvTVShows.Item("EFanartsPath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvTVShows.Item("FanartPath", iRow).Value.ToString) AndAlso _
                String.IsNullOrEmpty(Me.dgvTVShows.Item("LandscapePath", iRow).Value.ToString) AndAlso String.IsNullOrEmpty(Me.dgvTVShows.Item("NfoPath", iRow).Value.ToString) AndAlso _
                String.IsNullOrEmpty(Me.dgvTVShows.Item("PosterPath", iRow).Value.ToString) Then
                Me.ShowNoInfo(True, Enums.ContentType.TVShow)

                Me.currTV = Master.DB.LoadTVShowFromDB(Convert.ToInt64(Me.dgvTVShows.Item("idShow", iRow).Value), False, False, True, False)
                Me.FillSeasons(Convert.ToInt32(Me.dgvTVShows.Item("idShow", iRow).Value))

                If Not Me.fScanner.IsBusy AndAlso Not Me.bwMetaData.IsBusy AndAlso Not Me.bwLoadMovieInfo.IsBusy AndAlso Not Me.bwLoadMovieSetInfo.IsBusy AndAlso Not Me.bwLoadShowInfo.IsBusy AndAlso Not Me.bwLoadSeasonInfo.IsBusy AndAlso Not Me.bwLoadEpInfo.IsBusy AndAlso Not Me.bwReload_Movies.IsBusy AndAlso Not Me.bwReload_MovieSets.IsBusy AndAlso Not Me.bwCleanDB.IsBusy Then
                    Me.cmnuShow.Enabled = True
                End If
            Else
                Me.LoadInfo_TVShow(Convert.ToInt32(Me.dgvTVShows.Item("idShow", iRow).Value))
            End If
        End If
    End Sub

    Private Sub SetAVImages(ByVal aImage As Image())
        Me.pbResolution.Image = aImage(0)
        Me.pbVideo.Image = aImage(1)
        Me.pbVType.Image = aImage(2)
        Me.pbAudio.Image = aImage(3)
        Me.pbChannels.Image = aImage(4)
        Me.pbAudioLang0.Image = aImage(5)
        Me.pbAudioLang1.Image = aImage(6)
        Me.pbAudioLang2.Image = aImage(7)
        Me.pbAudioLang3.Image = aImage(8)
        Me.pbAudioLang4.Image = aImage(9)
        Me.pbAudioLang5.Image = aImage(10)
        Me.pbAudioLang6.Image = aImage(11)
        Me.pbSubtitleLang0.Image = aImage(12)
        Me.pbSubtitleLang1.Image = aImage(13)
        Me.pbSubtitleLang2.Image = aImage(14)
        Me.pbSubtitleLang3.Image = aImage(15)
        Me.pbSubtitleLang4.Image = aImage(16)
        Me.pbSubtitleLang5.Image = aImage(17)
        Me.pbSubtitleLang6.Image = aImage(18)

        ToolTips.SetToolTip(Me.pbAudioLang0, If(Me.pbAudioLang0.Image IsNot Nothing, Me.pbAudioLang0.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(Me.pbAudioLang1, If(Me.pbAudioLang1.Image IsNot Nothing, Me.pbAudioLang1.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(Me.pbAudioLang2, If(Me.pbAudioLang2.Image IsNot Nothing, Me.pbAudioLang2.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(Me.pbAudioLang3, If(Me.pbAudioLang3.Image IsNot Nothing, Me.pbAudioLang3.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(Me.pbAudioLang4, If(Me.pbAudioLang4.Image IsNot Nothing, Me.pbAudioLang4.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(Me.pbAudioLang5, If(Me.pbAudioLang5.Image IsNot Nothing, Me.pbAudioLang5.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(Me.pbAudioLang6, If(Me.pbAudioLang6.Image IsNot Nothing, Me.pbAudioLang6.Image.Tag.ToString, String.Empty))

        ToolTips.SetToolTip(Me.pbSubtitleLang0, If(Me.pbSubtitleLang0.Image IsNot Nothing, Me.pbSubtitleLang0.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(Me.pbSubtitleLang1, If(Me.pbSubtitleLang1.Image IsNot Nothing, Me.pbSubtitleLang1.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(Me.pbSubtitleLang2, If(Me.pbSubtitleLang2.Image IsNot Nothing, Me.pbSubtitleLang2.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(Me.pbSubtitleLang3, If(Me.pbSubtitleLang3.Image IsNot Nothing, Me.pbSubtitleLang3.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(Me.pbSubtitleLang4, If(Me.pbSubtitleLang4.Image IsNot Nothing, Me.pbSubtitleLang4.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(Me.pbSubtitleLang5, If(Me.pbSubtitleLang5.Image IsNot Nothing, Me.pbSubtitleLang5.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(Me.pbSubtitleLang6, If(Me.pbSubtitleLang6.Image IsNot Nothing, Me.pbSubtitleLang6.Image.Tag.ToString, String.Empty))
    End Sub

    Private Sub SetControlsEnabled(ByVal isEnabled As Boolean, Optional ByVal withLists As Boolean = False, Optional ByVal withTools As Boolean = True)
        Dim currMainTabTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
        For Each i As Object In Me.mnuMainTools.DropDownItems
            If TypeOf i Is ToolStripMenuItem Then
                Dim o As ToolStripMenuItem = DirectCast(i, ToolStripMenuItem)
                If o.Tag Is Nothing Then
                    o.Enabled = isEnabled AndAlso ((Me.dgvMovies.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.Movie) OrElse _
                                                   (Me.dgvMovieSets.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.MovieSet) OrElse _
                                                   (Me.dgvTVShows.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.TV))
                ElseIf TypeOf o.Tag Is Structures.ModulesMenus Then
                    Dim tagmenu As Structures.ModulesMenus = DirectCast(o.Tag, Structures.ModulesMenus)
                    o.Enabled = (isEnabled OrElse Not withTools) AndAlso (((tagmenu.IfTabMovies AndAlso currMainTabTag.ContentType = Enums.ContentType.Movie) OrElse _
                                                                           (tagmenu.IfTabMovieSets AndAlso currMainTabTag.ContentType = Enums.ContentType.MovieSet) OrElse _
                                                                           (tagmenu.IfTabTVShows AndAlso currMainTabTag.ContentType = Enums.ContentType.TV)) AndAlso _
                                                                       ((tagmenu.ForMovies AndAlso (Me.dgvMovies.RowCount > 0 OrElse tagmenu.IfNoMovies)) OrElse _
                                                                        (tagmenu.ForMovieSets AndAlso (Me.dgvMovieSets.RowCount > 0 OrElse tagmenu.IfNoMovieSets)) OrElse _
                                                                        (tagmenu.ForTVShows AndAlso (Me.dgvTVShows.RowCount > 0 OrElse tagmenu.IfNoTVShows))))
                End If
            ElseIf TypeOf i Is ToolStripSeparator Then
                Dim o As ToolStripSeparator = DirectCast(i, ToolStripSeparator)
                o.Visible = (Me.mnuMainTools.DropDownItems.IndexOf(o) < Me.mnuMainTools.DropDownItems.Count - 1)
            End If
        Next
        With Master.eSettings
            If (Not .FileSystemExpertCleaner AndAlso (.CleanDotFanartJPG OrElse .CleanFanartJPG OrElse .CleanFolderJPG OrElse .CleanMovieFanartJPG OrElse _
            .CleanMovieJPG OrElse .CleanMovieNameJPG OrElse .CleanMovieNFO OrElse .CleanMovieNFOB OrElse _
            .CleanMovieTBN OrElse .CleanMovieTBNB OrElse .CleanPosterJPG OrElse .CleanPosterTBN OrElse .CleanExtrathumbs)) OrElse _
            (.FileSystemExpertCleaner AndAlso (.FileSystemCleanerWhitelist OrElse .FileSystemCleanerWhitelistExts.Count > 0)) Then
                Me.mnuMainToolsCleanFiles.Enabled = isEnabled AndAlso Me.dgvMovies.RowCount > 0 AndAlso Me.tcMain.SelectedIndex = 0
            Else
                Me.mnuMainToolsCleanFiles.Enabled = False
            End If
            If Not String.IsNullOrEmpty(.MovieBackdropsPath) AndAlso Me.dgvMovies.RowCount > 0 Then
                Me.mnuMainToolsBackdrops.Enabled = True
            Else
                Me.mnuMainToolsBackdrops.Enabled = False
            End If
        End With
        Me.mnuMainEdit.Enabled = isEnabled
        Me.mnuScrapeMovies.Enabled = isEnabled AndAlso Me.dgvMovies.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.Movie
        Me.mnuScrapeMovies.Visible = currMainTabTag.ContentType = Enums.ContentType.Movie
        Me.mnuScrapeMovieSets.Enabled = isEnabled AndAlso Me.dgvMovieSets.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.MovieSet
        Me.mnuScrapeMovieSets.Visible = currMainTabTag.ContentType = Enums.ContentType.MovieSet
        Me.mnuScrapeTVShows.Enabled = isEnabled AndAlso Me.dgvTVShows.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.TV
        Me.mnuScrapeTVShows.Visible = currMainTabTag.ContentType = Enums.ContentType.TV
        Me.mnuUpdate.Enabled = isEnabled
        Me.tsbMediaCenters.Enabled = isEnabled
        Me.cmnuMovie.Enabled = isEnabled
        Me.cmnuMovieSet.Enabled = isEnabled
        Me.cmnuShow.Enabled = isEnabled
        Me.cmnuSeason.Enabled = isEnabled
        Me.cmnuEpisode.Enabled = isEnabled
        Me.tcMain.Enabled = isEnabled
        Me.btnMarkAll.Enabled = isEnabled
        Me.btnMetaDataRefresh.Enabled = isEnabled
        Me.scMain.IsSplitterFixed = Not isEnabled
        Me.scTV.IsSplitterFixed = Not isEnabled
        Me.scTVSeasonsEpisodes.IsSplitterFixed = Not isEnabled
        Me.mnuMainHelp.Enabled = isEnabled
        Me.cmnuTrayTools.Enabled = Me.mnuMainTools.Enabled
        Me.cmnuTrayScrapeMovies.Enabled = isEnabled AndAlso Me.dgvMovies.RowCount > 0
        Me.cmnuTrayScrapeMovieSets.Enabled = isEnabled AndAlso Me.dgvMovieSets.RowCount > 0
        Me.cmnuTrayScrapeTVShows.Enabled = isEnabled AndAlso Me.dgvTVShows.RowCount > 0
        Me.cmnuTrayUpdate.Enabled = isEnabled
        Me.cmnuTraySettings.Enabled = isEnabled
        Me.cmnuTrayExit.Enabled = isEnabled

        If withLists OrElse isEnabled Then
            Me.dgvMovies.TabStop = isEnabled
            Me.dgvMovieSets.TabStop = isEnabled
            Me.dgvTVShows.TabStop = isEnabled
            Me.dgvTVSeasons.TabStop = isEnabled
            Me.dgvTVEpisodes.TabStop = isEnabled
            Me.dgvMovies.Enabled = isEnabled
            Me.dgvMovieSets.Enabled = isEnabled
            Me.dgvTVShows.Enabled = isEnabled
            Me.dgvTVSeasons.Enabled = isEnabled
            Me.dgvTVEpisodes.Enabled = isEnabled
            Me.txtSearchMovies.Enabled = isEnabled
            Me.txtSearchMovieSets.Enabled = isEnabled
            Me.txtSearchShows.Enabled = isEnabled
        End If
    End Sub

    Private Sub SetWatchedStatus_Movie()
        Dim setWatched As Boolean = False
        If Me.dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                'if any one item is set as not watched, set menu to watched
                'else they are all watched so set menu to not watched
                If String.IsNullOrEmpty(sRow.Cells("Playcount").Value.ToString) OrElse sRow.Cells("Playcount").Value.ToString = "0" Then
                    setWatched = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                Dim hasWatched As Boolean = False
                Dim currPlaycount As Integer

                Dim tmpDBMovie As Database.DBElement = Master.DB.LoadMovieFromDB(Convert.ToInt64(sRow.Cells("idMovie").Value), False)

                currPlaycount = tmpDBMovie.Movie.PlayCount
                hasWatched = tmpDBMovie.Movie.PlayCountSpecified

                If Me.dgvMovies.SelectedRows.Count > 1 AndAlso setWatched Then
                    tmpDBMovie.Movie.PlayCount = If(tmpDBMovie.Movie.PlayCountSpecified, currPlaycount, 1)
                ElseIf Not hasWatched Then
                    tmpDBMovie.Movie.PlayCount = 1
                Else
                    tmpDBMovie.Movie.PlayCount = 0
                End If

                Master.DB.SaveMovieToDB(tmpDBMovie, False, True, True, False)
                RefreshRow_Movie(tmpDBMovie.ID)
                Application.DoEvents()
            Next
            SQLtransaction.Commit()
        End Using
    End Sub

    Private Sub SetWatchedStatus_TVEpisode()
        Dim setWatched As Boolean = False
        Dim SeasonsList As New List(Of Integer)
        If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                'if any one item is set as not watched, set menu to watched
                'else they are all watched so set menu to not watched
                If String.IsNullOrEmpty(sRow.Cells("Playcount").Value.ToString) OrElse sRow.Cells("Playcount").Value.ToString = "0" Then
                    setWatched = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Dim idShow As Integer = CInt(Me.dgvTVEpisodes.SelectedRows(0).Cells("idShow").Value)
            For Each sRow As DataGridViewRow In Me.dgvTVEpisodes.SelectedRows
                If Not SeasonsList.Contains(CInt(sRow.Cells("Season").Value)) Then SeasonsList.Add(CInt(sRow.Cells("Season").Value))
                Dim currPlaycount As Integer
                Dim hasWatched As Boolean = False

                Dim tmpDBTVEpisode As Database.DBElement = Master.DB.LoadTVEpisodeFromDB(Convert.ToInt64(sRow.Cells("idEpisode").Value), True, False)

                currPlaycount = tmpDBTVEpisode.TVEpisode.Playcount
                hasWatched = tmpDBTVEpisode.TVEpisode.PlaycountSpecified

                If Me.dgvTVEpisodes.SelectedRows.Count > 1 AndAlso setWatched Then
                    tmpDBTVEpisode.TVEpisode.Playcount = If(tmpDBTVEpisode.TVEpisode.PlaycountSpecified, currPlaycount, 1)
                ElseIf Not hasWatched Then
                    tmpDBTVEpisode.TVEpisode.Playcount = 1
                Else
                    tmpDBTVEpisode.TVEpisode.Playcount = 0
                End If

                Master.DB.SaveTVEpisodeToDB(tmpDBTVEpisode, False, True, True, False, False)
                Me.RefreshRow_TVEpisode(tmpDBTVEpisode.ID)
                Application.DoEvents()
            Next
            For Each iSeason In SeasonsList
                Me.RefreshRow_TVSeason(idShow, iSeason)
                Application.DoEvents()
            Next
            Me.RefreshRow_TVShow(idShow)
            Application.DoEvents()
            SQLtransaction.Commit()
        End Using
    End Sub

    Private Sub SetWatchedStatus_TVSeason()
        Dim setWatched As Boolean = False
        Dim ShowsList As New List(Of Integer)
        If Me.dgvTVSeasons.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                If Not CInt(sRow.Cells("Season").Value) = 999 Then
                    'if any one item is set as not watched, set menu to watched
                    'else they are all watched so set menu to not watched
                    If Not CBool(sRow.Cells("HasWatched").Value) Then
                        setWatched = True
                        Exit For
                    End If
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvTVSeasons.SelectedRows
                If Not CInt(sRow.Cells("Season").Value) = 999 Then
                    Dim hasWatched As Boolean = CBool(sRow.Cells("HasWatched").Value)
                    Dim iSeason As Integer = CInt(sRow.Cells("Season").Value)
                    Dim iShow As Integer = CInt(sRow.Cells("idShow").Value)
                    If Not ShowsList.Contains(iShow) Then ShowsList.Add(iShow)
                    Using SQLcommand_get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLcommand_get.CommandText = String.Format("SELECT idEpisode FROM episode WHERE NOT idFile = -1 AND idShow = {0} AND Season = {1};", iShow, iSeason)
                        Using SQLreader As SQLite.SQLiteDataReader = SQLcommand_get.ExecuteReader()
                            While SQLreader.Read
                                Dim currPlaycount As Integer

                                Dim tmpDBTVEpisode As Database.DBElement = Master.DB.LoadTVEpisodeFromDB(Convert.ToInt64(SQLreader("idEpisode")), True, False)

                                currPlaycount = tmpDBTVEpisode.TVEpisode.Playcount

                                If Me.dgvTVSeasons.SelectedRows.Count > 1 AndAlso setWatched Then
                                    tmpDBTVEpisode.TVEpisode.Playcount = If(tmpDBTVEpisode.TVEpisode.PlaycountSpecified, currPlaycount, 1)
                                ElseIf Not hasWatched Then
                                    tmpDBTVEpisode.TVEpisode.Playcount = If(tmpDBTVEpisode.TVEpisode.PlaycountSpecified, currPlaycount, 1)
                                Else
                                    tmpDBTVEpisode.TVEpisode.Playcount = 0
                                End If

                                Master.DB.SaveTVEpisodeToDB(tmpDBTVEpisode, False, True, True, False, False)
                                Me.RefreshRow_TVEpisode(tmpDBTVEpisode.ID)
                                Application.DoEvents()
                            End While
                        End Using
                    End Using
                    Me.RefreshRow_TVSeason(iShow, iSeason)
                    Application.DoEvents()
                End If
            Next
            For Each iShowID In ShowsList
                Me.RefreshRow_TVShow(iShowID)
                Application.DoEvents()
            Next
            SQLtransaction.Commit()
        End Using
    End Sub

    Private Sub SetWatchedStatus_TVShow()
        Dim setWatched As Boolean = False
        Dim SeasonsList As New List(Of Integer)
        If Me.dgvTVShows.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                'if any one item is set as not watched, set menu to watched
                'else they are all watched so set menu to not watched
                If Not CBool(sRow.Cells("HasWatched").Value) Then
                    setWatched = True
                    Exit For
                End If
            Next
        End If

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                Dim hasWatched As Boolean = CBool(sRow.Cells("HasWatched").Value)
                Dim ShowID As Integer = CInt(sRow.Cells("idShow").Value)
                Using SQLcommand_get As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLcommand_get.CommandText = String.Format("SELECT idEpisode, Season FROM episode WHERE NOT idFile = -1 AND idShow = {0};", ShowID)
                    Using SQLreader As SQLite.SQLiteDataReader = SQLcommand_get.ExecuteReader()
                        While SQLreader.Read
                            If Not SeasonsList.Contains(CInt(SQLreader("Season"))) Then SeasonsList.Add(CInt(SQLreader("Season")))
                            Dim currPlaycount As Integer

                            Dim tmpDBTVEpisode As Database.DBElement = Master.DB.LoadTVEpisodeFromDB(Convert.ToInt64(SQLreader("idEpisode")), True, False)

                            currPlaycount = tmpDBTVEpisode.TVEpisode.Playcount

                            If Me.dgvTVShows.SelectedRows.Count > 1 AndAlso setWatched Then
                                tmpDBTVEpisode.TVEpisode.Playcount = If(tmpDBTVEpisode.TVEpisode.PlaycountSpecified, currPlaycount, 1)
                            ElseIf Not hasWatched Then
                                tmpDBTVEpisode.TVEpisode.Playcount = If(tmpDBTVEpisode.TVEpisode.PlaycountSpecified, currPlaycount, 1)
                            Else
                                tmpDBTVEpisode.TVEpisode.Playcount = 0
                            End If

                            Master.DB.SaveTVEpisodeToDB(tmpDBTVEpisode, False, True, True, False, False)
                            Me.RefreshRow_TVEpisode(tmpDBTVEpisode.ID)
                            Application.DoEvents()
                        End While
                    End Using
                End Using
                For Each iSeason In SeasonsList
                    Me.RefreshRow_TVSeason(ShowID, iSeason)
                    Application.DoEvents()
                Next
                Me.RefreshRow_TVShow(ShowID)
                Application.DoEvents()
            Next
            SQLtransaction.Commit()
        End Using
    End Sub

    Private Sub cmnuMovieGenresAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieGenresAdd.Click
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                Dim tmpDBMovie As Database.DBElement = Master.DB.LoadMovieFromDB(Convert.ToInt32(sRow.Cells("idMovie").Value))
                If Not tmpDBMovie.Movie.Genres.Contains(Me.cmnuMovieGenresGenre.Text.Trim) Then
                    tmpDBMovie.Movie.Genres.Add(Me.cmnuMovieGenresGenre.Text.Trim)
                    Master.DB.SaveMovieToDB(tmpDBMovie, False, True, True, False)
                    RefreshRow_Movie(tmpDBMovie.ID)
                End If
            Next
            SQLtransaction.Commit()
        End Using
    End Sub

    Private Sub cmnuMovieGenresRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieGenresRemove.Click
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                Dim tmpDBMovie As Database.DBElement = Master.DB.LoadMovieFromDB(Convert.ToInt32(sRow.Cells("idMovie").Value))
                If tmpDBMovie.Movie.Genres.Contains(Me.cmnuMovieGenresGenre.Text.Trim) Then
                    tmpDBMovie.Movie.Genres.Remove(Me.cmnuMovieGenresGenre.Text.Trim)
                    Master.DB.SaveMovieToDB(tmpDBMovie, False, True, True, False)
                    RefreshRow_Movie(tmpDBMovie.ID)
                End If
            Next
            SQLtransaction.Commit()
        End Using
    End Sub

    Private Sub cmnuMovieGenresSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieGenresSet.Click
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                Dim tmpDBMovie As Database.DBElement = Master.DB.LoadMovieFromDB(Convert.ToInt32(sRow.Cells("idMovie").Value))
                tmpDBMovie.Movie.Genres.Clear()
                tmpDBMovie.Movie.Genres.Add(Me.cmnuMovieGenresGenre.Text.Trim)
                Master.DB.SaveMovieToDB(tmpDBMovie, False, True, True, False)
                RefreshRow_Movie(tmpDBMovie.ID)
            Next
            SQLtransaction.Commit()
        End Using
    End Sub

    Private Sub cmnumovieLanguageSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieLanguageSet.Click
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                Dim tmpDBMovie As Database.DBElement = Master.DB.LoadMovieFromDB(Convert.ToInt32(sRow.Cells("idMovie").Value), False)
                tmpDBMovie.Language = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = Me.cmnuMovieLanguageLanguages.Text).abbreviation
                tmpDBMovie.Movie.Language = tmpDBMovie.Language
                Master.DB.SaveMovieToDB(tmpDBMovie, False, True, True, False)
                RefreshRow_Movie(tmpDBMovie.ID)
            Next
            SQLtransaction.Commit()
        End Using
    End Sub

    Private Sub cmnuShowLanguageSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuShowLanguageSet.Click
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvTVShows.SelectedRows
                Dim tmpDBTVShow As Database.DBElement = Master.DB.LoadTVShowFromDB(Convert.ToInt32(sRow.Cells("idShow").Value), True, False, False)
                tmpDBTVShow.Language = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = Me.cmnuShowLanguageLanguages.Text).abbreviation
                tmpDBTVShow.TVShow.Language = tmpDBTVShow.Language
                Master.DB.SaveTVShowToDB(tmpDBTVShow, False, False, True, False, False)
                RefreshRow_TVShow(tmpDBTVShow.ID)
            Next
            SQLtransaction.Commit()
        End Using
    End Sub

    Private Sub cmnuMovieSetSortMethodSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuMovieSetSortMethodSet.Click
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In Me.dgvMovieSets.SelectedRows
                Dim tmpDBMovieSet As Database.DBElement = Master.DB.LoadMovieSetFromDB(Convert.ToInt32(sRow.Cells("idSet").Value))
                tmpDBMovieSet.SortMethod = CType(Me.cmnuMovieSetSortMethodMethods.ComboBox.SelectedValue, Enums.SortMethod_MovieSet)
                Master.DB.SaveMovieSetToDB(tmpDBMovieSet, False, True, True)
                RefreshRow_MovieSet(tmpDBMovieSet.ID)
            Next
            SQLtransaction.Commit()
        End Using
    End Sub
    ''' <summary>
    ''' Enable or disable the various menu and context-menu actions based on the currently-defined settings
    ''' </summary>
    ''' <param name="ReloadFilters"></param>
    ''' <remarks></remarks>
    Private Sub SetMenus(ByVal ReloadFilters As Boolean)
        Dim mnuItem As ToolStripItem
        Dim currMainTabTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)

        With Master.eSettings
            If (Not .FileSystemExpertCleaner AndAlso (.CleanDotFanartJPG OrElse .CleanFanartJPG OrElse .CleanFolderJPG OrElse .CleanMovieFanartJPG OrElse _
            .CleanMovieJPG OrElse .CleanMovieNameJPG OrElse .CleanMovieNFO OrElse .CleanMovieNFOB OrElse _
            .CleanMovieTBN OrElse .CleanMovieTBNB OrElse .CleanPosterJPG OrElse .CleanPosterTBN OrElse .CleanExtrathumbs)) OrElse _
            (.FileSystemExpertCleaner AndAlso (.FileSystemCleanerWhitelist OrElse .FileSystemCleanerWhitelistExts.Count > 0)) Then
                Me.mnuMainToolsCleanFiles.Enabled = True AndAlso Me.dgvMovies.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.Movie
            Else
                Me.mnuMainToolsCleanFiles.Enabled = False
            End If

            Me.mnuMainToolsBackdrops.Enabled = Directory.Exists(.MovieBackdropsPath)

            ' for future use
            Me.mnuMainToolsClearCache.Enabled = False

            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLNewcommand.CommandText = String.Concat("SELECT COUNT(idMovie) AS mcount FROM movie WHERE mark = 1;")
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    SQLcount.Read()
                    If SQLcount.HasRows AndAlso Convert.ToInt32(SQLcount("mcount")) > 0 Then
                        Me.btnMarkAll.Text = Master.eLang.GetString(105, "Unmark All")
                    Else
                        Me.btnMarkAll.Text = Master.eLang.GetString(35, "Mark All")
                    End If
                End Using
            End Using

            Me.mnuUpdateMovies.DropDownItems.Clear()
            Me.cmnuTrayUpdateMovies.DropDownItems.Clear()
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLNewcommand.CommandText = "SELECT COUNT(ID) AS cID FROM Sources;"
                If Convert.ToInt32(SQLNewcommand.ExecuteScalar) > 1 Then
                    mnuItem = Me.mnuUpdateMovies.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New System.EventHandler(AddressOf SourceSubClick))
                    mnuItem.Tag = String.Empty
                    mnuItem = Me.cmnuTrayUpdateMovies.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New System.EventHandler(AddressOf SourceSubClick))
                    mnuItem.Tag = String.Empty
                End If
                SQLNewcommand.CommandText = "SELECT Name, Exclude FROM Sources;"
                Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    While SQLReader.Read
                        mnuItem = Me.mnuUpdateMovies.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), SQLReader("Name")), Nothing, New System.EventHandler(AddressOf SourceSubClick))
                        mnuItem.Tag = SQLReader("Name").ToString
                        mnuItem.ForeColor = If(Convert.ToBoolean(SQLReader("Exclude")), Color.Gray, Color.Black)
                        mnuItem = Me.cmnuTrayUpdateMovies.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), SQLReader("Name")), Nothing, New System.EventHandler(AddressOf SourceSubClick))
                        mnuItem.Tag = SQLReader("Name").ToString
                        mnuItem.ForeColor = If(Convert.ToBoolean(SQLReader("Exclude")), Color.Gray, Color.Black)
                    End While
                End Using
            End Using

            Me.mnuUpdateShows.DropDownItems.Clear()
            Me.cmnuTrayUpdateShows.DropDownItems.Clear()
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLNewcommand.CommandText = "SELECT COUNT(ID) AS cID FROM TVSources;"
                If Convert.ToInt32(SQLNewcommand.ExecuteScalar) > 1 Then
                    mnuItem = Me.mnuUpdateShows.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New System.EventHandler(AddressOf TVSourceSubClick))
                    mnuItem.Tag = String.Empty
                    mnuItem = Me.cmnuTrayUpdateShows.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New System.EventHandler(AddressOf TVSourceSubClick))
                    mnuItem.Tag = String.Empty
                End If
                SQLNewcommand.CommandText = "SELECT Name, Exclude FROM TVSources;"
                Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    While SQLReader.Read
                        mnuItem = Me.mnuUpdateShows.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), SQLReader("Name")), Nothing, New System.EventHandler(AddressOf TVSourceSubClick))
                        mnuItem.Tag = SQLReader("Name").ToString
                        mnuItem.ForeColor = If(Convert.ToBoolean(SQLReader("Exclude")), Color.Gray, Color.Black)
                        mnuItem = Me.cmnuTrayUpdateShows.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), SQLReader("Name")), Nothing, New System.EventHandler(AddressOf TVSourceSubClick))
                        mnuItem.Tag = SQLReader("Name").ToString
                        mnuItem.ForeColor = If(Convert.ToBoolean(SQLReader("Exclude")), Color.Gray, Color.Black)
                    End While
                End Using
            End Using

            Me.cmnuMovieGenresGenre.Items.Clear()
            Me.clbFilterGenres_Movies.Items.Clear()
            Dim mGenre() As Object = APIXML.GetGenreList
            Me.cmnuMovieGenresGenre.Items.AddRange(mGenre)
            Me.clbFilterGenres_Movies.Items.Add(Master.eLang.None)
            Me.clbFilterGenres_Movies.Items.AddRange(mGenre)

            Me.clbFilterGenres_Shows.Items.Clear()
            Dim sGenre() As Object = APIXML.GetGenreList
            Me.clbFilterGenres_Shows.Items.Add(Master.eLang.None)
            Me.clbFilterGenres_Shows.Items.AddRange(sGenre)

            Me.clbFilterCountries_Movies.Items.Clear()
            Dim mCountry() As Object = Master.DB.GetMovieCountries
            Me.clbFilterCountries_Movies.Items.Add(Master.eLang.None)
            Me.clbFilterCountries_Movies.Items.AddRange(mCountry)

            Me.clbFilterDataFields_Movies.Items.Clear()
            Me.clbFilterDataFields_Movies.Items.AddRange(New Object() {"Certification", "Credits", "Director", "Imdb", "MPAA", "OriginalTitle", "Outline", "Plot", "Rating", "ReleaseDate", "Runtime", "SortTitle", "Studio", "TMDB", "TMDBColID", "Tagline", "Title", "Trailer", "Votes", "Year"})

            Me.cmnuMovieLanguageLanguages.Items.Clear()
            Me.cmnuMovieLanguageLanguages.Items.AddRange((From lLang In Master.eSettings.TVGeneralLanguages.Language Select lLang.name).ToArray)

            Me.cmnuShowLanguageLanguages.Items.Clear()
            Me.cmnuShowLanguageLanguages.Items.AddRange((From lLang In Master.eSettings.TVGeneralLanguages.Language Select lLang.name).ToArray)

            Dim SortMethods As New Dictionary(Of String, Enums.SortMethod_MovieSet)
            SortMethods.Add(Master.eLang.GetString(278, "Year"), Enums.SortMethod_MovieSet.Year)
            SortMethods.Add(Master.eLang.GetString(21, "Title"), Enums.SortMethod_MovieSet.Title)
            Me.cmnuMovieSetSortMethodMethods.ComboBox.DataSource = SortMethods.ToList
            Me.cmnuMovieSetSortMethodMethods.ComboBox.DisplayMember = "Key"
            Me.cmnuMovieSetSortMethodMethods.ComboBox.ValueMember = "Value"
            Me.cmnuMovieSetSortMethodMethods.ComboBox.BindingContext = Me.BindingContext

            Me.listViews_Movies.Clear()
            Me.listViews_Movies.Add(Master.eLang.GetString(786, "Default List"), "movielist")
            For Each cList As String In Master.DB.GetViewList(Enums.ContentType.Movie)
                Me.listViews_Movies.Add(Regex.Replace(cList, "movie-", String.Empty).Trim, cList)
            Next
            Me.cbFilterLists_Movies.DataSource = Me.listViews_Movies.ToList
            Me.cbFilterLists_Movies.DisplayMember = "Key"
            Me.cbFilterLists_Movies.ValueMember = "Value"
            Me.cbFilterLists_Movies.SelectedIndex = 0

            Me.listViews_MovieSets.Clear()
            Me.listViews_MovieSets.Add(Master.eLang.GetString(786, "Default List"), "setslist")
            For Each cList As String In Master.DB.GetViewList(Enums.ContentType.MovieSet)
                Me.listViews_MovieSets.Add(Regex.Replace(cList, "sets-", String.Empty).Trim, cList)
            Next
            Me.cbFilterLists_MovieSets.DataSource = Me.listViews_MovieSets.ToList
            Me.cbFilterLists_MovieSets.DisplayMember = "Key"
            Me.cbFilterLists_MovieSets.ValueMember = "Value"
            Me.cbFilterLists_MovieSets.SelectedIndex = 0

            Me.listViews_Shows.Clear()
            Me.listViews_Shows.Add(Master.eLang.GetString(786, "Default List"), "tvshowlist")
            For Each cList As String In Master.DB.GetViewList(Enums.ContentType.TVShow)
                Me.listViews_Shows.Add(Regex.Replace(cList, "tvshow-", String.Empty).Trim, cList)
            Next
            Me.cbFilterLists_Shows.DataSource = Me.listViews_Shows.ToList
            Me.cbFilterLists_Shows.DisplayMember = "Key"
            Me.cbFilterLists_Shows.ValueMember = "Value"
            Me.cbFilterLists_Shows.SelectedIndex = 0

            'not technically a menu, but it's a good place to put it
            If ReloadFilters Then

                RemoveHandler Me.cbFilterDataField_Movies.SelectedIndexChanged, AddressOf Me.clbFilterDataFields_Movies_LostFocus
                Me.cbFilterDataField_Movies.Items.Clear()
                Me.cbFilterDataField_Movies.Items.AddRange(New Object() {Master.eLang.GetString(1291, "Is Empty"), Master.eLang.GetString(1292, "Is Not Empty")})
                Me.cbFilterDataField_Movies.SelectedIndex = 0
                AddHandler Me.cbFilterDataField_Movies.SelectedIndexChanged, AddressOf Me.clbFilterDataFields_Movies_LostFocus

                Me.clbFilterSources_Movies.Items.Clear()
                Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLNewcommand.CommandText = String.Concat("SELECT Name FROM Sources;")
                    Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                        While SQLReader.Read
                            Me.clbFilterSources_Movies.Items.Add(SQLReader("Name"))
                        End While
                    End Using
                End Using

                Me.clbFilterSource_Shows.Items.Clear()
                Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLNewcommand.CommandText = String.Concat("SELECT Name FROM TVSources;")
                    Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                        While SQLReader.Read
                            Me.clbFilterSource_Shows.Items.Add(SQLReader("Name"))
                        End While
                    End Using
                End Using

                RemoveHandler Me.cbFilterYearFrom_Movies.SelectedIndexChanged, AddressOf Me.cbFilterYearFrom_Movies_SelectedIndexChanged
                Me.cbFilterYearFrom_Movies.Items.Clear()
                Me.cbFilterYearFrom_Movies.Items.Add(Master.eLang.All)
                For i As Integer = (Date.Now.Year + 1) To 1888 Step -1
                    Me.cbFilterYearFrom_Movies.Items.Add(i)
                Next
                Me.cbFilterYearFrom_Movies.SelectedIndex = 0
                AddHandler Me.cbFilterYearFrom_Movies.SelectedIndexChanged, AddressOf Me.cbFilterYearFrom_Movies_SelectedIndexChanged

                RemoveHandler Me.cbFilterYearModFrom_Movies.SelectedIndexChanged, AddressOf Me.cbFilterYearModFrom_Movies_SelectedIndexChanged
                Me.cbFilterYearModFrom_Movies.SelectedIndex = 0
                AddHandler Me.cbFilterYearModFrom_Movies.SelectedIndexChanged, AddressOf Me.cbFilterYearModFrom_Movies_SelectedIndexChanged

                RemoveHandler Me.cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf Me.cbFilterYearTo_Movies_SelectedIndexChanged
                Me.cbFilterYearTo_Movies.Items.Clear()
                Me.cbFilterYearTo_Movies.Items.Add(Master.eLang.All)
                For i As Integer = (Date.Now.Year + 1) To 1888 Step -1
                    Me.cbFilterYearTo_Movies.Items.Add(i)
                Next
                Me.cbFilterYearTo_Movies.SelectedIndex = 0
                AddHandler Me.cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf Me.cbFilterYearTo_Movies_SelectedIndexChanged

                RemoveHandler Me.cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf Me.cbFilterYearModTo_Movies_SelectedIndexChanged
                Me.cbFilterYearModTo_Movies.SelectedIndex = 0
                AddHandler Me.cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf Me.cbFilterYearModTo_Movies_SelectedIndexChanged

                RemoveHandler Me.cbFilterVideoSource_Movies.SelectedIndexChanged, AddressOf Me.cbFilterVideoSource_Movies_SelectedIndexChanged
                Me.cbFilterVideoSource_Movies.Items.Clear()
                Me.cbFilterVideoSource_Movies.Items.Add(Master.eLang.All)
                'Cocotus 2014/10/11 Automatically populate avalaible videosources from user settings to sourcefilter instead of using hardcoded list here!
                Dim mySources As New List(Of AdvancedSettingsComplexSettingsTableItem)
                mySources = clsAdvancedSettings.GetComplexSetting("MovieSources")
                If Not mySources Is Nothing Then
                    For Each k In mySources
                        If cbFilterVideoSource_Movies.Items.Contains(k.Value) = False Then
                            Me.cbFilterVideoSource_Movies.Items.Add(k.Value)
                        End If
                    Next
                Else
                    Me.cbFilterVideoSource_Movies.Items.AddRange(APIXML.SourceList.ToArray)
                End If
                Me.cbFilterVideoSource_Movies.Items.Add(Master.eLang.None)
                Me.cbFilterVideoSource_Movies.SelectedIndex = 0
                AddHandler Me.cbFilterVideoSource_Movies.SelectedIndexChanged, AddressOf Me.cbFilterVideoSource_Movies_SelectedIndexChanged
            End If

        End With
        Me.mnuScrapeMovies.Enabled = (Me.dgvMovies.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.Movie)
        Me.mnuScrapeMovies.Visible = currMainTabTag.ContentType = Enums.ContentType.Movie
        Me.mnuScrapeMovieSets.Enabled = (Me.dgvMovieSets.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.MovieSet)
        Me.mnuScrapeMovieSets.Visible = currMainTabTag.ContentType = Enums.ContentType.MovieSet
        Me.mnuScrapeTVShows.Enabled = (Me.dgvTVShows.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.TV)
        Me.mnuScrapeTVShows.Visible = currMainTabTag.ContentType = Enums.ContentType.TV
        Me.cmnuTrayScrapeMovies.Enabled = Me.dgvMovies.RowCount > 0
        Me.cmnuTrayScrapeMovieSets.Enabled = Me.dgvMovieSets.RowCount > 0
        Me.cmnuTrayScrapeTVShows.Enabled = Me.dgvTVShows.RowCount > 0
    End Sub

    Private Sub mnuMainToolsOfflineMM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsOfflineHolder.Click, cmnuTrayToolsOfflineHolder.Click
        Me.SetControlsEnabled(False)
        'Using dOfflineHolder As New dlgOfflineHolder
        '    dOfflineHolder.ShowDialog()
        'End Using
        Me.LoadMedia(New Structures.Scans With {.Movies = True, .TV = False})
        Me.SetControlsEnabled(True)
    End Sub

    Private Sub SetStatus(ByVal sText As String)
        Me.tslStatus.Text = sText.Replace("&", "&&")
    End Sub

    Private Function GetStatus() As String
        Return Me.tslStatus.Text.Replace("&&", "&")
    End Function

    Sub HideLoadingSettings()
        If Not Me.pnlLoadSettings.InvokeRequired Then
            Me.pnlLoadSettings.Visible = False
        End If
    End Sub
    ''' <summary>
    ''' Display the settings dialog (passed as parameter) and process the results.
    ''' </summary>
    ''' <param name="dlg"></param>
    ''' <remarks></remarks>
    Sub SettingsShow(ByVal dlg As dlgSettings)
        AddHandler dlg.LoadEnd, AddressOf HideLoadingSettings
        Dim dresult As Structures.SettingsResult = dlg.ShowDialog()
        RemoveHandler dlg.LoadEnd, AddressOf HideLoadingSettings
        Me.mnuMainEditSettings.Enabled = True
        Me.pnlLoadSettings.Visible = False
        Me.cmnuTraySettings.Enabled = True
        Me.cmnuTrayExit.Enabled = True

        'set all lists back to default before run "FillList"
        Me.currList_Movies = "movielist"
        Me.currList_MovieSets = "setslist"
        Me.currList_Shows = "tvshowlist"

        If Not dresult.DidCancel Then

            Me.SetUp(True)

            If Me.dgvMovies.RowCount > 0 Then
                Me.dgvMovies.Columns("BannerPath").Visible = Not CheckColumnHide_Movies("BannerPath")
                Me.dgvMovies.Columns("ClearArtPath").Visible = Not CheckColumnHide_Movies("ClearArtPath")
                Me.dgvMovies.Columns("ClearLogoPath").Visible = Not CheckColumnHide_Movies("ClearLogoPath")
                Me.dgvMovies.Columns("DiscArtPath").Visible = Not CheckColumnHide_Movies("DiscArtPath")
                Me.dgvMovies.Columns("EFanartsPath").Visible = Not CheckColumnHide_Movies("EFanartsPath")
                Me.dgvMovies.Columns("EThumbsPath").Visible = Not CheckColumnHide_Movies("EThumbsPath")
                Me.dgvMovies.Columns("FanartPath").Visible = Not CheckColumnHide_Movies("FanartPath")
                Me.dgvMovies.Columns("HasSet").Visible = Not CheckColumnHide_Movies("HasSet")
                Me.dgvMovies.Columns("HasSub").Visible = Not CheckColumnHide_Movies("HasSub")
                Me.dgvMovies.Columns("Imdb").Visible = Not CheckColumnHide_Movies("Imdb")
                Me.dgvMovies.Columns("LandscapePath").Visible = Not CheckColumnHide_Movies("LandscapePath")
                Me.dgvMovies.Columns("MPAA").Visible = Not CheckColumnHide_Movies("MPAA")
                Me.dgvMovies.Columns("NfoPath").Visible = Not CheckColumnHide_Movies("NfoPath")
                Me.dgvMovies.Columns("OriginalTitle").Visible = Not CheckColumnHide_Movies("OriginalTitle")
                Me.dgvMovies.Columns("Playcount").Visible = Not CheckColumnHide_Movies("Playcount")
                Me.dgvMovies.Columns("PosterPath").Visible = Not CheckColumnHide_Movies("PosterPath")
                Me.dgvMovies.Columns("Rating").Visible = Not CheckColumnHide_Movies("Rating")
                Me.dgvMovies.Columns("ThemePath").Visible = Not CheckColumnHide_Movies("ThemePath")
                Me.dgvMovies.Columns("TMDB").Visible = Not CheckColumnHide_Movies("TMDB")
                Me.dgvMovies.Columns("TrailerPath").Visible = Not CheckColumnHide_Movies("TrailerPath")
                Me.dgvMovies.Columns("Year").Visible = Not CheckColumnHide_Movies("Year")
            End If

            If Me.dgvMovieSets.RowCount > 0 Then
                Me.dgvMovieSets.Columns("BannerPath").Visible = Not CheckColumnHide_MovieSets("BannerPath")
                Me.dgvMovieSets.Columns("ClearArtPath").Visible = Not CheckColumnHide_MovieSets("ClearArtPath")
                Me.dgvMovieSets.Columns("ClearLogoPath").Visible = Not CheckColumnHide_MovieSets("ClearLogoPath")
                Me.dgvMovieSets.Columns("DiscArtPath").Visible = Not CheckColumnHide_MovieSets("DiscArtPath")
                Me.dgvMovieSets.Columns("FanartPath").Visible = Not CheckColumnHide_MovieSets("FanartPath")
                Me.dgvMovieSets.Columns("LandscapePath").Visible = Not CheckColumnHide_MovieSets("LandscapePath")
                Me.dgvMovieSets.Columns("NfoPath").Visible = Not CheckColumnHide_MovieSets("NfoPath")
                Me.dgvMovieSets.Columns("PosterPath").Visible = Not CheckColumnHide_MovieSets("PosterPath")
            End If

            If Me.dgvTVShows.RowCount > 0 Then
                Me.dgvTVShows.Columns("BannerPath").Visible = Not CheckColumnHide_TVShows("BannerPath")
                Me.dgvTVShows.Columns("CharacterArtPath").Visible = Not CheckColumnHide_TVShows("CharacterArtPath")
                Me.dgvTVShows.Columns("ClearArtPath").Visible = Not CheckColumnHide_TVShows("ClearArtPath")
                Me.dgvTVShows.Columns("ClearLogoPath").Visible = Not CheckColumnHide_TVShows("ClearLogoPath")
                Me.dgvTVShows.Columns("EFanartsPath").Visible = Not CheckColumnHide_TVShows("EFanartsPath")
                Me.dgvTVShows.Columns("FanartPath").Visible = Not CheckColumnHide_TVShows("FanartPath")
                Me.dgvTVShows.Columns("LandscapePath").Visible = Not CheckColumnHide_TVShows("LandscapePath")
                Me.dgvTVShows.Columns("NfoPath").Visible = Not CheckColumnHide_TVShows("NfoPath")
                Me.dgvTVShows.Columns("PosterPath").Visible = Not CheckColumnHide_TVShows("PosterPath")
                Me.dgvTVShows.Columns("Status").Visible = Not CheckColumnHide_TVShows("Status")
                Me.dgvTVShows.Columns("ThemePath").Visible = Not CheckColumnHide_TVShows("ThemePath")
            End If

            If Me.dgvTVSeasons.RowCount > 0 Then
                Me.dgvTVSeasons.Columns("BannerPath").Visible = Not CheckColumnHide_TVSeasons("BannerPath")
                Me.dgvTVSeasons.Columns("FanartPath").Visible = Not CheckColumnHide_TVSeasons("FanartPath")
                Me.dgvTVSeasons.Columns("LandscapePath").Visible = Not CheckColumnHide_TVSeasons("LandscapePath")
                Me.dgvTVSeasons.Columns("PosterPath").Visible = Not CheckColumnHide_TVSeasons("PosterPath")
            End If

            If Me.dgvTVEpisodes.RowCount > 0 Then
                Me.dgvTVEpisodes.Columns("FanartPath").Visible = Not CheckColumnHide_TVEpisodes("FanartPath")
                Me.dgvTVEpisodes.Columns("PosterPath").Visible = Not CheckColumnHide_TVEpisodes("PosterPath")
                Me.dgvTVEpisodes.Columns("HasSub").Visible = Not CheckColumnHide_TVEpisodes("HasSub")
                Me.dgvTVEpisodes.Columns("Playcount").Visible = Not CheckColumnHide_TVEpisodes("Playcount")
            End If

            'might as well wait for these
            While Me.bwMetaData.IsBusy OrElse Me.bwDownloadPic.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            If dresult.NeedsRefresh_Movie OrElse dresult.NeedsRefresh_MovieSet OrElse dresult.NeedsRefresh_TV OrElse dresult.NeedsUpdate Then
                If dresult.NeedsRefresh_Movie Then
                    If Not Me.fScanner.IsBusy Then
                        While Me.bwLoadMovieInfo.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse _
                            Me.bwLoadMovieSetInfo.IsBusy OrElse Me.bwMovieSetScraper.IsBusy OrElse Me.bwReload_MovieSets.IsBusy OrElse _
                            Me.bwLoadEpInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwReload_TVShows.IsBusy OrElse Me.bwCleanDB.IsBusy
                            Application.DoEvents()
                            'Threading.Thread.Sleep(50)
                        End While
                        Me.ReloadAll_Movie()
                    End If
                End If
                If dresult.NeedsRefresh_MovieSet Then
                    If Not Me.fScanner.IsBusy Then
                        While Me.bwLoadMovieInfo.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse _
                            Me.bwLoadMovieSetInfo.IsBusy OrElse Me.bwMovieSetScraper.IsBusy OrElse Me.bwReload_MovieSets.IsBusy OrElse _
                            Me.bwLoadEpInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwReload_TVShows.IsBusy OrElse Me.bwCleanDB.IsBusy
                            Application.DoEvents()
                            'Threading.Thread.Sleep(50)
                        End While
                        Me.ReloadAll_MovieSet()
                    End If
                End If
                If dresult.NeedsRefresh_TV Then
                    If Not Me.fScanner.IsBusy Then
                        While Me.bwLoadMovieInfo.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse _
                            Me.bwLoadMovieSetInfo.IsBusy OrElse Me.bwMovieSetScraper.IsBusy OrElse Me.bwReload_MovieSets.IsBusy OrElse _
                            Me.bwLoadEpInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwReload_TVShows.IsBusy OrElse Me.bwCleanDB.IsBusy
                            Application.DoEvents()
                            'Threading.Thread.Sleep(50)
                        End While
                        Me.ReloadAll_TVShow()
                    End If
                End If
                If dresult.NeedsUpdate Then
                    If Not Me.fScanner.IsBusy Then
                        While Me.bwLoadMovieInfo.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse _
                            Me.bwLoadMovieSetInfo.IsBusy OrElse Me.bwMovieSetScraper.IsBusy OrElse Me.bwReload_MovieSets.IsBusy OrElse _
                            Me.bwLoadEpInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwReload_TVShows.IsBusy OrElse Me.bwCleanDB.IsBusy
                            Application.DoEvents()
                            'Threading.Thread.Sleep(50)
                        End While
                        Me.LoadMedia(New Structures.Scans With {.Movies = True, .TV = True})
                    End If
                End If
            Else
                If Not Me.fScanner.IsBusy AndAlso Not Me.bwLoadMovieInfo.IsBusy AndAlso Not Me.bwMovieScraper.IsBusy AndAlso Not Me.bwReload_Movies.IsBusy AndAlso _
                    Not Me.bwLoadMovieSetInfo.IsBusy AndAlso Not Me.bwMovieSetScraper.IsBusy AndAlso Not Me.bwReload_MovieSets.IsBusy AndAlso _
                    Not Me.bwLoadEpInfo.IsBusy AndAlso Not Me.bwLoadSeasonInfo.IsBusy AndAlso Not Me.bwLoadShowInfo.IsBusy AndAlso Not Me.bwReload_TVShows.IsBusy AndAlso Not Me.bwCleanDB.IsBusy Then
                    Me.FillList(True, True, True)
                End If
            End If

            Me.SetMenus(True)
            If dresult.NeedsRestart Then
                While Me.bwLoadMovieInfo.IsBusy OrElse Me.bwMovieScraper.IsBusy OrElse Me.bwReload_Movies.IsBusy OrElse _
                    Me.bwLoadMovieSetInfo.IsBusy OrElse Me.bwMovieSetScraper.IsBusy OrElse Me.bwReload_MovieSets.IsBusy OrElse _
                    Me.bwLoadEpInfo.IsBusy OrElse Me.bwLoadSeasonInfo.IsBusy OrElse Me.bwLoadShowInfo.IsBusy OrElse Me.bwReload_TVShows.IsBusy OrElse Me.bwCleanDB.IsBusy
                    Application.DoEvents()
                    'Threading.Thread.Sleep(50)
                End While
                Using dRestart As New dlgRestart
                    If dRestart.ShowDialog = Windows.Forms.DialogResult.OK Then
                        Application.Restart()
                    End If
                End Using
            End If
        Else
            Me.SetMenus(False)
            Me.SetControlsEnabled(True)
        End If

    End Sub

    Private Sub mnuMainEditSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainEditSettings.Click, cmnuTraySettings.Click
        Try
            Me.SetControlsEnabled(False)
            Me.pnlLoadSettings.Visible = True

            Dim dThread As Threading.Thread = New Threading.Thread(AddressOf ShowSettings)
            dThread.SetApartmentState(Threading.ApartmentState.STA)
            dThread.Start()
        Catch ex As Exception
            Me.mnuMainEditSettings.Enabled = True
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub UpdateMainTabCounts()
        For Each mTabPage As TabPage In Me.tcMain.Controls
            Dim currTag As Structures.MainTabType = DirectCast(mTabPage.Tag, Structures.MainTabType)
            Dim mCount As Integer = Master.DB.GetViewMediaCount(currTag.DefaultList)
            Select Case currTag.ContentType
                Case Enums.ContentType.Movie, Enums.ContentType.MovieSet
                    If mCount > 0 Then
                        mTabPage.Text = String.Format("{0} ({1})", currTag.ContentName, mCount)
                    Else
                        mTabPage.Text = currTag.ContentName
                    End If
                Case Enums.ContentType.TV
                    If mCount > 0 Then
                        Dim epCount As Integer = Master.DB.GetViewMediaCount(currTag.DefaultList, True)
                        mTabPage.Text = String.Format("{0} ({1}/{2})", currTag.ContentName, mCount, epCount)
                    Else
                        mTabPage.Text = currTag.ContentName
                    End If
            End Select
        Next
    End Sub
    ''' <summary>
    ''' Update the displayed movie counts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMovieCount()
        Dim currTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
        If currTag.ContentType = Enums.ContentType.Movie Then
            If Me.dgvMovies.RowCount > 0 Then
                Me.tcMain.SelectedTab.Text = String.Format("{0} ({1})", currTag.ContentName, Me.dgvMovies.RowCount)
            Else
                Me.tcMain.SelectedTab.Text = currTag.ContentName
            End If
        End If
    End Sub
    ''' <summary>
    ''' Update the displayed movieset counts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMovieSetCount()
        Dim currTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
        If currTag.ContentType = Enums.ContentType.MovieSet Then
            If Me.dgvMovieSets.RowCount > 0 Then
                Me.tcMain.SelectedTab.Text = String.Format("{0} ({1})", currTag.ContentName, Me.dgvMovieSets.RowCount)
            Else
                Me.tcMain.SelectedTab.Text = currTag.ContentName
            End If
        End If
    End Sub
    ''' <summary>
    ''' Update the displayed show/episode counts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTVCount()
        Dim currTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
        If currTag.ContentType = Enums.ContentType.TV Then
            If Me.dgvTVShows.RowCount > 0 Then
                Dim epCount As Integer = Master.DB.GetViewMediaCount(Me.currList_Shows, True)
                Me.tcMain.SelectedTab.Text = String.Format("{0} ({1}/{2})", currTag.ContentName, Me.dgvTVShows.RowCount, epCount)
            Else
                Me.tcMain.SelectedTab.Text = currTag.ContentName
            End If
        End If
    End Sub
    ''' <summary>
    ''' Setup the default/initial text for the GUI's controls. 
    ''' Language used is based on the app's current setting.
    ''' </summary>
    ''' <param name="doTheme"></param>
    ''' <remarks></remarks>
    Private Sub SetUp(ByVal doTheme As Boolean)

        Try
            With Me
                .MinimumSize = New Size(800, 600)

                'All
                Dim strAll As String = Master.eLang.GetString(68, "All")
                .mnuScrapeSubmenuAll.Text = strAll

                'Missing Items
                Dim strMissingItems As String = Master.eLang.GetString(40, "Missing Items")
                .btnFilterMissing_Movies.Text = strMissingItems
                .btnFilterMissing_MovieSets.Text = strMissingItems
                .btnFilterMissing_Shows.Text = strMissingItems
                .mnuScrapeSubmenuMissing.Text = strMissingItems

                'New
                Dim strNew As String = Master.eLang.GetString(47, "New")
                .mnuScrapeSubmenuNew.Text = strNew

                'Marked
                Dim strMarked As String = Master.eLang.GetString(48, "Marked")
                .mnuScrapeSubmenuMarked.Text = strMarked

                'Current Filter
                Dim strCurrentFilter As String = Master.eLang.GetString(624, "Current Filter")
                .mnuScrapeSubmenuFilter.Text = strCurrentFilter

                'Custom Scraper
                Dim strCustomScraper = Master.eLang.GetString(81, "Custom Scraper...")
                .mnuScrapeSubmenuCustom.Text = strCustomScraper

                'Ask (Require Input If No Exact Match)
                Dim strAsk As String = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
                .mnuScrapeTypeAsk.Text = strAsk

                'Automatic (Force Best Match)
                Dim strAutomatic As String = Master.eLang.GetString(69, "Automatic (Force Best Match)")
                .mnuScrapeTypeAuto.Text = strAutomatic

                'Skip (Skip If More Than One Match)
                Dim strSkip As String = Master.eLang.GetString(1041, "Skip (Skip If More Than One Match)")
                .mnuScrapeTypeSkip.Text = strSkip

                'Actor Thumbs Only
                Dim strActorThumbsOnly = Master.eLang.GetString(973, "Actor Thumbs Only")
                .mnuScrapeModifierActorthumbs.Text = strActorThumbsOnly

                'All Items
                Dim strAllItems As String = Master.eLang.GetString(70, "All Items")
                .mnuScrapeModifierAll.Text = strAllItems

                'Banner Only
                Dim strBannerOnly As String = Master.eLang.GetString(1060, "Banner Only")
                .mnuScrapeModifierBanner.Text = strBannerOnly

                'CharacterArt Only
                Dim strCharacterArtOnly As String = Master.eLang.GetString(1121, "CharacterArt Only")
                .mnuScrapeModifierCharacterArt.Text = strCharacterArtOnly

                'ClearArt Only
                Dim strClearArtOnly As String = Master.eLang.GetString(1122, "ClearArt Only")
                .mnuScrapeModifierClearArt.Text = strClearArtOnly

                'ClearLogo Only
                Dim strClearLogoOnly As String = Master.eLang.GetString(1123, "ClearLogo Only")
                .mnuScrapeModifierClearLogo.Text = strClearLogoOnly

                'DiscArt Only
                Dim strDiscArtOnly As String = Master.eLang.GetString(1124, "DiscArt Only")
                .mnuScrapeModifierDiscArt.Text = strDiscArtOnly

                'Edit Season
                Dim strEditSeason As String = Master.eLang.GetString(769, "Edit Season")
                .cmnuSeasonEdit.Text = strEditSeason

                'Extrafanarts Only
                Dim strExtrafanartsOnly As String = Master.eLang.GetString(975, "Extrafanarts Only")
                .mnuScrapeModifierExtrafanarts.Text = strExtrafanartsOnly

                'Extrathumbs Only
                Dim strExtrathumbsOnly As String = Master.eLang.GetString(74, "Extrathumbs Only")
                .mnuScrapeModifierExtrathumbs.Text = strExtrathumbsOnly

                'Fanart Only
                Dim strFanartOnly As String = Master.eLang.GetString(73, "Fanart Only")
                .mnuScrapeModifierFanart.Text = strFanartOnly

                'Landscape Only
                Dim strLandscapeOnly As String = Master.eLang.GetString(1061, "Landscape Only")
                .mnuScrapeModifierLandscape.Text = strLandscapeOnly

                'Meta Data Only
                Dim strMetaDataOnly As String = Master.eLang.GetString(76, "Meta Data Only")
                .mnuScrapeModifierMetaData.Text = strMetaDataOnly

                'NFO Only
                Dim strNFOOnly As String = Master.eLang.GetString(71, "NFO Only")
                .mnuScrapeModifierNFO.Text = strNFOOnly

                'Poster Only
                Dim strPosterOnly As String = Master.eLang.GetString(72, "Poster Only")
                .mnuScrapeModifierPoster.Text = strPosterOnly

                'Reload All Movies
                Dim strReloadAllMovies As String = Master.eLang.GetString(18, "Re&load All Movies")
                .cmnuTrayToolsReloadMovies.Text = strReloadAllMovies
                .mnuMainToolsReloadMovies.Text = strReloadAllMovies

                'Reload All MovieSets
                Dim strReloadAllMovieSets As String = Master.eLang.GetString(1208, "Reload All MovieSets")
                .cmnuTrayToolsReloadMovieSets.Text = strReloadAllMovieSets
                .mnuMainToolsReloadMovieSets.Text = strReloadAllMovieSets

                'Reload All TV Shows
                Dim strReloadAllTVShows As String = Master.eLang.GetString(250, "Reload All TV Shows")
                .cmnuTrayToolsReloadTVShows.Text = strReloadAllTVShows
                .mnuMainToolsReloadTVShows.Text = strReloadAllTVShows

                'Scrape Movies
                Dim strScrapeMovies As String = Master.eLang.GetString(67, "Scrape Movies")
                .mnuScrapeMovies.Text = strScrapeMovies
                .cmnuTrayScrapeMovies.Text = strScrapeMovies

                'Scrape MovieSets
                Dim strScrapeMovieSets As String = Master.eLang.GetString(1213, "Scrape MovieSets")
                .mnuScrapeMovieSets.Text = strScrapeMovieSets
                .cmnuTrayScrapeMovieSets.Text = strScrapeMovieSets

                'Scrape TV Shows
                Dim strScrapeTVShows As String = Master.eLang.GetString(1234, "Scrape TV Shows")
                .mnuScrapeTVShows.Text = strScrapeTVShows
                .cmnuTrayScrapeTVShows.Text = strScrapeTVShows

                'Set
                Dim strSet As String = Master.eLang.GetString(29, "Set")
                .cmnuMovieGenresSet.Text = strSet
                .cmnuMovieLanguageSet.Text = strSet
                .cmnuShowLanguageSet.Text = strSet

                'Theme Only
                Dim strThemeOnly As String = Master.eLang.GetString(1125, "Theme Only")
                .mnuScrapeModifierTheme.Text = strThemeOnly

                'Trailer Only
                Dim strTrailerOnly As String = Master.eLang.GetString(75, "Trailer Only")
                .mnuScrapeModifierTrailer.Text = strTrailerOnly

                ' others
                .btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
                .btnClearFilters_Movies.Text = Master.eLang.GetString(37, "Clear Filters")
                .btnClearFilters_MovieSets.Text = .btnClearFilters_Movies.Text
                .btnClearFilters_Shows.Text = .btnClearFilters_Movies.Text
                .btnMarkAll.Text = Master.eLang.GetString(35, "Mark All")
                .btnMetaDataRefresh.Text = Master.eLang.GetString(58, "Refresh")
                .btnFilterSortDateAdded_Movies.Tag = String.Empty
                .btnFilterSortDateAdded_Movies.Text = Master.eLang.GetString(601, "Date Added")
                .btnFilterSortDateModified_Movies.Tag = String.Empty
                .btnFilterSortDateModified_Movies.Text = Master.eLang.GetString(1330, "Date Modified")
                .btnFilterSortRating_Movies.Tag = String.Empty
                .btnFilterSortRating_Movies.Text = Master.eLang.GetString(400, "Rating")
                .btnFilterSortTitle_Movies.Tag = String.Empty
                .btnFilterSortTitle_Movies.Text = Master.eLang.GetString(642, "Sort Title")
                .btnFilterSortTitle_Shows.Tag = String.Empty
                .btnFilterSortTitle_Shows.Text = Master.eLang.GetString(642, "Sort Title")
                .btnFilterSortYear_Movies.Tag = String.Empty
                .btnFilterSortYear_Movies.Text = Master.eLang.GetString(278, "Year")
                .chkFilterDuplicates_Movies.Text = Master.eLang.GetString(41, "Duplicates")
                .chkFilterEmpty_MovieSets.Text = Master.eLang.GetString(1275, "Empty")
                .chkFilterLock_Movies.Text = Master.eLang.GetString(43, "Locked")
                .chkFilterLock_MovieSets.Text = chkFilterLock_Movies.Text
                .chkFilterLock_Shows.Text = chkFilterLock_Movies.Text
                .chkFilterMark_Movies.Text = Master.eLang.GetString(48, "Marked")
                .chkFilterMark_MovieSets.Text = .chkFilterMark_Movies.Text
                .chkFilterMark_Shows.Text = .chkFilterMark_Movies.Text
                .chkFilterMarkCustom1_Movies.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker1Name), Master.eSettings.MovieGeneralCustomMarker1Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #1"))
                .chkFilterMarkCustom2_Movies.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker2Name), Master.eSettings.MovieGeneralCustomMarker2Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #2"))
                .chkFilterMarkCustom3_Movies.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker3Name), Master.eSettings.MovieGeneralCustomMarker3Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #3"))
                .chkFilterMarkCustom4_Movies.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker4Name), Master.eSettings.MovieGeneralCustomMarker4Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #4"))
                .chkFilterMultiple_MovieSets.Text = Master.eLang.GetString(876, "Multiple Movies")
                .chkFilterNew_Movies.Text = Master.eLang.GetString(47, "New")
                .chkFilterNew_MovieSets.Text = .chkFilterNew_Movies.Text
                .chkFilterNewEpisodes_Shows.Text = Master.eLang.GetString(1361, "New Episode(s)")
                .chkFilterNewShows_Shows.Text = Master.eLang.GetString(1362, "New Show(s)")
                .chkFilterOne_MovieSets.Text = Master.eLang.GetString(1289, "Only One Movie")
                .chkFilterTolerance_Movies.Text = Master.eLang.GetString(39, "Out of Tolerance")

                RemoveHandler Me.chkMovieMissingBanner.CheckedChanged, AddressOf Me.chkMovieMissingBanner_CheckedChanged
                .chkMovieMissingBanner.Checked = Master.eSettings.MovieMissingBanner
                AddHandler Me.chkMovieMissingBanner.CheckedChanged, AddressOf Me.chkMovieMissingBanner_CheckedChanged

                RemoveHandler Me.chkMovieMissingClearArt.CheckedChanged, AddressOf Me.chkMovieMissingClearArt_CheckedChanged
                .chkMovieMissingClearArt.Checked = Master.eSettings.MovieMissingClearArt
                AddHandler Me.chkMovieMissingClearArt.CheckedChanged, AddressOf Me.chkMovieMissingClearArt_CheckedChanged

                RemoveHandler Me.chkMovieMissingClearLogo.CheckedChanged, AddressOf Me.chkMovieMissingClearLogo_CheckedChanged
                .chkMovieMissingClearLogo.Checked = Master.eSettings.MovieMissingClearLogo
                AddHandler Me.chkMovieMissingClearLogo.CheckedChanged, AddressOf Me.chkMovieMissingClearLogo_CheckedChanged

                RemoveHandler Me.chkMovieMissingDiscArt.CheckedChanged, AddressOf Me.chkMovieMissingDiscArt_CheckedChanged
                .chkMovieMissingDiscArt.Checked = Master.eSettings.MovieMissingDiscArt
                AddHandler Me.chkMovieMissingDiscArt.CheckedChanged, AddressOf Me.chkMovieMissingDiscArt_CheckedChanged

                RemoveHandler Me.chkMovieMissingEFanarts.CheckedChanged, AddressOf Me.chkMovieMissingEFanarts_CheckedChanged
                .chkMovieMissingEFanarts.Checked = Master.eSettings.MovieMissingEFanarts
                AddHandler Me.chkMovieMissingEFanarts.CheckedChanged, AddressOf Me.chkMovieMissingEFanarts_CheckedChanged

                RemoveHandler Me.chkMovieMissingEThumbs.CheckedChanged, AddressOf Me.chkMovieMissingEThumbs_CheckedChanged
                .chkMovieMissingEThumbs.Checked = Master.eSettings.MovieMissingEThumbs
                AddHandler Me.chkMovieMissingEThumbs.CheckedChanged, AddressOf Me.chkMovieMissingEThumbs_CheckedChanged

                RemoveHandler Me.chkMovieMissingFanart.CheckedChanged, AddressOf Me.chkMovieMissingFanart_CheckedChanged
                .chkMovieMissingFanart.Checked = Master.eSettings.MovieMissingFanart
                AddHandler Me.chkMovieMissingFanart.CheckedChanged, AddressOf Me.chkMovieMissingFanart_CheckedChanged

                RemoveHandler Me.chkMovieMissingLandscape.CheckedChanged, AddressOf Me.chkMovieMissingLandscape_CheckedChanged
                .chkMovieMissingLandscape.Checked = Master.eSettings.MovieMissingLandscape
                AddHandler Me.chkMovieMissingLandscape.CheckedChanged, AddressOf Me.chkMovieMissingLandscape_CheckedChanged

                RemoveHandler Me.chkMovieMissingNFO.CheckedChanged, AddressOf Me.chkMovieMissingNFO_CheckedChanged
                .chkMovieMissingNFO.Checked = Master.eSettings.MovieMissingNFO
                AddHandler Me.chkMovieMissingNFO.CheckedChanged, AddressOf Me.chkMovieMissingNFO_CheckedChanged

                RemoveHandler Me.chkMovieMissingPoster.CheckedChanged, AddressOf Me.chkMovieMissingPoster_CheckedChanged
                .chkMovieMissingPoster.Checked = Master.eSettings.MovieMissingPoster
                AddHandler Me.chkMovieMissingPoster.CheckedChanged, AddressOf Me.chkMovieMissingPoster_CheckedChanged

                RemoveHandler Me.chkMovieMissingSubtitles.CheckedChanged, AddressOf Me.chkMovieMissingSubtitles_CheckedChanged
                .chkMovieMissingSubtitles.Checked = Master.eSettings.MovieMissingSubtitles
                AddHandler Me.chkMovieMissingSubtitles.CheckedChanged, AddressOf Me.chkMovieMissingSubtitles_CheckedChanged

                RemoveHandler Me.chkMovieMissingTheme.CheckedChanged, AddressOf Me.chkMovieMissingTheme_CheckedChanged
                .chkMovieMissingTheme.Checked = Master.eSettings.MovieMissingTheme
                AddHandler Me.chkMovieMissingTheme.CheckedChanged, AddressOf Me.chkMovieMissingTheme_CheckedChanged

                RemoveHandler Me.chkMovieMissingTrailer.CheckedChanged, AddressOf Me.chkMovieMissingTrailer_CheckedChanged
                .chkMovieMissingTrailer.Checked = Master.eSettings.MovieMissingTrailer
                AddHandler Me.chkMovieMissingTrailer.CheckedChanged, AddressOf Me.chkMovieMissingTrailer_CheckedChanged

                RemoveHandler Me.chkMovieSetMissingBanner.CheckedChanged, AddressOf Me.chkMovieSetMissingBanner_CheckedChanged
                .chkMovieSetMissingBanner.Checked = Master.eSettings.MovieSetMissingBanner
                AddHandler Me.chkMovieSetMissingBanner.CheckedChanged, AddressOf Me.chkMovieSetMissingBanner_CheckedChanged

                RemoveHandler Me.chkMovieSetMissingClearArt.CheckedChanged, AddressOf Me.chkMovieSetMissingClearArt_CheckedChanged
                .chkMovieSetMissingClearArt.Checked = Master.eSettings.MovieSetMissingClearArt
                AddHandler Me.chkMovieSetMissingClearArt.CheckedChanged, AddressOf Me.chkMovieSetMissingClearArt_CheckedChanged

                RemoveHandler Me.chkMovieSetMissingClearLogo.CheckedChanged, AddressOf Me.chkMovieSetMissingClearLogo_CheckedChanged
                .chkMovieSetMissingClearLogo.Checked = Master.eSettings.MovieSetMissingClearLogo
                AddHandler Me.chkMovieSetMissingClearLogo.CheckedChanged, AddressOf Me.chkMovieSetMissingClearLogo_CheckedChanged

                RemoveHandler Me.chkMovieSetMissingDiscArt.CheckedChanged, AddressOf Me.chkMovieSetMissingDiscArt_CheckedChanged
                .chkMovieSetMissingDiscArt.Checked = Master.eSettings.MovieSetMissingDiscArt
                AddHandler Me.chkMovieSetMissingDiscArt.CheckedChanged, AddressOf Me.chkMovieSetMissingDiscArt_CheckedChanged

                RemoveHandler Me.chkMovieSetMissingFanart.CheckedChanged, AddressOf Me.chkMovieSetMissingFanart_CheckedChanged
                .chkMovieSetMissingFanart.Checked = Master.eSettings.MovieSetMissingFanart
                AddHandler Me.chkMovieSetMissingFanart.CheckedChanged, AddressOf Me.chkMovieSetMissingFanart_CheckedChanged

                RemoveHandler Me.chkMovieSetMissingLandscape.CheckedChanged, AddressOf Me.chkMovieSetMissingLandscape_CheckedChanged
                .chkMovieSetMissingLandscape.Checked = Master.eSettings.MovieSetMissingLandscape
                AddHandler Me.chkMovieSetMissingLandscape.CheckedChanged, AddressOf Me.chkMovieSetMissingLandscape_CheckedChanged

                RemoveHandler Me.chkMovieSetMissingNFO.CheckedChanged, AddressOf Me.chkMovieSetMissingNFO_CheckedChanged
                .chkMovieSetMissingNFO.Checked = Master.eSettings.MovieSetMissingNFO
                AddHandler Me.chkMovieSetMissingNFO.CheckedChanged, AddressOf Me.chkMovieSetMissingNFO_CheckedChanged

                RemoveHandler Me.chkMovieSetMissingPoster.CheckedChanged, AddressOf Me.chkMovieSetMissingPoster_CheckedChanged
                .chkMovieSetMissingPoster.Checked = Master.eSettings.MovieSetMissingPoster
                AddHandler Me.chkMovieSetMissingPoster.CheckedChanged, AddressOf Me.chkMovieSetMissingPoster_CheckedChanged

                RemoveHandler Me.chkShowMissingBanner.CheckedChanged, AddressOf Me.chkShowMissingBanner_CheckedChanged
                .chkShowMissingBanner.Checked = Master.eSettings.TVShowMissingBanner
                AddHandler Me.chkShowMissingBanner.CheckedChanged, AddressOf Me.chkShowMissingBanner_CheckedChanged

                RemoveHandler Me.chkShowMissingCharacterArt.CheckedChanged, AddressOf Me.chkShowMissingCharacterArt_CheckedChanged
                .chkShowMissingCharacterArt.Checked = Master.eSettings.TVShowMissingCharacterArt
                AddHandler Me.chkShowMissingCharacterArt.CheckedChanged, AddressOf Me.chkShowMissingCharacterArt_CheckedChanged

                RemoveHandler Me.chkShowMissingClearArt.CheckedChanged, AddressOf Me.chkShowMissingClearArt_CheckedChanged
                .chkShowMissingClearArt.Checked = Master.eSettings.TVShowMissingClearArt
                AddHandler Me.chkShowMissingClearArt.CheckedChanged, AddressOf Me.chkShowMissingClearArt_CheckedChanged

                RemoveHandler Me.chkShowMissingClearLogo.CheckedChanged, AddressOf Me.chkShowMissingClearLogo_CheckedChanged
                .chkShowMissingClearLogo.Checked = Master.eSettings.TVShowMissingClearLogo
                AddHandler Me.chkShowMissingClearLogo.CheckedChanged, AddressOf Me.chkShowMissingClearLogo_CheckedChanged

                RemoveHandler Me.chkShowMissingEFanarts.CheckedChanged, AddressOf Me.chkShowMissingEFanarts_CheckedChanged
                .chkShowMissingEFanarts.Checked = Master.eSettings.TVShowMissingEFanarts
                AddHandler Me.chkShowMissingEFanarts.CheckedChanged, AddressOf Me.chkShowMissingEFanarts_CheckedChanged

                RemoveHandler Me.chkShowMissingFanart.CheckedChanged, AddressOf Me.chkShowMissingFanart_CheckedChanged
                .chkShowMissingFanart.Checked = Master.eSettings.TVShowMissingFanart
                AddHandler Me.chkShowMissingFanart.CheckedChanged, AddressOf Me.chkShowMissingFanart_CheckedChanged

                RemoveHandler Me.chkShowMissingLandscape.CheckedChanged, AddressOf Me.chkShowMissingLandscape_CheckedChanged
                .chkShowMissingLandscape.Checked = Master.eSettings.TVShowMissingLandscape
                AddHandler Me.chkShowMissingLandscape.CheckedChanged, AddressOf Me.chkShowMissingLandscape_CheckedChanged

                RemoveHandler Me.chkShowMissingNFO.CheckedChanged, AddressOf Me.chkShowMissingNFO_CheckedChanged
                .chkShowMissingNFO.Checked = Master.eSettings.TVShowMissingNFO
                AddHandler Me.chkShowMissingNFO.CheckedChanged, AddressOf Me.chkShowMissingNFO_CheckedChanged

                RemoveHandler Me.chkShowMissingPoster.CheckedChanged, AddressOf Me.chkShowMissingPoster_CheckedChanged
                .chkShowMissingPoster.Checked = Master.eSettings.TVShowMissingPoster
                AddHandler Me.chkShowMissingPoster.CheckedChanged, AddressOf Me.chkShowMissingPoster_CheckedChanged

                RemoveHandler Me.chkShowMissingTheme.CheckedChanged, AddressOf Me.chkShowMissingTheme_CheckedChanged
                .chkShowMissingTheme.Checked = Master.eSettings.TVShowMissingTheme
                AddHandler Me.chkShowMissingTheme.CheckedChanged, AddressOf Me.chkShowMissingTheme_CheckedChanged

                .cmnuEpisodeChange.Text = Master.eLang.GetString(772, "Change Episode")
                .cmnuEpisodeEdit.Text = Master.eLang.GetString(656, "Edit Episode")
                .cmnuEpisodeEdit.Text = Master.eLang.GetString(656, "Edit Episode")
                .cmnuEpisodeLock.Text = Master.eLang.GetString(24, "Lock")
                .cmnuEpisodeMark.Text = Master.eLang.GetString(23, "Mark")
                .cmnuEpisodeReload.Text = Master.eLang.GetString(22, "Reload")
                .cmnuEpisodeRemove.Text = Master.eLang.GetString(30, "Remove")
                .cmnuEpisodeRemoveFromDB.Text = Master.eLang.GetString(646, "Remove from Database")
                .cmnuEpisodeRemoveFromDisk.Text = Master.eLang.GetString(773, "Delete Episode")
                .cmnuEpisodeRescrape.Text = Master.eLang.GetString(147, "(Re)Scrape Episode")
                .cmnuMovieBrowseIMDB.Text = Master.eLang.GetString(1281, "Open IMDB-Page")
                .cmnuMovieBrowseTMDB.Text = Master.eLang.GetString(1282, "Open TMDB-Page")
                .cmnuMovieChange.Text = Master.eLang.GetString(32, "Change Movie")
                .cmnuMovieChangeAuto.Text = Master.eLang.GetString(1294, "Change Movie (Auto)")
                .cmnuMovieEdit.Text = Master.eLang.GetString(25, "Edit Movie")
                .cmnuMovieEditMetaData.Text = Master.eLang.GetString(603, "Edit Meta Data")
                .cmnuMovieGenres.Text = Master.eLang.GetString(20, "Genre")
                .cmnuMovieGenresAdd.Text = Master.eLang.GetString(28, "Add")
                .cmnuMovieGenresRemove.Text = Master.eLang.GetString(30, "Remove")
                .cmnuMovieGenresTitle.Text = Master.eLang.GetString(27, ">> Select Genre <<")
                .cmnuMovieLock.Text = Master.eLang.GetString(24, "Lock")
                .cmnuMovieMark.Text = Master.eLang.GetString(23, "Mark")
                .cmnuMovieMarkAs.Text = Master.eLang.GetString(1192, "Mark as")
                .cmnuMovieMarkAsCustom1.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker1Name), Master.eSettings.MovieGeneralCustomMarker1Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #1"))
                .cmnuMovieMarkAsCustom1.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker1Color)
                .cmnuMovieMarkAsCustom2.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker2Name), Master.eSettings.MovieGeneralCustomMarker2Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #2"))
                .cmnuMovieMarkAsCustom2.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker2Color)
                .cmnuMovieMarkAsCustom3.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker3Name), Master.eSettings.MovieGeneralCustomMarker3Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #3"))
                .cmnuMovieMarkAsCustom3.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker3Color)
                .cmnuMovieMarkAsCustom4.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker4Name), Master.eSettings.MovieGeneralCustomMarker4Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #4"))
                .cmnuMovieMarkAsCustom4.ForeColor = System.Drawing.Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker4Color)
                .cmnuMovieOpenFolder.Text = Master.eLang.GetString(33, "Open Containing Folder")
                .cmnuMovieReload.Text = Master.eLang.GetString(22, "Reload")
                .cmnuMovieRemove.Text = Master.eLang.GetString(30, "Remove")
                .cmnuMovieRemoveFromDB.Text = Master.eLang.GetString(646, "Remove From Database")
                .cmnuMovieRemoveFromDisk.Text = Master.eLang.GetString(34, "Delete Movie")
                .cmnuMovieRescrape.Text = Master.eLang.GetString(163, "(Re)Scrape Movie")
                .cmnuMovieRescrapeSelected.Text = Master.eLang.GetString(31, "(Re)Scrape Selected Movies")
                .cmnuMovieSetEdit.Text = Master.eLang.GetString(1131, "Edit MovieSet")
                .cmnuMovieSetNew.Text = Master.eLang.GetString(208, "Add New Set")
                .cmnuMovieSetRescrape.Text = Master.eLang.GetString(1233, "(Re)Scrape MovieSet")
                .cmnuMovieUpSelActors.Text = Master.eLang.GetString(725, "Actors")
                .cmnuMovieUpSelCert.Text = Master.eLang.GetString(56, "Certification")
                .cmnuMovieUpSelCollectionID.Text = Master.eLang.GetString(1135, "Collection ID")
                .cmnuMovieUpSelCountry.Text = Master.eLang.GetString(301, "Country")
                .cmnuMovieUpSelDirector.Text = Master.eLang.GetString(62, "Director")
                .cmnuMovieUpSelGenre.Text = Master.eLang.GetString(20, "Genre")
                .cmnuMovieUpSelMPAA.Text = Master.eLang.GetString(401, "MPAA")
                .cmnuMovieUpSelOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
                .cmnuMovieUpSelOutline.Text = Master.eLang.GetString(64, "Plot Outline")
                .cmnuMovieUpSelPlot.Text = Master.eLang.GetString(65, "Plot")
                .cmnuMovieUpSelProducers.Text = Master.eLang.GetString(393, "Producers")
                .cmnuMovieUpSelRating.Text = String.Concat(Master.eLang.GetString(400, "Rating"), " / ", Master.eLang.GetString(399, "Votes"))
                .cmnuMovieUpSelRelease.Text = Master.eLang.GetString(57, "Release Date")
                .cmnuMovieUpSelRuntime.Text = Master.eLang.GetString(396, "Runtime")
                .cmnuMovieUpSelStudio.Text = Master.eLang.GetString(395, "Studio")
                .cmnuMovieUpSelTagline.Text = Master.eLang.GetString(397, "Tagline")
                .cmnuMovieUpSelTitle.Text = Master.eLang.GetString(21, "Title")
                .cmnuMovieUpSelTop250.Text = Master.eLang.GetString(591, "Top 250")
                .cmnuMovieUpSelTrailer.Text = Master.eLang.GetString(151, "Trailer")
                .cmnuMovieUpSelWriter.Text = Master.eLang.GetString(777, "Writer")
                .cmnuMovieUpSelYear.Text = Master.eLang.GetString(278, "Year")
                .cmnuMovieTitle.Text = Master.eLang.GetString(21, "Title")
                .cmnuMovieUpSel.Text = Master.eLang.GetString(1126, "Update Single Data Field")
                .cmnuSeasonRemoveFromDB.Text = Master.eLang.GetString(646, "Remove from Database")
                .cmnuSeasonLock.Text = Master.eLang.GetString(24, "Lock")
                .cmnuSeasonMark.Text = Master.eLang.GetString(23, "Mark")
                .cmnuSeasonReload.Text = Master.eLang.GetString(22, "Reload")
                .cmnuSeasonRemove.Text = Master.eLang.GetString(30, "Remove")
                .cmnuSeasonRemoveFromDisk.Text = Master.eLang.GetString(771, "Delete Season")
                .cmnuSeasonRescrape.Text = Master.eLang.GetString(146, "(Re)Scrape Season")
                .cmnuShowChange.Text = Master.eLang.GetString(767, "Change Show")
                .cmnuShowClearCache.Text = Master.eLang.GetString(565, "Clear Cache")
                .cmnuShowClearCacheDataAndImages.Text = Master.eLang.GetString(583, "Data and Images")
                .cmnuShowClearCacheDataOnly.Text = Master.eLang.GetString(566, "Data Only")
                .cmnuShowClearCacheImagesOnly.Text = Master.eLang.GetString(567, "Images Only")
                .cmnuShowEdit.Text = Master.eLang.GetString(663, "Edit Show")
                .cmnuShowEdit.Text = Master.eLang.GetString(663, "Edit Show")
                .cmnuShowLanguage.Text = Master.eLang.GetString(1200, "Change Language")
                .cmnuShowLock.Text = Master.eLang.GetString(24, "Lock")
                .cmnuShowMark.Text = Master.eLang.GetString(23, "Mark")
                .cmnuShowReload.Text = Master.eLang.GetString(22, "Reload")
                .cmnuShowRemove.Text = Master.eLang.GetString(30, "Remove")
                .cmnuShowRemoveFromDB.Text = Master.eLang.GetString(646, "Remove from Database")
                .cmnuShowRemoveFromDisk.Text = Master.eLang.GetString(768, "Delete TV Show")
                .cmnuShowRefresh.Text = Master.eLang.GetString(1066, "Refresh Data")
                .cmnuShowRescrape.Text = Master.eLang.GetString(766, "(Re)Scrape Show")
                .cmnuTrayExit.Text = Master.eLang.GetString(2, "E&xit")
                .cmnuTraySettings.Text = Master.eLang.GetString(4, "&Settings...")
                .cmnuTrayTools.Text = Master.eLang.GetString(8, "&Tools")
                .cmnuTrayUpdate.Text = Master.eLang.GetString(82, "Update Library")
                .gbFilterDataField_Movies.Text = String.Concat(Master.eLang.GetString(1290, "Data Field"), ":")
                .gbFilterGeneral_Movies.Text = Master.eLang.GetString(38, "General")
                .gbFilterGeneral_MovieSets.Text = .gbFilterGeneral_Movies.Text
                .gbFilterGeneral_Shows.Text = Master.eLang.GetString(680, "Shows")
                .gbFilterModifier_Movies.Text = Master.eLang.GetString(44, "Modifier")
                .gbFilterModifier_MovieSets.Text = .gbFilterModifier_Movies.Text
                .gbFilterModifier_Shows.Text = .gbFilterModifier_Movies.Text
                .gbFilterSpecific_Movies.Text = Master.eLang.GetString(42, "Specific")
                .gbFilterSpecific_MovieSets.Text = .gbFilterSpecific_Movies.Text
                .gbFilterSpecific_Shows.Text = .gbFilterSpecific_Movies.Text
                .gbFilterSorting_Movies.Text = Master.eLang.GetString(600, "Extra Sorting")
                .gbFilterSorting_Shows.Text = .gbFilterSorting_Movies.Text
                .lblActorsHeader.Text = Master.eLang.GetString(63, "Cast")
                .lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
                .lblCertsHeader.Text = Master.eLang.GetString(56, "Certification(s)")
                .lblCharacterArtTitle.Text = Master.eLang.GetString(1140, "CharacterArt")
                .lblClearArtTitle.Text = Master.eLang.GetString(1096, "ClearArt")
                .lblClearLogoTitle.Text = Master.eLang.GetString(1097, "ClearLogo")
                .lblDirectorHeader.Text = Master.eLang.GetString(62, "Director")
                .lblDiscArtTitle.Text = Master.eLang.GetString(1098, "DiscArt")
                .lblFanartSmallTitle.Text = Master.eLang.GetString(149, "Fanart")
                .lblFilePathHeader.Text = Master.eLang.GetString(60, "File Path")
                .lblFilter_Movies.Text = Master.eLang.GetString(52, "Filters")
                .lblFilter_MovieSets.Text = .lblFilter_Movies.Text
                .lblFilter_Shows.Text = .lblFilter_Movies.Text
                .lblFilterCountries_Movies.Text = Master.eLang.GetString(301, "Country")
                .lblFilterCountriesClose_Movies.Text = Master.eLang.GetString(19, "Close")
                .lblFilterCountry_Movies.Text = String.Concat(Master.eLang.GetString(301, "Country"), ":")
                .lblFilterVideoSource_Movies.Text = Master.eLang.GetString(824, "Video Source:")
                .lblFilterGenre_Movies.Text = Master.eLang.GetString(51, "Genre:")
                .lblFilterGenre_Shows.Text = .lblFilterGenre_Movies.Text
                .lblFilterGenres_Movies.Text = Master.eLang.GetString(20, "Genre")
                .lblFilterGenres_Shows.Text = .lblFilterGenres_Movies.Text
                .lblFilterSource_Movies.Text = Master.eLang.GetString(50, "Source:")
                .lblFilterSource_Shows.Text = .lblFilterSource_Movies.Text
                .lblFilterSources_Movies.Text = Master.eLang.GetString(602, "Sources")
                .lblFilterSources_Shows.Text = .lblFilterSources_Movies.Text
                .lblFilterYear_Movies.Text = Master.eLang.GetString(49, "Year:")
                .lblFilterGenresClose_Movies.Text = Master.eLang.GetString(19, "Close")
                .lblFilterGenresClose_Shows.Text = .lblFilterGenresClose_Movies.Text
                .lblFilterDataFields_Movies.Text = Master.eLang.GetString(1290, "Data Field")
                .lblFilterDataFieldsClose_Movies.Text = .lblFilterGenresClose_Movies.Text
                .lblFilterSourcesClose_Movies.Text = .lblFilterGenresClose_Movies.Text
                .lblFilterSourcesClose_Shows.Text = .lblFilterGenresClose_Movies.Text
                .lblIMDBHeader.Text = Master.eLang.GetString(61, "IMDB ID")
                .lblInfoPanelHeader.Text = Master.eLang.GetString(66, "Info")
                .lblLandscapeTitle.Text = Master.eLang.GetString(1035, "Landscape")
                .lblLoadSettings.Text = Master.eLang.GetString(484, "Loading Settings...")
                .lblMetaDataHeader.Text = Master.eLang.GetString(59, "Meta Data")
                .lblMoviesInSetHeader.Text = Master.eLang.GetString(367, "Movies In Set")
                .lblOutlineHeader.Text = Master.eLang.GetString(64, "Plot Outline")
                .lblPlotHeader.Text = Master.eLang.GetString(65, "Plot")
                .lblPosterTitle.Text = Master.eLang.GetString(148, "Poster")
                .lblReleaseDateHeader.Text = Master.eLang.GetString(57, "Release Date")
                .mnuMainDonate.Text = Master.eLang.GetString(708, "Donate")
                .mnuMainDonate.Text = Master.eLang.GetString(708, "Donate")
                .mnuMainEdit.Text = Master.eLang.GetString(3, "&Edit")
                .mnuMainEditSettings.Text = Master.eLang.GetString(4, "&Settings...")
                .mnuMainFile.Text = Master.eLang.GetString(1, "&File")
                .mnuMainFileExit.Text = Master.eLang.GetString(2, "E&xit")
                .mnuMainHelp.Text = Master.eLang.GetString(5, "&Help")
                .mnuMainHelpAbout.Text = Master.eLang.GetString(6, "&About...")
                .mnuMainHelpUpdate.Text = Master.eLang.GetString(850, "&Check For Updates...")
                .mnuMainHelpVersions.Text = Master.eLang.GetString(793, "&Versions...")
                .mnuMainHelpWiki.Text = Master.eLang.GetString(869, "EmberMM.com &Wiki...")
                .mnuMainToolsExport.Text = Master.eLang.GetString(1174, "Export")
                .mnuMainToolsExportMovies.Text = Master.eLang.GetString(36, "Movies")
                .mnuMainToolsExportTvShows.Text = Master.eLang.GetString(653, "TV Shows")
                .mnuMainTools.Text = Master.eLang.GetString(8, "&Tools")
                .mnuMainToolsBackdrops.Text = Master.eLang.GetString(11, "Copy Existing Fanart To &Backdrops Folder")
                .mnuMainToolsCleanDB.Text = Master.eLang.GetString(709, "Clean &Database")
                .mnuMainToolsCleanFiles.Text = Master.eLang.GetString(9, "&Clean Files")
                .mnuMainToolsClearCache.Text = Master.eLang.GetString(17, "Clear &All Caches")
                .mnuMainToolsOfflineHolder.Text = Master.eLang.GetString(524, "&Offline Media Manager")
                .mnuMainToolsRewriteMovieContent.Text = Master.eLang.GetString(1298, "Rewrite All Movie Content")
                .mnuMainToolsSortFiles.Text = Master.eLang.GetString(10, "&Sort Files Into Folders")
                .mnuUpdate.Text = Master.eLang.GetString(82, "Update Library")
                .mnuUpdateMovies.Text = Master.eLang.GetString(36, "Movies")
                .mnuUpdateShows.Text = Master.eLang.GetString(653, "TV Shows")
                .pnlFilterCountries_Movies.Tag = String.Empty
                .pnlFilterGenres_Movies.Tag = String.Empty
                .pnlFilterGenres_Shows.Tag = String.Empty
                .pnlFilterDataFields_Movies.Tag = String.Empty
                .pnlFilterSources_Movies.Tag = String.Empty
                .pnlFilterSources_Shows.Tag = String.Empty
                .rbFilterAnd_Movies.Text = Master.eLang.GetString(45, "And")
                .rbFilterAnd_MovieSets.Text = .rbFilterAnd_Movies.Text
                .rbFilterAnd_Shows.Text = .rbFilterAnd_Movies.Text
                .rbFilterOr_Movies.Text = Master.eLang.GetString(46, "Or")
                .rbFilterOr_MovieSets.Text = .rbFilterOr_Movies.Text
                .rbFilterOr_Shows.Text = .rbFilterOr_Movies.Text
                .tsbMediaCenters.Text = Master.eLang.GetString(83, "Media Centers")
                .tslLoading.Text = Master.eLang.GetString(7, "Loading Media:")

                .cmnuEpisodeOpenFolder.Text = .cmnuMovieOpenFolder.Text
                .cmnuMovieSetLock.Text = .cmnuMovieLock.Text
                .cmnuMovieSetMark.Text = .cmnuMovieMark.Text
                .cmnuMovieSetReload.Text = .cmnuMovieReload.Text
                .cmnuMovieSetRemove.Text = .cmnuMovieRemove.Text
                .cmnuSeasonOpenFolder.Text = .cmnuMovieOpenFolder.Text
                .cmnuShowOpenFolder.Text = .cmnuMovieOpenFolder.Text
                .cmnuTrayToolsBackdrops.Text = .mnuMainToolsBackdrops.Text
                .cmnuTrayToolsCleanFiles.Text = .mnuMainToolsCleanFiles.Text
                .cmnuTrayToolsClearCache.Text = .mnuMainToolsClearCache.Text
                .cmnuTrayToolsOfflineHolder.Text = .mnuMainToolsOfflineHolder.Text
                .cmnuTrayToolsSortFiles.Text = .mnuMainToolsSortFiles.Text

                Dim TT As ToolTip = New System.Windows.Forms.ToolTip(.components)
                .mnuScrapeMovies.ToolTipText = Master.eLang.GetString(84, "Scrape/download data from the internet for multiple movies.")
                .mnuScrapeMovieSets.ToolTipText = Master.eLang.GetString(1214, "Scrape/download data from the internet for multiple moviesets.")
                .mnuScrapeTVShows.ToolTipText = Master.eLang.GetString(1235, "Scrape/download data from the internet for multiple tv shows.")
                .mnuUpdate.ToolTipText = Master.eLang.GetString(85, "Scans sources for new content and cleans database.")
                TT.SetToolTip(.btnMarkAll, Master.eLang.GetString(87, "Mark or Unmark all movies in the list."))
                TT.SetToolTip(.txtSearchMovies, Master.eLang.GetString(88, "Search the movie titles by entering text here."))
                TT.SetToolTip(.txtSearchMovieSets, Master.eLang.GetString(1267, "Search the movie titles by entering text here."))
                TT.SetToolTip(.txtSearchShows, Master.eLang.GetString(1268, "Search the tv show titles by entering text here."))
                TT.SetToolTip(.btnPlay, Master.eLang.GetString(89, "Play the movie file with the system default media player."))
                TT.SetToolTip(.btnMetaDataRefresh, Master.eLang.GetString(90, "Rescan and save the meta data for the selected movie."))
                TT.SetToolTip(.chkFilterDuplicates_Movies, Master.eLang.GetString(91, "Display only movies that have duplicate IMDB IDs."))
                TT.SetToolTip(.chkFilterTolerance_Movies, Master.eLang.GetString(92, "Display only movies whose title matching is out of tolerance."))
                TT.SetToolTip(.chkFilterMissing_Movies, Master.eLang.GetString(93, "Display only movies that have items missing."))
                TT.SetToolTip(.chkFilterNew_Movies, Master.eLang.GetString(94, "Display only new movies."))
                TT.SetToolTip(.chkFilterNew_MovieSets, Master.eLang.GetString(1269, "Display only new moviesets."))
                TT.SetToolTip(.chkFilterMark_Movies, Master.eLang.GetString(95, "Display only marked movies."))
                TT.SetToolTip(.chkFilterMark_MovieSets, Master.eLang.GetString(1270, "Display only marked moviesets."))
                TT.SetToolTip(.chkFilterLock_Movies, Master.eLang.GetString(96, "Display only locked movies."))
                TT.SetToolTip(.chkFilterLock_MovieSets, Master.eLang.GetString(1271, "Display only locked moviesets."))
                TT.SetToolTip(.txtFilterSource_Movies, Master.eLang.GetString(97, "Display only movies from the selected source."))
                TT.SetToolTip(.cbFilterVideoSource_Movies, Master.eLang.GetString(580, "Display only movies from the selected video source."))
                TT.Active = True

                RemoveHandler Me.cbSearchMovies.SelectedIndexChanged, AddressOf Me.cbSearchMovies_SelectedIndexChanged
                .cbSearchMovies.Items.Clear()
                .cbSearchMovies.Items.AddRange(New Object() {Master.eLang.GetString(21, "Title"), Master.eLang.GetString(302, "Original Title"), Master.eLang.GetString(100, "Actor"), Master.eLang.GetString(233, "Role"), Master.eLang.GetString(62, "Director"), Master.eLang.GetString(729, "Credits"), Master.eLang.GetString(301, "Country"), Master.eLang.GetString(395, "Studio")})
                If Me.cbSearchMovies.Items.Count > 0 Then
                    Me.cbSearchMovies.SelectedIndex = 0
                End If
                AddHandler Me.cbSearchMovies.SelectedIndexChanged, AddressOf Me.cbSearchMovies_SelectedIndexChanged

                RemoveHandler Me.cbSearchMovieSets.SelectedIndexChanged, AddressOf Me.cbSearchMovieSets_SelectedIndexChanged
                .cbSearchMovieSets.Items.Clear()
                .cbSearchMovieSets.Items.AddRange(New Object() {Master.eLang.GetString(21, "Title")})
                If Me.cbSearchMovieSets.Items.Count > 0 Then
                    Me.cbSearchMovieSets.SelectedIndex = 0
                End If
                AddHandler Me.cbSearchMovieSets.SelectedIndexChanged, AddressOf Me.cbSearchMovieSets_SelectedIndexChanged

                RemoveHandler Me.cbSearchShows.SelectedIndexChanged, AddressOf Me.cbSearchShows_SelectedIndexChanged
                .cbSearchShows.Items.Clear()
                .cbSearchShows.Items.AddRange(New Object() {Master.eLang.GetString(21, "Title")})
                If Me.cbSearchShows.Items.Count > 0 Then
                    Me.cbSearchShows.SelectedIndex = 0
                End If
                AddHandler Me.cbSearchShows.SelectedIndexChanged, AddressOf Me.cbSearchShows_SelectedIndexChanged

                If doTheme Then
                    Me.tTheme = New Theming
                    Dim currMainTabTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
                    .ApplyTheme(If(currMainTabTag.ContentType = Enums.ContentType.Movie, Theming.ThemeType.Movie, If(currMainTabTag.ContentType = Enums.ContentType.MovieSet, Theming.ThemeType.MovieSet, Theming.ThemeType.Show)))
                End If

            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Updates the label indicating there is no information for the current item.
    ''' </summary>
    ''' <param name="ShowIt"><c>Boolean</c> indicating whether the panel should be shown or not</param>
    ''' <param name="tType"></param>
    ''' <remarks></remarks>
    Private Sub ShowNoInfo(ByVal ShowIt As Boolean, Optional ByVal tType As Enums.ContentType = Enums.ContentType.Movie)
        If ShowIt Then
            Select Case tType
                Case Enums.ContentType.Movie
                    Me.lblNoInfo.Text = Master.eLang.GetString(55, "No information is available for this Movie")
                    If Not Me.currThemeType = Theming.ThemeType.Movie Then Me.ApplyTheme(Theming.ThemeType.Movie)
                Case Enums.ContentType.MovieSet
                    Me.lblNoInfo.Text = Master.eLang.GetString(1154, "No information is available for this MovieSet")
                    If Not Me.currThemeType = Theming.ThemeType.MovieSet Then Me.ApplyTheme(Theming.ThemeType.MovieSet)
                Case Enums.ContentType.TVEpisode
                    Me.lblNoInfo.Text = Master.eLang.GetString(652, "No information is available for this Episode")
                    If Not Me.currThemeType = Theming.ThemeType.Episode Then Me.ApplyTheme(Theming.ThemeType.Episode)
                Case Enums.ContentType.TVSeason
                    Me.lblNoInfo.Text = Master.eLang.GetString(1161, "No information is available for this Season")
                    If Not Me.currThemeType = Theming.ThemeType.Show Then Me.ApplyTheme(Theming.ThemeType.Show)
                Case Enums.ContentType.TVShow
                    Me.lblNoInfo.Text = Master.eLang.GetString(651, "No information is available for this Show")
                    If Not Me.currThemeType = Theming.ThemeType.Show Then Me.ApplyTheme(Theming.ThemeType.Show)
                Case Else
                    logger.Warn("Invalid media type <{0}>", tType)
            End Select
        End If

        Me.pnlNoInfo.Visible = ShowIt
    End Sub
    ''' <summary>
    ''' Triggers the display of the Settings dialog
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowSettings()
        Using dSettings As New dlgSettings
            Me.Invoke(New MySettingsShow(AddressOf SettingsShow), dSettings)
        End Using
    End Sub

    Private Sub SourceSubClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SourceName As String = DirectCast(sender, ToolStripItem).Tag.ToString

        'Remove any previous scrape as there is no warranty that the new dataset will match with the old one
        Dim aPath As String = FileUtils.Common.ReturnSettingsFile("Settings", "ScraperStatus.dat")
        If File.Exists(aPath) Then
            Try
                File.Delete(aPath)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Throw
            End Try
        End If

        Me.LoadMedia(New Structures.Scans With {.Movies = True}, SourceName)
    End Sub

    Private Sub tcMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tcMain.SelectedIndexChanged
        Me.ClearInfo()
        Me.ShowNoInfo(False)
        Dim currMainTabTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
        ModulesManager.Instance.RuntimeObjects.MediaTabSelected = currMainTabTag
        Select Case currMainTabTag.ContentType
            Case Enums.ContentType.Movie
                Me.currList_Movies = currMainTabTag.DefaultList
                Me.cbFilterLists_Movies.SelectedValue = Me.currList_Movies
                ModulesManager.Instance.RuntimeObjects.ListMovies = Me.currList_Movies
                Me.FillList(True, False, False)
                Me.mnuMainTools.Enabled = True
                Me.cmnuTrayTools.Enabled = True
                Me.mnuScrapeMovies.Visible = True
                Me.mnuScrapeMovieSets.Visible = False
                Me.mnuScrapeTVShows.Visible = False
                Me.pnlFilter_Movies.Visible = True
                Me.pnlFilter_MovieSets.Visible = False
                Me.pnlFilter_Shows.Visible = False
                Me.pnlFilterMissingItems_MovieSets.Visible = False
                Me.pnlFilterMissingItems_Shows.Visible = False
                Me.pnlListTop.Height = 56
                Me.pnlSearchMovies.Visible = True
                Me.pnlSearchMovieSets.Visible = False
                Me.pnlSearchTVShows.Visible = False
                Me.btnMarkAll.Visible = True
                Me.scTV.Visible = False
                Me.dgvMovieSets.Visible = False
                Me.dgvMovies.Visible = True
                Me.ApplyTheme(Theming.ThemeType.Movie)
                If Me.bwLoadEpInfo.IsBusy Then Me.bwLoadEpInfo.CancelAsync()
                If Me.bwLoadSeasonInfo.IsBusy Then Me.bwLoadSeasonInfo.CancelAsync()
                If Me.bwLoadShowInfo.IsBusy Then Me.bwLoadShowInfo.CancelAsync()
                If Me.bwLoadMovieSetInfo.IsBusy Then Me.bwLoadMovieSetInfo.CancelAsync()
                If Me.bwLoadMovieSetPosters.IsBusy Then Me.bwLoadMovieSetPosters.CancelAsync()
                If Me.bwDownloadPic.IsBusy Then Me.bwDownloadPic.CancelAsync()
                If Me.dgvMovies.RowCount > 0 Then
                    Me.prevRow_Movie = -1

                    Me.dgvMovies.CurrentCell = Nothing
                    Me.dgvMovies.ClearSelection()
                    Me.dgvMovies.Rows(0).Selected = True
                    Me.dgvMovies.CurrentCell = Me.dgvMovies.Rows(0).Cells("ListTitle")

                    Me.dgvMovies.Focus()
                Else
                    Me.SetControlsEnabled(True)
                End If

            Case Enums.ContentType.MovieSet
                Me.currList_MovieSets = currMainTabTag.DefaultList
                Me.cbFilterLists_MovieSets.SelectedValue = Me.currList_MovieSets
                ModulesManager.Instance.RuntimeObjects.ListMovieSets = Me.currList_MovieSets
                Me.FillList(False, True, False)
                Me.mnuMainTools.Enabled = True
                Me.cmnuTrayTools.Enabled = True
                Me.mnuScrapeMovies.Visible = False
                Me.mnuScrapeMovieSets.Visible = True
                Me.mnuScrapeTVShows.Visible = False
                Me.pnlFilter_Movies.Visible = False
                Me.pnlFilter_MovieSets.Visible = True
                Me.pnlFilter_Shows.Visible = False
                Me.pnlFilterMissingItems_Movies.Visible = False
                Me.pnlFilterMissingItems_Shows.Visible = False
                Me.pnlListTop.Height = 56
                Me.pnlSearchMovies.Visible = False
                Me.pnlSearchMovieSets.Visible = True
                Me.pnlSearchTVShows.Visible = False
                Me.btnMarkAll.Visible = False
                Me.scTV.Visible = False
                Me.dgvMovies.Visible = False
                Me.dgvMovieSets.Visible = True
                Me.ApplyTheme(Theming.ThemeType.MovieSet)
                If Me.bwLoadMovieInfo.IsBusy Then Me.bwLoadMovieInfo.CancelAsync()
                If Me.bwDownloadPic.IsBusy Then Me.bwDownloadPic.CancelAsync()
                If Me.bwLoadEpInfo.IsBusy Then Me.bwLoadEpInfo.CancelAsync()
                If Me.bwLoadSeasonInfo.IsBusy Then Me.bwLoadSeasonInfo.CancelAsync()
                If Me.bwLoadShowInfo.IsBusy Then Me.bwLoadShowInfo.CancelAsync()
                If Me.dgvMovieSets.RowCount > 0 Then
                    Me.prevRow_MovieSet = -1

                    Me.dgvMovieSets.CurrentCell = Nothing
                    Me.dgvMovieSets.ClearSelection()
                    Me.dgvMovieSets.Rows(0).Selected = True
                    Me.dgvMovieSets.CurrentCell = Me.dgvMovieSets.Rows(0).Cells("ListTitle")

                    Me.dgvMovieSets.Focus()
                Else
                    Me.SetControlsEnabled(True)
                End If

            Case Enums.ContentType.TV
                Me.currList_Shows = currMainTabTag.DefaultList
                Me.cbFilterLists_Shows.SelectedValue = Me.currList_Shows
                ModulesManager.Instance.RuntimeObjects.ListShows = Me.currList_Shows
                Me.FillList(False, False, True)
                Me.mnuMainTools.Enabled = True
                Me.cmnuTrayTools.Enabled = True
                Me.mnuScrapeMovies.Visible = False
                Me.mnuScrapeMovieSets.Visible = False
                Me.mnuScrapeTVShows.Visible = True
                Me.dgvMovies.Visible = False
                Me.dgvMovieSets.Visible = False
                Me.pnlFilter_Movies.Visible = False
                Me.pnlFilter_MovieSets.Visible = False
                Me.pnlFilter_Shows.Visible = True
                Me.pnlFilterMissingItems_Movies.Visible = False
                Me.pnlFilterMissingItems_MovieSets.Visible = False
                Me.pnlListTop.Height = 56
                Me.pnlSearchMovies.Visible = False
                Me.pnlSearchMovieSets.Visible = False
                Me.pnlSearchTVShows.Visible = True
                Me.btnMarkAll.Visible = False
                Me.scTV.Visible = True
                Me.ApplyTheme(Theming.ThemeType.Show)
                If Me.bwLoadMovieInfo.IsBusy Then Me.bwLoadMovieInfo.CancelAsync()
                If Me.bwLoadMovieSetInfo.IsBusy Then Me.bwLoadMovieSetInfo.CancelAsync()
                If Me.bwLoadMovieSetPosters.IsBusy Then Me.bwLoadMovieSetPosters.CancelAsync()
                If Me.bwDownloadPic.IsBusy Then Me.bwDownloadPic.CancelAsync()
                If Me.dgvTVShows.RowCount > 0 Then
                    Me.prevRow_TVShow = -1
                    Me.currList = 0

                    Me.dgvTVShows.CurrentCell = Nothing
                    Me.dgvTVShows.ClearSelection()
                    Me.dgvTVShows.Rows(0).Selected = True
                    Me.dgvTVShows.CurrentCell = Me.dgvTVShows.Rows(0).Cells("ListTitle")

                    Me.dgvTVShows.Focus()
                Else
                    Me.SetControlsEnabled(True)
                End If
        End Select
    End Sub

    Private Sub tmrAni_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrAni.Tick
        Try
            Dim currMainTabTag As Structures.MainTabType = DirectCast(Me.tcMain.SelectedTab.Tag, Structures.MainTabType)
            Select Case If(currMainTabTag.ContentType = Enums.ContentType.Movie, Me.MovieInfoPanelState, If(currMainTabTag.ContentType = Enums.ContentType.MovieSet, Me.MovieSetInfoPanelState, Me.TVShowInfoPanelState))
                Case 0
                    Me.pnlInfoPanel.Height = 25

                Case 1
                    Me.pnlInfoPanel.Height = Me.IPMid

                Case 2
                    Me.pnlInfoPanel.Height = Me.IPUp
            End Select

            Me.MoveGenres()
            Me.MoveMPAA()

            Dim aType As Integer = If(currMainTabTag.ContentType = Enums.ContentType.Movie, Me.MovieInfoPanelState, If(currMainTabTag.ContentType = Enums.ContentType.MovieSet, Me.MovieSetInfoPanelState, Me.TVShowInfoPanelState))
            Select Case aType
                Case 0
                    If Me.pnlInfoPanel.Height = 25 Then
                        Me.tmrAni.Stop()
                        Me.btnDown.Enabled = False
                        Me.btnMid.Enabled = True
                        Me.btnUp.Enabled = True
                    End If
                Case 1
                    If Me.pnlInfoPanel.Height = Me.IPMid Then
                        Me.tmrAni.Stop()
                        Me.btnMid.Enabled = False
                        Me.btnDown.Enabled = True
                        Me.btnUp.Enabled = True
                    End If
                Case 2
                    If Me.pnlInfoPanel.Height = Me.IPUp Then
                        Me.tmrAni.Stop()
                        Me.btnUp.Enabled = False
                        Me.btnDown.Enabled = True
                        Me.btnMid.Enabled = True
                    End If
            End Select

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub FilterMovement_Movies()
        If Me.FilterRaise_Movies Then
            Me.pnlFilter_Movies.AutoSize = True
        Else
            Me.pnlFilter_Movies.AutoSize = False
            Me.pnlFilter_Movies.Height = Me.pnlFilterTop_Movies.Height
        End If

        If Me.pnlFilter_Movies.Height = Me.pnlFilterTop_Movies.Height Then
            Me.btnFilterUp_Movies.Enabled = True
            Me.btnFilterDown_Movies.Enabled = False
        ElseIf Me.pnlFilter_Movies.AutoSize Then
            Me.btnFilterUp_Movies.Enabled = False
            Me.btnFilterDown_Movies.Enabled = True
        End If

        Me.dgvMovies.Invalidate()
    End Sub

    Private Sub FilterMovement_MovieSets()
        If Me.FilterRaise_MovieSets Then
            Me.pnlFilter_MovieSets.AutoSize = True
        Else
            Me.pnlFilter_MovieSets.AutoSize = False
            Me.pnlFilter_MovieSets.Height = Me.pnlFilterTop_MovieSets.Height
        End If

        If Me.pnlFilter_MovieSets.Height = Me.pnlFilterTop_MovieSets.Height Then
            Me.btnFilterUp_MovieSets.Enabled = True
            Me.btnFilterDown_MovieSets.Enabled = False
        ElseIf Me.pnlFilter_MovieSets.AutoSize Then
            Me.btnFilterUp_MovieSets.Enabled = False
            Me.btnFilterDown_MovieSets.Enabled = True
        End If

        Me.dgvMovieSets.Invalidate()
    End Sub

    Private Sub FilterMovement_Shows()
        If Me.FilterRaise_Shows Then
            Me.pnlFilter_Shows.AutoSize = True
        Else
            Me.pnlFilter_Shows.AutoSize = False
            Me.pnlFilter_Shows.Height = Me.pnlFilterTop_Shows.Height
        End If

        If Me.pnlFilter_Shows.Height = Me.pnlFilterTop_Shows.Height Then
            Me.btnFilterUp_Shows.Enabled = True
            Me.btnFilterDown_Shows.Enabled = False
        ElseIf Me.pnlFilter_Shows.AutoSize Then
            Me.btnFilterUp_Shows.Enabled = False
            Me.btnFilterDown_Shows.Enabled = True
        End If

        Me.dgvTVShows.Invalidate()
    End Sub

    Private Sub tmrLoad_TVEpisode_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoad_TVEpisode.Tick
        Me.tmrWait_TVEpisode.Stop()
        Me.tmrLoad_TVEpisode.Stop()

        If Me.dgvTVEpisodes.SelectedRows.Count > 0 Then

            If Me.dgvTVEpisodes.SelectedRows.Count > 1 Then
                Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvTVEpisodes.SelectedRows.Count))
            ElseIf Me.dgvTVEpisodes.SelectedRows.Count = 1 Then
                Me.SetStatus(Me.dgvTVEpisodes.SelectedRows(0).Cells("Title").Value.ToString)
            End If

            Me.SelectRow_TVEpisode(Me.dgvTVEpisodes.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub tmrLoad_TVSeason_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoad_TVSeason.Tick
        Me.tmrWait_TVSeason.Stop()
        Me.tmrLoad_TVSeason.Stop()

        If Me.dgvTVSeasons.SelectedRows.Count > 0 Then

            If Me.dgvTVSeasons.SelectedRows.Count > 1 Then
                Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvTVSeasons.SelectedRows.Count))
            ElseIf Me.dgvMovies.SelectedRows.Count = 1 Then
                Me.SetStatus(Me.dgvTVSeasons.SelectedRows(0).Cells("SeasonText").Value.ToString)
            End If

            Me.SelectRow_TVSeason(Me.dgvTVSeasons.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub tmrLoad_TVShow_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoad_TVShow.Tick
        Me.tmrWait_TVShow.Stop()
        Me.tmrLoad_TVShow.Stop()

        If Me.dgvTVShows.SelectedRows.Count > 0 Then

            If Me.dgvTVShows.SelectedRows.Count > 1 Then
                Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvTVShows.SelectedRows.Count))
            ElseIf Me.dgvTVShows.SelectedRows.Count = 1 Then
                Me.SetStatus(Me.dgvTVShows.SelectedRows(0).Cells("TVShowPath").Value.ToString)
            End If

            Me.SelectRow_TVShow(Me.dgvTVShows.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub tmrLoad_Movie_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoad_Movie.Tick
        Me.tmrWait_Movie.Stop()
        Me.tmrLoad_Movie.Stop()

        If Me.dgvMovies.SelectedRows.Count > 0 Then

            If Me.dgvMovies.SelectedRows.Count > 1 Then
                Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvMovies.SelectedRows.Count))
            ElseIf Me.dgvMovies.SelectedRows.Count = 1 Then
                Me.SetStatus(Me.dgvMovies.SelectedRows(0).Cells("MoviePath").Value.ToString)
            End If

            Me.SelectRow_Movie(Me.dgvMovies.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub tmrLoad_MovieSet_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrLoad_MovieSet.Tick
        Me.tmrWait_MovieSet.Stop()
        Me.tmrLoad_MovieSet.Stop()

        If Me.dgvMovieSets.SelectedRows.Count > 0 Then

            If Me.dgvMovieSets.SelectedRows.Count > 1 Then
                Me.SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), Me.dgvMovieSets.SelectedRows.Count))
            ElseIf Me.dgvMovieSets.SelectedRows.Count = 1 Then
                Me.SetStatus(Me.dgvMovieSets.SelectedRows(0).Cells("SetName").Value.ToString)
            End If

            Me.SelectRow_MovieSet(Me.dgvMovieSets.SelectedRows(0).Index)
        End If
    End Sub

    Private Sub tmrRunTasks_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrRunTasks.Tick
        Me.tmrRunTasks.Enabled = False
        Me.TasksDone = False
        While Me.TaskList.Count > 0
            GenericRunCallBack(TaskList.Item(0).mType, TaskList.Item(0).Params)
            Me.TaskList.RemoveAt(0)
        End While
        Me.TasksDone = True
    End Sub

    Private Sub tmrSearchWait_Movies_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearchWait_Movies.Tick
        Me.tmrSearch_Movies.Enabled = False
        If Me.prevTextSearch_Movies = Me.currTextSearch_Movies Then
            Me.tmrSearch_Movies.Enabled = True
        Else
            Me.prevTextSearch_Movies = Me.currTextSearch_Movies
        End If
    End Sub

    Private Sub tmrSearch_Movies_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearch_Movies.Tick
        Me.tmrSearchWait_Movies.Enabled = False
        Me.tmrSearch_Movies.Enabled = False
        bDoingSearch_Movies = True

        If Not String.IsNullOrEmpty(Me.txtSearchMovies.Text) Then
            Me.FilterArray_Movies.Remove(Me.filSearch_Movies)
            Me.filSearch_Movies = String.Empty

            Select Case Me.cbSearchMovies.Text
                Case Master.eLang.GetString(21, "Title")
                    Me.filSearch_Movies = String.Concat("Title LIKE '%", Me.txtSearchMovies.Text, "%'")
                    Me.FilterArray_Movies.Add(Me.filSearch_Movies)
                Case Master.eLang.GetString(302, "Original Title")
                    Me.filSearch_Movies = String.Concat("OriginalTitle LIKE '%", Me.txtSearchMovies.Text, "%'")
                    Me.FilterArray_Movies.Add(Me.filSearch_Movies)
                Case Master.eLang.GetString(100, "Actor")
                    Me.filSearch_Movies = Me.txtSearchMovies.Text
                Case Master.eLang.GetString(233, "Role")
                    Me.filSearch_Movies = Me.txtSearchMovies.Text
                Case Master.eLang.GetString(62, "Director")
                    Me.filSearch_Movies = String.Concat("Director LIKE '%", Me.txtSearchMovies.Text, "%'")
                    Me.FilterArray_Movies.Add(Me.filSearch_Movies)
                Case Master.eLang.GetString(729, "Credits")
                    Me.filSearch_Movies = String.Concat("Credits LIKE '%", Me.txtSearchMovies.Text, "%'")
                    Me.FilterArray_Movies.Add(Me.filSearch_Movies)
                Case Master.eLang.GetString(301, "Country")
                    Me.filSearch_Movies = String.Concat("Country LIKE '%", Me.txtSearchMovies.Text, "%'")
                    Me.FilterArray_Movies.Add(Me.filSearch_Movies)
                Case Master.eLang.GetString(395, "Studio")
                    Me.filSearch_Movies = String.Concat("Studio LIKE '%", Me.txtSearchMovies.Text, "%'")
                    Me.FilterArray_Movies.Add(Me.filSearch_Movies)
            End Select

            Me.RunFilter_Movies(Me.cbSearchMovies.Text = Master.eLang.GetString(100, "Actor") OrElse Me.cbSearchMovies.Text = Master.eLang.GetString(233, "Role"))

        Else
            If Not String.IsNullOrEmpty(Me.filSearch_Movies) Then
                Me.FilterArray_Movies.Remove(Me.filSearch_Movies)
                Me.filSearch_Movies = String.Empty
            End If
            Me.RunFilter_Movies(True)
        End If
    End Sub

    Private Sub tmrSearchWait_MovieSets_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearchWait_MovieSets.Tick
        Me.tmrSearch_MovieSets.Enabled = False
        If Me.prevTextSearch_MovieSets = Me.currTextSearch_MovieSets Then
            Me.tmrSearch_MovieSets.Enabled = True
        Else
            Me.prevTextSearch_MovieSets = Me.currTextSearch_MovieSets
        End If
    End Sub

    Private Sub tmrSearch_MovieSets_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearch_MovieSets.Tick
        Me.tmrSearchWait_MovieSets.Enabled = False
        Me.tmrSearch_MovieSets.Enabled = False
        bDoingSearch_MovieSets = True

        If Not String.IsNullOrEmpty(Me.txtSearchMovieSets.Text) Then
            Me.FilterArray_MovieSets.Remove(Me.filSearch_MovieSets)
            Me.filSearch_MovieSets = String.Empty

            Select Case Me.cbSearchMovieSets.Text
                Case Master.eLang.GetString(21, "Title")
                    Me.filSearch_MovieSets = String.Concat("SetName LIKE '%", Me.txtSearchMovieSets.Text, "%'")
                    Me.FilterArray_MovieSets.Add(Me.filSearch_MovieSets)
            End Select

            Me.RunFilter_MovieSets(False)

        Else
            If Not String.IsNullOrEmpty(Me.filSearch_MovieSets) Then
                Me.FilterArray_MovieSets.Remove(Me.filSearch_MovieSets)
                Me.filSearch_MovieSets = String.Empty
            End If
            Me.RunFilter_MovieSets(True)
        End If
    End Sub

    Private Sub tmrSearchWait_Shows_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearchWait_Shows.Tick
        Me.tmrSearch_Shows.Enabled = False
        If Me.prevTextSearch_Shows = Me.currTextSearch_Shows Then
            Me.tmrSearch_Shows.Enabled = True
        Else
            Me.prevTextSearch_Shows = Me.currTextSearch_Shows
        End If
    End Sub

    Private Sub tmrSearch_Shows_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearch_Shows.Tick
        Me.tmrSearchWait_Shows.Enabled = False
        Me.tmrSearch_Shows.Enabled = False
        bDoingSearch_Shows = True
        Try
            If Not String.IsNullOrEmpty(Me.txtSearchShows.Text) Then
                Me.FilterArray_Shows.Remove(Me.filSearch_Shows)
                Me.filSearch_Shows = String.Empty

                Select Case Me.cbSearchShows.Text
                    Case Master.eLang.GetString(21, "Title")
                        Me.filSearch_Shows = String.Concat("Title LIKE '%", Me.txtSearchShows.Text, "%'")
                        Me.FilterArray_Shows.Add(Me.filSearch_Shows)
                End Select

                Me.RunFilter_Shows(False)

            Else
                If Not String.IsNullOrEmpty(Me.filSearch_Shows) Then
                    Me.FilterArray_Shows.Remove(Me.filSearch_Shows)
                    Me.filSearch_Shows = String.Empty
                End If
                Me.RunFilter_Shows(True)
            End If
        Catch
        End Try
    End Sub

    Private Sub tmrWait_TVEpisode_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWait_TVEpisode.Tick
        Me.tmrLoad_TVSeason.Stop()
        Me.tmrLoad_TVShow.Stop()
        Me.tmrWait_TVSeason.Stop()
        Me.tmrWait_TVShow.Stop()

        If Not Me.prevRow_TVEpisode = Me.currRow_TVEpisode Then
            Me.prevRow_TVEpisode = Me.currRow_TVEpisode
            Me.tmrWait_TVEpisode.Stop()
            Me.tmrLoad_TVEpisode.Start()
        Else
            Me.tmrLoad_TVEpisode.Stop()
            Me.tmrWait_TVEpisode.Stop()
        End If
    End Sub

    Private Sub tmrWait_TVSeason_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWait_TVSeason.Tick
        Me.tmrLoad_TVShow.Stop()
        Me.tmrLoad_TVEpisode.Stop()
        Me.tmrWait_TVShow.Stop()
        Me.tmrWait_TVEpisode.Stop()

        If Not Me.prevRow_TVSeason = Me.currRow_TVSeason Then
            Me.prevRow_TVSeason = Me.currRow_TVSeason
            Me.tmrWait_TVSeason.Stop()
            Me.tmrLoad_TVSeason.Start()
        Else
            Me.tmrLoad_TVSeason.Stop()
            Me.tmrWait_TVSeason.Stop()
        End If
    End Sub

    Private Sub tmrWait_TVShow_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWait_TVShow.Tick
        Me.tmrLoad_TVSeason.Stop()
        Me.tmrLoad_TVEpisode.Stop()
        Me.tmrWait_TVSeason.Stop()
        Me.tmrWait_TVEpisode.Stop()

        If Not Me.prevRow_TVShow = Me.currRow_TVShow Then
            Me.prevRow_TVShow = Me.currRow_TVShow
            Me.tmrWait_TVShow.Stop()
            Me.tmrLoad_TVShow.Start()
        Else
            Me.tmrLoad_TVShow.Stop()
            Me.tmrWait_TVShow.Stop()
        End If
    End Sub

    Private Sub tmrWait_Movie_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWait_Movie.Tick
        If Not Me.prevRow_Movie = Me.currRow_Movie Then
            Me.prevRow_Movie = Me.currRow_Movie
            Me.tmrWait_Movie.Stop()
            Me.tmrLoad_Movie.Start()
        Else
            Me.tmrLoad_Movie.Stop()
            Me.tmrWait_Movie.Stop()
        End If
    End Sub

    Private Sub tmrWait_MovieSet_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrWait_MovieSet.Tick
        If Not Me.prevRow_MovieSet = Me.currRow_MovieSet Then
            Me.prevRow_MovieSet = Me.currRow_MovieSet
            Me.tmrWait_MovieSet.Stop()
            Me.tmrLoad_MovieSet.Start()
        Else
            Me.tmrLoad_MovieSet.Stop()
            Me.tmrWait_MovieSet.Stop()
        End If
    End Sub

    Private Sub mnuUpdate_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUpdate.ButtonClick
        Me.LoadMedia(New Structures.Scans With {.Movies = True, .MovieSets = True, .TV = True})
    End Sub

    Private Sub TVSourceSubClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SourceName As String = DirectCast(sender, ToolStripItem).Tag.ToString

        'Remove any previous scrape as there is no warranty that the new dataset will match with the old one
        Dim aPath As String = FileUtils.Common.ReturnSettingsFile("Settings", "ScraperStatus.dat")
        If File.Exists(aPath) Then
            Try
                File.Delete(aPath)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Throw
            End Try
        End If


        Me.LoadMedia(New Structures.Scans With {.TV = True}, SourceName)
    End Sub

    Private Sub txtFilterGenre_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterGenre_Movies.Click
        Me.pnlFilterGenres_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterSpecific_Movies.Left + Me.tblFilterSpecific_Movies.Left + Me.tblFilterSpecificData_Movies.Left + Me.txtFilterGenre_Movies.Left + 1, _
                                                       (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterSpecific_Movies.Top + Me.tblFilterSpecific_Movies.Top + Me.tblFilterSpecificData_Movies.Top + Me.txtFilterGenre_Movies.Top) - Me.pnlFilterGenres_Movies.Height)
        Me.pnlFilterGenres_Movies.Width = Me.txtFilterGenre_Movies.Width
        If Me.pnlFilterGenres_Movies.Visible Then
            Me.pnlFilterGenres_Movies.Visible = False
        ElseIf Not Me.pnlFilterGenres_Movies.Tag.ToString = "NO" Then
            Me.pnlFilterGenres_Movies.Tag = String.Empty
            Me.pnlFilterGenres_Movies.Visible = True
            Me.clbFilterGenres_Movies.Focus()
        Else
            Me.pnlFilterGenres_Movies.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterGenre_Shows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterGenre_Shows.Click
        Me.pnlFilterGenres_Shows.Location = New Point(Me.pnlFilter_Shows.Left + Me.tblFilter_Shows.Left + Me.gbFilterSpecific_Shows.Left + Me.tblFilterSpecific_Shows.Left + Me.tblFilterSpecificData_Shows.Left + Me.txtFilterGenre_Shows.Left + 1, _
                                                       (Me.pnlFilter_Shows.Top + Me.tblFilter_Shows.Top + Me.gbFilterSpecific_Shows.Top + Me.tblFilterSpecific_Shows.Top + Me.tblFilterSpecificData_Shows.Top + Me.txtFilterGenre_Shows.Top) - Me.pnlFilterGenres_Shows.Height)
        Me.pnlFilterGenres_Shows.Width = Me.txtFilterGenre_Shows.Width
        If Me.pnlFilterGenres_Shows.Visible Then
            Me.pnlFilterGenres_Shows.Visible = False
        ElseIf Not Me.pnlFilterGenres_Shows.Tag.ToString = "NO" Then
            Me.pnlFilterGenres_Shows.Tag = String.Empty
            Me.pnlFilterGenres_Shows.Visible = True
            Me.clbFilterGenres_Shows.Focus()
        Else
            Me.pnlFilterGenres_Shows.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterCountry_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterCountry_Movies.Click
        Me.pnlFilterCountries_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterSpecific_Movies.Left + Me.tblFilterSpecific_Movies.Left + Me.tblFilterSpecificData_Movies.Left + Me.txtFilterCountry_Movies.Left + 1, _
                                                       (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterSpecific_Movies.Top + Me.tblFilterSpecific_Movies.Top + Me.tblFilterSpecificData_Movies.Top + Me.txtFilterCountry_Movies.Top) - Me.pnlFilterCountries_Movies.Height)
        Me.pnlFilterCountries_Movies.Width = Me.txtFilterCountry_Movies.Width
        If Me.pnlFilterCountries_Movies.Visible Then
            Me.pnlFilterCountries_Movies.Visible = False
        ElseIf Not Me.pnlFilterCountries_Movies.Tag.ToString = "NO" Then
            Me.pnlFilterCountries_Movies.Tag = String.Empty
            Me.pnlFilterCountries_Movies.Visible = True
            Me.clbFilterCountries_Movies.Focus()
        Else
            Me.pnlFilterCountries_Movies.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterDataField_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterDataField_Movies.Click
        Me.pnlFilterDataFields_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterSpecific_Movies.Left + Me.tblFilterSpecific_Movies.Left + Me.tblFilterSpecificData_Movies.Left + Me.gbFilterDataField_Movies.Left + Me.tblFilterDataField_Movies.Left + Me.txtFilterDataField_Movies.Left + 1, _
                                                        (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterSpecific_Movies.Top + Me.tblFilterSpecific_Movies.Top + Me.tblFilterSpecificData_Movies.Top + Me.gbFilterDataField_Movies.Top + Me.tblFilterDataField_Movies.Top + Me.txtFilterDataField_Movies.Top) - Me.pnlFilterDataFields_Movies.Height)
        Me.pnlFilterDataFields_Movies.Width = Me.txtFilterDataField_Movies.Width
        If Me.pnlFilterDataFields_Movies.Visible Then
            Me.pnlFilterDataFields_Movies.Visible = False
        ElseIf Not Me.pnlFilterDataFields_Movies.Tag.ToString = "NO" Then
            Me.pnlFilterDataFields_Movies.Tag = String.Empty
            Me.pnlFilterDataFields_Movies.Visible = True
            Me.clbFilterDataFields_Movies.Focus()
        Else
            Me.pnlFilterDataFields_Movies.Tag = String.Empty
        End If
    End Sub

    Private Sub btnFilterMissing_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterMissing_Movies.Click
        Me.pnlFilterMissingItems_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterGeneral_Movies.Left + Me.tblFilterGeneral_Movies.Left + Me.btnFilterMissing_Movies.Left + 1, _
                                                       (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterGeneral_Movies.Top + Me.tblFilterGeneral_Movies.Top + Me.btnFilterMissing_Movies.Top) - Me.pnlFilterMissingItems_Movies.Height)
        If Me.pnlFilterMissingItems_Movies.Visible Then
            Me.pnlFilterMissingItems_Movies.Visible = False
        Else
            Me.pnlFilterMissingItems_Movies.Visible = True
        End If
    End Sub

    Private Sub btnFilterMissing_MovieSets_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterMissing_MovieSets.Click
        Me.pnlFilterMissingItems_MovieSets.Location = New Point(Me.pnlFilter_MovieSets.Left + Me.tblFilter_MovieSets.Left + Me.gbFilterGeneral_MovieSets.Left + Me.tblFilterGeneral_MovieSets.Left + Me.btnFilterMissing_MovieSets.Left + 1, _
                                                       (Me.pnlFilter_MovieSets.Top + Me.tblFilter_MovieSets.Top + Me.gbFilterGeneral_MovieSets.Top + Me.tblFilterGeneral_MovieSets.Top + Me.btnFilterMissing_MovieSets.Top) - Me.pnlFilterMissingItems_MovieSets.Height)
        If Me.pnlFilterMissingItems_MovieSets.Visible Then
            Me.pnlFilterMissingItems_MovieSets.Visible = False
        Else
            Me.pnlFilterMissingItems_MovieSets.Visible = True
        End If
    End Sub

    Private Sub btnFilterMissing_Shows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterMissing_Shows.Click
        Me.pnlFilterMissingItems_Shows.Location = New Point(Me.pnlFilter_Shows.Left + Me.tblFilter_Shows.Left + Me.gbFilterGeneral_Shows.Left + Me.tblFilterGeneral_Shows.Left + Me.btnFilterMissing_Shows.Left + 1, _
                                                       (Me.pnlFilter_Shows.Top + Me.tblFilter_Shows.Top + Me.gbFilterGeneral_Shows.Top + Me.tblFilterGeneral_Shows.Top + Me.btnFilterMissing_Shows.Top) - Me.pnlFilterMissingItems_Shows.Height)
        If Me.pnlFilterMissingItems_Shows.Visible Then
            Me.pnlFilterMissingItems_Shows.Visible = False
        Else
            Me.pnlFilterMissingItems_Shows.Visible = True
        End If
    End Sub

    Private Sub txtFilterSource_Movies_Movies_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterSource_Movies.Click
        Me.pnlFilterSources_Movies.Location = New Point(Me.pnlFilter_Movies.Left + Me.tblFilter_Movies.Left + Me.gbFilterSpecific_Movies.Left + Me.tblFilterSpecific_Movies.Left + Me.tblFilterSpecificData_Movies.Left + Me.txtFilterSource_Movies.Left + 1, _
                                                       (Me.pnlFilter_Movies.Top + Me.tblFilter_Movies.Top + Me.gbFilterSpecific_Movies.Top + Me.tblFilterSpecific_Movies.Top + Me.tblFilterSpecificData_Movies.Top + Me.txtFilterSource_Movies.Top) - Me.pnlFilterSources_Movies.Height)
        Me.pnlFilterSources_Movies.Width = Me.txtFilterSource_Movies.Width
        If Me.pnlFilterSources_Movies.Visible Then
            Me.pnlFilterSources_Movies.Visible = False
        ElseIf Not Me.pnlFilterSources_Movies.Tag.ToString = "NO" Then
            Me.pnlFilterSources_Movies.Tag = String.Empty
            Me.pnlFilterSources_Movies.Visible = True
            Me.clbFilterSources_Movies.Focus()
        Else
            Me.pnlFilterSources_Movies.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterSource_Shows_Shows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilterSource_Shows.Click
        Me.pnlFilterSources_Shows.Location = New Point(Me.pnlFilter_Shows.Left + Me.tblFilter_Shows.Left + Me.gbFilterSpecific_Shows.Left + Me.tblFilterSpecific_Shows.Left + Me.tblFilterSpecificData_Shows.Left + Me.txtFilterSource_Shows.Left + 1, _
                                                       (Me.pnlFilter_Shows.Top + Me.tblFilter_Shows.Top + Me.gbFilterSpecific_Shows.Top + Me.tblFilterSpecific_Shows.Top + Me.tblFilterSpecificData_Shows.Top + Me.txtFilterSource_Shows.Top) - Me.pnlFilterSources_Shows.Height)
        Me.pnlFilterSources_Shows.Width = Me.txtFilterSource_Shows.Width
        If Me.pnlFilterSources_Shows.Visible Then
            Me.pnlFilterSources_Shows.Visible = False
        ElseIf Not Me.pnlFilterSources_Shows.Tag.ToString = "NO" Then
            Me.pnlFilterSources_Shows.Tag = String.Empty
            Me.pnlFilterSources_Shows.Visible = True
            Me.clbFilterSource_Shows.Focus()
        Else
            Me.pnlFilterSources_Shows.Tag = String.Empty
        End If
    End Sub

    Private Sub txtSearchMovies_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchMovies.KeyPress
        e.Handled = Not StringUtils.AlphaNumericOnly(e.KeyChar, True)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Me.dgvMovies.Focus()
        End If
    End Sub

    Private Sub txtSearchMovies_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchMovies.TextChanged
        Me.currTextSearch_Movies = Me.txtSearchMovies.Text

        Me.tmrSearchWait_Movies.Enabled = False
        Me.tmrSearch_Movies.Enabled = False
        Me.tmrSearchWait_Movies.Enabled = True
    End Sub

    Private Sub txtSearchMovieSets_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchMovieSets.KeyPress
        e.Handled = Not StringUtils.AlphaNumericOnly(e.KeyChar, True)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Me.dgvMovieSets.Focus()
        End If
    End Sub

    Private Sub txtSearchMovieSets_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchMovieSets.TextChanged
        Me.currTextSearch_MovieSets = Me.txtSearchMovieSets.Text

        Me.tmrSearchWait_MovieSets.Enabled = False
        Me.tmrSearch_MovieSets.Enabled = False
        Me.tmrSearchWait_MovieSets.Enabled = True
    End Sub

    Private Sub txtSearchShows_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchShows.KeyPress
        e.Handled = Not StringUtils.AlphaNumericOnly(e.KeyChar, True)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Me.dgvTVShows.Focus()
        End If
    End Sub

    Private Sub txtSearchShows_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchShows.TextChanged
        Me.currTextSearch_Shows = Me.txtSearchShows.Text

        Me.tmrSearchWait_Shows.Enabled = False
        Me.tmrSearch_Shows.Enabled = False
        Me.tmrSearchWait_Shows.Enabled = True
    End Sub

    Private Sub VersionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpVersions.Click
        ModulesManager.Instance.GetVersions()
    End Sub

    Private Sub mnuMainHelpWiki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpWiki.Click
        Functions.Launch(My.Resources.urlEmberWiki)
    End Sub

    Private Sub mnuMainHelpForumEng_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpForumEng.Click
        Functions.Launch(My.Resources.urlForumEng)
    End Sub

    Private Sub mnuMainHelpForumGer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpForumGer.Click
        Functions.Launch(My.Resources.urlForumGer)
    End Sub

    Private Sub tmrAppExit_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrAppExit.Tick
        tmrAppExit.Enabled = False
        Me.Close()
    End Sub

    Private Sub CheckUpdatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainHelpUpdate.Click
        If Functions.CheckNeedUpdate() Then
            Using dNewVer As New dlgNewVersion
                If dNewVer.ShowDialog() = Windows.Forms.DialogResult.Abort Then
                    tmrAppExit.Enabled = True
                    CloseApp = True
                End If
            End Using
        Else
            MessageBox.Show(Master.eLang.GetString(851, "No Updates at this time"), "Updates", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub lblIMDBHeader_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblIMDBHeader.Click
        If Not String.IsNullOrEmpty(txtIMDBID.Text) Then
            Functions.Launch(String.Format("http://www.imdb.com/title/tt{0}/", txtIMDBID.Text))
        End If
    End Sub

    Private Sub lblIMDBHeader_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblIMDBHeader.MouseEnter
        If Not String.IsNullOrEmpty(txtIMDBID.Text) Then
            lblIMDBHeader.Tag = lblIMDBHeader.ForeColor
            lblIMDBHeader.ForeColor = Color.FromArgb(Not lblIMDBHeader.ForeColor.R, Not lblIMDBHeader.ForeColor.G, Not lblIMDBHeader.ForeColor.B)
            Me.Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub lblIMDBHeader_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblIMDBHeader.MouseLeave
        If Not lblIMDBHeader.Tag Is Nothing Then
            lblIMDBHeader.ForeColor = DirectCast(lblIMDBHeader.Tag, Color)
            Me.Cursor = Cursors.Default
            lblIMDBHeader.Tag = Nothing
        End If
    End Sub

    Private Sub lblTMDBHeader_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTMDBHeader.Click
        If Not String.IsNullOrEmpty(txtTMDBID.Text) Then
            If Not My.Resources.urlTheMovieDb.EndsWith("/") Then
                Functions.Launch(My.Resources.urlTheMovieDb & "/movie/" & txtTMDBID.Text)
            Else
                Functions.Launch(My.Resources.urlTheMovieDb & "movie/" & txtTMDBID.Text)
            End If
        End If
    End Sub

    Private Sub lblTMDBHeader_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTMDBHeader.MouseEnter
        If Not String.IsNullOrEmpty(txtTMDBID.Text) Then
            lblTMDBHeader.Tag = lblTMDBHeader.ForeColor
            lblTMDBHeader.ForeColor = Color.FromArgb(Not lblTMDBHeader.ForeColor.R, Not lblTMDBHeader.ForeColor.G, Not lblTMDBHeader.ForeColor.B)
            Me.Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub lblTMDBHeader_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTMDBHeader.MouseLeave
        If Not lblTMDBHeader.Tag Is Nothing Then
            lblTMDBHeader.ForeColor = DirectCast(lblTMDBHeader.Tag, Color)
            Me.Cursor = Cursors.Default
            lblTMDBHeader.Tag = Nothing
        End If
    End Sub

    Private Sub pbStudio_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStudio.MouseEnter

        If Not clsAdvancedSettings.GetBooleanSetting("StudioTagAlwaysOn", False) AndAlso Not String.IsNullOrEmpty(pbStudio.Tag.ToString) Then
            lblStudio.Text = pbStudio.Tag.ToString
        End If
    End Sub
    Private Sub pbStudio_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStudio.MouseLeave
        If Not clsAdvancedSettings.GetBooleanSetting("StudioTagAlwaysOn", False) Then lblStudio.Text = String.Empty
    End Sub

    Private Sub tmrKeyBuffer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrKeyBuffer.Tick
        tmrKeyBuffer.Enabled = False
        KeyBuffer = String.Empty
    End Sub

    Private Sub bwCheckVersion_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCheckVersion.DoWork
        Try
            Dim sHTTP As New EmberAPI.HTTP
            'Pull Assembly version info from current Ember repo on github
            Dim HTML As String
            HTML = sHTTP.DownloadData("https://raw.github.com/DanCooper/Ember-MM-Newscraper/master/EmberMediaManager/My%20Project/AssemblyInfo.vb")
            sHTTP = Nothing
            Dim aBit As String = "x64"
            If Master.is32Bit Then
                aBit = "x86"
            End If
            Dim VersionNumber As String = System.String.Format(Master.eLang.GetString(865, "Version {0}.{1}.{2}.{3} {4}"), My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision, aBit)
            ' Not localized as is the Assembly file version
            Dim VersionNumberO As String = System.String.Format("{0}.{1}.{2}.{3}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)
            If Not String.IsNullOrEmpty(HTML) Then
                'Example: AssemblyFileVersion("1.3.0.18")>
                Dim mc As MatchCollection = System.Text.RegularExpressions.Regex.Matches(HTML, "AssemblyFileVersion([^<]+)>")
                'check to see if at least one entry was found
                If mc.Count > 0 Then
                    'just use the first match if more are found and compare with running Ember Version
                    If mc(0).Value.ToString <> "AssemblyFileVersion(""" & VersionNumberO & """)>" Then
                        'means that running Ember version is outdated!
                        Me.Invoke(New UpdatemnuVersionDel(AddressOf UpdatemnuVersion), System.String.Format(Master.eLang.GetString(1009, "{0} - (New version available!)"), VersionNumber), Color.DarkRed)
                    Else
                        'Ember already up to date!
                        Me.Invoke(New UpdatemnuVersionDel(AddressOf UpdatemnuVersion), VersionNumber, Color.DarkGreen)
                    End If
                End If
                'if no github query possible, than simply display Ember version on form
            Else
                Me.Invoke(New UpdatemnuVersionDel(AddressOf UpdatemnuVersion), VersionNumber, Color.DarkBlue)
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Delegate Sub UpdatemnuVersionDel(sText As String, sForeColor As Color)

    Private Sub UpdatemnuVersion(sText As String, sForeColor As Color)
        mnuVersion.Text = sText
        mnuVersion.ForeColor = sForeColor
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim ID As Integer
        Dim IsTV As Boolean
        Dim DBElement As Database.DBElement
        Dim Options_Movie As Structures.ScrapeOptions_Movie
        Dim Options_MovieSet As Structures.ScrapeOptions_MovieSet
        Dim Options_TV As Structures.ScrapeOptions_TV
        Dim Path As String
        Dim pURL As String
        Dim ScrapeList As List(Of ScrapeItem)
        Dim ScrapeType As Enums.ScrapeType
        Dim Season As Integer
        Dim setEnabled As Boolean
        Dim SetName As String
        Dim withEpisodes As Boolean
        Dim withSeasons As Boolean

#End Region 'Fields

    End Structure

    Private Structure Results

#Region "Fields"

        Dim doFill As Boolean
        Dim fileInfo As String
        Dim IsTV As Boolean
        Dim DBElement As Database.DBElement
        Dim MovieInSetPosters As List(Of MovieInSetPoster)
        Dim ScrapeOptions_Movie As Structures.ScrapeOptions_Movie
        Dim ScrapeOptions_MovieSet As Structures.ScrapeOptions_MovieSet
        Dim Path As String
        Dim Result As Image
        Dim ScrapeType As Enums.ScrapeType
        Dim setEnabled As Boolean
        Dim Cancelled As Boolean

#End Region 'Fields

    End Structure

    Class MovieInSetPoster

#Region "Fields"

        Private _movieposter As Image
        Private _movietitle As String
        Private _movieyear As String

#End Region 'Fields

#Region "Properties"

        Public Property MoviePoster() As Image
            Get
                Return Me._movieposter
            End Get
            Set(ByVal value As Image)
                Me._movieposter = value
            End Set
        End Property

        Public Property MovieTitle() As String
            Get
                Return Me._movietitle
            End Get
            Set(ByVal value As String)
                Me._movietitle = value
            End Set
        End Property

        Public Property MovieYear() As String
            Get
                Return Me._movieyear
            End Get
            Set(ByVal value As String)
                Me._movieyear = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub New()
            Me.Clear()
        End Sub

        Public Sub Clear()
            _movieposter = Nothing
            _movietitle = String.Empty
            _movieyear = String.Empty
        End Sub

#End Region 'Methods

    End Class

    Structure ScrapeItem

#Region "Fields"

        Dim DataRow As DataRow
        Dim ScrapeModifier As Structures.ScrapeModifier

#End Region 'Fields

    End Structure

    Structure Task

#Region "Fields"

        Dim mType As Enums.ModuleEventType
        Dim Params As List(Of Object)

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class