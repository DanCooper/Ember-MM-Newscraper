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

Imports System.IO
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Public Class frmMain

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwCheckVersion As New ComponentModel.BackgroundWorker
    Friend WithEvents bwCleanDB As New ComponentModel.BackgroundWorker
    Friend WithEvents bwDownloadPic As New ComponentModel.BackgroundWorker
    Friend WithEvents bwDownloadGuestStarPic As New ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadImages As New ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadImages_MovieSetMoviePosters As New ComponentModel.BackgroundWorker
    Friend WithEvents bwMovieScraper As New ComponentModel.BackgroundWorker
    Friend WithEvents bwMoviesetScraper As New ComponentModel.BackgroundWorker
    Friend WithEvents bwReload_Movie As New ComponentModel.BackgroundWorker
    Friend WithEvents bwReload_Movieset As New ComponentModel.BackgroundWorker
    Friend WithEvents bwReload_TVShow As New ComponentModel.BackgroundWorker
    Friend WithEvents bwRewriteContent As New ComponentModel.BackgroundWorker
    Friend WithEvents bwTVScraper As New ComponentModel.BackgroundWorker
    Friend WithEvents bwTVEpisodeScraper As New ComponentModel.BackgroundWorker
    Friend WithEvents bwTVSeasonScraper As New ComponentModel.BackgroundWorker

    Public fCommandLine As New CommandLine

    Private _TaskList As New Queue(Of Task)
    Private _TasksDone As Boolean = True

    Private alActors As New List(Of String)
    Private alGuestStars As New List(Of String)
    Private _FilterPanelIsRaised_Movie As Boolean = False
    Private _FilterPanelIsRaised_Movieset As Boolean = False
    Private _FilterPanelIsRaised_TVShow As Boolean = False
    Private _InfoPanelState_Movie As Integer = 0 '0 = down, 1 = mid, 2 = up
    Private _InfoPanelState_Movieset As Integer = 0 '0 = down, 1 = mid, 2 = up
    Private _InfoPanelState_TVEpisode As Integer = 0 '0 = down, 1 = mid, 2 = up
    Private _InfoPanelState_TVSeason As Integer = 0 '0 = down, 1 = mid, 2 = up
    Private _InfoPanelState_TVShow As Integer = 0 '0 = down, 1 = mid, 2 = up

    Private _BsMovies As New BindingSource
    Private _BsMoviesets As New BindingSource
    Private _BsTVEpisodes As New BindingSource
    Private _BsTVSeasons As New BindingSource
    Private _BsTVShows As New BindingSource

    Private _DtMovies As New DataTable
    Private _DtMovieSets As New DataTable
    Private _DtTVEpisodes As New DataTable
    Private _DtTVSeasons As New DataTable
    Private _DtTVShows As New DataTable

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
    Private MainFanartSmall As New Images
    Private MainGuestStars As New Images
    Private MainKeyArt As New Images
    Private MainLandscape As New Images
    Private MainPoster As New Images
    Private pbGenre() As PictureBox = Nothing
    Private pnlGenre() As Panel = Nothing
    Private ReportDownloadPercent As Boolean = False
    Private sHTTP As New HTTP

    'Loading Delays
    Private currRow_Movie As Integer = -1
    Private currRow_MovieSet As Integer = -1
    Private currRow_TVEpisode As Integer = -1
    Private currRow_TVSeason As Integer = -1
    Private currRow_TVShow As Integer = -1
    Private currList As Integer = 0
    Private currThemeType As Enums.ContentType = Enums.ContentType.None
    Private prevRow_Movie As Integer = -1
    Private prevRow_MovieSet As Integer = -1
    Private prevRow_TVEpisode As Integer = -1
    Private prevRow_TVSeason As Integer = -1
    Private prevRow_TVShow As Integer = -1

    'list movies
    Private currList_Movies As String = "movielist" 'default movie list SQLite view, needed for LoadWithCommandLine() 

    'list moviesets
    Private currList_MovieSets As String = "moviesetlist" 'default moviesets list SQLite view, needed for LoadWithCommandLine() 

    'list shows
    Private currList_TVShows As String = "tvshowlist" 'default tv show list SQLite view, needed for LoadWithCommandLine() 

    'filter movies
    Private bDoingSearch_Movies As Boolean = False
    Private Filter_Movie As New SmartFilter.Filter(Enums.ContentType.Movie)
    Private filSearch_Movies As String = String.Empty
    Private currTextSearch_Movies As String = String.Empty
    Private prevTextSearch_Movies As String = String.Empty

    'filter moviesets
    Private bDoingSearch_MovieSets As Boolean = False
    Private Filter_Movieset As New SmartFilter.Filter(Enums.ContentType.Movieset)
    Private currTextSearch_MovieSets As String = String.Empty
    Private prevTextSearch_MovieSets As String = String.Empty

    'filter episodes
    Private Filter_TVEpisodes As New SmartFilter.Filter(Enums.ContentType.TVEpisode)

    'filter season
    Private Filter_TVSeasons As New SmartFilter.Filter(Enums.ContentType.TVSeason)

    'filter shows
    Private bDoingSearch_TVShows As Boolean = False
    Private Filter_TVShows As New SmartFilter.Filter(Enums.ContentType.TVShow)
    Private filSearch_TVShows As String = String.Empty
    Private currTextSearch_TVShows As String = String.Empty
    Private prevTextSearch_TVShows As String = String.Empty


    Private tTheme As Theming
    Private CloseApp As Boolean = False

    'Scraper menu tags
    Private _SelectedScrapeType As String = String.Empty
    Private _SelectedScrapeTypeMode As String = String.Empty
    Private _SelectedContentType As String = String.Empty

    Private oldStatus As String = String.Empty

    Private KeyBuffer As String = String.Empty

    Private _CurrDBElement As Database.DBElement

#End Region 'Fields

#Region "Delegates"

    Delegate Sub Delegate_dtListAddRow(ByVal dTable As DataTable, ByVal dRow As DataRow)
    Delegate Sub Delegate_dtListRemoveRow(ByVal dTable As DataTable, ByVal dRow As DataRow)
    Delegate Sub Delegate_dtListUpdateRow(ByVal dRow As DataRow, ByVal v As DataRow)

    Delegate Sub Delegate_ChangeToolStripLabel(control As ToolStripLabel,
                                               bVisible As Boolean,
                                               strValue As String)
    Delegate Sub Delegate_ChangeToolStripProgressBar(control As ToolStripProgressBar,
                                                     bVisible As Boolean,
                                                     iMaximum As Integer,
                                                     iMinimum As Integer,
                                                     iValue As Integer,
                                                     tStyle As ProgressBarStyle)

    Delegate Sub MySettingsShow(ByVal dlg As dlgSettings)

#End Region 'Delegates

#Region "Properties"

    Private ReadOnly Property AnyBackgroundWorkerIsBusy() As Boolean
        Get
            Return _
                bwCheckVersion.IsBusy OrElse
                bwCleanDB.IsBusy OrElse
                bwDownloadPic.IsBusy OrElse
                bwDownloadGuestStarPic.IsBusy OrElse
                bwLoadImages.IsBusy OrElse
                bwLoadImages_MovieSetMoviePosters.IsBusy OrElse
                bwMovieScraper.IsBusy OrElse
                bwMoviesetScraper.IsBusy OrElse
                bwReload_Movie.IsBusy OrElse
                bwReload_Movieset.IsBusy OrElse
                bwReload_TVShow.IsBusy OrElse
                bwRewriteContent.IsBusy OrElse
                bwTVScraper.IsBusy OrElse
                bwTVEpisodeScraper.IsBusy OrElse
                bwTVSeasonScraper.IsBusy
        End Get
    End Property

    Private ReadOnly Property AnyScraperIsBusy() As Boolean
        Get
            Return _
                bwMovieScraper.IsBusy OrElse
                bwMoviesetScraper.IsBusy OrElse
                bwTVScraper.IsBusy OrElse
                bwTVEpisodeScraper.IsBusy OrElse
                bwTVSeasonScraper.IsBusy
        End Get
    End Property

    Public Property GenrePanelColor() As Color = Color.Gainsboro
    Public Property InfoPanelMidHeight() As Integer = 280
    Public Property InfoPanelUpHeight() As Integer = 500

    Public Property BannerMaxHeight() As Integer = 160
    Public Property BannerMaxWidth() As Integer = 285
    Public Property CharacterArtMaxHeight() As Integer = 160
    Public Property CharacterArtMaxWidth() As Integer = 160
    Public Property ClearArtMaxHeight() As Integer = 160
    Public Property ClearArtMaxWidth() As Integer = 285
    Public Property ClearLogoMaxHeight() As Integer = 160
    Public Property ClearLogoMaxWidth() As Integer = 285
    Public Property DiscArtMaxHeight() As Integer = 160
    Public Property DiscArtMaxWidth() As Integer = 160
    Public Property FanartSmallMaxHeight() As Integer = 160
    Public Property FanartSmallMaxWidth() As Integer = 285
    Public Property KeyArtMaxHeight() As Integer = 160
    Public Property KeyArtMaxWidth() As Integer = 160
    Public Property LandscapeMaxHeight() As Integer = 160
    Public Property LandscapeMaxWidth() As Integer = 285
    Public Property MediaListColors As New XMLTheme.MediaListSettings
    Public Property PosterMaxHeight() As Integer = 160
    Public Property PosterMaxWidth() As Integer = 113

#End Region 'Properties

#Region "Dialog"

    Private Sub Dialog_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        _Logger.Info("====Ember Media Manager exiting====")
    End Sub
    ''' <summary>
    ''' The FormClosing event has been called, so prepare the form to shut down
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Dialog_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            Dim doSave As Boolean = True

            SetControlsEnabled(False, True)
            FilterPanel_IsEnabled_Movie(False)
            FilterPanel_IsEnabled_Movieset(False)
            FilterPanel_IsEnabled_TVShow(False)

            If Master.TaskManager.IsBusy OrElse Master.isCL Then
                doSave = False
            End If

            If Master.TaskManager.IsBusy Then Master.TaskManager.Cancel()
            If bwLoadImages.IsBusy Then bwLoadImages.CancelAsync()
            If bwLoadImages_MovieSetMoviePosters.IsBusy Then bwLoadImages_MovieSetMoviePosters.CancelAsync()
            If bwDownloadPic.IsBusy Then bwDownloadPic.CancelAsync()
            If bwDownloadGuestStarPic.IsBusy Then bwDownloadGuestStarPic.CancelAsync()
            If bwReload_Movie.IsBusy Then bwReload_Movie.CancelAsync()
            If bwCleanDB.IsBusy Then bwCleanDB.CancelAsync()
            If bwMovieScraper.IsBusy Then bwMovieScraper.CancelAsync()

            lblCanceling.Text = Master.eLang.GetString(99, "Canceling All Processes...")
            btnCancel.Visible = False
            lblCanceling.Visible = True
            prbCanceling.Visible = True
            pnlCancel.Visible = True
            Refresh()

            If AddonsManager.Instance.QueryAnyAddonIsBusy Then
                If MessageBox.Show("One or more modules are busy. Do you want to wait until all tasks are finished?", "One or more external Modules are busy", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                    While AddonsManager.Instance.QueryAnyAddonIsBusy
                        Application.DoEvents()
                        Threading.Thread.Sleep(50)
                    End While
                End If
            End If

            While Master.TaskManager.IsBusy OrElse
                bwCleanDB.IsBusy OrElse
                bwDownloadPic.IsBusy OrElse
                bwDownloadGuestStarPic.IsBusy OrElse
                bwLoadImages.IsBusy OrElse
                bwLoadImages_MovieSetMoviePosters.IsBusy OrElse
                bwMovieScraper.IsBusy OrElse
                bwReload_Movie.IsBusy OrElse
                bwReload_Movieset.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            If doSave Then
                If Master.eSettings.Movie.SourceSettings.ResetNewOnExit Then Master.DB.ClearNew(Enums.ContentType.Movie)
                If Master.eSettings.Movieset.SourceSettings.ResetNewOnExit Then Master.DB.ClearNew(Enums.ContentType.Movieset)
                If Master.eSettings.TVEpisode.SourceSettings.ResetNewOnExit Then Master.DB.ClearNew(Enums.ContentType.TVEpisode)
                If Master.eSettings.TVSeason.SourceSettings.ResetNewOnExit Then Master.DB.ClearNew(Enums.ContentType.TVSeason)
                If Master.eSettings.TVShow.SourceSettings.ResetNewOnExit Then Master.DB.ClearNew(Enums.ContentType.TVSeason)
            End If

            If Not Master.isCL Then
                Master.DB.Close_MyVideos()
            End If

            Master.eSettings.Version = Master.VersionAsString

            With Manager.mSettings.MainOptions.GuiSettings
                If Not Master.isCL Then
                    Application.DoEvents()
                    .FilterPanelIsRaised_Movie = _FilterPanelIsRaised_Movie
                    .FilterPanelIsRaised_Movieset = _FilterPanelIsRaised_Movieset
                    .FilterPanelIsRaised_TVShow = _FilterPanelIsRaised_TVShow
                    .InfoPanelState_Movie = _InfoPanelState_Movie
                    .InfoPanelState_Movieset = _InfoPanelState_Movieset
                    .InfoPanelState_TVEpisode = _InfoPanelState_TVEpisode
                    .InfoPanelState_TVSeason = _InfoPanelState_TVSeason
                    .InfoPanelState_TVShow = _InfoPanelState_TVShow
                    .SplitterDistance_MediaList = scMain.SplitterDistance
                    'Master.eSettings.GeneralSplitterDistanceTVShow and Master.eSettings.GeneralSplitterDistanceTVSeason will not be saved at this point
                    If WindowState = FormWindowState.Normal Then
                        .WindowLoc = Location
                        .WindowSize = Size
                    End If
                    If Not WindowState = FormWindowState.Minimized Then
                        .WindowState = WindowState
                    End If
                End If
            End With
            Manager.mSettings.Save()
            Master.eSettings.Save()

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
    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Visible = False

        TrayIcon = New NotifyIcon(components) With {
            .Icon = Icon,
            .ContextMenuStrip = cmnuTray,
            .Text = "Ember Media Manager",
            .Visible = True
        }

        bwCheckVersion.RunWorkerAsync()

        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(854, "Basic setup"))

        AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf MyResolveEventHandler

        AdvancedSettings.Start()

        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(858, "Loading Database..."))
        Master.DB.Connect_MyVideos()
        Master.DB.LoadAllGenres() 'TODO: check if needed at this point

        'set before modules has been loaded to prepare all RuntimeObjects that are used in modules
        AddonsManager.Instance.RuntimeObjects.MainMenu = mnuMain
        AddonsManager.Instance.RuntimeObjects.MainToolStrip = tsMain
        AddonsManager.Instance.RuntimeObjects.MediaListMovies = dgvMovies
        AddonsManager.Instance.RuntimeObjects.MediaListMovieSets = dgvMoviesets
        AddonsManager.Instance.RuntimeObjects.MediaListTVEpisodes = dgvTVEpisodes
        AddonsManager.Instance.RuntimeObjects.MediaListTVSeasons = dgvTVSeasons
        AddonsManager.Instance.RuntimeObjects.MediaListTVShows = dgvTVShows
        AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList = cmnuMovie
        AddonsManager.Instance.RuntimeObjects.ContextMenuMovieSetList = cmnuMovieSet
        AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList = cmnuEpisode
        AddonsManager.Instance.RuntimeObjects.ContextMenuTVSeasonList = cmnuSeason
        AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList = cmnuShow
        AddonsManager.Instance.RuntimeObjects.TrayMenu = cmnuTray

        'start loading modules in background
        AddonsManager.Instance.LoadAllAddons()

        If Not Master.isCL Then
            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(857, "Creating GUI..."))
        End If

        'setup some dummies so we don't get exceptions when resizing form/info panel
        ReDim Preserve pnlGenre(0)
        ReDim Preserve pbGenre(0)
        pnlGenre(0) = New Panel()
        pbGenre(0) = New PictureBox()

        AddHandler fCommandLine.TaskEvent, AddressOf Task_RunCallBack
        AddHandler AddonsManager.Instance.GenericEvent, AddressOf Generic_RunCallBack
        AddHandler Master.Events.ProgressUpdate, AddressOf Generic_RunCallBack
        AddHandler Master.TaskManager.ProgressUpdate, AddressOf TaskManager_ProgressUpdate

        Functions.DGVDoubleBuffer(dgvMovies)
        Functions.DGVDoubleBuffer(dgvMoviesets)
        Functions.DGVDoubleBuffer(dgvTVShows)
        Functions.DGVDoubleBuffer(dgvTVSeasons)
        Functions.DGVDoubleBuffer(dgvTVEpisodes)
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.UserPaint, True)

        If TypeOf tsMain.Renderer Is ToolStripProfessionalRenderer Then
            CType(tsMain.Renderer, ToolStripProfessionalRenderer).RoundedEdges = False
        End If

        If Not Directory.Exists(Master.TempPath) Then Directory.CreateDirectory(Master.TempPath)

        While Not AddonsManager.Instance.AllAddonsLoaded()
            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(856, "Loading Addons..."))
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        If Master.isCL Then ' Command Line
            Dialog_LoadWithCommandLine(Master.appArgs)
        Else 'Regular Run (GUI)
            Dialog_LoadWithGUI()
        End If
        Master.fLoading.Close()
    End Sub
    ''' <summary>
    ''' Performs startup routines specific to being initiated by the command line
    ''' </summary>
    ''' <param name="appArgs">Command line arguments. Must NOT be empty!</param>
    ''' <remarks></remarks>
    Private Sub Dialog_LoadWithCommandLine(ByVal appArgs As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs)
        _Logger.Trace("LoadWithCommandLine()")

        'filtered media lists are not possible, so use the default list for all lists
        RemoveHandler dgvMovies.CellEnter, AddressOf DataGridView_CellEnter_Movie
        RemoveHandler dgvMovies.RowsAdded, AddressOf DataGridView_RowsAdded
        RemoveHandler dgvMoviesets.RowsAdded, AddressOf DataGridView_RowsAdded
        RemoveHandler dgvTVShows.RowsAdded, AddressOf DataGridView_RowsAdded
        DataGridView_Fill_Main(True, True, True)
        AddHandler dgvMovies.CellEnter, AddressOf DataGridView_CellEnter_Movie
        AddHandler dgvMovies.RowsAdded, AddressOf DataGridView_RowsAdded
        AddHandler dgvMoviesets.RowsAdded, AddressOf DataGridView_RowsAdded
        AddHandler dgvTVShows.RowsAdded, AddressOf DataGridView_RowsAdded

        Dim Args() As String = appArgs.CommandLine.ToArray

        fCommandLine.RunCommandLine(Args)

        While Not _TaskList.Count = 0 OrElse Not _TasksDone
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Master.fLoading.Close()
        Close()
    End Sub
    ''' <summary>
    ''' Performs startup routines specific to being initiated as a GUI application (user interaction intended)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Dialog_LoadWithGUI()
        Try
            _Logger.Trace("LoadWithGUI()")
            If Not CloseApp Then

                Setup(True)

                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(863, "Positioning Controls..."))
                Location = Manager.mSettings.MainOptions.GuiSettings.WindowLoc
                Size = Manager.mSettings.MainOptions.GuiSettings.WindowSize
                WindowState = Manager.mSettings.MainOptions.GuiSettings.WindowState
                If Not WindowState = FormWindowState.Minimized Then
                    Master.AppPos = Bounds
                End If

                'SplitterDistance
                Try ' On error just ignore this a let it use default
                    scMain.SplitterDistance = Manager.mSettings.MainOptions.GuiSettings.SplitterDistance_MediaList
                    'Master.eSettings.GeneralSplitterDistanceTVShow and Master.eSettings.GeneralSplitterDistanceTVSeason will not be loaded at this point
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try

                'Info panels
                _InfoPanelState_Movie = Manager.mSettings.MainOptions.GuiSettings.InfoPanelState_Movie
                _InfoPanelState_Movieset = Manager.mSettings.MainOptions.GuiSettings.InfoPanelState_Movieset
                _InfoPanelState_TVEpisode = Manager.mSettings.MainOptions.GuiSettings.InfoPanelState_TVEpisode
                _InfoPanelState_TVSeason = Manager.mSettings.MainOptions.GuiSettings.InfoPanelState_TVSeason
                _InfoPanelState_TVShow = Manager.mSettings.MainOptions.GuiSettings.InfoPanelState_TVShow

                'Filter panels
                _FilterPanelIsRaised_Movie = Manager.mSettings.MainOptions.GuiSettings.FilterPanelIsRaised_Movie
                If _FilterPanelIsRaised_Movie Then
                    pnlFilter_Movies.AutoSize = True
                    btnFilterDown_Movies.Enabled = True
                    btnFilterUp_Movies.Enabled = False
                Else
                    pnlFilter_Movies.AutoSize = False
                    pnlFilter_Movies.Height = pnlFilterTop_Movies.Height
                    btnFilterDown_Movies.Enabled = False
                    btnFilterUp_Movies.Enabled = True
                End If

                _FilterPanelIsRaised_Movieset = Manager.mSettings.MainOptions.GuiSettings.FilterPanelIsRaised_Movieset
                If _FilterPanelIsRaised_Movieset Then
                    pnlFilter_MovieSets.AutoSize = True
                    btnFilterDown_MovieSets.Enabled = True
                    btnFilterUp_MovieSets.Enabled = False
                Else
                    pnlFilter_MovieSets.AutoSize = False
                    pnlFilter_MovieSets.Height = pnlFilterTop_MovieSets.Height
                    btnFilterDown_MovieSets.Enabled = False
                    btnFilterUp_MovieSets.Enabled = True
                End If

                _FilterPanelIsRaised_TVShow = Manager.mSettings.MainOptions.GuiSettings.FilterPanelIsRaised_TVShow
                If _FilterPanelIsRaised_TVShow Then
                    pnlFilter_Shows.AutoSize = True
                    btnFilterDown_Shows.Enabled = True
                    btnFilterUp_Shows.Enabled = False
                Else
                    pnlFilter_Shows.AutoSize = False
                    pnlFilter_Shows.Height = pnlFilterTop_Shows.Height
                    btnFilterDown_Shows.Enabled = False
                    btnFilterUp_Shows.Enabled = True
                End If

                'MenuItem Tags for better Enable/Disable handling
                mnuMainToolsCleanDB.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfNoMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfNoMovieSets = True, .IfNoTVShows = True, .IfTabTVShows = True}
                mnuMainToolsClearCache.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfNoMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfNoMovieSets = True, .IfNoTVShows = True, .IfTabTVShows = True}
                mnuMainToolsOfflineHolder.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .IfNoMovies = True}
                mnuMainToolsReloadMovies.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}
                mnuMainToolsReloadMovieSets.Tag = New Structures.ModulesMenus With {.ForMovieSets = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}
                mnuMainToolsReloadTVShows.Tag = New Structures.ModulesMenus With {.ForTVShows = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}
                mnuMainToolsRewriteContentMovie.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}
                mnuMainToolsSortFiles.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfNoMovies = True, .IfTabMovies = True, .IfTabMovieSets = True, .IfTabTVShows = True}

                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(1165, "Initializing Main Form. Please wait..."))

                Application.DoEvents()

                Visible = True

                Master.fLoading.SetLoadingMesg(Master.eLang.GetString(864, "Setting Menus..."))
                SetMenus(True)
                cmnuTrayExit.Enabled = True
                cmnuTraySettings.Enabled = True
                mnuMainEdit.Enabled = True

                'Fill all lists after MainTabs has been set to load the correct view/list
                RemoveHandler dgvMovies.CellEnter, AddressOf DataGridView_CellEnter_Movie
                RemoveHandler dgvMovies.RowsAdded, AddressOf DataGridView_RowsAdded
                RemoveHandler dgvMoviesets.RowsAdded, AddressOf DataGridView_RowsAdded
                RemoveHandler dgvTVShows.RowsAdded, AddressOf DataGridView_RowsAdded
                DataGridView_Fill_Main(True, True, True)
                AddHandler dgvMovies.CellEnter, AddressOf DataGridView_CellEnter_Movie
                AddHandler dgvMovies.RowsAdded, AddressOf DataGridView_RowsAdded
                AddHandler dgvMoviesets.RowsAdded, AddressOf DataGridView_RowsAdded
                AddHandler dgvTVShows.RowsAdded, AddressOf DataGridView_RowsAdded
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub Dialog_Move(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Move
        If Not WindowState = FormWindowState.Minimized Then
            Master.AppPos = Bounds
        End If
    End Sub
    ''' <summary>
    ''' The form has been resized, so re-position those controls that need to be re-located
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Dialog_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
        If Created Then
            If Not WindowState = FormWindowState.Minimized Then
                Master.AppPos = Bounds
            End If
            MoveMPAA()
            MoveGenres()
            ImageUtils.ResizePB(pbFanart, pbFanartCache, scMain.Panel2.Height - (pnlTop.Top + pnlTop.Height), scMain.Panel2.Width, True)
            pbFanart.Left = Convert.ToInt32((scMain.Panel2.Width - pbFanart.Width) / 2)
            pbFanart.Top = pnlTop.Top + pnlTop.Height
            pnlNoInfo.Location = New Point(Convert.ToInt32((scMain.Panel2.Width - pnlNoInfo.Width) / 2), Convert.ToInt32((scMain.Panel2.Height - pnlNoInfo.Height) / 2))
            pnlCancel.Location = New Point(Convert.ToInt32((scMain.Panel2.Width - pnlNoInfo.Width) / 2), 125)
            pnlFilterCountries_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterCountry_Movies.Left + 1,
                                                              (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterCountry_Movies.Top) - pnlFilterCountries_Movies.Height)
            pnlFilterGenres_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterGenre_Movies.Left + 1,
                                                           (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterGenre_Movies.Top) - pnlFilterGenres_Movies.Height)
            pnlFilterGenres_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterSpecific_Shows.Left + tblFilterSpecific_Shows.Left + tblFilterSpecificData_Shows.Left + txtFilterGenre_Shows.Left + 1,
                                                          (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterSpecific_Shows.Top + tblFilterSpecific_Shows.Top + tblFilterSpecificData_Shows.Top + txtFilterGenre_Shows.Top) - pnlFilterGenres_Shows.Height)
            pnlFilterDataFields_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + gbFilterDataField_Movies.Left + tblFilterDataField_Movies.Left + txtFilterDataField_Movies.Left + 1,
                                                               (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + gbFilterDataField_Movies.Top + tblFilterDataField_Movies.Top + txtFilterDataField_Movies.Top) - pnlFilterDataFields_Movies.Height)
            pnlFilterMissingItems_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterGeneral_Movies.Left + tblFilterGeneral_Movies.Left + btnFilterMissing_Movies.Left + 1,
                                                                 (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterGeneral_Movies.Top + tblFilterGeneral_Movies.Top + btnFilterMissing_Movies.Top) - pnlFilterMissingItems_Movies.Height)
            pnlFilterMissingItems_MovieSets.Location = New Point(pnlFilter_MovieSets.Left + tblFilter_MovieSets.Left + gbFilterGeneral_MovieSets.Left + tblFilterGeneral_MovieSets.Left + btnFilterMissing_MovieSets.Left + 1,
                                                                 (pnlFilter_MovieSets.Top + tblFilter_MovieSets.Top + gbFilterGeneral_MovieSets.Top + tblFilterGeneral_MovieSets.Top + btnFilterMissing_MovieSets.Top) - pnlFilterMissingItems_MovieSets.Height)
            pnlFilterMissingItems_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterGeneral_Shows.Left + tblFilterGeneral_Shows.Left + btnFilterMissing_Shows.Left + 1,
                                                                 (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterGeneral_Shows.Top + tblFilterGeneral_Shows.Top + btnFilterMissing_Shows.Top) - pnlFilterMissingItems_Shows.Height)
            pnlFilterSources_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterSource_Movies.Left + 1,
                                                            (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterSource_Movies.Top) - pnlFilterSources_Movies.Height)
            pnlFilterSources_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterSpecific_Shows.Left + tblFilterSpecific_Shows.Left + tblFilterSpecificData_Shows.Left + txtFilterSource_Shows.Left + 1,
                                                           (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterSpecific_Shows.Top + tblFilterSpecific_Shows.Top + tblFilterSpecificData_Shows.Top + txtFilterSource_Shows.Top) - pnlFilterSources_Shows.Height)
            pnlFilterTags_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterTag_Movies.Left + 1,
                                                           (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterTag_Movies.Top) - pnlFilterTags_Movies.Height)
            pnlFilterVideoSources_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterVideoSource_Movies.Left + 1,
                                                              (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterVideoSource_Movies.Top) - pnlFilterVideoSources_Movies.Height)
            pnlLoadSettings.Location = New Point(Convert.ToInt32((Width - pnlLoadSettings.Width) / 2), Convert.ToInt32((Height - pnlLoadSettings.Height) / 2))

            Theme_Apply(currThemeType)
        End If
    End Sub

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        If Not CloseApp Then
            BringToFront()
            Activate()
            cmnuTray.Enabled = True
        End If
    End Sub
    ''' <summary>
    ''' Setup the default/initial text for the GUI's controls. 
    ''' Language used is based on the app's current setting.
    ''' </summary>
    ''' <param name="doTheme"></param>
    ''' <remarks></remarks>
    Private Sub Setup(ByVal doTheme As Boolean)
        MinimumSize = New Size(800, 600)

        'Actor Thumbs Only
        Dim strActorThumbsOnly = Master.eLang.GetString(973, "Actor Thumbs Only")
        mnuScrapeModifierActorthumbs.Text = strActorThumbsOnly

        'Add
        Dim strAdd As String = Master.eLang.GetString(28, "Add")
        mnuGenresAdd.Text = strAdd
        mnuTagsAdd.Text = strAdd

        'All
        Dim strAll As String = Master.eLang.GetString(68, "All")
        mnuScrapeSubmenuAll.Text = strAll

        'All Items
        Dim strAllItems As String = Master.eLang.GetString(70, "All Items")
        mnuScrapeModifierAll.Text = strAllItems
        mnuMainToolsRewriteContentMovieAll.Text = strAllItems
        mnuMainToolsRewriteContentMovieSetAll.Text = strAllItems
        mnuMainToolsRewriteContentTVShowAll.Text = strAllItems

        'Ask (Require Input If No Exact Match)
        Dim strAsk As String = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
        mnuScrapeTypeAsk.Text = strAsk

        'Automatic (Force Best Match)
        Dim strAutomatic As String = Master.eLang.GetString(69, "Automatic (Force Best Match)")
        mnuScrapeTypeAuto.Text = strAutomatic

        'Custom Scraper
        Dim strCustomScraper = Master.eLang.GetString(81, "Custom Scraper...")
        mnuScrapeSubmenuCustom.Text = strCustomScraper

        'Banner Only
        Dim strBannerOnly As String = Master.eLang.GetString(1060, "Banner Only")
        mnuScrapeModifierBanner.Text = strBannerOnly

        'Change Language
        Dim strChangeLanguage As String = Master.eLang.GetString(1200, "Change Language")
        cmnuMovieLanguage.Text = strChangeLanguage
        cmnuMovieSetLanguage.Text = strChangeLanguage
        cmnuShowLanguage.Text = strChangeLanguage

        'CharacterArt Only
        Dim strCharacterArtOnly As String = Master.eLang.GetString(1121, "CharacterArt Only")
        mnuScrapeModifierCharacterArt.Text = strCharacterArtOnly

        'ClearArt Only
        Dim strClearArtOnly As String = Master.eLang.GetString(1122, "ClearArt Only")
        mnuScrapeModifierClearArt.Text = strClearArtOnly

        'ClearLogo Only
        Dim strClearLogoOnly As String = Master.eLang.GetString(1123, "ClearLogo Only")
        mnuScrapeModifierClearLogo.Text = strClearLogoOnly

        'Close
        Dim strClose As String = Master.eLang.Close
        lblFilterCountriesClose_Movie.Text = strClose
        lblFilterDataFieldsClose_Movie.Text = strClose
        lblFilterGenresClose_Movie.Text = strClose
        lblFilterGenresClose_TVShow.Text = strClose
        lblFilterSourcesClose_Movie.Text = strClose
        lblFilterSourcesClose_TVShow.Text = strClose
        lblFilterTagsClose_Movies.Text = strClose
        lblFilterVideoSourcesClose_Movie.Text = strClose

        'Current Filter
        Dim strCurrentFilter As String = Master.eLang.GetString(624, "Current Filter")
        mnuScrapeSubmenuFilter.Text = strCurrentFilter

        'Countries
        Dim strCountries As String = Master.eLang.GetString(237, "Countries")
        lblCountriesHeader.Text = strCountries

        'DiscArt Only
        Dim strDiscArtOnly As String = Master.eLang.GetString(1124, "DiscArt Only")
        mnuScrapeModifierDiscArt.Text = strDiscArtOnly

        'Edit Data Fields
        Dim strEditDataField As String = Master.eLang.GetString(1087, "Clear or Replace Data Fields")
        cmnuEpisodeEditDataFields.Text = String.Concat(strEditDataField, "...")
        cmnuMovieEditDataFields.Text = String.Concat(strEditDataField, "...")
        cmnuMovieSetEditDataFields.Text = String.Concat(strEditDataField, "...")
        cmnuSeasonEditDataFields.Text = String.Concat(strEditDataField, "...")
        cmnuShowEditDataFields.Text = String.Concat(strEditDataField, "...")

        'Edit Genres
        Dim strEditGenres As String = Master.eLang.GetString(1051, "Edit Genres")
        cmnuMovieEditGenres.Text = strEditGenres
        cmnuShowEditGenres.Text = strEditGenres

        'Edit Meta Data
        Dim strEditMetaData As String = Master.eLang.GetString(603, "Edit Metadata")
        cmnuEpisodeEditMetadata.Text = strEditMetaData
        cmnuMovieEditMetadata.Text = strEditMetaData

        'Edit Movie Sorting
        Dim strEditMovieSorting As String = Master.eLang.GetString(939, "Edit Movie Sorting")
        cmnuMovieSetEditSortMethod.Text = strEditMovieSorting

        'Edit Season
        Dim strEditSeason As String = Master.eLang.GetString(769, "Edit Season")
        cmnuSeasonEdit.Text = strEditSeason

        'Edit Tags
        Dim strEditTags As String = Master.eLang.GetString(1052, "Edit Tags")
        cmnuMovieEditTags.Text = strEditTags
        cmnuShowEditTags.Text = strEditTags

        'Episodes
        Dim strEpisodes As String = Master.eLang.GetString(682, "Episodes")
        gbFilterSpecificEpisodes_Shows.Text = strEpisodes

        'Extrafanarts Only
        Dim strExtrafanartsOnly As String = Master.eLang.GetString(975, "Extrafanarts Only")
        mnuScrapeModifierExtrafanarts.Text = strExtrafanartsOnly

        'Extrathumbs Only
        Dim strExtrathumbsOnly As String = Master.eLang.GetString(74, "Extrathumbs Only")
        mnuScrapeModifierExtrathumbs.Text = strExtrathumbsOnly

        'Fanart Only
        Dim strFanartOnly As String = Master.eLang.GetString(73, "Fanart Only")
        mnuScrapeModifierFanart.Text = strFanartOnly

        'Filters (Active/Inactive)
        Dim strFilters As String = Master.eLang.GetString(52, "Filters")
        lblFilter_Movies.Text = String.Format("{0} ({1})", strFilters, If(_BsMovies.Filter Is Nothing, Master.eLang.GetString(1091, "Inactive"), Master.eLang.GetString(1090, "Active")))
        lblFilter_MovieSets.Text = String.Format("{0} ({1})", strFilters, If(_BsMoviesets.Filter Is Nothing, Master.eLang.GetString(1091, "Inactive"), Master.eLang.GetString(1090, "Active")))
        lblFilter_Shows.Text = String.Format("{0} ({1})", strFilters, If(_BsTVShows.Filter Is Nothing, Master.eLang.GetString(1091, "Inactive"), Master.eLang.GetString(1090, "Active")))

        'KeyArt Only
        Dim strKeyArtOnly As String = Master.eLang.GetString(303, "KeyArt Only")
        mnuScrapeModifierKeyArt.Text = strKeyArtOnly

        'Landscape Only
        Dim strLandscapeOnly As String = Master.eLang.GetString(1061, "Landscape Only")
        mnuScrapeModifierLandscape.Text = strLandscapeOnly

        'List
        Dim strList As String = Master.eLang.GetString(1394, "List")
        gbFilterList_Movies.Text = strList
        gbFilterList_MovieSets.Text = strList
        gbFilterList_Shows.Text = strList

        'Lock
        Dim strLock As String = Master.eLang.GetString(24, "Lock")
        cmnuEpisodeLock.Text = strLock
        cmnuMovieLock.Text = strLock
        cmnuMoviesetLock.Text = strLock
        cmnuSeasonLock.Text = strLock
        cmnuShowLock.Text = strLock

        'Locked
        Dim strLocked As String = Master.eLang.GetString(43, "Locked")
        chkFilterLock_Movies.Text = strLocked
        chkFilterLock_MovieSets.Text = strLocked
        chkFilterLock_Shows.Text = strLocked
        chkFilterLockEpisodes_Shows.Text = strLocked

        'Mark
        Dim strMark As String = Master.eLang.GetString(23, "Mark")
        cmnuEpisodeMark.Text = strMark
        cmnuMovieMark.Text = strMark
        cmnuMoviesetMark.Text = strMark
        cmnuSeasonMark.Text = strMark
        cmnuShowMark.Text = strMark

        'Mark as Watched
        Dim strMarkAsWatched As String = Master.eLang.GetString(1072, "Mark as Watched")
        cmnuMovieWatched.Text = strMarkAsWatched
        cmnuEpisodeWatched.Text = strMarkAsWatched
        cmnuSeasonWatched.Text = strMarkAsWatched
        cmnuShowWatched.Text = strMarkAsWatched

        'Mark as Unwatched
        Dim strMarkAsUnwatched As String = Master.eLang.GetString(1073, "Mark as Unatched")
        cmnuMovieUnwatched.Text = strMarkAsUnwatched
        cmnuEpisodeUnwatched.Text = strMarkAsUnwatched
        cmnuSeasonUnwatched.Text = strMarkAsUnwatched
        cmnuShowUnwatched.Text = strMarkAsUnwatched

        'Marked
        Dim strMarked As String = Master.eLang.GetString(48, "Marked")
        chkFilterMark_Movies.Text = strMarked
        chkFilterMark_MovieSets.Text = strMarked
        chkFilterMark_Shows.Text = strMarked
        chkFilterMarkEpisodes_Shows.Text = strMarked
        mnuScrapeSubmenuMarked.Text = strMarked

        'Meta Data Only
        Dim strMetaDataOnly As String = Master.eLang.GetString(76, "Metadata Only")
        mnuScrapeModifierMetadata.Text = strMetaDataOnly

        'Missing Items
        Dim strMissingItems As String = Master.eLang.GetString(40, "Missing Items")
        btnFilterMissing_Movies.Text = strMissingItems
        btnFilterMissing_MovieSets.Text = strMissingItems
        btnFilterMissing_Shows.Text = strMissingItems
        mnuScrapeSubmenuMissing.Text = strMissingItems

        'New
        Dim strNew As String = Master.eLang.GetString(47, "New")
        chkFilterNew_Movies.Text = strNew
        chkFilterNew_MovieSets.Text = strNew
        chkFilterNewEpisodes_Shows.Text = strNew
        chkFilterNewShows_Shows.Text = strNew
        mnuScrapeSubmenuNew.Text = strNew

        'NFO Only
        Dim strNFOOnly As String = Master.eLang.GetString(71, "NFO Only")
        mnuScrapeModifierNFO.Text = strNFOOnly
        mnuMainToolsRewriteContentMovieNFO.Text = strNFOOnly
        mnuMainToolsRewriteContentMovieSetNFO.Text = strNFOOnly
        mnuMainToolsRewriteContentTVShowNFO.Text = strNFOOnly

        'Open Fanart.tv-Page
        Dim strOpenFanartTVPage As String = Master.eLang.GetString(1093, "Open Fanart.tv-Page")

        'Open IMDB-Page
        Dim strOpenIMDBPage As String = Master.eLang.GetString(1281, "Open IMDB-Page")
        cmnuEpisodeBrowseIMDB.Text = strOpenIMDBPage
        cmnuMovieBrowseIMDB.Text = strOpenIMDBPage
        cmnuSeasonBrowseIMDB.Text = strOpenIMDBPage
        cmnuShowBrowseIMDB.Text = strOpenIMDBPage

        'Open TMDB-Page
        Dim strOpenTMDBPage As String = Master.eLang.GetString(1282, "Open TMDB-Page")
        cmnuEpisodeBrowseTMDB.Text = strOpenTMDBPage
        cmnuMovieBrowseTMDB.Text = strOpenTMDBPage
        cmnuMovieSetBrowseTMDB.Text = strOpenTMDBPage
        cmnuSeasonBrowseTMDB.Text = strOpenTMDBPage
        cmnuShowBrowseTMDB.Text = strOpenTMDBPage

        'Open TVDB-Page
        Dim strOpenTVDBPage As String = Master.eLang.GetString(1092, "Open TVDB-Page")
        cmnuEpisodeBrowseTVDB.Text = strOpenTVDBPage
        cmnuSeasonBrowseTVDB.Text = strOpenTVDBPage
        cmnuShowBrowseTVDB.Text = strOpenTVDBPage

        'Poster Only
        Dim strPosterOnly As String = Master.eLang.GetString(72, "Poster Only")
        mnuScrapeModifierPoster.Text = strPosterOnly

        'Release Date
        Dim strReleaseDate As String = Master.eLang.GetString(57, "Release Date")
        btnFilterSortReleaseDate_Movies.Text = strReleaseDate

        'Reload All Movies
        Dim strReloadAllMovies As String = Master.eLang.GetString(18, "Reload All Movies")
        cmnuTrayToolsReloadMovies.Text = strReloadAllMovies
        mnuMainToolsReloadMovies.Text = strReloadAllMovies

        'Reload All MovieSets
        Dim strReloadAllMovieSets As String = Master.eLang.GetString(1208, "Reload All MovieSets")
        cmnuTrayToolsReloadMovieSets.Text = strReloadAllMovieSets
        mnuMainToolsReloadMovieSets.Text = strReloadAllMovieSets

        'Reload All TV Shows
        Dim strReloadAllTVShows As String = Master.eLang.GetString(250, "Reload All TV Shows")
        cmnuTrayToolsReloadTVShows.Text = strReloadAllTVShows
        mnuMainToolsReloadTVShows.Text = strReloadAllTVShows

        'Remove
        Dim strRemove As String = Master.eLang.GetString(30, "Remove")
        mnuGenresRemove.Text = strRemove
        mnuTagsRemove.Text = strRemove

        'Scrape Movies
        Dim strScrapeMovies As String = Master.eLang.GetString(67, "Scrape Movies")
        mnuScrapeMovies.Text = strScrapeMovies
        cmnuTrayScrapeMovies.Text = strScrapeMovies

        'Scrape MovieSets
        Dim strScrapeMovieSets As String = Master.eLang.GetString(1213, "Scrape MovieSets")
        mnuScrapeMoviesets.Text = strScrapeMovieSets
        cmnuTrayScrapeMovieSets.Text = strScrapeMovieSets

        'Scrape TV Shows
        Dim strScrapeTVShows As String = Master.eLang.GetString(1234, "Scrape TV Shows")
        mnuScrapeTVShows.Text = strScrapeTVShows
        cmnuTrayScrapeTVShows.Text = strScrapeTVShows

        'Select Profile
        Dim strSelectProfile As String = Master.eLang.GetString(1101, "Select profile")
        mnuMainFileProfile.Text = String.Concat(strSelectProfile, "...")

        'Set
        Dim strSet As String = Master.eLang.GetString(29, "Set")
        cmnuMovieSetEditSortMethodSet.Text = strSet
        mnuGenresSet.Text = strSet
        mnuLanguagesSet.Text = strSet
        mnuMoviesetsSet.Text = strSet
        mnuTagsSet.Text = strSet

        'Select Genre
        Dim strSelectGenre As String = Master.eLang.GetString(27, "Select Genre")
        mnuGenresTitleSelect.Text = String.Concat(">> ", strSelectGenre, " <<")

        'Select Tag
        Dim strSelectTag As String = Master.eLang.GetString(1021, "Select Tag")
        mnuTagsTitleSelect.Text = String.Concat(">> ", strSelectTag, " <<")

        'Shows
        Dim strShows As String = Master.eLang.GetString(680, "Shows")
        gbFilterGeneral_Shows.Text = strShows
        gbFilterSpecificShows_Shows.Text = strShows

        'Skip (Skip If More Than One Match)
        Dim strSkip As String = Master.eLang.GetString(1041, "Skip (Skip If More Than One Match)")
        mnuScrapeTypeSkip.Text = strSkip

        'Tags
        Dim strTags As String = Master.eLang.GetString(243, "Tags")
        mnuScrapeOptionTags.Text = strTags
        lblTagsHeader.Text = strTags

        'Theme Only
        Dim strThemeOnly As String = Master.eLang.GetString(1125, "Theme Only")
        mnuScrapeModifierTheme.Text = strThemeOnly

        'Trailer Only
        Dim strTrailerOnly As String = Master.eLang.GetString(75, "Trailer Only")
        mnuScrapeModifierTrailer.Text = strTrailerOnly

        'Unlock
        Dim strUnlock As String = Master.eLang.GetString(108, "Unlock")
        cmnuEpisodeUnlock.Text = strUnlock
        cmnuMovieUnlock.Text = strUnlock
        cmnuMoviesetUnlock.Text = strUnlock
        cmnuSeasonUnlock.Text = strUnlock
        cmnuShowUnlock.Text = strUnlock

        'Unmark
        Dim strUnmark As String = Master.eLang.GetString(107, "Unmark")
        cmnuEpisodeUnmark.Text = strUnmark
        cmnuMovieUnmark.Text = strUnmark
        cmnuMoviesetUnmark.Text = strUnmark
        cmnuSeasonUnmark.Text = strUnmark
        cmnuShowUnmark.Text = strUnmark

        'Update Single Data Field
        Dim strUpdateSingelDataField As String = Master.eLang.GetString(1126, "(Re)Scrape Single Data Field")
        cmnuEpisodeScrapeSingleDataField.Text = strUpdateSingelDataField
        cmnuMovieScrapeSingleDataField.Text = strUpdateSingelDataField
        cmnuMovieSetScrapeSingleDataField.Text = strUpdateSingelDataField
        cmnuSeasonScrapeSingleDataField.Text = strUpdateSingelDataField
        cmnuShowScrapeSingleDataField.Text = strUpdateSingelDataField

        ' others
        btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
        btnClearFilters_Movies.Text = Master.eLang.GetString(37, "Clear Filters")
        btnClearFilters_MovieSets.Text = btnClearFilters_Movies.Text
        btnClearFilters_Shows.Text = btnClearFilters_Movies.Text
        btnMarkAll.Text = Master.eLang.GetString(35, "Mark All")
        btnUnmarkAll.Text = Master.eLang.GetString(105, "Unmark All")
        btnMetaDataRefresh.Text = Master.eLang.GetString(58, "Refresh")
        btnFilterSortDateAdded_Movies.Tag = String.Empty
        btnFilterSortDateAdded_Movies.Text = Master.eLang.GetString(601, "Date Added")
        btnFilterSortDateModified_Movies.Tag = String.Empty
        btnFilterSortDateModified_Movies.Text = Master.eLang.GetString(1330, "Date Modified")
        btnFilterSortRating_Movies.Tag = String.Empty
        btnFilterSortRating_Movies.Text = Master.eLang.GetString(400, "Rating")
        btnFilterSortTitle_Movies.Tag = String.Empty
        btnFilterSortTitle_Movies.Text = Master.eLang.GetString(642, "Sort Title")
        btnFilterSortTitle_Shows.Tag = String.Empty
        btnFilterSortTitle_Shows.Text = Master.eLang.GetString(642, "Sort Title")
        btnFilterSortYear_Movies.Tag = String.Empty
        btnFilterSortYear_Movies.Text = Master.eLang.GetString(278, "Year")
        chkFilterDuplicates_Movies.Text = Master.eLang.GetString(41, "Duplicates")
        chkFilterEmpty_MovieSets.Text = Master.eLang.GetString(1275, "Empty")
        chkFilterMarkCustom1_Movies.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker1Name), Master.eSettings.MovieGeneralCustomMarker1Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #1"))
        chkFilterMarkCustom2_Movies.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker2Name), Master.eSettings.MovieGeneralCustomMarker2Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #2"))
        chkFilterMarkCustom3_Movies.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker3Name), Master.eSettings.MovieGeneralCustomMarker3Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #3"))
        chkFilterMarkCustom4_Movies.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker4Name), Master.eSettings.MovieGeneralCustomMarker4Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #4"))
        chkFilterMultiple_MovieSets.Text = Master.eLang.GetString(876, "Multiple Movies")
        chkFilterOne_MovieSets.Text = Master.eLang.GetString(1289, "Only One Movie")
        chkFilterTolerance_Movies.Text = Master.eLang.GetString(39, "Out of Tolerance")

        RemoveHandler chkMovieMissingBanner.CheckedChanged, AddressOf chkMovieMissingBanner_CheckedChanged
        chkMovieMissingBanner.Checked = Master.eSettings.MovieMissingBanner
        AddHandler chkMovieMissingBanner.CheckedChanged, AddressOf chkMovieMissingBanner_CheckedChanged

        RemoveHandler chkMovieMissingClearArt.CheckedChanged, AddressOf chkMovieMissingClearArt_CheckedChanged
        chkMovieMissingClearArt.Checked = Master.eSettings.MovieMissingClearArt
        AddHandler chkMovieMissingClearArt.CheckedChanged, AddressOf chkMovieMissingClearArt_CheckedChanged

        RemoveHandler chkMovieMissingClearLogo.CheckedChanged, AddressOf chkMovieMissingClearLogo_CheckedChanged
        chkMovieMissingClearLogo.Checked = Master.eSettings.MovieMissingClearLogo
        AddHandler chkMovieMissingClearLogo.CheckedChanged, AddressOf chkMovieMissingClearLogo_CheckedChanged

        RemoveHandler chkMovieMissingDiscArt.CheckedChanged, AddressOf chkMovieMissingDiscArt_CheckedChanged
        chkMovieMissingDiscArt.Checked = Master.eSettings.MovieMissingDiscArt
        AddHandler chkMovieMissingDiscArt.CheckedChanged, AddressOf chkMovieMissingDiscArt_CheckedChanged

        RemoveHandler chkMovieMissingExtrafanarts.CheckedChanged, AddressOf chkMovieMissingExtrafanarts_CheckedChanged
        chkMovieMissingExtrafanarts.Checked = Master.eSettings.MovieMissingExtrafanarts
        AddHandler chkMovieMissingExtrafanarts.CheckedChanged, AddressOf chkMovieMissingExtrafanarts_CheckedChanged

        RemoveHandler chkMovieMissingExtrathumbs.CheckedChanged, AddressOf chkMovieMissingExtrathumbs_CheckedChanged
        chkMovieMissingExtrathumbs.Checked = Master.eSettings.MovieMissingExtrathumbs
        AddHandler chkMovieMissingExtrathumbs.CheckedChanged, AddressOf chkMovieMissingExtrathumbs_CheckedChanged

        RemoveHandler chkMovieMissingFanart.CheckedChanged, AddressOf chkMovieMissingFanart_CheckedChanged
        chkMovieMissingFanart.Checked = Master.eSettings.MovieMissingFanart
        AddHandler chkMovieMissingFanart.CheckedChanged, AddressOf chkMovieMissingFanart_CheckedChanged

        RemoveHandler chkMovieMissingKeyArt.CheckedChanged, AddressOf chkMovieMissingKeyArt_CheckedChanged
        chkMovieMissingKeyArt.Checked = Master.eSettings.MovieMissingKeyArt
        AddHandler chkMovieMissingKeyArt.CheckedChanged, AddressOf chkMovieMissingKeyArt_CheckedChanged

        RemoveHandler chkMovieMissingLandscape.CheckedChanged, AddressOf chkMovieMissingLandscape_CheckedChanged
        chkMovieMissingLandscape.Checked = Master.eSettings.MovieMissingLandscape
        AddHandler chkMovieMissingLandscape.CheckedChanged, AddressOf chkMovieMissingLandscape_CheckedChanged

        RemoveHandler chkMovieMissingNFO.CheckedChanged, AddressOf chkMovieMissingNFO_CheckedChanged
        chkMovieMissingNFO.Checked = Master.eSettings.MovieMissingNFO
        AddHandler chkMovieMissingNFO.CheckedChanged, AddressOf chkMovieMissingNFO_CheckedChanged

        RemoveHandler chkMovieMissingPoster.CheckedChanged, AddressOf chkMovieMissingPoster_CheckedChanged
        chkMovieMissingPoster.Checked = Master.eSettings.MovieMissingPoster
        AddHandler chkMovieMissingPoster.CheckedChanged, AddressOf chkMovieMissingPoster_CheckedChanged

        RemoveHandler chkMovieMissingSubtitles.CheckedChanged, AddressOf chkMovieMissingSubtitles_CheckedChanged
        chkMovieMissingSubtitles.Checked = Master.eSettings.MovieMissingSubtitles
        AddHandler chkMovieMissingSubtitles.CheckedChanged, AddressOf chkMovieMissingSubtitles_CheckedChanged

        RemoveHandler chkMovieMissingTheme.CheckedChanged, AddressOf chkMovieMissingTheme_CheckedChanged
        chkMovieMissingTheme.Checked = Master.eSettings.MovieMissingTheme
        AddHandler chkMovieMissingTheme.CheckedChanged, AddressOf chkMovieMissingTheme_CheckedChanged

        RemoveHandler chkMovieMissingTrailer.CheckedChanged, AddressOf chkMovieMissingTrailer_CheckedChanged
        chkMovieMissingTrailer.Checked = Master.eSettings.MovieMissingTrailer
        AddHandler chkMovieMissingTrailer.CheckedChanged, AddressOf chkMovieMissingTrailer_CheckedChanged

        RemoveHandler chkMovieSetMissingBanner.CheckedChanged, AddressOf chkMovieSetMissingBanner_CheckedChanged
        chkMovieSetMissingBanner.Checked = Master.eSettings.MovieSetMissingBanner
        AddHandler chkMovieSetMissingBanner.CheckedChanged, AddressOf chkMovieSetMissingBanner_CheckedChanged

        RemoveHandler chkMovieSetMissingClearArt.CheckedChanged, AddressOf chkMovieSetMissingClearArt_CheckedChanged
        chkMovieSetMissingClearArt.Checked = Master.eSettings.MovieSetMissingClearArt
        AddHandler chkMovieSetMissingClearArt.CheckedChanged, AddressOf chkMovieSetMissingClearArt_CheckedChanged

        RemoveHandler chkMovieSetMissingClearLogo.CheckedChanged, AddressOf chkMovieSetMissingClearLogo_CheckedChanged
        chkMovieSetMissingClearLogo.Checked = Master.eSettings.MovieSetMissingClearLogo
        AddHandler chkMovieSetMissingClearLogo.CheckedChanged, AddressOf chkMovieSetMissingClearLogo_CheckedChanged

        RemoveHandler chkMovieSetMissingDiscArt.CheckedChanged, AddressOf chkMovieSetMissingDiscArt_CheckedChanged
        chkMovieSetMissingDiscArt.Checked = Master.eSettings.MovieSetMissingDiscArt
        AddHandler chkMovieSetMissingDiscArt.CheckedChanged, AddressOf chkMovieSetMissingDiscArt_CheckedChanged

        RemoveHandler chkMovieSetMissingFanart.CheckedChanged, AddressOf chkMovieSetMissingFanart_CheckedChanged
        chkMovieSetMissingFanart.Checked = Master.eSettings.MovieSetMissingFanart
        AddHandler chkMovieSetMissingFanart.CheckedChanged, AddressOf chkMovieSetMissingFanart_CheckedChanged

        RemoveHandler chkMovieSetMissingKeyArt.CheckedChanged, AddressOf chkMovieSetMissingKeyArt_CheckedChanged
        chkMovieSetMissingKeyArt.Checked = Master.eSettings.MovieSetMissingKeyArt
        AddHandler chkMovieSetMissingKeyArt.CheckedChanged, AddressOf chkMovieSetMissingKeyArt_CheckedChanged

        RemoveHandler chkMovieSetMissingLandscape.CheckedChanged, AddressOf chkMovieSetMissingLandscape_CheckedChanged
        chkMovieSetMissingLandscape.Checked = Master.eSettings.MovieSetMissingLandscape
        AddHandler chkMovieSetMissingLandscape.CheckedChanged, AddressOf chkMovieSetMissingLandscape_CheckedChanged

        RemoveHandler chkMovieSetMissingNFO.CheckedChanged, AddressOf chkMovieSetMissingNFO_CheckedChanged
        chkMovieSetMissingNFO.Checked = Master.eSettings.MovieSetMissingNFO
        AddHandler chkMovieSetMissingNFO.CheckedChanged, AddressOf chkMovieSetMissingNFO_CheckedChanged

        RemoveHandler chkMovieSetMissingPoster.CheckedChanged, AddressOf chkMovieSetMissingPoster_CheckedChanged
        chkMovieSetMissingPoster.Checked = Master.eSettings.MovieSetMissingPoster
        AddHandler chkMovieSetMissingPoster.CheckedChanged, AddressOf chkMovieSetMissingPoster_CheckedChanged

        RemoveHandler chkShowMissingBanner.CheckedChanged, AddressOf chkShowMissingBanner_CheckedChanged
        chkShowMissingBanner.Checked = Master.eSettings.TVShowMissingBanner
        AddHandler chkShowMissingBanner.CheckedChanged, AddressOf chkShowMissingBanner_CheckedChanged

        RemoveHandler chkShowMissingCharacterArt.CheckedChanged, AddressOf chkShowMissingCharacterArt_CheckedChanged
        chkShowMissingCharacterArt.Checked = Master.eSettings.TVShowMissingCharacterArt
        AddHandler chkShowMissingCharacterArt.CheckedChanged, AddressOf chkShowMissingCharacterArt_CheckedChanged

        RemoveHandler chkShowMissingClearArt.CheckedChanged, AddressOf chkShowMissingClearArt_CheckedChanged
        chkShowMissingClearArt.Checked = Master.eSettings.TVShowMissingClearArt
        AddHandler chkShowMissingClearArt.CheckedChanged, AddressOf chkShowMissingClearArt_CheckedChanged

        RemoveHandler chkShowMissingClearLogo.CheckedChanged, AddressOf chkShowMissingClearLogo_CheckedChanged
        chkShowMissingClearLogo.Checked = Master.eSettings.TVShowMissingClearLogo
        AddHandler chkShowMissingClearLogo.CheckedChanged, AddressOf chkShowMissingClearLogo_CheckedChanged

        RemoveHandler chkShowMissingExtrafanarts.CheckedChanged, AddressOf chkShowMissingExtrafanarts_CheckedChanged
        chkShowMissingExtrafanarts.Checked = Master.eSettings.TVShowMissingExtrafanarts
        AddHandler chkShowMissingExtrafanarts.CheckedChanged, AddressOf chkShowMissingExtrafanarts_CheckedChanged

        RemoveHandler chkShowMissingFanart.CheckedChanged, AddressOf chkShowMissingFanart_CheckedChanged
        chkShowMissingFanart.Checked = Master.eSettings.TVShowMissingFanart
        AddHandler chkShowMissingFanart.CheckedChanged, AddressOf chkShowMissingFanart_CheckedChanged

        RemoveHandler chkShowMissingKeyArt.CheckedChanged, AddressOf chkShowMissingKeyArt_CheckedChanged
        chkShowMissingKeyArt.Checked = Master.eSettings.TVShowMissingKeyArt
        AddHandler chkShowMissingKeyArt.CheckedChanged, AddressOf chkShowMissingKeyArt_CheckedChanged

        RemoveHandler chkShowMissingLandscape.CheckedChanged, AddressOf chkShowMissingLandscape_CheckedChanged
        chkShowMissingLandscape.Checked = Master.eSettings.TVShowMissingLandscape
        AddHandler chkShowMissingLandscape.CheckedChanged, AddressOf chkShowMissingLandscape_CheckedChanged

        RemoveHandler chkShowMissingNFO.CheckedChanged, AddressOf chkShowMissingNFO_CheckedChanged
        chkShowMissingNFO.Checked = Master.eSettings.TVShowMissingNFO
        AddHandler chkShowMissingNFO.CheckedChanged, AddressOf chkShowMissingNFO_CheckedChanged

        RemoveHandler chkShowMissingPoster.CheckedChanged, AddressOf chkShowMissingPoster_CheckedChanged
        chkShowMissingPoster.Checked = Master.eSettings.TVShowMissingPoster
        AddHandler chkShowMissingPoster.CheckedChanged, AddressOf chkShowMissingPoster_CheckedChanged

        RemoveHandler chkShowMissingTheme.CheckedChanged, AddressOf chkShowMissingTheme_CheckedChanged
        chkShowMissingTheme.Checked = Master.eSettings.TVShowMissingTheme
        AddHandler chkShowMissingTheme.CheckedChanged, AddressOf chkShowMissingTheme_CheckedChanged

        cmnuEpisodeChange.Text = Master.eLang.GetString(772, "Change Episode")
        cmnuEpisodeEdit.Text = Master.eLang.GetString(656, "Edit Episode")
        cmnuEpisodeReload.Text = Master.eLang.GetString(22, "Reload")
        cmnuEpisodeRemove.Text = Master.eLang.GetString(30, "Remove")
        cmnuEpisodeRemoveFromDB.Text = Master.eLang.GetString(646, "Remove from Database")
        cmnuEpisodeRemoveFromDisk.Text = Master.eLang.GetString(773, "Delete Episode")
        cmnuEpisodeScrape.Text = Master.eLang.GetString(147, "(Re)Scrape Episode")
        cmnuMovieChange.Text = Master.eLang.GetString(32, "Change Movie")
        cmnuMovieChangeAuto.Text = Master.eLang.GetString(1294, "Change Movie (Auto)")
        cmnuMovieEdit.Text = Master.eLang.GetString(25, "Edit Movie")
        cmnuMovieLock.Text = Master.eLang.GetString(24, "Lock")
        cmnuMovieMarkAs.Text = Master.eLang.GetString(1192, "Mark as")
        cmnuMovieMarkAsCustom1.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker1Name), Master.eSettings.MovieGeneralCustomMarker1Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #1"))
        cmnuMovieMarkAsCustom1.ForeColor = Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker1Color)
        cmnuMovieMarkAsCustom2.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker2Name), Master.eSettings.MovieGeneralCustomMarker2Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #2"))
        cmnuMovieMarkAsCustom2.ForeColor = Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker2Color)
        cmnuMovieMarkAsCustom3.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker3Name), Master.eSettings.MovieGeneralCustomMarker3Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #3"))
        cmnuMovieMarkAsCustom3.ForeColor = Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker3Color)
        cmnuMovieMarkAsCustom4.Text = If(Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralCustomMarker4Name), Master.eSettings.MovieGeneralCustomMarker4Name, String.Concat(Master.eLang.GetString(1191, "Custom"), " #4"))
        cmnuMovieMarkAsCustom4.ForeColor = Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker4Color)
        cmnuMovieOpenFolder.Text = Master.eLang.GetString(33, "Open Containing Folder")
        cmnuMovieReload.Text = Master.eLang.GetString(22, "Reload")
        cmnuMovieRemove.Text = Master.eLang.GetString(30, "Remove")
        cmnuMovieRemoveFromDB.Text = Master.eLang.GetString(646, "Remove From Database")
        cmnuMovieRemoveFromDisk.Text = Master.eLang.GetString(34, "Delete Movie")
        cmnuMovieScrape.Text = Master.eLang.GetString(163, "(Re)Scrape Movie")
        cmnuMovieScrapeSelected.Text = Master.eLang.GetString(31, "(Re)Scrape Selected Movies")
        cmnuMovieSetEdit.Text = Master.eLang.GetString(207, "Edit MovieSet")
        cmnuMovieSetNew.Text = Master.eLang.GetString(208, "Add New MovieSet")
        cmnuMovieSetScrape.Text = Master.eLang.GetString(1233, "(Re)Scrape MovieSet")
        cmnuMovieTitle.Text = Master.eLang.GetString(21, "Title")
        cmnuSeasonRemoveFromDB.Text = Master.eLang.GetString(646, "Remove from Database")
        cmnuSeasonReload.Text = Master.eLang.GetString(22, "Reload")
        cmnuSeasonRemove.Text = Master.eLang.GetString(30, "Remove")
        cmnuSeasonRemoveFromDisk.Text = Master.eLang.GetString(771, "Delete Season")
        cmnuSeasonScrape.Text = Master.eLang.GetString(146, "(Re)Scrape Season")
        cmnuShowChange.Text = Master.eLang.GetString(767, "Change TV Show")
        cmnuShowClearCache.Text = Master.eLang.GetString(565, "Clear Cache")
        cmnuShowClearCacheDataAndImages.Text = Master.eLang.GetString(583, "Data and Images")
        cmnuShowClearCacheDataOnly.Text = Master.eLang.GetString(566, "Data Only")
        cmnuShowClearCacheImagesOnly.Text = Master.eLang.GetString(567, "Images Only")
        cmnuShowEdit.Text = Master.eLang.GetString(663, "Edit Show")
        cmnuShowEdit.Text = Master.eLang.GetString(663, "Edit Show")
        cmnuShowReload.Text = Master.eLang.GetString(22, "Reload")
        cmnuShowRemove.Text = Master.eLang.GetString(30, "Remove")
        cmnuShowRemoveFromDB.Text = Master.eLang.GetString(646, "Remove from Database")
        cmnuShowRemoveFromDisk.Text = Master.eLang.GetString(768, "Delete TV Show")
        cmnuShowGetMissingEpisodes.Text = Master.eLang.GetString(1099, "Get Missing Episodes")
        cmnuShowScrape.Text = Master.eLang.GetString(766, "(Re)Scrape Show")
        cmnuTrayExit.Text = Master.eLang.GetString(2, "E&xit")
        cmnuTraySettings.Text = Master.eLang.GetString(4, "&Settings...")
        cmnuTrayTools.Text = Master.eLang.GetString(8, "&Tools")
        cmnuTrayUpdate.Text = Master.eLang.GetString(82, "Update Library")
        gbFilterDataField_Movies.Text = String.Concat(Master.eLang.GetString(1290, "Data Field"), ":")
        gbFilterGeneral_Movies.Text = Master.eLang.GetString(38, "General")
        gbFilterGeneral_MovieSets.Text = gbFilterGeneral_Movies.Text
        gbFilterModifier_Movies.Text = Master.eLang.GetString(44, "Modifier")
        gbFilterModifier_MovieSets.Text = gbFilterModifier_Movies.Text
        gbFilterModifier_Shows.Text = gbFilterModifier_Movies.Text
        gbFilterSpecific_Movies.Text = Master.eLang.GetString(42, "Specific")
        gbFilterSpecific_MovieSets.Text = gbFilterSpecific_Movies.Text
        gbFilterSpecific_Shows.Text = gbFilterSpecific_Movies.Text
        gbFilterSorting_Movies.Text = Master.eLang.GetString(600, "Extra Sorting")
        gbFilterSorting_Shows.Text = gbFilterSorting_Movies.Text
        lblActorsHeader.Text = Master.eLang.GetString(231, "Actors")
        lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
        lblCertificationsHeader.Text = Master.eLang.GetString(56, "Certifications")
        lblCharacterArtTitle.Text = Master.eLang.GetString(1140, "CharacterArt")
        lblClearArtTitle.Text = Master.eLang.GetString(1096, "ClearArt")
        lblClearLogoTitle.Text = Master.eLang.GetString(1097, "ClearLogo")
        lblCreditsHeader.Text = Master.eLang.GetString(394, "Credits (Writers)")
        lblDirectorsHeader.Text = Master.eLang.GetString(940, "Directors")
        lblDiscArtTitle.Text = Master.eLang.GetString(1098, "DiscArt")
        lblFanartSmallTitle.Text = Master.eLang.GetString(149, "Fanart")
        lblFilePathHeader.Text = Master.eLang.GetString(60, "File Path")
        lblFilterCountries_Movies.Text = Master.eLang.GetString(237, "Countries")
        lblFilterCountry_Movies.Text = String.Concat(Master.eLang.GetString(237, "Countries"), ":")
        lblFilterVideoSources_Movies.Text = Master.eLang.GetString(246, "Video Sources")
        lblFilterVideoSource_Movies.Text = String.Concat(Master.eLang.GetString(824, "Video Source"), ":")
        lblFilterGenre_Movies.Text = String.Concat(Master.eLang.GetString(725, "Genres"), ":")
        lblFilterGenre_Shows.Text = lblFilterGenre_Movies.Text
        lblFilterGenres_Movies.Text = Master.eLang.GetString(725, "Genres")
        lblFilterGenres_Shows.Text = lblFilterGenres_Movies.Text
        lblFilterSource_Movies.Text = Master.eLang.GetString(50, "Source:")
        lblFilterSource_Shows.Text = lblFilterSource_Movies.Text
        lblFilterSources_Movies.Text = Master.eLang.GetString(602, "Sources")
        lblFilterSources_Shows.Text = lblFilterSources_Movies.Text
        lblFilterYear_Movies.Text = String.Concat(Master.eLang.GetString(278, "Year"), ":")
        lblFilterDataFields_Movies.Text = Master.eLang.GetString(1290, "Data Field")
        lblIMDBHeader.Text = Master.eLang.GetString(61, "IMDB ID")
        lblInfoPanelHeader.Text = Master.eLang.GetString(66, "Info")
        lblLandscapeTitle.Text = Master.eLang.GetString(1035, "Landscape")
        lblLoadSettings.Text = Master.eLang.GetString(484, "Loading Settings...")
        lblMetaDataHeader.Text = Master.eLang.GetString(59, "Metadata")
        lblMoviesInSetHeader.Text = Master.eLang.GetString(367, "Movies In Set")
        lblOutlineHeader.Text = Master.eLang.GetString(64, "Plot Outline")
        lblPlotHeader.Text = Master.eLang.GetString(65, "Plot")
        lblPosterTitle.Text = Master.eLang.GetString(148, "Poster")
        lblPremieredHeader.Text = Master.eLang.GetString(724, "Premiered")
        lblTrailerPathHeader.Text = Master.eLang.GetString(1058, "Trailer Path")
        mnuMainDonate.Text = Master.eLang.GetString(708, "Donate")
        mnuMainDonate.Text = Master.eLang.GetString(708, "Donate")
        mnuMainEdit.Text = Master.eLang.GetString(3, "&Edit")
        mnuMainEditSettings.Text = Master.eLang.GetString(4, "&Settings...")
        mnuMainFile.Text = Master.eLang.GetString(1, "&File")
        mnuMainFileExit.Text = Master.eLang.GetString(2, "E&xit")
        mnuMainHelp.Text = Master.eLang.GetString(5, "&Help")
        mnuMainHelpAbout.Text = Master.eLang.GetString(6, "&About...")
        mnuMainHelpUpdate.Text = Master.eLang.GetString(850, "&Check For Updates...")
        mnuMainHelpVersions.Text = Master.eLang.GetString(793, "&Versions...")
        mnuMainHelpWiki.Text = Master.eLang.GetString(869, "Wiki")
        mnuMainToolsExport.Text = Master.eLang.GetString(1174, "Export")
        mnuMainToolsExportMovies.Text = Master.eLang.GetString(36, "Movies")
        mnuMainToolsExportTvShows.Text = Master.eLang.GetString(653, "TV Shows")
        mnuMainTools.Text = Master.eLang.GetString(8, "&Tools")
        mnuMainToolsBackdrops.Text = Master.eLang.GetString(11, "Copy Existing Fanart To Backdrops Folder")
        mnuMainToolsCleanDB.Text = Master.eLang.GetString(709, "Clean Database")
        mnuMainToolsCleanFiles.Text = Master.eLang.GetString(9, "&Clean Files")
        mnuMainToolsClearCache.Text = Master.eLang.GetString(17, "Clear All Caches")
        mnuMainToolsOfflineHolder.Text = Master.eLang.GetString(524, "&Offline Media Manager")
        mnuMainToolsRewriteContentMovie.Text = Master.eLang.GetString(1298, "Rewrite Movie Content")
        mnuMainToolsRewriteContentMovieSet.Text = Master.eLang.GetString(1094, "Rewrite MovieSet Content")
        mnuMainToolsRewriteContentTVShow.Text = Master.eLang.GetString(1095, "Rewrite TV Show Content")
        mnuMainToolsSortFiles.Text = Master.eLang.GetString(10, "&Sort Files Into Folders")
        mnuScrapeOptionActors.Text = Master.eLang.GetString(231, "Actors")
        mnuScrapeOptionAired.Text = Master.eLang.GetString(728, "Aired")
        mnuScrapeOptionCertifications.Text = Master.eLang.GetString(56, "Certification")
        mnuScrapeOptionCollection.Text = Master.eLang.GetString(424, "Collection")
        mnuScrapeOptionCountries.Text = Master.eLang.GetString(237, "Countries")
        mnuScrapeOptionCreators.Text = Master.eLang.GetString(744, "Creators")
        mnuScrapeOptionDirectors.Text = Master.eLang.GetString(940, "Directors")
        mnuScrapeOptionGenres.Text = Master.eLang.GetString(725, "Genres")
        mnuScrapeOptionGuestStars.Text = Master.eLang.GetString(508, "Guest Stars")
        mnuScrapeOptionMPAA.Text = Master.eLang.GetString(401, "MPAA")
        mnuScrapeOptionOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
        mnuScrapeOptionOutline.Text = Master.eLang.GetString(64, "Plot Outline")
        mnuScrapeOptionPlot.Text = Master.eLang.GetString(65, "Plot")
        mnuScrapeOptionPremiered.Text = Master.eLang.GetString(724, "Premiered")
        mnuScrapeOptionRating.Text = Master.eLang.GetString(400, "Rating")
        mnuScrapeOptionRuntime.Text = Master.eLang.GetString(238, "Runtime")
        mnuScrapeOptionStatus.Text = Master.eLang.GetString(215, "Status")
        mnuScrapeOptionStudios.Text = Master.eLang.GetString(226, "Studios")
        mnuScrapeOptionTagline.Text = Master.eLang.GetString(397, "Tagline")
        mnuScrapeOptionTitle.Text = Master.eLang.GetString(21, "Title")
        mnuScrapeOptionTop250.Text = Master.eLang.GetString(591, "Top 250")
        mnuScrapeOptionTrailer.Text = Master.eLang.GetString(151, "Trailer")
        mnuScrapeOptionUserRating.Text = Master.eLang.GetString(1467, "User Rating")
        mnuScrapeOptionWriters.Text = Master.eLang.GetString(777, "Writer")
        mnuUpdate.Text = Master.eLang.GetString(82, "Update Library")
        mnuUpdateMovies.Text = Master.eLang.GetString(36, "Movies")
        mnuUpdateShows.Text = Master.eLang.GetString(653, "TV Shows")
        pnlFilterCountries_Movies.Tag = String.Empty
        pnlFilterGenres_Movies.Tag = String.Empty
        pnlFilterGenres_Shows.Tag = String.Empty
        pnlFilterDataFields_Movies.Tag = String.Empty
        pnlFilterSources_Movies.Tag = String.Empty
        pnlFilterSources_Shows.Tag = String.Empty
        pnlFilterTags_Movies.Tag = String.Empty
        pnlFilterTags_Shows.Tag = String.Empty
        pnlFilterVideoSources_Movies.Tag = String.Empty
        rbFilterAnd_Movies.Text = Master.eLang.GetString(45, "And")
        rbFilterAnd_MovieSets.Text = rbFilterAnd_Movies.Text
        rbFilterAnd_Shows.Text = rbFilterAnd_Movies.Text
        rbFilterOr_Movies.Text = Master.eLang.GetString(46, "Or")
        rbFilterOr_MovieSets.Text = rbFilterOr_Movies.Text
        rbFilterOr_Shows.Text = rbFilterOr_Movies.Text
        tslLoading.Text = Master.eLang.GetString(7, "Loading Media:")

        cmnuEpisodeOpenFolder.Text = cmnuMovieOpenFolder.Text
        cmnuMoviesetLock.Text = cmnuMovieLock.Text
        cmnuMovieSetReload.Text = cmnuMovieReload.Text
        cmnuMovieSetRemove.Text = cmnuMovieRemove.Text
        cmnuSeasonOpenFolder.Text = cmnuMovieOpenFolder.Text
        cmnuShowOpenFolder.Text = cmnuMovieOpenFolder.Text
        cmnuTrayToolsBackdrops.Text = mnuMainToolsBackdrops.Text
        cmnuTrayToolsCleanFiles.Text = mnuMainToolsCleanFiles.Text
        cmnuTrayToolsClearCache.Text = mnuMainToolsClearCache.Text
        cmnuTrayToolsOfflineHolder.Text = mnuMainToolsOfflineHolder.Text
        cmnuTrayToolsSortFiles.Text = mnuMainToolsSortFiles.Text

        Dim TT As ToolTip = New ToolTip(components)
        mnuScrapeMovies.ToolTipText = Master.eLang.GetString(84, "Scrape/download data from the internet for multiple movies.")
        mnuScrapeMoviesets.ToolTipText = Master.eLang.GetString(1214, "Scrape/download data from the internet for multiple moviesets.")
        mnuScrapeTVShows.ToolTipText = Master.eLang.GetString(1235, "Scrape/download data from the internet for multiple tv shows.")
        mnuUpdate.ToolTipText = Master.eLang.GetString(85, "Scans sources for new content and cleans database.")
        TT.SetToolTip(btnMarkAll, Master.eLang.GetString(87, "Mark all items in the list"))
        TT.SetToolTip(btnUnmarkAll, Master.eLang.GetString(1100, "Unmark all items in the list"))
        TT.SetToolTip(txtSearchMovies, Master.eLang.GetString(88, "Search the movie titles by entering text here."))
        TT.SetToolTip(txtSearchMovieSets, Master.eLang.GetString(1267, "Search the movie titles by entering text here."))
        TT.SetToolTip(txtSearchShows, Master.eLang.GetString(1268, "Search the tv show titles by entering text here."))
        TT.SetToolTip(btnFilePlay, Master.eLang.GetString(89, "Play the movie file with the system default media player."))
        TT.SetToolTip(btnMetaDataRefresh, Master.eLang.GetString(90, "Rescan and save the Metadata for the selected video."))
        TT.SetToolTip(chkFilterDuplicates_Movies, Master.eLang.GetString(91, "Display only movies that have duplicate IMDB IDs."))
        TT.SetToolTip(chkFilterTolerance_Movies, Master.eLang.GetString(92, "Display only movies whose title matching is out of tolerance."))
        TT.SetToolTip(chkFilterMissing_Movies, Master.eLang.GetString(93, "Display only movies that have items missing."))
        TT.SetToolTip(chkFilterNew_Movies, Master.eLang.GetString(94, "Display only new movies."))
        TT.SetToolTip(chkFilterNew_MovieSets, Master.eLang.GetString(1269, "Display only new moviesets."))
        TT.SetToolTip(chkFilterMark_Movies, Master.eLang.GetString(95, "Display only marked movies."))
        TT.SetToolTip(chkFilterMark_MovieSets, Master.eLang.GetString(1270, "Display only marked moviesets."))
        TT.SetToolTip(chkFilterLock_Movies, Master.eLang.GetString(96, "Display only locked movies."))
        TT.SetToolTip(chkFilterLock_MovieSets, Master.eLang.GetString(1271, "Display only locked moviesets."))
        TT.SetToolTip(txtFilterSource_Movies, Master.eLang.GetString(97, "Display only movies from the selected source."))
        TT.Active = True

        RemoveHandler cbSearchMovies.SelectedIndexChanged, AddressOf cbSearchMovies_SelectedIndexChanged
        cbSearchMovies.Items.Clear()
        cbSearchMovies.Items.AddRange(New Object() {
                                      Master.eLang.GetString(21, "Title"),
                                      Master.eLang.GetString(302, "Original Title"),
                                      Master.eLang.GetString(100, "Actor"),
                                      Master.eLang.GetString(233, "Role"),
                                      Master.eLang.GetString(62, "Director"),
                                      Master.eLang.GetString(729, "Credits"),
                                      Master.eLang.GetString(301, "Country"),
                                      Master.eLang.GetString(395, "Studio")
                                      })
        If cbSearchMovies.Items.Count > 0 Then
            cbSearchMovies.SelectedIndex = 0
        End If
        AddHandler cbSearchMovies.SelectedIndexChanged, AddressOf cbSearchMovies_SelectedIndexChanged

        RemoveHandler cbSearchMovieSets.SelectedIndexChanged, AddressOf cbSearchMovieSets_SelectedIndexChanged
        cbSearchMovieSets.Items.Clear()
        cbSearchMovieSets.Items.AddRange(New Object() {
                                         Master.eLang.GetString(21, "Title"),
                                         String.Format("{0} ({1})", Master.eLang.GetString(21, "Title"), Master.eLang.GetString(1379, "Movie"))
                                         })
        If cbSearchMovieSets.Items.Count > 0 Then
            cbSearchMovieSets.SelectedIndex = 0
        End If
        AddHandler cbSearchMovieSets.SelectedIndexChanged, AddressOf cbSearchMovieSets_SelectedIndexChanged

        RemoveHandler cbSearchShows.SelectedIndexChanged, AddressOf cbSearchShows_SelectedIndexChanged
        cbSearchShows.Items.Clear()
        cbSearchShows.Items.AddRange(New Object() {
                                     Master.eLang.GetString(21, "Title"),
                                     Master.eLang.GetString(302, "Original Title"),
                                     Master.eLang.GetString(798, "Creator"),
                                     Master.eLang.GetString(301, "Country"),
                                     Master.eLang.GetString(395, "Studio")
                                     })
        If cbSearchShows.Items.Count > 0 Then
            cbSearchShows.SelectedIndex = 0
        End If
        AddHandler cbSearchShows.SelectedIndexChanged, AddressOf cbSearchShows_SelectedIndexChanged

        If doTheme Then
            tTheme = New Theming
            Dim currMainTabTag = MainTab_GetCurrentTag()
            Theme_Apply(currMainTabTag.ContentType)
        End If
    End Sub

#End Region 'Dialog

#Region "Methods"
    ''' <summary>
    ''' Adds a new single Movie row with informations from DB
    ''' </summary>
    ''' <param name="MovieId"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView_AddRow_Movie(ByVal MovieId As Long)
        If MovieId = -1 Then Return

        Dim myDelegate As New Delegate_dtListAddRow(AddressOf DataTable_AddRow)
        Dim newRow As DataRow = Nothing

        Dim nFilter = New SmartFilter.Filter(Enums.ContentType.Movie)
        nFilter.Rules.Add(New SmartFilter.Rule With {
                              .Field = Database.ColumnName.idMovie,
                              .[Operator] = SmartFilter.Operators.Is,
                              .Value = MovieId})

        Dim newTable = Master.DB.GetMovies(nFilter)
        If newTable.Rows.Count = 1 Then
            newRow = newTable.Rows.Item(0)
        End If

        Dim dRow = _DtMovies.NewRow()
        dRow.ItemArray = newRow.ItemArray

        If newRow IsNot Nothing Then
            RemoveHandler dgvMovies.CellEnter, AddressOf DataGridView_CellEnter_Movie
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {_DtMovies, dRow})
            Else
                _DtMovies.Rows.Add(dRow)
            End If
            AddHandler dgvMovies.CellEnter, AddressOf DataGridView_CellEnter_Movie
            currRow_Movie = -1
        End If
    End Sub
    ''' <summary>
    ''' Adds a new single MovieSet row with informations from DB
    ''' </summary>
    ''' <param name="MovieSetID"></param>
    ''' <remarks></remarks>
    Private Function DataGridView_AddRow_Movieset(ByVal MovieSetID As Long) As Integer
        If MovieSetID = -1 Then Return -1

        Dim myDelegate As New Delegate_dtListAddRow(AddressOf DataTable_AddRow)
        Dim newRow As DataRow = Nothing

        Dim nFilter = New SmartFilter.Filter(Enums.ContentType.Movieset)
        nFilter.Rules.Add(New SmartFilter.Rule With {
                              .Field = Database.ColumnName.idSet,
                              .[Operator] = SmartFilter.Operators.Is,
                              .Value = MovieSetID})

        Dim newTable = Master.DB.GetMoviesets(nFilter)
        If newTable.Rows.Count = 1 Then
            newRow = newTable.Rows.Item(0)
        End If

        Dim dRow = _DtMovieSets.NewRow()
        dRow.ItemArray = newRow.ItemArray

        If newRow IsNot Nothing Then
            RemoveHandler dgvMoviesets.CellEnter, AddressOf DataGridView_CellEnter_Movieset
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {_DtMovieSets, dRow})
            Else
                _DtMovieSets.Rows.Add(dRow)
            End If
            AddHandler dgvMoviesets.CellEnter, AddressOf DataGridView_CellEnter_Movieset
            currRow_MovieSet = -1
        End If

        Return _BsMoviesets.Find("idSet", MovieSetID)
    End Function
    ''' <summary>
    ''' Adds a new single TV Show row with informations from DB
    ''' </summary>
    ''' <param name="ShowID"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView_AddRow_TVShow(ByVal ShowID As Long)
        If ShowID = -1 Then Return

        Dim myDelegate As New Delegate_dtListAddRow(AddressOf DataTable_AddRow)
        Dim newRow As DataRow = Nothing

        Dim nFilter = New SmartFilter.Filter(Enums.ContentType.TVShow)
        nFilter.Rules.Add(New SmartFilter.Rule With {
                              .Field = Database.ColumnName.idShow,
                              .[Operator] = SmartFilter.Operators.Is,
                              .Value = ShowID})

        Dim newTable = Master.DB.GetTVShows(nFilter)
        If newTable.Rows.Count = 1 Then
            newRow = newTable.Rows.Item(0)
        End If

        Dim dRow = _DtTVShows.NewRow()
        dRow.ItemArray = newRow.ItemArray

        If newRow IsNot Nothing Then
            RemoveHandler dgvTVShows.CellEnter, AddressOf DataGridView_CellEnter_TVShow
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {_DtTVShows, dRow})
            Else
                _DtTVShows.Rows.Add(dRow)
            End If
            AddHandler dgvTVShows.CellEnter, AddressOf DataGridView_CellEnter_TVShow
            currRow_TVShow = -1
        End If
    End Sub

    Private Sub DataGridView_ApplyFilter_Movie()
        If Visible Then
            ClearInfo()

            currRow_Movie = -1
            prevRow_Movie = -2
            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing

            If Filter_Movie.AnyRuleSpecified Then
                lblFilter_Movies.Text = String.Format("{0} ({1})", Master.eLang.GetString(52, "Filters"), Master.eLang.GetString(1090, "Active"))
                Filter_Movie.Match = If(rbFilterAnd_Movies.Checked, SmartFilter.Conditions.All, SmartFilter.Conditions.Any)
                Filter_Movie.Build()
                _BsMovies.Filter = Filter_Movie.FilterForBindingSource
                AddonsManager.Instance.RuntimeObjects.FilterMovies = _BsMovies.Filter
            Else
                If chkFilterDuplicates_Movies.Checked Then
                    lblFilter_Movies.Text = String.Format("{0} ({1})", Master.eLang.GetString(52, "Filters"), Master.eLang.GetString(1090, "Active"))
                Else
                    lblFilter_Movies.Text = String.Format("{0} ({1})", Master.eLang.GetString(52, "Filters"), Master.eLang.GetString(1091, "Inactive"))
                End If

                _BsMovies.RemoveFilter()
                AddonsManager.Instance.RuntimeObjects.FilterMovies = String.Empty
            End If

            If Filter_Movie.NeedsQuery Then
                DataGridView_Fill_Main(True, False, False)
                AddonsManager.Instance.RuntimeObjects.FilterMoviesSearch = StringUtils.ConvertToValidFilterString(txtSearchMovies.Text)
                AddonsManager.Instance.RuntimeObjects.FilterMoviesType = cbSearchMovies.Text
            Else
                txtSearchMovies.Focus()
            End If
        End If
    End Sub

    Private Sub DataGridView_ApplyFilter_Movieset()
        If Visible Then
            ClearInfo()

            currRow_MovieSet = -1
            prevRow_MovieSet = -2
            dgvMoviesets.ClearSelection()
            dgvMoviesets.CurrentCell = Nothing

            If Filter_Movieset.AnyRuleSpecified Then
                lblFilter_MovieSets.Text = String.Format("{0} ({1})", Master.eLang.GetString(52, "Filters"), Master.eLang.GetString(1090, "Active"))
                Filter_Movieset.Match = If(rbFilterAnd_MovieSets.Checked, SmartFilter.Conditions.All, SmartFilter.Conditions.Any)
                _BsMoviesets.Filter = Filter_Movieset.FilterForBindingSource
                AddonsManager.Instance.RuntimeObjects.FilterMoviesets = _BsMoviesets.Filter
            Else
                lblFilter_MovieSets.Text = String.Format("{0} ({1})", Master.eLang.GetString(52, "Filters"), Master.eLang.GetString(1091, "Inactive"))
                _BsMoviesets.RemoveFilter()
                AddonsManager.Instance.RuntimeObjects.FilterMoviesets = String.Empty
            End If
            txtSearchMovieSets.Focus()
        End If
    End Sub

    Private Sub DataGridView_ApplyFilter_TVShow()
        If Visible Then
            ClearInfo()

            currRow_TVShow = -1
            prevRow_TVShow = -2
            currList = 0
            dgvTVShows.ClearSelection()
            dgvTVShows.CurrentCell = Nothing

            dgvTVSeasons.ClearSelection()
            dgvTVSeasons.CurrentCell = Nothing
            _BsTVSeasons.DataSource = Nothing
            dgvTVEpisodes.DataSource = Nothing

            dgvTVEpisodes.ClearSelection()
            dgvTVEpisodes.CurrentCell = Nothing
            _BsTVEpisodes.DataSource = Nothing
            dgvTVEpisodes.DataSource = Nothing

            If Filter_TVShows.AnyRuleSpecified Then
                lblFilter_Shows.Text = String.Format("{0} ({1})", Master.eLang.GetString(52, "Filters"), Master.eLang.GetString(1090, "Active"))
                Filter_TVShows.Match = If(rbFilterAnd_Shows.Checked, SmartFilter.Conditions.All, SmartFilter.Conditions.Any)
                _BsTVShows.Filter = Filter_TVShows.FilterForBindingSource
                AddonsManager.Instance.RuntimeObjects.FilterTVShows = _BsTVShows.Filter
            Else
                lblFilter_Shows.Text = String.Format("{0} ({1})", Master.eLang.GetString(52, "Filters"), Master.eLang.GetString(1091, "Inactive"))
                _BsTVShows.RemoveFilter()
                AddonsManager.Instance.RuntimeObjects.FilterTVShows = String.Empty
            End If
            txtSearchShows.Focus()
        End If
    End Sub

    Private Sub DataGridView_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles _
        dgvMovies.CellClick,
        dgvMoviesets.CellClick,
        dgvTVEpisodes.CellClick,
        dgvTVSeasons.CellClick,
        dgvTVShows.CellClick

        If e.RowIndex < 0 Then Exit Sub

        Dim bClickScrapeEnabled As Boolean
        Dim bClickScrapeShowResults As Boolean
        Dim eContentType As Enums.ContentType = Enums.ContentType.None
        Dim cDefaultOptions As Structures.ScrapeOptions
        Dim dgView As DataGridView = DirectCast(sender, DataGridView)

        Select Case True
            Case sender Is dgvMovies
                eContentType = Enums.ContentType.Movie
                bClickScrapeEnabled = Manager.mSettings.Movie.GuiSettings.ClickScrapeEnabled
                bClickScrapeShowResults = Manager.mSettings.Movie.GuiSettings.ClickScrapeEnabled
                cDefaultOptions = Master.eSettings.DefaultScrapeOptions(Enums.ContentType.Movie)
            Case sender Is dgvMoviesets
                eContentType = Enums.ContentType.Movieset
                bClickScrapeEnabled = Master.eSettings.MovieSetClickScrape
                bClickScrapeShowResults = Master.eSettings.MovieSetClickScrapeAsk
                cDefaultOptions = Master.eSettings.DefaultScrapeOptions(Enums.ContentType.Movieset)
            Case sender Is dgvTVEpisodes
                eContentType = Enums.ContentType.TVEpisode
                bClickScrapeEnabled = Manager.mSettings.TVEpisode.GuiSettings.ClickScrapeEnabled
                bClickScrapeShowResults = Manager.mSettings.TVEpisode.GuiSettings.ClickScrapeShowResults
                cDefaultOptions = Master.eSettings.DefaultScrapeOptions(Enums.ContentType.TV)
            Case sender Is dgvTVSeasons
                eContentType = Enums.ContentType.TVSeason
                bClickScrapeEnabled = Manager.mSettings.TVSeason.GuiSettings.ClickScrapeEnabled
                bClickScrapeShowResults = Manager.mSettings.TVSeason.GuiSettings.ClickScrapeShowResults
                cDefaultOptions = Master.eSettings.DefaultScrapeOptions(Enums.ContentType.TV)
            Case sender Is dgvTVShows
                eContentType = Enums.ContentType.TVShow
                bClickScrapeEnabled = Manager.mSettings.TVShow.GuiSettings.ClickScrapeEnabled
                bClickScrapeShowResults = Manager.mSettings.TVShow.GuiSettings.ClickScrapeShowResults
                cDefaultOptions = Master.eSettings.DefaultScrapeOptions(Enums.ContentType.TV)
            Case Else
                Return
        End Select

        If Not eContentType = Enums.ContentType.None Then
            Dim colName As String = dgView.Columns(e.ColumnIndex).Name
            If String.IsNullOrEmpty(colName) Then
                Return
            End If

            If Database.Helpers.ColumnIsWatchedState(colName) Then
                If Not (eContentType = Enums.ContentType.TVSeason AndAlso
                    CInt(dgvTVSeasons.Rows(e.RowIndex).Cells(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).Value) = -1) Then
                    TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetWatchedState) With {
                                           .CommonBooleanValue = If(String.IsNullOrEmpty(dgView.Rows(e.RowIndex).Cells(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed)).Value.ToString), True, False),
                                           .ContentType = eContentType,
                                           .SelectionType = Enums.SelectionType.Selected
                                           })
                End If

            ElseIf bClickScrapeEnabled AndAlso colName = Database.Helpers.GetColumnName(Database.ColumnName.HasMovieset) AndAlso Not bwMovieScraper.IsBusy Then
                Dim objCell As DataGridViewCell = dgView.Rows(e.RowIndex).Cells(e.ColumnIndex)

                dgView.ClearSelection()
                dgView.Rows(objCell.RowIndex).Selected = True

                Dim scrapeOptions As New Structures.ScrapeOptions With {
                    .Collection = True
                }
                Dim ScrapeModifiers As New Structures.ScrapeModifiers
                Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainNFO, True)
                Select Case eContentType
                    Case Enums.ContentType.Movie
                        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                                               .ContentType = Enums.ContentType.Movie,
                                               .ScrapeModifiers = ScrapeModifiers,
                                               .ScrapeOptions = scrapeOptions,
                                               .ScrapeType = If(bClickScrapeShowResults, Enums.ScrapeType.Ask, Enums.ScrapeType.Auto),
                                               .SelectionType = Enums.SelectionType.Selected
                                               })
                End Select

            ElseIf bClickScrapeEnabled AndAlso Database.Helpers.ColumnIsScrapeModifier(colName) AndAlso
                Not bwMovieScraper.IsBusy AndAlso
                Not bwMoviesetScraper.IsBusy AndAlso
                Not bwTVEpisodeScraper.IsBusy AndAlso
                Not bwTVSeasonScraper.IsBusy AndAlso
                Not bwTVScraper.IsBusy Then
                Dim objCell As DataGridViewCell = dgView.Rows(e.RowIndex).Cells(e.ColumnIndex)

                dgView.ClearSelection()
                dgView.Rows(objCell.RowIndex).Selected = True
                Select Case eContentType
                    Case Enums.ContentType.Movie
                        currRow_Movie = objCell.RowIndex
                    Case Enums.ContentType.Movieset
                        currRow_MovieSet = objCell.RowIndex
                    Case Enums.ContentType.TVEpisode
                        currRow_TVEpisode = objCell.RowIndex
                    Case Enums.ContentType.TVSeason
                        currRow_TVSeason = objCell.RowIndex
                    Case Enums.ContentType.TVShow
                        currRow_TVShow = objCell.RowIndex
                End Select

                Dim ScrapeModifiers As New Structures.ScrapeModifiers
                Select Case Database.Helpers.ConvertColumnNameToColumnType(colName)
                    Case Database.ColumnType.Banner
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainBanner, True)
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonBanner, True)
                    Case Database.ColumnType.ClearArt
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
                    Case Database.ColumnType.ClearLogo
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
                    Case Database.ColumnType.DiscArt
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainDiscArt, True)
                    Case Database.ColumnType.Extrafanarts
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainExtrafanarts, True)
                    Case Database.ColumnType.Extrathumbs
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainExtrathumbs, True)
                    Case Database.ColumnType.Fanart
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeFanart, True)
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonFanart, True)
                    Case Database.ColumnType.KeyArt
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainKeyArt, True)
                    Case Database.ColumnType.Landscape
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonLandscape, True)
                    Case Database.ColumnType.MetaData
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainMetadata, True)
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeMetadata, True)
                    Case Database.ColumnType.NFO
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainNFO, True)
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeNFO, True)
                    Case Database.ColumnType.Poster
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodePoster, True)
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonPoster, True)
                    Case Database.ColumnType.Subtitle
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainSubtitle, True)
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeSubtitle, True)
                    Case Database.ColumnType.Theme
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainTheme, True)
                    Case Database.ColumnType.Trailer
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainTrailer, True)
                End Select
                TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                                       .ContentType = Enums.ContentType.Movie,
                                       .ScrapeModifiers = ScrapeModifiers,
                                       .ScrapeOptions = cDefaultOptions,
                                       .ScrapeType = If(bClickScrapeShowResults, Enums.ScrapeType.Ask, Enums.ScrapeType.Auto),
                                       .SelectionType = Enums.SelectionType.Selected
                                       })
            End If
        End If
    End Sub

    Private Sub DataGridView_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles _
        dgvMovies.CellDoubleClick,
        dgvMoviesets.CellDoubleClick,
        dgvTVEpisodes.CellDoubleClick,
        dgvTVSeasons.CellDoubleClick,
        dgvTVShows.CellDoubleClick

        If e.RowIndex < 0 Then Exit Sub

        If AnyBackgroundWorkerIsBusy Then Return

        Dim contentType As Enums.ContentType
        Dim dgView As DataGridView = Nothing
        Dim tableName As Database.TableName
        Select Case True
            Case sender Is dgvMovies
                contentType = Enums.ContentType.Movie
                dgView = DirectCast(sender, DataGridView)
                tableName = Database.TableName.movie
            Case sender Is dgvMoviesets
                contentType = Enums.ContentType.Movieset
                dgView = DirectCast(sender, DataGridView)
                tableName = Database.TableName.movieset
            Case sender Is dgvTVEpisodes
                contentType = Enums.ContentType.TVEpisode
                dgView = DirectCast(sender, DataGridView)
                tableName = Database.TableName.episode
            Case sender Is dgvTVSeasons
                contentType = Enums.ContentType.TVSeason
                dgView = DirectCast(sender, DataGridView)
                tableName = Database.TableName.season
            Case sender Is dgvTVShows
                contentType = Enums.ContentType.TVShow
                dgView = DirectCast(sender, DataGridView)
                tableName = Database.TableName.tvshow
        End Select

        If dgView IsNot Nothing Then
            Dim indX As Integer = dgView.SelectedRows(0).Index
            Dim ID As Long = Convert.ToInt64(dgView.Item(Database.Helpers.GetMainIdName(tableName), indX).Value)
            Select Case contentType
                Case Enums.ContentType.Movie
                    Dim dbElement As Database.DBElement = Master.DB.Load_Movie(ID)
                    Edit_Movie(dbElement)
                Case Enums.ContentType.Movieset
                    Dim dbElement As Database.DBElement = Master.DB.Load_Movieset(ID)
                    Edit_Movieset(dbElement)
                Case Enums.ContentType.TVEpisode
                    Dim dbElement As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
                    Edit_TVEpisode(dbElement)
                Case Enums.ContentType.TVSeason
                    Dim dbElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)
                    Edit_TVSeason(dbElement)
                Case Enums.ContentType.TVShow
                    Dim dbElement As Database.DBElement = Master.DB.Load_TVShow(ID, True, False)
                    Edit_TVShow(dbElement)
            End Select
        End If
    End Sub

    Private Sub DataGridView_CellEnter_Movie(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvMovies.CellEnter
        Dim currMainTabTag = MainTab_GetCurrentTag()
        If Not currMainTabTag.ContentType = Enums.ContentType.Movie Then Return

        tmrWait_TVShow.Stop()
        tmrWait_TVSeason.Stop()
        tmrWait_TVEpisode.Stop()
        tmrWait_MovieSet.Stop()
        tmrWait_Movie.Stop()
        tmrLoad_TVShow.Stop()
        tmrLoad_TVSeason.Stop()
        tmrLoad_TVEpisode.Stop()
        tmrLoad_MovieSet.Stop()
        tmrLoad_Movie.Stop()

        currRow_Movie = e.RowIndex
        tmrWait_Movie.Start()
    End Sub

    Private Sub DataGridView_CellEnter_Movieset(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvMoviesets.CellEnter
        Dim currMainTabTag = MainTab_GetCurrentTag()
        If Not currMainTabTag.ContentType = Enums.ContentType.Movieset Then Return

        tmrWait_TVShow.Stop()
        tmrWait_TVSeason.Stop()
        tmrWait_TVEpisode.Stop()
        tmrWait_Movie.Stop()
        tmrWait_MovieSet.Stop()
        tmrLoad_TVShow.Stop()
        tmrLoad_TVSeason.Stop()
        tmrLoad_TVEpisode.Stop()
        tmrLoad_Movie.Stop()
        tmrLoad_MovieSet.Stop()

        currRow_MovieSet = e.RowIndex
        tmrWait_MovieSet.Start()
    End Sub

    Private Sub DataGridView_CellEnter_TVEpisode(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvTVEpisodes.CellEnter
        Dim currMainTabTag = MainTab_GetCurrentTag()
        If Not currMainTabTag.ContentType = Enums.ContentType.TV OrElse Not currList = 2 Then Return

        tmrWait_TVShow.Stop()
        tmrWait_TVSeason.Stop()
        tmrWait_Movie.Stop()
        tmrWait_MovieSet.Stop()
        tmrWait_TVEpisode.Stop()
        tmrLoad_TVShow.Stop()
        tmrLoad_TVSeason.Stop()
        tmrLoad_Movie.Stop()
        tmrLoad_MovieSet.Stop()
        tmrLoad_TVEpisode.Stop()

        currRow_TVEpisode = e.RowIndex
        tmrWait_TVEpisode.Start()
    End Sub

    Private Sub DataGridView_CellEnter_TVSeason(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvTVSeasons.CellEnter
        Dim currMainTabTag = MainTab_GetCurrentTag()
        If Not currMainTabTag.ContentType = Enums.ContentType.TV OrElse Not currList = 1 Then Return

        tmrWait_TVShow.Stop()
        tmrWait_Movie.Stop()
        tmrWait_MovieSet.Stop()
        tmrWait_TVEpisode.Stop()
        tmrWait_TVSeason.Stop()
        tmrLoad_TVShow.Stop()
        tmrLoad_Movie.Stop()
        tmrLoad_MovieSet.Stop()
        tmrLoad_TVEpisode.Stop()
        tmrLoad_TVSeason.Stop()

        currRow_TVSeason = e.RowIndex
        tmrWait_TVSeason.Start()
    End Sub

    Private Sub DataGridView_CellEnter_TVShow(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvTVShows.CellEnter
        Dim currMainTabTag = MainTab_GetCurrentTag()
        If Not currMainTabTag.ContentType = Enums.ContentType.TV OrElse Not currList = 0 Then Return

        tmrWait_Movie.Stop()
        tmrWait_MovieSet.Stop()
        tmrWait_TVSeason.Stop()
        tmrWait_TVEpisode.Stop()
        tmrWait_TVShow.Stop()
        tmrLoad_Movie.Stop()
        tmrLoad_MovieSet.Stop()
        tmrLoad_TVSeason.Stop()
        tmrLoad_TVEpisode.Stop()
        tmrLoad_TVShow.Stop()

        currRow_TVShow = e.RowIndex
        tmrWait_TVShow.Start()
    End Sub

    Private Sub DataGridView_CellMouseEnter(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles _
        dgvMovies.CellMouseEnter,
        dgvMoviesets.CellMouseEnter,
        dgvTVEpisodes.CellMouseEnter,
        dgvTVSeasons.CellMouseEnter,
        dgvTVShows.CellMouseEnter

        Dim bClickScrapeEnabled As Boolean
        Dim bClickScrapeAsk As Boolean
        Dim dgView As DataGridView = DirectCast(sender, DataGridView)

        Select Case True
            Case sender Is dgvMovies
                bClickScrapeAsk = Manager.mSettings.Movie.GuiSettings.ClickScrapeEnabled
                bClickScrapeEnabled = Manager.mSettings.Movie.GuiSettings.ClickScrapeEnabled
            Case sender Is dgvMoviesets
                bClickScrapeAsk = Master.eSettings.MovieSetClickScrapeAsk
                bClickScrapeEnabled = Master.eSettings.MovieSetClickScrape
            Case sender Is dgvTVEpisodes
                bClickScrapeAsk = Manager.mSettings.TVEpisode.GuiSettings.ClickScrapeShowResults
                bClickScrapeEnabled = Manager.mSettings.TVEpisode.GuiSettings.ClickScrapeEnabled
            Case sender Is dgvTVSeasons
                bClickScrapeAsk = Manager.mSettings.TVSeason.GuiSettings.ClickScrapeShowResults
                bClickScrapeEnabled = Manager.mSettings.TVSeason.GuiSettings.ClickScrapeEnabled
            Case sender Is dgvTVShows
                bClickScrapeAsk = Manager.mSettings.TVShow.GuiSettings.ClickScrapeShowResults
                bClickScrapeEnabled = Manager.mSettings.TVShow.GuiSettings.ClickScrapeEnabled
        End Select

        Dim colName As String = dgView.Columns(e.ColumnIndex).Name
        If Not String.IsNullOrEmpty(colName) Then
            dgView.ShowCellToolTips = True

            If Database.Helpers.ColumnIsWatchedState(colName) AndAlso e.RowIndex >= 0 Then
                oldStatus = GetStatus()
                SetStatus(Master.eLang.GetString(885, "Change Watched Status"))
            ElseIf bClickScrapeEnabled AndAlso Database.Helpers.ColumnIsScrapeModifier(colName) AndAlso e.RowIndex >= 0 AndAlso
                Not bwMovieScraper.IsBusy AndAlso
                Not bwMoviesetScraper.IsBusy AndAlso
                Not bwTVEpisodeScraper.IsBusy AndAlso
                Not bwTVSeasonScraper.IsBusy AndAlso
                Not bwTVScraper.IsBusy AndAlso
                Not (sender Is dgvTVSeasons AndAlso
                CInt(dgvTVSeasons.Rows(e.RowIndex).Cells(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).Value) = -1) Then

                dgView.ShowCellToolTips = False
                oldStatus = GetStatus()
                Dim strTitle As String = dgView.Rows(e.RowIndex).Cells(Database.Helpers.GetColumnName(Database.ColumnName.Title)).Value.ToString
                Dim strScrapeFor As String = String.Empty
                Dim strScrapeType As String = String.Empty
                Select Case Database.Helpers.ConvertColumnNameToColumnType(colName)
                    Case Database.ColumnType.Banner
                        strScrapeFor = Master.eLang.GetString(1060, "Banner Only")
                    Case Database.ColumnType.CharacterArt
                        strScrapeFor = Master.eLang.GetString(1121, "CharacterArt Only")
                    Case Database.ColumnType.ClearArt
                        strScrapeFor = Master.eLang.GetString(1122, "ClearArt Only")
                    Case Database.ColumnType.ClearLogo
                        strScrapeFor = Master.eLang.GetString(1123, "ClearLogo Only")
                    Case Database.ColumnType.DiscArt
                        strScrapeFor = Master.eLang.GetString(1124, "DiscArt Only")
                    Case Database.ColumnType.Extrafanarts
                        strScrapeFor = Master.eLang.GetString(975, "Extrafanarts Only")
                    Case Database.ColumnType.Extrathumbs
                        strScrapeFor = Master.eLang.GetString(74, "Extrathumbs Only")
                    Case Database.ColumnType.Fanart
                        strScrapeFor = Master.eLang.GetString(73, "Fanart Only")
                    Case Database.ColumnType.KeyArt
                        strScrapeFor = Master.eLang.GetString(303, "KeyArt Only")
                    Case Database.ColumnType.Landscape
                        strScrapeFor = Master.eLang.GetString(1061, "Landscape Only")
                    Case Database.ColumnType.NFO
                        strScrapeFor = Master.eLang.GetString(71, "NFO Only")
                    Case Database.ColumnType.MetaData
                        strScrapeFor = Master.eLang.GetString(76, "Metadata Only")
                    Case Database.ColumnType.Movieset
                        strScrapeFor = Master.eLang.GetString(1354, "MovieSet Informations Only")
                    Case Database.ColumnType.Poster
                        strScrapeFor = Master.eLang.GetString(72, "Poster Only")
                    Case Database.ColumnType.Subtitle
                        strScrapeFor = Master.eLang.GetString(1355, "Subtitles Only")
                    Case Database.ColumnType.Theme
                        strScrapeFor = Master.eLang.GetString(1125, "Theme Only")
                    Case Database.ColumnType.Trailer
                        strScrapeFor = Master.eLang.GetString(75, "Trailer Only")
                End Select
                If bClickScrapeAsk Then
                    strScrapeType = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
                Else
                    strScrapeType = Master.eLang.GetString(69, "Automatic (Force Best Match)")
                End If
                SetStatus(String.Format("Scrape ""{0}"" for {1} - {2}", strTitle, strScrapeFor, strScrapeType))
            Else
                oldStatus = String.Empty
            End If
        Else
            oldStatus = String.Empty
        End If
    End Sub

    Private Sub DataGridView_CellMouseLeave(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles _
        dgvMovies.CellMouseLeave,
        dgvMoviesets.CellMouseLeave,
        dgvTVEpisodes.CellMouseLeave,
        dgvTVSeasons.CellMouseLeave,
        dgvTVShows.CellMouseLeave

        If Not String.IsNullOrEmpty(oldStatus) Then SetStatus(oldStatus)
    End Sub

    Private Sub DataGridView_CellPainting(ByVal sender As Object, ByVal e As DataGridViewCellPaintingEventArgs) Handles _
        dgvMovies.CellPainting,
        dgvMoviesets.CellPainting,
        dgvTVEpisodes.CellPainting,
        dgvTVSeasons.CellPainting,
        dgvTVShows.CellPainting

        Dim dgView As DataGridView = DirectCast(sender, DataGridView)
        If dgView IsNot Nothing Then
            Dim colName As String = dgView.Columns(e.ColumnIndex).Name
            If Not String.IsNullOrEmpty(colName) Then
                If e.RowIndex >= 0 AndAlso Not dgView.Item(e.ColumnIndex, e.RowIndex).Displayed Then
                    e.Handled = True
                    Return
                End If

                'icons for column header
                If Database.Helpers.ColumnHeaderIsIcon(colName) AndAlso e.RowIndex = -1 Then
                    e.PaintBackground(e.ClipBounds, False)

                    Dim pt As Point = e.CellBounds.Location
                    Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)
                    pt.X += offset
                    pt.Y = 3

                    Select Case Database.Helpers.ConvertColumnNameToColumnType(colName)
                        Case Database.ColumnType.Banner
                            ilColumnIcons.Draw(e.Graphics, pt, 2)
                        Case Database.ColumnType.CharacterArt
                            ilColumnIcons.Draw(e.Graphics, pt, 3)
                        Case Database.ColumnType.ClearArt
                            ilColumnIcons.Draw(e.Graphics, pt, 4)
                        Case Database.ColumnType.ClearLogo
                            ilColumnIcons.Draw(e.Graphics, pt, 5)
                        Case Database.ColumnType.DiscArt
                            ilColumnIcons.Draw(e.Graphics, pt, 6)
                        Case Database.ColumnType.Extrafanarts
                            ilColumnIcons.Draw(e.Graphics, pt, 7)
                        Case Database.ColumnType.Extrathumbs
                            ilColumnIcons.Draw(e.Graphics, pt, 8)
                        Case Database.ColumnType.Fanart
                            ilColumnIcons.Draw(e.Graphics, pt, 9)
                        Case Database.ColumnType.KeyArt
                            ilColumnIcons.Draw(e.Graphics, pt, 12)
                        Case Database.ColumnType.Landscape
                            ilColumnIcons.Draw(e.Graphics, pt, 10)
                        Case Database.ColumnType.Movieset
                            ilColumnIcons.Draw(e.Graphics, pt, 13)
                        Case Database.ColumnType.NFO
                            ilColumnIcons.Draw(e.Graphics, pt, 11)
                        Case Database.ColumnType.Poster
                            ilColumnIcons.Draw(e.Graphics, pt, 12)
                        Case Database.ColumnType.Rating
                            ilColumnIcons.Draw(e.Graphics, pt, 18)
                        Case Database.ColumnType.Subtitle
                            ilColumnIcons.Draw(e.Graphics, pt, 14)
                        Case Database.ColumnType.Theme
                            ilColumnIcons.Draw(e.Graphics, pt, 15)
                        Case Database.ColumnType.Trailer
                            ilColumnIcons.Draw(e.Graphics, pt, 16)
                        Case Database.ColumnType.UserRating
                            ilColumnIcons.Draw(e.Graphics, pt, 19)
                        Case Database.ColumnType.WatchedState
                            ilColumnIcons.Draw(e.Graphics, pt, 17)
                    End Select
                    e.Handled = True
                End If

                'text fields
                If Database.Helpers.ColumnIsText(colName) AndAlso e.RowIndex >= 0 Then
                    If DataGridView_ColumnExists(dgView, Database.Helpers.GetColumnName(Database.ColumnName.IsMissing)) AndAlso
                        Convert.ToBoolean(dgView.Item(Database.Helpers.GetColumnName(Database.ColumnName.IsMissing), e.RowIndex).Value) Then
                        e.CellStyle.ForeColor = MediaListColors.Missing.ForeColor
                        e.CellStyle.SelectionForeColor = MediaListColors.Missing.SelectionForeColor
                    ElseIf DataGridView_ColumnExists(dgView, Database.Helpers.GetColumnName(Database.ColumnName.Marked)) AndAlso
                        Convert.ToBoolean(dgView.Item(Database.Helpers.GetColumnName(Database.ColumnName.Marked), e.RowIndex).Value) Then
                        e.CellStyle.ForeColor = MediaListColors.Marked.ForeColor
                        e.CellStyle.SelectionForeColor = MediaListColors.Marked.SelectionForeColor
                    ElseIf DataGridView_ColumnExists(dgView, Database.Helpers.GetColumnName(Database.ColumnName.[New])) AndAlso
                        Convert.ToBoolean(dgView.Item(Database.Helpers.GetColumnName(Database.ColumnName.[New]), e.RowIndex).Value) Then
                        e.CellStyle.ForeColor = MediaListColors.New.ForeColor
                        e.CellStyle.SelectionForeColor = MediaListColors.New.SelectionForeColor
                    ElseIf DataGridView_ColumnExists(dgView, Database.Helpers.GetColumnName(Database.ColumnName.NewEpisodesCount)) AndAlso
                        Convert.ToBoolean(dgView.Item(Database.Helpers.GetColumnName(Database.ColumnName.NewEpisodesCount), e.RowIndex).Value) Then
                        e.CellStyle.ForeColor = MediaListColors.New.ForeColor
                        e.CellStyle.SelectionForeColor = MediaListColors.New.SelectionForeColor
                    ElseIf DataGridView_ColumnExists(dgView, Database.Helpers.GetColumnName(Database.ColumnName.MarkedCustom1)) AndAlso
                        Convert.ToBoolean(dgView.Item(Database.Helpers.GetColumnName(Database.ColumnName.MarkedCustom1), e.RowIndex).Value) Then
                        e.CellStyle.ForeColor = Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker1Color)
                        e.CellStyle.SelectionForeColor = Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker1Color)
                    ElseIf DataGridView_ColumnExists(dgView, Database.Helpers.GetColumnName(Database.ColumnName.MarkedCustom2)) AndAlso
                        Convert.ToBoolean(dgView.Item(Database.Helpers.GetColumnName(Database.ColumnName.MarkedCustom2), e.RowIndex).Value) Then
                        e.CellStyle.ForeColor = Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker2Color)
                        e.CellStyle.SelectionForeColor = Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker2Color)
                    ElseIf DataGridView_ColumnExists(dgView, Database.Helpers.GetColumnName(Database.ColumnName.MarkedCustom3)) AndAlso
                        Convert.ToBoolean(dgView.Item(Database.Helpers.GetColumnName(Database.ColumnName.MarkedCustom3), e.RowIndex).Value) Then
                        e.CellStyle.ForeColor = Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker3Color)
                        e.CellStyle.SelectionForeColor = Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker3Color)
                    ElseIf DataGridView_ColumnExists(dgView, Database.Helpers.GetColumnName(Database.ColumnName.MarkedCustom4)) AndAlso
                        Convert.ToBoolean(dgView.Item(Database.Helpers.GetColumnName(Database.ColumnName.MarkedCustom4), e.RowIndex).Value) Then
                        e.CellStyle.ForeColor = Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker4Color)
                        e.CellStyle.SelectionForeColor = Color.FromArgb(Master.eSettings.MovieGeneralCustomMarker4Color)
                    Else
                        e.CellStyle.ForeColor = MediaListColors.Default.ForeColor
                        e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
                        e.CellStyle.SelectionForeColor = MediaListColors.Default.SelectionForeColor
                    End If
                End If

                If e.ColumnIndex >= 1 AndAlso e.RowIndex >= 0 Then
                    'background
                    If DataGridView_ColumnExists(dgView, Database.Helpers.GetColumnName(Database.ColumnName.IsMissing)) AndAlso
                        Convert.ToBoolean(dgView.Item(Database.Helpers.GetColumnName(Database.ColumnName.IsMissing), e.RowIndex).Value) Then
                        e.CellStyle.BackColor = MediaListColors.Missing.BackColor
                        e.CellStyle.SelectionBackColor = MediaListColors.Missing.SelectionBackColor
                    ElseIf DataGridView_ColumnExists(dgView, Database.Helpers.GetColumnName(Database.ColumnName.Locked)) AndAlso
                        Convert.ToBoolean(dgView.Item(Database.Helpers.GetColumnName(Database.ColumnName.Locked), e.RowIndex).Value) Then
                        e.CellStyle.BackColor = MediaListColors.Locked.BackColor
                        e.CellStyle.SelectionBackColor = MediaListColors.Locked.SelectionBackColor
                    ElseIf DataGridView_ColumnExists(dgView, Database.Helpers.GetColumnName(Database.ColumnName.OutOfTolerance)) AndAlso
                        Convert.ToBoolean(dgView.Item(Database.Helpers.GetColumnName(Database.ColumnName.OutOfTolerance), e.RowIndex).Value) Then
                        e.CellStyle.BackColor = MediaListColors.OutOfTolerance.BackColor
                        e.CellStyle.SelectionBackColor = MediaListColors.OutOfTolerance.SelectionBackColor
                    Else
                        e.CellStyle.BackColor = MediaListColors.Default.BackColor
                        e.CellStyle.SelectionBackColor = MediaListColors.Default.SelectionBackColor
                    End If

                    'checkbox icons
                    If Database.Helpers.ColumnIsCheckbox(colName) Then
                        'AndAlso (Not dgView Is dgvTVSeasons OrElse Not (colName = Database.Helpers.GetColumnName(Database.ColumnName.HasWatched) AndAlso
                        'CInt(dgView.Item(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber), e.RowIndex).Value) = -1)) Then
                        e.PaintBackground(e.ClipBounds, True)

                        Dim pt As Point = e.CellBounds.Location
                        Dim offset As Integer = Convert.ToInt32((e.CellBounds.Width - ilColumnIcons.ImageSize.Width) / 2)

                        pt.X += offset
                        pt.Y = e.CellBounds.Top + 3
                        If Database.Helpers.ColumnIsBoolean(colName) Then
                            ilColumnIcons.Draw(e.Graphics, pt, If(Convert.ToBoolean(e.Value), 0, 1))
                        Else
                            ilColumnIcons.Draw(e.Graphics, pt, If(Not String.IsNullOrEmpty(e.Value.ToString), 0, 1))
                        End If
                        e.Handled = True
                    End If
                End If

                'set the *All Seasons season number to "invisible"
                If e.RowIndex >= 0 AndAlso dgView Is dgvTVSeasons AndAlso colName = Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber) AndAlso
                    CInt(dgView.Item(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber), e.RowIndex).Value) = -1 Then
                    e.CellStyle.ForeColor = e.CellStyle.BackColor
                    e.CellStyle.SelectionForeColor = e.CellStyle.SelectionBackColor
                End If
            End If
        Else
            Return
        End If
    End Sub

    Private Function DataGridView_ColumnExists(ByVal dgView As DataGridView, ByVal columnName As String) As Boolean
        If dgView IsNot Nothing AndAlso Not String.IsNullOrEmpty(columnName) Then
            Return dgView.Columns.Contains(columnName)
        End If
        Return False
    End Function

    Private Function DataGridView_ColumnHasValue(ByVal dgView As DataGridView, ByVal columnName As String, ByVal row As Integer) As Boolean
        If dgView IsNot Nothing AndAlso Not String.IsNullOrEmpty(columnName) AndAlso row >= 0 Then
            Return DataGridView_ColumnExists(dgView, columnName) AndAlso Not String.IsNullOrEmpty(dgView.Item(columnName, row).Value.ToString)
        End If
        Return False
    End Function

    Private Function DataGridView_ColumnAnyInfoValue(ByVal dgView As DataGridView, ByVal row As Integer) As Boolean
        If dgView IsNot Nothing AndAlso row >= 0 Then
            Return _
                DataGridView_ColumnHasValue(dgView, Database.Helpers.GetColumnName(Database.ColumnName.BannerPath), row) OrElse
                DataGridView_ColumnHasValue(dgView, Database.Helpers.GetColumnName(Database.ColumnName.CharacterArtPath), row) OrElse
                DataGridView_ColumnHasValue(dgView, Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath), row) OrElse
                DataGridView_ColumnHasValue(dgView, Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath), row) OrElse
                DataGridView_ColumnHasValue(dgView, Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath), row) OrElse
                DataGridView_ColumnHasValue(dgView, Database.Helpers.GetColumnName(Database.ColumnName.FanartPath), row) OrElse
                DataGridView_ColumnHasValue(dgView, Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath), row) OrElse
                DataGridView_ColumnHasValue(dgView, Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath), row) OrElse
                DataGridView_ColumnHasValue(dgView, Database.Helpers.GetColumnName(Database.ColumnName.NfoPath), row) OrElse
                DataGridView_ColumnHasValue(dgView, Database.Helpers.GetColumnName(Database.ColumnName.PosterPath), row)
        End If
        Return False
    End Function
    ''' <summary>
    ''' Reloads the DB and refresh the lists
    ''' </summary>
    ''' <param name="DoMovies">reload movies</param>
    ''' <param name="DoMoviesets">reload moviesets</param>
    ''' <param name="DoTVShows">reload tv shows</param>
    ''' <remarks></remarks>
    Private Sub DataGridView_Fill_Main(ByVal DoMovies As Boolean, ByVal DoMoviesets As Boolean, ByVal DoTVShows As Boolean)
        If DoMovies Then
            _BsMovies.DataSource = Nothing
            dgvMovies.DataSource = Nothing
            _BsMovies.Filter = Filter_Movie.FilterForBindingSource
            _DtMovies = Master.DB.GetMovies(Filter_Movie)
        End If

        If DoMoviesets Then
            _BsMoviesets.DataSource = Nothing
            dgvMoviesets.DataSource = Nothing
            _BsMoviesets.Filter = Filter_Movieset.FilterForBindingSource
            _DtMovieSets = Master.DB.GetMoviesets(Filter_Movieset)
        End If

        If DoTVShows Then
            _BsTVShows.DataSource = Nothing
            dgvTVShows.DataSource = Nothing
            _BsTVSeasons.DataSource = Nothing
            dgvTVSeasons.DataSource = Nothing
            _BsTVEpisodes.DataSource = Nothing
            dgvTVEpisodes.DataSource = Nothing
            _BsTVShows.Filter = Filter_TVShows.FilterForBindingSource
            _DtTVShows = Master.DB.GetTVShows(Filter_TVShows)
        End If


        If Master.isCL Then
            LoadingDone = True
        Else
            If DoMovies Then
                prevRow_Movie = -2
                _BsMovies.DataSource = _DtMovies
                dgvMovies.DataSource = _BsMovies

                Try
                    If Manager.mSettings.Movie.GuiSettings.MediaListSorting.Count > 0 Then
                        For Each mColumn In Manager.mSettings.Movie.GuiSettings.MediaListSorting
                            dgvMovies.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                        Next
                    End If
                Catch ex As Exception
                    _Logger.Warn("default list for movie list sorting has been loaded")
                    Manager.mSettings.SetDefaultsForLists(Enums.DefaultType.MovieListSorting, True)
                    If Manager.mSettings.Movie.GuiSettings.MediaListSorting.Count > 0 Then
                        For Each mColumn In Manager.mSettings.Movie.GuiSettings.MediaListSorting
                            dgvMovies.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                        Next
                    End If
                End Try

                For i As Integer = 0 To dgvMovies.Columns.Count - 1
                    dgvMovies.Columns(i).Visible = False
                Next

                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).ToolTipText = Master.eLang.GetString(838, "Banner")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath)).ToolTipText = Master.eLang.GetString(1096, "ClearArt")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).ToolTipText = Master.eLang.GetString(1097, "ClearLogo")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath)).ToolTipText = Master.eLang.GetString(1098, "DiscArt")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath)).ToolTipText = Master.eLang.GetString(992, "Extrafanarts")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrathumbsPath)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrathumbsPath)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrathumbsPath)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrathumbsPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrathumbsPath)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.ExtrathumbsPath))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrathumbsPath)).ToolTipText = Master.eLang.GetString(153, "Extrathumbs")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ToolTipText = Master.eLang.GetString(149, "Fanart")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasMovieset)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasMovieset)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasMovieset)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasMovieset)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasMovieset)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.HasMovieset))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasMovieset)).ToolTipText = Master.eLang.GetString(1295, "Part of a MovieSet")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles)).ToolTipText = Master.eLang.GetString(152, "Subtitles")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed)).ToolTipText = Master.eLang.GetString(981, "Watched")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).ToolTipText = Master.eLang.GetString(296, "KeyArt")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).ToolTipText = Master.eLang.GetString(1035, "Landscape")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).MinimumWidth = 83
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).Visible = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ToolTipText = Master.eLang.GetString(21, "Title")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).HeaderText = Master.eLang.GetString(21, "Title")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).MinimumWidth = 45
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.MPAA))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).ToolTipText = Master.eLang.GetString(401, "MPAA")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).HeaderText = Master.eLang.GetString(401, "MPAA")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).ToolTipText = Master.eLang.GetString(150, "Nfo")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).ToolTipText = Master.eLang.GetString(302, "Original Title")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).HeaderText = Master.eLang.GetString(302, "Original Title")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ToolTipText = Master.eLang.GetString(148, "Poster")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath)).ToolTipText = Master.eLang.GetString(1118, "Theme")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Top250)).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Top250)).MinimumWidth = 35
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Top250)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Top250)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Top250)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Top250)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.Top250))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Top250)).ToolTipText = "Top 250"
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Top250)).HeaderText = "250"
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.TrailerPath)).Width = 20
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.TrailerPath)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.TrailerPath)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.TrailerPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.TrailerPath)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.TrailerPath))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.TrailerPath)).ToolTipText = Master.eLang.GetString(151, "Trailer")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).MinimumWidth = 30
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.UserRating))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).ToolTipText = Master.eLang.GetString(1467, "User Rating")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Year)).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Year)).Resizable = DataGridViewTriState.False
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Year)).ReadOnly = True
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Year)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Year)).Visible = DataGridView_ShowColumn_Movie(Database.Helpers.GetColumnName(Database.ColumnName.Year))
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Year)).ToolTipText = Master.eLang.GetString(278, "Year")
                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Year)).HeaderText = Master.eLang.GetString(278, "Year")

                dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If

            If DoMoviesets Then
                prevRow_MovieSet = -2
                dgvMoviesets.Enabled = False
                _BsMoviesets.DataSource = _DtMovieSets
                dgvMoviesets.DataSource = _BsMoviesets

                Try
                    If Manager.mSettings.Movieset.GuiSettings.MediaListSorting.Count > 0 Then
                        For Each mColumn In Manager.mSettings.Movieset.GuiSettings.MediaListSorting
                            dgvMoviesets.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                        Next
                    End If
                Catch ex As Exception
                    _Logger.Warn("Default list For movieset list sorting has been loaded")
                    Manager.mSettings.SetDefaultsForLists(Enums.DefaultType.MoviesetListSorting, True)
                    If Manager.mSettings.Movieset.GuiSettings.MediaListSorting.Count > 0 Then
                        For Each mColumn In Manager.mSettings.Movieset.GuiSettings.MediaListSorting
                            dgvMoviesets.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                        Next
                    End If
                End Try

                For i As Integer = 0 To dgvMoviesets.Columns.Count - 1
                    dgvMoviesets.Columns(i).Visible = False
                Next

                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).Width = 20
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).Resizable = DataGridViewTriState.False
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).ReadOnly = True
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).Visible = DataGridView_ShowColumn_Movieset(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath))
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).ToolTipText = Master.eLang.GetString(838, "Banner")
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Width = 20
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Resizable = DataGridViewTriState.False
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).ReadOnly = True
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Visible = DataGridView_ShowColumn_Movieset(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath))
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).ToolTipText = Master.eLang.GetString(1096, "ClearArt")
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Width = 20
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Resizable = DataGridViewTriState.False
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).ReadOnly = True
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Visible = DataGridView_ShowColumn_Movieset(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath))
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).ToolTipText = Master.eLang.GetString(1097, "ClearLogo")
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath)).Width = 20
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath)).Resizable = DataGridViewTriState.False
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath)).ReadOnly = True
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath)).Visible = DataGridView_ShowColumn_Movieset(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath))
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath)).ToolTipText = Master.eLang.GetString(1098, "DiscArt")
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Width = 20
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Resizable = DataGridViewTriState.False
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ReadOnly = True
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Visible = DataGridView_ShowColumn_Movieset(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath))
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ToolTipText = Master.eLang.GetString(149, "Fanart")
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).Width = 20
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).Resizable = DataGridViewTriState.False
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).ReadOnly = True
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).Visible = DataGridView_ShowColumn_Movieset(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath))
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).ToolTipText = Master.eLang.GetString(296, "KeyArt")
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).Width = 20
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).Resizable = DataGridViewTriState.False
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).ReadOnly = True
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).Visible = DataGridView_ShowColumn_Movieset(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath))
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).ToolTipText = Master.eLang.GetString(1035, "Landscape")
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).Resizable = DataGridViewTriState.False
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ReadOnly = True
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).MinimumWidth = 83
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).Visible = True
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ToolTipText = Master.eLang.GetString(21, "Title")
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).HeaderText = Master.eLang.GetString(21, "Title")
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).Width = 20
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).Resizable = DataGridViewTriState.False
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).ReadOnly = True
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).Visible = DataGridView_ShowColumn_Movieset(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath))
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).ToolTipText = Master.eLang.GetString(150, "Nfo")
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Width = 20
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Resizable = DataGridViewTriState.False
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ReadOnly = True
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Visible = DataGridView_ShowColumn_Movieset(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath))
                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ToolTipText = Master.eLang.GetString(148, "Poster")

                dgvMoviesets.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                dgvMoviesets.Enabled = True
            End If

            If DoTVShows Then
                currList = 0
                prevRow_TVEpisode = -2
                prevRow_TVSeason = -2
                prevRow_TVShow = -2
                dgvTVShows.Enabled = False
                _BsTVShows.DataSource = _DtTVShows
                dgvTVShows.DataSource = _BsTVShows

                Try
                    If Manager.mSettings.TVShow.GuiSettings.MediaListSorting.Count > 0 Then
                        For Each mColumn In Manager.mSettings.TVShow.GuiSettings.MediaListSorting
                            dgvTVShows.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                        Next
                    End If
                Catch ex As Exception
                    _Logger.Warn("Default list For tv show list sorting has been loaded")
                    Manager.mSettings.SetDefaultsForLists(Enums.DefaultType.TVShowListSorting, True)
                    If Manager.mSettings.TVShow.GuiSettings.MediaListSorting.Count > 0 Then
                        For Each mColumn In Manager.mSettings.TVShow.GuiSettings.MediaListSorting
                            dgvTVShows.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                        Next
                    End If
                End Try

                For i As Integer = 0 To dgvTVShows.Columns.Count - 1
                    dgvTVShows.Columns(i).Visible = False
                Next

                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).Width = 20
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).ToolTipText = Master.eLang.GetString(838, "Banner")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.CharacterArtPath)).Width = 20
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.CharacterArtPath)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.CharacterArtPath)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.CharacterArtPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.CharacterArtPath)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.CharacterArtPath))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.CharacterArtPath)).ToolTipText = Master.eLang.GetString(1140, "CharacterArt")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Width = 20
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).ToolTipText = Master.eLang.GetString(1096, "ClearArt")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Width = 20
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).ToolTipText = Master.eLang.GetString(1097, "ClearLogo")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath)).Width = 20
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath)).ToolTipText = Master.eLang.GetString(992, "Extrafanarts")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).MinimumWidth = 30
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).ToolTipText = Master.eLang.GetString(682, "Episodes")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).HeaderText = String.Empty
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Width = 20
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ToolTipText = Master.eLang.GetString(149, "Fanart")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasWatched)).Width = 20
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasWatched)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasWatched)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasWatched)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasWatched)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.HasWatched))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasWatched)).ToolTipText = Master.eLang.GetString(981, "Watched")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).Width = 20
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).ToolTipText = Master.eLang.GetString(296, "KeyArt")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).Width = 20
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).ToolTipText = Master.eLang.GetString(1035, "Landscape")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).MinimumWidth = 83
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).Visible = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ToolTipText = Master.eLang.GetString(21, "Title")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).HeaderText = Master.eLang.GetString(21, "Title")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).MinimumWidth = 45
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.MPAA))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).ToolTipText = Master.eLang.GetString(401, "MPAA")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.MPAA)).HeaderText = Master.eLang.GetString(401, "MPAA")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).Width = 20
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).ToolTipText = Master.eLang.GetString(150, "Nfo")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Width = 20
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ToolTipText = Master.eLang.GetString(148, "Poster")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Status)).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Status)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Status)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Status)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Status)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.Status))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Status)).ToolTipText = Master.eLang.GetString(215, "Status")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Status)).HeaderText = Master.eLang.GetString(215, "Status")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).ToolTipText = Master.eLang.GetString(302, "Original Title")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).HeaderText = Master.eLang.GetString(302, "Original Title")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath)).Width = 20
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath)).ToolTipText = Master.eLang.GetString(1118, "Theme")
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).MinimumWidth = 30
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).Resizable = DataGridViewTriState.False
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).ReadOnly = True
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).SortMode = DataGridViewColumnSortMode.Automatic
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).Visible = DataGridView_ShowColumn_TVShow(Database.Helpers.GetColumnName(Database.ColumnName.UserRating))
                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).ToolTipText = Master.eLang.GetString(1467, "User Rating")

                dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                dgvTVShows.Enabled = True
            End If

            If dgvMovies.RowCount > 0 OrElse dgvMoviesets.RowCount > 0 OrElse dgvTVShows.RowCount > 0 Then
                SetControlsEnabled(True)
            Else
                SetControlsEnabled(False, False, False)
                SetStatus(String.Empty)
                ClearInfo()
            End If
        End If

        If Not Master.isCL Then
            mnuUpdate.Enabled = True
            cmnuTrayExit.Enabled = True
            cmnuTraySettings.Enabled = True
            mnuMainEdit.Enabled = True
            cmnuTrayUpdate.Enabled = True
            mnuMainHelp.Enabled = True
            tslLoading.Visible = False
            tspbLoading.Visible = False
            tspbLoading.Value = 0
            tcMain.Enabled = True
            DoTitleCheck()
            FilterPanel_IsEnabled_Movie(True)
            FilterPanel_IsEnabled_Movieset(True)
            FilterPanel_IsEnabled_TVShow(True)
            If DoMovies Then
                DataGridView_SortingRestore_Movie()
            End If
            If DoMoviesets Then
                DataGridView_SortingRestore_Movieset()
            End If
            If DoTVShows Then
                DataGridView_SortingRestore_TVShow()
            End If
            If DoMovies AndAlso DoMoviesets AndAlso DoTVShows Then
                MainTab_UpdateCounts()
            End If
        End If
    End Sub

    Private Sub DataGridView_Fill_TVEpisode(ByVal ShowID As Long, ByVal Season As Integer)
        RemoveHandler dgvTVEpisodes.SelectionChanged, AddressOf DataGridView_SelectionChanged_TVEpisodes
        Dim sEpisodeSorting As Enums.EpisodeSorting = Master.DB.GetTVShowEpisodeSorting(ShowID)
        Dim bIsAllSeasons As Boolean = Season = -1

        _BsTVEpisodes.DataSource = Nothing
        dgvTVEpisodes.DataSource = Nothing

        dgvTVEpisodes.Enabled = False

        Filter_TVEpisodes = New SmartFilter.Filter(Enums.ContentType.TVEpisode)
        If bIsAllSeasons Then
            Filter_TVEpisodes.Rules.Add(New SmartFilter.Rule With {
                                          .Field = Database.ColumnName.idShow,
                                          .[Operator] = SmartFilter.Operators.Is,
                                          .Value = ShowID
                                          })
            Filter_TVEpisodes.OrderBy.Add(New SmartFilter.Order With {
                                            .SortedBy = Database.ColumnName.SeasonNumber
                                            })
        Else
            Filter_TVEpisodes.Rules.Add(New SmartFilter.Rule With {
                                          .Field = Database.ColumnName.idShow,
                                          .[Operator] = SmartFilter.Operators.Is,
                                          .Value = ShowID
                                          })
            Filter_TVEpisodes.Rules.Add(New SmartFilter.Rule With {
                                          .Field = Database.ColumnName.SeasonNumber,
                                          .[Operator] = SmartFilter.Operators.Is,
                                          .Value = Season
                                          })
        End If
        If Not Manager.mSettings.TVEpisode.GuiSettings.DisplayMissingElements Then
            Filter_TVEpisodes.Rules.Add(New SmartFilter.Rule With {
                                              .Field = Database.ColumnName.IsMissing,
                                              .[Operator] = SmartFilter.Operators.False
                                              })
        End If
        Filter_TVEpisodes.OrderBy.Add(New SmartFilter.Order With {
                                            .SortedBy = Database.ColumnName.EpisodeNumber
                                            })
        Filter_TVEpisodes.Build()
        _DtTVEpisodes = Master.DB.GetTVEpisodes(Filter_TVEpisodes)

        _BsTVEpisodes.DataSource = _DtTVEpisodes
        _BsTVEpisodes.Filter = Filter_TVEpisodes.FilterForBindingSource
        dgvTVEpisodes.DataSource = _BsTVEpisodes

        Try
            If Manager.mSettings.TVEpisode.GuiSettings.MediaListSorting.Count > 0 Then
                For Each mColumn In Manager.mSettings.TVEpisode.GuiSettings.MediaListSorting
                    dgvTVEpisodes.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                Next
            End If
        Catch ex As Exception
            _Logger.Warn("Default list For episode list sorting has been loaded")
            Manager.mSettings.SetDefaultsForLists(Enums.DefaultType.TVEpisodeListSorting, True)
            If Manager.mSettings.TVEpisode.GuiSettings.MediaListSorting.Count > 0 Then
                For Each mColumn In Manager.mSettings.TVEpisode.GuiSettings.MediaListSorting
                    dgvTVEpisodes.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                Next
            End If
        End Try

        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).DisplayIndex = 0
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeNumber)).DisplayIndex = 1
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Aired)).DisplayIndex = 2

        For i As Integer = 0 To dgvTVEpisodes.Columns.Count - 1
            dgvTVEpisodes.Columns(i).Visible = False
        Next

        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Aired)).Resizable = DataGridViewTriState.False
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Aired)).Width = 80
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Aired)).ReadOnly = True
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Aired)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Aired)).Visible = sEpisodeSorting = Enums.EpisodeSorting.Aired
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Aired)).ToolTipText = Master.eLang.GetString(728, "Aired")
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Aired)).HeaderText = Master.eLang.GetString(728, "Aired")
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeNumber)).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeNumber)).Resizable = DataGridViewTriState.False
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeNumber)).ReadOnly = True
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeNumber)).MinimumWidth = If(bIsAllSeasons, 41, 82)
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeNumber)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeNumber)).Visible = Not sEpisodeSorting = Enums.EpisodeSorting.Aired
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeNumber)).ToolTipText = Master.eLang.GetString(755, "Episode #")
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeNumber)).HeaderText = "#"
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeNumber)).DefaultCellStyle.Format = "00"
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Width = 20
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Resizable = DataGridViewTriState.False
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ReadOnly = True
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Visible = DataGridView_ShowColumn__TVEpisode(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath))
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ToolTipText = Master.eLang.GetString(149, "Fanart")
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles)).Width = 20
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles)).Resizable = DataGridViewTriState.False
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles)).ReadOnly = True
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles)).Visible = DataGridView_ShowColumn__TVEpisode(Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles))
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles)).ToolTipText = Master.eLang.GetString(152, "Subtitles")
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed)).Width = 20
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed)).Resizable = DataGridViewTriState.False
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed)).ReadOnly = True
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed)).Visible = DataGridView_ShowColumn__TVEpisode(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed))
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed)).ToolTipText = Master.eLang.GetString(981, "Watched")
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).Width = 20
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).Resizable = DataGridViewTriState.False
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).ReadOnly = True
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).Visible = DataGridView_ShowColumn__TVEpisode(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath))
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).ToolTipText = Master.eLang.GetString(150, "Nfo")
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).Resizable = DataGridViewTriState.False
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).ReadOnly = True
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).Visible = DataGridView_ShowColumn__TVEpisode(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle))
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).ToolTipText = Master.eLang.GetString(302, "Original Title")
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle)).HeaderText = Master.eLang.GetString(302, "Original Title")
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Width = 20
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Resizable = DataGridViewTriState.False
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ReadOnly = True
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Visible = DataGridView_ShowColumn__TVEpisode(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath))
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ToolTipText = Master.eLang.GetString(148, "Poster")
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).MinimumWidth = 41
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).Resizable = DataGridViewTriState.False
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).ReadOnly = True
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).Visible = bIsAllSeasons
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).ToolTipText = Master.eLang.GetString(659, "Season #")
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).HeaderText = "#"
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).DefaultCellStyle.Format = "00"
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).Resizable = DataGridViewTriState.False
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).ReadOnly = True
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).MinimumWidth = 83
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).Visible = True
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).ToolTipText = Master.eLang.GetString(21, "Title")
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).HeaderText = Master.eLang.GetString(21, "Title")
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).MinimumWidth = 30
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).Resizable = DataGridViewTriState.False
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).ReadOnly = True
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).Visible = DataGridView_ShowColumn__TVEpisode(Database.Helpers.GetColumnName(Database.ColumnName.UserRating))
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.UserRating)).ToolTipText = Master.eLang.GetString(1467, "User Rating")

        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeNumber)).ValueType = GetType(Integer)
        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).ValueType = GetType(Integer)

        dgvTVEpisodes.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        dgvTVEpisodes.CurrentCell = Nothing
        dgvTVEpisodes.ClearSelection()

        If Not Master.isCL Then DataGridView_SortingRestore_TVEpisode(bIsAllSeasons)

        dgvTVEpisodes.Enabled = True

        AddHandler dgvTVEpisodes.SelectionChanged, AddressOf DataGridView_SelectionChanged_TVEpisodes
    End Sub

    Private Sub DataGridView_Fill_TVSeason(ByVal ShowID As Long)
        RemoveHandler dgvTVSeasons.SelectionChanged, AddressOf DataGridView_SelectionChanged_TVSeason
        _BsTVSeasons.DataSource = Nothing
        dgvTVSeasons.DataSource = Nothing
        _BsTVEpisodes.DataSource = Nothing
        dgvTVEpisodes.DataSource = Nothing

        'TODO: TVDisplayMissingEpisodes handling
        'If Master.eSettings.TVDisplayMissingEpisodes Then
        '    Master.DB.FillDataTable(dtTVSeasons, String.Format("Select * FROM {0} WHERE idShow={1} ORDER BY {2};",
        '                                                       Database.Helpers.GetMainViewName(Enums.ContentType.TVSeason),
        '                                                       ShowID,
        '                                                       Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)))
        'Else
        '    Master.DB.FillDataTable(dtTVSeasons, String.Concat("Select DISTINCT seasonlist.* ",
        '                                                        "FROM seasonlist ",
        '                                                        "LEFT OUTER JOIN episodelist On (seasonlist.idShow = episodelist.idShow) And (seasonlist.season = episodelist.season) ",
        '                                                        "WHERE seasonlist.idShow = ", ShowID, " AND (episodelist.missing = 0 OR seasonlist.season = -1) ",
        '                                                        "ORDER BY seasonlist.season;"))
        'End If
        Filter_TVSeasons = New SmartFilter.Filter(Enums.ContentType.TVSeason)
        Filter_TVSeasons.Rules.Add(New SmartFilter.Rule With {
                                     .Field = Database.ColumnName.idShow,
                                     .[Operator] = SmartFilter.Operators.Is,
                                     .Value = ShowID})
        'Filter_TVSeasons.Rules.Add(New SmartFilter.Rule With {
        '                           .Field = Database.ColumnName.IsMissing,
        '                           .[Operator] = SmartFilter.Operators.IsNot,
        '                           .Value = Master.eSettings.TVDisplayMissingEpisodes
        '                           })
        Filter_TVSeasons.OrderBy.Add(New SmartFilter.Order With {
                                       .SortedBy = Database.ColumnName.SeasonNumber
                                       })
        Filter_TVSeasons.Build()
        _DtTVSeasons = Master.DB.GetTVSeasons(Filter_TVSeasons)

        _BsTVSeasons.DataSource = _DtTVSeasons
        _BsTVSeasons.Filter = Filter_TVSeasons.FilterForBindingSource
        dgvTVSeasons.DataSource = _BsTVSeasons

        If dgvTVSeasons.Columns.Count > 0 Then
            Try
                If Manager.mSettings.TVSeason.GuiSettings.MediaListSorting.Count > 0 Then
                    For Each mColumn In Manager.mSettings.TVSeason.GuiSettings.MediaListSorting
                        dgvTVSeasons.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                    Next
                End If
            Catch ex As Exception
                _Logger.Warn("default list for season list sorting has been loaded")
                Manager.mSettings.SetDefaultsForLists(Enums.DefaultType.TVSeasonListSorting, True)
                If Manager.mSettings.TVSeason.GuiSettings.MediaListSorting.Count > 0 Then
                    For Each mColumn In Manager.mSettings.TVSeason.GuiSettings.MediaListSorting
                        dgvTVSeasons.Columns(mColumn.Column.ToString).DisplayIndex = mColumn.DisplayIndex
                    Next
                End If
            End Try
        End If

        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).DisplayIndex = 0

        For i As Integer = 0 To dgvTVSeasons.Columns.Count - 1
            dgvTVSeasons.Columns(i).Visible = False
        Next

        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).Width = 20
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).Resizable = DataGridViewTriState.False
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).ReadOnly = True
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).Visible = Not DataGridView_ShowColumn_TVSeason(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath))
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).ToolTipText = Master.eLang.GetString(838, "Banner")
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).MinimumWidth = 30
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).Resizable = DataGridViewTriState.False
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).ReadOnly = True
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).Visible = DataGridView_ShowColumn_TVSeason(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount))
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).ToolTipText = Master.eLang.GetString(682, "Episodes")
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount)).HeaderText = String.Empty
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Width = 20
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Resizable = DataGridViewTriState.False
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ReadOnly = True
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).Visible = DataGridView_ShowColumn_TVSeason(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath))
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ToolTipText = Master.eLang.GetString(149, "Fanart")
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasWatched)).Width = 20
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasWatched)).Resizable = DataGridViewTriState.False
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasWatched)).ReadOnly = True
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasWatched)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasWatched)).Visible = DataGridView_ShowColumn_TVSeason(Database.Helpers.GetColumnName(Database.ColumnName.HasWatched))
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.HasWatched)).ToolTipText = Master.eLang.GetString(981, "Watched")
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).Width = 20
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).Resizable = DataGridViewTriState.False
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).ReadOnly = True
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).Visible = DataGridView_ShowColumn_TVSeason(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath))
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).ToolTipText = Master.eLang.GetString(1035, "Landscape")
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Width = 20
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Resizable = DataGridViewTriState.False
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ReadOnly = True
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).Visible = DataGridView_ShowColumn_TVSeason(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath))
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ToolTipText = Master.eLang.GetString(148, "Poster")
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).MinimumWidth = 41
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).Resizable = DataGridViewTriState.False
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).ReadOnly = True
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).Visible = True
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).ToolTipText = Master.eLang.GetString(659, "Season #")
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).HeaderText = "#"
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).DefaultCellStyle.Format = "00"
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).Resizable = DataGridViewTriState.False
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).ReadOnly = True
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).MinimumWidth = 83
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).SortMode = DataGridViewColumnSortMode.Automatic
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).Visible = True
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).ToolTipText = Master.eLang.GetString(865, "Season Title")
        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).HeaderText = Master.eLang.GetString(865, "Season Title")

        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)).ValueType = GetType(Integer)

        dgvTVSeasons.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Title)).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        If Not Master.isCL Then DataGridView_SortingRestore_TVSeason()

        If dgvTVSeasons.RowCount > 0 Then
            DataGridView_Fill_TVEpisode(ShowID, Convert.ToInt32(dgvTVSeasons.Item(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber), 0).Value))
        End If

        AddHandler dgvTVSeasons.SelectionChanged, AddressOf DataGridView_SelectionChanged_TVSeason
    End Sub

    Private Sub DataGridView_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles _
        dgvMovies.KeyDown,
        dgvMoviesets.KeyDown,
        dgvTVEpisodes.KeyDown,
        dgvTVSeasons.KeyDown,
        dgvTVShows.KeyDown

        'stop enter key from selecting next list item
        e.Handled = (e.KeyCode = Keys.Enter)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.F Then
            Select Case True
                Case sender Is dgvMovies
                    txtSearchMovies.Focus()
                Case sender Is dgvMoviesets
                    txtSearchMovieSets.Focus()
                Case sender Is dgvTVShows
                    txtSearchShows.Focus()
            End Select
        End If
    End Sub

    Private Sub DataGridView_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles _
        dgvMovies.KeyPress,
        dgvMoviesets.KeyPress,
        dgvTVEpisodes.KeyPress,
        dgvTVSeasons.KeyPress,
        dgvTVShows.KeyPress

        Dim dgView As DataGridView = DirectCast(sender, DataGridView)
        Dim contentType As Enums.ContentType = Enums.ContentType.None
        Dim strMainId As String = String.Empty
        Dim strSearchIn As String = String.Empty

        Select Case True
            Case sender Is dgvMovies
                contentType = Enums.ContentType.Movie
                strMainId = Database.Helpers.GetMainIdName(Database.TableName.movie)
                strSearchIn = Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)
            Case sender Is dgvMoviesets
                contentType = Enums.ContentType.Movieset
                strMainId = Database.Helpers.GetMainIdName(Database.TableName.movieset)
                strSearchIn = Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)
            Case sender Is dgvTVEpisodes
                contentType = Enums.ContentType.TVEpisode
                strMainId = Database.Helpers.GetMainIdName(Database.TableName.episode)
                strSearchIn = Database.Helpers.GetColumnName(Database.ColumnName.Title)
            Case sender Is dgvTVSeasons
                contentType = Enums.ContentType.TVSeason
                strMainId = Database.Helpers.GetMainIdName(Database.TableName.season)
                strSearchIn = Database.Helpers.GetColumnName(Database.ColumnName.Title)
            Case sender Is dgvTVShows
                contentType = Enums.ContentType.TVShow
                strMainId = Database.Helpers.GetMainIdName(Database.TableName.tvshow)
                strSearchIn = Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)
        End Select

        If StringUtils.AlphaNumericOnly(e.KeyChar) OrElse e.KeyChar = Convert.ToChar(Keys.Space) Then
            KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
            tmrKeyBuffer.Start()
            For Each drvRow As DataGridViewRow In dgView.Rows
                If drvRow.Cells(strSearchIn).Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                    drvRow.Selected = True
                    dgView.CurrentCell = drvRow.Cells(strSearchIn)
                    Exit For
                End If
            Next
        ElseIf e.KeyChar = Convert.ToChar(Keys.Enter) AndAlso Not AnyBackgroundWorkerIsBusy AndAlso Not dgView.SelectedRows.Count > 1 Then
            Dim indX As Integer = dgView.SelectedRows(0).Index
            Dim lngID As Long = Convert.ToInt64(dgView.Item(strMainId, indX).Value)
            Select Case contentType
                Case Enums.ContentType.Movie
                    Dim dbElement As Database.DBElement = Master.DB.Load_Movie(lngID)
                    Edit_Movie(dbElement)
                Case Enums.ContentType.Movieset
                    Dim dbElement As Database.DBElement = Master.DB.Load_Movieset(lngID)
                    Edit_Movieset(dbElement)
                Case Enums.ContentType.TVEpisode
                    Dim dbElement As Database.DBElement = Master.DB.Load_TVEpisode(lngID, True)
                    Edit_TVEpisode(dbElement)
                Case Enums.ContentType.TVSeason
                    Dim dbElement As Database.DBElement = Master.DB.Load_TVSeason(lngID, True, False)
                    Edit_TVSeason(dbElement)
                Case Enums.ContentType.TVShow
                    Dim dbElement As Database.DBElement = Master.DB.Load_TVShow(lngID, True, False)
                    Edit_TVShow(dbElement)
            End Select
        End If
    End Sub

    Private Sub DataGridView_MouseDown_Movie(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgvMovies.MouseDown
        If e.Button = MouseButtons.Right And dgvMovies.RowCount > 0 Then
            If bwCleanDB.IsBusy OrElse bwMovieScraper.IsBusy Then
                cmnuMovieTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
                Return
            End If

            cmnuMovie.Enabled = False

            Dim dgvHTI As DataGridView.HitTestInfo = dgvMovies.HitTest(e.X, e.Y)

            If dgvHTI.Type = DataGridViewHitTestType.Cell Then
                Dim bEnableIMDB As Boolean = False
                Dim bEnableTMDB As Boolean = False
                If dgvMovies.SelectedRows.Count > 1 AndAlso dgvMovies.Rows(dgvHTI.RowIndex).Selected Then
                    Dim bShowMark As Boolean = False
                    Dim bShowUnmark As Boolean = False
                    Dim bShowLock As Boolean = False
                    Dim bShowUnlock As Boolean = False
                    Dim bShowUnwatched As Boolean = False
                    Dim bShowWatched As Boolean = False

                    cmnuMovie.Enabled = True
                    cmnuMovieChange.Enabled = False
                    cmnuMovieChangeAuto.Enabled = False
                    cmnuMovieEdit.Enabled = False
                    cmnuMovieEditMetadata.Enabled = False
                    cmnuMovieScrape.Enabled = False

                    For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                        'if any item is set as unmarked, show menu "Mark"
                        'if any item is set as marked, show menu "Unmark"
                        If Not Convert.ToBoolean(sRow.Cells(Database.Helpers.GetColumnName(Database.ColumnName.Marked)).Value) Then
                            bShowMark = True
                        Else
                            bShowUnmark = True
                        End If
                        'if any item is set as unlocked, show menu "Lock"
                        'if any item is set as locked, show menu "Unlock"
                        If Not Convert.ToBoolean(sRow.Cells(Database.Helpers.GetColumnName(Database.ColumnName.Locked)).Value) Then
                            bShowLock = True
                        Else
                            bShowUnlock = True
                        End If
                        'if any item is set as unwatched, show menu "Mark as Watched"
                        'if any item is set as watched, show menu "Mark as Unwatched"
                        If String.IsNullOrEmpty(sRow.Cells(Database.Helpers.GetColumnName(Database.ColumnName.PlayCount)).Value.ToString) OrElse sRow.Cells(Database.Helpers.GetColumnName(Database.ColumnName.PlayCount)).Value.ToString = "0" Then
                            bShowWatched = True
                        Else
                            bShowUnwatched = True
                        End If
                        'if any item has an IMDb/TMDb ID, enable button "Open IMDb/TMDb-Page"
                        If Not String.IsNullOrEmpty(sRow.Cells(Database.Helpers.GetColumnName(Database.ColumnName.UniqueIDs)).Value.ToString) Then
                            Dim nUIdContainer As New MediaContainers.UniqueidContainer(sRow.Cells(Database.Helpers.GetColumnName(Database.ColumnName.UniqueIDs)).Value.ToString)
                            If nUIdContainer.IMDbIdSpecified Then bEnableIMDB = True
                            If nUIdContainer.TMDbIdSpecified Then bEnableTMDB = True
                        End If
                        If bShowMark AndAlso bShowUnmark AndAlso bShowLock AndAlso bShowUnlock AndAlso bShowWatched AndAlso bShowUnwatched AndAlso bShowUnwatched AndAlso bEnableIMDB AndAlso bEnableTMDB Then Exit For
                    Next

                    cmnuMovieTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                    'Genre submenu
                    mnuGenresGenre.Tag = String.Empty
                    If Not mnuGenresGenre.Items.Contains(String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")) Then
                        mnuGenresGenre.Items.Insert(0, String.Concat(Master.eLang.GetString(27, "Select Genre"), "..."))
                    End If
                    mnuGenresGenre.SelectedItem = String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")
                    mnuGenresAdd.Enabled = False
                    mnuGenresNew.Text = String.Empty
                    mnuGenresRemove.Enabled = False
                    mnuGenresSet.Enabled = False

                    'Language submenu
                    mnuLanguagesLanguage.Tag = String.Empty
                    If Not mnuLanguagesLanguage.Items.Contains(String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")) Then
                        mnuLanguagesLanguage.Items.Insert(0, String.Concat(Master.eLang.GetString(1199, "Select Language"), "..."))
                    End If
                    mnuLanguagesLanguage.SelectedItem = String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")
                    mnuLanguagesSet.Enabled = False

                    'Lock / Unlock menu
                    cmnuMovieLock.Visible = bShowLock
                    cmnuMovieUnlock.Visible = bShowUnlock

                    'Mark / Unmark menu
                    cmnuMovieMark.Visible = bShowMark
                    cmnuMovieUnmark.Visible = bShowUnmark

                    'Movieset submenu
                    If mnuMoviesetsMovieset.ComboBox.Items.Count > 0 Then mnuMoviesetsMovieset.ComboBox.SelectedItem = 0

                    'Tag submenu
                    mnuTagsTag.Tag = String.Empty
                    If Not mnuTagsTag.Items.Contains(String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")) Then
                        mnuTagsTag.Items.Insert(0, String.Concat(Master.eLang.GetString(1021, "Select Tag"), "..."))
                    End If
                    mnuTagsTag.SelectedItem = String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")
                    mnuTagsAdd.Enabled = False
                    mnuTagsNew.Text = String.Empty
                    mnuTagsRemove.Enabled = False
                    mnuTagsSet.Enabled = False

                    'Watched / Unwatched menu
                    cmnuMovieWatched.Visible = bShowWatched
                    cmnuMovieUnwatched.Visible = bShowUnwatched
                Else
                    cmnuMovieChange.Enabled = True
                    cmnuMovieChangeAuto.Enabled = True
                    cmnuMovieEdit.Enabled = True
                    cmnuMovieEditMetadata.Enabled = True
                    cmnuMovieScrape.Enabled = True

                    cmnuMovieTitle.Text = String.Concat(">> ", dgvMovies.Item(Database.Helpers.GetColumnName(Database.ColumnName.Title), dgvHTI.RowIndex).Value, " <<")

                    If Not dgvMovies.Rows(dgvHTI.RowIndex).Selected Then
                        prevRow_Movie = -1
                        dgvMovies.CurrentCell = Nothing
                        dgvMovies.ClearSelection()
                        dgvMovies.Rows(dgvHTI.RowIndex).Selected = True
                        dgvMovies.CurrentCell = dgvMovies.Item(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle), dgvHTI.RowIndex)
                        'cmnuMovie.Enabled = True
                    Else
                        cmnuMovie.Enabled = True
                    End If

                    'Genre submenu
                    mnuGenresGenre.Tag = dgvMovies.Item(Database.Helpers.GetColumnName(Database.ColumnName.Genres), dgvHTI.RowIndex).Value
                    If Not mnuGenresGenre.Items.Contains(String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")) Then
                        mnuGenresGenre.Items.Insert(0, String.Concat(Master.eLang.GetString(27, "Select Genre"), "..."))
                    End If
                    mnuGenresGenre.SelectedItem = String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")
                    mnuGenresAdd.Enabled = False
                    mnuGenresNew.Text = String.Empty
                    mnuGenresRemove.Enabled = False
                    mnuGenresSet.Enabled = False

                    'Language submenu
                    Dim strLang As String = dgvMovies.Item(Database.Helpers.GetColumnName(Database.ColumnName.Language), dgvHTI.RowIndex).Value.ToString
                    Dim Language = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = strLang)
                    If Language IsNot Nothing AndAlso Not String.IsNullOrEmpty(Language.Description) Then
                        mnuLanguagesLanguage.SelectedItem = Language.Description
                    Else
                        If Not mnuLanguagesLanguage.Items.Contains(String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")) Then
                            mnuLanguagesLanguage.Items.Insert(0, String.Concat(Master.eLang.GetString(1199, "Select Language"), "..."))
                        End If
                        mnuLanguagesLanguage.SelectedItem = String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")
                    End If
                    mnuLanguagesSet.Enabled = False

                    'Lock / Unlock menu
                    Dim bIsLocked As Boolean = Convert.ToBoolean(dgvMovies.Item(Database.Helpers.GetColumnName(Database.ColumnName.Locked), dgvHTI.RowIndex).Value)
                    cmnuMovieLock.Visible = Not bIsLocked
                    cmnuMovieUnlock.Visible = bIsLocked

                    'Mark / Unmark menu
                    Dim bIsMarked As Boolean = Convert.ToBoolean(dgvMovies.Item(Database.Helpers.GetColumnName(Database.ColumnName.Marked), dgvHTI.RowIndex).Value)
                    cmnuMovieMark.Visible = Not bIsMarked
                    cmnuMovieUnmark.Visible = bIsMarked

                    'Movieset submenu
                    If mnuMoviesetsMovieset.ComboBox.Items.Count > 0 Then mnuMoviesetsMovieset.ComboBox.SelectedItem = 0

                    'Tag submenu
                    mnuTagsTag.Tag = dgvMovies.Item(Database.Helpers.GetColumnName(Database.ColumnName.Tags), dgvHTI.RowIndex).Value
                    If Not mnuTagsTag.Items.Contains(String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")) Then
                        mnuTagsTag.Items.Insert(0, String.Concat(Master.eLang.GetString(1021, "Select Tag"), "..."))
                    End If
                    mnuTagsTag.SelectedItem = String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")
                    mnuTagsAdd.Enabled = False
                    mnuTagsNew.Text = String.Empty
                    mnuTagsRemove.Enabled = False
                    mnuTagsSet.Enabled = False

                    'Website links"
                    If Not String.IsNullOrEmpty(dgvMovies.Item(Database.Helpers.GetColumnName(Database.ColumnName.UniqueIDs), dgvHTI.RowIndex).Value.ToString) Then
                        Dim nUIdContainer As New MediaContainers.UniqueidContainer(dgvMovies.Item(Database.Helpers.GetColumnName(Database.ColumnName.UniqueIDs), dgvHTI.RowIndex).Value.ToString)
                        bEnableIMDB = nUIdContainer.IMDbIdSpecified
                        bEnableTMDB = nUIdContainer.TMDbIdSpecified
                    End If

                    'Watched / Unwatched menu
                    Dim bIsWatched As Boolean = Not String.IsNullOrEmpty(dgvMovies.Item(Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed), dgvHTI.RowIndex).Value.ToString)
                    cmnuMovieWatched.Visible = Not bIsWatched
                    cmnuMovieUnwatched.Visible = bIsWatched
                End If

                'Website links
                cmnuMovieBrowseIMDB.Enabled = bEnableIMDB
                cmnuMovieBrowseTMDB.Enabled = bEnableTMDB
            Else
                cmnuMovie.Enabled = False
                cmnuMovieTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
            End If
        End If
    End Sub

    Private Sub DataGridView_MouseDown_Movieset(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgvMoviesets.MouseDown
        If e.Button = MouseButtons.Right And dgvMoviesets.RowCount > 0 Then
            If bwCleanDB.IsBusy OrElse bwMoviesetScraper.IsBusy Then
                cmnuMovieSetTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
                Return
            End If

            cmnuMovieSet.Enabled = False

            Dim dgvHTI As DataGridView.HitTestInfo = dgvMoviesets.HitTest(e.X, e.Y)

            If dgvHTI.Type = DataGridViewHitTestType.Cell Then
                Dim bEnableTMDB As Boolean = False
                If dgvMoviesets.SelectedRows.Count > 1 AndAlso dgvMoviesets.Rows(dgvHTI.RowIndex).Selected Then
                    Dim bShowMark As Boolean = False
                    Dim bShowUnmark As Boolean = False
                    Dim bShowLock As Boolean = False
                    Dim bShowUnlock As Boolean = False

                    cmnuMovieSet.Enabled = True
                    ContextMenuStrip_EnableItems(cmnuMovieSet, True)
                    cmnuMovieSetEdit.Enabled = False
                    cmnuMovieSetScrape.Enabled = False

                    For Each sRow As DataGridViewRow In dgvMoviesets.SelectedRows
                        'if any one item is set as unmarked, show menu "Mark"
                        'if any one item is set as marked, show menu "Unmark"
                        If Not Convert.ToBoolean(sRow.Cells(Database.Helpers.GetColumnName(Database.ColumnName.Marked)).Value) Then
                            bShowMark = True
                        Else
                            bShowUnmark = True
                        End If
                        'if any one item is set as unlocked, show menu "Lock"
                        'if any one item is set as locked, show menu "Unlock"
                        If Not Convert.ToBoolean(sRow.Cells(Database.Helpers.GetColumnName(Database.ColumnName.Locked)).Value) Then
                            bShowLock = True
                        Else
                            bShowUnlock = True
                        End If
                        'if any item has an TMDb ID, enable button "TMDb-Page"
                        If Not String.IsNullOrEmpty(sRow.Cells(Database.Helpers.GetColumnName(Database.ColumnName.UniqueIDs)).Value.ToString) Then
                            Dim nUIdContainer As New MediaContainers.UniqueidContainer(sRow.Cells(Database.Helpers.GetColumnName(Database.ColumnName.UniqueIDs)).Value.ToString)
                            If nUIdContainer.TMDbIdSpecified Then bEnableTMDB = True
                        End If
                        If bShowMark AndAlso bShowUnmark AndAlso bShowLock AndAlso bShowUnlock AndAlso bEnableTMDB Then Exit For
                    Next

                    cmnuMovieSetTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                    cmnuMovieSetEditSortMethodMethods.SelectedIndex = -1
                    cmnuMovieSetEditSortMethodSet.Enabled = False

                    'Language submenu
                    mnuLanguagesLanguage.Tag = String.Empty
                    If Not mnuLanguagesLanguage.Items.Contains(String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")) Then
                        mnuLanguagesLanguage.Items.Insert(0, String.Concat(Master.eLang.GetString(1199, "Select Language"), "..."))
                    End If
                    mnuLanguagesLanguage.SelectedItem = String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")
                    mnuLanguagesSet.Enabled = False

                    'Lock / Unlock menu
                    cmnuMoviesetLock.Visible = bShowLock
                    cmnuMoviesetUnlock.Visible = bShowUnlock

                    'Mark / Unmark menu
                    cmnuMoviesetMark.Visible = bShowMark
                    cmnuMoviesetUnmark.Visible = bShowUnmark
                Else
                    cmnuMovieSet.Enabled = True
                    ContextMenuStrip_EnableItems(cmnuMovieSet, True)

                    cmnuMovieSetTitle.Text = String.Concat(">> ", dgvMoviesets.Item(Database.Helpers.GetColumnName(Database.ColumnName.Title), dgvHTI.RowIndex).Value, " <<")

                    If Not dgvMoviesets.Rows(dgvHTI.RowIndex).Selected Then
                        prevRow_MovieSet = -1
                        dgvMoviesets.CurrentCell = Nothing
                        dgvMoviesets.ClearSelection()
                        dgvMoviesets.Rows(dgvHTI.RowIndex).Selected = True
                        dgvMoviesets.CurrentCell = dgvMoviesets.Item(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle), dgvHTI.RowIndex)
                        'cmnuMovieSet.Enabled = True
                    Else
                        cmnuMovieSet.Enabled = True
                    End If

                    'SortMethod submenu
                    Dim SortMethod As Integer = CInt(dgvMoviesets.Item(Database.Helpers.GetColumnName(Database.ColumnName.SortMethod), dgvHTI.RowIndex).Value)
                    cmnuMovieSetEditSortMethodMethods.Text = DirectCast(CInt(dgvMoviesets.Item(Database.Helpers.GetColumnName(Database.ColumnName.SortMethod), dgvHTI.RowIndex).Value), Enums.SortMethod_MovieSet).ToString
                    cmnuMovieSetEditSortMethodSet.Enabled = False

                    'Language submenu
                    Dim strLang As String = dgvMoviesets.Item(Database.Helpers.GetColumnName(Database.ColumnName.Language), dgvHTI.RowIndex).Value.ToString
                    Dim Language = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = strLang)
                    If Language IsNot Nothing AndAlso Not String.IsNullOrEmpty(Language.Description) Then
                        mnuLanguagesLanguage.SelectedItem = Language.Description
                    Else
                        If Not mnuLanguagesLanguage.Items.Contains(String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")) Then
                            mnuLanguagesLanguage.Items.Insert(0, String.Concat(Master.eLang.GetString(1199, "Select Language"), "..."))
                        End If
                        mnuLanguagesLanguage.SelectedItem = String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")
                    End If
                    mnuLanguagesSet.Enabled = False

                    'Lock / Unlock menu
                    Dim bIsLocked As Boolean = Convert.ToBoolean(dgvMoviesets.Item(Database.Helpers.GetColumnName(Database.ColumnName.Locked), dgvHTI.RowIndex).Value)
                    cmnuMoviesetLock.Visible = Not bIsLocked
                    cmnuMoviesetUnlock.Visible = bIsLocked

                    'Mark / Unmark menu
                    Dim bIsMarked As Boolean = Convert.ToBoolean(dgvMoviesets.Item(Database.Helpers.GetColumnName(Database.ColumnName.Marked), dgvHTI.RowIndex).Value)
                    cmnuMoviesetMark.Visible = Not bIsMarked
                    cmnuMoviesetUnmark.Visible = bIsMarked
                End If

                'Website links"
                If Not String.IsNullOrEmpty(dgvMoviesets.Item(Database.Helpers.GetColumnName(Database.ColumnName.UniqueIDs), dgvHTI.RowIndex).Value.ToString) Then
                    Dim nUIdContainer As New MediaContainers.UniqueidContainer(dgvMoviesets.Item(Database.Helpers.GetColumnName(Database.ColumnName.UniqueIDs), dgvHTI.RowIndex).Value.ToString)
                    bEnableTMDB = nUIdContainer.TMDbIdSpecified
                End If

                'Website links
                cmnuMovieSetBrowseTMDB.Enabled = bEnableTMDB
            Else
                cmnuMovieSet.Enabled = True
                ContextMenuStrip_EnableItems(cmnuMovieSet, False)
                cmnuMovieSetNew.Enabled = True
            End If
        End If
    End Sub

    Private Sub DataGridView_MouseDown_TVEpisode(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgvTVEpisodes.MouseDown
        Dim hasMissing As Boolean

        If e.Button = MouseButtons.Right And dgvTVEpisodes.RowCount > 0 Then
            cmnuEpisode.Enabled = False

            Dim dgvHTI As DataGridView.HitTestInfo = dgvTVEpisodes.HitTest(e.X, e.Y)

            If dgvHTI.Type = DataGridViewHitTestType.Cell Then
                If dgvTVEpisodes.SelectedRows.Count > 1 AndAlso dgvTVEpisodes.Rows(dgvHTI.RowIndex).Selected Then
                    cmnuEpisode.Enabled = True

                    For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                        If Convert.ToInt64(sRow.Cells("idFile").Value) = -1 Then
                            hasMissing = True
                            Exit For
                        End If
                    Next

                    cmnuEpisodeTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                    If hasMissing Then
                        ShowEpisodeMenuItems(False)
                    Else
                        Dim bShowMark As Boolean = False
                        Dim bShowUnmark As Boolean = False
                        Dim bShowLock As Boolean = False
                        Dim bShowUnlock As Boolean = False
                        Dim bShowUnwatched As Boolean = False
                        Dim bShowWatched As Boolean = False

                        ShowEpisodeMenuItems(True)

                        cmnuEpisodeEditSeparator.Visible = True
                        cmnuEpisodeEdit.Visible = False
                        cmnuEpisodeEditMetadata.Visible = False
                        cmnuEpisodeScrapeSeparator.Visible = True
                        cmnuEpisodeScrape.Visible = False
                        cmnuEpisodeChange.Visible = False
                        cmnuEpisodeSep3.Visible = False
                        cmnuEpisodeOpenFolder.Visible = False

                        For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                            'if any one item is set as unmarked, show menu "Mark"
                            'if any one item is set as marked, show menu "Unmark"
                            If Not Convert.ToBoolean(sRow.Cells("marked").Value) Then
                                bShowMark = True
                                If bShowUnmark AndAlso bShowLock AndAlso bShowUnlock AndAlso bShowWatched AndAlso bShowUnwatched Then Exit For
                            Else
                                bShowUnmark = True
                                If bShowMark AndAlso bShowLock AndAlso bShowUnlock AndAlso bShowWatched AndAlso bShowUnwatched Then Exit For
                            End If
                            'if any one item is set as unlocked, show menu "Lock"
                            'if any one item is set as locked, show menu "Unlock"
                            If Not Convert.ToBoolean(sRow.Cells("locked").Value) Then
                                bShowLock = True
                                If bShowUnlock AndAlso bShowMark AndAlso bShowUnmark AndAlso bShowWatched AndAlso bShowUnwatched Then Exit For
                            Else
                                bShowUnlock = True
                                If bShowLock AndAlso bShowMark AndAlso bShowUnmark AndAlso bShowWatched AndAlso bShowUnwatched Then Exit For
                            End If
                            'if any one item is set as unwatched, show menu "Mark as Watched"
                            'if any one item is set as watched, show menu "Mark as Unwatched"
                            If String.IsNullOrEmpty(sRow.Cells("playcount").Value.ToString) OrElse sRow.Cells("playcount").Value.ToString = "0" Then
                                bShowWatched = True
                                If bShowLock AndAlso bShowUnlock AndAlso bShowMark AndAlso bShowUnmark AndAlso bShowUnwatched Then Exit For
                            Else
                                bShowUnwatched = True
                                If bShowLock AndAlso bShowUnlock AndAlso bShowMark AndAlso bShowUnmark AndAlso bShowWatched Then Exit For
                            End If
                        Next

                        'Lock / Unlock menu
                        cmnuEpisodeLock.Visible = bShowLock
                        cmnuEpisodeUnlock.Visible = bShowUnlock

                        'Mark / Unmark menu
                        cmnuEpisodeMark.Visible = bShowMark
                        cmnuEpisodeUnmark.Visible = bShowUnmark

                        'Watched / Unwatched menu
                        cmnuEpisodeWatched.Visible = bShowWatched
                        cmnuEpisodeUnwatched.Visible = bShowUnwatched
                    End If
                Else
                    cmnuEpisodeTitle.Text = String.Concat(">> ", dgvTVEpisodes.Item("title", dgvHTI.RowIndex).Value, " <<")

                    If Not dgvTVEpisodes.Rows(dgvHTI.RowIndex).Selected OrElse Not currList = 2 Then
                        prevRow_TVEpisode = -1
                        currList = 2
                        dgvTVEpisodes.CurrentCell = Nothing
                        dgvTVEpisodes.ClearSelection()
                        dgvTVEpisodes.Rows(dgvHTI.RowIndex).Selected = True
                        dgvTVEpisodes.CurrentCell = dgvTVEpisodes.Item("title", dgvHTI.RowIndex)
                    Else
                        cmnuEpisode.Enabled = True
                    End If

                    If Convert.ToInt64(dgvTVEpisodes.Item("idFile", dgvHTI.RowIndex).Value) = -1 Then hasMissing = True

                    If hasMissing Then
                        ShowEpisodeMenuItems(False)
                    Else
                        ShowEpisodeMenuItems(True)

                        cmnuEpisodeEditSeparator.Visible = True
                        cmnuEpisodeEdit.Visible = True
                        cmnuEpisodeEditMetadata.Visible = True
                        cmnuEpisodeScrapeSeparator.Visible = True
                        cmnuEpisodeScrape.Visible = True
                        cmnuEpisodeChange.Visible = True
                        cmnuEpisodeSep3.Visible = True
                        cmnuEpisodeOpenFolder.Visible = True

                        cmnuEpisodeMark.Text = If(Convert.ToBoolean(dgvTVEpisodes.Item("marked", dgvHTI.RowIndex).Value), Master.eLang.GetString(107, "Unmark"), Master.eLang.GetString(23, "Mark"))

                        'Lock / Unlock menu
                        Dim bIsLocked As Boolean = Convert.ToBoolean(dgvTVEpisodes.Item("locked", dgvHTI.RowIndex).Value)
                        cmnuEpisodeLock.Visible = Not bIsLocked
                        cmnuEpisodeUnlock.Visible = bIsLocked

                        'Mark / Unmark menu
                        Dim bIsMarked As Boolean = Convert.ToBoolean(dgvTVEpisodes.Item("marked", dgvHTI.RowIndex).Value)
                        cmnuEpisodeMark.Visible = Not bIsMarked
                        cmnuEpisodeUnmark.Visible = bIsMarked

                        'Watched / Unwatched menu
                        Dim bIsWatched As Boolean = Not String.IsNullOrEmpty(dgvTVEpisodes.Item("playcount", dgvHTI.RowIndex).Value.ToString) AndAlso Not dgvTVEpisodes.Item("playcount", dgvHTI.RowIndex).Value.ToString = "0"
                        cmnuEpisodeWatched.Visible = Not bIsWatched
                        cmnuEpisodeUnwatched.Visible = bIsWatched
                    End If

                End If
            Else
                cmnuEpisode.Enabled = False
                cmnuEpisodeTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
            End If
        End If
    End Sub

    Private Sub DataGridView_MouseDown_TVSeason(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgvTVSeasons.MouseDown
        If e.Button = MouseButtons.Right And dgvTVSeasons.RowCount > 0 Then

            cmnuSeason.Enabled = False

            Dim dgvHTI As DataGridView.HitTestInfo = dgvTVSeasons.HitTest(e.X, e.Y)
            If dgvHTI.Type = DataGridViewHitTestType.Cell Then

                If dgvTVSeasons.SelectedRows.Count > 1 AndAlso dgvTVSeasons.Rows(dgvHTI.RowIndex).Selected Then
                    Dim bShowMark As Boolean = False
                    Dim bShowUnmark As Boolean = False
                    Dim bShowLock As Boolean = False
                    Dim bShowUnlock As Boolean = False
                    Dim bShowUnwatched As Boolean = False
                    Dim bShowWatched As Boolean = False

                    cmnuSeason.Enabled = True
                    cmnuSeasonEdit.Visible = False
                    cmnuSeasonEditSeparator.Visible = False
                    cmnuSeasonScrape.Visible = False

                    For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                        'if any one item is set as unmarked, show menu "Mark"
                        'if any one item is set as marked, show menu "Unmark"
                        If Not Convert.ToBoolean(sRow.Cells("marked").Value) Then
                            bShowMark = True
                            If bShowUnmark AndAlso bShowLock AndAlso bShowUnlock AndAlso bShowWatched AndAlso bShowUnwatched Then Exit For
                        Else
                            bShowUnmark = True
                            If bShowMark AndAlso bShowLock AndAlso bShowUnlock AndAlso bShowWatched AndAlso bShowUnwatched Then Exit For
                        End If
                        'if any one item is set as unlocked, show menu "Lock"
                        'if any one item is set as locked, show menu "Unlock"
                        If Not Convert.ToBoolean(sRow.Cells("locked").Value) Then
                            bShowLock = True
                            If bShowUnlock AndAlso bShowMark AndAlso bShowUnmark AndAlso bShowWatched AndAlso bShowUnwatched Then Exit For
                        Else
                            bShowUnlock = True
                            If bShowLock AndAlso bShowMark AndAlso bShowUnmark AndAlso bShowWatched AndAlso bShowUnwatched Then Exit For
                        End If
                        'if any one item is set as unlocked, show menu "Lock"
                        'if any one item is set as locked, show menu "Unlock"
                        If Not CInt(sRow.Cells("season").Value) = -1 AndAlso Not Convert.ToBoolean(sRow.Cells("hasWatched").Value) Then
                            bShowWatched = True
                            If bShowLock AndAlso bShowUnlock AndAlso bShowMark AndAlso bShowUnmark AndAlso bShowUnwatched Then Exit For
                        Else
                            bShowUnwatched = True
                            If bShowLock AndAlso bShowUnlock AndAlso bShowMark AndAlso bShowUnmark AndAlso bShowWatched Then Exit For
                        End If
                    Next

                    cmnuSeasonTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                    'Lock / Unlock menu
                    cmnuSeasonLock.Visible = bShowLock
                    cmnuSeasonUnlock.Visible = bShowUnlock

                    'Mark / Unmark menu
                    cmnuSeasonMark.Visible = bShowMark
                    cmnuSeasonUnmark.Visible = bShowUnmark

                    'Watched / Unwatched menu
                    cmnuSeasonWatched.Visible = bShowWatched
                    cmnuSeasonUnwatched.Visible = bShowUnwatched

                Else
                    cmnuSeasonEdit.Visible = True
                    cmnuSeasonEditSeparator.Visible = True
                    cmnuSeasonScrape.Visible = True

                    cmnuSeasonTitle.Text = String.Concat(">> ", dgvTVSeasons.Item("title", dgvHTI.RowIndex).Value, " <<")
                    cmnuSeasonEdit.Enabled = Convert.ToInt32(dgvTVSeasons.Item("season", dgvHTI.RowIndex).Value) >= 0

                    If Not dgvTVSeasons.Rows(dgvHTI.RowIndex).Selected OrElse Not currList = 1 Then
                        prevRow_TVSeason = -1
                        currList = 1
                        dgvTVSeasons.CurrentCell = Nothing
                        dgvTVSeasons.ClearSelection()
                        dgvTVSeasons.Rows(dgvHTI.RowIndex).Selected = True
                        dgvTVSeasons.CurrentCell = dgvTVSeasons.Item("title", dgvHTI.RowIndex)
                    Else
                        cmnuSeason.Enabled = True
                    End If

                    'Lock / Unlock menu
                    Dim bIsLocked As Boolean = Convert.ToBoolean(dgvTVSeasons.Item("locked", dgvHTI.RowIndex).Value)
                    cmnuSeasonLock.Visible = Not bIsLocked
                    cmnuSeasonUnlock.Visible = bIsLocked

                    'Mark / Unmark menu
                    Dim bIsMarked As Boolean = Convert.ToBoolean(dgvTVSeasons.Item("marked", dgvHTI.RowIndex).Value)
                    cmnuSeasonMark.Visible = Not bIsMarked
                    cmnuSeasonUnmark.Visible = bIsMarked

                    'Watched / Unwatched menu
                    Dim bIsWatched As Boolean = False
                    Dim bIsAllSeasons As Boolean = CInt(dgvTVSeasons.Item("season", dgvHTI.RowIndex).Value) = -1
                    If Not bIsAllSeasons Then
                        bIsWatched = Convert.ToBoolean(dgvTVSeasons.Item("hasWatched", dgvHTI.RowIndex).Value)
                    End If
                    cmnuSeasonWatched.Visible = Not bIsWatched AndAlso Not bIsAllSeasons
                    cmnuSeasonUnwatched.Visible = bIsWatched AndAlso Not bIsAllSeasons
                End If
            Else
                cmnuSeason.Enabled = False
                cmnuSeasonTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
            End If
        End If
    End Sub

    Private Sub DataGridView_MouseDown_TVShow(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgvTVShows.MouseDown
        If e.Button = MouseButtons.Right And dgvTVShows.RowCount > 0 Then

            cmnuShow.Enabled = False

            Dim dgvHTI As DataGridView.HitTestInfo = dgvTVShows.HitTest(e.X, e.Y)

            If dgvHTI.Type = DataGridViewHitTestType.Cell Then
                If dgvTVShows.SelectedRows.Count > 1 AndAlso dgvTVShows.Rows(dgvHTI.RowIndex).Selected Then
                    Dim bShowMark As Boolean = False
                    Dim bShowUnmark As Boolean = False
                    Dim bShowLock As Boolean = False
                    Dim bShowUnlock As Boolean = False
                    Dim bShowUnwatched As Boolean = False
                    Dim bShowWatched As Boolean = False

                    cmnuShow.Enabled = True
                    cmnuShowChange.Visible = False
                    cmnuShowEdit.Visible = False
                    cmnuShowScrape.Visible = False

                    For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                        'if any one item is set as unmarked, show menu "Mark"
                        'if any one item is set as marked, show menu "Unmark"
                        If Not Convert.ToBoolean(sRow.Cells("marked").Value) Then
                            bShowMark = True
                            If bShowUnmark AndAlso bShowLock AndAlso bShowUnlock AndAlso bShowWatched AndAlso bShowUnwatched Then Exit For
                        Else
                            bShowUnmark = True
                            If bShowMark AndAlso bShowLock AndAlso bShowUnlock AndAlso bShowWatched AndAlso bShowUnwatched Then Exit For
                        End If
                        'if any one item is set as unlocked, show menu "Lock"
                        'if any one item is set as locked, show menu "Unlock"
                        If Not Convert.ToBoolean(sRow.Cells("locked").Value) Then
                            bShowLock = True
                            If bShowUnlock AndAlso bShowMark AndAlso bShowUnmark AndAlso bShowWatched AndAlso bShowUnwatched Then Exit For
                        Else
                            bShowUnlock = True
                            If bShowLock AndAlso bShowMark AndAlso bShowUnmark AndAlso bShowWatched AndAlso bShowUnwatched Then Exit For
                        End If
                        'if any one item is set as unwatched, show menu "Mark as Watched"
                        'if any one item is set as watched, show menu "Mark as Unwatched"
                        If Not Convert.ToBoolean(sRow.Cells("hasWatched").Value) Then
                            bShowWatched = True
                            If bShowLock AndAlso bShowUnlock AndAlso bShowMark AndAlso bShowUnmark AndAlso bShowUnwatched Then Exit For
                        Else
                            bShowUnwatched = True
                            If bShowLock AndAlso bShowUnlock AndAlso bShowMark AndAlso bShowUnmark AndAlso bShowWatched Then Exit For
                        End If
                    Next

                    cmnuShowTitle.Text = Master.eLang.GetString(106, ">> Multiple <<")

                    'Genre submenu
                    mnuGenresGenre.Tag = String.Empty
                    If Not mnuGenresGenre.Items.Contains(String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")) Then
                        mnuGenresGenre.Items.Insert(0, String.Concat(Master.eLang.GetString(27, "Select Genre"), "..."))
                    End If
                    mnuGenresGenre.SelectedItem = String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")
                    mnuGenresAdd.Enabled = False
                    mnuGenresNew.Text = String.Empty
                    mnuGenresRemove.Enabled = False
                    mnuGenresSet.Enabled = False

                    'Language submenu
                    mnuLanguagesLanguage.Tag = String.Empty
                    If Not mnuLanguagesLanguage.Items.Contains(String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")) Then
                        mnuLanguagesLanguage.Items.Insert(0, String.Concat(Master.eLang.GetString(1199, "Select Language"), "..."))
                    End If
                    mnuLanguagesLanguage.SelectedItem = String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")
                    mnuLanguagesSet.Enabled = False

                    'Lock / Unlock menu
                    cmnuShowLock.Visible = bShowLock
                    cmnuShowUnlock.Visible = bShowUnlock

                    'Mark / Unmark menu
                    cmnuShowMark.Visible = bShowMark
                    cmnuShowUnmark.Visible = bShowUnmark

                    'Tag submenu
                    mnuTagsTag.Tag = String.Empty
                    If Not mnuTagsTag.Items.Contains(String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")) Then
                        mnuTagsTag.Items.Insert(0, String.Concat(Master.eLang.GetString(1021, "Select Tag"), "..."))
                    End If
                    mnuTagsTag.SelectedItem = String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")
                    mnuTagsAdd.Enabled = False
                    mnuTagsNew.Text = String.Empty
                    mnuTagsRemove.Enabled = False
                    mnuTagsSet.Enabled = False

                    'Watched / Unwatched menu
                    cmnuShowWatched.Visible = bShowWatched
                    cmnuShowUnwatched.Visible = bShowUnwatched
                Else
                    cmnuShowChange.Visible = True
                    cmnuShowEdit.Visible = True
                    cmnuShowScrape.Visible = True

                    cmnuShowTitle.Text = String.Concat(">> ", dgvTVShows.Item("title", dgvHTI.RowIndex).Value, " <<")

                    If Not dgvTVShows.Rows(dgvHTI.RowIndex).Selected OrElse Not currList = 0 Then
                        prevRow_TVShow = -1
                        currList = 0
                        dgvTVShows.CurrentCell = Nothing
                        dgvTVShows.ClearSelection()
                        dgvTVShows.Rows(dgvHTI.RowIndex).Selected = True
                        dgvTVShows.CurrentCell = dgvTVShows.Item("listTitle", dgvHTI.RowIndex)
                        'cmnuShow.Enabled = True
                    Else
                        cmnuShow.Enabled = True
                    End If

                    'Genre submenu
                    mnuGenresGenre.Tag = dgvTVShows.Item("genre", dgvHTI.RowIndex).Value
                    If Not mnuGenresGenre.Items.Contains(String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")) Then
                        mnuGenresGenre.Items.Insert(0, String.Concat(Master.eLang.GetString(27, "Select Genre"), "..."))
                    End If
                    mnuGenresGenre.SelectedItem = String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")
                    mnuGenresAdd.Enabled = False
                    mnuGenresNew.Text = String.Empty
                    mnuGenresRemove.Enabled = False
                    mnuGenresSet.Enabled = False

                    'Language submenu
                    Dim strLang As String = dgvTVShows.Item("language", dgvHTI.RowIndex).Value.ToString
                    Dim Language = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = strLang)
                    If Language IsNot Nothing AndAlso Not String.IsNullOrEmpty(Language.Description) Then
                        mnuLanguagesLanguage.SelectedItem = Language.Description
                    Else
                        If Not mnuLanguagesLanguage.Items.Contains(String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")) Then
                            mnuLanguagesLanguage.Items.Insert(0, String.Concat(Master.eLang.GetString(1199, "Select Language"), "..."))
                        End If
                        mnuLanguagesLanguage.SelectedItem = String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")
                    End If
                    mnuLanguagesSet.Enabled = False

                    'Lock / Unlock menu
                    Dim bIsLocked As Boolean = Convert.ToBoolean(dgvTVShows.Item("locked", dgvHTI.RowIndex).Value)
                    cmnuShowLock.Visible = Not bIsLocked
                    cmnuShowUnlock.Visible = bIsLocked

                    'Mark / Unmark menu
                    Dim bIsMarked As Boolean = Convert.ToBoolean(dgvTVShows.Item("marked", dgvHTI.RowIndex).Value)
                    cmnuShowMark.Visible = Not bIsMarked
                    cmnuShowUnmark.Visible = bIsMarked

                    'Tag submenu
                    mnuTagsTag.Tag = dgvTVShows.Item("tag", dgvHTI.RowIndex).Value
                    If Not mnuTagsTag.Items.Contains(String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")) Then
                        mnuTagsTag.Items.Insert(0, String.Concat(Master.eLang.GetString(1021, "Select Tag"), "..."))
                    End If
                    mnuTagsTag.SelectedItem = String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")
                    mnuTagsAdd.Enabled = False
                    mnuTagsNew.Text = String.Empty
                    mnuTagsRemove.Enabled = False
                    mnuTagsSet.Enabled = False

                    'Watched / Unwatched menu
                    Dim bIsWatched As Boolean = Convert.ToBoolean(dgvTVShows.Item("hasWatched", dgvHTI.RowIndex).Value)
                    cmnuShowWatched.Visible = Not bIsWatched
                    cmnuShowUnwatched.Visible = bIsWatched
                End If
            Else
                cmnuShow.Enabled = False
                cmnuShowTitle.Text = Master.eLang.GetString(845, ">> No Item Selected <<")
            End If
        End If
    End Sub
    ''' <summary>
    ''' Refresh a single Movie row with informations from DB
    ''' </summary>
    ''' <param name="MovieID"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView_RefreshRow_Movie(ByVal MovieID As Long)
        Dim myDelegate As New Delegate_dtListUpdateRow(AddressOf DataTable_UpdateRow)
        Dim newDRow As DataRow = Nothing

        Dim nFilter = New SmartFilter.Filter(Enums.ContentType.Movie)
        nFilter.Rules.Add(New SmartFilter.Rule With {
                              .Field = Database.ColumnName.idMovie,
                              .[Operator] = SmartFilter.Operators.Is,
                              .Value = MovieID})

        Dim newTable = Master.DB.GetMovies(nFilter)
        If newTable.Rows.Count > 0 Then
            newDRow = newTable.Rows.Item(0)
        End If

        Dim oldDRow As DataRow = _DtMovies.Select(String.Format("{0} = {1}",
                                                               Database.Helpers.GetMainIdName(Database.TableName.movie),
                                                               MovieID.ToString)
                                                               ).FirstOrDefault()

        If oldDRow IsNot Nothing AndAlso newDRow IsNot Nothing Then
            Try
                If InvokeRequired Then
                    Invoke(myDelegate, New Object() {oldDRow, newDRow})
                Else
                    oldDRow.ItemArray = newDRow.ItemArray
                End If
            Catch ex As Exception
                'catch the situation in which a row has been removed at the same time we try to refresh the row (it's nothing to do)
            End Try
        End If

        If dgvMovies.Visible AndAlso dgvMovies.SelectedRows.Count > 0 AndAlso CInt(dgvMovies.SelectedRows(0).Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value) = MovieID Then
            DataGridView_SelectRow_Movie(dgvMovies.SelectedRows(0).Index)
        End If

        dgvMovies.Invalidate()
    End Sub
    ''' <summary>
    ''' Refresh a single MovieSet row with informations from DB
    ''' </summary>
    ''' <param name="MovieSetID"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView_RefreshRow_Movieset(ByVal MovieSetID As Long)
        Dim myDelegate As New Delegate_dtListUpdateRow(AddressOf DataTable_UpdateRow)
        Dim newDRow As DataRow = Nothing

        Dim nFilter = New SmartFilter.Filter(Enums.ContentType.Movieset)
        nFilter.Rules.Add(New SmartFilter.Rule With {
                              .Field = Database.ColumnName.idSet,
                              .[Operator] = SmartFilter.Operators.Is,
                              .Value = MovieSetID})

        Dim newTable = Master.DB.GetMoviesets(nFilter)
        If newTable.Rows.Count > 0 Then
            newDRow = newTable.Rows.Item(0)
        End If

        Dim oldDRow As DataRow = _DtMovieSets.Select(String.Format("{0} = {1}",
                                                                  Database.Helpers.GetMainIdName(Database.TableName.movieset),
                                                                  MovieSetID.ToString)
                                                                  ).FirstOrDefault()

        If oldDRow IsNot Nothing AndAlso newDRow IsNot Nothing Then
            Try
                If InvokeRequired Then
                    Invoke(myDelegate, New Object() {oldDRow, newDRow})
                Else
                    oldDRow.ItemArray = newDRow.ItemArray
                End If
            Catch ex As Exception
                'catch the situation in which a row has been removed at the same time we try to refresh the row (it's nothing to do)
            End Try
        End If

        If dgvMoviesets.Visible AndAlso dgvMoviesets.SelectedRows.Count > 0 AndAlso CInt(dgvMoviesets.SelectedRows(0).Cells(Database.Helpers.GetMainIdName(Database.TableName.movieset)).Value) = MovieSetID Then
            DataGridView_SelectRow_Movieset(dgvMoviesets.SelectedRows(0).Index)
        End If

        dgvMoviesets.Invalidate()
    End Sub
    ''' <summary>
    ''' Refresh a single TVEpsiode row with informations from DB
    ''' </summary>
    ''' <param name="EpisodeID"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView_RefreshRow_TVEpisode(ByVal EpisodeID As Long)
        If _DtTVEpisodes.Rows.Count > 0 Then
            Dim myDelegate As New Delegate_dtListUpdateRow(AddressOf DataTable_UpdateRow)
            Dim newDRow As DataRow = Nothing

            Dim nFilter = New SmartFilter.Filter(Enums.ContentType.TVEpisode)
            nFilter.Rules.Add(New SmartFilter.Rule With {
                              .Field = Database.ColumnName.idEpisode,
                              .[Operator] = SmartFilter.Operators.Is,
                              .Value = EpisodeID})

            Dim newTable = Master.DB.GetTVEpisodes(nFilter)
            If newTable.Rows.Count > 0 Then
                newDRow = newTable.Rows.Item(0)
            End If

            Dim oldDRow As DataRow = _DtTVEpisodes.Select(String.Format("{0} = {1}",
                                                                       Database.Helpers.GetMainIdName(Database.TableName.episode),
                                                                       EpisodeID.ToString)
                                                                       ).FirstOrDefault()

            If oldDRow IsNot Nothing AndAlso newDRow IsNot Nothing Then
                Try
                    If InvokeRequired Then
                        Invoke(myDelegate, New Object() {oldDRow, newDRow})
                    Else
                        oldDRow.ItemArray = newDRow.ItemArray
                    End If
                Catch ex As Exception
                    'catch the situation in which a row has been removed at the same time we try to refresh the row (it's nothing to do)
                End Try
            End If

            If dgvTVEpisodes.Visible AndAlso dgvTVEpisodes.SelectedRows.Count > 0 AndAlso CInt(dgvTVEpisodes.SelectedRows(0).Cells(Database.Helpers.GetMainIdName(Database.TableName.episode)).Value) = EpisodeID AndAlso currList = 2 Then
                DataGridView_SelectRow_TVEpisode(dgvTVEpisodes.SelectedRows(0).Index)
            End If

            dgvTVEpisodes.Invalidate()
        End If
    End Sub
    ''' <summary>
    ''' Refresh a single TVSeason row with informations from DB
    ''' </summary>
    ''' <param name="SeasonID"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView_RefreshRow_TVSeason(ByVal SeasonID As Long)
        If _DtTVSeasons.Rows.Count > 0 Then
            Dim myDelegate As New Delegate_dtListUpdateRow(AddressOf DataTable_UpdateRow)
            Dim newDRow As DataRow = Nothing

            Dim nFilter = New SmartFilter.Filter(Enums.ContentType.TVSeason)
            nFilter.Rules.Add(New SmartFilter.Rule With {
                              .Field = Database.ColumnName.idSeason,
                              .[Operator] = SmartFilter.Operators.Is,
                              .Value = SeasonID})

            Dim newTable = Master.DB.GetTVSeasons(nFilter)
            If newTable.Rows.Count > 0 Then
                newDRow = newTable.Rows.Item(0)
            End If

            Dim oldDRow As DataRow = _DtTVSeasons.Select(String.Format("{0} = {1}",
                                                                      Database.Helpers.GetMainIdName(Database.TableName.season),
                                                                      SeasonID.ToString)
                                                                      ).FirstOrDefault()

            If oldDRow IsNot Nothing AndAlso newDRow IsNot Nothing Then
                Try
                    If InvokeRequired Then
                        Invoke(myDelegate, New Object() {oldDRow, newDRow})
                    Else
                        oldDRow.ItemArray = newDRow.ItemArray
                    End If
                Catch ex As Exception
                    'catch the situation in which a row has been removed at the same time we try to refresh the row (it's nothing to do)
                End Try
            End If

            If dgvTVSeasons.Visible AndAlso dgvTVSeasons.SelectedRows.Count > 0 AndAlso CInt(dgvTVSeasons.SelectedRows(0).Cells(Database.Helpers.GetMainIdName(Database.TableName.season)).Value) = SeasonID AndAlso currList = 1 Then
                DataGridView_SelectRow_TVSeason(dgvTVSeasons.SelectedRows(0).Index)
            End If

            dgvTVSeasons.Invalidate()
        End If
    End Sub

    Private Sub DataGridView_RefreshRow_TVSeason(ByVal ShowID As Long, ByVal iSeason As Integer)
        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLNewcommand.CommandText = String.Format("SELECT {0} FROM {1} WHERE {2} = {3} AND {4} = {5};",
                                                      Database.Helpers.GetMainIdName(Database.TableName.season),
                                                      Database.Helpers.GetTableName(Database.TableName.season),
                                                      Database.Helpers.GetMainIdName(Database.TableName.tvshow),
                                                      ShowID,
                                                      Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber),
                                                      iSeason)
            Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                SQLreader.Read()
                If SQLreader.HasRows Then
                    DataGridView_RefreshRow_TVSeason(Convert.ToInt64(SQLreader(Database.Helpers.GetMainIdName(Database.TableName.season))))
                End If
            End Using
        End Using
    End Sub
    ''' <summary>
    ''' Refresh a single TVShow row with informations from DB
    ''' </summary>
    ''' <param name="ShowID"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView_RefreshRow_TVShow(ByVal ShowID As Long, Optional ByVal Force As Boolean = False)
        Dim myDelegate As New Delegate_dtListUpdateRow(AddressOf DataTable_UpdateRow)
        Dim newDRow As DataRow = Nothing

        Dim nFilter = New SmartFilter.Filter(Enums.ContentType.TVShow)
        nFilter.Rules.Add(New SmartFilter.Rule With {
                              .Field = Database.ColumnName.idShow,
                              .[Operator] = SmartFilter.Operators.Is,
                              .Value = ShowID})

        Dim newTable = Master.DB.GetTVShows(nFilter)
        If newTable.Rows.Count > 0 Then
            newDRow = newTable.Rows.Item(0)
        End If

        Dim oldDRow As DataRow = _DtTVShows.Select(String.Format("{0} = {1}",
                                                                Database.Helpers.GetMainIdName(Database.TableName.tvshow),
                                                                ShowID.ToString)
                                                                ).FirstOrDefault()

        If oldDRow IsNot Nothing AndAlso newDRow IsNot Nothing Then
            Try
                If InvokeRequired Then
                    Invoke(myDelegate, New Object() {oldDRow, newDRow})
                Else
                    oldDRow.ItemArray = newDRow.ItemArray
                End If
            Catch ex As Exception
                'catch the situation in which a row has been removed at the same time we try to refresh the row (it's nothing to do)
            End Try
        End If

        If dgvTVShows.Visible AndAlso dgvTVShows.SelectedRows.Count > 0 AndAlso CInt(dgvTVShows.SelectedRows(0).Cells(Database.Helpers.GetMainIdName(Database.TableName.tvshow)).Value) = ShowID AndAlso (currList = 0 OrElse Force) Then
            DataGridView_SelectRow_TVShow(dgvTVShows.SelectedRows(0).Index)
        End If

        dgvTVShows.Invalidate()
    End Sub
    ''' <summary>
    ''' Removes a single Movie row from list
    ''' </summary>
    ''' <param name="ID">Movie ID</param>
    ''' <remarks></remarks>
    Private Sub DataGridView_RemoveRow_Movie(ByVal ID As Long)
        Dim myDelegate As New Delegate_dtListRemoveRow(AddressOf DataTable_RemoveRow)

        Dim dRow = From drvRow In _DtMovies.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item(Database.Helpers.GetMainIdName(Database.TableName.movie))) = ID Select drvRow

        If dRow(0) IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {_DtMovies, dRow(0)})
            Else
                _DtMovies.Rows.Remove(DirectCast(dRow(0), DataRow))
            End If
        End If
        currRow_Movie = -1
        prevRow_Movie = -2
    End Sub
    ''' <summary>
    ''' Removes a single MovieSet row from list
    ''' </summary>
    ''' <param name="ID">MovieSet ID</param>
    ''' <remarks></remarks>
    Private Sub DataGridView_RemoveRow_Movieset(ByVal ID As Long)
        Dim myDelegate As New Delegate_dtListRemoveRow(AddressOf DataTable_RemoveRow)

        Dim dRow = From drvRow In _DtMovieSets.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idSet")) = ID Select drvRow

        If dRow(0) IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {_DtMovieSets, dRow(0)})
            Else
                _DtMovieSets.Rows.Remove(DirectCast(dRow(0), DataRow))
            End If
        End If
        currRow_MovieSet = -1
        prevRow_MovieSet = -2
    End Sub
    ''' <summary>
    ''' Removes a single TVEpisode row from list
    ''' </summary>
    ''' <param name="ID">TVEpisode ID</param>
    ''' <remarks></remarks>
    Private Sub DataGridView_RemoveRow_TVEpisode(ByVal ID As Long)
        Dim myDelegate As New Delegate_dtListRemoveRow(AddressOf DataTable_RemoveRow)

        Dim dRow = From drvRow In _DtTVEpisodes.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idEpisode")) = ID Select drvRow

        If dRow(0) IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {_DtTVEpisodes, dRow(0)})
            Else
                _DtTVEpisodes.Rows.Remove(DirectCast(dRow(0), DataRow))
            End If
        End If
        currRow_TVEpisode = -1
        prevRow_TVEpisode = -2
    End Sub
    ''' <summary>
    ''' Removes a single TVSeason row from list
    ''' </summary>
    ''' <param name="ID">TVSeason ID</param>
    ''' <remarks></remarks>
    Private Sub DataGridView_RemoveRow_TVSeason(ByVal ID As Long)
        Dim myDelegate As New Delegate_dtListRemoveRow(AddressOf DataTable_RemoveRow)

        Dim dRow = From drvRow In _DtTVSeasons.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idSeason")) = ID Select drvRow

        If dRow(0) IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {_DtTVSeasons, dRow(0)})
            Else
                _DtTVSeasons.Rows.Remove(DirectCast(dRow(0), DataRow))
            End If
        End If
        currRow_TVSeason = -1
        prevRow_TVSeason = -2
    End Sub
    ''' <summary>
    ''' Removes a single TVShow row from list
    ''' </summary>
    ''' <param name="ID">TVShow ID</param>
    ''' <remarks></remarks>
    Private Sub DataGridView_RemoveRow_TVShow(ByVal ID As Long)
        Dim myDelegate As New Delegate_dtListRemoveRow(AddressOf DataTable_RemoveRow)

        Dim dRow = From drvRow In _DtTVShows.Rows Where Convert.ToInt64(DirectCast(drvRow, DataRow).Item("idShow")) = ID Select drvRow

        If dRow(0) IsNot Nothing Then
            If InvokeRequired Then
                Invoke(myDelegate, New Object() {_DtTVShows, dRow(0)})
            Else
                _DtTVShows.Rows.Remove(DirectCast(dRow(0), DataRow))
            End If
        End If
        currRow_TVShow = -1
        prevRow_TVShow = -2
    End Sub

    Private Sub DataGridView_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles _
        dgvMovies.RowsAdded,
        dgvMoviesets.RowsAdded,
        dgvTVShows.RowsAdded

        Select Case True
            Case sender Is dgvMovies
                MainTab_SetCount_Movie()
            Case sender Is dgvMoviesets
                MainTab_SetCount_Movieset()
            Case sender Is dgvTVEpisodes
            Case sender Is dgvTVSeasons
            Case sender Is dgvTVShows
                If dgvTVShows.RowCount = 0 OrElse dgvTVShows.SelectedRows.Count = 0 Then
                    _BsTVSeasons.DataSource = Nothing
                    dgvTVSeasons.DataSource = Nothing
                    _BsTVEpisodes.DataSource = Nothing
                    dgvTVEpisodes.DataSource = Nothing
                End If
                MainTab_SetCount_TV()
        End Select
    End Sub

    Private Sub DataGridView_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles _
        dgvMovies.RowsRemoved,
        dgvMoviesets.RowsRemoved,
         dgvTVShows.RowsRemoved

        Select Case True
            Case sender Is dgvMovies
                MainTab_SetCount_Movie()
            Case sender Is dgvMoviesets
                MainTab_SetCount_Movieset()
            Case sender Is dgvTVEpisodes
            Case sender Is dgvTVSeasons
            Case sender Is dgvTVShows
                If dgvTVShows.RowCount = 0 OrElse dgvTVShows.SelectedRows.Count = 0 Then
                    _BsTVSeasons.DataSource = Nothing
                    dgvTVSeasons.DataSource = Nothing
                    _BsTVEpisodes.DataSource = Nothing
                    dgvTVEpisodes.DataSource = Nothing
                End If
                MainTab_SetCount_TV()
        End Select
    End Sub

    Private Sub DataGridView_SelectionChanged_Movie(sender As Object, e As EventArgs) Handles dgvMovies.SelectionChanged
        If dgvMovies.SelectedRows.Count > 0 Then
            If dgvMovies.SelectedRows.Count > 1 Then
                SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvMovies.SelectedRows.Count))
            ElseIf dgvMovies.SelectedRows.Count = 1 Then
                SetStatus(dgvMovies.SelectedRows(0).Cells("path").Value.ToString)
            End If
            currRow_Movie = dgvMovies.SelectedRows(0).Index
        Else
            currRow_Movie = -3
        End If
    End Sub

    Private Sub DataGridView_SelectionChanged_Movieset(sender As Object, e As EventArgs) Handles dgvMoviesets.SelectionChanged
        If dgvMoviesets.SelectedRows.Count > 0 Then
            If dgvMoviesets.SelectedRows.Count > 1 Then
                SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvMoviesets.SelectedRows.Count))
            ElseIf dgvMoviesets.SelectedRows.Count = 1 Then
                SetStatus(dgvMoviesets.SelectedRows(0).Cells("title").Value.ToString)
            End If
            currRow_MovieSet = dgvMoviesets.SelectedRows(0).Index
        Else
            currRow_MovieSet = -3
        End If
    End Sub

    Private Sub DataGridView_SelectionChanged_TVEpisodes(sender As Object, e As EventArgs) Handles dgvTVEpisodes.SelectionChanged
        If dgvTVEpisodes.SelectedRows.Count > 0 Then
            If dgvTVEpisodes.SelectedRows.Count > 1 Then
                SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvTVEpisodes.SelectedRows.Count))
            ElseIf dgvTVEpisodes.SelectedRows.Count = 1 Then
                SetStatus(dgvTVEpisodes.SelectedRows(0).Cells("path").Value.ToString)
            End If
            currRow_TVEpisode = dgvTVEpisodes.SelectedRows(0).Index
            If Not currList = 2 Then
                currList = 2
                prevRow_TVEpisode = -1
                DataGridView_SelectRow_TVEpisode(dgvTVEpisodes.SelectedRows(0).Index)
            End If
        Else
            currRow_TVEpisode = -3
        End If
    End Sub

    Private Sub DataGridView_SelectionChanged_TVSeason(sender As Object, e As EventArgs) Handles dgvTVSeasons.SelectionChanged
        If dgvTVSeasons.SelectedRows.Count > 0 Then
            If dgvTVSeasons.SelectedRows.Count > 1 Then
                SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvTVSeasons.SelectedRows.Count))
            ElseIf dgvTVSeasons.SelectedRows.Count = 1 Then
                SetStatus(dgvTVSeasons.SelectedRows(0).Cells("title").Value.ToString)
            End If
            currRow_TVSeason = dgvTVSeasons.SelectedRows(0).Index
            If Not currList = 1 Then
                currList = 1
                prevRow_TVSeason = -1
                DataGridView_SelectRow_TVSeason(dgvTVSeasons.SelectedRows(0).Index)
            End If
        Else
            currRow_TVSeason = -3
        End If
    End Sub

    Private Sub DataGridView_SelectionChanged_TVShow(sender As Object, e As EventArgs) Handles dgvTVShows.SelectionChanged
        If dgvTVShows.SelectedRows.Count > 0 Then
            If dgvTVShows.SelectedRows.Count > 1 Then
                SetStatus(String.Format(Master.eLang.GetString(627, "Selected Items: {0}"), dgvTVShows.SelectedRows.Count))
            ElseIf dgvTVShows.SelectedRows.Count = 1 Then
                SetStatus(dgvTVShows.SelectedRows(0).Cells("path").Value.ToString)
            End If
            currRow_TVShow = dgvTVShows.SelectedRows(0).Index
            If Not currList = 0 Then
                currList = 0
                prevRow_TVShow = -1
                DataGridView_SelectRow_TVShow(dgvTVShows.SelectedRows(0).Index)
            End If
        Else
            currRow_TVShow = -3
        End If
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the movie selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"><c>Integer</c> row which is currently selected</param>
    ''' <remarks></remarks>
    Private Sub DataGridView_SelectRow_Movie(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
        End While

        ClearInfo()

        If dgvMovies.Rows.Count > iRow Then
            If Not DataGridView_ColumnAnyInfoValue(dgvMovies, iRow) Then
                ShowNoInfo(True, Enums.ContentType.Movie)
                _CurrDBElement = Master.DB.Load_Movie(Convert.ToInt64(dgvMovies.Item(Database.Helpers.GetMainIdName(Database.TableName.movie), iRow).Value))
                FillScreenInfoWith_Movie()
            Else
                LoadInfo_Movie(Convert.ToInt64(dgvMovies.Item(Database.Helpers.GetMainIdName(Database.TableName.movie), iRow).Value))
            End If

            If Not bwMovieScraper.IsBusy AndAlso Not bwMoviesetScraper.IsBusy AndAlso Not bwReload_Movie.IsBusy AndAlso Not bwReload_Movieset.IsBusy AndAlso Not bwReload_TVShow.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                cmnuMovie.Enabled = True
            End If
        End If
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the movie selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"><c>Integer</c> row which is currently selected</param>
    ''' <remarks></remarks>
    Private Sub DataGridView_SelectRow_Movieset(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
        End While

        ClearInfo()
        If dgvMoviesets.Rows.Count > iRow Then
            If Not DataGridView_ColumnAnyInfoValue(dgvMoviesets, iRow) Then
                ShowNoInfo(True, Enums.ContentType.Movieset)
                _CurrDBElement = Master.DB.Load_Movieset(Convert.ToInt64(dgvMoviesets.Item(Database.Helpers.GetMainIdName(Database.TableName.movieset), iRow).Value))
                FillScreenInfoWith_Movieset()
            Else
                LoadInfo_Movieset(Convert.ToInt64(dgvMoviesets.Item(Database.Helpers.GetMainIdName(Database.TableName.movieset), iRow).Value))
            End If

            If Not bwMovieScraper.IsBusy AndAlso Not bwMoviesetScraper.IsBusy AndAlso
                Not bwReload_Movie.IsBusy AndAlso Not bwReload_Movieset.IsBusy AndAlso
                Not bwReload_TVShow.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                cmnuMovieSet.Enabled = True
            End If
        End If
    End Sub

    Private Sub DataGridView_SelectRow_TVEpisode(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
        End While

        ClearInfo()

        If dgvTVEpisodes.Rows.Count > iRow Then
            If Not Convert.ToInt64(dgvTVEpisodes.Item(Database.Helpers.GetMainIdName(Database.TableName.file), iRow).Value) = -1 AndAlso Not DataGridView_ColumnAnyInfoValue(dgvTVEpisodes, iRow) Then
                ShowNoInfo(True, Enums.ContentType.TVEpisode)
                _CurrDBElement = Master.DB.Load_TVEpisode(Convert.ToInt64(dgvTVEpisodes.Item(Database.Helpers.GetMainIdName(Database.TableName.episode), iRow).Value), True)
                FillScreenInfoWith_TVEpisode()
            Else
                LoadInfo_TVEpisode(Convert.ToInt64(dgvTVEpisodes.Item(Database.Helpers.GetMainIdName(Database.TableName.episode), iRow).Value))
            End If

            If Not Convert.ToInt64(dgvTVEpisodes.Item(Database.Helpers.GetMainIdName(Database.TableName.file), iRow).Value) = -1 AndAlso Not bwMovieScraper.IsBusy AndAlso Not bwMoviesetScraper.IsBusy AndAlso
                Not bwReload_Movie.IsBusy AndAlso Not bwReload_Movieset.IsBusy AndAlso Not bwReload_TVShow.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                cmnuEpisode.Enabled = True
            End If
        End If
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the TV Season selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView_SelectRow_TVSeason(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
        End While

        ClearInfo()

        If dgvTVSeasons.Rows.Count > iRow Then
            If Not Convert.ToBoolean(dgvTVSeasons.Item(Database.Helpers.GetColumnName(Database.ColumnName.IsMissing), iRow).Value) AndAlso Not DataGridView_ColumnAnyInfoValue(dgvTVSeasons, iRow) Then
                If Not currThemeType = Enums.ContentType.TVSeason Then Theme_Apply(Enums.ContentType.TVSeason)
                ShowNoInfo(True, Enums.ContentType.TVSeason)
                _CurrDBElement = Master.DB.Load_TVSeason(Convert.ToInt64(dgvTVSeasons.Item(Database.Helpers.GetMainIdName(Database.TableName.season), iRow).Value), True, False)
                DataGridView_Fill_TVEpisode(Convert.ToInt64(dgvTVSeasons.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow), iRow).Value), Convert.ToInt32(dgvTVSeasons.Item(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber), iRow).Value))
            Else
                LoadInfo_TVSeason(Convert.ToInt64(dgvTVSeasons.Item(Database.Helpers.GetMainIdName(Database.TableName.season), iRow).Value))
                DataGridView_Fill_TVEpisode(Convert.ToInt64(dgvTVSeasons.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow), iRow).Value), Convert.ToInt32(dgvTVSeasons.Item(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber), iRow).Value))
            End If

            If Not bwMovieScraper.IsBusy AndAlso Not bwMoviesetScraper.IsBusy AndAlso
                Not bwReload_Movie.IsBusy AndAlso Not bwReload_Movieset.IsBusy AndAlso
                Not bwReload_TVShow.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                cmnuSeason.Enabled = True
            End If
        End If
    End Sub
    ''' <summary>
    ''' Updates the media info panels (right side of disiplay) when the TV Show selector changes (left side of display)
    ''' </summary>
    ''' <param name="iRow"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView_SelectRow_TVShow(ByVal iRow As Integer)
        While tmrKeyBuffer.Enabled
            Application.DoEvents()
        End While

        ClearInfo()

        If dgvTVShows.Rows.Count > iRow Then
            If Not DataGridView_ColumnAnyInfoValue(dgvTVShows, iRow) Then
                ShowNoInfo(True, Enums.ContentType.TVShow)
                _CurrDBElement = Master.DB.Load_TVShow(Convert.ToInt64(dgvTVShows.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow), iRow).Value), False, False)
                DataGridView_Fill_TVSeason(Convert.ToInt64(dgvTVShows.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow), iRow).Value))
            Else
                LoadInfo_TVShow(Convert.ToInt64(dgvTVShows.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow), iRow).Value))
            End If

            If Not bwMovieScraper.IsBusy AndAlso Not bwMoviesetScraper.IsBusy AndAlso
                Not bwReload_Movie.IsBusy AndAlso Not bwReload_Movieset.IsBusy AndAlso
                Not bwReload_TVShow.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                cmnuShow.Enabled = True
            End If
        End If
    End Sub

    Private Function DataGridView_ShowColumn_Movie(ByVal columnName As String) As Boolean
        Dim lsColumn As GuiSettings.ListSorting = Manager.mSettings.Movie.GuiSettings.MediaListSorting.FirstOrDefault(Function(l) l.Column = columnName)
        Return If(lsColumn IsNot Nothing, lsColumn.Show, False)
    End Function

    Private Function DataGridView_ShowColumn_Movieset(ByVal columnName As String) As Boolean
        Dim lsColumn As GuiSettings.ListSorting = Manager.mSettings.Movieset.GuiSettings.MediaListSorting.FirstOrDefault(Function(l) l.Column = columnName)
        Return If(lsColumn IsNot Nothing, lsColumn.Show, False)
    End Function

    Private Function DataGridView_ShowColumn__TVEpisode(ByVal columnName As String) As Boolean
        Dim lsColumn As GuiSettings.ListSorting = Manager.mSettings.TVEpisode.GuiSettings.MediaListSorting.FirstOrDefault(Function(l) l.Column = columnName)
        Return If(lsColumn IsNot Nothing, lsColumn.Show, False)
    End Function

    Private Function DataGridView_ShowColumn_TVSeason(ByVal columnName As String) As Boolean
        Dim lsColumn As GuiSettings.ListSorting = Manager.mSettings.TVSeason.GuiSettings.MediaListSorting.FirstOrDefault(Function(l) l.Column = columnName)
        Return If(lsColumn IsNot Nothing, lsColumn.Show, False)
    End Function

    Private Function DataGridView_ShowColumn_TVShow(ByVal columnName As String) As Boolean
        Dim lsColumn As GuiSettings.ListSorting = Manager.mSettings.TVShow.GuiSettings.MediaListSorting.FirstOrDefault(Function(l) l.Column = columnName)
        Return If(lsColumn IsNot Nothing, lsColumn.Show, False)
    End Function

    Private Sub DataGridView_Sorted_Movie(ByVal sender As Object, ByVal e As EventArgs) Handles dgvMovies.Sorted
        prevRow_Movie = -1
        If dgvMovies.RowCount > 0 Then
            dgvMovies.CurrentCell = Nothing
            dgvMovies.ClearSelection()
            dgvMovies.Rows(0).Selected = True
            dgvMovies.CurrentCell = dgvMovies.Rows(0).Cells("listTitle")
        End If

        If dgvMovies.SortedColumn.HeaderCell.Value.ToString = "dateAdded" AndAlso dgvMovies.SortOrder = 1 Then
            btnFilterSortDateAdded_Movies.Tag = "ASC"
            btnFilterSortDateAdded_Movies.Image = My.Resources.asc
        ElseIf dgvMovies.SortedColumn.HeaderCell.Value.ToString = "dateAdded" AndAlso dgvMovies.SortOrder = 2 Then
            btnFilterSortDateAdded_Movies.Tag = "DESC"
            btnFilterSortDateAdded_Movies.Image = My.Resources.desc
        Else
            btnFilterSortDateAdded_Movies.Tag = String.Empty
            btnFilterSortDateAdded_Movies.Image = Nothing
        End If

        If dgvMovies.SortedColumn.HeaderCell.Value.ToString = "dateModified" AndAlso dgvMovies.SortOrder = 1 Then
            btnFilterSortDateModified_Movies.Tag = "ASC"
            btnFilterSortDateModified_Movies.Image = My.Resources.asc
        ElseIf dgvMovies.SortedColumn.HeaderCell.Value.ToString = "dateModified" AndAlso dgvMovies.SortOrder = 2 Then
            btnFilterSortDateModified_Movies.Tag = "DESC"
            btnFilterSortDateModified_Movies.Image = My.Resources.desc
        Else
            btnFilterSortDateModified_Movies.Tag = String.Empty
            btnFilterSortDateModified_Movies.Image = Nothing
        End If

        If dgvMovies.SortedColumn.HeaderCell.Value.ToString = "rating" AndAlso dgvMovies.SortOrder = 1 Then
            btnFilterSortRating_Movies.Tag = "ASC"
            btnFilterSortRating_Movies.Image = My.Resources.asc
        ElseIf dgvMovies.SortedColumn.HeaderCell.Value.ToString = "rating" AndAlso dgvMovies.SortOrder = 2 Then
            btnFilterSortRating_Movies.Tag = "DESC"
            btnFilterSortRating_Movies.Image = My.Resources.desc
        Else
            btnFilterSortRating_Movies.Tag = String.Empty
            btnFilterSortRating_Movies.Image = Nothing
        End If

        If dgvMovies.SortedColumn.HeaderCell.Value.ToString = "releaseDate" AndAlso dgvMovies.SortOrder = 1 Then
            btnFilterSortReleaseDate_Movies.Tag = "ASC"
            btnFilterSortReleaseDate_Movies.Image = My.Resources.asc
        ElseIf dgvMovies.SortedColumn.HeaderCell.Value.ToString = "releaseDate" AndAlso dgvMovies.SortOrder = 2 Then
            btnFilterSortReleaseDate_Movies.Tag = "DESC"
            btnFilterSortReleaseDate_Movies.Image = My.Resources.desc
        Else
            btnFilterSortReleaseDate_Movies.Tag = String.Empty
            btnFilterSortReleaseDate_Movies.Image = Nothing
        End If

        If dgvMovies.SortedColumn.HeaderCell.Value.ToString = "sortedTitle" AndAlso dgvMovies.SortOrder = 1 Then
            btnFilterSortTitle_Movies.Tag = "ASC"
            btnFilterSortTitle_Movies.Image = My.Resources.asc
        ElseIf dgvMovies.SortedColumn.HeaderCell.Value.ToString = "sortedTitle" AndAlso dgvMovies.SortOrder = 2 Then
            btnFilterSortTitle_Movies.Tag = "DESC"
            btnFilterSortTitle_Movies.Image = My.Resources.desc
        Else
            btnFilterSortTitle_Movies.Tag = String.Empty
            btnFilterSortTitle_Movies.Image = Nothing
        End If

        If dgvMovies.SortedColumn.HeaderCell.Value.ToString = "year" AndAlso dgvMovies.SortOrder = 1 Then
            btnFilterSortYear_Movies.Tag = "ASC"
            btnFilterSortYear_Movies.Image = My.Resources.asc
        ElseIf dgvMovies.SortedColumn.HeaderCell.Value.ToString = "year" AndAlso dgvMovies.SortOrder = 2 Then
            btnFilterSortYear_Movies.Tag = "DESC"
            btnFilterSortYear_Movies.Image = My.Resources.desc
        Else
            btnFilterSortYear_Movies.Tag = String.Empty
            btnFilterSortYear_Movies.Image = Nothing
        End If

        DataGridView_SortingSave_Movie()
    End Sub

    Private Sub DataGridView_Sorted_Movieset(ByVal sender As Object, ByVal e As EventArgs) Handles dgvMoviesets.Sorted
        prevRow_MovieSet = -1
        If dgvMoviesets.RowCount > 0 Then
            dgvMoviesets.CurrentCell = Nothing
            dgvMoviesets.ClearSelection()
            dgvMoviesets.Rows(0).Selected = True
            dgvMoviesets.CurrentCell = dgvMoviesets.Rows(0).Cells("listTitle")
        End If

        DataGridView_SortingSave_Movieset()
    End Sub

    Private Sub DataGridView_Sorted_TVEpisode(ByVal sender As Object, ByVal e As EventArgs) Handles dgvTVEpisodes.Sorted
        prevRow_TVEpisode = -1
        If dgvTVEpisodes.RowCount > 0 Then
            dgvTVEpisodes.CurrentCell = Nothing
            dgvTVEpisodes.ClearSelection()
            'dgvTVEpisodes.Rows(0).Selected = True
            'dgvTVEpisodes.CurrentCell = dgvTVEpisodes.Rows(0).Cells("Title")
        End If

        DataGridView_SortingSave_TVEpisode()
    End Sub

    Private Sub DataGridView_Sorted_TVSeason(ByVal sender As Object, ByVal e As EventArgs) Handles dgvTVSeasons.Sorted
        prevRow_TVSeason = -1
        If dgvTVSeasons.RowCount > 0 Then
            dgvTVSeasons.CurrentCell = Nothing
            dgvTVSeasons.ClearSelection()
            'dgvTVSeasons.Rows(0).Selected = True
            'dgvTVSeasons.CurrentCell = dgvTVSeasons.Rows(0).Cells("title")
        End If

        DataGridView_SortingSave_TVSeason()
    End Sub

    Private Sub DataGridView_Sorted_TVShow(ByVal sender As Object, ByVal e As EventArgs) Handles dgvTVShows.Sorted
        prevRow_TVShow = -1
        If dgvTVShows.RowCount > 0 Then
            dgvTVShows.CurrentCell = Nothing
            dgvTVShows.ClearSelection()
            dgvTVShows.Rows(0).Selected = True
            dgvTVShows.CurrentCell = dgvTVShows.Rows(0).Cells("listTitle")
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

        If dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "sortedTitle" AndAlso dgvTVShows.SortOrder = 1 Then
            btnFilterSortTitle_Shows.Tag = "ASC"
            btnFilterSortTitle_Shows.Image = My.Resources.asc
        ElseIf dgvTVShows.SortedColumn.HeaderCell.Value.ToString = "sortedTitle" AndAlso dgvTVShows.SortOrder = 2 Then
            btnFilterSortTitle_Shows.Tag = "DESC"
            btnFilterSortTitle_Shows.Image = My.Resources.desc
        Else
            btnFilterSortTitle_Shows.Tag = String.Empty
            btnFilterSortTitle_Shows.Image = Nothing
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

        DataGridView_SortingSave_TVShow()
    End Sub

    Private Sub DataGridView_SortingRestore_Movie()
        With Manager.mSettings.MainOptions.GuiSettings
            If .MainFilterSortColumn_Movie = 0 AndAlso .MainFilterSortOrder_Movie = 0 Then
                .MainFilterSortColumn_Movie = 4         'ListTitle in movielist
                .MainFilterSortOrder_Movie = 0          'ASC
            End If

            If dgvMovies.DataSource IsNot Nothing Then
                dgvMovies.Sort(dgvMovies.Columns(.MainFilterSortColumn_Movie), CType(.MainFilterSortOrder_Movie, ComponentModel.ListSortDirection))
            End If
        End With
    End Sub

    Private Sub DataGridView_SortingRestore_Movieset()
        With Manager.mSettings.MainOptions.GuiSettings
            If .MainFilterSortColumn_Movieset = 0 AndAlso .MainFilterSortOrder_Movie = 0 Then
                .MainFilterSortColumn_Movieset = 1         'ListTitle in movielist
                .MainFilterSortOrder_Movieset = 0          'ASC
            End If

            If dgvMoviesets.DataSource IsNot Nothing Then
                dgvMoviesets.Sort(dgvMoviesets.Columns(.MainFilterSortColumn_Movieset), CType(.MainFilterSortOrder_Movieset, ComponentModel.ListSortDirection))
            End If
        End With
    End Sub

    Private Sub DataGridView_SortingRestore_TVEpisode(ByVal bIsAllSeasons As Boolean)
        With Manager.mSettings.MainOptions.GuiSettings
            If .MainFilterSortColumn_TVEpisode = 0 AndAlso .MainFilterSortOrder_TVEpisode = 0 Then
                .MainFilterSortColumn_TVEpisode = 4         'Episode # in episodelist
                .MainFilterSortOrder_TVEpisode = 0          'ASC
            End If

            If dgvTVEpisodes.DataSource IsNot Nothing Then
                If bIsAllSeasons Then
                    dgvTVEpisodes.Sort(dgvTVEpisodes.Columns("Season"), CType(.MainFilterSortOrder_TVEpisode, ComponentModel.ListSortDirection))
                Else
                    dgvTVEpisodes.Sort(dgvTVEpisodes.Columns(.MainFilterSortColumn_TVEpisode), CType(.MainFilterSortOrder_TVEpisode, ComponentModel.ListSortDirection))
                End If
            End If
        End With
    End Sub

    Private Sub DataGridView_SortingRestore_TVSeason()
        With Manager.mSettings.MainOptions.GuiSettings
            If .MainFilterSortColumn_TVSeason = 0 AndAlso .MainFilterSortOrder_TVSeason = 0 Then
                .MainFilterSortColumn_TVSeason = 2         'Season # in seasonlist
                .MainFilterSortOrder_TVSeason = 0          'ASC
            End If

            If dgvTVSeasons.DataSource IsNot Nothing Then
                dgvTVSeasons.Sort(dgvTVSeasons.Columns(.MainFilterSortColumn_TVSeason), CType(.MainFilterSortOrder_TVSeason, ComponentModel.ListSortDirection))
            End If
        End With
    End Sub

    Private Sub DataGridView_SortingRestore_TVShow()
        With Manager.mSettings.MainOptions.GuiSettings
            If .MainFilterSortColumn_TVShow = 0 AndAlso .MainFilterSortOrder_TVShow = 0 Then
                .MainFilterSortColumn_TVShow = 2        'ListTitle in tvshowlist
                .MainFilterSortOrder_TVShow = 0          'ASC
            End If

            If dgvTVShows.DataSource IsNot Nothing Then
                dgvTVShows.Sort(dgvTVShows.Columns(.MainFilterSortColumn_TVShow), CType(.MainFilterSortOrder_TVShow, ComponentModel.ListSortDirection))
            End If
        End With
    End Sub

    Private Sub DataGridView_SortingSave_Movie()
        Dim Order As Integer
        If dgvMovies.SortOrder = SortOrder.None Then Order = 0 'ComponentModel.ListSortDirection has only ASC and DESC. So set [None] to ASC
        If dgvMovies.SortOrder = SortOrder.Ascending Then Order = 0
        If dgvMovies.SortOrder = SortOrder.Descending Then Order = 1


        Manager.mSettings.MainOptions.GuiSettings.MainFilterSortColumn_Movie = dgvMovies.SortedColumn.Index
        Manager.mSettings.MainOptions.GuiSettings.MainFilterSortOrder_Movie = Order
    End Sub

    Private Sub DataGridView_SortingSave_Movieset()
        Dim Order As Integer
        If dgvMoviesets.SortOrder = SortOrder.None Then Order = 0 'ComponentModel.ListSortDirection has only ASC and DESC. So set [None] to ASC
        If dgvMoviesets.SortOrder = SortOrder.Ascending Then Order = 0
        If dgvMoviesets.SortOrder = SortOrder.Descending Then Order = 1

        Manager.mSettings.MainOptions.GuiSettings.MainFilterSortColumn_Movieset = dgvMoviesets.SortedColumn.Index
        Manager.mSettings.MainOptions.GuiSettings.MainFilterSortOrder_Movieset = Order
    End Sub

    Private Sub DataGridView_SortingSave_TVEpisode()
        Dim Order As Integer
        If dgvTVEpisodes.SortOrder = SortOrder.None Then Order = 0 'ComponentModel.ListSortDirection has only ASC and DESC. So set [None] to ASC
        If dgvTVEpisodes.SortOrder = SortOrder.Ascending Then Order = 0
        If dgvTVEpisodes.SortOrder = SortOrder.Descending Then Order = 1

        Manager.mSettings.MainOptions.GuiSettings.MainFilterSortColumn_TVEpisode = dgvTVEpisodes.SortedColumn.Index
        Manager.mSettings.MainOptions.GuiSettings.MainFilterSortOrder_TVEpisode = Order
    End Sub

    Private Sub DataGridView_SortingSave_TVSeason()
        Dim Order As Integer
        If dgvTVSeasons.SortOrder = SortOrder.None Then Order = 0 'ComponentModel.ListSortDirection has only ASC and DESC. So set [None] to ASC
        If dgvTVSeasons.SortOrder = SortOrder.Ascending Then Order = 0
        If dgvTVSeasons.SortOrder = SortOrder.Descending Then Order = 1

        Manager.mSettings.MainOptions.GuiSettings.MainFilterSortColumn_TVSeason = dgvTVSeasons.SortedColumn.Index
        Manager.mSettings.MainOptions.GuiSettings.MainFilterSortOrder_TVSeason = Order
    End Sub

    Private Sub DataGridView_SortingSave_TVShow()
        Dim Order As Integer
        If dgvTVShows.SortOrder = SortOrder.None Then Order = 0 'ComponentModel.ListSortDirection has only ASC and DESC. So set [None] to ASC
        If dgvTVShows.SortOrder = SortOrder.Ascending Then Order = 0
        If dgvTVShows.SortOrder = SortOrder.Descending Then Order = 1

        Manager.mSettings.MainOptions.GuiSettings.MainFilterSortColumn_TVShow = dgvTVShows.SortedColumn.Index
        Manager.mSettings.MainOptions.GuiSettings.MainFilterSortOrder_TVShow = Order
    End Sub

    Private Sub DataTable_AddRow(ByVal dTable As DataTable, ByVal dRow As DataRow)
        dTable.Rows.Add(dRow)
    End Sub

    Private Sub DataTable_RemoveRow(ByVal dTable As DataTable, ByVal dRow As DataRow)
        dTable.Rows.Remove(dRow)
    End Sub

    Private Sub DataTable_UpdateRow(ByVal dRow As DataRow, ByVal newRow As DataRow)
        dRow.ItemArray = newRow.ItemArray
    End Sub

    Private Sub Edit_Movie(ByRef dbelement As Database.DBElement, Optional ByVal EventType As Enums.AddonEventType = Enums.AddonEventType.AfterEdit_Movie)
        SetControlsEnabled(False)
        If dbelement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(dbelement, True) Then
            Using dEditMovie As New dlgEdit_Movie
                AddonsManager.Instance.RunGeneric(Enums.AddonEventType.BeforeEdit_Movie, Nothing, Nothing, False, dbelement)
                Select Case dEditMovie.ShowDialog(dbelement)
                    Case DialogResult.OK
                        dbelement = dEditMovie.Result
                        AddonsManager.Instance.RunGeneric(EventType, Nothing, Nothing, False, dbelement)
                        tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                        Master.DB.Save_Movie(dbelement, False, True, True, True, False)
                        DataGridView_RefreshRow_Movie(dbelement.ID)
                    Case DialogResult.Retry 'Rescrape
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
                        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                                               .ContentType = dbelement.ContentType,
                                               .ListOfID = New List(Of Long) From {dbelement.ID},
                                               .ScrapeModifiers = ScrapeModifiers,
                                               .ScrapeOptions = Master.eSettings.DefaultScrapeOptions(dbelement.ContentType),
                                               .ScrapeType = Enums.ScrapeType.Manually,
                                               .SelectionType = Enums.SelectionType.None
                                               })
                    Case DialogResult.Abort 'Change Movie
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.DoSearch, True)
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
                        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                                               .ContentType = dbelement.ContentType,
                                               .ListOfID = New List(Of Long) From {dbelement.ID},
                                               .ScrapeModifiers = ScrapeModifiers,
                                               .ScrapeOptions = Master.eSettings.DefaultScrapeOptions(dbelement.ContentType),
                                               .ScrapeType = Enums.ScrapeType.Manually,
                                               .SelectionType = Enums.SelectionType.None
                                               })
                    Case Else
                        If InfoCleared Then LoadInfo_Movie(dbelement.ID)
                End Select
            End Using
        End If
        SetControlsEnabled(True)
    End Sub

    Private Sub Edit_Movieset(ByRef dbelement As Database.DBElement)
        SetControlsEnabled(False)
        If dbelement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(dbelement, True) Then
            Using dEditMovieSet As New dlgEdit_Movieset
                AddonsManager.Instance.RunGeneric(Enums.AddonEventType.BeforeEdit_Movieset, Nothing, Nothing, False, dbelement)
                Select Case dEditMovieSet.ShowDialog(dbelement)
                    Case DialogResult.OK
                        dbelement = dEditMovieSet.Result
                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.AfterEdit_Movieset, Nothing, Nothing, False, dbelement)
                        tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                        Master.DB.Save_Movieset(dbelement, False, True, True, True)
                        DataGridView_RefreshRow_Movieset(dbelement.ID)
                    Case DialogResult.Retry 'Rescrape
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
                        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                                               .ContentType = dbelement.ContentType,
                                               .ListOfID = New List(Of Long) From {dbelement.ID},
                                               .ScrapeModifiers = ScrapeModifiers,
                                               .ScrapeOptions = Master.eSettings.DefaultScrapeOptions(dbelement.ContentType),
                                               .ScrapeType = Enums.ScrapeType.Manually,
                                               .SelectionType = Enums.SelectionType.None
                                               })
                    Case DialogResult.Abort 'Change MovieSet
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.DoSearch, True)
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
                        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                                               .ContentType = dbelement.ContentType,
                                               .ListOfID = New List(Of Long) From {dbelement.ID},
                                               .ScrapeModifiers = ScrapeModifiers,
                                               .ScrapeOptions = Master.eSettings.DefaultScrapeOptions(dbelement.ContentType),
                                               .ScrapeType = Enums.ScrapeType.Manually,
                                               .SelectionType = Enums.SelectionType.None
                                               })
                    Case Else
                        If InfoCleared Then LoadInfo_Movieset(dbelement.ID)
                End Select
            End Using
        End If
        SetControlsEnabled(True)
    End Sub

    Private Sub Edit_TVEpisode(ByRef dbelement As Database.DBElement, Optional ByVal EventType As Enums.AddonEventType = Enums.AddonEventType.AfterEdit_TVEpisode)
        SetControlsEnabled(False)
        If dbelement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(dbelement, True) Then
            Using dEditTVEpisode As New dlgEdit_TVEpisode
                AddonsManager.Instance.RunGeneric(Enums.AddonEventType.BeforeEdit_TVEpisode, Nothing, Nothing, False, dbelement)
                Select Case dEditTVEpisode.ShowDialog(dbelement)
                    Case DialogResult.OK
                        dbelement = dEditTVEpisode.Result
                        AddonsManager.Instance.RunGeneric(EventType, Nothing, Nothing, False, dbelement)
                        tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                        Master.DB.Save_TVEpisode(dbelement, False, True, True, True, True)
                        DataGridView_RefreshRow_TVEpisode(dbelement.ID)
                    Case DialogResult.Retry 'Rescrape
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
                        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                                               .ContentType = dbelement.ContentType,
                                               .ListOfID = New List(Of Long) From {dbelement.ID},
                                               .ScrapeModifiers = ScrapeModifiers,
                                               .ScrapeOptions = Master.eSettings.DefaultScrapeOptions(dbelement.ContentType),
                                               .ScrapeType = Enums.ScrapeType.Manually,
                                               .SelectionType = Enums.SelectionType.None
                                               })
                    Case DialogResult.Abort 'Change TVEpisode
                        'TODO
                    Case Else
                        If InfoCleared Then LoadInfo_TVEpisode(dbelement.ID)
                End Select
            End Using
        End If
        SetControlsEnabled(True)
    End Sub

    Private Sub Edit_TVSeason(ByRef dbelement As Database.DBElement, Optional ByVal EventType As Enums.AddonEventType = Enums.AddonEventType.AfterEdit_TVSeason)
        SetControlsEnabled(False)
        If dbelement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(dbelement, True) Then
            Using dEditTVSeason As New dlgEdit_TVSeason
                AddonsManager.Instance.RunGeneric(Enums.AddonEventType.BeforeEdit_TVSeason, Nothing, Nothing, False, dbelement)
                Select Case dEditTVSeason.ShowDialog(dbelement)
                    Case DialogResult.OK
                        dbelement = dEditTVSeason.Result
                        AddonsManager.Instance.RunGeneric(EventType, Nothing, Nothing, False, dbelement)
                        tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                        Master.DB.Save_TVSeason(dbelement, False, True, True)
                        DataGridView_RefreshRow_TVSeason(dbelement.ID)
                    Case Else
                        'If Me.InfoCleared Then Me.LoadInfo_TVSeason(CInt(DBTVSeason.ID)) 'TODO: 
                End Select
            End Using
        End If
        SetControlsEnabled(True)
    End Sub

    Private Sub Edit_TVShow(ByRef dbelement As Database.DBElement, Optional ByVal type As Enums.AddonEventType = Enums.AddonEventType.AfterEdit_TVShow)
        SetControlsEnabled(False)
        If dbelement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(dbelement, True) Then
            Using dEditTVShow As New dlgEdit_TVShow
                AddonsManager.Instance.RunGeneric(Enums.AddonEventType.BeforeEdit_TVShow, Nothing, Nothing, False, dbelement)
                Select Case dEditTVShow.ShowDialog(dbelement)
                    Case DialogResult.OK
                        dbelement = dEditTVShow.Result
                        AddonsManager.Instance.RunGeneric(type, Nothing, Nothing, False, dbelement)
                        tslLoading.Text = String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":")
                        Master.DB.Save_TVShow(dbelement, False, True, True, True)
                        DataGridView_RefreshRow_TVShow(dbelement.ID)
                    Case DialogResult.Retry 'Rescrape
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
                        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                                               .ContentType = dbelement.ContentType,
                                               .ListOfID = New List(Of Long) From {dbelement.ID},
                                               .ScrapeModifiers = ScrapeModifiers,
                                               .ScrapeOptions = Master.eSettings.DefaultScrapeOptions(dbelement.ContentType),
                                               .ScrapeType = Enums.ScrapeType.Manually,
                                               .SelectionType = Enums.SelectionType.None
                                               })
                    Case DialogResult.Abort 'Change TVShow
                        Dim ScrapeModifiers As New Structures.ScrapeModifiers
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.DoSearch, True)
                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
                        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                                               .ContentType = dbelement.ContentType,
                                               .ListOfID = New List(Of Long) From {dbelement.ID},
                                               .ScrapeModifiers = ScrapeModifiers,
                                               .ScrapeOptions = Master.eSettings.DefaultScrapeOptions(dbelement.ContentType),
                                               .ScrapeType = Enums.ScrapeType.Manually,
                                               .SelectionType = Enums.SelectionType.None
                                               })
                    Case Else
                        If InfoCleared Then LoadInfo_TVShow(dbelement.ID)
                End Select
            End Using
        End If
        SetControlsEnabled(True)
    End Sub

    Private Sub Filter_Add_Boolean(ByVal columnName As Database.ColumnName, ByVal value As Boolean, ByVal contentType As Enums.ContentType)
        If value Then
            Dim rule As New SmartFilter.Rule With {.Field = columnName, .Operator = If(value, SmartFilter.Operators.True, SmartFilter.Operators.False)}
            Select Case contentType
                Case Enums.ContentType.Movie
                    Filter_Movie.Rules.Add(rule)
                Case Enums.ContentType.Movieset
                    Filter_Movieset.Rules.Add(rule)
                Case Enums.ContentType.TVShow
                    Filter_TVShows.Rules.Add(rule)
            End Select
        Else
            Select Case contentType
                Case Enums.ContentType.Movie
                    Filter_Movie.RemoveAll(columnName)
                Case Enums.ContentType.Movieset
                    Filter_Movieset.RemoveAll(columnName)
                Case Enums.ContentType.TVShow
                    Filter_TVShows.RemoveAll(columnName)
            End Select
        End If
        Select Case contentType
            Case Enums.ContentType.Movie
                DataGridView_ApplyFilter_Movie()
            Case Enums.ContentType.Movieset
                DataGridView_ApplyFilter_Movieset()
            Case Enums.ContentType.TVShow
                DataGridView_ApplyFilter_TVShow()
        End Select
    End Sub

    Private Sub Filter_Add_List(ByRef checkedListBox As CheckedListBox,
                                ByRef filterPanel As Panel,
                                ByRef textBox As TextBox,
                                ByVal columnName As Database.ColumnName,
                                ByVal contentType As Enums.ContentType)

        Dim filter As New List(Of SmartFilter.Rule)

        filterPanel.Visible = False
        filterPanel.Tag = "NO"

        If checkedListBox.CheckedItems.Count > 0 Then
            textBox.Text = String.Empty

            Dim lstItems As New List(Of String)
            lstItems.AddRange(checkedListBox.CheckedItems.OfType(Of String).ToList)

            textBox.Text = String.Join(" | ", lstItems.ToArray)

            For i As Integer = 0 To lstItems.Count - 1
                Dim rule As New SmartFilter.Rule
                rule.Field = columnName
                If lstItems.Item(i) = Master.eLang.None Then
                    rule.Operator = SmartFilter.Operators.IsNullOrEmpty
                Else
                    rule.Operator = SmartFilter.Operators.Contains
                    rule.Value = lstItems.Item(i)
                End If
                filter.Add(rule)
            Next

            Select Case contentType
                Case Enums.ContentType.Movie
                    Filter_Movie.RemoveAll(columnName)
                    Filter_Movie.Rules.AddRange(filter)
                    DataGridView_ApplyFilter_Movie()
                Case Enums.ContentType.TVShow
                    Filter_TVShows.RemoveAll(columnName)
                    Filter_TVShows.Rules.AddRange(filter)
                    DataGridView_ApplyFilter_TVShow()
            End Select
        Else
            Select Case contentType
                Case Enums.ContentType.Movie
                    If Filter_Movie.Contains(columnName) Then
                        textBox.Text = String.Empty
                        Filter_Movie.RemoveAll(columnName)
                        DataGridView_ApplyFilter_Movie()
                    End If
                Case Enums.ContentType.TVShow
                    If Filter_TVShows.Contains(columnName) Then
                        textBox.Text = String.Empty
                        Filter_TVShows.RemoveAll(columnName)
                        DataGridView_ApplyFilter_TVShow()
                    End If
            End Select
        End If
    End Sub

    Private Sub Filter_Add_GreaterThan(ByVal columnName As Database.ColumnName, ByVal value As Integer, ByVal contentType As Enums.ContentType)
        If value > -1 Then
            Dim rule As New SmartFilter.Rule With {.Field = columnName, .Operator = SmartFilter.Operators.GreaterThan, .Value = value}
            Select Case contentType
                Case Enums.ContentType.Movie
                    Filter_Movie.Rules.Add(rule)
                Case Enums.ContentType.Movieset
                    Filter_Movieset.Rules.Add(rule)
                Case Enums.ContentType.TVShow
                    Filter_TVShows.Rules.Add(rule)
            End Select
        Else
            Select Case contentType
                Case Enums.ContentType.Movie
                    Filter_Movie.RemoveAll(columnName)
                Case Enums.ContentType.Movieset
                    Filter_Movieset.RemoveAll(columnName)
                Case Enums.ContentType.TVShow
                    Filter_TVShows.RemoveAll(columnName)
            End Select
        End If
        Select Case contentType
            Case Enums.ContentType.Movie
                DataGridView_ApplyFilter_Movie()
            Case Enums.ContentType.Movieset
                DataGridView_ApplyFilter_Movieset()
            Case Enums.ContentType.TVShow
                DataGridView_ApplyFilter_TVShow()
        End Select
    End Sub

    Private Sub Filter_Add_Missing_Movie()
        Filter_Movie.RemoveAllMissingFilters()
        If chkFilterMissing_Movies.Checked Then
            With Master.eSettings
                If .MovieMissingBanner Then Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.BannerPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieMissingClearArt Then Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.ClearArtPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieMissingClearLogo Then Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.ClearLogoPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieMissingDiscArt Then Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.DiscArtPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieMissingExtrafanarts Then Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.ExtrafanartsPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieMissingExtrathumbs Then Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.ExtrathumbsPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieMissingFanart Then Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.FanartPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieMissingKeyArt Then Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.KeyArtPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieMissingLandscape Then Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.LandscapePath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieMissingNFO Then Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.NfoPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieMissingPoster Then Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.PosterPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieMissingSubtitles Then Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.HasSubtitles, .Operator = SmartFilter.Operators.False})
                If .MovieMissingTheme Then Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.ThemePath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieMissingTrailer Then Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.TrailerPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
            End With
        End If
        DataGridView_ApplyFilter_Movie()
    End Sub

    Private Sub Filter_Add_Missing_Movieset()
        Filter_Movieset.RemoveAllMissingFilters()
        If chkFilterMissing_MovieSets.Checked Then
            With Master.eSettings
                If .MovieSetMissingBanner Then Filter_Movieset.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.BannerPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieSetMissingClearArt Then Filter_Movieset.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.ClearArtPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieSetMissingClearLogo Then Filter_Movieset.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.ClearLogoPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieSetMissingDiscArt Then Filter_Movieset.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.DiscArtPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieSetMissingFanart Then Filter_Movieset.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.FanartPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieSetMissingKeyArt Then Filter_Movieset.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.KeyArtPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieSetMissingLandscape Then Filter_Movieset.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.LandscapePath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieSetMissingNFO Then Filter_Movieset.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.NfoPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .MovieSetMissingPoster Then Filter_Movieset.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.PosterPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
            End With
        End If
        DataGridView_ApplyFilter_Movieset()
    End Sub

    Private Sub Filter_Add_Missing_TVShow()
        Filter_TVShows.RemoveAllMissingFilters()
        If chkFilterMissing_Shows.Checked Then
            With Master.eSettings
                If .TVShowMissingBanner Then Filter_TVShows.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.BannerPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .TVShowMissingCharacterArt Then Filter_TVShows.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.CharacterArtPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .TVShowMissingClearArt Then Filter_TVShows.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.ClearArtPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .TVShowMissingClearLogo Then Filter_TVShows.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.ClearLogoPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .TVShowMissingExtrafanarts Then Filter_TVShows.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.ExtrafanartsPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .TVShowMissingFanart Then Filter_TVShows.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.FanartPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .TVShowMissingKeyArt Then Filter_TVShows.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.KeyArtPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .TVShowMissingLandscape Then Filter_TVShows.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.LandscapePath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .TVShowMissingNFO Then Filter_TVShows.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.NfoPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .TVShowMissingPoster Then Filter_TVShows.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.PosterPath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
                If .TVShowMissingTheme Then Filter_TVShows.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.ThemePath, .Operator = SmartFilter.Operators.IsNullOrEmpty})
            End With
        End If
        DataGridView_ApplyFilter_TVShow()
    End Sub

    Private Sub Filter_Add_SearchBar(ByRef searchBar As AdvancedControls.TextBox_with_Watermark,
                                     ByRef comboBox As ComboBox,
                                     ByVal contentType As Enums.ContentType)

        Dim filter As New List(Of SmartFilter.Rule)
        Dim rule As New SmartFilter.Rule With {
            .Operator = SmartFilter.Operators.Contains,
            .Value = searchBar.Text}

        If Not String.IsNullOrEmpty(searchBar.Text) Then
            Select Case comboBox.Text
                Case Master.eLang.GetString(100, "Actor")
                    rule.Field = Database.ColumnName.ActorName
                Case Master.eLang.GetString(301, "Country")
                    rule.Field = Database.ColumnName.Countries
                Case Master.eLang.GetString(798, "Creator")
                    rule.Field = Database.ColumnName.Creators
                Case Master.eLang.GetString(729, "Credits")
                    rule.Field = Database.ColumnName.Credits
                Case Master.eLang.GetString(62, "Director")
                    rule.Field = Database.ColumnName.Directors
                Case String.Format("{0} ({1})", Master.eLang.GetString(21, "Title"), Master.eLang.GetString(1379, "Movie"))
                    rule.Field = Database.ColumnName.MovieTitles
                Case Master.eLang.GetString(302, "Original Title")
                    rule.Field = Database.ColumnName.OriginalTitle
                Case Master.eLang.GetString(233, "Role")
                    rule.Field = Database.ColumnName.Role
                Case Master.eLang.GetString(395, "Studio")
                    rule.Field = Database.ColumnName.Studios
                Case Master.eLang.GetString(21, "Title")
                    rule.Field = Database.ColumnName.Title
            End Select

            filter.Add(rule)

            Select Case contentType
                Case Enums.ContentType.Movie
                    Filter_Movie.RemoveAllSearchbarFilters()
                    Filter_Movie.Rules.AddRange(filter)
                    DataGridView_ApplyFilter_Movie()
                Case Enums.ContentType.Movieset
                    Filter_Movieset.RemoveAllSearchbarFilters()
                    Filter_Movieset.Rules.AddRange(filter)
                    DataGridView_ApplyFilter_Movieset()
                Case Enums.ContentType.TVShow
                    Filter_TVShows.RemoveAllSearchbarFilters()
                    Filter_TVShows.Rules.AddRange(filter)
                    DataGridView_ApplyFilter_TVShow()
            End Select
        Else
            Select Case contentType
                Case Enums.ContentType.Movie
                    If Filter_Movie.ContainsAnyFromSearchBar Then
                        searchBar.Text = String.Empty
                        Filter_Movie.RemoveAllSearchbarFilters()
                        DataGridView_ApplyFilter_Movie()
                    End If
                Case Enums.ContentType.Movieset
                    If Filter_Movieset.ContainsAnyFromSearchBar Then
                        searchBar.Text = String.Empty
                        Filter_Movieset.RemoveAllSearchbarFilters()
                        DataGridView_ApplyFilter_Movieset()
                    End If
                Case Enums.ContentType.TVShow
                    If Filter_TVShows.ContainsAnyFromSearchBar Then
                        searchBar.Text = String.Empty
                        Filter_TVShows.RemoveAllSearchbarFilters()
                        DataGridView_ApplyFilter_TVShow()
                    End If
            End Select
        End If
    End Sub

    Private Sub Filter_Add_Source(ByRef checkedListBox As CheckedListBox,
                                  ByRef filterPanel As Panel,
                                  ByRef textBox As TextBox,
                                  ByVal contentType As Enums.ContentType)

        Dim filter As New SmartFilter.RuleWithOperator With {.InnerCondition = SmartFilter.Conditions.Any}

        filterPanel.Visible = False
        filterPanel.Tag = "NO"

        If checkedListBox.CheckedItems.Count > 0 Then
            textBox.Text = String.Empty

            Dim lstItems As New List(Of String)
            lstItems.AddRange(checkedListBox.CheckedItems.OfType(Of String).ToList)

            textBox.Text = String.Join(" | ", lstItems.ToArray)

            For i As Integer = 0 To lstItems.Count - 1
                Dim rule As New SmartFilter.Rule With {
                    .Field = Database.ColumnName.SourceName,
                    .Operator = SmartFilter.Operators.Is,
                    .Value = lstItems.Item(i)
                }
                filter.Rules.Add(rule)
            Next

            Select Case contentType
                Case Enums.ContentType.Movie
                    Filter_Movie.RemoveAll(Database.ColumnName.SourceName)
                    Filter_Movie.RulesWithOperator.Add(filter)
                    DataGridView_ApplyFilter_Movie()
                Case Enums.ContentType.TVShow
                    Filter_TVShows.RemoveAll(Database.ColumnName.SourceName)
                    Filter_TVShows.RulesWithOperator.Add(filter)
                    DataGridView_ApplyFilter_TVShow()
            End Select
        Else
            Select Case contentType
                Case Enums.ContentType.Movie
                    If Filter_Movie.Contains(Database.ColumnName.SourceName) Then
                        textBox.Text = String.Empty
                        Filter_Movie.RemoveAll(Database.ColumnName.SourceName)
                        DataGridView_ApplyFilter_Movie()
                    End If
                Case Enums.ContentType.TVShow
                    If Filter_TVShows.Contains(Database.ColumnName.SourceName) Then
                        textBox.Text = String.Empty
                        Filter_TVShows.RemoveAll(Database.ColumnName.SourceName)
                        DataGridView_ApplyFilter_TVShow()
                    End If
            End Select
        End If
    End Sub

    Private Sub Filter_Add_Year(ByRef fromYear As ComboBox,
                                ByRef fromYearMod As ComboBox,
                                ByRef toYear As ComboBox,
                                ByRef toYearMod As ComboBox,
                                ByVal contentType As Enums.ContentType)

        If Not String.IsNullOrEmpty(fromYear.Text) AndAlso Not fromYear.Text = Master.eLang.All Then
            Dim filter As New SmartFilter.RuleWithOperator With {.InnerCondition = SmartFilter.Conditions.All}
            filter.Rules.Add(New SmartFilter.Rule With {
                                         .Field = Database.ColumnName.Year,
                                         .Operator = SmartFilter.Filter.ConvertStringToOperator(fromYearMod.Text),
                                         .Value = fromYear.Text
                                         })

            Select Case fromYearMod.Text
                Case ">", ">="
                    toYearMod.Enabled = True
                    toYear.Enabled = True

                    If Not String.IsNullOrEmpty(toYear.Text) AndAlso Not toYear.Text = Master.eLang.All Then
                        filter.Rules.Add(New SmartFilter.Rule With {
                                         .Field = Database.ColumnName.Year,
                                         .Operator = SmartFilter.Filter.ConvertStringToOperator(toYearMod.Text),
                                         .Value = toYear.Text
                                         })
                    End If
                Case Else
                    toYearMod.Enabled = False
                    toYear.Enabled = False
            End Select


            Select Case contentType
                Case Enums.ContentType.Movie
                    Filter_Movie.RemoveAll(Database.ColumnName.Year)
                    Filter_Movie.RulesWithOperator.Add(filter)
                    DataGridView_ApplyFilter_Movie()
                Case Enums.ContentType.TVShow
                    Filter_TVShows.RemoveAll(Database.ColumnName.Year)
                    Filter_TVShows.RulesWithOperator.Add(filter)
                    DataGridView_ApplyFilter_TVShow()
            End Select
        Else
            toYearMod.Enabled = False
            toYear.Enabled = False

            Select Case contentType
                Case Enums.ContentType.Movie
                    If Filter_Movie.Contains(Database.ColumnName.Year) Then
                        Filter_Movie.RemoveAll(Database.ColumnName.Year)
                        DataGridView_ApplyFilter_Movie()
                    End If
                Case Enums.ContentType.TVShow
                    If Filter_TVShows.Contains(Database.ColumnName.Year) Then
                        Filter_TVShows.RemoveAll(Database.ColumnName.Year)
                        DataGridView_ApplyFilter_TVShow()
                    End If
            End Select
        End If
    End Sub

    Private Sub Filter_Add_Year_Movie(ByVal sender As Object, ByVal e As EventArgs) Handles _
        cbFilterYearModFrom_Movies.SelectedIndexChanged,
        cbFilterYearModTo_Movies.SelectedIndexChanged,
        cbFilterYearFrom_Movies.SelectedIndexChanged,
        cbFilterYearTo_Movies.SelectedIndexChanged

        Filter_Add_Year(cbFilterYearFrom_Movies, cbFilterYearModFrom_Movies, cbFilterYearTo_Movies, cbFilterYearModTo_Movies, Enums.ContentType.Movie)
    End Sub

    Private Sub Filter_RefreshList_Country_Movie()
        clbFilterCountries_Movies.Items.Clear()
        Dim mCountry() As Object = Master.DB.GetAllCountries
        clbFilterCountries_Movies.Items.Add(Master.eLang.None)
        clbFilterCountries_Movies.Items.AddRange(mCountry)

        If Filter_Movie.Contains(Database.ColumnName.Countries, SmartFilter.Operators.IsNullOrEmpty) Then
            clbFilterCountries_Movies.SetItemChecked(0, True)
        Else
            Dim rCountrys = Filter_Movie.Rules.Where(Function(f) f.Field = Database.ColumnName.Countries)
            If rCountrys.Count > 0 Then
                For i As Integer = 0 To rCountrys.Count - 1
                    If clbFilterCountries_Movies.FindString(rCountrys(i).Value.ToString) > 0 Then
                        clbFilterCountries_Movies.SetItemChecked(clbFilterCountries_Movies.FindString(rCountrys(i).Value.ToString), True)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub Filter_RefreshList_Genre_Movie()
        clbFilterGenres_Movies.Items.Clear()
        Dim mGenre() As Object = APIXML.GetGenreList
        clbFilterGenres_Movies.Items.Add(Master.eLang.None)
        clbFilterGenres_Movies.Items.AddRange(mGenre)

        If Filter_Movie.Contains(Database.ColumnName.Genres, SmartFilter.Operators.IsNullOrEmpty) Then
            clbFilterGenres_Movies.SetItemChecked(0, True)
        Else
            Dim rGenres = Filter_Movie.Rules.Where(Function(f) f.Field = Database.ColumnName.Genres)
            If rGenres.Count > 0 Then
                For i As Integer = 0 To rGenres.Count - 1
                    If clbFilterGenres_Movies.FindString(rGenres(i).Value.ToString) > 0 Then
                        clbFilterGenres_Movies.SetItemChecked(clbFilterGenres_Movies.FindString(rGenres(i).Value.ToString), True)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub Filter_RefreshList_Genre_TVShow()
        clbFilterGenres_Shows.Items.Clear()
        Dim mGenre() As Object = APIXML.GetGenreList
        clbFilterGenres_Shows.Items.Add(Master.eLang.None)
        clbFilterGenres_Shows.Items.AddRange(mGenre)

        If Filter_TVShows.Contains(Database.ColumnName.Genres, SmartFilter.Operators.IsNullOrEmpty) Then
            clbFilterGenres_Shows.SetItemChecked(0, True)
        Else
            Dim rGenres = Filter_TVShows.Rules.Where(Function(f) f.Field = Database.ColumnName.Genres)
            If rGenres.Count > 0 Then
                For i As Integer = 0 To rGenres.Count - 1
                    If clbFilterGenres_Shows.FindString(rGenres(i).Value.ToString) > 0 Then
                        clbFilterGenres_Shows.SetItemChecked(clbFilterGenres_Shows.FindString(rGenres(i).Value.ToString), True)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub Filter_RefreshList_Tag_Movie()
        clbFilterTags_Movies.Items.Clear()
        Dim mTag() As Object = Master.DB.GetAllTags
        clbFilterTags_Movies.Items.Add(Master.eLang.None)
        clbFilterTags_Movies.Items.AddRange(mTag)

        If Filter_Movie.Contains(Database.ColumnName.Tags, SmartFilter.Operators.IsNullOrEmpty) Then
            clbFilterTags_Movies.SetItemChecked(0, True)
        Else
            Dim rTags = Filter_Movie.Rules.Where(Function(f) f.Field = Database.ColumnName.Tags)
            If rTags.Count > 0 Then
                For i As Integer = 0 To rTags.Count - 1
                    If clbFilterTags_Movies.FindString(rTags(i).Value.ToString) > 0 Then
                        clbFilterTags_Movies.SetItemChecked(clbFilterTags_Movies.FindString(rTags(i).Value.ToString), True)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub Filter_RefreshList_Tag_TVShow()
        clbFilterTags_Shows.Items.Clear()
        Dim mTag() As Object = Master.DB.GetAllTags
        clbFilterTags_Shows.Items.Add(Master.eLang.None)
        clbFilterTags_Shows.Items.AddRange(mTag)

        If Filter_TVShows.Contains(Database.ColumnName.Tags, SmartFilter.Operators.IsNullOrEmpty) Then
            clbFilterTags_Shows.SetItemChecked(0, True)
        Else
            Dim rTags = Filter_TVShows.Rules.Where(Function(f) f.Field = Database.ColumnName.Tags)
            If rTags.Count > 0 Then
                For i As Integer = 0 To rTags.Count - 1
                    If clbFilterTags_Shows.FindString(rTags(i).Value.ToString) > 0 Then
                        clbFilterTags_Shows.SetItemChecked(clbFilterTags_Shows.FindString(rTags(i).Value.ToString), True)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub Filter_Refresh_Videosource_Movie()
        clbFilterVideoSources_Movies.Items.Clear()
        Dim mVideoSource() As Object = Master.DB.GetAllVideoSources_Movie
        clbFilterVideoSources_Movies.Items.Add(Master.eLang.None)
        clbFilterVideoSources_Movies.Items.AddRange(mVideoSource)

        If Filter_Movie.Contains(Database.ColumnName.VideoSource, SmartFilter.Operators.IsNullOrEmpty) Then
            clbFilterVideoSources_Movies.SetItemChecked(0, True)
        Else
            Dim rVideoSources = Filter_Movie.Rules.Where(Function(f) f.Field = Database.ColumnName.VideoSource)
            If rVideoSources.Count > 0 Then
                For i As Integer = 0 To rVideoSources.Count - 1
                    If clbFilterVideoSources_Movies.FindString(rVideoSources(i).Value.ToString) > 0 Then
                        clbFilterVideoSources_Movies.SetItemChecked(clbFilterVideoSources_Movies.FindString(rVideoSources(i).Value.ToString), True)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub Filter_Remove_Movie(Optional ByVal Reload As Boolean = False)
        lblFilter_Movies.Text = String.Format("{0} ({1})", Master.eLang.GetString(52, "Filters"), Master.eLang.GetString(1091, "Inactive"))
        _BsMovies.RemoveFilter()
        Filter_Movie = New SmartFilter.Filter(Enums.ContentType.Movie)
        filSearch_Movies = String.Empty

        RemoveHandler txtSearchMovies.TextChanged, AddressOf txtSearchMovies_TextChanged
        txtSearchMovies.Text = String.Empty
        AddHandler txtSearchMovies.TextChanged, AddressOf txtSearchMovies_TextChanged
        If cbSearchMovies.Items.Count > 0 Then
            cbSearchMovies.SelectedIndex = 0
        End If

        chkFilterDuplicates_Movies.Checked = False
        chkFilterLock_Movies.Checked = False
        chkFilterMark_Movies.Checked = False
        chkFilterMarkCustom1_Movies.Checked = False
        chkFilterMarkCustom2_Movies.Checked = False
        chkFilterMarkCustom3_Movies.Checked = False
        chkFilterMarkCustom4_Movies.Checked = False
        chkFilterMissing_Movies.Checked = False
        chkFilterNew_Movies.Checked = False
        chkFilterTolerance_Movies.Checked = False
        pnlFilterMissingItems_Movies.Visible = False
        rbFilterAnd_Movies.Checked = True
        rbFilterOr_Movies.Checked = False

        'Country
        txtFilterCountry_Movies.Text = String.Empty
        For i As Integer = 0 To clbFilterCountries_Movies.Items.Count - 1
            clbFilterCountries_Movies.SetItemChecked(i, False)
        Next
        'Data Field
        txtFilterDataField_Movies.Text = String.Empty
        For i As Integer = 0 To clbFilterDataFields_Movies.Items.Count - 1
            clbFilterDataFields_Movies.SetItemChecked(i, False)
        Next
        'Genre
        txtFilterGenre_Movies.Text = String.Empty
        For i As Integer = 0 To clbFilterGenres_Movies.Items.Count - 1
            clbFilterGenres_Movies.SetItemChecked(i, False)
        Next
        'Source
        txtFilterSource_Movies.Text = String.Empty
        For i As Integer = 0 To clbFilterSources_Movies.Items.Count - 1
            clbFilterSources_Movies.SetItemChecked(i, False)
        Next
        'Tag 
        txtFilterTag_Movies.Text = String.Empty
        For i As Integer = 0 To clbFilterTags_Movies.Items.Count - 1
            clbFilterTags_Movies.SetItemChecked(i, False)
        Next
        'VideoSource
        txtFilterVideoSource_Movies.Text = String.Empty
        For i As Integer = 0 To clbFilterVideoSources_Movies.Items.Count - 1
            clbFilterVideoSources_Movies.SetItemChecked(i, False)
        Next

        RemoveHandler cbFilterDataField_Movies.SelectedIndexChanged, AddressOf clbFilterDataFields_Movies_LostFocus
        If cbFilterDataField_Movies.Items.Count > 0 Then
            cbFilterDataField_Movies.SelectedIndex = 0
        End If
        AddHandler cbFilterDataField_Movies.SelectedIndexChanged, AddressOf clbFilterDataFields_Movies_LostFocus

        RemoveHandler cbFilterYearFrom_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie
        If cbFilterYearFrom_Movies.Items.Count > 0 Then
            cbFilterYearFrom_Movies.SelectedIndex = 0
        End If
        AddHandler cbFilterYearFrom_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie

        RemoveHandler cbFilterYearModFrom_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie
        If cbFilterYearModFrom_Movies.Items.Count > 0 Then
            cbFilterYearModFrom_Movies.SelectedIndex = 0
        End If
        AddHandler cbFilterYearModFrom_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie

        RemoveHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie
        If cbFilterYearTo_Movies.Items.Count > 0 Then
            cbFilterYearTo_Movies.SelectedIndex = 0
        End If
        cbFilterYearTo_Movies.Enabled = False
        AddHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie

        RemoveHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie
        If cbFilterYearModTo_Movies.Items.Count > 0 Then
            cbFilterYearModTo_Movies.SelectedIndex = 0
        End If
        cbFilterYearModTo_Movies.Enabled = False
        AddHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie

        If Reload Then DataGridView_Fill_Main(True, False, False)

        AddonsManager.Instance.RuntimeObjects.FilterMovies = String.Empty
    End Sub

    Private Sub Filter_Remove_Movieset(Optional ByVal Reload As Boolean = False)
        lblFilter_MovieSets.Text = String.Format("{0} ({1})", Master.eLang.GetString(52, "Filters"), Master.eLang.GetString(1091, "Inactive"))
        _BsMoviesets.RemoveFilter()
        Filter_Movieset = New SmartFilter.Filter(Enums.ContentType.Movieset)

        RemoveHandler txtSearchMovieSets.TextChanged, AddressOf txtSearchMovieSets_TextChanged
        txtSearchMovieSets.Text = String.Empty
        AddHandler txtSearchMovieSets.TextChanged, AddressOf txtSearchMovieSets_TextChanged
        If cbSearchMovieSets.Items.Count > 0 Then
            cbSearchMovieSets.SelectedIndex = 0
        End If

        chkFilterEmpty_MovieSets.Checked = False
        chkFilterLock_MovieSets.Checked = False
        chkFilterMark_MovieSets.Checked = False
        chkFilterMissing_MovieSets.Checked = False
        chkFilterMultiple_MovieSets.Checked = False
        chkFilterNew_MovieSets.Checked = False
        chkFilterOne_MovieSets.Checked = False
        pnlFilterMissingItems_MovieSets.Visible = False
        rbFilterAnd_MovieSets.Checked = True
        rbFilterOr_MovieSets.Checked = False

        If Reload Then DataGridView_Fill_Main(False, True, False)
    End Sub

    Private Sub Filter_Remove_TVShow(Optional ByVal Reload As Boolean = False)
        lblFilter_Shows.Text = String.Format("{0} ({1})", Master.eLang.GetString(52, "Filters"), Master.eLang.GetString(1091, "Inactive"))
        _BsTVShows.RemoveFilter()
        Filter_TVShows = New SmartFilter.Filter(Enums.ContentType.TVShow)
        filSearch_TVShows = String.Empty

        RemoveHandler txtSearchShows.TextChanged, AddressOf txtSearchShows_TextChanged
        txtSearchShows.Text = String.Empty
        AddHandler txtSearchShows.TextChanged, AddressOf txtSearchShows_TextChanged
        If cbSearchShows.Items.Count > 0 Then
            cbSearchShows.SelectedIndex = 0
        End If

        chkFilterLock_Shows.Checked = False
        chkFilterMark_Shows.Checked = False
        chkFilterMissing_Shows.Checked = False
        chkFilterNewEpisodes_Shows.Checked = False
        chkFilterNewShows_Shows.Checked = False
        pnlFilterMissingItems_Shows.Visible = False
        rbFilterAnd_Shows.Checked = True
        rbFilterOr_Shows.Checked = False

        'Genre 
        txtFilterGenre_Shows.Text = String.Empty
        For i As Integer = 0 To clbFilterGenres_Shows.Items.Count - 1
            clbFilterGenres_Shows.SetItemChecked(i, False)
        Next
        'Source 
        txtFilterSource_Shows.Text = String.Empty
        For i As Integer = 0 To clbFilterSource_Shows.Items.Count - 1
            clbFilterSource_Shows.SetItemChecked(i, False)
        Next
        'Tag 
        txtFilterTag_Shows.Text = String.Empty
        For i As Integer = 0 To clbFilterTags_Shows.Items.Count - 1
            clbFilterTags_Shows.SetItemChecked(i, False)
        Next

        If Reload Then DataGridView_Fill_Main(False, False, True)

        AddonsManager.Instance.RuntimeObjects.FilterTVShows = String.Empty
    End Sub

    Private Sub FilterPanel_FilterCountryClose_Movie_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblFilterCountriesClose_Movie.Click
        txtFilterCountry_Movies.Focus()
        pnlFilterCountries_Movies.Tag = String.Empty
    End Sub

    Private Sub FilterPanel_FilterGenreClose_Movie_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblFilterGenresClose_Movie.Click
        txtFilterGenre_Movies.Focus()
        pnlFilterGenres_Movies.Tag = String.Empty
    End Sub

    Private Sub FilterPanel_FilterGenresClose_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblFilterGenresClose_TVShow.Click
        txtFilterGenre_Shows.Focus()
        pnlFilterGenres_Shows.Tag = String.Empty
    End Sub

    Private Sub FilterPanel_FilterDataFieldsClose_Movie_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblFilterDataFieldsClose_Movie.Click
        txtFilterDataField_Movies.Focus()
        pnlFilterDataFields_Movies.Tag = String.Empty
    End Sub

    Private Sub FilterPanel_FilterMissingItemsClose_Movie_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblFilterMissingItemsClose_Movie.Click
        pnlFilterMissingItems_Movies.Visible = False
    End Sub

    Private Sub FilterPanel_FilterMissingItemsClose_Movieset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblFilterMissingItemsClose_Movieset.Click
        pnlFilterMissingItems_MovieSets.Visible = False
    End Sub

    Private Sub FilterPanel_FilterMissingItemsClose_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblFilterMissingItemsClose_TVShow.Click
        pnlFilterMissingItems_Shows.Visible = False
    End Sub

    Private Sub FilterPanel_FilterSourceClose_Movie_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblFilterSourcesClose_Movie.Click
        txtFilterSource_Movies.Focus()
        pnlFilterSources_Movies.Tag = String.Empty
    End Sub

    Private Sub FilterPanel_FilterSourceClose_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblFilterSourcesClose_TVShow.Click
        txtFilterSource_Shows.Focus()
        pnlFilterSources_Shows.Tag = String.Empty
    End Sub

    Private Sub FilterPanel_FilterTagClose_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblFilterTagsClose_Movies.Click
        txtFilterTag_Movies.Focus()
        pnlFilterTags_Movies.Tag = String.Empty
    End Sub

    Private Sub FilterPanel_FilterTagsClose_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblFilterTagsClose_TVShow.Click
        txtFilterTag_Shows.Focus()
        pnlFilterTags_Shows.Tag = String.Empty
    End Sub

    Private Sub FilterPanel_FilterVideSourceClose_Movie_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblFilterVideoSourcesClose_Movie.Click
        txtFilterVideoSource_Movies.Focus()
        pnlFilterVideoSources_Movies.Tag = String.Empty
    End Sub

    Private Sub FilterPanel_IsEnabled_Movie(ByVal isEnabled As Boolean)
        btnClearFilters_Movies.Enabled = isEnabled
        btnFilterMissing_Movies.Enabled = isEnabled
        btnFilterSortDateAdded_Movies.Enabled = isEnabled
        btnFilterSortDateModified_Movies.Enabled = isEnabled
        btnFilterSortRating_Movies.Enabled = isEnabled
        btnFilterSortTitle_Movies.Enabled = isEnabled
        btnFilterSortYear_Movies.Enabled = isEnabled
        cbFilterDataField_Movies.Enabled = isEnabled
        cbFilterLists_Movies.Enabled = isEnabled
        cbFilterLists_MovieSets.Enabled = isEnabled
        cbFilterLists_Shows.Enabled = isEnabled
        cbFilterYearFrom_Movies.Enabled = isEnabled
        cbFilterYearModFrom_Movies.Enabled = isEnabled
        cbSearchMovies.Enabled = isEnabled
        chkFilterDuplicates_Movies.Enabled = isEnabled
        chkFilterLock_Movies.Enabled = isEnabled
        chkFilterMark_Movies.Enabled = isEnabled
        chkFilterMarkCustom1_Movies.Enabled = isEnabled
        chkFilterMarkCustom2_Movies.Enabled = isEnabled
        chkFilterMarkCustom3_Movies.Enabled = isEnabled
        chkFilterMarkCustom4_Movies.Enabled = isEnabled
        chkFilterMissing_Movies.Enabled = If(Master.eSettings.MovieMissingItemsAnyEnabled, isEnabled, False)
        chkFilterNew_Movies.Enabled = isEnabled
        chkFilterTolerance_Movies.Enabled = If(Master.eSettings.MovieLevTolerance > 0, isEnabled, False)
        pnlFilterMissingItems_Movies.Visible = If(Not isEnabled, False, pnlFilterMissingItems_Movies.Visible)
        rbFilterAnd_Movies.Enabled = isEnabled
        rbFilterOr_Movies.Enabled = isEnabled
        txtFilterCountry_Movies.Enabled = isEnabled
        txtFilterGenre_Movies.Enabled = isEnabled
        txtFilterDataField_Movies.Enabled = isEnabled
        txtFilterSource_Movies.Enabled = isEnabled
        txtFilterVideoSource_Movies.Enabled = isEnabled
    End Sub

    Private Sub FilterPanel_IsEnabled_Movieset(ByVal isEnabled As Boolean)
        btnClearFilters_MovieSets.Enabled = isEnabled
        btnFilterMissing_MovieSets.Enabled = isEnabled
        cbSearchMovieSets.Enabled = isEnabled
        chkFilterEmpty_MovieSets.Enabled = isEnabled
        chkFilterLock_MovieSets.Enabled = isEnabled
        chkFilterMark_MovieSets.Enabled = isEnabled
        chkFilterMissing_MovieSets.Enabled = If(Master.eSettings.MovieSetMissingItemsAnyEnabled, isEnabled, False)
        chkFilterMultiple_MovieSets.Enabled = isEnabled
        chkFilterNew_MovieSets.Enabled = isEnabled
        chkFilterOne_MovieSets.Enabled = isEnabled
        pnlFilterMissingItems_MovieSets.Visible = If(Not isEnabled, False, pnlFilterMissingItems_MovieSets.Visible)
        rbFilterAnd_MovieSets.Enabled = isEnabled
        rbFilterOr_MovieSets.Enabled = isEnabled
    End Sub

    Private Sub FilterPanel_IsEnabled_TVShow(ByVal isEnabled As Boolean)
        btnClearFilters_Shows.Enabled = isEnabled
        btnFilterMissing_Shows.Enabled = isEnabled
        btnFilterSortTitle_Shows.Enabled = isEnabled
        cbSearchShows.Enabled = isEnabled
        chkFilterLock_Shows.Enabled = isEnabled
        chkFilterMark_Shows.Enabled = isEnabled
        chkFilterMissing_Shows.Enabled = If(Master.eSettings.TVShowMissingItemsAnyEnabled, isEnabled, False)
        chkFilterNewEpisodes_Shows.Enabled = isEnabled
        chkFilterNewShows_Shows.Enabled = isEnabled
        pnlFilterMissingItems_Shows.Visible = If(Not isEnabled, False, pnlFilterMissingItems_Shows.Visible)
        rbFilterAnd_Shows.Enabled = isEnabled
        rbFilterOr_Shows.Enabled = isEnabled
        txtFilterSource_Shows.Enabled = isEnabled
    End Sub

    Private Sub FilterPanel_Move_Movie()
        If _FilterPanelIsRaised_Movie Then
            pnlFilter_Movies.AutoSize = True
        Else
            pnlFilter_Movies.AutoSize = False
            pnlFilter_Movies.Height = pnlFilterTop_Movies.Height
        End If

        If pnlFilter_Movies.Height = pnlFilterTop_Movies.Height Then
            btnFilterUp_Movies.Enabled = True
            btnFilterDown_Movies.Enabled = False
        ElseIf pnlFilter_Movies.AutoSize Then
            btnFilterUp_Movies.Enabled = False
            btnFilterDown_Movies.Enabled = True
        End If

        dgvMovies.Invalidate()
    End Sub

    Private Sub FilterPanel_Move_Movieset()
        If _FilterPanelIsRaised_Movieset Then
            pnlFilter_MovieSets.AutoSize = True
        Else
            pnlFilter_MovieSets.AutoSize = False
            pnlFilter_MovieSets.Height = pnlFilterTop_MovieSets.Height
        End If

        If pnlFilter_MovieSets.Height = pnlFilterTop_MovieSets.Height Then
            btnFilterUp_MovieSets.Enabled = True
            btnFilterDown_MovieSets.Enabled = False
        ElseIf pnlFilter_MovieSets.AutoSize Then
            btnFilterUp_MovieSets.Enabled = False
            btnFilterDown_MovieSets.Enabled = True
        End If

        dgvMoviesets.Invalidate()
    End Sub

    Private Sub FilterPanel_Move_TVShow()
        If _FilterPanelIsRaised_TVShow Then
            pnlFilter_Shows.AutoSize = True
        Else
            pnlFilter_Shows.AutoSize = False
            pnlFilter_Shows.Height = pnlFilterTop_Shows.Height
        End If

        If pnlFilter_Shows.Height = pnlFilterTop_Shows.Height Then
            btnFilterUp_Shows.Enabled = True
            btnFilterDown_Shows.Enabled = False
        ElseIf pnlFilter_Shows.AutoSize Then
            btnFilterUp_Shows.Enabled = False
            btnFilterDown_Shows.Enabled = True
        End If

        dgvTVShows.Invalidate()
    End Sub
    ''' <summary>
    ''' This is a generic callback function.
    ''' </summary>
    ''' <param name="mType"></param>
    ''' <param name="_params"></param>
    ''' <remarks></remarks>
    Private Sub Generic_RunCallBack(ByVal mType As Enums.AddonEventType, ByRef _params As List(Of Object))
        Select Case mType

            Case Enums.AddonEventType.CommandLine
                Select Case _params(0).ToString
                    Case "addmoviesource"
                        Using dSource As New dlgSource_Movie(Master.DB.Load_AllSources_Movie)
                            If dSource.ShowDialog(CStr(_params(1)), CStr(_params(1))) = DialogResult.OK Then
                                Master.DB.Save_Source_Movie(dSource.Result)
                                SetMenus(True)
                            End If
                        End Using
                    Case "addtvshowsource"
                        Using dSource As New dlgSource_TVShow(Master.DB.Load_AllSources_TVShow)
                            If dSource.ShowDialog(CStr(_params(1)), CStr(_params(1))) = DialogResult.OK Then
                                SetMenus(True)
                            End If
                        End Using
                    Case "cleanvideodb"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(644, "Cleaning Database..."))
                        CleanDB(New Scanner.ScanOrCleanOptions With {.Movies = True, .Moviesets = True, .TV = True})
                        While bwCleanDB.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                    Case "close"
                        Close()
                        Application.Exit()
                    Case "loadmedia"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(860, "Loading Media..."))
                        LoadingDone = False
                        Master.TaskManager.AddTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scan) With {
                                             .ListOfID = New List(Of Long) From {CLng(_params(2))},
                                             .ScanOrCleanOptions = CType(_params(1), Scanner.ScanOrCleanOptions),
                                             .CommonStringValue = CStr(_params(3))
                                             })
                        While Not LoadingDone
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                    Case "run"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(859, "Running Addon..."))
                        Dim strModuleName As String = CStr(_params(1))
                        Dim oParameters As List(Of Object) = CType(_params(2), List(Of Object))
                        Dim gModule = AddonsManager.Instance.Addons.FirstOrDefault(Function(y) y.AssemblyName = strModuleName)
                        If gModule IsNot Nothing Then
                            gModule.ProcessorModule.Run(Nothing, Enums.AddonEventType.CommandLine, oParameters)
                        End If
                    Case "scrapemovies"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(861, "Command Line Scraping..."))
                        Dim ScrapeModifiers As Structures.ScrapeModifiers = CType(_params(2), Structures.ScrapeModifiers)
                        CreateScrapeList_Movie(CType(_params(1), Enums.ScrapeType), Master.eSettings.DefaultScrapeOptions(Enums.ContentType.Movie), ScrapeModifiers)
                        While bwMovieScraper.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                    Case "scrapemoviesets"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(861, "Command Line Scraping..."))
                        Dim ScrapeModifiers As Structures.ScrapeModifiers = CType(_params(2), Structures.ScrapeModifiers)
                        CreateScrapeList_Movieset(CType(_params(1), Enums.ScrapeType), Master.eSettings.DefaultScrapeOptions(Enums.ContentType.Movieset), ScrapeModifiers)
                        While bwMoviesetScraper.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                    Case "scrapetvshows"
                        Master.fLoading.SetProgressBarStyle(ProgressBarStyle.Marquee)
                        Master.fLoading.SetLoadingMesg(Master.eLang.GetString(861, "Command Line Scraping..."))
                        Dim ScrapeModifiers As Structures.ScrapeModifiers = CType(_params(2), Structures.ScrapeModifiers)
                        CreateScrapeList_TV(CType(_params(1), Enums.ScrapeType), Master.eSettings.DefaultScrapeOptions(Enums.ContentType.TV), ScrapeModifiers)
                        While bwTVScraper.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                End Select

            Case Enums.AddonEventType.Generic
                Select Case _params(0).ToString
                    Case "controlsenabled"
                        SetControlsEnabled(Convert.ToBoolean(_params(1)), If(_params.Count = 3, Convert.ToBoolean(_params(2)), False))
                    Case "filllist"
                        DataGridView_Fill_Main(CBool(_params(1)), CBool(_params(2)), CBool(_params(3)))
                End Select
            Case Enums.AddonEventType.Notification
                Select Case _params(0).ToString
                    Case "error"
                        dlgErrorViewer.Show(Me)
                    Case Else
                        Activate()
                End Select

            Case Enums.AddonEventType.AfterEdit_Movie
                DataGridView_RefreshRow_Movie(Convert.ToInt64(_params(0)))

            Case Enums.AddonEventType.AfterEdit_TVEpisode
                DataGridView_RefreshRow_TVEpisode(Convert.ToInt64(_params(0)))

            Case Enums.AddonEventType.AfterEdit_TVShow
                DataGridView_RefreshRow_TVShow(Convert.ToInt64(_params(0)))

            Case Enums.AddonEventType.Remove_Movie
                DataGridView_RemoveRow_Movie(Convert.ToInt64(_params(0)))

            Case Enums.AddonEventType.Remove_MovieSet
                DataGridView_RemoveRow_Movieset(Convert.ToInt64(_params(0)))

            Case Enums.AddonEventType.Remove_TVEpisode
                DataGridView_RemoveRow_TVEpisode(Convert.ToInt64(_params(0)))

            Case Enums.AddonEventType.Remove_TVSeason
                DataGridView_RemoveRow_TVSeason(Convert.ToInt64(_params(0)))

            Case Enums.AddonEventType.Remove_TVShow
                DataGridView_RemoveRow_TVShow(Convert.ToInt64(_params(0)))

            Case Else
                _Logger.Warn("Callback for <{0}> with no handler.", mType)
        End Select
    End Sub

    Private Function MainTab_GetCurrentTag() As Settings.MainTabSorting
        If tcMain.SelectedTab IsNot Nothing AndAlso TryCast(tcMain.SelectedTab.Tag, Settings.MainTabSorting) IsNot Nothing Then
            Return DirectCast(tcMain.SelectedTab.Tag, Settings.MainTabSorting)
        Else
            Return New Settings.MainTabSorting
        End If
    End Function

    Private Sub MainTab_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tcMain.SelectedIndexChanged
        ClearInfo()
        ShowNoInfo(False)
        Dim currMainTabTag = MainTab_GetCurrentTag()
        AddonsManager.Instance.RuntimeObjects.MediaTabSelected = currMainTabTag
        Select Case currMainTabTag.ContentType
            Case Enums.ContentType.Movie
                'fixing TV-Splitter issues
                RemoveHandler scTV.SplitterMoved, AddressOf TVSplitterMoved
                RemoveHandler scTVSeasonsEpisodes.SplitterMoved, AddressOf TVSplitterMoved

                currList_Movies = currMainTabTag.DefaultList
                cbFilterLists_Movies.SelectedValue = currList_Movies
                AddonsManager.Instance.RuntimeObjects.ListMovies = currList_Movies
                DataGridView_Fill_Main(True, False, False)
                mnuMainTools.Enabled = True
                cmnuTrayTools.Enabled = True
                mnuScrapeMovies.Visible = True
                mnuScrapeMoviesets.Visible = False
                mnuScrapeTVShows.Visible = False
                pnlFilter_Movies.Visible = True
                pnlFilter_MovieSets.Visible = False
                pnlFilter_Shows.Visible = False
                pnlFilterMissingItems_MovieSets.Visible = False
                pnlFilterMissingItems_Shows.Visible = False
                pnlListTop.Height = 56
                pnlSearchMovies.Visible = True
                pnlSearchMovieSets.Visible = False
                pnlSearchTVShows.Visible = False
                dgvMoviesets.Visible = False
                dgvMovies.Visible = True
                Theme_Apply(currMainTabTag.ContentType)
                If bwLoadImages.IsBusy Then bwLoadImages.CancelAsync()
                If bwLoadImages_MovieSetMoviePosters.IsBusy Then bwLoadImages_MovieSetMoviePosters.CancelAsync()
                If bwDownloadPic.IsBusy Then bwDownloadPic.CancelAsync()
                If bwDownloadGuestStarPic.IsBusy Then bwDownloadGuestStarPic.CancelAsync()
                If dgvMovies.RowCount > 0 Then
                    prevRow_Movie = -1

                    dgvMovies.CurrentCell = Nothing
                    dgvMovies.ClearSelection()
                    dgvMovies.Rows(0).Selected = True
                    dgvMovies.CurrentCell = dgvMovies.Rows(0).Cells("ListTitle")

                    dgvMovies.Focus()
                Else
                    SetControlsEnabled(True)
                End If

            Case Enums.ContentType.Movieset
                'fixing TV-Splitter issues
                RemoveHandler scTV.SplitterMoved, AddressOf TVSplitterMoved
                RemoveHandler scTVSeasonsEpisodes.SplitterMoved, AddressOf TVSplitterMoved

                currList_MovieSets = currMainTabTag.DefaultList
                cbFilterLists_MovieSets.SelectedValue = currList_MovieSets
                AddonsManager.Instance.RuntimeObjects.ListMovieSets = currList_MovieSets
                DataGridView_Fill_Main(False, True, False)
                mnuMainTools.Enabled = True
                cmnuTrayTools.Enabled = True
                mnuScrapeMovies.Visible = False
                mnuScrapeMoviesets.Visible = True
                mnuScrapeTVShows.Visible = False
                pnlFilter_Movies.Visible = False
                pnlFilter_MovieSets.Visible = True
                pnlFilter_Shows.Visible = False
                pnlFilterMissingItems_Movies.Visible = False
                pnlFilterMissingItems_Shows.Visible = False
                pnlListTop.Height = 56
                pnlSearchMovies.Visible = False
                pnlSearchMovieSets.Visible = True
                pnlSearchTVShows.Visible = False
                dgvMovies.Visible = False
                dgvMoviesets.Visible = True
                Theme_Apply(currMainTabTag.ContentType)
                If bwLoadImages.IsBusy Then bwLoadImages.CancelAsync()
                If bwDownloadPic.IsBusy Then bwDownloadPic.CancelAsync()
                If bwDownloadGuestStarPic.IsBusy Then bwDownloadGuestStarPic.CancelAsync()
                If dgvMoviesets.RowCount > 0 Then
                    prevRow_MovieSet = -1

                    dgvMoviesets.CurrentCell = Nothing
                    dgvMoviesets.ClearSelection()
                    dgvMoviesets.Rows(0).Selected = True
                    dgvMoviesets.CurrentCell = dgvMoviesets.Rows(0).Cells("ListTitle")

                    dgvMoviesets.Focus()
                Else
                    SetControlsEnabled(True)
                End If

            Case Enums.ContentType.TV
                currList_TVShows = currMainTabTag.DefaultList
                cbFilterLists_Shows.SelectedValue = currList_TVShows
                AddonsManager.Instance.RuntimeObjects.ListTVShows = currList_TVShows
                DataGridView_Fill_Main(False, False, True)
                mnuMainTools.Enabled = True
                cmnuTrayTools.Enabled = True
                mnuScrapeMovies.Visible = False
                mnuScrapeMoviesets.Visible = False
                mnuScrapeTVShows.Visible = True
                dgvMovies.Visible = False
                dgvMoviesets.Visible = False
                pnlFilter_Movies.Visible = False
                pnlFilter_MovieSets.Visible = False
                pnlFilter_Shows.Visible = True
                pnlFilterMissingItems_Movies.Visible = False
                pnlFilterMissingItems_MovieSets.Visible = False
                pnlListTop.Height = 56
                pnlSearchMovies.Visible = False
                pnlSearchMovieSets.Visible = False
                pnlSearchTVShows.Visible = True

                'fixing TV-Splitter issues
                Try
                    scTV.SplitterDistance = Manager.mSettings.MainOptions.GuiSettings.SplitterDistance_TVShow
                    scTVSeasonsEpisodes.SplitterDistance = Manager.mSettings.MainOptions.GuiSettings.SplitterDistance_TVSeason
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
                AddHandler scTV.SplitterMoved, AddressOf TVSplitterMoved
                AddHandler scTVSeasonsEpisodes.SplitterMoved, AddressOf TVSplitterMoved

                Theme_Apply(Enums.ContentType.TVShow)
                If bwLoadImages.IsBusy Then bwLoadImages.CancelAsync()
                If bwLoadImages_MovieSetMoviePosters.IsBusy Then bwLoadImages_MovieSetMoviePosters.CancelAsync()
                If bwDownloadPic.IsBusy Then bwDownloadPic.CancelAsync()
                If bwDownloadGuestStarPic.IsBusy Then bwDownloadGuestStarPic.CancelAsync()
                If dgvTVShows.RowCount > 0 Then
                    prevRow_TVShow = -1
                    currList = 0

                    dgvTVShows.CurrentCell = Nothing
                    dgvTVShows.ClearSelection()
                    dgvTVShows.Rows(0).Selected = True
                    dgvTVShows.CurrentCell = dgvTVShows.Rows(0).Cells("ListTitle")

                    dgvTVShows.Focus()
                Else
                    SetControlsEnabled(True)
                End If
        End Select
    End Sub

    Private Sub MainTab_SetTabs()
        tcMain.Visible = False
        'cleanup tabs
        tcMain.TabPages.Clear()
        If Master.eSettings.GeneralMainTabSorting.Count = 0 Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.MainTabSorting, True)
        End If
        'add tabs
        For Each nTab In Master.eSettings.GeneralMainTabSorting.OrderBy(Function(f) f.Order)
            tcMain.TabPages.Add(New TabPage With {.Text = nTab.Title, .Tag = nTab})
        Next

        'workaround to force that the first tab will be selected after adding tabs to an empty TabControl
        RemoveHandler tcMain.SelectedIndexChanged, AddressOf MainTab_SelectedIndexChanged
        tcMain.SelectedIndex = -1
        AddHandler tcMain.SelectedIndexChanged, AddressOf MainTab_SelectedIndexChanged
        tcMain.SelectTab(0)
        MainTab_UpdateCounts()
        tcMain.Visible = True
    End Sub
    ''' <summary>
    ''' Update the displayed movie counts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MainTab_SetCount_Movie()
        Dim currMainTabTag = MainTab_GetCurrentTag()
        If currMainTabTag.ContentType = Enums.ContentType.Movie Then
            If dgvMovies.RowCount > 0 Then
                tcMain.SelectedTab.Text = String.Format("{0} ({1})", currMainTabTag.Title, dgvMovies.RowCount)
            Else
                tcMain.SelectedTab.Text = currMainTabTag.Title
            End If
        End If
    End Sub
    ''' <summary>
    ''' Update the displayed movieset counts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MainTab_SetCount_Movieset()
        Dim currMainTabTag = MainTab_GetCurrentTag()
        If currMainTabTag.ContentType = Enums.ContentType.Movieset Then
            If dgvMoviesets.RowCount > 0 Then
                tcMain.SelectedTab.Text = String.Format("{0} ({1})", currMainTabTag.Title, dgvMoviesets.RowCount)
            Else
                tcMain.SelectedTab.Text = currMainTabTag.Title
            End If
        End If
    End Sub
    ''' <summary>
    ''' Update the displayed show/episode counts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MainTab_SetCount_TV()
        Dim currMainTabTag = MainTab_GetCurrentTag()
        If currMainTabTag.ContentType = Enums.ContentType.TV Then
            If dgvTVShows.RowCount > 0 Then
                Dim epCount As Integer = 0
                For i As Integer = 0 To dgvTVShows.Rows.Count - 1
                    epCount += CInt(dgvTVShows.Rows(i).Cells("Episodes").Value)
                Next
                tcMain.SelectedTab.Text = String.Format("{0} ({1}/{2})", currMainTabTag.Title, dgvTVShows.RowCount, epCount)
            Else
                tcMain.SelectedTab.Text = currMainTabTag.Title
            End If
        End If
    End Sub

    Private Sub MainTab_UpdateCounts()
        For Each mTabPage As TabPage In tcMain.Controls
            Dim currMainTabTag = DirectCast(mTabPage.Tag, Settings.MainTabSorting)
            Dim mCount As Integer = Master.DB.GetViewMediaCount(currMainTabTag.DefaultList)
            Select Case currMainTabTag.ContentType
                Case Enums.ContentType.Movie, Enums.ContentType.Movieset
                    If mCount = -1 Then
                        mTabPage.Text = String.Format("{0} ({1})", currMainTabTag.Title, "SQL Error")
                        mTabPage.Enabled = False
                    Else
                        mTabPage.Text = String.Format("{0} ({1})", currMainTabTag.Title, mCount)
                        mTabPage.Enabled = True
                    End If
                Case Enums.ContentType.TV
                    If mCount = -1 Then
                        mTabPage.Text = String.Format("{0} ({1})", currMainTabTag.Title, "SQL Error")
                        mTabPage.Enabled = False
                    Else
                        Dim epCount As Integer = Master.DB.GetViewMediaCount(currMainTabTag.DefaultList, True)
                        mTabPage.Text = String.Format("{0} ({1}/{2})", currMainTabTag.Title, mCount, epCount)
                        mTabPage.Enabled = True
                    End If
            End Select
        Next
    End Sub

    Private Sub Task_RunCallBack(ByVal mType As Enums.AddonEventType, ByRef _params As List(Of Object))
        _TaskList.Enqueue(New Task With {.mType = mType, .Params = _params})
        If _TasksDone Then
            _TasksDone = False
            _Task_RunTimer.Start()
        End If
    End Sub

    Private Sub Task_RunTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles _Task_RunTimer.Tick
        _Task_RunTimer.Enabled = False
        _TasksDone = False
        While _TaskList.Count > 0
            Dim nTask = _TaskList.Dequeue
            Generic_RunCallBack(nTask.mType, nTask.Params)
        End While
        _TasksDone = True
    End Sub

    Private Sub TaskManager_CreateTask(ByVal taskItem As TaskManager.TaskItem)
        Dim nDataGridView As DataGridView = Nothing
        Dim strIDName As String = String.Empty

        Select Case taskItem.ContentType
            Case Enums.ContentType.Movie
                nDataGridView = dgvMovies
                strIDName = Database.Helpers.GetMainIdName(Database.TableName.movie)
            Case Enums.ContentType.Movieset
                nDataGridView = dgvMoviesets
                strIDName = Database.Helpers.GetMainIdName(Database.TableName.movieset)
            Case Enums.ContentType.TVEpisode
                nDataGridView = dgvTVEpisodes
                strIDName = Database.Helpers.GetMainIdName(Database.TableName.episode)
            Case Enums.ContentType.TVSeason
                nDataGridView = dgvTVSeasons
                strIDName = Database.Helpers.GetMainIdName(Database.TableName.season)
            Case Enums.ContentType.TVShow, Enums.ContentType.TV
                nDataGridView = dgvTVShows
                strIDName = Database.Helpers.GetMainIdName(Database.TableName.tvshow)
        End Select

        If nDataGridView IsNot Nothing AndAlso Not String.IsNullOrEmpty(strIDName) Then
            Select Case taskItem.SelectionType
                Case Enums.SelectionType.Filtered
                    If nDataGridView.Rows.Count > 0 Then
                        For Each sRow As DataGridViewRow In nDataGridView.Rows
                            taskItem.ListOfID.Add(Convert.ToInt64(sRow.Cells(strIDName).Value))
                        Next
                        Master.TaskManager.AddTask(taskItem)
                    End If

                Case Enums.SelectionType.Selected
                    If nDataGridView.SelectedRows.Count > 0 Then
                        For Each sRow As DataGridViewRow In nDataGridView.SelectedRows
                            taskItem.ListOfID.Add(Convert.ToInt64(sRow.Cells(strIDName).Value))
                        Next
                        Master.TaskManager.AddTask(taskItem)
                    End If
                Case Else
                    Master.TaskManager.AddTask(taskItem)
            End Select
        End If
    End Sub

    Private Sub TaskManager_ProgressUpdate(ByVal eProgressValue As TaskManager.ProgressValue)
        Select Case eProgressValue.EventType

            Case TaskManager.EventType.RefreshRow
                Select Case eProgressValue.ContentType
                    Case Enums.ContentType.Movie
                        DataGridView_RefreshRow_Movie(eProgressValue.ID)
                    Case Enums.ContentType.Movieset
                        DataGridView_RefreshRow_Movieset(eProgressValue.ID)
                    Case Enums.ContentType.TVEpisode
                        DataGridView_RefreshRow_TVEpisode(eProgressValue.ID)
                    Case Enums.ContentType.TVSeason
                        DataGridView_RefreshRow_TVSeason(eProgressValue.ID)
                    Case Enums.ContentType.TVShow
                        DataGridView_RefreshRow_TVShow(eProgressValue.ID)
                End Select

            Case TaskManager.EventType.SimpleMessage
                SetStatus(eProgressValue.Message)
                'tspbLoading.Value = e.ProgressPercentage

            Case TaskManager.EventType.TaskManagerEnded
                ChangeToolStripLabel(tslLoading, False, String.Empty)
                ChangeToolStripProgressBar(tspbLoading, False, 0, 0, 0, ProgressBarStyle.Marquee)

            Case TaskManager.EventType.TaskManagerStarted
                ChangeToolStripLabel(tslLoading, True, eProgressValue.Message)
                ChangeToolStripProgressBar(tspbLoading, True, 100, 0, 0, ProgressBarStyle.Marquee)

            Case Else
                _Logger.Warn("Callback for <{0}> with no handler.", eProgressValue.EventType)
        End Select
    End Sub

    Private Sub Theme_Apply(ByVal tType As Enums.ContentType)
        pnlInfoPanel.SuspendLayout()

        currThemeType = tType

        tTheme.ApplyTheme(tType)

        Dim iState As Integer
        Select Case currThemeType
            Case Enums.ContentType.Movie
                iState = _InfoPanelState_Movie
            Case Enums.ContentType.Movieset
                iState = _InfoPanelState_Movieset
            Case Enums.ContentType.TVEpisode
                iState = _InfoPanelState_TVEpisode
            Case Enums.ContentType.TVSeason
                iState = _InfoPanelState_TVSeason
            Case Enums.ContentType.TVShow
                iState = _InfoPanelState_TVShow
        End Select
        Select Case iState
            Case 1
                If btnMid.Visible Then
                    pnlInfoPanel.Height = InfoPanelMidHeight
                    btnUp.Enabled = True
                    btnMid.Enabled = False
                    btnDown.Enabled = True
                ElseIf btnUp.Visible Then
                    pnlInfoPanel.Height = InfoPanelUpHeight
                    btnUp.Enabled = False
                    btnMid.Enabled = True
                    btnDown.Enabled = True
                    Select Case currThemeType
                        Case Enums.ContentType.Movie
                            _InfoPanelState_Movie = 2
                        Case Enums.ContentType.Movieset
                            _InfoPanelState_Movieset = 2
                        Case Enums.ContentType.TVEpisode
                            _InfoPanelState_TVEpisode = 2
                        Case Enums.ContentType.TVSeason
                            _InfoPanelState_TVSeason = 2
                        Case Enums.ContentType.TVShow
                            _InfoPanelState_TVShow = 2
                    End Select
                Else
                    pnlInfoPanel.Height = 32
                    btnUp.Enabled = True
                    btnMid.Enabled = True
                    btnDown.Enabled = False
                    Select Case currThemeType
                        Case Enums.ContentType.Movie
                            _InfoPanelState_Movie = 0
                        Case Enums.ContentType.Movieset
                            _InfoPanelState_Movieset = 0
                        Case Enums.ContentType.TVEpisode
                            _InfoPanelState_TVEpisode = 0
                        Case Enums.ContentType.TVSeason
                            _InfoPanelState_TVSeason = 0
                        Case Enums.ContentType.TVShow
                            _InfoPanelState_TVShow = 0
                    End Select
                End If
            Case 2
                If btnUp.Visible Then
                    pnlInfoPanel.Height = InfoPanelUpHeight
                    btnUp.Enabled = False
                    btnMid.Enabled = True
                    btnDown.Enabled = True
                ElseIf btnMid.Visible Then
                    pnlInfoPanel.Height = InfoPanelMidHeight
                    btnUp.Enabled = True
                    btnMid.Enabled = False
                    btnDown.Enabled = True
                    Select Case currThemeType
                        Case Enums.ContentType.Movie
                            _InfoPanelState_Movie = 1
                        Case Enums.ContentType.Movieset
                            _InfoPanelState_Movieset = 1
                        Case Enums.ContentType.TVEpisode
                            _InfoPanelState_TVEpisode = 1
                        Case Enums.ContentType.TVSeason
                            _InfoPanelState_TVSeason = 1
                        Case Enums.ContentType.TVShow
                            _InfoPanelState_TVShow = 1
                    End Select
                Else
                    pnlInfoPanel.Height = 32
                    btnUp.Enabled = True
                    btnMid.Enabled = True
                    btnDown.Enabled = False
                    Select Case currThemeType
                        Case Enums.ContentType.Movie
                            _InfoPanelState_Movie = 0
                        Case Enums.ContentType.Movieset
                            _InfoPanelState_Movieset = 0
                        Case Enums.ContentType.TVEpisode
                            _InfoPanelState_TVEpisode = 0
                        Case Enums.ContentType.TVSeason
                            _InfoPanelState_TVSeason = 0
                        Case Enums.ContentType.TVShow
                            _InfoPanelState_TVShow = 0
                    End Select
                End If
            Case Else
                pnlInfoPanel.Height = 32
                btnUp.Enabled = True
                btnMid.Enabled = True
                btnDown.Enabled = False
                Select Case currThemeType
                    Case Enums.ContentType.Movie
                        _InfoPanelState_Movie = 0
                    Case Enums.ContentType.Movieset
                        _InfoPanelState_Movieset = 0
                    Case Enums.ContentType.TVEpisode
                        _InfoPanelState_TVEpisode = 0
                    Case Enums.ContentType.TVSeason
                        _InfoPanelState_TVSeason = 0
                    Case Enums.ContentType.TVShow
                        _InfoPanelState_TVShow = 0
                End Select
        End Select

        pbActorsLoad.Visible = False
        pbActors.Image = My.Resources.actor_silhouette
        pbGuestStarsLoad.Visible = False
        pbGuestStars.Image = My.Resources.actor_silhouette

        pnlInfoPanel.ResumeLayout()
    End Sub
















    'Public Sub LoadMedia(ByVal Scan As Scanner.ScanOrCleanOptions, Optional ByVal SourceID As Long = -1, Optional ByVal Folder As String = "")
    '    Try
    '        SetStatus(Master.eLang.GetString(116, "Performing Preliminary Tasks (Gathering Data)..."))
    '        tspbLoading.ProgressBar.Style = ProgressBarStyle.Marquee
    '        tspbLoading.Visible = True

    '        Application.DoEvents()

    '        SetControlsEnabled(False)

    '        fScanner.CancelAndWait()

    '        If Scan.Moviesets Then
    '            prevRow_MovieSet = -1
    '            dgvMoviesets.DataSource = Nothing
    '        End If

    '        fScanner.Start(Scan, SourceID, Folder)

    '    Catch ex As Exception
    '        LoadingDone = True
    '        FilterPanel_IsEnabled_Movie(True)
    '        FilterPanel_IsEnabled_Movieset(True)
    '        FilterPanel_IsEnabled_TVShow(True)
    '        SetControlsEnabled(True)
    '        _Logger.Error(ex, New StackFrame().GetMethod().Name)
    '    End Try
    'End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainHelpAbout.Click
        Using dAbout As New dlgAbout
            dAbout.ShowDialog()
        End Using
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        btnCancel.Visible = False
        lblCanceling.Visible = True
        prbCanceling.Visible = True

        If bwMovieScraper.IsBusy Then bwMovieScraper.CancelAsync()
        If bwMoviesetScraper.IsBusy Then bwMoviesetScraper.CancelAsync()
        If bwReload_Movie.IsBusy Then bwReload_Movie.CancelAsync()
        If bwReload_Movieset.IsBusy Then bwReload_Movieset.CancelAsync()
        If bwReload_TVShow.IsBusy Then bwReload_TVShow.CancelAsync()
        If bwRewriteContent.IsBusy Then bwRewriteContent.CancelAsync()
        If bwTVEpisodeScraper.IsBusy Then bwTVEpisodeScraper.CancelAsync()
        If bwTVScraper.IsBusy Then bwTVScraper.CancelAsync()
        If bwTVSeasonScraper.IsBusy Then bwTVSeasonScraper.CancelAsync()
        While bwMovieScraper.IsBusy OrElse bwReload_Movie.IsBusy OrElse bwMoviesetScraper.IsBusy OrElse bwReload_Movieset.IsBusy OrElse
            bwReload_TVShow.IsBusy OrElse bwRewriteContent.IsBusy OrElse bwTVEpisodeScraper.IsBusy OrElse bwTVScraper.IsBusy OrElse
            bwTVSeasonScraper.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub btnClearFilters_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearFilters_Movies.Click
        Filter_Remove_Movie(True)
    End Sub

    Private Sub btnClearFilters_MovieSets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearFilters_MovieSets.Click
        Filter_Remove_Movieset(True)
    End Sub

    Private Sub btnClearFilters_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearFilters_Shows.Click
        Filter_Remove_TVShow(True)
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDown.Click
        tcMain.Focus()
        Select Case currThemeType
            Case Enums.ContentType.Movie
                _InfoPanelState_Movie = 0
            Case Enums.ContentType.Movieset
                _InfoPanelState_Movieset = 0
            Case Enums.ContentType.TVEpisode
                _InfoPanelState_TVEpisode = 0
            Case Enums.ContentType.TVSeason
                _InfoPanelState_TVSeason = 0
            Case Enums.ContentType.TVShow
                _InfoPanelState_TVShow = 0
        End Select
        MoveInfoPanel()
    End Sub

    Private Sub btnFilterDown_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterDown_Movies.Click
        _FilterPanelIsRaised_Movie = False
        FilterPanel_Move_Movie()
    End Sub

    Private Sub btnFilterDown_MovieSets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterDown_MovieSets.Click
        _FilterPanelIsRaised_Movieset = False
        FilterPanel_Move_Movieset()
    End Sub

    Private Sub btnFilterDown_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterDown_Shows.Click
        _FilterPanelIsRaised_TVShow = False
        FilterPanel_Move_TVShow()
    End Sub

    Private Sub btnFilterUp_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterUp_Movies.Click
        _FilterPanelIsRaised_Movie = True
        FilterPanel_Move_Movie()
    End Sub

    Private Sub btnFilterUp_MovieSets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterUp_MovieSets.Click

        _FilterPanelIsRaised_Movieset = True
        FilterPanel_Move_Movieset()
    End Sub

    Private Sub btnFilterUp_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterUp_Shows.Click
        _FilterPanelIsRaised_TVShow = True
        FilterPanel_Move_TVShow()
    End Sub

    Private Sub btnMarkAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMarkAll.Click
        Dim currMainTabTag = AddonsManager.Instance.RuntimeObjects.MediaTabSelected
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetMarkedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = currMainTabTag.ContentType,
                               .SelectionType = Enums.SelectionType.All
                               })
    End Sub

    Private Sub btnUnmarkAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUnmarkAll.Click
        Dim currMainTabTag = AddonsManager.Instance.RuntimeObjects.MediaTabSelected
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetMarkedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = currMainTabTag.ContentType,
                               .SelectionType = Enums.SelectionType.All
                               })
    End Sub

    Private Sub btnMid_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMid.Click
        tcMain.Focus()
        Select Case currThemeType
            Case Enums.ContentType.Movie
                _InfoPanelState_Movie = 1
            Case Enums.ContentType.Movieset
                _InfoPanelState_Movieset = 1
            Case Enums.ContentType.TVEpisode
                _InfoPanelState_TVEpisode = 1
            Case Enums.ContentType.TVSeason
                _InfoPanelState_TVSeason = 1
            Case Enums.ContentType.TVShow
                _InfoPanelState_TVShow = 1
        End Select
        MoveInfoPanel()
    End Sub

    Private Sub btnMetaDataRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMetaDataRefresh.Click
        Dim currMainTabTag = MainTab_GetCurrentTag()

        If currMainTabTag.ContentType = Enums.ContentType.Movie Then
            If dgvMovies.SelectedRows.Count = 1 Then
                Dim ScrapeModifiers As New Structures.ScrapeModifiers
                Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainMetadata, True)
                TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                                       .ContentType = currMainTabTag.ContentType,
                                       .ScrapeModifiers = ScrapeModifiers,
                                       .ScrapeType = Enums.ScrapeType.Auto,
                                       .SelectionType = Enums.SelectionType.Selected
                                       })
            End If
        ElseIf currMainTabTag.ContentType = Enums.ContentType.TV Then
            If dgvTVEpisodes.SelectedRows.Count = 1 AndAlso _CurrDBElement.FileItemSpecified Then
                Dim ScrapeModifiers As New Structures.ScrapeModifiers
                Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeMetadata, True)
                TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                                       .ContentType = currMainTabTag.ContentType,
                                       .ScrapeModifiers = ScrapeModifiers,
                                       .ScrapeType = Enums.ScrapeType.Auto,
                                       .SelectionType = Enums.SelectionType.Selected
                                       })
            End If
        End If
    End Sub

    Public Sub ClearInfo()
        If bwDownloadPic.IsBusy Then bwDownloadPic.CancelAsync()
        If bwDownloadGuestStarPic.IsBusy Then bwDownloadGuestStarPic.CancelAsync()
        If bwLoadImages.IsBusy Then bwLoadImages.CancelAsync()
        If bwLoadImages_MovieSetMoviePosters.IsBusy Then bwLoadImages_MovieSetMoviePosters.CancelAsync()

        While bwDownloadPic.IsBusy OrElse
            bwDownloadGuestStarPic.IsBusy OrElse
            bwLoadImages.IsBusy OrElse
            bwLoadImages_MovieSetMoviePosters.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        If pbFanart.Image IsNot Nothing Then
            pbFanart.Image.Dispose()
            pbFanart.Image = Nothing
        End If
        MainFanart.Clear()

        If pbBanner.Image IsNot Nothing Then
            pbBanner.Image.Dispose()
            pbBanner.Image = Nothing
        End If
        pnlBanner.Visible = False
        MainBanner.Clear()

        If pbCharacterArt.Image IsNot Nothing Then
            pbCharacterArt.Image.Dispose()
            pbCharacterArt.Image = Nothing
        End If
        pnlCharacterArt.Visible = False
        MainCharacterArt.Clear()

        If pbClearArt.Image IsNot Nothing Then
            pbClearArt.Image.Dispose()
            pbClearArt.Image = Nothing
        End If
        pnlClearArt.Visible = False
        MainClearArt.Clear()

        If pbClearLogo.Image IsNot Nothing Then
            pbClearLogo.Image.Dispose()
            pbClearLogo.Image = Nothing
        End If
        pnlClearLogo.Visible = False
        MainClearLogo.Clear()

        If pbPoster.Image IsNot Nothing Then
            pbPoster.Image.Dispose()
            pbPoster.Image = Nothing
        End If
        pnlPoster.Visible = False
        MainPoster.Clear()

        If pbFanartSmall.Image IsNot Nothing Then
            pbFanartSmall.Image.Dispose()
            pbFanartSmall.Image = Nothing
        End If
        pnlFanartSmall.Visible = False
        MainFanartSmall.Clear()

        If pbKeyArt.Image IsNot Nothing Then
            pbKeyArt.Image.Dispose()
            pbKeyArt.Image = Nothing
        End If
        pnlKeyArt.Visible = False
        MainKeyArt.Clear()

        If pbLandscape.Image IsNot Nothing Then
            pbLandscape.Image.Dispose()
            pbLandscape.Image = Nothing
        End If
        pnlLandscape.Visible = False
        MainLandscape.Clear()

        If pbDiscArt.Image IsNot Nothing Then
            pbDiscArt.Image.Dispose()
            pbDiscArt.Image = Nothing
        End If
        pnlDiscArt.Visible = False
        MainDiscArt.Clear()

        'remove all current genres
        Try
            For iDel As Integer = 0 To pnlGenre.Count - 1
                scMain.Panel2.Controls.Remove(pbGenre(iDel))
                scMain.Panel2.Controls.Remove(pnlGenre(iDel))
            Next
        Catch
        End Try

        If pbMPAA.Image IsNot Nothing Then
            pbMPAA.Image = Nothing
        End If
        pnlMPAA.Visible = False

        lblBannerSize.Text = String.Empty
        lblCertifications.Text = String.Empty
        lblCharacterArtSize.Text = String.Empty
        lblClearArtSize.Text = String.Empty
        lblClearLogoSize.Text = String.Empty
        lblCollections.Text = String.Empty
        lblCountries.Text = String.Empty
        lblCredits.Text = String.Empty
        lblDirectors.Text = String.Empty
        lblDiscArtSize.Text = String.Empty
        lblFanartSmallSize.Text = String.Empty
        lblIMDBHeader.Tag = Nothing
        lblLandscapeSize.Text = String.Empty
        lblOriginalTitle.Text = String.Empty
        lblPosterSize.Text = String.Empty
        lblRating.Text = String.Empty
        lblPremiered.Text = String.Empty
        lblRuntime.Text = String.Empty
        lblStatus.Text = String.Empty
        lblStudio.Text = String.Empty
        lblTagline.Text = String.Empty
        lblTags.Text = String.Empty
        lblTitle.Text = String.Empty
        lblTMDBHeader.Tag = Nothing
        lblTVDBHeader.Tag = Nothing
        txtFilePath.Text = String.Empty
        txtIMDBID.Text = String.Empty
        txtOutline.Text = String.Empty
        txtPlot.Text = String.Empty
        txtTMDBID.Text = String.Empty
        txtTVDBID.Text = String.Empty
        txtTrailerPath.Text = String.Empty
        pbStar1.Image = Nothing
        pbStar2.Image = Nothing
        pbStar3.Image = Nothing
        pbStar4.Image = Nothing
        pbStar5.Image = Nothing
        pbStar6.Image = Nothing
        pbStar7.Image = Nothing
        pbStar8.Image = Nothing
        pbStar9.Image = Nothing
        pbStar10.Image = Nothing
        ToolTips.SetToolTip(pbStar1, String.Empty)
        ToolTips.SetToolTip(pbStar2, String.Empty)
        ToolTips.SetToolTip(pbStar3, String.Empty)
        ToolTips.SetToolTip(pbStar4, String.Empty)
        ToolTips.SetToolTip(pbStar5, String.Empty)
        ToolTips.SetToolTip(pbStar6, String.Empty)
        ToolTips.SetToolTip(pbStar7, String.Empty)
        ToolTips.SetToolTip(pbStar8, String.Empty)
        ToolTips.SetToolTip(pbStar9, String.Empty)
        ToolTips.SetToolTip(pbStar10, String.Empty)

        lstActors.Items.Clear()
        If alActors IsNot Nothing Then
            alActors.Clear()
            alActors = Nothing
        End If
        If pbActors.Image IsNot Nothing Then
            pbActors.Image.Dispose()
            pbActors.Image = Nothing
        End If
        MainActors.Clear()

        lstGuestStars.Items.Clear()
        If alGuestStars IsNot Nothing Then
            alGuestStars.Clear()
            alGuestStars = Nothing
        End If
        If pbGuestStars.Image IsNot Nothing Then
            pbGuestStars.Image.Dispose()
            pbGuestStars.Image = Nothing
        End If
        MainGuestStars.Clear()

        If pbMPAA.Image IsNot Nothing Then
            pbMPAA.Image.Dispose()
            pbMPAA.Image = Nothing
        End If
        pbStudio.Image = Nothing
        pbVideoChannels.Image = Nothing
        pbVideoSource.Image = Nothing
        pbVideoCodec.Image = Nothing
        pbAudioCodec.Image = Nothing
        pbVideoResolution.Image = Nothing
        pbAudioChannels.Image = Nothing
        pbAudioLang0.Image = Nothing
        pbAudioLang1.Image = Nothing
        pbAudioLang2.Image = Nothing
        pbAudioLang3.Image = Nothing
        pbAudioLang4.Image = Nothing
        pbAudioLang5.Image = Nothing
        pbAudioLang6.Image = Nothing
        ToolTips.SetToolTip(pbAudioLang0, String.Empty)
        ToolTips.SetToolTip(pbAudioLang1, String.Empty)
        ToolTips.SetToolTip(pbAudioLang2, String.Empty)
        ToolTips.SetToolTip(pbAudioLang3, String.Empty)
        ToolTips.SetToolTip(pbAudioLang4, String.Empty)
        ToolTips.SetToolTip(pbAudioLang5, String.Empty)
        ToolTips.SetToolTip(pbAudioLang6, String.Empty)
        pbSubtitleLang0.Image = Nothing
        pbSubtitleLang1.Image = Nothing
        pbSubtitleLang2.Image = Nothing
        pbSubtitleLang3.Image = Nothing
        pbSubtitleLang4.Image = Nothing
        pbSubtitleLang5.Image = Nothing
        pbSubtitleLang6.Image = Nothing
        ToolTips.SetToolTip(pbSubtitleLang0, String.Empty)
        ToolTips.SetToolTip(pbSubtitleLang1, String.Empty)
        ToolTips.SetToolTip(pbSubtitleLang2, String.Empty)
        ToolTips.SetToolTip(pbSubtitleLang3, String.Empty)
        ToolTips.SetToolTip(pbSubtitleLang4, String.Empty)
        ToolTips.SetToolTip(pbSubtitleLang5, String.Empty)
        ToolTips.SetToolTip(pbSubtitleLang6, String.Empty)

        txtMetaData.Text = String.Empty

        lvMoviesInSet.Items.Clear()
        ilMoviesInSet.Images.Clear()

        InfoCleared = True

        Application.DoEvents()
    End Sub
    ''' <summary>
    ''' Launch video using system default player
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPlay_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilePlay.Click
        If Not String.IsNullOrEmpty(txtFilePath.Text) Then
            Dim nFile = New FileItem(txtFilePath.Text)
            If nFile.bIsOnline Then Functions.Launch(nFile.FirstPathFromStack, True)
        End If
    End Sub
    ''' <summary>
    ''' Launch trailer using system default player
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTrailerPlay_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTrailerPlay.Click
        If txtTrailerPath.Text.StartsWith("plugin://plugin.video.youtube") Then
            Functions.Launch(StringUtils.ConvertFromKodiTrailerFormatToYouTubeURL(txtTrailerPath.Text), True)
        Else
            Functions.Launch(txtTrailerPath.Text, True)
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by adding date
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>this filter is inverted (DESC first) to get the newest title on the top of the list</remarks>
    Private Sub btnFilterSortDateAdded_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterSortDateAdded_Movies.Click
        If dgvMovies.RowCount > 0 Then
            btnFilterSortRating_Movies.Tag = String.Empty
            btnFilterSortRating_Movies.Image = Nothing
            btnFilterSortDateModified_Movies.Tag = String.Empty
            btnFilterSortDateModified_Movies.Image = Nothing
            btnFilterSortReleaseDate_Movies.Tag = String.Empty
            btnFilterSortReleaseDate_Movies.Image = Nothing
            btnFilterSortTitle_Movies.Tag = String.Empty
            btnFilterSortTitle_Movies.Image = Nothing
            btnFilterSortYear_Movies.Tag = String.Empty
            btnFilterSortYear_Movies.Image = Nothing
            If btnFilterSortDateAdded_Movies.Tag.ToString = "DESC" Then
                btnFilterSortDateAdded_Movies.Tag = "ASC"
                btnFilterSortDateAdded_Movies.Image = My.Resources.asc
                dgvMovies.Sort(dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DateAdded)), ComponentModel.ListSortDirection.Ascending)
            Else
                btnFilterSortDateAdded_Movies.Tag = "DESC"
                btnFilterSortDateAdded_Movies.Image = My.Resources.desc
                dgvMovies.Sort(dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DateAdded)), ComponentModel.ListSortDirection.Descending)
            End If

            DataGridView_SortingSave_Movie()
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by last modification date
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>this filter is inverted (DESC first) to get the latest modified title on the top of the list</remarks>
    Private Sub btnFilterSortDateModified_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterSortDateModified_Movies.Click
        If dgvMovies.RowCount > 0 Then
            btnFilterSortDateAdded_Movies.Tag = String.Empty
            btnFilterSortDateAdded_Movies.Image = Nothing
            btnFilterSortRating_Movies.Tag = String.Empty
            btnFilterSortRating_Movies.Image = Nothing
            btnFilterSortReleaseDate_Movies.Tag = String.Empty
            btnFilterSortReleaseDate_Movies.Image = Nothing
            btnFilterSortTitle_Movies.Tag = String.Empty
            btnFilterSortTitle_Movies.Image = Nothing
            btnFilterSortYear_Movies.Tag = String.Empty
            btnFilterSortYear_Movies.Image = Nothing
            If btnFilterSortDateModified_Movies.Tag.ToString = "DESC" Then
                btnFilterSortDateModified_Movies.Tag = "ASC"
                btnFilterSortDateModified_Movies.Image = My.Resources.asc
                dgvMovies.Sort(dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DateModified)), System.ComponentModel.ListSortDirection.Ascending)
            Else
                btnFilterSortDateModified_Movies.Tag = "DESC"
                btnFilterSortDateModified_Movies.Image = My.Resources.desc
                dgvMovies.Sort(dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.DateModified)), System.ComponentModel.ListSortDirection.Descending)
            End If

            DataGridView_SortingSave_Movie()
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by sort title
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFilterSortTitle_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterSortTitle_Movies.Click
        If dgvMovies.RowCount > 0 Then
            btnFilterSortDateAdded_Movies.Tag = String.Empty
            btnFilterSortDateAdded_Movies.Image = Nothing
            btnFilterSortDateModified_Movies.Tag = String.Empty
            btnFilterSortDateModified_Movies.Image = Nothing
            btnFilterSortRating_Movies.Tag = String.Empty
            btnFilterSortRating_Movies.Image = Nothing
            btnFilterSortReleaseDate_Movies.Tag = String.Empty
            btnFilterSortReleaseDate_Movies.Image = Nothing
            btnFilterSortYear_Movies.Tag = String.Empty
            btnFilterSortYear_Movies.Image = Nothing
            If btnFilterSortTitle_Movies.Tag.ToString = "ASC" Then
                btnFilterSortTitle_Movies.Tag = "DSC"
                btnFilterSortTitle_Movies.Image = My.Resources.desc
                dgvMovies.Sort(dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SortedTitle)), System.ComponentModel.ListSortDirection.Descending)
            Else
                btnFilterSortTitle_Movies.Tag = "ASC"
                btnFilterSortTitle_Movies.Image = My.Resources.asc
                dgvMovies.Sort(dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SortedTitle)), System.ComponentModel.ListSortDirection.Ascending)
            End If

            DataGridView_SortingSave_Movie()
        End If
    End Sub
    ''' <summary>
    ''' sorts the tvshowlist by sort title
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFilterSortTitle_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterSortTitle_Shows.Click
        If dgvTVShows.RowCount > 0 Then
            If btnFilterSortTitle_Shows.Tag.ToString = "ASC" Then
                btnFilterSortTitle_Shows.Tag = "DSC"
                btnFilterSortTitle_Shows.Image = My.Resources.desc
                dgvTVShows.Sort(dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SortedTitle)), System.ComponentModel.ListSortDirection.Descending)
            Else
                btnFilterSortTitle_Shows.Tag = "ASC"
                btnFilterSortTitle_Shows.Image = My.Resources.asc
                dgvTVShows.Sort(dgvTVShows.Columns(Database.Helpers.GetColumnName(Database.ColumnName.SortedTitle)), System.ComponentModel.ListSortDirection.Ascending)
            End If

            DataGridView_SortingSave_TVShow()
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by rating
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>this filter is inverted (DESC first) to get the highest rated title on the top of the list</remarks>
    Private Sub btnFilterSortRating_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterSortRating_Movies.Click
        If dgvMovies.RowCount > 0 Then 'TODO: make it works with new ratings container
            btnFilterSortDateAdded_Movies.Tag = String.Empty
            btnFilterSortDateAdded_Movies.Image = Nothing
            btnFilterSortDateModified_Movies.Tag = String.Empty
            btnFilterSortDateModified_Movies.Image = Nothing
            btnFilterSortReleaseDate_Movies.Tag = String.Empty
            btnFilterSortReleaseDate_Movies.Image = Nothing
            btnFilterSortTitle_Movies.Tag = String.Empty
            btnFilterSortTitle_Movies.Image = Nothing
            btnFilterSortYear_Movies.Tag = String.Empty
            btnFilterSortYear_Movies.Image = Nothing
            If btnFilterSortRating_Movies.Tag.ToString = "DESC" Then
                btnFilterSortRating_Movies.Tag = "ASC"
                btnFilterSortRating_Movies.Image = My.Resources.asc
                dgvMovies.Sort(dgvMovies.Columns("Rating"), System.ComponentModel.ListSortDirection.Ascending)
            Else
                btnFilterSortRating_Movies.Tag = "DESC"
                btnFilterSortRating_Movies.Image = My.Resources.desc
                dgvMovies.Sort(dgvMovies.Columns("Rating"), System.ComponentModel.ListSortDirection.Descending)
            End If

            DataGridView_SortingSave_Movie()
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by release date
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>this filter is inverted (DESC first) to get the highest year title on the top of the list</remarks>
    Private Sub btnFilterSortReleaseDate_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterSortReleaseDate_Movies.Click
        If dgvMovies.RowCount > 0 Then
            btnFilterSortDateAdded_Movies.Tag = String.Empty
            btnFilterSortDateAdded_Movies.Image = Nothing
            btnFilterSortDateModified_Movies.Tag = String.Empty
            btnFilterSortDateModified_Movies.Image = Nothing
            btnFilterSortRating_Movies.Tag = String.Empty
            btnFilterSortRating_Movies.Image = Nothing
            btnFilterSortTitle_Movies.Tag = String.Empty
            btnFilterSortTitle_Movies.Image = Nothing
            btnFilterSortYear_Movies.Tag = String.Empty
            btnFilterSortYear_Movies.Image = Nothing
            If btnFilterSortReleaseDate_Movies.Tag.ToString = "DESC" Then
                btnFilterSortReleaseDate_Movies.Tag = "ASC"
                btnFilterSortReleaseDate_Movies.Image = My.Resources.asc
                dgvMovies.Sort(dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ReleaseDate)), System.ComponentModel.ListSortDirection.Ascending)
            Else
                btnFilterSortReleaseDate_Movies.Tag = "DESC"
                btnFilterSortReleaseDate_Movies.Image = My.Resources.desc
                dgvMovies.Sort(dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.ReleaseDate)), System.ComponentModel.ListSortDirection.Descending)
            End If

            DataGridView_SortingSave_Movie()
        End If
    End Sub
    ''' <summary>
    ''' sorts the movielist by year
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>this filter is inverted (DESC first) to get the highest year title on the top of the list</remarks>
    Private Sub btnFilterSortYear_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterSortYear_Movies.Click
        If dgvMovies.RowCount > 0 Then
            btnFilterSortDateAdded_Movies.Tag = String.Empty
            btnFilterSortDateAdded_Movies.Image = Nothing
            btnFilterSortDateModified_Movies.Tag = String.Empty
            btnFilterSortDateModified_Movies.Image = Nothing
            btnFilterSortRating_Movies.Tag = String.Empty
            btnFilterSortRating_Movies.Image = Nothing
            btnFilterSortReleaseDate_Movies.Tag = String.Empty
            btnFilterSortReleaseDate_Movies.Image = Nothing
            btnFilterSortTitle_Movies.Tag = String.Empty
            btnFilterSortTitle_Movies.Image = Nothing
            If btnFilterSortYear_Movies.Tag.ToString = "DESC" Then
                btnFilterSortYear_Movies.Tag = "ASC"
                btnFilterSortYear_Movies.Image = My.Resources.asc
                dgvMovies.Sort(dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Year)), System.ComponentModel.ListSortDirection.Ascending)
            Else
                btnFilterSortYear_Movies.Tag = "DESC"
                btnFilterSortYear_Movies.Image = My.Resources.desc
                dgvMovies.Sort(dgvMovies.Columns(Database.Helpers.GetColumnName(Database.ColumnName.Year)), System.ComponentModel.ListSortDirection.Descending)
            End If

            DataGridView_SortingSave_Movie()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUp.Click
        tcMain.Focus()
        Select Case currThemeType
            Case Enums.ContentType.Movie
                _InfoPanelState_Movie = 2
            Case Enums.ContentType.Movieset
                _InfoPanelState_Movieset = 2
            Case Enums.ContentType.TVEpisode
                _InfoPanelState_TVEpisode = 2
            Case Enums.ContentType.TVSeason
                _InfoPanelState_TVSeason = 2
            Case Enums.ContentType.TVShow
                _InfoPanelState_TVShow = 2
        End Select
        MoveInfoPanel()
    End Sub

    Private Sub bwCleanDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCleanDB.DoWork
        Dim Args As Scanner.ScanOrCleanOptions = DirectCast(e.Argument, Scanner.ScanOrCleanOptions)
        Master.DB.Clean(Args.Movies, Args.Moviesets, Args.TV)
    End Sub

    Private Sub bwCleanDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCleanDB.RunWorkerCompleted
        SetStatus(String.Empty)
        tspbLoading.Visible = False

        DataGridView_Fill_Main(True, True, True)
    End Sub

    Private Sub bwDownloadPic_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadPic.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Try
            sHTTP.StartDownloadImage(Args.pURL)

            While sHTTP.IsDownloading
                Application.DoEvents()
                If bwDownloadPic.CancellationPending Then
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
        pbActorsLoad.Visible = False

        If e.Cancelled Then
            pbActors.Image = My.Resources.actor_silhouette
        Else
            Dim Res As Results = DirectCast(e.Result, Results)

            If Res.Result IsNot Nothing Then
                pbActors.Image = Res.Result
            Else
                pbActors.Image = My.Resources.actor_silhouette
            End If
        End If
    End Sub

    Private Sub bwDownloadGuestStarPic_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadGuestStarPic.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Try
            sHTTP.StartDownloadImage(Args.pURL)

            While sHTTP.IsDownloading
                Application.DoEvents()
                If bwDownloadGuestStarPic.CancellationPending Then
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

    Private Sub bwDownloadGuestStarPic_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwDownloadGuestStarPic.RunWorkerCompleted
        pbGuestStarsLoad.Visible = False

        If e.Cancelled Then
            pbGuestStars.Image = My.Resources.actor_silhouette
        Else
            Dim Res As Results = DirectCast(e.Result, Results)

            If Res.Result IsNot Nothing Then
                pbGuestStars.Image = Res.Result
            Else
                pbGuestStars.Image = My.Resources.actor_silhouette
            End If
        End If
    End Sub

    Private Sub bwLoadImages_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadImages.DoWork
        MainActors.Clear()
        MainBanner.Clear()
        MainCharacterArt.Clear()
        MainClearArt.Clear()
        MainClearLogo.Clear()
        MainDiscArt.Clear()
        MainFanart.Clear()
        MainFanartSmall.Clear()
        MainKeyArt.Clear()
        MainLandscape.Clear()
        MainPoster.Clear()

        If bwLoadImages.CancellationPending Then
            e.Cancel = True
            Return
        End If

        _CurrDBElement.LoadAllImages(True, False)

        If bwLoadImages.CancellationPending Then
            e.Cancel = True
            Return
        End If

        With Manager.mSettings.MainOptions.GuiSettings
            If .DisplayBanner Then MainBanner = _CurrDBElement.ImagesContainer.Banner.ImageOriginal
            If .DisplayCharacterArt Then MainCharacterArt = _CurrDBElement.ImagesContainer.CharacterArt.ImageOriginal
            If .DisplayClearArt Then MainClearArt = _CurrDBElement.ImagesContainer.ClearArt.ImageOriginal
            If .DisplayClearLogo Then MainClearLogo = _CurrDBElement.ImagesContainer.ClearLogo.ImageOriginal
            If .DisplayDiscArt Then MainDiscArt = _CurrDBElement.ImagesContainer.DiscArt.ImageOriginal
            If .DisplayFanartAsBackground Then MainFanart = _CurrDBElement.ImagesContainer.Fanart.ImageOriginal
            If .DisplayFanart Then MainFanartSmall = _CurrDBElement.ImagesContainer.Fanart.ImageOriginal
            If .DisplayKeyArt Then MainKeyArt = _CurrDBElement.ImagesContainer.KeyArt.ImageOriginal
            If .DisplayLandscape Then MainLandscape = _CurrDBElement.ImagesContainer.Landscape.ImageOriginal
            If .DisplayPoster Then MainPoster = _CurrDBElement.ImagesContainer.Poster.ImageOriginal
        End With

        If bwLoadImages.CancellationPending Then
            e.Cancel = True
            Return
        End If

        If Manager.mSettings.MainOptions.GuiSettings.DisplayFanartAsBackground AndAlso _CurrDBElement.ContentType = Enums.ContentType.TVEpisode Then
            Dim NeedsGS As Boolean = False
            If _CurrDBElement.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing Then
                MainFanart = _CurrDBElement.ImagesContainer.Fanart.ImageOriginal
            Else
                Dim SeasonID As Long = Master.DB.GetTVSeasonIDFromEpisode(_CurrDBElement)
                Dim TVSeasonFanart As String = Master.DB.GetArtForItem(SeasonID, Enums.ContentType.TVSeason, "fanart")
                If Not String.IsNullOrEmpty(TVSeasonFanart) Then
                    MainFanart.LoadFromFile(TVSeasonFanart, True)
                    NeedsGS = True
                Else
                    Dim TVShowFanart As String = Master.DB.GetArtForItem(_CurrDBElement.ShowID, Enums.ContentType.TVShow, "fanart")
                    If Not String.IsNullOrEmpty(TVShowFanart) Then
                        MainFanart.LoadFromFile(TVShowFanart, True)
                        NeedsGS = True
                    End If
                End If
            End If

            If MainFanart.Image IsNot Nothing Then
                If Not _CurrDBElement.FileItemSpecified Then
                    MainFanart = ImageUtils.AddMissingStamp(MainFanart)
                ElseIf NeedsGS Then
                    MainFanart = ImageUtils.GrayScale(MainFanart)
                End If
            End If
        End If

        If Manager.mSettings.MainOptions.GuiSettings.DisplayFanartAsBackground AndAlso _CurrDBElement.ContentType = Enums.ContentType.TVSeason Then
            Dim NeedsGS As Boolean = False
            If _CurrDBElement.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing Then
                MainFanart = _CurrDBElement.ImagesContainer.Fanart.ImageOriginal
            Else
                Dim TVShowFanart As String = Master.DB.GetArtForItem(_CurrDBElement.ShowID, Enums.ContentType.TVShow, "fanart")
                If Not String.IsNullOrEmpty(TVShowFanart) Then
                    MainFanart.LoadFromFile(TVShowFanart, True)
                    NeedsGS = True
                End If
            End If

            If MainFanart.Image IsNot Nothing AndAlso NeedsGS Then
                MainFanart = ImageUtils.GrayScale(MainFanart)
            End If
        End If

        If bwLoadImages.CancellationPending Then
            e.Cancel = True
            Return
        End If
    End Sub

    Private Sub bwLoadImages_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadImages.RunWorkerCompleted
        If Not e.Cancelled Then
            FillScreenInfoWith_Images()
        End If
    End Sub

    Private Sub bwLoadImages_MovieSetMoviePosters_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadImages_MovieSetMoviePosters.DoWork
        Dim Posters As New List(Of MovieInSetPoster)

        Try
            If _CurrDBElement.MoviesInSetSpecified Then
                Try
                    For Each tMovieInSet As MediaContainers.MovieInSet In _CurrDBElement.MoviesInSet
                        If bwLoadImages_MovieSetMoviePosters.CancellationPending Then
                            e.Cancel = True
                            Return
                        End If

                        Dim ResImg As Image
                        If tMovieInSet.DBMovie.ImagesContainer.Poster.LoadAndCache(Enums.ContentType.Movie, True, True) Then
                            ResImg = tMovieInSet.DBMovie.ImagesContainer.Poster.ImageOriginal.Image
                            ImageUtils.ResizeImage(ResImg, 59, 88, True, Color.White.ToArgb())
                            Posters.Add(New MovieInSetPoster With {.MoviePoster = ResImg, .MovieTitle = tMovieInSet.DBMovie.MainDetails.Title, .MovieYear = StringUtils.GetYearFromString(tMovieInSet.DBMovie.MainDetails.Premiered)})
                        Else
                            Posters.Add(New MovieInSetPoster With {.MoviePoster = My.Resources.noposter, .MovieTitle = tMovieInSet.DBMovie.MainDetails.Title, .MovieYear = StringUtils.GetYearFromString(tMovieInSet.DBMovie.MainDetails.Premiered)})
                        End If
                    Next
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                    e.Result = New Results With {.MovieInSetPosters = Nothing}
                    e.Cancel = True
                End Try
            End If

            e.Result = New Results With {.MovieInSetPosters = Posters}
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            e.Result = New Results With {.MovieInSetPosters = Nothing}
            e.Cancel = True
        End Try
    End Sub

    Private Sub bwLoadImages_MovieSetMoviePosters_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadImages_MovieSetMoviePosters.RunWorkerCompleted
        lvMoviesInSet.Clear()
        ilMoviesInSet.Images.Clear()
        ilMoviesInSet.ImageSize = New Size(59, 88)
        ilMoviesInSet.ColorDepth = ColorDepth.Depth32Bit
        lvMoviesInSet.Visible = False

        If Not e.Cancelled Then
            Try
                Dim Res As Results = DirectCast(e.Result, Results)

                If Res.MovieInSetPosters IsNot Nothing AndAlso Res.MovieInSetPosters.Count > 0 Then
                    lvMoviesInSet.BeginUpdate()
                    For Each tPoster As MovieInSetPoster In Res.MovieInSetPosters
                        If tPoster IsNot Nothing Then
                            ilMoviesInSet.Images.Add(tPoster.MoviePoster)
                            lvMoviesInSet.Items.Add(String.Concat(tPoster.MovieTitle, Environment.NewLine, "(", tPoster.MovieYear, ")"), ilMoviesInSet.Images.Count - 1)
                        End If
                    Next
                    lvMoviesInSet.EndUpdate()
                    lvMoviesInSet.Visible = True
                End If
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
    End Sub

    Private Sub bwMovieScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwMovieScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeMovie As New Database.DBElement(Enums.ContentType.Movie)

        _Logger.Trace(String.Format("[Movie Scraper] [Start] Movies Count [{0}]", Args.ScrapeList.Count.ToString))

        'For Each tScrapeItem As ScrapeItem In Args.ScrapeList
        '    Dim Theme As New MediaContainers.Theme
        '    Dim tURL As String = String.Empty
        '    Dim aUrlList As New List(Of MediaContainers.Trailer)
        '    Dim tUrlList As New List(Of Themes)
        '    Dim OldListTitle As String = String.Empty
        '    Dim NewListTitle As String = String.Empty

        '    Cancelled = False

        '    If bwMovieScraper.CancellationPending Then Exit For
        '    OldListTitle = tScrapeItem.DataRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ToString
        '    bwMovieScraper.ReportProgress(1, OldListTitle)

        '    Dim dScrapeRow As DataRow = tScrapeItem.DataRow

        '    _Logger.Trace(String.Format("[Movie Scraper] [Start] Scraping {0}", OldListTitle))

        '    DBScrapeMovie = Master.DB.Load_Movie(Convert.ToInt64(tScrapeItem.DataRow.Item(Database.Helpers.GetMainIdName(Database.TableName.movie))))

        '    If tScrapeItem.ScrapeModifiers.MainNFO Then
        '        If AddonsManager.Instance.ScrapeData_Movie(DBScrapeMovie, tScrapeItem.ScrapeModifiers, Args.ScrapeType, Args.ScrapeOptions, Args.ScrapeList.Count = 1) Then
        '            _Logger.Trace(String.Format("[Movie Scraper] [Cancelled] Scraping {0}", OldListTitle))
        '            Cancelled = True
        '            If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
        '                bwMovieScraper.CancelAsync()
        '            End If
        '        End If
        '    Else
        '        ' if we do not have the movie ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
        '        If Not DBScrapeMovie.MainDetails.UniqueIDs.IMDbIdSpecified AndAlso (
        '            tScrapeItem.ScrapeModifiers.MainActorthumbs OrElse
        '            tScrapeItem.ScrapeModifiers.MainBanner OrElse
        '            tScrapeItem.ScrapeModifiers.MainClearArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainClearLogo OrElse
        '            tScrapeItem.ScrapeModifiers.MainDiscArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainExtrafanarts OrElse
        '            tScrapeItem.ScrapeModifiers.MainExtrathumbs OrElse
        '            tScrapeItem.ScrapeModifiers.MainFanart OrElse
        '            tScrapeItem.ScrapeModifiers.MainKeyArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainLandscape OrElse
        '            tScrapeItem.ScrapeModifiers.MainPoster OrElse
        '            tScrapeItem.ScrapeModifiers.MainTheme OrElse
        '            tScrapeItem.ScrapeModifiers.MainTrailer) Then
        '            Dim tModifiers As New Structures.ScrapeModifiers With {.MainNFO = True}
        '            Dim tOptions As New Structures.ScrapeOptions 'set all values to false to not override any field. ID's are always determined.
        '            If AddonsManager.Instance.ScrapeData_Movie(DBScrapeMovie, tModifiers, Args.ScrapeType, tOptions, Args.ScrapeList.Count = 1) Then
        '                _Logger.Trace(String.Format("[Movie Scraper] [Cancelled] Scraping {0}", OldListTitle))
        '                Cancelled = True
        '                If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
        '                    bwMovieScraper.CancelAsync()
        '                End If
        '            End If
        '        End If
        '    End If

        '    If bwMovieScraper.CancellationPending Then Exit For

        '    If Not Cancelled Then
        '        If Master.eSettings.Movie.DataSettings.MetadataScan.Enabled AndAlso tScrapeItem.ScrapeModifiers.MainMeta Then
        '            bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(140, "Scanning Metadata"), ":"))
        '            MetaData.UpdateFileInfo(DBScrapeMovie)
        '        End If
        '        If bwMovieScraper.CancellationPending Then Exit For

        '        NewListTitle = StringUtils.SortTokens(DBScrapeMovie.MainDetails.Title)

        '        If Not NewListTitle = OldListTitle Then
        '            bwMovieScraper.ReportProgress(0, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle))
        '        End If

        '        'get all images 
        '        If tScrapeItem.ScrapeModifiers.MainBanner OrElse
        '            tScrapeItem.ScrapeModifiers.MainClearArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainClearLogo OrElse
        '            tScrapeItem.ScrapeModifiers.MainDiscArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainExtrafanarts OrElse
        '            tScrapeItem.ScrapeModifiers.MainExtrathumbs OrElse
        '            tScrapeItem.ScrapeModifiers.MainFanart OrElse
        '            tScrapeItem.ScrapeModifiers.MainKeyArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainLandscape OrElse
        '            tScrapeItem.ScrapeModifiers.MainPoster Then

        '            Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
        '            bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(254, "Scraping Images"), ":"))
        '            If Not AddonsManager.Instance.ScrapeImage_Movie(DBScrapeMovie, SearchResultsContainer, tScrapeItem.ScrapeModifiers, Args.ScrapeList.Count = 1) Then
        '                If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.MovieImagesDisplayImageSelect Then
        '                    Using dImgSelect As New dlgImageSelect
        '                        If dImgSelect.ShowDialog(DBScrapeMovie, SearchResultsContainer) = DialogResult.OK Then
        '                            Images.SetPreferredImages(DBScrapeMovie, dImgSelect.Result)
        '                        End If
        '                    End Using
        '                Else
        '                    'autoscraping
        '                    Images.SetPreferredImages(DBScrapeMovie, SearchResultsContainer, tScrapeItem.ScrapeModifiers)
        '                End If
        '            End If
        '        End If

        '        If bwMovieScraper.CancellationPending Then Exit For

        '        'Theme
        '        If tScrapeItem.ScrapeModifiers.MainTheme Then
        '            bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(266, "Scraping Themes"), ":"))
        '            Dim SearchResults As New List(Of MediaContainers.Theme)
        '            If Not AddonsManager.Instance.ScrapeTheme_Movie(DBScrapeMovie, Enums.ModifierType.MainTheme, SearchResults) Then
        '                If Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
        '                    Using dThemeSelect As New dlgThemeSelect
        '                        If dThemeSelect.ShowDialog(DBScrapeMovie, SearchResults, AdvancedSettings.GetBooleanSetting("UseAsVideoPlayer", False, "generic.EmberCore.VLCPlayer")) = DialogResult.OK Then
        '                            DBScrapeMovie.Theme = dThemeSelect.Result
        '                        End If
        '                    End Using

        '                    'autoscraping
        '                ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
        '                    Dim newPreferredTheme As New MediaContainers.Theme
        '                    If Themes.GetPreferred(SearchResults, newPreferredTheme, Enums.ContentType.Movie) Then
        '                        DBScrapeMovie.Theme = newPreferredTheme
        '                    End If
        '                End If
        '            End If
        '        End If

        '        If bwMovieScraper.CancellationPending Then Exit For

        '        'Trailer
        '        If tScrapeItem.ScrapeModifiers.MainTrailer Then
        '            bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(574, "Scraping Trailers"), ":"))
        '            Dim SearchResults As New List(Of MediaContainers.Trailer)
        '            If Not AddonsManager.Instance.ScrapeTrailer_Movie(DBScrapeMovie, Enums.ModifierType.MainTrailer, SearchResults) Then
        '                If Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
        '                    Using dTrailerSelect As New dlgTrailerSelect
        '                        'note msavazzi why is always False with Player? If dTrailerSelect.ShowDialog(DBScrapeMovie, SearchResults, False, True, False) = DialogResult.OK Then
        '                        'DanCooper: the VLC COM interface is/was not able to call in multithread
        '                        If dTrailerSelect.ShowDialog(DBScrapeMovie, SearchResults, False, True, AdvancedSettings.GetBooleanSetting("UseAsVideoPlayer", False, "generic.EmberCore.VLCPlayer")) = DialogResult.OK Then
        '                            DBScrapeMovie.Trailer = dTrailerSelect.Result
        '                        End If
        '                    End Using

        '                    'autoscraping
        '                ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
        '                    Dim newPreferredTrailer As New MediaContainers.Trailer
        '                    If Trailers.GetPreferred(SearchResults, newPreferredTrailer, Enums.ContentType.Movie) Then
        '                        DBScrapeMovie.Trailer = newPreferredTrailer
        '                    End If
        '                End If
        '            End If
        '        End If

        '        If bwMovieScraper.CancellationPending Then Exit For

        '        If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
        '            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.ScraperMulti_Movie, Nothing, Nothing, False, DBScrapeMovie)
        '            bwMovieScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
        '            Master.DB.Save_Movie(DBScrapeMovie, False, tScrapeItem.ScrapeModifiers.MainNFO OrElse tScrapeItem.ScrapeModifiers.MainMeta, True, True, False)
        '            bwMovieScraper.ReportProgress(-2, DBScrapeMovie.ID)
        '            bwMovieScraper.ReportProgress(-1, If(Not OldListTitle = NewListTitle, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle), NewListTitle))
        '        End If
        '        _Logger.Trace(String.Format("[Movie Scraper] [Done] Scraping {0}", OldListTitle))
        '    Else
        '        _Logger.Trace(String.Format("[Movie Scraper] [Cancelled] Scraping {0}", OldListTitle))
        '    End If
        'Next

        e.Result = New Results With {.DBElement = DBScrapeMovie, .ScrapeType = Args.ScrapeType, .Cancelled = bwMovieScraper.CancellationPending}
        _Logger.Trace(String.Format("[Movie Scraper] [Done] Scraping"))
    End Sub

    Private Sub bwMovieSetScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwMoviesetScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeMovieSet As New Database.DBElement(Enums.ContentType.Movieset)

        _Logger.Trace(String.Format("[MovieSet Scraper] [Start] MovieSets Count [{0}]", Args.ScrapeList.Count.ToString))

        'For Each tScrapeItem As ScrapeItem In Args.ScrapeList
        '    Dim aContainer As New MediaContainers.SearchResultsContainer
        '    Dim NewListTitle As String = String.Empty
        '    Dim NewTMDBColID As String = String.Empty
        '    Dim NewTitle As String = String.Empty
        '    Dim OldListTitle As String = String.Empty
        '    Dim OldTitle As String = String.Empty
        '    Dim efList As New List(Of String)
        '    Dim etList As New List(Of String)
        '    Dim tURL As String = String.Empty

        '    Cancelled = False

        '    If bwMoviesetScraper.CancellationPending Then Exit For
        '    OldListTitle = tScrapeItem.DataRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ToString
        '    OldTitle = tScrapeItem.DataRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Title)).ToString
        '    bwMoviesetScraper.ReportProgress(1, OldListTitle)

        '    Dim dScrapeRow As DataRow = tScrapeItem.DataRow

        '    _Logger.Trace(String.Format("[MovieSet Scraper] [Start] Scraping {0}", OldListTitle))

        '    DBScrapeMovieSet = Master.DB.Load_Movieset(Convert.ToInt64(tScrapeItem.DataRow.Item(Database.Helpers.GetMainIdName(Database.TableName.movieset))))

        '    'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEditMovieSet, Nothing, DBScrapeMovieSet)

        '    If tScrapeItem.ScrapeModifiers.MainNFO Then
        '        bwMoviesetScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(253, "Scraping Data"), ":"))
        '        If AddonsManager.Instance.ScrapeData_MovieSet(DBScrapeMovieSet, tScrapeItem.ScrapeModifiers, Args.ScrapeType, Args.ScrapeOptions, Args.ScrapeList.Count = 1) Then
        '            _Logger.Trace(String.Format("[MovieSet Scraper] [Cancelled] Scraping {0}", OldListTitle))
        '            Cancelled = True
        '            If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
        '                bwMoviesetScraper.CancelAsync()
        '            End If
        '        End If
        '    Else
        '        ' if we do not have the movie set ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
        '        If Not DBScrapeMovieSet.MainDetails.UniqueIDs.TMDbIdSpecified AndAlso (
        '            tScrapeItem.ScrapeModifiers.MainBanner OrElse
        '            tScrapeItem.ScrapeModifiers.MainClearArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainClearLogo OrElse
        '            tScrapeItem.ScrapeModifiers.MainDiscArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainFanart OrElse
        '            tScrapeItem.ScrapeModifiers.MainKeyArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainLandscape OrElse
        '            tScrapeItem.ScrapeModifiers.MainPoster) Then
        '            Dim tModifiers As New Structures.ScrapeModifiers With {.MainNFO = True}
        '            Dim tOptions As New Structures.ScrapeOptions 'set all values to false to not override any field. ID's are always determined.
        '            If AddonsManager.Instance.ScrapeData_MovieSet(DBScrapeMovieSet, tModifiers, Args.ScrapeType, tOptions, Args.ScrapeList.Count = 1) Then
        '                _Logger.Trace(String.Format("[MovieSet Scraper] [Cancelled] Scraping {0}", OldListTitle))
        '                Cancelled = True
        '                If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
        '                    bwMoviesetScraper.CancelAsync()
        '                End If
        '            End If
        '        End If
        '    End If

        '    If bwMoviesetScraper.CancellationPending Then Exit For

        '    If Not Cancelled Then

        '        NewListTitle = StringUtils.SortTokens(DBScrapeMovieSet.MainDetails.Title)
        '        NewTitle = DBScrapeMovieSet.MainDetails.Title
        '        NewTMDBColID = DBScrapeMovieSet.MainDetails.UniqueIDs.TMDbId.ToString

        '        If Not NewListTitle = OldListTitle Then
        '            bwMoviesetScraper.ReportProgress(0, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle))
        '        End If

        '        'get all images
        '        If tScrapeItem.ScrapeModifiers.MainBanner OrElse
        '            tScrapeItem.ScrapeModifiers.MainClearArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainClearLogo OrElse
        '            tScrapeItem.ScrapeModifiers.MainDiscArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainExtrafanarts OrElse
        '            tScrapeItem.ScrapeModifiers.MainFanart OrElse
        '            tScrapeItem.ScrapeModifiers.MainKeyArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainLandscape OrElse
        '            tScrapeItem.ScrapeModifiers.MainPoster Then

        '            Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
        '            bwMoviesetScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(254, "Scraping Images"), ":"))
        '            If Not AddonsManager.Instance.ScrapeImage_MovieSet(DBScrapeMovieSet, SearchResultsContainer, tScrapeItem.ScrapeModifiers) Then
        '                If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.MovieImagesDisplayImageSelect Then
        '                    Using dImgSelect As New dlgImageSelect
        '                        If dImgSelect.ShowDialog(DBScrapeMovieSet, SearchResultsContainer) = DialogResult.OK Then
        '                            Images.SetPreferredImages(DBScrapeMovieSet, dImgSelect.Result)
        '                        End If
        '                    End Using
        '                Else
        '                    'autoscraping
        '                    Images.SetPreferredImages(DBScrapeMovieSet, SearchResultsContainer, tScrapeItem.ScrapeModifiers)
        '                End If
        '            End If
        '        End If

        '        If bwMovieScraper.CancellationPending Then Exit For

        '        If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
        '            bwMoviesetScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
        '            Master.DB.Save_Movieset(DBScrapeMovieSet, True, True, True, True)
        '            bwMoviesetScraper.ReportProgress(-2, DBScrapeMovieSet.ID)
        '            bwMoviesetScraper.ReportProgress(-1, If(Not OldListTitle = NewListTitle, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle), NewListTitle))
        '        End If
        '        _Logger.Trace(String.Format("[MovieSet Scraper] [Done] Scraping {0}", OldListTitle))
        '    Else
        '        _Logger.Trace(String.Format("[MovieSet Scraper] [Cancelled] Scraping {0}", OldListTitle))
        '    End If
        'Next

        e.Result = New Results With {.DBElement = DBScrapeMovieSet, .ScrapeType = Args.ScrapeType, .Cancelled = bwMoviesetScraper.CancellationPending}
        _Logger.Trace(String.Format("[MovieSet Scraper] [Done] Scraping"))
    End Sub

    Private Sub bwTVScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTVScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeShow As New Database.DBElement(Enums.ContentType.TVShow)

        _Logger.Trace(String.Format("[TVScraper] [Start] TV Shows Count [{0}]", Args.ScrapeList.Count.ToString))

        'For Each tScrapeItem As ScrapeItem In Args.ScrapeList
        '    Dim Theme As New MediaContainers.Theme
        '    Dim tURL As String = String.Empty
        '    Dim tUrlList As New List(Of Themes)
        '    Dim OldListTitle As String = String.Empty
        '    Dim NewListTitle As String = String.Empty

        '    Cancelled = False

        '    If bwTVScraper.CancellationPending Then Exit For
        '    OldListTitle = tScrapeItem.DataRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ToString
        '    bwTVScraper.ReportProgress(1, OldListTitle)

        '    Dim dScrapeRow As DataRow = tScrapeItem.DataRow

        '    _Logger.Trace(String.Format("[TVScraper] [Start] Scraping {0}", OldListTitle))

        '    DBScrapeShow = Master.DB.Load_TVShow_Full(Convert.ToInt64(tScrapeItem.DataRow.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow))))
        '    'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, DBScrapeMovie)

        '    If tScrapeItem.ScrapeModifiers.MainNFO OrElse tScrapeItem.ScrapeModifiers.EpisodeNFO Then
        '        bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(253, "Scraping Data"), ":"))
        '        If AddonsManager.Instance.ScrapeData_TVShow(DBScrapeShow, tScrapeItem.ScrapeModifiers, Args.ScrapeType, Args.ScrapeOptions, Args.ScrapeList.Count = 1) Then
        '            Cancelled = True
        '            If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
        '                _Logger.Trace(String.Concat("Canceled scraping: ", OldListTitle))
        '                bwTVScraper.CancelAsync()
        '            End If
        '        End If
        '    Else
        '        ' if we do not have the tvshow ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
        '        If Not DBScrapeShow.MainDetails.UniqueIDs.TVDbIdSpecified AndAlso (
        '            tScrapeItem.ScrapeModifiers.MainActorthumbs OrElse
        '            tScrapeItem.ScrapeModifiers.MainBanner OrElse
        '            tScrapeItem.ScrapeModifiers.MainCharacterArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainClearArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainClearLogo OrElse
        '            tScrapeItem.ScrapeModifiers.MainExtrafanarts OrElse
        '            tScrapeItem.ScrapeModifiers.MainFanart OrElse
        '            tScrapeItem.ScrapeModifiers.MainKeyArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainLandscape OrElse
        '            tScrapeItem.ScrapeModifiers.MainPoster OrElse
        '            tScrapeItem.ScrapeModifiers.MainTheme) Then
        '            Dim tOpt As New Structures.ScrapeOptions 'all false value not to override any field
        '            If AddonsManager.Instance.ScrapeData_TVShow(DBScrapeShow, tScrapeItem.ScrapeModifiers, Args.ScrapeType, tOpt, Args.ScrapeList.Count = 1) Then
        '                Exit For
        '            End If
        '        End If
        '    End If

        '    If bwTVScraper.CancellationPending Then Exit For

        '    If Not Cancelled Then
        '        NewListTitle = StringUtils.SortTokens(DBScrapeShow.MainDetails.Title)

        '        If Not NewListTitle = OldListTitle Then
        '            bwTVScraper.ReportProgress(0, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle))
        '        End If

        '        'get all images
        '        Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
        '        bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(254, "Scraping Images"), ":"))
        '        If Not AddonsManager.Instance.ScrapeImage_TV(DBScrapeShow, SearchResultsContainer, tScrapeItem.ScrapeModifiers, Args.ScrapeList.Count = 1) Then
        '            If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.TVImagesDisplayImageSelect Then
        '                Using dImgSelect As New dlgImageSelect
        '                    If dImgSelect.ShowDialog(DBScrapeShow, SearchResultsContainer) = DialogResult.OK Then
        '                        Images.SetPreferredImages(DBScrapeShow, dImgSelect.Result)
        '                    End If
        '                End Using
        '            Else
        '                'autoscraping
        '                Images.SetPreferredImages(DBScrapeShow, SearchResultsContainer, tScrapeItem.ScrapeModifiers)
        '            End If
        '        End If

        '        If bwTVScraper.CancellationPending Then Exit For

        '        'Theme
        '        If tScrapeItem.ScrapeModifiers.MainTheme Then
        '            bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(266, "Scraping Themes"), ":"))
        '            Dim SearchResults As New List(Of MediaContainers.Theme)
        '            If Not AddonsManager.Instance.ScrapeTheme_TVShow(DBScrapeShow, Enums.ModifierType.MainTheme, SearchResults) Then
        '                If Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
        '                    Using dThemeSelect As New dlgThemeSelect
        '                        If dThemeSelect.ShowDialog(DBScrapeShow, SearchResults, AdvancedSettings.GetBooleanSetting("UseAsVideoPlayer", False, "generic.EmberCore.VLCPlayer")) = DialogResult.OK Then
        '                            DBScrapeShow.Theme = dThemeSelect.Result
        '                        End If
        '                    End Using

        '                    'autoscraping
        '                ElseIf Not Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
        '                    Dim newPreferredTheme As New MediaContainers.Theme
        '                    If Themes.GetPreferred(SearchResults, newPreferredTheme, Enums.ContentType.TVShow) Then
        '                        DBScrapeShow.Theme = newPreferredTheme
        '                    End If
        '                End If
        '            End If
        '        End If

        '        If bwTVScraper.CancellationPending Then Exit For

        '        'Episode Meta Data
        '        If tScrapeItem.ScrapeModifiers.withEpisodes AndAlso tScrapeItem.ScrapeModifiers.EpisodeMeta AndAlso Master.eSettings.TVScraperMetaDataScan Then
        '            bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(140, "Scanning Metadata"), ":"))
        '            For Each tEpisode In DBScrapeShow.Episodes.Where(Function(f) f.FileItemSpecified)
        '                MetaData.UpdateFileInfo(tEpisode)
        '            Next
        '        End If

        '        If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
        '            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.ScraperMulti_TVShow, Nothing, Nothing, False, DBScrapeShow)
        '            bwTVScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
        '            Master.DB.Save_TVShow(DBScrapeShow, False, tScrapeItem.ScrapeModifiers.MainNFO OrElse tScrapeItem.ScrapeModifiers.MainMeta, True, tScrapeItem.ScrapeModifiers.withEpisodes)
        '            bwTVScraper.ReportProgress(-2, DBScrapeShow.ID)
        '            bwTVScraper.ReportProgress(-1, If(Not OldListTitle = NewListTitle, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldListTitle, NewListTitle), NewListTitle))
        '        End If
        '    End If

        '    _Logger.Trace(String.Concat("Ended scraping: ", OldListTitle))
        'Next

        e.Result = New Results With {.DBElement = DBScrapeShow, .ScrapeType = Args.ScrapeType, .Cancelled = bwTVScraper.CancellationPending}
        _Logger.Trace("Ended TV SHOW scrape")
    End Sub

    Private Sub bwTVEpisodeScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTVEpisodeScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeEpisode As New Database.DBElement(Enums.ContentType.TVEpisode)

        _Logger.Trace(String.Format("[TVEpisode] [Start] Episodes Count [{0}]", Args.ScrapeList.Count.ToString))

        'For Each tScrapeItem As ScrapeItem In Args.ScrapeList
        '    Dim OldEpisodeTitle As String = String.Empty
        '    Dim NewEpisodeTitle As String = String.Empty

        '    Cancelled = False

        '    If bwTVEpisodeScraper.CancellationPending Then Exit For
        '    OldEpisodeTitle = tScrapeItem.DataRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Title)).ToString
        '    bwTVEpisodeScraper.ReportProgress(1, OldEpisodeTitle)

        '    Dim dScrapeRow As DataRow = tScrapeItem.DataRow

        '    _Logger.Trace(String.Format("[TVEpisodeScraper] [Start] Scraping {0}", OldEpisodeTitle))

        '    DBScrapeEpisode = Master.DB.Load_TVEpisode(Convert.ToInt64(tScrapeItem.DataRow.Item(Database.Helpers.GetMainIdName(Database.TableName.episode))), True)
        '    'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, DBScrapeMovie)

        '    If tScrapeItem.ScrapeModifiers.EpisodeNFO Then
        '        bwTVEpisodeScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(253, "Scraping Data"), ":"))
        '        If AddonsManager.Instance.ScrapeData_TVEpisode(DBScrapeEpisode, Args.ScrapeOptions, Args.ScrapeList.Count = 1) Then
        '            Cancelled = True
        '            If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
        '                _Logger.Trace(String.Concat("Canceled scraping: ", OldEpisodeTitle))
        '                bwTVEpisodeScraper.CancelAsync()
        '            End If
        '        End If
        '    Else
        '        ' if we do not have the episode ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
        '        If Not DBScrapeEpisode.MainDetails.UniqueIDs.TVDbIdSpecified AndAlso (
        '            tScrapeItem.ScrapeModifiers.MainActorthumbs OrElse
        '            tScrapeItem.ScrapeModifiers.MainBanner OrElse
        '            tScrapeItem.ScrapeModifiers.MainCharacterArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainClearArt OrElse
        '            tScrapeItem.ScrapeModifiers.MainClearLogo OrElse
        '            tScrapeItem.ScrapeModifiers.MainExtrafanarts OrElse
        '            tScrapeItem.ScrapeModifiers.MainFanart OrElse
        '            tScrapeItem.ScrapeModifiers.MainLandscape OrElse
        '            tScrapeItem.ScrapeModifiers.MainPoster OrElse
        '            tScrapeItem.ScrapeModifiers.MainTheme) Then
        '            Dim tOpt As New Structures.ScrapeOptions 'all false value not to override any field
        '            If AddonsManager.Instance.ScrapeData_TVEpisode(DBScrapeEpisode, tOpt, Args.ScrapeList.Count = 1) Then
        '                Exit For
        '            End If
        '        End If
        '    End If

        '    If bwTVEpisodeScraper.CancellationPending Then Exit For

        '    If Not Cancelled Then
        '        If Master.eSettings.TVScraperMetaDataScan AndAlso tScrapeItem.ScrapeModifiers.EpisodeMeta Then
        '            MetaData.UpdateFileInfo(DBScrapeEpisode)
        '        End If
        '        If bwTVEpisodeScraper.CancellationPending Then Exit For

        '        NewEpisodeTitle = DBScrapeEpisode.MainDetails.Title

        '        If Not NewEpisodeTitle = OldEpisodeTitle Then
        '            bwTVEpisodeScraper.ReportProgress(0, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldEpisodeTitle, NewEpisodeTitle))
        '        End If

        '        'get all images
        '        If tScrapeItem.ScrapeModifiers.EpisodeFanart OrElse
        '            tScrapeItem.ScrapeModifiers.EpisodePoster Then
        '            Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
        '            bwTVEpisodeScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(265, "Scraping Episode Images"), ":"))
        '            If Not AddonsManager.Instance.ScrapeImage_TV(DBScrapeEpisode, SearchResultsContainer, tScrapeItem.ScrapeModifiers, Args.ScrapeList.Count = 1) Then
        '                If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.TVImagesDisplayImageSelect Then
        '                    Using dImgSelect As New dlgImageSelect
        '                        If dImgSelect.ShowDialog(DBScrapeEpisode, SearchResultsContainer) = DialogResult.OK Then
        '                            Images.SetPreferredImages(DBScrapeEpisode, dImgSelect.Result)
        '                        End If
        '                    End Using
        '                Else
        '                    'autoscraping
        '                    Images.SetPreferredImages(DBScrapeEpisode, SearchResultsContainer, tScrapeItem.ScrapeModifiers)
        '                End If
        '            End If
        '        End If

        '        If bwTVEpisodeScraper.CancellationPending Then Exit For

        '        If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
        '            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.ScraperMulti_TVEpisode, Nothing, Nothing, False, DBScrapeEpisode)
        '            bwTVEpisodeScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
        '            Master.DB.Save_TVEpisode(DBScrapeEpisode, False, tScrapeItem.ScrapeModifiers.EpisodeNFO OrElse tScrapeItem.ScrapeModifiers.EpisodeMeta, True, True, True)
        '            bwTVEpisodeScraper.ReportProgress(-2, DBScrapeEpisode.ID)
        '            bwTVEpisodeScraper.ReportProgress(-1, If(Not OldEpisodeTitle = NewEpisodeTitle, String.Format(Master.eLang.GetString(812, "Old Title: {0} | New Title: {1}"), OldEpisodeTitle, NewEpisodeTitle), NewEpisodeTitle))
        '        End If
        '    End If

        '    _Logger.Trace(String.Concat("Ended scraping: ", OldEpisodeTitle))
        'Next

        e.Result = New Results With {.DBElement = DBScrapeEpisode, .ScrapeType = Args.ScrapeType, .Cancelled = bwTVEpisodeScraper.CancellationPending}
        _Logger.Trace("Ended EPISODE scrape")
    End Sub

    Private Sub bwTVSeasonScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTVSeasonScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeSeason As New Database.DBElement(Enums.ContentType.TVSeason)

        _Logger.Trace(String.Format("[TVSeason Scraper] [Start] Seasons Count [{0}]", Args.ScrapeList.Count.ToString))

        'For Each tScrapeItem As ScrapeItem In Args.ScrapeList
        '    Dim tURL As String = String.Empty
        '    Dim tUrlList As New List(Of Themes)

        '    Cancelled = False

        '    If bwTVSeasonScraper.CancellationPending Then Exit For

        '    Dim dScrapeRow As DataRow = tScrapeItem.DataRow

        '    DBScrapeSeason = Master.DB.Load_TVSeason(Convert.ToInt64(tScrapeItem.DataRow.Item(Database.Helpers.GetMainIdName(Database.TableName.season))), True, False)
        '    'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, DBScrapeMovie)

        '    _Logger.Trace(String.Format("Start scraping: {0}: Season {1}", DBScrapeSeason.TVShowDetails.Title, DBScrapeSeason.MainDetails.Season))

        '    If tScrapeItem.ScrapeModifiers.SeasonNFO Then
        '        bwTVSeasonScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(253, "Scraping Data"), ":"))
        '        If AddonsManager.Instance.ScrapeData_TVSeason(DBScrapeSeason, Args.ScrapeOptions, Args.ScrapeList.Count = 1) Then
        '            Cancelled = True
        '            If Args.ScrapeType = Enums.ScrapeType.SingleAuto OrElse Args.ScrapeType = Enums.ScrapeType.SingleField OrElse Args.ScrapeType = Enums.ScrapeType.SingleScrape Then
        '                _Logger.Trace(String.Format("Canceled scraping: {0}: Season {1}", DBScrapeSeason.TVShowDetails.Title, DBScrapeSeason.MainDetails.Season))
        '                bwTVSeasonScraper.CancelAsync()
        '            End If
        '        End If
        '    Else
        '        ' if we do not have the tvshow ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
        '        If Not DBScrapeSeason.MainDetails.UniqueIDs.TVDbIdSpecified AndAlso (
        '            tScrapeItem.ScrapeModifiers.SeasonBanner OrElse
        '            tScrapeItem.ScrapeModifiers.SeasonFanart OrElse
        '            tScrapeItem.ScrapeModifiers.SeasonLandscape OrElse
        '            tScrapeItem.ScrapeModifiers.SeasonPoster) Then
        '            Dim tOpt As New Structures.ScrapeOptions 'all false value not to override any field
        '            If AddonsManager.Instance.ScrapeData_TVSeason(DBScrapeSeason, tOpt, Args.ScrapeList.Count = 1) Then
        '                Exit For
        '            End If
        '        End If
        '    End If

        '    If bwTVSeasonScraper.CancellationPending Then Exit For

        '    If Not Cancelled Then
        '        'get all images
        '        If tScrapeItem.ScrapeModifiers.AllSeasonsBanner OrElse
        '            tScrapeItem.ScrapeModifiers.AllSeasonsFanart OrElse
        '            tScrapeItem.ScrapeModifiers.AllSeasonsLandscape OrElse
        '            tScrapeItem.ScrapeModifiers.AllSeasonsPoster OrElse
        '            tScrapeItem.ScrapeModifiers.SeasonBanner OrElse
        '            tScrapeItem.ScrapeModifiers.SeasonFanart OrElse
        '            tScrapeItem.ScrapeModifiers.SeasonLandscape OrElse
        '            tScrapeItem.ScrapeModifiers.SeasonPoster Then

        '            Dim SearchResultsContainer As New MediaContainers.SearchResultsContainer
        '            bwTVSeasonScraper.ReportProgress(-3, "Scraping Season Images:")
        '            If Not AddonsManager.Instance.ScrapeImage_TV(DBScrapeSeason, SearchResultsContainer, tScrapeItem.ScrapeModifiers, Args.ScrapeList.Count = 1) Then
        '                If Args.ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.eSettings.TVImagesDisplayImageSelect Then
        '                    Using dImgSelect As New dlgImageSelect
        '                        If dImgSelect.ShowDialog(DBScrapeSeason, SearchResultsContainer) = DialogResult.OK Then
        '                            Images.SetPreferredImages(DBScrapeSeason, dImgSelect.Result)
        '                        End If
        '                    End Using
        '                Else
        '                    'autoscraping
        '                    Images.SetPreferredImages(DBScrapeSeason, SearchResultsContainer, tScrapeItem.ScrapeModifiers)
        '                End If
        '            End If
        '        End If

        '        If bwTVSeasonScraper.CancellationPending Then Exit For

        '        If Not (Args.ScrapeType = Enums.ScrapeType.SingleScrape) Then
        '            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.ScraperMulti_TVSeason, Nothing, Nothing, False, DBScrapeSeason)
        '            bwTVSeasonScraper.ReportProgress(-3, String.Concat(Master.eLang.GetString(399, "Downloading and Saving Contents into Database"), ":"))
        '            Master.DB.Save_TVSeason(DBScrapeSeason, False, True, True)
        '            bwTVSeasonScraper.ReportProgress(-2, DBScrapeSeason.ID)
        '        End If
        '    End If

        '    _Logger.Trace(String.Format("Ended scraping: {0}: Season {1}", DBScrapeSeason.TVShowDetails.Title, DBScrapeSeason.MainDetails.Season))
        'Next

        e.Result = New Results With {.DBElement = DBScrapeSeason, .ScrapeType = Args.ScrapeType, .Cancelled = bwTVSeasonScraper.CancellationPending}
        _Logger.Trace("Ended TV Season scrape")
    End Sub

    Private Sub bwTVSeasonScraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwTVSeasonScraper.ProgressChanged
        If e.ProgressPercentage = -1 Then
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"tvseasonscraped", 3, Master.eLang.GetString(247, "Season Scraped"), e.UserState.ToString, Nothing}))
        ElseIf e.ProgressPercentage = -2 Then
            DataGridView_RefreshRow_TVSeason(CLng(e.UserState))
        ElseIf e.ProgressPercentage = -3 Then
            tslLoading.Text = e.UserState.ToString
        Else
            tspbLoading.Value += e.ProgressPercentage
            SetStatus(e.UserState.ToString)
        End If
    End Sub

    Private Sub bwReload_Movies_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwReload_Movie.DoWork
        Dim iCount As Integer = 0
        Dim MovieIDs As New Dictionary(Of Long, String)
        Dim doFill As Boolean = False

        For Each sRow As DataRow In _DtMovies.Rows
            MovieIDs.Add(Convert.ToInt64(sRow.Item(Database.Helpers.GetMainIdName(Database.TableName.movie))),
                         sRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ToString)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each KVP As KeyValuePair(Of Long, String) In MovieIDs
                If bwReload_Movie.CancellationPending Then Return
                bwReload_Movie.ReportProgress(iCount, KVP.Value)
                If Reload_Movie(KVP.Key, True, False) Then
                    doFill = True
                Else
                    bwReload_Movie.ReportProgress(-1, KVP.Key)
                End If
                iCount += 1
            Next
            SQLtransaction.Commit()
        End Using
        e.Result = New Results With {.doFill = doFill}
    End Sub

    Private Sub bwReload_Movies_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwReload_Movie.ProgressChanged
        If e.ProgressPercentage = -1 Then
            DataGridView_RefreshRow_Movie(CLng(e.UserState))
        Else
            SetStatus(e.UserState.ToString)
            tspbLoading.Value = e.ProgressPercentage
        End If
    End Sub

    Private Sub bwReload_Movies_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwReload_Movie.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        tslLoading.Text = String.Empty
        tspbLoading.Visible = False
        tslLoading.Visible = False

        If Res.doFill Then
            DataGridView_Fill_Main(True, True, False)
        Else
            DoTitleCheck()
            SetControlsEnabled(True)
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub bwReload_MovieSets_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwReload_Movieset.DoWork
        Dim iCount As Integer = 0
        Dim MovieSetIDs As New Dictionary(Of Long, String)
        Dim doFill As Boolean = False

        For Each sRow As DataRow In _DtMovieSets.Rows
            MovieSetIDs.Add(Convert.ToInt64(sRow.Item(Database.Helpers.GetMainIdName(Database.TableName.movieset))),
                            sRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ToString)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each KVP As KeyValuePair(Of Long, String) In MovieSetIDs
                If bwReload_Movieset.CancellationPending Then Return
                bwReload_Movieset.ReportProgress(iCount, KVP.Value)
                If Reload_Movieset(KVP.Key, True) Then
                    doFill = True
                Else
                    bwReload_Movieset.ReportProgress(-1, KVP.Key)
                End If
                iCount += 1
            Next
            SQLtransaction.Commit()
        End Using
        e.Result = New Results With {.doFill = doFill}
    End Sub

    Private Sub bwReload_MovieSets_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwReload_Movieset.ProgressChanged
        If e.ProgressPercentage = -1 Then
            DataGridView_RefreshRow_Movieset(CLng(e.UserState))
        Else
            SetStatus(e.UserState.ToString)
            tspbLoading.Value = e.ProgressPercentage
        End If
    End Sub

    Private Sub bwReload_MovieSets_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwReload_Movieset.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        tslLoading.Text = String.Empty
        tspbLoading.Visible = False
        tslLoading.Visible = False

        If Res.doFill Then
            DataGridView_Fill_Main(False, True, False)
        Else
            DoTitleCheck()
            SetControlsEnabled(True)
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub bwReload_TVShows_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwReload_TVShow.DoWork
        Dim reloadFull As Boolean = DirectCast(e.Argument, Boolean)
        Dim iCount As Integer = 0
        Dim ShowIDs As New Dictionary(Of Long, String)
        Dim doFill As Boolean = False

        For Each sRow As DataRow In _DtTVShows.Rows
            ShowIDs.Add(Convert.ToInt64(sRow.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow))),
                        sRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ToString)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each KVP As KeyValuePair(Of Long, String) In ShowIDs
                If bwReload_TVShow.CancellationPending Then Return
                bwReload_TVShow.ReportProgress(iCount, KVP.Value)
                If Reload_TVShow(KVP.Key, True, False, reloadFull) Then
                    doFill = True
                Else
                    bwReload_TVShow.ReportProgress(-1, KVP.Key)
                End If
                iCount += 1
            Next
            SQLtransaction.Commit()
        End Using
        e.Result = New Results With {.doFill = doFill}
    End Sub

    Private Sub bwReload_TVShows_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwReload_TVShow.ProgressChanged
        If e.ProgressPercentage = -1 Then
            DataGridView_RefreshRow_TVShow(CLng(e.UserState))
        Else
            SetStatus(e.UserState.ToString)
            tspbLoading.Value = e.ProgressPercentage
        End If
    End Sub

    Private Sub bwReload_TVShows_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwReload_TVShow.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)
        tslLoading.Text = String.Empty
        tspbLoading.Visible = False
        tslLoading.Visible = False

        If Res.doFill Then
            DataGridView_Fill_Main(False, False, True)
        Else
            SetControlsEnabled(True)
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub bwRewriteContent_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwRewriteContent.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim iCount As Integer = 0
        Dim dicIDs As New Dictionary(Of Long, String)

        Select Case Args.ContentType
            Case Enums.ContentType.Movie
                For Each sRow As DataRow In _DtMovies.Rows
                    dicIDs.Add(Convert.ToInt64(sRow.Item(Database.Helpers.GetMainIdName(Database.TableName.movie))),
                               sRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ToString)
                Next

                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    For Each KVP As KeyValuePair(Of Long, String) In dicIDs
                        If bwRewriteContent.CancellationPending Then Return
                        bwRewriteContent.ReportProgress(iCount, KVP.Value)
                        Rewrite_Movie(KVP.Key, True, Args.Trigger)
                        iCount += 1
                    Next
                    SQLtransaction.Commit()
                End Using
            Case Enums.ContentType.Movieset
                For Each sRow As DataRow In _DtMovieSets.Rows
                    dicIDs.Add(Convert.ToInt64(sRow.Item(Database.Helpers.GetMainIdName(Database.TableName.movieset))),
                               sRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ToString)
                Next

                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    For Each KVP As KeyValuePair(Of Long, String) In dicIDs
                        If bwRewriteContent.CancellationPending Then Return
                        bwRewriteContent.ReportProgress(iCount, KVP.Value)
                        Rewrite_Movieset(KVP.Key, True, Args.Trigger)
                        iCount += 1
                    Next
                    SQLtransaction.Commit()
                End Using
            Case Enums.ContentType.TV
                For Each sRow As DataRow In _DtTVShows.Rows
                    dicIDs.Add(Convert.ToInt64(sRow.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow))),
                               sRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ListTitle)).ToString)
                Next

                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    For Each KVP As KeyValuePair(Of Long, String) In dicIDs
                        If bwRewriteContent.CancellationPending Then Return
                        bwRewriteContent.ReportProgress(iCount, KVP.Value)
                        Rewrite_TVShow(KVP.Key, True, Args.Trigger)
                        iCount += 1
                    Next
                    SQLtransaction.Commit()
                End Using
        End Select
    End Sub

    Private Sub bwRewriteContent_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwRewriteContent.ProgressChanged
        SetStatus(e.UserState.ToString)
        tspbLoading.Value = e.ProgressPercentage
    End Sub

    Private Sub bwRewriteContent_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwRewriteContent.RunWorkerCompleted
        tslLoading.Text = String.Empty
        tslLoading.Visible = False
        tspbLoading.Visible = False
        btnCancel.Visible = False
        lblCanceling.Visible = False
        prbCanceling.Visible = False
        pnlCancel.Visible = False

        DataGridView_Fill_Main(True, True, True)
    End Sub

    Private Sub cbFilterLists_Movies_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilterLists_Movies.SelectedIndexChanged
        If Not currList_Movies = CType(cbFilterLists_Movies.SelectedItem, KeyValuePair(Of String, String)).Value Then
            currList_Movies = CType(cbFilterLists_Movies.SelectedItem, KeyValuePair(Of String, String)).Value
            AddonsManager.Instance.RuntimeObjects.ListMovies = currList_Movies
            DataGridView_Fill_Main(True, False, False)
        End If
    End Sub

    Private Sub cbFilterLists_MovieSets_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilterLists_MovieSets.SelectedIndexChanged
        If Not currList_MovieSets = CType(cbFilterLists_MovieSets.SelectedItem, KeyValuePair(Of String, String)).Value Then
            currList_MovieSets = CType(cbFilterLists_MovieSets.SelectedItem, KeyValuePair(Of String, String)).Value
            AddonsManager.Instance.RuntimeObjects.ListMovieSets = currList_MovieSets
            DataGridView_Fill_Main(False, True, False)
        End If
    End Sub

    Private Sub cbFilterLists_Shows_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFilterLists_Shows.SelectedIndexChanged
        If Not currList_TVShows = CType(cbFilterLists_Shows.SelectedItem, KeyValuePair(Of String, String)).Value Then
            currList_TVShows = CType(cbFilterLists_Shows.SelectedItem, KeyValuePair(Of String, String)).Value
            AddonsManager.Instance.RuntimeObjects.ListTVShows = currList_TVShows
            DataGridView_Fill_Main(False, False, True)
        End If
    End Sub

    Private Sub cbSearchMovies_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbSearchMovies.SelectedIndexChanged
        currTextSearch_Movies = txtSearchMovies.Text

        tmrSearchWait_Movies.Enabled = False
        tmrSearch_Movies.Enabled = False
        tmrSearchWait_Movies.Enabled = True
    End Sub

    Private Sub cbSearchMovieSets_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbSearchMovieSets.SelectedIndexChanged
        currTextSearch_MovieSets = txtSearchMovieSets.Text

        tmrSearchWait_MovieSets.Enabled = False
        tmrSearch_MovieSets.Enabled = False
        tmrSearchWait_MovieSets.Enabled = True
    End Sub

    Private Sub cbSearchShows_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbSearchShows.SelectedIndexChanged
        currTextSearch_TVShows = txtSearchShows.Text

        tmrSearchWait_Shows.Enabled = False
        tmrSearch_Shows.Enabled = False
        tmrSearchWait_Shows.Enabled = True
    End Sub

    Private Sub chkFilterDuplicates_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterDuplicates_Movies.Click
        DataGridView_ApplyFilter_Movie()
    End Sub

    Private Sub chkFilterEmpty_MovieSets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterEmpty_MovieSets.Click
        If chkFilterEmpty_MovieSets.Checked Then
            Filter_Movieset.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.MovieCount, .Operator = SmartFilter.Operators.Is, .Value = 0})
        Else
            Filter_Movieset.RemoveAll(Database.ColumnName.MovieCount, SmartFilter.Operators.Is)
        End If
        DataGridView_ApplyFilter_Movieset()
    End Sub

    Private Sub chkFilterMultiple_MovieSets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterMultiple_MovieSets.Click
        Filter_Add_GreaterThan(Database.ColumnName.MovieCount, If(chkFilterMultiple_MovieSets.Checked, 1, -1), Enums.ContentType.Movieset)
    End Sub

    Private Sub chkFilterOne_MovieSets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterOne_MovieSets.Click
        If chkFilterOne_MovieSets.Checked Then
            Filter_Movieset.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.MovieCount, .Operator = SmartFilter.Operators.Is, .Value = 1})
        Else
            Filter_Movieset.RemoveAll(Database.ColumnName.MovieCount, SmartFilter.Operators.Is)
        End If
        DataGridView_ApplyFilter_Movieset()
    End Sub

    Private Sub chkFilterLock_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterLock_Movies.Click
        Filter_Add_Boolean(Database.ColumnName.Locked, chkFilterLock_Movies.Checked, Enums.ContentType.Movie)
    End Sub

    Private Sub chkFilterLock_MovieSets_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterLock_MovieSets.Click
        Filter_Add_Boolean(Database.ColumnName.Locked, chkFilterLock_MovieSets.Checked, Enums.ContentType.Movieset)
    End Sub

    Private Sub chkFilterLockEpisodes_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterLockEpisodes_Shows.Click
        Filter_Add_GreaterThan(Database.ColumnName.LockedEpisodesCount, If(chkFilterLockEpisodes_Shows.Checked, 0, -1), Enums.ContentType.TVShow)
    End Sub

    Private Sub chkFilterLock_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterLock_Shows.Click
        Filter_Add_Boolean(Database.ColumnName.Locked, chkFilterLock_Shows.Checked, Enums.ContentType.TVShow)
    End Sub

    Private Sub chkFilterMark_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterMark_Movies.Click
        Filter_Add_Boolean(Database.ColumnName.Marked, chkFilterMark_Movies.Checked, Enums.ContentType.Movie)
    End Sub

    Private Sub chkFilterMark_MovieSets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterMark_MovieSets.Click
        Filter_Add_Boolean(Database.ColumnName.Marked, chkFilterMark_MovieSets.Checked, Enums.ContentType.Movieset)
    End Sub

    Private Sub chkFilterMark_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterMark_Shows.Click
        Filter_Add_Boolean(Database.ColumnName.Marked, chkFilterMark_Shows.Checked, Enums.ContentType.TVShow)
    End Sub

    Private Sub chkFilterMarkEpisodes_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterMarkEpisodes_Shows.Click
        Filter_Add_GreaterThan(Database.ColumnName.MarkedEpisodesCount, If(chkFilterMarkEpisodes_Shows.Checked, 0, -1), Enums.ContentType.TVShow)
    End Sub

    Private Sub chkFilterMarkCustom1_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterMarkCustom1_Movies.Click
        Filter_Add_Boolean(Database.ColumnName.MarkedCustom1, chkFilterMarkCustom1_Movies.Checked, Enums.ContentType.Movie)
    End Sub

    Private Sub chkFilterMarkCustom2_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterMarkCustom2_Movies.Click
        Filter_Add_Boolean(Database.ColumnName.MarkedCustom2, chkFilterMarkCustom2_Movies.Checked, Enums.ContentType.Movie)
    End Sub

    Private Sub chkFilterMarkCustom3_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterMarkCustom3_Movies.Click
        Filter_Add_Boolean(Database.ColumnName.MarkedCustom3, chkFilterMarkCustom3_Movies.Checked, Enums.ContentType.Movie)
    End Sub

    Private Sub chkFilterMarkCustom4_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterMarkCustom4_Movies.Click
        Filter_Add_Boolean(Database.ColumnName.MarkedCustom4, chkFilterMarkCustom4_Movies.Checked, Enums.ContentType.Movie)
    End Sub

    Private Sub chkFilterMissing_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterMissing_Movies.Click
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkFilterMissing_MovieSets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterMissing_MovieSets.Click
        Filter_Add_Missing_Movieset()
    End Sub

    Private Sub chkFilterMissing_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterMissing_Shows.Click
        Filter_Add_Missing_TVShow()
    End Sub

    Private Sub chkFilterNew_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterNew_Movies.Click
        Filter_Add_Boolean(Database.ColumnName.[New], chkFilterNew_Movies.Checked, Enums.ContentType.Movie)
    End Sub

    Private Sub chkFilterNew_Moviesets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterNew_MovieSets.Click
        Filter_Add_Boolean(Database.ColumnName.[New], chkFilterNew_MovieSets.Checked, Enums.ContentType.Movieset)
    End Sub

    Private Sub chkFilterNewEpisodes_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterNewEpisodes_Shows.Click
        Filter_Add_GreaterThan(Database.ColumnName.NewEpisodesCount, If(chkFilterNewEpisodes_Shows.Checked, 0, -1), Enums.ContentType.TVShow)
    End Sub

    Private Sub chkFilterNewShows_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterNewShows_Shows.Click
        Filter_Add_Boolean(Database.ColumnName.[New], chkFilterNewShows_Shows.Checked, Enums.ContentType.TVShow)
    End Sub

    Private Sub chkFilterTolerance_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles chkFilterTolerance_Movies.Click
        If chkFilterTolerance_Movies.Checked Then
            Filter_Movie.Rules.Add(New SmartFilter.Rule With {.Field = Database.ColumnName.OutOfTolerance, .Operator = SmartFilter.Operators.True})
        Else
            Filter_Movie.RemoveAll(Database.ColumnName.OutOfTolerance)
        End If
        DataGridView_ApplyFilter_Movie()
    End Sub

    Private Sub chkMovieMissingBanner_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieMissingBanner.CheckedChanged
        Master.eSettings.MovieMissingBanner = chkMovieMissingBanner.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkMovieMissingClearArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieMissingClearArt.CheckedChanged
        Master.eSettings.MovieMissingClearArt = chkMovieMissingClearArt.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkMovieMissingClearLogo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieMissingClearLogo.CheckedChanged
        Master.eSettings.MovieMissingClearLogo = chkMovieMissingClearLogo.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkMovieMissingDiscArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieMissingDiscArt.CheckedChanged
        Master.eSettings.MovieMissingDiscArt = chkMovieMissingDiscArt.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkMovieMissingExtrafanarts_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieMissingExtrafanarts.CheckedChanged
        Master.eSettings.MovieMissingExtrafanarts = chkMovieMissingExtrafanarts.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkMovieMissingExtrathumbs_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieMissingExtrathumbs.CheckedChanged
        Master.eSettings.MovieMissingExtrathumbs = chkMovieMissingExtrathumbs.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkMovieMissingFanart_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieMissingFanart.CheckedChanged
        Master.eSettings.MovieMissingFanart = chkMovieMissingFanart.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkMovieMissingKeyArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieMissingKeyArt.CheckedChanged
        Master.eSettings.MovieMissingKeyArt = chkMovieMissingKeyArt.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkMovieMissingLandscape_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieMissingLandscape.CheckedChanged
        Master.eSettings.MovieMissingLandscape = chkMovieMissingLandscape.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkMovieMissingNFO_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieMissingNFO.CheckedChanged
        Master.eSettings.MovieMissingNFO = chkMovieMissingNFO.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkMovieMissingPoster_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieMissingPoster.CheckedChanged
        Master.eSettings.MovieMissingPoster = chkMovieMissingPoster.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkMovieMissingSubtitles_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieMissingSubtitles.CheckedChanged
        Master.eSettings.MovieMissingSubtitles = chkMovieMissingSubtitles.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkMovieMissingTheme_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieMissingTheme.CheckedChanged
        Master.eSettings.MovieMissingTheme = chkMovieMissingTheme.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkMovieMissingTrailer_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieMissingTrailer.CheckedChanged
        Master.eSettings.MovieMissingTrailer = chkMovieMissingTrailer.Checked
        chkFilterMissing_Movies.Enabled = Master.eSettings.MovieMissingItemsAnyEnabled
        chkFilterMissing_Movies.Checked = Master.eSettings.MovieMissingItemsAnyEnabled
        Filter_Add_Missing_Movie()
    End Sub

    Private Sub chkMovieSetMissingBanner_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetMissingBanner.CheckedChanged
        Master.eSettings.MovieSetMissingBanner = chkMovieSetMissingBanner.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Filter_Add_Missing_Movieset()
    End Sub

    Private Sub chkMovieSetMissingClearArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetMissingClearArt.CheckedChanged
        Master.eSettings.MovieSetMissingClearArt = chkMovieSetMissingClearArt.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Filter_Add_Missing_Movieset()
    End Sub

    Private Sub chkMovieSetMissingClearLogo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetMissingClearLogo.CheckedChanged
        Master.eSettings.MovieSetMissingClearLogo = chkMovieSetMissingClearLogo.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Filter_Add_Missing_Movieset()
    End Sub

    Private Sub chkMovieSetMissingDiscArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetMissingDiscArt.CheckedChanged
        Master.eSettings.MovieSetMissingDiscArt = chkMovieSetMissingDiscArt.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Filter_Add_Missing_Movieset()
    End Sub

    Private Sub chkMovieSetMissingFanart_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetMissingFanart.CheckedChanged
        Master.eSettings.MovieSetMissingFanart = chkMovieSetMissingFanart.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Filter_Add_Missing_Movieset()
    End Sub

    Private Sub chkMovieSetMissingKeyArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetMissingKeyArt.CheckedChanged
        Master.eSettings.MovieSetMissingKeyArt = chkMovieSetMissingKeyArt.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Filter_Add_Missing_Movieset()
    End Sub

    Private Sub chkMovieSetMissingLandscape_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetMissingLandscape.CheckedChanged
        Master.eSettings.MovieSetMissingLandscape = chkMovieSetMissingLandscape.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Filter_Add_Missing_Movieset()
    End Sub

    Private Sub chkMovieSetMissingNFO_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetMissingNFO.CheckedChanged
        Master.eSettings.MovieSetMissingNFO = chkMovieSetMissingNFO.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Filter_Add_Missing_Movieset()
    End Sub

    Private Sub chkMovieSetMissingPoster_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetMissingPoster.CheckedChanged
        Master.eSettings.MovieSetMissingPoster = chkMovieSetMissingPoster.Checked
        chkFilterMissing_MovieSets.Enabled = Master.eSettings.MovieSetMissingItemsAnyEnabled
        chkFilterMissing_MovieSets.Checked = Master.eSettings.MovieSetMissingItemsAnyEnabled
        Filter_Add_Missing_Movieset()
    End Sub

    Private Sub chkShowMissingBanner_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowMissingBanner.CheckedChanged
        Master.eSettings.TVShowMissingBanner = chkShowMissingBanner.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        Filter_Add_Missing_TVShow()
    End Sub

    Private Sub chkShowMissingCharacterArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowMissingCharacterArt.CheckedChanged
        Master.eSettings.TVShowMissingCharacterArt = chkShowMissingCharacterArt.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        Filter_Add_Missing_TVShow()
    End Sub

    Private Sub chkShowMissingClearArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowMissingClearArt.CheckedChanged
        Master.eSettings.TVShowMissingClearArt = chkShowMissingClearArt.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        Filter_Add_Missing_TVShow()
    End Sub

    Private Sub chkShowMissingClearLogo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowMissingClearLogo.CheckedChanged
        Master.eSettings.TVShowMissingClearLogo = chkShowMissingClearLogo.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        Filter_Add_Missing_TVShow()
    End Sub

    Private Sub chkShowMissingExtrafanarts_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowMissingExtrafanarts.CheckedChanged
        Master.eSettings.TVShowMissingExtrafanarts = chkShowMissingExtrafanarts.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        Filter_Add_Missing_TVShow()
    End Sub

    Private Sub chkShowMissingFanart_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowMissingFanart.CheckedChanged
        Master.eSettings.TVShowMissingFanart = chkShowMissingFanart.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        Filter_Add_Missing_TVShow()
    End Sub

    Private Sub chkShowMissingKeyArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowMissingKeyArt.CheckedChanged
        Master.eSettings.TVShowMissingKeyArt = chkShowMissingKeyArt.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        Filter_Add_Missing_TVShow()
    End Sub

    Private Sub chkShowMissingLandscape_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowMissingLandscape.CheckedChanged
        Master.eSettings.TVShowMissingLandscape = chkShowMissingLandscape.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        Filter_Add_Missing_TVShow()
    End Sub

    Private Sub chkShowMissingNFO_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowMissingNFO.CheckedChanged
        Master.eSettings.TVShowMissingNFO = chkShowMissingNFO.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        Filter_Add_Missing_TVShow()
    End Sub

    Private Sub chkShowMissingPoster_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowMissingPoster.CheckedChanged
        Master.eSettings.TVShowMissingPoster = chkShowMissingPoster.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        Filter_Add_Missing_TVShow()
    End Sub

    Private Sub chkShowMissingTheme_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowMissingTheme.CheckedChanged
        Master.eSettings.TVShowMissingTheme = chkShowMissingTheme.Checked
        chkFilterMissing_Shows.Enabled = Master.eSettings.TVShowMissingItemsAnyEnabled
        chkFilterMissing_Shows.Checked = Master.eSettings.TVShowMissingItemsAnyEnabled
        Filter_Add_Missing_TVShow()
    End Sub

    Private Sub clbFilterTags_Movies_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbFilterTags_Movies.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbFilterTags_Movies.Items.Count - 1
                clbFilterTags_Movies.SetItemChecked(i, False)
            Next
        Else
            clbFilterTags_Movies.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub clbFilterTags_Shows_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbFilterTags_Shows.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbFilterTags_Shows.Items.Count - 1
                clbFilterTags_Shows.SetItemChecked(i, False)
            Next
        Else
            clbFilterTags_Shows.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub clbFilterGenres_Movies_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbFilterGenres_Movies.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbFilterGenres_Movies.Items.Count - 1
                clbFilterGenres_Movies.SetItemChecked(i, False)
            Next
        Else
            clbFilterGenres_Movies.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub clbFilterGenres_Shows_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbFilterGenres_Shows.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbFilterGenres_Shows.Items.Count - 1
                clbFilterGenres_Shows.SetItemChecked(i, False)
            Next
        Else
            clbFilterGenres_Shows.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub clbFilterCountries_Movies_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbFilterCountries_Movies.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbFilterCountries_Movies.Items.Count - 1
                clbFilterCountries_Movies.SetItemChecked(i, False)
            Next
        Else
            clbFilterCountries_Movies.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub clbFilterTags_Movies_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles clbFilterTags_Movies.LostFocus
        Filter_Add_List(clbFilterTags_Movies, pnlFilterTags_Movies, txtFilterTag_Movies, Database.ColumnName.Tags, Enums.ContentType.Movie)
    End Sub

    Private Sub clbFilterTags_Shows_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles clbFilterTags_Shows.LostFocus
        Filter_Add_List(clbFilterTags_Shows, pnlFilterTags_Shows, txtFilterTag_Shows, Database.ColumnName.Tags, Enums.ContentType.TVShow)
    End Sub

    Private Sub clbFilterGenres_Movies_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles clbFilterGenres_Movies.LostFocus
        Filter_Add_List(clbFilterGenres_Movies, pnlFilterGenres_Movies, txtFilterGenre_Movies, Database.ColumnName.Genres, Enums.ContentType.Movie)
    End Sub

    Private Sub clbFilterGenres_Shows_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles clbFilterGenres_Shows.LostFocus
        Filter_Add_List(clbFilterGenres_Shows, pnlFilterGenres_Shows, txtFilterGenre_Shows, Database.ColumnName.Genres, Enums.ContentType.TVShow)
    End Sub

    Private Sub clbFilterCountries_Movies_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles clbFilterCountries_Movies.LostFocus
        Filter_Add_List(clbFilterCountries_Movies, pnlFilterCountries_Movies, txtFilterCountry_Movies, Database.ColumnName.Countries, Enums.ContentType.Movie)
    End Sub

    Private Sub clbFilterDataFields_Movies_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles clbFilterDataFields_Movies.LostFocus, cbFilterDataField_Movies.SelectedIndexChanged
        pnlFilterDataFields_Movies.Visible = False
        pnlFilterDataFields_Movies.Tag = "NO"

        If clbFilterDataFields_Movies.CheckedItems.Count > 0 Then
            Dim filter As New List(Of SmartFilter.Rule)
            txtFilterDataField_Movies.Text = String.Empty

            Dim lstItems As New List(Of String)
            lstItems.AddRange(clbFilterDataFields_Movies.CheckedItems.OfType(Of String).ToList)

            txtFilterDataField_Movies.Text = String.Join(" | ", lstItems.ToArray)

            For i As Integer = 0 To lstItems.Count - 1
                Dim rule As New SmartFilter.Rule
                rule.Field = Database.Helpers.GetColumnName(lstItems(i))
                rule.Operator = If(cbFilterDataField_Movies.SelectedIndex = 0, SmartFilter.Operators.IsNullOrEmpty, SmartFilter.Operators.IsNotNullOrEmpty)
                filter.Add(rule)
            Next

            Filter_Movie.RemoveAllDataFieldFilters()
            Filter_Movie.Rules.AddRange(filter)
            DataGridView_ApplyFilter_Movie()
        Else
            If Filter_Movie.ContainsAnyDataFieldFilter Then
                txtFilterDataField_Movies.Text = String.Empty
                Filter_Movie.RemoveAllDataFieldFilters()
                DataGridView_ApplyFilter_Movie()
            End If
        End If
    End Sub

    Private Sub clbFilterSource_Movies_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles clbFilterSources_Movies.LostFocus
        Filter_Add_Source(clbFilterSources_Movies, pnlFilterSources_Movies, txtFilterSource_Movies, Enums.ContentType.Movie)
    End Sub

    Private Sub clbFilterVideoSources_Movies_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles clbFilterVideoSources_Movies.LostFocus
        Filter_Add_List(clbFilterVideoSources_Movies, pnlFilterVideoSources_Movies, txtFilterVideoSource_Movies, Database.ColumnName.VideoSource, Enums.ContentType.Movie)
    End Sub

    Private Sub clbFilterSource_Shows_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles clbFilterSource_Shows.LostFocus
        Filter_Add_Source(clbFilterSource_Shows, pnlFilterSources_Shows, txtFilterSource_Shows, Enums.ContentType.TVShow)
    End Sub

    Private Sub mnuMainToolsCleanDB_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsCleanDB.Click, cmnuTrayToolsCleanDB.Click
        CleanDB(New Scanner.ScanOrCleanOptions With {.Movies = True, .Moviesets = True, .TV = True})
    End Sub

    Private Sub CleanDB(ByVal Clean As Scanner.ScanOrCleanOptions)
        SetControlsEnabled(False, True)
        tspbLoading.Style = ProgressBarStyle.Marquee
        FilterPanel_IsEnabled_Movie(False)
        FilterPanel_IsEnabled_Movieset(False)
        FilterPanel_IsEnabled_TVShow(False)

        SetStatus(Master.eLang.GetString(644, "Cleaning Database..."))
        tspbLoading.Visible = True

        bwCleanDB.WorkerSupportsCancellation = True
        bwCleanDB.RunWorkerAsync(Clean)
    End Sub

    Private Sub mnuMainToolsCleanFiles_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsCleanFiles.Click, cmnuTrayToolsCleanFiles.Click
        FileUtils.CleanUp.DoCleanUp()
    End Sub

    Private Sub mnuMainToolsClearCache_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsClearCache.Click, cmnuTrayToolsClearCache.Click
        FileUtils.Delete.Cache_All()
    End Sub

    Private Sub cmnuShowOpenFolder_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowOpenFolder.Click
        If dgvTVShows.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvTVShows.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVShows.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If

            If doOpen Then
                For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                    Using Explorer As New Process
                        Explorer.StartInfo.FileName = "explorer.exe"
                        Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", sRow.Cells(Database.Helpers.GetColumnName(Database.ColumnName.Path)).Value.ToString)
                        Explorer.Start()
                    End Using
                Next
            End If
        End If
    End Sub

    Private Sub cmnuShowClearCacheDataAndImages_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowClearCacheDataAndImages.Click
        Dim idList As New List(Of String)
        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
            idList.Add(sRow.Cells("TVDB").Value.ToString)
        Next
        FileUtils.Delete.Cache_Show(idList, True, True)
    End Sub

    Private Sub cmnuShowClearCacheDataOnly_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowClearCacheDataOnly.Click
        Dim idList As New List(Of String)
        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
            idList.Add(sRow.Cells("TVDB").Value.ToString)
        Next
        FileUtils.Delete.Cache_Show(idList, True, False)
    End Sub

    Private Sub cmnuShowClearCacheImagesOnly_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowClearCacheImagesOnly.Click
        Dim idList As New List(Of String)
        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
            idList.Add(sRow.Cells("TVDB").Value.ToString)
        Next
        FileUtils.Delete.Cache_Show(idList, False, True)
    End Sub

    Private Sub cmnuEpisodeChange_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuEpisodeChange.Click
        Dim indX As Integer = dgvTVEpisodes.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvTVEpisodes.Item("idEpisode", indX).Value)
        Dim ShowID As Long = Convert.ToInt64(dgvTVEpisodes.Item("idShow", indX).Value)

        SetControlsEnabled(False, True)

        Dim tmpEpisode As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
        Dim tmpShow As Database.DBElement = Master.DB.Load_TVShow(ShowID, False, False)

        Functions.SetScrapeModifiers(tmpShow.ScrapeModifiers, Enums.ModifierType.MainNFO, True)
        Functions.SetScrapeModifiers(tmpShow.ScrapeModifiers, Enums.ModifierType.withEpisodes, True)
        tmpShow.ScrapeOptions = Master.eSettings.DefaultScrapeOptions(Enums.ContentType.TV)
        tmpShow.ScrapeType = Enums.ScrapeType.Manually
        Dim nResult = Scraper.Run(tmpShow)
        If nResult IsNot Nothing Then
            If tmpShow.Episodes.Count > 0 Then
                Dim dlgChangeEp As New dlgTVChangeTVEpisode(tmpShow)
                If dlgChangeEp.ShowDialog = DialogResult.OK Then
                    If dlgChangeEp.Result.Count > 0 Then
                        If Master.eSettings.TVScraperMetaDataScan Then
                            MetaData.UpdateFileInfo(tmpEpisode)
                        End If
                        Master.DB.Change_TVEpisode(tmpEpisode, dlgChangeEp.Result, False)
                    End If
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(943, "There are no known episodes for this show. Scrape the show, season, or episode and try again."), Master.eLang.GetString(944, "No Known Episodes"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        DataGridView_RefreshRow_TVShow(ShowID, True)

        SetControlsEnabled(True)
    End Sub

    Private Sub cmnuShowGetMissingEpisodes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowGetMissingEpisodes.Click
        If dgvTVShows.SelectedRows.Count > 0 Then
            Dim lItemsToChange As New List(Of Long)
            For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                lItemsToChange.Add(Convert.ToInt64(sRow.Cells("idShow").Value))
            Next

            Master.TaskManager.AddTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.GetMissingEpisodes) With {
                                       .ListOfID = lItemsToChange,
                                       .ContentType = Enums.ContentType.TVShow
                                       })
        End If
    End Sub

    Private Sub cmnuShowChange_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowChange.Click
        If dgvTVShows.SelectedRows.Count = 1 Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.DoSearch, True)
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withEpisodes, True)
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withSeasons, True)
            TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                                   .ContentType = Enums.ContentType.TVShow,
                                   .ScrapeModifiers = ScrapeModifiers,
                                   .ScrapeOptions = Master.eSettings.DefaultScrapeOptions(Enums.ContentType.TVShow),
                                   .ScrapeType = Enums.ScrapeType.Manually,
                                   .SelectionType = Enums.SelectionType.Selected
                                   })
        End If
    End Sub

    Private Sub cmnuSeasonRemoveFromDisk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSeasonRemoveFromDisk.Click
        Dim lstTVSeasonID As New List(Of Long)

        For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
            Dim lngID As Long = Convert.ToInt64(sRow.Cells("idSeason").Value)
            If Not lstTVSeasonID.Contains(lngID) Then lstTVSeasonID.Add(lngID)
        Next

        If lstTVSeasonID.Count > 0 Then
            Using dlg As New dlgDeleteConfirm
                If dlg.ShowDialog(lstTVSeasonID, Enums.ContentType.TVSeason) = DialogResult.OK Then
                    DataGridView_Fill_TVSeason(Convert.ToInt64(dgvTVSeasons.Item("idShow", currRow_TVSeason).Value))
                    MainTab_SetCount_TV()
                End If
            End Using
        End If
    End Sub

    Private Sub cmnuEpisodeRemoveFromDisk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuEpisodeRemoveFromDisk.Click
        Dim lstTVEpisodeID As New List(Of Long)

        For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
            Dim lngID As Long = Convert.ToInt64(sRow.Cells("idEpisode").Value)
            If Not lstTVEpisodeID.Contains(lngID) Then lstTVEpisodeID.Add(lngID)
        Next

        If lstTVEpisodeID.Count > 0 Then
            Using dlg As New dlgDeleteConfirm
                If dlg.ShowDialog(lstTVEpisodeID, Enums.ContentType.TVEpisode) = DialogResult.OK Then
                    DataGridView_Fill_TVEpisode(Convert.ToInt64(dgvTVSeasons.Item("idShow", currRow_TVSeason).Value), Convert.ToInt32(dgvTVSeasons.Item("Season", currRow_TVSeason).Value))
                    MainTab_SetCount_TV()
                End If
            End Using
        End If
    End Sub

    Private Sub cmnuShowRemoveFromDisk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowRemoveFromDisk.Click
        Dim lstTVShowID As New List(Of Long)

        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
            Dim lngID As Long = Convert.ToInt64(sRow.Cells("idShow").Value)
            If Not lstTVShowID.Contains(lngID) Then lstTVShowID.Add(lngID)
        Next

        If lstTVShowID.Count > 0 Then
            Using dlg As New dlgDeleteConfirm
                If dlg.ShowDialog(lstTVShowID, Enums.ContentType.TVShow) = DialogResult.OK Then
                    DataGridView_Fill_Main(False, False, True)
                End If
            End Using
        End If
    End Sub

    Private Sub cmnuEpisodeEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuEpisodeEdit.Click
        If dgvTVEpisodes.SelectedRows.Count > 1 Then Return

        Dim indX As Integer = dgvTVEpisodes.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvTVEpisodes.Item("idEpisode", indX).Value)
        Dim tmpDBTVEpisode As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
        Edit_TVEpisode(tmpDBTVEpisode)
    End Sub

    Private Sub cmnuMovieEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieEdit.Click
        If dgvMovies.SelectedRows.Count > 1 Then Return

        Dim indX As Integer = dgvMovies.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvMovies.Item(Database.Helpers.GetMainIdName(Database.TableName.movie), indX).Value)
        Dim tmpDBMovie As Database.DBElement = Master.DB.Load_Movie(ID)
        Edit_Movie(tmpDBMovie)
    End Sub

    Private Sub cmnuShowEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowEdit.Click
        If dgvTVShows.SelectedRows.Count > 1 Then Return

        Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvTVShows.Item("idShow", indX).Value)
        Dim tmpDBMTVShow As Database.DBElement = Master.DB.Load_TVShow(ID, True, False)
        Edit_TVShow(tmpDBMTVShow)
    End Sub

    Private Sub cmnuEpisodeOpenFolder_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuEpisodeOpenFolder.Click
        If dgvTVEpisodes.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            Dim ePath As String = String.Empty

            If dgvTVEpisodes.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVEpisodes.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If

            If doOpen Then
                Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                        If Not Convert.ToInt64(sRow.Cells("idFile").Value) = -1 Then
                            SQLCommand.CommandText = String.Concat("SELECT path FROM file WHERE idFile = ", sRow.Cells("idFile").Value.ToString, ";")
                            ePath = SQLCommand.ExecuteScalar.ToString

                            If Not String.IsNullOrEmpty(ePath) Then
                                Using Explorer As New Process
                                    Explorer.StartInfo.FileName = "explorer.exe"
                                    Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", ePath)
                                    Explorer.Start()
                                End Using
                            End If
                        End If
                    Next
                End Using
            End If
        End If
    End Sub

    Private Sub cmnuMovieLock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieLock.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetLockedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = Enums.ContentType.Movie,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuMovieUnlock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieUnlock.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetLockedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = Enums.ContentType.Movie,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuMovieMark_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieMark.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetMarkedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = Enums.ContentType.Movie,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuMovieUnmark_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieUnmark.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetMarkedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = Enums.ContentType.Movie,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuMovieWatched_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieWatched.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetWatchedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = Enums.ContentType.Movie,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuMovieUnwatched_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieUnwatched.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetWatchedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = Enums.ContentType.Movie,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuMoviesetLock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMoviesetLock.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetLockedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = Enums.ContentType.Movieset,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuMoviesetUnlock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMoviesetUnlock.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetLockedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = Enums.ContentType.Movieset,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuMoviesetMark_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMoviesetMark.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetMarkedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = Enums.ContentType.Movieset,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuMoviesetUnmark_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMoviesetUnmark.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetMarkedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = Enums.ContentType.Movieset,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuEpisodeLock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuEpisodeLock.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetLockedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = Enums.ContentType.TVEpisode,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuEpisodeUnlock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuEpisodeUnlock.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetLockedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = Enums.ContentType.TVEpisode,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuEpisodeMark_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuEpisodeMark.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetMarkedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = Enums.ContentType.TVEpisode,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuEpisodeUnmark_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuEpisodeUnmark.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetMarkedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = Enums.ContentType.TVEpisode,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuEpisodeWatched_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuEpisodeWatched.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetWatchedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = Enums.ContentType.TVEpisode,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuEpisodeUnwatched_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuEpisodeUnwatched.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetWatchedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = Enums.ContentType.TVEpisode,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuSeasonLock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSeasonLock.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetLockedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = Enums.ContentType.TVSeason,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuSeasonUnlock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSeasonUnlock.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetLockedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = Enums.ContentType.TVSeason,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuSeasonMark_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSeasonMark.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetMarkedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = Enums.ContentType.TVSeason,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuSeasonUnmark_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSeasonUnmark.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetMarkedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = Enums.ContentType.TVSeason,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuSeasonWatched_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSeasonWatched.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetWatchedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = Enums.ContentType.TVSeason,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuSeasonUnwatched_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSeasonUnwatched.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetWatchedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = Enums.ContentType.TVSeason,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuShowLock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowLock.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetLockedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = Enums.ContentType.TVShow,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuShowUnlock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowUnlock.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetLockedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = Enums.ContentType.TVShow,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuShowMark_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowMark.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetMarkedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = Enums.ContentType.TVShow,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuShowUnmark_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowUnmark.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetMarkedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = Enums.ContentType.TVShow,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuShowWatched_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowWatched.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetWatchedState) With {
                               .CommonBooleanValue = True,
                               .ContentType = Enums.ContentType.TVShow,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuShowUnwatched_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowUnwatched.Click
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetWatchedState) With {
                               .CommonBooleanValue = False,
                               .ContentType = Enums.ContentType.TVShow,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub

    Private Sub cmnuMovieMarkAsCustom1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieMarkAsCustom1.Click
        Dim setMark As Boolean = False
        If dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
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
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, Database.Helpers.GetMainIdName(Database.TableName.movie))
                SQLcommand.CommandText = "UPDATE movie SET MarkCustom1 = (?) WHERE idMovie = (?);"
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    parMark.Value = If(dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("MarkCustom1").Value))
                    parID.Value = sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("MarkCustom1").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        If chkFilterMarkCustom1_Movies.Checked Then
            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing
            If dgvMovies.RowCount <= 0 Then ClearInfo()
        End If

        dgvMovies.Invalidate()
    End Sub

    Private Sub cmnuMovieMarkAsCustom2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieMarkAsCustom2.Click
        Dim setMark As Boolean = False
        If dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
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
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, Database.Helpers.GetMainIdName(Database.TableName.movie))
                SQLcommand.CommandText = "UPDATE movie SET MarkCustom2 = (?) WHERE idMovie = (?);"
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    parMark.Value = If(dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("MarkCustom2").Value))
                    parID.Value = sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("MarkCustom2").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        If chkFilterMarkCustom2_Movies.Checked Then
            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing
            If dgvMovies.RowCount <= 0 Then ClearInfo()
        End If

        dgvMovies.Invalidate()
    End Sub

    Private Sub cmnuMovieMarkAsCustom3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieMarkAsCustom3.Click
        Dim setMark As Boolean = False
        If dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
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
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, Database.Helpers.GetMainIdName(Database.TableName.movie))
                SQLcommand.CommandText = "UPDATE movie SET MarkCustom3 = (?) WHERE idMovie = (?);"
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    parMark.Value = If(dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("MarkCustom3").Value))
                    parID.Value = sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("MarkCustom3").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        If chkFilterMarkCustom3_Movies.Checked Then
            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing
            If dgvMovies.RowCount <= 0 Then ClearInfo()
        End If

        dgvMovies.Invalidate()
    End Sub

    Private Sub cmnuMovieMarkAsCustom4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieMarkAsCustom4.Click
        Dim setMark As Boolean = False
        If dgvMovies.SelectedRows.Count > 1 Then
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
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
                Dim parID As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parID", DbType.Int64, 0, Database.Helpers.GetMainIdName(Database.TableName.movie))
                SQLcommand.CommandText = "UPDATE movie SET MarkCustom4 = (?) WHERE idMovie = (?);"
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    parMark.Value = If(dgvMovies.SelectedRows.Count > 1, setMark, Not Convert.ToBoolean(sRow.Cells("MarkCustom4").Value))
                    parID.Value = sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value
                    SQLcommand.ExecuteNonQuery()
                    sRow.Cells("MarkCustom4").Value = parMark.Value
                Next
            End Using
            SQLtransaction.Commit()
        End Using

        If chkFilterMarkCustom4_Movies.Checked Then
            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing
            If dgvMovies.RowCount <= 0 Then ClearInfo()
        End If

        dgvMovies.Invalidate()
    End Sub

    Private Sub cmnuEpisodeEditMetaData_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuEpisodeEditMetadata.Click
        If dgvTVEpisodes.SelectedRows.Count > 1 Then Return
        Dim indX As Integer = dgvTVEpisodes.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvTVEpisodes.Item(Database.Helpers.GetMainIdName(Database.TableName.episode), indX).Value)
        Dim DBElement As Database.DBElement = Master.DB.Load_TVEpisode(ID, False)
        Using dFileInfo As New dlgFileInfo(DBElement.MainDetails.FileInfo)
            If dFileInfo.ShowDialog() = DialogResult.OK Then
                DBElement.MainDetails.FileInfo = dFileInfo.Result
                Master.DB.Save_TVEpisode(DBElement, False, True, False, False, True)
                DataGridView_RefreshRow_TVEpisode(ID)
            End If
        End Using
    End Sub

    Private Sub cmnuMovieEditMetaData_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieEditMetadata.Click
        If dgvMovies.SelectedRows.Count > 1 Then Return
        Dim indX As Integer = dgvMovies.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvMovies.Item(Database.Helpers.GetMainIdName(Database.TableName.movie), indX).Value)
        Dim DBElement As Database.DBElement = Master.DB.Load_Movie(ID)
        Using dFileInfo As New dlgFileInfo(DBElement.MainDetails.FileInfo)
            If dFileInfo.ShowDialog() = DialogResult.OK Then
                DBElement.MainDetails.FileInfo = dFileInfo.Result
                Master.DB.Save_Movie(DBElement, False, True, False, True, False)
                DataGridView_RefreshRow_Movie(ID)
            End If
        End Using
    End Sub

    Private Sub cmnuMovieReload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieReload.Click
        dgvMovies.Cursor = Cursors.WaitCursor
        SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        Dim showMessages As Boolean = dgvMovies.SelectedRows.Count = 1

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                If Reload_Movie(Convert.ToInt64(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value), True, showMessages) Then
                    doFill = True
                Else
                    DataGridView_RefreshRow_Movie(Convert.ToInt64(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value))
                End If
            Next
            SQLtransaction.Commit()
        End Using

        dgvMovies.Cursor = Cursors.Default
        SetControlsEnabled(True)

        If doFill Then
            DataGridView_Fill_Main(True, True, False)
        Else
            DoTitleCheck()
        End If
    End Sub

    Private Sub cmnuMovieSetEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieSetEdit.Click
        If dgvMoviesets.SelectedRows.Count > 1 Then Return

        Dim indX As Integer = dgvMoviesets.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvMoviesets.Item("idSet", indX).Value)
        Dim tmpDBMovieSet As Database.DBElement = Master.DB.Load_Movieset(ID)
        Edit_Movieset(tmpDBMovieSet)
    End Sub

    Private Sub cmnuMovieSetNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieSetNew.Click
        dgvMoviesets.ClearSelection()
        ClearInfo()

        Dim tmpDBMovieSet = New Database.DBElement(Enums.ContentType.Movieset)

        Using dNewSet As New dlgNewSet()
            If dNewSet.ShowDialog(tmpDBMovieSet) = DialogResult.OK Then
                tmpDBMovieSet = Master.DB.Save_Movieset(dNewSet.Result, False, False, False, False)
                Dim iNewRowIndex = DataGridView_AddRow_Movieset(tmpDBMovieSet.ID)
                If Not iNewRowIndex = -1 Then
                    dgvMoviesets.Rows(iNewRowIndex).Selected = True
                End If
                Edit_Movieset(tmpDBMovieSet)
            End If
        End Using

    End Sub

    Private Sub cmnuMovieSetReload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieSetReload.Click
        dgvMoviesets.Cursor = Cursors.WaitCursor
        SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In dgvMoviesets.SelectedRows
                If Reload_Movieset(Convert.ToInt64(sRow.Cells("idSet").Value), True) Then
                    doFill = True
                Else
                    DataGridView_RefreshRow_Movieset(Convert.ToInt64(sRow.Cells("idSet").Value))
                End If
            Next
            SQLtransaction.Commit()
        End Using

        dgvMoviesets.Cursor = Cursors.Default
        SetControlsEnabled(True)

        If doFill Then DataGridView_Fill_Main(False, True, False)
    End Sub

    Private Sub cmnuMovieSetRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieSetRemove.Click
        Dim lItemsToRemove As New List(Of Long)
        ClearInfo()

        For Each sRow As DataGridViewRow In dgvMoviesets.SelectedRows
            lItemsToRemove.Add(Convert.ToInt64(sRow.Cells("idSet").Value))
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each tID As Long In lItemsToRemove
                Master.DB.Remove_MovieSet(tID, True)
                DataGridView_RemoveRow_Movieset(tID)
            Next
            SQLtransaction.Commit()
        End Using

        DataGridView_Fill_Main(True, False, False)
    End Sub

    Private Sub cmnuEpisodeReload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuEpisodeReload.Click
        dgvTVShows.Cursor = Cursors.WaitCursor
        dgvTVSeasons.Cursor = Cursors.WaitCursor
        dgvTVEpisodes.Cursor = Cursors.WaitCursor
        SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If dgvTVEpisodes.SelectedRows.Count > 0 Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                    If Not Convert.ToInt64(sRow.Cells("idFile").Value) = -1 Then 'skipping missing episodes
                        If Reload_TVEpisode(Convert.ToInt64(sRow.Cells("idEpisode").Value), True, dgvTVEpisodes.SelectedRows.Count = 1) Then
                            doFill = True
                        Else
                            DataGridView_RefreshRow_TVEpisode(Convert.ToInt64(sRow.Cells("idEpisode").Value))
                        End If
                    End If
                Next
                SQLtransaction.Commit()
            End Using
        End If

        dgvTVShows.Cursor = Cursors.Default
        dgvTVSeasons.Cursor = Cursors.Default
        dgvTVEpisodes.Cursor = Cursors.Default
        SetControlsEnabled(True)

        If doFill Then DataGridView_Fill_TVEpisode(Convert.ToInt64(dgvTVEpisodes.SelectedRows(0).Cells("idEpisode").Value), Convert.ToInt32(dgvTVEpisodes.SelectedRows(0).Cells("Season").Value))
    End Sub

    Private Sub cmnuSeasonReload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSeasonReload.Click
        dgvTVShows.Cursor = Cursors.WaitCursor
        dgvTVSeasons.Cursor = Cursors.WaitCursor
        dgvTVEpisodes.Cursor = Cursors.WaitCursor
        SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If dgvTVSeasons.SelectedRows.Count > 0 Then
            tspbLoading.Style = ProgressBarStyle.Continuous
            tspbLoading.Value = 0
            tspbLoading.Maximum = dgvTVSeasons.SelectedRows.Count

            tslLoading.Text = String.Concat(Master.eLang.GetString(563, "Reloading Season"), ":")
            tslLoading.Visible = True
            tspbLoading.Visible = True
            Application.DoEvents()

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                    tspbLoading.Value += 1
                    If Reload_TVSeason(Convert.ToInt64(sRow.Cells("idSeason").Value), True, dgvTVSeasons.SelectedRows.Count = 1, False) Then
                        doFill = True
                    Else
                        DataGridView_RefreshRow_TVSeason(Convert.ToInt64(sRow.Cells("idSeason").Value))
                    End If
                Next
                SQLtransaction.Commit()
            End Using

            tslLoading.Visible = False
            tspbLoading.Visible = False
        End If

        dgvTVShows.Cursor = Cursors.Default
        dgvTVSeasons.Cursor = Cursors.Default
        dgvTVEpisodes.Cursor = Cursors.Default
        SetControlsEnabled(True)

        If doFill Then DataGridView_Fill_TVSeason(Convert.ToInt64(dgvTVSeasons.SelectedRows(0).Cells("idShow").Value))
    End Sub

    Private Sub cmnuSeasonReloadFull_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSeasonReloadFull.Click
        dgvTVShows.Cursor = Cursors.WaitCursor
        dgvTVSeasons.Cursor = Cursors.WaitCursor
        dgvTVEpisodes.Cursor = Cursors.WaitCursor
        SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If dgvTVSeasons.SelectedRows.Count > 0 Then
            tspbLoading.Style = ProgressBarStyle.Continuous
            tspbLoading.Value = 0
            tspbLoading.Maximum = dgvTVSeasons.SelectedRows.Count

            tslLoading.Text = String.Concat(Master.eLang.GetString(563, "Reloading Season"), ":")
            tslLoading.Visible = True
            tspbLoading.Visible = True
            Application.DoEvents()

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                    tspbLoading.Value += 1
                    If Reload_TVSeason(Convert.ToInt64(sRow.Cells("idSeason").Value), True, dgvTVSeasons.SelectedRows.Count = 1, False) Then
                        doFill = True
                    Else
                        DataGridView_RefreshRow_TVSeason(Convert.ToInt64(sRow.Cells("idSeason").Value))
                    End If
                Next
                SQLtransaction.Commit()
            End Using

            tslLoading.Visible = False
            tspbLoading.Visible = False
        End If

        dgvTVShows.Cursor = Cursors.Default
        dgvTVSeasons.Cursor = Cursors.Default
        dgvTVEpisodes.Cursor = Cursors.Default
        SetControlsEnabled(True)

        If doFill Then DataGridView_Fill_TVSeason(Convert.ToInt64(dgvTVSeasons.SelectedRows(0).Cells("idShow").Value))
    End Sub

    Private Sub cmnuShowReload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowReload.Click
        dgvTVShows.Cursor = Cursors.WaitCursor
        dgvTVSeasons.Cursor = Cursors.WaitCursor
        dgvTVEpisodes.Cursor = Cursors.WaitCursor
        SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If dgvTVShows.SelectedRows.Count > 1 Then
            tspbLoading.Style = ProgressBarStyle.Continuous
            tspbLoading.Value = 0
            tspbLoading.Maximum = dgvTVShows.SelectedRows.Count

            tslLoading.Text = String.Concat(Master.eLang.GetString(562, "Reloading Show"), ":")
            tslLoading.Visible = True
            tspbLoading.Visible = True
            Application.DoEvents()

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                    tspbLoading.Value += 1
                    If Reload_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), True, dgvTVShows.SelectedRows.Count = 1, False) Then
                        doFill = True
                    Else
                        DataGridView_RefreshRow_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value))
                    End If
                Next
                SQLtransaction.Commit()
            End Using

            tslLoading.Visible = False
            tspbLoading.Visible = False
        ElseIf dgvTVShows.SelectedRows.Count = 1 Then
            If Reload_TVShow(Convert.ToInt64(dgvTVShows.SelectedRows(0).Cells("idShow").Value), False, True, False) Then
                doFill = True
            Else
                DataGridView_RefreshRow_TVShow(Convert.ToInt64(dgvTVShows.SelectedRows(0).Cells("idShow").Value))
            End If
        End If

        dgvTVShows.Cursor = Cursors.Default
        dgvTVSeasons.Cursor = Cursors.Default
        dgvTVEpisodes.Cursor = Cursors.Default
        SetControlsEnabled(True)

        If doFill Then DataGridView_Fill_Main(False, False, True)
    End Sub

    Private Sub cmnuShowReloadFull_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowReloadFull.Click
        dgvTVShows.Cursor = Cursors.WaitCursor
        dgvTVSeasons.Cursor = Cursors.WaitCursor
        dgvTVEpisodes.Cursor = Cursors.WaitCursor
        SetControlsEnabled(False, True)

        Dim doFill As Boolean = False

        If dgvTVShows.SelectedRows.Count > 1 Then
            tspbLoading.Style = ProgressBarStyle.Continuous
            tspbLoading.Value = 0
            tspbLoading.Maximum = dgvTVShows.SelectedRows.Count

            tslLoading.Text = String.Concat(Master.eLang.GetString(562, "Reloading Show"), ":")
            tslLoading.Visible = True
            tspbLoading.Visible = True
            Application.DoEvents()

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                    tspbLoading.Value += 1
                    If Reload_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), True, dgvTVShows.SelectedRows.Count = 1, True) Then
                        doFill = True
                    Else
                        DataGridView_RefreshRow_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value))
                    End If
                Next
                SQLtransaction.Commit()
            End Using

            tslLoading.Visible = False
            tspbLoading.Visible = False
        ElseIf dgvTVShows.SelectedRows.Count = 1 Then
            If Reload_TVShow(Convert.ToInt64(dgvTVShows.SelectedRows(0).Cells("idShow").Value), False, True, True) Then
                doFill = True
            Else
                DataGridView_RefreshRow_TVShow(Convert.ToInt64(dgvTVShows.SelectedRows(0).Cells("idShow").Value))
            End If
        End If

        dgvTVShows.Cursor = Cursors.Default
        dgvTVSeasons.Cursor = Cursors.Default
        dgvTVEpisodes.Cursor = Cursors.Default
        SetControlsEnabled(True)

        If doFill Then DataGridView_Fill_Main(False, False, True)
    End Sub

    Private Sub cmnuSeasonRemoveFromDB_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSeasonRemoveFromDB.Click
        Dim lItemsToRemove As New List(Of Long)
        ClearInfo()

        For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
            lItemsToRemove.Add(Convert.ToInt64(sRow.Cells("idSeason").Value))
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Dim idShow As Integer = CInt(dgvTVSeasons.SelectedRows(0).Cells("idShow").Value)
            For Each tID As Long In lItemsToRemove
                If Not tID = -1 Then
                    Master.DB.Remove_TVSeason(tID, True)
                    DataGridView_RemoveRow_TVSeason(tID)
                End If
            Next
            Reload_TVShow(idShow, True, True, False) 'TODO: check if needed
            SQLtransaction.Commit()
        End Using

        MainTab_SetCount_TV()
    End Sub

    Private Sub cmnuEpisodeRemoveFromDB_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuEpisodeRemoveFromDB.Click
        Dim lItemsToRemove As New Dictionary(Of Long, Boolean)
        Dim SeasonsList As New List(Of Integer)
        ClearInfo()

        For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
            If Not SeasonsList.Contains(CInt(sRow.Cells("Season").Value)) Then SeasonsList.Add(CInt(sRow.Cells("Season").Value))
            lItemsToRemove.Add(Convert.ToInt64(sRow.Cells("idEpisode").Value), Convert.ToInt64(sRow.Cells("idFile").Value) = -1)
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Dim idShow As Integer = CInt(dgvTVEpisodes.SelectedRows(0).Cells("idShow").Value)
            For Each tID As KeyValuePair(Of Long, Boolean) In lItemsToRemove
                If tID.Value Then
                    Master.DB.Remove_TVEpisode(tID.Key, True, False, True) 'remove the "missing episode" from DB
                    DataGridView_RemoveRow_TVEpisode(tID.Key)
                Else
                    If Master.DB.Remove_TVEpisode(tID.Key, False, False, True) Then 'set the episode as "missing episode"
                        DataGridView_RemoveRow_TVEpisode(tID.Key)
                    Else
                        DataGridView_RefreshRow_TVEpisode(tID.Key)
                    End If
                End If
            Next

            For Each iSeason In SeasonsList
                DataGridView_RefreshRow_TVSeason(idShow, iSeason)
            Next
            DataGridView_RefreshRow_TVShow(idShow)

            SQLtransaction.Commit()
        End Using

        MainTab_SetCount_TV()
    End Sub

    Private Sub cmnuShowRemoveFromDB_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowRemoveFromDB.Click
        Dim lItemsToRemove As New List(Of Long)
        ClearInfo()

        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
            lItemsToRemove.Add(Convert.ToInt64(sRow.Cells("idShow").Value))
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each tID As Long In lItemsToRemove
                Master.DB.Remove_TVShow(tID, True)
                DataGridView_RemoveRow_TVShow(tID)
            Next
            SQLtransaction.Commit()
        End Using

        MainTab_SetCount_TV()
    End Sub

    Private Sub cmnuEpisodeRescrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuEpisodeScrape.Click
        If dgvTVEpisodes.SelectedRows.Count = 1 Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
            'CreateScrapeList_TVEpisode(Enums.ScrapeType.SingleScrape, Master.eSettings.DefaultScrapeOptions(Enums.ContentType.TV), ScrapeModifiers)
            'TODO
        End If
    End Sub

    Private Sub cmnuMovieRescrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieScrape.Click
        If dgvMovies.SelectedRows.Count = 1 Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
            'CreateScrapeList_Movie(Enums.ScrapeType.SingleScrape, Master.eSettings.DefaultScrapeOptions(Enums.ContentType.Movie), ScrapeModifiers)
        End If
    End Sub

    Private Sub cmnuMovieSetRescrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieSetScrape.Click
        If dgvMoviesets.SelectedRows.Count = 1 Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
            'CreateScrapeList_Movieset(Enums.ScrapeType.SingleScrape, Master.eSettings.DefaultScrapeOptions(Enums.ContentType.Movieset), ScrapeModifiers)
        End If
    End Sub

    Private Sub cmnuShowRescrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuShowScrape.Click
        If dgvTVShows.SelectedRows.Count > 0 Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withEpisodes, True)
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withSeasons, True)
            'CreateScrapeList_TV(Enums.ScrapeType.SingleScrape, Master.eSettings.DefaultScrapeOptions(Enums.ContentType.TV), ScrapeModifiers)
        End If
    End Sub
    ''' <summary>
    ''' User has selected "Change Movie" from the context menu. This will re-validate the movie title with the user,
    ''' and initiate a new manually scrape of the movie.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmnuMovieChange_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieChange.Click
        If dgvMovies.SelectedRows.Count <> 1 Then Return 'This method is only valid for when exactly one movie is selected
        Dim ScrapeModifiers As New Structures.ScrapeModifiers
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.DoSearch, True)
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
        'CreateScrapeList_Movie(Enums.ScrapeType.SingleScrape, Master.eSettings.DefaultScrapeOptions(Enums.ContentType.Movie), ScrapeModifiers)
    End Sub
    ''' <summary>
    ''' User has selected "Change Movie" from the context menu. This will re-validate the movie title with the user,
    ''' and initiate a new auto scrape of the movie.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmnuMovieChangeAuto_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieChangeAuto.Click
        If dgvMovies.SelectedRows.Count <> 1 Then Return 'This method is only valid for when exactly one movie is selected
        Dim ScrapeModifiers As New Structures.ScrapeModifiers
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.DoSearch, True)
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
        'CreateScrapeList_Movie(Enums.ScrapeType.SingleAuto, Master.eSettings.DefaultScrapeOptions(Enums.ContentType.Movie), ScrapeModifiers)
    End Sub

    Private Sub cmnuSeasonEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSeasonEdit.Click
        Dim indX As Integer = dgvTVSeasons.SelectedRows(0).Index
        Dim ID As Long = Convert.ToInt64(dgvTVSeasons.Item("idSeason", indX).Value)
        Dim tmpDBTVSeason As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)
        Edit_TVSeason(tmpDBTVSeason)
    End Sub

    Private Sub cmnuSeasonOpenFolder_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSeasonOpenFolder.Click
        If dgvTVSeasons.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            Dim SeasonPath As String = String.Empty

            If dgvTVSeasons.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVSeasons.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If

            If doOpen Then
                For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                    SeasonPath = FileUtils.Common.GetSeasonDirectoryFromShowPath(_CurrDBElement.ShowPath, Convert.ToInt32(sRow.Cells("Season").Value))

                    Using Explorer As New Process
                        Explorer.StartInfo.FileName = "explorer.exe"
                        If String.IsNullOrEmpty(SeasonPath) Then
                            Explorer.StartInfo.Arguments = String.Format("/root,""{0}""", _CurrDBElement.ShowPath)
                        Else
                            Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", SeasonPath)
                        End If
                        Explorer.Start()
                    End Using
                Next
            End If
        End If
    End Sub

    Private Sub cmnuSeasonRescrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSeasonScrape.Click
        If dgvTVSeasons.SelectedRows.Count > 0 Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
            'CreateScrapeList_TVSeason(Enums.ScrapeType.SingleScrape, Master.eSettings.DefaultScrapeOptions(Enums.ContentType.TV), ScrapeModifiers)
        End If
    End Sub

    Private Sub mnuMainToolsSortFiles_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsSortFiles.Click, cmnuTrayToolsSortFiles.Click
        SetControlsEnabled(False)
        Using dSortFiles As New dlgSortFiles
            dSortFiles.ShowDialog()
            SetControlsEnabled(True)
        End Using
    End Sub

    Private Sub mnuMainToolsBackdrops_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsBackdrops.Click, cmnuTrayToolsBackdrops.Click
        Master.TaskManager.AddTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.CopyBackdrops) With {.ContentType = Enums.ContentType.Movie})
    End Sub
    ''' <summary>
    ''' Enable or disable all items in a ContextMenuStrip
    ''' </summary>
    ''' <param name="ContextMenu">Context menu that contains the items to control</param>
    ''' <param name="Enable">Enable or disable all items</param>
    Private Sub ContextMenuStrip_EnableItems(ByRef ContextMenu As ContextMenuStrip, ByVal Enable As Boolean)
        For Each cItem As Object In ContextMenu.Items
            Select Case True
                Case TypeOf cItem Is ContextMenuStrip
                    Dim cmsItem As ContextMenuStrip = DirectCast(cItem, ContextMenuStrip)
                    cmsItem.Enabled = Enable
                Case TypeOf cItem Is ToolStripMenuItem
                    Dim cmsItem As ToolStripMenuItem = DirectCast(cItem, ToolStripMenuItem)
                    cmsItem.Enabled = Enable
                Case TypeOf cItem Is ToolStripSeparator
                    Dim cmsItem As ToolStripSeparator = DirectCast(cItem, ToolStripSeparator)
                    cmsItem.Enabled = Enable
            End Select
        Next
    End Sub
    ''' <summary>
    ''' Populate the form's Genre panel and picture box arrays with the 
    ''' appropriate genre images and (conditionally) labels 
    ''' </summary>
    ''' <param name="genres"><c>List (Of String)</c> holding genre names</param>
    ''' <remarks>If any individual genre is invalid or generates an error, 
    ''' the remaining genres are still processed, however the placement/spacing
    ''' of the remaining genres may show gaps where the erronious genres should have been</remarks>
    Private Sub CreateGenreThumbs(ByVal genres As List(Of String))
        If genres Is Nothing OrElse genres.Count = 0 Then Return

        genres.Sort()
        genres.Reverse()

        For i As Integer = 0 To genres.Count - 1
            Try
                ReDim Preserve pnlGenre(i)
                ReDim Preserve pbGenre(i)
                pnlGenre(i) = New Panel()
                pnlGenre(i).Visible = False
                pbGenre(i) = New PictureBox()
                pbGenre(i).Name = genres(i).Trim.ToUpper
                pnlGenre(i).Size = New Size(68, 100)
                pbGenre(i).Size = New Size(62, 94)
                pnlGenre(i).BackColor = GenrePanelColor
                pbGenre(i).BackColor = GenrePanelColor
                pnlGenre(i).BorderStyle = BorderStyle.FixedSingle
                pbGenre(i).SizeMode = PictureBoxSizeMode.StretchImage
                pbGenre(i).Image = MediaFlags.GetGenreImage(genres(i).Trim)
                pnlGenre(i).Left = ((pnlInfoPanel.Right) - (i * 73)) - 73
                pbGenre(i).Left = 2
                pnlGenre(i).Top = pnlInfoPanel.Top - 105
                pbGenre(i).Top = 2
                scMain.Panel2.Controls.Add(pnlGenre(i))
                pnlGenre(i).Controls.Add(pbGenre(i))
                pnlGenre(i).BringToFront()
                AddHandler pbGenre(i).MouseEnter, AddressOf pbGenre_MouseEnter
                AddHandler pbGenre(i).MouseLeave, AddressOf pbGenre_MouseLeave
                If Manager.mSettings.MainOptions.GuiSettings.DisplayGenreText Then
                    pbGenre(i).Image = ImageUtils.AddGenreString(pbGenre(i).Image, pbGenre(i).Name)
                End If
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
    End Sub

    Private Sub cmnuMovieRemoveFromDisk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieRemoveFromDisk.Click
        Dim lstMovieID As New List(Of Long)

        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
            Dim lngID As Long = Convert.ToInt64(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value)
            If Not lstMovieID.Contains(lngID) Then lstMovieID.Add(lngID)
        Next

        If lstMovieID.Count > 0 Then
            Using dlg As New dlgDeleteConfirm
                If dlg.ShowDialog(lstMovieID, Enums.ContentType.Movie) = DialogResult.OK Then
                    DataGridView_Fill_Main(True, True, False)
                End If
            End Using
        End If
    End Sub

    Private Sub ShowEpisodeMenuItems(ByVal Visible As Boolean)
        Dim cMnu As ToolStripMenuItem
        Dim cSep As ToolStripSeparator

        If Visible Then
            For Each cMnuItem As Object In cmnuEpisode.Items
                If TypeOf cMnuItem Is ToolStripMenuItem Then
                    DirectCast(cMnuItem, ToolStripMenuItem).Visible = True
                ElseIf TypeOf cMnuItem Is ToolStripSeparator Then
                    DirectCast(cMnuItem, ToolStripSeparator).Visible = True
                End If
            Next
            cmnuEpisodeRemoveFromDisk.Visible = True
        Else
            For Each cMnuItem As Object In cmnuEpisode.Items
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
                cmnuEpisodeRemoveFromDisk.Visible = False
            Next
        End If
    End Sub

    Private Sub mnuMainDonatePatreon_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainDonatePatreon.Click
        Process.Start("https://www.patreon.com/embermediamanager")
    End Sub

    Private Sub mnuMainDonatePayPal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainDonatePayPal.Click
        Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=VWVJCUV3KAUX2&lc=CH&item_name=Ember%20Media%20Manager&currency_code=CHF&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted")
    End Sub

    Private Sub DoTitleCheck()
        Master.TaskManager.AddTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.DoTitleCheck) With {
                                   .ContentType = Enums.ContentType.Movie
                                   })
    End Sub

    Private Sub EditDataFields_Click(sender As Object, e As EventArgs) Handles _
        cmnuEpisodeEditDataFields.Click,
        cmnuMovieEditDataFields.Click,
        cmnuMovieSetEditDataFields.Click,
        cmnuSeasonEditDataFields.Click,
        cmnuShowEditDataFields.Click

        Dim strContentType As String = DirectCast(sender, ToolStripMenuItem).Tag.ToString

        If Not String.IsNullOrEmpty(strContentType) Then
            Dim eContentType As Enums.ContentType

            Select Case strContentType
                Case "movie"
                    eContentType = Enums.ContentType.Movie
                Case "movieset"
                    eContentType = Enums.ContentType.Movieset
                Case "tvepisode"
                    eContentType = Enums.ContentType.TVEpisode
                Case "tvseason"
                    eContentType = Enums.ContentType.TVSeason
                Case "tvshow"
                    eContentType = Enums.ContentType.TVShow
                Case Else
                    eContentType = Enums.ContentType.None
            End Select

            If Not eContentType = Enums.ContentType.None Then
                Using dEditDataField As New dlgClearOrReplace
                    If dEditDataField.ShowDialog(eContentType) = DialogResult.OK Then
                        Dim nTaskItem = dEditDataField.Result
                        nTaskItem.ListOfID = New List(Of Long)
                        Select Case eContentType
                            Case Enums.ContentType.Movie
                                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                                    nTaskItem.ListOfID.Add(Convert.ToInt64(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value))
                                Next
                            Case Enums.ContentType.Movieset
                                For Each sRow As DataGridViewRow In dgvMoviesets.SelectedRows
                                    nTaskItem.ListOfID.Add(Convert.ToInt64(sRow.Cells("idSet").Value))
                                Next
                            Case Enums.ContentType.TVEpisode
                                For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                                    nTaskItem.ListOfID.Add(Convert.ToInt64(sRow.Cells("idEpisode").Value))
                                Next
                            Case Enums.ContentType.TVSeason
                                For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                                    nTaskItem.ListOfID.Add(Convert.ToInt64(sRow.Cells("idSeason").Value))
                                Next
                            Case Enums.ContentType.TVShow
                                For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                                    nTaskItem.ListOfID.Add(Convert.ToInt64(sRow.Cells("idShow").Value))
                                Next
                        End Select

                        Master.TaskManager.AddTask(nTaskItem)
                    End If
                End Using
            End If
        End If
    End Sub

    Private Sub ErrorOccurred()
        mnuMainError.Visible = True
        If dlgErrorViewer.Visible Then dlgErrorViewer.UpdateLog()
    End Sub

    Private Sub mnuMainError_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainError.Click
        dlgErrorViewer.Show(Me)
    End Sub

    Private Sub mnuMainFileExit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainFileExit.Click, cmnuTrayExit.Click
        If Master.isCL Then
            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(370, "Canceling Load..."))
            If bwMovieScraper.IsBusy Then bwMovieScraper.CancelAsync()
            If bwReload_Movie.IsBusy Then bwReload_Movie.CancelAsync()
            While bwMovieScraper.IsBusy OrElse bwReload_Movie.IsBusy OrElse bwMovieScraper.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        Else
            Close()
            Application.Exit()
        End If
    End Sub

    Private Sub FillScreenInfoWith_Images()
        If MainPoster.Image IsNot Nothing OrElse MainPoster.LoadFromMemoryStream Then
            lblPosterSize.Text = String.Format("{0} x {1}", MainPoster.Image.Width, MainPoster.Image.Height)
            pbPosterCache.Image = MainPoster.Image
            ImageUtils.ResizePB(pbPoster, pbPosterCache, PosterMaxHeight, PosterMaxWidth)
            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageGlassOverlay Then ImageUtils.SetGlassOverlay(pbPoster)

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageDimensions Then
                lblPosterSize.Visible = True
            Else
                lblPosterSize.Visible = False
            End If

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageNames Then
                lblPosterTitle.Visible = True
            Else
                lblPosterTitle.Visible = False
            End If
        Else
            If pbPoster.Image IsNot Nothing Then
                pbPoster.Image.Dispose()
                pbPoster.Image = Nothing
            End If
        End If

        If MainKeyArt.Image IsNot Nothing OrElse MainKeyArt.LoadFromMemoryStream Then
            lblKeyArtSize.Text = String.Format("{0} x {1}", MainKeyArt.Image.Width, MainKeyArt.Image.Height)
            pbKeyArtCache.Image = MainKeyArt.Image
            ImageUtils.ResizePB(pbKeyArt, pbKeyArtCache, KeyArtMaxHeight, KeyArtMaxWidth)
            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageGlassOverlay Then ImageUtils.SetGlassOverlay(pbKeyArt)

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageDimensions Then
                lblKeyArtSize.Visible = True
            Else
                lblKeyArtSize.Visible = False
            End If

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageNames Then
                lblKeyArtTitle.Visible = True
            Else
                lblKeyArtTitle.Visible = False
            End If
        Else
            If pbKeyArt.Image IsNot Nothing Then
                pbKeyArt.Image.Dispose()
                pbKeyArt.Image = Nothing
            End If
        End If

        If MainFanartSmall.Image IsNot Nothing OrElse MainFanartSmall.LoadFromMemoryStream Then
            lblFanartSmallSize.Text = String.Format("{0} x {1}", MainFanartSmall.Image.Width, MainFanartSmall.Image.Height)
            pbFanartSmallCache.Image = MainFanartSmall.Image
            ImageUtils.ResizePB(pbFanartSmall, pbFanartSmallCache, FanartSmallMaxHeight, FanartSmallMaxWidth)
            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageGlassOverlay Then ImageUtils.SetGlassOverlay(pbFanartSmall)

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageDimensions Then
                lblFanartSmallSize.Visible = True
            Else
                lblFanartSmallSize.Visible = False
            End If

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageNames Then
                lblFanartSmallTitle.Visible = True
            Else
                lblFanartSmallTitle.Visible = False
            End If
        Else
            If pbFanartSmall.Image IsNot Nothing Then
                pbFanartSmall.Image.Dispose()
                pbFanartSmall.Image = Nothing
            End If
        End If

        If MainLandscape.Image IsNot Nothing OrElse MainLandscape.LoadFromMemoryStream Then
            lblLandscapeSize.Text = String.Format("{0} x {1}", MainLandscape.Image.Width, MainLandscape.Image.Height)
            pbLandscapeCache.Image = MainLandscape.Image
            ImageUtils.ResizePB(pbLandscape, pbLandscapeCache, LandscapeMaxHeight, LandscapeMaxWidth)
            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageGlassOverlay Then ImageUtils.SetGlassOverlay(pbLandscape)

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageDimensions Then
                lblLandscapeSize.Visible = True
            Else
                lblLandscapeSize.Visible = False
            End If

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageNames Then
                lblLandscapeTitle.Visible = True
            Else
                lblLandscapeTitle.Visible = False
            End If
        Else
            If pbLandscape.Image IsNot Nothing Then
                pbLandscape.Image.Dispose()
                pbLandscape.Image = Nothing
            End If
        End If

        If MainClearArt.Image IsNot Nothing OrElse MainClearArt.LoadFromMemoryStream Then
            lblClearArtSize.Text = String.Format("{0} x {1}", MainClearArt.Image.Width, MainClearArt.Image.Height)
            pbClearArtCache.Image = MainClearArt.Image
            ImageUtils.ResizePB(pbClearArt, pbClearArtCache, ClearArtMaxHeight, ClearArtMaxWidth)
            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageGlassOverlay Then ImageUtils.SetGlassOverlay(pbClearArt)

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageDimensions Then
                lblClearArtSize.Visible = True
            Else
                lblClearArtSize.Visible = False
            End If

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageNames Then
                lblClearArtTitle.Visible = True
            Else
                lblClearArtTitle.Visible = False
            End If
        Else
            If pbClearArt.Image IsNot Nothing Then
                pbClearArt.Image.Dispose()
                pbClearArt.Image = Nothing
            End If
        End If

        If MainCharacterArt.Image IsNot Nothing OrElse MainCharacterArt.LoadFromMemoryStream Then
            lblCharacterArtSize.Text = String.Format("{0} x {1}", MainCharacterArt.Image.Width, MainCharacterArt.Image.Height)
            pbCharacterArtCache.Image = MainCharacterArt.Image
            ImageUtils.ResizePB(pbCharacterArt, pbCharacterArtCache, CharacterArtMaxHeight, CharacterArtMaxWidth)
            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageGlassOverlay Then ImageUtils.SetGlassOverlay(pbCharacterArt)

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageDimensions Then
                lblCharacterArtSize.Visible = True
            Else
                lblCharacterArtSize.Visible = False
            End If

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageNames Then
                lblCharacterArtTitle.Visible = True
            Else
                lblCharacterArtTitle.Visible = False
            End If
        Else
            If pbCharacterArt.Image IsNot Nothing Then
                pbCharacterArt.Image.Dispose()
                pbCharacterArt.Image = Nothing
            End If
        End If

        If MainDiscArt.Image IsNot Nothing OrElse MainDiscArt.LoadFromMemoryStream Then
            lblDiscArtSize.Text = String.Format("{0} x {1}", MainDiscArt.Image.Width, MainDiscArt.Image.Height)
            pbDiscArtCache.Image = MainDiscArt.Image
            ImageUtils.ResizePB(pbDiscArt, pbDiscArtCache, DiscArtMaxHeight, DiscArtMaxWidth)
            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageGlassOverlay Then ImageUtils.SetGlassOverlay(pbDiscArt)

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageDimensions Then
                lblDiscArtSize.Visible = True
            Else
                lblDiscArtSize.Visible = False
            End If

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageNames Then
                lblDiscArtTitle.Visible = True
            Else
                lblDiscArtTitle.Visible = False
            End If
        Else
            If pbDiscArt.Image IsNot Nothing Then
                pbDiscArt.Image.Dispose()
                pbDiscArt.Image = Nothing
            End If
        End If

        If MainBanner.Image IsNot Nothing OrElse MainBanner.LoadFromMemoryStream Then
            lblBannerSize.Text = String.Format("{0} x {1}", MainBanner.Image.Width, MainBanner.Image.Height)
            pbBannerCache.Image = MainBanner.Image
            ImageUtils.ResizePB(pbBanner, pbBannerCache, BannerMaxHeight, BannerMaxWidth)
            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageGlassOverlay Then ImageUtils.SetGlassOverlay(pbBanner)

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageDimensions Then
                lblBannerSize.Visible = True
            Else
                lblBannerSize.Visible = False
            End If

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageNames Then
                lblBannerTitle.Visible = True
            Else
                lblBannerTitle.Visible = False
            End If
        Else
            If pbBanner.Image IsNot Nothing Then
                pbBanner.Image.Dispose()
                pbBanner.Image = Nothing
            End If
        End If

        If MainClearLogo.Image IsNot Nothing OrElse MainClearLogo.LoadFromMemoryStream Then
            lblClearLogoSize.Text = String.Format("{0} x {1}", MainClearLogo.Image.Width, MainClearLogo.Image.Height)
            pbClearLogoCache.Image = MainClearLogo.Image
            ImageUtils.ResizePB(pbClearLogo, pbClearLogoCache, ClearLogoMaxHeight, ClearLogoMaxWidth)
            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageGlassOverlay Then ImageUtils.SetGlassOverlay(pbClearLogo)

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageDimensions Then
                lblClearLogoSize.Visible = True
            Else
                lblClearLogoSize.Visible = False
            End If

            If Manager.mSettings.MainOptions.GuiSettings.DisplayImgageNames Then
                lblClearLogoTitle.Visible = True
            Else
                lblClearLogoTitle.Visible = False
            End If
        Else
            If pbClearLogo.Image IsNot Nothing Then
                pbClearLogo.Image.Dispose()
                pbClearLogo.Image = Nothing
            End If
        End If

        If MainFanart.Image IsNot Nothing OrElse MainFanart.LoadFromMemoryStream Then
            pbFanartCache.Image = MainFanart.Image
            ImageUtils.ResizePB(pbFanart, pbFanartCache, scMain.Panel2.Height - (pnlTop.Top + pnlTop.Height), scMain.Panel2.Width, True)
            pbFanart.Left = Convert.ToInt32((scMain.Panel2.Width - pbFanart.Width) / 2)
            pbFanart.Top = pnlTop.Top + pnlTop.Height
        Else
            If pbFanartCache.Image IsNot Nothing Then
                pbFanartCache.Image.Dispose()
                pbFanartCache.Image = Nothing
            End If
            If pbFanart.Image IsNot Nothing Then
                pbFanart.Image.Dispose()
                pbFanart.Image = Nothing
            End If
        End If
        If pbBanner.Image IsNot Nothing Then pnlBanner.Visible = True
        If pbCharacterArt.Image IsNot Nothing Then pnlCharacterArt.Visible = True
        If pbClearArt.Image IsNot Nothing Then pnlClearArt.Visible = True
        If pbClearLogo.Image IsNot Nothing Then pnlClearLogo.Visible = True
        If pbDiscArt.Image IsNot Nothing Then pnlDiscArt.Visible = True
        If pbFanartSmall.Image IsNot Nothing Then pnlFanartSmall.Visible = True
        If pbKeyArt.Image IsNot Nothing Then pnlKeyArt.Visible = True
        If pbLandscape.Image IsNot Nothing Then pnlLandscape.Visible = True
        If pbPoster.Image IsNot Nothing Then pnlPoster.Visible = True
    End Sub

    Private Sub FillScreenInfoWith_Movie()
        SuspendLayout()
        If _CurrDBElement.MainDetails.TitleSpecified AndAlso _CurrDBElement.MainDetails.PremieredSpecified Then
            lblTitle.Text = String.Format("{0} ({1})", _CurrDBElement.MainDetails.Title, StringUtils.GetYearFromString(_CurrDBElement.MainDetails.Premiered))
        ElseIf _CurrDBElement.MainDetails.TitleSpecified AndAlso Not _CurrDBElement.MainDetails.PremieredSpecified Then
            lblTitle.Text = _CurrDBElement.MainDetails.Title
        ElseIf Not _CurrDBElement.MainDetails.TitleSpecified AndAlso _CurrDBElement.MainDetails.PremieredSpecified Then
            lblTitle.Text = String.Format(Master.eLang.GetString(117, "Unknown Movie ({0})"), StringUtils.GetYearFromString(_CurrDBElement.MainDetails.Premiered))
        End If

        If _CurrDBElement.MainDetails.OriginalTitleSpecified AndAlso Not _CurrDBElement.MainDetails.OriginalTitle = _CurrDBElement.MainDetails.Title Then
            lblOriginalTitle.Text = String.Format("{0}: {1}", Master.eLang.GetString(302, "Original Title"), _CurrDBElement.MainDetails.OriginalTitle)
        Else
            lblOriginalTitle.Text = String.Empty
        End If

        Try
            If _CurrDBElement.MainDetails.RatingSpecified Then
                If _CurrDBElement.MainDetails.VotesSpecified Then
                    Dim strRating As String = Double.Parse(_CurrDBElement.MainDetails.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                    Dim strVotes As String = Double.Parse(_CurrDBElement.MainDetails.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                    lblRating.Text = String.Concat(strRating, "/10 (", String.Format(Master.eLang.GetString(118, "{0} Votes"), strVotes), ")")
                Else
                    Dim strRating As String = Double.Parse(_CurrDBElement.MainDetails.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                    lblRating.Text = String.Concat(strRating, "/10")
                End If
            End If
        Catch ex As Exception
            _Logger.Error(String.Concat("Error: Not valid Rating or Votes (", _CurrDBElement.MainDetails.Rating, " / ", _CurrDBElement.MainDetails.Votes, ")"))
            lblRating.Text = "Error: Please rescrape Rating"
        End Try

        If _CurrDBElement.MainDetails.RuntimeSpecified Then
            lblRuntime.Text = String.Format(Master.eLang.GetString(112, "Runtime: {0}"), If(_CurrDBElement.MainDetails.Runtime.Contains("|"), Microsoft.VisualBasic.Strings.Left(_CurrDBElement.MainDetails.Runtime, _CurrDBElement.MainDetails.Runtime.IndexOf("|")), _CurrDBElement.MainDetails.Runtime)).Trim
        End If

        If _CurrDBElement.MainDetails.Top250Specified Then 'TODO: fix
            'pnlTop250.Visible = True
            'lblTop250.Text = currMovie.Movie.Top250.ToString
        Else
            'pnlTop250.Visible = False
        End If

        txtOutline.Text = _CurrDBElement.MainDetails.Outline
        txtPlot.Text = _CurrDBElement.MainDetails.Plot
        lblTagline.Text = _CurrDBElement.MainDetails.Tagline

        alActors = New List(Of String)

        If _CurrDBElement.MainDetails.ActorsSpecified Then
            pbActors.Image = My.Resources.actor_silhouette
            For Each actor As MediaContainers.Person In _CurrDBElement.MainDetails.Actors
                If Not String.IsNullOrEmpty(actor.LocalFilePath) AndAlso File.Exists(actor.LocalFilePath) Then
                    If Not actor.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not actor.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                        alActors.Add(actor.LocalFilePath)
                    Else
                        alActors.Add("none")
                    End If
                ElseIf Not String.IsNullOrEmpty(actor.URLOriginal) Then
                    If Not actor.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not actor.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                        alActors.Add(actor.URLOriginal)
                    Else
                        alActors.Add("none")
                    End If
                Else
                    alActors.Add("none")
                End If

                If String.IsNullOrEmpty(actor.Role.Trim) Then
                    lstActors.Items.Add(actor.Name.Trim)
                Else
                    lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), actor.Name.Trim, actor.Role.Trim))
                End If
            Next
            lstActors.SelectedIndex = 0
        End If

        If _CurrDBElement.MainDetails.MPAASpecified Then
            Dim tmpRatingImg As Image = MediaFlags.GetRatingImage(_CurrDBElement.MainDetails.MPAA)
            If tmpRatingImg IsNot Nothing Then
                pbMPAA.Image = tmpRatingImg
                MoveMPAA()
            End If
        End If

        If _CurrDBElement.MainDetails.GenresSpecified AndAlso Manager.mSettings.MainOptions.GuiSettings.DisplayGenreFlags Then
            CreateGenreThumbs(_CurrDBElement.MainDetails.Genres)
        End If

        If _CurrDBElement.MainDetails.StudiosSpecified AndAlso Manager.mSettings.MainOptions.GuiSettings.DisplayStudioFlag Then
            pbStudio.Image = MediaFlags.GetStudioImage(_CurrDBElement.MainDetails.Studios.Item(0).ToLower) 'ByDef all image file names are in lower case
            pbStudio.Tag = _CurrDBElement.MainDetails.Studios.Item(0)
        ElseIf Manager.mSettings.MainOptions.GuiSettings.DisplayStudioFlag Then
            pbStudio.Image = MediaFlags.GetStudioImage("####")
            pbStudio.Tag = String.Empty
        End If

        If Manager.mSettings.MainOptions.GuiSettings.DisplayStudioName Then
            lblStudio.Text = pbStudio.Tag.ToString
        End If

        If Master.eSettings.Movie.DataSettings.MetadataScan.Enabled Then 'TODO: show images even metadata is disabled
            SetAVImages(MediaFlags.GetAVImages(
                        _CurrDBElement.MainDetails.FileInfo,
                        Manager.mSettings.Movie.GuiSettings.PreferredAudioLanguage,
                        _CurrDBElement.ContentType,
                        _CurrDBElement.MainDetails.VideoSource
                        ))
            pnlInfoIcons.Width = pbVideoChannels.Width + pbVideoSource.Width + pbVideoCodec.Width + pbVideoResolution.Width + pbAudioCodec.Width + pbAudioChannels.Width + pbStudio.Width + 6
            pbStudio.Left = pbVideoChannels.Width + pbVideoSource.Width + pbVideoCodec.Width + pbVideoResolution.Width + pbAudioCodec.Width + pbAudioChannels.Width + 5
        Else
            pnlInfoIcons.Width = pbStudio.Width + 1
            pbStudio.Left = 0
        End If

        lblCertifications.Text = String.Join(" / ", _CurrDBElement.MainDetails.Certifications.ToArray)
        lblCollections.Text = String.Join(" / ", From sets In _CurrDBElement.MainDetails.Sets Select sets.Title)
        lblCountries.Text = String.Join(" / ", _CurrDBElement.MainDetails.Countries.ToArray)
        lblCredits.Text = String.Join(" / ", _CurrDBElement.MainDetails.Credits.ToArray)
        lblDirectors.Text = String.Join(" / ", _CurrDBElement.MainDetails.Directors.ToArray)
        lblDirectorsHeader.Text = Master.eLang.GetString(940, "Directors")
        lblPremiered.Text = _CurrDBElement.MainDetails.Premiered
        lblPremieredHeader.Text = Master.eLang.GetString(724, "Premiered")
        lblTags.Text = String.Join(" / ", _CurrDBElement.MainDetails.Tags.ToArray)

        lblIMDBHeader.Tag = StringUtils.GetURL_IMDb(_CurrDBElement)
        txtIMDBID.Text = _CurrDBElement.MainDetails.UniqueIDs.IMDbId
        lblTMDBHeader.Tag = StringUtils.GetURL_TMDb(_CurrDBElement)
        txtTMDBID.Text = If(_CurrDBElement.MainDetails.UniqueIDs.TMDbIdSpecified, _CurrDBElement.MainDetails.UniqueIDs.TMDbId.ToString, String.Empty)

        txtFilePath.Text = _CurrDBElement.FileItem.FullPath
        txtTrailerPath.Text = If(Not String.IsNullOrEmpty(_CurrDBElement.Trailer.LocalFilePath), _CurrDBElement.Trailer.LocalFilePath, _CurrDBElement.MainDetails.Trailer)

        txtMetaData.Text = Information.FIToString(_CurrDBElement)

        InfoCleared = False

        If bDoingSearch_Movies Then
            txtSearchMovies.Focus()
            bDoingSearch_Movies = False
        Else
            dgvMovies.Focus()
        End If

        If pbMPAA.Image IsNot Nothing Then pnlMPAA.Visible = True
        For i As Integer = 0 To pnlGenre.Count - 1
            pnlGenre(i).Visible = True
        Next

        ResumeLayout()
    End Sub

    Private Sub FillScreenInfoWith_Movieset()
        SuspendLayout()
        If _CurrDBElement.MainDetails.TitleSpecified AndAlso _CurrDBElement.MoviesInSet IsNot Nothing AndAlso _CurrDBElement.MoviesInSet.Count > 0 Then
            lblTitle.Text = String.Format("{0} ({1})", _CurrDBElement.MainDetails.Title, _CurrDBElement.MoviesInSet.Count)
        ElseIf _CurrDBElement.MainDetails.TitleSpecified Then
            lblTitle.Text = _CurrDBElement.MainDetails.Title
        Else
            lblTitle.Text = String.Empty
        End If

        txtPlot.Text = _CurrDBElement.MainDetails.Plot


        lblTMDBHeader.Tag = StringUtils.GetURL_TMDb(_CurrDBElement)
        txtTMDBID.Text = If(_CurrDBElement.MainDetails.UniqueIDs.TMDbIdSpecified, _CurrDBElement.MainDetails.UniqueIDs.TMDbId.ToString, String.Empty)

        If _CurrDBElement.MoviesInSet IsNot Nothing AndAlso _CurrDBElement.MoviesInSet.Count > 0 Then
            If bwLoadImages_MovieSetMoviePosters.IsBusy AndAlso Not bwLoadImages_MovieSetMoviePosters.CancellationPending Then
                bwLoadImages_MovieSetMoviePosters.CancelAsync()
            End If

            While bwLoadImages_MovieSetMoviePosters.IsBusy
                Application.DoEvents()
            End While

            bwLoadImages_MovieSetMoviePosters.WorkerSupportsCancellation = True
            bwLoadImages_MovieSetMoviePosters.RunWorkerAsync()
        End If

        InfoCleared = False

        If bDoingSearch_MovieSets Then
            txtSearchMovieSets.Focus()
            bDoingSearch_MovieSets = False
        Else
            dgvMoviesets.Focus()
        End If

        If pbMPAA.Image IsNot Nothing Then pnlMPAA.Visible = True
        For i As Integer = 0 To pnlGenre.Count - 1
            pnlGenre(i).Visible = True
        Next

        ResumeLayout()
    End Sub

    Private Sub FillScreenInfoWith_TVEpisode()
        SuspendLayout()
        lblTitle.Text = If(Not _CurrDBElement.FileItemSpecified, String.Concat(_CurrDBElement.MainDetails.Title, " ", Master.eLang.GetString(689, "[MISSING]")), _CurrDBElement.MainDetails.Title)
        txtPlot.Text = _CurrDBElement.MainDetails.Plot
        lblCredits.Text = String.Join(" / ", _CurrDBElement.MainDetails.Credits.ToArray)
        lblDirectors.Text = String.Join(" / ", _CurrDBElement.MainDetails.Directors.ToArray)
        lblDirectorsHeader.Text = Master.eLang.GetString(940, "Directors")
        txtFilePath.Text = If(_CurrDBElement.FileItemSpecified, _CurrDBElement.FileItem.FullPath, String.Empty)
        lblRuntime.Text = String.Format(Master.eLang.GetString(647, "Aired: {0}"), If(_CurrDBElement.MainDetails.AiredSpecified, Date.Parse(_CurrDBElement.MainDetails.Aired).ToShortDateString, "?"))
        lblPremiered.Text = _CurrDBElement.MainDetails.Aired
        lblPremieredHeader.Text = Master.eLang.GetString(728, "Aired")

        If _CurrDBElement.MainDetails.RuntimeSpecified Then
            lblRuntime.Text = String.Format(Master.eLang.GetString(112, "Runtime: {0}"), If(_CurrDBElement.MainDetails.Runtime.Contains("|"), Microsoft.VisualBasic.Strings.Left(_CurrDBElement.MainDetails.Runtime, _CurrDBElement.MainDetails.Runtime.IndexOf("|")), _CurrDBElement.MainDetails.Runtime)).Trim
        End If

        Try
            If _CurrDBElement.MainDetails.RatingSpecified Then
                If _CurrDBElement.MainDetails.VotesSpecified Then
                    Dim strRating As String = Double.Parse(_CurrDBElement.MainDetails.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                    Dim strVotes As String = Double.Parse(_CurrDBElement.MainDetails.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                    lblRating.Text = String.Concat(strRating, "/10 (", String.Format(Master.eLang.GetString(118, "{0} Votes"), strVotes), ")")
                Else
                    Dim strRating As String = Double.Parse(_CurrDBElement.MainDetails.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                    lblRating.Text = String.Concat(strRating, "/10")
                End If
            End If
        Catch ex As Exception
            _Logger.Error(String.Concat("Error: Not valid Rating or Votes (", _CurrDBElement.MainDetails.Rating, " / ", _CurrDBElement.MainDetails.Votes, ")"))
            lblRating.Text = "Error: Please rescrape Rating"
        End Try

        lblTagline.Text = String.Format(Master.eLang.GetString(648, "Season: {0}, Episode: {1}"),
                            If(Not _CurrDBElement.MainDetails.SeasonSpecified, "?", _CurrDBElement.MainDetails.Season.ToString),
                            If(Not _CurrDBElement.MainDetails.EpisodeSpecified, "?", _CurrDBElement.MainDetails.Episode.ToString))


        alActors = New List(Of String)
        If _CurrDBElement.MainDetails.ActorsSpecified Then
            pbActors.Image = My.Resources.actor_silhouette
            For Each actor As MediaContainers.Person In _CurrDBElement.MainDetails.Actors
                If Not String.IsNullOrEmpty(actor.LocalFilePath) AndAlso File.Exists(actor.LocalFilePath) Then
                    If Not actor.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not actor.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                        alActors.Add(actor.LocalFilePath)
                    Else
                        alActors.Add("none")
                    End If
                ElseIf Not String.IsNullOrEmpty(actor.URLOriginal) Then
                    If Not actor.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not actor.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                        alActors.Add(actor.URLOriginal)
                    Else
                        alActors.Add("none")
                    End If
                Else
                    alActors.Add("none")
                End If

                If String.IsNullOrEmpty(actor.Role.Trim) Then
                    lstActors.Items.Add(actor.Name.Trim)
                Else
                    lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), actor.Name.Trim, actor.Role.Trim))
                End If
            Next
            lstActors.SelectedIndex = 0
        End If

        alGuestStars = New List(Of String)
        If _CurrDBElement.MainDetails.GuestStarsSpecified Then
            pbGuestStars.Image = My.Resources.actor_silhouette
            For Each actor As MediaContainers.Person In _CurrDBElement.MainDetails.GuestStars
                If Not String.IsNullOrEmpty(actor.LocalFilePath) AndAlso File.Exists(actor.LocalFilePath) Then
                    If Not actor.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not actor.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                        alGuestStars.Add(actor.LocalFilePath)
                    Else
                        alGuestStars.Add("none")
                    End If
                ElseIf Not String.IsNullOrEmpty(actor.URLOriginal) Then
                    If Not actor.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not actor.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                        alGuestStars.Add(actor.URLOriginal)
                    Else
                        alGuestStars.Add("none")
                    End If
                Else
                    alGuestStars.Add("none")
                End If

                If String.IsNullOrEmpty(actor.Role.Trim) Then
                    lstGuestStars.Items.Add(actor.Name.Trim)
                Else
                    lstGuestStars.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), actor.Name.Trim, actor.Role.Trim))
                End If
            Next
            lstGuestStars.SelectedIndex = 0
        End If

        If _CurrDBElement.TVShowDetails.MPAASpecified Then
            Dim tmpRatingImg As Image = MediaFlags.GetTVRatingImage(_CurrDBElement.TVShowDetails.MPAA)
            If tmpRatingImg IsNot Nothing Then
                pbMPAA.Image = tmpRatingImg
                MoveMPAA()
            End If
        End If


        If _CurrDBElement.TVShowDetails.GenresSpecified AndAlso Manager.mSettings.MainOptions.GuiSettings.DisplayGenreFlags Then
            CreateGenreThumbs(_CurrDBElement.TVShowDetails.Genres)
        End If

        lblIMDBHeader.Tag = StringUtils.GetURL_IMDb(_CurrDBElement)
        txtIMDBID.Text = _CurrDBElement.MainDetails.UniqueIDs.IMDbId
        lblTMDBHeader.Tag = StringUtils.GetURL_TMDb(_CurrDBElement)
        txtTMDBID.Text = If(_CurrDBElement.MainDetails.UniqueIDs.TMDbIdSpecified, _CurrDBElement.MainDetails.UniqueIDs.TMDbId.ToString, String.Empty)
        lblTVDBHeader.Tag = StringUtils.GetURL_TVDb(_CurrDBElement)
        txtTMDBID.Text = If(_CurrDBElement.MainDetails.UniqueIDs.TVDbIdSpecified, _CurrDBElement.MainDetails.UniqueIDs.TVDbId.ToString, String.Empty)

        If _CurrDBElement.TVShowDetails.StudiosSpecified AndAlso Manager.mSettings.MainOptions.GuiSettings.DisplayStudioFlag Then
            pbStudio.Image = MediaFlags.GetStudioImage(_CurrDBElement.TVShowDetails.Studios.Item(0).ToLower) 'ByDef all image file names are in lower case
            pbStudio.Tag = _CurrDBElement.TVShowDetails.Studios.Item(0)
        ElseIf Manager.mSettings.MainOptions.GuiSettings.DisplayStudioFlag Then
            pbStudio.Image = MediaFlags.GetStudioImage("####")
            pbStudio.Tag = String.Empty
        End If

        If Manager.mSettings.MainOptions.GuiSettings.DisplayStudioName Then
            lblStudio.Text = pbStudio.Tag.ToString
        End If

        If Master.eSettings.TVScraperMetaDataScan AndAlso _CurrDBElement.FileItemSpecified Then
            SetAVImages(MediaFlags.GetAVImages(
                        _CurrDBElement.MainDetails.FileInfo,
                        Manager.mSettings.TVEpisode.GuiSettings.PreferredAudioLanguage,
                        _CurrDBElement.ContentType,
                        _CurrDBElement.MainDetails.VideoSource
                        ))
            pnlInfoIcons.Width = pbVideoChannels.Width + pbVideoSource.Width + pbVideoCodec.Width + pbVideoResolution.Width + pbAudioCodec.Width + pbAudioChannels.Width + pbStudio.Width + 6
            pbStudio.Left = pbVideoChannels.Width + pbVideoSource.Width + pbVideoCodec.Width + pbVideoResolution.Width + pbAudioCodec.Width + pbAudioChannels.Width + 5
        Else
            pnlInfoIcons.Width = pbStudio.Width + 1
            pbStudio.Left = 0
        End If

        txtMetaData.Text = Information.FIToString(_CurrDBElement)

        InfoCleared = False

        If pbMPAA.Image IsNot Nothing Then pnlMPAA.Visible = True
        For i As Integer = 0 To pnlGenre.Count - 1
            pnlGenre(i).Visible = True
        Next

        ResumeLayout()
    End Sub

    Private Sub FillScreenInfoWith_TVSeason()
        SuspendLayout()

        lblTitle.Text = _CurrDBElement.MainDetails.Title
        txtPlot.Text = _CurrDBElement.MainDetails.Plot
        lblRuntime.Text = _CurrDBElement.TVShowDetails.Runtime
        lblIMDBHeader.Tag = StringUtils.GetURL_IMDb(_CurrDBElement)
        txtIMDBID.Text = If(Not String.IsNullOrEmpty(lblIMDBHeader.Tag.ToString), "Link", String.Empty)
        lblTMDBHeader.Tag = StringUtils.GetURL_TMDb(_CurrDBElement)
        txtTMDBID.Text = If(Not String.IsNullOrEmpty(lblTMDBHeader.Tag.ToString), "Link", String.Empty)
        lblTVDBHeader.Tag = StringUtils.GetURL_TVDb(_CurrDBElement)
        txtTMDBID.Text = If(_CurrDBElement.MainDetails.UniqueIDs.TVDbIdSpecified, _CurrDBElement.MainDetails.UniqueIDs.TVDbId.ToString, String.Empty)
        lblCertifications.Text = String.Join(" / ", _CurrDBElement.TVShowDetails.Certifications.ToArray)
        lblPremiered.Text = _CurrDBElement.MainDetails.Aired
        lblPremieredHeader.Text = Master.eLang.GetString(728, "Aired")

        Try
            If _CurrDBElement.TVShowDetails.RatingSpecified Then
                If _CurrDBElement.TVShowDetails.VotesSpecified Then
                    Dim strRating As String = Double.Parse(_CurrDBElement.TVShowDetails.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                    Dim strVotes As String = Double.Parse(_CurrDBElement.TVShowDetails.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                    lblRating.Text = String.Concat(strRating, "/10 (", String.Format(Master.eLang.GetString(118, "{0} Votes"), strVotes), ")")
                Else
                    Dim strRating As String = Double.Parse(_CurrDBElement.TVShowDetails.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                    lblRating.Text = String.Concat(strRating, "/10")
                End If
            End If
        Catch ex As Exception
            _Logger.Error(String.Concat("Error: Not valid Rating or Votes (", _CurrDBElement.TVShowDetails.Rating, " / ", _CurrDBElement.TVShowDetails.Votes, ")"))
            lblRating.Text = "Error: Please rescrape Rating"
        End Try

        alActors = New List(Of String)

        If _CurrDBElement.TVShowDetails.ActorsSpecified Then
            pbActors.Image = My.Resources.actor_silhouette
            For Each actor As MediaContainers.Person In _CurrDBElement.TVShowDetails.Actors
                If Not String.IsNullOrEmpty(actor.LocalFilePath) AndAlso File.Exists(actor.LocalFilePath) Then
                    If Not actor.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not actor.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                        alActors.Add(actor.LocalFilePath)
                    Else
                        alActors.Add("none")
                    End If
                ElseIf Not String.IsNullOrEmpty(actor.URLOriginal) Then
                    If Not actor.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not actor.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                        alActors.Add(actor.URLOriginal)
                    Else
                        alActors.Add("none")
                    End If
                Else
                    alActors.Add("none")
                End If

                If String.IsNullOrEmpty(actor.Role.Trim) Then
                    lstActors.Items.Add(actor.Name.Trim)
                Else
                    lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), actor.Name.Trim, actor.Role.Trim))
                End If
            Next
            lstActors.SelectedIndex = 0
        End If

        If _CurrDBElement.TVShowDetails.MPAASpecified Then
            Dim tmpRatingImg As Image = MediaFlags.GetTVRatingImage(_CurrDBElement.TVShowDetails.MPAA)
            If tmpRatingImg IsNot Nothing Then
                pbMPAA.Image = tmpRatingImg
                MoveMPAA()
            End If
        End If


        If _CurrDBElement.TVShowDetails.GenresSpecified AndAlso Manager.mSettings.MainOptions.GuiSettings.DisplayGenreFlags Then
            CreateGenreThumbs(_CurrDBElement.TVShowDetails.Genres)
        End If

        If _CurrDBElement.TVShowDetails.StudiosSpecified Then
            pbStudio.Image = MediaFlags.GetStudioImage(_CurrDBElement.TVShowDetails.Studios.Item(0).ToLower) 'ByDef all image file names are in lower case
            pbStudio.Tag = _CurrDBElement.TVShowDetails.Studios.Item(0)
        Else
            pbStudio.Image = MediaFlags.GetStudioImage("####")
            pbStudio.Tag = String.Empty
        End If

        pnlInfoIcons.Width = pbStudio.Width + 1
        pbStudio.Left = 0

        InfoCleared = False

        If pbMPAA.Image IsNot Nothing Then pnlMPAA.Visible = True
        For i As Integer = 0 To pnlGenre.Count - 1
            pnlGenre(i).Visible = True
        Next

        ResumeLayout()
    End Sub

    Private Sub FillScreenInfoWith_TVShow()
        SuspendLayout()

        lblTitle.Text = _CurrDBElement.MainDetails.Title

        If _CurrDBElement.MainDetails.OriginalTitleSpecified AndAlso Not _CurrDBElement.MainDetails.OriginalTitle = _CurrDBElement.MainDetails.Title Then
            lblOriginalTitle.Text = String.Format("{0}: {1}", Master.eLang.GetString(302, "Original Title"), _CurrDBElement.MainDetails.OriginalTitle)
        Else
            lblOriginalTitle.Text = String.Empty
        End If

        txtPlot.Text = _CurrDBElement.MainDetails.Plot
        lblRuntime.Text = _CurrDBElement.MainDetails.Runtime
        lblCountries.Text = String.Join(" / ", _CurrDBElement.MainDetails.Countries.ToArray)
        lblDirectors.Text = String.Join(" / ", _CurrDBElement.MainDetails.Creators.ToArray)
        lblDirectorsHeader.Text = Master.eLang.GetString(744, "Creators")
        lblPremiered.Text = _CurrDBElement.MainDetails.Premiered
        lblPremieredHeader.Text = Master.eLang.GetString(724, "Premiered")
        lblIMDBHeader.Tag = StringUtils.GetURL_IMDb(_CurrDBElement)
        txtIMDBID.Text = _CurrDBElement.MainDetails.UniqueIDs.IMDbId
        lblTMDBHeader.Tag = StringUtils.GetURL_TMDb(_CurrDBElement)
        txtTMDBID.Text = _CurrDBElement.MainDetails.UniqueIDs.TMDbId.ToString
        lblTVDBHeader.Tag = StringUtils.GetURL_TVDb(_CurrDBElement)
        txtTVDBID.Text = If(_CurrDBElement.MainDetails.UniqueIDs.TVDbIdSpecified, _CurrDBElement.MainDetails.UniqueIDs.TVDbId.ToString, String.Empty)
        lblCertifications.Text = String.Join(" / ", _CurrDBElement.MainDetails.Certifications.ToArray)
        lblTags.Text = String.Join(" / ", _CurrDBElement.MainDetails.Tags.ToArray)
        lblStatus.Text = _CurrDBElement.MainDetails.Status

        Try
            If _CurrDBElement.MainDetails.RatingSpecified Then
                If _CurrDBElement.MainDetails.VotesSpecified Then
                    Dim strRating As String = Double.Parse(_CurrDBElement.MainDetails.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                    Dim strVotes As String = Double.Parse(_CurrDBElement.MainDetails.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                    lblRating.Text = String.Concat(strRating, "/10 (", String.Format(Master.eLang.GetString(118, "{0} Votes"), strVotes), ")")
                Else
                    Dim strRating As String = Double.Parse(_CurrDBElement.MainDetails.Rating, Globalization.CultureInfo.InvariantCulture).ToString("N1", Globalization.CultureInfo.CurrentCulture)
                    lblRating.Text = String.Concat(strRating, "/10")
                End If
            End If
        Catch ex As Exception
            _Logger.Error(String.Concat("Error: Not valid Rating or Votes (", _CurrDBElement.MainDetails.Rating, " / ", _CurrDBElement.MainDetails.Votes, ")"))
            lblRating.Text = "Error: Please rescrape Rating"
        End Try

        alActors = New List(Of String)

        If _CurrDBElement.MainDetails.ActorsSpecified Then
            pbActors.Image = My.Resources.actor_silhouette
            For Each actor As MediaContainers.Person In _CurrDBElement.MainDetails.Actors
                If Not String.IsNullOrEmpty(actor.LocalFilePath) AndAlso File.Exists(actor.LocalFilePath) Then
                    If Not actor.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not actor.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                        alActors.Add(actor.LocalFilePath)
                    Else
                        alActors.Add("none")
                    End If
                ElseIf Not String.IsNullOrEmpty(actor.URLOriginal) Then
                    If Not actor.URLOriginal.ToLower.IndexOf("addtiny.gif") > 0 AndAlso Not actor.URLOriginal.ToLower.IndexOf("no_photo") > 0 Then
                        alActors.Add(actor.URLOriginal)
                    Else
                        alActors.Add("none")
                    End If
                Else
                    alActors.Add("none")
                End If

                If String.IsNullOrEmpty(actor.Role.Trim) Then
                    lstActors.Items.Add(actor.Name.Trim)
                Else
                    lstActors.Items.Add(String.Format(Master.eLang.GetString(131, "{0} as {1}"), actor.Name.Trim, actor.Role.Trim))
                End If
            Next
            lstActors.SelectedIndex = 0
        End If

        If _CurrDBElement.MainDetails.MPAASpecified Then
            Dim tmpRatingImg As Image = MediaFlags.GetTVRatingImage(_CurrDBElement.MainDetails.MPAA)
            If tmpRatingImg IsNot Nothing Then
                pbMPAA.Image = tmpRatingImg
                MoveMPAA()
            End If
        End If


        If _CurrDBElement.MainDetails.GenresSpecified AndAlso Manager.mSettings.MainOptions.GuiSettings.DisplayGenreFlags Then
            CreateGenreThumbs(_CurrDBElement.MainDetails.Genres)
        End If

        If _CurrDBElement.MainDetails.StudiosSpecified AndAlso Manager.mSettings.MainOptions.GuiSettings.DisplayStudioFlag Then
            pbStudio.Image = MediaFlags.GetStudioImage(_CurrDBElement.MainDetails.Studios.Item(0).ToLower) 'ByDef all image file names are in lower case
            pbStudio.Tag = _CurrDBElement.MainDetails.Studios.Item(0)
        ElseIf Manager.mSettings.MainOptions.GuiSettings.DisplayStudioFlag Then
            pbStudio.Image = MediaFlags.GetStudioImage("####")
            pbStudio.Tag = String.Empty
        End If

        pnlInfoIcons.Width = pbStudio.Width + 1
        pbStudio.Left = 0

        InfoCleared = False

        If bDoingSearch_TVShows Then
            txtSearchShows.Focus()
            bDoingSearch_TVShows = False
        Else
            dgvTVShows.Focus()
        End If

        If pbMPAA.Image IsNot Nothing Then pnlMPAA.Visible = True
        For i As Integer = 0 To pnlGenre.Count - 1
            pnlGenre(i).Visible = True
        Next

        ResumeLayout()
    End Sub

    Public Sub ChangeToolStripLabel(control As ToolStripLabel, bVisible As Boolean, strValue As String)
        If control.Owner.InvokeRequired Then
            control.Owner.BeginInvoke(New Delegate_ChangeToolStripLabel(AddressOf ChangeToolStripLabel), New Object() {control, bVisible, strValue})
        Else
            control.Text = strValue
            control.Visible = bVisible
        End If
    End Sub

    Private Sub ChangeToolStripProgressBar(control As ToolStripProgressBar,
                                           bVisible As Boolean,
                                           iMaximum As Integer,
                                           iMinimum As Integer,
                                           iValue As Integer,
                                           tStyle As ProgressBarStyle)
        If control.Owner.InvokeRequired Then
            control.Owner.BeginInvoke(New Delegate_ChangeToolStripProgressBar(AddressOf ChangeToolStripProgressBar), New Object() {control, bVisible, iMaximum, iMinimum, iValue, tStyle})
        Else
            control.Maximum = iMaximum
            control.Minimum = iMinimum
            control.Style = tStyle
            control.Value = iValue
            control.Visible = bVisible
        End If
    End Sub

    Private Sub mnuGenresAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuGenresAdd.Click
        Dim strGenre As String = String.Empty
        If Not String.IsNullOrEmpty(mnuGenresNew.Text) Then
            strGenre = mnuGenresNew.Text.Trim
        ElseIf Not String.IsNullOrEmpty(mnuGenresGenre.Text.Trim) Then
            strGenre = mnuGenresGenre.Text.Trim
        End If

        If Not String.IsNullOrEmpty(strGenre) Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Select Case _SelectedContentType
                    Case "movie"
                        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value))
                            If Not tmpDBElement.MainDetails.Genres.Contains(strGenre) Then
                                tmpDBElement.MainDetails.Genres.Add(strGenre)
                                Master.DB.Save_Movie(tmpDBElement, True, True, False, True, False)
                                DataGridView_RefreshRow_Movie(tmpDBElement.ID)
                            End If
                        Next
                    Case "tvshow"
                        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), False, False)
                            If Not tmpDBElement.MainDetails.Genres.Contains(strGenre) Then
                                tmpDBElement.MainDetails.Genres.Add(strGenre)
                                Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)
                                DataGridView_RefreshRow_TVShow(tmpDBElement.ID)
                            End If
                        Next
                End Select
                SQLtransaction.Commit()
            End Using
        End If
    End Sub

    Private Sub mnuGenresGenre_DropDown(ByVal sender As Object, ByVal e As EventArgs) Handles mnuGenresGenre.DropDown
        mnuGenresGenre.Items.Clear()
        Dim mGenre() As Object = APIXML.GetGenreList
        mnuGenresGenre.Items.AddRange(mGenre)

        mnuGenresNew.Text = String.Empty
    End Sub

    Private Sub mnuGenresGenre_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles mnuGenresGenre.SelectedIndexChanged
        Dim iSelectedRowsCount As Integer = 0
        Select Case _SelectedContentType
            Case "movie"
                iSelectedRowsCount = dgvMovies.SelectedRows.Count
            Case "tvshow"
                iSelectedRowsCount = dgvTVShows.SelectedRows.Count
        End Select

        If iSelectedRowsCount > 1 Then
            mnuGenresRemove.Enabled = True
            mnuGenresAdd.Enabled = True
        Else
            mnuGenresRemove.Enabled = mnuGenresGenre.Tag.ToString.Contains(mnuGenresGenre.Text)
            mnuGenresAdd.Enabled = Not mnuGenresGenre.Tag.ToString.Contains(mnuGenresGenre.Text)
        End If
        mnuGenresSet.Enabled = True
    End Sub

    Private Sub mnuGenresNew_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles mnuGenresNew.TextChanged
        If Not String.IsNullOrEmpty(mnuGenresNew.Text) Then
            If Not mnuGenresGenre.Items.Contains(String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")) Then
                mnuGenresGenre.Items.Insert(0, String.Concat(Master.eLang.GetString(27, "Select Genre"), "..."))
            End If
            mnuGenresGenre.SelectedItem = String.Concat(Master.eLang.GetString(27, "Select Genre"), "...")
        End If

        If Not String.IsNullOrEmpty(mnuGenresNew.Text) Then
            mnuGenresAdd.Enabled = True
            mnuGenresRemove.Enabled = False
            mnuGenresSet.Enabled = True
        Else
            mnuGenresAdd.Enabled = False
            mnuGenresRemove.Enabled = False
            mnuGenresSet.Enabled = False
        End If
    End Sub

    Private Sub mnuGenresRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuGenresRemove.Click
        Dim strGenre As String = String.Empty
        If Not String.IsNullOrEmpty(mnuGenresNew.Text) Then
            strGenre = mnuGenresNew.Text.Trim
        ElseIf Not String.IsNullOrEmpty(mnuGenresGenre.Text.Trim) Then
            strGenre = mnuGenresGenre.Text.Trim
        End If

        If Not String.IsNullOrEmpty(strGenre) Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Select Case _SelectedContentType
                    Case "movie"
                        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value))
                            If tmpDBElement.MainDetails.Genres.Contains(strGenre) Then
                                tmpDBElement.MainDetails.Genres.Remove(strGenre)
                                Master.DB.Save_Movie(tmpDBElement, True, True, False, True, False)
                                DataGridView_RefreshRow_Movie(tmpDBElement.ID)
                            End If
                        Next
                    Case "tvshow"
                        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), False, False)
                            If tmpDBElement.MainDetails.Genres.Contains(strGenre) Then
                                tmpDBElement.MainDetails.Genres.Remove(strGenre)
                                Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)
                                DataGridView_RefreshRow_TVShow(tmpDBElement.ID)
                            End If
                        Next
                End Select
                SQLtransaction.Commit()
            End Using
        End If
    End Sub

    Private Sub mnuGenresSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuGenresSet.Click
        Dim strGenre As String = String.Empty
        If Not String.IsNullOrEmpty(mnuGenresNew.Text) Then
            strGenre = mnuGenresNew.Text.Trim
        ElseIf Not String.IsNullOrEmpty(mnuGenresGenre.Text.Trim) Then
            strGenre = mnuGenresGenre.Text.Trim
        End If

        If Not String.IsNullOrEmpty(strGenre) Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Select Case _SelectedContentType
                    Case "movie"
                        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value))
                            tmpDBElement.MainDetails.Genres.Clear()
                            tmpDBElement.MainDetails.Genres.Add(strGenre)
                            Master.DB.Save_Movie(tmpDBElement, True, True, False, True, False)
                            DataGridView_RefreshRow_Movie(tmpDBElement.ID)
                        Next
                    Case "tvshow"
                        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), False, False)
                            tmpDBElement.MainDetails.Genres.Clear()
                            tmpDBElement.MainDetails.Genres.Add(strGenre)
                            Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)
                            DataGridView_RefreshRow_TVShow(tmpDBElement.ID)
                        Next
                End Select
                SQLtransaction.Commit()
            End Using
        End If
    End Sub

    Private Sub mnuLanguagesLanguage_DropDown(ByVal sender As Object, ByVal e As EventArgs) Handles mnuLanguagesLanguage.DropDown
        If mnuLanguagesLanguage.Items.Contains(String.Concat(Master.eLang.GetString(1199, "Select Language"), "...")) Then
            mnuLanguagesLanguage.Items.Remove(String.Concat(Master.eLang.GetString(1199, "Select Language"), "..."))
        End If
    End Sub

    Private Sub mnuLanguagesLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles mnuLanguagesLanguage.SelectedIndexChanged
        mnuLanguagesSet.Enabled = True
    End Sub

    Private Sub mnuLanguagesSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuLanguagesSet.Click
        Dim strLanguage As String = String.Empty
        If Not String.IsNullOrEmpty(mnuLanguagesLanguage.Text.Trim) Then
            strLanguage = mnuLanguagesLanguage.Text.Trim
        End If

        If Not String.IsNullOrEmpty(strLanguage) Then
            Select Case _SelectedContentType
                Case "movie"
                    TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetLanguage) With {
                                           .CommonStringValue = strLanguage,
                                           .ContentType = Enums.ContentType.Movie,
                                           .SelectionType = Enums.SelectionType.Selected
                                           })
                Case "movieset"
                    TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetLanguage) With {
                                           .CommonStringValue = strLanguage,
                                           .ContentType = Enums.ContentType.Movieset,
                                           .SelectionType = Enums.SelectionType.Selected
                                           })
                Case "tvshow"
                    TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetLanguage) With {
                                           .CommonStringValue = strLanguage,
                                           .ContentType = Enums.ContentType.TVShow,
                                           .SelectionType = Enums.SelectionType.Selected
                                           })
            End Select
        End If
    End Sub

    Private Sub mnuMainFileProfile_Click(sender As Object, e As EventArgs) Handles mnuMainFileProfile.Click
        Using dProfileSelect As New dlgProfileSelect
            Select Case dProfileSelect.ShowDialog
                'TODO: add restart with commandline
                '    Case DialogResult.OK
                '        If Not Master.SettingsPath = dProfileSelect.SelectedProfileFullPath Then
                '            If MessageBox.Show(Master.eLang.GetString(1112, "Do you want to restart Ember Media Manager and load the selected profile?"),
                '                               Master.eLang.GetString(298, "Restart Ember Media Manager?"),
                '                               MessageBoxButtons.YesNo,
                '                               MessageBoxIcon.Question) = DialogResult.Yes Then
                '                Application.Restart()
                '            End If
                '        End If
            End Select
        End Using
    End Sub

    Private Sub mnuMoviesetsMovieset_DropDown(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMoviesetsMovieset.DropDown
        Dim items As New Dictionary(Of MediaContainers.SetDetails, String)
        items.Add(New MediaContainers.SetDetails, Master.eLang.None)
        'If tmpDBElement.Movie.SetsSpecified Then
        '    items.Add(tmpDBElement.Movie.Sets.First, tmpDBElement.Movie.Sets.First.Title)
        'End If
        For Each nSet In Master.DB.GetAllMovieSetDetails.Where(Function(f) Not items.Keys.Contains(f) AndAlso Not items.Values.Contains(f.Title))
            items.Add(nSet, nSet.Title)
        Next
        mnuMoviesetsMovieset.ComboBox.BindingContext = BindingContext
        mnuMoviesetsMovieset.ComboBox.DataSource = items.ToList
        mnuMoviesetsMovieset.ComboBox.DisplayMember = "Value"
        mnuMoviesetsMovieset.ComboBox.ValueMember = "Key"
        'If tmpDBElement.Movie.SetsSpecified Then
        '    mnuMoviesetsMovieset.SelectedIndex = 1
        'Else
        'mnuMoviesetsMovieset.ComboBox.SelectedIndex = 0
        'End If
    End Sub

    Private Sub mnuMoviesetsSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMoviesetsSet.Click
        Dim nMovieset As MediaContainers.SetDetails = Nothing
        Select Case mnuMoviesetsMovieset.SelectedIndex
            Case -1
                'new manually added entry
                If Not String.IsNullOrEmpty(mnuMoviesetsMovieset.Text.Trim) Then
                    nMovieset = New MediaContainers.SetDetails With {
                        .Title = mnuMoviesetsMovieset.Text.Trim
                    }
                End If
            Case 0
                '[none]
                'do nothing, Sets will be cleared if nMovieset is Nothing
            Case Else
                'existing MovieSet has been selected
                nMovieset = DirectCast(mnuMoviesetsMovieset.SelectedItem, KeyValuePair(Of MediaContainers.SetDetails, String)).Key
        End Select

        Select Case _SelectedContentType
            Case "movie"
                TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.SetMovieset) With {
                                       .GenericObject = nMovieset,
                                       .ContentType = Enums.ContentType.Movie,
                                       .SelectionType = Enums.SelectionType.Selected
                                       })
        End Select
    End Sub

    Private Sub mnuMainToolsExportMovies_Click(sender As Object, e As EventArgs) Handles mnuMainToolsExportMovies.Click
        Try
            Dim table = Master.DB.GetMovies()
            table.TableName = "movie"

            Dim dlgSave As New SaveFileDialog()
            dlgSave.FileName = "export_movies" + ".xml"
            dlgSave.Filter = "xml files (*.xml)|*.xml"
            dlgSave.FilterIndex = 2
            dlgSave.RestoreDirectory = True

            If dlgSave.ShowDialog() = DialogResult.OK Then
                table.WriteXml(dlgSave.FileName)
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub mnuMainToolsExportTvShows_Click(sender As Object, e As EventArgs) Handles mnuMainToolsExportTvShows.Click
        Try
            Dim table = Master.DB.GetTVShows()
            table.TableName = "tvshow"

            Dim dlgSave As New SaveFileDialog()
            dlgSave.FileName = "export_tvshow" + ".xml"
            dlgSave.Filter = "xml files (*.xml)|*.xml"
            dlgSave.FilterIndex = 2
            dlgSave.RestoreDirectory = True

            If dlgSave.ShowDialog() = DialogResult.OK Then
                table.WriteXml(dlgSave.FileName)
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub mnuMainToolsReloadMovies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsReloadMovies.Click, cmnuTrayToolsReloadMovies.Click
        Reload_All_Movie()
    End Sub

    Private Sub mnuMainToolsReloadMovieSets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsReloadMovieSets.Click
        Reload_All_Movieset()
    End Sub

    Private Sub mnuMainToolsReloadTVShows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsReloadTVShows.Click
        Reload_All_TVShow(True)
    End Sub

    Private Sub mnuMainToolsRewriteContentMovieAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsRewriteContentMovieAll.Click
        Rewrite_All_Movie(True)
    End Sub

    Private Sub mnuMainToolsRewriteContentMovieNFO_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsRewriteContentMovieNFO.Click
        Rewrite_All_Movie(False)
    End Sub

    Private Sub mnuMainToolsRewriteContentMovieSetAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsRewriteContentMovieSetAll.Click
        Rewrite_All_Movieset(True)
    End Sub

    Private Sub mnuMainToolsRewriteContentMovieSetNFO_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsRewriteContentMovieSetNFO.Click
        Rewrite_All_Movieset(False)
    End Sub

    Private Sub mnuMainToolsRewriteContentTVShowAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsRewriteContentTVShowAll.Click
        Rewrite_All_TVShow(True)
    End Sub

    Private Sub mnuMainToolsRewriteContentTVShowNFO_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsRewriteContentTVShowNFO.Click
        Rewrite_All_TVShow(False)
    End Sub

    Private Sub mnuTagsAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuTagsAdd.Click
        Dim strTag As String = String.Empty
        If Not String.IsNullOrEmpty(mnuTagsNew.Text) Then
            strTag = mnuTagsNew.Text.Trim
        ElseIf Not String.IsNullOrEmpty(mnuTagsTag.Text.Trim) Then
            strTag = mnuTagsTag.Text.Trim
        End If

        If Not String.IsNullOrEmpty(strTag) Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Select Case _SelectedContentType
                    Case "movie"
                        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value))
                            If Not tmpDBElement.MainDetails.Tags.Contains(strTag) Then
                                tmpDBElement.MainDetails.Tags.Add(strTag)
                                Master.DB.Save_Movie(tmpDBElement, True, True, False, True, False)
                                DataGridView_RefreshRow_Movie(tmpDBElement.ID)
                            End If
                        Next
                    Case "tvshow"
                        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), False, False)
                            If Not tmpDBElement.MainDetails.Tags.Contains(strTag) Then
                                tmpDBElement.MainDetails.Tags.Add(strTag)
                                Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)
                                DataGridView_RefreshRow_TVShow(tmpDBElement.ID)
                            End If
                        Next
                End Select
                SQLtransaction.Commit()
            End Using
        End If
    End Sub

    Private Sub mnuTagsNew_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles mnuTagsNew.TextChanged
        If Not String.IsNullOrEmpty(mnuTagsNew.Text) Then
            If Not mnuTagsTag.Items.Contains(String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")) Then
                mnuTagsTag.Items.Insert(0, String.Concat(Master.eLang.GetString(1021, "Select Tag"), "..."))
            End If
            mnuTagsTag.SelectedItem = String.Concat(Master.eLang.GetString(1021, "Select Tag"), "...")
        End If

        If Not String.IsNullOrEmpty(mnuTagsNew.Text) Then
            mnuTagsAdd.Enabled = True
            mnuTagsRemove.Enabled = False
            mnuTagsSet.Enabled = True
        Else
            mnuTagsAdd.Enabled = False
            mnuTagsRemove.Enabled = False
            mnuTagsSet.Enabled = False
        End If
    End Sub

    Private Sub mnuTagsRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuTagsRemove.Click
        Dim strTag As String = String.Empty
        If Not String.IsNullOrEmpty(mnuTagsNew.Text) Then
            strTag = mnuTagsNew.Text.Trim
        ElseIf Not String.IsNullOrEmpty(mnuTagsTag.Text.Trim) Then
            strTag = mnuTagsTag.Text.Trim
        End If

        If Not String.IsNullOrEmpty(strTag) Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Select Case _SelectedContentType
                    Case "movie"
                        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value))
                            If tmpDBElement.MainDetails.Tags.Contains(strTag) Then
                                tmpDBElement.MainDetails.Tags.Remove(strTag)
                                Master.DB.Save_Movie(tmpDBElement, True, True, False, True, False)
                                DataGridView_RefreshRow_Movie(tmpDBElement.ID)
                            End If
                        Next
                    Case "tvshow"
                        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), False, False)
                            If tmpDBElement.MainDetails.Tags.Contains(strTag) Then
                                tmpDBElement.MainDetails.Tags.Remove(strTag)
                                Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)
                                DataGridView_RefreshRow_TVShow(tmpDBElement.ID)
                            End If
                        Next
                End Select
                SQLtransaction.Commit()
            End Using
        End If
    End Sub

    Private Sub mnuTagsSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuTagsSet.Click
        Dim strTag As String = String.Empty
        If Not String.IsNullOrEmpty(mnuTagsNew.Text) Then
            strTag = mnuTagsNew.Text.Trim
        ElseIf Not String.IsNullOrEmpty(mnuTagsTag.Text.Trim) Then
            strTag = mnuTagsTag.Text.Trim
        End If

        If Not String.IsNullOrEmpty(strTag) Then
            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Select Case _SelectedContentType
                    Case "movie"
                        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value))
                            tmpDBElement.MainDetails.Tags.Clear()
                            tmpDBElement.MainDetails.Tags.Add(strTag)
                            Master.DB.Save_Movie(tmpDBElement, True, True, False, True, False)
                            DataGridView_RefreshRow_Movie(tmpDBElement.ID)
                        Next
                    Case "tvshow"
                        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                            Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), False, False)
                            tmpDBElement.MainDetails.Tags.Clear()
                            tmpDBElement.MainDetails.Tags.Add(strTag)
                            Master.DB.Save_TVShow(tmpDBElement, True, True, False, False)
                            DataGridView_RefreshRow_TVShow(tmpDBElement.ID)
                        Next
                End Select
                SQLtransaction.Commit()
            End Using
        End If
    End Sub

    Private Sub mnuTagsTag_DropDown(ByVal sender As Object, ByVal e As EventArgs) Handles mnuTagsTag.DropDown
        mnuTagsTag.Items.Clear()
        Dim mTag() As Object = Master.DB.GetAllTags
        mnuTagsTag.Items.AddRange(mTag)

        mnuTagsNew.Text = String.Empty
    End Sub

    Private Sub mnuTagsTag_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles mnuTagsTag.SelectedIndexChanged
        Dim iSelectedRowsCount As Integer = 0
        Select Case _SelectedContentType
            Case "movie"
                iSelectedRowsCount = dgvMovies.SelectedRows.Count
            Case "tvshow"
                iSelectedRowsCount = dgvTVShows.SelectedRows.Count
        End Select

        If iSelectedRowsCount > 1 Then
            mnuTagsRemove.Enabled = True
            mnuTagsAdd.Enabled = True
        Else
            mnuTagsRemove.Enabled = mnuTagsTag.Tag.ToString.Contains(mnuTagsTag.Text)
            mnuTagsAdd.Enabled = Not mnuTagsTag.Tag.ToString.Contains(mnuTagsTag.Text)
        End If
        mnuTagsSet.Enabled = True
    End Sub

    Private Sub cmnuMovieSetSortMethodMethods_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieSetEditSortMethodMethods.SelectedIndexChanged
        cmnuMovieSetEditSortMethodSet.Enabled = True
    End Sub

    Private Sub LoadInfo_Movie(ByVal ID As Long)
        ShowNoInfo(False)
        ClearInfo()

        _CurrDBElement = Master.DB.Load_Movie(ID)
        FillScreenInfoWith_Movie()

        If bwLoadImages.IsBusy AndAlso Not bwLoadImages.CancellationPending Then
            bwLoadImages.CancelAsync()
        End If

        While bwLoadImages.IsBusy
            Application.DoEvents()
        End While

        bwLoadImages = New ComponentModel.BackgroundWorker With {
            .WorkerSupportsCancellation = True
        }
        bwLoadImages.RunWorkerAsync()
    End Sub

    Private Sub LoadInfo_Movieset(ByVal ID As Long)
        ShowNoInfo(False)
        ClearInfo()

        _CurrDBElement = Master.DB.Load_Movieset(ID)
        FillScreenInfoWith_Movieset()

        If bwLoadImages.IsBusy AndAlso Not bwLoadImages.CancellationPending Then
            bwLoadImages.CancelAsync()
        End If

        While bwLoadImages.IsBusy
            Application.DoEvents()
        End While

        bwLoadImages = New ComponentModel.BackgroundWorker With {
            .WorkerSupportsCancellation = True
        }
        bwLoadImages.RunWorkerAsync()
    End Sub

    Private Sub LoadInfo_TVEpisode(ByVal ID As Long)
        ShowNoInfo(False)
        ClearInfo()

        If Not currThemeType = Enums.ContentType.TVEpisode Then Theme_Apply(Enums.ContentType.TVEpisode)

        _CurrDBElement = Master.DB.Load_TVEpisode(ID, True)
        FillScreenInfoWith_TVEpisode()

        If bwLoadImages.IsBusy AndAlso Not bwLoadImages.CancellationPending Then
            bwLoadImages.CancelAsync()
        End If

        While bwLoadImages.IsBusy
            Application.DoEvents()
        End While

        bwLoadImages = New ComponentModel.BackgroundWorker With {
            .WorkerSupportsCancellation = True
        }
        bwLoadImages.RunWorkerAsync()
    End Sub

    Private Sub LoadInfo_TVSeason(ByVal ID As Long)
        ShowNoInfo(False)
        ClearInfo()

        If Not currThemeType = Enums.ContentType.TVSeason Then Theme_Apply(Enums.ContentType.TVSeason)

        _CurrDBElement = Master.DB.Load_TVSeason(ID, True, False)
        FillScreenInfoWith_TVSeason()

        If bwLoadImages.IsBusy AndAlso Not bwLoadImages.CancellationPending Then
            bwLoadImages.CancelAsync()
        End If

        While bwLoadImages.IsBusy
            Application.DoEvents()
        End While

        bwLoadImages = New ComponentModel.BackgroundWorker With {
            .WorkerSupportsCancellation = True
        }
        bwLoadImages.RunWorkerAsync()
    End Sub

    Private Sub LoadInfo_TVShow(ByVal ID As Long)
        ShowNoInfo(False)
        ClearInfo()

        If Not currThemeType = Enums.ContentType.TVShow Then Theme_Apply(Enums.ContentType.TVShow)

        _CurrDBElement = Master.DB.Load_TVShow(ID, False, False)
        FillScreenInfoWith_TVShow()

        If bwLoadImages.IsBusy AndAlso Not bwLoadImages.CancellationPending Then
            bwLoadImages.CancelAsync()
        End If

        While bwLoadImages.IsBusy
            Application.DoEvents()
        End While

        bwLoadImages = New ComponentModel.BackgroundWorker With {
            .WorkerSupportsCancellation = True
        }
        bwLoadImages.RunWorkerAsync()

        DataGridView_Fill_TVSeason(ID)
    End Sub

    Private Sub lstActors_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstActors.SelectedValueChanged
        If lstActors.Items.Count > 0 AndAlso lstActors.SelectedItems.Count > 0 AndAlso alActors.Item(lstActors.SelectedIndex) IsNot Nothing AndAlso Not alActors.Item(lstActors.SelectedIndex).ToString = "none" Then

            If pbActors.Image IsNot Nothing Then
                pbActors.Image.Dispose()
                pbActors.Image = Nothing
            End If

            If Not alActors.Item(lstActors.SelectedIndex).ToString.Trim.StartsWith("http") Then
                MainActors.LoadFromFile(alActors.Item(lstActors.SelectedIndex).ToString, True)

                If MainActors.Image IsNot Nothing Then
                    pbActors.Image = MainActors.Image
                Else
                    pbActors.Image = My.Resources.actor_silhouette
                End If
            Else
                pbActorsLoad.Visible = True

                If bwDownloadPic.IsBusy Then
                    bwDownloadPic.CancelAsync()
                    While bwDownloadPic.IsBusy
                        Application.DoEvents()
                        Threading.Thread.Sleep(50)
                    End While
                End If

                bwDownloadPic = New ComponentModel.BackgroundWorker
                bwDownloadPic.WorkerSupportsCancellation = True
                bwDownloadPic.RunWorkerAsync(New Arguments With {.pURL = alActors.Item(lstActors.SelectedIndex).ToString})
            End If

        Else
            pbActors.Image = My.Resources.actor_silhouette
        End If
    End Sub

    Private Sub lstGuestStars_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstGuestStars.SelectedValueChanged
        If lstGuestStars.Items.Count > 0 AndAlso lstGuestStars.SelectedItems.Count > 0 AndAlso alGuestStars.Item(lstGuestStars.SelectedIndex) IsNot Nothing AndAlso Not alGuestStars.Item(lstGuestStars.SelectedIndex).ToString = "none" Then

            If pbGuestStars.Image IsNot Nothing Then
                pbGuestStars.Image.Dispose()
                pbGuestStars.Image = Nothing
            End If

            If Not alGuestStars.Item(lstGuestStars.SelectedIndex).ToString.Trim.StartsWith("http") Then
                MainGuestStars.LoadFromFile(alGuestStars.Item(lstGuestStars.SelectedIndex).ToString, True)

                If MainGuestStars.Image IsNot Nothing Then
                    pbGuestStars.Image = MainGuestStars.Image
                Else
                    pbGuestStars.Image = My.Resources.actor_silhouette
                End If
            Else
                pbGuestStarsLoad.Visible = True

                If bwDownloadGuestStarPic.IsBusy Then
                    bwDownloadGuestStarPic.CancelAsync()
                    While bwDownloadGuestStarPic.IsBusy
                        Application.DoEvents()
                        Threading.Thread.Sleep(50)
                    End While
                End If

                bwDownloadGuestStarPic = New ComponentModel.BackgroundWorker With {
                    .WorkerSupportsCancellation = True
                }
                bwDownloadGuestStarPic.RunWorkerAsync(New Arguments With {.pURL = alGuestStars.Item(lstGuestStars.SelectedIndex).ToString})
            End If

        Else
            pbGuestStars.Image = My.Resources.actor_silhouette
        End If
    End Sub

    Private Sub mnuContextMenuStrip_Opened(sender As Object, e As EventArgs) Handles mnuGenres.Opened, mnuLanguages.Opened, mnuMoviesets.Opened, mnuTags.Opened, mnuScrapeSubmenu.Opened
        Dim tContextMenuStrip As ContextMenuStrip = CType(sender, ContextMenuStrip)
        If tContextMenuStrip IsNot Nothing AndAlso tContextMenuStrip.OwnerItem IsNot Nothing AndAlso tContextMenuStrip.OwnerItem.Tag IsNot Nothing Then
            _SelectedContentType = tContextMenuStrip.OwnerItem.Tag.ToString
        End If
    End Sub

    Private Sub mnuScrapeMovies_ButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles mnuScrapeMovies.ButtonClick
        If Manager.mSettings.Movie.GuiSettings.CustomScrapeButtonEnabled Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Manager.mSettings.Movie.GuiSettings.CustomScrapeButtonModifierType, True)
            CreateScrapeList_Movie(Manager.mSettings.Movie.GuiSettings.CustomScrapeButtonScrapeType, Master.eSettings.DefaultScrapeOptions(Enums.ContentType.Movie), ScrapeModifiers)
        Else
            mnuScrapeMovies.ShowDropDown()
        End If
    End Sub

    Private Sub mnuScrapeMoviesets_ButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles mnuScrapeMoviesets.ButtonClick
        If Manager.mSettings.Movieset.GuiSettings.CustomScrapeButtonEnabled Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Manager.mSettings.Movieset.GuiSettings.CustomScrapeButtonModifierType, True)
            CreateScrapeList_Movieset(Manager.mSettings.Movieset.GuiSettings.CustomScrapeButtonScrapeType, Master.eSettings.DefaultScrapeOptions(Enums.ContentType.Movieset), ScrapeModifiers)
        Else
            mnuScrapeMoviesets.ShowDropDown()
        End If
    End Sub

    Private Sub mnuScrapeTVShows_ButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles mnuScrapeTVShows.ButtonClick
        If Manager.mSettings.TVShow.GuiSettings.CustomScrapeButtonEnabled Then
            Dim ScrapeModifiers As New Structures.ScrapeModifiers
            Functions.SetScrapeModifiers(ScrapeModifiers, Manager.mSettings.TVShow.GuiSettings.CustomScrapeButtonModifierType, True)
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withEpisodes, True)
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.withSeasons, True)
            CreateScrapeList_TV(Manager.mSettings.TVShow.GuiSettings.CustomScrapeButtonScrapeType, Master.eSettings.DefaultScrapeOptions(Enums.ContentType.TV), ScrapeModifiers)
        Else
            mnuScrapeTVShows.ShowDropDown()
        End If
    End Sub

    Private Sub mnuScrapeOption_Opened(sender As Object, e As EventArgs) Handles mnuScrapeOption.Opened
        _SelectedContentType = mnuScrapeOption.OwnerItem.Tag.ToString

        With Master.eSettings
            Select Case _SelectedContentType
                Case "movie"
                    With Master.eSettings.Movie.DataSettings
                        mnuScrapeOptionActors.Enabled = .Actors.Enabled
                        mnuScrapeOptionActors.Visible = True
                        mnuScrapeOptionAired.Enabled = False
                        mnuScrapeOptionAired.Visible = False
                        mnuScrapeOptionCertifications.Enabled = .Certifications.Enabled
                        mnuScrapeOptionCertifications.Visible = True
                        mnuScrapeOptionCollection.Enabled = .Collection.Enabled
                        mnuScrapeOptionCollection.Visible = True
                        mnuScrapeOptionCountries.Enabled = .Countries.Enabled
                        mnuScrapeOptionCountries.Visible = True
                        mnuScrapeOptionCreators.Enabled = False
                        mnuScrapeOptionCreators.Visible = False
                        mnuScrapeOptionDirectors.Enabled = .Directors.Enabled
                        mnuScrapeOptionDirectors.Visible = True
                        mnuScrapeOptionEpiGuideURL.Enabled = False
                        mnuScrapeOptionEpiGuideURL.Visible = False
                        mnuScrapeOptionGenres.Enabled = .Genres.Enabled
                        mnuScrapeOptionGenres.Visible = True
                        mnuScrapeOptionGuestStars.Enabled = False
                        mnuScrapeOptionGuestStars.Visible = False
                        mnuScrapeOptionMPAA.Enabled = .MPAA.Enabled
                        mnuScrapeOptionMPAA.Visible = True
                        mnuScrapeOptionOriginalTitle.Enabled = .OriginalTitle.Enabled
                        mnuScrapeOptionOriginalTitle.Visible = True
                        mnuScrapeOptionOutline.Enabled = .Outline.Enabled
                        mnuScrapeOptionOutline.Visible = True
                        mnuScrapeOptionPlot.Enabled = .Plot.Enabled
                        mnuScrapeOptionPlot.Visible = True
                        mnuScrapeOptionPremiered.Enabled = .Premiered.Enabled
                        mnuScrapeOptionPremiered.Visible = True
                        mnuScrapeOptionRating.Enabled = .Ratings.Enabled
                        mnuScrapeOptionRating.Visible = True
                        mnuScrapeOptionRuntime.Enabled = .Runtime.Enabled
                        mnuScrapeOptionRuntime.Visible = True
                        mnuScrapeOptionStatus.Enabled = False
                        mnuScrapeOptionStatus.Visible = False
                        mnuScrapeOptionStudios.Enabled = .Studios.Enabled
                        mnuScrapeOptionStudios.Visible = True
                        mnuScrapeOptionTagline.Enabled = .Tagline.Enabled
                        mnuScrapeOptionTagline.Visible = True
                        mnuScrapeOptionTags.Enabled = .Tags.Enabled
                        mnuScrapeOptionTags.Visible = True
                        mnuScrapeOptionTitle.Enabled = .Title.Enabled
                        mnuScrapeOptionTitle.Visible = True
                        mnuScrapeOptionTop250.Enabled = .Top250.Enabled
                        mnuScrapeOptionTop250.Visible = True
                        mnuScrapeOptionTrailer.Enabled = .TrailerLink.Enabled
                        mnuScrapeOptionTrailer.Visible = True
                        mnuScrapeOptionUserRating.Enabled = .UserRating.Enabled
                        mnuScrapeOptionUserRating.Visible = True
                        mnuScrapeOptionWriters.Enabled = .Credits.Enabled
                        mnuScrapeOptionWriters.Visible = True
                    End With
                Case "movieset"
                    mnuScrapeOptionActors.Enabled = False
                    mnuScrapeOptionActors.Visible = False
                    mnuScrapeOptionAired.Enabled = False
                    mnuScrapeOptionAired.Visible = False
                    mnuScrapeOptionCertifications.Enabled = False
                    mnuScrapeOptionCertifications.Visible = False
                    mnuScrapeOptionCollection.Enabled = False
                    mnuScrapeOptionCollection.Visible = False
                    mnuScrapeOptionCountries.Enabled = False
                    mnuScrapeOptionCountries.Visible = False
                    mnuScrapeOptionCreators.Enabled = False
                    mnuScrapeOptionCreators.Visible = False
                    mnuScrapeOptionDirectors.Enabled = False
                    mnuScrapeOptionDirectors.Visible = False
                    mnuScrapeOptionEpiGuideURL.Enabled = False
                    mnuScrapeOptionEpiGuideURL.Visible = False
                    mnuScrapeOptionGenres.Enabled = False
                    mnuScrapeOptionGenres.Visible = False
                    mnuScrapeOptionGuestStars.Enabled = False
                    mnuScrapeOptionGuestStars.Visible = False
                    mnuScrapeOptionMPAA.Enabled = False
                    mnuScrapeOptionMPAA.Visible = False
                    mnuScrapeOptionOriginalTitle.Enabled = False
                    mnuScrapeOptionOriginalTitle.Visible = False
                    mnuScrapeOptionOutline.Enabled = False
                    mnuScrapeOptionOutline.Visible = False
                    mnuScrapeOptionPlot.Enabled = .MoviesetScraperPlot
                    mnuScrapeOptionPlot.Visible = True
                    mnuScrapeOptionPremiered.Enabled = False
                    mnuScrapeOptionPremiered.Visible = False
                    mnuScrapeOptionRating.Enabled = False
                    mnuScrapeOptionRating.Visible = False
                    mnuScrapeOptionRuntime.Enabled = False
                    mnuScrapeOptionRuntime.Visible = False
                    mnuScrapeOptionStatus.Enabled = False
                    mnuScrapeOptionStatus.Visible = False
                    mnuScrapeOptionStudios.Enabled = False
                    mnuScrapeOptionStudios.Visible = False
                    mnuScrapeOptionTagline.Enabled = False
                    mnuScrapeOptionTagline.Visible = False
                    mnuScrapeOptionTags.Enabled = False
                    mnuScrapeOptionTags.Visible = False
                    mnuScrapeOptionTitle.Enabled = .MoviesetScraperTitle
                    mnuScrapeOptionTitle.Visible = True
                    mnuScrapeOptionTop250.Enabled = False
                    mnuScrapeOptionTop250.Visible = False
                    mnuScrapeOptionTrailer.Enabled = False
                    mnuScrapeOptionTrailer.Visible = False
                    mnuScrapeOptionUserRating.Enabled = False
                    mnuScrapeOptionUserRating.Visible = False
                    mnuScrapeOptionWriters.Enabled = False
                    mnuScrapeOptionWriters.Visible = False
                Case "tvepisode"
                    mnuScrapeOptionActors.Enabled = .TVScraperEpisodeActors
                    mnuScrapeOptionActors.Visible = True
                    mnuScrapeOptionAired.Enabled = .TVScraperEpisodeAired
                    mnuScrapeOptionAired.Visible = True
                    mnuScrapeOptionCertifications.Enabled = False
                    mnuScrapeOptionCertifications.Visible = False
                    mnuScrapeOptionCollection.Enabled = False
                    mnuScrapeOptionCollection.Visible = False
                    mnuScrapeOptionCountries.Enabled = False
                    mnuScrapeOptionCountries.Visible = False
                    mnuScrapeOptionCreators.Enabled = False
                    mnuScrapeOptionCreators.Visible = False
                    mnuScrapeOptionDirectors.Enabled = .TVScraperEpisodeDirector
                    mnuScrapeOptionDirectors.Visible = True
                    mnuScrapeOptionEpiGuideURL.Enabled = False
                    mnuScrapeOptionEpiGuideURL.Visible = False
                    mnuScrapeOptionGenres.Enabled = False
                    mnuScrapeOptionGenres.Visible = False
                    mnuScrapeOptionGuestStars.Enabled = .TVScraperEpisodeGuestStars
                    mnuScrapeOptionGuestStars.Visible = True
                    mnuScrapeOptionMPAA.Enabled = False
                    mnuScrapeOptionMPAA.Visible = False
                    mnuScrapeOptionOriginalTitle.Enabled = False
                    mnuScrapeOptionOriginalTitle.Visible = False
                    mnuScrapeOptionOutline.Enabled = False
                    mnuScrapeOptionOutline.Visible = False
                    mnuScrapeOptionPlot.Enabled = .TVScraperEpisodePlot
                    mnuScrapeOptionPlot.Visible = True
                    mnuScrapeOptionPremiered.Enabled = False
                    mnuScrapeOptionPremiered.Visible = False
                    mnuScrapeOptionRating.Enabled = .TVScraperEpisodeRating
                    mnuScrapeOptionRating.Visible = True
                    mnuScrapeOptionRuntime.Enabled = .TVScraperEpisodeRuntime
                    mnuScrapeOptionRuntime.Visible = True
                    mnuScrapeOptionStatus.Enabled = False
                    mnuScrapeOptionStatus.Visible = False
                    mnuScrapeOptionStudios.Enabled = False
                    mnuScrapeOptionStudios.Visible = False
                    mnuScrapeOptionTagline.Enabled = False
                    mnuScrapeOptionTagline.Visible = False
                    mnuScrapeOptionTags.Enabled = False
                    mnuScrapeOptionTags.Visible = False
                    mnuScrapeOptionTitle.Enabled = .TVScraperEpisodeTitle
                    mnuScrapeOptionTitle.Visible = True
                    mnuScrapeOptionTop250.Enabled = False
                    mnuScrapeOptionTop250.Visible = False
                    mnuScrapeOptionTrailer.Enabled = False
                    mnuScrapeOptionTrailer.Visible = False
                    mnuScrapeOptionUserRating.Enabled = .TVScraperEpisodeUserRating
                    mnuScrapeOptionUserRating.Visible = True
                    mnuScrapeOptionWriters.Enabled = .TVScraperEpisodeCredits
                    mnuScrapeOptionWriters.Visible = True
                Case "tvseason"
                    mnuScrapeOptionActors.Enabled = False
                    mnuScrapeOptionActors.Visible = False
                    mnuScrapeOptionAired.Enabled = .TVScraperSeasonAired
                    mnuScrapeOptionAired.Visible = True
                    mnuScrapeOptionCertifications.Enabled = False
                    mnuScrapeOptionCertifications.Visible = False
                    mnuScrapeOptionCollection.Enabled = False
                    mnuScrapeOptionCollection.Visible = False
                    mnuScrapeOptionCountries.Enabled = False
                    mnuScrapeOptionCountries.Visible = False
                    mnuScrapeOptionCreators.Enabled = False
                    mnuScrapeOptionCreators.Visible = False
                    mnuScrapeOptionDirectors.Enabled = False
                    mnuScrapeOptionDirectors.Visible = False
                    mnuScrapeOptionEpiGuideURL.Enabled = False
                    mnuScrapeOptionEpiGuideURL.Visible = False
                    mnuScrapeOptionGenres.Enabled = False
                    mnuScrapeOptionGenres.Visible = False
                    mnuScrapeOptionGuestStars.Enabled = False
                    mnuScrapeOptionGuestStars.Visible = False
                    mnuScrapeOptionMPAA.Enabled = False
                    mnuScrapeOptionMPAA.Visible = False
                    mnuScrapeOptionOriginalTitle.Enabled = False
                    mnuScrapeOptionOriginalTitle.Visible = False
                    mnuScrapeOptionOutline.Enabled = False
                    mnuScrapeOptionOutline.Visible = False
                    mnuScrapeOptionPlot.Enabled = .TVScraperSeasonPlot
                    mnuScrapeOptionPlot.Visible = True
                    mnuScrapeOptionPremiered.Enabled = False
                    mnuScrapeOptionPremiered.Visible = False
                    mnuScrapeOptionRating.Enabled = False
                    mnuScrapeOptionRating.Visible = False
                    mnuScrapeOptionRuntime.Enabled = False
                    mnuScrapeOptionRuntime.Visible = False
                    mnuScrapeOptionStatus.Enabled = False
                    mnuScrapeOptionStatus.Visible = False
                    mnuScrapeOptionStudios.Enabled = False
                    mnuScrapeOptionStudios.Visible = False
                    mnuScrapeOptionTagline.Enabled = False
                    mnuScrapeOptionTagline.Visible = False
                    mnuScrapeOptionTags.Enabled = False
                    mnuScrapeOptionTags.Visible = False
                    mnuScrapeOptionTitle.Enabled = .TVScraperSeasonTitle
                    mnuScrapeOptionTitle.Visible = True
                    mnuScrapeOptionTop250.Enabled = False
                    mnuScrapeOptionTop250.Visible = False
                    mnuScrapeOptionTrailer.Enabled = False
                    mnuScrapeOptionTrailer.Visible = False
                    mnuScrapeOptionUserRating.Enabled = False
                    mnuScrapeOptionUserRating.Visible = True
                    mnuScrapeOptionWriters.Enabled = False
                    mnuScrapeOptionWriters.Visible = False
                Case "tvshow"
                    mnuScrapeOptionActors.Enabled = .TVScraperShowActors
                    mnuScrapeOptionActors.Visible = True
                    mnuScrapeOptionAired.Enabled = False
                    mnuScrapeOptionAired.Visible = False
                    mnuScrapeOptionCertifications.Enabled = .TVScraperShowCert
                    mnuScrapeOptionCertifications.Visible = True
                    mnuScrapeOptionCollection.Enabled = False
                    mnuScrapeOptionCollection.Visible = False
                    mnuScrapeOptionCountries.Enabled = .TVScraperShowCountry
                    mnuScrapeOptionCountries.Visible = True
                    mnuScrapeOptionCreators.Enabled = .TVScraperShowCreators
                    mnuScrapeOptionCreators.Visible = True
                    mnuScrapeOptionDirectors.Enabled = False
                    mnuScrapeOptionDirectors.Visible = False
                    mnuScrapeOptionEpiGuideURL.Enabled = .TVScraperShowEpiGuideURL
                    mnuScrapeOptionEpiGuideURL.Visible = True
                    mnuScrapeOptionGenres.Enabled = .TVScraperShowGenre
                    mnuScrapeOptionGenres.Visible = True
                    mnuScrapeOptionGuestStars.Enabled = False
                    mnuScrapeOptionGuestStars.Visible = False
                    mnuScrapeOptionMPAA.Enabled = .TVScraperShowMPAA
                    mnuScrapeOptionMPAA.Visible = True
                    mnuScrapeOptionOriginalTitle.Enabled = .TVScraperShowOriginalTitle
                    mnuScrapeOptionOriginalTitle.Visible = True
                    mnuScrapeOptionOutline.Enabled = False
                    mnuScrapeOptionOutline.Visible = False
                    mnuScrapeOptionPlot.Enabled = .TVScraperShowPlot
                    mnuScrapeOptionPlot.Visible = True
                    mnuScrapeOptionPremiered.Enabled = .TVScraperShowPremiered
                    mnuScrapeOptionPremiered.Visible = True
                    mnuScrapeOptionRating.Enabled = .TVScraperShowRating
                    mnuScrapeOptionRating.Visible = True
                    mnuScrapeOptionRuntime.Enabled = .TVScraperShowRuntime
                    mnuScrapeOptionRuntime.Visible = True
                    mnuScrapeOptionStatus.Enabled = .TVScraperShowStatus
                    mnuScrapeOptionStatus.Visible = True
                    mnuScrapeOptionStudios.Enabled = .TVScraperShowStudio
                    mnuScrapeOptionStudios.Visible = True
                    mnuScrapeOptionTagline.Enabled = False
                    mnuScrapeOptionTagline.Visible = False
                    'mnuScrapeOptionTags.Enabled = .Tags.Enabled
                    mnuScrapeOptionTags.Visible = True
                    mnuScrapeOptionTitle.Enabled = .TVScraperShowTitle
                    mnuScrapeOptionTitle.Visible = True
                    mnuScrapeOptionTop250.Enabled = False
                    mnuScrapeOptionTop250.Visible = False
                    mnuScrapeOptionTrailer.Enabled = False
                    mnuScrapeOptionTrailer.Visible = False
                    mnuScrapeOptionUserRating.Enabled = .TVScraperShowUserRating
                    mnuScrapeOptionUserRating.Visible = True
                    mnuScrapeOptionWriters.Enabled = False
                    mnuScrapeOptionWriters.Visible = False
            End Select
        End With
    End Sub

    Private Sub mnuScrapeModifier_Opened(sender As Object, e As EventArgs) Handles mnuScrapeModifier.Opened
        _SelectedScrapeTypeMode = mnuScrapeModifier.OwnerItem.Tag.ToString

        With Master.eSettings
            Select Case _SelectedContentType
                Case "movie"
                    mnuScrapeModifierActorthumbs.Enabled = .MovieActorThumbsAnyEnabled
                    mnuScrapeModifierActorthumbs.Visible = True
                    mnuScrapeModifierBanner.Enabled = .MovieBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
                    mnuScrapeModifierBanner.Visible = True
                    mnuScrapeModifierCharacterArt.Enabled = False
                    mnuScrapeModifierCharacterArt.Visible = False
                    mnuScrapeModifierClearArt.Enabled = .MovieClearArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt)
                    mnuScrapeModifierClearArt.Visible = True
                    mnuScrapeModifierClearLogo.Enabled = .MovieClearLogoAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo)
                    mnuScrapeModifierClearLogo.Visible = True
                    mnuScrapeModifierDiscArt.Enabled = .MovieDiscArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt)
                    mnuScrapeModifierDiscArt.Visible = True
                    mnuScrapeModifierExtrafanarts.Enabled = .MovieExtrafanartsAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainExtrafanarts)
                    mnuScrapeModifierExtrafanarts.Visible = True
                    mnuScrapeModifierExtrathumbs.Enabled = .MovieExtrathumbsAnyEnabled AndAlso (AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainExtrathumbs) OrElse Master.eSettings.MovieExtrathumbsVideoExtraction)
                    mnuScrapeModifierExtrathumbs.Visible = True
                    mnuScrapeModifierFanart.Enabled = .MovieFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierKeyArt.Enabled = .MovieKeyArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainKeyArt)
                    mnuScrapeModifierKeyArt.Visible = True
                    mnuScrapeModifierLandscape.Enabled = .MovieLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape)
                    mnuScrapeModifierLandscape.Visible = True
                    mnuScrapeModifierMetadata.Enabled = .Movie.DataSettings.MetadataScan.Enabled
                    mnuScrapeModifierMetadata.Visible = True
                    mnuScrapeModifierNFO.Enabled = True
                    mnuScrapeModifierNFO.Visible = True
                    mnuScrapeModifierPoster.Enabled = .MoviePosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
                    mnuScrapeModifierPoster.Visible = True
                    mnuScrapeModifierTheme.Enabled = .MovieThemeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie(Enums.ModifierType.MainTheme)
                    mnuScrapeModifierTheme.Visible = True
                    mnuScrapeModifierTrailer.Enabled = .MovieTrailerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Trailer_Movie(Enums.ModifierType.MainTrailer)
                    mnuScrapeModifierTrailer.Visible = True
                Case "movieset"
                    mnuScrapeModifierActorthumbs.Enabled = False
                    mnuScrapeModifierActorthumbs.Visible = False
                    mnuScrapeModifierBanner.Enabled = .MovieSetBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainBanner)
                    mnuScrapeModifierBanner.Visible = True
                    mnuScrapeModifierCharacterArt.Enabled = False
                    mnuScrapeModifierCharacterArt.Visible = False
                    mnuScrapeModifierClearArt.Enabled = .MovieSetClearArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearArt)
                    mnuScrapeModifierClearArt.Visible = True
                    mnuScrapeModifierClearLogo.Enabled = .MovieSetClearLogoAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearLogo)
                    mnuScrapeModifierClearLogo.Visible = True
                    mnuScrapeModifierDiscArt.Enabled = .MovieSetDiscArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainDiscArt)
                    mnuScrapeModifierDiscArt.Visible = True
                    mnuScrapeModifierExtrafanarts.Enabled = False
                    mnuScrapeModifierExtrafanarts.Visible = False
                    mnuScrapeModifierExtrathumbs.Enabled = False
                    mnuScrapeModifierExtrathumbs.Visible = False
                    mnuScrapeModifierFanart.Enabled = .MovieSetFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierKeyArt.Enabled = .MovieSetKeyArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainKeyArt)
                    mnuScrapeModifierKeyArt.Visible = True
                    mnuScrapeModifierLandscape.Enabled = .MovieSetLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainLandscape)
                    mnuScrapeModifierLandscape.Visible = True
                    mnuScrapeModifierMetadata.Enabled = False
                    mnuScrapeModifierMetadata.Visible = False
                    mnuScrapeModifierNFO.Enabled = True
                    mnuScrapeModifierNFO.Visible = True
                    mnuScrapeModifierPoster.Enabled = .MovieSetPosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainPoster)
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
                    mnuScrapeModifierFanart.Enabled = .TVEpisodeFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierKeyArt.Enabled = False
                    mnuScrapeModifierKeyArt.Visible = False
                    mnuScrapeModifierLandscape.Enabled = False
                    mnuScrapeModifierLandscape.Visible = False
                    mnuScrapeModifierMetadata.Enabled = .TVScraperMetaDataScan
                    mnuScrapeModifierMetadata.Visible = True
                    mnuScrapeModifierNFO.Enabled = True
                    mnuScrapeModifierNFO.Visible = True
                    mnuScrapeModifierPoster.Enabled = .TVEpisodePosterAnyEnabled AndAlso (AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster) OrElse Master.eSettings.TVEpisodePosterVideoExtraction)
                    mnuScrapeModifierPoster.Visible = True
                    mnuScrapeModifierTheme.Enabled = False
                    mnuScrapeModifierTheme.Visible = False
                    mnuScrapeModifierTrailer.Enabled = False
                    mnuScrapeModifierTrailer.Visible = False
                Case "tvseason"
                    mnuScrapeModifierActorthumbs.Enabled = False
                    mnuScrapeModifierActorthumbs.Visible = False
                    mnuScrapeModifierBanner.Enabled = .TVSeasonBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)
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
                    mnuScrapeModifierFanart.Enabled = .TVSeasonFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierKeyArt.Enabled = False
                    mnuScrapeModifierKeyArt.Visible = False
                    mnuScrapeModifierLandscape.Enabled = .TVSeasonLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)
                    mnuScrapeModifierLandscape.Visible = True
                    mnuScrapeModifierMetadata.Enabled = False
                    mnuScrapeModifierMetadata.Visible = False
                    mnuScrapeModifierNFO.Enabled = False
                    mnuScrapeModifierNFO.Visible = False
                    mnuScrapeModifierPoster.Enabled = .TVSeasonPosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)
                    mnuScrapeModifierPoster.Visible = True
                    mnuScrapeModifierTheme.Enabled = False
                    mnuScrapeModifierTheme.Visible = False
                    mnuScrapeModifierTrailer.Enabled = False
                    mnuScrapeModifierTrailer.Visible = False
                Case "tvshow"
                    mnuScrapeModifierActorthumbs.Enabled = .TVShowActorThumbsAnyEnabled
                    mnuScrapeModifierActorthumbs.Visible = True
                    mnuScrapeModifierBanner.Enabled = .TVShowBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)
                    mnuScrapeModifierBanner.Visible = True
                    mnuScrapeModifierCharacterArt.Enabled = .TVShowCharacterArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
                    mnuScrapeModifierCharacterArt.Visible = True
                    mnuScrapeModifierClearArt.Enabled = .TVShowClearArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
                    mnuScrapeModifierClearArt.Visible = True
                    mnuScrapeModifierClearLogo.Enabled = .TVShowClearLogoAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
                    mnuScrapeModifierClearLogo.Visible = True
                    mnuScrapeModifierDiscArt.Enabled = False
                    mnuScrapeModifierDiscArt.Visible = False
                    mnuScrapeModifierExtrafanarts.Enabled = .TVShowExtrafanartsAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainExtrafanarts)
                    mnuScrapeModifierExtrafanarts.Visible = True
                    mnuScrapeModifierExtrathumbs.Enabled = False
                    mnuScrapeModifierExtrathumbs.Visible = False
                    mnuScrapeModifierFanart.Enabled = .TVShowFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    mnuScrapeModifierFanart.Visible = True
                    mnuScrapeModifierKeyArt.Enabled = .TVShowKeyArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainKeyArt)
                    mnuScrapeModifierKeyArt.Visible = True
                    mnuScrapeModifierLandscape.Enabled = .TVShowLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
                    mnuScrapeModifierLandscape.Visible = True
                    mnuScrapeModifierMetadata.Enabled = False
                    mnuScrapeModifierMetadata.Visible = False
                    mnuScrapeModifierNFO.Enabled = True
                    mnuScrapeModifierNFO.Visible = True
                    mnuScrapeModifierPoster.Enabled = .TVShowPosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
                    mnuScrapeModifierPoster.Visible = True
                    mnuScrapeModifierTheme.Enabled = .TvShowThemeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_TV(Enums.ModifierType.MainTheme)
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

    Private Sub Autoscraper(sender As Object, e As EventArgs) Handles _
        mnuScrapeModifierActorthumbs.Click,
        mnuScrapeModifierAll.Click,
        mnuScrapeModifierBanner.Click,
        mnuScrapeModifierCharacterArt.Click,
        mnuScrapeModifierClearArt.Click,
        mnuScrapeModifierClearLogo.Click,
        mnuScrapeModifierDiscArt.Click,
        mnuScrapeModifierExtrafanarts.Click,
        mnuScrapeModifierExtrathumbs.Click,
        mnuScrapeModifierFanart.Click,
        mnuScrapeModifierKeyArt.Click,
        mnuScrapeModifierLandscape.Click,
        mnuScrapeModifierMetadata.Click,
        mnuScrapeModifierNFO.Click,
        mnuScrapeModifierPoster.Click,
        mnuScrapeModifierTheme.Click,
        mnuScrapeModifierTrailer.Click,
        mnuScrapeSubmenuCustom.Click

        Dim strContentType As String = String.Empty
        Dim ModifierType As String = String.Empty
        Dim strScrapeType As String = String.Empty
        Dim eScrapeType As Enums.ScrapeType
        Dim eSelectionType As Enums.SelectionType
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Dim Menu As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        ModifierType = Menu.Tag.ToString
        strScrapeType = String.Concat(_SelectedScrapeType, "_", _SelectedScrapeTypeMode)
        strContentType = _SelectedContentType

        Dim eContentType As Enums.ContentType = Enums.ContentType.None
        Select Case strContentType
            Case "movie"
                eContentType = Enums.ContentType.Movie
            Case "movieset"
                eContentType = Enums.ContentType.Movieset
            Case "tvepisode"
                eContentType = Enums.ContentType.TVEpisode
            Case "tvseason"
                eContentType = Enums.ContentType.TVSeason
            Case "tvshow"
                eContentType = Enums.ContentType.TVShow
                ScrapeModifiers.withEpisodes = True
                ScrapeModifiers.withSeasons = True
            Case Else
                Return
        End Select

        If Not ModifierType = "custom" Then
            Select Case ModifierType
                Case "all"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.All, True)
                Case "actorthumbs"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainActorThumbs, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeActorThumbs, True)
                Case "banner"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainBanner, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsBanner, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonBanner, True)
                Case "characterart"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainCharacterArt, True)
                Case "clearart"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
                Case "clearlogo"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
                Case "discart"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainDiscArt, True)
                Case "extrafanarts"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainExtrafanarts, True)
                Case "extrathumbs"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainExtrathumbs, True)
                Case "fanart"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsFanart, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeFanart, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonFanart, True)
                Case "keyart"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainKeyArt, True)
                Case "landscape"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsLandscape, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonLandscape, True)
                Case "metadata"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainMetadata, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeMetadata, True)
                Case "nfo"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainNFO, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeNFO, True)
                Case "poster"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsPoster, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodePoster, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonPoster, True)
                Case "subtitle"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainSubtitle, True)
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeSubtitle, True)
                Case "theme"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainTheme, True)
                Case "trailer"
                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainTrailer, True)
            End Select

            Select Case strScrapeType
                Case "all_ask"
                    eScrapeType = Enums.ScrapeType.Ask
                    eSelectionType = Enums.SelectionType.All
                Case "all_auto"
                    eScrapeType = Enums.ScrapeType.Auto
                    eSelectionType = Enums.SelectionType.All
                Case "all_skip"
                    eScrapeType = Enums.ScrapeType.Skip
                    eSelectionType = Enums.SelectionType.All
                Case "filter_ask"
                    eScrapeType = Enums.ScrapeType.Ask
                    eSelectionType = Enums.SelectionType.Filtered
                Case "filter_auto"
                    eScrapeType = Enums.ScrapeType.Auto
                    eSelectionType = Enums.SelectionType.Filtered
                Case "filter_skip"
                    eScrapeType = Enums.ScrapeType.Skip
                    eSelectionType = Enums.SelectionType.Filtered
                Case "marked_ask"
                    eScrapeType = Enums.ScrapeType.Ask
                    eSelectionType = Enums.SelectionType.Marked
                Case "marked_auto"
                    eScrapeType = Enums.ScrapeType.Auto
                    eSelectionType = Enums.SelectionType.Marked
                Case "marked_skip"
                    eScrapeType = Enums.ScrapeType.Skip
                    eSelectionType = Enums.SelectionType.Marked
                Case "missing_ask"
                    eScrapeType = Enums.ScrapeType.Ask
                    eSelectionType = Enums.SelectionType.Missing
                Case "missing_auto"
                    eScrapeType = Enums.ScrapeType.Auto
                    eSelectionType = Enums.SelectionType.Missing
                Case "missing_skip"
                    eScrapeType = Enums.ScrapeType.Skip
                    eSelectionType = Enums.SelectionType.Missing
                Case "new_ask"
                    eScrapeType = Enums.ScrapeType.Ask
                    eSelectionType = Enums.SelectionType.[New]
                Case "new_auto"
                    eScrapeType = Enums.ScrapeType.Auto
                    eSelectionType = Enums.SelectionType.[New]
                Case "new_skip"
                    eScrapeType = Enums.ScrapeType.Skip
                    eSelectionType = Enums.SelectionType.[New]
                Case "selected_ask"
                    eScrapeType = Enums.ScrapeType.Ask
                    eSelectionType = Enums.SelectionType.Selected
                Case "selected_auto"
                    eScrapeType = Enums.ScrapeType.Auto
                    eSelectionType = Enums.SelectionType.Selected
                Case "selected_skip"
                    eScrapeType = Enums.ScrapeType.Skip
                    eSelectionType = Enums.SelectionType.Selected
            End Select
            TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                                   .ContentType = eContentType,
                                   .ScrapeModifiers = ScrapeModifiers,
                                   .ScrapeOptions = Master.eSettings.DefaultScrapeOptions(eContentType),
                                   .ScrapeType = eScrapeType,
                                   .SelectionType = eSelectionType
                                   })
        Else
            Using dlgCustomScraper As New dlgCustomScraper(eContentType)
                Dim CustomScraper As Structures.CustomUpdaterStruct = Nothing
                CustomScraper = dlgCustomScraper.ShowDialog()
                If Not CustomScraper.Canceled Then
                    TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                                                   .ContentType = eContentType,
                                                   .ScrapeModifiers = CustomScraper.ScrapeModifiers,
                                                   .ScrapeOptions = CustomScraper.ScrapeOptions,
                                                   .ScrapeType = CustomScraper.ScrapeType,
                                                   .SelectionType = CustomScraper.SelectionType
                                                   })
                End If
            End Using
        End If
    End Sub

    Private Sub SingleDataField(sender As Object, e As EventArgs) Handles _
        mnuScrapeOptionActors.Click,
        mnuScrapeOptionAired.Click,
        mnuScrapeOptionCertifications.Click,
        mnuScrapeOptionCollection.Click,
        mnuScrapeOptionCountries.Click,
        mnuScrapeOptionCreators.Click,
        mnuScrapeOptionDirectors.Click,
        mnuScrapeOptionEpiGuideURL.Click,
        mnuScrapeOptionGenres.Click,
        mnuScrapeOptionGuestStars.Click,
        mnuScrapeOptionMPAA.Click,
        mnuScrapeOptionOriginalTitle.Click,
        mnuScrapeOptionOutline.Click,
        mnuScrapeOptionPlot.Click,
        mnuScrapeOptionPremiered.Click,
        mnuScrapeOptionRating.Click,
        mnuScrapeOptionRuntime.Click,
        mnuScrapeOptionStatus.Click,
        mnuScrapeOptionStudios.Click,
        mnuScrapeOptionTagline.Click,
        mnuScrapeOptionTags.Click,
        mnuScrapeOptionTitle.Click,
        mnuScrapeOptionTop250.Click,
        mnuScrapeOptionTrailer.Click,
        mnuScrapeOptionUserRating.Click,
        mnuScrapeOptionWriters.Click

        Dim strContentType As String = String.Empty
        Dim ScrapeOption As String = String.Empty
        Dim ScrapeOptions As New Structures.ScrapeOptions
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Dim Menu As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        ScrapeOption = Menu.Tag.ToString
        strContentType = _SelectedContentType

        Select Case ScrapeOption
            Case "actors"
                ScrapeOptions.Actors = True
                ScrapeOptions.Episodes.Actors = True
            Case "aired"
                ScrapeOptions.Episodes.Aired = True
                ScrapeOptions.Seasons.Aired = True
            Case "certifications"
                ScrapeOptions.Certifications = True
            Case "collection"
                ScrapeOptions.Collection = True
            Case "countries"
                ScrapeOptions.Countries = True
            Case "creators"
                ScrapeOptions.Creators = True
            Case "directors"
                ScrapeOptions.Directors = True
                ScrapeOptions.Episodes.Directors = True
            Case "episodeguideurl"
                ScrapeOptions.EpisodeGuideURL = True
            Case "genres"
                ScrapeOptions.Genres = True
            Case "gueststars"
                ScrapeOptions.Episodes.GuestStars = True
            Case "mpaa"
                ScrapeOptions.MPAA = True
            Case "originaltitle"
                ScrapeOptions.OriginalTitle = True
            Case "outline"
                ScrapeOptions.Outline = True
            Case "plot"
                ScrapeOptions.Plot = True
                ScrapeOptions.Episodes.Plot = True
                ScrapeOptions.Seasons.Plot = True
            Case "premiered"
                ScrapeOptions.Premiered = True
            Case "rating"
                ScrapeOptions.Ratings = True
                ScrapeOptions.Episodes.Ratings = True
            Case "runtime"
                ScrapeOptions.Runtime = True
                ScrapeOptions.Episodes.Runtime = True
            Case "status"
                ScrapeOptions.Status = True
            Case "studios"
                ScrapeOptions.Studios = True
            Case "tagline"
                ScrapeOptions.Tagline = True
            Case "tags"
                ScrapeOptions.Tags = True
            Case "title"
                ScrapeOptions.Title = True
                ScrapeOptions.Episodes.Title = True
                ScrapeOptions.Seasons.Title = True
            Case "top250"
                ScrapeOptions.Top250 = True
            Case "trailer"
                ScrapeOptions.Trailer = True
            Case "userrating"
                ScrapeOptions.UserRating = True
                ScrapeOptions.Episodes.UserRating = True
            Case "writers"
                ScrapeOptions.Credits = True
                ScrapeOptions.Episodes.Credits = True
        End Select

        Dim eContentType As Enums.ContentType = Enums.ContentType.None
        Select Case strContentType
            Case "movie"
                ScrapeModifiers.MainNFO = True
                eContentType = Enums.ContentType.Movie
            Case "movieset"
                ScrapeModifiers.MainNFO = True
                eContentType = Enums.ContentType.Movieset
            Case "tvepisode"
                ScrapeModifiers.EpisodeNFO = True
                eContentType = Enums.ContentType.TVEpisode
            Case "tvseason"
                ScrapeModifiers.SeasonNFO = True
                eContentType = Enums.ContentType.TVSeason
            Case "tvshow"
                ScrapeModifiers.MainNFO = True
                eContentType = Enums.ContentType.TVShow
        End Select
        TaskManager_CreateTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scrape) With {
                               .ContentType = eContentType,
                               .ScrapeModifiers = ScrapeModifiers,
                               .ScrapeOptions = ScrapeOptions,
                               .ScrapeType = Enums.ScrapeType.Ask,
                               .SelectionType = Enums.SelectionType.Selected
                               })
    End Sub
    ''' <summary>
    ''' Slide the genre images along with the panel and move with form resizing
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MoveGenres()
        Try
            For i As Integer = 0 To pnlGenre.Count - 1
                pnlGenre(i).Left = ((pnlInfoPanel.Right) - (i * 73)) - 73
                pnlGenre(i).Top = pnlInfoPanel.Top - 105
            Next
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Slide the MPAA image along with the panel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MoveMPAA()
        pnlMPAA.BringToFront()
        pnlMPAA.Size = New Size(pbMPAA.Width + 10, pbMPAA.Height + 10)
        pbMPAA.Location = New Point(4, 4)
        pnlMPAA.Top = pnlInfoPanel.Top - (pnlMPAA.Height + 10)
    End Sub

    Private Sub CreateScrapeList_Movie(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal ScrapeModifiers As Structures.ScrapeModifiers)
        'Dim DataRowList As New List(Of DataRow)
        'Dim ScrapeList As New List(Of ScrapeItem)

        'Select Case sType
        '    Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip,
        '        Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
        '        'create snapshoot list of selected movies
        '        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
        '            DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
        '        Next
        '    Case Else
        '        For Each sRow As DataRow In _DtMovies.Rows
        '            DataRowList.Add(sRow)
        '        Next
        'End Select

        ''Dim ActorThumbsAllowed As Boolean = Master.eSettings.MovieActorThumbsAnyEnabled
        ''Dim BannerAllowed As Boolean = Master.eSettings.MovieBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
        ''Dim ClearArtAllowed As Boolean = Master.eSettings.MovieClearArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt)
        ''Dim ClearLogoAllowed As Boolean = Master.eSettings.MovieClearLogoAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo)
        ''Dim DiscArtAllowed As Boolean = Master.eSettings.MovieDiscArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt)
        ''Dim ExtrafanartsAllowed As Boolean = Master.eSettings.MovieExtrafanartsAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
        ''Dim ExtrathumbsAllowed As Boolean = Master.eSettings.MovieExtrathumbsAnyEnabled AndAlso (AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart) OrElse Master.eSettings.MovieExtrathumbsVideoExtraction)
        ''Dim FanartAllowed As Boolean = Master.eSettings.MovieFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
        ''Dim KeyArtAllowed As Boolean = Master.eSettings.MovieKeyArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
        ''Dim LandscapeAllowed As Boolean = Master.eSettings.MovieLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape)
        ''Dim PosterAllowed As Boolean = Master.eSettings.MoviePosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
        ''Dim ThemeAllowed As Boolean = Master.eSettings.MovieThemeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie(Enums.ModifierType.MainTheme)
        ''Dim TrailerAllowed As Boolean = Master.eSettings.MovieTrailerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Trailer_Movie(Enums.ModifierType.MainTrailer)

        ''create ScrapeList of movies acording to scrapetype
        'For Each drvRow As DataRow In DataRowList
        '    'If Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Locked))) AndAlso Not sType = Enums.ScrapeType.SingleScrape Then Continue For

        '    ''sModifier.MainSubtitles = ScrapeModifier.MainSubtitles AndAlso SubtitlesAllowed
        '    'Dim sModifier As New Structures.ScrapeModifiers With {
        '    '    .DoSearch = ScrapeModifiers.DoSearch,
        '    '    .MainActorthumbs = ScrapeModifiers.MainActorthumbs AndAlso ActorThumbsAllowed,
        '    '    .MainBanner = ScrapeModifiers.MainBanner AndAlso BannerAllowed,
        '    '    .MainClearArt = ScrapeModifiers.MainClearArt AndAlso ClearArtAllowed,
        '    '    .MainClearLogo = ScrapeModifiers.MainClearLogo AndAlso ClearLogoAllowed,
        '    '    .MainDiscArt = ScrapeModifiers.MainDiscArt AndAlso DiscArtAllowed,
        '    '    .MainExtrafanarts = ScrapeModifiers.MainExtrafanarts AndAlso ExtrafanartsAllowed,
        '    '    .MainExtrathumbs = ScrapeModifiers.MainExtrathumbs AndAlso ExtrathumbsAllowed,
        '    '    .MainFanart = ScrapeModifiers.MainFanart AndAlso FanartAllowed,
        '    '    .MainKeyArt = ScrapeModifiers.MainKeyArt AndAlso KeyArtAllowed,
        '    '    .MainLandscape = ScrapeModifiers.MainLandscape AndAlso LandscapeAllowed,
        '    '    .MainMeta = ScrapeModifiers.MainMeta,
        '    '    .MainNFO = ScrapeModifiers.MainNFO,
        '    '    .MainPoster = ScrapeModifiers.MainPoster AndAlso PosterAllowed,
        '    '    .MainTheme = ScrapeModifiers.MainTheme AndAlso ThemeAllowed,
        '    '    .MainTrailer = ScrapeModifiers.MainTrailer AndAlso TrailerAllowed
        '    '}

        '    Select Case sType
        '        'Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
        '        '    If Not Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.[New]))) Then Continue For
        '        'Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
        '        '    If Not Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Marked))) Then Continue For
        '        Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
        '            Dim index As Integer = _BsMovies.Find(Database.Helpers.GetMainIdName(Database.TableName.movie), drvRow.Item(0))
        '            If Not index >= 0 Then Continue For
        '            'Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
        '            '    If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).ToString) Then sModifier.MainBanner = False
        '            '    If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath)).ToString) Then sModifier.MainClearArt = False
        '            '    If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).ToString) Then sModifier.MainClearLogo = False
        '            '    If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath)).ToString) Then sModifier.MainDiscArt = False
        '            '    If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath)).ToString) Then sModifier.MainExtrafanarts = False
        '            '    If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ExtrathumbsPath)).ToString) Then sModifier.MainExtrathumbs = False
        '            '    If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ToString) Then sModifier.MainFanart = False
        '            '    If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).ToString) Then sModifier.MainKeyArt = False
        '            '    If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).ToString) Then sModifier.MainLandscape = False
        '            '    If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).ToString) Then sModifier.MainNFO = False
        '            '    If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ToString) Then sModifier.MainPoster = False
        '            '    If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath)).ToString) Then sModifier.MainTheme = False
        '            '    If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.TrailerPath)).ToString) Then sModifier.MainTrailer = False
        '    End Select
        '    'If Functions.ScrapeModifiersAnyEnabled(sModifier) Then
        '    'ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifiers = sModifier})
        '    'End If
        'Next

        'If Not ScrapeList.Count = 0 Then
        '    SetControlsEnabled(False)

        '    tspbLoading.Value = 0
        '    If ScrapeList.Count > 1 Then
        '        tspbLoading.Style = ProgressBarStyle.Continuous
        '        tspbLoading.Maximum = ScrapeList.Count
        '    Else
        '        tspbLoading.Maximum = 100
        '        tspbLoading.Style = ProgressBarStyle.Marquee
        '    End If

        '    'Select Case sType
        '    '    Case Enums.ScrapeType.AllAsk
        '    '        tslLoading.Text = Master.eLang.GetString(127, "Scraping Media (All Movies - Ask):")
        '    '    Case Enums.ScrapeType.AllAuto
        '    '        tslLoading.Text = Master.eLang.GetString(128, "Scraping Media (All Movies - Auto):")
        '    '    Case Enums.ScrapeType.AllSkip
        '    '        tslLoading.Text = Master.eLang.GetString(853, "Scraping Media (All Movies - Skip):")
        '    '    Case Enums.ScrapeType.MissingAuto
        '    '        tslLoading.Text = Master.eLang.GetString(132, "Scraping Media (Movies Missing Items - Auto):")
        '    '    Case Enums.ScrapeType.MissingAsk
        '    '        tslLoading.Text = Master.eLang.GetString(133, "Scraping Media (Movies Missing Items - Ask):")
        '    '    Case Enums.ScrapeType.MissingSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1042, "Scraping Media (Movies Missing Items - Skip):")
        '    '    Case Enums.ScrapeType.NewAsk
        '    '        tslLoading.Text = Master.eLang.GetString(134, "Scraping Media (New Movies - Ask):")
        '    '    Case Enums.ScrapeType.NewAuto
        '    '        tslLoading.Text = Master.eLang.GetString(135, "Scraping Media (New Movies - Auto):")
        '    '    Case Enums.ScrapeType.NewSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1043, "Scraping Media (New Movies - Skip):")
        '    '    Case Enums.ScrapeType.MarkedAsk
        '    '        tslLoading.Text = Master.eLang.GetString(136, "Scraping Media (Marked Movies - Ask):")
        '    '    Case Enums.ScrapeType.MarkedAuto
        '    '        tslLoading.Text = Master.eLang.GetString(137, "Scraping Media (Marked Movies - Auto):")
        '    '    Case Enums.ScrapeType.MarkedSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1044, "Scraping Media (Marked Movies - Skip):")
        '    '    Case Enums.ScrapeType.FilterAsk
        '    '        tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
        '    '    Case Enums.ScrapeType.FilterAuto
        '    '        tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
        '    '    Case Enums.ScrapeType.FilterAuto
        '    '        tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
        '    '    Case Enums.ScrapeType.SelectedAsk
        '    '        tslLoading.Text = Master.eLang.GetString(1128, "Scraping Media (Selected Movies - Ask):")
        '    '    Case Enums.ScrapeType.SelectedAuto
        '    '        tslLoading.Text = Master.eLang.GetString(1129, "Scraping Media (Selected Movies - Auto):")
        '    '    Case Enums.ScrapeType.SelectedSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1130, "Scraping Media (Selected Movies - Skip):")
        '    '    Case Enums.ScrapeType.SingleField
        '    '        tslLoading.Text = Master.eLang.GetString(1127, "Scraping Media (Selected Movies - Single Field):")
        '    '    Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
        '    '        tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
        '    'End Select

        '    'TaskManager_CreateTask(Enums.ContentType.Movie, )

        '    If Not sType = Enums.ScrapeType.SingleScrape Then
        '        btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
        '        lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
        '        btnCancel.Visible = True
        '        lblCanceling.Visible = False
        '        prbCanceling.Visible = False
        '        pnlCancel.Visible = True
        '    End If
        'End If
    End Sub

    Private Sub CreateScrapeList_Movieset(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal ScrapeModifiers As Structures.ScrapeModifiers)
        'Dim DataRowList As New List(Of DataRow)
        'Dim ScrapeList As New List(Of ScrapeItem)

        'Select Case sType
        '    Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip,
        '        Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
        '        'create snapshoot list of selected moviesets
        '        For Each sRow As DataGridViewRow In dgvMoviesets.SelectedRows
        '            DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
        '        Next
        '    Case Else
        '        For Each sRow As DataRow In _DtMovieSets.Rows
        '            DataRowList.Add(sRow)
        '        Next
        'End Select

        'Dim BannerAllowed As Boolean = Master.eSettings.MovieSetBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainBanner)
        'Dim ClearArtAllowed As Boolean = Master.eSettings.MovieSetClearArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearArt)
        'Dim ClearLogoAllowed As Boolean = Master.eSettings.MovieSetClearLogoAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearLogo)
        'Dim DiscArtAllowed As Boolean = Master.eSettings.MovieSetDiscArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainDiscArt)
        'Dim FanartAllowed As Boolean = Master.eSettings.MovieSetFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainFanart)
        'Dim KeyArtAllowed As Boolean = Master.eSettings.MovieSetKeyArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainPoster)
        'Dim LandscapeAllowed As Boolean = Master.eSettings.MovieSetLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainLandscape)
        'Dim PosterAllowed As Boolean = Master.eSettings.MovieSetPosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainPoster)

        ''create ScrapeList of moviesets acording to scrapetype
        'For Each drvRow As DataRow In DataRowList
        '    If Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Locked))) AndAlso Not sType = Enums.ScrapeType.SingleScrape Then Continue For

        '    Dim sModifier As New Structures.ScrapeModifiers With {
        '        .DoSearch = ScrapeModifiers.DoSearch,
        '        .MainBanner = ScrapeModifiers.MainBanner AndAlso BannerAllowed,
        '        .MainClearArt = ScrapeModifiers.MainClearArt AndAlso ClearArtAllowed,
        '        .MainClearLogo = ScrapeModifiers.MainClearLogo AndAlso ClearLogoAllowed,
        '        .MainDiscArt = ScrapeModifiers.MainDiscArt AndAlso DiscArtAllowed,
        '        .MainFanart = ScrapeModifiers.MainFanart AndAlso FanartAllowed,
        '        .MainKeyArt = ScrapeModifiers.MainKeyArt AndAlso KeyArtAllowed,
        '        .MainLandscape = ScrapeModifiers.MainLandscape AndAlso LandscapeAllowed,
        '        .MainNFO = ScrapeModifiers.MainNFO,
        '        .MainPoster = ScrapeModifiers.MainPoster AndAlso PosterAllowed
        '    }

        '    Select Case sType
        '        Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
        '            If Not Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.[New]))) Then Continue For
        '        Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
        '            If Not Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Marked))) Then Continue For
        '        Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
        '            Dim index As Integer = _BsMoviesets.Find(Database.Helpers.GetMainIdName(Database.TableName.movieset), drvRow.Item(0))
        '            If Not index >= 0 Then Continue For
        '        Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).ToString) Then sModifier.MainBanner = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath)).ToString) Then sModifier.MainClearArt = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).ToString) Then sModifier.MainClearLogo = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath)).ToString) Then sModifier.MainDiscArt = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ToString) Then sModifier.MainFanart = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).ToString) Then sModifier.MainKeyArt = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).ToString) Then sModifier.MainLandscape = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).ToString) Then sModifier.MainNFO = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ToString) Then sModifier.MainPoster = False
        '    End Select
        '    If Functions.ScrapeModifiersAnyEnabled(sModifier) Then
        '        ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifiers = sModifier})
        '    End If
        'Next

        'If Not ScrapeList.Count = 0 Then
        '    SetControlsEnabled(False)

        '    tspbLoading.Value = 0
        '    If ScrapeList.Count > 1 Then
        '        tspbLoading.Style = ProgressBarStyle.Continuous
        '        tspbLoading.Maximum = ScrapeList.Count
        '    Else
        '        tspbLoading.Maximum = 100
        '        tspbLoading.Style = ProgressBarStyle.Marquee
        '    End If

        '    'Select Case sType
        '    '    Case Enums.ScrapeType.AllAsk
        '    '        tslLoading.Text = Master.eLang.GetString(1215, "Scraping Media (All MovieSets - Ask):")
        '    '    Case Enums.ScrapeType.AllAuto
        '    '        tslLoading.Text = Master.eLang.GetString(1216, "Scraping Media (All MovieSets - Auto):")
        '    '    Case Enums.ScrapeType.AllSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1217, "Scraping Media (All MovieSets - Skip):")
        '    '    Case Enums.ScrapeType.MissingAuto
        '    '        tslLoading.Text = Master.eLang.GetString(1218, "Scraping Media (MovieSets Missing Items - Auto):")
        '    '    Case Enums.ScrapeType.MissingAsk
        '    '        tslLoading.Text = Master.eLang.GetString(1219, "Scraping Media (MovieSets Missing Items - Ask):")
        '    '    Case Enums.ScrapeType.MissingSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1220, "Scraping Media (MovieSets Missing Items - Skip):")
        '    '    Case Enums.ScrapeType.NewAsk
        '    '        tslLoading.Text = Master.eLang.GetString(1221, "Scraping Media (New MovieSets - Ask):")
        '    '    Case Enums.ScrapeType.NewAuto
        '    '        tslLoading.Text = Master.eLang.GetString(1222, "Scraping Media (New MovieSets - Auto):")
        '    '    Case Enums.ScrapeType.NewSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1223, "Scraping Media (New MovieSets - Skip):")
        '    '    Case Enums.ScrapeType.MarkedAsk
        '    '        tslLoading.Text = Master.eLang.GetString(1224, "Scraping Media (Marked MovieSets - Ask):")
        '    '    Case Enums.ScrapeType.MarkedAuto
        '    '        tslLoading.Text = Master.eLang.GetString(1225, "Scraping Media (Marked MovieSets - Auto):")
        '    '    Case Enums.ScrapeType.MarkedSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1226, "Scraping Media (Marked MovieSets - Skip):")
        '    '    Case Enums.ScrapeType.FilterAsk
        '    '        tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
        '    '    Case Enums.ScrapeType.FilterAuto
        '    '        tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
        '    '    Case Enums.ScrapeType.FilterAuto
        '    '        tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
        '    '    Case Enums.ScrapeType.AllAsk
        '    '        tslLoading.Text = Master.eLang.GetString(1358, "Scraping Media (Selected MovieSets - Ask):")
        '    '    Case Enums.ScrapeType.AllAuto
        '    '        tslLoading.Text = Master.eLang.GetString(1359, "Scraping Media (Selected MovieSets - Auto):")
        '    '    Case Enums.ScrapeType.AllSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1360, "Scraping Media (Selected MovieSets - Skip):")
        '    '    Case Enums.ScrapeType.SingleField
        '    '        tslLoading.Text = Master.eLang.GetString(1357, "Scraping Media (Selected MovieSets - Single Field):")
        '    '    Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
        '    '        tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
        '    'End Select


        '    If Not sType = Enums.ScrapeType.SingleScrape Then
        '        btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
        '        lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
        '        btnCancel.Visible = True
        '        lblCanceling.Visible = False
        '        prbCanceling.Visible = False
        '        pnlCancel.Visible = True
        '    End If

        '    tslLoading.Visible = True
        '    tspbLoading.Visible = True
        '    Application.DoEvents()
        '    bwMoviesetScraper.WorkerSupportsCancellation = True
        '    bwMoviesetScraper.WorkerReportsProgress = True
        '    bwMoviesetScraper.RunWorkerAsync(New Arguments With {.ScrapeOptions = ScrapeOptions, .ScrapeList = ScrapeList, .ScrapeType = sType})
        'End If
    End Sub

    Private Sub CreateScrapeList_TV(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal ScrapeModifiers As Structures.ScrapeModifiers)
        'Dim DataRowList As New List(Of DataRow)
        'Dim ScrapeList As New List(Of ScrapeItem)

        'Select Case sType
        '    Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip,
        '        Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
        '        'create snapshoot list of selected tv show
        '        For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
        '            DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
        '        Next
        '    Case Else
        '        For Each sRow As DataRow In _DtTVShows.Rows
        '            DataRowList.Add(sRow)
        '        Next
        'End Select

        'Dim AllSeasonsBannerAllowed As Boolean = Master.eSettings.TVAllSeasonsBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsBanner)
        'Dim AllSeasonsFanartAllowed As Boolean = Master.eSettings.TVAllSeasonsFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsFanart)
        'Dim AllSeasonsLandscapeAllowed As Boolean = Master.eSettings.TVAllSeasonsLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsLandscape)
        'Dim AllSeasonsPosterAllowed As Boolean = Master.eSettings.TVAllSeasonsPosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsPoster)
        'Dim EpisodeActorThumbsAllowed As Boolean = Master.eSettings.TVEpisodeActorThumbsAnyEnabled
        'Dim EpisodeFanartAllowed As Boolean = Master.eSettings.TVEpisodeFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart)
        'Dim EpisodeMetaAllowed As Boolean = Master.eSettings.TVScraperMetaDataScan
        'Dim EpisodePosterAllowed As Boolean = Master.eSettings.TVEpisodePosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster)
        'Dim MainActorThumbsAllowed As Boolean = Master.eSettings.TVShowActorThumbsAnyEnabled
        'Dim MainBannerAllowed As Boolean = Master.eSettings.TVShowBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)
        'Dim MainCharacterArtAllowed As Boolean = Master.eSettings.TVShowCharacterArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
        'Dim MainClearArtAllowed As Boolean = Master.eSettings.TVShowClearArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
        'Dim MainClearLogoAllowed As Boolean = Master.eSettings.TVShowClearLogoAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
        'Dim MainExtrafanartsAllowed As Boolean = Master.eSettings.TVShowExtrafanartsAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
        'Dim MainFanartAllowed As Boolean = Master.eSettings.TVShowFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
        'Dim MainKeyArtAllowed As Boolean = Master.eSettings.TVShowKeyArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
        'Dim MainLandscapeAllowed As Boolean = Master.eSettings.TVShowLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
        'Dim MainPosterAllowed As Boolean = Master.eSettings.TVShowPosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
        'Dim MainThemeAllowed As Boolean = Master.eSettings.TvShowThemeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_TV(Enums.ModifierType.MainTheme)
        'Dim SeasonBannerAllowed As Boolean = Master.eSettings.TVSeasonBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)
        'Dim SeasonFanartAllowed As Boolean = Master.eSettings.TVSeasonFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart)
        'Dim SeasonLandscapeAllowed As Boolean = Master.eSettings.TVSeasonLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)
        'Dim SeasonPosterAllowed As Boolean = Master.eSettings.TVSeasonPosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)

        ''create ScrapeList of tv shows acording to scrapetype
        'For Each drvRow As DataRow In DataRowList
        '    If Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Locked))) AndAlso Not sType = Enums.ScrapeType.SingleScrape Then Continue For

        '    Dim sModifier As New Structures.ScrapeModifiers With {
        '        .DoSearch = ScrapeModifiers.DoSearch,
        '        .AllSeasonsBanner = ScrapeModifiers.AllSeasonsBanner AndAlso AllSeasonsBannerAllowed,
        '        .AllSeasonsFanart = ScrapeModifiers.AllSeasonsFanart AndAlso AllSeasonsFanartAllowed,
        '        .AllSeasonsLandscape = ScrapeModifiers.AllSeasonsLandscape AndAlso AllSeasonsLandscapeAllowed,
        '        .AllSeasonsPoster = ScrapeModifiers.AllSeasonsPoster AndAlso AllSeasonsPosterAllowed,
        '        .EpisodeActorThumbs = ScrapeModifiers.EpisodeActorThumbs AndAlso EpisodeActorThumbsAllowed,
        '        .EpisodeFanart = ScrapeModifiers.EpisodeFanart AndAlso EpisodeFanartAllowed,
        '        .EpisodeMeta = ScrapeModifiers.EpisodeMeta AndAlso EpisodeMetaAllowed,
        '        .EpisodeNFO = ScrapeModifiers.EpisodeNFO,
        '        .EpisodePoster = ScrapeModifiers.EpisodePoster AndAlso EpisodePosterAllowed,
        '        .MainActorthumbs = ScrapeModifiers.MainActorthumbs AndAlso MainActorThumbsAllowed,
        '        .MainBanner = ScrapeModifiers.MainBanner AndAlso MainBannerAllowed,
        '        .MainCharacterArt = ScrapeModifiers.MainCharacterArt AndAlso MainCharacterArtAllowed,
        '        .MainClearArt = ScrapeModifiers.MainClearArt AndAlso MainClearArtAllowed,
        '        .MainClearLogo = ScrapeModifiers.MainClearLogo AndAlso MainClearLogoAllowed,
        '        .MainExtrafanarts = ScrapeModifiers.MainExtrafanarts AndAlso MainExtrafanartsAllowed,
        '        .MainFanart = ScrapeModifiers.MainFanart AndAlso MainFanartAllowed,
        '        .MainKeyArt = ScrapeModifiers.MainKeyArt AndAlso MainKeyArtAllowed,
        '        .MainLandscape = ScrapeModifiers.MainLandscape AndAlso MainLandscapeAllowed,
        '        .MainNFO = ScrapeModifiers.MainNFO,
        '        .MainPoster = ScrapeModifiers.MainPoster AndAlso MainPosterAllowed,
        '        .MainTheme = ScrapeModifiers.MainTheme AndAlso MainThemeAllowed,
        '        .SeasonBanner = ScrapeModifiers.SeasonBanner AndAlso SeasonBannerAllowed,
        '        .SeasonFanart = ScrapeModifiers.SeasonFanart AndAlso SeasonFanartAllowed,
        '        .SeasonLandscape = ScrapeModifiers.SeasonLandscape AndAlso SeasonLandscapeAllowed,
        '        .SeasonPoster = ScrapeModifiers.SeasonPoster AndAlso SeasonPosterAllowed,
        '        .withEpisodes = ScrapeModifiers.withEpisodes,
        '        .withSeasons = ScrapeModifiers.withSeasons
        '    }

        '    Select Case sType
        '        Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
        '            If Not Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.New))) Then Continue For
        '        Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
        '            If Not Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Marked))) Then Continue For
        '        Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
        '            Dim index As Integer = _BsTVShows.Find(Database.Helpers.GetMainIdName(Database.TableName.tvshow), drvRow.Item(0))
        '            If Not index >= 0 Then Continue For
        '        Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).ToString) Then sModifier.MainBanner = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.CharacterArtPath)).ToString) Then sModifier.MainCharacterArt = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath)).ToString) Then sModifier.MainClearArt = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath)).ToString) Then sModifier.MainClearLogo = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath)).ToString) Then sModifier.MainExtrafanarts = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ToString) Then sModifier.MainFanart = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath)).ToString) Then sModifier.MainKeyArt = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).ToString) Then sModifier.MainLandscape = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).ToString) Then sModifier.MainNFO = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ToString) Then sModifier.MainPoster = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.ThemePath)).ToString) Then sModifier.MainTheme = False
        '    End Select
        '    If Functions.ScrapeModifiersAnyEnabled(sModifier) Then
        '        ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifiers = sModifier})
        '    End If
        'Next

        'If Not ScrapeList.Count = 0 Then
        '    SetControlsEnabled(False)

        '    tspbLoading.Value = 0
        '    If ScrapeList.Count > 1 Then
        '        tspbLoading.Style = ProgressBarStyle.Continuous
        '        tspbLoading.Maximum = ScrapeList.Count
        '    Else
        '        tspbLoading.Maximum = 100
        '        tspbLoading.Style = ProgressBarStyle.Marquee
        '    End If

        '    'Select Case sType
        '    '    Case Enums.ScrapeType.AllAsk
        '    '        tslLoading.Text = Master.eLang.GetString(127, "Scraping Media (All Movies - Ask):")
        '    '    Case Enums.ScrapeType.AllAuto
        '    '        tslLoading.Text = Master.eLang.GetString(128, "Scraping Media (All Movies - Auto):")
        '    '    Case Enums.ScrapeType.AllSkip
        '    '        tslLoading.Text = Master.eLang.GetString(853, "Scraping Media (All Movies - Skip):")
        '    '    Case Enums.ScrapeType.MissingAuto
        '    '        tslLoading.Text = Master.eLang.GetString(132, "Scraping Media (Movies Missing Items - Auto):")
        '    '    Case Enums.ScrapeType.MissingAsk
        '    '        tslLoading.Text = Master.eLang.GetString(133, "Scraping Media (Movies Missing Items - Ask):")
        '    '    Case Enums.ScrapeType.MissingSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1042, "Scraping Media (Movies Missing Items - Skip):")
        '    '    Case Enums.ScrapeType.NewAsk
        '    '        tslLoading.Text = Master.eLang.GetString(134, "Scraping Media (New Movies - Ask):")
        '    '    Case Enums.ScrapeType.NewAuto
        '    '        tslLoading.Text = Master.eLang.GetString(135, "Scraping Media (New Movies - Auto):")
        '    '    Case Enums.ScrapeType.NewSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1043, "Scraping Media (New Movies - Skip):")
        '    '    Case Enums.ScrapeType.MarkedAsk
        '    '        tslLoading.Text = Master.eLang.GetString(136, "Scraping Media (Marked Movies - Ask):")
        '    '    Case Enums.ScrapeType.MarkedAuto
        '    '        tslLoading.Text = Master.eLang.GetString(137, "Scraping Media (Marked Movies - Auto):")
        '    '    Case Enums.ScrapeType.MarkedSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1044, "Scraping Media (Marked Movies - Skip):")
        '    '    Case Enums.ScrapeType.FilterAsk
        '    '        tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
        '    '    Case Enums.ScrapeType.FilterAuto
        '    '        tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
        '    '    Case Enums.ScrapeType.FilterAuto
        '    '        tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
        '    '    Case Enums.ScrapeType.SelectedAsk
        '    '        tslLoading.Text = Master.eLang.GetString(1128, "Scraping Media (Selected Movies - Ask):")
        '    '    Case Enums.ScrapeType.SelectedAuto
        '    '        tslLoading.Text = Master.eLang.GetString(1129, "Scraping Media (Selected Movies - Auto):")
        '    '    Case Enums.ScrapeType.SelectedSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1130, "Scraping Media (Selected Movies - Skip):")
        '    '    Case Enums.ScrapeType.SingleField
        '    '        tslLoading.Text = Master.eLang.GetString(1127, "Scraping Media (Selected Movies - Single Field):")
        '    '    Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
        '    '        tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
        '    'End Select

        '    If Not sType = Enums.ScrapeType.SingleScrape Then
        '        btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
        '        lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
        '        btnCancel.Visible = True
        '        lblCanceling.Visible = False
        '        prbCanceling.Visible = False
        '        pnlCancel.Visible = True
        '    End If

        '    tslLoading.Visible = True
        '    tspbLoading.Visible = True
        '    Application.DoEvents()
        '    bwTVScraper.WorkerSupportsCancellation = True
        '    bwTVScraper.WorkerReportsProgress = True
        '    bwTVScraper.RunWorkerAsync(New Arguments With {.ScrapeOptions = ScrapeOptions, .ScrapeList = ScrapeList, .ScrapeType = sType})
        'End If
    End Sub

    Private Sub CreateScrapeList_TVEpisode(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal ScrapeModifiers As Structures.ScrapeModifiers)
        'Dim DataRowList As New List(Of DataRow)
        'Dim ScrapeList As New List(Of ScrapeItem)

        'Select Case sType
        '    Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip,
        '        Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
        '        'create snapshoot list of selected tv episode
        '        For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
        '            DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
        '        Next
        '    Case Else
        '        For Each sRow As DataRow In _DtTVEpisodes.Rows
        '            DataRowList.Add(sRow)
        '        Next
        'End Select

        'Dim ActorThumbsAllowed As Boolean = Master.eSettings.TVEpisodeActorThumbsAnyEnabled
        'Dim FanartAllowed As Boolean = Master.eSettings.TVEpisodeFanartAnyEnabled AndAlso (AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart) OrElse
        '    AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart))
        'Dim PosterAllowed As Boolean = Master.eSettings.TVEpisodePosterAnyEnabled AndAlso (AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster) OrElse
        '    Master.eSettings.TVEpisodePosterVideoExtraction)

        ''create ScrapeList of episodes acording to scrapetype
        'For Each drvRow As DataRow In DataRowList
        '    If Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Locked))) AndAlso Not sType = Enums.ScrapeType.SingleScrape Then Continue For

        '    Dim sModifier As New Structures.ScrapeModifiers
        '    sModifier.DoSearch = ScrapeModifiers.DoSearch
        '    sModifier.EpisodeActorThumbs = ScrapeModifiers.EpisodeActorThumbs AndAlso ActorThumbsAllowed
        '    sModifier.EpisodeFanart = ScrapeModifiers.EpisodeFanart AndAlso FanartAllowed
        '    sModifier.EpisodeMeta = ScrapeModifiers.EpisodeMeta
        '    sModifier.EpisodeNFO = ScrapeModifiers.EpisodeNFO
        '    sModifier.EpisodePoster = ScrapeModifiers.EpisodePoster AndAlso PosterAllowed

        '    Select Case sType
        '        Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
        '            If Not Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.[New]))) Then Continue For
        '        Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
        '            If Not Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Marked))) Then Continue For
        '        Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
        '            Dim index As Integer = _BsTVEpisodes.Find(Database.Helpers.GetMainIdName(Database.TableName.episode), drvRow.Item(0))
        '            If Not index >= 0 Then Continue For
        '        Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ToString) Then sModifier.EpisodeFanart = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.NfoPath)).ToString) Then sModifier.EpisodeNFO = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ToString) Then sModifier.EpisodePoster = False
        '    End Select
        '    If Functions.ScrapeModifiersAnyEnabled(sModifier) Then
        '        ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifiers = sModifier})
        '    End If
        'Next

        'If Not ScrapeList.Count = 0 Then
        '    SetControlsEnabled(False)

        '    tspbLoading.Value = 0
        '    If ScrapeList.Count > 1 Then
        '        tspbLoading.Style = ProgressBarStyle.Continuous
        '        tspbLoading.Maximum = ScrapeList.Count
        '    Else
        '        tspbLoading.Maximum = 100
        '        tspbLoading.Style = ProgressBarStyle.Marquee
        '    End If

        '    'Select Case sType
        '    '    Case Enums.ScrapeType.AllAsk
        '    '        tslLoading.Text = Master.eLang.GetString(127, "Scraping Media (All Movies - Ask):")
        '    '    Case Enums.ScrapeType.AllAuto
        '    '        tslLoading.Text = Master.eLang.GetString(128, "Scraping Media (All Movies - Auto):")
        '    '    Case Enums.ScrapeType.AllSkip
        '    '        tslLoading.Text = Master.eLang.GetString(853, "Scraping Media (All Movies - Skip):")
        '    '    Case Enums.ScrapeType.MissingAuto
        '    '        tslLoading.Text = Master.eLang.GetString(132, "Scraping Media (Movies Missing Items - Auto):")
        '    '    Case Enums.ScrapeType.MissingAsk
        '    '        tslLoading.Text = Master.eLang.GetString(133, "Scraping Media (Movies Missing Items - Ask):")
        '    '    Case Enums.ScrapeType.MissingSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1042, "Scraping Media (Movies Missing Items - Skip):")
        '    '    Case Enums.ScrapeType.NewAsk
        '    '        tslLoading.Text = Master.eLang.GetString(134, "Scraping Media (New Movies - Ask):")
        '    '    Case Enums.ScrapeType.NewAuto
        '    '        tslLoading.Text = Master.eLang.GetString(135, "Scraping Media (New Movies - Auto):")
        '    '    Case Enums.ScrapeType.NewSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1043, "Scraping Media (New Movies - Skip):")
        '    '    Case Enums.ScrapeType.MarkedAsk
        '    '        tslLoading.Text = Master.eLang.GetString(136, "Scraping Media (Marked Movies - Ask):")
        '    '    Case Enums.ScrapeType.MarkedAuto
        '    '        tslLoading.Text = Master.eLang.GetString(137, "Scraping Media (Marked Movies - Auto):")
        '    '    Case Enums.ScrapeType.MarkedSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1044, "Scraping Media (Marked Movies - Skip):")
        '    '    Case Enums.ScrapeType.FilterAsk
        '    '        tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
        '    '    Case Enums.ScrapeType.FilterAuto
        '    '        tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
        '    '    Case Enums.ScrapeType.FilterAuto
        '    '        tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
        '    '    Case Enums.ScrapeType.AllAsk
        '    '        tslLoading.Text = Master.eLang.GetString(1128, "Scraping Media (Selected Movies - Ask):")
        '    '    Case Enums.ScrapeType.AllAuto
        '    '        tslLoading.Text = Master.eLang.GetString(1129, "Scraping Media (Selected Movies - Auto):")
        '    '    Case Enums.ScrapeType.AllSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1130, "Scraping Media (Selected Movies - Skip):")
        '    '    Case Enums.ScrapeType.SingleField
        '    '        tslLoading.Text = Master.eLang.GetString(1127, "Scraping Media (Selected Movies - Single Field):")
        '    '    Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
        '    '        tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
        '    'End Select

        '    If Not sType = Enums.ScrapeType.SingleScrape Then
        '        btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
        '        lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
        '        btnCancel.Visible = True
        '        lblCanceling.Visible = False
        '        prbCanceling.Visible = False
        '        pnlCancel.Visible = True
        '    End If

        '    tslLoading.Visible = True
        '    tspbLoading.Visible = True
        '    Application.DoEvents()
        '    bwTVEpisodeScraper.WorkerSupportsCancellation = True
        '    bwTVEpisodeScraper.WorkerReportsProgress = True
        '    bwTVEpisodeScraper.RunWorkerAsync(New Arguments With {.ScrapeOptions = ScrapeOptions, .ScrapeList = ScrapeList, .ScrapeType = sType})
        'End If
    End Sub

    Private Sub CreateScrapeList_TVSeason(ByVal sType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal ScrapeModifiers As Structures.ScrapeModifiers)
        'Dim DataRowList As New List(Of DataRow)
        'Dim ScrapeList As New List(Of ScrapeItem)

        'Select Case sType
        '    Case Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SelectedSkip,
        '        Enums.ScrapeType.SingleAuto, Enums.ScrapeType.SingleField, Enums.ScrapeType.SingleScrape
        '        'create snapshoot list of selected tv season
        '        For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
        '            DataRowList.Add(DirectCast(sRow.DataBoundItem, DataRowView).Row)
        '        Next
        '    Case Else
        '        For Each sRow As DataRow In _DtTVSeasons.Rows
        '            DataRowList.Add(sRow)
        '        Next
        'End Select

        'Dim AllSeasonsBannerAllowed As Boolean = Master.eSettings.TVAllSeasonsBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsBanner)
        'Dim AllSeasonsFanartAllowed As Boolean = Master.eSettings.TVAllSeasonsFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsFanart)
        'Dim AllSeasonsLandscapeAllowed As Boolean = Master.eSettings.TVAllSeasonsLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsLandscape)
        'Dim AllSeasonsPosterAllowed As Boolean = Master.eSettings.TVAllSeasonsPosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.AllSeasonsPoster)
        'Dim SeasonBannerAllowed As Boolean = Master.eSettings.TVSeasonBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)
        'Dim SeasonFanartAllowed As Boolean = Master.eSettings.TVSeasonFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart)
        'Dim SeasonLandscapeAllowed As Boolean = Master.eSettings.TVSeasonLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)
        'Dim SeasonPosterAllowed As Boolean = Master.eSettings.TVSeasonPosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)

        ''create ScrapeList of tv seasons acording to scrapetype
        'For Each drvRow As DataRow In DataRowList
        '    If Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Locked))) AndAlso Not sType = Enums.ScrapeType.SingleScrape Then Continue For

        '    Dim sModifier As New Structures.ScrapeModifiers
        '    Dim iSeason As Integer = CInt(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.SeasonNumber)))
        '    sModifier.DoSearch = ScrapeModifiers.DoSearch
        '    sModifier.AllSeasonsBanner = ScrapeModifiers.AllSeasonsBanner AndAlso AllSeasonsBannerAllowed AndAlso iSeason = -1
        '    sModifier.AllSeasonsFanart = ScrapeModifiers.AllSeasonsFanart AndAlso AllSeasonsFanartAllowed AndAlso iSeason = -1
        '    sModifier.AllSeasonsLandscape = ScrapeModifiers.AllSeasonsLandscape AndAlso AllSeasonsLandscapeAllowed AndAlso iSeason = -1
        '    sModifier.AllSeasonsPoster = ScrapeModifiers.AllSeasonsPoster AndAlso AllSeasonsPosterAllowed AndAlso iSeason = -1
        '    sModifier.SeasonBanner = ScrapeModifiers.SeasonBanner AndAlso SeasonBannerAllowed AndAlso Not iSeason = -1
        '    sModifier.SeasonFanart = ScrapeModifiers.SeasonFanart AndAlso SeasonFanartAllowed AndAlso Not iSeason = -1
        '    sModifier.SeasonLandscape = ScrapeModifiers.SeasonLandscape AndAlso SeasonLandscapeAllowed AndAlso Not iSeason = -1
        '    sModifier.SeasonNFO = ScrapeModifiers.SeasonNFO
        '    sModifier.SeasonPoster = ScrapeModifiers.SeasonPoster AndAlso SeasonPosterAllowed AndAlso Not iSeason = -1

        '    Select Case sType
        '        Case Enums.ScrapeType.NewAsk, Enums.ScrapeType.NewAuto, Enums.ScrapeType.NewSkip
        '            If Not Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.[New]))) Then Continue For
        '        Case Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MarkedSkip
        '            If Not Convert.ToBoolean(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Marked))) Then Continue For
        '        Case Enums.ScrapeType.FilterAsk, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FilterSkip
        '            Dim index As Integer = _BsTVShows.Find(Database.Helpers.GetMainIdName(Database.TableName.tvshow), drvRow.Item(0))
        '            If Not index >= 0 Then Continue For
        '        Case Enums.ScrapeType.MissingAsk, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.MissingSkip
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.BannerPath)).ToString) Then sModifier.SeasonBanner = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.FanartPath)).ToString) Then sModifier.SeasonFanart = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath)).ToString) Then sModifier.SeasonLandscape = False
        '            If Not String.IsNullOrEmpty(drvRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.PosterPath)).ToString) Then sModifier.SeasonPoster = False
        '    End Select
        '    If Functions.ScrapeModifiersAnyEnabled(sModifier) Then
        '        ScrapeList.Add(New ScrapeItem With {.DataRow = drvRow, .ScrapeModifiers = sModifier})
        '    End If
        'Next

        'If Not ScrapeList.Count = 0 Then
        '    SetControlsEnabled(False)

        '    tspbLoading.Value = 0
        '    If ScrapeList.Count > 1 Then
        '        tspbLoading.Style = ProgressBarStyle.Continuous
        '        tspbLoading.Maximum = ScrapeList.Count
        '    Else
        '        tspbLoading.Maximum = 100
        '        tspbLoading.Style = ProgressBarStyle.Marquee
        '    End If

        '    'Select Case sType
        '    '    Case Enums.ScrapeType.AllAsk
        '    '        tslLoading.Text = Master.eLang.GetString(127, "Scraping Media (All Movies - Ask):")
        '    '    Case Enums.ScrapeType.AllAuto
        '    '        tslLoading.Text = Master.eLang.GetString(128, "Scraping Media (All Movies - Auto):")
        '    '    Case Enums.ScrapeType.AllSkip
        '    '        tslLoading.Text = Master.eLang.GetString(853, "Scraping Media (All Movies - Skip):")
        '    '    Case Enums.ScrapeType.MissingAuto
        '    '        tslLoading.Text = Master.eLang.GetString(132, "Scraping Media (Movies Missing Items - Auto):")
        '    '    Case Enums.ScrapeType.MissingAsk
        '    '        tslLoading.Text = Master.eLang.GetString(133, "Scraping Media (Movies Missing Items - Ask):")
        '    '    Case Enums.ScrapeType.MissingSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1042, "Scraping Media (Movies Missing Items - Skip):")
        '    '    Case Enums.ScrapeType.NewAsk
        '    '        tslLoading.Text = Master.eLang.GetString(134, "Scraping Media (New Movies - Ask):")
        '    '    Case Enums.ScrapeType.NewAuto
        '    '        tslLoading.Text = Master.eLang.GetString(135, "Scraping Media (New Movies - Auto):")
        '    '    Case Enums.ScrapeType.NewSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1043, "Scraping Media (New Movies - Skip):")
        '    '    Case Enums.ScrapeType.MarkedAsk
        '    '        tslLoading.Text = Master.eLang.GetString(136, "Scraping Media (Marked Movies - Ask):")
        '    '    Case Enums.ScrapeType.MarkedAuto
        '    '        tslLoading.Text = Master.eLang.GetString(137, "Scraping Media (Marked Movies - Auto):")
        '    '    Case Enums.ScrapeType.MarkedSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1044, "Scraping Media (Marked Movies - Skip):")
        '    '    Case Enums.ScrapeType.FilterAsk
        '    '        tslLoading.Text = Master.eLang.GetString(622, "Scraping Media (Current Filter - Ask):")
        '    '    Case Enums.ScrapeType.FilterAuto
        '    '        tslLoading.Text = Master.eLang.GetString(623, "Scraping Media (Current Filter - Auto):")
        '    '    Case Enums.ScrapeType.FilterAuto
        '    '        tslLoading.Text = Master.eLang.GetString(1045, "Scraping Media (Current Filter - Skip):")
        '    '    Case Enums.ScrapeType.SelectedAsk
        '    '        tslLoading.Text = Master.eLang.GetString(1128, "Scraping Media (Selected Movies - Ask):")
        '    '    Case Enums.ScrapeType.SelectedAuto
        '    '        tslLoading.Text = Master.eLang.GetString(1129, "Scraping Media (Selected Movies - Auto):")
        '    '    Case Enums.ScrapeType.SelectedSkip
        '    '        tslLoading.Text = Master.eLang.GetString(1130, "Scraping Media (Selected Movies - Skip):")
        '    '    Case Enums.ScrapeType.SingleField
        '    '        tslLoading.Text = Master.eLang.GetString(1127, "Scraping Media (Selected Movies - Single Field):")
        '    '    Case Enums.ScrapeType.SingleScrape, Enums.ScrapeType.SingleAuto
        '    '        tslLoading.Text = Master.eLang.GetString(139, "Scraping:")
        '    'End Select

        '    If Not sType = Enums.ScrapeType.SingleScrape Then
        '        btnCancel.Text = Master.eLang.GetString(54, "Cancel Scraper")
        '        lblCanceling.Text = Master.eLang.GetString(53, "Canceling Scraper...")
        '        btnCancel.Visible = True
        '        lblCanceling.Visible = False
        '        prbCanceling.Visible = False
        '        pnlCancel.Visible = True
        '    End If

        '    tslLoading.Visible = True
        '    tspbLoading.Visible = True
        '    Application.DoEvents()
        '    bwTVSeasonScraper.WorkerSupportsCancellation = True
        '    bwTVSeasonScraper.WorkerReportsProgress = True
        '    bwTVSeasonScraper.RunWorkerAsync(New Arguments With {.ScrapeOptions = ScrapeOptions, .ScrapeList = ScrapeList, .ScrapeType = sType})
        'End If
    End Sub

    Private Sub InfoDownloaded_Movie(ByRef DBMovie As Database.DBElement)
        If Not String.IsNullOrEmpty(DBMovie.MainDetails.Title) Then
            tslLoading.Text = Master.eLang.GetString(576, "Verifying Movie Details:")
            Application.DoEvents()

            Edit_Movie(DBMovie, Enums.AddonEventType.ScraperSingle_Movie)
        End If

        pnlCancel.Visible = False
        tslLoading.Visible = False
        tspbLoading.Visible = False
        SetStatus(String.Empty)
        SetControlsEnabled(True)
        FilterPanel_IsEnabled_Movie(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_Movie(ByVal iPercent As Integer)
        If ReportDownloadPercent = True Then
            tspbLoading.Value = iPercent
            Refresh()
        End If
    End Sub

    Private Sub InfoDownloaded_MovieSet(ByRef DBMovieSet As Database.DBElement)
        If Not String.IsNullOrEmpty(DBMovieSet.MainDetails.Title) Then
            tslLoading.Text = Master.eLang.GetString(1205, "Verifying MovieSet Details:")
            Application.DoEvents()

            Edit_Movieset(DBMovieSet)
        End If

        pnlCancel.Visible = False
        tslLoading.Visible = False
        tspbLoading.Visible = False
        SetStatus(String.Empty)
        SetControlsEnabled(True)
        FilterPanel_IsEnabled_Movieset(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_MovieSet(ByVal iPercent As Integer)
        If ReportDownloadPercent = True Then
            tspbLoading.Value = iPercent
            Refresh()
        End If
    End Sub

    Private Sub InfoDownloaded_TV(ByRef DBTVShow As Database.DBElement)
        If DBTVShow.MainDetails.TitleSpecified Then
            tslLoading.Text = Master.eLang.GetString(761, "Verifying TV Show Details:")
            Application.DoEvents()

            Edit_TVShow(DBTVShow, Enums.AddonEventType.ScraperSingle_TVShow)
        End If

        pnlCancel.Visible = False
        tslLoading.Visible = False
        tspbLoading.Visible = False
        SetStatus(String.Empty)
        SetControlsEnabled(True)
        FilterPanel_IsEnabled_TVShow(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_TV(ByVal iPercent As Integer)
        If ReportDownloadPercent = True Then
            tspbLoading.Value = iPercent
            Refresh()
        End If
    End Sub

    Private Sub InfoDownloaded_TVEpisode(ByRef DBTVEpisode As Database.DBElement)
        If Not String.IsNullOrEmpty(DBTVEpisode.MainDetails.Title) Then
            tslLoading.Text = Master.eLang.GetString(762, "Verifying TV Episode Details:")
            Application.DoEvents()

            Edit_TVEpisode(DBTVEpisode, Enums.AddonEventType.ScraperSingle_TVEpisode)
        End If

        pnlCancel.Visible = False
        tslLoading.Visible = False
        tspbLoading.Visible = False
        SetStatus(String.Empty)
        SetControlsEnabled(True)
        FilterPanel_IsEnabled_TVShow(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_TVEpisode(ByVal iPercent As Integer)
        If ReportDownloadPercent = True Then
            tspbLoading.Value = iPercent
            Refresh()
        End If
    End Sub

    Private Sub InfoDownloaded_TVSeason(ByRef DBTVSeason As Database.DBElement)
        If Not String.IsNullOrEmpty(DBTVSeason.TVShowDetails.Title) Then
            tslLoading.Text = Master.eLang.GetString(80, "Verifying TV Season Details:")
            Application.DoEvents()

            Edit_TVSeason(DBTVSeason, Enums.AddonEventType.ScraperSingle_TVSeason)
        End If

        pnlCancel.Visible = False
        tslLoading.Visible = False
        tspbLoading.Visible = False
        SetStatus(String.Empty)
        SetControlsEnabled(True)
        FilterPanel_IsEnabled_TVShow(True)
    End Sub
    ''' <summary>
    ''' Update the progressbar for the download progress
    ''' </summary>
    ''' <param name="iPercent">Percent of progress (expect 0 - 100)</param>
    ''' <remarks></remarks>
    Private Sub InfoDownloadedPercent_TVSeason(ByVal iPercent As Integer)
        If ReportDownloadPercent = True Then
            tspbLoading.Value = iPercent
            Refresh()
        End If
    End Sub

    Function MyResolveEventHandler(ByVal sender As Object, ByVal args As ResolveEventArgs) As [Assembly]
        Dim asm As Assembly = Nothing
        Dim name As String = args.Name.Split(Convert.ToChar(","))(0)
        Dim version As Match = Regex.Match(args.Name.ToLower, "version=(.*?),")
        If version.Success Then
            asm = AddonsManager.AssemblyList.FirstOrDefault(Function(y) y.AssemblyName = name AndAlso y.AssemblyVersion.ToString = version.Groups(1).Value).Assembly
        Else
            asm = AddonsManager.AssemblyList.FirstOrDefault(Function(y) y.AssemblyName = name).Assembly
        End If
        If asm Is Nothing Then
            asm = AddonsManager.AssemblyList.FirstOrDefault(Function(y) y.AssemblyName = name.Split(Convert.ToChar("."))(0)).Assembly
        End If
        Return asm
    End Function

    Private Sub cmnuMovieOpenFolder_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieOpenFolder.Click
        If dgvMovies.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvMovies.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvMovies.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If
            If doOpen Then
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    Using Explorer As New Process
                        Explorer.StartInfo.FileName = "explorer.exe"
                        Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", FileUtils.Stacking.GetFirstPathFromStack(sRow.Cells(Database.Helpers.GetColumnName(Database.ColumnName.Path)).Value.ToString))
                        Explorer.Start()
                    End Using
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Open IMDB-Page of selected episode(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuEpisodeBrowseIMDB_Click(sender As Object, e As EventArgs) Handles cmnuEpisodeBrowseIMDB.Click
        If dgvTVEpisodes.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvTVEpisodes.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVEpisodes.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If
            If doOpen Then
                For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                    Functions.Launch(StringUtils.GetURL_IMDb(Master.DB.Load_TVEpisode(CLng(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.episode)).Value), True)))
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Open TMDB-Page of selected episode(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuEpisodeBrowseTMDB_Click(sender As Object, e As EventArgs) Handles cmnuEpisodeBrowseTMDB.Click
        If dgvTVEpisodes.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvTVEpisodes.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVEpisodes.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If
            If doOpen Then
                For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                    Functions.Launch(StringUtils.GetURL_TMDb(Master.DB.Load_TVEpisode(CLng(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.episode)).Value), True)))
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Open TVDB-Page of selected episode(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuEpisodeBrowseTVDB_Click(sender As Object, e As EventArgs) Handles cmnuEpisodeBrowseTVDB.Click
        If dgvTVEpisodes.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvTVEpisodes.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVEpisodes.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If
            If doOpen Then
                For Each sRow As DataGridViewRow In dgvTVEpisodes.SelectedRows
                    Functions.Launch(StringUtils.GetURL_TVDb(Master.DB.Load_TVEpisode(CLng(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.episode)).Value), True)))
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Open IMDB-Page of selected movie(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuMovieBrowseIMDB_Click(sender As Object, e As EventArgs) Handles cmnuMovieBrowseIMDB.Click
        If dgvMovies.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvMovies.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvMovies.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If
            If doOpen Then
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    Functions.Launch(StringUtils.GetURL_IMDb(Master.DB.Load_Movie(CLng(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value))))
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Open TMDB-Page of selected movie(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuMovieBrowseTMDB_Click(sender As Object, e As EventArgs) Handles cmnuMovieBrowseTMDB.Click
        If dgvMovies.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvMovies.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvMovies.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If
            If doOpen Then
                For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                    Functions.Launch(StringUtils.GetURL_TMDb(Master.DB.Load_Movie(CLng(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value))))
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Open TMDB-Page of selected movieset(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuMovieSetBrowseTMDB_Click(sender As Object, e As EventArgs) Handles cmnuMovieSetBrowseTMDB.Click
        If dgvMoviesets.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvMoviesets.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvMoviesets.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If
            If doOpen Then
                For Each sRow As DataGridViewRow In dgvMoviesets.SelectedRows
                    Functions.Launch(StringUtils.GetURL_TMDb(Master.DB.Load_Movieset(CLng(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movieset)).Value))))
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Open IMDB-Page of selected season(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuSeasonBrowseIMDB_Click(sender As Object, e As EventArgs) Handles cmnuSeasonBrowseIMDB.Click
        If dgvTVSeasons.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvTVSeasons.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVSeasons.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If
            If doOpen Then
                For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                    Functions.Launch(StringUtils.GetURL_IMDb(Master.DB.Load_TVSeason(CLng(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.season)).Value), True, False)))
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Open TMDB-Page of selected season(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuSeasonBrowseTMDB_Click(sender As Object, e As EventArgs) Handles cmnuSeasonBrowseTMDB.Click
        If dgvTVSeasons.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvTVSeasons.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVSeasons.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If
            If doOpen Then
                For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                    Functions.Launch(StringUtils.GetURL_TMDb(Master.DB.Load_TVSeason(CLng(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.season)).Value), True, False)))
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Open TVDB-Page of selected seasons(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuSeasonBrowseTVDB_Click(sender As Object, e As EventArgs) Handles cmnuSeasonBrowseTVDB.Click
        If dgvTVSeasons.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvTVSeasons.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVSeasons.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If
            If doOpen Then
                For Each sRow As DataGridViewRow In dgvTVSeasons.SelectedRows
                    Functions.Launch(StringUtils.GetURL_TVDb(Master.DB.Load_TVSeason(CLng(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.season)).Value), True, False)))
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Open IMDB-Page of selected show(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuShowBrowseIMDB_Click(sender As Object, e As EventArgs) Handles cmnuShowBrowseIMDB.Click
        If dgvTVShows.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvTVShows.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVShows.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If
            If doOpen Then
                For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                    Functions.Launch(StringUtils.GetURL_IMDb(Master.DB.Load_TVShow(CLng(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.tvshow)).Value), False, False)))
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Open TMDB-Page of selected tvshow(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuShowBrowseTMDB_Click(sender As Object, e As EventArgs) Handles cmnuShowBrowseTMDB.Click
        If dgvTVShows.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvTVShows.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVShows.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If
            If doOpen Then
                For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                    Functions.Launch(StringUtils.GetURL_TMDb(Master.DB.Load_TVShow(CLng(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.tvshow)).Value), False, False)))
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Open TVDB-Page of selected tvshow(s) in defaultbrowser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmnuShowBrowseTVDB_Click(sender As Object, e As EventArgs) Handles cmnuShowBrowseTVDB.Click
        If dgvTVShows.SelectedRows.Count > 0 Then
            Dim doOpen As Boolean = True
            If dgvTVShows.SelectedRows.Count > 10 Then
                If Not MessageBox.Show(String.Format(Master.eLang.GetString(635, "You have selected {0} folders to open. Are you sure you want to do this?"), dgvTVShows.SelectedRows.Count), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then doOpen = False
            End If
            If doOpen Then
                For Each sRow As DataGridViewRow In dgvTVShows.SelectedRows
                    Functions.Launch(StringUtils.GetURL_TVDb(Master.DB.Load_TVShow(CLng(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.tvshow)).Value), False, False)))
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' Draw genre text over the image when mouse hovers
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbGenre_MouseEnter(ByVal sender As Object, ByVal e As EventArgs)
        If Manager.mSettings.MainOptions.GuiSettings.DisplayGenreText Then Return 'Because Image already has genre text displayed
        GenreImage = DirectCast(sender, PictureBox).Image    'Store the image for later retrieval
        DirectCast(sender, PictureBox).Image = ImageUtils.AddGenreString(DirectCast(sender, PictureBox).Image, DirectCast(sender, PictureBox).Name.ToString)
    End Sub
    ''' <summary>
    ''' Reset genre image when mouse leaves to "clear" the text
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub pbGenre_MouseLeave(ByVal sender As Object, ByVal e As EventArgs)
        If Manager.mSettings.MainOptions.GuiSettings.DisplayGenreText Then Return
        DirectCast(sender, PictureBox).Image = GenreImage
    End Sub

    'Private Sub pbBanner_DoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbBanner.MouseDoubleClick
    '    Try
    '        If e.Button = MouseButtons.Left OrElse Not Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then
    '            If pbBannerCache.Image IsNot Nothing Then
    '                Using dImgView As New dlgImageViewer
    '                    dImgView.ShowDialog(pbBannerCache.Image)
    '                End Using
    '            End If
    '        ElseIf e.Button = MouseButtons.Right AndAlso Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then

    '            Select Case MainTab_GetCurrentTag().ContentType
    '                Case Enums.ContentType.Movie
    '                    If dgvMovies.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMovies.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMovies.Item(Database.Helpers.GetMainIdName(Database.TableName.movie), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)
    '                    Functions.SetScrapeModifiers(tmpDBElement.ScrapeModifiers, Enums.ModifierType.MainBanner, True)
    '                    Dim nResult = Scraper.Run(tmpDBElement)
    '                    If nResult IsNot Nothing Then
    '                        If nResult.lstImages.MainBanners.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, nResult.lstImages) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
    '                                Master.DB.Save_Movie(tmpDBElement, False, False, True, True, False)
    '                                DataGridView_RefreshRow_Movie(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.Movieset
    '                    If dgvMoviesets.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMoviesets.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMoviesets.Item(Database.Helpers.GetMainIdName(Database.TableName.movieset), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movieset(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainBanner, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
    '                        If aContainer.MainBanners.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
    '                                Master.DB.Save_Movieset(tmpDBElement, False, True, True, True)
    '                                DataGridView_RefreshRow_Movieset(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.TV
    '                    'TV Show list
    '                    If dgvTVShows.Focused Then
    '                        If dgvTVShows.SelectedRows.Count > 1 Then Return
    '                        SetControlsEnabled(False)

    '                        Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
    '                        Dim ID As Long = Convert.ToInt64(dgvTVShows.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow), indX).Value)
    '                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

    '                        Dim aContainer As New MediaContainers.SearchResultsContainer
    '                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainBanner, True)
    '                        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                            If aContainer.MainBanners.Count > 0 Then
    '                                Dim dlgImgS As New dlgImageSelect()
    '                                If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                    tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
    '                                    Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
    '                                    DataGridView_RefreshRow_TVShow(ID)
    '                                End If
    '                            Else
    '                                MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If
    '                        End If
    '                        SetControlsEnabled(True)

    '                        'TV Season list
    '                    ElseIf dgvTVSeasons.Focused Then
    '                        If dgvTVSeasons.SelectedRows.Count > 1 Then Return
    '                        SetControlsEnabled(False)

    '                        Dim indX As Integer = dgvTVSeasons.SelectedRows(0).Index
    '                        Dim ID As Long = Convert.ToInt64(dgvTVSeasons.Item(Database.Helpers.GetMainIdName(Database.TableName.season), indX).Value)
    '                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)

    '                        Dim aContainer As New MediaContainers.SearchResultsContainer
    '                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                        If tmpDBElement.MainDetails.Season_IsAllSeasons Then
    '                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsBanner, True)
    '                        Else
    '                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonBanner, True)
    '                        End If
    '                        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                            If aContainer.SeasonBanners.Count > 0 OrElse (tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso aContainer.MainBanners.Count > 0) Then
    '                                Dim dlgImgS As New dlgImageSelect()
    '                                If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                    tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
    '                                    Master.DB.Save_TVSeason(tmpDBElement, False, True, True)
    '                                    DataGridView_RefreshRow_TVSeason(ID)
    '                                End If
    '                            Else
    '                                MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If
    '                        End If
    '                        SetControlsEnabled(True)

    '                        'TV Episode list
    '                    ElseIf dgvTVEpisodes.Focused Then
    '                        Return
    '                    End If
    '            End Select
    '        End If
    '    Catch ex As Exception
    '        _Logger.Error(ex, New StackFrame().GetMethod().Name)
    '        SetControlsEnabled(True)
    '    End Try
    'End Sub

    'Private Sub pbCharacterArt_DoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbCharacterArt.MouseDoubleClick
    '    Try
    '        If e.Button = MouseButtons.Left OrElse Not Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then
    '            If pbCharacterArtCache.Image IsNot Nothing Then
    '                Using dImgView As New dlgImageViewer
    '                    dImgView.ShowDialog(pbCharacterArtCache.Image)
    '                End Using
    '            End If
    '        ElseIf e.Button = MouseButtons.Right AndAlso Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then

    '            Select Case MainTab_GetCurrentTag().ContentType
    '                Case Enums.ContentType.Movie
    '                    Return
    '                Case Enums.ContentType.Movieset
    '                    Return
    '                Case Enums.ContentType.TV
    '                    'TV Show list
    '                    If dgvTVShows.Focused Then
    '                        If dgvTVShows.SelectedRows.Count > 1 Then Return
    '                        SetControlsEnabled(False)

    '                        Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
    '                        Dim ID As Long = Convert.ToInt64(dgvTVShows.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow), indX).Value)
    '                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

    '                        Dim aContainer As New MediaContainers.SearchResultsContainer
    '                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainCharacterArt, True)
    '                        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                            If aContainer.MainCharacterArts.Count > 0 Then
    '                                Dim dlgImgS As New dlgImageSelect()
    '                                If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                    tmpDBElement.ImagesContainer.CharacterArt = dlgImgS.Result.ImagesContainer.CharacterArt
    '                                    Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
    '                                    DataGridView_RefreshRow_TVShow(ID)
    '                                End If
    '                            Else
    '                                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If
    '                        End If
    '                        SetControlsEnabled(True)

    '                        'TV Season list
    '                    ElseIf dgvTVSeasons.Focused Then
    '                        Return

    '                        'TV Episode list
    '                    ElseIf dgvTVEpisodes.Focused Then
    '                        Return
    '                    End If
    '            End Select
    '        End If
    '    Catch ex As Exception
    '        _Logger.Error(ex, New StackFrame().GetMethod().Name)
    '        SetControlsEnabled(True)
    '    End Try
    'End Sub

    'Private Sub pbClearArt_DoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbClearArt.MouseDoubleClick
    '    Try
    '        If e.Button = MouseButtons.Left OrElse Not Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then
    '            If pbClearArtCache.Image IsNot Nothing Then
    '                Using dImgView As New dlgImageViewer
    '                    dImgView.ShowDialog(pbClearArtCache.Image)
    '                End Using
    '            End If
    '        ElseIf e.Button = MouseButtons.Right AndAlso Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then

    '            Select Case MainTab_GetCurrentTag().ContentType
    '                Case Enums.ContentType.Movie
    '                    If dgvMovies.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMovies.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMovies.Item(Database.Helpers.GetMainIdName(Database.TableName.movie), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                        If aContainer.MainClearArts.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.ClearArt = dlgImgS.Result.ImagesContainer.ClearArt
    '                                Master.DB.Save_Movie(tmpDBElement, False, False, True, True, False)
    '                                DataGridView_RefreshRow_Movie(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(1102, "No ClearArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.Movieset
    '                    If dgvMoviesets.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMoviesets.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMoviesets.Item(Database.Helpers.GetMainIdName(Database.TableName.movieset), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movieset(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
    '                        If aContainer.MainClearArts.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.ClearArt = dlgImgS.Result.ImagesContainer.ClearArt
    '                                Master.DB.Save_Movieset(tmpDBElement, False, False, True, True)
    '                                DataGridView_RefreshRow_Movieset(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(1102, "No ClearArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.TV
    '                    'TV Show list
    '                    If dgvTVShows.Focused Then
    '                        If dgvTVShows.SelectedRows.Count > 1 Then Return
    '                        SetControlsEnabled(False)

    '                        Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
    '                        Dim ID As Long = Convert.ToInt64(dgvTVShows.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow), indX).Value)
    '                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

    '                        Dim aContainer As New MediaContainers.SearchResultsContainer
    '                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
    '                        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                            If aContainer.MainClearArts.Count > 0 Then
    '                                Dim dlgImgS As New dlgImageSelect()
    '                                If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                    tmpDBElement.ImagesContainer.ClearArt = dlgImgS.Result.ImagesContainer.ClearArt
    '                                    Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
    '                                    DataGridView_RefreshRow_TVShow(ID)
    '                                End If
    '                            Else
    '                                MessageBox.Show(Master.eLang.GetString(1102, "No ClearArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If
    '                        End If
    '                        SetControlsEnabled(True)

    '                        'TV Season list
    '                    ElseIf dgvTVSeasons.Focused Then
    '                        Return

    '                        'TV Episode list
    '                    ElseIf dgvTVEpisodes.Focused Then
    '                        Return
    '                    End If
    '            End Select
    '        End If
    '    Catch ex As Exception
    '        _Logger.Error(ex, New StackFrame().GetMethod().Name)
    '        SetControlsEnabled(True)
    '    End Try
    'End Sub

    'Private Sub pbClearLogo_DoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbClearLogo.MouseDoubleClick
    '    Try
    '        If e.Button = MouseButtons.Left OrElse Not Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then
    '            If pbClearLogoCache.Image IsNot Nothing Then
    '                Using dImgView As New dlgImageViewer
    '                    dImgView.ShowDialog(pbClearLogoCache.Image)
    '                End Using
    '            End If
    '        ElseIf e.Button = MouseButtons.Right AndAlso Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then

    '            Select Case MainTab_GetCurrentTag().ContentType
    '                Case Enums.ContentType.Movie
    '                    If dgvMovies.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMovies.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMovies.Item(Database.Helpers.GetMainIdName(Database.TableName.movie), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                        If aContainer.MainClearLogos.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.ClearLogo = dlgImgS.Result.ImagesContainer.ClearLogo
    '                                Master.DB.Save_Movie(tmpDBElement, False, False, True, True, False)
    '                                DataGridView_RefreshRow_Movie(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(1103, "No ClearLogos found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.Movieset
    '                    If dgvMoviesets.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMoviesets.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMoviesets.Item(Database.Helpers.GetMainIdName(Database.TableName.movieset), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movieset(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
    '                        If aContainer.MainClearLogos.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.ClearLogo = dlgImgS.Result.ImagesContainer.ClearLogo
    '                                Master.DB.Save_Movieset(tmpDBElement, False, False, True, True)
    '                                DataGridView_RefreshRow_Movieset(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(1103, "No ClearLogos found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.TV
    '                    'TV Show list
    '                    If dgvTVShows.Focused Then
    '                        If dgvTVShows.SelectedRows.Count > 1 Then Return
    '                        SetControlsEnabled(False)

    '                        Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
    '                        Dim ID As Long = Convert.ToInt64(dgvTVShows.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow), indX).Value)
    '                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

    '                        Dim aContainer As New MediaContainers.SearchResultsContainer
    '                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
    '                        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                            If aContainer.MainClearLogos.Count > 0 Then
    '                                Dim dlgImgS As New dlgImageSelect()
    '                                If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                    tmpDBElement.ImagesContainer.ClearLogo = dlgImgS.Result.ImagesContainer.ClearLogo
    '                                    Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
    '                                    DataGridView_RefreshRow_TVShow(ID)
    '                                End If
    '                            Else
    '                                MessageBox.Show(Master.eLang.GetString(1103, "No ClearLogos found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If
    '                        End If
    '                        SetControlsEnabled(True)

    '                        'TV Season list
    '                    ElseIf dgvTVSeasons.Focused Then
    '                        Return

    '                        'TV Episode list
    '                    ElseIf dgvTVEpisodes.Focused Then
    '                        Return
    '                    End If
    '            End Select
    '        End If
    '    Catch ex As Exception
    '        _Logger.Error(ex, New StackFrame().GetMethod().Name)
    '        SetControlsEnabled(True)
    '    End Try
    'End Sub

    'Private Sub pbDiscArt_DoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbDiscArt.MouseDoubleClick
    '    Try
    '        If e.Button = MouseButtons.Left OrElse Not Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then
    '            If pbDiscArtCache.Image IsNot Nothing Then
    '                Using dImgView As New dlgImageViewer
    '                    dImgView.ShowDialog(pbDiscArtCache.Image)
    '                End Using
    '            End If
    '        ElseIf e.Button = MouseButtons.Right AndAlso Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then

    '            Select Case MainTab_GetCurrentTag().ContentType
    '                Case Enums.ContentType.Movie
    '                    If dgvMovies.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMovies.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMovies.Item(Database.Helpers.GetMainIdName(Database.TableName.movie), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainDiscArt, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                        If aContainer.MainDiscArts.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.DiscArt = dlgImgS.Result.ImagesContainer.DiscArt
    '                                Master.DB.Save_Movie(tmpDBElement, False, False, True, True, False)
    '                                DataGridView_RefreshRow_Movie(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(1104, "No DiscArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.Movieset
    '                    If dgvMoviesets.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMoviesets.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMoviesets.Item(Database.Helpers.GetMainIdName(Database.TableName.movieset), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movieset(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainDiscArt, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
    '                        If aContainer.MainDiscArts.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.DiscArt = dlgImgS.Result.ImagesContainer.DiscArt
    '                                Master.DB.Save_Movieset(tmpDBElement, False, False, True, True)
    '                                DataGridView_RefreshRow_Movieset(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(1104, "No DiscArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.TV
    '                    'TV Show list
    '                    If dgvTVShows.Focused Then
    '                        Return

    '                        'TV Season list
    '                    ElseIf dgvTVSeasons.Focused Then
    '                        Return

    '                        'TV Episode list
    '                    ElseIf dgvTVEpisodes.Focused Then
    '                        Return
    '                    End If
    '            End Select
    '        End If
    '    Catch ex As Exception
    '        _Logger.Error(ex, New StackFrame().GetMethod().Name)
    '        SetControlsEnabled(True)
    '    End Try
    'End Sub
    '''' <summary>
    '''' Show the Fanart in the Image Viewer
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub pbFanart_DoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbFanart.MouseDoubleClick, pbFanartSmall.MouseDoubleClick
    '    Try
    '        If e.Button = MouseButtons.Left OrElse Not Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then
    '            If pbFanartCache.Image IsNot Nothing Then
    '                Using dImgView As New dlgImageViewer
    '                    dImgView.ShowDialog(pbFanartCache.Image)
    '                End Using
    '            ElseIf pbFanartSmallCache.Image IsNot Nothing Then
    '                Using dImgView As New dlgImageViewer
    '                    dImgView.ShowDialog(pbFanartSmallCache.Image)
    '                End Using
    '            End If
    '        ElseIf e.Button = MouseButtons.Right AndAlso Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then

    '            Select Case MainTab_GetCurrentTag().ContentType
    '                Case Enums.ContentType.Movie
    '                    If dgvMovies.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMovies.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMovies.Item(Database.Helpers.GetMainIdName(Database.TableName.movie), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                        If aContainer.MainFanarts.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
    '                                Master.DB.Save_Movie(tmpDBElement, False, False, True, True, False)
    '                                DataGridView_RefreshRow_Movie(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.Movieset
    '                    If dgvMoviesets.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMoviesets.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMoviesets.Item(Database.Helpers.GetMainIdName(Database.TableName.movieset), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movieset(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
    '                        If aContainer.MainFanarts.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
    '                                Master.DB.Save_Movieset(tmpDBElement, False, False, True, True)
    '                                DataGridView_RefreshRow_Movieset(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.TV
    '                    'TV Show list
    '                    If dgvTVShows.Focused Then
    '                        If dgvTVShows.SelectedRows.Count > 1 Then Return
    '                        SetControlsEnabled(False)

    '                        Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
    '                        Dim ID As Long = Convert.ToInt64(dgvTVShows.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow), indX).Value)
    '                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

    '                        Dim aContainer As New MediaContainers.SearchResultsContainer
    '                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
    '                        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                            If aContainer.MainFanarts.Count > 0 Then
    '                                Dim dlgImgS As New dlgImageSelect()
    '                                If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                    tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
    '                                    Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
    '                                    DataGridView_RefreshRow_TVShow(ID)
    '                                End If
    '                            Else
    '                                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If
    '                        End If
    '                        SetControlsEnabled(True)

    '                        'TV Season list
    '                    ElseIf dgvTVSeasons.Focused Then
    '                        If dgvTVSeasons.SelectedRows.Count > 1 Then Return
    '                        SetControlsEnabled(False)

    '                        Dim indX As Integer = dgvTVSeasons.SelectedRows(0).Index
    '                        Dim ID As Long = Convert.ToInt64(dgvTVSeasons.Item(Database.Helpers.GetMainIdName(Database.TableName.season), indX).Value)
    '                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)

    '                        Dim aContainer As New MediaContainers.SearchResultsContainer
    '                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                        If tmpDBElement.MainDetails.Season_IsAllSeasons Then
    '                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsFanart, True)
    '                        Else
    '                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonFanart, True)
    '                        End If
    '                        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                            If aContainer.SeasonFanarts.Count > 0 OrElse aContainer.MainFanarts.Count > 0 Then
    '                                Dim dlgImgS As New dlgImageSelect()
    '                                If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                    tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
    '                                    Master.DB.Save_TVSeason(tmpDBElement, False, True, True)
    '                                    DataGridView_RefreshRow_TVSeason(ID)
    '                                End If
    '                            Else
    '                                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If
    '                        End If
    '                        SetControlsEnabled(True)

    '                        'TV Episode list
    '                    ElseIf dgvTVEpisodes.Focused Then
    '                        If dgvTVEpisodes.SelectedRows.Count > 1 Then Return
    '                        SetControlsEnabled(False)

    '                        Dim indX As Integer = dgvTVEpisodes.SelectedRows(0).Index
    '                        Dim ID As Long = Convert.ToInt64(dgvTVEpisodes.Item(Database.Helpers.GetMainIdName(Database.TableName.episode), indX).Value)
    '                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)

    '                        Dim aContainer As New MediaContainers.SearchResultsContainer
    '                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodeFanart, True)
    '                        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                            If aContainer.EpisodeFanarts.Count > 0 OrElse aContainer.MainFanarts.Count > 0 Then
    '                                Dim dlgImgS As New dlgImageSelect()
    '                                If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                    tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
    '                                    Master.DB.Save_TVEpisode(tmpDBElement, False, False, True, False, True)
    '                                    DataGridView_RefreshRow_TVEpisode(ID)
    '                                End If
    '                            Else
    '                                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If
    '                        End If
    '                        SetControlsEnabled(True)
    '                    End If
    '            End Select
    '        End If
    '    Catch ex As Exception
    '        _Logger.Error(ex, New StackFrame().GetMethod().Name)
    '        SetControlsEnabled(True)
    '    End Try
    'End Sub

    'Private Sub pbKeyArt_DoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbKeyArt.MouseDoubleClick
    '    Try
    '        If e.Button = MouseButtons.Left OrElse Not Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then
    '            If pbKeyArtCache.Image IsNot Nothing Then
    '                Using dImgView As New dlgImageViewer
    '                    dImgView.ShowDialog(pbKeyArtCache.Image)
    '                End Using
    '            End If
    '        ElseIf e.Button = MouseButtons.Right AndAlso Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then

    '            Select Case MainTab_GetCurrentTag().ContentType
    '                Case Enums.ContentType.Movie
    '                    If dgvMovies.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMovies.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMovies.Item(Database.Helpers.GetMainIdName(Database.TableName.movie), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainKeyArt, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                        If aContainer.MainKeyArts.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.KeyArt = dlgImgS.Result.ImagesContainer.KeyArt
    '                                Master.DB.Save_Movie(tmpDBElement, False, False, True, True, False)
    '                                DataGridView_RefreshRow_Movie(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.Movieset
    '                    If dgvMoviesets.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMoviesets.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMoviesets.Item(Database.Helpers.GetMainIdName(Database.TableName.movieset), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movieset(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainKeyArt, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
    '                        If aContainer.MainKeyArts.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.KeyArt = dlgImgS.Result.ImagesContainer.KeyArt
    '                                Master.DB.Save_Movieset(tmpDBElement, False, False, True, True)
    '                                DataGridView_RefreshRow_Movieset(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.TV
    '                    'TV Show list
    '                    If dgvTVShows.Focused Then
    '                        If dgvTVShows.SelectedRows.Count > 1 Then Return
    '                        SetControlsEnabled(False)

    '                        Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
    '                        Dim ID As Long = Convert.ToInt64(dgvTVShows.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow), indX).Value)
    '                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

    '                        Dim aContainer As New MediaContainers.SearchResultsContainer
    '                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainKeyArt, True)
    '                        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                            If aContainer.MainKeyArts.Count > 0 Then
    '                                Dim dlgImgS As New dlgImageSelect()
    '                                If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                    tmpDBElement.ImagesContainer.KeyArt = dlgImgS.Result.ImagesContainer.KeyArt
    '                                    Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
    '                                    DataGridView_RefreshRow_TVShow(ID)
    '                                End If
    '                            Else
    '                                MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If
    '                        End If
    '                        SetControlsEnabled(True)
    '                    End If
    '            End Select
    '        End If
    '    Catch ex As Exception
    '        _Logger.Error(ex, New StackFrame().GetMethod().Name)
    '        SetControlsEnabled(True)
    '    End Try
    'End Sub

    'Private Sub pbLandscape_DoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbLandscape.MouseDoubleClick
    '    Try
    '        If e.Button = MouseButtons.Left OrElse Not Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then
    '            If pbLandscapeCache.Image IsNot Nothing Then
    '                Using dImgView As New dlgImageViewer
    '                    dImgView.ShowDialog(pbLandscapeCache.Image)
    '                End Using
    '            End If
    '        ElseIf e.Button = MouseButtons.Right AndAlso Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then

    '            Select Case MainTab_GetCurrentTag().ContentType
    '                Case Enums.ContentType.Movie
    '                    If dgvMovies.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMovies.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMovies.Item(Database.Helpers.GetMainIdName(Database.TableName.movie), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                        If aContainer.MainLandscapes.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
    '                                Master.DB.Save_Movie(tmpDBElement, False, False, True, True, False)
    '                                DataGridView_RefreshRow_Movie(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(1197, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.Movieset
    '                    If dgvMoviesets.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMoviesets.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMoviesets.Item(Database.Helpers.GetMainIdName(Database.TableName.movieset), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movieset(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
    '                        If aContainer.MainLandscapes.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
    '                                Master.DB.Save_Movieset(tmpDBElement, False, False, True, True)
    '                                DataGridView_RefreshRow_Movieset(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(1197, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.TV
    '                    'TV Show list
    '                    If dgvTVShows.Focused Then
    '                        If dgvTVShows.SelectedRows.Count > 1 Then Return
    '                        SetControlsEnabled(False)

    '                        Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
    '                        Dim ID As Long = Convert.ToInt64(dgvTVShows.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow), indX).Value)
    '                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

    '                        Dim aContainer As New MediaContainers.SearchResultsContainer
    '                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
    '                        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                            If aContainer.MainLandscapes.Count > 0 Then
    '                                Dim dlgImgS As New dlgImageSelect()
    '                                If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                    tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
    '                                    Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
    '                                    DataGridView_RefreshRow_TVShow(ID)
    '                                End If
    '                            Else
    '                                MessageBox.Show(Master.eLang.GetString(1197, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If
    '                        End If
    '                        SetControlsEnabled(True)

    '                        'TV Season list
    '                    ElseIf dgvTVSeasons.Focused Then
    '                        If dgvTVSeasons.SelectedRows.Count > 1 Then Return
    '                        SetControlsEnabled(False)

    '                        Dim indX As Integer = dgvTVSeasons.SelectedRows(0).Index
    '                        Dim ID As Long = Convert.ToInt64(dgvTVSeasons.Item(Database.Helpers.GetMainIdName(Database.TableName.season), indX).Value)
    '                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)

    '                        Dim aContainer As New MediaContainers.SearchResultsContainer
    '                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                        If tmpDBElement.MainDetails.Season_IsAllSeasons Then
    '                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsLandscape, True)
    '                        Else
    '                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonLandscape, True)
    '                        End If
    '                        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                            If aContainer.SeasonLandscapes.Count > 0 OrElse (tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso aContainer.MainLandscapes.Count > 0) Then
    '                                Dim dlgImgS As New dlgImageSelect()
    '                                If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                    tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
    '                                    Master.DB.Save_TVSeason(tmpDBElement, False, True, True)
    '                                    DataGridView_RefreshRow_TVSeason(ID)
    '                                End If
    '                            Else
    '                                MessageBox.Show(Master.eLang.GetString(1197, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If
    '                        End If
    '                        SetControlsEnabled(True)

    '                        'TV Episode list
    '                    ElseIf dgvTVEpisodes.Focused Then
    '                        Return
    '                    End If
    '            End Select
    '        End If
    '    Catch ex As Exception
    '        _Logger.Error(ex, New StackFrame().GetMethod().Name)
    '        SetControlsEnabled(True)
    '    End Try
    'End Sub

    'Private Sub pbPoster_DoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbPoster.MouseDoubleClick
    '    Try
    '        If e.Button = MouseButtons.Left OrElse Not Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then
    '            If pbPosterCache.Image IsNot Nothing Then
    '                Using dImgView As New dlgImageViewer
    '                    dImgView.ShowDialog(pbPosterCache.Image)
    '                End Using
    '            End If
    '        ElseIf e.Button = MouseButtons.Right AndAlso Manager.mSettings.MainOptions.GuiSettings.DoubleClickScrapeEnabled Then

    '            Select Case MainTab_GetCurrentTag().ContentType
    '                Case Enums.ContentType.Movie
    '                    If dgvMovies.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMovies.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMovies.Item(Database.Helpers.GetMainIdName(Database.TableName.movie), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                        If aContainer.MainPosters.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
    '                                Master.DB.Save_Movie(tmpDBElement, False, False, True, True, False)
    '                                DataGridView_RefreshRow_Movie(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.Movieset
    '                    If dgvMoviesets.SelectedRows.Count > 1 Then Return
    '                    SetControlsEnabled(False)

    '                    Dim indX As Integer = dgvMoviesets.SelectedRows(0).Index
    '                    Dim ID As Long = Convert.ToInt64(dgvMoviesets.Item(Database.Helpers.GetMainIdName(Database.TableName.movieset), indX).Value)
    '                    Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movieset(ID)

    '                    Dim aContainer As New MediaContainers.SearchResultsContainer
    '                    Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                    Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
    '                    If Not AddonsManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
    '                        If aContainer.MainPosters.Count > 0 Then
    '                            Dim dlgImgS As New dlgImageSelect()
    '                            If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
    '                                Master.DB.Save_Movieset(tmpDBElement, False, False, True, True)
    '                                DataGridView_RefreshRow_Movieset(ID)
    '                            End If
    '                        Else
    '                            MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        End If
    '                    End If
    '                    SetControlsEnabled(True)
    '                Case Enums.ContentType.TV
    '                    'TV Show list
    '                    If dgvTVShows.Focused Then
    '                        If dgvTVShows.SelectedRows.Count > 1 Then Return
    '                        SetControlsEnabled(False)

    '                        Dim indX As Integer = dgvTVShows.SelectedRows(0).Index
    '                        Dim ID As Long = Convert.ToInt64(dgvTVShows.Item(Database.Helpers.GetMainIdName(Database.TableName.tvshow), indX).Value)
    '                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)

    '                        Dim aContainer As New MediaContainers.SearchResultsContainer
    '                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
    '                        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                            If aContainer.MainPosters.Count > 0 Then
    '                                Dim dlgImgS As New dlgImageSelect()
    '                                If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                    tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
    '                                    Master.DB.Save_TVShow(tmpDBElement, False, False, True, False)
    '                                    DataGridView_RefreshRow_TVShow(ID)
    '                                End If
    '                            Else
    '                                MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If
    '                        End If
    '                        SetControlsEnabled(True)

    '                        'TV Season list
    '                    ElseIf dgvTVSeasons.Focused Then
    '                        If dgvTVSeasons.SelectedRows.Count > 1 Then Return
    '                        SetControlsEnabled(False)

    '                        Dim indX As Integer = dgvTVSeasons.SelectedRows(0).Index
    '                        Dim ID As Long = Convert.ToInt64(dgvTVSeasons.Item(Database.Helpers.GetMainIdName(Database.TableName.season), indX).Value)
    '                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)

    '                        Dim aContainer As New MediaContainers.SearchResultsContainer
    '                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                        If tmpDBElement.MainDetails.Season_IsAllSeasons Then
    '                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsPoster, True)
    '                        Else
    '                            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonPoster, True)
    '                        End If
    '                        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                            If aContainer.SeasonPosters.Count > 0 OrElse (tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso aContainer.MainPosters.Count > 0) Then
    '                                Dim dlgImgS As New dlgImageSelect()
    '                                If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                    tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
    '                                    Master.DB.Save_TVSeason(tmpDBElement, False, True, True)
    '                                    DataGridView_RefreshRow_TVSeason(ID)
    '                                End If
    '                            Else
    '                                MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If
    '                        End If
    '                        SetControlsEnabled(True)

    '                        'TV Episode list
    '                    ElseIf dgvTVEpisodes.Focused Then
    '                        If dgvTVEpisodes.SelectedRows.Count > 1 Then Return
    '                        SetControlsEnabled(False)

    '                        Dim indX As Integer = dgvTVEpisodes.SelectedRows(0).Index
    '                        Dim ID As Long = Convert.ToInt64(dgvTVEpisodes.Item(Database.Helpers.GetMainIdName(Database.TableName.episode), indX).Value)
    '                        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)

    '                        Dim aContainer As New MediaContainers.SearchResultsContainer
    '                        Dim ScrapeModifiers As New Structures.ScrapeModifiers

    '                        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.EpisodePoster, True)
    '                        If Not AddonsManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
    '                            If aContainer.EpisodePosters.Count > 0 Then
    '                                Dim dlgImgS As New dlgImageSelect()
    '                                If dlgImgS.ShowDialog(tmpDBElement, aContainer) = DialogResult.OK Then
    '                                    tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
    '                                    Master.DB.Save_TVEpisode(tmpDBElement, False, False, True, False, True)
    '                                    DataGridView_RefreshRow_TVEpisode(ID)
    '                                End If
    '                            Else
    '                                MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                            End If
    '                        End If
    '                        SetControlsEnabled(True)
    '                    End If
    '            End Select
    '        End If
    '    Catch ex As Exception
    '        _Logger.Error(ex, New StackFrame().GetMethod().Name)
    '        SetControlsEnabled(True)
    '    End Try
    'End Sub

    Private Sub rbFilterAnd_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbFilterAnd_Movies.Click
        If Filter_Movie.AnyRuleSpecified Then
            DataGridView_ApplyFilter_Movie()
        End If
    End Sub

    Private Sub rbFilterAnd_MovieSets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbFilterAnd_MovieSets.Click
        If Filter_Movieset.AnyRuleSpecified Then
            DataGridView_ApplyFilter_Movieset()
        End If
    End Sub

    Private Sub rbFilterAnd_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbFilterAnd_Shows.Click
        If Filter_TVShows.AnyRuleSpecified Then
            DataGridView_ApplyFilter_TVShow()
        End If
    End Sub

    Private Sub rbFilterOr_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbFilterOr_Movies.Click
        If Filter_Movie.AnyRuleSpecified Then
            DataGridView_ApplyFilter_Movie()
        End If
    End Sub

    Private Sub rbFilterOr_MovieSets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbFilterOr_MovieSets.Click
        If Filter_Movieset.AnyRuleSpecified Then
            DataGridView_ApplyFilter_Movieset()
        End If
    End Sub

    Private Sub rbFilterOr_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rbFilterOr_Shows.Click
        If Filter_TVShows.AnyRuleSpecified Then
            DataGridView_ApplyFilter_TVShow()
        End If
    End Sub

    Private Sub Reload_All_Movie()
        If _DtMovies.Rows.Count > 0 Then
            Cursor = Cursors.WaitCursor
            SetControlsEnabled(False, True)
            tspbLoading.Style = ProgressBarStyle.Continuous
            FilterPanel_IsEnabled_Movie(False)
            FilterPanel_IsEnabled_Movieset(False)
            FilterPanel_IsEnabled_TVShow(False)

            tspbLoading.Maximum = _DtMovies.Rows.Count + 1
            tspbLoading.Value = 0
            tslLoading.Text = String.Concat(Master.eLang.GetString(110, "Refreshing Media"), ":")
            tspbLoading.Visible = True
            tslLoading.Visible = True
            Application.DoEvents()
            bwReload_Movie.WorkerReportsProgress = True
            bwReload_Movie.WorkerSupportsCancellation = True
            bwReload_Movie.RunWorkerAsync()
        Else
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub Reload_All_Movieset()
        If _DtMovieSets.Rows.Count > 0 Then
            Cursor = Cursors.WaitCursor
            SetControlsEnabled(False, True)
            tspbLoading.Style = ProgressBarStyle.Continuous
            FilterPanel_IsEnabled_Movie(False)
            FilterPanel_IsEnabled_Movieset(False)
            FilterPanel_IsEnabled_TVShow(False)

            tspbLoading.Maximum = _DtMovieSets.Rows.Count + 1
            tspbLoading.Value = 0
            tslLoading.Text = String.Concat(Master.eLang.GetString(110, "Refreshing Media"), ":")
            tspbLoading.Visible = True
            tslLoading.Visible = True
            Application.DoEvents()
            bwReload_Movieset.WorkerReportsProgress = True
            bwReload_Movieset.WorkerSupportsCancellation = True
            bwReload_Movieset.RunWorkerAsync()
        Else
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub Reload_All_TVShow(ByVal reloadFull As Boolean)
        If _DtTVShows.Rows.Count > 0 Then
            Cursor = Cursors.WaitCursor
            SetControlsEnabled(False, True)
            tspbLoading.Style = ProgressBarStyle.Continuous
            FilterPanel_IsEnabled_Movie(False)
            FilterPanel_IsEnabled_Movieset(False)
            FilterPanel_IsEnabled_TVShow(False)

            tspbLoading.Maximum = _DtTVShows.Rows.Count + 1
            tspbLoading.Value = 0
            tslLoading.Text = String.Concat(Master.eLang.GetString(110, "Refreshing Media"), ":")
            tspbLoading.Visible = True
            tslLoading.Visible = True
            Application.DoEvents()
            bwReload_TVShow.WorkerReportsProgress = True
            bwReload_TVShow.WorkerSupportsCancellation = True
            bwReload_TVShow.RunWorkerAsync(reloadFull)
        Else
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub Rewrite_All_Movie(ByVal bRewriteAll As Boolean)
        If _DtMovies.Rows.Count > 0 Then
            SetControlsEnabled(False)
            tspbLoading.Style = ProgressBarStyle.Continuous
            FilterPanel_IsEnabled_Movie(False)
            FilterPanel_IsEnabled_Movieset(False)
            FilterPanel_IsEnabled_TVShow(False)

            btnCancel.Text = Master.eLang.GetString(1299, "Cancel Rewriting")
            lblCanceling.Text = Master.eLang.GetString(1300, "Canceling Rewriting...")
            btnCancel.Visible = True
            lblCanceling.Visible = False
            prbCanceling.Visible = False
            pnlCancel.Visible = True

            tspbLoading.Maximum = _DtMovies.Rows.Count + 1
            tspbLoading.Value = 0
            tslLoading.Text = Master.eLang.GetString(1297, "Rewriting Media:")
            tspbLoading.Visible = True
            tslLoading.Visible = True
            Application.DoEvents()
            bwRewriteContent.WorkerReportsProgress = True
            bwRewriteContent.WorkerSupportsCancellation = True
            bwRewriteContent.RunWorkerAsync(New Arguments With {.ContentType = Enums.ContentType.Movie, .Trigger = bRewriteAll})
        Else
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub Rewrite_All_Movieset(ByVal bRewriteAll As Boolean)
        If _DtMovieSets.Rows.Count > 0 Then
            SetControlsEnabled(False)
            tspbLoading.Style = ProgressBarStyle.Continuous
            FilterPanel_IsEnabled_Movie(False)
            FilterPanel_IsEnabled_Movieset(False)
            FilterPanel_IsEnabled_TVShow(False)

            btnCancel.Text = Master.eLang.GetString(1299, "Cancel Rewriting")
            lblCanceling.Text = Master.eLang.GetString(1300, "Canceling Rewriting...")
            btnCancel.Visible = True
            lblCanceling.Visible = False
            prbCanceling.Visible = False
            pnlCancel.Visible = True

            tspbLoading.Maximum = _DtMovieSets.Rows.Count + 1
            tspbLoading.Value = 0
            tslLoading.Text = Master.eLang.GetString(1297, "Rewriting Media:")
            tspbLoading.Visible = True
            tslLoading.Visible = True
            Application.DoEvents()
            bwRewriteContent.WorkerReportsProgress = True
            bwRewriteContent.WorkerSupportsCancellation = True
            bwRewriteContent.RunWorkerAsync(New Arguments With {.ContentType = Enums.ContentType.Movieset, .Trigger = bRewriteAll})
        Else
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub Rewrite_All_TVShow(ByVal bRewriteAll As Boolean)
        If _DtTVShows.Rows.Count > 0 Then
            SetControlsEnabled(False)
            tspbLoading.Style = ProgressBarStyle.Continuous
            FilterPanel_IsEnabled_Movie(False)
            FilterPanel_IsEnabled_Movieset(False)
            FilterPanel_IsEnabled_TVShow(False)

            btnCancel.Text = Master.eLang.GetString(1299, "Cancel Rewriting")
            lblCanceling.Text = Master.eLang.GetString(1300, "Canceling Rewriting...")
            btnCancel.Visible = True
            lblCanceling.Visible = False
            prbCanceling.Visible = False
            pnlCancel.Visible = True

            tspbLoading.Maximum = _DtTVShows.Rows.Count + 1
            tspbLoading.Value = 0
            tslLoading.Text = Master.eLang.GetString(1297, "Rewriting Media:")
            tspbLoading.Visible = True
            tslLoading.Visible = True
            Application.DoEvents()
            bwRewriteContent.WorkerReportsProgress = True
            bwRewriteContent.WorkerSupportsCancellation = True
            bwRewriteContent.RunWorkerAsync(New Arguments With {.ContentType = Enums.ContentType.TV, .Trigger = bRewriteAll})
        Else
            SetControlsEnabled(True)
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
        Dim DBMovie As Database.DBElement = Master.DB.Load_Movie(ID)

        If DBMovie.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBMovie, Not showMessage) Then
            Dim nScanner As New Scanner
            nScanner.Load_Movie(DBMovie, BatchMode)
            If Not BatchMode Then DataGridView_RefreshRow_Movie(DBMovie.ID)
        Else
            If showMessage AndAlso MessageBox.Show(String.Concat(Master.eLang.GetString(587, "This file is no longer available"), ".", Environment.NewLine,
                                                         Master.eLang.GetString(703, "Whould you like to remove it from the library?")),
                                                     Master.eLang.GetString(654, "Remove movie from library"),
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Master.DB.Remove_Movie(ID, BatchMode)
                Return True
            Else
                Return False
            End If
        End If

        If Not BatchMode Then
            DoTitleCheck()
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
    Private Function Reload_Movieset(ByVal ID As Long, Optional ByVal BatchMode As Boolean = False) As Boolean
        Dim DBMovieSet As Database.DBElement = Master.DB.Load_Movieset(ID)
        Dim nScanner As New Scanner
        nScanner.Load_MovieSet(DBMovieSet, BatchMode)
        If Not BatchMode Then DataGridView_RefreshRow_Movieset(DBMovieSet.ID)

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
        Dim DBTVEpisode As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
        Dim epCount As Integer = 0

        If Not DBTVEpisode.FileItem.IDSpecified Then Return False 'skipping missing episodes

        If DBTVEpisode.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBTVEpisode, showMessage) Then
            Dim nScanner As New Scanner
            nScanner.Load_TVEpisode(DBTVEpisode, False, BatchMode, False)
            If Not BatchMode Then DataGridView_RefreshRow_TVEpisode(DBTVEpisode.ID)
        Else
            If showMessage AndAlso MessageBox.Show(String.Concat(Master.eLang.GetString(587, "This file is no longer available"), ".", Environment.NewLine,
                                                         Master.eLang.GetString(703, "Whould you like to remove it from the library?")),
                                                     Master.eLang.GetString(738, "Remove episode from library"),
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Master.DB.Remove_TVEpisode(DBTVEpisode.FileItem.FullPath, False, BatchMode)
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
        Dim DBTVSeason As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)

        If DBTVSeason.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBTVSeason, showMessage) Then
            Dim nScanner As New Scanner
            nScanner.GetFolderContents_TVSeason(DBTVSeason)
            Master.DB.Save_TVSeason(DBTVSeason, BatchMode, False, True)
            If Not BatchMode Then DataGridView_RefreshRow_TVSeason(DBTVSeason.ID)
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
        Dim DBTVShow As Database.DBElement = Master.DB.Load_TVShow(ID, reloadFull, reloadFull)

        If DBTVShow.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBTVShow, showMessage) Then
            Dim nScanner As New Scanner
            nScanner.Load_TVShow(DBTVShow, False, BatchMode, False)
            If Not BatchMode Then DataGridView_RefreshRow_TVShow(DBTVShow.ID)
        Else
            If showMessage AndAlso MessageBox.Show(String.Concat(Master.eLang.GetString(719, "This path is no longer available"), ".", Environment.NewLine,
                                                         Master.eLang.GetString(703, "Whould you like to remove it from the library?")),
                                                     Master.eLang.GetString(776, "Remove tv show from library"),
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Master.DB.Remove_TVShow(ID, BatchMode)
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
    Private Function Rewrite_Movie(ByVal ID As Long, ByVal BatchMode As Boolean, ByVal bRewriteAll As Boolean) As Boolean
        Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movie(ID)

        If tmpDBElement.IsOnline Then
            Master.DB.Save_Movie(tmpDBElement, BatchMode, True, bRewriteAll, True, False)
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Load existing movieset content and save it again with all selected filenames
    ''' </summary>
    ''' <param name="ID">MovieSet ID</param>
    ''' <returns>reload list from database?</returns>
    ''' <remarks></remarks>
    Private Function Rewrite_Movieset(ByVal ID As Long, ByVal BatchMode As Boolean, ByVal bRewriteAll As Boolean) As Boolean
        Dim tmpDBElement As Database.DBElement = Master.DB.Load_Movieset(ID)

        If tmpDBElement.IsOnline Then
            Master.DB.Save_Movieset(tmpDBElement, BatchMode, True, bRewriteAll, True)
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Load existing movieset content and save it again with all selected filenames
    ''' </summary>
    ''' <param name="ID">MovieSet ID</param>
    ''' <returns>reload list from database?</returns>
    ''' <remarks></remarks>
    Private Function Rewrite_TVShow(ByVal ID As Long, ByVal BatchMode As Boolean, ByVal bRewriteAll As Boolean) As Boolean
        Dim tmpDBElement As Database.DBElement = Master.DB.Load_TVShow(ID, True, True, False)

        If tmpDBElement.IsOnline Then
            Master.DB.Save_TVShow(tmpDBElement, BatchMode, True, bRewriteAll, True)
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub cmnuMovieRemoveFromDB_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieRemoveFromDB.Click
        Dim lItemsToRemove As New List(Of Long)
        ClearInfo()

        For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
            lItemsToRemove.Add(Convert.ToInt64(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value))
        Next

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each tID As Long In lItemsToRemove
                Master.DB.Remove_Movie(tID, True)
                DataGridView_RemoveRow_Movie(tID)
            Next
            SQLtransaction.Commit()
        End Using

        DataGridView_Fill_Main(False, True, False)
    End Sub

    Private Sub scMain_SplitterMoved(ByVal sender As Object, ByVal e As SplitterEventArgs) Handles scMain.SplitterMoved
        Try
            If Created Then
                SuspendLayout()
                MoveMPAA()
                MoveGenres()

                ImageUtils.ResizePB(pbFanart, pbFanartCache, scMain.Panel2.Height - (pnlTop.Top + pnlTop.Height), scMain.Panel2.Width, True)
                pbFanart.Left = Convert.ToInt32((scMain.Panel2.Width - pbFanart.Width) / 2)
                pbFanart.Top = pnlTop.Top + pnlTop.Height
                pnlNoInfo.Location = New Point(Convert.ToInt32((scMain.Panel2.Width - pnlNoInfo.Width) / 2), Convert.ToInt32((scMain.Panel2.Height - pnlNoInfo.Height) / 2))
                pnlCancel.Location = New Point(Convert.ToInt32((scMain.Panel2.Width - pnlNoInfo.Width) / 2), 124)
                pnlFilterCountries_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterCountry_Movies.Left + 1,
                                                                  (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterCountry_Movies.Top) - pnlFilterCountries_Movies.Height)
                pnlFilterGenres_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterGenre_Movies.Left + 1,
                                                               (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterGenre_Movies.Top) - pnlFilterGenres_Movies.Height)
                pnlFilterGenres_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterSpecific_Shows.Left + tblFilterSpecific_Shows.Left + tblFilterSpecificData_Shows.Left + txtFilterGenre_Shows.Left + 1,
                                                              (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterSpecific_Shows.Top + tblFilterSpecific_Shows.Top + tblFilterSpecificData_Shows.Top + txtFilterGenre_Shows.Top) - pnlFilterGenres_Shows.Height)
                pnlFilterDataFields_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + gbFilterDataField_Movies.Left + tblFilterDataField_Movies.Left + txtFilterDataField_Movies.Left + 1,
                                                                   (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + gbFilterDataField_Movies.Top + tblFilterDataField_Movies.Top + txtFilterDataField_Movies.Top) - pnlFilterDataFields_Movies.Height)
                pnlFilterMissingItems_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterGeneral_Movies.Left + tblFilterGeneral_Movies.Left + btnFilterMissing_Movies.Left + 1,
                                                                     (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterGeneral_Movies.Top + tblFilterGeneral_Movies.Top + btnFilterMissing_Movies.Top) - pnlFilterMissingItems_Movies.Height)
                pnlFilterMissingItems_MovieSets.Location = New Point(pnlFilter_MovieSets.Left + tblFilter_MovieSets.Left + gbFilterGeneral_MovieSets.Left + tblFilterGeneral_MovieSets.Left + btnFilterMissing_MovieSets.Left + 1,
                                                                     (pnlFilter_MovieSets.Top + tblFilter_MovieSets.Top + gbFilterGeneral_MovieSets.Top + tblFilterGeneral_MovieSets.Top + btnFilterMissing_MovieSets.Top) - pnlFilterMissingItems_MovieSets.Height)
                pnlFilterMissingItems_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterGeneral_Shows.Left + tblFilterGeneral_Shows.Left + btnFilterMissing_Shows.Left + 1,
                                                                     (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterGeneral_Shows.Top + tblFilterGeneral_Shows.Top + btnFilterMissing_Shows.Top) - pnlFilterMissingItems_Shows.Height)
                pnlFilterSources_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterSource_Movies.Left + 1,
                                                                (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterSource_Movies.Top) - pnlFilterSources_Movies.Height)
                pnlFilterSources_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterSpecific_Shows.Left + tblFilterSpecific_Shows.Left + tblFilterSpecificData_Shows.Left + txtFilterSource_Shows.Left + 1,
                                                               (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterSpecific_Shows.Top + tblFilterSpecific_Shows.Top + tblFilterSpecificData_Shows.Top + txtFilterSource_Shows.Top) - pnlFilterSources_Shows.Height)
                pnlFilterTags_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterTag_Movies.Left + 1,
                                                               (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterTag_Movies.Top) - pnlFilterTags_Movies.Height)
                pnlFilterTags_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterSpecific_Shows.Left + tblFilterSpecific_Shows.Left + tblFilterSpecificData_Shows.Left + txtFilterTag_Shows.Left + 1,
                                                              (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterSpecific_Shows.Top + tblFilterSpecific_Shows.Top + tblFilterSpecificData_Shows.Top + txtFilterTag_Shows.Top) - pnlFilterTags_Shows.Height)
                pnlFilterVideoSources_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterVideoSource_Movies.Left + 1,
                                                                  (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterVideoSource_Movies.Top) - pnlFilterVideoSources_Movies.Height)

                Select Case tcMain.SelectedIndex
                    Case 0
                        dgvMovies.Focus()
                    Case 1
                        dgvMoviesets.Focus()
                    Case 2
                        dgvTVShows.Focus()
                End Select

                Theme_Apply(currThemeType)

                ResumeLayout(True)
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub SetAVImages(ByVal aImage As Image())
        pbVideoResolution.Image = aImage(0)
        pbVideoSource.Image = aImage(1)
        pbVideoCodec.Image = aImage(2)
        pbAudioCodec.Image = aImage(3)
        pbAudioChannels.Image = aImage(4)
        pbAudioLang0.Image = aImage(5)
        pbAudioLang1.Image = aImage(6)
        pbAudioLang2.Image = aImage(7)
        pbAudioLang3.Image = aImage(8)
        pbAudioLang4.Image = aImage(9)
        pbAudioLang5.Image = aImage(10)
        pbAudioLang6.Image = aImage(11)
        pbSubtitleLang0.Image = aImage(12)
        pbSubtitleLang1.Image = aImage(13)
        pbSubtitleLang2.Image = aImage(14)
        pbSubtitleLang3.Image = aImage(15)
        pbSubtitleLang4.Image = aImage(16)
        pbSubtitleLang5.Image = aImage(17)
        pbSubtitleLang6.Image = aImage(18)
        pbVideoChannels.Image = aImage(19)

        ToolTips.SetToolTip(pbAudioLang0, If(pbAudioLang0.Image IsNot Nothing, pbAudioLang0.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbAudioLang1, If(pbAudioLang1.Image IsNot Nothing, pbAudioLang1.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbAudioLang2, If(pbAudioLang2.Image IsNot Nothing, pbAudioLang2.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbAudioLang3, If(pbAudioLang3.Image IsNot Nothing, pbAudioLang3.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbAudioLang4, If(pbAudioLang4.Image IsNot Nothing, pbAudioLang4.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbAudioLang5, If(pbAudioLang5.Image IsNot Nothing, pbAudioLang5.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbAudioLang6, If(pbAudioLang6.Image IsNot Nothing, pbAudioLang6.Image.Tag.ToString, String.Empty))

        ToolTips.SetToolTip(pbSubtitleLang0, If(pbSubtitleLang0.Image IsNot Nothing, pbSubtitleLang0.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbSubtitleLang1, If(pbSubtitleLang1.Image IsNot Nothing, pbSubtitleLang1.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbSubtitleLang2, If(pbSubtitleLang2.Image IsNot Nothing, pbSubtitleLang2.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbSubtitleLang3, If(pbSubtitleLang3.Image IsNot Nothing, pbSubtitleLang3.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbSubtitleLang4, If(pbSubtitleLang4.Image IsNot Nothing, pbSubtitleLang4.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbSubtitleLang5, If(pbSubtitleLang5.Image IsNot Nothing, pbSubtitleLang5.Image.Tag.ToString, String.Empty))
        ToolTips.SetToolTip(pbSubtitleLang6, If(pbSubtitleLang6.Image IsNot Nothing, pbSubtitleLang6.Image.Tag.ToString, String.Empty))
    End Sub

    Private Sub SetControlsEnabled(ByVal isEnabled As Boolean, Optional ByVal withLists As Boolean = False, Optional ByVal withTools As Boolean = True)
        Dim currMainTabTag = MainTab_GetCurrentTag()
        For Each i As Object In mnuMainTools.DropDownItems
            If TypeOf i Is ToolStripMenuItem Then
                Dim o As ToolStripMenuItem = DirectCast(i, ToolStripMenuItem)
                If o.Tag Is Nothing Then
                    o.Enabled = isEnabled AndAlso ((dgvMovies.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.Movie) OrElse
                                                   (dgvMoviesets.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.Movieset) OrElse
                                                   (dgvTVShows.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.TV))
                ElseIf TypeOf o.Tag Is Structures.ModulesMenus Then
                    Dim tagmenu As Structures.ModulesMenus = DirectCast(o.Tag, Structures.ModulesMenus)
                    o.Enabled = (isEnabled OrElse Not withTools) AndAlso (((tagmenu.IfTabMovies AndAlso currMainTabTag.ContentType = Enums.ContentType.Movie) OrElse
                                                                           (tagmenu.IfTabMovieSets AndAlso currMainTabTag.ContentType = Enums.ContentType.Movieset) OrElse
                                                                           (tagmenu.IfTabTVShows AndAlso currMainTabTag.ContentType = Enums.ContentType.TV)) AndAlso
                                                                       ((tagmenu.ForMovies AndAlso (dgvMovies.RowCount > 0 OrElse tagmenu.IfNoMovies)) OrElse
                                                                        (tagmenu.ForMovieSets AndAlso (dgvMoviesets.RowCount > 0 OrElse tagmenu.IfNoMovieSets)) OrElse
                                                                        (tagmenu.ForTVShows AndAlso (dgvTVShows.RowCount > 0 OrElse tagmenu.IfNoTVShows))))
                End If
            ElseIf TypeOf i Is ToolStripSeparator Then
                Dim o As ToolStripSeparator = DirectCast(i, ToolStripSeparator)
                o.Visible = (mnuMainTools.DropDownItems.IndexOf(o) < mnuMainTools.DropDownItems.Count - 1)
            End If
        Next
        With Master.eSettings
            If Not String.IsNullOrEmpty(.MovieBackdropsPath) AndAlso dgvMovies.RowCount > 0 Then
                mnuMainToolsBackdrops.Enabled = True
            Else
                mnuMainToolsBackdrops.Enabled = False
            End If
        End With
        mnuMainEdit.Enabled = isEnabled
        mnuScrapeMovies.Enabled = isEnabled AndAlso dgvMovies.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.Movie
        mnuScrapeMovies.Visible = currMainTabTag.ContentType = Enums.ContentType.Movie
        mnuScrapeMoviesets.Enabled = isEnabled AndAlso dgvMoviesets.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.Movieset
        mnuScrapeMoviesets.Visible = currMainTabTag.ContentType = Enums.ContentType.Movieset
        mnuScrapeTVShows.Enabled = isEnabled AndAlso dgvTVShows.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.TV
        mnuScrapeTVShows.Visible = currMainTabTag.ContentType = Enums.ContentType.TV
        mnuUpdate.Enabled = isEnabled
        cmnuMovie.Enabled = isEnabled
        cmnuMovieSet.Enabled = isEnabled
        cmnuShow.Enabled = isEnabled
        cmnuSeason.Enabled = isEnabled
        cmnuEpisode.Enabled = isEnabled
        tcMain.Enabled = isEnabled
        btnMarkAll.Enabled = isEnabled
        btnMetaDataRefresh.Enabled = isEnabled
        btnUnmarkAll.Enabled = isEnabled
        scMain.IsSplitterFixed = Not isEnabled
        scTV.IsSplitterFixed = Not isEnabled
        scTVSeasonsEpisodes.IsSplitterFixed = Not isEnabled
        mnuMainHelp.Enabled = isEnabled
        cmnuTrayTools.Enabled = mnuMainTools.Enabled
        cmnuTrayScrapeMovies.Enabled = isEnabled AndAlso dgvMovies.RowCount > 0
        cmnuTrayScrapeMovieSets.Enabled = isEnabled AndAlso dgvMoviesets.RowCount > 0
        cmnuTrayScrapeTVShows.Enabled = isEnabled AndAlso dgvTVShows.RowCount > 0
        cmnuTrayUpdate.Enabled = isEnabled
        cmnuTraySettings.Enabled = isEnabled
        cmnuTrayExit.Enabled = isEnabled

        If withLists OrElse isEnabled Then
            dgvMovies.TabStop = isEnabled
            dgvMoviesets.TabStop = isEnabled
            dgvTVShows.TabStop = isEnabled
            dgvTVSeasons.TabStop = isEnabled
            dgvTVEpisodes.TabStop = isEnabled
            dgvMovies.Enabled = isEnabled
            dgvMoviesets.Enabled = isEnabled
            dgvTVShows.Enabled = isEnabled
            dgvTVSeasons.Enabled = isEnabled
            dgvTVEpisodes.Enabled = isEnabled
            txtSearchMovies.Enabled = isEnabled
            txtSearchMovieSets.Enabled = isEnabled
            txtSearchShows.Enabled = isEnabled
        End If
    End Sub

    Private Sub cmnuMovieSetSortMethodSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuMovieSetEditSortMethodSet.Click
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each sRow As DataGridViewRow In dgvMoviesets.SelectedRows
                Dim tmpDBMovieSet As Database.DBElement = Master.DB.Load_Movieset(Convert.ToInt64(sRow.Cells("idSet").Value))
                tmpDBMovieSet.SortMethod = CType(cmnuMovieSetEditSortMethodMethods.ComboBox.SelectedValue, Enums.SortMethod_MovieSet)
                Master.DB.Save_Movieset(tmpDBMovieSet, True, True, False, False)
                DataGridView_RefreshRow_Movieset(tmpDBMovieSet.ID)
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
        Dim currMainTabTag = MainTab_GetCurrentTag()

        With Master.eSettings
            mnuMainToolsBackdrops.Enabled = Not String.IsNullOrEmpty(.MovieBackdropsPath)

            'for future use
            mnuMainToolsClearCache.Enabled = False

            'Load source list for movies
            mnuUpdateMovies.DropDownItems.Clear()
            cmnuTrayUpdateMovies.DropDownItems.Clear()
            If Master.DB.Load_AllSources_Movie.Count > 1 Then
                mnuItem = mnuUpdateMovies.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New EventHandler(AddressOf SourceSubClick_Movie))
                mnuItem = cmnuTrayUpdateMovies.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New EventHandler(AddressOf SourceSubClick_Movie))
            End If
            For Each nSource In Master.DB.Load_AllSources_Movie
                mnuItem = mnuUpdateMovies.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), nSource.Name), Nothing, New EventHandler(AddressOf SourceSubClick_Movie))
                mnuItem.Tag = nSource.ID
                mnuItem.ForeColor = If(nSource.Exclude, Color.Gray, Color.Black)
                mnuItem = cmnuTrayUpdateMovies.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), nSource.Name), Nothing, New EventHandler(AddressOf SourceSubClick_Movie))
                mnuItem.Tag = nSource.ID
                mnuItem.ForeColor = If(nSource.Exclude, Color.Gray, Color.Black)
            Next

            'Load source list for tv shows
            mnuUpdateShows.DropDownItems.Clear()
            cmnuTrayUpdateShows.DropDownItems.Clear()
            If Master.DB.Load_AllSources_TVShow.Count > 1 Then
                mnuItem = mnuUpdateShows.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New EventHandler(AddressOf SourceSubClick_TV))
                mnuItem = cmnuTrayUpdateShows.DropDownItems.Add(Master.eLang.GetString(649, "Update All"), Nothing, New EventHandler(AddressOf SourceSubClick_TV))
            End If
            For Each nSource In Master.DB.Load_AllSources_TVShow
                mnuItem = mnuUpdateShows.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), nSource.Name), Nothing, New EventHandler(AddressOf SourceSubClick_TV))
                mnuItem.Tag = nSource.ID
                mnuItem.ForeColor = If(nSource.Exclude, Color.Gray, Color.Black)
                mnuItem = cmnuTrayUpdateShows.DropDownItems.Add(String.Format(Master.eLang.GetString(143, "Update {0} Only"), nSource.Name), Nothing, New EventHandler(AddressOf SourceSubClick_TV))
                mnuItem.Tag = nSource.ID
                mnuItem.ForeColor = If(nSource.Exclude, Color.Gray, Color.Black)
            Next

            'Load filter list DataFields for movies
            clbFilterDataFields_Movies.Items.Clear()
            clbFilterDataFields_Movies.Items.AddRange(New Object() {"Certification", "Credits", "Director", "Imdb", "MPAA", "OriginalTitle", "Outline", "Plot", "Rating", "ReleaseDate", "Runtime", "SortTitle", "Studio", "TMDB", "TMDBColID", "Tag", "Tagline", "Title", "Top250", "Trailer", "VideoSource", "Votes", "Year"})

            'Load sort methods list for moviesets
            Dim SortMethods As New Dictionary(Of String, Enums.SortMethod_MovieSet)
            SortMethods.Add(Master.eLang.GetString(278, "Year"), Enums.SortMethod_MovieSet.Year)
            SortMethods.Add(Master.eLang.GetString(21, "Title"), Enums.SortMethod_MovieSet.Title)
            cmnuMovieSetEditSortMethodMethods.ComboBox.DataSource = SortMethods.ToList
            cmnuMovieSetEditSortMethodMethods.ComboBox.DisplayMember = "Key"
            cmnuMovieSetEditSortMethodMethods.ComboBox.ValueMember = "Value"
            cmnuMovieSetEditSortMethodMethods.ComboBox.BindingContext = BindingContext

            'Load view list for movies
            Dim listViews_Movies As New Dictionary(Of String, String)
            listViews_Movies.Add(Master.eLang.GetString(786, "Default List"), "movielist")
            For Each cList As String In Master.DB.GetViewList(Enums.ContentType.Movie, True)
                listViews_Movies.Add(Regex.Replace(cList, "movie-", String.Empty).Trim, cList)
            Next
            RemoveHandler cbFilterLists_Movies.SelectedIndexChanged, AddressOf cbFilterLists_Movies_SelectedIndexChanged
            cbFilterLists_Movies.DataSource = listViews_Movies.ToList
            cbFilterLists_Movies.DisplayMember = "Key"
            cbFilterLists_Movies.ValueMember = "Value"
            cbFilterLists_Movies.SelectedValue = currList_Movies
            AddHandler cbFilterLists_Movies.SelectedIndexChanged, AddressOf cbFilterLists_Movies_SelectedIndexChanged

            'Load view list for moviesets
            Dim listViews_MovieSets As New Dictionary(Of String, String)
            listViews_MovieSets.Add(Master.eLang.GetString(786, "Default List"), "moviesetlist")
            For Each cList As String In Master.DB.GetViewList(Enums.ContentType.Movieset, True)
                listViews_MovieSets.Add(Regex.Replace(cList, "sets-", String.Empty).Trim, cList)
            Next
            RemoveHandler cbFilterLists_MovieSets.SelectedIndexChanged, AddressOf cbFilterLists_MovieSets_SelectedIndexChanged
            cbFilterLists_MovieSets.DataSource = listViews_MovieSets.ToList
            cbFilterLists_MovieSets.DisplayMember = "Key"
            cbFilterLists_MovieSets.ValueMember = "Value"
            cbFilterLists_MovieSets.SelectedValue = currList_MovieSets
            AddHandler cbFilterLists_MovieSets.SelectedIndexChanged, AddressOf cbFilterLists_MovieSets_SelectedIndexChanged

            'Load view list for tv shows
            Dim listViews_TVShows As New Dictionary(Of String, String)
            listViews_TVShows.Add(Master.eLang.GetString(786, "Default List"), "tvshowlist")
            For Each cList As String In Master.DB.GetViewList(Enums.ContentType.TVShow, True)
                listViews_TVShows.Add(Regex.Replace(cList, "tvshow-", String.Empty).Trim, cList)
            Next
            RemoveHandler cbFilterLists_Shows.SelectedIndexChanged, AddressOf cbFilterLists_Shows_SelectedIndexChanged
            cbFilterLists_Shows.DataSource = listViews_TVShows.ToList
            cbFilterLists_Shows.DisplayMember = "Key"
            cbFilterLists_Shows.ValueMember = "Value"
            cbFilterLists_Shows.SelectedValue = currList_TVShows
            AddHandler cbFilterLists_Shows.SelectedIndexChanged, AddressOf cbFilterLists_Shows_SelectedIndexChanged

            'Load language list
            mnuLanguagesLanguage.Items.Clear()
            mnuLanguagesLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguages.Languages.OrderBy(Function(f) f.Description) Select lLang.Description).ToArray)

            'MainTabs
            MainTab_SetTabs()

            'not technically a menu, but it's a good place to put it
            If ReloadFilters Then
                RemoveHandler cbFilterDataField_Movies.SelectedIndexChanged, AddressOf clbFilterDataFields_Movies_LostFocus
                cbFilterDataField_Movies.Items.Clear()
                cbFilterDataField_Movies.Items.AddRange(New Object() {Master.eLang.GetString(1291, "Is Empty"), Master.eLang.GetString(1292, "Is Not Empty")})
                cbFilterDataField_Movies.SelectedIndex = 0
                AddHandler cbFilterDataField_Movies.SelectedIndexChanged, AddressOf clbFilterDataFields_Movies_LostFocus

                'Load filter list sources for movies
                clbFilterSources_Movies.Items.Clear()
                clbFilterSources_Movies.Items.AddRange(Master.DB.GetAllSourceNames_Movie)

                'Load filter list sources for tv shows
                clbFilterSource_Shows.Items.Clear()
                clbFilterSource_Shows.Items.AddRange(Master.DB.GetAllSourceNames_TVShow)

                'Load filter list "years from" for movies
                RemoveHandler cbFilterYearFrom_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie
                cbFilterYearFrom_Movies.Items.Clear()
                cbFilterYearFrom_Movies.Items.Add(Master.eLang.All)
                cbFilterYearFrom_Movies.Items.AddRange(Master.DB.GetAllYears_Movie.Reverse.ToArray)
                cbFilterYearFrom_Movies.SelectedIndex = 0
                AddHandler cbFilterYearFrom_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie
                RemoveHandler cbFilterYearModFrom_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie
                cbFilterYearModFrom_Movies.SelectedIndex = 0
                AddHandler cbFilterYearModFrom_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie

                'Load filter list "years to" for movies
                RemoveHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie
                cbFilterYearTo_Movies.Items.Clear()
                cbFilterYearTo_Movies.Items.Add(Master.eLang.All)
                cbFilterYearTo_Movies.Items.AddRange(Master.DB.GetAllYears_Movie.Reverse.ToArray)
                cbFilterYearTo_Movies.SelectedIndex = 0
                AddHandler cbFilterYearTo_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie
                RemoveHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie
                cbFilterYearModTo_Movies.SelectedIndex = 0
                AddHandler cbFilterYearModTo_Movies.SelectedIndexChanged, AddressOf Filter_Add_Year_Movie
            End If

        End With
        mnuScrapeMovies.Enabled = (dgvMovies.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.Movie)
        mnuScrapeMovies.Visible = currMainTabTag.ContentType = Enums.ContentType.Movie
        mnuScrapeMoviesets.Enabled = (dgvMoviesets.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.Movieset)
        mnuScrapeMoviesets.Visible = currMainTabTag.ContentType = Enums.ContentType.Movieset
        mnuScrapeTVShows.Enabled = (dgvTVShows.RowCount > 0 AndAlso currMainTabTag.ContentType = Enums.ContentType.TV)
        mnuScrapeTVShows.Visible = currMainTabTag.ContentType = Enums.ContentType.TV
        cmnuTrayScrapeMovies.Enabled = dgvMovies.RowCount > 0
        cmnuTrayScrapeMovieSets.Enabled = dgvMoviesets.RowCount > 0
        cmnuTrayScrapeTVShows.Enabled = dgvTVShows.RowCount > 0
    End Sub

    Private Sub mnuMainToolsOfflineMM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsOfflineHolder.Click, cmnuTrayToolsOfflineHolder.Click
        SetControlsEnabled(False)
        'Using dOfflineHolder As New dlgOfflineHolder
        '    dOfflineHolder.ShowDialog()
        'End Using
        'LoadMedia(New Scanner.ScanOrCleanOptions With {.Movies = True, .TV = False})
        SetControlsEnabled(True)
    End Sub

    Private Sub SetStatus(ByVal sText As String)
        tslStatus.Text = sText.Replace("&", "&&")
    End Sub

    Private Function GetStatus() As String
        Return tslStatus.Text.Replace("&&", "&")
    End Function

    Sub HideLoadingSettings()
        If Not pnlLoadSettings.InvokeRequired Then
            pnlLoadSettings.Visible = False
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
        mnuMainEditSettings.Enabled = True
        pnlLoadSettings.Visible = False
        cmnuTraySettings.Enabled = True
        cmnuTrayExit.Enabled = True

        If Not dresult.DidCancel Then
            Setup(True)

            'TODO: make it more generic
            If dgvMovies.RowCount > 0 Then
                For Each column As DataGridViewColumn In dgvMovies.Columns
                    column.Visible = DataGridView_ShowColumn_Movie(column.Name)
                Next
            End If

            If dgvMoviesets.RowCount > 0 Then
                For Each column As DataGridViewColumn In dgvMoviesets.Columns
                    column.Visible = DataGridView_ShowColumn_Movieset(column.Name)
                Next
            End If

            If dgvTVEpisodes.RowCount > 0 Then
                For Each column As DataGridViewColumn In dgvTVEpisodes.Columns
                    column.Visible = DataGridView_ShowColumn__TVEpisode(column.Name)
                Next
            End If

            If dgvTVSeasons.RowCount > 0 Then
                For Each column As DataGridViewColumn In dgvTVSeasons.Columns
                    column.Visible = DataGridView_ShowColumn_TVSeason(column.Name)
                Next
            End If

            If dgvTVShows.RowCount > 0 Then
                For Each column As DataGridViewColumn In dgvTVShows.Columns
                    column.Visible = DataGridView_ShowColumn_TVShow(column.Name)
                Next
            End If

            'might as well wait for these
            While bwDownloadPic.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
            While bwDownloadGuestStarPic.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            If dresult.AnythingToDo Then
                If dresult.NeedsDBClean_Movie OrElse dresult.NeedsDBClean_TV Then
                    If MessageBox.Show(String.Format(Master.eLang.GetString(1007, "You've changed a setting that makes it necessary that the database is cleaned up. Please make sure that all sources are available!{0}{0}Should the process be continued?"), Environment.NewLine), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                        While bwLoadImages.IsBusy OrElse
                            bwMovieScraper.IsBusy OrElse
                            bwReload_Movie.IsBusy OrElse
                            bwMoviesetScraper.IsBusy OrElse
                            bwReload_Movieset.IsBusy OrElse
                            bwReload_TVShow.IsBusy OrElse
                            bwCleanDB.IsBusy
                            Application.DoEvents()
                            Threading.Thread.Sleep(50)
                        End While
                        'it's not necessary to clean the DB if we clean it anyway after DB update
                        Dim DBCleaner As New Scanner.ScanOrCleanOptions With {
                            .Movies = dresult.NeedsDBClean_Movie AndAlso Not (dresult.NeedsDBUpdate_Movie.Count > 0 AndAlso Master.eSettings.Movie.SourceSettings.CleanLibraryAfterUpdate),
                            .TV = dresult.NeedsDBClean_TV AndAlso Not (dresult.NeedsDBUpdate_TV.Count > 0 AndAlso Master.eSettings.TVShow.SourceSettings.CleanLibraryAfterUpdate)
                        }
                        CleanDB(DBCleaner)
                    End If
                End If

                If dresult.NeedsReload_Movie Then
                    Reload_All_Movie()
                End If
                If dresult.NeedsReload_Movieset Then
                    Reload_All_Movieset()
                End If
                If dresult.NeedsReload_TVEpisode OrElse dresult.NeedsReload_TVShow Then
                    Reload_All_TVShow(dresult.NeedsReload_TVEpisode)
                End If
                If dresult.NeedsDBUpdate_Movie.Count > 0 Then
                    If dresult.NeedsDBUpdate_Movie.Contains(-1) Then
                        Master.TaskManager.AddTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scan) With {
                                                   .ScanOrCleanOptions = New Scanner.ScanOrCleanOptions With {.Movies = True}
                                                   })
                    Else
                        Master.TaskManager.AddTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scan) With {
                                                   .ListOfID = dresult.NeedsDBUpdate_Movie,
                                                   .ScanOrCleanOptions = New Scanner.ScanOrCleanOptions With {.Movies = True}
                                                   })
                    End If
                End If
                If dresult.NeedsDBUpdate_TV.Count > 0 Then
                    If dresult.NeedsDBUpdate_TV.Contains(-1) Then
                        Master.TaskManager.AddTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scan) With {
                                                   .ScanOrCleanOptions = New Scanner.ScanOrCleanOptions With {.TV = True}
                                                   })
                    Else
                        Master.TaskManager.AddTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scan) With {
                                                   .ListOfID = dresult.NeedsDBUpdate_TV,
                                                   .ScanOrCleanOptions = New Scanner.ScanOrCleanOptions With {.TV = True}
                                                   })
                    End If
                End If
            End If

            If Not bwLoadImages.IsBusy AndAlso Not bwMovieScraper.IsBusy AndAlso Not bwReload_Movie.IsBusy AndAlso
                    Not bwMoviesetScraper.IsBusy AndAlso Not bwReload_Movieset.IsBusy AndAlso
                    Not bwReload_TVShow.IsBusy AndAlso Not bwCleanDB.IsBusy Then
                DataGridView_Fill_Main(True, True, True)
            End If

            SetMenus(True)

            If dresult.NeedsRestart Then
                While bwLoadImages.IsBusy OrElse
                    bwMovieScraper.IsBusy OrElse
                    bwReload_Movie.IsBusy OrElse
                    bwMoviesetScraper.IsBusy OrElse
                    bwReload_Movieset.IsBusy OrElse
                    bwReload_TVShow.IsBusy OrElse
                    bwCleanDB.IsBusy
                    Application.DoEvents()
                    Threading.Thread.Sleep(50)
                End While
                Dim dRestart As New dlgRestart
                If dRestart.ShowDialog = DialogResult.OK Then
                    Application.Restart()
                End If
            End If
        Else
            SetMenus(False)
            SetControlsEnabled(True)
        End If
    End Sub

    Private Sub mnuMainEdit_DropDownOpening(sender As Object, e As EventArgs) Handles mnuMainEdit.DropDownOpening
        mnuMainEditSettings.Enabled = Not AddonsManager.Instance.QueryAnyAddonIsBusy AndAlso Not AnyBackgroundWorkerIsBusy
    End Sub

    Private Sub cmnuTray_Opening(sender As Object, e As EventArgs) Handles cmnuTray.Opening
        cmnuTraySettings.Enabled = Not AddonsManager.Instance.QueryAnyAddonIsBusy AndAlso Not AnyBackgroundWorkerIsBusy
    End Sub

    Private Sub mnuMainEditSettings_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainEditSettings.Click, cmnuTraySettings.Click
        If Not AddonsManager.Instance.QueryAnyAddonIsBusy AndAlso Not AnyBackgroundWorkerIsBusy Then
            Try
                SetControlsEnabled(False)
                pnlLoadSettings.Visible = True

                Dim dThread As Threading.Thread = New Threading.Thread(AddressOf ShowSettings)
                dThread.SetApartmentState(Threading.ApartmentState.STA)
                dThread.Start()
            Catch ex As Exception
                mnuMainEditSettings.Enabled = True
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
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
                    lblNoInfo.Text = Master.eLang.GetString(55, "No information is available for this Movie")
                    If Not currThemeType = tType Then Theme_Apply(tType)
                Case Enums.ContentType.Movieset
                    lblNoInfo.Text = Master.eLang.GetString(1154, "No information is available for this MovieSet")
                    If Not currThemeType = tType Then Theme_Apply(tType)
                Case Enums.ContentType.TVEpisode
                    lblNoInfo.Text = Master.eLang.GetString(652, "No information is available for this Episode")
                    If Not currThemeType = tType Then Theme_Apply(tType)
                Case Enums.ContentType.TVSeason
                    lblNoInfo.Text = Master.eLang.GetString(1161, "No information is available for this Season")
                    If Not currThemeType = tType Then Theme_Apply(tType)
                Case Enums.ContentType.TVShow
                    lblNoInfo.Text = Master.eLang.GetString(651, "No information is available for this Show")
                    If Not currThemeType = tType Then Theme_Apply(tType)
            End Select
        End If

        pnlNoInfo.Visible = ShowIt
    End Sub
    ''' <summary>
    ''' Triggers the display of the Settings dialog
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowSettings()
        While AddonsManager.Instance.QueryAnyAddonIsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Using dSettings As New dlgSettings
            Invoke(New MySettingsShow(AddressOf SettingsShow), dSettings)
        End Using
    End Sub

    Private Sub SourceSubClick_Movie(ByVal sender As Object, ByVal e As EventArgs)
        Dim SourceID As Long = -1

        If DirectCast(sender, ToolStripItem).Tag IsNot Nothing Then
            SourceID = Convert.ToInt64(DirectCast(sender, ToolStripItem).Tag)
        End If

        Master.TaskManager.AddTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scan) With {
                                   .ListOfID = (New List(Of Long) From {SourceID}),
                                   .ScanOrCleanOptions = New Scanner.ScanOrCleanOptions With {.Movies = True}
                                   })
    End Sub

    Private Sub SourceSubClick_TV(ByVal sender As Object, ByVal e As EventArgs)
        Dim SourceID As Long = -1

        If DirectCast(sender, ToolStripItem).Tag IsNot Nothing Then
            SourceID = Convert.ToInt64(DirectCast(sender, ToolStripItem).Tag)
        End If

        Master.TaskManager.AddTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scan) With {
                                   .ListOfID = New List(Of Long) From {SourceID},
                                   .ScanOrCleanOptions = New Scanner.ScanOrCleanOptions With {.TV = True}
                                   })
    End Sub

    Private Sub TVSplitterMoved(sender As Object, e As SplitterEventArgs)
        'fixing TV-Splitter issues
        Manager.mSettings.MainOptions.GuiSettings.SplitterDistance_TVShow = scTV.SplitterDistance
        Manager.mSettings.MainOptions.GuiSettings.SplitterDistance_TVSeason = scTVSeasonsEpisodes.SplitterDistance
    End Sub

    Private Sub MoveInfoPanel()
        Dim iState As Integer
        Select Case currThemeType
            Case Enums.ContentType.Movie
                iState = _InfoPanelState_Movie
            Case Enums.ContentType.Movieset
                iState = _InfoPanelState_Movieset
            Case Enums.ContentType.TVEpisode
                iState = _InfoPanelState_TVEpisode
            Case Enums.ContentType.TVSeason
                iState = _InfoPanelState_TVSeason
            Case Enums.ContentType.TVShow
                iState = _InfoPanelState_TVShow
        End Select
        Select Case iState
            Case 0
                pnlInfoPanel.Height = 32
            Case 1
                pnlInfoPanel.Height = InfoPanelMidHeight
            Case 2
                pnlInfoPanel.Height = InfoPanelUpHeight
        End Select

        MoveGenres()
        MoveMPAA()

        Select Case iState
            Case 0
                If pnlInfoPanel.Height = 32 Then
                    btnDown.Enabled = False
                    btnMid.Enabled = True
                    btnUp.Enabled = True
                End If
            Case 1
                If pnlInfoPanel.Height = InfoPanelMidHeight Then
                    btnMid.Enabled = False
                    btnDown.Enabled = True
                    btnUp.Enabled = True
                End If
            Case 2
                If pnlInfoPanel.Height = InfoPanelUpHeight Then
                    btnUp.Enabled = False
                    btnDown.Enabled = True
                    btnMid.Enabled = True
                End If
        End Select
    End Sub

    Private Sub tmrLoad_Movie_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrLoad_Movie.Tick
        tmrWait_Movie.Stop()
        tmrLoad_Movie.Stop()

        If dgvMovies.SelectedRows.Count > 0 Then DataGridView_SelectRow_Movie(dgvMovies.SelectedRows(0).Index)
    End Sub

    Private Sub tmrLoad_MovieSet_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrLoad_MovieSet.Tick
        tmrWait_MovieSet.Stop()
        tmrLoad_MovieSet.Stop()

        If dgvMoviesets.SelectedRows.Count > 0 Then DataGridView_SelectRow_Movieset(dgvMoviesets.SelectedRows(0).Index)
    End Sub

    Private Sub tmrLoad_TVEpisode_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrLoad_TVEpisode.Tick
        tmrWait_TVEpisode.Stop()
        tmrLoad_TVEpisode.Stop()

        If dgvTVEpisodes.SelectedRows.Count > 0 Then DataGridView_SelectRow_TVEpisode(dgvTVEpisodes.SelectedRows(0).Index)
    End Sub

    Private Sub tmrLoad_TVSeason_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrLoad_TVSeason.Tick
        tmrWait_TVSeason.Stop()
        tmrLoad_TVSeason.Stop()

        If dgvTVSeasons.SelectedRows.Count > 0 Then DataGridView_SelectRow_TVSeason(dgvTVSeasons.SelectedRows(0).Index)
    End Sub

    Private Sub tmrLoad_TVShow_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrLoad_TVShow.Tick
        tmrWait_TVShow.Stop()
        tmrLoad_TVShow.Stop()

        If dgvTVShows.SelectedRows.Count > 0 Then DataGridView_SelectRow_TVShow(dgvTVShows.SelectedRows(0).Index)
    End Sub

    Private Sub tmrSearchWait_Movies_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrSearchWait_Movies.Tick
        tmrSearch_Movies.Enabled = False
        If prevTextSearch_Movies = currTextSearch_Movies Then
            tmrSearch_Movies.Enabled = True
        Else
            prevTextSearch_Movies = currTextSearch_Movies
        End If
    End Sub

    Private Sub tmrSearch_Movies_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrSearch_Movies.Tick
        tmrSearchWait_Movies.Enabled = False
        tmrSearch_Movies.Enabled = False
        bDoingSearch_Movies = True
        Filter_Add_SearchBar(txtSearchMovies, cbSearchMovies, Enums.ContentType.Movie)
    End Sub

    Private Sub tmrSearchWait_MovieSets_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrSearchWait_MovieSets.Tick
        tmrSearch_MovieSets.Enabled = False
        If prevTextSearch_MovieSets = currTextSearch_MovieSets Then
            tmrSearch_MovieSets.Enabled = True
        Else
            prevTextSearch_MovieSets = currTextSearch_MovieSets
        End If
    End Sub

    Private Sub tmrSearch_MovieSets_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrSearch_MovieSets.Tick
        tmrSearchWait_MovieSets.Enabled = False
        tmrSearch_MovieSets.Enabled = False
        bDoingSearch_MovieSets = True
        Filter_Add_SearchBar(txtSearchMovieSets, cbSearchMovieSets, Enums.ContentType.Movieset)
    End Sub

    Private Sub tmrSearchWait_Shows_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrSearchWait_Shows.Tick
        tmrSearch_Shows.Enabled = False
        If prevTextSearch_TVShows = currTextSearch_TVShows Then
            tmrSearch_Shows.Enabled = True
        Else
            prevTextSearch_TVShows = currTextSearch_TVShows
        End If
    End Sub

    Private Sub tmrSearch_Shows_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrSearch_Shows.Tick
        tmrSearchWait_Shows.Enabled = False
        tmrSearch_Shows.Enabled = False
        bDoingSearch_TVShows = True
        Filter_Add_SearchBar(txtSearchShows, cbSearchShows, Enums.ContentType.TVShow)
    End Sub

    Private Sub tmrWait_TVEpisode_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrWait_TVEpisode.Tick
        tmrLoad_TVSeason.Stop()
        tmrLoad_TVShow.Stop()
        tmrWait_TVSeason.Stop()
        tmrWait_TVShow.Stop()

        If Not prevRow_TVEpisode = currRow_TVEpisode Then
            prevRow_TVEpisode = currRow_TVEpisode
            tmrWait_TVEpisode.Stop()
            tmrLoad_TVEpisode.Start()
        Else
            tmrLoad_TVEpisode.Stop()
            tmrWait_TVEpisode.Stop()
        End If
    End Sub

    Private Sub tmrWait_TVSeason_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrWait_TVSeason.Tick
        tmrLoad_TVShow.Stop()
        tmrLoad_TVEpisode.Stop()
        tmrWait_TVShow.Stop()
        tmrWait_TVEpisode.Stop()

        If Not prevRow_TVSeason = currRow_TVSeason Then
            prevRow_TVSeason = currRow_TVSeason
            tmrWait_TVSeason.Stop()
            tmrLoad_TVSeason.Start()
        Else
            tmrLoad_TVSeason.Stop()
            tmrWait_TVSeason.Stop()
        End If
    End Sub

    Private Sub tmrWait_TVShow_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrWait_TVShow.Tick
        tmrLoad_TVSeason.Stop()
        tmrLoad_TVEpisode.Stop()
        tmrWait_TVSeason.Stop()
        tmrWait_TVEpisode.Stop()

        If Not prevRow_TVShow = currRow_TVShow Then
            prevRow_TVShow = currRow_TVShow
            tmrWait_TVShow.Stop()
            tmrLoad_TVShow.Start()
        Else
            tmrLoad_TVShow.Stop()
            tmrWait_TVShow.Stop()
        End If
    End Sub

    Private Sub tmrWait_Movie_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrWait_Movie.Tick
        If Not prevRow_Movie = currRow_Movie Then
            prevRow_Movie = currRow_Movie
            tmrWait_Movie.Stop()
            tmrLoad_Movie.Start()
        Else
            tmrLoad_Movie.Stop()
            tmrWait_Movie.Stop()
        End If
    End Sub

    Private Sub tmrWait_MovieSet_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrWait_MovieSet.Tick
        If Not prevRow_MovieSet = currRow_MovieSet Then
            prevRow_MovieSet = currRow_MovieSet
            tmrWait_MovieSet.Stop()
            tmrLoad_MovieSet.Start()
        Else
            tmrLoad_MovieSet.Stop()
            tmrWait_MovieSet.Stop()
        End If
    End Sub

    Private Sub mnuUpdate_ButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles mnuUpdate.ButtonClick
        Master.TaskManager.AddTask(New TaskManager.TaskItem(TaskManager.TaskItem.TaskType.Scan) With {
                                   .ScanOrCleanOptions = New Scanner.ScanOrCleanOptions With {.Movies = True, .Moviesets = True, .TV = True}
                                   })
    End Sub

    Private Sub txtFilterTag_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles txtFilterTag_Movies.Click
        pnlFilterTags_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterTag_Movies.Left + 1,
                                                       (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterTag_Movies.Top) - pnlFilterTags_Movies.Height)
        pnlFilterTags_Movies.Width = txtFilterTag_Movies.Width
        Filter_RefreshList_Tag_Movie()
        If pnlFilterTags_Movies.Visible Then
            pnlFilterTags_Movies.Visible = False
        ElseIf Not pnlFilterTags_Movies.Tag.ToString = "NO" Then
            pnlFilterTags_Movies.Tag = String.Empty
            pnlFilterTags_Movies.Visible = True
            clbFilterTags_Movies.Focus()
        Else
            pnlFilterTags_Movies.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterTag_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles txtFilterTag_Shows.Click
        pnlFilterTags_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterSpecific_Shows.Left + tblFilterSpecific_Shows.Left + tblFilterSpecificData_Shows.Left + txtFilterTag_Shows.Left + 1,
                                                       (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterSpecific_Shows.Top + tblFilterSpecific_Shows.Top + tblFilterSpecificData_Shows.Top + txtFilterTag_Shows.Top) - pnlFilterTags_Shows.Height)
        pnlFilterTags_Shows.Width = txtFilterTag_Shows.Width
        Filter_RefreshList_Tag_TVShow()
        If pnlFilterTags_Shows.Visible Then
            pnlFilterTags_Shows.Visible = False
        ElseIf Not pnlFilterTags_Shows.Tag.ToString = "NO" Then
            pnlFilterTags_Shows.Tag = String.Empty
            pnlFilterTags_Shows.Visible = True
            clbFilterTags_Shows.Focus()
        Else
            pnlFilterTags_Shows.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterGenre_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles txtFilterGenre_Movies.Click
        pnlFilterGenres_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterGenre_Movies.Left + 1,
                                                       (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterGenre_Movies.Top) - pnlFilterGenres_Movies.Height)
        pnlFilterGenres_Movies.Width = txtFilterGenre_Movies.Width
        Filter_RefreshList_Genre_Movie()
        If pnlFilterGenres_Movies.Visible Then
            pnlFilterGenres_Movies.Visible = False
        ElseIf Not pnlFilterGenres_Movies.Tag.ToString = "NO" Then
            pnlFilterGenres_Movies.Tag = String.Empty
            pnlFilterGenres_Movies.Visible = True
            clbFilterGenres_Movies.Focus()
        Else
            pnlFilterGenres_Movies.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterGenre_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles txtFilterGenre_Shows.Click
        pnlFilterGenres_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterSpecific_Shows.Left + tblFilterSpecific_Shows.Left + tblFilterSpecificData_Shows.Left + txtFilterGenre_Shows.Left + 1,
                                                       (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterSpecific_Shows.Top + tblFilterSpecific_Shows.Top + tblFilterSpecificData_Shows.Top + txtFilterGenre_Shows.Top) - pnlFilterGenres_Shows.Height)
        pnlFilterGenres_Shows.Width = txtFilterGenre_Shows.Width
        Filter_RefreshList_Genre_TVShow()
        If pnlFilterGenres_Shows.Visible Then
            pnlFilterGenres_Shows.Visible = False
        ElseIf Not pnlFilterGenres_Shows.Tag.ToString = "NO" Then
            pnlFilterGenres_Shows.Tag = String.Empty
            pnlFilterGenres_Shows.Visible = True
            clbFilterGenres_Shows.Focus()
        Else
            pnlFilterGenres_Shows.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterCountry_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles txtFilterCountry_Movies.Click
        pnlFilterCountries_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterCountry_Movies.Left + 1,
                                                       (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterCountry_Movies.Top) - pnlFilterCountries_Movies.Height)
        pnlFilterCountries_Movies.Width = txtFilterCountry_Movies.Width
        Filter_RefreshList_Country_Movie()
        If pnlFilterCountries_Movies.Visible Then
            pnlFilterCountries_Movies.Visible = False
        ElseIf Not pnlFilterCountries_Movies.Tag.ToString = "NO" Then
            pnlFilterCountries_Movies.Tag = String.Empty
            pnlFilterCountries_Movies.Visible = True
            clbFilterCountries_Movies.Focus()
        Else
            pnlFilterCountries_Movies.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterDataField_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles txtFilterDataField_Movies.Click
        pnlFilterDataFields_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + gbFilterDataField_Movies.Left + tblFilterDataField_Movies.Left + txtFilterDataField_Movies.Left + 1,
                                                        (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + gbFilterDataField_Movies.Top + tblFilterDataField_Movies.Top + txtFilterDataField_Movies.Top) - pnlFilterDataFields_Movies.Height)
        pnlFilterDataFields_Movies.Width = txtFilterDataField_Movies.Width
        If pnlFilterDataFields_Movies.Visible Then
            pnlFilterDataFields_Movies.Visible = False
        ElseIf Not pnlFilterDataFields_Movies.Tag.ToString = "NO" Then
            pnlFilterDataFields_Movies.Tag = String.Empty
            pnlFilterDataFields_Movies.Visible = True
            clbFilterDataFields_Movies.Focus()
        Else
            pnlFilterDataFields_Movies.Tag = String.Empty
        End If
    End Sub

    Private Sub btnFilterMissing_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterMissing_Movies.Click
        pnlFilterMissingItems_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterGeneral_Movies.Left + tblFilterGeneral_Movies.Left + btnFilterMissing_Movies.Left + 1,
                                                       (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterGeneral_Movies.Top + tblFilterGeneral_Movies.Top + btnFilterMissing_Movies.Top) - pnlFilterMissingItems_Movies.Height)
        If pnlFilterMissingItems_Movies.Visible Then
            pnlFilterMissingItems_Movies.Visible = False
        Else
            pnlFilterMissingItems_Movies.Visible = True
        End If
    End Sub

    Private Sub btnFilterMissing_MovieSets_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterMissing_MovieSets.Click
        pnlFilterMissingItems_MovieSets.Location = New Point(pnlFilter_MovieSets.Left + tblFilter_MovieSets.Left + gbFilterGeneral_MovieSets.Left + tblFilterGeneral_MovieSets.Left + btnFilterMissing_MovieSets.Left + 1,
                                                       (pnlFilter_MovieSets.Top + tblFilter_MovieSets.Top + gbFilterGeneral_MovieSets.Top + tblFilterGeneral_MovieSets.Top + btnFilterMissing_MovieSets.Top) - pnlFilterMissingItems_MovieSets.Height)
        If pnlFilterMissingItems_MovieSets.Visible Then
            pnlFilterMissingItems_MovieSets.Visible = False
        Else
            pnlFilterMissingItems_MovieSets.Visible = True
        End If
    End Sub

    Private Sub btnFilterMissing_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilterMissing_Shows.Click
        pnlFilterMissingItems_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterGeneral_Shows.Left + tblFilterGeneral_Shows.Left + btnFilterMissing_Shows.Left + 1,
                                                       (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterGeneral_Shows.Top + tblFilterGeneral_Shows.Top + btnFilterMissing_Shows.Top) - pnlFilterMissingItems_Shows.Height)
        If pnlFilterMissingItems_Shows.Visible Then
            pnlFilterMissingItems_Shows.Visible = False
        Else
            pnlFilterMissingItems_Shows.Visible = True
        End If
    End Sub

    Private Sub txtFilterSource_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles txtFilterSource_Movies.Click
        pnlFilterSources_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterSource_Movies.Left + 1,
                                                       (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterSource_Movies.Top) - pnlFilterSources_Movies.Height)
        pnlFilterSources_Movies.Width = txtFilterSource_Movies.Width
        If pnlFilterSources_Movies.Visible Then
            pnlFilterSources_Movies.Visible = False
        ElseIf Not pnlFilterSources_Movies.Tag.ToString = "NO" Then
            pnlFilterSources_Movies.Tag = String.Empty
            pnlFilterSources_Movies.Visible = True
            clbFilterSources_Movies.Focus()
        Else
            pnlFilterSources_Movies.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterSource_Shows_Click(ByVal sender As Object, ByVal e As EventArgs) Handles txtFilterSource_Shows.Click
        pnlFilterSources_Shows.Location = New Point(pnlFilter_Shows.Left + tblFilter_Shows.Left + gbFilterSpecific_Shows.Left + tblFilterSpecific_Shows.Left + tblFilterSpecificData_Shows.Left + txtFilterSource_Shows.Left + 1,
                                                       (pnlFilter_Shows.Top + tblFilter_Shows.Top + gbFilterSpecific_Shows.Top + tblFilterSpecific_Shows.Top + tblFilterSpecificData_Shows.Top + txtFilterSource_Shows.Top) - pnlFilterSources_Shows.Height)
        pnlFilterSources_Shows.Width = txtFilterSource_Shows.Width
        If pnlFilterSources_Shows.Visible Then
            pnlFilterSources_Shows.Visible = False
        ElseIf Not pnlFilterSources_Shows.Tag.ToString = "NO" Then
            pnlFilterSources_Shows.Tag = String.Empty
            pnlFilterSources_Shows.Visible = True
            clbFilterSource_Shows.Focus()
        Else
            pnlFilterSources_Shows.Tag = String.Empty
        End If
    End Sub

    Private Sub txtFilterVideoSource_Movies_Click(ByVal sender As Object, ByVal e As EventArgs) Handles txtFilterVideoSource_Movies.Click
        pnlFilterVideoSources_Movies.Location = New Point(pnlFilter_Movies.Left + tblFilter_Movies.Left + gbFilterSpecific_Movies.Left + tblFilterSpecific_Movies.Left + tblFilterSpecificData_Movies.Left + txtFilterVideoSource_Movies.Left + 1,
                                                       (pnlFilter_Movies.Top + tblFilter_Movies.Top + gbFilterSpecific_Movies.Top + tblFilterSpecific_Movies.Top + tblFilterSpecificData_Movies.Top + txtFilterVideoSource_Movies.Top) - pnlFilterVideoSources_Movies.Height)
        pnlFilterVideoSources_Movies.Width = txtFilterVideoSource_Movies.Width
        Filter_Refresh_Videosource_Movie()
        If pnlFilterVideoSources_Movies.Visible Then
            pnlFilterVideoSources_Movies.Visible = False
        ElseIf Not pnlFilterVideoSources_Movies.Tag.ToString = "NO" Then
            pnlFilterVideoSources_Movies.Tag = String.Empty
            pnlFilterVideoSources_Movies.Visible = True
            clbFilterVideoSources_Movies.Focus()
        Else
            pnlFilterVideoSources_Movies.Tag = String.Empty
        End If
    End Sub

    Private Sub txtSearchMovies_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSearchMovies.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            dgvMovies.Focus()
        End If
    End Sub

    Private Sub txtSearchMovies_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSearchMovies.TextChanged
        currTextSearch_Movies = txtSearchMovies.Text

        tmrSearchWait_Movies.Enabled = False
        tmrSearch_Movies.Enabled = False
        tmrSearchWait_Movies.Enabled = True
    End Sub

    Private Sub txtSearchMovieSets_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSearchMovieSets.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            dgvMoviesets.Focus()
        End If
    End Sub

    Private Sub txtSearchMovieSets_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSearchMovieSets.TextChanged
        currTextSearch_MovieSets = txtSearchMovieSets.Text

        tmrSearchWait_MovieSets.Enabled = False
        tmrSearch_MovieSets.Enabled = False
        tmrSearchWait_MovieSets.Enabled = True
    End Sub

    Private Sub txtSearchShows_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSearchShows.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            dgvTVShows.Focus()
        End If
    End Sub

    Private Sub txtSearchShows_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSearchShows.TextChanged
        currTextSearch_TVShows = txtSearchShows.Text

        tmrSearchWait_Shows.Enabled = False
        tmrSearch_Shows.Enabled = False
        tmrSearchWait_Shows.Enabled = True
    End Sub

    Private Sub VersionsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainHelpVersions.Click
        AddonsManager.Instance.GetVersions()
    End Sub

    Private Sub mnuMainHelpBugTracker_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainHelpBugTracker.Click
        Functions.Launch(My.Resources.urlEmberBugTracker)
    End Sub

    Private Sub mnuMainHelpWiki_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainHelpWiki.Click
        Functions.Launch(My.Resources.urlEmberWiki)
    End Sub

    Private Sub mnuMainHelpForumEng_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainHelpForumEng.Click
        Functions.Launch(My.Resources.urlForumEng)
    End Sub

    Private Sub mnuMainHelpForumGer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainHelpForumGer.Click
        Functions.Launch(My.Resources.urlForumGer)
    End Sub

    Private Sub mnuVersion_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuVersion.Click
        Functions.Launch(My.Resources.urlReleaseThread)
    End Sub

    Private Sub tmrAppExit_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrAppExit.Tick
        tmrAppExit.Enabled = False
        Close()
    End Sub

    Private Sub CheckUpdatesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainHelpUpdate.Click
        If Functions.CheckNeedUpdate() Then
            Using dNewVer As New dlgNewVersion
                If dNewVer.ShowDialog() = DialogResult.Abort Then
                    tmrAppExit.Enabled = True
                    CloseApp = True
                End If
            End Using
        Else
            MessageBox.Show(Master.eLang.GetString(851, "No Updates at this time"), "Updates", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub lblIMDBHeader_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblIMDBHeader.Click
        If lblIMDBHeader.Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(lblIMDBHeader.Tag.ToString) Then
            Functions.Launch(lblIMDBHeader.Tag.ToString)
        End If
    End Sub

    Private Sub lblIMDBHeader_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles lblIMDBHeader.MouseEnter
        If lblIMDBHeader.Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(lblIMDBHeader.Tag.ToString) Then
            Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub lblIMDBHeader_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles lblIMDBHeader.MouseLeave
        Cursor = Cursors.Default
    End Sub

    Private Sub lblTMDBHeader_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblTMDBHeader.Click
        If lblTMDBHeader.Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(lblTMDBHeader.Tag.ToString) Then
            Functions.Launch(lblTMDBHeader.Tag.ToString)
        End If
    End Sub

    Private Sub lblTMDBHeader_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles lblTMDBHeader.MouseEnter
        If lblTMDBHeader.Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(lblTMDBHeader.Tag.ToString) Then
            Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub lblTMDBHeader_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles lblTMDBHeader.MouseLeave
        Cursor = Cursors.Default
    End Sub

    Private Sub lblTVDBHeader_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblTVDBHeader.Click
        If lblTVDBHeader.Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(lblTVDBHeader.Tag.ToString) Then
            Functions.Launch(lblTVDBHeader.Tag.ToString)
        End If
    End Sub

    Private Sub lblTVDBHeader_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles lblTVDBHeader.MouseEnter
        If lblTVDBHeader.Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(lblTVDBHeader.Tag.ToString) Then
            Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub lblTVDBHeader_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles lblTVDBHeader.MouseLeave
        Cursor = Cursors.Default
    End Sub

    Private Sub pbStudio_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles pbStudio.MouseEnter
        If Not Manager.mSettings.MainOptions.GuiSettings.DisplayStudioName AndAlso pbStudio.Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(pbStudio.Tag.ToString) Then
            lblStudio.Text = pbStudio.Tag.ToString
        End If
    End Sub

    Private Sub pbStudio_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStudio.MouseLeave
        If Not Manager.mSettings.MainOptions.GuiSettings.DisplayStudioName Then lblStudio.Text = String.Empty
    End Sub

    Private Sub tmrKeyBuffer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrKeyBuffer.Tick
        tmrKeyBuffer.Enabled = False
        KeyBuffer = String.Empty
    End Sub

    Private Sub bwCheckVersion_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCheckVersion.DoWork
        Try
            'Pull Assembly version info from current Ember repo on github
            Dim HTML As String = HTTP.GetLatestVersionInfo

            If Not String.IsNullOrEmpty(HTML) Then
                'Example: AssemblyFileVersion("1.3.0.18")>
                Dim regVersion = Regex.Match(HTML, "AssemblyFileVersion\(""(\d*)\.(\d*)\.(\d*)(?>\.(\d*))?""\)>", RegexOptions.Singleline)
                'check to see if at least one entry was found
                If regVersion.Success Then
                    'just use the first match if more are found and compare with running Ember Version
                    Dim iMajor As Integer
                    Dim iMinor As Integer
                    Dim iBuild As Integer
                    Dim iRevision As Integer

                    Integer.TryParse(regVersion.Groups(1).Value, iMajor)
                    Integer.TryParse(regVersion.Groups(2).Value, iMinor)
                    Integer.TryParse(regVersion.Groups(3).Value, iBuild)
                    Integer.TryParse(regVersion.Groups(4).Value, iRevision)

                    If iMajor > My.Application.Info.Version.Major OrElse
                       iMinor > My.Application.Info.Version.Minor OrElse
                       iBuild > My.Application.Info.Version.Build Then
                        'means that running Ember version is outdated!
                        Invoke(New UpdatemnuVersionDel(AddressOf UpdatemnuVersion), String.Format("{0} - {1}", Master.VersionAsString, Master.eLang.GetString(1009, "New version available")), Color.DarkRed)
                    Else
                        'Ember already up to date!
                        Invoke(New UpdatemnuVersionDel(AddressOf UpdatemnuVersion), Master.VersionAsString, Color.DarkGreen)
                    End If
                End If
                'if no github query possible, than simply display Ember version on form
            Else
                Invoke(New UpdatemnuVersionDel(AddressOf UpdatemnuVersion), Master.VersionAsString, Color.DarkBlue)
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
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

        Dim ContentType As Enums.ContentType
        Dim ID As Long
        Dim IsTV As Boolean
        Dim DBElement As Database.DBElement
        Dim Path As String
        Dim pURL As String
        Dim ScrapeList As List(Of ScrapeItem)
        Dim ScrapeOptions As Structures.ScrapeOptions
        Dim ScrapeType As Enums.ScrapeType
        Dim Season As Integer
        Dim setEnabled As Boolean
        Dim SetName As String
        Dim Trigger As Boolean
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
        Dim ScrapeOptions As Structures.ScrapeOptions
        Dim Path As String
        Dim Result As Image
        Dim ScrapeType As Enums.ScrapeType
        Dim setEnabled As Boolean
        Dim Cancelled As Boolean

#End Region 'Fields

    End Structure

    Class MovieInSetPoster

#Region "Properties"

        Public Property MoviePoster() As Image = Nothing

        Public Property MovieTitle() As String = String.Empty

        Public Property MovieYear() As Integer = 0

#End Region 'Properties 

    End Class

    Structure ScrapeItem

#Region "Fields"

        Dim DataRow As DataRow
        Dim ScrapeModifiers As Structures.ScrapeModifiers

#End Region 'Fields

    End Structure

    Structure Task

#Region "Fields"

        Dim mType As Enums.AddonEventType
        Dim Params As List(Of Object)

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class