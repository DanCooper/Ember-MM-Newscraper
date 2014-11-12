<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Catch ex As Exception
        End Try
        Try
            'Finally
            MyBase.Dispose(disposing)
        Catch ex As Exception
        End Try
    End Sub

    Delegate Sub DelegateSub(ByVal b As Boolean)

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgSettings))
        Me.gbGeneralMisc = New System.Windows.Forms.GroupBox()
        Me.tblGeneralMisc = New System.Windows.Forms.TableLayoutPanel()
        Me.chkGeneralSourceFromFolder = New System.Windows.Forms.CheckBox()
        Me.chkGeneralCheckUpdates = New System.Windows.Forms.CheckBox()
        Me.lblGeneralOverwriteNfo = New System.Windows.Forms.Label()
        Me.chkGeneralOverwriteNfo = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDateAddedIgnoreNFO = New System.Windows.Forms.CheckBox()
        Me.gbGeneralDaemon = New System.Windows.Forms.GroupBox()
        Me.tblGeneralDaemon = New System.Windows.Forms.TableLayoutPanel()
        Me.lblGeneralDaemonPath = New System.Windows.Forms.Label()
        Me.btnGeneralDaemonPathBrowse = New System.Windows.Forms.Button()
        Me.lblGeneralDaemonDrive = New System.Windows.Forms.Label()
        Me.txtGeneralDaemonPath = New System.Windows.Forms.TextBox()
        Me.cbGeneralDaemonDrive = New System.Windows.Forms.ComboBox()
        Me.chkGeneralImagesGlassOverlay = New System.Windows.Forms.CheckBox()
        Me.chkGeneralHideFanartSmall = New System.Windows.Forms.CheckBox()
        Me.chkGeneralShowGenresText = New System.Windows.Forms.CheckBox()
        Me.chkGeneralHideFanart = New System.Windows.Forms.CheckBox()
        Me.chkGeneralInfoPanelAnim = New System.Windows.Forms.CheckBox()
        Me.chkGeneralHidePoster = New System.Windows.Forms.CheckBox()
        Me.chkGeneralShowImgDims = New System.Windows.Forms.CheckBox()
        Me.gbGeneralThemes = New System.Windows.Forms.GroupBox()
        Me.tblGeneralThemes = New System.Windows.Forms.TableLayoutPanel()
        Me.cbGeneralTVEpisodeTheme = New System.Windows.Forms.ComboBox()
        Me.cbGeneralMovieSetTheme = New System.Windows.Forms.ComboBox()
        Me.lblGeneralTVEpisodeTheme = New System.Windows.Forms.Label()
        Me.lblGeneralMovieTheme = New System.Windows.Forms.Label()
        Me.cbGeneralTVShowTheme = New System.Windows.Forms.ComboBox()
        Me.lblGeneralMovieSetTheme = New System.Windows.Forms.Label()
        Me.lblGeneralTVShowTheme = New System.Windows.Forms.Label()
        Me.cbGeneralMovieTheme = New System.Windows.Forms.ComboBox()
        Me.lblGeneralntLang = New System.Windows.Forms.Label()
        Me.cbGeneralLanguage = New System.Windows.Forms.ComboBox()
        Me.gbFileSystemCleanFiles = New System.Windows.Forms.GroupBox()
        Me.tcFileSystemCleaner = New System.Windows.Forms.TabControl()
        Me.tpFileSystemCleanerStandard = New System.Windows.Forms.TabPage()
        Me.chkCleanFolderJPG = New System.Windows.Forms.CheckBox()
        Me.chkCleanExtrathumbs = New System.Windows.Forms.CheckBox()
        Me.chkCleanMovieTBN = New System.Windows.Forms.CheckBox()
        Me.chkCleanMovieNameJPG = New System.Windows.Forms.CheckBox()
        Me.chkCleanMovieTBNb = New System.Windows.Forms.CheckBox()
        Me.chkCleanMovieJPG = New System.Windows.Forms.CheckBox()
        Me.chkCleanFanartJPG = New System.Windows.Forms.CheckBox()
        Me.chkCleanPosterJPG = New System.Windows.Forms.CheckBox()
        Me.chkCleanMovieFanartJPG = New System.Windows.Forms.CheckBox()
        Me.chkCleanPosterTBN = New System.Windows.Forms.CheckBox()
        Me.chkCleanMovieNFO = New System.Windows.Forms.CheckBox()
        Me.chkCleanDotFanartJPG = New System.Windows.Forms.CheckBox()
        Me.chkCleanMovieNFOb = New System.Windows.Forms.CheckBox()
        Me.tpFileSystemCleanerExpert = New System.Windows.Forms.TabPage()
        Me.chkFileSystemCleanerWhitelist = New System.Windows.Forms.CheckBox()
        Me.lblFileSystemCleanerWhitelist = New System.Windows.Forms.Label()
        Me.btnFileSystemCleanerWhitelistRemove = New System.Windows.Forms.Button()
        Me.btnFileSystemCleanerWhitelistAdd = New System.Windows.Forms.Button()
        Me.txtFileSystemCleanerWhitelist = New System.Windows.Forms.TextBox()
        Me.lstFileSystemCleanerWhitelist = New System.Windows.Forms.ListBox()
        Me.lblFileSystemCleanerWarning = New System.Windows.Forms.Label()
        Me.gbMovieGeneralMiscOpts = New System.Windows.Forms.GroupBox()
        Me.chkMovieClickScrape = New System.Windows.Forms.CheckBox()
        Me.chkMovieClickScrapeAsk = New System.Windows.Forms.CheckBox()
        Me.chkMovieGeneralMarkNew = New System.Windows.Forms.CheckBox()
        Me.pnlMovieImages = New System.Windows.Forms.Panel()
        Me.tblMovieImages = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMovieImagesOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieImagesOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieNoSaveImagesToNfo = New System.Windows.Forms.CheckBox()
        Me.gbMovieImagesLandscapeOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieImagesLandscapeOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieLandscapeOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbMovieImagesDiscArtOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieImagesDiscArtOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieDiscArtOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbMovieImagesClearLogoOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieImagesClearLogoOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieClearLogoOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbMovieImagesClearArtOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieImagesClearArtOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieClearArtOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbMovieImagesActorThumbsOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieImagesActorThumbsOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieActorThumbsOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbMovieImagesPosterOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieImagesPosterOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMoviePosterSize = New System.Windows.Forms.Label()
        Me.txtMoviePosterHeight = New System.Windows.Forms.TextBox()
        Me.txtMoviePosterWidth = New System.Windows.Forms.TextBox()
        Me.lblMoviePosterHeight = New System.Windows.Forms.Label()
        Me.cbMoviePosterPrefSize = New System.Windows.Forms.ComboBox()
        Me.lblMoviePosterWidth = New System.Windows.Forms.Label()
        Me.chkMoviePosterOverwrite = New System.Windows.Forms.CheckBox()
        Me.chkMoviePosterResize = New System.Windows.Forms.CheckBox()
        Me.chkMoviePosterPrefOnly = New System.Windows.Forms.CheckBox()
        Me.gbMovieImagesBannerOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieImagesBannerOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.txtMovieBannerHeight = New System.Windows.Forms.TextBox()
        Me.txtMovieBannerWidth = New System.Windows.Forms.TextBox()
        Me.lblMovieBannerHeight = New System.Windows.Forms.Label()
        Me.chkMovieBannerPrefOnly = New System.Windows.Forms.CheckBox()
        Me.lblMovieBannerType = New System.Windows.Forms.Label()
        Me.lblMovieBannerWidth = New System.Windows.Forms.Label()
        Me.cbMovieBannerPrefType = New System.Windows.Forms.ComboBox()
        Me.chkMovieBannerOverwrite = New System.Windows.Forms.CheckBox()
        Me.chkMovieBannerResize = New System.Windows.Forms.CheckBox()
        Me.gbMovieImagesEThumbsOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieImagesEThumbsOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.txtMovieEThumbsHeight = New System.Windows.Forms.TextBox()
        Me.txtMovieEThumbsWidth = New System.Windows.Forms.TextBox()
        Me.lblMovieEThumbsHeight = New System.Windows.Forms.Label()
        Me.txtMovieEThumbsLimit = New System.Windows.Forms.TextBox()
        Me.lblMovieEThumbsLimit = New System.Windows.Forms.Label()
        Me.lblMovieEThumbsWidth = New System.Windows.Forms.Label()
        Me.lblMovieEThumbsSize = New System.Windows.Forms.Label()
        Me.cbMovieEThumbsPrefSize = New System.Windows.Forms.ComboBox()
        Me.chkMovieEThumbsResize = New System.Windows.Forms.CheckBox()
        Me.chkMovieEThumbsPrefOnly = New System.Windows.Forms.CheckBox()
        Me.chkMovieEThumbsOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbMovieImagesEFanartsOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieImagesEFanartsOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMovieEFanartsSize = New System.Windows.Forms.Label()
        Me.cbMovieEFanartsPrefSize = New System.Windows.Forms.ComboBox()
        Me.chkMovieEFanartsPrefOnly = New System.Windows.Forms.CheckBox()
        Me.txtMovieEFanartsHeight = New System.Windows.Forms.TextBox()
        Me.txtMovieEFanartsWidth = New System.Windows.Forms.TextBox()
        Me.lblMovieEFanartsHeight = New System.Windows.Forms.Label()
        Me.chkMovieEFanartsResize = New System.Windows.Forms.CheckBox()
        Me.lblMovieEFanartsWidth = New System.Windows.Forms.Label()
        Me.chkMovieEFanartsOverwrite = New System.Windows.Forms.CheckBox()
        Me.lblMovieEFanartsLimit = New System.Windows.Forms.Label()
        Me.txtMovieEFanartsLimit = New System.Windows.Forms.TextBox()
        Me.gbMovieImagesFanartOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieImagesFanartOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.txtMovieFanartHeight = New System.Windows.Forms.TextBox()
        Me.txtMovieFanartWidth = New System.Windows.Forms.TextBox()
        Me.lblMovieFanartHeight = New System.Windows.Forms.Label()
        Me.chkMovieFanartPrefOnly = New System.Windows.Forms.CheckBox()
        Me.lblMovieFanartSize = New System.Windows.Forms.Label()
        Me.cbMovieFanartPrefSize = New System.Windows.Forms.ComboBox()
        Me.chkMovieFanartOverwrite = New System.Windows.Forms.CheckBox()
        Me.lblMovieFanartWidth = New System.Windows.Forms.Label()
        Me.chkMovieFanartResize = New System.Windows.Forms.CheckBox()
        Me.clbMovieGenre = New System.Windows.Forms.CheckedListBox()
        Me.gbMovieGeneralMediaListOpts = New System.Windows.Forms.GroupBox()
        Me.chkMovieMoviesetCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieDiscArtCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieClearLogoCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieClearArtCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieBannerCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieThemeCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieWatchedCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieEFanartsCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieLandscapeCol = New System.Windows.Forms.CheckBox()
        Me.txtMovieLevTolerance = New System.Windows.Forms.TextBox()
        Me.lblMovieLevTolerance = New System.Windows.Forms.Label()
        Me.chkMovieLevTolerance = New System.Windows.Forms.CheckBox()
        Me.chkMovieSubCol = New System.Windows.Forms.CheckBox()
        Me.chkMoviePosterCol = New System.Windows.Forms.CheckBox()
        Me.gbMovieSortTokensOpts = New System.Windows.Forms.GroupBox()
        Me.btnMovieSortTokenRemove = New System.Windows.Forms.Button()
        Me.btnMovieSortTokenAdd = New System.Windows.Forms.Button()
        Me.txtMovieSortToken = New System.Windows.Forms.TextBox()
        Me.lstMovieSortTokens = New System.Windows.Forms.ListBox()
        Me.chkMovieDisplayYear = New System.Windows.Forms.CheckBox()
        Me.chkMovieEThumbsCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieTrailerCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieNFOCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieFanartCol = New System.Windows.Forms.CheckBox()
        Me.lvMovieSources = New System.Windows.Forms.ListView()
        Me.colID = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colPath = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colRecur = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colFolder = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colSingle = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colExclude = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnMovieSourceRemove = New System.Windows.Forms.Button()
        Me.btnMovieSourceAdd = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.lblSettingsTopDetails = New System.Windows.Forms.Label()
        Me.lblSettingsTopTitle = New System.Windows.Forms.Label()
        Me.pbSettingsTopLogo = New System.Windows.Forms.PictureBox()
        Me.ilSettings = New System.Windows.Forms.ImageList(Me.components)
        Me.tvSettingsList = New System.Windows.Forms.TreeView()
        Me.pnlGeneral = New System.Windows.Forms.Panel()
        Me.tblGeneral = New System.Windows.Forms.TableLayoutPanel()
        Me.gbGeneralInterface = New System.Windows.Forms.GroupBox()
        Me.tblGeneralInterface = New System.Windows.Forms.TableLayoutPanel()
        Me.gbGeneralDateAdded = New System.Windows.Forms.GroupBox()
        Me.tblGeneralDateAdded = New System.Windows.Forms.TableLayoutPanel()
        Me.cbGeneralDateTime = New System.Windows.Forms.ComboBox()
        Me.gbGeneralMainWindow = New System.Windows.Forms.GroupBox()
        Me.tblGeneralMainWindow = New System.Windows.Forms.TableLayoutPanel()
        Me.chkGeneralDoubleClickScrape = New System.Windows.Forms.CheckBox()
        Me.chkGeneralHideLandscape = New System.Windows.Forms.CheckBox()
        Me.chkGeneralHideDiscArt = New System.Windows.Forms.CheckBox()
        Me.chkGeneralShowImgNames = New System.Windows.Forms.CheckBox()
        Me.chkGeneralHideClearArt = New System.Windows.Forms.CheckBox()
        Me.chkGeneralHideClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkGeneralHideBanner = New System.Windows.Forms.CheckBox()
        Me.chkGeneralHideCharacterArt = New System.Windows.Forms.CheckBox()
        Me.gbGeneralScrapers = New System.Windows.Forms.GroupBox()
        Me.tblGeneralScrapers = New System.Windows.Forms.TableLayoutPanel()
        Me.chkGeneralResumeScraper = New System.Windows.Forms.CheckBox()
        Me.pnlMovieGeneral = New System.Windows.Forms.Panel()
        Me.gbMovieGeneralCustomMarker = New System.Windows.Forms.GroupBox()
        Me.btnMovieGeneralCustomMarker4 = New System.Windows.Forms.Button()
        Me.txtMovieGeneralCustomMarker4 = New System.Windows.Forms.TextBox()
        Me.lblMovieGeneralCustomMarker4 = New System.Windows.Forms.Label()
        Me.btnMovieGeneralCustomMarker3 = New System.Windows.Forms.Button()
        Me.txtMovieGeneralCustomMarker3 = New System.Windows.Forms.TextBox()
        Me.lblMovieGeneralCustomMarker3 = New System.Windows.Forms.Label()
        Me.btnMovieGeneralCustomMarker2 = New System.Windows.Forms.Button()
        Me.txtMovieGeneralCustomMarker2 = New System.Windows.Forms.TextBox()
        Me.lblMovieGeneralCustomMarker2 = New System.Windows.Forms.Label()
        Me.btnMovieGeneralCustomMarker1 = New System.Windows.Forms.Button()
        Me.txtMovieGeneralCustomMarker1 = New System.Windows.Forms.TextBox()
        Me.lblMovieGeneralCustomMarker1 = New System.Windows.Forms.Label()
        Me.gbMovieGenrealIMDBMirrorOpts = New System.Windows.Forms.GroupBox()
        Me.lblMovieIMDBMirror = New System.Windows.Forms.Label()
        Me.txtMovieIMDBURL = New System.Windows.Forms.TextBox()
        Me.gbMovieGeneralGenreFilterOpts = New System.Windows.Forms.GroupBox()
        Me.gbMovieGeneralFiltersOpts = New System.Windows.Forms.GroupBox()
        Me.btnMovieFilterReset = New System.Windows.Forms.Button()
        Me.btnMovieFilterDown = New System.Windows.Forms.Button()
        Me.btnMovieFilterUp = New System.Windows.Forms.Button()
        Me.chkMovieProperCase = New System.Windows.Forms.CheckBox()
        Me.btnMovieFilterRemove = New System.Windows.Forms.Button()
        Me.btnMovieFilterAdd = New System.Windows.Forms.Button()
        Me.txtMovieFilter = New System.Windows.Forms.TextBox()
        Me.lstMovieFilters = New System.Windows.Forms.ListBox()
        Me.gbMovieGeneralMissingItemsOpts = New System.Windows.Forms.GroupBox()
        Me.chkMovieMissingDiscArt = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingClearArt = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingTheme = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingLandscape = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingBanner = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingEFanarts = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingTrailer = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingEThumbs = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingPoster = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingNFO = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingSubs = New System.Windows.Forms.CheckBox()
        Me.chkMovieMissingFanart = New System.Windows.Forms.CheckBox()
        Me.pnlFileSystem = New System.Windows.Forms.Panel()
        Me.gbFileSystemValidSubtitlesExts = New System.Windows.Forms.GroupBox()
        Me.btnFileSystemValidSubtitlesExtsReset = New System.Windows.Forms.Button()
        Me.btnFileSystemValidSubtitlesExtsRemove = New System.Windows.Forms.Button()
        Me.btnFileSystemValidSubtitlesExtsAdd = New System.Windows.Forms.Button()
        Me.txtFileSystemValidSubtitlesExts = New System.Windows.Forms.TextBox()
        Me.lstFileSystemValidSubtitlesExts = New System.Windows.Forms.ListBox()
        Me.gbFileSystemExcludedDirs = New System.Windows.Forms.GroupBox()
        Me.btnFileSystemExcludedDirsRemove = New System.Windows.Forms.Button()
        Me.btnFileSystemExcludedDirsAdd = New System.Windows.Forms.Button()
        Me.txtFileSystemExcludedDirs = New System.Windows.Forms.TextBox()
        Me.lstFileSystemExcludedDirs = New System.Windows.Forms.ListBox()
        Me.gbFileSystemValidThemeExts = New System.Windows.Forms.GroupBox()
        Me.btnFileSystemValidThemeExtsReset = New System.Windows.Forms.Button()
        Me.btnFileSystemValidThemeExtsRemove = New System.Windows.Forms.Button()
        Me.btnFileSystemValidThemeExtsAdd = New System.Windows.Forms.Button()
        Me.txtFileSystemValidThemeExts = New System.Windows.Forms.TextBox()
        Me.lstFileSystemValidThemeExts = New System.Windows.Forms.ListBox()
        Me.gbFileSystemNoStackExts = New System.Windows.Forms.GroupBox()
        Me.btnFileSystemNoStackExtsRemove = New System.Windows.Forms.Button()
        Me.btnFileSystemNoStackExtsAdd = New System.Windows.Forms.Button()
        Me.txtFileSystemNoStackExts = New System.Windows.Forms.TextBox()
        Me.lstFileSystemNoStackExts = New System.Windows.Forms.ListBox()
        Me.gbFileSystemValidExts = New System.Windows.Forms.GroupBox()
        Me.btnFileSystemValidExtsReset = New System.Windows.Forms.Button()
        Me.btnFileSystemValidExtsRemove = New System.Windows.Forms.Button()
        Me.btnFileSystemValidExtsAdd = New System.Windows.Forms.Button()
        Me.txtFileSystemValidExts = New System.Windows.Forms.TextBox()
        Me.lstFileSystemValidExts = New System.Windows.Forms.ListBox()
        Me.pnlProxy = New System.Windows.Forms.Panel()
        Me.gbProxyOpts = New System.Windows.Forms.GroupBox()
        Me.gbProxyCredsOpts = New System.Windows.Forms.GroupBox()
        Me.txtProxyDomain = New System.Windows.Forms.TextBox()
        Me.lblProxyDomain = New System.Windows.Forms.Label()
        Me.txtProxyPassword = New System.Windows.Forms.TextBox()
        Me.txtProxyUsername = New System.Windows.Forms.TextBox()
        Me.lblProxyUsername = New System.Windows.Forms.Label()
        Me.lblProxyPassword = New System.Windows.Forms.Label()
        Me.chkProxyCredsEnable = New System.Windows.Forms.CheckBox()
        Me.lblProxyPort = New System.Windows.Forms.Label()
        Me.lblProxyURI = New System.Windows.Forms.Label()
        Me.txtProxyPort = New System.Windows.Forms.TextBox()
        Me.txtProxyURI = New System.Windows.Forms.TextBox()
        Me.chkProxyEnable = New System.Windows.Forms.CheckBox()
        Me.gbMovieBackdropsFolder = New System.Windows.Forms.GroupBox()
        Me.chkMovieBackdropsAuto = New System.Windows.Forms.CheckBox()
        Me.btnMovieBackdropsPathBrowse = New System.Windows.Forms.Button()
        Me.txtMovieBackdropsPath = New System.Windows.Forms.TextBox()
        Me.lblSettingsCurrent = New System.Windows.Forms.Label()
        Me.pnlSettingsCurrent = New System.Windows.Forms.Panel()
        Me.pbSettingsCurrent = New System.Windows.Forms.PictureBox()
        Me.pnlMovieSources = New System.Windows.Forms.Panel()
        Me.gbMovieFileNaming = New System.Windows.Forms.GroupBox()
        Me.tcMovieFileNaming = New System.Windows.Forms.TabControl()
        Me.tpMovieFileNamingXBMC = New System.Windows.Forms.TabPage()
        Me.gbMovieXBMCTheme = New System.Windows.Forms.GroupBox()
        Me.chkMovieXBMCThemeMovie = New System.Windows.Forms.CheckBox()
        Me.btnMovieXBMCThemeCustomPathBrowse = New System.Windows.Forms.Button()
        Me.chkMovieXBMCThemeSub = New System.Windows.Forms.CheckBox()
        Me.txtMovieXBMCThemeSubDir = New System.Windows.Forms.TextBox()
        Me.txtMovieXBMCThemeCustomPath = New System.Windows.Forms.TextBox()
        Me.chkMovieXBMCThemeCustom = New System.Windows.Forms.CheckBox()
        Me.chkMovieXBMCThemeEnable = New System.Windows.Forms.CheckBox()
        Me.gbMovieXBMCOptionalSettings = New System.Windows.Forms.GroupBox()
        Me.chkMovieXBMCProtectVTSBDMV = New System.Windows.Forms.CheckBox()
        Me.chkMovieXBMCTrailerFormat = New System.Windows.Forms.CheckBox()
        Me.gbMovieEden = New System.Windows.Forms.GroupBox()
        Me.chkMovieExtrafanartsEden = New System.Windows.Forms.CheckBox()
        Me.chkMovieExtrathumbsEden = New System.Windows.Forms.CheckBox()
        Me.chkMovieUseEden = New System.Windows.Forms.CheckBox()
        Me.chkMovieActorThumbsEden = New System.Windows.Forms.CheckBox()
        Me.chkMovieTrailerEden = New System.Windows.Forms.CheckBox()
        Me.chkMovieFanartEden = New System.Windows.Forms.CheckBox()
        Me.chkMoviePosterEden = New System.Windows.Forms.CheckBox()
        Me.chkMovieNFOEden = New System.Windows.Forms.CheckBox()
        Me.gbMovieFrodo = New System.Windows.Forms.GroupBox()
        Me.chkMovieExtrafanartsFrodo = New System.Windows.Forms.CheckBox()
        Me.chkMovieExtrathumbsFrodo = New System.Windows.Forms.CheckBox()
        Me.chkMovieUseFrodo = New System.Windows.Forms.CheckBox()
        Me.chkMovieLandscapeFrodo = New System.Windows.Forms.CheckBox()
        Me.chkMovieBannerFrodo = New System.Windows.Forms.CheckBox()
        Me.chkMovieDiscArtFrodo = New System.Windows.Forms.CheckBox()
        Me.chkMovieClearArtFrodo = New System.Windows.Forms.CheckBox()
        Me.chkMovieClearLogoFrodo = New System.Windows.Forms.CheckBox()
        Me.chkMovieActorThumbsFrodo = New System.Windows.Forms.CheckBox()
        Me.chkMovieTrailerFrodo = New System.Windows.Forms.CheckBox()
        Me.chkMovieFanartFrodo = New System.Windows.Forms.CheckBox()
        Me.chkMoviePosterFrodo = New System.Windows.Forms.CheckBox()
        Me.chkMovieNFOFrodo = New System.Windows.Forms.CheckBox()
        Me.tpMovieFileNamingNMT = New System.Windows.Forms.TabPage()
        Me.gbMovieNMTOptionalSettings = New System.Windows.Forms.GroupBox()
        Me.chkMovieYAMJCompatibleSets = New System.Windows.Forms.CheckBox()
        Me.btnMovieYAMJWatchedFilesBrowse = New System.Windows.Forms.Button()
        Me.txtMovieYAMJWatchedFolder = New System.Windows.Forms.TextBox()
        Me.chkMovieYAMJWatchedFile = New System.Windows.Forms.CheckBox()
        Me.gbMovieNMJ = New System.Windows.Forms.GroupBox()
        Me.chkMovieUseNMJ = New System.Windows.Forms.CheckBox()
        Me.chkMovieBannerNMJ = New System.Windows.Forms.CheckBox()
        Me.chkMovieTrailerNMJ = New System.Windows.Forms.CheckBox()
        Me.chkMovieFanartNMJ = New System.Windows.Forms.CheckBox()
        Me.chkMoviePosterNMJ = New System.Windows.Forms.CheckBox()
        Me.chkMovieNFONMJ = New System.Windows.Forms.CheckBox()
        Me.gbMovieYAMJ = New System.Windows.Forms.GroupBox()
        Me.chkMovieUseYAMJ = New System.Windows.Forms.CheckBox()
        Me.chkMovieBannerYAMJ = New System.Windows.Forms.CheckBox()
        Me.chkMovieTrailerYAMJ = New System.Windows.Forms.CheckBox()
        Me.chkMovieFanartYAMJ = New System.Windows.Forms.CheckBox()
        Me.chkMoviePosterYAMJ = New System.Windows.Forms.CheckBox()
        Me.chkMovieNFOYAMJ = New System.Windows.Forms.CheckBox()
        Me.tpMovieFileNamingBoxee = New System.Windows.Forms.TabPage()
        Me.gbMovieBoxee = New System.Windows.Forms.GroupBox()
        Me.chkMovieUseBoxee = New System.Windows.Forms.CheckBox()
        Me.chkMovieFanartBoxee = New System.Windows.Forms.CheckBox()
        Me.chkMoviePosterBoxee = New System.Windows.Forms.CheckBox()
        Me.chkMovieNFOBoxee = New System.Windows.Forms.CheckBox()
        Me.tpMovieFileNamingExpert = New System.Windows.Forms.TabPage()
        Me.gbMovieExpert = New System.Windows.Forms.GroupBox()
        Me.tcMovieFileNamingExpert = New System.Windows.Forms.TabControl()
        Me.tpMovieFileNamingExpertSingle = New System.Windows.Forms.TabPage()
        Me.gbMovieExpertSingleOptionalSettings = New System.Windows.Forms.GroupBox()
        Me.chkMovieUnstackExpertSingle = New System.Windows.Forms.CheckBox()
        Me.chkMovieStackExpertSingle = New System.Windows.Forms.CheckBox()
        Me.chkMovieXBMCTrailerFormatExpertSingle = New System.Windows.Forms.CheckBox()
        Me.gbMovieExpertSingleOptionalImages = New System.Windows.Forms.GroupBox()
        Me.txtMovieActorThumbsExtExpertSingle = New System.Windows.Forms.TextBox()
        Me.chkMovieActorThumbsExpertSingle = New System.Windows.Forms.CheckBox()
        Me.chkMovieExtrafanartsExpertSingle = New System.Windows.Forms.CheckBox()
        Me.chkMovieExtrathumbsExpertSingle = New System.Windows.Forms.CheckBox()
        Me.lblMovieClearArtExpertSingle = New System.Windows.Forms.Label()
        Me.txtMoviePosterExpertSingle = New System.Windows.Forms.TextBox()
        Me.txtMovieFanartExpertSingle = New System.Windows.Forms.TextBox()
        Me.txtMovieTrailerExpertSingle = New System.Windows.Forms.TextBox()
        Me.txtMovieBannerExpertSingle = New System.Windows.Forms.TextBox()
        Me.txtMovieClearLogoExpertSingle = New System.Windows.Forms.TextBox()
        Me.txtMovieClearArtExpertSingle = New System.Windows.Forms.TextBox()
        Me.txtMovieLandscapeExpertSingle = New System.Windows.Forms.TextBox()
        Me.txtMovieDiscArtExpertSingle = New System.Windows.Forms.TextBox()
        Me.lblMovieLandscapeExpertSingle = New System.Windows.Forms.Label()
        Me.lblMovieDiscArtExpertSingle = New System.Windows.Forms.Label()
        Me.lblMovieBannerExpertSingle = New System.Windows.Forms.Label()
        Me.lblMovieTrailerExpertSingle = New System.Windows.Forms.Label()
        Me.lblMovieClearLogoExpertSingle = New System.Windows.Forms.Label()
        Me.lblMovieFanartExpertSingle = New System.Windows.Forms.Label()
        Me.lblMoviePosterExpertSingle = New System.Windows.Forms.Label()
        Me.txtMovieNFOExpertSingle = New System.Windows.Forms.TextBox()
        Me.lblMovieNFOExpertSingle = New System.Windows.Forms.Label()
        Me.tpMovieFileNamingExpertMulti = New System.Windows.Forms.TabPage()
        Me.gbMovieExpertMultiOptionalImages = New System.Windows.Forms.GroupBox()
        Me.txtMovieActorThumbsExtExpertMulti = New System.Windows.Forms.TextBox()
        Me.chkMovieActorThumbsExpertMulti = New System.Windows.Forms.CheckBox()
        Me.gbMovieExpertMultiOptionalSettings = New System.Windows.Forms.GroupBox()
        Me.chkMovieUnstackExpertMulti = New System.Windows.Forms.CheckBox()
        Me.chkMovieStackExpertMulti = New System.Windows.Forms.CheckBox()
        Me.chkMovieXBMCTrailerFormatExpertMulti = New System.Windows.Forms.CheckBox()
        Me.txtMoviePosterExpertMulti = New System.Windows.Forms.TextBox()
        Me.txtMovieFanartExpertMulti = New System.Windows.Forms.TextBox()
        Me.lblMovieClearArtExpertMulti = New System.Windows.Forms.Label()
        Me.txtMovieTrailerExpertMulti = New System.Windows.Forms.TextBox()
        Me.txtMovieBannerExpertMulti = New System.Windows.Forms.TextBox()
        Me.txtMovieClearLogoExpertMulti = New System.Windows.Forms.TextBox()
        Me.txtMovieClearArtExpertMulti = New System.Windows.Forms.TextBox()
        Me.txtMovieLandscapeExpertMulti = New System.Windows.Forms.TextBox()
        Me.txtMovieDiscArtExpertMulti = New System.Windows.Forms.TextBox()
        Me.lblMovieLandscapeExpertMulti = New System.Windows.Forms.Label()
        Me.lblMovieDiscArtExpertMulti = New System.Windows.Forms.Label()
        Me.lblMovieBannerExpertMulti = New System.Windows.Forms.Label()
        Me.lblMovieTrailerExpertMulti = New System.Windows.Forms.Label()
        Me.lblMovieClearLogoExpertMulti = New System.Windows.Forms.Label()
        Me.lblMovieFanartExpertMulti = New System.Windows.Forms.Label()
        Me.lblMoviePosterExpertMulti = New System.Windows.Forms.Label()
        Me.txtMovieNFOExpertMulti = New System.Windows.Forms.TextBox()
        Me.lblMovieNFOExpertMulti = New System.Windows.Forms.Label()
        Me.tpMovieFileNamingExpertVTS = New System.Windows.Forms.TabPage()
        Me.gbMovieExpertVTSOptionalSettings = New System.Windows.Forms.GroupBox()
        Me.chkMovieRecognizeVTSExpertVTS = New System.Windows.Forms.CheckBox()
        Me.chkMovieUseBaseDirectoryExpertVTS = New System.Windows.Forms.CheckBox()
        Me.chkMovieXBMCTrailerFormatExpertVTS = New System.Windows.Forms.CheckBox()
        Me.gbMovieExpertVTSOptionalImages = New System.Windows.Forms.GroupBox()
        Me.txtMovieActorThumbsExtExpertVTS = New System.Windows.Forms.TextBox()
        Me.chkMovieActorThumbsExpertVTS = New System.Windows.Forms.CheckBox()
        Me.chkMovieExtrafanartsExpertVTS = New System.Windows.Forms.CheckBox()
        Me.chkMovieExtrathumbsExpertVTS = New System.Windows.Forms.CheckBox()
        Me.lblMovieClearArtExpertVTS = New System.Windows.Forms.Label()
        Me.txtMoviePosterExpertVTS = New System.Windows.Forms.TextBox()
        Me.txtMovieFanartExpertVTS = New System.Windows.Forms.TextBox()
        Me.txtMovieTrailerExpertVTS = New System.Windows.Forms.TextBox()
        Me.txtMovieBannerExpertVTS = New System.Windows.Forms.TextBox()
        Me.txtMovieClearLogoExpertVTS = New System.Windows.Forms.TextBox()
        Me.txtMovieClearArtExpertVTS = New System.Windows.Forms.TextBox()
        Me.txtMovieLandscapeExpertVTS = New System.Windows.Forms.TextBox()
        Me.txtMovieDiscArtExpertVTS = New System.Windows.Forms.TextBox()
        Me.lblMovieLandscapeExpertVTS = New System.Windows.Forms.Label()
        Me.lblMovieDiscArtExpertVTS = New System.Windows.Forms.Label()
        Me.lblMovieBannerExpertVTS = New System.Windows.Forms.Label()
        Me.lblMovieTrailerExpertVTS = New System.Windows.Forms.Label()
        Me.lblMovieClearLogoExpertVTS = New System.Windows.Forms.Label()
        Me.lblMovieFanartExpertVTS = New System.Windows.Forms.Label()
        Me.lblMoviePosterExpertVTS = New System.Windows.Forms.Label()
        Me.txtMovieNFOExpertVTS = New System.Windows.Forms.TextBox()
        Me.lblMovieNFOExpertVTS = New System.Windows.Forms.Label()
        Me.tpMovieFileNamingExpertBDMV = New System.Windows.Forms.TabPage()
        Me.gbMovieExpertBDMVOptionalSettings = New System.Windows.Forms.GroupBox()
        Me.chkMovieUseBaseDirectoryExpertBDMV = New System.Windows.Forms.CheckBox()
        Me.chkMovieXBMCTrailerFormatExpertBDMV = New System.Windows.Forms.CheckBox()
        Me.gbMovieExpertBDMVOptionalImages = New System.Windows.Forms.GroupBox()
        Me.txtMovieActorThumbsExtExpertBDMV = New System.Windows.Forms.TextBox()
        Me.chkMovieActorThumbsExpertBDMV = New System.Windows.Forms.CheckBox()
        Me.chkMovieExtrafanartsExpertBDMV = New System.Windows.Forms.CheckBox()
        Me.chkMovieExtrathumbsExpertBDMV = New System.Windows.Forms.CheckBox()
        Me.lblMovieClearArtExpertBDMV = New System.Windows.Forms.Label()
        Me.txtMoviePosterExpertBDMV = New System.Windows.Forms.TextBox()
        Me.txtMovieFanartExpertBDMV = New System.Windows.Forms.TextBox()
        Me.txtMovieTrailerExpertBDMV = New System.Windows.Forms.TextBox()
        Me.txtMovieBannerExpertBDMV = New System.Windows.Forms.TextBox()
        Me.txtMovieClearLogoExpertBDMV = New System.Windows.Forms.TextBox()
        Me.txtMovieClearArtExpertBDMV = New System.Windows.Forms.TextBox()
        Me.txtMovieLandscapeExpertBDMV = New System.Windows.Forms.TextBox()
        Me.txtMovieDiscArtExpertBDMV = New System.Windows.Forms.TextBox()
        Me.lblMovieLandscapeExpertBDMV = New System.Windows.Forms.Label()
        Me.lblMovieDiscArtExpertBDMV = New System.Windows.Forms.Label()
        Me.lblMovieBannerExpertBDMV = New System.Windows.Forms.Label()
        Me.lblMovieTrailerExpertBDMV = New System.Windows.Forms.Label()
        Me.lblMovieClearLogoExpertBDMV = New System.Windows.Forms.Label()
        Me.lblMovieFanartExpertBDMV = New System.Windows.Forms.Label()
        Me.lblMoviePosterExpertBDMV = New System.Windows.Forms.Label()
        Me.txtMovieNFOExpertBDMV = New System.Windows.Forms.TextBox()
        Me.lblMovieNFOExpertBDMV = New System.Windows.Forms.Label()
        Me.chkMovieUseExpert = New System.Windows.Forms.CheckBox()
        Me.btnMovieSourceEdit = New System.Windows.Forms.Button()
        Me.gbMovieSourcesMiscOpts = New System.Windows.Forms.GroupBox()
        Me.chkMovieScanOrderModify = New System.Windows.Forms.CheckBox()
        Me.chkMovieSortBeforeScan = New System.Windows.Forms.CheckBox()
        Me.chkMovieGeneralIgnoreLastScan = New System.Windows.Forms.CheckBox()
        Me.chkMovieCleanDB = New System.Windows.Forms.CheckBox()
        Me.chkMovieSkipStackedSizeCheck = New System.Windows.Forms.CheckBox()
        Me.lblMovieSkipLessThanMB = New System.Windows.Forms.Label()
        Me.txtMovieSkipLessThan = New System.Windows.Forms.TextBox()
        Me.lblMovieSkipLessThan = New System.Windows.Forms.Label()
        Me.gbMovieSetMSAAPath = New System.Windows.Forms.GroupBox()
        Me.btnMovieSetPathMSAABrowse = New System.Windows.Forms.Button()
        Me.txtMovieSetPathMSAA = New System.Windows.Forms.TextBox()
        Me.pnlTVGeneral = New System.Windows.Forms.Panel()
        Me.gbTVGeneralMissingItemsOpts = New System.Windows.Forms.GroupBox()
        Me.gbTVGeneralMissingShowOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVShowMissingBanner = New System.Windows.Forms.CheckBox()
        Me.chkTVShowMissingTheme = New System.Windows.Forms.CheckBox()
        Me.chkTVShowMissingClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkTVShowMissingLandscape = New System.Windows.Forms.CheckBox()
        Me.chkTVShowMissingCharacterArt = New System.Windows.Forms.CheckBox()
        Me.chkTVShowMissingEFanarts = New System.Windows.Forms.CheckBox()
        Me.chkTVShowMissingPoster = New System.Windows.Forms.CheckBox()
        Me.chkTVShowMissingClearArt = New System.Windows.Forms.CheckBox()
        Me.chkTVShowMissingNFO = New System.Windows.Forms.CheckBox()
        Me.chkTVShowMissingFanart = New System.Windows.Forms.CheckBox()
        Me.gbTVSortTokensOpts = New System.Windows.Forms.GroupBox()
        Me.btnTVSortTokenRemove = New System.Windows.Forms.Button()
        Me.btnTVSortTokenAdd = New System.Windows.Forms.Button()
        Me.txtTVSortToken = New System.Windows.Forms.TextBox()
        Me.lstTVSortTokens = New System.Windows.Forms.ListBox()
        Me.gbTVGeneralLangOpts = New System.Windows.Forms.GroupBox()
        Me.btnTVGeneralLangFetch = New System.Windows.Forms.Button()
        Me.cbTVGeneralLang = New System.Windows.Forms.ComboBox()
        Me.gbTVGeneralMediaListOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVDisplayStatus = New System.Windows.Forms.CheckBox()
        Me.chkTVDisplayMissingEpisodes = New System.Windows.Forms.CheckBox()
        Me.gbTVGeneralListEpisodeOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVEpisodeNfoCol = New System.Windows.Forms.CheckBox()
        Me.chkTVEpisodeFanartCol = New System.Windows.Forms.CheckBox()
        Me.chkTVEpisodeWatchedCol = New System.Windows.Forms.CheckBox()
        Me.chkTVEpisodePosterCol = New System.Windows.Forms.CheckBox()
        Me.gbTVGeneralListSeasonOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVSeasonLandscapeCol = New System.Windows.Forms.CheckBox()
        Me.chkTVSeasonBannerCol = New System.Windows.Forms.CheckBox()
        Me.chkTVSeasonFanartCol = New System.Windows.Forms.CheckBox()
        Me.chkTVSeasonPosterCol = New System.Windows.Forms.CheckBox()
        Me.gbTVGeneralListShowOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVShowClearLogoCol = New System.Windows.Forms.CheckBox()
        Me.chkTVShowClearArtCol = New System.Windows.Forms.CheckBox()
        Me.chkTVShowCharacterArtCol = New System.Windows.Forms.CheckBox()
        Me.chkTVShowEFanartsCol = New System.Windows.Forms.CheckBox()
        Me.chkTVShowThemeCol = New System.Windows.Forms.CheckBox()
        Me.chkTVShowLandscapeCol = New System.Windows.Forms.CheckBox()
        Me.chkTVShowBannerCol = New System.Windows.Forms.CheckBox()
        Me.chkTVShowNfoCol = New System.Windows.Forms.CheckBox()
        Me.chkTVShowFanartCol = New System.Windows.Forms.CheckBox()
        Me.chkTVShowPosterCol = New System.Windows.Forms.CheckBox()
        Me.gbTVEpisodeFilterOpts = New System.Windows.Forms.GroupBox()
        Me.btnTVEpisodeFilterReset = New System.Windows.Forms.Button()
        Me.chkTVEpisodeNoFilter = New System.Windows.Forms.CheckBox()
        Me.btnTVEpisodeFilterDown = New System.Windows.Forms.Button()
        Me.btnTVEpisodeFilterUp = New System.Windows.Forms.Button()
        Me.chkTVEpisodeProperCase = New System.Windows.Forms.CheckBox()
        Me.btnTVEpisodeFilterRemove = New System.Windows.Forms.Button()
        Me.btnTVEpisodeFilterAdd = New System.Windows.Forms.Button()
        Me.txtTVEpisodeFilter = New System.Windows.Forms.TextBox()
        Me.lstTVEpisodeFilter = New System.Windows.Forms.ListBox()
        Me.gbTVGeneralMiscOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVGeneralMarkNewShows = New System.Windows.Forms.CheckBox()
        Me.chkTVGeneralMarkNewEpisodes = New System.Windows.Forms.CheckBox()
        Me.gbTVShowFilterOpts = New System.Windows.Forms.GroupBox()
        Me.btnTVShowFilterReset = New System.Windows.Forms.Button()
        Me.btnTVShowFilterDown = New System.Windows.Forms.Button()
        Me.btnTVShowFilterUp = New System.Windows.Forms.Button()
        Me.chkTVShowProperCase = New System.Windows.Forms.CheckBox()
        Me.btnTVShowFilterRemove = New System.Windows.Forms.Button()
        Me.btnTVShowFilterAdd = New System.Windows.Forms.Button()
        Me.txtTVShowFilter = New System.Windows.Forms.TextBox()
        Me.lstTVShowFilter = New System.Windows.Forms.ListBox()
        Me.pnlTVSources = New System.Windows.Forms.Panel()
        Me.tcTVSources = New System.Windows.Forms.TabControl()
        Me.tpTVSourcesGeneral = New System.Windows.Forms.TabPage()
        Me.gbTVFileNaming = New System.Windows.Forms.GroupBox()
        Me.tcTVFileNaming = New System.Windows.Forms.TabControl()
        Me.tpTVFileNamingXBMC = New System.Windows.Forms.TabPage()
        Me.gbTVXBMCAdditional = New System.Windows.Forms.GroupBox()
        Me.chkTVShowExtrafanartsXBMC = New System.Windows.Forms.CheckBox()
        Me.btnTVShowTVThemeBrowse = New System.Windows.Forms.Button()
        Me.txtTVShowTVThemeFolderXBMC = New System.Windows.Forms.TextBox()
        Me.chkTVShowTVThemeXBMC = New System.Windows.Forms.CheckBox()
        Me.chkTVSeasonLandscapeXBMC = New System.Windows.Forms.CheckBox()
        Me.chkTVShowLandscapeXBMC = New System.Windows.Forms.CheckBox()
        Me.chkTVShowCharacterArtXBMC = New System.Windows.Forms.CheckBox()
        Me.chkTVShowClearArtXBMC = New System.Windows.Forms.CheckBox()
        Me.chkTVShowClearLogoXBMC = New System.Windows.Forms.CheckBox()
        Me.gbTVFrodo = New System.Windows.Forms.GroupBox()
        Me.chkTVSeasonPosterFrodo = New System.Windows.Forms.CheckBox()
        Me.chkTVShowBannerFrodo = New System.Windows.Forms.CheckBox()
        Me.chkTVUseFrodo = New System.Windows.Forms.CheckBox()
        Me.chkTVEpisodeActorThumbsFrodo = New System.Windows.Forms.CheckBox()
        Me.chkTVSeasonBannerFrodo = New System.Windows.Forms.CheckBox()
        Me.chkTVEpisodePosterFrodo = New System.Windows.Forms.CheckBox()
        Me.chkTVShowActorThumbsFrodo = New System.Windows.Forms.CheckBox()
        Me.chkTVSeasonFanartFrodo = New System.Windows.Forms.CheckBox()
        Me.chkTVShowFanartFrodo = New System.Windows.Forms.CheckBox()
        Me.chkTVShowPosterFrodo = New System.Windows.Forms.CheckBox()
        Me.tpTVFileNamingNMT = New System.Windows.Forms.TabPage()
        Me.gbTVNMT = New System.Windows.Forms.GroupBox()
        Me.chkTVSeasonPosterNMJ = New System.Windows.Forms.CheckBox()
        Me.chkTVShowBannerNMJ = New System.Windows.Forms.CheckBox()
        Me.chkTVSeasonBannerNMJ = New System.Windows.Forms.CheckBox()
        Me.chkTVEpisodePosterNMJ = New System.Windows.Forms.CheckBox()
        Me.chkTVSeasonFanartNMJ = New System.Windows.Forms.CheckBox()
        Me.chkTVShowFanartNMJ = New System.Windows.Forms.CheckBox()
        Me.chkTVShowPosterNMJ = New System.Windows.Forms.CheckBox()
        Me.chkTVUseNMJ = New System.Windows.Forms.CheckBox()
        Me.gbTVYAMJ = New System.Windows.Forms.GroupBox()
        Me.chkTVSeasonPosterYAMJ = New System.Windows.Forms.CheckBox()
        Me.chkTVShowBannerYAMJ = New System.Windows.Forms.CheckBox()
        Me.chkTVSeasonBannerYAMJ = New System.Windows.Forms.CheckBox()
        Me.chkTVEpisodePosterYAMJ = New System.Windows.Forms.CheckBox()
        Me.chkTVSeasonFanartYAMJ = New System.Windows.Forms.CheckBox()
        Me.chkTVShowFanartYAMJ = New System.Windows.Forms.CheckBox()
        Me.chkTVShowPosterYAMJ = New System.Windows.Forms.CheckBox()
        Me.chkTVUseYAMJ = New System.Windows.Forms.CheckBox()
        Me.tpTVFileNamingBoxee = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkTVSeasonPosterBoxee = New System.Windows.Forms.CheckBox()
        Me.chkTVShowBannerBoxee = New System.Windows.Forms.CheckBox()
        Me.chkTVEpisodePosterBoxee = New System.Windows.Forms.CheckBox()
        Me.chkTVShowFanartBoxee = New System.Windows.Forms.CheckBox()
        Me.chkTVShowPosterBoxee = New System.Windows.Forms.CheckBox()
        Me.chkTVUseBoxee = New System.Windows.Forms.CheckBox()
        Me.tpTVFileNamingExpert = New System.Windows.Forms.TabPage()
        Me.lvTVSources = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.gbTVSourcesMiscOpts = New System.Windows.Forms.GroupBox()
        Me.lblTVSkipLessThanMB = New System.Windows.Forms.Label()
        Me.txtTVSkipLessThan = New System.Windows.Forms.TextBox()
        Me.lblTVSkipLessThan = New System.Windows.Forms.Label()
        Me.chkTVScanOrderModify = New System.Windows.Forms.CheckBox()
        Me.chkTVGeneralIgnoreLastScan = New System.Windows.Forms.CheckBox()
        Me.chkTVCleanDB = New System.Windows.Forms.CheckBox()
        Me.btnTVSourceAdd = New System.Windows.Forms.Button()
        Me.btnTVSourceEdit = New System.Windows.Forms.Button()
        Me.btnRemTVSource = New System.Windows.Forms.Button()
        Me.tpTVSourcesRegex = New System.Windows.Forms.TabPage()
        Me.btnTVShowRegexGet = New System.Windows.Forms.Button()
        Me.btnTVShowRegexDown = New System.Windows.Forms.Button()
        Me.btnTVShowRegexUp = New System.Windows.Forms.Button()
        Me.btnTVShowRegexReset = New System.Windows.Forms.Button()
        Me.gbTVShowRegex = New System.Windows.Forms.GroupBox()
        Me.btnTVShowRegexClear = New System.Windows.Forms.Button()
        Me.lblTVSeasonMatch = New System.Windows.Forms.Label()
        Me.btnTVShowRegexAdd = New System.Windows.Forms.Button()
        Me.txtTVSeasonRegex = New System.Windows.Forms.TextBox()
        Me.lblTVEpisodeRetrieve = New System.Windows.Forms.Label()
        Me.cbTVSeasonRetrieve = New System.Windows.Forms.ComboBox()
        Me.lblTVSeasonRetrieve = New System.Windows.Forms.Label()
        Me.txtTVEpisodeRegex = New System.Windows.Forms.TextBox()
        Me.lblTVEpisodeMatch = New System.Windows.Forms.Label()
        Me.cbTVEpisodeRetrieve = New System.Windows.Forms.ComboBox()
        Me.btnTVShowRegexEdit = New System.Windows.Forms.Button()
        Me.btnTVShowRegexRemove = New System.Windows.Forms.Button()
        Me.lvTVShowRegex = New System.Windows.Forms.ListView()
        Me.colTVShowRegexID = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colTVShowRegexSeason = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colTVShowRegexSeasonApply = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colTVShowRegexEpisode = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colTVShowRegexEpisodeApply = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.ToolTips = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlTVImages = New System.Windows.Forms.Panel()
        Me.tcTVImages = New System.Windows.Forms.TabControl()
        Me.tpTVShow = New System.Windows.Forms.TabPage()
        Me.gbTVShowEFanartsOpts = New System.Windows.Forms.GroupBox()
        Me.lblTVShowEFanartsLimit = New System.Windows.Forms.Label()
        Me.txtTVShowEFanartsLimit = New System.Windows.Forms.TextBox()
        Me.chkTVShowEFanartsPrefOnly = New System.Windows.Forms.CheckBox()
        Me.txtTVShowEFanartsWidth = New System.Windows.Forms.TextBox()
        Me.txtTVShowEFanartsHeight = New System.Windows.Forms.TextBox()
        Me.lblTVShowEFanartsWidth = New System.Windows.Forms.Label()
        Me.lblTVShowEFanartsHeight = New System.Windows.Forms.Label()
        Me.chkTVShowEFanartsResize = New System.Windows.Forms.CheckBox()
        Me.lblTVShowEFanartsSize = New System.Windows.Forms.Label()
        Me.cbTVShowEFanartsPrefSize = New System.Windows.Forms.ComboBox()
        Me.chkTVShowEFanartsOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbTVShowCharacterArtOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVShowCharacterArtOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbTVShowClearArtOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVShowClearArtOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbTVShowClearLogoOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVShowClearLogoOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbTVShowLandscapeOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVShowLandscapeOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbTVShowBannerOpts = New System.Windows.Forms.GroupBox()
        Me.txtTVShowBannerWidth = New System.Windows.Forms.TextBox()
        Me.txtTVShowBannerHeight = New System.Windows.Forms.TextBox()
        Me.lblTVShowBannerWidth = New System.Windows.Forms.Label()
        Me.lblTVShowBannerHeight = New System.Windows.Forms.Label()
        Me.chkTVShowBannerResize = New System.Windows.Forms.CheckBox()
        Me.lblTVShowBannerType = New System.Windows.Forms.Label()
        Me.cbTVShowBannerPrefType = New System.Windows.Forms.ComboBox()
        Me.chkTVShowBannerOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbTVShowPosterOpts = New System.Windows.Forms.GroupBox()
        Me.txtTVShowPosterWidth = New System.Windows.Forms.TextBox()
        Me.txtTVShowPosterHeight = New System.Windows.Forms.TextBox()
        Me.lblTVShowPosterWidth = New System.Windows.Forms.Label()
        Me.lblTVShowPosterHeight = New System.Windows.Forms.Label()
        Me.chkTVShowPosterResize = New System.Windows.Forms.CheckBox()
        Me.lblTVShowPosterSize = New System.Windows.Forms.Label()
        Me.cbTVShowPosterPrefSize = New System.Windows.Forms.ComboBox()
        Me.chkTVShowPosterOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbTVShowFanartOpts = New System.Windows.Forms.GroupBox()
        Me.txtTVShowFanartWidth = New System.Windows.Forms.TextBox()
        Me.txtTVShowFanartHeight = New System.Windows.Forms.TextBox()
        Me.lblTVShowFanartWidth = New System.Windows.Forms.Label()
        Me.lblTVShowFanartHeight = New System.Windows.Forms.Label()
        Me.chkTVShowFanartResize = New System.Windows.Forms.CheckBox()
        Me.cbTVShowFanartPrefSize = New System.Windows.Forms.ComboBox()
        Me.lblTVShowFanartSize = New System.Windows.Forms.Label()
        Me.chkTVShowFanartOverwrite = New System.Windows.Forms.CheckBox()
        Me.tpTVAllSeasons = New System.Windows.Forms.TabPage()
        Me.gbTVASLandscapeOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVASLandscapeOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbTVASFanartOpts = New System.Windows.Forms.GroupBox()
        Me.txtTVASFanartWidth = New System.Windows.Forms.TextBox()
        Me.txtTVASFanartHeight = New System.Windows.Forms.TextBox()
        Me.lblTVASFanartWidth = New System.Windows.Forms.Label()
        Me.lblTVASFanartHeight = New System.Windows.Forms.Label()
        Me.chkTVASFanartResize = New System.Windows.Forms.CheckBox()
        Me.cbTVASFanartPrefSize = New System.Windows.Forms.ComboBox()
        Me.lblTVASFanartSize = New System.Windows.Forms.Label()
        Me.chkTVASFanartOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbTVASBannerOpts = New System.Windows.Forms.GroupBox()
        Me.txtTVASBannerWidth = New System.Windows.Forms.TextBox()
        Me.txtTVASBannerHeight = New System.Windows.Forms.TextBox()
        Me.lblTVASBannerWidth = New System.Windows.Forms.Label()
        Me.lblTVASBannerHeight = New System.Windows.Forms.Label()
        Me.chkTVASBannerResize = New System.Windows.Forms.CheckBox()
        Me.lblTVASBannerType = New System.Windows.Forms.Label()
        Me.cbTVASBannerPrefType = New System.Windows.Forms.ComboBox()
        Me.chkTVASBannerOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbTVASPosterOpts = New System.Windows.Forms.GroupBox()
        Me.txtTVASPosterWidth = New System.Windows.Forms.TextBox()
        Me.txtTVASPosterHeight = New System.Windows.Forms.TextBox()
        Me.lblTVASPosterWidth = New System.Windows.Forms.Label()
        Me.lblTVASPosterHeight = New System.Windows.Forms.Label()
        Me.chkTVASPosterResize = New System.Windows.Forms.CheckBox()
        Me.lblTVASPosterSize = New System.Windows.Forms.Label()
        Me.cbTVASPosterPrefSize = New System.Windows.Forms.ComboBox()
        Me.chkTVASPosterOverwrite = New System.Windows.Forms.CheckBox()
        Me.tpTVSeason = New System.Windows.Forms.TabPage()
        Me.gbTVSeasonLandscapeOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVSeasonLandscapeOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbTVSeasonBannerOpts = New System.Windows.Forms.GroupBox()
        Me.txtTVSeasonBannerWidth = New System.Windows.Forms.TextBox()
        Me.txtTVSeasonBannerHeight = New System.Windows.Forms.TextBox()
        Me.lblTVSeasonBannerWidth = New System.Windows.Forms.Label()
        Me.lblTVSeasonBannerHeight = New System.Windows.Forms.Label()
        Me.chkTVSeasonBannerResize = New System.Windows.Forms.CheckBox()
        Me.lblTVSeasonBannerType = New System.Windows.Forms.Label()
        Me.cbTVSeasonBannerPrefType = New System.Windows.Forms.ComboBox()
        Me.chkTVSeasonBannerOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbTVSeasonPosterOpts = New System.Windows.Forms.GroupBox()
        Me.txtTVSeasonPosterWidth = New System.Windows.Forms.TextBox()
        Me.txtTVSeasonPosterHeight = New System.Windows.Forms.TextBox()
        Me.lblTVSeasonPosterWidth = New System.Windows.Forms.Label()
        Me.lblTVSeasonPosterHeight = New System.Windows.Forms.Label()
        Me.chkTVSeasonPosterResize = New System.Windows.Forms.CheckBox()
        Me.lblTVSeasonPosterSize = New System.Windows.Forms.Label()
        Me.cbTVSeasonPosterPrefSize = New System.Windows.Forms.ComboBox()
        Me.chkTVSeasonPosterOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbTVSeasonFanartOpts = New System.Windows.Forms.GroupBox()
        Me.txtTVSeasonFanartWidth = New System.Windows.Forms.TextBox()
        Me.txtTVSeasonFanartHeight = New System.Windows.Forms.TextBox()
        Me.lblTVSeasonFanartWidth = New System.Windows.Forms.Label()
        Me.lblTVSeasonFanartHeight = New System.Windows.Forms.Label()
        Me.chkTVSeasonFanartResize = New System.Windows.Forms.CheckBox()
        Me.cbTVSeasonFanartPrefSize = New System.Windows.Forms.ComboBox()
        Me.lblTVSeasonFanartSize = New System.Windows.Forms.Label()
        Me.chkTVSeasonFanartOverwrite = New System.Windows.Forms.CheckBox()
        Me.tpTVEpisode = New System.Windows.Forms.TabPage()
        Me.gbTVEpisodePosterOpts = New System.Windows.Forms.GroupBox()
        Me.lblTVEpisodePosterSize = New System.Windows.Forms.Label()
        Me.cbTVEpisodePosterPrefSize = New System.Windows.Forms.ComboBox()
        Me.txtTVEpisodePosterWidth = New System.Windows.Forms.TextBox()
        Me.txtTVEpisodePosterHeight = New System.Windows.Forms.TextBox()
        Me.lblTVEpisodePosterWidth = New System.Windows.Forms.Label()
        Me.lblTVEpisodePosterHeight = New System.Windows.Forms.Label()
        Me.chkTVEpisodePosterResize = New System.Windows.Forms.CheckBox()
        Me.chkTVEpisodePosterOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbTVEpisodeFanartOpts = New System.Windows.Forms.GroupBox()
        Me.txtTVEpisodeFanartWidth = New System.Windows.Forms.TextBox()
        Me.txtTVEpisodeFanartHeight = New System.Windows.Forms.TextBox()
        Me.lblTVEpisodeFanartWidth = New System.Windows.Forms.Label()
        Me.lblTVEpisodeFanartHeight = New System.Windows.Forms.Label()
        Me.chkTVEpisodeFanartResize = New System.Windows.Forms.CheckBox()
        Me.cbTVEpisodeFanartPrefSize = New System.Windows.Forms.ComboBox()
        Me.lblTVEpisodeFanartSize = New System.Windows.Forms.Label()
        Me.chkTVEpisodeFanartOverwrite = New System.Windows.Forms.CheckBox()
        Me.pnlTVScraper = New System.Windows.Forms.Panel()
        Me.gbTVScraperMiscOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVScraperUseSRuntimeForEp = New System.Windows.Forms.CheckBox()
        Me.gbTVScraperFieldsOpts = New System.Windows.Forms.GroupBox()
        Me.gbTVScraperFieldsShowOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVScraperShowVotes = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperShowRuntime = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperShowStatus = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperShowRating = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperShowActors = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperShowStudio = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperShowPremiered = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperShowEpiGuideURL = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperShowMPAA = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperShowPlot = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperShowGenre = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperShowTitle = New System.Windows.Forms.CheckBox()
        Me.gbTVScraperFieldsEpisodeOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVScraperEpisodeVotes = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperEpisodeRuntime = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperEpisodeActors = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperEpisodeCredits = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperEpisodeDirector = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperEpisodePlot = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperEpisodeRating = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperEpisodeAired = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperEpisodeTitle = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperEpisodeEpisode = New System.Windows.Forms.CheckBox()
        Me.chkTVScraperEpisodeSeason = New System.Windows.Forms.CheckBox()
        Me.gbTVScraperGlobalLocksOpts = New System.Windows.Forms.GroupBox()
        Me.gbTVScraperGlobalLocksEpisodeOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVLockEpisodeVotes = New System.Windows.Forms.CheckBox()
        Me.chkTVLockEpisodeRuntime = New System.Windows.Forms.CheckBox()
        Me.chkTVLockEpisodeTitle = New System.Windows.Forms.CheckBox()
        Me.chkTVLockEpisodeRating = New System.Windows.Forms.CheckBox()
        Me.chkTVLockEpisodePlot = New System.Windows.Forms.CheckBox()
        Me.gbTVScraperGlobalLocksShowOpts = New System.Windows.Forms.GroupBox()
        Me.chkTVLockShowVotes = New System.Windows.Forms.CheckBox()
        Me.chkTVLockShowRuntime = New System.Windows.Forms.CheckBox()
        Me.chkTVLockShowStatus = New System.Windows.Forms.CheckBox()
        Me.chkTVLockShowPlot = New System.Windows.Forms.CheckBox()
        Me.chkTVLockShowGenre = New System.Windows.Forms.CheckBox()
        Me.chkTVLockShowStudio = New System.Windows.Forms.CheckBox()
        Me.chkTVLockShowRating = New System.Windows.Forms.CheckBox()
        Me.chkTVLockShowTitle = New System.Windows.Forms.CheckBox()
        Me.gbTVScraperMetaDataOpts = New System.Windows.Forms.GroupBox()
        Me.gbTVScraperDefFIExtOpts = New System.Windows.Forms.GroupBox()
        Me.lstTVScraperDefFIExt = New System.Windows.Forms.ListBox()
        Me.txtTVScraperDefFIExt = New System.Windows.Forms.TextBox()
        Me.lblTVScraperDefFIExt = New System.Windows.Forms.Label()
        Me.btnTVScraperDefFIExtRemove = New System.Windows.Forms.Button()
        Me.btnTVScraperDefFIExtEdit = New System.Windows.Forms.Button()
        Me.btnTVScraperDefFIExtAdd = New System.Windows.Forms.Button()
        Me.gbTVScraperDurationOpts = New System.Windows.Forms.GroupBox()
        Me.lblTVScraperDurationRuntimeFormat = New System.Windows.Forms.Label()
        Me.chkTVScraperUseMDDuration = New System.Windows.Forms.CheckBox()
        Me.txtTVScraperDurationRuntimeFormat = New System.Windows.Forms.TextBox()
        Me.cbTVLanguageOverlay = New System.Windows.Forms.ComboBox()
        Me.lblTVLanguageOverlay = New System.Windows.Forms.Label()
        Me.chkTVScraperMetaDataScan = New System.Windows.Forms.CheckBox()
        Me.gbTVScraperOptionsOpts = New System.Windows.Forms.GroupBox()
        Me.lblTVScraperRatingRegion = New System.Windows.Forms.Label()
        Me.cbTVScraperRatingRegion = New System.Windows.Forms.ComboBox()
        Me.lblTVScraperOptionsOrdering = New System.Windows.Forms.Label()
        Me.cbTVScraperOptionsOrdering = New System.Windows.Forms.ComboBox()
        Me.lblTVScraperUpdateTime = New System.Windows.Forms.Label()
        Me.cbTVScraperUpdateTime = New System.Windows.Forms.ComboBox()
        Me.cbMovieScraperCertLang = New System.Windows.Forms.ComboBox()
        Me.lblMovieScraperOutlineLimit = New System.Windows.Forms.Label()
        Me.txtMovieScraperOutlineLimit = New System.Windows.Forms.TextBox()
        Me.gbMovieScraperMetaDataOpts = New System.Windows.Forms.GroupBox()
        Me.gbMovieScraperDefFIExtOpts = New System.Windows.Forms.GroupBox()
        Me.lstMovieScraperDefFIExt = New System.Windows.Forms.ListBox()
        Me.txtMovieScraperDefFIExt = New System.Windows.Forms.TextBox()
        Me.lblMovieScraperDefFIExt = New System.Windows.Forms.Label()
        Me.btnMovieScraperDefFIExtRemove = New System.Windows.Forms.Button()
        Me.btnMovieScraperDefFIExtEdit = New System.Windows.Forms.Button()
        Me.btnMovieScraperDefFIExtAdd = New System.Windows.Forms.Button()
        Me.chkMovieScraperMetaDataIFOScan = New System.Windows.Forms.CheckBox()
        Me.cbMovieLanguageOverlay = New System.Windows.Forms.ComboBox()
        Me.lblMovieLanguageOverlay = New System.Windows.Forms.Label()
        Me.gbMovieScraperDurationFormatOpts = New System.Windows.Forms.GroupBox()
        Me.lblMovieScraperDurationRuntimeFormat = New System.Windows.Forms.Label()
        Me.txtMovieScraperDurationRuntimeFormat = New System.Windows.Forms.TextBox()
        Me.chkMovieScraperUseMDDuration = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperMetaDataScan = New System.Windows.Forms.CheckBox()
        Me.gbMovieScraperGlobalOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieScraperGlobalOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieLockCollectionID = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkMovieLockRating = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockTitle = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperTitle = New System.Windows.Forms.Label()
        Me.lblMovieScraperRating = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblMovieScraperCollectionID = New System.Windows.Forms.Label()
        Me.lblMovieLockLanguageA = New System.Windows.Forms.Label()
        Me.lblMovieLockLanguageV = New System.Windows.Forms.Label()
        Me.lblMovieLockCollections = New System.Windows.Forms.Label()
        Me.chkMovieLockLanguageA = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockLanguageV = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperTitle = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperRating = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCollectionID = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockCollections = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperOriginalTitle = New System.Windows.Forms.Label()
        Me.chkMovieScraperOriginalTitle = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockOriginalTitle = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperYear = New System.Windows.Forms.Label()
        Me.chkMovieScraperYear = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockYear = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperRelease = New System.Windows.Forms.Label()
        Me.chkMovieScraperRelease = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockReleaseDate = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperPlot = New System.Windows.Forms.Label()
        Me.chkMovieScraperPlot = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockPlot = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperOutline = New System.Windows.Forms.Label()
        Me.chkMovieScraperOutline = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockOutline = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperTagline = New System.Windows.Forms.Label()
        Me.chkMovieScraperTagline = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockTagline = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperVotes = New System.Windows.Forms.Label()
        Me.chkMovieScraperVotes = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockVotes = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperTop250 = New System.Windows.Forms.Label()
        Me.chkMovieScraperTop250 = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockTop250 = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperMPAA = New System.Windows.Forms.Label()
        Me.chkMovieScraperMPAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockMPAA = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperCertification = New System.Windows.Forms.Label()
        Me.chkMovieScraperCert = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockCert = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperRuntime = New System.Windows.Forms.Label()
        Me.chkMovieScraperRuntime = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockRuntime = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperStudio = New System.Windows.Forms.Label()
        Me.chkMovieScraperStudio = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockStudio = New System.Windows.Forms.CheckBox()
        Me.txtMovieScraperStudioLimit = New System.Windows.Forms.TextBox()
        Me.lblMovieScraperTags = New System.Windows.Forms.Label()
        Me.chkMovieScraperTags = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockTags = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperTrailer = New System.Windows.Forms.Label()
        Me.chkMovieScraperTrailer = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockTrailer = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGenre = New System.Windows.Forms.Label()
        Me.chkMovieScraperGenre = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockGenre = New System.Windows.Forms.CheckBox()
        Me.txtMovieScraperGenreLimit = New System.Windows.Forms.TextBox()
        Me.lblMovieScraperCast = New System.Windows.Forms.Label()
        Me.chkMovieScraperCast = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockActors = New System.Windows.Forms.CheckBox()
        Me.txtMovieScraperCastLimit = New System.Windows.Forms.TextBox()
        Me.lblMovieScraperCountry = New System.Windows.Forms.Label()
        Me.chkMovieScraperCountry = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockCountry = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperDirector = New System.Windows.Forms.Label()
        Me.chkMovieScraperDirector = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockDirector = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperCredits = New System.Windows.Forms.Label()
        Me.chkMovieScraperCredits = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockCredits = New System.Windows.Forms.CheckBox()
        Me.gbMovieScraperMiscOpts = New System.Windows.Forms.GroupBox()
        Me.chkMovieScraperStudioWithImg = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperReleaseFormat = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCollectionsAuto = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCleanFields = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperDetailView = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCleanPlotOutline = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperOutlinePlotEnglishOverwrite = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperPlotForOutline = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperOutlineForPlot = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCastWithImg = New System.Windows.Forms.CheckBox()
        Me.gbMovieScraperCertificationOpts = New System.Windows.Forms.GroupBox()
        Me.chkMovieScraperCertForMPAAFallback = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCertForMPAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCertFSK = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCertOnlyValue = New System.Windows.Forms.CheckBox()
        Me.pnlMovieScraper = New System.Windows.Forms.Panel()
        Me.tblMovieScraper = New System.Windows.Forms.TableLayoutPanel()
        Me.tsSettingsTopMenu = New System.Windows.Forms.ToolStrip()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.gbSettingsHelp = New System.Windows.Forms.GroupBox()
        Me.pbSettingsHelpLogo = New System.Windows.Forms.PictureBox()
        Me.lblHelp = New System.Windows.Forms.Label()
        Me.pnlSettingsHelp = New System.Windows.Forms.Panel()
        Me.pnlMovieTrailers = New System.Windows.Forms.Panel()
        Me.gbMovieTrailerOpts = New System.Windows.Forms.GroupBox()
        Me.lblMovieTrailerDefaultSearch = New System.Windows.Forms.Label()
        Me.txtMovieTrailerDefaultSearch = New System.Windows.Forms.TextBox()
        Me.cbMovieTrailerMinQual = New System.Windows.Forms.ComboBox()
        Me.lblMovieTrailerMinQual = New System.Windows.Forms.Label()
        Me.cbMovieTrailerPrefQual = New System.Windows.Forms.ComboBox()
        Me.lblMovieTrailerPrefQual = New System.Windows.Forms.Label()
        Me.chkMovieTrailerDeleteExisting = New System.Windows.Forms.CheckBox()
        Me.chkMovieTrailerOverwrite = New System.Windows.Forms.CheckBox()
        Me.chkMovieTrailerEnable = New System.Windows.Forms.CheckBox()
        Me.fileBrowse = New System.Windows.Forms.OpenFileDialog()
        Me.pnlMovieThemes = New System.Windows.Forms.Panel()
        Me.gbMovieThemeOpts = New System.Windows.Forms.GroupBox()
        Me.chkMovieThemeOverwrite = New System.Windows.Forms.CheckBox()
        Me.chkMovieThemeEnable = New System.Windows.Forms.CheckBox()
        Me.pnlTVThemes = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cdColor = New System.Windows.Forms.ColorDialog()
        Me.pnlMovieSetGeneral = New System.Windows.Forms.Panel()
        Me.gbMovieSetGeneralMissingItemsOpts = New System.Windows.Forms.GroupBox()
        Me.chkMovieSetMissingDiscArt = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetMissingClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetMissingClearArt = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetMissingLandscape = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetMissingBanner = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetMissingPoster = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetMissingNFO = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetMissingFanart = New System.Windows.Forms.CheckBox()
        Me.gbMovieSetGeneralMiscOpts = New System.Windows.Forms.GroupBox()
        Me.chkMovieSetClickScrape = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetClickScrapeAsk = New System.Windows.Forms.CheckBox()
        Me.gbMovieSetGeneralMediaListOpts = New System.Windows.Forms.GroupBox()
        Me.gbMovieSetSortTokensOpts = New System.Windows.Forms.GroupBox()
        Me.btnMovieSetSortTokenRemove = New System.Windows.Forms.Button()
        Me.btnMovieSetSortTokenAdd = New System.Windows.Forms.Button()
        Me.txtMovieSetSortToken = New System.Windows.Forms.TextBox()
        Me.lstMovieSetSortTokens = New System.Windows.Forms.ListBox()
        Me.chkMovieSetDiscArtCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetClearLogoCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetClearArtCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetBannerCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetLandscapeCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetPosterCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetNFOCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetFanartCol = New System.Windows.Forms.CheckBox()
        Me.pnlMovieSetSources = New System.Windows.Forms.Panel()
        Me.gbMovieSetSourceMiscOpts = New System.Windows.Forms.GroupBox()
        Me.chkMovieSetCleanFiles = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetCleanDB = New System.Windows.Forms.CheckBox()
        Me.gbMovieSetFileNaming = New System.Windows.Forms.GroupBox()
        Me.tcMovieSetFileNaming = New System.Windows.Forms.TabControl()
        Me.tpMovieSetFileNamingXBMC = New System.Windows.Forms.TabPage()
        Me.pbMSAAInfo = New System.Windows.Forms.PictureBox()
        Me.gbMovieSetMSAA = New System.Windows.Forms.GroupBox()
        Me.chkMovieSetUseMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetLandscapeMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetBannerMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetClearArtMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetClearLogoMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetFanartMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetPosterMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetNFOMSAA = New System.Windows.Forms.CheckBox()
        Me.tpMovieSetFileNamingExpert = New System.Windows.Forms.TabPage()
        Me.gbMovieSetExpert = New System.Windows.Forms.GroupBox()
        Me.tcMovieSetFileNamingExpert = New System.Windows.Forms.TabControl()
        Me.tpMovieSetFileNamingExpertSingle = New System.Windows.Forms.TabPage()
        Me.lblMovieSetPathExpertSingle = New System.Windows.Forms.Label()
        Me.btnMovieSetPathExpertSingleBrowse = New System.Windows.Forms.Button()
        Me.txtMovieSetPathExpertSingle = New System.Windows.Forms.TextBox()
        Me.lblMovieSetClearArtExpertSingle = New System.Windows.Forms.Label()
        Me.txtMovieSetPosterExpertSingle = New System.Windows.Forms.TextBox()
        Me.txtMovieSetFanartExpertSingle = New System.Windows.Forms.TextBox()
        Me.txtMovieSetBannerExpertSingle = New System.Windows.Forms.TextBox()
        Me.txtMovieSetClearLogoExpertSingle = New System.Windows.Forms.TextBox()
        Me.txtMovieSetClearArtExpertSingle = New System.Windows.Forms.TextBox()
        Me.txtMovieSetLandscapeExpertSingle = New System.Windows.Forms.TextBox()
        Me.txtMovieSetNFOExpertSingle = New System.Windows.Forms.TextBox()
        Me.lblMovieSetLandscapeExpertSingle = New System.Windows.Forms.Label()
        Me.lblMovieSetBannerExpertSingle = New System.Windows.Forms.Label()
        Me.lblMovieSetClearLogoExpertSingle = New System.Windows.Forms.Label()
        Me.lblMovieSetFanartExpertSingle = New System.Windows.Forms.Label()
        Me.lblMovieSetPosterExpertSingle = New System.Windows.Forms.Label()
        Me.lblMovieSetNFOExpertSingle = New System.Windows.Forms.Label()
        Me.tpMovieSetFileNamingExpertParent = New System.Windows.Forms.TabPage()
        Me.txtMovieSetPosterExpertParent = New System.Windows.Forms.TextBox()
        Me.txtMovieSetFanartExpertParent = New System.Windows.Forms.TextBox()
        Me.txtMovieSetBannerExpertParent = New System.Windows.Forms.TextBox()
        Me.txtMovieSetClearLogoExpertParent = New System.Windows.Forms.TextBox()
        Me.txtMovieSetClearArtExpertParent = New System.Windows.Forms.TextBox()
        Me.txtMovieSetLandscapeExpertParent = New System.Windows.Forms.TextBox()
        Me.txtMovieSetNFOExpertParent = New System.Windows.Forms.TextBox()
        Me.lblMovieSetClearArtExpertParent = New System.Windows.Forms.Label()
        Me.lblMovieSetLandscapeExpertParent = New System.Windows.Forms.Label()
        Me.lblMovieSetBannerExpertParent = New System.Windows.Forms.Label()
        Me.lblMovieSetClearLogoExpertParent = New System.Windows.Forms.Label()
        Me.lblMovieSetFanartExpertParent = New System.Windows.Forms.Label()
        Me.lblMovieSetPosterExpertParent = New System.Windows.Forms.Label()
        Me.lblMovieSetNFOExpertParent = New System.Windows.Forms.Label()
        Me.chkMovieSetUseExpert = New System.Windows.Forms.CheckBox()
        Me.pnlMovieSetScraper = New System.Windows.Forms.Panel()
        Me.tblMovieSetScraper = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMovieSetScraperTitleRenamerOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetScraperTitleRenamerOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvMovieSetScraperTitleRenamer = New System.Windows.Forms.DataGridView()
        Me.tbcMovieSetScrapedTitleRenamerFrom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbcMovieSetScrapedTitleRenamerTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnMovieSetScraperTitleRenamerAdd = New System.Windows.Forms.Button()
        Me.btnMovieSetScraperTitleRenamerRemove = New System.Windows.Forms.Button()
        Me.gbMovieSetScraperGlobalOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetScraperGlobalOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieSetLockPlot = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetScraperPlot = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetLockTitle = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetScraperTitle = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblMovieSetScraperTitle = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblMovieSetScraperPlot = New System.Windows.Forms.Label()
        Me.pnlMovieSetImages = New System.Windows.Forms.Panel()
        Me.tblMovieSetImages = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMovieSetImagesPosterOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetImagesPosterOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.txtMovieSetPosterHeight = New System.Windows.Forms.TextBox()
        Me.txtMovieSetPosterWidth = New System.Windows.Forms.TextBox()
        Me.lblMovieSetPosterHeight = New System.Windows.Forms.Label()
        Me.chkMovieSetPosterPrefOnly = New System.Windows.Forms.CheckBox()
        Me.lblMovieSetPosterSize = New System.Windows.Forms.Label()
        Me.lblMovieSetPosterWidth = New System.Windows.Forms.Label()
        Me.cbMovieSetPosterPrefSize = New System.Windows.Forms.ComboBox()
        Me.chkMovieSetPosterOverwrite = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetPosterResize = New System.Windows.Forms.CheckBox()
        Me.gbMovieSetImagesClearLogoOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetImagesClearLogoOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieSetClearLogoOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbMovieSetImagesClearArtOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetImagesClearArtOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieSetClearArtOverwrite = New System.Windows.Forms.CheckBox()
        Me.gbMovieSetImagesBannerOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetImagesBannerOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.txtMovieSetBannerHeight = New System.Windows.Forms.TextBox()
        Me.txtMovieSetBannerWidth = New System.Windows.Forms.TextBox()
        Me.lblMovieSetBannerHeight = New System.Windows.Forms.Label()
        Me.chkMovieSetBannerPrefOnly = New System.Windows.Forms.CheckBox()
        Me.lblMovieSetBannerType = New System.Windows.Forms.Label()
        Me.lblMovieSetBannerWidth = New System.Windows.Forms.Label()
        Me.cbMovieSetBannerPrefType = New System.Windows.Forms.ComboBox()
        Me.chkMovieSetBannerOverwrite = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetBannerResize = New System.Windows.Forms.CheckBox()
        Me.gbMovieSetImagesFanartOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetImagesFanartOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.txtMovieSetFanartHeight = New System.Windows.Forms.TextBox()
        Me.txtMovieSetFanartWidth = New System.Windows.Forms.TextBox()
        Me.lblMovieSetFanartHeight = New System.Windows.Forms.Label()
        Me.lblMovieSetFanartSize = New System.Windows.Forms.Label()
        Me.cbMovieSetFanartPrefSize = New System.Windows.Forms.ComboBox()
        Me.lblMovieSetFanartWidth = New System.Windows.Forms.Label()
        Me.chkMovieSetFanartPrefOnly = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetFanartOverwrite = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetFanartResize = New System.Windows.Forms.CheckBox()
        Me.gbMovieSetImagesLandscapeOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetImagesLandscapeOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieSetLandscapeOverwrite = New System.Windows.Forms.CheckBox()
        Me.scSettings = New System.Windows.Forms.SplitContainer()
        Me.tblSettingsCurrent = New System.Windows.Forms.TableLayoutPanel()
        Me.scSettingsBody = New System.Windows.Forms.SplitContainer()
        Me.scSettingsMain = New System.Windows.Forms.SplitContainer()
        Me.tblSettingsFooter = New System.Windows.Forms.TableLayoutPanel()
        Me.gbGeneralMisc.SuspendLayout
        Me.tblGeneralMisc.SuspendLayout
        Me.gbGeneralDaemon.SuspendLayout
        Me.tblGeneralDaemon.SuspendLayout
        Me.gbGeneralThemes.SuspendLayout
        Me.tblGeneralThemes.SuspendLayout
        Me.gbFileSystemCleanFiles.SuspendLayout
        Me.tcFileSystemCleaner.SuspendLayout
        Me.tpFileSystemCleanerStandard.SuspendLayout
        Me.tpFileSystemCleanerExpert.SuspendLayout
        Me.gbMovieGeneralMiscOpts.SuspendLayout
        Me.pnlMovieImages.SuspendLayout
        Me.tblMovieImages.SuspendLayout
        Me.gbMovieImagesOpts.SuspendLayout
        Me.tblMovieImagesOpts.SuspendLayout
        Me.gbMovieImagesLandscapeOpts.SuspendLayout
        Me.tblMovieImagesLandscapeOpts.SuspendLayout
        Me.gbMovieImagesDiscArtOpts.SuspendLayout
        Me.tblMovieImagesDiscArtOpts.SuspendLayout
        Me.gbMovieImagesClearLogoOpts.SuspendLayout
        Me.tblMovieImagesClearLogoOpts.SuspendLayout
        Me.gbMovieImagesClearArtOpts.SuspendLayout
        Me.tblMovieImagesClearArtOpts.SuspendLayout
        Me.gbMovieImagesActorThumbsOpts.SuspendLayout
        Me.tblMovieImagesActorThumbsOpts.SuspendLayout
        Me.gbMovieImagesPosterOpts.SuspendLayout
        Me.tblMovieImagesPosterOpts.SuspendLayout
        Me.gbMovieImagesBannerOpts.SuspendLayout
        Me.tblMovieImagesBannerOpts.SuspendLayout
        Me.gbMovieImagesEThumbsOpts.SuspendLayout
        Me.tblMovieImagesEThumbsOpts.SuspendLayout
        Me.gbMovieImagesEFanartsOpts.SuspendLayout
        Me.tblMovieImagesEFanartsOpts.SuspendLayout
        Me.gbMovieImagesFanartOpts.SuspendLayout
        Me.tblMovieImagesFanartOpts.SuspendLayout
        Me.gbMovieGeneralMediaListOpts.SuspendLayout
        Me.gbMovieSortTokensOpts.SuspendLayout
        Me.pnlSettingsTop.SuspendLayout
        CType(Me.pbSettingsTopLogo,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlGeneral.SuspendLayout
        Me.tblGeneral.SuspendLayout
        Me.gbGeneralInterface.SuspendLayout
        Me.tblGeneralInterface.SuspendLayout
        Me.gbGeneralDateAdded.SuspendLayout
        Me.tblGeneralDateAdded.SuspendLayout
        Me.gbGeneralMainWindow.SuspendLayout
        Me.tblGeneralMainWindow.SuspendLayout
        Me.gbGeneralScrapers.SuspendLayout
        Me.tblGeneralScrapers.SuspendLayout
        Me.pnlMovieGeneral.SuspendLayout
        Me.gbMovieGeneralCustomMarker.SuspendLayout
        Me.gbMovieGenrealIMDBMirrorOpts.SuspendLayout
        Me.gbMovieGeneralGenreFilterOpts.SuspendLayout
        Me.gbMovieGeneralFiltersOpts.SuspendLayout
        Me.gbMovieGeneralMissingItemsOpts.SuspendLayout
        Me.pnlFileSystem.SuspendLayout
        Me.gbFileSystemValidSubtitlesExts.SuspendLayout
        Me.gbFileSystemExcludedDirs.SuspendLayout
        Me.gbFileSystemValidThemeExts.SuspendLayout
        Me.gbFileSystemNoStackExts.SuspendLayout
        Me.gbFileSystemValidExts.SuspendLayout
        Me.pnlProxy.SuspendLayout
        Me.gbProxyOpts.SuspendLayout
        Me.gbProxyCredsOpts.SuspendLayout
        Me.gbMovieBackdropsFolder.SuspendLayout
        Me.pnlSettingsCurrent.SuspendLayout
        CType(Me.pbSettingsCurrent,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlMovieSources.SuspendLayout
        Me.gbMovieFileNaming.SuspendLayout
        Me.tcMovieFileNaming.SuspendLayout
        Me.tpMovieFileNamingXBMC.SuspendLayout
        Me.gbMovieXBMCTheme.SuspendLayout
        Me.gbMovieXBMCOptionalSettings.SuspendLayout
        Me.gbMovieEden.SuspendLayout
        Me.gbMovieFrodo.SuspendLayout
        Me.tpMovieFileNamingNMT.SuspendLayout
        Me.gbMovieNMTOptionalSettings.SuspendLayout
        Me.gbMovieNMJ.SuspendLayout
        Me.gbMovieYAMJ.SuspendLayout
        Me.tpMovieFileNamingBoxee.SuspendLayout
        Me.gbMovieBoxee.SuspendLayout
        Me.tpMovieFileNamingExpert.SuspendLayout
        Me.gbMovieExpert.SuspendLayout
        Me.tcMovieFileNamingExpert.SuspendLayout
        Me.tpMovieFileNamingExpertSingle.SuspendLayout
        Me.gbMovieExpertSingleOptionalSettings.SuspendLayout
        Me.gbMovieExpertSingleOptionalImages.SuspendLayout
        Me.tpMovieFileNamingExpertMulti.SuspendLayout
        Me.gbMovieExpertMultiOptionalImages.SuspendLayout
        Me.gbMovieExpertMultiOptionalSettings.SuspendLayout
        Me.tpMovieFileNamingExpertVTS.SuspendLayout
        Me.gbMovieExpertVTSOptionalSettings.SuspendLayout
        Me.gbMovieExpertVTSOptionalImages.SuspendLayout
        Me.tpMovieFileNamingExpertBDMV.SuspendLayout
        Me.gbMovieExpertBDMVOptionalSettings.SuspendLayout
        Me.gbMovieExpertBDMVOptionalImages.SuspendLayout
        Me.gbMovieSourcesMiscOpts.SuspendLayout
        Me.gbMovieSetMSAAPath.SuspendLayout
        Me.pnlTVGeneral.SuspendLayout
        Me.gbTVGeneralMissingItemsOpts.SuspendLayout
        Me.gbTVGeneralMissingShowOpts.SuspendLayout
        Me.gbTVSortTokensOpts.SuspendLayout
        Me.gbTVGeneralLangOpts.SuspendLayout
        Me.gbTVGeneralMediaListOpts.SuspendLayout
        Me.gbTVGeneralListEpisodeOpts.SuspendLayout
        Me.gbTVGeneralListSeasonOpts.SuspendLayout
        Me.gbTVGeneralListShowOpts.SuspendLayout
        Me.gbTVEpisodeFilterOpts.SuspendLayout
        Me.gbTVGeneralMiscOpts.SuspendLayout
        Me.gbTVShowFilterOpts.SuspendLayout
        Me.pnlTVSources.SuspendLayout
        Me.tcTVSources.SuspendLayout
        Me.tpTVSourcesGeneral.SuspendLayout
        Me.gbTVFileNaming.SuspendLayout
        Me.tcTVFileNaming.SuspendLayout
        Me.tpTVFileNamingXBMC.SuspendLayout
        Me.gbTVXBMCAdditional.SuspendLayout
        Me.gbTVFrodo.SuspendLayout
        Me.tpTVFileNamingNMT.SuspendLayout
        Me.gbTVNMT.SuspendLayout
        Me.gbTVYAMJ.SuspendLayout
        Me.tpTVFileNamingBoxee.SuspendLayout
        Me.GroupBox1.SuspendLayout
        Me.gbTVSourcesMiscOpts.SuspendLayout
        Me.tpTVSourcesRegex.SuspendLayout
        Me.gbTVShowRegex.SuspendLayout
        Me.pnlTVImages.SuspendLayout
        Me.tcTVImages.SuspendLayout
        Me.tpTVShow.SuspendLayout
        Me.gbTVShowEFanartsOpts.SuspendLayout
        Me.gbTVShowCharacterArtOpts.SuspendLayout
        Me.gbTVShowClearArtOpts.SuspendLayout
        Me.gbTVShowClearLogoOpts.SuspendLayout
        Me.gbTVShowLandscapeOpts.SuspendLayout
        Me.gbTVShowBannerOpts.SuspendLayout
        Me.gbTVShowPosterOpts.SuspendLayout
        Me.gbTVShowFanartOpts.SuspendLayout
        Me.tpTVAllSeasons.SuspendLayout
        Me.gbTVASLandscapeOpts.SuspendLayout
        Me.gbTVASFanartOpts.SuspendLayout
        Me.gbTVASBannerOpts.SuspendLayout
        Me.gbTVASPosterOpts.SuspendLayout
        Me.tpTVSeason.SuspendLayout
        Me.gbTVSeasonLandscapeOpts.SuspendLayout
        Me.gbTVSeasonBannerOpts.SuspendLayout
        Me.gbTVSeasonPosterOpts.SuspendLayout
        Me.gbTVSeasonFanartOpts.SuspendLayout
        Me.tpTVEpisode.SuspendLayout
        Me.gbTVEpisodePosterOpts.SuspendLayout
        Me.gbTVEpisodeFanartOpts.SuspendLayout
        Me.pnlTVScraper.SuspendLayout
        Me.gbTVScraperMiscOpts.SuspendLayout
        Me.gbTVScraperFieldsOpts.SuspendLayout
        Me.gbTVScraperFieldsShowOpts.SuspendLayout
        Me.gbTVScraperFieldsEpisodeOpts.SuspendLayout
        Me.gbTVScraperGlobalLocksOpts.SuspendLayout
        Me.gbTVScraperGlobalLocksEpisodeOpts.SuspendLayout
        Me.gbTVScraperGlobalLocksShowOpts.SuspendLayout
        Me.gbTVScraperMetaDataOpts.SuspendLayout
        Me.gbTVScraperDefFIExtOpts.SuspendLayout
        Me.gbTVScraperDurationOpts.SuspendLayout
        Me.gbTVScraperOptionsOpts.SuspendLayout
        Me.gbMovieScraperMetaDataOpts.SuspendLayout
        Me.gbMovieScraperDefFIExtOpts.SuspendLayout
        Me.gbMovieScraperDurationFormatOpts.SuspendLayout
        Me.gbMovieScraperGlobalOpts.SuspendLayout
        Me.tblMovieScraperGlobalOpts.SuspendLayout
        Me.gbMovieScraperMiscOpts.SuspendLayout
        Me.gbMovieScraperCertificationOpts.SuspendLayout
        Me.pnlMovieScraper.SuspendLayout
        Me.tblMovieScraper.SuspendLayout
        Me.gbSettingsHelp.SuspendLayout
        CType(Me.pbSettingsHelpLogo,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlSettingsHelp.SuspendLayout
        Me.pnlMovieTrailers.SuspendLayout
        Me.gbMovieTrailerOpts.SuspendLayout
        Me.pnlMovieThemes.SuspendLayout
        Me.gbMovieThemeOpts.SuspendLayout
        Me.pnlTVThemes.SuspendLayout
        Me.pnlMovieSetGeneral.SuspendLayout
        Me.gbMovieSetGeneralMissingItemsOpts.SuspendLayout
        Me.gbMovieSetGeneralMiscOpts.SuspendLayout
        Me.gbMovieSetGeneralMediaListOpts.SuspendLayout
        Me.gbMovieSetSortTokensOpts.SuspendLayout
        Me.pnlMovieSetSources.SuspendLayout
        Me.gbMovieSetSourceMiscOpts.SuspendLayout
        Me.gbMovieSetFileNaming.SuspendLayout
        Me.tcMovieSetFileNaming.SuspendLayout
        Me.tpMovieSetFileNamingXBMC.SuspendLayout
        CType(Me.pbMSAAInfo,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbMovieSetMSAA.SuspendLayout
        Me.tpMovieSetFileNamingExpert.SuspendLayout
        Me.gbMovieSetExpert.SuspendLayout
        Me.tcMovieSetFileNamingExpert.SuspendLayout
        Me.tpMovieSetFileNamingExpertSingle.SuspendLayout
        Me.tpMovieSetFileNamingExpertParent.SuspendLayout
        Me.pnlMovieSetScraper.SuspendLayout
        Me.tblMovieSetScraper.SuspendLayout
        Me.gbMovieSetScraperTitleRenamerOpts.SuspendLayout
        Me.tblMovieSetScraperTitleRenamerOpts.SuspendLayout
        CType(Me.dgvMovieSetScraperTitleRenamer,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbMovieSetScraperGlobalOpts.SuspendLayout
        Me.tblMovieSetScraperGlobalOpts.SuspendLayout
        Me.pnlMovieSetImages.SuspendLayout
        Me.tblMovieSetImages.SuspendLayout
        Me.gbMovieSetImagesPosterOpts.SuspendLayout
        Me.tblMovieSetImagesPosterOpts.SuspendLayout
        Me.gbMovieSetImagesClearLogoOpts.SuspendLayout
        Me.tblMovieSetImagesClearLogoOpts.SuspendLayout
        Me.gbMovieSetImagesClearArtOpts.SuspendLayout
        Me.tblMovieSetImagesClearArtOpts.SuspendLayout
        Me.gbMovieSetImagesBannerOpts.SuspendLayout
        Me.tblMovieSetImagesBannerOpts.SuspendLayout
        Me.gbMovieSetImagesFanartOpts.SuspendLayout
        Me.tblMovieSetImagesFanartOpts.SuspendLayout
        Me.gbMovieSetImagesLandscapeOpts.SuspendLayout
        Me.tblMovieSetImagesLandscapeOpts.SuspendLayout
        CType(Me.scSettings,System.ComponentModel.ISupportInitialize).BeginInit
        Me.scSettings.Panel1.SuspendLayout
        Me.scSettings.Panel2.SuspendLayout
        Me.scSettings.SuspendLayout
        Me.tblSettingsCurrent.SuspendLayout
        CType(Me.scSettingsBody,System.ComponentModel.ISupportInitialize).BeginInit
        Me.scSettingsBody.Panel1.SuspendLayout
        Me.scSettingsBody.Panel2.SuspendLayout
        Me.scSettingsBody.SuspendLayout
        CType(Me.scSettingsMain,System.ComponentModel.ISupportInitialize).BeginInit
        Me.scSettingsMain.Panel1.SuspendLayout
        Me.scSettingsMain.Panel2.SuspendLayout
        Me.scSettingsMain.SuspendLayout
        Me.tblSettingsFooter.SuspendLayout
        Me.SuspendLayout
        '
        'gbGeneralMisc
        '
        Me.gbGeneralMisc.AutoSize = true
        Me.gbGeneralMisc.Controls.Add(Me.tblGeneralMisc)
        Me.gbGeneralMisc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGeneralMisc.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbGeneralMisc.Location = New System.Drawing.Point(247, 292)
        Me.gbGeneralMisc.Name = "gbGeneralMisc"
        Me.tblGeneral.SetRowSpan(Me.gbGeneralMisc, 2)
        Me.gbGeneralMisc.Size = New System.Drawing.Size(359, 121)
        Me.gbGeneralMisc.TabIndex = 1
        Me.gbGeneralMisc.TabStop = false
        Me.gbGeneralMisc.Text = "Miscellaneous"
        '
        'tblGeneralMisc
        '
        Me.tblGeneralMisc.AutoSize = true
        Me.tblGeneralMisc.ColumnCount = 2
        Me.tblGeneralMisc.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMisc.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMisc.Controls.Add(Me.chkGeneralSourceFromFolder, 0, 3)
        Me.tblGeneralMisc.Controls.Add(Me.chkGeneralCheckUpdates, 0, 0)
        Me.tblGeneralMisc.Controls.Add(Me.lblGeneralOverwriteNfo, 0, 2)
        Me.tblGeneralMisc.Controls.Add(Me.chkGeneralOverwriteNfo, 0, 1)
        Me.tblGeneralMisc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralMisc.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralMisc.Name = "tblGeneralMisc"
        Me.tblGeneralMisc.RowCount = 5
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.Size = New System.Drawing.Size(353, 100)
        Me.tblGeneralMisc.TabIndex = 17
        '
        'chkGeneralSourceFromFolder
        '
        Me.chkGeneralSourceFromFolder.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralSourceFromFolder.AutoSize = true
        Me.chkGeneralSourceFromFolder.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkGeneralSourceFromFolder.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralSourceFromFolder.Location = New System.Drawing.Point(3, 69)
        Me.chkGeneralSourceFromFolder.Name = "chkGeneralSourceFromFolder"
        Me.chkGeneralSourceFromFolder.Size = New System.Drawing.Size(243, 17)
        Me.chkGeneralSourceFromFolder.TabIndex = 5
        Me.chkGeneralSourceFromFolder.Text = "Include Folder Name in Source Type Check"
        Me.chkGeneralSourceFromFolder.UseVisualStyleBackColor = true
        '
        'chkGeneralCheckUpdates
        '
        Me.chkGeneralCheckUpdates.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralCheckUpdates.AutoSize = true
        Me.chkGeneralCheckUpdates.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralCheckUpdates.Location = New System.Drawing.Point(3, 3)
        Me.chkGeneralCheckUpdates.Name = "chkGeneralCheckUpdates"
        Me.chkGeneralCheckUpdates.Size = New System.Drawing.Size(121, 17)
        Me.chkGeneralCheckUpdates.TabIndex = 0
        Me.chkGeneralCheckUpdates.Text = "Check for Updates"
        Me.chkGeneralCheckUpdates.UseVisualStyleBackColor = true
        '
        'lblGeneralOverwriteNfo
        '
        Me.lblGeneralOverwriteNfo.AutoSize = true
        Me.lblGeneralOverwriteNfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblGeneralOverwriteNfo.Location = New System.Drawing.Point(3, 46)
        Me.lblGeneralOverwriteNfo.Name = "lblGeneralOverwriteNfo"
        Me.lblGeneralOverwriteNfo.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.lblGeneralOverwriteNfo.Size = New System.Drawing.Size(314, 12)
        Me.lblGeneralOverwriteNfo.TabIndex = 3
        Me.lblGeneralOverwriteNfo.Text = "(If unchecked, non-conforming nfos will be renamed to <filename>.info)"
        Me.lblGeneralOverwriteNfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkGeneralOverwriteNfo
        '
        Me.chkGeneralOverwriteNfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralOverwriteNfo.AutoSize = true
        Me.chkGeneralOverwriteNfo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralOverwriteNfo.Location = New System.Drawing.Point(3, 26)
        Me.chkGeneralOverwriteNfo.Name = "chkGeneralOverwriteNfo"
        Me.chkGeneralOverwriteNfo.Size = New System.Drawing.Size(191, 17)
        Me.chkGeneralOverwriteNfo.TabIndex = 2
        Me.chkGeneralOverwriteNfo.Text = "Overwrite Non-conforming nfos"
        Me.chkGeneralOverwriteNfo.UseVisualStyleBackColor = true
        '
        'chkGeneralDateAddedIgnoreNFO
        '
        Me.chkGeneralDateAddedIgnoreNFO.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDateAddedIgnoreNFO.AutoSize = true
        Me.chkGeneralDateAddedIgnoreNFO.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralDateAddedIgnoreNFO.Location = New System.Drawing.Point(3, 30)
        Me.chkGeneralDateAddedIgnoreNFO.Name = "chkGeneralDateAddedIgnoreNFO"
        Me.chkGeneralDateAddedIgnoreNFO.Size = New System.Drawing.Size(188, 17)
        Me.chkGeneralDateAddedIgnoreNFO.TabIndex = 10
        Me.chkGeneralDateAddedIgnoreNFO.Text = "Ignore <dateadded> from NFO"
        Me.chkGeneralDateAddedIgnoreNFO.UseVisualStyleBackColor = true
        '
        'gbGeneralDaemon
        '
        Me.gbGeneralDaemon.AutoSize = true
        Me.tblGeneral.SetColumnSpan(Me.gbGeneralDaemon, 2)
        Me.gbGeneralDaemon.Controls.Add(Me.tblGeneralDaemon)
        Me.gbGeneralDaemon.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGeneralDaemon.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbGeneralDaemon.Location = New System.Drawing.Point(3, 419)
        Me.gbGeneralDaemon.Name = "gbGeneralDaemon"
        Me.gbGeneralDaemon.Size = New System.Drawing.Size(603, 70)
        Me.gbGeneralDaemon.TabIndex = 13
        Me.gbGeneralDaemon.TabStop = false
        Me.gbGeneralDaemon.Text = "Configuration ISO Filescanning"
        '
        'tblGeneralDaemon
        '
        Me.tblGeneralDaemon.AutoSize = true
        Me.tblGeneralDaemon.ColumnCount = 3
        Me.tblGeneralDaemon.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralDaemon.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralDaemon.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralDaemon.Controls.Add(Me.btnGeneralDaemonPathBrowse, 2, 1)
        Me.tblGeneralDaemon.Controls.Add(Me.lblGeneralDaemonDrive, 0, 0)
        Me.tblGeneralDaemon.Controls.Add(Me.txtGeneralDaemonPath, 1, 1)
        Me.tblGeneralDaemon.Controls.Add(Me.cbGeneralDaemonDrive, 0, 1)
        Me.tblGeneralDaemon.Controls.Add(Me.lblGeneralDaemonPath, 1, 0)
        Me.tblGeneralDaemon.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralDaemon.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralDaemon.Name = "tblGeneralDaemon"
        Me.tblGeneralDaemon.RowCount = 3
        Me.tblGeneralDaemon.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblGeneralDaemon.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralDaemon.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralDaemon.Size = New System.Drawing.Size(597, 49)
        Me.tblGeneralDaemon.TabIndex = 17
        '
        'lblGeneralDaemonPath
        '
        Me.lblGeneralDaemonPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralDaemonPath.AutoSize = true
        Me.tblGeneralDaemon.SetColumnSpan(Me.lblGeneralDaemonPath, 2)
        Me.lblGeneralDaemonPath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblGeneralDaemonPath.Location = New System.Drawing.Point(82, 3)
        Me.lblGeneralDaemonPath.Name = "lblGeneralDaemonPath"
        Me.lblGeneralDaemonPath.Size = New System.Drawing.Size(179, 13)
        Me.lblGeneralDaemonPath.TabIndex = 2
        Me.lblGeneralDaemonPath.Text = "Path to DTLite.exe/VCDMount.exe"
        '
        'btnGeneralDaemonPathBrowse
        '
        Me.btnGeneralDaemonPathBrowse.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnGeneralDaemonPathBrowse.Location = New System.Drawing.Point(568, 23)
        Me.btnGeneralDaemonPathBrowse.Name = "btnGeneralDaemonPathBrowse"
        Me.btnGeneralDaemonPathBrowse.Size = New System.Drawing.Size(25, 23)
        Me.btnGeneralDaemonPathBrowse.TabIndex = 4
        Me.btnGeneralDaemonPathBrowse.Text = "..."
        Me.btnGeneralDaemonPathBrowse.UseVisualStyleBackColor = true
        '
        'lblGeneralDaemonDrive
        '
        Me.lblGeneralDaemonDrive.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralDaemonDrive.AutoSize = true
        Me.lblGeneralDaemonDrive.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblGeneralDaemonDrive.Location = New System.Drawing.Point(3, 3)
        Me.lblGeneralDaemonDrive.Name = "lblGeneralDaemonDrive"
        Me.lblGeneralDaemonDrive.Size = New System.Drawing.Size(63, 13)
        Me.lblGeneralDaemonDrive.TabIndex = 6
        Me.lblGeneralDaemonDrive.Text = "Driveletter:"
        '
        'txtGeneralDaemonPath
        '
        Me.txtGeneralDaemonPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtGeneralDaemonPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGeneralDaemonPath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtGeneralDaemonPath.Location = New System.Drawing.Point(82, 23)
        Me.txtGeneralDaemonPath.Name = "txtGeneralDaemonPath"
        Me.txtGeneralDaemonPath.Size = New System.Drawing.Size(480, 22)
        Me.txtGeneralDaemonPath.TabIndex = 3
        '
        'cbGeneralDaemonDrive
        '
        Me.cbGeneralDaemonDrive.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbGeneralDaemonDrive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGeneralDaemonDrive.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbGeneralDaemonDrive.FormattingEnabled = true
        Me.cbGeneralDaemonDrive.Items.AddRange(New Object() {"", "A:\", "B:\", "D:\", "E:\", "F:\", "G:\", "H:\", "I:\", "J:\", "K:\", "L:\", "M:\", "N:\", "O:\", "P:\", "Q:\", "R:\", "S:\", "T:\", "U:\", "V:\", "W:\", "X:\", "Y:\", "Z:\"})
        Me.cbGeneralDaemonDrive.Location = New System.Drawing.Point(3, 24)
        Me.cbGeneralDaemonDrive.Name = "cbGeneralDaemonDrive"
        Me.cbGeneralDaemonDrive.Size = New System.Drawing.Size(73, 21)
        Me.cbGeneralDaemonDrive.TabIndex = 7
        '
        'chkGeneralImagesGlassOverlay
        '
        Me.chkGeneralImagesGlassOverlay.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralImagesGlassOverlay.AutoSize = true
        Me.chkGeneralImagesGlassOverlay.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralImagesGlassOverlay.Location = New System.Drawing.Point(179, 26)
        Me.chkGeneralImagesGlassOverlay.Name = "chkGeneralImagesGlassOverlay"
        Me.chkGeneralImagesGlassOverlay.Size = New System.Drawing.Size(171, 17)
        Me.chkGeneralImagesGlassOverlay.TabIndex = 12
        Me.chkGeneralImagesGlassOverlay.Text = "Enable Images Glass Overlay"
        Me.chkGeneralImagesGlassOverlay.UseVisualStyleBackColor = true
        '
        'chkGeneralHideFanartSmall
        '
        Me.chkGeneralHideFanartSmall.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralHideFanartSmall.AutoSize = true
        Me.chkGeneralHideFanartSmall.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralHideFanartSmall.Location = New System.Drawing.Point(179, 118)
        Me.chkGeneralHideFanartSmall.Name = "chkGeneralHideFanartSmall"
        Me.chkGeneralHideFanartSmall.Size = New System.Drawing.Size(169, 17)
        Me.chkGeneralHideFanartSmall.TabIndex = 11
        Me.chkGeneralHideFanartSmall.Text = "Do Not Display Small Fanart"
        Me.chkGeneralHideFanartSmall.UseVisualStyleBackColor = true
        '
        'chkGeneralShowGenresText
        '
        Me.chkGeneralShowGenresText.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralShowGenresText.AutoSize = true
        Me.chkGeneralShowGenresText.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralShowGenresText.Location = New System.Drawing.Point(3, 49)
        Me.chkGeneralShowGenresText.Name = "chkGeneralShowGenresText"
        Me.chkGeneralShowGenresText.Size = New System.Drawing.Size(166, 17)
        Me.chkGeneralShowGenresText.TabIndex = 9
        Me.chkGeneralShowGenresText.Text = "Allways Display Genres Text"
        Me.chkGeneralShowGenresText.UseVisualStyleBackColor = true
        '
        'chkGeneralHideFanart
        '
        Me.chkGeneralHideFanart.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralHideFanart.AutoSize = true
        Me.chkGeneralHideFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralHideFanart.Location = New System.Drawing.Point(179, 95)
        Me.chkGeneralHideFanart.Name = "chkGeneralHideFanart"
        Me.chkGeneralHideFanart.Size = New System.Drawing.Size(139, 17)
        Me.chkGeneralHideFanart.TabIndex = 7
        Me.chkGeneralHideFanart.Text = "Do Not Display Fanart"
        Me.chkGeneralHideFanart.UseVisualStyleBackColor = true
        '
        'chkGeneralInfoPanelAnim
        '
        Me.chkGeneralInfoPanelAnim.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralInfoPanelAnim.AutoSize = true
        Me.chkGeneralInfoPanelAnim.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralInfoPanelAnim.Location = New System.Drawing.Point(3, 26)
        Me.chkGeneralInfoPanelAnim.Name = "chkGeneralInfoPanelAnim"
        Me.chkGeneralInfoPanelAnim.Size = New System.Drawing.Size(148, 17)
        Me.chkGeneralInfoPanelAnim.TabIndex = 4
        Me.chkGeneralInfoPanelAnim.Text = "Enable Panel Animation"
        Me.chkGeneralInfoPanelAnim.UseVisualStyleBackColor = true
        '
        'chkGeneralHidePoster
        '
        Me.chkGeneralHidePoster.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralHidePoster.AutoSize = true
        Me.chkGeneralHidePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralHidePoster.Location = New System.Drawing.Point(179, 164)
        Me.chkGeneralHidePoster.Name = "chkGeneralHidePoster"
        Me.chkGeneralHidePoster.Size = New System.Drawing.Size(138, 17)
        Me.chkGeneralHidePoster.TabIndex = 6
        Me.chkGeneralHidePoster.Text = "Do Not Display Poster"
        Me.chkGeneralHidePoster.UseVisualStyleBackColor = true
        '
        'chkGeneralShowImgDims
        '
        Me.chkGeneralShowImgDims.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralShowImgDims.AutoSize = true
        Me.chkGeneralShowImgDims.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralShowImgDims.Location = New System.Drawing.Point(3, 72)
        Me.chkGeneralShowImgDims.Name = "chkGeneralShowImgDims"
        Me.chkGeneralShowImgDims.Size = New System.Drawing.Size(160, 17)
        Me.chkGeneralShowImgDims.TabIndex = 8
        Me.chkGeneralShowImgDims.Text = "Display Image Dimensions"
        Me.chkGeneralShowImgDims.UseVisualStyleBackColor = true
        '
        'gbGeneralThemes
        '
        Me.gbGeneralThemes.AutoSize = true
        Me.gbGeneralThemes.Controls.Add(Me.tblGeneralThemes)
        Me.gbGeneralThemes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGeneralThemes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbGeneralThemes.Location = New System.Drawing.Point(3, 50)
        Me.gbGeneralThemes.Name = "gbGeneralThemes"
        Me.gbGeneralThemes.Size = New System.Drawing.Size(220, 209)
        Me.gbGeneralThemes.TabIndex = 2
        Me.gbGeneralThemes.TabStop = false
        Me.gbGeneralThemes.Text = "Themes"
        '
        'tblGeneralThemes
        '
        Me.tblGeneralThemes.AutoSize = true
        Me.tblGeneralThemes.ColumnCount = 2
        Me.tblGeneralThemes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralThemes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralThemes.Controls.Add(Me.cbGeneralTVEpisodeTheme, 0, 7)
        Me.tblGeneralThemes.Controls.Add(Me.cbGeneralMovieSetTheme, 0, 3)
        Me.tblGeneralThemes.Controls.Add(Me.lblGeneralTVEpisodeTheme, 0, 6)
        Me.tblGeneralThemes.Controls.Add(Me.lblGeneralMovieTheme, 0, 0)
        Me.tblGeneralThemes.Controls.Add(Me.cbGeneralTVShowTheme, 0, 5)
        Me.tblGeneralThemes.Controls.Add(Me.lblGeneralMovieSetTheme, 0, 2)
        Me.tblGeneralThemes.Controls.Add(Me.lblGeneralTVShowTheme, 0, 4)
        Me.tblGeneralThemes.Controls.Add(Me.cbGeneralMovieTheme, 0, 1)
        Me.tblGeneralThemes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralThemes.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralThemes.Name = "tblGeneralThemes"
        Me.tblGeneralThemes.RowCount = 9
        Me.tblGeneralThemes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblGeneralThemes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralThemes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblGeneralThemes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralThemes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblGeneralThemes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralThemes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblGeneralThemes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralThemes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralThemes.Size = New System.Drawing.Size(214, 188)
        Me.tblGeneralThemes.TabIndex = 17
        '
        'cbGeneralTVEpisodeTheme
        '
        Me.cbGeneralTVEpisodeTheme.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbGeneralTVEpisodeTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGeneralTVEpisodeTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbGeneralTVEpisodeTheme.FormattingEnabled = true
        Me.cbGeneralTVEpisodeTheme.Location = New System.Drawing.Point(3, 164)
        Me.cbGeneralTVEpisodeTheme.Name = "cbGeneralTVEpisodeTheme"
        Me.cbGeneralTVEpisodeTheme.Size = New System.Drawing.Size(208, 21)
        Me.cbGeneralTVEpisodeTheme.TabIndex = 5
        '
        'cbGeneralMovieSetTheme
        '
        Me.cbGeneralMovieSetTheme.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbGeneralMovieSetTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGeneralMovieSetTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbGeneralMovieSetTheme.FormattingEnabled = true
        Me.cbGeneralMovieSetTheme.Location = New System.Drawing.Point(3, 70)
        Me.cbGeneralMovieSetTheme.Name = "cbGeneralMovieSetTheme"
        Me.cbGeneralMovieSetTheme.Size = New System.Drawing.Size(208, 21)
        Me.cbGeneralMovieSetTheme.TabIndex = 7
        '
        'lblGeneralTVEpisodeTheme
        '
        Me.lblGeneralTVEpisodeTheme.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralTVEpisodeTheme.AutoSize = true
        Me.lblGeneralTVEpisodeTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblGeneralTVEpisodeTheme.Location = New System.Drawing.Point(3, 144)
        Me.lblGeneralTVEpisodeTheme.Name = "lblGeneralTVEpisodeTheme"
        Me.lblGeneralTVEpisodeTheme.Size = New System.Drawing.Size(87, 13)
        Me.lblGeneralTVEpisodeTheme.TabIndex = 4
        Me.lblGeneralTVEpisodeTheme.Text = "Episode Theme:"
        '
        'lblGeneralMovieTheme
        '
        Me.lblGeneralMovieTheme.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralMovieTheme.AutoSize = true
        Me.lblGeneralMovieTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblGeneralMovieTheme.Location = New System.Drawing.Point(3, 3)
        Me.lblGeneralMovieTheme.Name = "lblGeneralMovieTheme"
        Me.lblGeneralMovieTheme.Size = New System.Drawing.Size(77, 13)
        Me.lblGeneralMovieTheme.TabIndex = 0
        Me.lblGeneralMovieTheme.Text = "Movie Theme:"
        '
        'cbGeneralTVShowTheme
        '
        Me.cbGeneralTVShowTheme.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbGeneralTVShowTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGeneralTVShowTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbGeneralTVShowTheme.FormattingEnabled = true
        Me.cbGeneralTVShowTheme.Location = New System.Drawing.Point(3, 117)
        Me.cbGeneralTVShowTheme.Name = "cbGeneralTVShowTheme"
        Me.cbGeneralTVShowTheme.Size = New System.Drawing.Size(208, 21)
        Me.cbGeneralTVShowTheme.TabIndex = 3
        '
        'lblGeneralMovieSetTheme
        '
        Me.lblGeneralMovieSetTheme.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralMovieSetTheme.AutoSize = true
        Me.lblGeneralMovieSetTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblGeneralMovieSetTheme.Location = New System.Drawing.Point(3, 50)
        Me.lblGeneralMovieSetTheme.Name = "lblGeneralMovieSetTheme"
        Me.lblGeneralMovieSetTheme.Size = New System.Drawing.Size(93, 13)
        Me.lblGeneralMovieSetTheme.TabIndex = 6
        Me.lblGeneralMovieSetTheme.Text = "MovieSet Theme:"
        '
        'lblGeneralTVShowTheme
        '
        Me.lblGeneralTVShowTheme.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralTVShowTheme.AutoSize = true
        Me.lblGeneralTVShowTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblGeneralTVShowTheme.Location = New System.Drawing.Point(3, 97)
        Me.lblGeneralTVShowTheme.Name = "lblGeneralTVShowTheme"
        Me.lblGeneralTVShowTheme.Size = New System.Drawing.Size(90, 13)
        Me.lblGeneralTVShowTheme.TabIndex = 2
        Me.lblGeneralTVShowTheme.Text = "TV Show Theme:"
        '
        'cbGeneralMovieTheme
        '
        Me.cbGeneralMovieTheme.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbGeneralMovieTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGeneralMovieTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbGeneralMovieTheme.FormattingEnabled = true
        Me.cbGeneralMovieTheme.Location = New System.Drawing.Point(3, 23)
        Me.cbGeneralMovieTheme.Name = "cbGeneralMovieTheme"
        Me.cbGeneralMovieTheme.Size = New System.Drawing.Size(208, 21)
        Me.cbGeneralMovieTheme.TabIndex = 1
        '
        'lblGeneralntLang
        '
        Me.lblGeneralntLang.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralntLang.AutoSize = true
        Me.lblGeneralntLang.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblGeneralntLang.Location = New System.Drawing.Point(3, 3)
        Me.lblGeneralntLang.Name = "lblGeneralntLang"
        Me.lblGeneralntLang.Size = New System.Drawing.Size(109, 13)
        Me.lblGeneralntLang.TabIndex = 0
        Me.lblGeneralntLang.Text = "Interface Language:"
        '
        'cbGeneralLanguage
        '
        Me.cbGeneralLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbGeneralLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGeneralLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbGeneralLanguage.FormattingEnabled = true
        Me.cbGeneralLanguage.Location = New System.Drawing.Point(3, 23)
        Me.cbGeneralLanguage.Name = "cbGeneralLanguage"
        Me.cbGeneralLanguage.Size = New System.Drawing.Size(216, 21)
        Me.cbGeneralLanguage.TabIndex = 1
        '
        'gbFileSystemCleanFiles
        '
        Me.gbFileSystemCleanFiles.Controls.Add(Me.tcFileSystemCleaner)
        Me.gbFileSystemCleanFiles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbFileSystemCleanFiles.Location = New System.Drawing.Point(401, 6)
        Me.gbFileSystemCleanFiles.Name = "gbFileSystemCleanFiles"
        Me.gbFileSystemCleanFiles.Size = New System.Drawing.Size(208, 351)
        Me.gbFileSystemCleanFiles.TabIndex = 2
        Me.gbFileSystemCleanFiles.TabStop = false
        Me.gbFileSystemCleanFiles.Text = "Clean Files"
        '
        'tcFileSystemCleaner
        '
        Me.tcFileSystemCleaner.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tcFileSystemCleaner.Controls.Add(Me.tpFileSystemCleanerStandard)
        Me.tcFileSystemCleaner.Controls.Add(Me.tpFileSystemCleanerExpert)
        Me.tcFileSystemCleaner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.tcFileSystemCleaner.Location = New System.Drawing.Point(6, 19)
        Me.tcFileSystemCleaner.Name = "tcFileSystemCleaner"
        Me.tcFileSystemCleaner.SelectedIndex = 0
        Me.tcFileSystemCleaner.Size = New System.Drawing.Size(196, 326)
        Me.tcFileSystemCleaner.TabIndex = 0
        '
        'tpFileSystemCleanerStandard
        '
        Me.tpFileSystemCleanerStandard.BackColor = System.Drawing.Color.White
        Me.tpFileSystemCleanerStandard.Controls.Add(Me.chkCleanFolderJPG)
        Me.tpFileSystemCleanerStandard.Controls.Add(Me.chkCleanExtrathumbs)
        Me.tpFileSystemCleanerStandard.Controls.Add(Me.chkCleanMovieTBN)
        Me.tpFileSystemCleanerStandard.Controls.Add(Me.chkCleanMovieNameJPG)
        Me.tpFileSystemCleanerStandard.Controls.Add(Me.chkCleanMovieTBNb)
        Me.tpFileSystemCleanerStandard.Controls.Add(Me.chkCleanMovieJPG)
        Me.tpFileSystemCleanerStandard.Controls.Add(Me.chkCleanFanartJPG)
        Me.tpFileSystemCleanerStandard.Controls.Add(Me.chkCleanPosterJPG)
        Me.tpFileSystemCleanerStandard.Controls.Add(Me.chkCleanMovieFanartJPG)
        Me.tpFileSystemCleanerStandard.Controls.Add(Me.chkCleanPosterTBN)
        Me.tpFileSystemCleanerStandard.Controls.Add(Me.chkCleanMovieNFO)
        Me.tpFileSystemCleanerStandard.Controls.Add(Me.chkCleanDotFanartJPG)
        Me.tpFileSystemCleanerStandard.Controls.Add(Me.chkCleanMovieNFOb)
        Me.tpFileSystemCleanerStandard.Location = New System.Drawing.Point(4, 25)
        Me.tpFileSystemCleanerStandard.Name = "tpFileSystemCleanerStandard"
        Me.tpFileSystemCleanerStandard.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFileSystemCleanerStandard.Size = New System.Drawing.Size(188, 297)
        Me.tpFileSystemCleanerStandard.TabIndex = 0
        Me.tpFileSystemCleanerStandard.Text = "Standard"
        Me.tpFileSystemCleanerStandard.UseVisualStyleBackColor = true
        '
        'chkCleanFolderJPG
        '
        Me.chkCleanFolderJPG.AutoSize = true
        Me.chkCleanFolderJPG.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCleanFolderJPG.Location = New System.Drawing.Point(7, 10)
        Me.chkCleanFolderJPG.Name = "chkCleanFolderJPG"
        Me.chkCleanFolderJPG.Size = New System.Drawing.Size(81, 17)
        Me.chkCleanFolderJPG.TabIndex = 0
        Me.chkCleanFolderJPG.Text = "/folder.jpg"
        Me.chkCleanFolderJPG.UseVisualStyleBackColor = true
        '
        'chkCleanExtrathumbs
        '
        Me.chkCleanExtrathumbs.AutoSize = true
        Me.chkCleanExtrathumbs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCleanExtrathumbs.Location = New System.Drawing.Point(7, 238)
        Me.chkCleanExtrathumbs.Name = "chkCleanExtrathumbs"
        Me.chkCleanExtrathumbs.Size = New System.Drawing.Size(98, 17)
        Me.chkCleanExtrathumbs.TabIndex = 12
        Me.chkCleanExtrathumbs.Text = "/extrathumbs/"
        Me.chkCleanExtrathumbs.UseVisualStyleBackColor = true
        '
        'chkCleanMovieTBN
        '
        Me.chkCleanMovieTBN.AutoSize = true
        Me.chkCleanMovieTBN.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCleanMovieTBN.Location = New System.Drawing.Point(7, 29)
        Me.chkCleanMovieTBN.Name = "chkCleanMovieTBN"
        Me.chkCleanMovieTBN.Size = New System.Drawing.Size(81, 17)
        Me.chkCleanMovieTBN.TabIndex = 1
        Me.chkCleanMovieTBN.Text = "/movie.tbn"
        Me.chkCleanMovieTBN.UseVisualStyleBackColor = true
        '
        'chkCleanMovieNameJPG
        '
        Me.chkCleanMovieNameJPG.AutoSize = true
        Me.chkCleanMovieNameJPG.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCleanMovieNameJPG.Location = New System.Drawing.Point(7, 124)
        Me.chkCleanMovieNameJPG.Name = "chkCleanMovieNameJPG"
        Me.chkCleanMovieNameJPG.Size = New System.Drawing.Size(96, 17)
        Me.chkCleanMovieNameJPG.TabIndex = 6
        Me.chkCleanMovieNameJPG.Text = "/<movie>.jpg"
        Me.chkCleanMovieNameJPG.UseVisualStyleBackColor = true
        '
        'chkCleanMovieTBNb
        '
        Me.chkCleanMovieTBNb.AutoSize = true
        Me.chkCleanMovieTBNb.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCleanMovieTBNb.Location = New System.Drawing.Point(7, 48)
        Me.chkCleanMovieTBNb.Name = "chkCleanMovieTBNb"
        Me.chkCleanMovieTBNb.Size = New System.Drawing.Size(97, 17)
        Me.chkCleanMovieTBNb.TabIndex = 2
        Me.chkCleanMovieTBNb.Text = "/<movie>.tbn"
        Me.chkCleanMovieTBNb.UseVisualStyleBackColor = true
        '
        'chkCleanMovieJPG
        '
        Me.chkCleanMovieJPG.AutoSize = true
        Me.chkCleanMovieJPG.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCleanMovieJPG.Location = New System.Drawing.Point(7, 105)
        Me.chkCleanMovieJPG.Name = "chkCleanMovieJPG"
        Me.chkCleanMovieJPG.Size = New System.Drawing.Size(80, 17)
        Me.chkCleanMovieJPG.TabIndex = 5
        Me.chkCleanMovieJPG.Text = "/movie.jpg"
        Me.chkCleanMovieJPG.UseVisualStyleBackColor = true
        '
        'chkCleanFanartJPG
        '
        Me.chkCleanFanartJPG.AutoSize = true
        Me.chkCleanFanartJPG.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCleanFanartJPG.Location = New System.Drawing.Point(7, 143)
        Me.chkCleanFanartJPG.Name = "chkCleanFanartJPG"
        Me.chkCleanFanartJPG.Size = New System.Drawing.Size(81, 17)
        Me.chkCleanFanartJPG.TabIndex = 7
        Me.chkCleanFanartJPG.Text = "/fanart.jpg"
        Me.chkCleanFanartJPG.UseVisualStyleBackColor = true
        '
        'chkCleanPosterJPG
        '
        Me.chkCleanPosterJPG.AutoSize = true
        Me.chkCleanPosterJPG.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCleanPosterJPG.Location = New System.Drawing.Point(7, 86)
        Me.chkCleanPosterJPG.Name = "chkCleanPosterJPG"
        Me.chkCleanPosterJPG.Size = New System.Drawing.Size(83, 17)
        Me.chkCleanPosterJPG.TabIndex = 4
        Me.chkCleanPosterJPG.Text = "/poster.jpg"
        Me.chkCleanPosterJPG.UseVisualStyleBackColor = true
        '
        'chkCleanMovieFanartJPG
        '
        Me.chkCleanMovieFanartJPG.AutoSize = true
        Me.chkCleanMovieFanartJPG.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCleanMovieFanartJPG.Location = New System.Drawing.Point(7, 162)
        Me.chkCleanMovieFanartJPG.Name = "chkCleanMovieFanartJPG"
        Me.chkCleanMovieFanartJPG.Size = New System.Drawing.Size(131, 17)
        Me.chkCleanMovieFanartJPG.TabIndex = 8
        Me.chkCleanMovieFanartJPG.Text = "/<movie>-fanart.jpg"
        Me.chkCleanMovieFanartJPG.UseVisualStyleBackColor = true
        '
        'chkCleanPosterTBN
        '
        Me.chkCleanPosterTBN.AutoSize = true
        Me.chkCleanPosterTBN.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCleanPosterTBN.Location = New System.Drawing.Point(7, 67)
        Me.chkCleanPosterTBN.Name = "chkCleanPosterTBN"
        Me.chkCleanPosterTBN.Size = New System.Drawing.Size(84, 17)
        Me.chkCleanPosterTBN.TabIndex = 3
        Me.chkCleanPosterTBN.Text = "/poster.tbn"
        Me.chkCleanPosterTBN.UseVisualStyleBackColor = true
        '
        'chkCleanMovieNFO
        '
        Me.chkCleanMovieNFO.AutoSize = true
        Me.chkCleanMovieNFO.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCleanMovieNFO.Location = New System.Drawing.Point(7, 200)
        Me.chkCleanMovieNFO.Name = "chkCleanMovieNFO"
        Me.chkCleanMovieNFO.Size = New System.Drawing.Size(81, 17)
        Me.chkCleanMovieNFO.TabIndex = 10
        Me.chkCleanMovieNFO.Text = "/movie.nfo"
        Me.chkCleanMovieNFO.UseVisualStyleBackColor = true
        '
        'chkCleanDotFanartJPG
        '
        Me.chkCleanDotFanartJPG.AutoSize = true
        Me.chkCleanDotFanartJPG.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCleanDotFanartJPG.Location = New System.Drawing.Point(7, 181)
        Me.chkCleanDotFanartJPG.Name = "chkCleanDotFanartJPG"
        Me.chkCleanDotFanartJPG.Size = New System.Drawing.Size(130, 17)
        Me.chkCleanDotFanartJPG.TabIndex = 9
        Me.chkCleanDotFanartJPG.Text = "/<movie>.fanart.jpg"
        Me.chkCleanDotFanartJPG.UseVisualStyleBackColor = true
        '
        'chkCleanMovieNFOb
        '
        Me.chkCleanMovieNFOb.AutoSize = true
        Me.chkCleanMovieNFOb.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCleanMovieNFOb.Location = New System.Drawing.Point(7, 219)
        Me.chkCleanMovieNFOb.Name = "chkCleanMovieNFOb"
        Me.chkCleanMovieNFOb.Size = New System.Drawing.Size(97, 17)
        Me.chkCleanMovieNFOb.TabIndex = 11
        Me.chkCleanMovieNFOb.Text = "/<movie>.nfo"
        Me.chkCleanMovieNFOb.UseVisualStyleBackColor = true
        '
        'tpFileSystemCleanerExpert
        '
        Me.tpFileSystemCleanerExpert.BackColor = System.Drawing.Color.White
        Me.tpFileSystemCleanerExpert.Controls.Add(Me.chkFileSystemCleanerWhitelist)
        Me.tpFileSystemCleanerExpert.Controls.Add(Me.lblFileSystemCleanerWhitelist)
        Me.tpFileSystemCleanerExpert.Controls.Add(Me.btnFileSystemCleanerWhitelistRemove)
        Me.tpFileSystemCleanerExpert.Controls.Add(Me.btnFileSystemCleanerWhitelistAdd)
        Me.tpFileSystemCleanerExpert.Controls.Add(Me.txtFileSystemCleanerWhitelist)
        Me.tpFileSystemCleanerExpert.Controls.Add(Me.lstFileSystemCleanerWhitelist)
        Me.tpFileSystemCleanerExpert.Controls.Add(Me.lblFileSystemCleanerWarning)
        Me.tpFileSystemCleanerExpert.Location = New System.Drawing.Point(4, 25)
        Me.tpFileSystemCleanerExpert.Name = "tpFileSystemCleanerExpert"
        Me.tpFileSystemCleanerExpert.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFileSystemCleanerExpert.Size = New System.Drawing.Size(188, 297)
        Me.tpFileSystemCleanerExpert.TabIndex = 1
        Me.tpFileSystemCleanerExpert.Text = "Expert"
        Me.tpFileSystemCleanerExpert.UseVisualStyleBackColor = true
        '
        'chkFileSystemCleanerWhitelist
        '
        Me.chkFileSystemCleanerWhitelist.AutoSize = true
        Me.chkFileSystemCleanerWhitelist.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkFileSystemCleanerWhitelist.Location = New System.Drawing.Point(4, 85)
        Me.chkFileSystemCleanerWhitelist.Name = "chkFileSystemCleanerWhitelist"
        Me.chkFileSystemCleanerWhitelist.Size = New System.Drawing.Size(163, 17)
        Me.chkFileSystemCleanerWhitelist.TabIndex = 1
        Me.chkFileSystemCleanerWhitelist.Text = "Whitelist Video Extensions"
        Me.chkFileSystemCleanerWhitelist.UseVisualStyleBackColor = true
        '
        'lblFileSystemCleanerWhitelist
        '
        Me.lblFileSystemCleanerWhitelist.AutoSize = true
        Me.lblFileSystemCleanerWhitelist.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFileSystemCleanerWhitelist.Location = New System.Drawing.Point(19, 107)
        Me.lblFileSystemCleanerWhitelist.Name = "lblFileSystemCleanerWhitelist"
        Me.lblFileSystemCleanerWhitelist.Size = New System.Drawing.Size(127, 13)
        Me.lblFileSystemCleanerWhitelist.TabIndex = 2
        Me.lblFileSystemCleanerWhitelist.Text = "Whitelisted Extensions:"
        '
        'btnFileSystemCleanerWhitelistRemove
        '
        Me.btnFileSystemCleanerWhitelistRemove.Image = CType(resources.GetObject("btnFileSystemCleanerWhitelistRemove.Image"),System.Drawing.Image)
        Me.btnFileSystemCleanerWhitelistRemove.Location = New System.Drawing.Point(134, 251)
        Me.btnFileSystemCleanerWhitelistRemove.Name = "btnFileSystemCleanerWhitelistRemove"
        Me.btnFileSystemCleanerWhitelistRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemCleanerWhitelistRemove.TabIndex = 6
        Me.btnFileSystemCleanerWhitelistRemove.UseVisualStyleBackColor = true
        '
        'btnFileSystemCleanerWhitelistAdd
        '
        Me.btnFileSystemCleanerWhitelistAdd.Image = CType(resources.GetObject("btnFileSystemCleanerWhitelistAdd.Image"),System.Drawing.Image)
        Me.btnFileSystemCleanerWhitelistAdd.Location = New System.Drawing.Point(82, 251)
        Me.btnFileSystemCleanerWhitelistAdd.Name = "btnFileSystemCleanerWhitelistAdd"
        Me.btnFileSystemCleanerWhitelistAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemCleanerWhitelistAdd.TabIndex = 5
        Me.btnFileSystemCleanerWhitelistAdd.UseVisualStyleBackColor = true
        '
        'txtFileSystemCleanerWhitelist
        '
        Me.txtFileSystemCleanerWhitelist.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFileSystemCleanerWhitelist.Location = New System.Drawing.Point(20, 252)
        Me.txtFileSystemCleanerWhitelist.Name = "txtFileSystemCleanerWhitelist"
        Me.txtFileSystemCleanerWhitelist.Size = New System.Drawing.Size(61, 22)
        Me.txtFileSystemCleanerWhitelist.TabIndex = 4
        '
        'lstFileSystemCleanerWhitelist
        '
        Me.lstFileSystemCleanerWhitelist.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstFileSystemCleanerWhitelist.FormattingEnabled = true
        Me.lstFileSystemCleanerWhitelist.Location = New System.Drawing.Point(19, 126)
        Me.lstFileSystemCleanerWhitelist.Name = "lstFileSystemCleanerWhitelist"
        Me.lstFileSystemCleanerWhitelist.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFileSystemCleanerWhitelist.Size = New System.Drawing.Size(138, 108)
        Me.lstFileSystemCleanerWhitelist.TabIndex = 3
        '
        'lblFileSystemCleanerWarning
        '
        Me.lblFileSystemCleanerWarning.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblFileSystemCleanerWarning.ForeColor = System.Drawing.Color.Red
        Me.lblFileSystemCleanerWarning.Location = New System.Drawing.Point(12, 10)
        Me.lblFileSystemCleanerWarning.Name = "lblFileSystemCleanerWarning"
        Me.lblFileSystemCleanerWarning.Size = New System.Drawing.Size(155, 68)
        Me.lblFileSystemCleanerWarning.TabIndex = 0
        Me.lblFileSystemCleanerWarning.Text = "WARNING: Using the Expert Mode Cleaner could potentially delete wanted files. Tak"& _ 
    "e care when using this tool."
        Me.lblFileSystemCleanerWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gbMovieGeneralMiscOpts
        '
        Me.gbMovieGeneralMiscOpts.Controls.Add(Me.chkMovieClickScrape)
        Me.gbMovieGeneralMiscOpts.Controls.Add(Me.chkMovieClickScrapeAsk)
        Me.gbMovieGeneralMiscOpts.Controls.Add(Me.chkMovieGeneralMarkNew)
        Me.gbMovieGeneralMiscOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieGeneralMiscOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbMovieGeneralMiscOpts.Name = "gbMovieGeneralMiscOpts"
        Me.gbMovieGeneralMiscOpts.Size = New System.Drawing.Size(219, 71)
        Me.gbMovieGeneralMiscOpts.TabIndex = 1
        Me.gbMovieGeneralMiscOpts.TabStop = false
        Me.gbMovieGeneralMiscOpts.Text = "Miscellaneous"
        '
        'chkMovieClickScrape
        '
        Me.chkMovieClickScrape.AutoSize = true
        Me.chkMovieClickScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkMovieClickScrape.Location = New System.Drawing.Point(12, 32)
        Me.chkMovieClickScrape.Name = "chkMovieClickScrape"
        Me.chkMovieClickScrape.Size = New System.Drawing.Size(125, 17)
        Me.chkMovieClickScrape.TabIndex = 65
        Me.chkMovieClickScrape.Text = "Enable Click Scrape"
        Me.chkMovieClickScrape.UseVisualStyleBackColor = true
        '
        'chkMovieClickScrapeAsk
        '
        Me.chkMovieClickScrapeAsk.AutoSize = true
        Me.chkMovieClickScrapeAsk.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkMovieClickScrapeAsk.Location = New System.Drawing.Point(21, 48)
        Me.chkMovieClickScrapeAsk.Name = "chkMovieClickScrapeAsk"
        Me.chkMovieClickScrapeAsk.Size = New System.Drawing.Size(127, 17)
        Me.chkMovieClickScrapeAsk.TabIndex = 64
        Me.chkMovieClickScrapeAsk.Text = "Ask On Click Scrape"
        Me.chkMovieClickScrapeAsk.UseVisualStyleBackColor = true
        '
        'chkMovieGeneralMarkNew
        '
        Me.chkMovieGeneralMarkNew.AutoSize = true
        Me.chkMovieGeneralMarkNew.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieGeneralMarkNew.Location = New System.Drawing.Point(12, 16)
        Me.chkMovieGeneralMarkNew.Name = "chkMovieGeneralMarkNew"
        Me.chkMovieGeneralMarkNew.Size = New System.Drawing.Size(117, 17)
        Me.chkMovieGeneralMarkNew.TabIndex = 0
        Me.chkMovieGeneralMarkNew.Text = "Mark New Movies"
        Me.chkMovieGeneralMarkNew.UseVisualStyleBackColor = true
        '
        'pnlMovieImages
        '
        Me.pnlMovieImages.AutoSize = true
        Me.pnlMovieImages.BackColor = System.Drawing.Color.White
        Me.pnlMovieImages.Controls.Add(Me.tblMovieImages)
        Me.pnlMovieImages.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.pnlMovieImages.Location = New System.Drawing.Point(900, 900)
        Me.pnlMovieImages.Name = "pnlMovieImages"
        Me.pnlMovieImages.Size = New System.Drawing.Size(982, 600)
        Me.pnlMovieImages.TabIndex = 12
        Me.pnlMovieImages.Visible = false
        '
        'tblMovieImages
        '
        Me.tblMovieImages.AutoScroll = true
        Me.tblMovieImages.AutoSize = true
        Me.tblMovieImages.ColumnCount = 4
        Me.tblMovieImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImages.Controls.Add(Me.gbMovieImagesOpts, 0, 0)
        Me.tblMovieImages.Controls.Add(Me.gbMovieImagesLandscapeOpts, 2, 6)
        Me.tblMovieImages.Controls.Add(Me.gbMovieImagesDiscArtOpts, 2, 5)
        Me.tblMovieImages.Controls.Add(Me.gbMovieImagesClearLogoOpts, 2, 4)
        Me.tblMovieImages.Controls.Add(Me.gbMovieImagesClearArtOpts, 2, 3)
        Me.tblMovieImages.Controls.Add(Me.gbMovieImagesActorThumbsOpts, 2, 2)
        Me.tblMovieImages.Controls.Add(Me.gbMovieImagesPosterOpts, 0, 1)
        Me.tblMovieImages.Controls.Add(Me.gbMovieImagesBannerOpts, 1, 1)
        Me.tblMovieImages.Controls.Add(Me.gbMovieImagesEThumbsOpts, 1, 2)
        Me.tblMovieImages.Controls.Add(Me.gbMovieImagesEFanartsOpts, 0, 2)
        Me.tblMovieImages.Controls.Add(Me.gbMovieImagesFanartOpts, 2, 1)
        Me.tblMovieImages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieImages.Location = New System.Drawing.Point(0, 0)
        Me.tblMovieImages.Name = "tblMovieImages"
        Me.tblMovieImages.RowCount = 8
        Me.tblMovieImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImages.Size = New System.Drawing.Size(982, 600)
        Me.tblMovieImages.TabIndex = 17
        '
        'gbMovieImagesOpts
        '
        Me.gbMovieImagesOpts.AutoSize = true
        Me.gbMovieImagesOpts.Controls.Add(Me.tblMovieImagesOpts)
        Me.gbMovieImagesOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieImagesOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbMovieImagesOpts.Name = "gbMovieImagesOpts"
        Me.gbMovieImagesOpts.Size = New System.Drawing.Size(281, 44)
        Me.gbMovieImagesOpts.TabIndex = 0
        Me.gbMovieImagesOpts.TabStop = false
        Me.gbMovieImagesOpts.Text = "Images"
        '
        'tblMovieImagesOpts
        '
        Me.tblMovieImagesOpts.AutoSize = true
        Me.tblMovieImagesOpts.ColumnCount = 1
        Me.tblMovieImagesOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesOpts.Controls.Add(Me.chkMovieNoSaveImagesToNfo, 0, 0)
        Me.tblMovieImagesOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieImagesOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieImagesOpts.Name = "tblMovieImagesOpts"
        Me.tblMovieImagesOpts.RowCount = 1
        Me.tblMovieImagesOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesOpts.Size = New System.Drawing.Size(275, 23)
        Me.tblMovieImagesOpts.TabIndex = 17
        '
        'chkMovieNoSaveImagesToNfo
        '
        Me.chkMovieNoSaveImagesToNfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieNoSaveImagesToNfo.AutoSize = true
        Me.chkMovieNoSaveImagesToNfo.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkMovieNoSaveImagesToNfo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieNoSaveImagesToNfo.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieNoSaveImagesToNfo.Name = "chkMovieNoSaveImagesToNfo"
        Me.chkMovieNoSaveImagesToNfo.Size = New System.Drawing.Size(187, 17)
        Me.chkMovieNoSaveImagesToNfo.TabIndex = 2
        Me.chkMovieNoSaveImagesToNfo.Text = "Do Not Save Image URLs to Nfo"
        Me.chkMovieNoSaveImagesToNfo.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkMovieNoSaveImagesToNfo.UseVisualStyleBackColor = true
        '
        'gbMovieImagesLandscapeOpts
        '
        Me.gbMovieImagesLandscapeOpts.AutoSize = true
        Me.gbMovieImagesLandscapeOpts.Controls.Add(Me.tblMovieImagesLandscapeOpts)
        Me.gbMovieImagesLandscapeOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieImagesLandscapeOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesLandscapeOpts.Location = New System.Drawing.Point(577, 401)
        Me.gbMovieImagesLandscapeOpts.Name = "gbMovieImagesLandscapeOpts"
        Me.gbMovieImagesLandscapeOpts.Size = New System.Drawing.Size(281, 44)
        Me.gbMovieImagesLandscapeOpts.TabIndex = 14
        Me.gbMovieImagesLandscapeOpts.TabStop = false
        Me.gbMovieImagesLandscapeOpts.Text = "Landscape"
        '
        'tblMovieImagesLandscapeOpts
        '
        Me.tblMovieImagesLandscapeOpts.AutoSize = true
        Me.tblMovieImagesLandscapeOpts.ColumnCount = 1
        Me.tblMovieImagesLandscapeOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesLandscapeOpts.Controls.Add(Me.chkMovieLandscapeOverwrite, 0, 0)
        Me.tblMovieImagesLandscapeOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieImagesLandscapeOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieImagesLandscapeOpts.Name = "tblMovieImagesLandscapeOpts"
        Me.tblMovieImagesLandscapeOpts.RowCount = 1
        Me.tblMovieImagesLandscapeOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesLandscapeOpts.Size = New System.Drawing.Size(275, 23)
        Me.tblMovieImagesLandscapeOpts.TabIndex = 17
        '
        'chkMovieLandscapeOverwrite
        '
        Me.chkMovieLandscapeOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieLandscapeOverwrite.AutoSize = true
        Me.chkMovieLandscapeOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLandscapeOverwrite.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieLandscapeOverwrite.Name = "chkMovieLandscapeOverwrite"
        Me.chkMovieLandscapeOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieLandscapeOverwrite.TabIndex = 4
        Me.chkMovieLandscapeOverwrite.Text = "Overwrite Existing"
        Me.chkMovieLandscapeOverwrite.UseVisualStyleBackColor = true
        '
        'gbMovieImagesDiscArtOpts
        '
        Me.gbMovieImagesDiscArtOpts.AutoSize = true
        Me.gbMovieImagesDiscArtOpts.Controls.Add(Me.tblMovieImagesDiscArtOpts)
        Me.gbMovieImagesDiscArtOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieImagesDiscArtOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesDiscArtOpts.Location = New System.Drawing.Point(577, 351)
        Me.gbMovieImagesDiscArtOpts.Name = "gbMovieImagesDiscArtOpts"
        Me.gbMovieImagesDiscArtOpts.Size = New System.Drawing.Size(281, 44)
        Me.gbMovieImagesDiscArtOpts.TabIndex = 15
        Me.gbMovieImagesDiscArtOpts.TabStop = false
        Me.gbMovieImagesDiscArtOpts.Text = "DiscArt"
        '
        'tblMovieImagesDiscArtOpts
        '
        Me.tblMovieImagesDiscArtOpts.AutoSize = true
        Me.tblMovieImagesDiscArtOpts.ColumnCount = 1
        Me.tblMovieImagesDiscArtOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesDiscArtOpts.Controls.Add(Me.chkMovieDiscArtOverwrite, 0, 0)
        Me.tblMovieImagesDiscArtOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieImagesDiscArtOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieImagesDiscArtOpts.Name = "tblMovieImagesDiscArtOpts"
        Me.tblMovieImagesDiscArtOpts.RowCount = 1
        Me.tblMovieImagesDiscArtOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesDiscArtOpts.Size = New System.Drawing.Size(275, 23)
        Me.tblMovieImagesDiscArtOpts.TabIndex = 17
        '
        'chkMovieDiscArtOverwrite
        '
        Me.chkMovieDiscArtOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieDiscArtOverwrite.AutoSize = true
        Me.chkMovieDiscArtOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieDiscArtOverwrite.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieDiscArtOverwrite.Name = "chkMovieDiscArtOverwrite"
        Me.chkMovieDiscArtOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieDiscArtOverwrite.TabIndex = 4
        Me.chkMovieDiscArtOverwrite.Text = "Overwrite Existing"
        Me.chkMovieDiscArtOverwrite.UseVisualStyleBackColor = true
        '
        'gbMovieImagesClearLogoOpts
        '
        Me.gbMovieImagesClearLogoOpts.AutoSize = true
        Me.gbMovieImagesClearLogoOpts.Controls.Add(Me.tblMovieImagesClearLogoOpts)
        Me.gbMovieImagesClearLogoOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieImagesClearLogoOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesClearLogoOpts.Location = New System.Drawing.Point(577, 301)
        Me.gbMovieImagesClearLogoOpts.Name = "gbMovieImagesClearLogoOpts"
        Me.gbMovieImagesClearLogoOpts.Size = New System.Drawing.Size(281, 44)
        Me.gbMovieImagesClearLogoOpts.TabIndex = 15
        Me.gbMovieImagesClearLogoOpts.TabStop = false
        Me.gbMovieImagesClearLogoOpts.Text = "ClearLogo"
        '
        'tblMovieImagesClearLogoOpts
        '
        Me.tblMovieImagesClearLogoOpts.AutoSize = true
        Me.tblMovieImagesClearLogoOpts.ColumnCount = 1
        Me.tblMovieImagesClearLogoOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesClearLogoOpts.Controls.Add(Me.chkMovieClearLogoOverwrite, 0, 0)
        Me.tblMovieImagesClearLogoOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieImagesClearLogoOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieImagesClearLogoOpts.Name = "tblMovieImagesClearLogoOpts"
        Me.tblMovieImagesClearLogoOpts.RowCount = 1
        Me.tblMovieImagesClearLogoOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesClearLogoOpts.Size = New System.Drawing.Size(275, 23)
        Me.tblMovieImagesClearLogoOpts.TabIndex = 17
        '
        'chkMovieClearLogoOverwrite
        '
        Me.chkMovieClearLogoOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieClearLogoOverwrite.AutoSize = true
        Me.chkMovieClearLogoOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieClearLogoOverwrite.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieClearLogoOverwrite.Name = "chkMovieClearLogoOverwrite"
        Me.chkMovieClearLogoOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieClearLogoOverwrite.TabIndex = 4
        Me.chkMovieClearLogoOverwrite.Text = "Overwrite Existing"
        Me.chkMovieClearLogoOverwrite.UseVisualStyleBackColor = true
        '
        'gbMovieImagesClearArtOpts
        '
        Me.gbMovieImagesClearArtOpts.AutoSize = true
        Me.gbMovieImagesClearArtOpts.Controls.Add(Me.tblMovieImagesClearArtOpts)
        Me.gbMovieImagesClearArtOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieImagesClearArtOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesClearArtOpts.Location = New System.Drawing.Point(577, 251)
        Me.gbMovieImagesClearArtOpts.Name = "gbMovieImagesClearArtOpts"
        Me.gbMovieImagesClearArtOpts.Size = New System.Drawing.Size(281, 44)
        Me.gbMovieImagesClearArtOpts.TabIndex = 15
        Me.gbMovieImagesClearArtOpts.TabStop = false
        Me.gbMovieImagesClearArtOpts.Text = "ClearArt"
        '
        'tblMovieImagesClearArtOpts
        '
        Me.tblMovieImagesClearArtOpts.AutoSize = true
        Me.tblMovieImagesClearArtOpts.ColumnCount = 1
        Me.tblMovieImagesClearArtOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesClearArtOpts.Controls.Add(Me.chkMovieClearArtOverwrite, 0, 0)
        Me.tblMovieImagesClearArtOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieImagesClearArtOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieImagesClearArtOpts.Name = "tblMovieImagesClearArtOpts"
        Me.tblMovieImagesClearArtOpts.RowCount = 1
        Me.tblMovieImagesClearArtOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesClearArtOpts.Size = New System.Drawing.Size(275, 23)
        Me.tblMovieImagesClearArtOpts.TabIndex = 17
        '
        'chkMovieClearArtOverwrite
        '
        Me.chkMovieClearArtOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieClearArtOverwrite.AutoSize = true
        Me.chkMovieClearArtOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieClearArtOverwrite.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieClearArtOverwrite.Name = "chkMovieClearArtOverwrite"
        Me.chkMovieClearArtOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieClearArtOverwrite.TabIndex = 4
        Me.chkMovieClearArtOverwrite.Text = "Overwrite Existing"
        Me.chkMovieClearArtOverwrite.UseVisualStyleBackColor = true
        '
        'gbMovieImagesActorThumbsOpts
        '
        Me.gbMovieImagesActorThumbsOpts.AutoSize = true
        Me.gbMovieImagesActorThumbsOpts.Controls.Add(Me.tblMovieImagesActorThumbsOpts)
        Me.gbMovieImagesActorThumbsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieImagesActorThumbsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesActorThumbsOpts.Location = New System.Drawing.Point(577, 201)
        Me.gbMovieImagesActorThumbsOpts.Name = "gbMovieImagesActorThumbsOpts"
        Me.gbMovieImagesActorThumbsOpts.Size = New System.Drawing.Size(281, 44)
        Me.gbMovieImagesActorThumbsOpts.TabIndex = 16
        Me.gbMovieImagesActorThumbsOpts.TabStop = false
        Me.gbMovieImagesActorThumbsOpts.Text = "Actor Thumbs"
        '
        'tblMovieImagesActorThumbsOpts
        '
        Me.tblMovieImagesActorThumbsOpts.AutoSize = true
        Me.tblMovieImagesActorThumbsOpts.ColumnCount = 1
        Me.tblMovieImagesActorThumbsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesActorThumbsOpts.Controls.Add(Me.chkMovieActorThumbsOverwrite, 0, 0)
        Me.tblMovieImagesActorThumbsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieImagesActorThumbsOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieImagesActorThumbsOpts.Name = "tblMovieImagesActorThumbsOpts"
        Me.tblMovieImagesActorThumbsOpts.RowCount = 1
        Me.tblMovieImagesActorThumbsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesActorThumbsOpts.Size = New System.Drawing.Size(275, 23)
        Me.tblMovieImagesActorThumbsOpts.TabIndex = 17
        '
        'chkMovieActorThumbsOverwrite
        '
        Me.chkMovieActorThumbsOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieActorThumbsOverwrite.AutoSize = true
        Me.chkMovieActorThumbsOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieActorThumbsOverwrite.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieActorThumbsOverwrite.Name = "chkMovieActorThumbsOverwrite"
        Me.chkMovieActorThumbsOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieActorThumbsOverwrite.TabIndex = 4
        Me.chkMovieActorThumbsOverwrite.Text = "Overwrite Existing"
        Me.chkMovieActorThumbsOverwrite.UseVisualStyleBackColor = true
        '
        'gbMovieImagesPosterOpts
        '
        Me.gbMovieImagesPosterOpts.AutoSize = true
        Me.gbMovieImagesPosterOpts.Controls.Add(Me.tblMovieImagesPosterOpts)
        Me.gbMovieImagesPosterOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieImagesPosterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesPosterOpts.Location = New System.Drawing.Point(3, 53)
        Me.gbMovieImagesPosterOpts.Name = "gbMovieImagesPosterOpts"
        Me.gbMovieImagesPosterOpts.Size = New System.Drawing.Size(281, 142)
        Me.gbMovieImagesPosterOpts.TabIndex = 2
        Me.gbMovieImagesPosterOpts.TabStop = false
        Me.gbMovieImagesPosterOpts.Text = "Poster"
        '
        'tblMovieImagesPosterOpts
        '
        Me.tblMovieImagesPosterOpts.AutoSize = true
        Me.tblMovieImagesPosterOpts.ColumnCount = 5
        Me.tblMovieImagesPosterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesPosterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesPosterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesPosterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesPosterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesPosterOpts.Controls.Add(Me.lblMoviePosterSize, 0, 0)
        Me.tblMovieImagesPosterOpts.Controls.Add(Me.txtMoviePosterHeight, 3, 4)
        Me.tblMovieImagesPosterOpts.Controls.Add(Me.txtMoviePosterWidth, 1, 4)
        Me.tblMovieImagesPosterOpts.Controls.Add(Me.lblMoviePosterHeight, 2, 4)
        Me.tblMovieImagesPosterOpts.Controls.Add(Me.cbMoviePosterPrefSize, 0, 1)
        Me.tblMovieImagesPosterOpts.Controls.Add(Me.lblMoviePosterWidth, 0, 4)
        Me.tblMovieImagesPosterOpts.Controls.Add(Me.chkMoviePosterOverwrite, 0, 2)
        Me.tblMovieImagesPosterOpts.Controls.Add(Me.chkMoviePosterResize, 0, 3)
        Me.tblMovieImagesPosterOpts.Controls.Add(Me.chkMoviePosterPrefOnly, 2, 1)
        Me.tblMovieImagesPosterOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieImagesPosterOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieImagesPosterOpts.Name = "tblMovieImagesPosterOpts"
        Me.tblMovieImagesPosterOpts.RowCount = 6
        Me.tblMovieImagesPosterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblMovieImagesPosterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesPosterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesPosterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesPosterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesPosterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesPosterOpts.Size = New System.Drawing.Size(275, 121)
        Me.tblMovieImagesPosterOpts.TabIndex = 9
        '
        'lblMoviePosterSize
        '
        Me.lblMoviePosterSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMoviePosterSize.AutoSize = true
        Me.tblMovieImagesPosterOpts.SetColumnSpan(Me.lblMoviePosterSize, 4)
        Me.lblMoviePosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMoviePosterSize.Location = New System.Drawing.Point(3, 3)
        Me.lblMoviePosterSize.Name = "lblMoviePosterSize"
        Me.lblMoviePosterSize.Size = New System.Drawing.Size(80, 13)
        Me.lblMoviePosterSize.TabIndex = 0
        Me.lblMoviePosterSize.Text = "Preferred Size:"
        '
        'txtMoviePosterHeight
        '
        Me.txtMoviePosterHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMoviePosterHeight.Enabled = false
        Me.txtMoviePosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMoviePosterHeight.Location = New System.Drawing.Point(232, 96)
        Me.txtMoviePosterHeight.Name = "txtMoviePosterHeight"
        Me.txtMoviePosterHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtMoviePosterHeight.TabIndex = 8
        '
        'txtMoviePosterWidth
        '
        Me.txtMoviePosterWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMoviePosterWidth.Enabled = false
        Me.txtMoviePosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMoviePosterWidth.Location = New System.Drawing.Point(75, 96)
        Me.txtMoviePosterWidth.Name = "txtMoviePosterWidth"
        Me.txtMoviePosterWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtMoviePosterWidth.TabIndex = 6
        '
        'lblMoviePosterHeight
        '
        Me.lblMoviePosterHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMoviePosterHeight.AutoSize = true
        Me.lblMoviePosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMoviePosterHeight.Location = New System.Drawing.Point(157, 100)
        Me.lblMoviePosterHeight.Name = "lblMoviePosterHeight"
        Me.lblMoviePosterHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblMoviePosterHeight.TabIndex = 7
        Me.lblMoviePosterHeight.Text = "Max Height:"
        '
        'cbMoviePosterPrefSize
        '
        Me.tblMovieImagesPosterOpts.SetColumnSpan(Me.cbMoviePosterPrefSize, 2)
        Me.cbMoviePosterPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMoviePosterPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMoviePosterPrefSize.FormattingEnabled = true
        Me.cbMoviePosterPrefSize.Location = New System.Drawing.Point(3, 23)
        Me.cbMoviePosterPrefSize.Name = "cbMoviePosterPrefSize"
        Me.cbMoviePosterPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbMoviePosterPrefSize.TabIndex = 1
        '
        'lblMoviePosterWidth
        '
        Me.lblMoviePosterWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMoviePosterWidth.AutoSize = true
        Me.lblMoviePosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMoviePosterWidth.Location = New System.Drawing.Point(3, 100)
        Me.lblMoviePosterWidth.Name = "lblMoviePosterWidth"
        Me.lblMoviePosterWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblMoviePosterWidth.TabIndex = 5
        Me.lblMoviePosterWidth.Text = "Max Width:"
        '
        'chkMoviePosterOverwrite
        '
        Me.chkMoviePosterOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMoviePosterOverwrite.AutoSize = true
        Me.tblMovieImagesPosterOpts.SetColumnSpan(Me.chkMoviePosterOverwrite, 4)
        Me.chkMoviePosterOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMoviePosterOverwrite.Location = New System.Drawing.Point(3, 50)
        Me.chkMoviePosterOverwrite.Name = "chkMoviePosterOverwrite"
        Me.chkMoviePosterOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMoviePosterOverwrite.TabIndex = 3
        Me.chkMoviePosterOverwrite.Text = "Overwrite Existing"
        Me.chkMoviePosterOverwrite.UseVisualStyleBackColor = true
        '
        'chkMoviePosterResize
        '
        Me.chkMoviePosterResize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMoviePosterResize.AutoSize = true
        Me.tblMovieImagesPosterOpts.SetColumnSpan(Me.chkMoviePosterResize, 4)
        Me.chkMoviePosterResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMoviePosterResize.Location = New System.Drawing.Point(3, 73)
        Me.chkMoviePosterResize.Name = "chkMoviePosterResize"
        Me.chkMoviePosterResize.Size = New System.Drawing.Size(133, 17)
        Me.chkMoviePosterResize.TabIndex = 4
        Me.chkMoviePosterResize.Text = "Automatically Resize:"
        Me.chkMoviePosterResize.UseVisualStyleBackColor = true
        '
        'chkMoviePosterPrefOnly
        '
        Me.chkMoviePosterPrefOnly.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMoviePosterPrefOnly.AutoSize = true
        Me.tblMovieImagesPosterOpts.SetColumnSpan(Me.chkMoviePosterPrefOnly, 2)
        Me.chkMoviePosterPrefOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMoviePosterPrefOnly.Location = New System.Drawing.Point(157, 25)
        Me.chkMoviePosterPrefOnly.Name = "chkMoviePosterPrefOnly"
        Me.chkMoviePosterPrefOnly.Size = New System.Drawing.Size(50, 17)
        Me.chkMoviePosterPrefOnly.TabIndex = 2
        Me.chkMoviePosterPrefOnly.Text = "Only"
        Me.chkMoviePosterPrefOnly.UseVisualStyleBackColor = true
        '
        'gbMovieImagesBannerOpts
        '
        Me.gbMovieImagesBannerOpts.AutoSize = true
        Me.gbMovieImagesBannerOpts.Controls.Add(Me.tblMovieImagesBannerOpts)
        Me.gbMovieImagesBannerOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieImagesBannerOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesBannerOpts.Location = New System.Drawing.Point(290, 53)
        Me.gbMovieImagesBannerOpts.Name = "gbMovieImagesBannerOpts"
        Me.gbMovieImagesBannerOpts.Size = New System.Drawing.Size(281, 142)
        Me.gbMovieImagesBannerOpts.TabIndex = 12
        Me.gbMovieImagesBannerOpts.TabStop = false
        Me.gbMovieImagesBannerOpts.Text = "Banner"
        '
        'tblMovieImagesBannerOpts
        '
        Me.tblMovieImagesBannerOpts.AutoSize = true
        Me.tblMovieImagesBannerOpts.ColumnCount = 5
        Me.tblMovieImagesBannerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesBannerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesBannerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesBannerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesBannerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesBannerOpts.Controls.Add(Me.txtMovieBannerHeight, 3, 4)
        Me.tblMovieImagesBannerOpts.Controls.Add(Me.txtMovieBannerWidth, 1, 4)
        Me.tblMovieImagesBannerOpts.Controls.Add(Me.lblMovieBannerHeight, 2, 4)
        Me.tblMovieImagesBannerOpts.Controls.Add(Me.chkMovieBannerPrefOnly, 2, 1)
        Me.tblMovieImagesBannerOpts.Controls.Add(Me.lblMovieBannerType, 0, 0)
        Me.tblMovieImagesBannerOpts.Controls.Add(Me.lblMovieBannerWidth, 0, 4)
        Me.tblMovieImagesBannerOpts.Controls.Add(Me.cbMovieBannerPrefType, 0, 1)
        Me.tblMovieImagesBannerOpts.Controls.Add(Me.chkMovieBannerOverwrite, 0, 2)
        Me.tblMovieImagesBannerOpts.Controls.Add(Me.chkMovieBannerResize, 0, 3)
        Me.tblMovieImagesBannerOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieImagesBannerOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieImagesBannerOpts.Name = "tblMovieImagesBannerOpts"
        Me.tblMovieImagesBannerOpts.RowCount = 6
        Me.tblMovieImagesBannerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblMovieImagesBannerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesBannerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesBannerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesBannerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesBannerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesBannerOpts.Size = New System.Drawing.Size(275, 121)
        Me.tblMovieImagesBannerOpts.TabIndex = 17
        '
        'txtMovieBannerHeight
        '
        Me.txtMovieBannerHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieBannerHeight.Enabled = false
        Me.txtMovieBannerHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieBannerHeight.Location = New System.Drawing.Point(232, 96)
        Me.txtMovieBannerHeight.Name = "txtMovieBannerHeight"
        Me.txtMovieBannerHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieBannerHeight.TabIndex = 8
        '
        'txtMovieBannerWidth
        '
        Me.txtMovieBannerWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieBannerWidth.Enabled = false
        Me.txtMovieBannerWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieBannerWidth.Location = New System.Drawing.Point(75, 96)
        Me.txtMovieBannerWidth.Name = "txtMovieBannerWidth"
        Me.txtMovieBannerWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieBannerWidth.TabIndex = 6
        '
        'lblMovieBannerHeight
        '
        Me.lblMovieBannerHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieBannerHeight.AutoSize = true
        Me.lblMovieBannerHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieBannerHeight.Location = New System.Drawing.Point(157, 100)
        Me.lblMovieBannerHeight.Name = "lblMovieBannerHeight"
        Me.lblMovieBannerHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblMovieBannerHeight.TabIndex = 7
        Me.lblMovieBannerHeight.Text = "Max Height:"
        '
        'chkMovieBannerPrefOnly
        '
        Me.chkMovieBannerPrefOnly.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieBannerPrefOnly.AutoSize = true
        Me.tblMovieImagesBannerOpts.SetColumnSpan(Me.chkMovieBannerPrefOnly, 2)
        Me.chkMovieBannerPrefOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieBannerPrefOnly.Location = New System.Drawing.Point(157, 25)
        Me.chkMovieBannerPrefOnly.Name = "chkMovieBannerPrefOnly"
        Me.chkMovieBannerPrefOnly.Size = New System.Drawing.Size(50, 17)
        Me.chkMovieBannerPrefOnly.TabIndex = 2
        Me.chkMovieBannerPrefOnly.Text = "Only"
        Me.chkMovieBannerPrefOnly.UseVisualStyleBackColor = true
        '
        'lblMovieBannerType
        '
        Me.lblMovieBannerType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieBannerType.AutoSize = true
        Me.tblMovieImagesBannerOpts.SetColumnSpan(Me.lblMovieBannerType, 4)
        Me.lblMovieBannerType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieBannerType.Location = New System.Drawing.Point(3, 3)
        Me.lblMovieBannerType.Name = "lblMovieBannerType"
        Me.lblMovieBannerType.Size = New System.Drawing.Size(83, 13)
        Me.lblMovieBannerType.TabIndex = 0
        Me.lblMovieBannerType.Text = "Preferred Type:"
        '
        'lblMovieBannerWidth
        '
        Me.lblMovieBannerWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieBannerWidth.AutoSize = true
        Me.lblMovieBannerWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieBannerWidth.Location = New System.Drawing.Point(3, 100)
        Me.lblMovieBannerWidth.Name = "lblMovieBannerWidth"
        Me.lblMovieBannerWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblMovieBannerWidth.TabIndex = 5
        Me.lblMovieBannerWidth.Text = "Max Width:"
        '
        'cbMovieBannerPrefType
        '
        Me.cbMovieBannerPrefType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblMovieImagesBannerOpts.SetColumnSpan(Me.cbMovieBannerPrefType, 2)
        Me.cbMovieBannerPrefType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieBannerPrefType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieBannerPrefType.FormattingEnabled = true
        Me.cbMovieBannerPrefType.Location = New System.Drawing.Point(3, 23)
        Me.cbMovieBannerPrefType.Name = "cbMovieBannerPrefType"
        Me.cbMovieBannerPrefType.Size = New System.Drawing.Size(148, 21)
        Me.cbMovieBannerPrefType.TabIndex = 1
        '
        'chkMovieBannerOverwrite
        '
        Me.chkMovieBannerOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieBannerOverwrite.AutoSize = true
        Me.tblMovieImagesBannerOpts.SetColumnSpan(Me.chkMovieBannerOverwrite, 4)
        Me.chkMovieBannerOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieBannerOverwrite.Location = New System.Drawing.Point(3, 50)
        Me.chkMovieBannerOverwrite.Name = "chkMovieBannerOverwrite"
        Me.chkMovieBannerOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieBannerOverwrite.TabIndex = 3
        Me.chkMovieBannerOverwrite.Text = "Overwrite Existing"
        Me.chkMovieBannerOverwrite.UseVisualStyleBackColor = true
        '
        'chkMovieBannerResize
        '
        Me.chkMovieBannerResize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieBannerResize.AutoSize = true
        Me.tblMovieImagesBannerOpts.SetColumnSpan(Me.chkMovieBannerResize, 4)
        Me.chkMovieBannerResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieBannerResize.Location = New System.Drawing.Point(3, 73)
        Me.chkMovieBannerResize.Name = "chkMovieBannerResize"
        Me.chkMovieBannerResize.Size = New System.Drawing.Size(133, 17)
        Me.chkMovieBannerResize.TabIndex = 4
        Me.chkMovieBannerResize.Text = "Automatically Resize:"
        Me.chkMovieBannerResize.UseVisualStyleBackColor = true
        '
        'gbMovieImagesEThumbsOpts
        '
        Me.gbMovieImagesEThumbsOpts.AutoSize = true
        Me.gbMovieImagesEThumbsOpts.Controls.Add(Me.tblMovieImagesEThumbsOpts)
        Me.gbMovieImagesEThumbsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieImagesEThumbsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesEThumbsOpts.Location = New System.Drawing.Point(290, 201)
        Me.gbMovieImagesEThumbsOpts.Name = "gbMovieImagesEThumbsOpts"
        Me.tblMovieImages.SetRowSpan(Me.gbMovieImagesEThumbsOpts, 4)
        Me.gbMovieImagesEThumbsOpts.Size = New System.Drawing.Size(281, 194)
        Me.gbMovieImagesEThumbsOpts.TabIndex = 12
        Me.gbMovieImagesEThumbsOpts.TabStop = false
        Me.gbMovieImagesEThumbsOpts.Text = "Extrathumbs"
        '
        'tblMovieImagesEThumbsOpts
        '
        Me.tblMovieImagesEThumbsOpts.AutoSize = true
        Me.tblMovieImagesEThumbsOpts.ColumnCount = 5
        Me.tblMovieImagesEThumbsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesEThumbsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesEThumbsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesEThumbsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesEThumbsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesEThumbsOpts.Controls.Add(Me.txtMovieEThumbsHeight, 3, 5)
        Me.tblMovieImagesEThumbsOpts.Controls.Add(Me.txtMovieEThumbsWidth, 1, 5)
        Me.tblMovieImagesEThumbsOpts.Controls.Add(Me.lblMovieEThumbsHeight, 2, 5)
        Me.tblMovieImagesEThumbsOpts.Controls.Add(Me.txtMovieEThumbsLimit, 1, 2)
        Me.tblMovieImagesEThumbsOpts.Controls.Add(Me.lblMovieEThumbsLimit, 0, 2)
        Me.tblMovieImagesEThumbsOpts.Controls.Add(Me.lblMovieEThumbsWidth, 0, 5)
        Me.tblMovieImagesEThumbsOpts.Controls.Add(Me.lblMovieEThumbsSize, 0, 0)
        Me.tblMovieImagesEThumbsOpts.Controls.Add(Me.cbMovieEThumbsPrefSize, 0, 1)
        Me.tblMovieImagesEThumbsOpts.Controls.Add(Me.chkMovieEThumbsResize, 0, 4)
        Me.tblMovieImagesEThumbsOpts.Controls.Add(Me.chkMovieEThumbsPrefOnly, 2, 1)
        Me.tblMovieImagesEThumbsOpts.Controls.Add(Me.chkMovieEThumbsOverwrite, 0, 3)
        Me.tblMovieImagesEThumbsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieImagesEThumbsOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieImagesEThumbsOpts.Name = "tblMovieImagesEThumbsOpts"
        Me.tblMovieImagesEThumbsOpts.RowCount = 7
        Me.tblMovieImagesEThumbsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblMovieImagesEThumbsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesEThumbsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesEThumbsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesEThumbsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesEThumbsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesEThumbsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesEThumbsOpts.Size = New System.Drawing.Size(275, 173)
        Me.tblMovieImagesEThumbsOpts.TabIndex = 17
        '
        'txtMovieEThumbsHeight
        '
        Me.txtMovieEThumbsHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieEThumbsHeight.Enabled = false
        Me.txtMovieEThumbsHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieEThumbsHeight.Location = New System.Drawing.Point(232, 124)
        Me.txtMovieEThumbsHeight.Name = "txtMovieEThumbsHeight"
        Me.txtMovieEThumbsHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieEThumbsHeight.TabIndex = 8
        '
        'txtMovieEThumbsWidth
        '
        Me.txtMovieEThumbsWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieEThumbsWidth.Enabled = false
        Me.txtMovieEThumbsWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieEThumbsWidth.Location = New System.Drawing.Point(75, 124)
        Me.txtMovieEThumbsWidth.Name = "txtMovieEThumbsWidth"
        Me.txtMovieEThumbsWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieEThumbsWidth.TabIndex = 6
        '
        'lblMovieEThumbsHeight
        '
        Me.lblMovieEThumbsHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieEThumbsHeight.AutoSize = true
        Me.lblMovieEThumbsHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieEThumbsHeight.Location = New System.Drawing.Point(157, 128)
        Me.lblMovieEThumbsHeight.Name = "lblMovieEThumbsHeight"
        Me.lblMovieEThumbsHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblMovieEThumbsHeight.TabIndex = 7
        Me.lblMovieEThumbsHeight.Text = "Max Height:"
        '
        'txtMovieEThumbsLimit
        '
        Me.txtMovieEThumbsLimit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieEThumbsLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieEThumbsLimit.Location = New System.Drawing.Point(75, 50)
        Me.txtMovieEThumbsLimit.Name = "txtMovieEThumbsLimit"
        Me.txtMovieEThumbsLimit.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieEThumbsLimit.TabIndex = 12
        '
        'lblMovieEThumbsLimit
        '
        Me.lblMovieEThumbsLimit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieEThumbsLimit.AutoSize = true
        Me.lblMovieEThumbsLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMovieEThumbsLimit.Location = New System.Drawing.Point(3, 54)
        Me.lblMovieEThumbsLimit.Name = "lblMovieEThumbsLimit"
        Me.lblMovieEThumbsLimit.Size = New System.Drawing.Size(34, 13)
        Me.lblMovieEThumbsLimit.TabIndex = 14
        Me.lblMovieEThumbsLimit.Text = "Limit:"
        '
        'lblMovieEThumbsWidth
        '
        Me.lblMovieEThumbsWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieEThumbsWidth.AutoSize = true
        Me.lblMovieEThumbsWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieEThumbsWidth.Location = New System.Drawing.Point(3, 128)
        Me.lblMovieEThumbsWidth.Name = "lblMovieEThumbsWidth"
        Me.lblMovieEThumbsWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblMovieEThumbsWidth.TabIndex = 5
        Me.lblMovieEThumbsWidth.Text = "Max Width:"
        '
        'lblMovieEThumbsSize
        '
        Me.lblMovieEThumbsSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieEThumbsSize.AutoSize = true
        Me.tblMovieImagesEThumbsOpts.SetColumnSpan(Me.lblMovieEThumbsSize, 4)
        Me.lblMovieEThumbsSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieEThumbsSize.Location = New System.Drawing.Point(3, 3)
        Me.lblMovieEThumbsSize.Name = "lblMovieEThumbsSize"
        Me.lblMovieEThumbsSize.Size = New System.Drawing.Size(80, 13)
        Me.lblMovieEThumbsSize.TabIndex = 0
        Me.lblMovieEThumbsSize.Text = "Preferred Size:"
        '
        'cbMovieEThumbsPrefSize
        '
        Me.cbMovieEThumbsPrefSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblMovieImagesEThumbsOpts.SetColumnSpan(Me.cbMovieEThumbsPrefSize, 2)
        Me.cbMovieEThumbsPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieEThumbsPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieEThumbsPrefSize.FormattingEnabled = true
        Me.cbMovieEThumbsPrefSize.Location = New System.Drawing.Point(3, 23)
        Me.cbMovieEThumbsPrefSize.Name = "cbMovieEThumbsPrefSize"
        Me.cbMovieEThumbsPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbMovieEThumbsPrefSize.TabIndex = 1
        '
        'chkMovieEThumbsResize
        '
        Me.chkMovieEThumbsResize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieEThumbsResize.AutoSize = true
        Me.tblMovieImagesEThumbsOpts.SetColumnSpan(Me.chkMovieEThumbsResize, 4)
        Me.chkMovieEThumbsResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieEThumbsResize.Location = New System.Drawing.Point(3, 101)
        Me.chkMovieEThumbsResize.Name = "chkMovieEThumbsResize"
        Me.chkMovieEThumbsResize.Size = New System.Drawing.Size(133, 17)
        Me.chkMovieEThumbsResize.TabIndex = 4
        Me.chkMovieEThumbsResize.Text = "Automatically Resize:"
        Me.chkMovieEThumbsResize.UseVisualStyleBackColor = true
        '
        'chkMovieEThumbsPrefOnly
        '
        Me.chkMovieEThumbsPrefOnly.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieEThumbsPrefOnly.AutoSize = true
        Me.tblMovieImagesEThumbsOpts.SetColumnSpan(Me.chkMovieEThumbsPrefOnly, 2)
        Me.chkMovieEThumbsPrefOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieEThumbsPrefOnly.Location = New System.Drawing.Point(157, 25)
        Me.chkMovieEThumbsPrefOnly.Name = "chkMovieEThumbsPrefOnly"
        Me.chkMovieEThumbsPrefOnly.Size = New System.Drawing.Size(50, 17)
        Me.chkMovieEThumbsPrefOnly.TabIndex = 2
        Me.chkMovieEThumbsPrefOnly.Text = "Only"
        Me.chkMovieEThumbsPrefOnly.UseVisualStyleBackColor = true
        '
        'chkMovieEThumbsOverwrite
        '
        Me.chkMovieEThumbsOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieEThumbsOverwrite.AutoSize = true
        Me.tblMovieImagesEThumbsOpts.SetColumnSpan(Me.chkMovieEThumbsOverwrite, 4)
        Me.chkMovieEThumbsOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieEThumbsOverwrite.Location = New System.Drawing.Point(3, 78)
        Me.chkMovieEThumbsOverwrite.Name = "chkMovieEThumbsOverwrite"
        Me.chkMovieEThumbsOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieEThumbsOverwrite.TabIndex = 3
        Me.chkMovieEThumbsOverwrite.Text = "Overwrite Existing"
        Me.chkMovieEThumbsOverwrite.UseVisualStyleBackColor = true
        '
        'gbMovieImagesEFanartsOpts
        '
        Me.gbMovieImagesEFanartsOpts.AutoSize = true
        Me.gbMovieImagesEFanartsOpts.Controls.Add(Me.tblMovieImagesEFanartsOpts)
        Me.gbMovieImagesEFanartsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieImagesEFanartsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesEFanartsOpts.Location = New System.Drawing.Point(3, 201)
        Me.gbMovieImagesEFanartsOpts.Name = "gbMovieImagesEFanartsOpts"
        Me.tblMovieImages.SetRowSpan(Me.gbMovieImagesEFanartsOpts, 4)
        Me.gbMovieImagesEFanartsOpts.Size = New System.Drawing.Size(281, 194)
        Me.gbMovieImagesEFanartsOpts.TabIndex = 13
        Me.gbMovieImagesEFanartsOpts.TabStop = false
        Me.gbMovieImagesEFanartsOpts.Text = "Extrafanarts"
        '
        'tblMovieImagesEFanartsOpts
        '
        Me.tblMovieImagesEFanartsOpts.AutoSize = true
        Me.tblMovieImagesEFanartsOpts.ColumnCount = 5
        Me.tblMovieImagesEFanartsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesEFanartsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesEFanartsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesEFanartsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesEFanartsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesEFanartsOpts.Controls.Add(Me.lblMovieEFanartsSize, 0, 0)
        Me.tblMovieImagesEFanartsOpts.Controls.Add(Me.cbMovieEFanartsPrefSize, 0, 1)
        Me.tblMovieImagesEFanartsOpts.Controls.Add(Me.chkMovieEFanartsPrefOnly, 2, 1)
        Me.tblMovieImagesEFanartsOpts.Controls.Add(Me.txtMovieEFanartsHeight, 3, 5)
        Me.tblMovieImagesEFanartsOpts.Controls.Add(Me.txtMovieEFanartsWidth, 1, 5)
        Me.tblMovieImagesEFanartsOpts.Controls.Add(Me.lblMovieEFanartsHeight, 2, 5)
        Me.tblMovieImagesEFanartsOpts.Controls.Add(Me.chkMovieEFanartsResize, 0, 4)
        Me.tblMovieImagesEFanartsOpts.Controls.Add(Me.lblMovieEFanartsWidth, 0, 5)
        Me.tblMovieImagesEFanartsOpts.Controls.Add(Me.chkMovieEFanartsOverwrite, 0, 3)
        Me.tblMovieImagesEFanartsOpts.Controls.Add(Me.lblMovieEFanartsLimit, 0, 2)
        Me.tblMovieImagesEFanartsOpts.Controls.Add(Me.txtMovieEFanartsLimit, 1, 2)
        Me.tblMovieImagesEFanartsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieImagesEFanartsOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieImagesEFanartsOpts.Name = "tblMovieImagesEFanartsOpts"
        Me.tblMovieImagesEFanartsOpts.RowCount = 7
        Me.tblMovieImagesEFanartsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblMovieImagesEFanartsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesEFanartsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesEFanartsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesEFanartsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesEFanartsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesEFanartsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesEFanartsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblMovieImagesEFanartsOpts.Size = New System.Drawing.Size(275, 173)
        Me.tblMovieImagesEFanartsOpts.TabIndex = 14
        '
        'lblMovieEFanartsSize
        '
        Me.lblMovieEFanartsSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieEFanartsSize.AutoSize = true
        Me.tblMovieImagesEFanartsOpts.SetColumnSpan(Me.lblMovieEFanartsSize, 4)
        Me.lblMovieEFanartsSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieEFanartsSize.Location = New System.Drawing.Point(3, 3)
        Me.lblMovieEFanartsSize.Name = "lblMovieEFanartsSize"
        Me.lblMovieEFanartsSize.Size = New System.Drawing.Size(80, 13)
        Me.lblMovieEFanartsSize.TabIndex = 0
        Me.lblMovieEFanartsSize.Text = "Preferred Size:"
        '
        'cbMovieEFanartsPrefSize
        '
        Me.tblMovieImagesEFanartsOpts.SetColumnSpan(Me.cbMovieEFanartsPrefSize, 2)
        Me.cbMovieEFanartsPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieEFanartsPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieEFanartsPrefSize.FormattingEnabled = true
        Me.cbMovieEFanartsPrefSize.Location = New System.Drawing.Point(3, 23)
        Me.cbMovieEFanartsPrefSize.Name = "cbMovieEFanartsPrefSize"
        Me.cbMovieEFanartsPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbMovieEFanartsPrefSize.TabIndex = 1
        '
        'chkMovieEFanartsPrefOnly
        '
        Me.chkMovieEFanartsPrefOnly.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieEFanartsPrefOnly.AutoSize = true
        Me.tblMovieImagesEFanartsOpts.SetColumnSpan(Me.chkMovieEFanartsPrefOnly, 2)
        Me.chkMovieEFanartsPrefOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieEFanartsPrefOnly.Location = New System.Drawing.Point(157, 25)
        Me.chkMovieEFanartsPrefOnly.Name = "chkMovieEFanartsPrefOnly"
        Me.chkMovieEFanartsPrefOnly.Size = New System.Drawing.Size(50, 17)
        Me.chkMovieEFanartsPrefOnly.TabIndex = 2
        Me.chkMovieEFanartsPrefOnly.Text = "Only"
        Me.chkMovieEFanartsPrefOnly.UseVisualStyleBackColor = true
        '
        'txtMovieEFanartsHeight
        '
        Me.txtMovieEFanartsHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieEFanartsHeight.Enabled = false
        Me.txtMovieEFanartsHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieEFanartsHeight.Location = New System.Drawing.Point(232, 124)
        Me.txtMovieEFanartsHeight.Name = "txtMovieEFanartsHeight"
        Me.txtMovieEFanartsHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieEFanartsHeight.TabIndex = 8
        '
        'txtMovieEFanartsWidth
        '
        Me.txtMovieEFanartsWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieEFanartsWidth.Enabled = false
        Me.txtMovieEFanartsWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieEFanartsWidth.Location = New System.Drawing.Point(75, 124)
        Me.txtMovieEFanartsWidth.Name = "txtMovieEFanartsWidth"
        Me.txtMovieEFanartsWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieEFanartsWidth.TabIndex = 6
        '
        'lblMovieEFanartsHeight
        '
        Me.lblMovieEFanartsHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieEFanartsHeight.AutoSize = true
        Me.lblMovieEFanartsHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieEFanartsHeight.Location = New System.Drawing.Point(157, 128)
        Me.lblMovieEFanartsHeight.Name = "lblMovieEFanartsHeight"
        Me.lblMovieEFanartsHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblMovieEFanartsHeight.TabIndex = 7
        Me.lblMovieEFanartsHeight.Text = "Max Height:"
        '
        'chkMovieEFanartsResize
        '
        Me.chkMovieEFanartsResize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieEFanartsResize.AutoSize = true
        Me.tblMovieImagesEFanartsOpts.SetColumnSpan(Me.chkMovieEFanartsResize, 4)
        Me.chkMovieEFanartsResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieEFanartsResize.Location = New System.Drawing.Point(3, 101)
        Me.chkMovieEFanartsResize.Name = "chkMovieEFanartsResize"
        Me.chkMovieEFanartsResize.Size = New System.Drawing.Size(133, 17)
        Me.chkMovieEFanartsResize.TabIndex = 4
        Me.chkMovieEFanartsResize.Text = "Automatically Resize:"
        Me.chkMovieEFanartsResize.UseVisualStyleBackColor = true
        '
        'lblMovieEFanartsWidth
        '
        Me.lblMovieEFanartsWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieEFanartsWidth.AutoSize = true
        Me.lblMovieEFanartsWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieEFanartsWidth.Location = New System.Drawing.Point(3, 128)
        Me.lblMovieEFanartsWidth.Name = "lblMovieEFanartsWidth"
        Me.lblMovieEFanartsWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblMovieEFanartsWidth.TabIndex = 5
        Me.lblMovieEFanartsWidth.Text = "Max Width:"
        '
        'chkMovieEFanartsOverwrite
        '
        Me.chkMovieEFanartsOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieEFanartsOverwrite.AutoSize = true
        Me.tblMovieImagesEFanartsOpts.SetColumnSpan(Me.chkMovieEFanartsOverwrite, 4)
        Me.chkMovieEFanartsOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieEFanartsOverwrite.Location = New System.Drawing.Point(3, 78)
        Me.chkMovieEFanartsOverwrite.Name = "chkMovieEFanartsOverwrite"
        Me.chkMovieEFanartsOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieEFanartsOverwrite.TabIndex = 3
        Me.chkMovieEFanartsOverwrite.Text = "Overwrite Existing"
        Me.chkMovieEFanartsOverwrite.UseVisualStyleBackColor = true
        '
        'lblMovieEFanartsLimit
        '
        Me.lblMovieEFanartsLimit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieEFanartsLimit.AutoSize = true
        Me.lblMovieEFanartsLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMovieEFanartsLimit.Location = New System.Drawing.Point(3, 54)
        Me.lblMovieEFanartsLimit.Name = "lblMovieEFanartsLimit"
        Me.lblMovieEFanartsLimit.Size = New System.Drawing.Size(34, 13)
        Me.lblMovieEFanartsLimit.TabIndex = 13
        Me.lblMovieEFanartsLimit.Text = "Limit:"
        '
        'txtMovieEFanartsLimit
        '
        Me.txtMovieEFanartsLimit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieEFanartsLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieEFanartsLimit.Location = New System.Drawing.Point(75, 50)
        Me.txtMovieEFanartsLimit.Name = "txtMovieEFanartsLimit"
        Me.txtMovieEFanartsLimit.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieEFanartsLimit.TabIndex = 12
        '
        'gbMovieImagesFanartOpts
        '
        Me.gbMovieImagesFanartOpts.AutoSize = true
        Me.gbMovieImagesFanartOpts.Controls.Add(Me.tblMovieImagesFanartOpts)
        Me.gbMovieImagesFanartOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieImagesFanartOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesFanartOpts.Location = New System.Drawing.Point(577, 53)
        Me.gbMovieImagesFanartOpts.Name = "gbMovieImagesFanartOpts"
        Me.gbMovieImagesFanartOpts.Size = New System.Drawing.Size(281, 142)
        Me.gbMovieImagesFanartOpts.TabIndex = 3
        Me.gbMovieImagesFanartOpts.TabStop = false
        Me.gbMovieImagesFanartOpts.Text = "Fanart"
        '
        'tblMovieImagesFanartOpts
        '
        Me.tblMovieImagesFanartOpts.AutoSize = true
        Me.tblMovieImagesFanartOpts.ColumnCount = 5
        Me.tblMovieImagesFanartOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesFanartOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesFanartOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesFanartOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesFanartOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieImagesFanartOpts.Controls.Add(Me.txtMovieFanartHeight, 3, 4)
        Me.tblMovieImagesFanartOpts.Controls.Add(Me.txtMovieFanartWidth, 1, 4)
        Me.tblMovieImagesFanartOpts.Controls.Add(Me.lblMovieFanartHeight, 2, 4)
        Me.tblMovieImagesFanartOpts.Controls.Add(Me.chkMovieFanartPrefOnly, 2, 1)
        Me.tblMovieImagesFanartOpts.Controls.Add(Me.lblMovieFanartSize, 0, 0)
        Me.tblMovieImagesFanartOpts.Controls.Add(Me.cbMovieFanartPrefSize, 0, 1)
        Me.tblMovieImagesFanartOpts.Controls.Add(Me.chkMovieFanartOverwrite, 0, 2)
        Me.tblMovieImagesFanartOpts.Controls.Add(Me.lblMovieFanartWidth, 0, 4)
        Me.tblMovieImagesFanartOpts.Controls.Add(Me.chkMovieFanartResize, 0, 3)
        Me.tblMovieImagesFanartOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieImagesFanartOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieImagesFanartOpts.Name = "tblMovieImagesFanartOpts"
        Me.tblMovieImagesFanartOpts.RowCount = 6
        Me.tblMovieImagesFanartOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblMovieImagesFanartOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesFanartOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesFanartOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesFanartOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesFanartOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieImagesFanartOpts.Size = New System.Drawing.Size(275, 121)
        Me.tblMovieImagesFanartOpts.TabIndex = 17
        '
        'txtMovieFanartHeight
        '
        Me.txtMovieFanartHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieFanartHeight.Enabled = false
        Me.txtMovieFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieFanartHeight.Location = New System.Drawing.Point(232, 96)
        Me.txtMovieFanartHeight.Name = "txtMovieFanartHeight"
        Me.txtMovieFanartHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieFanartHeight.TabIndex = 8
        '
        'txtMovieFanartWidth
        '
        Me.txtMovieFanartWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieFanartWidth.Enabled = false
        Me.txtMovieFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieFanartWidth.Location = New System.Drawing.Point(75, 96)
        Me.txtMovieFanartWidth.Name = "txtMovieFanartWidth"
        Me.txtMovieFanartWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieFanartWidth.TabIndex = 6
        '
        'lblMovieFanartHeight
        '
        Me.lblMovieFanartHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieFanartHeight.AutoSize = true
        Me.lblMovieFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieFanartHeight.Location = New System.Drawing.Point(157, 100)
        Me.lblMovieFanartHeight.Name = "lblMovieFanartHeight"
        Me.lblMovieFanartHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblMovieFanartHeight.TabIndex = 7
        Me.lblMovieFanartHeight.Text = "Max Height:"
        '
        'chkMovieFanartPrefOnly
        '
        Me.chkMovieFanartPrefOnly.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieFanartPrefOnly.AutoSize = true
        Me.tblMovieImagesFanartOpts.SetColumnSpan(Me.chkMovieFanartPrefOnly, 2)
        Me.chkMovieFanartPrefOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieFanartPrefOnly.Location = New System.Drawing.Point(157, 25)
        Me.chkMovieFanartPrefOnly.Name = "chkMovieFanartPrefOnly"
        Me.chkMovieFanartPrefOnly.Size = New System.Drawing.Size(50, 17)
        Me.chkMovieFanartPrefOnly.TabIndex = 2
        Me.chkMovieFanartPrefOnly.Text = "Only"
        Me.chkMovieFanartPrefOnly.UseVisualStyleBackColor = true
        '
        'lblMovieFanartSize
        '
        Me.lblMovieFanartSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieFanartSize.AutoSize = true
        Me.tblMovieImagesFanartOpts.SetColumnSpan(Me.lblMovieFanartSize, 4)
        Me.lblMovieFanartSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieFanartSize.Location = New System.Drawing.Point(3, 3)
        Me.lblMovieFanartSize.Name = "lblMovieFanartSize"
        Me.lblMovieFanartSize.Size = New System.Drawing.Size(80, 13)
        Me.lblMovieFanartSize.TabIndex = 0
        Me.lblMovieFanartSize.Text = "Preferred Size:"
        '
        'cbMovieFanartPrefSize
        '
        Me.cbMovieFanartPrefSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblMovieImagesFanartOpts.SetColumnSpan(Me.cbMovieFanartPrefSize, 2)
        Me.cbMovieFanartPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieFanartPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieFanartPrefSize.FormattingEnabled = true
        Me.cbMovieFanartPrefSize.Location = New System.Drawing.Point(3, 23)
        Me.cbMovieFanartPrefSize.Name = "cbMovieFanartPrefSize"
        Me.cbMovieFanartPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbMovieFanartPrefSize.TabIndex = 1
        '
        'chkMovieFanartOverwrite
        '
        Me.chkMovieFanartOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieFanartOverwrite.AutoSize = true
        Me.tblMovieImagesFanartOpts.SetColumnSpan(Me.chkMovieFanartOverwrite, 4)
        Me.chkMovieFanartOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieFanartOverwrite.Location = New System.Drawing.Point(3, 50)
        Me.chkMovieFanartOverwrite.Name = "chkMovieFanartOverwrite"
        Me.chkMovieFanartOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieFanartOverwrite.TabIndex = 3
        Me.chkMovieFanartOverwrite.Text = "Overwrite Existing"
        Me.chkMovieFanartOverwrite.UseVisualStyleBackColor = true
        '
        'lblMovieFanartWidth
        '
        Me.lblMovieFanartWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieFanartWidth.AutoSize = true
        Me.lblMovieFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieFanartWidth.Location = New System.Drawing.Point(3, 100)
        Me.lblMovieFanartWidth.Name = "lblMovieFanartWidth"
        Me.lblMovieFanartWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblMovieFanartWidth.TabIndex = 5
        Me.lblMovieFanartWidth.Text = "Max Width:"
        '
        'chkMovieFanartResize
        '
        Me.chkMovieFanartResize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieFanartResize.AutoSize = true
        Me.tblMovieImagesFanartOpts.SetColumnSpan(Me.chkMovieFanartResize, 4)
        Me.chkMovieFanartResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieFanartResize.Location = New System.Drawing.Point(3, 73)
        Me.chkMovieFanartResize.Name = "chkMovieFanartResize"
        Me.chkMovieFanartResize.Size = New System.Drawing.Size(133, 17)
        Me.chkMovieFanartResize.TabIndex = 4
        Me.chkMovieFanartResize.Text = "Automatically Resize:"
        Me.chkMovieFanartResize.UseVisualStyleBackColor = true
        '
        'clbMovieGenre
        '
        Me.clbMovieGenre.CheckOnClick = true
        Me.clbMovieGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.clbMovieGenre.FormattingEnabled = true
        Me.clbMovieGenre.IntegralHeight = false
        Me.clbMovieGenre.Location = New System.Drawing.Point(10, 18)
        Me.clbMovieGenre.Name = "clbMovieGenre"
        Me.clbMovieGenre.Size = New System.Drawing.Size(157, 80)
        Me.clbMovieGenre.Sorted = true
        Me.clbMovieGenre.TabIndex = 0
        '
        'gbMovieGeneralMediaListOpts
        '
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieMoviesetCol)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieDiscArtCol)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieClearLogoCol)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieClearArtCol)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieBannerCol)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieThemeCol)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieWatchedCol)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieEFanartsCol)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieLandscapeCol)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.txtMovieLevTolerance)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.lblMovieLevTolerance)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieLevTolerance)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieSubCol)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMoviePosterCol)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.gbMovieSortTokensOpts)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieDisplayYear)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieEThumbsCol)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieTrailerCol)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieNFOCol)
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieFanartCol)
        Me.gbMovieGeneralMediaListOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieGeneralMediaListOpts.Location = New System.Drawing.Point(3, 80)
        Me.gbMovieGeneralMediaListOpts.Name = "gbMovieGeneralMediaListOpts"
        Me.gbMovieGeneralMediaListOpts.Size = New System.Drawing.Size(219, 417)
        Me.gbMovieGeneralMediaListOpts.TabIndex = 4
        Me.gbMovieGeneralMediaListOpts.TabStop = false
        Me.gbMovieGeneralMediaListOpts.Text = "Media List Options"
        '
        'chkMovieMoviesetCol
        '
        Me.chkMovieMoviesetCol.AutoSize = true
        Me.chkMovieMoviesetCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieMoviesetCol.Location = New System.Drawing.Point(6, 303)
        Me.chkMovieMoviesetCol.Name = "chkMovieMoviesetCol"
        Me.chkMovieMoviesetCol.Size = New System.Drawing.Size(143, 17)
        Me.chkMovieMoviesetCol.TabIndex = 83
        Me.chkMovieMoviesetCol.Text = "Hide MovieSet Column"
        Me.chkMovieMoviesetCol.UseVisualStyleBackColor = true
        '
        'chkMovieDiscArtCol
        '
        Me.chkMovieDiscArtCol.AutoSize = true
        Me.chkMovieDiscArtCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieDiscArtCol.Location = New System.Drawing.Point(6, 222)
        Me.chkMovieDiscArtCol.Name = "chkMovieDiscArtCol"
        Me.chkMovieDiscArtCol.Size = New System.Drawing.Size(132, 17)
        Me.chkMovieDiscArtCol.TabIndex = 82
        Me.chkMovieDiscArtCol.Text = "Hide DiscArt Column"
        Me.chkMovieDiscArtCol.UseVisualStyleBackColor = true
        '
        'chkMovieClearLogoCol
        '
        Me.chkMovieClearLogoCol.AutoSize = true
        Me.chkMovieClearLogoCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieClearLogoCol.Location = New System.Drawing.Point(6, 206)
        Me.chkMovieClearLogoCol.Name = "chkMovieClearLogoCol"
        Me.chkMovieClearLogoCol.Size = New System.Drawing.Size(148, 17)
        Me.chkMovieClearLogoCol.TabIndex = 81
        Me.chkMovieClearLogoCol.Text = "Hide ClearLogo Column"
        Me.chkMovieClearLogoCol.UseVisualStyleBackColor = true
        '
        'chkMovieClearArtCol
        '
        Me.chkMovieClearArtCol.AutoSize = true
        Me.chkMovieClearArtCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieClearArtCol.Location = New System.Drawing.Point(6, 190)
        Me.chkMovieClearArtCol.Name = "chkMovieClearArtCol"
        Me.chkMovieClearArtCol.Size = New System.Drawing.Size(137, 17)
        Me.chkMovieClearArtCol.TabIndex = 80
        Me.chkMovieClearArtCol.Text = "Hide ClearArt Column"
        Me.chkMovieClearArtCol.UseVisualStyleBackColor = true
        '
        'chkMovieBannerCol
        '
        Me.chkMovieBannerCol.AutoSize = true
        Me.chkMovieBannerCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieBannerCol.Location = New System.Drawing.Point(6, 173)
        Me.chkMovieBannerCol.Name = "chkMovieBannerCol"
        Me.chkMovieBannerCol.Size = New System.Drawing.Size(133, 17)
        Me.chkMovieBannerCol.TabIndex = 79
        Me.chkMovieBannerCol.Text = "Hide Banner Column"
        Me.chkMovieBannerCol.UseVisualStyleBackColor = true
        '
        'chkMovieThemeCol
        '
        Me.chkMovieThemeCol.AutoSize = true
        Me.chkMovieThemeCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieThemeCol.Location = New System.Drawing.Point(6, 367)
        Me.chkMovieThemeCol.Name = "chkMovieThemeCol"
        Me.chkMovieThemeCol.Size = New System.Drawing.Size(129, 17)
        Me.chkMovieThemeCol.TabIndex = 78
        Me.chkMovieThemeCol.Text = "Hide Theme Column"
        Me.chkMovieThemeCol.UseVisualStyleBackColor = true
        '
        'chkMovieWatchedCol
        '
        Me.chkMovieWatchedCol.AutoSize = true
        Me.chkMovieWatchedCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieWatchedCol.Location = New System.Drawing.Point(6, 400)
        Me.chkMovieWatchedCol.Name = "chkMovieWatchedCol"
        Me.chkMovieWatchedCol.Size = New System.Drawing.Size(142, 17)
        Me.chkMovieWatchedCol.TabIndex = 76
        Me.chkMovieWatchedCol.Text = "Hide Watched Column"
        Me.chkMovieWatchedCol.UseVisualStyleBackColor = true
        '
        'chkMovieEFanartsCol
        '
        Me.chkMovieEFanartsCol.AutoSize = true
        Me.chkMovieEFanartsCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieEFanartsCol.Location = New System.Drawing.Point(6, 238)
        Me.chkMovieEFanartsCol.Name = "chkMovieEFanartsCol"
        Me.chkMovieEFanartsCol.Size = New System.Drawing.Size(152, 17)
        Me.chkMovieEFanartsCol.TabIndex = 75
        Me.chkMovieEFanartsCol.Text = "Hide Extrafanart Column"
        Me.chkMovieEFanartsCol.UseVisualStyleBackColor = true
        '
        'chkMovieLandscapeCol
        '
        Me.chkMovieLandscapeCol.AutoSize = true
        Me.chkMovieLandscapeCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLandscapeCol.Location = New System.Drawing.Point(6, 287)
        Me.chkMovieLandscapeCol.Name = "chkMovieLandscapeCol"
        Me.chkMovieLandscapeCol.Size = New System.Drawing.Size(150, 17)
        Me.chkMovieLandscapeCol.TabIndex = 77
        Me.chkMovieLandscapeCol.Text = "Hide Landscape Column"
        Me.chkMovieLandscapeCol.UseVisualStyleBackColor = true
        '
        'txtMovieLevTolerance
        '
        Me.txtMovieLevTolerance.Enabled = false
        Me.txtMovieLevTolerance.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieLevTolerance.Location = New System.Drawing.Point(144, 52)
        Me.txtMovieLevTolerance.Name = "txtMovieLevTolerance"
        Me.txtMovieLevTolerance.Size = New System.Drawing.Size(61, 22)
        Me.txtMovieLevTolerance.TabIndex = 74
        '
        'lblMovieLevTolerance
        '
        Me.lblMovieLevTolerance.AutoSize = true
        Me.lblMovieLevTolerance.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieLevTolerance.Location = New System.Drawing.Point(27, 57)
        Me.lblMovieLevTolerance.Name = "lblMovieLevTolerance"
        Me.lblMovieLevTolerance.Size = New System.Drawing.Size(111, 13)
        Me.lblMovieLevTolerance.TabIndex = 73
        Me.lblMovieLevTolerance.Text = "Mismatch Tolerance:"
        Me.lblMovieLevTolerance.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkMovieLevTolerance
        '
        Me.chkMovieLevTolerance.AutoSize = true
        Me.chkMovieLevTolerance.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLevTolerance.Location = New System.Drawing.Point(8, 35)
        Me.chkMovieLevTolerance.Name = "chkMovieLevTolerance"
        Me.chkMovieLevTolerance.Size = New System.Drawing.Size(178, 17)
        Me.chkMovieLevTolerance.TabIndex = 72
        Me.chkMovieLevTolerance.Text = "Check Title Match Confidence"
        Me.chkMovieLevTolerance.UseVisualStyleBackColor = true
        '
        'chkMovieSubCol
        '
        Me.chkMovieSubCol.AutoSize = true
        Me.chkMovieSubCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSubCol.Location = New System.Drawing.Point(6, 351)
        Me.chkMovieSubCol.Name = "chkMovieSubCol"
        Me.chkMovieSubCol.Size = New System.Drawing.Size(136, 17)
        Me.chkMovieSubCol.TabIndex = 4
        Me.chkMovieSubCol.Text = "Hide Subtitle Column"
        Me.chkMovieSubCol.UseVisualStyleBackColor = true
        '
        'chkMoviePosterCol
        '
        Me.chkMoviePosterCol.AutoSize = true
        Me.chkMoviePosterCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMoviePosterCol.Location = New System.Drawing.Point(6, 335)
        Me.chkMoviePosterCol.Name = "chkMoviePosterCol"
        Me.chkMoviePosterCol.Size = New System.Drawing.Size(128, 17)
        Me.chkMoviePosterCol.TabIndex = 0
        Me.chkMoviePosterCol.Text = "Hide Poster Column"
        Me.chkMoviePosterCol.UseVisualStyleBackColor = true
        '
        'gbMovieSortTokensOpts
        '
        Me.gbMovieSortTokensOpts.Controls.Add(Me.btnMovieSortTokenRemove)
        Me.gbMovieSortTokensOpts.Controls.Add(Me.btnMovieSortTokenAdd)
        Me.gbMovieSortTokensOpts.Controls.Add(Me.txtMovieSortToken)
        Me.gbMovieSortTokensOpts.Controls.Add(Me.lstMovieSortTokens)
        Me.gbMovieSortTokensOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieSortTokensOpts.Location = New System.Drawing.Point(8, 77)
        Me.gbMovieSortTokensOpts.Name = "gbMovieSortTokensOpts"
        Me.gbMovieSortTokensOpts.Size = New System.Drawing.Size(200, 93)
        Me.gbMovieSortTokensOpts.TabIndex = 71
        Me.gbMovieSortTokensOpts.TabStop = false
        Me.gbMovieSortTokensOpts.Text = "Sort Tokens to Ignore"
        '
        'btnMovieSortTokenRemove
        '
        Me.btnMovieSortTokenRemove.Image = CType(resources.GetObject("btnMovieSortTokenRemove.Image"),System.Drawing.Image)
        Me.btnMovieSortTokenRemove.Location = New System.Drawing.Point(168, 64)
        Me.btnMovieSortTokenRemove.Name = "btnMovieSortTokenRemove"
        Me.btnMovieSortTokenRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieSortTokenRemove.TabIndex = 3
        Me.btnMovieSortTokenRemove.UseVisualStyleBackColor = true
        '
        'btnMovieSortTokenAdd
        '
        Me.btnMovieSortTokenAdd.Image = CType(resources.GetObject("btnMovieSortTokenAdd.Image"),System.Drawing.Image)
        Me.btnMovieSortTokenAdd.Location = New System.Drawing.Point(72, 64)
        Me.btnMovieSortTokenAdd.Name = "btnMovieSortTokenAdd"
        Me.btnMovieSortTokenAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieSortTokenAdd.TabIndex = 2
        Me.btnMovieSortTokenAdd.UseVisualStyleBackColor = true
        '
        'txtMovieSortToken
        '
        Me.txtMovieSortToken.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieSortToken.Location = New System.Drawing.Point(10, 64)
        Me.txtMovieSortToken.Name = "txtMovieSortToken"
        Me.txtMovieSortToken.Size = New System.Drawing.Size(61, 22)
        Me.txtMovieSortToken.TabIndex = 1
        '
        'lstMovieSortTokens
        '
        Me.lstMovieSortTokens.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstMovieSortTokens.FormattingEnabled = true
        Me.lstMovieSortTokens.Location = New System.Drawing.Point(10, 15)
        Me.lstMovieSortTokens.Name = "lstMovieSortTokens"
        Me.lstMovieSortTokens.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstMovieSortTokens.Size = New System.Drawing.Size(180, 43)
        Me.lstMovieSortTokens.Sorted = true
        Me.lstMovieSortTokens.TabIndex = 0
        '
        'chkMovieDisplayYear
        '
        Me.chkMovieDisplayYear.AutoSize = true
        Me.chkMovieDisplayYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieDisplayYear.Location = New System.Drawing.Point(8, 19)
        Me.chkMovieDisplayYear.Name = "chkMovieDisplayYear"
        Me.chkMovieDisplayYear.Size = New System.Drawing.Size(144, 17)
        Me.chkMovieDisplayYear.TabIndex = 70
        Me.chkMovieDisplayYear.Text = "Display Year in List Title"
        Me.chkMovieDisplayYear.UseVisualStyleBackColor = true
        '
        'chkMovieEThumbsCol
        '
        Me.chkMovieEThumbsCol.AutoSize = true
        Me.chkMovieEThumbsCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieEThumbsCol.Location = New System.Drawing.Point(6, 255)
        Me.chkMovieEThumbsCol.Name = "chkMovieEThumbsCol"
        Me.chkMovieEThumbsCol.Size = New System.Drawing.Size(155, 17)
        Me.chkMovieEThumbsCol.TabIndex = 5
        Me.chkMovieEThumbsCol.Text = "Hide Extrathumb Column"
        Me.chkMovieEThumbsCol.UseVisualStyleBackColor = true
        '
        'chkMovieTrailerCol
        '
        Me.chkMovieTrailerCol.AutoSize = true
        Me.chkMovieTrailerCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieTrailerCol.Location = New System.Drawing.Point(6, 383)
        Me.chkMovieTrailerCol.Name = "chkMovieTrailerCol"
        Me.chkMovieTrailerCol.Size = New System.Drawing.Size(127, 17)
        Me.chkMovieTrailerCol.TabIndex = 3
        Me.chkMovieTrailerCol.Text = "Hide Trailer Column"
        Me.chkMovieTrailerCol.UseVisualStyleBackColor = true
        '
        'chkMovieNFOCol
        '
        Me.chkMovieNFOCol.AutoSize = true
        Me.chkMovieNFOCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieNFOCol.Location = New System.Drawing.Point(6, 319)
        Me.chkMovieNFOCol.Name = "chkMovieNFOCol"
        Me.chkMovieNFOCol.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieNFOCol.TabIndex = 2
        Me.chkMovieNFOCol.Text = "Hide NFO Column"
        Me.chkMovieNFOCol.UseVisualStyleBackColor = true
        '
        'chkMovieFanartCol
        '
        Me.chkMovieFanartCol.AutoSize = true
        Me.chkMovieFanartCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieFanartCol.Location = New System.Drawing.Point(6, 272)
        Me.chkMovieFanartCol.Name = "chkMovieFanartCol"
        Me.chkMovieFanartCol.Size = New System.Drawing.Size(129, 17)
        Me.chkMovieFanartCol.TabIndex = 1
        Me.chkMovieFanartCol.Text = "Hide Fanart Column"
        Me.chkMovieFanartCol.UseVisualStyleBackColor = true
        '
        'lvMovieSources
        '
        Me.lvMovieSources.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colID, Me.colName, Me.colPath, Me.colRecur, Me.colFolder, Me.colSingle, Me.colExclude})
        Me.lvMovieSources.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lvMovieSources.FullRowSelect = true
        Me.lvMovieSources.HideSelection = false
        Me.lvMovieSources.Location = New System.Drawing.Point(5, 6)
        Me.lvMovieSources.Name = "lvMovieSources"
        Me.lvMovieSources.Size = New System.Drawing.Size(627, 105)
        Me.lvMovieSources.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvMovieSources.TabIndex = 0
        Me.lvMovieSources.UseCompatibleStateImageBehavior = false
        Me.lvMovieSources.View = System.Windows.Forms.View.Details
        '
        'colID
        '
        Me.colID.Width = 0
        '
        'colName
        '
        Me.colName.Text = "Name"
        Me.colName.Width = 100
        '
        'colPath
        '
        Me.colPath.Text = "Path"
        Me.colPath.Width = 200
        '
        'colRecur
        '
        Me.colRecur.Text = "Recursive"
        '
        'colFolder
        '
        Me.colFolder.Text = "Use Folder Name"
        Me.colFolder.Width = 110
        '
        'colSingle
        '
        Me.colSingle.Text = "Single Video"
        Me.colSingle.Width = 90
        '
        'colExclude
        '
        Me.colExclude.Text = "Exclude"
        '
        'btnMovieSourceRemove
        '
        Me.btnMovieSourceRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnMovieSourceRemove.Image = CType(resources.GetObject("btnMovieSourceRemove.Image"),System.Drawing.Image)
        Me.btnMovieSourceRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieSourceRemove.Location = New System.Drawing.Point(638, 88)
        Me.btnMovieSourceRemove.Name = "btnMovieSourceRemove"
        Me.btnMovieSourceRemove.Size = New System.Drawing.Size(104, 23)
        Me.btnMovieSourceRemove.TabIndex = 3
        Me.btnMovieSourceRemove.Text = "Remove"
        Me.btnMovieSourceRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieSourceRemove.UseVisualStyleBackColor = true
        '
        'btnMovieSourceAdd
        '
        Me.btnMovieSourceAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnMovieSourceAdd.Image = CType(resources.GetObject("btnMovieSourceAdd.Image"),System.Drawing.Image)
        Me.btnMovieSourceAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieSourceAdd.Location = New System.Drawing.Point(638, 6)
        Me.btnMovieSourceAdd.Name = "btnMovieSourceAdd"
        Me.btnMovieSourceAdd.Size = New System.Drawing.Size(104, 23)
        Me.btnMovieSourceAdd.TabIndex = 1
        Me.btnMovieSourceAdd.Text = "Add Source"
        Me.btnMovieSourceAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieSourceAdd.UseVisualStyleBackColor = true
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnOK.Location = New System.Drawing.Point(923, 41)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = true
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnApply.Enabled = false
        Me.btnApply.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnApply.Location = New System.Drawing.Point(763, 41)
        Me.btnApply.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(74, 23)
        Me.btnApply.TabIndex = 2
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = true
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnCancel.Location = New System.Drawing.Point(843, 41)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(74, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = true
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.SteelBlue
        Me.pnlSettingsTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSettingsTop.Controls.Add(Me.lblSettingsTopDetails)
        Me.pnlSettingsTop.Controls.Add(Me.lblSettingsTopTitle)
        Me.pnlSettingsTop.Controls.Add(Me.pbSettingsTopLogo)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(1008, 64)
        Me.pnlSettingsTop.TabIndex = 3
        '
        'lblSettingsTopDetails
        '
        Me.lblSettingsTopDetails.AutoSize = true
        Me.lblSettingsTopDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblSettingsTopDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblSettingsTopDetails.ForeColor = System.Drawing.Color.White
        Me.lblSettingsTopDetails.Location = New System.Drawing.Point(61, 38)
        Me.lblSettingsTopDetails.Name = "lblSettingsTopDetails"
        Me.lblSettingsTopDetails.Size = New System.Drawing.Size(245, 13)
        Me.lblSettingsTopDetails.TabIndex = 1
        Me.lblSettingsTopDetails.Text = "Configure Ember's appearance and operation."
        '
        'lblSettingsTopTitle
        '
        Me.lblSettingsTopTitle.AutoSize = true
        Me.lblSettingsTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblSettingsTopTitle.Font = New System.Drawing.Font("Segoe UI", 18!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblSettingsTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblSettingsTopTitle.Location = New System.Drawing.Point(58, 3)
        Me.lblSettingsTopTitle.Name = "lblSettingsTopTitle"
        Me.lblSettingsTopTitle.Size = New System.Drawing.Size(107, 32)
        Me.lblSettingsTopTitle.TabIndex = 0
        Me.lblSettingsTopTitle.Text = "Settings"
        '
        'pbSettingsTopLogo
        '
        Me.pbSettingsTopLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbSettingsTopLogo.Image = CType(resources.GetObject("pbSettingsTopLogo.Image"),System.Drawing.Image)
        Me.pbSettingsTopLogo.Location = New System.Drawing.Point(7, 8)
        Me.pbSettingsTopLogo.Name = "pbSettingsTopLogo"
        Me.pbSettingsTopLogo.Size = New System.Drawing.Size(48, 48)
        Me.pbSettingsTopLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbSettingsTopLogo.TabIndex = 0
        Me.pbSettingsTopLogo.TabStop = false
        '
        'ilSettings
        '
        Me.ilSettings.ImageStream = CType(resources.GetObject("ilSettings.ImageStream"),System.Windows.Forms.ImageListStreamer)
        Me.ilSettings.TransparentColor = System.Drawing.Color.Transparent
        Me.ilSettings.Images.SetKeyName(0, "process.png")
        Me.ilSettings.Images.SetKeyName(1, "comments.png")
        Me.ilSettings.Images.SetKeyName(2, "film.png")
        Me.ilSettings.Images.SetKeyName(3, "copy_paste.png")
        Me.ilSettings.Images.SetKeyName(4, "attachment.png")
        Me.ilSettings.Images.SetKeyName(5, "folder_full.png")
        Me.ilSettings.Images.SetKeyName(6, "image.png")
        Me.ilSettings.Images.SetKeyName(7, "television.ico")
        Me.ilSettings.Images.SetKeyName(8, "favorite_film.png")
        Me.ilSettings.Images.SetKeyName(9, "settingscheck.png")
        Me.ilSettings.Images.SetKeyName(10, "settingsx.png")
        Me.ilSettings.Images.SetKeyName(11, "note.png")
        '
        'tvSettingsList
        '
        Me.tvSettingsList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tvSettingsList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvSettingsList.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tvSettingsList.FullRowSelect = true
        Me.tvSettingsList.HideSelection = false
        Me.tvSettingsList.ImageIndex = 0
        Me.tvSettingsList.ImageList = Me.ilSettings
        Me.tvSettingsList.Location = New System.Drawing.Point(5, 0)
        Me.tvSettingsList.Name = "tvSettingsList"
        Me.tvSettingsList.SelectedImageIndex = 0
        Me.tvSettingsList.ShowLines = false
        Me.tvSettingsList.ShowPlusMinus = false
        Me.tvSettingsList.Size = New System.Drawing.Size(242, 502)
        Me.tvSettingsList.TabIndex = 7
        '
        'pnlGeneral
        '
        Me.pnlGeneral.AutoSize = true
        Me.pnlGeneral.BackColor = System.Drawing.Color.White
        Me.pnlGeneral.Controls.Add(Me.tblGeneral)
        Me.pnlGeneral.Location = New System.Drawing.Point(900, 900)
        Me.pnlGeneral.Name = "pnlGeneral"
        Me.pnlGeneral.Size = New System.Drawing.Size(750, 500)
        Me.pnlGeneral.TabIndex = 10
        Me.pnlGeneral.Visible = false
        '
        'tblGeneral
        '
        Me.tblGeneral.AutoScroll = true
        Me.tblGeneral.AutoSize = true
        Me.tblGeneral.ColumnCount = 3
        Me.tblGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneral.Controls.Add(Me.gbGeneralInterface, 0, 0)
        Me.tblGeneral.Controls.Add(Me.gbGeneralDateAdded, 0, 2)
        Me.tblGeneral.Controls.Add(Me.gbGeneralMainWindow, 1, 0)
        Me.tblGeneral.Controls.Add(Me.gbGeneralMisc, 1, 1)
        Me.tblGeneral.Controls.Add(Me.gbGeneralScrapers, 0, 1)
        Me.tblGeneral.Controls.Add(Me.gbGeneralDaemon, 0, 3)
        Me.tblGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneral.Location = New System.Drawing.Point(0, 0)
        Me.tblGeneral.Name = "tblGeneral"
        Me.tblGeneral.RowCount = 5
        Me.tblGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneral.Size = New System.Drawing.Size(750, 500)
        Me.tblGeneral.TabIndex = 17
        '
        'gbGeneralInterface
        '
        Me.gbGeneralInterface.AutoSize = true
        Me.gbGeneralInterface.Controls.Add(Me.tblGeneralInterface)
        Me.gbGeneralInterface.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGeneralInterface.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbGeneralInterface.Location = New System.Drawing.Point(3, 3)
        Me.gbGeneralInterface.Name = "gbGeneralInterface"
        Me.gbGeneralInterface.Size = New System.Drawing.Size(238, 283)
        Me.gbGeneralInterface.TabIndex = 0
        Me.gbGeneralInterface.TabStop = false
        Me.gbGeneralInterface.Text = "Interface"
        '
        'tblGeneralInterface
        '
        Me.tblGeneralInterface.AutoSize = true
        Me.tblGeneralInterface.ColumnCount = 2
        Me.tblGeneralInterface.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralInterface.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralInterface.Controls.Add(Me.gbGeneralThemes, 0, 2)
        Me.tblGeneralInterface.Controls.Add(Me.lblGeneralntLang, 0, 0)
        Me.tblGeneralInterface.Controls.Add(Me.cbGeneralLanguage, 0, 1)
        Me.tblGeneralInterface.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralInterface.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralInterface.Name = "tblGeneralInterface"
        Me.tblGeneralInterface.RowCount = 4
        Me.tblGeneralInterface.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblGeneralInterface.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralInterface.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralInterface.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralInterface.Size = New System.Drawing.Size(232, 262)
        Me.tblGeneralInterface.TabIndex = 17
        '
        'gbGeneralDateAdded
        '
        Me.gbGeneralDateAdded.AutoSize = true
        Me.gbGeneralDateAdded.Controls.Add(Me.tblGeneralDateAdded)
        Me.gbGeneralDateAdded.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGeneralDateAdded.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbGeneralDateAdded.Location = New System.Drawing.Point(3, 342)
        Me.gbGeneralDateAdded.Name = "gbGeneralDateAdded"
        Me.gbGeneralDateAdded.Size = New System.Drawing.Size(238, 71)
        Me.gbGeneralDateAdded.TabIndex = 16
        Me.gbGeneralDateAdded.TabStop = false
        Me.gbGeneralDateAdded.Text = "Adding Date"
        '
        'tblGeneralDateAdded
        '
        Me.tblGeneralDateAdded.AutoSize = true
        Me.tblGeneralDateAdded.ColumnCount = 2
        Me.tblGeneralDateAdded.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralDateAdded.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralDateAdded.Controls.Add(Me.chkGeneralDateAddedIgnoreNFO, 0, 1)
        Me.tblGeneralDateAdded.Controls.Add(Me.cbGeneralDateTime, 0, 0)
        Me.tblGeneralDateAdded.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralDateAdded.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralDateAdded.Name = "tblGeneralDateAdded"
        Me.tblGeneralDateAdded.RowCount = 3
        Me.tblGeneralDateAdded.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralDateAdded.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralDateAdded.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralDateAdded.Size = New System.Drawing.Size(232, 50)
        Me.tblGeneralDateAdded.TabIndex = 17
        '
        'cbGeneralDateTime
        '
        Me.cbGeneralDateTime.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbGeneralDateTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGeneralDateTime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbGeneralDateTime.FormattingEnabled = true
        Me.cbGeneralDateTime.Location = New System.Drawing.Point(3, 3)
        Me.cbGeneralDateTime.Name = "cbGeneralDateTime"
        Me.cbGeneralDateTime.Size = New System.Drawing.Size(226, 21)
        Me.cbGeneralDateTime.TabIndex = 11
        '
        'gbGeneralMainWindow
        '
        Me.gbGeneralMainWindow.AutoSize = true
        Me.gbGeneralMainWindow.Controls.Add(Me.tblGeneralMainWindow)
        Me.gbGeneralMainWindow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGeneralMainWindow.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbGeneralMainWindow.Location = New System.Drawing.Point(247, 3)
        Me.gbGeneralMainWindow.Name = "gbGeneralMainWindow"
        Me.gbGeneralMainWindow.Size = New System.Drawing.Size(359, 283)
        Me.gbGeneralMainWindow.TabIndex = 14
        Me.gbGeneralMainWindow.TabStop = false
        Me.gbGeneralMainWindow.Text = "Main Window"
        '
        'tblGeneralMainWindow
        '
        Me.tblGeneralMainWindow.AutoSize = true
        Me.tblGeneralMainWindow.ColumnCount = 3
        Me.tblGeneralMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralHidePoster, 1, 7)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralHideLandscape, 1, 6)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralShowImgDims, 0, 3)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralHideFanartSmall, 1, 5)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralHideDiscArt, 0, 8)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralShowImgNames, 1, 3)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralHideFanart, 1, 4)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralHideClearArt, 0, 6)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDoubleClickScrape, 0, 0)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralHideClearLogo, 0, 7)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralInfoPanelAnim, 0, 1)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralHideBanner, 0, 4)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralShowGenresText, 0, 2)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralImagesGlassOverlay, 1, 1)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralHideCharacterArt, 0, 5)
        Me.tblGeneralMainWindow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralMainWindow.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralMainWindow.Name = "tblGeneralMainWindow"
        Me.tblGeneralMainWindow.RowCount = 10
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.Size = New System.Drawing.Size(353, 262)
        Me.tblGeneralMainWindow.TabIndex = 17
        '
        'chkGeneralDoubleClickScrape
        '
        Me.chkGeneralDoubleClickScrape.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDoubleClickScrape.AutoSize = true
        Me.tblGeneralMainWindow.SetColumnSpan(Me.chkGeneralDoubleClickScrape, 2)
        Me.chkGeneralDoubleClickScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralDoubleClickScrape.Location = New System.Drawing.Point(3, 3)
        Me.chkGeneralDoubleClickScrape.Name = "chkGeneralDoubleClickScrape"
        Me.chkGeneralDoubleClickScrape.Size = New System.Drawing.Size(250, 17)
        Me.chkGeneralDoubleClickScrape.TabIndex = 19
        Me.chkGeneralDoubleClickScrape.Text = "Enable Image Scrape On Double Right Click"
        Me.chkGeneralDoubleClickScrape.UseVisualStyleBackColor = true
        '
        'chkGeneralHideLandscape
        '
        Me.chkGeneralHideLandscape.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralHideLandscape.AutoSize = true
        Me.chkGeneralHideLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralHideLandscape.Location = New System.Drawing.Point(179, 141)
        Me.chkGeneralHideLandscape.Name = "chkGeneralHideLandscape"
        Me.chkGeneralHideLandscape.Size = New System.Drawing.Size(160, 17)
        Me.chkGeneralHideLandscape.TabIndex = 18
        Me.chkGeneralHideLandscape.Text = "Do Not Display Landscape"
        Me.chkGeneralHideLandscape.UseVisualStyleBackColor = true
        '
        'chkGeneralHideDiscArt
        '
        Me.chkGeneralHideDiscArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralHideDiscArt.AutoSize = true
        Me.chkGeneralHideDiscArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralHideDiscArt.Location = New System.Drawing.Point(3, 187)
        Me.chkGeneralHideDiscArt.Name = "chkGeneralHideDiscArt"
        Me.chkGeneralHideDiscArt.Size = New System.Drawing.Size(142, 17)
        Me.chkGeneralHideDiscArt.TabIndex = 17
        Me.chkGeneralHideDiscArt.Text = "Do Not Display DiscArt"
        Me.chkGeneralHideDiscArt.UseVisualStyleBackColor = true
        '
        'chkGeneralShowImgNames
        '
        Me.chkGeneralShowImgNames.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralShowImgNames.AutoSize = true
        Me.chkGeneralShowImgNames.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralShowImgNames.Location = New System.Drawing.Point(179, 72)
        Me.chkGeneralShowImgNames.Name = "chkGeneralShowImgNames"
        Me.chkGeneralShowImgNames.Size = New System.Drawing.Size(134, 17)
        Me.chkGeneralShowImgNames.TabIndex = 20
        Me.chkGeneralShowImgNames.Text = "Display Image Names"
        Me.chkGeneralShowImgNames.UseVisualStyleBackColor = true
        '
        'chkGeneralHideClearArt
        '
        Me.chkGeneralHideClearArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralHideClearArt.AutoSize = true
        Me.chkGeneralHideClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralHideClearArt.Location = New System.Drawing.Point(3, 141)
        Me.chkGeneralHideClearArt.Name = "chkGeneralHideClearArt"
        Me.chkGeneralHideClearArt.Size = New System.Drawing.Size(147, 17)
        Me.chkGeneralHideClearArt.TabIndex = 14
        Me.chkGeneralHideClearArt.Text = "Do Not Display ClearArt"
        Me.chkGeneralHideClearArt.UseVisualStyleBackColor = true
        '
        'chkGeneralHideClearLogo
        '
        Me.chkGeneralHideClearLogo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralHideClearLogo.AutoSize = true
        Me.chkGeneralHideClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralHideClearLogo.Location = New System.Drawing.Point(3, 164)
        Me.chkGeneralHideClearLogo.Name = "chkGeneralHideClearLogo"
        Me.chkGeneralHideClearLogo.Size = New System.Drawing.Size(158, 17)
        Me.chkGeneralHideClearLogo.TabIndex = 16
        Me.chkGeneralHideClearLogo.Text = "Do Not Display ClearLogo"
        Me.chkGeneralHideClearLogo.UseVisualStyleBackColor = true
        '
        'chkGeneralHideBanner
        '
        Me.chkGeneralHideBanner.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralHideBanner.AutoSize = true
        Me.chkGeneralHideBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralHideBanner.Location = New System.Drawing.Point(3, 95)
        Me.chkGeneralHideBanner.Name = "chkGeneralHideBanner"
        Me.chkGeneralHideBanner.Size = New System.Drawing.Size(143, 17)
        Me.chkGeneralHideBanner.TabIndex = 13
        Me.chkGeneralHideBanner.Text = "Do Not Display Banner"
        Me.chkGeneralHideBanner.UseVisualStyleBackColor = true
        '
        'chkGeneralHideCharacterArt
        '
        Me.chkGeneralHideCharacterArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralHideCharacterArt.AutoSize = true
        Me.chkGeneralHideCharacterArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralHideCharacterArt.Location = New System.Drawing.Point(3, 118)
        Me.chkGeneralHideCharacterArt.Name = "chkGeneralHideCharacterArt"
        Me.chkGeneralHideCharacterArt.Size = New System.Drawing.Size(170, 17)
        Me.chkGeneralHideCharacterArt.TabIndex = 15
        Me.chkGeneralHideCharacterArt.Text = "Do Not Display CharacterArt"
        Me.chkGeneralHideCharacterArt.UseVisualStyleBackColor = true
        '
        'gbGeneralScrapers
        '
        Me.gbGeneralScrapers.AutoSize = true
        Me.gbGeneralScrapers.Controls.Add(Me.tblGeneralScrapers)
        Me.gbGeneralScrapers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGeneralScrapers.Enabled = false
        Me.gbGeneralScrapers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbGeneralScrapers.Location = New System.Drawing.Point(3, 292)
        Me.gbGeneralScrapers.Name = "gbGeneralScrapers"
        Me.gbGeneralScrapers.Size = New System.Drawing.Size(238, 44)
        Me.gbGeneralScrapers.TabIndex = 15
        Me.gbGeneralScrapers.TabStop = false
        Me.gbGeneralScrapers.Text = "Scrapers"
        '
        'tblGeneralScrapers
        '
        Me.tblGeneralScrapers.AutoSize = true
        Me.tblGeneralScrapers.ColumnCount = 1
        Me.tblGeneralScrapers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.tblGeneralScrapers.Controls.Add(Me.chkGeneralResumeScraper, 0, 0)
        Me.tblGeneralScrapers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralScrapers.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralScrapers.Name = "tblGeneralScrapers"
        Me.tblGeneralScrapers.RowCount = 1
        Me.tblGeneralScrapers.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.tblGeneralScrapers.Size = New System.Drawing.Size(232, 23)
        Me.tblGeneralScrapers.TabIndex = 17
        '
        'chkGeneralResumeScraper
        '
        Me.chkGeneralResumeScraper.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralResumeScraper.AutoSize = true
        Me.chkGeneralResumeScraper.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkGeneralResumeScraper.Location = New System.Drawing.Point(3, 3)
        Me.chkGeneralResumeScraper.Name = "chkGeneralResumeScraper"
        Me.chkGeneralResumeScraper.Size = New System.Drawing.Size(145, 17)
        Me.chkGeneralResumeScraper.TabIndex = 1
        Me.chkGeneralResumeScraper.Text = "Enable Scraper Resume"
        Me.chkGeneralResumeScraper.UseVisualStyleBackColor = true
        '
        'pnlMovieGeneral
        '
        Me.pnlMovieGeneral.BackColor = System.Drawing.Color.White
        Me.pnlMovieGeneral.Controls.Add(Me.gbMovieGeneralCustomMarker)
        Me.pnlMovieGeneral.Controls.Add(Me.gbMovieGenrealIMDBMirrorOpts)
        Me.pnlMovieGeneral.Controls.Add(Me.gbMovieGeneralGenreFilterOpts)
        Me.pnlMovieGeneral.Controls.Add(Me.gbMovieGeneralFiltersOpts)
        Me.pnlMovieGeneral.Controls.Add(Me.gbMovieGeneralMissingItemsOpts)
        Me.pnlMovieGeneral.Controls.Add(Me.gbMovieGeneralMiscOpts)
        Me.pnlMovieGeneral.Controls.Add(Me.gbMovieGeneralMediaListOpts)
        Me.pnlMovieGeneral.Location = New System.Drawing.Point(900, 900)
        Me.pnlMovieGeneral.Name = "pnlMovieGeneral"
        Me.pnlMovieGeneral.Size = New System.Drawing.Size(750, 500)
        Me.pnlMovieGeneral.TabIndex = 15
        Me.pnlMovieGeneral.Visible = false
        '
        'gbMovieGeneralCustomMarker
        '
        Me.gbMovieGeneralCustomMarker.Controls.Add(Me.btnMovieGeneralCustomMarker4)
        Me.gbMovieGeneralCustomMarker.Controls.Add(Me.txtMovieGeneralCustomMarker4)
        Me.gbMovieGeneralCustomMarker.Controls.Add(Me.lblMovieGeneralCustomMarker4)
        Me.gbMovieGeneralCustomMarker.Controls.Add(Me.btnMovieGeneralCustomMarker3)
        Me.gbMovieGeneralCustomMarker.Controls.Add(Me.txtMovieGeneralCustomMarker3)
        Me.gbMovieGeneralCustomMarker.Controls.Add(Me.lblMovieGeneralCustomMarker3)
        Me.gbMovieGeneralCustomMarker.Controls.Add(Me.btnMovieGeneralCustomMarker2)
        Me.gbMovieGeneralCustomMarker.Controls.Add(Me.txtMovieGeneralCustomMarker2)
        Me.gbMovieGeneralCustomMarker.Controls.Add(Me.lblMovieGeneralCustomMarker2)
        Me.gbMovieGeneralCustomMarker.Controls.Add(Me.btnMovieGeneralCustomMarker1)
        Me.gbMovieGeneralCustomMarker.Controls.Add(Me.txtMovieGeneralCustomMarker1)
        Me.gbMovieGeneralCustomMarker.Controls.Add(Me.lblMovieGeneralCustomMarker1)
        Me.gbMovieGeneralCustomMarker.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbMovieGeneralCustomMarker.Location = New System.Drawing.Point(427, 366)
        Me.gbMovieGeneralCustomMarker.Name = "gbMovieGeneralCustomMarker"
        Me.gbMovieGeneralCustomMarker.Size = New System.Drawing.Size(282, 131)
        Me.gbMovieGeneralCustomMarker.TabIndex = 9
        Me.gbMovieGeneralCustomMarker.TabStop = false
        Me.gbMovieGeneralCustomMarker.Text = "Custom Marker"
        '
        'btnMovieGeneralCustomMarker4
        '
        Me.btnMovieGeneralCustomMarker4.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.btnMovieGeneralCustomMarker4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnMovieGeneralCustomMarker4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnMovieGeneralCustomMarker4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnMovieGeneralCustomMarker4.Location = New System.Drawing.Point(252, 103)
        Me.btnMovieGeneralCustomMarker4.Name = "btnMovieGeneralCustomMarker4"
        Me.btnMovieGeneralCustomMarker4.Size = New System.Drawing.Size(24, 22)
        Me.btnMovieGeneralCustomMarker4.TabIndex = 24
        Me.btnMovieGeneralCustomMarker4.UseVisualStyleBackColor = false
        '
        'txtMovieGeneralCustomMarker4
        '
        Me.txtMovieGeneralCustomMarker4.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMovieGeneralCustomMarker4.Location = New System.Drawing.Point(110, 103)
        Me.txtMovieGeneralCustomMarker4.Name = "txtMovieGeneralCustomMarker4"
        Me.txtMovieGeneralCustomMarker4.Size = New System.Drawing.Size(136, 22)
        Me.txtMovieGeneralCustomMarker4.TabIndex = 23
        '
        'lblMovieGeneralCustomMarker4
        '
        Me.lblMovieGeneralCustomMarker4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieGeneralCustomMarker4.Location = New System.Drawing.Point(7, 106)
        Me.lblMovieGeneralCustomMarker4.Name = "lblMovieGeneralCustomMarker4"
        Me.lblMovieGeneralCustomMarker4.Size = New System.Drawing.Size(97, 13)
        Me.lblMovieGeneralCustomMarker4.TabIndex = 22
        Me.lblMovieGeneralCustomMarker4.Text = "Custom 4"
        '
        'btnMovieGeneralCustomMarker3
        '
        Me.btnMovieGeneralCustomMarker3.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.btnMovieGeneralCustomMarker3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnMovieGeneralCustomMarker3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnMovieGeneralCustomMarker3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnMovieGeneralCustomMarker3.Location = New System.Drawing.Point(252, 75)
        Me.btnMovieGeneralCustomMarker3.Name = "btnMovieGeneralCustomMarker3"
        Me.btnMovieGeneralCustomMarker3.Size = New System.Drawing.Size(24, 22)
        Me.btnMovieGeneralCustomMarker3.TabIndex = 21
        Me.btnMovieGeneralCustomMarker3.UseVisualStyleBackColor = false
        '
        'txtMovieGeneralCustomMarker3
        '
        Me.txtMovieGeneralCustomMarker3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMovieGeneralCustomMarker3.Location = New System.Drawing.Point(110, 75)
        Me.txtMovieGeneralCustomMarker3.Name = "txtMovieGeneralCustomMarker3"
        Me.txtMovieGeneralCustomMarker3.Size = New System.Drawing.Size(136, 22)
        Me.txtMovieGeneralCustomMarker3.TabIndex = 20
        '
        'lblMovieGeneralCustomMarker3
        '
        Me.lblMovieGeneralCustomMarker3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieGeneralCustomMarker3.Location = New System.Drawing.Point(7, 78)
        Me.lblMovieGeneralCustomMarker3.Name = "lblMovieGeneralCustomMarker3"
        Me.lblMovieGeneralCustomMarker3.Size = New System.Drawing.Size(97, 13)
        Me.lblMovieGeneralCustomMarker3.TabIndex = 19
        Me.lblMovieGeneralCustomMarker3.Text = "Custom 3"
        '
        'btnMovieGeneralCustomMarker2
        '
        Me.btnMovieGeneralCustomMarker2.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.btnMovieGeneralCustomMarker2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnMovieGeneralCustomMarker2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnMovieGeneralCustomMarker2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnMovieGeneralCustomMarker2.Location = New System.Drawing.Point(252, 47)
        Me.btnMovieGeneralCustomMarker2.Name = "btnMovieGeneralCustomMarker2"
        Me.btnMovieGeneralCustomMarker2.Size = New System.Drawing.Size(24, 22)
        Me.btnMovieGeneralCustomMarker2.TabIndex = 18
        Me.btnMovieGeneralCustomMarker2.UseVisualStyleBackColor = false
        '
        'txtMovieGeneralCustomMarker2
        '
        Me.txtMovieGeneralCustomMarker2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMovieGeneralCustomMarker2.Location = New System.Drawing.Point(110, 47)
        Me.txtMovieGeneralCustomMarker2.Name = "txtMovieGeneralCustomMarker2"
        Me.txtMovieGeneralCustomMarker2.Size = New System.Drawing.Size(136, 22)
        Me.txtMovieGeneralCustomMarker2.TabIndex = 17
        '
        'lblMovieGeneralCustomMarker2
        '
        Me.lblMovieGeneralCustomMarker2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieGeneralCustomMarker2.Location = New System.Drawing.Point(7, 50)
        Me.lblMovieGeneralCustomMarker2.Name = "lblMovieGeneralCustomMarker2"
        Me.lblMovieGeneralCustomMarker2.Size = New System.Drawing.Size(97, 13)
        Me.lblMovieGeneralCustomMarker2.TabIndex = 16
        Me.lblMovieGeneralCustomMarker2.Text = "Custom 2"
        '
        'btnMovieGeneralCustomMarker1
        '
        Me.btnMovieGeneralCustomMarker1.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.btnMovieGeneralCustomMarker1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnMovieGeneralCustomMarker1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnMovieGeneralCustomMarker1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnMovieGeneralCustomMarker1.Location = New System.Drawing.Point(252, 19)
        Me.btnMovieGeneralCustomMarker1.Name = "btnMovieGeneralCustomMarker1"
        Me.btnMovieGeneralCustomMarker1.Size = New System.Drawing.Size(24, 22)
        Me.btnMovieGeneralCustomMarker1.TabIndex = 15
        Me.btnMovieGeneralCustomMarker1.UseVisualStyleBackColor = false
        '
        'txtMovieGeneralCustomMarker1
        '
        Me.txtMovieGeneralCustomMarker1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMovieGeneralCustomMarker1.Location = New System.Drawing.Point(110, 19)
        Me.txtMovieGeneralCustomMarker1.Name = "txtMovieGeneralCustomMarker1"
        Me.txtMovieGeneralCustomMarker1.Size = New System.Drawing.Size(136, 22)
        Me.txtMovieGeneralCustomMarker1.TabIndex = 1
        '
        'lblMovieGeneralCustomMarker1
        '
        Me.lblMovieGeneralCustomMarker1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieGeneralCustomMarker1.Location = New System.Drawing.Point(7, 22)
        Me.lblMovieGeneralCustomMarker1.Name = "lblMovieGeneralCustomMarker1"
        Me.lblMovieGeneralCustomMarker1.Size = New System.Drawing.Size(97, 13)
        Me.lblMovieGeneralCustomMarker1.TabIndex = 0
        Me.lblMovieGeneralCustomMarker1.Text = "Custom 1"
        '
        'gbMovieGenrealIMDBMirrorOpts
        '
        Me.gbMovieGenrealIMDBMirrorOpts.Controls.Add(Me.lblMovieIMDBMirror)
        Me.gbMovieGenrealIMDBMirrorOpts.Controls.Add(Me.txtMovieIMDBURL)
        Me.gbMovieGenrealIMDBMirrorOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieGenrealIMDBMirrorOpts.Location = New System.Drawing.Point(228, 209)
        Me.gbMovieGenrealIMDBMirrorOpts.Name = "gbMovieGenrealIMDBMirrorOpts"
        Me.gbMovieGenrealIMDBMirrorOpts.Size = New System.Drawing.Size(382, 41)
        Me.gbMovieGenrealIMDBMirrorOpts.TabIndex = 8
        Me.gbMovieGenrealIMDBMirrorOpts.TabStop = false
        Me.gbMovieGenrealIMDBMirrorOpts.Text = "IMDB"
        '
        'lblMovieIMDBMirror
        '
        Me.lblMovieIMDBMirror.AutoSize = true
        Me.lblMovieIMDBMirror.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieIMDBMirror.Location = New System.Drawing.Point(6, 18)
        Me.lblMovieIMDBMirror.Name = "lblMovieIMDBMirror"
        Me.lblMovieIMDBMirror.Size = New System.Drawing.Size(73, 13)
        Me.lblMovieIMDBMirror.TabIndex = 0
        Me.lblMovieIMDBMirror.Text = "IMDB Mirror:"
        '
        'txtMovieIMDBURL
        '
        Me.txtMovieIMDBURL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieIMDBURL.Location = New System.Drawing.Point(79, 15)
        Me.txtMovieIMDBURL.Name = "txtMovieIMDBURL"
        Me.txtMovieIMDBURL.Size = New System.Drawing.Size(291, 22)
        Me.txtMovieIMDBURL.TabIndex = 1
        '
        'gbMovieGeneralGenreFilterOpts
        '
        Me.gbMovieGeneralGenreFilterOpts.Controls.Add(Me.clbMovieGenre)
        Me.gbMovieGeneralGenreFilterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieGeneralGenreFilterOpts.Location = New System.Drawing.Point(427, 256)
        Me.gbMovieGeneralGenreFilterOpts.Name = "gbMovieGeneralGenreFilterOpts"
        Me.gbMovieGeneralGenreFilterOpts.Size = New System.Drawing.Size(183, 104)
        Me.gbMovieGeneralGenreFilterOpts.TabIndex = 7
        Me.gbMovieGeneralGenreFilterOpts.TabStop = false
        Me.gbMovieGeneralGenreFilterOpts.Text = "Genre Language Filter"
        '
        'gbMovieGeneralFiltersOpts
        '
        Me.gbMovieGeneralFiltersOpts.Controls.Add(Me.btnMovieFilterReset)
        Me.gbMovieGeneralFiltersOpts.Controls.Add(Me.btnMovieFilterDown)
        Me.gbMovieGeneralFiltersOpts.Controls.Add(Me.btnMovieFilterUp)
        Me.gbMovieGeneralFiltersOpts.Controls.Add(Me.chkMovieProperCase)
        Me.gbMovieGeneralFiltersOpts.Controls.Add(Me.btnMovieFilterRemove)
        Me.gbMovieGeneralFiltersOpts.Controls.Add(Me.btnMovieFilterAdd)
        Me.gbMovieGeneralFiltersOpts.Controls.Add(Me.txtMovieFilter)
        Me.gbMovieGeneralFiltersOpts.Controls.Add(Me.lstMovieFilters)
        Me.gbMovieGeneralFiltersOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieGeneralFiltersOpts.Location = New System.Drawing.Point(228, 3)
        Me.gbMovieGeneralFiltersOpts.Name = "gbMovieGeneralFiltersOpts"
        Me.gbMovieGeneralFiltersOpts.Size = New System.Drawing.Size(382, 200)
        Me.gbMovieGeneralFiltersOpts.TabIndex = 6
        Me.gbMovieGeneralFiltersOpts.TabStop = false
        Me.gbMovieGeneralFiltersOpts.Text = "Folder/File Name Filters"
        '
        'btnMovieFilterReset
        '
        Me.btnMovieFilterReset.Image = CType(resources.GetObject("btnMovieFilterReset.Image"),System.Drawing.Image)
        Me.btnMovieFilterReset.Location = New System.Drawing.Point(355, 11)
        Me.btnMovieFilterReset.Name = "btnMovieFilterReset"
        Me.btnMovieFilterReset.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieFilterReset.TabIndex = 8
        Me.btnMovieFilterReset.UseVisualStyleBackColor = true
        '
        'btnMovieFilterDown
        '
        Me.btnMovieFilterDown.Image = CType(resources.GetObject("btnMovieFilterDown.Image"),System.Drawing.Image)
        Me.btnMovieFilterDown.Location = New System.Drawing.Point(313, 172)
        Me.btnMovieFilterDown.Name = "btnMovieFilterDown"
        Me.btnMovieFilterDown.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieFilterDown.TabIndex = 5
        Me.btnMovieFilterDown.UseVisualStyleBackColor = true
        '
        'btnMovieFilterUp
        '
        Me.btnMovieFilterUp.Image = CType(resources.GetObject("btnMovieFilterUp.Image"),System.Drawing.Image)
        Me.btnMovieFilterUp.Location = New System.Drawing.Point(289, 172)
        Me.btnMovieFilterUp.Name = "btnMovieFilterUp"
        Me.btnMovieFilterUp.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieFilterUp.TabIndex = 4
        Me.btnMovieFilterUp.UseVisualStyleBackColor = true
        '
        'chkMovieProperCase
        '
        Me.chkMovieProperCase.AutoSize = true
        Me.chkMovieProperCase.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieProperCase.Location = New System.Drawing.Point(6, 16)
        Me.chkMovieProperCase.Name = "chkMovieProperCase"
        Me.chkMovieProperCase.Size = New System.Drawing.Size(181, 17)
        Me.chkMovieProperCase.TabIndex = 0
        Me.chkMovieProperCase.Text = "Convert Names to Proper Case"
        Me.chkMovieProperCase.UseVisualStyleBackColor = true
        '
        'btnMovieFilterRemove
        '
        Me.btnMovieFilterRemove.Image = CType(resources.GetObject("btnMovieFilterRemove.Image"),System.Drawing.Image)
        Me.btnMovieFilterRemove.Location = New System.Drawing.Point(354, 172)
        Me.btnMovieFilterRemove.Name = "btnMovieFilterRemove"
        Me.btnMovieFilterRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieFilterRemove.TabIndex = 6
        Me.btnMovieFilterRemove.UseVisualStyleBackColor = true
        '
        'btnMovieFilterAdd
        '
        Me.btnMovieFilterAdd.Image = CType(resources.GetObject("btnMovieFilterAdd.Image"),System.Drawing.Image)
        Me.btnMovieFilterAdd.Location = New System.Drawing.Point(247, 172)
        Me.btnMovieFilterAdd.Name = "btnMovieFilterAdd"
        Me.btnMovieFilterAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieFilterAdd.TabIndex = 3
        Me.btnMovieFilterAdd.UseVisualStyleBackColor = true
        '
        'txtMovieFilter
        '
        Me.txtMovieFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieFilter.Location = New System.Drawing.Point(6, 173)
        Me.txtMovieFilter.Name = "txtMovieFilter"
        Me.txtMovieFilter.Size = New System.Drawing.Size(239, 22)
        Me.txtMovieFilter.TabIndex = 2
        '
        'lstMovieFilters
        '
        Me.lstMovieFilters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstMovieFilters.FormattingEnabled = true
        Me.lstMovieFilters.Location = New System.Drawing.Point(6, 36)
        Me.lstMovieFilters.Name = "lstMovieFilters"
        Me.lstMovieFilters.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstMovieFilters.Size = New System.Drawing.Size(371, 121)
        Me.lstMovieFilters.TabIndex = 1
        '
        'gbMovieGeneralMissingItemsOpts
        '
        Me.gbMovieGeneralMissingItemsOpts.Controls.Add(Me.chkMovieMissingDiscArt)
        Me.gbMovieGeneralMissingItemsOpts.Controls.Add(Me.chkMovieMissingClearLogo)
        Me.gbMovieGeneralMissingItemsOpts.Controls.Add(Me.chkMovieMissingClearArt)
        Me.gbMovieGeneralMissingItemsOpts.Controls.Add(Me.chkMovieMissingTheme)
        Me.gbMovieGeneralMissingItemsOpts.Controls.Add(Me.chkMovieMissingLandscape)
        Me.gbMovieGeneralMissingItemsOpts.Controls.Add(Me.chkMovieMissingBanner)
        Me.gbMovieGeneralMissingItemsOpts.Controls.Add(Me.chkMovieMissingEFanarts)
        Me.gbMovieGeneralMissingItemsOpts.Controls.Add(Me.chkMovieMissingTrailer)
        Me.gbMovieGeneralMissingItemsOpts.Controls.Add(Me.chkMovieMissingEThumbs)
        Me.gbMovieGeneralMissingItemsOpts.Controls.Add(Me.chkMovieMissingPoster)
        Me.gbMovieGeneralMissingItemsOpts.Controls.Add(Me.chkMovieMissingNFO)
        Me.gbMovieGeneralMissingItemsOpts.Controls.Add(Me.chkMovieMissingSubs)
        Me.gbMovieGeneralMissingItemsOpts.Controls.Add(Me.chkMovieMissingFanart)
        Me.gbMovieGeneralMissingItemsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieGeneralMissingItemsOpts.Location = New System.Drawing.Point(228, 256)
        Me.gbMovieGeneralMissingItemsOpts.Name = "gbMovieGeneralMissingItemsOpts"
        Me.gbMovieGeneralMissingItemsOpts.Size = New System.Drawing.Size(185, 241)
        Me.gbMovieGeneralMissingItemsOpts.TabIndex = 5
        Me.gbMovieGeneralMissingItemsOpts.TabStop = false
        Me.gbMovieGeneralMissingItemsOpts.Text = "Missing Items Filter"
        '
        'chkMovieMissingDiscArt
        '
        Me.chkMovieMissingDiscArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieMissingDiscArt.Location = New System.Drawing.Point(6, 69)
        Me.chkMovieMissingDiscArt.Name = "chkMovieMissingDiscArt"
        Me.chkMovieMissingDiscArt.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieMissingDiscArt.TabIndex = 15
        Me.chkMovieMissingDiscArt.Text = "Check for DiscArt"
        Me.chkMovieMissingDiscArt.UseVisualStyleBackColor = true
        '
        'chkMovieMissingClearLogo
        '
        Me.chkMovieMissingClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieMissingClearLogo.Location = New System.Drawing.Point(6, 52)
        Me.chkMovieMissingClearLogo.Name = "chkMovieMissingClearLogo"
        Me.chkMovieMissingClearLogo.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieMissingClearLogo.TabIndex = 14
        Me.chkMovieMissingClearLogo.Text = "Check for ClearLogo"
        Me.chkMovieMissingClearLogo.UseVisualStyleBackColor = true
        '
        'chkMovieMissingClearArt
        '
        Me.chkMovieMissingClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieMissingClearArt.Location = New System.Drawing.Point(6, 35)
        Me.chkMovieMissingClearArt.Name = "chkMovieMissingClearArt"
        Me.chkMovieMissingClearArt.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieMissingClearArt.TabIndex = 13
        Me.chkMovieMissingClearArt.Text = "Check for ClearArt"
        Me.chkMovieMissingClearArt.UseVisualStyleBackColor = true
        '
        'chkMovieMissingTheme
        '
        Me.chkMovieMissingTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieMissingTheme.Location = New System.Drawing.Point(5, 205)
        Me.chkMovieMissingTheme.Name = "chkMovieMissingTheme"
        Me.chkMovieMissingTheme.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieMissingTheme.TabIndex = 9
        Me.chkMovieMissingTheme.Text = "Check for Theme"
        Me.chkMovieMissingTheme.UseVisualStyleBackColor = true
        '
        'chkMovieMissingLandscape
        '
        Me.chkMovieMissingLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieMissingLandscape.Location = New System.Drawing.Point(6, 137)
        Me.chkMovieMissingLandscape.Name = "chkMovieMissingLandscape"
        Me.chkMovieMissingLandscape.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieMissingLandscape.TabIndex = 10
        Me.chkMovieMissingLandscape.Text = "Check for Landscape"
        Me.chkMovieMissingLandscape.UseVisualStyleBackColor = true
        '
        'chkMovieMissingBanner
        '
        Me.chkMovieMissingBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieMissingBanner.Location = New System.Drawing.Point(6, 18)
        Me.chkMovieMissingBanner.Name = "chkMovieMissingBanner"
        Me.chkMovieMissingBanner.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieMissingBanner.TabIndex = 11
        Me.chkMovieMissingBanner.Text = "Check for Banner"
        Me.chkMovieMissingBanner.UseVisualStyleBackColor = true
        '
        'chkMovieMissingEFanarts
        '
        Me.chkMovieMissingEFanarts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieMissingEFanarts.Location = New System.Drawing.Point(6, 86)
        Me.chkMovieMissingEFanarts.Name = "chkMovieMissingEFanarts"
        Me.chkMovieMissingEFanarts.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieMissingEFanarts.TabIndex = 12
        Me.chkMovieMissingEFanarts.Text = "Check for Extrafanarts"
        Me.chkMovieMissingEFanarts.UseVisualStyleBackColor = true
        '
        'chkMovieMissingTrailer
        '
        Me.chkMovieMissingTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieMissingTrailer.Location = New System.Drawing.Point(5, 222)
        Me.chkMovieMissingTrailer.Name = "chkMovieMissingTrailer"
        Me.chkMovieMissingTrailer.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieMissingTrailer.TabIndex = 9
        Me.chkMovieMissingTrailer.Text = "Check for Trailer"
        Me.chkMovieMissingTrailer.UseVisualStyleBackColor = true
        '
        'chkMovieMissingEThumbs
        '
        Me.chkMovieMissingEThumbs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieMissingEThumbs.Location = New System.Drawing.Point(6, 103)
        Me.chkMovieMissingEThumbs.Name = "chkMovieMissingEThumbs"
        Me.chkMovieMissingEThumbs.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieMissingEThumbs.TabIndex = 11
        Me.chkMovieMissingEThumbs.Text = "Check for Extrathumbs"
        Me.chkMovieMissingEThumbs.UseVisualStyleBackColor = true
        '
        'chkMovieMissingPoster
        '
        Me.chkMovieMissingPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieMissingPoster.Location = New System.Drawing.Point(6, 171)
        Me.chkMovieMissingPoster.Name = "chkMovieMissingPoster"
        Me.chkMovieMissingPoster.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieMissingPoster.TabIndex = 6
        Me.chkMovieMissingPoster.Text = "Check for Poster"
        Me.chkMovieMissingPoster.UseVisualStyleBackColor = true
        '
        'chkMovieMissingNFO
        '
        Me.chkMovieMissingNFO.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieMissingNFO.Location = New System.Drawing.Point(6, 154)
        Me.chkMovieMissingNFO.Name = "chkMovieMissingNFO"
        Me.chkMovieMissingNFO.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieMissingNFO.TabIndex = 8
        Me.chkMovieMissingNFO.Text = "Check for NFO"
        Me.chkMovieMissingNFO.UseVisualStyleBackColor = true
        '
        'chkMovieMissingSubs
        '
        Me.chkMovieMissingSubs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieMissingSubs.Location = New System.Drawing.Point(6, 188)
        Me.chkMovieMissingSubs.Name = "chkMovieMissingSubs"
        Me.chkMovieMissingSubs.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieMissingSubs.TabIndex = 10
        Me.chkMovieMissingSubs.Text = "Check for Subtitles"
        Me.chkMovieMissingSubs.UseVisualStyleBackColor = true
        '
        'chkMovieMissingFanart
        '
        Me.chkMovieMissingFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieMissingFanart.Location = New System.Drawing.Point(6, 120)
        Me.chkMovieMissingFanart.Name = "chkMovieMissingFanart"
        Me.chkMovieMissingFanart.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieMissingFanart.TabIndex = 7
        Me.chkMovieMissingFanart.Text = "Check for Fanart"
        Me.chkMovieMissingFanart.UseVisualStyleBackColor = true
        '
        'pnlFileSystem
        '
        Me.pnlFileSystem.BackColor = System.Drawing.Color.White
        Me.pnlFileSystem.Controls.Add(Me.gbFileSystemValidSubtitlesExts)
        Me.pnlFileSystem.Controls.Add(Me.gbFileSystemExcludedDirs)
        Me.pnlFileSystem.Controls.Add(Me.gbFileSystemValidThemeExts)
        Me.pnlFileSystem.Controls.Add(Me.gbFileSystemNoStackExts)
        Me.pnlFileSystem.Controls.Add(Me.gbFileSystemCleanFiles)
        Me.pnlFileSystem.Controls.Add(Me.gbFileSystemValidExts)
        Me.pnlFileSystem.Location = New System.Drawing.Point(900, 900)
        Me.pnlFileSystem.Name = "pnlFileSystem"
        Me.pnlFileSystem.Size = New System.Drawing.Size(750, 500)
        Me.pnlFileSystem.TabIndex = 17
        Me.pnlFileSystem.Visible = false
        '
        'gbFileSystemValidSubtitlesExts
        '
        Me.gbFileSystemValidSubtitlesExts.Controls.Add(Me.btnFileSystemValidSubtitlesExtsReset)
        Me.gbFileSystemValidSubtitlesExts.Controls.Add(Me.btnFileSystemValidSubtitlesExtsRemove)
        Me.gbFileSystemValidSubtitlesExts.Controls.Add(Me.btnFileSystemValidSubtitlesExtsAdd)
        Me.gbFileSystemValidSubtitlesExts.Controls.Add(Me.txtFileSystemValidSubtitlesExts)
        Me.gbFileSystemValidSubtitlesExts.Controls.Add(Me.lstFileSystemValidSubtitlesExts)
        Me.gbFileSystemValidSubtitlesExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbFileSystemValidSubtitlesExts.Location = New System.Drawing.Point(3, 181)
        Me.gbFileSystemValidSubtitlesExts.Name = "gbFileSystemValidSubtitlesExts"
        Me.gbFileSystemValidSubtitlesExts.Size = New System.Drawing.Size(192, 176)
        Me.gbFileSystemValidSubtitlesExts.TabIndex = 5
        Me.gbFileSystemValidSubtitlesExts.TabStop = false
        Me.gbFileSystemValidSubtitlesExts.Text = "Valid Subtitles Extensions"
        '
        'btnFileSystemValidSubtitlesExtsReset
        '
        Me.btnFileSystemValidSubtitlesExtsReset.Image = CType(resources.GetObject("btnFileSystemValidSubtitlesExtsReset.Image"),System.Drawing.Image)
        Me.btnFileSystemValidSubtitlesExtsReset.Location = New System.Drawing.Point(164, 12)
        Me.btnFileSystemValidSubtitlesExtsReset.Name = "btnFileSystemValidSubtitlesExtsReset"
        Me.btnFileSystemValidSubtitlesExtsReset.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidSubtitlesExtsReset.TabIndex = 5
        Me.btnFileSystemValidSubtitlesExtsReset.UseVisualStyleBackColor = true
        '
        'btnFileSystemValidSubtitlesExtsRemove
        '
        Me.btnFileSystemValidSubtitlesExtsRemove.Image = CType(resources.GetObject("btnFileSystemValidSubtitlesExtsRemove.Image"),System.Drawing.Image)
        Me.btnFileSystemValidSubtitlesExtsRemove.Location = New System.Drawing.Point(163, 137)
        Me.btnFileSystemValidSubtitlesExtsRemove.Name = "btnFileSystemValidSubtitlesExtsRemove"
        Me.btnFileSystemValidSubtitlesExtsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidSubtitlesExtsRemove.TabIndex = 3
        Me.btnFileSystemValidSubtitlesExtsRemove.UseVisualStyleBackColor = true
        '
        'btnFileSystemValidSubtitlesExtsAdd
        '
        Me.btnFileSystemValidSubtitlesExtsAdd.Image = CType(resources.GetObject("btnFileSystemValidSubtitlesExtsAdd.Image"),System.Drawing.Image)
        Me.btnFileSystemValidSubtitlesExtsAdd.Location = New System.Drawing.Point(68, 137)
        Me.btnFileSystemValidSubtitlesExtsAdd.Name = "btnFileSystemValidSubtitlesExtsAdd"
        Me.btnFileSystemValidSubtitlesExtsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidSubtitlesExtsAdd.TabIndex = 2
        Me.btnFileSystemValidSubtitlesExtsAdd.UseVisualStyleBackColor = true
        '
        'txtFileSystemValidSubtitlesExts
        '
        Me.txtFileSystemValidSubtitlesExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFileSystemValidSubtitlesExts.Location = New System.Drawing.Point(6, 138)
        Me.txtFileSystemValidSubtitlesExts.Name = "txtFileSystemValidSubtitlesExts"
        Me.txtFileSystemValidSubtitlesExts.Size = New System.Drawing.Size(61, 22)
        Me.txtFileSystemValidSubtitlesExts.TabIndex = 1
        '
        'lstFileSystemValidSubtitlesExts
        '
        Me.lstFileSystemValidSubtitlesExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstFileSystemValidSubtitlesExts.FormattingEnabled = true
        Me.lstFileSystemValidSubtitlesExts.Location = New System.Drawing.Point(6, 37)
        Me.lstFileSystemValidSubtitlesExts.Name = "lstFileSystemValidSubtitlesExts"
        Me.lstFileSystemValidSubtitlesExts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFileSystemValidSubtitlesExts.Size = New System.Drawing.Size(180, 95)
        Me.lstFileSystemValidSubtitlesExts.Sorted = true
        Me.lstFileSystemValidSubtitlesExts.TabIndex = 0
        '
        'gbFileSystemExcludedDirs
        '
        Me.gbFileSystemExcludedDirs.Controls.Add(Me.btnFileSystemExcludedDirsRemove)
        Me.gbFileSystemExcludedDirs.Controls.Add(Me.btnFileSystemExcludedDirsAdd)
        Me.gbFileSystemExcludedDirs.Controls.Add(Me.txtFileSystemExcludedDirs)
        Me.gbFileSystemExcludedDirs.Controls.Add(Me.lstFileSystemExcludedDirs)
        Me.gbFileSystemExcludedDirs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbFileSystemExcludedDirs.Location = New System.Drawing.Point(3, 363)
        Me.gbFileSystemExcludedDirs.Name = "gbFileSystemExcludedDirs"
        Me.gbFileSystemExcludedDirs.Size = New System.Drawing.Size(744, 134)
        Me.gbFileSystemExcludedDirs.TabIndex = 4
        Me.gbFileSystemExcludedDirs.TabStop = false
        Me.gbFileSystemExcludedDirs.Text = "Excluded Directories"
        '
        'btnFileSystemExcludedDirsRemove
        '
        Me.btnFileSystemExcludedDirsRemove.Image = CType(resources.GetObject("btnFileSystemExcludedDirsRemove.Image"),System.Drawing.Image)
        Me.btnFileSystemExcludedDirsRemove.Location = New System.Drawing.Point(715, 108)
        Me.btnFileSystemExcludedDirsRemove.Name = "btnFileSystemExcludedDirsRemove"
        Me.btnFileSystemExcludedDirsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemExcludedDirsRemove.TabIndex = 6
        Me.btnFileSystemExcludedDirsRemove.UseVisualStyleBackColor = true
        '
        'btnFileSystemExcludedDirsAdd
        '
        Me.btnFileSystemExcludedDirsAdd.Image = CType(resources.GetObject("btnFileSystemExcludedDirsAdd.Image"),System.Drawing.Image)
        Me.btnFileSystemExcludedDirsAdd.Location = New System.Drawing.Point(646, 109)
        Me.btnFileSystemExcludedDirsAdd.Name = "btnFileSystemExcludedDirsAdd"
        Me.btnFileSystemExcludedDirsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemExcludedDirsAdd.TabIndex = 5
        Me.btnFileSystemExcludedDirsAdd.UseVisualStyleBackColor = true
        '
        'txtFileSystemExcludedDirs
        '
        Me.txtFileSystemExcludedDirs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFileSystemExcludedDirs.Location = New System.Drawing.Point(9, 109)
        Me.txtFileSystemExcludedDirs.Name = "txtFileSystemExcludedDirs"
        Me.txtFileSystemExcludedDirs.Size = New System.Drawing.Size(631, 22)
        Me.txtFileSystemExcludedDirs.TabIndex = 4
        '
        'lstFileSystemExcludedDirs
        '
        Me.lstFileSystemExcludedDirs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstFileSystemExcludedDirs.FormattingEnabled = true
        Me.lstFileSystemExcludedDirs.Location = New System.Drawing.Point(9, 21)
        Me.lstFileSystemExcludedDirs.Name = "lstFileSystemExcludedDirs"
        Me.lstFileSystemExcludedDirs.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFileSystemExcludedDirs.Size = New System.Drawing.Size(729, 82)
        Me.lstFileSystemExcludedDirs.Sorted = true
        Me.lstFileSystemExcludedDirs.TabIndex = 1
        '
        'gbFileSystemValidThemeExts
        '
        Me.gbFileSystemValidThemeExts.Controls.Add(Me.btnFileSystemValidThemeExtsReset)
        Me.gbFileSystemValidThemeExts.Controls.Add(Me.btnFileSystemValidThemeExtsRemove)
        Me.gbFileSystemValidThemeExts.Controls.Add(Me.btnFileSystemValidThemeExtsAdd)
        Me.gbFileSystemValidThemeExts.Controls.Add(Me.txtFileSystemValidThemeExts)
        Me.gbFileSystemValidThemeExts.Controls.Add(Me.lstFileSystemValidThemeExts)
        Me.gbFileSystemValidThemeExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbFileSystemValidThemeExts.Location = New System.Drawing.Point(202, 140)
        Me.gbFileSystemValidThemeExts.Name = "gbFileSystemValidThemeExts"
        Me.gbFileSystemValidThemeExts.Size = New System.Drawing.Size(194, 217)
        Me.gbFileSystemValidThemeExts.TabIndex = 3
        Me.gbFileSystemValidThemeExts.TabStop = false
        Me.gbFileSystemValidThemeExts.Text = "Valid Theme Extensions"
        '
        'btnFileSystemValidThemeExtsReset
        '
        Me.btnFileSystemValidThemeExtsReset.Image = CType(resources.GetObject("btnFileSystemValidThemeExtsReset.Image"),System.Drawing.Image)
        Me.btnFileSystemValidThemeExtsReset.Location = New System.Drawing.Point(164, 12)
        Me.btnFileSystemValidThemeExtsReset.Name = "btnFileSystemValidThemeExtsReset"
        Me.btnFileSystemValidThemeExtsReset.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidThemeExtsReset.TabIndex = 4
        Me.btnFileSystemValidThemeExtsReset.UseVisualStyleBackColor = true
        '
        'btnFileSystemValidThemeExtsRemove
        '
        Me.btnFileSystemValidThemeExtsRemove.Image = CType(resources.GetObject("btnFileSystemValidThemeExtsRemove.Image"),System.Drawing.Image)
        Me.btnFileSystemValidThemeExtsRemove.Location = New System.Drawing.Point(163, 185)
        Me.btnFileSystemValidThemeExtsRemove.Name = "btnFileSystemValidThemeExtsRemove"
        Me.btnFileSystemValidThemeExtsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidThemeExtsRemove.TabIndex = 3
        Me.btnFileSystemValidThemeExtsRemove.UseVisualStyleBackColor = true
        '
        'btnFileSystemValidThemeExtsAdd
        '
        Me.btnFileSystemValidThemeExtsAdd.Image = CType(resources.GetObject("btnFileSystemValidThemeExtsAdd.Image"),System.Drawing.Image)
        Me.btnFileSystemValidThemeExtsAdd.Location = New System.Drawing.Point(68, 185)
        Me.btnFileSystemValidThemeExtsAdd.Name = "btnFileSystemValidThemeExtsAdd"
        Me.btnFileSystemValidThemeExtsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidThemeExtsAdd.TabIndex = 2
        Me.btnFileSystemValidThemeExtsAdd.UseVisualStyleBackColor = true
        '
        'txtFileSystemValidThemeExts
        '
        Me.txtFileSystemValidThemeExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFileSystemValidThemeExts.Location = New System.Drawing.Point(6, 186)
        Me.txtFileSystemValidThemeExts.Name = "txtFileSystemValidThemeExts"
        Me.txtFileSystemValidThemeExts.Size = New System.Drawing.Size(61, 22)
        Me.txtFileSystemValidThemeExts.TabIndex = 1
        '
        'lstFileSystemValidThemeExts
        '
        Me.lstFileSystemValidThemeExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstFileSystemValidThemeExts.FormattingEnabled = true
        Me.lstFileSystemValidThemeExts.Location = New System.Drawing.Point(6, 46)
        Me.lstFileSystemValidThemeExts.Name = "lstFileSystemValidThemeExts"
        Me.lstFileSystemValidThemeExts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFileSystemValidThemeExts.Size = New System.Drawing.Size(180, 134)
        Me.lstFileSystemValidThemeExts.Sorted = true
        Me.lstFileSystemValidThemeExts.TabIndex = 0
        '
        'gbFileSystemNoStackExts
        '
        Me.gbFileSystemNoStackExts.Controls.Add(Me.btnFileSystemNoStackExtsRemove)
        Me.gbFileSystemNoStackExts.Controls.Add(Me.btnFileSystemNoStackExtsAdd)
        Me.gbFileSystemNoStackExts.Controls.Add(Me.txtFileSystemNoStackExts)
        Me.gbFileSystemNoStackExts.Controls.Add(Me.lstFileSystemNoStackExts)
        Me.gbFileSystemNoStackExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbFileSystemNoStackExts.Location = New System.Drawing.Point(202, 6)
        Me.gbFileSystemNoStackExts.Name = "gbFileSystemNoStackExts"
        Me.gbFileSystemNoStackExts.Size = New System.Drawing.Size(194, 128)
        Me.gbFileSystemNoStackExts.TabIndex = 1
        Me.gbFileSystemNoStackExts.TabStop = false
        Me.gbFileSystemNoStackExts.Text = "No Stack Extensions"
        '
        'btnFileSystemNoStackExtsRemove
        '
        Me.btnFileSystemNoStackExtsRemove.Image = CType(resources.GetObject("btnFileSystemNoStackExtsRemove.Image"),System.Drawing.Image)
        Me.btnFileSystemNoStackExtsRemove.Location = New System.Drawing.Point(160, 91)
        Me.btnFileSystemNoStackExtsRemove.Name = "btnFileSystemNoStackExtsRemove"
        Me.btnFileSystemNoStackExtsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemNoStackExtsRemove.TabIndex = 3
        Me.btnFileSystemNoStackExtsRemove.UseVisualStyleBackColor = true
        '
        'btnFileSystemNoStackExtsAdd
        '
        Me.btnFileSystemNoStackExtsAdd.Image = CType(resources.GetObject("btnFileSystemNoStackExtsAdd.Image"),System.Drawing.Image)
        Me.btnFileSystemNoStackExtsAdd.Location = New System.Drawing.Point(73, 91)
        Me.btnFileSystemNoStackExtsAdd.Name = "btnFileSystemNoStackExtsAdd"
        Me.btnFileSystemNoStackExtsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemNoStackExtsAdd.TabIndex = 2
        Me.btnFileSystemNoStackExtsAdd.UseVisualStyleBackColor = true
        '
        'txtFileSystemNoStackExts
        '
        Me.txtFileSystemNoStackExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFileSystemNoStackExts.Location = New System.Drawing.Point(11, 92)
        Me.txtFileSystemNoStackExts.Name = "txtFileSystemNoStackExts"
        Me.txtFileSystemNoStackExts.Size = New System.Drawing.Size(61, 22)
        Me.txtFileSystemNoStackExts.TabIndex = 1
        '
        'lstFileSystemNoStackExts
        '
        Me.lstFileSystemNoStackExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstFileSystemNoStackExts.FormattingEnabled = true
        Me.lstFileSystemNoStackExts.Location = New System.Drawing.Point(11, 15)
        Me.lstFileSystemNoStackExts.Name = "lstFileSystemNoStackExts"
        Me.lstFileSystemNoStackExts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFileSystemNoStackExts.Size = New System.Drawing.Size(171, 69)
        Me.lstFileSystemNoStackExts.Sorted = true
        Me.lstFileSystemNoStackExts.TabIndex = 0
        '
        'gbFileSystemValidExts
        '
        Me.gbFileSystemValidExts.Controls.Add(Me.btnFileSystemValidExtsReset)
        Me.gbFileSystemValidExts.Controls.Add(Me.btnFileSystemValidExtsRemove)
        Me.gbFileSystemValidExts.Controls.Add(Me.btnFileSystemValidExtsAdd)
        Me.gbFileSystemValidExts.Controls.Add(Me.txtFileSystemValidExts)
        Me.gbFileSystemValidExts.Controls.Add(Me.lstFileSystemValidExts)
        Me.gbFileSystemValidExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbFileSystemValidExts.Location = New System.Drawing.Point(3, 6)
        Me.gbFileSystemValidExts.Name = "gbFileSystemValidExts"
        Me.gbFileSystemValidExts.Size = New System.Drawing.Size(192, 169)
        Me.gbFileSystemValidExts.TabIndex = 0
        Me.gbFileSystemValidExts.TabStop = false
        Me.gbFileSystemValidExts.Text = "Valid Video Extensions"
        '
        'btnFileSystemValidExtsReset
        '
        Me.btnFileSystemValidExtsReset.Image = CType(resources.GetObject("btnFileSystemValidExtsReset.Image"),System.Drawing.Image)
        Me.btnFileSystemValidExtsReset.Location = New System.Drawing.Point(164, 12)
        Me.btnFileSystemValidExtsReset.Name = "btnFileSystemValidExtsReset"
        Me.btnFileSystemValidExtsReset.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidExtsReset.TabIndex = 4
        Me.btnFileSystemValidExtsReset.UseVisualStyleBackColor = true
        '
        'btnFileSystemValidExtsRemove
        '
        Me.btnFileSystemValidExtsRemove.Image = CType(resources.GetObject("btnFileSystemValidExtsRemove.Image"),System.Drawing.Image)
        Me.btnFileSystemValidExtsRemove.Location = New System.Drawing.Point(162, 139)
        Me.btnFileSystemValidExtsRemove.Name = "btnFileSystemValidExtsRemove"
        Me.btnFileSystemValidExtsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidExtsRemove.TabIndex = 3
        Me.btnFileSystemValidExtsRemove.UseVisualStyleBackColor = true
        '
        'btnFileSystemValidExtsAdd
        '
        Me.btnFileSystemValidExtsAdd.Image = CType(resources.GetObject("btnFileSystemValidExtsAdd.Image"),System.Drawing.Image)
        Me.btnFileSystemValidExtsAdd.Location = New System.Drawing.Point(67, 139)
        Me.btnFileSystemValidExtsAdd.Name = "btnFileSystemValidExtsAdd"
        Me.btnFileSystemValidExtsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidExtsAdd.TabIndex = 2
        Me.btnFileSystemValidExtsAdd.UseVisualStyleBackColor = true
        '
        'txtFileSystemValidExts
        '
        Me.txtFileSystemValidExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtFileSystemValidExts.Location = New System.Drawing.Point(5, 140)
        Me.txtFileSystemValidExts.Name = "txtFileSystemValidExts"
        Me.txtFileSystemValidExts.Size = New System.Drawing.Size(61, 22)
        Me.txtFileSystemValidExts.TabIndex = 1
        '
        'lstFileSystemValidExts
        '
        Me.lstFileSystemValidExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstFileSystemValidExts.FormattingEnabled = true
        Me.lstFileSystemValidExts.Location = New System.Drawing.Point(6, 37)
        Me.lstFileSystemValidExts.Name = "lstFileSystemValidExts"
        Me.lstFileSystemValidExts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFileSystemValidExts.Size = New System.Drawing.Size(180, 95)
        Me.lstFileSystemValidExts.Sorted = true
        Me.lstFileSystemValidExts.TabIndex = 0
        '
        'pnlProxy
        '
        Me.pnlProxy.BackColor = System.Drawing.Color.White
        Me.pnlProxy.Controls.Add(Me.gbProxyOpts)
        Me.pnlProxy.Location = New System.Drawing.Point(900, 900)
        Me.pnlProxy.Name = "pnlProxy"
        Me.pnlProxy.Size = New System.Drawing.Size(750, 500)
        Me.pnlProxy.TabIndex = 18
        Me.pnlProxy.Visible = false
        '
        'gbProxyOpts
        '
        Me.gbProxyOpts.Controls.Add(Me.gbProxyCredsOpts)
        Me.gbProxyOpts.Controls.Add(Me.lblProxyPort)
        Me.gbProxyOpts.Controls.Add(Me.lblProxyURI)
        Me.gbProxyOpts.Controls.Add(Me.txtProxyPort)
        Me.gbProxyOpts.Controls.Add(Me.txtProxyURI)
        Me.gbProxyOpts.Controls.Add(Me.chkProxyEnable)
        Me.gbProxyOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbProxyOpts.Location = New System.Drawing.Point(3, 6)
        Me.gbProxyOpts.Name = "gbProxyOpts"
        Me.gbProxyOpts.Size = New System.Drawing.Size(290, 230)
        Me.gbProxyOpts.TabIndex = 0
        Me.gbProxyOpts.TabStop = false
        Me.gbProxyOpts.Text = "Proxy"
        '
        'gbProxyCredsOpts
        '
        Me.gbProxyCredsOpts.Controls.Add(Me.txtProxyDomain)
        Me.gbProxyCredsOpts.Controls.Add(Me.lblProxyDomain)
        Me.gbProxyCredsOpts.Controls.Add(Me.txtProxyPassword)
        Me.gbProxyCredsOpts.Controls.Add(Me.txtProxyUsername)
        Me.gbProxyCredsOpts.Controls.Add(Me.lblProxyUsername)
        Me.gbProxyCredsOpts.Controls.Add(Me.lblProxyPassword)
        Me.gbProxyCredsOpts.Controls.Add(Me.chkProxyCredsEnable)
        Me.gbProxyCredsOpts.Enabled = false
        Me.gbProxyCredsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbProxyCredsOpts.Location = New System.Drawing.Point(5, 115)
        Me.gbProxyCredsOpts.Name = "gbProxyCredsOpts"
        Me.gbProxyCredsOpts.Size = New System.Drawing.Size(279, 103)
        Me.gbProxyCredsOpts.TabIndex = 5
        Me.gbProxyCredsOpts.TabStop = false
        Me.gbProxyCredsOpts.Text = "Credentials"
        '
        'txtProxyDomain
        '
        Me.txtProxyDomain.Enabled = false
        Me.txtProxyDomain.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtProxyDomain.Location = New System.Drawing.Point(64, 69)
        Me.txtProxyDomain.Name = "txtProxyDomain"
        Me.txtProxyDomain.Size = New System.Drawing.Size(209, 22)
        Me.txtProxyDomain.TabIndex = 6
        '
        'lblProxyDomain
        '
        Me.lblProxyDomain.AutoSize = true
        Me.lblProxyDomain.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblProxyDomain.Location = New System.Drawing.Point(14, 72)
        Me.lblProxyDomain.Name = "lblProxyDomain"
        Me.lblProxyDomain.Size = New System.Drawing.Size(50, 13)
        Me.lblProxyDomain.TabIndex = 5
        Me.lblProxyDomain.Text = "Domain:"
        '
        'txtProxyPassword
        '
        Me.txtProxyPassword.Enabled = false
        Me.txtProxyPassword.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtProxyPassword.Location = New System.Drawing.Point(201, 39)
        Me.txtProxyPassword.Name = "txtProxyPassword"
        Me.txtProxyPassword.Size = New System.Drawing.Size(72, 22)
        Me.txtProxyPassword.TabIndex = 4
        Me.txtProxyPassword.UseSystemPasswordChar = true
        '
        'txtProxyUsername
        '
        Me.txtProxyUsername.Enabled = false
        Me.txtProxyUsername.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtProxyUsername.Location = New System.Drawing.Point(64, 39)
        Me.txtProxyUsername.Name = "txtProxyUsername"
        Me.txtProxyUsername.Size = New System.Drawing.Size(72, 22)
        Me.txtProxyUsername.TabIndex = 2
        '
        'lblProxyUsername
        '
        Me.lblProxyUsername.AutoSize = true
        Me.lblProxyUsername.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblProxyUsername.Location = New System.Drawing.Point(3, 42)
        Me.lblProxyUsername.Name = "lblProxyUsername"
        Me.lblProxyUsername.Size = New System.Drawing.Size(61, 13)
        Me.lblProxyUsername.TabIndex = 1
        Me.lblProxyUsername.Text = "Username:"
        '
        'lblProxyPassword
        '
        Me.lblProxyPassword.AutoSize = true
        Me.lblProxyPassword.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblProxyPassword.Location = New System.Drawing.Point(143, 42)
        Me.lblProxyPassword.Name = "lblProxyPassword"
        Me.lblProxyPassword.Size = New System.Drawing.Size(59, 13)
        Me.lblProxyPassword.TabIndex = 3
        Me.lblProxyPassword.Text = "Password:"
        '
        'chkProxyCredsEnable
        '
        Me.chkProxyCredsEnable.AutoSize = true
        Me.chkProxyCredsEnable.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkProxyCredsEnable.Location = New System.Drawing.Point(8, 17)
        Me.chkProxyCredsEnable.Name = "chkProxyCredsEnable"
        Me.chkProxyCredsEnable.Size = New System.Drawing.Size(122, 17)
        Me.chkProxyCredsEnable.TabIndex = 0
        Me.chkProxyCredsEnable.Text = "Enable Credentials"
        Me.chkProxyCredsEnable.UseVisualStyleBackColor = true
        '
        'lblProxyPort
        '
        Me.lblProxyPort.AutoSize = true
        Me.lblProxyPort.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblProxyPort.Location = New System.Drawing.Point(9, 88)
        Me.lblProxyPort.Name = "lblProxyPort"
        Me.lblProxyPort.Size = New System.Drawing.Size(61, 13)
        Me.lblProxyPort.TabIndex = 3
        Me.lblProxyPort.Text = "Proxy Port:"
        '
        'lblProxyURI
        '
        Me.lblProxyURI.AutoSize = true
        Me.lblProxyURI.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblProxyURI.Location = New System.Drawing.Point(9, 39)
        Me.lblProxyURI.Name = "lblProxyURI"
        Me.lblProxyURI.Size = New System.Drawing.Size(58, 13)
        Me.lblProxyURI.TabIndex = 1
        Me.lblProxyURI.Text = "Proxy URI:"
        '
        'txtProxyPort
        '
        Me.txtProxyPort.Enabled = false
        Me.txtProxyPort.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtProxyPort.Location = New System.Drawing.Point(72, 84)
        Me.txtProxyPort.Name = "txtProxyPort"
        Me.txtProxyPort.Size = New System.Drawing.Size(51, 22)
        Me.txtProxyPort.TabIndex = 4
        '
        'txtProxyURI
        '
        Me.txtProxyURI.Enabled = false
        Me.txtProxyURI.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtProxyURI.Location = New System.Drawing.Point(11, 54)
        Me.txtProxyURI.Name = "txtProxyURI"
        Me.txtProxyURI.Size = New System.Drawing.Size(267, 22)
        Me.txtProxyURI.TabIndex = 2
        '
        'chkProxyEnable
        '
        Me.chkProxyEnable.AutoSize = true
        Me.chkProxyEnable.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkProxyEnable.Location = New System.Drawing.Point(11, 17)
        Me.chkProxyEnable.Name = "chkProxyEnable"
        Me.chkProxyEnable.Size = New System.Drawing.Size(91, 17)
        Me.chkProxyEnable.TabIndex = 0
        Me.chkProxyEnable.Text = "Enable Proxy"
        Me.chkProxyEnable.UseVisualStyleBackColor = true
        '
        'gbMovieBackdropsFolder
        '
        Me.gbMovieBackdropsFolder.Controls.Add(Me.chkMovieBackdropsAuto)
        Me.gbMovieBackdropsFolder.Controls.Add(Me.btnMovieBackdropsPathBrowse)
        Me.gbMovieBackdropsFolder.Controls.Add(Me.txtMovieBackdropsPath)
        Me.gbMovieBackdropsFolder.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieBackdropsFolder.Location = New System.Drawing.Point(5, 412)
        Me.gbMovieBackdropsFolder.Name = "gbMovieBackdropsFolder"
        Me.gbMovieBackdropsFolder.Size = New System.Drawing.Size(212, 85)
        Me.gbMovieBackdropsFolder.TabIndex = 6
        Me.gbMovieBackdropsFolder.TabStop = false
        Me.gbMovieBackdropsFolder.Text = "Backdrops Folder"
        '
        'chkMovieBackdropsAuto
        '
        Me.chkMovieBackdropsAuto.Enabled = false
        Me.chkMovieBackdropsAuto.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieBackdropsAuto.Location = New System.Drawing.Point(6, 49)
        Me.chkMovieBackdropsAuto.Name = "chkMovieBackdropsAuto"
        Me.chkMovieBackdropsAuto.Size = New System.Drawing.Size(200, 33)
        Me.chkMovieBackdropsAuto.TabIndex = 2
        Me.chkMovieBackdropsAuto.Text = "Automatically Save Fanart To Backdrops Folder"
        Me.chkMovieBackdropsAuto.UseVisualStyleBackColor = true
        '
        'btnMovieBackdropsPathBrowse
        '
        Me.btnMovieBackdropsPathBrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnMovieBackdropsPathBrowse.Location = New System.Drawing.Point(181, 21)
        Me.btnMovieBackdropsPathBrowse.Name = "btnMovieBackdropsPathBrowse"
        Me.btnMovieBackdropsPathBrowse.Size = New System.Drawing.Size(25, 22)
        Me.btnMovieBackdropsPathBrowse.TabIndex = 1
        Me.btnMovieBackdropsPathBrowse.Text = "..."
        Me.btnMovieBackdropsPathBrowse.UseVisualStyleBackColor = true
        '
        'txtMovieBackdropsPath
        '
        Me.txtMovieBackdropsPath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieBackdropsPath.Location = New System.Drawing.Point(6, 21)
        Me.txtMovieBackdropsPath.Name = "txtMovieBackdropsPath"
        Me.txtMovieBackdropsPath.Size = New System.Drawing.Size(169, 22)
        Me.txtMovieBackdropsPath.TabIndex = 0
        '
        'lblSettingsCurrent
        '
        Me.lblSettingsCurrent.BackColor = System.Drawing.Color.Transparent
        Me.lblSettingsCurrent.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblSettingsCurrent.ForeColor = System.Drawing.Color.White
        Me.lblSettingsCurrent.Location = New System.Drawing.Point(26, -1)
        Me.lblSettingsCurrent.Name = "lblSettingsCurrent"
        Me.lblSettingsCurrent.Size = New System.Drawing.Size(969, 25)
        Me.lblSettingsCurrent.TabIndex = 0
        Me.lblSettingsCurrent.Text = "General"
        '
        'pnlSettingsCurrent
        '
        Me.pnlSettingsCurrent.BackColor = System.Drawing.Color.SteelBlue
        Me.pnlSettingsCurrent.Controls.Add(Me.pbSettingsCurrent)
        Me.pnlSettingsCurrent.Controls.Add(Me.lblSettingsCurrent)
        Me.pnlSettingsCurrent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsCurrent.Location = New System.Drawing.Point(5, 3)
        Me.pnlSettingsCurrent.Name = "pnlSettingsCurrent"
        Me.pnlSettingsCurrent.Size = New System.Drawing.Size(998, 27)
        Me.pnlSettingsCurrent.TabIndex = 5
        '
        'pbSettingsCurrent
        '
        Me.pbSettingsCurrent.Location = New System.Drawing.Point(2, 0)
        Me.pbSettingsCurrent.Name = "pbSettingsCurrent"
        Me.pbSettingsCurrent.Size = New System.Drawing.Size(24, 24)
        Me.pbSettingsCurrent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSettingsCurrent.TabIndex = 2
        Me.pbSettingsCurrent.TabStop = false
        '
        'pnlMovieSources
        '
        Me.pnlMovieSources.BackColor = System.Drawing.Color.White
        Me.pnlMovieSources.Controls.Add(Me.gbMovieFileNaming)
        Me.pnlMovieSources.Controls.Add(Me.gbMovieBackdropsFolder)
        Me.pnlMovieSources.Controls.Add(Me.btnMovieSourceEdit)
        Me.pnlMovieSources.Controls.Add(Me.gbMovieSourcesMiscOpts)
        Me.pnlMovieSources.Controls.Add(Me.lvMovieSources)
        Me.pnlMovieSources.Controls.Add(Me.btnMovieSourceRemove)
        Me.pnlMovieSources.Controls.Add(Me.btnMovieSourceAdd)
        Me.pnlMovieSources.Location = New System.Drawing.Point(900, 900)
        Me.pnlMovieSources.Name = "pnlMovieSources"
        Me.pnlMovieSources.Size = New System.Drawing.Size(750, 500)
        Me.pnlMovieSources.TabIndex = 13
        Me.pnlMovieSources.Visible = false
        '
        'gbMovieFileNaming
        '
        Me.gbMovieFileNaming.Controls.Add(Me.tcMovieFileNaming)
        Me.gbMovieFileNaming.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbMovieFileNaming.Location = New System.Drawing.Point(223, 113)
        Me.gbMovieFileNaming.Name = "gbMovieFileNaming"
        Me.gbMovieFileNaming.Size = New System.Drawing.Size(521, 384)
        Me.gbMovieFileNaming.TabIndex = 8
        Me.gbMovieFileNaming.TabStop = false
        Me.gbMovieFileNaming.Text = "File Naming"
        '
        'tcMovieFileNaming
        '
        Me.tcMovieFileNaming.Controls.Add(Me.tpMovieFileNamingXBMC)
        Me.tcMovieFileNaming.Controls.Add(Me.tpMovieFileNamingNMT)
        Me.tcMovieFileNaming.Controls.Add(Me.tpMovieFileNamingBoxee)
        Me.tcMovieFileNaming.Controls.Add(Me.tpMovieFileNamingExpert)
        Me.tcMovieFileNaming.Location = New System.Drawing.Point(6, 18)
        Me.tcMovieFileNaming.Name = "tcMovieFileNaming"
        Me.tcMovieFileNaming.SelectedIndex = 0
        Me.tcMovieFileNaming.Size = New System.Drawing.Size(513, 362)
        Me.tcMovieFileNaming.TabIndex = 7
        '
        'tpMovieFileNamingXBMC
        '
        Me.tpMovieFileNamingXBMC.Controls.Add(Me.gbMovieXBMCTheme)
        Me.tpMovieFileNamingXBMC.Controls.Add(Me.gbMovieXBMCOptionalSettings)
        Me.tpMovieFileNamingXBMC.Controls.Add(Me.gbMovieEden)
        Me.tpMovieFileNamingXBMC.Controls.Add(Me.gbMovieFrodo)
        Me.tpMovieFileNamingXBMC.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieFileNamingXBMC.Name = "tpMovieFileNamingXBMC"
        Me.tpMovieFileNamingXBMC.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMovieFileNamingXBMC.Size = New System.Drawing.Size(505, 336)
        Me.tpMovieFileNamingXBMC.TabIndex = 1
        Me.tpMovieFileNamingXBMC.Text = "XBMC"
        Me.tpMovieFileNamingXBMC.UseVisualStyleBackColor = true
        '
        'gbMovieXBMCTheme
        '
        Me.gbMovieXBMCTheme.Controls.Add(Me.chkMovieXBMCThemeMovie)
        Me.gbMovieXBMCTheme.Controls.Add(Me.btnMovieXBMCThemeCustomPathBrowse)
        Me.gbMovieXBMCTheme.Controls.Add(Me.chkMovieXBMCThemeSub)
        Me.gbMovieXBMCTheme.Controls.Add(Me.txtMovieXBMCThemeSubDir)
        Me.gbMovieXBMCTheme.Controls.Add(Me.txtMovieXBMCThemeCustomPath)
        Me.gbMovieXBMCTheme.Controls.Add(Me.chkMovieXBMCThemeCustom)
        Me.gbMovieXBMCTheme.Controls.Add(Me.chkMovieXBMCThemeEnable)
        Me.gbMovieXBMCTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gbMovieXBMCTheme.Location = New System.Drawing.Point(238, 156)
        Me.gbMovieXBMCTheme.Name = "gbMovieXBMCTheme"
        Me.gbMovieXBMCTheme.Size = New System.Drawing.Size(260, 174)
        Me.gbMovieXBMCTheme.TabIndex = 3
        Me.gbMovieXBMCTheme.TabStop = false
        Me.gbMovieXBMCTheme.Text = "Theme Settings"
        '
        'chkMovieXBMCThemeMovie
        '
        Me.chkMovieXBMCThemeMovie.Enabled = false
        Me.chkMovieXBMCThemeMovie.Location = New System.Drawing.Point(7, 46)
        Me.chkMovieXBMCThemeMovie.Name = "chkMovieXBMCThemeMovie"
        Me.chkMovieXBMCThemeMovie.Size = New System.Drawing.Size(247, 17)
        Me.chkMovieXBMCThemeMovie.TabIndex = 6
        Me.chkMovieXBMCThemeMovie.Text = "Store themes in movie directory"
        Me.chkMovieXBMCThemeMovie.UseVisualStyleBackColor = true
        '
        'btnMovieXBMCThemeCustomPathBrowse
        '
        Me.btnMovieXBMCThemeCustomPathBrowse.Enabled = false
        Me.btnMovieXBMCThemeCustomPathBrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnMovieXBMCThemeCustomPathBrowse.Location = New System.Drawing.Point(229, 93)
        Me.btnMovieXBMCThemeCustomPathBrowse.Name = "btnMovieXBMCThemeCustomPathBrowse"
        Me.btnMovieXBMCThemeCustomPathBrowse.Size = New System.Drawing.Size(25, 22)
        Me.btnMovieXBMCThemeCustomPathBrowse.TabIndex = 5
        Me.btnMovieXBMCThemeCustomPathBrowse.Text = "..."
        Me.btnMovieXBMCThemeCustomPathBrowse.UseVisualStyleBackColor = true
        '
        'chkMovieXBMCThemeSub
        '
        Me.chkMovieXBMCThemeSub.AutoSize = true
        Me.chkMovieXBMCThemeSub.Enabled = false
        Me.chkMovieXBMCThemeSub.Location = New System.Drawing.Point(7, 122)
        Me.chkMovieXBMCThemeSub.Name = "chkMovieXBMCThemeSub"
        Me.chkMovieXBMCThemeSub.Size = New System.Drawing.Size(181, 17)
        Me.chkMovieXBMCThemeSub.TabIndex = 4
        Me.chkMovieXBMCThemeSub.Text = "Store themes in sub directorys"
        Me.chkMovieXBMCThemeSub.UseVisualStyleBackColor = true
        '
        'txtMovieXBMCThemeSubDir
        '
        Me.txtMovieXBMCThemeSubDir.Enabled = false
        Me.txtMovieXBMCThemeSubDir.Location = New System.Drawing.Point(7, 145)
        Me.txtMovieXBMCThemeSubDir.Name = "txtMovieXBMCThemeSubDir"
        Me.txtMovieXBMCThemeSubDir.Size = New System.Drawing.Size(216, 22)
        Me.txtMovieXBMCThemeSubDir.TabIndex = 3
        '
        'txtMovieXBMCThemeCustomPath
        '
        Me.txtMovieXBMCThemeCustomPath.Enabled = false
        Me.txtMovieXBMCThemeCustomPath.Location = New System.Drawing.Point(7, 93)
        Me.txtMovieXBMCThemeCustomPath.Name = "txtMovieXBMCThemeCustomPath"
        Me.txtMovieXBMCThemeCustomPath.Size = New System.Drawing.Size(216, 22)
        Me.txtMovieXBMCThemeCustomPath.TabIndex = 2
        '
        'chkMovieXBMCThemeCustom
        '
        Me.chkMovieXBMCThemeCustom.Enabled = false
        Me.chkMovieXBMCThemeCustom.Location = New System.Drawing.Point(7, 69)
        Me.chkMovieXBMCThemeCustom.Name = "chkMovieXBMCThemeCustom"
        Me.chkMovieXBMCThemeCustom.Size = New System.Drawing.Size(247, 17)
        Me.chkMovieXBMCThemeCustom.TabIndex = 1
        Me.chkMovieXBMCThemeCustom.Text = "Store themes in a custom path"
        Me.chkMovieXBMCThemeCustom.UseVisualStyleBackColor = true
        '
        'chkMovieXBMCThemeEnable
        '
        Me.chkMovieXBMCThemeEnable.Enabled = false
        Me.chkMovieXBMCThemeEnable.Location = New System.Drawing.Point(7, 22)
        Me.chkMovieXBMCThemeEnable.Name = "chkMovieXBMCThemeEnable"
        Me.chkMovieXBMCThemeEnable.Size = New System.Drawing.Size(236, 17)
        Me.chkMovieXBMCThemeEnable.TabIndex = 0
        Me.chkMovieXBMCThemeEnable.Text = "Enable Theme"
        Me.chkMovieXBMCThemeEnable.UseVisualStyleBackColor = true
        '
        'gbMovieXBMCOptionalSettings
        '
        Me.gbMovieXBMCOptionalSettings.Controls.Add(Me.chkMovieXBMCProtectVTSBDMV)
        Me.gbMovieXBMCOptionalSettings.Controls.Add(Me.chkMovieXBMCTrailerFormat)
        Me.gbMovieXBMCOptionalSettings.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbMovieXBMCOptionalSettings.Location = New System.Drawing.Point(238, 6)
        Me.gbMovieXBMCOptionalSettings.Name = "gbMovieXBMCOptionalSettings"
        Me.gbMovieXBMCOptionalSettings.Size = New System.Drawing.Size(261, 107)
        Me.gbMovieXBMCOptionalSettings.TabIndex = 2
        Me.gbMovieXBMCOptionalSettings.TabStop = false
        Me.gbMovieXBMCOptionalSettings.Text = "Optional Settings"
        '
        'chkMovieXBMCProtectVTSBDMV
        '
        Me.chkMovieXBMCProtectVTSBDMV.Enabled = false
        Me.chkMovieXBMCProtectVTSBDMV.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieXBMCProtectVTSBDMV.Name = "chkMovieXBMCProtectVTSBDMV"
        Me.chkMovieXBMCProtectVTSBDMV.Size = New System.Drawing.Size(248, 59)
        Me.chkMovieXBMCProtectVTSBDMV.TabIndex = 1
        Me.chkMovieXBMCProtectVTSBDMV.Text = "Protect DVD/Bluray structure (no Fanart/Nfo/Poster will be saved inside VIDEO_TS/"& _ 
    "BDMV folder)"
        Me.chkMovieXBMCProtectVTSBDMV.UseVisualStyleBackColor = true
        '
        'chkMovieXBMCTrailerFormat
        '
        Me.chkMovieXBMCTrailerFormat.AutoSize = true
        Me.chkMovieXBMCTrailerFormat.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieXBMCTrailerFormat.Name = "chkMovieXBMCTrailerFormat"
        Me.chkMovieXBMCTrailerFormat.Size = New System.Drawing.Size(129, 17)
        Me.chkMovieXBMCTrailerFormat.TabIndex = 0
        Me.chkMovieXBMCTrailerFormat.Text = "XBMC Trailer Format"
        Me.chkMovieXBMCTrailerFormat.UseVisualStyleBackColor = true
        '
        'gbMovieEden
        '
        Me.gbMovieEden.Controls.Add(Me.chkMovieExtrafanartsEden)
        Me.gbMovieEden.Controls.Add(Me.chkMovieExtrathumbsEden)
        Me.gbMovieEden.Controls.Add(Me.chkMovieUseEden)
        Me.gbMovieEden.Controls.Add(Me.chkMovieActorThumbsEden)
        Me.gbMovieEden.Controls.Add(Me.chkMovieTrailerEden)
        Me.gbMovieEden.Controls.Add(Me.chkMovieFanartEden)
        Me.gbMovieEden.Controls.Add(Me.chkMoviePosterEden)
        Me.gbMovieEden.Controls.Add(Me.chkMovieNFOEden)
        Me.gbMovieEden.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbMovieEden.Location = New System.Drawing.Point(122, 6)
        Me.gbMovieEden.Name = "gbMovieEden"
        Me.gbMovieEden.Size = New System.Drawing.Size(110, 324)
        Me.gbMovieEden.TabIndex = 1
        Me.gbMovieEden.TabStop = false
        Me.gbMovieEden.Text = "Eden"
        '
        'chkMovieExtrafanartsEden
        '
        Me.chkMovieExtrafanartsEden.AutoSize = true
        Me.chkMovieExtrafanartsEden.Enabled = false
        Me.chkMovieExtrafanartsEden.Location = New System.Drawing.Point(6, 159)
        Me.chkMovieExtrafanartsEden.Name = "chkMovieExtrafanartsEden"
        Me.chkMovieExtrafanartsEden.Size = New System.Drawing.Size(87, 17)
        Me.chkMovieExtrafanartsEden.TabIndex = 20
        Me.chkMovieExtrafanartsEden.Text = "Extrafanarts"
        Me.chkMovieExtrafanartsEden.UseVisualStyleBackColor = true
        '
        'chkMovieExtrathumbsEden
        '
        Me.chkMovieExtrathumbsEden.AutoSize = true
        Me.chkMovieExtrathumbsEden.Enabled = false
        Me.chkMovieExtrathumbsEden.Location = New System.Drawing.Point(6, 136)
        Me.chkMovieExtrathumbsEden.Name = "chkMovieExtrathumbsEden"
        Me.chkMovieExtrathumbsEden.Size = New System.Drawing.Size(90, 17)
        Me.chkMovieExtrathumbsEden.TabIndex = 19
        Me.chkMovieExtrathumbsEden.Text = "Extrathumbs"
        Me.chkMovieExtrathumbsEden.UseVisualStyleBackColor = true
        '
        'chkMovieUseEden
        '
        Me.chkMovieUseEden.AutoSize = true
        Me.chkMovieUseEden.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieUseEden.Name = "chkMovieUseEden"
        Me.chkMovieUseEden.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieUseEden.TabIndex = 18
        Me.chkMovieUseEden.Text = "Use"
        Me.chkMovieUseEden.UseVisualStyleBackColor = true
        '
        'chkMovieActorThumbsEden
        '
        Me.chkMovieActorThumbsEden.AutoSize = true
        Me.chkMovieActorThumbsEden.Enabled = false
        Me.chkMovieActorThumbsEden.Location = New System.Drawing.Point(6, 113)
        Me.chkMovieActorThumbsEden.Name = "chkMovieActorThumbsEden"
        Me.chkMovieActorThumbsEden.Size = New System.Drawing.Size(96, 17)
        Me.chkMovieActorThumbsEden.TabIndex = 17
        Me.chkMovieActorThumbsEden.Text = "Actor Thumbs"
        Me.chkMovieActorThumbsEden.UseVisualStyleBackColor = true
        '
        'chkMovieTrailerEden
        '
        Me.chkMovieTrailerEden.AutoSize = true
        Me.chkMovieTrailerEden.Enabled = false
        Me.chkMovieTrailerEden.Location = New System.Drawing.Point(6, 182)
        Me.chkMovieTrailerEden.Name = "chkMovieTrailerEden"
        Me.chkMovieTrailerEden.Size = New System.Drawing.Size(57, 17)
        Me.chkMovieTrailerEden.TabIndex = 16
        Me.chkMovieTrailerEden.Text = "Trailer"
        Me.chkMovieTrailerEden.UseVisualStyleBackColor = true
        '
        'chkMovieFanartEden
        '
        Me.chkMovieFanartEden.AutoSize = true
        Me.chkMovieFanartEden.Enabled = false
        Me.chkMovieFanartEden.Location = New System.Drawing.Point(6, 90)
        Me.chkMovieFanartEden.Name = "chkMovieFanartEden"
        Me.chkMovieFanartEden.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieFanartEden.TabIndex = 15
        Me.chkMovieFanartEden.Text = "Fanart"
        Me.chkMovieFanartEden.UseVisualStyleBackColor = true
        '
        'chkMoviePosterEden
        '
        Me.chkMoviePosterEden.AutoSize = true
        Me.chkMoviePosterEden.Enabled = false
        Me.chkMoviePosterEden.Location = New System.Drawing.Point(6, 67)
        Me.chkMoviePosterEden.Name = "chkMoviePosterEden"
        Me.chkMoviePosterEden.Size = New System.Drawing.Size(58, 17)
        Me.chkMoviePosterEden.TabIndex = 14
        Me.chkMoviePosterEden.Text = "Poster"
        Me.chkMoviePosterEden.UseVisualStyleBackColor = true
        '
        'chkMovieNFOEden
        '
        Me.chkMovieNFOEden.AutoSize = true
        Me.chkMovieNFOEden.Enabled = false
        Me.chkMovieNFOEden.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieNFOEden.Name = "chkMovieNFOEden"
        Me.chkMovieNFOEden.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieNFOEden.TabIndex = 13
        Me.chkMovieNFOEden.Text = "NFO"
        Me.chkMovieNFOEden.UseVisualStyleBackColor = true
        '
        'gbMovieFrodo
        '
        Me.gbMovieFrodo.Controls.Add(Me.chkMovieExtrafanartsFrodo)
        Me.gbMovieFrodo.Controls.Add(Me.chkMovieExtrathumbsFrodo)
        Me.gbMovieFrodo.Controls.Add(Me.chkMovieUseFrodo)
        Me.gbMovieFrodo.Controls.Add(Me.chkMovieLandscapeFrodo)
        Me.gbMovieFrodo.Controls.Add(Me.chkMovieBannerFrodo)
        Me.gbMovieFrodo.Controls.Add(Me.chkMovieDiscArtFrodo)
        Me.gbMovieFrodo.Controls.Add(Me.chkMovieClearArtFrodo)
        Me.gbMovieFrodo.Controls.Add(Me.chkMovieClearLogoFrodo)
        Me.gbMovieFrodo.Controls.Add(Me.chkMovieActorThumbsFrodo)
        Me.gbMovieFrodo.Controls.Add(Me.chkMovieTrailerFrodo)
        Me.gbMovieFrodo.Controls.Add(Me.chkMovieFanartFrodo)
        Me.gbMovieFrodo.Controls.Add(Me.chkMoviePosterFrodo)
        Me.gbMovieFrodo.Controls.Add(Me.chkMovieNFOFrodo)
        Me.gbMovieFrodo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbMovieFrodo.Location = New System.Drawing.Point(6, 6)
        Me.gbMovieFrodo.Name = "gbMovieFrodo"
        Me.gbMovieFrodo.Size = New System.Drawing.Size(110, 324)
        Me.gbMovieFrodo.TabIndex = 0
        Me.gbMovieFrodo.TabStop = false
        Me.gbMovieFrodo.Text = "Frodo"
        '
        'chkMovieExtrafanartsFrodo
        '
        Me.chkMovieExtrafanartsFrodo.AutoSize = true
        Me.chkMovieExtrafanartsFrodo.Enabled = false
        Me.chkMovieExtrafanartsFrodo.Location = New System.Drawing.Point(6, 159)
        Me.chkMovieExtrafanartsFrodo.Name = "chkMovieExtrafanartsFrodo"
        Me.chkMovieExtrafanartsFrodo.Size = New System.Drawing.Size(87, 17)
        Me.chkMovieExtrafanartsFrodo.TabIndex = 12
        Me.chkMovieExtrafanartsFrodo.Text = "Extrafanarts"
        Me.chkMovieExtrafanartsFrodo.UseVisualStyleBackColor = true
        '
        'chkMovieExtrathumbsFrodo
        '
        Me.chkMovieExtrathumbsFrodo.AutoSize = true
        Me.chkMovieExtrathumbsFrodo.Enabled = false
        Me.chkMovieExtrathumbsFrodo.Location = New System.Drawing.Point(6, 136)
        Me.chkMovieExtrathumbsFrodo.Name = "chkMovieExtrathumbsFrodo"
        Me.chkMovieExtrathumbsFrodo.Size = New System.Drawing.Size(90, 17)
        Me.chkMovieExtrathumbsFrodo.TabIndex = 11
        Me.chkMovieExtrathumbsFrodo.Text = "Extrathumbs"
        Me.chkMovieExtrathumbsFrodo.UseVisualStyleBackColor = true
        '
        'chkMovieUseFrodo
        '
        Me.chkMovieUseFrodo.AutoSize = true
        Me.chkMovieUseFrodo.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieUseFrodo.Name = "chkMovieUseFrodo"
        Me.chkMovieUseFrodo.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieUseFrodo.TabIndex = 10
        Me.chkMovieUseFrodo.Text = "Use"
        Me.chkMovieUseFrodo.UseVisualStyleBackColor = true
        '
        'chkMovieLandscapeFrodo
        '
        Me.chkMovieLandscapeFrodo.AutoSize = true
        Me.chkMovieLandscapeFrodo.Enabled = false
        Me.chkMovieLandscapeFrodo.Location = New System.Drawing.Point(6, 297)
        Me.chkMovieLandscapeFrodo.Name = "chkMovieLandscapeFrodo"
        Me.chkMovieLandscapeFrodo.Size = New System.Drawing.Size(80, 17)
        Me.chkMovieLandscapeFrodo.TabIndex = 9
        Me.chkMovieLandscapeFrodo.Text = "Landscape"
        Me.chkMovieLandscapeFrodo.UseVisualStyleBackColor = true
        '
        'chkMovieBannerFrodo
        '
        Me.chkMovieBannerFrodo.AutoSize = true
        Me.chkMovieBannerFrodo.Enabled = false
        Me.chkMovieBannerFrodo.Location = New System.Drawing.Point(6, 205)
        Me.chkMovieBannerFrodo.Name = "chkMovieBannerFrodo"
        Me.chkMovieBannerFrodo.Size = New System.Drawing.Size(63, 17)
        Me.chkMovieBannerFrodo.TabIndex = 8
        Me.chkMovieBannerFrodo.Text = "Banner"
        Me.chkMovieBannerFrodo.UseVisualStyleBackColor = true
        '
        'chkMovieDiscArtFrodo
        '
        Me.chkMovieDiscArtFrodo.AutoSize = true
        Me.chkMovieDiscArtFrodo.Enabled = false
        Me.chkMovieDiscArtFrodo.Location = New System.Drawing.Point(6, 274)
        Me.chkMovieDiscArtFrodo.Name = "chkMovieDiscArtFrodo"
        Me.chkMovieDiscArtFrodo.Size = New System.Drawing.Size(62, 17)
        Me.chkMovieDiscArtFrodo.TabIndex = 7
        Me.chkMovieDiscArtFrodo.Text = "DiscArt"
        Me.chkMovieDiscArtFrodo.UseVisualStyleBackColor = true
        '
        'chkMovieClearArtFrodo
        '
        Me.chkMovieClearArtFrodo.AutoSize = true
        Me.chkMovieClearArtFrodo.Enabled = false
        Me.chkMovieClearArtFrodo.Location = New System.Drawing.Point(6, 251)
        Me.chkMovieClearArtFrodo.Name = "chkMovieClearArtFrodo"
        Me.chkMovieClearArtFrodo.Size = New System.Drawing.Size(67, 17)
        Me.chkMovieClearArtFrodo.TabIndex = 6
        Me.chkMovieClearArtFrodo.Text = "ClearArt"
        Me.chkMovieClearArtFrodo.UseVisualStyleBackColor = true
        '
        'chkMovieClearLogoFrodo
        '
        Me.chkMovieClearLogoFrodo.AutoSize = true
        Me.chkMovieClearLogoFrodo.Enabled = false
        Me.chkMovieClearLogoFrodo.Location = New System.Drawing.Point(6, 228)
        Me.chkMovieClearLogoFrodo.Name = "chkMovieClearLogoFrodo"
        Me.chkMovieClearLogoFrodo.Size = New System.Drawing.Size(78, 17)
        Me.chkMovieClearLogoFrodo.TabIndex = 5
        Me.chkMovieClearLogoFrodo.Text = "ClearLogo"
        Me.chkMovieClearLogoFrodo.UseVisualStyleBackColor = true
        '
        'chkMovieActorThumbsFrodo
        '
        Me.chkMovieActorThumbsFrodo.AutoSize = true
        Me.chkMovieActorThumbsFrodo.Enabled = false
        Me.chkMovieActorThumbsFrodo.Location = New System.Drawing.Point(6, 113)
        Me.chkMovieActorThumbsFrodo.Name = "chkMovieActorThumbsFrodo"
        Me.chkMovieActorThumbsFrodo.Size = New System.Drawing.Size(96, 17)
        Me.chkMovieActorThumbsFrodo.TabIndex = 4
        Me.chkMovieActorThumbsFrodo.Text = "Actor Thumbs"
        Me.chkMovieActorThumbsFrodo.UseVisualStyleBackColor = true
        '
        'chkMovieTrailerFrodo
        '
        Me.chkMovieTrailerFrodo.AutoSize = true
        Me.chkMovieTrailerFrodo.Enabled = false
        Me.chkMovieTrailerFrodo.Location = New System.Drawing.Point(6, 182)
        Me.chkMovieTrailerFrodo.Name = "chkMovieTrailerFrodo"
        Me.chkMovieTrailerFrodo.Size = New System.Drawing.Size(57, 17)
        Me.chkMovieTrailerFrodo.TabIndex = 3
        Me.chkMovieTrailerFrodo.Text = "Trailer"
        Me.chkMovieTrailerFrodo.UseVisualStyleBackColor = true
        '
        'chkMovieFanartFrodo
        '
        Me.chkMovieFanartFrodo.AutoSize = true
        Me.chkMovieFanartFrodo.Enabled = false
        Me.chkMovieFanartFrodo.Location = New System.Drawing.Point(6, 90)
        Me.chkMovieFanartFrodo.Name = "chkMovieFanartFrodo"
        Me.chkMovieFanartFrodo.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieFanartFrodo.TabIndex = 2
        Me.chkMovieFanartFrodo.Text = "Fanart"
        Me.chkMovieFanartFrodo.UseVisualStyleBackColor = true
        '
        'chkMoviePosterFrodo
        '
        Me.chkMoviePosterFrodo.AutoSize = true
        Me.chkMoviePosterFrodo.Enabled = false
        Me.chkMoviePosterFrodo.Location = New System.Drawing.Point(6, 67)
        Me.chkMoviePosterFrodo.Name = "chkMoviePosterFrodo"
        Me.chkMoviePosterFrodo.Size = New System.Drawing.Size(58, 17)
        Me.chkMoviePosterFrodo.TabIndex = 1
        Me.chkMoviePosterFrodo.Text = "Poster"
        Me.chkMoviePosterFrodo.UseVisualStyleBackColor = true
        '
        'chkMovieNFOFrodo
        '
        Me.chkMovieNFOFrodo.AutoSize = true
        Me.chkMovieNFOFrodo.Enabled = false
        Me.chkMovieNFOFrodo.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieNFOFrodo.Name = "chkMovieNFOFrodo"
        Me.chkMovieNFOFrodo.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieNFOFrodo.TabIndex = 0
        Me.chkMovieNFOFrodo.Text = "NFO"
        Me.chkMovieNFOFrodo.UseVisualStyleBackColor = true
        '
        'tpMovieFileNamingNMT
        '
        Me.tpMovieFileNamingNMT.Controls.Add(Me.gbMovieNMTOptionalSettings)
        Me.tpMovieFileNamingNMT.Controls.Add(Me.gbMovieNMJ)
        Me.tpMovieFileNamingNMT.Controls.Add(Me.gbMovieYAMJ)
        Me.tpMovieFileNamingNMT.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieFileNamingNMT.Name = "tpMovieFileNamingNMT"
        Me.tpMovieFileNamingNMT.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMovieFileNamingNMT.Size = New System.Drawing.Size(505, 336)
        Me.tpMovieFileNamingNMT.TabIndex = 3
        Me.tpMovieFileNamingNMT.Text = "NMT"
        Me.tpMovieFileNamingNMT.UseVisualStyleBackColor = true
        '
        'gbMovieNMTOptionalSettings
        '
        Me.gbMovieNMTOptionalSettings.Controls.Add(Me.chkMovieYAMJCompatibleSets)
        Me.gbMovieNMTOptionalSettings.Controls.Add(Me.btnMovieYAMJWatchedFilesBrowse)
        Me.gbMovieNMTOptionalSettings.Controls.Add(Me.txtMovieYAMJWatchedFolder)
        Me.gbMovieNMTOptionalSettings.Controls.Add(Me.chkMovieYAMJWatchedFile)
        Me.gbMovieNMTOptionalSettings.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gbMovieNMTOptionalSettings.Location = New System.Drawing.Point(238, 6)
        Me.gbMovieNMTOptionalSettings.Name = "gbMovieNMTOptionalSettings"
        Me.gbMovieNMTOptionalSettings.Size = New System.Drawing.Size(261, 107)
        Me.gbMovieNMTOptionalSettings.TabIndex = 18
        Me.gbMovieNMTOptionalSettings.TabStop = false
        Me.gbMovieNMTOptionalSettings.Text = "Optional Settings"
        '
        'chkMovieYAMJCompatibleSets
        '
        Me.chkMovieYAMJCompatibleSets.AutoSize = true
        Me.chkMovieYAMJCompatibleSets.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieYAMJCompatibleSets.Name = "chkMovieYAMJCompatibleSets"
        Me.chkMovieYAMJCompatibleSets.Size = New System.Drawing.Size(138, 17)
        Me.chkMovieYAMJCompatibleSets.TabIndex = 3
        Me.chkMovieYAMJCompatibleSets.Text = "YAMJ Compatible Sets"
        Me.chkMovieYAMJCompatibleSets.UseVisualStyleBackColor = true
        '
        'btnMovieYAMJWatchedFilesBrowse
        '
        Me.btnMovieYAMJWatchedFilesBrowse.Enabled = false
        Me.btnMovieYAMJWatchedFilesBrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnMovieYAMJWatchedFilesBrowse.Location = New System.Drawing.Point(230, 67)
        Me.btnMovieYAMJWatchedFilesBrowse.Name = "btnMovieYAMJWatchedFilesBrowse"
        Me.btnMovieYAMJWatchedFilesBrowse.Size = New System.Drawing.Size(25, 22)
        Me.btnMovieYAMJWatchedFilesBrowse.TabIndex = 2
        Me.btnMovieYAMJWatchedFilesBrowse.Text = "..."
        Me.btnMovieYAMJWatchedFilesBrowse.UseVisualStyleBackColor = true
        '
        'txtMovieYAMJWatchedFolder
        '
        Me.txtMovieYAMJWatchedFolder.Enabled = false
        Me.txtMovieYAMJWatchedFolder.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMovieYAMJWatchedFolder.Location = New System.Drawing.Point(6, 67)
        Me.txtMovieYAMJWatchedFolder.Name = "txtMovieYAMJWatchedFolder"
        Me.txtMovieYAMJWatchedFolder.Size = New System.Drawing.Size(218, 22)
        Me.txtMovieYAMJWatchedFolder.TabIndex = 1
        '
        'chkMovieYAMJWatchedFile
        '
        Me.chkMovieYAMJWatchedFile.AutoSize = true
        Me.chkMovieYAMJWatchedFile.Enabled = false
        Me.chkMovieYAMJWatchedFile.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkMovieYAMJWatchedFile.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieYAMJWatchedFile.Name = "chkMovieYAMJWatchedFile"
        Me.chkMovieYAMJWatchedFile.Size = New System.Drawing.Size(121, 17)
        Me.chkMovieYAMJWatchedFile.TabIndex = 0
        Me.chkMovieYAMJWatchedFile.Text = "Use .watched Files"
        Me.chkMovieYAMJWatchedFile.UseVisualStyleBackColor = true
        '
        'gbMovieNMJ
        '
        Me.gbMovieNMJ.Controls.Add(Me.chkMovieUseNMJ)
        Me.gbMovieNMJ.Controls.Add(Me.chkMovieBannerNMJ)
        Me.gbMovieNMJ.Controls.Add(Me.chkMovieTrailerNMJ)
        Me.gbMovieNMJ.Controls.Add(Me.chkMovieFanartNMJ)
        Me.gbMovieNMJ.Controls.Add(Me.chkMoviePosterNMJ)
        Me.gbMovieNMJ.Controls.Add(Me.chkMovieNFONMJ)
        Me.gbMovieNMJ.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbMovieNMJ.Location = New System.Drawing.Point(122, 6)
        Me.gbMovieNMJ.Name = "gbMovieNMJ"
        Me.gbMovieNMJ.Size = New System.Drawing.Size(110, 324)
        Me.gbMovieNMJ.TabIndex = 17
        Me.gbMovieNMJ.TabStop = false
        Me.gbMovieNMJ.Text = "NMJ"
        '
        'chkMovieUseNMJ
        '
        Me.chkMovieUseNMJ.AutoSize = true
        Me.chkMovieUseNMJ.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieUseNMJ.Name = "chkMovieUseNMJ"
        Me.chkMovieUseNMJ.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieUseNMJ.TabIndex = 16
        Me.chkMovieUseNMJ.Text = "Use"
        Me.chkMovieUseNMJ.UseVisualStyleBackColor = true
        '
        'chkMovieBannerNMJ
        '
        Me.chkMovieBannerNMJ.AutoSize = true
        Me.chkMovieBannerNMJ.Enabled = false
        Me.chkMovieBannerNMJ.Location = New System.Drawing.Point(6, 113)
        Me.chkMovieBannerNMJ.Name = "chkMovieBannerNMJ"
        Me.chkMovieBannerNMJ.Size = New System.Drawing.Size(63, 17)
        Me.chkMovieBannerNMJ.TabIndex = 15
        Me.chkMovieBannerNMJ.Text = "Banner"
        Me.chkMovieBannerNMJ.UseVisualStyleBackColor = true
        '
        'chkMovieTrailerNMJ
        '
        Me.chkMovieTrailerNMJ.AutoSize = true
        Me.chkMovieTrailerNMJ.Enabled = false
        Me.chkMovieTrailerNMJ.Location = New System.Drawing.Point(6, 136)
        Me.chkMovieTrailerNMJ.Name = "chkMovieTrailerNMJ"
        Me.chkMovieTrailerNMJ.Size = New System.Drawing.Size(57, 17)
        Me.chkMovieTrailerNMJ.TabIndex = 14
        Me.chkMovieTrailerNMJ.Text = "Trailer"
        Me.chkMovieTrailerNMJ.UseVisualStyleBackColor = true
        '
        'chkMovieFanartNMJ
        '
        Me.chkMovieFanartNMJ.AutoSize = true
        Me.chkMovieFanartNMJ.Enabled = false
        Me.chkMovieFanartNMJ.Location = New System.Drawing.Point(6, 90)
        Me.chkMovieFanartNMJ.Name = "chkMovieFanartNMJ"
        Me.chkMovieFanartNMJ.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieFanartNMJ.TabIndex = 13
        Me.chkMovieFanartNMJ.Text = "Fanart"
        Me.chkMovieFanartNMJ.UseVisualStyleBackColor = true
        '
        'chkMoviePosterNMJ
        '
        Me.chkMoviePosterNMJ.AutoSize = true
        Me.chkMoviePosterNMJ.Enabled = false
        Me.chkMoviePosterNMJ.Location = New System.Drawing.Point(6, 67)
        Me.chkMoviePosterNMJ.Name = "chkMoviePosterNMJ"
        Me.chkMoviePosterNMJ.Size = New System.Drawing.Size(58, 17)
        Me.chkMoviePosterNMJ.TabIndex = 12
        Me.chkMoviePosterNMJ.Text = "Poster"
        Me.chkMoviePosterNMJ.UseVisualStyleBackColor = true
        '
        'chkMovieNFONMJ
        '
        Me.chkMovieNFONMJ.AutoSize = true
        Me.chkMovieNFONMJ.Enabled = false
        Me.chkMovieNFONMJ.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieNFONMJ.Name = "chkMovieNFONMJ"
        Me.chkMovieNFONMJ.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieNFONMJ.TabIndex = 11
        Me.chkMovieNFONMJ.Text = "NFO"
        Me.chkMovieNFONMJ.UseVisualStyleBackColor = true
        '
        'gbMovieYAMJ
        '
        Me.gbMovieYAMJ.Controls.Add(Me.chkMovieUseYAMJ)
        Me.gbMovieYAMJ.Controls.Add(Me.chkMovieBannerYAMJ)
        Me.gbMovieYAMJ.Controls.Add(Me.chkMovieTrailerYAMJ)
        Me.gbMovieYAMJ.Controls.Add(Me.chkMovieFanartYAMJ)
        Me.gbMovieYAMJ.Controls.Add(Me.chkMoviePosterYAMJ)
        Me.gbMovieYAMJ.Controls.Add(Me.chkMovieNFOYAMJ)
        Me.gbMovieYAMJ.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbMovieYAMJ.Location = New System.Drawing.Point(6, 6)
        Me.gbMovieYAMJ.Name = "gbMovieYAMJ"
        Me.gbMovieYAMJ.Size = New System.Drawing.Size(110, 324)
        Me.gbMovieYAMJ.TabIndex = 3
        Me.gbMovieYAMJ.TabStop = false
        Me.gbMovieYAMJ.Text = "YAMJ"
        '
        'chkMovieUseYAMJ
        '
        Me.chkMovieUseYAMJ.AutoSize = true
        Me.chkMovieUseYAMJ.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieUseYAMJ.Name = "chkMovieUseYAMJ"
        Me.chkMovieUseYAMJ.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieUseYAMJ.TabIndex = 16
        Me.chkMovieUseYAMJ.Text = "Use"
        Me.chkMovieUseYAMJ.UseVisualStyleBackColor = true
        '
        'chkMovieBannerYAMJ
        '
        Me.chkMovieBannerYAMJ.AutoSize = true
        Me.chkMovieBannerYAMJ.Enabled = false
        Me.chkMovieBannerYAMJ.Location = New System.Drawing.Point(6, 113)
        Me.chkMovieBannerYAMJ.Name = "chkMovieBannerYAMJ"
        Me.chkMovieBannerYAMJ.Size = New System.Drawing.Size(63, 17)
        Me.chkMovieBannerYAMJ.TabIndex = 15
        Me.chkMovieBannerYAMJ.Text = "Banner"
        Me.chkMovieBannerYAMJ.UseVisualStyleBackColor = true
        '
        'chkMovieTrailerYAMJ
        '
        Me.chkMovieTrailerYAMJ.AutoSize = true
        Me.chkMovieTrailerYAMJ.Enabled = false
        Me.chkMovieTrailerYAMJ.Location = New System.Drawing.Point(6, 136)
        Me.chkMovieTrailerYAMJ.Name = "chkMovieTrailerYAMJ"
        Me.chkMovieTrailerYAMJ.Size = New System.Drawing.Size(57, 17)
        Me.chkMovieTrailerYAMJ.TabIndex = 14
        Me.chkMovieTrailerYAMJ.Text = "Trailer"
        Me.chkMovieTrailerYAMJ.UseVisualStyleBackColor = true
        '
        'chkMovieFanartYAMJ
        '
        Me.chkMovieFanartYAMJ.AutoSize = true
        Me.chkMovieFanartYAMJ.Enabled = false
        Me.chkMovieFanartYAMJ.Location = New System.Drawing.Point(6, 90)
        Me.chkMovieFanartYAMJ.Name = "chkMovieFanartYAMJ"
        Me.chkMovieFanartYAMJ.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieFanartYAMJ.TabIndex = 13
        Me.chkMovieFanartYAMJ.Text = "Fanart"
        Me.chkMovieFanartYAMJ.UseVisualStyleBackColor = true
        '
        'chkMoviePosterYAMJ
        '
        Me.chkMoviePosterYAMJ.AutoSize = true
        Me.chkMoviePosterYAMJ.Enabled = false
        Me.chkMoviePosterYAMJ.Location = New System.Drawing.Point(6, 67)
        Me.chkMoviePosterYAMJ.Name = "chkMoviePosterYAMJ"
        Me.chkMoviePosterYAMJ.Size = New System.Drawing.Size(58, 17)
        Me.chkMoviePosterYAMJ.TabIndex = 12
        Me.chkMoviePosterYAMJ.Text = "Poster"
        Me.chkMoviePosterYAMJ.UseVisualStyleBackColor = true
        '
        'chkMovieNFOYAMJ
        '
        Me.chkMovieNFOYAMJ.AutoSize = true
        Me.chkMovieNFOYAMJ.Enabled = false
        Me.chkMovieNFOYAMJ.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieNFOYAMJ.Name = "chkMovieNFOYAMJ"
        Me.chkMovieNFOYAMJ.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieNFOYAMJ.TabIndex = 11
        Me.chkMovieNFOYAMJ.Text = "NFO"
        Me.chkMovieNFOYAMJ.UseVisualStyleBackColor = true
        '
        'tpMovieFileNamingBoxee
        '
        Me.tpMovieFileNamingBoxee.Controls.Add(Me.gbMovieBoxee)
        Me.tpMovieFileNamingBoxee.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieFileNamingBoxee.Name = "tpMovieFileNamingBoxee"
        Me.tpMovieFileNamingBoxee.Size = New System.Drawing.Size(505, 336)
        Me.tpMovieFileNamingBoxee.TabIndex = 4
        Me.tpMovieFileNamingBoxee.Text = "Boxee"
        Me.tpMovieFileNamingBoxee.UseVisualStyleBackColor = true
        '
        'gbMovieBoxee
        '
        Me.gbMovieBoxee.Controls.Add(Me.chkMovieUseBoxee)
        Me.gbMovieBoxee.Controls.Add(Me.chkMovieFanartBoxee)
        Me.gbMovieBoxee.Controls.Add(Me.chkMoviePosterBoxee)
        Me.gbMovieBoxee.Controls.Add(Me.chkMovieNFOBoxee)
        Me.gbMovieBoxee.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbMovieBoxee.Location = New System.Drawing.Point(6, 6)
        Me.gbMovieBoxee.Name = "gbMovieBoxee"
        Me.gbMovieBoxee.Size = New System.Drawing.Size(110, 324)
        Me.gbMovieBoxee.TabIndex = 4
        Me.gbMovieBoxee.TabStop = false
        Me.gbMovieBoxee.Text = "Boxee"
        '
        'chkMovieUseBoxee
        '
        Me.chkMovieUseBoxee.AutoSize = true
        Me.chkMovieUseBoxee.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieUseBoxee.Name = "chkMovieUseBoxee"
        Me.chkMovieUseBoxee.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieUseBoxee.TabIndex = 16
        Me.chkMovieUseBoxee.Text = "Use"
        Me.chkMovieUseBoxee.UseVisualStyleBackColor = true
        '
        'chkMovieFanartBoxee
        '
        Me.chkMovieFanartBoxee.AutoSize = true
        Me.chkMovieFanartBoxee.Enabled = false
        Me.chkMovieFanartBoxee.Location = New System.Drawing.Point(6, 90)
        Me.chkMovieFanartBoxee.Name = "chkMovieFanartBoxee"
        Me.chkMovieFanartBoxee.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieFanartBoxee.TabIndex = 13
        Me.chkMovieFanartBoxee.Text = "Fanart"
        Me.chkMovieFanartBoxee.UseVisualStyleBackColor = true
        '
        'chkMoviePosterBoxee
        '
        Me.chkMoviePosterBoxee.AutoSize = true
        Me.chkMoviePosterBoxee.Enabled = false
        Me.chkMoviePosterBoxee.Location = New System.Drawing.Point(6, 67)
        Me.chkMoviePosterBoxee.Name = "chkMoviePosterBoxee"
        Me.chkMoviePosterBoxee.Size = New System.Drawing.Size(58, 17)
        Me.chkMoviePosterBoxee.TabIndex = 12
        Me.chkMoviePosterBoxee.Text = "Poster"
        Me.chkMoviePosterBoxee.UseVisualStyleBackColor = true
        '
        'chkMovieNFOBoxee
        '
        Me.chkMovieNFOBoxee.AutoSize = true
        Me.chkMovieNFOBoxee.Enabled = false
        Me.chkMovieNFOBoxee.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieNFOBoxee.Name = "chkMovieNFOBoxee"
        Me.chkMovieNFOBoxee.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieNFOBoxee.TabIndex = 11
        Me.chkMovieNFOBoxee.Text = "NFO"
        Me.chkMovieNFOBoxee.UseVisualStyleBackColor = true
        '
        'tpMovieFileNamingExpert
        '
        Me.tpMovieFileNamingExpert.Controls.Add(Me.gbMovieExpert)
        Me.tpMovieFileNamingExpert.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieFileNamingExpert.Name = "tpMovieFileNamingExpert"
        Me.tpMovieFileNamingExpert.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMovieFileNamingExpert.Size = New System.Drawing.Size(505, 336)
        Me.tpMovieFileNamingExpert.TabIndex = 2
        Me.tpMovieFileNamingExpert.Text = "Expert"
        Me.tpMovieFileNamingExpert.UseVisualStyleBackColor = true
        '
        'gbMovieExpert
        '
        Me.gbMovieExpert.Controls.Add(Me.tcMovieFileNamingExpert)
        Me.gbMovieExpert.Controls.Add(Me.chkMovieUseExpert)
        Me.gbMovieExpert.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gbMovieExpert.Location = New System.Drawing.Point(6, 6)
        Me.gbMovieExpert.Name = "gbMovieExpert"
        Me.gbMovieExpert.Size = New System.Drawing.Size(493, 324)
        Me.gbMovieExpert.TabIndex = 7
        Me.gbMovieExpert.TabStop = false
        Me.gbMovieExpert.Text = "Expert Settings"
        '
        'tcMovieFileNamingExpert
        '
        Me.tcMovieFileNamingExpert.Controls.Add(Me.tpMovieFileNamingExpertSingle)
        Me.tcMovieFileNamingExpert.Controls.Add(Me.tpMovieFileNamingExpertMulti)
        Me.tcMovieFileNamingExpert.Controls.Add(Me.tpMovieFileNamingExpertVTS)
        Me.tcMovieFileNamingExpert.Controls.Add(Me.tpMovieFileNamingExpertBDMV)
        Me.tcMovieFileNamingExpert.Location = New System.Drawing.Point(6, 44)
        Me.tcMovieFileNamingExpert.Name = "tcMovieFileNamingExpert"
        Me.tcMovieFileNamingExpert.SelectedIndex = 0
        Me.tcMovieFileNamingExpert.Size = New System.Drawing.Size(481, 280)
        Me.tcMovieFileNamingExpert.TabIndex = 2
        '
        'tpMovieFileNamingExpertSingle
        '
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.gbMovieExpertSingleOptionalSettings)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.gbMovieExpertSingleOptionalImages)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.lblMovieClearArtExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.txtMoviePosterExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.txtMovieFanartExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.txtMovieTrailerExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.txtMovieBannerExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.txtMovieClearLogoExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.txtMovieClearArtExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.txtMovieLandscapeExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.txtMovieDiscArtExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.lblMovieLandscapeExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.lblMovieDiscArtExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.lblMovieBannerExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.lblMovieTrailerExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.lblMovieClearLogoExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.lblMovieFanartExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.lblMoviePosterExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.txtMovieNFOExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Controls.Add(Me.lblMovieNFOExpertSingle)
        Me.tpMovieFileNamingExpertSingle.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieFileNamingExpertSingle.Name = "tpMovieFileNamingExpertSingle"
        Me.tpMovieFileNamingExpertSingle.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMovieFileNamingExpertSingle.Size = New System.Drawing.Size(473, 254)
        Me.tpMovieFileNamingExpertSingle.TabIndex = 0
        Me.tpMovieFileNamingExpertSingle.Text = "Single"
        Me.tpMovieFileNamingExpertSingle.UseVisualStyleBackColor = true
        '
        'gbMovieExpertSingleOptionalSettings
        '
        Me.gbMovieExpertSingleOptionalSettings.Controls.Add(Me.chkMovieUnstackExpertSingle)
        Me.gbMovieExpertSingleOptionalSettings.Controls.Add(Me.chkMovieStackExpertSingle)
        Me.gbMovieExpertSingleOptionalSettings.Controls.Add(Me.chkMovieXBMCTrailerFormatExpertSingle)
        Me.gbMovieExpertSingleOptionalSettings.Location = New System.Drawing.Point(297, 6)
        Me.gbMovieExpertSingleOptionalSettings.Name = "gbMovieExpertSingleOptionalSettings"
        Me.gbMovieExpertSingleOptionalSettings.Size = New System.Drawing.Size(170, 93)
        Me.gbMovieExpertSingleOptionalSettings.TabIndex = 12
        Me.gbMovieExpertSingleOptionalSettings.TabStop = false
        Me.gbMovieExpertSingleOptionalSettings.Text = "Optional Settings"
        '
        'chkMovieUnstackExpertSingle
        '
        Me.chkMovieUnstackExpertSingle.AutoSize = true
        Me.chkMovieUnstackExpertSingle.Enabled = false
        Me.chkMovieUnstackExpertSingle.Location = New System.Drawing.Point(6, 67)
        Me.chkMovieUnstackExpertSingle.Name = "chkMovieUnstackExpertSingle"
        Me.chkMovieUnstackExpertSingle.Size = New System.Drawing.Size(128, 17)
        Me.chkMovieUnstackExpertSingle.TabIndex = 3
        Me.chkMovieUnstackExpertSingle.Text = "also save unstacked"
        Me.chkMovieUnstackExpertSingle.UseVisualStyleBackColor = true
        '
        'chkMovieStackExpertSingle
        '
        Me.chkMovieStackExpertSingle.AutoSize = true
        Me.chkMovieStackExpertSingle.Enabled = false
        Me.chkMovieStackExpertSingle.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieStackExpertSingle.Name = "chkMovieStackExpertSingle"
        Me.chkMovieStackExpertSingle.Size = New System.Drawing.Size(116, 17)
        Me.chkMovieStackExpertSingle.TabIndex = 2
        Me.chkMovieStackExpertSingle.Text = "Stack <filename>"
        Me.chkMovieStackExpertSingle.UseVisualStyleBackColor = true
        '
        'chkMovieXBMCTrailerFormatExpertSingle
        '
        Me.chkMovieXBMCTrailerFormatExpertSingle.AutoSize = true
        Me.chkMovieXBMCTrailerFormatExpertSingle.Enabled = false
        Me.chkMovieXBMCTrailerFormatExpertSingle.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieXBMCTrailerFormatExpertSingle.Name = "chkMovieXBMCTrailerFormatExpertSingle"
        Me.chkMovieXBMCTrailerFormatExpertSingle.Size = New System.Drawing.Size(129, 17)
        Me.chkMovieXBMCTrailerFormatExpertSingle.TabIndex = 1
        Me.chkMovieXBMCTrailerFormatExpertSingle.Text = "XBMC Trailer Format"
        Me.chkMovieXBMCTrailerFormatExpertSingle.UseVisualStyleBackColor = true
        '
        'gbMovieExpertSingleOptionalImages
        '
        Me.gbMovieExpertSingleOptionalImages.Controls.Add(Me.txtMovieActorThumbsExtExpertSingle)
        Me.gbMovieExpertSingleOptionalImages.Controls.Add(Me.chkMovieActorThumbsExpertSingle)
        Me.gbMovieExpertSingleOptionalImages.Controls.Add(Me.chkMovieExtrafanartsExpertSingle)
        Me.gbMovieExpertSingleOptionalImages.Controls.Add(Me.chkMovieExtrathumbsExpertSingle)
        Me.gbMovieExpertSingleOptionalImages.Location = New System.Drawing.Point(297, 105)
        Me.gbMovieExpertSingleOptionalImages.Name = "gbMovieExpertSingleOptionalImages"
        Me.gbMovieExpertSingleOptionalImages.Size = New System.Drawing.Size(170, 93)
        Me.gbMovieExpertSingleOptionalImages.TabIndex = 13
        Me.gbMovieExpertSingleOptionalImages.TabStop = false
        Me.gbMovieExpertSingleOptionalImages.Text = "Optional Images"
        '
        'txtMovieActorThumbsExtExpertSingle
        '
        Me.txtMovieActorThumbsExtExpertSingle.Enabled = false
        Me.txtMovieActorThumbsExtExpertSingle.Location = New System.Drawing.Point(108, 19)
        Me.txtMovieActorThumbsExtExpertSingle.Name = "txtMovieActorThumbsExtExpertSingle"
        Me.txtMovieActorThumbsExtExpertSingle.Size = New System.Drawing.Size(46, 22)
        Me.txtMovieActorThumbsExtExpertSingle.TabIndex = 2
        '
        'chkMovieActorThumbsExpertSingle
        '
        Me.chkMovieActorThumbsExpertSingle.AutoSize = true
        Me.chkMovieActorThumbsExpertSingle.Enabled = false
        Me.chkMovieActorThumbsExpertSingle.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieActorThumbsExpertSingle.Name = "chkMovieActorThumbsExpertSingle"
        Me.chkMovieActorThumbsExpertSingle.Size = New System.Drawing.Size(96, 17)
        Me.chkMovieActorThumbsExpertSingle.TabIndex = 1
        Me.chkMovieActorThumbsExpertSingle.Text = "Actor Thumbs"
        Me.chkMovieActorThumbsExpertSingle.UseVisualStyleBackColor = true
        '
        'chkMovieExtrafanartsExpertSingle
        '
        Me.chkMovieExtrafanartsExpertSingle.AutoSize = true
        Me.chkMovieExtrafanartsExpertSingle.Enabled = false
        Me.chkMovieExtrafanartsExpertSingle.Location = New System.Drawing.Point(6, 67)
        Me.chkMovieExtrafanartsExpertSingle.Name = "chkMovieExtrafanartsExpertSingle"
        Me.chkMovieExtrafanartsExpertSingle.Size = New System.Drawing.Size(87, 17)
        Me.chkMovieExtrafanartsExpertSingle.TabIndex = 4
        Me.chkMovieExtrafanartsExpertSingle.Text = "Extrafanarts"
        Me.chkMovieExtrafanartsExpertSingle.UseVisualStyleBackColor = true
        '
        'chkMovieExtrathumbsExpertSingle
        '
        Me.chkMovieExtrathumbsExpertSingle.AutoSize = true
        Me.chkMovieExtrathumbsExpertSingle.Enabled = false
        Me.chkMovieExtrathumbsExpertSingle.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieExtrathumbsExpertSingle.Name = "chkMovieExtrathumbsExpertSingle"
        Me.chkMovieExtrathumbsExpertSingle.Size = New System.Drawing.Size(90, 17)
        Me.chkMovieExtrathumbsExpertSingle.TabIndex = 3
        Me.chkMovieExtrathumbsExpertSingle.Text = "Extrathumbs"
        Me.chkMovieExtrathumbsExpertSingle.UseVisualStyleBackColor = true
        '
        'lblMovieClearArtExpertSingle
        '
        Me.lblMovieClearArtExpertSingle.AutoSize = true
        Me.lblMovieClearArtExpertSingle.Location = New System.Drawing.Point(6, 171)
        Me.lblMovieClearArtExpertSingle.Name = "lblMovieClearArtExpertSingle"
        Me.lblMovieClearArtExpertSingle.Size = New System.Drawing.Size(48, 13)
        Me.lblMovieClearArtExpertSingle.TabIndex = 28
        Me.lblMovieClearArtExpertSingle.Text = "ClearArt"
        '
        'txtMoviePosterExpertSingle
        '
        Me.txtMoviePosterExpertSingle.Enabled = false
        Me.txtMoviePosterExpertSingle.Location = New System.Drawing.Point(71, 33)
        Me.txtMoviePosterExpertSingle.Name = "txtMoviePosterExpertSingle"
        Me.txtMoviePosterExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMoviePosterExpertSingle.TabIndex = 4
        '
        'txtMovieFanartExpertSingle
        '
        Me.txtMovieFanartExpertSingle.Enabled = false
        Me.txtMovieFanartExpertSingle.Location = New System.Drawing.Point(71, 60)
        Me.txtMovieFanartExpertSingle.Name = "txtMovieFanartExpertSingle"
        Me.txtMovieFanartExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieFanartExpertSingle.TabIndex = 5
        '
        'txtMovieTrailerExpertSingle
        '
        Me.txtMovieTrailerExpertSingle.Enabled = false
        Me.txtMovieTrailerExpertSingle.Location = New System.Drawing.Point(71, 87)
        Me.txtMovieTrailerExpertSingle.Name = "txtMovieTrailerExpertSingle"
        Me.txtMovieTrailerExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieTrailerExpertSingle.TabIndex = 6
        '
        'txtMovieBannerExpertSingle
        '
        Me.txtMovieBannerExpertSingle.Enabled = false
        Me.txtMovieBannerExpertSingle.Location = New System.Drawing.Point(71, 114)
        Me.txtMovieBannerExpertSingle.Name = "txtMovieBannerExpertSingle"
        Me.txtMovieBannerExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieBannerExpertSingle.TabIndex = 7
        '
        'txtMovieClearLogoExpertSingle
        '
        Me.txtMovieClearLogoExpertSingle.Enabled = false
        Me.txtMovieClearLogoExpertSingle.Location = New System.Drawing.Point(71, 141)
        Me.txtMovieClearLogoExpertSingle.Name = "txtMovieClearLogoExpertSingle"
        Me.txtMovieClearLogoExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieClearLogoExpertSingle.TabIndex = 8
        '
        'txtMovieClearArtExpertSingle
        '
        Me.txtMovieClearArtExpertSingle.Enabled = false
        Me.txtMovieClearArtExpertSingle.Location = New System.Drawing.Point(71, 168)
        Me.txtMovieClearArtExpertSingle.Name = "txtMovieClearArtExpertSingle"
        Me.txtMovieClearArtExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieClearArtExpertSingle.TabIndex = 9
        '
        'txtMovieLandscapeExpertSingle
        '
        Me.txtMovieLandscapeExpertSingle.Enabled = false
        Me.txtMovieLandscapeExpertSingle.Location = New System.Drawing.Point(71, 222)
        Me.txtMovieLandscapeExpertSingle.Name = "txtMovieLandscapeExpertSingle"
        Me.txtMovieLandscapeExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieLandscapeExpertSingle.TabIndex = 11
        '
        'txtMovieDiscArtExpertSingle
        '
        Me.txtMovieDiscArtExpertSingle.Enabled = false
        Me.txtMovieDiscArtExpertSingle.Location = New System.Drawing.Point(71, 195)
        Me.txtMovieDiscArtExpertSingle.Name = "txtMovieDiscArtExpertSingle"
        Me.txtMovieDiscArtExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieDiscArtExpertSingle.TabIndex = 10
        '
        'lblMovieLandscapeExpertSingle
        '
        Me.lblMovieLandscapeExpertSingle.AutoSize = true
        Me.lblMovieLandscapeExpertSingle.Location = New System.Drawing.Point(6, 225)
        Me.lblMovieLandscapeExpertSingle.Name = "lblMovieLandscapeExpertSingle"
        Me.lblMovieLandscapeExpertSingle.Size = New System.Drawing.Size(61, 13)
        Me.lblMovieLandscapeExpertSingle.TabIndex = 19
        Me.lblMovieLandscapeExpertSingle.Text = "Landscape"
        '
        'lblMovieDiscArtExpertSingle
        '
        Me.lblMovieDiscArtExpertSingle.AutoSize = true
        Me.lblMovieDiscArtExpertSingle.Location = New System.Drawing.Point(6, 198)
        Me.lblMovieDiscArtExpertSingle.Name = "lblMovieDiscArtExpertSingle"
        Me.lblMovieDiscArtExpertSingle.Size = New System.Drawing.Size(43, 13)
        Me.lblMovieDiscArtExpertSingle.TabIndex = 18
        Me.lblMovieDiscArtExpertSingle.Text = "DiscArt"
        '
        'lblMovieBannerExpertSingle
        '
        Me.lblMovieBannerExpertSingle.AutoSize = true
        Me.lblMovieBannerExpertSingle.Location = New System.Drawing.Point(6, 117)
        Me.lblMovieBannerExpertSingle.Name = "lblMovieBannerExpertSingle"
        Me.lblMovieBannerExpertSingle.Size = New System.Drawing.Size(44, 13)
        Me.lblMovieBannerExpertSingle.TabIndex = 17
        Me.lblMovieBannerExpertSingle.Text = "Banner"
        '
        'lblMovieTrailerExpertSingle
        '
        Me.lblMovieTrailerExpertSingle.AutoSize = true
        Me.lblMovieTrailerExpertSingle.Location = New System.Drawing.Point(6, 90)
        Me.lblMovieTrailerExpertSingle.Name = "lblMovieTrailerExpertSingle"
        Me.lblMovieTrailerExpertSingle.Size = New System.Drawing.Size(38, 13)
        Me.lblMovieTrailerExpertSingle.TabIndex = 13
        Me.lblMovieTrailerExpertSingle.Text = "Trailer"
        '
        'lblMovieClearLogoExpertSingle
        '
        Me.lblMovieClearLogoExpertSingle.AutoSize = true
        Me.lblMovieClearLogoExpertSingle.Location = New System.Drawing.Point(6, 144)
        Me.lblMovieClearLogoExpertSingle.Name = "lblMovieClearLogoExpertSingle"
        Me.lblMovieClearLogoExpertSingle.Size = New System.Drawing.Size(59, 13)
        Me.lblMovieClearLogoExpertSingle.TabIndex = 12
        Me.lblMovieClearLogoExpertSingle.Text = "ClearLogo"
        '
        'lblMovieFanartExpertSingle
        '
        Me.lblMovieFanartExpertSingle.AutoSize = true
        Me.lblMovieFanartExpertSingle.Location = New System.Drawing.Point(6, 64)
        Me.lblMovieFanartExpertSingle.Name = "lblMovieFanartExpertSingle"
        Me.lblMovieFanartExpertSingle.Size = New System.Drawing.Size(40, 13)
        Me.lblMovieFanartExpertSingle.TabIndex = 11
        Me.lblMovieFanartExpertSingle.Text = "Fanart"
        '
        'lblMoviePosterExpertSingle
        '
        Me.lblMoviePosterExpertSingle.AutoSize = true
        Me.lblMoviePosterExpertSingle.Location = New System.Drawing.Point(6, 36)
        Me.lblMoviePosterExpertSingle.Name = "lblMoviePosterExpertSingle"
        Me.lblMoviePosterExpertSingle.Size = New System.Drawing.Size(39, 13)
        Me.lblMoviePosterExpertSingle.TabIndex = 10
        Me.lblMoviePosterExpertSingle.Text = "Poster"
        '
        'txtMovieNFOExpertSingle
        '
        Me.txtMovieNFOExpertSingle.Enabled = false
        Me.txtMovieNFOExpertSingle.Location = New System.Drawing.Point(71, 6)
        Me.txtMovieNFOExpertSingle.Name = "txtMovieNFOExpertSingle"
        Me.txtMovieNFOExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieNFOExpertSingle.TabIndex = 3
        '
        'lblMovieNFOExpertSingle
        '
        Me.lblMovieNFOExpertSingle.AutoSize = true
        Me.lblMovieNFOExpertSingle.Location = New System.Drawing.Point(6, 9)
        Me.lblMovieNFOExpertSingle.Name = "lblMovieNFOExpertSingle"
        Me.lblMovieNFOExpertSingle.Size = New System.Drawing.Size(30, 13)
        Me.lblMovieNFOExpertSingle.TabIndex = 9
        Me.lblMovieNFOExpertSingle.Text = "NFO"
        '
        'tpMovieFileNamingExpertMulti
        '
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.gbMovieExpertMultiOptionalImages)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.gbMovieExpertMultiOptionalSettings)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.txtMoviePosterExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.txtMovieFanartExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.lblMovieClearArtExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.txtMovieTrailerExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.txtMovieBannerExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.txtMovieClearLogoExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.txtMovieClearArtExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.txtMovieLandscapeExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.txtMovieDiscArtExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.lblMovieLandscapeExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.lblMovieDiscArtExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.lblMovieBannerExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.lblMovieTrailerExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.lblMovieClearLogoExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.lblMovieFanartExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.lblMoviePosterExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.txtMovieNFOExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Controls.Add(Me.lblMovieNFOExpertMulti)
        Me.tpMovieFileNamingExpertMulti.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieFileNamingExpertMulti.Name = "tpMovieFileNamingExpertMulti"
        Me.tpMovieFileNamingExpertMulti.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMovieFileNamingExpertMulti.Size = New System.Drawing.Size(473, 254)
        Me.tpMovieFileNamingExpertMulti.TabIndex = 1
        Me.tpMovieFileNamingExpertMulti.Text = "Multi"
        Me.tpMovieFileNamingExpertMulti.UseVisualStyleBackColor = true
        '
        'gbMovieExpertMultiOptionalImages
        '
        Me.gbMovieExpertMultiOptionalImages.Controls.Add(Me.txtMovieActorThumbsExtExpertMulti)
        Me.gbMovieExpertMultiOptionalImages.Controls.Add(Me.chkMovieActorThumbsExpertMulti)
        Me.gbMovieExpertMultiOptionalImages.Location = New System.Drawing.Point(307, 105)
        Me.gbMovieExpertMultiOptionalImages.Name = "gbMovieExpertMultiOptionalImages"
        Me.gbMovieExpertMultiOptionalImages.Size = New System.Drawing.Size(160, 52)
        Me.gbMovieExpertMultiOptionalImages.TabIndex = 11
        Me.gbMovieExpertMultiOptionalImages.TabStop = false
        Me.gbMovieExpertMultiOptionalImages.Text = "Optional Images"
        '
        'txtMovieActorThumbsExtExpertMulti
        '
        Me.txtMovieActorThumbsExtExpertMulti.Enabled = false
        Me.txtMovieActorThumbsExtExpertMulti.Location = New System.Drawing.Point(108, 19)
        Me.txtMovieActorThumbsExtExpertMulti.Name = "txtMovieActorThumbsExtExpertMulti"
        Me.txtMovieActorThumbsExtExpertMulti.Size = New System.Drawing.Size(46, 22)
        Me.txtMovieActorThumbsExtExpertMulti.TabIndex = 2
        '
        'chkMovieActorThumbsExpertMulti
        '
        Me.chkMovieActorThumbsExpertMulti.AutoSize = true
        Me.chkMovieActorThumbsExpertMulti.Enabled = false
        Me.chkMovieActorThumbsExpertMulti.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieActorThumbsExpertMulti.Name = "chkMovieActorThumbsExpertMulti"
        Me.chkMovieActorThumbsExpertMulti.Size = New System.Drawing.Size(96, 17)
        Me.chkMovieActorThumbsExpertMulti.TabIndex = 1
        Me.chkMovieActorThumbsExpertMulti.Text = "Actor Thumbs"
        Me.chkMovieActorThumbsExpertMulti.UseVisualStyleBackColor = true
        '
        'gbMovieExpertMultiOptionalSettings
        '
        Me.gbMovieExpertMultiOptionalSettings.Controls.Add(Me.chkMovieUnstackExpertMulti)
        Me.gbMovieExpertMultiOptionalSettings.Controls.Add(Me.chkMovieStackExpertMulti)
        Me.gbMovieExpertMultiOptionalSettings.Controls.Add(Me.chkMovieXBMCTrailerFormatExpertMulti)
        Me.gbMovieExpertMultiOptionalSettings.Location = New System.Drawing.Point(307, 6)
        Me.gbMovieExpertMultiOptionalSettings.Name = "gbMovieExpertMultiOptionalSettings"
        Me.gbMovieExpertMultiOptionalSettings.Size = New System.Drawing.Size(160, 93)
        Me.gbMovieExpertMultiOptionalSettings.TabIndex = 10
        Me.gbMovieExpertMultiOptionalSettings.TabStop = false
        Me.gbMovieExpertMultiOptionalSettings.Text = "Optional Settings"
        '
        'chkMovieUnstackExpertMulti
        '
        Me.chkMovieUnstackExpertMulti.AutoSize = true
        Me.chkMovieUnstackExpertMulti.Enabled = false
        Me.chkMovieUnstackExpertMulti.Location = New System.Drawing.Point(6, 67)
        Me.chkMovieUnstackExpertMulti.Name = "chkMovieUnstackExpertMulti"
        Me.chkMovieUnstackExpertMulti.Size = New System.Drawing.Size(128, 17)
        Me.chkMovieUnstackExpertMulti.TabIndex = 3
        Me.chkMovieUnstackExpertMulti.Text = "also save unstacked"
        Me.chkMovieUnstackExpertMulti.UseVisualStyleBackColor = true
        '
        'chkMovieStackExpertMulti
        '
        Me.chkMovieStackExpertMulti.AutoSize = true
        Me.chkMovieStackExpertMulti.Enabled = false
        Me.chkMovieStackExpertMulti.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieStackExpertMulti.Name = "chkMovieStackExpertMulti"
        Me.chkMovieStackExpertMulti.Size = New System.Drawing.Size(116, 17)
        Me.chkMovieStackExpertMulti.TabIndex = 2
        Me.chkMovieStackExpertMulti.Text = "Stack <filename>"
        Me.chkMovieStackExpertMulti.UseVisualStyleBackColor = true
        '
        'chkMovieXBMCTrailerFormatExpertMulti
        '
        Me.chkMovieXBMCTrailerFormatExpertMulti.AutoSize = true
        Me.chkMovieXBMCTrailerFormatExpertMulti.Enabled = false
        Me.chkMovieXBMCTrailerFormatExpertMulti.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieXBMCTrailerFormatExpertMulti.Name = "chkMovieXBMCTrailerFormatExpertMulti"
        Me.chkMovieXBMCTrailerFormatExpertMulti.Size = New System.Drawing.Size(129, 17)
        Me.chkMovieXBMCTrailerFormatExpertMulti.TabIndex = 1
        Me.chkMovieXBMCTrailerFormatExpertMulti.Text = "XBMC Trailer Format"
        Me.chkMovieXBMCTrailerFormatExpertMulti.UseVisualStyleBackColor = true
        '
        'txtMoviePosterExpertMulti
        '
        Me.txtMoviePosterExpertMulti.Enabled = false
        Me.txtMoviePosterExpertMulti.Location = New System.Drawing.Point(71, 33)
        Me.txtMoviePosterExpertMulti.Name = "txtMoviePosterExpertMulti"
        Me.txtMoviePosterExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMoviePosterExpertMulti.TabIndex = 2
        '
        'txtMovieFanartExpertMulti
        '
        Me.txtMovieFanartExpertMulti.Enabled = false
        Me.txtMovieFanartExpertMulti.Location = New System.Drawing.Point(71, 60)
        Me.txtMovieFanartExpertMulti.Name = "txtMovieFanartExpertMulti"
        Me.txtMovieFanartExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieFanartExpertMulti.TabIndex = 3
        '
        'lblMovieClearArtExpertMulti
        '
        Me.lblMovieClearArtExpertMulti.AutoSize = true
        Me.lblMovieClearArtExpertMulti.Location = New System.Drawing.Point(6, 171)
        Me.lblMovieClearArtExpertMulti.Name = "lblMovieClearArtExpertMulti"
        Me.lblMovieClearArtExpertMulti.Size = New System.Drawing.Size(48, 13)
        Me.lblMovieClearArtExpertMulti.TabIndex = 51
        Me.lblMovieClearArtExpertMulti.Text = "ClearArt"
        '
        'txtMovieTrailerExpertMulti
        '
        Me.txtMovieTrailerExpertMulti.Enabled = false
        Me.txtMovieTrailerExpertMulti.Location = New System.Drawing.Point(71, 87)
        Me.txtMovieTrailerExpertMulti.Name = "txtMovieTrailerExpertMulti"
        Me.txtMovieTrailerExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieTrailerExpertMulti.TabIndex = 4
        '
        'txtMovieBannerExpertMulti
        '
        Me.txtMovieBannerExpertMulti.Enabled = false
        Me.txtMovieBannerExpertMulti.Location = New System.Drawing.Point(71, 114)
        Me.txtMovieBannerExpertMulti.Name = "txtMovieBannerExpertMulti"
        Me.txtMovieBannerExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieBannerExpertMulti.TabIndex = 5
        '
        'txtMovieClearLogoExpertMulti
        '
        Me.txtMovieClearLogoExpertMulti.Enabled = false
        Me.txtMovieClearLogoExpertMulti.Location = New System.Drawing.Point(71, 141)
        Me.txtMovieClearLogoExpertMulti.Name = "txtMovieClearLogoExpertMulti"
        Me.txtMovieClearLogoExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieClearLogoExpertMulti.TabIndex = 6
        '
        'txtMovieClearArtExpertMulti
        '
        Me.txtMovieClearArtExpertMulti.Enabled = false
        Me.txtMovieClearArtExpertMulti.Location = New System.Drawing.Point(71, 168)
        Me.txtMovieClearArtExpertMulti.Name = "txtMovieClearArtExpertMulti"
        Me.txtMovieClearArtExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieClearArtExpertMulti.TabIndex = 7
        '
        'txtMovieLandscapeExpertMulti
        '
        Me.txtMovieLandscapeExpertMulti.Enabled = false
        Me.txtMovieLandscapeExpertMulti.Location = New System.Drawing.Point(71, 222)
        Me.txtMovieLandscapeExpertMulti.Name = "txtMovieLandscapeExpertMulti"
        Me.txtMovieLandscapeExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieLandscapeExpertMulti.TabIndex = 9
        '
        'txtMovieDiscArtExpertMulti
        '
        Me.txtMovieDiscArtExpertMulti.Enabled = false
        Me.txtMovieDiscArtExpertMulti.Location = New System.Drawing.Point(71, 195)
        Me.txtMovieDiscArtExpertMulti.Name = "txtMovieDiscArtExpertMulti"
        Me.txtMovieDiscArtExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieDiscArtExpertMulti.TabIndex = 8
        '
        'lblMovieLandscapeExpertMulti
        '
        Me.lblMovieLandscapeExpertMulti.AutoSize = true
        Me.lblMovieLandscapeExpertMulti.Location = New System.Drawing.Point(6, 225)
        Me.lblMovieLandscapeExpertMulti.Name = "lblMovieLandscapeExpertMulti"
        Me.lblMovieLandscapeExpertMulti.Size = New System.Drawing.Size(61, 13)
        Me.lblMovieLandscapeExpertMulti.TabIndex = 42
        Me.lblMovieLandscapeExpertMulti.Text = "Landscape"
        '
        'lblMovieDiscArtExpertMulti
        '
        Me.lblMovieDiscArtExpertMulti.AutoSize = true
        Me.lblMovieDiscArtExpertMulti.Location = New System.Drawing.Point(6, 198)
        Me.lblMovieDiscArtExpertMulti.Name = "lblMovieDiscArtExpertMulti"
        Me.lblMovieDiscArtExpertMulti.Size = New System.Drawing.Size(43, 13)
        Me.lblMovieDiscArtExpertMulti.TabIndex = 41
        Me.lblMovieDiscArtExpertMulti.Text = "DiscArt"
        '
        'lblMovieBannerExpertMulti
        '
        Me.lblMovieBannerExpertMulti.AutoSize = true
        Me.lblMovieBannerExpertMulti.Location = New System.Drawing.Point(6, 117)
        Me.lblMovieBannerExpertMulti.Name = "lblMovieBannerExpertMulti"
        Me.lblMovieBannerExpertMulti.Size = New System.Drawing.Size(44, 13)
        Me.lblMovieBannerExpertMulti.TabIndex = 40
        Me.lblMovieBannerExpertMulti.Text = "Banner"
        '
        'lblMovieTrailerExpertMulti
        '
        Me.lblMovieTrailerExpertMulti.AutoSize = true
        Me.lblMovieTrailerExpertMulti.Location = New System.Drawing.Point(6, 90)
        Me.lblMovieTrailerExpertMulti.Name = "lblMovieTrailerExpertMulti"
        Me.lblMovieTrailerExpertMulti.Size = New System.Drawing.Size(38, 13)
        Me.lblMovieTrailerExpertMulti.TabIndex = 39
        Me.lblMovieTrailerExpertMulti.Text = "Trailer"
        '
        'lblMovieClearLogoExpertMulti
        '
        Me.lblMovieClearLogoExpertMulti.AutoSize = true
        Me.lblMovieClearLogoExpertMulti.Location = New System.Drawing.Point(6, 144)
        Me.lblMovieClearLogoExpertMulti.Name = "lblMovieClearLogoExpertMulti"
        Me.lblMovieClearLogoExpertMulti.Size = New System.Drawing.Size(59, 13)
        Me.lblMovieClearLogoExpertMulti.TabIndex = 38
        Me.lblMovieClearLogoExpertMulti.Text = "ClearLogo"
        '
        'lblMovieFanartExpertMulti
        '
        Me.lblMovieFanartExpertMulti.AutoSize = true
        Me.lblMovieFanartExpertMulti.Location = New System.Drawing.Point(6, 64)
        Me.lblMovieFanartExpertMulti.Name = "lblMovieFanartExpertMulti"
        Me.lblMovieFanartExpertMulti.Size = New System.Drawing.Size(40, 13)
        Me.lblMovieFanartExpertMulti.TabIndex = 37
        Me.lblMovieFanartExpertMulti.Text = "Fanart"
        '
        'lblMoviePosterExpertMulti
        '
        Me.lblMoviePosterExpertMulti.AutoSize = true
        Me.lblMoviePosterExpertMulti.Location = New System.Drawing.Point(6, 36)
        Me.lblMoviePosterExpertMulti.Name = "lblMoviePosterExpertMulti"
        Me.lblMoviePosterExpertMulti.Size = New System.Drawing.Size(39, 13)
        Me.lblMoviePosterExpertMulti.TabIndex = 36
        Me.lblMoviePosterExpertMulti.Text = "Poster"
        '
        'txtMovieNFOExpertMulti
        '
        Me.txtMovieNFOExpertMulti.Enabled = false
        Me.txtMovieNFOExpertMulti.Location = New System.Drawing.Point(71, 6)
        Me.txtMovieNFOExpertMulti.Name = "txtMovieNFOExpertMulti"
        Me.txtMovieNFOExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieNFOExpertMulti.TabIndex = 1
        '
        'lblMovieNFOExpertMulti
        '
        Me.lblMovieNFOExpertMulti.AutoSize = true
        Me.lblMovieNFOExpertMulti.Location = New System.Drawing.Point(6, 9)
        Me.lblMovieNFOExpertMulti.Name = "lblMovieNFOExpertMulti"
        Me.lblMovieNFOExpertMulti.Size = New System.Drawing.Size(30, 13)
        Me.lblMovieNFOExpertMulti.TabIndex = 35
        Me.lblMovieNFOExpertMulti.Text = "NFO"
        '
        'tpMovieFileNamingExpertVTS
        '
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.gbMovieExpertVTSOptionalSettings)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.gbMovieExpertVTSOptionalImages)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieClearArtExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.txtMoviePosterExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieFanartExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieTrailerExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieBannerExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieClearLogoExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieClearArtExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieLandscapeExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieDiscArtExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieLandscapeExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieDiscArtExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieBannerExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieTrailerExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieClearLogoExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieFanartExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.lblMoviePosterExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieNFOExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieNFOExpertVTS)
        Me.tpMovieFileNamingExpertVTS.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieFileNamingExpertVTS.Name = "tpMovieFileNamingExpertVTS"
        Me.tpMovieFileNamingExpertVTS.Size = New System.Drawing.Size(473, 254)
        Me.tpMovieFileNamingExpertVTS.TabIndex = 2
        Me.tpMovieFileNamingExpertVTS.Text = "VIDEO_TS"
        Me.tpMovieFileNamingExpertVTS.UseVisualStyleBackColor = true
        '
        'gbMovieExpertVTSOptionalSettings
        '
        Me.gbMovieExpertVTSOptionalSettings.Controls.Add(Me.chkMovieRecognizeVTSExpertVTS)
        Me.gbMovieExpertVTSOptionalSettings.Controls.Add(Me.chkMovieUseBaseDirectoryExpertVTS)
        Me.gbMovieExpertVTSOptionalSettings.Controls.Add(Me.chkMovieXBMCTrailerFormatExpertVTS)
        Me.gbMovieExpertVTSOptionalSettings.Location = New System.Drawing.Point(264, 6)
        Me.gbMovieExpertVTSOptionalSettings.Name = "gbMovieExpertVTSOptionalSettings"
        Me.gbMovieExpertVTSOptionalSettings.Size = New System.Drawing.Size(203, 130)
        Me.gbMovieExpertVTSOptionalSettings.TabIndex = 10
        Me.gbMovieExpertVTSOptionalSettings.TabStop = false
        Me.gbMovieExpertVTSOptionalSettings.Text = "Optional Settings"
        '
        'chkMovieRecognizeVTSExpertVTS
        '
        Me.chkMovieRecognizeVTSExpertVTS.Enabled = false
        Me.chkMovieRecognizeVTSExpertVTS.Location = New System.Drawing.Point(6, 86)
        Me.chkMovieRecognizeVTSExpertVTS.Name = "chkMovieRecognizeVTSExpertVTS"
        Me.chkMovieRecognizeVTSExpertVTS.Size = New System.Drawing.Size(191, 32)
        Me.chkMovieRecognizeVTSExpertVTS.TabIndex = 3
        Me.chkMovieRecognizeVTSExpertVTS.Text = "Detect VIDEO_TS folders even if they are not named VIDEO_TS"
        Me.chkMovieRecognizeVTSExpertVTS.UseVisualStyleBackColor = true
        '
        'chkMovieUseBaseDirectoryExpertVTS
        '
        Me.chkMovieUseBaseDirectoryExpertVTS.Enabled = false
        Me.chkMovieUseBaseDirectoryExpertVTS.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieUseBaseDirectoryExpertVTS.Name = "chkMovieUseBaseDirectoryExpertVTS"
        Me.chkMovieUseBaseDirectoryExpertVTS.Size = New System.Drawing.Size(191, 34)
        Me.chkMovieUseBaseDirectoryExpertVTS.TabIndex = 2
        Me.chkMovieUseBaseDirectoryExpertVTS.Text = "Don't save any files in VIDEO_TS/BDMV folders"
        Me.chkMovieUseBaseDirectoryExpertVTS.UseVisualStyleBackColor = true
        '
        'chkMovieXBMCTrailerFormatExpertVTS
        '
        Me.chkMovieXBMCTrailerFormatExpertVTS.Enabled = false
        Me.chkMovieXBMCTrailerFormatExpertVTS.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieXBMCTrailerFormatExpertVTS.Name = "chkMovieXBMCTrailerFormatExpertVTS"
        Me.chkMovieXBMCTrailerFormatExpertVTS.Size = New System.Drawing.Size(191, 17)
        Me.chkMovieXBMCTrailerFormatExpertVTS.TabIndex = 1
        Me.chkMovieXBMCTrailerFormatExpertVTS.Text = "XBMC Trailer Format"
        Me.chkMovieXBMCTrailerFormatExpertVTS.UseVisualStyleBackColor = true
        '
        'gbMovieExpertVTSOptionalImages
        '
        Me.gbMovieExpertVTSOptionalImages.Controls.Add(Me.txtMovieActorThumbsExtExpertVTS)
        Me.gbMovieExpertVTSOptionalImages.Controls.Add(Me.chkMovieActorThumbsExpertVTS)
        Me.gbMovieExpertVTSOptionalImages.Controls.Add(Me.chkMovieExtrafanartsExpertVTS)
        Me.gbMovieExpertVTSOptionalImages.Controls.Add(Me.chkMovieExtrathumbsExpertVTS)
        Me.gbMovieExpertVTSOptionalImages.Location = New System.Drawing.Point(264, 151)
        Me.gbMovieExpertVTSOptionalImages.Name = "gbMovieExpertVTSOptionalImages"
        Me.gbMovieExpertVTSOptionalImages.Size = New System.Drawing.Size(203, 93)
        Me.gbMovieExpertVTSOptionalImages.TabIndex = 11
        Me.gbMovieExpertVTSOptionalImages.TabStop = false
        Me.gbMovieExpertVTSOptionalImages.Text = "Optional Images"
        '
        'txtMovieActorThumbsExtExpertVTS
        '
        Me.txtMovieActorThumbsExtExpertVTS.Enabled = false
        Me.txtMovieActorThumbsExtExpertVTS.Location = New System.Drawing.Point(108, 19)
        Me.txtMovieActorThumbsExtExpertVTS.Name = "txtMovieActorThumbsExtExpertVTS"
        Me.txtMovieActorThumbsExtExpertVTS.Size = New System.Drawing.Size(46, 22)
        Me.txtMovieActorThumbsExtExpertVTS.TabIndex = 2
        '
        'chkMovieActorThumbsExpertVTS
        '
        Me.chkMovieActorThumbsExpertVTS.AutoSize = true
        Me.chkMovieActorThumbsExpertVTS.Enabled = false
        Me.chkMovieActorThumbsExpertVTS.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieActorThumbsExpertVTS.Name = "chkMovieActorThumbsExpertVTS"
        Me.chkMovieActorThumbsExpertVTS.Size = New System.Drawing.Size(96, 17)
        Me.chkMovieActorThumbsExpertVTS.TabIndex = 1
        Me.chkMovieActorThumbsExpertVTS.Text = "Actor Thumbs"
        Me.chkMovieActorThumbsExpertVTS.UseVisualStyleBackColor = true
        '
        'chkMovieExtrafanartsExpertVTS
        '
        Me.chkMovieExtrafanartsExpertVTS.AutoSize = true
        Me.chkMovieExtrafanartsExpertVTS.Enabled = false
        Me.chkMovieExtrafanartsExpertVTS.Location = New System.Drawing.Point(6, 67)
        Me.chkMovieExtrafanartsExpertVTS.Name = "chkMovieExtrafanartsExpertVTS"
        Me.chkMovieExtrafanartsExpertVTS.Size = New System.Drawing.Size(87, 17)
        Me.chkMovieExtrafanartsExpertVTS.TabIndex = 4
        Me.chkMovieExtrafanartsExpertVTS.Text = "Extrafanarts"
        Me.chkMovieExtrafanartsExpertVTS.UseVisualStyleBackColor = true
        '
        'chkMovieExtrathumbsExpertVTS
        '
        Me.chkMovieExtrathumbsExpertVTS.AutoSize = true
        Me.chkMovieExtrathumbsExpertVTS.Enabled = false
        Me.chkMovieExtrathumbsExpertVTS.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieExtrathumbsExpertVTS.Name = "chkMovieExtrathumbsExpertVTS"
        Me.chkMovieExtrathumbsExpertVTS.Size = New System.Drawing.Size(90, 17)
        Me.chkMovieExtrathumbsExpertVTS.TabIndex = 3
        Me.chkMovieExtrathumbsExpertVTS.Text = "Extrathumbs"
        Me.chkMovieExtrathumbsExpertVTS.UseVisualStyleBackColor = true
        '
        'lblMovieClearArtExpertVTS
        '
        Me.lblMovieClearArtExpertVTS.AutoSize = true
        Me.lblMovieClearArtExpertVTS.Location = New System.Drawing.Point(6, 171)
        Me.lblMovieClearArtExpertVTS.Name = "lblMovieClearArtExpertVTS"
        Me.lblMovieClearArtExpertVTS.Size = New System.Drawing.Size(48, 13)
        Me.lblMovieClearArtExpertVTS.TabIndex = 51
        Me.lblMovieClearArtExpertVTS.Text = "ClearArt"
        '
        'txtMoviePosterExpertVTS
        '
        Me.txtMoviePosterExpertVTS.Enabled = false
        Me.txtMoviePosterExpertVTS.Location = New System.Drawing.Point(71, 33)
        Me.txtMoviePosterExpertVTS.Name = "txtMoviePosterExpertVTS"
        Me.txtMoviePosterExpertVTS.Size = New System.Drawing.Size(187, 22)
        Me.txtMoviePosterExpertVTS.TabIndex = 2
        '
        'txtMovieFanartExpertVTS
        '
        Me.txtMovieFanartExpertVTS.Enabled = false
        Me.txtMovieFanartExpertVTS.Location = New System.Drawing.Point(71, 60)
        Me.txtMovieFanartExpertVTS.Name = "txtMovieFanartExpertVTS"
        Me.txtMovieFanartExpertVTS.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieFanartExpertVTS.TabIndex = 3
        '
        'txtMovieTrailerExpertVTS
        '
        Me.txtMovieTrailerExpertVTS.Enabled = false
        Me.txtMovieTrailerExpertVTS.Location = New System.Drawing.Point(71, 87)
        Me.txtMovieTrailerExpertVTS.Name = "txtMovieTrailerExpertVTS"
        Me.txtMovieTrailerExpertVTS.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieTrailerExpertVTS.TabIndex = 4
        '
        'txtMovieBannerExpertVTS
        '
        Me.txtMovieBannerExpertVTS.Enabled = false
        Me.txtMovieBannerExpertVTS.Location = New System.Drawing.Point(71, 114)
        Me.txtMovieBannerExpertVTS.Name = "txtMovieBannerExpertVTS"
        Me.txtMovieBannerExpertVTS.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieBannerExpertVTS.TabIndex = 5
        '
        'txtMovieClearLogoExpertVTS
        '
        Me.txtMovieClearLogoExpertVTS.Enabled = false
        Me.txtMovieClearLogoExpertVTS.Location = New System.Drawing.Point(71, 141)
        Me.txtMovieClearLogoExpertVTS.Name = "txtMovieClearLogoExpertVTS"
        Me.txtMovieClearLogoExpertVTS.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieClearLogoExpertVTS.TabIndex = 6
        '
        'txtMovieClearArtExpertVTS
        '
        Me.txtMovieClearArtExpertVTS.Enabled = false
        Me.txtMovieClearArtExpertVTS.Location = New System.Drawing.Point(71, 168)
        Me.txtMovieClearArtExpertVTS.Name = "txtMovieClearArtExpertVTS"
        Me.txtMovieClearArtExpertVTS.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieClearArtExpertVTS.TabIndex = 7
        '
        'txtMovieLandscapeExpertVTS
        '
        Me.txtMovieLandscapeExpertVTS.Enabled = false
        Me.txtMovieLandscapeExpertVTS.Location = New System.Drawing.Point(71, 222)
        Me.txtMovieLandscapeExpertVTS.Name = "txtMovieLandscapeExpertVTS"
        Me.txtMovieLandscapeExpertVTS.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieLandscapeExpertVTS.TabIndex = 9
        '
        'txtMovieDiscArtExpertVTS
        '
        Me.txtMovieDiscArtExpertVTS.Enabled = false
        Me.txtMovieDiscArtExpertVTS.Location = New System.Drawing.Point(71, 195)
        Me.txtMovieDiscArtExpertVTS.Name = "txtMovieDiscArtExpertVTS"
        Me.txtMovieDiscArtExpertVTS.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieDiscArtExpertVTS.TabIndex = 8
        '
        'lblMovieLandscapeExpertVTS
        '
        Me.lblMovieLandscapeExpertVTS.AutoSize = true
        Me.lblMovieLandscapeExpertVTS.Location = New System.Drawing.Point(6, 225)
        Me.lblMovieLandscapeExpertVTS.Name = "lblMovieLandscapeExpertVTS"
        Me.lblMovieLandscapeExpertVTS.Size = New System.Drawing.Size(61, 13)
        Me.lblMovieLandscapeExpertVTS.TabIndex = 42
        Me.lblMovieLandscapeExpertVTS.Text = "Landscape"
        '
        'lblMovieDiscArtExpertVTS
        '
        Me.lblMovieDiscArtExpertVTS.AutoSize = true
        Me.lblMovieDiscArtExpertVTS.Location = New System.Drawing.Point(6, 198)
        Me.lblMovieDiscArtExpertVTS.Name = "lblMovieDiscArtExpertVTS"
        Me.lblMovieDiscArtExpertVTS.Size = New System.Drawing.Size(43, 13)
        Me.lblMovieDiscArtExpertVTS.TabIndex = 41
        Me.lblMovieDiscArtExpertVTS.Text = "DiscArt"
        '
        'lblMovieBannerExpertVTS
        '
        Me.lblMovieBannerExpertVTS.AutoSize = true
        Me.lblMovieBannerExpertVTS.Location = New System.Drawing.Point(6, 117)
        Me.lblMovieBannerExpertVTS.Name = "lblMovieBannerExpertVTS"
        Me.lblMovieBannerExpertVTS.Size = New System.Drawing.Size(44, 13)
        Me.lblMovieBannerExpertVTS.TabIndex = 40
        Me.lblMovieBannerExpertVTS.Text = "Banner"
        '
        'lblMovieTrailerExpertVTS
        '
        Me.lblMovieTrailerExpertVTS.AutoSize = true
        Me.lblMovieTrailerExpertVTS.Location = New System.Drawing.Point(6, 90)
        Me.lblMovieTrailerExpertVTS.Name = "lblMovieTrailerExpertVTS"
        Me.lblMovieTrailerExpertVTS.Size = New System.Drawing.Size(38, 13)
        Me.lblMovieTrailerExpertVTS.TabIndex = 39
        Me.lblMovieTrailerExpertVTS.Text = "Trailer"
        '
        'lblMovieClearLogoExpertVTS
        '
        Me.lblMovieClearLogoExpertVTS.AutoSize = true
        Me.lblMovieClearLogoExpertVTS.Location = New System.Drawing.Point(6, 144)
        Me.lblMovieClearLogoExpertVTS.Name = "lblMovieClearLogoExpertVTS"
        Me.lblMovieClearLogoExpertVTS.Size = New System.Drawing.Size(59, 13)
        Me.lblMovieClearLogoExpertVTS.TabIndex = 38
        Me.lblMovieClearLogoExpertVTS.Text = "ClearLogo"
        '
        'lblMovieFanartExpertVTS
        '
        Me.lblMovieFanartExpertVTS.AutoSize = true
        Me.lblMovieFanartExpertVTS.Location = New System.Drawing.Point(6, 64)
        Me.lblMovieFanartExpertVTS.Name = "lblMovieFanartExpertVTS"
        Me.lblMovieFanartExpertVTS.Size = New System.Drawing.Size(40, 13)
        Me.lblMovieFanartExpertVTS.TabIndex = 37
        Me.lblMovieFanartExpertVTS.Text = "Fanart"
        '
        'lblMoviePosterExpertVTS
        '
        Me.lblMoviePosterExpertVTS.AutoSize = true
        Me.lblMoviePosterExpertVTS.Location = New System.Drawing.Point(6, 36)
        Me.lblMoviePosterExpertVTS.Name = "lblMoviePosterExpertVTS"
        Me.lblMoviePosterExpertVTS.Size = New System.Drawing.Size(39, 13)
        Me.lblMoviePosterExpertVTS.TabIndex = 36
        Me.lblMoviePosterExpertVTS.Text = "Poster"
        '
        'txtMovieNFOExpertVTS
        '
        Me.txtMovieNFOExpertVTS.Enabled = false
        Me.txtMovieNFOExpertVTS.Location = New System.Drawing.Point(71, 6)
        Me.txtMovieNFOExpertVTS.Name = "txtMovieNFOExpertVTS"
        Me.txtMovieNFOExpertVTS.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieNFOExpertVTS.TabIndex = 1
        '
        'lblMovieNFOExpertVTS
        '
        Me.lblMovieNFOExpertVTS.AutoSize = true
        Me.lblMovieNFOExpertVTS.Location = New System.Drawing.Point(6, 9)
        Me.lblMovieNFOExpertVTS.Name = "lblMovieNFOExpertVTS"
        Me.lblMovieNFOExpertVTS.Size = New System.Drawing.Size(30, 13)
        Me.lblMovieNFOExpertVTS.TabIndex = 35
        Me.lblMovieNFOExpertVTS.Text = "NFO"
        '
        'tpMovieFileNamingExpertBDMV
        '
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.gbMovieExpertBDMVOptionalSettings)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.gbMovieExpertBDMVOptionalImages)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieClearArtExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.txtMoviePosterExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieFanartExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieTrailerExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieBannerExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieClearLogoExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieClearArtExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieLandscapeExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieDiscArtExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieLandscapeExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieDiscArtExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieBannerExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieTrailerExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieClearLogoExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieFanartExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.lblMoviePosterExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieNFOExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieNFOExpertBDMV)
        Me.tpMovieFileNamingExpertBDMV.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieFileNamingExpertBDMV.Name = "tpMovieFileNamingExpertBDMV"
        Me.tpMovieFileNamingExpertBDMV.Size = New System.Drawing.Size(473, 254)
        Me.tpMovieFileNamingExpertBDMV.TabIndex = 3
        Me.tpMovieFileNamingExpertBDMV.Text = "BDMV"
        Me.tpMovieFileNamingExpertBDMV.UseVisualStyleBackColor = true
        '
        'gbMovieExpertBDMVOptionalSettings
        '
        Me.gbMovieExpertBDMVOptionalSettings.Controls.Add(Me.chkMovieUseBaseDirectoryExpertBDMV)
        Me.gbMovieExpertBDMVOptionalSettings.Controls.Add(Me.chkMovieXBMCTrailerFormatExpertBDMV)
        Me.gbMovieExpertBDMVOptionalSettings.Location = New System.Drawing.Point(264, 6)
        Me.gbMovieExpertBDMVOptionalSettings.Name = "gbMovieExpertBDMVOptionalSettings"
        Me.gbMovieExpertBDMVOptionalSettings.Size = New System.Drawing.Size(203, 93)
        Me.gbMovieExpertBDMVOptionalSettings.TabIndex = 10
        Me.gbMovieExpertBDMVOptionalSettings.TabStop = false
        Me.gbMovieExpertBDMVOptionalSettings.Text = "Optional Settings"
        '
        'chkMovieUseBaseDirectoryExpertBDMV
        '
        Me.chkMovieUseBaseDirectoryExpertBDMV.Enabled = false
        Me.chkMovieUseBaseDirectoryExpertBDMV.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieUseBaseDirectoryExpertBDMV.Name = "chkMovieUseBaseDirectoryExpertBDMV"
        Me.chkMovieUseBaseDirectoryExpertBDMV.Size = New System.Drawing.Size(191, 34)
        Me.chkMovieUseBaseDirectoryExpertBDMV.TabIndex = 2
        Me.chkMovieUseBaseDirectoryExpertBDMV.Text = "Don't save any files in VIDEO_TS/BDMV folders"
        Me.chkMovieUseBaseDirectoryExpertBDMV.UseVisualStyleBackColor = true
        '
        'chkMovieXBMCTrailerFormatExpertBDMV
        '
        Me.chkMovieXBMCTrailerFormatExpertBDMV.Enabled = false
        Me.chkMovieXBMCTrailerFormatExpertBDMV.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieXBMCTrailerFormatExpertBDMV.Name = "chkMovieXBMCTrailerFormatExpertBDMV"
        Me.chkMovieXBMCTrailerFormatExpertBDMV.Size = New System.Drawing.Size(191, 17)
        Me.chkMovieXBMCTrailerFormatExpertBDMV.TabIndex = 1
        Me.chkMovieXBMCTrailerFormatExpertBDMV.Text = "XBMC Trailer Format"
        Me.chkMovieXBMCTrailerFormatExpertBDMV.UseVisualStyleBackColor = true
        '
        'gbMovieExpertBDMVOptionalImages
        '
        Me.gbMovieExpertBDMVOptionalImages.Controls.Add(Me.txtMovieActorThumbsExtExpertBDMV)
        Me.gbMovieExpertBDMVOptionalImages.Controls.Add(Me.chkMovieActorThumbsExpertBDMV)
        Me.gbMovieExpertBDMVOptionalImages.Controls.Add(Me.chkMovieExtrafanartsExpertBDMV)
        Me.gbMovieExpertBDMVOptionalImages.Controls.Add(Me.chkMovieExtrathumbsExpertBDMV)
        Me.gbMovieExpertBDMVOptionalImages.Location = New System.Drawing.Point(264, 105)
        Me.gbMovieExpertBDMVOptionalImages.Name = "gbMovieExpertBDMVOptionalImages"
        Me.gbMovieExpertBDMVOptionalImages.Size = New System.Drawing.Size(203, 93)
        Me.gbMovieExpertBDMVOptionalImages.TabIndex = 1
        Me.gbMovieExpertBDMVOptionalImages.TabStop = false
        Me.gbMovieExpertBDMVOptionalImages.Text = "Optional Images"
        '
        'txtMovieActorThumbsExtExpertBDMV
        '
        Me.txtMovieActorThumbsExtExpertBDMV.Enabled = false
        Me.txtMovieActorThumbsExtExpertBDMV.Location = New System.Drawing.Point(108, 19)
        Me.txtMovieActorThumbsExtExpertBDMV.Name = "txtMovieActorThumbsExtExpertBDMV"
        Me.txtMovieActorThumbsExtExpertBDMV.Size = New System.Drawing.Size(46, 22)
        Me.txtMovieActorThumbsExtExpertBDMV.TabIndex = 2
        '
        'chkMovieActorThumbsExpertBDMV
        '
        Me.chkMovieActorThumbsExpertBDMV.AutoSize = true
        Me.chkMovieActorThumbsExpertBDMV.Enabled = false
        Me.chkMovieActorThumbsExpertBDMV.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieActorThumbsExpertBDMV.Name = "chkMovieActorThumbsExpertBDMV"
        Me.chkMovieActorThumbsExpertBDMV.Size = New System.Drawing.Size(96, 17)
        Me.chkMovieActorThumbsExpertBDMV.TabIndex = 1
        Me.chkMovieActorThumbsExpertBDMV.Text = "Actor Thumbs"
        Me.chkMovieActorThumbsExpertBDMV.UseVisualStyleBackColor = true
        '
        'chkMovieExtrafanartsExpertBDMV
        '
        Me.chkMovieExtrafanartsExpertBDMV.AutoSize = true
        Me.chkMovieExtrafanartsExpertBDMV.Enabled = false
        Me.chkMovieExtrafanartsExpertBDMV.Location = New System.Drawing.Point(6, 67)
        Me.chkMovieExtrafanartsExpertBDMV.Name = "chkMovieExtrafanartsExpertBDMV"
        Me.chkMovieExtrafanartsExpertBDMV.Size = New System.Drawing.Size(87, 17)
        Me.chkMovieExtrafanartsExpertBDMV.TabIndex = 4
        Me.chkMovieExtrafanartsExpertBDMV.Text = "Extrafanarts"
        Me.chkMovieExtrafanartsExpertBDMV.UseVisualStyleBackColor = true
        '
        'chkMovieExtrathumbsExpertBDMV
        '
        Me.chkMovieExtrathumbsExpertBDMV.AutoSize = true
        Me.chkMovieExtrathumbsExpertBDMV.Enabled = false
        Me.chkMovieExtrathumbsExpertBDMV.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieExtrathumbsExpertBDMV.Name = "chkMovieExtrathumbsExpertBDMV"
        Me.chkMovieExtrathumbsExpertBDMV.Size = New System.Drawing.Size(90, 17)
        Me.chkMovieExtrathumbsExpertBDMV.TabIndex = 3
        Me.chkMovieExtrathumbsExpertBDMV.Text = "Extrathumbs"
        Me.chkMovieExtrathumbsExpertBDMV.UseVisualStyleBackColor = true
        '
        'lblMovieClearArtExpertBDMV
        '
        Me.lblMovieClearArtExpertBDMV.AutoSize = true
        Me.lblMovieClearArtExpertBDMV.Location = New System.Drawing.Point(6, 171)
        Me.lblMovieClearArtExpertBDMV.Name = "lblMovieClearArtExpertBDMV"
        Me.lblMovieClearArtExpertBDMV.Size = New System.Drawing.Size(48, 13)
        Me.lblMovieClearArtExpertBDMV.TabIndex = 51
        Me.lblMovieClearArtExpertBDMV.Text = "ClearArt"
        '
        'txtMoviePosterExpertBDMV
        '
        Me.txtMoviePosterExpertBDMV.Enabled = false
        Me.txtMoviePosterExpertBDMV.Location = New System.Drawing.Point(71, 33)
        Me.txtMoviePosterExpertBDMV.Name = "txtMoviePosterExpertBDMV"
        Me.txtMoviePosterExpertBDMV.Size = New System.Drawing.Size(187, 22)
        Me.txtMoviePosterExpertBDMV.TabIndex = 2
        '
        'txtMovieFanartExpertBDMV
        '
        Me.txtMovieFanartExpertBDMV.Enabled = false
        Me.txtMovieFanartExpertBDMV.Location = New System.Drawing.Point(71, 60)
        Me.txtMovieFanartExpertBDMV.Name = "txtMovieFanartExpertBDMV"
        Me.txtMovieFanartExpertBDMV.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieFanartExpertBDMV.TabIndex = 3
        '
        'txtMovieTrailerExpertBDMV
        '
        Me.txtMovieTrailerExpertBDMV.Enabled = false
        Me.txtMovieTrailerExpertBDMV.Location = New System.Drawing.Point(71, 87)
        Me.txtMovieTrailerExpertBDMV.Name = "txtMovieTrailerExpertBDMV"
        Me.txtMovieTrailerExpertBDMV.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieTrailerExpertBDMV.TabIndex = 4
        '
        'txtMovieBannerExpertBDMV
        '
        Me.txtMovieBannerExpertBDMV.Enabled = false
        Me.txtMovieBannerExpertBDMV.Location = New System.Drawing.Point(71, 114)
        Me.txtMovieBannerExpertBDMV.Name = "txtMovieBannerExpertBDMV"
        Me.txtMovieBannerExpertBDMV.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieBannerExpertBDMV.TabIndex = 5
        '
        'txtMovieClearLogoExpertBDMV
        '
        Me.txtMovieClearLogoExpertBDMV.Enabled = false
        Me.txtMovieClearLogoExpertBDMV.Location = New System.Drawing.Point(71, 141)
        Me.txtMovieClearLogoExpertBDMV.Name = "txtMovieClearLogoExpertBDMV"
        Me.txtMovieClearLogoExpertBDMV.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieClearLogoExpertBDMV.TabIndex = 6
        '
        'txtMovieClearArtExpertBDMV
        '
        Me.txtMovieClearArtExpertBDMV.Enabled = false
        Me.txtMovieClearArtExpertBDMV.Location = New System.Drawing.Point(71, 168)
        Me.txtMovieClearArtExpertBDMV.Name = "txtMovieClearArtExpertBDMV"
        Me.txtMovieClearArtExpertBDMV.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieClearArtExpertBDMV.TabIndex = 7
        '
        'txtMovieLandscapeExpertBDMV
        '
        Me.txtMovieLandscapeExpertBDMV.Enabled = false
        Me.txtMovieLandscapeExpertBDMV.Location = New System.Drawing.Point(71, 222)
        Me.txtMovieLandscapeExpertBDMV.Name = "txtMovieLandscapeExpertBDMV"
        Me.txtMovieLandscapeExpertBDMV.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieLandscapeExpertBDMV.TabIndex = 9
        '
        'txtMovieDiscArtExpertBDMV
        '
        Me.txtMovieDiscArtExpertBDMV.Enabled = false
        Me.txtMovieDiscArtExpertBDMV.Location = New System.Drawing.Point(71, 195)
        Me.txtMovieDiscArtExpertBDMV.Name = "txtMovieDiscArtExpertBDMV"
        Me.txtMovieDiscArtExpertBDMV.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieDiscArtExpertBDMV.TabIndex = 8
        '
        'lblMovieLandscapeExpertBDMV
        '
        Me.lblMovieLandscapeExpertBDMV.AutoSize = true
        Me.lblMovieLandscapeExpertBDMV.Location = New System.Drawing.Point(6, 225)
        Me.lblMovieLandscapeExpertBDMV.Name = "lblMovieLandscapeExpertBDMV"
        Me.lblMovieLandscapeExpertBDMV.Size = New System.Drawing.Size(61, 13)
        Me.lblMovieLandscapeExpertBDMV.TabIndex = 42
        Me.lblMovieLandscapeExpertBDMV.Text = "Landscape"
        '
        'lblMovieDiscArtExpertBDMV
        '
        Me.lblMovieDiscArtExpertBDMV.AutoSize = true
        Me.lblMovieDiscArtExpertBDMV.Location = New System.Drawing.Point(6, 198)
        Me.lblMovieDiscArtExpertBDMV.Name = "lblMovieDiscArtExpertBDMV"
        Me.lblMovieDiscArtExpertBDMV.Size = New System.Drawing.Size(43, 13)
        Me.lblMovieDiscArtExpertBDMV.TabIndex = 41
        Me.lblMovieDiscArtExpertBDMV.Text = "DiscArt"
        '
        'lblMovieBannerExpertBDMV
        '
        Me.lblMovieBannerExpertBDMV.AutoSize = true
        Me.lblMovieBannerExpertBDMV.Location = New System.Drawing.Point(6, 117)
        Me.lblMovieBannerExpertBDMV.Name = "lblMovieBannerExpertBDMV"
        Me.lblMovieBannerExpertBDMV.Size = New System.Drawing.Size(44, 13)
        Me.lblMovieBannerExpertBDMV.TabIndex = 40
        Me.lblMovieBannerExpertBDMV.Text = "Banner"
        '
        'lblMovieTrailerExpertBDMV
        '
        Me.lblMovieTrailerExpertBDMV.AutoSize = true
        Me.lblMovieTrailerExpertBDMV.Location = New System.Drawing.Point(6, 90)
        Me.lblMovieTrailerExpertBDMV.Name = "lblMovieTrailerExpertBDMV"
        Me.lblMovieTrailerExpertBDMV.Size = New System.Drawing.Size(38, 13)
        Me.lblMovieTrailerExpertBDMV.TabIndex = 39
        Me.lblMovieTrailerExpertBDMV.Text = "Trailer"
        '
        'lblMovieClearLogoExpertBDMV
        '
        Me.lblMovieClearLogoExpertBDMV.AutoSize = true
        Me.lblMovieClearLogoExpertBDMV.Location = New System.Drawing.Point(6, 144)
        Me.lblMovieClearLogoExpertBDMV.Name = "lblMovieClearLogoExpertBDMV"
        Me.lblMovieClearLogoExpertBDMV.Size = New System.Drawing.Size(59, 13)
        Me.lblMovieClearLogoExpertBDMV.TabIndex = 38
        Me.lblMovieClearLogoExpertBDMV.Text = "ClearLogo"
        '
        'lblMovieFanartExpertBDMV
        '
        Me.lblMovieFanartExpertBDMV.AutoSize = true
        Me.lblMovieFanartExpertBDMV.Location = New System.Drawing.Point(6, 64)
        Me.lblMovieFanartExpertBDMV.Name = "lblMovieFanartExpertBDMV"
        Me.lblMovieFanartExpertBDMV.Size = New System.Drawing.Size(40, 13)
        Me.lblMovieFanartExpertBDMV.TabIndex = 37
        Me.lblMovieFanartExpertBDMV.Text = "Fanart"
        '
        'lblMoviePosterExpertBDMV
        '
        Me.lblMoviePosterExpertBDMV.AutoSize = true
        Me.lblMoviePosterExpertBDMV.Location = New System.Drawing.Point(6, 36)
        Me.lblMoviePosterExpertBDMV.Name = "lblMoviePosterExpertBDMV"
        Me.lblMoviePosterExpertBDMV.Size = New System.Drawing.Size(39, 13)
        Me.lblMoviePosterExpertBDMV.TabIndex = 36
        Me.lblMoviePosterExpertBDMV.Text = "Poster"
        '
        'txtMovieNFOExpertBDMV
        '
        Me.txtMovieNFOExpertBDMV.Enabled = false
        Me.txtMovieNFOExpertBDMV.Location = New System.Drawing.Point(71, 6)
        Me.txtMovieNFOExpertBDMV.Name = "txtMovieNFOExpertBDMV"
        Me.txtMovieNFOExpertBDMV.Size = New System.Drawing.Size(187, 22)
        Me.txtMovieNFOExpertBDMV.TabIndex = 1
        '
        'lblMovieNFOExpertBDMV
        '
        Me.lblMovieNFOExpertBDMV.AutoSize = true
        Me.lblMovieNFOExpertBDMV.Location = New System.Drawing.Point(6, 9)
        Me.lblMovieNFOExpertBDMV.Name = "lblMovieNFOExpertBDMV"
        Me.lblMovieNFOExpertBDMV.Size = New System.Drawing.Size(30, 13)
        Me.lblMovieNFOExpertBDMV.TabIndex = 35
        Me.lblMovieNFOExpertBDMV.Text = "NFO"
        '
        'chkMovieUseExpert
        '
        Me.chkMovieUseExpert.AutoSize = true
        Me.chkMovieUseExpert.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieUseExpert.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieUseExpert.Name = "chkMovieUseExpert"
        Me.chkMovieUseExpert.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieUseExpert.TabIndex = 1
        Me.chkMovieUseExpert.Text = "Use"
        Me.chkMovieUseExpert.UseVisualStyleBackColor = true
        '
        'btnMovieSourceEdit
        '
        Me.btnMovieSourceEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnMovieSourceEdit.Image = CType(resources.GetObject("btnMovieSourceEdit.Image"),System.Drawing.Image)
        Me.btnMovieSourceEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieSourceEdit.Location = New System.Drawing.Point(638, 35)
        Me.btnMovieSourceEdit.Name = "btnMovieSourceEdit"
        Me.btnMovieSourceEdit.Size = New System.Drawing.Size(104, 23)
        Me.btnMovieSourceEdit.TabIndex = 2
        Me.btnMovieSourceEdit.Text = "Edit Source"
        Me.btnMovieSourceEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieSourceEdit.UseVisualStyleBackColor = true
        '
        'gbMovieSourcesMiscOpts
        '
        Me.gbMovieSourcesMiscOpts.Controls.Add(Me.chkMovieScanOrderModify)
        Me.gbMovieSourcesMiscOpts.Controls.Add(Me.chkMovieSortBeforeScan)
        Me.gbMovieSourcesMiscOpts.Controls.Add(Me.chkMovieGeneralIgnoreLastScan)
        Me.gbMovieSourcesMiscOpts.Controls.Add(Me.chkMovieCleanDB)
        Me.gbMovieSourcesMiscOpts.Controls.Add(Me.chkMovieSkipStackedSizeCheck)
        Me.gbMovieSourcesMiscOpts.Controls.Add(Me.lblMovieSkipLessThanMB)
        Me.gbMovieSourcesMiscOpts.Controls.Add(Me.txtMovieSkipLessThan)
        Me.gbMovieSourcesMiscOpts.Controls.Add(Me.lblMovieSkipLessThan)
        Me.gbMovieSourcesMiscOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieSourcesMiscOpts.Location = New System.Drawing.Point(5, 113)
        Me.gbMovieSourcesMiscOpts.Name = "gbMovieSourcesMiscOpts"
        Me.gbMovieSourcesMiscOpts.Size = New System.Drawing.Size(212, 283)
        Me.gbMovieSourcesMiscOpts.TabIndex = 4
        Me.gbMovieSourcesMiscOpts.TabStop = false
        Me.gbMovieSourcesMiscOpts.Text = "Miscellaneous Options"
        '
        'chkMovieScanOrderModify
        '
        Me.chkMovieScanOrderModify.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScanOrderModify.Location = New System.Drawing.Point(6, 189)
        Me.chkMovieScanOrderModify.Name = "chkMovieScanOrderModify"
        Me.chkMovieScanOrderModify.Size = New System.Drawing.Size(200, 33)
        Me.chkMovieScanOrderModify.TabIndex = 8
        Me.chkMovieScanOrderModify.Text = "Scan in order of last write time."
        Me.chkMovieScanOrderModify.UseVisualStyleBackColor = true
        '
        'chkMovieSortBeforeScan
        '
        Me.chkMovieSortBeforeScan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSortBeforeScan.Location = New System.Drawing.Point(6, 113)
        Me.chkMovieSortBeforeScan.Name = "chkMovieSortBeforeScan"
        Me.chkMovieSortBeforeScan.Size = New System.Drawing.Size(200, 31)
        Me.chkMovieSortBeforeScan.TabIndex = 6
        Me.chkMovieSortBeforeScan.Text = "Sort files into folders before each library update"
        Me.chkMovieSortBeforeScan.UseVisualStyleBackColor = true
        '
        'chkMovieGeneralIgnoreLastScan
        '
        Me.chkMovieGeneralIgnoreLastScan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieGeneralIgnoreLastScan.Location = New System.Drawing.Point(6, 150)
        Me.chkMovieGeneralIgnoreLastScan.Name = "chkMovieGeneralIgnoreLastScan"
        Me.chkMovieGeneralIgnoreLastScan.Size = New System.Drawing.Size(200, 33)
        Me.chkMovieGeneralIgnoreLastScan.TabIndex = 7
        Me.chkMovieGeneralIgnoreLastScan.Text = "Always scan all media when updating library"
        Me.chkMovieGeneralIgnoreLastScan.UseVisualStyleBackColor = true
        '
        'chkMovieCleanDB
        '
        Me.chkMovieCleanDB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieCleanDB.Location = New System.Drawing.Point(6, 228)
        Me.chkMovieCleanDB.Name = "chkMovieCleanDB"
        Me.chkMovieCleanDB.Size = New System.Drawing.Size(200, 33)
        Me.chkMovieCleanDB.TabIndex = 9
        Me.chkMovieCleanDB.Text = "Clean database after updating library"
        Me.chkMovieCleanDB.UseVisualStyleBackColor = true
        '
        'chkMovieSkipStackedSizeCheck
        '
        Me.chkMovieSkipStackedSizeCheck.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkMovieSkipStackedSizeCheck.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSkipStackedSizeCheck.Location = New System.Drawing.Point(21, 80)
        Me.chkMovieSkipStackedSizeCheck.Name = "chkMovieSkipStackedSizeCheck"
        Me.chkMovieSkipStackedSizeCheck.Size = New System.Drawing.Size(185, 17)
        Me.chkMovieSkipStackedSizeCheck.TabIndex = 3
        Me.chkMovieSkipStackedSizeCheck.Text = "Skip Size Check of Stacked Files"
        Me.chkMovieSkipStackedSizeCheck.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.chkMovieSkipStackedSizeCheck.UseVisualStyleBackColor = true
        '
        'lblMovieSkipLessThanMB
        '
        Me.lblMovieSkipLessThanMB.AutoSize = true
        Me.lblMovieSkipLessThanMB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieSkipLessThanMB.Location = New System.Drawing.Point(78, 62)
        Me.lblMovieSkipLessThanMB.Name = "lblMovieSkipLessThanMB"
        Me.lblMovieSkipLessThanMB.Size = New System.Drawing.Size(24, 13)
        Me.lblMovieSkipLessThanMB.TabIndex = 2
        Me.lblMovieSkipLessThanMB.Text = "MB"
        '
        'txtMovieSkipLessThan
        '
        Me.txtMovieSkipLessThan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieSkipLessThan.Location = New System.Drawing.Point(21, 53)
        Me.txtMovieSkipLessThan.Name = "txtMovieSkipLessThan"
        Me.txtMovieSkipLessThan.Size = New System.Drawing.Size(51, 22)
        Me.txtMovieSkipLessThan.TabIndex = 1
        '
        'lblMovieSkipLessThan
        '
        Me.lblMovieSkipLessThan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieSkipLessThan.Location = New System.Drawing.Point(4, 26)
        Me.lblMovieSkipLessThan.Name = "lblMovieSkipLessThan"
        Me.lblMovieSkipLessThan.Size = New System.Drawing.Size(202, 13)
        Me.lblMovieSkipLessThan.TabIndex = 0
        Me.lblMovieSkipLessThan.Text = "Skip files smaller than:"
        '
        'gbMovieSetMSAAPath
        '
        Me.gbMovieSetMSAAPath.Controls.Add(Me.btnMovieSetPathMSAABrowse)
        Me.gbMovieSetMSAAPath.Controls.Add(Me.txtMovieSetPathMSAA)
        Me.gbMovieSetMSAAPath.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gbMovieSetMSAAPath.Location = New System.Drawing.Point(190, 6)
        Me.gbMovieSetMSAAPath.Name = "gbMovieSetMSAAPath"
        Me.gbMovieSetMSAAPath.Size = New System.Drawing.Size(307, 58)
        Me.gbMovieSetMSAAPath.TabIndex = 7
        Me.gbMovieSetMSAAPath.TabStop = false
        Me.gbMovieSetMSAAPath.Text = "MovieSet Artwork Folder"
        '
        'btnMovieSetPathMSAABrowse
        '
        Me.btnMovieSetPathMSAABrowse.Enabled = false
        Me.btnMovieSetPathMSAABrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnMovieSetPathMSAABrowse.Location = New System.Drawing.Point(276, 21)
        Me.btnMovieSetPathMSAABrowse.Name = "btnMovieSetPathMSAABrowse"
        Me.btnMovieSetPathMSAABrowse.Size = New System.Drawing.Size(25, 22)
        Me.btnMovieSetPathMSAABrowse.TabIndex = 1
        Me.btnMovieSetPathMSAABrowse.Text = "..."
        Me.btnMovieSetPathMSAABrowse.UseVisualStyleBackColor = true
        '
        'txtMovieSetPathMSAA
        '
        Me.txtMovieSetPathMSAA.Enabled = false
        Me.txtMovieSetPathMSAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieSetPathMSAA.Location = New System.Drawing.Point(6, 21)
        Me.txtMovieSetPathMSAA.Name = "txtMovieSetPathMSAA"
        Me.txtMovieSetPathMSAA.Size = New System.Drawing.Size(264, 22)
        Me.txtMovieSetPathMSAA.TabIndex = 0
        '
        'pnlTVGeneral
        '
        Me.pnlTVGeneral.BackColor = System.Drawing.Color.White
        Me.pnlTVGeneral.Controls.Add(Me.gbTVGeneralMissingItemsOpts)
        Me.pnlTVGeneral.Controls.Add(Me.gbTVSortTokensOpts)
        Me.pnlTVGeneral.Controls.Add(Me.gbTVGeneralLangOpts)
        Me.pnlTVGeneral.Controls.Add(Me.gbTVGeneralMediaListOpts)
        Me.pnlTVGeneral.Controls.Add(Me.gbTVEpisodeFilterOpts)
        Me.pnlTVGeneral.Controls.Add(Me.gbTVGeneralMiscOpts)
        Me.pnlTVGeneral.Controls.Add(Me.gbTVShowFilterOpts)
        Me.pnlTVGeneral.Location = New System.Drawing.Point(900, 900)
        Me.pnlTVGeneral.Name = "pnlTVGeneral"
        Me.pnlTVGeneral.Size = New System.Drawing.Size(750, 500)
        Me.pnlTVGeneral.TabIndex = 20
        Me.pnlTVGeneral.Visible = false
        '
        'gbTVGeneralMissingItemsOpts
        '
        Me.gbTVGeneralMissingItemsOpts.Controls.Add(Me.gbTVGeneralMissingShowOpts)
        Me.gbTVGeneralMissingItemsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVGeneralMissingItemsOpts.Location = New System.Drawing.Point(231, 103)
        Me.gbTVGeneralMissingItemsOpts.Name = "gbTVGeneralMissingItemsOpts"
        Me.gbTVGeneralMissingItemsOpts.Size = New System.Drawing.Size(185, 394)
        Me.gbTVGeneralMissingItemsOpts.TabIndex = 73
        Me.gbTVGeneralMissingItemsOpts.TabStop = false
        Me.gbTVGeneralMissingItemsOpts.Text = "Missing Items Filter"
        '
        'gbTVGeneralMissingShowOpts
        '
        Me.gbTVGeneralMissingShowOpts.Controls.Add(Me.chkTVShowMissingBanner)
        Me.gbTVGeneralMissingShowOpts.Controls.Add(Me.chkTVShowMissingTheme)
        Me.gbTVGeneralMissingShowOpts.Controls.Add(Me.chkTVShowMissingClearLogo)
        Me.gbTVGeneralMissingShowOpts.Controls.Add(Me.chkTVShowMissingLandscape)
        Me.gbTVGeneralMissingShowOpts.Controls.Add(Me.chkTVShowMissingCharacterArt)
        Me.gbTVGeneralMissingShowOpts.Controls.Add(Me.chkTVShowMissingEFanarts)
        Me.gbTVGeneralMissingShowOpts.Controls.Add(Me.chkTVShowMissingPoster)
        Me.gbTVGeneralMissingShowOpts.Controls.Add(Me.chkTVShowMissingClearArt)
        Me.gbTVGeneralMissingShowOpts.Controls.Add(Me.chkTVShowMissingNFO)
        Me.gbTVGeneralMissingShowOpts.Controls.Add(Me.chkTVShowMissingFanart)
        Me.gbTVGeneralMissingShowOpts.Location = New System.Drawing.Point(6, 21)
        Me.gbTVGeneralMissingShowOpts.Name = "gbTVGeneralMissingShowOpts"
        Me.gbTVGeneralMissingShowOpts.Size = New System.Drawing.Size(173, 180)
        Me.gbTVGeneralMissingShowOpts.TabIndex = 16
        Me.gbTVGeneralMissingShowOpts.TabStop = false
        Me.gbTVGeneralMissingShowOpts.Text = "Shows"
        '
        'chkTVShowMissingBanner
        '
        Me.chkTVShowMissingBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowMissingBanner.Location = New System.Drawing.Point(6, 16)
        Me.chkTVShowMissingBanner.Name = "chkTVShowMissingBanner"
        Me.chkTVShowMissingBanner.Size = New System.Drawing.Size(161, 17)
        Me.chkTVShowMissingBanner.TabIndex = 11
        Me.chkTVShowMissingBanner.Text = "Check for Banner"
        Me.chkTVShowMissingBanner.UseVisualStyleBackColor = true
        '
        'chkTVShowMissingTheme
        '
        Me.chkTVShowMissingTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowMissingTheme.Location = New System.Drawing.Point(6, 160)
        Me.chkTVShowMissingTheme.Name = "chkTVShowMissingTheme"
        Me.chkTVShowMissingTheme.Size = New System.Drawing.Size(161, 17)
        Me.chkTVShowMissingTheme.TabIndex = 9
        Me.chkTVShowMissingTheme.Text = "Check for Theme"
        Me.chkTVShowMissingTheme.UseVisualStyleBackColor = true
        '
        'chkTVShowMissingClearLogo
        '
        Me.chkTVShowMissingClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowMissingClearLogo.Location = New System.Drawing.Point(6, 64)
        Me.chkTVShowMissingClearLogo.Name = "chkTVShowMissingClearLogo"
        Me.chkTVShowMissingClearLogo.Size = New System.Drawing.Size(161, 17)
        Me.chkTVShowMissingClearLogo.TabIndex = 14
        Me.chkTVShowMissingClearLogo.Text = "Check for ClearLogo"
        Me.chkTVShowMissingClearLogo.UseVisualStyleBackColor = true
        '
        'chkTVShowMissingLandscape
        '
        Me.chkTVShowMissingLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowMissingLandscape.Location = New System.Drawing.Point(6, 112)
        Me.chkTVShowMissingLandscape.Name = "chkTVShowMissingLandscape"
        Me.chkTVShowMissingLandscape.Size = New System.Drawing.Size(161, 17)
        Me.chkTVShowMissingLandscape.TabIndex = 10
        Me.chkTVShowMissingLandscape.Text = "Check for Landscape"
        Me.chkTVShowMissingLandscape.UseVisualStyleBackColor = true
        '
        'chkTVShowMissingCharacterArt
        '
        Me.chkTVShowMissingCharacterArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowMissingCharacterArt.Location = New System.Drawing.Point(6, 32)
        Me.chkTVShowMissingCharacterArt.Name = "chkTVShowMissingCharacterArt"
        Me.chkTVShowMissingCharacterArt.Size = New System.Drawing.Size(161, 17)
        Me.chkTVShowMissingCharacterArt.TabIndex = 15
        Me.chkTVShowMissingCharacterArt.Text = "Check for CharacterArt"
        Me.chkTVShowMissingCharacterArt.UseVisualStyleBackColor = true
        '
        'chkTVShowMissingEFanarts
        '
        Me.chkTVShowMissingEFanarts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowMissingEFanarts.Location = New System.Drawing.Point(6, 80)
        Me.chkTVShowMissingEFanarts.Name = "chkTVShowMissingEFanarts"
        Me.chkTVShowMissingEFanarts.Size = New System.Drawing.Size(161, 17)
        Me.chkTVShowMissingEFanarts.TabIndex = 12
        Me.chkTVShowMissingEFanarts.Text = "Check for Extrafanarts"
        Me.chkTVShowMissingEFanarts.UseVisualStyleBackColor = true
        '
        'chkTVShowMissingPoster
        '
        Me.chkTVShowMissingPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowMissingPoster.Location = New System.Drawing.Point(6, 144)
        Me.chkTVShowMissingPoster.Name = "chkTVShowMissingPoster"
        Me.chkTVShowMissingPoster.Size = New System.Drawing.Size(161, 17)
        Me.chkTVShowMissingPoster.TabIndex = 6
        Me.chkTVShowMissingPoster.Text = "Check for Poster"
        Me.chkTVShowMissingPoster.UseVisualStyleBackColor = true
        '
        'chkTVShowMissingClearArt
        '
        Me.chkTVShowMissingClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowMissingClearArt.Location = New System.Drawing.Point(6, 48)
        Me.chkTVShowMissingClearArt.Name = "chkTVShowMissingClearArt"
        Me.chkTVShowMissingClearArt.Size = New System.Drawing.Size(161, 17)
        Me.chkTVShowMissingClearArt.TabIndex = 13
        Me.chkTVShowMissingClearArt.Text = "Check for ClearArt"
        Me.chkTVShowMissingClearArt.UseVisualStyleBackColor = true
        '
        'chkTVShowMissingNFO
        '
        Me.chkTVShowMissingNFO.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowMissingNFO.Location = New System.Drawing.Point(6, 128)
        Me.chkTVShowMissingNFO.Name = "chkTVShowMissingNFO"
        Me.chkTVShowMissingNFO.Size = New System.Drawing.Size(161, 17)
        Me.chkTVShowMissingNFO.TabIndex = 8
        Me.chkTVShowMissingNFO.Text = "Check for NFO"
        Me.chkTVShowMissingNFO.UseVisualStyleBackColor = true
        '
        'chkTVShowMissingFanart
        '
        Me.chkTVShowMissingFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowMissingFanart.Location = New System.Drawing.Point(6, 96)
        Me.chkTVShowMissingFanart.Name = "chkTVShowMissingFanart"
        Me.chkTVShowMissingFanart.Size = New System.Drawing.Size(161, 17)
        Me.chkTVShowMissingFanart.TabIndex = 7
        Me.chkTVShowMissingFanart.Text = "Check for Fanart"
        Me.chkTVShowMissingFanart.UseVisualStyleBackColor = true
        '
        'gbTVSortTokensOpts
        '
        Me.gbTVSortTokensOpts.Controls.Add(Me.btnTVSortTokenRemove)
        Me.gbTVSortTokensOpts.Controls.Add(Me.btnTVSortTokenAdd)
        Me.gbTVSortTokensOpts.Controls.Add(Me.txtTVSortToken)
        Me.gbTVSortTokensOpts.Controls.Add(Me.lstTVSortTokens)
        Me.gbTVSortTokensOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVSortTokensOpts.Location = New System.Drawing.Point(231, 7)
        Me.gbTVSortTokensOpts.Name = "gbTVSortTokensOpts"
        Me.gbTVSortTokensOpts.Size = New System.Drawing.Size(185, 93)
        Me.gbTVSortTokensOpts.TabIndex = 72
        Me.gbTVSortTokensOpts.TabStop = false
        Me.gbTVSortTokensOpts.Text = "Sort Tokens to Ignore"
        '
        'btnTVSortTokenRemove
        '
        Me.btnTVSortTokenRemove.Image = CType(resources.GetObject("btnTVSortTokenRemove.Image"),System.Drawing.Image)
        Me.btnTVSortTokenRemove.Location = New System.Drawing.Point(156, 64)
        Me.btnTVSortTokenRemove.Name = "btnTVSortTokenRemove"
        Me.btnTVSortTokenRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnTVSortTokenRemove.TabIndex = 3
        Me.btnTVSortTokenRemove.UseVisualStyleBackColor = true
        '
        'btnTVSortTokenAdd
        '
        Me.btnTVSortTokenAdd.Image = CType(resources.GetObject("btnTVSortTokenAdd.Image"),System.Drawing.Image)
        Me.btnTVSortTokenAdd.Location = New System.Drawing.Point(106, 64)
        Me.btnTVSortTokenAdd.Name = "btnTVSortTokenAdd"
        Me.btnTVSortTokenAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnTVSortTokenAdd.TabIndex = 2
        Me.btnTVSortTokenAdd.UseVisualStyleBackColor = true
        '
        'txtTVSortToken
        '
        Me.txtTVSortToken.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVSortToken.Location = New System.Drawing.Point(10, 64)
        Me.txtTVSortToken.Name = "txtTVSortToken"
        Me.txtTVSortToken.Size = New System.Drawing.Size(90, 22)
        Me.txtTVSortToken.TabIndex = 1
        '
        'lstTVSortTokens
        '
        Me.lstTVSortTokens.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstTVSortTokens.FormattingEnabled = true
        Me.lstTVSortTokens.Location = New System.Drawing.Point(10, 15)
        Me.lstTVSortTokens.Name = "lstTVSortTokens"
        Me.lstTVSortTokens.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstTVSortTokens.Size = New System.Drawing.Size(169, 43)
        Me.lstTVSortTokens.Sorted = true
        Me.lstTVSortTokens.TabIndex = 0
        '
        'gbTVGeneralLangOpts
        '
        Me.gbTVGeneralLangOpts.Controls.Add(Me.btnTVGeneralLangFetch)
        Me.gbTVGeneralLangOpts.Controls.Add(Me.cbTVGeneralLang)
        Me.gbTVGeneralLangOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbTVGeneralLangOpts.Location = New System.Drawing.Point(422, 398)
        Me.gbTVGeneralLangOpts.Name = "gbTVGeneralLangOpts"
        Me.gbTVGeneralLangOpts.Size = New System.Drawing.Size(201, 99)
        Me.gbTVGeneralLangOpts.TabIndex = 4
        Me.gbTVGeneralLangOpts.TabStop = false
        Me.gbTVGeneralLangOpts.Text = "Language Options"
        '
        'btnTVGeneralLangFetch
        '
        Me.btnTVGeneralLangFetch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnTVGeneralLangFetch.Location = New System.Drawing.Point(16, 55)
        Me.btnTVGeneralLangFetch.Name = "btnTVGeneralLangFetch"
        Me.btnTVGeneralLangFetch.Size = New System.Drawing.Size(166, 23)
        Me.btnTVGeneralLangFetch.TabIndex = 10
        Me.btnTVGeneralLangFetch.Text = "Fetch Available Languages"
        Me.btnTVGeneralLangFetch.UseVisualStyleBackColor = true
        '
        'cbTVGeneralLang
        '
        Me.cbTVGeneralLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVGeneralLang.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVGeneralLang.Location = New System.Drawing.Point(16, 21)
        Me.cbTVGeneralLang.Name = "cbTVGeneralLang"
        Me.cbTVGeneralLang.Size = New System.Drawing.Size(166, 21)
        Me.cbTVGeneralLang.TabIndex = 11
        '
        'gbTVGeneralMediaListOpts
        '
        Me.gbTVGeneralMediaListOpts.Controls.Add(Me.chkTVDisplayStatus)
        Me.gbTVGeneralMediaListOpts.Controls.Add(Me.chkTVDisplayMissingEpisodes)
        Me.gbTVGeneralMediaListOpts.Controls.Add(Me.gbTVGeneralListEpisodeOpts)
        Me.gbTVGeneralMediaListOpts.Controls.Add(Me.gbTVGeneralListSeasonOpts)
        Me.gbTVGeneralMediaListOpts.Controls.Add(Me.gbTVGeneralListShowOpts)
        Me.gbTVGeneralMediaListOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVGeneralMediaListOpts.Location = New System.Drawing.Point(6, 64)
        Me.gbTVGeneralMediaListOpts.Name = "gbTVGeneralMediaListOpts"
        Me.gbTVGeneralMediaListOpts.Size = New System.Drawing.Size(219, 433)
        Me.gbTVGeneralMediaListOpts.TabIndex = 1
        Me.gbTVGeneralMediaListOpts.TabStop = false
        Me.gbTVGeneralMediaListOpts.Text = "Media List Options"
        '
        'chkTVDisplayStatus
        '
        Me.chkTVDisplayStatus.AutoSize = true
        Me.chkTVDisplayStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVDisplayStatus.Location = New System.Drawing.Point(6, 16)
        Me.chkTVDisplayStatus.Name = "chkTVDisplayStatus"
        Me.chkTVDisplayStatus.Size = New System.Drawing.Size(155, 17)
        Me.chkTVDisplayStatus.TabIndex = 73
        Me.chkTVDisplayStatus.Text = "Display Status in List Title"
        Me.chkTVDisplayStatus.UseVisualStyleBackColor = true
        '
        'chkTVDisplayMissingEpisodes
        '
        Me.chkTVDisplayMissingEpisodes.AutoSize = true
        Me.chkTVDisplayMissingEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVDisplayMissingEpisodes.Location = New System.Drawing.Point(6, 32)
        Me.chkTVDisplayMissingEpisodes.Name = "chkTVDisplayMissingEpisodes"
        Me.chkTVDisplayMissingEpisodes.Size = New System.Drawing.Size(155, 17)
        Me.chkTVDisplayMissingEpisodes.TabIndex = 3
        Me.chkTVDisplayMissingEpisodes.Text = "Display Missing Episodes"
        Me.chkTVDisplayMissingEpisodes.UseVisualStyleBackColor = true
        '
        'gbTVGeneralListEpisodeOpts
        '
        Me.gbTVGeneralListEpisodeOpts.Controls.Add(Me.chkTVEpisodeNfoCol)
        Me.gbTVGeneralListEpisodeOpts.Controls.Add(Me.chkTVEpisodeFanartCol)
        Me.gbTVGeneralListEpisodeOpts.Controls.Add(Me.chkTVEpisodeWatchedCol)
        Me.gbTVGeneralListEpisodeOpts.Controls.Add(Me.chkTVEpisodePosterCol)
        Me.gbTVGeneralListEpisodeOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVGeneralListEpisodeOpts.Location = New System.Drawing.Point(9, 339)
        Me.gbTVGeneralListEpisodeOpts.Name = "gbTVGeneralListEpisodeOpts"
        Me.gbTVGeneralListEpisodeOpts.Size = New System.Drawing.Size(199, 87)
        Me.gbTVGeneralListEpisodeOpts.TabIndex = 2
        Me.gbTVGeneralListEpisodeOpts.TabStop = false
        Me.gbTVGeneralListEpisodeOpts.Text = "Episodes"
        '
        'chkTVEpisodeNfoCol
        '
        Me.chkTVEpisodeNfoCol.AutoSize = true
        Me.chkTVEpisodeNfoCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVEpisodeNfoCol.Location = New System.Drawing.Point(6, 32)
        Me.chkTVEpisodeNfoCol.Name = "chkTVEpisodeNfoCol"
        Me.chkTVEpisodeNfoCol.Size = New System.Drawing.Size(119, 17)
        Me.chkTVEpisodeNfoCol.TabIndex = 2
        Me.chkTVEpisodeNfoCol.Text = "Hide NFO Column"
        Me.chkTVEpisodeNfoCol.UseVisualStyleBackColor = true
        '
        'chkTVEpisodeFanartCol
        '
        Me.chkTVEpisodeFanartCol.AutoSize = true
        Me.chkTVEpisodeFanartCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVEpisodeFanartCol.Location = New System.Drawing.Point(6, 16)
        Me.chkTVEpisodeFanartCol.Name = "chkTVEpisodeFanartCol"
        Me.chkTVEpisodeFanartCol.Size = New System.Drawing.Size(129, 17)
        Me.chkTVEpisodeFanartCol.TabIndex = 1
        Me.chkTVEpisodeFanartCol.Text = "Hide Fanart Column"
        Me.chkTVEpisodeFanartCol.UseVisualStyleBackColor = true
        '
        'chkTVEpisodeWatchedCol
        '
        Me.chkTVEpisodeWatchedCol.AutoSize = true
        Me.chkTVEpisodeWatchedCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVEpisodeWatchedCol.Location = New System.Drawing.Point(6, 64)
        Me.chkTVEpisodeWatchedCol.Name = "chkTVEpisodeWatchedCol"
        Me.chkTVEpisodeWatchedCol.Size = New System.Drawing.Size(142, 17)
        Me.chkTVEpisodeWatchedCol.TabIndex = 83
        Me.chkTVEpisodeWatchedCol.Text = "Hide Watched Column"
        Me.chkTVEpisodeWatchedCol.UseVisualStyleBackColor = true
        '
        'chkTVEpisodePosterCol
        '
        Me.chkTVEpisodePosterCol.AutoSize = true
        Me.chkTVEpisodePosterCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVEpisodePosterCol.Location = New System.Drawing.Point(6, 48)
        Me.chkTVEpisodePosterCol.Name = "chkTVEpisodePosterCol"
        Me.chkTVEpisodePosterCol.Size = New System.Drawing.Size(128, 17)
        Me.chkTVEpisodePosterCol.TabIndex = 0
        Me.chkTVEpisodePosterCol.Text = "Hide Poster Column"
        Me.chkTVEpisodePosterCol.UseVisualStyleBackColor = true
        '
        'gbTVGeneralListSeasonOpts
        '
        Me.gbTVGeneralListSeasonOpts.Controls.Add(Me.chkTVSeasonLandscapeCol)
        Me.gbTVGeneralListSeasonOpts.Controls.Add(Me.chkTVSeasonBannerCol)
        Me.gbTVGeneralListSeasonOpts.Controls.Add(Me.chkTVSeasonFanartCol)
        Me.gbTVGeneralListSeasonOpts.Controls.Add(Me.chkTVSeasonPosterCol)
        Me.gbTVGeneralListSeasonOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVGeneralListSeasonOpts.Location = New System.Drawing.Point(9, 246)
        Me.gbTVGeneralListSeasonOpts.Name = "gbTVGeneralListSeasonOpts"
        Me.gbTVGeneralListSeasonOpts.Size = New System.Drawing.Size(199, 87)
        Me.gbTVGeneralListSeasonOpts.TabIndex = 1
        Me.gbTVGeneralListSeasonOpts.TabStop = false
        Me.gbTVGeneralListSeasonOpts.Text = "Seasons"
        '
        'chkTVSeasonLandscapeCol
        '
        Me.chkTVSeasonLandscapeCol.AutoSize = true
        Me.chkTVSeasonLandscapeCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVSeasonLandscapeCol.Location = New System.Drawing.Point(6, 48)
        Me.chkTVSeasonLandscapeCol.Name = "chkTVSeasonLandscapeCol"
        Me.chkTVSeasonLandscapeCol.Size = New System.Drawing.Size(150, 17)
        Me.chkTVSeasonLandscapeCol.TabIndex = 3
        Me.chkTVSeasonLandscapeCol.Text = "Hide Landscape Column"
        Me.chkTVSeasonLandscapeCol.UseVisualStyleBackColor = true
        '
        'chkTVSeasonBannerCol
        '
        Me.chkTVSeasonBannerCol.AutoSize = true
        Me.chkTVSeasonBannerCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVSeasonBannerCol.Location = New System.Drawing.Point(6, 16)
        Me.chkTVSeasonBannerCol.Name = "chkTVSeasonBannerCol"
        Me.chkTVSeasonBannerCol.Size = New System.Drawing.Size(133, 17)
        Me.chkTVSeasonBannerCol.TabIndex = 2
        Me.chkTVSeasonBannerCol.Text = "Hide Banner Column"
        Me.chkTVSeasonBannerCol.UseVisualStyleBackColor = true
        '
        'chkTVSeasonFanartCol
        '
        Me.chkTVSeasonFanartCol.AutoSize = true
        Me.chkTVSeasonFanartCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVSeasonFanartCol.Location = New System.Drawing.Point(6, 32)
        Me.chkTVSeasonFanartCol.Name = "chkTVSeasonFanartCol"
        Me.chkTVSeasonFanartCol.Size = New System.Drawing.Size(129, 17)
        Me.chkTVSeasonFanartCol.TabIndex = 1
        Me.chkTVSeasonFanartCol.Text = "Hide Fanart Column"
        Me.chkTVSeasonFanartCol.UseVisualStyleBackColor = true
        '
        'chkTVSeasonPosterCol
        '
        Me.chkTVSeasonPosterCol.AutoSize = true
        Me.chkTVSeasonPosterCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVSeasonPosterCol.Location = New System.Drawing.Point(6, 64)
        Me.chkTVSeasonPosterCol.Name = "chkTVSeasonPosterCol"
        Me.chkTVSeasonPosterCol.Size = New System.Drawing.Size(128, 17)
        Me.chkTVSeasonPosterCol.TabIndex = 0
        Me.chkTVSeasonPosterCol.Text = "Hide Poster Column"
        Me.chkTVSeasonPosterCol.UseVisualStyleBackColor = true
        '
        'gbTVGeneralListShowOpts
        '
        Me.gbTVGeneralListShowOpts.Controls.Add(Me.chkTVShowClearLogoCol)
        Me.gbTVGeneralListShowOpts.Controls.Add(Me.chkTVShowClearArtCol)
        Me.gbTVGeneralListShowOpts.Controls.Add(Me.chkTVShowCharacterArtCol)
        Me.gbTVGeneralListShowOpts.Controls.Add(Me.chkTVShowEFanartsCol)
        Me.gbTVGeneralListShowOpts.Controls.Add(Me.chkTVShowThemeCol)
        Me.gbTVGeneralListShowOpts.Controls.Add(Me.chkTVShowLandscapeCol)
        Me.gbTVGeneralListShowOpts.Controls.Add(Me.chkTVShowBannerCol)
        Me.gbTVGeneralListShowOpts.Controls.Add(Me.chkTVShowNfoCol)
        Me.gbTVGeneralListShowOpts.Controls.Add(Me.chkTVShowFanartCol)
        Me.gbTVGeneralListShowOpts.Controls.Add(Me.chkTVShowPosterCol)
        Me.gbTVGeneralListShowOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVGeneralListShowOpts.Location = New System.Drawing.Point(9, 60)
        Me.gbTVGeneralListShowOpts.Name = "gbTVGeneralListShowOpts"
        Me.gbTVGeneralListShowOpts.Size = New System.Drawing.Size(199, 180)
        Me.gbTVGeneralListShowOpts.TabIndex = 0
        Me.gbTVGeneralListShowOpts.TabStop = false
        Me.gbTVGeneralListShowOpts.Text = "Shows"
        '
        'chkTVShowClearLogoCol
        '
        Me.chkTVShowClearLogoCol.AutoSize = true
        Me.chkTVShowClearLogoCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowClearLogoCol.Location = New System.Drawing.Point(6, 64)
        Me.chkTVShowClearLogoCol.Name = "chkTVShowClearLogoCol"
        Me.chkTVShowClearLogoCol.Size = New System.Drawing.Size(148, 17)
        Me.chkTVShowClearLogoCol.TabIndex = 88
        Me.chkTVShowClearLogoCol.Text = "Hide ClearLogo Column"
        Me.chkTVShowClearLogoCol.UseVisualStyleBackColor = true
        '
        'chkTVShowClearArtCol
        '
        Me.chkTVShowClearArtCol.AutoSize = true
        Me.chkTVShowClearArtCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowClearArtCol.Location = New System.Drawing.Point(6, 48)
        Me.chkTVShowClearArtCol.Name = "chkTVShowClearArtCol"
        Me.chkTVShowClearArtCol.Size = New System.Drawing.Size(137, 17)
        Me.chkTVShowClearArtCol.TabIndex = 87
        Me.chkTVShowClearArtCol.Text = "Hide ClearArt Column"
        Me.chkTVShowClearArtCol.UseVisualStyleBackColor = true
        '
        'chkTVShowCharacterArtCol
        '
        Me.chkTVShowCharacterArtCol.AutoSize = true
        Me.chkTVShowCharacterArtCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowCharacterArtCol.Location = New System.Drawing.Point(6, 32)
        Me.chkTVShowCharacterArtCol.Name = "chkTVShowCharacterArtCol"
        Me.chkTVShowCharacterArtCol.Size = New System.Drawing.Size(160, 17)
        Me.chkTVShowCharacterArtCol.TabIndex = 89
        Me.chkTVShowCharacterArtCol.Text = "Hide CharacterArt Column"
        Me.chkTVShowCharacterArtCol.UseVisualStyleBackColor = true
        '
        'chkTVShowEFanartsCol
        '
        Me.chkTVShowEFanartsCol.AutoSize = true
        Me.chkTVShowEFanartsCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowEFanartsCol.Location = New System.Drawing.Point(6, 80)
        Me.chkTVShowEFanartsCol.Name = "chkTVShowEFanartsCol"
        Me.chkTVShowEFanartsCol.Size = New System.Drawing.Size(152, 17)
        Me.chkTVShowEFanartsCol.TabIndex = 82
        Me.chkTVShowEFanartsCol.Text = "Hide Extrafanart Column"
        Me.chkTVShowEFanartsCol.UseVisualStyleBackColor = true
        '
        'chkTVShowThemeCol
        '
        Me.chkTVShowThemeCol.AutoSize = true
        Me.chkTVShowThemeCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowThemeCol.Location = New System.Drawing.Point(6, 160)
        Me.chkTVShowThemeCol.Name = "chkTVShowThemeCol"
        Me.chkTVShowThemeCol.Size = New System.Drawing.Size(129, 17)
        Me.chkTVShowThemeCol.TabIndex = 85
        Me.chkTVShowThemeCol.Text = "Hide Theme Column"
        Me.chkTVShowThemeCol.UseVisualStyleBackColor = true
        '
        'chkTVShowLandscapeCol
        '
        Me.chkTVShowLandscapeCol.AutoSize = true
        Me.chkTVShowLandscapeCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowLandscapeCol.Location = New System.Drawing.Point(6, 112)
        Me.chkTVShowLandscapeCol.Name = "chkTVShowLandscapeCol"
        Me.chkTVShowLandscapeCol.Size = New System.Drawing.Size(150, 17)
        Me.chkTVShowLandscapeCol.TabIndex = 84
        Me.chkTVShowLandscapeCol.Text = "Hide Landscape Column"
        Me.chkTVShowLandscapeCol.UseVisualStyleBackColor = true
        '
        'chkTVShowBannerCol
        '
        Me.chkTVShowBannerCol.AutoSize = true
        Me.chkTVShowBannerCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowBannerCol.Location = New System.Drawing.Point(6, 16)
        Me.chkTVShowBannerCol.Name = "chkTVShowBannerCol"
        Me.chkTVShowBannerCol.Size = New System.Drawing.Size(133, 17)
        Me.chkTVShowBannerCol.TabIndex = 86
        Me.chkTVShowBannerCol.Text = "Hide Banner Column"
        Me.chkTVShowBannerCol.UseVisualStyleBackColor = true
        '
        'chkTVShowNfoCol
        '
        Me.chkTVShowNfoCol.AutoSize = true
        Me.chkTVShowNfoCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowNfoCol.Location = New System.Drawing.Point(6, 128)
        Me.chkTVShowNfoCol.Name = "chkTVShowNfoCol"
        Me.chkTVShowNfoCol.Size = New System.Drawing.Size(119, 17)
        Me.chkTVShowNfoCol.TabIndex = 2
        Me.chkTVShowNfoCol.Text = "Hide NFO Column"
        Me.chkTVShowNfoCol.UseVisualStyleBackColor = true
        '
        'chkTVShowFanartCol
        '
        Me.chkTVShowFanartCol.AutoSize = true
        Me.chkTVShowFanartCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowFanartCol.Location = New System.Drawing.Point(6, 96)
        Me.chkTVShowFanartCol.Name = "chkTVShowFanartCol"
        Me.chkTVShowFanartCol.Size = New System.Drawing.Size(129, 17)
        Me.chkTVShowFanartCol.TabIndex = 1
        Me.chkTVShowFanartCol.Text = "Hide Fanart Column"
        Me.chkTVShowFanartCol.UseVisualStyleBackColor = true
        '
        'chkTVShowPosterCol
        '
        Me.chkTVShowPosterCol.AutoSize = true
        Me.chkTVShowPosterCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowPosterCol.Location = New System.Drawing.Point(6, 144)
        Me.chkTVShowPosterCol.Name = "chkTVShowPosterCol"
        Me.chkTVShowPosterCol.Size = New System.Drawing.Size(128, 17)
        Me.chkTVShowPosterCol.TabIndex = 0
        Me.chkTVShowPosterCol.Text = "Hide Poster Column"
        Me.chkTVShowPosterCol.UseVisualStyleBackColor = true
        '
        'gbTVEpisodeFilterOpts
        '
        Me.gbTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterReset)
        Me.gbTVEpisodeFilterOpts.Controls.Add(Me.chkTVEpisodeNoFilter)
        Me.gbTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterDown)
        Me.gbTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterUp)
        Me.gbTVEpisodeFilterOpts.Controls.Add(Me.chkTVEpisodeProperCase)
        Me.gbTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterRemove)
        Me.gbTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterAdd)
        Me.gbTVEpisodeFilterOpts.Controls.Add(Me.txtTVEpisodeFilter)
        Me.gbTVEpisodeFilterOpts.Controls.Add(Me.lstTVEpisodeFilter)
        Me.gbTVEpisodeFilterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVEpisodeFilterOpts.Location = New System.Drawing.Point(422, 187)
        Me.gbTVEpisodeFilterOpts.Name = "gbTVEpisodeFilterOpts"
        Me.gbTVEpisodeFilterOpts.Size = New System.Drawing.Size(325, 205)
        Me.gbTVEpisodeFilterOpts.TabIndex = 3
        Me.gbTVEpisodeFilterOpts.TabStop = false
        Me.gbTVEpisodeFilterOpts.Text = "Episode Folder/File Name Filters"
        '
        'btnTVEpisodeFilterReset
        '
        Me.btnTVEpisodeFilterReset.Image = CType(resources.GetObject("btnTVEpisodeFilterReset.Image"),System.Drawing.Image)
        Me.btnTVEpisodeFilterReset.Location = New System.Drawing.Point(297, 38)
        Me.btnTVEpisodeFilterReset.Name = "btnTVEpisodeFilterReset"
        Me.btnTVEpisodeFilterReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterReset.TabIndex = 3
        Me.btnTVEpisodeFilterReset.UseVisualStyleBackColor = true
        '
        'chkTVEpisodeNoFilter
        '
        Me.chkTVEpisodeNoFilter.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVEpisodeNoFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkTVEpisodeNoFilter.Location = New System.Drawing.Point(6, 15)
        Me.chkTVEpisodeNoFilter.Name = "chkTVEpisodeNoFilter"
        Me.chkTVEpisodeNoFilter.Size = New System.Drawing.Size(371, 21)
        Me.chkTVEpisodeNoFilter.TabIndex = 0
        Me.chkTVEpisodeNoFilter.Text = "Build Episode Title Instead of Filtering"
        Me.chkTVEpisodeNoFilter.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVEpisodeNoFilter.UseVisualStyleBackColor = true
        '
        'btnTVEpisodeFilterDown
        '
        Me.btnTVEpisodeFilterDown.Image = CType(resources.GetObject("btnTVEpisodeFilterDown.Image"),System.Drawing.Image)
        Me.btnTVEpisodeFilterDown.Location = New System.Drawing.Point(267, 176)
        Me.btnTVEpisodeFilterDown.Name = "btnTVEpisodeFilterDown"
        Me.btnTVEpisodeFilterDown.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterDown.TabIndex = 7
        Me.btnTVEpisodeFilterDown.UseVisualStyleBackColor = true
        '
        'btnTVEpisodeFilterUp
        '
        Me.btnTVEpisodeFilterUp.Image = CType(resources.GetObject("btnTVEpisodeFilterUp.Image"),System.Drawing.Image)
        Me.btnTVEpisodeFilterUp.Location = New System.Drawing.Point(243, 176)
        Me.btnTVEpisodeFilterUp.Name = "btnTVEpisodeFilterUp"
        Me.btnTVEpisodeFilterUp.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterUp.TabIndex = 6
        Me.btnTVEpisodeFilterUp.UseVisualStyleBackColor = true
        '
        'chkTVEpisodeProperCase
        '
        Me.chkTVEpisodeProperCase.AutoSize = true
        Me.chkTVEpisodeProperCase.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVEpisodeProperCase.Location = New System.Drawing.Point(6, 47)
        Me.chkTVEpisodeProperCase.Name = "chkTVEpisodeProperCase"
        Me.chkTVEpisodeProperCase.Size = New System.Drawing.Size(181, 17)
        Me.chkTVEpisodeProperCase.TabIndex = 1
        Me.chkTVEpisodeProperCase.Text = "Convert Names to Proper Case"
        Me.chkTVEpisodeProperCase.UseVisualStyleBackColor = true
        '
        'btnTVEpisodeFilterRemove
        '
        Me.btnTVEpisodeFilterRemove.Image = CType(resources.GetObject("btnTVEpisodeFilterRemove.Image"),System.Drawing.Image)
        Me.btnTVEpisodeFilterRemove.Location = New System.Drawing.Point(301, 176)
        Me.btnTVEpisodeFilterRemove.Name = "btnTVEpisodeFilterRemove"
        Me.btnTVEpisodeFilterRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterRemove.TabIndex = 8
        Me.btnTVEpisodeFilterRemove.UseVisualStyleBackColor = true
        '
        'btnTVEpisodeFilterAdd
        '
        Me.btnTVEpisodeFilterAdd.Image = CType(resources.GetObject("btnTVEpisodeFilterAdd.Image"),System.Drawing.Image)
        Me.btnTVEpisodeFilterAdd.Location = New System.Drawing.Point(207, 176)
        Me.btnTVEpisodeFilterAdd.Name = "btnTVEpisodeFilterAdd"
        Me.btnTVEpisodeFilterAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterAdd.TabIndex = 5
        Me.btnTVEpisodeFilterAdd.UseVisualStyleBackColor = true
        '
        'txtTVEpisodeFilter
        '
        Me.txtTVEpisodeFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVEpisodeFilter.Location = New System.Drawing.Point(6, 177)
        Me.txtTVEpisodeFilter.Name = "txtTVEpisodeFilter"
        Me.txtTVEpisodeFilter.Size = New System.Drawing.Size(195, 22)
        Me.txtTVEpisodeFilter.TabIndex = 4
        '
        'lstTVEpisodeFilter
        '
        Me.lstTVEpisodeFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstTVEpisodeFilter.FormattingEnabled = true
        Me.lstTVEpisodeFilter.Location = New System.Drawing.Point(6, 64)
        Me.lstTVEpisodeFilter.Name = "lstTVEpisodeFilter"
        Me.lstTVEpisodeFilter.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstTVEpisodeFilter.Size = New System.Drawing.Size(314, 95)
        Me.lstTVEpisodeFilter.TabIndex = 2
        '
        'gbTVGeneralMiscOpts
        '
        Me.gbTVGeneralMiscOpts.Controls.Add(Me.chkTVGeneralMarkNewShows)
        Me.gbTVGeneralMiscOpts.Controls.Add(Me.chkTVGeneralMarkNewEpisodes)
        Me.gbTVGeneralMiscOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVGeneralMiscOpts.Location = New System.Drawing.Point(6, 7)
        Me.gbTVGeneralMiscOpts.Name = "gbTVGeneralMiscOpts"
        Me.gbTVGeneralMiscOpts.Size = New System.Drawing.Size(219, 54)
        Me.gbTVGeneralMiscOpts.TabIndex = 0
        Me.gbTVGeneralMiscOpts.TabStop = false
        Me.gbTVGeneralMiscOpts.Text = "Miscellaneous"
        '
        'chkTVGeneralMarkNewShows
        '
        Me.chkTVGeneralMarkNewShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVGeneralMarkNewShows.Location = New System.Drawing.Point(6, 16)
        Me.chkTVGeneralMarkNewShows.Name = "chkTVGeneralMarkNewShows"
        Me.chkTVGeneralMarkNewShows.Size = New System.Drawing.Size(204, 17)
        Me.chkTVGeneralMarkNewShows.TabIndex = 3
        Me.chkTVGeneralMarkNewShows.Text = "Mark New Shows"
        Me.chkTVGeneralMarkNewShows.UseVisualStyleBackColor = true
        '
        'chkTVGeneralMarkNewEpisodes
        '
        Me.chkTVGeneralMarkNewEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVGeneralMarkNewEpisodes.Location = New System.Drawing.Point(6, 32)
        Me.chkTVGeneralMarkNewEpisodes.Name = "chkTVGeneralMarkNewEpisodes"
        Me.chkTVGeneralMarkNewEpisodes.Size = New System.Drawing.Size(204, 17)
        Me.chkTVGeneralMarkNewEpisodes.TabIndex = 4
        Me.chkTVGeneralMarkNewEpisodes.Text = "Mark New Episodes"
        Me.chkTVGeneralMarkNewEpisodes.UseVisualStyleBackColor = true
        '
        'gbTVShowFilterOpts
        '
        Me.gbTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterReset)
        Me.gbTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterDown)
        Me.gbTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterUp)
        Me.gbTVShowFilterOpts.Controls.Add(Me.chkTVShowProperCase)
        Me.gbTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterRemove)
        Me.gbTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterAdd)
        Me.gbTVShowFilterOpts.Controls.Add(Me.txtTVShowFilter)
        Me.gbTVShowFilterOpts.Controls.Add(Me.lstTVShowFilter)
        Me.gbTVShowFilterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVShowFilterOpts.Location = New System.Drawing.Point(422, 9)
        Me.gbTVShowFilterOpts.Name = "gbTVShowFilterOpts"
        Me.gbTVShowFilterOpts.Size = New System.Drawing.Size(325, 175)
        Me.gbTVShowFilterOpts.TabIndex = 2
        Me.gbTVShowFilterOpts.TabStop = false
        Me.gbTVShowFilterOpts.Text = "Show Folder/File Name Filters"
        '
        'btnTVShowFilterReset
        '
        Me.btnTVShowFilterReset.Image = CType(resources.GetObject("btnTVShowFilterReset.Image"),System.Drawing.Image)
        Me.btnTVShowFilterReset.Location = New System.Drawing.Point(297, 9)
        Me.btnTVShowFilterReset.Name = "btnTVShowFilterReset"
        Me.btnTVShowFilterReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterReset.TabIndex = 2
        Me.btnTVShowFilterReset.UseVisualStyleBackColor = true
        '
        'btnTVShowFilterDown
        '
        Me.btnTVShowFilterDown.Image = CType(resources.GetObject("btnTVShowFilterDown.Image"),System.Drawing.Image)
        Me.btnTVShowFilterDown.Location = New System.Drawing.Point(267, 147)
        Me.btnTVShowFilterDown.Name = "btnTVShowFilterDown"
        Me.btnTVShowFilterDown.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterDown.TabIndex = 6
        Me.btnTVShowFilterDown.UseVisualStyleBackColor = true
        '
        'btnTVShowFilterUp
        '
        Me.btnTVShowFilterUp.Image = CType(resources.GetObject("btnTVShowFilterUp.Image"),System.Drawing.Image)
        Me.btnTVShowFilterUp.Location = New System.Drawing.Point(243, 147)
        Me.btnTVShowFilterUp.Name = "btnTVShowFilterUp"
        Me.btnTVShowFilterUp.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterUp.TabIndex = 5
        Me.btnTVShowFilterUp.UseVisualStyleBackColor = true
        '
        'chkTVShowProperCase
        '
        Me.chkTVShowProperCase.AutoSize = true
        Me.chkTVShowProperCase.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowProperCase.Location = New System.Drawing.Point(6, 17)
        Me.chkTVShowProperCase.Name = "chkTVShowProperCase"
        Me.chkTVShowProperCase.Size = New System.Drawing.Size(181, 17)
        Me.chkTVShowProperCase.TabIndex = 0
        Me.chkTVShowProperCase.Text = "Convert Names to Proper Case"
        Me.chkTVShowProperCase.UseVisualStyleBackColor = true
        '
        'btnTVShowFilterRemove
        '
        Me.btnTVShowFilterRemove.Image = CType(resources.GetObject("btnTVShowFilterRemove.Image"),System.Drawing.Image)
        Me.btnTVShowFilterRemove.Location = New System.Drawing.Point(301, 147)
        Me.btnTVShowFilterRemove.Name = "btnTVShowFilterRemove"
        Me.btnTVShowFilterRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterRemove.TabIndex = 7
        Me.btnTVShowFilterRemove.UseVisualStyleBackColor = true
        '
        'btnTVShowFilterAdd
        '
        Me.btnTVShowFilterAdd.Image = CType(resources.GetObject("btnTVShowFilterAdd.Image"),System.Drawing.Image)
        Me.btnTVShowFilterAdd.Location = New System.Drawing.Point(207, 147)
        Me.btnTVShowFilterAdd.Name = "btnTVShowFilterAdd"
        Me.btnTVShowFilterAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterAdd.TabIndex = 4
        Me.btnTVShowFilterAdd.UseVisualStyleBackColor = true
        '
        'txtTVShowFilter
        '
        Me.txtTVShowFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVShowFilter.Location = New System.Drawing.Point(6, 147)
        Me.txtTVShowFilter.Name = "txtTVShowFilter"
        Me.txtTVShowFilter.Size = New System.Drawing.Size(195, 22)
        Me.txtTVShowFilter.TabIndex = 3
        '
        'lstTVShowFilter
        '
        Me.lstTVShowFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstTVShowFilter.FormattingEnabled = true
        Me.lstTVShowFilter.Location = New System.Drawing.Point(6, 35)
        Me.lstTVShowFilter.Name = "lstTVShowFilter"
        Me.lstTVShowFilter.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstTVShowFilter.Size = New System.Drawing.Size(314, 95)
        Me.lstTVShowFilter.TabIndex = 1
        '
        'pnlTVSources
        '
        Me.pnlTVSources.BackColor = System.Drawing.Color.White
        Me.pnlTVSources.Controls.Add(Me.tcTVSources)
        Me.pnlTVSources.Location = New System.Drawing.Point(900, 900)
        Me.pnlTVSources.Name = "pnlTVSources"
        Me.pnlTVSources.Size = New System.Drawing.Size(750, 500)
        Me.pnlTVSources.TabIndex = 11
        Me.pnlTVSources.Visible = false
        '
        'tcTVSources
        '
        Me.tcTVSources.Controls.Add(Me.tpTVSourcesGeneral)
        Me.tcTVSources.Controls.Add(Me.tpTVSourcesRegex)
        Me.tcTVSources.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.tcTVSources.Location = New System.Drawing.Point(3, 4)
        Me.tcTVSources.Name = "tcTVSources"
        Me.tcTVSources.SelectedIndex = 0
        Me.tcTVSources.Size = New System.Drawing.Size(744, 493)
        Me.tcTVSources.TabIndex = 0
        '
        'tpTVSourcesGeneral
        '
        Me.tpTVSourcesGeneral.Controls.Add(Me.gbTVFileNaming)
        Me.tpTVSourcesGeneral.Controls.Add(Me.lvTVSources)
        Me.tpTVSourcesGeneral.Controls.Add(Me.gbTVSourcesMiscOpts)
        Me.tpTVSourcesGeneral.Controls.Add(Me.btnTVSourceAdd)
        Me.tpTVSourcesGeneral.Controls.Add(Me.btnTVSourceEdit)
        Me.tpTVSourcesGeneral.Controls.Add(Me.btnRemTVSource)
        Me.tpTVSourcesGeneral.Location = New System.Drawing.Point(4, 22)
        Me.tpTVSourcesGeneral.Name = "tpTVSourcesGeneral"
        Me.tpTVSourcesGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTVSourcesGeneral.Size = New System.Drawing.Size(736, 467)
        Me.tpTVSourcesGeneral.TabIndex = 0
        Me.tpTVSourcesGeneral.Text = "General"
        Me.tpTVSourcesGeneral.UseVisualStyleBackColor = true
        '
        'gbTVFileNaming
        '
        Me.gbTVFileNaming.Controls.Add(Me.tcTVFileNaming)
        Me.gbTVFileNaming.Location = New System.Drawing.Point(163, 110)
        Me.gbTVFileNaming.Name = "gbTVFileNaming"
        Me.gbTVFileNaming.Size = New System.Drawing.Size(567, 351)
        Me.gbTVFileNaming.TabIndex = 6
        Me.gbTVFileNaming.TabStop = false
        Me.gbTVFileNaming.Text = "File Naming"
        '
        'tcTVFileNaming
        '
        Me.tcTVFileNaming.Controls.Add(Me.tpTVFileNamingXBMC)
        Me.tcTVFileNaming.Controls.Add(Me.tpTVFileNamingNMT)
        Me.tcTVFileNaming.Controls.Add(Me.tpTVFileNamingBoxee)
        Me.tcTVFileNaming.Controls.Add(Me.tpTVFileNamingExpert)
        Me.tcTVFileNaming.Location = New System.Drawing.Point(6, 18)
        Me.tcTVFileNaming.Name = "tcTVFileNaming"
        Me.tcTVFileNaming.SelectedIndex = 0
        Me.tcTVFileNaming.Size = New System.Drawing.Size(555, 327)
        Me.tcTVFileNaming.TabIndex = 0
        '
        'tpTVFileNamingXBMC
        '
        Me.tpTVFileNamingXBMC.Controls.Add(Me.gbTVXBMCAdditional)
        Me.tpTVFileNamingXBMC.Controls.Add(Me.gbTVFrodo)
        Me.tpTVFileNamingXBMC.Location = New System.Drawing.Point(4, 22)
        Me.tpTVFileNamingXBMC.Name = "tpTVFileNamingXBMC"
        Me.tpTVFileNamingXBMC.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTVFileNamingXBMC.Size = New System.Drawing.Size(547, 301)
        Me.tpTVFileNamingXBMC.TabIndex = 0
        Me.tpTVFileNamingXBMC.Text = "XBMC"
        Me.tpTVFileNamingXBMC.UseVisualStyleBackColor = true
        '
        'gbTVXBMCAdditional
        '
        Me.gbTVXBMCAdditional.Controls.Add(Me.chkTVShowExtrafanartsXBMC)
        Me.gbTVXBMCAdditional.Controls.Add(Me.btnTVShowTVThemeBrowse)
        Me.gbTVXBMCAdditional.Controls.Add(Me.txtTVShowTVThemeFolderXBMC)
        Me.gbTVXBMCAdditional.Controls.Add(Me.chkTVShowTVThemeXBMC)
        Me.gbTVXBMCAdditional.Controls.Add(Me.chkTVSeasonLandscapeXBMC)
        Me.gbTVXBMCAdditional.Controls.Add(Me.chkTVShowLandscapeXBMC)
        Me.gbTVXBMCAdditional.Controls.Add(Me.chkTVShowCharacterArtXBMC)
        Me.gbTVXBMCAdditional.Controls.Add(Me.chkTVShowClearArtXBMC)
        Me.gbTVXBMCAdditional.Controls.Add(Me.chkTVShowClearLogoXBMC)
        Me.gbTVXBMCAdditional.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gbTVXBMCAdditional.Location = New System.Drawing.Point(341, 6)
        Me.gbTVXBMCAdditional.Name = "gbTVXBMCAdditional"
        Me.gbTVXBMCAdditional.Size = New System.Drawing.Size(200, 268)
        Me.gbTVXBMCAdditional.TabIndex = 2
        Me.gbTVXBMCAdditional.TabStop = false
        Me.gbTVXBMCAdditional.Text = "Additional Stuff"
        '
        'chkTVShowExtrafanartsXBMC
        '
        Me.chkTVShowExtrafanartsXBMC.AutoSize = true
        Me.chkTVShowExtrafanartsXBMC.Enabled = false
        Me.chkTVShowExtrafanartsXBMC.Location = New System.Drawing.Point(7, 93)
        Me.chkTVShowExtrafanartsXBMC.Name = "chkTVShowExtrafanartsXBMC"
        Me.chkTVShowExtrafanartsXBMC.Size = New System.Drawing.Size(87, 17)
        Me.chkTVShowExtrafanartsXBMC.TabIndex = 8
        Me.chkTVShowExtrafanartsXBMC.Text = "Extrafanarts"
        Me.chkTVShowExtrafanartsXBMC.UseVisualStyleBackColor = true
        '
        'btnTVShowTVThemeBrowse
        '
        Me.btnTVShowTVThemeBrowse.Enabled = false
        Me.btnTVShowTVThemeBrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnTVShowTVThemeBrowse.Location = New System.Drawing.Point(169, 192)
        Me.btnTVShowTVThemeBrowse.Name = "btnTVShowTVThemeBrowse"
        Me.btnTVShowTVThemeBrowse.Size = New System.Drawing.Size(25, 22)
        Me.btnTVShowTVThemeBrowse.TabIndex = 7
        Me.btnTVShowTVThemeBrowse.Text = "..."
        Me.btnTVShowTVThemeBrowse.UseVisualStyleBackColor = true
        '
        'txtTVShowTVThemeFolderXBMC
        '
        Me.txtTVShowTVThemeFolderXBMC.Enabled = false
        Me.txtTVShowTVThemeFolderXBMC.Location = New System.Drawing.Point(7, 192)
        Me.txtTVShowTVThemeFolderXBMC.Name = "txtTVShowTVThemeFolderXBMC"
        Me.txtTVShowTVThemeFolderXBMC.Size = New System.Drawing.Size(156, 22)
        Me.txtTVShowTVThemeFolderXBMC.TabIndex = 6
        '
        'chkTVShowTVThemeXBMC
        '
        Me.chkTVShowTVThemeXBMC.AutoSize = true
        Me.chkTVShowTVThemeXBMC.Enabled = false
        Me.chkTVShowTVThemeXBMC.Location = New System.Drawing.Point(7, 168)
        Me.chkTVShowTVThemeXBMC.Name = "chkTVShowTVThemeXBMC"
        Me.chkTVShowTVThemeXBMC.Size = New System.Drawing.Size(74, 17)
        Me.chkTVShowTVThemeXBMC.TabIndex = 5
        Me.chkTVShowTVThemeXBMC.Text = "TV Theme"
        Me.chkTVShowTVThemeXBMC.UseVisualStyleBackColor = true
        '
        'chkTVSeasonLandscapeXBMC
        '
        Me.chkTVSeasonLandscapeXBMC.AutoSize = true
        Me.chkTVSeasonLandscapeXBMC.Enabled = false
        Me.chkTVSeasonLandscapeXBMC.Location = New System.Drawing.Point(7, 141)
        Me.chkTVSeasonLandscapeXBMC.Name = "chkTVSeasonLandscapeXBMC"
        Me.chkTVSeasonLandscapeXBMC.Size = New System.Drawing.Size(120, 17)
        Me.chkTVSeasonLandscapeXBMC.TabIndex = 4
        Me.chkTVSeasonLandscapeXBMC.Text = "Season Landscape"
        Me.chkTVSeasonLandscapeXBMC.UseVisualStyleBackColor = true
        '
        'chkTVShowLandscapeXBMC
        '
        Me.chkTVShowLandscapeXBMC.AutoSize = true
        Me.chkTVShowLandscapeXBMC.Enabled = false
        Me.chkTVShowLandscapeXBMC.Location = New System.Drawing.Point(7, 117)
        Me.chkTVShowLandscapeXBMC.Name = "chkTVShowLandscapeXBMC"
        Me.chkTVShowLandscapeXBMC.Size = New System.Drawing.Size(112, 17)
        Me.chkTVShowLandscapeXBMC.TabIndex = 3
        Me.chkTVShowLandscapeXBMC.Text = "Show Landscape"
        Me.chkTVShowLandscapeXBMC.UseVisualStyleBackColor = true
        '
        'chkTVShowCharacterArtXBMC
        '
        Me.chkTVShowCharacterArtXBMC.AutoSize = true
        Me.chkTVShowCharacterArtXBMC.Enabled = false
        Me.chkTVShowCharacterArtXBMC.Location = New System.Drawing.Point(7, 70)
        Me.chkTVShowCharacterArtXBMC.Name = "chkTVShowCharacterArtXBMC"
        Me.chkTVShowCharacterArtXBMC.Size = New System.Drawing.Size(90, 17)
        Me.chkTVShowCharacterArtXBMC.TabIndex = 2
        Me.chkTVShowCharacterArtXBMC.Text = "CharacterArt"
        Me.chkTVShowCharacterArtXBMC.UseVisualStyleBackColor = true
        '
        'chkTVShowClearArtXBMC
        '
        Me.chkTVShowClearArtXBMC.AutoSize = true
        Me.chkTVShowClearArtXBMC.Enabled = false
        Me.chkTVShowClearArtXBMC.Location = New System.Drawing.Point(7, 46)
        Me.chkTVShowClearArtXBMC.Name = "chkTVShowClearArtXBMC"
        Me.chkTVShowClearArtXBMC.Size = New System.Drawing.Size(67, 17)
        Me.chkTVShowClearArtXBMC.TabIndex = 1
        Me.chkTVShowClearArtXBMC.Text = "ClearArt"
        Me.chkTVShowClearArtXBMC.UseVisualStyleBackColor = true
        '
        'chkTVShowClearLogoXBMC
        '
        Me.chkTVShowClearLogoXBMC.AutoSize = true
        Me.chkTVShowClearLogoXBMC.Enabled = false
        Me.chkTVShowClearLogoXBMC.Location = New System.Drawing.Point(7, 22)
        Me.chkTVShowClearLogoXBMC.Name = "chkTVShowClearLogoXBMC"
        Me.chkTVShowClearLogoXBMC.Size = New System.Drawing.Size(78, 17)
        Me.chkTVShowClearLogoXBMC.TabIndex = 0
        Me.chkTVShowClearLogoXBMC.Text = "ClearLogo"
        Me.chkTVShowClearLogoXBMC.UseVisualStyleBackColor = true
        '
        'gbTVFrodo
        '
        Me.gbTVFrodo.Controls.Add(Me.chkTVSeasonPosterFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVShowBannerFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVUseFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVEpisodeActorThumbsFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVSeasonBannerFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVEpisodePosterFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVShowActorThumbsFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVSeasonFanartFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVShowFanartFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVShowPosterFrodo)
        Me.gbTVFrodo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbTVFrodo.Location = New System.Drawing.Point(6, 6)
        Me.gbTVFrodo.Name = "gbTVFrodo"
        Me.gbTVFrodo.Size = New System.Drawing.Size(152, 289)
        Me.gbTVFrodo.TabIndex = 1
        Me.gbTVFrodo.TabStop = false
        Me.gbTVFrodo.Text = "Frodo"
        '
        'chkTVSeasonPosterFrodo
        '
        Me.chkTVSeasonPosterFrodo.AutoSize = true
        Me.chkTVSeasonPosterFrodo.Enabled = false
        Me.chkTVSeasonPosterFrodo.Location = New System.Drawing.Point(6, 136)
        Me.chkTVSeasonPosterFrodo.Name = "chkTVSeasonPosterFrodo"
        Me.chkTVSeasonPosterFrodo.Size = New System.Drawing.Size(98, 17)
        Me.chkTVSeasonPosterFrodo.TabIndex = 12
        Me.chkTVSeasonPosterFrodo.Text = "Season Poster"
        Me.chkTVSeasonPosterFrodo.UseVisualStyleBackColor = true
        '
        'chkTVShowBannerFrodo
        '
        Me.chkTVShowBannerFrodo.AutoSize = true
        Me.chkTVShowBannerFrodo.Enabled = false
        Me.chkTVShowBannerFrodo.Location = New System.Drawing.Point(6, 113)
        Me.chkTVShowBannerFrodo.Name = "chkTVShowBannerFrodo"
        Me.chkTVShowBannerFrodo.Size = New System.Drawing.Size(95, 17)
        Me.chkTVShowBannerFrodo.TabIndex = 11
        Me.chkTVShowBannerFrodo.Text = "Show Banner"
        Me.chkTVShowBannerFrodo.UseVisualStyleBackColor = true
        '
        'chkTVUseFrodo
        '
        Me.chkTVUseFrodo.AutoSize = true
        Me.chkTVUseFrodo.Location = New System.Drawing.Point(6, 21)
        Me.chkTVUseFrodo.Name = "chkTVUseFrodo"
        Me.chkTVUseFrodo.Size = New System.Drawing.Size(45, 17)
        Me.chkTVUseFrodo.TabIndex = 10
        Me.chkTVUseFrodo.Text = "Use"
        Me.chkTVUseFrodo.UseVisualStyleBackColor = true
        '
        'chkTVEpisodeActorThumbsFrodo
        '
        Me.chkTVEpisodeActorThumbsFrodo.AutoSize = true
        Me.chkTVEpisodeActorThumbsFrodo.Enabled = false
        Me.chkTVEpisodeActorThumbsFrodo.Location = New System.Drawing.Point(6, 251)
        Me.chkTVEpisodeActorThumbsFrodo.Name = "chkTVEpisodeActorThumbsFrodo"
        Me.chkTVEpisodeActorThumbsFrodo.Size = New System.Drawing.Size(140, 17)
        Me.chkTVEpisodeActorThumbsFrodo.TabIndex = 9
        Me.chkTVEpisodeActorThumbsFrodo.Text = "Episode Actor Thumbs"
        Me.chkTVEpisodeActorThumbsFrodo.UseVisualStyleBackColor = true
        '
        'chkTVSeasonBannerFrodo
        '
        Me.chkTVSeasonBannerFrodo.AutoSize = true
        Me.chkTVSeasonBannerFrodo.Enabled = false
        Me.chkTVSeasonBannerFrodo.Location = New System.Drawing.Point(6, 182)
        Me.chkTVSeasonBannerFrodo.Name = "chkTVSeasonBannerFrodo"
        Me.chkTVSeasonBannerFrodo.Size = New System.Drawing.Size(103, 17)
        Me.chkTVSeasonBannerFrodo.TabIndex = 8
        Me.chkTVSeasonBannerFrodo.Text = "Season Banner"
        Me.chkTVSeasonBannerFrodo.UseVisualStyleBackColor = true
        '
        'chkTVEpisodePosterFrodo
        '
        Me.chkTVEpisodePosterFrodo.AutoSize = true
        Me.chkTVEpisodePosterFrodo.Enabled = false
        Me.chkTVEpisodePosterFrodo.Location = New System.Drawing.Point(6, 205)
        Me.chkTVEpisodePosterFrodo.Name = "chkTVEpisodePosterFrodo"
        Me.chkTVEpisodePosterFrodo.Size = New System.Drawing.Size(102, 17)
        Me.chkTVEpisodePosterFrodo.TabIndex = 5
        Me.chkTVEpisodePosterFrodo.Text = "Episode Poster"
        Me.chkTVEpisodePosterFrodo.UseVisualStyleBackColor = true
        '
        'chkTVShowActorThumbsFrodo
        '
        Me.chkTVShowActorThumbsFrodo.AutoSize = true
        Me.chkTVShowActorThumbsFrodo.Enabled = false
        Me.chkTVShowActorThumbsFrodo.Location = New System.Drawing.Point(6, 90)
        Me.chkTVShowActorThumbsFrodo.Name = "chkTVShowActorThumbsFrodo"
        Me.chkTVShowActorThumbsFrodo.Size = New System.Drawing.Size(128, 17)
        Me.chkTVShowActorThumbsFrodo.TabIndex = 4
        Me.chkTVShowActorThumbsFrodo.Text = "Show Actor Thumbs"
        Me.chkTVShowActorThumbsFrodo.UseVisualStyleBackColor = true
        '
        'chkTVSeasonFanartFrodo
        '
        Me.chkTVSeasonFanartFrodo.AutoSize = true
        Me.chkTVSeasonFanartFrodo.Enabled = false
        Me.chkTVSeasonFanartFrodo.Location = New System.Drawing.Point(6, 159)
        Me.chkTVSeasonFanartFrodo.Name = "chkTVSeasonFanartFrodo"
        Me.chkTVSeasonFanartFrodo.Size = New System.Drawing.Size(99, 17)
        Me.chkTVSeasonFanartFrodo.TabIndex = 3
        Me.chkTVSeasonFanartFrodo.Text = "Season Fanart"
        Me.chkTVSeasonFanartFrodo.UseVisualStyleBackColor = true
        '
        'chkTVShowFanartFrodo
        '
        Me.chkTVShowFanartFrodo.AutoSize = true
        Me.chkTVShowFanartFrodo.Enabled = false
        Me.chkTVShowFanartFrodo.Location = New System.Drawing.Point(6, 67)
        Me.chkTVShowFanartFrodo.Name = "chkTVShowFanartFrodo"
        Me.chkTVShowFanartFrodo.Size = New System.Drawing.Size(91, 17)
        Me.chkTVShowFanartFrodo.TabIndex = 2
        Me.chkTVShowFanartFrodo.Text = "Show Fanart"
        Me.chkTVShowFanartFrodo.UseVisualStyleBackColor = true
        '
        'chkTVShowPosterFrodo
        '
        Me.chkTVShowPosterFrodo.AutoSize = true
        Me.chkTVShowPosterFrodo.Enabled = false
        Me.chkTVShowPosterFrodo.Location = New System.Drawing.Point(6, 44)
        Me.chkTVShowPosterFrodo.Name = "chkTVShowPosterFrodo"
        Me.chkTVShowPosterFrodo.Size = New System.Drawing.Size(90, 17)
        Me.chkTVShowPosterFrodo.TabIndex = 1
        Me.chkTVShowPosterFrodo.Text = "Show Poster"
        Me.chkTVShowPosterFrodo.UseVisualStyleBackColor = true
        '
        'tpTVFileNamingNMT
        '
        Me.tpTVFileNamingNMT.Controls.Add(Me.gbTVNMT)
        Me.tpTVFileNamingNMT.Controls.Add(Me.gbTVYAMJ)
        Me.tpTVFileNamingNMT.Location = New System.Drawing.Point(4, 22)
        Me.tpTVFileNamingNMT.Name = "tpTVFileNamingNMT"
        Me.tpTVFileNamingNMT.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTVFileNamingNMT.Size = New System.Drawing.Size(547, 301)
        Me.tpTVFileNamingNMT.TabIndex = 1
        Me.tpTVFileNamingNMT.Text = "NMT"
        Me.tpTVFileNamingNMT.UseVisualStyleBackColor = true
        '
        'gbTVNMT
        '
        Me.gbTVNMT.Controls.Add(Me.chkTVSeasonPosterNMJ)
        Me.gbTVNMT.Controls.Add(Me.chkTVShowBannerNMJ)
        Me.gbTVNMT.Controls.Add(Me.chkTVSeasonBannerNMJ)
        Me.gbTVNMT.Controls.Add(Me.chkTVEpisodePosterNMJ)
        Me.gbTVNMT.Controls.Add(Me.chkTVSeasonFanartNMJ)
        Me.gbTVNMT.Controls.Add(Me.chkTVShowFanartNMJ)
        Me.gbTVNMT.Controls.Add(Me.chkTVShowPosterNMJ)
        Me.gbTVNMT.Controls.Add(Me.chkTVUseNMJ)
        Me.gbTVNMT.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbTVNMT.Location = New System.Drawing.Point(164, 9)
        Me.gbTVNMT.Name = "gbTVNMT"
        Me.gbTVNMT.Size = New System.Drawing.Size(152, 289)
        Me.gbTVNMT.TabIndex = 19
        Me.gbTVNMT.TabStop = false
        Me.gbTVNMT.Text = "NMJ"
        '
        'chkTVSeasonPosterNMJ
        '
        Me.chkTVSeasonPosterNMJ.AutoSize = true
        Me.chkTVSeasonPosterNMJ.Enabled = false
        Me.chkTVSeasonPosterNMJ.Location = New System.Drawing.Point(6, 136)
        Me.chkTVSeasonPosterNMJ.Name = "chkTVSeasonPosterNMJ"
        Me.chkTVSeasonPosterNMJ.Size = New System.Drawing.Size(98, 17)
        Me.chkTVSeasonPosterNMJ.TabIndex = 26
        Me.chkTVSeasonPosterNMJ.Text = "Season Poster"
        Me.chkTVSeasonPosterNMJ.UseVisualStyleBackColor = true
        '
        'chkTVShowBannerNMJ
        '
        Me.chkTVShowBannerNMJ.AutoSize = true
        Me.chkTVShowBannerNMJ.Enabled = false
        Me.chkTVShowBannerNMJ.Location = New System.Drawing.Point(6, 113)
        Me.chkTVShowBannerNMJ.Name = "chkTVShowBannerNMJ"
        Me.chkTVShowBannerNMJ.Size = New System.Drawing.Size(95, 17)
        Me.chkTVShowBannerNMJ.TabIndex = 25
        Me.chkTVShowBannerNMJ.Text = "Show Banner"
        Me.chkTVShowBannerNMJ.UseVisualStyleBackColor = true
        '
        'chkTVSeasonBannerNMJ
        '
        Me.chkTVSeasonBannerNMJ.AutoSize = true
        Me.chkTVSeasonBannerNMJ.Enabled = false
        Me.chkTVSeasonBannerNMJ.Location = New System.Drawing.Point(6, 182)
        Me.chkTVSeasonBannerNMJ.Name = "chkTVSeasonBannerNMJ"
        Me.chkTVSeasonBannerNMJ.Size = New System.Drawing.Size(103, 17)
        Me.chkTVSeasonBannerNMJ.TabIndex = 23
        Me.chkTVSeasonBannerNMJ.Text = "Season Banner"
        Me.chkTVSeasonBannerNMJ.UseVisualStyleBackColor = true
        '
        'chkTVEpisodePosterNMJ
        '
        Me.chkTVEpisodePosterNMJ.AutoSize = true
        Me.chkTVEpisodePosterNMJ.Enabled = false
        Me.chkTVEpisodePosterNMJ.Location = New System.Drawing.Point(6, 205)
        Me.chkTVEpisodePosterNMJ.Name = "chkTVEpisodePosterNMJ"
        Me.chkTVEpisodePosterNMJ.Size = New System.Drawing.Size(102, 17)
        Me.chkTVEpisodePosterNMJ.TabIndex = 21
        Me.chkTVEpisodePosterNMJ.Text = "Episode Poster"
        Me.chkTVEpisodePosterNMJ.UseVisualStyleBackColor = true
        '
        'chkTVSeasonFanartNMJ
        '
        Me.chkTVSeasonFanartNMJ.AutoSize = true
        Me.chkTVSeasonFanartNMJ.Enabled = false
        Me.chkTVSeasonFanartNMJ.Location = New System.Drawing.Point(6, 159)
        Me.chkTVSeasonFanartNMJ.Name = "chkTVSeasonFanartNMJ"
        Me.chkTVSeasonFanartNMJ.Size = New System.Drawing.Size(99, 17)
        Me.chkTVSeasonFanartNMJ.TabIndex = 19
        Me.chkTVSeasonFanartNMJ.Text = "Season Fanart"
        Me.chkTVSeasonFanartNMJ.UseVisualStyleBackColor = true
        '
        'chkTVShowFanartNMJ
        '
        Me.chkTVShowFanartNMJ.AutoSize = true
        Me.chkTVShowFanartNMJ.Enabled = false
        Me.chkTVShowFanartNMJ.Location = New System.Drawing.Point(6, 67)
        Me.chkTVShowFanartNMJ.Name = "chkTVShowFanartNMJ"
        Me.chkTVShowFanartNMJ.Size = New System.Drawing.Size(91, 17)
        Me.chkTVShowFanartNMJ.TabIndex = 18
        Me.chkTVShowFanartNMJ.Text = "Show Fanart"
        Me.chkTVShowFanartNMJ.UseVisualStyleBackColor = true
        '
        'chkTVShowPosterNMJ
        '
        Me.chkTVShowPosterNMJ.AutoSize = true
        Me.chkTVShowPosterNMJ.Enabled = false
        Me.chkTVShowPosterNMJ.Location = New System.Drawing.Point(6, 44)
        Me.chkTVShowPosterNMJ.Name = "chkTVShowPosterNMJ"
        Me.chkTVShowPosterNMJ.Size = New System.Drawing.Size(90, 17)
        Me.chkTVShowPosterNMJ.TabIndex = 17
        Me.chkTVShowPosterNMJ.Text = "Show Poster"
        Me.chkTVShowPosterNMJ.UseVisualStyleBackColor = true
        '
        'chkTVUseNMJ
        '
        Me.chkTVUseNMJ.AutoSize = true
        Me.chkTVUseNMJ.Location = New System.Drawing.Point(6, 21)
        Me.chkTVUseNMJ.Name = "chkTVUseNMJ"
        Me.chkTVUseNMJ.Size = New System.Drawing.Size(45, 17)
        Me.chkTVUseNMJ.TabIndex = 16
        Me.chkTVUseNMJ.Text = "Use"
        Me.chkTVUseNMJ.UseVisualStyleBackColor = true
        '
        'gbTVYAMJ
        '
        Me.gbTVYAMJ.Controls.Add(Me.chkTVSeasonPosterYAMJ)
        Me.gbTVYAMJ.Controls.Add(Me.chkTVShowBannerYAMJ)
        Me.gbTVYAMJ.Controls.Add(Me.chkTVSeasonBannerYAMJ)
        Me.gbTVYAMJ.Controls.Add(Me.chkTVEpisodePosterYAMJ)
        Me.gbTVYAMJ.Controls.Add(Me.chkTVSeasonFanartYAMJ)
        Me.gbTVYAMJ.Controls.Add(Me.chkTVShowFanartYAMJ)
        Me.gbTVYAMJ.Controls.Add(Me.chkTVShowPosterYAMJ)
        Me.gbTVYAMJ.Controls.Add(Me.chkTVUseYAMJ)
        Me.gbTVYAMJ.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbTVYAMJ.Location = New System.Drawing.Point(6, 6)
        Me.gbTVYAMJ.Name = "gbTVYAMJ"
        Me.gbTVYAMJ.Size = New System.Drawing.Size(152, 289)
        Me.gbTVYAMJ.TabIndex = 18
        Me.gbTVYAMJ.TabStop = false
        Me.gbTVYAMJ.Text = "YAMJ"
        '
        'chkTVSeasonPosterYAMJ
        '
        Me.chkTVSeasonPosterYAMJ.AutoSize = true
        Me.chkTVSeasonPosterYAMJ.Enabled = false
        Me.chkTVSeasonPosterYAMJ.Location = New System.Drawing.Point(6, 136)
        Me.chkTVSeasonPosterYAMJ.Name = "chkTVSeasonPosterYAMJ"
        Me.chkTVSeasonPosterYAMJ.Size = New System.Drawing.Size(98, 17)
        Me.chkTVSeasonPosterYAMJ.TabIndex = 26
        Me.chkTVSeasonPosterYAMJ.Text = "Season Poster"
        Me.chkTVSeasonPosterYAMJ.UseVisualStyleBackColor = true
        '
        'chkTVShowBannerYAMJ
        '
        Me.chkTVShowBannerYAMJ.AutoSize = true
        Me.chkTVShowBannerYAMJ.Enabled = false
        Me.chkTVShowBannerYAMJ.Location = New System.Drawing.Point(6, 113)
        Me.chkTVShowBannerYAMJ.Name = "chkTVShowBannerYAMJ"
        Me.chkTVShowBannerYAMJ.Size = New System.Drawing.Size(95, 17)
        Me.chkTVShowBannerYAMJ.TabIndex = 25
        Me.chkTVShowBannerYAMJ.Text = "Show Banner"
        Me.chkTVShowBannerYAMJ.UseVisualStyleBackColor = true
        '
        'chkTVSeasonBannerYAMJ
        '
        Me.chkTVSeasonBannerYAMJ.AutoSize = true
        Me.chkTVSeasonBannerYAMJ.Enabled = false
        Me.chkTVSeasonBannerYAMJ.Location = New System.Drawing.Point(6, 182)
        Me.chkTVSeasonBannerYAMJ.Name = "chkTVSeasonBannerYAMJ"
        Me.chkTVSeasonBannerYAMJ.Size = New System.Drawing.Size(103, 17)
        Me.chkTVSeasonBannerYAMJ.TabIndex = 23
        Me.chkTVSeasonBannerYAMJ.Text = "Season Banner"
        Me.chkTVSeasonBannerYAMJ.UseVisualStyleBackColor = true
        '
        'chkTVEpisodePosterYAMJ
        '
        Me.chkTVEpisodePosterYAMJ.AutoSize = true
        Me.chkTVEpisodePosterYAMJ.Enabled = false
        Me.chkTVEpisodePosterYAMJ.Location = New System.Drawing.Point(6, 205)
        Me.chkTVEpisodePosterYAMJ.Name = "chkTVEpisodePosterYAMJ"
        Me.chkTVEpisodePosterYAMJ.Size = New System.Drawing.Size(102, 17)
        Me.chkTVEpisodePosterYAMJ.TabIndex = 21
        Me.chkTVEpisodePosterYAMJ.Text = "Episode Poster"
        Me.chkTVEpisodePosterYAMJ.UseVisualStyleBackColor = true
        '
        'chkTVSeasonFanartYAMJ
        '
        Me.chkTVSeasonFanartYAMJ.AutoSize = true
        Me.chkTVSeasonFanartYAMJ.Enabled = false
        Me.chkTVSeasonFanartYAMJ.Location = New System.Drawing.Point(6, 159)
        Me.chkTVSeasonFanartYAMJ.Name = "chkTVSeasonFanartYAMJ"
        Me.chkTVSeasonFanartYAMJ.Size = New System.Drawing.Size(99, 17)
        Me.chkTVSeasonFanartYAMJ.TabIndex = 19
        Me.chkTVSeasonFanartYAMJ.Text = "Season Fanart"
        Me.chkTVSeasonFanartYAMJ.UseVisualStyleBackColor = true
        '
        'chkTVShowFanartYAMJ
        '
        Me.chkTVShowFanartYAMJ.AutoSize = true
        Me.chkTVShowFanartYAMJ.Enabled = false
        Me.chkTVShowFanartYAMJ.Location = New System.Drawing.Point(6, 67)
        Me.chkTVShowFanartYAMJ.Name = "chkTVShowFanartYAMJ"
        Me.chkTVShowFanartYAMJ.Size = New System.Drawing.Size(91, 17)
        Me.chkTVShowFanartYAMJ.TabIndex = 18
        Me.chkTVShowFanartYAMJ.Text = "Show Fanart"
        Me.chkTVShowFanartYAMJ.UseVisualStyleBackColor = true
        '
        'chkTVShowPosterYAMJ
        '
        Me.chkTVShowPosterYAMJ.AutoSize = true
        Me.chkTVShowPosterYAMJ.Enabled = false
        Me.chkTVShowPosterYAMJ.Location = New System.Drawing.Point(6, 44)
        Me.chkTVShowPosterYAMJ.Name = "chkTVShowPosterYAMJ"
        Me.chkTVShowPosterYAMJ.Size = New System.Drawing.Size(90, 17)
        Me.chkTVShowPosterYAMJ.TabIndex = 17
        Me.chkTVShowPosterYAMJ.Text = "Show Poster"
        Me.chkTVShowPosterYAMJ.UseVisualStyleBackColor = true
        '
        'chkTVUseYAMJ
        '
        Me.chkTVUseYAMJ.AutoSize = true
        Me.chkTVUseYAMJ.Location = New System.Drawing.Point(6, 21)
        Me.chkTVUseYAMJ.Name = "chkTVUseYAMJ"
        Me.chkTVUseYAMJ.Size = New System.Drawing.Size(45, 17)
        Me.chkTVUseYAMJ.TabIndex = 16
        Me.chkTVUseYAMJ.Text = "Use"
        Me.chkTVUseYAMJ.UseVisualStyleBackColor = true
        '
        'tpTVFileNamingBoxee
        '
        Me.tpTVFileNamingBoxee.Controls.Add(Me.GroupBox1)
        Me.tpTVFileNamingBoxee.Location = New System.Drawing.Point(4, 22)
        Me.tpTVFileNamingBoxee.Name = "tpTVFileNamingBoxee"
        Me.tpTVFileNamingBoxee.Size = New System.Drawing.Size(547, 301)
        Me.tpTVFileNamingBoxee.TabIndex = 3
        Me.tpTVFileNamingBoxee.Text = "Boxee"
        Me.tpTVFileNamingBoxee.UseVisualStyleBackColor = true
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkTVSeasonPosterBoxee)
        Me.GroupBox1.Controls.Add(Me.chkTVShowBannerBoxee)
        Me.GroupBox1.Controls.Add(Me.chkTVEpisodePosterBoxee)
        Me.GroupBox1.Controls.Add(Me.chkTVShowFanartBoxee)
        Me.GroupBox1.Controls.Add(Me.chkTVShowPosterBoxee)
        Me.GroupBox1.Controls.Add(Me.chkTVUseBoxee)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(152, 289)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Boxee"
        '
        'chkTVSeasonPosterBoxee
        '
        Me.chkTVSeasonPosterBoxee.AutoSize = true
        Me.chkTVSeasonPosterBoxee.Enabled = false
        Me.chkTVSeasonPosterBoxee.Location = New System.Drawing.Point(6, 113)
        Me.chkTVSeasonPosterBoxee.Name = "chkTVSeasonPosterBoxee"
        Me.chkTVSeasonPosterBoxee.Size = New System.Drawing.Size(98, 17)
        Me.chkTVSeasonPosterBoxee.TabIndex = 26
        Me.chkTVSeasonPosterBoxee.Text = "Season Poster"
        Me.chkTVSeasonPosterBoxee.UseVisualStyleBackColor = true
        '
        'chkTVShowBannerBoxee
        '
        Me.chkTVShowBannerBoxee.AutoSize = true
        Me.chkTVShowBannerBoxee.Enabled = false
        Me.chkTVShowBannerBoxee.Location = New System.Drawing.Point(6, 90)
        Me.chkTVShowBannerBoxee.Name = "chkTVShowBannerBoxee"
        Me.chkTVShowBannerBoxee.Size = New System.Drawing.Size(95, 17)
        Me.chkTVShowBannerBoxee.TabIndex = 25
        Me.chkTVShowBannerBoxee.Text = "Show Banner"
        Me.chkTVShowBannerBoxee.UseVisualStyleBackColor = true
        '
        'chkTVEpisodePosterBoxee
        '
        Me.chkTVEpisodePosterBoxee.AutoSize = true
        Me.chkTVEpisodePosterBoxee.Enabled = false
        Me.chkTVEpisodePosterBoxee.Location = New System.Drawing.Point(6, 136)
        Me.chkTVEpisodePosterBoxee.Name = "chkTVEpisodePosterBoxee"
        Me.chkTVEpisodePosterBoxee.Size = New System.Drawing.Size(102, 17)
        Me.chkTVEpisodePosterBoxee.TabIndex = 21
        Me.chkTVEpisodePosterBoxee.Text = "Episode Poster"
        Me.chkTVEpisodePosterBoxee.UseVisualStyleBackColor = true
        '
        'chkTVShowFanartBoxee
        '
        Me.chkTVShowFanartBoxee.AutoSize = true
        Me.chkTVShowFanartBoxee.Enabled = false
        Me.chkTVShowFanartBoxee.Location = New System.Drawing.Point(6, 67)
        Me.chkTVShowFanartBoxee.Name = "chkTVShowFanartBoxee"
        Me.chkTVShowFanartBoxee.Size = New System.Drawing.Size(91, 17)
        Me.chkTVShowFanartBoxee.TabIndex = 18
        Me.chkTVShowFanartBoxee.Text = "Show Fanart"
        Me.chkTVShowFanartBoxee.UseVisualStyleBackColor = true
        '
        'chkTVShowPosterBoxee
        '
        Me.chkTVShowPosterBoxee.AutoSize = true
        Me.chkTVShowPosterBoxee.Enabled = false
        Me.chkTVShowPosterBoxee.Location = New System.Drawing.Point(6, 44)
        Me.chkTVShowPosterBoxee.Name = "chkTVShowPosterBoxee"
        Me.chkTVShowPosterBoxee.Size = New System.Drawing.Size(90, 17)
        Me.chkTVShowPosterBoxee.TabIndex = 17
        Me.chkTVShowPosterBoxee.Text = "Show Poster"
        Me.chkTVShowPosterBoxee.UseVisualStyleBackColor = true
        '
        'chkTVUseBoxee
        '
        Me.chkTVUseBoxee.AutoSize = true
        Me.chkTVUseBoxee.Location = New System.Drawing.Point(6, 21)
        Me.chkTVUseBoxee.Name = "chkTVUseBoxee"
        Me.chkTVUseBoxee.Size = New System.Drawing.Size(45, 17)
        Me.chkTVUseBoxee.TabIndex = 16
        Me.chkTVUseBoxee.Text = "Use"
        Me.chkTVUseBoxee.UseVisualStyleBackColor = true
        '
        'tpTVFileNamingExpert
        '
        Me.tpTVFileNamingExpert.Location = New System.Drawing.Point(4, 22)
        Me.tpTVFileNamingExpert.Name = "tpTVFileNamingExpert"
        Me.tpTVFileNamingExpert.Size = New System.Drawing.Size(547, 301)
        Me.tpTVFileNamingExpert.TabIndex = 2
        Me.tpTVFileNamingExpert.Text = "Expert"
        Me.tpTVFileNamingExpert.UseVisualStyleBackColor = true
        '
        'lvTVSources
        '
        Me.lvTVSources.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6})
        Me.lvTVSources.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lvTVSources.FullRowSelect = true
        Me.lvTVSources.HideSelection = false
        Me.lvTVSources.Location = New System.Drawing.Point(6, 4)
        Me.lvTVSources.Name = "lvTVSources"
        Me.lvTVSources.Size = New System.Drawing.Size(614, 105)
        Me.lvTVSources.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvTVSources.TabIndex = 0
        Me.lvTVSources.UseCompatibleStateImageBehavior = false
        Me.lvTVSources.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Width = 0
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Name"
        Me.ColumnHeader2.Width = 94
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Path"
        Me.ColumnHeader3.Width = 308
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Language"
        Me.ColumnHeader4.Width = 80
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Ordering"
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Exclude"
        '
        'gbTVSourcesMiscOpts
        '
        Me.gbTVSourcesMiscOpts.Controls.Add(Me.lblTVSkipLessThanMB)
        Me.gbTVSourcesMiscOpts.Controls.Add(Me.txtTVSkipLessThan)
        Me.gbTVSourcesMiscOpts.Controls.Add(Me.lblTVSkipLessThan)
        Me.gbTVSourcesMiscOpts.Controls.Add(Me.chkTVScanOrderModify)
        Me.gbTVSourcesMiscOpts.Controls.Add(Me.chkTVGeneralIgnoreLastScan)
        Me.gbTVSourcesMiscOpts.Controls.Add(Me.chkTVCleanDB)
        Me.gbTVSourcesMiscOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVSourcesMiscOpts.Location = New System.Drawing.Point(6, 110)
        Me.gbTVSourcesMiscOpts.Name = "gbTVSourcesMiscOpts"
        Me.gbTVSourcesMiscOpts.Size = New System.Drawing.Size(151, 188)
        Me.gbTVSourcesMiscOpts.TabIndex = 4
        Me.gbTVSourcesMiscOpts.TabStop = false
        Me.gbTVSourcesMiscOpts.Text = "Miscellaneous Options"
        '
        'lblTVSkipLessThanMB
        '
        Me.lblTVSkipLessThanMB.AutoSize = true
        Me.lblTVSkipLessThanMB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVSkipLessThanMB.Location = New System.Drawing.Point(114, 39)
        Me.lblTVSkipLessThanMB.Name = "lblTVSkipLessThanMB"
        Me.lblTVSkipLessThanMB.Size = New System.Drawing.Size(24, 13)
        Me.lblTVSkipLessThanMB.TabIndex = 2
        Me.lblTVSkipLessThanMB.Text = "MB"
        '
        'txtTVSkipLessThan
        '
        Me.txtTVSkipLessThan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVSkipLessThan.Location = New System.Drawing.Point(11, 34)
        Me.txtTVSkipLessThan.Name = "txtTVSkipLessThan"
        Me.txtTVSkipLessThan.Size = New System.Drawing.Size(100, 22)
        Me.txtTVSkipLessThan.TabIndex = 0
        '
        'lblTVSkipLessThan
        '
        Me.lblTVSkipLessThan.AutoSize = true
        Me.lblTVSkipLessThan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVSkipLessThan.Location = New System.Drawing.Point(6, 18)
        Me.lblTVSkipLessThan.Name = "lblTVSkipLessThan"
        Me.lblTVSkipLessThan.Size = New System.Drawing.Size(122, 13)
        Me.lblTVSkipLessThan.TabIndex = 1
        Me.lblTVSkipLessThan.Text = "Skip files smaller than:"
        '
        'chkTVScanOrderModify
        '
        Me.chkTVScanOrderModify.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVScanOrderModify.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScanOrderModify.Location = New System.Drawing.Point(4, 110)
        Me.chkTVScanOrderModify.Name = "chkTVScanOrderModify"
        Me.chkTVScanOrderModify.Size = New System.Drawing.Size(142, 43)
        Me.chkTVScanOrderModify.TabIndex = 4
        Me.chkTVScanOrderModify.Text = "Scan in order of last write time"
        Me.chkTVScanOrderModify.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVScanOrderModify.UseVisualStyleBackColor = true
        '
        'chkTVGeneralIgnoreLastScan
        '
        Me.chkTVGeneralIgnoreLastScan.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVGeneralIgnoreLastScan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVGeneralIgnoreLastScan.Location = New System.Drawing.Point(4, 68)
        Me.chkTVGeneralIgnoreLastScan.Name = "chkTVGeneralIgnoreLastScan"
        Me.chkTVGeneralIgnoreLastScan.Size = New System.Drawing.Size(142, 43)
        Me.chkTVGeneralIgnoreLastScan.TabIndex = 3
        Me.chkTVGeneralIgnoreLastScan.Text = "Ignore last scan time when updating library"
        Me.chkTVGeneralIgnoreLastScan.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVGeneralIgnoreLastScan.UseVisualStyleBackColor = true
        '
        'chkTVCleanDB
        '
        Me.chkTVCleanDB.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVCleanDB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVCleanDB.Location = New System.Drawing.Point(4, 152)
        Me.chkTVCleanDB.Name = "chkTVCleanDB"
        Me.chkTVCleanDB.Size = New System.Drawing.Size(142, 43)
        Me.chkTVCleanDB.TabIndex = 5
        Me.chkTVCleanDB.Text = "Clean database after updating library"
        Me.chkTVCleanDB.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVCleanDB.UseVisualStyleBackColor = true
        '
        'btnTVSourceAdd
        '
        Me.btnTVSourceAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnTVSourceAdd.Image = CType(resources.GetObject("btnTVSourceAdd.Image"),System.Drawing.Image)
        Me.btnTVSourceAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVSourceAdd.Location = New System.Drawing.Point(626, 4)
        Me.btnTVSourceAdd.Name = "btnTVSourceAdd"
        Me.btnTVSourceAdd.Size = New System.Drawing.Size(104, 23)
        Me.btnTVSourceAdd.TabIndex = 1
        Me.btnTVSourceAdd.Text = "Add Source"
        Me.btnTVSourceAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVSourceAdd.UseVisualStyleBackColor = true
        '
        'btnTVSourceEdit
        '
        Me.btnTVSourceEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnTVSourceEdit.Image = CType(resources.GetObject("btnTVSourceEdit.Image"),System.Drawing.Image)
        Me.btnTVSourceEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVSourceEdit.Location = New System.Drawing.Point(626, 34)
        Me.btnTVSourceEdit.Name = "btnTVSourceEdit"
        Me.btnTVSourceEdit.Size = New System.Drawing.Size(104, 23)
        Me.btnTVSourceEdit.TabIndex = 2
        Me.btnTVSourceEdit.Text = "Edit Source"
        Me.btnTVSourceEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVSourceEdit.UseVisualStyleBackColor = true
        '
        'btnRemTVSource
        '
        Me.btnRemTVSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnRemTVSource.Image = CType(resources.GetObject("btnRemTVSource.Image"),System.Drawing.Image)
        Me.btnRemTVSource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemTVSource.Location = New System.Drawing.Point(626, 86)
        Me.btnRemTVSource.Name = "btnRemTVSource"
        Me.btnRemTVSource.Size = New System.Drawing.Size(104, 23)
        Me.btnRemTVSource.TabIndex = 3
        Me.btnRemTVSource.Text = "Remove"
        Me.btnRemTVSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemTVSource.UseVisualStyleBackColor = true
        '
        'tpTVSourcesRegex
        '
        Me.tpTVSourcesRegex.Controls.Add(Me.btnTVShowRegexGet)
        Me.tpTVSourcesRegex.Controls.Add(Me.btnTVShowRegexDown)
        Me.tpTVSourcesRegex.Controls.Add(Me.btnTVShowRegexUp)
        Me.tpTVSourcesRegex.Controls.Add(Me.btnTVShowRegexReset)
        Me.tpTVSourcesRegex.Controls.Add(Me.gbTVShowRegex)
        Me.tpTVSourcesRegex.Controls.Add(Me.btnTVShowRegexEdit)
        Me.tpTVSourcesRegex.Controls.Add(Me.btnTVShowRegexRemove)
        Me.tpTVSourcesRegex.Controls.Add(Me.lvTVShowRegex)
        Me.tpTVSourcesRegex.Location = New System.Drawing.Point(4, 22)
        Me.tpTVSourcesRegex.Name = "tpTVSourcesRegex"
        Me.tpTVSourcesRegex.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTVSourcesRegex.Size = New System.Drawing.Size(736, 467)
        Me.tpTVSourcesRegex.TabIndex = 1
        Me.tpTVSourcesRegex.Text = "Regex"
        Me.tpTVSourcesRegex.UseVisualStyleBackColor = true
        '
        'btnTVShowRegexGet
        '
        Me.btnTVShowRegexGet.Image = CType(resources.GetObject("btnTVShowRegexGet.Image"),System.Drawing.Image)
        Me.btnTVShowRegexGet.Location = New System.Drawing.Point(550, 3)
        Me.btnTVShowRegexGet.Name = "btnTVShowRegexGet"
        Me.btnTVShowRegexGet.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowRegexGet.TabIndex = 1
        Me.btnTVShowRegexGet.UseVisualStyleBackColor = true
        '
        'btnTVShowRegexDown
        '
        Me.btnTVShowRegexDown.Image = CType(resources.GetObject("btnTVShowRegexDown.Image"),System.Drawing.Image)
        Me.btnTVShowRegexDown.Location = New System.Drawing.Point(304, 167)
        Me.btnTVShowRegexDown.Name = "btnTVShowRegexDown"
        Me.btnTVShowRegexDown.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowRegexDown.TabIndex = 5
        Me.btnTVShowRegexDown.UseVisualStyleBackColor = true
        '
        'btnTVShowRegexUp
        '
        Me.btnTVShowRegexUp.Image = CType(resources.GetObject("btnTVShowRegexUp.Image"),System.Drawing.Image)
        Me.btnTVShowRegexUp.Location = New System.Drawing.Point(280, 167)
        Me.btnTVShowRegexUp.Name = "btnTVShowRegexUp"
        Me.btnTVShowRegexUp.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowRegexUp.TabIndex = 4
        Me.btnTVShowRegexUp.UseVisualStyleBackColor = true
        '
        'btnTVShowRegexReset
        '
        Me.btnTVShowRegexReset.Image = CType(resources.GetObject("btnTVShowRegexReset.Image"),System.Drawing.Image)
        Me.btnTVShowRegexReset.Location = New System.Drawing.Point(576, 3)
        Me.btnTVShowRegexReset.Name = "btnTVShowRegexReset"
        Me.btnTVShowRegexReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowRegexReset.TabIndex = 2
        Me.btnTVShowRegexReset.UseVisualStyleBackColor = true
        '
        'gbTVShowRegex
        '
        Me.gbTVShowRegex.Controls.Add(Me.btnTVShowRegexClear)
        Me.gbTVShowRegex.Controls.Add(Me.lblTVSeasonMatch)
        Me.gbTVShowRegex.Controls.Add(Me.btnTVShowRegexAdd)
        Me.gbTVShowRegex.Controls.Add(Me.txtTVSeasonRegex)
        Me.gbTVShowRegex.Controls.Add(Me.lblTVEpisodeRetrieve)
        Me.gbTVShowRegex.Controls.Add(Me.cbTVSeasonRetrieve)
        Me.gbTVShowRegex.Controls.Add(Me.lblTVSeasonRetrieve)
        Me.gbTVShowRegex.Controls.Add(Me.txtTVEpisodeRegex)
        Me.gbTVShowRegex.Controls.Add(Me.lblTVEpisodeMatch)
        Me.gbTVShowRegex.Controls.Add(Me.cbTVEpisodeRetrieve)
        Me.gbTVShowRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbTVShowRegex.Location = New System.Drawing.Point(6, 196)
        Me.gbTVShowRegex.Name = "gbTVShowRegex"
        Me.gbTVShowRegex.Size = New System.Drawing.Size(592, 148)
        Me.gbTVShowRegex.TabIndex = 7
        Me.gbTVShowRegex.TabStop = false
        Me.gbTVShowRegex.Text = "Show Match Regex"
        '
        'btnTVShowRegexClear
        '
        Me.btnTVShowRegexClear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnTVShowRegexClear.Image = CType(resources.GetObject("btnTVShowRegexClear.Image"),System.Drawing.Image)
        Me.btnTVShowRegexClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVShowRegexClear.Location = New System.Drawing.Point(10, 116)
        Me.btnTVShowRegexClear.Name = "btnTVShowRegexClear"
        Me.btnTVShowRegexClear.Size = New System.Drawing.Size(104, 23)
        Me.btnTVShowRegexClear.TabIndex = 8
        Me.btnTVShowRegexClear.Text = "Clear"
        Me.btnTVShowRegexClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVShowRegexClear.UseVisualStyleBackColor = true
        '
        'lblTVSeasonMatch
        '
        Me.lblTVSeasonMatch.AutoSize = true
        Me.lblTVSeasonMatch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVSeasonMatch.Location = New System.Drawing.Point(8, 25)
        Me.lblTVSeasonMatch.Name = "lblTVSeasonMatch"
        Me.lblTVSeasonMatch.Size = New System.Drawing.Size(116, 13)
        Me.lblTVSeasonMatch.TabIndex = 0
        Me.lblTVSeasonMatch.Text = "Season Match Regex:"
        '
        'btnTVShowRegexAdd
        '
        Me.btnTVShowRegexAdd.Enabled = false
        Me.btnTVShowRegexAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnTVShowRegexAdd.Image = CType(resources.GetObject("btnTVShowRegexAdd.Image"),System.Drawing.Image)
        Me.btnTVShowRegexAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVShowRegexAdd.Location = New System.Drawing.Point(482, 117)
        Me.btnTVShowRegexAdd.Name = "btnTVShowRegexAdd"
        Me.btnTVShowRegexAdd.Size = New System.Drawing.Size(104, 23)
        Me.btnTVShowRegexAdd.TabIndex = 9
        Me.btnTVShowRegexAdd.Text = "Add Regex"
        Me.btnTVShowRegexAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVShowRegexAdd.UseVisualStyleBackColor = true
        '
        'txtTVSeasonRegex
        '
        Me.txtTVSeasonRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVSeasonRegex.Location = New System.Drawing.Point(10, 40)
        Me.txtTVSeasonRegex.Name = "txtTVSeasonRegex"
        Me.txtTVSeasonRegex.Size = New System.Drawing.Size(417, 22)
        Me.txtTVSeasonRegex.TabIndex = 1
        '
        'lblTVEpisodeRetrieve
        '
        Me.lblTVEpisodeRetrieve.AutoSize = true
        Me.lblTVEpisodeRetrieve.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVEpisodeRetrieve.Location = New System.Drawing.Point(441, 69)
        Me.lblTVEpisodeRetrieve.Name = "lblTVEpisodeRetrieve"
        Me.lblTVEpisodeRetrieve.Size = New System.Drawing.Size(54, 13)
        Me.lblTVEpisodeRetrieve.TabIndex = 6
        Me.lblTVEpisodeRetrieve.Text = "Apply To:"
        '
        'cbTVSeasonRetrieve
        '
        Me.cbTVSeasonRetrieve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVSeasonRetrieve.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbTVSeasonRetrieve.FormattingEnabled = true
        Me.cbTVSeasonRetrieve.Items.AddRange(New Object() {"Folder Name", "File Name"})
        Me.cbTVSeasonRetrieve.Location = New System.Drawing.Point(443, 40)
        Me.cbTVSeasonRetrieve.Name = "cbTVSeasonRetrieve"
        Me.cbTVSeasonRetrieve.Size = New System.Drawing.Size(135, 21)
        Me.cbTVSeasonRetrieve.TabIndex = 3
        '
        'lblTVSeasonRetrieve
        '
        Me.lblTVSeasonRetrieve.AutoSize = true
        Me.lblTVSeasonRetrieve.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVSeasonRetrieve.Location = New System.Drawing.Point(441, 25)
        Me.lblTVSeasonRetrieve.Name = "lblTVSeasonRetrieve"
        Me.lblTVSeasonRetrieve.Size = New System.Drawing.Size(54, 13)
        Me.lblTVSeasonRetrieve.TabIndex = 2
        Me.lblTVSeasonRetrieve.Text = "Apply To:"
        '
        'txtTVEpisodeRegex
        '
        Me.txtTVEpisodeRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVEpisodeRegex.Location = New System.Drawing.Point(10, 84)
        Me.txtTVEpisodeRegex.Name = "txtTVEpisodeRegex"
        Me.txtTVEpisodeRegex.Size = New System.Drawing.Size(417, 22)
        Me.txtTVEpisodeRegex.TabIndex = 5
        '
        'lblTVEpisodeMatch
        '
        Me.lblTVEpisodeMatch.AutoSize = true
        Me.lblTVEpisodeMatch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVEpisodeMatch.Location = New System.Drawing.Point(8, 69)
        Me.lblTVEpisodeMatch.Name = "lblTVEpisodeMatch"
        Me.lblTVEpisodeMatch.Size = New System.Drawing.Size(120, 13)
        Me.lblTVEpisodeMatch.TabIndex = 4
        Me.lblTVEpisodeMatch.Text = "Episode Match Regex:"
        '
        'cbTVEpisodeRetrieve
        '
        Me.cbTVEpisodeRetrieve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVEpisodeRetrieve.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cbTVEpisodeRetrieve.FormattingEnabled = true
        Me.cbTVEpisodeRetrieve.Items.AddRange(New Object() {"Folder Name", "File Name", "Season Result"})
        Me.cbTVEpisodeRetrieve.Location = New System.Drawing.Point(443, 84)
        Me.cbTVEpisodeRetrieve.Name = "cbTVEpisodeRetrieve"
        Me.cbTVEpisodeRetrieve.Size = New System.Drawing.Size(135, 21)
        Me.cbTVEpisodeRetrieve.TabIndex = 7
        '
        'btnTVShowRegexEdit
        '
        Me.btnTVShowRegexEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnTVShowRegexEdit.Image = CType(resources.GetObject("btnTVShowRegexEdit.Image"),System.Drawing.Image)
        Me.btnTVShowRegexEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVShowRegexEdit.Location = New System.Drawing.Point(1, 167)
        Me.btnTVShowRegexEdit.Name = "btnTVShowRegexEdit"
        Me.btnTVShowRegexEdit.Size = New System.Drawing.Size(104, 23)
        Me.btnTVShowRegexEdit.TabIndex = 3
        Me.btnTVShowRegexEdit.Text = "Edit Regex"
        Me.btnTVShowRegexEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVShowRegexEdit.UseVisualStyleBackColor = true
        '
        'btnTVShowRegexRemove
        '
        Me.btnTVShowRegexRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnTVShowRegexRemove.Image = CType(resources.GetObject("btnTVShowRegexRemove.Image"),System.Drawing.Image)
        Me.btnTVShowRegexRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVShowRegexRemove.Location = New System.Drawing.Point(494, 167)
        Me.btnTVShowRegexRemove.Name = "btnTVShowRegexRemove"
        Me.btnTVShowRegexRemove.Size = New System.Drawing.Size(104, 23)
        Me.btnTVShowRegexRemove.TabIndex = 6
        Me.btnTVShowRegexRemove.Text = "Remove"
        Me.btnTVShowRegexRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVShowRegexRemove.UseVisualStyleBackColor = true
        '
        'lvTVShowRegex
        '
        Me.lvTVShowRegex.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTVShowRegexID, Me.colTVShowRegexSeason, Me.colTVShowRegexSeasonApply, Me.colTVShowRegexEpisode, Me.colTVShowRegexEpisodeApply})
        Me.lvTVShowRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lvTVShowRegex.FullRowSelect = true
        Me.lvTVShowRegex.HideSelection = false
        Me.lvTVShowRegex.Location = New System.Drawing.Point(0, 28)
        Me.lvTVShowRegex.Name = "lvTVShowRegex"
        Me.lvTVShowRegex.Size = New System.Drawing.Size(598, 135)
        Me.lvTVShowRegex.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvTVShowRegex.TabIndex = 0
        Me.lvTVShowRegex.UseCompatibleStateImageBehavior = false
        Me.lvTVShowRegex.View = System.Windows.Forms.View.Details
        '
        'colTVShowRegexID
        '
        Me.colTVShowRegexID.DisplayIndex = 4
        Me.colTVShowRegexID.Width = 0
        '
        'colTVShowRegexSeason
        '
        Me.colTVShowRegexSeason.DisplayIndex = 0
        Me.colTVShowRegexSeason.Text = "Season Regex"
        Me.colTVShowRegexSeason.Width = 224
        '
        'colTVShowRegexSeasonApply
        '
        Me.colTVShowRegexSeasonApply.DisplayIndex = 1
        Me.colTVShowRegexSeasonApply.Text = "Apply To"
        Me.colTVShowRegexSeasonApply.Width = 70
        '
        'colTVShowRegexEpisode
        '
        Me.colTVShowRegexEpisode.DisplayIndex = 2
        Me.colTVShowRegexEpisode.Text = "Episode Regex"
        Me.colTVShowRegexEpisode.Width = 219
        '
        'colTVShowRegexEpisodeApply
        '
        Me.colTVShowRegexEpisodeApply.DisplayIndex = 3
        Me.colTVShowRegexEpisodeApply.Text = "Apply To"
        Me.colTVShowRegexEpisodeApply.Width = 70
        '
        'pnlTVImages
        '
        Me.pnlTVImages.BackColor = System.Drawing.Color.White
        Me.pnlTVImages.Controls.Add(Me.tcTVImages)
        Me.pnlTVImages.Location = New System.Drawing.Point(900, 900)
        Me.pnlTVImages.Name = "pnlTVImages"
        Me.pnlTVImages.Size = New System.Drawing.Size(750, 500)
        Me.pnlTVImages.TabIndex = 16
        Me.pnlTVImages.Visible = false
        '
        'tcTVImages
        '
        Me.tcTVImages.Controls.Add(Me.tpTVShow)
        Me.tcTVImages.Controls.Add(Me.tpTVAllSeasons)
        Me.tcTVImages.Controls.Add(Me.tpTVSeason)
        Me.tcTVImages.Controls.Add(Me.tpTVEpisode)
        Me.tcTVImages.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.tcTVImages.Location = New System.Drawing.Point(5, 6)
        Me.tcTVImages.Name = "tcTVImages"
        Me.tcTVImages.SelectedIndex = 0
        Me.tcTVImages.Size = New System.Drawing.Size(742, 491)
        Me.tcTVImages.TabIndex = 0
        '
        'tpTVShow
        '
        Me.tpTVShow.Controls.Add(Me.gbTVShowEFanartsOpts)
        Me.tpTVShow.Controls.Add(Me.gbTVShowCharacterArtOpts)
        Me.tpTVShow.Controls.Add(Me.gbTVShowClearArtOpts)
        Me.tpTVShow.Controls.Add(Me.gbTVShowClearLogoOpts)
        Me.tpTVShow.Controls.Add(Me.gbTVShowLandscapeOpts)
        Me.tpTVShow.Controls.Add(Me.gbTVShowBannerOpts)
        Me.tpTVShow.Controls.Add(Me.gbTVShowPosterOpts)
        Me.tpTVShow.Controls.Add(Me.gbTVShowFanartOpts)
        Me.tpTVShow.Location = New System.Drawing.Point(4, 22)
        Me.tpTVShow.Name = "tpTVShow"
        Me.tpTVShow.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTVShow.Size = New System.Drawing.Size(734, 465)
        Me.tpTVShow.TabIndex = 0
        Me.tpTVShow.Text = "TV Show"
        Me.tpTVShow.UseVisualStyleBackColor = true
        '
        'gbTVShowEFanartsOpts
        '
        Me.gbTVShowEFanartsOpts.Controls.Add(Me.lblTVShowEFanartsLimit)
        Me.gbTVShowEFanartsOpts.Controls.Add(Me.txtTVShowEFanartsLimit)
        Me.gbTVShowEFanartsOpts.Controls.Add(Me.chkTVShowEFanartsPrefOnly)
        Me.gbTVShowEFanartsOpts.Controls.Add(Me.txtTVShowEFanartsWidth)
        Me.gbTVShowEFanartsOpts.Controls.Add(Me.txtTVShowEFanartsHeight)
        Me.gbTVShowEFanartsOpts.Controls.Add(Me.lblTVShowEFanartsWidth)
        Me.gbTVShowEFanartsOpts.Controls.Add(Me.lblTVShowEFanartsHeight)
        Me.gbTVShowEFanartsOpts.Controls.Add(Me.chkTVShowEFanartsResize)
        Me.gbTVShowEFanartsOpts.Controls.Add(Me.lblTVShowEFanartsSize)
        Me.gbTVShowEFanartsOpts.Controls.Add(Me.cbTVShowEFanartsPrefSize)
        Me.gbTVShowEFanartsOpts.Controls.Add(Me.chkTVShowEFanartsOverwrite)
        Me.gbTVShowEFanartsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVShowEFanartsOpts.Location = New System.Drawing.Point(474, 179)
        Me.gbTVShowEFanartsOpts.Name = "gbTVShowEFanartsOpts"
        Me.gbTVShowEFanartsOpts.Size = New System.Drawing.Size(228, 181)
        Me.gbTVShowEFanartsOpts.TabIndex = 14
        Me.gbTVShowEFanartsOpts.TabStop = false
        Me.gbTVShowEFanartsOpts.Text = "Extrafanarts"
        '
        'lblTVShowEFanartsLimit
        '
        Me.lblTVShowEFanartsLimit.AutoSize = true
        Me.lblTVShowEFanartsLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblTVShowEFanartsLimit.Location = New System.Drawing.Point(3, 25)
        Me.lblTVShowEFanartsLimit.Name = "lblTVShowEFanartsLimit"
        Me.lblTVShowEFanartsLimit.Size = New System.Drawing.Size(34, 13)
        Me.lblTVShowEFanartsLimit.TabIndex = 13
        Me.lblTVShowEFanartsLimit.Text = "Limit:"
        '
        'txtTVShowEFanartsLimit
        '
        Me.txtTVShowEFanartsLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVShowEFanartsLimit.Location = New System.Drawing.Point(43, 22)
        Me.txtTVShowEFanartsLimit.Name = "txtTVShowEFanartsLimit"
        Me.txtTVShowEFanartsLimit.Size = New System.Drawing.Size(40, 22)
        Me.txtTVShowEFanartsLimit.TabIndex = 12
        '
        'chkTVShowEFanartsPrefOnly
        '
        Me.chkTVShowEFanartsPrefOnly.AutoSize = true
        Me.chkTVShowEFanartsPrefOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowEFanartsPrefOnly.Location = New System.Drawing.Point(163, 86)
        Me.chkTVShowEFanartsPrefOnly.Name = "chkTVShowEFanartsPrefOnly"
        Me.chkTVShowEFanartsPrefOnly.Size = New System.Drawing.Size(50, 17)
        Me.chkTVShowEFanartsPrefOnly.TabIndex = 2
        Me.chkTVShowEFanartsPrefOnly.Text = "Only"
        Me.chkTVShowEFanartsPrefOnly.UseVisualStyleBackColor = true
        '
        'txtTVShowEFanartsWidth
        '
        Me.txtTVShowEFanartsWidth.Enabled = false
        Me.txtTVShowEFanartsWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVShowEFanartsWidth.Location = New System.Drawing.Point(68, 148)
        Me.txtTVShowEFanartsWidth.Name = "txtTVShowEFanartsWidth"
        Me.txtTVShowEFanartsWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtTVShowEFanartsWidth.TabIndex = 6
        '
        'txtTVShowEFanartsHeight
        '
        Me.txtTVShowEFanartsHeight.Enabled = false
        Me.txtTVShowEFanartsHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVShowEFanartsHeight.Location = New System.Drawing.Point(175, 148)
        Me.txtTVShowEFanartsHeight.Name = "txtTVShowEFanartsHeight"
        Me.txtTVShowEFanartsHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtTVShowEFanartsHeight.TabIndex = 8
        '
        'lblTVShowEFanartsWidth
        '
        Me.lblTVShowEFanartsWidth.AutoSize = true
        Me.lblTVShowEFanartsWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVShowEFanartsWidth.Location = New System.Drawing.Point(3, 152)
        Me.lblTVShowEFanartsWidth.Name = "lblTVShowEFanartsWidth"
        Me.lblTVShowEFanartsWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblTVShowEFanartsWidth.TabIndex = 5
        Me.lblTVShowEFanartsWidth.Text = "Max Width:"
        '
        'lblTVShowEFanartsHeight
        '
        Me.lblTVShowEFanartsHeight.AutoSize = true
        Me.lblTVShowEFanartsHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVShowEFanartsHeight.Location = New System.Drawing.Point(106, 152)
        Me.lblTVShowEFanartsHeight.Name = "lblTVShowEFanartsHeight"
        Me.lblTVShowEFanartsHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblTVShowEFanartsHeight.TabIndex = 7
        Me.lblTVShowEFanartsHeight.Text = "Max Height:"
        '
        'chkTVShowEFanartsResize
        '
        Me.chkTVShowEFanartsResize.AutoSize = true
        Me.chkTVShowEFanartsResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowEFanartsResize.Location = New System.Drawing.Point(6, 130)
        Me.chkTVShowEFanartsResize.Name = "chkTVShowEFanartsResize"
        Me.chkTVShowEFanartsResize.Size = New System.Drawing.Size(133, 17)
        Me.chkTVShowEFanartsResize.TabIndex = 4
        Me.chkTVShowEFanartsResize.Text = "Automatically Resize:"
        Me.chkTVShowEFanartsResize.UseVisualStyleBackColor = true
        '
        'lblTVShowEFanartsSize
        '
        Me.lblTVShowEFanartsSize.AutoSize = true
        Me.lblTVShowEFanartsSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVShowEFanartsSize.Location = New System.Drawing.Point(4, 66)
        Me.lblTVShowEFanartsSize.Name = "lblTVShowEFanartsSize"
        Me.lblTVShowEFanartsSize.Size = New System.Drawing.Size(80, 13)
        Me.lblTVShowEFanartsSize.TabIndex = 0
        Me.lblTVShowEFanartsSize.Text = "Preferred Size:"
        '
        'cbTVShowEFanartsPrefSize
        '
        Me.cbTVShowEFanartsPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVShowEFanartsPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVShowEFanartsPrefSize.FormattingEnabled = true
        Me.cbTVShowEFanartsPrefSize.Location = New System.Drawing.Point(6, 82)
        Me.cbTVShowEFanartsPrefSize.Name = "cbTVShowEFanartsPrefSize"
        Me.cbTVShowEFanartsPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbTVShowEFanartsPrefSize.TabIndex = 1
        '
        'chkTVShowEFanartsOverwrite
        '
        Me.chkTVShowEFanartsOverwrite.AutoSize = true
        Me.chkTVShowEFanartsOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowEFanartsOverwrite.Location = New System.Drawing.Point(6, 110)
        Me.chkTVShowEFanartsOverwrite.Name = "chkTVShowEFanartsOverwrite"
        Me.chkTVShowEFanartsOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVShowEFanartsOverwrite.TabIndex = 3
        Me.chkTVShowEFanartsOverwrite.Text = "Overwrite Existing"
        Me.chkTVShowEFanartsOverwrite.UseVisualStyleBackColor = true
        '
        'gbTVShowCharacterArtOpts
        '
        Me.gbTVShowCharacterArtOpts.Controls.Add(Me.chkTVShowCharacterArtOverwrite)
        Me.gbTVShowCharacterArtOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVShowCharacterArtOpts.Location = New System.Drawing.Point(240, 179)
        Me.gbTVShowCharacterArtOpts.Name = "gbTVShowCharacterArtOpts"
        Me.gbTVShowCharacterArtOpts.Size = New System.Drawing.Size(228, 44)
        Me.gbTVShowCharacterArtOpts.TabIndex = 6
        Me.gbTVShowCharacterArtOpts.TabStop = false
        Me.gbTVShowCharacterArtOpts.Text = "CharacterArt"
        '
        'chkTVShowCharacterArtOverwrite
        '
        Me.chkTVShowCharacterArtOverwrite.AutoSize = true
        Me.chkTVShowCharacterArtOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowCharacterArtOverwrite.Location = New System.Drawing.Point(6, 21)
        Me.chkTVShowCharacterArtOverwrite.Name = "chkTVShowCharacterArtOverwrite"
        Me.chkTVShowCharacterArtOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVShowCharacterArtOverwrite.TabIndex = 4
        Me.chkTVShowCharacterArtOverwrite.Text = "Overwrite Existing"
        Me.chkTVShowCharacterArtOverwrite.UseVisualStyleBackColor = true
        '
        'gbTVShowClearArtOpts
        '
        Me.gbTVShowClearArtOpts.Controls.Add(Me.chkTVShowClearArtOverwrite)
        Me.gbTVShowClearArtOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVShowClearArtOpts.Location = New System.Drawing.Point(6, 229)
        Me.gbTVShowClearArtOpts.Name = "gbTVShowClearArtOpts"
        Me.gbTVShowClearArtOpts.Size = New System.Drawing.Size(228, 44)
        Me.gbTVShowClearArtOpts.TabIndex = 5
        Me.gbTVShowClearArtOpts.TabStop = false
        Me.gbTVShowClearArtOpts.Text = "ClearArt"
        '
        'chkTVShowClearArtOverwrite
        '
        Me.chkTVShowClearArtOverwrite.AutoSize = true
        Me.chkTVShowClearArtOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowClearArtOverwrite.Location = New System.Drawing.Point(6, 21)
        Me.chkTVShowClearArtOverwrite.Name = "chkTVShowClearArtOverwrite"
        Me.chkTVShowClearArtOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVShowClearArtOverwrite.TabIndex = 4
        Me.chkTVShowClearArtOverwrite.Text = "Overwrite Existing"
        Me.chkTVShowClearArtOverwrite.UseVisualStyleBackColor = true
        '
        'gbTVShowClearLogoOpts
        '
        Me.gbTVShowClearLogoOpts.Controls.Add(Me.chkTVShowClearLogoOverwrite)
        Me.gbTVShowClearLogoOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVShowClearLogoOpts.Location = New System.Drawing.Point(240, 229)
        Me.gbTVShowClearLogoOpts.Name = "gbTVShowClearLogoOpts"
        Me.gbTVShowClearLogoOpts.Size = New System.Drawing.Size(228, 44)
        Me.gbTVShowClearLogoOpts.TabIndex = 4
        Me.gbTVShowClearLogoOpts.TabStop = false
        Me.gbTVShowClearLogoOpts.Text = "ClearLogo"
        '
        'chkTVShowClearLogoOverwrite
        '
        Me.chkTVShowClearLogoOverwrite.AutoSize = true
        Me.chkTVShowClearLogoOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowClearLogoOverwrite.Location = New System.Drawing.Point(6, 21)
        Me.chkTVShowClearLogoOverwrite.Name = "chkTVShowClearLogoOverwrite"
        Me.chkTVShowClearLogoOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVShowClearLogoOverwrite.TabIndex = 4
        Me.chkTVShowClearLogoOverwrite.Text = "Overwrite Existing"
        Me.chkTVShowClearLogoOverwrite.UseVisualStyleBackColor = true
        '
        'gbTVShowLandscapeOpts
        '
        Me.gbTVShowLandscapeOpts.Controls.Add(Me.chkTVShowLandscapeOverwrite)
        Me.gbTVShowLandscapeOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVShowLandscapeOpts.Location = New System.Drawing.Point(6, 179)
        Me.gbTVShowLandscapeOpts.Name = "gbTVShowLandscapeOpts"
        Me.gbTVShowLandscapeOpts.Size = New System.Drawing.Size(228, 44)
        Me.gbTVShowLandscapeOpts.TabIndex = 3
        Me.gbTVShowLandscapeOpts.TabStop = false
        Me.gbTVShowLandscapeOpts.Text = "Landscape"
        '
        'chkTVShowLandscapeOverwrite
        '
        Me.chkTVShowLandscapeOverwrite.AutoSize = true
        Me.chkTVShowLandscapeOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowLandscapeOverwrite.Location = New System.Drawing.Point(6, 21)
        Me.chkTVShowLandscapeOverwrite.Name = "chkTVShowLandscapeOverwrite"
        Me.chkTVShowLandscapeOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVShowLandscapeOverwrite.TabIndex = 4
        Me.chkTVShowLandscapeOverwrite.Text = "Overwrite Existing"
        Me.chkTVShowLandscapeOverwrite.UseVisualStyleBackColor = true
        '
        'gbTVShowBannerOpts
        '
        Me.gbTVShowBannerOpts.Controls.Add(Me.txtTVShowBannerWidth)
        Me.gbTVShowBannerOpts.Controls.Add(Me.txtTVShowBannerHeight)
        Me.gbTVShowBannerOpts.Controls.Add(Me.lblTVShowBannerWidth)
        Me.gbTVShowBannerOpts.Controls.Add(Me.lblTVShowBannerHeight)
        Me.gbTVShowBannerOpts.Controls.Add(Me.chkTVShowBannerResize)
        Me.gbTVShowBannerOpts.Controls.Add(Me.lblTVShowBannerType)
        Me.gbTVShowBannerOpts.Controls.Add(Me.cbTVShowBannerPrefType)
        Me.gbTVShowBannerOpts.Controls.Add(Me.chkTVShowBannerOverwrite)
        Me.gbTVShowBannerOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVShowBannerOpts.Location = New System.Drawing.Point(240, 6)
        Me.gbTVShowBannerOpts.Name = "gbTVShowBannerOpts"
        Me.gbTVShowBannerOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbTVShowBannerOpts.TabIndex = 2
        Me.gbTVShowBannerOpts.TabStop = false
        Me.gbTVShowBannerOpts.Text = "Banner"
        '
        'txtTVShowBannerWidth
        '
        Me.txtTVShowBannerWidth.Enabled = false
        Me.txtTVShowBannerWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVShowBannerWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtTVShowBannerWidth.Name = "txtTVShowBannerWidth"
        Me.txtTVShowBannerWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtTVShowBannerWidth.TabIndex = 7
        '
        'txtTVShowBannerHeight
        '
        Me.txtTVShowBannerHeight.Enabled = false
        Me.txtTVShowBannerHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVShowBannerHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtTVShowBannerHeight.Name = "txtTVShowBannerHeight"
        Me.txtTVShowBannerHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtTVShowBannerHeight.TabIndex = 9
        '
        'lblTVShowBannerWidth
        '
        Me.lblTVShowBannerWidth.AutoSize = true
        Me.lblTVShowBannerWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVShowBannerWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblTVShowBannerWidth.Name = "lblTVShowBannerWidth"
        Me.lblTVShowBannerWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblTVShowBannerWidth.TabIndex = 6
        Me.lblTVShowBannerWidth.Text = "Max Width:"
        '
        'lblTVShowBannerHeight
        '
        Me.lblTVShowBannerHeight.AutoSize = true
        Me.lblTVShowBannerHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVShowBannerHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblTVShowBannerHeight.Name = "lblTVShowBannerHeight"
        Me.lblTVShowBannerHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblTVShowBannerHeight.TabIndex = 8
        Me.lblTVShowBannerHeight.Text = "Max Height:"
        '
        'chkTVShowBannerResize
        '
        Me.chkTVShowBannerResize.AutoSize = true
        Me.chkTVShowBannerResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowBannerResize.Location = New System.Drawing.Point(6, 69)
        Me.chkTVShowBannerResize.Name = "chkTVShowBannerResize"
        Me.chkTVShowBannerResize.Size = New System.Drawing.Size(133, 17)
        Me.chkTVShowBannerResize.TabIndex = 5
        Me.chkTVShowBannerResize.Text = "Automatically Resize:"
        Me.chkTVShowBannerResize.UseVisualStyleBackColor = true
        '
        'lblTVShowBannerType
        '
        Me.lblTVShowBannerType.AutoSize = true
        Me.lblTVShowBannerType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVShowBannerType.Location = New System.Drawing.Point(3, 14)
        Me.lblTVShowBannerType.Name = "lblTVShowBannerType"
        Me.lblTVShowBannerType.Size = New System.Drawing.Size(83, 13)
        Me.lblTVShowBannerType.TabIndex = 0
        Me.lblTVShowBannerType.Text = "Preferred Type:"
        '
        'cbTVShowBannerPrefType
        '
        Me.cbTVShowBannerPrefType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVShowBannerPrefType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVShowBannerPrefType.FormattingEnabled = true
        Me.cbTVShowBannerPrefType.Location = New System.Drawing.Point(6, 29)
        Me.cbTVShowBannerPrefType.Name = "cbTVShowBannerPrefType"
        Me.cbTVShowBannerPrefType.Size = New System.Drawing.Size(148, 21)
        Me.cbTVShowBannerPrefType.TabIndex = 3
        '
        'chkTVShowBannerOverwrite
        '
        Me.chkTVShowBannerOverwrite.AutoSize = true
        Me.chkTVShowBannerOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowBannerOverwrite.Location = New System.Drawing.Point(6, 53)
        Me.chkTVShowBannerOverwrite.Name = "chkTVShowBannerOverwrite"
        Me.chkTVShowBannerOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVShowBannerOverwrite.TabIndex = 4
        Me.chkTVShowBannerOverwrite.Text = "Overwrite Existing"
        Me.chkTVShowBannerOverwrite.UseVisualStyleBackColor = true
        '
        'gbTVShowPosterOpts
        '
        Me.gbTVShowPosterOpts.Controls.Add(Me.txtTVShowPosterWidth)
        Me.gbTVShowPosterOpts.Controls.Add(Me.txtTVShowPosterHeight)
        Me.gbTVShowPosterOpts.Controls.Add(Me.lblTVShowPosterWidth)
        Me.gbTVShowPosterOpts.Controls.Add(Me.lblTVShowPosterHeight)
        Me.gbTVShowPosterOpts.Controls.Add(Me.chkTVShowPosterResize)
        Me.gbTVShowPosterOpts.Controls.Add(Me.lblTVShowPosterSize)
        Me.gbTVShowPosterOpts.Controls.Add(Me.cbTVShowPosterPrefSize)
        Me.gbTVShowPosterOpts.Controls.Add(Me.chkTVShowPosterOverwrite)
        Me.gbTVShowPosterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVShowPosterOpts.Location = New System.Drawing.Point(6, 6)
        Me.gbTVShowPosterOpts.Name = "gbTVShowPosterOpts"
        Me.gbTVShowPosterOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbTVShowPosterOpts.TabIndex = 0
        Me.gbTVShowPosterOpts.TabStop = false
        Me.gbTVShowPosterOpts.Text = "Poster"
        '
        'txtTVShowPosterWidth
        '
        Me.txtTVShowPosterWidth.Enabled = false
        Me.txtTVShowPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVShowPosterWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtTVShowPosterWidth.Name = "txtTVShowPosterWidth"
        Me.txtTVShowPosterWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtTVShowPosterWidth.TabIndex = 7
        '
        'txtTVShowPosterHeight
        '
        Me.txtTVShowPosterHeight.Enabled = false
        Me.txtTVShowPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVShowPosterHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtTVShowPosterHeight.Name = "txtTVShowPosterHeight"
        Me.txtTVShowPosterHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtTVShowPosterHeight.TabIndex = 9
        '
        'lblTVShowPosterWidth
        '
        Me.lblTVShowPosterWidth.AutoSize = true
        Me.lblTVShowPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVShowPosterWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblTVShowPosterWidth.Name = "lblTVShowPosterWidth"
        Me.lblTVShowPosterWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblTVShowPosterWidth.TabIndex = 6
        Me.lblTVShowPosterWidth.Text = "Max Width:"
        '
        'lblTVShowPosterHeight
        '
        Me.lblTVShowPosterHeight.AutoSize = true
        Me.lblTVShowPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVShowPosterHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblTVShowPosterHeight.Name = "lblTVShowPosterHeight"
        Me.lblTVShowPosterHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblTVShowPosterHeight.TabIndex = 8
        Me.lblTVShowPosterHeight.Text = "Max Height:"
        '
        'chkTVShowPosterResize
        '
        Me.chkTVShowPosterResize.AutoSize = true
        Me.chkTVShowPosterResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowPosterResize.Location = New System.Drawing.Point(6, 69)
        Me.chkTVShowPosterResize.Name = "chkTVShowPosterResize"
        Me.chkTVShowPosterResize.Size = New System.Drawing.Size(133, 17)
        Me.chkTVShowPosterResize.TabIndex = 5
        Me.chkTVShowPosterResize.Text = "Automatically Resize:"
        Me.chkTVShowPosterResize.UseVisualStyleBackColor = true
        '
        'lblTVShowPosterSize
        '
        Me.lblTVShowPosterSize.AutoSize = true
        Me.lblTVShowPosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVShowPosterSize.Location = New System.Drawing.Point(3, 14)
        Me.lblTVShowPosterSize.Name = "lblTVShowPosterSize"
        Me.lblTVShowPosterSize.Size = New System.Drawing.Size(80, 13)
        Me.lblTVShowPosterSize.TabIndex = 0
        Me.lblTVShowPosterSize.Text = "Preferred Size:"
        '
        'cbTVShowPosterPrefSize
        '
        Me.cbTVShowPosterPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVShowPosterPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVShowPosterPrefSize.FormattingEnabled = true
        Me.cbTVShowPosterPrefSize.Location = New System.Drawing.Point(6, 29)
        Me.cbTVShowPosterPrefSize.Name = "cbTVShowPosterPrefSize"
        Me.cbTVShowPosterPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbTVShowPosterPrefSize.TabIndex = 3
        '
        'chkTVShowPosterOverwrite
        '
        Me.chkTVShowPosterOverwrite.AutoSize = true
        Me.chkTVShowPosterOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowPosterOverwrite.Location = New System.Drawing.Point(6, 53)
        Me.chkTVShowPosterOverwrite.Name = "chkTVShowPosterOverwrite"
        Me.chkTVShowPosterOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVShowPosterOverwrite.TabIndex = 4
        Me.chkTVShowPosterOverwrite.Text = "Overwrite Existing"
        Me.chkTVShowPosterOverwrite.UseVisualStyleBackColor = true
        '
        'gbTVShowFanartOpts
        '
        Me.gbTVShowFanartOpts.Controls.Add(Me.txtTVShowFanartWidth)
        Me.gbTVShowFanartOpts.Controls.Add(Me.txtTVShowFanartHeight)
        Me.gbTVShowFanartOpts.Controls.Add(Me.lblTVShowFanartWidth)
        Me.gbTVShowFanartOpts.Controls.Add(Me.lblTVShowFanartHeight)
        Me.gbTVShowFanartOpts.Controls.Add(Me.chkTVShowFanartResize)
        Me.gbTVShowFanartOpts.Controls.Add(Me.cbTVShowFanartPrefSize)
        Me.gbTVShowFanartOpts.Controls.Add(Me.lblTVShowFanartSize)
        Me.gbTVShowFanartOpts.Controls.Add(Me.chkTVShowFanartOverwrite)
        Me.gbTVShowFanartOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVShowFanartOpts.Location = New System.Drawing.Point(474, 6)
        Me.gbTVShowFanartOpts.Name = "gbTVShowFanartOpts"
        Me.gbTVShowFanartOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbTVShowFanartOpts.TabIndex = 1
        Me.gbTVShowFanartOpts.TabStop = false
        Me.gbTVShowFanartOpts.Text = "Fanart"
        '
        'txtTVShowFanartWidth
        '
        Me.txtTVShowFanartWidth.Enabled = false
        Me.txtTVShowFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVShowFanartWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtTVShowFanartWidth.Name = "txtTVShowFanartWidth"
        Me.txtTVShowFanartWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtTVShowFanartWidth.TabIndex = 5
        '
        'txtTVShowFanartHeight
        '
        Me.txtTVShowFanartHeight.Enabled = false
        Me.txtTVShowFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVShowFanartHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtTVShowFanartHeight.Name = "txtTVShowFanartHeight"
        Me.txtTVShowFanartHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtTVShowFanartHeight.TabIndex = 7
        '
        'lblTVShowFanartWidth
        '
        Me.lblTVShowFanartWidth.AutoSize = true
        Me.lblTVShowFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVShowFanartWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblTVShowFanartWidth.Name = "lblTVShowFanartWidth"
        Me.lblTVShowFanartWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblTVShowFanartWidth.TabIndex = 4
        Me.lblTVShowFanartWidth.Text = "Max Width:"
        '
        'lblTVShowFanartHeight
        '
        Me.lblTVShowFanartHeight.AutoSize = true
        Me.lblTVShowFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVShowFanartHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblTVShowFanartHeight.Name = "lblTVShowFanartHeight"
        Me.lblTVShowFanartHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblTVShowFanartHeight.TabIndex = 6
        Me.lblTVShowFanartHeight.Text = "Max Height:"
        '
        'chkTVShowFanartResize
        '
        Me.chkTVShowFanartResize.AutoSize = true
        Me.chkTVShowFanartResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowFanartResize.Location = New System.Drawing.Point(6, 69)
        Me.chkTVShowFanartResize.Name = "chkTVShowFanartResize"
        Me.chkTVShowFanartResize.Size = New System.Drawing.Size(133, 17)
        Me.chkTVShowFanartResize.TabIndex = 3
        Me.chkTVShowFanartResize.Text = "Automatically Resize:"
        Me.chkTVShowFanartResize.UseVisualStyleBackColor = true
        '
        'cbTVShowFanartPrefSize
        '
        Me.cbTVShowFanartPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVShowFanartPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVShowFanartPrefSize.FormattingEnabled = true
        Me.cbTVShowFanartPrefSize.Location = New System.Drawing.Point(6, 29)
        Me.cbTVShowFanartPrefSize.Name = "cbTVShowFanartPrefSize"
        Me.cbTVShowFanartPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbTVShowFanartPrefSize.TabIndex = 1
        '
        'lblTVShowFanartSize
        '
        Me.lblTVShowFanartSize.AutoSize = true
        Me.lblTVShowFanartSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVShowFanartSize.Location = New System.Drawing.Point(3, 14)
        Me.lblTVShowFanartSize.Name = "lblTVShowFanartSize"
        Me.lblTVShowFanartSize.Size = New System.Drawing.Size(80, 13)
        Me.lblTVShowFanartSize.TabIndex = 0
        Me.lblTVShowFanartSize.Text = "Preferred Size:"
        '
        'chkTVShowFanartOverwrite
        '
        Me.chkTVShowFanartOverwrite.AutoSize = true
        Me.chkTVShowFanartOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVShowFanartOverwrite.Location = New System.Drawing.Point(6, 53)
        Me.chkTVShowFanartOverwrite.Name = "chkTVShowFanartOverwrite"
        Me.chkTVShowFanartOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVShowFanartOverwrite.TabIndex = 2
        Me.chkTVShowFanartOverwrite.Text = "Overwrite Existing"
        Me.chkTVShowFanartOverwrite.UseVisualStyleBackColor = true
        '
        'tpTVAllSeasons
        '
        Me.tpTVAllSeasons.Controls.Add(Me.gbTVASLandscapeOpts)
        Me.tpTVAllSeasons.Controls.Add(Me.gbTVASFanartOpts)
        Me.tpTVAllSeasons.Controls.Add(Me.gbTVASBannerOpts)
        Me.tpTVAllSeasons.Controls.Add(Me.gbTVASPosterOpts)
        Me.tpTVAllSeasons.Location = New System.Drawing.Point(4, 22)
        Me.tpTVAllSeasons.Name = "tpTVAllSeasons"
        Me.tpTVAllSeasons.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTVAllSeasons.Size = New System.Drawing.Size(734, 465)
        Me.tpTVAllSeasons.TabIndex = 3
        Me.tpTVAllSeasons.Text = "TV All Seasons"
        Me.tpTVAllSeasons.UseVisualStyleBackColor = true
        '
        'gbTVASLandscapeOpts
        '
        Me.gbTVASLandscapeOpts.Controls.Add(Me.chkTVASLandscapeOverwrite)
        Me.gbTVASLandscapeOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVASLandscapeOpts.Location = New System.Drawing.Point(6, 179)
        Me.gbTVASLandscapeOpts.Name = "gbTVASLandscapeOpts"
        Me.gbTVASLandscapeOpts.Size = New System.Drawing.Size(228, 44)
        Me.gbTVASLandscapeOpts.TabIndex = 8
        Me.gbTVASLandscapeOpts.TabStop = false
        Me.gbTVASLandscapeOpts.Text = "Landscape"
        '
        'chkTVASLandscapeOverwrite
        '
        Me.chkTVASLandscapeOverwrite.AutoSize = true
        Me.chkTVASLandscapeOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVASLandscapeOverwrite.Location = New System.Drawing.Point(6, 21)
        Me.chkTVASLandscapeOverwrite.Name = "chkTVASLandscapeOverwrite"
        Me.chkTVASLandscapeOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVASLandscapeOverwrite.TabIndex = 4
        Me.chkTVASLandscapeOverwrite.Text = "Overwrite Existing"
        Me.chkTVASLandscapeOverwrite.UseVisualStyleBackColor = true
        '
        'gbTVASFanartOpts
        '
        Me.gbTVASFanartOpts.Controls.Add(Me.txtTVASFanartWidth)
        Me.gbTVASFanartOpts.Controls.Add(Me.txtTVASFanartHeight)
        Me.gbTVASFanartOpts.Controls.Add(Me.lblTVASFanartWidth)
        Me.gbTVASFanartOpts.Controls.Add(Me.lblTVASFanartHeight)
        Me.gbTVASFanartOpts.Controls.Add(Me.chkTVASFanartResize)
        Me.gbTVASFanartOpts.Controls.Add(Me.cbTVASFanartPrefSize)
        Me.gbTVASFanartOpts.Controls.Add(Me.lblTVASFanartSize)
        Me.gbTVASFanartOpts.Controls.Add(Me.chkTVASFanartOverwrite)
        Me.gbTVASFanartOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVASFanartOpts.Location = New System.Drawing.Point(474, 6)
        Me.gbTVASFanartOpts.Name = "gbTVASFanartOpts"
        Me.gbTVASFanartOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbTVASFanartOpts.TabIndex = 7
        Me.gbTVASFanartOpts.TabStop = false
        Me.gbTVASFanartOpts.Text = "Fanart"
        '
        'txtTVASFanartWidth
        '
        Me.txtTVASFanartWidth.Enabled = false
        Me.txtTVASFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVASFanartWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtTVASFanartWidth.Name = "txtTVASFanartWidth"
        Me.txtTVASFanartWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtTVASFanartWidth.TabIndex = 5
        '
        'txtTVASFanartHeight
        '
        Me.txtTVASFanartHeight.Enabled = false
        Me.txtTVASFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVASFanartHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtTVASFanartHeight.Name = "txtTVASFanartHeight"
        Me.txtTVASFanartHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtTVASFanartHeight.TabIndex = 7
        '
        'lblTVASFanartWidth
        '
        Me.lblTVASFanartWidth.AutoSize = true
        Me.lblTVASFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVASFanartWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblTVASFanartWidth.Name = "lblTVASFanartWidth"
        Me.lblTVASFanartWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblTVASFanartWidth.TabIndex = 4
        Me.lblTVASFanartWidth.Text = "Max Width:"
        '
        'lblTVASFanartHeight
        '
        Me.lblTVASFanartHeight.AutoSize = true
        Me.lblTVASFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVASFanartHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblTVASFanartHeight.Name = "lblTVASFanartHeight"
        Me.lblTVASFanartHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblTVASFanartHeight.TabIndex = 6
        Me.lblTVASFanartHeight.Text = "Max Height:"
        '
        'chkTVASFanartResize
        '
        Me.chkTVASFanartResize.AutoSize = true
        Me.chkTVASFanartResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVASFanartResize.Location = New System.Drawing.Point(6, 69)
        Me.chkTVASFanartResize.Name = "chkTVASFanartResize"
        Me.chkTVASFanartResize.Size = New System.Drawing.Size(133, 17)
        Me.chkTVASFanartResize.TabIndex = 3
        Me.chkTVASFanartResize.Text = "Automatically Resize:"
        Me.chkTVASFanartResize.UseVisualStyleBackColor = true
        '
        'cbTVASFanartPrefSize
        '
        Me.cbTVASFanartPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVASFanartPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVASFanartPrefSize.FormattingEnabled = true
        Me.cbTVASFanartPrefSize.Location = New System.Drawing.Point(6, 29)
        Me.cbTVASFanartPrefSize.Name = "cbTVASFanartPrefSize"
        Me.cbTVASFanartPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbTVASFanartPrefSize.TabIndex = 1
        '
        'lblTVASFanartSize
        '
        Me.lblTVASFanartSize.AutoSize = true
        Me.lblTVASFanartSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVASFanartSize.Location = New System.Drawing.Point(3, 14)
        Me.lblTVASFanartSize.Name = "lblTVASFanartSize"
        Me.lblTVASFanartSize.Size = New System.Drawing.Size(80, 13)
        Me.lblTVASFanartSize.TabIndex = 0
        Me.lblTVASFanartSize.Text = "Preferred Size:"
        '
        'chkTVASFanartOverwrite
        '
        Me.chkTVASFanartOverwrite.AutoSize = true
        Me.chkTVASFanartOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVASFanartOverwrite.Location = New System.Drawing.Point(6, 53)
        Me.chkTVASFanartOverwrite.Name = "chkTVASFanartOverwrite"
        Me.chkTVASFanartOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVASFanartOverwrite.TabIndex = 2
        Me.chkTVASFanartOverwrite.Text = "Overwrite Existing"
        Me.chkTVASFanartOverwrite.UseVisualStyleBackColor = true
        '
        'gbTVASBannerOpts
        '
        Me.gbTVASBannerOpts.Controls.Add(Me.txtTVASBannerWidth)
        Me.gbTVASBannerOpts.Controls.Add(Me.txtTVASBannerHeight)
        Me.gbTVASBannerOpts.Controls.Add(Me.lblTVASBannerWidth)
        Me.gbTVASBannerOpts.Controls.Add(Me.lblTVASBannerHeight)
        Me.gbTVASBannerOpts.Controls.Add(Me.chkTVASBannerResize)
        Me.gbTVASBannerOpts.Controls.Add(Me.lblTVASBannerType)
        Me.gbTVASBannerOpts.Controls.Add(Me.cbTVASBannerPrefType)
        Me.gbTVASBannerOpts.Controls.Add(Me.chkTVASBannerOverwrite)
        Me.gbTVASBannerOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVASBannerOpts.Location = New System.Drawing.Point(240, 6)
        Me.gbTVASBannerOpts.Name = "gbTVASBannerOpts"
        Me.gbTVASBannerOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbTVASBannerOpts.TabIndex = 6
        Me.gbTVASBannerOpts.TabStop = false
        Me.gbTVASBannerOpts.Text = "Banner"
        '
        'txtTVASBannerWidth
        '
        Me.txtTVASBannerWidth.Enabled = false
        Me.txtTVASBannerWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVASBannerWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtTVASBannerWidth.Name = "txtTVASBannerWidth"
        Me.txtTVASBannerWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtTVASBannerWidth.TabIndex = 7
        '
        'txtTVASBannerHeight
        '
        Me.txtTVASBannerHeight.Enabled = false
        Me.txtTVASBannerHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVASBannerHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtTVASBannerHeight.Name = "txtTVASBannerHeight"
        Me.txtTVASBannerHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtTVASBannerHeight.TabIndex = 9
        '
        'lblTVASBannerWidth
        '
        Me.lblTVASBannerWidth.AutoSize = true
        Me.lblTVASBannerWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVASBannerWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblTVASBannerWidth.Name = "lblTVASBannerWidth"
        Me.lblTVASBannerWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblTVASBannerWidth.TabIndex = 6
        Me.lblTVASBannerWidth.Text = "Max Width:"
        '
        'lblTVASBannerHeight
        '
        Me.lblTVASBannerHeight.AutoSize = true
        Me.lblTVASBannerHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVASBannerHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblTVASBannerHeight.Name = "lblTVASBannerHeight"
        Me.lblTVASBannerHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblTVASBannerHeight.TabIndex = 8
        Me.lblTVASBannerHeight.Text = "Max Height:"
        '
        'chkTVASBannerResize
        '
        Me.chkTVASBannerResize.AutoSize = true
        Me.chkTVASBannerResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVASBannerResize.Location = New System.Drawing.Point(6, 69)
        Me.chkTVASBannerResize.Name = "chkTVASBannerResize"
        Me.chkTVASBannerResize.Size = New System.Drawing.Size(133, 17)
        Me.chkTVASBannerResize.TabIndex = 5
        Me.chkTVASBannerResize.Text = "Automatically Resize:"
        Me.chkTVASBannerResize.UseVisualStyleBackColor = true
        '
        'lblTVASBannerType
        '
        Me.lblTVASBannerType.AutoSize = true
        Me.lblTVASBannerType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVASBannerType.Location = New System.Drawing.Point(3, 14)
        Me.lblTVASBannerType.Name = "lblTVASBannerType"
        Me.lblTVASBannerType.Size = New System.Drawing.Size(83, 13)
        Me.lblTVASBannerType.TabIndex = 0
        Me.lblTVASBannerType.Text = "Preferred Type:"
        '
        'cbTVASBannerPrefType
        '
        Me.cbTVASBannerPrefType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVASBannerPrefType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVASBannerPrefType.FormattingEnabled = true
        Me.cbTVASBannerPrefType.Location = New System.Drawing.Point(6, 29)
        Me.cbTVASBannerPrefType.Name = "cbTVASBannerPrefType"
        Me.cbTVASBannerPrefType.Size = New System.Drawing.Size(148, 21)
        Me.cbTVASBannerPrefType.TabIndex = 3
        '
        'chkTVASBannerOverwrite
        '
        Me.chkTVASBannerOverwrite.AutoSize = true
        Me.chkTVASBannerOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVASBannerOverwrite.Location = New System.Drawing.Point(6, 53)
        Me.chkTVASBannerOverwrite.Name = "chkTVASBannerOverwrite"
        Me.chkTVASBannerOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVASBannerOverwrite.TabIndex = 4
        Me.chkTVASBannerOverwrite.Text = "Overwrite Existing"
        Me.chkTVASBannerOverwrite.UseVisualStyleBackColor = true
        '
        'gbTVASPosterOpts
        '
        Me.gbTVASPosterOpts.Controls.Add(Me.txtTVASPosterWidth)
        Me.gbTVASPosterOpts.Controls.Add(Me.txtTVASPosterHeight)
        Me.gbTVASPosterOpts.Controls.Add(Me.lblTVASPosterWidth)
        Me.gbTVASPosterOpts.Controls.Add(Me.lblTVASPosterHeight)
        Me.gbTVASPosterOpts.Controls.Add(Me.chkTVASPosterResize)
        Me.gbTVASPosterOpts.Controls.Add(Me.lblTVASPosterSize)
        Me.gbTVASPosterOpts.Controls.Add(Me.cbTVASPosterPrefSize)
        Me.gbTVASPosterOpts.Controls.Add(Me.chkTVASPosterOverwrite)
        Me.gbTVASPosterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVASPosterOpts.Location = New System.Drawing.Point(6, 6)
        Me.gbTVASPosterOpts.Name = "gbTVASPosterOpts"
        Me.gbTVASPosterOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbTVASPosterOpts.TabIndex = 5
        Me.gbTVASPosterOpts.TabStop = false
        Me.gbTVASPosterOpts.Text = "Poster"
        '
        'txtTVASPosterWidth
        '
        Me.txtTVASPosterWidth.Enabled = false
        Me.txtTVASPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVASPosterWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtTVASPosterWidth.Name = "txtTVASPosterWidth"
        Me.txtTVASPosterWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtTVASPosterWidth.TabIndex = 7
        '
        'txtTVASPosterHeight
        '
        Me.txtTVASPosterHeight.Enabled = false
        Me.txtTVASPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVASPosterHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtTVASPosterHeight.Name = "txtTVASPosterHeight"
        Me.txtTVASPosterHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtTVASPosterHeight.TabIndex = 9
        '
        'lblTVASPosterWidth
        '
        Me.lblTVASPosterWidth.AutoSize = true
        Me.lblTVASPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVASPosterWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblTVASPosterWidth.Name = "lblTVASPosterWidth"
        Me.lblTVASPosterWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblTVASPosterWidth.TabIndex = 6
        Me.lblTVASPosterWidth.Text = "Max Width:"
        '
        'lblTVASPosterHeight
        '
        Me.lblTVASPosterHeight.AutoSize = true
        Me.lblTVASPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVASPosterHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblTVASPosterHeight.Name = "lblTVASPosterHeight"
        Me.lblTVASPosterHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblTVASPosterHeight.TabIndex = 8
        Me.lblTVASPosterHeight.Text = "Max Height:"
        '
        'chkTVASPosterResize
        '
        Me.chkTVASPosterResize.AutoSize = true
        Me.chkTVASPosterResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVASPosterResize.Location = New System.Drawing.Point(6, 69)
        Me.chkTVASPosterResize.Name = "chkTVASPosterResize"
        Me.chkTVASPosterResize.Size = New System.Drawing.Size(133, 17)
        Me.chkTVASPosterResize.TabIndex = 5
        Me.chkTVASPosterResize.Text = "Automatically Resize:"
        Me.chkTVASPosterResize.UseVisualStyleBackColor = true
        '
        'lblTVASPosterSize
        '
        Me.lblTVASPosterSize.AutoSize = true
        Me.lblTVASPosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVASPosterSize.Location = New System.Drawing.Point(3, 14)
        Me.lblTVASPosterSize.Name = "lblTVASPosterSize"
        Me.lblTVASPosterSize.Size = New System.Drawing.Size(80, 13)
        Me.lblTVASPosterSize.TabIndex = 0
        Me.lblTVASPosterSize.Text = "Preferred Size:"
        '
        'cbTVASPosterPrefSize
        '
        Me.cbTVASPosterPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVASPosterPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVASPosterPrefSize.FormattingEnabled = true
        Me.cbTVASPosterPrefSize.Location = New System.Drawing.Point(6, 29)
        Me.cbTVASPosterPrefSize.Name = "cbTVASPosterPrefSize"
        Me.cbTVASPosterPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbTVASPosterPrefSize.TabIndex = 3
        '
        'chkTVASPosterOverwrite
        '
        Me.chkTVASPosterOverwrite.AutoSize = true
        Me.chkTVASPosterOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVASPosterOverwrite.Location = New System.Drawing.Point(6, 53)
        Me.chkTVASPosterOverwrite.Name = "chkTVASPosterOverwrite"
        Me.chkTVASPosterOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVASPosterOverwrite.TabIndex = 4
        Me.chkTVASPosterOverwrite.Text = "Overwrite Existing"
        Me.chkTVASPosterOverwrite.UseVisualStyleBackColor = true
        '
        'tpTVSeason
        '
        Me.tpTVSeason.Controls.Add(Me.gbTVSeasonLandscapeOpts)
        Me.tpTVSeason.Controls.Add(Me.gbTVSeasonBannerOpts)
        Me.tpTVSeason.Controls.Add(Me.gbTVSeasonPosterOpts)
        Me.tpTVSeason.Controls.Add(Me.gbTVSeasonFanartOpts)
        Me.tpTVSeason.Location = New System.Drawing.Point(4, 22)
        Me.tpTVSeason.Name = "tpTVSeason"
        Me.tpTVSeason.Size = New System.Drawing.Size(734, 465)
        Me.tpTVSeason.TabIndex = 2
        Me.tpTVSeason.Text = "TV Season"
        Me.tpTVSeason.UseVisualStyleBackColor = true
        '
        'gbTVSeasonLandscapeOpts
        '
        Me.gbTVSeasonLandscapeOpts.Controls.Add(Me.chkTVSeasonLandscapeOverwrite)
        Me.gbTVSeasonLandscapeOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVSeasonLandscapeOpts.Location = New System.Drawing.Point(6, 179)
        Me.gbTVSeasonLandscapeOpts.Name = "gbTVSeasonLandscapeOpts"
        Me.gbTVSeasonLandscapeOpts.Size = New System.Drawing.Size(228, 44)
        Me.gbTVSeasonLandscapeOpts.TabIndex = 12
        Me.gbTVSeasonLandscapeOpts.TabStop = false
        Me.gbTVSeasonLandscapeOpts.Text = "Landscape"
        '
        'chkTVSeasonLandscapeOverwrite
        '
        Me.chkTVSeasonLandscapeOverwrite.AutoSize = true
        Me.chkTVSeasonLandscapeOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVSeasonLandscapeOverwrite.Location = New System.Drawing.Point(6, 21)
        Me.chkTVSeasonLandscapeOverwrite.Name = "chkTVSeasonLandscapeOverwrite"
        Me.chkTVSeasonLandscapeOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVSeasonLandscapeOverwrite.TabIndex = 2
        Me.chkTVSeasonLandscapeOverwrite.Text = "Overwrite Existing"
        Me.chkTVSeasonLandscapeOverwrite.UseVisualStyleBackColor = true
        '
        'gbTVSeasonBannerOpts
        '
        Me.gbTVSeasonBannerOpts.Controls.Add(Me.txtTVSeasonBannerWidth)
        Me.gbTVSeasonBannerOpts.Controls.Add(Me.txtTVSeasonBannerHeight)
        Me.gbTVSeasonBannerOpts.Controls.Add(Me.lblTVSeasonBannerWidth)
        Me.gbTVSeasonBannerOpts.Controls.Add(Me.lblTVSeasonBannerHeight)
        Me.gbTVSeasonBannerOpts.Controls.Add(Me.chkTVSeasonBannerResize)
        Me.gbTVSeasonBannerOpts.Controls.Add(Me.lblTVSeasonBannerType)
        Me.gbTVSeasonBannerOpts.Controls.Add(Me.cbTVSeasonBannerPrefType)
        Me.gbTVSeasonBannerOpts.Controls.Add(Me.chkTVSeasonBannerOverwrite)
        Me.gbTVSeasonBannerOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVSeasonBannerOpts.Location = New System.Drawing.Point(240, 6)
        Me.gbTVSeasonBannerOpts.Name = "gbTVSeasonBannerOpts"
        Me.gbTVSeasonBannerOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbTVSeasonBannerOpts.TabIndex = 11
        Me.gbTVSeasonBannerOpts.TabStop = false
        Me.gbTVSeasonBannerOpts.Text = "Banner"
        '
        'txtTVSeasonBannerWidth
        '
        Me.txtTVSeasonBannerWidth.Enabled = false
        Me.txtTVSeasonBannerWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVSeasonBannerWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtTVSeasonBannerWidth.Name = "txtTVSeasonBannerWidth"
        Me.txtTVSeasonBannerWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtTVSeasonBannerWidth.TabIndex = 5
        '
        'txtTVSeasonBannerHeight
        '
        Me.txtTVSeasonBannerHeight.Enabled = false
        Me.txtTVSeasonBannerHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVSeasonBannerHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtTVSeasonBannerHeight.Name = "txtTVSeasonBannerHeight"
        Me.txtTVSeasonBannerHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtTVSeasonBannerHeight.TabIndex = 7
        '
        'lblTVSeasonBannerWidth
        '
        Me.lblTVSeasonBannerWidth.AutoSize = true
        Me.lblTVSeasonBannerWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVSeasonBannerWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblTVSeasonBannerWidth.Name = "lblTVSeasonBannerWidth"
        Me.lblTVSeasonBannerWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblTVSeasonBannerWidth.TabIndex = 4
        Me.lblTVSeasonBannerWidth.Text = "Max Width:"
        '
        'lblTVSeasonBannerHeight
        '
        Me.lblTVSeasonBannerHeight.AutoSize = true
        Me.lblTVSeasonBannerHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVSeasonBannerHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblTVSeasonBannerHeight.Name = "lblTVSeasonBannerHeight"
        Me.lblTVSeasonBannerHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblTVSeasonBannerHeight.TabIndex = 6
        Me.lblTVSeasonBannerHeight.Text = "Max Height:"
        '
        'chkTVSeasonBannerResize
        '
        Me.chkTVSeasonBannerResize.AutoSize = true
        Me.chkTVSeasonBannerResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVSeasonBannerResize.Location = New System.Drawing.Point(6, 69)
        Me.chkTVSeasonBannerResize.Name = "chkTVSeasonBannerResize"
        Me.chkTVSeasonBannerResize.Size = New System.Drawing.Size(133, 17)
        Me.chkTVSeasonBannerResize.TabIndex = 3
        Me.chkTVSeasonBannerResize.Text = "Automatically Resize:"
        Me.chkTVSeasonBannerResize.UseVisualStyleBackColor = true
        '
        'lblTVSeasonBannerType
        '
        Me.lblTVSeasonBannerType.AutoSize = true
        Me.lblTVSeasonBannerType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVSeasonBannerType.Location = New System.Drawing.Point(3, 14)
        Me.lblTVSeasonBannerType.Name = "lblTVSeasonBannerType"
        Me.lblTVSeasonBannerType.Size = New System.Drawing.Size(83, 13)
        Me.lblTVSeasonBannerType.TabIndex = 0
        Me.lblTVSeasonBannerType.Text = "Preferred Type:"
        '
        'cbTVSeasonBannerPrefType
        '
        Me.cbTVSeasonBannerPrefType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVSeasonBannerPrefType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVSeasonBannerPrefType.FormattingEnabled = true
        Me.cbTVSeasonBannerPrefType.Location = New System.Drawing.Point(6, 29)
        Me.cbTVSeasonBannerPrefType.Name = "cbTVSeasonBannerPrefType"
        Me.cbTVSeasonBannerPrefType.Size = New System.Drawing.Size(148, 21)
        Me.cbTVSeasonBannerPrefType.TabIndex = 1
        '
        'chkTVSeasonBannerOverwrite
        '
        Me.chkTVSeasonBannerOverwrite.AutoSize = true
        Me.chkTVSeasonBannerOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVSeasonBannerOverwrite.Location = New System.Drawing.Point(6, 53)
        Me.chkTVSeasonBannerOverwrite.Name = "chkTVSeasonBannerOverwrite"
        Me.chkTVSeasonBannerOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVSeasonBannerOverwrite.TabIndex = 2
        Me.chkTVSeasonBannerOverwrite.Text = "Overwrite Existing"
        Me.chkTVSeasonBannerOverwrite.UseVisualStyleBackColor = true
        '
        'gbTVSeasonPosterOpts
        '
        Me.gbTVSeasonPosterOpts.Controls.Add(Me.txtTVSeasonPosterWidth)
        Me.gbTVSeasonPosterOpts.Controls.Add(Me.txtTVSeasonPosterHeight)
        Me.gbTVSeasonPosterOpts.Controls.Add(Me.lblTVSeasonPosterWidth)
        Me.gbTVSeasonPosterOpts.Controls.Add(Me.lblTVSeasonPosterHeight)
        Me.gbTVSeasonPosterOpts.Controls.Add(Me.chkTVSeasonPosterResize)
        Me.gbTVSeasonPosterOpts.Controls.Add(Me.lblTVSeasonPosterSize)
        Me.gbTVSeasonPosterOpts.Controls.Add(Me.cbTVSeasonPosterPrefSize)
        Me.gbTVSeasonPosterOpts.Controls.Add(Me.chkTVSeasonPosterOverwrite)
        Me.gbTVSeasonPosterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVSeasonPosterOpts.Location = New System.Drawing.Point(6, 6)
        Me.gbTVSeasonPosterOpts.Name = "gbTVSeasonPosterOpts"
        Me.gbTVSeasonPosterOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbTVSeasonPosterOpts.TabIndex = 0
        Me.gbTVSeasonPosterOpts.TabStop = false
        Me.gbTVSeasonPosterOpts.Text = "Poster"
        '
        'txtTVSeasonPosterWidth
        '
        Me.txtTVSeasonPosterWidth.Enabled = false
        Me.txtTVSeasonPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVSeasonPosterWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtTVSeasonPosterWidth.Name = "txtTVSeasonPosterWidth"
        Me.txtTVSeasonPosterWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtTVSeasonPosterWidth.TabIndex = 5
        '
        'txtTVSeasonPosterHeight
        '
        Me.txtTVSeasonPosterHeight.Enabled = false
        Me.txtTVSeasonPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVSeasonPosterHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtTVSeasonPosterHeight.Name = "txtTVSeasonPosterHeight"
        Me.txtTVSeasonPosterHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtTVSeasonPosterHeight.TabIndex = 7
        '
        'lblTVSeasonPosterWidth
        '
        Me.lblTVSeasonPosterWidth.AutoSize = true
        Me.lblTVSeasonPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVSeasonPosterWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblTVSeasonPosterWidth.Name = "lblTVSeasonPosterWidth"
        Me.lblTVSeasonPosterWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblTVSeasonPosterWidth.TabIndex = 4
        Me.lblTVSeasonPosterWidth.Text = "Max Width:"
        '
        'lblTVSeasonPosterHeight
        '
        Me.lblTVSeasonPosterHeight.AutoSize = true
        Me.lblTVSeasonPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVSeasonPosterHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblTVSeasonPosterHeight.Name = "lblTVSeasonPosterHeight"
        Me.lblTVSeasonPosterHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblTVSeasonPosterHeight.TabIndex = 6
        Me.lblTVSeasonPosterHeight.Text = "Max Height:"
        '
        'chkTVSeasonPosterResize
        '
        Me.chkTVSeasonPosterResize.AutoSize = true
        Me.chkTVSeasonPosterResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVSeasonPosterResize.Location = New System.Drawing.Point(6, 69)
        Me.chkTVSeasonPosterResize.Name = "chkTVSeasonPosterResize"
        Me.chkTVSeasonPosterResize.Size = New System.Drawing.Size(133, 17)
        Me.chkTVSeasonPosterResize.TabIndex = 3
        Me.chkTVSeasonPosterResize.Text = "Automatically Resize:"
        Me.chkTVSeasonPosterResize.UseVisualStyleBackColor = true
        '
        'lblTVSeasonPosterSize
        '
        Me.lblTVSeasonPosterSize.AutoSize = true
        Me.lblTVSeasonPosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVSeasonPosterSize.Location = New System.Drawing.Point(3, 14)
        Me.lblTVSeasonPosterSize.Name = "lblTVSeasonPosterSize"
        Me.lblTVSeasonPosterSize.Size = New System.Drawing.Size(80, 13)
        Me.lblTVSeasonPosterSize.TabIndex = 0
        Me.lblTVSeasonPosterSize.Text = "Preferred Size:"
        '
        'cbTVSeasonPosterPrefSize
        '
        Me.cbTVSeasonPosterPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVSeasonPosterPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVSeasonPosterPrefSize.FormattingEnabled = true
        Me.cbTVSeasonPosterPrefSize.Location = New System.Drawing.Point(6, 29)
        Me.cbTVSeasonPosterPrefSize.Name = "cbTVSeasonPosterPrefSize"
        Me.cbTVSeasonPosterPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbTVSeasonPosterPrefSize.TabIndex = 1
        '
        'chkTVSeasonPosterOverwrite
        '
        Me.chkTVSeasonPosterOverwrite.AutoSize = true
        Me.chkTVSeasonPosterOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVSeasonPosterOverwrite.Location = New System.Drawing.Point(6, 53)
        Me.chkTVSeasonPosterOverwrite.Name = "chkTVSeasonPosterOverwrite"
        Me.chkTVSeasonPosterOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVSeasonPosterOverwrite.TabIndex = 2
        Me.chkTVSeasonPosterOverwrite.Text = "Overwrite Existing"
        Me.chkTVSeasonPosterOverwrite.UseVisualStyleBackColor = true
        '
        'gbTVSeasonFanartOpts
        '
        Me.gbTVSeasonFanartOpts.Controls.Add(Me.txtTVSeasonFanartWidth)
        Me.gbTVSeasonFanartOpts.Controls.Add(Me.txtTVSeasonFanartHeight)
        Me.gbTVSeasonFanartOpts.Controls.Add(Me.lblTVSeasonFanartWidth)
        Me.gbTVSeasonFanartOpts.Controls.Add(Me.lblTVSeasonFanartHeight)
        Me.gbTVSeasonFanartOpts.Controls.Add(Me.chkTVSeasonFanartResize)
        Me.gbTVSeasonFanartOpts.Controls.Add(Me.cbTVSeasonFanartPrefSize)
        Me.gbTVSeasonFanartOpts.Controls.Add(Me.lblTVSeasonFanartSize)
        Me.gbTVSeasonFanartOpts.Controls.Add(Me.chkTVSeasonFanartOverwrite)
        Me.gbTVSeasonFanartOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVSeasonFanartOpts.Location = New System.Drawing.Point(474, 6)
        Me.gbTVSeasonFanartOpts.Name = "gbTVSeasonFanartOpts"
        Me.gbTVSeasonFanartOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbTVSeasonFanartOpts.TabIndex = 1
        Me.gbTVSeasonFanartOpts.TabStop = false
        Me.gbTVSeasonFanartOpts.Text = "Fanart"
        '
        'txtTVSeasonFanartWidth
        '
        Me.txtTVSeasonFanartWidth.Enabled = false
        Me.txtTVSeasonFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVSeasonFanartWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtTVSeasonFanartWidth.Name = "txtTVSeasonFanartWidth"
        Me.txtTVSeasonFanartWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtTVSeasonFanartWidth.TabIndex = 5
        '
        'txtTVSeasonFanartHeight
        '
        Me.txtTVSeasonFanartHeight.Enabled = false
        Me.txtTVSeasonFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVSeasonFanartHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtTVSeasonFanartHeight.Name = "txtTVSeasonFanartHeight"
        Me.txtTVSeasonFanartHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtTVSeasonFanartHeight.TabIndex = 7
        '
        'lblTVSeasonFanartWidth
        '
        Me.lblTVSeasonFanartWidth.AutoSize = true
        Me.lblTVSeasonFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVSeasonFanartWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblTVSeasonFanartWidth.Name = "lblTVSeasonFanartWidth"
        Me.lblTVSeasonFanartWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblTVSeasonFanartWidth.TabIndex = 4
        Me.lblTVSeasonFanartWidth.Text = "Max Width:"
        '
        'lblTVSeasonFanartHeight
        '
        Me.lblTVSeasonFanartHeight.AutoSize = true
        Me.lblTVSeasonFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVSeasonFanartHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblTVSeasonFanartHeight.Name = "lblTVSeasonFanartHeight"
        Me.lblTVSeasonFanartHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblTVSeasonFanartHeight.TabIndex = 6
        Me.lblTVSeasonFanartHeight.Text = "Max Height:"
        '
        'chkTVSeasonFanartResize
        '
        Me.chkTVSeasonFanartResize.AutoSize = true
        Me.chkTVSeasonFanartResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVSeasonFanartResize.Location = New System.Drawing.Point(6, 69)
        Me.chkTVSeasonFanartResize.Name = "chkTVSeasonFanartResize"
        Me.chkTVSeasonFanartResize.Size = New System.Drawing.Size(133, 17)
        Me.chkTVSeasonFanartResize.TabIndex = 3
        Me.chkTVSeasonFanartResize.Text = "Automatically Resize:"
        Me.chkTVSeasonFanartResize.UseVisualStyleBackColor = true
        '
        'cbTVSeasonFanartPrefSize
        '
        Me.cbTVSeasonFanartPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVSeasonFanartPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVSeasonFanartPrefSize.FormattingEnabled = true
        Me.cbTVSeasonFanartPrefSize.Location = New System.Drawing.Point(6, 29)
        Me.cbTVSeasonFanartPrefSize.Name = "cbTVSeasonFanartPrefSize"
        Me.cbTVSeasonFanartPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbTVSeasonFanartPrefSize.TabIndex = 1
        '
        'lblTVSeasonFanartSize
        '
        Me.lblTVSeasonFanartSize.AutoSize = true
        Me.lblTVSeasonFanartSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVSeasonFanartSize.Location = New System.Drawing.Point(3, 14)
        Me.lblTVSeasonFanartSize.Name = "lblTVSeasonFanartSize"
        Me.lblTVSeasonFanartSize.Size = New System.Drawing.Size(80, 13)
        Me.lblTVSeasonFanartSize.TabIndex = 0
        Me.lblTVSeasonFanartSize.Text = "Preferred Size:"
        '
        'chkTVSeasonFanartOverwrite
        '
        Me.chkTVSeasonFanartOverwrite.AutoSize = true
        Me.chkTVSeasonFanartOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVSeasonFanartOverwrite.Location = New System.Drawing.Point(6, 53)
        Me.chkTVSeasonFanartOverwrite.Name = "chkTVSeasonFanartOverwrite"
        Me.chkTVSeasonFanartOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVSeasonFanartOverwrite.TabIndex = 2
        Me.chkTVSeasonFanartOverwrite.Text = "Overwrite Existing"
        Me.chkTVSeasonFanartOverwrite.UseVisualStyleBackColor = true
        '
        'tpTVEpisode
        '
        Me.tpTVEpisode.Controls.Add(Me.gbTVEpisodePosterOpts)
        Me.tpTVEpisode.Controls.Add(Me.gbTVEpisodeFanartOpts)
        Me.tpTVEpisode.Location = New System.Drawing.Point(4, 22)
        Me.tpTVEpisode.Name = "tpTVEpisode"
        Me.tpTVEpisode.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTVEpisode.Size = New System.Drawing.Size(734, 465)
        Me.tpTVEpisode.TabIndex = 1
        Me.tpTVEpisode.Text = "TV Episode"
        Me.tpTVEpisode.UseVisualStyleBackColor = true
        '
        'gbTVEpisodePosterOpts
        '
        Me.gbTVEpisodePosterOpts.Controls.Add(Me.lblTVEpisodePosterSize)
        Me.gbTVEpisodePosterOpts.Controls.Add(Me.cbTVEpisodePosterPrefSize)
        Me.gbTVEpisodePosterOpts.Controls.Add(Me.txtTVEpisodePosterWidth)
        Me.gbTVEpisodePosterOpts.Controls.Add(Me.txtTVEpisodePosterHeight)
        Me.gbTVEpisodePosterOpts.Controls.Add(Me.lblTVEpisodePosterWidth)
        Me.gbTVEpisodePosterOpts.Controls.Add(Me.lblTVEpisodePosterHeight)
        Me.gbTVEpisodePosterOpts.Controls.Add(Me.chkTVEpisodePosterResize)
        Me.gbTVEpisodePosterOpts.Controls.Add(Me.chkTVEpisodePosterOverwrite)
        Me.gbTVEpisodePosterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVEpisodePosterOpts.Location = New System.Drawing.Point(6, 6)
        Me.gbTVEpisodePosterOpts.Name = "gbTVEpisodePosterOpts"
        Me.gbTVEpisodePosterOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbTVEpisodePosterOpts.TabIndex = 0
        Me.gbTVEpisodePosterOpts.TabStop = false
        Me.gbTVEpisodePosterOpts.Text = "Poster"
        '
        'lblTVEpisodePosterSize
        '
        Me.lblTVEpisodePosterSize.AutoSize = true
        Me.lblTVEpisodePosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVEpisodePosterSize.Location = New System.Drawing.Point(3, 14)
        Me.lblTVEpisodePosterSize.Name = "lblTVEpisodePosterSize"
        Me.lblTVEpisodePosterSize.Size = New System.Drawing.Size(80, 13)
        Me.lblTVEpisodePosterSize.TabIndex = 10
        Me.lblTVEpisodePosterSize.Text = "Preferred Size:"
        '
        'cbTVEpisodePosterPrefSize
        '
        Me.cbTVEpisodePosterPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVEpisodePosterPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVEpisodePosterPrefSize.FormattingEnabled = true
        Me.cbTVEpisodePosterPrefSize.Location = New System.Drawing.Point(6, 29)
        Me.cbTVEpisodePosterPrefSize.Name = "cbTVEpisodePosterPrefSize"
        Me.cbTVEpisodePosterPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbTVEpisodePosterPrefSize.TabIndex = 9
        '
        'txtTVEpisodePosterWidth
        '
        Me.txtTVEpisodePosterWidth.Enabled = false
        Me.txtTVEpisodePosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVEpisodePosterWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtTVEpisodePosterWidth.Name = "txtTVEpisodePosterWidth"
        Me.txtTVEpisodePosterWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtTVEpisodePosterWidth.TabIndex = 3
        '
        'txtTVEpisodePosterHeight
        '
        Me.txtTVEpisodePosterHeight.Enabled = false
        Me.txtTVEpisodePosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVEpisodePosterHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtTVEpisodePosterHeight.Name = "txtTVEpisodePosterHeight"
        Me.txtTVEpisodePosterHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtTVEpisodePosterHeight.TabIndex = 5
        '
        'lblTVEpisodePosterWidth
        '
        Me.lblTVEpisodePosterWidth.AutoSize = true
        Me.lblTVEpisodePosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVEpisodePosterWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblTVEpisodePosterWidth.Name = "lblTVEpisodePosterWidth"
        Me.lblTVEpisodePosterWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblTVEpisodePosterWidth.TabIndex = 2
        Me.lblTVEpisodePosterWidth.Text = "Max Width:"
        '
        'lblTVEpisodePosterHeight
        '
        Me.lblTVEpisodePosterHeight.AutoSize = true
        Me.lblTVEpisodePosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVEpisodePosterHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblTVEpisodePosterHeight.Name = "lblTVEpisodePosterHeight"
        Me.lblTVEpisodePosterHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblTVEpisodePosterHeight.TabIndex = 4
        Me.lblTVEpisodePosterHeight.Text = "Max Height:"
        '
        'chkTVEpisodePosterResize
        '
        Me.chkTVEpisodePosterResize.AutoSize = true
        Me.chkTVEpisodePosterResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVEpisodePosterResize.Location = New System.Drawing.Point(6, 69)
        Me.chkTVEpisodePosterResize.Name = "chkTVEpisodePosterResize"
        Me.chkTVEpisodePosterResize.Size = New System.Drawing.Size(133, 17)
        Me.chkTVEpisodePosterResize.TabIndex = 1
        Me.chkTVEpisodePosterResize.Text = "Automatically Resize:"
        Me.chkTVEpisodePosterResize.UseVisualStyleBackColor = true
        '
        'chkTVEpisodePosterOverwrite
        '
        Me.chkTVEpisodePosterOverwrite.AutoSize = true
        Me.chkTVEpisodePosterOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVEpisodePosterOverwrite.Location = New System.Drawing.Point(6, 53)
        Me.chkTVEpisodePosterOverwrite.Name = "chkTVEpisodePosterOverwrite"
        Me.chkTVEpisodePosterOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVEpisodePosterOverwrite.TabIndex = 0
        Me.chkTVEpisodePosterOverwrite.Text = "Overwrite Existing"
        Me.chkTVEpisodePosterOverwrite.UseVisualStyleBackColor = true
        '
        'gbTVEpisodeFanartOpts
        '
        Me.gbTVEpisodeFanartOpts.Controls.Add(Me.txtTVEpisodeFanartWidth)
        Me.gbTVEpisodeFanartOpts.Controls.Add(Me.txtTVEpisodeFanartHeight)
        Me.gbTVEpisodeFanartOpts.Controls.Add(Me.lblTVEpisodeFanartWidth)
        Me.gbTVEpisodeFanartOpts.Controls.Add(Me.lblTVEpisodeFanartHeight)
        Me.gbTVEpisodeFanartOpts.Controls.Add(Me.chkTVEpisodeFanartResize)
        Me.gbTVEpisodeFanartOpts.Controls.Add(Me.cbTVEpisodeFanartPrefSize)
        Me.gbTVEpisodeFanartOpts.Controls.Add(Me.lblTVEpisodeFanartSize)
        Me.gbTVEpisodeFanartOpts.Controls.Add(Me.chkTVEpisodeFanartOverwrite)
        Me.gbTVEpisodeFanartOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVEpisodeFanartOpts.Location = New System.Drawing.Point(240, 6)
        Me.gbTVEpisodeFanartOpts.Name = "gbTVEpisodeFanartOpts"
        Me.gbTVEpisodeFanartOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbTVEpisodeFanartOpts.TabIndex = 1
        Me.gbTVEpisodeFanartOpts.TabStop = false
        Me.gbTVEpisodeFanartOpts.Text = "Fanart"
        '
        'txtTVEpisodeFanartWidth
        '
        Me.txtTVEpisodeFanartWidth.Enabled = false
        Me.txtTVEpisodeFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVEpisodeFanartWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtTVEpisodeFanartWidth.Name = "txtTVEpisodeFanartWidth"
        Me.txtTVEpisodeFanartWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtTVEpisodeFanartWidth.TabIndex = 5
        '
        'txtTVEpisodeFanartHeight
        '
        Me.txtTVEpisodeFanartHeight.Enabled = false
        Me.txtTVEpisodeFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVEpisodeFanartHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtTVEpisodeFanartHeight.Name = "txtTVEpisodeFanartHeight"
        Me.txtTVEpisodeFanartHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtTVEpisodeFanartHeight.TabIndex = 7
        '
        'lblTVEpisodeFanartWidth
        '
        Me.lblTVEpisodeFanartWidth.AutoSize = true
        Me.lblTVEpisodeFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVEpisodeFanartWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblTVEpisodeFanartWidth.Name = "lblTVEpisodeFanartWidth"
        Me.lblTVEpisodeFanartWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblTVEpisodeFanartWidth.TabIndex = 4
        Me.lblTVEpisodeFanartWidth.Text = "Max Width:"
        '
        'lblTVEpisodeFanartHeight
        '
        Me.lblTVEpisodeFanartHeight.AutoSize = true
        Me.lblTVEpisodeFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVEpisodeFanartHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblTVEpisodeFanartHeight.Name = "lblTVEpisodeFanartHeight"
        Me.lblTVEpisodeFanartHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblTVEpisodeFanartHeight.TabIndex = 6
        Me.lblTVEpisodeFanartHeight.Text = "Max Height:"
        '
        'chkTVEpisodeFanartResize
        '
        Me.chkTVEpisodeFanartResize.AutoSize = true
        Me.chkTVEpisodeFanartResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVEpisodeFanartResize.Location = New System.Drawing.Point(6, 69)
        Me.chkTVEpisodeFanartResize.Name = "chkTVEpisodeFanartResize"
        Me.chkTVEpisodeFanartResize.Size = New System.Drawing.Size(133, 17)
        Me.chkTVEpisodeFanartResize.TabIndex = 3
        Me.chkTVEpisodeFanartResize.Text = "Automatically Resize:"
        Me.chkTVEpisodeFanartResize.UseVisualStyleBackColor = true
        '
        'cbTVEpisodeFanartPrefSize
        '
        Me.cbTVEpisodeFanartPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVEpisodeFanartPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVEpisodeFanartPrefSize.FormattingEnabled = true
        Me.cbTVEpisodeFanartPrefSize.Location = New System.Drawing.Point(6, 29)
        Me.cbTVEpisodeFanartPrefSize.Name = "cbTVEpisodeFanartPrefSize"
        Me.cbTVEpisodeFanartPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbTVEpisodeFanartPrefSize.TabIndex = 1
        '
        'lblTVEpisodeFanartSize
        '
        Me.lblTVEpisodeFanartSize.AutoSize = true
        Me.lblTVEpisodeFanartSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVEpisodeFanartSize.Location = New System.Drawing.Point(3, 14)
        Me.lblTVEpisodeFanartSize.Name = "lblTVEpisodeFanartSize"
        Me.lblTVEpisodeFanartSize.Size = New System.Drawing.Size(80, 13)
        Me.lblTVEpisodeFanartSize.TabIndex = 0
        Me.lblTVEpisodeFanartSize.Text = "Preferred Size:"
        '
        'chkTVEpisodeFanartOverwrite
        '
        Me.chkTVEpisodeFanartOverwrite.AutoSize = true
        Me.chkTVEpisodeFanartOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVEpisodeFanartOverwrite.Location = New System.Drawing.Point(6, 53)
        Me.chkTVEpisodeFanartOverwrite.Name = "chkTVEpisodeFanartOverwrite"
        Me.chkTVEpisodeFanartOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkTVEpisodeFanartOverwrite.TabIndex = 2
        Me.chkTVEpisodeFanartOverwrite.Text = "Overwrite Existing"
        Me.chkTVEpisodeFanartOverwrite.UseVisualStyleBackColor = true
        '
        'pnlTVScraper
        '
        Me.pnlTVScraper.BackColor = System.Drawing.Color.White
        Me.pnlTVScraper.Controls.Add(Me.gbTVScraperMiscOpts)
        Me.pnlTVScraper.Controls.Add(Me.gbTVScraperFieldsOpts)
        Me.pnlTVScraper.Controls.Add(Me.gbTVScraperGlobalLocksOpts)
        Me.pnlTVScraper.Controls.Add(Me.gbTVScraperMetaDataOpts)
        Me.pnlTVScraper.Controls.Add(Me.gbTVScraperOptionsOpts)
        Me.pnlTVScraper.Location = New System.Drawing.Point(900, 900)
        Me.pnlTVScraper.Name = "pnlTVScraper"
        Me.pnlTVScraper.Size = New System.Drawing.Size(750, 500)
        Me.pnlTVScraper.TabIndex = 19
        Me.pnlTVScraper.Visible = false
        '
        'gbTVScraperMiscOpts
        '
        Me.gbTVScraperMiscOpts.Controls.Add(Me.chkTVScraperUseSRuntimeForEp)
        Me.gbTVScraperMiscOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbTVScraperMiscOpts.Location = New System.Drawing.Point(3, 439)
        Me.gbTVScraperMiscOpts.Name = "gbTVScraperMiscOpts"
        Me.gbTVScraperMiscOpts.Size = New System.Drawing.Size(403, 49)
        Me.gbTVScraperMiscOpts.TabIndex = 5
        Me.gbTVScraperMiscOpts.TabStop = false
        Me.gbTVScraperMiscOpts.Text = "Miscellaneous"
        '
        'chkTVScraperUseSRuntimeForEp
        '
        Me.chkTVScraperUseSRuntimeForEp.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperUseSRuntimeForEp.Location = New System.Drawing.Point(9, 21)
        Me.chkTVScraperUseSRuntimeForEp.Name = "chkTVScraperUseSRuntimeForEp"
        Me.chkTVScraperUseSRuntimeForEp.Size = New System.Drawing.Size(388, 17)
        Me.chkTVScraperUseSRuntimeForEp.TabIndex = 1
        Me.chkTVScraperUseSRuntimeForEp.Text = "Use Show Runtime for Episodes if no Episode Runtime can be found"
        Me.chkTVScraperUseSRuntimeForEp.UseVisualStyleBackColor = true
        '
        'gbTVScraperFieldsOpts
        '
        Me.gbTVScraperFieldsOpts.Controls.Add(Me.gbTVScraperFieldsShowOpts)
        Me.gbTVScraperFieldsOpts.Controls.Add(Me.gbTVScraperFieldsEpisodeOpts)
        Me.gbTVScraperFieldsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVScraperFieldsOpts.Location = New System.Drawing.Point(3, 295)
        Me.gbTVScraperFieldsOpts.Name = "gbTVScraperFieldsOpts"
        Me.gbTVScraperFieldsOpts.Size = New System.Drawing.Size(403, 137)
        Me.gbTVScraperFieldsOpts.TabIndex = 3
        Me.gbTVScraperFieldsOpts.TabStop = false
        Me.gbTVScraperFieldsOpts.Text = "Scraper Fields"
        '
        'gbTVScraperFieldsShowOpts
        '
        Me.gbTVScraperFieldsShowOpts.Controls.Add(Me.chkTVScraperShowVotes)
        Me.gbTVScraperFieldsShowOpts.Controls.Add(Me.chkTVScraperShowRuntime)
        Me.gbTVScraperFieldsShowOpts.Controls.Add(Me.chkTVScraperShowStatus)
        Me.gbTVScraperFieldsShowOpts.Controls.Add(Me.chkTVScraperShowRating)
        Me.gbTVScraperFieldsShowOpts.Controls.Add(Me.chkTVScraperShowActors)
        Me.gbTVScraperFieldsShowOpts.Controls.Add(Me.chkTVScraperShowStudio)
        Me.gbTVScraperFieldsShowOpts.Controls.Add(Me.chkTVScraperShowPremiered)
        Me.gbTVScraperFieldsShowOpts.Controls.Add(Me.chkTVScraperShowEpiGuideURL)
        Me.gbTVScraperFieldsShowOpts.Controls.Add(Me.chkTVScraperShowMPAA)
        Me.gbTVScraperFieldsShowOpts.Controls.Add(Me.chkTVScraperShowPlot)
        Me.gbTVScraperFieldsShowOpts.Controls.Add(Me.chkTVScraperShowGenre)
        Me.gbTVScraperFieldsShowOpts.Controls.Add(Me.chkTVScraperShowTitle)
        Me.gbTVScraperFieldsShowOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVScraperFieldsShowOpts.Location = New System.Drawing.Point(3, 14)
        Me.gbTVScraperFieldsShowOpts.Name = "gbTVScraperFieldsShowOpts"
        Me.gbTVScraperFieldsShowOpts.Size = New System.Drawing.Size(213, 117)
        Me.gbTVScraperFieldsShowOpts.TabIndex = 0
        Me.gbTVScraperFieldsShowOpts.TabStop = false
        Me.gbTVScraperFieldsShowOpts.Text = "Show"
        '
        'chkTVScraperShowVotes
        '
        Me.chkTVScraperShowVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperShowVotes.Location = New System.Drawing.Point(130, 94)
        Me.chkTVScraperShowVotes.Name = "chkTVScraperShowVotes"
        Me.chkTVScraperShowVotes.Size = New System.Drawing.Size(78, 17)
        Me.chkTVScraperShowVotes.TabIndex = 11
        Me.chkTVScraperShowVotes.Text = "Votes"
        Me.chkTVScraperShowVotes.UseVisualStyleBackColor = true
        '
        'chkTVScraperShowRuntime
        '
        Me.chkTVScraperShowRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperShowRuntime.Location = New System.Drawing.Point(6, 94)
        Me.chkTVScraperShowRuntime.Name = "chkTVScraperShowRuntime"
        Me.chkTVScraperShowRuntime.Size = New System.Drawing.Size(118, 17)
        Me.chkTVScraperShowRuntime.TabIndex = 10
        Me.chkTVScraperShowRuntime.Text = "Runtime"
        Me.chkTVScraperShowRuntime.UseVisualStyleBackColor = true
        '
        'chkTVScraperShowStatus
        '
        Me.chkTVScraperShowStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperShowStatus.Location = New System.Drawing.Point(130, 77)
        Me.chkTVScraperShowStatus.Name = "chkTVScraperShowStatus"
        Me.chkTVScraperShowStatus.Size = New System.Drawing.Size(78, 17)
        Me.chkTVScraperShowStatus.TabIndex = 9
        Me.chkTVScraperShowStatus.Text = "Status"
        Me.chkTVScraperShowStatus.UseVisualStyleBackColor = true
        '
        'chkTVScraperShowRating
        '
        Me.chkTVScraperShowRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperShowRating.Location = New System.Drawing.Point(130, 29)
        Me.chkTVScraperShowRating.Name = "chkTVScraperShowRating"
        Me.chkTVScraperShowRating.Size = New System.Drawing.Size(78, 17)
        Me.chkTVScraperShowRating.TabIndex = 6
        Me.chkTVScraperShowRating.Text = "Rating"
        Me.chkTVScraperShowRating.UseVisualStyleBackColor = true
        '
        'chkTVScraperShowActors
        '
        Me.chkTVScraperShowActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperShowActors.Location = New System.Drawing.Point(130, 61)
        Me.chkTVScraperShowActors.Name = "chkTVScraperShowActors"
        Me.chkTVScraperShowActors.Size = New System.Drawing.Size(78, 17)
        Me.chkTVScraperShowActors.TabIndex = 8
        Me.chkTVScraperShowActors.Text = "Actors"
        Me.chkTVScraperShowActors.UseVisualStyleBackColor = true
        '
        'chkTVScraperShowStudio
        '
        Me.chkTVScraperShowStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperShowStudio.Location = New System.Drawing.Point(130, 45)
        Me.chkTVScraperShowStudio.Name = "chkTVScraperShowStudio"
        Me.chkTVScraperShowStudio.Size = New System.Drawing.Size(78, 17)
        Me.chkTVScraperShowStudio.TabIndex = 7
        Me.chkTVScraperShowStudio.Text = "Studio"
        Me.chkTVScraperShowStudio.UseVisualStyleBackColor = true
        '
        'chkTVScraperShowPremiered
        '
        Me.chkTVScraperShowPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperShowPremiered.Location = New System.Drawing.Point(130, 13)
        Me.chkTVScraperShowPremiered.Name = "chkTVScraperShowPremiered"
        Me.chkTVScraperShowPremiered.Size = New System.Drawing.Size(78, 17)
        Me.chkTVScraperShowPremiered.TabIndex = 5
        Me.chkTVScraperShowPremiered.Text = "Premiered"
        Me.chkTVScraperShowPremiered.UseVisualStyleBackColor = true
        '
        'chkTVScraperShowEpiGuideURL
        '
        Me.chkTVScraperShowEpiGuideURL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperShowEpiGuideURL.Location = New System.Drawing.Point(6, 29)
        Me.chkTVScraperShowEpiGuideURL.Name = "chkTVScraperShowEpiGuideURL"
        Me.chkTVScraperShowEpiGuideURL.Size = New System.Drawing.Size(118, 17)
        Me.chkTVScraperShowEpiGuideURL.TabIndex = 1
        Me.chkTVScraperShowEpiGuideURL.Text = "EpisodeGuideURL"
        Me.chkTVScraperShowEpiGuideURL.UseVisualStyleBackColor = true
        '
        'chkTVScraperShowMPAA
        '
        Me.chkTVScraperShowMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperShowMPAA.Location = New System.Drawing.Point(6, 61)
        Me.chkTVScraperShowMPAA.Name = "chkTVScraperShowMPAA"
        Me.chkTVScraperShowMPAA.Size = New System.Drawing.Size(119, 17)
        Me.chkTVScraperShowMPAA.TabIndex = 3
        Me.chkTVScraperShowMPAA.Text = "MPAA"
        Me.chkTVScraperShowMPAA.UseVisualStyleBackColor = true
        '
        'chkTVScraperShowPlot
        '
        Me.chkTVScraperShowPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperShowPlot.Location = New System.Drawing.Point(6, 77)
        Me.chkTVScraperShowPlot.Name = "chkTVScraperShowPlot"
        Me.chkTVScraperShowPlot.Size = New System.Drawing.Size(119, 17)
        Me.chkTVScraperShowPlot.TabIndex = 4
        Me.chkTVScraperShowPlot.Text = "Plot"
        Me.chkTVScraperShowPlot.UseVisualStyleBackColor = true
        '
        'chkTVScraperShowGenre
        '
        Me.chkTVScraperShowGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperShowGenre.Location = New System.Drawing.Point(6, 45)
        Me.chkTVScraperShowGenre.Name = "chkTVScraperShowGenre"
        Me.chkTVScraperShowGenre.Size = New System.Drawing.Size(118, 17)
        Me.chkTVScraperShowGenre.TabIndex = 2
        Me.chkTVScraperShowGenre.Text = "Genre"
        Me.chkTVScraperShowGenre.UseVisualStyleBackColor = true
        '
        'chkTVScraperShowTitle
        '
        Me.chkTVScraperShowTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperShowTitle.Location = New System.Drawing.Point(6, 13)
        Me.chkTVScraperShowTitle.Name = "chkTVScraperShowTitle"
        Me.chkTVScraperShowTitle.Size = New System.Drawing.Size(118, 17)
        Me.chkTVScraperShowTitle.TabIndex = 0
        Me.chkTVScraperShowTitle.Text = "Title"
        Me.chkTVScraperShowTitle.UseVisualStyleBackColor = true
        '
        'gbTVScraperFieldsEpisodeOpts
        '
        Me.gbTVScraperFieldsEpisodeOpts.Controls.Add(Me.chkTVScraperEpisodeVotes)
        Me.gbTVScraperFieldsEpisodeOpts.Controls.Add(Me.chkTVScraperEpisodeRuntime)
        Me.gbTVScraperFieldsEpisodeOpts.Controls.Add(Me.chkTVScraperEpisodeActors)
        Me.gbTVScraperFieldsEpisodeOpts.Controls.Add(Me.chkTVScraperEpisodeCredits)
        Me.gbTVScraperFieldsEpisodeOpts.Controls.Add(Me.chkTVScraperEpisodeDirector)
        Me.gbTVScraperFieldsEpisodeOpts.Controls.Add(Me.chkTVScraperEpisodePlot)
        Me.gbTVScraperFieldsEpisodeOpts.Controls.Add(Me.chkTVScraperEpisodeRating)
        Me.gbTVScraperFieldsEpisodeOpts.Controls.Add(Me.chkTVScraperEpisodeAired)
        Me.gbTVScraperFieldsEpisodeOpts.Controls.Add(Me.chkTVScraperEpisodeTitle)
        Me.gbTVScraperFieldsEpisodeOpts.Controls.Add(Me.chkTVScraperEpisodeEpisode)
        Me.gbTVScraperFieldsEpisodeOpts.Controls.Add(Me.chkTVScraperEpisodeSeason)
        Me.gbTVScraperFieldsEpisodeOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVScraperFieldsEpisodeOpts.Location = New System.Drawing.Point(219, 14)
        Me.gbTVScraperFieldsEpisodeOpts.Name = "gbTVScraperFieldsEpisodeOpts"
        Me.gbTVScraperFieldsEpisodeOpts.Size = New System.Drawing.Size(181, 117)
        Me.gbTVScraperFieldsEpisodeOpts.TabIndex = 1
        Me.gbTVScraperFieldsEpisodeOpts.TabStop = false
        Me.gbTVScraperFieldsEpisodeOpts.Text = "Episode"
        '
        'chkTVScraperEpisodeVotes
        '
        Me.chkTVScraperEpisodeVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperEpisodeVotes.Location = New System.Drawing.Point(94, 77)
        Me.chkTVScraperEpisodeVotes.Name = "chkTVScraperEpisodeVotes"
        Me.chkTVScraperEpisodeVotes.Size = New System.Drawing.Size(81, 17)
        Me.chkTVScraperEpisodeVotes.TabIndex = 10
        Me.chkTVScraperEpisodeVotes.Text = "Votes"
        Me.chkTVScraperEpisodeVotes.UseVisualStyleBackColor = true
        '
        'chkTVScraperEpisodeRuntime
        '
        Me.chkTVScraperEpisodeRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperEpisodeRuntime.Location = New System.Drawing.Point(6, 94)
        Me.chkTVScraperEpisodeRuntime.Name = "chkTVScraperEpisodeRuntime"
        Me.chkTVScraperEpisodeRuntime.Size = New System.Drawing.Size(82, 17)
        Me.chkTVScraperEpisodeRuntime.TabIndex = 9
        Me.chkTVScraperEpisodeRuntime.Text = "Runtime"
        Me.chkTVScraperEpisodeRuntime.UseVisualStyleBackColor = true
        '
        'chkTVScraperEpisodeActors
        '
        Me.chkTVScraperEpisodeActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperEpisodeActors.Location = New System.Drawing.Point(94, 60)
        Me.chkTVScraperEpisodeActors.Name = "chkTVScraperEpisodeActors"
        Me.chkTVScraperEpisodeActors.Size = New System.Drawing.Size(81, 17)
        Me.chkTVScraperEpisodeActors.TabIndex = 0
        Me.chkTVScraperEpisodeActors.Text = "Actors"
        Me.chkTVScraperEpisodeActors.UseVisualStyleBackColor = true
        '
        'chkTVScraperEpisodeCredits
        '
        Me.chkTVScraperEpisodeCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperEpisodeCredits.Location = New System.Drawing.Point(94, 44)
        Me.chkTVScraperEpisodeCredits.Name = "chkTVScraperEpisodeCredits"
        Me.chkTVScraperEpisodeCredits.Size = New System.Drawing.Size(81, 17)
        Me.chkTVScraperEpisodeCredits.TabIndex = 8
        Me.chkTVScraperEpisodeCredits.Text = "Credits"
        Me.chkTVScraperEpisodeCredits.UseVisualStyleBackColor = true
        '
        'chkTVScraperEpisodeDirector
        '
        Me.chkTVScraperEpisodeDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperEpisodeDirector.Location = New System.Drawing.Point(94, 28)
        Me.chkTVScraperEpisodeDirector.Name = "chkTVScraperEpisodeDirector"
        Me.chkTVScraperEpisodeDirector.Size = New System.Drawing.Size(81, 17)
        Me.chkTVScraperEpisodeDirector.TabIndex = 7
        Me.chkTVScraperEpisodeDirector.Text = "Director"
        Me.chkTVScraperEpisodeDirector.UseVisualStyleBackColor = true
        '
        'chkTVScraperEpisodePlot
        '
        Me.chkTVScraperEpisodePlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperEpisodePlot.Location = New System.Drawing.Point(94, 12)
        Me.chkTVScraperEpisodePlot.Name = "chkTVScraperEpisodePlot"
        Me.chkTVScraperEpisodePlot.Size = New System.Drawing.Size(81, 17)
        Me.chkTVScraperEpisodePlot.TabIndex = 6
        Me.chkTVScraperEpisodePlot.Text = "Plot"
        Me.chkTVScraperEpisodePlot.UseVisualStyleBackColor = true
        '
        'chkTVScraperEpisodeRating
        '
        Me.chkTVScraperEpisodeRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperEpisodeRating.Location = New System.Drawing.Point(6, 77)
        Me.chkTVScraperEpisodeRating.Name = "chkTVScraperEpisodeRating"
        Me.chkTVScraperEpisodeRating.Size = New System.Drawing.Size(82, 17)
        Me.chkTVScraperEpisodeRating.TabIndex = 5
        Me.chkTVScraperEpisodeRating.Text = "Rating"
        Me.chkTVScraperEpisodeRating.UseVisualStyleBackColor = true
        '
        'chkTVScraperEpisodeAired
        '
        Me.chkTVScraperEpisodeAired.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperEpisodeAired.Location = New System.Drawing.Point(6, 61)
        Me.chkTVScraperEpisodeAired.Name = "chkTVScraperEpisodeAired"
        Me.chkTVScraperEpisodeAired.Size = New System.Drawing.Size(82, 17)
        Me.chkTVScraperEpisodeAired.TabIndex = 4
        Me.chkTVScraperEpisodeAired.Text = "Aired"
        Me.chkTVScraperEpisodeAired.UseVisualStyleBackColor = true
        '
        'chkTVScraperEpisodeTitle
        '
        Me.chkTVScraperEpisodeTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperEpisodeTitle.Location = New System.Drawing.Point(6, 13)
        Me.chkTVScraperEpisodeTitle.Name = "chkTVScraperEpisodeTitle"
        Me.chkTVScraperEpisodeTitle.Size = New System.Drawing.Size(82, 17)
        Me.chkTVScraperEpisodeTitle.TabIndex = 0
        Me.chkTVScraperEpisodeTitle.Text = "Title"
        Me.chkTVScraperEpisodeTitle.UseVisualStyleBackColor = true
        '
        'chkTVScraperEpisodeEpisode
        '
        Me.chkTVScraperEpisodeEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperEpisodeEpisode.Location = New System.Drawing.Point(6, 45)
        Me.chkTVScraperEpisodeEpisode.Name = "chkTVScraperEpisodeEpisode"
        Me.chkTVScraperEpisodeEpisode.Size = New System.Drawing.Size(82, 17)
        Me.chkTVScraperEpisodeEpisode.TabIndex = 3
        Me.chkTVScraperEpisodeEpisode.Text = "Episode"
        Me.chkTVScraperEpisodeEpisode.UseVisualStyleBackColor = true
        '
        'chkTVScraperEpisodeSeason
        '
        Me.chkTVScraperEpisodeSeason.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperEpisodeSeason.Location = New System.Drawing.Point(6, 29)
        Me.chkTVScraperEpisodeSeason.Name = "chkTVScraperEpisodeSeason"
        Me.chkTVScraperEpisodeSeason.Size = New System.Drawing.Size(82, 17)
        Me.chkTVScraperEpisodeSeason.TabIndex = 2
        Me.chkTVScraperEpisodeSeason.Text = "Season"
        Me.chkTVScraperEpisodeSeason.UseVisualStyleBackColor = true
        '
        'gbTVScraperGlobalLocksOpts
        '
        Me.gbTVScraperGlobalLocksOpts.Controls.Add(Me.gbTVScraperGlobalLocksEpisodeOpts)
        Me.gbTVScraperGlobalLocksOpts.Controls.Add(Me.gbTVScraperGlobalLocksShowOpts)
        Me.gbTVScraperGlobalLocksOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVScraperGlobalLocksOpts.Location = New System.Drawing.Point(208, 6)
        Me.gbTVScraperGlobalLocksOpts.Name = "gbTVScraperGlobalLocksOpts"
        Me.gbTVScraperGlobalLocksOpts.Size = New System.Drawing.Size(198, 283)
        Me.gbTVScraperGlobalLocksOpts.TabIndex = 1
        Me.gbTVScraperGlobalLocksOpts.TabStop = false
        Me.gbTVScraperGlobalLocksOpts.Text = "Global Locks"
        '
        'gbTVScraperGlobalLocksEpisodeOpts
        '
        Me.gbTVScraperGlobalLocksEpisodeOpts.Controls.Add(Me.chkTVLockEpisodeVotes)
        Me.gbTVScraperGlobalLocksEpisodeOpts.Controls.Add(Me.chkTVLockEpisodeRuntime)
        Me.gbTVScraperGlobalLocksEpisodeOpts.Controls.Add(Me.chkTVLockEpisodeTitle)
        Me.gbTVScraperGlobalLocksEpisodeOpts.Controls.Add(Me.chkTVLockEpisodeRating)
        Me.gbTVScraperGlobalLocksEpisodeOpts.Controls.Add(Me.chkTVLockEpisodePlot)
        Me.gbTVScraperGlobalLocksEpisodeOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVScraperGlobalLocksEpisodeOpts.Location = New System.Drawing.Point(6, 172)
        Me.gbTVScraperGlobalLocksEpisodeOpts.Name = "gbTVScraperGlobalLocksEpisodeOpts"
        Me.gbTVScraperGlobalLocksEpisodeOpts.Size = New System.Drawing.Size(181, 105)
        Me.gbTVScraperGlobalLocksEpisodeOpts.TabIndex = 1
        Me.gbTVScraperGlobalLocksEpisodeOpts.TabStop = false
        Me.gbTVScraperGlobalLocksEpisodeOpts.Text = "Episode"
        '
        'chkTVLockEpisodeVotes
        '
        Me.chkTVLockEpisodeVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVLockEpisodeVotes.Location = New System.Drawing.Point(6, 79)
        Me.chkTVLockEpisodeVotes.Name = "chkTVLockEpisodeVotes"
        Me.chkTVLockEpisodeVotes.Size = New System.Drawing.Size(168, 17)
        Me.chkTVLockEpisodeVotes.TabIndex = 4
        Me.chkTVLockEpisodeVotes.Text = "Lock Votes"
        Me.chkTVLockEpisodeVotes.UseVisualStyleBackColor = true
        '
        'chkTVLockEpisodeRuntime
        '
        Me.chkTVLockEpisodeRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVLockEpisodeRuntime.Location = New System.Drawing.Point(6, 63)
        Me.chkTVLockEpisodeRuntime.Name = "chkTVLockEpisodeRuntime"
        Me.chkTVLockEpisodeRuntime.Size = New System.Drawing.Size(168, 17)
        Me.chkTVLockEpisodeRuntime.TabIndex = 3
        Me.chkTVLockEpisodeRuntime.Text = "Lock Runtime"
        Me.chkTVLockEpisodeRuntime.UseVisualStyleBackColor = true
        '
        'chkTVLockEpisodeTitle
        '
        Me.chkTVLockEpisodeTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVLockEpisodeTitle.Location = New System.Drawing.Point(6, 15)
        Me.chkTVLockEpisodeTitle.Name = "chkTVLockEpisodeTitle"
        Me.chkTVLockEpisodeTitle.Size = New System.Drawing.Size(166, 17)
        Me.chkTVLockEpisodeTitle.TabIndex = 0
        Me.chkTVLockEpisodeTitle.Text = "Lock Title"
        Me.chkTVLockEpisodeTitle.UseVisualStyleBackColor = true
        '
        'chkTVLockEpisodeRating
        '
        Me.chkTVLockEpisodeRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVLockEpisodeRating.Location = New System.Drawing.Point(6, 47)
        Me.chkTVLockEpisodeRating.Name = "chkTVLockEpisodeRating"
        Me.chkTVLockEpisodeRating.Size = New System.Drawing.Size(168, 17)
        Me.chkTVLockEpisodeRating.TabIndex = 2
        Me.chkTVLockEpisodeRating.Text = "Lock Rating"
        Me.chkTVLockEpisodeRating.UseVisualStyleBackColor = true
        '
        'chkTVLockEpisodePlot
        '
        Me.chkTVLockEpisodePlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVLockEpisodePlot.Location = New System.Drawing.Point(6, 31)
        Me.chkTVLockEpisodePlot.Name = "chkTVLockEpisodePlot"
        Me.chkTVLockEpisodePlot.Size = New System.Drawing.Size(166, 17)
        Me.chkTVLockEpisodePlot.TabIndex = 1
        Me.chkTVLockEpisodePlot.Text = "Lock Plot"
        Me.chkTVLockEpisodePlot.UseVisualStyleBackColor = true
        '
        'gbTVScraperGlobalLocksShowOpts
        '
        Me.gbTVScraperGlobalLocksShowOpts.Controls.Add(Me.chkTVLockShowVotes)
        Me.gbTVScraperGlobalLocksShowOpts.Controls.Add(Me.chkTVLockShowRuntime)
        Me.gbTVScraperGlobalLocksShowOpts.Controls.Add(Me.chkTVLockShowStatus)
        Me.gbTVScraperGlobalLocksShowOpts.Controls.Add(Me.chkTVLockShowPlot)
        Me.gbTVScraperGlobalLocksShowOpts.Controls.Add(Me.chkTVLockShowGenre)
        Me.gbTVScraperGlobalLocksShowOpts.Controls.Add(Me.chkTVLockShowStudio)
        Me.gbTVScraperGlobalLocksShowOpts.Controls.Add(Me.chkTVLockShowRating)
        Me.gbTVScraperGlobalLocksShowOpts.Controls.Add(Me.chkTVLockShowTitle)
        Me.gbTVScraperGlobalLocksShowOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVScraperGlobalLocksShowOpts.Location = New System.Drawing.Point(5, 13)
        Me.gbTVScraperGlobalLocksShowOpts.Name = "gbTVScraperGlobalLocksShowOpts"
        Me.gbTVScraperGlobalLocksShowOpts.Size = New System.Drawing.Size(181, 153)
        Me.gbTVScraperGlobalLocksShowOpts.TabIndex = 0
        Me.gbTVScraperGlobalLocksShowOpts.TabStop = false
        Me.gbTVScraperGlobalLocksShowOpts.Text = "Show"
        '
        'chkTVLockShowVotes
        '
        Me.chkTVLockShowVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVLockShowVotes.Location = New System.Drawing.Point(6, 128)
        Me.chkTVLockShowVotes.Name = "chkTVLockShowVotes"
        Me.chkTVLockShowVotes.Size = New System.Drawing.Size(168, 17)
        Me.chkTVLockShowVotes.TabIndex = 7
        Me.chkTVLockShowVotes.Text = "Lock Votes"
        Me.chkTVLockShowVotes.UseVisualStyleBackColor = true
        '
        'chkTVLockShowRuntime
        '
        Me.chkTVLockShowRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVLockShowRuntime.Location = New System.Drawing.Point(6, 110)
        Me.chkTVLockShowRuntime.Name = "chkTVLockShowRuntime"
        Me.chkTVLockShowRuntime.Size = New System.Drawing.Size(168, 17)
        Me.chkTVLockShowRuntime.TabIndex = 6
        Me.chkTVLockShowRuntime.Text = "Lock Runtime"
        Me.chkTVLockShowRuntime.UseVisualStyleBackColor = true
        '
        'chkTVLockShowStatus
        '
        Me.chkTVLockShowStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVLockShowStatus.Location = New System.Drawing.Point(6, 77)
        Me.chkTVLockShowStatus.Name = "chkTVLockShowStatus"
        Me.chkTVLockShowStatus.Size = New System.Drawing.Size(168, 17)
        Me.chkTVLockShowStatus.TabIndex = 5
        Me.chkTVLockShowStatus.Text = "Lock Status"
        Me.chkTVLockShowStatus.UseVisualStyleBackColor = true
        '
        'chkTVLockShowPlot
        '
        Me.chkTVLockShowPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVLockShowPlot.Location = New System.Drawing.Point(6, 29)
        Me.chkTVLockShowPlot.Name = "chkTVLockShowPlot"
        Me.chkTVLockShowPlot.Size = New System.Drawing.Size(168, 17)
        Me.chkTVLockShowPlot.TabIndex = 1
        Me.chkTVLockShowPlot.Text = "Lock Plot"
        Me.chkTVLockShowPlot.UseVisualStyleBackColor = true
        '
        'chkTVLockShowGenre
        '
        Me.chkTVLockShowGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVLockShowGenre.Location = New System.Drawing.Point(6, 61)
        Me.chkTVLockShowGenre.Name = "chkTVLockShowGenre"
        Me.chkTVLockShowGenre.Size = New System.Drawing.Size(168, 17)
        Me.chkTVLockShowGenre.TabIndex = 3
        Me.chkTVLockShowGenre.Text = "Lock Genre"
        Me.chkTVLockShowGenre.UseVisualStyleBackColor = true
        '
        'chkTVLockShowStudio
        '
        Me.chkTVLockShowStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVLockShowStudio.Location = New System.Drawing.Point(6, 93)
        Me.chkTVLockShowStudio.Name = "chkTVLockShowStudio"
        Me.chkTVLockShowStudio.Size = New System.Drawing.Size(168, 17)
        Me.chkTVLockShowStudio.TabIndex = 4
        Me.chkTVLockShowStudio.Text = "Lock Studio"
        Me.chkTVLockShowStudio.UseVisualStyleBackColor = true
        '
        'chkTVLockShowRating
        '
        Me.chkTVLockShowRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVLockShowRating.Location = New System.Drawing.Point(6, 45)
        Me.chkTVLockShowRating.Name = "chkTVLockShowRating"
        Me.chkTVLockShowRating.Size = New System.Drawing.Size(168, 17)
        Me.chkTVLockShowRating.TabIndex = 2
        Me.chkTVLockShowRating.Text = "Lock Rating"
        Me.chkTVLockShowRating.UseVisualStyleBackColor = true
        '
        'chkTVLockShowTitle
        '
        Me.chkTVLockShowTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVLockShowTitle.Location = New System.Drawing.Point(6, 13)
        Me.chkTVLockShowTitle.Name = "chkTVLockShowTitle"
        Me.chkTVLockShowTitle.Size = New System.Drawing.Size(168, 17)
        Me.chkTVLockShowTitle.TabIndex = 0
        Me.chkTVLockShowTitle.Text = "Lock Title"
        Me.chkTVLockShowTitle.UseVisualStyleBackColor = true
        '
        'gbTVScraperMetaDataOpts
        '
        Me.gbTVScraperMetaDataOpts.Controls.Add(Me.gbTVScraperDefFIExtOpts)
        Me.gbTVScraperMetaDataOpts.Controls.Add(Me.gbTVScraperDurationOpts)
        Me.gbTVScraperMetaDataOpts.Controls.Add(Me.cbTVLanguageOverlay)
        Me.gbTVScraperMetaDataOpts.Controls.Add(Me.lblTVLanguageOverlay)
        Me.gbTVScraperMetaDataOpts.Controls.Add(Me.chkTVScraperMetaDataScan)
        Me.gbTVScraperMetaDataOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVScraperMetaDataOpts.Location = New System.Drawing.Point(412, 6)
        Me.gbTVScraperMetaDataOpts.Name = "gbTVScraperMetaDataOpts"
        Me.gbTVScraperMetaDataOpts.Size = New System.Drawing.Size(335, 386)
        Me.gbTVScraperMetaDataOpts.TabIndex = 2
        Me.gbTVScraperMetaDataOpts.TabStop = false
        Me.gbTVScraperMetaDataOpts.Text = "Meta Data"
        '
        'gbTVScraperDefFIExtOpts
        '
        Me.gbTVScraperDefFIExtOpts.Controls.Add(Me.lstTVScraperDefFIExt)
        Me.gbTVScraperDefFIExtOpts.Controls.Add(Me.txtTVScraperDefFIExt)
        Me.gbTVScraperDefFIExtOpts.Controls.Add(Me.lblTVScraperDefFIExt)
        Me.gbTVScraperDefFIExtOpts.Controls.Add(Me.btnTVScraperDefFIExtRemove)
        Me.gbTVScraperDefFIExtOpts.Controls.Add(Me.btnTVScraperDefFIExtEdit)
        Me.gbTVScraperDefFIExtOpts.Controls.Add(Me.btnTVScraperDefFIExtAdd)
        Me.gbTVScraperDefFIExtOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVScraperDefFIExtOpts.Location = New System.Drawing.Point(12, 93)
        Me.gbTVScraperDefFIExtOpts.Name = "gbTVScraperDefFIExtOpts"
        Me.gbTVScraperDefFIExtOpts.Size = New System.Drawing.Size(183, 144)
        Me.gbTVScraperDefFIExtOpts.TabIndex = 3
        Me.gbTVScraperDefFIExtOpts.TabStop = false
        Me.gbTVScraperDefFIExtOpts.Text = "Defaults by File Type"
        '
        'lstTVScraperDefFIExt
        '
        Me.lstTVScraperDefFIExt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstTVScraperDefFIExt.FormattingEnabled = true
        Me.lstTVScraperDefFIExt.Location = New System.Drawing.Point(10, 16)
        Me.lstTVScraperDefFIExt.Name = "lstTVScraperDefFIExt"
        Me.lstTVScraperDefFIExt.Size = New System.Drawing.Size(165, 95)
        Me.lstTVScraperDefFIExt.TabIndex = 0
        '
        'txtTVScraperDefFIExt
        '
        Me.txtTVScraperDefFIExt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTVScraperDefFIExt.Location = New System.Drawing.Point(73, 116)
        Me.txtTVScraperDefFIExt.Name = "txtTVScraperDefFIExt"
        Me.txtTVScraperDefFIExt.Size = New System.Drawing.Size(35, 22)
        Me.txtTVScraperDefFIExt.TabIndex = 2
        '
        'lblTVScraperDefFIExt
        '
        Me.lblTVScraperDefFIExt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVScraperDefFIExt.Location = New System.Drawing.Point(8, 116)
        Me.lblTVScraperDefFIExt.Name = "lblTVScraperDefFIExt"
        Me.lblTVScraperDefFIExt.Size = New System.Drawing.Size(66, 19)
        Me.lblTVScraperDefFIExt.TabIndex = 1
        Me.lblTVScraperDefFIExt.Text = "File Type"
        Me.lblTVScraperDefFIExt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnTVScraperDefFIExtRemove
        '
        Me.btnTVScraperDefFIExtRemove.Enabled = false
        Me.btnTVScraperDefFIExtRemove.Image = CType(resources.GetObject("btnTVScraperDefFIExtRemove.Image"),System.Drawing.Image)
        Me.btnTVScraperDefFIExtRemove.Location = New System.Drawing.Point(153, 115)
        Me.btnTVScraperDefFIExtRemove.Name = "btnTVScraperDefFIExtRemove"
        Me.btnTVScraperDefFIExtRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnTVScraperDefFIExtRemove.TabIndex = 4
        Me.btnTVScraperDefFIExtRemove.UseVisualStyleBackColor = true
        '
        'btnTVScraperDefFIExtEdit
        '
        Me.btnTVScraperDefFIExtEdit.Enabled = false
        Me.btnTVScraperDefFIExtEdit.Image = CType(resources.GetObject("btnTVScraperDefFIExtEdit.Image"),System.Drawing.Image)
        Me.btnTVScraperDefFIExtEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVScraperDefFIExtEdit.Location = New System.Drawing.Point(130, 115)
        Me.btnTVScraperDefFIExtEdit.Name = "btnTVScraperDefFIExtEdit"
        Me.btnTVScraperDefFIExtEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnTVScraperDefFIExtEdit.TabIndex = 3
        Me.btnTVScraperDefFIExtEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVScraperDefFIExtEdit.UseVisualStyleBackColor = true
        '
        'btnTVScraperDefFIExtAdd
        '
        Me.btnTVScraperDefFIExtAdd.Enabled = false
        Me.btnTVScraperDefFIExtAdd.Image = CType(resources.GetObject("btnTVScraperDefFIExtAdd.Image"),System.Drawing.Image)
        Me.btnTVScraperDefFIExtAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVScraperDefFIExtAdd.Location = New System.Drawing.Point(108, 115)
        Me.btnTVScraperDefFIExtAdd.Name = "btnTVScraperDefFIExtAdd"
        Me.btnTVScraperDefFIExtAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnTVScraperDefFIExtAdd.TabIndex = 29
        Me.btnTVScraperDefFIExtAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVScraperDefFIExtAdd.UseVisualStyleBackColor = true
        '
        'gbTVScraperDurationOpts
        '
        Me.gbTVScraperDurationOpts.Controls.Add(Me.lblTVScraperDurationRuntimeFormat)
        Me.gbTVScraperDurationOpts.Controls.Add(Me.chkTVScraperUseMDDuration)
        Me.gbTVScraperDurationOpts.Controls.Add(Me.txtTVScraperDurationRuntimeFormat)
        Me.gbTVScraperDurationOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbTVScraperDurationOpts.Location = New System.Drawing.Point(12, 249)
        Me.gbTVScraperDurationOpts.Name = "gbTVScraperDurationOpts"
        Me.gbTVScraperDurationOpts.Size = New System.Drawing.Size(317, 91)
        Me.gbTVScraperDurationOpts.TabIndex = 4
        Me.gbTVScraperDurationOpts.TabStop = false
        Me.gbTVScraperDurationOpts.Text = "Duration Format"
        '
        'lblTVScraperDurationRuntimeFormat
        '
        Me.lblTVScraperDurationRuntimeFormat.Font = New System.Drawing.Font("Segoe UI", 7!)
        Me.lblTVScraperDurationRuntimeFormat.Location = New System.Drawing.Point(218, 21)
        Me.lblTVScraperDurationRuntimeFormat.Name = "lblTVScraperDurationRuntimeFormat"
        Me.lblTVScraperDurationRuntimeFormat.Size = New System.Drawing.Size(72, 50)
        Me.lblTVScraperDurationRuntimeFormat.TabIndex = 24
        Me.lblTVScraperDurationRuntimeFormat.Text = "<h>=Hours"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"<m>=Minutes"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"<s>=Seconds"
        Me.lblTVScraperDurationRuntimeFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkTVScraperUseMDDuration
        '
        Me.chkTVScraperUseMDDuration.AutoSize = true
        Me.chkTVScraperUseMDDuration.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperUseMDDuration.Location = New System.Drawing.Point(9, 21)
        Me.chkTVScraperUseMDDuration.Name = "chkTVScraperUseMDDuration"
        Me.chkTVScraperUseMDDuration.Size = New System.Drawing.Size(158, 17)
        Me.chkTVScraperUseMDDuration.TabIndex = 1
        Me.chkTVScraperUseMDDuration.Text = "Use Duration for Runtime"
        Me.chkTVScraperUseMDDuration.UseVisualStyleBackColor = true
        '
        'txtTVScraperDurationRuntimeFormat
        '
        Me.txtTVScraperDurationRuntimeFormat.Location = New System.Drawing.Point(9, 44)
        Me.txtTVScraperDurationRuntimeFormat.Name = "txtTVScraperDurationRuntimeFormat"
        Me.txtTVScraperDurationRuntimeFormat.Size = New System.Drawing.Size(188, 22)
        Me.txtTVScraperDurationRuntimeFormat.TabIndex = 0
        '
        'cbTVLanguageOverlay
        '
        Me.cbTVLanguageOverlay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVLanguageOverlay.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVLanguageOverlay.FormattingEnabled = true
        Me.cbTVLanguageOverlay.Location = New System.Drawing.Point(13, 62)
        Me.cbTVLanguageOverlay.Name = "cbTVLanguageOverlay"
        Me.cbTVLanguageOverlay.Size = New System.Drawing.Size(182, 21)
        Me.cbTVLanguageOverlay.Sorted = true
        Me.cbTVLanguageOverlay.TabIndex = 2
        '
        'lblTVLanguageOverlay
        '
        Me.lblTVLanguageOverlay.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVLanguageOverlay.Location = New System.Drawing.Point(4, 35)
        Me.lblTVLanguageOverlay.Name = "lblTVLanguageOverlay"
        Me.lblTVLanguageOverlay.Size = New System.Drawing.Size(202, 29)
        Me.lblTVLanguageOverlay.TabIndex = 1
        Me.lblTVLanguageOverlay.Text = "Display Overlay if Video Contains an Audio Stream With the Following Language:"
        Me.lblTVLanguageOverlay.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'chkTVScraperMetaDataScan
        '
        Me.chkTVScraperMetaDataScan.AutoSize = true
        Me.chkTVScraperMetaDataScan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkTVScraperMetaDataScan.Location = New System.Drawing.Point(5, 16)
        Me.chkTVScraperMetaDataScan.Name = "chkTVScraperMetaDataScan"
        Me.chkTVScraperMetaDataScan.Size = New System.Drawing.Size(106, 17)
        Me.chkTVScraperMetaDataScan.TabIndex = 0
        Me.chkTVScraperMetaDataScan.Text = "Scan Meta Data"
        Me.chkTVScraperMetaDataScan.UseVisualStyleBackColor = true
        '
        'gbTVScraperOptionsOpts
        '
        Me.gbTVScraperOptionsOpts.Controls.Add(Me.lblTVScraperRatingRegion)
        Me.gbTVScraperOptionsOpts.Controls.Add(Me.cbTVScraperRatingRegion)
        Me.gbTVScraperOptionsOpts.Controls.Add(Me.lblTVScraperOptionsOrdering)
        Me.gbTVScraperOptionsOpts.Controls.Add(Me.cbTVScraperOptionsOrdering)
        Me.gbTVScraperOptionsOpts.Controls.Add(Me.lblTVScraperUpdateTime)
        Me.gbTVScraperOptionsOpts.Controls.Add(Me.cbTVScraperUpdateTime)
        Me.gbTVScraperOptionsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbTVScraperOptionsOpts.Location = New System.Drawing.Point(5, 6)
        Me.gbTVScraperOptionsOpts.Name = "gbTVScraperOptionsOpts"
        Me.gbTVScraperOptionsOpts.Size = New System.Drawing.Size(200, 243)
        Me.gbTVScraperOptionsOpts.TabIndex = 0
        Me.gbTVScraperOptionsOpts.TabStop = false
        Me.gbTVScraperOptionsOpts.Text = "Options"
        '
        'lblTVScraperRatingRegion
        '
        Me.lblTVScraperRatingRegion.AutoSize = true
        Me.lblTVScraperRatingRegion.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVScraperRatingRegion.Location = New System.Drawing.Point(12, 163)
        Me.lblTVScraperRatingRegion.Name = "lblTVScraperRatingRegion"
        Me.lblTVScraperRatingRegion.Size = New System.Drawing.Size(99, 13)
        Me.lblTVScraperRatingRegion.TabIndex = 5
        Me.lblTVScraperRatingRegion.Text = "TV Rating Region:"
        '
        'cbTVScraperRatingRegion
        '
        Me.cbTVScraperRatingRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVScraperRatingRegion.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVScraperRatingRegion.FormattingEnabled = true
        Me.cbTVScraperRatingRegion.Location = New System.Drawing.Point(12, 178)
        Me.cbTVScraperRatingRegion.Name = "cbTVScraperRatingRegion"
        Me.cbTVScraperRatingRegion.Size = New System.Drawing.Size(163, 21)
        Me.cbTVScraperRatingRegion.TabIndex = 6
        '
        'lblTVScraperOptionsOrdering
        '
        Me.lblTVScraperOptionsOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblTVScraperOptionsOrdering.Location = New System.Drawing.Point(10, 73)
        Me.lblTVScraperOptionsOrdering.Name = "lblTVScraperOptionsOrdering"
        Me.lblTVScraperOptionsOrdering.Size = New System.Drawing.Size(177, 13)
        Me.lblTVScraperOptionsOrdering.TabIndex = 7
        Me.lblTVScraperOptionsOrdering.Text = "Default Episode Ordering:"
        Me.lblTVScraperOptionsOrdering.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cbTVScraperOptionsOrdering
        '
        Me.cbTVScraperOptionsOrdering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVScraperOptionsOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVScraperOptionsOrdering.FormattingEnabled = true
        Me.cbTVScraperOptionsOrdering.Location = New System.Drawing.Point(15, 89)
        Me.cbTVScraperOptionsOrdering.Name = "cbTVScraperOptionsOrdering"
        Me.cbTVScraperOptionsOrdering.Size = New System.Drawing.Size(166, 21)
        Me.cbTVScraperOptionsOrdering.TabIndex = 8
        '
        'lblTVScraperUpdateTime
        '
        Me.lblTVScraperUpdateTime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTVScraperUpdateTime.Location = New System.Drawing.Point(3, 18)
        Me.lblTVScraperUpdateTime.Name = "lblTVScraperUpdateTime"
        Me.lblTVScraperUpdateTime.Size = New System.Drawing.Size(190, 31)
        Me.lblTVScraperUpdateTime.TabIndex = 5
        Me.lblTVScraperUpdateTime.Text = "Re-download Show Information Every:"
        Me.lblTVScraperUpdateTime.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cbTVScraperUpdateTime
        '
        Me.cbTVScraperUpdateTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVScraperUpdateTime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVScraperUpdateTime.FormattingEnabled = true
        Me.cbTVScraperUpdateTime.Location = New System.Drawing.Point(15, 49)
        Me.cbTVScraperUpdateTime.Name = "cbTVScraperUpdateTime"
        Me.cbTVScraperUpdateTime.Size = New System.Drawing.Size(166, 21)
        Me.cbTVScraperUpdateTime.TabIndex = 6
        '
        'cbMovieScraperCertLang
        '
        Me.cbMovieScraperCertLang.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cbMovieScraperCertLang.DropDownHeight = 200
        Me.cbMovieScraperCertLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieScraperCertLang.DropDownWidth = 110
        Me.cbMovieScraperCertLang.Enabled = false
        Me.cbMovieScraperCertLang.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieScraperCertLang.FormattingEnabled = true
        Me.cbMovieScraperCertLang.IntegralHeight = false
        Me.cbMovieScraperCertLang.Items.AddRange(New Object() {"Argentina", "Australia", "Belgium", "Brazil", "Canada", "Finland", "France", "Germany", "Hong Kong", "Hungary", "Iceland", "Ireland", "Netherlands", "New Zealand", "Peru", "Poland", "Portugal", "Serbia", "Singapore", "South Korea", "Spain", "Sweden", "Switzerland", "Turkey", "UK", "USA"})
        Me.cbMovieScraperCertLang.Location = New System.Drawing.Point(164, 246)
        Me.cbMovieScraperCertLang.Name = "cbMovieScraperCertLang"
        Me.cbMovieScraperCertLang.Size = New System.Drawing.Size(70, 21)
        Me.cbMovieScraperCertLang.Sorted = true
        Me.cbMovieScraperCertLang.TabIndex = 5
        '
        'lblMovieScraperOutlineLimit
        '
        Me.lblMovieScraperOutlineLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieScraperOutlineLimit.Location = New System.Drawing.Point(287, 76)
        Me.lblMovieScraperOutlineLimit.Name = "lblMovieScraperOutlineLimit"
        Me.lblMovieScraperOutlineLimit.Size = New System.Drawing.Size(34, 17)
        Me.lblMovieScraperOutlineLimit.TabIndex = 70
        Me.lblMovieScraperOutlineLimit.Text = "Limit:"
        Me.lblMovieScraperOutlineLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMovieScraperOutlineLimit
        '
        Me.txtMovieScraperOutlineLimit.Enabled = false
        Me.txtMovieScraperOutlineLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieScraperOutlineLimit.Location = New System.Drawing.Point(327, 78)
        Me.txtMovieScraperOutlineLimit.Name = "txtMovieScraperOutlineLimit"
        Me.txtMovieScraperOutlineLimit.Size = New System.Drawing.Size(54, 22)
        Me.txtMovieScraperOutlineLimit.TabIndex = 69
        '
        'gbMovieScraperMetaDataOpts
        '
        Me.gbMovieScraperMetaDataOpts.Controls.Add(Me.gbMovieScraperDefFIExtOpts)
        Me.gbMovieScraperMetaDataOpts.Controls.Add(Me.chkMovieScraperMetaDataIFOScan)
        Me.gbMovieScraperMetaDataOpts.Controls.Add(Me.cbMovieLanguageOverlay)
        Me.gbMovieScraperMetaDataOpts.Controls.Add(Me.lblMovieLanguageOverlay)
        Me.gbMovieScraperMetaDataOpts.Controls.Add(Me.gbMovieScraperDurationFormatOpts)
        Me.gbMovieScraperMetaDataOpts.Controls.Add(Me.chkMovieScraperMetaDataScan)
        Me.gbMovieScraperMetaDataOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieScraperMetaDataOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieScraperMetaDataOpts.Location = New System.Drawing.Point(293, 3)
        Me.gbMovieScraperMetaDataOpts.Name = "gbMovieScraperMetaDataOpts"
        Me.gbMovieScraperMetaDataOpts.Size = New System.Drawing.Size(454, 194)
        Me.gbMovieScraperMetaDataOpts.TabIndex = 63
        Me.gbMovieScraperMetaDataOpts.TabStop = false
        Me.gbMovieScraperMetaDataOpts.Text = "Meta Data"
        '
        'gbMovieScraperDefFIExtOpts
        '
        Me.gbMovieScraperDefFIExtOpts.Controls.Add(Me.lstMovieScraperDefFIExt)
        Me.gbMovieScraperDefFIExtOpts.Controls.Add(Me.txtMovieScraperDefFIExt)
        Me.gbMovieScraperDefFIExtOpts.Controls.Add(Me.lblMovieScraperDefFIExt)
        Me.gbMovieScraperDefFIExtOpts.Controls.Add(Me.btnMovieScraperDefFIExtRemove)
        Me.gbMovieScraperDefFIExtOpts.Controls.Add(Me.btnMovieScraperDefFIExtEdit)
        Me.gbMovieScraperDefFIExtOpts.Controls.Add(Me.btnMovieScraperDefFIExtAdd)
        Me.gbMovieScraperDefFIExtOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieScraperDefFIExtOpts.Location = New System.Drawing.Point(273, 16)
        Me.gbMovieScraperDefFIExtOpts.Name = "gbMovieScraperDefFIExtOpts"
        Me.gbMovieScraperDefFIExtOpts.Size = New System.Drawing.Size(172, 173)
        Me.gbMovieScraperDefFIExtOpts.TabIndex = 8
        Me.gbMovieScraperDefFIExtOpts.TabStop = false
        Me.gbMovieScraperDefFIExtOpts.Text = "Defaults by File Type"
        '
        'lstMovieScraperDefFIExt
        '
        Me.lstMovieScraperDefFIExt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstMovieScraperDefFIExt.FormattingEnabled = true
        Me.lstMovieScraperDefFIExt.Location = New System.Drawing.Point(6, 18)
        Me.lstMovieScraperDefFIExt.Name = "lstMovieScraperDefFIExt"
        Me.lstMovieScraperDefFIExt.Size = New System.Drawing.Size(160, 108)
        Me.lstMovieScraperDefFIExt.TabIndex = 34
        '
        'txtMovieScraperDefFIExt
        '
        Me.txtMovieScraperDefFIExt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieScraperDefFIExt.Location = New System.Drawing.Point(6, 148)
        Me.txtMovieScraperDefFIExt.Name = "txtMovieScraperDefFIExt"
        Me.txtMovieScraperDefFIExt.Size = New System.Drawing.Size(81, 22)
        Me.txtMovieScraperDefFIExt.TabIndex = 33
        '
        'lblMovieScraperDefFIExt
        '
        Me.lblMovieScraperDefFIExt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieScraperDefFIExt.Location = New System.Drawing.Point(3, 133)
        Me.lblMovieScraperDefFIExt.Name = "lblMovieScraperDefFIExt"
        Me.lblMovieScraperDefFIExt.Size = New System.Drawing.Size(63, 19)
        Me.lblMovieScraperDefFIExt.TabIndex = 32
        Me.lblMovieScraperDefFIExt.Text = "File Type:"
        '
        'btnMovieScraperDefFIExtRemove
        '
        Me.btnMovieScraperDefFIExtRemove.Enabled = false
        Me.btnMovieScraperDefFIExtRemove.Image = CType(resources.GetObject("btnMovieScraperDefFIExtRemove.Image"),System.Drawing.Image)
        Me.btnMovieScraperDefFIExtRemove.Location = New System.Drawing.Point(138, 148)
        Me.btnMovieScraperDefFIExtRemove.Name = "btnMovieScraperDefFIExtRemove"
        Me.btnMovieScraperDefFIExtRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieScraperDefFIExtRemove.TabIndex = 31
        Me.btnMovieScraperDefFIExtRemove.UseVisualStyleBackColor = true
        '
        'btnMovieScraperDefFIExtEdit
        '
        Me.btnMovieScraperDefFIExtEdit.Enabled = false
        Me.btnMovieScraperDefFIExtEdit.Image = CType(resources.GetObject("btnMovieScraperDefFIExtEdit.Image"),System.Drawing.Image)
        Me.btnMovieScraperDefFIExtEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieScraperDefFIExtEdit.Location = New System.Drawing.Point(115, 148)
        Me.btnMovieScraperDefFIExtEdit.Name = "btnMovieScraperDefFIExtEdit"
        Me.btnMovieScraperDefFIExtEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieScraperDefFIExtEdit.TabIndex = 30
        Me.btnMovieScraperDefFIExtEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieScraperDefFIExtEdit.UseVisualStyleBackColor = true
        '
        'btnMovieScraperDefFIExtAdd
        '
        Me.btnMovieScraperDefFIExtAdd.Enabled = false
        Me.btnMovieScraperDefFIExtAdd.Image = CType(resources.GetObject("btnMovieScraperDefFIExtAdd.Image"),System.Drawing.Image)
        Me.btnMovieScraperDefFIExtAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieScraperDefFIExtAdd.Location = New System.Drawing.Point(93, 148)
        Me.btnMovieScraperDefFIExtAdd.Name = "btnMovieScraperDefFIExtAdd"
        Me.btnMovieScraperDefFIExtAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieScraperDefFIExtAdd.TabIndex = 29
        Me.btnMovieScraperDefFIExtAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieScraperDefFIExtAdd.UseVisualStyleBackColor = true
        '
        'chkMovieScraperMetaDataIFOScan
        '
        Me.chkMovieScraperMetaDataIFOScan.AutoSize = true
        Me.chkMovieScraperMetaDataIFOScan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperMetaDataIFOScan.Location = New System.Drawing.Point(6, 33)
        Me.chkMovieScraperMetaDataIFOScan.Name = "chkMovieScraperMetaDataIFOScan"
        Me.chkMovieScraperMetaDataIFOScan.Size = New System.Drawing.Size(123, 17)
        Me.chkMovieScraperMetaDataIFOScan.TabIndex = 18
        Me.chkMovieScraperMetaDataIFOScan.Text = "Enable IFO Parsing"
        Me.chkMovieScraperMetaDataIFOScan.UseVisualStyleBackColor = true
        '
        'cbMovieLanguageOverlay
        '
        Me.cbMovieLanguageOverlay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieLanguageOverlay.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieLanguageOverlay.FormattingEnabled = true
        Me.cbMovieLanguageOverlay.Location = New System.Drawing.Point(11, 155)
        Me.cbMovieLanguageOverlay.Name = "cbMovieLanguageOverlay"
        Me.cbMovieLanguageOverlay.Size = New System.Drawing.Size(174, 21)
        Me.cbMovieLanguageOverlay.Sorted = true
        Me.cbMovieLanguageOverlay.TabIndex = 17
        '
        'lblMovieLanguageOverlay
        '
        Me.lblMovieLanguageOverlay.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieLanguageOverlay.Location = New System.Drawing.Point(6, 123)
        Me.lblMovieLanguageOverlay.Name = "lblMovieLanguageOverlay"
        Me.lblMovieLanguageOverlay.Size = New System.Drawing.Size(234, 29)
        Me.lblMovieLanguageOverlay.TabIndex = 16
        Me.lblMovieLanguageOverlay.Text = "Display Overlay if Video Contains an Audio Stream With the Following Language:"
        '
        'gbMovieScraperDurationFormatOpts
        '
        Me.gbMovieScraperDurationFormatOpts.Controls.Add(Me.lblMovieScraperDurationRuntimeFormat)
        Me.gbMovieScraperDurationFormatOpts.Controls.Add(Me.txtMovieScraperDurationRuntimeFormat)
        Me.gbMovieScraperDurationFormatOpts.Controls.Add(Me.chkMovieScraperUseMDDuration)
        Me.gbMovieScraperDurationFormatOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieScraperDurationFormatOpts.Location = New System.Drawing.Point(6, 50)
        Me.gbMovieScraperDurationFormatOpts.Name = "gbMovieScraperDurationFormatOpts"
        Me.gbMovieScraperDurationFormatOpts.Size = New System.Drawing.Size(234, 64)
        Me.gbMovieScraperDurationFormatOpts.TabIndex = 9
        Me.gbMovieScraperDurationFormatOpts.TabStop = false
        Me.gbMovieScraperDurationFormatOpts.Text = "Duration Format"
        '
        'lblMovieScraperDurationRuntimeFormat
        '
        Me.lblMovieScraperDurationRuntimeFormat.Font = New System.Drawing.Font("Segoe UI", 7!)
        Me.lblMovieScraperDurationRuntimeFormat.Location = New System.Drawing.Point(160, 10)
        Me.lblMovieScraperDurationRuntimeFormat.Name = "lblMovieScraperDurationRuntimeFormat"
        Me.lblMovieScraperDurationRuntimeFormat.Size = New System.Drawing.Size(72, 50)
        Me.lblMovieScraperDurationRuntimeFormat.TabIndex = 23
        Me.lblMovieScraperDurationRuntimeFormat.Text = "<h>=Hours"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"<m>=Minutes"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"<s>=Seconds"
        Me.lblMovieScraperDurationRuntimeFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMovieScraperDurationRuntimeFormat
        '
        Me.txtMovieScraperDurationRuntimeFormat.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieScraperDurationRuntimeFormat.Location = New System.Drawing.Point(5, 34)
        Me.txtMovieScraperDurationRuntimeFormat.Name = "txtMovieScraperDurationRuntimeFormat"
        Me.txtMovieScraperDurationRuntimeFormat.Size = New System.Drawing.Size(145, 22)
        Me.txtMovieScraperDurationRuntimeFormat.TabIndex = 22
        '
        'chkMovieScraperUseMDDuration
        '
        Me.chkMovieScraperUseMDDuration.AutoSize = true
        Me.chkMovieScraperUseMDDuration.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperUseMDDuration.Location = New System.Drawing.Point(5, 15)
        Me.chkMovieScraperUseMDDuration.Name = "chkMovieScraperUseMDDuration"
        Me.chkMovieScraperUseMDDuration.Size = New System.Drawing.Size(158, 17)
        Me.chkMovieScraperUseMDDuration.TabIndex = 8
        Me.chkMovieScraperUseMDDuration.Text = "Use Duration for Runtime"
        Me.chkMovieScraperUseMDDuration.UseVisualStyleBackColor = true
        '
        'chkMovieScraperMetaDataScan
        '
        Me.chkMovieScraperMetaDataScan.AutoSize = true
        Me.chkMovieScraperMetaDataScan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperMetaDataScan.Location = New System.Drawing.Point(6, 16)
        Me.chkMovieScraperMetaDataScan.Name = "chkMovieScraperMetaDataScan"
        Me.chkMovieScraperMetaDataScan.Size = New System.Drawing.Size(106, 17)
        Me.chkMovieScraperMetaDataScan.TabIndex = 7
        Me.chkMovieScraperMetaDataScan.Text = "Scan Meta Data"
        Me.chkMovieScraperMetaDataScan.UseVisualStyleBackColor = true
        '
        'gbMovieScraperGlobalOpts
        '
        Me.gbMovieScraperGlobalOpts.Controls.Add(Me.tblMovieScraperGlobalOpts)
        Me.gbMovieScraperGlobalOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieScraperGlobalOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieScraperGlobalOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbMovieScraperGlobalOpts.Name = "gbMovieScraperGlobalOpts"
        Me.tblMovieScraper.SetRowSpan(Me.gbMovieScraperGlobalOpts, 4)
        Me.gbMovieScraperGlobalOpts.Size = New System.Drawing.Size(284, 494)
        Me.gbMovieScraperGlobalOpts.TabIndex = 1
        Me.gbMovieScraperGlobalOpts.TabStop = false
        Me.gbMovieScraperGlobalOpts.Text = "Scraper Fields - Global"
        '
        'tblMovieScraperGlobalOpts
        '
        Me.tblMovieScraperGlobalOpts.AutoScroll = true
        Me.tblMovieScraperGlobalOpts.ColumnCount = 5
        Me.tblMovieScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockCollectionID, 2, 22)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.Label1, 2, 0)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.Label4, 3, 0)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.cbMovieScraperCertLang, 3, 12)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockRating, 2, 8)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockTitle, 2, 1)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperTitle, 0, 1)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperRating, 0, 8)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.Label3, 0, 0)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperCollectionID, 0, 22)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieLockLanguageA, 0, 24)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieLockLanguageV, 0, 25)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieLockCollections, 0, 23)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockLanguageA, 2, 24)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockLanguageV, 2, 25)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperTitle, 1, 1)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperRating, 1, 8)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperCollectionID, 1, 22)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockCollections, 2, 23)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperOriginalTitle, 0, 2)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperOriginalTitle, 1, 2)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockOriginalTitle, 2, 2)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperYear, 0, 3)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperYear, 1, 3)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockYear, 2, 3)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperRelease, 0, 4)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperRelease, 1, 4)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockReleaseDate, 2, 4)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperPlot, 0, 5)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperPlot, 1, 5)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockPlot, 2, 5)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperOutline, 0, 6)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperOutline, 1, 6)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockOutline, 2, 6)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperTagline, 0, 7)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperTagline, 1, 7)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockTagline, 2, 7)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperVotes, 0, 9)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperVotes, 1, 9)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockVotes, 2, 9)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperTop250, 0, 10)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperTop250, 1, 10)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockTop250, 2, 10)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperMPAA, 0, 11)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperMPAA, 1, 11)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockMPAA, 2, 11)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperCertification, 0, 12)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperCert, 1, 12)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockCert, 2, 12)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperRuntime, 0, 13)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperRuntime, 1, 13)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockRuntime, 2, 13)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperStudio, 0, 21)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperStudio, 1, 21)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockStudio, 2, 21)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.txtMovieScraperStudioLimit, 3, 21)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperTags, 0, 14)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperTags, 1, 14)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockTags, 2, 14)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperTrailer, 0, 15)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperTrailer, 1, 15)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockTrailer, 2, 15)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGenre, 0, 16)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperGenre, 1, 16)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockGenre, 2, 16)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.txtMovieScraperGenreLimit, 3, 16)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperCast, 0, 17)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperCast, 1, 17)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockActors, 2, 17)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.txtMovieScraperCastLimit, 3, 17)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperCountry, 0, 20)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperCountry, 1, 20)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockCountry, 2, 20)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperDirector, 0, 18)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperDirector, 1, 18)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockDirector, 2, 18)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperCredits, 0, 19)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperCredits, 1, 19)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockCredits, 2, 19)
        Me.tblMovieScraperGlobalOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieScraperGlobalOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieScraperGlobalOpts.Name = "tblMovieScraperGlobalOpts"
        Me.tblMovieScraperGlobalOpts.RowCount = 27
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.Size = New System.Drawing.Size(278, 473)
        Me.tblMovieScraperGlobalOpts.TabIndex = 0
        '
        'chkMovieLockCollectionID
        '
        Me.chkMovieLockCollectionID.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockCollectionID.AutoSize = true
        Me.chkMovieLockCollectionID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockCollectionID.Location = New System.Drawing.Point(135, 477)
        Me.chkMovieLockCollectionID.Name = "chkMovieLockCollectionID"
        Me.chkMovieLockCollectionID.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockCollectionID.TabIndex = 5
        Me.chkMovieLockCollectionID.UseVisualStyleBackColor = true
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(127, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Lock"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(182, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Limit"
        '
        'chkMovieLockRating
        '
        Me.chkMovieLockRating.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockRating.AutoSize = true
        Me.chkMovieLockRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockRating.Location = New System.Drawing.Point(135, 166)
        Me.chkMovieLockRating.Name = "chkMovieLockRating"
        Me.chkMovieLockRating.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockRating.TabIndex = 4
        Me.chkMovieLockRating.UseVisualStyleBackColor = true
        '
        'chkMovieLockTitle
        '
        Me.chkMovieLockTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockTitle.Location = New System.Drawing.Point(135, 23)
        Me.chkMovieLockTitle.Name = "chkMovieLockTitle"
        Me.chkMovieLockTitle.Size = New System.Drawing.Size(14, 17)
        Me.chkMovieLockTitle.TabIndex = 3
        Me.chkMovieLockTitle.UseVisualStyleBackColor = true
        '
        'lblMovieScraperTitle
        '
        Me.lblMovieScraperTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperTitle.AutoSize = true
        Me.lblMovieScraperTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperTitle.Location = New System.Drawing.Point(3, 25)
        Me.lblMovieScraperTitle.Name = "lblMovieScraperTitle"
        Me.lblMovieScraperTitle.Size = New System.Drawing.Size(28, 13)
        Me.lblMovieScraperTitle.TabIndex = 67
        Me.lblMovieScraperTitle.Text = "Title"
        '
        'lblMovieScraperRating
        '
        Me.lblMovieScraperRating.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperRating.AutoSize = true
        Me.lblMovieScraperRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperRating.Location = New System.Drawing.Point(3, 166)
        Me.lblMovieScraperRating.Name = "lblMovieScraperRating"
        Me.lblMovieScraperRating.Size = New System.Drawing.Size(41, 13)
        Me.lblMovieScraperRating.TabIndex = 68
        Me.lblMovieScraperRating.Text = "Rating"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.AutoSize = true
        Me.tblMovieScraperGlobalOpts.SetColumnSpan(Me.Label3, 2)
        Me.Label3.Location = New System.Drawing.Point(23, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Scraper Fields"
        '
        'lblMovieScraperCollectionID
        '
        Me.lblMovieScraperCollectionID.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperCollectionID.AutoSize = true
        Me.lblMovieScraperCollectionID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperCollectionID.Location = New System.Drawing.Point(3, 477)
        Me.lblMovieScraperCollectionID.Name = "lblMovieScraperCollectionID"
        Me.lblMovieScraperCollectionID.Size = New System.Drawing.Size(73, 13)
        Me.lblMovieScraperCollectionID.TabIndex = 68
        Me.lblMovieScraperCollectionID.Text = "Collection ID"
        '
        'lblMovieLockLanguageA
        '
        Me.lblMovieLockLanguageA.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieLockLanguageA.AutoSize = true
        Me.lblMovieLockLanguageA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieLockLanguageA.Location = New System.Drawing.Point(3, 517)
        Me.lblMovieLockLanguageA.Name = "lblMovieLockLanguageA"
        Me.lblMovieLockLanguageA.Size = New System.Drawing.Size(97, 13)
        Me.lblMovieLockLanguageA.TabIndex = 68
        Me.lblMovieLockLanguageA.Text = "Language (audio)"
        '
        'lblMovieLockLanguageV
        '
        Me.lblMovieLockLanguageV.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieLockLanguageV.AutoSize = true
        Me.lblMovieLockLanguageV.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieLockLanguageV.Location = New System.Drawing.Point(3, 537)
        Me.lblMovieLockLanguageV.Name = "lblMovieLockLanguageV"
        Me.lblMovieLockLanguageV.Size = New System.Drawing.Size(95, 13)
        Me.lblMovieLockLanguageV.TabIndex = 68
        Me.lblMovieLockLanguageV.Text = "Language (video)"
        '
        'lblMovieLockCollections
        '
        Me.lblMovieLockCollections.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieLockCollections.AutoSize = true
        Me.lblMovieLockCollections.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieLockCollections.Location = New System.Drawing.Point(3, 497)
        Me.lblMovieLockCollections.Name = "lblMovieLockCollections"
        Me.lblMovieLockCollections.Size = New System.Drawing.Size(64, 13)
        Me.lblMovieLockCollections.TabIndex = 68
        Me.lblMovieLockCollections.Text = "Collections"
        '
        'chkMovieLockLanguageA
        '
        Me.chkMovieLockLanguageA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockLanguageA.AutoSize = true
        Me.chkMovieLockLanguageA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockLanguageA.Location = New System.Drawing.Point(135, 517)
        Me.chkMovieLockLanguageA.Name = "chkMovieLockLanguageA"
        Me.chkMovieLockLanguageA.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockLanguageA.TabIndex = 48
        Me.chkMovieLockLanguageA.UseVisualStyleBackColor = true
        '
        'chkMovieLockLanguageV
        '
        Me.chkMovieLockLanguageV.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockLanguageV.AutoSize = true
        Me.chkMovieLockLanguageV.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockLanguageV.Location = New System.Drawing.Point(135, 537)
        Me.chkMovieLockLanguageV.Name = "chkMovieLockLanguageV"
        Me.chkMovieLockLanguageV.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockLanguageV.TabIndex = 47
        Me.chkMovieLockLanguageV.UseVisualStyleBackColor = true
        '
        'chkMovieScraperTitle
        '
        Me.chkMovieScraperTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperTitle.AutoSize = true
        Me.chkMovieScraperTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperTitle.Location = New System.Drawing.Point(106, 24)
        Me.chkMovieScraperTitle.Name = "chkMovieScraperTitle"
        Me.chkMovieScraperTitle.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperTitle.TabIndex = 0
        Me.chkMovieScraperTitle.UseVisualStyleBackColor = true
        '
        'chkMovieScraperRating
        '
        Me.chkMovieScraperRating.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperRating.AutoSize = true
        Me.chkMovieScraperRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperRating.Location = New System.Drawing.Point(106, 166)
        Me.chkMovieScraperRating.Name = "chkMovieScraperRating"
        Me.chkMovieScraperRating.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperRating.TabIndex = 4
        Me.chkMovieScraperRating.UseVisualStyleBackColor = true
        '
        'chkMovieScraperCollectionID
        '
        Me.chkMovieScraperCollectionID.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperCollectionID.AutoSize = true
        Me.chkMovieScraperCollectionID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperCollectionID.Location = New System.Drawing.Point(106, 477)
        Me.chkMovieScraperCollectionID.Name = "chkMovieScraperCollectionID"
        Me.chkMovieScraperCollectionID.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperCollectionID.TabIndex = 26
        Me.chkMovieScraperCollectionID.UseVisualStyleBackColor = true
        '
        'chkMovieLockCollections
        '
        Me.chkMovieLockCollections.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockCollections.AutoSize = true
        Me.chkMovieLockCollections.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockCollections.Location = New System.Drawing.Point(135, 497)
        Me.chkMovieLockCollections.Name = "chkMovieLockCollections"
        Me.chkMovieLockCollections.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockCollections.TabIndex = 66
        Me.chkMovieLockCollections.UseVisualStyleBackColor = true
        '
        'lblMovieScraperOriginalTitle
        '
        Me.lblMovieScraperOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperOriginalTitle.AutoSize = true
        Me.lblMovieScraperOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperOriginalTitle.Location = New System.Drawing.Point(3, 46)
        Me.lblMovieScraperOriginalTitle.Name = "lblMovieScraperOriginalTitle"
        Me.lblMovieScraperOriginalTitle.Size = New System.Drawing.Size(73, 13)
        Me.lblMovieScraperOriginalTitle.TabIndex = 68
        Me.lblMovieScraperOriginalTitle.Text = "Original Title"
        '
        'chkMovieScraperOriginalTitle
        '
        Me.chkMovieScraperOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperOriginalTitle.AutoSize = true
        Me.chkMovieScraperOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperOriginalTitle.Location = New System.Drawing.Point(106, 46)
        Me.chkMovieScraperOriginalTitle.Name = "chkMovieScraperOriginalTitle"
        Me.chkMovieScraperOriginalTitle.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperOriginalTitle.TabIndex = 29
        Me.chkMovieScraperOriginalTitle.UseVisualStyleBackColor = true
        '
        'chkMovieLockOriginalTitle
        '
        Me.chkMovieLockOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockOriginalTitle.AutoSize = true
        Me.chkMovieLockOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockOriginalTitle.Location = New System.Drawing.Point(135, 46)
        Me.chkMovieLockOriginalTitle.Name = "chkMovieLockOriginalTitle"
        Me.chkMovieLockOriginalTitle.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockOriginalTitle.TabIndex = 65
        Me.chkMovieLockOriginalTitle.UseVisualStyleBackColor = true
        '
        'lblMovieScraperYear
        '
        Me.lblMovieScraperYear.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperYear.AutoSize = true
        Me.lblMovieScraperYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperYear.Location = New System.Drawing.Point(3, 66)
        Me.lblMovieScraperYear.Name = "lblMovieScraperYear"
        Me.lblMovieScraperYear.Size = New System.Drawing.Size(28, 13)
        Me.lblMovieScraperYear.TabIndex = 68
        Me.lblMovieScraperYear.Text = "Year"
        '
        'chkMovieScraperYear
        '
        Me.chkMovieScraperYear.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperYear.AutoSize = true
        Me.chkMovieScraperYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperYear.Location = New System.Drawing.Point(106, 66)
        Me.chkMovieScraperYear.Name = "chkMovieScraperYear"
        Me.chkMovieScraperYear.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperYear.TabIndex = 1
        Me.chkMovieScraperYear.UseVisualStyleBackColor = true
        '
        'chkMovieLockYear
        '
        Me.chkMovieLockYear.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockYear.AutoSize = true
        Me.chkMovieLockYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockYear.Location = New System.Drawing.Point(135, 66)
        Me.chkMovieLockYear.Name = "chkMovieLockYear"
        Me.chkMovieLockYear.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockYear.TabIndex = 52
        Me.chkMovieLockYear.UseVisualStyleBackColor = true
        '
        'lblMovieScraperRelease
        '
        Me.lblMovieScraperRelease.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperRelease.AutoSize = true
        Me.lblMovieScraperRelease.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperRelease.Location = New System.Drawing.Point(3, 86)
        Me.lblMovieScraperRelease.Name = "lblMovieScraperRelease"
        Me.lblMovieScraperRelease.Size = New System.Drawing.Size(73, 13)
        Me.lblMovieScraperRelease.TabIndex = 68
        Me.lblMovieScraperRelease.Text = "Release Date"
        '
        'chkMovieScraperRelease
        '
        Me.chkMovieScraperRelease.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperRelease.AutoSize = true
        Me.chkMovieScraperRelease.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperRelease.Location = New System.Drawing.Point(106, 86)
        Me.chkMovieScraperRelease.Name = "chkMovieScraperRelease"
        Me.chkMovieScraperRelease.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperRelease.TabIndex = 3
        Me.chkMovieScraperRelease.UseVisualStyleBackColor = true
        '
        'chkMovieLockReleaseDate
        '
        Me.chkMovieLockReleaseDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockReleaseDate.AutoSize = true
        Me.chkMovieLockReleaseDate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockReleaseDate.Location = New System.Drawing.Point(135, 86)
        Me.chkMovieLockReleaseDate.Name = "chkMovieLockReleaseDate"
        Me.chkMovieLockReleaseDate.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockReleaseDate.TabIndex = 55
        Me.chkMovieLockReleaseDate.UseVisualStyleBackColor = true
        '
        'lblMovieScraperPlot
        '
        Me.lblMovieScraperPlot.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperPlot.AutoSize = true
        Me.lblMovieScraperPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperPlot.Location = New System.Drawing.Point(3, 106)
        Me.lblMovieScraperPlot.Name = "lblMovieScraperPlot"
        Me.lblMovieScraperPlot.Size = New System.Drawing.Size(27, 13)
        Me.lblMovieScraperPlot.TabIndex = 68
        Me.lblMovieScraperPlot.Text = "Plot"
        '
        'chkMovieScraperPlot
        '
        Me.chkMovieScraperPlot.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperPlot.AutoSize = true
        Me.chkMovieScraperPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperPlot.Location = New System.Drawing.Point(106, 106)
        Me.chkMovieScraperPlot.Name = "chkMovieScraperPlot"
        Me.chkMovieScraperPlot.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperPlot.TabIndex = 12
        Me.chkMovieScraperPlot.UseVisualStyleBackColor = true
        '
        'chkMovieLockPlot
        '
        Me.chkMovieLockPlot.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockPlot.AutoSize = true
        Me.chkMovieLockPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockPlot.Location = New System.Drawing.Point(135, 106)
        Me.chkMovieLockPlot.Name = "chkMovieLockPlot"
        Me.chkMovieLockPlot.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockPlot.TabIndex = 0
        Me.chkMovieLockPlot.UseVisualStyleBackColor = true
        '
        'lblMovieScraperOutline
        '
        Me.lblMovieScraperOutline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperOutline.AutoSize = true
        Me.lblMovieScraperOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperOutline.Location = New System.Drawing.Point(3, 126)
        Me.lblMovieScraperOutline.Name = "lblMovieScraperOutline"
        Me.lblMovieScraperOutline.Size = New System.Drawing.Size(46, 13)
        Me.lblMovieScraperOutline.TabIndex = 68
        Me.lblMovieScraperOutline.Text = "Outline"
        '
        'chkMovieScraperOutline
        '
        Me.chkMovieScraperOutline.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperOutline.AutoSize = true
        Me.chkMovieScraperOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperOutline.Location = New System.Drawing.Point(106, 126)
        Me.chkMovieScraperOutline.Name = "chkMovieScraperOutline"
        Me.chkMovieScraperOutline.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperOutline.TabIndex = 11
        Me.chkMovieScraperOutline.UseVisualStyleBackColor = true
        '
        'chkMovieLockOutline
        '
        Me.chkMovieLockOutline.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockOutline.AutoSize = true
        Me.chkMovieLockOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockOutline.Location = New System.Drawing.Point(135, 126)
        Me.chkMovieLockOutline.Name = "chkMovieLockOutline"
        Me.chkMovieLockOutline.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockOutline.TabIndex = 1
        Me.chkMovieLockOutline.UseVisualStyleBackColor = true
        '
        'lblMovieScraperTagline
        '
        Me.lblMovieScraperTagline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperTagline.AutoSize = true
        Me.lblMovieScraperTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperTagline.Location = New System.Drawing.Point(3, 146)
        Me.lblMovieScraperTagline.Name = "lblMovieScraperTagline"
        Me.lblMovieScraperTagline.Size = New System.Drawing.Size(44, 13)
        Me.lblMovieScraperTagline.TabIndex = 68
        Me.lblMovieScraperTagline.Text = "Tagline"
        '
        'chkMovieScraperTagline
        '
        Me.chkMovieScraperTagline.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperTagline.AutoSize = true
        Me.chkMovieScraperTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperTagline.Location = New System.Drawing.Point(106, 146)
        Me.chkMovieScraperTagline.Name = "chkMovieScraperTagline"
        Me.chkMovieScraperTagline.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperTagline.TabIndex = 8
        Me.chkMovieScraperTagline.UseVisualStyleBackColor = true
        '
        'chkMovieLockTagline
        '
        Me.chkMovieLockTagline.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockTagline.AutoSize = true
        Me.chkMovieLockTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockTagline.Location = New System.Drawing.Point(135, 146)
        Me.chkMovieLockTagline.Name = "chkMovieLockTagline"
        Me.chkMovieLockTagline.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockTagline.TabIndex = 3
        Me.chkMovieLockTagline.UseVisualStyleBackColor = true
        '
        'lblMovieScraperVotes
        '
        Me.lblMovieScraperVotes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperVotes.AutoSize = true
        Me.lblMovieScraperVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperVotes.Location = New System.Drawing.Point(3, 186)
        Me.lblMovieScraperVotes.Name = "lblMovieScraperVotes"
        Me.lblMovieScraperVotes.Size = New System.Drawing.Size(36, 13)
        Me.lblMovieScraperVotes.TabIndex = 68
        Me.lblMovieScraperVotes.Text = "Votes"
        '
        'chkMovieScraperVotes
        '
        Me.chkMovieScraperVotes.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperVotes.AutoSize = true
        Me.chkMovieScraperVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperVotes.Location = New System.Drawing.Point(106, 186)
        Me.chkMovieScraperVotes.Name = "chkMovieScraperVotes"
        Me.chkMovieScraperVotes.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperVotes.TabIndex = 6
        Me.chkMovieScraperVotes.UseVisualStyleBackColor = true
        '
        'chkMovieLockVotes
        '
        Me.chkMovieLockVotes.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockVotes.AutoSize = true
        Me.chkMovieLockVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockVotes.Location = New System.Drawing.Point(135, 186)
        Me.chkMovieLockVotes.Name = "chkMovieLockVotes"
        Me.chkMovieLockVotes.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockVotes.TabIndex = 53
        Me.chkMovieLockVotes.UseVisualStyleBackColor = true
        '
        'lblMovieScraperTop250
        '
        Me.lblMovieScraperTop250.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperTop250.AutoSize = true
        Me.lblMovieScraperTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperTop250.Location = New System.Drawing.Point(3, 206)
        Me.lblMovieScraperTop250.Name = "lblMovieScraperTop250"
        Me.lblMovieScraperTop250.Size = New System.Drawing.Size(47, 13)
        Me.lblMovieScraperTop250.TabIndex = 68
        Me.lblMovieScraperTop250.Text = "Top 250"
        '
        'chkMovieScraperTop250
        '
        Me.chkMovieScraperTop250.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperTop250.AutoSize = true
        Me.chkMovieScraperTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperTop250.Location = New System.Drawing.Point(106, 206)
        Me.chkMovieScraperTop250.Name = "chkMovieScraperTop250"
        Me.chkMovieScraperTop250.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperTop250.TabIndex = 23
        Me.chkMovieScraperTop250.UseVisualStyleBackColor = true
        '
        'chkMovieLockTop250
        '
        Me.chkMovieLockTop250.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockTop250.AutoSize = true
        Me.chkMovieLockTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockTop250.Location = New System.Drawing.Point(135, 206)
        Me.chkMovieLockTop250.Name = "chkMovieLockTop250"
        Me.chkMovieLockTop250.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockTop250.TabIndex = 61
        Me.chkMovieLockTop250.UseVisualStyleBackColor = true
        '
        'lblMovieScraperMPAA
        '
        Me.lblMovieScraperMPAA.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperMPAA.AutoSize = true
        Me.lblMovieScraperMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperMPAA.Location = New System.Drawing.Point(3, 226)
        Me.lblMovieScraperMPAA.Name = "lblMovieScraperMPAA"
        Me.lblMovieScraperMPAA.Size = New System.Drawing.Size(37, 13)
        Me.lblMovieScraperMPAA.TabIndex = 68
        Me.lblMovieScraperMPAA.Text = "MPAA"
        '
        'chkMovieScraperMPAA
        '
        Me.chkMovieScraperMPAA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperMPAA.AutoSize = true
        Me.chkMovieScraperMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperMPAA.Location = New System.Drawing.Point(106, 226)
        Me.chkMovieScraperMPAA.Name = "chkMovieScraperMPAA"
        Me.chkMovieScraperMPAA.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperMPAA.TabIndex = 24
        Me.chkMovieScraperMPAA.UseVisualStyleBackColor = true
        '
        'chkMovieLockMPAA
        '
        Me.chkMovieLockMPAA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockMPAA.AutoSize = true
        Me.chkMovieLockMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockMPAA.Location = New System.Drawing.Point(135, 226)
        Me.chkMovieLockMPAA.Name = "chkMovieLockMPAA"
        Me.chkMovieLockMPAA.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockMPAA.TabIndex = 49
        Me.chkMovieLockMPAA.UseVisualStyleBackColor = true
        '
        'lblMovieScraperCertification
        '
        Me.lblMovieScraperCertification.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperCertification.AutoSize = true
        Me.lblMovieScraperCertification.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperCertification.Location = New System.Drawing.Point(3, 250)
        Me.lblMovieScraperCertification.Name = "lblMovieScraperCertification"
        Me.lblMovieScraperCertification.Size = New System.Drawing.Size(70, 13)
        Me.lblMovieScraperCertification.TabIndex = 68
        Me.lblMovieScraperCertification.Text = "Certification"
        '
        'chkMovieScraperCert
        '
        Me.chkMovieScraperCert.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperCert.AutoSize = true
        Me.chkMovieScraperCert.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperCert.Location = New System.Drawing.Point(106, 249)
        Me.chkMovieScraperCert.Name = "chkMovieScraperCert"
        Me.chkMovieScraperCert.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperCert.TabIndex = 24
        Me.chkMovieScraperCert.UseVisualStyleBackColor = true
        '
        'chkMovieLockCert
        '
        Me.chkMovieLockCert.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockCert.AutoSize = true
        Me.chkMovieLockCert.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockCert.Location = New System.Drawing.Point(135, 249)
        Me.chkMovieLockCert.Name = "chkMovieLockCert"
        Me.chkMovieLockCert.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockCert.TabIndex = 49
        Me.chkMovieLockCert.UseVisualStyleBackColor = true
        '
        'lblMovieScraperRuntime
        '
        Me.lblMovieScraperRuntime.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperRuntime.AutoSize = true
        Me.lblMovieScraperRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperRuntime.Location = New System.Drawing.Point(3, 273)
        Me.lblMovieScraperRuntime.Name = "lblMovieScraperRuntime"
        Me.lblMovieScraperRuntime.Size = New System.Drawing.Size(50, 13)
        Me.lblMovieScraperRuntime.TabIndex = 68
        Me.lblMovieScraperRuntime.Text = "Runtime"
        '
        'chkMovieScraperRuntime
        '
        Me.chkMovieScraperRuntime.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperRuntime.AutoSize = true
        Me.chkMovieScraperRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperRuntime.Location = New System.Drawing.Point(106, 273)
        Me.chkMovieScraperRuntime.Name = "chkMovieScraperRuntime"
        Me.chkMovieScraperRuntime.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperRuntime.TabIndex = 13
        Me.chkMovieScraperRuntime.UseVisualStyleBackColor = true
        '
        'chkMovieLockRuntime
        '
        Me.chkMovieLockRuntime.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockRuntime.AutoSize = true
        Me.chkMovieLockRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockRuntime.Location = New System.Drawing.Point(135, 273)
        Me.chkMovieLockRuntime.Name = "chkMovieLockRuntime"
        Me.chkMovieLockRuntime.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockRuntime.TabIndex = 51
        Me.chkMovieLockRuntime.UseVisualStyleBackColor = true
        '
        'lblMovieScraperStudio
        '
        Me.lblMovieScraperStudio.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperStudio.AutoSize = true
        Me.lblMovieScraperStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperStudio.Location = New System.Drawing.Point(3, 453)
        Me.lblMovieScraperStudio.Name = "lblMovieScraperStudio"
        Me.lblMovieScraperStudio.Size = New System.Drawing.Size(41, 13)
        Me.lblMovieScraperStudio.TabIndex = 68
        Me.lblMovieScraperStudio.Text = "Studio"
        '
        'chkMovieScraperStudio
        '
        Me.chkMovieScraperStudio.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperStudio.AutoSize = true
        Me.chkMovieScraperStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperStudio.Location = New System.Drawing.Point(106, 453)
        Me.chkMovieScraperStudio.Name = "chkMovieScraperStudio"
        Me.chkMovieScraperStudio.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperStudio.TabIndex = 14
        Me.chkMovieScraperStudio.UseVisualStyleBackColor = true
        '
        'chkMovieLockStudio
        '
        Me.chkMovieLockStudio.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockStudio.AutoSize = true
        Me.chkMovieLockStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockStudio.Location = New System.Drawing.Point(135, 453)
        Me.chkMovieLockStudio.Name = "chkMovieLockStudio"
        Me.chkMovieLockStudio.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockStudio.TabIndex = 54
        Me.chkMovieLockStudio.UseVisualStyleBackColor = true
        '
        'txtMovieScraperStudioLimit
        '
        Me.txtMovieScraperStudioLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtMovieScraperStudioLimit.Enabled = false
        Me.txtMovieScraperStudioLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieScraperStudioLimit.Location = New System.Drawing.Point(179, 449)
        Me.txtMovieScraperStudioLimit.Name = "txtMovieScraperStudioLimit"
        Me.txtMovieScraperStudioLimit.Size = New System.Drawing.Size(39, 22)
        Me.txtMovieScraperStudioLimit.TabIndex = 30
        '
        'lblMovieScraperTags
        '
        Me.lblMovieScraperTags.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperTags.AutoSize = true
        Me.lblMovieScraperTags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperTags.Location = New System.Drawing.Point(3, 293)
        Me.lblMovieScraperTags.Name = "lblMovieScraperTags"
        Me.lblMovieScraperTags.Size = New System.Drawing.Size(30, 13)
        Me.lblMovieScraperTags.TabIndex = 68
        Me.lblMovieScraperTags.Text = "Tags"
        '
        'chkMovieScraperTags
        '
        Me.chkMovieScraperTags.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperTags.AutoSize = true
        Me.chkMovieScraperTags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperTags.Location = New System.Drawing.Point(106, 293)
        Me.chkMovieScraperTags.Name = "chkMovieScraperTags"
        Me.chkMovieScraperTags.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperTags.TabIndex = 27
        Me.chkMovieScraperTags.UseVisualStyleBackColor = true
        '
        'chkMovieLockTags
        '
        Me.chkMovieLockTags.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockTags.AutoSize = true
        Me.chkMovieLockTags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockTags.Location = New System.Drawing.Point(135, 293)
        Me.chkMovieLockTags.Name = "chkMovieLockTags"
        Me.chkMovieLockTags.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockTags.TabIndex = 64
        Me.chkMovieLockTags.UseVisualStyleBackColor = true
        '
        'lblMovieScraperTrailer
        '
        Me.lblMovieScraperTrailer.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperTrailer.AutoSize = true
        Me.lblMovieScraperTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperTrailer.Location = New System.Drawing.Point(3, 313)
        Me.lblMovieScraperTrailer.Name = "lblMovieScraperTrailer"
        Me.lblMovieScraperTrailer.Size = New System.Drawing.Size(38, 13)
        Me.lblMovieScraperTrailer.TabIndex = 68
        Me.lblMovieScraperTrailer.Text = "Trailer"
        '
        'chkMovieScraperTrailer
        '
        Me.chkMovieScraperTrailer.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperTrailer.AutoSize = true
        Me.chkMovieScraperTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperTrailer.Location = New System.Drawing.Point(106, 313)
        Me.chkMovieScraperTrailer.Name = "chkMovieScraperTrailer"
        Me.chkMovieScraperTrailer.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperTrailer.TabIndex = 5
        Me.chkMovieScraperTrailer.UseVisualStyleBackColor = true
        '
        'chkMovieLockTrailer
        '
        Me.chkMovieLockTrailer.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockTrailer.AutoSize = true
        Me.chkMovieLockTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockTrailer.Location = New System.Drawing.Point(135, 313)
        Me.chkMovieLockTrailer.Name = "chkMovieLockTrailer"
        Me.chkMovieLockTrailer.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockTrailer.TabIndex = 46
        Me.chkMovieLockTrailer.UseVisualStyleBackColor = true
        '
        'lblMovieScraperGenre
        '
        Me.lblMovieScraperGenre.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGenre.AutoSize = true
        Me.lblMovieScraperGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperGenre.Location = New System.Drawing.Point(3, 337)
        Me.lblMovieScraperGenre.Name = "lblMovieScraperGenre"
        Me.lblMovieScraperGenre.Size = New System.Drawing.Size(38, 13)
        Me.lblMovieScraperGenre.TabIndex = 68
        Me.lblMovieScraperGenre.Text = "Genre"
        '
        'chkMovieScraperGenre
        '
        Me.chkMovieScraperGenre.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperGenre.AutoSize = true
        Me.chkMovieScraperGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperGenre.Location = New System.Drawing.Point(106, 337)
        Me.chkMovieScraperGenre.Name = "chkMovieScraperGenre"
        Me.chkMovieScraperGenre.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperGenre.TabIndex = 10
        Me.chkMovieScraperGenre.UseVisualStyleBackColor = true
        '
        'chkMovieLockGenre
        '
        Me.chkMovieLockGenre.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockGenre.AutoSize = true
        Me.chkMovieLockGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockGenre.Location = New System.Drawing.Point(135, 337)
        Me.chkMovieLockGenre.Name = "chkMovieLockGenre"
        Me.chkMovieLockGenre.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockGenre.TabIndex = 7
        Me.chkMovieLockGenre.UseVisualStyleBackColor = true
        '
        'txtMovieScraperGenreLimit
        '
        Me.txtMovieScraperGenreLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtMovieScraperGenreLimit.Enabled = false
        Me.txtMovieScraperGenreLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieScraperGenreLimit.Location = New System.Drawing.Point(179, 333)
        Me.txtMovieScraperGenreLimit.Name = "txtMovieScraperGenreLimit"
        Me.txtMovieScraperGenreLimit.Size = New System.Drawing.Size(39, 22)
        Me.txtMovieScraperGenreLimit.TabIndex = 21
        '
        'lblMovieScraperCast
        '
        Me.lblMovieScraperCast.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperCast.AutoSize = true
        Me.lblMovieScraperCast.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperCast.Location = New System.Drawing.Point(3, 365)
        Me.lblMovieScraperCast.Name = "lblMovieScraperCast"
        Me.lblMovieScraperCast.Size = New System.Drawing.Size(39, 13)
        Me.lblMovieScraperCast.TabIndex = 68
        Me.lblMovieScraperCast.Text = "Actors"
        '
        'chkMovieScraperCast
        '
        Me.chkMovieScraperCast.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperCast.AutoSize = true
        Me.chkMovieScraperCast.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperCast.Location = New System.Drawing.Point(106, 365)
        Me.chkMovieScraperCast.Name = "chkMovieScraperCast"
        Me.chkMovieScraperCast.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperCast.TabIndex = 7
        Me.chkMovieScraperCast.UseVisualStyleBackColor = true
        '
        'chkMovieLockActors
        '
        Me.chkMovieLockActors.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockActors.AutoSize = true
        Me.chkMovieLockActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockActors.Location = New System.Drawing.Point(135, 365)
        Me.chkMovieLockActors.Name = "chkMovieLockActors"
        Me.chkMovieLockActors.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockActors.TabIndex = 50
        Me.chkMovieLockActors.UseVisualStyleBackColor = true
        '
        'txtMovieScraperCastLimit
        '
        Me.txtMovieScraperCastLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtMovieScraperCastLimit.Enabled = false
        Me.txtMovieScraperCastLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieScraperCastLimit.Location = New System.Drawing.Point(179, 361)
        Me.txtMovieScraperCastLimit.Name = "txtMovieScraperCastLimit"
        Me.txtMovieScraperCastLimit.Size = New System.Drawing.Size(39, 22)
        Me.txtMovieScraperCastLimit.TabIndex = 19
        '
        'lblMovieScraperCountry
        '
        Me.lblMovieScraperCountry.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperCountry.AutoSize = true
        Me.lblMovieScraperCountry.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperCountry.Location = New System.Drawing.Point(3, 429)
        Me.lblMovieScraperCountry.Name = "lblMovieScraperCountry"
        Me.lblMovieScraperCountry.Size = New System.Drawing.Size(48, 13)
        Me.lblMovieScraperCountry.TabIndex = 68
        Me.lblMovieScraperCountry.Text = "Country"
        '
        'chkMovieScraperCountry
        '
        Me.chkMovieScraperCountry.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperCountry.AutoSize = true
        Me.chkMovieScraperCountry.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperCountry.Location = New System.Drawing.Point(106, 429)
        Me.chkMovieScraperCountry.Name = "chkMovieScraperCountry"
        Me.chkMovieScraperCountry.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperCountry.TabIndex = 25
        Me.chkMovieScraperCountry.UseVisualStyleBackColor = true
        '
        'chkMovieLockCountry
        '
        Me.chkMovieLockCountry.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockCountry.AutoSize = true
        Me.chkMovieLockCountry.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockCountry.Location = New System.Drawing.Point(135, 429)
        Me.chkMovieLockCountry.Name = "chkMovieLockCountry"
        Me.chkMovieLockCountry.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockCountry.TabIndex = 63
        Me.chkMovieLockCountry.UseVisualStyleBackColor = true
        '
        'lblMovieScraperDirector
        '
        Me.lblMovieScraperDirector.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperDirector.AutoSize = true
        Me.lblMovieScraperDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperDirector.Location = New System.Drawing.Point(3, 389)
        Me.lblMovieScraperDirector.Name = "lblMovieScraperDirector"
        Me.lblMovieScraperDirector.Size = New System.Drawing.Size(48, 13)
        Me.lblMovieScraperDirector.TabIndex = 68
        Me.lblMovieScraperDirector.Text = "Director"
        '
        'chkMovieScraperDirector
        '
        Me.chkMovieScraperDirector.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperDirector.AutoSize = true
        Me.chkMovieScraperDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperDirector.Location = New System.Drawing.Point(106, 389)
        Me.chkMovieScraperDirector.Name = "chkMovieScraperDirector"
        Me.chkMovieScraperDirector.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperDirector.TabIndex = 9
        Me.chkMovieScraperDirector.UseVisualStyleBackColor = true
        '
        'chkMovieLockDirector
        '
        Me.chkMovieLockDirector.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockDirector.AutoSize = true
        Me.chkMovieLockDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockDirector.Location = New System.Drawing.Point(135, 389)
        Me.chkMovieLockDirector.Name = "chkMovieLockDirector"
        Me.chkMovieLockDirector.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockDirector.TabIndex = 57
        Me.chkMovieLockDirector.UseVisualStyleBackColor = true
        '
        'lblMovieScraperCredits
        '
        Me.lblMovieScraperCredits.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperCredits.AutoSize = true
        Me.lblMovieScraperCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieScraperCredits.Location = New System.Drawing.Point(3, 409)
        Me.lblMovieScraperCredits.Name = "lblMovieScraperCredits"
        Me.lblMovieScraperCredits.Size = New System.Drawing.Size(43, 13)
        Me.lblMovieScraperCredits.TabIndex = 68
        Me.lblMovieScraperCredits.Text = "Credits"
        '
        'chkMovieScraperCredits
        '
        Me.chkMovieScraperCredits.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperCredits.AutoSize = true
        Me.chkMovieScraperCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperCredits.Location = New System.Drawing.Point(106, 409)
        Me.chkMovieScraperCredits.Name = "chkMovieScraperCredits"
        Me.chkMovieScraperCredits.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperCredits.TabIndex = 15
        Me.chkMovieScraperCredits.UseVisualStyleBackColor = true
        '
        'chkMovieLockCredits
        '
        Me.chkMovieLockCredits.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockCredits.AutoSize = true
        Me.chkMovieLockCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieLockCredits.Location = New System.Drawing.Point(135, 409)
        Me.chkMovieLockCredits.Name = "chkMovieLockCredits"
        Me.chkMovieLockCredits.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockCredits.TabIndex = 58
        Me.chkMovieLockCredits.UseVisualStyleBackColor = true
        '
        'gbMovieScraperMiscOpts
        '
        Me.gbMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperStudioWithImg)
        Me.gbMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperReleaseFormat)
        Me.gbMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperCollectionsAuto)
        Me.gbMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperCleanFields)
        Me.gbMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperDetailView)
        Me.gbMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperCleanPlotOutline)
        Me.gbMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperOutlinePlotEnglishOverwrite)
        Me.gbMovieScraperMiscOpts.Controls.Add(Me.lblMovieScraperOutlineLimit)
        Me.gbMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperPlotForOutline)
        Me.gbMovieScraperMiscOpts.Controls.Add(Me.txtMovieScraperOutlineLimit)
        Me.gbMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperOutlineForPlot)
        Me.gbMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperCastWithImg)
        Me.gbMovieScraperMiscOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieScraperMiscOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieScraperMiscOpts.Location = New System.Drawing.Point(293, 203)
        Me.gbMovieScraperMiscOpts.Name = "gbMovieScraperMiscOpts"
        Me.gbMovieScraperMiscOpts.Size = New System.Drawing.Size(454, 194)
        Me.gbMovieScraperMiscOpts.TabIndex = 0
        Me.gbMovieScraperMiscOpts.TabStop = false
        Me.gbMovieScraperMiscOpts.Text = "Miscellaneous"
        '
        'chkMovieScraperStudioWithImg
        '
        Me.chkMovieScraperStudioWithImg.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperStudioWithImg.Location = New System.Drawing.Point(6, 38)
        Me.chkMovieScraperStudioWithImg.Name = "chkMovieScraperStudioWithImg"
        Me.chkMovieScraperStudioWithImg.Size = New System.Drawing.Size(410, 17)
        Me.chkMovieScraperStudioWithImg.TabIndex = 82
        Me.chkMovieScraperStudioWithImg.Text = "Scrape Only Studios With Images"
        Me.chkMovieScraperStudioWithImg.UseVisualStyleBackColor = true
        '
        'chkMovieScraperReleaseFormat
        '
        Me.chkMovieScraperReleaseFormat.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperReleaseFormat.Location = New System.Drawing.Point(6, 171)
        Me.chkMovieScraperReleaseFormat.Name = "chkMovieScraperReleaseFormat"
        Me.chkMovieScraperReleaseFormat.Size = New System.Drawing.Size(410, 17)
        Me.chkMovieScraperReleaseFormat.TabIndex = 81
        Me.chkMovieScraperReleaseFormat.Text = "Date format Releasedate: yyyy-mm-dd"
        Me.chkMovieScraperReleaseFormat.UseVisualStyleBackColor = true
        '
        'chkMovieScraperCollectionsAuto
        '
        Me.chkMovieScraperCollectionsAuto.Enabled = false
        Me.chkMovieScraperCollectionsAuto.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperCollectionsAuto.Location = New System.Drawing.Point(6, 152)
        Me.chkMovieScraperCollectionsAuto.Name = "chkMovieScraperCollectionsAuto"
        Me.chkMovieScraperCollectionsAuto.Size = New System.Drawing.Size(252, 17)
        Me.chkMovieScraperCollectionsAuto.TabIndex = 80
        Me.chkMovieScraperCollectionsAuto.Text = "Add Movie automatically to Collections"
        Me.chkMovieScraperCollectionsAuto.UseVisualStyleBackColor = true
        '
        'chkMovieScraperCleanFields
        '
        Me.chkMovieScraperCleanFields.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperCleanFields.Location = New System.Drawing.Point(266, 19)
        Me.chkMovieScraperCleanFields.Name = "chkMovieScraperCleanFields"
        Me.chkMovieScraperCleanFields.Size = New System.Drawing.Size(179, 17)
        Me.chkMovieScraperCleanFields.TabIndex = 79
        Me.chkMovieScraperCleanFields.Text = "Cleanup disabled fields"
        Me.chkMovieScraperCleanFields.UseVisualStyleBackColor = true
        '
        'chkMovieScraperDetailView
        '
        Me.chkMovieScraperDetailView.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperDetailView.Location = New System.Drawing.Point(6, 133)
        Me.chkMovieScraperDetailView.Name = "chkMovieScraperDetailView"
        Me.chkMovieScraperDetailView.Size = New System.Drawing.Size(415, 17)
        Me.chkMovieScraperDetailView.TabIndex = 78
        Me.chkMovieScraperDetailView.Text = "Show scraped results in detailed view"
        Me.chkMovieScraperDetailView.UseVisualStyleBackColor = true
        '
        'chkMovieScraperCleanPlotOutline
        '
        Me.chkMovieScraperCleanPlotOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperCleanPlotOutline.Location = New System.Drawing.Point(6, 57)
        Me.chkMovieScraperCleanPlotOutline.Name = "chkMovieScraperCleanPlotOutline"
        Me.chkMovieScraperCleanPlotOutline.Size = New System.Drawing.Size(415, 17)
        Me.chkMovieScraperCleanPlotOutline.TabIndex = 76
        Me.chkMovieScraperCleanPlotOutline.Text = "Clean Plot/Outline"
        Me.chkMovieScraperCleanPlotOutline.UseVisualStyleBackColor = true
        '
        'chkMovieScraperOutlinePlotEnglishOverwrite
        '
        Me.chkMovieScraperOutlinePlotEnglishOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperOutlinePlotEnglishOverwrite.Location = New System.Drawing.Point(6, 114)
        Me.chkMovieScraperOutlinePlotEnglishOverwrite.Name = "chkMovieScraperOutlinePlotEnglishOverwrite"
        Me.chkMovieScraperOutlinePlotEnglishOverwrite.Size = New System.Drawing.Size(254, 17)
        Me.chkMovieScraperOutlinePlotEnglishOverwrite.TabIndex = 72
        Me.chkMovieScraperOutlinePlotEnglishOverwrite.Text = "Only overwrite english plot/outline"
        Me.chkMovieScraperOutlinePlotEnglishOverwrite.UseVisualStyleBackColor = true
        '
        'chkMovieScraperPlotForOutline
        '
        Me.chkMovieScraperPlotForOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperPlotForOutline.Location = New System.Drawing.Point(6, 76)
        Me.chkMovieScraperPlotForOutline.Name = "chkMovieScraperPlotForOutline"
        Me.chkMovieScraperPlotForOutline.Size = New System.Drawing.Size(275, 17)
        Me.chkMovieScraperPlotForOutline.TabIndex = 68
        Me.chkMovieScraperPlotForOutline.Text = "Use Plot  for Outline if Outline is Empty"
        Me.chkMovieScraperPlotForOutline.UseVisualStyleBackColor = true
        '
        'chkMovieScraperOutlineForPlot
        '
        Me.chkMovieScraperOutlineForPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperOutlineForPlot.Location = New System.Drawing.Point(6, 95)
        Me.chkMovieScraperOutlineForPlot.Name = "chkMovieScraperOutlineForPlot"
        Me.chkMovieScraperOutlineForPlot.Size = New System.Drawing.Size(380, 17)
        Me.chkMovieScraperOutlineForPlot.TabIndex = 3
        Me.chkMovieScraperOutlineForPlot.Text = "Use Outline for Plot if Plot is Empty"
        Me.chkMovieScraperOutlineForPlot.UseVisualStyleBackColor = true
        '
        'chkMovieScraperCastWithImg
        '
        Me.chkMovieScraperCastWithImg.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperCastWithImg.Location = New System.Drawing.Point(6, 19)
        Me.chkMovieScraperCastWithImg.Name = "chkMovieScraperCastWithImg"
        Me.chkMovieScraperCastWithImg.Size = New System.Drawing.Size(254, 17)
        Me.chkMovieScraperCastWithImg.TabIndex = 1
        Me.chkMovieScraperCastWithImg.Text = "Scrape Only Actors With Images"
        Me.chkMovieScraperCastWithImg.UseVisualStyleBackColor = true
        '
        'gbMovieScraperCertificationOpts
        '
        Me.gbMovieScraperCertificationOpts.Controls.Add(Me.chkMovieScraperCertForMPAAFallback)
        Me.gbMovieScraperCertificationOpts.Controls.Add(Me.chkMovieScraperCertForMPAA)
        Me.gbMovieScraperCertificationOpts.Controls.Add(Me.chkMovieScraperCertFSK)
        Me.gbMovieScraperCertificationOpts.Controls.Add(Me.chkMovieScraperCertOnlyValue)
        Me.gbMovieScraperCertificationOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieScraperCertificationOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbMovieScraperCertificationOpts.Location = New System.Drawing.Point(293, 403)
        Me.gbMovieScraperCertificationOpts.Name = "gbMovieScraperCertificationOpts"
        Me.gbMovieScraperCertificationOpts.Size = New System.Drawing.Size(454, 84)
        Me.gbMovieScraperCertificationOpts.TabIndex = 77
        Me.gbMovieScraperCertificationOpts.TabStop = false
        Me.gbMovieScraperCertificationOpts.Text = "Certification"
        '
        'chkMovieScraperCertForMPAAFallback
        '
        Me.chkMovieScraperCertForMPAAFallback.Enabled = false
        Me.chkMovieScraperCertForMPAAFallback.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperCertForMPAAFallback.Location = New System.Drawing.Point(250, 16)
        Me.chkMovieScraperCertForMPAAFallback.Name = "chkMovieScraperCertForMPAAFallback"
        Me.chkMovieScraperCertForMPAAFallback.Size = New System.Drawing.Size(171, 17)
        Me.chkMovieScraperCertForMPAAFallback.TabIndex = 68
        Me.chkMovieScraperCertForMPAAFallback.Text = "Only if no MPAA is found"
        Me.chkMovieScraperCertForMPAAFallback.UseVisualStyleBackColor = true
        '
        'chkMovieScraperCertForMPAA
        '
        Me.chkMovieScraperCertForMPAA.Enabled = false
        Me.chkMovieScraperCertForMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperCertForMPAA.Location = New System.Drawing.Point(6, 16)
        Me.chkMovieScraperCertForMPAA.Name = "chkMovieScraperCertForMPAA"
        Me.chkMovieScraperCertForMPAA.Size = New System.Drawing.Size(238, 17)
        Me.chkMovieScraperCertForMPAA.TabIndex = 6
        Me.chkMovieScraperCertForMPAA.Text = "Use Certification for MPAA (XBMC users)"
        Me.chkMovieScraperCertForMPAA.UseVisualStyleBackColor = true
        '
        'chkMovieScraperCertFSK
        '
        Me.chkMovieScraperCertFSK.Enabled = false
        Me.chkMovieScraperCertFSK.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperCertFSK.Location = New System.Drawing.Point(6, 36)
        Me.chkMovieScraperCertFSK.Name = "chkMovieScraperCertFSK"
        Me.chkMovieScraperCertFSK.Size = New System.Drawing.Size(312, 17)
        Me.chkMovieScraperCertFSK.TabIndex = 67
        Me.chkMovieScraperCertFSK.Text = "Use MPAA as Fallback for FSK Rating"
        Me.chkMovieScraperCertFSK.UseVisualStyleBackColor = true
        '
        'chkMovieScraperCertOnlyValue
        '
        Me.chkMovieScraperCertOnlyValue.Enabled = false
        Me.chkMovieScraperCertOnlyValue.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperCertOnlyValue.Location = New System.Drawing.Point(6, 57)
        Me.chkMovieScraperCertOnlyValue.Name = "chkMovieScraperCertOnlyValue"
        Me.chkMovieScraperCertOnlyValue.Size = New System.Drawing.Size(312, 17)
        Me.chkMovieScraperCertOnlyValue.TabIndex = 66
        Me.chkMovieScraperCertOnlyValue.Text = "MPAA: Save only number (only for YAMJ)"
        Me.chkMovieScraperCertOnlyValue.UseVisualStyleBackColor = true
        '
        'pnlMovieScraper
        '
        Me.pnlMovieScraper.BackColor = System.Drawing.Color.White
        Me.pnlMovieScraper.Controls.Add(Me.tblMovieScraper)
        Me.pnlMovieScraper.Location = New System.Drawing.Point(900, 900)
        Me.pnlMovieScraper.Name = "pnlMovieScraper"
        Me.pnlMovieScraper.Size = New System.Drawing.Size(750, 500)
        Me.pnlMovieScraper.TabIndex = 14
        Me.pnlMovieScraper.Visible = false
        '
        'tblMovieScraper
        '
        Me.tblMovieScraper.AutoScroll = true
        Me.tblMovieScraper.ColumnCount = 3
        Me.tblMovieScraper.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 290!))
        Me.tblMovieScraper.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 460!))
        Me.tblMovieScraper.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraper.Controls.Add(Me.gbMovieScraperGlobalOpts, 0, 0)
        Me.tblMovieScraper.Controls.Add(Me.gbMovieScraperCertificationOpts, 1, 2)
        Me.tblMovieScraper.Controls.Add(Me.gbMovieScraperMiscOpts, 1, 1)
        Me.tblMovieScraper.Controls.Add(Me.gbMovieScraperMetaDataOpts, 1, 0)
        Me.tblMovieScraper.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieScraper.Location = New System.Drawing.Point(0, 0)
        Me.tblMovieScraper.Name = "tblMovieScraper"
        Me.tblMovieScraper.RowCount = 4
        Me.tblMovieScraper.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200!))
        Me.tblMovieScraper.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200!))
        Me.tblMovieScraper.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90!))
        Me.tblMovieScraper.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraper.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblMovieScraper.Size = New System.Drawing.Size(750, 500)
        Me.tblMovieScraper.TabIndex = 69
        '
        'tsSettingsTopMenu
        '
        Me.tsSettingsTopMenu.AllowMerge = false
        Me.tsSettingsTopMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsSettingsTopMenu.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tsSettingsTopMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.tsSettingsTopMenu.Location = New System.Drawing.Point(0, 64)
        Me.tsSettingsTopMenu.Name = "tsSettingsTopMenu"
        Me.tsSettingsTopMenu.Size = New System.Drawing.Size(1008, 25)
        Me.tsSettingsTopMenu.Stretch = true
        Me.tsSettingsTopMenu.TabIndex = 4
        Me.tsSettingsTopMenu.Text = "ToolStrip1"
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.BackColor = System.Drawing.Color.White
        Me.pnlSettingsMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(752, 502)
        Me.pnlSettingsMain.TabIndex = 9
        '
        'gbSettingsHelp
        '
        Me.gbSettingsHelp.BackColor = System.Drawing.Color.White
        Me.gbSettingsHelp.Controls.Add(Me.pbSettingsHelpLogo)
        Me.gbSettingsHelp.Controls.Add(Me.lblHelp)
        Me.gbSettingsHelp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSettingsHelp.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbSettingsHelp.Location = New System.Drawing.Point(0, 0)
        Me.gbSettingsHelp.Name = "gbSettingsHelp"
        Me.gbSettingsHelp.Size = New System.Drawing.Size(600, 66)
        Me.gbSettingsHelp.TabIndex = 0
        Me.gbSettingsHelp.TabStop = false
        Me.gbSettingsHelp.Text = "     Help"
        '
        'pbSettingsHelpLogo
        '
        Me.pbSettingsHelpLogo.Image = CType(resources.GetObject("pbSettingsHelpLogo.Image"),System.Drawing.Image)
        Me.pbSettingsHelpLogo.Location = New System.Drawing.Point(6, 0)
        Me.pbSettingsHelpLogo.Name = "pbSettingsHelpLogo"
        Me.pbSettingsHelpLogo.Size = New System.Drawing.Size(16, 16)
        Me.pbSettingsHelpLogo.TabIndex = 1
        Me.pbSettingsHelpLogo.TabStop = false
        '
        'lblHelp
        '
        Me.lblHelp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHelp.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblHelp.Location = New System.Drawing.Point(3, 18)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(594, 45)
        Me.lblHelp.TabIndex = 0
        '
        'pnlSettingsHelp
        '
        Me.pnlSettingsHelp.BackColor = System.Drawing.Color.White
        Me.pnlSettingsHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSettingsHelp.Controls.Add(Me.gbSettingsHelp)
        Me.pnlSettingsHelp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsHelp.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlSettingsHelp.Location = New System.Drawing.Point(3, 3)
        Me.pnlSettingsHelp.Name = "pnlSettingsHelp"
        Me.pnlSettingsHelp.Size = New System.Drawing.Size(602, 68)
        Me.pnlSettingsHelp.TabIndex = 8
        '
        'pnlMovieTrailers
        '
        Me.pnlMovieTrailers.BackColor = System.Drawing.Color.White
        Me.pnlMovieTrailers.Controls.Add(Me.gbMovieTrailerOpts)
        Me.pnlMovieTrailers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlMovieTrailers.Location = New System.Drawing.Point(900, 900)
        Me.pnlMovieTrailers.Name = "pnlMovieTrailers"
        Me.pnlMovieTrailers.Size = New System.Drawing.Size(750, 500)
        Me.pnlMovieTrailers.TabIndex = 21
        Me.pnlMovieTrailers.Visible = false
        '
        'gbMovieTrailerOpts
        '
        Me.gbMovieTrailerOpts.Controls.Add(Me.lblMovieTrailerDefaultSearch)
        Me.gbMovieTrailerOpts.Controls.Add(Me.txtMovieTrailerDefaultSearch)
        Me.gbMovieTrailerOpts.Controls.Add(Me.cbMovieTrailerMinQual)
        Me.gbMovieTrailerOpts.Controls.Add(Me.lblMovieTrailerMinQual)
        Me.gbMovieTrailerOpts.Controls.Add(Me.cbMovieTrailerPrefQual)
        Me.gbMovieTrailerOpts.Controls.Add(Me.lblMovieTrailerPrefQual)
        Me.gbMovieTrailerOpts.Controls.Add(Me.chkMovieTrailerDeleteExisting)
        Me.gbMovieTrailerOpts.Controls.Add(Me.chkMovieTrailerOverwrite)
        Me.gbMovieTrailerOpts.Controls.Add(Me.chkMovieTrailerEnable)
        Me.gbMovieTrailerOpts.Location = New System.Drawing.Point(12, 11)
        Me.gbMovieTrailerOpts.Name = "gbMovieTrailerOpts"
        Me.gbMovieTrailerOpts.Size = New System.Drawing.Size(213, 252)
        Me.gbMovieTrailerOpts.TabIndex = 1
        Me.gbMovieTrailerOpts.TabStop = false
        Me.gbMovieTrailerOpts.Text = "Trailers"
        '
        'lblMovieTrailerDefaultSearch
        '
        Me.lblMovieTrailerDefaultSearch.AutoSize = true
        Me.lblMovieTrailerDefaultSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieTrailerDefaultSearch.Location = New System.Drawing.Point(24, 196)
        Me.lblMovieTrailerDefaultSearch.Name = "lblMovieTrailerDefaultSearch"
        Me.lblMovieTrailerDefaultSearch.Size = New System.Drawing.Size(139, 13)
        Me.lblMovieTrailerDefaultSearch.TabIndex = 11
        Me.lblMovieTrailerDefaultSearch.Text = "Default Search Parameter:"
        '
        'txtMovieTrailerDefaultSearch
        '
        Me.txtMovieTrailerDefaultSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMovieTrailerDefaultSearch.Location = New System.Drawing.Point(25, 212)
        Me.txtMovieTrailerDefaultSearch.Name = "txtMovieTrailerDefaultSearch"
        Me.txtMovieTrailerDefaultSearch.Size = New System.Drawing.Size(182, 22)
        Me.txtMovieTrailerDefaultSearch.TabIndex = 10
        '
        'cbMovieTrailerMinQual
        '
        Me.cbMovieTrailerMinQual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieTrailerMinQual.Enabled = false
        Me.cbMovieTrailerMinQual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieTrailerMinQual.FormattingEnabled = true
        Me.cbMovieTrailerMinQual.Location = New System.Drawing.Point(25, 148)
        Me.cbMovieTrailerMinQual.Name = "cbMovieTrailerMinQual"
        Me.cbMovieTrailerMinQual.Size = New System.Drawing.Size(125, 21)
        Me.cbMovieTrailerMinQual.TabIndex = 9
        '
        'lblMovieTrailerMinQual
        '
        Me.lblMovieTrailerMinQual.AutoSize = true
        Me.lblMovieTrailerMinQual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieTrailerMinQual.Location = New System.Drawing.Point(24, 132)
        Me.lblMovieTrailerMinQual.Name = "lblMovieTrailerMinQual"
        Me.lblMovieTrailerMinQual.Size = New System.Drawing.Size(97, 13)
        Me.lblMovieTrailerMinQual.TabIndex = 8
        Me.lblMovieTrailerMinQual.Text = "Minimum Quality:"
        '
        'cbMovieTrailerPrefQual
        '
        Me.cbMovieTrailerPrefQual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieTrailerPrefQual.Enabled = false
        Me.cbMovieTrailerPrefQual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieTrailerPrefQual.FormattingEnabled = true
        Me.cbMovieTrailerPrefQual.Location = New System.Drawing.Point(25, 94)
        Me.cbMovieTrailerPrefQual.Name = "cbMovieTrailerPrefQual"
        Me.cbMovieTrailerPrefQual.Size = New System.Drawing.Size(125, 21)
        Me.cbMovieTrailerPrefQual.TabIndex = 7
        '
        'lblMovieTrailerPrefQual
        '
        Me.lblMovieTrailerPrefQual.AutoSize = true
        Me.lblMovieTrailerPrefQual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieTrailerPrefQual.Location = New System.Drawing.Point(24, 78)
        Me.lblMovieTrailerPrefQual.Name = "lblMovieTrailerPrefQual"
        Me.lblMovieTrailerPrefQual.Size = New System.Drawing.Size(96, 13)
        Me.lblMovieTrailerPrefQual.TabIndex = 6
        Me.lblMovieTrailerPrefQual.Text = "Preferred Quality:"
        '
        'chkMovieTrailerDeleteExisting
        '
        Me.chkMovieTrailerDeleteExisting.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkMovieTrailerDeleteExisting.Enabled = false
        Me.chkMovieTrailerDeleteExisting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieTrailerDeleteExisting.Location = New System.Drawing.Point(25, 57)
        Me.chkMovieTrailerDeleteExisting.Name = "chkMovieTrailerDeleteExisting"
        Me.chkMovieTrailerDeleteExisting.Size = New System.Drawing.Size(152, 27)
        Me.chkMovieTrailerDeleteExisting.TabIndex = 5
        Me.chkMovieTrailerDeleteExisting.Text = "Delete All Existing"
        Me.chkMovieTrailerDeleteExisting.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkMovieTrailerDeleteExisting.UseVisualStyleBackColor = true
        '
        'chkMovieTrailerOverwrite
        '
        Me.chkMovieTrailerOverwrite.AutoSize = true
        Me.chkMovieTrailerOverwrite.Enabled = false
        Me.chkMovieTrailerOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieTrailerOverwrite.Location = New System.Drawing.Point(25, 39)
        Me.chkMovieTrailerOverwrite.Name = "chkMovieTrailerOverwrite"
        Me.chkMovieTrailerOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieTrailerOverwrite.TabIndex = 4
        Me.chkMovieTrailerOverwrite.Text = "Overwrite Existing"
        Me.chkMovieTrailerOverwrite.UseVisualStyleBackColor = true
        '
        'chkMovieTrailerEnable
        '
        Me.chkMovieTrailerEnable.AutoSize = true
        Me.chkMovieTrailerEnable.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieTrailerEnable.Location = New System.Drawing.Point(12, 16)
        Me.chkMovieTrailerEnable.Name = "chkMovieTrailerEnable"
        Me.chkMovieTrailerEnable.Size = New System.Drawing.Size(140, 17)
        Me.chkMovieTrailerEnable.TabIndex = 0
        Me.chkMovieTrailerEnable.Text = "Enable Trailer Support"
        Me.chkMovieTrailerEnable.UseVisualStyleBackColor = true
        '
        'pnlMovieThemes
        '
        Me.pnlMovieThemes.BackColor = System.Drawing.Color.White
        Me.pnlMovieThemes.Controls.Add(Me.gbMovieThemeOpts)
        Me.pnlMovieThemes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlMovieThemes.Location = New System.Drawing.Point(900, 900)
        Me.pnlMovieThemes.Name = "pnlMovieThemes"
        Me.pnlMovieThemes.Size = New System.Drawing.Size(750, 500)
        Me.pnlMovieThemes.TabIndex = 22
        Me.pnlMovieThemes.Visible = false
        '
        'gbMovieThemeOpts
        '
        Me.gbMovieThemeOpts.Controls.Add(Me.chkMovieThemeOverwrite)
        Me.gbMovieThemeOpts.Controls.Add(Me.chkMovieThemeEnable)
        Me.gbMovieThemeOpts.Location = New System.Drawing.Point(12, 21)
        Me.gbMovieThemeOpts.Name = "gbMovieThemeOpts"
        Me.gbMovieThemeOpts.Size = New System.Drawing.Size(235, 64)
        Me.gbMovieThemeOpts.TabIndex = 2
        Me.gbMovieThemeOpts.TabStop = false
        Me.gbMovieThemeOpts.Text = "Themes"
        '
        'chkMovieThemeOverwrite
        '
        Me.chkMovieThemeOverwrite.AutoSize = true
        Me.chkMovieThemeOverwrite.Enabled = false
        Me.chkMovieThemeOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieThemeOverwrite.Location = New System.Drawing.Point(25, 39)
        Me.chkMovieThemeOverwrite.Name = "chkMovieThemeOverwrite"
        Me.chkMovieThemeOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieThemeOverwrite.TabIndex = 4
        Me.chkMovieThemeOverwrite.Text = "Overwrite Existing"
        Me.chkMovieThemeOverwrite.UseVisualStyleBackColor = true
        '
        'chkMovieThemeEnable
        '
        Me.chkMovieThemeEnable.AutoSize = true
        Me.chkMovieThemeEnable.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieThemeEnable.Location = New System.Drawing.Point(12, 16)
        Me.chkMovieThemeEnable.Name = "chkMovieThemeEnable"
        Me.chkMovieThemeEnable.Size = New System.Drawing.Size(142, 17)
        Me.chkMovieThemeEnable.TabIndex = 0
        Me.chkMovieThemeEnable.Text = "Enable Theme Support"
        Me.chkMovieThemeEnable.UseVisualStyleBackColor = true
        '
        'pnlTVThemes
        '
        Me.pnlTVThemes.BackColor = System.Drawing.Color.White
        Me.pnlTVThemes.Controls.Add(Me.Label2)
        Me.pnlTVThemes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlTVThemes.Location = New System.Drawing.Point(900, 900)
        Me.pnlTVThemes.Name = "pnlTVThemes"
        Me.pnlTVThemes.Size = New System.Drawing.Size(750, 500)
        Me.pnlTVThemes.TabIndex = 23
        Me.pnlTVThemes.Visible = false
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(206, 235)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(131, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "TV Themes Dummy Label"
        '
        'pnlMovieSetGeneral
        '
        Me.pnlMovieSetGeneral.BackColor = System.Drawing.Color.White
        Me.pnlMovieSetGeneral.Controls.Add(Me.gbMovieSetGeneralMissingItemsOpts)
        Me.pnlMovieSetGeneral.Controls.Add(Me.gbMovieSetGeneralMiscOpts)
        Me.pnlMovieSetGeneral.Controls.Add(Me.gbMovieSetGeneralMediaListOpts)
        Me.pnlMovieSetGeneral.Location = New System.Drawing.Point(900, 900)
        Me.pnlMovieSetGeneral.Name = "pnlMovieSetGeneral"
        Me.pnlMovieSetGeneral.Size = New System.Drawing.Size(750, 500)
        Me.pnlMovieSetGeneral.TabIndex = 24
        Me.pnlMovieSetGeneral.Visible = false
        '
        'gbMovieSetGeneralMissingItemsOpts
        '
        Me.gbMovieSetGeneralMissingItemsOpts.Controls.Add(Me.chkMovieSetMissingDiscArt)
        Me.gbMovieSetGeneralMissingItemsOpts.Controls.Add(Me.chkMovieSetMissingClearLogo)
        Me.gbMovieSetGeneralMissingItemsOpts.Controls.Add(Me.chkMovieSetMissingClearArt)
        Me.gbMovieSetGeneralMissingItemsOpts.Controls.Add(Me.chkMovieSetMissingLandscape)
        Me.gbMovieSetGeneralMissingItemsOpts.Controls.Add(Me.chkMovieSetMissingBanner)
        Me.gbMovieSetGeneralMissingItemsOpts.Controls.Add(Me.chkMovieSetMissingPoster)
        Me.gbMovieSetGeneralMissingItemsOpts.Controls.Add(Me.chkMovieSetMissingNFO)
        Me.gbMovieSetGeneralMissingItemsOpts.Controls.Add(Me.chkMovieSetMissingFanart)
        Me.gbMovieSetGeneralMissingItemsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieSetGeneralMissingItemsOpts.Location = New System.Drawing.Point(228, 256)
        Me.gbMovieSetGeneralMissingItemsOpts.Name = "gbMovieSetGeneralMissingItemsOpts"
        Me.gbMovieSetGeneralMissingItemsOpts.Size = New System.Drawing.Size(185, 241)
        Me.gbMovieSetGeneralMissingItemsOpts.TabIndex = 5
        Me.gbMovieSetGeneralMissingItemsOpts.TabStop = false
        Me.gbMovieSetGeneralMissingItemsOpts.Text = "Missing Items Filter"
        '
        'chkMovieSetMissingDiscArt
        '
        Me.chkMovieSetMissingDiscArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetMissingDiscArt.Location = New System.Drawing.Point(6, 69)
        Me.chkMovieSetMissingDiscArt.Name = "chkMovieSetMissingDiscArt"
        Me.chkMovieSetMissingDiscArt.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieSetMissingDiscArt.TabIndex = 15
        Me.chkMovieSetMissingDiscArt.Text = "Check for DiscArt"
        Me.chkMovieSetMissingDiscArt.UseVisualStyleBackColor = true
        '
        'chkMovieSetMissingClearLogo
        '
        Me.chkMovieSetMissingClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetMissingClearLogo.Location = New System.Drawing.Point(6, 52)
        Me.chkMovieSetMissingClearLogo.Name = "chkMovieSetMissingClearLogo"
        Me.chkMovieSetMissingClearLogo.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieSetMissingClearLogo.TabIndex = 14
        Me.chkMovieSetMissingClearLogo.Text = "Check for ClearLogo"
        Me.chkMovieSetMissingClearLogo.UseVisualStyleBackColor = true
        '
        'chkMovieSetMissingClearArt
        '
        Me.chkMovieSetMissingClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetMissingClearArt.Location = New System.Drawing.Point(6, 35)
        Me.chkMovieSetMissingClearArt.Name = "chkMovieSetMissingClearArt"
        Me.chkMovieSetMissingClearArt.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieSetMissingClearArt.TabIndex = 13
        Me.chkMovieSetMissingClearArt.Text = "Check for ClearArt"
        Me.chkMovieSetMissingClearArt.UseVisualStyleBackColor = true
        '
        'chkMovieSetMissingLandscape
        '
        Me.chkMovieSetMissingLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetMissingLandscape.Location = New System.Drawing.Point(6, 137)
        Me.chkMovieSetMissingLandscape.Name = "chkMovieSetMissingLandscape"
        Me.chkMovieSetMissingLandscape.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieSetMissingLandscape.TabIndex = 10
        Me.chkMovieSetMissingLandscape.Text = "Check for Landscape"
        Me.chkMovieSetMissingLandscape.UseVisualStyleBackColor = true
        '
        'chkMovieSetMissingBanner
        '
        Me.chkMovieSetMissingBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetMissingBanner.Location = New System.Drawing.Point(6, 17)
        Me.chkMovieSetMissingBanner.Name = "chkMovieSetMissingBanner"
        Me.chkMovieSetMissingBanner.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieSetMissingBanner.TabIndex = 11
        Me.chkMovieSetMissingBanner.Text = "Check for Banner"
        Me.chkMovieSetMissingBanner.UseVisualStyleBackColor = true
        '
        'chkMovieSetMissingPoster
        '
        Me.chkMovieSetMissingPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetMissingPoster.Location = New System.Drawing.Point(6, 171)
        Me.chkMovieSetMissingPoster.Name = "chkMovieSetMissingPoster"
        Me.chkMovieSetMissingPoster.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieSetMissingPoster.TabIndex = 6
        Me.chkMovieSetMissingPoster.Text = "Check for Poster"
        Me.chkMovieSetMissingPoster.UseVisualStyleBackColor = true
        '
        'chkMovieSetMissingNFO
        '
        Me.chkMovieSetMissingNFO.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetMissingNFO.Location = New System.Drawing.Point(6, 154)
        Me.chkMovieSetMissingNFO.Name = "chkMovieSetMissingNFO"
        Me.chkMovieSetMissingNFO.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieSetMissingNFO.TabIndex = 8
        Me.chkMovieSetMissingNFO.Text = "Check for NFO"
        Me.chkMovieSetMissingNFO.UseVisualStyleBackColor = true
        '
        'chkMovieSetMissingFanart
        '
        Me.chkMovieSetMissingFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetMissingFanart.Location = New System.Drawing.Point(6, 120)
        Me.chkMovieSetMissingFanart.Name = "chkMovieSetMissingFanart"
        Me.chkMovieSetMissingFanart.Size = New System.Drawing.Size(174, 17)
        Me.chkMovieSetMissingFanart.TabIndex = 7
        Me.chkMovieSetMissingFanart.Text = "Check for Fanart"
        Me.chkMovieSetMissingFanart.UseVisualStyleBackColor = true
        '
        'gbMovieSetGeneralMiscOpts
        '
        Me.gbMovieSetGeneralMiscOpts.Controls.Add(Me.chkMovieSetClickScrape)
        Me.gbMovieSetGeneralMiscOpts.Controls.Add(Me.chkMovieSetClickScrapeAsk)
        Me.gbMovieSetGeneralMiscOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieSetGeneralMiscOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbMovieSetGeneralMiscOpts.Name = "gbMovieSetGeneralMiscOpts"
        Me.gbMovieSetGeneralMiscOpts.Size = New System.Drawing.Size(219, 71)
        Me.gbMovieSetGeneralMiscOpts.TabIndex = 1
        Me.gbMovieSetGeneralMiscOpts.TabStop = false
        Me.gbMovieSetGeneralMiscOpts.Text = "Miscellaneous"
        '
        'chkMovieSetClickScrape
        '
        Me.chkMovieSetClickScrape.AutoSize = true
        Me.chkMovieSetClickScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkMovieSetClickScrape.Location = New System.Drawing.Point(12, 32)
        Me.chkMovieSetClickScrape.Name = "chkMovieSetClickScrape"
        Me.chkMovieSetClickScrape.Size = New System.Drawing.Size(125, 17)
        Me.chkMovieSetClickScrape.TabIndex = 65
        Me.chkMovieSetClickScrape.Text = "Enable Click Scrape"
        Me.chkMovieSetClickScrape.UseVisualStyleBackColor = true
        '
        'chkMovieSetClickScrapeAsk
        '
        Me.chkMovieSetClickScrapeAsk.AutoSize = true
        Me.chkMovieSetClickScrapeAsk.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkMovieSetClickScrapeAsk.Location = New System.Drawing.Point(21, 48)
        Me.chkMovieSetClickScrapeAsk.Name = "chkMovieSetClickScrapeAsk"
        Me.chkMovieSetClickScrapeAsk.Size = New System.Drawing.Size(127, 17)
        Me.chkMovieSetClickScrapeAsk.TabIndex = 64
        Me.chkMovieSetClickScrapeAsk.Text = "Ask On Click Scrape"
        Me.chkMovieSetClickScrapeAsk.UseVisualStyleBackColor = true
        '
        'gbMovieSetGeneralMediaListOpts
        '
        Me.gbMovieSetGeneralMediaListOpts.Controls.Add(Me.gbMovieSetSortTokensOpts)
        Me.gbMovieSetGeneralMediaListOpts.Controls.Add(Me.chkMovieSetDiscArtCol)
        Me.gbMovieSetGeneralMediaListOpts.Controls.Add(Me.chkMovieSetClearLogoCol)
        Me.gbMovieSetGeneralMediaListOpts.Controls.Add(Me.chkMovieSetClearArtCol)
        Me.gbMovieSetGeneralMediaListOpts.Controls.Add(Me.chkMovieSetBannerCol)
        Me.gbMovieSetGeneralMediaListOpts.Controls.Add(Me.chkMovieSetLandscapeCol)
        Me.gbMovieSetGeneralMediaListOpts.Controls.Add(Me.chkMovieSetPosterCol)
        Me.gbMovieSetGeneralMediaListOpts.Controls.Add(Me.chkMovieSetNFOCol)
        Me.gbMovieSetGeneralMediaListOpts.Controls.Add(Me.chkMovieSetFanartCol)
        Me.gbMovieSetGeneralMediaListOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieSetGeneralMediaListOpts.Location = New System.Drawing.Point(3, 80)
        Me.gbMovieSetGeneralMediaListOpts.Name = "gbMovieSetGeneralMediaListOpts"
        Me.gbMovieSetGeneralMediaListOpts.Size = New System.Drawing.Size(219, 417)
        Me.gbMovieSetGeneralMediaListOpts.TabIndex = 4
        Me.gbMovieSetGeneralMediaListOpts.TabStop = false
        Me.gbMovieSetGeneralMediaListOpts.Text = "Media List Options"
        '
        'gbMovieSetSortTokensOpts
        '
        Me.gbMovieSetSortTokensOpts.Controls.Add(Me.btnMovieSetSortTokenRemove)
        Me.gbMovieSetSortTokensOpts.Controls.Add(Me.btnMovieSetSortTokenAdd)
        Me.gbMovieSetSortTokensOpts.Controls.Add(Me.txtMovieSetSortToken)
        Me.gbMovieSetSortTokensOpts.Controls.Add(Me.lstMovieSetSortTokens)
        Me.gbMovieSetSortTokensOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieSetSortTokensOpts.Location = New System.Drawing.Point(8, 77)
        Me.gbMovieSetSortTokensOpts.Name = "gbMovieSetSortTokensOpts"
        Me.gbMovieSetSortTokensOpts.Size = New System.Drawing.Size(200, 93)
        Me.gbMovieSetSortTokensOpts.TabIndex = 83
        Me.gbMovieSetSortTokensOpts.TabStop = false
        Me.gbMovieSetSortTokensOpts.Text = "Sort Tokens to Ignore"
        '
        'btnMovieSetSortTokenRemove
        '
        Me.btnMovieSetSortTokenRemove.Image = CType(resources.GetObject("btnMovieSetSortTokenRemove.Image"),System.Drawing.Image)
        Me.btnMovieSetSortTokenRemove.Location = New System.Drawing.Point(168, 64)
        Me.btnMovieSetSortTokenRemove.Name = "btnMovieSetSortTokenRemove"
        Me.btnMovieSetSortTokenRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieSetSortTokenRemove.TabIndex = 3
        Me.btnMovieSetSortTokenRemove.UseVisualStyleBackColor = true
        '
        'btnMovieSetSortTokenAdd
        '
        Me.btnMovieSetSortTokenAdd.Image = CType(resources.GetObject("btnMovieSetSortTokenAdd.Image"),System.Drawing.Image)
        Me.btnMovieSetSortTokenAdd.Location = New System.Drawing.Point(72, 64)
        Me.btnMovieSetSortTokenAdd.Name = "btnMovieSetSortTokenAdd"
        Me.btnMovieSetSortTokenAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieSetSortTokenAdd.TabIndex = 2
        Me.btnMovieSetSortTokenAdd.UseVisualStyleBackColor = true
        '
        'txtMovieSetSortToken
        '
        Me.txtMovieSetSortToken.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieSetSortToken.Location = New System.Drawing.Point(10, 64)
        Me.txtMovieSetSortToken.Name = "txtMovieSetSortToken"
        Me.txtMovieSetSortToken.Size = New System.Drawing.Size(61, 22)
        Me.txtMovieSetSortToken.TabIndex = 1
        '
        'lstMovieSetSortTokens
        '
        Me.lstMovieSetSortTokens.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstMovieSetSortTokens.FormattingEnabled = true
        Me.lstMovieSetSortTokens.Location = New System.Drawing.Point(10, 15)
        Me.lstMovieSetSortTokens.Name = "lstMovieSetSortTokens"
        Me.lstMovieSetSortTokens.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstMovieSetSortTokens.Size = New System.Drawing.Size(180, 43)
        Me.lstMovieSetSortTokens.Sorted = true
        Me.lstMovieSetSortTokens.TabIndex = 0
        '
        'chkMovieSetDiscArtCol
        '
        Me.chkMovieSetDiscArtCol.AutoSize = true
        Me.chkMovieSetDiscArtCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetDiscArtCol.Location = New System.Drawing.Point(6, 227)
        Me.chkMovieSetDiscArtCol.Name = "chkMovieSetDiscArtCol"
        Me.chkMovieSetDiscArtCol.Size = New System.Drawing.Size(132, 17)
        Me.chkMovieSetDiscArtCol.TabIndex = 82
        Me.chkMovieSetDiscArtCol.Text = "Hide DiscArt Column"
        Me.chkMovieSetDiscArtCol.UseVisualStyleBackColor = true
        '
        'chkMovieSetClearLogoCol
        '
        Me.chkMovieSetClearLogoCol.AutoSize = true
        Me.chkMovieSetClearLogoCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetClearLogoCol.Location = New System.Drawing.Point(6, 210)
        Me.chkMovieSetClearLogoCol.Name = "chkMovieSetClearLogoCol"
        Me.chkMovieSetClearLogoCol.Size = New System.Drawing.Size(148, 17)
        Me.chkMovieSetClearLogoCol.TabIndex = 81
        Me.chkMovieSetClearLogoCol.Text = "Hide ClearLogo Column"
        Me.chkMovieSetClearLogoCol.UseVisualStyleBackColor = true
        '
        'chkMovieSetClearArtCol
        '
        Me.chkMovieSetClearArtCol.AutoSize = true
        Me.chkMovieSetClearArtCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetClearArtCol.Location = New System.Drawing.Point(6, 193)
        Me.chkMovieSetClearArtCol.Name = "chkMovieSetClearArtCol"
        Me.chkMovieSetClearArtCol.Size = New System.Drawing.Size(137, 17)
        Me.chkMovieSetClearArtCol.TabIndex = 80
        Me.chkMovieSetClearArtCol.Text = "Hide ClearArt Column"
        Me.chkMovieSetClearArtCol.UseVisualStyleBackColor = true
        '
        'chkMovieSetBannerCol
        '
        Me.chkMovieSetBannerCol.AutoSize = true
        Me.chkMovieSetBannerCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetBannerCol.Location = New System.Drawing.Point(6, 176)
        Me.chkMovieSetBannerCol.Name = "chkMovieSetBannerCol"
        Me.chkMovieSetBannerCol.Size = New System.Drawing.Size(133, 17)
        Me.chkMovieSetBannerCol.TabIndex = 79
        Me.chkMovieSetBannerCol.Text = "Hide Banner Column"
        Me.chkMovieSetBannerCol.UseVisualStyleBackColor = true
        '
        'chkMovieSetLandscapeCol
        '
        Me.chkMovieSetLandscapeCol.AutoSize = true
        Me.chkMovieSetLandscapeCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetLandscapeCol.Location = New System.Drawing.Point(6, 295)
        Me.chkMovieSetLandscapeCol.Name = "chkMovieSetLandscapeCol"
        Me.chkMovieSetLandscapeCol.Size = New System.Drawing.Size(150, 17)
        Me.chkMovieSetLandscapeCol.TabIndex = 77
        Me.chkMovieSetLandscapeCol.Text = "Hide Landscape Column"
        Me.chkMovieSetLandscapeCol.UseVisualStyleBackColor = true
        '
        'chkMovieSetPosterCol
        '
        Me.chkMovieSetPosterCol.AutoSize = true
        Me.chkMovieSetPosterCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetPosterCol.Location = New System.Drawing.Point(6, 329)
        Me.chkMovieSetPosterCol.Name = "chkMovieSetPosterCol"
        Me.chkMovieSetPosterCol.Size = New System.Drawing.Size(128, 17)
        Me.chkMovieSetPosterCol.TabIndex = 0
        Me.chkMovieSetPosterCol.Text = "Hide Poster Column"
        Me.chkMovieSetPosterCol.UseVisualStyleBackColor = true
        '
        'chkMovieSetNFOCol
        '
        Me.chkMovieSetNFOCol.AutoSize = true
        Me.chkMovieSetNFOCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetNFOCol.Location = New System.Drawing.Point(6, 312)
        Me.chkMovieSetNFOCol.Name = "chkMovieSetNFOCol"
        Me.chkMovieSetNFOCol.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieSetNFOCol.TabIndex = 2
        Me.chkMovieSetNFOCol.Text = "Hide NFO Column"
        Me.chkMovieSetNFOCol.UseVisualStyleBackColor = true
        '
        'chkMovieSetFanartCol
        '
        Me.chkMovieSetFanartCol.AutoSize = true
        Me.chkMovieSetFanartCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetFanartCol.Location = New System.Drawing.Point(6, 278)
        Me.chkMovieSetFanartCol.Name = "chkMovieSetFanartCol"
        Me.chkMovieSetFanartCol.Size = New System.Drawing.Size(129, 17)
        Me.chkMovieSetFanartCol.TabIndex = 1
        Me.chkMovieSetFanartCol.Text = "Hide Fanart Column"
        Me.chkMovieSetFanartCol.UseVisualStyleBackColor = true
        '
        'pnlMovieSetSources
        '
        Me.pnlMovieSetSources.BackColor = System.Drawing.Color.White
        Me.pnlMovieSetSources.Controls.Add(Me.gbMovieSetSourceMiscOpts)
        Me.pnlMovieSetSources.Controls.Add(Me.gbMovieSetFileNaming)
        Me.pnlMovieSetSources.Location = New System.Drawing.Point(900, 900)
        Me.pnlMovieSetSources.Name = "pnlMovieSetSources"
        Me.pnlMovieSetSources.Size = New System.Drawing.Size(750, 500)
        Me.pnlMovieSetSources.TabIndex = 25
        Me.pnlMovieSetSources.Visible = false
        '
        'gbMovieSetSourceMiscOpts
        '
        Me.gbMovieSetSourceMiscOpts.Controls.Add(Me.chkMovieSetCleanFiles)
        Me.gbMovieSetSourceMiscOpts.Controls.Add(Me.chkMovieSetCleanDB)
        Me.gbMovieSetSourceMiscOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieSetSourceMiscOpts.Location = New System.Drawing.Point(5, 113)
        Me.gbMovieSetSourceMiscOpts.Name = "gbMovieSetSourceMiscOpts"
        Me.gbMovieSetSourceMiscOpts.Size = New System.Drawing.Size(212, 283)
        Me.gbMovieSetSourceMiscOpts.TabIndex = 9
        Me.gbMovieSetSourceMiscOpts.TabStop = false
        Me.gbMovieSetSourceMiscOpts.Text = "Miscellaneous Options"
        '
        'chkMovieSetCleanFiles
        '
        Me.chkMovieSetCleanFiles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetCleanFiles.Location = New System.Drawing.Point(6, 189)
        Me.chkMovieSetCleanFiles.Name = "chkMovieSetCleanFiles"
        Me.chkMovieSetCleanFiles.Size = New System.Drawing.Size(200, 33)
        Me.chkMovieSetCleanFiles.TabIndex = 8
        Me.chkMovieSetCleanFiles.Text = "Remove Images and NFOs with MovieSets"
        Me.chkMovieSetCleanFiles.UseVisualStyleBackColor = true
        '
        'chkMovieSetCleanDB
        '
        Me.chkMovieSetCleanDB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetCleanDB.Location = New System.Drawing.Point(6, 228)
        Me.chkMovieSetCleanDB.Name = "chkMovieSetCleanDB"
        Me.chkMovieSetCleanDB.Size = New System.Drawing.Size(200, 33)
        Me.chkMovieSetCleanDB.TabIndex = 9
        Me.chkMovieSetCleanDB.Text = "Clean database after updating library"
        Me.chkMovieSetCleanDB.UseVisualStyleBackColor = true
        '
        'gbMovieSetFileNaming
        '
        Me.gbMovieSetFileNaming.Controls.Add(Me.tcMovieSetFileNaming)
        Me.gbMovieSetFileNaming.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbMovieSetFileNaming.Location = New System.Drawing.Point(223, 113)
        Me.gbMovieSetFileNaming.Name = "gbMovieSetFileNaming"
        Me.gbMovieSetFileNaming.Size = New System.Drawing.Size(521, 384)
        Me.gbMovieSetFileNaming.TabIndex = 8
        Me.gbMovieSetFileNaming.TabStop = false
        Me.gbMovieSetFileNaming.Text = "File Naming"
        '
        'tcMovieSetFileNaming
        '
        Me.tcMovieSetFileNaming.Controls.Add(Me.tpMovieSetFileNamingXBMC)
        Me.tcMovieSetFileNaming.Controls.Add(Me.tpMovieSetFileNamingExpert)
        Me.tcMovieSetFileNaming.Location = New System.Drawing.Point(6, 18)
        Me.tcMovieSetFileNaming.Name = "tcMovieSetFileNaming"
        Me.tcMovieSetFileNaming.SelectedIndex = 0
        Me.tcMovieSetFileNaming.Size = New System.Drawing.Size(513, 362)
        Me.tcMovieSetFileNaming.TabIndex = 7
        '
        'tpMovieSetFileNamingXBMC
        '
        Me.tpMovieSetFileNamingXBMC.Controls.Add(Me.pbMSAAInfo)
        Me.tpMovieSetFileNamingXBMC.Controls.Add(Me.gbMovieSetMSAAPath)
        Me.tpMovieSetFileNamingXBMC.Controls.Add(Me.gbMovieSetMSAA)
        Me.tpMovieSetFileNamingXBMC.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieSetFileNamingXBMC.Name = "tpMovieSetFileNamingXBMC"
        Me.tpMovieSetFileNamingXBMC.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMovieSetFileNamingXBMC.Size = New System.Drawing.Size(505, 336)
        Me.tpMovieSetFileNamingXBMC.TabIndex = 1
        Me.tpMovieSetFileNamingXBMC.Text = "XBMC"
        Me.tpMovieSetFileNamingXBMC.UseVisualStyleBackColor = true
        '
        'pbMSAAInfo
        '
        Me.pbMSAAInfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.msaa
        Me.pbMSAAInfo.Location = New System.Drawing.Point(217, 74)
        Me.pbMSAAInfo.Name = "pbMSAAInfo"
        Me.pbMSAAInfo.Size = New System.Drawing.Size(250, 250)
        Me.pbMSAAInfo.TabIndex = 8
        Me.pbMSAAInfo.TabStop = false
        '
        'gbMovieSetMSAA
        '
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetUseMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetLandscapeMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetBannerMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetClearArtMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetClearLogoMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetFanartMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetPosterMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetNFOMSAA)
        Me.gbMovieSetMSAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbMovieSetMSAA.Location = New System.Drawing.Point(6, 6)
        Me.gbMovieSetMSAA.Name = "gbMovieSetMSAA"
        Me.gbMovieSetMSAA.Size = New System.Drawing.Size(178, 324)
        Me.gbMovieSetMSAA.TabIndex = 0
        Me.gbMovieSetMSAA.TabStop = false
        Me.gbMovieSetMSAA.Text = "Movie Set Artwork Automator"
        '
        'chkMovieSetUseMSAA
        '
        Me.chkMovieSetUseMSAA.AutoSize = true
        Me.chkMovieSetUseMSAA.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieSetUseMSAA.Name = "chkMovieSetUseMSAA"
        Me.chkMovieSetUseMSAA.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieSetUseMSAA.TabIndex = 10
        Me.chkMovieSetUseMSAA.Text = "Use"
        Me.chkMovieSetUseMSAA.UseVisualStyleBackColor = true
        '
        'chkMovieSetLandscapeMSAA
        '
        Me.chkMovieSetLandscapeMSAA.AutoSize = true
        Me.chkMovieSetLandscapeMSAA.Enabled = false
        Me.chkMovieSetLandscapeMSAA.Location = New System.Drawing.Point(6, 182)
        Me.chkMovieSetLandscapeMSAA.Name = "chkMovieSetLandscapeMSAA"
        Me.chkMovieSetLandscapeMSAA.Size = New System.Drawing.Size(80, 17)
        Me.chkMovieSetLandscapeMSAA.TabIndex = 9
        Me.chkMovieSetLandscapeMSAA.Text = "Landscape"
        Me.chkMovieSetLandscapeMSAA.UseVisualStyleBackColor = true
        '
        'chkMovieSetBannerMSAA
        '
        Me.chkMovieSetBannerMSAA.AutoSize = true
        Me.chkMovieSetBannerMSAA.Enabled = false
        Me.chkMovieSetBannerMSAA.Location = New System.Drawing.Point(6, 113)
        Me.chkMovieSetBannerMSAA.Name = "chkMovieSetBannerMSAA"
        Me.chkMovieSetBannerMSAA.Size = New System.Drawing.Size(63, 17)
        Me.chkMovieSetBannerMSAA.TabIndex = 8
        Me.chkMovieSetBannerMSAA.Text = "Banner"
        Me.chkMovieSetBannerMSAA.UseVisualStyleBackColor = true
        '
        'chkMovieSetClearArtMSAA
        '
        Me.chkMovieSetClearArtMSAA.AutoSize = true
        Me.chkMovieSetClearArtMSAA.Enabled = false
        Me.chkMovieSetClearArtMSAA.Location = New System.Drawing.Point(6, 159)
        Me.chkMovieSetClearArtMSAA.Name = "chkMovieSetClearArtMSAA"
        Me.chkMovieSetClearArtMSAA.Size = New System.Drawing.Size(67, 17)
        Me.chkMovieSetClearArtMSAA.TabIndex = 6
        Me.chkMovieSetClearArtMSAA.Text = "ClearArt"
        Me.chkMovieSetClearArtMSAA.UseVisualStyleBackColor = true
        '
        'chkMovieSetClearLogoMSAA
        '
        Me.chkMovieSetClearLogoMSAA.AutoSize = true
        Me.chkMovieSetClearLogoMSAA.Enabled = false
        Me.chkMovieSetClearLogoMSAA.Location = New System.Drawing.Point(6, 136)
        Me.chkMovieSetClearLogoMSAA.Name = "chkMovieSetClearLogoMSAA"
        Me.chkMovieSetClearLogoMSAA.Size = New System.Drawing.Size(78, 17)
        Me.chkMovieSetClearLogoMSAA.TabIndex = 5
        Me.chkMovieSetClearLogoMSAA.Text = "ClearLogo"
        Me.chkMovieSetClearLogoMSAA.UseVisualStyleBackColor = true
        '
        'chkMovieSetFanartMSAA
        '
        Me.chkMovieSetFanartMSAA.AutoSize = true
        Me.chkMovieSetFanartMSAA.Enabled = false
        Me.chkMovieSetFanartMSAA.Location = New System.Drawing.Point(6, 90)
        Me.chkMovieSetFanartMSAA.Name = "chkMovieSetFanartMSAA"
        Me.chkMovieSetFanartMSAA.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieSetFanartMSAA.TabIndex = 2
        Me.chkMovieSetFanartMSAA.Text = "Fanart"
        Me.chkMovieSetFanartMSAA.UseVisualStyleBackColor = true
        '
        'chkMovieSetPosterMSAA
        '
        Me.chkMovieSetPosterMSAA.AutoSize = true
        Me.chkMovieSetPosterMSAA.Enabled = false
        Me.chkMovieSetPosterMSAA.Location = New System.Drawing.Point(6, 67)
        Me.chkMovieSetPosterMSAA.Name = "chkMovieSetPosterMSAA"
        Me.chkMovieSetPosterMSAA.Size = New System.Drawing.Size(58, 17)
        Me.chkMovieSetPosterMSAA.TabIndex = 1
        Me.chkMovieSetPosterMSAA.Text = "Poster"
        Me.chkMovieSetPosterMSAA.UseVisualStyleBackColor = true
        '
        'chkMovieSetNFOMSAA
        '
        Me.chkMovieSetNFOMSAA.AutoSize = true
        Me.chkMovieSetNFOMSAA.Enabled = false
        Me.chkMovieSetNFOMSAA.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieSetNFOMSAA.Name = "chkMovieSetNFOMSAA"
        Me.chkMovieSetNFOMSAA.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieSetNFOMSAA.TabIndex = 0
        Me.chkMovieSetNFOMSAA.Text = "NFO"
        Me.chkMovieSetNFOMSAA.UseVisualStyleBackColor = true
        '
        'tpMovieSetFileNamingExpert
        '
        Me.tpMovieSetFileNamingExpert.Controls.Add(Me.gbMovieSetExpert)
        Me.tpMovieSetFileNamingExpert.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieSetFileNamingExpert.Name = "tpMovieSetFileNamingExpert"
        Me.tpMovieSetFileNamingExpert.Size = New System.Drawing.Size(505, 336)
        Me.tpMovieSetFileNamingExpert.TabIndex = 2
        Me.tpMovieSetFileNamingExpert.Text = "Expert"
        Me.tpMovieSetFileNamingExpert.UseVisualStyleBackColor = true
        '
        'gbMovieSetExpert
        '
        Me.gbMovieSetExpert.Controls.Add(Me.tcMovieSetFileNamingExpert)
        Me.gbMovieSetExpert.Controls.Add(Me.chkMovieSetUseExpert)
        Me.gbMovieSetExpert.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gbMovieSetExpert.Location = New System.Drawing.Point(6, 6)
        Me.gbMovieSetExpert.Name = "gbMovieSetExpert"
        Me.gbMovieSetExpert.Size = New System.Drawing.Size(493, 324)
        Me.gbMovieSetExpert.TabIndex = 8
        Me.gbMovieSetExpert.TabStop = false
        Me.gbMovieSetExpert.Text = "Expert Settings"
        '
        'tcMovieSetFileNamingExpert
        '
        Me.tcMovieSetFileNamingExpert.Controls.Add(Me.tpMovieSetFileNamingExpertSingle)
        Me.tcMovieSetFileNamingExpert.Controls.Add(Me.tpMovieSetFileNamingExpertParent)
        Me.tcMovieSetFileNamingExpert.Location = New System.Drawing.Point(6, 44)
        Me.tcMovieSetFileNamingExpert.Name = "tcMovieSetFileNamingExpert"
        Me.tcMovieSetFileNamingExpert.SelectedIndex = 0
        Me.tcMovieSetFileNamingExpert.Size = New System.Drawing.Size(481, 280)
        Me.tcMovieSetFileNamingExpert.TabIndex = 2
        '
        'tpMovieSetFileNamingExpertSingle
        '
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.lblMovieSetPathExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.btnMovieSetPathExpertSingleBrowse)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.txtMovieSetPathExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.lblMovieSetClearArtExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.txtMovieSetPosterExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.txtMovieSetFanartExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.txtMovieSetBannerExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.txtMovieSetClearLogoExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.txtMovieSetClearArtExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.txtMovieSetLandscapeExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.txtMovieSetNFOExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.lblMovieSetLandscapeExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.lblMovieSetBannerExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.lblMovieSetClearLogoExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.lblMovieSetFanartExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.lblMovieSetPosterExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Controls.Add(Me.lblMovieSetNFOExpertSingle)
        Me.tpMovieSetFileNamingExpertSingle.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieSetFileNamingExpertSingle.Name = "tpMovieSetFileNamingExpertSingle"
        Me.tpMovieSetFileNamingExpertSingle.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMovieSetFileNamingExpertSingle.Size = New System.Drawing.Size(473, 254)
        Me.tpMovieSetFileNamingExpertSingle.TabIndex = 0
        Me.tpMovieSetFileNamingExpertSingle.Text = "Single Folder"
        Me.tpMovieSetFileNamingExpertSingle.UseVisualStyleBackColor = true
        '
        'lblMovieSetPathExpertSingle
        '
        Me.lblMovieSetPathExpertSingle.AutoSize = true
        Me.lblMovieSetPathExpertSingle.Location = New System.Drawing.Point(6, 201)
        Me.lblMovieSetPathExpertSingle.Name = "lblMovieSetPathExpertSingle"
        Me.lblMovieSetPathExpertSingle.Size = New System.Drawing.Size(30, 13)
        Me.lblMovieSetPathExpertSingle.TabIndex = 31
        Me.lblMovieSetPathExpertSingle.Text = "Path"
        '
        'btnMovieSetPathExpertSingleBrowse
        '
        Me.btnMovieSetPathExpertSingleBrowse.Enabled = false
        Me.btnMovieSetPathExpertSingleBrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnMovieSetPathExpertSingleBrowse.Location = New System.Drawing.Point(297, 198)
        Me.btnMovieSetPathExpertSingleBrowse.Name = "btnMovieSetPathExpertSingleBrowse"
        Me.btnMovieSetPathExpertSingleBrowse.Size = New System.Drawing.Size(25, 22)
        Me.btnMovieSetPathExpertSingleBrowse.TabIndex = 30
        Me.btnMovieSetPathExpertSingleBrowse.Text = "..."
        Me.btnMovieSetPathExpertSingleBrowse.UseVisualStyleBackColor = true
        '
        'txtMovieSetPathExpertSingle
        '
        Me.txtMovieSetPathExpertSingle.Enabled = false
        Me.txtMovieSetPathExpertSingle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieSetPathExpertSingle.Location = New System.Drawing.Point(71, 198)
        Me.txtMovieSetPathExpertSingle.Name = "txtMovieSetPathExpertSingle"
        Me.txtMovieSetPathExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetPathExpertSingle.TabIndex = 29
        '
        'lblMovieSetClearArtExpertSingle
        '
        Me.lblMovieSetClearArtExpertSingle.AutoSize = true
        Me.lblMovieSetClearArtExpertSingle.Location = New System.Drawing.Point(6, 145)
        Me.lblMovieSetClearArtExpertSingle.Name = "lblMovieSetClearArtExpertSingle"
        Me.lblMovieSetClearArtExpertSingle.Size = New System.Drawing.Size(48, 13)
        Me.lblMovieSetClearArtExpertSingle.TabIndex = 28
        Me.lblMovieSetClearArtExpertSingle.Text = "ClearArt"
        '
        'txtMovieSetPosterExpertSingle
        '
        Me.txtMovieSetPosterExpertSingle.Enabled = false
        Me.txtMovieSetPosterExpertSingle.Location = New System.Drawing.Point(71, 33)
        Me.txtMovieSetPosterExpertSingle.Name = "txtMovieSetPosterExpertSingle"
        Me.txtMovieSetPosterExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetPosterExpertSingle.TabIndex = 4
        '
        'txtMovieSetFanartExpertSingle
        '
        Me.txtMovieSetFanartExpertSingle.Enabled = false
        Me.txtMovieSetFanartExpertSingle.Location = New System.Drawing.Point(71, 60)
        Me.txtMovieSetFanartExpertSingle.Name = "txtMovieSetFanartExpertSingle"
        Me.txtMovieSetFanartExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetFanartExpertSingle.TabIndex = 5
        '
        'txtMovieSetBannerExpertSingle
        '
        Me.txtMovieSetBannerExpertSingle.Enabled = false
        Me.txtMovieSetBannerExpertSingle.Location = New System.Drawing.Point(71, 88)
        Me.txtMovieSetBannerExpertSingle.Name = "txtMovieSetBannerExpertSingle"
        Me.txtMovieSetBannerExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetBannerExpertSingle.TabIndex = 7
        '
        'txtMovieSetClearLogoExpertSingle
        '
        Me.txtMovieSetClearLogoExpertSingle.Enabled = false
        Me.txtMovieSetClearLogoExpertSingle.Location = New System.Drawing.Point(71, 115)
        Me.txtMovieSetClearLogoExpertSingle.Name = "txtMovieSetClearLogoExpertSingle"
        Me.txtMovieSetClearLogoExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetClearLogoExpertSingle.TabIndex = 8
        '
        'txtMovieSetClearArtExpertSingle
        '
        Me.txtMovieSetClearArtExpertSingle.Enabled = false
        Me.txtMovieSetClearArtExpertSingle.Location = New System.Drawing.Point(71, 142)
        Me.txtMovieSetClearArtExpertSingle.Name = "txtMovieSetClearArtExpertSingle"
        Me.txtMovieSetClearArtExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetClearArtExpertSingle.TabIndex = 9
        '
        'txtMovieSetLandscapeExpertSingle
        '
        Me.txtMovieSetLandscapeExpertSingle.Enabled = false
        Me.txtMovieSetLandscapeExpertSingle.Location = New System.Drawing.Point(71, 170)
        Me.txtMovieSetLandscapeExpertSingle.Name = "txtMovieSetLandscapeExpertSingle"
        Me.txtMovieSetLandscapeExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetLandscapeExpertSingle.TabIndex = 11
        '
        'txtMovieSetNFOExpertSingle
        '
        Me.txtMovieSetNFOExpertSingle.Enabled = false
        Me.txtMovieSetNFOExpertSingle.Location = New System.Drawing.Point(71, 6)
        Me.txtMovieSetNFOExpertSingle.Name = "txtMovieSetNFOExpertSingle"
        Me.txtMovieSetNFOExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetNFOExpertSingle.TabIndex = 3
        '
        'lblMovieSetLandscapeExpertSingle
        '
        Me.lblMovieSetLandscapeExpertSingle.AutoSize = true
        Me.lblMovieSetLandscapeExpertSingle.Location = New System.Drawing.Point(6, 173)
        Me.lblMovieSetLandscapeExpertSingle.Name = "lblMovieSetLandscapeExpertSingle"
        Me.lblMovieSetLandscapeExpertSingle.Size = New System.Drawing.Size(61, 13)
        Me.lblMovieSetLandscapeExpertSingle.TabIndex = 19
        Me.lblMovieSetLandscapeExpertSingle.Text = "Landscape"
        '
        'lblMovieSetBannerExpertSingle
        '
        Me.lblMovieSetBannerExpertSingle.AutoSize = true
        Me.lblMovieSetBannerExpertSingle.Location = New System.Drawing.Point(6, 91)
        Me.lblMovieSetBannerExpertSingle.Name = "lblMovieSetBannerExpertSingle"
        Me.lblMovieSetBannerExpertSingle.Size = New System.Drawing.Size(44, 13)
        Me.lblMovieSetBannerExpertSingle.TabIndex = 17
        Me.lblMovieSetBannerExpertSingle.Text = "Banner"
        '
        'lblMovieSetClearLogoExpertSingle
        '
        Me.lblMovieSetClearLogoExpertSingle.AutoSize = true
        Me.lblMovieSetClearLogoExpertSingle.Location = New System.Drawing.Point(6, 118)
        Me.lblMovieSetClearLogoExpertSingle.Name = "lblMovieSetClearLogoExpertSingle"
        Me.lblMovieSetClearLogoExpertSingle.Size = New System.Drawing.Size(59, 13)
        Me.lblMovieSetClearLogoExpertSingle.TabIndex = 12
        Me.lblMovieSetClearLogoExpertSingle.Text = "ClearLogo"
        '
        'lblMovieSetFanartExpertSingle
        '
        Me.lblMovieSetFanartExpertSingle.AutoSize = true
        Me.lblMovieSetFanartExpertSingle.Location = New System.Drawing.Point(6, 64)
        Me.lblMovieSetFanartExpertSingle.Name = "lblMovieSetFanartExpertSingle"
        Me.lblMovieSetFanartExpertSingle.Size = New System.Drawing.Size(40, 13)
        Me.lblMovieSetFanartExpertSingle.TabIndex = 11
        Me.lblMovieSetFanartExpertSingle.Text = "Fanart"
        '
        'lblMovieSetPosterExpertSingle
        '
        Me.lblMovieSetPosterExpertSingle.AutoSize = true
        Me.lblMovieSetPosterExpertSingle.Location = New System.Drawing.Point(6, 36)
        Me.lblMovieSetPosterExpertSingle.Name = "lblMovieSetPosterExpertSingle"
        Me.lblMovieSetPosterExpertSingle.Size = New System.Drawing.Size(39, 13)
        Me.lblMovieSetPosterExpertSingle.TabIndex = 10
        Me.lblMovieSetPosterExpertSingle.Text = "Poster"
        '
        'lblMovieSetNFOExpertSingle
        '
        Me.lblMovieSetNFOExpertSingle.AutoSize = true
        Me.lblMovieSetNFOExpertSingle.Location = New System.Drawing.Point(6, 9)
        Me.lblMovieSetNFOExpertSingle.Name = "lblMovieSetNFOExpertSingle"
        Me.lblMovieSetNFOExpertSingle.Size = New System.Drawing.Size(30, 13)
        Me.lblMovieSetNFOExpertSingle.TabIndex = 9
        Me.lblMovieSetNFOExpertSingle.Text = "NFO"
        '
        'tpMovieSetFileNamingExpertParent
        '
        Me.tpMovieSetFileNamingExpertParent.Controls.Add(Me.txtMovieSetPosterExpertParent)
        Me.tpMovieSetFileNamingExpertParent.Controls.Add(Me.txtMovieSetFanartExpertParent)
        Me.tpMovieSetFileNamingExpertParent.Controls.Add(Me.txtMovieSetBannerExpertParent)
        Me.tpMovieSetFileNamingExpertParent.Controls.Add(Me.txtMovieSetClearLogoExpertParent)
        Me.tpMovieSetFileNamingExpertParent.Controls.Add(Me.txtMovieSetClearArtExpertParent)
        Me.tpMovieSetFileNamingExpertParent.Controls.Add(Me.txtMovieSetLandscapeExpertParent)
        Me.tpMovieSetFileNamingExpertParent.Controls.Add(Me.txtMovieSetNFOExpertParent)
        Me.tpMovieSetFileNamingExpertParent.Controls.Add(Me.lblMovieSetClearArtExpertParent)
        Me.tpMovieSetFileNamingExpertParent.Controls.Add(Me.lblMovieSetLandscapeExpertParent)
        Me.tpMovieSetFileNamingExpertParent.Controls.Add(Me.lblMovieSetBannerExpertParent)
        Me.tpMovieSetFileNamingExpertParent.Controls.Add(Me.lblMovieSetClearLogoExpertParent)
        Me.tpMovieSetFileNamingExpertParent.Controls.Add(Me.lblMovieSetFanartExpertParent)
        Me.tpMovieSetFileNamingExpertParent.Controls.Add(Me.lblMovieSetPosterExpertParent)
        Me.tpMovieSetFileNamingExpertParent.Controls.Add(Me.lblMovieSetNFOExpertParent)
        Me.tpMovieSetFileNamingExpertParent.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieSetFileNamingExpertParent.Name = "tpMovieSetFileNamingExpertParent"
        Me.tpMovieSetFileNamingExpertParent.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMovieSetFileNamingExpertParent.Size = New System.Drawing.Size(473, 254)
        Me.tpMovieSetFileNamingExpertParent.TabIndex = 1
        Me.tpMovieSetFileNamingExpertParent.Text = "Parent Folder"
        Me.tpMovieSetFileNamingExpertParent.UseVisualStyleBackColor = true
        '
        'txtMovieSetPosterExpertParent
        '
        Me.txtMovieSetPosterExpertParent.Enabled = false
        Me.txtMovieSetPosterExpertParent.Location = New System.Drawing.Point(71, 33)
        Me.txtMovieSetPosterExpertParent.Name = "txtMovieSetPosterExpertParent"
        Me.txtMovieSetPosterExpertParent.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetPosterExpertParent.TabIndex = 2
        '
        'txtMovieSetFanartExpertParent
        '
        Me.txtMovieSetFanartExpertParent.Enabled = false
        Me.txtMovieSetFanartExpertParent.Location = New System.Drawing.Point(71, 60)
        Me.txtMovieSetFanartExpertParent.Name = "txtMovieSetFanartExpertParent"
        Me.txtMovieSetFanartExpertParent.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetFanartExpertParent.TabIndex = 3
        '
        'txtMovieSetBannerExpertParent
        '
        Me.txtMovieSetBannerExpertParent.Enabled = false
        Me.txtMovieSetBannerExpertParent.Location = New System.Drawing.Point(71, 88)
        Me.txtMovieSetBannerExpertParent.Name = "txtMovieSetBannerExpertParent"
        Me.txtMovieSetBannerExpertParent.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetBannerExpertParent.TabIndex = 5
        '
        'txtMovieSetClearLogoExpertParent
        '
        Me.txtMovieSetClearLogoExpertParent.Enabled = false
        Me.txtMovieSetClearLogoExpertParent.Location = New System.Drawing.Point(71, 115)
        Me.txtMovieSetClearLogoExpertParent.Name = "txtMovieSetClearLogoExpertParent"
        Me.txtMovieSetClearLogoExpertParent.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetClearLogoExpertParent.TabIndex = 6
        '
        'txtMovieSetClearArtExpertParent
        '
        Me.txtMovieSetClearArtExpertParent.Enabled = false
        Me.txtMovieSetClearArtExpertParent.Location = New System.Drawing.Point(71, 142)
        Me.txtMovieSetClearArtExpertParent.Name = "txtMovieSetClearArtExpertParent"
        Me.txtMovieSetClearArtExpertParent.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetClearArtExpertParent.TabIndex = 7
        '
        'txtMovieSetLandscapeExpertParent
        '
        Me.txtMovieSetLandscapeExpertParent.Enabled = false
        Me.txtMovieSetLandscapeExpertParent.Location = New System.Drawing.Point(71, 170)
        Me.txtMovieSetLandscapeExpertParent.Name = "txtMovieSetLandscapeExpertParent"
        Me.txtMovieSetLandscapeExpertParent.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetLandscapeExpertParent.TabIndex = 9
        '
        'txtMovieSetNFOExpertParent
        '
        Me.txtMovieSetNFOExpertParent.Enabled = false
        Me.txtMovieSetNFOExpertParent.Location = New System.Drawing.Point(71, 6)
        Me.txtMovieSetNFOExpertParent.Name = "txtMovieSetNFOExpertParent"
        Me.txtMovieSetNFOExpertParent.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieSetNFOExpertParent.TabIndex = 1
        '
        'lblMovieSetClearArtExpertParent
        '
        Me.lblMovieSetClearArtExpertParent.AutoSize = true
        Me.lblMovieSetClearArtExpertParent.Location = New System.Drawing.Point(6, 145)
        Me.lblMovieSetClearArtExpertParent.Name = "lblMovieSetClearArtExpertParent"
        Me.lblMovieSetClearArtExpertParent.Size = New System.Drawing.Size(48, 13)
        Me.lblMovieSetClearArtExpertParent.TabIndex = 51
        Me.lblMovieSetClearArtExpertParent.Text = "ClearArt"
        '
        'lblMovieSetLandscapeExpertParent
        '
        Me.lblMovieSetLandscapeExpertParent.AutoSize = true
        Me.lblMovieSetLandscapeExpertParent.Location = New System.Drawing.Point(6, 173)
        Me.lblMovieSetLandscapeExpertParent.Name = "lblMovieSetLandscapeExpertParent"
        Me.lblMovieSetLandscapeExpertParent.Size = New System.Drawing.Size(61, 13)
        Me.lblMovieSetLandscapeExpertParent.TabIndex = 42
        Me.lblMovieSetLandscapeExpertParent.Text = "Landscape"
        '
        'lblMovieSetBannerExpertParent
        '
        Me.lblMovieSetBannerExpertParent.AutoSize = true
        Me.lblMovieSetBannerExpertParent.Location = New System.Drawing.Point(6, 91)
        Me.lblMovieSetBannerExpertParent.Name = "lblMovieSetBannerExpertParent"
        Me.lblMovieSetBannerExpertParent.Size = New System.Drawing.Size(44, 13)
        Me.lblMovieSetBannerExpertParent.TabIndex = 40
        Me.lblMovieSetBannerExpertParent.Text = "Banner"
        '
        'lblMovieSetClearLogoExpertParent
        '
        Me.lblMovieSetClearLogoExpertParent.AutoSize = true
        Me.lblMovieSetClearLogoExpertParent.Location = New System.Drawing.Point(6, 118)
        Me.lblMovieSetClearLogoExpertParent.Name = "lblMovieSetClearLogoExpertParent"
        Me.lblMovieSetClearLogoExpertParent.Size = New System.Drawing.Size(59, 13)
        Me.lblMovieSetClearLogoExpertParent.TabIndex = 38
        Me.lblMovieSetClearLogoExpertParent.Text = "ClearLogo"
        '
        'lblMovieSetFanartExpertParent
        '
        Me.lblMovieSetFanartExpertParent.AutoSize = true
        Me.lblMovieSetFanartExpertParent.Location = New System.Drawing.Point(6, 64)
        Me.lblMovieSetFanartExpertParent.Name = "lblMovieSetFanartExpertParent"
        Me.lblMovieSetFanartExpertParent.Size = New System.Drawing.Size(40, 13)
        Me.lblMovieSetFanartExpertParent.TabIndex = 37
        Me.lblMovieSetFanartExpertParent.Text = "Fanart"
        '
        'lblMovieSetPosterExpertParent
        '
        Me.lblMovieSetPosterExpertParent.AutoSize = true
        Me.lblMovieSetPosterExpertParent.Location = New System.Drawing.Point(6, 36)
        Me.lblMovieSetPosterExpertParent.Name = "lblMovieSetPosterExpertParent"
        Me.lblMovieSetPosterExpertParent.Size = New System.Drawing.Size(39, 13)
        Me.lblMovieSetPosterExpertParent.TabIndex = 36
        Me.lblMovieSetPosterExpertParent.Text = "Poster"
        '
        'lblMovieSetNFOExpertParent
        '
        Me.lblMovieSetNFOExpertParent.AutoSize = true
        Me.lblMovieSetNFOExpertParent.Location = New System.Drawing.Point(6, 9)
        Me.lblMovieSetNFOExpertParent.Name = "lblMovieSetNFOExpertParent"
        Me.lblMovieSetNFOExpertParent.Size = New System.Drawing.Size(30, 13)
        Me.lblMovieSetNFOExpertParent.TabIndex = 35
        Me.lblMovieSetNFOExpertParent.Text = "NFO"
        '
        'chkMovieSetUseExpert
        '
        Me.chkMovieSetUseExpert.AutoSize = true
        Me.chkMovieSetUseExpert.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetUseExpert.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieSetUseExpert.Name = "chkMovieSetUseExpert"
        Me.chkMovieSetUseExpert.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieSetUseExpert.TabIndex = 1
        Me.chkMovieSetUseExpert.Text = "Use"
        Me.chkMovieSetUseExpert.UseVisualStyleBackColor = true
        '
        'pnlMovieSetScraper
        '
        Me.pnlMovieSetScraper.AutoSize = true
        Me.pnlMovieSetScraper.BackColor = System.Drawing.Color.White
        Me.pnlMovieSetScraper.Controls.Add(Me.tblMovieSetScraper)
        Me.pnlMovieSetScraper.Location = New System.Drawing.Point(900, 900)
        Me.pnlMovieSetScraper.Name = "pnlMovieSetScraper"
        Me.pnlMovieSetScraper.Size = New System.Drawing.Size(750, 500)
        Me.pnlMovieSetScraper.TabIndex = 26
        Me.pnlMovieSetScraper.Visible = false
        '
        'tblMovieSetScraper
        '
        Me.tblMovieSetScraper.AutoScroll = true
        Me.tblMovieSetScraper.AutoSize = true
        Me.tblMovieSetScraper.ColumnCount = 3
        Me.tblMovieSetScraper.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraper.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraper.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraper.Controls.Add(Me.gbMovieSetScraperTitleRenamerOpts, 1, 0)
        Me.tblMovieSetScraper.Controls.Add(Me.gbMovieSetScraperGlobalOpts, 0, 0)
        Me.tblMovieSetScraper.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetScraper.Location = New System.Drawing.Point(0, 0)
        Me.tblMovieSetScraper.Name = "tblMovieSetScraper"
        Me.tblMovieSetScraper.RowCount = 2
        Me.tblMovieSetScraper.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraper.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraper.Size = New System.Drawing.Size(750, 500)
        Me.tblMovieSetScraper.TabIndex = 70
        '
        'gbMovieSetScraperTitleRenamerOpts
        '
        Me.gbMovieSetScraperTitleRenamerOpts.AutoSize = true
        Me.gbMovieSetScraperTitleRenamerOpts.Controls.Add(Me.tblMovieSetScraperTitleRenamerOpts)
        Me.gbMovieSetScraperTitleRenamerOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetScraperTitleRenamerOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbMovieSetScraperTitleRenamerOpts.Location = New System.Drawing.Point(169, 3)
        Me.gbMovieSetScraperTitleRenamerOpts.Name = "gbMovieSetScraperTitleRenamerOpts"
        Me.gbMovieSetScraperTitleRenamerOpts.Size = New System.Drawing.Size(314, 212)
        Me.gbMovieSetScraperTitleRenamerOpts.TabIndex = 69
        Me.gbMovieSetScraperTitleRenamerOpts.TabStop = false
        Me.gbMovieSetScraperTitleRenamerOpts.Text = "Title Renamer"
        '
        'tblMovieSetScraperTitleRenamerOpts
        '
        Me.tblMovieSetScraperTitleRenamerOpts.AutoSize = true
        Me.tblMovieSetScraperTitleRenamerOpts.ColumnCount = 3
        Me.tblMovieSetScraperTitleRenamerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperTitleRenamerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperTitleRenamerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperTitleRenamerOpts.Controls.Add(Me.btnMovieSetScraperTitleRenamerAdd, 0, 1)
        Me.tblMovieSetScraperTitleRenamerOpts.Controls.Add(Me.btnMovieSetScraperTitleRenamerRemove, 1, 1)
        Me.tblMovieSetScraperTitleRenamerOpts.Controls.Add(Me.dgvMovieSetScraperTitleRenamer, 0, 0)
        Me.tblMovieSetScraperTitleRenamerOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetScraperTitleRenamerOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetScraperTitleRenamerOpts.Name = "tblMovieSetScraperTitleRenamerOpts"
        Me.tblMovieSetScraperTitleRenamerOpts.RowCount = 3
        Me.tblMovieSetScraperTitleRenamerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperTitleRenamerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperTitleRenamerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperTitleRenamerOpts.Size = New System.Drawing.Size(308, 191)
        Me.tblMovieSetScraperTitleRenamerOpts.TabIndex = 70
        '
        'dgvMovieSetScraperTitleRenamer
        '
        Me.dgvMovieSetScraperTitleRenamer.AllowUserToAddRows = false
        Me.dgvMovieSetScraperTitleRenamer.AllowUserToDeleteRows = false
        Me.dgvMovieSetScraperTitleRenamer.AllowUserToResizeColumns = false
        Me.dgvMovieSetScraperTitleRenamer.AllowUserToResizeRows = false
        Me.dgvMovieSetScraperTitleRenamer.BackgroundColor = System.Drawing.Color.White
        Me.dgvMovieSetScraperTitleRenamer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMovieSetScraperTitleRenamer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.tbcMovieSetScrapedTitleRenamerFrom, Me.tbcMovieSetScrapedTitleRenamerTo})
        Me.tblMovieSetScraperTitleRenamerOpts.SetColumnSpan(Me.dgvMovieSetScraperTitleRenamer, 2)
        Me.dgvMovieSetScraperTitleRenamer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMovieSetScraperTitleRenamer.Location = New System.Drawing.Point(3, 3)
        Me.dgvMovieSetScraperTitleRenamer.MultiSelect = false
        Me.dgvMovieSetScraperTitleRenamer.Name = "dgvMovieSetScraperTitleRenamer"
        Me.dgvMovieSetScraperTitleRenamer.RowHeadersVisible = false
        Me.dgvMovieSetScraperTitleRenamer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvMovieSetScraperTitleRenamer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvMovieSetScraperTitleRenamer.ShowCellErrors = false
        Me.dgvMovieSetScraperTitleRenamer.ShowCellToolTips = false
        Me.dgvMovieSetScraperTitleRenamer.ShowRowErrors = false
        Me.dgvMovieSetScraperTitleRenamer.Size = New System.Drawing.Size(302, 156)
        Me.dgvMovieSetScraperTitleRenamer.TabIndex = 68
        '
        'tbcMovieSetScrapedTitleRenamerFrom
        '
        Me.tbcMovieSetScrapedTitleRenamerFrom.FillWeight = 130!
        Me.tbcMovieSetScrapedTitleRenamerFrom.HeaderText = "From"
        Me.tbcMovieSetScrapedTitleRenamerFrom.Name = "tbcMovieSetScrapedTitleRenamerFrom"
        Me.tbcMovieSetScrapedTitleRenamerFrom.Width = 130
        '
        'tbcMovieSetScrapedTitleRenamerTo
        '
        Me.tbcMovieSetScrapedTitleRenamerTo.FillWeight = 150!
        Me.tbcMovieSetScrapedTitleRenamerTo.HeaderText = "To"
        Me.tbcMovieSetScrapedTitleRenamerTo.Name = "tbcMovieSetScrapedTitleRenamerTo"
        Me.tbcMovieSetScrapedTitleRenamerTo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.tbcMovieSetScrapedTitleRenamerTo.Width = 150
        '
        'btnMovieSetScraperTitleRenamerAdd
        '
        Me.btnMovieSetScraperTitleRenamerAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnMovieSetScraperTitleRenamerAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnMovieSetScraperTitleRenamerAdd.Image = CType(resources.GetObject("btnMovieSetScraperTitleRenamerAdd.Image"),System.Drawing.Image)
        Me.btnMovieSetScraperTitleRenamerAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieSetScraperTitleRenamerAdd.Location = New System.Drawing.Point(3, 165)
        Me.btnMovieSetScraperTitleRenamerAdd.Name = "btnMovieSetScraperTitleRenamerAdd"
        Me.btnMovieSetScraperTitleRenamerAdd.Size = New System.Drawing.Size(87, 23)
        Me.btnMovieSetScraperTitleRenamerAdd.TabIndex = 69
        Me.btnMovieSetScraperTitleRenamerAdd.Text = "Add"
        Me.btnMovieSetScraperTitleRenamerAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieSetScraperTitleRenamerAdd.UseVisualStyleBackColor = true
        '
        'btnMovieSetScraperTitleRenamerRemove
        '
        Me.btnMovieSetScraperTitleRenamerRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnMovieSetScraperTitleRenamerRemove.Enabled = false
        Me.btnMovieSetScraperTitleRenamerRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnMovieSetScraperTitleRenamerRemove.Image = CType(resources.GetObject("btnMovieSetScraperTitleRenamerRemove.Image"),System.Drawing.Image)
        Me.btnMovieSetScraperTitleRenamerRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieSetScraperTitleRenamerRemove.Location = New System.Drawing.Point(218, 165)
        Me.btnMovieSetScraperTitleRenamerRemove.Name = "btnMovieSetScraperTitleRenamerRemove"
        Me.btnMovieSetScraperTitleRenamerRemove.Size = New System.Drawing.Size(87, 23)
        Me.btnMovieSetScraperTitleRenamerRemove.TabIndex = 70
        Me.btnMovieSetScraperTitleRenamerRemove.Text = "Remove"
        Me.btnMovieSetScraperTitleRenamerRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieSetScraperTitleRenamerRemove.UseVisualStyleBackColor = true
        '
        'gbMovieSetScraperGlobalOpts
        '
        Me.gbMovieSetScraperGlobalOpts.AutoSize = true
        Me.gbMovieSetScraperGlobalOpts.Controls.Add(Me.tblMovieSetScraperGlobalOpts)
        Me.gbMovieSetScraperGlobalOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetScraperGlobalOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieSetScraperGlobalOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbMovieSetScraperGlobalOpts.MinimumSize = New System.Drawing.Size(160, 0)
        Me.gbMovieSetScraperGlobalOpts.Name = "gbMovieSetScraperGlobalOpts"
        Me.gbMovieSetScraperGlobalOpts.Size = New System.Drawing.Size(160, 212)
        Me.gbMovieSetScraperGlobalOpts.TabIndex = 67
        Me.gbMovieSetScraperGlobalOpts.TabStop = false
        Me.gbMovieSetScraperGlobalOpts.Text = "Scraper Fields - Global"
        '
        'tblMovieSetScraperGlobalOpts
        '
        Me.tblMovieSetScraperGlobalOpts.AutoScroll = true
        Me.tblMovieSetScraperGlobalOpts.AutoSize = true
        Me.tblMovieSetScraperGlobalOpts.ColumnCount = 4
        Me.tblMovieSetScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.chkMovieSetLockPlot, 2, 2)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.chkMovieSetScraperPlot, 1, 2)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.chkMovieSetLockTitle, 2, 1)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.chkMovieSetScraperTitle, 1, 1)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.Label5, 2, 0)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.lblMovieSetScraperTitle, 0, 1)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.Label9, 0, 0)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.lblMovieSetScraperPlot, 0, 2)
        Me.tblMovieSetScraperGlobalOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetScraperGlobalOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetScraperGlobalOpts.Name = "tblMovieSetScraperGlobalOpts"
        Me.tblMovieSetScraperGlobalOpts.RowCount = 4
        Me.tblMovieSetScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblMovieSetScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperGlobalOpts.Size = New System.Drawing.Size(154, 191)
        Me.tblMovieSetScraperGlobalOpts.TabIndex = 1
        '
        'chkMovieSetLockPlot
        '
        Me.chkMovieSetLockPlot.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieSetLockPlot.AutoSize = true
        Me.chkMovieSetLockPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetLockPlot.Location = New System.Drawing.Point(95, 43)
        Me.chkMovieSetLockPlot.Name = "chkMovieSetLockPlot"
        Me.chkMovieSetLockPlot.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieSetLockPlot.TabIndex = 0
        Me.chkMovieSetLockPlot.UseVisualStyleBackColor = true
        '
        'chkMovieSetScraperPlot
        '
        Me.chkMovieSetScraperPlot.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieSetScraperPlot.AutoSize = true
        Me.chkMovieSetScraperPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetScraperPlot.Location = New System.Drawing.Point(51, 43)
        Me.chkMovieSetScraperPlot.Name = "chkMovieSetScraperPlot"
        Me.chkMovieSetScraperPlot.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieSetScraperPlot.TabIndex = 12
        Me.chkMovieSetScraperPlot.UseVisualStyleBackColor = true
        '
        'chkMovieSetLockTitle
        '
        Me.chkMovieSetLockTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieSetLockTitle.AutoSize = true
        Me.chkMovieSetLockTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetLockTitle.Location = New System.Drawing.Point(95, 23)
        Me.chkMovieSetLockTitle.Name = "chkMovieSetLockTitle"
        Me.chkMovieSetLockTitle.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieSetLockTitle.TabIndex = 2
        Me.chkMovieSetLockTitle.UseVisualStyleBackColor = true
        '
        'chkMovieSetScraperTitle
        '
        Me.chkMovieSetScraperTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieSetScraperTitle.AutoSize = true
        Me.chkMovieSetScraperTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetScraperTitle.Location = New System.Drawing.Point(51, 23)
        Me.chkMovieSetScraperTitle.Name = "chkMovieSetScraperTitle"
        Me.chkMovieSetScraperTitle.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieSetScraperTitle.TabIndex = 0
        Me.chkMovieSetScraperTitle.UseVisualStyleBackColor = true
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label5.AutoSize = true
        Me.Label5.Location = New System.Drawing.Point(87, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Lock"
        '
        'lblMovieSetScraperTitle
        '
        Me.lblMovieSetScraperTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSetScraperTitle.AutoSize = true
        Me.lblMovieSetScraperTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieSetScraperTitle.Location = New System.Drawing.Point(3, 23)
        Me.lblMovieSetScraperTitle.Name = "lblMovieSetScraperTitle"
        Me.lblMovieSetScraperTitle.Size = New System.Drawing.Size(28, 13)
        Me.lblMovieSetScraperTitle.TabIndex = 67
        Me.lblMovieSetScraperTitle.Text = "Title"
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label9.AutoSize = true
        Me.tblMovieSetScraperGlobalOpts.SetColumnSpan(Me.Label9, 2)
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(78, 13)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Scraper Fields"
        '
        'lblMovieSetScraperPlot
        '
        Me.lblMovieSetScraperPlot.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSetScraperPlot.AutoSize = true
        Me.lblMovieSetScraperPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieSetScraperPlot.Location = New System.Drawing.Point(3, 43)
        Me.lblMovieSetScraperPlot.Name = "lblMovieSetScraperPlot"
        Me.lblMovieSetScraperPlot.Size = New System.Drawing.Size(27, 13)
        Me.lblMovieSetScraperPlot.TabIndex = 68
        Me.lblMovieSetScraperPlot.Text = "Plot"
        '
        'pnlMovieSetImages
        '
        Me.pnlMovieSetImages.AutoSize = true
        Me.pnlMovieSetImages.BackColor = System.Drawing.Color.White
        Me.pnlMovieSetImages.Controls.Add(Me.tblMovieSetImages)
        Me.pnlMovieSetImages.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlMovieSetImages.Location = New System.Drawing.Point(900, 900)
        Me.pnlMovieSetImages.Name = "pnlMovieSetImages"
        Me.pnlMovieSetImages.Size = New System.Drawing.Size(861, 550)
        Me.pnlMovieSetImages.TabIndex = 27
        Me.pnlMovieSetImages.Visible = false
        '
        'tblMovieSetImages
        '
        Me.tblMovieSetImages.AutoScroll = true
        Me.tblMovieSetImages.AutoSize = true
        Me.tblMovieSetImages.ColumnCount = 4
        Me.tblMovieSetImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImages.Controls.Add(Me.gbMovieSetImagesPosterOpts, 0, 0)
        Me.tblMovieSetImages.Controls.Add(Me.gbMovieSetImagesClearLogoOpts, 1, 1)
        Me.tblMovieSetImages.Controls.Add(Me.gbMovieSetImagesClearArtOpts, 0, 1)
        Me.tblMovieSetImages.Controls.Add(Me.gbMovieSetImagesBannerOpts, 1, 0)
        Me.tblMovieSetImages.Controls.Add(Me.gbMovieSetImagesFanartOpts, 2, 0)
        Me.tblMovieSetImages.Controls.Add(Me.gbMovieSetImagesLandscapeOpts, 2, 1)
        Me.tblMovieSetImages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetImages.Location = New System.Drawing.Point(0, 0)
        Me.tblMovieSetImages.Name = "tblMovieSetImages"
        Me.tblMovieSetImages.RowCount = 3
        Me.tblMovieSetImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImages.Size = New System.Drawing.Size(861, 550)
        Me.tblMovieSetImages.TabIndex = 16
        '
        'gbMovieSetImagesPosterOpts
        '
        Me.gbMovieSetImagesPosterOpts.AutoSize = true
        Me.gbMovieSetImagesPosterOpts.Controls.Add(Me.tblMovieSetImagesPosterOpts)
        Me.gbMovieSetImagesPosterOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetImagesPosterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieSetImagesPosterOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbMovieSetImagesPosterOpts.Name = "gbMovieSetImagesPosterOpts"
        Me.gbMovieSetImagesPosterOpts.Size = New System.Drawing.Size(281, 142)
        Me.gbMovieSetImagesPosterOpts.TabIndex = 2
        Me.gbMovieSetImagesPosterOpts.TabStop = false
        Me.gbMovieSetImagesPosterOpts.Text = "Poster"
        '
        'tblMovieSetImagesPosterOpts
        '
        Me.tblMovieSetImagesPosterOpts.AutoSize = true
        Me.tblMovieSetImagesPosterOpts.ColumnCount = 5
        Me.tblMovieSetImagesPosterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesPosterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesPosterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesPosterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesPosterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesPosterOpts.Controls.Add(Me.txtMovieSetPosterHeight, 3, 4)
        Me.tblMovieSetImagesPosterOpts.Controls.Add(Me.txtMovieSetPosterWidth, 1, 4)
        Me.tblMovieSetImagesPosterOpts.Controls.Add(Me.lblMovieSetPosterHeight, 2, 4)
        Me.tblMovieSetImagesPosterOpts.Controls.Add(Me.chkMovieSetPosterPrefOnly, 2, 1)
        Me.tblMovieSetImagesPosterOpts.Controls.Add(Me.lblMovieSetPosterSize, 0, 0)
        Me.tblMovieSetImagesPosterOpts.Controls.Add(Me.lblMovieSetPosterWidth, 0, 4)
        Me.tblMovieSetImagesPosterOpts.Controls.Add(Me.cbMovieSetPosterPrefSize, 0, 1)
        Me.tblMovieSetImagesPosterOpts.Controls.Add(Me.chkMovieSetPosterOverwrite, 0, 2)
        Me.tblMovieSetImagesPosterOpts.Controls.Add(Me.chkMovieSetPosterResize, 0, 3)
        Me.tblMovieSetImagesPosterOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetImagesPosterOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetImagesPosterOpts.Name = "tblMovieSetImagesPosterOpts"
        Me.tblMovieSetImagesPosterOpts.RowCount = 6
        Me.tblMovieSetImagesPosterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblMovieSetImagesPosterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesPosterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesPosterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesPosterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesPosterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesPosterOpts.Size = New System.Drawing.Size(275, 121)
        Me.tblMovieSetImagesPosterOpts.TabIndex = 16
        '
        'txtMovieSetPosterHeight
        '
        Me.txtMovieSetPosterHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieSetPosterHeight.Enabled = false
        Me.txtMovieSetPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieSetPosterHeight.Location = New System.Drawing.Point(232, 96)
        Me.txtMovieSetPosterHeight.Name = "txtMovieSetPosterHeight"
        Me.txtMovieSetPosterHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieSetPosterHeight.TabIndex = 8
        '
        'txtMovieSetPosterWidth
        '
        Me.txtMovieSetPosterWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieSetPosterWidth.Enabled = false
        Me.txtMovieSetPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieSetPosterWidth.Location = New System.Drawing.Point(75, 96)
        Me.txtMovieSetPosterWidth.Name = "txtMovieSetPosterWidth"
        Me.txtMovieSetPosterWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieSetPosterWidth.TabIndex = 6
        '
        'lblMovieSetPosterHeight
        '
        Me.lblMovieSetPosterHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSetPosterHeight.AutoSize = true
        Me.lblMovieSetPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieSetPosterHeight.Location = New System.Drawing.Point(157, 100)
        Me.lblMovieSetPosterHeight.Name = "lblMovieSetPosterHeight"
        Me.lblMovieSetPosterHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblMovieSetPosterHeight.TabIndex = 7
        Me.lblMovieSetPosterHeight.Text = "Max Height:"
        '
        'chkMovieSetPosterPrefOnly
        '
        Me.chkMovieSetPosterPrefOnly.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSetPosterPrefOnly.AutoSize = true
        Me.tblMovieSetImagesPosterOpts.SetColumnSpan(Me.chkMovieSetPosterPrefOnly, 2)
        Me.chkMovieSetPosterPrefOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetPosterPrefOnly.Location = New System.Drawing.Point(157, 25)
        Me.chkMovieSetPosterPrefOnly.Name = "chkMovieSetPosterPrefOnly"
        Me.chkMovieSetPosterPrefOnly.Size = New System.Drawing.Size(50, 17)
        Me.chkMovieSetPosterPrefOnly.TabIndex = 2
        Me.chkMovieSetPosterPrefOnly.Text = "Only"
        Me.chkMovieSetPosterPrefOnly.UseVisualStyleBackColor = true
        '
        'lblMovieSetPosterSize
        '
        Me.lblMovieSetPosterSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSetPosterSize.AutoSize = true
        Me.tblMovieSetImagesPosterOpts.SetColumnSpan(Me.lblMovieSetPosterSize, 4)
        Me.lblMovieSetPosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieSetPosterSize.Location = New System.Drawing.Point(3, 3)
        Me.lblMovieSetPosterSize.Name = "lblMovieSetPosterSize"
        Me.lblMovieSetPosterSize.Size = New System.Drawing.Size(80, 13)
        Me.lblMovieSetPosterSize.TabIndex = 0
        Me.lblMovieSetPosterSize.Text = "Preferred Size:"
        '
        'lblMovieSetPosterWidth
        '
        Me.lblMovieSetPosterWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSetPosterWidth.AutoSize = true
        Me.lblMovieSetPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieSetPosterWidth.Location = New System.Drawing.Point(3, 100)
        Me.lblMovieSetPosterWidth.Name = "lblMovieSetPosterWidth"
        Me.lblMovieSetPosterWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblMovieSetPosterWidth.TabIndex = 5
        Me.lblMovieSetPosterWidth.Text = "Max Width:"
        '
        'cbMovieSetPosterPrefSize
        '
        Me.cbMovieSetPosterPrefSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblMovieSetImagesPosterOpts.SetColumnSpan(Me.cbMovieSetPosterPrefSize, 2)
        Me.cbMovieSetPosterPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieSetPosterPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieSetPosterPrefSize.FormattingEnabled = true
        Me.cbMovieSetPosterPrefSize.Location = New System.Drawing.Point(3, 23)
        Me.cbMovieSetPosterPrefSize.Name = "cbMovieSetPosterPrefSize"
        Me.cbMovieSetPosterPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbMovieSetPosterPrefSize.TabIndex = 1
        '
        'chkMovieSetPosterOverwrite
        '
        Me.chkMovieSetPosterOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSetPosterOverwrite.AutoSize = true
        Me.tblMovieSetImagesPosterOpts.SetColumnSpan(Me.chkMovieSetPosterOverwrite, 4)
        Me.chkMovieSetPosterOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetPosterOverwrite.Location = New System.Drawing.Point(3, 50)
        Me.chkMovieSetPosterOverwrite.Name = "chkMovieSetPosterOverwrite"
        Me.chkMovieSetPosterOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieSetPosterOverwrite.TabIndex = 3
        Me.chkMovieSetPosterOverwrite.Text = "Overwrite Existing"
        Me.chkMovieSetPosterOverwrite.UseVisualStyleBackColor = true
        '
        'chkMovieSetPosterResize
        '
        Me.chkMovieSetPosterResize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSetPosterResize.AutoSize = true
        Me.tblMovieSetImagesPosterOpts.SetColumnSpan(Me.chkMovieSetPosterResize, 4)
        Me.chkMovieSetPosterResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetPosterResize.Location = New System.Drawing.Point(3, 73)
        Me.chkMovieSetPosterResize.Name = "chkMovieSetPosterResize"
        Me.chkMovieSetPosterResize.Size = New System.Drawing.Size(133, 17)
        Me.chkMovieSetPosterResize.TabIndex = 4
        Me.chkMovieSetPosterResize.Text = "Automatically Resize:"
        Me.chkMovieSetPosterResize.UseVisualStyleBackColor = true
        '
        'gbMovieSetImagesClearLogoOpts
        '
        Me.gbMovieSetImagesClearLogoOpts.AutoSize = true
        Me.gbMovieSetImagesClearLogoOpts.Controls.Add(Me.tblMovieSetImagesClearLogoOpts)
        Me.gbMovieSetImagesClearLogoOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetImagesClearLogoOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieSetImagesClearLogoOpts.Location = New System.Drawing.Point(290, 151)
        Me.gbMovieSetImagesClearLogoOpts.Name = "gbMovieSetImagesClearLogoOpts"
        Me.gbMovieSetImagesClearLogoOpts.Size = New System.Drawing.Size(281, 44)
        Me.gbMovieSetImagesClearLogoOpts.TabIndex = 15
        Me.gbMovieSetImagesClearLogoOpts.TabStop = false
        Me.gbMovieSetImagesClearLogoOpts.Text = "ClearLogo"
        '
        'tblMovieSetImagesClearLogoOpts
        '
        Me.tblMovieSetImagesClearLogoOpts.AutoSize = true
        Me.tblMovieSetImagesClearLogoOpts.ColumnCount = 1
        Me.tblMovieSetImagesClearLogoOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesClearLogoOpts.Controls.Add(Me.chkMovieSetClearLogoOverwrite, 0, 0)
        Me.tblMovieSetImagesClearLogoOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetImagesClearLogoOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetImagesClearLogoOpts.Name = "tblMovieSetImagesClearLogoOpts"
        Me.tblMovieSetImagesClearLogoOpts.RowCount = 1
        Me.tblMovieSetImagesClearLogoOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesClearLogoOpts.Size = New System.Drawing.Size(275, 23)
        Me.tblMovieSetImagesClearLogoOpts.TabIndex = 16
        '
        'chkMovieSetClearLogoOverwrite
        '
        Me.chkMovieSetClearLogoOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSetClearLogoOverwrite.AutoSize = true
        Me.chkMovieSetClearLogoOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetClearLogoOverwrite.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieSetClearLogoOverwrite.Name = "chkMovieSetClearLogoOverwrite"
        Me.chkMovieSetClearLogoOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieSetClearLogoOverwrite.TabIndex = 4
        Me.chkMovieSetClearLogoOverwrite.Text = "Overwrite Existing"
        Me.chkMovieSetClearLogoOverwrite.UseVisualStyleBackColor = true
        '
        'gbMovieSetImagesClearArtOpts
        '
        Me.gbMovieSetImagesClearArtOpts.AutoSize = true
        Me.gbMovieSetImagesClearArtOpts.Controls.Add(Me.tblMovieSetImagesClearArtOpts)
        Me.gbMovieSetImagesClearArtOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetImagesClearArtOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieSetImagesClearArtOpts.Location = New System.Drawing.Point(3, 151)
        Me.gbMovieSetImagesClearArtOpts.Name = "gbMovieSetImagesClearArtOpts"
        Me.gbMovieSetImagesClearArtOpts.Size = New System.Drawing.Size(281, 44)
        Me.gbMovieSetImagesClearArtOpts.TabIndex = 15
        Me.gbMovieSetImagesClearArtOpts.TabStop = false
        Me.gbMovieSetImagesClearArtOpts.Text = "ClearArt"
        '
        'tblMovieSetImagesClearArtOpts
        '
        Me.tblMovieSetImagesClearArtOpts.AutoSize = true
        Me.tblMovieSetImagesClearArtOpts.ColumnCount = 1
        Me.tblMovieSetImagesClearArtOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesClearArtOpts.Controls.Add(Me.chkMovieSetClearArtOverwrite, 0, 0)
        Me.tblMovieSetImagesClearArtOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetImagesClearArtOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetImagesClearArtOpts.Name = "tblMovieSetImagesClearArtOpts"
        Me.tblMovieSetImagesClearArtOpts.RowCount = 1
        Me.tblMovieSetImagesClearArtOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesClearArtOpts.Size = New System.Drawing.Size(275, 23)
        Me.tblMovieSetImagesClearArtOpts.TabIndex = 16
        '
        'chkMovieSetClearArtOverwrite
        '
        Me.chkMovieSetClearArtOverwrite.AutoSize = true
        Me.chkMovieSetClearArtOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetClearArtOverwrite.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieSetClearArtOverwrite.Name = "chkMovieSetClearArtOverwrite"
        Me.chkMovieSetClearArtOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieSetClearArtOverwrite.TabIndex = 4
        Me.chkMovieSetClearArtOverwrite.Text = "Overwrite Existing"
        Me.chkMovieSetClearArtOverwrite.UseVisualStyleBackColor = true
        '
        'gbMovieSetImagesBannerOpts
        '
        Me.gbMovieSetImagesBannerOpts.AutoSize = true
        Me.gbMovieSetImagesBannerOpts.Controls.Add(Me.tblMovieSetImagesBannerOpts)
        Me.gbMovieSetImagesBannerOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetImagesBannerOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieSetImagesBannerOpts.Location = New System.Drawing.Point(290, 3)
        Me.gbMovieSetImagesBannerOpts.Name = "gbMovieSetImagesBannerOpts"
        Me.gbMovieSetImagesBannerOpts.Size = New System.Drawing.Size(281, 142)
        Me.gbMovieSetImagesBannerOpts.TabIndex = 12
        Me.gbMovieSetImagesBannerOpts.TabStop = false
        Me.gbMovieSetImagesBannerOpts.Text = "Banner"
        '
        'tblMovieSetImagesBannerOpts
        '
        Me.tblMovieSetImagesBannerOpts.AutoSize = true
        Me.tblMovieSetImagesBannerOpts.ColumnCount = 5
        Me.tblMovieSetImagesBannerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesBannerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesBannerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesBannerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesBannerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesBannerOpts.Controls.Add(Me.txtMovieSetBannerHeight, 3, 4)
        Me.tblMovieSetImagesBannerOpts.Controls.Add(Me.txtMovieSetBannerWidth, 1, 4)
        Me.tblMovieSetImagesBannerOpts.Controls.Add(Me.lblMovieSetBannerHeight, 2, 4)
        Me.tblMovieSetImagesBannerOpts.Controls.Add(Me.chkMovieSetBannerPrefOnly, 2, 1)
        Me.tblMovieSetImagesBannerOpts.Controls.Add(Me.lblMovieSetBannerType, 0, 0)
        Me.tblMovieSetImagesBannerOpts.Controls.Add(Me.lblMovieSetBannerWidth, 0, 4)
        Me.tblMovieSetImagesBannerOpts.Controls.Add(Me.cbMovieSetBannerPrefType, 0, 1)
        Me.tblMovieSetImagesBannerOpts.Controls.Add(Me.chkMovieSetBannerOverwrite, 0, 2)
        Me.tblMovieSetImagesBannerOpts.Controls.Add(Me.chkMovieSetBannerResize, 0, 3)
        Me.tblMovieSetImagesBannerOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetImagesBannerOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetImagesBannerOpts.Name = "tblMovieSetImagesBannerOpts"
        Me.tblMovieSetImagesBannerOpts.RowCount = 6
        Me.tblMovieSetImagesBannerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblMovieSetImagesBannerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesBannerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesBannerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesBannerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesBannerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesBannerOpts.Size = New System.Drawing.Size(275, 121)
        Me.tblMovieSetImagesBannerOpts.TabIndex = 16
        '
        'txtMovieSetBannerHeight
        '
        Me.txtMovieSetBannerHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieSetBannerHeight.Enabled = false
        Me.txtMovieSetBannerHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieSetBannerHeight.Location = New System.Drawing.Point(232, 96)
        Me.txtMovieSetBannerHeight.Name = "txtMovieSetBannerHeight"
        Me.txtMovieSetBannerHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieSetBannerHeight.TabIndex = 8
        '
        'txtMovieSetBannerWidth
        '
        Me.txtMovieSetBannerWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieSetBannerWidth.Enabled = false
        Me.txtMovieSetBannerWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieSetBannerWidth.Location = New System.Drawing.Point(92, 96)
        Me.txtMovieSetBannerWidth.Name = "txtMovieSetBannerWidth"
        Me.txtMovieSetBannerWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieSetBannerWidth.TabIndex = 6
        '
        'lblMovieSetBannerHeight
        '
        Me.lblMovieSetBannerHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSetBannerHeight.AutoSize = true
        Me.lblMovieSetBannerHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieSetBannerHeight.Location = New System.Drawing.Point(157, 100)
        Me.lblMovieSetBannerHeight.Name = "lblMovieSetBannerHeight"
        Me.lblMovieSetBannerHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblMovieSetBannerHeight.TabIndex = 7
        Me.lblMovieSetBannerHeight.Text = "Max Height:"
        '
        'chkMovieSetBannerPrefOnly
        '
        Me.chkMovieSetBannerPrefOnly.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSetBannerPrefOnly.AutoSize = true
        Me.tblMovieSetImagesBannerOpts.SetColumnSpan(Me.chkMovieSetBannerPrefOnly, 2)
        Me.chkMovieSetBannerPrefOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetBannerPrefOnly.Location = New System.Drawing.Point(157, 25)
        Me.chkMovieSetBannerPrefOnly.Name = "chkMovieSetBannerPrefOnly"
        Me.chkMovieSetBannerPrefOnly.Size = New System.Drawing.Size(50, 17)
        Me.chkMovieSetBannerPrefOnly.TabIndex = 2
        Me.chkMovieSetBannerPrefOnly.Text = "Only"
        Me.chkMovieSetBannerPrefOnly.UseVisualStyleBackColor = true
        '
        'lblMovieSetBannerType
        '
        Me.lblMovieSetBannerType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSetBannerType.AutoSize = true
        Me.lblMovieSetBannerType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieSetBannerType.Location = New System.Drawing.Point(3, 3)
        Me.lblMovieSetBannerType.Name = "lblMovieSetBannerType"
        Me.lblMovieSetBannerType.Size = New System.Drawing.Size(83, 13)
        Me.lblMovieSetBannerType.TabIndex = 0
        Me.lblMovieSetBannerType.Text = "Preferred Type:"
        '
        'lblMovieSetBannerWidth
        '
        Me.lblMovieSetBannerWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSetBannerWidth.AutoSize = true
        Me.lblMovieSetBannerWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieSetBannerWidth.Location = New System.Drawing.Point(3, 100)
        Me.lblMovieSetBannerWidth.Name = "lblMovieSetBannerWidth"
        Me.lblMovieSetBannerWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblMovieSetBannerWidth.TabIndex = 5
        Me.lblMovieSetBannerWidth.Text = "Max Width:"
        '
        'cbMovieSetBannerPrefType
        '
        Me.cbMovieSetBannerPrefType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblMovieSetImagesBannerOpts.SetColumnSpan(Me.cbMovieSetBannerPrefType, 2)
        Me.cbMovieSetBannerPrefType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieSetBannerPrefType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieSetBannerPrefType.FormattingEnabled = true
        Me.cbMovieSetBannerPrefType.Location = New System.Drawing.Point(3, 23)
        Me.cbMovieSetBannerPrefType.Name = "cbMovieSetBannerPrefType"
        Me.cbMovieSetBannerPrefType.Size = New System.Drawing.Size(148, 21)
        Me.cbMovieSetBannerPrefType.TabIndex = 1
        '
        'chkMovieSetBannerOverwrite
        '
        Me.chkMovieSetBannerOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSetBannerOverwrite.AutoSize = true
        Me.tblMovieSetImagesBannerOpts.SetColumnSpan(Me.chkMovieSetBannerOverwrite, 4)
        Me.chkMovieSetBannerOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetBannerOverwrite.Location = New System.Drawing.Point(3, 50)
        Me.chkMovieSetBannerOverwrite.Name = "chkMovieSetBannerOverwrite"
        Me.chkMovieSetBannerOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieSetBannerOverwrite.TabIndex = 3
        Me.chkMovieSetBannerOverwrite.Text = "Overwrite Existing"
        Me.chkMovieSetBannerOverwrite.UseVisualStyleBackColor = true
        '
        'chkMovieSetBannerResize
        '
        Me.chkMovieSetBannerResize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSetBannerResize.AutoSize = true
        Me.tblMovieSetImagesBannerOpts.SetColumnSpan(Me.chkMovieSetBannerResize, 4)
        Me.chkMovieSetBannerResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetBannerResize.Location = New System.Drawing.Point(3, 73)
        Me.chkMovieSetBannerResize.Name = "chkMovieSetBannerResize"
        Me.chkMovieSetBannerResize.Size = New System.Drawing.Size(133, 17)
        Me.chkMovieSetBannerResize.TabIndex = 4
        Me.chkMovieSetBannerResize.Text = "Automatically Resize:"
        Me.chkMovieSetBannerResize.UseVisualStyleBackColor = true
        '
        'gbMovieSetImagesFanartOpts
        '
        Me.gbMovieSetImagesFanartOpts.AutoSize = true
        Me.gbMovieSetImagesFanartOpts.Controls.Add(Me.tblMovieSetImagesFanartOpts)
        Me.gbMovieSetImagesFanartOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetImagesFanartOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieSetImagesFanartOpts.Location = New System.Drawing.Point(577, 3)
        Me.gbMovieSetImagesFanartOpts.Name = "gbMovieSetImagesFanartOpts"
        Me.gbMovieSetImagesFanartOpts.Size = New System.Drawing.Size(281, 142)
        Me.gbMovieSetImagesFanartOpts.TabIndex = 3
        Me.gbMovieSetImagesFanartOpts.TabStop = false
        Me.gbMovieSetImagesFanartOpts.Text = "Fanart"
        '
        'tblMovieSetImagesFanartOpts
        '
        Me.tblMovieSetImagesFanartOpts.AutoSize = true
        Me.tblMovieSetImagesFanartOpts.ColumnCount = 5
        Me.tblMovieSetImagesFanartOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesFanartOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesFanartOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesFanartOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesFanartOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesFanartOpts.Controls.Add(Me.txtMovieSetFanartHeight, 3, 4)
        Me.tblMovieSetImagesFanartOpts.Controls.Add(Me.txtMovieSetFanartWidth, 1, 4)
        Me.tblMovieSetImagesFanartOpts.Controls.Add(Me.lblMovieSetFanartHeight, 2, 4)
        Me.tblMovieSetImagesFanartOpts.Controls.Add(Me.lblMovieSetFanartSize, 0, 0)
        Me.tblMovieSetImagesFanartOpts.Controls.Add(Me.cbMovieSetFanartPrefSize, 0, 1)
        Me.tblMovieSetImagesFanartOpts.Controls.Add(Me.lblMovieSetFanartWidth, 0, 4)
        Me.tblMovieSetImagesFanartOpts.Controls.Add(Me.chkMovieSetFanartPrefOnly, 2, 1)
        Me.tblMovieSetImagesFanartOpts.Controls.Add(Me.chkMovieSetFanartOverwrite, 0, 2)
        Me.tblMovieSetImagesFanartOpts.Controls.Add(Me.chkMovieSetFanartResize, 0, 3)
        Me.tblMovieSetImagesFanartOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetImagesFanartOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetImagesFanartOpts.Name = "tblMovieSetImagesFanartOpts"
        Me.tblMovieSetImagesFanartOpts.RowCount = 6
        Me.tblMovieSetImagesFanartOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.tblMovieSetImagesFanartOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesFanartOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesFanartOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesFanartOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesFanartOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesFanartOpts.Size = New System.Drawing.Size(275, 121)
        Me.tblMovieSetImagesFanartOpts.TabIndex = 16
        '
        'txtMovieSetFanartHeight
        '
        Me.txtMovieSetFanartHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieSetFanartHeight.Enabled = false
        Me.txtMovieSetFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieSetFanartHeight.Location = New System.Drawing.Point(232, 96)
        Me.txtMovieSetFanartHeight.Name = "txtMovieSetFanartHeight"
        Me.txtMovieSetFanartHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieSetFanartHeight.TabIndex = 8
        '
        'txtMovieSetFanartWidth
        '
        Me.txtMovieSetFanartWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieSetFanartWidth.Enabled = false
        Me.txtMovieSetFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieSetFanartWidth.Location = New System.Drawing.Point(75, 96)
        Me.txtMovieSetFanartWidth.Name = "txtMovieSetFanartWidth"
        Me.txtMovieSetFanartWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieSetFanartWidth.TabIndex = 6
        '
        'lblMovieSetFanartHeight
        '
        Me.lblMovieSetFanartHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSetFanartHeight.AutoSize = true
        Me.lblMovieSetFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieSetFanartHeight.Location = New System.Drawing.Point(157, 100)
        Me.lblMovieSetFanartHeight.Name = "lblMovieSetFanartHeight"
        Me.lblMovieSetFanartHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblMovieSetFanartHeight.TabIndex = 7
        Me.lblMovieSetFanartHeight.Text = "Max Height:"
        '
        'lblMovieSetFanartSize
        '
        Me.lblMovieSetFanartSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSetFanartSize.AutoSize = true
        Me.tblMovieSetImagesFanartOpts.SetColumnSpan(Me.lblMovieSetFanartSize, 4)
        Me.lblMovieSetFanartSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieSetFanartSize.Location = New System.Drawing.Point(3, 3)
        Me.lblMovieSetFanartSize.Name = "lblMovieSetFanartSize"
        Me.lblMovieSetFanartSize.Size = New System.Drawing.Size(80, 13)
        Me.lblMovieSetFanartSize.TabIndex = 0
        Me.lblMovieSetFanartSize.Text = "Preferred Size:"
        '
        'cbMovieSetFanartPrefSize
        '
        Me.cbMovieSetFanartPrefSize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblMovieSetImagesFanartOpts.SetColumnSpan(Me.cbMovieSetFanartPrefSize, 2)
        Me.cbMovieSetFanartPrefSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieSetFanartPrefSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieSetFanartPrefSize.FormattingEnabled = true
        Me.cbMovieSetFanartPrefSize.Location = New System.Drawing.Point(3, 23)
        Me.cbMovieSetFanartPrefSize.Name = "cbMovieSetFanartPrefSize"
        Me.cbMovieSetFanartPrefSize.Size = New System.Drawing.Size(148, 21)
        Me.cbMovieSetFanartPrefSize.TabIndex = 1
        '
        'lblMovieSetFanartWidth
        '
        Me.lblMovieSetFanartWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSetFanartWidth.AutoSize = true
        Me.lblMovieSetFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMovieSetFanartWidth.Location = New System.Drawing.Point(3, 100)
        Me.lblMovieSetFanartWidth.Name = "lblMovieSetFanartWidth"
        Me.lblMovieSetFanartWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblMovieSetFanartWidth.TabIndex = 5
        Me.lblMovieSetFanartWidth.Text = "Max Width:"
        '
        'chkMovieSetFanartPrefOnly
        '
        Me.chkMovieSetFanartPrefOnly.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSetFanartPrefOnly.AutoSize = true
        Me.tblMovieSetImagesFanartOpts.SetColumnSpan(Me.chkMovieSetFanartPrefOnly, 2)
        Me.chkMovieSetFanartPrefOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetFanartPrefOnly.Location = New System.Drawing.Point(157, 25)
        Me.chkMovieSetFanartPrefOnly.Name = "chkMovieSetFanartPrefOnly"
        Me.chkMovieSetFanartPrefOnly.Size = New System.Drawing.Size(50, 17)
        Me.chkMovieSetFanartPrefOnly.TabIndex = 2
        Me.chkMovieSetFanartPrefOnly.Text = "Only"
        Me.chkMovieSetFanartPrefOnly.UseVisualStyleBackColor = true
        '
        'chkMovieSetFanartOverwrite
        '
        Me.chkMovieSetFanartOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSetFanartOverwrite.AutoSize = true
        Me.tblMovieSetImagesFanartOpts.SetColumnSpan(Me.chkMovieSetFanartOverwrite, 4)
        Me.chkMovieSetFanartOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetFanartOverwrite.Location = New System.Drawing.Point(3, 50)
        Me.chkMovieSetFanartOverwrite.Name = "chkMovieSetFanartOverwrite"
        Me.chkMovieSetFanartOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieSetFanartOverwrite.TabIndex = 3
        Me.chkMovieSetFanartOverwrite.Text = "Overwrite Existing"
        Me.chkMovieSetFanartOverwrite.UseVisualStyleBackColor = true
        '
        'chkMovieSetFanartResize
        '
        Me.chkMovieSetFanartResize.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSetFanartResize.AutoSize = true
        Me.tblMovieSetImagesFanartOpts.SetColumnSpan(Me.chkMovieSetFanartResize, 4)
        Me.chkMovieSetFanartResize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetFanartResize.Location = New System.Drawing.Point(3, 73)
        Me.chkMovieSetFanartResize.Name = "chkMovieSetFanartResize"
        Me.chkMovieSetFanartResize.Size = New System.Drawing.Size(133, 17)
        Me.chkMovieSetFanartResize.TabIndex = 4
        Me.chkMovieSetFanartResize.Text = "Automatically Resize:"
        Me.chkMovieSetFanartResize.UseVisualStyleBackColor = true
        '
        'gbMovieSetImagesLandscapeOpts
        '
        Me.gbMovieSetImagesLandscapeOpts.AutoSize = true
        Me.gbMovieSetImagesLandscapeOpts.Controls.Add(Me.tblMovieSetImagesLandscapeOpts)
        Me.gbMovieSetImagesLandscapeOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetImagesLandscapeOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieSetImagesLandscapeOpts.Location = New System.Drawing.Point(577, 151)
        Me.gbMovieSetImagesLandscapeOpts.Name = "gbMovieSetImagesLandscapeOpts"
        Me.gbMovieSetImagesLandscapeOpts.Size = New System.Drawing.Size(281, 44)
        Me.gbMovieSetImagesLandscapeOpts.TabIndex = 14
        Me.gbMovieSetImagesLandscapeOpts.TabStop = false
        Me.gbMovieSetImagesLandscapeOpts.Text = "Landscape"
        '
        'tblMovieSetImagesLandscapeOpts
        '
        Me.tblMovieSetImagesLandscapeOpts.AutoSize = true
        Me.tblMovieSetImagesLandscapeOpts.ColumnCount = 1
        Me.tblMovieSetImagesLandscapeOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetImagesLandscapeOpts.Controls.Add(Me.chkMovieSetLandscapeOverwrite, 0, 0)
        Me.tblMovieSetImagesLandscapeOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetImagesLandscapeOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetImagesLandscapeOpts.Name = "tblMovieSetImagesLandscapeOpts"
        Me.tblMovieSetImagesLandscapeOpts.RowCount = 1
        Me.tblMovieSetImagesLandscapeOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetImagesLandscapeOpts.Size = New System.Drawing.Size(275, 23)
        Me.tblMovieSetImagesLandscapeOpts.TabIndex = 16
        '
        'chkMovieSetLandscapeOverwrite
        '
        Me.chkMovieSetLandscapeOverwrite.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSetLandscapeOverwrite.AutoSize = true
        Me.chkMovieSetLandscapeOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSetLandscapeOverwrite.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieSetLandscapeOverwrite.Name = "chkMovieSetLandscapeOverwrite"
        Me.chkMovieSetLandscapeOverwrite.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieSetLandscapeOverwrite.TabIndex = 4
        Me.chkMovieSetLandscapeOverwrite.Text = "Overwrite Existing"
        Me.chkMovieSetLandscapeOverwrite.UseVisualStyleBackColor = true
        '
        'scSettings
        '
        Me.scSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scSettings.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.scSettings.IsSplitterFixed = true
        Me.scSettings.Location = New System.Drawing.Point(0, 0)
        Me.scSettings.Name = "scSettings"
        Me.scSettings.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scSettings.Panel1
        '
        Me.scSettings.Panel1.Controls.Add(Me.tblSettingsCurrent)
        Me.scSettings.Panel1.Controls.Add(Me.tsSettingsTopMenu)
        Me.scSettings.Panel1.Controls.Add(Me.pnlSettingsTop)
        '
        'scSettings.Panel2
        '
        Me.scSettings.Panel2.Controls.Add(Me.scSettingsBody)
        Me.scSettings.Size = New System.Drawing.Size(1008, 729)
        Me.scSettings.SplitterDistance = 145
        Me.scSettings.TabIndex = 28
        '
        'tblSettingsCurrent
        '
        Me.tblSettingsCurrent.ColumnCount = 1
        Me.tblSettingsCurrent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.tblSettingsCurrent.Controls.Add(Me.pnlSettingsCurrent, 0, 0)
        Me.tblSettingsCurrent.Dock = System.Windows.Forms.DockStyle.Top
        Me.tblSettingsCurrent.Location = New System.Drawing.Point(0, 89)
        Me.tblSettingsCurrent.Name = "tblSettingsCurrent"
        Me.tblSettingsCurrent.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.tblSettingsCurrent.RowCount = 1
        Me.tblSettingsCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.tblSettingsCurrent.Size = New System.Drawing.Size(1008, 33)
        Me.tblSettingsCurrent.TabIndex = 6
        '
        'scSettingsBody
        '
        Me.scSettingsBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scSettingsBody.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.scSettingsBody.IsSplitterFixed = true
        Me.scSettingsBody.Location = New System.Drawing.Point(0, 0)
        Me.scSettingsBody.Name = "scSettingsBody"
        Me.scSettingsBody.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scSettingsBody.Panel1
        '
        Me.scSettingsBody.Panel1.Controls.Add(Me.scSettingsMain)
        '
        'scSettingsBody.Panel2
        '
        Me.scSettingsBody.Panel2.Controls.Add(Me.tblSettingsFooter)
        Me.scSettingsBody.Panel2.Padding = New System.Windows.Forms.Padding(2, 0, 5, 0)
        Me.scSettingsBody.Size = New System.Drawing.Size(1008, 580)
        Me.scSettingsBody.SplitterDistance = 502
        Me.scSettingsBody.TabIndex = 99
        '
        'scSettingsMain
        '
        Me.scSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scSettingsMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.scSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.scSettingsMain.Name = "scSettingsMain"
        '
        'scSettingsMain.Panel1
        '
        Me.scSettingsMain.Panel1.Controls.Add(Me.tvSettingsList)
        Me.scSettingsMain.Panel1.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        '
        'scSettingsMain.Panel2
        '
        Me.scSettingsMain.Panel2.Controls.Add(Me.pnlSettingsMain)
        Me.scSettingsMain.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.scSettingsMain.Size = New System.Drawing.Size(1008, 502)
        Me.scSettingsMain.SplitterDistance = 247
        Me.scSettingsMain.TabIndex = 0
        '
        'tblSettingsFooter
        '
        Me.tblSettingsFooter.ColumnCount = 5
        Me.tblSettingsFooter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80!))
        Me.tblSettingsFooter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20!))
        Me.tblSettingsFooter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80!))
        Me.tblSettingsFooter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80!))
        Me.tblSettingsFooter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80!))
        Me.tblSettingsFooter.Controls.Add(Me.btnApply, 2, 0)
        Me.tblSettingsFooter.Controls.Add(Me.btnCancel, 3, 0)
        Me.tblSettingsFooter.Controls.Add(Me.pnlSettingsHelp, 0, 0)
        Me.tblSettingsFooter.Controls.Add(Me.btnOK, 4, 0)
        Me.tblSettingsFooter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsFooter.Location = New System.Drawing.Point(2, 0)
        Me.tblSettingsFooter.Name = "tblSettingsFooter"
        Me.tblSettingsFooter.RowCount = 1
        Me.tblSettingsFooter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.tblSettingsFooter.Size = New System.Drawing.Size(1001, 74)
        Me.tblSettingsFooter.TabIndex = 0
        '
        'dlgSettings
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96!, 96!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.pnlMovieSetScraper)
        Me.Controls.Add(Me.pnlGeneral)
        Me.Controls.Add(Me.pnlMovieSetImages)
        Me.Controls.Add(Me.pnlMovieGeneral)
        Me.Controls.Add(Me.pnlMovieImages)
        Me.Controls.Add(Me.pnlMovieScraper)
        Me.Controls.Add(Me.scSettings)
        Me.Controls.Add(Me.pnlTVGeneral)
        Me.Controls.Add(Me.pnlMovieSetSources)
        Me.Controls.Add(Me.pnlMovieSetGeneral)
        Me.Controls.Add(Me.pnlTVThemes)
        Me.Controls.Add(Me.pnlMovieThemes)
        Me.Controls.Add(Me.pnlProxy)
        Me.Controls.Add(Me.pnlTVScraper)
        Me.Controls.Add(Me.pnlMovieTrailers)
        Me.Controls.Add(Me.pnlTVImages)
        Me.Controls.Add(Me.pnlTVSources)
        Me.Controls.Add(Me.pnlMovieSources)
        Me.Controls.Add(Me.pnlFileSystem)
        Me.DoubleBuffered = true
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MinimizeBox = false
        Me.Name = "dlgSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings"
        Me.gbGeneralMisc.ResumeLayout(false)
        Me.gbGeneralMisc.PerformLayout
        Me.tblGeneralMisc.ResumeLayout(false)
        Me.tblGeneralMisc.PerformLayout
        Me.gbGeneralDaemon.ResumeLayout(false)
        Me.gbGeneralDaemon.PerformLayout
        Me.tblGeneralDaemon.ResumeLayout(false)
        Me.tblGeneralDaemon.PerformLayout
        Me.gbGeneralThemes.ResumeLayout(false)
        Me.gbGeneralThemes.PerformLayout
        Me.tblGeneralThemes.ResumeLayout(false)
        Me.tblGeneralThemes.PerformLayout
        Me.gbFileSystemCleanFiles.ResumeLayout(false)
        Me.tcFileSystemCleaner.ResumeLayout(false)
        Me.tpFileSystemCleanerStandard.ResumeLayout(false)
        Me.tpFileSystemCleanerStandard.PerformLayout
        Me.tpFileSystemCleanerExpert.ResumeLayout(false)
        Me.tpFileSystemCleanerExpert.PerformLayout
        Me.gbMovieGeneralMiscOpts.ResumeLayout(false)
        Me.gbMovieGeneralMiscOpts.PerformLayout
        Me.pnlMovieImages.ResumeLayout(false)
        Me.pnlMovieImages.PerformLayout
        Me.tblMovieImages.ResumeLayout(false)
        Me.tblMovieImages.PerformLayout
        Me.gbMovieImagesOpts.ResumeLayout(false)
        Me.gbMovieImagesOpts.PerformLayout
        Me.tblMovieImagesOpts.ResumeLayout(false)
        Me.tblMovieImagesOpts.PerformLayout
        Me.gbMovieImagesLandscapeOpts.ResumeLayout(false)
        Me.gbMovieImagesLandscapeOpts.PerformLayout
        Me.tblMovieImagesLandscapeOpts.ResumeLayout(false)
        Me.tblMovieImagesLandscapeOpts.PerformLayout
        Me.gbMovieImagesDiscArtOpts.ResumeLayout(false)
        Me.gbMovieImagesDiscArtOpts.PerformLayout
        Me.tblMovieImagesDiscArtOpts.ResumeLayout(false)
        Me.tblMovieImagesDiscArtOpts.PerformLayout
        Me.gbMovieImagesClearLogoOpts.ResumeLayout(false)
        Me.gbMovieImagesClearLogoOpts.PerformLayout
        Me.tblMovieImagesClearLogoOpts.ResumeLayout(false)
        Me.tblMovieImagesClearLogoOpts.PerformLayout
        Me.gbMovieImagesClearArtOpts.ResumeLayout(false)
        Me.gbMovieImagesClearArtOpts.PerformLayout
        Me.tblMovieImagesClearArtOpts.ResumeLayout(false)
        Me.tblMovieImagesClearArtOpts.PerformLayout
        Me.gbMovieImagesActorThumbsOpts.ResumeLayout(false)
        Me.gbMovieImagesActorThumbsOpts.PerformLayout
        Me.tblMovieImagesActorThumbsOpts.ResumeLayout(false)
        Me.tblMovieImagesActorThumbsOpts.PerformLayout
        Me.gbMovieImagesPosterOpts.ResumeLayout(false)
        Me.gbMovieImagesPosterOpts.PerformLayout
        Me.tblMovieImagesPosterOpts.ResumeLayout(false)
        Me.tblMovieImagesPosterOpts.PerformLayout
        Me.gbMovieImagesBannerOpts.ResumeLayout(false)
        Me.gbMovieImagesBannerOpts.PerformLayout
        Me.tblMovieImagesBannerOpts.ResumeLayout(false)
        Me.tblMovieImagesBannerOpts.PerformLayout
        Me.gbMovieImagesEThumbsOpts.ResumeLayout(false)
        Me.gbMovieImagesEThumbsOpts.PerformLayout
        Me.tblMovieImagesEThumbsOpts.ResumeLayout(false)
        Me.tblMovieImagesEThumbsOpts.PerformLayout
        Me.gbMovieImagesEFanartsOpts.ResumeLayout(false)
        Me.gbMovieImagesEFanartsOpts.PerformLayout
        Me.tblMovieImagesEFanartsOpts.ResumeLayout(false)
        Me.tblMovieImagesEFanartsOpts.PerformLayout
        Me.gbMovieImagesFanartOpts.ResumeLayout(false)
        Me.gbMovieImagesFanartOpts.PerformLayout
        Me.tblMovieImagesFanartOpts.ResumeLayout(false)
        Me.tblMovieImagesFanartOpts.PerformLayout
        Me.gbMovieGeneralMediaListOpts.ResumeLayout(false)
        Me.gbMovieGeneralMediaListOpts.PerformLayout
        Me.gbMovieSortTokensOpts.ResumeLayout(false)
        Me.gbMovieSortTokensOpts.PerformLayout
        Me.pnlSettingsTop.ResumeLayout(false)
        Me.pnlSettingsTop.PerformLayout
        CType(Me.pbSettingsTopLogo,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlGeneral.ResumeLayout(false)
        Me.pnlGeneral.PerformLayout
        Me.tblGeneral.ResumeLayout(false)
        Me.tblGeneral.PerformLayout
        Me.gbGeneralInterface.ResumeLayout(false)
        Me.gbGeneralInterface.PerformLayout
        Me.tblGeneralInterface.ResumeLayout(false)
        Me.tblGeneralInterface.PerformLayout
        Me.gbGeneralDateAdded.ResumeLayout(false)
        Me.gbGeneralDateAdded.PerformLayout
        Me.tblGeneralDateAdded.ResumeLayout(false)
        Me.tblGeneralDateAdded.PerformLayout
        Me.gbGeneralMainWindow.ResumeLayout(false)
        Me.gbGeneralMainWindow.PerformLayout
        Me.tblGeneralMainWindow.ResumeLayout(false)
        Me.tblGeneralMainWindow.PerformLayout
        Me.gbGeneralScrapers.ResumeLayout(false)
        Me.gbGeneralScrapers.PerformLayout
        Me.tblGeneralScrapers.ResumeLayout(false)
        Me.tblGeneralScrapers.PerformLayout
        Me.pnlMovieGeneral.ResumeLayout(false)
        Me.gbMovieGeneralCustomMarker.ResumeLayout(false)
        Me.gbMovieGeneralCustomMarker.PerformLayout
        Me.gbMovieGenrealIMDBMirrorOpts.ResumeLayout(false)
        Me.gbMovieGenrealIMDBMirrorOpts.PerformLayout
        Me.gbMovieGeneralGenreFilterOpts.ResumeLayout(false)
        Me.gbMovieGeneralFiltersOpts.ResumeLayout(false)
        Me.gbMovieGeneralFiltersOpts.PerformLayout
        Me.gbMovieGeneralMissingItemsOpts.ResumeLayout(false)
        Me.pnlFileSystem.ResumeLayout(false)
        Me.gbFileSystemValidSubtitlesExts.ResumeLayout(false)
        Me.gbFileSystemValidSubtitlesExts.PerformLayout
        Me.gbFileSystemExcludedDirs.ResumeLayout(false)
        Me.gbFileSystemExcludedDirs.PerformLayout
        Me.gbFileSystemValidThemeExts.ResumeLayout(false)
        Me.gbFileSystemValidThemeExts.PerformLayout
        Me.gbFileSystemNoStackExts.ResumeLayout(false)
        Me.gbFileSystemNoStackExts.PerformLayout
        Me.gbFileSystemValidExts.ResumeLayout(false)
        Me.gbFileSystemValidExts.PerformLayout
        Me.pnlProxy.ResumeLayout(false)
        Me.gbProxyOpts.ResumeLayout(false)
        Me.gbProxyOpts.PerformLayout
        Me.gbProxyCredsOpts.ResumeLayout(false)
        Me.gbProxyCredsOpts.PerformLayout
        Me.gbMovieBackdropsFolder.ResumeLayout(false)
        Me.gbMovieBackdropsFolder.PerformLayout
        Me.pnlSettingsCurrent.ResumeLayout(false)
        CType(Me.pbSettingsCurrent,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlMovieSources.ResumeLayout(false)
        Me.gbMovieFileNaming.ResumeLayout(false)
        Me.tcMovieFileNaming.ResumeLayout(false)
        Me.tpMovieFileNamingXBMC.ResumeLayout(false)
        Me.gbMovieXBMCTheme.ResumeLayout(false)
        Me.gbMovieXBMCTheme.PerformLayout
        Me.gbMovieXBMCOptionalSettings.ResumeLayout(false)
        Me.gbMovieXBMCOptionalSettings.PerformLayout
        Me.gbMovieEden.ResumeLayout(false)
        Me.gbMovieEden.PerformLayout
        Me.gbMovieFrodo.ResumeLayout(false)
        Me.gbMovieFrodo.PerformLayout
        Me.tpMovieFileNamingNMT.ResumeLayout(false)
        Me.gbMovieNMTOptionalSettings.ResumeLayout(false)
        Me.gbMovieNMTOptionalSettings.PerformLayout
        Me.gbMovieNMJ.ResumeLayout(false)
        Me.gbMovieNMJ.PerformLayout
        Me.gbMovieYAMJ.ResumeLayout(false)
        Me.gbMovieYAMJ.PerformLayout
        Me.tpMovieFileNamingBoxee.ResumeLayout(false)
        Me.gbMovieBoxee.ResumeLayout(false)
        Me.gbMovieBoxee.PerformLayout
        Me.tpMovieFileNamingExpert.ResumeLayout(false)
        Me.gbMovieExpert.ResumeLayout(false)
        Me.gbMovieExpert.PerformLayout
        Me.tcMovieFileNamingExpert.ResumeLayout(false)
        Me.tpMovieFileNamingExpertSingle.ResumeLayout(false)
        Me.tpMovieFileNamingExpertSingle.PerformLayout
        Me.gbMovieExpertSingleOptionalSettings.ResumeLayout(false)
        Me.gbMovieExpertSingleOptionalSettings.PerformLayout
        Me.gbMovieExpertSingleOptionalImages.ResumeLayout(false)
        Me.gbMovieExpertSingleOptionalImages.PerformLayout
        Me.tpMovieFileNamingExpertMulti.ResumeLayout(false)
        Me.tpMovieFileNamingExpertMulti.PerformLayout
        Me.gbMovieExpertMultiOptionalImages.ResumeLayout(false)
        Me.gbMovieExpertMultiOptionalImages.PerformLayout
        Me.gbMovieExpertMultiOptionalSettings.ResumeLayout(false)
        Me.gbMovieExpertMultiOptionalSettings.PerformLayout
        Me.tpMovieFileNamingExpertVTS.ResumeLayout(false)
        Me.tpMovieFileNamingExpertVTS.PerformLayout
        Me.gbMovieExpertVTSOptionalSettings.ResumeLayout(false)
        Me.gbMovieExpertVTSOptionalImages.ResumeLayout(false)
        Me.gbMovieExpertVTSOptionalImages.PerformLayout
        Me.tpMovieFileNamingExpertBDMV.ResumeLayout(false)
        Me.tpMovieFileNamingExpertBDMV.PerformLayout
        Me.gbMovieExpertBDMVOptionalSettings.ResumeLayout(false)
        Me.gbMovieExpertBDMVOptionalImages.ResumeLayout(false)
        Me.gbMovieExpertBDMVOptionalImages.PerformLayout
        Me.gbMovieSourcesMiscOpts.ResumeLayout(false)
        Me.gbMovieSourcesMiscOpts.PerformLayout
        Me.gbMovieSetMSAAPath.ResumeLayout(false)
        Me.gbMovieSetMSAAPath.PerformLayout
        Me.pnlTVGeneral.ResumeLayout(false)
        Me.gbTVGeneralMissingItemsOpts.ResumeLayout(false)
        Me.gbTVGeneralMissingShowOpts.ResumeLayout(false)
        Me.gbTVSortTokensOpts.ResumeLayout(false)
        Me.gbTVSortTokensOpts.PerformLayout
        Me.gbTVGeneralLangOpts.ResumeLayout(false)
        Me.gbTVGeneralMediaListOpts.ResumeLayout(false)
        Me.gbTVGeneralMediaListOpts.PerformLayout
        Me.gbTVGeneralListEpisodeOpts.ResumeLayout(false)
        Me.gbTVGeneralListEpisodeOpts.PerformLayout
        Me.gbTVGeneralListSeasonOpts.ResumeLayout(false)
        Me.gbTVGeneralListSeasonOpts.PerformLayout
        Me.gbTVGeneralListShowOpts.ResumeLayout(false)
        Me.gbTVGeneralListShowOpts.PerformLayout
        Me.gbTVEpisodeFilterOpts.ResumeLayout(false)
        Me.gbTVEpisodeFilterOpts.PerformLayout
        Me.gbTVGeneralMiscOpts.ResumeLayout(false)
        Me.gbTVShowFilterOpts.ResumeLayout(false)
        Me.gbTVShowFilterOpts.PerformLayout
        Me.pnlTVSources.ResumeLayout(false)
        Me.tcTVSources.ResumeLayout(false)
        Me.tpTVSourcesGeneral.ResumeLayout(false)
        Me.gbTVFileNaming.ResumeLayout(false)
        Me.tcTVFileNaming.ResumeLayout(false)
        Me.tpTVFileNamingXBMC.ResumeLayout(false)
        Me.gbTVXBMCAdditional.ResumeLayout(false)
        Me.gbTVXBMCAdditional.PerformLayout
        Me.gbTVFrodo.ResumeLayout(false)
        Me.gbTVFrodo.PerformLayout
        Me.tpTVFileNamingNMT.ResumeLayout(false)
        Me.gbTVNMT.ResumeLayout(false)
        Me.gbTVNMT.PerformLayout
        Me.gbTVYAMJ.ResumeLayout(false)
        Me.gbTVYAMJ.PerformLayout
        Me.tpTVFileNamingBoxee.ResumeLayout(false)
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.gbTVSourcesMiscOpts.ResumeLayout(false)
        Me.gbTVSourcesMiscOpts.PerformLayout
        Me.tpTVSourcesRegex.ResumeLayout(false)
        Me.gbTVShowRegex.ResumeLayout(false)
        Me.gbTVShowRegex.PerformLayout
        Me.pnlTVImages.ResumeLayout(false)
        Me.tcTVImages.ResumeLayout(false)
        Me.tpTVShow.ResumeLayout(false)
        Me.gbTVShowEFanartsOpts.ResumeLayout(false)
        Me.gbTVShowEFanartsOpts.PerformLayout
        Me.gbTVShowCharacterArtOpts.ResumeLayout(false)
        Me.gbTVShowCharacterArtOpts.PerformLayout
        Me.gbTVShowClearArtOpts.ResumeLayout(false)
        Me.gbTVShowClearArtOpts.PerformLayout
        Me.gbTVShowClearLogoOpts.ResumeLayout(false)
        Me.gbTVShowClearLogoOpts.PerformLayout
        Me.gbTVShowLandscapeOpts.ResumeLayout(false)
        Me.gbTVShowLandscapeOpts.PerformLayout
        Me.gbTVShowBannerOpts.ResumeLayout(false)
        Me.gbTVShowBannerOpts.PerformLayout
        Me.gbTVShowPosterOpts.ResumeLayout(false)
        Me.gbTVShowPosterOpts.PerformLayout
        Me.gbTVShowFanartOpts.ResumeLayout(false)
        Me.gbTVShowFanartOpts.PerformLayout
        Me.tpTVAllSeasons.ResumeLayout(false)
        Me.gbTVASLandscapeOpts.ResumeLayout(false)
        Me.gbTVASLandscapeOpts.PerformLayout
        Me.gbTVASFanartOpts.ResumeLayout(false)
        Me.gbTVASFanartOpts.PerformLayout
        Me.gbTVASBannerOpts.ResumeLayout(false)
        Me.gbTVASBannerOpts.PerformLayout
        Me.gbTVASPosterOpts.ResumeLayout(false)
        Me.gbTVASPosterOpts.PerformLayout
        Me.tpTVSeason.ResumeLayout(false)
        Me.gbTVSeasonLandscapeOpts.ResumeLayout(false)
        Me.gbTVSeasonLandscapeOpts.PerformLayout
        Me.gbTVSeasonBannerOpts.ResumeLayout(false)
        Me.gbTVSeasonBannerOpts.PerformLayout
        Me.gbTVSeasonPosterOpts.ResumeLayout(false)
        Me.gbTVSeasonPosterOpts.PerformLayout
        Me.gbTVSeasonFanartOpts.ResumeLayout(false)
        Me.gbTVSeasonFanartOpts.PerformLayout
        Me.tpTVEpisode.ResumeLayout(false)
        Me.gbTVEpisodePosterOpts.ResumeLayout(false)
        Me.gbTVEpisodePosterOpts.PerformLayout
        Me.gbTVEpisodeFanartOpts.ResumeLayout(false)
        Me.gbTVEpisodeFanartOpts.PerformLayout
        Me.pnlTVScraper.ResumeLayout(false)
        Me.gbTVScraperMiscOpts.ResumeLayout(false)
        Me.gbTVScraperFieldsOpts.ResumeLayout(false)
        Me.gbTVScraperFieldsShowOpts.ResumeLayout(false)
        Me.gbTVScraperFieldsEpisodeOpts.ResumeLayout(false)
        Me.gbTVScraperGlobalLocksOpts.ResumeLayout(false)
        Me.gbTVScraperGlobalLocksEpisodeOpts.ResumeLayout(false)
        Me.gbTVScraperGlobalLocksShowOpts.ResumeLayout(false)
        Me.gbTVScraperMetaDataOpts.ResumeLayout(false)
        Me.gbTVScraperMetaDataOpts.PerformLayout
        Me.gbTVScraperDefFIExtOpts.ResumeLayout(false)
        Me.gbTVScraperDefFIExtOpts.PerformLayout
        Me.gbTVScraperDurationOpts.ResumeLayout(false)
        Me.gbTVScraperDurationOpts.PerformLayout
        Me.gbTVScraperOptionsOpts.ResumeLayout(false)
        Me.gbTVScraperOptionsOpts.PerformLayout
        Me.gbMovieScraperMetaDataOpts.ResumeLayout(false)
        Me.gbMovieScraperMetaDataOpts.PerformLayout
        Me.gbMovieScraperDefFIExtOpts.ResumeLayout(false)
        Me.gbMovieScraperDefFIExtOpts.PerformLayout
        Me.gbMovieScraperDurationFormatOpts.ResumeLayout(false)
        Me.gbMovieScraperDurationFormatOpts.PerformLayout
        Me.gbMovieScraperGlobalOpts.ResumeLayout(false)
        Me.tblMovieScraperGlobalOpts.ResumeLayout(false)
        Me.tblMovieScraperGlobalOpts.PerformLayout
        Me.gbMovieScraperMiscOpts.ResumeLayout(false)
        Me.gbMovieScraperMiscOpts.PerformLayout
        Me.gbMovieScraperCertificationOpts.ResumeLayout(false)
        Me.pnlMovieScraper.ResumeLayout(false)
        Me.tblMovieScraper.ResumeLayout(false)
        Me.gbSettingsHelp.ResumeLayout(false)
        CType(Me.pbSettingsHelpLogo,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlSettingsHelp.ResumeLayout(false)
        Me.pnlMovieTrailers.ResumeLayout(false)
        Me.gbMovieTrailerOpts.ResumeLayout(false)
        Me.gbMovieTrailerOpts.PerformLayout
        Me.pnlMovieThemes.ResumeLayout(false)
        Me.gbMovieThemeOpts.ResumeLayout(false)
        Me.gbMovieThemeOpts.PerformLayout
        Me.pnlTVThemes.ResumeLayout(false)
        Me.pnlTVThemes.PerformLayout
        Me.pnlMovieSetGeneral.ResumeLayout(false)
        Me.gbMovieSetGeneralMissingItemsOpts.ResumeLayout(false)
        Me.gbMovieSetGeneralMiscOpts.ResumeLayout(false)
        Me.gbMovieSetGeneralMiscOpts.PerformLayout
        Me.gbMovieSetGeneralMediaListOpts.ResumeLayout(false)
        Me.gbMovieSetGeneralMediaListOpts.PerformLayout
        Me.gbMovieSetSortTokensOpts.ResumeLayout(false)
        Me.gbMovieSetSortTokensOpts.PerformLayout
        Me.pnlMovieSetSources.ResumeLayout(false)
        Me.gbMovieSetSourceMiscOpts.ResumeLayout(false)
        Me.gbMovieSetFileNaming.ResumeLayout(false)
        Me.tcMovieSetFileNaming.ResumeLayout(false)
        Me.tpMovieSetFileNamingXBMC.ResumeLayout(false)
        CType(Me.pbMSAAInfo,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbMovieSetMSAA.ResumeLayout(false)
        Me.gbMovieSetMSAA.PerformLayout
        Me.tpMovieSetFileNamingExpert.ResumeLayout(false)
        Me.gbMovieSetExpert.ResumeLayout(false)
        Me.gbMovieSetExpert.PerformLayout
        Me.tcMovieSetFileNamingExpert.ResumeLayout(false)
        Me.tpMovieSetFileNamingExpertSingle.ResumeLayout(false)
        Me.tpMovieSetFileNamingExpertSingle.PerformLayout
        Me.tpMovieSetFileNamingExpertParent.ResumeLayout(false)
        Me.tpMovieSetFileNamingExpertParent.PerformLayout
        Me.pnlMovieSetScraper.ResumeLayout(false)
        Me.pnlMovieSetScraper.PerformLayout
        Me.tblMovieSetScraper.ResumeLayout(false)
        Me.tblMovieSetScraper.PerformLayout
        Me.gbMovieSetScraperTitleRenamerOpts.ResumeLayout(false)
        Me.gbMovieSetScraperTitleRenamerOpts.PerformLayout
        Me.tblMovieSetScraperTitleRenamerOpts.ResumeLayout(false)
        CType(Me.dgvMovieSetScraperTitleRenamer,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbMovieSetScraperGlobalOpts.ResumeLayout(false)
        Me.gbMovieSetScraperGlobalOpts.PerformLayout
        Me.tblMovieSetScraperGlobalOpts.ResumeLayout(false)
        Me.tblMovieSetScraperGlobalOpts.PerformLayout
        Me.pnlMovieSetImages.ResumeLayout(false)
        Me.pnlMovieSetImages.PerformLayout
        Me.tblMovieSetImages.ResumeLayout(false)
        Me.tblMovieSetImages.PerformLayout
        Me.gbMovieSetImagesPosterOpts.ResumeLayout(false)
        Me.gbMovieSetImagesPosterOpts.PerformLayout
        Me.tblMovieSetImagesPosterOpts.ResumeLayout(false)
        Me.tblMovieSetImagesPosterOpts.PerformLayout
        Me.gbMovieSetImagesClearLogoOpts.ResumeLayout(false)
        Me.gbMovieSetImagesClearLogoOpts.PerformLayout
        Me.tblMovieSetImagesClearLogoOpts.ResumeLayout(false)
        Me.tblMovieSetImagesClearLogoOpts.PerformLayout
        Me.gbMovieSetImagesClearArtOpts.ResumeLayout(false)
        Me.gbMovieSetImagesClearArtOpts.PerformLayout
        Me.tblMovieSetImagesClearArtOpts.ResumeLayout(false)
        Me.tblMovieSetImagesClearArtOpts.PerformLayout
        Me.gbMovieSetImagesBannerOpts.ResumeLayout(false)
        Me.gbMovieSetImagesBannerOpts.PerformLayout
        Me.tblMovieSetImagesBannerOpts.ResumeLayout(false)
        Me.tblMovieSetImagesBannerOpts.PerformLayout
        Me.gbMovieSetImagesFanartOpts.ResumeLayout(false)
        Me.gbMovieSetImagesFanartOpts.PerformLayout
        Me.tblMovieSetImagesFanartOpts.ResumeLayout(false)
        Me.tblMovieSetImagesFanartOpts.PerformLayout
        Me.gbMovieSetImagesLandscapeOpts.ResumeLayout(false)
        Me.gbMovieSetImagesLandscapeOpts.PerformLayout
        Me.tblMovieSetImagesLandscapeOpts.ResumeLayout(false)
        Me.tblMovieSetImagesLandscapeOpts.PerformLayout
        Me.scSettings.Panel1.ResumeLayout(false)
        Me.scSettings.Panel1.PerformLayout
        Me.scSettings.Panel2.ResumeLayout(false)
        CType(Me.scSettings,System.ComponentModel.ISupportInitialize).EndInit
        Me.scSettings.ResumeLayout(false)
        Me.tblSettingsCurrent.ResumeLayout(false)
        Me.scSettingsBody.Panel1.ResumeLayout(false)
        Me.scSettingsBody.Panel2.ResumeLayout(false)
        CType(Me.scSettingsBody,System.ComponentModel.ISupportInitialize).EndInit
        Me.scSettingsBody.ResumeLayout(false)
        Me.scSettingsMain.Panel1.ResumeLayout(false)
        Me.scSettingsMain.Panel2.ResumeLayout(false)
        CType(Me.scSettingsMain,System.ComponentModel.ISupportInitialize).EndInit
        Me.scSettingsMain.ResumeLayout(false)
        Me.tblSettingsFooter.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents lblSettingsTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblSettingsTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbSettingsTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents gbFileSystemCleanFiles As System.Windows.Forms.GroupBox
    Friend WithEvents chkCleanFolderJPG As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanFanartJPG As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanMovieTBNb As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanMovieTBN As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanMovieNFOb As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanMovieNFO As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanMovieFanartJPG As System.Windows.Forms.CheckBox
    Friend WithEvents gbGeneralMisc As System.Windows.Forms.GroupBox
    Friend WithEvents lblGeneralOverwriteNfo As System.Windows.Forms.Label
    Friend WithEvents chkGeneralOverwriteNfo As System.Windows.Forms.CheckBox
    Friend WithEvents lvMovieSources As System.Windows.Forms.ListView
    Friend WithEvents colPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRecur As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkMovieFanartOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoviePosterOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents cbMovieFanartPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblMovieFanartSize As System.Windows.Forms.Label
    Friend WithEvents lblMoviePosterSize As System.Windows.Forms.Label
    Friend WithEvents cbMoviePosterPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents gbMovieGeneralMediaListOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieTrailerCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieNFOCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieFanartCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoviePosterCol As System.Windows.Forms.CheckBox
    Friend WithEvents btnMovieSourceRemove As System.Windows.Forms.Button
    Friend WithEvents btnMovieSourceAdd As System.Windows.Forms.Button
    Friend WithEvents gbMovieImagesOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkCleanMovieNameJPG As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanMovieJPG As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanPosterJPG As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanPosterTBN As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanDotFanartJPG As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieGeneralMiscOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieGeneralMarkNew As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieImagesFanartOpts As System.Windows.Forms.GroupBox
    Friend WithEvents lblMovieFanartWidth As System.Windows.Forms.Label
    Friend WithEvents lblMovieFanartHeight As System.Windows.Forms.Label
    Friend WithEvents chkMovieFanartResize As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieFanartWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieFanartHeight As System.Windows.Forms.TextBox
    Friend WithEvents gbMovieImagesPosterOpts As System.Windows.Forms.GroupBox
    Friend WithEvents lblMoviePosterWidth As System.Windows.Forms.Label
    Friend WithEvents lblMoviePosterHeight As System.Windows.Forms.Label
    Friend WithEvents chkMoviePosterResize As System.Windows.Forms.CheckBox
    Friend WithEvents txtMoviePosterWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtMoviePosterHeight As System.Windows.Forms.TextBox
    Friend WithEvents ilSettings As System.Windows.Forms.ImageList
    Friend WithEvents tvSettingsList As System.Windows.Forms.TreeView
    Friend WithEvents pnlGeneral As System.Windows.Forms.Panel
    Friend WithEvents pnlProxy As System.Windows.Forms.Panel
    Friend WithEvents pnlMovieGeneral As System.Windows.Forms.Panel
    Friend WithEvents lblSettingsCurrent As System.Windows.Forms.Label
    Friend WithEvents pnlSettingsCurrent As System.Windows.Forms.Panel
    Friend WithEvents chkCleanExtrathumbs As System.Windows.Forms.CheckBox
    Friend WithEvents pnlFileSystem As System.Windows.Forms.Panel
    Friend WithEvents gbFileSystemValidExts As System.Windows.Forms.GroupBox
    Friend WithEvents btnFileSystemValidExtsRemove As System.Windows.Forms.Button
    Friend WithEvents btnFileSystemValidExtsAdd As System.Windows.Forms.Button
    Friend WithEvents txtFileSystemValidExts As System.Windows.Forms.TextBox
    Friend WithEvents lstFileSystemValidExts As System.Windows.Forms.ListBox
    Friend WithEvents chkGeneralCheckUpdates As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieBackdropsFolder As System.Windows.Forms.GroupBox
    Friend WithEvents txtMovieBackdropsPath As System.Windows.Forms.TextBox
    Friend WithEvents btnMovieBackdropsPathBrowse As System.Windows.Forms.Button
    Friend WithEvents chkMovieBackdropsAuto As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieEThumbsCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSubCol As System.Windows.Forms.CheckBox
    Friend WithEvents pnlMovieSources As System.Windows.Forms.Panel
    Friend WithEvents clbMovieGenre As System.Windows.Forms.CheckedListBox
    Friend WithEvents pnlMovieImages As System.Windows.Forms.Panel
    Friend WithEvents gbMovieSourcesMiscOpts As System.Windows.Forms.GroupBox
    Friend WithEvents lblMovieSkipLessThanMB As System.Windows.Forms.Label
    Friend WithEvents txtMovieSkipLessThan As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieSkipLessThan As System.Windows.Forms.Label
    Friend WithEvents chkMovieSkipStackedSizeCheck As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieFanartPrefOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieNoSaveImagesToNfo As System.Windows.Forms.CheckBox
    Friend WithEvents tcFileSystemCleaner As System.Windows.Forms.TabControl
    Friend WithEvents tpFileSystemCleanerStandard As System.Windows.Forms.TabPage
    Friend WithEvents tpFileSystemCleanerExpert As System.Windows.Forms.TabPage
    Friend WithEvents lblFileSystemCleanerWarning As System.Windows.Forms.Label
    Friend WithEvents btnFileSystemCleanerWhitelistRemove As System.Windows.Forms.Button
    Friend WithEvents btnFileSystemCleanerWhitelistAdd As System.Windows.Forms.Button
    Friend WithEvents txtFileSystemCleanerWhitelist As System.Windows.Forms.TextBox
    Friend WithEvents lstFileSystemCleanerWhitelist As System.Windows.Forms.ListBox
    Friend WithEvents lblFileSystemCleanerWhitelist As System.Windows.Forms.Label
    Friend WithEvents chkFileSystemCleanerWhitelist As System.Windows.Forms.CheckBox
    Friend WithEvents chkGeneralInfoPanelAnim As System.Windows.Forms.CheckBox
    Friend WithEvents chkGeneralShowImgDims As System.Windows.Forms.CheckBox
    Friend WithEvents chkGeneralHideFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkGeneralHidePoster As System.Windows.Forms.CheckBox
    Friend WithEvents gbFileSystemNoStackExts As System.Windows.Forms.GroupBox
    Friend WithEvents btnFileSystemNoStackExtsRemove As System.Windows.Forms.Button
    Friend WithEvents btnFileSystemNoStackExtsAdd As System.Windows.Forms.Button
    Friend WithEvents txtFileSystemNoStackExts As System.Windows.Forms.TextBox
    Friend WithEvents lstFileSystemNoStackExts As System.Windows.Forms.ListBox
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colFolder As System.Windows.Forms.ColumnHeader
    Friend WithEvents colSingle As System.Windows.Forms.ColumnHeader
    Friend WithEvents colID As System.Windows.Forms.ColumnHeader
    Friend WithEvents fbdBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnMovieSourceEdit As System.Windows.Forms.Button
    Friend WithEvents chkGeneralShowGenresText As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieDisplayYear As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieSortTokensOpts As System.Windows.Forms.GroupBox
    Friend WithEvents btnMovieSortTokenRemove As System.Windows.Forms.Button
    Friend WithEvents btnMovieSortTokenAdd As System.Windows.Forms.Button
    Friend WithEvents txtMovieSortToken As System.Windows.Forms.TextBox
    Friend WithEvents lstMovieSortTokens As System.Windows.Forms.ListBox
    Friend WithEvents txtMovieLevTolerance As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieLevTolerance As System.Windows.Forms.Label
    Friend WithEvents chkMovieLevTolerance As System.Windows.Forms.CheckBox
    Friend WithEvents lblGeneralntLang As System.Windows.Forms.Label
    Friend WithEvents cbGeneralLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents gbMovieGeneralMissingItemsOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieMissingEThumbs As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingSubs As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingNFO As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingPoster As System.Windows.Forms.CheckBox
    Friend WithEvents lblGeneralMovieTheme As System.Windows.Forms.Label
    Friend WithEvents cbGeneralMovieTheme As System.Windows.Forms.ComboBox
    Friend WithEvents gbGeneralThemes As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTips As System.Windows.Forms.ToolTip
    Friend WithEvents pnlTVSources As System.Windows.Forms.Panel
    Friend WithEvents btnTVSourceEdit As System.Windows.Forms.Button
    Friend WithEvents lvTVSources As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnRemTVSource As System.Windows.Forms.Button
    Friend WithEvents btnTVSourceAdd As System.Windows.Forms.Button
    Friend WithEvents chkMovieCleanDB As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieGeneralIgnoreLastScan As System.Windows.Forms.CheckBox
    Friend WithEvents pnlTVGeneral As System.Windows.Forms.Panel
    Friend WithEvents gbTVEpisodeFilterOpts As System.Windows.Forms.GroupBox
    Friend WithEvents btnTVEpisodeFilterDown As System.Windows.Forms.Button
    Friend WithEvents btnTVEpisodeFilterUp As System.Windows.Forms.Button
    Friend WithEvents chkTVEpisodeProperCase As System.Windows.Forms.CheckBox
    Friend WithEvents btnTVEpisodeFilterRemove As System.Windows.Forms.Button
    Friend WithEvents btnTVEpisodeFilterAdd As System.Windows.Forms.Button
    Friend WithEvents txtTVEpisodeFilter As System.Windows.Forms.TextBox
    Friend WithEvents lstTVEpisodeFilter As System.Windows.Forms.ListBox
    Friend WithEvents gbTVShowFilterOpts As System.Windows.Forms.GroupBox
    Friend WithEvents btnTVShowFilterDown As System.Windows.Forms.Button
    Friend WithEvents btnTVShowFilterUp As System.Windows.Forms.Button
    Friend WithEvents chkTVShowProperCase As System.Windows.Forms.CheckBox
    Friend WithEvents btnTVShowFilterRemove As System.Windows.Forms.Button
    Friend WithEvents btnTVShowFilterAdd As System.Windows.Forms.Button
    Friend WithEvents txtTVShowFilter As System.Windows.Forms.TextBox
    Friend WithEvents lstTVShowFilter As System.Windows.Forms.ListBox
    Friend WithEvents gbMovieGeneralFiltersOpts As System.Windows.Forms.GroupBox
    Friend WithEvents btnMovieFilterDown As System.Windows.Forms.Button
    Friend WithEvents btnMovieFilterUp As System.Windows.Forms.Button
    Friend WithEvents chkMovieProperCase As System.Windows.Forms.CheckBox
    Friend WithEvents btnMovieFilterRemove As System.Windows.Forms.Button
    Friend WithEvents btnMovieFilterAdd As System.Windows.Forms.Button
    Friend WithEvents txtMovieFilter As System.Windows.Forms.TextBox
    Friend WithEvents lstMovieFilters As System.Windows.Forms.ListBox
    Friend WithEvents gbMovieGeneralGenreFilterOpts As System.Windows.Forms.GroupBox
    Friend WithEvents gbTVSourcesMiscOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVGeneralIgnoreLastScan As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVCleanDB As System.Windows.Forms.CheckBox
    Friend WithEvents gbGeneralInterface As System.Windows.Forms.GroupBox
    Friend WithEvents cbGeneralTVEpisodeTheme As System.Windows.Forms.ComboBox
    Friend WithEvents lblGeneralTVEpisodeTheme As System.Windows.Forms.Label
    Friend WithEvents cbGeneralTVShowTheme As System.Windows.Forms.ComboBox
    Friend WithEvents lblGeneralTVShowTheme As System.Windows.Forms.Label
    Friend WithEvents tcTVSources As System.Windows.Forms.TabControl
    Friend WithEvents tpTVSourcesGeneral As System.Windows.Forms.TabPage
    Friend WithEvents tpTVSourcesRegex As System.Windows.Forms.TabPage
    Friend WithEvents lvTVShowRegex As System.Windows.Forms.ListView
    Friend WithEvents colTVShowRegexSeason As System.Windows.Forms.ColumnHeader
    Friend WithEvents colTVShowRegexSeasonApply As System.Windows.Forms.ColumnHeader
    Friend WithEvents colTVShowRegexEpisode As System.Windows.Forms.ColumnHeader
    Friend WithEvents colTVShowRegexEpisodeApply As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnTVShowRegexAdd As System.Windows.Forms.Button
    Friend WithEvents btnTVShowRegexEdit As System.Windows.Forms.Button
    Friend WithEvents btnTVShowRegexRemove As System.Windows.Forms.Button
    Friend WithEvents txtTVSeasonRegex As System.Windows.Forms.TextBox
    Friend WithEvents cbTVEpisodeRetrieve As System.Windows.Forms.ComboBox
    Friend WithEvents txtTVEpisodeRegex As System.Windows.Forms.TextBox
    Friend WithEvents cbTVSeasonRetrieve As System.Windows.Forms.ComboBox
    Friend WithEvents lblTVSeasonMatch As System.Windows.Forms.Label
    Friend WithEvents gbTVShowRegex As System.Windows.Forms.GroupBox
    Friend WithEvents lblTVEpisodeRetrieve As System.Windows.Forms.Label
    Friend WithEvents lblTVSeasonRetrieve As System.Windows.Forms.Label
    Friend WithEvents lblTVEpisodeMatch As System.Windows.Forms.Label
    Friend WithEvents colTVShowRegexID As System.Windows.Forms.ColumnHeader
    Friend WithEvents pnlTVImages As System.Windows.Forms.Panel
    Friend WithEvents gbTVShowFanartOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtTVShowFanartWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtTVShowFanartHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblTVShowFanartWidth As System.Windows.Forms.Label
    Friend WithEvents lblTVShowFanartHeight As System.Windows.Forms.Label
    Friend WithEvents chkTVShowFanartResize As System.Windows.Forms.CheckBox
    Friend WithEvents cbTVShowFanartPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblTVShowFanartSize As System.Windows.Forms.Label
    Friend WithEvents chkTVShowFanartOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVShowPosterOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtTVShowPosterWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtTVShowPosterHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblTVShowPosterWidth As System.Windows.Forms.Label
    Friend WithEvents lblTVShowPosterHeight As System.Windows.Forms.Label
    Friend WithEvents chkTVShowPosterResize As System.Windows.Forms.CheckBox
    Friend WithEvents lblTVShowPosterSize As System.Windows.Forms.Label
    Friend WithEvents cbTVShowPosterPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents chkTVShowPosterOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents tcTVImages As System.Windows.Forms.TabControl
    Friend WithEvents tpTVShow As System.Windows.Forms.TabPage
    Friend WithEvents tpTVEpisode As System.Windows.Forms.TabPage
    Friend WithEvents pnlTVScraper As System.Windows.Forms.Panel
    Friend WithEvents gbTVGeneralMiscOpts As System.Windows.Forms.GroupBox
    Friend WithEvents gbTVGeneralMediaListOpts As System.Windows.Forms.GroupBox
    Friend WithEvents gbTVGeneralListEpisodeOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVEpisodeNfoCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVEpisodeFanartCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVEpisodePosterCol As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVGeneralListSeasonOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVSeasonFanartCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVSeasonPosterCol As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVGeneralListShowOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVShowNfoCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowFanartCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowPosterCol As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVEpisodePosterOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtTVEpisodePosterWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtTVEpisodePosterHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblTVEpisodePosterWidth As System.Windows.Forms.Label
    Friend WithEvents lblTVEpisodePosterHeight As System.Windows.Forms.Label
    Friend WithEvents chkTVEpisodePosterResize As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVEpisodePosterOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVEpisodeFanartOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtTVEpisodeFanartWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtTVEpisodeFanartHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblTVEpisodeFanartWidth As System.Windows.Forms.Label
    Friend WithEvents lblTVEpisodeFanartHeight As System.Windows.Forms.Label
    Friend WithEvents chkTVEpisodeFanartResize As System.Windows.Forms.CheckBox
    Friend WithEvents cbTVEpisodeFanartPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblTVEpisodeFanartSize As System.Windows.Forms.Label
    Friend WithEvents chkTVEpisodeFanartOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbProxyOpts As System.Windows.Forms.GroupBox
    Friend WithEvents gbProxyCredsOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkProxyCredsEnable As System.Windows.Forms.CheckBox
    Friend WithEvents lblProxyPort As System.Windows.Forms.Label
    Friend WithEvents lblProxyURI As System.Windows.Forms.Label
    Friend WithEvents txtProxyPort As System.Windows.Forms.TextBox
    Friend WithEvents txtProxyURI As System.Windows.Forms.TextBox
    Friend WithEvents chkProxyEnable As System.Windows.Forms.CheckBox
    Friend WithEvents txtProxyDomain As System.Windows.Forms.TextBox
    Friend WithEvents lblProxyDomain As System.Windows.Forms.Label
    Friend WithEvents txtProxyPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtProxyUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblProxyUsername As System.Windows.Forms.Label
    Friend WithEvents lblProxyPassword As System.Windows.Forms.Label
    Friend WithEvents chkGeneralSourceFromFolder As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSortBeforeScan As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVScraperOptionsOpts As System.Windows.Forms.GroupBox
    Friend WithEvents gbMovieScraperMetaDataOpts As System.Windows.Forms.GroupBox
    Friend WithEvents gbMovieScraperDefFIExtOpts As System.Windows.Forms.GroupBox
    Friend WithEvents lstMovieScraperDefFIExt As System.Windows.Forms.ListBox
    Friend WithEvents txtMovieScraperDefFIExt As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieScraperDefFIExt As System.Windows.Forms.Label
    Friend WithEvents btnMovieScraperDefFIExtRemove As System.Windows.Forms.Button
    Friend WithEvents btnMovieScraperDefFIExtEdit As System.Windows.Forms.Button
    Friend WithEvents btnMovieScraperDefFIExtAdd As System.Windows.Forms.Button
    Friend WithEvents chkMovieScraperMetaDataIFOScan As System.Windows.Forms.CheckBox
    Friend WithEvents cbMovieLanguageOverlay As System.Windows.Forms.ComboBox
    Friend WithEvents lblMovieLanguageOverlay As System.Windows.Forms.Label
    Friend WithEvents gbMovieScraperDurationFormatOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieScraperMetaDataScan As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperUseMDDuration As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieScraperGlobalOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieLockCollectionID As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieScraperMiscOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieScraperOutlineForPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperCastWithImg As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperCertForMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents cbMovieScraperCertLang As System.Windows.Forms.ComboBox
    Friend WithEvents pnlMovieScraper As System.Windows.Forms.Panel
    Friend WithEvents tpTVSeason As System.Windows.Forms.TabPage
    Friend WithEvents gbTVSeasonPosterOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtTVSeasonPosterWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtTVSeasonPosterHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblTVSeasonPosterWidth As System.Windows.Forms.Label
    Friend WithEvents lblTVSeasonPosterHeight As System.Windows.Forms.Label
    Friend WithEvents chkTVSeasonPosterResize As System.Windows.Forms.CheckBox
    Friend WithEvents lblTVSeasonPosterSize As System.Windows.Forms.Label
    Friend WithEvents cbTVSeasonPosterPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents chkTVSeasonPosterOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVSeasonFanartOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtTVSeasonFanartWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtTVSeasonFanartHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblTVSeasonFanartWidth As System.Windows.Forms.Label
    Friend WithEvents lblTVSeasonFanartHeight As System.Windows.Forms.Label
    Friend WithEvents chkTVSeasonFanartResize As System.Windows.Forms.CheckBox
    Friend WithEvents cbTVSeasonFanartPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblTVSeasonFanartSize As System.Windows.Forms.Label
    Friend WithEvents chkTVSeasonFanartOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScanOrderModify As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScanOrderModify As System.Windows.Forms.CheckBox
    Friend WithEvents lblTVScraperUpdateTime As System.Windows.Forms.Label
    Friend WithEvents cbTVScraperUpdateTime As System.Windows.Forms.ComboBox
    Friend WithEvents chkTVEpisodeNoFilter As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVDisplayMissingEpisodes As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVScraperMetaDataOpts As System.Windows.Forms.GroupBox
    Friend WithEvents gbTVScraperDefFIExtOpts As System.Windows.Forms.GroupBox
    Friend WithEvents lstTVScraperDefFIExt As System.Windows.Forms.ListBox
    Friend WithEvents txtTVScraperDefFIExt As System.Windows.Forms.TextBox
    Friend WithEvents lblTVScraperDefFIExt As System.Windows.Forms.Label
    Friend WithEvents btnTVScraperDefFIExtRemove As System.Windows.Forms.Button
    Friend WithEvents btnTVScraperDefFIExtEdit As System.Windows.Forms.Button
    Friend WithEvents btnTVScraperDefFIExtAdd As System.Windows.Forms.Button
    Friend WithEvents cbTVLanguageOverlay As System.Windows.Forms.ComboBox
    Friend WithEvents lblTVLanguageOverlay As System.Windows.Forms.Label
    Friend WithEvents chkTVScraperMetaDataScan As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVScraperFieldsOpts As System.Windows.Forms.GroupBox
    Friend WithEvents gbTVScraperGlobalLocksOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVLockEpisodePlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVLockShowStudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVLockEpisodeRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVLockEpisodeTitle As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVScraperGlobalLocksEpisodeOpts As System.Windows.Forms.GroupBox
    Friend WithEvents gbTVScraperGlobalLocksShowOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVLockShowPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVLockShowGenre As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVLockShowRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVLockShowTitle As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVScraperFieldsShowOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVScraperShowEpiGuideURL As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperShowMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperShowPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperShowGenre As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperShowTitle As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVScraperFieldsEpisodeOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVScraperEpisodeTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperEpisodeEpisode As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperEpisodeSeason As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperShowRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperShowActors As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperShowStudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperShowPremiered As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperEpisodeActors As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperEpisodeCredits As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperEpisodeDirector As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperEpisodePlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperEpisodeRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperEpisodeAired As System.Windows.Forms.CheckBox
    Friend WithEvents lblMovieScraperDurationRuntimeFormat As System.Windows.Forms.Label
    Friend WithEvents txtMovieScraperDurationRuntimeFormat As System.Windows.Forms.TextBox
    Friend WithEvents tsSettingsTopMenu As System.Windows.Forms.ToolStrip
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents pbSettingsCurrent As System.Windows.Forms.PictureBox
    Friend WithEvents gbSettingsHelp As System.Windows.Forms.GroupBox
    Friend WithEvents lblHelp As System.Windows.Forms.Label
    Friend WithEvents pnlSettingsHelp As System.Windows.Forms.Panel
    Friend WithEvents pbSettingsHelpLogo As System.Windows.Forms.PictureBox
    Friend WithEvents chkTVGeneralMarkNewShows As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVGeneralMarkNewEpisodes As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperCertOnlyValue As System.Windows.Forms.CheckBox
    Friend WithEvents cbTVScraperOptionsOrdering As System.Windows.Forms.ComboBox
    Friend WithEvents lblTVScraperOptionsOrdering As System.Windows.Forms.Label
    Friend WithEvents btnTVShowFilterReset As System.Windows.Forms.Button
    Friend WithEvents btnTVEpisodeFilterReset As System.Windows.Forms.Button
    Friend WithEvents btnMovieFilterReset As System.Windows.Forms.Button
    Friend WithEvents btnFileSystemValidExtsReset As System.Windows.Forms.Button
    Friend WithEvents btnTVShowRegexReset As System.Windows.Forms.Button
    Friend WithEvents btnTVShowRegexDown As System.Windows.Forms.Button
    Friend WithEvents btnTVShowRegexUp As System.Windows.Forms.Button
    Friend WithEvents lblTVSkipLessThanMB As System.Windows.Forms.Label
    Friend WithEvents txtTVSkipLessThan As System.Windows.Forms.TextBox
    Friend WithEvents lblTVSkipLessThan As System.Windows.Forms.Label
    Friend WithEvents btnTVShowRegexClear As System.Windows.Forms.Button
    Friend WithEvents btnTVShowRegexGet As System.Windows.Forms.Button
    Friend WithEvents chkMovieClickScrapeAsk As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieClickScrape As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoviePosterPrefOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkGeneralDateAddedIgnoreNFO As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockLanguageV As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockLanguageA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperCertFSK As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieGenrealIMDBMirrorOpts As System.Windows.Forms.GroupBox
    Friend WithEvents lblMovieIMDBMirror As System.Windows.Forms.Label
    Friend WithEvents txtMovieIMDBURL As System.Windows.Forms.TextBox
    Friend WithEvents pnlMovieTrailers As System.Windows.Forms.Panel
    Friend WithEvents gbMovieTrailerOpts As System.Windows.Forms.GroupBox
    Friend WithEvents cbMovieTrailerPrefQual As System.Windows.Forms.ComboBox
    Friend WithEvents lblMovieTrailerPrefQual As System.Windows.Forms.Label
    Friend WithEvents chkMovieTrailerDeleteExisting As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieTrailerOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieTrailerEnable As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieScraperOutlineLimit As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieScraperPlotForOutline As System.Windows.Forms.CheckBox
    Friend WithEvents lblMovieScraperOutlineLimit As System.Windows.Forms.Label
    Friend WithEvents chkGeneralHideFanartSmall As System.Windows.Forms.CheckBox
    Friend WithEvents chkGeneralImagesGlassOverlay As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieFileNaming As System.Windows.Forms.GroupBox
    Friend WithEvents tcMovieFileNaming As System.Windows.Forms.TabControl
    Friend WithEvents tpMovieFileNamingXBMC As System.Windows.Forms.TabPage
    Friend WithEvents tpMovieFileNamingExpert As System.Windows.Forms.TabPage
    Friend WithEvents gbMovieEden As System.Windows.Forms.GroupBox
    Friend WithEvents gbMovieFrodo As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieActorThumbsFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieTrailerFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieFanartFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoviePosterFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieNFOFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieClearLogoFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieDiscArtFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieClearArtFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieBannerFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLandscapeFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieUseFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieExtrafanartsFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieExtrathumbsFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieExtrafanartsEden As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieExtrathumbsEden As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieUseEden As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieActorThumbsEden As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieTrailerEden As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieFanartEden As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoviePosterEden As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieNFOEden As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVScraperDurationOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVScraperUseMDDuration As System.Windows.Forms.CheckBox
    Friend WithEvents txtTVScraperDurationRuntimeFormat As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieMissingEFanarts As System.Windows.Forms.CheckBox
    Friend WithEvents tpMovieFileNamingNMT As System.Windows.Forms.TabPage
    Friend WithEvents gbMovieNMJ As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieUseNMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieBannerNMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieTrailerNMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieFanartNMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoviePosterNMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieNFONMJ As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieYAMJ As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieUseYAMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieBannerYAMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieTrailerYAMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieFanartYAMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoviePosterYAMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieNFOYAMJ As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieImagesEFanartsOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieEFanartsPrefOnly As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieEFanartsWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieEFanartsHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieEFanartsWidth As System.Windows.Forms.Label
    Friend WithEvents lblMovieEFanartsHeight As System.Windows.Forms.Label
    Friend WithEvents chkMovieEFanartsResize As System.Windows.Forms.CheckBox
    Friend WithEvents lblMovieEFanartsSize As System.Windows.Forms.Label
    Friend WithEvents cbMovieEFanartsPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents chkMovieEFanartsOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieImagesEThumbsOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieEThumbsPrefOnly As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieEThumbsWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieEThumbsHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieEThumbsWidth As System.Windows.Forms.Label
    Friend WithEvents lblMovieEThumbsHeight As System.Windows.Forms.Label
    Friend WithEvents chkMovieEThumbsResize As System.Windows.Forms.CheckBox
    Friend WithEvents lblMovieEThumbsSize As System.Windows.Forms.Label
    Friend WithEvents cbMovieEThumbsPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents chkMovieEThumbsOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieXBMCOptionalSettings As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieXBMCTrailerFormat As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieWatchedCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieEFanartsCol As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieSetMSAAPath As System.Windows.Forms.GroupBox
    Friend WithEvents btnMovieSetPathMSAABrowse As System.Windows.Forms.Button
    Friend WithEvents txtMovieSetPathMSAA As System.Windows.Forms.TextBox
    Friend WithEvents gbGeneralDaemon As System.Windows.Forms.GroupBox
    Friend WithEvents btnGeneralDaemonPathBrowse As System.Windows.Forms.Button
    Friend WithEvents txtGeneralDaemonPath As System.Windows.Forms.TextBox
    Friend WithEvents lblGeneralDaemonPath As System.Windows.Forms.Label
    Friend WithEvents lblGeneralDaemonDrive As System.Windows.Forms.Label
    Friend WithEvents cbGeneralDaemonDrive As System.Windows.Forms.ComboBox
    Friend WithEvents fileBrowse As System.Windows.Forms.OpenFileDialog
    Friend WithEvents chkMovieUseExpert As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieExpert As System.Windows.Forms.GroupBox
    Friend WithEvents tcMovieFileNamingExpert As System.Windows.Forms.TabControl
    Friend WithEvents tpMovieFileNamingExpertSingle As System.Windows.Forms.TabPage
    Friend WithEvents txtMoviePosterExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieFanartExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieTrailerExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieBannerExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieClearLogoExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieClearArtExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieLandscapeExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieDiscArtExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieLandscapeExpertSingle As System.Windows.Forms.Label
    Friend WithEvents lblMovieDiscArtExpertSingle As System.Windows.Forms.Label
    Friend WithEvents lblMovieBannerExpertSingle As System.Windows.Forms.Label
    Friend WithEvents lblMovieTrailerExpertSingle As System.Windows.Forms.Label
    Friend WithEvents lblMovieClearLogoExpertSingle As System.Windows.Forms.Label
    Friend WithEvents lblMovieFanartExpertSingle As System.Windows.Forms.Label
    Friend WithEvents lblMoviePosterExpertSingle As System.Windows.Forms.Label
    Friend WithEvents txtMovieNFOExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieNFOExpertSingle As System.Windows.Forms.Label
    Friend WithEvents tpMovieFileNamingExpertMulti As System.Windows.Forms.TabPage
    Friend WithEvents tpMovieFileNamingExpertVTS As System.Windows.Forms.TabPage
    Friend WithEvents tpMovieFileNamingExpertBDMV As System.Windows.Forms.TabPage
    Friend WithEvents lblMovieClearArtExpertSingle As System.Windows.Forms.Label
    Friend WithEvents gbMovieExpertSingleOptionalImages As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieActorThumbsExpertSingle As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieExtrafanartsExpertSingle As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieExtrathumbsExpertSingle As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieExpertSingleOptionalSettings As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieXBMCTrailerFormatExpertSingle As System.Windows.Forms.CheckBox
    Friend WithEvents txtMoviePosterExpertMulti As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieFanartExpertMulti As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieClearArtExpertMulti As System.Windows.Forms.Label
    Friend WithEvents txtMovieTrailerExpertMulti As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieBannerExpertMulti As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieClearLogoExpertMulti As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieClearArtExpertMulti As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieLandscapeExpertMulti As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieDiscArtExpertMulti As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieLandscapeExpertMulti As System.Windows.Forms.Label
    Friend WithEvents lblMovieDiscArtExpertMulti As System.Windows.Forms.Label
    Friend WithEvents lblMovieBannerExpertMulti As System.Windows.Forms.Label
    Friend WithEvents lblMovieTrailerExpertMulti As System.Windows.Forms.Label
    Friend WithEvents lblMovieClearLogoExpertMulti As System.Windows.Forms.Label
    Friend WithEvents lblMovieFanartExpertMulti As System.Windows.Forms.Label
    Friend WithEvents lblMoviePosterExpertMulti As System.Windows.Forms.Label
    Friend WithEvents txtMovieNFOExpertMulti As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieNFOExpertMulti As System.Windows.Forms.Label
    Friend WithEvents chkMovieStackExpertSingle As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieUnstackExpertSingle As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieActorThumbsExtExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents gbMovieExpertMultiOptionalImages As System.Windows.Forms.GroupBox
    Friend WithEvents txtMovieActorThumbsExtExpertMulti As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieActorThumbsExpertMulti As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieExpertMultiOptionalSettings As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieUnstackExpertMulti As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieStackExpertMulti As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieXBMCTrailerFormatExpertMulti As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieExpertVTSOptionalSettings As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieXBMCTrailerFormatExpertVTS As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieExpertVTSOptionalImages As System.Windows.Forms.GroupBox
    Friend WithEvents txtMovieActorThumbsExtExpertVTS As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieActorThumbsExpertVTS As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieExtrafanartsExpertVTS As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieExtrathumbsExpertVTS As System.Windows.Forms.CheckBox
    Friend WithEvents lblMovieClearArtExpertVTS As System.Windows.Forms.Label
    Friend WithEvents txtMoviePosterExpertVTS As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieFanartExpertVTS As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieTrailerExpertVTS As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieBannerExpertVTS As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieClearLogoExpertVTS As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieClearArtExpertVTS As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieLandscapeExpertVTS As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieDiscArtExpertVTS As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieLandscapeExpertVTS As System.Windows.Forms.Label
    Friend WithEvents lblMovieDiscArtExpertVTS As System.Windows.Forms.Label
    Friend WithEvents lblMovieBannerExpertVTS As System.Windows.Forms.Label
    Friend WithEvents lblMovieTrailerExpertVTS As System.Windows.Forms.Label
    Friend WithEvents lblMovieClearLogoExpertVTS As System.Windows.Forms.Label
    Friend WithEvents lblMovieFanartExpertVTS As System.Windows.Forms.Label
    Friend WithEvents lblMoviePosterExpertVTS As System.Windows.Forms.Label
    Friend WithEvents txtMovieNFOExpertVTS As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieNFOExpertVTS As System.Windows.Forms.Label
    Friend WithEvents gbMovieExpertBDMVOptionalSettings As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieXBMCTrailerFormatExpertBDMV As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieExpertBDMVOptionalImages As System.Windows.Forms.GroupBox
    Friend WithEvents txtMovieActorThumbsExtExpertBDMV As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieActorThumbsExpertBDMV As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieExtrafanartsExpertBDMV As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieExtrathumbsExpertBDMV As System.Windows.Forms.CheckBox
    Friend WithEvents lblMovieClearArtExpertBDMV As System.Windows.Forms.Label
    Friend WithEvents txtMoviePosterExpertBDMV As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieFanartExpertBDMV As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieTrailerExpertBDMV As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieBannerExpertBDMV As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieClearLogoExpertBDMV As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieClearArtExpertBDMV As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieLandscapeExpertBDMV As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieDiscArtExpertBDMV As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieLandscapeExpertBDMV As System.Windows.Forms.Label
    Friend WithEvents lblMovieDiscArtExpertBDMV As System.Windows.Forms.Label
    Friend WithEvents lblMovieBannerExpertBDMV As System.Windows.Forms.Label
    Friend WithEvents lblMovieTrailerExpertBDMV As System.Windows.Forms.Label
    Friend WithEvents lblMovieClearLogoExpertBDMV As System.Windows.Forms.Label
    Friend WithEvents lblMovieFanartExpertBDMV As System.Windows.Forms.Label
    Friend WithEvents lblMoviePosterExpertBDMV As System.Windows.Forms.Label
    Friend WithEvents txtMovieNFOExpertBDMV As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieNFOExpertBDMV As System.Windows.Forms.Label
    Friend WithEvents chkMovieUseBaseDirectoryExpertVTS As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieUseBaseDirectoryExpertBDMV As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieRecognizeVTSExpertVTS As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieXBMCProtectVTSBDMV As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieNMTOptionalSettings As System.Windows.Forms.GroupBox
    Friend WithEvents btnMovieYAMJWatchedFilesBrowse As System.Windows.Forms.Button
    Friend WithEvents txtMovieYAMJWatchedFolder As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieYAMJWatchedFile As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperOutlinePlotEnglishOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVFileNaming As System.Windows.Forms.GroupBox
    Friend WithEvents tcTVFileNaming As System.Windows.Forms.TabControl
    Friend WithEvents tpTVFileNamingXBMC As System.Windows.Forms.TabPage
    Friend WithEvents tpTVFileNamingNMT As System.Windows.Forms.TabPage
    Friend WithEvents tpTVFileNamingExpert As System.Windows.Forms.TabPage
    Friend WithEvents gbTVFrodo As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVSeasonPosterFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowBannerFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVUseFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVEpisodeActorThumbsFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVSeasonBannerFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVEpisodePosterFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowActorThumbsFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVSeasonFanartFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowFanartFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowPosterFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVXBMCAdditional As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVShowTVThemeXBMC As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVSeasonLandscapeXBMC As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowLandscapeXBMC As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowCharacterArtXBMC As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowClearArtXBMC As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowClearLogoXBMC As System.Windows.Forms.CheckBox
    Friend WithEvents txtTVShowTVThemeFolderXBMC As System.Windows.Forms.TextBox
    Friend WithEvents btnTVShowTVThemeBrowse As System.Windows.Forms.Button
    Friend WithEvents gbTVShowBannerOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtTVShowBannerWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtTVShowBannerHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblTVShowBannerWidth As System.Windows.Forms.Label
    Friend WithEvents lblTVShowBannerHeight As System.Windows.Forms.Label
    Friend WithEvents chkTVShowBannerResize As System.Windows.Forms.CheckBox
    Friend WithEvents lblTVShowBannerType As System.Windows.Forms.Label
    Friend WithEvents cbTVShowBannerPrefType As System.Windows.Forms.ComboBox
    Friend WithEvents chkTVShowBannerOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVSeasonBannerOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtTVSeasonBannerWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtTVSeasonBannerHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblTVSeasonBannerWidth As System.Windows.Forms.Label
    Friend WithEvents lblTVSeasonBannerHeight As System.Windows.Forms.Label
    Friend WithEvents chkTVSeasonBannerResize As System.Windows.Forms.CheckBox
    Friend WithEvents lblTVSeasonBannerType As System.Windows.Forms.Label
    Friend WithEvents cbTVSeasonBannerPrefType As System.Windows.Forms.ComboBox
    Friend WithEvents chkTVSeasonBannerOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVSeasonLandscapeOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVSeasonLandscapeOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents tpTVAllSeasons As System.Windows.Forms.TabPage
    Friend WithEvents gbTVASFanartOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtTVASFanartWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtTVASFanartHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblTVASFanartWidth As System.Windows.Forms.Label
    Friend WithEvents lblTVASFanartHeight As System.Windows.Forms.Label
    Friend WithEvents chkTVASFanartResize As System.Windows.Forms.CheckBox
    Friend WithEvents cbTVASFanartPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblTVASFanartSize As System.Windows.Forms.Label
    Friend WithEvents chkTVASFanartOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVASBannerOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtTVASBannerWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtTVASBannerHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblTVASBannerWidth As System.Windows.Forms.Label
    Friend WithEvents lblTVASBannerHeight As System.Windows.Forms.Label
    Friend WithEvents chkTVASBannerResize As System.Windows.Forms.CheckBox
    Friend WithEvents lblTVASBannerType As System.Windows.Forms.Label
    Friend WithEvents cbTVASBannerPrefType As System.Windows.Forms.ComboBox
    Friend WithEvents chkTVASBannerOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVASPosterOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtTVASPosterWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtTVASPosterHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblTVASPosterWidth As System.Windows.Forms.Label
    Friend WithEvents lblTVASPosterHeight As System.Windows.Forms.Label
    Friend WithEvents chkTVASPosterResize As System.Windows.Forms.CheckBox
    Friend WithEvents lblTVASPosterSize As System.Windows.Forms.Label
    Friend WithEvents cbTVASPosterPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents chkTVASPosterOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVShowLandscapeOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVShowLandscapeOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVShowClearArtOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVShowClearArtOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVShowClearLogoOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVShowClearLogoOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVShowCharacterArtOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVShowCharacterArtOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieImagesBannerOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieBannerPrefOnly As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieBannerWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieBannerHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieBannerWidth As System.Windows.Forms.Label
    Friend WithEvents lblMovieBannerHeight As System.Windows.Forms.Label
    Friend WithEvents chkMovieBannerResize As System.Windows.Forms.CheckBox
    Friend WithEvents lblMovieBannerType As System.Windows.Forms.Label
    Friend WithEvents cbMovieBannerPrefType As System.Windows.Forms.ComboBox
    Friend WithEvents chkMovieBannerOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieImagesLandscapeOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieLandscapeOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieImagesClearArtOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieClearArtOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieImagesClearLogoOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieClearLogoOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieImagesDiscArtOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieDiscArtOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieImagesActorThumbsOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieActorThumbsOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents cbMovieTrailerMinQual As System.Windows.Forms.ComboBox
    Friend WithEvents lblMovieTrailerMinQual As System.Windows.Forms.Label
    Friend WithEvents lblTVScraperDurationRuntimeFormat As System.Windows.Forms.Label
    Friend WithEvents lblTVScraperRatingRegion As System.Windows.Forms.Label
    Friend WithEvents cbTVScraperRatingRegion As System.Windows.Forms.ComboBox
    Friend WithEvents txtMovieEFanartsLimit As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieEThumbsLimit As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieYAMJCompatibleSets As System.Windows.Forms.CheckBox
    Friend WithEvents lblMovieEFanartsLimit As System.Windows.Forms.Label
    Friend WithEvents lblMovieEThumbsLimit As System.Windows.Forms.Label
    Friend WithEvents gbTVASLandscapeOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVASLandscapeOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVNMT As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVSeasonPosterNMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowBannerNMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVSeasonBannerNMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVEpisodePosterNMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVSeasonFanartNMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowFanartNMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowPosterNMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVUseNMJ As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVYAMJ As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVSeasonPosterYAMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowBannerYAMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVSeasonBannerYAMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVEpisodePosterYAMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVSeasonFanartYAMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowFanartYAMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowPosterYAMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVUseYAMJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVLockShowStatus As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperShowStatus As System.Windows.Forms.CheckBox
    Friend WithEvents pnlMovieThemes As System.Windows.Forms.Panel
    Friend WithEvents pnlTVThemes As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkMovieBannerCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieThemeCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLandscapeCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingTheme As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingBanner As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieXBMCTheme As System.Windows.Forms.GroupBox
    Friend WithEvents btnMovieXBMCThemeCustomPathBrowse As System.Windows.Forms.Button
    Friend WithEvents chkMovieXBMCThemeSub As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieXBMCThemeSubDir As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieXBMCThemeCustomPath As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieXBMCThemeCustom As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieXBMCThemeEnable As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieXBMCThemeMovie As System.Windows.Forms.CheckBox
    Friend WithEvents gbFileSystemValidThemeExts As System.Windows.Forms.GroupBox
    Friend WithEvents btnFileSystemValidThemeExtsReset As System.Windows.Forms.Button
    Friend WithEvents btnFileSystemValidThemeExtsRemove As System.Windows.Forms.Button
    Friend WithEvents btnFileSystemValidThemeExtsAdd As System.Windows.Forms.Button
    Friend WithEvents txtFileSystemValidThemeExts As System.Windows.Forms.TextBox
    Friend WithEvents lstFileSystemValidThemeExts As System.Windows.Forms.ListBox
    Friend WithEvents gbMovieThemeOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieThemeOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieThemeEnable As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieDiscArtCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieClearLogoCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieClearArtCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingDiscArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMissingClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents tpMovieFileNamingBoxee As System.Windows.Forms.TabPage
    Friend WithEvents gbMovieBoxee As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieUseBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieFanartBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoviePosterBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieNFOBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVGeneralLangOpts As System.Windows.Forms.GroupBox
    Friend WithEvents btnTVGeneralLangFetch As System.Windows.Forms.Button
    Friend WithEvents cbTVGeneralLang As System.Windows.Forms.ComboBox
    Friend WithEvents tpTVFileNamingBoxee As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVSeasonPosterBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowBannerBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVEpisodePosterBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowFanartBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowPosterBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVUseBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVEpisodeWatchedCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowClearLogoCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowClearArtCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowCharacterArtCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowEFanartsCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowThemeCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowLandscapeCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowBannerCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVSeasonLandscapeCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVSeasonBannerCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowExtrafanartsXBMC As System.Windows.Forms.CheckBox
    Friend WithEvents gbGeneralMainWindow As System.Windows.Forms.GroupBox
    Friend WithEvents chkGeneralHideLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkGeneralHideDiscArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkGeneralHideClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkGeneralHideBanner As System.Windows.Forms.CheckBox
    Friend WithEvents chkGeneralHideClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents chkGeneralHideCharacterArt As System.Windows.Forms.CheckBox
    Friend WithEvents cbGeneralMovieSetTheme As System.Windows.Forms.ComboBox
    Friend WithEvents lblGeneralMovieSetTheme As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblMovieTrailerDefaultSearch As System.Windows.Forms.Label
    Friend WithEvents txtMovieTrailerDefaultSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblTVEpisodePosterSize As System.Windows.Forms.Label
    Friend WithEvents cbTVEpisodePosterPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents gbMovieGeneralCustomMarker As System.Windows.Forms.GroupBox
    Friend WithEvents btnMovieGeneralCustomMarker4 As System.Windows.Forms.Button
    Friend WithEvents txtMovieGeneralCustomMarker4 As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieGeneralCustomMarker4 As System.Windows.Forms.Label
    Friend WithEvents btnMovieGeneralCustomMarker3 As System.Windows.Forms.Button
    Friend WithEvents txtMovieGeneralCustomMarker3 As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieGeneralCustomMarker3 As System.Windows.Forms.Label
    Friend WithEvents btnMovieGeneralCustomMarker2 As System.Windows.Forms.Button
    Friend WithEvents txtMovieGeneralCustomMarker2 As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieGeneralCustomMarker2 As System.Windows.Forms.Label
    Friend WithEvents btnMovieGeneralCustomMarker1 As System.Windows.Forms.Button
    Friend WithEvents txtMovieGeneralCustomMarker1 As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieGeneralCustomMarker1 As System.Windows.Forms.Label
    Friend WithEvents cdColor As System.Windows.Forms.ColorDialog
    Friend WithEvents gbGeneralScrapers As System.Windows.Forms.GroupBox
    Friend WithEvents chkGeneralDoubleClickScrape As System.Windows.Forms.CheckBox
    Friend WithEvents pnlMovieSetGeneral As System.Windows.Forms.Panel
    Friend WithEvents gbMovieSetGeneralMissingItemsOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieSetMissingDiscArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetMissingClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetMissingClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetMissingLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetMissingBanner As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetMissingPoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetMissingNFO As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetMissingFanart As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieSetGeneralMiscOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieSetClickScrape As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetClickScrapeAsk As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieSetGeneralMediaListOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieSetDiscArtCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetClearLogoCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetClearArtCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetBannerCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetLandscapeCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetPosterCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetNFOCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetFanartCol As System.Windows.Forms.CheckBox
    Friend WithEvents pnlMovieSetSources As System.Windows.Forms.Panel
    Friend WithEvents gbMovieSetFileNaming As System.Windows.Forms.GroupBox
    Friend WithEvents pnlMovieSetScraper As System.Windows.Forms.Panel
    Friend WithEvents chkMovieSetLockTitle As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieSetScraperGlobalOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieSetScraperPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetScraperTitle As System.Windows.Forms.CheckBox
    Friend WithEvents pnlMovieSetImages As System.Windows.Forms.Panel
    Friend WithEvents gbMovieSetImagesClearArtOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieSetClearArtOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieSetImagesClearLogoOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieSetClearLogoOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieSetImagesBannerOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieSetBannerPrefOnly As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieSetBannerWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetBannerHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieSetBannerWidth As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetBannerHeight As System.Windows.Forms.Label
    Friend WithEvents chkMovieSetBannerResize As System.Windows.Forms.CheckBox
    Friend WithEvents lblMovieSetBannerType As System.Windows.Forms.Label
    Friend WithEvents cbMovieSetBannerPrefType As System.Windows.Forms.ComboBox
    Friend WithEvents chkMovieSetBannerOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieSetImagesLandscapeOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieSetLandscapeOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieSetImagesFanartOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtMovieSetFanartWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetFanartHeight As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieSetFanartPrefOnly As System.Windows.Forms.CheckBox
    Friend WithEvents lblMovieSetFanartWidth As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetFanartHeight As System.Windows.Forms.Label
    Friend WithEvents chkMovieSetFanartResize As System.Windows.Forms.CheckBox
    Friend WithEvents cbMovieSetFanartPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblMovieSetFanartSize As System.Windows.Forms.Label
    Friend WithEvents chkMovieSetFanartOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieSetImagesPosterOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieSetPosterPrefOnly As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieSetPosterWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetPosterHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieSetPosterWidth As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetPosterHeight As System.Windows.Forms.Label
    Friend WithEvents chkMovieSetPosterResize As System.Windows.Forms.CheckBox
    Friend WithEvents lblMovieSetPosterSize As System.Windows.Forms.Label
    Friend WithEvents cbMovieSetPosterPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents chkMovieSetPosterOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents tcMovieSetFileNaming As System.Windows.Forms.TabControl
    Friend WithEvents tpMovieSetFileNamingXBMC As System.Windows.Forms.TabPage
    Friend WithEvents gbMovieSetMSAA As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieSetUseMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetLandscapeMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetBannerMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetClearArtMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetClearLogoMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetFanartMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetPosterMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetNFOMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents pbMSAAInfo As System.Windows.Forms.PictureBox
    Friend WithEvents gbMovieSetSortTokensOpts As System.Windows.Forms.GroupBox
    Friend WithEvents btnMovieSetSortTokenRemove As System.Windows.Forms.Button
    Friend WithEvents btnMovieSetSortTokenAdd As System.Windows.Forms.Button
    Friend WithEvents txtMovieSetSortToken As System.Windows.Forms.TextBox
    Friend WithEvents lstMovieSetSortTokens As System.Windows.Forms.ListBox
    Friend WithEvents gbGeneralDateAdded As System.Windows.Forms.GroupBox
    Friend WithEvents cbGeneralDateTime As System.Windows.Forms.ComboBox
    Friend WithEvents chkMovieScraperCleanPlotOutline As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieScraperCertificationOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieScraperDetailView As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperShowRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVLockShowRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkGeneralShowImgNames As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperCleanFields As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVSortTokensOpts As System.Windows.Forms.GroupBox
    Friend WithEvents btnTVSortTokenRemove As System.Windows.Forms.Button
    Friend WithEvents btnTVSortTokenAdd As System.Windows.Forms.Button
    Friend WithEvents txtTVSortToken As System.Windows.Forms.TextBox
    Friend WithEvents lstTVSortTokens As System.Windows.Forms.ListBox
    Friend WithEvents chkTVDisplayStatus As System.Windows.Forms.CheckBox
    Friend WithEvents colExclude As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkTVScraperEpisodeRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVLockEpisodeRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVScraperMiscOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVScraperUseSRuntimeForEp As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperCollectionsAuto As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperReleaseFormat As System.Windows.Forms.CheckBox
    Friend WithEvents gbFileSystemExcludedDirs As System.Windows.Forms.GroupBox
    Friend WithEvents btnFileSystemExcludedDirsRemove As System.Windows.Forms.Button
    Friend WithEvents btnFileSystemExcludedDirsAdd As System.Windows.Forms.Button
    Friend WithEvents txtFileSystemExcludedDirs As System.Windows.Forms.TextBox
    Friend WithEvents lstFileSystemExcludedDirs As System.Windows.Forms.ListBox
    Friend WithEvents gbTVGeneralMissingItemsOpts As System.Windows.Forms.GroupBox
    Friend WithEvents gbTVGeneralMissingShowOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVShowMissingBanner As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowMissingTheme As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowMissingClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowMissingLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowMissingCharacterArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowMissingEFanarts As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowMissingPoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowMissingClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowMissingNFO As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowMissingFanart As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieSetSourceMiscOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieSetCleanFiles As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetCleanDB As System.Windows.Forms.CheckBox
    Friend WithEvents dgvMovieSetScraperTitleRenamer As System.Windows.Forms.DataGridView
    Friend WithEvents gbMovieSetScraperTitleRenamerOpts As System.Windows.Forms.GroupBox
    Friend WithEvents btnMovieSetScraperTitleRenamerRemove As System.Windows.Forms.Button
    Friend WithEvents btnMovieSetScraperTitleRenamerAdd As System.Windows.Forms.Button
    Friend WithEvents tbcMovieSetScrapedTitleRenamerFrom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tbcMovieSetScrapedTitleRenamerTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkMovieScraperStudioWithImg As System.Windows.Forms.CheckBox
    Friend WithEvents gbFileSystemValidSubtitlesExts As System.Windows.Forms.GroupBox
    Friend WithEvents btnFileSystemValidSubtitlesExtsRemove As System.Windows.Forms.Button
    Friend WithEvents btnFileSystemValidSubtitlesExtsAdd As System.Windows.Forms.Button
    Friend WithEvents txtFileSystemValidSubtitlesExts As System.Windows.Forms.TextBox
    Friend WithEvents lstFileSystemValidSubtitlesExts As System.Windows.Forms.ListBox
    Friend WithEvents btnFileSystemValidSubtitlesExtsReset As System.Windows.Forms.Button
    Friend WithEvents tblMovieScraperGlobalOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtMovieScraperStudioLimit As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieLockCollections As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperCollectionID As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperOriginalTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockOriginalTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperTags As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieScraperGenreLimit As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieScraperTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperTitle As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieScraperCastLimit As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieLockTags As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockGenre As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperGenre As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperOutline As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperCredits As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockCredits As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockOutline As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockCountry As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperDirector As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperCert As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockDirector As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperVotes As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockVotes As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperCast As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkMovieLockTop250 As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockActors As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkMovieScraperCountry As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkMovieLockPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockReleaseDate As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockYear As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperRelease As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockCert As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperTop250 As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperYear As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockTagline As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockStudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperStudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperTagline As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockTitle As System.Windows.Forms.CheckBox
    Friend WithEvents lblMovieScraperTitle As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperGenre As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperOutline As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperPlot As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperYear As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperRating As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperTagline As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperRuntime As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperTop250 As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperStudio As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperCountry As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperRelease As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperCertification As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperOriginalTitle As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperCast As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperVotes As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperDirector As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperCredits As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperTags As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperTrailer As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperCollectionID As System.Windows.Forms.Label
    Friend WithEvents lblMovieLockLanguageA As System.Windows.Forms.Label
    Friend WithEvents lblMovieLockLanguageV As System.Windows.Forms.Label
    Friend WithEvents lblMovieLockCollections As System.Windows.Forms.Label
    Friend WithEvents lblMovieScraperMPAA As System.Windows.Forms.Label
    Friend WithEvents chkMovieScraperMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLockMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperCertForMPAAFallback As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVShowEFanartsOpts As System.Windows.Forms.GroupBox
    Friend WithEvents lblTVShowEFanartsLimit As System.Windows.Forms.Label
    Friend WithEvents txtTVShowEFanartsLimit As System.Windows.Forms.TextBox
    Friend WithEvents chkTVShowEFanartsPrefOnly As System.Windows.Forms.CheckBox
    Friend WithEvents txtTVShowEFanartsWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtTVShowEFanartsHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblTVShowEFanartsWidth As System.Windows.Forms.Label
    Friend WithEvents lblTVShowEFanartsHeight As System.Windows.Forms.Label
    Friend WithEvents chkTVShowEFanartsResize As System.Windows.Forms.CheckBox
    Friend WithEvents lblTVShowEFanartsSize As System.Windows.Forms.Label
    Friend WithEvents cbTVShowEFanartsPrefSize As System.Windows.Forms.ComboBox
    Friend WithEvents chkTVShowEFanartsOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents tpMovieSetFileNamingExpert As System.Windows.Forms.TabPage
    Friend WithEvents gbMovieSetExpert As System.Windows.Forms.GroupBox
    Friend WithEvents tcMovieSetFileNamingExpert As System.Windows.Forms.TabControl
    Friend WithEvents tpMovieSetFileNamingExpertSingle As System.Windows.Forms.TabPage
    Friend WithEvents lblMovieSetClearArtExpertSingle As System.Windows.Forms.Label
    Friend WithEvents txtMovieSetPosterExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetFanartExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetBannerExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetClearLogoExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetClearArtExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetLandscapeExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetNFOExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieSetLandscapeExpertSingle As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetBannerExpertSingle As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetClearLogoExpertSingle As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetFanartExpertSingle As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetPosterExpertSingle As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetNFOExpertSingle As System.Windows.Forms.Label
    Friend WithEvents tpMovieSetFileNamingExpertParent As System.Windows.Forms.TabPage
    Friend WithEvents txtMovieSetPosterExpertParent As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetFanartExpertParent As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetBannerExpertParent As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetClearLogoExpertParent As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetClearArtExpertParent As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetLandscapeExpertParent As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieSetNFOExpertParent As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieSetClearArtExpertParent As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetLandscapeExpertParent As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetBannerExpertParent As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetClearLogoExpertParent As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetFanartExpertParent As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetPosterExpertParent As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetNFOExpertParent As System.Windows.Forms.Label
    Friend WithEvents chkMovieSetUseExpert As System.Windows.Forms.CheckBox
    Friend WithEvents lblMovieSetPathExpertSingle As System.Windows.Forms.Label
    Friend WithEvents btnMovieSetPathExpertSingleBrowse As System.Windows.Forms.Button
    Friend WithEvents txtMovieSetPathExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkTVScraperShowVotes As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScraperEpisodeVotes As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVLockEpisodeVotes As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVLockShowVotes As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieMoviesetCol As System.Windows.Forms.CheckBox
    Friend WithEvents tblMovieSetScraperGlobalOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkMovieSetLockPlot As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetScraperTitle As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblMovieSetScraperPlot As System.Windows.Forms.Label
    Friend WithEvents scSettings As System.Windows.Forms.SplitContainer
    Friend WithEvents scSettingsBody As System.Windows.Forms.SplitContainer
    Friend WithEvents scSettingsMain As System.Windows.Forms.SplitContainer
    Friend WithEvents tblSettingsFooter As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblSettingsCurrent As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieScraper As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieImages As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieImagesPosterOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieImagesOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieImagesEFanartsOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieImagesBannerOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieImagesEThumbsOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieImagesLandscapeOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieImagesDiscArtOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieImagesClearLogoOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieImagesClearArtOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieImagesActorThumbsOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieImagesFanartOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieSetImages As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieSetImagesLandscapeOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieSetImagesClearLogoOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieSetImagesClearArtOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieSetImagesFanartOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieSetImagesBannerOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieSetImagesPosterOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblGeneral As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblGeneralDateAdded As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblGeneralScrapers As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkGeneralResumeScraper As System.Windows.Forms.CheckBox
    Friend WithEvents tblGeneralDaemon As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblGeneralMisc As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblGeneralInterface As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblGeneralMainWindow As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblGeneralThemes As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieSetScraper As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMovieSetScraperTitleRenamerOpts As System.Windows.Forms.TableLayoutPanel
End Class