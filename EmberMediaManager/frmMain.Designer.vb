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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        Me.mnuMainHelpForumEng = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainHelpForumGer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainHelpSeparator0 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMainHelpVersions = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainHelpUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainHelpSeparator1 = New System.Windows.Forms.ToolStripSeparator()
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
        Me.mnuMainToolsSeparator0 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMainToolsOfflineHolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMainToolsClearCache = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsReloadMovies = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsReloadMovieSets = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsRewriteMovieContent = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsCleanDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMainToolsExport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsExportMovies = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsExportTvShows = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainDonate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainError = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.pnlFilterCountries_Movies = New System.Windows.Forms.Panel()
        Me.pnlFilterCountriesMain_Movies = New System.Windows.Forms.Panel()
        Me.clbFilterCountries_Movies = New System.Windows.Forms.CheckedListBox()
        Me.pnlFilterCountriesTop_Movies = New System.Windows.Forms.Panel()
        Me.tblFilterCountriesTop_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFilterCountries_Movies = New System.Windows.Forms.Label()
        Me.lblFilterCountriesClose_Movies = New System.Windows.Forms.Label()
        Me.pnlFilterGenres_Movies = New System.Windows.Forms.Panel()
        Me.pnlFilterGenresMain_Movies = New System.Windows.Forms.Panel()
        Me.clbFilterGenres_Movies = New System.Windows.Forms.CheckedListBox()
        Me.pnlFilterGenresTop_Movies = New System.Windows.Forms.Panel()
        Me.tblFilterGenresTop_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFilterGenresClose_Movies = New System.Windows.Forms.Label()
        Me.lblFilterGenres_Movies = New System.Windows.Forms.Label()
        Me.pnlFilterGenres_Shows = New System.Windows.Forms.Panel()
        Me.pnlFilterGenresMain_Shows = New System.Windows.Forms.Panel()
        Me.clbFilterGenres_Shows = New System.Windows.Forms.CheckedListBox()
        Me.pnlFilterGenresTop_Shows = New System.Windows.Forms.Panel()
        Me.tblFilterGenresTop_Shows = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFilterGenres_Shows = New System.Windows.Forms.Label()
        Me.lblFilterGenresClose_Shows = New System.Windows.Forms.Label()
        Me.pnlFilterDataFields_Movies = New System.Windows.Forms.Panel()
        Me.pnlFilterDataFieldsMain_Movies = New System.Windows.Forms.Panel()
        Me.clbFilterDataFields_Movies = New System.Windows.Forms.CheckedListBox()
        Me.pnlFilterDataFieldsTop_Movies = New System.Windows.Forms.Panel()
        Me.tblFilterDataFieldsTop_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFilterDataFields_Movies = New System.Windows.Forms.Label()
        Me.lblFilterDataFieldsClose_Movies = New System.Windows.Forms.Label()
        Me.pnlFilterMissingItems_Movies = New System.Windows.Forms.Panel()
        Me.pnlFilterMissingItemsMain_Movies = New System.Windows.Forms.Panel()
        Me.tblFilterMissingItemsMain_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieMissingBanner = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingClearArt = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingDiscArt = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingEFanarts = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingEThumbs = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingFanart = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingLandscape = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingTrailer = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingTheme = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingSubtitles = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingPoster = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingNFO = New System.Windows.Forms.CheckBox()
        Me.pnlFilterMissingItemsTop_Movies = New System.Windows.Forms.Panel()
        Me.tblFilterMissingItemsTop_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFilterMissingItems_Movies = New System.Windows.Forms.Label()
        Me.lblFilterMissingItemsClose_Movies = New System.Windows.Forms.Label()
        Me.pnlFilterMissingItems_MovieSets = New System.Windows.Forms.Panel()
        Me.pnlFilterMissingItemsMain_MovieSets = New System.Windows.Forms.Panel()
        Me.tlbFilterMissingItemsMain_MovieSets = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieSetMissingBanner = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetMissingClearArt = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetMissingClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetMissingDiscArt = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetMissingFanart = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetMissingLandscape = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetMissingPoster = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetMissingNFO = New System.Windows.Forms.CheckBox()
        Me.pnlFilterMissingItemsTop_MovieSets = New System.Windows.Forms.Panel()
        Me.tblFilterMissingItemsTop_MovieSets = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFilterMissingItems_MovieSets = New System.Windows.Forms.Label()
        Me.lblFilterMissingItemsClose_MovieSets = New System.Windows.Forms.Label()
        Me.pnlFilterMissingItems_Shows = New System.Windows.Forms.Panel()
        Me.pnlFilterMissingItemsMain_Shows = New System.Windows.Forms.Panel()
        Me.tblFilterMissingItemsMain_Shows = New System.Windows.Forms.TableLayoutPanel()
        Me.chkShowMissingBanner = New System.Windows.Forms.CheckBox()
        Me.chkShowMissingClearArt = New System.Windows.Forms.CheckBox()
        Me.chkShowMissingClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkShowMissingEFanarts = New System.Windows.Forms.CheckBox()
        Me.chkShowMissingFanart = New System.Windows.Forms.CheckBox()
        Me.chkShowMissingLandscape = New System.Windows.Forms.CheckBox()
        Me.chkShowMissingPoster = New System.Windows.Forms.CheckBox()
        Me.chkShowMissingNFO = New System.Windows.Forms.CheckBox()
        Me.chkShowMissingCharacterArt = New System.Windows.Forms.CheckBox()
        Me.chkShowMissingTheme = New System.Windows.Forms.CheckBox()
        Me.pnlFilterMissingItemsTop_Shows = New System.Windows.Forms.Panel()
        Me.tblFilterMissingItemsTop_Shows = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFilterMissingItems_Shows = New System.Windows.Forms.Label()
        Me.lblFilterMissingItemsClose_Shows = New System.Windows.Forms.Label()
        Me.pnlFilterSources_Movies = New System.Windows.Forms.Panel()
        Me.pnlFilterSourcesMain_Movies = New System.Windows.Forms.Panel()
        Me.clbFilterSources_Movies = New System.Windows.Forms.CheckedListBox()
        Me.pnlFilterSourcesTop_Movies = New System.Windows.Forms.Panel()
        Me.tblFilterSourcesTop_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFilterSources_Movies = New System.Windows.Forms.Label()
        Me.lblFilterSourcesClose_Movies = New System.Windows.Forms.Label()
        Me.pnlFilterSources_Shows = New System.Windows.Forms.Panel()
        Me.pnlFilterSourcesMain_Shows = New System.Windows.Forms.Panel()
        Me.clbFilterSource_Shows = New System.Windows.Forms.CheckedListBox()
        Me.pnlFilterSourcesTop_Shows = New System.Windows.Forms.Panel()
        Me.tblFilterSourcesTop_Shows = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFilterSources_Shows = New System.Windows.Forms.Label()
        Me.lblFilterSourcesClose_Shows = New System.Windows.Forms.Label()
        Me.dgvMovies = New System.Windows.Forms.DataGridView()
        Me.cmnuMovie = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuMovieTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieReload = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieMarkAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieMarkAsCustom1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieMarkAsCustom2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieMarkAsCustom3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieMarkAsCustom4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieLock = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieWatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieEditMetaData = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieGenres = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieGenresTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieGenresGenre = New System.Windows.Forms.ToolStripComboBox()
        Me.cmnuMovieGenresAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieGenresSet = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieGenresRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSep3 = New System.Windows.Forms.ToolStripSeparator()
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
        Me.cmnuMovieMovieReSelAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskMetaData = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieReSelSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSel = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelCert = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelCountry = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelDirector = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelMPAA = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelPlot = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelOutline = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelProducers = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelRelease = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelRating = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelStudio = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelTagline = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelTop250 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelWriter = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUpSelYear = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieChange = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieChangeAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSep4 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieBrowseIMDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieBrowseTMDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSep5 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieRemoveFromDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieRemoveFromDisc = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgvMovieSets = New System.Windows.Forms.DataGridView()
        Me.cmnuMovieSet = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuMovieSetTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieSetReload = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetLock = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetSep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieSetNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetSep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieSetRescrape = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.cmnuShowLanguage = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowLanguageLanguages = New System.Windows.Forms.ToolStripComboBox()
        Me.cmnuShowLanguageSet = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.cmnuSeasonRemoveFromDB = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.pnlSearchMovies = New System.Windows.Forms.Panel()
        Me.cbSearchMovies = New System.Windows.Forms.ComboBox()
        Me.picSearchMovies = New System.Windows.Forms.PictureBox()
        Me.txtSearchMovies = New System.Windows.Forms.TextBox()
        Me.pnlSearchMovieSets = New System.Windows.Forms.Panel()
        Me.cbSearchMovieSets = New System.Windows.Forms.ComboBox()
        Me.picSearchMovieSets = New System.Windows.Forms.PictureBox()
        Me.txtSearchMovieSets = New System.Windows.Forms.TextBox()
        Me.pnlSearchTVShows = New System.Windows.Forms.Panel()
        Me.cbSearchShows = New System.Windows.Forms.ComboBox()
        Me.picSearchTVShows = New System.Windows.Forms.PictureBox()
        Me.txtSearchShows = New System.Windows.Forms.TextBox()
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.tpMovies = New System.Windows.Forms.TabPage()
        Me.tpMovieSets = New System.Windows.Forms.TabPage()
        Me.tpTVShows = New System.Windows.Forms.TabPage()
        Me.pnlFilter_Movies = New System.Windows.Forms.Panel()
        Me.tblFilter_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.gbFilterGeneral_Movies = New System.Windows.Forms.GroupBox()
        Me.tblFilterGeneral_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.chkFilterTolerance_Movies = New System.Windows.Forms.CheckBox()
        Me.chkFilterDuplicates_Movies = New System.Windows.Forms.CheckBox()
        Me.chkFilterMissing_Movies = New System.Windows.Forms.CheckBox()
        Me.btnFilterMissing_Movies = New System.Windows.Forms.Button()
        Me.gbFilterSorting_Movies = New System.Windows.Forms.GroupBox()
        Me.tblFilterSorting_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.btnFilterSortYear_Movies = New System.Windows.Forms.Button()
        Me.btnFilterSortRating_Movies = New System.Windows.Forms.Button()
        Me.btnFilterSortDateAdded_Movies = New System.Windows.Forms.Button()
        Me.btnFilterSortTitle_Movies = New System.Windows.Forms.Button()
        Me.btnFilterSortDateModified_Movies = New System.Windows.Forms.Button()
        Me.btnClearFilters_Movies = New System.Windows.Forms.Button()
        Me.gbFilterSpecific_Movies = New System.Windows.Forms.GroupBox()
        Me.tblFilterSpecific_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.gbFilterModifier_Movies = New System.Windows.Forms.GroupBox()
        Me.tblFilterModifier_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.rbFilterOr_Movies = New System.Windows.Forms.RadioButton()
        Me.rbFilterAnd_Movies = New System.Windows.Forms.RadioButton()
        Me.tblFilterSpecificData_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.gbFilterDataField_Movies = New System.Windows.Forms.GroupBox()
        Me.tblFilterDataField_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.txtFilterDataField_Movies = New System.Windows.Forms.TextBox()
        Me.cbFilterDataField_Movies = New System.Windows.Forms.ComboBox()
        Me.lblFilterGenre_Movies = New System.Windows.Forms.Label()
        Me.txtFilterGenre_Movies = New System.Windows.Forms.TextBox()
        Me.lblFilterCountry_Movies = New System.Windows.Forms.Label()
        Me.txtFilterCountry_Movies = New System.Windows.Forms.TextBox()
        Me.lblFilterVideoSource_Movies = New System.Windows.Forms.Label()
        Me.cbFilterVideoSource_Movies = New System.Windows.Forms.ComboBox()
        Me.lblFilterSource_Movies = New System.Windows.Forms.Label()
        Me.txtFilterSource_Movies = New System.Windows.Forms.TextBox()
        Me.cbFilterYearModFrom_Movies = New System.Windows.Forms.ComboBox()
        Me.lblFilterYear_Movies = New System.Windows.Forms.Label()
        Me.cbFilterYearModTo_Movies = New System.Windows.Forms.ComboBox()
        Me.cbFilterYearFrom_Movies = New System.Windows.Forms.ComboBox()
        Me.cbFilterYearTo_Movies = New System.Windows.Forms.ComboBox()
        Me.chkFilterNew_Movies = New System.Windows.Forms.CheckBox()
        Me.chkFilterMark_Movies = New System.Windows.Forms.CheckBox()
        Me.chkFilterMarkCustom1_Movies = New System.Windows.Forms.CheckBox()
        Me.chkFilterMarkCustom4_Movies = New System.Windows.Forms.CheckBox()
        Me.chkFilterMarkCustom2_Movies = New System.Windows.Forms.CheckBox()
        Me.chkFilterMarkCustom3_Movies = New System.Windows.Forms.CheckBox()
        Me.chkFilterLock_Movies = New System.Windows.Forms.CheckBox()
        Me.pnlFilterTop_Movies = New System.Windows.Forms.Panel()
        Me.tblFilterTop_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFilter_Movies = New System.Windows.Forms.Label()
        Me.btnFilterUp_Movies = New System.Windows.Forms.Button()
        Me.btnFilterDown_Movies = New System.Windows.Forms.Button()
        Me.pnlFilter_MovieSets = New System.Windows.Forms.Panel()
        Me.tblFilter_MovieSets = New System.Windows.Forms.TableLayoutPanel()
        Me.gbFilterGeneral_MovieSets = New System.Windows.Forms.GroupBox()
        Me.tblFilterGeneral_MovieSets = New System.Windows.Forms.TableLayoutPanel()
        Me.btnFilterMissing_MovieSets = New System.Windows.Forms.Button()
        Me.chkFilterMissing_MovieSets = New System.Windows.Forms.CheckBox()
        Me.chkFilterOne_MovieSets = New System.Windows.Forms.CheckBox()
        Me.chkFilterEmpty_MovieSets = New System.Windows.Forms.CheckBox()
        Me.gbFilterSpecific_MovieSets = New System.Windows.Forms.GroupBox()
        Me.tblFilterSpecific_MovieSets = New System.Windows.Forms.TableLayoutPanel()
        Me.gbFilterModifier_MovieSets = New System.Windows.Forms.GroupBox()
        Me.tblFilterModifier_MovieSets = New System.Windows.Forms.TableLayoutPanel()
        Me.rbFilterOr_MovieSets = New System.Windows.Forms.RadioButton()
        Me.rbFilterAnd_MovieSets = New System.Windows.Forms.RadioButton()
        Me.chkFilterLock_MovieSets = New System.Windows.Forms.CheckBox()
        Me.chkFilterNew_MovieSets = New System.Windows.Forms.CheckBox()
        Me.chkFilterMark_MovieSets = New System.Windows.Forms.CheckBox()
        Me.btnClearFilters_MovieSets = New System.Windows.Forms.Button()
        Me.pnlFilterTop_MovieSets = New System.Windows.Forms.Panel()
        Me.tblFilterTop_MovieSets = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFilter_MovieSets = New System.Windows.Forms.Label()
        Me.btnFilterUp_MovieSets = New System.Windows.Forms.Button()
        Me.btnFilterDown_MovieSets = New System.Windows.Forms.Button()
        Me.pnlFilter_Shows = New System.Windows.Forms.Panel()
        Me.tblFilter_Shows = New System.Windows.Forms.TableLayoutPanel()
        Me.gbFilterGeneral_Shows = New System.Windows.Forms.GroupBox()
        Me.tblFilterGeneral_Shows = New System.Windows.Forms.TableLayoutPanel()
        Me.btnFilterMissing_Shows = New System.Windows.Forms.Button()
        Me.chkFilterMissing_Shows = New System.Windows.Forms.CheckBox()
        Me.gbFilterSpecific_Shows = New System.Windows.Forms.GroupBox()
        Me.tblFilterSpecific_Shows = New System.Windows.Forms.TableLayoutPanel()
        Me.chkFilterLock_Shows = New System.Windows.Forms.CheckBox()
        Me.gbFilterModifier_Shows = New System.Windows.Forms.GroupBox()
        Me.tblFilterModifier_Shows = New System.Windows.Forms.TableLayoutPanel()
        Me.rbFilterOr_Shows = New System.Windows.Forms.RadioButton()
        Me.rbFilterAnd_Shows = New System.Windows.Forms.RadioButton()
        Me.chkFilterMark_Shows = New System.Windows.Forms.CheckBox()
        Me.tblFilterSpecificData_Shows = New System.Windows.Forms.TableLayoutPanel()
        Me.txtFilterSource_Shows = New System.Windows.Forms.TextBox()
        Me.lblFilterGenre_Shows = New System.Windows.Forms.Label()
        Me.txtFilterGenre_Shows = New System.Windows.Forms.TextBox()
        Me.lblFilterSource_Shows = New System.Windows.Forms.Label()
        Me.chkFilterNew_Shows = New System.Windows.Forms.CheckBox()
        Me.btnClearFilters_Shows = New System.Windows.Forms.Button()
        Me.pnlFilterTop_Shows = New System.Windows.Forms.Panel()
        Me.tblFilterTop_Shows = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFilter_Shows = New System.Windows.Forms.Label()
        Me.btnFilterUp_Shows = New System.Windows.Forms.Button()
        Me.btnFilterDown_Shows = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tlpHeader = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblOriginalTitle = New System.Windows.Forms.Label()
        Me.pnlRating = New System.Windows.Forms.Panel()
        Me.pbStar10 = New System.Windows.Forms.PictureBox()
        Me.pbStar9 = New System.Windows.Forms.PictureBox()
        Me.pbStar8 = New System.Windows.Forms.PictureBox()
        Me.pbStar7 = New System.Windows.Forms.PictureBox()
        Me.pbStar6 = New System.Windows.Forms.PictureBox()
        Me.pbStar5 = New System.Windows.Forms.PictureBox()
        Me.pbStar4 = New System.Windows.Forms.PictureBox()
        Me.pbStar3 = New System.Windows.Forms.PictureBox()
        Me.pbStar2 = New System.Windows.Forms.PictureBox()
        Me.pbStar1 = New System.Windows.Forms.PictureBox()
        Me.lblTagline = New System.Windows.Forms.Label()
        Me.lblRuntime = New System.Windows.Forms.Label()
        Me.lblRating = New System.Windows.Forms.Label()
        Me.pnlInfoIcons = New System.Windows.Forms.Panel()
        Me.pbSubtitleLang6 = New System.Windows.Forms.PictureBox()
        Me.pbSubtitleLang5 = New System.Windows.Forms.PictureBox()
        Me.pbSubtitleLang4 = New System.Windows.Forms.PictureBox()
        Me.pbSubtitleLang3 = New System.Windows.Forms.PictureBox()
        Me.pbSubtitleLang2 = New System.Windows.Forms.PictureBox()
        Me.pbSubtitleLang1 = New System.Windows.Forms.PictureBox()
        Me.pbSubtitleLang0 = New System.Windows.Forms.PictureBox()
        Me.pbAudioLang6 = New System.Windows.Forms.PictureBox()
        Me.pbAudioLang5 = New System.Windows.Forms.PictureBox()
        Me.pbAudioLang4 = New System.Windows.Forms.PictureBox()
        Me.pbAudioLang3 = New System.Windows.Forms.PictureBox()
        Me.pbAudioLang2 = New System.Windows.Forms.PictureBox()
        Me.pbAudioLang1 = New System.Windows.Forms.PictureBox()
        Me.pbAudioLang0 = New System.Windows.Forms.PictureBox()
        Me.lblStudio = New System.Windows.Forms.Label()
        Me.pbVType = New System.Windows.Forms.PictureBox()
        Me.pbStudio = New System.Windows.Forms.PictureBox()
        Me.pbVideo = New System.Windows.Forms.PictureBox()
        Me.pbAudio = New System.Windows.Forms.PictureBox()
        Me.pbResolution = New System.Windows.Forms.PictureBox()
        Me.pbChannels = New System.Windows.Forms.PictureBox()
        Me.pnlCancel = New System.Windows.Forms.Panel()
        Me.prbCanceling = New System.Windows.Forms.ProgressBar()
        Me.lblCanceling = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlNoInfo = New System.Windows.Forms.Panel()
        Me.pnlNoInfoBG = New System.Windows.Forms.Panel()
        Me.pbNoInfo = New System.Windows.Forms.PictureBox()
        Me.lblNoInfo = New System.Windows.Forms.Label()
        Me.pnlInfoPanel = New System.Windows.Forms.Panel()
        Me.pnlMoviesInSet = New System.Windows.Forms.Panel()
        Me.lvMoviesInSet = New System.Windows.Forms.ListView()
        Me.ilMoviesInSet = New System.Windows.Forms.ImageList(Me.components)
        Me.lblMoviesInSetHeader = New System.Windows.Forms.Label()
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
        Me.pnlLandscape = New System.Windows.Forms.Panel()
        Me.pbLandscape = New System.Windows.Forms.PictureBox()
        Me.pbLandscapeCache = New System.Windows.Forms.PictureBox()
        Me.pnlClearArt = New System.Windows.Forms.Panel()
        Me.pbClearArt = New System.Windows.Forms.PictureBox()
        Me.pbClearArtCache = New System.Windows.Forms.PictureBox()
        Me.pnlMPAA = New System.Windows.Forms.Panel()
        Me.pbMPAA = New System.Windows.Forms.PictureBox()
        Me.pbFanartCache = New System.Windows.Forms.PictureBox()
        Me.pbFanart = New System.Windows.Forms.PictureBox()
        Me.tsMain = New System.Windows.Forms.ToolStrip()
        Me.mnuScrapeMovies = New System.Windows.Forms.ToolStripDropDownButton()
        Me.mnuMovieAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieAllSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMiss = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMissSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieNewSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieMarkSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilter = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieFilterSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieCustom = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieRestart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeMovieSets = New System.Windows.Forms.ToolStripDropDownButton()
        Me.mnuMovieSetAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetAllSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMiss = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMissSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetNewSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetMarkSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilter = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetFilterSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetCustom = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMovieSetRestart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUpdate = New System.Windows.Forms.ToolStripSplitButton()
        Me.mnuUpdateMovies = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUpdateShows = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsbMediaCenters = New System.Windows.Forms.ToolStripSplitButton()
        Me.ilColumnIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.tmrWaitMovie = New System.Windows.Forms.Timer(Me.components)
        Me.tmrLoadMovie = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearchWait_Movies = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearch_Movies = New System.Windows.Forms.Timer(Me.components)
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
        Me.cmnuTrayMovieAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoMetaData = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieAllSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMiss = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMissSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieNewSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieMarkSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilter = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAutoTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskActor = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskEFanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskEThumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskNfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterAskTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieFilterSkipAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieCustom = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMovieRestart = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayMediaCenters = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator23 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuTrayTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsCleanFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsSortFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsBackdrops = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator24 = New System.Windows.Forms.ToolStripSeparator()
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
        Me.tmrLoadMovieSet = New System.Windows.Forms.Timer(Me.components)
        Me.tmrWaitMovieSet = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearchWait_MovieSets = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearch_MovieSets = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearchWait_Shows = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearch_Shows = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip.SuspendLayout
        Me.mnuMain.SuspendLayout
        CType(Me.scMain,System.ComponentModel.ISupportInitialize).BeginInit
        Me.scMain.Panel1.SuspendLayout
        Me.scMain.Panel2.SuspendLayout
        Me.scMain.SuspendLayout
        Me.pnlFilterCountries_Movies.SuspendLayout
        Me.pnlFilterCountriesMain_Movies.SuspendLayout
        Me.pnlFilterCountriesTop_Movies.SuspendLayout
        Me.tblFilterCountriesTop_Movies.SuspendLayout
        Me.pnlFilterGenres_Movies.SuspendLayout
        Me.pnlFilterGenresMain_Movies.SuspendLayout
        Me.pnlFilterGenresTop_Movies.SuspendLayout
        Me.tblFilterGenresTop_Movies.SuspendLayout
        Me.pnlFilterGenres_Shows.SuspendLayout
        Me.pnlFilterGenresMain_Shows.SuspendLayout
        Me.pnlFilterGenresTop_Shows.SuspendLayout
        Me.tblFilterGenresTop_Shows.SuspendLayout
        Me.pnlFilterDataFields_Movies.SuspendLayout
        Me.pnlFilterDataFieldsMain_Movies.SuspendLayout
        Me.pnlFilterDataFieldsTop_Movies.SuspendLayout
        Me.tblFilterDataFieldsTop_Movies.SuspendLayout
        Me.pnlFilterMissingItems_Movies.SuspendLayout
        Me.pnlFilterMissingItemsMain_Movies.SuspendLayout
        Me.tblFilterMissingItemsMain_Movies.SuspendLayout
        Me.pnlFilterMissingItemsTop_Movies.SuspendLayout
        Me.tblFilterMissingItemsTop_Movies.SuspendLayout
        Me.pnlFilterMissingItems_MovieSets.SuspendLayout
        Me.pnlFilterMissingItemsMain_MovieSets.SuspendLayout
        Me.tlbFilterMissingItemsMain_MovieSets.SuspendLayout
        Me.pnlFilterMissingItemsTop_MovieSets.SuspendLayout
        Me.tblFilterMissingItemsTop_MovieSets.SuspendLayout
        Me.pnlFilterMissingItems_Shows.SuspendLayout
        Me.pnlFilterMissingItemsMain_Shows.SuspendLayout
        Me.tblFilterMissingItemsMain_Shows.SuspendLayout
        Me.pnlFilterMissingItemsTop_Shows.SuspendLayout
        Me.tblFilterMissingItemsTop_Shows.SuspendLayout
        Me.pnlFilterSources_Movies.SuspendLayout
        Me.pnlFilterSourcesMain_Movies.SuspendLayout
        Me.pnlFilterSourcesTop_Movies.SuspendLayout
        Me.tblFilterSourcesTop_Movies.SuspendLayout
        Me.pnlFilterSources_Shows.SuspendLayout
        Me.pnlFilterSourcesMain_Shows.SuspendLayout
        Me.pnlFilterSourcesTop_Shows.SuspendLayout
        Me.tblFilterSourcesTop_Shows.SuspendLayout
        CType(Me.dgvMovies,System.ComponentModel.ISupportInitialize).BeginInit
        Me.cmnuMovie.SuspendLayout
        CType(Me.dgvMovieSets,System.ComponentModel.ISupportInitialize).BeginInit
        Me.cmnuMovieSet.SuspendLayout
        CType(Me.scTV,System.ComponentModel.ISupportInitialize).BeginInit
        Me.scTV.Panel1.SuspendLayout
        Me.scTV.Panel2.SuspendLayout
        Me.scTV.SuspendLayout
        CType(Me.dgvTVShows,System.ComponentModel.ISupportInitialize).BeginInit
        Me.cmnuShow.SuspendLayout
        CType(Me.scTVSeasonsEpisodes,System.ComponentModel.ISupportInitialize).BeginInit
        Me.scTVSeasonsEpisodes.Panel1.SuspendLayout
        Me.scTVSeasonsEpisodes.Panel2.SuspendLayout
        Me.scTVSeasonsEpisodes.SuspendLayout
        CType(Me.dgvTVSeasons,System.ComponentModel.ISupportInitialize).BeginInit
        Me.cmnuSeason.SuspendLayout
        CType(Me.dgvTVEpisodes,System.ComponentModel.ISupportInitialize).BeginInit
        Me.cmnuEpisode.SuspendLayout
        Me.pnlListTop.SuspendLayout
        Me.pnlSearchMovies.SuspendLayout
        CType(Me.picSearchMovies,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlSearchMovieSets.SuspendLayout
        CType(Me.picSearchMovieSets,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlSearchTVShows.SuspendLayout
        CType(Me.picSearchTVShows,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tcMain.SuspendLayout
        Me.pnlFilter_Movies.SuspendLayout
        Me.tblFilter_Movies.SuspendLayout
        Me.gbFilterGeneral_Movies.SuspendLayout
        Me.tblFilterGeneral_Movies.SuspendLayout
        Me.gbFilterSorting_Movies.SuspendLayout
        Me.tblFilterSorting_Movies.SuspendLayout
        Me.gbFilterSpecific_Movies.SuspendLayout
        Me.tblFilterSpecific_Movies.SuspendLayout
        Me.gbFilterModifier_Movies.SuspendLayout
        Me.tblFilterModifier_Movies.SuspendLayout
        Me.tblFilterSpecificData_Movies.SuspendLayout
        Me.gbFilterDataField_Movies.SuspendLayout
        Me.tblFilterDataField_Movies.SuspendLayout
        Me.pnlFilterTop_Movies.SuspendLayout
        Me.tblFilterTop_Movies.SuspendLayout
        Me.pnlFilter_MovieSets.SuspendLayout
        Me.tblFilter_MovieSets.SuspendLayout
        Me.gbFilterGeneral_MovieSets.SuspendLayout
        Me.tblFilterGeneral_MovieSets.SuspendLayout
        Me.gbFilterSpecific_MovieSets.SuspendLayout
        Me.tblFilterSpecific_MovieSets.SuspendLayout
        Me.gbFilterModifier_MovieSets.SuspendLayout
        Me.tblFilterModifier_MovieSets.SuspendLayout
        Me.pnlFilterTop_MovieSets.SuspendLayout
        Me.tblFilterTop_MovieSets.SuspendLayout
        Me.pnlFilter_Shows.SuspendLayout
        Me.tblFilter_Shows.SuspendLayout
        Me.gbFilterGeneral_Shows.SuspendLayout
        Me.tblFilterGeneral_Shows.SuspendLayout
        Me.gbFilterSpecific_Shows.SuspendLayout
        Me.tblFilterSpecific_Shows.SuspendLayout
        Me.gbFilterModifier_Shows.SuspendLayout
        Me.tblFilterModifier_Shows.SuspendLayout
        Me.tblFilterSpecificData_Shows.SuspendLayout
        Me.pnlFilterTop_Shows.SuspendLayout
        Me.tblFilterTop_Shows.SuspendLayout
        Me.pnlTop.SuspendLayout
        Me.tlpHeader.SuspendLayout
        Me.pnlRating.SuspendLayout
        CType(Me.pbStar10,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbStar9,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbStar8,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbStar7,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbStar6,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbStar5,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbStar4,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbStar3,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbStar2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbStar1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlInfoIcons.SuspendLayout
        CType(Me.pbSubtitleLang6,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbSubtitleLang5,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbSubtitleLang4,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbSubtitleLang3,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbSubtitleLang2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbSubtitleLang1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbSubtitleLang0,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbAudioLang6,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbAudioLang5,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbAudioLang4,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbAudioLang3,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbAudioLang2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbAudioLang1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbAudioLang0,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbVType,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbStudio,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbVideo,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbAudio,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbResolution,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbChannels,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlCancel.SuspendLayout
        Me.pnlNoInfo.SuspendLayout
        Me.pnlNoInfoBG.SuspendLayout
        CType(Me.pbNoInfo,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlInfoPanel.SuspendLayout
        Me.pnlMoviesInSet.SuspendLayout
        CType(Me.pbMILoading,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlActors.SuspendLayout
        CType(Me.pbActLoad,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbActors,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlTop250.SuspendLayout
        CType(Me.pbTop250,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlPoster.SuspendLayout
        CType(Me.pbPoster,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbPosterCache,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbFanartSmallCache,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlFanartSmall.SuspendLayout
        CType(Me.pbFanartSmall,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlLandscape.SuspendLayout
        CType(Me.pbLandscape,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbLandscapeCache,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlClearArt.SuspendLayout
        CType(Me.pbClearArt,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbClearArtCache,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlMPAA.SuspendLayout
        CType(Me.pbMPAA,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbFanartCache,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.pbFanart,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tsMain.SuspendLayout
        Me.cmnuTray.SuspendLayout
        Me.pnlLoadSettingsBG.SuspendLayout
        CType(Me.pbLoadSettings,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlLoadSettings.SuspendLayout
        Me.SuspendLayout
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
        Me.mnuMainFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.mnuMainFile.Name = "mnuMainFile"
        Me.mnuMainFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuMainFile.Text = "&File"
        '
        'mnuMainFileExit
        '
        Me.mnuMainFileExit.Image = CType(resources.GetObject("mnuMainFileExit.Image"),System.Drawing.Image)
        Me.mnuMainFileExit.Name = "mnuMainFileExit"
        Me.mnuMainFileExit.Size = New System.Drawing.Size(92, 22)
        Me.mnuMainFileExit.Text = "E&xit"
        '
        'mnuMainEdit
        '
        Me.mnuMainEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainEditSettings})
        Me.mnuMainEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.mnuMainEdit.Name = "mnuMainEdit"
        Me.mnuMainEdit.Size = New System.Drawing.Size(39, 20)
        Me.mnuMainEdit.Text = "&Edit"
        '
        'mnuMainEditSettings
        '
        Me.mnuMainEditSettings.Image = CType(resources.GetObject("mnuMainEditSettings.Image"),System.Drawing.Image)
        Me.mnuMainEditSettings.Name = "mnuMainEditSettings"
        Me.mnuMainEditSettings.Size = New System.Drawing.Size(125, 22)
        Me.mnuMainEditSettings.Text = "&Settings..."
        '
        'mnuMainHelp
        '
        Me.mnuMainHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainHelpWiki, Me.mnuMainHelpForumEng, Me.mnuMainHelpForumGer, Me.mnuMainHelpSeparator0, Me.mnuMainHelpVersions, Me.mnuMainHelpUpdate, Me.mnuMainHelpSeparator1, Me.mnuMainHelpAbout})
        Me.mnuMainHelp.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.mnuMainHelp.Name = "mnuMainHelp"
        Me.mnuMainHelp.Size = New System.Drawing.Size(43, 20)
        Me.mnuMainHelp.Text = "&Help"
        '
        'mnuMainHelpWiki
        '
        Me.mnuMainHelpWiki.Image = CType(resources.GetObject("mnuMainHelpWiki.Image"),System.Drawing.Image)
        Me.mnuMainHelpWiki.Name = "mnuMainHelpWiki"
        Me.mnuMainHelpWiki.Size = New System.Drawing.Size(185, 22)
        Me.mnuMainHelpWiki.Text = "EmberMM.com &Wiki..."
        Me.mnuMainHelpWiki.Visible = false
        '
        'mnuMainHelpForumEng
        '
        Me.mnuMainHelpForumEng.Image = Global.Ember_Media_Manager.My.Resources.Resources.en
        Me.mnuMainHelpForumEng.Name = "mnuMainHelpForumEng"
        Me.mnuMainHelpForumEng.Size = New System.Drawing.Size(185, 22)
        Me.mnuMainHelpForumEng.Text = "English Forum"
        '
        'mnuMainHelpForumGer
        '
        Me.mnuMainHelpForumGer.Image = Global.Ember_Media_Manager.My.Resources.Resources.de
        Me.mnuMainHelpForumGer.Name = "mnuMainHelpForumGer"
        Me.mnuMainHelpForumGer.Size = New System.Drawing.Size(185, 22)
        Me.mnuMainHelpForumGer.Text = "German Forum"
        '
        'mnuMainHelpSeparator0
        '
        Me.mnuMainHelpSeparator0.Name = "mnuMainHelpSeparator0"
        Me.mnuMainHelpSeparator0.Size = New System.Drawing.Size(182, 6)
        '
        'mnuMainHelpVersions
        '
        Me.mnuMainHelpVersions.Image = CType(resources.GetObject("mnuMainHelpVersions.Image"),System.Drawing.Image)
        Me.mnuMainHelpVersions.Name = "mnuMainHelpVersions"
        Me.mnuMainHelpVersions.Size = New System.Drawing.Size(185, 22)
        Me.mnuMainHelpVersions.Text = "&Versions..."
        '
        'mnuMainHelpUpdate
        '
        Me.mnuMainHelpUpdate.Enabled = false
        Me.mnuMainHelpUpdate.Image = CType(resources.GetObject("mnuMainHelpUpdate.Image"),System.Drawing.Image)
        Me.mnuMainHelpUpdate.Name = "mnuMainHelpUpdate"
        Me.mnuMainHelpUpdate.Size = New System.Drawing.Size(185, 22)
        Me.mnuMainHelpUpdate.Text = "Check For Updates"
        Me.mnuMainHelpUpdate.Visible = false
        '
        'mnuMainHelpSeparator1
        '
        Me.mnuMainHelpSeparator1.Name = "mnuMainHelpSeparator1"
        Me.mnuMainHelpSeparator1.Size = New System.Drawing.Size(182, 6)
        '
        'mnuMainHelpAbout
        '
        Me.mnuMainHelpAbout.Image = CType(resources.GetObject("mnuMainHelpAbout.Image"),System.Drawing.Image)
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
        Me.tsSpring.Spring = true
        Me.tsSpring.Text = "  "
        '
        'tslLoading
        '
        Me.tslLoading.AutoSize = false
        Me.tslLoading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tslLoading.Name = "tslLoading"
        Me.tslLoading.Size = New System.Drawing.Size(424, 17)
        Me.tslLoading.Text = "Loading Media:"
        Me.tslLoading.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.tslLoading.Visible = false
        '
        'tspbLoading
        '
        Me.tspbLoading.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tspbLoading.AutoSize = false
        Me.tspbLoading.MarqueeAnimationSpeed = 25
        Me.tspbLoading.Name = "tspbLoading"
        Me.tspbLoading.Size = New System.Drawing.Size(150, 16)
        Me.tspbLoading.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.tspbLoading.Visible = false
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
        Me.mnuMainTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainToolsCleanFiles, Me.mnuMainToolsSortFiles, Me.mnuMainToolsBackdrops, Me.mnuMainToolsSeparator0, Me.mnuMainToolsOfflineHolder, Me.mnuMainToolsSeparator1, Me.mnuMainToolsClearCache, Me.mnuMainToolsReloadMovies, Me.mnuMainToolsReloadMovieSets, Me.mnuMainToolsRewriteMovieContent, Me.mnuMainToolsCleanDB, Me.mnuMainToolsSeparator2, Me.mnuMainToolsExport})
        Me.mnuMainTools.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.mnuMainTools.Name = "mnuMainTools"
        Me.mnuMainTools.Size = New System.Drawing.Size(46, 20)
        Me.mnuMainTools.Text = "&Tools"
        '
        'mnuMainToolsCleanFiles
        '
        Me.mnuMainToolsCleanFiles.Image = CType(resources.GetObject("mnuMainToolsCleanFiles.Image"),System.Drawing.Image)
        Me.mnuMainToolsCleanFiles.Name = "mnuMainToolsCleanFiles"
        Me.mnuMainToolsCleanFiles.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt)  _
            Or System.Windows.Forms.Keys.C),System.Windows.Forms.Keys)
        Me.mnuMainToolsCleanFiles.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsCleanFiles.Text = "&Clean Files"
        '
        'mnuMainToolsSortFiles
        '
        Me.mnuMainToolsSortFiles.Image = CType(resources.GetObject("mnuMainToolsSortFiles.Image"),System.Drawing.Image)
        Me.mnuMainToolsSortFiles.Name = "mnuMainToolsSortFiles"
        Me.mnuMainToolsSortFiles.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt)  _
            Or System.Windows.Forms.Keys.S),System.Windows.Forms.Keys)
        Me.mnuMainToolsSortFiles.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsSortFiles.Text = "&Sort Files Into Folders"
        '
        'mnuMainToolsBackdrops
        '
        Me.mnuMainToolsBackdrops.Image = CType(resources.GetObject("mnuMainToolsBackdrops.Image"),System.Drawing.Image)
        Me.mnuMainToolsBackdrops.Name = "mnuMainToolsBackdrops"
        Me.mnuMainToolsBackdrops.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt)  _
            Or System.Windows.Forms.Keys.B),System.Windows.Forms.Keys)
        Me.mnuMainToolsBackdrops.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsBackdrops.Text = "Copy Existing Fanart To &Backdrops Folder"
        '
        'mnuMainToolsSeparator0
        '
        Me.mnuMainToolsSeparator0.Name = "mnuMainToolsSeparator0"
        Me.mnuMainToolsSeparator0.Size = New System.Drawing.Size(349, 6)
        '
        'mnuMainToolsOfflineHolder
        '
        Me.mnuMainToolsOfflineHolder.Image = CType(resources.GetObject("mnuMainToolsOfflineHolder.Image"),System.Drawing.Image)
        Me.mnuMainToolsOfflineHolder.Name = "mnuMainToolsOfflineHolder"
        Me.mnuMainToolsOfflineHolder.ShortcutKeyDisplayString = ""
        Me.mnuMainToolsOfflineHolder.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt)  _
            Or System.Windows.Forms.Keys.O),System.Windows.Forms.Keys)
        Me.mnuMainToolsOfflineHolder.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsOfflineHolder.Text = "&Offline Media Manager"
        '
        'mnuMainToolsSeparator1
        '
        Me.mnuMainToolsSeparator1.Name = "mnuMainToolsSeparator1"
        Me.mnuMainToolsSeparator1.Size = New System.Drawing.Size(349, 6)
        '
        'mnuMainToolsClearCache
        '
        Me.mnuMainToolsClearCache.Image = CType(resources.GetObject("mnuMainToolsClearCache.Image"),System.Drawing.Image)
        Me.mnuMainToolsClearCache.Name = "mnuMainToolsClearCache"
        Me.mnuMainToolsClearCache.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt)  _
            Or System.Windows.Forms.Keys.A),System.Windows.Forms.Keys)
        Me.mnuMainToolsClearCache.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsClearCache.Text = "Clear &All Caches"
        '
        'mnuMainToolsReloadMovies
        '
        Me.mnuMainToolsReloadMovies.Image = CType(resources.GetObject("mnuMainToolsReloadMovies.Image"),System.Drawing.Image)
        Me.mnuMainToolsReloadMovies.Name = "mnuMainToolsReloadMovies"
        Me.mnuMainToolsReloadMovies.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt)  _
            Or System.Windows.Forms.Keys.L),System.Windows.Forms.Keys)
        Me.mnuMainToolsReloadMovies.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsReloadMovies.Tag = ""
        Me.mnuMainToolsReloadMovies.Text = "Re&load All Movies"
        '
        'mnuMainToolsReloadMovieSets
        '
        Me.mnuMainToolsReloadMovieSets.Image = CType(resources.GetObject("mnuMainToolsReloadMovieSets.Image"),System.Drawing.Image)
        Me.mnuMainToolsReloadMovieSets.Name = "mnuMainToolsReloadMovieSets"
        Me.mnuMainToolsReloadMovieSets.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsReloadMovieSets.Text = "Reload All MovieSets"
        '
        'mnuMainToolsRewriteMovieContent
        '
        Me.mnuMainToolsRewriteMovieContent.Image = CType(resources.GetObject("mnuMainToolsRewriteMovieContent.Image"),System.Drawing.Image)
        Me.mnuMainToolsRewriteMovieContent.Name = "mnuMainToolsRewriteMovieContent"
        Me.mnuMainToolsRewriteMovieContent.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsRewriteMovieContent.Text = "Rewrite All Movie Content"
        '
        'mnuMainToolsCleanDB
        '
        Me.mnuMainToolsCleanDB.Image = CType(resources.GetObject("mnuMainToolsCleanDB.Image"),System.Drawing.Image)
        Me.mnuMainToolsCleanDB.Name = "mnuMainToolsCleanDB"
        Me.mnuMainToolsCleanDB.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt)  _
            Or System.Windows.Forms.Keys.D),System.Windows.Forms.Keys)
        Me.mnuMainToolsCleanDB.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsCleanDB.Text = "Clean &Database"
        '
        'mnuMainToolsSeparator2
        '
        Me.mnuMainToolsSeparator2.Name = "mnuMainToolsSeparator2"
        Me.mnuMainToolsSeparator2.Size = New System.Drawing.Size(349, 6)
        '
        'mnuMainToolsExport
        '
        Me.mnuMainToolsExport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainToolsExportMovies, Me.mnuMainToolsExportTvShows})
        Me.mnuMainToolsExport.Image = Global.Ember_Media_Manager.My.Resources.Resources.modules
        Me.mnuMainToolsExport.Name = "mnuMainToolsExport"
        Me.mnuMainToolsExport.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsExport.Text = "Export"
        '
        'mnuMainToolsExportMovies
        '
        Me.mnuMainToolsExportMovies.Name = "mnuMainToolsExportMovies"
        Me.mnuMainToolsExportMovies.Size = New System.Drawing.Size(123, 22)
        Me.mnuMainToolsExportMovies.Text = "Movies"
        '
        'mnuMainToolsExportTvShows
        '
        Me.mnuMainToolsExportTvShows.Name = "mnuMainToolsExportTvShows"
        Me.mnuMainToolsExportTvShows.Size = New System.Drawing.Size(123, 22)
        Me.mnuMainToolsExportTvShows.Text = "TV Shows"
        '
        'mnuMainDonate
        '
        Me.mnuMainDonate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.mnuMainDonate.Image = CType(resources.GetObject("mnuMainDonate.Image"),System.Drawing.Image)
        Me.mnuMainDonate.Name = "mnuMainDonate"
        Me.mnuMainDonate.Size = New System.Drawing.Size(73, 20)
        Me.mnuMainDonate.Text = "Donate"
        '
        'mnuMainError
        '
        Me.mnuMainError.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.mnuMainError.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mnuMainError.Image = CType(resources.GetObject("mnuMainError.Image"),System.Drawing.Image)
        Me.mnuMainError.Name = "mnuMainError"
        Me.mnuMainError.Size = New System.Drawing.Size(28, 20)
        Me.mnuMainError.ToolTipText = "An Error Has Occurred"
        Me.mnuMainError.Visible = false
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
        Me.scMain.Panel1.Controls.Add(Me.pnlFilterCountries_Movies)
        Me.scMain.Panel1.Controls.Add(Me.pnlFilterGenres_Movies)
        Me.scMain.Panel1.Controls.Add(Me.pnlFilterGenres_Shows)
        Me.scMain.Panel1.Controls.Add(Me.pnlFilterDataFields_Movies)
        Me.scMain.Panel1.Controls.Add(Me.pnlFilterMissingItems_Movies)
        Me.scMain.Panel1.Controls.Add(Me.pnlFilterMissingItems_MovieSets)
        Me.scMain.Panel1.Controls.Add(Me.pnlFilterMissingItems_Shows)
        Me.scMain.Panel1.Controls.Add(Me.pnlFilterSources_Movies)
        Me.scMain.Panel1.Controls.Add(Me.pnlFilterSources_Shows)
        Me.scMain.Panel1.Controls.Add(Me.dgvMovies)
        Me.scMain.Panel1.Controls.Add(Me.dgvMovieSets)
        Me.scMain.Panel1.Controls.Add(Me.scTV)
        Me.scMain.Panel1.Controls.Add(Me.pnlListTop)
        Me.scMain.Panel1.Controls.Add(Me.pnlFilter_Movies)
        Me.scMain.Panel1.Controls.Add(Me.pnlFilter_MovieSets)
        Me.scMain.Panel1.Controls.Add(Me.pnlFilter_Shows)
        Me.scMain.Panel1.Margin = New System.Windows.Forms.Padding(3)
        Me.scMain.Panel1MinSize = 165
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.scMain.Panel2.Controls.Add(Me.pnlTop)
        Me.scMain.Panel2.Controls.Add(Me.pnlCancel)
        Me.scMain.Panel2.Controls.Add(Me.pnlNoInfo)
        Me.scMain.Panel2.Controls.Add(Me.pnlInfoPanel)
        Me.scMain.Panel2.Controls.Add(Me.pnlPoster)
        Me.scMain.Panel2.Controls.Add(Me.pbPosterCache)
        Me.scMain.Panel2.Controls.Add(Me.pbFanartSmallCache)
        Me.scMain.Panel2.Controls.Add(Me.pnlFanartSmall)
        Me.scMain.Panel2.Controls.Add(Me.pnlLandscape)
        Me.scMain.Panel2.Controls.Add(Me.pbLandscapeCache)
        Me.scMain.Panel2.Controls.Add(Me.pnlClearArt)
        Me.scMain.Panel2.Controls.Add(Me.pbClearArtCache)
        Me.scMain.Panel2.Controls.Add(Me.pnlMPAA)
        Me.scMain.Panel2.Controls.Add(Me.pbFanartCache)
        Me.scMain.Panel2.Controls.Add(Me.pbFanart)
        Me.scMain.Panel2.Controls.Add(Me.tsMain)
        Me.scMain.Panel2.Margin = New System.Windows.Forms.Padding(3)
        Me.scMain.Size = New System.Drawing.Size(1344, 687)
        Me.scMain.SplitterDistance = 567
        Me.scMain.TabIndex = 7
        Me.scMain.TabStop = false
        '
        'pnlFilterCountries_Movies
        '
        Me.pnlFilterCountries_Movies.Controls.Add(Me.pnlFilterCountriesMain_Movies)
        Me.pnlFilterCountries_Movies.Controls.Add(Me.pnlFilterCountriesTop_Movies)
        Me.pnlFilterCountries_Movies.Location = New System.Drawing.Point(0, 900)
        Me.pnlFilterCountries_Movies.Name = "pnlFilterCountries_Movies"
        Me.pnlFilterCountries_Movies.Size = New System.Drawing.Size(189, 192)
        Me.pnlFilterCountries_Movies.TabIndex = 25
        Me.pnlFilterCountries_Movies.Visible = false
        '
        'pnlFilterCountriesMain_Movies
        '
        Me.pnlFilterCountriesMain_Movies.AutoSize = true
        Me.pnlFilterCountriesMain_Movies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilterCountriesMain_Movies.Controls.Add(Me.clbFilterCountries_Movies)
        Me.pnlFilterCountriesMain_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFilterCountriesMain_Movies.Location = New System.Drawing.Point(0, 20)
        Me.pnlFilterCountriesMain_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlFilterCountriesMain_Movies.Name = "pnlFilterCountriesMain_Movies"
        Me.pnlFilterCountriesMain_Movies.Size = New System.Drawing.Size(189, 172)
        Me.pnlFilterCountriesMain_Movies.TabIndex = 26
        '
        'clbFilterCountries_Movies
        '
        Me.clbFilterCountries_Movies.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.clbFilterCountries_Movies.CheckOnClick = true
        Me.clbFilterCountries_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbFilterCountries_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.clbFilterCountries_Movies.FormattingEnabled = true
        Me.clbFilterCountries_Movies.Location = New System.Drawing.Point(0, 0)
        Me.clbFilterCountries_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.clbFilterCountries_Movies.Name = "clbFilterCountries_Movies"
        Me.clbFilterCountries_Movies.Size = New System.Drawing.Size(187, 170)
        Me.clbFilterCountries_Movies.TabIndex = 8
        Me.clbFilterCountries_Movies.TabStop = false
        '
        'pnlFilterCountriesTop_Movies
        '
        Me.pnlFilterCountriesTop_Movies.AutoSize = true
        Me.pnlFilterCountriesTop_Movies.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.pnlFilterCountriesTop_Movies.Controls.Add(Me.tblFilterCountriesTop_Movies)
        Me.pnlFilterCountriesTop_Movies.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterCountriesTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterCountriesTop_Movies.Name = "pnlFilterCountriesTop_Movies"
        Me.pnlFilterCountriesTop_Movies.Size = New System.Drawing.Size(189, 20)
        Me.pnlFilterCountriesTop_Movies.TabIndex = 25
        '
        'tblFilterCountriesTop_Movies
        '
        Me.tblFilterCountriesTop_Movies.AutoSize = true
        Me.tblFilterCountriesTop_Movies.ColumnCount = 3
        Me.tblFilterCountriesTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterCountriesTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tblFilterCountriesTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterCountriesTop_Movies.Controls.Add(Me.lblFilterCountries_Movies, 0, 0)
        Me.tblFilterCountriesTop_Movies.Controls.Add(Me.lblFilterCountriesClose_Movies, 2, 0)
        Me.tblFilterCountriesTop_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterCountriesTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterCountriesTop_Movies.Name = "tblFilterCountriesTop_Movies"
        Me.tblFilterCountriesTop_Movies.RowCount = 2
        Me.tblFilterCountriesTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterCountriesTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterCountriesTop_Movies.Size = New System.Drawing.Size(189, 20)
        Me.tblFilterCountriesTop_Movies.TabIndex = 0
        '
        'lblFilterCountries_Movies
        '
        Me.lblFilterCountries_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterCountries_Movies.AutoSize = true
        Me.lblFilterCountries_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterCountries_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterCountries_Movies.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilterCountries_Movies.Location = New System.Drawing.Point(3, 3)
        Me.lblFilterCountries_Movies.Name = "lblFilterCountries_Movies"
        Me.lblFilterCountries_Movies.Size = New System.Drawing.Size(57, 13)
        Me.lblFilterCountries_Movies.TabIndex = 23
        Me.lblFilterCountries_Movies.Text = "Countries"
        Me.lblFilterCountries_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterCountriesClose_Movies
        '
        Me.lblFilterCountriesClose_Movies.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblFilterCountriesClose_Movies.AutoSize = true
        Me.lblFilterCountriesClose_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterCountriesClose_Movies.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterCountriesClose_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterCountriesClose_Movies.ForeColor = System.Drawing.Color.White
        Me.lblFilterCountriesClose_Movies.Location = New System.Drawing.Point(151, 3)
        Me.lblFilterCountriesClose_Movies.Name = "lblFilterCountriesClose_Movies"
        Me.lblFilterCountriesClose_Movies.Size = New System.Drawing.Size(35, 13)
        Me.lblFilterCountriesClose_Movies.TabIndex = 24
        Me.lblFilterCountriesClose_Movies.Text = "Close"
        '
        'pnlFilterGenres_Movies
        '
        Me.pnlFilterGenres_Movies.BackColor = System.Drawing.Color.Transparent
        Me.pnlFilterGenres_Movies.Controls.Add(Me.pnlFilterGenresMain_Movies)
        Me.pnlFilterGenres_Movies.Controls.Add(Me.pnlFilterGenresTop_Movies)
        Me.pnlFilterGenres_Movies.Location = New System.Drawing.Point(0, 900)
        Me.pnlFilterGenres_Movies.Name = "pnlFilterGenres_Movies"
        Me.pnlFilterGenres_Movies.Size = New System.Drawing.Size(189, 192)
        Me.pnlFilterGenres_Movies.TabIndex = 15
        Me.pnlFilterGenres_Movies.Visible = false
        '
        'pnlFilterGenresMain_Movies
        '
        Me.pnlFilterGenresMain_Movies.AutoSize = true
        Me.pnlFilterGenresMain_Movies.BackColor = System.Drawing.Color.Transparent
        Me.pnlFilterGenresMain_Movies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilterGenresMain_Movies.Controls.Add(Me.clbFilterGenres_Movies)
        Me.pnlFilterGenresMain_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFilterGenresMain_Movies.Location = New System.Drawing.Point(0, 20)
        Me.pnlFilterGenresMain_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlFilterGenresMain_Movies.Name = "pnlFilterGenresMain_Movies"
        Me.pnlFilterGenresMain_Movies.Size = New System.Drawing.Size(189, 172)
        Me.pnlFilterGenresMain_Movies.TabIndex = 26
        '
        'clbFilterGenres_Movies
        '
        Me.clbFilterGenres_Movies.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.clbFilterGenres_Movies.CheckOnClick = true
        Me.clbFilterGenres_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbFilterGenres_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.clbFilterGenres_Movies.FormattingEnabled = true
        Me.clbFilterGenres_Movies.Location = New System.Drawing.Point(0, 0)
        Me.clbFilterGenres_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.clbFilterGenres_Movies.Name = "clbFilterGenres_Movies"
        Me.clbFilterGenres_Movies.Size = New System.Drawing.Size(187, 170)
        Me.clbFilterGenres_Movies.TabIndex = 8
        Me.clbFilterGenres_Movies.TabStop = false
        '
        'pnlFilterGenresTop_Movies
        '
        Me.pnlFilterGenresTop_Movies.AutoSize = true
        Me.pnlFilterGenresTop_Movies.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.pnlFilterGenresTop_Movies.Controls.Add(Me.tblFilterGenresTop_Movies)
        Me.pnlFilterGenresTop_Movies.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterGenresTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterGenresTop_Movies.Name = "pnlFilterGenresTop_Movies"
        Me.pnlFilterGenresTop_Movies.Size = New System.Drawing.Size(189, 20)
        Me.pnlFilterGenresTop_Movies.TabIndex = 25
        '
        'tblFilterGenresTop_Movies
        '
        Me.tblFilterGenresTop_Movies.AutoSize = true
        Me.tblFilterGenresTop_Movies.ColumnCount = 3
        Me.tblFilterGenresTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGenresTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tblFilterGenresTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGenresTop_Movies.Controls.Add(Me.lblFilterGenresClose_Movies, 2, 0)
        Me.tblFilterGenresTop_Movies.Controls.Add(Me.lblFilterGenres_Movies, 0, 0)
        Me.tblFilterGenresTop_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterGenresTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterGenresTop_Movies.Name = "tblFilterGenresTop_Movies"
        Me.tblFilterGenresTop_Movies.RowCount = 2
        Me.tblFilterGenresTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterGenresTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGenresTop_Movies.Size = New System.Drawing.Size(189, 20)
        Me.tblFilterGenresTop_Movies.TabIndex = 0
        '
        'lblFilterGenresClose_Movies
        '
        Me.lblFilterGenresClose_Movies.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblFilterGenresClose_Movies.AutoSize = true
        Me.lblFilterGenresClose_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterGenresClose_Movies.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterGenresClose_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterGenresClose_Movies.ForeColor = System.Drawing.Color.White
        Me.lblFilterGenresClose_Movies.Location = New System.Drawing.Point(151, 3)
        Me.lblFilterGenresClose_Movies.Name = "lblFilterGenresClose_Movies"
        Me.lblFilterGenresClose_Movies.Size = New System.Drawing.Size(35, 13)
        Me.lblFilterGenresClose_Movies.TabIndex = 24
        Me.lblFilterGenresClose_Movies.Text = "Close"
        '
        'lblFilterGenres_Movies
        '
        Me.lblFilterGenres_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterGenres_Movies.AutoSize = true
        Me.lblFilterGenres_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterGenres_Movies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFilterGenres_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterGenres_Movies.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilterGenres_Movies.Location = New System.Drawing.Point(3, 2)
        Me.lblFilterGenres_Movies.Name = "lblFilterGenres_Movies"
        Me.lblFilterGenres_Movies.Size = New System.Drawing.Size(45, 15)
        Me.lblFilterGenres_Movies.TabIndex = 23
        Me.lblFilterGenres_Movies.Text = "Genres"
        Me.lblFilterGenres_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlFilterGenres_Shows
        '
        Me.pnlFilterGenres_Shows.BackColor = System.Drawing.Color.Transparent
        Me.pnlFilterGenres_Shows.Controls.Add(Me.pnlFilterGenresMain_Shows)
        Me.pnlFilterGenres_Shows.Controls.Add(Me.pnlFilterGenresTop_Shows)
        Me.pnlFilterGenres_Shows.Location = New System.Drawing.Point(0, 900)
        Me.pnlFilterGenres_Shows.Name = "pnlFilterGenres_Shows"
        Me.pnlFilterGenres_Shows.Size = New System.Drawing.Size(189, 192)
        Me.pnlFilterGenres_Shows.TabIndex = 16
        Me.pnlFilterGenres_Shows.Visible = false
        '
        'pnlFilterGenresMain_Shows
        '
        Me.pnlFilterGenresMain_Shows.AutoSize = true
        Me.pnlFilterGenresMain_Shows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilterGenresMain_Shows.Controls.Add(Me.clbFilterGenres_Shows)
        Me.pnlFilterGenresMain_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFilterGenresMain_Shows.Location = New System.Drawing.Point(0, 20)
        Me.pnlFilterGenresMain_Shows.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlFilterGenresMain_Shows.Name = "pnlFilterGenresMain_Shows"
        Me.pnlFilterGenresMain_Shows.Size = New System.Drawing.Size(189, 172)
        Me.pnlFilterGenresMain_Shows.TabIndex = 26
        '
        'clbFilterGenres_Shows
        '
        Me.clbFilterGenres_Shows.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.clbFilterGenres_Shows.CheckOnClick = true
        Me.clbFilterGenres_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbFilterGenres_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.clbFilterGenres_Shows.FormattingEnabled = true
        Me.clbFilterGenres_Shows.Location = New System.Drawing.Point(0, 0)
        Me.clbFilterGenres_Shows.Margin = New System.Windows.Forms.Padding(0)
        Me.clbFilterGenres_Shows.Name = "clbFilterGenres_Shows"
        Me.clbFilterGenres_Shows.Size = New System.Drawing.Size(187, 170)
        Me.clbFilterGenres_Shows.TabIndex = 8
        Me.clbFilterGenres_Shows.TabStop = false
        '
        'pnlFilterGenresTop_Shows
        '
        Me.pnlFilterGenresTop_Shows.AutoSize = true
        Me.pnlFilterGenresTop_Shows.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.pnlFilterGenresTop_Shows.Controls.Add(Me.tblFilterGenresTop_Shows)
        Me.pnlFilterGenresTop_Shows.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterGenresTop_Shows.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterGenresTop_Shows.Name = "pnlFilterGenresTop_Shows"
        Me.pnlFilterGenresTop_Shows.Size = New System.Drawing.Size(189, 20)
        Me.pnlFilterGenresTop_Shows.TabIndex = 25
        '
        'tblFilterGenresTop_Shows
        '
        Me.tblFilterGenresTop_Shows.AutoSize = true
        Me.tblFilterGenresTop_Shows.ColumnCount = 3
        Me.tblFilterGenresTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGenresTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tblFilterGenresTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGenresTop_Shows.Controls.Add(Me.lblFilterGenres_Shows, 0, 0)
        Me.tblFilterGenresTop_Shows.Controls.Add(Me.lblFilterGenresClose_Shows, 2, 0)
        Me.tblFilterGenresTop_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterGenresTop_Shows.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterGenresTop_Shows.Name = "tblFilterGenresTop_Shows"
        Me.tblFilterGenresTop_Shows.RowCount = 2
        Me.tblFilterGenresTop_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterGenresTop_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGenresTop_Shows.Size = New System.Drawing.Size(189, 20)
        Me.tblFilterGenresTop_Shows.TabIndex = 0
        '
        'lblFilterGenres_Shows
        '
        Me.lblFilterGenres_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterGenres_Shows.AutoSize = true
        Me.lblFilterGenres_Shows.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterGenres_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterGenres_Shows.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilterGenres_Shows.Location = New System.Drawing.Point(3, 3)
        Me.lblFilterGenres_Shows.Name = "lblFilterGenres_Shows"
        Me.lblFilterGenres_Shows.Size = New System.Drawing.Size(43, 13)
        Me.lblFilterGenres_Shows.TabIndex = 23
        Me.lblFilterGenres_Shows.Text = "Genres"
        Me.lblFilterGenres_Shows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterGenresClose_Shows
        '
        Me.lblFilterGenresClose_Shows.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblFilterGenresClose_Shows.AutoSize = true
        Me.lblFilterGenresClose_Shows.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterGenresClose_Shows.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterGenresClose_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterGenresClose_Shows.ForeColor = System.Drawing.Color.White
        Me.lblFilterGenresClose_Shows.Location = New System.Drawing.Point(151, 3)
        Me.lblFilterGenresClose_Shows.Name = "lblFilterGenresClose_Shows"
        Me.lblFilterGenresClose_Shows.Size = New System.Drawing.Size(35, 13)
        Me.lblFilterGenresClose_Shows.TabIndex = 24
        Me.lblFilterGenresClose_Shows.Text = "Close"
        '
        'pnlFilterDataFields_Movies
        '
        Me.pnlFilterDataFields_Movies.BackColor = System.Drawing.Color.Transparent
        Me.pnlFilterDataFields_Movies.Controls.Add(Me.pnlFilterDataFieldsMain_Movies)
        Me.pnlFilterDataFields_Movies.Controls.Add(Me.pnlFilterDataFieldsTop_Movies)
        Me.pnlFilterDataFields_Movies.Location = New System.Drawing.Point(0, 900)
        Me.pnlFilterDataFields_Movies.Name = "pnlFilterDataFields_Movies"
        Me.pnlFilterDataFields_Movies.Size = New System.Drawing.Size(189, 192)
        Me.pnlFilterDataFields_Movies.TabIndex = 26
        Me.pnlFilterDataFields_Movies.Visible = false
        '
        'pnlFilterDataFieldsMain_Movies
        '
        Me.pnlFilterDataFieldsMain_Movies.AutoSize = true
        Me.pnlFilterDataFieldsMain_Movies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilterDataFieldsMain_Movies.Controls.Add(Me.clbFilterDataFields_Movies)
        Me.pnlFilterDataFieldsMain_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFilterDataFieldsMain_Movies.Location = New System.Drawing.Point(0, 20)
        Me.pnlFilterDataFieldsMain_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlFilterDataFieldsMain_Movies.Name = "pnlFilterDataFieldsMain_Movies"
        Me.pnlFilterDataFieldsMain_Movies.Size = New System.Drawing.Size(189, 172)
        Me.pnlFilterDataFieldsMain_Movies.TabIndex = 26
        '
        'clbFilterDataFields_Movies
        '
        Me.clbFilterDataFields_Movies.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.clbFilterDataFields_Movies.CheckOnClick = true
        Me.clbFilterDataFields_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbFilterDataFields_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.clbFilterDataFields_Movies.FormattingEnabled = true
        Me.clbFilterDataFields_Movies.Location = New System.Drawing.Point(0, 0)
        Me.clbFilterDataFields_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.clbFilterDataFields_Movies.Name = "clbFilterDataFields_Movies"
        Me.clbFilterDataFields_Movies.Size = New System.Drawing.Size(187, 170)
        Me.clbFilterDataFields_Movies.TabIndex = 8
        Me.clbFilterDataFields_Movies.TabStop = false
        '
        'pnlFilterDataFieldsTop_Movies
        '
        Me.pnlFilterDataFieldsTop_Movies.AutoSize = true
        Me.pnlFilterDataFieldsTop_Movies.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.pnlFilterDataFieldsTop_Movies.Controls.Add(Me.tblFilterDataFieldsTop_Movies)
        Me.pnlFilterDataFieldsTop_Movies.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterDataFieldsTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterDataFieldsTop_Movies.Name = "pnlFilterDataFieldsTop_Movies"
        Me.pnlFilterDataFieldsTop_Movies.Size = New System.Drawing.Size(189, 20)
        Me.pnlFilterDataFieldsTop_Movies.TabIndex = 25
        '
        'tblFilterDataFieldsTop_Movies
        '
        Me.tblFilterDataFieldsTop_Movies.AutoSize = true
        Me.tblFilterDataFieldsTop_Movies.ColumnCount = 3
        Me.tblFilterDataFieldsTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterDataFieldsTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tblFilterDataFieldsTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterDataFieldsTop_Movies.Controls.Add(Me.lblFilterDataFields_Movies, 0, 0)
        Me.tblFilterDataFieldsTop_Movies.Controls.Add(Me.lblFilterDataFieldsClose_Movies, 2, 0)
        Me.tblFilterDataFieldsTop_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterDataFieldsTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterDataFieldsTop_Movies.Name = "tblFilterDataFieldsTop_Movies"
        Me.tblFilterDataFieldsTop_Movies.RowCount = 2
        Me.tblFilterDataFieldsTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterDataFieldsTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterDataFieldsTop_Movies.Size = New System.Drawing.Size(189, 20)
        Me.tblFilterDataFieldsTop_Movies.TabIndex = 26
        '
        'lblFilterDataFields_Movies
        '
        Me.lblFilterDataFields_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterDataFields_Movies.AutoSize = true
        Me.lblFilterDataFields_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterDataFields_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterDataFields_Movies.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilterDataFields_Movies.Location = New System.Drawing.Point(3, 3)
        Me.lblFilterDataFields_Movies.Name = "lblFilterDataFields_Movies"
        Me.lblFilterDataFields_Movies.Size = New System.Drawing.Size(64, 13)
        Me.lblFilterDataFields_Movies.TabIndex = 23
        Me.lblFilterDataFields_Movies.Text = "Data Fields"
        Me.lblFilterDataFields_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterDataFieldsClose_Movies
        '
        Me.lblFilterDataFieldsClose_Movies.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblFilterDataFieldsClose_Movies.AutoSize = true
        Me.lblFilterDataFieldsClose_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterDataFieldsClose_Movies.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterDataFieldsClose_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterDataFieldsClose_Movies.ForeColor = System.Drawing.Color.White
        Me.lblFilterDataFieldsClose_Movies.Location = New System.Drawing.Point(151, 3)
        Me.lblFilterDataFieldsClose_Movies.Name = "lblFilterDataFieldsClose_Movies"
        Me.lblFilterDataFieldsClose_Movies.Size = New System.Drawing.Size(35, 13)
        Me.lblFilterDataFieldsClose_Movies.TabIndex = 24
        Me.lblFilterDataFieldsClose_Movies.Text = "Close"
        '
        'pnlFilterMissingItems_Movies
        '
        Me.pnlFilterMissingItems_Movies.AutoSize = true
        Me.pnlFilterMissingItems_Movies.Controls.Add(Me.pnlFilterMissingItemsMain_Movies)
        Me.pnlFilterMissingItems_Movies.Controls.Add(Me.pnlFilterMissingItemsTop_Movies)
        Me.pnlFilterMissingItems_Movies.Location = New System.Drawing.Point(0, 900)
        Me.pnlFilterMissingItems_Movies.Name = "pnlFilterMissingItems_Movies"
        Me.pnlFilterMissingItems_Movies.Size = New System.Drawing.Size(170, 321)
        Me.pnlFilterMissingItems_Movies.TabIndex = 27
        Me.pnlFilterMissingItems_Movies.Visible = false
        '
        'pnlFilterMissingItemsMain_Movies
        '
        Me.pnlFilterMissingItemsMain_Movies.AutoSize = true
        Me.pnlFilterMissingItemsMain_Movies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilterMissingItemsMain_Movies.Controls.Add(Me.tblFilterMissingItemsMain_Movies)
        Me.pnlFilterMissingItemsMain_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFilterMissingItemsMain_Movies.Location = New System.Drawing.Point(0, 20)
        Me.pnlFilterMissingItemsMain_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlFilterMissingItemsMain_Movies.Name = "pnlFilterMissingItemsMain_Movies"
        Me.pnlFilterMissingItemsMain_Movies.Size = New System.Drawing.Size(170, 301)
        Me.pnlFilterMissingItemsMain_Movies.TabIndex = 26
        '
        'tblFilterMissingItemsMain_Movies
        '
        Me.tblFilterMissingItemsMain_Movies.AutoSize = true
        Me.tblFilterMissingItemsMain_Movies.ColumnCount = 2
        Me.tblFilterMissingItemsMain_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsMain_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingBanner, 0, 0)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingClearArt, 0, 1)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingClearLogo, 0, 2)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingDiscArt, 0, 3)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingEFanarts, 0, 4)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingEThumbs, 0, 5)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingFanart, 0, 6)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingLandscape, 0, 7)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingTrailer, 0, 12)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingTheme, 0, 11)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingSubtitles, 0, 10)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingPoster, 0, 9)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingNFO, 0, 8)
        Me.tblFilterMissingItemsMain_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterMissingItemsMain_Movies.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterMissingItemsMain_Movies.Name = "tblFilterMissingItemsMain_Movies"
        Me.tblFilterMissingItemsMain_Movies.RowCount = 14
        Me.tblFilterMissingItemsMain_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Movies.Size = New System.Drawing.Size(168, 299)
        Me.tblFilterMissingItemsMain_Movies.TabIndex = 0
        '
        'chkMovieMissingBanner
        '
        Me.chkMovieMissingBanner.AutoSize = true
        Me.chkMovieMissingBanner.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieMissingBanner.Name = "chkMovieMissingBanner"
        Me.chkMovieMissingBanner.Size = New System.Drawing.Size(63, 17)
        Me.chkMovieMissingBanner.TabIndex = 0
        Me.chkMovieMissingBanner.Text = "Banner"
        Me.chkMovieMissingBanner.UseVisualStyleBackColor = true
        '
        'chkMovieMissingClearArt
        '
        Me.chkMovieMissingClearArt.AutoSize = true
        Me.chkMovieMissingClearArt.Location = New System.Drawing.Point(3, 26)
        Me.chkMovieMissingClearArt.Name = "chkMovieMissingClearArt"
        Me.chkMovieMissingClearArt.Size = New System.Drawing.Size(67, 17)
        Me.chkMovieMissingClearArt.TabIndex = 0
        Me.chkMovieMissingClearArt.Text = "ClearArt"
        Me.chkMovieMissingClearArt.UseVisualStyleBackColor = true
        '
        'chkMovieMissingClearLogo
        '
        Me.chkMovieMissingClearLogo.AutoSize = true
        Me.chkMovieMissingClearLogo.Location = New System.Drawing.Point(3, 49)
        Me.chkMovieMissingClearLogo.Name = "chkMovieMissingClearLogo"
        Me.chkMovieMissingClearLogo.Size = New System.Drawing.Size(78, 17)
        Me.chkMovieMissingClearLogo.TabIndex = 0
        Me.chkMovieMissingClearLogo.Text = "ClearLogo"
        Me.chkMovieMissingClearLogo.UseVisualStyleBackColor = true
        '
        'chkMovieMissingDiscArt
        '
        Me.chkMovieMissingDiscArt.AutoSize = true
        Me.chkMovieMissingDiscArt.Location = New System.Drawing.Point(3, 72)
        Me.chkMovieMissingDiscArt.Name = "chkMovieMissingDiscArt"
        Me.chkMovieMissingDiscArt.Size = New System.Drawing.Size(62, 17)
        Me.chkMovieMissingDiscArt.TabIndex = 0
        Me.chkMovieMissingDiscArt.Text = "DiscArt"
        Me.chkMovieMissingDiscArt.UseVisualStyleBackColor = true
        '
        'chkMovieMissingEFanarts
        '
        Me.chkMovieMissingEFanarts.AutoSize = true
        Me.chkMovieMissingEFanarts.Location = New System.Drawing.Point(3, 95)
        Me.chkMovieMissingEFanarts.Name = "chkMovieMissingEFanarts"
        Me.chkMovieMissingEFanarts.Size = New System.Drawing.Size(87, 17)
        Me.chkMovieMissingEFanarts.TabIndex = 0
        Me.chkMovieMissingEFanarts.Text = "Extrafanarts"
        Me.chkMovieMissingEFanarts.UseVisualStyleBackColor = true
        '
        'chkMovieMissingEThumbs
        '
        Me.chkMovieMissingEThumbs.AutoSize = true
        Me.chkMovieMissingEThumbs.Location = New System.Drawing.Point(3, 118)
        Me.chkMovieMissingEThumbs.Name = "chkMovieMissingEThumbs"
        Me.chkMovieMissingEThumbs.Size = New System.Drawing.Size(90, 17)
        Me.chkMovieMissingEThumbs.TabIndex = 0
        Me.chkMovieMissingEThumbs.Text = "Extrathumbs"
        Me.chkMovieMissingEThumbs.UseVisualStyleBackColor = true
        '
        'chkMovieMissingFanart
        '
        Me.chkMovieMissingFanart.AutoSize = true
        Me.chkMovieMissingFanart.Location = New System.Drawing.Point(3, 141)
        Me.chkMovieMissingFanart.Name = "chkMovieMissingFanart"
        Me.chkMovieMissingFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieMissingFanart.TabIndex = 0
        Me.chkMovieMissingFanart.Text = "Fanart"
        Me.chkMovieMissingFanart.UseVisualStyleBackColor = true
        '
        'chkMovieMissingLandscape
        '
        Me.chkMovieMissingLandscape.AutoSize = true
        Me.chkMovieMissingLandscape.Location = New System.Drawing.Point(3, 164)
        Me.chkMovieMissingLandscape.Name = "chkMovieMissingLandscape"
        Me.chkMovieMissingLandscape.Size = New System.Drawing.Size(80, 17)
        Me.chkMovieMissingLandscape.TabIndex = 0
        Me.chkMovieMissingLandscape.Text = "Landscape"
        Me.chkMovieMissingLandscape.UseVisualStyleBackColor = true
        '
        'chkMovieMissingTrailer
        '
        Me.chkMovieMissingTrailer.AutoSize = true
        Me.chkMovieMissingTrailer.Location = New System.Drawing.Point(3, 279)
        Me.chkMovieMissingTrailer.Name = "chkMovieMissingTrailer"
        Me.chkMovieMissingTrailer.Size = New System.Drawing.Size(57, 17)
        Me.chkMovieMissingTrailer.TabIndex = 0
        Me.chkMovieMissingTrailer.Text = "Trailer"
        Me.chkMovieMissingTrailer.UseVisualStyleBackColor = true
        '
        'chkMovieMissingTheme
        '
        Me.chkMovieMissingTheme.AutoSize = true
        Me.chkMovieMissingTheme.Location = New System.Drawing.Point(3, 256)
        Me.chkMovieMissingTheme.Name = "chkMovieMissingTheme"
        Me.chkMovieMissingTheme.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieMissingTheme.TabIndex = 0
        Me.chkMovieMissingTheme.Text = "Theme"
        Me.chkMovieMissingTheme.UseVisualStyleBackColor = true
        '
        'chkMovieMissingSubtitles
        '
        Me.chkMovieMissingSubtitles.AutoSize = true
        Me.chkMovieMissingSubtitles.Location = New System.Drawing.Point(3, 233)
        Me.chkMovieMissingSubtitles.Name = "chkMovieMissingSubtitles"
        Me.chkMovieMissingSubtitles.Size = New System.Drawing.Size(66, 17)
        Me.chkMovieMissingSubtitles.TabIndex = 0
        Me.chkMovieMissingSubtitles.Text = "Subtitle"
        Me.chkMovieMissingSubtitles.UseVisualStyleBackColor = true
        '
        'chkMovieMissingPoster
        '
        Me.chkMovieMissingPoster.AutoSize = true
        Me.chkMovieMissingPoster.Location = New System.Drawing.Point(3, 210)
        Me.chkMovieMissingPoster.Name = "chkMovieMissingPoster"
        Me.chkMovieMissingPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkMovieMissingPoster.TabIndex = 0
        Me.chkMovieMissingPoster.Text = "Poster"
        Me.chkMovieMissingPoster.UseVisualStyleBackColor = true
        '
        'chkMovieMissingNFO
        '
        Me.chkMovieMissingNFO.AutoSize = true
        Me.chkMovieMissingNFO.Location = New System.Drawing.Point(3, 187)
        Me.chkMovieMissingNFO.Name = "chkMovieMissingNFO"
        Me.chkMovieMissingNFO.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieMissingNFO.TabIndex = 0
        Me.chkMovieMissingNFO.Text = "NFO"
        Me.chkMovieMissingNFO.UseVisualStyleBackColor = true
        '
        'pnlFilterMissingItemsTop_Movies
        '
        Me.pnlFilterMissingItemsTop_Movies.AutoSize = true
        Me.pnlFilterMissingItemsTop_Movies.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.pnlFilterMissingItemsTop_Movies.Controls.Add(Me.tblFilterMissingItemsTop_Movies)
        Me.pnlFilterMissingItemsTop_Movies.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterMissingItemsTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterMissingItemsTop_Movies.Name = "pnlFilterMissingItemsTop_Movies"
        Me.pnlFilterMissingItemsTop_Movies.Size = New System.Drawing.Size(170, 20)
        Me.pnlFilterMissingItemsTop_Movies.TabIndex = 25
        '
        'tblFilterMissingItemsTop_Movies
        '
        Me.tblFilterMissingItemsTop_Movies.AutoSize = true
        Me.tblFilterMissingItemsTop_Movies.ColumnCount = 3
        Me.tblFilterMissingItemsTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tblFilterMissingItemsTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsTop_Movies.Controls.Add(Me.lblFilterMissingItems_Movies, 0, 0)
        Me.tblFilterMissingItemsTop_Movies.Controls.Add(Me.lblFilterMissingItemsClose_Movies, 2, 0)
        Me.tblFilterMissingItemsTop_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterMissingItemsTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterMissingItemsTop_Movies.Name = "tblFilterMissingItemsTop_Movies"
        Me.tblFilterMissingItemsTop_Movies.RowCount = 2
        Me.tblFilterMissingItemsTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterMissingItemsTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsTop_Movies.Size = New System.Drawing.Size(170, 20)
        Me.tblFilterMissingItemsTop_Movies.TabIndex = 0
        '
        'lblFilterMissingItems_Movies
        '
        Me.lblFilterMissingItems_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterMissingItems_Movies.AutoSize = true
        Me.lblFilterMissingItems_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterMissingItems_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterMissingItems_Movies.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilterMissingItems_Movies.Location = New System.Drawing.Point(3, 3)
        Me.lblFilterMissingItems_Movies.Name = "lblFilterMissingItems_Movies"
        Me.lblFilterMissingItems_Movies.Size = New System.Drawing.Size(79, 13)
        Me.lblFilterMissingItems_Movies.TabIndex = 23
        Me.lblFilterMissingItems_Movies.Text = "Missing Items"
        Me.lblFilterMissingItems_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterMissingItemsClose_Movies
        '
        Me.lblFilterMissingItemsClose_Movies.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblFilterMissingItemsClose_Movies.AutoSize = true
        Me.lblFilterMissingItemsClose_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterMissingItemsClose_Movies.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterMissingItemsClose_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterMissingItemsClose_Movies.ForeColor = System.Drawing.Color.White
        Me.lblFilterMissingItemsClose_Movies.Location = New System.Drawing.Point(132, 3)
        Me.lblFilterMissingItemsClose_Movies.Name = "lblFilterMissingItemsClose_Movies"
        Me.lblFilterMissingItemsClose_Movies.Size = New System.Drawing.Size(35, 13)
        Me.lblFilterMissingItemsClose_Movies.TabIndex = 24
        Me.lblFilterMissingItemsClose_Movies.Text = "Close"
        '
        'pnlFilterMissingItems_MovieSets
        '
        Me.pnlFilterMissingItems_MovieSets.AutoSize = true
        Me.pnlFilterMissingItems_MovieSets.Controls.Add(Me.pnlFilterMissingItemsMain_MovieSets)
        Me.pnlFilterMissingItems_MovieSets.Controls.Add(Me.pnlFilterMissingItemsTop_MovieSets)
        Me.pnlFilterMissingItems_MovieSets.Location = New System.Drawing.Point(0, 900)
        Me.pnlFilterMissingItems_MovieSets.Name = "pnlFilterMissingItems_MovieSets"
        Me.pnlFilterMissingItems_MovieSets.Size = New System.Drawing.Size(170, 206)
        Me.pnlFilterMissingItems_MovieSets.TabIndex = 28
        Me.pnlFilterMissingItems_MovieSets.Visible = false
        '
        'pnlFilterMissingItemsMain_MovieSets
        '
        Me.pnlFilterMissingItemsMain_MovieSets.AutoSize = true
        Me.pnlFilterMissingItemsMain_MovieSets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilterMissingItemsMain_MovieSets.Controls.Add(Me.tlbFilterMissingItemsMain_MovieSets)
        Me.pnlFilterMissingItemsMain_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFilterMissingItemsMain_MovieSets.Location = New System.Drawing.Point(0, 20)
        Me.pnlFilterMissingItemsMain_MovieSets.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlFilterMissingItemsMain_MovieSets.Name = "pnlFilterMissingItemsMain_MovieSets"
        Me.pnlFilterMissingItemsMain_MovieSets.Size = New System.Drawing.Size(170, 186)
        Me.pnlFilterMissingItemsMain_MovieSets.TabIndex = 26
        '
        'tlbFilterMissingItemsMain_MovieSets
        '
        Me.tlbFilterMissingItemsMain_MovieSets.AutoSize = true
        Me.tlbFilterMissingItemsMain_MovieSets.ColumnCount = 2
        Me.tlbFilterMissingItemsMain_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlbFilterMissingItemsMain_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlbFilterMissingItemsMain_MovieSets.Controls.Add(Me.chkMovieSetMissingBanner, 0, 0)
        Me.tlbFilterMissingItemsMain_MovieSets.Controls.Add(Me.chkMovieSetMissingClearArt, 0, 1)
        Me.tlbFilterMissingItemsMain_MovieSets.Controls.Add(Me.chkMovieSetMissingClearLogo, 0, 2)
        Me.tlbFilterMissingItemsMain_MovieSets.Controls.Add(Me.chkMovieSetMissingDiscArt, 0, 3)
        Me.tlbFilterMissingItemsMain_MovieSets.Controls.Add(Me.chkMovieSetMissingFanart, 0, 4)
        Me.tlbFilterMissingItemsMain_MovieSets.Controls.Add(Me.chkMovieSetMissingLandscape, 0, 5)
        Me.tlbFilterMissingItemsMain_MovieSets.Controls.Add(Me.chkMovieSetMissingPoster, 0, 7)
        Me.tlbFilterMissingItemsMain_MovieSets.Controls.Add(Me.chkMovieSetMissingNFO, 0, 6)
        Me.tlbFilterMissingItemsMain_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlbFilterMissingItemsMain_MovieSets.Location = New System.Drawing.Point(0, 0)
        Me.tlbFilterMissingItemsMain_MovieSets.Name = "tlbFilterMissingItemsMain_MovieSets"
        Me.tlbFilterMissingItemsMain_MovieSets.RowCount = 9
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tlbFilterMissingItemsMain_MovieSets.Size = New System.Drawing.Size(168, 184)
        Me.tlbFilterMissingItemsMain_MovieSets.TabIndex = 0
        '
        'chkMovieSetMissingBanner
        '
        Me.chkMovieSetMissingBanner.AutoSize = true
        Me.chkMovieSetMissingBanner.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieSetMissingBanner.Name = "chkMovieSetMissingBanner"
        Me.chkMovieSetMissingBanner.Size = New System.Drawing.Size(63, 17)
        Me.chkMovieSetMissingBanner.TabIndex = 0
        Me.chkMovieSetMissingBanner.Text = "Banner"
        Me.chkMovieSetMissingBanner.UseVisualStyleBackColor = true
        '
        'chkMovieSetMissingClearArt
        '
        Me.chkMovieSetMissingClearArt.AutoSize = true
        Me.chkMovieSetMissingClearArt.Location = New System.Drawing.Point(3, 26)
        Me.chkMovieSetMissingClearArt.Name = "chkMovieSetMissingClearArt"
        Me.chkMovieSetMissingClearArt.Size = New System.Drawing.Size(67, 17)
        Me.chkMovieSetMissingClearArt.TabIndex = 0
        Me.chkMovieSetMissingClearArt.Text = "ClearArt"
        Me.chkMovieSetMissingClearArt.UseVisualStyleBackColor = true
        '
        'chkMovieSetMissingClearLogo
        '
        Me.chkMovieSetMissingClearLogo.AutoSize = true
        Me.chkMovieSetMissingClearLogo.Location = New System.Drawing.Point(3, 49)
        Me.chkMovieSetMissingClearLogo.Name = "chkMovieSetMissingClearLogo"
        Me.chkMovieSetMissingClearLogo.Size = New System.Drawing.Size(78, 17)
        Me.chkMovieSetMissingClearLogo.TabIndex = 0
        Me.chkMovieSetMissingClearLogo.Text = "ClearLogo"
        Me.chkMovieSetMissingClearLogo.UseVisualStyleBackColor = true
        '
        'chkMovieSetMissingDiscArt
        '
        Me.chkMovieSetMissingDiscArt.AutoSize = true
        Me.chkMovieSetMissingDiscArt.Location = New System.Drawing.Point(3, 72)
        Me.chkMovieSetMissingDiscArt.Name = "chkMovieSetMissingDiscArt"
        Me.chkMovieSetMissingDiscArt.Size = New System.Drawing.Size(62, 17)
        Me.chkMovieSetMissingDiscArt.TabIndex = 0
        Me.chkMovieSetMissingDiscArt.Text = "DiscArt"
        Me.chkMovieSetMissingDiscArt.UseVisualStyleBackColor = true
        '
        'chkMovieSetMissingFanart
        '
        Me.chkMovieSetMissingFanart.AutoSize = true
        Me.chkMovieSetMissingFanart.Location = New System.Drawing.Point(3, 95)
        Me.chkMovieSetMissingFanart.Name = "chkMovieSetMissingFanart"
        Me.chkMovieSetMissingFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieSetMissingFanart.TabIndex = 0
        Me.chkMovieSetMissingFanart.Text = "Fanart"
        Me.chkMovieSetMissingFanart.UseVisualStyleBackColor = true
        '
        'chkMovieSetMissingLandscape
        '
        Me.chkMovieSetMissingLandscape.AutoSize = true
        Me.chkMovieSetMissingLandscape.Location = New System.Drawing.Point(3, 118)
        Me.chkMovieSetMissingLandscape.Name = "chkMovieSetMissingLandscape"
        Me.chkMovieSetMissingLandscape.Size = New System.Drawing.Size(80, 17)
        Me.chkMovieSetMissingLandscape.TabIndex = 0
        Me.chkMovieSetMissingLandscape.Text = "Landscape"
        Me.chkMovieSetMissingLandscape.UseVisualStyleBackColor = true
        '
        'chkMovieSetMissingPoster
        '
        Me.chkMovieSetMissingPoster.AutoSize = true
        Me.chkMovieSetMissingPoster.Location = New System.Drawing.Point(3, 164)
        Me.chkMovieSetMissingPoster.Name = "chkMovieSetMissingPoster"
        Me.chkMovieSetMissingPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkMovieSetMissingPoster.TabIndex = 0
        Me.chkMovieSetMissingPoster.Text = "Poster"
        Me.chkMovieSetMissingPoster.UseVisualStyleBackColor = true
        '
        'chkMovieSetMissingNFO
        '
        Me.chkMovieSetMissingNFO.AutoSize = true
        Me.chkMovieSetMissingNFO.Location = New System.Drawing.Point(3, 141)
        Me.chkMovieSetMissingNFO.Name = "chkMovieSetMissingNFO"
        Me.chkMovieSetMissingNFO.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieSetMissingNFO.TabIndex = 0
        Me.chkMovieSetMissingNFO.Text = "NFO"
        Me.chkMovieSetMissingNFO.UseVisualStyleBackColor = true
        '
        'pnlFilterMissingItemsTop_MovieSets
        '
        Me.pnlFilterMissingItemsTop_MovieSets.AutoSize = true
        Me.pnlFilterMissingItemsTop_MovieSets.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.pnlFilterMissingItemsTop_MovieSets.Controls.Add(Me.tblFilterMissingItemsTop_MovieSets)
        Me.pnlFilterMissingItemsTop_MovieSets.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterMissingItemsTop_MovieSets.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterMissingItemsTop_MovieSets.Name = "pnlFilterMissingItemsTop_MovieSets"
        Me.pnlFilterMissingItemsTop_MovieSets.Size = New System.Drawing.Size(170, 20)
        Me.pnlFilterMissingItemsTop_MovieSets.TabIndex = 25
        '
        'tblFilterMissingItemsTop_MovieSets
        '
        Me.tblFilterMissingItemsTop_MovieSets.AutoSize = true
        Me.tblFilterMissingItemsTop_MovieSets.ColumnCount = 3
        Me.tblFilterMissingItemsTop_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsTop_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tblFilterMissingItemsTop_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsTop_MovieSets.Controls.Add(Me.lblFilterMissingItems_MovieSets, 0, 0)
        Me.tblFilterMissingItemsTop_MovieSets.Controls.Add(Me.lblFilterMissingItemsClose_MovieSets, 2, 0)
        Me.tblFilterMissingItemsTop_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterMissingItemsTop_MovieSets.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterMissingItemsTop_MovieSets.Name = "tblFilterMissingItemsTop_MovieSets"
        Me.tblFilterMissingItemsTop_MovieSets.RowCount = 2
        Me.tblFilterMissingItemsTop_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterMissingItemsTop_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsTop_MovieSets.Size = New System.Drawing.Size(170, 20)
        Me.tblFilterMissingItemsTop_MovieSets.TabIndex = 0
        '
        'lblFilterMissingItems_MovieSets
        '
        Me.lblFilterMissingItems_MovieSets.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterMissingItems_MovieSets.AutoSize = true
        Me.lblFilterMissingItems_MovieSets.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterMissingItems_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterMissingItems_MovieSets.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilterMissingItems_MovieSets.Location = New System.Drawing.Point(3, 3)
        Me.lblFilterMissingItems_MovieSets.Name = "lblFilterMissingItems_MovieSets"
        Me.lblFilterMissingItems_MovieSets.Size = New System.Drawing.Size(79, 13)
        Me.lblFilterMissingItems_MovieSets.TabIndex = 23
        Me.lblFilterMissingItems_MovieSets.Text = "Missing Items"
        Me.lblFilterMissingItems_MovieSets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterMissingItemsClose_MovieSets
        '
        Me.lblFilterMissingItemsClose_MovieSets.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblFilterMissingItemsClose_MovieSets.AutoSize = true
        Me.lblFilterMissingItemsClose_MovieSets.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterMissingItemsClose_MovieSets.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterMissingItemsClose_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterMissingItemsClose_MovieSets.ForeColor = System.Drawing.Color.White
        Me.lblFilterMissingItemsClose_MovieSets.Location = New System.Drawing.Point(132, 3)
        Me.lblFilterMissingItemsClose_MovieSets.Name = "lblFilterMissingItemsClose_MovieSets"
        Me.lblFilterMissingItemsClose_MovieSets.Size = New System.Drawing.Size(35, 13)
        Me.lblFilterMissingItemsClose_MovieSets.TabIndex = 24
        Me.lblFilterMissingItemsClose_MovieSets.Text = "Close"
        '
        'pnlFilterMissingItems_Shows
        '
        Me.pnlFilterMissingItems_Shows.AutoSize = true
        Me.pnlFilterMissingItems_Shows.Controls.Add(Me.pnlFilterMissingItemsMain_Shows)
        Me.pnlFilterMissingItems_Shows.Controls.Add(Me.pnlFilterMissingItemsTop_Shows)
        Me.pnlFilterMissingItems_Shows.Location = New System.Drawing.Point(0, 900)
        Me.pnlFilterMissingItems_Shows.Name = "pnlFilterMissingItems_Shows"
        Me.pnlFilterMissingItems_Shows.Size = New System.Drawing.Size(170, 252)
        Me.pnlFilterMissingItems_Shows.TabIndex = 29
        Me.pnlFilterMissingItems_Shows.Visible = false
        '
        'pnlFilterMissingItemsMain_Shows
        '
        Me.pnlFilterMissingItemsMain_Shows.AutoSize = true
        Me.pnlFilterMissingItemsMain_Shows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilterMissingItemsMain_Shows.Controls.Add(Me.tblFilterMissingItemsMain_Shows)
        Me.pnlFilterMissingItemsMain_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFilterMissingItemsMain_Shows.Location = New System.Drawing.Point(0, 20)
        Me.pnlFilterMissingItemsMain_Shows.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlFilterMissingItemsMain_Shows.Name = "pnlFilterMissingItemsMain_Shows"
        Me.pnlFilterMissingItemsMain_Shows.Size = New System.Drawing.Size(170, 232)
        Me.pnlFilterMissingItemsMain_Shows.TabIndex = 26
        '
        'tblFilterMissingItemsMain_Shows
        '
        Me.tblFilterMissingItemsMain_Shows.AutoSize = true
        Me.tblFilterMissingItemsMain_Shows.ColumnCount = 2
        Me.tblFilterMissingItemsMain_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsMain_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsMain_Shows.Controls.Add(Me.chkShowMissingBanner, 0, 0)
        Me.tblFilterMissingItemsMain_Shows.Controls.Add(Me.chkShowMissingClearArt, 0, 2)
        Me.tblFilterMissingItemsMain_Shows.Controls.Add(Me.chkShowMissingClearLogo, 0, 3)
        Me.tblFilterMissingItemsMain_Shows.Controls.Add(Me.chkShowMissingEFanarts, 0, 4)
        Me.tblFilterMissingItemsMain_Shows.Controls.Add(Me.chkShowMissingFanart, 0, 5)
        Me.tblFilterMissingItemsMain_Shows.Controls.Add(Me.chkShowMissingLandscape, 0, 6)
        Me.tblFilterMissingItemsMain_Shows.Controls.Add(Me.chkShowMissingPoster, 0, 8)
        Me.tblFilterMissingItemsMain_Shows.Controls.Add(Me.chkShowMissingNFO, 0, 7)
        Me.tblFilterMissingItemsMain_Shows.Controls.Add(Me.chkShowMissingCharacterArt, 0, 1)
        Me.tblFilterMissingItemsMain_Shows.Controls.Add(Me.chkShowMissingTheme, 0, 9)
        Me.tblFilterMissingItemsMain_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterMissingItemsMain_Shows.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterMissingItemsMain_Shows.Name = "tblFilterMissingItemsMain_Shows"
        Me.tblFilterMissingItemsMain_Shows.RowCount = 11
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterMissingItemsMain_Shows.Size = New System.Drawing.Size(168, 230)
        Me.tblFilterMissingItemsMain_Shows.TabIndex = 0
        '
        'chkShowMissingBanner
        '
        Me.chkShowMissingBanner.AutoSize = true
        Me.chkShowMissingBanner.Location = New System.Drawing.Point(3, 3)
        Me.chkShowMissingBanner.Name = "chkShowMissingBanner"
        Me.chkShowMissingBanner.Size = New System.Drawing.Size(63, 17)
        Me.chkShowMissingBanner.TabIndex = 0
        Me.chkShowMissingBanner.Text = "Banner"
        Me.chkShowMissingBanner.UseVisualStyleBackColor = true
        '
        'chkShowMissingClearArt
        '
        Me.chkShowMissingClearArt.AutoSize = true
        Me.chkShowMissingClearArt.Location = New System.Drawing.Point(3, 49)
        Me.chkShowMissingClearArt.Name = "chkShowMissingClearArt"
        Me.chkShowMissingClearArt.Size = New System.Drawing.Size(67, 17)
        Me.chkShowMissingClearArt.TabIndex = 0
        Me.chkShowMissingClearArt.Text = "ClearArt"
        Me.chkShowMissingClearArt.UseVisualStyleBackColor = true
        '
        'chkShowMissingClearLogo
        '
        Me.chkShowMissingClearLogo.AutoSize = true
        Me.chkShowMissingClearLogo.Location = New System.Drawing.Point(3, 72)
        Me.chkShowMissingClearLogo.Name = "chkShowMissingClearLogo"
        Me.chkShowMissingClearLogo.Size = New System.Drawing.Size(78, 17)
        Me.chkShowMissingClearLogo.TabIndex = 0
        Me.chkShowMissingClearLogo.Text = "ClearLogo"
        Me.chkShowMissingClearLogo.UseVisualStyleBackColor = true
        '
        'chkShowMissingEFanarts
        '
        Me.chkShowMissingEFanarts.AutoSize = true
        Me.chkShowMissingEFanarts.Location = New System.Drawing.Point(3, 95)
        Me.chkShowMissingEFanarts.Name = "chkShowMissingEFanarts"
        Me.chkShowMissingEFanarts.Size = New System.Drawing.Size(87, 17)
        Me.chkShowMissingEFanarts.TabIndex = 0
        Me.chkShowMissingEFanarts.Text = "Extrafanarts"
        Me.chkShowMissingEFanarts.UseVisualStyleBackColor = true
        '
        'chkShowMissingFanart
        '
        Me.chkShowMissingFanart.AutoSize = true
        Me.chkShowMissingFanart.Location = New System.Drawing.Point(3, 118)
        Me.chkShowMissingFanart.Name = "chkShowMissingFanart"
        Me.chkShowMissingFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkShowMissingFanart.TabIndex = 0
        Me.chkShowMissingFanart.Text = "Fanart"
        Me.chkShowMissingFanart.UseVisualStyleBackColor = true
        '
        'chkShowMissingLandscape
        '
        Me.chkShowMissingLandscape.AutoSize = true
        Me.chkShowMissingLandscape.Location = New System.Drawing.Point(3, 141)
        Me.chkShowMissingLandscape.Name = "chkShowMissingLandscape"
        Me.chkShowMissingLandscape.Size = New System.Drawing.Size(80, 17)
        Me.chkShowMissingLandscape.TabIndex = 0
        Me.chkShowMissingLandscape.Text = "Landscape"
        Me.chkShowMissingLandscape.UseVisualStyleBackColor = true
        '
        'chkShowMissingPoster
        '
        Me.chkShowMissingPoster.AutoSize = true
        Me.chkShowMissingPoster.Location = New System.Drawing.Point(3, 187)
        Me.chkShowMissingPoster.Name = "chkShowMissingPoster"
        Me.chkShowMissingPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkShowMissingPoster.TabIndex = 0
        Me.chkShowMissingPoster.Text = "Poster"
        Me.chkShowMissingPoster.UseVisualStyleBackColor = true
        '
        'chkShowMissingNFO
        '
        Me.chkShowMissingNFO.AutoSize = true
        Me.chkShowMissingNFO.Location = New System.Drawing.Point(3, 164)
        Me.chkShowMissingNFO.Name = "chkShowMissingNFO"
        Me.chkShowMissingNFO.Size = New System.Drawing.Size(49, 17)
        Me.chkShowMissingNFO.TabIndex = 0
        Me.chkShowMissingNFO.Text = "NFO"
        Me.chkShowMissingNFO.UseVisualStyleBackColor = true
        '
        'chkShowMissingCharacterArt
        '
        Me.chkShowMissingCharacterArt.AutoSize = true
        Me.chkShowMissingCharacterArt.Location = New System.Drawing.Point(3, 26)
        Me.chkShowMissingCharacterArt.Name = "chkShowMissingCharacterArt"
        Me.chkShowMissingCharacterArt.Size = New System.Drawing.Size(90, 17)
        Me.chkShowMissingCharacterArt.TabIndex = 0
        Me.chkShowMissingCharacterArt.Text = "CharacterArt"
        Me.chkShowMissingCharacterArt.UseVisualStyleBackColor = true
        '
        'chkShowMissingTheme
        '
        Me.chkShowMissingTheme.AutoSize = true
        Me.chkShowMissingTheme.Location = New System.Drawing.Point(3, 210)
        Me.chkShowMissingTheme.Name = "chkShowMissingTheme"
        Me.chkShowMissingTheme.Size = New System.Drawing.Size(59, 17)
        Me.chkShowMissingTheme.TabIndex = 0
        Me.chkShowMissingTheme.Text = "Theme"
        Me.chkShowMissingTheme.UseVisualStyleBackColor = true
        '
        'pnlFilterMissingItemsTop_Shows
        '
        Me.pnlFilterMissingItemsTop_Shows.AutoSize = true
        Me.pnlFilterMissingItemsTop_Shows.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.pnlFilterMissingItemsTop_Shows.Controls.Add(Me.tblFilterMissingItemsTop_Shows)
        Me.pnlFilterMissingItemsTop_Shows.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterMissingItemsTop_Shows.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterMissingItemsTop_Shows.Name = "pnlFilterMissingItemsTop_Shows"
        Me.pnlFilterMissingItemsTop_Shows.Size = New System.Drawing.Size(170, 20)
        Me.pnlFilterMissingItemsTop_Shows.TabIndex = 25
        '
        'tblFilterMissingItemsTop_Shows
        '
        Me.tblFilterMissingItemsTop_Shows.AutoSize = true
        Me.tblFilterMissingItemsTop_Shows.ColumnCount = 3
        Me.tblFilterMissingItemsTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tblFilterMissingItemsTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsTop_Shows.Controls.Add(Me.lblFilterMissingItems_Shows, 0, 0)
        Me.tblFilterMissingItemsTop_Shows.Controls.Add(Me.lblFilterMissingItemsClose_Shows, 2, 0)
        Me.tblFilterMissingItemsTop_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterMissingItemsTop_Shows.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterMissingItemsTop_Shows.Name = "tblFilterMissingItemsTop_Shows"
        Me.tblFilterMissingItemsTop_Shows.RowCount = 2
        Me.tblFilterMissingItemsTop_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterMissingItemsTop_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsTop_Shows.Size = New System.Drawing.Size(170, 20)
        Me.tblFilterMissingItemsTop_Shows.TabIndex = 0
        '
        'lblFilterMissingItems_Shows
        '
        Me.lblFilterMissingItems_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterMissingItems_Shows.AutoSize = true
        Me.lblFilterMissingItems_Shows.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterMissingItems_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterMissingItems_Shows.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilterMissingItems_Shows.Location = New System.Drawing.Point(3, 3)
        Me.lblFilterMissingItems_Shows.Name = "lblFilterMissingItems_Shows"
        Me.lblFilterMissingItems_Shows.Size = New System.Drawing.Size(79, 13)
        Me.lblFilterMissingItems_Shows.TabIndex = 23
        Me.lblFilterMissingItems_Shows.Text = "Missing Items"
        Me.lblFilterMissingItems_Shows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterMissingItemsClose_Shows
        '
        Me.lblFilterMissingItemsClose_Shows.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblFilterMissingItemsClose_Shows.AutoSize = true
        Me.lblFilterMissingItemsClose_Shows.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterMissingItemsClose_Shows.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterMissingItemsClose_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterMissingItemsClose_Shows.ForeColor = System.Drawing.Color.White
        Me.lblFilterMissingItemsClose_Shows.Location = New System.Drawing.Point(132, 3)
        Me.lblFilterMissingItemsClose_Shows.Name = "lblFilterMissingItemsClose_Shows"
        Me.lblFilterMissingItemsClose_Shows.Size = New System.Drawing.Size(35, 13)
        Me.lblFilterMissingItemsClose_Shows.TabIndex = 24
        Me.lblFilterMissingItemsClose_Shows.Text = "Close"
        '
        'pnlFilterSources_Movies
        '
        Me.pnlFilterSources_Movies.Controls.Add(Me.pnlFilterSourcesMain_Movies)
        Me.pnlFilterSources_Movies.Controls.Add(Me.pnlFilterSourcesTop_Movies)
        Me.pnlFilterSources_Movies.Location = New System.Drawing.Point(0, 900)
        Me.pnlFilterSources_Movies.Name = "pnlFilterSources_Movies"
        Me.pnlFilterSources_Movies.Size = New System.Drawing.Size(189, 192)
        Me.pnlFilterSources_Movies.TabIndex = 16
        Me.pnlFilterSources_Movies.Visible = false
        '
        'pnlFilterSourcesMain_Movies
        '
        Me.pnlFilterSourcesMain_Movies.AutoSize = true
        Me.pnlFilterSourcesMain_Movies.BackColor = System.Drawing.Color.Transparent
        Me.pnlFilterSourcesMain_Movies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilterSourcesMain_Movies.Controls.Add(Me.clbFilterSources_Movies)
        Me.pnlFilterSourcesMain_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFilterSourcesMain_Movies.Location = New System.Drawing.Point(0, 20)
        Me.pnlFilterSourcesMain_Movies.Name = "pnlFilterSourcesMain_Movies"
        Me.pnlFilterSourcesMain_Movies.Size = New System.Drawing.Size(189, 172)
        Me.pnlFilterSourcesMain_Movies.TabIndex = 26
        '
        'clbFilterSources_Movies
        '
        Me.clbFilterSources_Movies.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.clbFilterSources_Movies.CheckOnClick = true
        Me.clbFilterSources_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbFilterSources_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.clbFilterSources_Movies.FormattingEnabled = true
        Me.clbFilterSources_Movies.Location = New System.Drawing.Point(0, 0)
        Me.clbFilterSources_Movies.Name = "clbFilterSources_Movies"
        Me.clbFilterSources_Movies.Size = New System.Drawing.Size(187, 170)
        Me.clbFilterSources_Movies.TabIndex = 8
        Me.clbFilterSources_Movies.TabStop = false
        '
        'pnlFilterSourcesTop_Movies
        '
        Me.pnlFilterSourcesTop_Movies.AutoSize = true
        Me.pnlFilterSourcesTop_Movies.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.pnlFilterSourcesTop_Movies.Controls.Add(Me.tblFilterSourcesTop_Movies)
        Me.pnlFilterSourcesTop_Movies.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterSourcesTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterSourcesTop_Movies.Name = "pnlFilterSourcesTop_Movies"
        Me.pnlFilterSourcesTop_Movies.Size = New System.Drawing.Size(189, 20)
        Me.pnlFilterSourcesTop_Movies.TabIndex = 25
        '
        'tblFilterSourcesTop_Movies
        '
        Me.tblFilterSourcesTop_Movies.AutoSize = true
        Me.tblFilterSourcesTop_Movies.ColumnCount = 3
        Me.tblFilterSourcesTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSourcesTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tblFilterSourcesTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSourcesTop_Movies.Controls.Add(Me.lblFilterSources_Movies, 0, 0)
        Me.tblFilterSourcesTop_Movies.Controls.Add(Me.lblFilterSourcesClose_Movies, 2, 0)
        Me.tblFilterSourcesTop_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterSourcesTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterSourcesTop_Movies.Name = "tblFilterSourcesTop_Movies"
        Me.tblFilterSourcesTop_Movies.RowCount = 2
        Me.tblFilterSourcesTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterSourcesTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSourcesTop_Movies.Size = New System.Drawing.Size(189, 20)
        Me.tblFilterSourcesTop_Movies.TabIndex = 0
        '
        'lblFilterSources_Movies
        '
        Me.lblFilterSources_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterSources_Movies.AutoSize = true
        Me.lblFilterSources_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterSources_Movies.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterSources_Movies.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilterSources_Movies.Location = New System.Drawing.Point(3, 3)
        Me.lblFilterSources_Movies.Name = "lblFilterSources_Movies"
        Me.lblFilterSources_Movies.Size = New System.Drawing.Size(53, 13)
        Me.lblFilterSources_Movies.TabIndex = 23
        Me.lblFilterSources_Movies.Text = "Sources"
        Me.lblFilterSources_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterSourcesClose_Movies
        '
        Me.lblFilterSourcesClose_Movies.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblFilterSourcesClose_Movies.AutoSize = true
        Me.lblFilterSourcesClose_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterSourcesClose_Movies.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterSourcesClose_Movies.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterSourcesClose_Movies.ForeColor = System.Drawing.Color.White
        Me.lblFilterSourcesClose_Movies.Location = New System.Drawing.Point(153, 3)
        Me.lblFilterSourcesClose_Movies.Name = "lblFilterSourcesClose_Movies"
        Me.lblFilterSourcesClose_Movies.Size = New System.Drawing.Size(33, 13)
        Me.lblFilterSourcesClose_Movies.TabIndex = 24
        Me.lblFilterSourcesClose_Movies.Text = "Close"
        '
        'pnlFilterSources_Shows
        '
        Me.pnlFilterSources_Shows.BackColor = System.Drawing.Color.Transparent
        Me.pnlFilterSources_Shows.Controls.Add(Me.pnlFilterSourcesMain_Shows)
        Me.pnlFilterSources_Shows.Controls.Add(Me.pnlFilterSourcesTop_Shows)
        Me.pnlFilterSources_Shows.Location = New System.Drawing.Point(0, 900)
        Me.pnlFilterSources_Shows.Name = "pnlFilterSources_Shows"
        Me.pnlFilterSources_Shows.Size = New System.Drawing.Size(189, 192)
        Me.pnlFilterSources_Shows.TabIndex = 34
        Me.pnlFilterSources_Shows.Visible = false
        '
        'pnlFilterSourcesMain_Shows
        '
        Me.pnlFilterSourcesMain_Shows.AutoSize = true
        Me.pnlFilterSourcesMain_Shows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilterSourcesMain_Shows.Controls.Add(Me.clbFilterSource_Shows)
        Me.pnlFilterSourcesMain_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFilterSourcesMain_Shows.Location = New System.Drawing.Point(0, 20)
        Me.pnlFilterSourcesMain_Shows.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlFilterSourcesMain_Shows.Name = "pnlFilterSourcesMain_Shows"
        Me.pnlFilterSourcesMain_Shows.Size = New System.Drawing.Size(189, 172)
        Me.pnlFilterSourcesMain_Shows.TabIndex = 26
        '
        'clbFilterSource_Shows
        '
        Me.clbFilterSource_Shows.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.clbFilterSource_Shows.CheckOnClick = true
        Me.clbFilterSource_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbFilterSource_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.clbFilterSource_Shows.FormattingEnabled = true
        Me.clbFilterSource_Shows.Location = New System.Drawing.Point(0, 0)
        Me.clbFilterSource_Shows.Margin = New System.Windows.Forms.Padding(0)
        Me.clbFilterSource_Shows.Name = "clbFilterSource_Shows"
        Me.clbFilterSource_Shows.Size = New System.Drawing.Size(187, 170)
        Me.clbFilterSource_Shows.TabIndex = 8
        Me.clbFilterSource_Shows.TabStop = false
        '
        'pnlFilterSourcesTop_Shows
        '
        Me.pnlFilterSourcesTop_Shows.AutoSize = true
        Me.pnlFilterSourcesTop_Shows.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.pnlFilterSourcesTop_Shows.Controls.Add(Me.tblFilterSourcesTop_Shows)
        Me.pnlFilterSourcesTop_Shows.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterSourcesTop_Shows.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterSourcesTop_Shows.Name = "pnlFilterSourcesTop_Shows"
        Me.pnlFilterSourcesTop_Shows.Size = New System.Drawing.Size(189, 20)
        Me.pnlFilterSourcesTop_Shows.TabIndex = 25
        '
        'tblFilterSourcesTop_Shows
        '
        Me.tblFilterSourcesTop_Shows.AutoSize = true
        Me.tblFilterSourcesTop_Shows.ColumnCount = 3
        Me.tblFilterSourcesTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSourcesTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tblFilterSourcesTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSourcesTop_Shows.Controls.Add(Me.lblFilterSources_Shows, 0, 0)
        Me.tblFilterSourcesTop_Shows.Controls.Add(Me.lblFilterSourcesClose_Shows, 2, 0)
        Me.tblFilterSourcesTop_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterSourcesTop_Shows.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterSourcesTop_Shows.Name = "tblFilterSourcesTop_Shows"
        Me.tblFilterSourcesTop_Shows.RowCount = 2
        Me.tblFilterSourcesTop_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterSourcesTop_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSourcesTop_Shows.Size = New System.Drawing.Size(189, 20)
        Me.tblFilterSourcesTop_Shows.TabIndex = 0
        '
        'lblFilterSources_Shows
        '
        Me.lblFilterSources_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterSources_Shows.AutoSize = true
        Me.lblFilterSources_Shows.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterSources_Shows.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterSources_Shows.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilterSources_Shows.Location = New System.Drawing.Point(3, 3)
        Me.lblFilterSources_Shows.Name = "lblFilterSources_Shows"
        Me.lblFilterSources_Shows.Size = New System.Drawing.Size(53, 13)
        Me.lblFilterSources_Shows.TabIndex = 23
        Me.lblFilterSources_Shows.Text = "Sources"
        Me.lblFilterSources_Shows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilterSourcesClose_Shows
        '
        Me.lblFilterSourcesClose_Shows.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblFilterSourcesClose_Shows.AutoSize = true
        Me.lblFilterSourcesClose_Shows.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterSourcesClose_Shows.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterSourcesClose_Shows.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilterSourcesClose_Shows.ForeColor = System.Drawing.Color.White
        Me.lblFilterSourcesClose_Shows.Location = New System.Drawing.Point(153, 3)
        Me.lblFilterSourcesClose_Shows.Name = "lblFilterSourcesClose_Shows"
        Me.lblFilterSourcesClose_Shows.Size = New System.Drawing.Size(33, 13)
        Me.lblFilterSourcesClose_Shows.TabIndex = 24
        Me.lblFilterSourcesClose_Shows.Text = "Close"
        '
        'dgvMovies
        '
        Me.dgvMovies.AllowUserToAddRows = false
        Me.dgvMovies.AllowUserToDeleteRows = false
        Me.dgvMovies.AllowUserToResizeRows = false
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(230,Byte),Integer), CType(CType(230,Byte),Integer), CType(CType(230,Byte),Integer))
        Me.dgvMovies.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvMovies.BackgroundColor = System.Drawing.Color.White
        Me.dgvMovies.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvMovies.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvMovies.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvMovies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMovies.ContextMenuStrip = Me.cmnuMovie
        Me.dgvMovies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMovies.GridColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer))
        Me.dgvMovies.Location = New System.Drawing.Point(0, 56)
        Me.dgvMovies.Name = "dgvMovies"
        Me.dgvMovies.ReadOnly = true
        Me.dgvMovies.RowHeadersVisible = false
        Me.dgvMovies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMovies.ShowCellErrors = false
        Me.dgvMovies.ShowRowErrors = false
        Me.dgvMovies.Size = New System.Drawing.Size(567, 0)
        Me.dgvMovies.StandardTab = true
        Me.dgvMovies.TabIndex = 0
        '
        'cmnuMovie
        '
        Me.cmnuMovie.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieTitle, Me.cmnuMovieSep1, Me.cmnuMovieReload, Me.cmnuMovieMark, Me.cmnuMovieMarkAs, Me.cmnuMovieLock, Me.cmnuMovieWatched, Me.cmnuMovieSep2, Me.cmnuMovieEdit, Me.cmnuMovieEditMetaData, Me.cmnuMovieGenres, Me.cmnuMovieSep3, Me.cmnuMovieRescrape, Me.cmnuMovieReSel, Me.cmnuMovieUpSel, Me.cmnuMovieChange, Me.cmnuMovieChangeAuto, Me.cmnuMovieSep4, Me.cmnuMovieBrowseIMDB, Me.cmnuMovieBrowseTMDB, Me.cmnuMovieOpenFolder, Me.cmnuMovieSep5, Me.cmnuMovieRemove})
        Me.cmnuMovie.Name = "mnuMediaList"
        Me.cmnuMovie.Size = New System.Drawing.Size(247, 430)
        '
        'cmnuMovieTitle
        '
        Me.cmnuMovieTitle.Enabled = false
        Me.cmnuMovieTitle.Image = CType(resources.GetObject("cmnuMovieTitle.Image"),System.Drawing.Image)
        Me.cmnuMovieTitle.Name = "cmnuMovieTitle"
        Me.cmnuMovieTitle.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieTitle.Text = "Title"
        '
        'cmnuMovieSep1
        '
        Me.cmnuMovieSep1.Name = "cmnuMovieSep1"
        Me.cmnuMovieSep1.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuMovieReload
        '
        Me.cmnuMovieReload.Image = CType(resources.GetObject("cmnuMovieReload.Image"),System.Drawing.Image)
        Me.cmnuMovieReload.Name = "cmnuMovieReload"
        Me.cmnuMovieReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmnuMovieReload.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieReload.Text = "Reload"
        '
        'cmnuMovieMark
        '
        Me.cmnuMovieMark.Image = CType(resources.GetObject("cmnuMovieMark.Image"),System.Drawing.Image)
        Me.cmnuMovieMark.Name = "cmnuMovieMark"
        Me.cmnuMovieMark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M),System.Windows.Forms.Keys)
        Me.cmnuMovieMark.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieMark.Text = "Mark"
        '
        'cmnuMovieMarkAs
        '
        Me.cmnuMovieMarkAs.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieMarkAsCustom1, Me.cmnuMovieMarkAsCustom2, Me.cmnuMovieMarkAsCustom3, Me.cmnuMovieMarkAsCustom4})
        Me.cmnuMovieMarkAs.Image = Global.Ember_Media_Manager.My.Resources.Resources.valid2
        Me.cmnuMovieMarkAs.Name = "cmnuMovieMarkAs"
        Me.cmnuMovieMarkAs.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieMarkAs.Text = "Mark as"
        '
        'cmnuMovieMarkAsCustom1
        '
        Me.cmnuMovieMarkAsCustom1.Name = "cmnuMovieMarkAsCustom1"
        Me.cmnuMovieMarkAsCustom1.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D1),System.Windows.Forms.Keys)
        Me.cmnuMovieMarkAsCustom1.Size = New System.Drawing.Size(174, 22)
        Me.cmnuMovieMarkAsCustom1.Text = "Custom #1"
        '
        'cmnuMovieMarkAsCustom2
        '
        Me.cmnuMovieMarkAsCustom2.Name = "cmnuMovieMarkAsCustom2"
        Me.cmnuMovieMarkAsCustom2.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D2),System.Windows.Forms.Keys)
        Me.cmnuMovieMarkAsCustom2.Size = New System.Drawing.Size(174, 22)
        Me.cmnuMovieMarkAsCustom2.Text = "Custom #2"
        '
        'cmnuMovieMarkAsCustom3
        '
        Me.cmnuMovieMarkAsCustom3.Name = "cmnuMovieMarkAsCustom3"
        Me.cmnuMovieMarkAsCustom3.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D3),System.Windows.Forms.Keys)
        Me.cmnuMovieMarkAsCustom3.Size = New System.Drawing.Size(174, 22)
        Me.cmnuMovieMarkAsCustom3.Text = "Custom #3"
        '
        'cmnuMovieMarkAsCustom4
        '
        Me.cmnuMovieMarkAsCustom4.Name = "cmnuMovieMarkAsCustom4"
        Me.cmnuMovieMarkAsCustom4.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D4),System.Windows.Forms.Keys)
        Me.cmnuMovieMarkAsCustom4.Size = New System.Drawing.Size(174, 22)
        Me.cmnuMovieMarkAsCustom4.Text = "Custom #4"
        '
        'cmnuMovieLock
        '
        Me.cmnuMovieLock.Image = CType(resources.GetObject("cmnuMovieLock.Image"),System.Drawing.Image)
        Me.cmnuMovieLock.Name = "cmnuMovieLock"
        Me.cmnuMovieLock.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L),System.Windows.Forms.Keys)
        Me.cmnuMovieLock.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieLock.Text = "Lock"
        '
        'cmnuMovieWatched
        '
        Me.cmnuMovieWatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuMovieWatched.Name = "cmnuMovieWatched"
        Me.cmnuMovieWatched.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W),System.Windows.Forms.Keys)
        Me.cmnuMovieWatched.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieWatched.Text = "Watched"
        '
        'cmnuMovieSep2
        '
        Me.cmnuMovieSep2.Name = "cmnuMovieSep2"
        Me.cmnuMovieSep2.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuMovieEdit
        '
        Me.cmnuMovieEdit.Image = CType(resources.GetObject("cmnuMovieEdit.Image"),System.Drawing.Image)
        Me.cmnuMovieEdit.Name = "cmnuMovieEdit"
        Me.cmnuMovieEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E),System.Windows.Forms.Keys)
        Me.cmnuMovieEdit.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieEdit.Text = "Edit Movie"
        '
        'cmnuMovieEditMetaData
        '
        Me.cmnuMovieEditMetaData.Image = CType(resources.GetObject("cmnuMovieEditMetaData.Image"),System.Drawing.Image)
        Me.cmnuMovieEditMetaData.Name = "cmnuMovieEditMetaData"
        Me.cmnuMovieEditMetaData.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D),System.Windows.Forms.Keys)
        Me.cmnuMovieEditMetaData.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieEditMetaData.Text = "Edit Meta Data"
        '
        'cmnuMovieGenres
        '
        Me.cmnuMovieGenres.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieGenresTitle, Me.cmnuMovieGenresGenre, Me.cmnuMovieGenresAdd, Me.cmnuMovieGenresSet, Me.cmnuMovieGenresRemove})
        Me.cmnuMovieGenres.Image = CType(resources.GetObject("cmnuMovieGenres.Image"),System.Drawing.Image)
        Me.cmnuMovieGenres.Name = "cmnuMovieGenres"
        Me.cmnuMovieGenres.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieGenres.Text = "Genres"
        '
        'cmnuMovieGenresTitle
        '
        Me.cmnuMovieGenresTitle.Enabled = false
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
        Me.cmnuMovieGenresGenre.Sorted = true
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
        'cmnuMovieSep3
        '
        Me.cmnuMovieSep3.Name = "cmnuMovieSep3"
        Me.cmnuMovieSep3.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuMovieRescrape
        '
        Me.cmnuMovieRescrape.Image = CType(resources.GetObject("cmnuMovieRescrape.Image"),System.Drawing.Image)
        Me.cmnuMovieRescrape.Name = "cmnuMovieRescrape"
        Me.cmnuMovieRescrape.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I),System.Windows.Forms.Keys)
        Me.cmnuMovieRescrape.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieRescrape.Text = "(Re)Scrape Movie"
        '
        'cmnuMovieReSel
        '
        Me.cmnuMovieReSel.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieReSelAuto, Me.cmnuMovieReSelAsk, Me.cmnuMovieReSelSkip})
        Me.cmnuMovieReSel.Image = CType(resources.GetObject("cmnuMovieReSel.Image"),System.Drawing.Image)
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
        Me.cmnuMovieReSelAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieReSelAskAll, Me.cmnuMovieReSelAskActor, Me.cmnuMovieReSelAskBanner, Me.cmnuMovieReSelAskClearArt, Me.cmnuMovieReSelAskClearLogo, Me.cmnuMovieReSelAskDiscArt, Me.cmnuMovieReSelAskEFanarts, Me.cmnuMovieReSelAskEThumbs, Me.cmnuMovieMovieReSelAskFanart, Me.cmnuMovieReSelAskLandscape, Me.cmnuMovieReSelAskMetaData, Me.cmnuMovieReSelAskNfo, Me.cmnuMovieReSelAskPoster, Me.cmnuMovieReSelAskTheme, Me.cmnuMovieReSelAskTrailer})
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
        'cmnuMovieMovieReSelAskFanart
        '
        Me.cmnuMovieMovieReSelAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuMovieMovieReSelAskFanart.Name = "cmnuMovieMovieReSelAskFanart"
        Me.cmnuMovieMovieReSelAskFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuMovieMovieReSelAskFanart.Text = "Fanart Only"
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
        'cmnuMovieUpSel
        '
        Me.cmnuMovieUpSel.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieUpSelCert, Me.cmnuMovieUpSelCountry, Me.cmnuMovieUpSelDirector, Me.cmnuMovieUpSelMPAA, Me.cmnuMovieUpSelPlot, Me.cmnuMovieUpSelOutline, Me.cmnuMovieUpSelProducers, Me.cmnuMovieUpSelRelease, Me.cmnuMovieUpSelRating, Me.cmnuMovieUpSelStudio, Me.cmnuMovieUpSelTagline, Me.cmnuMovieUpSelTitle, Me.cmnuMovieUpSelTop250, Me.cmnuMovieUpSelTrailer, Me.cmnuMovieUpSelWriter, Me.cmnuMovieUpSelYear})
        Me.cmnuMovieUpSel.Name = "cmnuMovieUpSel"
        Me.cmnuMovieUpSel.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieUpSel.Text = "Update Single Data Field"
        '
        'cmnuMovieUpSelCert
        '
        Me.cmnuMovieUpSelCert.Name = "cmnuMovieUpSelCert"
        Me.cmnuMovieUpSelCert.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelCert.Text = "Certification"
        '
        'cmnuMovieUpSelCountry
        '
        Me.cmnuMovieUpSelCountry.Name = "cmnuMovieUpSelCountry"
        Me.cmnuMovieUpSelCountry.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelCountry.Text = "Country"
        '
        'cmnuMovieUpSelDirector
        '
        Me.cmnuMovieUpSelDirector.Name = "cmnuMovieUpSelDirector"
        Me.cmnuMovieUpSelDirector.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelDirector.Text = "Director"
        '
        'cmnuMovieUpSelMPAA
        '
        Me.cmnuMovieUpSelMPAA.Name = "cmnuMovieUpSelMPAA"
        Me.cmnuMovieUpSelMPAA.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelMPAA.Text = "MPAA"
        '
        'cmnuMovieUpSelPlot
        '
        Me.cmnuMovieUpSelPlot.Name = "cmnuMovieUpSelPlot"
        Me.cmnuMovieUpSelPlot.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelPlot.Text = "Plot"
        '
        'cmnuMovieUpSelOutline
        '
        Me.cmnuMovieUpSelOutline.Name = "cmnuMovieUpSelOutline"
        Me.cmnuMovieUpSelOutline.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelOutline.Text = "Plot Outline"
        '
        'cmnuMovieUpSelProducers
        '
        Me.cmnuMovieUpSelProducers.Name = "cmnuMovieUpSelProducers"
        Me.cmnuMovieUpSelProducers.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelProducers.Text = "Producers"
        '
        'cmnuMovieUpSelRelease
        '
        Me.cmnuMovieUpSelRelease.Name = "cmnuMovieUpSelRelease"
        Me.cmnuMovieUpSelRelease.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelRelease.Text = "Release Date"
        '
        'cmnuMovieUpSelRating
        '
        Me.cmnuMovieUpSelRating.Name = "cmnuMovieUpSelRating"
        Me.cmnuMovieUpSelRating.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelRating.Text = "Rating / Votes"
        '
        'cmnuMovieUpSelStudio
        '
        Me.cmnuMovieUpSelStudio.Name = "cmnuMovieUpSelStudio"
        Me.cmnuMovieUpSelStudio.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelStudio.Text = "Studio"
        '
        'cmnuMovieUpSelTagline
        '
        Me.cmnuMovieUpSelTagline.Name = "cmnuMovieUpSelTagline"
        Me.cmnuMovieUpSelTagline.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelTagline.Text = "Tagline"
        '
        'cmnuMovieUpSelTitle
        '
        Me.cmnuMovieUpSelTitle.Name = "cmnuMovieUpSelTitle"
        Me.cmnuMovieUpSelTitle.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelTitle.Text = "Title"
        '
        'cmnuMovieUpSelTop250
        '
        Me.cmnuMovieUpSelTop250.Name = "cmnuMovieUpSelTop250"
        Me.cmnuMovieUpSelTop250.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelTop250.Text = "Top 250"
        '
        'cmnuMovieUpSelTrailer
        '
        Me.cmnuMovieUpSelTrailer.Name = "cmnuMovieUpSelTrailer"
        Me.cmnuMovieUpSelTrailer.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelTrailer.Text = "Trailer"
        '
        'cmnuMovieUpSelWriter
        '
        Me.cmnuMovieUpSelWriter.Name = "cmnuMovieUpSelWriter"
        Me.cmnuMovieUpSelWriter.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelWriter.Text = "Writer"
        '
        'cmnuMovieUpSelYear
        '
        Me.cmnuMovieUpSelYear.Name = "cmnuMovieUpSelYear"
        Me.cmnuMovieUpSelYear.Size = New System.Drawing.Size(148, 22)
        Me.cmnuMovieUpSelYear.Text = "Year"
        '
        'cmnuMovieChange
        '
        Me.cmnuMovieChange.Image = CType(resources.GetObject("cmnuMovieChange.Image"),System.Drawing.Image)
        Me.cmnuMovieChange.Name = "cmnuMovieChange"
        Me.cmnuMovieChange.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C),System.Windows.Forms.Keys)
        Me.cmnuMovieChange.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieChange.Text = "Change Movie"
        '
        'cmnuMovieChangeAuto
        '
        Me.cmnuMovieChangeAuto.Image = CType(resources.GetObject("cmnuMovieChangeAuto.Image"),System.Drawing.Image)
        Me.cmnuMovieChangeAuto.Name = "cmnuMovieChangeAuto"
        Me.cmnuMovieChangeAuto.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieChangeAuto.Text = "Change Movie (Auto)"
        '
        'cmnuMovieSep4
        '
        Me.cmnuMovieSep4.Name = "cmnuMovieSep4"
        Me.cmnuMovieSep4.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuMovieBrowseIMDB
        '
        Me.cmnuMovieBrowseIMDB.Image = Global.Ember_Media_Manager.My.Resources.Resources.imdb
        Me.cmnuMovieBrowseIMDB.Name = "cmnuMovieBrowseIMDB"
        Me.cmnuMovieBrowseIMDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B),System.Windows.Forms.Keys)
        Me.cmnuMovieBrowseIMDB.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieBrowseIMDB.Text = "Open IMDB-Page"
        '
        'cmnuMovieBrowseTMDB
        '
        Me.cmnuMovieBrowseTMDB.Image = Global.Ember_Media_Manager.My.Resources.Resources.tmdb
        Me.cmnuMovieBrowseTMDB.Name = "cmnuMovieBrowseTMDB"
        Me.cmnuMovieBrowseTMDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N),System.Windows.Forms.Keys)
        Me.cmnuMovieBrowseTMDB.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieBrowseTMDB.Text = "Open TMDB-Page"
        '
        'cmnuMovieOpenFolder
        '
        Me.cmnuMovieOpenFolder.Image = CType(resources.GetObject("cmnuMovieOpenFolder.Image"),System.Drawing.Image)
        Me.cmnuMovieOpenFolder.Name = "cmnuMovieOpenFolder"
        Me.cmnuMovieOpenFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O),System.Windows.Forms.Keys)
        Me.cmnuMovieOpenFolder.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieOpenFolder.Text = "Open Containing Folder"
        '
        'cmnuMovieSep5
        '
        Me.cmnuMovieSep5.Name = "cmnuMovieSep5"
        Me.cmnuMovieSep5.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuMovieRemove
        '
        Me.cmnuMovieRemove.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieRemoveFromDB, Me.cmnuMovieRemoveFromDisc})
        Me.cmnuMovieRemove.Image = CType(resources.GetObject("cmnuMovieRemove.Image"),System.Drawing.Image)
        Me.cmnuMovieRemove.Name = "cmnuMovieRemove"
        Me.cmnuMovieRemove.Size = New System.Drawing.Size(246, 22)
        Me.cmnuMovieRemove.Text = "Remove"
        '
        'cmnuMovieRemoveFromDB
        '
        Me.cmnuMovieRemoveFromDB.Image = CType(resources.GetObject("cmnuMovieRemoveFromDB.Image"),System.Drawing.Image)
        Me.cmnuMovieRemoveFromDB.Name = "cmnuMovieRemoveFromDB"
        Me.cmnuMovieRemoveFromDB.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.cmnuMovieRemoveFromDB.Size = New System.Drawing.Size(225, 22)
        Me.cmnuMovieRemoveFromDB.Text = "Remove from Database"
        '
        'cmnuMovieRemoveFromDisc
        '
        Me.cmnuMovieRemoveFromDisc.Image = CType(resources.GetObject("cmnuMovieRemoveFromDisc.Image"),System.Drawing.Image)
        Me.cmnuMovieRemoveFromDisc.Name = "cmnuMovieRemoveFromDisc"
        Me.cmnuMovieRemoveFromDisc.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Delete),System.Windows.Forms.Keys)
        Me.cmnuMovieRemoveFromDisc.Size = New System.Drawing.Size(225, 22)
        Me.cmnuMovieRemoveFromDisc.Text = "Delete Movie"
        '
        'dgvMovieSets
        '
        Me.dgvMovieSets.AllowUserToAddRows = false
        Me.dgvMovieSets.AllowUserToDeleteRows = false
        Me.dgvMovieSets.AllowUserToResizeRows = false
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(230,Byte),Integer), CType(CType(230,Byte),Integer), CType(CType(230,Byte),Integer))
        Me.dgvMovieSets.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvMovieSets.BackgroundColor = System.Drawing.Color.White
        Me.dgvMovieSets.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvMovieSets.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvMovieSets.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvMovieSets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMovieSets.ContextMenuStrip = Me.cmnuMovieSet
        Me.dgvMovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMovieSets.GridColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer))
        Me.dgvMovieSets.Location = New System.Drawing.Point(0, 56)
        Me.dgvMovieSets.Name = "dgvMovieSets"
        Me.dgvMovieSets.ReadOnly = true
        Me.dgvMovieSets.RowHeadersVisible = false
        Me.dgvMovieSets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMovieSets.ShowCellErrors = false
        Me.dgvMovieSets.ShowRowErrors = false
        Me.dgvMovieSets.Size = New System.Drawing.Size(567, 0)
        Me.dgvMovieSets.StandardTab = true
        Me.dgvMovieSets.TabIndex = 17
        '
        'cmnuMovieSet
        '
        Me.cmnuMovieSet.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieSetTitle, Me.cmnuMovieSetSep1, Me.cmnuMovieSetReload, Me.cmnuMovieSetMark, Me.cmnuMovieSetLock, Me.cmnuMovieSetSep2, Me.cmnuMovieSetNew, Me.cmnuMovieSetEdit, Me.cmnuMovieSetRemove, Me.cmnuMovieSetSep3, Me.cmnuMovieSetRescrape})
        Me.cmnuMovieSet.Name = "cmnuMovieSets"
        Me.cmnuMovieSet.Size = New System.Drawing.Size(222, 198)
        '
        'cmnuMovieSetTitle
        '
        Me.cmnuMovieSetTitle.Enabled = false
        Me.cmnuMovieSetTitle.Image = CType(resources.GetObject("cmnuMovieSetTitle.Image"),System.Drawing.Image)
        Me.cmnuMovieSetTitle.Name = "cmnuMovieSetTitle"
        Me.cmnuMovieSetTitle.Size = New System.Drawing.Size(221, 22)
        Me.cmnuMovieSetTitle.Text = "Title"
        '
        'cmnuMovieSetSep1
        '
        Me.cmnuMovieSetSep1.Name = "cmnuMovieSetSep1"
        Me.cmnuMovieSetSep1.Size = New System.Drawing.Size(218, 6)
        '
        'cmnuMovieSetReload
        '
        Me.cmnuMovieSetReload.Image = CType(resources.GetObject("cmnuMovieSetReload.Image"),System.Drawing.Image)
        Me.cmnuMovieSetReload.Name = "cmnuMovieSetReload"
        Me.cmnuMovieSetReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmnuMovieSetReload.Size = New System.Drawing.Size(221, 22)
        Me.cmnuMovieSetReload.Text = "Reload"
        '
        'cmnuMovieSetMark
        '
        Me.cmnuMovieSetMark.Image = CType(resources.GetObject("cmnuMovieSetMark.Image"),System.Drawing.Image)
        Me.cmnuMovieSetMark.Name = "cmnuMovieSetMark"
        Me.cmnuMovieSetMark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M),System.Windows.Forms.Keys)
        Me.cmnuMovieSetMark.Size = New System.Drawing.Size(221, 22)
        Me.cmnuMovieSetMark.Text = "Mark"
        '
        'cmnuMovieSetLock
        '
        Me.cmnuMovieSetLock.Image = CType(resources.GetObject("cmnuMovieSetLock.Image"),System.Drawing.Image)
        Me.cmnuMovieSetLock.Name = "cmnuMovieSetLock"
        Me.cmnuMovieSetLock.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L),System.Windows.Forms.Keys)
        Me.cmnuMovieSetLock.Size = New System.Drawing.Size(221, 22)
        Me.cmnuMovieSetLock.Text = "Lock"
        '
        'cmnuMovieSetSep2
        '
        Me.cmnuMovieSetSep2.Name = "cmnuMovieSetSep2"
        Me.cmnuMovieSetSep2.Size = New System.Drawing.Size(218, 6)
        '
        'cmnuMovieSetNew
        '
        Me.cmnuMovieSetNew.Image = Global.Ember_Media_Manager.My.Resources.Resources.menuAdd
        Me.cmnuMovieSetNew.Name = "cmnuMovieSetNew"
        Me.cmnuMovieSetNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N),System.Windows.Forms.Keys)
        Me.cmnuMovieSetNew.Size = New System.Drawing.Size(221, 22)
        Me.cmnuMovieSetNew.Text = "Add New Set"
        '
        'cmnuMovieSetEdit
        '
        Me.cmnuMovieSetEdit.Image = CType(resources.GetObject("cmnuMovieSetEdit.Image"),System.Drawing.Image)
        Me.cmnuMovieSetEdit.Name = "cmnuMovieSetEdit"
        Me.cmnuMovieSetEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E),System.Windows.Forms.Keys)
        Me.cmnuMovieSetEdit.Size = New System.Drawing.Size(221, 22)
        Me.cmnuMovieSetEdit.Text = "Edit Set"
        '
        'cmnuMovieSetRemove
        '
        Me.cmnuMovieSetRemove.Image = CType(resources.GetObject("cmnuMovieSetRemove.Image"),System.Drawing.Image)
        Me.cmnuMovieSetRemove.Name = "cmnuMovieSetRemove"
        Me.cmnuMovieSetRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.cmnuMovieSetRemove.Size = New System.Drawing.Size(221, 22)
        Me.cmnuMovieSetRemove.Text = "Remove"
        '
        'cmnuMovieSetSep3
        '
        Me.cmnuMovieSetSep3.Name = "cmnuMovieSetSep3"
        Me.cmnuMovieSetSep3.Size = New System.Drawing.Size(218, 6)
        '
        'cmnuMovieSetRescrape
        '
        Me.cmnuMovieSetRescrape.Image = CType(resources.GetObject("cmnuMovieSetRescrape.Image"),System.Drawing.Image)
        Me.cmnuMovieSetRescrape.Name = "cmnuMovieSetRescrape"
        Me.cmnuMovieSetRescrape.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I),System.Windows.Forms.Keys)
        Me.cmnuMovieSetRescrape.Size = New System.Drawing.Size(221, 22)
        Me.cmnuMovieSetRescrape.Text = "(Re)Scrape MovieSet"
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
        Me.scTV.Size = New System.Drawing.Size(567, 0)
        Me.scTV.SplitterDistance = 25
        Me.scTV.TabIndex = 3
        Me.scTV.TabStop = false
        '
        'dgvTVShows
        '
        Me.dgvTVShows.AllowUserToAddRows = false
        Me.dgvTVShows.AllowUserToDeleteRows = false
        Me.dgvTVShows.AllowUserToResizeRows = false
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(230,Byte),Integer), CType(CType(230,Byte),Integer), CType(CType(230,Byte),Integer))
        Me.dgvTVShows.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvTVShows.BackgroundColor = System.Drawing.Color.White
        Me.dgvTVShows.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTVShows.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvTVShows.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvTVShows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTVShows.ContextMenuStrip = Me.cmnuShow
        Me.dgvTVShows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTVShows.GridColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer))
        Me.dgvTVShows.Location = New System.Drawing.Point(0, 2)
        Me.dgvTVShows.Name = "dgvTVShows"
        Me.dgvTVShows.ReadOnly = true
        Me.dgvTVShows.RowHeadersVisible = false
        Me.dgvTVShows.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTVShows.ShowCellErrors = false
        Me.dgvTVShows.ShowRowErrors = false
        Me.dgvTVShows.Size = New System.Drawing.Size(567, 23)
        Me.dgvTVShows.StandardTab = true
        Me.dgvTVShows.TabIndex = 0
        '
        'cmnuShow
        '
        Me.cmnuShow.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuShowTitle, Me.ToolStripMenuItem2, Me.cmnuShowReload, Me.cmnuShowMark, Me.cmnuShowLock, Me.cmnuShowWatched, Me.ToolStripSeparator8, Me.cmnuShowEdit, Me.ToolStripSeparator7, Me.cmnuShowRescrape, Me.cmnuShowRefresh, Me.cmnuShowLanguage, Me.cmnuShowChange, Me.ToolStripSeparator11, Me.cmnuShowOpenFolder, Me.ToolStripSeparator20, Me.cmnuShowRemove})
        Me.cmnuShow.Name = "mnuShows"
        Me.cmnuShow.Size = New System.Drawing.Size(247, 298)
        '
        'cmnuShowTitle
        '
        Me.cmnuShowTitle.Enabled = false
        Me.cmnuShowTitle.Image = CType(resources.GetObject("cmnuShowTitle.Image"),System.Drawing.Image)
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
        Me.cmnuShowReload.Image = CType(resources.GetObject("cmnuShowReload.Image"),System.Drawing.Image)
        Me.cmnuShowReload.Name = "cmnuShowReload"
        Me.cmnuShowReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmnuShowReload.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowReload.Text = "Reload"
        '
        'cmnuShowMark
        '
        Me.cmnuShowMark.Image = CType(resources.GetObject("cmnuShowMark.Image"),System.Drawing.Image)
        Me.cmnuShowMark.Name = "cmnuShowMark"
        Me.cmnuShowMark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M),System.Windows.Forms.Keys)
        Me.cmnuShowMark.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowMark.Text = "Mark"
        '
        'cmnuShowLock
        '
        Me.cmnuShowLock.Image = CType(resources.GetObject("cmnuShowLock.Image"),System.Drawing.Image)
        Me.cmnuShowLock.Name = "cmnuShowLock"
        Me.cmnuShowLock.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L),System.Windows.Forms.Keys)
        Me.cmnuShowLock.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowLock.Text = "Lock"
        '
        'cmnuShowWatched
        '
        Me.cmnuShowWatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuShowWatched.Name = "cmnuShowWatched"
        Me.cmnuShowWatched.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W),System.Windows.Forms.Keys)
        Me.cmnuShowWatched.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowWatched.Text = "Watched"
        Me.cmnuShowWatched.Visible = false
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuShowEdit
        '
        Me.cmnuShowEdit.Image = CType(resources.GetObject("cmnuShowEdit.Image"),System.Drawing.Image)
        Me.cmnuShowEdit.Name = "cmnuShowEdit"
        Me.cmnuShowEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E),System.Windows.Forms.Keys)
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
        Me.cmnuShowRescrape.Image = CType(resources.GetObject("cmnuShowRescrape.Image"),System.Drawing.Image)
        Me.cmnuShowRescrape.Name = "cmnuShowRescrape"
        Me.cmnuShowRescrape.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I),System.Windows.Forms.Keys)
        Me.cmnuShowRescrape.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowRescrape.Text = "(Re)Scrape Show"
        '
        'cmnuShowRefresh
        '
        Me.cmnuShowRefresh.Name = "cmnuShowRefresh"
        Me.cmnuShowRefresh.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R),System.Windows.Forms.Keys)
        Me.cmnuShowRefresh.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowRefresh.Text = "Refresh Data"
        '
        'cmnuShowLanguage
        '
        Me.cmnuShowLanguage.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuShowLanguageLanguages, Me.cmnuShowLanguageSet})
        Me.cmnuShowLanguage.Name = "cmnuShowLanguage"
        Me.cmnuShowLanguage.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowLanguage.Text = "Change Language"
        '
        'cmnuShowLanguageLanguages
        '
        Me.cmnuShowLanguageLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmnuShowLanguageLanguages.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.cmnuShowLanguageLanguages.Name = "cmnuShowLanguageLanguages"
        Me.cmnuShowLanguageLanguages.Size = New System.Drawing.Size(135, 23)
        Me.cmnuShowLanguageLanguages.Sorted = true
        '
        'cmnuShowLanguageSet
        '
        Me.cmnuShowLanguageSet.Name = "cmnuShowLanguageSet"
        Me.cmnuShowLanguageSet.Size = New System.Drawing.Size(195, 22)
        Me.cmnuShowLanguageSet.Text = "Set"
        '
        'cmnuShowChange
        '
        Me.cmnuShowChange.Image = CType(resources.GetObject("cmnuShowChange.Image"),System.Drawing.Image)
        Me.cmnuShowChange.Name = "cmnuShowChange"
        Me.cmnuShowChange.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C),System.Windows.Forms.Keys)
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
        Me.cmnuShowOpenFolder.Image = CType(resources.GetObject("cmnuShowOpenFolder.Image"),System.Drawing.Image)
        Me.cmnuShowOpenFolder.Name = "cmnuShowOpenFolder"
        Me.cmnuShowOpenFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O),System.Windows.Forms.Keys)
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
        Me.cmnuShowRemove.Image = CType(resources.GetObject("cmnuShowRemove.Image"),System.Drawing.Image)
        Me.cmnuShowRemove.Name = "cmnuShowRemove"
        Me.cmnuShowRemove.Size = New System.Drawing.Size(246, 22)
        Me.cmnuShowRemove.Text = "Remove"
        '
        'cmnuShowRemoveFromDB
        '
        Me.cmnuShowRemoveFromDB.Image = CType(resources.GetObject("cmnuShowRemoveFromDB.Image"),System.Drawing.Image)
        Me.cmnuShowRemoveFromDB.Name = "cmnuShowRemoveFromDB"
        Me.cmnuShowRemoveFromDB.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.cmnuShowRemoveFromDB.Size = New System.Drawing.Size(225, 22)
        Me.cmnuShowRemoveFromDB.Text = "Remove from Database"
        '
        'cmnuShowRemoveFromDisk
        '
        Me.cmnuShowRemoveFromDisk.Image = CType(resources.GetObject("cmnuShowRemoveFromDisk.Image"),System.Drawing.Image)
        Me.cmnuShowRemoveFromDisk.Name = "cmnuShowRemoveFromDisk"
        Me.cmnuShowRemoveFromDisk.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Delete),System.Windows.Forms.Keys)
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
        Me.scTVSeasonsEpisodes.Size = New System.Drawing.Size(567, 25)
        Me.scTVSeasonsEpisodes.SplitterDistance = 25
        Me.scTVSeasonsEpisodes.TabIndex = 0
        Me.scTVSeasonsEpisodes.TabStop = false
        '
        'dgvTVSeasons
        '
        Me.dgvTVSeasons.AllowUserToAddRows = false
        Me.dgvTVSeasons.AllowUserToDeleteRows = false
        Me.dgvTVSeasons.AllowUserToResizeRows = false
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(230,Byte),Integer), CType(CType(230,Byte),Integer), CType(CType(230,Byte),Integer))
        Me.dgvTVSeasons.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvTVSeasons.BackgroundColor = System.Drawing.Color.White
        Me.dgvTVSeasons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTVSeasons.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvTVSeasons.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvTVSeasons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTVSeasons.ContextMenuStrip = Me.cmnuSeason
        Me.dgvTVSeasons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTVSeasons.GridColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer))
        Me.dgvTVSeasons.Location = New System.Drawing.Point(0, 0)
        Me.dgvTVSeasons.Name = "dgvTVSeasons"
        Me.dgvTVSeasons.ReadOnly = true
        Me.dgvTVSeasons.RowHeadersVisible = false
        Me.dgvTVSeasons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTVSeasons.ShowCellErrors = false
        Me.dgvTVSeasons.ShowRowErrors = false
        Me.dgvTVSeasons.Size = New System.Drawing.Size(567, 25)
        Me.dgvTVSeasons.StandardTab = true
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
        Me.cmnuSeasonTitle.Enabled = false
        Me.cmnuSeasonTitle.Image = CType(resources.GetObject("cmnuSeasonTitle.Image"),System.Drawing.Image)
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
        Me.cmnuSeasonReload.Image = CType(resources.GetObject("cmnuSeasonReload.Image"),System.Drawing.Image)
        Me.cmnuSeasonReload.Name = "cmnuSeasonReload"
        Me.cmnuSeasonReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmnuSeasonReload.Size = New System.Drawing.Size(246, 22)
        Me.cmnuSeasonReload.Text = "Reload"
        '
        'cmnuSeasonMark
        '
        Me.cmnuSeasonMark.Image = CType(resources.GetObject("cmnuSeasonMark.Image"),System.Drawing.Image)
        Me.cmnuSeasonMark.Name = "cmnuSeasonMark"
        Me.cmnuSeasonMark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M),System.Windows.Forms.Keys)
        Me.cmnuSeasonMark.Size = New System.Drawing.Size(246, 22)
        Me.cmnuSeasonMark.Text = "Mark"
        '
        'cmnuSeasonLock
        '
        Me.cmnuSeasonLock.Image = CType(resources.GetObject("cmnuSeasonLock.Image"),System.Drawing.Image)
        Me.cmnuSeasonLock.Name = "cmnuSeasonLock"
        Me.cmnuSeasonLock.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L),System.Windows.Forms.Keys)
        Me.cmnuSeasonLock.Size = New System.Drawing.Size(246, 22)
        Me.cmnuSeasonLock.Text = "Lock"
        '
        'cmnuSeasonWatched
        '
        Me.cmnuSeasonWatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuSeasonWatched.Name = "cmnuSeasonWatched"
        Me.cmnuSeasonWatched.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W),System.Windows.Forms.Keys)
        Me.cmnuSeasonWatched.Size = New System.Drawing.Size(246, 22)
        Me.cmnuSeasonWatched.Text = "Watched"
        Me.cmnuSeasonWatched.Visible = false
        '
        'ToolStripSeparator16
        '
        Me.ToolStripSeparator16.Name = "ToolStripSeparator16"
        Me.ToolStripSeparator16.Size = New System.Drawing.Size(243, 6)
        '
        'cmnuSeasonChangeImages
        '
        Me.cmnuSeasonChangeImages.Image = CType(resources.GetObject("cmnuSeasonChangeImages.Image"),System.Drawing.Image)
        Me.cmnuSeasonChangeImages.Name = "cmnuSeasonChangeImages"
        Me.cmnuSeasonChangeImages.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E),System.Windows.Forms.Keys)
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
        Me.cmnuSeasonRescrape.Image = CType(resources.GetObject("cmnuSeasonRescrape.Image"),System.Drawing.Image)
        Me.cmnuSeasonRescrape.Name = "cmnuSeasonRescrape"
        Me.cmnuSeasonRescrape.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I),System.Windows.Forms.Keys)
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
        Me.cmnuSeasonOpenFolder.Image = CType(resources.GetObject("cmnuSeasonOpenFolder.Image"),System.Drawing.Image)
        Me.cmnuSeasonOpenFolder.Name = "cmnuSeasonOpenFolder"
        Me.cmnuSeasonOpenFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O),System.Windows.Forms.Keys)
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
        Me.cmnuSeasonRemove.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuSeasonRemoveFromDB, Me.cmnuSeasonRemoveFromDisk})
        Me.cmnuSeasonRemove.Image = CType(resources.GetObject("cmnuSeasonRemove.Image"),System.Drawing.Image)
        Me.cmnuSeasonRemove.Name = "cmnuSeasonRemove"
        Me.cmnuSeasonRemove.Size = New System.Drawing.Size(246, 22)
        Me.cmnuSeasonRemove.Text = "Remove"
        '
        'cmnuSeasonRemoveFromDB
        '
        Me.cmnuSeasonRemoveFromDB.Image = CType(resources.GetObject("cmnuSeasonRemoveFromDB.Image"),System.Drawing.Image)
        Me.cmnuSeasonRemoveFromDB.Name = "cmnuSeasonRemoveFromDB"
        Me.cmnuSeasonRemoveFromDB.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.cmnuSeasonRemoveFromDB.Size = New System.Drawing.Size(225, 22)
        Me.cmnuSeasonRemoveFromDB.Text = "Remove from Database"
        '
        'cmnuSeasonRemoveFromDisk
        '
        Me.cmnuSeasonRemoveFromDisk.Image = CType(resources.GetObject("cmnuSeasonRemoveFromDisk.Image"),System.Drawing.Image)
        Me.cmnuSeasonRemoveFromDisk.Name = "cmnuSeasonRemoveFromDisk"
        Me.cmnuSeasonRemoveFromDisk.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Delete),System.Windows.Forms.Keys)
        Me.cmnuSeasonRemoveFromDisk.Size = New System.Drawing.Size(225, 22)
        Me.cmnuSeasonRemoveFromDisk.Text = "Delete Season"
        '
        'dgvTVEpisodes
        '
        Me.dgvTVEpisodes.AllowUserToAddRows = false
        Me.dgvTVEpisodes.AllowUserToDeleteRows = false
        Me.dgvTVEpisodes.AllowUserToResizeRows = false
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(230,Byte),Integer), CType(CType(230,Byte),Integer), CType(CType(230,Byte),Integer))
        Me.dgvTVEpisodes.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvTVEpisodes.BackgroundColor = System.Drawing.Color.White
        Me.dgvTVEpisodes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTVEpisodes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvTVEpisodes.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvTVEpisodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTVEpisodes.ContextMenuStrip = Me.cmnuEpisode
        Me.dgvTVEpisodes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTVEpisodes.GridColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer), CType(CType(240,Byte),Integer))
        Me.dgvTVEpisodes.Location = New System.Drawing.Point(0, 0)
        Me.dgvTVEpisodes.Name = "dgvTVEpisodes"
        Me.dgvTVEpisodes.ReadOnly = true
        Me.dgvTVEpisodes.RowHeadersVisible = false
        Me.dgvTVEpisodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTVEpisodes.ShowCellErrors = false
        Me.dgvTVEpisodes.ShowRowErrors = false
        Me.dgvTVEpisodes.Size = New System.Drawing.Size(567, 25)
        Me.dgvTVEpisodes.StandardTab = true
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
        Me.cmnuEpisodeTitle.Enabled = false
        Me.cmnuEpisodeTitle.Image = CType(resources.GetObject("cmnuEpisodeTitle.Image"),System.Drawing.Image)
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
        Me.cmnuEpisodeReload.Image = CType(resources.GetObject("cmnuEpisodeReload.Image"),System.Drawing.Image)
        Me.cmnuEpisodeReload.Name = "cmnuEpisodeReload"
        Me.cmnuEpisodeReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmnuEpisodeReload.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeReload.Text = "Reload"
        '
        'cmnuEpisodeMark
        '
        Me.cmnuEpisodeMark.Image = CType(resources.GetObject("cmnuEpisodeMark.Image"),System.Drawing.Image)
        Me.cmnuEpisodeMark.Name = "cmnuEpisodeMark"
        Me.cmnuEpisodeMark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M),System.Windows.Forms.Keys)
        Me.cmnuEpisodeMark.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeMark.Text = "Mark"
        '
        'cmnuEpisodeLock
        '
        Me.cmnuEpisodeLock.Image = CType(resources.GetObject("cmnuEpisodeLock.Image"),System.Drawing.Image)
        Me.cmnuEpisodeLock.Name = "cmnuEpisodeLock"
        Me.cmnuEpisodeLock.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L),System.Windows.Forms.Keys)
        Me.cmnuEpisodeLock.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeLock.Text = "Lock"
        '
        'cmnuEpisodeWatched
        '
        Me.cmnuEpisodeWatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuEpisodeWatched.Name = "cmnuEpisodeWatched"
        Me.cmnuEpisodeWatched.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W),System.Windows.Forms.Keys)
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
        Me.cmnuEpisodeEdit.Image = CType(resources.GetObject("cmnuEpisodeEdit.Image"),System.Drawing.Image)
        Me.cmnuEpisodeEdit.Name = "cmnuEpisodeEdit"
        Me.cmnuEpisodeEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E),System.Windows.Forms.Keys)
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
        Me.cmnuEpisodeRescrape.Image = CType(resources.GetObject("cmnuEpisodeRescrape.Image"),System.Drawing.Image)
        Me.cmnuEpisodeRescrape.Name = "cmnuEpisodeRescrape"
        Me.cmnuEpisodeRescrape.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I),System.Windows.Forms.Keys)
        Me.cmnuEpisodeRescrape.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeRescrape.Text = "(Re)Scrape Episode"
        '
        'cmnuEpisodeChange
        '
        Me.cmnuEpisodeChange.Image = CType(resources.GetObject("cmnuEpisodeChange.Image"),System.Drawing.Image)
        Me.cmnuEpisodeChange.Name = "cmnuEpisodeChange"
        Me.cmnuEpisodeChange.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C),System.Windows.Forms.Keys)
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
        Me.cmnuEpisodeOpenFolder.Image = CType(resources.GetObject("cmnuEpisodeOpenFolder.Image"),System.Drawing.Image)
        Me.cmnuEpisodeOpenFolder.Name = "cmnuEpisodeOpenFolder"
        Me.cmnuEpisodeOpenFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O),System.Windows.Forms.Keys)
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
        Me.cmnuEpisodeRemove.Image = CType(resources.GetObject("cmnuEpisodeRemove.Image"),System.Drawing.Image)
        Me.cmnuEpisodeRemove.Name = "cmnuEpisodeRemove"
        Me.cmnuEpisodeRemove.Size = New System.Drawing.Size(246, 22)
        Me.cmnuEpisodeRemove.Text = "Remove"
        '
        'cmnuEpisodeRemoveFromDB
        '
        Me.cmnuEpisodeRemoveFromDB.Image = CType(resources.GetObject("cmnuEpisodeRemoveFromDB.Image"),System.Drawing.Image)
        Me.cmnuEpisodeRemoveFromDB.Name = "cmnuEpisodeRemoveFromDB"
        Me.cmnuEpisodeRemoveFromDB.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.cmnuEpisodeRemoveFromDB.Size = New System.Drawing.Size(225, 22)
        Me.cmnuEpisodeRemoveFromDB.Text = "Remove from Database"
        '
        'cmnuEpisodeRemoveFromDisk
        '
        Me.cmnuEpisodeRemoveFromDisk.Image = CType(resources.GetObject("cmnuEpisodeRemoveFromDisk.Image"),System.Drawing.Image)
        Me.cmnuEpisodeRemoveFromDisk.Name = "cmnuEpisodeRemoveFromDisk"
        Me.cmnuEpisodeRemoveFromDisk.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Delete),System.Windows.Forms.Keys)
        Me.cmnuEpisodeRemoveFromDisk.Size = New System.Drawing.Size(225, 22)
        Me.cmnuEpisodeRemoveFromDisk.Text = "Delete Episode"
        '
        'pnlListTop
        '
        Me.pnlListTop.BackColor = System.Drawing.SystemColors.Control
        Me.pnlListTop.Controls.Add(Me.btnMarkAll)
        Me.pnlListTop.Controls.Add(Me.pnlSearchMovies)
        Me.pnlListTop.Controls.Add(Me.pnlSearchMovieSets)
        Me.pnlListTop.Controls.Add(Me.pnlSearchTVShows)
        Me.pnlListTop.Controls.Add(Me.tcMain)
        Me.pnlListTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlListTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlListTop.Name = "pnlListTop"
        Me.pnlListTop.Size = New System.Drawing.Size(567, 56)
        Me.pnlListTop.TabIndex = 14
        '
        'btnMarkAll
        '
        Me.btnMarkAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnMarkAll.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnMarkAll.Image = CType(resources.GetObject("btnMarkAll.Image"),System.Drawing.Image)
        Me.btnMarkAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMarkAll.Location = New System.Drawing.Point(459, 1)
        Me.btnMarkAll.Name = "btnMarkAll"
        Me.btnMarkAll.Size = New System.Drawing.Size(109, 21)
        Me.btnMarkAll.TabIndex = 1
        Me.btnMarkAll.Text = "Mark All"
        Me.btnMarkAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMarkAll.UseVisualStyleBackColor = true
        '
        'pnlSearchMovies
        '
        Me.pnlSearchMovies.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pnlSearchMovies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearchMovies.Controls.Add(Me.cbSearchMovies)
        Me.pnlSearchMovies.Controls.Add(Me.picSearchMovies)
        Me.pnlSearchMovies.Controls.Add(Me.txtSearchMovies)
        Me.pnlSearchMovies.Location = New System.Drawing.Point(0, 25)
        Me.pnlSearchMovies.Name = "pnlSearchMovies"
        Me.pnlSearchMovies.Size = New System.Drawing.Size(567, 35)
        Me.pnlSearchMovies.TabIndex = 0
        '
        'cbSearchMovies
        '
        Me.cbSearchMovies.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbSearchMovies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearchMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbSearchMovies.FormattingEnabled = true
        Me.cbSearchMovies.Location = New System.Drawing.Point(437, 5)
        Me.cbSearchMovies.Name = "cbSearchMovies"
        Me.cbSearchMovies.Size = New System.Drawing.Size(100, 21)
        Me.cbSearchMovies.TabIndex = 1
        '
        'picSearchMovies
        '
        Me.picSearchMovies.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.picSearchMovies.Image = CType(resources.GetObject("picSearchMovies.Image"),System.Drawing.Image)
        Me.picSearchMovies.Location = New System.Drawing.Point(543, 8)
        Me.picSearchMovies.Name = "picSearchMovies"
        Me.picSearchMovies.Size = New System.Drawing.Size(16, 16)
        Me.picSearchMovies.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picSearchMovies.TabIndex = 1
        Me.picSearchMovies.TabStop = false
        '
        'txtSearchMovies
        '
        Me.txtSearchMovies.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtSearchMovies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.txtSearchMovies.Location = New System.Drawing.Point(7, 4)
        Me.txtSearchMovies.Name = "txtSearchMovies"
        Me.txtSearchMovies.Size = New System.Drawing.Size(424, 22)
        Me.txtSearchMovies.TabIndex = 0
        '
        'pnlSearchMovieSets
        '
        Me.pnlSearchMovieSets.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pnlSearchMovieSets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearchMovieSets.Controls.Add(Me.cbSearchMovieSets)
        Me.pnlSearchMovieSets.Controls.Add(Me.picSearchMovieSets)
        Me.pnlSearchMovieSets.Controls.Add(Me.txtSearchMovieSets)
        Me.pnlSearchMovieSets.Location = New System.Drawing.Point(0, 25)
        Me.pnlSearchMovieSets.Name = "pnlSearchMovieSets"
        Me.pnlSearchMovieSets.Size = New System.Drawing.Size(567, 35)
        Me.pnlSearchMovieSets.TabIndex = 26
        '
        'cbSearchMovieSets
        '
        Me.cbSearchMovieSets.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbSearchMovieSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearchMovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbSearchMovieSets.FormattingEnabled = true
        Me.cbSearchMovieSets.Location = New System.Drawing.Point(437, 5)
        Me.cbSearchMovieSets.Name = "cbSearchMovieSets"
        Me.cbSearchMovieSets.Size = New System.Drawing.Size(100, 21)
        Me.cbSearchMovieSets.TabIndex = 1
        '
        'picSearchMovieSets
        '
        Me.picSearchMovieSets.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.picSearchMovieSets.Image = CType(resources.GetObject("picSearchMovieSets.Image"),System.Drawing.Image)
        Me.picSearchMovieSets.Location = New System.Drawing.Point(543, 8)
        Me.picSearchMovieSets.Name = "picSearchMovieSets"
        Me.picSearchMovieSets.Size = New System.Drawing.Size(16, 16)
        Me.picSearchMovieSets.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picSearchMovieSets.TabIndex = 1
        Me.picSearchMovieSets.TabStop = false
        '
        'txtSearchMovieSets
        '
        Me.txtSearchMovieSets.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtSearchMovieSets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchMovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.txtSearchMovieSets.Location = New System.Drawing.Point(7, 4)
        Me.txtSearchMovieSets.Name = "txtSearchMovieSets"
        Me.txtSearchMovieSets.Size = New System.Drawing.Size(424, 22)
        Me.txtSearchMovieSets.TabIndex = 0
        '
        'pnlSearchTVShows
        '
        Me.pnlSearchTVShows.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pnlSearchTVShows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSearchTVShows.Controls.Add(Me.cbSearchShows)
        Me.pnlSearchTVShows.Controls.Add(Me.picSearchTVShows)
        Me.pnlSearchTVShows.Controls.Add(Me.txtSearchShows)
        Me.pnlSearchTVShows.Location = New System.Drawing.Point(0, 25)
        Me.pnlSearchTVShows.Name = "pnlSearchTVShows"
        Me.pnlSearchTVShows.Size = New System.Drawing.Size(567, 35)
        Me.pnlSearchTVShows.TabIndex = 26
        '
        'cbSearchShows
        '
        Me.cbSearchShows.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbSearchShows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearchShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbSearchShows.FormattingEnabled = true
        Me.cbSearchShows.Location = New System.Drawing.Point(437, 5)
        Me.cbSearchShows.Name = "cbSearchShows"
        Me.cbSearchShows.Size = New System.Drawing.Size(100, 21)
        Me.cbSearchShows.TabIndex = 1
        '
        'picSearchTVShows
        '
        Me.picSearchTVShows.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.picSearchTVShows.Image = CType(resources.GetObject("picSearchTVShows.Image"),System.Drawing.Image)
        Me.picSearchTVShows.Location = New System.Drawing.Point(543, 8)
        Me.picSearchTVShows.Name = "picSearchTVShows"
        Me.picSearchTVShows.Size = New System.Drawing.Size(16, 16)
        Me.picSearchTVShows.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picSearchTVShows.TabIndex = 1
        Me.picSearchTVShows.TabStop = false
        '
        'txtSearchShows
        '
        Me.txtSearchShows.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtSearchShows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.txtSearchShows.Location = New System.Drawing.Point(7, 4)
        Me.txtSearchShows.Name = "txtSearchShows"
        Me.txtSearchShows.Size = New System.Drawing.Size(424, 22)
        Me.txtSearchShows.TabIndex = 0
        '
        'tcMain
        '
        Me.tcMain.Controls.Add(Me.tpMovies)
        Me.tcMain.Controls.Add(Me.tpMovieSets)
        Me.tcMain.Controls.Add(Me.tpTVShows)
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.tcMain.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.tcMain.ItemSize = New System.Drawing.Size(50, 19)
        Me.tcMain.Location = New System.Drawing.Point(0, 0)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(567, 21)
        Me.tcMain.TabIndex = 0
        Me.tcMain.TabStop = false
        '
        'tpMovies
        '
        Me.tpMovies.Location = New System.Drawing.Point(4, 23)
        Me.tpMovies.Name = "tpMovies"
        Me.tpMovies.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMovies.Size = New System.Drawing.Size(559, 0)
        Me.tpMovies.TabIndex = 0
        Me.tpMovies.Tag = ""
        Me.tpMovies.Text = "Movies"
        Me.tpMovies.UseVisualStyleBackColor = true
        '
        'tpMovieSets
        '
        Me.tpMovieSets.Location = New System.Drawing.Point(4, 23)
        Me.tpMovieSets.Name = "tpMovieSets"
        Me.tpMovieSets.Size = New System.Drawing.Size(559, 0)
        Me.tpMovieSets.TabIndex = 2
        Me.tpMovieSets.Tag = ""
        Me.tpMovieSets.Text = "Sets"
        Me.tpMovieSets.UseVisualStyleBackColor = true
        '
        'tpTVShows
        '
        Me.tpTVShows.Location = New System.Drawing.Point(4, 23)
        Me.tpTVShows.Name = "tpTVShows"
        Me.tpTVShows.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTVShows.Size = New System.Drawing.Size(559, 0)
        Me.tpTVShows.TabIndex = 1
        Me.tpTVShows.Tag = ""
        Me.tpTVShows.Text = "TV Shows"
        Me.tpTVShows.UseVisualStyleBackColor = true
        '
        'pnlFilter_Movies
        '
        Me.pnlFilter_Movies.AutoSize = true
        Me.pnlFilter_Movies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilter_Movies.Controls.Add(Me.tblFilter_Movies)
        Me.pnlFilter_Movies.Controls.Add(Me.pnlFilterTop_Movies)
        Me.pnlFilter_Movies.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFilter_Movies.Location = New System.Drawing.Point(0, 52)
        Me.pnlFilter_Movies.Name = "pnlFilter_Movies"
        Me.pnlFilter_Movies.Size = New System.Drawing.Size(567, 295)
        Me.pnlFilter_Movies.TabIndex = 12
        Me.pnlFilter_Movies.Visible = false
        '
        'tblFilter_Movies
        '
        Me.tblFilter_Movies.AutoScroll = true
        Me.tblFilter_Movies.AutoSize = true
        Me.tblFilter_Movies.ColumnCount = 3
        Me.tblFilter_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilter_Movies.Controls.Add(Me.gbFilterGeneral_Movies, 0, 0)
        Me.tblFilter_Movies.Controls.Add(Me.gbFilterSorting_Movies, 0, 1)
        Me.tblFilter_Movies.Controls.Add(Me.btnClearFilters_Movies, 0, 2)
        Me.tblFilter_Movies.Controls.Add(Me.gbFilterSpecific_Movies, 1, 0)
        Me.tblFilter_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilter_Movies.Location = New System.Drawing.Point(0, 22)
        Me.tblFilter_Movies.Name = "tblFilter_Movies"
        Me.tblFilter_Movies.RowCount = 4
        Me.tblFilter_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Movies.Size = New System.Drawing.Size(565, 271)
        Me.tblFilter_Movies.TabIndex = 8
        '
        'gbFilterGeneral_Movies
        '
        Me.gbFilterGeneral_Movies.AutoSize = true
        Me.gbFilterGeneral_Movies.Controls.Add(Me.tblFilterGeneral_Movies)
        Me.gbFilterGeneral_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterGeneral_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbFilterGeneral_Movies.Location = New System.Drawing.Point(3, 3)
        Me.gbFilterGeneral_Movies.Name = "gbFilterGeneral_Movies"
        Me.gbFilterGeneral_Movies.Size = New System.Drawing.Size(124, 90)
        Me.gbFilterGeneral_Movies.TabIndex = 3
        Me.gbFilterGeneral_Movies.TabStop = false
        Me.gbFilterGeneral_Movies.Text = "General"
        '
        'tblFilterGeneral_Movies
        '
        Me.tblFilterGeneral_Movies.AutoSize = true
        Me.tblFilterGeneral_Movies.ColumnCount = 2
        Me.tblFilterGeneral_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGeneral_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGeneral_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterGeneral_Movies.Controls.Add(Me.chkFilterTolerance_Movies, 0, 2)
        Me.tblFilterGeneral_Movies.Controls.Add(Me.chkFilterDuplicates_Movies, 0, 1)
        Me.tblFilterGeneral_Movies.Controls.Add(Me.chkFilterMissing_Movies, 0, 0)
        Me.tblFilterGeneral_Movies.Controls.Add(Me.btnFilterMissing_Movies, 1, 0)
        Me.tblFilterGeneral_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterGeneral_Movies.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterGeneral_Movies.Name = "tblFilterGeneral_Movies"
        Me.tblFilterGeneral_Movies.RowCount = 4
        Me.tblFilterGeneral_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_Movies.Size = New System.Drawing.Size(118, 69)
        Me.tblFilterGeneral_Movies.TabIndex = 40
        '
        'chkFilterTolerance_Movies
        '
        Me.chkFilterTolerance_Movies.AutoSize = true
        Me.tblFilterGeneral_Movies.SetColumnSpan(Me.chkFilterTolerance_Movies, 2)
        Me.chkFilterTolerance_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterTolerance_Movies.Location = New System.Drawing.Point(3, 49)
        Me.chkFilterTolerance_Movies.Name = "chkFilterTolerance_Movies"
        Me.chkFilterTolerance_Movies.Size = New System.Drawing.Size(112, 17)
        Me.chkFilterTolerance_Movies.TabIndex = 2
        Me.chkFilterTolerance_Movies.Text = "Out of Tolerance"
        Me.chkFilterTolerance_Movies.UseVisualStyleBackColor = true
        '
        'chkFilterDuplicates_Movies
        '
        Me.chkFilterDuplicates_Movies.AutoSize = true
        Me.tblFilterGeneral_Movies.SetColumnSpan(Me.chkFilterDuplicates_Movies, 2)
        Me.chkFilterDuplicates_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterDuplicates_Movies.Location = New System.Drawing.Point(3, 26)
        Me.chkFilterDuplicates_Movies.Name = "chkFilterDuplicates_Movies"
        Me.chkFilterDuplicates_Movies.Size = New System.Drawing.Size(80, 17)
        Me.chkFilterDuplicates_Movies.TabIndex = 0
        Me.chkFilterDuplicates_Movies.Text = "Duplicates"
        Me.chkFilterDuplicates_Movies.UseVisualStyleBackColor = true
        '
        'chkFilterMissing_Movies
        '
        Me.chkFilterMissing_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMissing_Movies.AutoSize = true
        Me.chkFilterMissing_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterMissing_Movies.Location = New System.Drawing.Point(3, 4)
        Me.chkFilterMissing_Movies.Name = "chkFilterMissing_Movies"
        Me.chkFilterMissing_Movies.Size = New System.Drawing.Size(15, 14)
        Me.chkFilterMissing_Movies.TabIndex = 1
        Me.chkFilterMissing_Movies.UseVisualStyleBackColor = true
        '
        'btnFilterMissing_Movies
        '
        Me.btnFilterMissing_Movies.AutoSize = true
        Me.btnFilterMissing_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterMissing_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnFilterMissing_Movies.Location = New System.Drawing.Point(21, 0)
        Me.btnFilterMissing_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterMissing_Movies.Name = "btnFilterMissing_Movies"
        Me.btnFilterMissing_Movies.Size = New System.Drawing.Size(97, 23)
        Me.btnFilterMissing_Movies.TabIndex = 3
        Me.btnFilterMissing_Movies.Text = "Missing Items"
        Me.btnFilterMissing_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterMissing_Movies.UseVisualStyleBackColor = true
        '
        'gbFilterSorting_Movies
        '
        Me.gbFilterSorting_Movies.AutoSize = true
        Me.gbFilterSorting_Movies.Controls.Add(Me.tblFilterSorting_Movies)
        Me.gbFilterSorting_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterSorting_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbFilterSorting_Movies.Location = New System.Drawing.Point(3, 99)
        Me.gbFilterSorting_Movies.Name = "gbFilterSorting_Movies"
        Me.gbFilterSorting_Movies.Size = New System.Drawing.Size(124, 136)
        Me.gbFilterSorting_Movies.TabIndex = 4
        Me.gbFilterSorting_Movies.TabStop = false
        Me.gbFilterSorting_Movies.Text = "Extra Sorting"
        '
        'tblFilterSorting_Movies
        '
        Me.tblFilterSorting_Movies.AutoSize = true
        Me.tblFilterSorting_Movies.ColumnCount = 1
        Me.tblFilterSorting_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSorting_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterSorting_Movies.Controls.Add(Me.btnFilterSortYear_Movies, 0, 4)
        Me.tblFilterSorting_Movies.Controls.Add(Me.btnFilterSortRating_Movies, 0, 3)
        Me.tblFilterSorting_Movies.Controls.Add(Me.btnFilterSortDateAdded_Movies, 0, 0)
        Me.tblFilterSorting_Movies.Controls.Add(Me.btnFilterSortTitle_Movies, 0, 2)
        Me.tblFilterSorting_Movies.Controls.Add(Me.btnFilterSortDateModified_Movies, 0, 1)
        Me.tblFilterSorting_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterSorting_Movies.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterSorting_Movies.Name = "tblFilterSorting_Movies"
        Me.tblFilterSorting_Movies.RowCount = 6
        Me.tblFilterSorting_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSorting_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSorting_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSorting_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSorting_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSorting_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSorting_Movies.Size = New System.Drawing.Size(118, 115)
        Me.tblFilterSorting_Movies.TabIndex = 8
        '
        'btnFilterSortYear_Movies
        '
        Me.btnFilterSortYear_Movies.AutoSize = true
        Me.btnFilterSortYear_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterSortYear_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnFilterSortYear_Movies.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFilterSortYear_Movies.Location = New System.Drawing.Point(0, 92)
        Me.btnFilterSortYear_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterSortYear_Movies.Name = "btnFilterSortYear_Movies"
        Me.btnFilterSortYear_Movies.Size = New System.Drawing.Size(118, 23)
        Me.btnFilterSortYear_Movies.TabIndex = 3
        Me.btnFilterSortYear_Movies.Text = "Year"
        Me.btnFilterSortYear_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterSortYear_Movies.UseVisualStyleBackColor = true
        '
        'btnFilterSortRating_Movies
        '
        Me.btnFilterSortRating_Movies.AutoSize = true
        Me.btnFilterSortRating_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterSortRating_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnFilterSortRating_Movies.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFilterSortRating_Movies.Location = New System.Drawing.Point(0, 69)
        Me.btnFilterSortRating_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterSortRating_Movies.Name = "btnFilterSortRating_Movies"
        Me.btnFilterSortRating_Movies.Size = New System.Drawing.Size(118, 23)
        Me.btnFilterSortRating_Movies.TabIndex = 2
        Me.btnFilterSortRating_Movies.Text = "Rating"
        Me.btnFilterSortRating_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterSortRating_Movies.UseVisualStyleBackColor = true
        '
        'btnFilterSortDateAdded_Movies
        '
        Me.btnFilterSortDateAdded_Movies.AutoSize = true
        Me.btnFilterSortDateAdded_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterSortDateAdded_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnFilterSortDateAdded_Movies.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFilterSortDateAdded_Movies.Location = New System.Drawing.Point(0, 0)
        Me.btnFilterSortDateAdded_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterSortDateAdded_Movies.Name = "btnFilterSortDateAdded_Movies"
        Me.btnFilterSortDateAdded_Movies.Size = New System.Drawing.Size(118, 23)
        Me.btnFilterSortDateAdded_Movies.TabIndex = 0
        Me.btnFilterSortDateAdded_Movies.Text = "Date Added"
        Me.btnFilterSortDateAdded_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterSortDateAdded_Movies.UseVisualStyleBackColor = true
        '
        'btnFilterSortTitle_Movies
        '
        Me.btnFilterSortTitle_Movies.AutoSize = true
        Me.btnFilterSortTitle_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterSortTitle_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnFilterSortTitle_Movies.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFilterSortTitle_Movies.Location = New System.Drawing.Point(0, 46)
        Me.btnFilterSortTitle_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterSortTitle_Movies.Name = "btnFilterSortTitle_Movies"
        Me.btnFilterSortTitle_Movies.Size = New System.Drawing.Size(118, 23)
        Me.btnFilterSortTitle_Movies.TabIndex = 1
        Me.btnFilterSortTitle_Movies.Text = "Sort Title"
        Me.btnFilterSortTitle_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterSortTitle_Movies.UseVisualStyleBackColor = true
        '
        'btnFilterSortDateModified_Movies
        '
        Me.btnFilterSortDateModified_Movies.AutoSize = true
        Me.btnFilterSortDateModified_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterSortDateModified_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnFilterSortDateModified_Movies.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFilterSortDateModified_Movies.Location = New System.Drawing.Point(0, 23)
        Me.btnFilterSortDateModified_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterSortDateModified_Movies.Name = "btnFilterSortDateModified_Movies"
        Me.btnFilterSortDateModified_Movies.Size = New System.Drawing.Size(118, 23)
        Me.btnFilterSortDateModified_Movies.TabIndex = 1
        Me.btnFilterSortDateModified_Movies.Text = "Date Modified"
        Me.btnFilterSortDateModified_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterSortDateModified_Movies.UseVisualStyleBackColor = true
        '
        'btnClearFilters_Movies
        '
        Me.btnClearFilters_Movies.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnClearFilters_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnClearFilters_Movies.Image = CType(resources.GetObject("btnClearFilters_Movies.Image"),System.Drawing.Image)
        Me.btnClearFilters_Movies.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearFilters_Movies.Location = New System.Drawing.Point(19, 241)
        Me.btnClearFilters_Movies.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.btnClearFilters_Movies.Name = "btnClearFilters_Movies"
        Me.btnClearFilters_Movies.Size = New System.Drawing.Size(92, 20)
        Me.btnClearFilters_Movies.TabIndex = 5
        Me.btnClearFilters_Movies.Text = "Clear Filters"
        Me.btnClearFilters_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClearFilters_Movies.UseVisualStyleBackColor = true
        '
        'gbFilterSpecific_Movies
        '
        Me.gbFilterSpecific_Movies.AutoSize = true
        Me.gbFilterSpecific_Movies.Controls.Add(Me.tblFilterSpecific_Movies)
        Me.gbFilterSpecific_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterSpecific_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbFilterSpecific_Movies.Location = New System.Drawing.Point(133, 3)
        Me.gbFilterSpecific_Movies.Name = "gbFilterSpecific_Movies"
        Me.tblFilter_Movies.SetRowSpan(Me.gbFilterSpecific_Movies, 3)
        Me.gbFilterSpecific_Movies.Size = New System.Drawing.Size(405, 265)
        Me.gbFilterSpecific_Movies.TabIndex = 6
        Me.gbFilterSpecific_Movies.TabStop = false
        Me.gbFilterSpecific_Movies.Text = "Specific"
        '
        'tblFilterSpecific_Movies
        '
        Me.tblFilterSpecific_Movies.AutoSize = true
        Me.tblFilterSpecific_Movies.ColumnCount = 3
        Me.tblFilterSpecific_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecific_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecific_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecific_Movies.Controls.Add(Me.gbFilterModifier_Movies, 0, 0)
        Me.tblFilterSpecific_Movies.Controls.Add(Me.tblFilterSpecificData_Movies, 1, 0)
        Me.tblFilterSpecific_Movies.Controls.Add(Me.chkFilterNew_Movies, 0, 1)
        Me.tblFilterSpecific_Movies.Controls.Add(Me.chkFilterMark_Movies, 0, 2)
        Me.tblFilterSpecific_Movies.Controls.Add(Me.chkFilterMarkCustom1_Movies, 0, 3)
        Me.tblFilterSpecific_Movies.Controls.Add(Me.chkFilterMarkCustom4_Movies, 0, 6)
        Me.tblFilterSpecific_Movies.Controls.Add(Me.chkFilterMarkCustom2_Movies, 0, 4)
        Me.tblFilterSpecific_Movies.Controls.Add(Me.chkFilterMarkCustom3_Movies, 0, 5)
        Me.tblFilterSpecific_Movies.Controls.Add(Me.chkFilterLock_Movies, 0, 7)
        Me.tblFilterSpecific_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterSpecific_Movies.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterSpecific_Movies.Name = "tblFilterSpecific_Movies"
        Me.tblFilterSpecific_Movies.RowCount = 8
        Me.tblFilterSpecific_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Movies.Size = New System.Drawing.Size(399, 244)
        Me.tblFilterSpecific_Movies.TabIndex = 40
        '
        'gbFilterModifier_Movies
        '
        Me.gbFilterModifier_Movies.AutoSize = true
        Me.gbFilterModifier_Movies.Controls.Add(Me.tblFilterModifier_Movies)
        Me.gbFilterModifier_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterModifier_Movies.Location = New System.Drawing.Point(3, 3)
        Me.gbFilterModifier_Movies.Name = "gbFilterModifier_Movies"
        Me.gbFilterModifier_Movies.Size = New System.Drawing.Size(102, 44)
        Me.gbFilterModifier_Movies.TabIndex = 3
        Me.gbFilterModifier_Movies.TabStop = false
        Me.gbFilterModifier_Movies.Text = "Modifier"
        '
        'tblFilterModifier_Movies
        '
        Me.tblFilterModifier_Movies.AutoSize = true
        Me.tblFilterModifier_Movies.ColumnCount = 3
        Me.tblFilterModifier_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterModifier_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterModifier_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterModifier_Movies.Controls.Add(Me.rbFilterOr_Movies, 1, 0)
        Me.tblFilterModifier_Movies.Controls.Add(Me.rbFilterAnd_Movies, 0, 0)
        Me.tblFilterModifier_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterModifier_Movies.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterModifier_Movies.Name = "tblFilterModifier_Movies"
        Me.tblFilterModifier_Movies.RowCount = 2
        Me.tblFilterModifier_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterModifier_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterModifier_Movies.Size = New System.Drawing.Size(96, 23)
        Me.tblFilterModifier_Movies.TabIndex = 40
        '
        'rbFilterOr_Movies
        '
        Me.rbFilterOr_Movies.AutoSize = true
        Me.rbFilterOr_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbFilterOr_Movies.Location = New System.Drawing.Point(55, 3)
        Me.rbFilterOr_Movies.Name = "rbFilterOr_Movies"
        Me.rbFilterOr_Movies.Size = New System.Drawing.Size(38, 17)
        Me.rbFilterOr_Movies.TabIndex = 1
        Me.rbFilterOr_Movies.Text = "Or"
        Me.rbFilterOr_Movies.UseVisualStyleBackColor = true
        '
        'rbFilterAnd_Movies
        '
        Me.rbFilterAnd_Movies.AutoSize = true
        Me.rbFilterAnd_Movies.Checked = true
        Me.rbFilterAnd_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbFilterAnd_Movies.Location = New System.Drawing.Point(3, 3)
        Me.rbFilterAnd_Movies.Name = "rbFilterAnd_Movies"
        Me.rbFilterAnd_Movies.Size = New System.Drawing.Size(46, 17)
        Me.rbFilterAnd_Movies.TabIndex = 0
        Me.rbFilterAnd_Movies.TabStop = true
        Me.rbFilterAnd_Movies.Text = "And"
        Me.rbFilterAnd_Movies.UseVisualStyleBackColor = true
        '
        'tblFilterSpecificData_Movies
        '
        Me.tblFilterSpecificData_Movies.AutoSize = true
        Me.tblFilterSpecificData_Movies.ColumnCount = 4
        Me.tblFilterSpecificData_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecificData_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecificData_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecificData_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecificData_Movies.Controls.Add(Me.gbFilterDataField_Movies, 0, 6)
        Me.tblFilterSpecificData_Movies.Controls.Add(Me.lblFilterGenre_Movies, 0, 0)
        Me.tblFilterSpecificData_Movies.Controls.Add(Me.txtFilterGenre_Movies, 1, 0)
        Me.tblFilterSpecificData_Movies.Controls.Add(Me.lblFilterCountry_Movies, 0, 1)
        Me.tblFilterSpecificData_Movies.Controls.Add(Me.txtFilterCountry_Movies, 1, 1)
        Me.tblFilterSpecificData_Movies.Controls.Add(Me.lblFilterVideoSource_Movies, 0, 2)
        Me.tblFilterSpecificData_Movies.Controls.Add(Me.cbFilterVideoSource_Movies, 1, 2)
        Me.tblFilterSpecificData_Movies.Controls.Add(Me.lblFilterSource_Movies, 0, 5)
        Me.tblFilterSpecificData_Movies.Controls.Add(Me.txtFilterSource_Movies, 1, 5)
        Me.tblFilterSpecificData_Movies.Controls.Add(Me.cbFilterYearModFrom_Movies, 1, 3)
        Me.tblFilterSpecificData_Movies.Controls.Add(Me.lblFilterYear_Movies, 0, 3)
        Me.tblFilterSpecificData_Movies.Controls.Add(Me.cbFilterYearModTo_Movies, 1, 4)
        Me.tblFilterSpecificData_Movies.Controls.Add(Me.cbFilterYearFrom_Movies, 2, 3)
        Me.tblFilterSpecificData_Movies.Controls.Add(Me.cbFilterYearTo_Movies, 2, 4)
        Me.tblFilterSpecificData_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterSpecificData_Movies.Location = New System.Drawing.Point(111, 3)
        Me.tblFilterSpecificData_Movies.Name = "tblFilterSpecificData_Movies"
        Me.tblFilterSpecificData_Movies.RowCount = 8
        Me.tblFilterSpecific_Movies.SetRowSpan(Me.tblFilterSpecificData_Movies, 9)
        Me.tblFilterSpecificData_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecificData_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecificData_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecificData_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecificData_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecificData_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecificData_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecificData_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecificData_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterSpecificData_Movies.Size = New System.Drawing.Size(285, 238)
        Me.tblFilterSpecificData_Movies.TabIndex = 7
        '
        'gbFilterDataField_Movies
        '
        Me.gbFilterDataField_Movies.AutoSize = true
        Me.tblFilterSpecificData_Movies.SetColumnSpan(Me.gbFilterDataField_Movies, 3)
        Me.gbFilterDataField_Movies.Controls.Add(Me.tblFilterDataField_Movies)
        Me.gbFilterDataField_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbFilterDataField_Movies.Location = New System.Drawing.Point(3, 168)
        Me.gbFilterDataField_Movies.Name = "gbFilterDataField_Movies"
        Me.gbFilterDataField_Movies.Size = New System.Drawing.Size(279, 49)
        Me.gbFilterDataField_Movies.TabIndex = 39
        Me.gbFilterDataField_Movies.TabStop = false
        Me.gbFilterDataField_Movies.Text = "Data Field:"
        '
        'tblFilterDataField_Movies
        '
        Me.tblFilterDataField_Movies.AutoSize = true
        Me.tblFilterDataField_Movies.ColumnCount = 3
        Me.tblFilterDataField_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterDataField_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterDataField_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterDataField_Movies.Controls.Add(Me.txtFilterDataField_Movies, 1, 0)
        Me.tblFilterDataField_Movies.Controls.Add(Me.cbFilterDataField_Movies, 0, 0)
        Me.tblFilterDataField_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterDataField_Movies.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterDataField_Movies.Name = "tblFilterDataField_Movies"
        Me.tblFilterDataField_Movies.RowCount = 2
        Me.tblFilterDataField_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterDataField_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterDataField_Movies.Size = New System.Drawing.Size(273, 28)
        Me.tblFilterDataField_Movies.TabIndex = 41
        '
        'txtFilterDataField_Movies
        '
        Me.txtFilterDataField_Movies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilterDataField_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFilterDataField_Movies.Location = New System.Drawing.Point(110, 3)
        Me.txtFilterDataField_Movies.Name = "txtFilterDataField_Movies"
        Me.txtFilterDataField_Movies.ReadOnly = true
        Me.txtFilterDataField_Movies.Size = New System.Drawing.Size(160, 22)
        Me.txtFilterDataField_Movies.TabIndex = 38
        '
        'cbFilterDataField_Movies
        '
        Me.cbFilterDataField_Movies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterDataField_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbFilterDataField_Movies.FormattingEnabled = true
        Me.cbFilterDataField_Movies.Location = New System.Drawing.Point(3, 3)
        Me.cbFilterDataField_Movies.Name = "cbFilterDataField_Movies"
        Me.cbFilterDataField_Movies.Size = New System.Drawing.Size(101, 21)
        Me.cbFilterDataField_Movies.TabIndex = 39
        '
        'lblFilterGenre_Movies
        '
        Me.lblFilterGenre_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterGenre_Movies.AutoSize = true
        Me.lblFilterGenre_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterGenre_Movies.Location = New System.Drawing.Point(3, 7)
        Me.lblFilterGenre_Movies.Name = "lblFilterGenre_Movies"
        Me.lblFilterGenre_Movies.Size = New System.Drawing.Size(41, 13)
        Me.lblFilterGenre_Movies.TabIndex = 31
        Me.lblFilterGenre_Movies.Text = "Genre:"
        '
        'txtFilterGenre_Movies
        '
        Me.txtFilterGenre_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFilterGenre_Movies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tblFilterSpecificData_Movies.SetColumnSpan(Me.txtFilterGenre_Movies, 2)
        Me.txtFilterGenre_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFilterGenre_Movies.Location = New System.Drawing.Point(87, 3)
        Me.txtFilterGenre_Movies.Name = "txtFilterGenre_Movies"
        Me.txtFilterGenre_Movies.ReadOnly = true
        Me.txtFilterGenre_Movies.Size = New System.Drawing.Size(189, 22)
        Me.txtFilterGenre_Movies.TabIndex = 4
        '
        'lblFilterCountry_Movies
        '
        Me.lblFilterCountry_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterCountry_Movies.AutoSize = true
        Me.lblFilterCountry_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterCountry_Movies.Location = New System.Drawing.Point(3, 35)
        Me.lblFilterCountry_Movies.Name = "lblFilterCountry_Movies"
        Me.lblFilterCountry_Movies.Size = New System.Drawing.Size(51, 13)
        Me.lblFilterCountry_Movies.TabIndex = 37
        Me.lblFilterCountry_Movies.Text = "Country:"
        '
        'txtFilterCountry_Movies
        '
        Me.txtFilterCountry_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFilterCountry_Movies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tblFilterSpecificData_Movies.SetColumnSpan(Me.txtFilterCountry_Movies, 2)
        Me.txtFilterCountry_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFilterCountry_Movies.Location = New System.Drawing.Point(87, 31)
        Me.txtFilterCountry_Movies.Name = "txtFilterCountry_Movies"
        Me.txtFilterCountry_Movies.ReadOnly = true
        Me.txtFilterCountry_Movies.Size = New System.Drawing.Size(189, 22)
        Me.txtFilterCountry_Movies.TabIndex = 36
        '
        'lblFilterVideoSource_Movies
        '
        Me.lblFilterVideoSource_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterVideoSource_Movies.AutoSize = true
        Me.lblFilterVideoSource_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterVideoSource_Movies.Location = New System.Drawing.Point(3, 63)
        Me.lblFilterVideoSource_Movies.Name = "lblFilterVideoSource_Movies"
        Me.lblFilterVideoSource_Movies.Size = New System.Drawing.Size(78, 13)
        Me.lblFilterVideoSource_Movies.TabIndex = 8
        Me.lblFilterVideoSource_Movies.Text = "Video Source:"
        '
        'cbFilterVideoSource_Movies
        '
        Me.cbFilterVideoSource_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblFilterSpecificData_Movies.SetColumnSpan(Me.cbFilterVideoSource_Movies, 2)
        Me.cbFilterVideoSource_Movies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterVideoSource_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbFilterVideoSource_Movies.FormattingEnabled = true
        Me.cbFilterVideoSource_Movies.Location = New System.Drawing.Point(87, 59)
        Me.cbFilterVideoSource_Movies.Name = "cbFilterVideoSource_Movies"
        Me.cbFilterVideoSource_Movies.Size = New System.Drawing.Size(189, 21)
        Me.cbFilterVideoSource_Movies.TabIndex = 9
        '
        'lblFilterSource_Movies
        '
        Me.lblFilterSource_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterSource_Movies.AutoSize = true
        Me.lblFilterSource_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterSource_Movies.Location = New System.Drawing.Point(3, 144)
        Me.lblFilterSource_Movies.Name = "lblFilterSource_Movies"
        Me.lblFilterSource_Movies.Size = New System.Drawing.Size(45, 13)
        Me.lblFilterSource_Movies.TabIndex = 10
        Me.lblFilterSource_Movies.Text = "Source:"
        '
        'txtFilterSource_Movies
        '
        Me.txtFilterSource_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFilterSource_Movies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tblFilterSpecificData_Movies.SetColumnSpan(Me.txtFilterSource_Movies, 2)
        Me.txtFilterSource_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFilterSource_Movies.Location = New System.Drawing.Point(87, 140)
        Me.txtFilterSource_Movies.Name = "txtFilterSource_Movies"
        Me.txtFilterSource_Movies.ReadOnly = true
        Me.txtFilterSource_Movies.Size = New System.Drawing.Size(189, 22)
        Me.txtFilterSource_Movies.TabIndex = 11
        '
        'cbFilterYearModFrom_Movies
        '
        Me.cbFilterYearModFrom_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbFilterYearModFrom_Movies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterYearModFrom_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbFilterYearModFrom_Movies.FormattingEnabled = true
        Me.cbFilterYearModFrom_Movies.Items.AddRange(New Object() {"=", "<>", ">=", ">", "<=", "<"})
        Me.cbFilterYearModFrom_Movies.Location = New System.Drawing.Point(87, 86)
        Me.cbFilterYearModFrom_Movies.Name = "cbFilterYearModFrom_Movies"
        Me.cbFilterYearModFrom_Movies.Size = New System.Drawing.Size(70, 21)
        Me.cbFilterYearModFrom_Movies.TabIndex = 6
        '
        'lblFilterYear_Movies
        '
        Me.lblFilterYear_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterYear_Movies.AutoSize = true
        Me.lblFilterYear_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterYear_Movies.Location = New System.Drawing.Point(3, 103)
        Me.lblFilterYear_Movies.Name = "lblFilterYear_Movies"
        Me.tblFilterSpecificData_Movies.SetRowSpan(Me.lblFilterYear_Movies, 2)
        Me.lblFilterYear_Movies.Size = New System.Drawing.Size(31, 13)
        Me.lblFilterYear_Movies.TabIndex = 5
        Me.lblFilterYear_Movies.Text = "Year:"
        '
        'cbFilterYearModTo_Movies
        '
        Me.cbFilterYearModTo_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbFilterYearModTo_Movies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterYearModTo_Movies.Enabled = false
        Me.cbFilterYearModTo_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbFilterYearModTo_Movies.FormattingEnabled = true
        Me.cbFilterYearModTo_Movies.Items.AddRange(New Object() {"<=", "<"})
        Me.cbFilterYearModTo_Movies.Location = New System.Drawing.Point(87, 113)
        Me.cbFilterYearModTo_Movies.Name = "cbFilterYearModTo_Movies"
        Me.cbFilterYearModTo_Movies.Size = New System.Drawing.Size(70, 21)
        Me.cbFilterYearModTo_Movies.TabIndex = 40
        '
        'cbFilterYearFrom_Movies
        '
        Me.cbFilterYearFrom_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbFilterYearFrom_Movies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterYearFrom_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbFilterYearFrom_Movies.FormattingEnabled = true
        Me.cbFilterYearFrom_Movies.Items.AddRange(New Object() {"=", ">", "<", "!="})
        Me.cbFilterYearFrom_Movies.Location = New System.Drawing.Point(163, 86)
        Me.cbFilterYearFrom_Movies.Name = "cbFilterYearFrom_Movies"
        Me.cbFilterYearFrom_Movies.Size = New System.Drawing.Size(113, 21)
        Me.cbFilterYearFrom_Movies.TabIndex = 7
        '
        'cbFilterYearTo_Movies
        '
        Me.cbFilterYearTo_Movies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterYearTo_Movies.Enabled = false
        Me.cbFilterYearTo_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbFilterYearTo_Movies.FormattingEnabled = true
        Me.cbFilterYearTo_Movies.Location = New System.Drawing.Point(163, 113)
        Me.cbFilterYearTo_Movies.Name = "cbFilterYearTo_Movies"
        Me.cbFilterYearTo_Movies.Size = New System.Drawing.Size(113, 21)
        Me.cbFilterYearTo_Movies.TabIndex = 41
        '
        'chkFilterNew_Movies
        '
        Me.chkFilterNew_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterNew_Movies.AutoSize = true
        Me.chkFilterNew_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterNew_Movies.Location = New System.Drawing.Point(3, 53)
        Me.chkFilterNew_Movies.Name = "chkFilterNew_Movies"
        Me.chkFilterNew_Movies.Size = New System.Drawing.Size(49, 17)
        Me.chkFilterNew_Movies.TabIndex = 0
        Me.chkFilterNew_Movies.Text = "New"
        Me.chkFilterNew_Movies.UseVisualStyleBackColor = true
        '
        'chkFilterMark_Movies
        '
        Me.chkFilterMark_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMark_Movies.AutoSize = true
        Me.chkFilterMark_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterMark_Movies.Location = New System.Drawing.Point(3, 76)
        Me.chkFilterMark_Movies.Name = "chkFilterMark_Movies"
        Me.chkFilterMark_Movies.Size = New System.Drawing.Size(65, 17)
        Me.chkFilterMark_Movies.TabIndex = 1
        Me.chkFilterMark_Movies.Text = "Marked"
        Me.chkFilterMark_Movies.UseVisualStyleBackColor = true
        '
        'chkFilterMarkCustom1_Movies
        '
        Me.chkFilterMarkCustom1_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMarkCustom1_Movies.AutoSize = true
        Me.chkFilterMarkCustom1_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterMarkCustom1_Movies.Location = New System.Drawing.Point(3, 99)
        Me.chkFilterMarkCustom1_Movies.Name = "chkFilterMarkCustom1_Movies"
        Me.chkFilterMarkCustom1_Movies.Size = New System.Drawing.Size(81, 17)
        Me.chkFilterMarkCustom1_Movies.TabIndex = 32
        Me.chkFilterMarkCustom1_Movies.Text = "Custom #1"
        Me.chkFilterMarkCustom1_Movies.UseVisualStyleBackColor = true
        '
        'chkFilterMarkCustom4_Movies
        '
        Me.chkFilterMarkCustom4_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMarkCustom4_Movies.AutoSize = true
        Me.chkFilterMarkCustom4_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterMarkCustom4_Movies.Location = New System.Drawing.Point(3, 168)
        Me.chkFilterMarkCustom4_Movies.Name = "chkFilterMarkCustom4_Movies"
        Me.chkFilterMarkCustom4_Movies.Size = New System.Drawing.Size(81, 17)
        Me.chkFilterMarkCustom4_Movies.TabIndex = 35
        Me.chkFilterMarkCustom4_Movies.Text = "Custom #4"
        Me.chkFilterMarkCustom4_Movies.UseVisualStyleBackColor = true
        '
        'chkFilterMarkCustom2_Movies
        '
        Me.chkFilterMarkCustom2_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMarkCustom2_Movies.AutoSize = true
        Me.chkFilterMarkCustom2_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterMarkCustom2_Movies.Location = New System.Drawing.Point(3, 122)
        Me.chkFilterMarkCustom2_Movies.Name = "chkFilterMarkCustom2_Movies"
        Me.chkFilterMarkCustom2_Movies.Size = New System.Drawing.Size(81, 17)
        Me.chkFilterMarkCustom2_Movies.TabIndex = 33
        Me.chkFilterMarkCustom2_Movies.Text = "Custom #2"
        Me.chkFilterMarkCustom2_Movies.UseVisualStyleBackColor = true
        '
        'chkFilterMarkCustom3_Movies
        '
        Me.chkFilterMarkCustom3_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMarkCustom3_Movies.AutoSize = true
        Me.chkFilterMarkCustom3_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterMarkCustom3_Movies.Location = New System.Drawing.Point(3, 145)
        Me.chkFilterMarkCustom3_Movies.Name = "chkFilterMarkCustom3_Movies"
        Me.chkFilterMarkCustom3_Movies.Size = New System.Drawing.Size(81, 17)
        Me.chkFilterMarkCustom3_Movies.TabIndex = 34
        Me.chkFilterMarkCustom3_Movies.Text = "Custom #3"
        Me.chkFilterMarkCustom3_Movies.UseVisualStyleBackColor = true
        '
        'chkFilterLock_Movies
        '
        Me.chkFilterLock_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterLock_Movies.AutoSize = true
        Me.chkFilterLock_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkFilterLock_Movies.Location = New System.Drawing.Point(3, 191)
        Me.chkFilterLock_Movies.Name = "chkFilterLock_Movies"
        Me.chkFilterLock_Movies.Size = New System.Drawing.Size(62, 17)
        Me.chkFilterLock_Movies.TabIndex = 2
        Me.chkFilterLock_Movies.Text = "Locked"
        Me.chkFilterLock_Movies.UseVisualStyleBackColor = true
        '
        'pnlFilterTop_Movies
        '
        Me.pnlFilterTop_Movies.AutoSize = true
        Me.pnlFilterTop_Movies.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.pnlFilterTop_Movies.Controls.Add(Me.tblFilterTop_Movies)
        Me.pnlFilterTop_Movies.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterTop_Movies.Name = "pnlFilterTop_Movies"
        Me.pnlFilterTop_Movies.Size = New System.Drawing.Size(565, 22)
        Me.pnlFilterTop_Movies.TabIndex = 7
        '
        'tblFilterTop_Movies
        '
        Me.tblFilterTop_Movies.AutoSize = true
        Me.tblFilterTop_Movies.BackColor = System.Drawing.Color.Transparent
        Me.tblFilterTop_Movies.ColumnCount = 4
        Me.tblFilterTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tblFilterTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterTop_Movies.Controls.Add(Me.lblFilter_Movies, 0, 0)
        Me.tblFilterTop_Movies.Controls.Add(Me.btnFilterUp_Movies, 2, 0)
        Me.tblFilterTop_Movies.Controls.Add(Me.btnFilterDown_Movies, 3, 0)
        Me.tblFilterTop_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterTop_Movies.Name = "tblFilterTop_Movies"
        Me.tblFilterTop_Movies.RowCount = 2
        Me.tblFilterTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterTop_Movies.Size = New System.Drawing.Size(565, 22)
        Me.tblFilterTop_Movies.TabIndex = 0
        '
        'lblFilter_Movies
        '
        Me.lblFilter_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilter_Movies.AutoSize = true
        Me.lblFilter_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilter_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilter_Movies.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilter_Movies.Location = New System.Drawing.Point(3, 4)
        Me.lblFilter_Movies.Name = "lblFilter_Movies"
        Me.lblFilter_Movies.Size = New System.Drawing.Size(38, 13)
        Me.lblFilter_Movies.TabIndex = 0
        Me.lblFilter_Movies.Text = "Filters"
        Me.lblFilter_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnFilterUp_Movies
        '
        Me.btnFilterUp_Movies.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFilterUp_Movies.BackColor = System.Drawing.SystemColors.Control
        Me.btnFilterUp_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnFilterUp_Movies.Location = New System.Drawing.Point(505, 0)
        Me.btnFilterUp_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterUp_Movies.Name = "btnFilterUp_Movies"
        Me.btnFilterUp_Movies.Size = New System.Drawing.Size(30, 22)
        Me.btnFilterUp_Movies.TabIndex = 1
        Me.btnFilterUp_Movies.TabStop = false
        Me.btnFilterUp_Movies.Text = "^"
        Me.btnFilterUp_Movies.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFilterUp_Movies.UseVisualStyleBackColor = false
        '
        'btnFilterDown_Movies
        '
        Me.btnFilterDown_Movies.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFilterDown_Movies.BackColor = System.Drawing.SystemColors.Control
        Me.btnFilterDown_Movies.Enabled = false
        Me.btnFilterDown_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnFilterDown_Movies.Location = New System.Drawing.Point(535, 0)
        Me.btnFilterDown_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterDown_Movies.Name = "btnFilterDown_Movies"
        Me.btnFilterDown_Movies.Size = New System.Drawing.Size(30, 22)
        Me.btnFilterDown_Movies.TabIndex = 2
        Me.btnFilterDown_Movies.TabStop = false
        Me.btnFilterDown_Movies.Text = "v"
        Me.btnFilterDown_Movies.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFilterDown_Movies.UseVisualStyleBackColor = false
        '
        'pnlFilter_MovieSets
        '
        Me.pnlFilter_MovieSets.AutoSize = true
        Me.pnlFilter_MovieSets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilter_MovieSets.Controls.Add(Me.tblFilter_MovieSets)
        Me.pnlFilter_MovieSets.Controls.Add(Me.pnlFilterTop_MovieSets)
        Me.pnlFilter_MovieSets.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFilter_MovieSets.Location = New System.Drawing.Point(0, 347)
        Me.pnlFilter_MovieSets.Name = "pnlFilter_MovieSets"
        Me.pnlFilter_MovieSets.Size = New System.Drawing.Size(567, 170)
        Me.pnlFilter_MovieSets.TabIndex = 26
        Me.pnlFilter_MovieSets.Visible = false
        '
        'tblFilter_MovieSets
        '
        Me.tblFilter_MovieSets.AutoScroll = true
        Me.tblFilter_MovieSets.AutoSize = true
        Me.tblFilter_MovieSets.ColumnCount = 3
        Me.tblFilter_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_MovieSets.Controls.Add(Me.gbFilterGeneral_MovieSets, 0, 0)
        Me.tblFilter_MovieSets.Controls.Add(Me.gbFilterSpecific_MovieSets, 1, 0)
        Me.tblFilter_MovieSets.Controls.Add(Me.btnClearFilters_MovieSets, 0, 1)
        Me.tblFilter_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilter_MovieSets.Location = New System.Drawing.Point(0, 22)
        Me.tblFilter_MovieSets.Name = "tblFilter_MovieSets"
        Me.tblFilter_MovieSets.RowCount = 3
        Me.tblFilter_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_MovieSets.Size = New System.Drawing.Size(565, 146)
        Me.tblFilter_MovieSets.TabIndex = 8
        '
        'gbFilterGeneral_MovieSets
        '
        Me.gbFilterGeneral_MovieSets.AutoSize = true
        Me.gbFilterGeneral_MovieSets.Controls.Add(Me.tblFilterGeneral_MovieSets)
        Me.gbFilterGeneral_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterGeneral_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbFilterGeneral_MovieSets.Location = New System.Drawing.Point(3, 3)
        Me.gbFilterGeneral_MovieSets.Name = "gbFilterGeneral_MovieSets"
        Me.gbFilterGeneral_MovieSets.Size = New System.Drawing.Size(124, 90)
        Me.gbFilterGeneral_MovieSets.TabIndex = 3
        Me.gbFilterGeneral_MovieSets.TabStop = false
        Me.gbFilterGeneral_MovieSets.Text = "General"
        '
        'tblFilterGeneral_MovieSets
        '
        Me.tblFilterGeneral_MovieSets.AutoSize = true
        Me.tblFilterGeneral_MovieSets.ColumnCount = 2
        Me.tblFilterGeneral_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGeneral_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGeneral_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterGeneral_MovieSets.Controls.Add(Me.btnFilterMissing_MovieSets, 1, 0)
        Me.tblFilterGeneral_MovieSets.Controls.Add(Me.chkFilterMissing_MovieSets, 0, 0)
        Me.tblFilterGeneral_MovieSets.Controls.Add(Me.chkFilterOne_MovieSets, 0, 2)
        Me.tblFilterGeneral_MovieSets.Controls.Add(Me.chkFilterEmpty_MovieSets, 0, 1)
        Me.tblFilterGeneral_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterGeneral_MovieSets.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterGeneral_MovieSets.Name = "tblFilterGeneral_MovieSets"
        Me.tblFilterGeneral_MovieSets.RowCount = 4
        Me.tblFilterGeneral_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_MovieSets.Size = New System.Drawing.Size(118, 69)
        Me.tblFilterGeneral_MovieSets.TabIndex = 9
        '
        'btnFilterMissing_MovieSets
        '
        Me.btnFilterMissing_MovieSets.AutoSize = true
        Me.btnFilterMissing_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterMissing_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnFilterMissing_MovieSets.Location = New System.Drawing.Point(21, 0)
        Me.btnFilterMissing_MovieSets.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterMissing_MovieSets.Name = "btnFilterMissing_MovieSets"
        Me.btnFilterMissing_MovieSets.Size = New System.Drawing.Size(97, 23)
        Me.btnFilterMissing_MovieSets.TabIndex = 4
        Me.btnFilterMissing_MovieSets.Text = "Missing Items"
        Me.btnFilterMissing_MovieSets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterMissing_MovieSets.UseVisualStyleBackColor = true
        '
        'chkFilterMissing_MovieSets
        '
        Me.chkFilterMissing_MovieSets.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMissing_MovieSets.AutoSize = true
        Me.chkFilterMissing_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterMissing_MovieSets.Location = New System.Drawing.Point(3, 4)
        Me.chkFilterMissing_MovieSets.Name = "chkFilterMissing_MovieSets"
        Me.chkFilterMissing_MovieSets.Size = New System.Drawing.Size(15, 14)
        Me.chkFilterMissing_MovieSets.TabIndex = 1
        Me.chkFilterMissing_MovieSets.UseVisualStyleBackColor = true
        '
        'chkFilterOne_MovieSets
        '
        Me.chkFilterOne_MovieSets.AutoSize = true
        Me.tblFilterGeneral_MovieSets.SetColumnSpan(Me.chkFilterOne_MovieSets, 2)
        Me.chkFilterOne_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterOne_MovieSets.Location = New System.Drawing.Point(3, 49)
        Me.chkFilterOne_MovieSets.Name = "chkFilterOne_MovieSets"
        Me.chkFilterOne_MovieSets.Size = New System.Drawing.Size(109, 17)
        Me.chkFilterOne_MovieSets.TabIndex = 3
        Me.chkFilterOne_MovieSets.Text = "Only One Movie"
        Me.chkFilterOne_MovieSets.UseVisualStyleBackColor = true
        '
        'chkFilterEmpty_MovieSets
        '
        Me.chkFilterEmpty_MovieSets.AutoSize = true
        Me.tblFilterGeneral_MovieSets.SetColumnSpan(Me.chkFilterEmpty_MovieSets, 2)
        Me.chkFilterEmpty_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterEmpty_MovieSets.Location = New System.Drawing.Point(3, 26)
        Me.chkFilterEmpty_MovieSets.Name = "chkFilterEmpty_MovieSets"
        Me.chkFilterEmpty_MovieSets.Size = New System.Drawing.Size(57, 17)
        Me.chkFilterEmpty_MovieSets.TabIndex = 2
        Me.chkFilterEmpty_MovieSets.Text = "Empty"
        Me.chkFilterEmpty_MovieSets.UseVisualStyleBackColor = true
        '
        'gbFilterSpecific_MovieSets
        '
        Me.gbFilterSpecific_MovieSets.AutoSize = true
        Me.gbFilterSpecific_MovieSets.Controls.Add(Me.tblFilterSpecific_MovieSets)
        Me.gbFilterSpecific_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbFilterSpecific_MovieSets.Location = New System.Drawing.Point(133, 3)
        Me.gbFilterSpecific_MovieSets.Name = "gbFilterSpecific_MovieSets"
        Me.tblFilter_MovieSets.SetRowSpan(Me.gbFilterSpecific_MovieSets, 2)
        Me.gbFilterSpecific_MovieSets.Size = New System.Drawing.Size(114, 140)
        Me.gbFilterSpecific_MovieSets.TabIndex = 6
        Me.gbFilterSpecific_MovieSets.TabStop = false
        Me.gbFilterSpecific_MovieSets.Text = "Specific"
        '
        'tblFilterSpecific_MovieSets
        '
        Me.tblFilterSpecific_MovieSets.AutoSize = true
        Me.tblFilterSpecific_MovieSets.ColumnCount = 2
        Me.tblFilterSpecific_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecific_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecific_MovieSets.Controls.Add(Me.gbFilterModifier_MovieSets, 0, 0)
        Me.tblFilterSpecific_MovieSets.Controls.Add(Me.chkFilterLock_MovieSets, 0, 3)
        Me.tblFilterSpecific_MovieSets.Controls.Add(Me.chkFilterNew_MovieSets, 0, 1)
        Me.tblFilterSpecific_MovieSets.Controls.Add(Me.chkFilterMark_MovieSets, 0, 2)
        Me.tblFilterSpecific_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterSpecific_MovieSets.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterSpecific_MovieSets.Name = "tblFilterSpecific_MovieSets"
        Me.tblFilterSpecific_MovieSets.RowCount = 5
        Me.tblFilterSpecific_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_MovieSets.Size = New System.Drawing.Size(108, 119)
        Me.tblFilterSpecific_MovieSets.TabIndex = 9
        '
        'gbFilterModifier_MovieSets
        '
        Me.gbFilterModifier_MovieSets.AutoSize = true
        Me.gbFilterModifier_MovieSets.Controls.Add(Me.tblFilterModifier_MovieSets)
        Me.gbFilterModifier_MovieSets.Location = New System.Drawing.Point(3, 3)
        Me.gbFilterModifier_MovieSets.Name = "gbFilterModifier_MovieSets"
        Me.gbFilterModifier_MovieSets.Size = New System.Drawing.Size(102, 44)
        Me.gbFilterModifier_MovieSets.TabIndex = 3
        Me.gbFilterModifier_MovieSets.TabStop = false
        Me.gbFilterModifier_MovieSets.Text = "Modifier"
        '
        'tblFilterModifier_MovieSets
        '
        Me.tblFilterModifier_MovieSets.AutoSize = true
        Me.tblFilterModifier_MovieSets.ColumnCount = 3
        Me.tblFilterModifier_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterModifier_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterModifier_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterModifier_MovieSets.Controls.Add(Me.rbFilterOr_MovieSets, 1, 0)
        Me.tblFilterModifier_MovieSets.Controls.Add(Me.rbFilterAnd_MovieSets, 0, 0)
        Me.tblFilterModifier_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterModifier_MovieSets.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterModifier_MovieSets.Name = "tblFilterModifier_MovieSets"
        Me.tblFilterModifier_MovieSets.RowCount = 2
        Me.tblFilterModifier_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterModifier_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterModifier_MovieSets.Size = New System.Drawing.Size(96, 23)
        Me.tblFilterModifier_MovieSets.TabIndex = 9
        '
        'rbFilterOr_MovieSets
        '
        Me.rbFilterOr_MovieSets.AutoSize = true
        Me.rbFilterOr_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbFilterOr_MovieSets.Location = New System.Drawing.Point(55, 3)
        Me.rbFilterOr_MovieSets.Name = "rbFilterOr_MovieSets"
        Me.rbFilterOr_MovieSets.Size = New System.Drawing.Size(38, 17)
        Me.rbFilterOr_MovieSets.TabIndex = 1
        Me.rbFilterOr_MovieSets.Text = "Or"
        Me.rbFilterOr_MovieSets.UseVisualStyleBackColor = true
        '
        'rbFilterAnd_MovieSets
        '
        Me.rbFilterAnd_MovieSets.AutoSize = true
        Me.rbFilterAnd_MovieSets.Checked = true
        Me.rbFilterAnd_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbFilterAnd_MovieSets.Location = New System.Drawing.Point(3, 3)
        Me.rbFilterAnd_MovieSets.Name = "rbFilterAnd_MovieSets"
        Me.rbFilterAnd_MovieSets.Size = New System.Drawing.Size(46, 17)
        Me.rbFilterAnd_MovieSets.TabIndex = 0
        Me.rbFilterAnd_MovieSets.TabStop = true
        Me.rbFilterAnd_MovieSets.Text = "And"
        Me.rbFilterAnd_MovieSets.UseVisualStyleBackColor = true
        '
        'chkFilterLock_MovieSets
        '
        Me.chkFilterLock_MovieSets.AutoSize = true
        Me.chkFilterLock_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkFilterLock_MovieSets.Location = New System.Drawing.Point(3, 99)
        Me.chkFilterLock_MovieSets.Name = "chkFilterLock_MovieSets"
        Me.chkFilterLock_MovieSets.Size = New System.Drawing.Size(62, 17)
        Me.chkFilterLock_MovieSets.TabIndex = 2
        Me.chkFilterLock_MovieSets.Text = "Locked"
        Me.chkFilterLock_MovieSets.UseVisualStyleBackColor = true
        '
        'chkFilterNew_MovieSets
        '
        Me.chkFilterNew_MovieSets.AutoSize = true
        Me.chkFilterNew_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterNew_MovieSets.Location = New System.Drawing.Point(3, 53)
        Me.chkFilterNew_MovieSets.Name = "chkFilterNew_MovieSets"
        Me.chkFilterNew_MovieSets.Size = New System.Drawing.Size(49, 17)
        Me.chkFilterNew_MovieSets.TabIndex = 0
        Me.chkFilterNew_MovieSets.Text = "New"
        Me.chkFilterNew_MovieSets.UseVisualStyleBackColor = true
        '
        'chkFilterMark_MovieSets
        '
        Me.chkFilterMark_MovieSets.AutoSize = true
        Me.chkFilterMark_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterMark_MovieSets.Location = New System.Drawing.Point(3, 76)
        Me.chkFilterMark_MovieSets.Name = "chkFilterMark_MovieSets"
        Me.chkFilterMark_MovieSets.Size = New System.Drawing.Size(65, 17)
        Me.chkFilterMark_MovieSets.TabIndex = 1
        Me.chkFilterMark_MovieSets.Text = "Marked"
        Me.chkFilterMark_MovieSets.UseVisualStyleBackColor = true
        '
        'btnClearFilters_MovieSets
        '
        Me.btnClearFilters_MovieSets.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnClearFilters_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnClearFilters_MovieSets.Image = CType(resources.GetObject("btnClearFilters_MovieSets.Image"),System.Drawing.Image)
        Me.btnClearFilters_MovieSets.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearFilters_MovieSets.Location = New System.Drawing.Point(19, 116)
        Me.btnClearFilters_MovieSets.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.btnClearFilters_MovieSets.Name = "btnClearFilters_MovieSets"
        Me.btnClearFilters_MovieSets.Size = New System.Drawing.Size(92, 20)
        Me.btnClearFilters_MovieSets.TabIndex = 5
        Me.btnClearFilters_MovieSets.Text = "Clear Filters"
        Me.btnClearFilters_MovieSets.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClearFilters_MovieSets.UseVisualStyleBackColor = true
        '
        'pnlFilterTop_MovieSets
        '
        Me.pnlFilterTop_MovieSets.AutoSize = true
        Me.pnlFilterTop_MovieSets.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.pnlFilterTop_MovieSets.Controls.Add(Me.tblFilterTop_MovieSets)
        Me.pnlFilterTop_MovieSets.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterTop_MovieSets.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterTop_MovieSets.Name = "pnlFilterTop_MovieSets"
        Me.pnlFilterTop_MovieSets.Size = New System.Drawing.Size(565, 22)
        Me.pnlFilterTop_MovieSets.TabIndex = 4
        '
        'tblFilterTop_MovieSets
        '
        Me.tblFilterTop_MovieSets.AutoSize = true
        Me.tblFilterTop_MovieSets.ColumnCount = 4
        Me.tblFilterTop_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterTop_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tblFilterTop_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterTop_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterTop_MovieSets.Controls.Add(Me.lblFilter_MovieSets, 0, 0)
        Me.tblFilterTop_MovieSets.Controls.Add(Me.btnFilterUp_MovieSets, 2, 0)
        Me.tblFilterTop_MovieSets.Controls.Add(Me.btnFilterDown_MovieSets, 3, 0)
        Me.tblFilterTop_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterTop_MovieSets.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterTop_MovieSets.Name = "tblFilterTop_MovieSets"
        Me.tblFilterTop_MovieSets.RowCount = 2
        Me.tblFilterTop_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterTop_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterTop_MovieSets.Size = New System.Drawing.Size(565, 22)
        Me.tblFilterTop_MovieSets.TabIndex = 0
        '
        'lblFilter_MovieSets
        '
        Me.lblFilter_MovieSets.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilter_MovieSets.AutoSize = true
        Me.lblFilter_MovieSets.BackColor = System.Drawing.Color.Transparent
        Me.lblFilter_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilter_MovieSets.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilter_MovieSets.Location = New System.Drawing.Point(3, 4)
        Me.lblFilter_MovieSets.Name = "lblFilter_MovieSets"
        Me.lblFilter_MovieSets.Size = New System.Drawing.Size(38, 13)
        Me.lblFilter_MovieSets.TabIndex = 0
        Me.lblFilter_MovieSets.Text = "Filters"
        Me.lblFilter_MovieSets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnFilterUp_MovieSets
        '
        Me.btnFilterUp_MovieSets.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFilterUp_MovieSets.BackColor = System.Drawing.SystemColors.Control
        Me.btnFilterUp_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnFilterUp_MovieSets.Location = New System.Drawing.Point(505, 0)
        Me.btnFilterUp_MovieSets.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterUp_MovieSets.Name = "btnFilterUp_MovieSets"
        Me.btnFilterUp_MovieSets.Size = New System.Drawing.Size(30, 22)
        Me.btnFilterUp_MovieSets.TabIndex = 1
        Me.btnFilterUp_MovieSets.TabStop = false
        Me.btnFilterUp_MovieSets.Text = "^"
        Me.btnFilterUp_MovieSets.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFilterUp_MovieSets.UseVisualStyleBackColor = false
        '
        'btnFilterDown_MovieSets
        '
        Me.btnFilterDown_MovieSets.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFilterDown_MovieSets.BackColor = System.Drawing.SystemColors.Control
        Me.btnFilterDown_MovieSets.Enabled = false
        Me.btnFilterDown_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnFilterDown_MovieSets.Location = New System.Drawing.Point(535, 0)
        Me.btnFilterDown_MovieSets.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterDown_MovieSets.Name = "btnFilterDown_MovieSets"
        Me.btnFilterDown_MovieSets.Size = New System.Drawing.Size(30, 22)
        Me.btnFilterDown_MovieSets.TabIndex = 2
        Me.btnFilterDown_MovieSets.TabStop = false
        Me.btnFilterDown_MovieSets.Text = "v"
        Me.btnFilterDown_MovieSets.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFilterDown_MovieSets.UseVisualStyleBackColor = false
        '
        'pnlFilter_Shows
        '
        Me.pnlFilter_Shows.AutoSize = true
        Me.pnlFilter_Shows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilter_Shows.Controls.Add(Me.tblFilter_Shows)
        Me.pnlFilter_Shows.Controls.Add(Me.pnlFilterTop_Shows)
        Me.pnlFilter_Shows.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFilter_Shows.Location = New System.Drawing.Point(0, 517)
        Me.pnlFilter_Shows.Name = "pnlFilter_Shows"
        Me.pnlFilter_Shows.Size = New System.Drawing.Size(567, 170)
        Me.pnlFilter_Shows.TabIndex = 27
        Me.pnlFilter_Shows.Visible = false
        '
        'tblFilter_Shows
        '
        Me.tblFilter_Shows.AutoScroll = true
        Me.tblFilter_Shows.AutoSize = true
        Me.tblFilter_Shows.ColumnCount = 3
        Me.tblFilter_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_Shows.Controls.Add(Me.gbFilterGeneral_Shows, 0, 0)
        Me.tblFilter_Shows.Controls.Add(Me.gbFilterSpecific_Shows, 1, 0)
        Me.tblFilter_Shows.Controls.Add(Me.btnClearFilters_Shows, 0, 1)
        Me.tblFilter_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilter_Shows.Location = New System.Drawing.Point(0, 22)
        Me.tblFilter_Shows.Name = "tblFilter_Shows"
        Me.tblFilter_Shows.RowCount = 3
        Me.tblFilter_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Shows.Size = New System.Drawing.Size(565, 146)
        Me.tblFilter_Shows.TabIndex = 8
        '
        'gbFilterGeneral_Shows
        '
        Me.gbFilterGeneral_Shows.AutoSize = true
        Me.gbFilterGeneral_Shows.Controls.Add(Me.tblFilterGeneral_Shows)
        Me.gbFilterGeneral_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterGeneral_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbFilterGeneral_Shows.Location = New System.Drawing.Point(3, 3)
        Me.gbFilterGeneral_Shows.Name = "gbFilterGeneral_Shows"
        Me.gbFilterGeneral_Shows.Size = New System.Drawing.Size(124, 44)
        Me.gbFilterGeneral_Shows.TabIndex = 3
        Me.gbFilterGeneral_Shows.TabStop = false
        Me.gbFilterGeneral_Shows.Text = "General"
        '
        'tblFilterGeneral_Shows
        '
        Me.tblFilterGeneral_Shows.AutoSize = true
        Me.tblFilterGeneral_Shows.ColumnCount = 2
        Me.tblFilterGeneral_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGeneral_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGeneral_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterGeneral_Shows.Controls.Add(Me.btnFilterMissing_Shows, 1, 0)
        Me.tblFilterGeneral_Shows.Controls.Add(Me.chkFilterMissing_Shows, 0, 0)
        Me.tblFilterGeneral_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterGeneral_Shows.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterGeneral_Shows.Name = "tblFilterGeneral_Shows"
        Me.tblFilterGeneral_Shows.RowCount = 2
        Me.tblFilterGeneral_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_Shows.Size = New System.Drawing.Size(118, 23)
        Me.tblFilterGeneral_Shows.TabIndex = 34
        '
        'btnFilterMissing_Shows
        '
        Me.btnFilterMissing_Shows.AutoSize = true
        Me.btnFilterMissing_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterMissing_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnFilterMissing_Shows.Location = New System.Drawing.Point(21, 0)
        Me.btnFilterMissing_Shows.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterMissing_Shows.Name = "btnFilterMissing_Shows"
        Me.btnFilterMissing_Shows.Size = New System.Drawing.Size(97, 23)
        Me.btnFilterMissing_Shows.TabIndex = 5
        Me.btnFilterMissing_Shows.Text = "Missing Items"
        Me.btnFilterMissing_Shows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterMissing_Shows.UseVisualStyleBackColor = true
        '
        'chkFilterMissing_Shows
        '
        Me.chkFilterMissing_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMissing_Shows.AutoSize = true
        Me.chkFilterMissing_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterMissing_Shows.Location = New System.Drawing.Point(3, 4)
        Me.chkFilterMissing_Shows.Name = "chkFilterMissing_Shows"
        Me.chkFilterMissing_Shows.Size = New System.Drawing.Size(15, 14)
        Me.chkFilterMissing_Shows.TabIndex = 1
        Me.chkFilterMissing_Shows.UseVisualStyleBackColor = true
        '
        'gbFilterSpecific_Shows
        '
        Me.gbFilterSpecific_Shows.AutoSize = true
        Me.gbFilterSpecific_Shows.Controls.Add(Me.tblFilterSpecific_Shows)
        Me.gbFilterSpecific_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterSpecific_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbFilterSpecific_Shows.Location = New System.Drawing.Point(133, 3)
        Me.gbFilterSpecific_Shows.Name = "gbFilterSpecific_Shows"
        Me.tblFilter_Shows.SetRowSpan(Me.gbFilterSpecific_Shows, 2)
        Me.gbFilterSpecific_Shows.Size = New System.Drawing.Size(337, 140)
        Me.gbFilterSpecific_Shows.TabIndex = 6
        Me.gbFilterSpecific_Shows.TabStop = false
        Me.gbFilterSpecific_Shows.Text = "Specific"
        '
        'tblFilterSpecific_Shows
        '
        Me.tblFilterSpecific_Shows.AutoSize = true
        Me.tblFilterSpecific_Shows.ColumnCount = 3
        Me.tblFilterSpecific_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecific_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecific_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecific_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblFilterSpecific_Shows.Controls.Add(Me.chkFilterLock_Shows, 0, 3)
        Me.tblFilterSpecific_Shows.Controls.Add(Me.gbFilterModifier_Shows, 0, 0)
        Me.tblFilterSpecific_Shows.Controls.Add(Me.chkFilterMark_Shows, 0, 2)
        Me.tblFilterSpecific_Shows.Controls.Add(Me.tblFilterSpecificData_Shows, 1, 0)
        Me.tblFilterSpecific_Shows.Controls.Add(Me.chkFilterNew_Shows, 0, 1)
        Me.tblFilterSpecific_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterSpecific_Shows.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterSpecific_Shows.Name = "tblFilterSpecific_Shows"
        Me.tblFilterSpecific_Shows.RowCount = 5
        Me.tblFilterSpecific_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Shows.Size = New System.Drawing.Size(331, 119)
        Me.tblFilterSpecific_Shows.TabIndex = 8
        '
        'chkFilterLock_Shows
        '
        Me.chkFilterLock_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterLock_Shows.AutoSize = true
        Me.chkFilterLock_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkFilterLock_Shows.Location = New System.Drawing.Point(3, 99)
        Me.chkFilterLock_Shows.Name = "chkFilterLock_Shows"
        Me.chkFilterLock_Shows.Size = New System.Drawing.Size(62, 17)
        Me.chkFilterLock_Shows.TabIndex = 2
        Me.chkFilterLock_Shows.Text = "Locked"
        Me.chkFilterLock_Shows.UseVisualStyleBackColor = true
        '
        'gbFilterModifier_Shows
        '
        Me.gbFilterModifier_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.gbFilterModifier_Shows.AutoSize = true
        Me.gbFilterModifier_Shows.Controls.Add(Me.tblFilterModifier_Shows)
        Me.gbFilterModifier_Shows.Location = New System.Drawing.Point(3, 3)
        Me.gbFilterModifier_Shows.Name = "gbFilterModifier_Shows"
        Me.gbFilterModifier_Shows.Size = New System.Drawing.Size(102, 44)
        Me.gbFilterModifier_Shows.TabIndex = 3
        Me.gbFilterModifier_Shows.TabStop = false
        Me.gbFilterModifier_Shows.Text = "Modifier"
        '
        'tblFilterModifier_Shows
        '
        Me.tblFilterModifier_Shows.AutoSize = true
        Me.tblFilterModifier_Shows.ColumnCount = 3
        Me.tblFilterModifier_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterModifier_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterModifier_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterModifier_Shows.Controls.Add(Me.rbFilterOr_Shows, 1, 0)
        Me.tblFilterModifier_Shows.Controls.Add(Me.rbFilterAnd_Shows, 0, 0)
        Me.tblFilterModifier_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterModifier_Shows.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterModifier_Shows.Name = "tblFilterModifier_Shows"
        Me.tblFilterModifier_Shows.RowCount = 2
        Me.tblFilterModifier_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterModifier_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterModifier_Shows.Size = New System.Drawing.Size(96, 23)
        Me.tblFilterModifier_Shows.TabIndex = 8
        '
        'rbFilterOr_Shows
        '
        Me.rbFilterOr_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbFilterOr_Shows.AutoSize = true
        Me.rbFilterOr_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbFilterOr_Shows.Location = New System.Drawing.Point(55, 3)
        Me.rbFilterOr_Shows.Name = "rbFilterOr_Shows"
        Me.rbFilterOr_Shows.Size = New System.Drawing.Size(38, 17)
        Me.rbFilterOr_Shows.TabIndex = 1
        Me.rbFilterOr_Shows.Text = "Or"
        Me.rbFilterOr_Shows.UseVisualStyleBackColor = true
        '
        'rbFilterAnd_Shows
        '
        Me.rbFilterAnd_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbFilterAnd_Shows.AutoSize = true
        Me.rbFilterAnd_Shows.Checked = true
        Me.rbFilterAnd_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.rbFilterAnd_Shows.Location = New System.Drawing.Point(3, 3)
        Me.rbFilterAnd_Shows.Name = "rbFilterAnd_Shows"
        Me.rbFilterAnd_Shows.Size = New System.Drawing.Size(46, 17)
        Me.rbFilterAnd_Shows.TabIndex = 0
        Me.rbFilterAnd_Shows.TabStop = true
        Me.rbFilterAnd_Shows.Text = "And"
        Me.rbFilterAnd_Shows.UseVisualStyleBackColor = true
        '
        'chkFilterMark_Shows
        '
        Me.chkFilterMark_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMark_Shows.AutoSize = true
        Me.chkFilterMark_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterMark_Shows.Location = New System.Drawing.Point(3, 76)
        Me.chkFilterMark_Shows.Name = "chkFilterMark_Shows"
        Me.chkFilterMark_Shows.Size = New System.Drawing.Size(65, 17)
        Me.chkFilterMark_Shows.TabIndex = 1
        Me.chkFilterMark_Shows.Text = "Marked"
        Me.chkFilterMark_Shows.UseVisualStyleBackColor = true
        '
        'tblFilterSpecificData_Shows
        '
        Me.tblFilterSpecificData_Shows.AutoSize = true
        Me.tblFilterSpecificData_Shows.ColumnCount = 3
        Me.tblFilterSpecificData_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecificData_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecificData_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecificData_Shows.Controls.Add(Me.txtFilterSource_Shows, 1, 1)
        Me.tblFilterSpecificData_Shows.Controls.Add(Me.lblFilterGenre_Shows, 0, 0)
        Me.tblFilterSpecificData_Shows.Controls.Add(Me.txtFilterGenre_Shows, 1, 0)
        Me.tblFilterSpecificData_Shows.Controls.Add(Me.lblFilterSource_Shows, 0, 1)
        Me.tblFilterSpecificData_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterSpecificData_Shows.Location = New System.Drawing.Point(111, 3)
        Me.tblFilterSpecificData_Shows.Name = "tblFilterSpecificData_Shows"
        Me.tblFilterSpecificData_Shows.RowCount = 3
        Me.tblFilterSpecific_Shows.SetRowSpan(Me.tblFilterSpecificData_Shows, 2)
        Me.tblFilterSpecificData_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecificData_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecificData_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecificData_Shows.Size = New System.Drawing.Size(217, 67)
        Me.tblFilterSpecificData_Shows.TabIndex = 7
        '
        'txtFilterSource_Shows
        '
        Me.txtFilterSource_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFilterSource_Shows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilterSource_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFilterSource_Shows.Location = New System.Drawing.Point(54, 31)
        Me.txtFilterSource_Shows.Name = "txtFilterSource_Shows"
        Me.txtFilterSource_Shows.ReadOnly = true
        Me.txtFilterSource_Shows.Size = New System.Drawing.Size(160, 22)
        Me.txtFilterSource_Shows.TabIndex = 33
        '
        'lblFilterGenre_Shows
        '
        Me.lblFilterGenre_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterGenre_Shows.AutoSize = true
        Me.lblFilterGenre_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterGenre_Shows.Location = New System.Drawing.Point(3, 7)
        Me.lblFilterGenre_Shows.Name = "lblFilterGenre_Shows"
        Me.lblFilterGenre_Shows.Size = New System.Drawing.Size(41, 13)
        Me.lblFilterGenre_Shows.TabIndex = 31
        Me.lblFilterGenre_Shows.Text = "Genre:"
        '
        'txtFilterGenre_Shows
        '
        Me.txtFilterGenre_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFilterGenre_Shows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilterGenre_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFilterGenre_Shows.Location = New System.Drawing.Point(54, 3)
        Me.txtFilterGenre_Shows.Name = "txtFilterGenre_Shows"
        Me.txtFilterGenre_Shows.ReadOnly = true
        Me.txtFilterGenre_Shows.Size = New System.Drawing.Size(160, 22)
        Me.txtFilterGenre_Shows.TabIndex = 4
        '
        'lblFilterSource_Shows
        '
        Me.lblFilterSource_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterSource_Shows.AutoSize = true
        Me.lblFilterSource_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilterSource_Shows.Location = New System.Drawing.Point(3, 35)
        Me.lblFilterSource_Shows.Name = "lblFilterSource_Shows"
        Me.lblFilterSource_Shows.Size = New System.Drawing.Size(45, 13)
        Me.lblFilterSource_Shows.TabIndex = 32
        Me.lblFilterSource_Shows.Text = "Source:"
        '
        'chkFilterNew_Shows
        '
        Me.chkFilterNew_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterNew_Shows.AutoSize = true
        Me.chkFilterNew_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkFilterNew_Shows.Location = New System.Drawing.Point(3, 53)
        Me.chkFilterNew_Shows.Name = "chkFilterNew_Shows"
        Me.chkFilterNew_Shows.Size = New System.Drawing.Size(49, 17)
        Me.chkFilterNew_Shows.TabIndex = 0
        Me.chkFilterNew_Shows.Text = "New"
        Me.chkFilterNew_Shows.UseVisualStyleBackColor = true
        '
        'btnClearFilters_Shows
        '
        Me.btnClearFilters_Shows.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnClearFilters_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnClearFilters_Shows.Image = CType(resources.GetObject("btnClearFilters_Shows.Image"),System.Drawing.Image)
        Me.btnClearFilters_Shows.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearFilters_Shows.Location = New System.Drawing.Point(19, 116)
        Me.btnClearFilters_Shows.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.btnClearFilters_Shows.Name = "btnClearFilters_Shows"
        Me.btnClearFilters_Shows.Size = New System.Drawing.Size(92, 20)
        Me.btnClearFilters_Shows.TabIndex = 5
        Me.btnClearFilters_Shows.Text = "Clear Filters"
        Me.btnClearFilters_Shows.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClearFilters_Shows.UseVisualStyleBackColor = true
        '
        'pnlFilterTop_Shows
        '
        Me.pnlFilterTop_Shows.AutoSize = true
        Me.pnlFilterTop_Shows.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.pnlFilterTop_Shows.Controls.Add(Me.tblFilterTop_Shows)
        Me.pnlFilterTop_Shows.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterTop_Shows.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterTop_Shows.Name = "pnlFilterTop_Shows"
        Me.pnlFilterTop_Shows.Size = New System.Drawing.Size(565, 22)
        Me.pnlFilterTop_Shows.TabIndex = 7
        '
        'tblFilterTop_Shows
        '
        Me.tblFilterTop_Shows.AutoSize = true
        Me.tblFilterTop_Shows.ColumnCount = 4
        Me.tblFilterTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tblFilterTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterTop_Shows.Controls.Add(Me.lblFilter_Shows, 0, 0)
        Me.tblFilterTop_Shows.Controls.Add(Me.btnFilterUp_Shows, 2, 0)
        Me.tblFilterTop_Shows.Controls.Add(Me.btnFilterDown_Shows, 3, 0)
        Me.tblFilterTop_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterTop_Shows.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterTop_Shows.Name = "tblFilterTop_Shows"
        Me.tblFilterTop_Shows.RowCount = 2
        Me.tblFilterTop_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterTop_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterTop_Shows.Size = New System.Drawing.Size(565, 22)
        Me.tblFilterTop_Shows.TabIndex = 34
        '
        'lblFilter_Shows
        '
        Me.lblFilter_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilter_Shows.AutoSize = true
        Me.lblFilter_Shows.BackColor = System.Drawing.Color.Transparent
        Me.lblFilter_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFilter_Shows.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilter_Shows.Location = New System.Drawing.Point(3, 4)
        Me.lblFilter_Shows.Name = "lblFilter_Shows"
        Me.lblFilter_Shows.Size = New System.Drawing.Size(38, 13)
        Me.lblFilter_Shows.TabIndex = 0
        Me.lblFilter_Shows.Text = "Filters"
        Me.lblFilter_Shows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnFilterUp_Shows
        '
        Me.btnFilterUp_Shows.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFilterUp_Shows.BackColor = System.Drawing.SystemColors.Control
        Me.btnFilterUp_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnFilterUp_Shows.Location = New System.Drawing.Point(505, 0)
        Me.btnFilterUp_Shows.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterUp_Shows.Name = "btnFilterUp_Shows"
        Me.btnFilterUp_Shows.Size = New System.Drawing.Size(30, 22)
        Me.btnFilterUp_Shows.TabIndex = 1
        Me.btnFilterUp_Shows.TabStop = false
        Me.btnFilterUp_Shows.Text = "^"
        Me.btnFilterUp_Shows.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFilterUp_Shows.UseVisualStyleBackColor = false
        '
        'btnFilterDown_Shows
        '
        Me.btnFilterDown_Shows.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFilterDown_Shows.BackColor = System.Drawing.SystemColors.Control
        Me.btnFilterDown_Shows.Enabled = false
        Me.btnFilterDown_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnFilterDown_Shows.Location = New System.Drawing.Point(535, 0)
        Me.btnFilterDown_Shows.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterDown_Shows.Name = "btnFilterDown_Shows"
        Me.btnFilterDown_Shows.Size = New System.Drawing.Size(30, 22)
        Me.btnFilterDown_Shows.TabIndex = 2
        Me.btnFilterDown_Shows.TabStop = false
        Me.btnFilterDown_Shows.Text = "v"
        Me.btnFilterDown_Shows.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFilterDown_Shows.UseVisualStyleBackColor = false
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.tlpHeader)
        Me.pnlTop.Controls.Add(Me.pnlInfoIcons)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 25)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(773, 98)
        Me.pnlTop.TabIndex = 9
        Me.pnlTop.Visible = false
        '
        'tlpHeader
        '
        Me.tlpHeader.AutoSize = true
        Me.tlpHeader.ColumnCount = 3
        Me.tlpHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250!))
        Me.tlpHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpHeader.Controls.Add(Me.lblTitle, 0, 0)
        Me.tlpHeader.Controls.Add(Me.lblOriginalTitle, 0, 1)
        Me.tlpHeader.Controls.Add(Me.pnlRating, 0, 2)
        Me.tlpHeader.Controls.Add(Me.lblTagline, 0, 3)
        Me.tlpHeader.Controls.Add(Me.lblRuntime, 2, 2)
        Me.tlpHeader.Controls.Add(Me.lblRating, 1, 2)
        Me.tlpHeader.Dock = System.Windows.Forms.DockStyle.Left
        Me.tlpHeader.Location = New System.Drawing.Point(0, 0)
        Me.tlpHeader.Name = "tlpHeader"
        Me.tlpHeader.RowCount = 4
        Me.tlpHeader.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpHeader.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpHeader.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpHeader.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tlpHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tlpHeader.Size = New System.Drawing.Size(346, 96)
        Me.tlpHeader.TabIndex = 39
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTitle.AutoSize = true
        Me.lblTitle.BackColor = System.Drawing.Color.Transparent
        Me.tlpHeader.SetColumnSpan(Me.lblTitle, 3)
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.Black
        Me.lblTitle.Location = New System.Drawing.Point(3, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.lblTitle.Size = New System.Drawing.Size(43, 22)
        Me.lblTitle.TabIndex = 25
        Me.lblTitle.Text = "Title"
        Me.lblTitle.UseMnemonic = false
        '
        'lblOriginalTitle
        '
        Me.lblOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblOriginalTitle.AutoSize = true
        Me.lblOriginalTitle.BackColor = System.Drawing.Color.Transparent
        Me.tlpHeader.SetColumnSpan(Me.lblOriginalTitle, 3)
        Me.lblOriginalTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblOriginalTitle.ForeColor = System.Drawing.Color.Black
        Me.lblOriginalTitle.Location = New System.Drawing.Point(3, 22)
        Me.lblOriginalTitle.Name = "lblOriginalTitle"
        Me.lblOriginalTitle.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.lblOriginalTitle.Size = New System.Drawing.Size(65, 16)
        Me.lblOriginalTitle.TabIndex = 38
        Me.lblOriginalTitle.Text = "OriginalTitle"
        Me.lblOriginalTitle.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblOriginalTitle.UseMnemonic = false
        '
        'pnlRating
        '
        Me.pnlRating.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.pnlRating.AutoSize = true
        Me.pnlRating.BackColor = System.Drawing.Color.Transparent
        Me.pnlRating.Controls.Add(Me.pbStar10)
        Me.pnlRating.Controls.Add(Me.pbStar9)
        Me.pnlRating.Controls.Add(Me.pbStar8)
        Me.pnlRating.Controls.Add(Me.pbStar7)
        Me.pnlRating.Controls.Add(Me.pbStar6)
        Me.pnlRating.Controls.Add(Me.pbStar5)
        Me.pnlRating.Controls.Add(Me.pbStar4)
        Me.pnlRating.Controls.Add(Me.pbStar3)
        Me.pnlRating.Controls.Add(Me.pbStar2)
        Me.pnlRating.Controls.Add(Me.pbStar1)
        Me.pnlRating.Location = New System.Drawing.Point(3, 41)
        Me.pnlRating.Name = "pnlRating"
        Me.pnlRating.Size = New System.Drawing.Size(244, 28)
        Me.pnlRating.TabIndex = 24
        '
        'pbStar10
        '
        Me.pbStar10.Location = New System.Drawing.Point(217, 1)
        Me.pbStar10.Name = "pbStar10"
        Me.pbStar10.Size = New System.Drawing.Size(24, 24)
        Me.pbStar10.TabIndex = 32
        Me.pbStar10.TabStop = false
        '
        'pbStar9
        '
        Me.pbStar9.Location = New System.Drawing.Point(193, 1)
        Me.pbStar9.Name = "pbStar9"
        Me.pbStar9.Size = New System.Drawing.Size(24, 24)
        Me.pbStar9.TabIndex = 31
        Me.pbStar9.TabStop = false
        '
        'pbStar8
        '
        Me.pbStar8.Location = New System.Drawing.Point(169, 1)
        Me.pbStar8.Name = "pbStar8"
        Me.pbStar8.Size = New System.Drawing.Size(24, 24)
        Me.pbStar8.TabIndex = 30
        Me.pbStar8.TabStop = false
        '
        'pbStar7
        '
        Me.pbStar7.Location = New System.Drawing.Point(145, 1)
        Me.pbStar7.Name = "pbStar7"
        Me.pbStar7.Size = New System.Drawing.Size(24, 24)
        Me.pbStar7.TabIndex = 29
        Me.pbStar7.TabStop = false
        '
        'pbStar6
        '
        Me.pbStar6.Location = New System.Drawing.Point(121, 1)
        Me.pbStar6.Name = "pbStar6"
        Me.pbStar6.Size = New System.Drawing.Size(24, 24)
        Me.pbStar6.TabIndex = 28
        Me.pbStar6.TabStop = false
        '
        'pbStar5
        '
        Me.pbStar5.Location = New System.Drawing.Point(97, 1)
        Me.pbStar5.Name = "pbStar5"
        Me.pbStar5.Size = New System.Drawing.Size(24, 24)
        Me.pbStar5.TabIndex = 27
        Me.pbStar5.TabStop = false
        '
        'pbStar4
        '
        Me.pbStar4.Location = New System.Drawing.Point(73, 1)
        Me.pbStar4.Name = "pbStar4"
        Me.pbStar4.Size = New System.Drawing.Size(24, 24)
        Me.pbStar4.TabIndex = 26
        Me.pbStar4.TabStop = false
        '
        'pbStar3
        '
        Me.pbStar3.Location = New System.Drawing.Point(49, 1)
        Me.pbStar3.Name = "pbStar3"
        Me.pbStar3.Size = New System.Drawing.Size(24, 24)
        Me.pbStar3.TabIndex = 25
        Me.pbStar3.TabStop = false
        '
        'pbStar2
        '
        Me.pbStar2.Location = New System.Drawing.Point(25, 1)
        Me.pbStar2.Name = "pbStar2"
        Me.pbStar2.Size = New System.Drawing.Size(24, 24)
        Me.pbStar2.TabIndex = 24
        Me.pbStar2.TabStop = false
        '
        'pbStar1
        '
        Me.pbStar1.Location = New System.Drawing.Point(1, 1)
        Me.pbStar1.Name = "pbStar1"
        Me.pbStar1.Size = New System.Drawing.Size(24, 24)
        Me.pbStar1.TabIndex = 23
        Me.pbStar1.TabStop = false
        '
        'lblTagline
        '
        Me.lblTagline.AutoSize = true
        Me.lblTagline.BackColor = System.Drawing.Color.Transparent
        Me.tlpHeader.SetColumnSpan(Me.lblTagline, 3)
        Me.lblTagline.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTagline.ForeColor = System.Drawing.Color.Black
        Me.lblTagline.Location = New System.Drawing.Point(3, 72)
        Me.lblTagline.Name = "lblTagline"
        Me.lblTagline.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.lblTagline.Size = New System.Drawing.Size(45, 16)
        Me.lblTagline.TabIndex = 26
        Me.lblTagline.Text = "Tagline"
        Me.lblTagline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTagline.UseMnemonic = false
        '
        'lblRuntime
        '
        Me.lblRuntime.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRuntime.AutoSize = true
        Me.lblRuntime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblRuntime.ForeColor = System.Drawing.Color.Black
        Me.lblRuntime.Location = New System.Drawing.Point(297, 48)
        Me.lblRuntime.Name = "lblRuntime"
        Me.lblRuntime.Size = New System.Drawing.Size(46, 13)
        Me.lblRuntime.TabIndex = 32
        Me.lblRuntime.Text = "Runtime"
        Me.lblRuntime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRating
        '
        Me.lblRating.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRating.AutoSize = true
        Me.lblRating.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblRating.ForeColor = System.Drawing.Color.Black
        Me.lblRating.Location = New System.Drawing.Point(253, 48)
        Me.lblRating.Name = "lblRating"
        Me.lblRating.Size = New System.Drawing.Size(38, 13)
        Me.lblRating.TabIndex = 22
        Me.lblRating.Text = "Rating"
        Me.lblRating.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlInfoIcons
        '
        Me.pnlInfoIcons.BackColor = System.Drawing.Color.Transparent
        Me.pnlInfoIcons.Controls.Add(Me.pbSubtitleLang6)
        Me.pnlInfoIcons.Controls.Add(Me.pbSubtitleLang5)
        Me.pnlInfoIcons.Controls.Add(Me.pbSubtitleLang4)
        Me.pnlInfoIcons.Controls.Add(Me.pbSubtitleLang3)
        Me.pnlInfoIcons.Controls.Add(Me.pbSubtitleLang2)
        Me.pnlInfoIcons.Controls.Add(Me.pbSubtitleLang1)
        Me.pnlInfoIcons.Controls.Add(Me.pbSubtitleLang0)
        Me.pnlInfoIcons.Controls.Add(Me.pbAudioLang6)
        Me.pnlInfoIcons.Controls.Add(Me.pbAudioLang5)
        Me.pnlInfoIcons.Controls.Add(Me.pbAudioLang4)
        Me.pnlInfoIcons.Controls.Add(Me.pbAudioLang3)
        Me.pnlInfoIcons.Controls.Add(Me.pbAudioLang2)
        Me.pnlInfoIcons.Controls.Add(Me.pbAudioLang1)
        Me.pnlInfoIcons.Controls.Add(Me.pbAudioLang0)
        Me.pnlInfoIcons.Controls.Add(Me.lblStudio)
        Me.pnlInfoIcons.Controls.Add(Me.pbVType)
        Me.pnlInfoIcons.Controls.Add(Me.pbStudio)
        Me.pnlInfoIcons.Controls.Add(Me.pbVideo)
        Me.pnlInfoIcons.Controls.Add(Me.pbAudio)
        Me.pnlInfoIcons.Controls.Add(Me.pbResolution)
        Me.pnlInfoIcons.Controls.Add(Me.pbChannels)
        Me.pnlInfoIcons.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlInfoIcons.Location = New System.Drawing.Point(381, 0)
        Me.pnlInfoIcons.Name = "pnlInfoIcons"
        Me.pnlInfoIcons.Size = New System.Drawing.Size(390, 96)
        Me.pnlInfoIcons.TabIndex = 31
        '
        'pbSubtitleLang6
        '
        Me.pbSubtitleLang6.Location = New System.Drawing.Point(351, 70)
        Me.pbSubtitleLang6.Name = "pbSubtitleLang6"
        Me.pbSubtitleLang6.Size = New System.Drawing.Size(25, 25)
        Me.pbSubtitleLang6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSubtitleLang6.TabIndex = 53
        Me.pbSubtitleLang6.TabStop = false
        '
        'pbSubtitleLang5
        '
        Me.pbSubtitleLang5.Location = New System.Drawing.Point(325, 70)
        Me.pbSubtitleLang5.Name = "pbSubtitleLang5"
        Me.pbSubtitleLang5.Size = New System.Drawing.Size(25, 25)
        Me.pbSubtitleLang5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSubtitleLang5.TabIndex = 52
        Me.pbSubtitleLang5.TabStop = false
        '
        'pbSubtitleLang4
        '
        Me.pbSubtitleLang4.Location = New System.Drawing.Point(299, 70)
        Me.pbSubtitleLang4.Name = "pbSubtitleLang4"
        Me.pbSubtitleLang4.Size = New System.Drawing.Size(25, 25)
        Me.pbSubtitleLang4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSubtitleLang4.TabIndex = 51
        Me.pbSubtitleLang4.TabStop = false
        '
        'pbSubtitleLang3
        '
        Me.pbSubtitleLang3.Location = New System.Drawing.Point(273, 70)
        Me.pbSubtitleLang3.Name = "pbSubtitleLang3"
        Me.pbSubtitleLang3.Size = New System.Drawing.Size(25, 25)
        Me.pbSubtitleLang3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSubtitleLang3.TabIndex = 50
        Me.pbSubtitleLang3.TabStop = false
        '
        'pbSubtitleLang2
        '
        Me.pbSubtitleLang2.Location = New System.Drawing.Point(247, 70)
        Me.pbSubtitleLang2.Name = "pbSubtitleLang2"
        Me.pbSubtitleLang2.Size = New System.Drawing.Size(25, 25)
        Me.pbSubtitleLang2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSubtitleLang2.TabIndex = 49
        Me.pbSubtitleLang2.TabStop = false
        '
        'pbSubtitleLang1
        '
        Me.pbSubtitleLang1.Location = New System.Drawing.Point(221, 70)
        Me.pbSubtitleLang1.Name = "pbSubtitleLang1"
        Me.pbSubtitleLang1.Size = New System.Drawing.Size(25, 25)
        Me.pbSubtitleLang1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSubtitleLang1.TabIndex = 48
        Me.pbSubtitleLang1.TabStop = false
        '
        'pbSubtitleLang0
        '
        Me.pbSubtitleLang0.Location = New System.Drawing.Point(195, 70)
        Me.pbSubtitleLang0.Name = "pbSubtitleLang0"
        Me.pbSubtitleLang0.Size = New System.Drawing.Size(25, 25)
        Me.pbSubtitleLang0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSubtitleLang0.TabIndex = 47
        Me.pbSubtitleLang0.TabStop = false
        '
        'pbAudioLang6
        '
        Me.pbAudioLang6.Location = New System.Drawing.Point(156, 70)
        Me.pbAudioLang6.Name = "pbAudioLang6"
        Me.pbAudioLang6.Size = New System.Drawing.Size(25, 25)
        Me.pbAudioLang6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudioLang6.TabIndex = 46
        Me.pbAudioLang6.TabStop = false
        '
        'pbAudioLang5
        '
        Me.pbAudioLang5.Location = New System.Drawing.Point(130, 70)
        Me.pbAudioLang5.Name = "pbAudioLang5"
        Me.pbAudioLang5.Size = New System.Drawing.Size(25, 25)
        Me.pbAudioLang5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudioLang5.TabIndex = 45
        Me.pbAudioLang5.TabStop = false
        '
        'pbAudioLang4
        '
        Me.pbAudioLang4.Location = New System.Drawing.Point(104, 70)
        Me.pbAudioLang4.Name = "pbAudioLang4"
        Me.pbAudioLang4.Size = New System.Drawing.Size(25, 25)
        Me.pbAudioLang4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudioLang4.TabIndex = 44
        Me.pbAudioLang4.TabStop = false
        '
        'pbAudioLang3
        '
        Me.pbAudioLang3.Location = New System.Drawing.Point(78, 70)
        Me.pbAudioLang3.Name = "pbAudioLang3"
        Me.pbAudioLang3.Size = New System.Drawing.Size(25, 25)
        Me.pbAudioLang3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudioLang3.TabIndex = 43
        Me.pbAudioLang3.TabStop = false
        '
        'pbAudioLang2
        '
        Me.pbAudioLang2.Location = New System.Drawing.Point(52, 70)
        Me.pbAudioLang2.Name = "pbAudioLang2"
        Me.pbAudioLang2.Size = New System.Drawing.Size(25, 25)
        Me.pbAudioLang2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudioLang2.TabIndex = 42
        Me.pbAudioLang2.TabStop = false
        '
        'pbAudioLang1
        '
        Me.pbAudioLang1.Location = New System.Drawing.Point(26, 70)
        Me.pbAudioLang1.Name = "pbAudioLang1"
        Me.pbAudioLang1.Size = New System.Drawing.Size(25, 25)
        Me.pbAudioLang1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudioLang1.TabIndex = 41
        Me.pbAudioLang1.TabStop = false
        '
        'pbAudioLang0
        '
        Me.pbAudioLang0.Location = New System.Drawing.Point(0, 70)
        Me.pbAudioLang0.Name = "pbAudioLang0"
        Me.pbAudioLang0.Size = New System.Drawing.Size(25, 25)
        Me.pbAudioLang0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudioLang0.TabIndex = 40
        Me.pbAudioLang0.TabStop = false
        '
        'lblStudio
        '
        Me.lblStudio.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblStudio.Location = New System.Drawing.Point(220, 5)
        Me.lblStudio.Name = "lblStudio"
        Me.lblStudio.Size = New System.Drawing.Size(167, 18)
        Me.lblStudio.TabIndex = 37
        Me.lblStudio.Text = "           "
        Me.lblStudio.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'pbVType
        '
        Me.pbVType.BackColor = System.Drawing.Color.Transparent
        Me.pbVType.Location = New System.Drawing.Point(65, 26)
        Me.pbVType.Name = "pbVType"
        Me.pbVType.Size = New System.Drawing.Size(64, 44)
        Me.pbVType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbVType.TabIndex = 36
        Me.pbVType.TabStop = false
        '
        'pbStudio
        '
        Me.pbStudio.BackColor = System.Drawing.Color.Transparent
        Me.pbStudio.Location = New System.Drawing.Point(325, 26)
        Me.pbStudio.Name = "pbStudio"
        Me.pbStudio.Size = New System.Drawing.Size(64, 44)
        Me.pbStudio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbStudio.TabIndex = 31
        Me.pbStudio.TabStop = false
        '
        'pbVideo
        '
        Me.pbVideo.BackColor = System.Drawing.Color.Transparent
        Me.pbVideo.Location = New System.Drawing.Point(0, 26)
        Me.pbVideo.Name = "pbVideo"
        Me.pbVideo.Size = New System.Drawing.Size(64, 44)
        Me.pbVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbVideo.TabIndex = 33
        Me.pbVideo.TabStop = false
        '
        'pbAudio
        '
        Me.pbAudio.BackColor = System.Drawing.Color.Transparent
        Me.pbAudio.Location = New System.Drawing.Point(195, 26)
        Me.pbAudio.Name = "pbAudio"
        Me.pbAudio.Size = New System.Drawing.Size(64, 44)
        Me.pbAudio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudio.TabIndex = 35
        Me.pbAudio.TabStop = false
        '
        'pbResolution
        '
        Me.pbResolution.BackColor = System.Drawing.Color.Transparent
        Me.pbResolution.Location = New System.Drawing.Point(130, 26)
        Me.pbResolution.Name = "pbResolution"
        Me.pbResolution.Size = New System.Drawing.Size(64, 44)
        Me.pbResolution.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbResolution.TabIndex = 34
        Me.pbResolution.TabStop = false
        '
        'pbChannels
        '
        Me.pbChannels.BackColor = System.Drawing.Color.Transparent
        Me.pbChannels.Location = New System.Drawing.Point(260, 26)
        Me.pbChannels.Name = "pbChannels"
        Me.pbChannels.Size = New System.Drawing.Size(64, 44)
        Me.pbChannels.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbChannels.TabIndex = 32
        Me.pbChannels.TabStop = false
        '
        'pnlCancel
        '
        Me.pnlCancel.BackColor = System.Drawing.Color.LightGray
        Me.pnlCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCancel.Controls.Add(Me.prbCanceling)
        Me.pnlCancel.Controls.Add(Me.lblCanceling)
        Me.pnlCancel.Controls.Add(Me.btnCancel)
        Me.pnlCancel.Location = New System.Drawing.Point(273, 124)
        Me.pnlCancel.Name = "pnlCancel"
        Me.pnlCancel.Size = New System.Drawing.Size(214, 63)
        Me.pnlCancel.TabIndex = 8
        Me.pnlCancel.Visible = false
        '
        'prbCanceling
        '
        Me.prbCanceling.Location = New System.Drawing.Point(5, 32)
        Me.prbCanceling.MarqueeAnimationSpeed = 25
        Me.prbCanceling.Name = "prbCanceling"
        Me.prbCanceling.Size = New System.Drawing.Size(203, 20)
        Me.prbCanceling.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.prbCanceling.TabIndex = 2
        Me.prbCanceling.Visible = false
        '
        'lblCanceling
        '
        Me.lblCanceling.AutoSize = true
        Me.lblCanceling.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblCanceling.Location = New System.Drawing.Point(4, 12)
        Me.lblCanceling.Name = "lblCanceling"
        Me.lblCanceling.Size = New System.Drawing.Size(128, 17)
        Me.lblCanceling.TabIndex = 1
        Me.lblCanceling.Text = "Canceling Scraper..."
        Me.lblCanceling.Visible = false
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"),System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(4, 4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(205, 55)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.TabStop = false
        Me.btnCancel.Text = "Cancel Scraper"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = true
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
        Me.pnlNoInfo.Visible = false
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
        Me.pbNoInfo.Image = CType(resources.GetObject("pbNoInfo.Image"),System.Drawing.Image)
        Me.pbNoInfo.Location = New System.Drawing.Point(7, 38)
        Me.pbNoInfo.Name = "pbNoInfo"
        Me.pbNoInfo.Size = New System.Drawing.Size(63, 63)
        Me.pbNoInfo.TabIndex = 1
        Me.pbNoInfo.TabStop = false
        '
        'lblNoInfo
        '
        Me.lblNoInfo.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
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
        Me.pnlInfoPanel.Controls.Add(Me.pnlMoviesInSet)
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
        Me.pnlInfoPanel.Size = New System.Drawing.Size(773, 342)
        Me.pnlInfoPanel.TabIndex = 10
        '
        'pnlMoviesInSet
        '
        Me.pnlMoviesInSet.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pnlMoviesInSet.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlMoviesInSet.Controls.Add(Me.lvMoviesInSet)
        Me.pnlMoviesInSet.Controls.Add(Me.lblMoviesInSetHeader)
        Me.pnlMoviesInSet.Location = New System.Drawing.Point(0, 493)
        Me.pnlMoviesInSet.Name = "pnlMoviesInSet"
        Me.pnlMoviesInSet.Size = New System.Drawing.Size(773, 244)
        Me.pnlMoviesInSet.TabIndex = 41
        '
        'lvMoviesInSet
        '
        Me.lvMoviesInSet.LargeImageList = Me.ilMoviesInSet
        Me.lvMoviesInSet.Location = New System.Drawing.Point(3, 23)
        Me.lvMoviesInSet.Name = "lvMoviesInSet"
        Me.lvMoviesInSet.Size = New System.Drawing.Size(765, 182)
        Me.lvMoviesInSet.TabIndex = 41
        Me.lvMoviesInSet.UseCompatibleStateImageBehavior = false
        '
        'ilMoviesInSet
        '
        Me.ilMoviesInSet.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ilMoviesInSet.ImageSize = New System.Drawing.Size(16, 16)
        Me.ilMoviesInSet.TransparentColor = System.Drawing.Color.Transparent
        '
        'lblMoviesInSetHeader
        '
        Me.lblMoviesInSetHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblMoviesInSetHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblMoviesInSetHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMoviesInSetHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMoviesInSetHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblMoviesInSetHeader.Location = New System.Drawing.Point(3, 3)
        Me.lblMoviesInSetHeader.Name = "lblMoviesInSetHeader"
        Me.lblMoviesInSetHeader.Size = New System.Drawing.Size(765, 17)
        Me.lblMoviesInSetHeader.TabIndex = 40
        Me.lblMoviesInSetHeader.Text = "Movies in Set"
        Me.lblMoviesInSetHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCerts
        '
        Me.txtCerts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtCerts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCerts.Location = New System.Drawing.Point(117, 208)
        Me.txtCerts.Name = "txtCerts"
        Me.txtCerts.ReadOnly = true
        Me.txtCerts.Size = New System.Drawing.Size(337, 22)
        Me.txtCerts.TabIndex = 3
        Me.txtCerts.TabStop = false
        '
        'lblCertsHeader
        '
        Me.lblCertsHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblCertsHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblCertsHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCertsHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblCertsHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblCertsHeader.Location = New System.Drawing.Point(117, 188)
        Me.lblCertsHeader.Name = "lblCertsHeader"
        Me.lblCertsHeader.Size = New System.Drawing.Size(337, 17)
        Me.lblCertsHeader.TabIndex = 2
        Me.lblCertsHeader.Text = "Certifications"
        Me.lblCertsHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblReleaseDate
        '
        Me.lblReleaseDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblReleaseDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblReleaseDate.ForeColor = System.Drawing.Color.Black
        Me.lblReleaseDate.Location = New System.Drawing.Point(288, 48)
        Me.lblReleaseDate.Name = "lblReleaseDate"
        Me.lblReleaseDate.Size = New System.Drawing.Size(105, 16)
        Me.lblReleaseDate.TabIndex = 39
        Me.lblReleaseDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReleaseDateHeader
        '
        Me.lblReleaseDateHeader.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblReleaseDateHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblReleaseDateHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReleaseDateHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblReleaseDateHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblReleaseDateHeader.Location = New System.Drawing.Point(288, 27)
        Me.lblReleaseDateHeader.Name = "lblReleaseDateHeader"
        Me.lblReleaseDateHeader.Size = New System.Drawing.Size(105, 17)
        Me.lblReleaseDateHeader.TabIndex = 38
        Me.lblReleaseDateHeader.Text = "Release Date"
        Me.lblReleaseDateHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnMid
        '
        Me.btnMid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnMid.BackColor = System.Drawing.SystemColors.Control
        Me.btnMid.Location = New System.Drawing.Point(702, 1)
        Me.btnMid.Name = "btnMid"
        Me.btnMid.Size = New System.Drawing.Size(30, 22)
        Me.btnMid.TabIndex = 37
        Me.btnMid.TabStop = false
        Me.btnMid.Text = "-"
        Me.btnMid.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnMid.UseVisualStyleBackColor = false
        '
        'pbMILoading
        '
        Me.pbMILoading.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pbMILoading.Image = CType(resources.GetObject("pbMILoading.Image"),System.Drawing.Image)
        Me.pbMILoading.Location = New System.Drawing.Point(604, 374)
        Me.pbMILoading.Name = "pbMILoading"
        Me.pbMILoading.Size = New System.Drawing.Size(41, 39)
        Me.pbMILoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbMILoading.TabIndex = 36
        Me.pbMILoading.TabStop = false
        Me.pbMILoading.Visible = false
        '
        'btnMetaDataRefresh
        '
        Me.btnMetaDataRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnMetaDataRefresh.Location = New System.Drawing.Point(691, 278)
        Me.btnMetaDataRefresh.Name = "btnMetaDataRefresh"
        Me.btnMetaDataRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnMetaDataRefresh.TabIndex = 9
        Me.btnMetaDataRefresh.TabStop = false
        Me.btnMetaDataRefresh.Text = "Refresh"
        Me.btnMetaDataRefresh.UseVisualStyleBackColor = true
        '
        'lblMetaDataHeader
        '
        Me.lblMetaDataHeader.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblMetaDataHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblMetaDataHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMetaDataHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMetaDataHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblMetaDataHeader.Location = New System.Drawing.Point(467, 282)
        Me.lblMetaDataHeader.Name = "lblMetaDataHeader"
        Me.lblMetaDataHeader.Size = New System.Drawing.Size(294, 17)
        Me.lblMetaDataHeader.TabIndex = 8
        Me.lblMetaDataHeader.Text = "Meta Data"
        Me.lblMetaDataHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMetaData
        '
        Me.txtMetaData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtMetaData.BackColor = System.Drawing.Color.Gainsboro
        Me.txtMetaData.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMetaData.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMetaData.ForeColor = System.Drawing.Color.Black
        Me.txtMetaData.Location = New System.Drawing.Point(467, 303)
        Me.txtMetaData.Multiline = true
        Me.txtMetaData.Name = "txtMetaData"
        Me.txtMetaData.ReadOnly = true
        Me.txtMetaData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMetaData.Size = New System.Drawing.Size(296, 184)
        Me.txtMetaData.TabIndex = 10
        Me.txtMetaData.TabStop = false
        '
        'btnPlay
        '
        Me.btnPlay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnPlay.Image = Global.Ember_Media_Manager.My.Resources.Resources.Play_Icon
        Me.btnPlay.Location = New System.Drawing.Point(435, 254)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(20, 20)
        Me.btnPlay.TabIndex = 6
        Me.btnPlay.TabStop = false
        Me.btnPlay.UseVisualStyleBackColor = true
        '
        'txtFilePath
        '
        Me.txtFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilePath.Location = New System.Drawing.Point(3, 254)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = true
        Me.txtFilePath.Size = New System.Drawing.Size(429, 22)
        Me.txtFilePath.TabIndex = 5
        Me.txtFilePath.TabStop = false
        '
        'lblFilePathHeader
        '
        Me.lblFilePathHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblFilePathHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblFilePathHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFilePathHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFilePathHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilePathHeader.Location = New System.Drawing.Point(3, 234)
        Me.lblFilePathHeader.Name = "lblFilePathHeader"
        Me.lblFilePathHeader.Size = New System.Drawing.Size(451, 17)
        Me.lblFilePathHeader.TabIndex = 4
        Me.lblFilePathHeader.Text = "File Path"
        Me.lblFilePathHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIMDBID
        '
        Me.txtIMDBID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIMDBID.Location = New System.Drawing.Point(3, 208)
        Me.txtIMDBID.Name = "txtIMDBID"
        Me.txtIMDBID.ReadOnly = true
        Me.txtIMDBID.Size = New System.Drawing.Size(108, 22)
        Me.txtIMDBID.TabIndex = 1
        Me.txtIMDBID.TabStop = false
        '
        'lblIMDBHeader
        '
        Me.lblIMDBHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblIMDBHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIMDBHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
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
        Me.lblDirector.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblDirector.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblDirector.ForeColor = System.Drawing.Color.Black
        Me.lblDirector.Location = New System.Drawing.Point(3, 48)
        Me.lblDirector.Name = "lblDirector"
        Me.lblDirector.Size = New System.Drawing.Size(280, 16)
        Me.lblDirector.TabIndex = 27
        Me.lblDirector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDirectorHeader
        '
        Me.lblDirectorHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblDirectorHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblDirectorHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDirectorHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblDirectorHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblDirectorHeader.Location = New System.Drawing.Point(3, 27)
        Me.lblDirectorHeader.Name = "lblDirectorHeader"
        Me.lblDirectorHeader.Size = New System.Drawing.Size(279, 17)
        Me.lblDirectorHeader.TabIndex = 21
        Me.lblDirectorHeader.Text = "Director"
        Me.lblDirectorHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlActors
        '
        Me.pnlActors.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pnlActors.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlActors.Controls.Add(Me.pbActLoad)
        Me.pnlActors.Controls.Add(Me.lstActors)
        Me.pnlActors.Controls.Add(Me.pbActors)
        Me.pnlActors.Controls.Add(Me.lblActorsHeader)
        Me.pnlActors.Location = New System.Drawing.Point(466, 29)
        Me.pnlActors.Name = "pnlActors"
        Me.pnlActors.Size = New System.Drawing.Size(302, 244)
        Me.pnlActors.TabIndex = 19
        '
        'pbActLoad
        '
        Me.pbActLoad.Image = CType(resources.GetObject("pbActLoad.Image"),System.Drawing.Image)
        Me.pbActLoad.Location = New System.Drawing.Point(240, 111)
        Me.pbActLoad.Name = "pbActLoad"
        Me.pbActLoad.Size = New System.Drawing.Size(41, 39)
        Me.pbActLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbActLoad.TabIndex = 26
        Me.pbActLoad.TabStop = false
        Me.pbActLoad.Visible = false
        '
        'lstActors
        '
        Me.lstActors.BackColor = System.Drawing.Color.Gainsboro
        Me.lstActors.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstActors.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lstActors.ForeColor = System.Drawing.Color.Black
        Me.lstActors.FormattingEnabled = true
        Me.lstActors.Location = New System.Drawing.Point(3, 21)
        Me.lstActors.Name = "lstActors"
        Me.lstActors.Size = New System.Drawing.Size(214, 221)
        Me.lstActors.TabIndex = 28
        Me.lstActors.TabStop = false
        '
        'pbActors
        '
        Me.pbActors.Image = Global.Ember_Media_Manager.My.Resources.Resources.actor_silhouette
        Me.pbActors.Location = New System.Drawing.Point(220, 75)
        Me.pbActors.Name = "pbActors"
        Me.pbActors.Size = New System.Drawing.Size(81, 106)
        Me.pbActors.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbActors.TabIndex = 27
        Me.pbActors.TabStop = false
        '
        'lblActorsHeader
        '
        Me.lblActorsHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblActorsHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblActorsHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
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
        Me.lblOutlineHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblOutlineHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblOutlineHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOutlineHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblOutlineHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblOutlineHeader.Location = New System.Drawing.Point(3, 81)
        Me.lblOutlineHeader.Name = "lblOutlineHeader"
        Me.lblOutlineHeader.Size = New System.Drawing.Size(451, 17)
        Me.lblOutlineHeader.TabIndex = 17
        Me.lblOutlineHeader.Text = "Plot Outline"
        Me.lblOutlineHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtOutline
        '
        Me.txtOutline.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtOutline.BackColor = System.Drawing.Color.Gainsboro
        Me.txtOutline.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtOutline.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtOutline.ForeColor = System.Drawing.Color.Black
        Me.txtOutline.Location = New System.Drawing.Point(3, 103)
        Me.txtOutline.Multiline = true
        Me.txtOutline.Name = "txtOutline"
        Me.txtOutline.ReadOnly = true
        Me.txtOutline.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOutline.Size = New System.Drawing.Size(451, 78)
        Me.txtOutline.TabIndex = 16
        Me.txtOutline.TabStop = false
        '
        'pnlTop250
        '
        Me.pnlTop250.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pnlTop250.Controls.Add(Me.lblTop250)
        Me.pnlTop250.Controls.Add(Me.pbTop250)
        Me.pnlTop250.Location = New System.Drawing.Point(397, 27)
        Me.pnlTop250.Name = "pnlTop250"
        Me.pnlTop250.Size = New System.Drawing.Size(56, 48)
        Me.pnlTop250.TabIndex = 15
        '
        'lblTop250
        '
        Me.lblTop250.BackColor = System.Drawing.Color.Gainsboro
        Me.lblTop250.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
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
        Me.pbTop250.Image = CType(resources.GetObject("pbTop250.Image"),System.Drawing.Image)
        Me.pbTop250.Location = New System.Drawing.Point(1, 1)
        Me.pbTop250.Name = "pbTop250"
        Me.pbTop250.Size = New System.Drawing.Size(54, 30)
        Me.pbTop250.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbTop250.TabIndex = 14
        Me.pbTop250.TabStop = false
        '
        'lblPlotHeader
        '
        Me.lblPlotHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblPlotHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblPlotHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPlotHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPlotHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblPlotHeader.Location = New System.Drawing.Point(3, 282)
        Me.lblPlotHeader.Name = "lblPlotHeader"
        Me.lblPlotHeader.Size = New System.Drawing.Size(451, 17)
        Me.lblPlotHeader.TabIndex = 6
        Me.lblPlotHeader.Text = "Plot"
        Me.lblPlotHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPlot
        '
        Me.txtPlot.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtPlot.BackColor = System.Drawing.Color.Gainsboro
        Me.txtPlot.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPlot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtPlot.ForeColor = System.Drawing.Color.Black
        Me.txtPlot.Location = New System.Drawing.Point(3, 303)
        Me.txtPlot.Multiline = true
        Me.txtPlot.Name = "txtPlot"
        Me.txtPlot.ReadOnly = true
        Me.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlot.Size = New System.Drawing.Size(451, 184)
        Me.txtPlot.TabIndex = 7
        Me.txtPlot.TabStop = false
        '
        'btnDown
        '
        Me.btnDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnDown.BackColor = System.Drawing.SystemColors.Control
        Me.btnDown.Location = New System.Drawing.Point(733, 1)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(30, 22)
        Me.btnDown.TabIndex = 6
        Me.btnDown.TabStop = false
        Me.btnDown.Text = "v"
        Me.btnDown.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDown.UseVisualStyleBackColor = false
        '
        'btnUp
        '
        Me.btnUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnUp.BackColor = System.Drawing.SystemColors.Control
        Me.btnUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnUp.Location = New System.Drawing.Point(670, 1)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(30, 22)
        Me.btnUp.TabIndex = 1
        Me.btnUp.TabStop = false
        Me.btnUp.Text = "^"
        Me.btnUp.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnUp.UseVisualStyleBackColor = false
        '
        'lblInfoPanelHeader
        '
        Me.lblInfoPanelHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblInfoPanelHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblInfoPanelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInfoPanelHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblInfoPanelHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblInfoPanelHeader.Location = New System.Drawing.Point(3, 3)
        Me.lblInfoPanelHeader.Name = "lblInfoPanelHeader"
        Me.lblInfoPanelHeader.Size = New System.Drawing.Size(765, 17)
        Me.lblInfoPanelHeader.TabIndex = 0
        Me.lblInfoPanelHeader.Text = "Info"
        Me.lblInfoPanelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlPoster
        '
        Me.pnlPoster.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPoster.Controls.Add(Me.pbPoster)
        Me.pnlPoster.Location = New System.Drawing.Point(9, 136)
        Me.pnlPoster.Name = "pnlPoster"
        Me.pnlPoster.Size = New System.Drawing.Size(131, 169)
        Me.pnlPoster.TabIndex = 2
        Me.pnlPoster.Visible = false
        '
        'pbPoster
        '
        Me.pbPoster.BackColor = System.Drawing.SystemColors.Control
        Me.pbPoster.Location = New System.Drawing.Point(4, 4)
        Me.pbPoster.Name = "pbPoster"
        Me.pbPoster.Size = New System.Drawing.Size(121, 159)
        Me.pbPoster.TabIndex = 0
        Me.pbPoster.TabStop = false
        '
        'pbPosterCache
        '
        Me.pbPosterCache.Location = New System.Drawing.Point(454, 107)
        Me.pbPosterCache.Name = "pbPosterCache"
        Me.pbPosterCache.Size = New System.Drawing.Size(115, 111)
        Me.pbPosterCache.TabIndex = 12
        Me.pbPosterCache.TabStop = false
        Me.pbPosterCache.Visible = false
        '
        'pbFanartSmallCache
        '
        Me.pbFanartSmallCache.Location = New System.Drawing.Point(697, 107)
        Me.pbFanartSmallCache.Name = "pbFanartSmallCache"
        Me.pbFanartSmallCache.Size = New System.Drawing.Size(115, 111)
        Me.pbFanartSmallCache.TabIndex = 15
        Me.pbFanartSmallCache.TabStop = false
        Me.pbFanartSmallCache.Visible = false
        '
        'pnlFanartSmall
        '
        Me.pnlFanartSmall.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlFanartSmall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFanartSmall.Controls.Add(Me.pbFanartSmall)
        Me.pnlFanartSmall.Location = New System.Drawing.Point(146, 136)
        Me.pnlFanartSmall.Name = "pnlFanartSmall"
        Me.pnlFanartSmall.Size = New System.Drawing.Size(293, 169)
        Me.pnlFanartSmall.TabIndex = 14
        Me.pnlFanartSmall.Visible = false
        '
        'pbFanartSmall
        '
        Me.pbFanartSmall.BackColor = System.Drawing.SystemColors.Control
        Me.pbFanartSmall.Location = New System.Drawing.Point(4, 4)
        Me.pbFanartSmall.Name = "pbFanartSmall"
        Me.pbFanartSmall.Size = New System.Drawing.Size(283, 159)
        Me.pbFanartSmall.TabIndex = 0
        Me.pbFanartSmall.TabStop = false
        '
        'pnlLandscape
        '
        Me.pnlLandscape.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlLandscape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlLandscape.Controls.Add(Me.pbLandscape)
        Me.pnlLandscape.Location = New System.Drawing.Point(445, 136)
        Me.pnlLandscape.Name = "pnlLandscape"
        Me.pnlLandscape.Size = New System.Drawing.Size(293, 169)
        Me.pnlLandscape.TabIndex = 15
        Me.pnlLandscape.Visible = false
        '
        'pbLandscape
        '
        Me.pbLandscape.BackColor = System.Drawing.SystemColors.Control
        Me.pbLandscape.Location = New System.Drawing.Point(4, 4)
        Me.pbLandscape.Name = "pbLandscape"
        Me.pbLandscape.Size = New System.Drawing.Size(283, 159)
        Me.pbLandscape.TabIndex = 0
        Me.pbLandscape.TabStop = false
        '
        'pbLandscapeCache
        '
        Me.pbLandscapeCache.Location = New System.Drawing.Point(697, 228)
        Me.pbLandscapeCache.Name = "pbLandscapeCache"
        Me.pbLandscapeCache.Size = New System.Drawing.Size(115, 111)
        Me.pbLandscapeCache.TabIndex = 16
        Me.pbLandscapeCache.TabStop = false
        Me.pbLandscapeCache.Visible = false
        '
        'pnlClearArt
        '
        Me.pnlClearArt.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlClearArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlClearArt.Controls.Add(Me.pbClearArt)
        Me.pnlClearArt.Location = New System.Drawing.Point(445, 306)
        Me.pnlClearArt.Name = "pnlClearArt"
        Me.pnlClearArt.Size = New System.Drawing.Size(293, 169)
        Me.pnlClearArt.TabIndex = 16
        Me.pnlClearArt.Visible = false
        '
        'pbClearArt
        '
        Me.pbClearArt.BackColor = System.Drawing.SystemColors.Control
        Me.pbClearArt.Location = New System.Drawing.Point(4, 4)
        Me.pbClearArt.Name = "pbClearArt"
        Me.pbClearArt.Size = New System.Drawing.Size(283, 159)
        Me.pbClearArt.TabIndex = 0
        Me.pbClearArt.TabStop = false
        '
        'pbClearArtCache
        '
        Me.pbClearArtCache.Location = New System.Drawing.Point(818, 107)
        Me.pbClearArtCache.Name = "pbClearArtCache"
        Me.pbClearArtCache.Size = New System.Drawing.Size(115, 111)
        Me.pbClearArtCache.TabIndex = 17
        Me.pbClearArtCache.TabStop = false
        Me.pbClearArtCache.Visible = false
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
        Me.pnlMPAA.Visible = false
        '
        'pbMPAA
        '
        Me.pbMPAA.Location = New System.Drawing.Point(1, 1)
        Me.pbMPAA.Name = "pbMPAA"
        Me.pbMPAA.Size = New System.Drawing.Size(249, 57)
        Me.pbMPAA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbMPAA.TabIndex = 13
        Me.pbMPAA.TabStop = false
        '
        'pbFanartCache
        '
        Me.pbFanartCache.Location = New System.Drawing.Point(576, 107)
        Me.pbFanartCache.Name = "pbFanartCache"
        Me.pbFanartCache.Size = New System.Drawing.Size(115, 111)
        Me.pbFanartCache.TabIndex = 3
        Me.pbFanartCache.TabStop = false
        Me.pbFanartCache.Visible = false
        '
        'pbFanart
        '
        Me.pbFanart.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pbFanart.BackColor = System.Drawing.Color.DimGray
        Me.pbFanart.Location = New System.Drawing.Point(38, 123)
        Me.pbFanart.Name = "pbFanart"
        Me.pbFanart.Size = New System.Drawing.Size(696, 250)
        Me.pbFanart.TabIndex = 1
        Me.pbFanart.TabStop = false
        '
        'tsMain
        '
        Me.tsMain.BackColor = System.Drawing.SystemColors.Control
        Me.tsMain.CanOverflow = false
        Me.tsMain.GripMargin = New System.Windows.Forms.Padding(0)
        Me.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuScrapeMovies, Me.mnuScrapeMovieSets, Me.mnuUpdate, Me.tsbMediaCenters})
        Me.tsMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.tsMain.Location = New System.Drawing.Point(0, 0)
        Me.tsMain.Name = "tsMain"
        Me.tsMain.Padding = New System.Windows.Forms.Padding(0)
        Me.tsMain.Size = New System.Drawing.Size(773, 25)
        Me.tsMain.Stretch = true
        Me.tsMain.TabIndex = 6
        '
        'mnuScrapeMovies
        '
        Me.mnuScrapeMovies.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieAll, Me.mnuMovieMiss, Me.mnuMovieNew, Me.mnuMovieMark, Me.mnuMovieFilter, Me.mnuMovieCustom, Me.mnuMovieRestart})
        Me.mnuScrapeMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.mnuScrapeMovies.Image = CType(resources.GetObject("mnuScrapeMovies.Image"),System.Drawing.Image)
        Me.mnuScrapeMovies.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuScrapeMovies.Name = "mnuScrapeMovies"
        Me.mnuScrapeMovies.Size = New System.Drawing.Size(109, 22)
        Me.mnuScrapeMovies.Text = "Scrape Movies"
        '
        'mnuMovieAll
        '
        Me.mnuMovieAll.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieAllAuto, Me.mnuMovieAllAsk, Me.mnuMovieAllSkip})
        Me.mnuMovieAll.Name = "mnuMovieAll"
        Me.mnuMovieAll.Size = New System.Drawing.Size(183, 22)
        Me.mnuMovieAll.Text = "All Movies"
        '
        'mnuMovieAllAuto
        '
        Me.mnuMovieAllAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieAllAutoAll, Me.mnuMovieAllAutoActor, Me.mnuMovieAllAutoBanner, Me.mnuMovieAllAutoClearArt, Me.mnuMovieAllAutoClearLogo, Me.mnuMovieAllAutoDiscArt, Me.mnuMovieAllAutoEFanarts, Me.mnuMovieAllAutoEThumbs, Me.mnuMovieAllAutoFanart, Me.mnuMovieAllAutoLandscape, Me.mnuMovieAllAutoMI, Me.mnuMovieAllAutoNfo, Me.mnuMovieAllAutoPoster, Me.mnuMovieAllAutoTheme, Me.mnuMovieAllAutoTrailer})
        Me.mnuMovieAllAuto.Name = "mnuMovieAllAuto"
        Me.mnuMovieAllAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieAllAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuMovieAllAutoAll
        '
        Me.mnuMovieAllAutoAll.Name = "mnuMovieAllAutoAll"
        Me.mnuMovieAllAutoAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoAll.Text = "All Items"
        '
        'mnuMovieAllAutoActor
        '
        Me.mnuMovieAllAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuMovieAllAutoActor.Name = "mnuMovieAllAutoActor"
        Me.mnuMovieAllAutoActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoActor.Text = "Actor Thumbs Only"
        '
        'mnuMovieAllAutoBanner
        '
        Me.mnuMovieAllAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieAllAutoBanner.Name = "mnuMovieAllAutoBanner"
        Me.mnuMovieAllAutoBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoBanner.Text = "Banner Only"
        '
        'mnuMovieAllAutoClearArt
        '
        Me.mnuMovieAllAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieAllAutoClearArt.Name = "mnuMovieAllAutoClearArt"
        Me.mnuMovieAllAutoClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoClearArt.Text = "ClearArt Only"
        '
        'mnuMovieAllAutoClearLogo
        '
        Me.mnuMovieAllAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieAllAutoClearLogo.Name = "mnuMovieAllAutoClearLogo"
        Me.mnuMovieAllAutoClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieAllAutoDiscArt
        '
        Me.mnuMovieAllAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieAllAutoDiscArt.Name = "mnuMovieAllAutoDiscArt"
        Me.mnuMovieAllAutoDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieAllAutoEFanarts
        '
        Me.mnuMovieAllAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieAllAutoEFanarts.Name = "mnuMovieAllAutoEFanarts"
        Me.mnuMovieAllAutoEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoEFanarts.Text = "Extrafanarts Only"
        '
        'mnuMovieAllAutoEThumbs
        '
        Me.mnuMovieAllAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieAllAutoEThumbs.Name = "mnuMovieAllAutoEThumbs"
        Me.mnuMovieAllAutoEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoEThumbs.Text = "Extrathumbs Only"
        '
        'mnuMovieAllAutoFanart
        '
        Me.mnuMovieAllAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieAllAutoFanart.Name = "mnuMovieAllAutoFanart"
        Me.mnuMovieAllAutoFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoFanart.Text = "Fanart Only"
        '
        'mnuMovieAllAutoLandscape
        '
        Me.mnuMovieAllAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieAllAutoLandscape.Name = "mnuMovieAllAutoLandscape"
        Me.mnuMovieAllAutoLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoLandscape.Text = "Landscape Only"
        '
        'mnuMovieAllAutoMI
        '
        Me.mnuMovieAllAutoMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuMovieAllAutoMI.Name = "mnuMovieAllAutoMI"
        Me.mnuMovieAllAutoMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoMI.Text = "Meta Data Only"
        '
        'mnuMovieAllAutoNfo
        '
        Me.mnuMovieAllAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieAllAutoNfo.Name = "mnuMovieAllAutoNfo"
        Me.mnuMovieAllAutoNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoNfo.Text = "NFO Only"
        '
        'mnuMovieAllAutoPoster
        '
        Me.mnuMovieAllAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieAllAutoPoster.Name = "mnuMovieAllAutoPoster"
        Me.mnuMovieAllAutoPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoPoster.Text = "Poster Only"
        '
        'mnuMovieAllAutoTheme
        '
        Me.mnuMovieAllAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuMovieAllAutoTheme.Name = "mnuMovieAllAutoTheme"
        Me.mnuMovieAllAutoTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoTheme.Text = "Theme Only"
        '
        'mnuMovieAllAutoTrailer
        '
        Me.mnuMovieAllAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuMovieAllAutoTrailer.Name = "mnuMovieAllAutoTrailer"
        Me.mnuMovieAllAutoTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAutoTrailer.Text = "Trailer Only"
        '
        'mnuMovieAllAsk
        '
        Me.mnuMovieAllAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieAllAskAll, Me.mnuMovieAllAskActor, Me.mnuMovieAllAskBanner, Me.mnuMovieAllAskClearArt, Me.mnuMovieAllAskClearLogo, Me.mnuMovieAllAskDiscArt, Me.mnuMovieAllAskEFanarts, Me.mnuMovieAllAskEThumbs, Me.mnuMovieAllAskFanart, Me.mnuMovieAllAskLandscape, Me.mnuMovieAllAskMI, Me.mnuMovieAllAskNfo, Me.mnuMovieAllAskPoster, Me.mnuMovieAllAskTheme, Me.mnuMovieAllAskTrailer})
        Me.mnuMovieAllAsk.Name = "mnuMovieAllAsk"
        Me.mnuMovieAllAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieAllAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuMovieAllAskAll
        '
        Me.mnuMovieAllAskAll.Name = "mnuMovieAllAskAll"
        Me.mnuMovieAllAskAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskAll.Text = "All Items"
        '
        'mnuMovieAllAskActor
        '
        Me.mnuMovieAllAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuMovieAllAskActor.Name = "mnuMovieAllAskActor"
        Me.mnuMovieAllAskActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskActor.Text = "Actor Thumbs Only"
        '
        'mnuMovieAllAskBanner
        '
        Me.mnuMovieAllAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieAllAskBanner.Name = "mnuMovieAllAskBanner"
        Me.mnuMovieAllAskBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskBanner.Text = "Banner Only"
        '
        'mnuMovieAllAskClearArt
        '
        Me.mnuMovieAllAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieAllAskClearArt.Name = "mnuMovieAllAskClearArt"
        Me.mnuMovieAllAskClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskClearArt.Text = "ClearArt Only"
        '
        'mnuMovieAllAskClearLogo
        '
        Me.mnuMovieAllAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieAllAskClearLogo.Name = "mnuMovieAllAskClearLogo"
        Me.mnuMovieAllAskClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieAllAskDiscArt
        '
        Me.mnuMovieAllAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieAllAskDiscArt.Name = "mnuMovieAllAskDiscArt"
        Me.mnuMovieAllAskDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieAllAskEFanarts
        '
        Me.mnuMovieAllAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieAllAskEFanarts.Name = "mnuMovieAllAskEFanarts"
        Me.mnuMovieAllAskEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskEFanarts.Text = "Extrafanarts Only"
        '
        'mnuMovieAllAskEThumbs
        '
        Me.mnuMovieAllAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieAllAskEThumbs.Name = "mnuMovieAllAskEThumbs"
        Me.mnuMovieAllAskEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskEThumbs.Text = "Extrathumbs Only"
        '
        'mnuMovieAllAskFanart
        '
        Me.mnuMovieAllAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieAllAskFanart.Name = "mnuMovieAllAskFanart"
        Me.mnuMovieAllAskFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskFanart.Text = "Fanart Only"
        '
        'mnuMovieAllAskLandscape
        '
        Me.mnuMovieAllAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieAllAskLandscape.Name = "mnuMovieAllAskLandscape"
        Me.mnuMovieAllAskLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskLandscape.Text = "Landscape Only"
        '
        'mnuMovieAllAskMI
        '
        Me.mnuMovieAllAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuMovieAllAskMI.Name = "mnuMovieAllAskMI"
        Me.mnuMovieAllAskMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskMI.Text = "Meta Data Only"
        '
        'mnuMovieAllAskNfo
        '
        Me.mnuMovieAllAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieAllAskNfo.Name = "mnuMovieAllAskNfo"
        Me.mnuMovieAllAskNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskNfo.Text = "NFO Only"
        '
        'mnuMovieAllAskPoster
        '
        Me.mnuMovieAllAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieAllAskPoster.Name = "mnuMovieAllAskPoster"
        Me.mnuMovieAllAskPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskPoster.Text = "Poster Only"
        '
        'mnuMovieAllAskTheme
        '
        Me.mnuMovieAllAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuMovieAllAskTheme.Name = "mnuMovieAllAskTheme"
        Me.mnuMovieAllAskTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskTheme.Text = "Theme Only"
        '
        'mnuMovieAllAskTrailer
        '
        Me.mnuMovieAllAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuMovieAllAskTrailer.Name = "mnuMovieAllAskTrailer"
        Me.mnuMovieAllAskTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieAllAskTrailer.Text = "Trailer Only"
        '
        'mnuMovieAllSkip
        '
        Me.mnuMovieAllSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieAllSkipAll})
        Me.mnuMovieAllSkip.Name = "mnuMovieAllSkip"
        Me.mnuMovieAllSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieAllSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'mnuMovieAllSkipAll
        '
        Me.mnuMovieAllSkipAll.Name = "mnuMovieAllSkipAll"
        Me.mnuMovieAllSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuMovieAllSkipAll.Text = "All Items"
        '
        'mnuMovieMiss
        '
        Me.mnuMovieMiss.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieMissAuto, Me.mnuMovieMissAsk, Me.mnuMovieMissSkip})
        Me.mnuMovieMiss.Name = "mnuMovieMiss"
        Me.mnuMovieMiss.Size = New System.Drawing.Size(183, 22)
        Me.mnuMovieMiss.Text = "Movies Missing Items"
        '
        'mnuMovieMissAuto
        '
        Me.mnuMovieMissAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieMissAutoAll, Me.mnuMovieMissAutoActor, Me.mnuMovieMissAutoBanner, Me.mnuMovieMissAutoClearArt, Me.mnuMovieMissAutoClearLogo, Me.mnuMovieMissAutoDiscArt, Me.mnuMovieMissAutoEFanarts, Me.mnuMovieMissAutoEThumbs, Me.mnuMovieMissAutoFanart, Me.mnuMovieMissAutoLandscape, Me.mnuMovieMissAutoNfo, Me.mnuMovieMissAutoPoster, Me.mnuMovieMissAutoTheme, Me.mnuMovieMissAutoTrailer})
        Me.mnuMovieMissAuto.Name = "mnuMovieMissAuto"
        Me.mnuMovieMissAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieMissAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuMovieMissAutoAll
        '
        Me.mnuMovieMissAutoAll.Enabled = false
        Me.mnuMovieMissAutoAll.Name = "mnuMovieMissAutoAll"
        Me.mnuMovieMissAutoAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAutoAll.Text = "All Items"
        Me.mnuMovieMissAutoAll.Visible = false
        '
        'mnuMovieMissAutoActor
        '
        Me.mnuMovieMissAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuMovieMissAutoActor.Name = "mnuMovieMissAutoActor"
        Me.mnuMovieMissAutoActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAutoActor.Text = "Actor Thumbs Only"
        '
        'mnuMovieMissAutoBanner
        '
        Me.mnuMovieMissAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieMissAutoBanner.Name = "mnuMovieMissAutoBanner"
        Me.mnuMovieMissAutoBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAutoBanner.Text = "Banner Only"
        '
        'mnuMovieMissAutoClearArt
        '
        Me.mnuMovieMissAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieMissAutoClearArt.Name = "mnuMovieMissAutoClearArt"
        Me.mnuMovieMissAutoClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAutoClearArt.Text = "ClearArt Only"
        '
        'mnuMovieMissAutoClearLogo
        '
        Me.mnuMovieMissAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieMissAutoClearLogo.Name = "mnuMovieMissAutoClearLogo"
        Me.mnuMovieMissAutoClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieMissAutoDiscArt
        '
        Me.mnuMovieMissAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieMissAutoDiscArt.Name = "mnuMovieMissAutoDiscArt"
        Me.mnuMovieMissAutoDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieMissAutoEFanarts
        '
        Me.mnuMovieMissAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieMissAutoEFanarts.Name = "mnuMovieMissAutoEFanarts"
        Me.mnuMovieMissAutoEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAutoEFanarts.Text = "Extrafanarts Only"
        '
        'mnuMovieMissAutoEThumbs
        '
        Me.mnuMovieMissAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieMissAutoEThumbs.Name = "mnuMovieMissAutoEThumbs"
        Me.mnuMovieMissAutoEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAutoEThumbs.Text = "Extrathumbs Only"
        '
        'mnuMovieMissAutoFanart
        '
        Me.mnuMovieMissAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieMissAutoFanart.Name = "mnuMovieMissAutoFanart"
        Me.mnuMovieMissAutoFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAutoFanart.Text = "Fanart Only"
        '
        'mnuMovieMissAutoLandscape
        '
        Me.mnuMovieMissAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieMissAutoLandscape.Name = "mnuMovieMissAutoLandscape"
        Me.mnuMovieMissAutoLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAutoLandscape.Text = "Landscape Only"
        '
        'mnuMovieMissAutoNfo
        '
        Me.mnuMovieMissAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieMissAutoNfo.Name = "mnuMovieMissAutoNfo"
        Me.mnuMovieMissAutoNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAutoNfo.Text = "NFO Only"
        '
        'mnuMovieMissAutoPoster
        '
        Me.mnuMovieMissAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieMissAutoPoster.Name = "mnuMovieMissAutoPoster"
        Me.mnuMovieMissAutoPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAutoPoster.Text = "Poster Only"
        '
        'mnuMovieMissAutoTheme
        '
        Me.mnuMovieMissAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuMovieMissAutoTheme.Name = "mnuMovieMissAutoTheme"
        Me.mnuMovieMissAutoTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAutoTheme.Text = "Theme Only"
        '
        'mnuMovieMissAutoTrailer
        '
        Me.mnuMovieMissAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuMovieMissAutoTrailer.Name = "mnuMovieMissAutoTrailer"
        Me.mnuMovieMissAutoTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAutoTrailer.Text = "Trailer Only"
        '
        'mnuMovieMissAsk
        '
        Me.mnuMovieMissAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieMissAskAll, Me.mnuMovieMissAskActor, Me.mnuMovieMissAskBanner, Me.mnuMovieMissAskClearArt, Me.mnuMovieMissAskClearLogo, Me.mnuMovieMissAskDiscArt, Me.mnuMovieMissAskEFanarts, Me.mnuMovieMissAskEThumbs, Me.mnuMovieMissAskFanart, Me.mnuMovieMissAskLandscape, Me.mnuMovieMissAskNfo, Me.mnuMovieMissAskPoster, Me.mnuMovieMissAskTheme, Me.mnuMovieMissAskTrailer})
        Me.mnuMovieMissAsk.Name = "mnuMovieMissAsk"
        Me.mnuMovieMissAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieMissAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuMovieMissAskAll
        '
        Me.mnuMovieMissAskAll.Enabled = false
        Me.mnuMovieMissAskAll.Name = "mnuMovieMissAskAll"
        Me.mnuMovieMissAskAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAskAll.Text = "All Items"
        Me.mnuMovieMissAskAll.Visible = false
        '
        'mnuMovieMissAskActor
        '
        Me.mnuMovieMissAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuMovieMissAskActor.Name = "mnuMovieMissAskActor"
        Me.mnuMovieMissAskActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAskActor.Text = "Actor Thumbs Only"
        '
        'mnuMovieMissAskBanner
        '
        Me.mnuMovieMissAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieMissAskBanner.Name = "mnuMovieMissAskBanner"
        Me.mnuMovieMissAskBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAskBanner.Text = "Banner Only"
        '
        'mnuMovieMissAskClearArt
        '
        Me.mnuMovieMissAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieMissAskClearArt.Name = "mnuMovieMissAskClearArt"
        Me.mnuMovieMissAskClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAskClearArt.Text = "ClearArt Only"
        '
        'mnuMovieMissAskClearLogo
        '
        Me.mnuMovieMissAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieMissAskClearLogo.Name = "mnuMovieMissAskClearLogo"
        Me.mnuMovieMissAskClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieMissAskDiscArt
        '
        Me.mnuMovieMissAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieMissAskDiscArt.Name = "mnuMovieMissAskDiscArt"
        Me.mnuMovieMissAskDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAskDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieMissAskEFanarts
        '
        Me.mnuMovieMissAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieMissAskEFanarts.Name = "mnuMovieMissAskEFanarts"
        Me.mnuMovieMissAskEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAskEFanarts.Text = "Extrafanarts Only"
        '
        'mnuMovieMissAskEThumbs
        '
        Me.mnuMovieMissAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieMissAskEThumbs.Name = "mnuMovieMissAskEThumbs"
        Me.mnuMovieMissAskEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAskEThumbs.Text = "Extrathumbs Only"
        '
        'mnuMovieMissAskFanart
        '
        Me.mnuMovieMissAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieMissAskFanart.Name = "mnuMovieMissAskFanart"
        Me.mnuMovieMissAskFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAskFanart.Text = "Fanart Only"
        '
        'mnuMovieMissAskLandscape
        '
        Me.mnuMovieMissAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieMissAskLandscape.Name = "mnuMovieMissAskLandscape"
        Me.mnuMovieMissAskLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAskLandscape.Text = "Landscape Only"
        '
        'mnuMovieMissAskNfo
        '
        Me.mnuMovieMissAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieMissAskNfo.Name = "mnuMovieMissAskNfo"
        Me.mnuMovieMissAskNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAskNfo.Text = "NFO Only"
        '
        'mnuMovieMissAskPoster
        '
        Me.mnuMovieMissAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieMissAskPoster.Name = "mnuMovieMissAskPoster"
        Me.mnuMovieMissAskPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAskPoster.Text = "Poster Only"
        '
        'mnuMovieMissAskTheme
        '
        Me.mnuMovieMissAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuMovieMissAskTheme.Name = "mnuMovieMissAskTheme"
        Me.mnuMovieMissAskTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAskTheme.Text = "Theme Only"
        '
        'mnuMovieMissAskTrailer
        '
        Me.mnuMovieMissAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuMovieMissAskTrailer.Name = "mnuMovieMissAskTrailer"
        Me.mnuMovieMissAskTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMissAskTrailer.Text = "Trailer Only"
        '
        'mnuMovieMissSkip
        '
        Me.mnuMovieMissSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieMissSkipAll})
        Me.mnuMovieMissSkip.Enabled = false
        Me.mnuMovieMissSkip.Name = "mnuMovieMissSkip"
        Me.mnuMovieMissSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieMissSkip.Text = "Skip (Skip If More Than One Match)"
        Me.mnuMovieMissSkip.Visible = false
        '
        'mnuMovieMissSkipAll
        '
        Me.mnuMovieMissSkipAll.Name = "mnuMovieMissSkipAll"
        Me.mnuMovieMissSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuMovieMissSkipAll.Text = "All Items"
        '
        'mnuMovieNew
        '
        Me.mnuMovieNew.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieNewAuto, Me.mnuMovieNewAsk, Me.mnuMovieNewSkip})
        Me.mnuMovieNew.Name = "mnuMovieNew"
        Me.mnuMovieNew.Size = New System.Drawing.Size(183, 22)
        Me.mnuMovieNew.Text = "New Movies"
        '
        'mnuMovieNewAuto
        '
        Me.mnuMovieNewAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieNewAutoAll, Me.mnuMovieNewAutoActor, Me.mnuMovieNewAutoBanner, Me.mnuMovieNewAutoClearArt, Me.mnuMovieNewAutoClearLogo, Me.mnuMovieNewAutoDiscArt, Me.mnuMovieNewAutoEFanarts, Me.mnuMovieNewAutoEThumbs, Me.mnuMovieNewAutoFanart, Me.mnuMovieNewAutoLandscape, Me.mnuMovieNewAutoMI, Me.mnuMovieNewAutoNfo, Me.mnuMovieNewAutoPoster, Me.mnuMovieNewAutoTheme, Me.mnuMovieNewAutoTrailer})
        Me.mnuMovieNewAuto.Name = "mnuMovieNewAuto"
        Me.mnuMovieNewAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieNewAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuMovieNewAutoAll
        '
        Me.mnuMovieNewAutoAll.Name = "mnuMovieNewAutoAll"
        Me.mnuMovieNewAutoAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoAll.Text = "All Items"
        '
        'mnuMovieNewAutoActor
        '
        Me.mnuMovieNewAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuMovieNewAutoActor.Name = "mnuMovieNewAutoActor"
        Me.mnuMovieNewAutoActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoActor.Text = "Actor Thumbs Only"
        '
        'mnuMovieNewAutoBanner
        '
        Me.mnuMovieNewAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieNewAutoBanner.Name = "mnuMovieNewAutoBanner"
        Me.mnuMovieNewAutoBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoBanner.Text = "Banner Only"
        '
        'mnuMovieNewAutoClearArt
        '
        Me.mnuMovieNewAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieNewAutoClearArt.Name = "mnuMovieNewAutoClearArt"
        Me.mnuMovieNewAutoClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoClearArt.Text = "ClearArt Only"
        '
        'mnuMovieNewAutoClearLogo
        '
        Me.mnuMovieNewAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieNewAutoClearLogo.Name = "mnuMovieNewAutoClearLogo"
        Me.mnuMovieNewAutoClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieNewAutoDiscArt
        '
        Me.mnuMovieNewAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieNewAutoDiscArt.Name = "mnuMovieNewAutoDiscArt"
        Me.mnuMovieNewAutoDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieNewAutoEFanarts
        '
        Me.mnuMovieNewAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieNewAutoEFanarts.Name = "mnuMovieNewAutoEFanarts"
        Me.mnuMovieNewAutoEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoEFanarts.Text = "Extrafanarts Only"
        '
        'mnuMovieNewAutoEThumbs
        '
        Me.mnuMovieNewAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieNewAutoEThumbs.Name = "mnuMovieNewAutoEThumbs"
        Me.mnuMovieNewAutoEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoEThumbs.Text = "Extrathumbs Only"
        '
        'mnuMovieNewAutoFanart
        '
        Me.mnuMovieNewAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieNewAutoFanart.Name = "mnuMovieNewAutoFanart"
        Me.mnuMovieNewAutoFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoFanart.Text = "Fanart Only"
        '
        'mnuMovieNewAutoLandscape
        '
        Me.mnuMovieNewAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieNewAutoLandscape.Name = "mnuMovieNewAutoLandscape"
        Me.mnuMovieNewAutoLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoLandscape.Text = "Landscape Only"
        '
        'mnuMovieNewAutoMI
        '
        Me.mnuMovieNewAutoMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuMovieNewAutoMI.Name = "mnuMovieNewAutoMI"
        Me.mnuMovieNewAutoMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoMI.Text = "Meta Data Only"
        '
        'mnuMovieNewAutoNfo
        '
        Me.mnuMovieNewAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieNewAutoNfo.Name = "mnuMovieNewAutoNfo"
        Me.mnuMovieNewAutoNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoNfo.Text = "NFO Only"
        '
        'mnuMovieNewAutoPoster
        '
        Me.mnuMovieNewAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieNewAutoPoster.Name = "mnuMovieNewAutoPoster"
        Me.mnuMovieNewAutoPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoPoster.Text = "Poster Only"
        '
        'mnuMovieNewAutoTheme
        '
        Me.mnuMovieNewAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuMovieNewAutoTheme.Name = "mnuMovieNewAutoTheme"
        Me.mnuMovieNewAutoTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoTheme.Text = "Theme Only"
        '
        'mnuMovieNewAutoTrailer
        '
        Me.mnuMovieNewAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuMovieNewAutoTrailer.Name = "mnuMovieNewAutoTrailer"
        Me.mnuMovieNewAutoTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAutoTrailer.Text = "Trailer Only"
        '
        'mnuMovieNewAsk
        '
        Me.mnuMovieNewAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieNewAskAll, Me.mnuMovieNewAskActor, Me.mnuMovieNewAskBanner, Me.mnuMovieNewAskClearArt, Me.mnuMovieNewAskClearLogo, Me.mnuMovieNewAskDiscArt, Me.mnuMovieNewAskEFanarts, Me.mnuMovieNewAskEThumbs, Me.mnuMovieNewAskFanart, Me.mnuMovieNewAskLandscape, Me.mnuMovieNewAskMI, Me.mnuMovieNewAskNfo, Me.mnuMovieNewAskPoster, Me.mnuMovieNewAskTheme, Me.mnuMovieNewAskTrailer})
        Me.mnuMovieNewAsk.Name = "mnuMovieNewAsk"
        Me.mnuMovieNewAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieNewAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuMovieNewAskAll
        '
        Me.mnuMovieNewAskAll.Name = "mnuMovieNewAskAll"
        Me.mnuMovieNewAskAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskAll.Text = "All Items"
        '
        'mnuMovieNewAskActor
        '
        Me.mnuMovieNewAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuMovieNewAskActor.Name = "mnuMovieNewAskActor"
        Me.mnuMovieNewAskActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskActor.Text = "Actor Thumbs Only"
        '
        'mnuMovieNewAskBanner
        '
        Me.mnuMovieNewAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieNewAskBanner.Name = "mnuMovieNewAskBanner"
        Me.mnuMovieNewAskBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskBanner.Text = "Banner Only"
        '
        'mnuMovieNewAskClearArt
        '
        Me.mnuMovieNewAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieNewAskClearArt.Name = "mnuMovieNewAskClearArt"
        Me.mnuMovieNewAskClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskClearArt.Text = "ClearArt Only"
        '
        'mnuMovieNewAskClearLogo
        '
        Me.mnuMovieNewAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieNewAskClearLogo.Name = "mnuMovieNewAskClearLogo"
        Me.mnuMovieNewAskClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieNewAskDiscArt
        '
        Me.mnuMovieNewAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieNewAskDiscArt.Name = "mnuMovieNewAskDiscArt"
        Me.mnuMovieNewAskDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieNewAskEFanarts
        '
        Me.mnuMovieNewAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieNewAskEFanarts.Name = "mnuMovieNewAskEFanarts"
        Me.mnuMovieNewAskEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskEFanarts.Text = "Extrafanarts Only"
        '
        'mnuMovieNewAskEThumbs
        '
        Me.mnuMovieNewAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieNewAskEThumbs.Name = "mnuMovieNewAskEThumbs"
        Me.mnuMovieNewAskEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskEThumbs.Text = "Extrathumbs Only"
        '
        'mnuMovieNewAskFanart
        '
        Me.mnuMovieNewAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieNewAskFanart.Name = "mnuMovieNewAskFanart"
        Me.mnuMovieNewAskFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskFanart.Text = "Fanart Only"
        '
        'mnuMovieNewAskLandscape
        '
        Me.mnuMovieNewAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieNewAskLandscape.Name = "mnuMovieNewAskLandscape"
        Me.mnuMovieNewAskLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskLandscape.Text = "Landscape Only"
        '
        'mnuMovieNewAskMI
        '
        Me.mnuMovieNewAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuMovieNewAskMI.Name = "mnuMovieNewAskMI"
        Me.mnuMovieNewAskMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskMI.Text = "Meta Data Only"
        '
        'mnuMovieNewAskNfo
        '
        Me.mnuMovieNewAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieNewAskNfo.Name = "mnuMovieNewAskNfo"
        Me.mnuMovieNewAskNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskNfo.Text = "NFO Only"
        '
        'mnuMovieNewAskPoster
        '
        Me.mnuMovieNewAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieNewAskPoster.Name = "mnuMovieNewAskPoster"
        Me.mnuMovieNewAskPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskPoster.Text = "Poster Only"
        '
        'mnuMovieNewAskTheme
        '
        Me.mnuMovieNewAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuMovieNewAskTheme.Name = "mnuMovieNewAskTheme"
        Me.mnuMovieNewAskTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskTheme.Text = "Theme Only"
        '
        'mnuMovieNewAskTrailer
        '
        Me.mnuMovieNewAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuMovieNewAskTrailer.Name = "mnuMovieNewAskTrailer"
        Me.mnuMovieNewAskTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieNewAskTrailer.Text = "Trailer Only"
        '
        'mnuMovieNewSkip
        '
        Me.mnuMovieNewSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieNewSkipAll})
        Me.mnuMovieNewSkip.Name = "mnuMovieNewSkip"
        Me.mnuMovieNewSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieNewSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'mnuMovieNewSkipAll
        '
        Me.mnuMovieNewSkipAll.Name = "mnuMovieNewSkipAll"
        Me.mnuMovieNewSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuMovieNewSkipAll.Text = "All Items"
        '
        'mnuMovieMark
        '
        Me.mnuMovieMark.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieMarkAuto, Me.mnuMovieMarkAsk, Me.mnuMovieMarkSkip})
        Me.mnuMovieMark.Name = "mnuMovieMark"
        Me.mnuMovieMark.Size = New System.Drawing.Size(183, 22)
        Me.mnuMovieMark.Text = "Marked Movies"
        '
        'mnuMovieMarkAuto
        '
        Me.mnuMovieMarkAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieMarkAutoAll, Me.mnuMovieMarkAutoActor, Me.mnuMovieMarkAutoBanner, Me.mnuMovieMarkAutoClearArt, Me.mnuMovieMarkAutoClearLogo, Me.mnuMovieMarkAutoDiscArt, Me.mnuMovieMarkAutoEFanarts, Me.mnuMovieMarkAutoEThumbs, Me.mnuMovieMarkAutoFanart, Me.mnuMovieMarkAutoLandscape, Me.mnuMovieMarkAutoMI, Me.mnuMovieMarkAutoNfo, Me.mnuMovieMarkAutoPoster, Me.mnuMovieMarkAutoTheme, Me.mnuMovieMarkAutoTrailer})
        Me.mnuMovieMarkAuto.Name = "mnuMovieMarkAuto"
        Me.mnuMovieMarkAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieMarkAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuMovieMarkAutoAll
        '
        Me.mnuMovieMarkAutoAll.Name = "mnuMovieMarkAutoAll"
        Me.mnuMovieMarkAutoAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoAll.Text = "All Items"
        '
        'mnuMovieMarkAutoActor
        '
        Me.mnuMovieMarkAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuMovieMarkAutoActor.Name = "mnuMovieMarkAutoActor"
        Me.mnuMovieMarkAutoActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoActor.Text = "Actor Thumbs Only"
        '
        'mnuMovieMarkAutoBanner
        '
        Me.mnuMovieMarkAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieMarkAutoBanner.Name = "mnuMovieMarkAutoBanner"
        Me.mnuMovieMarkAutoBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoBanner.Text = "Banner Only"
        '
        'mnuMovieMarkAutoClearArt
        '
        Me.mnuMovieMarkAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieMarkAutoClearArt.Name = "mnuMovieMarkAutoClearArt"
        Me.mnuMovieMarkAutoClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoClearArt.Text = "ClearArt Only"
        '
        'mnuMovieMarkAutoClearLogo
        '
        Me.mnuMovieMarkAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieMarkAutoClearLogo.Name = "mnuMovieMarkAutoClearLogo"
        Me.mnuMovieMarkAutoClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieMarkAutoDiscArt
        '
        Me.mnuMovieMarkAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieMarkAutoDiscArt.Name = "mnuMovieMarkAutoDiscArt"
        Me.mnuMovieMarkAutoDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieMarkAutoEFanarts
        '
        Me.mnuMovieMarkAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieMarkAutoEFanarts.Name = "mnuMovieMarkAutoEFanarts"
        Me.mnuMovieMarkAutoEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoEFanarts.Text = "Extrafanarts Only"
        '
        'mnuMovieMarkAutoEThumbs
        '
        Me.mnuMovieMarkAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieMarkAutoEThumbs.Name = "mnuMovieMarkAutoEThumbs"
        Me.mnuMovieMarkAutoEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoEThumbs.Text = "Extrathumbs Only"
        '
        'mnuMovieMarkAutoFanart
        '
        Me.mnuMovieMarkAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieMarkAutoFanart.Name = "mnuMovieMarkAutoFanart"
        Me.mnuMovieMarkAutoFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoFanart.Text = "Fanart Only"
        '
        'mnuMovieMarkAutoLandscape
        '
        Me.mnuMovieMarkAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieMarkAutoLandscape.Name = "mnuMovieMarkAutoLandscape"
        Me.mnuMovieMarkAutoLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoLandscape.Text = "Landscape Only"
        '
        'mnuMovieMarkAutoMI
        '
        Me.mnuMovieMarkAutoMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuMovieMarkAutoMI.Name = "mnuMovieMarkAutoMI"
        Me.mnuMovieMarkAutoMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoMI.Text = "Meta Data Only"
        '
        'mnuMovieMarkAutoNfo
        '
        Me.mnuMovieMarkAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieMarkAutoNfo.Name = "mnuMovieMarkAutoNfo"
        Me.mnuMovieMarkAutoNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoNfo.Text = "NFO Only"
        '
        'mnuMovieMarkAutoPoster
        '
        Me.mnuMovieMarkAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieMarkAutoPoster.Name = "mnuMovieMarkAutoPoster"
        Me.mnuMovieMarkAutoPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoPoster.Text = "Poster Only"
        '
        'mnuMovieMarkAutoTheme
        '
        Me.mnuMovieMarkAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuMovieMarkAutoTheme.Name = "mnuMovieMarkAutoTheme"
        Me.mnuMovieMarkAutoTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoTheme.Text = "Theme Only"
        '
        'mnuMovieMarkAutoTrailer
        '
        Me.mnuMovieMarkAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuMovieMarkAutoTrailer.Name = "mnuMovieMarkAutoTrailer"
        Me.mnuMovieMarkAutoTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAutoTrailer.Text = "Trailer Only"
        '
        'mnuMovieMarkAsk
        '
        Me.mnuMovieMarkAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieMarkAskAll, Me.mnuMovieMarkAskActor, Me.mnuMovieMarkAskBanner, Me.mnuMovieMarkAskClearArt, Me.mnuMovieMarkAskClearLogo, Me.mnuMovieMarkAskDiscArt, Me.mnuMovieMarkAskEFanarts, Me.mnuMovieMarkAskEThumbs, Me.mnuMovieMarkAskFanart, Me.mnuMovieMarkAskLandscape, Me.mnuMovieMarkAskMI, Me.mnuMovieMarkAskNfo, Me.mnuMovieMarkAskPoster, Me.mnuMovieMarkAskTheme, Me.mnuMovieMarkAskTrailer})
        Me.mnuMovieMarkAsk.Name = "mnuMovieMarkAsk"
        Me.mnuMovieMarkAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieMarkAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuMovieMarkAskAll
        '
        Me.mnuMovieMarkAskAll.Name = "mnuMovieMarkAskAll"
        Me.mnuMovieMarkAskAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskAll.Text = "All Items"
        '
        'mnuMovieMarkAskActor
        '
        Me.mnuMovieMarkAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuMovieMarkAskActor.Name = "mnuMovieMarkAskActor"
        Me.mnuMovieMarkAskActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskActor.Text = "Actor Thumbs Only"
        '
        'mnuMovieMarkAskBanner
        '
        Me.mnuMovieMarkAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieMarkAskBanner.Name = "mnuMovieMarkAskBanner"
        Me.mnuMovieMarkAskBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskBanner.Text = "Banner Only"
        '
        'mnuMovieMarkAskClearArt
        '
        Me.mnuMovieMarkAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieMarkAskClearArt.Name = "mnuMovieMarkAskClearArt"
        Me.mnuMovieMarkAskClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskClearArt.Text = "ClearArt Only"
        '
        'mnuMovieMarkAskClearLogo
        '
        Me.mnuMovieMarkAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieMarkAskClearLogo.Name = "mnuMovieMarkAskClearLogo"
        Me.mnuMovieMarkAskClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieMarkAskDiscArt
        '
        Me.mnuMovieMarkAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieMarkAskDiscArt.Name = "mnuMovieMarkAskDiscArt"
        Me.mnuMovieMarkAskDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieMarkAskEFanarts
        '
        Me.mnuMovieMarkAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieMarkAskEFanarts.Name = "mnuMovieMarkAskEFanarts"
        Me.mnuMovieMarkAskEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskEFanarts.Text = "Extrafanarts Only"
        '
        'mnuMovieMarkAskEThumbs
        '
        Me.mnuMovieMarkAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieMarkAskEThumbs.Name = "mnuMovieMarkAskEThumbs"
        Me.mnuMovieMarkAskEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskEThumbs.Text = "Extrathumbs Only"
        '
        'mnuMovieMarkAskFanart
        '
        Me.mnuMovieMarkAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieMarkAskFanart.Name = "mnuMovieMarkAskFanart"
        Me.mnuMovieMarkAskFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskFanart.Text = "Fanart Only"
        '
        'mnuMovieMarkAskLandscape
        '
        Me.mnuMovieMarkAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieMarkAskLandscape.Name = "mnuMovieMarkAskLandscape"
        Me.mnuMovieMarkAskLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskLandscape.Text = "Landscape Only"
        '
        'mnuMovieMarkAskMI
        '
        Me.mnuMovieMarkAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuMovieMarkAskMI.Name = "mnuMovieMarkAskMI"
        Me.mnuMovieMarkAskMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskMI.Text = "Meta Data Only"
        '
        'mnuMovieMarkAskNfo
        '
        Me.mnuMovieMarkAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieMarkAskNfo.Name = "mnuMovieMarkAskNfo"
        Me.mnuMovieMarkAskNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskNfo.Text = "NFO Only"
        '
        'mnuMovieMarkAskPoster
        '
        Me.mnuMovieMarkAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieMarkAskPoster.Name = "mnuMovieMarkAskPoster"
        Me.mnuMovieMarkAskPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskPoster.Text = "Poster Only"
        '
        'mnuMovieMarkAskTheme
        '
        Me.mnuMovieMarkAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuMovieMarkAskTheme.Name = "mnuMovieMarkAskTheme"
        Me.mnuMovieMarkAskTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskTheme.Text = "Theme Only"
        '
        'mnuMovieMarkAskTrailer
        '
        Me.mnuMovieMarkAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuMovieMarkAskTrailer.Name = "mnuMovieMarkAskTrailer"
        Me.mnuMovieMarkAskTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieMarkAskTrailer.Text = "Trailer Only"
        '
        'mnuMovieMarkSkip
        '
        Me.mnuMovieMarkSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieMarkSkipAll})
        Me.mnuMovieMarkSkip.Name = "mnuMovieMarkSkip"
        Me.mnuMovieMarkSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieMarkSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'mnuMovieMarkSkipAll
        '
        Me.mnuMovieMarkSkipAll.Name = "mnuMovieMarkSkipAll"
        Me.mnuMovieMarkSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuMovieMarkSkipAll.Text = "All Items"
        '
        'mnuMovieFilter
        '
        Me.mnuMovieFilter.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieFilterAuto, Me.mnuMovieFilterAsk, Me.mnuMovieFilterSkip})
        Me.mnuMovieFilter.Name = "mnuMovieFilter"
        Me.mnuMovieFilter.Size = New System.Drawing.Size(183, 22)
        Me.mnuMovieFilter.Text = "Current Filter"
        '
        'mnuMovieFilterAuto
        '
        Me.mnuMovieFilterAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieFilterAutoAll, Me.mnuMovieFilterAutoActor, Me.mnuMovieFilterAutoBanner, Me.mnuMovieFilterAutoClearArt, Me.mnuMovieFilterAutoClearLogo, Me.mnuMovieFilterAutoDiscArt, Me.mnuMovieFilterAutoEFanarts, Me.mnuMovieFilterAutoEThumbs, Me.mnuMovieFilterAutoFanart, Me.mnuMovieFilterAutoLandscape, Me.mnuMovieFilterAutoMI, Me.mnuMovieFilterAutoNfo, Me.mnuMovieFilterAutoPoster, Me.mnuMovieFilterAutoTheme, Me.mnuMovieFilterAutoTrailer})
        Me.mnuMovieFilterAuto.Name = "mnuMovieFilterAuto"
        Me.mnuMovieFilterAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieFilterAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuMovieFilterAutoAll
        '
        Me.mnuMovieFilterAutoAll.Name = "mnuMovieFilterAutoAll"
        Me.mnuMovieFilterAutoAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoAll.Text = "All Items"
        '
        'mnuMovieFilterAutoActor
        '
        Me.mnuMovieFilterAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuMovieFilterAutoActor.Name = "mnuMovieFilterAutoActor"
        Me.mnuMovieFilterAutoActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoActor.Text = "Actor Thumbs Only"
        '
        'mnuMovieFilterAutoBanner
        '
        Me.mnuMovieFilterAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieFilterAutoBanner.Name = "mnuMovieFilterAutoBanner"
        Me.mnuMovieFilterAutoBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoBanner.Text = "Banner Only"
        '
        'mnuMovieFilterAutoClearArt
        '
        Me.mnuMovieFilterAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieFilterAutoClearArt.Name = "mnuMovieFilterAutoClearArt"
        Me.mnuMovieFilterAutoClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoClearArt.Text = "ClearArt Only"
        '
        'mnuMovieFilterAutoClearLogo
        '
        Me.mnuMovieFilterAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieFilterAutoClearLogo.Name = "mnuMovieFilterAutoClearLogo"
        Me.mnuMovieFilterAutoClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieFilterAutoDiscArt
        '
        Me.mnuMovieFilterAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieFilterAutoDiscArt.Name = "mnuMovieFilterAutoDiscArt"
        Me.mnuMovieFilterAutoDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieFilterAutoEFanarts
        '
        Me.mnuMovieFilterAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieFilterAutoEFanarts.Name = "mnuMovieFilterAutoEFanarts"
        Me.mnuMovieFilterAutoEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoEFanarts.Text = "Extrafanarts Only"
        '
        'mnuMovieFilterAutoEThumbs
        '
        Me.mnuMovieFilterAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieFilterAutoEThumbs.Name = "mnuMovieFilterAutoEThumbs"
        Me.mnuMovieFilterAutoEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoEThumbs.Text = "Extrathumbs Only"
        '
        'mnuMovieFilterAutoFanart
        '
        Me.mnuMovieFilterAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieFilterAutoFanart.Name = "mnuMovieFilterAutoFanart"
        Me.mnuMovieFilterAutoFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoFanart.Text = "Fanart Only"
        '
        'mnuMovieFilterAutoLandscape
        '
        Me.mnuMovieFilterAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieFilterAutoLandscape.Name = "mnuMovieFilterAutoLandscape"
        Me.mnuMovieFilterAutoLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoLandscape.Text = "Landscape Only"
        '
        'mnuMovieFilterAutoMI
        '
        Me.mnuMovieFilterAutoMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuMovieFilterAutoMI.Name = "mnuMovieFilterAutoMI"
        Me.mnuMovieFilterAutoMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoMI.Text = "Meta Data Only"
        '
        'mnuMovieFilterAutoNfo
        '
        Me.mnuMovieFilterAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieFilterAutoNfo.Name = "mnuMovieFilterAutoNfo"
        Me.mnuMovieFilterAutoNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoNfo.Text = "NFO Only"
        '
        'mnuMovieFilterAutoPoster
        '
        Me.mnuMovieFilterAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieFilterAutoPoster.Name = "mnuMovieFilterAutoPoster"
        Me.mnuMovieFilterAutoPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoPoster.Text = "Poster Only"
        '
        'mnuMovieFilterAutoTheme
        '
        Me.mnuMovieFilterAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuMovieFilterAutoTheme.Name = "mnuMovieFilterAutoTheme"
        Me.mnuMovieFilterAutoTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoTheme.Text = "Theme Only"
        '
        'mnuMovieFilterAutoTrailer
        '
        Me.mnuMovieFilterAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuMovieFilterAutoTrailer.Name = "mnuMovieFilterAutoTrailer"
        Me.mnuMovieFilterAutoTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAutoTrailer.Text = "Trailer Only"
        '
        'mnuMovieFilterAsk
        '
        Me.mnuMovieFilterAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieFilterAskAll, Me.mnuMovieFilterAskActor, Me.mnuMovieFilterAskBanner, Me.mnuMovieFilterAskClearArt, Me.mnuMovieFilterAskClearLogo, Me.mnuMovieFilterAskDiscArt, Me.mnuMovieFilterAskEFanarts, Me.mnuMovieFilterAskEThumbs, Me.mnuMovieFilterAskFanart, Me.mnuMovieFilterAskLandscape, Me.mnuMovieFilterAskMI, Me.mnuMovieFilterAskNfo, Me.mnuMovieFilterAskPoster, Me.mnuMovieFilterAskTheme, Me.mnuMovieFilterAskTrailer})
        Me.mnuMovieFilterAsk.Name = "mnuMovieFilterAsk"
        Me.mnuMovieFilterAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieFilterAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuMovieFilterAskAll
        '
        Me.mnuMovieFilterAskAll.Name = "mnuMovieFilterAskAll"
        Me.mnuMovieFilterAskAll.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskAll.Text = "All Items"
        '
        'mnuMovieFilterAskActor
        '
        Me.mnuMovieFilterAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuMovieFilterAskActor.Name = "mnuMovieFilterAskActor"
        Me.mnuMovieFilterAskActor.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskActor.Text = "Actor Thumbs Only"
        '
        'mnuMovieFilterAskBanner
        '
        Me.mnuMovieFilterAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieFilterAskBanner.Name = "mnuMovieFilterAskBanner"
        Me.mnuMovieFilterAskBanner.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskBanner.Text = "Banner Only"
        '
        'mnuMovieFilterAskClearArt
        '
        Me.mnuMovieFilterAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieFilterAskClearArt.Name = "mnuMovieFilterAskClearArt"
        Me.mnuMovieFilterAskClearArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskClearArt.Text = "ClearArt Only"
        '
        'mnuMovieFilterAskClearLogo
        '
        Me.mnuMovieFilterAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieFilterAskClearLogo.Name = "mnuMovieFilterAskClearLogo"
        Me.mnuMovieFilterAskClearLogo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieFilterAskDiscArt
        '
        Me.mnuMovieFilterAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieFilterAskDiscArt.Name = "mnuMovieFilterAskDiscArt"
        Me.mnuMovieFilterAskDiscArt.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieFilterAskEFanarts
        '
        Me.mnuMovieFilterAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieFilterAskEFanarts.Name = "mnuMovieFilterAskEFanarts"
        Me.mnuMovieFilterAskEFanarts.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskEFanarts.Text = "Extrafanarts Only"
        '
        'mnuMovieFilterAskEThumbs
        '
        Me.mnuMovieFilterAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieFilterAskEThumbs.Name = "mnuMovieFilterAskEThumbs"
        Me.mnuMovieFilterAskEThumbs.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskEThumbs.Text = "Extrathumbs Only"
        '
        'mnuMovieFilterAskFanart
        '
        Me.mnuMovieFilterAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieFilterAskFanart.Name = "mnuMovieFilterAskFanart"
        Me.mnuMovieFilterAskFanart.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskFanart.Text = "Fanart Only"
        '
        'mnuMovieFilterAskLandscape
        '
        Me.mnuMovieFilterAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieFilterAskLandscape.Name = "mnuMovieFilterAskLandscape"
        Me.mnuMovieFilterAskLandscape.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskLandscape.Text = "Landscape Only"
        '
        'mnuMovieFilterAskMI
        '
        Me.mnuMovieFilterAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuMovieFilterAskMI.Name = "mnuMovieFilterAskMI"
        Me.mnuMovieFilterAskMI.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskMI.Text = "Meta Data Only"
        '
        'mnuMovieFilterAskNfo
        '
        Me.mnuMovieFilterAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieFilterAskNfo.Name = "mnuMovieFilterAskNfo"
        Me.mnuMovieFilterAskNfo.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskNfo.Text = "NFO Only"
        '
        'mnuMovieFilterAskPoster
        '
        Me.mnuMovieFilterAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieFilterAskPoster.Name = "mnuMovieFilterAskPoster"
        Me.mnuMovieFilterAskPoster.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskPoster.Text = "Poster Only"
        '
        'mnuMovieFilterAskTheme
        '
        Me.mnuMovieFilterAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuMovieFilterAskTheme.Name = "mnuMovieFilterAskTheme"
        Me.mnuMovieFilterAskTheme.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskTheme.Text = "Theme Only"
        '
        'mnuMovieFilterAskTrailer
        '
        Me.mnuMovieFilterAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuMovieFilterAskTrailer.Name = "mnuMovieFilterAskTrailer"
        Me.mnuMovieFilterAskTrailer.Size = New System.Drawing.Size(171, 22)
        Me.mnuMovieFilterAskTrailer.Text = "Trailer Only"
        '
        'mnuMovieFilterSkip
        '
        Me.mnuMovieFilterSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieFilterSkipAll})
        Me.mnuMovieFilterSkip.Name = "mnuMovieFilterSkip"
        Me.mnuMovieFilterSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieFilterSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'mnuMovieFilterSkipAll
        '
        Me.mnuMovieFilterSkipAll.Name = "mnuMovieFilterSkipAll"
        Me.mnuMovieFilterSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuMovieFilterSkipAll.Text = "All Items"
        '
        'mnuMovieCustom
        '
        Me.mnuMovieCustom.Name = "mnuMovieCustom"
        Me.mnuMovieCustom.Size = New System.Drawing.Size(183, 22)
        Me.mnuMovieCustom.Text = "Custom Scraper..."
        '
        'mnuMovieRestart
        '
        Me.mnuMovieRestart.Name = "mnuMovieRestart"
        Me.mnuMovieRestart.Size = New System.Drawing.Size(183, 22)
        Me.mnuMovieRestart.Text = "Restart last scrape..."
        '
        'mnuScrapeMovieSets
        '
        Me.mnuScrapeMovieSets.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetAll, Me.mnuMovieSetMiss, Me.mnuMovieSetNew, Me.mnuMovieSetMark, Me.mnuMovieSetFilter, Me.mnuMovieSetCustom, Me.mnuMovieSetRestart})
        Me.mnuScrapeMovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.mnuScrapeMovieSets.Image = CType(resources.GetObject("mnuScrapeMovieSets.Image"),System.Drawing.Image)
        Me.mnuScrapeMovieSets.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuScrapeMovieSets.Name = "mnuScrapeMovieSets"
        Me.mnuScrapeMovieSets.Size = New System.Drawing.Size(125, 22)
        Me.mnuScrapeMovieSets.Text = "Scrape MovieSets"
        Me.mnuScrapeMovieSets.Visible = false
        '
        'mnuMovieSetAll
        '
        Me.mnuMovieSetAll.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetAllAuto, Me.mnuMovieSetAllAsk, Me.mnuMovieSetAllSkip})
        Me.mnuMovieSetAll.Name = "mnuMovieSetAll"
        Me.mnuMovieSetAll.Size = New System.Drawing.Size(199, 22)
        Me.mnuMovieSetAll.Text = "All MovieSets"
        '
        'mnuMovieSetAllAuto
        '
        Me.mnuMovieSetAllAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetAllAutoAll, Me.mnuMovieSetAllAutoBanner, Me.mnuMovieSetAllAutoClearArt, Me.mnuMovieSetAllAutoClearLogo, Me.mnuMovieSetAllAutoDiscArt, Me.mnuMovieSetAllAutoEFanarts, Me.mnuMovieSetAllAutoEThumbs, Me.mnuMovieSetAllAutoFanart, Me.mnuMovieSetAllAutoLandscape, Me.mnuMovieSetAllAutoNfo, Me.mnuMovieSetAllAutoPoster})
        Me.mnuMovieSetAllAuto.Name = "mnuMovieSetAllAuto"
        Me.mnuMovieSetAllAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetAllAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuMovieSetAllAutoAll
        '
        Me.mnuMovieSetAllAutoAll.Name = "mnuMovieSetAllAutoAll"
        Me.mnuMovieSetAllAutoAll.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAutoAll.Text = "All Items"
        '
        'mnuMovieSetAllAutoBanner
        '
        Me.mnuMovieSetAllAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieSetAllAutoBanner.Name = "mnuMovieSetAllAutoBanner"
        Me.mnuMovieSetAllAutoBanner.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAutoBanner.Text = "Banner Only"
        '
        'mnuMovieSetAllAutoClearArt
        '
        Me.mnuMovieSetAllAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieSetAllAutoClearArt.Name = "mnuMovieSetAllAutoClearArt"
        Me.mnuMovieSetAllAutoClearArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAutoClearArt.Text = "ClearArt Only"
        '
        'mnuMovieSetAllAutoClearLogo
        '
        Me.mnuMovieSetAllAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieSetAllAutoClearLogo.Name = "mnuMovieSetAllAutoClearLogo"
        Me.mnuMovieSetAllAutoClearLogo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieSetAllAutoDiscArt
        '
        Me.mnuMovieSetAllAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieSetAllAutoDiscArt.Name = "mnuMovieSetAllAutoDiscArt"
        Me.mnuMovieSetAllAutoDiscArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieSetAllAutoEFanarts
        '
        Me.mnuMovieSetAllAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieSetAllAutoEFanarts.Name = "mnuMovieSetAllAutoEFanarts"
        Me.mnuMovieSetAllAutoEFanarts.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAutoEFanarts.Text = "Extrafanarts Only"
        Me.mnuMovieSetAllAutoEFanarts.Visible = false
        '
        'mnuMovieSetAllAutoEThumbs
        '
        Me.mnuMovieSetAllAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieSetAllAutoEThumbs.Name = "mnuMovieSetAllAutoEThumbs"
        Me.mnuMovieSetAllAutoEThumbs.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAutoEThumbs.Text = "Extrathumbs Only"
        Me.mnuMovieSetAllAutoEThumbs.Visible = false
        '
        'mnuMovieSetAllAutoFanart
        '
        Me.mnuMovieSetAllAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieSetAllAutoFanart.Name = "mnuMovieSetAllAutoFanart"
        Me.mnuMovieSetAllAutoFanart.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAutoFanart.Text = "Fanart Only"
        '
        'mnuMovieSetAllAutoLandscape
        '
        Me.mnuMovieSetAllAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieSetAllAutoLandscape.Name = "mnuMovieSetAllAutoLandscape"
        Me.mnuMovieSetAllAutoLandscape.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAutoLandscape.Text = "Landscape Only"
        '
        'mnuMovieSetAllAutoNfo
        '
        Me.mnuMovieSetAllAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieSetAllAutoNfo.Name = "mnuMovieSetAllAutoNfo"
        Me.mnuMovieSetAllAutoNfo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAutoNfo.Text = "NFO Only"
        '
        'mnuMovieSetAllAutoPoster
        '
        Me.mnuMovieSetAllAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieSetAllAutoPoster.Name = "mnuMovieSetAllAutoPoster"
        Me.mnuMovieSetAllAutoPoster.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAutoPoster.Text = "Poster Only"
        '
        'mnuMovieSetAllAsk
        '
        Me.mnuMovieSetAllAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetAllAskAll, Me.mnuMovieSetAllAskBanner, Me.mnuMovieSetAllAskClearArt, Me.mnuMovieSetAllAskClearLogo, Me.mnuMovieSetAllAskDiscArt, Me.mnuMovieSetAllAskEFanarts, Me.mnuMovieSetAllAskEThumbs, Me.mnuMovieSetAllAskFanart, Me.mnuMovieSetAllAskLandscape, Me.mnuMovieSetAllAskNfo, Me.mnuMovieSetAllAskPoster})
        Me.mnuMovieSetAllAsk.Name = "mnuMovieSetAllAsk"
        Me.mnuMovieSetAllAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetAllAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuMovieSetAllAskAll
        '
        Me.mnuMovieSetAllAskAll.Name = "mnuMovieSetAllAskAll"
        Me.mnuMovieSetAllAskAll.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAskAll.Text = "All Items"
        '
        'mnuMovieSetAllAskBanner
        '
        Me.mnuMovieSetAllAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieSetAllAskBanner.Name = "mnuMovieSetAllAskBanner"
        Me.mnuMovieSetAllAskBanner.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAskBanner.Text = "Banner Only"
        '
        'mnuMovieSetAllAskClearArt
        '
        Me.mnuMovieSetAllAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieSetAllAskClearArt.Name = "mnuMovieSetAllAskClearArt"
        Me.mnuMovieSetAllAskClearArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAskClearArt.Text = "ClearArt Only"
        '
        'mnuMovieSetAllAskClearLogo
        '
        Me.mnuMovieSetAllAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieSetAllAskClearLogo.Name = "mnuMovieSetAllAskClearLogo"
        Me.mnuMovieSetAllAskClearLogo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieSetAllAskDiscArt
        '
        Me.mnuMovieSetAllAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieSetAllAskDiscArt.Name = "mnuMovieSetAllAskDiscArt"
        Me.mnuMovieSetAllAskDiscArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAskDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieSetAllAskEFanarts
        '
        Me.mnuMovieSetAllAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieSetAllAskEFanarts.Name = "mnuMovieSetAllAskEFanarts"
        Me.mnuMovieSetAllAskEFanarts.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAskEFanarts.Text = "Extrafanarts Only"
        Me.mnuMovieSetAllAskEFanarts.Visible = false
        '
        'mnuMovieSetAllAskEThumbs
        '
        Me.mnuMovieSetAllAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieSetAllAskEThumbs.Name = "mnuMovieSetAllAskEThumbs"
        Me.mnuMovieSetAllAskEThumbs.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAskEThumbs.Text = "Extrathumbs Only"
        Me.mnuMovieSetAllAskEThumbs.Visible = false
        '
        'mnuMovieSetAllAskFanart
        '
        Me.mnuMovieSetAllAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieSetAllAskFanart.Name = "mnuMovieSetAllAskFanart"
        Me.mnuMovieSetAllAskFanart.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAskFanart.Text = "Fanart Only"
        '
        'mnuMovieSetAllAskLandscape
        '
        Me.mnuMovieSetAllAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieSetAllAskLandscape.Name = "mnuMovieSetAllAskLandscape"
        Me.mnuMovieSetAllAskLandscape.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAskLandscape.Text = "Landscape Only"
        '
        'mnuMovieSetAllAskNfo
        '
        Me.mnuMovieSetAllAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieSetAllAskNfo.Name = "mnuMovieSetAllAskNfo"
        Me.mnuMovieSetAllAskNfo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAskNfo.Text = "NFO Only"
        '
        'mnuMovieSetAllAskPoster
        '
        Me.mnuMovieSetAllAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieSetAllAskPoster.Name = "mnuMovieSetAllAskPoster"
        Me.mnuMovieSetAllAskPoster.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetAllAskPoster.Text = "Poster Only"
        '
        'mnuMovieSetAllSkip
        '
        Me.mnuMovieSetAllSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetAllSkipAll})
        Me.mnuMovieSetAllSkip.Name = "mnuMovieSetAllSkip"
        Me.mnuMovieSetAllSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetAllSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'mnuMovieSetAllSkipAll
        '
        Me.mnuMovieSetAllSkipAll.Name = "mnuMovieSetAllSkipAll"
        Me.mnuMovieSetAllSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuMovieSetAllSkipAll.Text = "All Items"
        '
        'mnuMovieSetMiss
        '
        Me.mnuMovieSetMiss.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetMissAuto, Me.mnuMovieSetMissAsk, Me.mnuMovieSetMissSkip})
        Me.mnuMovieSetMiss.Name = "mnuMovieSetMiss"
        Me.mnuMovieSetMiss.Size = New System.Drawing.Size(199, 22)
        Me.mnuMovieSetMiss.Text = "MovieSets Missing Items"
        '
        'mnuMovieSetMissAuto
        '
        Me.mnuMovieSetMissAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetMissAutoAll, Me.mnuMovieSetMissAutoBanner, Me.mnuMovieSetMissAutoClearArt, Me.mnuMovieSetMissAutoClearLogo, Me.mnuMovieSetMissAutoDiscArt, Me.mnuMovieSetMissAutoEFanarts, Me.mnuMovieSetMissAutoEThumbs, Me.mnuMovieSetMissAutoFanart, Me.mnuMovieSetMissAutoLandscape, Me.mnuMovieSetMissAutoNfo, Me.mnuMovieSetMissAutoPoster})
        Me.mnuMovieSetMissAuto.Name = "mnuMovieSetMissAuto"
        Me.mnuMovieSetMissAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetMissAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuMovieSetMissAutoAll
        '
        Me.mnuMovieSetMissAutoAll.Enabled = false
        Me.mnuMovieSetMissAutoAll.Name = "mnuMovieSetMissAutoAll"
        Me.mnuMovieSetMissAutoAll.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAutoAll.Text = "All Items"
        Me.mnuMovieSetMissAutoAll.Visible = false
        '
        'mnuMovieSetMissAutoBanner
        '
        Me.mnuMovieSetMissAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieSetMissAutoBanner.Name = "mnuMovieSetMissAutoBanner"
        Me.mnuMovieSetMissAutoBanner.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAutoBanner.Text = "Banner Only"
        '
        'mnuMovieSetMissAutoClearArt
        '
        Me.mnuMovieSetMissAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieSetMissAutoClearArt.Name = "mnuMovieSetMissAutoClearArt"
        Me.mnuMovieSetMissAutoClearArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAutoClearArt.Text = "ClearArt Only"
        '
        'mnuMovieSetMissAutoClearLogo
        '
        Me.mnuMovieSetMissAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieSetMissAutoClearLogo.Name = "mnuMovieSetMissAutoClearLogo"
        Me.mnuMovieSetMissAutoClearLogo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieSetMissAutoDiscArt
        '
        Me.mnuMovieSetMissAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieSetMissAutoDiscArt.Name = "mnuMovieSetMissAutoDiscArt"
        Me.mnuMovieSetMissAutoDiscArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieSetMissAutoEFanarts
        '
        Me.mnuMovieSetMissAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieSetMissAutoEFanarts.Name = "mnuMovieSetMissAutoEFanarts"
        Me.mnuMovieSetMissAutoEFanarts.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAutoEFanarts.Text = "Extrafanarts Only"
        Me.mnuMovieSetMissAutoEFanarts.Visible = false
        '
        'mnuMovieSetMissAutoEThumbs
        '
        Me.mnuMovieSetMissAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieSetMissAutoEThumbs.Name = "mnuMovieSetMissAutoEThumbs"
        Me.mnuMovieSetMissAutoEThumbs.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAutoEThumbs.Text = "Extrathumbs Only"
        Me.mnuMovieSetMissAutoEThumbs.Visible = false
        '
        'mnuMovieSetMissAutoFanart
        '
        Me.mnuMovieSetMissAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieSetMissAutoFanart.Name = "mnuMovieSetMissAutoFanart"
        Me.mnuMovieSetMissAutoFanart.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAutoFanart.Text = "Fanart Only"
        '
        'mnuMovieSetMissAutoLandscape
        '
        Me.mnuMovieSetMissAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieSetMissAutoLandscape.Name = "mnuMovieSetMissAutoLandscape"
        Me.mnuMovieSetMissAutoLandscape.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAutoLandscape.Text = "Landscape Only"
        '
        'mnuMovieSetMissAutoNfo
        '
        Me.mnuMovieSetMissAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieSetMissAutoNfo.Name = "mnuMovieSetMissAutoNfo"
        Me.mnuMovieSetMissAutoNfo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAutoNfo.Text = "NFO Only"
        '
        'mnuMovieSetMissAutoPoster
        '
        Me.mnuMovieSetMissAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieSetMissAutoPoster.Name = "mnuMovieSetMissAutoPoster"
        Me.mnuMovieSetMissAutoPoster.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAutoPoster.Text = "Poster Only"
        '
        'mnuMovieSetMissAsk
        '
        Me.mnuMovieSetMissAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetMissAskAll, Me.mnuMovieSetMissAskBanner, Me.mnuMovieSetMissAskClearArt, Me.mnuMovieSetMissAskClearLogo, Me.mnuMovieSetMissAskDiscArt, Me.mnuMovieSetMissAskEFanarts, Me.mnuMovieSetMissAskEThumbs, Me.mnuMovieSetMissAskFanart, Me.mnuMovieSetMissAskLandscape, Me.mnuMovieSetMissAskNfo, Me.mnuMovieSetMissAskPoster})
        Me.mnuMovieSetMissAsk.Name = "mnuMovieSetMissAsk"
        Me.mnuMovieSetMissAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetMissAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuMovieSetMissAskAll
        '
        Me.mnuMovieSetMissAskAll.Enabled = false
        Me.mnuMovieSetMissAskAll.Name = "mnuMovieSetMissAskAll"
        Me.mnuMovieSetMissAskAll.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAskAll.Text = "All Items"
        Me.mnuMovieSetMissAskAll.Visible = false
        '
        'mnuMovieSetMissAskBanner
        '
        Me.mnuMovieSetMissAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieSetMissAskBanner.Name = "mnuMovieSetMissAskBanner"
        Me.mnuMovieSetMissAskBanner.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAskBanner.Text = "Banner Only"
        '
        'mnuMovieSetMissAskClearArt
        '
        Me.mnuMovieSetMissAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieSetMissAskClearArt.Name = "mnuMovieSetMissAskClearArt"
        Me.mnuMovieSetMissAskClearArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAskClearArt.Text = "ClearArt Only"
        '
        'mnuMovieSetMissAskClearLogo
        '
        Me.mnuMovieSetMissAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieSetMissAskClearLogo.Name = "mnuMovieSetMissAskClearLogo"
        Me.mnuMovieSetMissAskClearLogo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieSetMissAskDiscArt
        '
        Me.mnuMovieSetMissAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieSetMissAskDiscArt.Name = "mnuMovieSetMissAskDiscArt"
        Me.mnuMovieSetMissAskDiscArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAskDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieSetMissAskEFanarts
        '
        Me.mnuMovieSetMissAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieSetMissAskEFanarts.Name = "mnuMovieSetMissAskEFanarts"
        Me.mnuMovieSetMissAskEFanarts.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAskEFanarts.Text = "Extrafanarts Only"
        Me.mnuMovieSetMissAskEFanarts.Visible = false
        '
        'mnuMovieSetMissAskEThumbs
        '
        Me.mnuMovieSetMissAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieSetMissAskEThumbs.Name = "mnuMovieSetMissAskEThumbs"
        Me.mnuMovieSetMissAskEThumbs.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAskEThumbs.Text = "Extrathumbs Only"
        Me.mnuMovieSetMissAskEThumbs.Visible = false
        '
        'mnuMovieSetMissAskFanart
        '
        Me.mnuMovieSetMissAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieSetMissAskFanart.Name = "mnuMovieSetMissAskFanart"
        Me.mnuMovieSetMissAskFanart.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAskFanart.Text = "Fanart Only"
        '
        'mnuMovieSetMissAskLandscape
        '
        Me.mnuMovieSetMissAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieSetMissAskLandscape.Name = "mnuMovieSetMissAskLandscape"
        Me.mnuMovieSetMissAskLandscape.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAskLandscape.Text = "Landscape Only"
        '
        'mnuMovieSetMissAskNfo
        '
        Me.mnuMovieSetMissAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieSetMissAskNfo.Name = "mnuMovieSetMissAskNfo"
        Me.mnuMovieSetMissAskNfo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAskNfo.Text = "NFO Only"
        '
        'mnuMovieSetMissAskPoster
        '
        Me.mnuMovieSetMissAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieSetMissAskPoster.Name = "mnuMovieSetMissAskPoster"
        Me.mnuMovieSetMissAskPoster.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMissAskPoster.Text = "Poster Only"
        '
        'mnuMovieSetMissSkip
        '
        Me.mnuMovieSetMissSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetMissSkipAll})
        Me.mnuMovieSetMissSkip.Enabled = false
        Me.mnuMovieSetMissSkip.Name = "mnuMovieSetMissSkip"
        Me.mnuMovieSetMissSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetMissSkip.Text = "Skip (Skip If More Than One Match)"
        Me.mnuMovieSetMissSkip.Visible = false
        '
        'mnuMovieSetMissSkipAll
        '
        Me.mnuMovieSetMissSkipAll.Name = "mnuMovieSetMissSkipAll"
        Me.mnuMovieSetMissSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuMovieSetMissSkipAll.Text = "All Items"
        '
        'mnuMovieSetNew
        '
        Me.mnuMovieSetNew.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetNewAuto, Me.mnuMovieSetNewAsk, Me.mnuMovieSetNewSkip})
        Me.mnuMovieSetNew.Name = "mnuMovieSetNew"
        Me.mnuMovieSetNew.Size = New System.Drawing.Size(199, 22)
        Me.mnuMovieSetNew.Text = "New MovieSets"
        '
        'mnuMovieSetNewAuto
        '
        Me.mnuMovieSetNewAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetNewAutoAll, Me.mnuMovieSetNewAutoBanner, Me.mnuMovieSetNewAutoClearArt, Me.mnuMovieSetNewAutoClearLogo, Me.mnuMovieSetNewAutoDiscArt, Me.mnuMovieSetNewAutoEFanarts, Me.mnuMovieSetNewAutoEThumbs, Me.mnuMovieSetNewAutoFanart, Me.mnuMovieSetNewAutoLandscape, Me.mnuMovieSetNewAutoNfo, Me.mnuMovieSetNewAutoPoster})
        Me.mnuMovieSetNewAuto.Name = "mnuMovieSetNewAuto"
        Me.mnuMovieSetNewAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetNewAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuMovieSetNewAutoAll
        '
        Me.mnuMovieSetNewAutoAll.Name = "mnuMovieSetNewAutoAll"
        Me.mnuMovieSetNewAutoAll.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAutoAll.Text = "All Items"
        '
        'mnuMovieSetNewAutoBanner
        '
        Me.mnuMovieSetNewAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieSetNewAutoBanner.Name = "mnuMovieSetNewAutoBanner"
        Me.mnuMovieSetNewAutoBanner.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAutoBanner.Text = "Banner Only"
        '
        'mnuMovieSetNewAutoClearArt
        '
        Me.mnuMovieSetNewAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieSetNewAutoClearArt.Name = "mnuMovieSetNewAutoClearArt"
        Me.mnuMovieSetNewAutoClearArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAutoClearArt.Text = "ClearArt Only"
        '
        'mnuMovieSetNewAutoClearLogo
        '
        Me.mnuMovieSetNewAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieSetNewAutoClearLogo.Name = "mnuMovieSetNewAutoClearLogo"
        Me.mnuMovieSetNewAutoClearLogo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieSetNewAutoDiscArt
        '
        Me.mnuMovieSetNewAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieSetNewAutoDiscArt.Name = "mnuMovieSetNewAutoDiscArt"
        Me.mnuMovieSetNewAutoDiscArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieSetNewAutoEFanarts
        '
        Me.mnuMovieSetNewAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieSetNewAutoEFanarts.Name = "mnuMovieSetNewAutoEFanarts"
        Me.mnuMovieSetNewAutoEFanarts.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAutoEFanarts.Text = "Extrafanarts Only"
        Me.mnuMovieSetNewAutoEFanarts.Visible = false
        '
        'mnuMovieSetNewAutoEThumbs
        '
        Me.mnuMovieSetNewAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieSetNewAutoEThumbs.Name = "mnuMovieSetNewAutoEThumbs"
        Me.mnuMovieSetNewAutoEThumbs.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAutoEThumbs.Text = "Extrathumbs Only"
        Me.mnuMovieSetNewAutoEThumbs.Visible = false
        '
        'mnuMovieSetNewAutoFanart
        '
        Me.mnuMovieSetNewAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieSetNewAutoFanart.Name = "mnuMovieSetNewAutoFanart"
        Me.mnuMovieSetNewAutoFanart.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAutoFanart.Text = "Fanart Only"
        '
        'mnuMovieSetNewAutoLandscape
        '
        Me.mnuMovieSetNewAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieSetNewAutoLandscape.Name = "mnuMovieSetNewAutoLandscape"
        Me.mnuMovieSetNewAutoLandscape.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAutoLandscape.Text = "Landscape Only"
        '
        'mnuMovieSetNewAutoNfo
        '
        Me.mnuMovieSetNewAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieSetNewAutoNfo.Name = "mnuMovieSetNewAutoNfo"
        Me.mnuMovieSetNewAutoNfo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAutoNfo.Text = "NFO Only"
        '
        'mnuMovieSetNewAutoPoster
        '
        Me.mnuMovieSetNewAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieSetNewAutoPoster.Name = "mnuMovieSetNewAutoPoster"
        Me.mnuMovieSetNewAutoPoster.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAutoPoster.Text = "Poster Only"
        '
        'mnuMovieSetNewAsk
        '
        Me.mnuMovieSetNewAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetNewAskAll, Me.mnuMovieSetNewAskBanner, Me.mnuMovieSetNewAskClearArt, Me.mnuMovieSetNewAskClearLogo, Me.mnuMovieSetNewAskDiscArt, Me.mnuMovieSetNewAskEFanarts, Me.mnuMovieSetNewAskEThumbs, Me.mnuMovieSetNewAskFanart, Me.mnuMovieSetNewAskLandscape, Me.mnuMovieSetNewAskNfo, Me.mnuMovieSetNewAskPoster})
        Me.mnuMovieSetNewAsk.Name = "mnuMovieSetNewAsk"
        Me.mnuMovieSetNewAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetNewAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuMovieSetNewAskAll
        '
        Me.mnuMovieSetNewAskAll.Name = "mnuMovieSetNewAskAll"
        Me.mnuMovieSetNewAskAll.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAskAll.Text = "All Items"
        '
        'mnuMovieSetNewAskBanner
        '
        Me.mnuMovieSetNewAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieSetNewAskBanner.Name = "mnuMovieSetNewAskBanner"
        Me.mnuMovieSetNewAskBanner.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAskBanner.Text = "Banner Only"
        '
        'mnuMovieSetNewAskClearArt
        '
        Me.mnuMovieSetNewAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieSetNewAskClearArt.Name = "mnuMovieSetNewAskClearArt"
        Me.mnuMovieSetNewAskClearArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAskClearArt.Text = "ClearArt Only"
        '
        'mnuMovieSetNewAskClearLogo
        '
        Me.mnuMovieSetNewAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieSetNewAskClearLogo.Name = "mnuMovieSetNewAskClearLogo"
        Me.mnuMovieSetNewAskClearLogo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieSetNewAskDiscArt
        '
        Me.mnuMovieSetNewAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieSetNewAskDiscArt.Name = "mnuMovieSetNewAskDiscArt"
        Me.mnuMovieSetNewAskDiscArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAskDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieSetNewAskEFanarts
        '
        Me.mnuMovieSetNewAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieSetNewAskEFanarts.Name = "mnuMovieSetNewAskEFanarts"
        Me.mnuMovieSetNewAskEFanarts.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAskEFanarts.Text = "Extrafanarts Only"
        Me.mnuMovieSetNewAskEFanarts.Visible = false
        '
        'mnuMovieSetNewAskEThumbs
        '
        Me.mnuMovieSetNewAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieSetNewAskEThumbs.Name = "mnuMovieSetNewAskEThumbs"
        Me.mnuMovieSetNewAskEThumbs.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAskEThumbs.Text = "Extrathumbs Only"
        Me.mnuMovieSetNewAskEThumbs.Visible = false
        '
        'mnuMovieSetNewAskFanart
        '
        Me.mnuMovieSetNewAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieSetNewAskFanart.Name = "mnuMovieSetNewAskFanart"
        Me.mnuMovieSetNewAskFanart.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAskFanart.Text = "Fanart Only"
        '
        'mnuMovieSetNewAskLandscape
        '
        Me.mnuMovieSetNewAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieSetNewAskLandscape.Name = "mnuMovieSetNewAskLandscape"
        Me.mnuMovieSetNewAskLandscape.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAskLandscape.Text = "Landscape Only"
        '
        'mnuMovieSetNewAskNfo
        '
        Me.mnuMovieSetNewAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieSetNewAskNfo.Name = "mnuMovieSetNewAskNfo"
        Me.mnuMovieSetNewAskNfo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAskNfo.Text = "NFO Only"
        '
        'mnuMovieSetNewAskPoster
        '
        Me.mnuMovieSetNewAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieSetNewAskPoster.Name = "mnuMovieSetNewAskPoster"
        Me.mnuMovieSetNewAskPoster.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetNewAskPoster.Text = "Poster Only"
        '
        'mnuMovieSetNewSkip
        '
        Me.mnuMovieSetNewSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetNewSkipAll})
        Me.mnuMovieSetNewSkip.Name = "mnuMovieSetNewSkip"
        Me.mnuMovieSetNewSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetNewSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'mnuMovieSetNewSkipAll
        '
        Me.mnuMovieSetNewSkipAll.Name = "mnuMovieSetNewSkipAll"
        Me.mnuMovieSetNewSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuMovieSetNewSkipAll.Text = "All Items"
        '
        'mnuMovieSetMark
        '
        Me.mnuMovieSetMark.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetMarkAuto, Me.mnuMovieSetMarkAsk, Me.mnuMovieSetMarkSkip})
        Me.mnuMovieSetMark.Name = "mnuMovieSetMark"
        Me.mnuMovieSetMark.Size = New System.Drawing.Size(199, 22)
        Me.mnuMovieSetMark.Text = "Marked MovieSets"
        '
        'mnuMovieSetMarkAuto
        '
        Me.mnuMovieSetMarkAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetMarkAutoAll, Me.mnuMovieSetMarkAutoBanner, Me.mnuMovieSetMarkAutoClearArt, Me.mnuMovieSetMarkAutoClearLogo, Me.mnuMovieSetMarkAutoDiscArt, Me.mnuMovieSetMarkAutoEFanarts, Me.mnuMovieSetMarkAutoEThumbs, Me.mnuMovieSetMarkAutoFanart, Me.mnuMovieSetMarkAutoLandscape, Me.mnuMovieSetMarkAutoNfo, Me.mnuMovieSetMarkAutoPoster})
        Me.mnuMovieSetMarkAuto.Name = "mnuMovieSetMarkAuto"
        Me.mnuMovieSetMarkAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetMarkAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuMovieSetMarkAutoAll
        '
        Me.mnuMovieSetMarkAutoAll.Name = "mnuMovieSetMarkAutoAll"
        Me.mnuMovieSetMarkAutoAll.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAutoAll.Text = "All Items"
        '
        'mnuMovieSetMarkAutoBanner
        '
        Me.mnuMovieSetMarkAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieSetMarkAutoBanner.Name = "mnuMovieSetMarkAutoBanner"
        Me.mnuMovieSetMarkAutoBanner.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAutoBanner.Text = "Banner Only"
        '
        'mnuMovieSetMarkAutoClearArt
        '
        Me.mnuMovieSetMarkAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieSetMarkAutoClearArt.Name = "mnuMovieSetMarkAutoClearArt"
        Me.mnuMovieSetMarkAutoClearArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAutoClearArt.Text = "ClearArt Only"
        '
        'mnuMovieSetMarkAutoClearLogo
        '
        Me.mnuMovieSetMarkAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieSetMarkAutoClearLogo.Name = "mnuMovieSetMarkAutoClearLogo"
        Me.mnuMovieSetMarkAutoClearLogo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieSetMarkAutoDiscArt
        '
        Me.mnuMovieSetMarkAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieSetMarkAutoDiscArt.Name = "mnuMovieSetMarkAutoDiscArt"
        Me.mnuMovieSetMarkAutoDiscArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieSetMarkAutoEFanarts
        '
        Me.mnuMovieSetMarkAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieSetMarkAutoEFanarts.Name = "mnuMovieSetMarkAutoEFanarts"
        Me.mnuMovieSetMarkAutoEFanarts.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAutoEFanarts.Text = "Extrafanarts Only"
        Me.mnuMovieSetMarkAutoEFanarts.Visible = false
        '
        'mnuMovieSetMarkAutoEThumbs
        '
        Me.mnuMovieSetMarkAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieSetMarkAutoEThumbs.Name = "mnuMovieSetMarkAutoEThumbs"
        Me.mnuMovieSetMarkAutoEThumbs.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAutoEThumbs.Text = "Extrathumbs Only"
        Me.mnuMovieSetMarkAutoEThumbs.Visible = false
        '
        'mnuMovieSetMarkAutoFanart
        '
        Me.mnuMovieSetMarkAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieSetMarkAutoFanart.Name = "mnuMovieSetMarkAutoFanart"
        Me.mnuMovieSetMarkAutoFanart.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAutoFanart.Text = "Fanart Only"
        '
        'mnuMovieSetMarkAutoLandscape
        '
        Me.mnuMovieSetMarkAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieSetMarkAutoLandscape.Name = "mnuMovieSetMarkAutoLandscape"
        Me.mnuMovieSetMarkAutoLandscape.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAutoLandscape.Text = "Landscape Only"
        '
        'mnuMovieSetMarkAutoNfo
        '
        Me.mnuMovieSetMarkAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieSetMarkAutoNfo.Name = "mnuMovieSetMarkAutoNfo"
        Me.mnuMovieSetMarkAutoNfo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAutoNfo.Text = "NFO Only"
        '
        'mnuMovieSetMarkAutoPoster
        '
        Me.mnuMovieSetMarkAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieSetMarkAutoPoster.Name = "mnuMovieSetMarkAutoPoster"
        Me.mnuMovieSetMarkAutoPoster.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAutoPoster.Text = "Poster Only"
        '
        'mnuMovieSetMarkAsk
        '
        Me.mnuMovieSetMarkAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetMarkAskAll, Me.mnuMovieSetMarkAskBanner, Me.mnuMovieSetMarkAskClearArt, Me.mnuMovieSetMarkAskClearLogo, Me.mnuMovieSetMarkAskDiscArt, Me.mnuMovieSetMarkAskEFanarts, Me.mnuMovieSetMarkAskEThumbs, Me.mnuMovieSetMarkAskFanart, Me.mnuMovieSetMarkAskLandscape, Me.mnuMovieSetMarkAskNfo, Me.mnuMovieSetMarkAskPoster})
        Me.mnuMovieSetMarkAsk.Name = "mnuMovieSetMarkAsk"
        Me.mnuMovieSetMarkAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetMarkAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuMovieSetMarkAskAll
        '
        Me.mnuMovieSetMarkAskAll.Name = "mnuMovieSetMarkAskAll"
        Me.mnuMovieSetMarkAskAll.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAskAll.Text = "All Items"
        '
        'mnuMovieSetMarkAskBanner
        '
        Me.mnuMovieSetMarkAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieSetMarkAskBanner.Name = "mnuMovieSetMarkAskBanner"
        Me.mnuMovieSetMarkAskBanner.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAskBanner.Text = "Banner Only"
        '
        'mnuMovieSetMarkAskClearArt
        '
        Me.mnuMovieSetMarkAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieSetMarkAskClearArt.Name = "mnuMovieSetMarkAskClearArt"
        Me.mnuMovieSetMarkAskClearArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAskClearArt.Text = "ClearArt Only"
        '
        'mnuMovieSetMarkAskClearLogo
        '
        Me.mnuMovieSetMarkAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieSetMarkAskClearLogo.Name = "mnuMovieSetMarkAskClearLogo"
        Me.mnuMovieSetMarkAskClearLogo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieSetMarkAskDiscArt
        '
        Me.mnuMovieSetMarkAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieSetMarkAskDiscArt.Name = "mnuMovieSetMarkAskDiscArt"
        Me.mnuMovieSetMarkAskDiscArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAskDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieSetMarkAskEFanarts
        '
        Me.mnuMovieSetMarkAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieSetMarkAskEFanarts.Name = "mnuMovieSetMarkAskEFanarts"
        Me.mnuMovieSetMarkAskEFanarts.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAskEFanarts.Text = "Extrafanarts Only"
        Me.mnuMovieSetMarkAskEFanarts.Visible = false
        '
        'mnuMovieSetMarkAskEThumbs
        '
        Me.mnuMovieSetMarkAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieSetMarkAskEThumbs.Name = "mnuMovieSetMarkAskEThumbs"
        Me.mnuMovieSetMarkAskEThumbs.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAskEThumbs.Text = "Extrathumbs Only"
        Me.mnuMovieSetMarkAskEThumbs.Visible = false
        '
        'mnuMovieSetMarkAskFanart
        '
        Me.mnuMovieSetMarkAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieSetMarkAskFanart.Name = "mnuMovieSetMarkAskFanart"
        Me.mnuMovieSetMarkAskFanart.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAskFanart.Text = "Fanart Only"
        '
        'mnuMovieSetMarkAskLandscape
        '
        Me.mnuMovieSetMarkAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieSetMarkAskLandscape.Name = "mnuMovieSetMarkAskLandscape"
        Me.mnuMovieSetMarkAskLandscape.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAskLandscape.Text = "Landscape Only"
        '
        'mnuMovieSetMarkAskNfo
        '
        Me.mnuMovieSetMarkAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieSetMarkAskNfo.Name = "mnuMovieSetMarkAskNfo"
        Me.mnuMovieSetMarkAskNfo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAskNfo.Text = "NFO Only"
        '
        'mnuMovieSetMarkAskPoster
        '
        Me.mnuMovieSetMarkAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieSetMarkAskPoster.Name = "mnuMovieSetMarkAskPoster"
        Me.mnuMovieSetMarkAskPoster.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetMarkAskPoster.Text = "Poster Only"
        '
        'mnuMovieSetMarkSkip
        '
        Me.mnuMovieSetMarkSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetMarkSkipAll})
        Me.mnuMovieSetMarkSkip.Name = "mnuMovieSetMarkSkip"
        Me.mnuMovieSetMarkSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetMarkSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'mnuMovieSetMarkSkipAll
        '
        Me.mnuMovieSetMarkSkipAll.Name = "mnuMovieSetMarkSkipAll"
        Me.mnuMovieSetMarkSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuMovieSetMarkSkipAll.Text = "All Items"
        '
        'mnuMovieSetFilter
        '
        Me.mnuMovieSetFilter.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetFilterAuto, Me.mnuMovieSetFilterAsk, Me.mnuMovieSetFilterSkip})
        Me.mnuMovieSetFilter.Name = "mnuMovieSetFilter"
        Me.mnuMovieSetFilter.Size = New System.Drawing.Size(199, 22)
        Me.mnuMovieSetFilter.Text = "Current Filter"
        Me.mnuMovieSetFilter.Visible = false
        '
        'mnuMovieSetFilterAuto
        '
        Me.mnuMovieSetFilterAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetFilterAutoAll, Me.mnuMovieSetFilterAutoBanner, Me.mnuMovieSetFilterAutoClearArt, Me.mnuMovieSetFilterAutoClearLogo, Me.mnuMovieSetFilterAutoDiscArt, Me.mnuMovieSetFilterAutoEFanarts, Me.mnuMovieSetFilterAutoEThumbs, Me.mnuMovieSetFilterAutoFanart, Me.mnuMovieSetFilterAutoLandscape, Me.mnuMovieSetFilterAutoNfo, Me.mnuMovieSetFilterAutoPoster})
        Me.mnuMovieSetFilterAuto.Name = "mnuMovieSetFilterAuto"
        Me.mnuMovieSetFilterAuto.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetFilterAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuMovieSetFilterAutoAll
        '
        Me.mnuMovieSetFilterAutoAll.Name = "mnuMovieSetFilterAutoAll"
        Me.mnuMovieSetFilterAutoAll.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAutoAll.Text = "All Items"
        '
        'mnuMovieSetFilterAutoBanner
        '
        Me.mnuMovieSetFilterAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieSetFilterAutoBanner.Name = "mnuMovieSetFilterAutoBanner"
        Me.mnuMovieSetFilterAutoBanner.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAutoBanner.Text = "Banner Only"
        '
        'mnuMovieSetFilterAutoClearArt
        '
        Me.mnuMovieSetFilterAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieSetFilterAutoClearArt.Name = "mnuMovieSetFilterAutoClearArt"
        Me.mnuMovieSetFilterAutoClearArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAutoClearArt.Text = "ClearArt Only"
        '
        'mnuMovieSetFilterAutoClearLogo
        '
        Me.mnuMovieSetFilterAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieSetFilterAutoClearLogo.Name = "mnuMovieSetFilterAutoClearLogo"
        Me.mnuMovieSetFilterAutoClearLogo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAutoClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieSetFilterAutoDiscArt
        '
        Me.mnuMovieSetFilterAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieSetFilterAutoDiscArt.Name = "mnuMovieSetFilterAutoDiscArt"
        Me.mnuMovieSetFilterAutoDiscArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAutoDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieSetFilterAutoEFanarts
        '
        Me.mnuMovieSetFilterAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieSetFilterAutoEFanarts.Name = "mnuMovieSetFilterAutoEFanarts"
        Me.mnuMovieSetFilterAutoEFanarts.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAutoEFanarts.Text = "Extrafanarts Only"
        Me.mnuMovieSetFilterAutoEFanarts.Visible = false
        '
        'mnuMovieSetFilterAutoEThumbs
        '
        Me.mnuMovieSetFilterAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieSetFilterAutoEThumbs.Name = "mnuMovieSetFilterAutoEThumbs"
        Me.mnuMovieSetFilterAutoEThumbs.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAutoEThumbs.Text = "Extrathumbs Only"
        Me.mnuMovieSetFilterAutoEThumbs.Visible = false
        '
        'mnuMovieSetFilterAutoFanart
        '
        Me.mnuMovieSetFilterAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieSetFilterAutoFanart.Name = "mnuMovieSetFilterAutoFanart"
        Me.mnuMovieSetFilterAutoFanart.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAutoFanart.Text = "Fanart Only"
        '
        'mnuMovieSetFilterAutoLandscape
        '
        Me.mnuMovieSetFilterAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieSetFilterAutoLandscape.Name = "mnuMovieSetFilterAutoLandscape"
        Me.mnuMovieSetFilterAutoLandscape.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAutoLandscape.Text = "Landscape Only"
        '
        'mnuMovieSetFilterAutoNfo
        '
        Me.mnuMovieSetFilterAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieSetFilterAutoNfo.Name = "mnuMovieSetFilterAutoNfo"
        Me.mnuMovieSetFilterAutoNfo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAutoNfo.Text = "NFO Only"
        '
        'mnuMovieSetFilterAutoPoster
        '
        Me.mnuMovieSetFilterAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieSetFilterAutoPoster.Name = "mnuMovieSetFilterAutoPoster"
        Me.mnuMovieSetFilterAutoPoster.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAutoPoster.Text = "Poster Only"
        '
        'mnuMovieSetFilterAsk
        '
        Me.mnuMovieSetFilterAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetFilterAskAll, Me.mnuMovieSetFilterAskBanner, Me.mnuMovieSetFilterAskClearArt, Me.mnuMovieSetFilterAskClearLogo, Me.mnuMovieSetFilterAskDiscArt, Me.mnuMovieSetFilterAskEFanarts, Me.mnuMovieSetFilterAskEThumbs, Me.mnuMovieSetFilterAskFanart, Me.mnuMovieSetFilterAskLandscape, Me.mnuMovieSetFilterAskNfo, Me.mnuMovieSetFilterAskPoster})
        Me.mnuMovieSetFilterAsk.Name = "mnuMovieSetFilterAsk"
        Me.mnuMovieSetFilterAsk.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetFilterAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuMovieSetFilterAskAll
        '
        Me.mnuMovieSetFilterAskAll.Name = "mnuMovieSetFilterAskAll"
        Me.mnuMovieSetFilterAskAll.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAskAll.Text = "All Items"
        '
        'mnuMovieSetFilterAskBanner
        '
        Me.mnuMovieSetFilterAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuMovieSetFilterAskBanner.Name = "mnuMovieSetFilterAskBanner"
        Me.mnuMovieSetFilterAskBanner.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAskBanner.Text = "Banner Only"
        '
        'mnuMovieSetFilterAskClearArt
        '
        Me.mnuMovieSetFilterAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuMovieSetFilterAskClearArt.Name = "mnuMovieSetFilterAskClearArt"
        Me.mnuMovieSetFilterAskClearArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAskClearArt.Text = "ClearArt Only"
        '
        'mnuMovieSetFilterAskClearLogo
        '
        Me.mnuMovieSetFilterAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuMovieSetFilterAskClearLogo.Name = "mnuMovieSetFilterAskClearLogo"
        Me.mnuMovieSetFilterAskClearLogo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAskClearLogo.Text = "ClearLogo Only"
        '
        'mnuMovieSetFilterAskDiscArt
        '
        Me.mnuMovieSetFilterAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuMovieSetFilterAskDiscArt.Name = "mnuMovieSetFilterAskDiscArt"
        Me.mnuMovieSetFilterAskDiscArt.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAskDiscArt.Text = "DiscArt Only"
        '
        'mnuMovieSetFilterAskEFanarts
        '
        Me.mnuMovieSetFilterAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuMovieSetFilterAskEFanarts.Name = "mnuMovieSetFilterAskEFanarts"
        Me.mnuMovieSetFilterAskEFanarts.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAskEFanarts.Text = "Extrafanarts Only"
        Me.mnuMovieSetFilterAskEFanarts.Visible = false
        '
        'mnuMovieSetFilterAskEThumbs
        '
        Me.mnuMovieSetFilterAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuMovieSetFilterAskEThumbs.Name = "mnuMovieSetFilterAskEThumbs"
        Me.mnuMovieSetFilterAskEThumbs.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAskEThumbs.Text = "Extrathumbs Only"
        Me.mnuMovieSetFilterAskEThumbs.Visible = false
        '
        'mnuMovieSetFilterAskFanart
        '
        Me.mnuMovieSetFilterAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuMovieSetFilterAskFanart.Name = "mnuMovieSetFilterAskFanart"
        Me.mnuMovieSetFilterAskFanart.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAskFanart.Text = "Fanart Only"
        '
        'mnuMovieSetFilterAskLandscape
        '
        Me.mnuMovieSetFilterAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuMovieSetFilterAskLandscape.Name = "mnuMovieSetFilterAskLandscape"
        Me.mnuMovieSetFilterAskLandscape.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAskLandscape.Text = "Landscape Only"
        '
        'mnuMovieSetFilterAskNfo
        '
        Me.mnuMovieSetFilterAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuMovieSetFilterAskNfo.Name = "mnuMovieSetFilterAskNfo"
        Me.mnuMovieSetFilterAskNfo.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAskNfo.Text = "NFO Only"
        '
        'mnuMovieSetFilterAskPoster
        '
        Me.mnuMovieSetFilterAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuMovieSetFilterAskPoster.Name = "mnuMovieSetFilterAskPoster"
        Me.mnuMovieSetFilterAskPoster.Size = New System.Drawing.Size(165, 22)
        Me.mnuMovieSetFilterAskPoster.Text = "Poster Only"
        '
        'mnuMovieSetFilterSkip
        '
        Me.mnuMovieSetFilterSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMovieSetFilterSkipAll})
        Me.mnuMovieSetFilterSkip.Name = "mnuMovieSetFilterSkip"
        Me.mnuMovieSetFilterSkip.Size = New System.Drawing.Size(264, 22)
        Me.mnuMovieSetFilterSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'mnuMovieSetFilterSkipAll
        '
        Me.mnuMovieSetFilterSkipAll.Name = "mnuMovieSetFilterSkipAll"
        Me.mnuMovieSetFilterSkipAll.Size = New System.Drawing.Size(117, 22)
        Me.mnuMovieSetFilterSkipAll.Text = "All Items"
        '
        'mnuMovieSetCustom
        '
        Me.mnuMovieSetCustom.Name = "mnuMovieSetCustom"
        Me.mnuMovieSetCustom.Size = New System.Drawing.Size(199, 22)
        Me.mnuMovieSetCustom.Text = "Custom Scraper..."
        Me.mnuMovieSetCustom.Visible = false
        '
        'mnuMovieSetRestart
        '
        Me.mnuMovieSetRestart.Name = "mnuMovieSetRestart"
        Me.mnuMovieSetRestart.Size = New System.Drawing.Size(199, 22)
        Me.mnuMovieSetRestart.Text = "Restart last scrape..."
        Me.mnuMovieSetRestart.Visible = false
        '
        'mnuUpdate
        '
        Me.mnuUpdate.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuUpdateMovies, Me.mnuUpdateShows})
        Me.mnuUpdate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.mnuUpdate.Image = CType(resources.GetObject("mnuUpdate.Image"),System.Drawing.Image)
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
        Me.tsbMediaCenters.Enabled = false
        Me.tsbMediaCenters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.tsbMediaCenters.Image = CType(resources.GetObject("tsbMediaCenters.Image"),System.Drawing.Image)
        Me.tsbMediaCenters.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbMediaCenters.Name = "tsbMediaCenters"
        Me.tsbMediaCenters.Size = New System.Drawing.Size(113, 22)
        Me.tsbMediaCenters.Text = "Media Centers"
        Me.tsbMediaCenters.Visible = false
        '
        'ilColumnIcons
        '
        Me.ilColumnIcons.ImageStream = CType(resources.GetObject("ilColumnIcons.ImageStream"),System.Windows.Forms.ImageListStreamer)
        Me.ilColumnIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ilColumnIcons.Images.SetKeyName(0, "listcheck.png")
        Me.ilColumnIcons.Images.SetKeyName(1, "listdotgrey.png")
        Me.ilColumnIcons.Images.SetKeyName(2, "hasBanner.png")
        Me.ilColumnIcons.Images.SetKeyName(3, "hasCharacterArt.png")
        Me.ilColumnIcons.Images.SetKeyName(4, "hasClearArt.png")
        Me.ilColumnIcons.Images.SetKeyName(5, "hasClearLogo.png")
        Me.ilColumnIcons.Images.SetKeyName(6, "hasDiscArt.png")
        Me.ilColumnIcons.Images.SetKeyName(7, "hasExtrafanart.png")
        Me.ilColumnIcons.Images.SetKeyName(8, "hasExtrathumb.png")
        Me.ilColumnIcons.Images.SetKeyName(9, "hasFanart.png")
        Me.ilColumnIcons.Images.SetKeyName(10, "hasLandscape.png")
        Me.ilColumnIcons.Images.SetKeyName(11, "hasNfo.png")
        Me.ilColumnIcons.Images.SetKeyName(12, "hasPoster.png")
        Me.ilColumnIcons.Images.SetKeyName(13, "hasSet.png")
        Me.ilColumnIcons.Images.SetKeyName(14, "hasSubtitle.png")
        Me.ilColumnIcons.Images.SetKeyName(15, "hasTheme.png")
        Me.ilColumnIcons.Images.SetKeyName(16, "hasTrailer.png")
        Me.ilColumnIcons.Images.SetKeyName(17, "hasWatched.png")
        '
        'tmrWaitMovie
        '
        Me.tmrWaitMovie.Interval = 250
        '
        'tmrLoadMovie
        '
        '
        'tmrSearchWait_Movies
        '
        Me.tmrSearchWait_Movies.Interval = 250
        '
        'tmrSearch_Movies
        '
        Me.tmrSearch_Movies.Interval = 250
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
        Me.cmnuTrayTitle.Enabled = false
        Me.cmnuTrayTitle.Image = CType(resources.GetObject("cmnuTrayTitle.Image"),System.Drawing.Image)
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
        Me.cmnuTrayUpdate.Image = CType(resources.GetObject("cmnuTrayUpdate.Image"),System.Drawing.Image)
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
        Me.cmnuTrayScrape.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieAll, Me.cmnuTrayMovieMiss, Me.cmnuTrayMovieNew, Me.cmnuTrayMovieMark, Me.cmnuTrayMovieFilter, Me.cmnuTrayMovieCustom, Me.cmnuTrayMovieRestart})
        Me.cmnuTrayScrape.Image = CType(resources.GetObject("cmnuTrayScrape.Image"),System.Drawing.Image)
        Me.cmnuTrayScrape.Name = "cmnuTrayScrape"
        Me.cmnuTrayScrape.Size = New System.Drawing.Size(194, 22)
        Me.cmnuTrayScrape.Text = "Scrape Media"
        '
        'cmnuTrayMovieAll
        '
        Me.cmnuTrayMovieAll.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieAllAuto, Me.cmnuTrayMovieAllAsk, Me.cmnuTrayMovieAllSkip})
        Me.cmnuTrayMovieAll.Name = "cmnuTrayMovieAll"
        Me.cmnuTrayMovieAll.Size = New System.Drawing.Size(188, 22)
        Me.cmnuTrayMovieAll.Text = "All Movies"
        '
        'cmnuTrayMovieAllAuto
        '
        Me.cmnuTrayMovieAllAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieAllAutoAll, Me.cmnuTrayMovieAllAutoActor, Me.cmnuTrayMovieAllAutoBanner, Me.cmnuTrayMovieAllAutoClearArt, Me.cmnuTrayMovieAllAutoClearLogo, Me.cmnuTrayMovieAllAutoDiscArt, Me.cmnuTrayMovieAllAutoEFanarts, Me.cmnuTrayMovieAllAutoEThumbs, Me.cmnuTrayMovieAllAutoFanart, Me.cmnuTrayMovieAllAutoLandscape, Me.cmnuTrayMovieAllAutoMetaData, Me.cmnuTrayMovieAllAutoNfo, Me.cmnuTrayMovieAllAutoPoster, Me.cmnuTrayMovieAllAutoTheme, Me.cmnuTrayMovieAllAutoTrailer})
        Me.cmnuTrayMovieAllAuto.Name = "cmnuTrayMovieAllAuto"
        Me.cmnuTrayMovieAllAuto.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieAllAuto.Text = "Automatic (Force Best Match)"
        '
        'cmnuTrayMovieAllAutoAll
        '
        Me.cmnuTrayMovieAllAutoAll.Name = "cmnuTrayMovieAllAutoAll"
        Me.cmnuTrayMovieAllAutoAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoAll.Text = "All Items"
        '
        'cmnuTrayMovieAllAutoActor
        '
        Me.cmnuTrayMovieAllAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayMovieAllAutoActor.Name = "cmnuTrayMovieAllAutoActor"
        Me.cmnuTrayMovieAllAutoActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayMovieAllAutoBanner
        '
        Me.cmnuTrayMovieAllAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayMovieAllAutoBanner.Name = "cmnuTrayMovieAllAutoBanner"
        Me.cmnuTrayMovieAllAutoBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoBanner.Text = "Banner Only"
        '
        'cmnuTrayMovieAllAutoClearArt
        '
        Me.cmnuTrayMovieAllAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayMovieAllAutoClearArt.Name = "cmnuTrayMovieAllAutoClearArt"
        Me.cmnuTrayMovieAllAutoClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayMovieAllAutoClearLogo
        '
        Me.cmnuTrayMovieAllAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayMovieAllAutoClearLogo.Name = "cmnuTrayMovieAllAutoClearLogo"
        Me.cmnuTrayMovieAllAutoClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayMovieAllAutoDiscArt
        '
        Me.cmnuTrayMovieAllAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayMovieAllAutoDiscArt.Name = "cmnuTrayMovieAllAutoDiscArt"
        Me.cmnuTrayMovieAllAutoDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayMovieAllAutoEFanarts
        '
        Me.cmnuTrayMovieAllAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayMovieAllAutoEFanarts.Name = "cmnuTrayMovieAllAutoEFanarts"
        Me.cmnuTrayMovieAllAutoEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayMovieAllAutoEThumbs
        '
        Me.cmnuTrayMovieAllAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayMovieAllAutoEThumbs.Name = "cmnuTrayMovieAllAutoEThumbs"
        Me.cmnuTrayMovieAllAutoEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayMovieAllAutoFanart
        '
        Me.cmnuTrayMovieAllAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayMovieAllAutoFanart.Name = "cmnuTrayMovieAllAutoFanart"
        Me.cmnuTrayMovieAllAutoFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoFanart.Text = "Fanart Only"
        '
        'cmnuTrayMovieAllAutoLandscape
        '
        Me.cmnuTrayMovieAllAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayMovieAllAutoLandscape.Name = "cmnuTrayMovieAllAutoLandscape"
        Me.cmnuTrayMovieAllAutoLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoLandscape.Text = "Landscape Only"
        '
        'cmnuTrayMovieAllAutoMetaData
        '
        Me.cmnuTrayMovieAllAutoMetaData.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayMovieAllAutoMetaData.Name = "cmnuTrayMovieAllAutoMetaData"
        Me.cmnuTrayMovieAllAutoMetaData.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoMetaData.Text = "Meta Data Only"
        '
        'cmnuTrayMovieAllAutoNfo
        '
        Me.cmnuTrayMovieAllAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayMovieAllAutoNfo.Name = "cmnuTrayMovieAllAutoNfo"
        Me.cmnuTrayMovieAllAutoNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoNfo.Text = "NFO Only"
        '
        'cmnuTrayMovieAllAutoPoster
        '
        Me.cmnuTrayMovieAllAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayMovieAllAutoPoster.Name = "cmnuTrayMovieAllAutoPoster"
        Me.cmnuTrayMovieAllAutoPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoPoster.Text = "Poster Only"
        '
        'cmnuTrayMovieAllAutoTheme
        '
        Me.cmnuTrayMovieAllAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayMovieAllAutoTheme.Name = "cmnuTrayMovieAllAutoTheme"
        Me.cmnuTrayMovieAllAutoTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoTheme.Text = "Theme Only"
        '
        'cmnuTrayMovieAllAutoTrailer
        '
        Me.cmnuTrayMovieAllAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayMovieAllAutoTrailer.Name = "cmnuTrayMovieAllAutoTrailer"
        Me.cmnuTrayMovieAllAutoTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAutoTrailer.Text = "Trailer Only"
        '
        'cmnuTrayMovieAllAsk
        '
        Me.cmnuTrayMovieAllAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieAllAskAll, Me.cmnuTrayMovieAllAskActor, Me.cmnuTrayMovieAllAskBanner, Me.cmnuTrayMovieAllAskClearArt, Me.cmnuTrayMovieAllAskClearLogo, Me.cmnuTrayMovieAllAskDiscArt, Me.cmnuTrayMovieAllAskEFanarts, Me.cmnuTrayMovieAllAskEThumbs, Me.cmnuTrayMovieAllAskFanart, Me.cmnuTrayMovieAllAskLandscape, Me.cmnuTrayMovieAllAskMI, Me.cmnuTrayMovieAllAskNfo, Me.cmnuTrayMovieAllAskPoster, Me.cmnuTrayMovieAllAskTheme, Me.cmnuTrayMovieAllAskTrailer})
        Me.cmnuTrayMovieAllAsk.Name = "cmnuTrayMovieAllAsk"
        Me.cmnuTrayMovieAllAsk.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieAllAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'cmnuTrayMovieAllAskAll
        '
        Me.cmnuTrayMovieAllAskAll.Name = "cmnuTrayMovieAllAskAll"
        Me.cmnuTrayMovieAllAskAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskAll.Text = "All Items"
        '
        'cmnuTrayMovieAllAskActor
        '
        Me.cmnuTrayMovieAllAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayMovieAllAskActor.Name = "cmnuTrayMovieAllAskActor"
        Me.cmnuTrayMovieAllAskActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayMovieAllAskBanner
        '
        Me.cmnuTrayMovieAllAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayMovieAllAskBanner.Name = "cmnuTrayMovieAllAskBanner"
        Me.cmnuTrayMovieAllAskBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskBanner.Text = "Banner Only"
        '
        'cmnuTrayMovieAllAskClearArt
        '
        Me.cmnuTrayMovieAllAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayMovieAllAskClearArt.Name = "cmnuTrayMovieAllAskClearArt"
        Me.cmnuTrayMovieAllAskClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayMovieAllAskClearLogo
        '
        Me.cmnuTrayMovieAllAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayMovieAllAskClearLogo.Name = "cmnuTrayMovieAllAskClearLogo"
        Me.cmnuTrayMovieAllAskClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayMovieAllAskDiscArt
        '
        Me.cmnuTrayMovieAllAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayMovieAllAskDiscArt.Name = "cmnuTrayMovieAllAskDiscArt"
        Me.cmnuTrayMovieAllAskDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayMovieAllAskEFanarts
        '
        Me.cmnuTrayMovieAllAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayMovieAllAskEFanarts.Name = "cmnuTrayMovieAllAskEFanarts"
        Me.cmnuTrayMovieAllAskEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayMovieAllAskEThumbs
        '
        Me.cmnuTrayMovieAllAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayMovieAllAskEThumbs.Name = "cmnuTrayMovieAllAskEThumbs"
        Me.cmnuTrayMovieAllAskEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayMovieAllAskFanart
        '
        Me.cmnuTrayMovieAllAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayMovieAllAskFanart.Name = "cmnuTrayMovieAllAskFanart"
        Me.cmnuTrayMovieAllAskFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskFanart.Text = "Fanart Only"
        '
        'cmnuTrayMovieAllAskLandscape
        '
        Me.cmnuTrayMovieAllAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayMovieAllAskLandscape.Name = "cmnuTrayMovieAllAskLandscape"
        Me.cmnuTrayMovieAllAskLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskLandscape.Text = "Landscape Only"
        '
        'cmnuTrayMovieAllAskMI
        '
        Me.cmnuTrayMovieAllAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayMovieAllAskMI.Name = "cmnuTrayMovieAllAskMI"
        Me.cmnuTrayMovieAllAskMI.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskMI.Text = "Meta Data Only"
        '
        'cmnuTrayMovieAllAskNfo
        '
        Me.cmnuTrayMovieAllAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayMovieAllAskNfo.Name = "cmnuTrayMovieAllAskNfo"
        Me.cmnuTrayMovieAllAskNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskNfo.Text = "NFO Only"
        '
        'cmnuTrayMovieAllAskPoster
        '
        Me.cmnuTrayMovieAllAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayMovieAllAskPoster.Name = "cmnuTrayMovieAllAskPoster"
        Me.cmnuTrayMovieAllAskPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskPoster.Text = "Poster Only"
        '
        'cmnuTrayMovieAllAskTheme
        '
        Me.cmnuTrayMovieAllAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayMovieAllAskTheme.Name = "cmnuTrayMovieAllAskTheme"
        Me.cmnuTrayMovieAllAskTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskTheme.Text = "Theme Only"
        '
        'cmnuTrayMovieAllAskTrailer
        '
        Me.cmnuTrayMovieAllAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayMovieAllAskTrailer.Name = "cmnuTrayMovieAllAskTrailer"
        Me.cmnuTrayMovieAllAskTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieAllAskTrailer.Text = "Trailer Only"
        '
        'cmnuTrayMovieAllSkip
        '
        Me.cmnuTrayMovieAllSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieAllSkipAll})
        Me.cmnuTrayMovieAllSkip.Name = "cmnuTrayMovieAllSkip"
        Me.cmnuTrayMovieAllSkip.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieAllSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'cmnuTrayMovieAllSkipAll
        '
        Me.cmnuTrayMovieAllSkipAll.Name = "cmnuTrayMovieAllSkipAll"
        Me.cmnuTrayMovieAllSkipAll.Size = New System.Drawing.Size(120, 22)
        Me.cmnuTrayMovieAllSkipAll.Text = "All Items"
        '
        'cmnuTrayMovieMiss
        '
        Me.cmnuTrayMovieMiss.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieMissAuto, Me.cmnuTrayMovieMissAsk, Me.cmnuTrayMovieMissSkip})
        Me.cmnuTrayMovieMiss.Name = "cmnuTrayMovieMiss"
        Me.cmnuTrayMovieMiss.Size = New System.Drawing.Size(188, 22)
        Me.cmnuTrayMovieMiss.Text = "Movies Missing Items"
        '
        'cmnuTrayMovieMissAuto
        '
        Me.cmnuTrayMovieMissAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieMissAutoAll, Me.cmnuTrayMovieMissAutoActor, Me.cmnuTrayMovieMissAutoBanner, Me.cmnuTrayMovieMissAutoClearArt, Me.cmnuTrayMovieMissAutoClearLogo, Me.cmnuTrayMovieMissAutoDiscArt, Me.cmnuTrayMovieMissAutoEFanarts, Me.cmnuTrayMovieMissAutoEThumbs, Me.cmnuTrayMovieMissAutoFanart, Me.cmnuTrayMovieMissAutoLandscape, Me.cmnuTrayMovieMissAutoNfo, Me.cmnuTrayMovieMissAutoPoster, Me.cmnuTrayMovieMissAutoTheme, Me.cmnuTrayMovieMissAutoTrailer})
        Me.cmnuTrayMovieMissAuto.Name = "cmnuTrayMovieMissAuto"
        Me.cmnuTrayMovieMissAuto.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieMissAuto.Text = "Automatic (Force Best Match)"
        '
        'cmnuTrayMovieMissAutoAll
        '
        Me.cmnuTrayMovieMissAutoAll.Enabled = false
        Me.cmnuTrayMovieMissAutoAll.Name = "cmnuTrayMovieMissAutoAll"
        Me.cmnuTrayMovieMissAutoAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAutoAll.Text = "All Items"
        Me.cmnuTrayMovieMissAutoAll.Visible = false
        '
        'cmnuTrayMovieMissAutoActor
        '
        Me.cmnuTrayMovieMissAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayMovieMissAutoActor.Name = "cmnuTrayMovieMissAutoActor"
        Me.cmnuTrayMovieMissAutoActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAutoActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayMovieMissAutoBanner
        '
        Me.cmnuTrayMovieMissAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayMovieMissAutoBanner.Name = "cmnuTrayMovieMissAutoBanner"
        Me.cmnuTrayMovieMissAutoBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAutoBanner.Text = "Banner Only"
        '
        'cmnuTrayMovieMissAutoClearArt
        '
        Me.cmnuTrayMovieMissAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayMovieMissAutoClearArt.Name = "cmnuTrayMovieMissAutoClearArt"
        Me.cmnuTrayMovieMissAutoClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAutoClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayMovieMissAutoClearLogo
        '
        Me.cmnuTrayMovieMissAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayMovieMissAutoClearLogo.Name = "cmnuTrayMovieMissAutoClearLogo"
        Me.cmnuTrayMovieMissAutoClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAutoClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayMovieMissAutoDiscArt
        '
        Me.cmnuTrayMovieMissAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayMovieMissAutoDiscArt.Name = "cmnuTrayMovieMissAutoDiscArt"
        Me.cmnuTrayMovieMissAutoDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAutoDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayMovieMissAutoEFanarts
        '
        Me.cmnuTrayMovieMissAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayMovieMissAutoEFanarts.Name = "cmnuTrayMovieMissAutoEFanarts"
        Me.cmnuTrayMovieMissAutoEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAutoEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayMovieMissAutoEThumbs
        '
        Me.cmnuTrayMovieMissAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayMovieMissAutoEThumbs.Name = "cmnuTrayMovieMissAutoEThumbs"
        Me.cmnuTrayMovieMissAutoEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAutoEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayMovieMissAutoFanart
        '
        Me.cmnuTrayMovieMissAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayMovieMissAutoFanart.Name = "cmnuTrayMovieMissAutoFanart"
        Me.cmnuTrayMovieMissAutoFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAutoFanart.Text = "Fanart Only"
        '
        'cmnuTrayMovieMissAutoLandscape
        '
        Me.cmnuTrayMovieMissAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayMovieMissAutoLandscape.Name = "cmnuTrayMovieMissAutoLandscape"
        Me.cmnuTrayMovieMissAutoLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAutoLandscape.Text = "Landscape Only"
        '
        'cmnuTrayMovieMissAutoNfo
        '
        Me.cmnuTrayMovieMissAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayMovieMissAutoNfo.Name = "cmnuTrayMovieMissAutoNfo"
        Me.cmnuTrayMovieMissAutoNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAutoNfo.Text = "NFO Only"
        '
        'cmnuTrayMovieMissAutoPoster
        '
        Me.cmnuTrayMovieMissAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayMovieMissAutoPoster.Name = "cmnuTrayMovieMissAutoPoster"
        Me.cmnuTrayMovieMissAutoPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAutoPoster.Text = "Poster Only"
        '
        'cmnuTrayMovieMissAutoTheme
        '
        Me.cmnuTrayMovieMissAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayMovieMissAutoTheme.Name = "cmnuTrayMovieMissAutoTheme"
        Me.cmnuTrayMovieMissAutoTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAutoTheme.Text = "Theme Only"
        '
        'cmnuTrayMovieMissAutoTrailer
        '
        Me.cmnuTrayMovieMissAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayMovieMissAutoTrailer.Name = "cmnuTrayMovieMissAutoTrailer"
        Me.cmnuTrayMovieMissAutoTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAutoTrailer.Text = "Trailer Only"
        '
        'cmnuTrayMovieMissAsk
        '
        Me.cmnuTrayMovieMissAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieMissAskAll, Me.cmnuTrayMovieMissAskActor, Me.cmnuTrayMovieMissAskBanner, Me.cmnuTrayMovieMissAskClearArt, Me.cmnuTrayMovieMissAskClearLogo, Me.cmnuTrayMovieMissAskDiscArt, Me.cmnuTrayMovieMissAskEFanarts, Me.cmnuTrayMovieMissAskEThumbs, Me.cmnuTrayMovieMissAskFanart, Me.cmnuTrayMovieMissAskLandscape, Me.cmnuTrayMovieMissAskNfo, Me.cmnuTrayMovieMissAskPoster, Me.cmnuTrayMovieMissAskTheme, Me.cmnuTrayMovieMissAskTrailer})
        Me.cmnuTrayMovieMissAsk.Name = "cmnuTrayMovieMissAsk"
        Me.cmnuTrayMovieMissAsk.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieMissAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'cmnuTrayMovieMissAskAll
        '
        Me.cmnuTrayMovieMissAskAll.Enabled = false
        Me.cmnuTrayMovieMissAskAll.Name = "cmnuTrayMovieMissAskAll"
        Me.cmnuTrayMovieMissAskAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAskAll.Text = "All Items"
        Me.cmnuTrayMovieMissAskAll.Visible = false
        '
        'cmnuTrayMovieMissAskActor
        '
        Me.cmnuTrayMovieMissAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayMovieMissAskActor.Name = "cmnuTrayMovieMissAskActor"
        Me.cmnuTrayMovieMissAskActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAskActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayMovieMissAskBanner
        '
        Me.cmnuTrayMovieMissAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayMovieMissAskBanner.Name = "cmnuTrayMovieMissAskBanner"
        Me.cmnuTrayMovieMissAskBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAskBanner.Text = "Banner Only"
        '
        'cmnuTrayMovieMissAskClearArt
        '
        Me.cmnuTrayMovieMissAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayMovieMissAskClearArt.Name = "cmnuTrayMovieMissAskClearArt"
        Me.cmnuTrayMovieMissAskClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAskClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayMovieMissAskClearLogo
        '
        Me.cmnuTrayMovieMissAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayMovieMissAskClearLogo.Name = "cmnuTrayMovieMissAskClearLogo"
        Me.cmnuTrayMovieMissAskClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAskClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayMovieMissAskDiscArt
        '
        Me.cmnuTrayMovieMissAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayMovieMissAskDiscArt.Name = "cmnuTrayMovieMissAskDiscArt"
        Me.cmnuTrayMovieMissAskDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAskDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayMovieMissAskEFanarts
        '
        Me.cmnuTrayMovieMissAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayMovieMissAskEFanarts.Name = "cmnuTrayMovieMissAskEFanarts"
        Me.cmnuTrayMovieMissAskEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAskEFanarts.Text = "Extrfanarts Only"
        '
        'cmnuTrayMovieMissAskEThumbs
        '
        Me.cmnuTrayMovieMissAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayMovieMissAskEThumbs.Name = "cmnuTrayMovieMissAskEThumbs"
        Me.cmnuTrayMovieMissAskEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAskEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayMovieMissAskFanart
        '
        Me.cmnuTrayMovieMissAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayMovieMissAskFanart.Name = "cmnuTrayMovieMissAskFanart"
        Me.cmnuTrayMovieMissAskFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAskFanart.Text = "Fanart Only"
        '
        'cmnuTrayMovieMissAskLandscape
        '
        Me.cmnuTrayMovieMissAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayMovieMissAskLandscape.Name = "cmnuTrayMovieMissAskLandscape"
        Me.cmnuTrayMovieMissAskLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAskLandscape.Text = "Landscape Only"
        '
        'cmnuTrayMovieMissAskNfo
        '
        Me.cmnuTrayMovieMissAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayMovieMissAskNfo.Name = "cmnuTrayMovieMissAskNfo"
        Me.cmnuTrayMovieMissAskNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAskNfo.Text = "NFO Only"
        '
        'cmnuTrayMovieMissAskPoster
        '
        Me.cmnuTrayMovieMissAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayMovieMissAskPoster.Name = "cmnuTrayMovieMissAskPoster"
        Me.cmnuTrayMovieMissAskPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAskPoster.Text = "Poster Only"
        '
        'cmnuTrayMovieMissAskTheme
        '
        Me.cmnuTrayMovieMissAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayMovieMissAskTheme.Name = "cmnuTrayMovieMissAskTheme"
        Me.cmnuTrayMovieMissAskTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAskTheme.Text = "Theme Only"
        '
        'cmnuTrayMovieMissAskTrailer
        '
        Me.cmnuTrayMovieMissAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayMovieMissAskTrailer.Name = "cmnuTrayMovieMissAskTrailer"
        Me.cmnuTrayMovieMissAskTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMissAskTrailer.Text = "Trailer Only"
        '
        'cmnuTrayMovieMissSkip
        '
        Me.cmnuTrayMovieMissSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieMissSkipAll})
        Me.cmnuTrayMovieMissSkip.Enabled = false
        Me.cmnuTrayMovieMissSkip.Name = "cmnuTrayMovieMissSkip"
        Me.cmnuTrayMovieMissSkip.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieMissSkip.Text = "Skip (Skip If More Than One Match)"
        Me.cmnuTrayMovieMissSkip.Visible = false
        '
        'cmnuTrayMovieMissSkipAll
        '
        Me.cmnuTrayMovieMissSkipAll.Name = "cmnuTrayMovieMissSkipAll"
        Me.cmnuTrayMovieMissSkipAll.Size = New System.Drawing.Size(120, 22)
        Me.cmnuTrayMovieMissSkipAll.Text = "All Items"
        '
        'cmnuTrayMovieNew
        '
        Me.cmnuTrayMovieNew.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieNewAuto, Me.cmnuTrayMovieNewAsk, Me.cmnuTrayMovieNewSkip})
        Me.cmnuTrayMovieNew.Name = "cmnuTrayMovieNew"
        Me.cmnuTrayMovieNew.Size = New System.Drawing.Size(188, 22)
        Me.cmnuTrayMovieNew.Text = "New Movies"
        '
        'cmnuTrayMovieNewAuto
        '
        Me.cmnuTrayMovieNewAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieNewAutoAll, Me.cmnuTrayMovieNewAutoActor, Me.cmnuTrayMovieNewAutoBanner, Me.cmnuTrayMovieNewAutoClearArt, Me.cmnuTrayMovieNewAutoClearLogo, Me.cmnuTrayMovieNewAutoDiscArt, Me.cmnuTrayMovieNewAutoEFanarts, Me.cmnuTrayMovieNewAutoEThumbs, Me.cmnuTrayMovieNewAutoFanart, Me.cmnuTrayMovieNewAutoLandscape, Me.cmnuTrayMovieNewAutoMI, Me.cmnuTrayMovieNewAutoNfo, Me.cmnuTrayMovieNewAutoPoster, Me.cmnuTrayMovieNewAutoTheme, Me.cmnuTrayMovieNewAutoTrailer})
        Me.cmnuTrayMovieNewAuto.Name = "cmnuTrayMovieNewAuto"
        Me.cmnuTrayMovieNewAuto.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieNewAuto.Text = "Automatic (Force Best Match)"
        '
        'cmnuTrayMovieNewAutoAll
        '
        Me.cmnuTrayMovieNewAutoAll.Name = "cmnuTrayMovieNewAutoAll"
        Me.cmnuTrayMovieNewAutoAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoAll.Text = "All Items"
        '
        'cmnuTrayMovieNewAutoActor
        '
        Me.cmnuTrayMovieNewAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayMovieNewAutoActor.Name = "cmnuTrayMovieNewAutoActor"
        Me.cmnuTrayMovieNewAutoActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayMovieNewAutoBanner
        '
        Me.cmnuTrayMovieNewAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayMovieNewAutoBanner.Name = "cmnuTrayMovieNewAutoBanner"
        Me.cmnuTrayMovieNewAutoBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoBanner.Text = "Banner Only"
        '
        'cmnuTrayMovieNewAutoClearArt
        '
        Me.cmnuTrayMovieNewAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayMovieNewAutoClearArt.Name = "cmnuTrayMovieNewAutoClearArt"
        Me.cmnuTrayMovieNewAutoClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayMovieNewAutoClearLogo
        '
        Me.cmnuTrayMovieNewAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayMovieNewAutoClearLogo.Name = "cmnuTrayMovieNewAutoClearLogo"
        Me.cmnuTrayMovieNewAutoClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayMovieNewAutoDiscArt
        '
        Me.cmnuTrayMovieNewAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayMovieNewAutoDiscArt.Name = "cmnuTrayMovieNewAutoDiscArt"
        Me.cmnuTrayMovieNewAutoDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayMovieNewAutoEFanarts
        '
        Me.cmnuTrayMovieNewAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayMovieNewAutoEFanarts.Name = "cmnuTrayMovieNewAutoEFanarts"
        Me.cmnuTrayMovieNewAutoEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayMovieNewAutoEThumbs
        '
        Me.cmnuTrayMovieNewAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayMovieNewAutoEThumbs.Name = "cmnuTrayMovieNewAutoEThumbs"
        Me.cmnuTrayMovieNewAutoEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayMovieNewAutoFanart
        '
        Me.cmnuTrayMovieNewAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayMovieNewAutoFanart.Name = "cmnuTrayMovieNewAutoFanart"
        Me.cmnuTrayMovieNewAutoFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoFanart.Text = "Fanart Only"
        '
        'cmnuTrayMovieNewAutoLandscape
        '
        Me.cmnuTrayMovieNewAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayMovieNewAutoLandscape.Name = "cmnuTrayMovieNewAutoLandscape"
        Me.cmnuTrayMovieNewAutoLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoLandscape.Text = "Landscape Only"
        '
        'cmnuTrayMovieNewAutoMI
        '
        Me.cmnuTrayMovieNewAutoMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayMovieNewAutoMI.Name = "cmnuTrayMovieNewAutoMI"
        Me.cmnuTrayMovieNewAutoMI.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoMI.Text = "Meta Data Only"
        '
        'cmnuTrayMovieNewAutoNfo
        '
        Me.cmnuTrayMovieNewAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayMovieNewAutoNfo.Name = "cmnuTrayMovieNewAutoNfo"
        Me.cmnuTrayMovieNewAutoNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoNfo.Text = "NFO Only"
        '
        'cmnuTrayMovieNewAutoPoster
        '
        Me.cmnuTrayMovieNewAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayMovieNewAutoPoster.Name = "cmnuTrayMovieNewAutoPoster"
        Me.cmnuTrayMovieNewAutoPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoPoster.Text = "Poster Only"
        '
        'cmnuTrayMovieNewAutoTheme
        '
        Me.cmnuTrayMovieNewAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayMovieNewAutoTheme.Name = "cmnuTrayMovieNewAutoTheme"
        Me.cmnuTrayMovieNewAutoTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoTheme.Text = "Theme Only"
        '
        'cmnuTrayMovieNewAutoTrailer
        '
        Me.cmnuTrayMovieNewAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayMovieNewAutoTrailer.Name = "cmnuTrayMovieNewAutoTrailer"
        Me.cmnuTrayMovieNewAutoTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAutoTrailer.Text = "Trailer Only"
        '
        'cmnuTrayMovieNewAsk
        '
        Me.cmnuTrayMovieNewAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieNewAskAll, Me.cmnuTrayMovieNewAskActor, Me.cmnuTrayMovieNewAskBanner, Me.cmnuTrayMovieNewAskClearArt, Me.cmnuTrayMovieNewAskClearLogo, Me.cmnuTrayMovieNewAskDiscArt, Me.cmnuTrayMovieNewAskEFanarts, Me.cmnuTrayMovieNewAskEThumbs, Me.cmnuTrayMovieNewAskFanart, Me.cmnuTrayMovieNewAskLandscape, Me.cmnuTrayMovieNewAskMI, Me.cmnuTrayMovieNewAskNfo, Me.cmnuTrayMovieNewAskPoster, Me.cmnuTrayMovieNewAskTheme, Me.cmnuTrayMovieNewAskTrailer})
        Me.cmnuTrayMovieNewAsk.Name = "cmnuTrayMovieNewAsk"
        Me.cmnuTrayMovieNewAsk.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieNewAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'cmnuTrayMovieNewAskAll
        '
        Me.cmnuTrayMovieNewAskAll.Name = "cmnuTrayMovieNewAskAll"
        Me.cmnuTrayMovieNewAskAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskAll.Text = "All Items"
        '
        'cmnuTrayMovieNewAskActor
        '
        Me.cmnuTrayMovieNewAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayMovieNewAskActor.Name = "cmnuTrayMovieNewAskActor"
        Me.cmnuTrayMovieNewAskActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayMovieNewAskBanner
        '
        Me.cmnuTrayMovieNewAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayMovieNewAskBanner.Name = "cmnuTrayMovieNewAskBanner"
        Me.cmnuTrayMovieNewAskBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskBanner.Text = "Banner Only"
        '
        'cmnuTrayMovieNewAskClearArt
        '
        Me.cmnuTrayMovieNewAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayMovieNewAskClearArt.Name = "cmnuTrayMovieNewAskClearArt"
        Me.cmnuTrayMovieNewAskClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayMovieNewAskClearLogo
        '
        Me.cmnuTrayMovieNewAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayMovieNewAskClearLogo.Name = "cmnuTrayMovieNewAskClearLogo"
        Me.cmnuTrayMovieNewAskClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayMovieNewAskDiscArt
        '
        Me.cmnuTrayMovieNewAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayMovieNewAskDiscArt.Name = "cmnuTrayMovieNewAskDiscArt"
        Me.cmnuTrayMovieNewAskDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayMovieNewAskEFanarts
        '
        Me.cmnuTrayMovieNewAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayMovieNewAskEFanarts.Name = "cmnuTrayMovieNewAskEFanarts"
        Me.cmnuTrayMovieNewAskEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayMovieNewAskEThumbs
        '
        Me.cmnuTrayMovieNewAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayMovieNewAskEThumbs.Name = "cmnuTrayMovieNewAskEThumbs"
        Me.cmnuTrayMovieNewAskEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayMovieNewAskFanart
        '
        Me.cmnuTrayMovieNewAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayMovieNewAskFanart.Name = "cmnuTrayMovieNewAskFanart"
        Me.cmnuTrayMovieNewAskFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskFanart.Text = "Fanart Only"
        '
        'cmnuTrayMovieNewAskLandscape
        '
        Me.cmnuTrayMovieNewAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayMovieNewAskLandscape.Name = "cmnuTrayMovieNewAskLandscape"
        Me.cmnuTrayMovieNewAskLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskLandscape.Text = "Landscape Only"
        '
        'cmnuTrayMovieNewAskMI
        '
        Me.cmnuTrayMovieNewAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayMovieNewAskMI.Name = "cmnuTrayMovieNewAskMI"
        Me.cmnuTrayMovieNewAskMI.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskMI.Text = "Meta Data Only"
        '
        'cmnuTrayMovieNewAskNfo
        '
        Me.cmnuTrayMovieNewAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayMovieNewAskNfo.Name = "cmnuTrayMovieNewAskNfo"
        Me.cmnuTrayMovieNewAskNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskNfo.Text = "NFO Only"
        '
        'cmnuTrayMovieNewAskPoster
        '
        Me.cmnuTrayMovieNewAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayMovieNewAskPoster.Name = "cmnuTrayMovieNewAskPoster"
        Me.cmnuTrayMovieNewAskPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskPoster.Text = "Poster Only"
        '
        'cmnuTrayMovieNewAskTheme
        '
        Me.cmnuTrayMovieNewAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayMovieNewAskTheme.Name = "cmnuTrayMovieNewAskTheme"
        Me.cmnuTrayMovieNewAskTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskTheme.Text = "Theme Only"
        '
        'cmnuTrayMovieNewAskTrailer
        '
        Me.cmnuTrayMovieNewAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayMovieNewAskTrailer.Name = "cmnuTrayMovieNewAskTrailer"
        Me.cmnuTrayMovieNewAskTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieNewAskTrailer.Text = "Trailer Only"
        '
        'cmnuTrayMovieNewSkip
        '
        Me.cmnuTrayMovieNewSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieNewSkipAll})
        Me.cmnuTrayMovieNewSkip.Name = "cmnuTrayMovieNewSkip"
        Me.cmnuTrayMovieNewSkip.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieNewSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'cmnuTrayMovieNewSkipAll
        '
        Me.cmnuTrayMovieNewSkipAll.Name = "cmnuTrayMovieNewSkipAll"
        Me.cmnuTrayMovieNewSkipAll.Size = New System.Drawing.Size(120, 22)
        Me.cmnuTrayMovieNewSkipAll.Text = "All Items"
        '
        'cmnuTrayMovieMark
        '
        Me.cmnuTrayMovieMark.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieMarkAuto, Me.cmnuTrayMovieMarkAsk, Me.cmnuTrayMovieMarkSkip})
        Me.cmnuTrayMovieMark.Name = "cmnuTrayMovieMark"
        Me.cmnuTrayMovieMark.Size = New System.Drawing.Size(188, 22)
        Me.cmnuTrayMovieMark.Text = "Marked Movies"
        '
        'cmnuTrayMovieMarkAuto
        '
        Me.cmnuTrayMovieMarkAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieMarkAutoAll, Me.cmnuTrayMovieMarkAutoActor, Me.cmnuTrayMovieMarkAutoBanner, Me.cmnuTrayMovieMarkAutoClearArt, Me.cmnuTrayMovieMarkAutoClearLogo, Me.cmnuTrayMovieMarkAutoDiscArt, Me.cmnuTrayMovieMarkAutoEFanarts, Me.cmnuTrayMovieMarkAutoEThumbs, Me.cmnuTrayMovieMarkAutoFanart, Me.cmnuTrayMovieMarkAutoLandscape, Me.cmnuTrayMovieMarkAutoMI, Me.cmnuTrayMovieMarkAutoNfo, Me.cmnuTrayMovieMarkAutoPoster, Me.cmnuTrayMovieMarkAutoTheme, Me.cmnuTrayMovieMarkAutoTrailer})
        Me.cmnuTrayMovieMarkAuto.Name = "cmnuTrayMovieMarkAuto"
        Me.cmnuTrayMovieMarkAuto.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieMarkAuto.Text = "Automatic (Force Best Match)"
        '
        'cmnuTrayMovieMarkAutoAll
        '
        Me.cmnuTrayMovieMarkAutoAll.Name = "cmnuTrayMovieMarkAutoAll"
        Me.cmnuTrayMovieMarkAutoAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoAll.Text = "All Items"
        '
        'cmnuTrayMovieMarkAutoActor
        '
        Me.cmnuTrayMovieMarkAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayMovieMarkAutoActor.Name = "cmnuTrayMovieMarkAutoActor"
        Me.cmnuTrayMovieMarkAutoActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayMovieMarkAutoBanner
        '
        Me.cmnuTrayMovieMarkAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayMovieMarkAutoBanner.Name = "cmnuTrayMovieMarkAutoBanner"
        Me.cmnuTrayMovieMarkAutoBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoBanner.Text = "Banner Only"
        '
        'cmnuTrayMovieMarkAutoClearArt
        '
        Me.cmnuTrayMovieMarkAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayMovieMarkAutoClearArt.Name = "cmnuTrayMovieMarkAutoClearArt"
        Me.cmnuTrayMovieMarkAutoClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayMovieMarkAutoClearLogo
        '
        Me.cmnuTrayMovieMarkAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayMovieMarkAutoClearLogo.Name = "cmnuTrayMovieMarkAutoClearLogo"
        Me.cmnuTrayMovieMarkAutoClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayMovieMarkAutoDiscArt
        '
        Me.cmnuTrayMovieMarkAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayMovieMarkAutoDiscArt.Name = "cmnuTrayMovieMarkAutoDiscArt"
        Me.cmnuTrayMovieMarkAutoDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayMovieMarkAutoEFanarts
        '
        Me.cmnuTrayMovieMarkAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayMovieMarkAutoEFanarts.Name = "cmnuTrayMovieMarkAutoEFanarts"
        Me.cmnuTrayMovieMarkAutoEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayMovieMarkAutoEThumbs
        '
        Me.cmnuTrayMovieMarkAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayMovieMarkAutoEThumbs.Name = "cmnuTrayMovieMarkAutoEThumbs"
        Me.cmnuTrayMovieMarkAutoEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayMovieMarkAutoFanart
        '
        Me.cmnuTrayMovieMarkAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayMovieMarkAutoFanart.Name = "cmnuTrayMovieMarkAutoFanart"
        Me.cmnuTrayMovieMarkAutoFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoFanart.Text = "Fanart Only"
        '
        'cmnuTrayMovieMarkAutoLandscape
        '
        Me.cmnuTrayMovieMarkAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayMovieMarkAutoLandscape.Name = "cmnuTrayMovieMarkAutoLandscape"
        Me.cmnuTrayMovieMarkAutoLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoLandscape.Text = "Landscape Only"
        '
        'cmnuTrayMovieMarkAutoMI
        '
        Me.cmnuTrayMovieMarkAutoMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayMovieMarkAutoMI.Name = "cmnuTrayMovieMarkAutoMI"
        Me.cmnuTrayMovieMarkAutoMI.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoMI.Text = "Meta Data Only"
        '
        'cmnuTrayMovieMarkAutoNfo
        '
        Me.cmnuTrayMovieMarkAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayMovieMarkAutoNfo.Name = "cmnuTrayMovieMarkAutoNfo"
        Me.cmnuTrayMovieMarkAutoNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoNfo.Text = "NFO Only"
        '
        'cmnuTrayMovieMarkAutoPoster
        '
        Me.cmnuTrayMovieMarkAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayMovieMarkAutoPoster.Name = "cmnuTrayMovieMarkAutoPoster"
        Me.cmnuTrayMovieMarkAutoPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoPoster.Text = "Poster Only"
        '
        'cmnuTrayMovieMarkAutoTheme
        '
        Me.cmnuTrayMovieMarkAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayMovieMarkAutoTheme.Name = "cmnuTrayMovieMarkAutoTheme"
        Me.cmnuTrayMovieMarkAutoTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoTheme.Text = "Theme Only"
        '
        'cmnuTrayMovieMarkAutoTrailer
        '
        Me.cmnuTrayMovieMarkAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayMovieMarkAutoTrailer.Name = "cmnuTrayMovieMarkAutoTrailer"
        Me.cmnuTrayMovieMarkAutoTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAutoTrailer.Text = "Trailer Only"
        '
        'cmnuTrayMovieMarkAsk
        '
        Me.cmnuTrayMovieMarkAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieMarkAskAll, Me.cmnuTrayMovieMarkAskActor, Me.cmnuTrayMovieMarkAskBanner, Me.cmnuTrayMovieMarkAskClearArt, Me.cmnuTrayMovieMarkAskClearLogo, Me.cmnuTrayMovieMarkAskDiscArt, Me.cmnuTrayMovieMarkAskEFanarts, Me.cmnuTrayMovieMarkAskEThumbs, Me.cmnuTrayMovieMarkAskFanart, Me.cmnuTrayMovieMarkAskLandscape, Me.cmnuTrayMovieMarkAskMI, Me.cmnuTrayMovieMarkAskNfo, Me.cmnuTrayMovieMarkAskPoster, Me.cmnuTrayMovieMarkAskTheme, Me.cmnuTrayMovieMarkAskTrailer})
        Me.cmnuTrayMovieMarkAsk.Name = "cmnuTrayMovieMarkAsk"
        Me.cmnuTrayMovieMarkAsk.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieMarkAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'cmnuTrayMovieMarkAskAll
        '
        Me.cmnuTrayMovieMarkAskAll.Name = "cmnuTrayMovieMarkAskAll"
        Me.cmnuTrayMovieMarkAskAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskAll.Text = "All Items"
        '
        'cmnuTrayMovieMarkAskActor
        '
        Me.cmnuTrayMovieMarkAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayMovieMarkAskActor.Name = "cmnuTrayMovieMarkAskActor"
        Me.cmnuTrayMovieMarkAskActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayMovieMarkAskBanner
        '
        Me.cmnuTrayMovieMarkAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayMovieMarkAskBanner.Name = "cmnuTrayMovieMarkAskBanner"
        Me.cmnuTrayMovieMarkAskBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskBanner.Text = "Banner Only"
        '
        'cmnuTrayMovieMarkAskClearArt
        '
        Me.cmnuTrayMovieMarkAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayMovieMarkAskClearArt.Name = "cmnuTrayMovieMarkAskClearArt"
        Me.cmnuTrayMovieMarkAskClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayMovieMarkAskClearLogo
        '
        Me.cmnuTrayMovieMarkAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayMovieMarkAskClearLogo.Name = "cmnuTrayMovieMarkAskClearLogo"
        Me.cmnuTrayMovieMarkAskClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayMovieMarkAskDiscArt
        '
        Me.cmnuTrayMovieMarkAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayMovieMarkAskDiscArt.Name = "cmnuTrayMovieMarkAskDiscArt"
        Me.cmnuTrayMovieMarkAskDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayMovieMarkAskEFanarts
        '
        Me.cmnuTrayMovieMarkAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayMovieMarkAskEFanarts.Name = "cmnuTrayMovieMarkAskEFanarts"
        Me.cmnuTrayMovieMarkAskEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayMovieMarkAskEThumbs
        '
        Me.cmnuTrayMovieMarkAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayMovieMarkAskEThumbs.Name = "cmnuTrayMovieMarkAskEThumbs"
        Me.cmnuTrayMovieMarkAskEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayMovieMarkAskFanart
        '
        Me.cmnuTrayMovieMarkAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayMovieMarkAskFanart.Name = "cmnuTrayMovieMarkAskFanart"
        Me.cmnuTrayMovieMarkAskFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskFanart.Text = "Fanart Only"
        '
        'cmnuTrayMovieMarkAskLandscape
        '
        Me.cmnuTrayMovieMarkAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayMovieMarkAskLandscape.Name = "cmnuTrayMovieMarkAskLandscape"
        Me.cmnuTrayMovieMarkAskLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskLandscape.Text = "Landscape Only"
        '
        'cmnuTrayMovieMarkAskMI
        '
        Me.cmnuTrayMovieMarkAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayMovieMarkAskMI.Name = "cmnuTrayMovieMarkAskMI"
        Me.cmnuTrayMovieMarkAskMI.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskMI.Text = "Meta Data Only"
        '
        'cmnuTrayMovieMarkAskNfo
        '
        Me.cmnuTrayMovieMarkAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayMovieMarkAskNfo.Name = "cmnuTrayMovieMarkAskNfo"
        Me.cmnuTrayMovieMarkAskNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskNfo.Text = "NFO Only"
        '
        'cmnuTrayMovieMarkAskPoster
        '
        Me.cmnuTrayMovieMarkAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayMovieMarkAskPoster.Name = "cmnuTrayMovieMarkAskPoster"
        Me.cmnuTrayMovieMarkAskPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskPoster.Text = "Poster Only"
        '
        'cmnuTrayMovieMarkAskTheme
        '
        Me.cmnuTrayMovieMarkAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayMovieMarkAskTheme.Name = "cmnuTrayMovieMarkAskTheme"
        Me.cmnuTrayMovieMarkAskTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskTheme.Text = "Theme Only"
        '
        'cmnuTrayMovieMarkAskTrailer
        '
        Me.cmnuTrayMovieMarkAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayMovieMarkAskTrailer.Name = "cmnuTrayMovieMarkAskTrailer"
        Me.cmnuTrayMovieMarkAskTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieMarkAskTrailer.Text = "Trailer Only"
        '
        'cmnuTrayMovieMarkSkip
        '
        Me.cmnuTrayMovieMarkSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieMarkSkipAll})
        Me.cmnuTrayMovieMarkSkip.Name = "cmnuTrayMovieMarkSkip"
        Me.cmnuTrayMovieMarkSkip.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieMarkSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'cmnuTrayMovieMarkSkipAll
        '
        Me.cmnuTrayMovieMarkSkipAll.Name = "cmnuTrayMovieMarkSkipAll"
        Me.cmnuTrayMovieMarkSkipAll.Size = New System.Drawing.Size(120, 22)
        Me.cmnuTrayMovieMarkSkipAll.Text = "All Items"
        '
        'cmnuTrayMovieFilter
        '
        Me.cmnuTrayMovieFilter.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieFilterAuto, Me.cmnuTrayMovieFilterAsk, Me.cmnuTrayMovieFilterSkip})
        Me.cmnuTrayMovieFilter.Name = "cmnuTrayMovieFilter"
        Me.cmnuTrayMovieFilter.Size = New System.Drawing.Size(188, 22)
        Me.cmnuTrayMovieFilter.Text = "Current Filter"
        '
        'cmnuTrayMovieFilterAuto
        '
        Me.cmnuTrayMovieFilterAuto.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieFilterAutoAll, Me.cmnuTrayMovieFilterAutoActor, Me.cmnuTrayMovieFilterAutoBanner, Me.cmnuTrayMovieFilterAutoClearArt, Me.cmnuTrayMovieFilterAutoClearLogo, Me.cmnuTrayMovieFilterAutoDiscArt, Me.cmnuTrayMovieFilterAutoEFanarts, Me.cmnuTrayMovieFilterAutoEThumbs, Me.cmnuTrayMovieFilterAutoFanart, Me.cmnuTrayMovieFilterAutoLandscape, Me.cmnuTrayMovieFilterAutoMI, Me.cmnuTrayMovieFilterAutoNfo, Me.cmnuTrayMovieFilterAutoPoster, Me.cmnuTrayMovieFilterAutoTheme, Me.cmnuTrayMovieFilterAutoTrailer})
        Me.cmnuTrayMovieFilterAuto.Name = "cmnuTrayMovieFilterAuto"
        Me.cmnuTrayMovieFilterAuto.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieFilterAuto.Text = "Automatic (Force Best Match)"
        '
        'cmnuTrayMovieFilterAutoAll
        '
        Me.cmnuTrayMovieFilterAutoAll.Name = "cmnuTrayMovieFilterAutoAll"
        Me.cmnuTrayMovieFilterAutoAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoAll.Text = "All Items"
        '
        'cmnuTrayMovieFilterAutoActor
        '
        Me.cmnuTrayMovieFilterAutoActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayMovieFilterAutoActor.Name = "cmnuTrayMovieFilterAutoActor"
        Me.cmnuTrayMovieFilterAutoActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayMovieFilterAutoBanner
        '
        Me.cmnuTrayMovieFilterAutoBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayMovieFilterAutoBanner.Name = "cmnuTrayMovieFilterAutoBanner"
        Me.cmnuTrayMovieFilterAutoBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoBanner.Text = "Banner Only"
        '
        'cmnuTrayMovieFilterAutoClearArt
        '
        Me.cmnuTrayMovieFilterAutoClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayMovieFilterAutoClearArt.Name = "cmnuTrayMovieFilterAutoClearArt"
        Me.cmnuTrayMovieFilterAutoClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayMovieFilterAutoClearLogo
        '
        Me.cmnuTrayMovieFilterAutoClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayMovieFilterAutoClearLogo.Name = "cmnuTrayMovieFilterAutoClearLogo"
        Me.cmnuTrayMovieFilterAutoClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayMovieFilterAutoDiscArt
        '
        Me.cmnuTrayMovieFilterAutoDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayMovieFilterAutoDiscArt.Name = "cmnuTrayMovieFilterAutoDiscArt"
        Me.cmnuTrayMovieFilterAutoDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayMovieFilterAutoEFanarts
        '
        Me.cmnuTrayMovieFilterAutoEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayMovieFilterAutoEFanarts.Name = "cmnuTrayMovieFilterAutoEFanarts"
        Me.cmnuTrayMovieFilterAutoEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayMovieFilterAutoEThumbs
        '
        Me.cmnuTrayMovieFilterAutoEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayMovieFilterAutoEThumbs.Name = "cmnuTrayMovieFilterAutoEThumbs"
        Me.cmnuTrayMovieFilterAutoEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayMovieFilterAutoFanart
        '
        Me.cmnuTrayMovieFilterAutoFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayMovieFilterAutoFanart.Name = "cmnuTrayMovieFilterAutoFanart"
        Me.cmnuTrayMovieFilterAutoFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoFanart.Text = "Fanart Only"
        '
        'cmnuTrayMovieFilterAutoLandscape
        '
        Me.cmnuTrayMovieFilterAutoLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayMovieFilterAutoLandscape.Name = "cmnuTrayMovieFilterAutoLandscape"
        Me.cmnuTrayMovieFilterAutoLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoLandscape.Text = "Landscape Only"
        '
        'cmnuTrayMovieFilterAutoMI
        '
        Me.cmnuTrayMovieFilterAutoMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayMovieFilterAutoMI.Name = "cmnuTrayMovieFilterAutoMI"
        Me.cmnuTrayMovieFilterAutoMI.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoMI.Text = "Meta Data Only"
        '
        'cmnuTrayMovieFilterAutoNfo
        '
        Me.cmnuTrayMovieFilterAutoNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayMovieFilterAutoNfo.Name = "cmnuTrayMovieFilterAutoNfo"
        Me.cmnuTrayMovieFilterAutoNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoNfo.Text = "NFO Only"
        '
        'cmnuTrayMovieFilterAutoPoster
        '
        Me.cmnuTrayMovieFilterAutoPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayMovieFilterAutoPoster.Name = "cmnuTrayMovieFilterAutoPoster"
        Me.cmnuTrayMovieFilterAutoPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoPoster.Text = "Poster Only"
        '
        'cmnuTrayMovieFilterAutoTheme
        '
        Me.cmnuTrayMovieFilterAutoTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayMovieFilterAutoTheme.Name = "cmnuTrayMovieFilterAutoTheme"
        Me.cmnuTrayMovieFilterAutoTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoTheme.Text = "Theme Only"
        '
        'cmnuTrayMovieFilterAutoTrailer
        '
        Me.cmnuTrayMovieFilterAutoTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayMovieFilterAutoTrailer.Name = "cmnuTrayMovieFilterAutoTrailer"
        Me.cmnuTrayMovieFilterAutoTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAutoTrailer.Text = "Trailer Only"
        '
        'cmnuTrayMovieFilterAsk
        '
        Me.cmnuTrayMovieFilterAsk.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieFilterAskAll, Me.cmnuTrayMovieFilterAskActor, Me.cmnuTrayMovieFilterAskBanner, Me.cmnuTrayMovieFilterAskClearArt, Me.cmnuTrayMovieFilterAskClearLogo, Me.cmnuTrayMovieFilterAskDiscArt, Me.cmnuTrayMovieFilterAskEFanarts, Me.cmnuTrayMovieFilterAskEThumbs, Me.cmnuTrayMovieFilterAskFanart, Me.cmnuTrayMovieFilterAskLandscape, Me.cmnuTrayMovieFilterAskMI, Me.cmnuTrayMovieFilterAskNfo, Me.cmnuTrayMovieFilterAskPoster, Me.cmnuTrayMovieFilterAskTheme, Me.cmnuTrayMovieFilterAskTrailer})
        Me.cmnuTrayMovieFilterAsk.Name = "cmnuTrayMovieFilterAsk"
        Me.cmnuTrayMovieFilterAsk.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieFilterAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'cmnuTrayMovieFilterAskAll
        '
        Me.cmnuTrayMovieFilterAskAll.Name = "cmnuTrayMovieFilterAskAll"
        Me.cmnuTrayMovieFilterAskAll.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskAll.Text = "All Items"
        '
        'cmnuTrayMovieFilterAskActor
        '
        Me.cmnuTrayMovieFilterAskActor.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.cmnuTrayMovieFilterAskActor.Name = "cmnuTrayMovieFilterAskActor"
        Me.cmnuTrayMovieFilterAskActor.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskActor.Text = "Actor Thumbs Only"
        '
        'cmnuTrayMovieFilterAskBanner
        '
        Me.cmnuTrayMovieFilterAskBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.cmnuTrayMovieFilterAskBanner.Name = "cmnuTrayMovieFilterAskBanner"
        Me.cmnuTrayMovieFilterAskBanner.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskBanner.Text = "Banner Only"
        '
        'cmnuTrayMovieFilterAskClearArt
        '
        Me.cmnuTrayMovieFilterAskClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.cmnuTrayMovieFilterAskClearArt.Name = "cmnuTrayMovieFilterAskClearArt"
        Me.cmnuTrayMovieFilterAskClearArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskClearArt.Text = "ClearArt Only"
        '
        'cmnuTrayMovieFilterAskClearLogo
        '
        Me.cmnuTrayMovieFilterAskClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.cmnuTrayMovieFilterAskClearLogo.Name = "cmnuTrayMovieFilterAskClearLogo"
        Me.cmnuTrayMovieFilterAskClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskClearLogo.Text = "ClearLogo Only"
        '
        'cmnuTrayMovieFilterAskDiscArt
        '
        Me.cmnuTrayMovieFilterAskDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.cmnuTrayMovieFilterAskDiscArt.Name = "cmnuTrayMovieFilterAskDiscArt"
        Me.cmnuTrayMovieFilterAskDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskDiscArt.Text = "DiscArt Only"
        '
        'cmnuTrayMovieFilterAskEFanarts
        '
        Me.cmnuTrayMovieFilterAskEFanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.cmnuTrayMovieFilterAskEFanarts.Name = "cmnuTrayMovieFilterAskEFanarts"
        Me.cmnuTrayMovieFilterAskEFanarts.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskEFanarts.Text = "Extrafanarts Only"
        '
        'cmnuTrayMovieFilterAskEThumbs
        '
        Me.cmnuTrayMovieFilterAskEThumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.cmnuTrayMovieFilterAskEThumbs.Name = "cmnuTrayMovieFilterAskEThumbs"
        Me.cmnuTrayMovieFilterAskEThumbs.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskEThumbs.Text = "Extrathumbs Only"
        '
        'cmnuTrayMovieFilterAskFanart
        '
        Me.cmnuTrayMovieFilterAskFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.cmnuTrayMovieFilterAskFanart.Name = "cmnuTrayMovieFilterAskFanart"
        Me.cmnuTrayMovieFilterAskFanart.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskFanart.Text = "Fanart Only"
        '
        'cmnuTrayMovieFilterAskLandscape
        '
        Me.cmnuTrayMovieFilterAskLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.cmnuTrayMovieFilterAskLandscape.Name = "cmnuTrayMovieFilterAskLandscape"
        Me.cmnuTrayMovieFilterAskLandscape.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskLandscape.Text = "Landscape Only"
        '
        'cmnuTrayMovieFilterAskMI
        '
        Me.cmnuTrayMovieFilterAskMI.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.cmnuTrayMovieFilterAskMI.Name = "cmnuTrayMovieFilterAskMI"
        Me.cmnuTrayMovieFilterAskMI.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskMI.Text = "Meta Data Only"
        '
        'cmnuTrayMovieFilterAskNfo
        '
        Me.cmnuTrayMovieFilterAskNfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.cmnuTrayMovieFilterAskNfo.Name = "cmnuTrayMovieFilterAskNfo"
        Me.cmnuTrayMovieFilterAskNfo.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskNfo.Text = "NFO Only"
        '
        'cmnuTrayMovieFilterAskPoster
        '
        Me.cmnuTrayMovieFilterAskPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.cmnuTrayMovieFilterAskPoster.Name = "cmnuTrayMovieFilterAskPoster"
        Me.cmnuTrayMovieFilterAskPoster.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskPoster.Text = "Poster Only"
        '
        'cmnuTrayMovieFilterAskTheme
        '
        Me.cmnuTrayMovieFilterAskTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.cmnuTrayMovieFilterAskTheme.Name = "cmnuTrayMovieFilterAskTheme"
        Me.cmnuTrayMovieFilterAskTheme.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskTheme.Text = "Theme Only"
        '
        'cmnuTrayMovieFilterAskTrailer
        '
        Me.cmnuTrayMovieFilterAskTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.cmnuTrayMovieFilterAskTrailer.Name = "cmnuTrayMovieFilterAskTrailer"
        Me.cmnuTrayMovieFilterAskTrailer.Size = New System.Drawing.Size(178, 22)
        Me.cmnuTrayMovieFilterAskTrailer.Text = "Trailer Only"
        '
        'cmnuTrayMovieFilterSkip
        '
        Me.cmnuTrayMovieFilterSkip.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayMovieFilterSkipAll})
        Me.cmnuTrayMovieFilterSkip.Name = "cmnuTrayMovieFilterSkip"
        Me.cmnuTrayMovieFilterSkip.Size = New System.Drawing.Size(271, 22)
        Me.cmnuTrayMovieFilterSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'cmnuTrayMovieFilterSkipAll
        '
        Me.cmnuTrayMovieFilterSkipAll.Name = "cmnuTrayMovieFilterSkipAll"
        Me.cmnuTrayMovieFilterSkipAll.Size = New System.Drawing.Size(120, 22)
        Me.cmnuTrayMovieFilterSkipAll.Text = "All Items"
        '
        'cmnuTrayMovieCustom
        '
        Me.cmnuTrayMovieCustom.Name = "cmnuTrayMovieCustom"
        Me.cmnuTrayMovieCustom.Size = New System.Drawing.Size(188, 22)
        Me.cmnuTrayMovieCustom.Text = "Custom Scraper..."
        '
        'cmnuTrayMovieRestart
        '
        Me.cmnuTrayMovieRestart.Name = "cmnuTrayMovieRestart"
        Me.cmnuTrayMovieRestart.Size = New System.Drawing.Size(188, 22)
        Me.cmnuTrayMovieRestart.Text = "Restart last scrape..."
        '
        'cmnuTrayMediaCenters
        '
        Me.cmnuTrayMediaCenters.Image = CType(resources.GetObject("cmnuTrayMediaCenters.Image"),System.Drawing.Image)
        Me.cmnuTrayMediaCenters.Name = "cmnuTrayMediaCenters"
        Me.cmnuTrayMediaCenters.Size = New System.Drawing.Size(194, 22)
        Me.cmnuTrayMediaCenters.Text = "Media Centers"
        Me.cmnuTrayMediaCenters.Visible = false
        '
        'ToolStripSeparator23
        '
        Me.ToolStripSeparator23.Name = "ToolStripSeparator23"
        Me.ToolStripSeparator23.Size = New System.Drawing.Size(191, 6)
        '
        'cmnuTrayTools
        '
        Me.cmnuTrayTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayToolsCleanFiles, Me.cmnuTrayToolsSortFiles, Me.cmnuTrayToolsBackdrops, Me.ToolStripSeparator24, Me.cmnuTrayToolsOfflineHolder, Me.ToolStripSeparator25, Me.cmnuTrayToolsClearCache, Me.cmnuTrayToolsReloadMovies, Me.cmnuTrayToolsCleanDB, Me.ToolStripSeparator26})
        Me.cmnuTrayTools.Image = CType(resources.GetObject("cmnuTrayTools.Image"),System.Drawing.Image)
        Me.cmnuTrayTools.Name = "cmnuTrayTools"
        Me.cmnuTrayTools.Size = New System.Drawing.Size(194, 22)
        Me.cmnuTrayTools.Text = "Tools"
        '
        'cmnuTrayToolsCleanFiles
        '
        Me.cmnuTrayToolsCleanFiles.Image = CType(resources.GetObject("cmnuTrayToolsCleanFiles.Image"),System.Drawing.Image)
        Me.cmnuTrayToolsCleanFiles.Name = "cmnuTrayToolsCleanFiles"
        Me.cmnuTrayToolsCleanFiles.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsCleanFiles.Text = "Clean Files"
        '
        'cmnuTrayToolsSortFiles
        '
        Me.cmnuTrayToolsSortFiles.Image = CType(resources.GetObject("cmnuTrayToolsSortFiles.Image"),System.Drawing.Image)
        Me.cmnuTrayToolsSortFiles.Name = "cmnuTrayToolsSortFiles"
        Me.cmnuTrayToolsSortFiles.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsSortFiles.Text = "Sort Files into Folders"
        '
        'cmnuTrayToolsBackdrops
        '
        Me.cmnuTrayToolsBackdrops.Image = CType(resources.GetObject("cmnuTrayToolsBackdrops.Image"),System.Drawing.Image)
        Me.cmnuTrayToolsBackdrops.Name = "cmnuTrayToolsBackdrops"
        Me.cmnuTrayToolsBackdrops.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsBackdrops.Text = "Copy Existing Fanart to Backdrops Folder"
        '
        'ToolStripSeparator24
        '
        Me.ToolStripSeparator24.Name = "ToolStripSeparator24"
        Me.ToolStripSeparator24.Size = New System.Drawing.Size(286, 6)
        '
        'cmnuTrayToolsOfflineHolder
        '
        Me.cmnuTrayToolsOfflineHolder.Image = CType(resources.GetObject("cmnuTrayToolsOfflineHolder.Image"),System.Drawing.Image)
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
        Me.cmnuTrayToolsClearCache.Image = CType(resources.GetObject("cmnuTrayToolsClearCache.Image"),System.Drawing.Image)
        Me.cmnuTrayToolsClearCache.Name = "cmnuTrayToolsClearCache"
        Me.cmnuTrayToolsClearCache.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsClearCache.Text = "Clear All Caches"
        '
        'cmnuTrayToolsReloadMovies
        '
        Me.cmnuTrayToolsReloadMovies.Image = CType(resources.GetObject("cmnuTrayToolsReloadMovies.Image"),System.Drawing.Image)
        Me.cmnuTrayToolsReloadMovies.Name = "cmnuTrayToolsReloadMovies"
        Me.cmnuTrayToolsReloadMovies.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsReloadMovies.Text = "Reload All Movies"
        '
        'cmnuTrayToolsCleanDB
        '
        Me.cmnuTrayToolsCleanDB.Image = CType(resources.GetObject("cmnuTrayToolsCleanDB.Image"),System.Drawing.Image)
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
        Me.cmnuTraySettings.Image = CType(resources.GetObject("cmnuTraySettings.Image"),System.Drawing.Image)
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
        Me.cmnuTrayExit.Image = CType(resources.GetObject("cmnuTrayExit.Image"),System.Drawing.Image)
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
        Me.pbLoadSettings.Image = CType(resources.GetObject("pbLoadSettings.Image"),System.Drawing.Image)
        Me.pbLoadSettings.Location = New System.Drawing.Point(12, 11)
        Me.pbLoadSettings.Name = "pbLoadSettings"
        Me.pbLoadSettings.Size = New System.Drawing.Size(48, 48)
        Me.pbLoadSettings.TabIndex = 2
        Me.pbLoadSettings.TabStop = false
        '
        'lblLoadSettings
        '
        Me.lblLoadSettings.Font = New System.Drawing.Font("Segoe UI", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
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
        Me.pnlLoadSettings.Location = New System.Drawing.Point(615, 287)
        Me.pnlLoadSettings.Name = "pnlLoadSettings"
        Me.pnlLoadSettings.Size = New System.Drawing.Size(257, 119)
        Me.pnlLoadSettings.TabIndex = 13
        Me.pnlLoadSettings.Visible = false
        '
        'tmrAppExit
        '
        Me.tmrAppExit.Interval = 1000
        '
        'tmrKeyBuffer
        '
        Me.tmrKeyBuffer.Interval = 2000
        '
        'tmrLoadMovieSet
        '
        '
        'tmrWaitMovieSet
        '
        Me.tmrWaitMovieSet.Interval = 250
        '
        'tmrSearchWait_MovieSets
        '
        Me.tmrSearchWait_MovieSets.Interval = 250
        '
        'tmrSearch_MovieSets
        '
        Me.tmrSearch_MovieSets.Interval = 250
        '
        'tmrSearchWait_Shows
        '
        Me.tmrSearchWait_Shows.Interval = 250
        '
        'tmrSearch_Shows
        '
        Me.tmrSearch_Shows.Interval = 250
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96!, 96!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1354, 733)
        Me.Controls.Add(Me.pnlLoadSettings)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.mnuMain)
        Me.DoubleBuffered = true
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnuMain
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "frmMain"
        Me.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Text = "Ember Media Manager"
        Me.StatusStrip.ResumeLayout(false)
        Me.StatusStrip.PerformLayout
        Me.mnuMain.ResumeLayout(false)
        Me.mnuMain.PerformLayout
        Me.scMain.Panel1.ResumeLayout(false)
        Me.scMain.Panel1.PerformLayout
        Me.scMain.Panel2.ResumeLayout(false)
        Me.scMain.Panel2.PerformLayout
        CType(Me.scMain,System.ComponentModel.ISupportInitialize).EndInit
        Me.scMain.ResumeLayout(false)
        Me.pnlFilterCountries_Movies.ResumeLayout(false)
        Me.pnlFilterCountries_Movies.PerformLayout
        Me.pnlFilterCountriesMain_Movies.ResumeLayout(false)
        Me.pnlFilterCountriesTop_Movies.ResumeLayout(false)
        Me.pnlFilterCountriesTop_Movies.PerformLayout
        Me.tblFilterCountriesTop_Movies.ResumeLayout(false)
        Me.tblFilterCountriesTop_Movies.PerformLayout
        Me.pnlFilterGenres_Movies.ResumeLayout(false)
        Me.pnlFilterGenres_Movies.PerformLayout
        Me.pnlFilterGenresMain_Movies.ResumeLayout(false)
        Me.pnlFilterGenresTop_Movies.ResumeLayout(false)
        Me.pnlFilterGenresTop_Movies.PerformLayout
        Me.tblFilterGenresTop_Movies.ResumeLayout(false)
        Me.tblFilterGenresTop_Movies.PerformLayout
        Me.pnlFilterGenres_Shows.ResumeLayout(false)
        Me.pnlFilterGenres_Shows.PerformLayout
        Me.pnlFilterGenresMain_Shows.ResumeLayout(false)
        Me.pnlFilterGenresTop_Shows.ResumeLayout(false)
        Me.pnlFilterGenresTop_Shows.PerformLayout
        Me.tblFilterGenresTop_Shows.ResumeLayout(false)
        Me.tblFilterGenresTop_Shows.PerformLayout
        Me.pnlFilterDataFields_Movies.ResumeLayout(false)
        Me.pnlFilterDataFields_Movies.PerformLayout
        Me.pnlFilterDataFieldsMain_Movies.ResumeLayout(false)
        Me.pnlFilterDataFieldsTop_Movies.ResumeLayout(false)
        Me.pnlFilterDataFieldsTop_Movies.PerformLayout
        Me.tblFilterDataFieldsTop_Movies.ResumeLayout(false)
        Me.tblFilterDataFieldsTop_Movies.PerformLayout
        Me.pnlFilterMissingItems_Movies.ResumeLayout(false)
        Me.pnlFilterMissingItems_Movies.PerformLayout
        Me.pnlFilterMissingItemsMain_Movies.ResumeLayout(false)
        Me.pnlFilterMissingItemsMain_Movies.PerformLayout
        Me.tblFilterMissingItemsMain_Movies.ResumeLayout(false)
        Me.tblFilterMissingItemsMain_Movies.PerformLayout
        Me.pnlFilterMissingItemsTop_Movies.ResumeLayout(false)
        Me.pnlFilterMissingItemsTop_Movies.PerformLayout
        Me.tblFilterMissingItemsTop_Movies.ResumeLayout(false)
        Me.tblFilterMissingItemsTop_Movies.PerformLayout
        Me.pnlFilterMissingItems_MovieSets.ResumeLayout(false)
        Me.pnlFilterMissingItems_MovieSets.PerformLayout
        Me.pnlFilterMissingItemsMain_MovieSets.ResumeLayout(false)
        Me.pnlFilterMissingItemsMain_MovieSets.PerformLayout
        Me.tlbFilterMissingItemsMain_MovieSets.ResumeLayout(false)
        Me.tlbFilterMissingItemsMain_MovieSets.PerformLayout
        Me.pnlFilterMissingItemsTop_MovieSets.ResumeLayout(false)
        Me.pnlFilterMissingItemsTop_MovieSets.PerformLayout
        Me.tblFilterMissingItemsTop_MovieSets.ResumeLayout(false)
        Me.tblFilterMissingItemsTop_MovieSets.PerformLayout
        Me.pnlFilterMissingItems_Shows.ResumeLayout(false)
        Me.pnlFilterMissingItems_Shows.PerformLayout
        Me.pnlFilterMissingItemsMain_Shows.ResumeLayout(false)
        Me.pnlFilterMissingItemsMain_Shows.PerformLayout
        Me.tblFilterMissingItemsMain_Shows.ResumeLayout(false)
        Me.tblFilterMissingItemsMain_Shows.PerformLayout
        Me.pnlFilterMissingItemsTop_Shows.ResumeLayout(false)
        Me.pnlFilterMissingItemsTop_Shows.PerformLayout
        Me.tblFilterMissingItemsTop_Shows.ResumeLayout(false)
        Me.tblFilterMissingItemsTop_Shows.PerformLayout
        Me.pnlFilterSources_Movies.ResumeLayout(false)
        Me.pnlFilterSources_Movies.PerformLayout
        Me.pnlFilterSourcesMain_Movies.ResumeLayout(false)
        Me.pnlFilterSourcesTop_Movies.ResumeLayout(false)
        Me.pnlFilterSourcesTop_Movies.PerformLayout
        Me.tblFilterSourcesTop_Movies.ResumeLayout(false)
        Me.tblFilterSourcesTop_Movies.PerformLayout
        Me.pnlFilterSources_Shows.ResumeLayout(false)
        Me.pnlFilterSources_Shows.PerformLayout
        Me.pnlFilterSourcesMain_Shows.ResumeLayout(false)
        Me.pnlFilterSourcesTop_Shows.ResumeLayout(false)
        Me.pnlFilterSourcesTop_Shows.PerformLayout
        Me.tblFilterSourcesTop_Shows.ResumeLayout(false)
        Me.tblFilterSourcesTop_Shows.PerformLayout
        CType(Me.dgvMovies,System.ComponentModel.ISupportInitialize).EndInit
        Me.cmnuMovie.ResumeLayout(false)
        CType(Me.dgvMovieSets,System.ComponentModel.ISupportInitialize).EndInit
        Me.cmnuMovieSet.ResumeLayout(false)
        Me.scTV.Panel1.ResumeLayout(false)
        Me.scTV.Panel2.ResumeLayout(false)
        CType(Me.scTV,System.ComponentModel.ISupportInitialize).EndInit
        Me.scTV.ResumeLayout(false)
        CType(Me.dgvTVShows,System.ComponentModel.ISupportInitialize).EndInit
        Me.cmnuShow.ResumeLayout(false)
        Me.scTVSeasonsEpisodes.Panel1.ResumeLayout(false)
        Me.scTVSeasonsEpisodes.Panel2.ResumeLayout(false)
        CType(Me.scTVSeasonsEpisodes,System.ComponentModel.ISupportInitialize).EndInit
        Me.scTVSeasonsEpisodes.ResumeLayout(false)
        CType(Me.dgvTVSeasons,System.ComponentModel.ISupportInitialize).EndInit
        Me.cmnuSeason.ResumeLayout(false)
        CType(Me.dgvTVEpisodes,System.ComponentModel.ISupportInitialize).EndInit
        Me.cmnuEpisode.ResumeLayout(false)
        Me.pnlListTop.ResumeLayout(false)
        Me.pnlSearchMovies.ResumeLayout(false)
        Me.pnlSearchMovies.PerformLayout
        CType(Me.picSearchMovies,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlSearchMovieSets.ResumeLayout(false)
        Me.pnlSearchMovieSets.PerformLayout
        CType(Me.picSearchMovieSets,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlSearchTVShows.ResumeLayout(false)
        Me.pnlSearchTVShows.PerformLayout
        CType(Me.picSearchTVShows,System.ComponentModel.ISupportInitialize).EndInit
        Me.tcMain.ResumeLayout(false)
        Me.pnlFilter_Movies.ResumeLayout(false)
        Me.pnlFilter_Movies.PerformLayout
        Me.tblFilter_Movies.ResumeLayout(false)
        Me.tblFilter_Movies.PerformLayout
        Me.gbFilterGeneral_Movies.ResumeLayout(false)
        Me.gbFilterGeneral_Movies.PerformLayout
        Me.tblFilterGeneral_Movies.ResumeLayout(false)
        Me.tblFilterGeneral_Movies.PerformLayout
        Me.gbFilterSorting_Movies.ResumeLayout(false)
        Me.gbFilterSorting_Movies.PerformLayout
        Me.tblFilterSorting_Movies.ResumeLayout(false)
        Me.tblFilterSorting_Movies.PerformLayout
        Me.gbFilterSpecific_Movies.ResumeLayout(false)
        Me.gbFilterSpecific_Movies.PerformLayout
        Me.tblFilterSpecific_Movies.ResumeLayout(false)
        Me.tblFilterSpecific_Movies.PerformLayout
        Me.gbFilterModifier_Movies.ResumeLayout(false)
        Me.gbFilterModifier_Movies.PerformLayout
        Me.tblFilterModifier_Movies.ResumeLayout(false)
        Me.tblFilterModifier_Movies.PerformLayout
        Me.tblFilterSpecificData_Movies.ResumeLayout(false)
        Me.tblFilterSpecificData_Movies.PerformLayout
        Me.gbFilterDataField_Movies.ResumeLayout(false)
        Me.gbFilterDataField_Movies.PerformLayout
        Me.tblFilterDataField_Movies.ResumeLayout(false)
        Me.tblFilterDataField_Movies.PerformLayout
        Me.pnlFilterTop_Movies.ResumeLayout(false)
        Me.pnlFilterTop_Movies.PerformLayout
        Me.tblFilterTop_Movies.ResumeLayout(false)
        Me.tblFilterTop_Movies.PerformLayout
        Me.pnlFilter_MovieSets.ResumeLayout(false)
        Me.pnlFilter_MovieSets.PerformLayout
        Me.tblFilter_MovieSets.ResumeLayout(false)
        Me.tblFilter_MovieSets.PerformLayout
        Me.gbFilterGeneral_MovieSets.ResumeLayout(false)
        Me.gbFilterGeneral_MovieSets.PerformLayout
        Me.tblFilterGeneral_MovieSets.ResumeLayout(false)
        Me.tblFilterGeneral_MovieSets.PerformLayout
        Me.gbFilterSpecific_MovieSets.ResumeLayout(false)
        Me.gbFilterSpecific_MovieSets.PerformLayout
        Me.tblFilterSpecific_MovieSets.ResumeLayout(false)
        Me.tblFilterSpecific_MovieSets.PerformLayout
        Me.gbFilterModifier_MovieSets.ResumeLayout(false)
        Me.gbFilterModifier_MovieSets.PerformLayout
        Me.tblFilterModifier_MovieSets.ResumeLayout(false)
        Me.tblFilterModifier_MovieSets.PerformLayout
        Me.pnlFilterTop_MovieSets.ResumeLayout(false)
        Me.pnlFilterTop_MovieSets.PerformLayout
        Me.tblFilterTop_MovieSets.ResumeLayout(false)
        Me.tblFilterTop_MovieSets.PerformLayout
        Me.pnlFilter_Shows.ResumeLayout(false)
        Me.pnlFilter_Shows.PerformLayout
        Me.tblFilter_Shows.ResumeLayout(false)
        Me.tblFilter_Shows.PerformLayout
        Me.gbFilterGeneral_Shows.ResumeLayout(false)
        Me.gbFilterGeneral_Shows.PerformLayout
        Me.tblFilterGeneral_Shows.ResumeLayout(false)
        Me.tblFilterGeneral_Shows.PerformLayout
        Me.gbFilterSpecific_Shows.ResumeLayout(false)
        Me.gbFilterSpecific_Shows.PerformLayout
        Me.tblFilterSpecific_Shows.ResumeLayout(false)
        Me.tblFilterSpecific_Shows.PerformLayout
        Me.gbFilterModifier_Shows.ResumeLayout(false)
        Me.gbFilterModifier_Shows.PerformLayout
        Me.tblFilterModifier_Shows.ResumeLayout(false)
        Me.tblFilterModifier_Shows.PerformLayout
        Me.tblFilterSpecificData_Shows.ResumeLayout(false)
        Me.tblFilterSpecificData_Shows.PerformLayout
        Me.pnlFilterTop_Shows.ResumeLayout(false)
        Me.pnlFilterTop_Shows.PerformLayout
        Me.tblFilterTop_Shows.ResumeLayout(false)
        Me.tblFilterTop_Shows.PerformLayout
        Me.pnlTop.ResumeLayout(false)
        Me.pnlTop.PerformLayout
        Me.tlpHeader.ResumeLayout(false)
        Me.tlpHeader.PerformLayout
        Me.pnlRating.ResumeLayout(false)
        CType(Me.pbStar10,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbStar9,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbStar8,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbStar7,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbStar6,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbStar5,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbStar4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbStar3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbStar2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbStar1,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlInfoIcons.ResumeLayout(false)
        CType(Me.pbSubtitleLang6,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbSubtitleLang5,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbSubtitleLang4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbSubtitleLang3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbSubtitleLang2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbSubtitleLang1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbSubtitleLang0,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbAudioLang6,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbAudioLang5,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbAudioLang4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbAudioLang3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbAudioLang2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbAudioLang1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbAudioLang0,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbVType,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbStudio,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbVideo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbAudio,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbResolution,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbChannels,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlCancel.ResumeLayout(false)
        Me.pnlCancel.PerformLayout
        Me.pnlNoInfo.ResumeLayout(false)
        Me.pnlNoInfoBG.ResumeLayout(false)
        CType(Me.pbNoInfo,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlInfoPanel.ResumeLayout(false)
        Me.pnlInfoPanel.PerformLayout
        Me.pnlMoviesInSet.ResumeLayout(false)
        CType(Me.pbMILoading,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlActors.ResumeLayout(false)
        CType(Me.pbActLoad,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbActors,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlTop250.ResumeLayout(false)
        CType(Me.pbTop250,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlPoster.ResumeLayout(false)
        CType(Me.pbPoster,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbPosterCache,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbFanartSmallCache,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlFanartSmall.ResumeLayout(false)
        CType(Me.pbFanartSmall,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlLandscape.ResumeLayout(false)
        CType(Me.pbLandscape,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbLandscapeCache,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlClearArt.ResumeLayout(false)
        CType(Me.pbClearArt,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbClearArtCache,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlMPAA.ResumeLayout(false)
        Me.pnlMPAA.PerformLayout
        CType(Me.pbMPAA,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbFanartCache,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.pbFanart,System.ComponentModel.ISupportInitialize).EndInit
        Me.tsMain.ResumeLayout(false)
        Me.tsMain.PerformLayout
        Me.cmnuTray.ResumeLayout(false)
        Me.pnlLoadSettingsBG.ResumeLayout(false)
        CType(Me.pbLoadSettings,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlLoadSettings.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

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
    Friend WithEvents mnuScrapeMovies As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents btnMid As System.Windows.Forms.Button
    Friend WithEvents pbPosterCache As System.Windows.Forms.PictureBox
    Friend WithEvents txtCerts As System.Windows.Forms.TextBox
    Friend WithEvents lblCertsHeader As System.Windows.Forms.Label
    Friend WithEvents lblReleaseDate As System.Windows.Forms.Label
    Friend WithEvents lblReleaseDateHeader As System.Windows.Forms.Label
    Friend WithEvents ilColumnIcons As System.Windows.Forms.ImageList
    Friend WithEvents mnuMovieAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMiss As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrWaitMovie As System.Windows.Forms.Timer
    Friend WithEvents tmrLoadMovie As System.Windows.Forms.Timer
    Friend WithEvents pnlSearchMovies As System.Windows.Forms.Panel
    Friend WithEvents picSearchMovies As System.Windows.Forms.PictureBox
    Friend WithEvents txtSearchMovies As System.Windows.Forms.TextBox
    Friend WithEvents tmrSearchWait_Movies As System.Windows.Forms.Timer
    Friend WithEvents tmrSearch_Movies As System.Windows.Forms.Timer
    Friend WithEvents lblNoInfo As System.Windows.Forms.Label
    Friend WithEvents pbNoInfo As System.Windows.Forms.PictureBox
    Friend WithEvents mnuMainTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsCleanFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsSortFiles As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlFilter_Movies As System.Windows.Forms.Panel
    Friend WithEvents lblFilter_Movies As System.Windows.Forms.Label
    Friend WithEvents chkFilterNew_Movies As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterMark_Movies As System.Windows.Forms.CheckBox
    Friend WithEvents rbFilterOr_Movies As System.Windows.Forms.RadioButton
    Friend WithEvents rbFilterAnd_Movies As System.Windows.Forms.RadioButton
    Friend WithEvents chkFilterDuplicates_Movies As System.Windows.Forms.CheckBox
    Friend WithEvents tsbMediaCenters As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents cmnuMovie As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmnuMovieMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieChange As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieSep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents prbCanceling As System.Windows.Forms.ProgressBar
    Friend WithEvents lblCanceling As System.Windows.Forms.Label
    Private WithEvents pnlNoInfo As System.Windows.Forms.Panel
    Friend WithEvents pnlCancel As System.Windows.Forms.Panel
    Friend WithEvents cmnuMovieSep4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieOpenFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieRemoveFromDisc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieLock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsBackdrops As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnMarkAll As System.Windows.Forms.Button
    Friend WithEvents mnuMainToolsSeparator0 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMainToolsSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMainToolsClearCache As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnFilterDown_Movies As System.Windows.Forms.Button
    Friend WithEvents btnFilterUp_Movies As System.Windows.Forms.Button
    Friend WithEvents lblFilterSource_Movies As System.Windows.Forms.Label
    Friend WithEvents mnuMovieAllAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieCustom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAutoMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieReload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsReloadMovies As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieGenres As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieGenresAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieGenresSet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieGenresRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieGenresGenre As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents cmnuMovieGenresTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUpdate As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents lblFilterGenre_Movies As System.Windows.Forms.Label
    Friend WithEvents clbFilterGenres_Movies As System.Windows.Forms.CheckedListBox
    Friend WithEvents txtFilterGenre_Movies As System.Windows.Forms.TextBox
    Friend WithEvents pnlFilterGenres_Movies As System.Windows.Forms.Panel
    Friend WithEvents lblFilterGenresClose_Movies As System.Windows.Forms.Label
    Friend WithEvents lblFilterGenres_Movies As System.Windows.Forms.Label
    Friend WithEvents cbSearchMovies As System.Windows.Forms.ComboBox
    Friend WithEvents cbFilterYearModFrom_Movies As System.Windows.Forms.ComboBox
    Friend WithEvents lblFilterYear_Movies As System.Windows.Forms.Label
    Friend WithEvents cbFilterYearFrom_Movies As System.Windows.Forms.ComboBox
    Friend WithEvents gbFilterSpecific_Movies As System.Windows.Forms.GroupBox
    Friend WithEvents gbFilterModifier_Movies As System.Windows.Forms.GroupBox
    Friend WithEvents gbFilterGeneral_Movies As System.Windows.Forms.GroupBox
    Friend WithEvents btnClearFilters_Movies As System.Windows.Forms.Button
    Friend WithEvents chkFilterLock_Movies As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterMissing_Movies As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterTolerance_Movies As System.Windows.Forms.CheckBox
    Friend WithEvents lblFilterVideoSource_Movies As System.Windows.Forms.Label
    Friend WithEvents cbFilterVideoSource_Movies As System.Windows.Forms.ComboBox
    Friend WithEvents cmnuMovieEditMetaData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gbFilterSorting_Movies As System.Windows.Forms.GroupBox
    Friend WithEvents btnFilterSortDateAdded_Movies As System.Windows.Forms.Button
    Friend WithEvents pnlFilterSources_Movies As System.Windows.Forms.Panel
    Friend WithEvents lblFilterSourcesClose_Movies As System.Windows.Forms.Label
    Friend WithEvents lblFilterSources_Movies As System.Windows.Forms.Label
    Friend WithEvents clbFilterSources_Movies As System.Windows.Forms.CheckedListBox
    Friend WithEvents txtFilterSource_Movies As System.Windows.Forms.TextBox
    Friend WithEvents mnuMovieFilter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnFilterSortTitle_Movies As System.Windows.Forms.Button
    Friend WithEvents ToolTips As System.Windows.Forms.ToolTip
    Friend WithEvents btnFilterSortRating_Movies As System.Windows.Forms.Button
    Friend WithEvents tpTVShows As System.Windows.Forms.TabPage
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
    Friend WithEvents cmnuMovieSep5 As System.Windows.Forms.ToolStripSeparator
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
    Friend WithEvents cmnuMovieMovieReSelAskFanart As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents cmnuSeasonRemoveFromDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonRemoveFromDisk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeChange As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator17 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMainHelpWiki As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainHelpSeparator0 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMainHelpSeparator1 As System.Windows.Forms.ToolStripSeparator
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
    Friend WithEvents cmnuTrayToolsClearCache As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayToolsReloadMovies As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayToolsCleanDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator24 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator25 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator26 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuTrayMovieAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMiss As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieCustom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoMetaData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoMI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskMI As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents lblRating As System.Windows.Forms.Label
    Friend WithEvents lblOriginalTitle As System.Windows.Forms.Label
    Friend WithEvents pnlFanartSmall As System.Windows.Forms.Panel
    Friend WithEvents pbFanartSmall As System.Windows.Forms.PictureBox
    Friend WithEvents pbFanartSmallCache As System.Windows.Forms.PictureBox
    Friend WithEvents mnuMovieAllAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieWatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowWatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonWatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeWatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskActor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpMovies As System.Windows.Forms.TabPage
    Friend WithEvents mnuMainToolsOfflineHolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayToolsOfflineHolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVersion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowRefresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieAllAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMissAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieNewAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieMarkAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieFilterAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieReSelAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieAllAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMissAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieNewAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieMarkAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAutoTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieFilterAskTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelRating As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpMovieSets As System.Windows.Forms.TabPage
    Friend WithEvents dgvMovieSets As System.Windows.Forms.DataGridView
    Friend WithEvents dgvMovies As System.Windows.Forms.DataGridView
    Friend WithEvents cmnuMovieSet As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tmrLoadMovieSet As System.Windows.Forms.Timer
    Friend WithEvents tmrWaitMovieSet As System.Windows.Forms.Timer
    Friend WithEvents cmnuMovieSetReload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieSetNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlLandscape As System.Windows.Forms.Panel
    Friend WithEvents pbLandscape As System.Windows.Forms.PictureBox
    Friend WithEvents pbFanart As System.Windows.Forms.PictureBox
    Friend WithEvents pbLandscapeCache As System.Windows.Forms.PictureBox
    Friend WithEvents pnlClearArt As System.Windows.Forms.Panel
    Friend WithEvents pbClearArt As System.Windows.Forms.PictureBox
    Friend WithEvents pbClearArtCache As System.Windows.Forms.PictureBox
    Friend WithEvents lblMoviesInSetHeader As System.Windows.Forms.Label
    Friend WithEvents pnlMoviesInSet As System.Windows.Forms.Panel
    Friend WithEvents mnuMainToolsExport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsExportMovies As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsExportTvShows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieRestart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayMovieRestart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieMarkAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieMarkAsCustom1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieMarkAsCustom2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieMarkAsCustom3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieMarkAsCustom4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelCert As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelCountry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelDirector As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelMPAA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelOutline As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelPlot As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelProducers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelRelease As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelStudio As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelTagline As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelTop250 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelWriter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieUpSelYear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetSep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieSetRescrape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowLanguage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowLanguageSet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowLanguageLanguages As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents pbStar10 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar9 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar8 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar7 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar6 As System.Windows.Forms.PictureBox
    Friend WithEvents mnuMainToolsReloadMovieSets As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeMovieSets As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents mnuMovieSetAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetAllSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMiss As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMissSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetNewSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetMarkSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAutoAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAutoBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAutoClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAutoClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAutoDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAutoEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAutoEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAutoFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAutoLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAutoNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAutoPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAskAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAskBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAskClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAskClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAskDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAskEFanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAskEThumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAskFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAskLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAskNfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterAskPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetFilterSkipAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetCustom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMovieSetRestart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetLock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetSep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chkFilterMarkCustom4_Movies As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterMarkCustom3_Movies As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterMarkCustom2_Movies As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterMarkCustom1_Movies As System.Windows.Forms.CheckBox
    Friend WithEvents txtFilterCountry_Movies As System.Windows.Forms.TextBox
    Friend WithEvents lblFilterCountry_Movies As System.Windows.Forms.Label
    Friend WithEvents pnlFilterCountries_Movies As System.Windows.Forms.Panel
    Friend WithEvents clbFilterCountries_Movies As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblFilterCountriesClose_Movies As System.Windows.Forms.Label
    Friend WithEvents lblFilterCountries_Movies As System.Windows.Forms.Label
    Friend WithEvents pnlSearchMovieSets As System.Windows.Forms.Panel
    Friend WithEvents cbSearchMovieSets As System.Windows.Forms.ComboBox
    Friend WithEvents picSearchMovieSets As System.Windows.Forms.PictureBox
    Friend WithEvents txtSearchMovieSets As System.Windows.Forms.TextBox
    Friend WithEvents tmrSearchWait_MovieSets As System.Windows.Forms.Timer
    Friend WithEvents tmrSearch_MovieSets As System.Windows.Forms.Timer
    Friend WithEvents pnlSearchTVShows As System.Windows.Forms.Panel
    Friend WithEvents cbSearchShows As System.Windows.Forms.ComboBox
    Friend WithEvents picSearchTVShows As System.Windows.Forms.PictureBox
    Friend WithEvents txtSearchShows As System.Windows.Forms.TextBox
    Friend WithEvents tmrSearchWait_Shows As System.Windows.Forms.Timer
    Friend WithEvents tmrSearch_Shows As System.Windows.Forms.Timer
    Friend WithEvents pnlFilter_MovieSets As System.Windows.Forms.Panel
    Friend WithEvents btnClearFilters_MovieSets As System.Windows.Forms.Button
    Friend WithEvents gbFilterGeneral_MovieSets As System.Windows.Forms.GroupBox
    Friend WithEvents chkFilterMissing_MovieSets As System.Windows.Forms.CheckBox
    Friend WithEvents gbFilterSpecific_MovieSets As System.Windows.Forms.GroupBox
    Friend WithEvents chkFilterLock_MovieSets As System.Windows.Forms.CheckBox
    Friend WithEvents gbFilterModifier_MovieSets As System.Windows.Forms.GroupBox
    Friend WithEvents rbFilterAnd_MovieSets As System.Windows.Forms.RadioButton
    Friend WithEvents rbFilterOr_MovieSets As System.Windows.Forms.RadioButton
    Friend WithEvents chkFilterNew_MovieSets As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterMark_MovieSets As System.Windows.Forms.CheckBox
    Friend WithEvents btnFilterDown_MovieSets As System.Windows.Forms.Button
    Friend WithEvents btnFilterUp_MovieSets As System.Windows.Forms.Button
    Friend WithEvents lblFilter_MovieSets As System.Windows.Forms.Label
    Friend WithEvents pnlFilter_Shows As System.Windows.Forms.Panel
    Friend WithEvents btnClearFilters_Shows As System.Windows.Forms.Button
    Friend WithEvents gbFilterGeneral_Shows As System.Windows.Forms.GroupBox
    Friend WithEvents chkFilterMissing_Shows As System.Windows.Forms.CheckBox
    Friend WithEvents gbFilterSpecific_Shows As System.Windows.Forms.GroupBox
    Friend WithEvents chkFilterLock_Shows As System.Windows.Forms.CheckBox
    Friend WithEvents gbFilterModifier_Shows As System.Windows.Forms.GroupBox
    Friend WithEvents rbFilterAnd_Shows As System.Windows.Forms.RadioButton
    Friend WithEvents rbFilterOr_Shows As System.Windows.Forms.RadioButton
    Friend WithEvents chkFilterNew_Shows As System.Windows.Forms.CheckBox
    Friend WithEvents chkFilterMark_Shows As System.Windows.Forms.CheckBox
    Friend WithEvents txtFilterGenre_Shows As System.Windows.Forms.TextBox
    Friend WithEvents lblFilterGenre_Shows As System.Windows.Forms.Label
    Friend WithEvents btnFilterDown_Shows As System.Windows.Forms.Button
    Friend WithEvents btnFilterUp_Shows As System.Windows.Forms.Button
    Friend WithEvents lblFilter_Shows As System.Windows.Forms.Label
    Friend WithEvents txtFilterSource_Shows As System.Windows.Forms.TextBox
    Friend WithEvents lblFilterSource_Shows As System.Windows.Forms.Label
    Friend WithEvents pnlFilterSources_Shows As System.Windows.Forms.Panel
    Friend WithEvents lblFilterSourcesClose_Shows As System.Windows.Forms.Label
    Friend WithEvents lblFilterSources_Shows As System.Windows.Forms.Label
    Friend WithEvents clbFilterSource_Shows As System.Windows.Forms.CheckedListBox
    Friend WithEvents pnlFilterGenres_Shows As System.Windows.Forms.Panel
    Friend WithEvents clbFilterGenres_Shows As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblFilterGenresClose_Shows As System.Windows.Forms.Label
    Friend WithEvents lblFilterGenres_Shows As System.Windows.Forms.Label
    Friend WithEvents chkFilterEmpty_MovieSets As System.Windows.Forms.CheckBox
    Friend WithEvents cmnuMovieBrowseIMDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieBrowseTMDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkFilterOne_MovieSets As System.Windows.Forms.CheckBox
    Friend WithEvents txtFilterDataField_Movies As System.Windows.Forms.TextBox
    Friend WithEvents pnlFilterDataFields_Movies As System.Windows.Forms.Panel
    Friend WithEvents clbFilterDataFields_Movies As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblFilterDataFieldsClose_Movies As System.Windows.Forms.Label
    Friend WithEvents lblFilterDataFields_Movies As System.Windows.Forms.Label
    Friend WithEvents gbFilterDataField_Movies As System.Windows.Forms.GroupBox
    Friend WithEvents cbFilterDataField_Movies As System.Windows.Forms.ComboBox
    Friend WithEvents lvMoviesInSet As System.Windows.Forms.ListView
    Friend WithEvents ilMoviesInSet As System.Windows.Forms.ImageList
    Friend WithEvents cmnuMovieChangeAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pbAudioLang0 As System.Windows.Forms.PictureBox
    Friend WithEvents pbAudioLang6 As System.Windows.Forms.PictureBox
    Friend WithEvents pbAudioLang5 As System.Windows.Forms.PictureBox
    Friend WithEvents pbAudioLang4 As System.Windows.Forms.PictureBox
    Friend WithEvents pbAudioLang3 As System.Windows.Forms.PictureBox
    Friend WithEvents pbAudioLang2 As System.Windows.Forms.PictureBox
    Friend WithEvents pbAudioLang1 As System.Windows.Forms.PictureBox
    Friend WithEvents tlpHeader As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pbSubtitleLang6 As System.Windows.Forms.PictureBox
    Friend WithEvents pbSubtitleLang5 As System.Windows.Forms.PictureBox
    Friend WithEvents pbSubtitleLang4 As System.Windows.Forms.PictureBox
    Friend WithEvents pbSubtitleLang3 As System.Windows.Forms.PictureBox
    Friend WithEvents pbSubtitleLang2 As System.Windows.Forms.PictureBox
    Friend WithEvents pbSubtitleLang1 As System.Windows.Forms.PictureBox
    Friend WithEvents pbSubtitleLang0 As System.Windows.Forms.PictureBox
    Friend WithEvents mnuMainToolsRewriteMovieContent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tblFilter_MovieSets As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilterGeneral_MovieSets As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilterSpecific_MovieSets As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilterModifier_MovieSets As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlFilterTop_MovieSets As System.Windows.Forms.Panel
    Friend WithEvents tblFilterTop_MovieSets As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilter_Shows As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilterGeneral_Shows As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilterSpecific_Shows As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilterModifier_Shows As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlFilterTop_Shows As System.Windows.Forms.Panel
    Friend WithEvents tblFilterTop_Shows As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlFilterTop_Movies As System.Windows.Forms.Panel
    Friend WithEvents tblFilterTop_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilterGeneral_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilterSorting_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilterModifier_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilterSpecific_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilter_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilterDataField_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilterSpecificData_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilterSpecificData_Shows As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlFilterGenresMain_Movies As System.Windows.Forms.Panel
    Friend WithEvents pnlFilterGenresTop_Movies As System.Windows.Forms.Panel
    Friend WithEvents tblFilterGenresTop_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlFilterCountriesTop_Movies As System.Windows.Forms.Panel
    Friend WithEvents tblFilterCountriesTop_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlFilterCountriesMain_Movies As System.Windows.Forms.Panel
    Friend WithEvents pnlFilterSourcesMain_Movies As System.Windows.Forms.Panel
    Friend WithEvents pnlFilterSourcesTop_Movies As System.Windows.Forms.Panel
    Friend WithEvents tblFilterSourcesTop_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlFilterDataFieldsMain_Movies As System.Windows.Forms.Panel
    Friend WithEvents pnlFilterDataFieldsTop_Movies As System.Windows.Forms.Panel
    Friend WithEvents tblFilterDataFieldsTop_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlFilterGenresMain_Shows As System.Windows.Forms.Panel
    Friend WithEvents pnlFilterGenresTop_Shows As System.Windows.Forms.Panel
    Friend WithEvents tblFilterGenresTop_Shows As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlFilterSourcesTop_Shows As System.Windows.Forms.Panel
    Friend WithEvents tblFilterSourcesTop_Shows As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlFilterSourcesMain_Shows As System.Windows.Forms.Panel
    Friend WithEvents mnuMainHelpForumEng As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainHelpForumGer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cbFilterYearModTo_Movies As System.Windows.Forms.ComboBox
    Friend WithEvents cbFilterYearTo_Movies As System.Windows.Forms.ComboBox
    Friend WithEvents btnFilterSortYear_Movies As System.Windows.Forms.Button
    Friend WithEvents btnFilterSortDateModified_Movies As System.Windows.Forms.Button
    Friend WithEvents btnFilterMissing_Movies As System.Windows.Forms.Button
    Friend WithEvents btnFilterMissing_MovieSets As System.Windows.Forms.Button
    Friend WithEvents btnFilterMissing_Shows As System.Windows.Forms.Button
    Friend WithEvents pnlFilterMissingItems_Movies As System.Windows.Forms.Panel
    Friend WithEvents pnlFilterMissingItemsMain_Movies As System.Windows.Forms.Panel
    Friend WithEvents tblFilterMissingItemsMain_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkMovieMissingBanner As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingDiscArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingEFanarts As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingEThumbs As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingTheme As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingSubtitles As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingPoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingNFO As System.Windows.Forms.CheckBox
    Friend WithEvents pnlFilterMissingItemsTop_Movies As System.Windows.Forms.Panel
    Friend WithEvents tblFilterMissingItemsTop_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblFilterMissingItems_Movies As System.Windows.Forms.Label
    Friend WithEvents lblFilterMissingItemsClose_Movies As System.Windows.Forms.Label
    Friend WithEvents pnlFilterMissingItems_MovieSets As System.Windows.Forms.Panel
    Friend WithEvents pnlFilterMissingItemsMain_MovieSets As System.Windows.Forms.Panel
    Friend WithEvents tlbFilterMissingItemsMain_MovieSets As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkMovieSetMissingBanner As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetMissingClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetMissingClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetMissingDiscArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetMissingFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetMissingLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetMissingPoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetMissingNFO As System.Windows.Forms.CheckBox
    Friend WithEvents pnlFilterMissingItemsTop_MovieSets As System.Windows.Forms.Panel
    Friend WithEvents tblFilterMissingItemsTop_MovieSets As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblFilterMissingItems_MovieSets As System.Windows.Forms.Label
    Friend WithEvents lblFilterMissingItemsClose_MovieSets As System.Windows.Forms.Label
    Friend WithEvents pnlFilterMissingItems_Shows As System.Windows.Forms.Panel
    Friend WithEvents pnlFilterMissingItemsMain_Shows As System.Windows.Forms.Panel
    Friend WithEvents tblFilterMissingItemsMain_Shows As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkShowMissingBanner As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowMissingClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowMissingClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowMissingEFanarts As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowMissingFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowMissingLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowMissingPoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowMissingNFO As System.Windows.Forms.CheckBox
    Friend WithEvents pnlFilterMissingItemsTop_Shows As System.Windows.Forms.Panel
    Friend WithEvents tblFilterMissingItemsTop_Shows As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblFilterMissingItems_Shows As System.Windows.Forms.Label
    Friend WithEvents lblFilterMissingItemsClose_Shows As System.Windows.Forms.Label
    Friend WithEvents chkShowMissingCharacterArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowMissingTheme As System.Windows.Forms.CheckBox
End Class
