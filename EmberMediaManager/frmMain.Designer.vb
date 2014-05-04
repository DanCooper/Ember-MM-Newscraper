<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BottomToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.TopToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.RightToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.LeftToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.ContentPanel = New System.Windows.Forms.ToolStripContentPanel()
        Me.mnuMainFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainFileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainEditSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainHelpWiki = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator18 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMainHelpVersions = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainHelpUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator19 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMainHelpAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.tslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsSpring = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslLoading = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tspbLoading = New System.Windows.Forms.ToolStripProgressBar()
        Me.tmrAni = New System.Windows.Forms.Timer(Me.components)
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.mnuMainTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsCleanFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsSortFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsBackdrops = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMainToolsSetsManager = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsOfflineHolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMainToolsClearCache = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsReloadMovies = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsCleanDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMainDonate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainError = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.pnlFilterGenre = New System.Windows.Forms.Panel()
        Me.clbFilterGenres = New System.Windows.Forms.CheckedListBox()
        Me.lblGFilClose = New System.Windows.Forms.Label()
        Me.lblFilterGenres = New System.Windows.Forms.Label()
        Me.pnlFilterSource = New System.Windows.Forms.Panel()
        Me.lblSFilClose = New System.Windows.Forms.Label()
        Me.lblFilterSources = New System.Windows.Forms.Label()
        Me.clbFilterSource = New System.Windows.Forms.CheckedListBox()
        Me.dgvMovies = New System.Windows.Forms.DataGridView()
        Me.cmnuMovie = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuMovieTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieReload = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieLock = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieWatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieEditMetaData = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieGenres = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieGenresTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieGenresGenre = New System.Windows.Forms.ToolStripComboBox()
        Me.cmnuMovieGenresAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieGenresSet = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieGenresRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSep = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieRescrape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSel = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoMetaData = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskMetaData = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieChange = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieRemoveFromDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieRemoveFromDisk = New System.Windows.Forms.ToolStripMenuItem()
        Me.scTV = New System.Windows.Forms.SplitContainer()
        Me.dgvTVShows = New System.Windows.Forms.DataGridView()
        Me.cmnuShow = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuShowTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuShowReload = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowLock = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowWatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuShowEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuShowRescrape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowRefresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowChange = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuShowOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuShowRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowRemoveFromDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowRemoveFromDisk = New System.Windows.Forms.ToolStripMenuItem()
        Me.scTVSeasonsEpisodes = New System.Windows.Forms.SplitContainer()
        Me.dgvTVSeasons = New System.Windows.Forms.DataGridView()
        Me.cmnuSeason = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuSeasonTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuSeasonReload = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonLock = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonWatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator16 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuSeasonChangeImages = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuSeasonRescrape = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuSeasonOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator27 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuSeasonRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuRemoveSeasonFromDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonRemoveFromDisk = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgvTVEpisodes = New System.Windows.Forms.DataGridView()
        Me.cmnuEpisode = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuEpisodeTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuEpisodeReload = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeLock = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeWatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuEpisodeEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuEpisodeRescrape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeChange = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuEpisodeOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator28 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuEpisodeRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeRemoveFromDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeRemoveFromDisk = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlListTop = New System.Windows.Forms.Panel()
        Me.btnMarkAll = New System.Windows.Forms.Button()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.cbSearch = New System.Windows.Forms.ComboBox()
        Me.picSearch = New System.Windows.Forms.PictureBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.tpMovies = New System.Windows.Forms.TabPage()
        Me.tpShows = New System.Windows.Forms.TabPage()
        Me.pnlFilter = New System.Windows.Forms.Panel()
        Me.gbSort = New System.Windows.Forms.GroupBox()
        Me.btnIMDBRating = New System.Windows.Forms.Button()
        Me.btnSortTitle = New System.Windows.Forms.Button()
        Me.btnSortDate = New System.Windows.Forms.Button()
        Me.btnClearFilters = New System.Windows.Forms.Button()
        Me.gbFilterGeneral = New System.Windows.Forms.GroupBox()
        Me.chkFilterTolerance = New System.Windows.Forms.CheckBox()
        Me.chkFilterMissing = New System.Windows.Forms.CheckBox()
        Me.chkFilterDupe = New System.Windows.Forms.CheckBox()
        Me.gbFilterSpecific = New System.Windows.Forms.GroupBox()
        Me.txtFilterSource = New System.Windows.Forms.TextBox()
        Me.lblFilterFileSource = New System.Windows.Forms.Label()
        Me.cbFilterFileSource = New System.Windows.Forms.ComboBox()
        Me.chkFilterLock = New System.Windows.Forms.CheckBox()
        Me.gbFilterModifier = New System.Windows.Forms.GroupBox()
        Me.rbFilterAnd = New System.Windows.Forms.RadioButton()
        Me.rbFilterOr = New System.Windows.Forms.RadioButton()
        Me.chkFilterNew = New System.Windows.Forms.CheckBox()
        Me.cbFilterYear = New System.Windows.Forms.ComboBox()
        Me.chkFilterMark = New System.Windows.Forms.CheckBox()
        Me.cbFilterYearMod = New System.Windows.Forms.ComboBox()
        Me.lblFilterYear = New System.Windows.Forms.Label()
        Me.txtFilterGenre = New System.Windows.Forms.TextBox()
        Me.lblFilterSource = New System.Windows.Forms.Label()
        Me.lblFilterGenre = New System.Windows.Forms.Label()
        Me.btnFilterDown = New System.Windows.Forms.Button()
        Me.btnFilterUp = New System.Windows.Forms.Button()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlInfoIcons = New System.Windows.Forms.Panel()
        Me.lblOriginalTitle = New System.Windows.Forms.Label()
        Me.lblStudio = New System.Windows.Forms.Label()
        Me.pbVType = New System.Windows.Forms.PictureBox()
        Me.pbStudio = New System.Windows.Forms.PictureBox()
        Me.pbVideo = New System.Windows.Forms.PictureBox()
        Me.pbAudio = New System.Windows.Forms.PictureBox()
        Me.pbResolution = New System.Windows.Forms.PictureBox()
        Me.pbChannels = New System.Windows.Forms.PictureBox()
        Me.lblRuntime = New System.Windows.Forms.Label()
        Me.lblTagline = New System.Windows.Forms.Label()
        Me.pnlRating = New System.Windows.Forms.Panel()
        Me.pbStar5 = New System.Windows.Forms.PictureBox()
        Me.pbStar4 = New System.Windows.Forms.PictureBox()
        Me.pbStar3 = New System.Windows.Forms.PictureBox()
        Me.pbStar2 = New System.Windows.Forms.PictureBox()
        Me.pbStar1 = New System.Windows.Forms.PictureBox()
        Me.lblVotes = New System.Windows.Forms.Label()
        Me.pnlCancel = New System.Windows.Forms.Panel()
        Me.prbCanceling = New System.Windows.Forms.ProgressBar()
        Me.lblCanceling = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlAllSeason = New System.Windows.Forms.Panel()
        Me.pbAllSeason = New System.Windows.Forms.PictureBox()
        Me.pbAllSeasonCache = New System.Windows.Forms.PictureBox()
        Me.pnlNoInfo = New System.Windows.Forms.Panel()
        Me.pnlNoInfoBG = New System.Windows.Forms.Panel()
        Me.pbNoInfo = New System.Windows.Forms.PictureBox()
        Me.lblNoInfo = New System.Windows.Forms.Label()
        Me.pnlInfoPanel = New System.Windows.Forms.Panel()
        Me.txtCerts = New System.Windows.Forms.TextBox()
        Me.lblCertsHeader = New System.Windows.Forms.Label()
        Me.lblReleaseDate = New System.Windows.Forms.Label()
        Me.lblReleaseDateHeader = New System.Windows.Forms.Label()
        Me.btnMid = New System.Windows.Forms.Button()
        Me.pbMILoading = New System.Windows.Forms.PictureBox()
        Me.btnMetaDataRefresh = New System.Windows.Forms.Button()
        Me.lblMetaDataHeader = New System.Windows.Forms.Label()
        Me.txtMetaData = New System.Windows.Forms.TextBox()
        Me.btnPlay = New System.Windows.Forms.Button()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.lblFilePathHeader = New System.Windows.Forms.Label()
        Me.txtIMDBID = New System.Windows.Forms.TextBox()
        Me.lblIMDBHeader = New System.Windows.Forms.Label()
        Me.lblDirector = New System.Windows.Forms.Label()
        Me.lblDirectorHeader = New System.Windows.Forms.Label()
        Me.pnlActors = New System.Windows.Forms.Panel()
        Me.pbActLoad = New System.Windows.Forms.PictureBox()
        Me.lstActors = New System.Windows.Forms.ListBox()
        Me.pbActors = New System.Windows.Forms.PictureBox()
        Me.lblActorsHeader = New System.Windows.Forms.Label()
        Me.lblOutlineHeader = New System.Windows.Forms.Label()
        Me.txtOutline = New System.Windows.Forms.TextBox()
        Me.pnlTop250 = New System.Windows.Forms.Panel()
        Me.lblTop250 = New System.Windows.Forms.Label()
        Me.pbTop250 = New System.Windows.Forms.PictureBox()
        Me.lblPlotHeader = New System.Windows.Forms.Label()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.lblInfoPanelHeader = New System.Windows.Forms.Label()
        Me.pnlPoster = New System.Windows.Forms.Panel()
        Me.pbPoster = New System.Windows.Forms.PictureBox()
        Me.pbPosterCache = New System.Windows.Forms.PictureBox()
        Me.pbFanartSmallCache = New System.Windows.Forms.PictureBox()
        Me.pnlFanartSmall = New System.Windows.Forms.Panel()
        Me.pbFanartSmall = New System.Windows.Forms.PictureBox()
        Me.pnlMPAA = New System.Windows.Forms.Panel()
        Me.pbMPAA = New System.Windows.Forms.PictureBox()
        Me.pbFanartCache = New System.Windows.Forms.PictureBox()
        Me.pbFanart = New System.Windows.Forms.PictureBox()
        Me.tsMain = New System.Windows.Forms.ToolStrip()
        Me.tsbAutoPilot = New System.Windows.Forms.ToolStripDropDownButton()
        Me.mnuAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAllSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMiss = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMissSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilter = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFilterSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCustom = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUpdate = New System.Windows.Forms.ToolStripSplitButton()
        Me.mnuUpdateMovies = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUpdateShows = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbMediaCenters = New System.Windows.Forms.ToolStripSplitButton()
        Me.ilColumnIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.tmrWait = New System.Windows.Forms.Timer(Me.components)
        Me.tmrLoad = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearchWait = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearch = New System.Windows.Forms.Timer(Me.components)
        Me.tmrFilterAni = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTips = New System.Windows.Forms.ToolTip(Me.components)
        Me.tmrWaitShow = New System.Windows.Forms.Timer(Me.components)
        Me.tmrLoadShow = New System.Windows.Forms.Timer(Me.components)
        Me.tmrWaitSeason = New System.Windows.Forms.Timer(Me.components)
        Me.tmrLoadSeason = New System.Windows.Forms.Timer(Me.components)
        Me.tmrWaitEp = New System.Windows.Forms.Timer(Me.components)
        Me.tmrLoadEp = New System.Windows.Forms.Timer(Me.components)
        Me.cmnuTray = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuTrayTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator21 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuTrayUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayUpdateMovies = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayUpdateShows = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayScrape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoMetaData = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayAllSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMiss = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMissSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayNewSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMarkSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilter = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayFilterSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayCustom = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMediaCenters = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator23 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuTrayTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsCleanFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsSortFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsBackdrops = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator24 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuTrayToolsSetsManager = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsOfflineHolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator25 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuTrayToolsClearCache = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsReloadMovies = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsCleanDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator26 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator22 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuTraySettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuTrayExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlLoadSettingsBG = New System.Windows.Forms.Panel()
        Me.pbLoadSettings = New System.Windows.Forms.PictureBox()
        Me.lblLoadSettings = New System.Windows.Forms.Label()
        Me.prbLoadSettings = New System.Windows.Forms.ProgressBar()
        Me.pnlLoadSettings = New System.Windows.Forms.Panel()
        Me.tmrAppExit = New System.Windows.Forms.Timer(Me.components)
        Me.tmrKeyBuffer = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip.SuspendLayout()
        Me.mnuMain.SuspendLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.pnlFilterGenre.SuspendLayout()
        Me.pnlFilterSource.SuspendLayout()
        CType(Me.dgvMovies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmnuMovie.SuspendLayout()
        CType(Me.scTV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scTV.Panel1.SuspendLayout()
        Me.scTV.Panel2.SuspendLayout()
        Me.scTV.SuspendLayout()
        CType(Me.dgvTVShows, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmnuShow.SuspendLayout()
        CType(Me.scTVSeasonsEpisodes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scTVSeasonsEpisodes.Panel1.SuspendLayout()
        Me.scTVSeasonsEpisodes.Panel2.SuspendLayout()
        Me.scTVSeasonsEpisodes.SuspendLayout()
        CType(Me.dgvTVSeasons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmnuSeason.SuspendLayout()
        CType(Me.dgvTVEpisodes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmnuEpisode.SuspendLayout()
        Me.pnlListTop.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.picSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcMain.SuspendLayout()
        Me.pnlFilter.SuspendLayout()
        Me.gbSort.SuspendLayout()
        Me.gbFilterGeneral.SuspendLayout()
        Me.gbFilterSpecific.SuspendLayout()
        Me.gbFilterModifier.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlInfoIcons.SuspendLayout()
        CType(Me.pbVType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStudio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbVideo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbAudio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbResolution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbChannels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRating.SuspendLayout()
        CType(Me.pbStar5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCancel.SuspendLayout()
        Me.pnlAllSeason.SuspendLayout()
        CType(Me.pbAllSeason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbAllSeasonCache, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlNoInfo.SuspendLayout()
        Me.pnlNoInfoBG.SuspendLayout()
        CType(Me.pbNoInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInfoPanel.SuspendLayout()
        CType(Me.pbMILoading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlActors.SuspendLayout()
        CType(Me.pbActLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbActors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop250.SuspendLayout()
        CType(Me.pbTop250, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPoster.SuspendLayout()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbPosterCache, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFanartSmallCache, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFanartSmall.SuspendLayout()
        CType(Me.pbFanartSmall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMPAA.SuspendLayout()
        CType(Me.pbMPAA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFanartCache, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tsMain.SuspendLayout()
        Me.cmnuTray.SuspendLayout()
        Me.pnlLoadSettingsBG.SuspendLayout()
        CType(Me.pbLoadSettings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLoadSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'BottomToolStripPanel
        '
        Me.BottomToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.BottomToolStripPanel.Name = "BottomToolStripPanel"
        Me.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.BottomToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.BottomToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'TopToolStripPanel
        '
        Me.TopToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopToolStripPanel.Name = "TopToolStripPanel"
        Me.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.TopToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.TopToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'RightToolStripPanel
        '
        Me.RightToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.RightToolStripPanel.Name = "RightToolStripPanel"
        Me.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.RightToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.RightToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'LeftToolStripPanel
        '
        Me.LeftToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.LeftToolStripPanel.Name = "LeftToolStripPanel"
        Me.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.LeftToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LeftToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'ContentPanel
        '
        Me.ContentPanel.Size = New System.Drawing.Size(150, 175)
        '
        'mnuMainFile
        '
        Me.mnuMainFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainFileExit})
        Me.mnuMainFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.mnuMainFile.Name = "mnuMainFile"
        Me.mnuMainFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuMainFile.Text = "&File"
        '
        'mnuMainFileExit
        '
        Me.mnuMainFileExit.Image = CType(resources.GetObject("mnuMainFileExit.Image"), System.Drawing.Image)
        Me.mnuMainFileExit.Name = "mnuMainFileExit"
        Me.mnuMainFileExit.Size = New System.Drawing.Size(92, 22)
        Me.mnuMainFileExit.Text = "E&xit"
        '
        'mnuMainEdit
        '
        Me.mnuMainEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainEditSettings})
        Me.mnuMainEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.mnuMainEdit.Name = "mnuMainEdit"
        Me.mnuMainEdit.Size = New System.Drawing.Size(39, 20)
        Me.mnuMainEdit.Text = "&Edit"
        '
        'mnuMainEditSettings
        '
        Me.mnuMainEditSettings.Image = CType(resources.GetObject("mnuMainEditSettings.Image"), System.Drawing.Image)
        Me.mnuMainEditSettings.Name = "mnuMainEditSettings"
        Me.mnuMainEditSettings.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainEditSettings.Text = "&Settings..."
        '
        'mnuMainHelp
        '
        Me.mnuMainHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainHelpWiki, Me.ToolStripSeparator18, Me.mnuMainHelpVersions, Me.mnuMainHelpUpdate, Me.ToolStripSeparator19, Me.mnuMainHelpAbout})
        Me.mnuMainHelp.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.mnuMainHelp.Name = "mnuMainHelp"
        Me.mnuMainHelp.Size = New System.Drawing.Size(43, 20)
        Me.mnuMainHelp.Text = "&Help"
        '
        'mnuMainHelpWiki
        '
        Me.mnuMainHelpWiki.Image = CType(resources.GetObject("mnuMainHelpWiki.Image"), System.Drawing.Image)
        Me.mnuMainHelpWiki.Name = "mnuMainHelpWiki"
        Me.mnuMainHelpWiki.Size = New System.Drawing.Size(185, 22)
        Me.mnuMainHelpWiki.Text = "EmberMM.com &Wiki..."
        Me.mnuMainHelpWiki.Visible = False
        '
        'ToolStripSeparator18
        '
        Me.ToolStripSeparator18.Name = "ToolStripSeparator18"
        Me.ToolStripSeparator18.Size = New System.Drawing.Size(182, 6)
        Me.ToolStripSeparator18.Visible = False
        '
        'mnuMainHelpVersions
        '
        Me.mnuMainHelpVersions.Image = CType(resources.GetObject("mnuMainHelpVersions.Image"), System.Drawing.Image)
        Me.mnuMainHelpVersions.Name = "mnuMainHelpVersions"
        Me.mnuMainHelpVersions.Size = New System.Drawing.Size(185, 22)
        Me.mnuMainHelpVersions.Text = "&Versions..."
        '
        'mnuMainHelpUpdate
        '
        Me.mnuMainHelpUpdate.Enabled = False
        Me.mnuMainHelpUpdate.Image = CType(resources.GetObject("mnuMainHelpUpdate.Image"), System.Drawing.Image)
        Me.mnuMainHelpUpdate.Name = "mnuMainHelpUpdate"
        Me.mnuMainHelpUpdate.Size = New System.Drawing.Size(185, 22)
        Me.mnuMainHelpUpdate.Text = "Check For Updates"
        Me.mnuMainHelpUpdate.Visible = False
        '
        'ToolStripSeparator19
        '
        Me.ToolStripSeparator19.Name = "ToolStripSeparator19"
        Me.ToolStripSeparator19.Size = New System.Drawing.Size(182, 6)
        '
        'mnuMainHelpAbout
        '
        Me.mnuMainHelpAbout.Image = CType(resources.GetObject("mnuMainHelpAbout.Image"), System.Drawing.Image)
        Me.mnuMainHelpAbout.Name = "mnuMainHelpAbout"
        Me.mnuMainHelpAbout.Size = New System.Drawing.Size(185, 22)
        Me.mnuMainHelpAbout.Text = "&About..."
        '
        'StatusStrip
        '
        Me.StatusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslStatus, Me.tsSpring, Me.tslLoading, Me.tspbLoading})
        Me.StatusStrip.Location = New System.Drawing.Point(5, 711)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1344, 22)
        Me.StatusStrip.TabIndex = 6
        Me.StatusStrip.Text = "StatusStrip"
        '
        'tslStatus
        '
        Me.tslStatus.Name = "tslStatus"
        Me.tslStatus.Size = New System.Drawing.Size(0, 17)
        Me.tslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tsSpring
        '
        Me.tsSpring.Name = "tsSpring"
        Me.tsSpring.Size = New System.Drawing.Size(1329, 17)
        Me.tsSpring.Spring = True
        Me.tsSpring.Text = "  "
        '
        'tslLoading
        '
        Me.tslLoading.AutoSize = False
        Me.tslLoading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tslLoading.Name = "tslLoading"
        Me.tslLoading.Size = New System.Drawing.Size(424, 17)
        Me.tslLoading.Text = "Loading Media:"
        Me.tslLoading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.tslLoading.Visible = False
        '
        'tspbLoading
        '
        Me.tspbLoading.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tspbLoading.AutoSize = False
        Me.tspbLoading.MarqueeAnimationSpeed = 25
        Me.tspbLoading.Name = "tspbLoading"
        Me.tspbLoading.Size = New System.Drawing.Size(150, 16)
        Me.tspbLoading.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.tspbLoading.Visible = False
        '
        'tmrAni
        '
        Me.tmrAni.Interval = 1
        '
        'mnuMain
        '
        Me.mnuMain.BackColor = System.Drawing.SystemColors.Control
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainFile, Me.mnuMainEdit, Me.mnuMainTools, Me.mnuMainHelp, Me.mnuMainDonate, Me.mnuMainError, Me.mnuVersion})
        Me.mnuMain.Location = New System.Drawing.Point(5, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(1344, 24)
        Me.mnuMain.TabIndex = 0
        Me.mnuMain.Text = "MenuStrip"
        '
        'mnuMainTools
        '
        Me.mnuMainTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainToolsCleanFiles, Me.mnuMainToolsSortFiles, Me.mnuMainToolsBackdrops, Me.ToolStripSeparator4, Me.mnuMainToolsSetsManager, Me.mnuMainToolsOfflineHolder, Me.ToolStripMenuItem3, Me.mnuMainToolsClearCache, Me.mnuMainToolsReloadMovies, Me.mnuMainToolsCleanDB, Me.ToolStripSeparator5})
        Me.mnuMainTools.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.mnuMainTools.Name = "mnuMainTools"
        Me.mnuMainTools.Size = New System.Drawing.Size(46, 20)
        Me.mnuMainTools.Text = "&Tools"
        '
        'mnuMainToolsCleanFiles
        '
        Me.mnuMainToolsCleanFiles.Image = CType(resources.GetObject("mnuMainToolsCleanFiles.Image"), System.Drawing.Image)
        Me.mnuMainToolsCleanFiles.Name = "mnuMainToolsCleanFiles"
        Me.mnuMainToolsCleanFiles.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.mnuMainToolsCleanFiles.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsCleanFiles.Text = "&Clean Files"
        '
        'mnuMainToolsSortFiles
        '
        Me.mnuMainToolsSortFiles.Image = CType(resources.GetObject("mnuMainToolsSortFiles.Image"), System.Drawing.Image)
        Me.mnuMainToolsSortFiles.Name = "mnuMainToolsSortFiles"
        Me.mnuMainToolsSortFiles.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuMainToolsSortFiles.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsSortFiles.Text = "&Sort Files Into Folders"
        '
        'mnuMainToolsBackdrops
        '
        Me.mnuMainToolsBackdrops.Image = CType(resources.GetObject("mnuMainToolsBackdrops.Image"), System.Drawing.Image)
        Me.mnuMainToolsBackdrops.Name = "mnuMainToolsBackdrops"
        Me.mnuMainToolsBackdrops.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.mnuMainToolsBackdrops.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsBackdrops.Text = "Copy Existing Fanart To &Backdrops Folder"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(349, 6)
        '
        'mnuMainToolsSetsManager
        '
        Me.mnuMainToolsSetsManager.Image = CType(resources.GetObject("mnuMainToolsSetsManager.Image"), System.Drawing.Image)
        Me.mnuMainToolsSetsManager.Name = "mnuMainToolsSetsManager"
        Me.mnuMainToolsSetsManager.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.mnuMainToolsSetsManager.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsSetsManager.Text = "Sets &Manager"
        '
        'mnuMainToolsOfflineHolder
        '
        Me.mnuMainToolsOfflineHolder.Image = CType(resources.GetObject("mnuMainToolsOfflineHolder.Image"), System.Drawing.Image)
        Me.mnuMainToolsOfflineHolder.Name = "mnuMainToolsOfflineHolder"
        Me.mnuMainToolsOfflineHolder.ShortcutKeyDisplayString = ""
        Me.mnuMainToolsOfflineHolder.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.mnuMainToolsOfflineHolder.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsOfflineHolder.Text = "&Offline Media Manager"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(349, 6)
        '
        'mnuMainToolsClearCache
        '
        Me.mnuMainToolsClearCache.Image = CType(resources.GetObject("mnuMainToolsClearCache.Image"), System.Drawing.Image)
        Me.mnuMainToolsClearCache.Name = "mnuMainToolsClearCache"
        Me.mnuMainToolsClearCache.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.mnuMainToolsClearCache.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsClearCache.Text = "Clear &All Caches"
        '
        'mnuMainToolsReloadMovies
        '
        Me.mnuMainToolsReloadMovies.Image = CType(resources.GetObject("mnuMainToolsReloadMovies.Image"), System.Drawing.Image)
        Me.mnuMainToolsReloadMovies.Name = "mnuMainToolsReloadMovies"
        Me.mnuMainToolsReloadMovies.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.mnuMainToolsReloadMovies.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsReloadMovies.Text = "Re&load All Movies"
        '
        'mnuMainToolsCleanDB
        '
        Me.mnuMainToolsCleanDB.Image = CType(resources.GetObject("mnuMainToolsCleanDB.Image"), System.Drawing.Image)
        Me.mnuMainToolsCleanDB.Name = "mnuMainToolsCleanDB"
        Me.mnuMainToolsCleanDB.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.mnuMainToolsCleanDB.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsCleanDB.Text = "Clean &Database"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(349, 6)
        '
        'mnuMainDonate
        '
        Me.mnuMainDonate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.mnuMainDonate.Image = CType(resources.GetObject("mnuMainDonate.Image"), System.Drawing.Image)
        Me.mnuMainDonate.Name = "mnuMainDonate"
        Me.mnuMainDonate.Size = New System.Drawing.Size(73, 20)
        Me.mnuMainDonate.Text = "Donate"
        '
        'mnuMainError
        '
        Me.mnuMainError.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.mnuMainError.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mnuMainError.Image = CType(resources.GetObject("mnuMainError.Image"), System.Drawing.Image)
        Me.mnuMainError.Name = "mnuMainError"
        Me.mnuMainError.Size = New System.Drawing.Size(28, 20)
        Me.mnuMainError.ToolTipText = "An Error Has Occurred"
        Me.mnuMainError.Visible = False
        '
        'mnuVersion
        '
        Me.mnuVersion.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.mnuVersion.Name = "mnuVersion"
        Me.mnuVersion.Size = New System.Drawing.Size(83, 20)
        Me.mnuVersion.Text = "mnuVersion"
        '
        'scMain
        '
        Me.scMain.BackColor = System.Drawing.SystemColors.Control
        Me.scMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.scMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.scMain.Location = New System.Drawing.Point(5, 24)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.scMain.Panel1.Controls.Add(Me.pnlFilterGenre)
        Me.scMain.Panel1.Controls.Add(Me.pnlFilterSource)
        Me.scMain.Panel1.Controls.Add(Me.dgvMovies)
        Me.scMain.Panel1.Controls.Add(Me.scTV)
        Me.scMain.Panel1.Controls.Add(Me.pnlListTop)
        Me.scMain.Panel1.Controls.Add(Me.pnlFilter)
        Me.scMain.Panel1.Margin = New System.Windows.Forms.Padding(3)
        Me.scMain.Panel1MinSize = 165
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.scMain.Panel2.Controls.Add(Me.pnlTop)
        Me.scMain.Panel2.Controls.Add(Me.pnlCancel)
        Me.scMain.Panel2.Controls.Add(Me.pnlAllSeason)
        Me.scMain.Panel2.Controls.Add(Me.pbAllSeasonCache)
        Me.scMain.Panel2.Controls.Add(Me.pnlNoInfo)
        Me.scMain.Panel2.Controls.Add(Me.pnlInfoPanel)
        Me.scMain.Panel2.Controls.Add(Me.pnlPoster)
        Me.scMain.Panel2.Controls.Add(Me.pbPosterCache)
        Me.scMain.Panel2.Controls.Add(Me.pbFanartSmallCache)
        Me.scMain.Panel2.Controls.Add(Me.pnlFanartSmall)
        Me.scMain.Panel2.Controls.Add(Me.pnlMPAA)
        Me.scMain.Panel2.Controls.Add(Me.pbFanartCache)
        Me.scMain.Panel2.Controls.Add(Me.pbFanart)
        Me.scMain.Panel2.Controls.Add(Me.tsMain)
        Me.scMain.Panel2.Margin = New System.Windows.Forms.Padding(3)
        Me.scMain.Size = New System.Drawing.Size(1344, 687)
        Me.scMain.SplitterDistance = 364
        Me.scMain.TabIndex = 7
        Me.scMain.TabStop = False
        '
        'pnlFilterGenre
        '
        Me.pnlFilterGenre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilterGenre.Controls.Add(Me.clbFilterGenres)
        Me.pnlFilterGenre.Controls.Add(Me.lblGFilClose)
        Me.pnlFilterGenre.Controls.Add(Me.lblFilterGenres)
        Me.pnlFilterGenre.Location = New System.Drawing.Point(186, 444)
        Me.pnlFilterGenre.Name = "pnlFilterGenre"
        Me.pnlFilterGenre.Size = New System.Drawing.Size(166, 146)
        Me.pnlFilterGenre.TabIndex = 15
        Me.pnlFilterGenre.Visible = False
        '
        'clbFilterGenres
        '
        Me.clbFilterGenres.CheckOnClick = True
        Me.clbFilterGenres.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.clbFilterGenres.FormattingEnabled = True
        Me.clbFilterGenres.Location = New System.Drawing.Point(1, 20)
        Me.clbFilterGenres.Name = "clbFilterGenres"
        Me.clbFilterGenres.Size = New System.Drawing.Size(162, 123)
        Me.clbFilterGenres.TabIndex = 8
        Me.clbFilterGenres.TabStop = False
        '
        'lblGFilClose
        '
        Me.lblGFilClose.AutoSize = True
        Me.lblGFilClose.BackColor = System.Drawing.Color.DimGray
        Me.lblGFilClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblGFilClose.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblGFilClose.ForeColor = System.Drawing.Color.White
        Me.lblGFilClose.Location = New System.Drawing.Point(130, 2)
        Me.lblGFilClose.Name = "lblGFilClose"
        Me.lblGFilClose.Size = New System.Drawing.Size(35, 13)
        Me.lblGFilClose.TabIndex = 24
        Me.lblGFilClose.Text = "Close"
        '
        'lblFilterGenres
        '
        Me.lblFilterGenres.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFilterGenres.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblFilterGenres.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFilterGenres.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilterGenres.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilterGenres.Location = New System.Drawing.Point(1, 1)
        Me.lblFilterGenres.Name = "lblFilterGenres"
        Me.lblFilterGenres.Size = New System.Drawing.Size(162, 17)
        Me.lblFilterGenres.TabIndex = 23
        Me.lblFilterGenres.Text = "Genres"
        Me.lblFilterGenres.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlFilterSource
        '
        Me.pnlFilterSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilterSource.Controls.Add(Me.lblSFilClose)
        Me.pnlFilterSource.Controls.Add(Me.lblFilterSources)
        Me.pnlFilterSource.Controls.Add(Me.clbFilterSource)
        Me.pnlFilterSource.Location = New System.Drawing.Point(186, 515)
        Me.pnlFilterSource.Name = "pnlFilterSource"
        Me.pnlFilterSource.Size = New System.Drawing.Size(166, 146)
        Me.pnlFilterSource.TabIndex = 16
        Me.pnlFilterSource.Visible = False
        '
        'lblSFilClose
        '
        Me.lblSFilClose.AutoSize = True
        Me.lblSFilClose.BackColor = System.Drawing.Color.DimGray
        Me.lblSFilClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblSFilClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSFilClose.ForeColor = System.Drawing.Color.White
        Me.lblSFilClose.Location = New System.Drawing.Point(130, 2)
        Me.lblSFilClose.Name = "lblSFilClose"
        Me.lblSFilClose.Size = New System.Drawing.Size(33, 13)
        Me.lblSFilClose.TabIndex = 24
        Me.lblSFilClose.Text = "Close"
        '
        'lblFilterSources
        '
        Me.lblFilterSources.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFilterSources.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblFilterSources.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFilterSources.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilterSources.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilterSources.Location = New System.Drawing.Point(1, 1)
        Me.lblFilterSources.Name = "lblFilterSources"
        Me.lblFilterSources.Size = New System.Drawing.Size(162, 17)
        Me.lblFilterSources.TabIndex = 23
        Me.lblFilterSources.Text = "Sources"
        Me.lblFilterSources.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'clbFilterSource
        '
        Me.clbFilterSource.CheckOnClick = True
        Me.clbFilterSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.clbFilterSource.FormattingEnabled = True
        Me.clbFilterSource.Location = New System.Drawing.Point(1, 20)
        Me.clbFilterSource.Name = "clbFilterSource"
        Me.clbFilterSource.Size = New System.Drawing.Size(162, 123)
        Me.clbFilterSource.TabIndex = 8
        Me.clbFilterSource.TabStop = False
        '
        'dgvMovies
        '
        Me.dgvMovies.AllowUserToAddRows = False
        Me.dgvMovies.AllowUserToDeleteRows = False
        Me.dgvMovies.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.dgvMovies.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvMovies.BackgroundColor = System.Drawing.Color.White
        Me.dgvMovies.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvMovies.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvMovies.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvMovies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMovies.ContextMenuStrip = Me.cmnuMovie
        Me.dgvMovies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMovies.GridColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvMovies.Location = New System.Drawing.Point(0, 56)
        Me.dgvMovies.Name = "dgvMovies"
        Me.dgvMovies.ReadOnly = True
        Me.dgvMovies.RowHeadersVisible = False
        Me.dgvMovies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMovies.ShowCellErrors = False
        Me.dgvMovies.ShowRowErrors = False
        Me.dgvMovies.Size = New System.Drawing.Size(364, 451)
        Me.dgvMovies.StandardTab = True
        Me.dgvMovies.TabIndex = 0
        '
        'cmnuMovie
        '
        Me.cmnuMovie.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieTitle, Me.ToolStripSeparator3, Me.cmnuMovieReload, Me.cmnuMovieMark, Me.cmnuMovieLock, Me.cmnuMovieWatched, Me.ToolStripMenuItem1, Me.cmnuMovieEdit, Me.cmnuMovieEditMetaData, Me.cmnuMovieGenres, Me.cmnuSep, Me.cmnuMovieRescrape, Me.cmnuMovieReSel, Me.cmnuMovieChange, Me.cmnuSep2, Me.cmnuMovieOpenFolder, Me.ToolStripSeparator1, Me.cmnuMovieRemove})
        Me.cmnuMovie.Name = "mnuMediaList"
        Me.cmnuMovie.Size = New System.Drawing.Size(247, 320)
        '
        'cmnuMovieTitle
        '
        Me.cmnuMovieTitle.Enabled = False
        Me.cmnuMovieTitle.Image = CType(resources.GetObject("cmnuMovieTitle.Image"), System.Drawing.Image)
        Me.cmnuMovieTitle.Name = "cmnuMovieTitle"
        Me.cmnuMovieTitle.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieTitle.Text = "Title"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuMovieReload
        '
        Me.cmnuMovieReload.Image = CType(resources.GetObject("cmnuMovieReload.Image"), System.Drawing.Image)
        Me.cmnuMovieReload.Name = "cmnuMovieReload"
        Me.cmnuMovieReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmnuMovieReload.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieReload.Text = "Reload"
        '
        'cmnuMovieMark
        '
        Me.cmnuMovieMark.Image = CType(resources.GetObject("cmnuMovieMark.Image"), System.Drawing.Image)
        Me.cmnuMovieMark.Name = "cmnuMovieMark"
        Me.cmnuMovieMark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.cmnuMovieMark.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieMark.Text = "Mark"
        '
        'cmnuMovieLock
        '
        Me.cmnuMovieLock.Image = CType(resources.GetObject("cmnuMovieLock.Image"), System.Drawing.Image)
        Me.cmnuMovieLock.Name = "cmnuMovieLock"
        Me.cmnuMovieLock.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.cmnuMovieLock.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieLock.Text = "Lock"
        '
        'cmnuMovieWatched
        '
        Me.cmnuMovieWatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuMovieWatched.Name = "cmnuMovieWatched"
        Me.cmnuMovieWatched.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.cmnuMovieWatched.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieWatched.Text = "Watched"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuMovieEdit
        '
        Me.cmnuMovieEdit.Image = CType(resources.GetObject("cmnuMovieEdit.Image"), System.Drawing.Image)
        Me.cmnuMovieEdit.Name = "cmnuMovieEdit"
        Me.cmnuMovieEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.cmnuMovieEdit.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieEdit.Text = "Edit Movie"
        '
        'cmnuMovieEditMetaData
        '
        Me.cmnuMovieEditMetaData.Image = CType(resources.GetObject("cmnuMovieEditMetaData.Image"), System.Drawing.Image)
        Me.cmnuMovieEditMetaData.Name = "cmnuMovieEditMetaData"
        Me.cmnuMovieEditMetaData.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.cmnuMovieEditMetaData.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieEditMetaData.Text = "Edit Meta Data"
        '
        'cmnuMovieGenres
        '
        Me.cmnuMovieGenres.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieGenresTitle, Me.cmnuMovieGenresGenre, Me.cmnuMovieGenresAdd, Me.cmnuMovieGenresSet, Me.cmnuMovieGenresRemove})
        Me.cmnuMovieGenres.Image = CType(resources.GetObject("cmnuMovieGenres.Image"), System.Drawing.Image)
        Me.cmnuMovieGenres.Name = "cmnuMovieGenres"
        Me.cmnuMovieGenres.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieGenres.Text = "Genres"
        '
        'cmnuMovieGenresTitle
        '
        Me.cmnuMovieGenresTitle.Enabled = False
        Me.cmnuMovieGenresTitle.Name = "cmnuMovieGenresTitle"
        Me.cmnuMovieGenresTitle.Size = New System.Drawing.Size(195, 22)
        Me.cmnuMovieGenresTitle.Text = ">> Select Genre <<"
        '
        'cmnuMovieGenresGenre
        '
        Me.cmnuMovieGenresGenre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmnuMovieGenresGenre.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.cmnuMovieGenresGenre.Name = "cmnuMovieGenresGenre"
        Me.cmnuMovieGenresGenre.Size = New System.Drawing.Size(135, 23)
        Me.cmnuMovieGenresGenre.Sorted = True
        '
        'cmnuMovieGenresAdd
        '
        Me.cmnuMovieGenresAdd.Name = "cmnuMovieGenresAdd"
        Me.cmnuMovieGenresAdd.Size = New System.Drawing.Size(195, 22)
        Me.cmnuMovieGenresAdd.Text = "Add"
        '
        'cmnuMovieGenresSet
        '
        Me.cmnuMovieGenresSet.Name = "cmnuMovieGenresSet"
        Me.cmnuMovieGenresSet.Size = New System.Drawing.Size(195, 22)
        Me.cmnuMovieGenresSet.Text = "Set"
        '
        'cmnuMovieGenresRemove
        '
        Me.cmnuMovieGenresRemove.Name = "cmnuMovieGenresRemove"
        Me.cmnuMovieGenresRemove.Size = New System.Drawing.Size(195, 22)
        Me.cmnuMovieGenresRemove.Text = "Remove"
        '
        'cmnuSep
        '
        Me.cmnuSep.Name = "cmnuSep"
        Me.cmnuSep.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuMovieRescrape
        '
        Me.cmnuMovieRescrape.Image = CType(resources.GetObject("cmnuMovieRescrape.Image"), System.Drawing.Image)
        Me.cmnuMovieRescrape.Name = "cmnuMovieRescrape"
        Me.cmnuMovieRescrape.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.cmnuMovieRescrape.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieRescrape.Text = "(Re)Scrape Movie"
        '
        'cmnuMovieReSel
        '
        Me.cmnuMovieReSel.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieReSelAuto, Me.cmnuMovieReSelAsk, Me.cmnuMovieReSelSkip})
        Me.cmnuMovieReSel.Image = CType(resources.GetObject("cmnuMovieReSel.Image"), System.Drawing.Image)
        Me.cmnuMovieReSel.Name = "cmnuMovieReSel"
        Me.cmnuMovieReSel.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieReSel.Text = "(Re)Scrape Selected Movies"
        '
        'cmnuMovieReSelAuto
        '
        Me.cmnuMovieReSelAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieReSelAutoAll, Me.cmnuMovieReSelAutoActor, Me.cmnuMovieReSelAutoBanner, Me.cmnuMovieReSelAutoClearArt, Me.cmnuMovieReSelAutoClearLogo, Me.cmnuMovieReSelAutoDiscArt, Me.cmnuMovieReSelAutoEFanarts, Me.cmnuMovieReSelAutoEThumbs, Me.cmnuMovieReSelAutoFanart, Me.cmnuMovieReSelAutoLandscape, Me.cmnuMovieReSelAutoMetaData, Me.cmnuMovieReSelAutoNfo, Me.cmnuMovieReSelAutoPoster, Me.cmnuMovieReSelAutoTheme, Me.cmnuMovieReSelAutoTrailer})
        Me.cmnuMovieReSelAuto.Name = "cmnuMovieReSelAuto"
        Me.cmnuMovieReSelAuto.Size = New System.Drawing.Size(271, 22)
        Me.cmnuMovieReSelAuto.Text = "Automatic (Force Best Match)"
        '
        'cmnuMovieReSelAutoAll
        '
        Me.cmnuMovieReSelAutoAll.Name = "cmnuMovieReSelAutoAll"
        Me.cmnuMovieReSelAutoAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoAll.Text = "All Items"
        '
        'cmnuMovieReSelAutoActor
        '
        Me.cmnuMovieReSelAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuMovieReSelAutoActor.Name = "cmnuMovieReSelAutoActor"
        Me.cmnuMovieReSelAutoActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoActor.Text = "Actor Thumbs Only"
        '
        'cmnuMovieReSelAutoBanner
        '
        Me.cmnuMovieReSelAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuMovieReSelAutoBanner.Name = "cmnuMovieReSelAutoBanner"
        Me.cmnuMovieReSelAutoBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoBanner.Text = "Banner Only"
        '
        'cmnuMovieReSelAutoClearArt
        '
        Me.cmnuMovieReSelAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuMovieReSelAutoClearArt.Name = "cmnuMovieReSelAutoClearArt"
        Me.cmnuMovieReSelAutoClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoClearArt.Text = "ClearArt Only"
        '
        'cmnuMovieReSelAutoClearLogo
        '
        Me.cmnuMovieReSelAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuMovieReSelAutoClearLogo.Name = "cmnuMovieReSelAutoClearLogo"
        Me.cmnuMovieReSelAutoClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoClearLogo.Text = "ClearLogo Only"
        '
        'cmnuMovieReSelAutoDiscArt
        '
        Me.cmnuMovieReSelAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuMovieReSelAutoDiscArt.Name = "cmnuMovieReSelAutoDiscArt"
        Me.cmnuMovieReSelAutoDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoDiscArt.Text = "DiscArt Only"
        '
        'cmnuMovieReSelAutoEFanarts
        '
        Me.cmnuMovieReSelAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuMovieReSelAutoEFanarts.Name = "cmnuMovieReSelAutoEFanarts"
        Me.cmnuMovieReSelAutoEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuMovieReSelAutoEThumbs
        '
        Me.cmnuMovieReSelAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuMovieReSelAutoEThumbs.Name = "cmnuMovieReSelAutoEThumbs"
        Me.cmnuMovieReSelAutoEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuMovieReSelAutoFanart
        '
        Me.cmnuMovieReSelAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuMovieReSelAutoFanart.Name = "cmnuMovieReSelAutoFanart"
        Me.cmnuMovieReSelAutoFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoFanart.Text = "Fanart Only"
        '
        'cmnuMovieReSelAutoLandscape
        '
        Me.cmnuMovieReSelAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuMovieReSelAutoLandscape.Name = "cmnuMovieReSelAutoLandscape"
        Me.cmnuMovieReSelAutoLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoLandscape.Text = "Landscape Only"
        '
        'cmnuMovieReSelAutoMetaData
        '
        Me.cmnuMovieReSelAutoMetaData.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuMovieReSelAutoMetaData.Name = "cmnuMovieReSelAutoMetaData"
        Me.cmnuMovieReSelAutoMetaData.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoMetaData.Text = "Meta Data Only"
        '
        'cmnuMovieReSelAutoNfo
        '
        Me.cmnuMovieReSelAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuMovieReSelAutoNfo.Name = "cmnuMovieReSelAutoNfo"
        Me.cmnuMovieReSelAutoNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoNfo.Text = "NFO Only"
        '
        'cmnuMovieReSelAutoPoster
        '
        Me.cmnuMovieReSelAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuMovieReSelAutoPoster.Name = "cmnuMovieReSelAutoPoster"
        Me.cmnuMovieReSelAutoPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoPoster.Text = "Poster Only"
        '
        'cmnuMovieReSelAutoTheme
        '
        Me.cmnuMovieReSelAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuMovieReSelAutoTheme.Name = "cmnuMovieReSelAutoTheme"
        Me.cmnuMovieReSelAutoTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoTheme.Text = "Theme Only"
        '
        'cmnuMovieReSelAutoTrailer
        '
        Me.cmnuMovieReSelAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuMovieReSelAutoTrailer.Name = "cmnuMovieReSelAutoTrailer"
        Me.cmnuMovieReSelAutoTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAutoTrailer.Text = "Trailer Only"
        '
        'cmnuMovieReSelAsk
        '
        Me.cmnuMovieReSelAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieReSelAskAll, Me.cmnuMovieReSelAskActor, Me.cmnuMovieReSelAskBanner, Me.cmnuMovieReSelAskClearArt, Me.cmnuMovieReSelAskClearLogo, Me.cmnuMovieReSelAskDiscArt, Me.cmnuMovieReSelAskEFanarts, Me.cmnuMovieReSelAskEThumbs, Me.cmnuMovieReSelAskFanart, Me.cmnuMovieReSelAskLandscape, Me.cmnuMovieReSelAskMetaData, Me.cmnuMovieReSelAskNfo, Me.cmnuMovieReSelAskPoster, Me.cmnuMovieReSelAskTheme, Me.cmnuMovieReSelAskTrailer})
        Me.cmnuMovieReSelAsk.Name = "cmnuMovieReSelAsk"
        Me.cmnuMovieReSelAsk.Size = New System.Drawing.Size(271, 22)
        Me.cmnuMovieReSelAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'cmnuMovieReSelAskAll
        '
        Me.cmnuMovieReSelAskAll.Name = "cmnuMovieReSelAskAll"
        Me.cmnuMovieReSelAskAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskAll.Text = "All Items"
        '
        'cmnuMovieReSelAskActor
        '
        Me.cmnuMovieReSelAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuMovieReSelAskActor.Name = "cmnuMovieReSelAskActor"
        Me.cmnuMovieReSelAskActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskActor.Text = "Actor Thumbs Only"
        '
        'cmnuMovieReSelAskBanner
        '
        Me.cmnuMovieReSelAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuMovieReSelAskBanner.Name = "cmnuMovieReSelAskBanner"
        Me.cmnuMovieReSelAskBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskBanner.Text = "Banner Only"
        '
        'cmnuMovieReSelAskClearArt
        '
        Me.cmnuMovieReSelAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuMovieReSelAskClearArt.Name = "cmnuMovieReSelAskClearArt"
        Me.cmnuMovieReSelAskClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskClearArt.Text = "ClearArt Only"
        '
        'cmnuMovieReSelAskClearLogo
        '
        Me.cmnuMovieReSelAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuMovieReSelAskClearLogo.Name = "cmnuMovieReSelAskClearLogo"
        Me.cmnuMovieReSelAskClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskClearLogo.Text = "ClearLogo Only"
        '
        'cmnuMovieReSelAskDiscArt
        '
        Me.cmnuMovieReSelAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuMovieReSelAskDiscArt.Name = "cmnuMovieReSelAskDiscArt"
        Me.cmnuMovieReSelAskDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskDiscArt.Text = "DiscArt Only"
        '
        'cmnuMovieReSelAskEFanarts
        '
        Me.cmnuMovieReSelAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuMovieReSelAskEFanarts.Name = "cmnuMovieReSelAskEFanarts"
        Me.cmnuMovieReSelAskEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuMovieReSelAskEThumbs
        '
        Me.cmnuMovieReSelAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuMovieReSelAskEThumbs.Name = "cmnuMovieReSelAskEThumbs"
        Me.cmnuMovieReSelAskEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuMovieReSelAskFanart
        '
        Me.cmnuMovieReSelAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuMovieReSelAskFanart.Name = "cmnuMovieReSelAskFanart"
        Me.cmnuMovieReSelAskFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskFanart.Text = "Fanart Only"
        '
        'cmnuMovieReSelAskLandscape
        '
        Me.cmnuMovieReSelAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuMovieReSelAskLandscape.Name = "cmnuMovieReSelAskLandscape"
        Me.cmnuMovieReSelAskLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskLandscape.Text = "Landscape Only"
        '
        'cmnuMovieReSelAskMetaData
        '
        Me.cmnuMovieReSelAskMetaData.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuMovieReSelAskMetaData.Name = "cmnuMovieReSelAskMetaData"
        Me.cmnuMovieReSelAskMetaData.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskMetaData.Text = "Meta Data Only"
        '
        'cmnuMovieReSelAskNfo
        '
        Me.cmnuMovieReSelAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuMovieReSelAskNfo.Name = "cmnuMovieReSelAskNfo"
        Me.cmnuMovieReSelAskNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskNfo.Text = "NFO Only"
        '
        'cmnuMovieReSelAskPoster
        '
        Me.cmnuMovieReSelAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasSubtitle
        Me.cmnuMovieReSelAskPoster.Name = "cmnuMovieReSelAskPoster"
        Me.cmnuMovieReSelAskPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskPoster.Text = "Poster Only"
        '
        'cmnuMovieReSelAskTheme
        '
        Me.cmnuMovieReSelAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuMovieReSelAskTheme.Name = "cmnuMovieReSelAskTheme"
        Me.cmnuMovieReSelAskTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskTheme.Text = "Theme Only"
        '
        'cmnuMovieReSelAskTrailer
        '
        Me.cmnuMovieReSelAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuMovieReSelAskTrailer.Name = "cmnuMovieReSelAskTrailer"
        Me.cmnuMovieReSelAskTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieReSelAskTrailer.Text = "Trailer Only"
        '
        'cmnuMovieReSelSkip
        '
        Me.cmnuMovieReSelSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieReSelSkipAll})
        Me.cmnuMovieReSelSkip.Name = "cmnuMovieReSelSkip"
        Me.cmnuMovieReSelSkip.Size = New System.Drawing.Size(271, 22)
        Me.cmnuMovieReSelSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'cmnuMovieReSelSkipAll
        '
        Me.cmnuMovieReSelSkipAll.Name = "cmnuMovieReSelSkipAll"
        Me.cmnuMovieReSelSkipAll.Size = New System.Drawing.Size(120, 22)
        Me.cmnuMovieReSelSkipAll.Text = "All Items"
        '
        'cmnuMovieChange
        '
        Me.cmnuMovieChange.Image = CType(resources.GetObject("cmnuMovieChange.Image"), System.Drawing.Image)
        Me.cmnuMovieChange.Name = "cmnuMovieChange"
        Me.cmnuMovieChange.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.cmnuMovieChange.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieChange.Text = "Change Movie"
        '
        'cmnuSep2
        '
        Me.cmnuSep2.Name = "cmnuSep2"
        Me.cmnuSep2.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuMovieOpenFolder
        '
        Me.cmnuMovieOpenFolder.Image = CType(resources.GetObject("cmnuMovieOpenFolder.Image"), System.Drawing.Image)
        Me.cmnuMovieOpenFolder.Name = "cmnuMovieOpenFolder"
        Me.cmnuMovieOpenFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.cmnuMovieOpenFolder.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieOpenFolder.Text = "Open Containing Folder"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuMovieRemove
        '
        Me.cmnuMovieRemove.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieRemoveFromDB, Me.cmnuMovieRemoveFromDisk})
        Me.cmnuMovieRemove.Image = CType(resources.GetObject("cmnuMovieRemove.Image"), System.Drawing.Image)
        Me.cmnuMovieRemove.Name = "cmnuMovieRemove"
        Me.cmnuMovieRemove.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieRemove.Text = "Remove"
        '
        'cmnuMovieRemoveFromDB
        '
        Me.cmnuMovieRemoveFromDB.Image = CType(resources.GetObject("cmnuMovieRemoveFromDB.Image"), System.Drawing.Image)
        Me.cmnuMovieRemoveFromDB.Name = "cmnuMovieRemoveFromDB"
        Me.cmnuMovieRemoveFromDB.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.cmnuMovieRemoveFromDB.Size = New System.Drawing.Size(225, 22)
        Me.cmnuMovieRemoveFromDB.Text = "Remove from Database"
        '
        'cmnuMovieRemoveFromDisk
        '
        Me.cmnuMovieRemoveFromDisk.Image = CType(resources.GetObject("cmnuMovieRemoveFromDisk.Image"), System.Drawing.Image)
        Me.cmnuMovieRemoveFromDisk.Name = "cmnuMovieRemoveFromDisk"
        Me.cmnuMovieRemoveFromDisk.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Delete), System.Windows.Forms.Keys)
        Me.cmnuMovieRemoveFromDisk.Size = New System.Drawing.Size(225, 22)
        Me.cmnuMovieRemoveFromDisk.Text = "Delete Movie"
        '
        'scTV
        '
        Me.scTV.BackColor = System.Drawing.Color.Gainsboro
        Me.scTV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scTV.Location = New System.Drawing.Point(0, 56)
        Me.scTV.Name = "scTV"
        Me.scTV.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scTV.Panel1
        '
        Me.scTV.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.scTV.Panel1.Controls.Add(Me.dgvTVShows)
        Me.scTV.Panel1.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        '
        'scTV.Panel2
        '
        Me.scTV.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.scTV.Panel2.Controls.Add(Me.scTVSeasonsEpisodes)
        Me.scTV.Size = New System.Drawing.Size(364, 451)
        Me.scTV.SplitterDistance = 113
        Me.scTV.TabIndex = 3
        Me.scTV.TabStop = False
        '
        'dgvTVShows
        '
        Me.dgvTVShows.AllowUserToAddRows = False
        Me.dgvTVShows.AllowUserToDeleteRows = False
        Me.dgvTVShows.AllowUserToResizeRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.dgvTVShows.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvTVShows.BackgroundColor = System.Drawing.Color.White
        Me.dgvTVShows.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTVShows.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvTVShows.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvTVShows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTVShows.ContextMenuStrip = Me.cmnuShow
        Me.dgvTVShows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTVShows.GridColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvTVShows.Location = New System.Drawing.Point(0, 2)
        Me.dgvTVShows.Name = "dgvTVShows"
        Me.dgvTVShows.ReadOnly = True
        Me.dgvTVShows.RowHeadersVisible = False
        Me.dgvTVShows.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTVShows.ShowCellErrors = False
        Me.dgvTVShows.ShowRowErrors = False
        Me.dgvTVShows.Size = New System.Drawing.Size(364, 111)
        Me.dgvTVShows.StandardTab = True
        Me.dgvTVShows.TabIndex = 0
        '
        'cmnuShow
        '
        Me.cmnuShow.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuShowTitle, Me.ToolStripMenuItem2, Me.cmnuShowReload, Me.cmnuShowMark, Me.cmnuShowLock, Me.cmnuShowWatched, Me.ToolStripSeparator8, Me.cmnuShowEdit, Me.ToolStripSeparator7, Me.cmnuShowRescrape, Me.cmnuShowRefresh, Me.cmnuShowChange, Me.ToolStripSeparator11, Me.cmnuShowOpenFolder, Me.ToolStripSeparator20, Me.cmnuShowRemove})
        Me.cmnuShow.Name = "mnuShows"
        Me.cmnuShow.Size = New System.Drawing.Size(247, 276)
        '
        'cmnuShowTitle
        '
        Me.cmnuShowTitle.Enabled = False
        Me.cmnuShowTitle.Image = CType(resources.GetObject("cmnuShowTitle.Image"), System.Drawing.Image)
        Me.cmnuShowTitle.Name = "cmnuShowTitle"
        Me.cmnuShowTitle.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowTitle.Text = "Title"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuShowReload
        '
        Me.cmnuShowReload.Image = CType(resources.GetObject("cmnuShowReload.Image"), System.Drawing.Image)
        Me.cmnuShowReload.Name = "cmnuShowReload"
        Me.cmnuShowReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmnuShowReload.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowReload.Text = "Reload"
        '
        'cmnuShowMark
        '
        Me.cmnuShowMark.Image = CType(resources.GetObject("cmnuShowMark.Image"), System.Drawing.Image)
        Me.cmnuShowMark.Name = "cmnuShowMark"
        Me.cmnuShowMark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.cmnuShowMark.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowMark.Text = "Mark"
        '
        'cmnuShowLock
        '
        Me.cmnuShowLock.Image = CType(resources.GetObject("cmnuShowLock.Image"), System.Drawing.Image)
        Me.cmnuShowLock.Name = "cmnuShowLock"
        Me.cmnuShowLock.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.cmnuShowLock.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowLock.Text = "Lock"
        '
        'cmnuShowWatched
        '
        Me.cmnuShowWatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuShowWatched.Name = "cmnuShowWatched"
        Me.cmnuShowWatched.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.cmnuShowWatched.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowWatched.Text = "Watched"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuShowEdit
        '
        Me.cmnuShowEdit.Image = CType(resources.GetObject("cmnuShowEdit.Image"), System.Drawing.Image)
        Me.cmnuShowEdit.Name = "cmnuShowEdit"
        Me.cmnuShowEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.cmnuShowEdit.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowEdit.Text = "Edit Show"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuShowRescrape
        '
        Me.cmnuShowRescrape.Image = CType(resources.GetObject("cmnuShowRescrape.Image"), System.Drawing.Image)
        Me.cmnuShowRescrape.Name = "cmnuShowRescrape"
        Me.cmnuShowRescrape.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.cmnuShowRescrape.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowRescrape.Text = "(Re)Scrape Show"
        '
        'cmnuShowRefresh
        '
        Me.cmnuShowRefresh.Name = "cmnuShowRefresh"
        Me.cmnuShowRefresh.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.cmnuShowRefresh.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowRefresh.Text = "Refresh Data"
        '
        'cmnuShowChange
        '
        Me.cmnuShowChange.Image = CType(resources.GetObject("cmnuShowChange.Image"), System.Drawing.Image)
        Me.cmnuShowChange.Name = "cmnuShowChange"
        Me.cmnuShowChange.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.cmnuShowChange.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowChange.Text = "Change Show"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuShowOpenFolder
        '
        Me.cmnuShowOpenFolder.Image = CType(resources.GetObject("cmnuShowOpenFolder.Image"), System.Drawing.Image)
        Me.cmnuShowOpenFolder.Name = "cmnuShowOpenFolder"
        Me.cmnuShowOpenFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.cmnuShowOpenFolder.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowOpenFolder.Text = "Open Containing Folder"
        '
        'ToolStripSeparator20
        '
        Me.ToolStripSeparator20.Name = "ToolStripSeparator20"
        Me.ToolStripSeparator20.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuShowRemove
        '
        Me.cmnuShowRemove.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuShowRemoveFromDB, Me.cmnuShowRemoveFromDisk})
        Me.cmnuShowRemove.Image = CType(resources.GetObject("cmnuShowRemove.Image"), System.Drawing.Image)
        Me.cmnuShowRemove.Name = "cmnuShowRemove"
        Me.cmnuShowRemove.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowRemove.Text = "Remove"
        '
        'cmnuShowRemoveFromDB
        '
        Me.cmnuShowRemoveFromDB.Image = CType(resources.GetObject("cmnuShowRemoveFromDB.Image"), System.Drawing.Image)
        Me.cmnuShowRemoveFromDB.Name = "cmnuShowRemoveFromDB"
        Me.cmnuShowRemoveFromDB.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.cmnuShowRemoveFromDB.Size = New System.Drawing.Size(225, 22)
        Me.cmnuShowRemoveFromDB.Text = "Remove from Database"
        '
        'cmnuShowRemoveFromDisk
        '
        Me.cmnuShowRemoveFromDisk.Image = CType(resources.GetObject("cmnuShowRemoveFromDisk.Image"), System.Drawing.Image)
        Me.cmnuShowRemoveFromDisk.Name = "cmnuShowRemoveFromDisk"
        Me.cmnuShowRemoveFromDisk.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Delete), System.Windows.Forms.Keys)
        Me.cmnuShowRemoveFromDisk.Size = New System.Drawing.Size(225, 22)
        Me.cmnuShowRemoveFromDisk.Text = "Delete TV Show"
        '
        'scTVSeasonsEpisodes
        '
        Me.scTVSeasonsEpisodes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scTVSeasonsEpisodes.Location = New System.Drawing.Point(0, 0)
        Me.scTVSeasonsEpisodes.Name = "scTVSeasonsEpisodes"
        Me.scTVSeasonsEpisodes.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scTVSeasonsEpisodes.Panel1
        '
        Me.scTVSeasonsEpisodes.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.scTVSeasonsEpisodes.Panel1.Controls.Add(Me.dgvTVSeasons)
        '
        'scTVSeasonsEpisodes.Panel2
        '
        Me.scTVSeasonsEpisodes.Panel2.Controls.Add(Me.dgvTVEpisodes)
        Me.scTVSeasonsEpisodes.Size = New System.Drawing.Size(364, 334)
        Me.scTVSeasonsEpisodes.SplitterDistance = 114
        Me.scTVSeasonsEpisodes.TabIndex = 0
        Me.scTVSeasonsEpisodes.TabStop = False
        '
        'dgvTVSeasons
        '
        Me.dgvTVSeasons.AllowUserToAddRows = False
        Me.dgvTVSeasons.AllowUserToDeleteRows = False
        Me.dgvTVSeasons.AllowUserToResizeRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.dgvTVSeasons.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvTVSeasons.BackgroundColor = System.Drawing.Color.White
        Me.dgvTVSeasons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTVSeasons.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvTVSeasons.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvTVSeasons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTVSeasons.ContextMenuStrip = Me.cmnuSeason
        Me.dgvTVSeasons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTVSeasons.GridColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvTVSeasons.Location = New System.Drawing.Point(0, 0)
        Me.dgvTVSeasons.Name = "dgvTVSeasons"
        Me.dgvTVSeasons.ReadOnly = True
        Me.dgvTVSeasons.RowHeadersVisible = False
        Me.dgvTVSeasons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTVSeasons.ShowCellErrors = False
        Me.dgvTVSeasons.ShowRowErrors = False
        Me.dgvTVSeasons.Size = New System.Drawing.Size(364, 114)
        Me.dgvTVSeasons.StandardTab = True
        Me.dgvTVSeasons.TabIndex = 0
        '
        'cmnuSeason
        '
        Me.cmnuSeason.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuSeasonTitle, Me.ToolStripSeparator17, Me.cmnuSeasonReload, Me.cmnuSeasonMark, Me.cmnuSeasonLock, Me.cmnuSeasonWatched, Me.ToolStripSeparator16, Me.cmnuSeasonChangeImages, Me.ToolStripSeparator14, Me.cmnuSeasonRescrape, Me.ToolStripSeparator15, Me.cmnuSeasonOpenFolder, Me.ToolStripSeparator27, Me.cmnuSeasonRemove})
        Me.cmnuSeason.Name = "mnuSeasons"
        Me.cmnuSeason.Size = New System.Drawing.Size(247, 232)
        '
        'cmnuSeasonTitle
        '
        Me.cmnuSeasonTitle.Enabled = False
        Me.cmnuSeasonTitle.Image = CType(resources.GetObject("cmnuSeasonTitle.Image"), System.Drawing.Image)
        Me.cmnuSeasonTitle.Name = "cmnuSeasonTitle"
        Me.cmnuSeasonTitle.Size = New System.Drawing.Size(246, 22)
        Me.cmnuSeasonTitle.Text = "Title"
        '
        'ToolStripSeparator17
        '
        Me.ToolStripSeparator17.Name = "ToolStripSeparator17"
        Me.ToolStripSeparator17.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuSeasonReload
        '
        Me.cmnuSeasonReload.Image = CType(resources.GetObject("cmnuSeasonReload.Image"), System.Drawing.Image)
        Me.cmnuSeasonReload.Name = "cmnuSeasonReload"
        Me.cmnuSeasonReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmnuSeasonReload.Size = New System.Drawing.Size(246, 22)
        Me.cmnuSeasonReload.Text = "Reload"
        '
        'cmnuSeasonMark
        '
        Me.cmnuSeasonMark.Image = CType(resources.GetObject("cmnuSeasonMark.Image"), System.Drawing.Image)
        Me.cmnuSeasonMark.Name = "cmnuSeasonMark"
        Me.cmnuSeasonMark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.cmnuSeasonMark.Size = New System.Drawing.Size(246, 22)
        Me.cmnuSeasonMark.Text = "Mark"
        '
        'cmnuSeasonLock
        '
        Me.cmnuSeasonLock.Image = CType(resources.GetObject("cmnuSeasonLock.Image"), System.Drawing.Image)
        Me.cmnuSeasonLock.Name = "cmnuSeasonLock"
        Me.cmnuSeasonLock.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.cmnuSeasonLock.Size = New System.Drawing.Size(246, 22)
        Me.cmnuSeasonLock.Text = "Lock"
        '
        'cmnuSeasonWatched
        '
        Me.cmnuSeasonWatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuSeasonWatched.Name = "cmnuSeasonWatched"
        Me.cmnuSeasonWatched.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.cmnuSeasonWatched.Size = New System.Drawing.Size(246, 22)
        Me.cmnuSeasonWatched.Text = "Watched"
        '
        'ToolStripSeparator16
        '
        Me.ToolStripSeparator16.Name = "ToolStripSeparator16"
        Me.ToolStripSeparator16.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuSeasonChangeImages
        '
        Me.cmnuSeasonChangeImages.Image = CType(resources.GetObject("cmnuSeasonChangeImages.Image"), System.Drawing.Image)
        Me.cmnuSeasonChangeImages.Name = "cmnuSeasonChangeImages"
        Me.cmnuSeasonChangeImages.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.cmnuSeasonChangeImages.Size = New System.Drawing.Size(246, 22)
        Me.cmnuSeasonChangeImages.Text = "Change Images"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuSeasonRescrape
        '
        Me.cmnuSeasonRescrape.Image = CType(resources.GetObject("cmnuSeasonRescrape.Image"), System.Drawing.Image)
        Me.cmnuSeasonRescrape.Name = "cmnuSeasonRescrape"
        Me.cmnuSeasonRescrape.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.cmnuSeasonRescrape.Size = New System.Drawing.Size(246, 22)
        Me.cmnuSeasonRescrape.Text = "(Re)Scrape Season"
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuSeasonOpenFolder
        '
        Me.cmnuSeasonOpenFolder.Image = CType(resources.GetObject("cmnuSeasonOpenFolder.Image"), System.Drawing.Image)
        Me.cmnuSeasonOpenFolder.Name = "cmnuSeasonOpenFolder"
        Me.cmnuSeasonOpenFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.cmnuSeasonOpenFolder.Size = New System.Drawing.Size(246, 22)
        Me.cmnuSeasonOpenFolder.Text = "Open Contianing Folder"
        '
        'ToolStripSeparator27
        '
        Me.ToolStripSeparator27.Name = "ToolStripSeparator27"
        Me.ToolStripSeparator27.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuSeasonRemove
        '
        Me.cmnuSeasonRemove.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuRemoveSeasonFromDB, Me.cmnuSeasonRemoveFromDisk})
        Me.cmnuSeasonRemove.Image = CType(resources.GetObject("cmnuSeasonRemove.Image"), System.Drawing.Image)
        Me.cmnuSeasonRemove.Name = "cmnuSeasonRemove"
        Me.cmnuSeasonRemove.Size = New System.Drawing.Size(246, 22)
        Me.cmnuSeasonRemove.Text = "Remove"
        '
        'cmnuRemoveSeasonFromDB
        '
        Me.cmnuRemoveSeasonFromDB.Image = CType(resources.GetObject("cmnuRemoveSeasonFromDB.Image"), System.Drawing.Image)
        Me.cmnuRemoveSeasonFromDB.Name = "cmnuRemoveSeasonFromDB"
        Me.cmnuRemoveSeasonFromDB.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.cmnuRemoveSeasonFromDB.Size = New System.Drawing.Size(225, 22)
        Me.cmnuRemoveSeasonFromDB.Text = "Remove from Database"
        '
        'cmnuSeasonRemoveFromDisk
        '
        Me.cmnuSeasonRemoveFromDisk.Image = CType(resources.GetObject("cmnuSeasonRemoveFromDisk.Image"), System.Drawing.Image)
        Me.cmnuSeasonRemoveFromDisk.Name = "cmnuSeasonRemoveFromDisk"
        Me.cmnuSeasonRemoveFromDisk.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Delete), System.Windows.Forms.Keys)
        Me.cmnuSeasonRemoveFromDisk.Size = New System.Drawing.Size(225, 22)
        Me.cmnuSeasonRemoveFromDisk.Text = "Delete Season"
        '
        'dgvTVEpisodes
        '
        Me.dgvTVEpisodes.AllowUserToAddRows = False
        Me.dgvTVEpisodes.AllowUserToDeleteRows = False
        Me.dgvTVEpisodes.AllowUserToResizeRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.dgvTVEpisodes.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvTVEpisodes.BackgroundColor = System.Drawing.Color.White
        Me.dgvTVEpisodes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTVEpisodes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvTVEpisodes.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvTVEpisodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTVEpisodes.ContextMenuStrip = Me.cmnuEpisode
        Me.dgvTVEpisodes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTVEpisodes.GridColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvTVEpisodes.Location = New System.Drawing.Point(0, 0)
        Me.dgvTVEpisodes.Name = "dgvTVEpisodes"
        Me.dgvTVEpisodes.ReadOnly = True
        Me.dgvTVEpisodes.RowHeadersVisible = False
        Me.dgvTVEpisodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTVEpisodes.ShowCellErrors = False
        Me.dgvTVEpisodes.ShowRowErrors = False
        Me.dgvTVEpisodes.Size = New System.Drawing.Size(364, 216)
        Me.dgvTVEpisodes.StandardTab = True
        Me.dgvTVEpisodes.TabIndex = 0
        '
        'cmnuEpisode
        '
        Me.cmnuEpisode.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuEpisodeTitle, Me.ToolStripSeparator6, Me.cmnuEpisodeReload, Me.cmnuEpisodeMark, Me.cmnuEpisodeLock, Me.cmnuEpisodeWatched, Me.ToolStripSeparator9, Me.cmnuEpisodeEdit, Me.ToolStripSeparator10, Me.cmnuEpisodeRescrape, Me.cmnuEpisodeChange, Me.ToolStripSeparator12, Me.cmnuEpisodeOpenFolder, Me.ToolStripSeparator28, Me.cmnuEpisodeRemove})
        Me.cmnuEpisode.Name = "mnuEpisodes"
        Me.cmnuEpisode.Size = New System.Drawing.Size(247, 254)
        '
        'cmnuEpisodeTitle
        '
        Me.cmnuEpisodeTitle.Enabled = False
        Me.cmnuEpisodeTitle.Image = CType(resources.GetObject("cmnuEpisodeTitle.Image"), System.Drawing.Image)
        Me.cmnuEpisodeTitle.Name = "cmnuEpisodeTitle"
        Me.cmnuEpisodeTitle.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeTitle.Text = "Title"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuEpisodeReload
        '
        Me.cmnuEpisodeReload.Image = CType(resources.GetObject("cmnuEpisodeReload.Image"), System.Drawing.Image)
        Me.cmnuEpisodeReload.Name = "cmnuEpisodeReload"
        Me.cmnuEpisodeReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmnuEpisodeReload.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeReload.Text = "Reload"
        '
        'cmnuEpisodeMark
        '
        Me.cmnuEpisodeMark.Image = CType(resources.GetObject("cmnuEpisodeMark.Image"), System.Drawing.Image)
        Me.cmnuEpisodeMark.Name = "cmnuEpisodeMark"
        Me.cmnuEpisodeMark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.cmnuEpisodeMark.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeMark.Text = "Mark"
        '
        'cmnuEpisodeLock
        '
        Me.cmnuEpisodeLock.Image = CType(resources.GetObject("cmnuEpisodeLock.Image"), System.Drawing.Image)
        Me.cmnuEpisodeLock.Name = "cmnuEpisodeLock"
        Me.cmnuEpisodeLock.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.cmnuEpisodeLock.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeLock.Text = "Lock"
        '
        'cmnuEpisodeWatched
        '
        Me.cmnuEpisodeWatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuEpisodeWatched.Name = "cmnuEpisodeWatched"
        Me.cmnuEpisodeWatched.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.cmnuEpisodeWatched.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeWatched.Text = "Watched"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuEpisodeEdit
        '
        Me.cmnuEpisodeEdit.Image = CType(resources.GetObject("cmnuEpisodeEdit.Image"), System.Drawing.Image)
        Me.cmnuEpisodeEdit.Name = "cmnuEpisodeEdit"
        Me.cmnuEpisodeEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.cmnuEpisodeEdit.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeEdit.Text = "Edit Episode"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuEpisodeRescrape
        '
        Me.cmnuEpisodeRescrape.Image = CType(resources.GetObject("cmnuEpisodeRescrape.Image"), System.Drawing.Image)
        Me.cmnuEpisodeRescrape.Name = "cmnuEpisodeRescrape"
        Me.cmnuEpisodeRescrape.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.cmnuEpisodeRescrape.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeRescrape.Text = "(Re)Scrape Episode"
        '
        'cmnuEpisodeChange
        '
        Me.cmnuEpisodeChange.Image = CType(resources.GetObject("cmnuEpisodeChange.Image"), System.Drawing.Image)
        Me.cmnuEpisodeChange.Name = "cmnuEpisodeChange"
        Me.cmnuEpisodeChange.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.cmnuEpisodeChange.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeChange.Text = "Change Episode"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuEpisodeOpenFolder
        '
        Me.cmnuEpisodeOpenFolder.Image = CType(resources.GetObject("cmnuEpisodeOpenFolder.Image"), System.Drawing.Image)
        Me.cmnuEpisodeOpenFolder.Name = "cmnuEpisodeOpenFolder"
        Me.cmnuEpisodeOpenFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.cmnuEpisodeOpenFolder.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeOpenFolder.Text = "Open Contianing Folder"
        '
        'ToolStripSeparator28
        '
        Me.ToolStripSeparator28.Name = "ToolStripSeparator28"
        Me.ToolStripSeparator28.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuEpisodeRemove
        '
        Me.cmnuEpisodeRemove.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuEpisodeRemoveFromDB, Me.cmnuEpisodeRemoveFromDisk})
        Me.cmnuEpisodeRemove.Image = CType(resources.GetObject("cmnuEpisodeRemove.Image"), System.Drawing.Image)
        Me.cmnuEpisodeRemove.Name = "cmnuEpisodeRemove"
        Me.cmnuEpisodeRemove.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeRemove.Text = "Remove"
        '
        'cmnuEpisodeRemoveFromDB
        '
        Me.cmnuEpisodeRemoveFromDB.Image = CType(resources.GetObject("cmnuEpisodeRemoveFromDB.Image"), System.Drawing.Image)
        Me.cmnuEpisodeRemoveFromDB.Name = "cmnuEpisodeRemoveFromDB"
        Me.cmnuEpisodeRemoveFromDB.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.cmnuEpisodeRemoveFromDB.Size = New System.Drawing.Size(225, 22)
        Me.cmnuEpisodeRemoveFromDB.Text = "Remove from Database"
        '
        'cmnuEpisodeRemoveFromDisk
        '
        Me.cmnuEpisodeRemoveFromDisk.Image = CType(resources.GetObject("cmnuEpisodeRemoveFromDisk.Image"), System.Drawing.Image)
        Me.cmnuEpisodeRemoveFromDisk.Name = "cmnuEpisodeRemoveFromDisk"
        Me.cmnuEpisodeRemoveFromDisk.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Delete), System.Windows.Forms.Keys)
        Me.cmnuEpisodeRemoveFromDisk.Size = New System.Drawing.Size(225, 22)
        Me.cmnuEpisodeRemoveFromDisk.Text = "Delete Episode"
        '
        'pnlListTop
        '
        Me.pnlListTop.BackColor = System.Drawing.SystemColors.Control
        Me.pnlListTop.Controls.Add(Me.btnMarkAll)
        Me.pnlListTop.Controls.Add(Me.pnlSearch)
        Me.pnlListTop.Controls.Add(Me.tcMain)
        Me.pnlListTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlListTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlListTop.Name = "pnlListTop"
        Me.pnlListTop.Size = New System.Drawing.Size(364, 56)
        Me.pnlListTop.TabIndex = 14
        '
        'btnMarkAll
        '
        Me.btnMarkAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMarkAll.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMarkAll.Image = CType(resources.GetObject("btnMarkAll.Image"), System.Drawing.Image)
        Me.btnMarkAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMarkAll.Location = New System.Drawing.Point(256, 1)
        Me.btnMarkAll.Name = "btnMarkAll"
        Me.btnMarkAll.Size = New System.Drawing.Size(109, 21)
        Me.btnMarkAll.TabIndex = 1
        Me.btnMarkAll.Text = "Mark All"
        Me.btnMarkAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMarkAll.UseVisualStyleBackColor = True
        '
        'pnlSearch
        '
        Me.pnlSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearch.Controls.Add(Me.cbSearch)
        Me.pnlSearch.Controls.Add(Me.picSearch)
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Location = New System.Drawing.Point(0, 25)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(364, 35)
        Me.pnlSearch.TabIndex = 0
        '
        'cbSearch
        '
        Me.cbSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSearch.FormattingEnabled = True
        Me.cbSearch.Location = New System.Drawing.Point(253, 6)
        Me.cbSearch.Name = "cbSearch"
        Me.cbSearch.Size = New System.Drawing.Size(83, 21)
        Me.cbSearch.TabIndex = 1
        '
        'picSearch
        '
        Me.picSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picSearch.Image = CType(resources.GetObject("picSearch.Image"), System.Drawing.Image)
        Me.picSearch.Location = New System.Drawing.Point(340, 8)
        Me.picSearch.Name = "picSearch"
        Me.picSearch.Size = New System.Drawing.Size(16, 16)
        Me.picSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picSearch.TabIndex = 1
        Me.picSearch.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(7, 6)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(242, 22)
        Me.txtSearch.TabIndex = 0
        '
        'tcMain
        '
        Me.tcMain.Controls.Add(Me.tpMovies)
        Me.tcMain.Controls.Add(Me.tpShows)
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.tcMain.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tcMain.ItemSize = New System.Drawing.Size(50, 19)
        Me.tcMain.Location = New System.Drawing.Point(0, 0)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(364, 21)
        Me.tcMain.TabIndex = 0
        Me.tcMain.TabStop = False
        '
        'tpMovies
        '
        Me.tpMovies.Location = New System.Drawing.Point(4, 23)
        Me.tpMovies.Name = "tpMovies"
        Me.tpMovies.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMovies.Size = New System.Drawing.Size(356, 0)
        Me.tpMovies.TabIndex = 0
        Me.tpMovies.Text = "Movies"
        Me.tpMovies.UseVisualStyleBackColor = True
        '
        'tpShows
        '
        Me.tpShows.Location = New System.Drawing.Point(4, 23)
        Me.tpShows.Name = "tpShows"
        Me.tpShows.Padding = New System.Windows.Forms.Padding(3)
        Me.tpShows.Size = New System.Drawing.Size(356, 0)
        Me.tpShows.TabIndex = 1
        Me.tpShows.Text = "TV Shows"
        Me.tpShows.UseVisualStyleBackColor = True
        '
        'pnlFilter
        '
        Me.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilter.Controls.Add(Me.gbSort)
        Me.pnlFilter.Controls.Add(Me.btnClearFilters)
        Me.pnlFilter.Controls.Add(Me.gbFilterGeneral)
        Me.pnlFilter.Controls.Add(Me.gbFilterSpecific)
        Me.pnlFilter.Controls.Add(Me.btnFilterDown)
        Me.pnlFilter.Controls.Add(Me.btnFilterUp)
        Me.pnlFilter.Controls.Add(Me.lblFilter)
        Me.pnlFilter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFilter.Location = New System.Drawing.Point(0, 507)
        Me.pnlFilter.Name = "pnlFilter"
        Me.pnlFilter.Size = New System.Drawing.Size(364, 180)
        Me.pnlFilter.TabIndex = 12
        Me.pnlFilter.Visible = False
        '
        'gbSort
        '
        Me.gbSort.Controls.Add(Me.btnIMDBRating)
        Me.gbSort.Controls.Add(Me.btnSortTitle)
        Me.gbSort.Controls.Add(Me.btnSortDate)
        Me.gbSort.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbSort.Location = New System.Drawing.Point(3, 81)
        Me.gbSort.Name = "gbSort"
        Me.gbSort.Size = New System.Drawing.Size(131, 77)
        Me.gbSort.TabIndex = 4
        Me.gbSort.TabStop = False
        Me.gbSort.Text = "Extra Sorting"
        '
        'btnIMDBRating
        '
        Me.btnIMDBRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnIMDBRating.Image = Global.Ember_Media_Manager.My.Resources.Resources.desc
        Me.btnIMDBRating.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnIMDBRating.Location = New System.Drawing.Point(7, 53)
        Me.btnIMDBRating.Name = "btnIMDBRating"
        Me.btnIMDBRating.Size = New System.Drawing.Size(117, 21)
        Me.btnIMDBRating.TabIndex = 2
        Me.btnIMDBRating.Text = "IMDB Rating"
        Me.btnIMDBRating.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnIMDBRating.UseVisualStyleBackColor = True
        '
        'btnSortTitle
        '
        Me.btnSortTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSortTitle.Image = Global.Ember_Media_Manager.My.Resources.Resources.desc
        Me.btnSortTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSortTitle.Location = New System.Drawing.Point(7, 33)
        Me.btnSortTitle.Name = "btnSortTitle"
        Me.btnSortTitle.Size = New System.Drawing.Size(117, 21)
        Me.btnSortTitle.TabIndex = 1
        Me.btnSortTitle.Text = "Sort Title"
        Me.btnSortTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSortTitle.UseVisualStyleBackColor = True
        '
        'btnSortDate
        '
        Me.btnSortDate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSortDate.Image = Global.Ember_Media_Manager.My.Resources.Resources.desc
        Me.btnSortDate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSortDate.Location = New System.Drawing.Point(7, 13)
        Me.btnSortDate.Name = "btnSortDate"
        Me.btnSortDate.Size = New System.Drawing.Size(117, 21)
        Me.btnSortDate.TabIndex = 0
        Me.btnSortDate.Text = "Date Added"
        Me.btnSortDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSortDate.UseVisualStyleBackColor = True
        '
        'btnClearFilters
        '
        Me.btnClearFilters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClearFilters.Image = CType(resources.GetObject("btnClearFilters.Image"), System.Drawing.Image)
        Me.btnClearFilters.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearFilters.Location = New System.Drawing.Point(22, 160)
        Me.btnClearFilters.Name = "btnClearFilters"
        Me.btnClearFilters.Size = New System.Drawing.Size(92, 20)
        Me.btnClearFilters.TabIndex = 5
        Me.btnClearFilters.Text = "Clear Filters"
        Me.btnClearFilters.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClearFilters.UseVisualStyleBackColor = True
        '
        'gbFilterGeneral
        '
        Me.gbFilterGeneral.Controls.Add(Me.chkFilterTolerance)
        Me.gbFilterGeneral.Controls.Add(Me.chkFilterMissing)
        Me.gbFilterGeneral.Controls.Add(Me.chkFilterDupe)
        Me.gbFilterGeneral.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFilterGeneral.Location = New System.Drawing.Point(3, 22)
        Me.gbFilterGeneral.Name = "gbFilterGeneral"
        Me.gbFilterGeneral.Size = New System.Drawing.Size(131, 59)
        Me.gbFilterGeneral.TabIndex = 3
        Me.gbFilterGeneral.TabStop = False
        Me.gbFilterGeneral.Text = "General"
        '
        'chkFilterTolerance
        '
        Me.chkFilterTolerance.AutoSize = True
        Me.chkFilterTolerance.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterTolerance.Location = New System.Drawing.Point(7, 41)
        Me.chkFilterTolerance.Name = "chkFilterTolerance"
        Me.chkFilterTolerance.Size = New System.Drawing.Size(112, 17)
        Me.chkFilterTolerance.TabIndex = 2
        Me.chkFilterTolerance.Text = "Out of Tolerance"
        Me.chkFilterTolerance.UseVisualStyleBackColor = True
        '
        'chkFilterMissing
        '
        Me.chkFilterMissing.AutoSize = True
        Me.chkFilterMissing.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterMissing.Location = New System.Drawing.Point(7, 27)
        Me.chkFilterMissing.Name = "chkFilterMissing"
        Me.chkFilterMissing.Size = New System.Drawing.Size(96, 17)
        Me.chkFilterMissing.TabIndex = 1
        Me.chkFilterMissing.Text = "Missing Items"
        Me.chkFilterMissing.UseVisualStyleBackColor = True
        '
        'chkFilterDupe
        '
        Me.chkFilterDupe.AutoSize = True
        Me.chkFilterDupe.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterDupe.Location = New System.Drawing.Point(7, 13)
        Me.chkFilterDupe.Name = "chkFilterDupe"
        Me.chkFilterDupe.Size = New System.Drawing.Size(80, 17)
        Me.chkFilterDupe.TabIndex = 0
        Me.chkFilterDupe.Text = "Duplicates"
        Me.chkFilterDupe.UseVisualStyleBackColor = True
        '
        'gbFilterSpecific
        '
        Me.gbFilterSpecific.Controls.Add(Me.txtFilterSource)
        Me.gbFilterSpecific.Controls.Add(Me.lblFilterFileSource)
        Me.gbFilterSpecific.Controls.Add(Me.cbFilterFileSource)
        Me.gbFilterSpecific.Controls.Add(Me.chkFilterLock)
        Me.gbFilterSpecific.Controls.Add(Me.gbFilterModifier)
        Me.gbFilterSpecific.Controls.Add(Me.chkFilterNew)
        Me.gbFilterSpecific.Controls.Add(Me.cbFilterYear)
        Me.gbFilterSpecific.Controls.Add(Me.chkFilterMark)
        Me.gbFilterSpecific.Controls.Add(Me.cbFilterYearMod)
        Me.gbFilterSpecific.Controls.Add(Me.lblFilterYear)
        Me.gbFilterSpecific.Controls.Add(Me.txtFilterGenre)
        Me.gbFilterSpecific.Controls.Add(Me.lblFilterSource)
        Me.gbFilterSpecific.Controls.Add(Me.lblFilterGenre)
        Me.gbFilterSpecific.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFilterSpecific.Location = New System.Drawing.Point(135, 22)
        Me.gbFilterSpecific.Name = "gbFilterSpecific"
        Me.gbFilterSpecific.Size = New System.Drawing.Size(224, 155)
        Me.gbFilterSpecific.TabIndex = 6
        Me.gbFilterSpecific.TabStop = False
        Me.gbFilterSpecific.Text = "Specific"
        '
        'txtFilterSource
        '
        Me.txtFilterSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilterSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilterSource.Location = New System.Drawing.Point(50, 129)
        Me.txtFilterSource.Name = "txtFilterSource"
        Me.txtFilterSource.ReadOnly = True
        Me.txtFilterSource.Size = New System.Drawing.Size(166, 22)
        Me.txtFilterSource.TabIndex = 11
        '
        'lblFilterFileSource
        '
        Me.lblFilterFileSource.AutoSize = True
        Me.lblFilterFileSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilterFileSource.Location = New System.Drawing.Point(6, 108)
        Me.lblFilterFileSource.Name = "lblFilterFileSource"
        Me.lblFilterFileSource.Size = New System.Drawing.Size(66, 13)
        Me.lblFilterFileSource.TabIndex = 8
        Me.lblFilterFileSource.Text = "File Source:"
        '
        'cbFilterFileSource
        '
        Me.cbFilterFileSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterFileSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterFileSource.FormattingEnabled = True
        Me.cbFilterFileSource.Location = New System.Drawing.Point(77, 105)
        Me.cbFilterFileSource.Name = "cbFilterFileSource"
        Me.cbFilterFileSource.Size = New System.Drawing.Size(139, 21)
        Me.cbFilterFileSource.TabIndex = 9
        '
        'chkFilterLock
        '
        Me.chkFilterLock.AutoSize = True
        Me.chkFilterLock.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFilterLock.Location = New System.Drawing.Point(73, 18)
        Me.chkFilterLock.Name = "chkFilterLock"
        Me.chkFilterLock.Size = New System.Drawing.Size(62, 17)
        Me.chkFilterLock.TabIndex = 2
        Me.chkFilterLock.Text = "Locked"
        Me.chkFilterLock.UseVisualStyleBackColor = True
        '
        'gbFilterModifier
        '
        Me.gbFilterModifier.Controls.Add(Me.rbFilterAnd)
        Me.gbFilterModifier.Controls.Add(Me.rbFilterOr)
        Me.gbFilterModifier.Location = New System.Drawing.Point(140, 10)
        Me.gbFilterModifier.Name = "gbFilterModifier"
        Me.gbFilterModifier.Size = New System.Drawing.Size(76, 43)
        Me.gbFilterModifier.TabIndex = 3
        Me.gbFilterModifier.TabStop = False
        Me.gbFilterModifier.Text = "Modifier"
        '
        'rbFilterAnd
        '
        Me.rbFilterAnd.AutoSize = True
        Me.rbFilterAnd.Checked = True
        Me.rbFilterAnd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbFilterAnd.Location = New System.Drawing.Point(6, 11)
        Me.rbFilterAnd.Name = "rbFilterAnd"
        Me.rbFilterAnd.Size = New System.Drawing.Size(46, 17)
        Me.rbFilterAnd.TabIndex = 0
        Me.rbFilterAnd.TabStop = True
        Me.rbFilterAnd.Text = "And"
        Me.rbFilterAnd.UseVisualStyleBackColor = True
        '
        'rbFilterOr
        '
        Me.rbFilterOr.AutoSize = True
        Me.rbFilterOr.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbFilterOr.Location = New System.Drawing.Point(6, 25)
        Me.rbFilterOr.Name = "rbFilterOr"
        Me.rbFilterOr.Size = New System.Drawing.Size(38, 17)
        Me.rbFilterOr.TabIndex = 1
        Me.rbFilterOr.Text = "Or"
        Me.rbFilterOr.UseVisualStyleBackColor = True
        '
        'chkFilterNew
        '
        Me.chkFilterNew.AutoSize = True
        Me.chkFilterNew.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterNew.Location = New System.Drawing.Point(9, 18)
        Me.chkFilterNew.Name = "chkFilterNew"
        Me.chkFilterNew.Size = New System.Drawing.Size(49, 17)
        Me.chkFilterNew.TabIndex = 0
        Me.chkFilterNew.Text = "New"
        Me.chkFilterNew.UseVisualStyleBackColor = True
        '
        'cbFilterYear
        '
        Me.cbFilterYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterYear.FormattingEnabled = True
        Me.cbFilterYear.Items.AddRange(New Object() {"=", ">", "<", "!="})
        Me.cbFilterYear.Location = New System.Drawing.Point(141, 81)
        Me.cbFilterYear.Name = "cbFilterYear"
        Me.cbFilterYear.Size = New System.Drawing.Size(75, 21)
        Me.cbFilterYear.TabIndex = 7
        '
        'chkFilterMark
        '
        Me.chkFilterMark.AutoSize = True
        Me.chkFilterMark.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterMark.Location = New System.Drawing.Point(9, 36)
        Me.chkFilterMark.Name = "chkFilterMark"
        Me.chkFilterMark.Size = New System.Drawing.Size(65, 17)
        Me.chkFilterMark.TabIndex = 1
        Me.chkFilterMark.Text = "Marked"
        Me.chkFilterMark.UseVisualStyleBackColor = True
        '
        'cbFilterYearMod
        '
        Me.cbFilterYearMod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterYearMod.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterYearMod.FormattingEnabled = True
        Me.cbFilterYearMod.Items.AddRange(New Object() {"=", ">", "<", "<>"})
        Me.cbFilterYearMod.Location = New System.Drawing.Point(77, 81)
        Me.cbFilterYearMod.Name = "cbFilterYearMod"
        Me.cbFilterYearMod.Size = New System.Drawing.Size(59, 21)
        Me.cbFilterYearMod.TabIndex = 6
        '
        'lblFilterYear
        '
        Me.lblFilterYear.AutoSize = True
        Me.lblFilterYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilterYear.Location = New System.Drawing.Point(6, 83)
        Me.lblFilterYear.Name = "lblFilterYear"
        Me.lblFilterYear.Size = New System.Drawing.Size(31, 13)
        Me.lblFilterYear.TabIndex = 5
        Me.lblFilterYear.Text = "Year:"
        '
        'txtFilterGenre
        '
        Me.txtFilterGenre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilterGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilterGenre.Location = New System.Drawing.Point(50, 56)
        Me.txtFilterGenre.Name = "txtFilterGenre"
        Me.txtFilterGenre.ReadOnly = True
        Me.txtFilterGenre.Size = New System.Drawing.Size(166, 22)
        Me.txtFilterGenre.TabIndex = 4
        '
        'lblFilterSource
        '
        Me.lblFilterSource.AutoSize = True
        Me.lblFilterSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilterSource.Location = New System.Drawing.Point(6, 132)
        Me.lblFilterSource.Name = "lblFilterSource"
        Me.lblFilterSource.Size = New System.Drawing.Size(45, 13)
        Me.lblFilterSource.TabIndex = 10
        Me.lblFilterSource.Text = "Source:"
        '
        'lblFilterGenre
        '
        Me.lblFilterGenre.AutoSize = True
        Me.lblFilterGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilterGenre.Location = New System.Drawing.Point(6, 58)
        Me.lblFilterGenre.Name = "lblFilterGenre"
        Me.lblFilterGenre.Size = New System.Drawing.Size(41, 13)
        Me.lblFilterGenre.TabIndex = 31
        Me.lblFilterGenre.Text = "Genre:"
        '
        'btnFilterDown
        '
        Me.btnFilterDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFilterDown.BackColor = System.Drawing.SystemColors.Control
        Me.btnFilterDown.Enabled = False
        Me.btnFilterDown.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFilterDown.Location = New System.Drawing.Point(324, 1)
        Me.btnFilterDown.Name = "btnFilterDown"
        Me.btnFilterDown.Size = New System.Drawing.Size(30, 22)
        Me.btnFilterDown.TabIndex = 2
        Me.btnFilterDown.TabStop = False
        Me.btnFilterDown.Text = "v"
        Me.btnFilterDown.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFilterDown.UseVisualStyleBackColor = False
        '
        'btnFilterUp
        '
        Me.btnFilterUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFilterUp.BackColor = System.Drawing.SystemColors.Control
        Me.btnFilterUp.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFilterUp.Location = New System.Drawing.Point(292, 1)
        Me.btnFilterUp.Name = "btnFilterUp"
        Me.btnFilterUp.Size = New System.Drawing.Size(30, 22)
        Me.btnFilterUp.TabIndex = 1
        Me.btnFilterUp.TabStop = False
        Me.btnFilterUp.Text = "^"
        Me.btnFilterUp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFilterUp.UseVisualStyleBackColor = False
        '
        'lblFilter
        '
        Me.lblFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFilter.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilter.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilter.Location = New System.Drawing.Point(4, 3)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(354, 17)
        Me.lblFilter.TabIndex = 0
        Me.lblFilter.Text = "Filters"
        Me.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.lblTitle)
        Me.pnlTop.Controls.Add(Me.pnlInfoIcons)
        Me.pnlTop.Controls.Add(Me.lblRuntime)
        Me.pnlTop.Controls.Add(Me.lblTagline)
        Me.pnlTop.Controls.Add(Me.pnlRating)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 25)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(976, 74)
        Me.pnlTop.TabIndex = 9
        Me.pnlTop.Visible = False
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.Black
        Me.lblTitle.Location = New System.Drawing.Point(4, 2)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(0, 20)
        Me.lblTitle.TabIndex = 25
        Me.lblTitle.UseMnemonic = False
        '
        'pnlInfoIcons
        '
        Me.pnlInfoIcons.BackColor = System.Drawing.Color.Transparent
        Me.pnlInfoIcons.Controls.Add(Me.lblOriginalTitle)
        Me.pnlInfoIcons.Controls.Add(Me.lblStudio)
        Me.pnlInfoIcons.Controls.Add(Me.pbVType)
        Me.pnlInfoIcons.Controls.Add(Me.pbStudio)
        Me.pnlInfoIcons.Controls.Add(Me.pbVideo)
        Me.pnlInfoIcons.Controls.Add(Me.pbAudio)
        Me.pnlInfoIcons.Controls.Add(Me.pbResolution)
        Me.pnlInfoIcons.Controls.Add(Me.pbChannels)
        Me.pnlInfoIcons.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlInfoIcons.Location = New System.Drawing.Point(584, 0)
        Me.pnlInfoIcons.Name = "pnlInfoIcons"
        Me.pnlInfoIcons.Size = New System.Drawing.Size(390, 72)
        Me.pnlInfoIcons.TabIndex = 31
        '
        'lblOriginalTitle
        '
        Me.lblOriginalTitle.AutoSize = True
        Me.lblOriginalTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblOriginalTitle.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblOriginalTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOriginalTitle.ForeColor = System.Drawing.Color.Black
        Me.lblOriginalTitle.Location = New System.Drawing.Point(390, 0)
        Me.lblOriginalTitle.Name = "lblOriginalTitle"
        Me.lblOriginalTitle.Size = New System.Drawing.Size(0, 20)
        Me.lblOriginalTitle.TabIndex = 38
        Me.lblOriginalTitle.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblOriginalTitle.UseMnemonic = False
        '
        'lblStudio
        '
        Me.lblStudio.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStudio.Location = New System.Drawing.Point(223, 55)
        Me.lblStudio.Name = "lblStudio"
        Me.lblStudio.Size = New System.Drawing.Size(167, 18)
        Me.lblStudio.TabIndex = 37
        Me.lblStudio.Text = "           "
        Me.lblStudio.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'pbVType
        '
        Me.pbVType.BackColor = System.Drawing.Color.Transparent
        Me.pbVType.Location = New System.Drawing.Point(65, 15)
        Me.pbVType.Name = "pbVType"
        Me.pbVType.Size = New System.Drawing.Size(64, 44)
        Me.pbVType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbVType.TabIndex = 36
        Me.pbVType.TabStop = False
        '
        'pbStudio
        '
        Me.pbStudio.BackColor = System.Drawing.Color.Transparent
        Me.pbStudio.Location = New System.Drawing.Point(325, 15)
        Me.pbStudio.Name = "pbStudio"
        Me.pbStudio.Size = New System.Drawing.Size(64, 44)
        Me.pbStudio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbStudio.TabIndex = 31
        Me.pbStudio.TabStop = False
        '
        'pbVideo
        '
        Me.pbVideo.BackColor = System.Drawing.Color.Transparent
        Me.pbVideo.Location = New System.Drawing.Point(0, 15)
        Me.pbVideo.Name = "pbVideo"
        Me.pbVideo.Size = New System.Drawing.Size(64, 44)
        Me.pbVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbVideo.TabIndex = 33
        Me.pbVideo.TabStop = False
        '
        'pbAudio
        '
        Me.pbAudio.BackColor = System.Drawing.Color.Transparent
        Me.pbAudio.Location = New System.Drawing.Point(195, 15)
        Me.pbAudio.Name = "pbAudio"
        Me.pbAudio.Size = New System.Drawing.Size(64, 44)
        Me.pbAudio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudio.TabIndex = 35
        Me.pbAudio.TabStop = False
        '
        'pbResolution
        '
        Me.pbResolution.BackColor = System.Drawing.Color.Transparent
        Me.pbResolution.Location = New System.Drawing.Point(130, 15)
        Me.pbResolution.Name = "pbResolution"
        Me.pbResolution.Size = New System.Drawing.Size(64, 44)
        Me.pbResolution.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbResolution.TabIndex = 34
        Me.pbResolution.TabStop = False
        '
        'pbChannels
        '
        Me.pbChannels.BackColor = System.Drawing.Color.Transparent
        Me.pbChannels.Location = New System.Drawing.Point(260, 15)
        Me.pbChannels.Name = "pbChannels"
        Me.pbChannels.Size = New System.Drawing.Size(64, 44)
        Me.pbChannels.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbChannels.TabIndex = 32
        Me.pbChannels.TabStop = False
        '
        'lblRuntime
        '
        Me.lblRuntime.AutoSize = True
        Me.lblRuntime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuntime.ForeColor = System.Drawing.Color.Black
        Me.lblRuntime.Location = New System.Drawing.Point(213, 32)
        Me.lblRuntime.Name = "lblRuntime"
        Me.lblRuntime.Size = New System.Drawing.Size(0, 13)
        Me.lblRuntime.TabIndex = 32
        Me.lblRuntime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTagline
        '
        Me.lblTagline.AutoSize = True
        Me.lblTagline.BackColor = System.Drawing.Color.Transparent
        Me.lblTagline.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTagline.ForeColor = System.Drawing.Color.Black
        Me.lblTagline.Location = New System.Drawing.Point(5, 55)
        Me.lblTagline.Name = "lblTagline"
        Me.lblTagline.Size = New System.Drawing.Size(0, 13)
        Me.lblTagline.TabIndex = 26
        Me.lblTagline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTagline.UseMnemonic = False
        '
        'pnlRating
        '
        Me.pnlRating.BackColor = System.Drawing.Color.Transparent
        Me.pnlRating.Controls.Add(Me.pbStar5)
        Me.pnlRating.Controls.Add(Me.pbStar4)
        Me.pnlRating.Controls.Add(Me.pbStar3)
        Me.pnlRating.Controls.Add(Me.pbStar2)
        Me.pnlRating.Controls.Add(Me.pbStar1)
        Me.pnlRating.Controls.Add(Me.lblVotes)
        Me.pnlRating.Location = New System.Drawing.Point(6, 24)
        Me.pnlRating.Name = "pnlRating"
        Me.pnlRating.Size = New System.Drawing.Size(206, 27)
        Me.pnlRating.TabIndex = 24
        '
        'pbStar5
        '
        Me.pbStar5.Location = New System.Drawing.Point(97, 1)
        Me.pbStar5.Name = "pbStar5"
        Me.pbStar5.Size = New System.Drawing.Size(24, 24)
        Me.pbStar5.TabIndex = 27
        Me.pbStar5.TabStop = False
        '
        'pbStar4
        '
        Me.pbStar4.Location = New System.Drawing.Point(73, 1)
        Me.pbStar4.Name = "pbStar4"
        Me.pbStar4.Size = New System.Drawing.Size(24, 24)
        Me.pbStar4.TabIndex = 26
        Me.pbStar4.TabStop = False
        '
        'pbStar3
        '
        Me.pbStar3.Location = New System.Drawing.Point(49, 1)
        Me.pbStar3.Name = "pbStar3"
        Me.pbStar3.Size = New System.Drawing.Size(24, 24)
        Me.pbStar3.TabIndex = 25
        Me.pbStar3.TabStop = False
        '
        'pbStar2
        '
        Me.pbStar2.Location = New System.Drawing.Point(25, 1)
        Me.pbStar2.Name = "pbStar2"
        Me.pbStar2.Size = New System.Drawing.Size(24, 24)
        Me.pbStar2.TabIndex = 24
        Me.pbStar2.TabStop = False
        '
        'pbStar1
        '
        Me.pbStar1.Location = New System.Drawing.Point(1, 1)
        Me.pbStar1.Name = "pbStar1"
        Me.pbStar1.Size = New System.Drawing.Size(24, 24)
        Me.pbStar1.TabIndex = 23
        Me.pbStar1.TabStop = False
        '
        'lblVotes
        '
        Me.lblVotes.AutoSize = True
        Me.lblVotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVotes.ForeColor = System.Drawing.Color.Black
        Me.lblVotes.Location = New System.Drawing.Point(123, 8)
        Me.lblVotes.Name = "lblVotes"
        Me.lblVotes.Size = New System.Drawing.Size(0, 13)
        Me.lblVotes.TabIndex = 22
        Me.lblVotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlCancel
        '
        Me.pnlCancel.BackColor = System.Drawing.Color.LightGray
        Me.pnlCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCancel.Controls.Add(Me.prbCanceling)
        Me.pnlCancel.Controls.Add(Me.lblCanceling)
        Me.pnlCancel.Controls.Add(Me.btnCancel)
        Me.pnlCancel.Location = New System.Drawing.Point(273, 100)
        Me.pnlCancel.Name = "pnlCancel"
        Me.pnlCancel.Size = New System.Drawing.Size(214, 63)
        Me.pnlCancel.TabIndex = 8
        Me.pnlCancel.Visible = False
        '
        'prbCanceling
        '
        Me.prbCanceling.Location = New System.Drawing.Point(5, 32)
        Me.prbCanceling.MarqueeAnimationSpeed = 25
        Me.prbCanceling.Name = "prbCanceling"
        Me.prbCanceling.Size = New System.Drawing.Size(203, 20)
        Me.prbCanceling.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.prbCanceling.TabIndex = 2
        Me.prbCanceling.Visible = False
        '
        'lblCanceling
        '
        Me.lblCanceling.AutoSize = True
        Me.lblCanceling.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCanceling.Location = New System.Drawing.Point(4, 12)
        Me.lblCanceling.Name = "lblCanceling"
        Me.lblCanceling.Size = New System.Drawing.Size(128, 17)
        Me.lblCanceling.TabIndex = 1
        Me.lblCanceling.Text = "Canceling Scraper..."
        Me.lblCanceling.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(4, 4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(205, 55)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.TabStop = False
        Me.btnCancel.Text = "Cancel Scraper"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'pnlAllSeason
        '
        Me.pnlAllSeason.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlAllSeason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlAllSeason.Controls.Add(Me.pbAllSeason)
        Me.pnlAllSeason.Location = New System.Drawing.Point(508, 112)
        Me.pnlAllSeason.Name = "pnlAllSeason"
        Me.pnlAllSeason.Size = New System.Drawing.Size(131, 169)
        Me.pnlAllSeason.TabIndex = 3
        Me.pnlAllSeason.Visible = False
        '
        'pbAllSeason
        '
        Me.pbAllSeason.BackColor = System.Drawing.SystemColors.Control
        Me.pbAllSeason.Location = New System.Drawing.Point(4, 4)
        Me.pbAllSeason.Name = "pbAllSeason"
        Me.pbAllSeason.Size = New System.Drawing.Size(121, 159)
        Me.pbAllSeason.TabIndex = 0
        Me.pbAllSeason.TabStop = False
        '
        'pbAllSeasonCache
        '
        Me.pbAllSeasonCache.Location = New System.Drawing.Point(333, 107)
        Me.pbAllSeasonCache.Name = "pbAllSeasonCache"
        Me.pbAllSeasonCache.Size = New System.Drawing.Size(115, 111)
        Me.pbAllSeasonCache.TabIndex = 13
        Me.pbAllSeasonCache.TabStop = False
        Me.pbAllSeasonCache.Visible = False
        '
        'pnlNoInfo
        '
        Me.pnlNoInfo.BackColor = System.Drawing.Color.LightGray
        Me.pnlNoInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlNoInfo.Controls.Add(Me.pnlNoInfoBG)
        Me.pnlNoInfo.Location = New System.Drawing.Point(241, 300)
        Me.pnlNoInfo.Name = "pnlNoInfo"
        Me.pnlNoInfo.Size = New System.Drawing.Size(259, 143)
        Me.pnlNoInfo.TabIndex = 8
        Me.pnlNoInfo.Visible = False
        '
        'pnlNoInfoBG
        '
        Me.pnlNoInfoBG.BackColor = System.Drawing.Color.White
        Me.pnlNoInfoBG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlNoInfoBG.Controls.Add(Me.pbNoInfo)
        Me.pnlNoInfoBG.Controls.Add(Me.lblNoInfo)
        Me.pnlNoInfoBG.Location = New System.Drawing.Point(3, 4)
        Me.pnlNoInfoBG.Name = "pnlNoInfoBG"
        Me.pnlNoInfoBG.Size = New System.Drawing.Size(251, 133)
        Me.pnlNoInfoBG.TabIndex = 0
        '
        'pbNoInfo
        '
        Me.pbNoInfo.Image = CType(resources.GetObject("pbNoInfo.Image"), System.Drawing.Image)
        Me.pbNoInfo.Location = New System.Drawing.Point(7, 38)
        Me.pbNoInfo.Name = "pbNoInfo"
        Me.pbNoInfo.Size = New System.Drawing.Size(63, 63)
        Me.pbNoInfo.TabIndex = 1
        Me.pbNoInfo.TabStop = False
        '
        'lblNoInfo
        '
        Me.lblNoInfo.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblNoInfo.Location = New System.Drawing.Point(71, 29)
        Me.lblNoInfo.Name = "lblNoInfo"
        Me.lblNoInfo.Size = New System.Drawing.Size(173, 78)
        Me.lblNoInfo.TabIndex = 0
        Me.lblNoInfo.Text = "No Information is Available for This Movie"
        Me.lblNoInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlInfoPanel
        '
        Me.pnlInfoPanel.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlInfoPanel.Controls.Add(Me.txtCerts)
        Me.pnlInfoPanel.Controls.Add(Me.lblCertsHeader)
        Me.pnlInfoPanel.Controls.Add(Me.lblReleaseDate)
        Me.pnlInfoPanel.Controls.Add(Me.lblReleaseDateHeader)
        Me.pnlInfoPanel.Controls.Add(Me.btnMid)
        Me.pnlInfoPanel.Controls.Add(Me.pbMILoading)
        Me.pnlInfoPanel.Controls.Add(Me.btnMetaDataRefresh)
        Me.pnlInfoPanel.Controls.Add(Me.lblMetaDataHeader)
        Me.pnlInfoPanel.Controls.Add(Me.txtMetaData)
        Me.pnlInfoPanel.Controls.Add(Me.btnPlay)
        Me.pnlInfoPanel.Controls.Add(Me.txtFilePath)
        Me.pnlInfoPanel.Controls.Add(Me.lblFilePathHeader)
        Me.pnlInfoPanel.Controls.Add(Me.txtIMDBID)
        Me.pnlInfoPanel.Controls.Add(Me.lblIMDBHeader)
        Me.pnlInfoPanel.Controls.Add(Me.lblDirector)
        Me.pnlInfoPanel.Controls.Add(Me.lblDirectorHeader)
        Me.pnlInfoPanel.Controls.Add(Me.pnlActors)
        Me.pnlInfoPanel.Controls.Add(Me.lblOutlineHeader)
        Me.pnlInfoPanel.Controls.Add(Me.txtOutline)
        Me.pnlInfoPanel.Controls.Add(Me.pnlTop250)
        Me.pnlInfoPanel.Controls.Add(Me.lblPlotHeader)
        Me.pnlInfoPanel.Controls.Add(Me.txtPlot)
        Me.pnlInfoPanel.Controls.Add(Me.btnDown)
        Me.pnlInfoPanel.Controls.Add(Me.btnUp)
        Me.pnlInfoPanel.Controls.Add(Me.lblInfoPanelHeader)
        Me.pnlInfoPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlInfoPanel.Location = New System.Drawing.Point(0, 345)
        Me.pnlInfoPanel.Name = "pnlInfoPanel"
        Me.pnlInfoPanel.Size = New System.Drawing.Size(976, 342)
        Me.pnlInfoPanel.TabIndex = 10
        '
        'txtCerts
        '
        Me.txtCerts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCerts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCerts.Location = New System.Drawing.Point(117, 208)
        Me.txtCerts.Name = "txtCerts"
        Me.txtCerts.ReadOnly = True
        Me.txtCerts.Size = New System.Drawing.Size(540, 22)
        Me.txtCerts.TabIndex = 3
        Me.txtCerts.TabStop = False
        '
        'lblCertsHeader
        '
        Me.lblCertsHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCertsHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblCertsHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCertsHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCertsHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblCertsHeader.Location = New System.Drawing.Point(117, 188)
        Me.lblCertsHeader.Name = "lblCertsHeader"
        Me.lblCertsHeader.Size = New System.Drawing.Size(540, 17)
        Me.lblCertsHeader.TabIndex = 2
        Me.lblCertsHeader.Text = "Certifications"
        Me.lblCertsHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblReleaseDate
        '
        Me.lblReleaseDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblReleaseDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReleaseDate.ForeColor = System.Drawing.Color.Black
        Me.lblReleaseDate.Location = New System.Drawing.Point(491, 48)
        Me.lblReleaseDate.Name = "lblReleaseDate"
        Me.lblReleaseDate.Size = New System.Drawing.Size(105, 16)
        Me.lblReleaseDate.TabIndex = 39
        Me.lblReleaseDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReleaseDateHeader
        '
        Me.lblReleaseDateHeader.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblReleaseDateHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblReleaseDateHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReleaseDateHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReleaseDateHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblReleaseDateHeader.Location = New System.Drawing.Point(491, 27)
        Me.lblReleaseDateHeader.Name = "lblReleaseDateHeader"
        Me.lblReleaseDateHeader.Size = New System.Drawing.Size(105, 17)
        Me.lblReleaseDateHeader.TabIndex = 38
        Me.lblReleaseDateHeader.Text = "Release Date"
        Me.lblReleaseDateHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnMid
        '
        Me.btnMid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMid.BackColor = System.Drawing.SystemColors.Control
        Me.btnMid.Location = New System.Drawing.Point(905, 1)
        Me.btnMid.Name = "btnMid"
        Me.btnMid.Size = New System.Drawing.Size(30, 22)
        Me.btnMid.TabIndex = 37
        Me.btnMid.TabStop = False
        Me.btnMid.Text = "-"
        Me.btnMid.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnMid.UseVisualStyleBackColor = False
        '
        'pbMILoading
        '
        Me.pbMILoading.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbMILoading.Image = CType(resources.GetObject("pbMILoading.Image"), System.Drawing.Image)
        Me.pbMILoading.Location = New System.Drawing.Point(807, 374)
        Me.pbMILoading.Name = "pbMILoading"
        Me.pbMILoading.Size = New System.Drawing.Size(41, 39)
        Me.pbMILoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbMILoading.TabIndex = 36
        Me.pbMILoading.TabStop = False
        Me.pbMILoading.Visible = False
        '
        'btnMetaDataRefresh
        '
        Me.btnMetaDataRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMetaDataRefresh.Location = New System.Drawing.Point(894, 278)
        Me.btnMetaDataRefresh.Name = "btnMetaDataRefresh"
        Me.btnMetaDataRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnMetaDataRefresh.TabIndex = 9
        Me.btnMetaDataRefresh.TabStop = False
        Me.btnMetaDataRefresh.Text = "Refresh"
        Me.btnMetaDataRefresh.UseVisualStyleBackColor = True
        '
        'lblMetaDataHeader
        '
        Me.lblMetaDataHeader.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMetaDataHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblMetaDataHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMetaDataHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMetaDataHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblMetaDataHeader.Location = New System.Drawing.Point(670, 282)
        Me.lblMetaDataHeader.Name = "lblMetaDataHeader"
        Me.lblMetaDataHeader.Size = New System.Drawing.Size(294, 17)
        Me.lblMetaDataHeader.TabIndex = 8
        Me.lblMetaDataHeader.Text = "Meta Data"
        Me.lblMetaDataHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMetaData
        '
        Me.txtMetaData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMetaData.BackColor = System.Drawing.Color.Gainsboro
        Me.txtMetaData.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMetaData.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMetaData.ForeColor = System.Drawing.Color.Black
        Me.txtMetaData.Location = New System.Drawing.Point(670, 303)
        Me.txtMetaData.Multiline = True
        Me.txtMetaData.Name = "txtMetaData"
        Me.txtMetaData.ReadOnly = True
        Me.txtMetaData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMetaData.Size = New System.Drawing.Size(296, 184)
        Me.txtMetaData.TabIndex = 10
        Me.txtMetaData.TabStop = False
        '
        'btnPlay
        '
        Me.btnPlay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPlay.Image = Global.Ember_Media_Manager.My.Resources.Resources.Play_Icon
        Me.btnPlay.Location = New System.Drawing.Point(638, 254)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(20, 20)
        Me.btnPlay.TabIndex = 6
        Me.btnPlay.TabStop = False
        Me.btnPlay.UseVisualStyleBackColor = True
        '
        'txtFilePath
        '
        Me.txtFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilePath.Location = New System.Drawing.Point(3, 254)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(632, 22)
        Me.txtFilePath.TabIndex = 5
        Me.txtFilePath.TabStop = False
        '
        'lblFilePathHeader
        '
        Me.lblFilePathHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFilePathHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblFilePathHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFilePathHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilePathHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilePathHeader.Location = New System.Drawing.Point(3, 234)
        Me.lblFilePathHeader.Name = "lblFilePathHeader"
        Me.lblFilePathHeader.Size = New System.Drawing.Size(654, 17)
        Me.lblFilePathHeader.TabIndex = 4
        Me.lblFilePathHeader.Text = "File Path"
        Me.lblFilePathHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIMDBID
        '
        Me.txtIMDBID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIMDBID.Location = New System.Drawing.Point(3, 208)
        Me.txtIMDBID.Name = "txtIMDBID"
        Me.txtIMDBID.ReadOnly = True
        Me.txtIMDBID.Size = New System.Drawing.Size(108, 22)
        Me.txtIMDBID.TabIndex = 1
        Me.txtIMDBID.TabStop = False
        '
        'lblIMDBHeader
        '
        Me.lblIMDBHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblIMDBHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIMDBHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIMDBHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblIMDBHeader.Location = New System.Drawing.Point(3, 188)
        Me.lblIMDBHeader.Name = "lblIMDBHeader"
        Me.lblIMDBHeader.Size = New System.Drawing.Size(108, 17)
        Me.lblIMDBHeader.TabIndex = 0
        Me.lblIMDBHeader.Text = "IMDB ID"
        Me.lblIMDBHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDirector
        '
        Me.lblDirector.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDirector.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDirector.ForeColor = System.Drawing.Color.Black
        Me.lblDirector.Location = New System.Drawing.Point(3, 48)
        Me.lblDirector.Name = "lblDirector"
        Me.lblDirector.Size = New System.Drawing.Size(483, 16)
        Me.lblDirector.TabIndex = 27
        Me.lblDirector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDirectorHeader
        '
        Me.lblDirectorHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDirectorHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblDirectorHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDirectorHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDirectorHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblDirectorHeader.Location = New System.Drawing.Point(3, 27)
        Me.lblDirectorHeader.Name = "lblDirectorHeader"
        Me.lblDirectorHeader.Size = New System.Drawing.Size(482, 17)
        Me.lblDirectorHeader.TabIndex = 21
        Me.lblDirectorHeader.Text = "Director"
        Me.lblDirectorHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlActors
        '
        Me.pnlActors.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlActors.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlActors.Controls.Add(Me.pbActLoad)
        Me.pnlActors.Controls.Add(Me.lstActors)
        Me.pnlActors.Controls.Add(Me.pbActors)
        Me.pnlActors.Controls.Add(Me.lblActorsHeader)
        Me.pnlActors.Location = New System.Drawing.Point(669, 29)
        Me.pnlActors.Name = "pnlActors"
        Me.pnlActors.Size = New System.Drawing.Size(302, 244)
        Me.pnlActors.TabIndex = 19
        '
        'pbActLoad
        '
        Me.pbActLoad.Image = CType(resources.GetObject("pbActLoad.Image"), System.Drawing.Image)
        Me.pbActLoad.Location = New System.Drawing.Point(240, 111)
        Me.pbActLoad.Name = "pbActLoad"
        Me.pbActLoad.Size = New System.Drawing.Size(41, 39)
        Me.pbActLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbActLoad.TabIndex = 26
        Me.pbActLoad.TabStop = False
        Me.pbActLoad.Visible = False
        '
        'lstActors
        '
        Me.lstActors.BackColor = System.Drawing.Color.Gainsboro
        Me.lstActors.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstActors.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstActors.ForeColor = System.Drawing.Color.Black
        Me.lstActors.FormattingEnabled = True
        Me.lstActors.Location = New System.Drawing.Point(3, 21)
        Me.lstActors.Name = "lstActors"
        Me.lstActors.Size = New System.Drawing.Size(214, 221)
        Me.lstActors.TabIndex = 28
        Me.lstActors.TabStop = False
        '
        'pbActors
        '
        Me.pbActors.Image = Global.Ember_Media_Manager.My.Resources.Resources.actor_silhouette
        Me.pbActors.Location = New System.Drawing.Point(220, 75)
        Me.pbActors.Name = "pbActors"
        Me.pbActors.Size = New System.Drawing.Size(81, 106)
        Me.pbActors.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbActors.TabIndex = 27
        Me.pbActors.TabStop = False
        '
        'lblActorsHeader
        '
        Me.lblActorsHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblActorsHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblActorsHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActorsHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblActorsHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblActorsHeader.Name = "lblActorsHeader"
        Me.lblActorsHeader.Size = New System.Drawing.Size(301, 17)
        Me.lblActorsHeader.TabIndex = 18
        Me.lblActorsHeader.Text = "Cast"
        Me.lblActorsHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblOutlineHeader
        '
        Me.lblOutlineHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOutlineHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblOutlineHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOutlineHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOutlineHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblOutlineHeader.Location = New System.Drawing.Point(3, 81)
        Me.lblOutlineHeader.Name = "lblOutlineHeader"
        Me.lblOutlineHeader.Size = New System.Drawing.Size(654, 17)
        Me.lblOutlineHeader.TabIndex = 17
        Me.lblOutlineHeader.Text = "Plot Outline"
        Me.lblOutlineHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtOutline
        '
        Me.txtOutline.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOutline.BackColor = System.Drawing.Color.Gainsboro
        Me.txtOutline.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtOutline.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOutline.ForeColor = System.Drawing.Color.Black
        Me.txtOutline.Location = New System.Drawing.Point(3, 103)
        Me.txtOutline.Multiline = True
        Me.txtOutline.Name = "txtOutline"
        Me.txtOutline.ReadOnly = True
        Me.txtOutline.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOutline.Size = New System.Drawing.Size(654, 78)
        Me.txtOutline.TabIndex = 16
        Me.txtOutline.TabStop = False
        '
        'pnlTop250
        '
        Me.pnlTop250.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTop250.Controls.Add(Me.lblTop250)
        Me.pnlTop250.Controls.Add(Me.pbTop250)
        Me.pnlTop250.Location = New System.Drawing.Point(600, 27)
        Me.pnlTop250.Name = "pnlTop250"
        Me.pnlTop250.Size = New System.Drawing.Size(56, 48)
        Me.pnlTop250.TabIndex = 15
        '
        'lblTop250
        '
        Me.lblTop250.BackColor = System.Drawing.Color.Gainsboro
        Me.lblTop250.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTop250.ForeColor = System.Drawing.Color.Black
        Me.lblTop250.Location = New System.Drawing.Point(1, 30)
        Me.lblTop250.Name = "lblTop250"
        Me.lblTop250.Size = New System.Drawing.Size(52, 17)
        Me.lblTop250.TabIndex = 15
        Me.lblTop250.Text = "# 250"
        Me.lblTop250.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pbTop250
        '
        Me.pbTop250.Image = CType(resources.GetObject("pbTop250.Image"), System.Drawing.Image)
        Me.pbTop250.Location = New System.Drawing.Point(1, 1)
        Me.pbTop250.Name = "pbTop250"
        Me.pbTop250.Size = New System.Drawing.Size(54, 30)
        Me.pbTop250.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbTop250.TabIndex = 14
        Me.pbTop250.TabStop = False
        '
        'lblPlotHeader
        '
        Me.lblPlotHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPlotHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblPlotHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPlotHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlotHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblPlotHeader.Location = New System.Drawing.Point(3, 282)
        Me.lblPlotHeader.Name = "lblPlotHeader"
        Me.lblPlotHeader.Size = New System.Drawing.Size(654, 17)
        Me.lblPlotHeader.TabIndex = 6
        Me.lblPlotHeader.Text = "Plot"
        Me.lblPlotHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPlot
        '
        Me.txtPlot.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPlot.BackColor = System.Drawing.Color.Gainsboro
        Me.txtPlot.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPlot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlot.ForeColor = System.Drawing.Color.Black
        Me.txtPlot.Location = New System.Drawing.Point(3, 303)
        Me.txtPlot.Multiline = True
        Me.txtPlot.Name = "txtPlot"
        Me.txtPlot.ReadOnly = True
        Me.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlot.Size = New System.Drawing.Size(654, 184)
        Me.txtPlot.TabIndex = 7
        Me.txtPlot.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDown.BackColor = System.Drawing.SystemColors.Control
        Me.btnDown.Location = New System.Drawing.Point(936, 1)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(30, 22)
        Me.btnDown.TabIndex = 6
        Me.btnDown.TabStop = False
        Me.btnDown.Text = "v"
        Me.btnDown.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDown.UseVisualStyleBackColor = False
        '
        'btnUp
        '
        Me.btnUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUp.BackColor = System.Drawing.SystemColors.Control
        Me.btnUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUp.Location = New System.Drawing.Point(873, 1)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(30, 22)
        Me.btnUp.TabIndex = 1
        Me.btnUp.TabStop = False
        Me.btnUp.Text = "^"
        Me.btnUp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnUp.UseVisualStyleBackColor = False
        '
        'lblInfoPanelHeader
        '
        Me.lblInfoPanelHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInfoPanelHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblInfoPanelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInfoPanelHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfoPanelHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblInfoPanelHeader.Location = New System.Drawing.Point(3, 3)
        Me.lblInfoPanelHeader.Name = "lblInfoPanelHeader"
        Me.lblInfoPanelHeader.Size = New System.Drawing.Size(968, 17)
        Me.lblInfoPanelHeader.TabIndex = 0
        Me.lblInfoPanelHeader.Text = "Info"
        Me.lblInfoPanelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlPoster
        '
        Me.pnlPoster.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPoster.Controls.Add(Me.pbPoster)
        Me.pnlPoster.Location = New System.Drawing.Point(9, 112)
        Me.pnlPoster.Name = "pnlPoster"
        Me.pnlPoster.Size = New System.Drawing.Size(131, 169)
        Me.pnlPoster.TabIndex = 2
        Me.pnlPoster.Visible = False
        '
        'pbPoster
        '
        Me.pbPoster.BackColor = System.Drawing.SystemColors.Control
        Me.pbPoster.Location = New System.Drawing.Point(4, 4)
        Me.pbPoster.Name = "pbPoster"
        Me.pbPoster.Size = New System.Drawing.Size(121, 159)
        Me.pbPoster.TabIndex = 0
        Me.pbPoster.TabStop = False
        '
        'pbPosterCache
        '
        Me.pbPosterCache.Location = New System.Drawing.Point(454, 107)
        Me.pbPosterCache.Name = "pbPosterCache"
        Me.pbPosterCache.Size = New System.Drawing.Size(115, 111)
        Me.pbPosterCache.TabIndex = 12
        Me.pbPosterCache.TabStop = False
        Me.pbPosterCache.Visible = False
        '
        'pbFanartSmallCache
        '
        Me.pbFanartSmallCache.Location = New System.Drawing.Point(697, 107)
        Me.pbFanartSmallCache.Name = "pbFanartSmallCache"
        Me.pbFanartSmallCache.Size = New System.Drawing.Size(115, 111)
        Me.pbFanartSmallCache.TabIndex = 15
        Me.pbFanartSmallCache.TabStop = False
        Me.pbFanartSmallCache.Visible = False
        '
        'pnlFanartSmall
        '
        Me.pnlFanartSmall.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlFanartSmall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFanartSmall.Controls.Add(Me.pbFanartSmall)
        Me.pnlFanartSmall.Location = New System.Drawing.Point(146, 112)
        Me.pnlFanartSmall.Name = "pnlFanartSmall"
        Me.pnlFanartSmall.Size = New System.Drawing.Size(293, 169)
        Me.pnlFanartSmall.TabIndex = 14
        Me.pnlFanartSmall.Visible = False
        '
        'pbFanartSmall
        '
        Me.pbFanartSmall.BackColor = System.Drawing.SystemColors.Control
        Me.pbFanartSmall.Location = New System.Drawing.Point(4, 4)
        Me.pbFanartSmall.Name = "pbFanartSmall"
        Me.pbFanartSmall.Size = New System.Drawing.Size(283, 159)
        Me.pbFanartSmall.TabIndex = 0
        Me.pbFanartSmall.TabStop = False
        '
        'pnlMPAA
        '
        Me.pnlMPAA.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlMPAA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMPAA.Controls.Add(Me.pbMPAA)
        Me.pnlMPAA.Location = New System.Drawing.Point(4, 609)
        Me.pnlMPAA.Name = "pnlMPAA"
        Me.pnlMPAA.Size = New System.Drawing.Size(202, 45)
        Me.pnlMPAA.TabIndex = 11
        Me.pnlMPAA.Visible = False
        '
        'pbMPAA
        '
        Me.pbMPAA.Location = New System.Drawing.Point(1, 1)
        Me.pbMPAA.Name = "pbMPAA"
        Me.pbMPAA.Size = New System.Drawing.Size(249, 57)
        Me.pbMPAA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbMPAA.TabIndex = 13
        Me.pbMPAA.TabStop = False
        '
        'pbFanartCache
        '
        Me.pbFanartCache.Location = New System.Drawing.Point(576, 107)
        Me.pbFanartCache.Name = "pbFanartCache"
        Me.pbFanartCache.Size = New System.Drawing.Size(115, 111)
        Me.pbFanartCache.TabIndex = 3
        Me.pbFanartCache.TabStop = False
        Me.pbFanartCache.Visible = False
        '
        'pbFanart
        '
        Me.pbFanart.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pbFanart.BackColor = System.Drawing.Color.DimGray
        Me.pbFanart.Location = New System.Drawing.Point(140, 99)
        Me.pbFanart.Name = "pbFanart"
        Me.pbFanart.Size = New System.Drawing.Size(696, 250)
        Me.pbFanart.TabIndex = 1
        Me.pbFanart.TabStop = False
        '
        'tsMain
        '
        Me.tsMain.BackColor = System.Drawing.SystemColors.Control
        Me.tsMain.CanOverflow = False
        Me.tsMain.GripMargin = New System.Windows.Forms.Padding(0)
        Me.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbAutoPilot, Me.mnuUpdate, Me.tsbMediaCenters})
        Me.tsMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.tsMain.Location = New System.Drawing.Point(0, 0)
        Me.tsMain.Name = "tsMain"
        Me.tsMain.Padding = New System.Windows.Forms.Padding(0)
        Me.tsMain.Size = New System.Drawing.Size(976, 25)
        Me.tsMain.Stretch = True
        Me.tsMain.TabIndex = 6
        '
        'tsbAutoPilot
        '
        Me.tsbAutoPilot.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAll, Me.mnuMiss, Me.mnuNew, Me.mnuMark, Me.mnuFilter, Me.mnuCustom})
        Me.tsbAutoPilot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tsbAutoPilot.Image = CType(resources.GetObject("tsbAutoPilot.Image"), System.Drawing.Image)
        Me.tsbAutoPilot.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbAutoPilot.Name = "tsbAutoPilot"
        Me.tsbAutoPilot.Size = New System.Drawing.Size(105, 22)
        Me.tsbAutoPilot.Text = "Scrape Media"
        '
        'mnuAll
        '
        Me.mnuAll.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAllAuto, Me.mnuAllAsk, Me.mnuAllSkip})
        Me.mnuAll.Name = "mnuAll"
        Me.mnuAll.Size = New System.Drawing.Size(183, 22)
        Me.mnuAll.Text = "All Movies"
        '
        'mnuAllAuto
        '
        Me.mnuAllAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAllAutoAll, Me.mnuAllAutoActor, Me.mnuAllAutoBanner, Me.mnuAllAutoClearArt, Me.mnuAllAutoClearLogo, Me.mnuAllAutoDiscArt, Me.mnuAllAutoEFanarts, Me.mnuAllAutoEThumbs, Me.mnuAllAutoFanart, Me.mnuAllAutoLandscape, Me.mnuAllAutoMI, Me.mnuAllAutoNfo, Me.mnuAllAutoPoster, Me.mnuAllAutoTheme, Me.mnuAllAutoTrailer})
        Me.mnuAllAuto.Name = "mnuAllAuto"
        Me.mnuAllAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuAllAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuAllAutoAll
        '
        Me.mnuAllAutoAll.Name = "mnuAllAutoAll"
        Me.mnuAllAutoAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoAll.Text = "All Items"
        '
        'mnuAllAutoActor
        '
        Me.mnuAllAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuAllAutoActor.Name = "mnuAllAutoActor"
        Me.mnuAllAutoActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoActor.Text = "Actor Thumbs Only"
        '
        'mnuAllAutoBanner
        '
        Me.mnuAllAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuAllAutoBanner.Name = "mnuAllAutoBanner"
        Me.mnuAllAutoBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoBanner.Text = "Banner Only"
        '
        'mnuAllAutoClearArt
        '
        Me.mnuAllAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuAllAutoClearArt.Name = "mnuAllAutoClearArt"
        Me.mnuAllAutoClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoClearArt.Text = "ClearArt Only"
        '
        'mnuAllAutoClearLogo
        '
        Me.mnuAllAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuAllAutoClearLogo.Name = "mnuAllAutoClearLogo"
        Me.mnuAllAutoClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuAllAutoDiscArt
        '
        Me.mnuAllAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuAllAutoDiscArt.Name = "mnuAllAutoDiscArt"
        Me.mnuAllAutoDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuAllAutoEFanarts
        '
        Me.mnuAllAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuAllAutoEFanarts.Name = "mnuAllAutoEFanarts"
        Me.mnuAllAutoEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoEFanarts.Text = "Extrafanarts Only"
        '
        'mnuAllAutoEThumbs
        '
        Me.mnuAllAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuAllAutoEThumbs.Name = "mnuAllAutoEThumbs"
        Me.mnuAllAutoEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoEThumbs.Text = "Extrathumbs Only"
        '
        'mnuAllAutoFanart
        '
        Me.mnuAllAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuAllAutoFanart.Name = "mnuAllAutoFanart"
        Me.mnuAllAutoFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoFanart.Text = "Fanart Only"
        '
        'mnuAllAutoLandscape
        '
        Me.mnuAllAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuAllAutoLandscape.Name = "mnuAllAutoLandscape"
        Me.mnuAllAutoLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoLandscape.Text = "Landscape Only"
        '
        'mnuAllAutoMI
        '
        Me.mnuAllAutoMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuAllAutoMI.Name = "mnuAllAutoMI"
        Me.mnuAllAutoMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoMI.Text = "Meta Data Only"
        '
        'mnuAllAutoNfo
        '
        Me.mnuAllAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuAllAutoNfo.Name = "mnuAllAutoNfo"
        Me.mnuAllAutoNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoNfo.Text = "NFO Only"
        '
        'mnuAllAutoPoster
        '
        Me.mnuAllAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuAllAutoPoster.Name = "mnuAllAutoPoster"
        Me.mnuAllAutoPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoPoster.Text = "Poster Only"
        '
        'mnuAllAutoTheme
        '
        Me.mnuAllAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuAllAutoTheme.Name = "mnuAllAutoTheme"
        Me.mnuAllAutoTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoTheme.Text = "Theme Only"
        '
        'mnuAllAutoTrailer
        '
        Me.mnuAllAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuAllAutoTrailer.Name = "mnuAllAutoTrailer"
        Me.mnuAllAutoTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAutoTrailer.Text = "Trailer Only"
        '
        'mnuAllAsk
        '
        Me.mnuAllAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAllAskAll, Me.mnuAllAskActor, Me.mnuAllAskBanner, Me.mnuAllAskClearArt, Me.mnuAllAskClearLogo, Me.mnuAllAskDiscArt, Me.mnuAllAskEFanarts, Me.mnuAllAskEThumbs, Me.mnuAllAskFanart, Me.mnuAllAskLandscape, Me.mnuAllAskMI, Me.mnuAllAskNfo, Me.mnuAllAskPoster, Me.mnuAllAskTheme, Me.mnuAllAskTrailer})
        Me.mnuAllAsk.Name = "mnuAllAsk"
        Me.mnuAllAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuAllAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuAllAskAll
        '
        Me.mnuAllAskAll.Name = "mnuAllAskAll"
        Me.mnuAllAskAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskAll.Text = "All Items"
        '
        'mnuAllAskActor
        '
        Me.mnuAllAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuAllAskActor.Name = "mnuAllAskActor"
        Me.mnuAllAskActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskActor.Text = "Actor Thumbs Only"
        '
        'mnuAllAskBanner
        '
        Me.mnuAllAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuAllAskBanner.Name = "mnuAllAskBanner"
        Me.mnuAllAskBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskBanner.Text = "Banner Only"
        '
        'mnuAllAskClearArt
        '
        Me.mnuAllAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuAllAskClearArt.Name = "mnuAllAskClearArt"
        Me.mnuAllAskClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskClearArt.Text = "ClearArt Only"
        '
        'mnuAllAskClearLogo
        '
        Me.mnuAllAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuAllAskClearLogo.Name = "mnuAllAskClearLogo"
        Me.mnuAllAskClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuAllAskDiscArt
        '
        Me.mnuAllAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuAllAskDiscArt.Name = "mnuAllAskDiscArt"
        Me.mnuAllAskDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskDiscArt.Text = "DiscArt Only"
        '
        'mnuAllAskEFanarts
        '
        Me.mnuAllAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuAllAskEFanarts.Name = "mnuAllAskEFanarts"
        Me.mnuAllAskEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskEFanarts.Text = "Extrafanarts Only"
        '
        'mnuAllAskEThumbs
        '
        Me.mnuAllAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuAllAskEThumbs.Name = "mnuAllAskEThumbs"
        Me.mnuAllAskEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskEThumbs.Text = "Extrathumbs Only"
        '
        'mnuAllAskFanart
        '
        Me.mnuAllAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuAllAskFanart.Name = "mnuAllAskFanart"
        Me.mnuAllAskFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskFanart.Text = "Fanart Only"
        '
        'mnuAllAskLandscape
        '
        Me.mnuAllAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuAllAskLandscape.Name = "mnuAllAskLandscape"
        Me.mnuAllAskLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskLandscape.Text = "Landscape Only"
        '
        'mnuAllAskMI
        '
        Me.mnuAllAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuAllAskMI.Name = "mnuAllAskMI"
        Me.mnuAllAskMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskMI.Text = "Meta Data Only"
        '
        'mnuAllAskNfo
        '
        Me.mnuAllAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuAllAskNfo.Name = "mnuAllAskNfo"
        Me.mnuAllAskNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskNfo.Text = "NFO Only"
        '
        'mnuAllAskPoster
        '
        Me.mnuAllAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuAllAskPoster.Name = "mnuAllAskPoster"
        Me.mnuAllAskPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskPoster.Text = "Poster Only"
        '
        'mnuAllAskTheme
        '
        Me.mnuAllAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuAllAskTheme.Name = "mnuAllAskTheme"
        Me.mnuAllAskTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskTheme.Text = "Theme Only"
        '
        'mnuAllAskTrailer
        '
        Me.mnuAllAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuAllAskTrailer.Name = "mnuAllAskTrailer"
        Me.mnuAllAskTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuAllAskTrailer.Text = "Trailer Only"
        '
        'mnuAllSkip
        '
        Me.mnuAllSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAllSkipAll})
        Me.mnuAllSkip.Name = "mnuAllSkip"
        Me.mnuAllSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuAllSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'mnuAllSkipAll
        '
        Me.mnuAllSkipAll.Name = "mnuAllSkipAll"
        Me.mnuAllSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuAllSkipAll.Text = "All Items"
        '
        'mnuMiss
        '
        Me.mnuMiss.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMissAuto, Me.mnuMissAsk, Me.mnuMissSkip})
        Me.mnuMiss.Name = "mnuMiss"
        Me.mnuMiss.Size = New System.Drawing.Size(183, 22)
        Me.mnuMiss.Text = "Movies Missing Items"
        '
        'mnuMissAuto
        '
        Me.mnuMissAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMissAutoAll, Me.mnuMissAutoActor, Me.mnuMissAutoBanner, Me.mnuMissAutoClearArt, Me.mnuMissAutoClearLogo, Me.mnuMissAutoDiscArt, Me.mnuMissAutoEFanarts, Me.mnuMissAutoEThumbs, Me.mnuMissAutoFanart, Me.mnuMissAutoLandscape, Me.mnuMissAutoNfo, Me.mnuMissAutoPoster, Me.mnuMissAutoTrailer, Me.mnuMissAutoTheme})
        Me.mnuMissAuto.Name = "mnuMissAuto"
        Me.mnuMissAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuMissAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuMissAutoAll
        '
        Me.mnuMissAutoAll.Name = "mnuMissAutoAll"
        Me.mnuMissAutoAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAutoAll.Text = "All Items"
        '
        'mnuMissAutoActor
        '
        Me.mnuMissAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuMissAutoActor.Name = "mnuMissAutoActor"
        Me.mnuMissAutoActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAutoActor.Text = "Actor Thumbs Only"
        '
        'mnuMissAutoBanner
        '
        Me.mnuMissAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMissAutoBanner.Name = "mnuMissAutoBanner"
        Me.mnuMissAutoBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAutoBanner.Text = "Banner Only"
        '
        'mnuMissAutoClearArt
        '
        Me.mnuMissAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMissAutoClearArt.Name = "mnuMissAutoClearArt"
        Me.mnuMissAutoClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAutoClearArt.Text = "ClearArt Only"
        '
        'mnuMissAutoClearLogo
        '
        Me.mnuMissAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMissAutoClearLogo.Name = "mnuMissAutoClearLogo"
        Me.mnuMissAutoClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuMissAutoDiscArt
        '
        Me.mnuMissAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMissAutoDiscArt.Name = "mnuMissAutoDiscArt"
        Me.mnuMissAutoDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuMissAutoEFanarts
        '
        Me.mnuMissAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMissAutoEFanarts.Name = "mnuMissAutoEFanarts"
        Me.mnuMissAutoEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAutoEFanarts.Text = "Extrafanarts Only"
        '
        'mnuMissAutoEThumbs
        '
        Me.mnuMissAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMissAutoEThumbs.Name = "mnuMissAutoEThumbs"
        Me.mnuMissAutoEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAutoEThumbs.Text = "Extrathumbs Only"
        '
        'mnuMissAutoFanart
        '
        Me.mnuMissAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMissAutoFanart.Name = "mnuMissAutoFanart"
        Me.mnuMissAutoFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAutoFanart.Text = "Fanart Only"
        '
        'mnuMissAutoLandscape
        '
        Me.mnuMissAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMissAutoLandscape.Name = "mnuMissAutoLandscape"
        Me.mnuMissAutoLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAutoLandscape.Text = "Landscape Only"
        '
        'mnuMissAutoNfo
        '
        Me.mnuMissAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMissAutoNfo.Name = "mnuMissAutoNfo"
        Me.mnuMissAutoNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAutoNfo.Text = "NFO Only"
        '
        'mnuMissAutoPoster
        '
        Me.mnuMissAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMissAutoPoster.Name = "mnuMissAutoPoster"
        Me.mnuMissAutoPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAutoPoster.Text = "Poster Only"
        '
        'mnuMissAutoTrailer
        '
        Me.mnuMissAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuMissAutoTrailer.Name = "mnuMissAutoTrailer"
        Me.mnuMissAutoTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAutoTrailer.Text = "Trailer Only"
        '
        'mnuMissAutoTheme
        '
        Me.mnuMissAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuMissAutoTheme.Name = "mnuMissAutoTheme"
        Me.mnuMissAutoTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAutoTheme.Text = "Theme Only"
        '
        'mnuMissAsk
        '
        Me.mnuMissAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMissAskAll, Me.mnuMissAskActor, Me.mnuMissAskBanner, Me.mnuMissAskClearArt, Me.mnuMissAskClearLogo, Me.mnuMissAskDiscArt, Me.mnuMissAskEFanarts, Me.mnuMissAskEThumbs, Me.mnuMissAskFanart, Me.mnuMissAskLandscape, Me.mnuMissAskNfo, Me.mnuMissAskPoster, Me.mnuMissAskTheme, Me.mnuMissAskTrailer})
        Me.mnuMissAsk.Name = "mnuMissAsk"
        Me.mnuMissAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuMissAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuMissAskAll
        '
        Me.mnuMissAskAll.Name = "mnuMissAskAll"
        Me.mnuMissAskAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAskAll.Text = "All Items"
        '
        'mnuMissAskActor
        '
        Me.mnuMissAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuMissAskActor.Name = "mnuMissAskActor"
        Me.mnuMissAskActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAskActor.Text = "Actor Thumbs Only"
        '
        'mnuMissAskBanner
        '
        Me.mnuMissAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMissAskBanner.Name = "mnuMissAskBanner"
        Me.mnuMissAskBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAskBanner.Text = "Banner Only"
        '
        'mnuMissAskClearArt
        '
        Me.mnuMissAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMissAskClearArt.Name = "mnuMissAskClearArt"
        Me.mnuMissAskClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAskClearArt.Text = "ClearArt Only"
        '
        'mnuMissAskClearLogo
        '
        Me.mnuMissAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMissAskClearLogo.Name = "mnuMissAskClearLogo"
        Me.mnuMissAskClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuMissAskDiscArt
        '
        Me.mnuMissAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMissAskDiscArt.Name = "mnuMissAskDiscArt"
        Me.mnuMissAskDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAskDiscArt.Text = "DiscArt Only"
        '
        'mnuMissAskEFanarts
        '
        Me.mnuMissAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMissAskEFanarts.Name = "mnuMissAskEFanarts"
        Me.mnuMissAskEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAskEFanarts.Text = "Extrafanarts Only"
        '
        'mnuMissAskEThumbs
        '
        Me.mnuMissAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMissAskEThumbs.Name = "mnuMissAskEThumbs"
        Me.mnuMissAskEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAskEThumbs.Text = "Extrathumbs Only"
        '
        'mnuMissAskFanart
        '
        Me.mnuMissAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMissAskFanart.Name = "mnuMissAskFanart"
        Me.mnuMissAskFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAskFanart.Text = "Fanart Only"
        '
        'mnuMissAskLandscape
        '
        Me.mnuMissAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMissAskLandscape.Name = "mnuMissAskLandscape"
        Me.mnuMissAskLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAskLandscape.Text = "Landscape Only"
        '
        'mnuMissAskNfo
        '
        Me.mnuMissAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMissAskNfo.Name = "mnuMissAskNfo"
        Me.mnuMissAskNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAskNfo.Text = "NFO Only"
        '
        'mnuMissAskPoster
        '
        Me.mnuMissAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMissAskPoster.Name = "mnuMissAskPoster"
        Me.mnuMissAskPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAskPoster.Text = "Poster Only"
        '
        'mnuMissAskTheme
        '
        Me.mnuMissAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuMissAskTheme.Name = "mnuMissAskTheme"
        Me.mnuMissAskTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAskTheme.Text = "Theme Only"
        '
        'mnuMissAskTrailer
        '
        Me.mnuMissAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuMissAskTrailer.Name = "mnuMissAskTrailer"
        Me.mnuMissAskTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuMissAskTrailer.Text = "Trailer Only"
        '
        'mnuMissSkip
        '
        Me.mnuMissSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMissSkipAll})
        Me.mnuMissSkip.Name = "mnuMissSkip"
        Me.mnuMissSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuMissSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'mnuMissSkipAll
        '
        Me.mnuMissSkipAll.Name = "mnuMissSkipAll"
        Me.mnuMissSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuMissSkipAll.Text = "All Items"
        '
        'mnuNew
        '
        Me.mnuNew.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNewAuto, Me.mnuNewAsk, Me.mnuNewSkip})
        Me.mnuNew.Name = "mnuNew"
        Me.mnuNew.Size = New System.Drawing.Size(183, 22)
        Me.mnuNew.Text = "New Movies"
        '
        'mnuNewAuto
        '
        Me.mnuNewAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNewAutoAll, Me.mnuNewAutoActor, Me.mnuNewAutoBanner, Me.mnuNewAutoClearArt, Me.mnuNewAutoClearLogo, Me.mnuNewAutoDiscArt, Me.mnuNewAutoEFanarts, Me.mnuNewAutoEThumbs, Me.mnuNewAutoFanart, Me.mnuNewAutoLandscape, Me.mnuNewAutoMI, Me.mnuNewAutoNfo, Me.mnuNewAutoPoster, Me.mnuNewAutoTheme, Me.mnuNewAutoTrailer})
        Me.mnuNewAuto.Name = "mnuNewAuto"
        Me.mnuNewAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuNewAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuNewAutoAll
        '
        Me.mnuNewAutoAll.Name = "mnuNewAutoAll"
        Me.mnuNewAutoAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoAll.Text = "All Items"
        '
        'mnuNewAutoActor
        '
        Me.mnuNewAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuNewAutoActor.Name = "mnuNewAutoActor"
        Me.mnuNewAutoActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoActor.Text = "Actor Thumbs Only"
        '
        'mnuNewAutoBanner
        '
        Me.mnuNewAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuNewAutoBanner.Name = "mnuNewAutoBanner"
        Me.mnuNewAutoBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoBanner.Text = "Banner Only"
        '
        'mnuNewAutoClearArt
        '
        Me.mnuNewAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuNewAutoClearArt.Name = "mnuNewAutoClearArt"
        Me.mnuNewAutoClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoClearArt.Text = "ClearArt Only"
        '
        'mnuNewAutoClearLogo
        '
        Me.mnuNewAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuNewAutoClearLogo.Name = "mnuNewAutoClearLogo"
        Me.mnuNewAutoClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuNewAutoDiscArt
        '
        Me.mnuNewAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuNewAutoDiscArt.Name = "mnuNewAutoDiscArt"
        Me.mnuNewAutoDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuNewAutoEFanarts
        '
        Me.mnuNewAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuNewAutoEFanarts.Name = "mnuNewAutoEFanarts"
        Me.mnuNewAutoEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoEFanarts.Text = "Extrafanarts Only"
        '
        'mnuNewAutoEThumbs
        '
        Me.mnuNewAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuNewAutoEThumbs.Name = "mnuNewAutoEThumbs"
        Me.mnuNewAutoEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoEThumbs.Text = "Extrathumbs Only"
        '
        'mnuNewAutoFanart
        '
        Me.mnuNewAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuNewAutoFanart.Name = "mnuNewAutoFanart"
        Me.mnuNewAutoFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoFanart.Text = "Fanart Only"
        '
        'mnuNewAutoLandscape
        '
        Me.mnuNewAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuNewAutoLandscape.Name = "mnuNewAutoLandscape"
        Me.mnuNewAutoLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoLandscape.Text = "Landscape Only"
        '
        'mnuNewAutoMI
        '
        Me.mnuNewAutoMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuNewAutoMI.Name = "mnuNewAutoMI"
        Me.mnuNewAutoMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoMI.Text = "Meta Data Only"
        '
        'mnuNewAutoNfo
        '
        Me.mnuNewAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuNewAutoNfo.Name = "mnuNewAutoNfo"
        Me.mnuNewAutoNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoNfo.Text = "NFO Only"
        '
        'mnuNewAutoPoster
        '
        Me.mnuNewAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuNewAutoPoster.Name = "mnuNewAutoPoster"
        Me.mnuNewAutoPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoPoster.Text = "Poster Only"
        '
        'mnuNewAutoTheme
        '
        Me.mnuNewAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuNewAutoTheme.Name = "mnuNewAutoTheme"
        Me.mnuNewAutoTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoTheme.Text = "Theme Only"
        '
        'mnuNewAutoTrailer
        '
        Me.mnuNewAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuNewAutoTrailer.Name = "mnuNewAutoTrailer"
        Me.mnuNewAutoTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAutoTrailer.Text = "Trailer Only"
        '
        'mnuNewAsk
        '
        Me.mnuNewAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNewAskAll, Me.mnuNewAskActor, Me.mnuNewAskBanner, Me.mnuNewAskClearArt, Me.mnuNewAskClearLogo, Me.mnuNewAskDiscArt, Me.mnuNewAskEFanarts, Me.mnuNewAskEThumbs, Me.mnuNewAskFanart, Me.mnuNewAskLandscape, Me.mnuNewAskMI, Me.mnuNewAskNfo, Me.mnuNewAskPoster, Me.mnuNewAskTheme, Me.mnuNewAskTrailer})
        Me.mnuNewAsk.Name = "mnuNewAsk"
        Me.mnuNewAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuNewAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuNewAskAll
        '
        Me.mnuNewAskAll.Name = "mnuNewAskAll"
        Me.mnuNewAskAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskAll.Text = "All Items"
        '
        'mnuNewAskActor
        '
        Me.mnuNewAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuNewAskActor.Name = "mnuNewAskActor"
        Me.mnuNewAskActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskActor.Text = "Actor Thumbs Only"
        '
        'mnuNewAskBanner
        '
        Me.mnuNewAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuNewAskBanner.Name = "mnuNewAskBanner"
        Me.mnuNewAskBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskBanner.Text = "Banner Only"
        '
        'mnuNewAskClearArt
        '
        Me.mnuNewAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuNewAskClearArt.Name = "mnuNewAskClearArt"
        Me.mnuNewAskClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskClearArt.Text = "ClearArt Only"
        '
        'mnuNewAskClearLogo
        '
        Me.mnuNewAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuNewAskClearLogo.Name = "mnuNewAskClearLogo"
        Me.mnuNewAskClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuNewAskDiscArt
        '
        Me.mnuNewAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuNewAskDiscArt.Name = "mnuNewAskDiscArt"
        Me.mnuNewAskDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskDiscArt.Text = "DiscArt Only"
        '
        'mnuNewAskEFanarts
        '
        Me.mnuNewAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuNewAskEFanarts.Name = "mnuNewAskEFanarts"
        Me.mnuNewAskEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskEFanarts.Text = "Extrafanarts Only"
        '
        'mnuNewAskEThumbs
        '
        Me.mnuNewAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuNewAskEThumbs.Name = "mnuNewAskEThumbs"
        Me.mnuNewAskEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskEThumbs.Text = "Extrathumbs Only"
        '
        'mnuNewAskFanart
        '
        Me.mnuNewAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuNewAskFanart.Name = "mnuNewAskFanart"
        Me.mnuNewAskFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskFanart.Text = "Fanart Only"
        '
        'mnuNewAskLandscape
        '
        Me.mnuNewAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuNewAskLandscape.Name = "mnuNewAskLandscape"
        Me.mnuNewAskLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskLandscape.Text = "Landscape Only"
        '
        'mnuNewAskMI
        '
        Me.mnuNewAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuNewAskMI.Name = "mnuNewAskMI"
        Me.mnuNewAskMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskMI.Text = "Meta Data Only"
        '
        'mnuNewAskNfo
        '
        Me.mnuNewAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuNewAskNfo.Name = "mnuNewAskNfo"
        Me.mnuNewAskNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskNfo.Text = "NFO Only"
        '
        'mnuNewAskPoster
        '
        Me.mnuNewAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuNewAskPoster.Name = "mnuNewAskPoster"
        Me.mnuNewAskPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskPoster.Text = "Poster Only"
        '
        'mnuNewAskTheme
        '
        Me.mnuNewAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuNewAskTheme.Name = "mnuNewAskTheme"
        Me.mnuNewAskTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskTheme.Text = "Theme Only"
        '
        'mnuNewAskTrailer
        '
        Me.mnuNewAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuNewAskTrailer.Name = "mnuNewAskTrailer"
        Me.mnuNewAskTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuNewAskTrailer.Text = "Trailer Only"
        '
        'mnuNewSkip
        '
        Me.mnuNewSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNewSkipAll})
        Me.mnuNewSkip.Name = "mnuNewSkip"
        Me.mnuNewSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuNewSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'mnuNewSkipAll
        '
        Me.mnuNewSkipAll.Name = "mnuNewSkipAll"
        Me.mnuNewSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuNewSkipAll.Text = "All Items"
        '
        'mnuMark
        '
        Me.mnuMark.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMarkAuto, Me.mnuMarkAsk, Me.mnuMarkSkip})
        Me.mnuMark.Name = "mnuMark"
        Me.mnuMark.Size = New System.Drawing.Size(183, 22)
        Me.mnuMark.Text = "Marked Movies"
        '
        'mnuMarkAuto
        '
        Me.mnuMarkAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMarkAutoAll, Me.mnuMarkAutoActor, Me.mnuMarkAutoBanner, Me.mnuMarkAutoClearArt, Me.mnuMarkAutoClearLogo, Me.mnuMarkAutoDiscArt, Me.mnuMarkAutoEFanarts, Me.mnuMarkAutoEThumbs, Me.mnuMarkAutoFanart, Me.mnuMarkAutoLandscape, Me.mnuMarkAutoMI, Me.mnuMarkAutoNfo, Me.mnuMarkAutoPoster, Me.mnuMarkAutoTheme, Me.mnuMarkAutoTrailer})
        Me.mnuMarkAuto.Name = "mnuMarkAuto"
        Me.mnuMarkAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuMarkAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuMarkAutoAll
        '
        Me.mnuMarkAutoAll.Name = "mnuMarkAutoAll"
        Me.mnuMarkAutoAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoAll.Text = "All Items"
        '
        'mnuMarkAutoActor
        '
        Me.mnuMarkAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuMarkAutoActor.Name = "mnuMarkAutoActor"
        Me.mnuMarkAutoActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoActor.Text = "Actor Thumbs Only"
        '
        'mnuMarkAutoBanner
        '
        Me.mnuMarkAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMarkAutoBanner.Name = "mnuMarkAutoBanner"
        Me.mnuMarkAutoBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoBanner.Text = "Banner Only"
        '
        'mnuMarkAutoClearArt
        '
        Me.mnuMarkAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMarkAutoClearArt.Name = "mnuMarkAutoClearArt"
        Me.mnuMarkAutoClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoClearArt.Text = "ClearArt Only"
        '
        'mnuMarkAutoClearLogo
        '
        Me.mnuMarkAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMarkAutoClearLogo.Name = "mnuMarkAutoClearLogo"
        Me.mnuMarkAutoClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuMarkAutoDiscArt
        '
        Me.mnuMarkAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMarkAutoDiscArt.Name = "mnuMarkAutoDiscArt"
        Me.mnuMarkAutoDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuMarkAutoEFanarts
        '
        Me.mnuMarkAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMarkAutoEFanarts.Name = "mnuMarkAutoEFanarts"
        Me.mnuMarkAutoEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoEFanarts.Text = "Extrafanarts Only"
        '
        'mnuMarkAutoEThumbs
        '
        Me.mnuMarkAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMarkAutoEThumbs.Name = "mnuMarkAutoEThumbs"
        Me.mnuMarkAutoEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoEThumbs.Text = "Extrathumbs Only"
        '
        'mnuMarkAutoFanart
        '
        Me.mnuMarkAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMarkAutoFanart.Name = "mnuMarkAutoFanart"
        Me.mnuMarkAutoFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoFanart.Text = "Fanart Only"
        '
        'mnuMarkAutoLandscape
        '
        Me.mnuMarkAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMarkAutoLandscape.Name = "mnuMarkAutoLandscape"
        Me.mnuMarkAutoLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoLandscape.Text = "Landscape Only"
        '
        'mnuMarkAutoMI
        '
        Me.mnuMarkAutoMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuMarkAutoMI.Name = "mnuMarkAutoMI"
        Me.mnuMarkAutoMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoMI.Text = "Meta Data Only"
        '
        'mnuMarkAutoNfo
        '
        Me.mnuMarkAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMarkAutoNfo.Name = "mnuMarkAutoNfo"
        Me.mnuMarkAutoNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoNfo.Text = "NFO Only"
        '
        'mnuMarkAutoPoster
        '
        Me.mnuMarkAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMarkAutoPoster.Name = "mnuMarkAutoPoster"
        Me.mnuMarkAutoPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoPoster.Text = "Poster Only"
        '
        'mnuMarkAutoTheme
        '
        Me.mnuMarkAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuMarkAutoTheme.Name = "mnuMarkAutoTheme"
        Me.mnuMarkAutoTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoTheme.Text = "Theme Only"
        '
        'mnuMarkAutoTrailer
        '
        Me.mnuMarkAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuMarkAutoTrailer.Name = "mnuMarkAutoTrailer"
        Me.mnuMarkAutoTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAutoTrailer.Text = "Trailer Only"
        '
        'mnuMarkAsk
        '
        Me.mnuMarkAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMarkAskAll, Me.mnuMarkAskActor, Me.mnuMarkAskBanner, Me.mnuMarkAskClearArt, Me.mnuMarkAskClearLogo, Me.mnuMarkAskDiscArt, Me.mnuMarkAskEFanarts, Me.mnuMarkAskEThumbs, Me.mnuMarkAskFanart, Me.mnuMarkAskLandscape, Me.mnuMarkAskMI, Me.mnuMarkAskNfo, Me.mnuMarkAskPoster, Me.mnuMarkAskTheme, Me.mnuMarkAskTrailer})
        Me.mnuMarkAsk.Name = "mnuMarkAsk"
        Me.mnuMarkAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuMarkAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuMarkAskAll
        '
        Me.mnuMarkAskAll.Name = "mnuMarkAskAll"
        Me.mnuMarkAskAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskAll.Text = "All Items"
        '
        'mnuMarkAskActor
        '
        Me.mnuMarkAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuMarkAskActor.Name = "mnuMarkAskActor"
        Me.mnuMarkAskActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskActor.Text = "Actor Thumbs Only"
        '
        'mnuMarkAskBanner
        '
        Me.mnuMarkAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMarkAskBanner.Name = "mnuMarkAskBanner"
        Me.mnuMarkAskBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskBanner.Text = "Banner Only"
        '
        'mnuMarkAskClearArt
        '
        Me.mnuMarkAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMarkAskClearArt.Name = "mnuMarkAskClearArt"
        Me.mnuMarkAskClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskClearArt.Text = "ClearArt Only"
        '
        'mnuMarkAskClearLogo
        '
        Me.mnuMarkAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMarkAskClearLogo.Name = "mnuMarkAskClearLogo"
        Me.mnuMarkAskClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuMarkAskDiscArt
        '
        Me.mnuMarkAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMarkAskDiscArt.Name = "mnuMarkAskDiscArt"
        Me.mnuMarkAskDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskDiscArt.Text = "DiscArt Only"
        '
        'mnuMarkAskEFanarts
        '
        Me.mnuMarkAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMarkAskEFanarts.Name = "mnuMarkAskEFanarts"
        Me.mnuMarkAskEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskEFanarts.Text = "Extrafanarts Only"
        '
        'mnuMarkAskEThumbs
        '
        Me.mnuMarkAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMarkAskEThumbs.Name = "mnuMarkAskEThumbs"
        Me.mnuMarkAskEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskEThumbs.Text = "Extrathumbs Only"
        '
        'mnuMarkAskFanart
        '
        Me.mnuMarkAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMarkAskFanart.Name = "mnuMarkAskFanart"
        Me.mnuMarkAskFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskFanart.Text = "Fanart Only"
        '
        'mnuMarkAskLandscape
        '
        Me.mnuMarkAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMarkAskLandscape.Name = "mnuMarkAskLandscape"
        Me.mnuMarkAskLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskLandscape.Text = "Landscape Only"
        '
        'mnuMarkAskMI
        '
        Me.mnuMarkAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuMarkAskMI.Name = "mnuMarkAskMI"
        Me.mnuMarkAskMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskMI.Text = "Meta Data Only"
        '
        'mnuMarkAskNfo
        '
        Me.mnuMarkAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMarkAskNfo.Name = "mnuMarkAskNfo"
        Me.mnuMarkAskNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskNfo.Text = "NFO Only"
        '
        'mnuMarkAskPoster
        '
        Me.mnuMarkAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMarkAskPoster.Name = "mnuMarkAskPoster"
        Me.mnuMarkAskPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskPoster.Text = "Poster Only"
        '
        'mnuMarkAskTheme
        '
        Me.mnuMarkAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuMarkAskTheme.Name = "mnuMarkAskTheme"
        Me.mnuMarkAskTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskTheme.Text = "Theme Only"
        '
        'mnuMarkAskTrailer
        '
        Me.mnuMarkAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuMarkAskTrailer.Name = "mnuMarkAskTrailer"
        Me.mnuMarkAskTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuMarkAskTrailer.Text = "Trailer Only"
        '
        'mnuMarkSkip
        '
        Me.mnuMarkSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMarkSkipAll})
        Me.mnuMarkSkip.Name = "mnuMarkSkip"
        Me.mnuMarkSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuMarkSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'mnuMarkSkipAll
        '
        Me.mnuMarkSkipAll.Name = "mnuMarkSkipAll"
        Me.mnuMarkSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuMarkSkipAll.Text = "All Items"
        '
        'mnuFilter
        '
        Me.mnuFilter.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFilterAuto, Me.mnuFilterAsk, Me.mnuFilterSkip})
        Me.mnuFilter.Name = "mnuFilter"
        Me.mnuFilter.Size = New System.Drawing.Size(183, 22)
        Me.mnuFilter.Text = "Current Filter"
        '
        'mnuFilterAuto
        '
        Me.mnuFilterAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFilterAutoAll, Me.mnuFilterAutoActor, Me.mnuFilterAutoBanner, Me.mnuFilterAutoClearArt, Me.mnuFilterAutoClearLogo, Me.mnuFilterAutoDiscArt, Me.mnuFilterAutoEFanarts, Me.mnuFilterAutoEThumbs, Me.mnuFilterAutoFanart, Me.mnuFilterAutoLandscape, Me.mnuFilterAutoMI, Me.mnuFilterAutoNfo, Me.mnuFilterAutoPoster, Me.mnuFilterAutoTheme, Me.mnuFilterAutoTrailer})
        Me.mnuFilterAuto.Name = "mnuFilterAuto"
        Me.mnuFilterAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuFilterAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuFilterAutoAll
        '
        Me.mnuFilterAutoAll.Name = "mnuFilterAutoAll"
        Me.mnuFilterAutoAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoAll.Text = "All Items"
        '
        'mnuFilterAutoActor
        '
        Me.mnuFilterAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuFilterAutoActor.Name = "mnuFilterAutoActor"
        Me.mnuFilterAutoActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoActor.Text = "Actor Thumbs Only"
        '
        'mnuFilterAutoBanner
        '
        Me.mnuFilterAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuFilterAutoBanner.Name = "mnuFilterAutoBanner"
        Me.mnuFilterAutoBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoBanner.Text = "Banner Only"
        '
        'mnuFilterAutoClearArt
        '
        Me.mnuFilterAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuFilterAutoClearArt.Name = "mnuFilterAutoClearArt"
        Me.mnuFilterAutoClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoClearArt.Text = "ClearArt Only"
        '
        'mnuFilterAutoClearLogo
        '
        Me.mnuFilterAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuFilterAutoClearLogo.Name = "mnuFilterAutoClearLogo"
        Me.mnuFilterAutoClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuFilterAutoDiscArt
        '
        Me.mnuFilterAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuFilterAutoDiscArt.Name = "mnuFilterAutoDiscArt"
        Me.mnuFilterAutoDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuFilterAutoEFanarts
        '
        Me.mnuFilterAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuFilterAutoEFanarts.Name = "mnuFilterAutoEFanarts"
        Me.mnuFilterAutoEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoEFanarts.Text = "Extrafanarts Only"
        '
        'mnuFilterAutoEThumbs
        '
        Me.mnuFilterAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuFilterAutoEThumbs.Name = "mnuFilterAutoEThumbs"
        Me.mnuFilterAutoEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoEThumbs.Text = "Extrathumbs Only"
        '
        'mnuFilterAutoFanart
        '
        Me.mnuFilterAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuFilterAutoFanart.Name = "mnuFilterAutoFanart"
        Me.mnuFilterAutoFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoFanart.Text = "Fanart Only"
        '
        'mnuFilterAutoLandscape
        '
        Me.mnuFilterAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuFilterAutoLandscape.Name = "mnuFilterAutoLandscape"
        Me.mnuFilterAutoLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoLandscape.Text = "Landscape Only"
        '
        'mnuFilterAutoMI
        '
        Me.mnuFilterAutoMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuFilterAutoMI.Name = "mnuFilterAutoMI"
        Me.mnuFilterAutoMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoMI.Text = "Meta Data Only"
        '
        'mnuFilterAutoNfo
        '
        Me.mnuFilterAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuFilterAutoNfo.Name = "mnuFilterAutoNfo"
        Me.mnuFilterAutoNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoNfo.Text = "NFO Only"
        '
        'mnuFilterAutoPoster
        '
        Me.mnuFilterAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuFilterAutoPoster.Name = "mnuFilterAutoPoster"
        Me.mnuFilterAutoPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoPoster.Text = "Poster Only"
        '
        'mnuFilterAutoTheme
        '
        Me.mnuFilterAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuFilterAutoTheme.Name = "mnuFilterAutoTheme"
        Me.mnuFilterAutoTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoTheme.Text = "Theme Only"
        '
        'mnuFilterAutoTrailer
        '
        Me.mnuFilterAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuFilterAutoTrailer.Name = "mnuFilterAutoTrailer"
        Me.mnuFilterAutoTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAutoTrailer.Text = "Trailer Only"
        '
        'mnuFilterAsk
        '
        Me.mnuFilterAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFilterAskAll, Me.mnuFilterAskActor, Me.mnuFilterAskBanner, Me.mnuFilterAskClearArt, Me.mnuFilterAskClearLogo, Me.mnuFilterAskDiscArt, Me.mnuFilterAskEFanarts, Me.mnuFilterAskEThumbs, Me.mnuFilterAskFanart, Me.mnuFilterAskLandscape, Me.mnuFilterAskMI, Me.mnuFilterAskNfo, Me.mnuFilterAskPoster, Me.mnuFilterAskTheme, Me.mnuFilterAskTrailer})
        Me.mnuFilterAsk.Name = "mnuFilterAsk"
        Me.mnuFilterAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuFilterAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuFilterAskAll
        '
        Me.mnuFilterAskAll.Name = "mnuFilterAskAll"
        Me.mnuFilterAskAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskAll.Text = "All Items"
        '
        'mnuFilterAskActor
        '
        Me.mnuFilterAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuFilterAskActor.Name = "mnuFilterAskActor"
        Me.mnuFilterAskActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskActor.Text = "Actor Thumbs Only"
        '
        'mnuFilterAskBanner
        '
        Me.mnuFilterAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuFilterAskBanner.Name = "mnuFilterAskBanner"
        Me.mnuFilterAskBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskBanner.Text = "Banner Only"
        '
        'mnuFilterAskClearArt
        '
        Me.mnuFilterAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuFilterAskClearArt.Name = "mnuFilterAskClearArt"
        Me.mnuFilterAskClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskClearArt.Text = "ClearArt Only"
        '
        'mnuFilterAskClearLogo
        '
        Me.mnuFilterAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuFilterAskClearLogo.Name = "mnuFilterAskClearLogo"
        Me.mnuFilterAskClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuFilterAskDiscArt
        '
        Me.mnuFilterAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuFilterAskDiscArt.Name = "mnuFilterAskDiscArt"
        Me.mnuFilterAskDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskDiscArt.Text = "DiscArt Only"
        '
        'mnuFilterAskEFanarts
        '
        Me.mnuFilterAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuFilterAskEFanarts.Name = "mnuFilterAskEFanarts"
        Me.mnuFilterAskEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskEFanarts.Text = "Extrafanarts Only"
        '
        'mnuFilterAskEThumbs
        '
        Me.mnuFilterAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuFilterAskEThumbs.Name = "mnuFilterAskEThumbs"
        Me.mnuFilterAskEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskEThumbs.Text = "Extrathumbs Only"
        '
        'mnuFilterAskFanart
        '
        Me.mnuFilterAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuFilterAskFanart.Name = "mnuFilterAskFanart"
        Me.mnuFilterAskFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskFanart.Text = "Fanart Only"
        '
        'mnuFilterAskLandscape
        '
        Me.mnuFilterAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuFilterAskLandscape.Name = "mnuFilterAskLandscape"
        Me.mnuFilterAskLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskLandscape.Text = "Landscape Only"
        '
        'mnuFilterAskMI
        '
        Me.mnuFilterAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuFilterAskMI.Name = "mnuFilterAskMI"
        Me.mnuFilterAskMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskMI.Text = "Meta Data Only"
        '
        'mnuFilterAskNfo
        '
        Me.mnuFilterAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuFilterAskNfo.Name = "mnuFilterAskNfo"
        Me.mnuFilterAskNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskNfo.Text = "NFO Only"
        '
        'mnuFilterAskPoster
        '
        Me.mnuFilterAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuFilterAskPoster.Name = "mnuFilterAskPoster"
        Me.mnuFilterAskPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskPoster.Text = "Poster Only"
        '
        'mnuFilterAskTheme
        '
        Me.mnuFilterAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuFilterAskTheme.Name = "mnuFilterAskTheme"
        Me.mnuFilterAskTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskTheme.Text = "Theme Only"
        '
        'mnuFilterAskTrailer
        '
        Me.mnuFilterAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuFilterAskTrailer.Name = "mnuFilterAskTrailer"
        Me.mnuFilterAskTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuFilterAskTrailer.Text = "Trailer Only"
        '
        'mnuFilterSkip
        '
        Me.mnuFilterSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFilterSkipAll})
        Me.mnuFilterSkip.Name = "mnuFilterSkip"
        Me.mnuFilterSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuFilterSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'mnuFilterSkipAll
        '
        Me.mnuFilterSkipAll.Name = "mnuFilterSkipAll"
        Me.mnuFilterSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuFilterSkipAll.Text = "All Items"
        '
        'mnuCustom
        '
        Me.mnuCustom.Name = "mnuCustom"
        Me.mnuCustom.Size = New System.Drawing.Size(183, 22)
        Me.mnuCustom.Text = "Custom Scraper..."
        '
        'mnuUpdate
        '
        Me.mnuUpdate.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuUpdateMovies, Me.mnuUpdateShows})
        Me.mnuUpdate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.mnuUpdate.Image = CType(resources.GetObject("mnuUpdate.Image"), System.Drawing.Image)
        Me.mnuUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuUpdate.Name = "mnuUpdate"
        Me.mnuUpdate.Size = New System.Drawing.Size(114, 22)
        Me.mnuUpdate.Text = "Update Library"
        '
        'mnuUpdateMovies
        '
        Me.mnuUpdateMovies.Name = "mnuUpdateMovies"
        Me.mnuUpdateMovies.Size = New System.Drawing.Size(123, 22)
        Me.mnuUpdateMovies.Text = "Movies"
        '
        'mnuUpdateShows
        '
        Me.mnuUpdateShows.Name = "mnuUpdateShows"
        Me.mnuUpdateShows.Size = New System.Drawing.Size(123, 22)
        Me.mnuUpdateShows.Text = "TV Shows"
        '
        'tsbMediaCenters
        '
        Me.tsbMediaCenters.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsbMediaCenters.Enabled = False
        Me.tsbMediaCenters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tsbMediaCenters.Image = CType(resources.GetObject("tsbMediaCenters.Image"), System.Drawing.Image)
        Me.tsbMediaCenters.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbMediaCenters.Name = "tsbMediaCenters"
        Me.tsbMediaCenters.Size = New System.Drawing.Size(113, 22)
        Me.tsbMediaCenters.Text = "Media Centers"
        Me.tsbMediaCenters.Visible = False
        '
        'ilColumnIcons
        '
        Me.ilColumnIcons.ImageStream = CType(resources.GetObject("ilColumnIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilColumnIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ilColumnIcons.Images.SetKeyName(0, "hasPoster.png")
        Me.ilColumnIcons.Images.SetKeyName(1, "hasFanart.png")
        Me.ilColumnIcons.Images.SetKeyName(2, "hasNfo.png")
        Me.ilColumnIcons.Images.SetKeyName(3, "hasTrailer.png")
        Me.ilColumnIcons.Images.SetKeyName(4, "hasSubtitle.png")
        Me.ilColumnIcons.Images.SetKeyName(5, "hasExtrathumb.png")
        Me.ilColumnIcons.Images.SetKeyName(6, "listcheck.png")
        Me.ilColumnIcons.Images.SetKeyName(7, "listdotgrey.png")
        Me.ilColumnIcons.Images.SetKeyName(8, "hasWatched.png")
        Me.ilColumnIcons.Images.SetKeyName(9, "hasExtrafanart.png")
        Me.ilColumnIcons.Images.SetKeyName(10, "hasBanner.png")
        Me.ilColumnIcons.Images.SetKeyName(11, "hasLandscape.png")
        Me.ilColumnIcons.Images.SetKeyName(12, "hasTheme.png")
        Me.ilColumnIcons.Images.SetKeyName(13, "hasDiscArt.png")
        Me.ilColumnIcons.Images.SetKeyName(14, "hasClearLogo.png")
        Me.ilColumnIcons.Images.SetKeyName(15, "hasClearArt.png")
        '
        'tmrWait
        '
        Me.tmrWait.Interval = 250
        '
        'tmrLoad
        '
        '
        'tmrSearchWait
        '
        Me.tmrSearchWait.Interval = 250
        '
        'tmrSearch
        '
        Me.tmrSearch.Interval = 250
        '
        'tmrFilterAni
        '
        Me.tmrFilterAni.Interval = 1
        '
        'ToolTips
        '
        Me.ToolTips.AutoPopDelay = 15000
        Me.ToolTips.InitialDelay = 500
        Me.ToolTips.ReshowDelay = 100
        '
        'tmrWaitShow
        '
        Me.tmrWaitShow.Interval = 250
        '
        'tmrLoadShow
        '
        '
        'tmrWaitSeason
        '
        Me.tmrWaitSeason.Interval = 250
        '
        'tmrLoadSeason
        '
        '
        'tmrWaitEp
        '
        Me.tmrWaitEp.Interval = 250
        '
        'tmrLoadEp
        '
        '
        'cmnuTray
        '
        Me.cmnuTray.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayTitle, Me.ToolStripSeparator21, Me.cmnuTrayUpdate, Me.cmnuTrayScrape, Me.cmnuTrayMediaCenters, Me.ToolStripSeparator23, Me.cmnuTrayTools, Me.ToolStripSeparator22, Me.cmnuTraySettings, Me.ToolStripSeparator13, Me.cmnuTrayExit})
        Me.cmnuTray.Name = "cmnuTrayIcon"
        Me.cmnuTray.Size = New System.Drawing.Size(195, 182)
        Me.cmnuTray.Text = "Ember Media Manager"
        '
        'cmnuTrayTitle
        '
        Me.cmnuTrayTitle.Enabled = False
        Me.cmnuTrayTitle.Image = CType(resources.GetObject("cmnuTrayTitle.Image"), System.Drawing.Image)
        Me.cmnuTrayTitle.Name = "cmnuTrayTitle"
        Me.cmnuTrayTitle.Size = New System.Drawing.Size(194, 22)
        Me.cmnuTrayTitle.Text = "Ember Media Manager"
        '
        'ToolStripSeparator21
        '
        Me.ToolStripSeparator21.Name = "ToolStripSeparator21"
        Me.ToolStripSeparator21.Size = New System.Drawing.Size(191, 6)
        '
        'cmnuTrayUpdate
        '
        Me.cmnuTrayUpdate.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayUpdateMovies, Me.cmnuTrayUpdateShows})
        Me.cmnuTrayUpdate.Image = CType(resources.GetObject("cmnuTrayUpdate.Image"), System.Drawing.Image)
        Me.cmnuTrayUpdate.Name = "cmnuTrayUpdate"
        Me.cmnuTrayUpdate.Size = New System.Drawing.Size(194, 22)
        Me.cmnuTrayUpdate.Text = "Update Media"
        '
        'cmnuTrayUpdateMovies
        '
        Me.cmnuTrayUpdateMovies.Name = "cmnuTrayUpdateMovies"
        Me.cmnuTrayUpdateMovies.Size = New System.Drawing.Size(125, 22)
        Me.cmnuTrayUpdateMovies.Text = "Movies"
        '
        'cmnuTrayUpdateShows
        '
        Me.cmnuTrayUpdateShows.Name = "cmnuTrayUpdateShows"
        Me.cmnuTrayUpdateShows.Size = New System.Drawing.Size(125, 22)
        Me.cmnuTrayUpdateShows.Text = "TV Shows"
        '
        'cmnuTrayScrape
        '
        Me.cmnuTrayScrape.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayAll, Me.cmnuTrayMiss, Me.cmnuTrayNew, Me.cmnuTrayMark, Me.cmnuTrayFilter, Me.cmnuTrayCustom})
        Me.cmnuTrayScrape.Image = CType(resources.GetObject("cmnuTrayScrape.Image"), System.Drawing.Image)
        Me.cmnuTrayScrape.Name = "cmnuTrayScrape"
        Me.cmnuTrayScrape.Size = New System.Drawing.Size(194, 22)
        Me.cmnuTrayScrape.Text = "Scrape Media"
        '
        'cmnuTrayAll
        '
        Me.cmnuTrayAll.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayAllAuto, Me.cmnuTrayAllAsk, Me.cmnuTrayAllSkip})
        Me.cmnuTrayAll.Name = "cmnuTrayAll"
        Me.cmnuTrayAll.Size = New System.Drawing.Size(188, 22)
        Me.cmnuTrayAll.Text = "All Movies"
        '
        'cmnuTrayAllAuto
        '
        Me.cmnuTrayAllAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayAllAutoAll, Me.cmnuTrayAllAutoActor, Me.cmnuTrayAllAutoBanner, Me.cmnuTrayAllAutoClearArt, Me.cmnuTrayAllAutoClearLogo, Me.cmnuTrayAllAutoDiscArt, Me.cmnuTrayAllAutoEFanarts, Me.cmnuTrayAllAutoEThumbs, Me.cmnuTrayAllAutoFanart, Me.cmnuTrayAllAutoLandscape, Me.cmnuTrayAllAutoMetaData, Me.cmnuTrayAllAutoNfo, Me.cmnuTrayAllAutoPoster, Me.cmnuTrayAllAutoTheme, Me.cmnuTrayAllAutoTrailer})
        Me.cmnuTrayAllAuto.Name = "cmnuTrayAllAuto"
        Me.cmnuTrayAllAuto.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayAllAuto.Text = "Automatic (Force Best Match)"
        '
        'cmnuTrayAllAutoAll
        '
        Me.cmnuTrayAllAutoAll.Name = "cmnuTrayAllAutoAll"
        Me.cmnuTrayAllAutoAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoAll.Text = "All Items"
        '
        'cmnuTrayAllAutoActor
        '
        Me.cmnuTrayAllAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayAllAutoActor.Name = "cmnuTrayAllAutoActor"
        Me.cmnuTrayAllAutoActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayAllAutoBanner
        '
        Me.cmnuTrayAllAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayAllAutoBanner.Name = "cmnuTrayAllAutoBanner"
        Me.cmnuTrayAllAutoBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoBanner.Text = "Banner Only"
        '
        'cmnuTrayAllAutoClearArt
        '
        Me.cmnuTrayAllAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayAllAutoClearArt.Name = "cmnuTrayAllAutoClearArt"
        Me.cmnuTrayAllAutoClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayAllAutoClearLogo
        '
        Me.cmnuTrayAllAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayAllAutoClearLogo.Name = "cmnuTrayAllAutoClearLogo"
        Me.cmnuTrayAllAutoClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayAllAutoDiscArt
        '
        Me.cmnuTrayAllAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayAllAutoDiscArt.Name = "cmnuTrayAllAutoDiscArt"
        Me.cmnuTrayAllAutoDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayAllAutoEFanarts
        '
        Me.cmnuTrayAllAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayAllAutoEFanarts.Name = "cmnuTrayAllAutoEFanarts"
        Me.cmnuTrayAllAutoEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayAllAutoEThumbs
        '
        Me.cmnuTrayAllAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayAllAutoEThumbs.Name = "cmnuTrayAllAutoEThumbs"
        Me.cmnuTrayAllAutoEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayAllAutoFanart
        '
        Me.cmnuTrayAllAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayAllAutoFanart.Name = "cmnuTrayAllAutoFanart"
        Me.cmnuTrayAllAutoFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoFanart.Text = "Fanart Only"
        '
        'cmnuTrayAllAutoLandscape
        '
        Me.cmnuTrayAllAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayAllAutoLandscape.Name = "cmnuTrayAllAutoLandscape"
        Me.cmnuTrayAllAutoLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoLandscape.Text = "Landscape Only"
        '
        'cmnuTrayAllAutoMetaData
        '
        Me.cmnuTrayAllAutoMetaData.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayAllAutoMetaData.Name = "cmnuTrayAllAutoMetaData"
        Me.cmnuTrayAllAutoMetaData.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoMetaData.Text = "Meta Data Only"
        '
        'cmnuTrayAllAutoNfo
        '
        Me.cmnuTrayAllAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayAllAutoNfo.Name = "cmnuTrayAllAutoNfo"
        Me.cmnuTrayAllAutoNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoNfo.Text = "NFO Only"
        '
        'cmnuTrayAllAutoPoster
        '
        Me.cmnuTrayAllAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayAllAutoPoster.Name = "cmnuTrayAllAutoPoster"
        Me.cmnuTrayAllAutoPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoPoster.Text = "Poster Only"
        '
        'cmnuTrayAllAutoTheme
        '
        Me.cmnuTrayAllAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayAllAutoTheme.Name = "cmnuTrayAllAutoTheme"
        Me.cmnuTrayAllAutoTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoTheme.Text = "Theme Only"
        '
        'cmnuTrayAllAutoTrailer
        '
        Me.cmnuTrayAllAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayAllAutoTrailer.Name = "cmnuTrayAllAutoTrailer"
        Me.cmnuTrayAllAutoTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAutoTrailer.Text = "Trailer Only"
        '
        'cmnuTrayAllAsk
        '
        Me.cmnuTrayAllAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayAllAskAll, Me.cmnuTrayAllAskActor, Me.cmnuTrayAllAskBanner, Me.cmnuTrayAllAskClearArt, Me.cmnuTrayAllAskClearLogo, Me.cmnuTrayAllAskDiscArt, Me.cmnuTrayAllAskEFanarts, Me.cmnuTrayAllAskEThumbs, Me.cmnuTrayAllAskFanart, Me.cmnuTrayAllAskLandscape, Me.cmnuTrayAllAskMI, Me.cmnuTrayAllAskNfo, Me.cmnuTrayAllAskPoster, Me.cmnuTrayAllAskTheme, Me.cmnuTrayAllAskTrailer})
        Me.cmnuTrayAllAsk.Name = "cmnuTrayAllAsk"
        Me.cmnuTrayAllAsk.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayAllAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'cmnuTrayAllAskAll
        '
        Me.cmnuTrayAllAskAll.Name = "cmnuTrayAllAskAll"
        Me.cmnuTrayAllAskAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskAll.Text = "All Items"
        '
        'cmnuTrayAllAskActor
        '
        Me.cmnuTrayAllAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayAllAskActor.Name = "cmnuTrayAllAskActor"
        Me.cmnuTrayAllAskActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayAllAskBanner
        '
        Me.cmnuTrayAllAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayAllAskBanner.Name = "cmnuTrayAllAskBanner"
        Me.cmnuTrayAllAskBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskBanner.Text = "Banner Only"
        '
        'cmnuTrayAllAskClearArt
        '
        Me.cmnuTrayAllAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayAllAskClearArt.Name = "cmnuTrayAllAskClearArt"
        Me.cmnuTrayAllAskClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayAllAskClearLogo
        '
        Me.cmnuTrayAllAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayAllAskClearLogo.Name = "cmnuTrayAllAskClearLogo"
        Me.cmnuTrayAllAskClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayAllAskDiscArt
        '
        Me.cmnuTrayAllAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayAllAskDiscArt.Name = "cmnuTrayAllAskDiscArt"
        Me.cmnuTrayAllAskDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayAllAskEFanarts
        '
        Me.cmnuTrayAllAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayAllAskEFanarts.Name = "cmnuTrayAllAskEFanarts"
        Me.cmnuTrayAllAskEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayAllAskEThumbs
        '
        Me.cmnuTrayAllAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayAllAskEThumbs.Name = "cmnuTrayAllAskEThumbs"
        Me.cmnuTrayAllAskEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayAllAskFanart
        '
        Me.cmnuTrayAllAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayAllAskFanart.Name = "cmnuTrayAllAskFanart"
        Me.cmnuTrayAllAskFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskFanart.Text = "Fanart Only"
        '
        'cmnuTrayAllAskLandscape
        '
        Me.cmnuTrayAllAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayAllAskLandscape.Name = "cmnuTrayAllAskLandscape"
        Me.cmnuTrayAllAskLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskLandscape.Text = "Landscape Only"
        '
        'cmnuTrayAllAskMI
        '
        Me.cmnuTrayAllAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayAllAskMI.Name = "cmnuTrayAllAskMI"
        Me.cmnuTrayAllAskMI.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskMI.Text = "Meta Data Only"
        '
        'cmnuTrayAllAskNfo
        '
        Me.cmnuTrayAllAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayAllAskNfo.Name = "cmnuTrayAllAskNfo"
        Me.cmnuTrayAllAskNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskNfo.Text = "NFO Only"
        '
        'cmnuTrayAllAskPoster
        '
        Me.cmnuTrayAllAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayAllAskPoster.Name = "cmnuTrayAllAskPoster"
        Me.cmnuTrayAllAskPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskPoster.Text = "Poster Only"
        '
        'cmnuTrayAllAskTheme
        '
        Me.cmnuTrayAllAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayAllAskTheme.Name = "cmnuTrayAllAskTheme"
        Me.cmnuTrayAllAskTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskTheme.Text = "Theme Only"
        '
        'cmnuTrayAllAskTrailer
        '
        Me.cmnuTrayAllAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayAllAskTrailer.Name = "cmnuTrayAllAskTrailer"
        Me.cmnuTrayAllAskTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayAllAskTrailer.Text = "Trailer Only"
        '
        'cmnuTrayAllSkip
        '
        Me.cmnuTrayAllSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayAllSkipAll})
        Me.cmnuTrayAllSkip.Name = "cmnuTrayAllSkip"
        Me.cmnuTrayAllSkip.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayAllSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'cmnuTrayAllSkipAll
        '
        Me.cmnuTrayAllSkipAll.Name = "cmnuTrayAllSkipAll"
        Me.cmnuTrayAllSkipAll.Size = New System.Drawing.Size(120, 22)
        Me.cmnuTrayAllSkipAll.Text = "All Items"
        '
        'cmnuTrayMiss
        '
        Me.cmnuTrayMiss.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMissAuto, Me.cmnuTrayMissAsk, Me.cmnuTrayMissSkip})
        Me.cmnuTrayMiss.Name = "cmnuTrayMiss"
        Me.cmnuTrayMiss.Size = New System.Drawing.Size(188, 22)
        Me.cmnuTrayMiss.Text = "Movies Missing Items"
        '
        'cmnuTrayMissAuto
        '
        Me.cmnuTrayMissAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMissAutoAll, Me.cmnuTrayMissAutoActor, Me.cmnuTrayMissAutoBanner, Me.cmnuTrayMissAutoClearArt, Me.cmnuTrayMissAutoClearLogo, Me.cmnuTrayMissAutoDiscArt, Me.cmnuTrayMissAutoEFanarts, Me.cmnuTrayMissAutoEThumbs, Me.cmnuTrayMissAutoFanart, Me.cmnuTrayMissAutoLandscape, Me.cmnuTrayMissAutoNfo, Me.cmnuTrayMissAutoPoster, Me.cmnuTrayMissAutoTheme, Me.cmnuTrayMissAutoTrailer})
        Me.cmnuTrayMissAuto.Name = "cmnuTrayMissAuto"
        Me.cmnuTrayMissAuto.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMissAuto.Text = "Automatic (Force Best Match)"
        '
        'cmnuTrayMissAutoAll
        '
        Me.cmnuTrayMissAutoAll.Name = "cmnuTrayMissAutoAll"
        Me.cmnuTrayMissAutoAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAutoAll.Text = "All Items"
        '
        'cmnuTrayMissAutoActor
        '
        Me.cmnuTrayMissAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayMissAutoActor.Name = "cmnuTrayMissAutoActor"
        Me.cmnuTrayMissAutoActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAutoActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayMissAutoBanner
        '
        Me.cmnuTrayMissAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayMissAutoBanner.Name = "cmnuTrayMissAutoBanner"
        Me.cmnuTrayMissAutoBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAutoBanner.Text = "Banner Only"
        '
        'cmnuTrayMissAutoClearArt
        '
        Me.cmnuTrayMissAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayMissAutoClearArt.Name = "cmnuTrayMissAutoClearArt"
        Me.cmnuTrayMissAutoClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAutoClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayMissAutoClearLogo
        '
        Me.cmnuTrayMissAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayMissAutoClearLogo.Name = "cmnuTrayMissAutoClearLogo"
        Me.cmnuTrayMissAutoClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAutoClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayMissAutoDiscArt
        '
        Me.cmnuTrayMissAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayMissAutoDiscArt.Name = "cmnuTrayMissAutoDiscArt"
        Me.cmnuTrayMissAutoDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAutoDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayMissAutoEFanarts
        '
        Me.cmnuTrayMissAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayMissAutoEFanarts.Name = "cmnuTrayMissAutoEFanarts"
        Me.cmnuTrayMissAutoEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAutoEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayMissAutoEThumbs
        '
        Me.cmnuTrayMissAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayMissAutoEThumbs.Name = "cmnuTrayMissAutoEThumbs"
        Me.cmnuTrayMissAutoEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAutoEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayMissAutoFanart
        '
        Me.cmnuTrayMissAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayMissAutoFanart.Name = "cmnuTrayMissAutoFanart"
        Me.cmnuTrayMissAutoFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAutoFanart.Text = "Fanart Only"
        '
        'cmnuTrayMissAutoLandscape
        '
        Me.cmnuTrayMissAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayMissAutoLandscape.Name = "cmnuTrayMissAutoLandscape"
        Me.cmnuTrayMissAutoLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAutoLandscape.Text = "Landscape Only"
        '
        'cmnuTrayMissAutoNfo
        '
        Me.cmnuTrayMissAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayMissAutoNfo.Name = "cmnuTrayMissAutoNfo"
        Me.cmnuTrayMissAutoNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAutoNfo.Text = "NFO Only"
        '
        'cmnuTrayMissAutoPoster
        '
        Me.cmnuTrayMissAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayMissAutoPoster.Name = "cmnuTrayMissAutoPoster"
        Me.cmnuTrayMissAutoPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAutoPoster.Text = "Poster Only"
        '
        'cmnuTrayMissAutoTheme
        '
        Me.cmnuTrayMissAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayMissAutoTheme.Name = "cmnuTrayMissAutoTheme"
        Me.cmnuTrayMissAutoTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAutoTheme.Text = "Theme Only"
        '
        'cmnuTrayMissAutoTrailer
        '
        Me.cmnuTrayMissAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayMissAutoTrailer.Name = "cmnuTrayMissAutoTrailer"
        Me.cmnuTrayMissAutoTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAutoTrailer.Text = "Trailer Only"
        '
        'cmnuTrayMissAsk
        '
        Me.cmnuTrayMissAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMissAskAll, Me.cmnuTrayMissAskActor, Me.cmnuTrayMissAskBanner, Me.cmnuTrayMissAskClearArt, Me.cmnuTrayMissAskClearLogo, Me.cmnuTrayMissAskDiscArt, Me.cmnuTrayMissAskEFanarts, Me.cmnuTrayMissAskEThumbs, Me.cmnuTrayMissAskFanart, Me.cmnuTrayMissAskLandscape, Me.cmnuTrayMissAskNfo, Me.cmnuTrayMissAskPoster, Me.cmnuTrayMissAskTheme, Me.cmnuTrayMissAskTrailer})
        Me.cmnuTrayMissAsk.Name = "cmnuTrayMissAsk"
        Me.cmnuTrayMissAsk.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMissAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'cmnuTrayMissAskAll
        '
        Me.cmnuTrayMissAskAll.Name = "cmnuTrayMissAskAll"
        Me.cmnuTrayMissAskAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAskAll.Text = "All Items"
        '
        'cmnuTrayMissAskActor
        '
        Me.cmnuTrayMissAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayMissAskActor.Name = "cmnuTrayMissAskActor"
        Me.cmnuTrayMissAskActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAskActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayMissAskBanner
        '
        Me.cmnuTrayMissAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayMissAskBanner.Name = "cmnuTrayMissAskBanner"
        Me.cmnuTrayMissAskBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAskBanner.Text = "Banner Only"
        '
        'cmnuTrayMissAskClearArt
        '
        Me.cmnuTrayMissAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayMissAskClearArt.Name = "cmnuTrayMissAskClearArt"
        Me.cmnuTrayMissAskClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAskClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayMissAskClearLogo
        '
        Me.cmnuTrayMissAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayMissAskClearLogo.Name = "cmnuTrayMissAskClearLogo"
        Me.cmnuTrayMissAskClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAskClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayMissAskDiscArt
        '
        Me.cmnuTrayMissAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayMissAskDiscArt.Name = "cmnuTrayMissAskDiscArt"
        Me.cmnuTrayMissAskDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAskDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayMissAskEFanarts
        '
        Me.cmnuTrayMissAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayMissAskEFanarts.Name = "cmnuTrayMissAskEFanarts"
        Me.cmnuTrayMissAskEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAskEFanarts.Text = "Extrfanarts Only"
        '
        'cmnuTrayMissAskEThumbs
        '
        Me.cmnuTrayMissAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayMissAskEThumbs.Name = "cmnuTrayMissAskEThumbs"
        Me.cmnuTrayMissAskEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAskEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayMissAskFanart
        '
        Me.cmnuTrayMissAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayMissAskFanart.Name = "cmnuTrayMissAskFanart"
        Me.cmnuTrayMissAskFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAskFanart.Text = "Fanart Only"
        '
        'cmnuTrayMissAskLandscape
        '
        Me.cmnuTrayMissAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayMissAskLandscape.Name = "cmnuTrayMissAskLandscape"
        Me.cmnuTrayMissAskLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAskLandscape.Text = "Landscape Only"
        '
        'cmnuTrayMissAskNfo
        '
        Me.cmnuTrayMissAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayMissAskNfo.Name = "cmnuTrayMissAskNfo"
        Me.cmnuTrayMissAskNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAskNfo.Text = "NFO Only"
        '
        'cmnuTrayMissAskPoster
        '
        Me.cmnuTrayMissAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayMissAskPoster.Name = "cmnuTrayMissAskPoster"
        Me.cmnuTrayMissAskPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAskPoster.Text = "Poster Only"
        '
        'cmnuTrayMissAskTheme
        '
        Me.cmnuTrayMissAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayMissAskTheme.Name = "cmnuTrayMissAskTheme"
        Me.cmnuTrayMissAskTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAskTheme.Text = "Theme Only"
        '
        'cmnuTrayMissAskTrailer
        '
        Me.cmnuTrayMissAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayMissAskTrailer.Name = "cmnuTrayMissAskTrailer"
        Me.cmnuTrayMissAskTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMissAskTrailer.Text = "Trailer Only"
        '
        'cmnuTrayMissSkip
        '
        Me.cmnuTrayMissSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMissSkipAll})
        Me.cmnuTrayMissSkip.Name = "cmnuTrayMissSkip"
        Me.cmnuTrayMissSkip.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMissSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'cmnuTrayMissSkipAll
        '
        Me.cmnuTrayMissSkipAll.Name = "cmnuTrayMissSkipAll"
        Me.cmnuTrayMissSkipAll.Size = New System.Drawing.Size(120, 22)
        Me.cmnuTrayMissSkipAll.Text = "All Items"
        '
        'cmnuTrayNew
        '
        Me.cmnuTrayNew.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayNewAuto, Me.cmnuTrayNewAsk, Me.cmnuTrayNewSkip})
        Me.cmnuTrayNew.Name = "cmnuTrayNew"
        Me.cmnuTrayNew.Size = New System.Drawing.Size(188, 22)
        Me.cmnuTrayNew.Text = "New Movies"
        '
        'cmnuTrayNewAuto
        '
        Me.cmnuTrayNewAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayNewAutoAll, Me.cmnuTrayNewAutoActor, Me.cmnuTrayNewAutoBanner, Me.cmnuTrayNewAutoClearArt, Me.cmnuTrayNewAutoClearLogo, Me.cmnuTrayNewAutoDiscArt, Me.cmnuTrayNewAutoEFanarts, Me.cmnuTrayNewAutoEThumbs, Me.cmnuTrayNewAutoFanart, Me.cmnuTrayNewAutoLandscape, Me.cmnuTrayNewAutoMI, Me.cmnuTrayNewAutoNfo, Me.cmnuTrayNewAutoPoster, Me.cmnuTrayNewAutoTheme, Me.cmnuTrayNewAutoTrailer})
        Me.cmnuTrayNewAuto.Name = "cmnuTrayNewAuto"
        Me.cmnuTrayNewAuto.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayNewAuto.Text = "Automatic (Force Best Match)"
        '
        'cmnuTrayNewAutoAll
        '
        Me.cmnuTrayNewAutoAll.Name = "cmnuTrayNewAutoAll"
        Me.cmnuTrayNewAutoAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoAll.Text = "All Items"
        '
        'cmnuTrayNewAutoActor
        '
        Me.cmnuTrayNewAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayNewAutoActor.Name = "cmnuTrayNewAutoActor"
        Me.cmnuTrayNewAutoActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayNewAutoBanner
        '
        Me.cmnuTrayNewAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayNewAutoBanner.Name = "cmnuTrayNewAutoBanner"
        Me.cmnuTrayNewAutoBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoBanner.Text = "Banner Only"
        '
        'cmnuTrayNewAutoClearArt
        '
        Me.cmnuTrayNewAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayNewAutoClearArt.Name = "cmnuTrayNewAutoClearArt"
        Me.cmnuTrayNewAutoClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayNewAutoClearLogo
        '
        Me.cmnuTrayNewAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayNewAutoClearLogo.Name = "cmnuTrayNewAutoClearLogo"
        Me.cmnuTrayNewAutoClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayNewAutoDiscArt
        '
        Me.cmnuTrayNewAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayNewAutoDiscArt.Name = "cmnuTrayNewAutoDiscArt"
        Me.cmnuTrayNewAutoDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayNewAutoEFanarts
        '
        Me.cmnuTrayNewAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayNewAutoEFanarts.Name = "cmnuTrayNewAutoEFanarts"
        Me.cmnuTrayNewAutoEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayNewAutoEThumbs
        '
        Me.cmnuTrayNewAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayNewAutoEThumbs.Name = "cmnuTrayNewAutoEThumbs"
        Me.cmnuTrayNewAutoEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayNewAutoFanart
        '
        Me.cmnuTrayNewAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayNewAutoFanart.Name = "cmnuTrayNewAutoFanart"
        Me.cmnuTrayNewAutoFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoFanart.Text = "Fanart Only"
        '
        'cmnuTrayNewAutoLandscape
        '
        Me.cmnuTrayNewAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayNewAutoLandscape.Name = "cmnuTrayNewAutoLandscape"
        Me.cmnuTrayNewAutoLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoLandscape.Text = "Landscape Only"
        '
        'cmnuTrayNewAutoMI
        '
        Me.cmnuTrayNewAutoMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayNewAutoMI.Name = "cmnuTrayNewAutoMI"
        Me.cmnuTrayNewAutoMI.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoMI.Text = "Meta Data Only"
        '
        'cmnuTrayNewAutoNfo
        '
        Me.cmnuTrayNewAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayNewAutoNfo.Name = "cmnuTrayNewAutoNfo"
        Me.cmnuTrayNewAutoNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoNfo.Text = "NFO Only"
        '
        'cmnuTrayNewAutoPoster
        '
        Me.cmnuTrayNewAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayNewAutoPoster.Name = "cmnuTrayNewAutoPoster"
        Me.cmnuTrayNewAutoPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoPoster.Text = "Poster Only"
        '
        'cmnuTrayNewAutoTheme
        '
        Me.cmnuTrayNewAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayNewAutoTheme.Name = "cmnuTrayNewAutoTheme"
        Me.cmnuTrayNewAutoTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoTheme.Text = "Theme Only"
        '
        'cmnuTrayNewAutoTrailer
        '
        Me.cmnuTrayNewAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayNewAutoTrailer.Name = "cmnuTrayNewAutoTrailer"
        Me.cmnuTrayNewAutoTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAutoTrailer.Text = "Trailer Only"
        '
        'cmnuTrayNewAsk
        '
        Me.cmnuTrayNewAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayNewAskAll, Me.cmnuTrayNewAskActor, Me.cmnuTrayNewAskBanner, Me.cmnuTrayNewAskClearArt, Me.cmnuTrayNewAskClearLogo, Me.cmnuTrayNewAskDiscArt, Me.cmnuTrayNewAskEFanarts, Me.cmnuTrayNewAskEThumbs, Me.cmnuTrayNewAskFanart, Me.cmnuTrayNewAskLandscape, Me.cmnuTrayNewAskMI, Me.cmnuTrayNewAskNfo, Me.cmnuTrayNewAskPoster, Me.cmnuTrayNewAskTheme, Me.cmnuTrayNewAskTrailer})
        Me.cmnuTrayNewAsk.Name = "cmnuTrayNewAsk"
        Me.cmnuTrayNewAsk.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayNewAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'cmnuTrayNewAskAll
        '
        Me.cmnuTrayNewAskAll.Name = "cmnuTrayNewAskAll"
        Me.cmnuTrayNewAskAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskAll.Text = "All Items"
        '
        'cmnuTrayNewAskActor
        '
        Me.cmnuTrayNewAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayNewAskActor.Name = "cmnuTrayNewAskActor"
        Me.cmnuTrayNewAskActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayNewAskBanner
        '
        Me.cmnuTrayNewAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayNewAskBanner.Name = "cmnuTrayNewAskBanner"
        Me.cmnuTrayNewAskBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskBanner.Text = "Banner Only"
        '
        'cmnuTrayNewAskClearArt
        '
        Me.cmnuTrayNewAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayNewAskClearArt.Name = "cmnuTrayNewAskClearArt"
        Me.cmnuTrayNewAskClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayNewAskClearLogo
        '
        Me.cmnuTrayNewAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayNewAskClearLogo.Name = "cmnuTrayNewAskClearLogo"
        Me.cmnuTrayNewAskClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayNewAskDiscArt
        '
        Me.cmnuTrayNewAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayNewAskDiscArt.Name = "cmnuTrayNewAskDiscArt"
        Me.cmnuTrayNewAskDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayNewAskEFanarts
        '
        Me.cmnuTrayNewAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayNewAskEFanarts.Name = "cmnuTrayNewAskEFanarts"
        Me.cmnuTrayNewAskEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayNewAskEThumbs
        '
        Me.cmnuTrayNewAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayNewAskEThumbs.Name = "cmnuTrayNewAskEThumbs"
        Me.cmnuTrayNewAskEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayNewAskFanart
        '
        Me.cmnuTrayNewAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayNewAskFanart.Name = "cmnuTrayNewAskFanart"
        Me.cmnuTrayNewAskFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskFanart.Text = "Fanart Only"
        '
        'cmnuTrayNewAskLandscape
        '
        Me.cmnuTrayNewAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayNewAskLandscape.Name = "cmnuTrayNewAskLandscape"
        Me.cmnuTrayNewAskLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskLandscape.Text = "Landscape Only"
        '
        'cmnuTrayNewAskMI
        '
        Me.cmnuTrayNewAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayNewAskMI.Name = "cmnuTrayNewAskMI"
        Me.cmnuTrayNewAskMI.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskMI.Text = "Meta Data Only"
        '
        'cmnuTrayNewAskNfo
        '
        Me.cmnuTrayNewAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayNewAskNfo.Name = "cmnuTrayNewAskNfo"
        Me.cmnuTrayNewAskNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskNfo.Text = "NFO Only"
        '
        'cmnuTrayNewAskPoster
        '
        Me.cmnuTrayNewAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayNewAskPoster.Name = "cmnuTrayNewAskPoster"
        Me.cmnuTrayNewAskPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskPoster.Text = "Poster Only"
        '
        'cmnuTrayNewAskTheme
        '
        Me.cmnuTrayNewAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayNewAskTheme.Name = "cmnuTrayNewAskTheme"
        Me.cmnuTrayNewAskTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskTheme.Text = "Theme Only"
        '
        'cmnuTrayNewAskTrailer
        '
        Me.cmnuTrayNewAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayNewAskTrailer.Name = "cmnuTrayNewAskTrailer"
        Me.cmnuTrayNewAskTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayNewAskTrailer.Text = "Trailer Only"
        '
        'cmnuTrayNewSkip
        '
        Me.cmnuTrayNewSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayNewSkipAll})
        Me.cmnuTrayNewSkip.Name = "cmnuTrayNewSkip"
        Me.cmnuTrayNewSkip.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayNewSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'cmnuTrayNewSkipAll
        '
        Me.cmnuTrayNewSkipAll.Name = "cmnuTrayNewSkipAll"
        Me.cmnuTrayNewSkipAll.Size = New System.Drawing.Size(120, 22)
        Me.cmnuTrayNewSkipAll.Text = "All Items"
        '
        'cmnuTrayMark
        '
        Me.cmnuTrayMark.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMarkAuto, Me.cmnuTrayMarkAsk, Me.cmnuTrayMarkSkip})
        Me.cmnuTrayMark.Name = "cmnuTrayMark"
        Me.cmnuTrayMark.Size = New System.Drawing.Size(188, 22)
        Me.cmnuTrayMark.Text = "Marked Movies"
        '
        'cmnuTrayMarkAuto
        '
        Me.cmnuTrayMarkAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMarkAutoAll, Me.cmnuTrayMarkAutoActor, Me.cmnuTrayMarkAutoBanner, Me.cmnuTrayMarkAutoClearArt, Me.cmnuTrayMarkAutoClearLogo, Me.cmnuTrayMarkAutoDiscArt, Me.cmnuTrayMarkAutoEFanarts, Me.cmnuTrayMarkAutoEThumbs, Me.cmnuTrayMarkAutoFanart, Me.cmnuTrayMarkAutoLandscape, Me.cmnuTrayMarkAutoMI, Me.cmnuTrayMarkAutoNfo, Me.cmnuTrayMarkAutoPoster, Me.cmnuTrayMarkAutoTheme, Me.cmnuTrayMarkAutoTrailer})
        Me.cmnuTrayMarkAuto.Name = "cmnuTrayMarkAuto"
        Me.cmnuTrayMarkAuto.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMarkAuto.Text = "Automatic (Force Best Match)"
        '
        'cmnuTrayMarkAutoAll
        '
        Me.cmnuTrayMarkAutoAll.Name = "cmnuTrayMarkAutoAll"
        Me.cmnuTrayMarkAutoAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoAll.Text = "All Items"
        '
        'cmnuTrayMarkAutoActor
        '
        Me.cmnuTrayMarkAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayMarkAutoActor.Name = "cmnuTrayMarkAutoActor"
        Me.cmnuTrayMarkAutoActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayMarkAutoBanner
        '
        Me.cmnuTrayMarkAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayMarkAutoBanner.Name = "cmnuTrayMarkAutoBanner"
        Me.cmnuTrayMarkAutoBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoBanner.Text = "Banner Only"
        '
        'cmnuTrayMarkAutoClearArt
        '
        Me.cmnuTrayMarkAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayMarkAutoClearArt.Name = "cmnuTrayMarkAutoClearArt"
        Me.cmnuTrayMarkAutoClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayMarkAutoClearLogo
        '
        Me.cmnuTrayMarkAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayMarkAutoClearLogo.Name = "cmnuTrayMarkAutoClearLogo"
        Me.cmnuTrayMarkAutoClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayMarkAutoDiscArt
        '
        Me.cmnuTrayMarkAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayMarkAutoDiscArt.Name = "cmnuTrayMarkAutoDiscArt"
        Me.cmnuTrayMarkAutoDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayMarkAutoEFanarts
        '
        Me.cmnuTrayMarkAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayMarkAutoEFanarts.Name = "cmnuTrayMarkAutoEFanarts"
        Me.cmnuTrayMarkAutoEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayMarkAutoEThumbs
        '
        Me.cmnuTrayMarkAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayMarkAutoEThumbs.Name = "cmnuTrayMarkAutoEThumbs"
        Me.cmnuTrayMarkAutoEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayMarkAutoFanart
        '
        Me.cmnuTrayMarkAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayMarkAutoFanart.Name = "cmnuTrayMarkAutoFanart"
        Me.cmnuTrayMarkAutoFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoFanart.Text = "Fanart Only"
        '
        'cmnuTrayMarkAutoLandscape
        '
        Me.cmnuTrayMarkAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayMarkAutoLandscape.Name = "cmnuTrayMarkAutoLandscape"
        Me.cmnuTrayMarkAutoLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoLandscape.Text = "Landscape Only"
        '
        'cmnuTrayMarkAutoMI
        '
        Me.cmnuTrayMarkAutoMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayMarkAutoMI.Name = "cmnuTrayMarkAutoMI"
        Me.cmnuTrayMarkAutoMI.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoMI.Text = "Meta Data Only"
        '
        'cmnuTrayMarkAutoNfo
        '
        Me.cmnuTrayMarkAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayMarkAutoNfo.Name = "cmnuTrayMarkAutoNfo"
        Me.cmnuTrayMarkAutoNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoNfo.Text = "NFO Only"
        '
        'cmnuTrayMarkAutoPoster
        '
        Me.cmnuTrayMarkAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayMarkAutoPoster.Name = "cmnuTrayMarkAutoPoster"
        Me.cmnuTrayMarkAutoPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoPoster.Text = "Poster Only"
        '
        'cmnuTrayMarkAutoTheme
        '
        Me.cmnuTrayMarkAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayMarkAutoTheme.Name = "cmnuTrayMarkAutoTheme"
        Me.cmnuTrayMarkAutoTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoTheme.Text = "Theme Only"
        '
        'cmnuTrayMarkAutoTrailer
        '
        Me.cmnuTrayMarkAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayMarkAutoTrailer.Name = "cmnuTrayMarkAutoTrailer"
        Me.cmnuTrayMarkAutoTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAutoTrailer.Text = "Trailer Only"
        '
        'cmnuTrayMarkAsk
        '
        Me.cmnuTrayMarkAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMarkAskAll, Me.cmnuTrayMarkAskActor, Me.cmnuTrayMarkAskBanner, Me.cmnuTrayMarkAskClearArt, Me.cmnuTrayMarkAskClearLogo, Me.cmnuTrayMarkAskDiscArt, Me.cmnuTrayMarkAskEFanarts, Me.cmnuTrayMarkAskEThumbs, Me.cmnuTrayMarkAskFanart, Me.cmnuTrayMarkAskLandscape, Me.cmnuTrayMarkAskMI, Me.cmnuTrayMarkAskNfo, Me.cmnuTrayMarkAskPoster, Me.cmnuTrayMarkAskTheme, Me.cmnuTrayMarkAskTrailer})
        Me.cmnuTrayMarkAsk.Name = "cmnuTrayMarkAsk"
        Me.cmnuTrayMarkAsk.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMarkAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'cmnuTrayMarkAskAll
        '
        Me.cmnuTrayMarkAskAll.Name = "cmnuTrayMarkAskAll"
        Me.cmnuTrayMarkAskAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskAll.Text = "All Items"
        '
        'cmnuTrayMarkAskActor
        '
        Me.cmnuTrayMarkAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayMarkAskActor.Name = "cmnuTrayMarkAskActor"
        Me.cmnuTrayMarkAskActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayMarkAskBanner
        '
        Me.cmnuTrayMarkAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayMarkAskBanner.Name = "cmnuTrayMarkAskBanner"
        Me.cmnuTrayMarkAskBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskBanner.Text = "Banner Only"
        '
        'cmnuTrayMarkAskClearArt
        '
        Me.cmnuTrayMarkAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayMarkAskClearArt.Name = "cmnuTrayMarkAskClearArt"
        Me.cmnuTrayMarkAskClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayMarkAskClearLogo
        '
        Me.cmnuTrayMarkAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayMarkAskClearLogo.Name = "cmnuTrayMarkAskClearLogo"
        Me.cmnuTrayMarkAskClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayMarkAskDiscArt
        '
        Me.cmnuTrayMarkAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayMarkAskDiscArt.Name = "cmnuTrayMarkAskDiscArt"
        Me.cmnuTrayMarkAskDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayMarkAskEFanarts
        '
        Me.cmnuTrayMarkAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayMarkAskEFanarts.Name = "cmnuTrayMarkAskEFanarts"
        Me.cmnuTrayMarkAskEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayMarkAskEThumbs
        '
        Me.cmnuTrayMarkAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayMarkAskEThumbs.Name = "cmnuTrayMarkAskEThumbs"
        Me.cmnuTrayMarkAskEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayMarkAskFanart
        '
        Me.cmnuTrayMarkAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayMarkAskFanart.Name = "cmnuTrayMarkAskFanart"
        Me.cmnuTrayMarkAskFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskFanart.Text = "Fanart Only"
        '
        'cmnuTrayMarkAskLandscape
        '
        Me.cmnuTrayMarkAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayMarkAskLandscape.Name = "cmnuTrayMarkAskLandscape"
        Me.cmnuTrayMarkAskLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskLandscape.Text = "Landscape Only"
        '
        'cmnuTrayMarkAskMI
        '
        Me.cmnuTrayMarkAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayMarkAskMI.Name = "cmnuTrayMarkAskMI"
        Me.cmnuTrayMarkAskMI.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskMI.Text = "Meta Data Only"
        '
        'cmnuTrayMarkAskNfo
        '
        Me.cmnuTrayMarkAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayMarkAskNfo.Name = "cmnuTrayMarkAskNfo"
        Me.cmnuTrayMarkAskNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskNfo.Text = "NFO Only"
        '
        'cmnuTrayMarkAskPoster
        '
        Me.cmnuTrayMarkAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayMarkAskPoster.Name = "cmnuTrayMarkAskPoster"
        Me.cmnuTrayMarkAskPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskPoster.Text = "Poster Only"
        '
        'cmnuTrayMarkAskTheme
        '
        Me.cmnuTrayMarkAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayMarkAskTheme.Name = "cmnuTrayMarkAskTheme"
        Me.cmnuTrayMarkAskTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskTheme.Text = "Theme Only"
        '
        'cmnuTrayMarkAskTrailer
        '
        Me.cmnuTrayMarkAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayMarkAskTrailer.Name = "cmnuTrayMarkAskTrailer"
        Me.cmnuTrayMarkAskTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMarkAskTrailer.Text = "Trailer Only"
        '
        'cmnuTrayMarkSkip
        '
        Me.cmnuTrayMarkSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMarkSkipAll})
        Me.cmnuTrayMarkSkip.Name = "cmnuTrayMarkSkip"
        Me.cmnuTrayMarkSkip.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMarkSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'cmnuTrayMarkSkipAll
        '
        Me.cmnuTrayMarkSkipAll.Name = "cmnuTrayMarkSkipAll"
        Me.cmnuTrayMarkSkipAll.Size = New System.Drawing.Size(120, 22)
        Me.cmnuTrayMarkSkipAll.Text = "All Items"
        '
        'cmnuTrayFilter
        '
        Me.cmnuTrayFilter.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayFilterAuto, Me.cmnuTrayFilterAsk, Me.cmnuTrayFilterSkip})
        Me.cmnuTrayFilter.Name = "cmnuTrayFilter"
        Me.cmnuTrayFilter.Size = New System.Drawing.Size(188, 22)
        Me.cmnuTrayFilter.Text = "Current Filter"
        '
        'cmnuTrayFilterAuto
        '
        Me.cmnuTrayFilterAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayFilterAutoAll, Me.cmnuTrayFilterAutoActor, Me.cmnuTrayFilterAutoBanner, Me.cmnuTrayFilterAutoClearArt, Me.cmnuTrayFilterAutoClearLogo, Me.cmnuTrayFilterAutoDiscArt, Me.cmnuTrayFilterAutoEFanarts, Me.cmnuTrayFilterAutoEThumbs, Me.cmnuTrayFilterAutoFanart, Me.cmnuTrayFilterAutoLandscape, Me.cmnuTrayFilterAutoMI, Me.cmnuTrayFilterAutoNfo, Me.cmnuTrayFilterAutoPoster, Me.cmnuTrayFilterAutoTheme, Me.cmnuTrayFilterAutoTrailer})
        Me.cmnuTrayFilterAuto.Name = "cmnuTrayFilterAuto"
        Me.cmnuTrayFilterAuto.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayFilterAuto.Text = "Automatic (Force Best Match)"
        '
        'cmnuTrayFilterAutoAll
        '
        Me.cmnuTrayFilterAutoAll.Name = "cmnuTrayFilterAutoAll"
        Me.cmnuTrayFilterAutoAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoAll.Text = "All Items"
        '
        'cmnuTrayFilterAutoActor
        '
        Me.cmnuTrayFilterAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayFilterAutoActor.Name = "cmnuTrayFilterAutoActor"
        Me.cmnuTrayFilterAutoActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayFilterAutoBanner
        '
        Me.cmnuTrayFilterAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayFilterAutoBanner.Name = "cmnuTrayFilterAutoBanner"
        Me.cmnuTrayFilterAutoBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoBanner.Text = "Banner Only"
        '
        'cmnuTrayFilterAutoClearArt
        '
        Me.cmnuTrayFilterAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayFilterAutoClearArt.Name = "cmnuTrayFilterAutoClearArt"
        Me.cmnuTrayFilterAutoClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayFilterAutoClearLogo
        '
        Me.cmnuTrayFilterAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayFilterAutoClearLogo.Name = "cmnuTrayFilterAutoClearLogo"
        Me.cmnuTrayFilterAutoClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayFilterAutoDiscArt
        '
        Me.cmnuTrayFilterAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayFilterAutoDiscArt.Name = "cmnuTrayFilterAutoDiscArt"
        Me.cmnuTrayFilterAutoDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayFilterAutoEFanarts
        '
        Me.cmnuTrayFilterAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayFilterAutoEFanarts.Name = "cmnuTrayFilterAutoEFanarts"
        Me.cmnuTrayFilterAutoEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayFilterAutoEThumbs
        '
        Me.cmnuTrayFilterAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayFilterAutoEThumbs.Name = "cmnuTrayFilterAutoEThumbs"
        Me.cmnuTrayFilterAutoEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayFilterAutoFanart
        '
        Me.cmnuTrayFilterAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayFilterAutoFanart.Name = "cmnuTrayFilterAutoFanart"
        Me.cmnuTrayFilterAutoFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoFanart.Text = "Fanart Only"
        '
        'cmnuTrayFilterAutoLandscape
        '
        Me.cmnuTrayFilterAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayFilterAutoLandscape.Name = "cmnuTrayFilterAutoLandscape"
        Me.cmnuTrayFilterAutoLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoLandscape.Text = "Landscape Only"
        '
        'cmnuTrayFilterAutoMI
        '
        Me.cmnuTrayFilterAutoMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayFilterAutoMI.Name = "cmnuTrayFilterAutoMI"
        Me.cmnuTrayFilterAutoMI.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoMI.Text = "Meta Data Only"
        '
        'cmnuTrayFilterAutoNfo
        '
        Me.cmnuTrayFilterAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayFilterAutoNfo.Name = "cmnuTrayFilterAutoNfo"
        Me.cmnuTrayFilterAutoNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoNfo.Text = "NFO Only"
        '
        'cmnuTrayFilterAutoPoster
        '
        Me.cmnuTrayFilterAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayFilterAutoPoster.Name = "cmnuTrayFilterAutoPoster"
        Me.cmnuTrayFilterAutoPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoPoster.Text = "Poster Only"
        '
        'cmnuTrayFilterAutoTheme
        '
        Me.cmnuTrayFilterAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayFilterAutoTheme.Name = "cmnuTrayFilterAutoTheme"
        Me.cmnuTrayFilterAutoTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoTheme.Text = "Theme Only"
        '
        'cmnuTrayFilterAutoTrailer
        '
        Me.cmnuTrayFilterAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayFilterAutoTrailer.Name = "cmnuTrayFilterAutoTrailer"
        Me.cmnuTrayFilterAutoTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAutoTrailer.Text = "Trailer Only"
        '
        'cmnuTrayFilterAsk
        '
        Me.cmnuTrayFilterAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayFilterAskAll, Me.cmnuTrayFilterAskActor, Me.cmnuTrayFilterAskBanner, Me.cmnuTrayFilterAskClearArt, Me.cmnuTrayFilterAskClearLogo, Me.cmnuTrayFilterAskDiscArt, Me.cmnuTrayFilterAskEFanarts, Me.cmnuTrayFilterAskEThumbs, Me.cmnuTrayFilterAskFanart, Me.cmnuTrayFilterAskLandscape, Me.cmnuTrayFilterAskMI, Me.cmnuTrayFilterAskNfo, Me.cmnuTrayFilterAskPoster, Me.cmnuTrayFilterAskTheme, Me.cmnuTrayFilterAskTrailer})
        Me.cmnuTrayFilterAsk.Name = "cmnuTrayFilterAsk"
        Me.cmnuTrayFilterAsk.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayFilterAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'cmnuTrayFilterAskAll
        '
        Me.cmnuTrayFilterAskAll.Name = "cmnuTrayFilterAskAll"
        Me.cmnuTrayFilterAskAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskAll.Text = "All Items"
        '
        'cmnuTrayFilterAskActor
        '
        Me.cmnuTrayFilterAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayFilterAskActor.Name = "cmnuTrayFilterAskActor"
        Me.cmnuTrayFilterAskActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayFilterAskBanner
        '
        Me.cmnuTrayFilterAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayFilterAskBanner.Name = "cmnuTrayFilterAskBanner"
        Me.cmnuTrayFilterAskBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskBanner.Text = "Banner Only"
        '
        'cmnuTrayFilterAskClearArt
        '
        Me.cmnuTrayFilterAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayFilterAskClearArt.Name = "cmnuTrayFilterAskClearArt"
        Me.cmnuTrayFilterAskClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayFilterAskClearLogo
        '
        Me.cmnuTrayFilterAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayFilterAskClearLogo.Name = "cmnuTrayFilterAskClearLogo"
        Me.cmnuTrayFilterAskClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayFilterAskDiscArt
        '
        Me.cmnuTrayFilterAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayFilterAskDiscArt.Name = "cmnuTrayFilterAskDiscArt"
        Me.cmnuTrayFilterAskDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayFilterAskEFanarts
        '
        Me.cmnuTrayFilterAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayFilterAskEFanarts.Name = "cmnuTrayFilterAskEFanarts"
        Me.cmnuTrayFilterAskEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayFilterAskEThumbs
        '
        Me.cmnuTrayFilterAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayFilterAskEThumbs.Name = "cmnuTrayFilterAskEThumbs"
        Me.cmnuTrayFilterAskEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayFilterAskFanart
        '
        Me.cmnuTrayFilterAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayFilterAskFanart.Name = "cmnuTrayFilterAskFanart"
        Me.cmnuTrayFilterAskFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskFanart.Text = "Fanart Only"
        '
        'cmnuTrayFilterAskLandscape
        '
        Me.cmnuTrayFilterAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayFilterAskLandscape.Name = "cmnuTrayFilterAskLandscape"
        Me.cmnuTrayFilterAskLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskLandscape.Text = "Landscape Only"
        '
        'cmnuTrayFilterAskMI
        '
        Me.cmnuTrayFilterAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayFilterAskMI.Name = "cmnuTrayFilterAskMI"
        Me.cmnuTrayFilterAskMI.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskMI.Text = "Meta Data Only"
        '
        'cmnuTrayFilterAskNfo
        '
        Me.cmnuTrayFilterAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayFilterAskNfo.Name = "cmnuTrayFilterAskNfo"
        Me.cmnuTrayFilterAskNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskNfo.Text = "NFO Only"
        '
        'cmnuTrayFilterAskPoster
        '
        Me.cmnuTrayFilterAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayFilterAskPoster.Name = "cmnuTrayFilterAskPoster"
        Me.cmnuTrayFilterAskPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskPoster.Text = "Poster Only"
        '
        'cmnuTrayFilterAskTheme
        '
        Me.cmnuTrayFilterAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayFilterAskTheme.Name = "cmnuTrayFilterAskTheme"
        Me.cmnuTrayFilterAskTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskTheme.Text = "Theme Only"
        '
        'cmnuTrayFilterAskTrailer
        '
        Me.cmnuTrayFilterAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayFilterAskTrailer.Name = "cmnuTrayFilterAskTrailer"
        Me.cmnuTrayFilterAskTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayFilterAskTrailer.Text = "Trailer Only"
        '
        'cmnuTrayFilterSkip
        '
        Me.cmnuTrayFilterSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayFilterSkipAll})
        Me.cmnuTrayFilterSkip.Name = "cmnuTrayFilterSkip"
        Me.cmnuTrayFilterSkip.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayFilterSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'cmnuTrayFilterSkipAll
        '
        Me.cmnuTrayFilterSkipAll.Name = "cmnuTrayFilterSkipAll"
        Me.cmnuTrayFilterSkipAll.Size = New System.Drawing.Size(120, 22)
        Me.cmnuTrayFilterSkipAll.Text = "All Items"
        '
        'cmnuTrayCustom
        '
        Me.cmnuTrayCustom.Name = "cmnuTrayCustom"
        Me.cmnuTrayCustom.Size = New System.Drawing.Size(188, 22)
        Me.cmnuTrayCustom.Text = "Custom Scraper..."
        '
        'cmnuTrayMediaCenters
        '
        Me.cmnuTrayMediaCenters.Image = CType(resources.GetObject("cmnuTrayMediaCenters.Image"), System.Drawing.Image)
        Me.cmnuTrayMediaCenters.Name = "cmnuTrayMediaCenters"
        Me.cmnuTrayMediaCenters.Size = New System.Drawing.Size(194, 22)
        Me.cmnuTrayMediaCenters.Text = "Media Centers"
        Me.cmnuTrayMediaCenters.Visible = False
        '
        'ToolStripSeparator23
        '
        Me.ToolStripSeparator23.Name = "ToolStripSeparator23"
        Me.ToolStripSeparator23.Size = New System.Drawing.Size(191, 6)
        '
        'cmnuTrayTools
        '
        Me.cmnuTrayTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayToolsCleanFiles, Me.cmnuTrayToolsSortFiles, Me.cmnuTrayToolsBackdrops, Me.ToolStripSeparator24, Me.cmnuTrayToolsSetsManager, Me.cmnuTrayToolsOfflineHolder, Me.ToolStripSeparator25, Me.cmnuTrayToolsClearCache, Me.cmnuTrayToolsReloadMovies, Me.cmnuTrayToolsCleanDB, Me.ToolStripSeparator26})
        Me.cmnuTrayTools.Image = CType(resources.GetObject("cmnuTrayTools.Image"), System.Drawing.Image)
        Me.cmnuTrayTools.Name = "cmnuTrayTools"
        Me.cmnuTrayTools.Size = New System.Drawing.Size(194, 22)
        Me.cmnuTrayTools.Text = "Tools"
        '
        'cmnuTrayToolsCleanFiles
        '
        Me.cmnuTrayToolsCleanFiles.Image = CType(resources.GetObject("cmnuTrayToolsCleanFiles.Image"), System.Drawing.Image)
        Me.cmnuTrayToolsCleanFiles.Name = "cmnuTrayToolsCleanFiles"
        Me.cmnuTrayToolsCleanFiles.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsCleanFiles.Text = "Clean Files"
        '
        'cmnuTrayToolsSortFiles
        '
        Me.cmnuTrayToolsSortFiles.Image = CType(resources.GetObject("cmnuTrayToolsSortFiles.Image"), System.Drawing.Image)
        Me.cmnuTrayToolsSortFiles.Name = "cmnuTrayToolsSortFiles"
        Me.cmnuTrayToolsSortFiles.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsSortFiles.Text = "Sort Files into Folders"
        '
        'cmnuTrayToolsBackdrops
        '
        Me.cmnuTrayToolsBackdrops.Image = CType(resources.GetObject("cmnuTrayToolsBackdrops.Image"), System.Drawing.Image)
        Me.cmnuTrayToolsBackdrops.Name = "cmnuTrayToolsBackdrops"
        Me.cmnuTrayToolsBackdrops.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsBackdrops.Text = "Copy Existing Fanart to Backdrops Folder"
        '
        'ToolStripSeparator24
        '
        Me.ToolStripSeparator24.Name = "ToolStripSeparator24"
        Me.ToolStripSeparator24.Size = New System.Drawing.Size(286, 6)
        '
        'cmnuTrayToolsSetsManager
        '
        Me.cmnuTrayToolsSetsManager.Image = CType(resources.GetObject("cmnuTrayToolsSetsManager.Image"), System.Drawing.Image)
        Me.cmnuTrayToolsSetsManager.Name = "cmnuTrayToolsSetsManager"
        Me.cmnuTrayToolsSetsManager.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsSetsManager.Text = "Sets Manager"
        '
        'cmnuTrayToolsOfflineHolder
        '
        Me.cmnuTrayToolsOfflineHolder.Image = CType(resources.GetObject("cmnuTrayToolsOfflineHolder.Image"), System.Drawing.Image)
        Me.cmnuTrayToolsOfflineHolder.Name = "cmnuTrayToolsOfflineHolder"
        Me.cmnuTrayToolsOfflineHolder.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsOfflineHolder.Text = "Offline Media Manager"
        '
        'ToolStripSeparator25
        '
        Me.ToolStripSeparator25.Name = "ToolStripSeparator25"
        Me.ToolStripSeparator25.Size = New System.Drawing.Size(286, 6)
        '
        'cmnuTrayToolsClearCache
        '
        Me.cmnuTrayToolsClearCache.Image = CType(resources.GetObject("cmnuTrayToolsClearCache.Image"), System.Drawing.Image)
        Me.cmnuTrayToolsClearCache.Name = "cmnuTrayToolsClearCache"
        Me.cmnuTrayToolsClearCache.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsClearCache.Text = "Clear All Caches"
        '
        'cmnuTrayToolsReloadMovies
        '
        Me.cmnuTrayToolsReloadMovies.Image = CType(resources.GetObject("cmnuTrayToolsReloadMovies.Image"), System.Drawing.Image)
        Me.cmnuTrayToolsReloadMovies.Name = "cmnuTrayToolsReloadMovies"
        Me.cmnuTrayToolsReloadMovies.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsReloadMovies.Text = "Reload All Movies"
        '
        'cmnuTrayToolsCleanDB
        '
        Me.cmnuTrayToolsCleanDB.Image = CType(resources.GetObject("cmnuTrayToolsCleanDB.Image"), System.Drawing.Image)
        Me.cmnuTrayToolsCleanDB.Name = "cmnuTrayToolsCleanDB"
        Me.cmnuTrayToolsCleanDB.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsCleanDB.Text = "Clean Database"
        '
        'ToolStripSeparator26
        '
        Me.ToolStripSeparator26.Name = "ToolStripSeparator26"
        Me.ToolStripSeparator26.Size = New System.Drawing.Size(286, 6)
        '
        'ToolStripSeparator22
        '
        Me.ToolStripSeparator22.Name = "ToolStripSeparator22"
        Me.ToolStripSeparator22.Size = New System.Drawing.Size(191, 6)
        '
        'cmnuTraySettings
        '
        Me.cmnuTraySettings.Image = CType(resources.GetObject("cmnuTraySettings.Image"), System.Drawing.Image)
        Me.cmnuTraySettings.Name = "cmnuTraySettings"
        Me.cmnuTraySettings.Size = New System.Drawing.Size(194, 22)
        Me.cmnuTraySettings.Text = "Settings..."
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(191, 6)
        '
        'cmnuTrayExit
        '
        Me.cmnuTrayExit.Image = CType(resources.GetObject("cmnuTrayExit.Image"), System.Drawing.Image)
        Me.cmnuTrayExit.Name = "cmnuTrayExit"
        Me.cmnuTrayExit.Size = New System.Drawing.Size(194, 22)
        Me.cmnuTrayExit.Text = "Exit"
        '
        'pnlLoadSettingsBG
        '
        Me.pnlLoadSettingsBG.BackColor = System.Drawing.Color.White
        Me.pnlLoadSettingsBG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlLoadSettingsBG.Controls.Add(Me.pbLoadSettings)
        Me.pnlLoadSettingsBG.Controls.Add(Me.lblLoadSettings)
        Me.pnlLoadSettingsBG.Controls.Add(Me.prbLoadSettings)
        Me.pnlLoadSettingsBG.Location = New System.Drawing.Point(3, 3)
        Me.pnlLoadSettingsBG.Name = "pnlLoadSettingsBG"
        Me.pnlLoadSettingsBG.Size = New System.Drawing.Size(249, 111)
        Me.pnlLoadSettingsBG.TabIndex = 2
        '
        'pbLoadSettings
        '
        Me.pbLoadSettings.Image = CType(resources.GetObject("pbLoadSettings.Image"), System.Drawing.Image)
        Me.pbLoadSettings.Location = New System.Drawing.Point(12, 11)
        Me.pbLoadSettings.Name = "pbLoadSettings"
        Me.pbLoadSettings.Size = New System.Drawing.Size(48, 48)
        Me.pbLoadSettings.TabIndex = 2
        Me.pbLoadSettings.TabStop = False
        '
        'lblLoadSettings
        '
        Me.lblLoadSettings.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoadSettings.Location = New System.Drawing.Point(63, 11)
        Me.lblLoadSettings.Name = "lblLoadSettings"
        Me.lblLoadSettings.Size = New System.Drawing.Size(175, 48)
        Me.lblLoadSettings.TabIndex = 0
        Me.lblLoadSettings.Text = "Loading Settings..."
        Me.lblLoadSettings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'prbLoadSettings
        '
        Me.prbLoadSettings.Location = New System.Drawing.Point(8, 68)
        Me.prbLoadSettings.MarqueeAnimationSpeed = 25
        Me.prbLoadSettings.Name = "prbLoadSettings"
        Me.prbLoadSettings.Size = New System.Drawing.Size(231, 23)
        Me.prbLoadSettings.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.prbLoadSettings.TabIndex = 1
        '
        'pnlLoadSettings
        '
        Me.pnlLoadSettings.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlLoadSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlLoadSettings.Controls.Add(Me.pnlLoadSettingsBG)
        Me.pnlLoadSettings.Location = New System.Drawing.Point(380, 287)
        Me.pnlLoadSettings.Name = "pnlLoadSettings"
        Me.pnlLoadSettings.Size = New System.Drawing.Size(257, 119)
        Me.pnlLoadSettings.TabIndex = 13
        Me.pnlLoadSettings.Visible = False
        '
        'tmrAppExit
        '
        Me.tmrAppExit.Interval = 1000
        '
        'tmrKeyBuffer
        '
        Me.tmrKeyBuffer.Interval = 1000
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1354, 733)
        Me.Controls.Add(Me.pnlLoadSettings)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.mnuMain)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnuMain
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "frmMain"
        Me.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Text = "Ember Media Manager"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        Me.scMain.Panel2.PerformLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.pnlFilterGenre.ResumeLayout(False)
        Me.pnlFilterGenre.PerformLayout()
        Me.pnlFilterSource.ResumeLayout(False)
        Me.pnlFilterSource.PerformLayout()
        CType(Me.dgvMovies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmnuMovie.ResumeLayout(False)
        Me.scTV.Panel1.ResumeLayout(False)
        Me.scTV.Panel2.ResumeLayout(False)
        CType(Me.scTV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scTV.ResumeLayout(False)
        CType(Me.dgvTVShows, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmnuShow.ResumeLayout(False)
        Me.scTVSeasonsEpisodes.Panel1.ResumeLayout(False)
        Me.scTVSeasonsEpisodes.Panel2.ResumeLayout(False)
        CType(Me.scTVSeasonsEpisodes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scTVSeasonsEpisodes.ResumeLayout(False)
        CType(Me.dgvTVSeasons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmnuSeason.ResumeLayout(False)
        CType(Me.dgvTVEpisodes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmnuEpisode.ResumeLayout(False)
        Me.pnlListTop.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.picSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcMain.ResumeLayout(False)
        Me.pnlFilter.ResumeLayout(False)
        Me.gbSort.ResumeLayout(False)
        Me.gbFilterGeneral.ResumeLayout(False)
        Me.gbFilterGeneral.PerformLayout()
        Me.gbFilterSpecific.ResumeLayout(False)
        Me.gbFilterSpecific.PerformLayout()
        Me.gbFilterModifier.ResumeLayout(False)
        Me.gbFilterModifier.PerformLayout()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlInfoIcons.ResumeLayout(False)
        Me.pnlInfoIcons.PerformLayout()
        CType(Me.pbVType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStudio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbVideo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbAudio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbResolution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbChannels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRating.ResumeLayout(False)
        Me.pnlRating.PerformLayout()
        CType(Me.pbStar5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCancel.ResumeLayout(False)
        Me.pnlCancel.PerformLayout()
        Me.pnlAllSeason.ResumeLayout(False)
        CType(Me.pbAllSeason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbAllSeasonCache, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlNoInfo.ResumeLayout(False)
        Me.pnlNoInfoBG.ResumeLayout(False)
        CType(Me.pbNoInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInfoPanel.ResumeLayout(False)
        Me.pnlInfoPanel.PerformLayout()
        CType(Me.pbMILoading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlActors.ResumeLayout(False)
        CType(Me.pbActLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbActors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop250.ResumeLayout(False)
        CType(Me.pbTop250, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPoster.ResumeLayout(False)
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbPosterCache, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFanartSmallCache, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFanartSmall.ResumeLayout(False)
        CType(Me.pbFanartSmall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMPAA.ResumeLayout(False)
        Me.pnlMPAA.PerformLayout()
        CType(Me.pbMPAA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFanartCache, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tsMain.ResumeLayout(False)
        Me.tsMain.PerformLayout()
        Me.cmnuTray.ResumeLayout(False)
        Me.pnlLoadSettingsBG.ResumeLayout(False)
        CType(Me.pbLoadSettings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLoadSettings.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BottomToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents TopToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents RightToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents LeftToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents ContentPanel As System.Windows.Forms.ToolStripContentPanel
    Friend WithEvents mnuMainFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainFileExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainEditSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents tslStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tslLoading As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tspbLoading As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents tmrAni As System.Windows.Forms.Timer
    Friend WithEvents mnuMainHelpAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents scMain As System.Windows.Forms.SplitContainer
    Friend WithEvents pbFanartCache As System.Windows.Forms.PictureBox
    Friend WithEvents pnlPoster As System.Windows.Forms.Panel
    Friend WithEvents pbPoster As System.Windows.Forms.PictureBox
    Friend WithEvents pbFanart As System.Windows.Forms.PictureBox
    Friend WithEvents tcMain As System.Windows.Forms.TabControl
    Friend WithEvents pnlMPAA As System.Windows.Forms.Panel
    Friend WithEvents pbMPAA As System.Windows.Forms.PictureBox
    Friend WithEvents pnlInfoPanel As System.Windows.Forms.Panel
    Friend WithEvents btnPlay As System.Windows.Forms.Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents lblFilePathHeader As System.Windows.Forms.Label
    Friend WithEvents txtIMDBID As System.Windows.Forms.TextBox
    Friend WithEvents lblIMDBHeader As System.Windows.Forms.Label
    Friend WithEvents lblDirector As System.Windows.Forms.Label
    Friend WithEvents lblDirectorHeader As System.Windows.Forms.Label
    Friend WithEvents pnlActors As System.Windows.Forms.Panel
    Friend WithEvents pbActLoad As System.Windows.Forms.PictureBox
    Friend WithEvents lstActors As System.Windows.Forms.ListBox
    Friend WithEvents pbActors As System.Windows.Forms.PictureBox
    Friend WithEvents lblActorsHeader As System.Windows.Forms.Label
    Friend WithEvents lblOutlineHeader As System.Windows.Forms.Label
    Friend WithEvents txtOutline As System.Windows.Forms.TextBox
    Friend WithEvents pnlTop250 As System.Windows.Forms.Panel
    Friend WithEvents lblTop250 As System.Windows.Forms.Label
    Friend WithEvents pbTop250 As System.Windows.Forms.PictureBox
    Friend WithEvents lblPlotHeader As System.Windows.Forms.Label
    Friend WithEvents txtPlot As System.Windows.Forms.TextBox
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents lblInfoPanelHeader As System.Windows.Forms.Label
    Friend WithEvents tsMain As System.Windows.Forms.ToolStrip
    Friend WithEvents btnMetaDataRefresh As System.Windows.Forms.Button
    Friend WithEvents lblMetaDataHeader As System.Windows.Forms.Label
    Friend WithEvents txtMetaData As System.Windows.Forms.TextBox
    Friend WithEvents pbMILoading As System.Windows.Forms.PictureBox
    Friend WithEvents tsbAutoPilot As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents btnMid As System.Windows.Forms.Button
    Friend WithEvents pbPosterCache As System.Windows.Forms.PictureBox
    Friend WithEvents txtCerts As System.Windows.Forms.TextBox
    Friend WithEvents lblCertsHeader As System.Windows.Forms.Label
    Friend WithEvents lblReleaseDate As System.Windows.Forms.Label
    Friend WithEvents lblReleaseDateHeader As System.Windows.Forms.Label
    Friend WithEvents ilColumnIcons As System.Windows.Forms.ImageList
    Friend WithEvents mnuAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMiss As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrWait As System.Windows.Forms.Timer
    Friend WithEvents tmrLoad As System.Windows.Forms.Timer
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents picSearch As System.Windows.Forms.PictureBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents tmrSearchWait As System.Windows.Forms.Timer
    Friend WithEvents tmrSearch As System.Windows.Forms.Timer
    Friend WithEvents lblNoInfo As System.Windows.Forms.Label
    Friend WithEvents pbNoInfo As System.Windows.Forms.PictureBox
    Friend WithEvents mnuMainTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsCleanFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsSortFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlFilter As System.Windows.Forms.Panel
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents chkFilterNew As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterMark As System.Windows.Forms.CheckBox
    Friend WithEvents rbFilterOr As System.Windows.Forms.RadioButton
    Friend WithEvents rbFilterAnd As System.Windows.Forms.RadioButton
    Friend WithEvents chkFilterDupe As System.Windows.Forms.CheckBox
    Friend WithEvents tsbMediaCenters As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents cmnuMovie As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmnuMovieMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieChange As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuSep As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents prbCanceling As System.Windows.Forms.ProgressBar
    Friend WithEvents lblCanceling As System.Windows.Forms.Label
    Private WithEvents pnlNoInfo As System.Windows.Forms.Panel
    Friend WithEvents pnlCancel As System.Windows.Forms.Panel
    Friend WithEvents cmnuSep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieOpenFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieRemoveFromDisk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieLock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsBackdrops As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnMarkAll As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMainToolsSetsManager As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMainToolsClearCache As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnFilterDown As System.Windows.Forms.Button
    Friend WithEvents btnFilterUp As System.Windows.Forms.Button
    Friend WithEvents tmrFilterAni As System.Windows.Forms.Timer
    Friend WithEvents lblFilterSource As System.Windows.Forms.Label
    Friend WithEvents mnuAllAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCustom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAutoMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieReload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsReloadMovies As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieGenres As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieGenresAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieGenresSet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieGenresRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieGenresGenre As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents cmnuMovieGenresTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUpdate As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents lblFilterGenre As System.Windows.Forms.Label
    Friend WithEvents clbFilterGenres As System.Windows.Forms.CheckedListBox
    Friend WithEvents txtFilterGenre As System.Windows.Forms.TextBox
    Friend WithEvents pnlFilterGenre As System.Windows.Forms.Panel
    Friend WithEvents lblGFilClose As System.Windows.Forms.Label
    Friend WithEvents lblFilterGenres As System.Windows.Forms.Label
    Friend WithEvents cbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents cbFilterYearMod As System.Windows.Forms.ComboBox
    Friend WithEvents lblFilterYear As System.Windows.Forms.Label
    Friend WithEvents cbFilterYear As System.Windows.Forms.ComboBox
    Friend WithEvents gbFilterSpecific As System.Windows.Forms.GroupBox
    Friend WithEvents gbFilterModifier As System.Windows.Forms.GroupBox
    Friend WithEvents gbFilterGeneral As System.Windows.Forms.GroupBox
    Friend WithEvents btnClearFilters As System.Windows.Forms.Button
    Friend WithEvents chkFilterLock As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterMissing As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterTolerance As System.Windows.Forms.CheckBox
    Friend WithEvents lblFilterFileSource As System.Windows.Forms.Label
    Friend WithEvents cbFilterFileSource As System.Windows.Forms.ComboBox
    Friend WithEvents cmnuMovieEditMetaData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gbSort As System.Windows.Forms.GroupBox
    Friend WithEvents btnSortDate As System.Windows.Forms.Button
    Friend WithEvents pnlFilterSource As System.Windows.Forms.Panel
    Friend WithEvents lblSFilClose As System.Windows.Forms.Label
    Friend WithEvents lblFilterSources As System.Windows.Forms.Label
    Friend WithEvents clbFilterSource As System.Windows.Forms.CheckedListBox
    Friend WithEvents txtFilterSource As System.Windows.Forms.TextBox
    Friend WithEvents mnuFilter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnSortTitle As System.Windows.Forms.Button
    Friend WithEvents ToolTips As System.Windows.Forms.ToolTip
    Friend WithEvents btnIMDBRating As System.Windows.Forms.Button
    Friend WithEvents tpShows As System.Windows.Forms.TabPage
    Friend WithEvents dgvMovies As System.Windows.Forms.DataGridView
    Friend WithEvents scTV As System.Windows.Forms.SplitContainer
    Friend WithEvents scTVSeasonsEpisodes As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlListTop As System.Windows.Forms.Panel
    Friend WithEvents pnlNoInfoBG As System.Windows.Forms.Panel
    Friend WithEvents dgvTVShows As System.Windows.Forms.DataGridView
    Friend WithEvents dgvTVSeasons As System.Windows.Forms.DataGridView
    Friend WithEvents dgvTVEpisodes As System.Windows.Forms.DataGridView
    Friend WithEvents mnuMainToolsCleanDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieRemoveFromDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuUpdateMovies As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUpdateShows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainDonate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrWaitShow As System.Windows.Forms.Timer
    Friend WithEvents tmrLoadShow As System.Windows.Forms.Timer
    Friend WithEvents tmrWaitSeason As System.Windows.Forms.Timer
    Friend WithEvents tmrLoadSeason As System.Windows.Forms.Timer
    Friend WithEvents tmrWaitEp As System.Windows.Forms.Timer
    Friend WithEvents tmrLoadEp As System.Windows.Forms.Timer
    Friend WithEvents tsSpring As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cmnuShow As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmnuSeason As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmnuEpisode As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmnuShowTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuShowEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuEpisodeEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuShowRescrape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowChange As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowReload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowLock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuEpisodeReload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeLock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuEpisodeRescrape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuShowRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowRemoveFromDisk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowRemoveFromDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuEpisodeRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeRemoveFromDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeRemoveFromDisk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoMetaData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskMetaData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainHelpVersions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonChangeImages As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuSeasonRescrape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuSeasonRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonReload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonLock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator16 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuRemoveSeasonFromDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonRemoveFromDisk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeChange As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator17 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMainHelpWiki As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator18 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator19 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMainError As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieRescrape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrayIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents cmnuTray As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuTrayExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator21 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuTrayUpdate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayScrape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator22 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuTraySettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMediaCenters As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator23 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuTrayUpdateMovies As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayUpdateShows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayToolsCleanFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayToolsSortFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayToolsBackdrops As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayToolsSetsManager As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayToolsClearCache As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayToolsReloadMovies As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayToolsCleanDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator24 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator25 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator26 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuTrayAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMiss As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayCustom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoMetaData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlLoadSettingsBG As System.Windows.Forms.Panel
    Friend WithEvents pbLoadSettings As System.Windows.Forms.PictureBox
    Friend WithEvents lblLoadSettings As System.Windows.Forms.Label
    Friend WithEvents prbLoadSettings As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlLoadSettings As System.Windows.Forms.Panel
    Friend WithEvents cmnuShowOpenFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator20 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuSeasonOpenFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator27 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuEpisodeOpenFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator28 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents pnlAllSeason As System.Windows.Forms.Panel
    Friend WithEvents pbAllSeason As System.Windows.Forms.PictureBox
    Friend WithEvents pbAllSeasonCache As System.Windows.Forms.PictureBox
    Friend WithEvents tmrAppExit As System.Windows.Forms.Timer
    Friend WithEvents mnuMainHelpUpdate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrKeyBuffer As System.Windows.Forms.Timer
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlInfoIcons As System.Windows.Forms.Panel
    Friend WithEvents lblStudio As System.Windows.Forms.Label
    Friend WithEvents pbVType As System.Windows.Forms.PictureBox
    Friend WithEvents pbStudio As System.Windows.Forms.PictureBox
    Friend WithEvents pbVideo As System.Windows.Forms.PictureBox
    Friend WithEvents pbAudio As System.Windows.Forms.PictureBox
    Friend WithEvents pbResolution As System.Windows.Forms.PictureBox
    Friend WithEvents pbChannels As System.Windows.Forms.PictureBox
    Friend WithEvents lblRuntime As System.Windows.Forms.Label
    Friend WithEvents lblTagline As System.Windows.Forms.Label
    Friend WithEvents pnlRating As System.Windows.Forms.Panel
    Friend WithEvents pbStar5 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar4 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar3 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar2 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblVotes As System.Windows.Forms.Label
    Friend WithEvents lblOriginalTitle As System.Windows.Forms.Label
    Friend WithEvents pnlFanartSmall As System.Windows.Forms.Panel
    Friend WithEvents pbFanartSmall As System.Windows.Forms.PictureBox
    Friend WithEvents pbFanartSmallCache As System.Windows.Forms.PictureBox
    Friend WithEvents mnuAllAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieWatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowWatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonWatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeWatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpMovies As System.Windows.Forms.TabPage
    Friend WithEvents mnuMainToolsOfflineHolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayToolsOfflineHolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVersion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowRefresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAllAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMissAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFilterAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayAllAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMissAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayNewAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMarkAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayFilterAskTheme As System.Windows.Forms.ToolStripMenuItem
End Class
