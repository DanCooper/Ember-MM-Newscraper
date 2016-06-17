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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        Me.mnuMainToolsCleanDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMainToolsReloadMovies = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsReloadMovieSets = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsReloadTVShows = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMainToolsRewriteMovieContent = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.chkMovieMissingExtrafanarts = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingExtrathumbs = New System.Windows.Forms.CheckBox()
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
        Me.chkShowMissingExtrafanarts = New System.Windows.Forms.CheckBox()
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
        Me.cmnuMovieDatabaseSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieReload = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieMarkAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieMarkAsCustom1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieMarkAsCustom2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieMarkAsCustom3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieMarkAsCustom4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieWatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieUnwatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieLock = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieEditSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieEditMetaData = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieEditGenres = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGenres = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuGenresTitleSelect = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGenresGenre = New System.Windows.Forms.ToolStripComboBox()
        Me.mnuGenresSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuGenresTitleNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGenresNew = New System.Windows.Forms.ToolStripTextBox()
        Me.mnuGenresSep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuGenresAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGenresSet = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGenresRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowEditGenres = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieEditTags = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTags = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuTagsTitleSelect = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTagsTag = New System.Windows.Forms.ToolStripComboBox()
        Me.mnuTagsSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuTagsTitleNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTagsNew = New System.Windows.Forms.ToolStripTextBox()
        Me.mnuTagsSep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuTagsAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTagsSet = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTagsRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowEditTags = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieRescrapeSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieScrape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieScrapeSelected = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeType = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuScrapeTypeAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifier = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuScrapeModifierAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierActorthumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierBanner = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierCharacterArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierClearArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierClearLogo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierDiscArt = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierExtrafanarts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierExtrathumbs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierFanart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierLandscape = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierMetaData = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierNFO = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierPoster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierTheme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeModifierTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeTypeSkip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeTypeAsk = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeSubmenuFilter = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieScrapeSingleDataField = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOption = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuScrapeOptionActors = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionAired = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionCertifications = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionCollectionID = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionCreators = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionCountries = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionDirectors = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionEpiGuideURL = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionGenres = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionGuestStars = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionMPAA = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionOriginalTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionPlot = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionOutline = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionPremiered = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionRating = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionReleaseDate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionRuntime = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionStatus = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionStudios = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionTagline = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionTop250 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionTrailer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionWriters = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeOptionYear = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeScrapeSingleDataField = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieChange = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieChangeAuto = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieLanguage = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLanguages = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuLanguagesTitleSelect = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLanguagesLanguage = New System.Windows.Forms.ToolStripComboBox()
        Me.mnuLanguagesSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuLanguagesSet = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowLanguage = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieBrowseIMDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieBrowseTMDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieRemoveSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieRemoveFromDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieRemoveFromDisk = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgvMovieSets = New System.Windows.Forms.DataGridView()
        Me.cmnuMovieSet = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuMovieSetTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetDatabaseSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieSetReload = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetLock = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetNewSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieSetNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetEditSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieSetEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetEditSortMethod = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetEditSortMethodMethods = New System.Windows.Forms.ToolStripComboBox()
        Me.cmnuMovieSetEditSortMethodSet = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetScrapeSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieSetScrape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetScrapeSelected = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetScrapeSingleDataField = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetLanguage = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetSep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieSetBrowseTMDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuMovieSetRemoveSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuMovieSetRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.scTV = New System.Windows.Forms.SplitContainer()
        Me.dgvTVShows = New System.Windows.Forms.DataGridView()
        Me.cmnuShow = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuShowTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowDatabaseSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuShowReload = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowReloadFull = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowWatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowUnwatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowLock = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowEditSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuShowEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowScrapeSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuShowScrape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowScrapeSelected = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowScrapeSingleDataField = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowScrapeRefreshData = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowChange = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowSep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuShowBrowseIMDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowBrowseTMDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowBrowseTVDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowSep4 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuShowClearCache = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowClearCacheDataAndImages = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowClearCacheSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuShowClearCacheDataOnly = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowClearCacheImagesOnly = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowRemoveSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuShowRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowRemoveFromDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuShowRemoveFromDisk = New System.Windows.Forms.ToolStripMenuItem()
        Me.scTVSeasonsEpisodes = New System.Windows.Forms.SplitContainer()
        Me.dgvTVSeasons = New System.Windows.Forms.DataGridView()
        Me.cmnuSeason = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuSeasonTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonDatabaseSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuSeasonReload = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonReloadFull = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonWatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonUnwatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonLock = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonEditSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuSeasonEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonScrapeSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuSeasonScrape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonScrapeSelected = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonScrapeSingleDataField = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonSep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuSeasonBrowseIMDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonBrowseTMDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonBrowseTVDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonRemoveSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuSeasonRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonRemoveFromDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSeasonRemoveFromDisk = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgvTVEpisodes = New System.Windows.Forms.DataGridView()
        Me.cmnuEpisode = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuEpisodeTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeDatabaseSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuEpisodeReload = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeWatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeUnwatched = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeLock = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeEditSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuEpisodeEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeScrapeSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuEpisodeScrape = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeScrapeSelected = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeChange = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeSep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuEpisodeBrowseIMDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeBrowseTMDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeBrowseTVDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeOpenFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeRemoveSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuEpisodeRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeRemoveFromDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuEpisodeRemoveFromDisk = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlListTop = New System.Windows.Forms.Panel()
        Me.tblListTop = New System.Windows.Forms.TableLayoutPanel()
        Me.tcMain = New System.Windows.Forms.TabControl()
        Me.tpMovies = New System.Windows.Forms.TabPage()
        Me.tpMovieSets = New System.Windows.Forms.TabPage()
        Me.tpTVShows = New System.Windows.Forms.TabPage()
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
        Me.gbFilterLists_Movies = New System.Windows.Forms.GroupBox()
        Me.tblFilterLists_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.cbFilterLists_Movies = New System.Windows.Forms.ComboBox()
        Me.pnlFilterTop_Movies = New System.Windows.Forms.Panel()
        Me.tblFilterTop_Movies = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFilter_Movies = New System.Windows.Forms.Label()
        Me.btnFilterUp_Movies = New System.Windows.Forms.Button()
        Me.btnFilterDown_Movies = New System.Windows.Forms.Button()
        Me.pnlFilter_MovieSets = New System.Windows.Forms.Panel()
        Me.tblFilter_MovieSets = New System.Windows.Forms.TableLayoutPanel()
        Me.gbFilterLists_MovieSets = New System.Windows.Forms.GroupBox()
        Me.tblFilterLists_MovieSets = New System.Windows.Forms.TableLayoutPanel()
        Me.cbFilterLists_MovieSets = New System.Windows.Forms.ComboBox()
        Me.gbFilterGeneral_MovieSets = New System.Windows.Forms.GroupBox()
        Me.tblFilterGeneral_MovieSets = New System.Windows.Forms.TableLayoutPanel()
        Me.btnFilterMissing_MovieSets = New System.Windows.Forms.Button()
        Me.chkFilterMissing_MovieSets = New System.Windows.Forms.CheckBox()
        Me.chkFilterOne_MovieSets = New System.Windows.Forms.CheckBox()
        Me.chkFilterEmpty_MovieSets = New System.Windows.Forms.CheckBox()
        Me.chkFilterMultiple_MovieSets = New System.Windows.Forms.CheckBox()
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
        Me.gbFilterSorting_Shows = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnFilterSortTitle_Shows = New System.Windows.Forms.Button()
        Me.gbFilterLists_Shows = New System.Windows.Forms.GroupBox()
        Me.tblFilterLists_Shows = New System.Windows.Forms.TableLayoutPanel()
        Me.cbFilterLists_Shows = New System.Windows.Forms.ComboBox()
        Me.gbFilterGeneral_Shows = New System.Windows.Forms.GroupBox()
        Me.tblFilterGeneral_Shows = New System.Windows.Forms.TableLayoutPanel()
        Me.btnFilterMissing_Shows = New System.Windows.Forms.Button()
        Me.chkFilterMissing_Shows = New System.Windows.Forms.CheckBox()
        Me.gbFilterSpecific_Shows = New System.Windows.Forms.GroupBox()
        Me.tblFilterSpecific_Shows = New System.Windows.Forms.TableLayoutPanel()
        Me.chkFilterNewEpisodes_Shows = New System.Windows.Forms.CheckBox()
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
        Me.chkFilterNewShows_Shows = New System.Windows.Forms.CheckBox()
        Me.btnClearFilters_Shows = New System.Windows.Forms.Button()
        Me.pnlFilterTop_Shows = New System.Windows.Forms.Panel()
        Me.tblFilterTop_Shows = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFilter_Shows = New System.Windows.Forms.Label()
        Me.btnFilterUp_Shows = New System.Windows.Forms.Button()
        Me.btnFilterDown_Shows = New System.Windows.Forms.Button()
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
        Me.txtCertifications = New System.Windows.Forms.TextBox()
        Me.lblCertificationsHeader = New System.Windows.Forms.Label()
        Me.lblReleaseDate = New System.Windows.Forms.Label()
        Me.lblReleaseDateHeader = New System.Windows.Forms.Label()
        Me.btnMid = New System.Windows.Forms.Button()
        Me.pbMILoading = New System.Windows.Forms.PictureBox()
        Me.btnMetaDataRefresh = New System.Windows.Forms.Button()
        Me.lblMetaDataHeader = New System.Windows.Forms.Label()
        Me.txtMetaData = New System.Windows.Forms.TextBox()
        Me.btnFilePlay = New System.Windows.Forms.Button()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.lblFilePathHeader = New System.Windows.Forms.Label()
        Me.txtTMDBID = New System.Windows.Forms.TextBox()
        Me.txtIMDBID = New System.Windows.Forms.TextBox()
        Me.lblTMDBHeader = New System.Windows.Forms.Label()
        Me.lblIMDBHeader = New System.Windows.Forms.Label()
        Me.lblDirectors = New System.Windows.Forms.Label()
        Me.lblDirectorsHeader = New System.Windows.Forms.Label()
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
        Me.pbBannerCache = New System.Windows.Forms.PictureBox()
        Me.pnlBanner = New System.Windows.Forms.Panel()
        Me.pnlBannerMain = New System.Windows.Forms.Panel()
        Me.tblBannerMain = New System.Windows.Forms.TableLayoutPanel()
        Me.pbBanner = New System.Windows.Forms.PictureBox()
        Me.pnlBannerBottom = New System.Windows.Forms.Panel()
        Me.tblBannerBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblBannerSize = New System.Windows.Forms.Label()
        Me.pnlBannerTop = New System.Windows.Forms.Panel()
        Me.tblBannerTop = New System.Windows.Forms.TableLayoutPanel()
        Me.lblBannerTitle = New System.Windows.Forms.Label()
        Me.pbCache = New System.Windows.Forms.PictureBox()
        Me.pnlClearLogo = New System.Windows.Forms.Panel()
        Me.pnlClearLogoMain = New System.Windows.Forms.Panel()
        Me.tblClearLogoMain = New System.Windows.Forms.TableLayoutPanel()
        Me.pbClearLogo = New System.Windows.Forms.PictureBox()
        Me.pnlClearLogoBottom = New System.Windows.Forms.Panel()
        Me.tblClearLogoBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblClearLogoSize = New System.Windows.Forms.Label()
        Me.pnlClearLogoTop = New System.Windows.Forms.Panel()
        Me.tblClearLogoTop = New System.Windows.Forms.TableLayoutPanel()
        Me.lblClearLogoTitle = New System.Windows.Forms.Label()
        Me.pnlCharacterArt = New System.Windows.Forms.Panel()
        Me.pnlCharacterArtMain = New System.Windows.Forms.Panel()
        Me.tblCharacterArtMain = New System.Windows.Forms.TableLayoutPanel()
        Me.pbCharacterArt = New System.Windows.Forms.PictureBox()
        Me.pnlCharacterArtBottom = New System.Windows.Forms.Panel()
        Me.tblCharacterArtBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblCharacterArtSize = New System.Windows.Forms.Label()
        Me.pnlCharacterArtTop = New System.Windows.Forms.Panel()
        Me.tblCharacterArtTop = New System.Windows.Forms.TableLayoutPanel()
        Me.lblCharacterArtTitle = New System.Windows.Forms.Label()
        Me.pbCharacterArtCache = New System.Windows.Forms.PictureBox()
        Me.pnlDiscArt = New System.Windows.Forms.Panel()
        Me.pnlDiscArtMain = New System.Windows.Forms.Panel()
        Me.tblDiscArtMain = New System.Windows.Forms.TableLayoutPanel()
        Me.pbDiscArt = New System.Windows.Forms.PictureBox()
        Me.pnlDiscArtBottom = New System.Windows.Forms.Panel()
        Me.tblDiscArtBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDiscArtSize = New System.Windows.Forms.Label()
        Me.pnlDiscArtTop = New System.Windows.Forms.Panel()
        Me.tblDiscArtTop = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDiscArtTitle = New System.Windows.Forms.Label()
        Me.pbDiscArtCache = New System.Windows.Forms.PictureBox()
        Me.pbClearLogoCache = New System.Windows.Forms.PictureBox()
        Me.pnlClearArt = New System.Windows.Forms.Panel()
        Me.pnlClearArtMain = New System.Windows.Forms.Panel()
        Me.tblClearArtMain = New System.Windows.Forms.TableLayoutPanel()
        Me.pbClearArt = New System.Windows.Forms.PictureBox()
        Me.pnlClearArtBottom = New System.Windows.Forms.Panel()
        Me.tblClearArtBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblClearArtSize = New System.Windows.Forms.Label()
        Me.pnlClearArtTop = New System.Windows.Forms.Panel()
        Me.tblClearArtTop = New System.Windows.Forms.TableLayoutPanel()
        Me.lblClearArtTitle = New System.Windows.Forms.Label()
        Me.pnlLandscape = New System.Windows.Forms.Panel()
        Me.pnlLandscapeMain = New System.Windows.Forms.Panel()
        Me.tblLandscapeMain = New System.Windows.Forms.TableLayoutPanel()
        Me.pbLandscape = New System.Windows.Forms.PictureBox()
        Me.pnlLandscapeBottom = New System.Windows.Forms.Panel()
        Me.tblLandscapeBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblLandscapeSize = New System.Windows.Forms.Label()
        Me.pnlLandscapeTop = New System.Windows.Forms.Panel()
        Me.tblLandscapeTop = New System.Windows.Forms.TableLayoutPanel()
        Me.lblLandscapeTitle = New System.Windows.Forms.Label()
        Me.pnlFanartSmall = New System.Windows.Forms.Panel()
        Me.pnlFanartSmallMain = New System.Windows.Forms.Panel()
        Me.tblFanartSmallMain = New System.Windows.Forms.TableLayoutPanel()
        Me.pbFanartSmall = New System.Windows.Forms.PictureBox()
        Me.pnlFanartSmallBottom = New System.Windows.Forms.Panel()
        Me.tblFanartSmallBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFanartSmallSize = New System.Windows.Forms.Label()
        Me.pnlFanartSmallTop = New System.Windows.Forms.Panel()
        Me.tblFanartSmallTop = New System.Windows.Forms.TableLayoutPanel()
        Me.lblFanartSmallTitle = New System.Windows.Forms.Label()
        Me.pnlPoster = New System.Windows.Forms.Panel()
        Me.pnlPosterMain = New System.Windows.Forms.Panel()
        Me.tblPosterMain = New System.Windows.Forms.TableLayoutPanel()
        Me.pbPoster = New System.Windows.Forms.PictureBox()
        Me.pnlPosterBottom = New System.Windows.Forms.Panel()
        Me.tblPosterBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblPosterSize = New System.Windows.Forms.Label()
        Me.pnlPosterTop = New System.Windows.Forms.Panel()
        Me.tblPosterTop = New System.Windows.Forms.TableLayoutPanel()
        Me.lblPosterTitle = New System.Windows.Forms.Label()
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
        Me.pbPosterCache = New System.Windows.Forms.PictureBox()
        Me.pbFanartSmallCache = New System.Windows.Forms.PictureBox()
        Me.pbLandscapeCache = New System.Windows.Forms.PictureBox()
        Me.pbClearArtCache = New System.Windows.Forms.PictureBox()
        Me.pnlMPAA = New System.Windows.Forms.Panel()
        Me.pbMPAA = New System.Windows.Forms.PictureBox()
        Me.pbFanartCache = New System.Windows.Forms.PictureBox()
        Me.pbFanart = New System.Windows.Forms.PictureBox()
        Me.tsMain = New System.Windows.Forms.ToolStrip()
        Me.mnuScrapeMovies = New System.Windows.Forms.ToolStripSplitButton()
        Me.mnuScrapeSubmenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuScrapeSubmenuAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeSubmenuMissing = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeSubmenuNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeSubmenuMarked = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeSubmenuCustom = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayScrapeMovieSets = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuScrapeMovieSets = New System.Windows.Forms.ToolStripSplitButton()
        Me.mnuScrapeTVShows = New System.Windows.Forms.ToolStripSplitButton()
        Me.mnuUpdate = New System.Windows.Forms.ToolStripSplitButton()
        Me.mnuUpdateMovies = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUpdateShows = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayScrapeTVShows = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayScrapeMovies = New System.Windows.Forms.ToolStripMenuItem()
        Me.ilColumnIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.tmrWait_Movie = New System.Windows.Forms.Timer(Me.components)
        Me.tmrLoad_Movie = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearchWait_Movies = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearch_Movies = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTips = New System.Windows.Forms.ToolTip(Me.components)
        Me.tmrWait_TVShow = New System.Windows.Forms.Timer(Me.components)
        Me.tmrLoad_TVShow = New System.Windows.Forms.Timer(Me.components)
        Me.tmrWait_TVSeason = New System.Windows.Forms.Timer(Me.components)
        Me.tmrLoad_TVSeason = New System.Windows.Forms.Timer(Me.components)
        Me.tmrWait_TVEpisode = New System.Windows.Forms.Timer(Me.components)
        Me.tmrLoad_TVEpisode = New System.Windows.Forms.Timer(Me.components)
        Me.cmnuTray = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuTrayTitle = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator21 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuTrayUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayUpdateMovies = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayUpdateShows = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator23 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuTrayTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsCleanFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsSortFiles = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsBackdrops = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator24 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuTrayToolsOfflineHolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator25 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuTrayToolsClearCache = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsCleanDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmnuTrayToolsReloadMovies = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsReloadMovieSets = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTrayToolsReloadTVShows = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.tmrLoad_MovieSet = New System.Windows.Forms.Timer(Me.components)
        Me.tmrWait_MovieSet = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearchWait_MovieSets = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearch_MovieSets = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearchWait_Shows = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearch_Shows = New System.Windows.Forms.Timer(Me.components)
        Me.tmrRunTasks = New System.Windows.Forms.Timer(Me.components)
        Me.lblTrailerPathHeader = New System.Windows.Forms.Label()
        Me.txtTrailerPath = New System.Windows.Forms.TextBox()
        Me.btnTrailerPlay = New System.Windows.Forms.Button()
        Me.StatusStrip.SuspendLayout()
        Me.mnuMain.SuspendLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.pnlFilterCountries_Movies.SuspendLayout()
        Me.pnlFilterCountriesMain_Movies.SuspendLayout()
        Me.pnlFilterCountriesTop_Movies.SuspendLayout()
        Me.tblFilterCountriesTop_Movies.SuspendLayout()
        Me.pnlFilterGenres_Movies.SuspendLayout()
        Me.pnlFilterGenresMain_Movies.SuspendLayout()
        Me.pnlFilterGenresTop_Movies.SuspendLayout()
        Me.tblFilterGenresTop_Movies.SuspendLayout()
        Me.pnlFilterGenres_Shows.SuspendLayout()
        Me.pnlFilterGenresMain_Shows.SuspendLayout()
        Me.pnlFilterGenresTop_Shows.SuspendLayout()
        Me.tblFilterGenresTop_Shows.SuspendLayout()
        Me.pnlFilterDataFields_Movies.SuspendLayout()
        Me.pnlFilterDataFieldsMain_Movies.SuspendLayout()
        Me.pnlFilterDataFieldsTop_Movies.SuspendLayout()
        Me.tblFilterDataFieldsTop_Movies.SuspendLayout()
        Me.pnlFilterMissingItems_Movies.SuspendLayout()
        Me.pnlFilterMissingItemsMain_Movies.SuspendLayout()
        Me.tblFilterMissingItemsMain_Movies.SuspendLayout()
        Me.pnlFilterMissingItemsTop_Movies.SuspendLayout()
        Me.tblFilterMissingItemsTop_Movies.SuspendLayout()
        Me.pnlFilterMissingItems_MovieSets.SuspendLayout()
        Me.pnlFilterMissingItemsMain_MovieSets.SuspendLayout()
        Me.tlbFilterMissingItemsMain_MovieSets.SuspendLayout()
        Me.pnlFilterMissingItemsTop_MovieSets.SuspendLayout()
        Me.tblFilterMissingItemsTop_MovieSets.SuspendLayout()
        Me.pnlFilterMissingItems_Shows.SuspendLayout()
        Me.pnlFilterMissingItemsMain_Shows.SuspendLayout()
        Me.tblFilterMissingItemsMain_Shows.SuspendLayout()
        Me.pnlFilterMissingItemsTop_Shows.SuspendLayout()
        Me.tblFilterMissingItemsTop_Shows.SuspendLayout()
        Me.pnlFilterSources_Movies.SuspendLayout()
        Me.pnlFilterSourcesMain_Movies.SuspendLayout()
        Me.pnlFilterSourcesTop_Movies.SuspendLayout()
        Me.tblFilterSourcesTop_Movies.SuspendLayout()
        Me.pnlFilterSources_Shows.SuspendLayout()
        Me.pnlFilterSourcesMain_Shows.SuspendLayout()
        Me.pnlFilterSourcesTop_Shows.SuspendLayout()
        Me.tblFilterSourcesTop_Shows.SuspendLayout()
        CType(Me.dgvMovies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmnuMovie.SuspendLayout()
        Me.mnuGenres.SuspendLayout()
        Me.mnuTags.SuspendLayout()
        Me.mnuScrapeType.SuspendLayout()
        Me.mnuScrapeModifier.SuspendLayout()
        Me.mnuScrapeOption.SuspendLayout()
        Me.mnuLanguages.SuspendLayout()
        CType(Me.dgvMovieSets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmnuMovieSet.SuspendLayout()
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
        Me.tblListTop.SuspendLayout()
        Me.tcMain.SuspendLayout()
        Me.pnlSearchMovies.SuspendLayout()
        CType(Me.picSearchMovies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearchMovieSets.SuspendLayout()
        CType(Me.picSearchMovieSets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearchTVShows.SuspendLayout()
        CType(Me.picSearchTVShows, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFilter_Movies.SuspendLayout()
        Me.tblFilter_Movies.SuspendLayout()
        Me.gbFilterGeneral_Movies.SuspendLayout()
        Me.tblFilterGeneral_Movies.SuspendLayout()
        Me.gbFilterSorting_Movies.SuspendLayout()
        Me.tblFilterSorting_Movies.SuspendLayout()
        Me.gbFilterSpecific_Movies.SuspendLayout()
        Me.tblFilterSpecific_Movies.SuspendLayout()
        Me.gbFilterModifier_Movies.SuspendLayout()
        Me.tblFilterModifier_Movies.SuspendLayout()
        Me.tblFilterSpecificData_Movies.SuspendLayout()
        Me.gbFilterDataField_Movies.SuspendLayout()
        Me.tblFilterDataField_Movies.SuspendLayout()
        Me.gbFilterLists_Movies.SuspendLayout()
        Me.tblFilterLists_Movies.SuspendLayout()
        Me.pnlFilterTop_Movies.SuspendLayout()
        Me.tblFilterTop_Movies.SuspendLayout()
        Me.pnlFilter_MovieSets.SuspendLayout()
        Me.tblFilter_MovieSets.SuspendLayout()
        Me.gbFilterLists_MovieSets.SuspendLayout()
        Me.tblFilterLists_MovieSets.SuspendLayout()
        Me.gbFilterGeneral_MovieSets.SuspendLayout()
        Me.tblFilterGeneral_MovieSets.SuspendLayout()
        Me.gbFilterSpecific_MovieSets.SuspendLayout()
        Me.tblFilterSpecific_MovieSets.SuspendLayout()
        Me.gbFilterModifier_MovieSets.SuspendLayout()
        Me.tblFilterModifier_MovieSets.SuspendLayout()
        Me.pnlFilterTop_MovieSets.SuspendLayout()
        Me.tblFilterTop_MovieSets.SuspendLayout()
        Me.pnlFilter_Shows.SuspendLayout()
        Me.tblFilter_Shows.SuspendLayout()
        Me.gbFilterSorting_Shows.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.gbFilterLists_Shows.SuspendLayout()
        Me.tblFilterLists_Shows.SuspendLayout()
        Me.gbFilterGeneral_Shows.SuspendLayout()
        Me.tblFilterGeneral_Shows.SuspendLayout()
        Me.gbFilterSpecific_Shows.SuspendLayout()
        Me.tblFilterSpecific_Shows.SuspendLayout()
        Me.gbFilterModifier_Shows.SuspendLayout()
        Me.tblFilterModifier_Shows.SuspendLayout()
        Me.tblFilterSpecificData_Shows.SuspendLayout()
        Me.pnlFilterTop_Shows.SuspendLayout()
        Me.tblFilterTop_Shows.SuspendLayout()
        Me.pnlCancel.SuspendLayout()
        Me.pnlNoInfo.SuspendLayout()
        Me.pnlNoInfoBG.SuspendLayout()
        CType(Me.pbNoInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInfoPanel.SuspendLayout()
        Me.pnlMoviesInSet.SuspendLayout()
        CType(Me.pbMILoading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlActors.SuspendLayout()
        CType(Me.pbActLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbActors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop250.SuspendLayout()
        CType(Me.pbTop250, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbBannerCache, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBanner.SuspendLayout()
        Me.pnlBannerMain.SuspendLayout()
        Me.tblBannerMain.SuspendLayout()
        CType(Me.pbBanner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBannerBottom.SuspendLayout()
        Me.tblBannerBottom.SuspendLayout()
        Me.pnlBannerTop.SuspendLayout()
        Me.tblBannerTop.SuspendLayout()
        CType(Me.pbCache, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlClearLogo.SuspendLayout()
        Me.pnlClearLogoMain.SuspendLayout()
        Me.tblClearLogoMain.SuspendLayout()
        CType(Me.pbClearLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlClearLogoBottom.SuspendLayout()
        Me.tblClearLogoBottom.SuspendLayout()
        Me.pnlClearLogoTop.SuspendLayout()
        Me.tblClearLogoTop.SuspendLayout()
        Me.pnlCharacterArt.SuspendLayout()
        Me.pnlCharacterArtMain.SuspendLayout()
        Me.tblCharacterArtMain.SuspendLayout()
        CType(Me.pbCharacterArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCharacterArtBottom.SuspendLayout()
        Me.tblCharacterArtBottom.SuspendLayout()
        Me.pnlCharacterArtTop.SuspendLayout()
        Me.tblCharacterArtTop.SuspendLayout()
        CType(Me.pbCharacterArtCache, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDiscArt.SuspendLayout()
        Me.pnlDiscArtMain.SuspendLayout()
        Me.tblDiscArtMain.SuspendLayout()
        CType(Me.pbDiscArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDiscArtBottom.SuspendLayout()
        Me.tblDiscArtBottom.SuspendLayout()
        Me.pnlDiscArtTop.SuspendLayout()
        Me.tblDiscArtTop.SuspendLayout()
        CType(Me.pbDiscArtCache, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbClearLogoCache, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlClearArt.SuspendLayout()
        Me.pnlClearArtMain.SuspendLayout()
        Me.tblClearArtMain.SuspendLayout()
        CType(Me.pbClearArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlClearArtBottom.SuspendLayout()
        Me.tblClearArtBottom.SuspendLayout()
        Me.pnlClearArtTop.SuspendLayout()
        Me.tblClearArtTop.SuspendLayout()
        Me.pnlLandscape.SuspendLayout()
        Me.pnlLandscapeMain.SuspendLayout()
        Me.tblLandscapeMain.SuspendLayout()
        CType(Me.pbLandscape, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLandscapeBottom.SuspendLayout()
        Me.tblLandscapeBottom.SuspendLayout()
        Me.pnlLandscapeTop.SuspendLayout()
        Me.tblLandscapeTop.SuspendLayout()
        Me.pnlFanartSmall.SuspendLayout()
        Me.pnlFanartSmallMain.SuspendLayout()
        Me.tblFanartSmallMain.SuspendLayout()
        CType(Me.pbFanartSmall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFanartSmallBottom.SuspendLayout()
        Me.tblFanartSmallBottom.SuspendLayout()
        Me.pnlFanartSmallTop.SuspendLayout()
        Me.tblFanartSmallTop.SuspendLayout()
        Me.pnlPoster.SuspendLayout()
        Me.pnlPosterMain.SuspendLayout()
        Me.tblPosterMain.SuspendLayout()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPosterBottom.SuspendLayout()
        Me.tblPosterBottom.SuspendLayout()
        Me.pnlPosterTop.SuspendLayout()
        Me.tblPosterTop.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.tlpHeader.SuspendLayout()
        Me.pnlRating.SuspendLayout()
        CType(Me.pbStar10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInfoIcons.SuspendLayout()
        CType(Me.pbSubtitleLang6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbSubtitleLang5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbSubtitleLang4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbSubtitleLang3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbSubtitleLang2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbSubtitleLang1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbSubtitleLang0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbAudioLang6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbAudioLang5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbAudioLang4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbAudioLang3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbAudioLang2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbAudioLang1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbAudioLang0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbVType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStudio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbVideo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbAudio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbResolution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbChannels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbPosterCache, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFanartSmallCache, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbLandscapeCache, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbClearArtCache, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMPAA.SuspendLayout()
        CType(Me.pbMPAA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFanartCache, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tsMain.SuspendLayout()
        Me.mnuScrapeSubmenu.SuspendLayout()
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
        Me.mnuMainHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainHelpWiki, Me.mnuMainHelpForumEng, Me.mnuMainHelpForumGer, Me.mnuMainHelpSeparator0, Me.mnuMainHelpVersions, Me.mnuMainHelpUpdate, Me.mnuMainHelpSeparator1, Me.mnuMainHelpAbout})
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
        'mnuMainHelpSeparator1
        '
        Me.mnuMainHelpSeparator1.Name = "mnuMainHelpSeparator1"
        Me.mnuMainHelpSeparator1.Size = New System.Drawing.Size(182, 6)
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
        Me.StatusStrip.Location = New System.Drawing.Point(5, 923)
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
        Me.mnuMainTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainToolsCleanFiles, Me.mnuMainToolsSortFiles, Me.mnuMainToolsBackdrops, Me.mnuMainToolsSeparator0, Me.mnuMainToolsOfflineHolder, Me.mnuMainToolsSeparator1, Me.mnuMainToolsClearCache, Me.mnuMainToolsCleanDB, Me.ToolStripSeparator2, Me.mnuMainToolsReloadMovies, Me.mnuMainToolsReloadMovieSets, Me.mnuMainToolsReloadTVShows, Me.ToolStripSeparator3, Me.mnuMainToolsRewriteMovieContent, Me.mnuMainToolsSeparator2, Me.mnuMainToolsExport})
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
        'mnuMainToolsSeparator0
        '
        Me.mnuMainToolsSeparator0.Name = "mnuMainToolsSeparator0"
        Me.mnuMainToolsSeparator0.Size = New System.Drawing.Size(349, 6)
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
        Me.mnuMainToolsOfflineHolder.Visible = False
        '
        'mnuMainToolsSeparator1
        '
        Me.mnuMainToolsSeparator1.Name = "mnuMainToolsSeparator1"
        Me.mnuMainToolsSeparator1.Size = New System.Drawing.Size(349, 6)
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
        'mnuMainToolsCleanDB
        '
        Me.mnuMainToolsCleanDB.Image = CType(resources.GetObject("mnuMainToolsCleanDB.Image"), System.Drawing.Image)
        Me.mnuMainToolsCleanDB.Name = "mnuMainToolsCleanDB"
        Me.mnuMainToolsCleanDB.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.mnuMainToolsCleanDB.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsCleanDB.Text = "Clean &Database"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(349, 6)
        '
        'mnuMainToolsReloadMovies
        '
        Me.mnuMainToolsReloadMovies.Image = CType(resources.GetObject("mnuMainToolsReloadMovies.Image"), System.Drawing.Image)
        Me.mnuMainToolsReloadMovies.Name = "mnuMainToolsReloadMovies"
        Me.mnuMainToolsReloadMovies.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.mnuMainToolsReloadMovies.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsReloadMovies.Tag = ""
        Me.mnuMainToolsReloadMovies.Text = "Re&load All Movies"
        '
        'mnuMainToolsReloadMovieSets
        '
        Me.mnuMainToolsReloadMovieSets.Image = CType(resources.GetObject("mnuMainToolsReloadMovieSets.Image"), System.Drawing.Image)
        Me.mnuMainToolsReloadMovieSets.Name = "mnuMainToolsReloadMovieSets"
        Me.mnuMainToolsReloadMovieSets.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsReloadMovieSets.Text = "Reload All MovieSets"
        '
        'mnuMainToolsReloadTVShows
        '
        Me.mnuMainToolsReloadTVShows.Image = CType(resources.GetObject("mnuMainToolsReloadTVShows.Image"), System.Drawing.Image)
        Me.mnuMainToolsReloadTVShows.Name = "mnuMainToolsReloadTVShows"
        Me.mnuMainToolsReloadTVShows.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsReloadTVShows.Tag = ""
        Me.mnuMainToolsReloadTVShows.Text = "Reload All TV Shows"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(349, 6)
        '
        'mnuMainToolsRewriteMovieContent
        '
        Me.mnuMainToolsRewriteMovieContent.Image = CType(resources.GetObject("mnuMainToolsRewriteMovieContent.Image"), System.Drawing.Image)
        Me.mnuMainToolsRewriteMovieContent.Name = "mnuMainToolsRewriteMovieContent"
        Me.mnuMainToolsRewriteMovieContent.Size = New System.Drawing.Size(352, 22)
        Me.mnuMainToolsRewriteMovieContent.Text = "Rewrite All Movie Content"
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
        Me.scMain.Panel2.Controls.Add(Me.pnlCancel)
        Me.scMain.Panel2.Controls.Add(Me.pnlNoInfo)
        Me.scMain.Panel2.Controls.Add(Me.pnlInfoPanel)
        Me.scMain.Panel2.Controls.Add(Me.pbBannerCache)
        Me.scMain.Panel2.Controls.Add(Me.pnlBanner)
        Me.scMain.Panel2.Controls.Add(Me.pbCache)
        Me.scMain.Panel2.Controls.Add(Me.pnlClearLogo)
        Me.scMain.Panel2.Controls.Add(Me.pnlCharacterArt)
        Me.scMain.Panel2.Controls.Add(Me.pbCharacterArtCache)
        Me.scMain.Panel2.Controls.Add(Me.pnlDiscArt)
        Me.scMain.Panel2.Controls.Add(Me.pbDiscArtCache)
        Me.scMain.Panel2.Controls.Add(Me.pbClearLogoCache)
        Me.scMain.Panel2.Controls.Add(Me.pnlClearArt)
        Me.scMain.Panel2.Controls.Add(Me.pnlLandscape)
        Me.scMain.Panel2.Controls.Add(Me.pnlFanartSmall)
        Me.scMain.Panel2.Controls.Add(Me.pnlPoster)
        Me.scMain.Panel2.Controls.Add(Me.pnlTop)
        Me.scMain.Panel2.Controls.Add(Me.pbPosterCache)
        Me.scMain.Panel2.Controls.Add(Me.pbFanartSmallCache)
        Me.scMain.Panel2.Controls.Add(Me.pbLandscapeCache)
        Me.scMain.Panel2.Controls.Add(Me.pbClearArtCache)
        Me.scMain.Panel2.Controls.Add(Me.pnlMPAA)
        Me.scMain.Panel2.Controls.Add(Me.pbFanartCache)
        Me.scMain.Panel2.Controls.Add(Me.pbFanart)
        Me.scMain.Panel2.Controls.Add(Me.tsMain)
        Me.scMain.Panel2.Margin = New System.Windows.Forms.Padding(3)
        Me.scMain.Size = New System.Drawing.Size(1344, 899)
        Me.scMain.SplitterDistance = 567
        Me.scMain.TabIndex = 7
        Me.scMain.TabStop = False
        '
        'pnlFilterCountries_Movies
        '
        Me.pnlFilterCountries_Movies.Controls.Add(Me.pnlFilterCountriesMain_Movies)
        Me.pnlFilterCountries_Movies.Controls.Add(Me.pnlFilterCountriesTop_Movies)
        Me.pnlFilterCountries_Movies.Location = New System.Drawing.Point(0, 900)
        Me.pnlFilterCountries_Movies.Name = "pnlFilterCountries_Movies"
        Me.pnlFilterCountries_Movies.Size = New System.Drawing.Size(189, 192)
        Me.pnlFilterCountries_Movies.TabIndex = 25
        Me.pnlFilterCountries_Movies.Visible = False
        '
        'pnlFilterCountriesMain_Movies
        '
        Me.pnlFilterCountriesMain_Movies.AutoSize = True
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
        Me.clbFilterCountries_Movies.CheckOnClick = True
        Me.clbFilterCountries_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbFilterCountries_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.clbFilterCountries_Movies.FormattingEnabled = True
        Me.clbFilterCountries_Movies.Location = New System.Drawing.Point(0, 0)
        Me.clbFilterCountries_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.clbFilterCountries_Movies.Name = "clbFilterCountries_Movies"
        Me.clbFilterCountries_Movies.Size = New System.Drawing.Size(187, 170)
        Me.clbFilterCountries_Movies.TabIndex = 8
        Me.clbFilterCountries_Movies.TabStop = False
        '
        'pnlFilterCountriesTop_Movies
        '
        Me.pnlFilterCountriesTop_Movies.AutoSize = True
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
        Me.tblFilterCountriesTop_Movies.AutoSize = True
        Me.tblFilterCountriesTop_Movies.ColumnCount = 3
        Me.tblFilterCountriesTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterCountriesTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblFilterCountriesTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterCountriesTop_Movies.Controls.Add(Me.lblFilterCountries_Movies, 0, 0)
        Me.tblFilterCountriesTop_Movies.Controls.Add(Me.lblFilterCountriesClose_Movies, 2, 0)
        Me.tblFilterCountriesTop_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterCountriesTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterCountriesTop_Movies.Name = "tblFilterCountriesTop_Movies"
        Me.tblFilterCountriesTop_Movies.RowCount = 2
        Me.tblFilterCountriesTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterCountriesTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterCountriesTop_Movies.Size = New System.Drawing.Size(189, 20)
        Me.tblFilterCountriesTop_Movies.TabIndex = 0
        '
        'lblFilterCountries_Movies
        '
        Me.lblFilterCountries_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterCountries_Movies.AutoSize = True
        Me.lblFilterCountries_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterCountries_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.lblFilterCountriesClose_Movies.AutoSize = True
        Me.lblFilterCountriesClose_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterCountriesClose_Movies.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterCountriesClose_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.pnlFilterGenres_Movies.Visible = False
        '
        'pnlFilterGenresMain_Movies
        '
        Me.pnlFilterGenresMain_Movies.AutoSize = True
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
        Me.clbFilterGenres_Movies.CheckOnClick = True
        Me.clbFilterGenres_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbFilterGenres_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.clbFilterGenres_Movies.FormattingEnabled = True
        Me.clbFilterGenres_Movies.Location = New System.Drawing.Point(0, 0)
        Me.clbFilterGenres_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.clbFilterGenres_Movies.Name = "clbFilterGenres_Movies"
        Me.clbFilterGenres_Movies.Size = New System.Drawing.Size(187, 170)
        Me.clbFilterGenres_Movies.TabIndex = 8
        Me.clbFilterGenres_Movies.TabStop = False
        '
        'pnlFilterGenresTop_Movies
        '
        Me.pnlFilterGenresTop_Movies.AutoSize = True
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
        Me.tblFilterGenresTop_Movies.AutoSize = True
        Me.tblFilterGenresTop_Movies.ColumnCount = 3
        Me.tblFilterGenresTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGenresTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblFilterGenresTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGenresTop_Movies.Controls.Add(Me.lblFilterGenresClose_Movies, 2, 0)
        Me.tblFilterGenresTop_Movies.Controls.Add(Me.lblFilterGenres_Movies, 0, 0)
        Me.tblFilterGenresTop_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterGenresTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterGenresTop_Movies.Name = "tblFilterGenresTop_Movies"
        Me.tblFilterGenresTop_Movies.RowCount = 2
        Me.tblFilterGenresTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterGenresTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGenresTop_Movies.Size = New System.Drawing.Size(189, 20)
        Me.tblFilterGenresTop_Movies.TabIndex = 0
        '
        'lblFilterGenresClose_Movies
        '
        Me.lblFilterGenresClose_Movies.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblFilterGenresClose_Movies.AutoSize = True
        Me.lblFilterGenresClose_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterGenresClose_Movies.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterGenresClose_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.lblFilterGenres_Movies.AutoSize = True
        Me.lblFilterGenres_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterGenres_Movies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFilterGenres_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.pnlFilterGenres_Shows.Visible = False
        '
        'pnlFilterGenresMain_Shows
        '
        Me.pnlFilterGenresMain_Shows.AutoSize = True
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
        Me.clbFilterGenres_Shows.CheckOnClick = True
        Me.clbFilterGenres_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbFilterGenres_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.clbFilterGenres_Shows.FormattingEnabled = True
        Me.clbFilterGenres_Shows.Location = New System.Drawing.Point(0, 0)
        Me.clbFilterGenres_Shows.Margin = New System.Windows.Forms.Padding(0)
        Me.clbFilterGenres_Shows.Name = "clbFilterGenres_Shows"
        Me.clbFilterGenres_Shows.Size = New System.Drawing.Size(187, 170)
        Me.clbFilterGenres_Shows.TabIndex = 8
        Me.clbFilterGenres_Shows.TabStop = False
        '
        'pnlFilterGenresTop_Shows
        '
        Me.pnlFilterGenresTop_Shows.AutoSize = True
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
        Me.tblFilterGenresTop_Shows.AutoSize = True
        Me.tblFilterGenresTop_Shows.ColumnCount = 3
        Me.tblFilterGenresTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGenresTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblFilterGenresTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGenresTop_Shows.Controls.Add(Me.lblFilterGenres_Shows, 0, 0)
        Me.tblFilterGenresTop_Shows.Controls.Add(Me.lblFilterGenresClose_Shows, 2, 0)
        Me.tblFilterGenresTop_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterGenresTop_Shows.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterGenresTop_Shows.Name = "tblFilterGenresTop_Shows"
        Me.tblFilterGenresTop_Shows.RowCount = 2
        Me.tblFilterGenresTop_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterGenresTop_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGenresTop_Shows.Size = New System.Drawing.Size(189, 20)
        Me.tblFilterGenresTop_Shows.TabIndex = 0
        '
        'lblFilterGenres_Shows
        '
        Me.lblFilterGenres_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterGenres_Shows.AutoSize = True
        Me.lblFilterGenres_Shows.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterGenres_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.lblFilterGenresClose_Shows.AutoSize = True
        Me.lblFilterGenresClose_Shows.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterGenresClose_Shows.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterGenresClose_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.pnlFilterDataFields_Movies.Visible = False
        '
        'pnlFilterDataFieldsMain_Movies
        '
        Me.pnlFilterDataFieldsMain_Movies.AutoSize = True
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
        Me.clbFilterDataFields_Movies.CheckOnClick = True
        Me.clbFilterDataFields_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbFilterDataFields_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.clbFilterDataFields_Movies.FormattingEnabled = True
        Me.clbFilterDataFields_Movies.Location = New System.Drawing.Point(0, 0)
        Me.clbFilterDataFields_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.clbFilterDataFields_Movies.Name = "clbFilterDataFields_Movies"
        Me.clbFilterDataFields_Movies.Size = New System.Drawing.Size(187, 170)
        Me.clbFilterDataFields_Movies.TabIndex = 8
        Me.clbFilterDataFields_Movies.TabStop = False
        '
        'pnlFilterDataFieldsTop_Movies
        '
        Me.pnlFilterDataFieldsTop_Movies.AutoSize = True
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
        Me.tblFilterDataFieldsTop_Movies.AutoSize = True
        Me.tblFilterDataFieldsTop_Movies.ColumnCount = 3
        Me.tblFilterDataFieldsTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterDataFieldsTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblFilterDataFieldsTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterDataFieldsTop_Movies.Controls.Add(Me.lblFilterDataFields_Movies, 0, 0)
        Me.tblFilterDataFieldsTop_Movies.Controls.Add(Me.lblFilterDataFieldsClose_Movies, 2, 0)
        Me.tblFilterDataFieldsTop_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterDataFieldsTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterDataFieldsTop_Movies.Name = "tblFilterDataFieldsTop_Movies"
        Me.tblFilterDataFieldsTop_Movies.RowCount = 2
        Me.tblFilterDataFieldsTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterDataFieldsTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterDataFieldsTop_Movies.Size = New System.Drawing.Size(189, 20)
        Me.tblFilterDataFieldsTop_Movies.TabIndex = 26
        '
        'lblFilterDataFields_Movies
        '
        Me.lblFilterDataFields_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterDataFields_Movies.AutoSize = True
        Me.lblFilterDataFields_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterDataFields_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.lblFilterDataFieldsClose_Movies.AutoSize = True
        Me.lblFilterDataFieldsClose_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterDataFieldsClose_Movies.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterDataFieldsClose_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilterDataFieldsClose_Movies.ForeColor = System.Drawing.Color.White
        Me.lblFilterDataFieldsClose_Movies.Location = New System.Drawing.Point(151, 3)
        Me.lblFilterDataFieldsClose_Movies.Name = "lblFilterDataFieldsClose_Movies"
        Me.lblFilterDataFieldsClose_Movies.Size = New System.Drawing.Size(35, 13)
        Me.lblFilterDataFieldsClose_Movies.TabIndex = 24
        Me.lblFilterDataFieldsClose_Movies.Text = "Close"
        '
        'pnlFilterMissingItems_Movies
        '
        Me.pnlFilterMissingItems_Movies.AutoSize = True
        Me.pnlFilterMissingItems_Movies.Controls.Add(Me.pnlFilterMissingItemsMain_Movies)
        Me.pnlFilterMissingItems_Movies.Controls.Add(Me.pnlFilterMissingItemsTop_Movies)
        Me.pnlFilterMissingItems_Movies.Location = New System.Drawing.Point(0, 900)
        Me.pnlFilterMissingItems_Movies.Name = "pnlFilterMissingItems_Movies"
        Me.pnlFilterMissingItems_Movies.Size = New System.Drawing.Size(170, 321)
        Me.pnlFilterMissingItems_Movies.TabIndex = 27
        Me.pnlFilterMissingItems_Movies.Visible = False
        '
        'pnlFilterMissingItemsMain_Movies
        '
        Me.pnlFilterMissingItemsMain_Movies.AutoSize = True
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
        Me.tblFilterMissingItemsMain_Movies.AutoSize = True
        Me.tblFilterMissingItemsMain_Movies.ColumnCount = 2
        Me.tblFilterMissingItemsMain_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsMain_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingBanner, 0, 0)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingClearArt, 0, 1)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingClearLogo, 0, 2)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingDiscArt, 0, 3)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingExtrafanarts, 0, 4)
        Me.tblFilterMissingItemsMain_Movies.Controls.Add(Me.chkMovieMissingExtrathumbs, 0, 5)
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
        Me.chkMovieMissingBanner.AutoSize = True
        Me.chkMovieMissingBanner.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieMissingBanner.Name = "chkMovieMissingBanner"
        Me.chkMovieMissingBanner.Size = New System.Drawing.Size(63, 17)
        Me.chkMovieMissingBanner.TabIndex = 0
        Me.chkMovieMissingBanner.Text = "Banner"
        Me.chkMovieMissingBanner.UseVisualStyleBackColor = True
        '
        'chkMovieMissingClearArt
        '
        Me.chkMovieMissingClearArt.AutoSize = True
        Me.chkMovieMissingClearArt.Location = New System.Drawing.Point(3, 26)
        Me.chkMovieMissingClearArt.Name = "chkMovieMissingClearArt"
        Me.chkMovieMissingClearArt.Size = New System.Drawing.Size(67, 17)
        Me.chkMovieMissingClearArt.TabIndex = 0
        Me.chkMovieMissingClearArt.Text = "ClearArt"
        Me.chkMovieMissingClearArt.UseVisualStyleBackColor = True
        '
        'chkMovieMissingClearLogo
        '
        Me.chkMovieMissingClearLogo.AutoSize = True
        Me.chkMovieMissingClearLogo.Location = New System.Drawing.Point(3, 49)
        Me.chkMovieMissingClearLogo.Name = "chkMovieMissingClearLogo"
        Me.chkMovieMissingClearLogo.Size = New System.Drawing.Size(78, 17)
        Me.chkMovieMissingClearLogo.TabIndex = 0
        Me.chkMovieMissingClearLogo.Text = "ClearLogo"
        Me.chkMovieMissingClearLogo.UseVisualStyleBackColor = True
        '
        'chkMovieMissingDiscArt
        '
        Me.chkMovieMissingDiscArt.AutoSize = True
        Me.chkMovieMissingDiscArt.Location = New System.Drawing.Point(3, 72)
        Me.chkMovieMissingDiscArt.Name = "chkMovieMissingDiscArt"
        Me.chkMovieMissingDiscArt.Size = New System.Drawing.Size(62, 17)
        Me.chkMovieMissingDiscArt.TabIndex = 0
        Me.chkMovieMissingDiscArt.Text = "DiscArt"
        Me.chkMovieMissingDiscArt.UseVisualStyleBackColor = True
        '
        'chkMovieMissingExtrafanarts
        '
        Me.chkMovieMissingExtrafanarts.AutoSize = True
        Me.chkMovieMissingExtrafanarts.Location = New System.Drawing.Point(3, 95)
        Me.chkMovieMissingExtrafanarts.Name = "chkMovieMissingExtrafanarts"
        Me.chkMovieMissingExtrafanarts.Size = New System.Drawing.Size(87, 17)
        Me.chkMovieMissingExtrafanarts.TabIndex = 0
        Me.chkMovieMissingExtrafanarts.Text = "Extrafanarts"
        Me.chkMovieMissingExtrafanarts.UseVisualStyleBackColor = True
        '
        'chkMovieMissingExtrathumbs
        '
        Me.chkMovieMissingExtrathumbs.AutoSize = True
        Me.chkMovieMissingExtrathumbs.Location = New System.Drawing.Point(3, 118)
        Me.chkMovieMissingExtrathumbs.Name = "chkMovieMissingExtrathumbs"
        Me.chkMovieMissingExtrathumbs.Size = New System.Drawing.Size(90, 17)
        Me.chkMovieMissingExtrathumbs.TabIndex = 0
        Me.chkMovieMissingExtrathumbs.Text = "Extrathumbs"
        Me.chkMovieMissingExtrathumbs.UseVisualStyleBackColor = True
        '
        'chkMovieMissingFanart
        '
        Me.chkMovieMissingFanart.AutoSize = True
        Me.chkMovieMissingFanart.Location = New System.Drawing.Point(3, 141)
        Me.chkMovieMissingFanart.Name = "chkMovieMissingFanart"
        Me.chkMovieMissingFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieMissingFanart.TabIndex = 0
        Me.chkMovieMissingFanart.Text = "Fanart"
        Me.chkMovieMissingFanart.UseVisualStyleBackColor = True
        '
        'chkMovieMissingLandscape
        '
        Me.chkMovieMissingLandscape.AutoSize = True
        Me.chkMovieMissingLandscape.Location = New System.Drawing.Point(3, 164)
        Me.chkMovieMissingLandscape.Name = "chkMovieMissingLandscape"
        Me.chkMovieMissingLandscape.Size = New System.Drawing.Size(80, 17)
        Me.chkMovieMissingLandscape.TabIndex = 0
        Me.chkMovieMissingLandscape.Text = "Landscape"
        Me.chkMovieMissingLandscape.UseVisualStyleBackColor = True
        '
        'chkMovieMissingTrailer
        '
        Me.chkMovieMissingTrailer.AutoSize = True
        Me.chkMovieMissingTrailer.Location = New System.Drawing.Point(3, 279)
        Me.chkMovieMissingTrailer.Name = "chkMovieMissingTrailer"
        Me.chkMovieMissingTrailer.Size = New System.Drawing.Size(57, 17)
        Me.chkMovieMissingTrailer.TabIndex = 0
        Me.chkMovieMissingTrailer.Text = "Trailer"
        Me.chkMovieMissingTrailer.UseVisualStyleBackColor = True
        '
        'chkMovieMissingTheme
        '
        Me.chkMovieMissingTheme.AutoSize = True
        Me.chkMovieMissingTheme.Location = New System.Drawing.Point(3, 256)
        Me.chkMovieMissingTheme.Name = "chkMovieMissingTheme"
        Me.chkMovieMissingTheme.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieMissingTheme.TabIndex = 0
        Me.chkMovieMissingTheme.Text = "Theme"
        Me.chkMovieMissingTheme.UseVisualStyleBackColor = True
        '
        'chkMovieMissingSubtitles
        '
        Me.chkMovieMissingSubtitles.AutoSize = True
        Me.chkMovieMissingSubtitles.Location = New System.Drawing.Point(3, 233)
        Me.chkMovieMissingSubtitles.Name = "chkMovieMissingSubtitles"
        Me.chkMovieMissingSubtitles.Size = New System.Drawing.Size(66, 17)
        Me.chkMovieMissingSubtitles.TabIndex = 0
        Me.chkMovieMissingSubtitles.Text = "Subtitle"
        Me.chkMovieMissingSubtitles.UseVisualStyleBackColor = True
        '
        'chkMovieMissingPoster
        '
        Me.chkMovieMissingPoster.AutoSize = True
        Me.chkMovieMissingPoster.Location = New System.Drawing.Point(3, 210)
        Me.chkMovieMissingPoster.Name = "chkMovieMissingPoster"
        Me.chkMovieMissingPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkMovieMissingPoster.TabIndex = 0
        Me.chkMovieMissingPoster.Text = "Poster"
        Me.chkMovieMissingPoster.UseVisualStyleBackColor = True
        '
        'chkMovieMissingNFO
        '
        Me.chkMovieMissingNFO.AutoSize = True
        Me.chkMovieMissingNFO.Location = New System.Drawing.Point(3, 187)
        Me.chkMovieMissingNFO.Name = "chkMovieMissingNFO"
        Me.chkMovieMissingNFO.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieMissingNFO.TabIndex = 0
        Me.chkMovieMissingNFO.Text = "NFO"
        Me.chkMovieMissingNFO.UseVisualStyleBackColor = True
        '
        'pnlFilterMissingItemsTop_Movies
        '
        Me.pnlFilterMissingItemsTop_Movies.AutoSize = True
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
        Me.tblFilterMissingItemsTop_Movies.AutoSize = True
        Me.tblFilterMissingItemsTop_Movies.ColumnCount = 3
        Me.tblFilterMissingItemsTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblFilterMissingItemsTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsTop_Movies.Controls.Add(Me.lblFilterMissingItems_Movies, 0, 0)
        Me.tblFilterMissingItemsTop_Movies.Controls.Add(Me.lblFilterMissingItemsClose_Movies, 2, 0)
        Me.tblFilterMissingItemsTop_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterMissingItemsTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterMissingItemsTop_Movies.Name = "tblFilterMissingItemsTop_Movies"
        Me.tblFilterMissingItemsTop_Movies.RowCount = 2
        Me.tblFilterMissingItemsTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterMissingItemsTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsTop_Movies.Size = New System.Drawing.Size(170, 20)
        Me.tblFilterMissingItemsTop_Movies.TabIndex = 0
        '
        'lblFilterMissingItems_Movies
        '
        Me.lblFilterMissingItems_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterMissingItems_Movies.AutoSize = True
        Me.lblFilterMissingItems_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterMissingItems_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.lblFilterMissingItemsClose_Movies.AutoSize = True
        Me.lblFilterMissingItemsClose_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterMissingItemsClose_Movies.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterMissingItemsClose_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilterMissingItemsClose_Movies.ForeColor = System.Drawing.Color.White
        Me.lblFilterMissingItemsClose_Movies.Location = New System.Drawing.Point(132, 3)
        Me.lblFilterMissingItemsClose_Movies.Name = "lblFilterMissingItemsClose_Movies"
        Me.lblFilterMissingItemsClose_Movies.Size = New System.Drawing.Size(35, 13)
        Me.lblFilterMissingItemsClose_Movies.TabIndex = 24
        Me.lblFilterMissingItemsClose_Movies.Text = "Close"
        '
        'pnlFilterMissingItems_MovieSets
        '
        Me.pnlFilterMissingItems_MovieSets.AutoSize = True
        Me.pnlFilterMissingItems_MovieSets.Controls.Add(Me.pnlFilterMissingItemsMain_MovieSets)
        Me.pnlFilterMissingItems_MovieSets.Controls.Add(Me.pnlFilterMissingItemsTop_MovieSets)
        Me.pnlFilterMissingItems_MovieSets.Location = New System.Drawing.Point(0, 900)
        Me.pnlFilterMissingItems_MovieSets.Name = "pnlFilterMissingItems_MovieSets"
        Me.pnlFilterMissingItems_MovieSets.Size = New System.Drawing.Size(170, 206)
        Me.pnlFilterMissingItems_MovieSets.TabIndex = 28
        Me.pnlFilterMissingItems_MovieSets.Visible = False
        '
        'pnlFilterMissingItemsMain_MovieSets
        '
        Me.pnlFilterMissingItemsMain_MovieSets.AutoSize = True
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
        Me.tlbFilterMissingItemsMain_MovieSets.AutoSize = True
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
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlbFilterMissingItemsMain_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlbFilterMissingItemsMain_MovieSets.Size = New System.Drawing.Size(168, 184)
        Me.tlbFilterMissingItemsMain_MovieSets.TabIndex = 0
        '
        'chkMovieSetMissingBanner
        '
        Me.chkMovieSetMissingBanner.AutoSize = True
        Me.chkMovieSetMissingBanner.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieSetMissingBanner.Name = "chkMovieSetMissingBanner"
        Me.chkMovieSetMissingBanner.Size = New System.Drawing.Size(63, 17)
        Me.chkMovieSetMissingBanner.TabIndex = 0
        Me.chkMovieSetMissingBanner.Text = "Banner"
        Me.chkMovieSetMissingBanner.UseVisualStyleBackColor = True
        '
        'chkMovieSetMissingClearArt
        '
        Me.chkMovieSetMissingClearArt.AutoSize = True
        Me.chkMovieSetMissingClearArt.Location = New System.Drawing.Point(3, 26)
        Me.chkMovieSetMissingClearArt.Name = "chkMovieSetMissingClearArt"
        Me.chkMovieSetMissingClearArt.Size = New System.Drawing.Size(67, 17)
        Me.chkMovieSetMissingClearArt.TabIndex = 0
        Me.chkMovieSetMissingClearArt.Text = "ClearArt"
        Me.chkMovieSetMissingClearArt.UseVisualStyleBackColor = True
        '
        'chkMovieSetMissingClearLogo
        '
        Me.chkMovieSetMissingClearLogo.AutoSize = True
        Me.chkMovieSetMissingClearLogo.Location = New System.Drawing.Point(3, 49)
        Me.chkMovieSetMissingClearLogo.Name = "chkMovieSetMissingClearLogo"
        Me.chkMovieSetMissingClearLogo.Size = New System.Drawing.Size(78, 17)
        Me.chkMovieSetMissingClearLogo.TabIndex = 0
        Me.chkMovieSetMissingClearLogo.Text = "ClearLogo"
        Me.chkMovieSetMissingClearLogo.UseVisualStyleBackColor = True
        '
        'chkMovieSetMissingDiscArt
        '
        Me.chkMovieSetMissingDiscArt.AutoSize = True
        Me.chkMovieSetMissingDiscArt.Location = New System.Drawing.Point(3, 72)
        Me.chkMovieSetMissingDiscArt.Name = "chkMovieSetMissingDiscArt"
        Me.chkMovieSetMissingDiscArt.Size = New System.Drawing.Size(62, 17)
        Me.chkMovieSetMissingDiscArt.TabIndex = 0
        Me.chkMovieSetMissingDiscArt.Text = "DiscArt"
        Me.chkMovieSetMissingDiscArt.UseVisualStyleBackColor = True
        '
        'chkMovieSetMissingFanart
        '
        Me.chkMovieSetMissingFanart.AutoSize = True
        Me.chkMovieSetMissingFanart.Location = New System.Drawing.Point(3, 95)
        Me.chkMovieSetMissingFanart.Name = "chkMovieSetMissingFanart"
        Me.chkMovieSetMissingFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieSetMissingFanart.TabIndex = 0
        Me.chkMovieSetMissingFanart.Text = "Fanart"
        Me.chkMovieSetMissingFanart.UseVisualStyleBackColor = True
        '
        'chkMovieSetMissingLandscape
        '
        Me.chkMovieSetMissingLandscape.AutoSize = True
        Me.chkMovieSetMissingLandscape.Location = New System.Drawing.Point(3, 118)
        Me.chkMovieSetMissingLandscape.Name = "chkMovieSetMissingLandscape"
        Me.chkMovieSetMissingLandscape.Size = New System.Drawing.Size(80, 17)
        Me.chkMovieSetMissingLandscape.TabIndex = 0
        Me.chkMovieSetMissingLandscape.Text = "Landscape"
        Me.chkMovieSetMissingLandscape.UseVisualStyleBackColor = True
        '
        'chkMovieSetMissingPoster
        '
        Me.chkMovieSetMissingPoster.AutoSize = True
        Me.chkMovieSetMissingPoster.Location = New System.Drawing.Point(3, 164)
        Me.chkMovieSetMissingPoster.Name = "chkMovieSetMissingPoster"
        Me.chkMovieSetMissingPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkMovieSetMissingPoster.TabIndex = 0
        Me.chkMovieSetMissingPoster.Text = "Poster"
        Me.chkMovieSetMissingPoster.UseVisualStyleBackColor = True
        '
        'chkMovieSetMissingNFO
        '
        Me.chkMovieSetMissingNFO.AutoSize = True
        Me.chkMovieSetMissingNFO.Location = New System.Drawing.Point(3, 141)
        Me.chkMovieSetMissingNFO.Name = "chkMovieSetMissingNFO"
        Me.chkMovieSetMissingNFO.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieSetMissingNFO.TabIndex = 0
        Me.chkMovieSetMissingNFO.Text = "NFO"
        Me.chkMovieSetMissingNFO.UseVisualStyleBackColor = True
        '
        'pnlFilterMissingItemsTop_MovieSets
        '
        Me.pnlFilterMissingItemsTop_MovieSets.AutoSize = True
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
        Me.tblFilterMissingItemsTop_MovieSets.AutoSize = True
        Me.tblFilterMissingItemsTop_MovieSets.ColumnCount = 3
        Me.tblFilterMissingItemsTop_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsTop_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblFilterMissingItemsTop_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsTop_MovieSets.Controls.Add(Me.lblFilterMissingItems_MovieSets, 0, 0)
        Me.tblFilterMissingItemsTop_MovieSets.Controls.Add(Me.lblFilterMissingItemsClose_MovieSets, 2, 0)
        Me.tblFilterMissingItemsTop_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterMissingItemsTop_MovieSets.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterMissingItemsTop_MovieSets.Name = "tblFilterMissingItemsTop_MovieSets"
        Me.tblFilterMissingItemsTop_MovieSets.RowCount = 2
        Me.tblFilterMissingItemsTop_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterMissingItemsTop_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsTop_MovieSets.Size = New System.Drawing.Size(170, 20)
        Me.tblFilterMissingItemsTop_MovieSets.TabIndex = 0
        '
        'lblFilterMissingItems_MovieSets
        '
        Me.lblFilterMissingItems_MovieSets.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterMissingItems_MovieSets.AutoSize = True
        Me.lblFilterMissingItems_MovieSets.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterMissingItems_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.lblFilterMissingItemsClose_MovieSets.AutoSize = True
        Me.lblFilterMissingItemsClose_MovieSets.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterMissingItemsClose_MovieSets.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterMissingItemsClose_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilterMissingItemsClose_MovieSets.ForeColor = System.Drawing.Color.White
        Me.lblFilterMissingItemsClose_MovieSets.Location = New System.Drawing.Point(132, 3)
        Me.lblFilterMissingItemsClose_MovieSets.Name = "lblFilterMissingItemsClose_MovieSets"
        Me.lblFilterMissingItemsClose_MovieSets.Size = New System.Drawing.Size(35, 13)
        Me.lblFilterMissingItemsClose_MovieSets.TabIndex = 24
        Me.lblFilterMissingItemsClose_MovieSets.Text = "Close"
        '
        'pnlFilterMissingItems_Shows
        '
        Me.pnlFilterMissingItems_Shows.AutoSize = True
        Me.pnlFilterMissingItems_Shows.Controls.Add(Me.pnlFilterMissingItemsMain_Shows)
        Me.pnlFilterMissingItems_Shows.Controls.Add(Me.pnlFilterMissingItemsTop_Shows)
        Me.pnlFilterMissingItems_Shows.Location = New System.Drawing.Point(0, 900)
        Me.pnlFilterMissingItems_Shows.Name = "pnlFilterMissingItems_Shows"
        Me.pnlFilterMissingItems_Shows.Size = New System.Drawing.Size(170, 252)
        Me.pnlFilterMissingItems_Shows.TabIndex = 29
        Me.pnlFilterMissingItems_Shows.Visible = False
        '
        'pnlFilterMissingItemsMain_Shows
        '
        Me.pnlFilterMissingItemsMain_Shows.AutoSize = True
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
        Me.tblFilterMissingItemsMain_Shows.AutoSize = True
        Me.tblFilterMissingItemsMain_Shows.ColumnCount = 2
        Me.tblFilterMissingItemsMain_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsMain_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsMain_Shows.Controls.Add(Me.chkShowMissingBanner, 0, 0)
        Me.tblFilterMissingItemsMain_Shows.Controls.Add(Me.chkShowMissingClearArt, 0, 2)
        Me.tblFilterMissingItemsMain_Shows.Controls.Add(Me.chkShowMissingClearLogo, 0, 3)
        Me.tblFilterMissingItemsMain_Shows.Controls.Add(Me.chkShowMissingExtrafanarts, 0, 4)
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
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterMissingItemsMain_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterMissingItemsMain_Shows.Size = New System.Drawing.Size(168, 230)
        Me.tblFilterMissingItemsMain_Shows.TabIndex = 0
        '
        'chkShowMissingBanner
        '
        Me.chkShowMissingBanner.AutoSize = True
        Me.chkShowMissingBanner.Location = New System.Drawing.Point(3, 3)
        Me.chkShowMissingBanner.Name = "chkShowMissingBanner"
        Me.chkShowMissingBanner.Size = New System.Drawing.Size(63, 17)
        Me.chkShowMissingBanner.TabIndex = 0
        Me.chkShowMissingBanner.Text = "Banner"
        Me.chkShowMissingBanner.UseVisualStyleBackColor = True
        '
        'chkShowMissingClearArt
        '
        Me.chkShowMissingClearArt.AutoSize = True
        Me.chkShowMissingClearArt.Location = New System.Drawing.Point(3, 49)
        Me.chkShowMissingClearArt.Name = "chkShowMissingClearArt"
        Me.chkShowMissingClearArt.Size = New System.Drawing.Size(67, 17)
        Me.chkShowMissingClearArt.TabIndex = 0
        Me.chkShowMissingClearArt.Text = "ClearArt"
        Me.chkShowMissingClearArt.UseVisualStyleBackColor = True
        '
        'chkShowMissingClearLogo
        '
        Me.chkShowMissingClearLogo.AutoSize = True
        Me.chkShowMissingClearLogo.Location = New System.Drawing.Point(3, 72)
        Me.chkShowMissingClearLogo.Name = "chkShowMissingClearLogo"
        Me.chkShowMissingClearLogo.Size = New System.Drawing.Size(78, 17)
        Me.chkShowMissingClearLogo.TabIndex = 0
        Me.chkShowMissingClearLogo.Text = "ClearLogo"
        Me.chkShowMissingClearLogo.UseVisualStyleBackColor = True
        '
        'chkShowMissingExtrafanarts
        '
        Me.chkShowMissingExtrafanarts.AutoSize = True
        Me.chkShowMissingExtrafanarts.Location = New System.Drawing.Point(3, 95)
        Me.chkShowMissingExtrafanarts.Name = "chkShowMissingExtrafanarts"
        Me.chkShowMissingExtrafanarts.Size = New System.Drawing.Size(87, 17)
        Me.chkShowMissingExtrafanarts.TabIndex = 0
        Me.chkShowMissingExtrafanarts.Text = "Extrafanarts"
        Me.chkShowMissingExtrafanarts.UseVisualStyleBackColor = True
        '
        'chkShowMissingFanart
        '
        Me.chkShowMissingFanart.AutoSize = True
        Me.chkShowMissingFanart.Location = New System.Drawing.Point(3, 118)
        Me.chkShowMissingFanart.Name = "chkShowMissingFanart"
        Me.chkShowMissingFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkShowMissingFanart.TabIndex = 0
        Me.chkShowMissingFanart.Text = "Fanart"
        Me.chkShowMissingFanart.UseVisualStyleBackColor = True
        '
        'chkShowMissingLandscape
        '
        Me.chkShowMissingLandscape.AutoSize = True
        Me.chkShowMissingLandscape.Location = New System.Drawing.Point(3, 141)
        Me.chkShowMissingLandscape.Name = "chkShowMissingLandscape"
        Me.chkShowMissingLandscape.Size = New System.Drawing.Size(80, 17)
        Me.chkShowMissingLandscape.TabIndex = 0
        Me.chkShowMissingLandscape.Text = "Landscape"
        Me.chkShowMissingLandscape.UseVisualStyleBackColor = True
        '
        'chkShowMissingPoster
        '
        Me.chkShowMissingPoster.AutoSize = True
        Me.chkShowMissingPoster.Location = New System.Drawing.Point(3, 187)
        Me.chkShowMissingPoster.Name = "chkShowMissingPoster"
        Me.chkShowMissingPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkShowMissingPoster.TabIndex = 0
        Me.chkShowMissingPoster.Text = "Poster"
        Me.chkShowMissingPoster.UseVisualStyleBackColor = True
        '
        'chkShowMissingNFO
        '
        Me.chkShowMissingNFO.AutoSize = True
        Me.chkShowMissingNFO.Location = New System.Drawing.Point(3, 164)
        Me.chkShowMissingNFO.Name = "chkShowMissingNFO"
        Me.chkShowMissingNFO.Size = New System.Drawing.Size(49, 17)
        Me.chkShowMissingNFO.TabIndex = 0
        Me.chkShowMissingNFO.Text = "NFO"
        Me.chkShowMissingNFO.UseVisualStyleBackColor = True
        '
        'chkShowMissingCharacterArt
        '
        Me.chkShowMissingCharacterArt.AutoSize = True
        Me.chkShowMissingCharacterArt.Location = New System.Drawing.Point(3, 26)
        Me.chkShowMissingCharacterArt.Name = "chkShowMissingCharacterArt"
        Me.chkShowMissingCharacterArt.Size = New System.Drawing.Size(90, 17)
        Me.chkShowMissingCharacterArt.TabIndex = 0
        Me.chkShowMissingCharacterArt.Text = "CharacterArt"
        Me.chkShowMissingCharacterArt.UseVisualStyleBackColor = True
        '
        'chkShowMissingTheme
        '
        Me.chkShowMissingTheme.AutoSize = True
        Me.chkShowMissingTheme.Location = New System.Drawing.Point(3, 210)
        Me.chkShowMissingTheme.Name = "chkShowMissingTheme"
        Me.chkShowMissingTheme.Size = New System.Drawing.Size(59, 17)
        Me.chkShowMissingTheme.TabIndex = 0
        Me.chkShowMissingTheme.Text = "Theme"
        Me.chkShowMissingTheme.UseVisualStyleBackColor = True
        '
        'pnlFilterMissingItemsTop_Shows
        '
        Me.pnlFilterMissingItemsTop_Shows.AutoSize = True
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
        Me.tblFilterMissingItemsTop_Shows.AutoSize = True
        Me.tblFilterMissingItemsTop_Shows.ColumnCount = 3
        Me.tblFilterMissingItemsTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblFilterMissingItemsTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterMissingItemsTop_Shows.Controls.Add(Me.lblFilterMissingItems_Shows, 0, 0)
        Me.tblFilterMissingItemsTop_Shows.Controls.Add(Me.lblFilterMissingItemsClose_Shows, 2, 0)
        Me.tblFilterMissingItemsTop_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterMissingItemsTop_Shows.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterMissingItemsTop_Shows.Name = "tblFilterMissingItemsTop_Shows"
        Me.tblFilterMissingItemsTop_Shows.RowCount = 2
        Me.tblFilterMissingItemsTop_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterMissingItemsTop_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterMissingItemsTop_Shows.Size = New System.Drawing.Size(170, 20)
        Me.tblFilterMissingItemsTop_Shows.TabIndex = 0
        '
        'lblFilterMissingItems_Shows
        '
        Me.lblFilterMissingItems_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterMissingItems_Shows.AutoSize = True
        Me.lblFilterMissingItems_Shows.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterMissingItems_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.lblFilterMissingItemsClose_Shows.AutoSize = True
        Me.lblFilterMissingItemsClose_Shows.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterMissingItemsClose_Shows.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterMissingItemsClose_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.pnlFilterSources_Movies.Visible = False
        '
        'pnlFilterSourcesMain_Movies
        '
        Me.pnlFilterSourcesMain_Movies.AutoSize = True
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
        Me.clbFilterSources_Movies.CheckOnClick = True
        Me.clbFilterSources_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbFilterSources_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.clbFilterSources_Movies.FormattingEnabled = True
        Me.clbFilterSources_Movies.Location = New System.Drawing.Point(0, 0)
        Me.clbFilterSources_Movies.Name = "clbFilterSources_Movies"
        Me.clbFilterSources_Movies.Size = New System.Drawing.Size(187, 170)
        Me.clbFilterSources_Movies.TabIndex = 8
        Me.clbFilterSources_Movies.TabStop = False
        '
        'pnlFilterSourcesTop_Movies
        '
        Me.pnlFilterSourcesTop_Movies.AutoSize = True
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
        Me.tblFilterSourcesTop_Movies.AutoSize = True
        Me.tblFilterSourcesTop_Movies.ColumnCount = 3
        Me.tblFilterSourcesTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSourcesTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblFilterSourcesTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSourcesTop_Movies.Controls.Add(Me.lblFilterSources_Movies, 0, 0)
        Me.tblFilterSourcesTop_Movies.Controls.Add(Me.lblFilterSourcesClose_Movies, 2, 0)
        Me.tblFilterSourcesTop_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterSourcesTop_Movies.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterSourcesTop_Movies.Name = "tblFilterSourcesTop_Movies"
        Me.tblFilterSourcesTop_Movies.RowCount = 2
        Me.tblFilterSourcesTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterSourcesTop_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSourcesTop_Movies.Size = New System.Drawing.Size(189, 20)
        Me.tblFilterSourcesTop_Movies.TabIndex = 0
        '
        'lblFilterSources_Movies
        '
        Me.lblFilterSources_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterSources_Movies.AutoSize = True
        Me.lblFilterSources_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterSources_Movies.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.lblFilterSourcesClose_Movies.AutoSize = True
        Me.lblFilterSourcesClose_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterSourcesClose_Movies.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterSourcesClose_Movies.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.pnlFilterSources_Shows.Visible = False
        '
        'pnlFilterSourcesMain_Shows
        '
        Me.pnlFilterSourcesMain_Shows.AutoSize = True
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
        Me.clbFilterSource_Shows.CheckOnClick = True
        Me.clbFilterSource_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbFilterSource_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.clbFilterSource_Shows.FormattingEnabled = True
        Me.clbFilterSource_Shows.Location = New System.Drawing.Point(0, 0)
        Me.clbFilterSource_Shows.Margin = New System.Windows.Forms.Padding(0)
        Me.clbFilterSource_Shows.Name = "clbFilterSource_Shows"
        Me.clbFilterSource_Shows.Size = New System.Drawing.Size(187, 170)
        Me.clbFilterSource_Shows.TabIndex = 8
        Me.clbFilterSource_Shows.TabStop = False
        '
        'pnlFilterSourcesTop_Shows
        '
        Me.pnlFilterSourcesTop_Shows.AutoSize = True
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
        Me.tblFilterSourcesTop_Shows.AutoSize = True
        Me.tblFilterSourcesTop_Shows.ColumnCount = 3
        Me.tblFilterSourcesTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSourcesTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblFilterSourcesTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSourcesTop_Shows.Controls.Add(Me.lblFilterSources_Shows, 0, 0)
        Me.tblFilterSourcesTop_Shows.Controls.Add(Me.lblFilterSourcesClose_Shows, 2, 0)
        Me.tblFilterSourcesTop_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterSourcesTop_Shows.Location = New System.Drawing.Point(0, 0)
        Me.tblFilterSourcesTop_Shows.Name = "tblFilterSourcesTop_Shows"
        Me.tblFilterSourcesTop_Shows.RowCount = 2
        Me.tblFilterSourcesTop_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterSourcesTop_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSourcesTop_Shows.Size = New System.Drawing.Size(189, 20)
        Me.tblFilterSourcesTop_Shows.TabIndex = 0
        '
        'lblFilterSources_Shows
        '
        Me.lblFilterSources_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterSources_Shows.AutoSize = True
        Me.lblFilterSources_Shows.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterSources_Shows.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.lblFilterSourcesClose_Shows.AutoSize = True
        Me.lblFilterSourcesClose_Shows.BackColor = System.Drawing.Color.Transparent
        Me.lblFilterSourcesClose_Shows.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblFilterSourcesClose_Shows.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilterSourcesClose_Shows.ForeColor = System.Drawing.Color.White
        Me.lblFilterSourcesClose_Shows.Location = New System.Drawing.Point(153, 3)
        Me.lblFilterSourcesClose_Shows.Name = "lblFilterSourcesClose_Shows"
        Me.lblFilterSourcesClose_Shows.Size = New System.Drawing.Size(33, 13)
        Me.lblFilterSourcesClose_Shows.TabIndex = 24
        Me.lblFilterSourcesClose_Shows.Text = "Close"
        '
        'dgvMovies
        '
        Me.dgvMovies.AllowUserToAddRows = False
        Me.dgvMovies.AllowUserToDeleteRows = False
        Me.dgvMovies.AllowUserToResizeRows = False
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.dgvMovies.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
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
        Me.dgvMovies.Size = New System.Drawing.Size(567, 17)
        Me.dgvMovies.StandardTab = True
        Me.dgvMovies.TabIndex = 0
        '
        'cmnuMovie
        '
        Me.cmnuMovie.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieTitle, Me.cmnuMovieDatabaseSeparator, Me.cmnuMovieReload, Me.cmnuMovieMark, Me.cmnuMovieMarkAs, Me.cmnuMovieWatched, Me.cmnuMovieUnwatched, Me.cmnuMovieLock, Me.cmnuMovieEditSeparator, Me.cmnuMovieEdit, Me.cmnuMovieEditMetaData, Me.cmnuMovieEditGenres, Me.cmnuMovieEditTags, Me.cmnuMovieRescrapeSeparator, Me.cmnuMovieScrape, Me.cmnuMovieScrapeSelected, Me.cmnuMovieScrapeSingleDataField, Me.cmnuMovieChange, Me.cmnuMovieChangeAuto, Me.cmnuMovieLanguage, Me.cmnuMovieSep3, Me.cmnuMovieBrowseIMDB, Me.cmnuMovieBrowseTMDB, Me.cmnuMovieOpenFolder, Me.cmnuMovieRemoveSeparator, Me.cmnuMovieRemove})
        Me.cmnuMovie.Name = "mnuMediaList"
        Me.cmnuMovie.Size = New System.Drawing.Size(249, 496)
        '
        'cmnuMovieTitle
        '
        Me.cmnuMovieTitle.Enabled = False
        Me.cmnuMovieTitle.Image = CType(resources.GetObject("cmnuMovieTitle.Image"), System.Drawing.Image)
        Me.cmnuMovieTitle.Name = "cmnuMovieTitle"
        Me.cmnuMovieTitle.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieTitle.Text = "Title"
        '
        'cmnuMovieDatabaseSeparator
        '
        Me.cmnuMovieDatabaseSeparator.Name = "cmnuMovieDatabaseSeparator"
        Me.cmnuMovieDatabaseSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuMovieReload
        '
        Me.cmnuMovieReload.Image = CType(resources.GetObject("cmnuMovieReload.Image"), System.Drawing.Image)
        Me.cmnuMovieReload.Name = "cmnuMovieReload"
        Me.cmnuMovieReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmnuMovieReload.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieReload.Text = "Reload"
        '
        'cmnuMovieMark
        '
        Me.cmnuMovieMark.Image = CType(resources.GetObject("cmnuMovieMark.Image"), System.Drawing.Image)
        Me.cmnuMovieMark.Name = "cmnuMovieMark"
        Me.cmnuMovieMark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.cmnuMovieMark.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieMark.Text = "Mark"
        '
        'cmnuMovieMarkAs
        '
        Me.cmnuMovieMarkAs.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieMarkAsCustom1, Me.cmnuMovieMarkAsCustom2, Me.cmnuMovieMarkAsCustom3, Me.cmnuMovieMarkAsCustom4})
        Me.cmnuMovieMarkAs.Image = Global.Ember_Media_Manager.My.Resources.Resources.valid2
        Me.cmnuMovieMarkAs.Name = "cmnuMovieMarkAs"
        Me.cmnuMovieMarkAs.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieMarkAs.Text = "Mark as"
        '
        'cmnuMovieMarkAsCustom1
        '
        Me.cmnuMovieMarkAsCustom1.Name = "cmnuMovieMarkAsCustom1"
        Me.cmnuMovieMarkAsCustom1.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D1), System.Windows.Forms.Keys)
        Me.cmnuMovieMarkAsCustom1.Size = New System.Drawing.Size(174, 22)
        Me.cmnuMovieMarkAsCustom1.Text = "Custom #1"
        '
        'cmnuMovieMarkAsCustom2
        '
        Me.cmnuMovieMarkAsCustom2.Name = "cmnuMovieMarkAsCustom2"
        Me.cmnuMovieMarkAsCustom2.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D2), System.Windows.Forms.Keys)
        Me.cmnuMovieMarkAsCustom2.Size = New System.Drawing.Size(174, 22)
        Me.cmnuMovieMarkAsCustom2.Text = "Custom #2"
        '
        'cmnuMovieMarkAsCustom3
        '
        Me.cmnuMovieMarkAsCustom3.Name = "cmnuMovieMarkAsCustom3"
        Me.cmnuMovieMarkAsCustom3.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D3), System.Windows.Forms.Keys)
        Me.cmnuMovieMarkAsCustom3.Size = New System.Drawing.Size(174, 22)
        Me.cmnuMovieMarkAsCustom3.Text = "Custom #3"
        '
        'cmnuMovieMarkAsCustom4
        '
        Me.cmnuMovieMarkAsCustom4.Name = "cmnuMovieMarkAsCustom4"
        Me.cmnuMovieMarkAsCustom4.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D4), System.Windows.Forms.Keys)
        Me.cmnuMovieMarkAsCustom4.Size = New System.Drawing.Size(174, 22)
        Me.cmnuMovieMarkAsCustom4.Text = "Custom #4"
        '
        'cmnuMovieWatched
        '
        Me.cmnuMovieWatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuMovieWatched.Name = "cmnuMovieWatched"
        Me.cmnuMovieWatched.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.cmnuMovieWatched.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieWatched.Text = "Mark as Watched"
        '
        'cmnuMovieUnwatched
        '
        Me.cmnuMovieUnwatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuMovieUnwatched.Name = "cmnuMovieUnwatched"
        Me.cmnuMovieUnwatched.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.cmnuMovieUnwatched.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieUnwatched.Text = "Mark as Unwatched"
        '
        'cmnuMovieLock
        '
        Me.cmnuMovieLock.Image = CType(resources.GetObject("cmnuMovieLock.Image"), System.Drawing.Image)
        Me.cmnuMovieLock.Name = "cmnuMovieLock"
        Me.cmnuMovieLock.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.cmnuMovieLock.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieLock.Text = "Lock"
        '
        'cmnuMovieEditSeparator
        '
        Me.cmnuMovieEditSeparator.Name = "cmnuMovieEditSeparator"
        Me.cmnuMovieEditSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuMovieEdit
        '
        Me.cmnuMovieEdit.Image = CType(resources.GetObject("cmnuMovieEdit.Image"), System.Drawing.Image)
        Me.cmnuMovieEdit.Name = "cmnuMovieEdit"
        Me.cmnuMovieEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.cmnuMovieEdit.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieEdit.Text = "Edit Movie"
        '
        'cmnuMovieEditMetaData
        '
        Me.cmnuMovieEditMetaData.Image = CType(resources.GetObject("cmnuMovieEditMetaData.Image"), System.Drawing.Image)
        Me.cmnuMovieEditMetaData.Name = "cmnuMovieEditMetaData"
        Me.cmnuMovieEditMetaData.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.cmnuMovieEditMetaData.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieEditMetaData.Text = "Edit Meta Data"
        '
        'cmnuMovieEditGenres
        '
        Me.cmnuMovieEditGenres.DropDown = Me.mnuGenres
        Me.cmnuMovieEditGenres.Image = Global.Ember_Media_Manager.My.Resources.Resources.heart
        Me.cmnuMovieEditGenres.Name = "cmnuMovieEditGenres"
        Me.cmnuMovieEditGenres.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieEditGenres.Tag = "movie"
        Me.cmnuMovieEditGenres.Text = "Edit Genres"
        '
        'mnuGenres
        '
        Me.mnuGenres.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuGenresTitleSelect, Me.mnuGenresGenre, Me.mnuGenresSep1, Me.mnuGenresTitleNew, Me.mnuGenresNew, Me.mnuGenresSep2, Me.mnuGenresAdd, Me.mnuGenresSet, Me.mnuGenresRemove})
        Me.mnuGenres.Name = "mnuGenres"
        Me.mnuGenres.OwnerItem = Me.cmnuMovieEditGenres
        Me.mnuGenres.Size = New System.Drawing.Size(196, 178)
        '
        'mnuGenresTitleSelect
        '
        Me.mnuGenresTitleSelect.Enabled = False
        Me.mnuGenresTitleSelect.Name = "mnuGenresTitleSelect"
        Me.mnuGenresTitleSelect.Size = New System.Drawing.Size(195, 22)
        Me.mnuGenresTitleSelect.Text = ">> Select Genre <<"
        '
        'mnuGenresGenre
        '
        Me.mnuGenresGenre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mnuGenresGenre.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.mnuGenresGenre.Name = "mnuGenresGenre"
        Me.mnuGenresGenre.Size = New System.Drawing.Size(135, 23)
        '
        'mnuGenresSep1
        '
        Me.mnuGenresSep1.Name = "mnuGenresSep1"
        Me.mnuGenresSep1.Size = New System.Drawing.Size(192, 6)
        '
        'mnuGenresTitleNew
        '
        Me.mnuGenresTitleNew.Enabled = False
        Me.mnuGenresTitleNew.Name = "mnuGenresTitleNew"
        Me.mnuGenresTitleNew.Size = New System.Drawing.Size(195, 22)
        Me.mnuGenresTitleNew.Text = ">> New Genre <<"
        '
        'mnuGenresNew
        '
        Me.mnuGenresNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.mnuGenresNew.Name = "mnuGenresNew"
        Me.mnuGenresNew.Size = New System.Drawing.Size(135, 23)
        '
        'mnuGenresSep2
        '
        Me.mnuGenresSep2.Name = "mnuGenresSep2"
        Me.mnuGenresSep2.Size = New System.Drawing.Size(192, 6)
        '
        'mnuGenresAdd
        '
        Me.mnuGenresAdd.Name = "mnuGenresAdd"
        Me.mnuGenresAdd.Size = New System.Drawing.Size(195, 22)
        Me.mnuGenresAdd.Text = "Add"
        '
        'mnuGenresSet
        '
        Me.mnuGenresSet.Name = "mnuGenresSet"
        Me.mnuGenresSet.Size = New System.Drawing.Size(195, 22)
        Me.mnuGenresSet.Text = "Set"
        '
        'mnuGenresRemove
        '
        Me.mnuGenresRemove.Name = "mnuGenresRemove"
        Me.mnuGenresRemove.Size = New System.Drawing.Size(195, 22)
        Me.mnuGenresRemove.Text = "Remove"
        '
        'cmnuShowEditGenres
        '
        Me.cmnuShowEditGenres.DropDown = Me.mnuGenres
        Me.cmnuShowEditGenres.Image = Global.Ember_Media_Manager.My.Resources.Resources.heart
        Me.cmnuShowEditGenres.Name = "cmnuShowEditGenres"
        Me.cmnuShowEditGenres.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowEditGenres.Tag = "tvshow"
        Me.cmnuShowEditGenres.Text = "Edit Genres"
        '
        'cmnuMovieEditTags
        '
        Me.cmnuMovieEditTags.DropDown = Me.mnuTags
        Me.cmnuMovieEditTags.Image = Global.Ember_Media_Manager.My.Resources.Resources.MovieSet
        Me.cmnuMovieEditTags.Name = "cmnuMovieEditTags"
        Me.cmnuMovieEditTags.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieEditTags.Tag = "movie"
        Me.cmnuMovieEditTags.Text = "Edit Tags"
        '
        'mnuTags
        '
        Me.mnuTags.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuTagsTitleSelect, Me.mnuTagsTag, Me.mnuTagsSep1, Me.mnuTagsTitleNew, Me.mnuTagsNew, Me.mnuTagsSep2, Me.mnuTagsAdd, Me.mnuTagsSet, Me.mnuTagsRemove})
        Me.mnuTags.Name = "mnuTags"
        Me.mnuTags.OwnerItem = Me.cmnuMovieEditTags
        Me.mnuTags.Size = New System.Drawing.Size(196, 178)
        '
        'mnuTagsTitleSelect
        '
        Me.mnuTagsTitleSelect.Enabled = False
        Me.mnuTagsTitleSelect.Name = "mnuTagsTitleSelect"
        Me.mnuTagsTitleSelect.Size = New System.Drawing.Size(195, 22)
        Me.mnuTagsTitleSelect.Text = ">> Select Tag <<"
        '
        'mnuTagsTag
        '
        Me.mnuTagsTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mnuTagsTag.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.mnuTagsTag.Name = "mnuTagsTag"
        Me.mnuTagsTag.Size = New System.Drawing.Size(135, 23)
        Me.mnuTagsTag.Sorted = True
        '
        'mnuTagsSep1
        '
        Me.mnuTagsSep1.Name = "mnuTagsSep1"
        Me.mnuTagsSep1.Size = New System.Drawing.Size(192, 6)
        '
        'mnuTagsTitleNew
        '
        Me.mnuTagsTitleNew.Enabled = False
        Me.mnuTagsTitleNew.Name = "mnuTagsTitleNew"
        Me.mnuTagsTitleNew.Size = New System.Drawing.Size(195, 22)
        Me.mnuTagsTitleNew.Text = ">> New Tag <<"
        '
        'mnuTagsNew
        '
        Me.mnuTagsNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.mnuTagsNew.Name = "mnuTagsNew"
        Me.mnuTagsNew.Size = New System.Drawing.Size(135, 23)
        '
        'mnuTagsSep2
        '
        Me.mnuTagsSep2.Name = "mnuTagsSep2"
        Me.mnuTagsSep2.Size = New System.Drawing.Size(192, 6)
        '
        'mnuTagsAdd
        '
        Me.mnuTagsAdd.Name = "mnuTagsAdd"
        Me.mnuTagsAdd.Size = New System.Drawing.Size(195, 22)
        Me.mnuTagsAdd.Text = "Add"
        '
        'mnuTagsSet
        '
        Me.mnuTagsSet.Name = "mnuTagsSet"
        Me.mnuTagsSet.Size = New System.Drawing.Size(195, 22)
        Me.mnuTagsSet.Text = "Set"
        '
        'mnuTagsRemove
        '
        Me.mnuTagsRemove.Name = "mnuTagsRemove"
        Me.mnuTagsRemove.Size = New System.Drawing.Size(195, 22)
        Me.mnuTagsRemove.Text = "Remove"
        '
        'cmnuShowEditTags
        '
        Me.cmnuShowEditTags.DropDown = Me.mnuTags
        Me.cmnuShowEditTags.Image = Global.Ember_Media_Manager.My.Resources.Resources.MovieSet
        Me.cmnuShowEditTags.Name = "cmnuShowEditTags"
        Me.cmnuShowEditTags.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowEditTags.Tag = "tvshow"
        Me.cmnuShowEditTags.Text = "Edit Tags"
        '
        'cmnuMovieRescrapeSeparator
        '
        Me.cmnuMovieRescrapeSeparator.Name = "cmnuMovieRescrapeSeparator"
        Me.cmnuMovieRescrapeSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuMovieScrape
        '
        Me.cmnuMovieScrape.Image = CType(resources.GetObject("cmnuMovieScrape.Image"), System.Drawing.Image)
        Me.cmnuMovieScrape.Name = "cmnuMovieScrape"
        Me.cmnuMovieScrape.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.cmnuMovieScrape.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieScrape.Text = "(Re)Scrape Movie"
        '
        'cmnuMovieScrapeSelected
        '
        Me.cmnuMovieScrapeSelected.DropDown = Me.mnuScrapeType
        Me.cmnuMovieScrapeSelected.Image = CType(resources.GetObject("cmnuMovieScrapeSelected.Image"), System.Drawing.Image)
        Me.cmnuMovieScrapeSelected.Name = "cmnuMovieScrapeSelected"
        Me.cmnuMovieScrapeSelected.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieScrapeSelected.Tag = "movie"
        Me.cmnuMovieScrapeSelected.Text = "(Re)Scrape Selected Movies"
        '
        'mnuScrapeType
        '
        Me.mnuScrapeType.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuScrapeTypeAuto, Me.mnuScrapeTypeAsk, Me.mnuScrapeTypeSkip})
        Me.mnuScrapeType.Name = "mnuScrapeType"
        Me.mnuScrapeType.OwnerItem = Me.mnuScrapeSubmenuMarked
        Me.mnuScrapeType.Size = New System.Drawing.Size(272, 70)
        '
        'mnuScrapeTypeAuto
        '
        Me.mnuScrapeTypeAuto.DropDown = Me.mnuScrapeModifier
        Me.mnuScrapeTypeAuto.Name = "mnuScrapeTypeAuto"
        Me.mnuScrapeTypeAuto.Size = New System.Drawing.Size(271, 22)
        Me.mnuScrapeTypeAuto.Tag = "auto"
        Me.mnuScrapeTypeAuto.Text = "Automatic (Force Best Match)"
        '
        'mnuScrapeModifier
        '
        Me.mnuScrapeModifier.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuScrapeModifierAll, Me.mnuScrapeModifierActorthumbs, Me.mnuScrapeModifierBanner, Me.mnuScrapeModifierCharacterArt, Me.mnuScrapeModifierClearArt, Me.mnuScrapeModifierClearLogo, Me.mnuScrapeModifierDiscArt, Me.mnuScrapeModifierExtrafanarts, Me.mnuScrapeModifierExtrathumbs, Me.mnuScrapeModifierFanart, Me.mnuScrapeModifierLandscape, Me.mnuScrapeModifierMetaData, Me.mnuScrapeModifierNFO, Me.mnuScrapeModifierPoster, Me.mnuScrapeModifierTheme, Me.mnuScrapeModifierTrailer})
        Me.mnuScrapeModifier.Name = "mnuScrapeModifier"
        Me.mnuScrapeModifier.OwnerItem = Me.mnuScrapeTypeAsk
        Me.mnuScrapeModifier.Size = New System.Drawing.Size(179, 356)
        '
        'mnuScrapeModifierAll
        '
        Me.mnuScrapeModifierAll.Name = "mnuScrapeModifierAll"
        Me.mnuScrapeModifierAll.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierAll.Tag = "all"
        Me.mnuScrapeModifierAll.Text = "All Items"
        '
        'mnuScrapeModifierActorthumbs
        '
        Me.mnuScrapeModifierActorthumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasActorThumb
        Me.mnuScrapeModifierActorthumbs.Name = "mnuScrapeModifierActorthumbs"
        Me.mnuScrapeModifierActorthumbs.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierActorthumbs.Tag = "actorthumbs"
        Me.mnuScrapeModifierActorthumbs.Text = "Actor Thumbs Only"
        '
        'mnuScrapeModifierBanner
        '
        Me.mnuScrapeModifierBanner.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasBanner
        Me.mnuScrapeModifierBanner.Name = "mnuScrapeModifierBanner"
        Me.mnuScrapeModifierBanner.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierBanner.Tag = "banner"
        Me.mnuScrapeModifierBanner.Text = "Banner Only"
        '
        'mnuScrapeModifierCharacterArt
        '
        Me.mnuScrapeModifierCharacterArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasCharacterArt
        Me.mnuScrapeModifierCharacterArt.Name = "mnuScrapeModifierCharacterArt"
        Me.mnuScrapeModifierCharacterArt.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierCharacterArt.Tag = "clearart"
        Me.mnuScrapeModifierCharacterArt.Text = "CharacterArt Only"
        '
        'mnuScrapeModifierClearArt
        '
        Me.mnuScrapeModifierClearArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearArt
        Me.mnuScrapeModifierClearArt.Name = "mnuScrapeModifierClearArt"
        Me.mnuScrapeModifierClearArt.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierClearArt.Tag = "clearart"
        Me.mnuScrapeModifierClearArt.Text = "ClearArt Only"
        '
        'mnuScrapeModifierClearLogo
        '
        Me.mnuScrapeModifierClearLogo.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasClearLogo
        Me.mnuScrapeModifierClearLogo.Name = "mnuScrapeModifierClearLogo"
        Me.mnuScrapeModifierClearLogo.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierClearLogo.Tag = "clearlogo"
        Me.mnuScrapeModifierClearLogo.Text = "ClearLogo Only"
        '
        'mnuScrapeModifierDiscArt
        '
        Me.mnuScrapeModifierDiscArt.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasDiscArt
        Me.mnuScrapeModifierDiscArt.Name = "mnuScrapeModifierDiscArt"
        Me.mnuScrapeModifierDiscArt.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierDiscArt.Tag = "discart"
        Me.mnuScrapeModifierDiscArt.Text = "DiscArt"
        '
        'mnuScrapeModifierExtrafanarts
        '
        Me.mnuScrapeModifierExtrafanarts.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrafanart
        Me.mnuScrapeModifierExtrafanarts.Name = "mnuScrapeModifierExtrafanarts"
        Me.mnuScrapeModifierExtrafanarts.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierExtrafanarts.Tag = "extrafanarts"
        Me.mnuScrapeModifierExtrafanarts.Text = "Extrafanarts Only"
        '
        'mnuScrapeModifierExtrathumbs
        '
        Me.mnuScrapeModifierExtrathumbs.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasExtrathumb
        Me.mnuScrapeModifierExtrathumbs.Name = "mnuScrapeModifierExtrathumbs"
        Me.mnuScrapeModifierExtrathumbs.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierExtrathumbs.Tag = "extrathumbs"
        Me.mnuScrapeModifierExtrathumbs.Text = "Extrathumbs Only"
        '
        'mnuScrapeModifierFanart
        '
        Me.mnuScrapeModifierFanart.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasFanart
        Me.mnuScrapeModifierFanart.Name = "mnuScrapeModifierFanart"
        Me.mnuScrapeModifierFanart.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierFanart.Tag = "fanart"
        Me.mnuScrapeModifierFanart.Text = "Fanart Only"
        '
        'mnuScrapeModifierLandscape
        '
        Me.mnuScrapeModifierLandscape.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasLandscape
        Me.mnuScrapeModifierLandscape.Name = "mnuScrapeModifierLandscape"
        Me.mnuScrapeModifierLandscape.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierLandscape.Tag = "landscape"
        Me.mnuScrapeModifierLandscape.Text = "Landscape Only"
        '
        'mnuScrapeModifierMetaData
        '
        Me.mnuScrapeModifierMetaData.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasMeta
        Me.mnuScrapeModifierMetaData.Name = "mnuScrapeModifierMetaData"
        Me.mnuScrapeModifierMetaData.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierMetaData.Tag = "metadata"
        Me.mnuScrapeModifierMetaData.Text = "Meta Data Only"
        '
        'mnuScrapeModifierNFO
        '
        Me.mnuScrapeModifierNFO.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasNfo
        Me.mnuScrapeModifierNFO.Name = "mnuScrapeModifierNFO"
        Me.mnuScrapeModifierNFO.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierNFO.Tag = "nfo"
        Me.mnuScrapeModifierNFO.Text = "NFO Only"
        '
        'mnuScrapeModifierPoster
        '
        Me.mnuScrapeModifierPoster.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasPoster
        Me.mnuScrapeModifierPoster.Name = "mnuScrapeModifierPoster"
        Me.mnuScrapeModifierPoster.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierPoster.Tag = "poster"
        Me.mnuScrapeModifierPoster.Text = "Poster Only"
        '
        'mnuScrapeModifierTheme
        '
        Me.mnuScrapeModifierTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTheme
        Me.mnuScrapeModifierTheme.Name = "mnuScrapeModifierTheme"
        Me.mnuScrapeModifierTheme.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierTheme.Tag = "theme"
        Me.mnuScrapeModifierTheme.Text = "Theme Only"
        '
        'mnuScrapeModifierTrailer
        '
        Me.mnuScrapeModifierTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.hasTrailer
        Me.mnuScrapeModifierTrailer.Name = "mnuScrapeModifierTrailer"
        Me.mnuScrapeModifierTrailer.Size = New System.Drawing.Size(178, 22)
        Me.mnuScrapeModifierTrailer.Tag = "trailer"
        Me.mnuScrapeModifierTrailer.Text = "Trailer Only"
        '
        'mnuScrapeTypeSkip
        '
        Me.mnuScrapeTypeSkip.DropDown = Me.mnuScrapeModifier
        Me.mnuScrapeTypeSkip.Name = "mnuScrapeTypeSkip"
        Me.mnuScrapeTypeSkip.Size = New System.Drawing.Size(271, 22)
        Me.mnuScrapeTypeSkip.Tag = "skip"
        Me.mnuScrapeTypeSkip.Text = "Skip (Skip If More Than One Match)"
        '
        'mnuScrapeTypeAsk
        '
        Me.mnuScrapeTypeAsk.DropDown = Me.mnuScrapeModifier
        Me.mnuScrapeTypeAsk.Name = "mnuScrapeTypeAsk"
        Me.mnuScrapeTypeAsk.Size = New System.Drawing.Size(271, 22)
        Me.mnuScrapeTypeAsk.Tag = "ask"
        Me.mnuScrapeTypeAsk.Text = "Ask (Require Input If No Exact Match)"
        '
        'mnuScrapeSubmenuFilter
        '
        Me.mnuScrapeSubmenuFilter.DropDown = Me.mnuScrapeType
        Me.mnuScrapeSubmenuFilter.Name = "mnuScrapeSubmenuFilter"
        Me.mnuScrapeSubmenuFilter.Size = New System.Drawing.Size(167, 22)
        Me.mnuScrapeSubmenuFilter.Tag = "filter"
        Me.mnuScrapeSubmenuFilter.Text = "Current Filter"
        '
        'cmnuMovieScrapeSingleDataField
        '
        Me.cmnuMovieScrapeSingleDataField.DropDown = Me.mnuScrapeOption
        Me.cmnuMovieScrapeSingleDataField.Name = "cmnuMovieScrapeSingleDataField"
        Me.cmnuMovieScrapeSingleDataField.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieScrapeSingleDataField.Tag = "movie"
        Me.cmnuMovieScrapeSingleDataField.Text = "(Re)Scrape Single Data Field"
        '
        'mnuScrapeOption
        '
        Me.mnuScrapeOption.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuScrapeOptionActors, Me.mnuScrapeOptionAired, Me.mnuScrapeOptionCertifications, Me.mnuScrapeOptionCollectionID, Me.mnuScrapeOptionCreators, Me.mnuScrapeOptionCountries, Me.mnuScrapeOptionDirectors, Me.mnuScrapeOptionEpiGuideURL, Me.mnuScrapeOptionGenres, Me.mnuScrapeOptionGuestStars, Me.mnuScrapeOptionMPAA, Me.mnuScrapeOptionOriginalTitle, Me.mnuScrapeOptionPlot, Me.mnuScrapeOptionOutline, Me.mnuScrapeOptionPremiered, Me.mnuScrapeOptionRating, Me.mnuScrapeOptionReleaseDate, Me.mnuScrapeOptionRuntime, Me.mnuScrapeOptionStatus, Me.mnuScrapeOptionStudios, Me.mnuScrapeOptionTagline, Me.mnuScrapeOptionTitle, Me.mnuScrapeOptionTop250, Me.mnuScrapeOptionTrailer, Me.mnuScrapeOptionWriters, Me.mnuScrapeOptionYear})
        Me.mnuScrapeOption.Name = "mnuScrapeOption"
        Me.mnuScrapeOption.OwnerItem = Me.cmnuSeasonScrapeSingleDataField
        Me.mnuScrapeOption.Size = New System.Drawing.Size(174, 576)
        '
        'mnuScrapeOptionActors
        '
        Me.mnuScrapeOptionActors.Name = "mnuScrapeOptionActors"
        Me.mnuScrapeOptionActors.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionActors.Tag = "actors"
        Me.mnuScrapeOptionActors.Text = "Actors"
        '
        'mnuScrapeOptionAired
        '
        Me.mnuScrapeOptionAired.Name = "mnuScrapeOptionAired"
        Me.mnuScrapeOptionAired.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionAired.Tag = "aired"
        Me.mnuScrapeOptionAired.Text = "Aired"
        '
        'mnuScrapeOptionCertifications
        '
        Me.mnuScrapeOptionCertifications.Name = "mnuScrapeOptionCertifications"
        Me.mnuScrapeOptionCertifications.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionCertifications.Tag = "certifications"
        Me.mnuScrapeOptionCertifications.Text = "Certifications"
        '
        'mnuScrapeOptionCollectionID
        '
        Me.mnuScrapeOptionCollectionID.Name = "mnuScrapeOptionCollectionID"
        Me.mnuScrapeOptionCollectionID.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionCollectionID.Tag = "collectionid"
        Me.mnuScrapeOptionCollectionID.Text = "Collection ID"
        '
        'mnuScrapeOptionCreators
        '
        Me.mnuScrapeOptionCreators.Name = "mnuScrapeOptionCreators"
        Me.mnuScrapeOptionCreators.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionCreators.Tag = "creators"
        Me.mnuScrapeOptionCreators.Text = "Creators"
        '
        'mnuScrapeOptionCountries
        '
        Me.mnuScrapeOptionCountries.Name = "mnuScrapeOptionCountries"
        Me.mnuScrapeOptionCountries.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionCountries.Tag = "countries"
        Me.mnuScrapeOptionCountries.Text = "Countries"
        '
        'mnuScrapeOptionDirectors
        '
        Me.mnuScrapeOptionDirectors.Name = "mnuScrapeOptionDirectors"
        Me.mnuScrapeOptionDirectors.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionDirectors.Tag = "directors"
        Me.mnuScrapeOptionDirectors.Text = "Directors"
        '
        'mnuScrapeOptionEpiGuideURL
        '
        Me.mnuScrapeOptionEpiGuideURL.Name = "mnuScrapeOptionEpiGuideURL"
        Me.mnuScrapeOptionEpiGuideURL.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionEpiGuideURL.Tag = "epiguideurl"
        Me.mnuScrapeOptionEpiGuideURL.Text = "Episode Guide URL"
        '
        'mnuScrapeOptionGenres
        '
        Me.mnuScrapeOptionGenres.Name = "mnuScrapeOptionGenres"
        Me.mnuScrapeOptionGenres.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionGenres.Tag = "genres"
        Me.mnuScrapeOptionGenres.Text = "Genres"
        '
        'mnuScrapeOptionGuestStars
        '
        Me.mnuScrapeOptionGuestStars.Name = "mnuScrapeOptionGuestStars"
        Me.mnuScrapeOptionGuestStars.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionGuestStars.Tag = "gueststars"
        Me.mnuScrapeOptionGuestStars.Text = "Guest Stars"
        '
        'mnuScrapeOptionMPAA
        '
        Me.mnuScrapeOptionMPAA.Name = "mnuScrapeOptionMPAA"
        Me.mnuScrapeOptionMPAA.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionMPAA.Tag = "mpaa"
        Me.mnuScrapeOptionMPAA.Text = "MPAA"
        '
        'mnuScrapeOptionOriginalTitle
        '
        Me.mnuScrapeOptionOriginalTitle.Name = "mnuScrapeOptionOriginalTitle"
        Me.mnuScrapeOptionOriginalTitle.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionOriginalTitle.Tag = "originaltitle"
        Me.mnuScrapeOptionOriginalTitle.Text = "Original Title"
        '
        'mnuScrapeOptionPlot
        '
        Me.mnuScrapeOptionPlot.Name = "mnuScrapeOptionPlot"
        Me.mnuScrapeOptionPlot.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionPlot.Tag = "plot"
        Me.mnuScrapeOptionPlot.Text = "Plot"
        '
        'mnuScrapeOptionOutline
        '
        Me.mnuScrapeOptionOutline.Name = "mnuScrapeOptionOutline"
        Me.mnuScrapeOptionOutline.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionOutline.Tag = "outline"
        Me.mnuScrapeOptionOutline.Text = "Plot Outline"
        '
        'mnuScrapeOptionPremiered
        '
        Me.mnuScrapeOptionPremiered.Name = "mnuScrapeOptionPremiered"
        Me.mnuScrapeOptionPremiered.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionPremiered.Tag = "premiered"
        Me.mnuScrapeOptionPremiered.Text = "Premiered"
        '
        'mnuScrapeOptionRating
        '
        Me.mnuScrapeOptionRating.Name = "mnuScrapeOptionRating"
        Me.mnuScrapeOptionRating.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionRating.Tag = "rating"
        Me.mnuScrapeOptionRating.Text = "Rating / Votes"
        '
        'mnuScrapeOptionReleaseDate
        '
        Me.mnuScrapeOptionReleaseDate.Name = "mnuScrapeOptionReleaseDate"
        Me.mnuScrapeOptionReleaseDate.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionReleaseDate.Tag = "releasedate"
        Me.mnuScrapeOptionReleaseDate.Text = "Release Date"
        '
        'mnuScrapeOptionRuntime
        '
        Me.mnuScrapeOptionRuntime.Name = "mnuScrapeOptionRuntime"
        Me.mnuScrapeOptionRuntime.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionRuntime.Tag = "runtime"
        Me.mnuScrapeOptionRuntime.Text = "Runtime"
        '
        'mnuScrapeOptionStatus
        '
        Me.mnuScrapeOptionStatus.Name = "mnuScrapeOptionStatus"
        Me.mnuScrapeOptionStatus.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionStatus.Tag = "status"
        Me.mnuScrapeOptionStatus.Text = "Status"
        '
        'mnuScrapeOptionStudios
        '
        Me.mnuScrapeOptionStudios.Name = "mnuScrapeOptionStudios"
        Me.mnuScrapeOptionStudios.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionStudios.Tag = "studios"
        Me.mnuScrapeOptionStudios.Text = "Studios"
        '
        'mnuScrapeOptionTagline
        '
        Me.mnuScrapeOptionTagline.Name = "mnuScrapeOptionTagline"
        Me.mnuScrapeOptionTagline.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionTagline.Tag = "tagline"
        Me.mnuScrapeOptionTagline.Text = "Tagline"
        '
        'mnuScrapeOptionTitle
        '
        Me.mnuScrapeOptionTitle.Name = "mnuScrapeOptionTitle"
        Me.mnuScrapeOptionTitle.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionTitle.Tag = "title"
        Me.mnuScrapeOptionTitle.Text = "Title"
        '
        'mnuScrapeOptionTop250
        '
        Me.mnuScrapeOptionTop250.Name = "mnuScrapeOptionTop250"
        Me.mnuScrapeOptionTop250.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionTop250.Tag = "top250"
        Me.mnuScrapeOptionTop250.Text = "Top 250"
        '
        'mnuScrapeOptionTrailer
        '
        Me.mnuScrapeOptionTrailer.Name = "mnuScrapeOptionTrailer"
        Me.mnuScrapeOptionTrailer.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionTrailer.Tag = "trailer"
        Me.mnuScrapeOptionTrailer.Text = "Trailer"
        '
        'mnuScrapeOptionWriters
        '
        Me.mnuScrapeOptionWriters.Name = "mnuScrapeOptionWriters"
        Me.mnuScrapeOptionWriters.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionWriters.Tag = "writers"
        Me.mnuScrapeOptionWriters.Text = "Writers"
        '
        'mnuScrapeOptionYear
        '
        Me.mnuScrapeOptionYear.Name = "mnuScrapeOptionYear"
        Me.mnuScrapeOptionYear.Size = New System.Drawing.Size(173, 22)
        Me.mnuScrapeOptionYear.Tag = "year"
        Me.mnuScrapeOptionYear.Text = "Year"
        '
        'cmnuEpisodeScrapeSingleDataField
        '
        Me.cmnuEpisodeScrapeSingleDataField.DropDown = Me.mnuScrapeOption
        Me.cmnuEpisodeScrapeSingleDataField.Name = "cmnuEpisodeScrapeSingleDataField"
        Me.cmnuEpisodeScrapeSingleDataField.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeScrapeSingleDataField.Tag = "tvepisode"
        Me.cmnuEpisodeScrapeSingleDataField.Text = "(Re)Scrape Single Data Field"
        '
        'cmnuMovieChange
        '
        Me.cmnuMovieChange.Image = CType(resources.GetObject("cmnuMovieChange.Image"), System.Drawing.Image)
        Me.cmnuMovieChange.Name = "cmnuMovieChange"
        Me.cmnuMovieChange.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.cmnuMovieChange.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieChange.Text = "Change Movie"
        '
        'cmnuMovieChangeAuto
        '
        Me.cmnuMovieChangeAuto.Image = CType(resources.GetObject("cmnuMovieChangeAuto.Image"), System.Drawing.Image)
        Me.cmnuMovieChangeAuto.Name = "cmnuMovieChangeAuto"
        Me.cmnuMovieChangeAuto.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieChangeAuto.Text = "Change Movie (Auto)"
        '
        'cmnuMovieLanguage
        '
        Me.cmnuMovieLanguage.DropDown = Me.mnuLanguages
        Me.cmnuMovieLanguage.Name = "cmnuMovieLanguage"
        Me.cmnuMovieLanguage.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieLanguage.Tag = "movie"
        Me.cmnuMovieLanguage.Text = "Change Language"
        '
        'mnuLanguages
        '
        Me.mnuLanguages.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuLanguagesTitleSelect, Me.mnuLanguagesLanguage, Me.mnuLanguagesSep1, Me.mnuLanguagesSet})
        Me.mnuLanguages.Name = "mnuLanguages"
        Me.mnuLanguages.OwnerItem = Me.cmnuMovieSetLanguage
        Me.mnuLanguages.Size = New System.Drawing.Size(199, 81)
        '
        'mnuLanguagesTitleSelect
        '
        Me.mnuLanguagesTitleSelect.Enabled = False
        Me.mnuLanguagesTitleSelect.Name = "mnuLanguagesTitleSelect"
        Me.mnuLanguagesTitleSelect.Size = New System.Drawing.Size(198, 22)
        Me.mnuLanguagesTitleSelect.Text = ">> Select Language <<"
        '
        'mnuLanguagesLanguage
        '
        Me.mnuLanguagesLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mnuLanguagesLanguage.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.mnuLanguagesLanguage.Name = "mnuLanguagesLanguage"
        Me.mnuLanguagesLanguage.Size = New System.Drawing.Size(135, 23)
        '
        'mnuLanguagesSep1
        '
        Me.mnuLanguagesSep1.Name = "mnuLanguagesSep1"
        Me.mnuLanguagesSep1.Size = New System.Drawing.Size(195, 6)
        '
        'mnuLanguagesSet
        '
        Me.mnuLanguagesSet.Name = "mnuLanguagesSet"
        Me.mnuLanguagesSet.Size = New System.Drawing.Size(198, 22)
        Me.mnuLanguagesSet.Text = "Set"
        '
        'cmnuShowLanguage
        '
        Me.cmnuShowLanguage.DropDown = Me.mnuLanguages
        Me.cmnuShowLanguage.Name = "cmnuShowLanguage"
        Me.cmnuShowLanguage.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowLanguage.Tag = "tvshow"
        Me.cmnuShowLanguage.Text = "Change Language"
        '
        'cmnuMovieSep3
        '
        Me.cmnuMovieSep3.Name = "cmnuMovieSep3"
        Me.cmnuMovieSep3.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuMovieBrowseIMDB
        '
        Me.cmnuMovieBrowseIMDB.Image = Global.Ember_Media_Manager.My.Resources.Resources.imdb
        Me.cmnuMovieBrowseIMDB.Name = "cmnuMovieBrowseIMDB"
        Me.cmnuMovieBrowseIMDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.cmnuMovieBrowseIMDB.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieBrowseIMDB.Text = "Open IMDB-Page"
        '
        'cmnuMovieBrowseTMDB
        '
        Me.cmnuMovieBrowseTMDB.Image = Global.Ember_Media_Manager.My.Resources.Resources.tmdb
        Me.cmnuMovieBrowseTMDB.Name = "cmnuMovieBrowseTMDB"
        Me.cmnuMovieBrowseTMDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.cmnuMovieBrowseTMDB.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieBrowseTMDB.Text = "Open TMDB-Page"
        '
        'cmnuMovieOpenFolder
        '
        Me.cmnuMovieOpenFolder.Image = CType(resources.GetObject("cmnuMovieOpenFolder.Image"), System.Drawing.Image)
        Me.cmnuMovieOpenFolder.Name = "cmnuMovieOpenFolder"
        Me.cmnuMovieOpenFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.cmnuMovieOpenFolder.Size = New System.Drawing.Size(248, 22)
        Me.cmnuMovieOpenFolder.Text = "Open Containing Folder"
        '
        'cmnuMovieRemoveSeparator
        '
        Me.cmnuMovieRemoveSeparator.Name = "cmnuMovieRemoveSeparator"
        Me.cmnuMovieRemoveSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuMovieRemove
        '
        Me.cmnuMovieRemove.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieRemoveFromDB, Me.cmnuMovieRemoveFromDisk})
        Me.cmnuMovieRemove.Image = CType(resources.GetObject("cmnuMovieRemove.Image"), System.Drawing.Image)
        Me.cmnuMovieRemove.Name = "cmnuMovieRemove"
        Me.cmnuMovieRemove.Size = New System.Drawing.Size(248, 22)
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
        'dgvMovieSets
        '
        Me.dgvMovieSets.AllowUserToAddRows = False
        Me.dgvMovieSets.AllowUserToDeleteRows = False
        Me.dgvMovieSets.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.dgvMovieSets.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvMovieSets.BackgroundColor = System.Drawing.Color.White
        Me.dgvMovieSets.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvMovieSets.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvMovieSets.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvMovieSets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMovieSets.ContextMenuStrip = Me.cmnuMovieSet
        Me.dgvMovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMovieSets.GridColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvMovieSets.Location = New System.Drawing.Point(0, 56)
        Me.dgvMovieSets.Name = "dgvMovieSets"
        Me.dgvMovieSets.ReadOnly = True
        Me.dgvMovieSets.RowHeadersVisible = False
        Me.dgvMovieSets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMovieSets.ShowCellErrors = False
        Me.dgvMovieSets.ShowRowErrors = False
        Me.dgvMovieSets.Size = New System.Drawing.Size(567, 17)
        Me.dgvMovieSets.StandardTab = True
        Me.dgvMovieSets.TabIndex = 17
        '
        'cmnuMovieSet
        '
        Me.cmnuMovieSet.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieSetTitle, Me.cmnuMovieSetDatabaseSeparator, Me.cmnuMovieSetReload, Me.cmnuMovieSetMark, Me.cmnuMovieSetLock, Me.cmnuMovieSetNewSeparator, Me.cmnuMovieSetNew, Me.cmnuMovieSetEditSeparator, Me.cmnuMovieSetEdit, Me.cmnuMovieSetEditSortMethod, Me.cmnuMovieSetScrapeSeparator, Me.cmnuMovieSetScrape, Me.cmnuMovieSetScrapeSelected, Me.cmnuMovieSetScrapeSingleDataField, Me.cmnuMovieSetLanguage, Me.cmnuMovieSetSep3, Me.cmnuMovieSetBrowseTMDB, Me.cmnuMovieSetRemoveSeparator, Me.cmnuMovieSetRemove})
        Me.cmnuMovieSet.Name = "cmnuMovieSets"
        Me.cmnuMovieSet.Size = New System.Drawing.Size(235, 326)
        '
        'cmnuMovieSetTitle
        '
        Me.cmnuMovieSetTitle.Enabled = False
        Me.cmnuMovieSetTitle.Image = CType(resources.GetObject("cmnuMovieSetTitle.Image"), System.Drawing.Image)
        Me.cmnuMovieSetTitle.Name = "cmnuMovieSetTitle"
        Me.cmnuMovieSetTitle.Size = New System.Drawing.Size(234, 22)
        Me.cmnuMovieSetTitle.Text = "Title"
        '
        'cmnuMovieSetDatabaseSeparator
        '
        Me.cmnuMovieSetDatabaseSeparator.Name = "cmnuMovieSetDatabaseSeparator"
        Me.cmnuMovieSetDatabaseSeparator.Size = New System.Drawing.Size(231, 6)
        '
        'cmnuMovieSetReload
        '
        Me.cmnuMovieSetReload.Image = CType(resources.GetObject("cmnuMovieSetReload.Image"), System.Drawing.Image)
        Me.cmnuMovieSetReload.Name = "cmnuMovieSetReload"
        Me.cmnuMovieSetReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmnuMovieSetReload.Size = New System.Drawing.Size(234, 22)
        Me.cmnuMovieSetReload.Text = "Reload"
        '
        'cmnuMovieSetMark
        '
        Me.cmnuMovieSetMark.Image = CType(resources.GetObject("cmnuMovieSetMark.Image"), System.Drawing.Image)
        Me.cmnuMovieSetMark.Name = "cmnuMovieSetMark"
        Me.cmnuMovieSetMark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.cmnuMovieSetMark.Size = New System.Drawing.Size(234, 22)
        Me.cmnuMovieSetMark.Text = "Mark"
        '
        'cmnuMovieSetLock
        '
        Me.cmnuMovieSetLock.Image = CType(resources.GetObject("cmnuMovieSetLock.Image"), System.Drawing.Image)
        Me.cmnuMovieSetLock.Name = "cmnuMovieSetLock"
        Me.cmnuMovieSetLock.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.cmnuMovieSetLock.Size = New System.Drawing.Size(234, 22)
        Me.cmnuMovieSetLock.Text = "Lock"
        '
        'cmnuMovieSetNewSeparator
        '
        Me.cmnuMovieSetNewSeparator.Name = "cmnuMovieSetNewSeparator"
        Me.cmnuMovieSetNewSeparator.Size = New System.Drawing.Size(231, 6)
        '
        'cmnuMovieSetNew
        '
        Me.cmnuMovieSetNew.Image = Global.Ember_Media_Manager.My.Resources.Resources.menuAdd
        Me.cmnuMovieSetNew.Name = "cmnuMovieSetNew"
        Me.cmnuMovieSetNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.cmnuMovieSetNew.Size = New System.Drawing.Size(234, 22)
        Me.cmnuMovieSetNew.Text = "Add New MovieSet"
        '
        'cmnuMovieSetEditSeparator
        '
        Me.cmnuMovieSetEditSeparator.Name = "cmnuMovieSetEditSeparator"
        Me.cmnuMovieSetEditSeparator.Size = New System.Drawing.Size(231, 6)
        '
        'cmnuMovieSetEdit
        '
        Me.cmnuMovieSetEdit.Image = CType(resources.GetObject("cmnuMovieSetEdit.Image"), System.Drawing.Image)
        Me.cmnuMovieSetEdit.Name = "cmnuMovieSetEdit"
        Me.cmnuMovieSetEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.cmnuMovieSetEdit.Size = New System.Drawing.Size(234, 22)
        Me.cmnuMovieSetEdit.Text = "Edit MovieSet"
        '
        'cmnuMovieSetEditSortMethod
        '
        Me.cmnuMovieSetEditSortMethod.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuMovieSetEditSortMethodMethods, Me.cmnuMovieSetEditSortMethodSet})
        Me.cmnuMovieSetEditSortMethod.Name = "cmnuMovieSetEditSortMethod"
        Me.cmnuMovieSetEditSortMethod.Size = New System.Drawing.Size(234, 22)
        Me.cmnuMovieSetEditSortMethod.Text = "Edit Movie Sorting"
        '
        'cmnuMovieSetEditSortMethodMethods
        '
        Me.cmnuMovieSetEditSortMethodMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmnuMovieSetEditSortMethodMethods.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.cmnuMovieSetEditSortMethodMethods.Name = "cmnuMovieSetEditSortMethodMethods"
        Me.cmnuMovieSetEditSortMethodMethods.Size = New System.Drawing.Size(121, 23)
        '
        'cmnuMovieSetEditSortMethodSet
        '
        Me.cmnuMovieSetEditSortMethodSet.Name = "cmnuMovieSetEditSortMethodSet"
        Me.cmnuMovieSetEditSortMethodSet.Size = New System.Drawing.Size(181, 22)
        Me.cmnuMovieSetEditSortMethodSet.Text = "Set"
        '
        'cmnuMovieSetScrapeSeparator
        '
        Me.cmnuMovieSetScrapeSeparator.Name = "cmnuMovieSetScrapeSeparator"
        Me.cmnuMovieSetScrapeSeparator.Size = New System.Drawing.Size(231, 6)
        '
        'cmnuMovieSetScrape
        '
        Me.cmnuMovieSetScrape.Image = CType(resources.GetObject("cmnuMovieSetScrape.Image"), System.Drawing.Image)
        Me.cmnuMovieSetScrape.Name = "cmnuMovieSetScrape"
        Me.cmnuMovieSetScrape.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.cmnuMovieSetScrape.Size = New System.Drawing.Size(234, 22)
        Me.cmnuMovieSetScrape.Text = "(Re)Scrape MovieSet"
        '
        'cmnuMovieSetScrapeSelected
        '
        Me.cmnuMovieSetScrapeSelected.DropDown = Me.mnuScrapeType
        Me.cmnuMovieSetScrapeSelected.Image = CType(resources.GetObject("cmnuMovieSetScrapeSelected.Image"), System.Drawing.Image)
        Me.cmnuMovieSetScrapeSelected.Name = "cmnuMovieSetScrapeSelected"
        Me.cmnuMovieSetScrapeSelected.Size = New System.Drawing.Size(234, 22)
        Me.cmnuMovieSetScrapeSelected.Tag = "movieset"
        Me.cmnuMovieSetScrapeSelected.Text = "(Re)Scrape Selected MovieSets"
        '
        'cmnuMovieSetScrapeSingleDataField
        '
        Me.cmnuMovieSetScrapeSingleDataField.DropDown = Me.mnuScrapeOption
        Me.cmnuMovieSetScrapeSingleDataField.Name = "cmnuMovieSetScrapeSingleDataField"
        Me.cmnuMovieSetScrapeSingleDataField.Size = New System.Drawing.Size(234, 22)
        Me.cmnuMovieSetScrapeSingleDataField.Tag = "movieset"
        Me.cmnuMovieSetScrapeSingleDataField.Text = "(Re)Scrape Single Data Field"
        '
        'cmnuMovieSetLanguage
        '
        Me.cmnuMovieSetLanguage.DropDown = Me.mnuLanguages
        Me.cmnuMovieSetLanguage.Name = "cmnuMovieSetLanguage"
        Me.cmnuMovieSetLanguage.Size = New System.Drawing.Size(234, 22)
        Me.cmnuMovieSetLanguage.Tag = "movieset"
        Me.cmnuMovieSetLanguage.Text = "Change Language"
        '
        'cmnuMovieSetSep3
        '
        Me.cmnuMovieSetSep3.Name = "cmnuMovieSetSep3"
        Me.cmnuMovieSetSep3.Size = New System.Drawing.Size(231, 6)
        '
        'cmnuMovieSetBrowseTMDB
        '
        Me.cmnuMovieSetBrowseTMDB.Image = Global.Ember_Media_Manager.My.Resources.Resources.tmdb
        Me.cmnuMovieSetBrowseTMDB.Name = "cmnuMovieSetBrowseTMDB"
        Me.cmnuMovieSetBrowseTMDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.cmnuMovieSetBrowseTMDB.Size = New System.Drawing.Size(234, 22)
        Me.cmnuMovieSetBrowseTMDB.Text = "Open TMDB-Page"
        '
        'cmnuMovieSetRemoveSeparator
        '
        Me.cmnuMovieSetRemoveSeparator.Name = "cmnuMovieSetRemoveSeparator"
        Me.cmnuMovieSetRemoveSeparator.Size = New System.Drawing.Size(231, 6)
        '
        'cmnuMovieSetRemove
        '
        Me.cmnuMovieSetRemove.Image = CType(resources.GetObject("cmnuMovieSetRemove.Image"), System.Drawing.Image)
        Me.cmnuMovieSetRemove.Name = "cmnuMovieSetRemove"
        Me.cmnuMovieSetRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.cmnuMovieSetRemove.Size = New System.Drawing.Size(234, 22)
        Me.cmnuMovieSetRemove.Text = "Remove"
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
        Me.scTV.Size = New System.Drawing.Size(567, 17)
        Me.scTV.SplitterDistance = 25
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
        Me.dgvTVShows.Size = New System.Drawing.Size(567, 23)
        Me.dgvTVShows.StandardTab = True
        Me.dgvTVShows.TabIndex = 0
        '
        'cmnuShow
        '
        Me.cmnuShow.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuShowTitle, Me.cmnuShowDatabaseSeparator, Me.cmnuShowReload, Me.cmnuShowReloadFull, Me.cmnuShowMark, Me.cmnuShowWatched, Me.cmnuShowUnwatched, Me.cmnuShowLock, Me.cmnuShowEditSeparator, Me.cmnuShowEdit, Me.cmnuShowEditGenres, Me.cmnuShowEditTags, Me.cmnuShowScrapeSeparator, Me.cmnuShowScrape, Me.cmnuShowScrapeSelected, Me.cmnuShowScrapeSingleDataField, Me.cmnuShowScrapeRefreshData, Me.cmnuShowChange, Me.cmnuShowLanguage, Me.cmnuShowSep3, Me.cmnuShowBrowseIMDB, Me.cmnuShowBrowseTMDB, Me.cmnuShowBrowseTVDB, Me.cmnuShowOpenFolder, Me.cmnuShowSep4, Me.cmnuShowClearCache, Me.cmnuShowRemoveSeparator, Me.cmnuShowRemove})
        Me.cmnuShow.Name = "mnuShows"
        Me.cmnuShow.Size = New System.Drawing.Size(249, 524)
        '
        'cmnuShowTitle
        '
        Me.cmnuShowTitle.Enabled = False
        Me.cmnuShowTitle.Image = CType(resources.GetObject("cmnuShowTitle.Image"), System.Drawing.Image)
        Me.cmnuShowTitle.Name = "cmnuShowTitle"
        Me.cmnuShowTitle.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowTitle.Text = "Title"
        '
        'cmnuShowDatabaseSeparator
        '
        Me.cmnuShowDatabaseSeparator.Name = "cmnuShowDatabaseSeparator"
        Me.cmnuShowDatabaseSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuShowReload
        '
        Me.cmnuShowReload.Image = CType(resources.GetObject("cmnuShowReload.Image"), System.Drawing.Image)
        Me.cmnuShowReload.Name = "cmnuShowReload"
        Me.cmnuShowReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmnuShowReload.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowReload.Text = "Reload"
        '
        'cmnuShowReloadFull
        '
        Me.cmnuShowReloadFull.Image = CType(resources.GetObject("cmnuShowReloadFull.Image"), System.Drawing.Image)
        Me.cmnuShowReloadFull.Name = "cmnuShowReloadFull"
        Me.cmnuShowReloadFull.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F5), System.Windows.Forms.Keys)
        Me.cmnuShowReloadFull.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowReloadFull.Text = "Reload Full"
        '
        'cmnuShowMark
        '
        Me.cmnuShowMark.Image = CType(resources.GetObject("cmnuShowMark.Image"), System.Drawing.Image)
        Me.cmnuShowMark.Name = "cmnuShowMark"
        Me.cmnuShowMark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.cmnuShowMark.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowMark.Text = "Mark"
        '
        'cmnuShowWatched
        '
        Me.cmnuShowWatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuShowWatched.Name = "cmnuShowWatched"
        Me.cmnuShowWatched.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.cmnuShowWatched.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowWatched.Text = "Mark as Watched"
        '
        'cmnuShowUnwatched
        '
        Me.cmnuShowUnwatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuShowUnwatched.Name = "cmnuShowUnwatched"
        Me.cmnuShowUnwatched.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.cmnuShowUnwatched.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowUnwatched.Text = "Mark as Unwatched"
        '
        'cmnuShowLock
        '
        Me.cmnuShowLock.Image = CType(resources.GetObject("cmnuShowLock.Image"), System.Drawing.Image)
        Me.cmnuShowLock.Name = "cmnuShowLock"
        Me.cmnuShowLock.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.cmnuShowLock.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowLock.Text = "Lock"
        '
        'cmnuShowEditSeparator
        '
        Me.cmnuShowEditSeparator.Name = "cmnuShowEditSeparator"
        Me.cmnuShowEditSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuShowEdit
        '
        Me.cmnuShowEdit.Image = CType(resources.GetObject("cmnuShowEdit.Image"), System.Drawing.Image)
        Me.cmnuShowEdit.Name = "cmnuShowEdit"
        Me.cmnuShowEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.cmnuShowEdit.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowEdit.Text = "Edit Show"
        '
        'cmnuShowScrapeSeparator
        '
        Me.cmnuShowScrapeSeparator.Name = "cmnuShowScrapeSeparator"
        Me.cmnuShowScrapeSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuShowScrape
        '
        Me.cmnuShowScrape.Image = CType(resources.GetObject("cmnuShowScrape.Image"), System.Drawing.Image)
        Me.cmnuShowScrape.Name = "cmnuShowScrape"
        Me.cmnuShowScrape.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.cmnuShowScrape.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowScrape.Text = "(Re)Scrape Show"
        '
        'cmnuShowScrapeSelected
        '
        Me.cmnuShowScrapeSelected.DropDown = Me.mnuScrapeType
        Me.cmnuShowScrapeSelected.Image = CType(resources.GetObject("cmnuShowScrapeSelected.Image"), System.Drawing.Image)
        Me.cmnuShowScrapeSelected.Name = "cmnuShowScrapeSelected"
        Me.cmnuShowScrapeSelected.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowScrapeSelected.Tag = "tvshow"
        Me.cmnuShowScrapeSelected.Text = "(Re)Scrape Selected Shows"
        '
        'cmnuShowScrapeSingleDataField
        '
        Me.cmnuShowScrapeSingleDataField.DropDown = Me.mnuScrapeOption
        Me.cmnuShowScrapeSingleDataField.Name = "cmnuShowScrapeSingleDataField"
        Me.cmnuShowScrapeSingleDataField.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowScrapeSingleDataField.Tag = "tvshow"
        Me.cmnuShowScrapeSingleDataField.Text = "(Re)Scrape Single Data Field"
        '
        'cmnuShowScrapeRefreshData
        '
        Me.cmnuShowScrapeRefreshData.Name = "cmnuShowScrapeRefreshData"
        Me.cmnuShowScrapeRefreshData.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.cmnuShowScrapeRefreshData.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowScrapeRefreshData.Text = "Refresh Data"
        '
        'cmnuShowChange
        '
        Me.cmnuShowChange.Image = CType(resources.GetObject("cmnuShowChange.Image"), System.Drawing.Image)
        Me.cmnuShowChange.Name = "cmnuShowChange"
        Me.cmnuShowChange.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.cmnuShowChange.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowChange.Text = "Change Show"
        '
        'cmnuShowSep3
        '
        Me.cmnuShowSep3.Name = "cmnuShowSep3"
        Me.cmnuShowSep3.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuShowBrowseIMDB
        '
        Me.cmnuShowBrowseIMDB.Image = Global.Ember_Media_Manager.My.Resources.Resources.imdb
        Me.cmnuShowBrowseIMDB.Name = "cmnuShowBrowseIMDB"
        Me.cmnuShowBrowseIMDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.cmnuShowBrowseIMDB.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowBrowseIMDB.Text = "Open IMDB-Page"
        '
        'cmnuShowBrowseTMDB
        '
        Me.cmnuShowBrowseTMDB.Image = Global.Ember_Media_Manager.My.Resources.Resources.tmdb
        Me.cmnuShowBrowseTMDB.Name = "cmnuShowBrowseTMDB"
        Me.cmnuShowBrowseTMDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.cmnuShowBrowseTMDB.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowBrowseTMDB.Text = "Open TMDB-Page"
        '
        'cmnuShowBrowseTVDB
        '
        Me.cmnuShowBrowseTVDB.Image = Global.Ember_Media_Manager.My.Resources.Resources.tvdb
        Me.cmnuShowBrowseTVDB.Name = "cmnuShowBrowseTVDB"
        Me.cmnuShowBrowseTVDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.cmnuShowBrowseTVDB.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowBrowseTVDB.Text = "Open TVDB-Page"
        '
        'cmnuShowOpenFolder
        '
        Me.cmnuShowOpenFolder.Image = CType(resources.GetObject("cmnuShowOpenFolder.Image"), System.Drawing.Image)
        Me.cmnuShowOpenFolder.Name = "cmnuShowOpenFolder"
        Me.cmnuShowOpenFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.cmnuShowOpenFolder.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowOpenFolder.Text = "Open Containing Folder"
        '
        'cmnuShowSep4
        '
        Me.cmnuShowSep4.Name = "cmnuShowSep4"
        Me.cmnuShowSep4.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuShowClearCache
        '
        Me.cmnuShowClearCache.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuShowClearCacheDataAndImages, Me.cmnuShowClearCacheSeparator, Me.cmnuShowClearCacheDataOnly, Me.cmnuShowClearCacheImagesOnly})
        Me.cmnuShowClearCache.Image = CType(resources.GetObject("cmnuShowClearCache.Image"), System.Drawing.Image)
        Me.cmnuShowClearCache.Name = "cmnuShowClearCache"
        Me.cmnuShowClearCache.Size = New System.Drawing.Size(248, 22)
        Me.cmnuShowClearCache.Text = "Clear Cache"
        '
        'cmnuShowClearCacheDataAndImages
        '
        Me.cmnuShowClearCacheDataAndImages.Name = "cmnuShowClearCacheDataAndImages"
        Me.cmnuShowClearCacheDataAndImages.Size = New System.Drawing.Size(162, 22)
        Me.cmnuShowClearCacheDataAndImages.Text = "Data and Images"
        '
        'cmnuShowClearCacheSeparator
        '
        Me.cmnuShowClearCacheSeparator.Name = "cmnuShowClearCacheSeparator"
        Me.cmnuShowClearCacheSeparator.Size = New System.Drawing.Size(159, 6)
        '
        'cmnuShowClearCacheDataOnly
        '
        Me.cmnuShowClearCacheDataOnly.Name = "cmnuShowClearCacheDataOnly"
        Me.cmnuShowClearCacheDataOnly.Size = New System.Drawing.Size(162, 22)
        Me.cmnuShowClearCacheDataOnly.Text = "Data Only"
        '
        'cmnuShowClearCacheImagesOnly
        '
        Me.cmnuShowClearCacheImagesOnly.Name = "cmnuShowClearCacheImagesOnly"
        Me.cmnuShowClearCacheImagesOnly.Size = New System.Drawing.Size(162, 22)
        Me.cmnuShowClearCacheImagesOnly.Text = "Images Only"
        '
        'cmnuShowRemoveSeparator
        '
        Me.cmnuShowRemoveSeparator.Name = "cmnuShowRemoveSeparator"
        Me.cmnuShowRemoveSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuShowRemove
        '
        Me.cmnuShowRemove.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuShowRemoveFromDB, Me.cmnuShowRemoveFromDisk})
        Me.cmnuShowRemove.Image = CType(resources.GetObject("cmnuShowRemove.Image"), System.Drawing.Image)
        Me.cmnuShowRemove.Name = "cmnuShowRemove"
        Me.cmnuShowRemove.Size = New System.Drawing.Size(248, 22)
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
        Me.scTVSeasonsEpisodes.Size = New System.Drawing.Size(567, 25)
        Me.scTVSeasonsEpisodes.SplitterDistance = 25
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
        Me.dgvTVSeasons.Size = New System.Drawing.Size(567, 25)
        Me.dgvTVSeasons.StandardTab = True
        Me.dgvTVSeasons.TabIndex = 0
        '
        'cmnuSeason
        '
        Me.cmnuSeason.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuSeasonTitle, Me.cmnuSeasonDatabaseSeparator, Me.cmnuSeasonReload, Me.cmnuSeasonReloadFull, Me.cmnuSeasonMark, Me.cmnuSeasonWatched, Me.cmnuSeasonUnwatched, Me.cmnuSeasonLock, Me.cmnuSeasonEditSeparator, Me.cmnuSeasonEdit, Me.cmnuSeasonScrapeSeparator, Me.cmnuSeasonScrape, Me.cmnuSeasonScrapeSelected, Me.cmnuSeasonScrapeSingleDataField, Me.cmnuSeasonSep3, Me.cmnuSeasonBrowseIMDB, Me.cmnuSeasonBrowseTMDB, Me.cmnuSeasonBrowseTVDB, Me.cmnuSeasonOpenFolder, Me.cmnuSeasonRemoveSeparator, Me.cmnuSeasonRemove})
        Me.cmnuSeason.Name = "mnuSeasons"
        Me.cmnuSeason.Size = New System.Drawing.Size(249, 386)
        '
        'cmnuSeasonTitle
        '
        Me.cmnuSeasonTitle.Enabled = False
        Me.cmnuSeasonTitle.Image = CType(resources.GetObject("cmnuSeasonTitle.Image"), System.Drawing.Image)
        Me.cmnuSeasonTitle.Name = "cmnuSeasonTitle"
        Me.cmnuSeasonTitle.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonTitle.Text = "Title"
        '
        'cmnuSeasonDatabaseSeparator
        '
        Me.cmnuSeasonDatabaseSeparator.Name = "cmnuSeasonDatabaseSeparator"
        Me.cmnuSeasonDatabaseSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuSeasonReload
        '
        Me.cmnuSeasonReload.Image = CType(resources.GetObject("cmnuSeasonReload.Image"), System.Drawing.Image)
        Me.cmnuSeasonReload.Name = "cmnuSeasonReload"
        Me.cmnuSeasonReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmnuSeasonReload.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonReload.Text = "Reload"
        '
        'cmnuSeasonReloadFull
        '
        Me.cmnuSeasonReloadFull.Image = CType(resources.GetObject("cmnuSeasonReloadFull.Image"), System.Drawing.Image)
        Me.cmnuSeasonReloadFull.Name = "cmnuSeasonReloadFull"
        Me.cmnuSeasonReloadFull.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F5), System.Windows.Forms.Keys)
        Me.cmnuSeasonReloadFull.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonReloadFull.Text = "Reload Full"
        '
        'cmnuSeasonMark
        '
        Me.cmnuSeasonMark.Image = CType(resources.GetObject("cmnuSeasonMark.Image"), System.Drawing.Image)
        Me.cmnuSeasonMark.Name = "cmnuSeasonMark"
        Me.cmnuSeasonMark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.cmnuSeasonMark.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonMark.Text = "Mark"
        '
        'cmnuSeasonWatched
        '
        Me.cmnuSeasonWatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuSeasonWatched.Name = "cmnuSeasonWatched"
        Me.cmnuSeasonWatched.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.cmnuSeasonWatched.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonWatched.Text = "Mark as Watched"
        '
        'cmnuSeasonUnwatched
        '
        Me.cmnuSeasonUnwatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuSeasonUnwatched.Name = "cmnuSeasonUnwatched"
        Me.cmnuSeasonUnwatched.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.cmnuSeasonUnwatched.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonUnwatched.Text = "Mark as Unwatched"
        '
        'cmnuSeasonLock
        '
        Me.cmnuSeasonLock.Image = CType(resources.GetObject("cmnuSeasonLock.Image"), System.Drawing.Image)
        Me.cmnuSeasonLock.Name = "cmnuSeasonLock"
        Me.cmnuSeasonLock.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.cmnuSeasonLock.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonLock.Text = "Lock"
        '
        'cmnuSeasonEditSeparator
        '
        Me.cmnuSeasonEditSeparator.Name = "cmnuSeasonEditSeparator"
        Me.cmnuSeasonEditSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuSeasonEdit
        '
        Me.cmnuSeasonEdit.Image = CType(resources.GetObject("cmnuSeasonEdit.Image"), System.Drawing.Image)
        Me.cmnuSeasonEdit.Name = "cmnuSeasonEdit"
        Me.cmnuSeasonEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.cmnuSeasonEdit.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonEdit.Text = "Edit Season"
        '
        'cmnuSeasonScrapeSeparator
        '
        Me.cmnuSeasonScrapeSeparator.Name = "cmnuSeasonScrapeSeparator"
        Me.cmnuSeasonScrapeSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuSeasonScrape
        '
        Me.cmnuSeasonScrape.Image = CType(resources.GetObject("cmnuSeasonScrape.Image"), System.Drawing.Image)
        Me.cmnuSeasonScrape.Name = "cmnuSeasonScrape"
        Me.cmnuSeasonScrape.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.cmnuSeasonScrape.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonScrape.Text = "(Re)Scrape Season"
        '
        'cmnuSeasonScrapeSelected
        '
        Me.cmnuSeasonScrapeSelected.DropDown = Me.mnuScrapeType
        Me.cmnuSeasonScrapeSelected.Image = CType(resources.GetObject("cmnuSeasonScrapeSelected.Image"), System.Drawing.Image)
        Me.cmnuSeasonScrapeSelected.Name = "cmnuSeasonScrapeSelected"
        Me.cmnuSeasonScrapeSelected.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonScrapeSelected.Tag = "tvseason"
        Me.cmnuSeasonScrapeSelected.Text = "(Re)Scrape Selected Seasons"
        '
        'cmnuSeasonScrapeSingleDataField
        '
        Me.cmnuSeasonScrapeSingleDataField.DropDown = Me.mnuScrapeOption
        Me.cmnuSeasonScrapeSingleDataField.Name = "cmnuSeasonScrapeSingleDataField"
        Me.cmnuSeasonScrapeSingleDataField.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonScrapeSingleDataField.Tag = "tvseason"
        Me.cmnuSeasonScrapeSingleDataField.Text = "(Re)Scrape Single Data Field"
        '
        'cmnuSeasonSep3
        '
        Me.cmnuSeasonSep3.Name = "cmnuSeasonSep3"
        Me.cmnuSeasonSep3.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuSeasonBrowseIMDB
        '
        Me.cmnuSeasonBrowseIMDB.Image = Global.Ember_Media_Manager.My.Resources.Resources.imdb
        Me.cmnuSeasonBrowseIMDB.Name = "cmnuSeasonBrowseIMDB"
        Me.cmnuSeasonBrowseIMDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.cmnuSeasonBrowseIMDB.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonBrowseIMDB.Text = "Open IMDB-Page"
        '
        'cmnuSeasonBrowseTMDB
        '
        Me.cmnuSeasonBrowseTMDB.Image = Global.Ember_Media_Manager.My.Resources.Resources.tmdb
        Me.cmnuSeasonBrowseTMDB.Name = "cmnuSeasonBrowseTMDB"
        Me.cmnuSeasonBrowseTMDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.cmnuSeasonBrowseTMDB.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonBrowseTMDB.Text = "Open TMDB-Page"
        '
        'cmnuSeasonBrowseTVDB
        '
        Me.cmnuSeasonBrowseTVDB.Image = Global.Ember_Media_Manager.My.Resources.Resources.tvdb
        Me.cmnuSeasonBrowseTVDB.Name = "cmnuSeasonBrowseTVDB"
        Me.cmnuSeasonBrowseTVDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.cmnuSeasonBrowseTVDB.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonBrowseTVDB.Text = "Open TVDB-Page"
        '
        'cmnuSeasonOpenFolder
        '
        Me.cmnuSeasonOpenFolder.Image = CType(resources.GetObject("cmnuSeasonOpenFolder.Image"), System.Drawing.Image)
        Me.cmnuSeasonOpenFolder.Name = "cmnuSeasonOpenFolder"
        Me.cmnuSeasonOpenFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.cmnuSeasonOpenFolder.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonOpenFolder.Text = "Open Containing Folder"
        '
        'cmnuSeasonRemoveSeparator
        '
        Me.cmnuSeasonRemoveSeparator.Name = "cmnuSeasonRemoveSeparator"
        Me.cmnuSeasonRemoveSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuSeasonRemove
        '
        Me.cmnuSeasonRemove.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuSeasonRemoveFromDB, Me.cmnuSeasonRemoveFromDisk})
        Me.cmnuSeasonRemove.Image = CType(resources.GetObject("cmnuSeasonRemove.Image"), System.Drawing.Image)
        Me.cmnuSeasonRemove.Name = "cmnuSeasonRemove"
        Me.cmnuSeasonRemove.Size = New System.Drawing.Size(248, 22)
        Me.cmnuSeasonRemove.Text = "Remove"
        '
        'cmnuSeasonRemoveFromDB
        '
        Me.cmnuSeasonRemoveFromDB.Image = CType(resources.GetObject("cmnuSeasonRemoveFromDB.Image"), System.Drawing.Image)
        Me.cmnuSeasonRemoveFromDB.Name = "cmnuSeasonRemoveFromDB"
        Me.cmnuSeasonRemoveFromDB.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.cmnuSeasonRemoveFromDB.Size = New System.Drawing.Size(225, 22)
        Me.cmnuSeasonRemoveFromDB.Text = "Remove from Database"
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
        Me.dgvTVEpisodes.Size = New System.Drawing.Size(567, 25)
        Me.dgvTVEpisodes.StandardTab = True
        Me.dgvTVEpisodes.TabIndex = 0
        '
        'cmnuEpisode
        '
        Me.cmnuEpisode.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuEpisodeTitle, Me.cmnuEpisodeDatabaseSeparator, Me.cmnuEpisodeReload, Me.cmnuEpisodeMark, Me.cmnuEpisodeWatched, Me.cmnuEpisodeUnwatched, Me.cmnuEpisodeLock, Me.cmnuEpisodeEditSeparator, Me.cmnuEpisodeEdit, Me.cmnuEpisodeScrapeSeparator, Me.cmnuEpisodeScrape, Me.cmnuEpisodeScrapeSelected, Me.cmnuEpisodeScrapeSingleDataField, Me.cmnuEpisodeChange, Me.cmnuEpisodeSep3, Me.cmnuEpisodeBrowseIMDB, Me.cmnuEpisodeBrowseTMDB, Me.cmnuEpisodeBrowseTVDB, Me.cmnuEpisodeOpenFolder, Me.cmnuEpisodeRemoveSeparator, Me.cmnuEpisodeRemove})
        Me.cmnuEpisode.Name = "mnuEpisodes"
        Me.cmnuEpisode.Size = New System.Drawing.Size(249, 386)
        '
        'cmnuEpisodeTitle
        '
        Me.cmnuEpisodeTitle.Enabled = False
        Me.cmnuEpisodeTitle.Image = CType(resources.GetObject("cmnuEpisodeTitle.Image"), System.Drawing.Image)
        Me.cmnuEpisodeTitle.Name = "cmnuEpisodeTitle"
        Me.cmnuEpisodeTitle.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeTitle.Text = "Title"
        '
        'cmnuEpisodeDatabaseSeparator
        '
        Me.cmnuEpisodeDatabaseSeparator.Name = "cmnuEpisodeDatabaseSeparator"
        Me.cmnuEpisodeDatabaseSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuEpisodeReload
        '
        Me.cmnuEpisodeReload.Image = CType(resources.GetObject("cmnuEpisodeReload.Image"), System.Drawing.Image)
        Me.cmnuEpisodeReload.Name = "cmnuEpisodeReload"
        Me.cmnuEpisodeReload.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmnuEpisodeReload.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeReload.Text = "Reload"
        '
        'cmnuEpisodeMark
        '
        Me.cmnuEpisodeMark.Image = CType(resources.GetObject("cmnuEpisodeMark.Image"), System.Drawing.Image)
        Me.cmnuEpisodeMark.Name = "cmnuEpisodeMark"
        Me.cmnuEpisodeMark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.cmnuEpisodeMark.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeMark.Text = "Mark"
        '
        'cmnuEpisodeWatched
        '
        Me.cmnuEpisodeWatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuEpisodeWatched.Name = "cmnuEpisodeWatched"
        Me.cmnuEpisodeWatched.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.cmnuEpisodeWatched.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeWatched.Text = "Mark as Watched"
        '
        'cmnuEpisodeUnwatched
        '
        Me.cmnuEpisodeUnwatched.Image = Global.Ember_Media_Manager.My.Resources.Resources.haswatched
        Me.cmnuEpisodeUnwatched.Name = "cmnuEpisodeUnwatched"
        Me.cmnuEpisodeUnwatched.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.cmnuEpisodeUnwatched.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeUnwatched.Text = "Mark as Unwatched"
        '
        'cmnuEpisodeLock
        '
        Me.cmnuEpisodeLock.Image = CType(resources.GetObject("cmnuEpisodeLock.Image"), System.Drawing.Image)
        Me.cmnuEpisodeLock.Name = "cmnuEpisodeLock"
        Me.cmnuEpisodeLock.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.cmnuEpisodeLock.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeLock.Text = "Lock"
        '
        'cmnuEpisodeEditSeparator
        '
        Me.cmnuEpisodeEditSeparator.Name = "cmnuEpisodeEditSeparator"
        Me.cmnuEpisodeEditSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuEpisodeEdit
        '
        Me.cmnuEpisodeEdit.Image = CType(resources.GetObject("cmnuEpisodeEdit.Image"), System.Drawing.Image)
        Me.cmnuEpisodeEdit.Name = "cmnuEpisodeEdit"
        Me.cmnuEpisodeEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.cmnuEpisodeEdit.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeEdit.Text = "Edit Episode"
        '
        'cmnuEpisodeScrapeSeparator
        '
        Me.cmnuEpisodeScrapeSeparator.Name = "cmnuEpisodeScrapeSeparator"
        Me.cmnuEpisodeScrapeSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuEpisodeScrape
        '
        Me.cmnuEpisodeScrape.Image = CType(resources.GetObject("cmnuEpisodeScrape.Image"), System.Drawing.Image)
        Me.cmnuEpisodeScrape.Name = "cmnuEpisodeScrape"
        Me.cmnuEpisodeScrape.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.cmnuEpisodeScrape.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeScrape.Text = "(Re)Scrape Episode"
        '
        'cmnuEpisodeScrapeSelected
        '
        Me.cmnuEpisodeScrapeSelected.DropDown = Me.mnuScrapeType
        Me.cmnuEpisodeScrapeSelected.Image = CType(resources.GetObject("cmnuEpisodeScrapeSelected.Image"), System.Drawing.Image)
        Me.cmnuEpisodeScrapeSelected.Name = "cmnuEpisodeScrapeSelected"
        Me.cmnuEpisodeScrapeSelected.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeScrapeSelected.Tag = "tvepisode"
        Me.cmnuEpisodeScrapeSelected.Text = "(Re)Scrape Selected Episodes"
        '
        'cmnuEpisodeChange
        '
        Me.cmnuEpisodeChange.Image = CType(resources.GetObject("cmnuEpisodeChange.Image"), System.Drawing.Image)
        Me.cmnuEpisodeChange.Name = "cmnuEpisodeChange"
        Me.cmnuEpisodeChange.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.cmnuEpisodeChange.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeChange.Text = "Change Episode"
        '
        'cmnuEpisodeSep3
        '
        Me.cmnuEpisodeSep3.Name = "cmnuEpisodeSep3"
        Me.cmnuEpisodeSep3.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuEpisodeBrowseIMDB
        '
        Me.cmnuEpisodeBrowseIMDB.Image = Global.Ember_Media_Manager.My.Resources.Resources.imdb
        Me.cmnuEpisodeBrowseIMDB.Name = "cmnuEpisodeBrowseIMDB"
        Me.cmnuEpisodeBrowseIMDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.cmnuEpisodeBrowseIMDB.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeBrowseIMDB.Text = "Open IMDB-Page"
        '
        'cmnuEpisodeBrowseTMDB
        '
        Me.cmnuEpisodeBrowseTMDB.Image = Global.Ember_Media_Manager.My.Resources.Resources.tmdb
        Me.cmnuEpisodeBrowseTMDB.Name = "cmnuEpisodeBrowseTMDB"
        Me.cmnuEpisodeBrowseTMDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.cmnuEpisodeBrowseTMDB.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeBrowseTMDB.Text = "Open TMDB-Page"
        '
        'cmnuEpisodeBrowseTVDB
        '
        Me.cmnuEpisodeBrowseTVDB.Image = Global.Ember_Media_Manager.My.Resources.Resources.tvdb
        Me.cmnuEpisodeBrowseTVDB.Name = "cmnuEpisodeBrowseTVDB"
        Me.cmnuEpisodeBrowseTVDB.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.cmnuEpisodeBrowseTVDB.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeBrowseTVDB.Text = "Open TVDB-Page"
        '
        'cmnuEpisodeOpenFolder
        '
        Me.cmnuEpisodeOpenFolder.Image = CType(resources.GetObject("cmnuEpisodeOpenFolder.Image"), System.Drawing.Image)
        Me.cmnuEpisodeOpenFolder.Name = "cmnuEpisodeOpenFolder"
        Me.cmnuEpisodeOpenFolder.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.cmnuEpisodeOpenFolder.Size = New System.Drawing.Size(248, 22)
        Me.cmnuEpisodeOpenFolder.Text = "Open Containing Folder"
        '
        'cmnuEpisodeRemoveSeparator
        '
        Me.cmnuEpisodeRemoveSeparator.Name = "cmnuEpisodeRemoveSeparator"
        Me.cmnuEpisodeRemoveSeparator.Size = New System.Drawing.Size(245, 6)
        '
        'cmnuEpisodeRemove
        '
        Me.cmnuEpisodeRemove.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuEpisodeRemoveFromDB, Me.cmnuEpisodeRemoveFromDisk})
        Me.cmnuEpisodeRemove.Image = CType(resources.GetObject("cmnuEpisodeRemove.Image"), System.Drawing.Image)
        Me.cmnuEpisodeRemove.Name = "cmnuEpisodeRemove"
        Me.cmnuEpisodeRemove.Size = New System.Drawing.Size(248, 22)
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
        Me.pnlListTop.Controls.Add(Me.tblListTop)
        Me.pnlListTop.Controls.Add(Me.pnlSearchMovies)
        Me.pnlListTop.Controls.Add(Me.pnlSearchMovieSets)
        Me.pnlListTop.Controls.Add(Me.pnlSearchTVShows)
        Me.pnlListTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlListTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlListTop.Name = "pnlListTop"
        Me.pnlListTop.Size = New System.Drawing.Size(567, 56)
        Me.pnlListTop.TabIndex = 14
        '
        'tblListTop
        '
        Me.tblListTop.AutoSize = True
        Me.tblListTop.ColumnCount = 2
        Me.tblListTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblListTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblListTop.Controls.Add(Me.tcMain, 0, 0)
        Me.tblListTop.Controls.Add(Me.btnMarkAll, 1, 0)
        Me.tblListTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.tblListTop.Location = New System.Drawing.Point(0, 0)
        Me.tblListTop.Name = "tblListTop"
        Me.tblListTop.RowCount = 1
        Me.tblListTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblListTop.Size = New System.Drawing.Size(567, 20)
        Me.tblListTop.TabIndex = 0
        '
        'tcMain
        '
        Me.tcMain.Controls.Add(Me.tpMovies)
        Me.tcMain.Controls.Add(Me.tpMovieSets)
        Me.tcMain.Controls.Add(Me.tpTVShows)
        Me.tcMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcMain.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tcMain.ItemSize = New System.Drawing.Size(50, 19)
        Me.tcMain.Location = New System.Drawing.Point(0, 0)
        Me.tcMain.Margin = New System.Windows.Forms.Padding(0)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(458, 20)
        Me.tcMain.TabIndex = 0
        Me.tcMain.TabStop = False
        '
        'tpMovies
        '
        Me.tpMovies.Location = New System.Drawing.Point(4, 23)
        Me.tpMovies.Name = "tpMovies"
        Me.tpMovies.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMovies.Size = New System.Drawing.Size(450, 0)
        Me.tpMovies.TabIndex = 0
        Me.tpMovies.Tag = ""
        Me.tpMovies.Text = "Movies"
        Me.tpMovies.UseVisualStyleBackColor = True
        '
        'tpMovieSets
        '
        Me.tpMovieSets.Location = New System.Drawing.Point(4, 23)
        Me.tpMovieSets.Name = "tpMovieSets"
        Me.tpMovieSets.Size = New System.Drawing.Size(450, 0)
        Me.tpMovieSets.TabIndex = 2
        Me.tpMovieSets.Tag = ""
        Me.tpMovieSets.Text = "Sets"
        Me.tpMovieSets.UseVisualStyleBackColor = True
        '
        'tpTVShows
        '
        Me.tpTVShows.Location = New System.Drawing.Point(4, 23)
        Me.tpTVShows.Name = "tpTVShows"
        Me.tpTVShows.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTVShows.Size = New System.Drawing.Size(450, 0)
        Me.tpTVShows.TabIndex = 1
        Me.tpTVShows.Tag = ""
        Me.tpTVShows.Text = "TV Shows"
        Me.tpTVShows.UseVisualStyleBackColor = True
        '
        'btnMarkAll
        '
        Me.btnMarkAll.AutoSize = True
        Me.btnMarkAll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMarkAll.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMarkAll.Image = CType(resources.GetObject("btnMarkAll.Image"), System.Drawing.Image)
        Me.btnMarkAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMarkAll.Location = New System.Drawing.Point(458, 0)
        Me.btnMarkAll.Margin = New System.Windows.Forms.Padding(0)
        Me.btnMarkAll.Name = "btnMarkAll"
        Me.btnMarkAll.Size = New System.Drawing.Size(109, 20)
        Me.btnMarkAll.TabIndex = 1
        Me.btnMarkAll.Text = "Mark All"
        Me.btnMarkAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMarkAll.UseVisualStyleBackColor = True
        '
        'pnlSearchMovies
        '
        Me.pnlSearchMovies.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.cbSearchMovies.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSearchMovies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearchMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSearchMovies.FormattingEnabled = True
        Me.cbSearchMovies.Location = New System.Drawing.Point(437, 5)
        Me.cbSearchMovies.Name = "cbSearchMovies"
        Me.cbSearchMovies.Size = New System.Drawing.Size(100, 21)
        Me.cbSearchMovies.TabIndex = 1
        '
        'picSearchMovies
        '
        Me.picSearchMovies.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picSearchMovies.Image = CType(resources.GetObject("picSearchMovies.Image"), System.Drawing.Image)
        Me.picSearchMovies.Location = New System.Drawing.Point(543, 8)
        Me.picSearchMovies.Name = "picSearchMovies"
        Me.picSearchMovies.Size = New System.Drawing.Size(16, 16)
        Me.picSearchMovies.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picSearchMovies.TabIndex = 1
        Me.picSearchMovies.TabStop = False
        '
        'txtSearchMovies
        '
        Me.txtSearchMovies.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchMovies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSearchMovies.Location = New System.Drawing.Point(7, 4)
        Me.txtSearchMovies.Name = "txtSearchMovies"
        Me.txtSearchMovies.Size = New System.Drawing.Size(424, 22)
        Me.txtSearchMovies.TabIndex = 0
        '
        'pnlSearchMovieSets
        '
        Me.pnlSearchMovieSets.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.cbSearchMovieSets.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSearchMovieSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearchMovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSearchMovieSets.FormattingEnabled = True
        Me.cbSearchMovieSets.Location = New System.Drawing.Point(437, 5)
        Me.cbSearchMovieSets.Name = "cbSearchMovieSets"
        Me.cbSearchMovieSets.Size = New System.Drawing.Size(100, 21)
        Me.cbSearchMovieSets.TabIndex = 1
        '
        'picSearchMovieSets
        '
        Me.picSearchMovieSets.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picSearchMovieSets.Image = CType(resources.GetObject("picSearchMovieSets.Image"), System.Drawing.Image)
        Me.picSearchMovieSets.Location = New System.Drawing.Point(543, 8)
        Me.picSearchMovieSets.Name = "picSearchMovieSets"
        Me.picSearchMovieSets.Size = New System.Drawing.Size(16, 16)
        Me.picSearchMovieSets.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picSearchMovieSets.TabIndex = 1
        Me.picSearchMovieSets.TabStop = False
        '
        'txtSearchMovieSets
        '
        Me.txtSearchMovieSets.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchMovieSets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchMovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSearchMovieSets.Location = New System.Drawing.Point(7, 4)
        Me.txtSearchMovieSets.Name = "txtSearchMovieSets"
        Me.txtSearchMovieSets.Size = New System.Drawing.Size(424, 22)
        Me.txtSearchMovieSets.TabIndex = 0
        '
        'pnlSearchTVShows
        '
        Me.pnlSearchTVShows.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.cbSearchShows.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbSearchShows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearchShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSearchShows.FormattingEnabled = True
        Me.cbSearchShows.Location = New System.Drawing.Point(437, 5)
        Me.cbSearchShows.Name = "cbSearchShows"
        Me.cbSearchShows.Size = New System.Drawing.Size(100, 21)
        Me.cbSearchShows.TabIndex = 1
        '
        'picSearchTVShows
        '
        Me.picSearchTVShows.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picSearchTVShows.Image = CType(resources.GetObject("picSearchTVShows.Image"), System.Drawing.Image)
        Me.picSearchTVShows.Location = New System.Drawing.Point(543, 8)
        Me.picSearchTVShows.Name = "picSearchTVShows"
        Me.picSearchTVShows.Size = New System.Drawing.Size(16, 16)
        Me.picSearchTVShows.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picSearchTVShows.TabIndex = 1
        Me.picSearchTVShows.TabStop = False
        '
        'txtSearchShows
        '
        Me.txtSearchShows.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchShows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSearchShows.Location = New System.Drawing.Point(7, 4)
        Me.txtSearchShows.Name = "txtSearchShows"
        Me.txtSearchShows.Size = New System.Drawing.Size(424, 22)
        Me.txtSearchShows.TabIndex = 0
        '
        'pnlFilter_Movies
        '
        Me.pnlFilter_Movies.AutoSize = True
        Me.pnlFilter_Movies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilter_Movies.Controls.Add(Me.tblFilter_Movies)
        Me.pnlFilter_Movies.Controls.Add(Me.pnlFilterTop_Movies)
        Me.pnlFilter_Movies.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFilter_Movies.Location = New System.Drawing.Point(0, 73)
        Me.pnlFilter_Movies.Name = "pnlFilter_Movies"
        Me.pnlFilter_Movies.Size = New System.Drawing.Size(567, 349)
        Me.pnlFilter_Movies.TabIndex = 12
        Me.pnlFilter_Movies.Visible = False
        '
        'tblFilter_Movies
        '
        Me.tblFilter_Movies.AutoScroll = True
        Me.tblFilter_Movies.AutoSize = True
        Me.tblFilter_Movies.ColumnCount = 3
        Me.tblFilter_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_Movies.Controls.Add(Me.gbFilterGeneral_Movies, 0, 1)
        Me.tblFilter_Movies.Controls.Add(Me.gbFilterSorting_Movies, 0, 3)
        Me.tblFilter_Movies.Controls.Add(Me.btnClearFilters_Movies, 0, 4)
        Me.tblFilter_Movies.Controls.Add(Me.gbFilterSpecific_Movies, 1, 1)
        Me.tblFilter_Movies.Controls.Add(Me.gbFilterLists_Movies, 0, 0)
        Me.tblFilter_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilter_Movies.Location = New System.Drawing.Point(0, 22)
        Me.tblFilter_Movies.Name = "tblFilter_Movies"
        Me.tblFilter_Movies.RowCount = 5
        Me.tblFilter_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Movies.Size = New System.Drawing.Size(565, 325)
        Me.tblFilter_Movies.TabIndex = 8
        '
        'gbFilterGeneral_Movies
        '
        Me.gbFilterGeneral_Movies.AutoSize = True
        Me.gbFilterGeneral_Movies.Controls.Add(Me.tblFilterGeneral_Movies)
        Me.gbFilterGeneral_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterGeneral_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFilterGeneral_Movies.Location = New System.Drawing.Point(3, 57)
        Me.gbFilterGeneral_Movies.Name = "gbFilterGeneral_Movies"
        Me.tblFilter_Movies.SetRowSpan(Me.gbFilterGeneral_Movies, 2)
        Me.gbFilterGeneral_Movies.Size = New System.Drawing.Size(124, 90)
        Me.gbFilterGeneral_Movies.TabIndex = 3
        Me.gbFilterGeneral_Movies.TabStop = False
        Me.gbFilterGeneral_Movies.Text = "General"
        '
        'tblFilterGeneral_Movies
        '
        Me.tblFilterGeneral_Movies.AutoSize = True
        Me.tblFilterGeneral_Movies.ColumnCount = 2
        Me.tblFilterGeneral_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGeneral_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGeneral_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
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
        Me.chkFilterTolerance_Movies.AutoSize = True
        Me.tblFilterGeneral_Movies.SetColumnSpan(Me.chkFilterTolerance_Movies, 2)
        Me.chkFilterTolerance_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterTolerance_Movies.Location = New System.Drawing.Point(3, 49)
        Me.chkFilterTolerance_Movies.Name = "chkFilterTolerance_Movies"
        Me.chkFilterTolerance_Movies.Size = New System.Drawing.Size(112, 17)
        Me.chkFilterTolerance_Movies.TabIndex = 2
        Me.chkFilterTolerance_Movies.Text = "Out of Tolerance"
        Me.chkFilterTolerance_Movies.UseVisualStyleBackColor = True
        '
        'chkFilterDuplicates_Movies
        '
        Me.chkFilterDuplicates_Movies.AutoSize = True
        Me.tblFilterGeneral_Movies.SetColumnSpan(Me.chkFilterDuplicates_Movies, 2)
        Me.chkFilterDuplicates_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterDuplicates_Movies.Location = New System.Drawing.Point(3, 26)
        Me.chkFilterDuplicates_Movies.Name = "chkFilterDuplicates_Movies"
        Me.chkFilterDuplicates_Movies.Size = New System.Drawing.Size(80, 17)
        Me.chkFilterDuplicates_Movies.TabIndex = 0
        Me.chkFilterDuplicates_Movies.Text = "Duplicates"
        Me.chkFilterDuplicates_Movies.UseVisualStyleBackColor = True
        '
        'chkFilterMissing_Movies
        '
        Me.chkFilterMissing_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMissing_Movies.AutoSize = True
        Me.chkFilterMissing_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterMissing_Movies.Location = New System.Drawing.Point(3, 4)
        Me.chkFilterMissing_Movies.Name = "chkFilterMissing_Movies"
        Me.chkFilterMissing_Movies.Size = New System.Drawing.Size(15, 14)
        Me.chkFilterMissing_Movies.TabIndex = 1
        Me.chkFilterMissing_Movies.UseVisualStyleBackColor = True
        '
        'btnFilterMissing_Movies
        '
        Me.btnFilterMissing_Movies.AutoSize = True
        Me.btnFilterMissing_Movies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFilterMissing_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterMissing_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFilterMissing_Movies.Location = New System.Drawing.Point(21, 0)
        Me.btnFilterMissing_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterMissing_Movies.Name = "btnFilterMissing_Movies"
        Me.btnFilterMissing_Movies.Size = New System.Drawing.Size(97, 23)
        Me.btnFilterMissing_Movies.TabIndex = 3
        Me.btnFilterMissing_Movies.Text = "Missing Items"
        Me.btnFilterMissing_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterMissing_Movies.UseVisualStyleBackColor = True
        '
        'gbFilterSorting_Movies
        '
        Me.gbFilterSorting_Movies.AutoSize = True
        Me.gbFilterSorting_Movies.Controls.Add(Me.tblFilterSorting_Movies)
        Me.gbFilterSorting_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterSorting_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFilterSorting_Movies.Location = New System.Drawing.Point(3, 153)
        Me.gbFilterSorting_Movies.Name = "gbFilterSorting_Movies"
        Me.gbFilterSorting_Movies.Size = New System.Drawing.Size(124, 136)
        Me.gbFilterSorting_Movies.TabIndex = 4
        Me.gbFilterSorting_Movies.TabStop = False
        Me.gbFilterSorting_Movies.Text = "Extra Sorting"
        '
        'tblFilterSorting_Movies
        '
        Me.tblFilterSorting_Movies.AutoSize = True
        Me.tblFilterSorting_Movies.ColumnCount = 1
        Me.tblFilterSorting_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSorting_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
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
        Me.btnFilterSortYear_Movies.AutoSize = True
        Me.btnFilterSortYear_Movies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFilterSortYear_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterSortYear_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFilterSortYear_Movies.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFilterSortYear_Movies.Location = New System.Drawing.Point(0, 92)
        Me.btnFilterSortYear_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterSortYear_Movies.Name = "btnFilterSortYear_Movies"
        Me.btnFilterSortYear_Movies.Size = New System.Drawing.Size(118, 23)
        Me.btnFilterSortYear_Movies.TabIndex = 3
        Me.btnFilterSortYear_Movies.Text = "Year"
        Me.btnFilterSortYear_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterSortYear_Movies.UseVisualStyleBackColor = True
        '
        'btnFilterSortRating_Movies
        '
        Me.btnFilterSortRating_Movies.AutoSize = True
        Me.btnFilterSortRating_Movies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFilterSortRating_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterSortRating_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFilterSortRating_Movies.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFilterSortRating_Movies.Location = New System.Drawing.Point(0, 69)
        Me.btnFilterSortRating_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterSortRating_Movies.Name = "btnFilterSortRating_Movies"
        Me.btnFilterSortRating_Movies.Size = New System.Drawing.Size(118, 23)
        Me.btnFilterSortRating_Movies.TabIndex = 2
        Me.btnFilterSortRating_Movies.Text = "Rating"
        Me.btnFilterSortRating_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterSortRating_Movies.UseVisualStyleBackColor = True
        '
        'btnFilterSortDateAdded_Movies
        '
        Me.btnFilterSortDateAdded_Movies.AutoSize = True
        Me.btnFilterSortDateAdded_Movies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFilterSortDateAdded_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterSortDateAdded_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFilterSortDateAdded_Movies.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFilterSortDateAdded_Movies.Location = New System.Drawing.Point(0, 0)
        Me.btnFilterSortDateAdded_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterSortDateAdded_Movies.Name = "btnFilterSortDateAdded_Movies"
        Me.btnFilterSortDateAdded_Movies.Size = New System.Drawing.Size(118, 23)
        Me.btnFilterSortDateAdded_Movies.TabIndex = 0
        Me.btnFilterSortDateAdded_Movies.Text = "Date Added"
        Me.btnFilterSortDateAdded_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterSortDateAdded_Movies.UseVisualStyleBackColor = True
        '
        'btnFilterSortTitle_Movies
        '
        Me.btnFilterSortTitle_Movies.AutoSize = True
        Me.btnFilterSortTitle_Movies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFilterSortTitle_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterSortTitle_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFilterSortTitle_Movies.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFilterSortTitle_Movies.Location = New System.Drawing.Point(0, 46)
        Me.btnFilterSortTitle_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterSortTitle_Movies.Name = "btnFilterSortTitle_Movies"
        Me.btnFilterSortTitle_Movies.Size = New System.Drawing.Size(118, 23)
        Me.btnFilterSortTitle_Movies.TabIndex = 1
        Me.btnFilterSortTitle_Movies.Text = "Sort Title"
        Me.btnFilterSortTitle_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterSortTitle_Movies.UseVisualStyleBackColor = True
        '
        'btnFilterSortDateModified_Movies
        '
        Me.btnFilterSortDateModified_Movies.AutoSize = True
        Me.btnFilterSortDateModified_Movies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFilterSortDateModified_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterSortDateModified_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFilterSortDateModified_Movies.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFilterSortDateModified_Movies.Location = New System.Drawing.Point(0, 23)
        Me.btnFilterSortDateModified_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterSortDateModified_Movies.Name = "btnFilterSortDateModified_Movies"
        Me.btnFilterSortDateModified_Movies.Size = New System.Drawing.Size(118, 23)
        Me.btnFilterSortDateModified_Movies.TabIndex = 1
        Me.btnFilterSortDateModified_Movies.Text = "Date Modified"
        Me.btnFilterSortDateModified_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterSortDateModified_Movies.UseVisualStyleBackColor = True
        '
        'btnClearFilters_Movies
        '
        Me.btnClearFilters_Movies.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnClearFilters_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClearFilters_Movies.Image = CType(resources.GetObject("btnClearFilters_Movies.Image"), System.Drawing.Image)
        Me.btnClearFilters_Movies.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearFilters_Movies.Location = New System.Drawing.Point(19, 295)
        Me.btnClearFilters_Movies.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.btnClearFilters_Movies.Name = "btnClearFilters_Movies"
        Me.btnClearFilters_Movies.Size = New System.Drawing.Size(92, 20)
        Me.btnClearFilters_Movies.TabIndex = 5
        Me.btnClearFilters_Movies.Text = "Clear Filters"
        Me.btnClearFilters_Movies.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClearFilters_Movies.UseVisualStyleBackColor = True
        '
        'gbFilterSpecific_Movies
        '
        Me.gbFilterSpecific_Movies.AutoSize = True
        Me.gbFilterSpecific_Movies.Controls.Add(Me.tblFilterSpecific_Movies)
        Me.gbFilterSpecific_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterSpecific_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFilterSpecific_Movies.Location = New System.Drawing.Point(133, 57)
        Me.gbFilterSpecific_Movies.Name = "gbFilterSpecific_Movies"
        Me.tblFilter_Movies.SetRowSpan(Me.gbFilterSpecific_Movies, 4)
        Me.gbFilterSpecific_Movies.Size = New System.Drawing.Size(405, 265)
        Me.gbFilterSpecific_Movies.TabIndex = 6
        Me.gbFilterSpecific_Movies.TabStop = False
        Me.gbFilterSpecific_Movies.Text = "Specific"
        '
        'tblFilterSpecific_Movies
        '
        Me.tblFilterSpecific_Movies.AutoSize = True
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
        Me.tblFilterSpecific_Movies.RowCount = 10
        Me.tblFilterSpecific_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
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
        Me.gbFilterModifier_Movies.AutoSize = True
        Me.gbFilterModifier_Movies.Controls.Add(Me.tblFilterModifier_Movies)
        Me.gbFilterModifier_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterModifier_Movies.Location = New System.Drawing.Point(3, 3)
        Me.gbFilterModifier_Movies.Name = "gbFilterModifier_Movies"
        Me.gbFilterModifier_Movies.Size = New System.Drawing.Size(102, 44)
        Me.gbFilterModifier_Movies.TabIndex = 3
        Me.gbFilterModifier_Movies.TabStop = False
        Me.gbFilterModifier_Movies.Text = "Modifier"
        '
        'tblFilterModifier_Movies
        '
        Me.tblFilterModifier_Movies.AutoSize = True
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
        Me.rbFilterOr_Movies.AutoSize = True
        Me.rbFilterOr_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbFilterOr_Movies.Location = New System.Drawing.Point(55, 3)
        Me.rbFilterOr_Movies.Name = "rbFilterOr_Movies"
        Me.rbFilterOr_Movies.Size = New System.Drawing.Size(38, 17)
        Me.rbFilterOr_Movies.TabIndex = 1
        Me.rbFilterOr_Movies.Text = "Or"
        Me.rbFilterOr_Movies.UseVisualStyleBackColor = True
        '
        'rbFilterAnd_Movies
        '
        Me.rbFilterAnd_Movies.AutoSize = True
        Me.rbFilterAnd_Movies.Checked = True
        Me.rbFilterAnd_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbFilterAnd_Movies.Location = New System.Drawing.Point(3, 3)
        Me.rbFilterAnd_Movies.Name = "rbFilterAnd_Movies"
        Me.rbFilterAnd_Movies.Size = New System.Drawing.Size(46, 17)
        Me.rbFilterAnd_Movies.TabIndex = 0
        Me.rbFilterAnd_Movies.TabStop = True
        Me.rbFilterAnd_Movies.Text = "And"
        Me.rbFilterAnd_Movies.UseVisualStyleBackColor = True
        '
        'tblFilterSpecificData_Movies
        '
        Me.tblFilterSpecificData_Movies.AutoSize = True
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
        Me.tblFilterSpecificData_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterSpecificData_Movies.Size = New System.Drawing.Size(285, 220)
        Me.tblFilterSpecificData_Movies.TabIndex = 7
        '
        'gbFilterDataField_Movies
        '
        Me.gbFilterDataField_Movies.AutoSize = True
        Me.tblFilterSpecificData_Movies.SetColumnSpan(Me.gbFilterDataField_Movies, 3)
        Me.gbFilterDataField_Movies.Controls.Add(Me.tblFilterDataField_Movies)
        Me.gbFilterDataField_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFilterDataField_Movies.Location = New System.Drawing.Point(3, 168)
        Me.gbFilterDataField_Movies.Name = "gbFilterDataField_Movies"
        Me.gbFilterDataField_Movies.Size = New System.Drawing.Size(279, 49)
        Me.gbFilterDataField_Movies.TabIndex = 39
        Me.gbFilterDataField_Movies.TabStop = False
        Me.gbFilterDataField_Movies.Text = "Data Field:"
        '
        'tblFilterDataField_Movies
        '
        Me.tblFilterDataField_Movies.AutoSize = True
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
        Me.txtFilterDataField_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilterDataField_Movies.Location = New System.Drawing.Point(110, 3)
        Me.txtFilterDataField_Movies.Name = "txtFilterDataField_Movies"
        Me.txtFilterDataField_Movies.ReadOnly = True
        Me.txtFilterDataField_Movies.Size = New System.Drawing.Size(160, 22)
        Me.txtFilterDataField_Movies.TabIndex = 38
        '
        'cbFilterDataField_Movies
        '
        Me.cbFilterDataField_Movies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterDataField_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterDataField_Movies.FormattingEnabled = True
        Me.cbFilterDataField_Movies.Location = New System.Drawing.Point(3, 3)
        Me.cbFilterDataField_Movies.Name = "cbFilterDataField_Movies"
        Me.cbFilterDataField_Movies.Size = New System.Drawing.Size(101, 21)
        Me.cbFilterDataField_Movies.TabIndex = 39
        '
        'lblFilterGenre_Movies
        '
        Me.lblFilterGenre_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterGenre_Movies.AutoSize = True
        Me.lblFilterGenre_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.txtFilterGenre_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilterGenre_Movies.Location = New System.Drawing.Point(87, 3)
        Me.txtFilterGenre_Movies.Name = "txtFilterGenre_Movies"
        Me.txtFilterGenre_Movies.ReadOnly = True
        Me.txtFilterGenre_Movies.Size = New System.Drawing.Size(189, 22)
        Me.txtFilterGenre_Movies.TabIndex = 4
        '
        'lblFilterCountry_Movies
        '
        Me.lblFilterCountry_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterCountry_Movies.AutoSize = True
        Me.lblFilterCountry_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.txtFilterCountry_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilterCountry_Movies.Location = New System.Drawing.Point(87, 31)
        Me.txtFilterCountry_Movies.Name = "txtFilterCountry_Movies"
        Me.txtFilterCountry_Movies.ReadOnly = True
        Me.txtFilterCountry_Movies.Size = New System.Drawing.Size(189, 22)
        Me.txtFilterCountry_Movies.TabIndex = 36
        '
        'lblFilterVideoSource_Movies
        '
        Me.lblFilterVideoSource_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterVideoSource_Movies.AutoSize = True
        Me.lblFilterVideoSource_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.cbFilterVideoSource_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterVideoSource_Movies.FormattingEnabled = True
        Me.cbFilterVideoSource_Movies.Location = New System.Drawing.Point(87, 59)
        Me.cbFilterVideoSource_Movies.Name = "cbFilterVideoSource_Movies"
        Me.cbFilterVideoSource_Movies.Size = New System.Drawing.Size(189, 21)
        Me.cbFilterVideoSource_Movies.TabIndex = 9
        '
        'lblFilterSource_Movies
        '
        Me.lblFilterSource_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterSource_Movies.AutoSize = True
        Me.lblFilterSource_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.txtFilterSource_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilterSource_Movies.Location = New System.Drawing.Point(87, 140)
        Me.txtFilterSource_Movies.Name = "txtFilterSource_Movies"
        Me.txtFilterSource_Movies.ReadOnly = True
        Me.txtFilterSource_Movies.Size = New System.Drawing.Size(189, 22)
        Me.txtFilterSource_Movies.TabIndex = 11
        '
        'cbFilterYearModFrom_Movies
        '
        Me.cbFilterYearModFrom_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbFilterYearModFrom_Movies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterYearModFrom_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterYearModFrom_Movies.FormattingEnabled = True
        Me.cbFilterYearModFrom_Movies.Items.AddRange(New Object() {"=", "<>", ">=", ">", "<=", "<"})
        Me.cbFilterYearModFrom_Movies.Location = New System.Drawing.Point(87, 86)
        Me.cbFilterYearModFrom_Movies.Name = "cbFilterYearModFrom_Movies"
        Me.cbFilterYearModFrom_Movies.Size = New System.Drawing.Size(70, 21)
        Me.cbFilterYearModFrom_Movies.TabIndex = 6
        '
        'lblFilterYear_Movies
        '
        Me.lblFilterYear_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterYear_Movies.AutoSize = True
        Me.lblFilterYear_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.cbFilterYearModTo_Movies.Enabled = False
        Me.cbFilterYearModTo_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbFilterYearModTo_Movies.FormattingEnabled = True
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
        Me.cbFilterYearFrom_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterYearFrom_Movies.FormattingEnabled = True
        Me.cbFilterYearFrom_Movies.Items.AddRange(New Object() {"=", ">", "<", "!="})
        Me.cbFilterYearFrom_Movies.Location = New System.Drawing.Point(163, 86)
        Me.cbFilterYearFrom_Movies.Name = "cbFilterYearFrom_Movies"
        Me.cbFilterYearFrom_Movies.Size = New System.Drawing.Size(113, 21)
        Me.cbFilterYearFrom_Movies.TabIndex = 7
        '
        'cbFilterYearTo_Movies
        '
        Me.cbFilterYearTo_Movies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterYearTo_Movies.Enabled = False
        Me.cbFilterYearTo_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbFilterYearTo_Movies.FormattingEnabled = True
        Me.cbFilterYearTo_Movies.Location = New System.Drawing.Point(163, 113)
        Me.cbFilterYearTo_Movies.Name = "cbFilterYearTo_Movies"
        Me.cbFilterYearTo_Movies.Size = New System.Drawing.Size(113, 21)
        Me.cbFilterYearTo_Movies.TabIndex = 41
        '
        'chkFilterNew_Movies
        '
        Me.chkFilterNew_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterNew_Movies.AutoSize = True
        Me.chkFilterNew_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterNew_Movies.Location = New System.Drawing.Point(3, 53)
        Me.chkFilterNew_Movies.Name = "chkFilterNew_Movies"
        Me.chkFilterNew_Movies.Size = New System.Drawing.Size(49, 17)
        Me.chkFilterNew_Movies.TabIndex = 0
        Me.chkFilterNew_Movies.Text = "New"
        Me.chkFilterNew_Movies.UseVisualStyleBackColor = True
        '
        'chkFilterMark_Movies
        '
        Me.chkFilterMark_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMark_Movies.AutoSize = True
        Me.chkFilterMark_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterMark_Movies.Location = New System.Drawing.Point(3, 76)
        Me.chkFilterMark_Movies.Name = "chkFilterMark_Movies"
        Me.chkFilterMark_Movies.Size = New System.Drawing.Size(65, 17)
        Me.chkFilterMark_Movies.TabIndex = 1
        Me.chkFilterMark_Movies.Text = "Marked"
        Me.chkFilterMark_Movies.UseVisualStyleBackColor = True
        '
        'chkFilterMarkCustom1_Movies
        '
        Me.chkFilterMarkCustom1_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMarkCustom1_Movies.AutoSize = True
        Me.chkFilterMarkCustom1_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterMarkCustom1_Movies.Location = New System.Drawing.Point(3, 99)
        Me.chkFilterMarkCustom1_Movies.Name = "chkFilterMarkCustom1_Movies"
        Me.chkFilterMarkCustom1_Movies.Size = New System.Drawing.Size(81, 17)
        Me.chkFilterMarkCustom1_Movies.TabIndex = 32
        Me.chkFilterMarkCustom1_Movies.Text = "Custom #1"
        Me.chkFilterMarkCustom1_Movies.UseVisualStyleBackColor = True
        '
        'chkFilterMarkCustom4_Movies
        '
        Me.chkFilterMarkCustom4_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMarkCustom4_Movies.AutoSize = True
        Me.chkFilterMarkCustom4_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterMarkCustom4_Movies.Location = New System.Drawing.Point(3, 168)
        Me.chkFilterMarkCustom4_Movies.Name = "chkFilterMarkCustom4_Movies"
        Me.chkFilterMarkCustom4_Movies.Size = New System.Drawing.Size(81, 17)
        Me.chkFilterMarkCustom4_Movies.TabIndex = 35
        Me.chkFilterMarkCustom4_Movies.Text = "Custom #4"
        Me.chkFilterMarkCustom4_Movies.UseVisualStyleBackColor = True
        '
        'chkFilterMarkCustom2_Movies
        '
        Me.chkFilterMarkCustom2_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMarkCustom2_Movies.AutoSize = True
        Me.chkFilterMarkCustom2_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterMarkCustom2_Movies.Location = New System.Drawing.Point(3, 122)
        Me.chkFilterMarkCustom2_Movies.Name = "chkFilterMarkCustom2_Movies"
        Me.chkFilterMarkCustom2_Movies.Size = New System.Drawing.Size(81, 17)
        Me.chkFilterMarkCustom2_Movies.TabIndex = 33
        Me.chkFilterMarkCustom2_Movies.Text = "Custom #2"
        Me.chkFilterMarkCustom2_Movies.UseVisualStyleBackColor = True
        '
        'chkFilterMarkCustom3_Movies
        '
        Me.chkFilterMarkCustom3_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMarkCustom3_Movies.AutoSize = True
        Me.chkFilterMarkCustom3_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterMarkCustom3_Movies.Location = New System.Drawing.Point(3, 145)
        Me.chkFilterMarkCustom3_Movies.Name = "chkFilterMarkCustom3_Movies"
        Me.chkFilterMarkCustom3_Movies.Size = New System.Drawing.Size(81, 17)
        Me.chkFilterMarkCustom3_Movies.TabIndex = 34
        Me.chkFilterMarkCustom3_Movies.Text = "Custom #3"
        Me.chkFilterMarkCustom3_Movies.UseVisualStyleBackColor = True
        '
        'chkFilterLock_Movies
        '
        Me.chkFilterLock_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterLock_Movies.AutoSize = True
        Me.chkFilterLock_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFilterLock_Movies.Location = New System.Drawing.Point(3, 191)
        Me.chkFilterLock_Movies.Name = "chkFilterLock_Movies"
        Me.chkFilterLock_Movies.Size = New System.Drawing.Size(62, 17)
        Me.chkFilterLock_Movies.TabIndex = 2
        Me.chkFilterLock_Movies.Text = "Locked"
        Me.chkFilterLock_Movies.UseVisualStyleBackColor = True
        '
        'gbFilterLists_Movies
        '
        Me.gbFilterLists_Movies.AutoSize = True
        Me.tblFilter_Movies.SetColumnSpan(Me.gbFilterLists_Movies, 2)
        Me.gbFilterLists_Movies.Controls.Add(Me.tblFilterLists_Movies)
        Me.gbFilterLists_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterLists_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbFilterLists_Movies.Location = New System.Drawing.Point(3, 3)
        Me.gbFilterLists_Movies.Name = "gbFilterLists_Movies"
        Me.gbFilterLists_Movies.Size = New System.Drawing.Size(535, 48)
        Me.gbFilterLists_Movies.TabIndex = 7
        Me.gbFilterLists_Movies.TabStop = False
        Me.gbFilterLists_Movies.Text = "Lists"
        '
        'tblFilterLists_Movies
        '
        Me.tblFilterLists_Movies.AutoSize = True
        Me.tblFilterLists_Movies.ColumnCount = 1
        Me.tblFilterLists_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterLists_Movies.Controls.Add(Me.cbFilterLists_Movies, 0, 0)
        Me.tblFilterLists_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterLists_Movies.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterLists_Movies.Name = "tblFilterLists_Movies"
        Me.tblFilterLists_Movies.RowCount = 1
        Me.tblFilterLists_Movies.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterLists_Movies.Size = New System.Drawing.Size(529, 27)
        Me.tblFilterLists_Movies.TabIndex = 0
        '
        'cbFilterLists_Movies
        '
        Me.cbFilterLists_Movies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbFilterLists_Movies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterLists_Movies.Enabled = False
        Me.cbFilterLists_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbFilterLists_Movies.FormattingEnabled = True
        Me.cbFilterLists_Movies.Location = New System.Drawing.Point(3, 3)
        Me.cbFilterLists_Movies.Name = "cbFilterLists_Movies"
        Me.cbFilterLists_Movies.Size = New System.Drawing.Size(523, 21)
        Me.cbFilterLists_Movies.TabIndex = 43
        '
        'pnlFilterTop_Movies
        '
        Me.pnlFilterTop_Movies.AutoSize = True
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
        Me.tblFilterTop_Movies.AutoSize = True
        Me.tblFilterTop_Movies.BackColor = System.Drawing.Color.Transparent
        Me.tblFilterTop_Movies.ColumnCount = 4
        Me.tblFilterTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterTop_Movies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
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
        Me.lblFilter_Movies.AutoSize = True
        Me.lblFilter_Movies.BackColor = System.Drawing.Color.Transparent
        Me.lblFilter_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.btnFilterUp_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFilterUp_Movies.Location = New System.Drawing.Point(505, 0)
        Me.btnFilterUp_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterUp_Movies.Name = "btnFilterUp_Movies"
        Me.btnFilterUp_Movies.Size = New System.Drawing.Size(30, 22)
        Me.btnFilterUp_Movies.TabIndex = 1
        Me.btnFilterUp_Movies.TabStop = False
        Me.btnFilterUp_Movies.Text = "^"
        Me.btnFilterUp_Movies.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFilterUp_Movies.UseVisualStyleBackColor = False
        '
        'btnFilterDown_Movies
        '
        Me.btnFilterDown_Movies.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFilterDown_Movies.BackColor = System.Drawing.SystemColors.Control
        Me.btnFilterDown_Movies.Enabled = False
        Me.btnFilterDown_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFilterDown_Movies.Location = New System.Drawing.Point(535, 0)
        Me.btnFilterDown_Movies.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterDown_Movies.Name = "btnFilterDown_Movies"
        Me.btnFilterDown_Movies.Size = New System.Drawing.Size(30, 22)
        Me.btnFilterDown_Movies.TabIndex = 2
        Me.btnFilterDown_Movies.TabStop = False
        Me.btnFilterDown_Movies.Text = "v"
        Me.btnFilterDown_Movies.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFilterDown_Movies.UseVisualStyleBackColor = False
        '
        'pnlFilter_MovieSets
        '
        Me.pnlFilter_MovieSets.AutoSize = True
        Me.pnlFilter_MovieSets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilter_MovieSets.Controls.Add(Me.tblFilter_MovieSets)
        Me.pnlFilter_MovieSets.Controls.Add(Me.pnlFilterTop_MovieSets)
        Me.pnlFilter_MovieSets.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFilter_MovieSets.Location = New System.Drawing.Point(0, 422)
        Me.pnlFilter_MovieSets.Name = "pnlFilter_MovieSets"
        Me.pnlFilter_MovieSets.Size = New System.Drawing.Size(567, 230)
        Me.pnlFilter_MovieSets.TabIndex = 26
        Me.pnlFilter_MovieSets.Visible = False
        '
        'tblFilter_MovieSets
        '
        Me.tblFilter_MovieSets.AutoScroll = True
        Me.tblFilter_MovieSets.AutoSize = True
        Me.tblFilter_MovieSets.ColumnCount = 3
        Me.tblFilter_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_MovieSets.Controls.Add(Me.gbFilterLists_MovieSets, 0, 0)
        Me.tblFilter_MovieSets.Controls.Add(Me.gbFilterGeneral_MovieSets, 0, 1)
        Me.tblFilter_MovieSets.Controls.Add(Me.gbFilterSpecific_MovieSets, 1, 1)
        Me.tblFilter_MovieSets.Controls.Add(Me.btnClearFilters_MovieSets, 0, 2)
        Me.tblFilter_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilter_MovieSets.Location = New System.Drawing.Point(0, 22)
        Me.tblFilter_MovieSets.Name = "tblFilter_MovieSets"
        Me.tblFilter_MovieSets.RowCount = 4
        Me.tblFilter_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_MovieSets.Size = New System.Drawing.Size(565, 206)
        Me.tblFilter_MovieSets.TabIndex = 8
        '
        'gbFilterLists_MovieSets
        '
        Me.gbFilterLists_MovieSets.AutoSize = True
        Me.tblFilter_MovieSets.SetColumnSpan(Me.gbFilterLists_MovieSets, 2)
        Me.gbFilterLists_MovieSets.Controls.Add(Me.tblFilterLists_MovieSets)
        Me.gbFilterLists_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterLists_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbFilterLists_MovieSets.Location = New System.Drawing.Point(3, 3)
        Me.gbFilterLists_MovieSets.Name = "gbFilterLists_MovieSets"
        Me.gbFilterLists_MovieSets.Size = New System.Drawing.Size(535, 48)
        Me.gbFilterLists_MovieSets.TabIndex = 8
        Me.gbFilterLists_MovieSets.TabStop = False
        Me.gbFilterLists_MovieSets.Text = "Lists"
        '
        'tblFilterLists_MovieSets
        '
        Me.tblFilterLists_MovieSets.AutoSize = True
        Me.tblFilterLists_MovieSets.ColumnCount = 1
        Me.tblFilterLists_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterLists_MovieSets.Controls.Add(Me.cbFilterLists_MovieSets, 0, 0)
        Me.tblFilterLists_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterLists_MovieSets.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterLists_MovieSets.Name = "tblFilterLists_MovieSets"
        Me.tblFilterLists_MovieSets.RowCount = 1
        Me.tblFilterLists_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterLists_MovieSets.Size = New System.Drawing.Size(529, 27)
        Me.tblFilterLists_MovieSets.TabIndex = 0
        '
        'cbFilterLists_MovieSets
        '
        Me.cbFilterLists_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbFilterLists_MovieSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterLists_MovieSets.Enabled = False
        Me.cbFilterLists_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbFilterLists_MovieSets.FormattingEnabled = True
        Me.cbFilterLists_MovieSets.Location = New System.Drawing.Point(3, 3)
        Me.cbFilterLists_MovieSets.Name = "cbFilterLists_MovieSets"
        Me.cbFilterLists_MovieSets.Size = New System.Drawing.Size(523, 21)
        Me.cbFilterLists_MovieSets.TabIndex = 43
        '
        'gbFilterGeneral_MovieSets
        '
        Me.gbFilterGeneral_MovieSets.AutoSize = True
        Me.gbFilterGeneral_MovieSets.Controls.Add(Me.tblFilterGeneral_MovieSets)
        Me.gbFilterGeneral_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterGeneral_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFilterGeneral_MovieSets.Location = New System.Drawing.Point(3, 57)
        Me.gbFilterGeneral_MovieSets.Name = "gbFilterGeneral_MovieSets"
        Me.gbFilterGeneral_MovieSets.Size = New System.Drawing.Size(121, 113)
        Me.gbFilterGeneral_MovieSets.TabIndex = 3
        Me.gbFilterGeneral_MovieSets.TabStop = False
        Me.gbFilterGeneral_MovieSets.Text = "General"
        '
        'tblFilterGeneral_MovieSets
        '
        Me.tblFilterGeneral_MovieSets.AutoSize = True
        Me.tblFilterGeneral_MovieSets.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblFilterGeneral_MovieSets.ColumnCount = 2
        Me.tblFilterGeneral_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGeneral_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGeneral_MovieSets.Controls.Add(Me.btnFilterMissing_MovieSets, 1, 0)
        Me.tblFilterGeneral_MovieSets.Controls.Add(Me.chkFilterMissing_MovieSets, 0, 0)
        Me.tblFilterGeneral_MovieSets.Controls.Add(Me.chkFilterOne_MovieSets, 0, 2)
        Me.tblFilterGeneral_MovieSets.Controls.Add(Me.chkFilterEmpty_MovieSets, 0, 1)
        Me.tblFilterGeneral_MovieSets.Controls.Add(Me.chkFilterMultiple_MovieSets, 0, 3)
        Me.tblFilterGeneral_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterGeneral_MovieSets.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterGeneral_MovieSets.Name = "tblFilterGeneral_MovieSets"
        Me.tblFilterGeneral_MovieSets.RowCount = 5
        Me.tblFilterGeneral_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_MovieSets.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_MovieSets.Size = New System.Drawing.Size(115, 92)
        Me.tblFilterGeneral_MovieSets.TabIndex = 9
        '
        'btnFilterMissing_MovieSets
        '
        Me.btnFilterMissing_MovieSets.AutoSize = True
        Me.btnFilterMissing_MovieSets.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFilterMissing_MovieSets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterMissing_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFilterMissing_MovieSets.Location = New System.Drawing.Point(21, 0)
        Me.btnFilterMissing_MovieSets.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterMissing_MovieSets.Name = "btnFilterMissing_MovieSets"
        Me.btnFilterMissing_MovieSets.Size = New System.Drawing.Size(94, 23)
        Me.btnFilterMissing_MovieSets.TabIndex = 4
        Me.btnFilterMissing_MovieSets.Text = "Missing Items"
        Me.btnFilterMissing_MovieSets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterMissing_MovieSets.UseVisualStyleBackColor = True
        '
        'chkFilterMissing_MovieSets
        '
        Me.chkFilterMissing_MovieSets.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMissing_MovieSets.AutoSize = True
        Me.chkFilterMissing_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterMissing_MovieSets.Location = New System.Drawing.Point(3, 4)
        Me.chkFilterMissing_MovieSets.Name = "chkFilterMissing_MovieSets"
        Me.chkFilterMissing_MovieSets.Size = New System.Drawing.Size(15, 14)
        Me.chkFilterMissing_MovieSets.TabIndex = 1
        Me.chkFilterMissing_MovieSets.UseVisualStyleBackColor = True
        '
        'chkFilterOne_MovieSets
        '
        Me.chkFilterOne_MovieSets.AutoSize = True
        Me.tblFilterGeneral_MovieSets.SetColumnSpan(Me.chkFilterOne_MovieSets, 2)
        Me.chkFilterOne_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterOne_MovieSets.Location = New System.Drawing.Point(3, 49)
        Me.chkFilterOne_MovieSets.Name = "chkFilterOne_MovieSets"
        Me.chkFilterOne_MovieSets.Size = New System.Drawing.Size(109, 17)
        Me.chkFilterOne_MovieSets.TabIndex = 3
        Me.chkFilterOne_MovieSets.Text = "Only One Movie"
        Me.chkFilterOne_MovieSets.UseVisualStyleBackColor = True
        '
        'chkFilterEmpty_MovieSets
        '
        Me.chkFilterEmpty_MovieSets.AutoSize = True
        Me.tblFilterGeneral_MovieSets.SetColumnSpan(Me.chkFilterEmpty_MovieSets, 2)
        Me.chkFilterEmpty_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterEmpty_MovieSets.Location = New System.Drawing.Point(3, 26)
        Me.chkFilterEmpty_MovieSets.Name = "chkFilterEmpty_MovieSets"
        Me.chkFilterEmpty_MovieSets.Size = New System.Drawing.Size(57, 17)
        Me.chkFilterEmpty_MovieSets.TabIndex = 2
        Me.chkFilterEmpty_MovieSets.Text = "Empty"
        Me.chkFilterEmpty_MovieSets.UseVisualStyleBackColor = True
        '
        'chkFilterMultiple_MovieSets
        '
        Me.chkFilterMultiple_MovieSets.AutoSize = True
        Me.tblFilterGeneral_MovieSets.SetColumnSpan(Me.chkFilterMultiple_MovieSets, 2)
        Me.chkFilterMultiple_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterMultiple_MovieSets.Location = New System.Drawing.Point(3, 72)
        Me.chkFilterMultiple_MovieSets.Name = "chkFilterMultiple_MovieSets"
        Me.chkFilterMultiple_MovieSets.Size = New System.Drawing.Size(108, 17)
        Me.chkFilterMultiple_MovieSets.TabIndex = 3
        Me.chkFilterMultiple_MovieSets.Text = "Multiple Movies"
        Me.chkFilterMultiple_MovieSets.UseVisualStyleBackColor = True
        '
        'gbFilterSpecific_MovieSets
        '
        Me.gbFilterSpecific_MovieSets.AutoSize = True
        Me.gbFilterSpecific_MovieSets.Controls.Add(Me.tblFilterSpecific_MovieSets)
        Me.gbFilterSpecific_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFilterSpecific_MovieSets.Location = New System.Drawing.Point(130, 57)
        Me.gbFilterSpecific_MovieSets.Name = "gbFilterSpecific_MovieSets"
        Me.tblFilter_MovieSets.SetRowSpan(Me.gbFilterSpecific_MovieSets, 2)
        Me.gbFilterSpecific_MovieSets.Size = New System.Drawing.Size(114, 140)
        Me.gbFilterSpecific_MovieSets.TabIndex = 6
        Me.gbFilterSpecific_MovieSets.TabStop = False
        Me.gbFilterSpecific_MovieSets.Text = "Specific"
        '
        'tblFilterSpecific_MovieSets
        '
        Me.tblFilterSpecific_MovieSets.AutoSize = True
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
        Me.gbFilterModifier_MovieSets.AutoSize = True
        Me.gbFilterModifier_MovieSets.Controls.Add(Me.tblFilterModifier_MovieSets)
        Me.gbFilterModifier_MovieSets.Location = New System.Drawing.Point(3, 3)
        Me.gbFilterModifier_MovieSets.Name = "gbFilterModifier_MovieSets"
        Me.gbFilterModifier_MovieSets.Size = New System.Drawing.Size(102, 44)
        Me.gbFilterModifier_MovieSets.TabIndex = 3
        Me.gbFilterModifier_MovieSets.TabStop = False
        Me.gbFilterModifier_MovieSets.Text = "Modifier"
        '
        'tblFilterModifier_MovieSets
        '
        Me.tblFilterModifier_MovieSets.AutoSize = True
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
        Me.rbFilterOr_MovieSets.AutoSize = True
        Me.rbFilterOr_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbFilterOr_MovieSets.Location = New System.Drawing.Point(55, 3)
        Me.rbFilterOr_MovieSets.Name = "rbFilterOr_MovieSets"
        Me.rbFilterOr_MovieSets.Size = New System.Drawing.Size(38, 17)
        Me.rbFilterOr_MovieSets.TabIndex = 1
        Me.rbFilterOr_MovieSets.Text = "Or"
        Me.rbFilterOr_MovieSets.UseVisualStyleBackColor = True
        '
        'rbFilterAnd_MovieSets
        '
        Me.rbFilterAnd_MovieSets.AutoSize = True
        Me.rbFilterAnd_MovieSets.Checked = True
        Me.rbFilterAnd_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbFilterAnd_MovieSets.Location = New System.Drawing.Point(3, 3)
        Me.rbFilterAnd_MovieSets.Name = "rbFilterAnd_MovieSets"
        Me.rbFilterAnd_MovieSets.Size = New System.Drawing.Size(46, 17)
        Me.rbFilterAnd_MovieSets.TabIndex = 0
        Me.rbFilterAnd_MovieSets.TabStop = True
        Me.rbFilterAnd_MovieSets.Text = "And"
        Me.rbFilterAnd_MovieSets.UseVisualStyleBackColor = True
        '
        'chkFilterLock_MovieSets
        '
        Me.chkFilterLock_MovieSets.AutoSize = True
        Me.chkFilterLock_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFilterLock_MovieSets.Location = New System.Drawing.Point(3, 99)
        Me.chkFilterLock_MovieSets.Name = "chkFilterLock_MovieSets"
        Me.chkFilterLock_MovieSets.Size = New System.Drawing.Size(62, 17)
        Me.chkFilterLock_MovieSets.TabIndex = 2
        Me.chkFilterLock_MovieSets.Text = "Locked"
        Me.chkFilterLock_MovieSets.UseVisualStyleBackColor = True
        '
        'chkFilterNew_MovieSets
        '
        Me.chkFilterNew_MovieSets.AutoSize = True
        Me.chkFilterNew_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterNew_MovieSets.Location = New System.Drawing.Point(3, 53)
        Me.chkFilterNew_MovieSets.Name = "chkFilterNew_MovieSets"
        Me.chkFilterNew_MovieSets.Size = New System.Drawing.Size(49, 17)
        Me.chkFilterNew_MovieSets.TabIndex = 0
        Me.chkFilterNew_MovieSets.Text = "New"
        Me.chkFilterNew_MovieSets.UseVisualStyleBackColor = True
        '
        'chkFilterMark_MovieSets
        '
        Me.chkFilterMark_MovieSets.AutoSize = True
        Me.chkFilterMark_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterMark_MovieSets.Location = New System.Drawing.Point(3, 76)
        Me.chkFilterMark_MovieSets.Name = "chkFilterMark_MovieSets"
        Me.chkFilterMark_MovieSets.Size = New System.Drawing.Size(65, 17)
        Me.chkFilterMark_MovieSets.TabIndex = 1
        Me.chkFilterMark_MovieSets.Text = "Marked"
        Me.chkFilterMark_MovieSets.UseVisualStyleBackColor = True
        '
        'btnClearFilters_MovieSets
        '
        Me.btnClearFilters_MovieSets.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnClearFilters_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClearFilters_MovieSets.Image = CType(resources.GetObject("btnClearFilters_MovieSets.Image"), System.Drawing.Image)
        Me.btnClearFilters_MovieSets.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearFilters_MovieSets.Location = New System.Drawing.Point(17, 176)
        Me.btnClearFilters_MovieSets.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.btnClearFilters_MovieSets.Name = "btnClearFilters_MovieSets"
        Me.btnClearFilters_MovieSets.Size = New System.Drawing.Size(92, 20)
        Me.btnClearFilters_MovieSets.TabIndex = 5
        Me.btnClearFilters_MovieSets.Text = "Clear Filters"
        Me.btnClearFilters_MovieSets.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClearFilters_MovieSets.UseVisualStyleBackColor = True
        '
        'pnlFilterTop_MovieSets
        '
        Me.pnlFilterTop_MovieSets.AutoSize = True
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
        Me.tblFilterTop_MovieSets.AutoSize = True
        Me.tblFilterTop_MovieSets.ColumnCount = 4
        Me.tblFilterTop_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterTop_MovieSets.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
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
        Me.lblFilter_MovieSets.AutoSize = True
        Me.lblFilter_MovieSets.BackColor = System.Drawing.Color.Transparent
        Me.lblFilter_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.btnFilterUp_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFilterUp_MovieSets.Location = New System.Drawing.Point(505, 0)
        Me.btnFilterUp_MovieSets.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterUp_MovieSets.Name = "btnFilterUp_MovieSets"
        Me.btnFilterUp_MovieSets.Size = New System.Drawing.Size(30, 22)
        Me.btnFilterUp_MovieSets.TabIndex = 1
        Me.btnFilterUp_MovieSets.TabStop = False
        Me.btnFilterUp_MovieSets.Text = "^"
        Me.btnFilterUp_MovieSets.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFilterUp_MovieSets.UseVisualStyleBackColor = False
        '
        'btnFilterDown_MovieSets
        '
        Me.btnFilterDown_MovieSets.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFilterDown_MovieSets.BackColor = System.Drawing.SystemColors.Control
        Me.btnFilterDown_MovieSets.Enabled = False
        Me.btnFilterDown_MovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFilterDown_MovieSets.Location = New System.Drawing.Point(535, 0)
        Me.btnFilterDown_MovieSets.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterDown_MovieSets.Name = "btnFilterDown_MovieSets"
        Me.btnFilterDown_MovieSets.Size = New System.Drawing.Size(30, 22)
        Me.btnFilterDown_MovieSets.TabIndex = 2
        Me.btnFilterDown_MovieSets.TabStop = False
        Me.btnFilterDown_MovieSets.Text = "v"
        Me.btnFilterDown_MovieSets.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFilterDown_MovieSets.UseVisualStyleBackColor = False
        '
        'pnlFilter_Shows
        '
        Me.pnlFilter_Shows.AutoSize = True
        Me.pnlFilter_Shows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilter_Shows.Controls.Add(Me.tblFilter_Shows)
        Me.pnlFilter_Shows.Controls.Add(Me.pnlFilterTop_Shows)
        Me.pnlFilter_Shows.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFilter_Shows.Location = New System.Drawing.Point(0, 652)
        Me.pnlFilter_Shows.Name = "pnlFilter_Shows"
        Me.pnlFilter_Shows.Size = New System.Drawing.Size(567, 247)
        Me.pnlFilter_Shows.TabIndex = 27
        Me.pnlFilter_Shows.Visible = False
        '
        'tblFilter_Shows
        '
        Me.tblFilter_Shows.AutoScroll = True
        Me.tblFilter_Shows.AutoSize = True
        Me.tblFilter_Shows.ColumnCount = 3
        Me.tblFilter_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilter_Shows.Controls.Add(Me.gbFilterSorting_Shows, 0, 2)
        Me.tblFilter_Shows.Controls.Add(Me.gbFilterLists_Shows, 0, 0)
        Me.tblFilter_Shows.Controls.Add(Me.gbFilterGeneral_Shows, 0, 1)
        Me.tblFilter_Shows.Controls.Add(Me.gbFilterSpecific_Shows, 1, 1)
        Me.tblFilter_Shows.Controls.Add(Me.btnClearFilters_Shows, 0, 3)
        Me.tblFilter_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilter_Shows.Location = New System.Drawing.Point(0, 22)
        Me.tblFilter_Shows.Name = "tblFilter_Shows"
        Me.tblFilter_Shows.RowCount = 5
        Me.tblFilter_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilter_Shows.Size = New System.Drawing.Size(565, 223)
        Me.tblFilter_Shows.TabIndex = 8
        '
        'gbFilterSorting_Shows
        '
        Me.gbFilterSorting_Shows.AutoSize = True
        Me.gbFilterSorting_Shows.Controls.Add(Me.TableLayoutPanel1)
        Me.gbFilterSorting_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterSorting_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFilterSorting_Shows.Location = New System.Drawing.Point(3, 107)
        Me.gbFilterSorting_Shows.Name = "gbFilterSorting_Shows"
        Me.gbFilterSorting_Shows.Size = New System.Drawing.Size(114, 44)
        Me.gbFilterSorting_Shows.TabIndex = 10
        Me.gbFilterSorting_Shows.TabStop = False
        Me.gbFilterSorting_Shows.Text = "Extra Sorting"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnFilterSortTitle_Shows, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 18)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(108, 23)
        Me.TableLayoutPanel1.TabIndex = 8
        '
        'btnFilterSortTitle_Shows
        '
        Me.btnFilterSortTitle_Shows.AutoSize = True
        Me.btnFilterSortTitle_Shows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFilterSortTitle_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterSortTitle_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFilterSortTitle_Shows.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFilterSortTitle_Shows.Location = New System.Drawing.Point(0, 0)
        Me.btnFilterSortTitle_Shows.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterSortTitle_Shows.Name = "btnFilterSortTitle_Shows"
        Me.btnFilterSortTitle_Shows.Size = New System.Drawing.Size(108, 23)
        Me.btnFilterSortTitle_Shows.TabIndex = 1
        Me.btnFilterSortTitle_Shows.Text = "Sort Title"
        Me.btnFilterSortTitle_Shows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterSortTitle_Shows.UseVisualStyleBackColor = True
        '
        'gbFilterLists_Shows
        '
        Me.gbFilterLists_Shows.AutoSize = True
        Me.tblFilter_Shows.SetColumnSpan(Me.gbFilterLists_Shows, 2)
        Me.gbFilterLists_Shows.Controls.Add(Me.tblFilterLists_Shows)
        Me.gbFilterLists_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterLists_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbFilterLists_Shows.Location = New System.Drawing.Point(3, 3)
        Me.gbFilterLists_Shows.Name = "gbFilterLists_Shows"
        Me.gbFilterLists_Shows.Size = New System.Drawing.Size(535, 48)
        Me.gbFilterLists_Shows.TabIndex = 9
        Me.gbFilterLists_Shows.TabStop = False
        Me.gbFilterLists_Shows.Text = "Lists"
        '
        'tblFilterLists_Shows
        '
        Me.tblFilterLists_Shows.AutoSize = True
        Me.tblFilterLists_Shows.ColumnCount = 1
        Me.tblFilterLists_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterLists_Shows.Controls.Add(Me.cbFilterLists_Shows, 0, 0)
        Me.tblFilterLists_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterLists_Shows.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterLists_Shows.Name = "tblFilterLists_Shows"
        Me.tblFilterLists_Shows.RowCount = 1
        Me.tblFilterLists_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterLists_Shows.Size = New System.Drawing.Size(529, 27)
        Me.tblFilterLists_Shows.TabIndex = 0
        '
        'cbFilterLists_Shows
        '
        Me.cbFilterLists_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbFilterLists_Shows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterLists_Shows.Enabled = False
        Me.cbFilterLists_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbFilterLists_Shows.FormattingEnabled = True
        Me.cbFilterLists_Shows.Location = New System.Drawing.Point(3, 3)
        Me.cbFilterLists_Shows.Name = "cbFilterLists_Shows"
        Me.cbFilterLists_Shows.Size = New System.Drawing.Size(523, 21)
        Me.cbFilterLists_Shows.TabIndex = 43
        '
        'gbFilterGeneral_Shows
        '
        Me.gbFilterGeneral_Shows.AutoSize = True
        Me.gbFilterGeneral_Shows.Controls.Add(Me.tblFilterGeneral_Shows)
        Me.gbFilterGeneral_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterGeneral_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFilterGeneral_Shows.Location = New System.Drawing.Point(3, 57)
        Me.gbFilterGeneral_Shows.Name = "gbFilterGeneral_Shows"
        Me.gbFilterGeneral_Shows.Size = New System.Drawing.Size(114, 44)
        Me.gbFilterGeneral_Shows.TabIndex = 3
        Me.gbFilterGeneral_Shows.TabStop = False
        Me.gbFilterGeneral_Shows.Text = "Shows"
        '
        'tblFilterGeneral_Shows
        '
        Me.tblFilterGeneral_Shows.AutoSize = True
        Me.tblFilterGeneral_Shows.ColumnCount = 2
        Me.tblFilterGeneral_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGeneral_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterGeneral_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterGeneral_Shows.Controls.Add(Me.btnFilterMissing_Shows, 1, 0)
        Me.tblFilterGeneral_Shows.Controls.Add(Me.chkFilterMissing_Shows, 0, 0)
        Me.tblFilterGeneral_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterGeneral_Shows.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterGeneral_Shows.Name = "tblFilterGeneral_Shows"
        Me.tblFilterGeneral_Shows.RowCount = 2
        Me.tblFilterGeneral_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterGeneral_Shows.Size = New System.Drawing.Size(108, 23)
        Me.tblFilterGeneral_Shows.TabIndex = 34
        '
        'btnFilterMissing_Shows
        '
        Me.btnFilterMissing_Shows.AutoSize = True
        Me.btnFilterMissing_Shows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnFilterMissing_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFilterMissing_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFilterMissing_Shows.Location = New System.Drawing.Point(21, 0)
        Me.btnFilterMissing_Shows.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterMissing_Shows.Name = "btnFilterMissing_Shows"
        Me.btnFilterMissing_Shows.Size = New System.Drawing.Size(87, 23)
        Me.btnFilterMissing_Shows.TabIndex = 5
        Me.btnFilterMissing_Shows.Text = "Missing Items"
        Me.btnFilterMissing_Shows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFilterMissing_Shows.UseVisualStyleBackColor = True
        '
        'chkFilterMissing_Shows
        '
        Me.chkFilterMissing_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMissing_Shows.AutoSize = True
        Me.chkFilterMissing_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterMissing_Shows.Location = New System.Drawing.Point(3, 4)
        Me.chkFilterMissing_Shows.Name = "chkFilterMissing_Shows"
        Me.chkFilterMissing_Shows.Size = New System.Drawing.Size(15, 14)
        Me.chkFilterMissing_Shows.TabIndex = 1
        Me.chkFilterMissing_Shows.UseVisualStyleBackColor = True
        '
        'gbFilterSpecific_Shows
        '
        Me.gbFilterSpecific_Shows.AutoSize = True
        Me.gbFilterSpecific_Shows.Controls.Add(Me.tblFilterSpecific_Shows)
        Me.gbFilterSpecific_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterSpecific_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFilterSpecific_Shows.Location = New System.Drawing.Point(123, 57)
        Me.gbFilterSpecific_Shows.Name = "gbFilterSpecific_Shows"
        Me.tblFilter_Shows.SetRowSpan(Me.gbFilterSpecific_Shows, 3)
        Me.gbFilterSpecific_Shows.Size = New System.Drawing.Size(415, 163)
        Me.gbFilterSpecific_Shows.TabIndex = 6
        Me.gbFilterSpecific_Shows.TabStop = False
        Me.gbFilterSpecific_Shows.Text = "Specific"
        '
        'tblFilterSpecific_Shows
        '
        Me.tblFilterSpecific_Shows.AutoSize = True
        Me.tblFilterSpecific_Shows.ColumnCount = 3
        Me.tblFilterSpecific_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecific_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecific_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecific_Shows.Controls.Add(Me.chkFilterNewEpisodes_Shows, 0, 2)
        Me.tblFilterSpecific_Shows.Controls.Add(Me.chkFilterLock_Shows, 0, 4)
        Me.tblFilterSpecific_Shows.Controls.Add(Me.gbFilterModifier_Shows, 0, 0)
        Me.tblFilterSpecific_Shows.Controls.Add(Me.chkFilterMark_Shows, 0, 3)
        Me.tblFilterSpecific_Shows.Controls.Add(Me.tblFilterSpecificData_Shows, 1, 0)
        Me.tblFilterSpecific_Shows.Controls.Add(Me.chkFilterNewShows_Shows, 0, 1)
        Me.tblFilterSpecific_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterSpecific_Shows.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterSpecific_Shows.Name = "tblFilterSpecific_Shows"
        Me.tblFilterSpecific_Shows.RowCount = 6
        Me.tblFilterSpecific_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Shows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterSpecific_Shows.Size = New System.Drawing.Size(409, 142)
        Me.tblFilterSpecific_Shows.TabIndex = 8
        '
        'chkFilterNewEpisodes_Shows
        '
        Me.chkFilterNewEpisodes_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterNewEpisodes_Shows.AutoSize = True
        Me.chkFilterNewEpisodes_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterNewEpisodes_Shows.Location = New System.Drawing.Point(3, 76)
        Me.chkFilterNewEpisodes_Shows.Name = "chkFilterNewEpisodes_Shows"
        Me.chkFilterNewEpisodes_Shows.Size = New System.Drawing.Size(104, 17)
        Me.chkFilterNewEpisodes_Shows.TabIndex = 8
        Me.chkFilterNewEpisodes_Shows.Text = "New Episode(s)"
        Me.chkFilterNewEpisodes_Shows.UseVisualStyleBackColor = True
        '
        'chkFilterLock_Shows
        '
        Me.chkFilterLock_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterLock_Shows.AutoSize = True
        Me.chkFilterLock_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFilterLock_Shows.Location = New System.Drawing.Point(3, 122)
        Me.chkFilterLock_Shows.Name = "chkFilterLock_Shows"
        Me.chkFilterLock_Shows.Size = New System.Drawing.Size(62, 17)
        Me.chkFilterLock_Shows.TabIndex = 2
        Me.chkFilterLock_Shows.Text = "Locked"
        Me.chkFilterLock_Shows.UseVisualStyleBackColor = True
        '
        'gbFilterModifier_Shows
        '
        Me.gbFilterModifier_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.gbFilterModifier_Shows.AutoSize = True
        Me.gbFilterModifier_Shows.Controls.Add(Me.tblFilterModifier_Shows)
        Me.gbFilterModifier_Shows.Location = New System.Drawing.Point(3, 3)
        Me.gbFilterModifier_Shows.Name = "gbFilterModifier_Shows"
        Me.gbFilterModifier_Shows.Size = New System.Drawing.Size(102, 44)
        Me.gbFilterModifier_Shows.TabIndex = 3
        Me.gbFilterModifier_Shows.TabStop = False
        Me.gbFilterModifier_Shows.Text = "Modifier"
        '
        'tblFilterModifier_Shows
        '
        Me.tblFilterModifier_Shows.AutoSize = True
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
        Me.rbFilterOr_Shows.AutoSize = True
        Me.rbFilterOr_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbFilterOr_Shows.Location = New System.Drawing.Point(55, 3)
        Me.rbFilterOr_Shows.Name = "rbFilterOr_Shows"
        Me.rbFilterOr_Shows.Size = New System.Drawing.Size(38, 17)
        Me.rbFilterOr_Shows.TabIndex = 1
        Me.rbFilterOr_Shows.Text = "Or"
        Me.rbFilterOr_Shows.UseVisualStyleBackColor = True
        '
        'rbFilterAnd_Shows
        '
        Me.rbFilterAnd_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbFilterAnd_Shows.AutoSize = True
        Me.rbFilterAnd_Shows.Checked = True
        Me.rbFilterAnd_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbFilterAnd_Shows.Location = New System.Drawing.Point(3, 3)
        Me.rbFilterAnd_Shows.Name = "rbFilterAnd_Shows"
        Me.rbFilterAnd_Shows.Size = New System.Drawing.Size(46, 17)
        Me.rbFilterAnd_Shows.TabIndex = 0
        Me.rbFilterAnd_Shows.TabStop = True
        Me.rbFilterAnd_Shows.Text = "And"
        Me.rbFilterAnd_Shows.UseVisualStyleBackColor = True
        '
        'chkFilterMark_Shows
        '
        Me.chkFilterMark_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMark_Shows.AutoSize = True
        Me.chkFilterMark_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterMark_Shows.Location = New System.Drawing.Point(3, 99)
        Me.chkFilterMark_Shows.Name = "chkFilterMark_Shows"
        Me.chkFilterMark_Shows.Size = New System.Drawing.Size(65, 17)
        Me.chkFilterMark_Shows.TabIndex = 1
        Me.chkFilterMark_Shows.Text = "Marked"
        Me.chkFilterMark_Shows.UseVisualStyleBackColor = True
        '
        'tblFilterSpecificData_Shows
        '
        Me.tblFilterSpecificData_Shows.AutoSize = True
        Me.tblFilterSpecificData_Shows.ColumnCount = 3
        Me.tblFilterSpecificData_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecificData_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecificData_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterSpecificData_Shows.Controls.Add(Me.txtFilterSource_Shows, 1, 1)
        Me.tblFilterSpecificData_Shows.Controls.Add(Me.lblFilterGenre_Shows, 0, 0)
        Me.tblFilterSpecificData_Shows.Controls.Add(Me.txtFilterGenre_Shows, 1, 0)
        Me.tblFilterSpecificData_Shows.Controls.Add(Me.lblFilterSource_Shows, 0, 1)
        Me.tblFilterSpecificData_Shows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterSpecificData_Shows.Location = New System.Drawing.Point(113, 3)
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
        Me.txtFilterSource_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilterSource_Shows.Location = New System.Drawing.Point(54, 31)
        Me.txtFilterSource_Shows.Name = "txtFilterSource_Shows"
        Me.txtFilterSource_Shows.ReadOnly = True
        Me.txtFilterSource_Shows.Size = New System.Drawing.Size(160, 22)
        Me.txtFilterSource_Shows.TabIndex = 33
        '
        'lblFilterGenre_Shows
        '
        Me.lblFilterGenre_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterGenre_Shows.AutoSize = True
        Me.lblFilterGenre_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.txtFilterGenre_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilterGenre_Shows.Location = New System.Drawing.Point(54, 3)
        Me.txtFilterGenre_Shows.Name = "txtFilterGenre_Shows"
        Me.txtFilterGenre_Shows.ReadOnly = True
        Me.txtFilterGenre_Shows.Size = New System.Drawing.Size(160, 22)
        Me.txtFilterGenre_Shows.TabIndex = 4
        '
        'lblFilterSource_Shows
        '
        Me.lblFilterSource_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilterSource_Shows.AutoSize = True
        Me.lblFilterSource_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilterSource_Shows.Location = New System.Drawing.Point(3, 35)
        Me.lblFilterSource_Shows.Name = "lblFilterSource_Shows"
        Me.lblFilterSource_Shows.Size = New System.Drawing.Size(45, 13)
        Me.lblFilterSource_Shows.TabIndex = 32
        Me.lblFilterSource_Shows.Text = "Source:"
        '
        'chkFilterNewShows_Shows
        '
        Me.chkFilterNewShows_Shows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterNewShows_Shows.AutoSize = True
        Me.chkFilterNewShows_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkFilterNewShows_Shows.Location = New System.Drawing.Point(3, 53)
        Me.chkFilterNewShows_Shows.Name = "chkFilterNewShows_Shows"
        Me.chkFilterNewShows_Shows.Size = New System.Drawing.Size(92, 17)
        Me.chkFilterNewShows_Shows.TabIndex = 0
        Me.chkFilterNewShows_Shows.Text = "New Show(s)"
        Me.chkFilterNewShows_Shows.UseVisualStyleBackColor = True
        '
        'btnClearFilters_Shows
        '
        Me.btnClearFilters_Shows.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnClearFilters_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClearFilters_Shows.Image = CType(resources.GetObject("btnClearFilters_Shows.Image"), System.Drawing.Image)
        Me.btnClearFilters_Shows.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearFilters_Shows.Location = New System.Drawing.Point(14, 193)
        Me.btnClearFilters_Shows.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.btnClearFilters_Shows.Name = "btnClearFilters_Shows"
        Me.btnClearFilters_Shows.Size = New System.Drawing.Size(92, 20)
        Me.btnClearFilters_Shows.TabIndex = 5
        Me.btnClearFilters_Shows.Text = "Clear Filters"
        Me.btnClearFilters_Shows.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClearFilters_Shows.UseVisualStyleBackColor = True
        '
        'pnlFilterTop_Shows
        '
        Me.pnlFilterTop_Shows.AutoSize = True
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
        Me.tblFilterTop_Shows.AutoSize = True
        Me.tblFilterTop_Shows.ColumnCount = 4
        Me.tblFilterTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterTop_Shows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
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
        Me.lblFilter_Shows.AutoSize = True
        Me.lblFilter_Shows.BackColor = System.Drawing.Color.Transparent
        Me.lblFilter_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.btnFilterUp_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFilterUp_Shows.Location = New System.Drawing.Point(505, 0)
        Me.btnFilterUp_Shows.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterUp_Shows.Name = "btnFilterUp_Shows"
        Me.btnFilterUp_Shows.Size = New System.Drawing.Size(30, 22)
        Me.btnFilterUp_Shows.TabIndex = 1
        Me.btnFilterUp_Shows.TabStop = False
        Me.btnFilterUp_Shows.Text = "^"
        Me.btnFilterUp_Shows.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFilterUp_Shows.UseVisualStyleBackColor = False
        '
        'btnFilterDown_Shows
        '
        Me.btnFilterDown_Shows.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFilterDown_Shows.BackColor = System.Drawing.SystemColors.Control
        Me.btnFilterDown_Shows.Enabled = False
        Me.btnFilterDown_Shows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFilterDown_Shows.Location = New System.Drawing.Point(535, 0)
        Me.btnFilterDown_Shows.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilterDown_Shows.Name = "btnFilterDown_Shows"
        Me.btnFilterDown_Shows.Size = New System.Drawing.Size(30, 22)
        Me.btnFilterDown_Shows.TabIndex = 2
        Me.btnFilterDown_Shows.TabStop = False
        Me.btnFilterDown_Shows.Text = "v"
        Me.btnFilterDown_Shows.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFilterDown_Shows.UseVisualStyleBackColor = False
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
        Me.pnlInfoPanel.Controls.Add(Me.pnlMoviesInSet)
        Me.pnlInfoPanel.Controls.Add(Me.txtCertifications)
        Me.pnlInfoPanel.Controls.Add(Me.lblCertificationsHeader)
        Me.pnlInfoPanel.Controls.Add(Me.lblReleaseDate)
        Me.pnlInfoPanel.Controls.Add(Me.lblReleaseDateHeader)
        Me.pnlInfoPanel.Controls.Add(Me.btnMid)
        Me.pnlInfoPanel.Controls.Add(Me.pbMILoading)
        Me.pnlInfoPanel.Controls.Add(Me.btnMetaDataRefresh)
        Me.pnlInfoPanel.Controls.Add(Me.lblMetaDataHeader)
        Me.pnlInfoPanel.Controls.Add(Me.txtMetaData)
        Me.pnlInfoPanel.Controls.Add(Me.btnTrailerPlay)
        Me.pnlInfoPanel.Controls.Add(Me.btnFilePlay)
        Me.pnlInfoPanel.Controls.Add(Me.txtTrailerPath)
        Me.pnlInfoPanel.Controls.Add(Me.txtFilePath)
        Me.pnlInfoPanel.Controls.Add(Me.lblTrailerPathHeader)
        Me.pnlInfoPanel.Controls.Add(Me.lblFilePathHeader)
        Me.pnlInfoPanel.Controls.Add(Me.txtTMDBID)
        Me.pnlInfoPanel.Controls.Add(Me.txtIMDBID)
        Me.pnlInfoPanel.Controls.Add(Me.lblTMDBHeader)
        Me.pnlInfoPanel.Controls.Add(Me.lblIMDBHeader)
        Me.pnlInfoPanel.Controls.Add(Me.lblDirectors)
        Me.pnlInfoPanel.Controls.Add(Me.lblDirectorsHeader)
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
        Me.pnlInfoPanel.Location = New System.Drawing.Point(0, 557)
        Me.pnlInfoPanel.Name = "pnlInfoPanel"
        Me.pnlInfoPanel.Size = New System.Drawing.Size(773, 342)
        Me.pnlInfoPanel.TabIndex = 10
        '
        'pnlMoviesInSet
        '
        Me.pnlMoviesInSet.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.lvMoviesInSet.UseCompatibleStateImageBehavior = False
        '
        'ilMoviesInSet
        '
        Me.ilMoviesInSet.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ilMoviesInSet.ImageSize = New System.Drawing.Size(16, 16)
        Me.ilMoviesInSet.TransparentColor = System.Drawing.Color.Transparent
        '
        'lblMoviesInSetHeader
        '
        Me.lblMoviesInSetHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMoviesInSetHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblMoviesInSetHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMoviesInSetHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoviesInSetHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblMoviesInSetHeader.Location = New System.Drawing.Point(3, 3)
        Me.lblMoviesInSetHeader.Name = "lblMoviesInSetHeader"
        Me.lblMoviesInSetHeader.Size = New System.Drawing.Size(765, 17)
        Me.lblMoviesInSetHeader.TabIndex = 40
        Me.lblMoviesInSetHeader.Text = "Movies in Set"
        Me.lblMoviesInSetHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCertifications
        '
        Me.txtCertifications.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCertifications.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCertifications.Location = New System.Drawing.Point(231, 208)
        Me.txtCertifications.Name = "txtCertifications"
        Me.txtCertifications.ReadOnly = True
        Me.txtCertifications.Size = New System.Drawing.Size(223, 22)
        Me.txtCertifications.TabIndex = 3
        Me.txtCertifications.TabStop = False
        '
        'lblCertificationsHeader
        '
        Me.lblCertificationsHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCertificationsHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblCertificationsHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCertificationsHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCertificationsHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblCertificationsHeader.Location = New System.Drawing.Point(231, 188)
        Me.lblCertificationsHeader.Name = "lblCertificationsHeader"
        Me.lblCertificationsHeader.Size = New System.Drawing.Size(223, 17)
        Me.lblCertificationsHeader.TabIndex = 2
        Me.lblCertificationsHeader.Text = "Certifications"
        Me.lblCertificationsHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblReleaseDate
        '
        Me.lblReleaseDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblReleaseDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReleaseDate.ForeColor = System.Drawing.Color.Black
        Me.lblReleaseDate.Location = New System.Drawing.Point(288, 48)
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
        Me.lblReleaseDateHeader.Location = New System.Drawing.Point(288, 27)
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
        Me.btnMid.Location = New System.Drawing.Point(702, 1)
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
        Me.pbMILoading.Location = New System.Drawing.Point(604, 374)
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
        Me.btnMetaDataRefresh.Location = New System.Drawing.Point(691, 278)
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
        Me.lblMetaDataHeader.Location = New System.Drawing.Point(467, 282)
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
        Me.txtMetaData.Location = New System.Drawing.Point(467, 303)
        Me.txtMetaData.Multiline = True
        Me.txtMetaData.Name = "txtMetaData"
        Me.txtMetaData.ReadOnly = True
        Me.txtMetaData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMetaData.Size = New System.Drawing.Size(296, 184)
        Me.txtMetaData.TabIndex = 10
        Me.txtMetaData.TabStop = False
        '
        'btnFilePlay
        '
        Me.btnFilePlay.Image = Global.Ember_Media_Manager.My.Resources.Resources.Play_Icon
        Me.btnFilePlay.Location = New System.Drawing.Point(205, 254)
        Me.btnFilePlay.Name = "btnFilePlay"
        Me.btnFilePlay.Size = New System.Drawing.Size(20, 20)
        Me.btnFilePlay.TabIndex = 6
        Me.btnFilePlay.TabStop = False
        Me.btnFilePlay.UseVisualStyleBackColor = True
        '
        'txtFilePath
        '
        Me.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilePath.Location = New System.Drawing.Point(3, 254)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(200, 22)
        Me.txtFilePath.TabIndex = 5
        Me.txtFilePath.TabStop = False
        '
        'lblFilePathHeader
        '
        Me.lblFilePathHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblFilePathHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFilePathHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilePathHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFilePathHeader.Location = New System.Drawing.Point(3, 234)
        Me.lblFilePathHeader.Name = "lblFilePathHeader"
        Me.lblFilePathHeader.Size = New System.Drawing.Size(222, 17)
        Me.lblFilePathHeader.TabIndex = 4
        Me.lblFilePathHeader.Text = "File Path"
        Me.lblFilePathHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTMDBID
        '
        Me.txtTMDBID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTMDBID.Location = New System.Drawing.Point(117, 208)
        Me.txtTMDBID.Name = "txtTMDBID"
        Me.txtTMDBID.ReadOnly = True
        Me.txtTMDBID.Size = New System.Drawing.Size(108, 22)
        Me.txtTMDBID.TabIndex = 1
        Me.txtTMDBID.TabStop = False
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
        'lblTMDBHeader
        '
        Me.lblTMDBHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblTMDBHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTMDBHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTMDBHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblTMDBHeader.Location = New System.Drawing.Point(117, 188)
        Me.lblTMDBHeader.Name = "lblTMDBHeader"
        Me.lblTMDBHeader.Size = New System.Drawing.Size(108, 17)
        Me.lblTMDBHeader.TabIndex = 0
        Me.lblTMDBHeader.Text = "TMDB ID"
        Me.lblTMDBHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'lblDirectors
        '
        Me.lblDirectors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDirectors.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDirectors.ForeColor = System.Drawing.Color.Black
        Me.lblDirectors.Location = New System.Drawing.Point(3, 48)
        Me.lblDirectors.Name = "lblDirectors"
        Me.lblDirectors.Size = New System.Drawing.Size(280, 16)
        Me.lblDirectors.TabIndex = 27
        Me.lblDirectors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDirectors.UseMnemonic = False
        '
        'lblDirectorsHeader
        '
        Me.lblDirectorsHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDirectorsHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblDirectorsHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDirectorsHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDirectorsHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblDirectorsHeader.Location = New System.Drawing.Point(3, 27)
        Me.lblDirectorsHeader.Name = "lblDirectorsHeader"
        Me.lblDirectorsHeader.Size = New System.Drawing.Size(279, 17)
        Me.lblDirectorsHeader.TabIndex = 21
        Me.lblDirectorsHeader.Text = "Directors"
        Me.lblDirectorsHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlActors
        '
        Me.pnlActors.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.lblActorsHeader.Text = "Actors"
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
        Me.lblOutlineHeader.Size = New System.Drawing.Size(451, 17)
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
        Me.txtOutline.Size = New System.Drawing.Size(451, 78)
        Me.txtOutline.TabIndex = 16
        Me.txtOutline.TabStop = False
        '
        'pnlTop250
        '
        Me.pnlTop250.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.lblPlotHeader.Size = New System.Drawing.Size(451, 17)
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
        Me.txtPlot.Size = New System.Drawing.Size(451, 184)
        Me.txtPlot.TabIndex = 7
        Me.txtPlot.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDown.BackColor = System.Drawing.SystemColors.Control
        Me.btnDown.Location = New System.Drawing.Point(733, 1)
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
        Me.btnUp.Location = New System.Drawing.Point(670, 1)
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
        Me.lblInfoPanelHeader.Size = New System.Drawing.Size(765, 17)
        Me.lblInfoPanelHeader.TabIndex = 0
        Me.lblInfoPanelHeader.Text = "Info"
        Me.lblInfoPanelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pbBannerCache
        '
        Me.pbBannerCache.Location = New System.Drawing.Point(740, 130)
        Me.pbBannerCache.Name = "pbBannerCache"
        Me.pbBannerCache.Size = New System.Drawing.Size(50, 50)
        Me.pbBannerCache.TabIndex = 28
        Me.pbBannerCache.TabStop = False
        Me.pbBannerCache.Visible = False
        '
        'pnlBanner
        '
        Me.pnlBanner.AutoSize = True
        Me.pnlBanner.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlBanner.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlBanner.Controls.Add(Me.pnlBannerMain)
        Me.pnlBanner.Controls.Add(Me.pnlBannerBottom)
        Me.pnlBanner.Controls.Add(Me.pnlBannerTop)
        Me.pnlBanner.Location = New System.Drawing.Point(172, 215)
        Me.pnlBanner.Name = "pnlBanner"
        Me.pnlBanner.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlBanner.Size = New System.Drawing.Size(56, 82)
        Me.pnlBanner.TabIndex = 27
        Me.pnlBanner.Visible = False
        '
        'pnlBannerMain
        '
        Me.pnlBannerMain.AutoSize = True
        Me.pnlBannerMain.Controls.Add(Me.tblBannerMain)
        Me.pnlBannerMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBannerMain.Location = New System.Drawing.Point(2, 15)
        Me.pnlBannerMain.Name = "pnlBannerMain"
        Me.pnlBannerMain.Size = New System.Drawing.Size(50, 50)
        Me.pnlBannerMain.TabIndex = 3
        '
        'tblBannerMain
        '
        Me.tblBannerMain.AutoSize = True
        Me.tblBannerMain.ColumnCount = 1
        Me.tblBannerMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBannerMain.Controls.Add(Me.pbBanner, 0, 0)
        Me.tblBannerMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBannerMain.Location = New System.Drawing.Point(0, 0)
        Me.tblBannerMain.Name = "tblBannerMain"
        Me.tblBannerMain.RowCount = 1
        Me.tblBannerMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBannerMain.Size = New System.Drawing.Size(50, 50)
        Me.tblBannerMain.TabIndex = 1
        '
        'pbBanner
        '
        Me.pbBanner.BackColor = System.Drawing.Color.Gray
        Me.pbBanner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbBanner.Location = New System.Drawing.Point(0, 0)
        Me.pbBanner.Margin = New System.Windows.Forms.Padding(0)
        Me.pbBanner.Name = "pbBanner"
        Me.pbBanner.Size = New System.Drawing.Size(50, 50)
        Me.pbBanner.TabIndex = 0
        Me.pbBanner.TabStop = False
        '
        'pnlBannerBottom
        '
        Me.pnlBannerBottom.AutoSize = True
        Me.pnlBannerBottom.BackColor = System.Drawing.Color.DimGray
        Me.pnlBannerBottom.Controls.Add(Me.tblBannerBottom)
        Me.pnlBannerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBannerBottom.Location = New System.Drawing.Point(2, 65)
        Me.pnlBannerBottom.Name = "pnlBannerBottom"
        Me.pnlBannerBottom.Size = New System.Drawing.Size(50, 13)
        Me.pnlBannerBottom.TabIndex = 2
        '
        'tblBannerBottom
        '
        Me.tblBannerBottom.AutoSize = True
        Me.tblBannerBottom.ColumnCount = 3
        Me.tblBannerBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblBannerBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBannerBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblBannerBottom.Controls.Add(Me.lblBannerSize, 1, 0)
        Me.tblBannerBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBannerBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBannerBottom.Name = "tblBannerBottom"
        Me.tblBannerBottom.RowCount = 2
        Me.tblBannerBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBannerBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBannerBottom.Size = New System.Drawing.Size(50, 13)
        Me.tblBannerBottom.TabIndex = 0
        '
        'lblBannerSize
        '
        Me.lblBannerSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblBannerSize.AutoSize = True
        Me.lblBannerSize.BackColor = System.Drawing.Color.Transparent
        Me.lblBannerSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblBannerSize.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblBannerSize.Location = New System.Drawing.Point(11, 0)
        Me.lblBannerSize.Name = "lblBannerSize"
        Me.lblBannerSize.Size = New System.Drawing.Size(27, 13)
        Me.lblBannerSize.TabIndex = 2
        Me.lblBannerSize.Text = "Size"
        Me.lblBannerSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlBannerTop
        '
        Me.pnlBannerTop.AutoSize = True
        Me.pnlBannerTop.BackColor = System.Drawing.Color.DimGray
        Me.pnlBannerTop.Controls.Add(Me.tblBannerTop)
        Me.pnlBannerTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlBannerTop.Location = New System.Drawing.Point(2, 2)
        Me.pnlBannerTop.Name = "pnlBannerTop"
        Me.pnlBannerTop.Size = New System.Drawing.Size(50, 13)
        Me.pnlBannerTop.TabIndex = 1
        '
        'tblBannerTop
        '
        Me.tblBannerTop.AutoSize = True
        Me.tblBannerTop.ColumnCount = 3
        Me.tblBannerTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblBannerTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBannerTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblBannerTop.Controls.Add(Me.lblBannerTitle, 1, 0)
        Me.tblBannerTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBannerTop.Location = New System.Drawing.Point(0, 0)
        Me.tblBannerTop.Name = "tblBannerTop"
        Me.tblBannerTop.RowCount = 2
        Me.tblBannerTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBannerTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBannerTop.Size = New System.Drawing.Size(50, 13)
        Me.tblBannerTop.TabIndex = 0
        '
        'lblBannerTitle
        '
        Me.lblBannerTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblBannerTitle.AutoSize = True
        Me.lblBannerTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblBannerTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblBannerTitle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblBannerTitle.Location = New System.Drawing.Point(3, 0)
        Me.lblBannerTitle.Name = "lblBannerTitle"
        Me.lblBannerTitle.Size = New System.Drawing.Size(44, 13)
        Me.lblBannerTitle.TabIndex = 1
        Me.lblBannerTitle.Text = "Banner"
        Me.lblBannerTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pbCache
        '
        Me.pbCache.Location = New System.Drawing.Point(740, 130)
        Me.pbCache.Name = "pbCache"
        Me.pbCache.Size = New System.Drawing.Size(50, 50)
        Me.pbCache.TabIndex = 26
        Me.pbCache.TabStop = False
        Me.pbCache.Visible = False
        '
        'pnlClearLogo
        '
        Me.pnlClearLogo.AutoSize = True
        Me.pnlClearLogo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlClearLogo.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlClearLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlClearLogo.Controls.Add(Me.pnlClearLogoMain)
        Me.pnlClearLogo.Controls.Add(Me.pnlClearLogoBottom)
        Me.pnlClearLogo.Controls.Add(Me.pnlClearLogoTop)
        Me.pnlClearLogo.Location = New System.Drawing.Point(116, 215)
        Me.pnlClearLogo.Name = "pnlClearLogo"
        Me.pnlClearLogo.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlClearLogo.Size = New System.Drawing.Size(56, 82)
        Me.pnlClearLogo.TabIndex = 25
        Me.pnlClearLogo.Visible = False
        '
        'pnlClearLogoMain
        '
        Me.pnlClearLogoMain.AutoSize = True
        Me.pnlClearLogoMain.Controls.Add(Me.tblClearLogoMain)
        Me.pnlClearLogoMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlClearLogoMain.Location = New System.Drawing.Point(2, 15)
        Me.pnlClearLogoMain.Name = "pnlClearLogoMain"
        Me.pnlClearLogoMain.Size = New System.Drawing.Size(50, 50)
        Me.pnlClearLogoMain.TabIndex = 3
        '
        'tblClearLogoMain
        '
        Me.tblClearLogoMain.AutoSize = True
        Me.tblClearLogoMain.ColumnCount = 1
        Me.tblClearLogoMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearLogoMain.Controls.Add(Me.pbClearLogo, 0, 0)
        Me.tblClearLogoMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblClearLogoMain.Location = New System.Drawing.Point(0, 0)
        Me.tblClearLogoMain.Name = "tblClearLogoMain"
        Me.tblClearLogoMain.RowCount = 1
        Me.tblClearLogoMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearLogoMain.Size = New System.Drawing.Size(50, 50)
        Me.tblClearLogoMain.TabIndex = 1
        '
        'pbClearLogo
        '
        Me.pbClearLogo.BackColor = System.Drawing.Color.Gray
        Me.pbClearLogo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbClearLogo.Location = New System.Drawing.Point(0, 0)
        Me.pbClearLogo.Margin = New System.Windows.Forms.Padding(0)
        Me.pbClearLogo.Name = "pbClearLogo"
        Me.pbClearLogo.Size = New System.Drawing.Size(50, 50)
        Me.pbClearLogo.TabIndex = 0
        Me.pbClearLogo.TabStop = False
        '
        'pnlClearLogoBottom
        '
        Me.pnlClearLogoBottom.AutoSize = True
        Me.pnlClearLogoBottom.BackColor = System.Drawing.Color.DimGray
        Me.pnlClearLogoBottom.Controls.Add(Me.tblClearLogoBottom)
        Me.pnlClearLogoBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlClearLogoBottom.Location = New System.Drawing.Point(2, 65)
        Me.pnlClearLogoBottom.Name = "pnlClearLogoBottom"
        Me.pnlClearLogoBottom.Size = New System.Drawing.Size(50, 13)
        Me.pnlClearLogoBottom.TabIndex = 2
        '
        'tblClearLogoBottom
        '
        Me.tblClearLogoBottom.AutoSize = True
        Me.tblClearLogoBottom.ColumnCount = 3
        Me.tblClearLogoBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblClearLogoBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearLogoBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblClearLogoBottom.Controls.Add(Me.lblClearLogoSize, 1, 0)
        Me.tblClearLogoBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblClearLogoBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblClearLogoBottom.Name = "tblClearLogoBottom"
        Me.tblClearLogoBottom.RowCount = 2
        Me.tblClearLogoBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearLogoBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearLogoBottom.Size = New System.Drawing.Size(50, 13)
        Me.tblClearLogoBottom.TabIndex = 0
        '
        'lblClearLogoSize
        '
        Me.lblClearLogoSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblClearLogoSize.AutoSize = True
        Me.lblClearLogoSize.BackColor = System.Drawing.Color.Transparent
        Me.lblClearLogoSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblClearLogoSize.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblClearLogoSize.Location = New System.Drawing.Point(11, 0)
        Me.lblClearLogoSize.Name = "lblClearLogoSize"
        Me.lblClearLogoSize.Size = New System.Drawing.Size(27, 13)
        Me.lblClearLogoSize.TabIndex = 2
        Me.lblClearLogoSize.Text = "Size"
        Me.lblClearLogoSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlClearLogoTop
        '
        Me.pnlClearLogoTop.AutoSize = True
        Me.pnlClearLogoTop.BackColor = System.Drawing.Color.DimGray
        Me.pnlClearLogoTop.Controls.Add(Me.tblClearLogoTop)
        Me.pnlClearLogoTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlClearLogoTop.Location = New System.Drawing.Point(2, 2)
        Me.pnlClearLogoTop.Name = "pnlClearLogoTop"
        Me.pnlClearLogoTop.Size = New System.Drawing.Size(50, 13)
        Me.pnlClearLogoTop.TabIndex = 1
        '
        'tblClearLogoTop
        '
        Me.tblClearLogoTop.AutoSize = True
        Me.tblClearLogoTop.ColumnCount = 3
        Me.tblClearLogoTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblClearLogoTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearLogoTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblClearLogoTop.Controls.Add(Me.lblClearLogoTitle, 1, 0)
        Me.tblClearLogoTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblClearLogoTop.Location = New System.Drawing.Point(0, 0)
        Me.tblClearLogoTop.Name = "tblClearLogoTop"
        Me.tblClearLogoTop.RowCount = 2
        Me.tblClearLogoTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearLogoTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearLogoTop.Size = New System.Drawing.Size(50, 13)
        Me.tblClearLogoTop.TabIndex = 0
        '
        'lblClearLogoTitle
        '
        Me.lblClearLogoTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblClearLogoTitle.AutoSize = True
        Me.lblClearLogoTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblClearLogoTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblClearLogoTitle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblClearLogoTitle.Location = New System.Drawing.Point(-5, 0)
        Me.lblClearLogoTitle.Name = "lblClearLogoTitle"
        Me.lblClearLogoTitle.Size = New System.Drawing.Size(60, 13)
        Me.lblClearLogoTitle.TabIndex = 1
        Me.lblClearLogoTitle.Text = "ClearLogo"
        Me.lblClearLogoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlCharacterArt
        '
        Me.pnlCharacterArt.AutoSize = True
        Me.pnlCharacterArt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlCharacterArt.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlCharacterArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCharacterArt.Controls.Add(Me.pnlCharacterArtMain)
        Me.pnlCharacterArt.Controls.Add(Me.pnlCharacterArtBottom)
        Me.pnlCharacterArt.Controls.Add(Me.pnlCharacterArtTop)
        Me.pnlCharacterArt.Location = New System.Drawing.Point(60, 215)
        Me.pnlCharacterArt.Name = "pnlCharacterArt"
        Me.pnlCharacterArt.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlCharacterArt.Size = New System.Drawing.Size(56, 82)
        Me.pnlCharacterArt.TabIndex = 24
        Me.pnlCharacterArt.Visible = False
        '
        'pnlCharacterArtMain
        '
        Me.pnlCharacterArtMain.AutoSize = True
        Me.pnlCharacterArtMain.Controls.Add(Me.tblCharacterArtMain)
        Me.pnlCharacterArtMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCharacterArtMain.Location = New System.Drawing.Point(2, 15)
        Me.pnlCharacterArtMain.Name = "pnlCharacterArtMain"
        Me.pnlCharacterArtMain.Size = New System.Drawing.Size(50, 50)
        Me.pnlCharacterArtMain.TabIndex = 3
        '
        'tblCharacterArtMain
        '
        Me.tblCharacterArtMain.AutoSize = True
        Me.tblCharacterArtMain.ColumnCount = 1
        Me.tblCharacterArtMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCharacterArtMain.Controls.Add(Me.pbCharacterArt, 0, 0)
        Me.tblCharacterArtMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblCharacterArtMain.Location = New System.Drawing.Point(0, 0)
        Me.tblCharacterArtMain.Name = "tblCharacterArtMain"
        Me.tblCharacterArtMain.RowCount = 1
        Me.tblCharacterArtMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCharacterArtMain.Size = New System.Drawing.Size(50, 50)
        Me.tblCharacterArtMain.TabIndex = 1
        '
        'pbCharacterArt
        '
        Me.pbCharacterArt.BackColor = System.Drawing.Color.Gray
        Me.pbCharacterArt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbCharacterArt.Location = New System.Drawing.Point(0, 0)
        Me.pbCharacterArt.Margin = New System.Windows.Forms.Padding(0)
        Me.pbCharacterArt.Name = "pbCharacterArt"
        Me.pbCharacterArt.Size = New System.Drawing.Size(50, 50)
        Me.pbCharacterArt.TabIndex = 0
        Me.pbCharacterArt.TabStop = False
        '
        'pnlCharacterArtBottom
        '
        Me.pnlCharacterArtBottom.AutoSize = True
        Me.pnlCharacterArtBottom.BackColor = System.Drawing.Color.DimGray
        Me.pnlCharacterArtBottom.Controls.Add(Me.tblCharacterArtBottom)
        Me.pnlCharacterArtBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlCharacterArtBottom.Location = New System.Drawing.Point(2, 65)
        Me.pnlCharacterArtBottom.Name = "pnlCharacterArtBottom"
        Me.pnlCharacterArtBottom.Size = New System.Drawing.Size(50, 13)
        Me.pnlCharacterArtBottom.TabIndex = 2
        '
        'tblCharacterArtBottom
        '
        Me.tblCharacterArtBottom.AutoSize = True
        Me.tblCharacterArtBottom.ColumnCount = 3
        Me.tblCharacterArtBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblCharacterArtBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCharacterArtBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblCharacterArtBottom.Controls.Add(Me.lblCharacterArtSize, 1, 0)
        Me.tblCharacterArtBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblCharacterArtBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblCharacterArtBottom.Name = "tblCharacterArtBottom"
        Me.tblCharacterArtBottom.RowCount = 2
        Me.tblCharacterArtBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCharacterArtBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCharacterArtBottom.Size = New System.Drawing.Size(50, 13)
        Me.tblCharacterArtBottom.TabIndex = 0
        '
        'lblCharacterArtSize
        '
        Me.lblCharacterArtSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCharacterArtSize.AutoSize = True
        Me.lblCharacterArtSize.BackColor = System.Drawing.Color.Transparent
        Me.lblCharacterArtSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCharacterArtSize.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblCharacterArtSize.Location = New System.Drawing.Point(11, 0)
        Me.lblCharacterArtSize.Name = "lblCharacterArtSize"
        Me.lblCharacterArtSize.Size = New System.Drawing.Size(27, 13)
        Me.lblCharacterArtSize.TabIndex = 2
        Me.lblCharacterArtSize.Text = "Size"
        Me.lblCharacterArtSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlCharacterArtTop
        '
        Me.pnlCharacterArtTop.AutoSize = True
        Me.pnlCharacterArtTop.BackColor = System.Drawing.Color.DimGray
        Me.pnlCharacterArtTop.Controls.Add(Me.tblCharacterArtTop)
        Me.pnlCharacterArtTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCharacterArtTop.Location = New System.Drawing.Point(2, 2)
        Me.pnlCharacterArtTop.Name = "pnlCharacterArtTop"
        Me.pnlCharacterArtTop.Size = New System.Drawing.Size(50, 13)
        Me.pnlCharacterArtTop.TabIndex = 1
        '
        'tblCharacterArtTop
        '
        Me.tblCharacterArtTop.AutoSize = True
        Me.tblCharacterArtTop.ColumnCount = 3
        Me.tblCharacterArtTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblCharacterArtTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCharacterArtTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblCharacterArtTop.Controls.Add(Me.lblCharacterArtTitle, 1, 0)
        Me.tblCharacterArtTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblCharacterArtTop.Location = New System.Drawing.Point(0, 0)
        Me.tblCharacterArtTop.Name = "tblCharacterArtTop"
        Me.tblCharacterArtTop.RowCount = 2
        Me.tblCharacterArtTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCharacterArtTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCharacterArtTop.Size = New System.Drawing.Size(50, 13)
        Me.tblCharacterArtTop.TabIndex = 0
        '
        'lblCharacterArtTitle
        '
        Me.lblCharacterArtTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCharacterArtTitle.AutoSize = True
        Me.lblCharacterArtTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblCharacterArtTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCharacterArtTitle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblCharacterArtTitle.Location = New System.Drawing.Point(-11, 0)
        Me.lblCharacterArtTitle.Name = "lblCharacterArtTitle"
        Me.lblCharacterArtTitle.Size = New System.Drawing.Size(72, 13)
        Me.lblCharacterArtTitle.TabIndex = 1
        Me.lblCharacterArtTitle.Text = "CharacterArt"
        Me.lblCharacterArtTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pbCharacterArtCache
        '
        Me.pbCharacterArtCache.Location = New System.Drawing.Point(740, 130)
        Me.pbCharacterArtCache.Name = "pbCharacterArtCache"
        Me.pbCharacterArtCache.Size = New System.Drawing.Size(50, 50)
        Me.pbCharacterArtCache.TabIndex = 23
        Me.pbCharacterArtCache.TabStop = False
        Me.pbCharacterArtCache.Visible = False
        '
        'pnlDiscArt
        '
        Me.pnlDiscArt.AutoSize = True
        Me.pnlDiscArt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlDiscArt.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlDiscArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDiscArt.Controls.Add(Me.pnlDiscArtMain)
        Me.pnlDiscArt.Controls.Add(Me.pnlDiscArtBottom)
        Me.pnlDiscArt.Controls.Add(Me.pnlDiscArtTop)
        Me.pnlDiscArt.Location = New System.Drawing.Point(5, 215)
        Me.pnlDiscArt.Name = "pnlDiscArt"
        Me.pnlDiscArt.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlDiscArt.Size = New System.Drawing.Size(56, 82)
        Me.pnlDiscArt.TabIndex = 22
        Me.pnlDiscArt.Visible = False
        '
        'pnlDiscArtMain
        '
        Me.pnlDiscArtMain.AutoSize = True
        Me.pnlDiscArtMain.Controls.Add(Me.tblDiscArtMain)
        Me.pnlDiscArtMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDiscArtMain.Location = New System.Drawing.Point(2, 15)
        Me.pnlDiscArtMain.Name = "pnlDiscArtMain"
        Me.pnlDiscArtMain.Size = New System.Drawing.Size(50, 50)
        Me.pnlDiscArtMain.TabIndex = 3
        '
        'tblDiscArtMain
        '
        Me.tblDiscArtMain.AutoSize = True
        Me.tblDiscArtMain.ColumnCount = 1
        Me.tblDiscArtMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDiscArtMain.Controls.Add(Me.pbDiscArt, 0, 0)
        Me.tblDiscArtMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblDiscArtMain.Location = New System.Drawing.Point(0, 0)
        Me.tblDiscArtMain.Name = "tblDiscArtMain"
        Me.tblDiscArtMain.RowCount = 1
        Me.tblDiscArtMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDiscArtMain.Size = New System.Drawing.Size(50, 50)
        Me.tblDiscArtMain.TabIndex = 1
        '
        'pbDiscArt
        '
        Me.pbDiscArt.BackColor = System.Drawing.Color.Gray
        Me.pbDiscArt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbDiscArt.Location = New System.Drawing.Point(0, 0)
        Me.pbDiscArt.Margin = New System.Windows.Forms.Padding(0)
        Me.pbDiscArt.Name = "pbDiscArt"
        Me.pbDiscArt.Size = New System.Drawing.Size(50, 50)
        Me.pbDiscArt.TabIndex = 0
        Me.pbDiscArt.TabStop = False
        '
        'pnlDiscArtBottom
        '
        Me.pnlDiscArtBottom.AutoSize = True
        Me.pnlDiscArtBottom.BackColor = System.Drawing.Color.DimGray
        Me.pnlDiscArtBottom.Controls.Add(Me.tblDiscArtBottom)
        Me.pnlDiscArtBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlDiscArtBottom.Location = New System.Drawing.Point(2, 65)
        Me.pnlDiscArtBottom.Name = "pnlDiscArtBottom"
        Me.pnlDiscArtBottom.Size = New System.Drawing.Size(50, 13)
        Me.pnlDiscArtBottom.TabIndex = 2
        '
        'tblDiscArtBottom
        '
        Me.tblDiscArtBottom.AutoSize = True
        Me.tblDiscArtBottom.ColumnCount = 3
        Me.tblDiscArtBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblDiscArtBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDiscArtBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblDiscArtBottom.Controls.Add(Me.lblDiscArtSize, 1, 0)
        Me.tblDiscArtBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblDiscArtBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblDiscArtBottom.Name = "tblDiscArtBottom"
        Me.tblDiscArtBottom.RowCount = 2
        Me.tblDiscArtBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDiscArtBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDiscArtBottom.Size = New System.Drawing.Size(50, 13)
        Me.tblDiscArtBottom.TabIndex = 0
        '
        'lblDiscArtSize
        '
        Me.lblDiscArtSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblDiscArtSize.AutoSize = True
        Me.lblDiscArtSize.BackColor = System.Drawing.Color.Transparent
        Me.lblDiscArtSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblDiscArtSize.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblDiscArtSize.Location = New System.Drawing.Point(11, 0)
        Me.lblDiscArtSize.Name = "lblDiscArtSize"
        Me.lblDiscArtSize.Size = New System.Drawing.Size(27, 13)
        Me.lblDiscArtSize.TabIndex = 2
        Me.lblDiscArtSize.Text = "Size"
        Me.lblDiscArtSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlDiscArtTop
        '
        Me.pnlDiscArtTop.AutoSize = True
        Me.pnlDiscArtTop.BackColor = System.Drawing.Color.DimGray
        Me.pnlDiscArtTop.Controls.Add(Me.tblDiscArtTop)
        Me.pnlDiscArtTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDiscArtTop.Location = New System.Drawing.Point(2, 2)
        Me.pnlDiscArtTop.Name = "pnlDiscArtTop"
        Me.pnlDiscArtTop.Size = New System.Drawing.Size(50, 13)
        Me.pnlDiscArtTop.TabIndex = 1
        '
        'tblDiscArtTop
        '
        Me.tblDiscArtTop.AutoSize = True
        Me.tblDiscArtTop.ColumnCount = 3
        Me.tblDiscArtTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblDiscArtTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDiscArtTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblDiscArtTop.Controls.Add(Me.lblDiscArtTitle, 1, 0)
        Me.tblDiscArtTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblDiscArtTop.Location = New System.Drawing.Point(0, 0)
        Me.tblDiscArtTop.Name = "tblDiscArtTop"
        Me.tblDiscArtTop.RowCount = 2
        Me.tblDiscArtTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDiscArtTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDiscArtTop.Size = New System.Drawing.Size(50, 13)
        Me.tblDiscArtTop.TabIndex = 0
        '
        'lblDiscArtTitle
        '
        Me.lblDiscArtTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblDiscArtTitle.AutoSize = True
        Me.lblDiscArtTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblDiscArtTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblDiscArtTitle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblDiscArtTitle.Location = New System.Drawing.Point(3, 0)
        Me.lblDiscArtTitle.Name = "lblDiscArtTitle"
        Me.lblDiscArtTitle.Size = New System.Drawing.Size(44, 13)
        Me.lblDiscArtTitle.TabIndex = 1
        Me.lblDiscArtTitle.Text = "DiscArt"
        Me.lblDiscArtTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pbDiscArtCache
        '
        Me.pbDiscArtCache.Location = New System.Drawing.Point(740, 130)
        Me.pbDiscArtCache.Name = "pbDiscArtCache"
        Me.pbDiscArtCache.Size = New System.Drawing.Size(50, 50)
        Me.pbDiscArtCache.TabIndex = 21
        Me.pbDiscArtCache.TabStop = False
        Me.pbDiscArtCache.Visible = False
        '
        'pbClearLogoCache
        '
        Me.pbClearLogoCache.Location = New System.Drawing.Point(740, 130)
        Me.pbClearLogoCache.Name = "pbClearLogoCache"
        Me.pbClearLogoCache.Size = New System.Drawing.Size(50, 50)
        Me.pbClearLogoCache.TabIndex = 20
        Me.pbClearLogoCache.TabStop = False
        Me.pbClearLogoCache.Visible = False
        '
        'pnlClearArt
        '
        Me.pnlClearArt.AutoSize = True
        Me.pnlClearArt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlClearArt.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlClearArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlClearArt.Controls.Add(Me.pnlClearArtMain)
        Me.pnlClearArt.Controls.Add(Me.pnlClearArtBottom)
        Me.pnlClearArt.Controls.Add(Me.pnlClearArtTop)
        Me.pnlClearArt.Location = New System.Drawing.Point(172, 130)
        Me.pnlClearArt.Name = "pnlClearArt"
        Me.pnlClearArt.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlClearArt.Size = New System.Drawing.Size(56, 82)
        Me.pnlClearArt.TabIndex = 19
        Me.pnlClearArt.Visible = False
        '
        'pnlClearArtMain
        '
        Me.pnlClearArtMain.AutoSize = True
        Me.pnlClearArtMain.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlClearArtMain.Controls.Add(Me.tblClearArtMain)
        Me.pnlClearArtMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlClearArtMain.Location = New System.Drawing.Point(2, 15)
        Me.pnlClearArtMain.Name = "pnlClearArtMain"
        Me.pnlClearArtMain.Size = New System.Drawing.Size(50, 50)
        Me.pnlClearArtMain.TabIndex = 3
        '
        'tblClearArtMain
        '
        Me.tblClearArtMain.AutoSize = True
        Me.tblClearArtMain.ColumnCount = 1
        Me.tblClearArtMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearArtMain.Controls.Add(Me.pbClearArt, 0, 0)
        Me.tblClearArtMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblClearArtMain.Location = New System.Drawing.Point(0, 0)
        Me.tblClearArtMain.Name = "tblClearArtMain"
        Me.tblClearArtMain.RowCount = 1
        Me.tblClearArtMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearArtMain.Size = New System.Drawing.Size(50, 50)
        Me.tblClearArtMain.TabIndex = 1
        '
        'pbClearArt
        '
        Me.pbClearArt.BackColor = System.Drawing.Color.Gray
        Me.pbClearArt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbClearArt.Location = New System.Drawing.Point(0, 0)
        Me.pbClearArt.Margin = New System.Windows.Forms.Padding(0)
        Me.pbClearArt.Name = "pbClearArt"
        Me.pbClearArt.Size = New System.Drawing.Size(50, 50)
        Me.pbClearArt.TabIndex = 0
        Me.pbClearArt.TabStop = False
        '
        'pnlClearArtBottom
        '
        Me.pnlClearArtBottom.AutoSize = True
        Me.pnlClearArtBottom.BackColor = System.Drawing.Color.DimGray
        Me.pnlClearArtBottom.Controls.Add(Me.tblClearArtBottom)
        Me.pnlClearArtBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlClearArtBottom.Location = New System.Drawing.Point(2, 65)
        Me.pnlClearArtBottom.Name = "pnlClearArtBottom"
        Me.pnlClearArtBottom.Size = New System.Drawing.Size(50, 13)
        Me.pnlClearArtBottom.TabIndex = 2
        '
        'tblClearArtBottom
        '
        Me.tblClearArtBottom.AutoSize = True
        Me.tblClearArtBottom.ColumnCount = 3
        Me.tblClearArtBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblClearArtBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearArtBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblClearArtBottom.Controls.Add(Me.lblClearArtSize, 1, 0)
        Me.tblClearArtBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblClearArtBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblClearArtBottom.Name = "tblClearArtBottom"
        Me.tblClearArtBottom.RowCount = 2
        Me.tblClearArtBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearArtBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearArtBottom.Size = New System.Drawing.Size(50, 13)
        Me.tblClearArtBottom.TabIndex = 0
        '
        'lblClearArtSize
        '
        Me.lblClearArtSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblClearArtSize.AutoSize = True
        Me.lblClearArtSize.BackColor = System.Drawing.Color.Transparent
        Me.lblClearArtSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblClearArtSize.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblClearArtSize.Location = New System.Drawing.Point(11, 0)
        Me.lblClearArtSize.Name = "lblClearArtSize"
        Me.lblClearArtSize.Size = New System.Drawing.Size(27, 13)
        Me.lblClearArtSize.TabIndex = 2
        Me.lblClearArtSize.Text = "Size"
        Me.lblClearArtSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlClearArtTop
        '
        Me.pnlClearArtTop.AutoSize = True
        Me.pnlClearArtTop.BackColor = System.Drawing.Color.DimGray
        Me.pnlClearArtTop.Controls.Add(Me.tblClearArtTop)
        Me.pnlClearArtTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlClearArtTop.Location = New System.Drawing.Point(2, 2)
        Me.pnlClearArtTop.Name = "pnlClearArtTop"
        Me.pnlClearArtTop.Size = New System.Drawing.Size(50, 13)
        Me.pnlClearArtTop.TabIndex = 1
        '
        'tblClearArtTop
        '
        Me.tblClearArtTop.AutoSize = True
        Me.tblClearArtTop.ColumnCount = 3
        Me.tblClearArtTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblClearArtTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearArtTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblClearArtTop.Controls.Add(Me.lblClearArtTitle, 1, 0)
        Me.tblClearArtTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblClearArtTop.Location = New System.Drawing.Point(0, 0)
        Me.tblClearArtTop.Name = "tblClearArtTop"
        Me.tblClearArtTop.RowCount = 2
        Me.tblClearArtTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearArtTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearArtTop.Size = New System.Drawing.Size(50, 13)
        Me.tblClearArtTop.TabIndex = 0
        '
        'lblClearArtTitle
        '
        Me.lblClearArtTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblClearArtTitle.AutoSize = True
        Me.lblClearArtTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblClearArtTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblClearArtTitle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblClearArtTitle.Location = New System.Drawing.Point(1, 0)
        Me.lblClearArtTitle.Name = "lblClearArtTitle"
        Me.lblClearArtTitle.Size = New System.Drawing.Size(49, 13)
        Me.lblClearArtTitle.TabIndex = 1
        Me.lblClearArtTitle.Text = "ClearArt"
        Me.lblClearArtTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlLandscape
        '
        Me.pnlLandscape.AutoSize = True
        Me.pnlLandscape.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlLandscape.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlLandscape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlLandscape.Controls.Add(Me.pnlLandscapeMain)
        Me.pnlLandscape.Controls.Add(Me.pnlLandscapeBottom)
        Me.pnlLandscape.Controls.Add(Me.pnlLandscapeTop)
        Me.pnlLandscape.Location = New System.Drawing.Point(116, 130)
        Me.pnlLandscape.Name = "pnlLandscape"
        Me.pnlLandscape.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlLandscape.Size = New System.Drawing.Size(56, 82)
        Me.pnlLandscape.TabIndex = 18
        Me.pnlLandscape.Visible = False
        '
        'pnlLandscapeMain
        '
        Me.pnlLandscapeMain.AutoSize = True
        Me.pnlLandscapeMain.Controls.Add(Me.tblLandscapeMain)
        Me.pnlLandscapeMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLandscapeMain.Location = New System.Drawing.Point(2, 15)
        Me.pnlLandscapeMain.Name = "pnlLandscapeMain"
        Me.pnlLandscapeMain.Size = New System.Drawing.Size(50, 50)
        Me.pnlLandscapeMain.TabIndex = 3
        '
        'tblLandscapeMain
        '
        Me.tblLandscapeMain.AutoSize = True
        Me.tblLandscapeMain.ColumnCount = 1
        Me.tblLandscapeMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblLandscapeMain.Controls.Add(Me.pbLandscape, 0, 0)
        Me.tblLandscapeMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblLandscapeMain.Location = New System.Drawing.Point(0, 0)
        Me.tblLandscapeMain.Name = "tblLandscapeMain"
        Me.tblLandscapeMain.RowCount = 1
        Me.tblLandscapeMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLandscapeMain.Size = New System.Drawing.Size(50, 50)
        Me.tblLandscapeMain.TabIndex = 1
        '
        'pbLandscape
        '
        Me.pbLandscape.BackColor = System.Drawing.Color.Gray
        Me.pbLandscape.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbLandscape.Location = New System.Drawing.Point(0, 0)
        Me.pbLandscape.Margin = New System.Windows.Forms.Padding(0)
        Me.pbLandscape.Name = "pbLandscape"
        Me.pbLandscape.Size = New System.Drawing.Size(50, 50)
        Me.pbLandscape.TabIndex = 0
        Me.pbLandscape.TabStop = False
        '
        'pnlLandscapeBottom
        '
        Me.pnlLandscapeBottom.AutoSize = True
        Me.pnlLandscapeBottom.BackColor = System.Drawing.Color.DimGray
        Me.pnlLandscapeBottom.Controls.Add(Me.tblLandscapeBottom)
        Me.pnlLandscapeBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlLandscapeBottom.Location = New System.Drawing.Point(2, 65)
        Me.pnlLandscapeBottom.Name = "pnlLandscapeBottom"
        Me.pnlLandscapeBottom.Size = New System.Drawing.Size(50, 13)
        Me.pnlLandscapeBottom.TabIndex = 2
        '
        'tblLandscapeBottom
        '
        Me.tblLandscapeBottom.AutoSize = True
        Me.tblLandscapeBottom.ColumnCount = 3
        Me.tblLandscapeBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblLandscapeBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblLandscapeBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblLandscapeBottom.Controls.Add(Me.lblLandscapeSize, 1, 0)
        Me.tblLandscapeBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblLandscapeBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblLandscapeBottom.Name = "tblLandscapeBottom"
        Me.tblLandscapeBottom.RowCount = 2
        Me.tblLandscapeBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLandscapeBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLandscapeBottom.Size = New System.Drawing.Size(50, 13)
        Me.tblLandscapeBottom.TabIndex = 0
        '
        'lblLandscapeSize
        '
        Me.lblLandscapeSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblLandscapeSize.AutoSize = True
        Me.lblLandscapeSize.BackColor = System.Drawing.Color.Transparent
        Me.lblLandscapeSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblLandscapeSize.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblLandscapeSize.Location = New System.Drawing.Point(11, 0)
        Me.lblLandscapeSize.Name = "lblLandscapeSize"
        Me.lblLandscapeSize.Size = New System.Drawing.Size(27, 13)
        Me.lblLandscapeSize.TabIndex = 2
        Me.lblLandscapeSize.Text = "Size"
        Me.lblLandscapeSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlLandscapeTop
        '
        Me.pnlLandscapeTop.AutoSize = True
        Me.pnlLandscapeTop.BackColor = System.Drawing.Color.DimGray
        Me.pnlLandscapeTop.Controls.Add(Me.tblLandscapeTop)
        Me.pnlLandscapeTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLandscapeTop.Location = New System.Drawing.Point(2, 2)
        Me.pnlLandscapeTop.Name = "pnlLandscapeTop"
        Me.pnlLandscapeTop.Size = New System.Drawing.Size(50, 13)
        Me.pnlLandscapeTop.TabIndex = 1
        '
        'tblLandscapeTop
        '
        Me.tblLandscapeTop.AutoSize = True
        Me.tblLandscapeTop.ColumnCount = 3
        Me.tblLandscapeTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblLandscapeTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblLandscapeTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblLandscapeTop.Controls.Add(Me.lblLandscapeTitle, 1, 0)
        Me.tblLandscapeTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblLandscapeTop.Location = New System.Drawing.Point(0, 0)
        Me.tblLandscapeTop.Name = "tblLandscapeTop"
        Me.tblLandscapeTop.RowCount = 2
        Me.tblLandscapeTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLandscapeTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLandscapeTop.Size = New System.Drawing.Size(50, 13)
        Me.tblLandscapeTop.TabIndex = 0
        '
        'lblLandscapeTitle
        '
        Me.lblLandscapeTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblLandscapeTitle.AutoSize = True
        Me.lblLandscapeTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblLandscapeTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblLandscapeTitle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblLandscapeTitle.Location = New System.Drawing.Point(-6, 0)
        Me.lblLandscapeTitle.Name = "lblLandscapeTitle"
        Me.lblLandscapeTitle.Size = New System.Drawing.Size(62, 13)
        Me.lblLandscapeTitle.TabIndex = 1
        Me.lblLandscapeTitle.Text = "Landscape"
        Me.lblLandscapeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlFanartSmall
        '
        Me.pnlFanartSmall.AutoSize = True
        Me.pnlFanartSmall.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlFanartSmall.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlFanartSmall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFanartSmall.Controls.Add(Me.pnlFanartSmallMain)
        Me.pnlFanartSmall.Controls.Add(Me.pnlFanartSmallBottom)
        Me.pnlFanartSmall.Controls.Add(Me.pnlFanartSmallTop)
        Me.pnlFanartSmall.Location = New System.Drawing.Point(60, 130)
        Me.pnlFanartSmall.Name = "pnlFanartSmall"
        Me.pnlFanartSmall.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlFanartSmall.Size = New System.Drawing.Size(56, 82)
        Me.pnlFanartSmall.TabIndex = 14
        Me.pnlFanartSmall.Visible = False
        '
        'pnlFanartSmallMain
        '
        Me.pnlFanartSmallMain.AutoSize = True
        Me.pnlFanartSmallMain.Controls.Add(Me.tblFanartSmallMain)
        Me.pnlFanartSmallMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFanartSmallMain.Location = New System.Drawing.Point(2, 15)
        Me.pnlFanartSmallMain.Name = "pnlFanartSmallMain"
        Me.pnlFanartSmallMain.Size = New System.Drawing.Size(50, 50)
        Me.pnlFanartSmallMain.TabIndex = 3
        '
        'tblFanartSmallMain
        '
        Me.tblFanartSmallMain.AutoSize = True
        Me.tblFanartSmallMain.ColumnCount = 1
        Me.tblFanartSmallMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFanartSmallMain.Controls.Add(Me.pbFanartSmall, 0, 0)
        Me.tblFanartSmallMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFanartSmallMain.Location = New System.Drawing.Point(0, 0)
        Me.tblFanartSmallMain.Name = "tblFanartSmallMain"
        Me.tblFanartSmallMain.RowCount = 1
        Me.tblFanartSmallMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFanartSmallMain.Size = New System.Drawing.Size(50, 50)
        Me.tblFanartSmallMain.TabIndex = 1
        '
        'pbFanartSmall
        '
        Me.pbFanartSmall.BackColor = System.Drawing.Color.Gray
        Me.pbFanartSmall.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbFanartSmall.Location = New System.Drawing.Point(0, 0)
        Me.pbFanartSmall.Margin = New System.Windows.Forms.Padding(0)
        Me.pbFanartSmall.Name = "pbFanartSmall"
        Me.pbFanartSmall.Size = New System.Drawing.Size(50, 50)
        Me.pbFanartSmall.TabIndex = 0
        Me.pbFanartSmall.TabStop = False
        '
        'pnlFanartSmallBottom
        '
        Me.pnlFanartSmallBottom.AutoSize = True
        Me.pnlFanartSmallBottom.BackColor = System.Drawing.Color.DimGray
        Me.pnlFanartSmallBottom.Controls.Add(Me.tblFanartSmallBottom)
        Me.pnlFanartSmallBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFanartSmallBottom.Location = New System.Drawing.Point(2, 65)
        Me.pnlFanartSmallBottom.Name = "pnlFanartSmallBottom"
        Me.pnlFanartSmallBottom.Size = New System.Drawing.Size(50, 13)
        Me.pnlFanartSmallBottom.TabIndex = 2
        '
        'tblFanartSmallBottom
        '
        Me.tblFanartSmallBottom.AutoSize = True
        Me.tblFanartSmallBottom.ColumnCount = 3
        Me.tblFanartSmallBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblFanartSmallBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFanartSmallBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblFanartSmallBottom.Controls.Add(Me.lblFanartSmallSize, 1, 0)
        Me.tblFanartSmallBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFanartSmallBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblFanartSmallBottom.Name = "tblFanartSmallBottom"
        Me.tblFanartSmallBottom.RowCount = 2
        Me.tblFanartSmallBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFanartSmallBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFanartSmallBottom.Size = New System.Drawing.Size(50, 13)
        Me.tblFanartSmallBottom.TabIndex = 0
        '
        'lblFanartSmallSize
        '
        Me.lblFanartSmallSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFanartSmallSize.AutoSize = True
        Me.lblFanartSmallSize.BackColor = System.Drawing.Color.Transparent
        Me.lblFanartSmallSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFanartSmallSize.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFanartSmallSize.Location = New System.Drawing.Point(11, 0)
        Me.lblFanartSmallSize.Name = "lblFanartSmallSize"
        Me.lblFanartSmallSize.Size = New System.Drawing.Size(27, 13)
        Me.lblFanartSmallSize.TabIndex = 2
        Me.lblFanartSmallSize.Text = "Size"
        Me.lblFanartSmallSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlFanartSmallTop
        '
        Me.pnlFanartSmallTop.AutoSize = True
        Me.pnlFanartSmallTop.BackColor = System.Drawing.Color.DimGray
        Me.pnlFanartSmallTop.Controls.Add(Me.tblFanartSmallTop)
        Me.pnlFanartSmallTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFanartSmallTop.Location = New System.Drawing.Point(2, 2)
        Me.pnlFanartSmallTop.Name = "pnlFanartSmallTop"
        Me.pnlFanartSmallTop.Size = New System.Drawing.Size(50, 13)
        Me.pnlFanartSmallTop.TabIndex = 1
        '
        'tblFanartSmallTop
        '
        Me.tblFanartSmallTop.AutoSize = True
        Me.tblFanartSmallTop.ColumnCount = 3
        Me.tblFanartSmallTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblFanartSmallTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFanartSmallTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblFanartSmallTop.Controls.Add(Me.lblFanartSmallTitle, 1, 0)
        Me.tblFanartSmallTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFanartSmallTop.Location = New System.Drawing.Point(0, 0)
        Me.tblFanartSmallTop.Name = "tblFanartSmallTop"
        Me.tblFanartSmallTop.RowCount = 2
        Me.tblFanartSmallTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFanartSmallTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFanartSmallTop.Size = New System.Drawing.Size(50, 13)
        Me.tblFanartSmallTop.TabIndex = 0
        '
        'lblFanartSmallTitle
        '
        Me.lblFanartSmallTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFanartSmallTitle.AutoSize = True
        Me.lblFanartSmallTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblFanartSmallTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFanartSmallTitle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFanartSmallTitle.Location = New System.Drawing.Point(5, 0)
        Me.lblFanartSmallTitle.Name = "lblFanartSmallTitle"
        Me.lblFanartSmallTitle.Size = New System.Drawing.Size(40, 13)
        Me.lblFanartSmallTitle.TabIndex = 1
        Me.lblFanartSmallTitle.Text = "Fanart"
        Me.lblFanartSmallTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlPoster
        '
        Me.pnlPoster.AutoSize = True
        Me.pnlPoster.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlPoster.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPoster.Controls.Add(Me.pnlPosterMain)
        Me.pnlPoster.Controls.Add(Me.pnlPosterBottom)
        Me.pnlPoster.Controls.Add(Me.pnlPosterTop)
        Me.pnlPoster.Location = New System.Drawing.Point(4, 130)
        Me.pnlPoster.Name = "pnlPoster"
        Me.pnlPoster.Padding = New System.Windows.Forms.Padding(2)
        Me.pnlPoster.Size = New System.Drawing.Size(56, 82)
        Me.pnlPoster.TabIndex = 2
        Me.pnlPoster.Visible = False
        '
        'pnlPosterMain
        '
        Me.pnlPosterMain.AutoSize = True
        Me.pnlPosterMain.Controls.Add(Me.tblPosterMain)
        Me.pnlPosterMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPosterMain.Location = New System.Drawing.Point(2, 15)
        Me.pnlPosterMain.Name = "pnlPosterMain"
        Me.pnlPosterMain.Size = New System.Drawing.Size(50, 50)
        Me.pnlPosterMain.TabIndex = 3
        '
        'tblPosterMain
        '
        Me.tblPosterMain.AutoSize = True
        Me.tblPosterMain.ColumnCount = 1
        Me.tblPosterMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblPosterMain.Controls.Add(Me.pbPoster, 0, 0)
        Me.tblPosterMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblPosterMain.Location = New System.Drawing.Point(0, 0)
        Me.tblPosterMain.Name = "tblPosterMain"
        Me.tblPosterMain.RowCount = 1
        Me.tblPosterMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblPosterMain.Size = New System.Drawing.Size(50, 50)
        Me.tblPosterMain.TabIndex = 1
        '
        'pbPoster
        '
        Me.pbPoster.BackColor = System.Drawing.Color.Gray
        Me.pbPoster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbPoster.Location = New System.Drawing.Point(0, 0)
        Me.pbPoster.Margin = New System.Windows.Forms.Padding(0)
        Me.pbPoster.Name = "pbPoster"
        Me.pbPoster.Size = New System.Drawing.Size(50, 50)
        Me.pbPoster.TabIndex = 0
        Me.pbPoster.TabStop = False
        '
        'pnlPosterBottom
        '
        Me.pnlPosterBottom.AutoSize = True
        Me.pnlPosterBottom.BackColor = System.Drawing.Color.DimGray
        Me.pnlPosterBottom.Controls.Add(Me.tblPosterBottom)
        Me.pnlPosterBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlPosterBottom.Location = New System.Drawing.Point(2, 65)
        Me.pnlPosterBottom.Name = "pnlPosterBottom"
        Me.pnlPosterBottom.Size = New System.Drawing.Size(50, 13)
        Me.pnlPosterBottom.TabIndex = 2
        '
        'tblPosterBottom
        '
        Me.tblPosterBottom.AutoSize = True
        Me.tblPosterBottom.ColumnCount = 3
        Me.tblPosterBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblPosterBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblPosterBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblPosterBottom.Controls.Add(Me.lblPosterSize, 1, 0)
        Me.tblPosterBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblPosterBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblPosterBottom.Name = "tblPosterBottom"
        Me.tblPosterBottom.RowCount = 2
        Me.tblPosterBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblPosterBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblPosterBottom.Size = New System.Drawing.Size(50, 13)
        Me.tblPosterBottom.TabIndex = 0
        '
        'lblPosterSize
        '
        Me.lblPosterSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPosterSize.AutoSize = True
        Me.lblPosterSize.BackColor = System.Drawing.Color.Transparent
        Me.lblPosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPosterSize.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblPosterSize.Location = New System.Drawing.Point(11, 0)
        Me.lblPosterSize.Name = "lblPosterSize"
        Me.lblPosterSize.Size = New System.Drawing.Size(27, 13)
        Me.lblPosterSize.TabIndex = 2
        Me.lblPosterSize.Text = "Size"
        Me.lblPosterSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlPosterTop
        '
        Me.pnlPosterTop.AutoSize = True
        Me.pnlPosterTop.BackColor = System.Drawing.Color.DimGray
        Me.pnlPosterTop.Controls.Add(Me.tblPosterTop)
        Me.pnlPosterTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPosterTop.Location = New System.Drawing.Point(2, 2)
        Me.pnlPosterTop.Name = "pnlPosterTop"
        Me.pnlPosterTop.Size = New System.Drawing.Size(50, 13)
        Me.pnlPosterTop.TabIndex = 1
        '
        'tblPosterTop
        '
        Me.tblPosterTop.AutoSize = True
        Me.tblPosterTop.ColumnCount = 3
        Me.tblPosterTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblPosterTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblPosterTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblPosterTop.Controls.Add(Me.lblPosterTitle, 1, 0)
        Me.tblPosterTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblPosterTop.Location = New System.Drawing.Point(0, 0)
        Me.tblPosterTop.Name = "tblPosterTop"
        Me.tblPosterTop.RowCount = 2
        Me.tblPosterTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblPosterTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblPosterTop.Size = New System.Drawing.Size(50, 13)
        Me.tblPosterTop.TabIndex = 0
        '
        'lblPosterTitle
        '
        Me.lblPosterTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPosterTitle.AutoSize = True
        Me.lblPosterTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblPosterTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPosterTitle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblPosterTitle.Location = New System.Drawing.Point(5, 0)
        Me.lblPosterTitle.Name = "lblPosterTitle"
        Me.lblPosterTitle.Size = New System.Drawing.Size(40, 13)
        Me.lblPosterTitle.TabIndex = 1
        Me.lblPosterTitle.Text = "Poster"
        Me.lblPosterTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.pnlTop.Visible = False
        '
        'tlpHeader
        '
        Me.tlpHeader.AutoSize = True
        Me.tlpHeader.ColumnCount = 3
        Me.tlpHeader.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
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
        Me.tlpHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpHeader.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpHeader.Size = New System.Drawing.Size(346, 96)
        Me.tlpHeader.TabIndex = 39
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.Color.Transparent
        Me.tlpHeader.SetColumnSpan(Me.lblTitle, 3)
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.Black
        Me.lblTitle.Location = New System.Drawing.Point(3, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.lblTitle.Size = New System.Drawing.Size(43, 22)
        Me.lblTitle.TabIndex = 25
        Me.lblTitle.Text = "Title"
        Me.lblTitle.UseMnemonic = False
        '
        'lblOriginalTitle
        '
        Me.lblOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblOriginalTitle.AutoSize = True
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
        Me.lblOriginalTitle.UseMnemonic = False
        '
        'pnlRating
        '
        Me.pnlRating.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.pnlRating.AutoSize = True
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
        Me.pbStar10.TabStop = False
        '
        'pbStar9
        '
        Me.pbStar9.Location = New System.Drawing.Point(193, 1)
        Me.pbStar9.Name = "pbStar9"
        Me.pbStar9.Size = New System.Drawing.Size(24, 24)
        Me.pbStar9.TabIndex = 31
        Me.pbStar9.TabStop = False
        '
        'pbStar8
        '
        Me.pbStar8.Location = New System.Drawing.Point(169, 1)
        Me.pbStar8.Name = "pbStar8"
        Me.pbStar8.Size = New System.Drawing.Size(24, 24)
        Me.pbStar8.TabIndex = 30
        Me.pbStar8.TabStop = False
        '
        'pbStar7
        '
        Me.pbStar7.Location = New System.Drawing.Point(145, 1)
        Me.pbStar7.Name = "pbStar7"
        Me.pbStar7.Size = New System.Drawing.Size(24, 24)
        Me.pbStar7.TabIndex = 29
        Me.pbStar7.TabStop = False
        '
        'pbStar6
        '
        Me.pbStar6.Location = New System.Drawing.Point(121, 1)
        Me.pbStar6.Name = "pbStar6"
        Me.pbStar6.Size = New System.Drawing.Size(24, 24)
        Me.pbStar6.TabIndex = 28
        Me.pbStar6.TabStop = False
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
        'lblTagline
        '
        Me.lblTagline.AutoSize = True
        Me.lblTagline.BackColor = System.Drawing.Color.Transparent
        Me.tlpHeader.SetColumnSpan(Me.lblTagline, 3)
        Me.lblTagline.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTagline.ForeColor = System.Drawing.Color.Black
        Me.lblTagline.Location = New System.Drawing.Point(3, 72)
        Me.lblTagline.Name = "lblTagline"
        Me.lblTagline.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.lblTagline.Size = New System.Drawing.Size(45, 16)
        Me.lblTagline.TabIndex = 26
        Me.lblTagline.Text = "Tagline"
        Me.lblTagline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTagline.UseMnemonic = False
        '
        'lblRuntime
        '
        Me.lblRuntime.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRuntime.AutoSize = True
        Me.lblRuntime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.lblRating.AutoSize = True
        Me.lblRating.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.pbSubtitleLang6.TabStop = False
        '
        'pbSubtitleLang5
        '
        Me.pbSubtitleLang5.Location = New System.Drawing.Point(325, 70)
        Me.pbSubtitleLang5.Name = "pbSubtitleLang5"
        Me.pbSubtitleLang5.Size = New System.Drawing.Size(25, 25)
        Me.pbSubtitleLang5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSubtitleLang5.TabIndex = 52
        Me.pbSubtitleLang5.TabStop = False
        '
        'pbSubtitleLang4
        '
        Me.pbSubtitleLang4.Location = New System.Drawing.Point(299, 70)
        Me.pbSubtitleLang4.Name = "pbSubtitleLang4"
        Me.pbSubtitleLang4.Size = New System.Drawing.Size(25, 25)
        Me.pbSubtitleLang4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSubtitleLang4.TabIndex = 51
        Me.pbSubtitleLang4.TabStop = False
        '
        'pbSubtitleLang3
        '
        Me.pbSubtitleLang3.Location = New System.Drawing.Point(273, 70)
        Me.pbSubtitleLang3.Name = "pbSubtitleLang3"
        Me.pbSubtitleLang3.Size = New System.Drawing.Size(25, 25)
        Me.pbSubtitleLang3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSubtitleLang3.TabIndex = 50
        Me.pbSubtitleLang3.TabStop = False
        '
        'pbSubtitleLang2
        '
        Me.pbSubtitleLang2.Location = New System.Drawing.Point(247, 70)
        Me.pbSubtitleLang2.Name = "pbSubtitleLang2"
        Me.pbSubtitleLang2.Size = New System.Drawing.Size(25, 25)
        Me.pbSubtitleLang2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSubtitleLang2.TabIndex = 49
        Me.pbSubtitleLang2.TabStop = False
        '
        'pbSubtitleLang1
        '
        Me.pbSubtitleLang1.Location = New System.Drawing.Point(221, 70)
        Me.pbSubtitleLang1.Name = "pbSubtitleLang1"
        Me.pbSubtitleLang1.Size = New System.Drawing.Size(25, 25)
        Me.pbSubtitleLang1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSubtitleLang1.TabIndex = 48
        Me.pbSubtitleLang1.TabStop = False
        '
        'pbSubtitleLang0
        '
        Me.pbSubtitleLang0.Location = New System.Drawing.Point(195, 70)
        Me.pbSubtitleLang0.Name = "pbSubtitleLang0"
        Me.pbSubtitleLang0.Size = New System.Drawing.Size(25, 25)
        Me.pbSubtitleLang0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSubtitleLang0.TabIndex = 47
        Me.pbSubtitleLang0.TabStop = False
        '
        'pbAudioLang6
        '
        Me.pbAudioLang6.Location = New System.Drawing.Point(156, 70)
        Me.pbAudioLang6.Name = "pbAudioLang6"
        Me.pbAudioLang6.Size = New System.Drawing.Size(25, 25)
        Me.pbAudioLang6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudioLang6.TabIndex = 46
        Me.pbAudioLang6.TabStop = False
        '
        'pbAudioLang5
        '
        Me.pbAudioLang5.Location = New System.Drawing.Point(130, 70)
        Me.pbAudioLang5.Name = "pbAudioLang5"
        Me.pbAudioLang5.Size = New System.Drawing.Size(25, 25)
        Me.pbAudioLang5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudioLang5.TabIndex = 45
        Me.pbAudioLang5.TabStop = False
        '
        'pbAudioLang4
        '
        Me.pbAudioLang4.Location = New System.Drawing.Point(104, 70)
        Me.pbAudioLang4.Name = "pbAudioLang4"
        Me.pbAudioLang4.Size = New System.Drawing.Size(25, 25)
        Me.pbAudioLang4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudioLang4.TabIndex = 44
        Me.pbAudioLang4.TabStop = False
        '
        'pbAudioLang3
        '
        Me.pbAudioLang3.Location = New System.Drawing.Point(78, 70)
        Me.pbAudioLang3.Name = "pbAudioLang3"
        Me.pbAudioLang3.Size = New System.Drawing.Size(25, 25)
        Me.pbAudioLang3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudioLang3.TabIndex = 43
        Me.pbAudioLang3.TabStop = False
        '
        'pbAudioLang2
        '
        Me.pbAudioLang2.Location = New System.Drawing.Point(52, 70)
        Me.pbAudioLang2.Name = "pbAudioLang2"
        Me.pbAudioLang2.Size = New System.Drawing.Size(25, 25)
        Me.pbAudioLang2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudioLang2.TabIndex = 42
        Me.pbAudioLang2.TabStop = False
        '
        'pbAudioLang1
        '
        Me.pbAudioLang1.Location = New System.Drawing.Point(26, 70)
        Me.pbAudioLang1.Name = "pbAudioLang1"
        Me.pbAudioLang1.Size = New System.Drawing.Size(25, 25)
        Me.pbAudioLang1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudioLang1.TabIndex = 41
        Me.pbAudioLang1.TabStop = False
        '
        'pbAudioLang0
        '
        Me.pbAudioLang0.Location = New System.Drawing.Point(0, 70)
        Me.pbAudioLang0.Name = "pbAudioLang0"
        Me.pbAudioLang0.Size = New System.Drawing.Size(25, 25)
        Me.pbAudioLang0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudioLang0.TabIndex = 40
        Me.pbAudioLang0.TabStop = False
        '
        'lblStudio
        '
        Me.lblStudio.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStudio.Location = New System.Drawing.Point(220, 5)
        Me.lblStudio.Name = "lblStudio"
        Me.lblStudio.Size = New System.Drawing.Size(167, 18)
        Me.lblStudio.TabIndex = 37
        Me.lblStudio.Text = "Studios"
        Me.lblStudio.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblStudio.UseMnemonic = False
        '
        'pbVType
        '
        Me.pbVType.BackColor = System.Drawing.Color.Transparent
        Me.pbVType.Location = New System.Drawing.Point(65, 26)
        Me.pbVType.Name = "pbVType"
        Me.pbVType.Size = New System.Drawing.Size(64, 44)
        Me.pbVType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbVType.TabIndex = 36
        Me.pbVType.TabStop = False
        '
        'pbStudio
        '
        Me.pbStudio.BackColor = System.Drawing.Color.Transparent
        Me.pbStudio.Location = New System.Drawing.Point(325, 26)
        Me.pbStudio.Name = "pbStudio"
        Me.pbStudio.Size = New System.Drawing.Size(64, 44)
        Me.pbStudio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbStudio.TabIndex = 31
        Me.pbStudio.TabStop = False
        '
        'pbVideo
        '
        Me.pbVideo.BackColor = System.Drawing.Color.Transparent
        Me.pbVideo.Location = New System.Drawing.Point(0, 26)
        Me.pbVideo.Name = "pbVideo"
        Me.pbVideo.Size = New System.Drawing.Size(64, 44)
        Me.pbVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbVideo.TabIndex = 33
        Me.pbVideo.TabStop = False
        '
        'pbAudio
        '
        Me.pbAudio.BackColor = System.Drawing.Color.Transparent
        Me.pbAudio.Location = New System.Drawing.Point(195, 26)
        Me.pbAudio.Name = "pbAudio"
        Me.pbAudio.Size = New System.Drawing.Size(64, 44)
        Me.pbAudio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbAudio.TabIndex = 35
        Me.pbAudio.TabStop = False
        '
        'pbResolution
        '
        Me.pbResolution.BackColor = System.Drawing.Color.Transparent
        Me.pbResolution.Location = New System.Drawing.Point(130, 26)
        Me.pbResolution.Name = "pbResolution"
        Me.pbResolution.Size = New System.Drawing.Size(64, 44)
        Me.pbResolution.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbResolution.TabIndex = 34
        Me.pbResolution.TabStop = False
        '
        'pbChannels
        '
        Me.pbChannels.BackColor = System.Drawing.Color.Transparent
        Me.pbChannels.Location = New System.Drawing.Point(260, 26)
        Me.pbChannels.Name = "pbChannels"
        Me.pbChannels.Size = New System.Drawing.Size(64, 44)
        Me.pbChannels.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbChannels.TabIndex = 32
        Me.pbChannels.TabStop = False
        '
        'pbPosterCache
        '
        Me.pbPosterCache.Location = New System.Drawing.Point(740, 130)
        Me.pbPosterCache.Name = "pbPosterCache"
        Me.pbPosterCache.Size = New System.Drawing.Size(50, 50)
        Me.pbPosterCache.TabIndex = 12
        Me.pbPosterCache.TabStop = False
        Me.pbPosterCache.Visible = False
        '
        'pbFanartSmallCache
        '
        Me.pbFanartSmallCache.Location = New System.Drawing.Point(740, 130)
        Me.pbFanartSmallCache.Name = "pbFanartSmallCache"
        Me.pbFanartSmallCache.Size = New System.Drawing.Size(50, 50)
        Me.pbFanartSmallCache.TabIndex = 15
        Me.pbFanartSmallCache.TabStop = False
        Me.pbFanartSmallCache.Visible = False
        '
        'pbLandscapeCache
        '
        Me.pbLandscapeCache.Location = New System.Drawing.Point(740, 130)
        Me.pbLandscapeCache.Name = "pbLandscapeCache"
        Me.pbLandscapeCache.Size = New System.Drawing.Size(50, 50)
        Me.pbLandscapeCache.TabIndex = 16
        Me.pbLandscapeCache.TabStop = False
        Me.pbLandscapeCache.Visible = False
        '
        'pbClearArtCache
        '
        Me.pbClearArtCache.Location = New System.Drawing.Point(818, 107)
        Me.pbClearArtCache.Name = "pbClearArtCache"
        Me.pbClearArtCache.Size = New System.Drawing.Size(115, 111)
        Me.pbClearArtCache.TabIndex = 17
        Me.pbClearArtCache.TabStop = False
        Me.pbClearArtCache.Visible = False
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
        Me.pbFanartCache.Location = New System.Drawing.Point(740, 130)
        Me.pbFanartCache.Name = "pbFanartCache"
        Me.pbFanartCache.Size = New System.Drawing.Size(50, 50)
        Me.pbFanartCache.TabIndex = 3
        Me.pbFanartCache.TabStop = False
        Me.pbFanartCache.Visible = False
        '
        'pbFanart
        '
        Me.pbFanart.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pbFanart.BackColor = System.Drawing.Color.DimGray
        Me.pbFanart.Location = New System.Drawing.Point(38, 123)
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
        Me.tsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuScrapeMovies, Me.mnuScrapeMovieSets, Me.mnuScrapeTVShows, Me.mnuUpdate})
        Me.tsMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.tsMain.Location = New System.Drawing.Point(0, 0)
        Me.tsMain.Name = "tsMain"
        Me.tsMain.Padding = New System.Windows.Forms.Padding(0)
        Me.tsMain.Size = New System.Drawing.Size(773, 25)
        Me.tsMain.Stretch = True
        Me.tsMain.TabIndex = 6
        '
        'mnuScrapeMovies
        '
        Me.mnuScrapeMovies.DropDown = Me.mnuScrapeSubmenu
        Me.mnuScrapeMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.mnuScrapeMovies.Image = CType(resources.GetObject("mnuScrapeMovies.Image"), System.Drawing.Image)
        Me.mnuScrapeMovies.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuScrapeMovies.Name = "mnuScrapeMovies"
        Me.mnuScrapeMovies.Size = New System.Drawing.Size(112, 22)
        Me.mnuScrapeMovies.Tag = "movie"
        Me.mnuScrapeMovies.Text = "Scrape Movies"
        '
        'mnuScrapeSubmenu
        '
        Me.mnuScrapeSubmenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuScrapeSubmenuAll, Me.mnuScrapeSubmenuMissing, Me.mnuScrapeSubmenuNew, Me.mnuScrapeSubmenuMarked, Me.mnuScrapeSubmenuFilter, Me.mnuScrapeSubmenuCustom})
        Me.mnuScrapeSubmenu.Name = "mnuScrapeSubmenu"
        Me.mnuScrapeSubmenu.OwnerItem = Me.cmnuTrayScrapeMovies
        Me.mnuScrapeSubmenu.Size = New System.Drawing.Size(168, 136)
        '
        'mnuScrapeSubmenuAll
        '
        Me.mnuScrapeSubmenuAll.DropDown = Me.mnuScrapeType
        Me.mnuScrapeSubmenuAll.Name = "mnuScrapeSubmenuAll"
        Me.mnuScrapeSubmenuAll.Size = New System.Drawing.Size(167, 22)
        Me.mnuScrapeSubmenuAll.Tag = "all"
        Me.mnuScrapeSubmenuAll.Text = "All"
        '
        'mnuScrapeSubmenuMissing
        '
        Me.mnuScrapeSubmenuMissing.DropDown = Me.mnuScrapeType
        Me.mnuScrapeSubmenuMissing.Name = "mnuScrapeSubmenuMissing"
        Me.mnuScrapeSubmenuMissing.Size = New System.Drawing.Size(167, 22)
        Me.mnuScrapeSubmenuMissing.Tag = "missing"
        Me.mnuScrapeSubmenuMissing.Text = "Missing Items"
        '
        'mnuScrapeSubmenuNew
        '
        Me.mnuScrapeSubmenuNew.DropDown = Me.mnuScrapeType
        Me.mnuScrapeSubmenuNew.Name = "mnuScrapeSubmenuNew"
        Me.mnuScrapeSubmenuNew.Size = New System.Drawing.Size(167, 22)
        Me.mnuScrapeSubmenuNew.Tag = "new"
        Me.mnuScrapeSubmenuNew.Text = "New"
        '
        'mnuScrapeSubmenuMarked
        '
        Me.mnuScrapeSubmenuMarked.DropDown = Me.mnuScrapeType
        Me.mnuScrapeSubmenuMarked.Name = "mnuScrapeSubmenuMarked"
        Me.mnuScrapeSubmenuMarked.Size = New System.Drawing.Size(167, 22)
        Me.mnuScrapeSubmenuMarked.Tag = "marked"
        Me.mnuScrapeSubmenuMarked.Text = "Marked"
        '
        'mnuScrapeSubmenuCustom
        '
        Me.mnuScrapeSubmenuCustom.Name = "mnuScrapeSubmenuCustom"
        Me.mnuScrapeSubmenuCustom.Size = New System.Drawing.Size(167, 22)
        Me.mnuScrapeSubmenuCustom.Tag = "custom"
        Me.mnuScrapeSubmenuCustom.Text = "Custom Scraper..."
        '
        'cmnuTrayScrapeMovieSets
        '
        Me.cmnuTrayScrapeMovieSets.DropDown = Me.mnuScrapeSubmenu
        Me.cmnuTrayScrapeMovieSets.Image = CType(resources.GetObject("cmnuTrayScrapeMovieSets.Image"), System.Drawing.Image)
        Me.cmnuTrayScrapeMovieSets.Name = "cmnuTrayScrapeMovieSets"
        Me.cmnuTrayScrapeMovieSets.Size = New System.Drawing.Size(194, 22)
        Me.cmnuTrayScrapeMovieSets.Tag = "movieset"
        Me.cmnuTrayScrapeMovieSets.Text = "Scrape MovieSets"
        '
        'mnuScrapeMovieSets
        '
        Me.mnuScrapeMovieSets.DropDown = Me.mnuScrapeSubmenu
        Me.mnuScrapeMovieSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.mnuScrapeMovieSets.Image = CType(resources.GetObject("mnuScrapeMovieSets.Image"), System.Drawing.Image)
        Me.mnuScrapeMovieSets.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuScrapeMovieSets.Name = "mnuScrapeMovieSets"
        Me.mnuScrapeMovieSets.Size = New System.Drawing.Size(128, 22)
        Me.mnuScrapeMovieSets.Tag = "movieset"
        Me.mnuScrapeMovieSets.Text = "Scrape MovieSets"
        Me.mnuScrapeMovieSets.Visible = False
        '
        'mnuScrapeTVShows
        '
        Me.mnuScrapeTVShows.DropDown = Me.mnuScrapeSubmenu
        Me.mnuScrapeTVShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.mnuScrapeTVShows.Image = CType(resources.GetObject("mnuScrapeTVShows.Image"), System.Drawing.Image)
        Me.mnuScrapeTVShows.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuScrapeTVShows.Name = "mnuScrapeTVShows"
        Me.mnuScrapeTVShows.Size = New System.Drawing.Size(125, 22)
        Me.mnuScrapeTVShows.Tag = "tvshow"
        Me.mnuScrapeTVShows.Text = "Scrape TV Shows"
        Me.mnuScrapeTVShows.Visible = False
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
        'cmnuTrayScrapeTVShows
        '
        Me.cmnuTrayScrapeTVShows.DropDown = Me.mnuScrapeSubmenu
        Me.cmnuTrayScrapeTVShows.Image = CType(resources.GetObject("cmnuTrayScrapeTVShows.Image"), System.Drawing.Image)
        Me.cmnuTrayScrapeTVShows.Name = "cmnuTrayScrapeTVShows"
        Me.cmnuTrayScrapeTVShows.Size = New System.Drawing.Size(194, 22)
        Me.cmnuTrayScrapeTVShows.Tag = "tvshow"
        Me.cmnuTrayScrapeTVShows.Text = "Scrape TV Shows"
        '
        'cmnuTrayScrapeMovies
        '
        Me.cmnuTrayScrapeMovies.DropDown = Me.mnuScrapeSubmenu
        Me.cmnuTrayScrapeMovies.Image = CType(resources.GetObject("cmnuTrayScrapeMovies.Image"), System.Drawing.Image)
        Me.cmnuTrayScrapeMovies.Name = "cmnuTrayScrapeMovies"
        Me.cmnuTrayScrapeMovies.Size = New System.Drawing.Size(194, 22)
        Me.cmnuTrayScrapeMovies.Tag = "movie"
        Me.cmnuTrayScrapeMovies.Text = "Scrape Movies"
        '
        'ilColumnIcons
        '
        Me.ilColumnIcons.ImageStream = CType(resources.GetObject("ilColumnIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
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
        'tmrWait_Movie
        '
        Me.tmrWait_Movie.Interval = 250
        '
        'tmrLoad_Movie
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
        'tmrWait_TVShow
        '
        Me.tmrWait_TVShow.Interval = 250
        '
        'tmrLoad_TVShow
        '
        '
        'tmrWait_TVSeason
        '
        Me.tmrWait_TVSeason.Interval = 250
        '
        'tmrLoad_TVSeason
        '
        '
        'tmrWait_TVEpisode
        '
        Me.tmrWait_TVEpisode.Interval = 250
        '
        'tmrLoad_TVEpisode
        '
        '
        'cmnuTray
        '
        Me.cmnuTray.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayTitle, Me.ToolStripSeparator21, Me.cmnuTrayUpdate, Me.cmnuTrayScrapeMovies, Me.cmnuTrayScrapeMovieSets, Me.cmnuTrayScrapeTVShows, Me.ToolStripSeparator23, Me.cmnuTrayTools, Me.ToolStripSeparator22, Me.cmnuTraySettings, Me.ToolStripSeparator13, Me.cmnuTrayExit})
        Me.cmnuTray.Name = "cmnuTrayIcon"
        Me.cmnuTray.Size = New System.Drawing.Size(195, 204)
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
        'ToolStripSeparator23
        '
        Me.ToolStripSeparator23.Name = "ToolStripSeparator23"
        Me.ToolStripSeparator23.Size = New System.Drawing.Size(191, 6)
        '
        'cmnuTrayTools
        '
        Me.cmnuTrayTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTrayToolsCleanFiles, Me.cmnuTrayToolsSortFiles, Me.cmnuTrayToolsBackdrops, Me.ToolStripSeparator24, Me.cmnuTrayToolsOfflineHolder, Me.ToolStripSeparator25, Me.cmnuTrayToolsClearCache, Me.cmnuTrayToolsCleanDB, Me.ToolStripSeparator4, Me.cmnuTrayToolsReloadMovies, Me.cmnuTrayToolsReloadMovieSets, Me.cmnuTrayToolsReloadTVShows, Me.ToolStripSeparator26})
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
        'cmnuTrayToolsOfflineHolder
        '
        Me.cmnuTrayToolsOfflineHolder.Image = CType(resources.GetObject("cmnuTrayToolsOfflineHolder.Image"), System.Drawing.Image)
        Me.cmnuTrayToolsOfflineHolder.Name = "cmnuTrayToolsOfflineHolder"
        Me.cmnuTrayToolsOfflineHolder.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsOfflineHolder.Text = "Offline Media Manager"
        Me.cmnuTrayToolsOfflineHolder.Visible = False
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
        'cmnuTrayToolsCleanDB
        '
        Me.cmnuTrayToolsCleanDB.Image = CType(resources.GetObject("cmnuTrayToolsCleanDB.Image"), System.Drawing.Image)
        Me.cmnuTrayToolsCleanDB.Name = "cmnuTrayToolsCleanDB"
        Me.cmnuTrayToolsCleanDB.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsCleanDB.Text = "Clean Database"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(286, 6)
        '
        'cmnuTrayToolsReloadMovies
        '
        Me.cmnuTrayToolsReloadMovies.Image = CType(resources.GetObject("cmnuTrayToolsReloadMovies.Image"), System.Drawing.Image)
        Me.cmnuTrayToolsReloadMovies.Name = "cmnuTrayToolsReloadMovies"
        Me.cmnuTrayToolsReloadMovies.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsReloadMovies.Text = "Reload All Movies"
        '
        'cmnuTrayToolsReloadMovieSets
        '
        Me.cmnuTrayToolsReloadMovieSets.Image = CType(resources.GetObject("cmnuTrayToolsReloadMovieSets.Image"), System.Drawing.Image)
        Me.cmnuTrayToolsReloadMovieSets.Name = "cmnuTrayToolsReloadMovieSets"
        Me.cmnuTrayToolsReloadMovieSets.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsReloadMovieSets.Text = "Reload All MovieSets"
        '
        'cmnuTrayToolsReloadTVShows
        '
        Me.cmnuTrayToolsReloadTVShows.Image = CType(resources.GetObject("cmnuTrayToolsReloadTVShows.Image"), System.Drawing.Image)
        Me.cmnuTrayToolsReloadTVShows.Name = "cmnuTrayToolsReloadTVShows"
        Me.cmnuTrayToolsReloadTVShows.Size = New System.Drawing.Size(289, 22)
        Me.cmnuTrayToolsReloadTVShows.Text = "Reload All TV Shows"
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
        Me.pnlLoadSettings.Location = New System.Drawing.Point(615, 287)
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
        Me.tmrKeyBuffer.Interval = 2000
        '
        'tmrLoad_MovieSet
        '
        '
        'tmrWait_MovieSet
        '
        Me.tmrWait_MovieSet.Interval = 250
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
        'tmrRunTasks
        '
        '
        'lblTrailerPathHeader
        '
        Me.lblTrailerPathHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTrailerPathHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblTrailerPathHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTrailerPathHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrailerPathHeader.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblTrailerPathHeader.Location = New System.Drawing.Point(231, 234)
        Me.lblTrailerPathHeader.Name = "lblTrailerPathHeader"
        Me.lblTrailerPathHeader.Size = New System.Drawing.Size(222, 17)
        Me.lblTrailerPathHeader.TabIndex = 4
        Me.lblTrailerPathHeader.Text = "Trailer Path"
        Me.lblTrailerPathHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTrailerPath
        '
        Me.txtTrailerPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTrailerPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrailerPath.Location = New System.Drawing.Point(231, 254)
        Me.txtTrailerPath.Name = "txtTrailerPath"
        Me.txtTrailerPath.ReadOnly = True
        Me.txtTrailerPath.Size = New System.Drawing.Size(200, 22)
        Me.txtTrailerPath.TabIndex = 5
        Me.txtTrailerPath.TabStop = False
        '
        'btnTrailerPlay
        '
        Me.btnTrailerPlay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTrailerPlay.Image = Global.Ember_Media_Manager.My.Resources.Resources.Play_Icon
        Me.btnTrailerPlay.Location = New System.Drawing.Point(434, 254)
        Me.btnTrailerPlay.Name = "btnTrailerPlay"
        Me.btnTrailerPlay.Size = New System.Drawing.Size(20, 20)
        Me.btnTrailerPlay.TabIndex = 6
        Me.btnTrailerPlay.TabStop = False
        Me.btnTrailerPlay.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1354, 945)
        Me.Controls.Add(Me.pnlLoadSettings)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.mnuMain)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnuMain
        Me.MinimumSize = New System.Drawing.Size(800, 700)
        Me.Name = "frmMain"
        Me.Padding = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Text = "Ember Media Manager"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel1.PerformLayout()
        Me.scMain.Panel2.ResumeLayout(False)
        Me.scMain.Panel2.PerformLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.pnlFilterCountries_Movies.ResumeLayout(False)
        Me.pnlFilterCountries_Movies.PerformLayout()
        Me.pnlFilterCountriesMain_Movies.ResumeLayout(False)
        Me.pnlFilterCountriesTop_Movies.ResumeLayout(False)
        Me.pnlFilterCountriesTop_Movies.PerformLayout()
        Me.tblFilterCountriesTop_Movies.ResumeLayout(False)
        Me.tblFilterCountriesTop_Movies.PerformLayout()
        Me.pnlFilterGenres_Movies.ResumeLayout(False)
        Me.pnlFilterGenres_Movies.PerformLayout()
        Me.pnlFilterGenresMain_Movies.ResumeLayout(False)
        Me.pnlFilterGenresTop_Movies.ResumeLayout(False)
        Me.pnlFilterGenresTop_Movies.PerformLayout()
        Me.tblFilterGenresTop_Movies.ResumeLayout(False)
        Me.tblFilterGenresTop_Movies.PerformLayout()
        Me.pnlFilterGenres_Shows.ResumeLayout(False)
        Me.pnlFilterGenres_Shows.PerformLayout()
        Me.pnlFilterGenresMain_Shows.ResumeLayout(False)
        Me.pnlFilterGenresTop_Shows.ResumeLayout(False)
        Me.pnlFilterGenresTop_Shows.PerformLayout()
        Me.tblFilterGenresTop_Shows.ResumeLayout(False)
        Me.tblFilterGenresTop_Shows.PerformLayout()
        Me.pnlFilterDataFields_Movies.ResumeLayout(False)
        Me.pnlFilterDataFields_Movies.PerformLayout()
        Me.pnlFilterDataFieldsMain_Movies.ResumeLayout(False)
        Me.pnlFilterDataFieldsTop_Movies.ResumeLayout(False)
        Me.pnlFilterDataFieldsTop_Movies.PerformLayout()
        Me.tblFilterDataFieldsTop_Movies.ResumeLayout(False)
        Me.tblFilterDataFieldsTop_Movies.PerformLayout()
        Me.pnlFilterMissingItems_Movies.ResumeLayout(False)
        Me.pnlFilterMissingItems_Movies.PerformLayout()
        Me.pnlFilterMissingItemsMain_Movies.ResumeLayout(False)
        Me.pnlFilterMissingItemsMain_Movies.PerformLayout()
        Me.tblFilterMissingItemsMain_Movies.ResumeLayout(False)
        Me.tblFilterMissingItemsMain_Movies.PerformLayout()
        Me.pnlFilterMissingItemsTop_Movies.ResumeLayout(False)
        Me.pnlFilterMissingItemsTop_Movies.PerformLayout()
        Me.tblFilterMissingItemsTop_Movies.ResumeLayout(False)
        Me.tblFilterMissingItemsTop_Movies.PerformLayout()
        Me.pnlFilterMissingItems_MovieSets.ResumeLayout(False)
        Me.pnlFilterMissingItems_MovieSets.PerformLayout()
        Me.pnlFilterMissingItemsMain_MovieSets.ResumeLayout(False)
        Me.pnlFilterMissingItemsMain_MovieSets.PerformLayout()
        Me.tlbFilterMissingItemsMain_MovieSets.ResumeLayout(False)
        Me.tlbFilterMissingItemsMain_MovieSets.PerformLayout()
        Me.pnlFilterMissingItemsTop_MovieSets.ResumeLayout(False)
        Me.pnlFilterMissingItemsTop_MovieSets.PerformLayout()
        Me.tblFilterMissingItemsTop_MovieSets.ResumeLayout(False)
        Me.tblFilterMissingItemsTop_MovieSets.PerformLayout()
        Me.pnlFilterMissingItems_Shows.ResumeLayout(False)
        Me.pnlFilterMissingItems_Shows.PerformLayout()
        Me.pnlFilterMissingItemsMain_Shows.ResumeLayout(False)
        Me.pnlFilterMissingItemsMain_Shows.PerformLayout()
        Me.tblFilterMissingItemsMain_Shows.ResumeLayout(False)
        Me.tblFilterMissingItemsMain_Shows.PerformLayout()
        Me.pnlFilterMissingItemsTop_Shows.ResumeLayout(False)
        Me.pnlFilterMissingItemsTop_Shows.PerformLayout()
        Me.tblFilterMissingItemsTop_Shows.ResumeLayout(False)
        Me.tblFilterMissingItemsTop_Shows.PerformLayout()
        Me.pnlFilterSources_Movies.ResumeLayout(False)
        Me.pnlFilterSources_Movies.PerformLayout()
        Me.pnlFilterSourcesMain_Movies.ResumeLayout(False)
        Me.pnlFilterSourcesTop_Movies.ResumeLayout(False)
        Me.pnlFilterSourcesTop_Movies.PerformLayout()
        Me.tblFilterSourcesTop_Movies.ResumeLayout(False)
        Me.tblFilterSourcesTop_Movies.PerformLayout()
        Me.pnlFilterSources_Shows.ResumeLayout(False)
        Me.pnlFilterSources_Shows.PerformLayout()
        Me.pnlFilterSourcesMain_Shows.ResumeLayout(False)
        Me.pnlFilterSourcesTop_Shows.ResumeLayout(False)
        Me.pnlFilterSourcesTop_Shows.PerformLayout()
        Me.tblFilterSourcesTop_Shows.ResumeLayout(False)
        Me.tblFilterSourcesTop_Shows.PerformLayout()
        CType(Me.dgvMovies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmnuMovie.ResumeLayout(False)
        Me.mnuGenres.ResumeLayout(False)
        Me.mnuGenres.PerformLayout()
        Me.mnuTags.ResumeLayout(False)
        Me.mnuTags.PerformLayout()
        Me.mnuScrapeType.ResumeLayout(False)
        Me.mnuScrapeModifier.ResumeLayout(False)
        Me.mnuScrapeOption.ResumeLayout(False)
        Me.mnuLanguages.ResumeLayout(False)
        CType(Me.dgvMovieSets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmnuMovieSet.ResumeLayout(False)
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
        Me.pnlListTop.PerformLayout()
        Me.tblListTop.ResumeLayout(False)
        Me.tblListTop.PerformLayout()
        Me.tcMain.ResumeLayout(False)
        Me.pnlSearchMovies.ResumeLayout(False)
        Me.pnlSearchMovies.PerformLayout()
        CType(Me.picSearchMovies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearchMovieSets.ResumeLayout(False)
        Me.pnlSearchMovieSets.PerformLayout()
        CType(Me.picSearchMovieSets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearchTVShows.ResumeLayout(False)
        Me.pnlSearchTVShows.PerformLayout()
        CType(Me.picSearchTVShows, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFilter_Movies.ResumeLayout(False)
        Me.pnlFilter_Movies.PerformLayout()
        Me.tblFilter_Movies.ResumeLayout(False)
        Me.tblFilter_Movies.PerformLayout()
        Me.gbFilterGeneral_Movies.ResumeLayout(False)
        Me.gbFilterGeneral_Movies.PerformLayout()
        Me.tblFilterGeneral_Movies.ResumeLayout(False)
        Me.tblFilterGeneral_Movies.PerformLayout()
        Me.gbFilterSorting_Movies.ResumeLayout(False)
        Me.gbFilterSorting_Movies.PerformLayout()
        Me.tblFilterSorting_Movies.ResumeLayout(False)
        Me.tblFilterSorting_Movies.PerformLayout()
        Me.gbFilterSpecific_Movies.ResumeLayout(False)
        Me.gbFilterSpecific_Movies.PerformLayout()
        Me.tblFilterSpecific_Movies.ResumeLayout(False)
        Me.tblFilterSpecific_Movies.PerformLayout()
        Me.gbFilterModifier_Movies.ResumeLayout(False)
        Me.gbFilterModifier_Movies.PerformLayout()
        Me.tblFilterModifier_Movies.ResumeLayout(False)
        Me.tblFilterModifier_Movies.PerformLayout()
        Me.tblFilterSpecificData_Movies.ResumeLayout(False)
        Me.tblFilterSpecificData_Movies.PerformLayout()
        Me.gbFilterDataField_Movies.ResumeLayout(False)
        Me.gbFilterDataField_Movies.PerformLayout()
        Me.tblFilterDataField_Movies.ResumeLayout(False)
        Me.tblFilterDataField_Movies.PerformLayout()
        Me.gbFilterLists_Movies.ResumeLayout(False)
        Me.gbFilterLists_Movies.PerformLayout()
        Me.tblFilterLists_Movies.ResumeLayout(False)
        Me.pnlFilterTop_Movies.ResumeLayout(False)
        Me.pnlFilterTop_Movies.PerformLayout()
        Me.tblFilterTop_Movies.ResumeLayout(False)
        Me.tblFilterTop_Movies.PerformLayout()
        Me.pnlFilter_MovieSets.ResumeLayout(False)
        Me.pnlFilter_MovieSets.PerformLayout()
        Me.tblFilter_MovieSets.ResumeLayout(False)
        Me.tblFilter_MovieSets.PerformLayout()
        Me.gbFilterLists_MovieSets.ResumeLayout(False)
        Me.gbFilterLists_MovieSets.PerformLayout()
        Me.tblFilterLists_MovieSets.ResumeLayout(False)
        Me.gbFilterGeneral_MovieSets.ResumeLayout(False)
        Me.gbFilterGeneral_MovieSets.PerformLayout()
        Me.tblFilterGeneral_MovieSets.ResumeLayout(False)
        Me.tblFilterGeneral_MovieSets.PerformLayout()
        Me.gbFilterSpecific_MovieSets.ResumeLayout(False)
        Me.gbFilterSpecific_MovieSets.PerformLayout()
        Me.tblFilterSpecific_MovieSets.ResumeLayout(False)
        Me.tblFilterSpecific_MovieSets.PerformLayout()
        Me.gbFilterModifier_MovieSets.ResumeLayout(False)
        Me.gbFilterModifier_MovieSets.PerformLayout()
        Me.tblFilterModifier_MovieSets.ResumeLayout(False)
        Me.tblFilterModifier_MovieSets.PerformLayout()
        Me.pnlFilterTop_MovieSets.ResumeLayout(False)
        Me.pnlFilterTop_MovieSets.PerformLayout()
        Me.tblFilterTop_MovieSets.ResumeLayout(False)
        Me.tblFilterTop_MovieSets.PerformLayout()
        Me.pnlFilter_Shows.ResumeLayout(False)
        Me.pnlFilter_Shows.PerformLayout()
        Me.tblFilter_Shows.ResumeLayout(False)
        Me.tblFilter_Shows.PerformLayout()
        Me.gbFilterSorting_Shows.ResumeLayout(False)
        Me.gbFilterSorting_Shows.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.gbFilterLists_Shows.ResumeLayout(False)
        Me.gbFilterLists_Shows.PerformLayout()
        Me.tblFilterLists_Shows.ResumeLayout(False)
        Me.gbFilterGeneral_Shows.ResumeLayout(False)
        Me.gbFilterGeneral_Shows.PerformLayout()
        Me.tblFilterGeneral_Shows.ResumeLayout(False)
        Me.tblFilterGeneral_Shows.PerformLayout()
        Me.gbFilterSpecific_Shows.ResumeLayout(False)
        Me.gbFilterSpecific_Shows.PerformLayout()
        Me.tblFilterSpecific_Shows.ResumeLayout(False)
        Me.tblFilterSpecific_Shows.PerformLayout()
        Me.gbFilterModifier_Shows.ResumeLayout(False)
        Me.gbFilterModifier_Shows.PerformLayout()
        Me.tblFilterModifier_Shows.ResumeLayout(False)
        Me.tblFilterModifier_Shows.PerformLayout()
        Me.tblFilterSpecificData_Shows.ResumeLayout(False)
        Me.tblFilterSpecificData_Shows.PerformLayout()
        Me.pnlFilterTop_Shows.ResumeLayout(False)
        Me.pnlFilterTop_Shows.PerformLayout()
        Me.tblFilterTop_Shows.ResumeLayout(False)
        Me.tblFilterTop_Shows.PerformLayout()
        Me.pnlCancel.ResumeLayout(False)
        Me.pnlCancel.PerformLayout()
        Me.pnlNoInfo.ResumeLayout(False)
        Me.pnlNoInfoBG.ResumeLayout(False)
        CType(Me.pbNoInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInfoPanel.ResumeLayout(False)
        Me.pnlInfoPanel.PerformLayout()
        Me.pnlMoviesInSet.ResumeLayout(False)
        CType(Me.pbMILoading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlActors.ResumeLayout(False)
        CType(Me.pbActLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbActors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop250.ResumeLayout(False)
        CType(Me.pbTop250, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbBannerCache, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBanner.ResumeLayout(False)
        Me.pnlBanner.PerformLayout()
        Me.pnlBannerMain.ResumeLayout(False)
        Me.pnlBannerMain.PerformLayout()
        Me.tblBannerMain.ResumeLayout(False)
        CType(Me.pbBanner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBannerBottom.ResumeLayout(False)
        Me.pnlBannerBottom.PerformLayout()
        Me.tblBannerBottom.ResumeLayout(False)
        Me.tblBannerBottom.PerformLayout()
        Me.pnlBannerTop.ResumeLayout(False)
        Me.pnlBannerTop.PerformLayout()
        Me.tblBannerTop.ResumeLayout(False)
        Me.tblBannerTop.PerformLayout()
        CType(Me.pbCache, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlClearLogo.ResumeLayout(False)
        Me.pnlClearLogo.PerformLayout()
        Me.pnlClearLogoMain.ResumeLayout(False)
        Me.pnlClearLogoMain.PerformLayout()
        Me.tblClearLogoMain.ResumeLayout(False)
        CType(Me.pbClearLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlClearLogoBottom.ResumeLayout(False)
        Me.pnlClearLogoBottom.PerformLayout()
        Me.tblClearLogoBottom.ResumeLayout(False)
        Me.tblClearLogoBottom.PerformLayout()
        Me.pnlClearLogoTop.ResumeLayout(False)
        Me.pnlClearLogoTop.PerformLayout()
        Me.tblClearLogoTop.ResumeLayout(False)
        Me.tblClearLogoTop.PerformLayout()
        Me.pnlCharacterArt.ResumeLayout(False)
        Me.pnlCharacterArt.PerformLayout()
        Me.pnlCharacterArtMain.ResumeLayout(False)
        Me.pnlCharacterArtMain.PerformLayout()
        Me.tblCharacterArtMain.ResumeLayout(False)
        CType(Me.pbCharacterArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCharacterArtBottom.ResumeLayout(False)
        Me.pnlCharacterArtBottom.PerformLayout()
        Me.tblCharacterArtBottom.ResumeLayout(False)
        Me.tblCharacterArtBottom.PerformLayout()
        Me.pnlCharacterArtTop.ResumeLayout(False)
        Me.pnlCharacterArtTop.PerformLayout()
        Me.tblCharacterArtTop.ResumeLayout(False)
        Me.tblCharacterArtTop.PerformLayout()
        CType(Me.pbCharacterArtCache, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDiscArt.ResumeLayout(False)
        Me.pnlDiscArt.PerformLayout()
        Me.pnlDiscArtMain.ResumeLayout(False)
        Me.pnlDiscArtMain.PerformLayout()
        Me.tblDiscArtMain.ResumeLayout(False)
        CType(Me.pbDiscArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDiscArtBottom.ResumeLayout(False)
        Me.pnlDiscArtBottom.PerformLayout()
        Me.tblDiscArtBottom.ResumeLayout(False)
        Me.tblDiscArtBottom.PerformLayout()
        Me.pnlDiscArtTop.ResumeLayout(False)
        Me.pnlDiscArtTop.PerformLayout()
        Me.tblDiscArtTop.ResumeLayout(False)
        Me.tblDiscArtTop.PerformLayout()
        CType(Me.pbDiscArtCache, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbClearLogoCache, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlClearArt.ResumeLayout(False)
        Me.pnlClearArt.PerformLayout()
        Me.pnlClearArtMain.ResumeLayout(False)
        Me.pnlClearArtMain.PerformLayout()
        Me.tblClearArtMain.ResumeLayout(False)
        CType(Me.pbClearArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlClearArtBottom.ResumeLayout(False)
        Me.pnlClearArtBottom.PerformLayout()
        Me.tblClearArtBottom.ResumeLayout(False)
        Me.tblClearArtBottom.PerformLayout()
        Me.pnlClearArtTop.ResumeLayout(False)
        Me.pnlClearArtTop.PerformLayout()
        Me.tblClearArtTop.ResumeLayout(False)
        Me.tblClearArtTop.PerformLayout()
        Me.pnlLandscape.ResumeLayout(False)
        Me.pnlLandscape.PerformLayout()
        Me.pnlLandscapeMain.ResumeLayout(False)
        Me.pnlLandscapeMain.PerformLayout()
        Me.tblLandscapeMain.ResumeLayout(False)
        CType(Me.pbLandscape, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLandscapeBottom.ResumeLayout(False)
        Me.pnlLandscapeBottom.PerformLayout()
        Me.tblLandscapeBottom.ResumeLayout(False)
        Me.tblLandscapeBottom.PerformLayout()
        Me.pnlLandscapeTop.ResumeLayout(False)
        Me.pnlLandscapeTop.PerformLayout()
        Me.tblLandscapeTop.ResumeLayout(False)
        Me.tblLandscapeTop.PerformLayout()
        Me.pnlFanartSmall.ResumeLayout(False)
        Me.pnlFanartSmall.PerformLayout()
        Me.pnlFanartSmallMain.ResumeLayout(False)
        Me.pnlFanartSmallMain.PerformLayout()
        Me.tblFanartSmallMain.ResumeLayout(False)
        CType(Me.pbFanartSmall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFanartSmallBottom.ResumeLayout(False)
        Me.pnlFanartSmallBottom.PerformLayout()
        Me.tblFanartSmallBottom.ResumeLayout(False)
        Me.tblFanartSmallBottom.PerformLayout()
        Me.pnlFanartSmallTop.ResumeLayout(False)
        Me.pnlFanartSmallTop.PerformLayout()
        Me.tblFanartSmallTop.ResumeLayout(False)
        Me.tblFanartSmallTop.PerformLayout()
        Me.pnlPoster.ResumeLayout(False)
        Me.pnlPoster.PerformLayout()
        Me.pnlPosterMain.ResumeLayout(False)
        Me.pnlPosterMain.PerformLayout()
        Me.tblPosterMain.ResumeLayout(False)
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPosterBottom.ResumeLayout(False)
        Me.pnlPosterBottom.PerformLayout()
        Me.tblPosterBottom.ResumeLayout(False)
        Me.tblPosterBottom.PerformLayout()
        Me.pnlPosterTop.ResumeLayout(False)
        Me.pnlPosterTop.PerformLayout()
        Me.tblPosterTop.ResumeLayout(False)
        Me.tblPosterTop.PerformLayout()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tlpHeader.ResumeLayout(False)
        Me.tlpHeader.PerformLayout()
        Me.pnlRating.ResumeLayout(False)
        CType(Me.pbStar10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInfoIcons.ResumeLayout(False)
        CType(Me.pbSubtitleLang6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbSubtitleLang5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbSubtitleLang4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbSubtitleLang3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbSubtitleLang2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbSubtitleLang1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbSubtitleLang0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbAudioLang6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbAudioLang5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbAudioLang4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbAudioLang3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbAudioLang2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbAudioLang1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbAudioLang0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbVType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStudio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbVideo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbAudio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbResolution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbChannels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbPosterCache, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFanartSmallCache, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbLandscapeCache, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbClearArtCache, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMPAA.ResumeLayout(False)
        Me.pnlMPAA.PerformLayout()
        CType(Me.pbMPAA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFanartCache, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tsMain.ResumeLayout(False)
        Me.tsMain.PerformLayout()
        Me.mnuScrapeSubmenu.ResumeLayout(False)
        Me.cmnuTray.ResumeLayout(False)
        Me.pnlLoadSettingsBG.ResumeLayout(False)
        CType(Me.pbLoadSettings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLoadSettings.ResumeLayout(False)
        Me.ResumeLayout(False)
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
    Friend WithEvents btnFilePlay As System.Windows.Forms.Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents lblFilePathHeader As System.Windows.Forms.Label
    Friend WithEvents txtIMDBID As System.Windows.Forms.TextBox
    Friend WithEvents lblIMDBHeader As System.Windows.Forms.Label
    Friend WithEvents lblDirectors As System.Windows.Forms.Label
    Friend WithEvents lblDirectorsHeader As System.Windows.Forms.Label
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
    Friend WithEvents btnMid As System.Windows.Forms.Button
    Friend WithEvents pbPosterCache As System.Windows.Forms.PictureBox
    Friend WithEvents txtCertifications As System.Windows.Forms.TextBox
    Friend WithEvents lblCertificationsHeader As System.Windows.Forms.Label
    Friend WithEvents lblReleaseDate As System.Windows.Forms.Label
    Friend WithEvents lblReleaseDateHeader As System.Windows.Forms.Label
    Friend WithEvents ilColumnIcons As System.Windows.Forms.ImageList
    Friend WithEvents tmrWait_Movie As System.Windows.Forms.Timer
    Friend WithEvents tmrLoad_Movie As System.Windows.Forms.Timer
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
    Friend WithEvents cmnuMovie As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmnuMovieMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieEditSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieChange As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieDatabaseSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieRescrapeSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents prbCanceling As System.Windows.Forms.ProgressBar
    Friend WithEvents lblCanceling As System.Windows.Forms.Label
    Private WithEvents pnlNoInfo As System.Windows.Forms.Panel
    Friend WithEvents pnlCancel As System.Windows.Forms.Panel
    Friend WithEvents cmnuMovieSep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieOpenFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieRemoveFromDisk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieLock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsBackdrops As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnMarkAll As System.Windows.Forms.Button
    Friend WithEvents mnuMainToolsSeparator0 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMainToolsSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMainToolsClearCache As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnFilterDown_Movies As System.Windows.Forms.Button
    Friend WithEvents btnFilterUp_Movies As System.Windows.Forms.Button
    Friend WithEvents lblFilterSource_Movies As System.Windows.Forms.Label
    Friend WithEvents mnuMainToolsSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieReload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsReloadMovies As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieEditGenres As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents cmnuMovieRemoveSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuUpdateMovies As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuUpdateShows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainDonate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrWait_TVShow As System.Windows.Forms.Timer
    Friend WithEvents tmrLoad_TVShow As System.Windows.Forms.Timer
    Friend WithEvents tmrWait_TVSeason As System.Windows.Forms.Timer
    Friend WithEvents tmrLoad_TVSeason As System.Windows.Forms.Timer
    Friend WithEvents tmrWait_TVEpisode As System.Windows.Forms.Timer
    Friend WithEvents tmrLoad_TVEpisode As System.Windows.Forms.Timer
    Friend WithEvents tsSpring As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cmnuShow As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmnuSeason As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmnuEpisode As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmnuShowTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowDatabaseSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuShowEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeDatabaseSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuEpisodeEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowScrapeSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuShowScrape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowChange As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowReload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowLock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowEditSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuEpisodeReload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeLock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeEditSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuEpisodeScrapeSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuEpisodeScrape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieScrapeSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowRemoveSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuShowRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowRemoveFromDisk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowRemoveFromDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeSep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuEpisodeRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeRemoveFromDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeRemoveFromDisk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainHelpVersions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonScrapeSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuSeasonScrape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonSep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuSeasonRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonReload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonLock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonEditSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuSeasonRemoveFromDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonRemoveFromDisk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeChange As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonDatabaseSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMainHelpWiki As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainHelpSeparator0 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMainHelpSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMainError As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieScrape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrayIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents cmnuTray As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuTrayExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator21 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuTrayUpdate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayScrapeMovies As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator22 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuTraySettings As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents pnlLoadSettingsBG As System.Windows.Forms.Panel
    Friend WithEvents pbLoadSettings As System.Windows.Forms.PictureBox
    Friend WithEvents lblLoadSettings As System.Windows.Forms.Label
    Friend WithEvents prbLoadSettings As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlLoadSettings As System.Windows.Forms.Panel
    Friend WithEvents cmnuShowOpenFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowSep4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuSeasonOpenFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonRemoveSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuEpisodeOpenFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeRemoveSeparator As System.Windows.Forms.ToolStripSeparator
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
    Friend WithEvents pbFanartSmallCache As System.Windows.Forms.PictureBox
    Friend WithEvents cmnuMovieWatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowWatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonWatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeWatched As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpMovies As System.Windows.Forms.TabPage
    Friend WithEvents mnuMainToolsOfflineHolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayToolsOfflineHolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuVersion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowScrapeRefreshData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieScrapeSingleDataField As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpMovieSets As System.Windows.Forms.TabPage
    Friend WithEvents dgvMovieSets As System.Windows.Forms.DataGridView
    Friend WithEvents dgvMovies As System.Windows.Forms.DataGridView
    Friend WithEvents cmnuMovieSet As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tmrLoad_MovieSet As System.Windows.Forms.Timer
    Friend WithEvents tmrWait_MovieSet As System.Windows.Forms.Timer
    Friend WithEvents cmnuMovieSetReload As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetTitle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetDatabaseSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieSetNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetRemove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pbFanart As System.Windows.Forms.PictureBox
    Friend WithEvents pbLandscapeCache As System.Windows.Forms.PictureBox
    Friend WithEvents pbClearArtCache As System.Windows.Forms.PictureBox
    Friend WithEvents lblMoviesInSetHeader As System.Windows.Forms.Label
    Friend WithEvents pnlMoviesInSet As System.Windows.Forms.Panel
    Friend WithEvents mnuMainToolsExport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsExportMovies As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsExportTvShows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieMarkAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieMarkAsCustom1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieMarkAsCustom2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieMarkAsCustom3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieMarkAsCustom4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetScrapeSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieSetScrape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowLanguage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pbStar10 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar9 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar8 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar7 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar6 As System.Windows.Forms.PictureBox
    Friend WithEvents mnuMainToolsReloadMovieSets As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetMark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetLock As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetEditSeparator As System.Windows.Forms.ToolStripSeparator
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
    Friend WithEvents chkFilterNewShows_Shows As System.Windows.Forms.CheckBox
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
    Friend WithEvents chkMovieMissingExtrafanarts As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingExtrathumbs As System.Windows.Forms.CheckBox
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
    Friend WithEvents chkShowMissingExtrafanarts As System.Windows.Forms.CheckBox
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
    Friend WithEvents chkFilterNewEpisodes_Shows As System.Windows.Forms.CheckBox
    Friend WithEvents pnlPosterBottom As System.Windows.Forms.Panel
    Friend WithEvents tblPosterBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblPosterSize As System.Windows.Forms.Label
    Friend WithEvents pnlPosterTop As System.Windows.Forms.Panel
    Friend WithEvents tblPosterTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblPosterTitle As System.Windows.Forms.Label
    Friend WithEvents pnlPosterMain As System.Windows.Forms.Panel
    Friend WithEvents tblPosterMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlFanartSmall As System.Windows.Forms.Panel
    Friend WithEvents pnlFanartSmallMain As System.Windows.Forms.Panel
    Friend WithEvents tblFanartSmallMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pbFanartSmall As System.Windows.Forms.PictureBox
    Friend WithEvents pnlFanartSmallBottom As System.Windows.Forms.Panel
    Friend WithEvents tblFanartSmallBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblFanartSmallSize As System.Windows.Forms.Label
    Friend WithEvents pnlFanartSmallTop As System.Windows.Forms.Panel
    Friend WithEvents tblFanartSmallTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblFanartSmallTitle As System.Windows.Forms.Label
    Friend WithEvents pnlLandscape As System.Windows.Forms.Panel
    Friend WithEvents pnlLandscapeMain As System.Windows.Forms.Panel
    Friend WithEvents tblLandscapeMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pbLandscape As System.Windows.Forms.PictureBox
    Friend WithEvents pnlLandscapeBottom As System.Windows.Forms.Panel
    Friend WithEvents tblLandscapeBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblLandscapeSize As System.Windows.Forms.Label
    Friend WithEvents pnlLandscapeTop As System.Windows.Forms.Panel
    Friend WithEvents tblLandscapeTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblLandscapeTitle As System.Windows.Forms.Label
    Friend WithEvents pnlClearArt As System.Windows.Forms.Panel
    Friend WithEvents pnlClearArtMain As System.Windows.Forms.Panel
    Friend WithEvents tblClearArtMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pbClearArt As System.Windows.Forms.PictureBox
    Friend WithEvents pnlClearArtBottom As System.Windows.Forms.Panel
    Friend WithEvents tblClearArtBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblClearArtSize As System.Windows.Forms.Label
    Friend WithEvents pnlClearArtTop As System.Windows.Forms.Panel
    Friend WithEvents tblClearArtTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblClearArtTitle As System.Windows.Forms.Label
    Friend WithEvents pnlDiscArt As System.Windows.Forms.Panel
    Friend WithEvents pnlDiscArtMain As System.Windows.Forms.Panel
    Friend WithEvents tblDiscArtMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pbDiscArt As System.Windows.Forms.PictureBox
    Friend WithEvents pnlDiscArtBottom As System.Windows.Forms.Panel
    Friend WithEvents tblDiscArtBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblDiscArtSize As System.Windows.Forms.Label
    Friend WithEvents pnlDiscArtTop As System.Windows.Forms.Panel
    Friend WithEvents tblDiscArtTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblDiscArtTitle As System.Windows.Forms.Label
    Friend WithEvents pbDiscArtCache As System.Windows.Forms.PictureBox
    Friend WithEvents pbClearLogoCache As System.Windows.Forms.PictureBox
    Friend WithEvents pnlCharacterArt As System.Windows.Forms.Panel
    Friend WithEvents pnlCharacterArtMain As System.Windows.Forms.Panel
    Friend WithEvents tblCharacterArtMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pbCharacterArt As System.Windows.Forms.PictureBox
    Friend WithEvents pnlCharacterArtBottom As System.Windows.Forms.Panel
    Friend WithEvents tblCharacterArtBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblCharacterArtSize As System.Windows.Forms.Label
    Friend WithEvents pnlCharacterArtTop As System.Windows.Forms.Panel
    Friend WithEvents tblCharacterArtTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblCharacterArtTitle As System.Windows.Forms.Label
    Friend WithEvents pbCharacterArtCache As System.Windows.Forms.PictureBox
    Friend WithEvents pnlClearLogo As System.Windows.Forms.Panel
    Friend WithEvents pnlClearLogoMain As System.Windows.Forms.Panel
    Friend WithEvents tblClearLogoMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pbClearLogo As System.Windows.Forms.PictureBox
    Friend WithEvents pnlClearLogoBottom As System.Windows.Forms.Panel
    Friend WithEvents tblClearLogoBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblClearLogoSize As System.Windows.Forms.Label
    Friend WithEvents pnlClearLogoTop As System.Windows.Forms.Panel
    Friend WithEvents tblClearLogoTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblClearLogoTitle As System.Windows.Forms.Label
    Friend WithEvents pbCache As System.Windows.Forms.PictureBox
    Friend WithEvents pnlBanner As System.Windows.Forms.Panel
    Friend WithEvents pnlBannerMain As System.Windows.Forms.Panel
    Friend WithEvents tblBannerMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pbBanner As System.Windows.Forms.PictureBox
    Friend WithEvents pnlBannerBottom As System.Windows.Forms.Panel
    Friend WithEvents tblBannerBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblBannerSize As System.Windows.Forms.Label
    Friend WithEvents pnlBannerTop As System.Windows.Forms.Panel
    Friend WithEvents tblBannerTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblBannerTitle As System.Windows.Forms.Label
    Friend WithEvents pbBannerCache As System.Windows.Forms.PictureBox
    Friend WithEvents cmnuShowClearCache As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowClearCacheDataOnly As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowClearCacheImagesOnly As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowSep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuShowClearCacheDataAndImages As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtTMDBID As System.Windows.Forms.TextBox
    Friend WithEvents lblTMDBHeader As System.Windows.Forms.Label
    Friend WithEvents gbFilterLists_Movies As System.Windows.Forms.GroupBox
    Friend WithEvents tblFilterLists_Movies As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cbFilterLists_Movies As System.Windows.Forms.ComboBox
    Friend WithEvents tblListTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbFilterLists_MovieSets As System.Windows.Forms.GroupBox
    Friend WithEvents tblFilterLists_MovieSets As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cbFilterLists_MovieSets As System.Windows.Forms.ComboBox
    Friend WithEvents gbFilterLists_Shows As System.Windows.Forms.GroupBox
    Friend WithEvents tblFilterLists_Shows As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cbFilterLists_Shows As System.Windows.Forms.ComboBox
    Friend WithEvents chkFilterMultiple_MovieSets As System.Windows.Forms.CheckBox
    Friend WithEvents gbFilterSorting_Shows As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnFilterSortTitle_Shows As System.Windows.Forms.Button
    Friend WithEvents cmnuMovieSetEditSortMethod As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetEditSortMethodMethods As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents cmnuMovieSetEditSortMethodSet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrRunTasks As System.Windows.Forms.Timer
    Friend WithEvents mnuScrapeModifier As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuScrapeModifierAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierActorthumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierBanner As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierCharacterArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierClearArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierClearLogo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierDiscArt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierExtrafanarts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierExtrathumbs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierFanart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierLandscape As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierMetaData As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierNFO As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierPoster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeModifierTrailer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeTypeSkip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeType As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuScrapeTypeAuto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeTypeAsk As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeSubmenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuScrapeSubmenuAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeSubmenuMissing As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeSubmenuNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeSubmenuMarked As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeSubmenuFilter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeSubmenuCustom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayScrapeMovieSets As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayScrapeTVShows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowReloadFull As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonReloadFull As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetScrapeSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuEpisodeScrapeSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuShowScrapeSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuSeasonScrapeSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsReloadTVShows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayToolsReloadMovieSets As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrayToolsReloadTVShows As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmnuMovieLanguage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuMovieSetLanguage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuScrapeOption As ContextMenuStrip
    Friend WithEvents mnuScrapeOptionActors As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionCertifications As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionCollectionID As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionCountries As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionDirectors As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionGenres As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionMPAA As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionOriginalTitle As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionPlot As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionRating As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionReleaseDate As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionRuntime As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionStudios As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionTagline As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionTitle As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionTop250 As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionTrailer As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionWriters As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionYear As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionOutline As ToolStripMenuItem
    Friend WithEvents cmnuMovieSetScrapeSingleDataField As ToolStripMenuItem
    Friend WithEvents cmnuEpisodeScrapeSingleDataField As ToolStripMenuItem
    Friend WithEvents cmnuShowScrapeSingleDataField As ToolStripMenuItem
    Friend WithEvents cmnuSeasonScrapeSingleDataField As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionCreators As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionGuestStars As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionPremiered As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionStatus As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionAired As ToolStripMenuItem
    Friend WithEvents mnuScrapeOptionEpiGuideURL As ToolStripMenuItem
    Friend WithEvents cmnuMovieSetSep3 As ToolStripSeparator
    Friend WithEvents cmnuMovieSetBrowseTMDB As ToolStripMenuItem
    Friend WithEvents cmnuShowBrowseIMDB As ToolStripMenuItem
    Friend WithEvents cmnuShowBrowseTMDB As ToolStripMenuItem
    Friend WithEvents cmnuShowBrowseTVDB As ToolStripMenuItem
    Friend WithEvents cmnuEpisodeBrowseIMDB As ToolStripMenuItem
    Friend WithEvents cmnuEpisodeBrowseTMDB As ToolStripMenuItem
    Friend WithEvents cmnuEpisodeBrowseTVDB As ToolStripMenuItem
    Friend WithEvents cmnuSeasonBrowseTMDB As ToolStripMenuItem
    Friend WithEvents cmnuSeasonBrowseTVDB As ToolStripMenuItem
    Friend WithEvents cmnuSeasonBrowseIMDB As ToolStripMenuItem
    Friend WithEvents cmnuMovieEditTags As ToolStripMenuItem
    Friend WithEvents mnuTags As ContextMenuStrip
    Friend WithEvents mnuTagsTitleSelect As ToolStripMenuItem
    Friend WithEvents mnuTagsTag As ToolStripComboBox
    Friend WithEvents mnuTagsAdd As ToolStripMenuItem
    Friend WithEvents mnuTagsSet As ToolStripMenuItem
    Friend WithEvents mnuTagsRemove As ToolStripMenuItem
    Friend WithEvents cmnuShowEditTags As ToolStripMenuItem
    Friend WithEvents cmnuShowEditGenres As ToolStripMenuItem
    Friend WithEvents mnuGenres As ContextMenuStrip
    Friend WithEvents mnuGenresTitleSelect As ToolStripMenuItem
    Friend WithEvents mnuGenresGenre As ToolStripComboBox
    Friend WithEvents mnuGenresAdd As ToolStripMenuItem
    Friend WithEvents mnuGenresSet As ToolStripMenuItem
    Friend WithEvents mnuGenresRemove As ToolStripMenuItem
    Friend WithEvents mnuTagsNew As ToolStripTextBox
    Friend WithEvents mnuGenresSep1 As ToolStripSeparator
    Friend WithEvents mnuGenresTitleNew As ToolStripMenuItem
    Friend WithEvents mnuGenresNew As ToolStripTextBox
    Friend WithEvents mnuGenresSep2 As ToolStripSeparator
    Friend WithEvents mnuTagsTitleNew As ToolStripMenuItem
    Friend WithEvents mnuTagsSep1 As ToolStripSeparator
    Friend WithEvents mnuTagsSep2 As ToolStripSeparator
    Friend WithEvents mnuScrapeMovies As ToolStripSplitButton
    Friend WithEvents mnuScrapeTVShows As ToolStripSplitButton
    Friend WithEvents mnuScrapeMovieSets As ToolStripSplitButton
    Friend WithEvents cmnuShowClearCacheSeparator As ToolStripSeparator
    Friend WithEvents cmnuMovieSetNewSeparator As ToolStripSeparator
    Friend WithEvents cmnuMovieSetRemoveSeparator As ToolStripSeparator
    Friend WithEvents mnuLanguages As ContextMenuStrip
    Friend WithEvents mnuLanguagesTitleSelect As ToolStripMenuItem
    Friend WithEvents mnuLanguagesLanguage As ToolStripComboBox
    Friend WithEvents mnuLanguagesSep1 As ToolStripSeparator
    Friend WithEvents mnuLanguagesSet As ToolStripMenuItem
    Friend WithEvents cmnuMovieUnwatched As ToolStripMenuItem
    Friend WithEvents cmnuShowUnwatched As ToolStripMenuItem
    Friend WithEvents cmnuSeasonUnwatched As ToolStripMenuItem
    Friend WithEvents cmnuEpisodeUnwatched As ToolStripMenuItem
    Friend WithEvents btnTrailerPlay As Button
    Friend WithEvents txtTrailerPath As TextBox
    Friend WithEvents lblTrailerPathHeader As Label
End Class
