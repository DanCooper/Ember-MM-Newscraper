﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.gb_DAEMON = New System.Windows.Forms.GroupBox()
        Me.lbl_DAEMON_driveletter = New System.Windows.Forms.Label()
        Me.cbo_DAEMON_driveletter = New System.Windows.Forms.ComboBox()
        Me.bt_DAEMON_Programpath = New System.Windows.Forms.Button()
        Me.txt_DAEMON_Programpath = New System.Windows.Forms.TextBox()
        Me.lbl_DAEMON_Programpath = New System.Windows.Forms.Label()
        Me.chkImagesGlassOverlay = New System.Windows.Forms.CheckBox()
        Me.chkNoDisplayFanartSmall = New System.Windows.Forms.CheckBox()
        Me.chkSpecialDateAdd = New System.Windows.Forms.CheckBox()
        Me.chkShowGenresText = New System.Windows.Forms.CheckBox()
        Me.chkSourceFromFolder = New System.Windows.Forms.CheckBox()
        Me.chkNoDisplayFanart = New System.Windows.Forms.CheckBox()
        Me.chkInfoPanelAnim = New System.Windows.Forms.CheckBox()
        Me.chkNoDisplayPoster = New System.Windows.Forms.CheckBox()
        Me.chkShowDims = New System.Windows.Forms.CheckBox()
        Me.chkUpdates = New System.Windows.Forms.CheckBox()
        Me.chkOverwriteNfo = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox29 = New System.Windows.Forms.GroupBox()
        Me.cbEpTheme = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbTVShowTheme = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbMovieTheme = New System.Windows.Forms.ComboBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.cbIntLang = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.tcCleaner = New System.Windows.Forms.TabControl()
        Me.tpStandard = New System.Windows.Forms.TabPage()
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
        Me.tpExpert = New System.Windows.Forms.TabPage()
        Me.chkWhitelistVideo = New System.Windows.Forms.CheckBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.btnRemoveWhitelist = New System.Windows.Forms.Button()
        Me.btnAddWhitelist = New System.Windows.Forms.Button()
        Me.txtWhitelist = New System.Windows.Forms.TextBox()
        Me.lstWhitelist = New System.Windows.Forms.ListBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.GroupBox12 = New System.Windows.Forms.GroupBox()
        Me.chkClickScrape = New System.Windows.Forms.CheckBox()
        Me.chkAskCheckboxScrape = New System.Windows.Forms.CheckBox()
        Me.chkMarkNew = New System.Windows.Forms.CheckBox()
        Me.pnlMovieImages = New System.Windows.Forms.Panel()
        Me.gbMovieImagesEFanarts = New System.Windows.Forms.GroupBox()
        Me.chkMovieEFanartsOnly = New System.Windows.Forms.CheckBox()
        Me.txtMovieEFanartsWidth = New System.Windows.Forms.TextBox()
        Me.txtMovieEFanartsHeight = New System.Windows.Forms.TextBox()
        Me.lblMovieEFanartsQual = New System.Windows.Forms.Label()
        Me.tbMovieEFanartsQual = New System.Windows.Forms.TrackBar()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.chkMovieResizeEFanarts = New System.Windows.Forms.CheckBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.cbMovieEFanartsSize = New System.Windows.Forms.ComboBox()
        Me.chkMovieOverwriteEFanarts = New System.Windows.Forms.CheckBox()
        Me.gbMovieImagesEThumbs = New System.Windows.Forms.GroupBox()
        Me.chkMovieEThumbsOnly = New System.Windows.Forms.CheckBox()
        Me.txtMovieEThumbsWidth = New System.Windows.Forms.TextBox()
        Me.txtMovieEThumbsHeight = New System.Windows.Forms.TextBox()
        Me.lblMovieEThumbsQual = New System.Windows.Forms.Label()
        Me.tbMovieEThumbsQual = New System.Windows.Forms.TrackBar()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.chkMovieResizeEThumbs = New System.Windows.Forms.CheckBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cbMovieEThumbsSize = New System.Windows.Forms.ComboBox()
        Me.chkMovieOverwriteEThumbs = New System.Windows.Forms.CheckBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.chkMovieScraperActorThumbs = New System.Windows.Forms.CheckBox()
        Me.chkMovieNoSaveImagesToNfo = New System.Windows.Forms.CheckBox()
        Me.chkMovieSingleScrapeImages = New System.Windows.Forms.CheckBox()
        Me.gbMovieImagesFanart = New System.Windows.Forms.GroupBox()
        Me.txtMovieFanartWidth = New System.Windows.Forms.TextBox()
        Me.txtMovieFanartHeight = New System.Windows.Forms.TextBox()
        Me.chkMovieFanartOnly = New System.Windows.Forms.CheckBox()
        Me.lblMovieFanartQual = New System.Windows.Forms.Label()
        Me.tbMovieFanartQual = New System.Windows.Forms.TrackBar()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.chkMovieResizeFanart = New System.Windows.Forms.CheckBox()
        Me.cbMovieFanartSize = New System.Windows.Forms.ComboBox()
        Me.lblFanartSize = New System.Windows.Forms.Label()
        Me.chkMovieOverwriteFanart = New System.Windows.Forms.CheckBox()
        Me.gbMovieImagesPoster = New System.Windows.Forms.GroupBox()
        Me.chkMoviePosterOnly = New System.Windows.Forms.CheckBox()
        Me.txtMoviePosterWidth = New System.Windows.Forms.TextBox()
        Me.txtMoviePosterHeight = New System.Windows.Forms.TextBox()
        Me.lblMoviePosterQual = New System.Windows.Forms.Label()
        Me.tbMoviePosterQual = New System.Windows.Forms.TrackBar()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.chkMovieResizePoster = New System.Windows.Forms.CheckBox()
        Me.lblPosterSize = New System.Windows.Forms.Label()
        Me.cbMoviePosterSize = New System.Windows.Forms.ComboBox()
        Me.chkMovieOverwritePoster = New System.Windows.Forms.CheckBox()
        Me.lbGenre = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkMovieWatchedCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieEFanartsCol = New System.Windows.Forms.CheckBox()
        Me.txtCheckTitleTol = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.chkCheckTitles = New System.Windows.Forms.CheckBox()
        Me.GroupBox25 = New System.Windows.Forms.GroupBox()
        Me.btnRemoveToken = New System.Windows.Forms.Button()
        Me.btnAddToken = New System.Windows.Forms.Button()
        Me.txtSortToken = New System.Windows.Forms.TextBox()
        Me.lstSortTokens = New System.Windows.Forms.ListBox()
        Me.chkDisplayYear = New System.Windows.Forms.CheckBox()
        Me.chkMovieEThumbsCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieSubCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieTrailerCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieInfoCol = New System.Windows.Forms.CheckBox()
        Me.chkMovieFanartCol = New System.Windows.Forms.CheckBox()
        Me.chkMoviePosterCol = New System.Windows.Forms.CheckBox()
        Me.lvMovies = New System.Windows.Forms.ListView()
        Me.colID = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colPath = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colRecur = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colFolder = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colSingle = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnMovieRem = New System.Windows.Forms.Button()
        Me.btnMovieAddFolder = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.ilSettings = New System.Windows.Forms.ImageList(Me.components)
        Me.tvSettings = New System.Windows.Forms.TreeView()
        Me.pnlGeneral = New System.Windows.Forms.Panel()
        Me.gbInterface = New System.Windows.Forms.GroupBox()
        Me.pnlMovieGeneral = New System.Windows.Forms.Panel()
        Me.GroupBox30 = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtIMDBURL = New System.Windows.Forms.TextBox()
        Me.gbGenreFilter = New System.Windows.Forms.GroupBox()
        Me.gbFilters = New System.Windows.Forms.GroupBox()
        Me.btnResetMovieFilters = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.chkProperCase = New System.Windows.Forms.CheckBox()
        Me.btnRemoveFilter = New System.Windows.Forms.Button()
        Me.btnAddFilter = New System.Windows.Forms.Button()
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.lstFilters = New System.Windows.Forms.ListBox()
        Me.GroupBox27 = New System.Windows.Forms.GroupBox()
        Me.chkMissingEFanarts = New System.Windows.Forms.CheckBox()
        Me.chkMissingEThumbs = New System.Windows.Forms.CheckBox()
        Me.chkMissingSubs = New System.Windows.Forms.CheckBox()
        Me.chkMissingTrailer = New System.Windows.Forms.CheckBox()
        Me.chkMissingNFO = New System.Windows.Forms.CheckBox()
        Me.chkMissingFanart = New System.Windows.Forms.CheckBox()
        Me.chkMissingPoster = New System.Windows.Forms.CheckBox()
        Me.pnlExtensions = New System.Windows.Forms.Panel()
        Me.GroupBox22 = New System.Windows.Forms.GroupBox()
        Me.btnRemoveNoStack = New System.Windows.Forms.Button()
        Me.btnAddNoStack = New System.Windows.Forms.Button()
        Me.txtNoStack = New System.Windows.Forms.TextBox()
        Me.lstNoStack = New System.Windows.Forms.ListBox()
        Me.GroupBox18 = New System.Windows.Forms.GroupBox()
        Me.btnResetValidExts = New System.Windows.Forms.Button()
        Me.btnRemMovieExt = New System.Windows.Forms.Button()
        Me.btnAddMovieExt = New System.Windows.Forms.Button()
        Me.txtMovieExt = New System.Windows.Forms.TextBox()
        Me.lstMovieExts = New System.Windows.Forms.ListBox()
        Me.pnlProxy = New System.Windows.Forms.Panel()
        Me.gbProxy = New System.Windows.Forms.GroupBox()
        Me.gbCreds = New System.Windows.Forms.GroupBox()
        Me.txtProxyDomain = New System.Windows.Forms.TextBox()
        Me.lblProxyDomain = New System.Windows.Forms.Label()
        Me.txtProxyPassword = New System.Windows.Forms.TextBox()
        Me.txtProxyUsername = New System.Windows.Forms.TextBox()
        Me.lblProxyUN = New System.Windows.Forms.Label()
        Me.lblProxyPW = New System.Windows.Forms.Label()
        Me.chkEnableCredentials = New System.Windows.Forms.CheckBox()
        Me.lblProxyPort = New System.Windows.Forms.Label()
        Me.lblProxyURI = New System.Windows.Forms.Label()
        Me.txtProxyPort = New System.Windows.Forms.TextBox()
        Me.txtProxyURI = New System.Windows.Forms.TextBox()
        Me.chkEnableProxy = New System.Windows.Forms.CheckBox()
        Me.gbMovieBackdropsFolder = New System.Windows.Forms.GroupBox()
        Me.chkAutoBD = New System.Windows.Forms.CheckBox()
        Me.btnBrowseBackdrops = New System.Windows.Forms.Button()
        Me.txtBDPath = New System.Windows.Forms.TextBox()
        Me.lblCurrent = New System.Windows.Forms.Label()
        Me.pnlCurrent = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pbCurrent = New System.Windows.Forms.PictureBox()
        Me.pnlMovieSources = New System.Windows.Forms.Panel()
        Me.gbMovieSetsFolder = New System.Windows.Forms.GroupBox()
        Me.btnBrowseMoviesets = New System.Windows.Forms.Button()
        Me.txtMoviesetsPath = New System.Windows.Forms.TextBox()
        Me.gbMovieFileNaming = New System.Windows.Forms.GroupBox()
        Me.tbcMovieFileNaming = New System.Windows.Forms.TabControl()
        Me.tpMovieFileNamingXBMC = New System.Windows.Forms.TabPage()
        Me.gbMovieXBMCOptional = New System.Windows.Forms.GroupBox()
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
        Me.gbMovieNMTOptional = New System.Windows.Forms.GroupBox()
        Me.btnMovieBrowseWatchedFiles = New System.Windows.Forms.Button()
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
        Me.tpMovieFileNamingExpert = New System.Windows.Forms.TabPage()
        Me.gbMovieExpert = New System.Windows.Forms.GroupBox()
        Me.tbcMovieFileNamingExpert = New System.Windows.Forms.TabControl()
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
        Me.tbMovieFileNamingExpertVTS = New System.Windows.Forms.TabPage()
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
        Me.tbMovieFileNamingExpertBDMV = New System.Windows.Forms.TabPage()
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
        Me.btnEditSource = New System.Windows.Forms.Button()
        Me.gbMovieMiscOptions = New System.Windows.Forms.GroupBox()
        Me.chkScanOrderModify = New System.Windows.Forms.CheckBox()
        Me.chkSortBeforeScan = New System.Windows.Forms.CheckBox()
        Me.chkIgnoreLastScan = New System.Windows.Forms.CheckBox()
        Me.chkCleanDB = New System.Windows.Forms.CheckBox()
        Me.chkSkipStackedSizeCheck = New System.Windows.Forms.CheckBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtSkipLessThan = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.pnlTVGeneral = New System.Windows.Forms.Panel()
        Me.gbTVListOptions = New System.Windows.Forms.GroupBox()
        Me.chkDisplayMissingEpisodes = New System.Windows.Forms.CheckBox()
        Me.gbEpisodeListOptions = New System.Windows.Forms.GroupBox()
        Me.chkEpisodeNfoCol = New System.Windows.Forms.CheckBox()
        Me.chkEpisodeFanartCol = New System.Windows.Forms.CheckBox()
        Me.chkEpisodePosterCol = New System.Windows.Forms.CheckBox()
        Me.gbSeasonListOptions = New System.Windows.Forms.GroupBox()
        Me.chkSeasonFanartCol = New System.Windows.Forms.CheckBox()
        Me.chkSeasonPosterCol = New System.Windows.Forms.CheckBox()
        Me.gbShowListOptions = New System.Windows.Forms.GroupBox()
        Me.chkShowNfoCol = New System.Windows.Forms.CheckBox()
        Me.chkShowFanartCol = New System.Windows.Forms.CheckBox()
        Me.chkShowPosterCol = New System.Windows.Forms.CheckBox()
        Me.gbEpFilter = New System.Windows.Forms.GroupBox()
        Me.btnResetEpFilter = New System.Windows.Forms.Button()
        Me.chkNoFilterEpisode = New System.Windows.Forms.CheckBox()
        Me.btnEpFilterDown = New System.Windows.Forms.Button()
        Me.btnEpFilterUp = New System.Windows.Forms.Button()
        Me.chkEpProperCase = New System.Windows.Forms.CheckBox()
        Me.btnRemoveEpFilter = New System.Windows.Forms.Button()
        Me.btnAddEpFilter = New System.Windows.Forms.Button()
        Me.txtEpFilter = New System.Windows.Forms.TextBox()
        Me.lstEpFilters = New System.Windows.Forms.ListBox()
        Me.gbTVMisc = New System.Windows.Forms.GroupBox()
        Me.chkMarkNewShows = New System.Windows.Forms.CheckBox()
        Me.chkMarkNewEpisodes = New System.Windows.Forms.CheckBox()
        Me.chkDisplayAllSeason = New System.Windows.Forms.CheckBox()
        Me.lblRatingRegion = New System.Windows.Forms.Label()
        Me.cbRatingRegion = New System.Windows.Forms.ComboBox()
        Me.gbShowFilter = New System.Windows.Forms.GroupBox()
        Me.btnResetShowFilters = New System.Windows.Forms.Button()
        Me.btnShowFilterDown = New System.Windows.Forms.Button()
        Me.btnShowFilterUp = New System.Windows.Forms.Button()
        Me.chkShowProperCase = New System.Windows.Forms.CheckBox()
        Me.btnRemoveShowFilter = New System.Windows.Forms.Button()
        Me.btnAddShowFilter = New System.Windows.Forms.Button()
        Me.txtShowFilter = New System.Windows.Forms.TextBox()
        Me.lstShowFilters = New System.Windows.Forms.ListBox()
        Me.pnlTVSources = New System.Windows.Forms.Panel()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.gbTVFileNaming = New System.Windows.Forms.GroupBox()
        Me.tbcTVFileNaming = New System.Windows.Forms.TabControl()
        Me.tpTVFileNamingXBMC = New System.Windows.Forms.TabPage()
        Me.gbTVXBMCAdditional = New System.Windows.Forms.GroupBox()
        Me.btnTVBrowseTVTheme = New System.Windows.Forms.Button()
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
        Me.chkTVEpisodeFanartFrodo = New System.Windows.Forms.CheckBox()
        Me.chkTVEpisodePosterFrodo = New System.Windows.Forms.CheckBox()
        Me.chkTVShowActorThumbsFrodo = New System.Windows.Forms.CheckBox()
        Me.chkTVSeasonFanartFrodo = New System.Windows.Forms.CheckBox()
        Me.chkTVShowFanartFrodo = New System.Windows.Forms.CheckBox()
        Me.chkTVShowPosterFrodo = New System.Windows.Forms.CheckBox()
        Me.tpTVFileNamingNMT = New System.Windows.Forms.TabPage()
        Me.tpTVFileNamingExpert = New System.Windows.Forms.TabPage()
        Me.lvTVSources = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.gbMiscTVSourceOpts = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTVSkipLessThan = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkTVScanOrderModify = New System.Windows.Forms.CheckBox()
        Me.chkTVIgnoreLastScan = New System.Windows.Forms.CheckBox()
        Me.chkTVCleanDB = New System.Windows.Forms.CheckBox()
        Me.btnAddTVSource = New System.Windows.Forms.Button()
        Me.btnEditTVSource = New System.Windows.Forms.Button()
        Me.btnRemTVSource = New System.Windows.Forms.Button()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.btnGetTVProfiles = New System.Windows.Forms.Button()
        Me.btnRegexDown = New System.Windows.Forms.Button()
        Me.btnRegexUp = New System.Windows.Forms.Button()
        Me.btnResetShowRegex = New System.Windows.Forms.Button()
        Me.gbShowRegex = New System.Windows.Forms.GroupBox()
        Me.btnClearRegex = New System.Windows.Forms.Button()
        Me.lblSeasonMatch = New System.Windows.Forms.Label()
        Me.btnAddShowRegex = New System.Windows.Forms.Button()
        Me.txtSeasonRegex = New System.Windows.Forms.TextBox()
        Me.lblEpisodeRetrieve = New System.Windows.Forms.Label()
        Me.cboSeasonRetrieve = New System.Windows.Forms.ComboBox()
        Me.lblSeasonRetrieve = New System.Windows.Forms.Label()
        Me.txtEpRegex = New System.Windows.Forms.TextBox()
        Me.lblEpisodeMatch = New System.Windows.Forms.Label()
        Me.cboEpRetrieve = New System.Windows.Forms.ComboBox()
        Me.btnEditShowRegex = New System.Windows.Forms.Button()
        Me.btnRemoveShowRegex = New System.Windows.Forms.Button()
        Me.lvShowRegex = New System.Windows.Forms.ListView()
        Me.colRegID = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colSeason = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colSeasonApply = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colEpisode = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.colEpApply = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.ToolTips = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlTVImages = New System.Windows.Forms.Panel()
        Me.TabControl3 = New System.Windows.Forms.TabControl()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.gbAllSPosterOpts = New System.Windows.Forms.GroupBox()
        Me.rbAllSPoster = New System.Windows.Forms.RadioButton()
        Me.rbAllSBanner = New System.Windows.Forms.RadioButton()
        Me.txtAllSPosterWidth = New System.Windows.Forms.TextBox()
        Me.txtAllSPosterHeight = New System.Windows.Forms.TextBox()
        Me.lblAllSPosterQual = New System.Windows.Forms.Label()
        Me.tbAllSPosterQual = New System.Windows.Forms.TrackBar()
        Me.lblAllSPosterQ = New System.Windows.Forms.Label()
        Me.lblAllSPosterWidth = New System.Windows.Forms.Label()
        Me.lblAllSPosterHeight = New System.Windows.Forms.Label()
        Me.chkResizeAllSPoster = New System.Windows.Forms.CheckBox()
        Me.lblAllSPosterSize = New System.Windows.Forms.Label()
        Me.cbAllSPosterSize = New System.Windows.Forms.ComboBox()
        Me.chkOverwriteAllSPoster = New System.Windows.Forms.CheckBox()
        Me.gbShowPosterOpts = New System.Windows.Forms.GroupBox()
        Me.rbPoster = New System.Windows.Forms.RadioButton()
        Me.rbBanner = New System.Windows.Forms.RadioButton()
        Me.txtShowPosterWidth = New System.Windows.Forms.TextBox()
        Me.txtShowPosterHeight = New System.Windows.Forms.TextBox()
        Me.lblShowPosterQual = New System.Windows.Forms.Label()
        Me.tbShowPosterQual = New System.Windows.Forms.TrackBar()
        Me.lblShowPosterQ = New System.Windows.Forms.Label()
        Me.lblShowPosterWidth = New System.Windows.Forms.Label()
        Me.lblShowPosterHeight = New System.Windows.Forms.Label()
        Me.chkResizeShowPoster = New System.Windows.Forms.CheckBox()
        Me.lblShowPosterSize = New System.Windows.Forms.Label()
        Me.cbShowPosterSize = New System.Windows.Forms.ComboBox()
        Me.chkOverwriteShowPoster = New System.Windows.Forms.CheckBox()
        Me.gbShowFanartOpts = New System.Windows.Forms.GroupBox()
        Me.txtShowFanartWidth = New System.Windows.Forms.TextBox()
        Me.txtShowFanartHeight = New System.Windows.Forms.TextBox()
        Me.lblShowFanartQual = New System.Windows.Forms.Label()
        Me.tbShowFanartQual = New System.Windows.Forms.TrackBar()
        Me.lblShowFanartQ = New System.Windows.Forms.Label()
        Me.lblShowFanartWidth = New System.Windows.Forms.Label()
        Me.lblShowFanartHeight = New System.Windows.Forms.Label()
        Me.chkResizeShowFanart = New System.Windows.Forms.CheckBox()
        Me.cbShowFanartSize = New System.Windows.Forms.ComboBox()
        Me.lblShowFanartSize = New System.Windows.Forms.Label()
        Me.chkOverwriteShowFanart = New System.Windows.Forms.CheckBox()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.gbSeaPosterOpts = New System.Windows.Forms.GroupBox()
        Me.txtSeaPosterWidth = New System.Windows.Forms.TextBox()
        Me.txtSeaPosterHeight = New System.Windows.Forms.TextBox()
        Me.lblSeaPosterQual = New System.Windows.Forms.Label()
        Me.tbSeaPosterQual = New System.Windows.Forms.TrackBar()
        Me.lblSeaPosterQ = New System.Windows.Forms.Label()
        Me.lblSeaPosterWidth = New System.Windows.Forms.Label()
        Me.lblSeaPosterHeight = New System.Windows.Forms.Label()
        Me.chkSeaResizePoster = New System.Windows.Forms.CheckBox()
        Me.lblSeaPosterSize = New System.Windows.Forms.Label()
        Me.cbSeaPosterSize = New System.Windows.Forms.ComboBox()
        Me.chkSeaOverwritePoster = New System.Windows.Forms.CheckBox()
        Me.gbSeaFanartOpts = New System.Windows.Forms.GroupBox()
        Me.txtSeaFanartWidth = New System.Windows.Forms.TextBox()
        Me.txtSeaFanartHeight = New System.Windows.Forms.TextBox()
        Me.lblSeaFanartQual = New System.Windows.Forms.Label()
        Me.tbSeaFanartQual = New System.Windows.Forms.TrackBar()
        Me.lblSeaFanartQ = New System.Windows.Forms.Label()
        Me.lblSeaFanartWidth = New System.Windows.Forms.Label()
        Me.lblSeaFanartHeight = New System.Windows.Forms.Label()
        Me.chkSeaResizeFanart = New System.Windows.Forms.CheckBox()
        Me.cbSeaFanartSize = New System.Windows.Forms.ComboBox()
        Me.lblSeaFanartSize = New System.Windows.Forms.Label()
        Me.chkSeaOverwriteFanart = New System.Windows.Forms.CheckBox()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.gbEpPosterOpts = New System.Windows.Forms.GroupBox()
        Me.txtEpPosterWidth = New System.Windows.Forms.TextBox()
        Me.txtEpPosterHeight = New System.Windows.Forms.TextBox()
        Me.lblEpPosterQual = New System.Windows.Forms.Label()
        Me.tbEpPosterQual = New System.Windows.Forms.TrackBar()
        Me.lblEpPosterQ = New System.Windows.Forms.Label()
        Me.lblEpPosterWidth = New System.Windows.Forms.Label()
        Me.lblEpPosterHeight = New System.Windows.Forms.Label()
        Me.chkResizeEpPoster = New System.Windows.Forms.CheckBox()
        Me.chkOverwriteEpPoster = New System.Windows.Forms.CheckBox()
        Me.gbEpFanartOpts = New System.Windows.Forms.GroupBox()
        Me.txtEpFanartWidth = New System.Windows.Forms.TextBox()
        Me.txtEpFanartHeight = New System.Windows.Forms.TextBox()
        Me.lblEpFanartQual = New System.Windows.Forms.Label()
        Me.tbEpFanartQual = New System.Windows.Forms.TrackBar()
        Me.lblEpFanartQ = New System.Windows.Forms.Label()
        Me.lblEpFanartWidth = New System.Windows.Forms.Label()
        Me.lblEpFanartHeight = New System.Windows.Forms.Label()
        Me.chkResizeEpFanart = New System.Windows.Forms.CheckBox()
        Me.cbEpFanartSize = New System.Windows.Forms.ComboBox()
        Me.lblEpFanartSize = New System.Windows.Forms.Label()
        Me.chkOverwriteEpFanart = New System.Windows.Forms.CheckBox()
        Me.pnlTVScraper = New System.Windows.Forms.Panel()
        Me.gbTVScraperDuration = New System.Windows.Forms.GroupBox()
        Me.chkUseEPDuration = New System.Windows.Forms.CheckBox()
        Me.txtEPRuntimeFormat = New System.Windows.Forms.TextBox()
        Me.GroupBox32 = New System.Windows.Forms.GroupBox()
        Me.GroupBox35 = New System.Windows.Forms.GroupBox()
        Me.chkScraperShowRating = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowActors = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowStudio = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowPremiered = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowEGU = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowMPAA = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowPlot = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowGenre = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowTitle = New System.Windows.Forms.CheckBox()
        Me.GroupBox34 = New System.Windows.Forms.GroupBox()
        Me.chkScraperEpActors = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpCredits = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpDirector = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpPlot = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpRating = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpAired = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpTitle = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpEpisode = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpSeason = New System.Windows.Forms.CheckBox()
        Me.GroupBox33 = New System.Windows.Forms.GroupBox()
        Me.gbEpLocks = New System.Windows.Forms.GroupBox()
        Me.chkEpLockTitle = New System.Windows.Forms.CheckBox()
        Me.chkEpLockRating = New System.Windows.Forms.CheckBox()
        Me.chkEpLockPlot = New System.Windows.Forms.CheckBox()
        Me.gbShowLocks = New System.Windows.Forms.GroupBox()
        Me.chkShowLockPlot = New System.Windows.Forms.CheckBox()
        Me.chkShowLockGenre = New System.Windows.Forms.CheckBox()
        Me.chkShowLockStudio = New System.Windows.Forms.CheckBox()
        Me.chkShowLockRating = New System.Windows.Forms.CheckBox()
        Me.chkShowLockTitle = New System.Windows.Forms.CheckBox()
        Me.GroupBox31 = New System.Windows.Forms.GroupBox()
        Me.gbTVMIDefaults = New System.Windows.Forms.GroupBox()
        Me.lstTVMetaData = New System.Windows.Forms.ListBox()
        Me.txtTVDefFIExt = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.btnRemoveTVMetaDataFT = New System.Windows.Forms.Button()
        Me.btnEditTVMetaDataFT = New System.Windows.Forms.Button()
        Me.btnNewTVMetaDataFT = New System.Windows.Forms.Button()
        Me.cboTVMetaDataOverlay = New System.Windows.Forms.ComboBox()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.chkTVScanMetaData = New System.Windows.Forms.CheckBox()
        Me.gbTVScraperOptions = New System.Windows.Forms.GroupBox()
        Me.lblOrdering = New System.Windows.Forms.Label()
        Me.cbOrdering = New System.Windows.Forms.ComboBox()
        Me.lblTVUpdate = New System.Windows.Forms.Label()
        Me.cboTVUpdate = New System.Windows.Forms.ComboBox()
        Me.gbOptions = New System.Windows.Forms.GroupBox()
        Me.chkCertification = New System.Windows.Forms.CheckBox()
        Me.chkTop250 = New System.Windows.Forms.CheckBox()
        Me.chkCountry = New System.Windows.Forms.CheckBox()
        Me.txtGenreLimit = New System.Windows.Forms.TextBox()
        Me.lblLimit2 = New System.Windows.Forms.Label()
        Me.txtActorLimit = New System.Windows.Forms.TextBox()
        Me.lblLimit = New System.Windows.Forms.Label()
        Me.chkCrew = New System.Windows.Forms.CheckBox()
        Me.chkMusicBy = New System.Windows.Forms.CheckBox()
        Me.chkProducers = New System.Windows.Forms.CheckBox()
        Me.chkWriters = New System.Windows.Forms.CheckBox()
        Me.chkStudio = New System.Windows.Forms.CheckBox()
        Me.chkRuntime = New System.Windows.Forms.CheckBox()
        Me.chkPlot = New System.Windows.Forms.CheckBox()
        Me.chkOutline = New System.Windows.Forms.CheckBox()
        Me.chkGenre = New System.Windows.Forms.CheckBox()
        Me.chkDirector = New System.Windows.Forms.CheckBox()
        Me.chkTagline = New System.Windows.Forms.CheckBox()
        Me.chkCast = New System.Windows.Forms.CheckBox()
        Me.chkVotes = New System.Windows.Forms.CheckBox()
        Me.chkTrailer = New System.Windows.Forms.CheckBox()
        Me.chkRating = New System.Windows.Forms.CheckBox()
        Me.chkRelease = New System.Windows.Forms.CheckBox()
        Me.chkMPAA = New System.Windows.Forms.CheckBox()
        Me.chkYear = New System.Windows.Forms.CheckBox()
        Me.chkTitle = New System.Windows.Forms.CheckBox()
        Me.lblLimit3 = New System.Windows.Forms.Label()
        Me.txtOutlineLimit = New System.Windows.Forms.TextBox()
        Me.GroupBox26 = New System.Windows.Forms.GroupBox()
        Me.GroupBox28 = New System.Windows.Forms.GroupBox()
        Me.lstMetaData = New System.Windows.Forms.ListBox()
        Me.txtDefFIExt = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.btnRemoveMetaDataFT = New System.Windows.Forms.Button()
        Me.btnEditMetaDataFT = New System.Windows.Forms.Button()
        Me.btnNewMetaDataFT = New System.Windows.Forms.Button()
        Me.chkIFOScan = New System.Windows.Forms.CheckBox()
        Me.cbLanguages = New System.Windows.Forms.ComboBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.gbRTFormat = New System.Windows.Forms.GroupBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.txtRuntimeFormat = New System.Windows.Forms.TextBox()
        Me.chkUseMIDuration = New System.Windows.Forms.CheckBox()
        Me.chkScanMediaInfo = New System.Windows.Forms.CheckBox()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.chkLockMPAA = New System.Windows.Forms.CheckBox()
        Me.chkLockLanguageA = New System.Windows.Forms.CheckBox()
        Me.chkLockLanguageV = New System.Windows.Forms.CheckBox()
        Me.chkLockOutline = New System.Windows.Forms.CheckBox()
        Me.chkLockPlot = New System.Windows.Forms.CheckBox()
        Me.chkLockTrailer = New System.Windows.Forms.CheckBox()
        Me.chkLockGenre = New System.Windows.Forms.CheckBox()
        Me.chkLockRealStudio = New System.Windows.Forms.CheckBox()
        Me.chkLockRating = New System.Windows.Forms.CheckBox()
        Me.chkLockTagline = New System.Windows.Forms.CheckBox()
        Me.chkLockTitle = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkOutlinePlotEnglishOverwrite = New System.Windows.Forms.CheckBox()
        Me.chkTitleFallback = New System.Windows.Forms.CheckBox()
        Me.chkPlotForOutline = New System.Windows.Forms.CheckBox()
        Me.chkUseMPAAFSK = New System.Windows.Forms.CheckBox()
        Me.chkFullCast = New System.Windows.Forms.CheckBox()
        Me.chkForceTitle = New System.Windows.Forms.CheckBox()
        Me.cbForce = New System.Windows.Forms.ComboBox()
        Me.chkOnlyValueForCert = New System.Windows.Forms.CheckBox()
        Me.chkOutlineForPlot = New System.Windows.Forms.CheckBox()
        Me.chkCastWithImg = New System.Windows.Forms.CheckBox()
        Me.chkUseCertForMPAA = New System.Windows.Forms.CheckBox()
        Me.chkFullCrew = New System.Windows.Forms.CheckBox()
        Me.cbCert = New System.Windows.Forms.ComboBox()
        Me.chkCert = New System.Windows.Forms.CheckBox()
        Me.pnlMovieScraper = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.gbHelp = New System.Windows.Forms.GroupBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.lblHelp = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnlMovieTrailers = New System.Windows.Forms.Panel()
        Me.GroupBox15 = New System.Windows.Forms.GroupBox()
        Me.cbTrailerQuality = New System.Windows.Forms.ComboBox()
        Me.lblPreferredQuality = New System.Windows.Forms.Label()
        Me.chkDeleteAllTrailers = New System.Windows.Forms.CheckBox()
        Me.chkOverwriteTrailer = New System.Windows.Forms.CheckBox()
        Me.chkDownloadTrailer = New System.Windows.Forms.CheckBox()
        Me.fileBrowse = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox4.SuspendLayout
        Me.gb_DAEMON.SuspendLayout
        Me.GroupBox29.SuspendLayout
        Me.GroupBox3.SuspendLayout
        Me.tcCleaner.SuspendLayout
        Me.tpStandard.SuspendLayout
        Me.tpExpert.SuspendLayout
        Me.GroupBox12.SuspendLayout
        Me.pnlMovieImages.SuspendLayout
        Me.gbMovieImagesEFanarts.SuspendLayout
        CType(Me.tbMovieEFanartsQual,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbMovieImagesEThumbs.SuspendLayout
        CType(Me.tbMovieEThumbsQual,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox9.SuspendLayout
        Me.gbMovieImagesFanart.SuspendLayout
        CType(Me.tbMovieFanartQual,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbMovieImagesPoster.SuspendLayout
        CType(Me.tbMoviePosterQual,System.ComponentModel.ISupportInitialize).BeginInit
        Me.GroupBox2.SuspendLayout
        Me.GroupBox25.SuspendLayout
        Me.pnlTop.SuspendLayout
        CType(Me.pbTopLogo,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlGeneral.SuspendLayout
        Me.gbInterface.SuspendLayout
        Me.pnlMovieGeneral.SuspendLayout
        Me.GroupBox30.SuspendLayout
        Me.gbGenreFilter.SuspendLayout
        Me.gbFilters.SuspendLayout
        Me.GroupBox27.SuspendLayout
        Me.pnlExtensions.SuspendLayout
        Me.GroupBox22.SuspendLayout
        Me.GroupBox18.SuspendLayout
        Me.pnlProxy.SuspendLayout
        Me.gbProxy.SuspendLayout
        Me.gbCreds.SuspendLayout
        Me.gbMovieBackdropsFolder.SuspendLayout
        Me.Panel1.SuspendLayout
        CType(Me.pbCurrent,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlMovieSources.SuspendLayout
        Me.gbMovieSetsFolder.SuspendLayout
        Me.gbMovieFileNaming.SuspendLayout
        Me.tbcMovieFileNaming.SuspendLayout
        Me.tpMovieFileNamingXBMC.SuspendLayout
        Me.gbMovieXBMCOptional.SuspendLayout
        Me.gbMovieEden.SuspendLayout
        Me.gbMovieFrodo.SuspendLayout
        Me.tpMovieFileNamingNMT.SuspendLayout
        Me.gbMovieNMTOptional.SuspendLayout
        Me.gbMovieNMJ.SuspendLayout
        Me.gbMovieYAMJ.SuspendLayout
        Me.tpMovieFileNamingExpert.SuspendLayout
        Me.gbMovieExpert.SuspendLayout
        Me.tbcMovieFileNamingExpert.SuspendLayout
        Me.tpMovieFileNamingExpertSingle.SuspendLayout
        Me.gbMovieExpertSingleOptionalSettings.SuspendLayout
        Me.gbMovieExpertSingleOptionalImages.SuspendLayout
        Me.tpMovieFileNamingExpertMulti.SuspendLayout
        Me.gbMovieExpertMultiOptionalImages.SuspendLayout
        Me.gbMovieExpertMultiOptionalSettings.SuspendLayout
        Me.tbMovieFileNamingExpertVTS.SuspendLayout
        Me.gbMovieExpertVTSOptionalSettings.SuspendLayout
        Me.gbMovieExpertVTSOptionalImages.SuspendLayout
        Me.tbMovieFileNamingExpertBDMV.SuspendLayout
        Me.gbMovieExpertBDMVOptionalSettings.SuspendLayout
        Me.gbMovieExpertBDMVOptionalImages.SuspendLayout
        Me.gbMovieMiscOptions.SuspendLayout
        Me.pnlTVGeneral.SuspendLayout
        Me.gbTVListOptions.SuspendLayout
        Me.gbEpisodeListOptions.SuspendLayout
        Me.gbSeasonListOptions.SuspendLayout
        Me.gbShowListOptions.SuspendLayout
        Me.gbEpFilter.SuspendLayout
        Me.gbTVMisc.SuspendLayout
        Me.gbShowFilter.SuspendLayout
        Me.pnlTVSources.SuspendLayout
        Me.TabControl2.SuspendLayout
        Me.TabPage3.SuspendLayout
        Me.gbTVFileNaming.SuspendLayout
        Me.tbcTVFileNaming.SuspendLayout
        Me.tpTVFileNamingXBMC.SuspendLayout
        Me.gbTVXBMCAdditional.SuspendLayout
        Me.gbTVFrodo.SuspendLayout
        Me.gbMiscTVSourceOpts.SuspendLayout
        Me.TabPage4.SuspendLayout
        Me.gbShowRegex.SuspendLayout
        Me.pnlTVImages.SuspendLayout
        Me.TabControl3.SuspendLayout
        Me.TabPage5.SuspendLayout
        Me.gbAllSPosterOpts.SuspendLayout
        CType(Me.tbAllSPosterQual,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbShowPosterOpts.SuspendLayout
        CType(Me.tbShowPosterQual,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbShowFanartOpts.SuspendLayout
        CType(Me.tbShowFanartQual,System.ComponentModel.ISupportInitialize).BeginInit
        Me.TabPage6.SuspendLayout
        Me.gbSeaPosterOpts.SuspendLayout
        CType(Me.tbSeaPosterQual,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbSeaFanartOpts.SuspendLayout
        CType(Me.tbSeaFanartQual,System.ComponentModel.ISupportInitialize).BeginInit
        Me.TabPage7.SuspendLayout
        Me.gbEpPosterOpts.SuspendLayout
        CType(Me.tbEpPosterQual,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbEpFanartOpts.SuspendLayout
        CType(Me.tbEpFanartQual,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlTVScraper.SuspendLayout
        Me.gbTVScraperDuration.SuspendLayout
        Me.GroupBox32.SuspendLayout
        Me.GroupBox35.SuspendLayout
        Me.GroupBox34.SuspendLayout
        Me.GroupBox33.SuspendLayout
        Me.gbEpLocks.SuspendLayout
        Me.gbShowLocks.SuspendLayout
        Me.GroupBox31.SuspendLayout
        Me.gbTVMIDefaults.SuspendLayout
        Me.gbTVScraperOptions.SuspendLayout
        Me.gbOptions.SuspendLayout
        Me.GroupBox26.SuspendLayout
        Me.GroupBox28.SuspendLayout
        Me.gbRTFormat.SuspendLayout
        Me.GroupBox10.SuspendLayout
        Me.GroupBox1.SuspendLayout
        Me.pnlMovieScraper.SuspendLayout
        Me.gbHelp.SuspendLayout
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel2.SuspendLayout
        Me.pnlMovieTrailers.SuspendLayout
        Me.GroupBox15.SuspendLayout
        Me.SuspendLayout
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.gb_DAEMON)
        Me.GroupBox4.Controls.Add(Me.chkImagesGlassOverlay)
        Me.GroupBox4.Controls.Add(Me.chkNoDisplayFanartSmall)
        Me.GroupBox4.Controls.Add(Me.chkSpecialDateAdd)
        Me.GroupBox4.Controls.Add(Me.chkShowGenresText)
        Me.GroupBox4.Controls.Add(Me.chkSourceFromFolder)
        Me.GroupBox4.Controls.Add(Me.chkNoDisplayFanart)
        Me.GroupBox4.Controls.Add(Me.chkInfoPanelAnim)
        Me.GroupBox4.Controls.Add(Me.chkNoDisplayPoster)
        Me.GroupBox4.Controls.Add(Me.chkShowDims)
        Me.GroupBox4.Controls.Add(Me.chkUpdates)
        Me.GroupBox4.Controls.Add(Me.chkOverwriteNfo)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(210, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(537, 494)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = false
        Me.GroupBox4.Text = "Miscellaneous"
        '
        'gb_DAEMON
        '
        Me.gb_DAEMON.Controls.Add(Me.lbl_DAEMON_driveletter)
        Me.gb_DAEMON.Controls.Add(Me.cbo_DAEMON_driveletter)
        Me.gb_DAEMON.Controls.Add(Me.bt_DAEMON_Programpath)
        Me.gb_DAEMON.Controls.Add(Me.txt_DAEMON_Programpath)
        Me.gb_DAEMON.Controls.Add(Me.lbl_DAEMON_Programpath)
        Me.gb_DAEMON.Location = New System.Drawing.Point(6, 256)
        Me.gb_DAEMON.Name = "gb_DAEMON"
        Me.gb_DAEMON.Size = New System.Drawing.Size(396, 122)
        Me.gb_DAEMON.TabIndex = 13
        Me.gb_DAEMON.TabStop = false
        Me.gb_DAEMON.Text = "DAEMON Tools"
        '
        'lbl_DAEMON_driveletter
        '
        Me.lbl_DAEMON_driveletter.AutoSize = true
        Me.lbl_DAEMON_driveletter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lbl_DAEMON_driveletter.Location = New System.Drawing.Point(7, 29)
        Me.lbl_DAEMON_driveletter.Name = "lbl_DAEMON_driveletter"
        Me.lbl_DAEMON_driveletter.Size = New System.Drawing.Size(63, 13)
        Me.lbl_DAEMON_driveletter.TabIndex = 6
        Me.lbl_DAEMON_driveletter.Text = "Driveletter:"
        '
        'cbo_DAEMON_driveletter
        '
        Me.cbo_DAEMON_driveletter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_DAEMON_driveletter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbo_DAEMON_driveletter.FormattingEnabled = true
        Me.cbo_DAEMON_driveletter.Items.AddRange(New Object() {"", "A:\", "B:\", "D:\", "E:\", "F:\", "G:\", "H:\", "I:\", "J:\", "K:\", "L:\", "M:\", "N:\", "O:\", "P:\", "Q:\", "R:\", "S:\", "T:\", "U:\", "V:\", "W:\", "X:\", "Y:\", "Z:\"})
        Me.cbo_DAEMON_driveletter.Location = New System.Drawing.Point(77, 30)
        Me.cbo_DAEMON_driveletter.Name = "cbo_DAEMON_driveletter"
        Me.cbo_DAEMON_driveletter.Size = New System.Drawing.Size(48, 21)
        Me.cbo_DAEMON_driveletter.TabIndex = 7
        '
        'bt_DAEMON_Programpath
        '
        Me.bt_DAEMON_Programpath.Location = New System.Drawing.Point(259, 82)
        Me.bt_DAEMON_Programpath.Name = "bt_DAEMON_Programpath"
        Me.bt_DAEMON_Programpath.Size = New System.Drawing.Size(25, 23)
        Me.bt_DAEMON_Programpath.TabIndex = 4
        Me.bt_DAEMON_Programpath.Text = "..."
        Me.bt_DAEMON_Programpath.UseVisualStyleBackColor = true
        '
        'txt_DAEMON_Programpath
        '
        Me.txt_DAEMON_Programpath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_DAEMON_Programpath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txt_DAEMON_Programpath.Location = New System.Drawing.Point(9, 82)
        Me.txt_DAEMON_Programpath.Name = "txt_DAEMON_Programpath"
        Me.txt_DAEMON_Programpath.Size = New System.Drawing.Size(241, 22)
        Me.txt_DAEMON_Programpath.TabIndex = 3
        '
        'lbl_DAEMON_Programpath
        '
        Me.lbl_DAEMON_Programpath.AutoSize = true
        Me.lbl_DAEMON_Programpath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lbl_DAEMON_Programpath.Location = New System.Drawing.Point(6, 68)
        Me.lbl_DAEMON_Programpath.Name = "lbl_DAEMON_Programpath"
        Me.lbl_DAEMON_Programpath.Size = New System.Drawing.Size(136, 13)
        Me.lbl_DAEMON_Programpath.TabIndex = 2
        Me.lbl_DAEMON_Programpath.Text = ".EXE path (i.e. DTLite.exe):"
        '
        'chkImagesGlassOverlay
        '
        Me.chkImagesGlassOverlay.AutoSize = true
        Me.chkImagesGlassOverlay.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkImagesGlassOverlay.Location = New System.Drawing.Point(10, 233)
        Me.chkImagesGlassOverlay.Name = "chkImagesGlassOverlay"
        Me.chkImagesGlassOverlay.Size = New System.Drawing.Size(171, 17)
        Me.chkImagesGlassOverlay.TabIndex = 12
        Me.chkImagesGlassOverlay.Text = "Enable Images Glass Overlay"
        Me.chkImagesGlassOverlay.UseVisualStyleBackColor = true
        '
        'chkNoDisplayFanartSmall
        '
        Me.chkNoDisplayFanartSmall.AutoSize = true
        Me.chkNoDisplayFanartSmall.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkNoDisplayFanartSmall.Location = New System.Drawing.Point(10, 210)
        Me.chkNoDisplayFanartSmall.Name = "chkNoDisplayFanartSmall"
        Me.chkNoDisplayFanartSmall.Size = New System.Drawing.Size(169, 17)
        Me.chkNoDisplayFanartSmall.TabIndex = 11
        Me.chkNoDisplayFanartSmall.Text = "Do Not Display Small Fanart"
        Me.chkNoDisplayFanartSmall.UseVisualStyleBackColor = true
        '
        'chkSpecialDateAdd
        '
        Me.chkSpecialDateAdd.AutoSize = true
        Me.chkSpecialDateAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkSpecialDateAdd.Location = New System.Drawing.Point(10, 195)
        Me.chkSpecialDateAdd.Name = "chkSpecialDateAdd"
        Me.chkSpecialDateAdd.Size = New System.Drawing.Size(196, 17)
        Me.chkSpecialDateAdd.TabIndex = 10
        Me.chkSpecialDateAdd.Text = "Use file creation date of videofile"
        Me.chkSpecialDateAdd.UseVisualStyleBackColor = true
        '
        'chkShowGenresText
        '
        Me.chkShowGenresText.AutoSize = true
        Me.chkShowGenresText.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkShowGenresText.Location = New System.Drawing.Point(10, 178)
        Me.chkShowGenresText.Name = "chkShowGenresText"
        Me.chkShowGenresText.Size = New System.Drawing.Size(166, 17)
        Me.chkShowGenresText.TabIndex = 9
        Me.chkShowGenresText.Text = "Allways Display Genres Text"
        Me.chkShowGenresText.UseVisualStyleBackColor = true
        '
        'chkSourceFromFolder
        '
        Me.chkSourceFromFolder.AutoSize = true
        Me.chkSourceFromFolder.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkSourceFromFolder.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkSourceFromFolder.Location = New System.Drawing.Point(10, 110)
        Me.chkSourceFromFolder.Name = "chkSourceFromFolder"
        Me.chkSourceFromFolder.Size = New System.Drawing.Size(243, 17)
        Me.chkSourceFromFolder.TabIndex = 5
        Me.chkSourceFromFolder.Text = "Include Folder Name in Source Type Check"
        Me.chkSourceFromFolder.UseVisualStyleBackColor = true
        '
        'chkNoDisplayFanart
        '
        Me.chkNoDisplayFanart.AutoSize = true
        Me.chkNoDisplayFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkNoDisplayFanart.Location = New System.Drawing.Point(10, 144)
        Me.chkNoDisplayFanart.Name = "chkNoDisplayFanart"
        Me.chkNoDisplayFanart.Size = New System.Drawing.Size(139, 17)
        Me.chkNoDisplayFanart.TabIndex = 7
        Me.chkNoDisplayFanart.Text = "Do Not Display Fanart"
        Me.chkNoDisplayFanart.UseVisualStyleBackColor = true
        '
        'chkInfoPanelAnim
        '
        Me.chkInfoPanelAnim.AutoSize = true
        Me.chkInfoPanelAnim.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkInfoPanelAnim.Location = New System.Drawing.Point(10, 94)
        Me.chkInfoPanelAnim.Name = "chkInfoPanelAnim"
        Me.chkInfoPanelAnim.Size = New System.Drawing.Size(148, 17)
        Me.chkInfoPanelAnim.TabIndex = 4
        Me.chkInfoPanelAnim.Text = "Enable Panel Animation"
        Me.chkInfoPanelAnim.UseVisualStyleBackColor = true
        '
        'chkNoDisplayPoster
        '
        Me.chkNoDisplayPoster.AutoSize = true
        Me.chkNoDisplayPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkNoDisplayPoster.Location = New System.Drawing.Point(10, 127)
        Me.chkNoDisplayPoster.Name = "chkNoDisplayPoster"
        Me.chkNoDisplayPoster.Size = New System.Drawing.Size(138, 17)
        Me.chkNoDisplayPoster.TabIndex = 6
        Me.chkNoDisplayPoster.Text = "Do Not Display Poster"
        Me.chkNoDisplayPoster.UseVisualStyleBackColor = true
        '
        'chkShowDims
        '
        Me.chkShowDims.AutoSize = true
        Me.chkShowDims.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkShowDims.Location = New System.Drawing.Point(10, 161)
        Me.chkShowDims.Name = "chkShowDims"
        Me.chkShowDims.Size = New System.Drawing.Size(160, 17)
        Me.chkShowDims.TabIndex = 8
        Me.chkShowDims.Text = "Display Image Dimensions"
        Me.chkShowDims.UseVisualStyleBackColor = true
        '
        'chkUpdates
        '
        Me.chkUpdates.AutoSize = true
        Me.chkUpdates.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkUpdates.Location = New System.Drawing.Point(10, 20)
        Me.chkUpdates.Name = "chkUpdates"
        Me.chkUpdates.Size = New System.Drawing.Size(121, 17)
        Me.chkUpdates.TabIndex = 0
        Me.chkUpdates.Text = "Check for Updates"
        Me.chkUpdates.UseVisualStyleBackColor = true
        '
        'chkOverwriteNfo
        '
        Me.chkOverwriteNfo.AutoSize = true
        Me.chkOverwriteNfo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkOverwriteNfo.Location = New System.Drawing.Point(10, 54)
        Me.chkOverwriteNfo.Name = "chkOverwriteNfo"
        Me.chkOverwriteNfo.Size = New System.Drawing.Size(191, 17)
        Me.chkOverwriteNfo.TabIndex = 2
        Me.chkOverwriteNfo.Text = "Overwrite Non-conforming nfos"
        Me.chkOverwriteNfo.UseVisualStyleBackColor = true
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label5.Location = New System.Drawing.Point(20, 68)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(165, 24)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "(If unchecked, non-conforming nfos will be renamed to <filename>.info)"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox29
        '
        Me.GroupBox29.Controls.Add(Me.cbEpTheme)
        Me.GroupBox29.Controls.Add(Me.Label3)
        Me.GroupBox29.Controls.Add(Me.cbTVShowTheme)
        Me.GroupBox29.Controls.Add(Me.Label1)
        Me.GroupBox29.Controls.Add(Me.cbMovieTheme)
        Me.GroupBox29.Controls.Add(Me.Label35)
        Me.GroupBox29.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.GroupBox29.Location = New System.Drawing.Point(8, 61)
        Me.GroupBox29.Name = "GroupBox29"
        Me.GroupBox29.Size = New System.Drawing.Size(179, 166)
        Me.GroupBox29.TabIndex = 2
        Me.GroupBox29.TabStop = false
        Me.GroupBox29.Text = "Themes"
        '
        'cbEpTheme
        '
        Me.cbEpTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEpTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbEpTheme.FormattingEnabled = true
        Me.cbEpTheme.Location = New System.Drawing.Point(9, 129)
        Me.cbEpTheme.Name = "cbEpTheme"
        Me.cbEpTheme.Size = New System.Drawing.Size(162, 21)
        Me.cbEpTheme.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Episode Theme:"
        '
        'cbTVShowTheme
        '
        Me.cbTVShowTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVShowTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbTVShowTheme.FormattingEnabled = true
        Me.cbTVShowTheme.Location = New System.Drawing.Point(9, 83)
        Me.cbTVShowTheme.Name = "cbTVShowTheme"
        Me.cbTVShowTheme.Size = New System.Drawing.Size(162, 21)
        Me.cbTVShowTheme.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "TV Show Theme:"
        '
        'cbMovieTheme
        '
        Me.cbMovieTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieTheme.FormattingEnabled = true
        Me.cbMovieTheme.Location = New System.Drawing.Point(9, 38)
        Me.cbMovieTheme.Name = "cbMovieTheme"
        Me.cbMovieTheme.Size = New System.Drawing.Size(162, 21)
        Me.cbMovieTheme.TabIndex = 1
        '
        'Label35
        '
        Me.Label35.AutoSize = true
        Me.Label35.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label35.Location = New System.Drawing.Point(7, 23)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(77, 13)
        Me.Label35.TabIndex = 0
        Me.Label35.Text = "Movie Theme:"
        '
        'Label32
        '
        Me.Label32.AutoSize = true
        Me.Label32.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label32.Location = New System.Drawing.Point(6, 18)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(109, 13)
        Me.Label32.TabIndex = 0
        Me.Label32.Text = "Interface Language:"
        '
        'cbIntLang
        '
        Me.cbIntLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbIntLang.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbIntLang.FormattingEnabled = true
        Me.cbIntLang.Location = New System.Drawing.Point(9, 34)
        Me.cbIntLang.Name = "cbIntLang"
        Me.cbIntLang.Size = New System.Drawing.Size(137, 21)
        Me.cbIntLang.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.tcCleaner)
        Me.GroupBox3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(401, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(208, 385)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = false
        Me.GroupBox3.Text = "Clean Files"
        '
        'tcCleaner
        '
        Me.tcCleaner.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tcCleaner.Controls.Add(Me.tpStandard)
        Me.tcCleaner.Controls.Add(Me.tpExpert)
        Me.tcCleaner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.tcCleaner.Location = New System.Drawing.Point(6, 19)
        Me.tcCleaner.Name = "tcCleaner"
        Me.tcCleaner.SelectedIndex = 0
        Me.tcCleaner.Size = New System.Drawing.Size(196, 363)
        Me.tcCleaner.TabIndex = 0
        '
        'tpStandard
        '
        Me.tpStandard.BackColor = System.Drawing.Color.White
        Me.tpStandard.Controls.Add(Me.chkCleanFolderJPG)
        Me.tpStandard.Controls.Add(Me.chkCleanExtrathumbs)
        Me.tpStandard.Controls.Add(Me.chkCleanMovieTBN)
        Me.tpStandard.Controls.Add(Me.chkCleanMovieNameJPG)
        Me.tpStandard.Controls.Add(Me.chkCleanMovieTBNb)
        Me.tpStandard.Controls.Add(Me.chkCleanMovieJPG)
        Me.tpStandard.Controls.Add(Me.chkCleanFanartJPG)
        Me.tpStandard.Controls.Add(Me.chkCleanPosterJPG)
        Me.tpStandard.Controls.Add(Me.chkCleanMovieFanartJPG)
        Me.tpStandard.Controls.Add(Me.chkCleanPosterTBN)
        Me.tpStandard.Controls.Add(Me.chkCleanMovieNFO)
        Me.tpStandard.Controls.Add(Me.chkCleanDotFanartJPG)
        Me.tpStandard.Controls.Add(Me.chkCleanMovieNFOb)
        Me.tpStandard.Location = New System.Drawing.Point(4, 25)
        Me.tpStandard.Name = "tpStandard"
        Me.tpStandard.Padding = New System.Windows.Forms.Padding(3)
        Me.tpStandard.Size = New System.Drawing.Size(188, 334)
        Me.tpStandard.TabIndex = 0
        Me.tpStandard.Text = "Standard"
        Me.tpStandard.UseVisualStyleBackColor = true
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
        'tpExpert
        '
        Me.tpExpert.BackColor = System.Drawing.Color.White
        Me.tpExpert.Controls.Add(Me.chkWhitelistVideo)
        Me.tpExpert.Controls.Add(Me.Label27)
        Me.tpExpert.Controls.Add(Me.btnRemoveWhitelist)
        Me.tpExpert.Controls.Add(Me.btnAddWhitelist)
        Me.tpExpert.Controls.Add(Me.txtWhitelist)
        Me.tpExpert.Controls.Add(Me.lstWhitelist)
        Me.tpExpert.Controls.Add(Me.Label25)
        Me.tpExpert.Location = New System.Drawing.Point(4, 25)
        Me.tpExpert.Name = "tpExpert"
        Me.tpExpert.Padding = New System.Windows.Forms.Padding(3)
        Me.tpExpert.Size = New System.Drawing.Size(188, 334)
        Me.tpExpert.TabIndex = 1
        Me.tpExpert.Text = "Expert"
        Me.tpExpert.UseVisualStyleBackColor = true
        '
        'chkWhitelistVideo
        '
        Me.chkWhitelistVideo.AutoSize = true
        Me.chkWhitelistVideo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkWhitelistVideo.Location = New System.Drawing.Point(4, 85)
        Me.chkWhitelistVideo.Name = "chkWhitelistVideo"
        Me.chkWhitelistVideo.Size = New System.Drawing.Size(163, 17)
        Me.chkWhitelistVideo.TabIndex = 1
        Me.chkWhitelistVideo.Text = "Whitelist Video Extensions"
        Me.chkWhitelistVideo.UseVisualStyleBackColor = true
        '
        'Label27
        '
        Me.Label27.AutoSize = true
        Me.Label27.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label27.Location = New System.Drawing.Point(19, 107)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(127, 13)
        Me.Label27.TabIndex = 2
        Me.Label27.Text = "Whitelisted Extensions:"
        '
        'btnRemoveWhitelist
        '
        Me.btnRemoveWhitelist.Image = CType(resources.GetObject("btnRemoveWhitelist.Image"),System.Drawing.Image)
        Me.btnRemoveWhitelist.Location = New System.Drawing.Point(134, 251)
        Me.btnRemoveWhitelist.Name = "btnRemoveWhitelist"
        Me.btnRemoveWhitelist.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveWhitelist.TabIndex = 6
        Me.btnRemoveWhitelist.UseVisualStyleBackColor = true
        '
        'btnAddWhitelist
        '
        Me.btnAddWhitelist.Image = CType(resources.GetObject("btnAddWhitelist.Image"),System.Drawing.Image)
        Me.btnAddWhitelist.Location = New System.Drawing.Point(82, 251)
        Me.btnAddWhitelist.Name = "btnAddWhitelist"
        Me.btnAddWhitelist.Size = New System.Drawing.Size(23, 23)
        Me.btnAddWhitelist.TabIndex = 5
        Me.btnAddWhitelist.UseVisualStyleBackColor = true
        '
        'txtWhitelist
        '
        Me.txtWhitelist.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtWhitelist.Location = New System.Drawing.Point(20, 252)
        Me.txtWhitelist.Name = "txtWhitelist"
        Me.txtWhitelist.Size = New System.Drawing.Size(61, 22)
        Me.txtWhitelist.TabIndex = 4
        '
        'lstWhitelist
        '
        Me.lstWhitelist.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstWhitelist.FormattingEnabled = true
        Me.lstWhitelist.Location = New System.Drawing.Point(19, 126)
        Me.lstWhitelist.Name = "lstWhitelist"
        Me.lstWhitelist.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstWhitelist.Size = New System.Drawing.Size(138, 108)
        Me.lstWhitelist.TabIndex = 3
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Red
        Me.Label25.Location = New System.Drawing.Point(12, 10)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(155, 68)
        Me.Label25.TabIndex = 0
        Me.Label25.Text = "WARNING: Using the Expert Mode Cleaner could potentially delete wanted files. Tak"& _ 
    "e care when using this tool."
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.chkClickScrape)
        Me.GroupBox12.Controls.Add(Me.chkAskCheckboxScrape)
        Me.GroupBox12.Controls.Add(Me.chkMarkNew)
        Me.GroupBox12.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.GroupBox12.Location = New System.Drawing.Point(6, 7)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(219, 93)
        Me.GroupBox12.TabIndex = 1
        Me.GroupBox12.TabStop = false
        Me.GroupBox12.Text = "Miscellaneous"
        '
        'chkClickScrape
        '
        Me.chkClickScrape.AutoSize = true
        Me.chkClickScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkClickScrape.Location = New System.Drawing.Point(12, 36)
        Me.chkClickScrape.Name = "chkClickScrape"
        Me.chkClickScrape.Size = New System.Drawing.Size(125, 17)
        Me.chkClickScrape.TabIndex = 65
        Me.chkClickScrape.Text = "Enable Click Scrape"
        Me.chkClickScrape.UseVisualStyleBackColor = true
        '
        'chkAskCheckboxScrape
        '
        Me.chkAskCheckboxScrape.AutoSize = true
        Me.chkAskCheckboxScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.chkAskCheckboxScrape.Location = New System.Drawing.Point(25, 56)
        Me.chkAskCheckboxScrape.Name = "chkAskCheckboxScrape"
        Me.chkAskCheckboxScrape.Size = New System.Drawing.Size(127, 17)
        Me.chkAskCheckboxScrape.TabIndex = 64
        Me.chkAskCheckboxScrape.Text = "Ask On Click Scrape"
        Me.chkAskCheckboxScrape.UseVisualStyleBackColor = true
        '
        'chkMarkNew
        '
        Me.chkMarkNew.AutoSize = true
        Me.chkMarkNew.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMarkNew.Location = New System.Drawing.Point(12, 14)
        Me.chkMarkNew.Name = "chkMarkNew"
        Me.chkMarkNew.Size = New System.Drawing.Size(117, 17)
        Me.chkMarkNew.TabIndex = 0
        Me.chkMarkNew.Text = "Mark New Movies"
        Me.chkMarkNew.UseVisualStyleBackColor = true
        '
        'pnlMovieImages
        '
        Me.pnlMovieImages.BackColor = System.Drawing.Color.White
        Me.pnlMovieImages.Controls.Add(Me.gbMovieImagesEFanarts)
        Me.pnlMovieImages.Controls.Add(Me.gbMovieImagesEThumbs)
        Me.pnlMovieImages.Controls.Add(Me.GroupBox9)
        Me.pnlMovieImages.Controls.Add(Me.gbMovieImagesFanart)
        Me.pnlMovieImages.Controls.Add(Me.gbMovieImagesPoster)
        Me.pnlMovieImages.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlMovieImages.Location = New System.Drawing.Point(1250, 470)
        Me.pnlMovieImages.Name = "pnlMovieImages"
        Me.pnlMovieImages.Size = New System.Drawing.Size(750, 500)
        Me.pnlMovieImages.TabIndex = 12
        Me.pnlMovieImages.Visible = false
        '
        'gbMovieImagesEFanarts
        '
        Me.gbMovieImagesEFanarts.Controls.Add(Me.chkMovieEFanartsOnly)
        Me.gbMovieImagesEFanarts.Controls.Add(Me.txtMovieEFanartsWidth)
        Me.gbMovieImagesEFanarts.Controls.Add(Me.txtMovieEFanartsHeight)
        Me.gbMovieImagesEFanarts.Controls.Add(Me.lblMovieEFanartsQual)
        Me.gbMovieImagesEFanarts.Controls.Add(Me.tbMovieEFanartsQual)
        Me.gbMovieImagesEFanarts.Controls.Add(Me.Label22)
        Me.gbMovieImagesEFanarts.Controls.Add(Me.Label23)
        Me.gbMovieImagesEFanarts.Controls.Add(Me.Label28)
        Me.gbMovieImagesEFanarts.Controls.Add(Me.chkMovieResizeEFanarts)
        Me.gbMovieImagesEFanarts.Controls.Add(Me.Label29)
        Me.gbMovieImagesEFanarts.Controls.Add(Me.cbMovieEFanartsSize)
        Me.gbMovieImagesEFanarts.Controls.Add(Me.chkMovieOverwriteEFanarts)
        Me.gbMovieImagesEFanarts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesEFanarts.Location = New System.Drawing.Point(229, 259)
        Me.gbMovieImagesEFanarts.Name = "gbMovieImagesEFanarts"
        Me.gbMovieImagesEFanarts.Size = New System.Drawing.Size(218, 170)
        Me.gbMovieImagesEFanarts.TabIndex = 13
        Me.gbMovieImagesEFanarts.TabStop = false
        Me.gbMovieImagesEFanarts.Text = "Extrafanarts"
        '
        'chkMovieEFanartsOnly
        '
        Me.chkMovieEFanartsOnly.AutoSize = true
        Me.chkMovieEFanartsOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieEFanartsOnly.Location = New System.Drawing.Point(163, 38)
        Me.chkMovieEFanartsOnly.Name = "chkMovieEFanartsOnly"
        Me.chkMovieEFanartsOnly.Size = New System.Drawing.Size(50, 17)
        Me.chkMovieEFanartsOnly.TabIndex = 2
        Me.chkMovieEFanartsOnly.Text = "Only"
        Me.chkMovieEFanartsOnly.UseVisualStyleBackColor = true
        '
        'txtMovieEFanartsWidth
        '
        Me.txtMovieEFanartsWidth.Enabled = false
        Me.txtMovieEFanartsWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieEFanartsWidth.Location = New System.Drawing.Point(68, 100)
        Me.txtMovieEFanartsWidth.Name = "txtMovieEFanartsWidth"
        Me.txtMovieEFanartsWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieEFanartsWidth.TabIndex = 6
        '
        'txtMovieEFanartsHeight
        '
        Me.txtMovieEFanartsHeight.Enabled = false
        Me.txtMovieEFanartsHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieEFanartsHeight.Location = New System.Drawing.Point(175, 100)
        Me.txtMovieEFanartsHeight.Name = "txtMovieEFanartsHeight"
        Me.txtMovieEFanartsHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieEFanartsHeight.TabIndex = 8
        '
        'lblMovieEFanartsQual
        '
        Me.lblMovieEFanartsQual.AutoSize = true
        Me.lblMovieEFanartsQual.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieEFanartsQual.Location = New System.Drawing.Point(183, 147)
        Me.lblMovieEFanartsQual.Name = "lblMovieEFanartsQual"
        Me.lblMovieEFanartsQual.Size = New System.Drawing.Size(29, 17)
        Me.lblMovieEFanartsQual.TabIndex = 11
        Me.lblMovieEFanartsQual.Text = "100"
        '
        'tbMovieEFanartsQual
        '
        Me.tbMovieEFanartsQual.AutoSize = false
        Me.tbMovieEFanartsQual.LargeChange = 10
        Me.tbMovieEFanartsQual.Location = New System.Drawing.Point(7, 139)
        Me.tbMovieEFanartsQual.Maximum = 100
        Me.tbMovieEFanartsQual.Name = "tbMovieEFanartsQual"
        Me.tbMovieEFanartsQual.Size = New System.Drawing.Size(179, 27)
        Me.tbMovieEFanartsQual.TabIndex = 10
        Me.tbMovieEFanartsQual.TickFrequency = 10
        Me.tbMovieEFanartsQual.Value = 100
        '
        'Label22
        '
        Me.Label22.AutoSize = true
        Me.Label22.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label22.Location = New System.Drawing.Point(3, 127)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(46, 13)
        Me.Label22.TabIndex = 9
        Me.Label22.Text = "Quality:"
        '
        'Label23
        '
        Me.Label23.AutoSize = true
        Me.Label23.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label23.Location = New System.Drawing.Point(3, 104)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(66, 13)
        Me.Label23.TabIndex = 5
        Me.Label23.Text = "Max Width:"
        '
        'Label28
        '
        Me.Label28.AutoSize = true
        Me.Label28.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label28.Location = New System.Drawing.Point(106, 104)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(69, 13)
        Me.Label28.TabIndex = 7
        Me.Label28.Text = "Max Height:"
        '
        'chkMovieResizeEFanarts
        '
        Me.chkMovieResizeEFanarts.AutoSize = true
        Me.chkMovieResizeEFanarts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieResizeEFanarts.Location = New System.Drawing.Point(6, 82)
        Me.chkMovieResizeEFanarts.Name = "chkMovieResizeEFanarts"
        Me.chkMovieResizeEFanarts.Size = New System.Drawing.Size(133, 17)
        Me.chkMovieResizeEFanarts.TabIndex = 4
        Me.chkMovieResizeEFanarts.Text = "Automatically Resize:"
        Me.chkMovieResizeEFanarts.UseVisualStyleBackColor = true
        '
        'Label29
        '
        Me.Label29.AutoSize = true
        Me.Label29.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label29.Location = New System.Drawing.Point(4, 18)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(80, 13)
        Me.Label29.TabIndex = 0
        Me.Label29.Text = "Preferred Size:"
        '
        'cbMovieEFanartsSize
        '
        Me.cbMovieEFanartsSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieEFanartsSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieEFanartsSize.FormattingEnabled = true
        Me.cbMovieEFanartsSize.Location = New System.Drawing.Point(6, 34)
        Me.cbMovieEFanartsSize.Name = "cbMovieEFanartsSize"
        Me.cbMovieEFanartsSize.Size = New System.Drawing.Size(148, 21)
        Me.cbMovieEFanartsSize.TabIndex = 1
        '
        'chkMovieOverwriteEFanarts
        '
        Me.chkMovieOverwriteEFanarts.AutoSize = true
        Me.chkMovieOverwriteEFanarts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieOverwriteEFanarts.Location = New System.Drawing.Point(6, 62)
        Me.chkMovieOverwriteEFanarts.Name = "chkMovieOverwriteEFanarts"
        Me.chkMovieOverwriteEFanarts.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieOverwriteEFanarts.TabIndex = 3
        Me.chkMovieOverwriteEFanarts.Text = "Overwrite Existing"
        Me.chkMovieOverwriteEFanarts.UseVisualStyleBackColor = true
        '
        'gbMovieImagesEThumbs
        '
        Me.gbMovieImagesEThumbs.Controls.Add(Me.chkMovieEThumbsOnly)
        Me.gbMovieImagesEThumbs.Controls.Add(Me.txtMovieEThumbsWidth)
        Me.gbMovieImagesEThumbs.Controls.Add(Me.txtMovieEThumbsHeight)
        Me.gbMovieImagesEThumbs.Controls.Add(Me.lblMovieEThumbsQual)
        Me.gbMovieImagesEThumbs.Controls.Add(Me.tbMovieEThumbsQual)
        Me.gbMovieImagesEThumbs.Controls.Add(Me.Label14)
        Me.gbMovieImagesEThumbs.Controls.Add(Me.Label15)
        Me.gbMovieImagesEThumbs.Controls.Add(Me.Label16)
        Me.gbMovieImagesEThumbs.Controls.Add(Me.chkMovieResizeEThumbs)
        Me.gbMovieImagesEThumbs.Controls.Add(Me.Label17)
        Me.gbMovieImagesEThumbs.Controls.Add(Me.cbMovieEThumbsSize)
        Me.gbMovieImagesEThumbs.Controls.Add(Me.chkMovieOverwriteEThumbs)
        Me.gbMovieImagesEThumbs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesEThumbs.Location = New System.Drawing.Point(3, 259)
        Me.gbMovieImagesEThumbs.Name = "gbMovieImagesEThumbs"
        Me.gbMovieImagesEThumbs.Size = New System.Drawing.Size(218, 170)
        Me.gbMovieImagesEThumbs.TabIndex = 12
        Me.gbMovieImagesEThumbs.TabStop = false
        Me.gbMovieImagesEThumbs.Text = "Extrathumbs"
        '
        'chkMovieEThumbsOnly
        '
        Me.chkMovieEThumbsOnly.AutoSize = true
        Me.chkMovieEThumbsOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieEThumbsOnly.Location = New System.Drawing.Point(163, 38)
        Me.chkMovieEThumbsOnly.Name = "chkMovieEThumbsOnly"
        Me.chkMovieEThumbsOnly.Size = New System.Drawing.Size(50, 17)
        Me.chkMovieEThumbsOnly.TabIndex = 2
        Me.chkMovieEThumbsOnly.Text = "Only"
        Me.chkMovieEThumbsOnly.UseVisualStyleBackColor = true
        '
        'txtMovieEThumbsWidth
        '
        Me.txtMovieEThumbsWidth.Enabled = false
        Me.txtMovieEThumbsWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieEThumbsWidth.Location = New System.Drawing.Point(68, 100)
        Me.txtMovieEThumbsWidth.Name = "txtMovieEThumbsWidth"
        Me.txtMovieEThumbsWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieEThumbsWidth.TabIndex = 6
        '
        'txtMovieEThumbsHeight
        '
        Me.txtMovieEThumbsHeight.Enabled = false
        Me.txtMovieEThumbsHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieEThumbsHeight.Location = New System.Drawing.Point(175, 100)
        Me.txtMovieEThumbsHeight.Name = "txtMovieEThumbsHeight"
        Me.txtMovieEThumbsHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieEThumbsHeight.TabIndex = 8
        '
        'lblMovieEThumbsQual
        '
        Me.lblMovieEThumbsQual.AutoSize = true
        Me.lblMovieEThumbsQual.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieEThumbsQual.Location = New System.Drawing.Point(183, 147)
        Me.lblMovieEThumbsQual.Name = "lblMovieEThumbsQual"
        Me.lblMovieEThumbsQual.Size = New System.Drawing.Size(29, 17)
        Me.lblMovieEThumbsQual.TabIndex = 11
        Me.lblMovieEThumbsQual.Text = "100"
        '
        'tbMovieEThumbsQual
        '
        Me.tbMovieEThumbsQual.AutoSize = false
        Me.tbMovieEThumbsQual.LargeChange = 10
        Me.tbMovieEThumbsQual.Location = New System.Drawing.Point(7, 139)
        Me.tbMovieEThumbsQual.Maximum = 100
        Me.tbMovieEThumbsQual.Name = "tbMovieEThumbsQual"
        Me.tbMovieEThumbsQual.Size = New System.Drawing.Size(179, 27)
        Me.tbMovieEThumbsQual.TabIndex = 10
        Me.tbMovieEThumbsQual.TickFrequency = 10
        Me.tbMovieEThumbsQual.Value = 100
        '
        'Label14
        '
        Me.Label14.AutoSize = true
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 127)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(46, 13)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "Quality:"
        '
        'Label15
        '
        Me.Label15.AutoSize = true
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 104)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(66, 13)
        Me.Label15.TabIndex = 5
        Me.Label15.Text = "Max Width:"
        '
        'Label16
        '
        Me.Label16.AutoSize = true
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label16.Location = New System.Drawing.Point(106, 104)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(69, 13)
        Me.Label16.TabIndex = 7
        Me.Label16.Text = "Max Height:"
        '
        'chkMovieResizeEThumbs
        '
        Me.chkMovieResizeEThumbs.AutoSize = true
        Me.chkMovieResizeEThumbs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieResizeEThumbs.Location = New System.Drawing.Point(6, 82)
        Me.chkMovieResizeEThumbs.Name = "chkMovieResizeEThumbs"
        Me.chkMovieResizeEThumbs.Size = New System.Drawing.Size(133, 17)
        Me.chkMovieResizeEThumbs.TabIndex = 4
        Me.chkMovieResizeEThumbs.Text = "Automatically Resize:"
        Me.chkMovieResizeEThumbs.UseVisualStyleBackColor = true
        '
        'Label17
        '
        Me.Label17.AutoSize = true
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label17.Location = New System.Drawing.Point(4, 18)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(80, 13)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Preferred Size:"
        '
        'cbMovieEThumbsSize
        '
        Me.cbMovieEThumbsSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieEThumbsSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieEThumbsSize.FormattingEnabled = true
        Me.cbMovieEThumbsSize.Location = New System.Drawing.Point(6, 34)
        Me.cbMovieEThumbsSize.Name = "cbMovieEThumbsSize"
        Me.cbMovieEThumbsSize.Size = New System.Drawing.Size(148, 21)
        Me.cbMovieEThumbsSize.TabIndex = 1
        '
        'chkMovieOverwriteEThumbs
        '
        Me.chkMovieOverwriteEThumbs.AutoSize = true
        Me.chkMovieOverwriteEThumbs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieOverwriteEThumbs.Location = New System.Drawing.Point(6, 62)
        Me.chkMovieOverwriteEThumbs.Name = "chkMovieOverwriteEThumbs"
        Me.chkMovieOverwriteEThumbs.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieOverwriteEThumbs.TabIndex = 3
        Me.chkMovieOverwriteEThumbs.Text = "Overwrite Existing"
        Me.chkMovieOverwriteEThumbs.UseVisualStyleBackColor = true
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.chkMovieScraperActorThumbs)
        Me.GroupBox9.Controls.Add(Me.chkMovieNoSaveImagesToNfo)
        Me.GroupBox9.Controls.Add(Me.chkMovieSingleScrapeImages)
        Me.GroupBox9.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.GroupBox9.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(200, 74)
        Me.GroupBox9.TabIndex = 0
        Me.GroupBox9.TabStop = false
        Me.GroupBox9.Text = "Images"
        '
        'chkMovieScraperActorThumbs
        '
        Me.chkMovieScraperActorThumbs.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkMovieScraperActorThumbs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieScraperActorThumbs.Location = New System.Drawing.Point(6, 35)
        Me.chkMovieScraperActorThumbs.Name = "chkMovieScraperActorThumbs"
        Me.chkMovieScraperActorThumbs.Size = New System.Drawing.Size(173, 19)
        Me.chkMovieScraperActorThumbs.TabIndex = 1
        Me.chkMovieScraperActorThumbs.Text = "Enable Actor Thumbs"
        Me.chkMovieScraperActorThumbs.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkMovieScraperActorThumbs.UseVisualStyleBackColor = true
        '
        'chkMovieNoSaveImagesToNfo
        '
        Me.chkMovieNoSaveImagesToNfo.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkMovieNoSaveImagesToNfo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieNoSaveImagesToNfo.Location = New System.Drawing.Point(6, 53)
        Me.chkMovieNoSaveImagesToNfo.Name = "chkMovieNoSaveImagesToNfo"
        Me.chkMovieNoSaveImagesToNfo.Size = New System.Drawing.Size(188, 18)
        Me.chkMovieNoSaveImagesToNfo.TabIndex = 2
        Me.chkMovieNoSaveImagesToNfo.Text = "Do Not Save Image URLs to Nfo"
        Me.chkMovieNoSaveImagesToNfo.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkMovieNoSaveImagesToNfo.UseVisualStyleBackColor = true
        '
        'chkMovieSingleScrapeImages
        '
        Me.chkMovieSingleScrapeImages.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkMovieSingleScrapeImages.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSingleScrapeImages.Location = New System.Drawing.Point(6, 16)
        Me.chkMovieSingleScrapeImages.Name = "chkMovieSingleScrapeImages"
        Me.chkMovieSingleScrapeImages.Size = New System.Drawing.Size(173, 19)
        Me.chkMovieSingleScrapeImages.TabIndex = 0
        Me.chkMovieSingleScrapeImages.Text = "Get on Single Scrape"
        Me.chkMovieSingleScrapeImages.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkMovieSingleScrapeImages.UseVisualStyleBackColor = true
        '
        'gbMovieImagesFanart
        '
        Me.gbMovieImagesFanart.Controls.Add(Me.txtMovieFanartWidth)
        Me.gbMovieImagesFanart.Controls.Add(Me.txtMovieFanartHeight)
        Me.gbMovieImagesFanart.Controls.Add(Me.chkMovieFanartOnly)
        Me.gbMovieImagesFanart.Controls.Add(Me.lblMovieFanartQual)
        Me.gbMovieImagesFanart.Controls.Add(Me.tbMovieFanartQual)
        Me.gbMovieImagesFanart.Controls.Add(Me.Label26)
        Me.gbMovieImagesFanart.Controls.Add(Me.Label9)
        Me.gbMovieImagesFanart.Controls.Add(Me.Label10)
        Me.gbMovieImagesFanart.Controls.Add(Me.chkMovieResizeFanart)
        Me.gbMovieImagesFanart.Controls.Add(Me.cbMovieFanartSize)
        Me.gbMovieImagesFanart.Controls.Add(Me.lblFanartSize)
        Me.gbMovieImagesFanart.Controls.Add(Me.chkMovieOverwriteFanart)
        Me.gbMovieImagesFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesFanart.Location = New System.Drawing.Point(228, 83)
        Me.gbMovieImagesFanart.Name = "gbMovieImagesFanart"
        Me.gbMovieImagesFanart.Size = New System.Drawing.Size(218, 170)
        Me.gbMovieImagesFanart.TabIndex = 3
        Me.gbMovieImagesFanart.TabStop = false
        Me.gbMovieImagesFanart.Text = "Fanart"
        '
        'txtMovieFanartWidth
        '
        Me.txtMovieFanartWidth.Enabled = false
        Me.txtMovieFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieFanartWidth.Location = New System.Drawing.Point(69, 100)
        Me.txtMovieFanartWidth.Name = "txtMovieFanartWidth"
        Me.txtMovieFanartWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieFanartWidth.TabIndex = 6
        '
        'txtMovieFanartHeight
        '
        Me.txtMovieFanartHeight.Enabled = false
        Me.txtMovieFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMovieFanartHeight.Location = New System.Drawing.Point(175, 100)
        Me.txtMovieFanartHeight.Name = "txtMovieFanartHeight"
        Me.txtMovieFanartHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieFanartHeight.TabIndex = 8
        '
        'chkMovieFanartOnly
        '
        Me.chkMovieFanartOnly.AutoSize = true
        Me.chkMovieFanartOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieFanartOnly.Location = New System.Drawing.Point(163, 38)
        Me.chkMovieFanartOnly.Name = "chkMovieFanartOnly"
        Me.chkMovieFanartOnly.Size = New System.Drawing.Size(50, 17)
        Me.chkMovieFanartOnly.TabIndex = 2
        Me.chkMovieFanartOnly.Text = "Only"
        Me.chkMovieFanartOnly.UseVisualStyleBackColor = true
        '
        'lblMovieFanartQual
        '
        Me.lblMovieFanartQual.AutoSize = true
        Me.lblMovieFanartQual.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMovieFanartQual.Location = New System.Drawing.Point(183, 151)
        Me.lblMovieFanartQual.Name = "lblMovieFanartQual"
        Me.lblMovieFanartQual.Size = New System.Drawing.Size(29, 17)
        Me.lblMovieFanartQual.TabIndex = 11
        Me.lblMovieFanartQual.Text = "100"
        '
        'tbMovieFanartQual
        '
        Me.tbMovieFanartQual.AutoSize = false
        Me.tbMovieFanartQual.LargeChange = 10
        Me.tbMovieFanartQual.Location = New System.Drawing.Point(6, 139)
        Me.tbMovieFanartQual.Maximum = 100
        Me.tbMovieFanartQual.Name = "tbMovieFanartQual"
        Me.tbMovieFanartQual.Size = New System.Drawing.Size(180, 27)
        Me.tbMovieFanartQual.TabIndex = 10
        Me.tbMovieFanartQual.TickFrequency = 10
        Me.tbMovieFanartQual.Value = 100
        '
        'Label26
        '
        Me.Label26.AutoSize = true
        Me.Label26.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label26.Location = New System.Drawing.Point(3, 127)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(46, 13)
        Me.Label26.TabIndex = 9
        Me.Label26.Text = "Quality:"
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 104)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "Max Width:"
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label10.Location = New System.Drawing.Point(107, 104)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 13)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "Max Height:"
        '
        'chkMovieResizeFanart
        '
        Me.chkMovieResizeFanart.AutoSize = true
        Me.chkMovieResizeFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieResizeFanart.Location = New System.Drawing.Point(6, 82)
        Me.chkMovieResizeFanart.Name = "chkMovieResizeFanart"
        Me.chkMovieResizeFanart.Size = New System.Drawing.Size(133, 17)
        Me.chkMovieResizeFanart.TabIndex = 4
        Me.chkMovieResizeFanart.Text = "Automatically Resize:"
        Me.chkMovieResizeFanart.UseVisualStyleBackColor = true
        '
        'cbMovieFanartSize
        '
        Me.cbMovieFanartSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieFanartSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMovieFanartSize.FormattingEnabled = true
        Me.cbMovieFanartSize.Location = New System.Drawing.Point(6, 34)
        Me.cbMovieFanartSize.Name = "cbMovieFanartSize"
        Me.cbMovieFanartSize.Size = New System.Drawing.Size(148, 21)
        Me.cbMovieFanartSize.TabIndex = 1
        '
        'lblFanartSize
        '
        Me.lblFanartSize.AutoSize = true
        Me.lblFanartSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblFanartSize.Location = New System.Drawing.Point(4, 18)
        Me.lblFanartSize.Name = "lblFanartSize"
        Me.lblFanartSize.Size = New System.Drawing.Size(80, 13)
        Me.lblFanartSize.TabIndex = 0
        Me.lblFanartSize.Text = "Preferred Size:"
        '
        'chkMovieOverwriteFanart
        '
        Me.chkMovieOverwriteFanart.AutoSize = true
        Me.chkMovieOverwriteFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieOverwriteFanart.Location = New System.Drawing.Point(6, 62)
        Me.chkMovieOverwriteFanart.Name = "chkMovieOverwriteFanart"
        Me.chkMovieOverwriteFanart.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieOverwriteFanart.TabIndex = 3
        Me.chkMovieOverwriteFanart.Text = "Overwrite Existing"
        Me.chkMovieOverwriteFanart.UseVisualStyleBackColor = true
        '
        'gbMovieImagesPoster
        '
        Me.gbMovieImagesPoster.Controls.Add(Me.chkMoviePosterOnly)
        Me.gbMovieImagesPoster.Controls.Add(Me.txtMoviePosterWidth)
        Me.gbMovieImagesPoster.Controls.Add(Me.txtMoviePosterHeight)
        Me.gbMovieImagesPoster.Controls.Add(Me.lblMoviePosterQual)
        Me.gbMovieImagesPoster.Controls.Add(Me.tbMoviePosterQual)
        Me.gbMovieImagesPoster.Controls.Add(Me.Label24)
        Me.gbMovieImagesPoster.Controls.Add(Me.Label11)
        Me.gbMovieImagesPoster.Controls.Add(Me.Label12)
        Me.gbMovieImagesPoster.Controls.Add(Me.chkMovieResizePoster)
        Me.gbMovieImagesPoster.Controls.Add(Me.lblPosterSize)
        Me.gbMovieImagesPoster.Controls.Add(Me.cbMoviePosterSize)
        Me.gbMovieImagesPoster.Controls.Add(Me.chkMovieOverwritePoster)
        Me.gbMovieImagesPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.gbMovieImagesPoster.Location = New System.Drawing.Point(3, 83)
        Me.gbMovieImagesPoster.Name = "gbMovieImagesPoster"
        Me.gbMovieImagesPoster.Size = New System.Drawing.Size(218, 170)
        Me.gbMovieImagesPoster.TabIndex = 2
        Me.gbMovieImagesPoster.TabStop = false
        Me.gbMovieImagesPoster.Text = "Poster"
        '
        'chkMoviePosterOnly
        '
        Me.chkMoviePosterOnly.AutoSize = true
        Me.chkMoviePosterOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMoviePosterOnly.Location = New System.Drawing.Point(163, 38)
        Me.chkMoviePosterOnly.Name = "chkMoviePosterOnly"
        Me.chkMoviePosterOnly.Size = New System.Drawing.Size(50, 17)
        Me.chkMoviePosterOnly.TabIndex = 2
        Me.chkMoviePosterOnly.Text = "Only"
        Me.chkMoviePosterOnly.UseVisualStyleBackColor = true
        '
        'txtMoviePosterWidth
        '
        Me.txtMoviePosterWidth.Enabled = false
        Me.txtMoviePosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMoviePosterWidth.Location = New System.Drawing.Point(68, 100)
        Me.txtMoviePosterWidth.Name = "txtMoviePosterWidth"
        Me.txtMoviePosterWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtMoviePosterWidth.TabIndex = 6
        '
        'txtMoviePosterHeight
        '
        Me.txtMoviePosterHeight.Enabled = false
        Me.txtMoviePosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMoviePosterHeight.Location = New System.Drawing.Point(175, 100)
        Me.txtMoviePosterHeight.Name = "txtMoviePosterHeight"
        Me.txtMoviePosterHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtMoviePosterHeight.TabIndex = 8
        '
        'lblMoviePosterQual
        '
        Me.lblMoviePosterQual.AutoSize = true
        Me.lblMoviePosterQual.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lblMoviePosterQual.Location = New System.Drawing.Point(183, 147)
        Me.lblMoviePosterQual.Name = "lblMoviePosterQual"
        Me.lblMoviePosterQual.Size = New System.Drawing.Size(29, 17)
        Me.lblMoviePosterQual.TabIndex = 11
        Me.lblMoviePosterQual.Text = "100"
        '
        'tbMoviePosterQual
        '
        Me.tbMoviePosterQual.AutoSize = false
        Me.tbMoviePosterQual.LargeChange = 10
        Me.tbMoviePosterQual.Location = New System.Drawing.Point(7, 139)
        Me.tbMoviePosterQual.Maximum = 100
        Me.tbMoviePosterQual.Name = "tbMoviePosterQual"
        Me.tbMoviePosterQual.Size = New System.Drawing.Size(179, 27)
        Me.tbMoviePosterQual.TabIndex = 10
        Me.tbMoviePosterQual.TickFrequency = 10
        Me.tbMoviePosterQual.Value = 100
        '
        'Label24
        '
        Me.Label24.AutoSize = true
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label24.Location = New System.Drawing.Point(3, 127)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(46, 13)
        Me.Label24.TabIndex = 9
        Me.Label24.Text = "Quality:"
        '
        'Label11
        '
        Me.Label11.AutoSize = true
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 104)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(66, 13)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "Max Width:"
        '
        'Label12
        '
        Me.Label12.AutoSize = true
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label12.Location = New System.Drawing.Point(106, 104)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(69, 13)
        Me.Label12.TabIndex = 7
        Me.Label12.Text = "Max Height:"
        '
        'chkMovieResizePoster
        '
        Me.chkMovieResizePoster.AutoSize = true
        Me.chkMovieResizePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieResizePoster.Location = New System.Drawing.Point(6, 82)
        Me.chkMovieResizePoster.Name = "chkMovieResizePoster"
        Me.chkMovieResizePoster.Size = New System.Drawing.Size(133, 17)
        Me.chkMovieResizePoster.TabIndex = 4
        Me.chkMovieResizePoster.Text = "Automatically Resize:"
        Me.chkMovieResizePoster.UseVisualStyleBackColor = true
        '
        'lblPosterSize
        '
        Me.lblPosterSize.AutoSize = true
        Me.lblPosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPosterSize.Location = New System.Drawing.Point(4, 18)
        Me.lblPosterSize.Name = "lblPosterSize"
        Me.lblPosterSize.Size = New System.Drawing.Size(80, 13)
        Me.lblPosterSize.TabIndex = 0
        Me.lblPosterSize.Text = "Preferred Size:"
        '
        'cbMoviePosterSize
        '
        Me.cbMoviePosterSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMoviePosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.cbMoviePosterSize.FormattingEnabled = true
        Me.cbMoviePosterSize.Location = New System.Drawing.Point(6, 34)
        Me.cbMoviePosterSize.Name = "cbMoviePosterSize"
        Me.cbMoviePosterSize.Size = New System.Drawing.Size(148, 21)
        Me.cbMoviePosterSize.TabIndex = 1
        '
        'chkMovieOverwritePoster
        '
        Me.chkMovieOverwritePoster.AutoSize = true
        Me.chkMovieOverwritePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieOverwritePoster.Location = New System.Drawing.Point(6, 62)
        Me.chkMovieOverwritePoster.Name = "chkMovieOverwritePoster"
        Me.chkMovieOverwritePoster.Size = New System.Drawing.Size(119, 17)
        Me.chkMovieOverwritePoster.TabIndex = 3
        Me.chkMovieOverwritePoster.Text = "Overwrite Existing"
        Me.chkMovieOverwritePoster.UseVisualStyleBackColor = true
        '
        'lbGenre
        '
        Me.lbGenre.CheckOnClick = true
        Me.lbGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lbGenre.FormattingEnabled = true
        Me.lbGenre.IntegralHeight = false
        Me.lbGenre.Location = New System.Drawing.Point(10, 18)
        Me.lbGenre.Name = "lbGenre"
        Me.lbGenre.Size = New System.Drawing.Size(157, 97)
        Me.lbGenre.Sorted = true
        Me.lbGenre.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkMovieWatchedCol)
        Me.GroupBox2.Controls.Add(Me.chkMovieEFanartsCol)
        Me.GroupBox2.Controls.Add(Me.txtCheckTitleTol)
        Me.GroupBox2.Controls.Add(Me.Label30)
        Me.GroupBox2.Controls.Add(Me.chkCheckTitles)
        Me.GroupBox2.Controls.Add(Me.GroupBox25)
        Me.GroupBox2.Controls.Add(Me.chkDisplayYear)
        Me.GroupBox2.Controls.Add(Me.chkMovieEThumbsCol)
        Me.GroupBox2.Controls.Add(Me.chkMovieSubCol)
        Me.GroupBox2.Controls.Add(Me.chkMovieTrailerCol)
        Me.GroupBox2.Controls.Add(Me.chkMovieInfoCol)
        Me.GroupBox2.Controls.Add(Me.chkMovieFanartCol)
        Me.GroupBox2.Controls.Add(Me.chkMoviePosterCol)
        Me.GroupBox2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(7, 106)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(218, 306)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = "Media List Options"
        '
        'chkMovieWatchedCol
        '
        Me.chkMovieWatchedCol.AutoSize = true
        Me.chkMovieWatchedCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieWatchedCol.Location = New System.Drawing.Point(9, 290)
        Me.chkMovieWatchedCol.Name = "chkMovieWatchedCol"
        Me.chkMovieWatchedCol.Size = New System.Drawing.Size(142, 17)
        Me.chkMovieWatchedCol.TabIndex = 76
        Me.chkMovieWatchedCol.Text = "Hide Watched Column"
        Me.chkMovieWatchedCol.UseVisualStyleBackColor = true
        '
        'chkMovieEFanartsCol
        '
        Me.chkMovieEFanartsCol.AutoSize = true
        Me.chkMovieEFanartsCol.Enabled = false
        Me.chkMovieEFanartsCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieEFanartsCol.Location = New System.Drawing.Point(9, 274)
        Me.chkMovieEFanartsCol.Name = "chkMovieEFanartsCol"
        Me.chkMovieEFanartsCol.Size = New System.Drawing.Size(152, 17)
        Me.chkMovieEFanartsCol.TabIndex = 75
        Me.chkMovieEFanartsCol.Text = "Hide Extrafanart Column"
        Me.chkMovieEFanartsCol.UseVisualStyleBackColor = true
        '
        'txtCheckTitleTol
        '
        Me.txtCheckTitleTol.Enabled = false
        Me.txtCheckTitleTol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtCheckTitleTol.Location = New System.Drawing.Point(142, 50)
        Me.txtCheckTitleTol.Name = "txtCheckTitleTol"
        Me.txtCheckTitleTol.Size = New System.Drawing.Size(61, 22)
        Me.txtCheckTitleTol.TabIndex = 74
        '
        'Label30
        '
        Me.Label30.AutoSize = true
        Me.Label30.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label30.Location = New System.Drawing.Point(27, 54)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(111, 13)
        Me.Label30.TabIndex = 73
        Me.Label30.Text = "Mismatch Tolerance:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkCheckTitles
        '
        Me.chkCheckTitles.AutoSize = true
        Me.chkCheckTitles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkCheckTitles.Location = New System.Drawing.Point(8, 35)
        Me.chkCheckTitles.Name = "chkCheckTitles"
        Me.chkCheckTitles.Size = New System.Drawing.Size(178, 17)
        Me.chkCheckTitles.TabIndex = 72
        Me.chkCheckTitles.Text = "Check Title Match Confidence"
        Me.chkCheckTitles.UseVisualStyleBackColor = true
        '
        'GroupBox25
        '
        Me.GroupBox25.Controls.Add(Me.btnRemoveToken)
        Me.GroupBox25.Controls.Add(Me.btnAddToken)
        Me.GroupBox25.Controls.Add(Me.txtSortToken)
        Me.GroupBox25.Controls.Add(Me.lstSortTokens)
        Me.GroupBox25.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.GroupBox25.Location = New System.Drawing.Point(9, 73)
        Me.GroupBox25.Name = "GroupBox25"
        Me.GroupBox25.Size = New System.Drawing.Size(200, 102)
        Me.GroupBox25.TabIndex = 71
        Me.GroupBox25.TabStop = false
        Me.GroupBox25.Text = "Sort Tokens to Ignore"
        '
        'btnRemoveToken
        '
        Me.btnRemoveToken.Image = CType(resources.GetObject("btnRemoveToken.Image"),System.Drawing.Image)
        Me.btnRemoveToken.Location = New System.Drawing.Point(168, 72)
        Me.btnRemoveToken.Name = "btnRemoveToken"
        Me.btnRemoveToken.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveToken.TabIndex = 3
        Me.btnRemoveToken.UseVisualStyleBackColor = true
        '
        'btnAddToken
        '
        Me.btnAddToken.Image = CType(resources.GetObject("btnAddToken.Image"),System.Drawing.Image)
        Me.btnAddToken.Location = New System.Drawing.Point(72, 72)
        Me.btnAddToken.Name = "btnAddToken"
        Me.btnAddToken.Size = New System.Drawing.Size(23, 23)
        Me.btnAddToken.TabIndex = 2
        Me.btnAddToken.UseVisualStyleBackColor = true
        '
        'txtSortToken
        '
        Me.txtSortToken.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtSortToken.Location = New System.Drawing.Point(10, 73)
        Me.txtSortToken.Name = "txtSortToken"
        Me.txtSortToken.Size = New System.Drawing.Size(61, 22)
        Me.txtSortToken.TabIndex = 1
        '
        'lstSortTokens
        '
        Me.lstSortTokens.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lstSortTokens.FormattingEnabled = true
        Me.lstSortTokens.Location = New System.Drawing.Point(10, 15)
        Me.lstSortTokens.Name = "lstSortTokens"
        Me.lstSortTokens.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstSortTokens.Size = New System.Drawing.Size(180, 43)
        Me.lstSortTokens.Sorted = true
        Me.lstSortTokens.TabIndex = 0
        '
        'chkDisplayYear
        '
        Me.chkDisplayYear.AutoSize = true
        Me.chkDisplayYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkDisplayYear.Location = New System.Drawing.Point(8, 19)
        Me.chkDisplayYear.Name = "chkDisplayYear"
        Me.chkDisplayYear.Size = New System.Drawing.Size(144, 17)
        Me.chkDisplayYear.TabIndex = 70
        Me.chkDisplayYear.Text = "Display Year in List Title"
        Me.chkDisplayYear.UseVisualStyleBackColor = true
        '
        'chkMovieEThumbsCol
        '
        Me.chkMovieEThumbsCol.AutoSize = true
        Me.chkMovieEThumbsCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieEThumbsCol.Location = New System.Drawing.Point(9, 258)
        Me.chkMovieEThumbsCol.Name = "chkMovieEThumbsCol"
        Me.chkMovieEThumbsCol.Size = New System.Drawing.Size(155, 17)
        Me.chkMovieEThumbsCol.TabIndex = 5
        Me.chkMovieEThumbsCol.Text = "Hide Extrathumb Column"
        Me.chkMovieEThumbsCol.UseVisualStyleBackColor = true
        '
        'chkMovieSubCol
        '
        Me.chkMovieSubCol.AutoSize = true
        Me.chkMovieSubCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieSubCol.Location = New System.Drawing.Point(9, 242)
        Me.chkMovieSubCol.Name = "chkMovieSubCol"
        Me.chkMovieSubCol.Size = New System.Drawing.Size(116, 17)
        Me.chkMovieSubCol.TabIndex = 4
        Me.chkMovieSubCol.Text = "Hide Sub Column"
        Me.chkMovieSubCol.UseVisualStyleBackColor = true
        '
        'chkMovieTrailerCol
        '
        Me.chkMovieTrailerCol.AutoSize = true
        Me.chkMovieTrailerCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieTrailerCol.Location = New System.Drawing.Point(9, 226)
        Me.chkMovieTrailerCol.Name = "chkMovieTrailerCol"
        Me.chkMovieTrailerCol.Size = New System.Drawing.Size(127, 17)
        Me.chkMovieTrailerCol.TabIndex = 3
        Me.chkMovieTrailerCol.Text = "Hide Trailer Column"
        Me.chkMovieTrailerCol.UseVisualStyleBackColor = true
        '
        'chkMovieInfoCol
        '
        Me.chkMovieInfoCol.AutoSize = true
        Me.chkMovieInfoCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieInfoCol.Location = New System.Drawing.Point(9, 210)
        Me.chkMovieInfoCol.Name = "chkMovieInfoCol"
        Me.chkMovieInfoCol.Size = New System.Drawing.Size(117, 17)
        Me.chkMovieInfoCol.TabIndex = 2
        Me.chkMovieInfoCol.Text = "Hide Info Column"
        Me.chkMovieInfoCol.UseVisualStyleBackColor = true
        '
        'chkMovieFanartCol
        '
        Me.chkMovieFanartCol.AutoSize = true
        Me.chkMovieFanartCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMovieFanartCol.Location = New System.Drawing.Point(9, 194)
        Me.chkMovieFanartCol.Name = "chkMovieFanartCol"
        Me.chkMovieFanartCol.Size = New System.Drawing.Size(129, 17)
        Me.chkMovieFanartCol.TabIndex = 1
        Me.chkMovieFanartCol.Text = "Hide Fanart Column"
        Me.chkMovieFanartCol.UseVisualStyleBackColor = true
        '
        'chkMoviePosterCol
        '
        Me.chkMoviePosterCol.AutoSize = true
        Me.chkMoviePosterCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chkMoviePosterCol.Location = New System.Drawing.Point(9, 178)
        Me.chkMoviePosterCol.Name = "chkMoviePosterCol"
        Me.chkMoviePosterCol.Size = New System.Drawing.Size(128, 17)
        Me.chkMoviePosterCol.TabIndex = 0
        Me.chkMoviePosterCol.Text = "Hide Poster Column"
        Me.chkMoviePosterCol.UseVisualStyleBackColor = true
        '
        'lvMovies
        '
        Me.lvMovies.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colID, Me.colName, Me.colPath, Me.colRecur, Me.colFolder, Me.colSingle})
        Me.lvMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.lvMovies.FullRowSelect = true
        Me.lvMovies.HideSelection = false
        Me.lvMovies.Location = New System.Drawing.Point(5, 6)
        Me.lvMovies.Name = "lvMovies"
        Me.lvMovies.Size = New System.Drawing.Size(627, 105)
        Me.lvMovies.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvMovies.TabIndex = 0
        Me.lvMovies.UseCompatibleStateImageBehavior = false
        Me.lvMovies.View = System.Windows.Forms.View.Details
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
        Me.colPath.Width = 235
        '
        'colRecur
        '
        Me.colRecur.Text = "Recursive"
        Me.colRecur.Width = 78
        '
        'colFolder
        '
        Me.colFolder.Text = "Use Folder Name"
        Me.colFolder.Width = 116
        '
        'colSingle
        '
        Me.colSingle.Text = "Single Video"
        Me.colSingle.Width = 90
        '
        'btnMovieRem
        '
        Me.btnMovieRem.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnMovieRem.Image = CType(resources.GetObject("btnMovieRem.Image"),System.Drawing.Image)
        Me.btnMovieRem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieRem.Location = New System.Drawing.Point(638, 88)
        Me.btnMovieRem.Name = "btnMovieRem"
        Me.btnMovieRem.Size = New System.Drawing.Size(104, 23)
        Me.btnMovieRem.TabIndex = 3
        Me.btnMovieRem.Text = "Remove"
        Me.btnMovieRem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieRem.UseVisualStyleBackColor = true
        '
        'btnMovieAddFolder
        '
        Me.btnMovieAddFolder.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnMovieAddFolder.Image = CType(resources.GetObject("btnMovieAddFolder.Image"),System.Drawing.Image)
        Me.btnMovieAddFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieAddFolder.Location = New System.Drawing.Point(638, 6)
        Me.btnMovieAddFolder.Name = "btnMovieAddFolder"
        Me.btnMovieAddFolder.Size = New System.Drawing.Size(104, 23)
        Me.btnMovieAddFolder.TabIndex = 1
        Me.btnMovieAddFolder.Text = "Add Source"
        Me.btnMovieAddFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieAddFolder.UseVisualStyleBackColor = true
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnOK.Location = New System.Drawing.Point(929, 703)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = true
        '
        'btnApply
        '
        Me.btnApply.Enabled = false
        Me.btnApply.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnApply.Location = New System.Drawing.Point(766, 703)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 23)
        Me.btnApply.TabIndex = 2
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = true
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238,Byte))
        Me.btnCancel.Location = New System.Drawing.Point(848, 703)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = true
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.SteelBlue
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.lblTopDetails)
        Me.pnlTop.Controls.Add(Me.lblTopTitle)
        Me.pnlTop.Controls.Add(Me.pbTopLogo)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1008, 64)
        Me.pnlTop.TabIndex = 3
        '
        'lblTopDetails
        '
        Me.lblTopDetails.AutoSize = True
        Me.lblTopDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblTopDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopDetails.ForeColor = System.Drawing.Color.White
        Me.lblTopDetails.Location = New System.Drawing.Point(61, 38)
        Me.lblTopDetails.Name = "lblTopDetails"
        Me.lblTopDetails.Size = New System.Drawing.Size(245, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Configure Ember's appearance and operation."
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(58, 3)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(107, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Settings"
        '
        'pbTopLogo
        '
        Me.pbTopLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbTopLogo.Image = CType(resources.GetObject("pbTopLogo.Image"), System.Drawing.Image)
        Me.pbTopLogo.Location = New System.Drawing.Point(7, 8)
        Me.pbTopLogo.Name = "pbTopLogo"
        Me.pbTopLogo.Size = New System.Drawing.Size(48, 48)
        Me.pbTopLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbTopLogo.TabIndex = 0
        Me.pbTopLogo.TabStop = False
        '
        'ilSettings
        '
        Me.ilSettings.ImageStream = CType(resources.GetObject("ilSettings.ImageStream"), System.Windows.Forms.ImageListStreamer)
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
        '
        'tvSettings
        '
        Me.tvSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tvSettings.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvSettings.FullRowSelect = True
        Me.tvSettings.HideSelection = False
        Me.tvSettings.ImageIndex = 0
        Me.tvSettings.ImageList = Me.ilSettings
        Me.tvSettings.Location = New System.Drawing.Point(5, 147)
        Me.tvSettings.Name = "tvSettings"
        Me.tvSettings.SelectedImageIndex = 0
        Me.tvSettings.ShowLines = False
        Me.tvSettings.ShowPlusMinus = False
        Me.tvSettings.Size = New System.Drawing.Size(242, 502)
        Me.tvSettings.TabIndex = 7
        '
        'pnlGeneral
        '
        Me.pnlGeneral.BackColor = System.Drawing.Color.White
        Me.pnlGeneral.Controls.Add(Me.gbInterface)
        Me.pnlGeneral.Controls.Add(Me.GroupBox4)
        Me.pnlGeneral.Location = New System.Drawing.Point(1175, 560)
        Me.pnlGeneral.Name = "pnlGeneral"
        Me.pnlGeneral.Size = New System.Drawing.Size(750, 500)
        Me.pnlGeneral.TabIndex = 10
        Me.pnlGeneral.Visible = False
        '
        'gbInterface
        '
        Me.gbInterface.Controls.Add(Me.GroupBox29)
        Me.gbInterface.Controls.Add(Me.Label32)
        Me.gbInterface.Controls.Add(Me.cbIntLang)
        Me.gbInterface.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbInterface.Location = New System.Drawing.Point(7, 3)
        Me.gbInterface.Name = "gbInterface"
        Me.gbInterface.Size = New System.Drawing.Size(196, 344)
        Me.gbInterface.TabIndex = 0
        Me.gbInterface.TabStop = False
        Me.gbInterface.Text = "Interface"
        '
        'pnlMovieGeneral
        '
        Me.pnlMovieGeneral.BackColor = System.Drawing.Color.White
        Me.pnlMovieGeneral.Controls.Add(Me.GroupBox30)
        Me.pnlMovieGeneral.Controls.Add(Me.gbGenreFilter)
        Me.pnlMovieGeneral.Controls.Add(Me.gbFilters)
        Me.pnlMovieGeneral.Controls.Add(Me.GroupBox27)
        Me.pnlMovieGeneral.Controls.Add(Me.GroupBox12)
        Me.pnlMovieGeneral.Controls.Add(Me.GroupBox2)
        Me.pnlMovieGeneral.Location = New System.Drawing.Point(1275, 440)
        Me.pnlMovieGeneral.Name = "pnlMovieGeneral"
        Me.pnlMovieGeneral.Size = New System.Drawing.Size(750, 500)
        Me.pnlMovieGeneral.TabIndex = 15
        Me.pnlMovieGeneral.Visible = False
        '
        'GroupBox30
        '
        Me.GroupBox30.Controls.Add(Me.Label18)
        Me.GroupBox30.Controls.Add(Me.txtIMDBURL)
        Me.GroupBox30.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox30.Location = New System.Drawing.Point(235, 344)
        Me.GroupBox30.Name = "GroupBox30"
        Me.GroupBox30.Size = New System.Drawing.Size(376, 41)
        Me.GroupBox30.TabIndex = 8
        Me.GroupBox30.TabStop = False
        Me.GroupBox30.Text = "IMDB"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(6, 18)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(73, 13)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "IMDB Mirror:"
        '
        'txtIMDBURL
        '
        Me.txtIMDBURL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIMDBURL.Location = New System.Drawing.Point(79, 15)
        Me.txtIMDBURL.Name = "txtIMDBURL"
        Me.txtIMDBURL.Size = New System.Drawing.Size(291, 22)
        Me.txtIMDBURL.TabIndex = 1
        '
        'gbGenreFilter
        '
        Me.gbGenreFilter.Controls.Add(Me.lbGenre)
        Me.gbGenreFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbGenreFilter.Location = New System.Drawing.Point(428, 209)
        Me.gbGenreFilter.Name = "gbGenreFilter"
        Me.gbGenreFilter.Size = New System.Drawing.Size(183, 123)
        Me.gbGenreFilter.TabIndex = 7
        Me.gbGenreFilter.TabStop = False
        Me.gbGenreFilter.Text = "Genre Language Filter"
        '
        'gbFilters
        '
        Me.gbFilters.Controls.Add(Me.btnResetMovieFilters)
        Me.gbFilters.Controls.Add(Me.btnDown)
        Me.gbFilters.Controls.Add(Me.btnUp)
        Me.gbFilters.Controls.Add(Me.chkProperCase)
        Me.gbFilters.Controls.Add(Me.btnRemoveFilter)
        Me.gbFilters.Controls.Add(Me.btnAddFilter)
        Me.gbFilters.Controls.Add(Me.txtFilter)
        Me.gbFilters.Controls.Add(Me.lstFilters)
        Me.gbFilters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFilters.Location = New System.Drawing.Point(229, 7)
        Me.gbFilters.Name = "gbFilters"
        Me.gbFilters.Size = New System.Drawing.Size(382, 200)
        Me.gbFilters.TabIndex = 6
        Me.gbFilters.TabStop = False
        Me.gbFilters.Text = "Folder/File Name Filters"
        '
        'btnResetMovieFilters
        '
        Me.btnResetMovieFilters.Image = CType(resources.GetObject("btnResetMovieFilters.Image"), System.Drawing.Image)
        Me.btnResetMovieFilters.Location = New System.Drawing.Point(355, 11)
        Me.btnResetMovieFilters.Name = "btnResetMovieFilters"
        Me.btnResetMovieFilters.Size = New System.Drawing.Size(23, 23)
        Me.btnResetMovieFilters.TabIndex = 8
        Me.btnResetMovieFilters.UseVisualStyleBackColor = True
        '
        'btnDown
        '
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(313, 172)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(23, 23)
        Me.btnDown.TabIndex = 5
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(289, 172)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(23, 23)
        Me.btnUp.TabIndex = 4
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'chkProperCase
        '
        Me.chkProperCase.AutoSize = True
        Me.chkProperCase.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkProperCase.Location = New System.Drawing.Point(6, 16)
        Me.chkProperCase.Name = "chkProperCase"
        Me.chkProperCase.Size = New System.Drawing.Size(181, 17)
        Me.chkProperCase.TabIndex = 0
        Me.chkProperCase.Text = "Convert Names to Proper Case"
        Me.chkProperCase.UseVisualStyleBackColor = True
        '
        'btnRemoveFilter
        '
        Me.btnRemoveFilter.Image = CType(resources.GetObject("btnRemoveFilter.Image"), System.Drawing.Image)
        Me.btnRemoveFilter.Location = New System.Drawing.Point(354, 172)
        Me.btnRemoveFilter.Name = "btnRemoveFilter"
        Me.btnRemoveFilter.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveFilter.TabIndex = 6
        Me.btnRemoveFilter.UseVisualStyleBackColor = True
        '
        'btnAddFilter
        '
        Me.btnAddFilter.Image = CType(resources.GetObject("btnAddFilter.Image"), System.Drawing.Image)
        Me.btnAddFilter.Location = New System.Drawing.Point(247, 172)
        Me.btnAddFilter.Name = "btnAddFilter"
        Me.btnAddFilter.Size = New System.Drawing.Size(23, 23)
        Me.btnAddFilter.TabIndex = 3
        Me.btnAddFilter.UseVisualStyleBackColor = True
        '
        'txtFilter
        '
        Me.txtFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilter.Location = New System.Drawing.Point(6, 173)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(239, 22)
        Me.txtFilter.TabIndex = 2
        '
        'lstFilters
        '
        Me.lstFilters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstFilters.FormattingEnabled = True
        Me.lstFilters.Location = New System.Drawing.Point(6, 36)
        Me.lstFilters.Name = "lstFilters"
        Me.lstFilters.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFilters.Size = New System.Drawing.Size(371, 121)
        Me.lstFilters.TabIndex = 1
        '
        'GroupBox27
        '
        Me.GroupBox27.Controls.Add(Me.chkMissingEFanarts)
        Me.GroupBox27.Controls.Add(Me.chkMissingEThumbs)
        Me.GroupBox27.Controls.Add(Me.chkMissingSubs)
        Me.GroupBox27.Controls.Add(Me.chkMissingTrailer)
        Me.GroupBox27.Controls.Add(Me.chkMissingNFO)
        Me.GroupBox27.Controls.Add(Me.chkMissingFanart)
        Me.GroupBox27.Controls.Add(Me.chkMissingPoster)
        Me.GroupBox27.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox27.Location = New System.Drawing.Point(229, 209)
        Me.GroupBox27.Name = "GroupBox27"
        Me.GroupBox27.Size = New System.Drawing.Size(185, 133)
        Me.GroupBox27.TabIndex = 5
        Me.GroupBox27.TabStop = False
        Me.GroupBox27.Text = "Missing Items Filter"
        '
        'chkMissingEFanarts
        '
        Me.chkMissingEFanarts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMissingEFanarts.Location = New System.Drawing.Point(8, 113)
        Me.chkMissingEFanarts.Name = "chkMissingEFanarts"
        Me.chkMissingEFanarts.Size = New System.Drawing.Size(174, 17)
        Me.chkMissingEFanarts.TabIndex = 12
        Me.chkMissingEFanarts.Text = "Check for Extrafanarts"
        Me.chkMissingEFanarts.UseVisualStyleBackColor = True
        '
        'chkMissingEThumbs
        '
        Me.chkMissingEThumbs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMissingEThumbs.Location = New System.Drawing.Point(8, 98)
        Me.chkMissingEThumbs.Name = "chkMissingEThumbs"
        Me.chkMissingEThumbs.Size = New System.Drawing.Size(174, 17)
        Me.chkMissingEThumbs.TabIndex = 11
        Me.chkMissingEThumbs.Text = "Check for Extrathumbs"
        Me.chkMissingEThumbs.UseVisualStyleBackColor = True
        '
        'chkMissingSubs
        '
        Me.chkMissingSubs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMissingSubs.Location = New System.Drawing.Point(8, 82)
        Me.chkMissingSubs.Name = "chkMissingSubs"
        Me.chkMissingSubs.Size = New System.Drawing.Size(174, 17)
        Me.chkMissingSubs.TabIndex = 10
        Me.chkMissingSubs.Text = "Check for Subs"
        Me.chkMissingSubs.UseVisualStyleBackColor = True
        '
        'chkMissingTrailer
        '
        Me.chkMissingTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMissingTrailer.Location = New System.Drawing.Point(8, 66)
        Me.chkMissingTrailer.Name = "chkMissingTrailer"
        Me.chkMissingTrailer.Size = New System.Drawing.Size(174, 17)
        Me.chkMissingTrailer.TabIndex = 9
        Me.chkMissingTrailer.Text = "Check for Trailer"
        Me.chkMissingTrailer.UseVisualStyleBackColor = True
        '
        'chkMissingNFO
        '
        Me.chkMissingNFO.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMissingNFO.Location = New System.Drawing.Point(8, 50)
        Me.chkMissingNFO.Name = "chkMissingNFO"
        Me.chkMissingNFO.Size = New System.Drawing.Size(174, 17)
        Me.chkMissingNFO.TabIndex = 8
        Me.chkMissingNFO.Text = "Check for NFO"
        Me.chkMissingNFO.UseVisualStyleBackColor = True
        '
        'chkMissingFanart
        '
        Me.chkMissingFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMissingFanart.Location = New System.Drawing.Point(8, 34)
        Me.chkMissingFanart.Name = "chkMissingFanart"
        Me.chkMissingFanart.Size = New System.Drawing.Size(174, 17)
        Me.chkMissingFanart.TabIndex = 7
        Me.chkMissingFanart.Text = "Check for Fanart"
        Me.chkMissingFanart.UseVisualStyleBackColor = True
        '
        'chkMissingPoster
        '
        Me.chkMissingPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMissingPoster.Location = New System.Drawing.Point(8, 18)
        Me.chkMissingPoster.Name = "chkMissingPoster"
        Me.chkMissingPoster.Size = New System.Drawing.Size(174, 17)
        Me.chkMissingPoster.TabIndex = 6
        Me.chkMissingPoster.Text = "Check for Poster"
        Me.chkMissingPoster.UseVisualStyleBackColor = True
        '
        'pnlExtensions
        '
        Me.pnlExtensions.BackColor = System.Drawing.Color.White
        Me.pnlExtensions.Controls.Add(Me.GroupBox22)
        Me.pnlExtensions.Controls.Add(Me.GroupBox3)
        Me.pnlExtensions.Controls.Add(Me.GroupBox18)
        Me.pnlExtensions.Location = New System.Drawing.Point(1225, 500)
        Me.pnlExtensions.Name = "pnlExtensions"
        Me.pnlExtensions.Size = New System.Drawing.Size(750, 500)
        Me.pnlExtensions.TabIndex = 17
        Me.pnlExtensions.Visible = False
        '
        'GroupBox22
        '
        Me.GroupBox22.Controls.Add(Me.btnRemoveNoStack)
        Me.GroupBox22.Controls.Add(Me.btnAddNoStack)
        Me.GroupBox22.Controls.Add(Me.txtNoStack)
        Me.GroupBox22.Controls.Add(Me.lstNoStack)
        Me.GroupBox22.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox22.Location = New System.Drawing.Point(202, 6)
        Me.GroupBox22.Name = "GroupBox22"
        Me.GroupBox22.Size = New System.Drawing.Size(194, 199)
        Me.GroupBox22.TabIndex = 1
        Me.GroupBox22.TabStop = False
        Me.GroupBox22.Text = "No Stack Extensions"
        '
        'btnRemoveNoStack
        '
        Me.btnRemoveNoStack.Image = CType(resources.GetObject("btnRemoveNoStack.Image"), System.Drawing.Image)
        Me.btnRemoveNoStack.Location = New System.Drawing.Point(160, 167)
        Me.btnRemoveNoStack.Name = "btnRemoveNoStack"
        Me.btnRemoveNoStack.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveNoStack.TabIndex = 3
        Me.btnRemoveNoStack.UseVisualStyleBackColor = True
        '
        'btnAddNoStack
        '
        Me.btnAddNoStack.Image = CType(resources.GetObject("btnAddNoStack.Image"), System.Drawing.Image)
        Me.btnAddNoStack.Location = New System.Drawing.Point(73, 167)
        Me.btnAddNoStack.Name = "btnAddNoStack"
        Me.btnAddNoStack.Size = New System.Drawing.Size(23, 23)
        Me.btnAddNoStack.TabIndex = 2
        Me.btnAddNoStack.UseVisualStyleBackColor = True
        '
        'txtNoStack
        '
        Me.txtNoStack.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoStack.Location = New System.Drawing.Point(11, 168)
        Me.txtNoStack.Name = "txtNoStack"
        Me.txtNoStack.Size = New System.Drawing.Size(61, 22)
        Me.txtNoStack.TabIndex = 1
        '
        'lstNoStack
        '
        Me.lstNoStack.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstNoStack.FormattingEnabled = True
        Me.lstNoStack.Location = New System.Drawing.Point(11, 15)
        Me.lstNoStack.Name = "lstNoStack"
        Me.lstNoStack.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstNoStack.Size = New System.Drawing.Size(171, 134)
        Me.lstNoStack.Sorted = True
        Me.lstNoStack.TabIndex = 0
        '
        'GroupBox18
        '
        Me.GroupBox18.Controls.Add(Me.btnResetValidExts)
        Me.GroupBox18.Controls.Add(Me.btnRemMovieExt)
        Me.GroupBox18.Controls.Add(Me.btnAddMovieExt)
        Me.GroupBox18.Controls.Add(Me.txtMovieExt)
        Me.GroupBox18.Controls.Add(Me.lstMovieExts)
        Me.GroupBox18.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox18.Location = New System.Drawing.Point(3, 6)
        Me.GroupBox18.Name = "GroupBox18"
        Me.GroupBox18.Size = New System.Drawing.Size(192, 385)
        Me.GroupBox18.TabIndex = 0
        Me.GroupBox18.TabStop = False
        Me.GroupBox18.Text = "Valid Video Extensions"
        '
        'btnResetValidExts
        '
        Me.btnResetValidExts.Image = CType(resources.GetObject("btnResetValidExts.Image"), System.Drawing.Image)
        Me.btnResetValidExts.Location = New System.Drawing.Point(164, 12)
        Me.btnResetValidExts.Name = "btnResetValidExts"
        Me.btnResetValidExts.Size = New System.Drawing.Size(23, 23)
        Me.btnResetValidExts.TabIndex = 4
        Me.btnResetValidExts.UseVisualStyleBackColor = True
        '
        'btnRemMovieExt
        '
        Me.btnRemMovieExt.Image = CType(resources.GetObject("btnRemMovieExt.Image"), System.Drawing.Image)
        Me.btnRemMovieExt.Location = New System.Drawing.Point(163, 356)
        Me.btnRemMovieExt.Name = "btnRemMovieExt"
        Me.btnRemMovieExt.Size = New System.Drawing.Size(23, 23)
        Me.btnRemMovieExt.TabIndex = 3
        Me.btnRemMovieExt.UseVisualStyleBackColor = True
        '
        'btnAddMovieExt
        '
        Me.btnAddMovieExt.Image = CType(resources.GetObject("btnAddMovieExt.Image"), System.Drawing.Image)
        Me.btnAddMovieExt.Location = New System.Drawing.Point(68, 356)
        Me.btnAddMovieExt.Name = "btnAddMovieExt"
        Me.btnAddMovieExt.Size = New System.Drawing.Size(23, 23)
        Me.btnAddMovieExt.TabIndex = 2
        Me.btnAddMovieExt.UseVisualStyleBackColor = True
        '
        'txtMovieExt
        '
        Me.txtMovieExt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieExt.Location = New System.Drawing.Point(6, 357)
        Me.txtMovieExt.Name = "txtMovieExt"
        Me.txtMovieExt.Size = New System.Drawing.Size(61, 22)
        Me.txtMovieExt.TabIndex = 1
        '
        'lstMovieExts
        '
        Me.lstMovieExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstMovieExts.FormattingEnabled = True
        Me.lstMovieExts.Location = New System.Drawing.Point(6, 37)
        Me.lstMovieExts.Name = "lstMovieExts"
        Me.lstMovieExts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstMovieExts.Size = New System.Drawing.Size(180, 303)
        Me.lstMovieExts.Sorted = True
        Me.lstMovieExts.TabIndex = 0
        '
        'pnlProxy
        '
        Me.pnlProxy.BackColor = System.Drawing.Color.White
        Me.pnlProxy.Controls.Add(Me.gbProxy)
        Me.pnlProxy.Location = New System.Drawing.Point(1075, 680)
        Me.pnlProxy.Name = "pnlProxy"
        Me.pnlProxy.Size = New System.Drawing.Size(750, 500)
        Me.pnlProxy.TabIndex = 18
        Me.pnlProxy.Visible = False
        '
        'gbProxy
        '
        Me.gbProxy.Controls.Add(Me.gbCreds)
        Me.gbProxy.Controls.Add(Me.lblProxyPort)
        Me.gbProxy.Controls.Add(Me.lblProxyURI)
        Me.gbProxy.Controls.Add(Me.txtProxyPort)
        Me.gbProxy.Controls.Add(Me.txtProxyURI)
        Me.gbProxy.Controls.Add(Me.chkEnableProxy)
        Me.gbProxy.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbProxy.Location = New System.Drawing.Point(3, 6)
        Me.gbProxy.Name = "gbProxy"
        Me.gbProxy.Size = New System.Drawing.Size(290, 230)
        Me.gbProxy.TabIndex = 0
        Me.gbProxy.TabStop = False
        Me.gbProxy.Text = "Proxy"
        '
        'gbCreds
        '
        Me.gbCreds.Controls.Add(Me.txtProxyDomain)
        Me.gbCreds.Controls.Add(Me.lblProxyDomain)
        Me.gbCreds.Controls.Add(Me.txtProxyPassword)
        Me.gbCreds.Controls.Add(Me.txtProxyUsername)
        Me.gbCreds.Controls.Add(Me.lblProxyUN)
        Me.gbCreds.Controls.Add(Me.lblProxyPW)
        Me.gbCreds.Controls.Add(Me.chkEnableCredentials)
        Me.gbCreds.Enabled = False
        Me.gbCreds.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbCreds.Location = New System.Drawing.Point(5, 115)
        Me.gbCreds.Name = "gbCreds"
        Me.gbCreds.Size = New System.Drawing.Size(279, 103)
        Me.gbCreds.TabIndex = 5
        Me.gbCreds.TabStop = False
        Me.gbCreds.Text = "Credentials"
        '
        'txtProxyDomain
        '
        Me.txtProxyDomain.Enabled = False
        Me.txtProxyDomain.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProxyDomain.Location = New System.Drawing.Point(64, 69)
        Me.txtProxyDomain.Name = "txtProxyDomain"
        Me.txtProxyDomain.Size = New System.Drawing.Size(209, 22)
        Me.txtProxyDomain.TabIndex = 6
        '
        'lblProxyDomain
        '
        Me.lblProxyDomain.AutoSize = True
        Me.lblProxyDomain.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProxyDomain.Location = New System.Drawing.Point(14, 72)
        Me.lblProxyDomain.Name = "lblProxyDomain"
        Me.lblProxyDomain.Size = New System.Drawing.Size(50, 13)
        Me.lblProxyDomain.TabIndex = 5
        Me.lblProxyDomain.Text = "Domain:"
        '
        'txtProxyPassword
        '
        Me.txtProxyPassword.Enabled = False
        Me.txtProxyPassword.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProxyPassword.Location = New System.Drawing.Point(201, 39)
        Me.txtProxyPassword.Name = "txtProxyPassword"
        Me.txtProxyPassword.Size = New System.Drawing.Size(72, 22)
        Me.txtProxyPassword.TabIndex = 4
        Me.txtProxyPassword.UseSystemPasswordChar = True
        '
        'txtProxyUsername
        '
        Me.txtProxyUsername.Enabled = False
        Me.txtProxyUsername.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProxyUsername.Location = New System.Drawing.Point(64, 39)
        Me.txtProxyUsername.Name = "txtProxyUsername"
        Me.txtProxyUsername.Size = New System.Drawing.Size(72, 22)
        Me.txtProxyUsername.TabIndex = 2
        '
        'lblProxyUN
        '
        Me.lblProxyUN.AutoSize = True
        Me.lblProxyUN.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProxyUN.Location = New System.Drawing.Point(3, 42)
        Me.lblProxyUN.Name = "lblProxyUN"
        Me.lblProxyUN.Size = New System.Drawing.Size(61, 13)
        Me.lblProxyUN.TabIndex = 1
        Me.lblProxyUN.Text = "Username:"
        '
        'lblProxyPW
        '
        Me.lblProxyPW.AutoSize = True
        Me.lblProxyPW.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProxyPW.Location = New System.Drawing.Point(143, 42)
        Me.lblProxyPW.Name = "lblProxyPW"
        Me.lblProxyPW.Size = New System.Drawing.Size(59, 13)
        Me.lblProxyPW.TabIndex = 3
        Me.lblProxyPW.Text = "Password:"
        '
        'chkEnableCredentials
        '
        Me.chkEnableCredentials.AutoSize = True
        Me.chkEnableCredentials.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnableCredentials.Location = New System.Drawing.Point(8, 17)
        Me.chkEnableCredentials.Name = "chkEnableCredentials"
        Me.chkEnableCredentials.Size = New System.Drawing.Size(122, 17)
        Me.chkEnableCredentials.TabIndex = 0
        Me.chkEnableCredentials.Text = "Enable Credentials"
        Me.chkEnableCredentials.UseVisualStyleBackColor = True
        '
        'lblProxyPort
        '
        Me.lblProxyPort.AutoSize = True
        Me.lblProxyPort.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProxyPort.Location = New System.Drawing.Point(9, 88)
        Me.lblProxyPort.Name = "lblProxyPort"
        Me.lblProxyPort.Size = New System.Drawing.Size(61, 13)
        Me.lblProxyPort.TabIndex = 3
        Me.lblProxyPort.Text = "Proxy Port:"
        '
        'lblProxyURI
        '
        Me.lblProxyURI.AutoSize = True
        Me.lblProxyURI.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProxyURI.Location = New System.Drawing.Point(9, 39)
        Me.lblProxyURI.Name = "lblProxyURI"
        Me.lblProxyURI.Size = New System.Drawing.Size(58, 13)
        Me.lblProxyURI.TabIndex = 1
        Me.lblProxyURI.Text = "Proxy URI:"
        '
        'txtProxyPort
        '
        Me.txtProxyPort.Enabled = False
        Me.txtProxyPort.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProxyPort.Location = New System.Drawing.Point(72, 84)
        Me.txtProxyPort.Name = "txtProxyPort"
        Me.txtProxyPort.Size = New System.Drawing.Size(51, 22)
        Me.txtProxyPort.TabIndex = 4
        '
        'txtProxyURI
        '
        Me.txtProxyURI.Enabled = False
        Me.txtProxyURI.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProxyURI.Location = New System.Drawing.Point(11, 54)
        Me.txtProxyURI.Name = "txtProxyURI"
        Me.txtProxyURI.Size = New System.Drawing.Size(267, 22)
        Me.txtProxyURI.TabIndex = 2
        '
        'chkEnableProxy
        '
        Me.chkEnableProxy.AutoSize = True
        Me.chkEnableProxy.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnableProxy.Location = New System.Drawing.Point(11, 17)
        Me.chkEnableProxy.Name = "chkEnableProxy"
        Me.chkEnableProxy.Size = New System.Drawing.Size(91, 17)
        Me.chkEnableProxy.TabIndex = 0
        Me.chkEnableProxy.Text = "Enable Proxy"
        Me.chkEnableProxy.UseVisualStyleBackColor = True
        '
        'gbMovieBackdropsFolder
        '
        Me.gbMovieBackdropsFolder.Controls.Add(Me.chkAutoBD)
        Me.gbMovieBackdropsFolder.Controls.Add(Me.btnBrowseBackdrops)
        Me.gbMovieBackdropsFolder.Controls.Add(Me.txtBDPath)
        Me.gbMovieBackdropsFolder.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieBackdropsFolder.Location = New System.Drawing.Point(5, 412)
        Me.gbMovieBackdropsFolder.Name = "gbMovieBackdropsFolder"
        Me.gbMovieBackdropsFolder.Size = New System.Drawing.Size(212, 85)
        Me.gbMovieBackdropsFolder.TabIndex = 6
        Me.gbMovieBackdropsFolder.TabStop = False
        Me.gbMovieBackdropsFolder.Text = "Backdrops Folder"
        '
        'chkAutoBD
        '
        Me.chkAutoBD.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoBD.Location = New System.Drawing.Point(6, 49)
        Me.chkAutoBD.Name = "chkAutoBD"
        Me.chkAutoBD.Size = New System.Drawing.Size(200, 33)
        Me.chkAutoBD.TabIndex = 2
        Me.chkAutoBD.Text = "Automatically Save Fanart To Backdrops Folder"
        Me.chkAutoBD.UseVisualStyleBackColor = True
        '
        'btnBrowseBackdrops
        '
        Me.btnBrowseBackdrops.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnBrowseBackdrops.Location = New System.Drawing.Point(181, 21)
        Me.btnBrowseBackdrops.Name = "btnBrowseBackdrops"
        Me.btnBrowseBackdrops.Size = New System.Drawing.Size(25, 22)
        Me.btnBrowseBackdrops.TabIndex = 1
        Me.btnBrowseBackdrops.Text = "..."
        Me.btnBrowseBackdrops.UseVisualStyleBackColor = True
        '
        'txtBDPath
        '
        Me.txtBDPath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBDPath.Location = New System.Drawing.Point(6, 21)
        Me.txtBDPath.Name = "txtBDPath"
        Me.txtBDPath.Size = New System.Drawing.Size(169, 22)
        Me.txtBDPath.TabIndex = 0
        '
        'lblCurrent
        '
        Me.lblCurrent.BackColor = System.Drawing.Color.SteelBlue
        Me.lblCurrent.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrent.ForeColor = System.Drawing.Color.White
        Me.lblCurrent.Location = New System.Drawing.Point(26, -1)
        Me.lblCurrent.Name = "lblCurrent"
        Me.lblCurrent.Size = New System.Drawing.Size(489, 25)
        Me.lblCurrent.TabIndex = 0
        Me.lblCurrent.Text = "General"
        '
        'pnlCurrent
        '
        Me.pnlCurrent.BackColor = System.Drawing.Color.SteelBlue
        Me.pnlCurrent.Location = New System.Drawing.Point(516, 119)
        Me.pnlCurrent.Name = "pnlCurrent"
        Me.pnlCurrent.Size = New System.Drawing.Size(487, 25)
        Me.pnlCurrent.TabIndex = 6
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.SteelBlue
        Me.Panel1.Controls.Add(Me.pbCurrent)
        Me.Panel1.Controls.Add(Me.lblCurrent)
        Me.Panel1.Location = New System.Drawing.Point(5, 119)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(515, 25)
        Me.Panel1.TabIndex = 5
        '
        'pbCurrent
        '
        Me.pbCurrent.Location = New System.Drawing.Point(2, 0)
        Me.pbCurrent.Name = "pbCurrent"
        Me.pbCurrent.Size = New System.Drawing.Size(24, 24)
        Me.pbCurrent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbCurrent.TabIndex = 2
        Me.pbCurrent.TabStop = False
        '
        'pnlMovieSources
        '
        Me.pnlMovieSources.BackColor = System.Drawing.Color.White
        Me.pnlMovieSources.Controls.Add(Me.gbMovieSetsFolder)
        Me.pnlMovieSources.Controls.Add(Me.gbMovieFileNaming)
        Me.pnlMovieSources.Controls.Add(Me.gbMovieBackdropsFolder)
        Me.pnlMovieSources.Controls.Add(Me.btnEditSource)
        Me.pnlMovieSources.Controls.Add(Me.gbMovieMiscOptions)
        Me.pnlMovieSources.Controls.Add(Me.lvMovies)
        Me.pnlMovieSources.Controls.Add(Me.btnMovieRem)
        Me.pnlMovieSources.Controls.Add(Me.btnMovieAddFolder)
        Me.pnlMovieSources.Location = New System.Drawing.Point(1300, 410)
        Me.pnlMovieSources.Name = "pnlMovieSources"
        Me.pnlMovieSources.Size = New System.Drawing.Size(750, 500)
        Me.pnlMovieSources.TabIndex = 13
        Me.pnlMovieSources.Visible = False
        '
        'gbMovieSetsFolder
        '
        Me.gbMovieSetsFolder.Controls.Add(Me.btnBrowseMoviesets)
        Me.gbMovieSetsFolder.Controls.Add(Me.txtMoviesetsPath)
        Me.gbMovieSetsFolder.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieSetsFolder.Location = New System.Drawing.Point(5, 348)
        Me.gbMovieSetsFolder.Name = "gbMovieSetsFolder"
        Me.gbMovieSetsFolder.Size = New System.Drawing.Size(212, 58)
        Me.gbMovieSetsFolder.TabIndex = 7
        Me.gbMovieSetsFolder.TabStop = False
        Me.gbMovieSetsFolder.Text = "MovieSets Folder"
        '
        'btnBrowseMoviesets
        '
        Me.btnBrowseMoviesets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnBrowseMoviesets.Location = New System.Drawing.Point(181, 21)
        Me.btnBrowseMoviesets.Name = "btnBrowseMoviesets"
        Me.btnBrowseMoviesets.Size = New System.Drawing.Size(25, 22)
        Me.btnBrowseMoviesets.TabIndex = 1
        Me.btnBrowseMoviesets.Text = "..."
        Me.btnBrowseMoviesets.UseVisualStyleBackColor = True
        '
        'txtMoviesetsPath
        '
        Me.txtMoviesetsPath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMoviesetsPath.Location = New System.Drawing.Point(6, 21)
        Me.txtMoviesetsPath.Name = "txtMoviesetsPath"
        Me.txtMoviesetsPath.Size = New System.Drawing.Size(169, 22)
        Me.txtMoviesetsPath.TabIndex = 0
        '
        'gbMovieFileNaming
        '
        Me.gbMovieFileNaming.Controls.Add(Me.tbcMovieFileNaming)
        Me.gbMovieFileNaming.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieFileNaming.Location = New System.Drawing.Point(223, 113)
        Me.gbMovieFileNaming.Name = "gbMovieFileNaming"
        Me.gbMovieFileNaming.Size = New System.Drawing.Size(521, 384)
        Me.gbMovieFileNaming.TabIndex = 8
        Me.gbMovieFileNaming.TabStop = False
        Me.gbMovieFileNaming.Text = "File Naming"
        '
        'tbcMovieFileNaming
        '
        Me.tbcMovieFileNaming.Controls.Add(Me.tpMovieFileNamingXBMC)
        Me.tbcMovieFileNaming.Controls.Add(Me.tpMovieFileNamingNMT)
        Me.tbcMovieFileNaming.Controls.Add(Me.tpMovieFileNamingExpert)
        Me.tbcMovieFileNaming.Location = New System.Drawing.Point(6, 18)
        Me.tbcMovieFileNaming.Name = "tbcMovieFileNaming"
        Me.tbcMovieFileNaming.SelectedIndex = 0
        Me.tbcMovieFileNaming.Size = New System.Drawing.Size(513, 362)
        Me.tbcMovieFileNaming.TabIndex = 7
        '
        'tpMovieFileNamingXBMC
        '
        Me.tpMovieFileNamingXBMC.Controls.Add(Me.gbMovieXBMCOptional)
        Me.tpMovieFileNamingXBMC.Controls.Add(Me.gbMovieEden)
        Me.tpMovieFileNamingXBMC.Controls.Add(Me.gbMovieFrodo)
        Me.tpMovieFileNamingXBMC.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieFileNamingXBMC.Name = "tpMovieFileNamingXBMC"
        Me.tpMovieFileNamingXBMC.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMovieFileNamingXBMC.Size = New System.Drawing.Size(505, 336)
        Me.tpMovieFileNamingXBMC.TabIndex = 1
        Me.tpMovieFileNamingXBMC.Text = "XBMC"
        Me.tpMovieFileNamingXBMC.UseVisualStyleBackColor = True
        '
        'gbMovieXBMCOptional
        '
        Me.gbMovieXBMCOptional.Controls.Add(Me.chkMovieXBMCProtectVTSBDMV)
        Me.gbMovieXBMCOptional.Controls.Add(Me.chkMovieXBMCTrailerFormat)
        Me.gbMovieXBMCOptional.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieXBMCOptional.Location = New System.Drawing.Point(238, 6)
        Me.gbMovieXBMCOptional.Name = "gbMovieXBMCOptional"
        Me.gbMovieXBMCOptional.Size = New System.Drawing.Size(261, 66)
        Me.gbMovieXBMCOptional.TabIndex = 2
        Me.gbMovieXBMCOptional.TabStop = False
        Me.gbMovieXBMCOptional.Text = "Optional Settings"
        '
        'chkMovieXBMCProtectVTSBDMV
        '
        Me.chkMovieXBMCProtectVTSBDMV.AutoSize = True
        Me.chkMovieXBMCProtectVTSBDMV.Enabled = False
        Me.chkMovieXBMCProtectVTSBDMV.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieXBMCProtectVTSBDMV.Name = "chkMovieXBMCProtectVTSBDMV"
        Me.chkMovieXBMCProtectVTSBDMV.Size = New System.Drawing.Size(222, 17)
        Me.chkMovieXBMCProtectVTSBDMV.TabIndex = 1
        Me.chkMovieXBMCProtectVTSBDMV.Text = "Protect VIDEO_TS and BDMV Structure"
        Me.chkMovieXBMCProtectVTSBDMV.UseVisualStyleBackColor = True
        '
        'chkMovieXBMCTrailerFormat
        '
        Me.chkMovieXBMCTrailerFormat.AutoSize = True
        Me.chkMovieXBMCTrailerFormat.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieXBMCTrailerFormat.Name = "chkMovieXBMCTrailerFormat"
        Me.chkMovieXBMCTrailerFormat.Size = New System.Drawing.Size(179, 17)
        Me.chkMovieXBMCTrailerFormat.TabIndex = 0
        Me.chkMovieXBMCTrailerFormat.Text = "YouTube Plugin Trailer Format"
        Me.chkMovieXBMCTrailerFormat.UseVisualStyleBackColor = True
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
        Me.gbMovieEden.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieEden.Location = New System.Drawing.Point(122, 6)
        Me.gbMovieEden.Name = "gbMovieEden"
        Me.gbMovieEden.Size = New System.Drawing.Size(110, 324)
        Me.gbMovieEden.TabIndex = 1
        Me.gbMovieEden.TabStop = False
        Me.gbMovieEden.Text = "Eden"
        '
        'chkMovieExtrafanartsEden
        '
        Me.chkMovieExtrafanartsEden.AutoSize = True
        Me.chkMovieExtrafanartsEden.Enabled = False
        Me.chkMovieExtrafanartsEden.Location = New System.Drawing.Point(6, 159)
        Me.chkMovieExtrafanartsEden.Name = "chkMovieExtrafanartsEden"
        Me.chkMovieExtrafanartsEden.Size = New System.Drawing.Size(87, 17)
        Me.chkMovieExtrafanartsEden.TabIndex = 20
        Me.chkMovieExtrafanartsEden.Text = "Extrafanarts"
        Me.chkMovieExtrafanartsEden.UseVisualStyleBackColor = True
        '
        'chkMovieExtrathumbsEden
        '
        Me.chkMovieExtrathumbsEden.AutoSize = True
        Me.chkMovieExtrathumbsEden.Enabled = False
        Me.chkMovieExtrathumbsEden.Location = New System.Drawing.Point(6, 136)
        Me.chkMovieExtrathumbsEden.Name = "chkMovieExtrathumbsEden"
        Me.chkMovieExtrathumbsEden.Size = New System.Drawing.Size(90, 17)
        Me.chkMovieExtrathumbsEden.TabIndex = 19
        Me.chkMovieExtrathumbsEden.Text = "Extrathumbs"
        Me.chkMovieExtrathumbsEden.UseVisualStyleBackColor = True
        '
        'chkMovieUseEden
        '
        Me.chkMovieUseEden.AutoSize = True
        Me.chkMovieUseEden.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieUseEden.Name = "chkMovieUseEden"
        Me.chkMovieUseEden.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieUseEden.TabIndex = 18
        Me.chkMovieUseEden.Text = "Use"
        Me.chkMovieUseEden.UseVisualStyleBackColor = True
        '
        'chkMovieActorThumbsEden
        '
        Me.chkMovieActorThumbsEden.AutoSize = True
        Me.chkMovieActorThumbsEden.Enabled = False
        Me.chkMovieActorThumbsEden.Location = New System.Drawing.Point(6, 113)
        Me.chkMovieActorThumbsEden.Name = "chkMovieActorThumbsEden"
        Me.chkMovieActorThumbsEden.Size = New System.Drawing.Size(96, 17)
        Me.chkMovieActorThumbsEden.TabIndex = 17
        Me.chkMovieActorThumbsEden.Text = "Actor Thumbs"
        Me.chkMovieActorThumbsEden.UseVisualStyleBackColor = True
        '
        'chkMovieTrailerEden
        '
        Me.chkMovieTrailerEden.AutoSize = True
        Me.chkMovieTrailerEden.Enabled = False
        Me.chkMovieTrailerEden.Location = New System.Drawing.Point(6, 182)
        Me.chkMovieTrailerEden.Name = "chkMovieTrailerEden"
        Me.chkMovieTrailerEden.Size = New System.Drawing.Size(57, 17)
        Me.chkMovieTrailerEden.TabIndex = 16
        Me.chkMovieTrailerEden.Text = "Trailer"
        Me.chkMovieTrailerEden.UseVisualStyleBackColor = True
        '
        'chkMovieFanartEden
        '
        Me.chkMovieFanartEden.AutoSize = True
        Me.chkMovieFanartEden.Enabled = False
        Me.chkMovieFanartEden.Location = New System.Drawing.Point(6, 90)
        Me.chkMovieFanartEden.Name = "chkMovieFanartEden"
        Me.chkMovieFanartEden.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieFanartEden.TabIndex = 15
        Me.chkMovieFanartEden.Text = "Fanart"
        Me.chkMovieFanartEden.UseVisualStyleBackColor = True
        '
        'chkMoviePosterEden
        '
        Me.chkMoviePosterEden.AutoSize = True
        Me.chkMoviePosterEden.Enabled = False
        Me.chkMoviePosterEden.Location = New System.Drawing.Point(6, 67)
        Me.chkMoviePosterEden.Name = "chkMoviePosterEden"
        Me.chkMoviePosterEden.Size = New System.Drawing.Size(58, 17)
        Me.chkMoviePosterEden.TabIndex = 14
        Me.chkMoviePosterEden.Text = "Poster"
        Me.chkMoviePosterEden.UseVisualStyleBackColor = True
        '
        'chkMovieNFOEden
        '
        Me.chkMovieNFOEden.AutoSize = True
        Me.chkMovieNFOEden.Enabled = False
        Me.chkMovieNFOEden.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieNFOEden.Name = "chkMovieNFOEden"
        Me.chkMovieNFOEden.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieNFOEden.TabIndex = 13
        Me.chkMovieNFOEden.Text = "NFO"
        Me.chkMovieNFOEden.UseVisualStyleBackColor = True
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
        Me.gbMovieFrodo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieFrodo.Location = New System.Drawing.Point(6, 6)
        Me.gbMovieFrodo.Name = "gbMovieFrodo"
        Me.gbMovieFrodo.Size = New System.Drawing.Size(110, 324)
        Me.gbMovieFrodo.TabIndex = 0
        Me.gbMovieFrodo.TabStop = False
        Me.gbMovieFrodo.Text = "Frodo"
        '
        'chkMovieExtrafanartsFrodo
        '
        Me.chkMovieExtrafanartsFrodo.AutoSize = True
        Me.chkMovieExtrafanartsFrodo.Enabled = False
        Me.chkMovieExtrafanartsFrodo.Location = New System.Drawing.Point(6, 159)
        Me.chkMovieExtrafanartsFrodo.Name = "chkMovieExtrafanartsFrodo"
        Me.chkMovieExtrafanartsFrodo.Size = New System.Drawing.Size(87, 17)
        Me.chkMovieExtrafanartsFrodo.TabIndex = 12
        Me.chkMovieExtrafanartsFrodo.Text = "Extrafanarts"
        Me.chkMovieExtrafanartsFrodo.UseVisualStyleBackColor = True
        '
        'chkMovieExtrathumbsFrodo
        '
        Me.chkMovieExtrathumbsFrodo.AutoSize = True
        Me.chkMovieExtrathumbsFrodo.Enabled = False
        Me.chkMovieExtrathumbsFrodo.Location = New System.Drawing.Point(6, 136)
        Me.chkMovieExtrathumbsFrodo.Name = "chkMovieExtrathumbsFrodo"
        Me.chkMovieExtrathumbsFrodo.Size = New System.Drawing.Size(90, 17)
        Me.chkMovieExtrathumbsFrodo.TabIndex = 11
        Me.chkMovieExtrathumbsFrodo.Text = "Extrathumbs"
        Me.chkMovieExtrathumbsFrodo.UseVisualStyleBackColor = True
        '
        'chkMovieUseFrodo
        '
        Me.chkMovieUseFrodo.AutoSize = True
        Me.chkMovieUseFrodo.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieUseFrodo.Name = "chkMovieUseFrodo"
        Me.chkMovieUseFrodo.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieUseFrodo.TabIndex = 10
        Me.chkMovieUseFrodo.Text = "Use"
        Me.chkMovieUseFrodo.UseVisualStyleBackColor = True
        '
        'chkMovieLandscapeFrodo
        '
        Me.chkMovieLandscapeFrodo.AutoSize = True
        Me.chkMovieLandscapeFrodo.Enabled = False
        Me.chkMovieLandscapeFrodo.Location = New System.Drawing.Point(6, 297)
        Me.chkMovieLandscapeFrodo.Name = "chkMovieLandscapeFrodo"
        Me.chkMovieLandscapeFrodo.Size = New System.Drawing.Size(80, 17)
        Me.chkMovieLandscapeFrodo.TabIndex = 9
        Me.chkMovieLandscapeFrodo.Text = "Landscape"
        Me.chkMovieLandscapeFrodo.UseVisualStyleBackColor = True
        '
        'chkMovieBannerFrodo
        '
        Me.chkMovieBannerFrodo.AutoSize = True
        Me.chkMovieBannerFrodo.Enabled = False
        Me.chkMovieBannerFrodo.Location = New System.Drawing.Point(6, 205)
        Me.chkMovieBannerFrodo.Name = "chkMovieBannerFrodo"
        Me.chkMovieBannerFrodo.Size = New System.Drawing.Size(63, 17)
        Me.chkMovieBannerFrodo.TabIndex = 8
        Me.chkMovieBannerFrodo.Text = "Banner"
        Me.chkMovieBannerFrodo.UseVisualStyleBackColor = True
        '
        'chkMovieDiscArtFrodo
        '
        Me.chkMovieDiscArtFrodo.AutoSize = True
        Me.chkMovieDiscArtFrodo.Enabled = False
        Me.chkMovieDiscArtFrodo.Location = New System.Drawing.Point(6, 274)
        Me.chkMovieDiscArtFrodo.Name = "chkMovieDiscArtFrodo"
        Me.chkMovieDiscArtFrodo.Size = New System.Drawing.Size(62, 17)
        Me.chkMovieDiscArtFrodo.TabIndex = 7
        Me.chkMovieDiscArtFrodo.Text = "DiscArt"
        Me.chkMovieDiscArtFrodo.UseVisualStyleBackColor = True
        '
        'chkMovieClearArtFrodo
        '
        Me.chkMovieClearArtFrodo.AutoSize = True
        Me.chkMovieClearArtFrodo.Enabled = False
        Me.chkMovieClearArtFrodo.Location = New System.Drawing.Point(6, 251)
        Me.chkMovieClearArtFrodo.Name = "chkMovieClearArtFrodo"
        Me.chkMovieClearArtFrodo.Size = New System.Drawing.Size(67, 17)
        Me.chkMovieClearArtFrodo.TabIndex = 6
        Me.chkMovieClearArtFrodo.Text = "ClearArt"
        Me.chkMovieClearArtFrodo.UseVisualStyleBackColor = True
        '
        'chkMovieClearLogoFrodo
        '
        Me.chkMovieClearLogoFrodo.AutoSize = True
        Me.chkMovieClearLogoFrodo.Enabled = False
        Me.chkMovieClearLogoFrodo.Location = New System.Drawing.Point(6, 228)
        Me.chkMovieClearLogoFrodo.Name = "chkMovieClearLogoFrodo"
        Me.chkMovieClearLogoFrodo.Size = New System.Drawing.Size(78, 17)
        Me.chkMovieClearLogoFrodo.TabIndex = 5
        Me.chkMovieClearLogoFrodo.Text = "ClearLogo"
        Me.chkMovieClearLogoFrodo.UseVisualStyleBackColor = True
        '
        'chkMovieActorThumbsFrodo
        '
        Me.chkMovieActorThumbsFrodo.AutoSize = True
        Me.chkMovieActorThumbsFrodo.Enabled = False
        Me.chkMovieActorThumbsFrodo.Location = New System.Drawing.Point(6, 113)
        Me.chkMovieActorThumbsFrodo.Name = "chkMovieActorThumbsFrodo"
        Me.chkMovieActorThumbsFrodo.Size = New System.Drawing.Size(96, 17)
        Me.chkMovieActorThumbsFrodo.TabIndex = 4
        Me.chkMovieActorThumbsFrodo.Text = "Actor Thumbs"
        Me.chkMovieActorThumbsFrodo.UseVisualStyleBackColor = True
        '
        'chkMovieTrailerFrodo
        '
        Me.chkMovieTrailerFrodo.AutoSize = True
        Me.chkMovieTrailerFrodo.Enabled = False
        Me.chkMovieTrailerFrodo.Location = New System.Drawing.Point(6, 182)
        Me.chkMovieTrailerFrodo.Name = "chkMovieTrailerFrodo"
        Me.chkMovieTrailerFrodo.Size = New System.Drawing.Size(57, 17)
        Me.chkMovieTrailerFrodo.TabIndex = 3
        Me.chkMovieTrailerFrodo.Text = "Trailer"
        Me.chkMovieTrailerFrodo.UseVisualStyleBackColor = True
        '
        'chkMovieFanartFrodo
        '
        Me.chkMovieFanartFrodo.AutoSize = True
        Me.chkMovieFanartFrodo.Enabled = False
        Me.chkMovieFanartFrodo.Location = New System.Drawing.Point(6, 90)
        Me.chkMovieFanartFrodo.Name = "chkMovieFanartFrodo"
        Me.chkMovieFanartFrodo.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieFanartFrodo.TabIndex = 2
        Me.chkMovieFanartFrodo.Text = "Fanart"
        Me.chkMovieFanartFrodo.UseVisualStyleBackColor = True
        '
        'chkMoviePosterFrodo
        '
        Me.chkMoviePosterFrodo.AutoSize = True
        Me.chkMoviePosterFrodo.Enabled = False
        Me.chkMoviePosterFrodo.Location = New System.Drawing.Point(6, 67)
        Me.chkMoviePosterFrodo.Name = "chkMoviePosterFrodo"
        Me.chkMoviePosterFrodo.Size = New System.Drawing.Size(58, 17)
        Me.chkMoviePosterFrodo.TabIndex = 1
        Me.chkMoviePosterFrodo.Text = "Poster"
        Me.chkMoviePosterFrodo.UseVisualStyleBackColor = True
        '
        'chkMovieNFOFrodo
        '
        Me.chkMovieNFOFrodo.AutoSize = True
        Me.chkMovieNFOFrodo.Enabled = False
        Me.chkMovieNFOFrodo.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieNFOFrodo.Name = "chkMovieNFOFrodo"
        Me.chkMovieNFOFrodo.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieNFOFrodo.TabIndex = 0
        Me.chkMovieNFOFrodo.Text = "NFO"
        Me.chkMovieNFOFrodo.UseVisualStyleBackColor = True
        '
        'tpMovieFileNamingNMT
        '
        Me.tpMovieFileNamingNMT.Controls.Add(Me.gbMovieNMTOptional)
        Me.tpMovieFileNamingNMT.Controls.Add(Me.gbMovieNMJ)
        Me.tpMovieFileNamingNMT.Controls.Add(Me.gbMovieYAMJ)
        Me.tpMovieFileNamingNMT.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieFileNamingNMT.Name = "tpMovieFileNamingNMT"
        Me.tpMovieFileNamingNMT.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMovieFileNamingNMT.Size = New System.Drawing.Size(505, 336)
        Me.tpMovieFileNamingNMT.TabIndex = 3
        Me.tpMovieFileNamingNMT.Text = "NMT"
        Me.tpMovieFileNamingNMT.UseVisualStyleBackColor = True
        '
        'gbMovieNMTOptional
        '
        Me.gbMovieNMTOptional.Controls.Add(Me.btnMovieBrowseWatchedFiles)
        Me.gbMovieNMTOptional.Controls.Add(Me.txtMovieYAMJWatchedFolder)
        Me.gbMovieNMTOptional.Controls.Add(Me.chkMovieYAMJWatchedFile)
        Me.gbMovieNMTOptional.Location = New System.Drawing.Point(238, 6)
        Me.gbMovieNMTOptional.Name = "gbMovieNMTOptional"
        Me.gbMovieNMTOptional.Size = New System.Drawing.Size(261, 84)
        Me.gbMovieNMTOptional.TabIndex = 18
        Me.gbMovieNMTOptional.TabStop = False
        Me.gbMovieNMTOptional.Text = "Optional Settings"
        '
        'btnMovieBrowseWatchedFiles
        '
        Me.btnMovieBrowseWatchedFiles.Enabled = False
        Me.btnMovieBrowseWatchedFiles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMovieBrowseWatchedFiles.Location = New System.Drawing.Point(230, 44)
        Me.btnMovieBrowseWatchedFiles.Name = "btnMovieBrowseWatchedFiles"
        Me.btnMovieBrowseWatchedFiles.Size = New System.Drawing.Size(25, 22)
        Me.btnMovieBrowseWatchedFiles.TabIndex = 2
        Me.btnMovieBrowseWatchedFiles.Text = "..."
        Me.btnMovieBrowseWatchedFiles.UseVisualStyleBackColor = True
        '
        'txtMovieYAMJWatchedFolder
        '
        Me.txtMovieYAMJWatchedFolder.Enabled = False
        Me.txtMovieYAMJWatchedFolder.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMovieYAMJWatchedFolder.Location = New System.Drawing.Point(6, 44)
        Me.txtMovieYAMJWatchedFolder.Name = "txtMovieYAMJWatchedFolder"
        Me.txtMovieYAMJWatchedFolder.Size = New System.Drawing.Size(218, 22)
        Me.txtMovieYAMJWatchedFolder.TabIndex = 1
        '
        'chkMovieYAMJWatchedFile
        '
        Me.chkMovieYAMJWatchedFile.AutoSize = True
        Me.chkMovieYAMJWatchedFile.Enabled = False
        Me.chkMovieYAMJWatchedFile.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkMovieYAMJWatchedFile.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieYAMJWatchedFile.Name = "chkMovieYAMJWatchedFile"
        Me.chkMovieYAMJWatchedFile.Size = New System.Drawing.Size(121, 17)
        Me.chkMovieYAMJWatchedFile.TabIndex = 0
        Me.chkMovieYAMJWatchedFile.Text = "Use .watched Files"
        Me.chkMovieYAMJWatchedFile.UseVisualStyleBackColor = True
        '
        'gbMovieNMJ
        '
        Me.gbMovieNMJ.Controls.Add(Me.chkMovieUseNMJ)
        Me.gbMovieNMJ.Controls.Add(Me.chkMovieBannerNMJ)
        Me.gbMovieNMJ.Controls.Add(Me.chkMovieTrailerNMJ)
        Me.gbMovieNMJ.Controls.Add(Me.chkMovieFanartNMJ)
        Me.gbMovieNMJ.Controls.Add(Me.chkMoviePosterNMJ)
        Me.gbMovieNMJ.Controls.Add(Me.chkMovieNFONMJ)
        Me.gbMovieNMJ.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieNMJ.Location = New System.Drawing.Point(122, 6)
        Me.gbMovieNMJ.Name = "gbMovieNMJ"
        Me.gbMovieNMJ.Size = New System.Drawing.Size(110, 324)
        Me.gbMovieNMJ.TabIndex = 17
        Me.gbMovieNMJ.TabStop = False
        Me.gbMovieNMJ.Text = "NMJ"
        '
        'chkMovieUseNMJ
        '
        Me.chkMovieUseNMJ.AutoSize = True
        Me.chkMovieUseNMJ.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieUseNMJ.Name = "chkMovieUseNMJ"
        Me.chkMovieUseNMJ.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieUseNMJ.TabIndex = 16
        Me.chkMovieUseNMJ.Text = "Use"
        Me.chkMovieUseNMJ.UseVisualStyleBackColor = True
        '
        'chkMovieBannerNMJ
        '
        Me.chkMovieBannerNMJ.AutoSize = True
        Me.chkMovieBannerNMJ.Enabled = False
        Me.chkMovieBannerNMJ.Location = New System.Drawing.Point(6, 113)
        Me.chkMovieBannerNMJ.Name = "chkMovieBannerNMJ"
        Me.chkMovieBannerNMJ.Size = New System.Drawing.Size(63, 17)
        Me.chkMovieBannerNMJ.TabIndex = 15
        Me.chkMovieBannerNMJ.Text = "Banner"
        Me.chkMovieBannerNMJ.UseVisualStyleBackColor = True
        '
        'chkMovieTrailerNMJ
        '
        Me.chkMovieTrailerNMJ.AutoSize = True
        Me.chkMovieTrailerNMJ.Enabled = False
        Me.chkMovieTrailerNMJ.Location = New System.Drawing.Point(6, 136)
        Me.chkMovieTrailerNMJ.Name = "chkMovieTrailerNMJ"
        Me.chkMovieTrailerNMJ.Size = New System.Drawing.Size(57, 17)
        Me.chkMovieTrailerNMJ.TabIndex = 14
        Me.chkMovieTrailerNMJ.Text = "Trailer"
        Me.chkMovieTrailerNMJ.UseVisualStyleBackColor = True
        '
        'chkMovieFanartNMJ
        '
        Me.chkMovieFanartNMJ.AutoSize = True
        Me.chkMovieFanartNMJ.Enabled = False
        Me.chkMovieFanartNMJ.Location = New System.Drawing.Point(6, 90)
        Me.chkMovieFanartNMJ.Name = "chkMovieFanartNMJ"
        Me.chkMovieFanartNMJ.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieFanartNMJ.TabIndex = 13
        Me.chkMovieFanartNMJ.Text = "Fanart"
        Me.chkMovieFanartNMJ.UseVisualStyleBackColor = True
        '
        'chkMoviePosterNMJ
        '
        Me.chkMoviePosterNMJ.AutoSize = True
        Me.chkMoviePosterNMJ.Enabled = False
        Me.chkMoviePosterNMJ.Location = New System.Drawing.Point(6, 67)
        Me.chkMoviePosterNMJ.Name = "chkMoviePosterNMJ"
        Me.chkMoviePosterNMJ.Size = New System.Drawing.Size(58, 17)
        Me.chkMoviePosterNMJ.TabIndex = 12
        Me.chkMoviePosterNMJ.Text = "Poster"
        Me.chkMoviePosterNMJ.UseVisualStyleBackColor = True
        '
        'chkMovieNFONMJ
        '
        Me.chkMovieNFONMJ.AutoSize = True
        Me.chkMovieNFONMJ.Enabled = False
        Me.chkMovieNFONMJ.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieNFONMJ.Name = "chkMovieNFONMJ"
        Me.chkMovieNFONMJ.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieNFONMJ.TabIndex = 11
        Me.chkMovieNFONMJ.Text = "NFO"
        Me.chkMovieNFONMJ.UseVisualStyleBackColor = True
        '
        'gbMovieYAMJ
        '
        Me.gbMovieYAMJ.Controls.Add(Me.chkMovieUseYAMJ)
        Me.gbMovieYAMJ.Controls.Add(Me.chkMovieBannerYAMJ)
        Me.gbMovieYAMJ.Controls.Add(Me.chkMovieTrailerYAMJ)
        Me.gbMovieYAMJ.Controls.Add(Me.chkMovieFanartYAMJ)
        Me.gbMovieYAMJ.Controls.Add(Me.chkMoviePosterYAMJ)
        Me.gbMovieYAMJ.Controls.Add(Me.chkMovieNFOYAMJ)
        Me.gbMovieYAMJ.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieYAMJ.Location = New System.Drawing.Point(6, 6)
        Me.gbMovieYAMJ.Name = "gbMovieYAMJ"
        Me.gbMovieYAMJ.Size = New System.Drawing.Size(110, 324)
        Me.gbMovieYAMJ.TabIndex = 3
        Me.gbMovieYAMJ.TabStop = False
        Me.gbMovieYAMJ.Text = "YAMJ"
        '
        'chkMovieUseYAMJ
        '
        Me.chkMovieUseYAMJ.AutoSize = True
        Me.chkMovieUseYAMJ.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieUseYAMJ.Name = "chkMovieUseYAMJ"
        Me.chkMovieUseYAMJ.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieUseYAMJ.TabIndex = 16
        Me.chkMovieUseYAMJ.Text = "Use"
        Me.chkMovieUseYAMJ.UseVisualStyleBackColor = True
        '
        'chkMovieBannerYAMJ
        '
        Me.chkMovieBannerYAMJ.AutoSize = True
        Me.chkMovieBannerYAMJ.Enabled = False
        Me.chkMovieBannerYAMJ.Location = New System.Drawing.Point(6, 113)
        Me.chkMovieBannerYAMJ.Name = "chkMovieBannerYAMJ"
        Me.chkMovieBannerYAMJ.Size = New System.Drawing.Size(63, 17)
        Me.chkMovieBannerYAMJ.TabIndex = 15
        Me.chkMovieBannerYAMJ.Text = "Banner"
        Me.chkMovieBannerYAMJ.UseVisualStyleBackColor = True
        '
        'chkMovieTrailerYAMJ
        '
        Me.chkMovieTrailerYAMJ.AutoSize = True
        Me.chkMovieTrailerYAMJ.Enabled = False
        Me.chkMovieTrailerYAMJ.Location = New System.Drawing.Point(6, 136)
        Me.chkMovieTrailerYAMJ.Name = "chkMovieTrailerYAMJ"
        Me.chkMovieTrailerYAMJ.Size = New System.Drawing.Size(57, 17)
        Me.chkMovieTrailerYAMJ.TabIndex = 14
        Me.chkMovieTrailerYAMJ.Text = "Trailer"
        Me.chkMovieTrailerYAMJ.UseVisualStyleBackColor = True
        '
        'chkMovieFanartYAMJ
        '
        Me.chkMovieFanartYAMJ.AutoSize = True
        Me.chkMovieFanartYAMJ.Enabled = False
        Me.chkMovieFanartYAMJ.Location = New System.Drawing.Point(6, 90)
        Me.chkMovieFanartYAMJ.Name = "chkMovieFanartYAMJ"
        Me.chkMovieFanartYAMJ.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieFanartYAMJ.TabIndex = 13
        Me.chkMovieFanartYAMJ.Text = "Fanart"
        Me.chkMovieFanartYAMJ.UseVisualStyleBackColor = True
        '
        'chkMoviePosterYAMJ
        '
        Me.chkMoviePosterYAMJ.AutoSize = True
        Me.chkMoviePosterYAMJ.Enabled = False
        Me.chkMoviePosterYAMJ.Location = New System.Drawing.Point(6, 67)
        Me.chkMoviePosterYAMJ.Name = "chkMoviePosterYAMJ"
        Me.chkMoviePosterYAMJ.Size = New System.Drawing.Size(58, 17)
        Me.chkMoviePosterYAMJ.TabIndex = 12
        Me.chkMoviePosterYAMJ.Text = "Poster"
        Me.chkMoviePosterYAMJ.UseVisualStyleBackColor = True
        '
        'chkMovieNFOYAMJ
        '
        Me.chkMovieNFOYAMJ.AutoSize = True
        Me.chkMovieNFOYAMJ.Enabled = False
        Me.chkMovieNFOYAMJ.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieNFOYAMJ.Name = "chkMovieNFOYAMJ"
        Me.chkMovieNFOYAMJ.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieNFOYAMJ.TabIndex = 11
        Me.chkMovieNFOYAMJ.Text = "NFO"
        Me.chkMovieNFOYAMJ.UseVisualStyleBackColor = True
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
        Me.tpMovieFileNamingExpert.UseVisualStyleBackColor = True
        '
        'gbMovieExpert
        '
        Me.gbMovieExpert.Controls.Add(Me.tbcMovieFileNamingExpert)
        Me.gbMovieExpert.Controls.Add(Me.chkMovieUseExpert)
        Me.gbMovieExpert.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gbMovieExpert.Location = New System.Drawing.Point(6, 6)
        Me.gbMovieExpert.Name = "gbMovieExpert"
        Me.gbMovieExpert.Size = New System.Drawing.Size(493, 324)
        Me.gbMovieExpert.TabIndex = 7
        Me.gbMovieExpert.TabStop = False
        Me.gbMovieExpert.Text = "Expert Settings"
        '
        'tbcMovieFileNamingExpert
        '
        Me.tbcMovieFileNamingExpert.Controls.Add(Me.tpMovieFileNamingExpertSingle)
        Me.tbcMovieFileNamingExpert.Controls.Add(Me.tpMovieFileNamingExpertMulti)
        Me.tbcMovieFileNamingExpert.Controls.Add(Me.tbMovieFileNamingExpertVTS)
        Me.tbcMovieFileNamingExpert.Controls.Add(Me.tbMovieFileNamingExpertBDMV)
        Me.tbcMovieFileNamingExpert.Location = New System.Drawing.Point(6, 44)
        Me.tbcMovieFileNamingExpert.Name = "tbcMovieFileNamingExpert"
        Me.tbcMovieFileNamingExpert.SelectedIndex = 0
        Me.tbcMovieFileNamingExpert.Size = New System.Drawing.Size(481, 280)
        Me.tbcMovieFileNamingExpert.TabIndex = 2
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
        Me.tpMovieFileNamingExpertSingle.UseVisualStyleBackColor = True
        '
        'gbMovieExpertSingleOptionalSettings
        '
        Me.gbMovieExpertSingleOptionalSettings.Controls.Add(Me.chkMovieUnstackExpertSingle)
        Me.gbMovieExpertSingleOptionalSettings.Controls.Add(Me.chkMovieStackExpertSingle)
        Me.gbMovieExpertSingleOptionalSettings.Controls.Add(Me.chkMovieXBMCTrailerFormatExpertSingle)
        Me.gbMovieExpertSingleOptionalSettings.Location = New System.Drawing.Point(307, 6)
        Me.gbMovieExpertSingleOptionalSettings.Name = "gbMovieExpertSingleOptionalSettings"
        Me.gbMovieExpertSingleOptionalSettings.Size = New System.Drawing.Size(160, 93)
        Me.gbMovieExpertSingleOptionalSettings.TabIndex = 12
        Me.gbMovieExpertSingleOptionalSettings.TabStop = False
        Me.gbMovieExpertSingleOptionalSettings.Text = "Optional Settings"
        '
        'chkMovieUnstackExpertSingle
        '
        Me.chkMovieUnstackExpertSingle.AutoSize = True
        Me.chkMovieUnstackExpertSingle.Enabled = False
        Me.chkMovieUnstackExpertSingle.Location = New System.Drawing.Point(6, 67)
        Me.chkMovieUnstackExpertSingle.Name = "chkMovieUnstackExpertSingle"
        Me.chkMovieUnstackExpertSingle.Size = New System.Drawing.Size(128, 17)
        Me.chkMovieUnstackExpertSingle.TabIndex = 3
        Me.chkMovieUnstackExpertSingle.Text = "also save unstacked"
        Me.chkMovieUnstackExpertSingle.UseVisualStyleBackColor = True
        '
        'chkMovieStackExpertSingle
        '
        Me.chkMovieStackExpertSingle.AutoSize = True
        Me.chkMovieStackExpertSingle.Enabled = False
        Me.chkMovieStackExpertSingle.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieStackExpertSingle.Name = "chkMovieStackExpertSingle"
        Me.chkMovieStackExpertSingle.Size = New System.Drawing.Size(116, 17)
        Me.chkMovieStackExpertSingle.TabIndex = 2
        Me.chkMovieStackExpertSingle.Text = "Stack <filename>"
        Me.chkMovieStackExpertSingle.UseVisualStyleBackColor = True
        '
        'chkMovieXBMCTrailerFormatExpertSingle
        '
        Me.chkMovieXBMCTrailerFormatExpertSingle.AutoSize = True
        Me.chkMovieXBMCTrailerFormatExpertSingle.Enabled = False
        Me.chkMovieXBMCTrailerFormatExpertSingle.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieXBMCTrailerFormatExpertSingle.Name = "chkMovieXBMCTrailerFormatExpertSingle"
        Me.chkMovieXBMCTrailerFormatExpertSingle.Size = New System.Drawing.Size(129, 17)
        Me.chkMovieXBMCTrailerFormatExpertSingle.TabIndex = 1
        Me.chkMovieXBMCTrailerFormatExpertSingle.Text = "XBMC Trailer Format"
        Me.chkMovieXBMCTrailerFormatExpertSingle.UseVisualStyleBackColor = True
        '
        'gbMovieExpertSingleOptionalImages
        '
        Me.gbMovieExpertSingleOptionalImages.Controls.Add(Me.txtMovieActorThumbsExtExpertSingle)
        Me.gbMovieExpertSingleOptionalImages.Controls.Add(Me.chkMovieActorThumbsExpertSingle)
        Me.gbMovieExpertSingleOptionalImages.Controls.Add(Me.chkMovieExtrafanartsExpertSingle)
        Me.gbMovieExpertSingleOptionalImages.Controls.Add(Me.chkMovieExtrathumbsExpertSingle)
        Me.gbMovieExpertSingleOptionalImages.Location = New System.Drawing.Point(307, 105)
        Me.gbMovieExpertSingleOptionalImages.Name = "gbMovieExpertSingleOptionalImages"
        Me.gbMovieExpertSingleOptionalImages.Size = New System.Drawing.Size(160, 93)
        Me.gbMovieExpertSingleOptionalImages.TabIndex = 13
        Me.gbMovieExpertSingleOptionalImages.TabStop = False
        Me.gbMovieExpertSingleOptionalImages.Text = "Optional Images"
        '
        'txtMovieActorThumbsExtExpertSingle
        '
        Me.txtMovieActorThumbsExtExpertSingle.Enabled = False
        Me.txtMovieActorThumbsExtExpertSingle.Location = New System.Drawing.Point(108, 19)
        Me.txtMovieActorThumbsExtExpertSingle.Name = "txtMovieActorThumbsExtExpertSingle"
        Me.txtMovieActorThumbsExtExpertSingle.Size = New System.Drawing.Size(46, 22)
        Me.txtMovieActorThumbsExtExpertSingle.TabIndex = 2
        '
        'chkMovieActorThumbsExpertSingle
        '
        Me.chkMovieActorThumbsExpertSingle.AutoSize = True
        Me.chkMovieActorThumbsExpertSingle.Enabled = False
        Me.chkMovieActorThumbsExpertSingle.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieActorThumbsExpertSingle.Name = "chkMovieActorThumbsExpertSingle"
        Me.chkMovieActorThumbsExpertSingle.Size = New System.Drawing.Size(96, 17)
        Me.chkMovieActorThumbsExpertSingle.TabIndex = 1
        Me.chkMovieActorThumbsExpertSingle.Text = "Actor Thumbs"
        Me.chkMovieActorThumbsExpertSingle.UseVisualStyleBackColor = True
        '
        'chkMovieExtrafanartsExpertSingle
        '
        Me.chkMovieExtrafanartsExpertSingle.AutoSize = True
        Me.chkMovieExtrafanartsExpertSingle.Enabled = False
        Me.chkMovieExtrafanartsExpertSingle.Location = New System.Drawing.Point(6, 67)
        Me.chkMovieExtrafanartsExpertSingle.Name = "chkMovieExtrafanartsExpertSingle"
        Me.chkMovieExtrafanartsExpertSingle.Size = New System.Drawing.Size(87, 17)
        Me.chkMovieExtrafanartsExpertSingle.TabIndex = 4
        Me.chkMovieExtrafanartsExpertSingle.Text = "Extrafanarts"
        Me.chkMovieExtrafanartsExpertSingle.UseVisualStyleBackColor = True
        '
        'chkMovieExtrathumbsExpertSingle
        '
        Me.chkMovieExtrathumbsExpertSingle.AutoSize = True
        Me.chkMovieExtrathumbsExpertSingle.Enabled = False
        Me.chkMovieExtrathumbsExpertSingle.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieExtrathumbsExpertSingle.Name = "chkMovieExtrathumbsExpertSingle"
        Me.chkMovieExtrathumbsExpertSingle.Size = New System.Drawing.Size(90, 17)
        Me.chkMovieExtrathumbsExpertSingle.TabIndex = 3
        Me.chkMovieExtrathumbsExpertSingle.Text = "Extrathumbs"
        Me.chkMovieExtrathumbsExpertSingle.UseVisualStyleBackColor = True
        '
        'lblMovieClearArtExpertSingle
        '
        Me.lblMovieClearArtExpertSingle.AutoSize = True
        Me.lblMovieClearArtExpertSingle.Location = New System.Drawing.Point(6, 171)
        Me.lblMovieClearArtExpertSingle.Name = "lblMovieClearArtExpertSingle"
        Me.lblMovieClearArtExpertSingle.Size = New System.Drawing.Size(48, 13)
        Me.lblMovieClearArtExpertSingle.TabIndex = 28
        Me.lblMovieClearArtExpertSingle.Text = "ClearArt"
        '
        'txtMoviePosterExpertSingle
        '
        Me.txtMoviePosterExpertSingle.Enabled = False
        Me.txtMoviePosterExpertSingle.Location = New System.Drawing.Point(71, 33)
        Me.txtMoviePosterExpertSingle.Name = "txtMoviePosterExpertSingle"
        Me.txtMoviePosterExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMoviePosterExpertSingle.TabIndex = 4
        '
        'txtMovieFanartExpertSingle
        '
        Me.txtMovieFanartExpertSingle.Enabled = False
        Me.txtMovieFanartExpertSingle.Location = New System.Drawing.Point(71, 60)
        Me.txtMovieFanartExpertSingle.Name = "txtMovieFanartExpertSingle"
        Me.txtMovieFanartExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieFanartExpertSingle.TabIndex = 5
        '
        'txtMovieTrailerExpertSingle
        '
        Me.txtMovieTrailerExpertSingle.Enabled = False
        Me.txtMovieTrailerExpertSingle.Location = New System.Drawing.Point(71, 87)
        Me.txtMovieTrailerExpertSingle.Name = "txtMovieTrailerExpertSingle"
        Me.txtMovieTrailerExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieTrailerExpertSingle.TabIndex = 6
        '
        'txtMovieBannerExpertSingle
        '
        Me.txtMovieBannerExpertSingle.Enabled = False
        Me.txtMovieBannerExpertSingle.Location = New System.Drawing.Point(71, 114)
        Me.txtMovieBannerExpertSingle.Name = "txtMovieBannerExpertSingle"
        Me.txtMovieBannerExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieBannerExpertSingle.TabIndex = 7
        '
        'txtMovieClearLogoExpertSingle
        '
        Me.txtMovieClearLogoExpertSingle.Enabled = False
        Me.txtMovieClearLogoExpertSingle.Location = New System.Drawing.Point(71, 141)
        Me.txtMovieClearLogoExpertSingle.Name = "txtMovieClearLogoExpertSingle"
        Me.txtMovieClearLogoExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieClearLogoExpertSingle.TabIndex = 8
        '
        'txtMovieClearArtExpertSingle
        '
        Me.txtMovieClearArtExpertSingle.Enabled = False
        Me.txtMovieClearArtExpertSingle.Location = New System.Drawing.Point(71, 168)
        Me.txtMovieClearArtExpertSingle.Name = "txtMovieClearArtExpertSingle"
        Me.txtMovieClearArtExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieClearArtExpertSingle.TabIndex = 9
        '
        'txtMovieLandscapeExpertSingle
        '
        Me.txtMovieLandscapeExpertSingle.Enabled = False
        Me.txtMovieLandscapeExpertSingle.Location = New System.Drawing.Point(71, 222)
        Me.txtMovieLandscapeExpertSingle.Name = "txtMovieLandscapeExpertSingle"
        Me.txtMovieLandscapeExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieLandscapeExpertSingle.TabIndex = 11
        '
        'txtMovieDiscArtExpertSingle
        '
        Me.txtMovieDiscArtExpertSingle.Enabled = False
        Me.txtMovieDiscArtExpertSingle.Location = New System.Drawing.Point(71, 195)
        Me.txtMovieDiscArtExpertSingle.Name = "txtMovieDiscArtExpertSingle"
        Me.txtMovieDiscArtExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieDiscArtExpertSingle.TabIndex = 10
        '
        'lblMovieLandscapeExpertSingle
        '
        Me.lblMovieLandscapeExpertSingle.AutoSize = True
        Me.lblMovieLandscapeExpertSingle.Location = New System.Drawing.Point(6, 225)
        Me.lblMovieLandscapeExpertSingle.Name = "lblMovieLandscapeExpertSingle"
        Me.lblMovieLandscapeExpertSingle.Size = New System.Drawing.Size(61, 13)
        Me.lblMovieLandscapeExpertSingle.TabIndex = 19
        Me.lblMovieLandscapeExpertSingle.Text = "Landscape"
        '
        'lblMovieDiscArtExpertSingle
        '
        Me.lblMovieDiscArtExpertSingle.AutoSize = True
        Me.lblMovieDiscArtExpertSingle.Location = New System.Drawing.Point(6, 198)
        Me.lblMovieDiscArtExpertSingle.Name = "lblMovieDiscArtExpertSingle"
        Me.lblMovieDiscArtExpertSingle.Size = New System.Drawing.Size(43, 13)
        Me.lblMovieDiscArtExpertSingle.TabIndex = 18
        Me.lblMovieDiscArtExpertSingle.Text = "DiscArt"
        '
        'lblMovieBannerExpertSingle
        '
        Me.lblMovieBannerExpertSingle.AutoSize = True
        Me.lblMovieBannerExpertSingle.Location = New System.Drawing.Point(6, 117)
        Me.lblMovieBannerExpertSingle.Name = "lblMovieBannerExpertSingle"
        Me.lblMovieBannerExpertSingle.Size = New System.Drawing.Size(44, 13)
        Me.lblMovieBannerExpertSingle.TabIndex = 17
        Me.lblMovieBannerExpertSingle.Text = "Banner"
        '
        'lblMovieTrailerExpertSingle
        '
        Me.lblMovieTrailerExpertSingle.AutoSize = True
        Me.lblMovieTrailerExpertSingle.Location = New System.Drawing.Point(6, 90)
        Me.lblMovieTrailerExpertSingle.Name = "lblMovieTrailerExpertSingle"
        Me.lblMovieTrailerExpertSingle.Size = New System.Drawing.Size(38, 13)
        Me.lblMovieTrailerExpertSingle.TabIndex = 13
        Me.lblMovieTrailerExpertSingle.Text = "Trailer"
        '
        'lblMovieClearLogoExpertSingle
        '
        Me.lblMovieClearLogoExpertSingle.AutoSize = True
        Me.lblMovieClearLogoExpertSingle.Location = New System.Drawing.Point(6, 144)
        Me.lblMovieClearLogoExpertSingle.Name = "lblMovieClearLogoExpertSingle"
        Me.lblMovieClearLogoExpertSingle.Size = New System.Drawing.Size(59, 13)
        Me.lblMovieClearLogoExpertSingle.TabIndex = 12
        Me.lblMovieClearLogoExpertSingle.Text = "ClearLogo"
        '
        'lblMovieFanartExpertSingle
        '
        Me.lblMovieFanartExpertSingle.AutoSize = True
        Me.lblMovieFanartExpertSingle.Location = New System.Drawing.Point(6, 64)
        Me.lblMovieFanartExpertSingle.Name = "lblMovieFanartExpertSingle"
        Me.lblMovieFanartExpertSingle.Size = New System.Drawing.Size(40, 13)
        Me.lblMovieFanartExpertSingle.TabIndex = 11
        Me.lblMovieFanartExpertSingle.Text = "Fanart"
        '
        'lblMoviePosterExpertSingle
        '
        Me.lblMoviePosterExpertSingle.AutoSize = True
        Me.lblMoviePosterExpertSingle.Location = New System.Drawing.Point(6, 36)
        Me.lblMoviePosterExpertSingle.Name = "lblMoviePosterExpertSingle"
        Me.lblMoviePosterExpertSingle.Size = New System.Drawing.Size(39, 13)
        Me.lblMoviePosterExpertSingle.TabIndex = 10
        Me.lblMoviePosterExpertSingle.Text = "Poster"
        '
        'txtMovieNFOExpertSingle
        '
        Me.txtMovieNFOExpertSingle.Enabled = False
        Me.txtMovieNFOExpertSingle.Location = New System.Drawing.Point(71, 6)
        Me.txtMovieNFOExpertSingle.Name = "txtMovieNFOExpertSingle"
        Me.txtMovieNFOExpertSingle.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieNFOExpertSingle.TabIndex = 3
        '
        'lblMovieNFOExpertSingle
        '
        Me.lblMovieNFOExpertSingle.AutoSize = True
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
        Me.tpMovieFileNamingExpertMulti.UseVisualStyleBackColor = True
        '
        'gbMovieExpertMultiOptionalImages
        '
        Me.gbMovieExpertMultiOptionalImages.Controls.Add(Me.txtMovieActorThumbsExtExpertMulti)
        Me.gbMovieExpertMultiOptionalImages.Controls.Add(Me.chkMovieActorThumbsExpertMulti)
        Me.gbMovieExpertMultiOptionalImages.Location = New System.Drawing.Point(307, 105)
        Me.gbMovieExpertMultiOptionalImages.Name = "gbMovieExpertMultiOptionalImages"
        Me.gbMovieExpertMultiOptionalImages.Size = New System.Drawing.Size(160, 52)
        Me.gbMovieExpertMultiOptionalImages.TabIndex = 11
        Me.gbMovieExpertMultiOptionalImages.TabStop = False
        Me.gbMovieExpertMultiOptionalImages.Text = "Optional Images"
        '
        'txtMovieActorThumbsExtExpertMulti
        '
        Me.txtMovieActorThumbsExtExpertMulti.Enabled = False
        Me.txtMovieActorThumbsExtExpertMulti.Location = New System.Drawing.Point(108, 19)
        Me.txtMovieActorThumbsExtExpertMulti.Name = "txtMovieActorThumbsExtExpertMulti"
        Me.txtMovieActorThumbsExtExpertMulti.Size = New System.Drawing.Size(46, 22)
        Me.txtMovieActorThumbsExtExpertMulti.TabIndex = 2
        '
        'chkMovieActorThumbsExpertMulti
        '
        Me.chkMovieActorThumbsExpertMulti.AutoSize = True
        Me.chkMovieActorThumbsExpertMulti.Enabled = False
        Me.chkMovieActorThumbsExpertMulti.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieActorThumbsExpertMulti.Name = "chkMovieActorThumbsExpertMulti"
        Me.chkMovieActorThumbsExpertMulti.Size = New System.Drawing.Size(96, 17)
        Me.chkMovieActorThumbsExpertMulti.TabIndex = 1
        Me.chkMovieActorThumbsExpertMulti.Text = "Actor Thumbs"
        Me.chkMovieActorThumbsExpertMulti.UseVisualStyleBackColor = True
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
        Me.gbMovieExpertMultiOptionalSettings.TabStop = False
        Me.gbMovieExpertMultiOptionalSettings.Text = "Optional Settings"
        '
        'chkMovieUnstackExpertMulti
        '
        Me.chkMovieUnstackExpertMulti.AutoSize = True
        Me.chkMovieUnstackExpertMulti.Enabled = False
        Me.chkMovieUnstackExpertMulti.Location = New System.Drawing.Point(6, 67)
        Me.chkMovieUnstackExpertMulti.Name = "chkMovieUnstackExpertMulti"
        Me.chkMovieUnstackExpertMulti.Size = New System.Drawing.Size(128, 17)
        Me.chkMovieUnstackExpertMulti.TabIndex = 3
        Me.chkMovieUnstackExpertMulti.Text = "also save unstacked"
        Me.chkMovieUnstackExpertMulti.UseVisualStyleBackColor = True
        '
        'chkMovieStackExpertMulti
        '
        Me.chkMovieStackExpertMulti.AutoSize = True
        Me.chkMovieStackExpertMulti.Enabled = False
        Me.chkMovieStackExpertMulti.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieStackExpertMulti.Name = "chkMovieStackExpertMulti"
        Me.chkMovieStackExpertMulti.Size = New System.Drawing.Size(116, 17)
        Me.chkMovieStackExpertMulti.TabIndex = 2
        Me.chkMovieStackExpertMulti.Text = "Stack <filename>"
        Me.chkMovieStackExpertMulti.UseVisualStyleBackColor = True
        '
        'chkMovieXBMCTrailerFormatExpertMulti
        '
        Me.chkMovieXBMCTrailerFormatExpertMulti.AutoSize = True
        Me.chkMovieXBMCTrailerFormatExpertMulti.Enabled = False
        Me.chkMovieXBMCTrailerFormatExpertMulti.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieXBMCTrailerFormatExpertMulti.Name = "chkMovieXBMCTrailerFormatExpertMulti"
        Me.chkMovieXBMCTrailerFormatExpertMulti.Size = New System.Drawing.Size(129, 17)
        Me.chkMovieXBMCTrailerFormatExpertMulti.TabIndex = 1
        Me.chkMovieXBMCTrailerFormatExpertMulti.Text = "XBMC Trailer Format"
        Me.chkMovieXBMCTrailerFormatExpertMulti.UseVisualStyleBackColor = True
        '
        'txtMoviePosterExpertMulti
        '
        Me.txtMoviePosterExpertMulti.Enabled = False
        Me.txtMoviePosterExpertMulti.Location = New System.Drawing.Point(71, 33)
        Me.txtMoviePosterExpertMulti.Name = "txtMoviePosterExpertMulti"
        Me.txtMoviePosterExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMoviePosterExpertMulti.TabIndex = 2
        '
        'txtMovieFanartExpertMulti
        '
        Me.txtMovieFanartExpertMulti.Enabled = False
        Me.txtMovieFanartExpertMulti.Location = New System.Drawing.Point(71, 60)
        Me.txtMovieFanartExpertMulti.Name = "txtMovieFanartExpertMulti"
        Me.txtMovieFanartExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieFanartExpertMulti.TabIndex = 3
        '
        'lblMovieClearArtExpertMulti
        '
        Me.lblMovieClearArtExpertMulti.AutoSize = True
        Me.lblMovieClearArtExpertMulti.Location = New System.Drawing.Point(6, 171)
        Me.lblMovieClearArtExpertMulti.Name = "lblMovieClearArtExpertMulti"
        Me.lblMovieClearArtExpertMulti.Size = New System.Drawing.Size(48, 13)
        Me.lblMovieClearArtExpertMulti.TabIndex = 51
        Me.lblMovieClearArtExpertMulti.Text = "ClearArt"
        '
        'txtMovieTrailerExpertMulti
        '
        Me.txtMovieTrailerExpertMulti.Enabled = False
        Me.txtMovieTrailerExpertMulti.Location = New System.Drawing.Point(71, 87)
        Me.txtMovieTrailerExpertMulti.Name = "txtMovieTrailerExpertMulti"
        Me.txtMovieTrailerExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieTrailerExpertMulti.TabIndex = 4
        '
        'txtMovieBannerExpertMulti
        '
        Me.txtMovieBannerExpertMulti.Enabled = False
        Me.txtMovieBannerExpertMulti.Location = New System.Drawing.Point(71, 114)
        Me.txtMovieBannerExpertMulti.Name = "txtMovieBannerExpertMulti"
        Me.txtMovieBannerExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieBannerExpertMulti.TabIndex = 5
        '
        'txtMovieClearLogoExpertMulti
        '
        Me.txtMovieClearLogoExpertMulti.Enabled = False
        Me.txtMovieClearLogoExpertMulti.Location = New System.Drawing.Point(71, 141)
        Me.txtMovieClearLogoExpertMulti.Name = "txtMovieClearLogoExpertMulti"
        Me.txtMovieClearLogoExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieClearLogoExpertMulti.TabIndex = 6
        '
        'txtMovieClearArtExpertMulti
        '
        Me.txtMovieClearArtExpertMulti.Enabled = False
        Me.txtMovieClearArtExpertMulti.Location = New System.Drawing.Point(71, 168)
        Me.txtMovieClearArtExpertMulti.Name = "txtMovieClearArtExpertMulti"
        Me.txtMovieClearArtExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieClearArtExpertMulti.TabIndex = 7
        '
        'txtMovieLandscapeExpertMulti
        '
        Me.txtMovieLandscapeExpertMulti.Enabled = False
        Me.txtMovieLandscapeExpertMulti.Location = New System.Drawing.Point(71, 222)
        Me.txtMovieLandscapeExpertMulti.Name = "txtMovieLandscapeExpertMulti"
        Me.txtMovieLandscapeExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieLandscapeExpertMulti.TabIndex = 9
        '
        'txtMovieDiscArtExpertMulti
        '
        Me.txtMovieDiscArtExpertMulti.Enabled = False
        Me.txtMovieDiscArtExpertMulti.Location = New System.Drawing.Point(71, 195)
        Me.txtMovieDiscArtExpertMulti.Name = "txtMovieDiscArtExpertMulti"
        Me.txtMovieDiscArtExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieDiscArtExpertMulti.TabIndex = 8
        '
        'lblMovieLandscapeExpertMulti
        '
        Me.lblMovieLandscapeExpertMulti.AutoSize = True
        Me.lblMovieLandscapeExpertMulti.Location = New System.Drawing.Point(6, 225)
        Me.lblMovieLandscapeExpertMulti.Name = "lblMovieLandscapeExpertMulti"
        Me.lblMovieLandscapeExpertMulti.Size = New System.Drawing.Size(61, 13)
        Me.lblMovieLandscapeExpertMulti.TabIndex = 42
        Me.lblMovieLandscapeExpertMulti.Text = "Landscape"
        '
        'lblMovieDiscArtExpertMulti
        '
        Me.lblMovieDiscArtExpertMulti.AutoSize = True
        Me.lblMovieDiscArtExpertMulti.Location = New System.Drawing.Point(6, 198)
        Me.lblMovieDiscArtExpertMulti.Name = "lblMovieDiscArtExpertMulti"
        Me.lblMovieDiscArtExpertMulti.Size = New System.Drawing.Size(43, 13)
        Me.lblMovieDiscArtExpertMulti.TabIndex = 41
        Me.lblMovieDiscArtExpertMulti.Text = "DiscArt"
        '
        'lblMovieBannerExpertMulti
        '
        Me.lblMovieBannerExpertMulti.AutoSize = True
        Me.lblMovieBannerExpertMulti.Location = New System.Drawing.Point(6, 117)
        Me.lblMovieBannerExpertMulti.Name = "lblMovieBannerExpertMulti"
        Me.lblMovieBannerExpertMulti.Size = New System.Drawing.Size(44, 13)
        Me.lblMovieBannerExpertMulti.TabIndex = 40
        Me.lblMovieBannerExpertMulti.Text = "Banner"
        '
        'lblMovieTrailerExpertMulti
        '
        Me.lblMovieTrailerExpertMulti.AutoSize = True
        Me.lblMovieTrailerExpertMulti.Location = New System.Drawing.Point(6, 90)
        Me.lblMovieTrailerExpertMulti.Name = "lblMovieTrailerExpertMulti"
        Me.lblMovieTrailerExpertMulti.Size = New System.Drawing.Size(38, 13)
        Me.lblMovieTrailerExpertMulti.TabIndex = 39
        Me.lblMovieTrailerExpertMulti.Text = "Trailer"
        '
        'lblMovieClearLogoExpertMulti
        '
        Me.lblMovieClearLogoExpertMulti.AutoSize = True
        Me.lblMovieClearLogoExpertMulti.Location = New System.Drawing.Point(6, 144)
        Me.lblMovieClearLogoExpertMulti.Name = "lblMovieClearLogoExpertMulti"
        Me.lblMovieClearLogoExpertMulti.Size = New System.Drawing.Size(59, 13)
        Me.lblMovieClearLogoExpertMulti.TabIndex = 38
        Me.lblMovieClearLogoExpertMulti.Text = "ClearLogo"
        '
        'lblMovieFanartExpertMulti
        '
        Me.lblMovieFanartExpertMulti.AutoSize = True
        Me.lblMovieFanartExpertMulti.Location = New System.Drawing.Point(6, 64)
        Me.lblMovieFanartExpertMulti.Name = "lblMovieFanartExpertMulti"
        Me.lblMovieFanartExpertMulti.Size = New System.Drawing.Size(40, 13)
        Me.lblMovieFanartExpertMulti.TabIndex = 37
        Me.lblMovieFanartExpertMulti.Text = "Fanart"
        '
        'lblMoviePosterExpertMulti
        '
        Me.lblMoviePosterExpertMulti.AutoSize = True
        Me.lblMoviePosterExpertMulti.Location = New System.Drawing.Point(6, 36)
        Me.lblMoviePosterExpertMulti.Name = "lblMoviePosterExpertMulti"
        Me.lblMoviePosterExpertMulti.Size = New System.Drawing.Size(39, 13)
        Me.lblMoviePosterExpertMulti.TabIndex = 36
        Me.lblMoviePosterExpertMulti.Text = "Poster"
        '
        'txtMovieNFOExpertMulti
        '
        Me.txtMovieNFOExpertMulti.Enabled = False
        Me.txtMovieNFOExpertMulti.Location = New System.Drawing.Point(71, 6)
        Me.txtMovieNFOExpertMulti.Name = "txtMovieNFOExpertMulti"
        Me.txtMovieNFOExpertMulti.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieNFOExpertMulti.TabIndex = 1
        '
        'lblMovieNFOExpertMulti
        '
        Me.lblMovieNFOExpertMulti.AutoSize = True
        Me.lblMovieNFOExpertMulti.Location = New System.Drawing.Point(6, 9)
        Me.lblMovieNFOExpertMulti.Name = "lblMovieNFOExpertMulti"
        Me.lblMovieNFOExpertMulti.Size = New System.Drawing.Size(30, 13)
        Me.lblMovieNFOExpertMulti.TabIndex = 35
        Me.lblMovieNFOExpertMulti.Text = "NFO"
        '
        'tbMovieFileNamingExpertVTS
        '
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.gbMovieExpertVTSOptionalSettings)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.gbMovieExpertVTSOptionalImages)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieClearArtExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.txtMoviePosterExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieFanartExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieTrailerExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieBannerExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieClearLogoExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieClearArtExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieLandscapeExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieDiscArtExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieLandscapeExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieDiscArtExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieBannerExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieTrailerExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieClearLogoExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieFanartExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.lblMoviePosterExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.txtMovieNFOExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Controls.Add(Me.lblMovieNFOExpertVTS)
        Me.tbMovieFileNamingExpertVTS.Location = New System.Drawing.Point(4, 22)
        Me.tbMovieFileNamingExpertVTS.Name = "tbMovieFileNamingExpertVTS"
        Me.tbMovieFileNamingExpertVTS.Size = New System.Drawing.Size(473, 254)
        Me.tbMovieFileNamingExpertVTS.TabIndex = 2
        Me.tbMovieFileNamingExpertVTS.Text = "VIDEO_TS"
        Me.tbMovieFileNamingExpertVTS.UseVisualStyleBackColor = True
        '
        'gbMovieExpertVTSOptionalSettings
        '
        Me.gbMovieExpertVTSOptionalSettings.Controls.Add(Me.chkMovieRecognizeVTSExpertVTS)
        Me.gbMovieExpertVTSOptionalSettings.Controls.Add(Me.chkMovieUseBaseDirectoryExpertVTS)
        Me.gbMovieExpertVTSOptionalSettings.Controls.Add(Me.chkMovieXBMCTrailerFormatExpertVTS)
        Me.gbMovieExpertVTSOptionalSettings.Location = New System.Drawing.Point(307, 6)
        Me.gbMovieExpertVTSOptionalSettings.Name = "gbMovieExpertVTSOptionalSettings"
        Me.gbMovieExpertVTSOptionalSettings.Size = New System.Drawing.Size(160, 93)
        Me.gbMovieExpertVTSOptionalSettings.TabIndex = 10
        Me.gbMovieExpertVTSOptionalSettings.TabStop = False
        Me.gbMovieExpertVTSOptionalSettings.Text = "Optional Settings"
        '
        'chkMovieRecognizeVTSExpertVTS
        '
        Me.chkMovieRecognizeVTSExpertVTS.AutoSize = True
        Me.chkMovieRecognizeVTSExpertVTS.Enabled = False
        Me.chkMovieRecognizeVTSExpertVTS.Location = New System.Drawing.Point(6, 67)
        Me.chkMovieRecognizeVTSExpertVTS.Name = "chkMovieRecognizeVTSExpertVTS"
        Me.chkMovieRecognizeVTSExpertVTS.Size = New System.Drawing.Size(131, 17)
        Me.chkMovieRecognizeVTSExpertVTS.TabIndex = 3
        Me.chkMovieRecognizeVTSExpertVTS.Text = "Recognize VIDEO_TS"
        Me.chkMovieRecognizeVTSExpertVTS.UseVisualStyleBackColor = True
        '
        'chkMovieUseBaseDirectoryExpertVTS
        '
        Me.chkMovieUseBaseDirectoryExpertVTS.AutoSize = True
        Me.chkMovieUseBaseDirectoryExpertVTS.Enabled = False
        Me.chkMovieUseBaseDirectoryExpertVTS.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieUseBaseDirectoryExpertVTS.Name = "chkMovieUseBaseDirectoryExpertVTS"
        Me.chkMovieUseBaseDirectoryExpertVTS.Size = New System.Drawing.Size(121, 17)
        Me.chkMovieUseBaseDirectoryExpertVTS.TabIndex = 2
        Me.chkMovieUseBaseDirectoryExpertVTS.Text = "Use Base Directory"
        Me.chkMovieUseBaseDirectoryExpertVTS.UseVisualStyleBackColor = True
        '
        'chkMovieXBMCTrailerFormatExpertVTS
        '
        Me.chkMovieXBMCTrailerFormatExpertVTS.AutoSize = True
        Me.chkMovieXBMCTrailerFormatExpertVTS.Enabled = False
        Me.chkMovieXBMCTrailerFormatExpertVTS.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieXBMCTrailerFormatExpertVTS.Name = "chkMovieXBMCTrailerFormatExpertVTS"
        Me.chkMovieXBMCTrailerFormatExpertVTS.Size = New System.Drawing.Size(129, 17)
        Me.chkMovieXBMCTrailerFormatExpertVTS.TabIndex = 1
        Me.chkMovieXBMCTrailerFormatExpertVTS.Text = "XBMC Trailer Format"
        Me.chkMovieXBMCTrailerFormatExpertVTS.UseVisualStyleBackColor = True
        '
        'gbMovieExpertVTSOptionalImages
        '
        Me.gbMovieExpertVTSOptionalImages.Controls.Add(Me.txtMovieActorThumbsExtExpertVTS)
        Me.gbMovieExpertVTSOptionalImages.Controls.Add(Me.chkMovieActorThumbsExpertVTS)
        Me.gbMovieExpertVTSOptionalImages.Controls.Add(Me.chkMovieExtrafanartsExpertVTS)
        Me.gbMovieExpertVTSOptionalImages.Controls.Add(Me.chkMovieExtrathumbsExpertVTS)
        Me.gbMovieExpertVTSOptionalImages.Location = New System.Drawing.Point(307, 105)
        Me.gbMovieExpertVTSOptionalImages.Name = "gbMovieExpertVTSOptionalImages"
        Me.gbMovieExpertVTSOptionalImages.Size = New System.Drawing.Size(160, 93)
        Me.gbMovieExpertVTSOptionalImages.TabIndex = 11
        Me.gbMovieExpertVTSOptionalImages.TabStop = False
        Me.gbMovieExpertVTSOptionalImages.Text = "Optional Images"
        '
        'txtMovieActorThumbsExtExpertVTS
        '
        Me.txtMovieActorThumbsExtExpertVTS.Enabled = False
        Me.txtMovieActorThumbsExtExpertVTS.Location = New System.Drawing.Point(108, 19)
        Me.txtMovieActorThumbsExtExpertVTS.Name = "txtMovieActorThumbsExtExpertVTS"
        Me.txtMovieActorThumbsExtExpertVTS.Size = New System.Drawing.Size(46, 22)
        Me.txtMovieActorThumbsExtExpertVTS.TabIndex = 2
        '
        'chkMovieActorThumbsExpertVTS
        '
        Me.chkMovieActorThumbsExpertVTS.AutoSize = True
        Me.chkMovieActorThumbsExpertVTS.Enabled = False
        Me.chkMovieActorThumbsExpertVTS.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieActorThumbsExpertVTS.Name = "chkMovieActorThumbsExpertVTS"
        Me.chkMovieActorThumbsExpertVTS.Size = New System.Drawing.Size(96, 17)
        Me.chkMovieActorThumbsExpertVTS.TabIndex = 1
        Me.chkMovieActorThumbsExpertVTS.Text = "Actor Thumbs"
        Me.chkMovieActorThumbsExpertVTS.UseVisualStyleBackColor = True
        '
        'chkMovieExtrafanartsExpertVTS
        '
        Me.chkMovieExtrafanartsExpertVTS.AutoSize = True
        Me.chkMovieExtrafanartsExpertVTS.Enabled = False
        Me.chkMovieExtrafanartsExpertVTS.Location = New System.Drawing.Point(6, 67)
        Me.chkMovieExtrafanartsExpertVTS.Name = "chkMovieExtrafanartsExpertVTS"
        Me.chkMovieExtrafanartsExpertVTS.Size = New System.Drawing.Size(87, 17)
        Me.chkMovieExtrafanartsExpertVTS.TabIndex = 4
        Me.chkMovieExtrafanartsExpertVTS.Text = "Extrafanarts"
        Me.chkMovieExtrafanartsExpertVTS.UseVisualStyleBackColor = True
        '
        'chkMovieExtrathumbsExpertVTS
        '
        Me.chkMovieExtrathumbsExpertVTS.AutoSize = True
        Me.chkMovieExtrathumbsExpertVTS.Enabled = False
        Me.chkMovieExtrathumbsExpertVTS.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieExtrathumbsExpertVTS.Name = "chkMovieExtrathumbsExpertVTS"
        Me.chkMovieExtrathumbsExpertVTS.Size = New System.Drawing.Size(90, 17)
        Me.chkMovieExtrathumbsExpertVTS.TabIndex = 3
        Me.chkMovieExtrathumbsExpertVTS.Text = "Extrathumbs"
        Me.chkMovieExtrathumbsExpertVTS.UseVisualStyleBackColor = True
        '
        'lblMovieClearArtExpertVTS
        '
        Me.lblMovieClearArtExpertVTS.AutoSize = True
        Me.lblMovieClearArtExpertVTS.Location = New System.Drawing.Point(6, 171)
        Me.lblMovieClearArtExpertVTS.Name = "lblMovieClearArtExpertVTS"
        Me.lblMovieClearArtExpertVTS.Size = New System.Drawing.Size(48, 13)
        Me.lblMovieClearArtExpertVTS.TabIndex = 51
        Me.lblMovieClearArtExpertVTS.Text = "ClearArt"
        '
        'txtMoviePosterExpertVTS
        '
        Me.txtMoviePosterExpertVTS.Enabled = False
        Me.txtMoviePosterExpertVTS.Location = New System.Drawing.Point(71, 33)
        Me.txtMoviePosterExpertVTS.Name = "txtMoviePosterExpertVTS"
        Me.txtMoviePosterExpertVTS.Size = New System.Drawing.Size(220, 22)
        Me.txtMoviePosterExpertVTS.TabIndex = 2
        '
        'txtMovieFanartExpertVTS
        '
        Me.txtMovieFanartExpertVTS.Enabled = False
        Me.txtMovieFanartExpertVTS.Location = New System.Drawing.Point(71, 60)
        Me.txtMovieFanartExpertVTS.Name = "txtMovieFanartExpertVTS"
        Me.txtMovieFanartExpertVTS.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieFanartExpertVTS.TabIndex = 3
        '
        'txtMovieTrailerExpertVTS
        '
        Me.txtMovieTrailerExpertVTS.Enabled = False
        Me.txtMovieTrailerExpertVTS.Location = New System.Drawing.Point(71, 87)
        Me.txtMovieTrailerExpertVTS.Name = "txtMovieTrailerExpertVTS"
        Me.txtMovieTrailerExpertVTS.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieTrailerExpertVTS.TabIndex = 4
        '
        'txtMovieBannerExpertVTS
        '
        Me.txtMovieBannerExpertVTS.Enabled = False
        Me.txtMovieBannerExpertVTS.Location = New System.Drawing.Point(71, 114)
        Me.txtMovieBannerExpertVTS.Name = "txtMovieBannerExpertVTS"
        Me.txtMovieBannerExpertVTS.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieBannerExpertVTS.TabIndex = 5
        '
        'txtMovieClearLogoExpertVTS
        '
        Me.txtMovieClearLogoExpertVTS.Enabled = False
        Me.txtMovieClearLogoExpertVTS.Location = New System.Drawing.Point(71, 141)
        Me.txtMovieClearLogoExpertVTS.Name = "txtMovieClearLogoExpertVTS"
        Me.txtMovieClearLogoExpertVTS.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieClearLogoExpertVTS.TabIndex = 6
        '
        'txtMovieClearArtExpertVTS
        '
        Me.txtMovieClearArtExpertVTS.Enabled = False
        Me.txtMovieClearArtExpertVTS.Location = New System.Drawing.Point(71, 168)
        Me.txtMovieClearArtExpertVTS.Name = "txtMovieClearArtExpertVTS"
        Me.txtMovieClearArtExpertVTS.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieClearArtExpertVTS.TabIndex = 7
        '
        'txtMovieLandscapeExpertVTS
        '
        Me.txtMovieLandscapeExpertVTS.Enabled = False
        Me.txtMovieLandscapeExpertVTS.Location = New System.Drawing.Point(71, 222)
        Me.txtMovieLandscapeExpertVTS.Name = "txtMovieLandscapeExpertVTS"
        Me.txtMovieLandscapeExpertVTS.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieLandscapeExpertVTS.TabIndex = 9
        '
        'txtMovieDiscArtExpertVTS
        '
        Me.txtMovieDiscArtExpertVTS.Enabled = False
        Me.txtMovieDiscArtExpertVTS.Location = New System.Drawing.Point(71, 195)
        Me.txtMovieDiscArtExpertVTS.Name = "txtMovieDiscArtExpertVTS"
        Me.txtMovieDiscArtExpertVTS.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieDiscArtExpertVTS.TabIndex = 8
        '
        'lblMovieLandscapeExpertVTS
        '
        Me.lblMovieLandscapeExpertVTS.AutoSize = True
        Me.lblMovieLandscapeExpertVTS.Location = New System.Drawing.Point(6, 225)
        Me.lblMovieLandscapeExpertVTS.Name = "lblMovieLandscapeExpertVTS"
        Me.lblMovieLandscapeExpertVTS.Size = New System.Drawing.Size(61, 13)
        Me.lblMovieLandscapeExpertVTS.TabIndex = 42
        Me.lblMovieLandscapeExpertVTS.Text = "Landscape"
        '
        'lblMovieDiscArtExpertVTS
        '
        Me.lblMovieDiscArtExpertVTS.AutoSize = True
        Me.lblMovieDiscArtExpertVTS.Location = New System.Drawing.Point(6, 198)
        Me.lblMovieDiscArtExpertVTS.Name = "lblMovieDiscArtExpertVTS"
        Me.lblMovieDiscArtExpertVTS.Size = New System.Drawing.Size(43, 13)
        Me.lblMovieDiscArtExpertVTS.TabIndex = 41
        Me.lblMovieDiscArtExpertVTS.Text = "DiscArt"
        '
        'lblMovieBannerExpertVTS
        '
        Me.lblMovieBannerExpertVTS.AutoSize = True
        Me.lblMovieBannerExpertVTS.Location = New System.Drawing.Point(6, 117)
        Me.lblMovieBannerExpertVTS.Name = "lblMovieBannerExpertVTS"
        Me.lblMovieBannerExpertVTS.Size = New System.Drawing.Size(44, 13)
        Me.lblMovieBannerExpertVTS.TabIndex = 40
        Me.lblMovieBannerExpertVTS.Text = "Banner"
        '
        'lblMovieTrailerExpertVTS
        '
        Me.lblMovieTrailerExpertVTS.AutoSize = True
        Me.lblMovieTrailerExpertVTS.Location = New System.Drawing.Point(6, 90)
        Me.lblMovieTrailerExpertVTS.Name = "lblMovieTrailerExpertVTS"
        Me.lblMovieTrailerExpertVTS.Size = New System.Drawing.Size(38, 13)
        Me.lblMovieTrailerExpertVTS.TabIndex = 39
        Me.lblMovieTrailerExpertVTS.Text = "Trailer"
        '
        'lblMovieClearLogoExpertVTS
        '
        Me.lblMovieClearLogoExpertVTS.AutoSize = True
        Me.lblMovieClearLogoExpertVTS.Location = New System.Drawing.Point(6, 144)
        Me.lblMovieClearLogoExpertVTS.Name = "lblMovieClearLogoExpertVTS"
        Me.lblMovieClearLogoExpertVTS.Size = New System.Drawing.Size(59, 13)
        Me.lblMovieClearLogoExpertVTS.TabIndex = 38
        Me.lblMovieClearLogoExpertVTS.Text = "ClearLogo"
        '
        'lblMovieFanartExpertVTS
        '
        Me.lblMovieFanartExpertVTS.AutoSize = True
        Me.lblMovieFanartExpertVTS.Location = New System.Drawing.Point(6, 64)
        Me.lblMovieFanartExpertVTS.Name = "lblMovieFanartExpertVTS"
        Me.lblMovieFanartExpertVTS.Size = New System.Drawing.Size(40, 13)
        Me.lblMovieFanartExpertVTS.TabIndex = 37
        Me.lblMovieFanartExpertVTS.Text = "Fanart"
        '
        'lblMoviePosterExpertVTS
        '
        Me.lblMoviePosterExpertVTS.AutoSize = True
        Me.lblMoviePosterExpertVTS.Location = New System.Drawing.Point(6, 36)
        Me.lblMoviePosterExpertVTS.Name = "lblMoviePosterExpertVTS"
        Me.lblMoviePosterExpertVTS.Size = New System.Drawing.Size(39, 13)
        Me.lblMoviePosterExpertVTS.TabIndex = 36
        Me.lblMoviePosterExpertVTS.Text = "Poster"
        '
        'txtMovieNFOExpertVTS
        '
        Me.txtMovieNFOExpertVTS.Enabled = False
        Me.txtMovieNFOExpertVTS.Location = New System.Drawing.Point(71, 6)
        Me.txtMovieNFOExpertVTS.Name = "txtMovieNFOExpertVTS"
        Me.txtMovieNFOExpertVTS.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieNFOExpertVTS.TabIndex = 1
        '
        'lblMovieNFOExpertVTS
        '
        Me.lblMovieNFOExpertVTS.AutoSize = True
        Me.lblMovieNFOExpertVTS.Location = New System.Drawing.Point(6, 9)
        Me.lblMovieNFOExpertVTS.Name = "lblMovieNFOExpertVTS"
        Me.lblMovieNFOExpertVTS.Size = New System.Drawing.Size(30, 13)
        Me.lblMovieNFOExpertVTS.TabIndex = 35
        Me.lblMovieNFOExpertVTS.Text = "NFO"
        '
        'tbMovieFileNamingExpertBDMV
        '
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.gbMovieExpertBDMVOptionalSettings)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.gbMovieExpertBDMVOptionalImages)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieClearArtExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.txtMoviePosterExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieFanartExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieTrailerExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieBannerExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieClearLogoExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieClearArtExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieLandscapeExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieDiscArtExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieLandscapeExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieDiscArtExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieBannerExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieTrailerExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieClearLogoExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieFanartExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.lblMoviePosterExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.txtMovieNFOExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Controls.Add(Me.lblMovieNFOExpertBDMV)
        Me.tbMovieFileNamingExpertBDMV.Location = New System.Drawing.Point(4, 22)
        Me.tbMovieFileNamingExpertBDMV.Name = "tbMovieFileNamingExpertBDMV"
        Me.tbMovieFileNamingExpertBDMV.Size = New System.Drawing.Size(473, 254)
        Me.tbMovieFileNamingExpertBDMV.TabIndex = 3
        Me.tbMovieFileNamingExpertBDMV.Text = "BDMV"
        Me.tbMovieFileNamingExpertBDMV.UseVisualStyleBackColor = True
        '
        'gbMovieExpertBDMVOptionalSettings
        '
        Me.gbMovieExpertBDMVOptionalSettings.Controls.Add(Me.chkMovieUseBaseDirectoryExpertBDMV)
        Me.gbMovieExpertBDMVOptionalSettings.Controls.Add(Me.chkMovieXBMCTrailerFormatExpertBDMV)
        Me.gbMovieExpertBDMVOptionalSettings.Location = New System.Drawing.Point(307, 6)
        Me.gbMovieExpertBDMVOptionalSettings.Name = "gbMovieExpertBDMVOptionalSettings"
        Me.gbMovieExpertBDMVOptionalSettings.Size = New System.Drawing.Size(160, 71)
        Me.gbMovieExpertBDMVOptionalSettings.TabIndex = 10
        Me.gbMovieExpertBDMVOptionalSettings.TabStop = False
        Me.gbMovieExpertBDMVOptionalSettings.Text = "Optional Settings"
        '
        'chkMovieUseBaseDirectoryExpertBDMV
        '
        Me.chkMovieUseBaseDirectoryExpertBDMV.AutoSize = True
        Me.chkMovieUseBaseDirectoryExpertBDMV.Enabled = False
        Me.chkMovieUseBaseDirectoryExpertBDMV.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieUseBaseDirectoryExpertBDMV.Name = "chkMovieUseBaseDirectoryExpertBDMV"
        Me.chkMovieUseBaseDirectoryExpertBDMV.Size = New System.Drawing.Size(121, 17)
        Me.chkMovieUseBaseDirectoryExpertBDMV.TabIndex = 2
        Me.chkMovieUseBaseDirectoryExpertBDMV.Text = "Use Base Directory"
        Me.chkMovieUseBaseDirectoryExpertBDMV.UseVisualStyleBackColor = True
        '
        'chkMovieXBMCTrailerFormatExpertBDMV
        '
        Me.chkMovieXBMCTrailerFormatExpertBDMV.AutoSize = True
        Me.chkMovieXBMCTrailerFormatExpertBDMV.Enabled = False
        Me.chkMovieXBMCTrailerFormatExpertBDMV.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieXBMCTrailerFormatExpertBDMV.Name = "chkMovieXBMCTrailerFormatExpertBDMV"
        Me.chkMovieXBMCTrailerFormatExpertBDMV.Size = New System.Drawing.Size(129, 17)
        Me.chkMovieXBMCTrailerFormatExpertBDMV.TabIndex = 1
        Me.chkMovieXBMCTrailerFormatExpertBDMV.Text = "XBMC Trailer Format"
        Me.chkMovieXBMCTrailerFormatExpertBDMV.UseVisualStyleBackColor = True
        '
        'gbMovieExpertBDMVOptionalImages
        '
        Me.gbMovieExpertBDMVOptionalImages.Controls.Add(Me.txtMovieActorThumbsExtExpertBDMV)
        Me.gbMovieExpertBDMVOptionalImages.Controls.Add(Me.chkMovieActorThumbsExpertBDMV)
        Me.gbMovieExpertBDMVOptionalImages.Controls.Add(Me.chkMovieExtrafanartsExpertBDMV)
        Me.gbMovieExpertBDMVOptionalImages.Controls.Add(Me.chkMovieExtrathumbsExpertBDMV)
        Me.gbMovieExpertBDMVOptionalImages.Location = New System.Drawing.Point(307, 105)
        Me.gbMovieExpertBDMVOptionalImages.Name = "gbMovieExpertBDMVOptionalImages"
        Me.gbMovieExpertBDMVOptionalImages.Size = New System.Drawing.Size(160, 93)
        Me.gbMovieExpertBDMVOptionalImages.TabIndex = 1
        Me.gbMovieExpertBDMVOptionalImages.TabStop = False
        Me.gbMovieExpertBDMVOptionalImages.Text = "Optional Images"
        '
        'txtMovieActorThumbsExtExpertBDMV
        '
        Me.txtMovieActorThumbsExtExpertBDMV.Enabled = False
        Me.txtMovieActorThumbsExtExpertBDMV.Location = New System.Drawing.Point(108, 19)
        Me.txtMovieActorThumbsExtExpertBDMV.Name = "txtMovieActorThumbsExtExpertBDMV"
        Me.txtMovieActorThumbsExtExpertBDMV.Size = New System.Drawing.Size(46, 22)
        Me.txtMovieActorThumbsExtExpertBDMV.TabIndex = 2
        '
        'chkMovieActorThumbsExpertBDMV
        '
        Me.chkMovieActorThumbsExpertBDMV.AutoSize = True
        Me.chkMovieActorThumbsExpertBDMV.Enabled = False
        Me.chkMovieActorThumbsExpertBDMV.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieActorThumbsExpertBDMV.Name = "chkMovieActorThumbsExpertBDMV"
        Me.chkMovieActorThumbsExpertBDMV.Size = New System.Drawing.Size(96, 17)
        Me.chkMovieActorThumbsExpertBDMV.TabIndex = 1
        Me.chkMovieActorThumbsExpertBDMV.Text = "Actor Thumbs"
        Me.chkMovieActorThumbsExpertBDMV.UseVisualStyleBackColor = True
        '
        'chkMovieExtrafanartsExpertBDMV
        '
        Me.chkMovieExtrafanartsExpertBDMV.AutoSize = True
        Me.chkMovieExtrafanartsExpertBDMV.Enabled = False
        Me.chkMovieExtrafanartsExpertBDMV.Location = New System.Drawing.Point(6, 67)
        Me.chkMovieExtrafanartsExpertBDMV.Name = "chkMovieExtrafanartsExpertBDMV"
        Me.chkMovieExtrafanartsExpertBDMV.Size = New System.Drawing.Size(87, 17)
        Me.chkMovieExtrafanartsExpertBDMV.TabIndex = 4
        Me.chkMovieExtrafanartsExpertBDMV.Text = "Extrafanarts"
        Me.chkMovieExtrafanartsExpertBDMV.UseVisualStyleBackColor = True
        '
        'chkMovieExtrathumbsExpertBDMV
        '
        Me.chkMovieExtrathumbsExpertBDMV.AutoSize = True
        Me.chkMovieExtrathumbsExpertBDMV.Enabled = False
        Me.chkMovieExtrathumbsExpertBDMV.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieExtrathumbsExpertBDMV.Name = "chkMovieExtrathumbsExpertBDMV"
        Me.chkMovieExtrathumbsExpertBDMV.Size = New System.Drawing.Size(90, 17)
        Me.chkMovieExtrathumbsExpertBDMV.TabIndex = 3
        Me.chkMovieExtrathumbsExpertBDMV.Text = "Extrathumbs"
        Me.chkMovieExtrathumbsExpertBDMV.UseVisualStyleBackColor = True
        '
        'lblMovieClearArtExpertBDMV
        '
        Me.lblMovieClearArtExpertBDMV.AutoSize = True
        Me.lblMovieClearArtExpertBDMV.Location = New System.Drawing.Point(6, 171)
        Me.lblMovieClearArtExpertBDMV.Name = "lblMovieClearArtExpertBDMV"
        Me.lblMovieClearArtExpertBDMV.Size = New System.Drawing.Size(48, 13)
        Me.lblMovieClearArtExpertBDMV.TabIndex = 51
        Me.lblMovieClearArtExpertBDMV.Text = "ClearArt"
        '
        'txtMoviePosterExpertBDMV
        '
        Me.txtMoviePosterExpertBDMV.Enabled = False
        Me.txtMoviePosterExpertBDMV.Location = New System.Drawing.Point(71, 33)
        Me.txtMoviePosterExpertBDMV.Name = "txtMoviePosterExpertBDMV"
        Me.txtMoviePosterExpertBDMV.Size = New System.Drawing.Size(220, 22)
        Me.txtMoviePosterExpertBDMV.TabIndex = 2
        '
        'txtMovieFanartExpertBDMV
        '
        Me.txtMovieFanartExpertBDMV.Enabled = False
        Me.txtMovieFanartExpertBDMV.Location = New System.Drawing.Point(71, 60)
        Me.txtMovieFanartExpertBDMV.Name = "txtMovieFanartExpertBDMV"
        Me.txtMovieFanartExpertBDMV.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieFanartExpertBDMV.TabIndex = 3
        '
        'txtMovieTrailerExpertBDMV
        '
        Me.txtMovieTrailerExpertBDMV.Enabled = False
        Me.txtMovieTrailerExpertBDMV.Location = New System.Drawing.Point(71, 87)
        Me.txtMovieTrailerExpertBDMV.Name = "txtMovieTrailerExpertBDMV"
        Me.txtMovieTrailerExpertBDMV.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieTrailerExpertBDMV.TabIndex = 4
        '
        'txtMovieBannerExpertBDMV
        '
        Me.txtMovieBannerExpertBDMV.Enabled = False
        Me.txtMovieBannerExpertBDMV.Location = New System.Drawing.Point(71, 114)
        Me.txtMovieBannerExpertBDMV.Name = "txtMovieBannerExpertBDMV"
        Me.txtMovieBannerExpertBDMV.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieBannerExpertBDMV.TabIndex = 5
        '
        'txtMovieClearLogoExpertBDMV
        '
        Me.txtMovieClearLogoExpertBDMV.Enabled = False
        Me.txtMovieClearLogoExpertBDMV.Location = New System.Drawing.Point(71, 141)
        Me.txtMovieClearLogoExpertBDMV.Name = "txtMovieClearLogoExpertBDMV"
        Me.txtMovieClearLogoExpertBDMV.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieClearLogoExpertBDMV.TabIndex = 6
        '
        'txtMovieClearArtExpertBDMV
        '
        Me.txtMovieClearArtExpertBDMV.Enabled = False
        Me.txtMovieClearArtExpertBDMV.Location = New System.Drawing.Point(71, 168)
        Me.txtMovieClearArtExpertBDMV.Name = "txtMovieClearArtExpertBDMV"
        Me.txtMovieClearArtExpertBDMV.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieClearArtExpertBDMV.TabIndex = 7
        '
        'txtMovieLandscapeExpertBDMV
        '
        Me.txtMovieLandscapeExpertBDMV.Enabled = False
        Me.txtMovieLandscapeExpertBDMV.Location = New System.Drawing.Point(71, 222)
        Me.txtMovieLandscapeExpertBDMV.Name = "txtMovieLandscapeExpertBDMV"
        Me.txtMovieLandscapeExpertBDMV.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieLandscapeExpertBDMV.TabIndex = 9
        '
        'txtMovieDiscArtExpertBDMV
        '
        Me.txtMovieDiscArtExpertBDMV.Enabled = False
        Me.txtMovieDiscArtExpertBDMV.Location = New System.Drawing.Point(71, 195)
        Me.txtMovieDiscArtExpertBDMV.Name = "txtMovieDiscArtExpertBDMV"
        Me.txtMovieDiscArtExpertBDMV.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieDiscArtExpertBDMV.TabIndex = 8
        '
        'lblMovieLandscapeExpertBDMV
        '
        Me.lblMovieLandscapeExpertBDMV.AutoSize = True
        Me.lblMovieLandscapeExpertBDMV.Location = New System.Drawing.Point(6, 225)
        Me.lblMovieLandscapeExpertBDMV.Name = "lblMovieLandscapeExpertBDMV"
        Me.lblMovieLandscapeExpertBDMV.Size = New System.Drawing.Size(61, 13)
        Me.lblMovieLandscapeExpertBDMV.TabIndex = 42
        Me.lblMovieLandscapeExpertBDMV.Text = "Landscape"
        '
        'lblMovieDiscArtExpertBDMV
        '
        Me.lblMovieDiscArtExpertBDMV.AutoSize = True
        Me.lblMovieDiscArtExpertBDMV.Location = New System.Drawing.Point(6, 198)
        Me.lblMovieDiscArtExpertBDMV.Name = "lblMovieDiscArtExpertBDMV"
        Me.lblMovieDiscArtExpertBDMV.Size = New System.Drawing.Size(43, 13)
        Me.lblMovieDiscArtExpertBDMV.TabIndex = 41
        Me.lblMovieDiscArtExpertBDMV.Text = "DiscArt"
        '
        'lblMovieBannerExpertBDMV
        '
        Me.lblMovieBannerExpertBDMV.AutoSize = True
        Me.lblMovieBannerExpertBDMV.Location = New System.Drawing.Point(6, 117)
        Me.lblMovieBannerExpertBDMV.Name = "lblMovieBannerExpertBDMV"
        Me.lblMovieBannerExpertBDMV.Size = New System.Drawing.Size(44, 13)
        Me.lblMovieBannerExpertBDMV.TabIndex = 40
        Me.lblMovieBannerExpertBDMV.Text = "Banner"
        '
        'lblMovieTrailerExpertBDMV
        '
        Me.lblMovieTrailerExpertBDMV.AutoSize = True
        Me.lblMovieTrailerExpertBDMV.Location = New System.Drawing.Point(6, 90)
        Me.lblMovieTrailerExpertBDMV.Name = "lblMovieTrailerExpertBDMV"
        Me.lblMovieTrailerExpertBDMV.Size = New System.Drawing.Size(38, 13)
        Me.lblMovieTrailerExpertBDMV.TabIndex = 39
        Me.lblMovieTrailerExpertBDMV.Text = "Trailer"
        '
        'lblMovieClearLogoExpertBDMV
        '
        Me.lblMovieClearLogoExpertBDMV.AutoSize = True
        Me.lblMovieClearLogoExpertBDMV.Location = New System.Drawing.Point(6, 144)
        Me.lblMovieClearLogoExpertBDMV.Name = "lblMovieClearLogoExpertBDMV"
        Me.lblMovieClearLogoExpertBDMV.Size = New System.Drawing.Size(59, 13)
        Me.lblMovieClearLogoExpertBDMV.TabIndex = 38
        Me.lblMovieClearLogoExpertBDMV.Text = "ClearLogo"
        '
        'lblMovieFanartExpertBDMV
        '
        Me.lblMovieFanartExpertBDMV.AutoSize = True
        Me.lblMovieFanartExpertBDMV.Location = New System.Drawing.Point(6, 64)
        Me.lblMovieFanartExpertBDMV.Name = "lblMovieFanartExpertBDMV"
        Me.lblMovieFanartExpertBDMV.Size = New System.Drawing.Size(40, 13)
        Me.lblMovieFanartExpertBDMV.TabIndex = 37
        Me.lblMovieFanartExpertBDMV.Text = "Fanart"
        '
        'lblMoviePosterExpertBDMV
        '
        Me.lblMoviePosterExpertBDMV.AutoSize = True
        Me.lblMoviePosterExpertBDMV.Location = New System.Drawing.Point(6, 36)
        Me.lblMoviePosterExpertBDMV.Name = "lblMoviePosterExpertBDMV"
        Me.lblMoviePosterExpertBDMV.Size = New System.Drawing.Size(39, 13)
        Me.lblMoviePosterExpertBDMV.TabIndex = 36
        Me.lblMoviePosterExpertBDMV.Text = "Poster"
        '
        'txtMovieNFOExpertBDMV
        '
        Me.txtMovieNFOExpertBDMV.Enabled = False
        Me.txtMovieNFOExpertBDMV.Location = New System.Drawing.Point(71, 6)
        Me.txtMovieNFOExpertBDMV.Name = "txtMovieNFOExpertBDMV"
        Me.txtMovieNFOExpertBDMV.Size = New System.Drawing.Size(220, 22)
        Me.txtMovieNFOExpertBDMV.TabIndex = 1
        '
        'lblMovieNFOExpertBDMV
        '
        Me.lblMovieNFOExpertBDMV.AutoSize = True
        Me.lblMovieNFOExpertBDMV.Location = New System.Drawing.Point(6, 9)
        Me.lblMovieNFOExpertBDMV.Name = "lblMovieNFOExpertBDMV"
        Me.lblMovieNFOExpertBDMV.Size = New System.Drawing.Size(30, 13)
        Me.lblMovieNFOExpertBDMV.TabIndex = 35
        Me.lblMovieNFOExpertBDMV.Text = "NFO"
        '
        'chkMovieUseExpert
        '
        Me.chkMovieUseExpert.AutoSize = True
        Me.chkMovieUseExpert.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieUseExpert.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieUseExpert.Name = "chkMovieUseExpert"
        Me.chkMovieUseExpert.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieUseExpert.TabIndex = 1
        Me.chkMovieUseExpert.Text = "Use"
        Me.chkMovieUseExpert.UseVisualStyleBackColor = True
        '
        'btnEditSource
        '
        Me.btnEditSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditSource.Image = CType(resources.GetObject("btnEditSource.Image"), System.Drawing.Image)
        Me.btnEditSource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditSource.Location = New System.Drawing.Point(638, 35)
        Me.btnEditSource.Name = "btnEditSource"
        Me.btnEditSource.Size = New System.Drawing.Size(104, 23)
        Me.btnEditSource.TabIndex = 2
        Me.btnEditSource.Text = "Edit Source"
        Me.btnEditSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditSource.UseVisualStyleBackColor = True
        '
        'gbMovieMiscOptions
        '
        Me.gbMovieMiscOptions.Controls.Add(Me.chkScanOrderModify)
        Me.gbMovieMiscOptions.Controls.Add(Me.chkSortBeforeScan)
        Me.gbMovieMiscOptions.Controls.Add(Me.chkIgnoreLastScan)
        Me.gbMovieMiscOptions.Controls.Add(Me.chkCleanDB)
        Me.gbMovieMiscOptions.Controls.Add(Me.chkSkipStackedSizeCheck)
        Me.gbMovieMiscOptions.Controls.Add(Me.Label21)
        Me.gbMovieMiscOptions.Controls.Add(Me.txtSkipLessThan)
        Me.gbMovieMiscOptions.Controls.Add(Me.Label20)
        Me.gbMovieMiscOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieMiscOptions.Location = New System.Drawing.Point(5, 113)
        Me.gbMovieMiscOptions.Name = "gbMovieMiscOptions"
        Me.gbMovieMiscOptions.Size = New System.Drawing.Size(212, 229)
        Me.gbMovieMiscOptions.TabIndex = 4
        Me.gbMovieMiscOptions.TabStop = False
        Me.gbMovieMiscOptions.Text = "Miscellaneous Options"
        '
        'chkScanOrderModify
        '
        Me.chkScanOrderModify.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScanOrderModify.Location = New System.Drawing.Point(6, 148)
        Me.chkScanOrderModify.Name = "chkScanOrderModify"
        Me.chkScanOrderModify.Size = New System.Drawing.Size(200, 33)
        Me.chkScanOrderModify.TabIndex = 8
        Me.chkScanOrderModify.Text = "Scan in order of last write time."
        Me.chkScanOrderModify.UseVisualStyleBackColor = True
        '
        'chkSortBeforeScan
        '
        Me.chkSortBeforeScan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSortBeforeScan.Location = New System.Drawing.Point(6, 72)
        Me.chkSortBeforeScan.Name = "chkSortBeforeScan"
        Me.chkSortBeforeScan.Size = New System.Drawing.Size(200, 31)
        Me.chkSortBeforeScan.TabIndex = 6
        Me.chkSortBeforeScan.Text = "Sort files into folders before each library update"
        Me.chkSortBeforeScan.UseVisualStyleBackColor = True
        '
        'chkIgnoreLastScan
        '
        Me.chkIgnoreLastScan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIgnoreLastScan.Location = New System.Drawing.Point(6, 109)
        Me.chkIgnoreLastScan.Name = "chkIgnoreLastScan"
        Me.chkIgnoreLastScan.Size = New System.Drawing.Size(200, 33)
        Me.chkIgnoreLastScan.TabIndex = 7
        Me.chkIgnoreLastScan.Text = "Always scan all media when updating library"
        Me.chkIgnoreLastScan.UseVisualStyleBackColor = True
        '
        'chkCleanDB
        '
        Me.chkCleanDB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCleanDB.Location = New System.Drawing.Point(6, 187)
        Me.chkCleanDB.Name = "chkCleanDB"
        Me.chkCleanDB.Size = New System.Drawing.Size(200, 33)
        Me.chkCleanDB.TabIndex = 9
        Me.chkCleanDB.Text = "Clean database after updating library"
        Me.chkCleanDB.UseVisualStyleBackColor = True
        '
        'chkSkipStackedSizeCheck
        '
        Me.chkSkipStackedSizeCheck.AutoSize = True
        Me.chkSkipStackedSizeCheck.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkSkipStackedSizeCheck.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSkipStackedSizeCheck.Location = New System.Drawing.Point(21, 49)
        Me.chkSkipStackedSizeCheck.Name = "chkSkipStackedSizeCheck"
        Me.chkSkipStackedSizeCheck.Size = New System.Drawing.Size(188, 17)
        Me.chkSkipStackedSizeCheck.TabIndex = 3
        Me.chkSkipStackedSizeCheck.Text = "Skip Size Check of Stacked Files"
        Me.chkSkipStackedSizeCheck.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.chkSkipStackedSizeCheck.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(185, 26)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(24, 13)
        Me.Label21.TabIndex = 2
        Me.Label21.Text = "MB"
        '
        'txtSkipLessThan
        '
        Me.txtSkipLessThan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSkipLessThan.Location = New System.Drawing.Point(130, 21)
        Me.txtSkipLessThan.Name = "txtSkipLessThan"
        Me.txtSkipLessThan.Size = New System.Drawing.Size(51, 22)
        Me.txtSkipLessThan.TabIndex = 1
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(4, 26)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(122, 13)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "Skip files smaller than:"
        '
        'pnlTVGeneral
        '
        Me.pnlTVGeneral.BackColor = System.Drawing.Color.White
        Me.pnlTVGeneral.Controls.Add(Me.gbTVListOptions)
        Me.pnlTVGeneral.Controls.Add(Me.gbEpFilter)
        Me.pnlTVGeneral.Controls.Add(Me.gbTVMisc)
        Me.pnlTVGeneral.Controls.Add(Me.gbShowFilter)
        Me.pnlTVGeneral.Location = New System.Drawing.Point(1200, 530)
        Me.pnlTVGeneral.Name = "pnlTVGeneral"
        Me.pnlTVGeneral.Size = New System.Drawing.Size(750, 500)
        Me.pnlTVGeneral.TabIndex = 20
        Me.pnlTVGeneral.Visible = False
        '
        'gbTVListOptions
        '
        Me.gbTVListOptions.Controls.Add(Me.chkDisplayMissingEpisodes)
        Me.gbTVListOptions.Controls.Add(Me.gbEpisodeListOptions)
        Me.gbTVListOptions.Controls.Add(Me.gbSeasonListOptions)
        Me.gbTVListOptions.Controls.Add(Me.gbShowListOptions)
        Me.gbTVListOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTVListOptions.Location = New System.Drawing.Point(6, 150)
        Me.gbTVListOptions.Name = "gbTVListOptions"
        Me.gbTVListOptions.Size = New System.Drawing.Size(219, 245)
        Me.gbTVListOptions.TabIndex = 1
        Me.gbTVListOptions.TabStop = False
        Me.gbTVListOptions.Text = "Media List Options"
        '
        'chkDisplayMissingEpisodes
        '
        Me.chkDisplayMissingEpisodes.AutoSize = True
        Me.chkDisplayMissingEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDisplayMissingEpisodes.Location = New System.Drawing.Point(10, 224)
        Me.chkDisplayMissingEpisodes.Name = "chkDisplayMissingEpisodes"
        Me.chkDisplayMissingEpisodes.Size = New System.Drawing.Size(155, 17)
        Me.chkDisplayMissingEpisodes.TabIndex = 3
        Me.chkDisplayMissingEpisodes.Text = "Display Missing Episodes"
        Me.chkDisplayMissingEpisodes.UseVisualStyleBackColor = True
        '
        'gbEpisodeListOptions
        '
        Me.gbEpisodeListOptions.Controls.Add(Me.chkEpisodeNfoCol)
        Me.gbEpisodeListOptions.Controls.Add(Me.chkEpisodeFanartCol)
        Me.gbEpisodeListOptions.Controls.Add(Me.chkEpisodePosterCol)
        Me.gbEpisodeListOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbEpisodeListOptions.Location = New System.Drawing.Point(10, 143)
        Me.gbEpisodeListOptions.Name = "gbEpisodeListOptions"
        Me.gbEpisodeListOptions.Size = New System.Drawing.Size(199, 68)
        Me.gbEpisodeListOptions.TabIndex = 2
        Me.gbEpisodeListOptions.TabStop = False
        Me.gbEpisodeListOptions.Text = "Episodes"
        '
        'chkEpisodeNfoCol
        '
        Me.chkEpisodeNfoCol.AutoSize = True
        Me.chkEpisodeNfoCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEpisodeNfoCol.Location = New System.Drawing.Point(7, 48)
        Me.chkEpisodeNfoCol.Name = "chkEpisodeNfoCol"
        Me.chkEpisodeNfoCol.Size = New System.Drawing.Size(117, 17)
        Me.chkEpisodeNfoCol.TabIndex = 2
        Me.chkEpisodeNfoCol.Text = "Hide Info Column"
        Me.chkEpisodeNfoCol.UseVisualStyleBackColor = True
        '
        'chkEpisodeFanartCol
        '
        Me.chkEpisodeFanartCol.AutoSize = True
        Me.chkEpisodeFanartCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEpisodeFanartCol.Location = New System.Drawing.Point(7, 32)
        Me.chkEpisodeFanartCol.Name = "chkEpisodeFanartCol"
        Me.chkEpisodeFanartCol.Size = New System.Drawing.Size(129, 17)
        Me.chkEpisodeFanartCol.TabIndex = 1
        Me.chkEpisodeFanartCol.Text = "Hide Fanart Column"
        Me.chkEpisodeFanartCol.UseVisualStyleBackColor = True
        '
        'chkEpisodePosterCol
        '
        Me.chkEpisodePosterCol.AutoSize = True
        Me.chkEpisodePosterCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEpisodePosterCol.Location = New System.Drawing.Point(7, 16)
        Me.chkEpisodePosterCol.Name = "chkEpisodePosterCol"
        Me.chkEpisodePosterCol.Size = New System.Drawing.Size(128, 17)
        Me.chkEpisodePosterCol.TabIndex = 0
        Me.chkEpisodePosterCol.Text = "Hide Poster Column"
        Me.chkEpisodePosterCol.UseVisualStyleBackColor = True
        '
        'gbSeasonListOptions
        '
        Me.gbSeasonListOptions.Controls.Add(Me.chkSeasonFanartCol)
        Me.gbSeasonListOptions.Controls.Add(Me.chkSeasonPosterCol)
        Me.gbSeasonListOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbSeasonListOptions.Location = New System.Drawing.Point(10, 87)
        Me.gbSeasonListOptions.Name = "gbSeasonListOptions"
        Me.gbSeasonListOptions.Size = New System.Drawing.Size(199, 52)
        Me.gbSeasonListOptions.TabIndex = 1
        Me.gbSeasonListOptions.TabStop = False
        Me.gbSeasonListOptions.Text = "Seasons"
        '
        'chkSeasonFanartCol
        '
        Me.chkSeasonFanartCol.AutoSize = True
        Me.chkSeasonFanartCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSeasonFanartCol.Location = New System.Drawing.Point(7, 32)
        Me.chkSeasonFanartCol.Name = "chkSeasonFanartCol"
        Me.chkSeasonFanartCol.Size = New System.Drawing.Size(129, 17)
        Me.chkSeasonFanartCol.TabIndex = 1
        Me.chkSeasonFanartCol.Text = "Hide Fanart Column"
        Me.chkSeasonFanartCol.UseVisualStyleBackColor = True
        '
        'chkSeasonPosterCol
        '
        Me.chkSeasonPosterCol.AutoSize = True
        Me.chkSeasonPosterCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSeasonPosterCol.Location = New System.Drawing.Point(7, 16)
        Me.chkSeasonPosterCol.Name = "chkSeasonPosterCol"
        Me.chkSeasonPosterCol.Size = New System.Drawing.Size(128, 17)
        Me.chkSeasonPosterCol.TabIndex = 0
        Me.chkSeasonPosterCol.Text = "Hide Poster Column"
        Me.chkSeasonPosterCol.UseVisualStyleBackColor = True
        '
        'gbShowListOptions
        '
        Me.gbShowListOptions.Controls.Add(Me.chkShowNfoCol)
        Me.gbShowListOptions.Controls.Add(Me.chkShowFanartCol)
        Me.gbShowListOptions.Controls.Add(Me.chkShowPosterCol)
        Me.gbShowListOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbShowListOptions.Location = New System.Drawing.Point(9, 16)
        Me.gbShowListOptions.Name = "gbShowListOptions"
        Me.gbShowListOptions.Size = New System.Drawing.Size(199, 68)
        Me.gbShowListOptions.TabIndex = 0
        Me.gbShowListOptions.TabStop = False
        Me.gbShowListOptions.Text = "Shows"
        '
        'chkShowNfoCol
        '
        Me.chkShowNfoCol.AutoSize = True
        Me.chkShowNfoCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowNfoCol.Location = New System.Drawing.Point(7, 48)
        Me.chkShowNfoCol.Name = "chkShowNfoCol"
        Me.chkShowNfoCol.Size = New System.Drawing.Size(117, 17)
        Me.chkShowNfoCol.TabIndex = 2
        Me.chkShowNfoCol.Text = "Hide Info Column"
        Me.chkShowNfoCol.UseVisualStyleBackColor = True
        '
        'chkShowFanartCol
        '
        Me.chkShowFanartCol.AutoSize = True
        Me.chkShowFanartCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowFanartCol.Location = New System.Drawing.Point(7, 32)
        Me.chkShowFanartCol.Name = "chkShowFanartCol"
        Me.chkShowFanartCol.Size = New System.Drawing.Size(129, 17)
        Me.chkShowFanartCol.TabIndex = 1
        Me.chkShowFanartCol.Text = "Hide Fanart Column"
        Me.chkShowFanartCol.UseVisualStyleBackColor = True
        '
        'chkShowPosterCol
        '
        Me.chkShowPosterCol.AutoSize = True
        Me.chkShowPosterCol.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowPosterCol.Location = New System.Drawing.Point(7, 16)
        Me.chkShowPosterCol.Name = "chkShowPosterCol"
        Me.chkShowPosterCol.Size = New System.Drawing.Size(128, 17)
        Me.chkShowPosterCol.TabIndex = 0
        Me.chkShowPosterCol.Text = "Hide Poster Column"
        Me.chkShowPosterCol.UseVisualStyleBackColor = True
        '
        'gbEpFilter
        '
        Me.gbEpFilter.Controls.Add(Me.btnResetEpFilter)
        Me.gbEpFilter.Controls.Add(Me.chkNoFilterEpisode)
        Me.gbEpFilter.Controls.Add(Me.btnEpFilterDown)
        Me.gbEpFilter.Controls.Add(Me.btnEpFilterUp)
        Me.gbEpFilter.Controls.Add(Me.chkEpProperCase)
        Me.gbEpFilter.Controls.Add(Me.btnRemoveEpFilter)
        Me.gbEpFilter.Controls.Add(Me.btnAddEpFilter)
        Me.gbEpFilter.Controls.Add(Me.txtEpFilter)
        Me.gbEpFilter.Controls.Add(Me.lstEpFilters)
        Me.gbEpFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbEpFilter.Location = New System.Drawing.Point(229, 185)
        Me.gbEpFilter.Name = "gbEpFilter"
        Me.gbEpFilter.Size = New System.Drawing.Size(382, 205)
        Me.gbEpFilter.TabIndex = 3
        Me.gbEpFilter.TabStop = False
        Me.gbEpFilter.Text = "Episode Folder/File Name Filters"
        '
        'btnResetEpFilter
        '
        Me.btnResetEpFilter.Image = CType(resources.GetObject("btnResetEpFilter.Image"), System.Drawing.Image)
        Me.btnResetEpFilter.Location = New System.Drawing.Point(354, 38)
        Me.btnResetEpFilter.Name = "btnResetEpFilter"
        Me.btnResetEpFilter.Size = New System.Drawing.Size(23, 23)
        Me.btnResetEpFilter.TabIndex = 3
        Me.btnResetEpFilter.UseVisualStyleBackColor = True
        '
        'chkNoFilterEpisode
        '
        Me.chkNoFilterEpisode.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkNoFilterEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkNoFilterEpisode.Location = New System.Drawing.Point(6, 15)
        Me.chkNoFilterEpisode.Name = "chkNoFilterEpisode"
        Me.chkNoFilterEpisode.Size = New System.Drawing.Size(371, 21)
        Me.chkNoFilterEpisode.TabIndex = 0
        Me.chkNoFilterEpisode.Text = "Build Episode Title Instead of Filtering"
        Me.chkNoFilterEpisode.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkNoFilterEpisode.UseVisualStyleBackColor = True
        '
        'btnEpFilterDown
        '
        Me.btnEpFilterDown.Image = CType(resources.GetObject("btnEpFilterDown.Image"), System.Drawing.Image)
        Me.btnEpFilterDown.Location = New System.Drawing.Point(320, 176)
        Me.btnEpFilterDown.Name = "btnEpFilterDown"
        Me.btnEpFilterDown.Size = New System.Drawing.Size(23, 23)
        Me.btnEpFilterDown.TabIndex = 7
        Me.btnEpFilterDown.UseVisualStyleBackColor = True
        '
        'btnEpFilterUp
        '
        Me.btnEpFilterUp.Image = CType(resources.GetObject("btnEpFilterUp.Image"), System.Drawing.Image)
        Me.btnEpFilterUp.Location = New System.Drawing.Point(296, 176)
        Me.btnEpFilterUp.Name = "btnEpFilterUp"
        Me.btnEpFilterUp.Size = New System.Drawing.Size(23, 23)
        Me.btnEpFilterUp.TabIndex = 6
        Me.btnEpFilterUp.UseVisualStyleBackColor = True
        '
        'chkEpProperCase
        '
        Me.chkEpProperCase.AutoSize = True
        Me.chkEpProperCase.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEpProperCase.Location = New System.Drawing.Point(6, 47)
        Me.chkEpProperCase.Name = "chkEpProperCase"
        Me.chkEpProperCase.Size = New System.Drawing.Size(181, 17)
        Me.chkEpProperCase.TabIndex = 1
        Me.chkEpProperCase.Text = "Convert Names to Proper Case"
        Me.chkEpProperCase.UseVisualStyleBackColor = True
        '
        'btnRemoveEpFilter
        '
        Me.btnRemoveEpFilter.Image = CType(resources.GetObject("btnRemoveEpFilter.Image"), System.Drawing.Image)
        Me.btnRemoveEpFilter.Location = New System.Drawing.Point(354, 176)
        Me.btnRemoveEpFilter.Name = "btnRemoveEpFilter"
        Me.btnRemoveEpFilter.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveEpFilter.TabIndex = 8
        Me.btnRemoveEpFilter.UseVisualStyleBackColor = True
        '
        'btnAddEpFilter
        '
        Me.btnAddEpFilter.Image = CType(resources.GetObject("btnAddEpFilter.Image"), System.Drawing.Image)
        Me.btnAddEpFilter.Location = New System.Drawing.Point(260, 176)
        Me.btnAddEpFilter.Name = "btnAddEpFilter"
        Me.btnAddEpFilter.Size = New System.Drawing.Size(23, 23)
        Me.btnAddEpFilter.TabIndex = 5
        Me.btnAddEpFilter.UseVisualStyleBackColor = True
        '
        'txtEpFilter
        '
        Me.txtEpFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEpFilter.Location = New System.Drawing.Point(6, 177)
        Me.txtEpFilter.Name = "txtEpFilter"
        Me.txtEpFilter.Size = New System.Drawing.Size(252, 22)
        Me.txtEpFilter.TabIndex = 4
        '
        'lstEpFilters
        '
        Me.lstEpFilters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstEpFilters.FormattingEnabled = True
        Me.lstEpFilters.Location = New System.Drawing.Point(6, 64)
        Me.lstEpFilters.Name = "lstEpFilters"
        Me.lstEpFilters.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstEpFilters.Size = New System.Drawing.Size(371, 95)
        Me.lstEpFilters.TabIndex = 2
        '
        'gbTVMisc
        '
        Me.gbTVMisc.Controls.Add(Me.chkMarkNewShows)
        Me.gbTVMisc.Controls.Add(Me.chkMarkNewEpisodes)
        Me.gbTVMisc.Controls.Add(Me.chkDisplayAllSeason)
        Me.gbTVMisc.Controls.Add(Me.lblRatingRegion)
        Me.gbTVMisc.Controls.Add(Me.cbRatingRegion)
        Me.gbTVMisc.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTVMisc.Location = New System.Drawing.Point(6, 7)
        Me.gbTVMisc.Name = "gbTVMisc"
        Me.gbTVMisc.Size = New System.Drawing.Size(219, 142)
        Me.gbTVMisc.TabIndex = 0
        Me.gbTVMisc.TabStop = False
        Me.gbTVMisc.Text = "Miscellaneous"
        '
        'chkMarkNewShows
        '
        Me.chkMarkNewShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarkNewShows.Location = New System.Drawing.Point(8, 90)
        Me.chkMarkNewShows.Name = "chkMarkNewShows"
        Me.chkMarkNewShows.Size = New System.Drawing.Size(204, 17)
        Me.chkMarkNewShows.TabIndex = 3
        Me.chkMarkNewShows.Text = "Mark New Shows"
        Me.chkMarkNewShows.UseVisualStyleBackColor = True
        '
        'chkMarkNewEpisodes
        '
        Me.chkMarkNewEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarkNewEpisodes.Location = New System.Drawing.Point(8, 113)
        Me.chkMarkNewEpisodes.Name = "chkMarkNewEpisodes"
        Me.chkMarkNewEpisodes.Size = New System.Drawing.Size(204, 17)
        Me.chkMarkNewEpisodes.TabIndex = 4
        Me.chkMarkNewEpisodes.Text = "Mark New Episodes"
        Me.chkMarkNewEpisodes.UseVisualStyleBackColor = True
        '
        'chkDisplayAllSeason
        '
        Me.chkDisplayAllSeason.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDisplayAllSeason.Location = New System.Drawing.Point(8, 67)
        Me.chkDisplayAllSeason.Name = "chkDisplayAllSeason"
        Me.chkDisplayAllSeason.Size = New System.Drawing.Size(204, 17)
        Me.chkDisplayAllSeason.TabIndex = 2
        Me.chkDisplayAllSeason.Text = "Display All Season Poster"
        Me.chkDisplayAllSeason.UseVisualStyleBackColor = True
        '
        'lblRatingRegion
        '
        Me.lblRatingRegion.AutoSize = True
        Me.lblRatingRegion.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRatingRegion.Location = New System.Drawing.Point(8, 21)
        Me.lblRatingRegion.Name = "lblRatingRegion"
        Me.lblRatingRegion.Size = New System.Drawing.Size(99, 13)
        Me.lblRatingRegion.TabIndex = 0
        Me.lblRatingRegion.Text = "TV Rating Region:"
        '
        'cbRatingRegion
        '
        Me.cbRatingRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRatingRegion.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbRatingRegion.FormattingEnabled = True
        Me.cbRatingRegion.Location = New System.Drawing.Point(8, 36)
        Me.cbRatingRegion.Name = "cbRatingRegion"
        Me.cbRatingRegion.Size = New System.Drawing.Size(163, 21)
        Me.cbRatingRegion.TabIndex = 1
        '
        'gbShowFilter
        '
        Me.gbShowFilter.Controls.Add(Me.btnResetShowFilters)
        Me.gbShowFilter.Controls.Add(Me.btnShowFilterDown)
        Me.gbShowFilter.Controls.Add(Me.btnShowFilterUp)
        Me.gbShowFilter.Controls.Add(Me.chkShowProperCase)
        Me.gbShowFilter.Controls.Add(Me.btnRemoveShowFilter)
        Me.gbShowFilter.Controls.Add(Me.btnAddShowFilter)
        Me.gbShowFilter.Controls.Add(Me.txtShowFilter)
        Me.gbShowFilter.Controls.Add(Me.lstShowFilters)
        Me.gbShowFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbShowFilter.Location = New System.Drawing.Point(229, 7)
        Me.gbShowFilter.Name = "gbShowFilter"
        Me.gbShowFilter.Size = New System.Drawing.Size(382, 175)
        Me.gbShowFilter.TabIndex = 2
        Me.gbShowFilter.TabStop = False
        Me.gbShowFilter.Text = "Show Folder/File Name Filters"
        '
        'btnResetShowFilters
        '
        Me.btnResetShowFilters.Image = CType(resources.GetObject("btnResetShowFilters.Image"), System.Drawing.Image)
        Me.btnResetShowFilters.Location = New System.Drawing.Point(354, 9)
        Me.btnResetShowFilters.Name = "btnResetShowFilters"
        Me.btnResetShowFilters.Size = New System.Drawing.Size(23, 23)
        Me.btnResetShowFilters.TabIndex = 2
        Me.btnResetShowFilters.UseVisualStyleBackColor = True
        '
        'btnShowFilterDown
        '
        Me.btnShowFilterDown.Image = CType(resources.GetObject("btnShowFilterDown.Image"), System.Drawing.Image)
        Me.btnShowFilterDown.Location = New System.Drawing.Point(320, 146)
        Me.btnShowFilterDown.Name = "btnShowFilterDown"
        Me.btnShowFilterDown.Size = New System.Drawing.Size(23, 23)
        Me.btnShowFilterDown.TabIndex = 6
        Me.btnShowFilterDown.UseVisualStyleBackColor = True
        '
        'btnShowFilterUp
        '
        Me.btnShowFilterUp.Image = CType(resources.GetObject("btnShowFilterUp.Image"), System.Drawing.Image)
        Me.btnShowFilterUp.Location = New System.Drawing.Point(296, 146)
        Me.btnShowFilterUp.Name = "btnShowFilterUp"
        Me.btnShowFilterUp.Size = New System.Drawing.Size(23, 23)
        Me.btnShowFilterUp.TabIndex = 5
        Me.btnShowFilterUp.UseVisualStyleBackColor = True
        '
        'chkShowProperCase
        '
        Me.chkShowProperCase.AutoSize = True
        Me.chkShowProperCase.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowProperCase.Location = New System.Drawing.Point(6, 17)
        Me.chkShowProperCase.Name = "chkShowProperCase"
        Me.chkShowProperCase.Size = New System.Drawing.Size(181, 17)
        Me.chkShowProperCase.TabIndex = 0
        Me.chkShowProperCase.Text = "Convert Names to Proper Case"
        Me.chkShowProperCase.UseVisualStyleBackColor = True
        '
        'btnRemoveShowFilter
        '
        Me.btnRemoveShowFilter.Image = CType(resources.GetObject("btnRemoveShowFilter.Image"), System.Drawing.Image)
        Me.btnRemoveShowFilter.Location = New System.Drawing.Point(354, 146)
        Me.btnRemoveShowFilter.Name = "btnRemoveShowFilter"
        Me.btnRemoveShowFilter.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveShowFilter.TabIndex = 7
        Me.btnRemoveShowFilter.UseVisualStyleBackColor = True
        '
        'btnAddShowFilter
        '
        Me.btnAddShowFilter.Image = CType(resources.GetObject("btnAddShowFilter.Image"), System.Drawing.Image)
        Me.btnAddShowFilter.Location = New System.Drawing.Point(260, 146)
        Me.btnAddShowFilter.Name = "btnAddShowFilter"
        Me.btnAddShowFilter.Size = New System.Drawing.Size(23, 23)
        Me.btnAddShowFilter.TabIndex = 4
        Me.btnAddShowFilter.UseVisualStyleBackColor = True
        '
        'txtShowFilter
        '
        Me.txtShowFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShowFilter.Location = New System.Drawing.Point(6, 147)
        Me.txtShowFilter.Name = "txtShowFilter"
        Me.txtShowFilter.Size = New System.Drawing.Size(252, 22)
        Me.txtShowFilter.TabIndex = 3
        '
        'lstShowFilters
        '
        Me.lstShowFilters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstShowFilters.FormattingEnabled = True
        Me.lstShowFilters.Location = New System.Drawing.Point(6, 35)
        Me.lstShowFilters.Name = "lstShowFilters"
        Me.lstShowFilters.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstShowFilters.Size = New System.Drawing.Size(371, 95)
        Me.lstShowFilters.TabIndex = 1
        '
        'pnlTVSources
        '
        Me.pnlTVSources.BackColor = System.Drawing.Color.White
        Me.pnlTVSources.Controls.Add(Me.TabControl2)
        Me.pnlTVSources.Location = New System.Drawing.Point(1350, 350)
        Me.pnlTVSources.Name = "pnlTVSources"
        Me.pnlTVSources.Size = New System.Drawing.Size(750, 500)
        Me.pnlTVSources.TabIndex = 11
        Me.pnlTVSources.Visible = False
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPage3)
        Me.TabControl2.Controls.Add(Me.TabPage4)
        Me.TabControl2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.TabControl2.Location = New System.Drawing.Point(3, 4)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(744, 493)
        Me.TabControl2.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.gbTVFileNaming)
        Me.TabPage3.Controls.Add(Me.lvTVSources)
        Me.TabPage3.Controls.Add(Me.gbMiscTVSourceOpts)
        Me.TabPage3.Controls.Add(Me.btnAddTVSource)
        Me.TabPage3.Controls.Add(Me.btnEditTVSource)
        Me.TabPage3.Controls.Add(Me.btnRemTVSource)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(736, 467)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "General"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'gbTVFileNaming
        '
        Me.gbTVFileNaming.Controls.Add(Me.tbcTVFileNaming)
        Me.gbTVFileNaming.Location = New System.Drawing.Point(163, 110)
        Me.gbTVFileNaming.Name = "gbTVFileNaming"
        Me.gbTVFileNaming.Size = New System.Drawing.Size(567, 351)
        Me.gbTVFileNaming.TabIndex = 6
        Me.gbTVFileNaming.TabStop = False
        Me.gbTVFileNaming.Text = "File Naming"
        '
        'tbcTVFileNaming
        '
        Me.tbcTVFileNaming.Controls.Add(Me.tpTVFileNamingXBMC)
        Me.tbcTVFileNaming.Controls.Add(Me.tpTVFileNamingNMT)
        Me.tbcTVFileNaming.Controls.Add(Me.tpTVFileNamingExpert)
        Me.tbcTVFileNaming.Location = New System.Drawing.Point(6, 18)
        Me.tbcTVFileNaming.Name = "tbcTVFileNaming"
        Me.tbcTVFileNaming.SelectedIndex = 0
        Me.tbcTVFileNaming.Size = New System.Drawing.Size(555, 327)
        Me.tbcTVFileNaming.TabIndex = 0
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
        Me.tpTVFileNamingXBMC.UseVisualStyleBackColor = True
        '
        'gbTVXBMCAdditional
        '
        Me.gbTVXBMCAdditional.Controls.Add(Me.btnTVBrowseTVTheme)
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
        Me.gbTVXBMCAdditional.Size = New System.Drawing.Size(200, 199)
        Me.gbTVXBMCAdditional.TabIndex = 2
        Me.gbTVXBMCAdditional.TabStop = False
        Me.gbTVXBMCAdditional.Text = "Additional Stuff"
        '
        'btnTVBrowseTVTheme
        '
        Me.btnTVBrowseTVTheme.Enabled = False
        Me.btnTVBrowseTVTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTVBrowseTVTheme.Location = New System.Drawing.Point(169, 166)
        Me.btnTVBrowseTVTheme.Name = "btnTVBrowseTVTheme"
        Me.btnTVBrowseTVTheme.Size = New System.Drawing.Size(25, 22)
        Me.btnTVBrowseTVTheme.TabIndex = 7
        Me.btnTVBrowseTVTheme.Text = "..."
        Me.btnTVBrowseTVTheme.UseVisualStyleBackColor = True
        '
        'txtTVShowTVThemeFolderXBMC
        '
        Me.txtTVShowTVThemeFolderXBMC.Enabled = False
        Me.txtTVShowTVThemeFolderXBMC.Location = New System.Drawing.Point(7, 166)
        Me.txtTVShowTVThemeFolderXBMC.Name = "txtTVShowTVThemeFolderXBMC"
        Me.txtTVShowTVThemeFolderXBMC.Size = New System.Drawing.Size(156, 22)
        Me.txtTVShowTVThemeFolderXBMC.TabIndex = 6
        '
        'chkTVShowTVThemeXBMC
        '
        Me.chkTVShowTVThemeXBMC.AutoSize = True
        Me.chkTVShowTVThemeXBMC.Enabled = False
        Me.chkTVShowTVThemeXBMC.Location = New System.Drawing.Point(7, 142)
        Me.chkTVShowTVThemeXBMC.Name = "chkTVShowTVThemeXBMC"
        Me.chkTVShowTVThemeXBMC.Size = New System.Drawing.Size(74, 17)
        Me.chkTVShowTVThemeXBMC.TabIndex = 5
        Me.chkTVShowTVThemeXBMC.Text = "TV Theme"
        Me.chkTVShowTVThemeXBMC.UseVisualStyleBackColor = True
        '
        'chkTVSeasonLandscapeXBMC
        '
        Me.chkTVSeasonLandscapeXBMC.AutoSize = True
        Me.chkTVSeasonLandscapeXBMC.Enabled = False
        Me.chkTVSeasonLandscapeXBMC.Location = New System.Drawing.Point(7, 118)
        Me.chkTVSeasonLandscapeXBMC.Name = "chkTVSeasonLandscapeXBMC"
        Me.chkTVSeasonLandscapeXBMC.Size = New System.Drawing.Size(120, 17)
        Me.chkTVSeasonLandscapeXBMC.TabIndex = 4
        Me.chkTVSeasonLandscapeXBMC.Text = "Season Landscape"
        Me.chkTVSeasonLandscapeXBMC.UseVisualStyleBackColor = True
        '
        'chkTVShowLandscapeXBMC
        '
        Me.chkTVShowLandscapeXBMC.AutoSize = True
        Me.chkTVShowLandscapeXBMC.Enabled = False
        Me.chkTVShowLandscapeXBMC.Location = New System.Drawing.Point(7, 94)
        Me.chkTVShowLandscapeXBMC.Name = "chkTVShowLandscapeXBMC"
        Me.chkTVShowLandscapeXBMC.Size = New System.Drawing.Size(112, 17)
        Me.chkTVShowLandscapeXBMC.TabIndex = 3
        Me.chkTVShowLandscapeXBMC.Text = "Show Landscape"
        Me.chkTVShowLandscapeXBMC.UseVisualStyleBackColor = True
        '
        'chkTVShowCharacterArtXBMC
        '
        Me.chkTVShowCharacterArtXBMC.AutoSize = True
        Me.chkTVShowCharacterArtXBMC.Enabled = False
        Me.chkTVShowCharacterArtXBMC.Location = New System.Drawing.Point(7, 70)
        Me.chkTVShowCharacterArtXBMC.Name = "chkTVShowCharacterArtXBMC"
        Me.chkTVShowCharacterArtXBMC.Size = New System.Drawing.Size(90, 17)
        Me.chkTVShowCharacterArtXBMC.TabIndex = 2
        Me.chkTVShowCharacterArtXBMC.Text = "CharacterArt"
        Me.chkTVShowCharacterArtXBMC.UseVisualStyleBackColor = True
        '
        'chkTVShowClearArtXBMC
        '
        Me.chkTVShowClearArtXBMC.AutoSize = True
        Me.chkTVShowClearArtXBMC.Enabled = False
        Me.chkTVShowClearArtXBMC.Location = New System.Drawing.Point(7, 46)
        Me.chkTVShowClearArtXBMC.Name = "chkTVShowClearArtXBMC"
        Me.chkTVShowClearArtXBMC.Size = New System.Drawing.Size(67, 17)
        Me.chkTVShowClearArtXBMC.TabIndex = 1
        Me.chkTVShowClearArtXBMC.Text = "ClearArt"
        Me.chkTVShowClearArtXBMC.UseVisualStyleBackColor = True
        '
        'chkTVShowClearLogoXBMC
        '
        Me.chkTVShowClearLogoXBMC.AutoSize = True
        Me.chkTVShowClearLogoXBMC.Enabled = False
        Me.chkTVShowClearLogoXBMC.Location = New System.Drawing.Point(7, 22)
        Me.chkTVShowClearLogoXBMC.Name = "chkTVShowClearLogoXBMC"
        Me.chkTVShowClearLogoXBMC.Size = New System.Drawing.Size(78, 17)
        Me.chkTVShowClearLogoXBMC.TabIndex = 0
        Me.chkTVShowClearLogoXBMC.Text = "ClearLogo"
        Me.chkTVShowClearLogoXBMC.UseVisualStyleBackColor = True
        '
        'gbTVFrodo
        '
        Me.gbTVFrodo.Controls.Add(Me.chkTVSeasonPosterFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVShowBannerFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVUseFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVEpisodeActorThumbsFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVSeasonBannerFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVEpisodeFanartFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVEpisodePosterFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVShowActorThumbsFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVSeasonFanartFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVShowFanartFrodo)
        Me.gbTVFrodo.Controls.Add(Me.chkTVShowPosterFrodo)
        Me.gbTVFrodo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTVFrodo.Location = New System.Drawing.Point(6, 6)
        Me.gbTVFrodo.Name = "gbTVFrodo"
        Me.gbTVFrodo.Size = New System.Drawing.Size(152, 289)
        Me.gbTVFrodo.TabIndex = 1
        Me.gbTVFrodo.TabStop = False
        Me.gbTVFrodo.Text = "Frodo"
        '
        'chkTVSeasonPosterFrodo
        '
        Me.chkTVSeasonPosterFrodo.AutoSize = True
        Me.chkTVSeasonPosterFrodo.Enabled = False
        Me.chkTVSeasonPosterFrodo.Location = New System.Drawing.Point(6, 136)
        Me.chkTVSeasonPosterFrodo.Name = "chkTVSeasonPosterFrodo"
        Me.chkTVSeasonPosterFrodo.Size = New System.Drawing.Size(98, 17)
        Me.chkTVSeasonPosterFrodo.TabIndex = 12
        Me.chkTVSeasonPosterFrodo.Text = "Season Poster"
        Me.chkTVSeasonPosterFrodo.UseVisualStyleBackColor = True
        '
        'chkTVShowBannerFrodo
        '
        Me.chkTVShowBannerFrodo.AutoSize = True
        Me.chkTVShowBannerFrodo.Enabled = False
        Me.chkTVShowBannerFrodo.Location = New System.Drawing.Point(6, 113)
        Me.chkTVShowBannerFrodo.Name = "chkTVShowBannerFrodo"
        Me.chkTVShowBannerFrodo.Size = New System.Drawing.Size(95, 17)
        Me.chkTVShowBannerFrodo.TabIndex = 11
        Me.chkTVShowBannerFrodo.Text = "Show Banner"
        Me.chkTVShowBannerFrodo.UseVisualStyleBackColor = True
        '
        'chkTVUseFrodo
        '
        Me.chkTVUseFrodo.AutoSize = True
        Me.chkTVUseFrodo.Location = New System.Drawing.Point(6, 21)
        Me.chkTVUseFrodo.Name = "chkTVUseFrodo"
        Me.chkTVUseFrodo.Size = New System.Drawing.Size(45, 17)
        Me.chkTVUseFrodo.TabIndex = 10
        Me.chkTVUseFrodo.Text = "Use"
        Me.chkTVUseFrodo.UseVisualStyleBackColor = True
        '
        'chkTVEpisodeActorThumbsFrodo
        '
        Me.chkTVEpisodeActorThumbsFrodo.AutoSize = True
        Me.chkTVEpisodeActorThumbsFrodo.Enabled = False
        Me.chkTVEpisodeActorThumbsFrodo.Location = New System.Drawing.Point(6, 251)
        Me.chkTVEpisodeActorThumbsFrodo.Name = "chkTVEpisodeActorThumbsFrodo"
        Me.chkTVEpisodeActorThumbsFrodo.Size = New System.Drawing.Size(140, 17)
        Me.chkTVEpisodeActorThumbsFrodo.TabIndex = 9
        Me.chkTVEpisodeActorThumbsFrodo.Text = "Epsiode Actor Thumbs"
        Me.chkTVEpisodeActorThumbsFrodo.UseVisualStyleBackColor = True
        '
        'chkTVSeasonBannerFrodo
        '
        Me.chkTVSeasonBannerFrodo.AutoSize = True
        Me.chkTVSeasonBannerFrodo.Enabled = False
        Me.chkTVSeasonBannerFrodo.Location = New System.Drawing.Point(6, 182)
        Me.chkTVSeasonBannerFrodo.Name = "chkTVSeasonBannerFrodo"
        Me.chkTVSeasonBannerFrodo.Size = New System.Drawing.Size(103, 17)
        Me.chkTVSeasonBannerFrodo.TabIndex = 8
        Me.chkTVSeasonBannerFrodo.Text = "Season Banner"
        Me.chkTVSeasonBannerFrodo.UseVisualStyleBackColor = True
        '
        'chkTVEpisodeFanartFrodo
        '
        Me.chkTVEpisodeFanartFrodo.AutoSize = True
        Me.chkTVEpisodeFanartFrodo.Enabled = False
        Me.chkTVEpisodeFanartFrodo.Location = New System.Drawing.Point(6, 228)
        Me.chkTVEpisodeFanartFrodo.Name = "chkTVEpisodeFanartFrodo"
        Me.chkTVEpisodeFanartFrodo.Size = New System.Drawing.Size(103, 17)
        Me.chkTVEpisodeFanartFrodo.TabIndex = 6
        Me.chkTVEpisodeFanartFrodo.Text = "Episode Fanart"
        Me.chkTVEpisodeFanartFrodo.UseVisualStyleBackColor = True
        '
        'chkTVEpisodePosterFrodo
        '
        Me.chkTVEpisodePosterFrodo.AutoSize = True
        Me.chkTVEpisodePosterFrodo.Enabled = False
        Me.chkTVEpisodePosterFrodo.Location = New System.Drawing.Point(6, 205)
        Me.chkTVEpisodePosterFrodo.Name = "chkTVEpisodePosterFrodo"
        Me.chkTVEpisodePosterFrodo.Size = New System.Drawing.Size(102, 17)
        Me.chkTVEpisodePosterFrodo.TabIndex = 5
        Me.chkTVEpisodePosterFrodo.Text = "Episode Poster"
        Me.chkTVEpisodePosterFrodo.UseVisualStyleBackColor = True
        '
        'chkTVShowActorThumbsFrodo
        '
        Me.chkTVShowActorThumbsFrodo.AutoSize = True
        Me.chkTVShowActorThumbsFrodo.Enabled = False
        Me.chkTVShowActorThumbsFrodo.Location = New System.Drawing.Point(6, 90)
        Me.chkTVShowActorThumbsFrodo.Name = "chkTVShowActorThumbsFrodo"
        Me.chkTVShowActorThumbsFrodo.Size = New System.Drawing.Size(128, 17)
        Me.chkTVShowActorThumbsFrodo.TabIndex = 4
        Me.chkTVShowActorThumbsFrodo.Text = "Show Actor Thumbs"
        Me.chkTVShowActorThumbsFrodo.UseVisualStyleBackColor = True
        '
        'chkTVSeasonFanartFrodo
        '
        Me.chkTVSeasonFanartFrodo.AutoSize = True
        Me.chkTVSeasonFanartFrodo.Enabled = False
        Me.chkTVSeasonFanartFrodo.Location = New System.Drawing.Point(6, 159)
        Me.chkTVSeasonFanartFrodo.Name = "chkTVSeasonFanartFrodo"
        Me.chkTVSeasonFanartFrodo.Size = New System.Drawing.Size(99, 17)
        Me.chkTVSeasonFanartFrodo.TabIndex = 3
        Me.chkTVSeasonFanartFrodo.Text = "Season Fanart"
        Me.chkTVSeasonFanartFrodo.UseVisualStyleBackColor = True
        '
        'chkTVShowFanartFrodo
        '
        Me.chkTVShowFanartFrodo.AutoSize = True
        Me.chkTVShowFanartFrodo.Enabled = False
        Me.chkTVShowFanartFrodo.Location = New System.Drawing.Point(6, 67)
        Me.chkTVShowFanartFrodo.Name = "chkTVShowFanartFrodo"
        Me.chkTVShowFanartFrodo.Size = New System.Drawing.Size(91, 17)
        Me.chkTVShowFanartFrodo.TabIndex = 2
        Me.chkTVShowFanartFrodo.Text = "Show Fanart"
        Me.chkTVShowFanartFrodo.UseVisualStyleBackColor = True
        '
        'chkTVShowPosterFrodo
        '
        Me.chkTVShowPosterFrodo.AutoSize = True
        Me.chkTVShowPosterFrodo.Enabled = False
        Me.chkTVShowPosterFrodo.Location = New System.Drawing.Point(6, 44)
        Me.chkTVShowPosterFrodo.Name = "chkTVShowPosterFrodo"
        Me.chkTVShowPosterFrodo.Size = New System.Drawing.Size(90, 17)
        Me.chkTVShowPosterFrodo.TabIndex = 1
        Me.chkTVShowPosterFrodo.Text = "Show Poster"
        Me.chkTVShowPosterFrodo.UseVisualStyleBackColor = True
        '
        'tpTVFileNamingNMT
        '
        Me.tpTVFileNamingNMT.Location = New System.Drawing.Point(4, 22)
        Me.tpTVFileNamingNMT.Name = "tpTVFileNamingNMT"
        Me.tpTVFileNamingNMT.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTVFileNamingNMT.Size = New System.Drawing.Size(547, 301)
        Me.tpTVFileNamingNMT.TabIndex = 1
        Me.tpTVFileNamingNMT.Text = "NMT"
        Me.tpTVFileNamingNMT.UseVisualStyleBackColor = True
        '
        'tpTVFileNamingExpert
        '
        Me.tpTVFileNamingExpert.Location = New System.Drawing.Point(4, 22)
        Me.tpTVFileNamingExpert.Name = "tpTVFileNamingExpert"
        Me.tpTVFileNamingExpert.Size = New System.Drawing.Size(547, 301)
        Me.tpTVFileNamingExpert.TabIndex = 2
        Me.tpTVFileNamingExpert.Text = "Expert"
        Me.tpTVFileNamingExpert.UseVisualStyleBackColor = True
        '
        'lvTVSources
        '
        Me.lvTVSources.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lvTVSources.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvTVSources.FullRowSelect = True
        Me.lvTVSources.HideSelection = False
        Me.lvTVSources.Location = New System.Drawing.Point(6, 4)
        Me.lvTVSources.Name = "lvTVSources"
        Me.lvTVSources.Size = New System.Drawing.Size(614, 105)
        Me.lvTVSources.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvTVSources.TabIndex = 0
        Me.lvTVSources.UseCompatibleStateImageBehavior = False
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
        Me.ColumnHeader3.Width = 368
        '
        'gbMiscTVSourceOpts
        '
        Me.gbMiscTVSourceOpts.Controls.Add(Me.Label6)
        Me.gbMiscTVSourceOpts.Controls.Add(Me.txtTVSkipLessThan)
        Me.gbMiscTVSourceOpts.Controls.Add(Me.Label7)
        Me.gbMiscTVSourceOpts.Controls.Add(Me.chkTVScanOrderModify)
        Me.gbMiscTVSourceOpts.Controls.Add(Me.chkTVIgnoreLastScan)
        Me.gbMiscTVSourceOpts.Controls.Add(Me.chkTVCleanDB)
        Me.gbMiscTVSourceOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMiscTVSourceOpts.Location = New System.Drawing.Point(6, 110)
        Me.gbMiscTVSourceOpts.Name = "gbMiscTVSourceOpts"
        Me.gbMiscTVSourceOpts.Size = New System.Drawing.Size(151, 188)
        Me.gbMiscTVSourceOpts.TabIndex = 4
        Me.gbMiscTVSourceOpts.TabStop = False
        Me.gbMiscTVSourceOpts.Text = "Miscellaneous Options"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(114, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(24, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "MB"
        '
        'txtTVSkipLessThan
        '
        Me.txtTVSkipLessThan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVSkipLessThan.Location = New System.Drawing.Point(11, 34)
        Me.txtTVSkipLessThan.Name = "txtTVSkipLessThan"
        Me.txtTVSkipLessThan.Size = New System.Drawing.Size(100, 22)
        Me.txtTVSkipLessThan.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(122, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Skip files smaller than:"
        '
        'chkTVScanOrderModify
        '
        Me.chkTVScanOrderModify.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVScanOrderModify.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVScanOrderModify.Location = New System.Drawing.Point(4, 110)
        Me.chkTVScanOrderModify.Name = "chkTVScanOrderModify"
        Me.chkTVScanOrderModify.Size = New System.Drawing.Size(142, 43)
        Me.chkTVScanOrderModify.TabIndex = 4
        Me.chkTVScanOrderModify.Text = "Scan in order of last write time"
        Me.chkTVScanOrderModify.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVScanOrderModify.UseVisualStyleBackColor = True
        '
        'chkTVIgnoreLastScan
        '
        Me.chkTVIgnoreLastScan.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVIgnoreLastScan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVIgnoreLastScan.Location = New System.Drawing.Point(4, 68)
        Me.chkTVIgnoreLastScan.Name = "chkTVIgnoreLastScan"
        Me.chkTVIgnoreLastScan.Size = New System.Drawing.Size(142, 43)
        Me.chkTVIgnoreLastScan.TabIndex = 3
        Me.chkTVIgnoreLastScan.Text = "Ignore last scan time when updating library"
        Me.chkTVIgnoreLastScan.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVIgnoreLastScan.UseVisualStyleBackColor = True
        '
        'chkTVCleanDB
        '
        Me.chkTVCleanDB.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVCleanDB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVCleanDB.Location = New System.Drawing.Point(4, 152)
        Me.chkTVCleanDB.Name = "chkTVCleanDB"
        Me.chkTVCleanDB.Size = New System.Drawing.Size(142, 43)
        Me.chkTVCleanDB.TabIndex = 5
        Me.chkTVCleanDB.Text = "Clean database after updating library"
        Me.chkTVCleanDB.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVCleanDB.UseVisualStyleBackColor = True
        '
        'btnAddTVSource
        '
        Me.btnAddTVSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnAddTVSource.Image = CType(resources.GetObject("btnAddTVSource.Image"), System.Drawing.Image)
        Me.btnAddTVSource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddTVSource.Location = New System.Drawing.Point(626, 4)
        Me.btnAddTVSource.Name = "btnAddTVSource"
        Me.btnAddTVSource.Size = New System.Drawing.Size(104, 23)
        Me.btnAddTVSource.TabIndex = 1
        Me.btnAddTVSource.Text = "Add Source"
        Me.btnAddTVSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddTVSource.UseVisualStyleBackColor = True
        '
        'btnEditTVSource
        '
        Me.btnEditTVSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnEditTVSource.Image = CType(resources.GetObject("btnEditTVSource.Image"), System.Drawing.Image)
        Me.btnEditTVSource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditTVSource.Location = New System.Drawing.Point(626, 34)
        Me.btnEditTVSource.Name = "btnEditTVSource"
        Me.btnEditTVSource.Size = New System.Drawing.Size(104, 23)
        Me.btnEditTVSource.TabIndex = 2
        Me.btnEditTVSource.Text = "Edit Source"
        Me.btnEditTVSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditTVSource.UseVisualStyleBackColor = True
        '
        'btnRemTVSource
        '
        Me.btnRemTVSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemTVSource.Image = CType(resources.GetObject("btnRemTVSource.Image"), System.Drawing.Image)
        Me.btnRemTVSource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemTVSource.Location = New System.Drawing.Point(626, 86)
        Me.btnRemTVSource.Name = "btnRemTVSource"
        Me.btnRemTVSource.Size = New System.Drawing.Size(104, 23)
        Me.btnRemTVSource.TabIndex = 3
        Me.btnRemTVSource.Text = "Remove"
        Me.btnRemTVSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemTVSource.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.btnGetTVProfiles)
        Me.TabPage4.Controls.Add(Me.btnRegexDown)
        Me.TabPage4.Controls.Add(Me.btnRegexUp)
        Me.TabPage4.Controls.Add(Me.btnResetShowRegex)
        Me.TabPage4.Controls.Add(Me.gbShowRegex)
        Me.TabPage4.Controls.Add(Me.btnEditShowRegex)
        Me.TabPage4.Controls.Add(Me.btnRemoveShowRegex)
        Me.TabPage4.Controls.Add(Me.lvShowRegex)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(736, 467)
        Me.TabPage4.TabIndex = 1
        Me.TabPage4.Text = "Regex"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'btnGetTVProfiles
        '
        Me.btnGetTVProfiles.Image = CType(resources.GetObject("btnGetTVProfiles.Image"), System.Drawing.Image)
        Me.btnGetTVProfiles.Location = New System.Drawing.Point(550, 3)
        Me.btnGetTVProfiles.Name = "btnGetTVProfiles"
        Me.btnGetTVProfiles.Size = New System.Drawing.Size(23, 23)
        Me.btnGetTVProfiles.TabIndex = 1
        Me.btnGetTVProfiles.UseVisualStyleBackColor = True
        '
        'btnRegexDown
        '
        Me.btnRegexDown.Image = CType(resources.GetObject("btnRegexDown.Image"), System.Drawing.Image)
        Me.btnRegexDown.Location = New System.Drawing.Point(304, 167)
        Me.btnRegexDown.Name = "btnRegexDown"
        Me.btnRegexDown.Size = New System.Drawing.Size(23, 23)
        Me.btnRegexDown.TabIndex = 5
        Me.btnRegexDown.UseVisualStyleBackColor = True
        '
        'btnRegexUp
        '
        Me.btnRegexUp.Image = CType(resources.GetObject("btnRegexUp.Image"), System.Drawing.Image)
        Me.btnRegexUp.Location = New System.Drawing.Point(280, 167)
        Me.btnRegexUp.Name = "btnRegexUp"
        Me.btnRegexUp.Size = New System.Drawing.Size(23, 23)
        Me.btnRegexUp.TabIndex = 4
        Me.btnRegexUp.UseVisualStyleBackColor = True
        '
        'btnResetShowRegex
        '
        Me.btnResetShowRegex.Image = CType(resources.GetObject("btnResetShowRegex.Image"), System.Drawing.Image)
        Me.btnResetShowRegex.Location = New System.Drawing.Point(576, 3)
        Me.btnResetShowRegex.Name = "btnResetShowRegex"
        Me.btnResetShowRegex.Size = New System.Drawing.Size(23, 23)
        Me.btnResetShowRegex.TabIndex = 2
        Me.btnResetShowRegex.UseVisualStyleBackColor = True
        '
        'gbShowRegex
        '
        Me.gbShowRegex.Controls.Add(Me.btnClearRegex)
        Me.gbShowRegex.Controls.Add(Me.lblSeasonMatch)
        Me.gbShowRegex.Controls.Add(Me.btnAddShowRegex)
        Me.gbShowRegex.Controls.Add(Me.txtSeasonRegex)
        Me.gbShowRegex.Controls.Add(Me.lblEpisodeRetrieve)
        Me.gbShowRegex.Controls.Add(Me.cboSeasonRetrieve)
        Me.gbShowRegex.Controls.Add(Me.lblSeasonRetrieve)
        Me.gbShowRegex.Controls.Add(Me.txtEpRegex)
        Me.gbShowRegex.Controls.Add(Me.lblEpisodeMatch)
        Me.gbShowRegex.Controls.Add(Me.cboEpRetrieve)
        Me.gbShowRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbShowRegex.Location = New System.Drawing.Point(6, 196)
        Me.gbShowRegex.Name = "gbShowRegex"
        Me.gbShowRegex.Size = New System.Drawing.Size(592, 148)
        Me.gbShowRegex.TabIndex = 7
        Me.gbShowRegex.TabStop = False
        Me.gbShowRegex.Text = "Show Match Regex"
        '
        'btnClearRegex
        '
        Me.btnClearRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClearRegex.Image = CType(resources.GetObject("btnClearRegex.Image"), System.Drawing.Image)
        Me.btnClearRegex.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClearRegex.Location = New System.Drawing.Point(10, 116)
        Me.btnClearRegex.Name = "btnClearRegex"
        Me.btnClearRegex.Size = New System.Drawing.Size(104, 23)
        Me.btnClearRegex.TabIndex = 8
        Me.btnClearRegex.Text = "Clear"
        Me.btnClearRegex.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClearRegex.UseVisualStyleBackColor = True
        '
        'lblSeasonMatch
        '
        Me.lblSeasonMatch.AutoSize = True
        Me.lblSeasonMatch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeasonMatch.Location = New System.Drawing.Point(8, 25)
        Me.lblSeasonMatch.Name = "lblSeasonMatch"
        Me.lblSeasonMatch.Size = New System.Drawing.Size(116, 13)
        Me.lblSeasonMatch.TabIndex = 0
        Me.lblSeasonMatch.Text = "Season Match Regex:"
        '
        'btnAddShowRegex
        '
        Me.btnAddShowRegex.Enabled = False
        Me.btnAddShowRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnAddShowRegex.Image = CType(resources.GetObject("btnAddShowRegex.Image"), System.Drawing.Image)
        Me.btnAddShowRegex.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddShowRegex.Location = New System.Drawing.Point(482, 117)
        Me.btnAddShowRegex.Name = "btnAddShowRegex"
        Me.btnAddShowRegex.Size = New System.Drawing.Size(104, 23)
        Me.btnAddShowRegex.TabIndex = 9
        Me.btnAddShowRegex.Text = "Add Regex"
        Me.btnAddShowRegex.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddShowRegex.UseVisualStyleBackColor = True
        '
        'txtSeasonRegex
        '
        Me.txtSeasonRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeasonRegex.Location = New System.Drawing.Point(10, 40)
        Me.txtSeasonRegex.Name = "txtSeasonRegex"
        Me.txtSeasonRegex.Size = New System.Drawing.Size(417, 22)
        Me.txtSeasonRegex.TabIndex = 1
        '
        'lblEpisodeRetrieve
        '
        Me.lblEpisodeRetrieve.AutoSize = True
        Me.lblEpisodeRetrieve.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEpisodeRetrieve.Location = New System.Drawing.Point(441, 69)
        Me.lblEpisodeRetrieve.Name = "lblEpisodeRetrieve"
        Me.lblEpisodeRetrieve.Size = New System.Drawing.Size(54, 13)
        Me.lblEpisodeRetrieve.TabIndex = 6
        Me.lblEpisodeRetrieve.Text = "Apply To:"
        '
        'cboSeasonRetrieve
        '
        Me.cboSeasonRetrieve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSeasonRetrieve.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSeasonRetrieve.FormattingEnabled = True
        Me.cboSeasonRetrieve.Items.AddRange(New Object() {"Folder Name", "File Name"})
        Me.cboSeasonRetrieve.Location = New System.Drawing.Point(443, 40)
        Me.cboSeasonRetrieve.Name = "cboSeasonRetrieve"
        Me.cboSeasonRetrieve.Size = New System.Drawing.Size(135, 21)
        Me.cboSeasonRetrieve.TabIndex = 3
        '
        'lblSeasonRetrieve
        '
        Me.lblSeasonRetrieve.AutoSize = True
        Me.lblSeasonRetrieve.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeasonRetrieve.Location = New System.Drawing.Point(441, 25)
        Me.lblSeasonRetrieve.Name = "lblSeasonRetrieve"
        Me.lblSeasonRetrieve.Size = New System.Drawing.Size(54, 13)
        Me.lblSeasonRetrieve.TabIndex = 2
        Me.lblSeasonRetrieve.Text = "Apply To:"
        '
        'txtEpRegex
        '
        Me.txtEpRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEpRegex.Location = New System.Drawing.Point(10, 84)
        Me.txtEpRegex.Name = "txtEpRegex"
        Me.txtEpRegex.Size = New System.Drawing.Size(417, 22)
        Me.txtEpRegex.TabIndex = 5
        '
        'lblEpisodeMatch
        '
        Me.lblEpisodeMatch.AutoSize = True
        Me.lblEpisodeMatch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEpisodeMatch.Location = New System.Drawing.Point(8, 69)
        Me.lblEpisodeMatch.Name = "lblEpisodeMatch"
        Me.lblEpisodeMatch.Size = New System.Drawing.Size(120, 13)
        Me.lblEpisodeMatch.TabIndex = 4
        Me.lblEpisodeMatch.Text = "Episode Match Regex:"
        '
        'cboEpRetrieve
        '
        Me.cboEpRetrieve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEpRetrieve.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEpRetrieve.FormattingEnabled = True
        Me.cboEpRetrieve.Items.AddRange(New Object() {"Folder Name", "File Name", "Season Result"})
        Me.cboEpRetrieve.Location = New System.Drawing.Point(443, 84)
        Me.cboEpRetrieve.Name = "cboEpRetrieve"
        Me.cboEpRetrieve.Size = New System.Drawing.Size(135, 21)
        Me.cboEpRetrieve.TabIndex = 7
        '
        'btnEditShowRegex
        '
        Me.btnEditShowRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnEditShowRegex.Image = CType(resources.GetObject("btnEditShowRegex.Image"), System.Drawing.Image)
        Me.btnEditShowRegex.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditShowRegex.Location = New System.Drawing.Point(1, 167)
        Me.btnEditShowRegex.Name = "btnEditShowRegex"
        Me.btnEditShowRegex.Size = New System.Drawing.Size(104, 23)
        Me.btnEditShowRegex.TabIndex = 3
        Me.btnEditShowRegex.Text = "Edit Regex"
        Me.btnEditShowRegex.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditShowRegex.UseVisualStyleBackColor = True
        '
        'btnRemoveShowRegex
        '
        Me.btnRemoveShowRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveShowRegex.Image = CType(resources.GetObject("btnRemoveShowRegex.Image"), System.Drawing.Image)
        Me.btnRemoveShowRegex.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemoveShowRegex.Location = New System.Drawing.Point(494, 167)
        Me.btnRemoveShowRegex.Name = "btnRemoveShowRegex"
        Me.btnRemoveShowRegex.Size = New System.Drawing.Size(104, 23)
        Me.btnRemoveShowRegex.TabIndex = 6
        Me.btnRemoveShowRegex.Text = "Remove"
        Me.btnRemoveShowRegex.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemoveShowRegex.UseVisualStyleBackColor = True
        '
        'lvShowRegex
        '
        Me.lvShowRegex.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colRegID, Me.colSeason, Me.colSeasonApply, Me.colEpisode, Me.colEpApply})
        Me.lvShowRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvShowRegex.FullRowSelect = True
        Me.lvShowRegex.HideSelection = False
        Me.lvShowRegex.Location = New System.Drawing.Point(0, 28)
        Me.lvShowRegex.Name = "lvShowRegex"
        Me.lvShowRegex.Size = New System.Drawing.Size(598, 135)
        Me.lvShowRegex.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvShowRegex.TabIndex = 0
        Me.lvShowRegex.UseCompatibleStateImageBehavior = False
        Me.lvShowRegex.View = System.Windows.Forms.View.Details
        '
        'colRegID
        '
        Me.colRegID.DisplayIndex = 4
        Me.colRegID.Width = 0
        '
        'colSeason
        '
        Me.colSeason.DisplayIndex = 0
        Me.colSeason.Text = "Season Regex"
        Me.colSeason.Width = 224
        '
        'colSeasonApply
        '
        Me.colSeasonApply.DisplayIndex = 1
        Me.colSeasonApply.Text = "Apply To"
        Me.colSeasonApply.Width = 70
        '
        'colEpisode
        '
        Me.colEpisode.DisplayIndex = 2
        Me.colEpisode.Text = "Episode Regex"
        Me.colEpisode.Width = 219
        '
        'colEpApply
        '
        Me.colEpApply.DisplayIndex = 3
        Me.colEpApply.Text = "Apply To"
        Me.colEpApply.Width = 70
        '
        'fbdBrowse
        '
        Me.fbdBrowse.Description = "Select the folder where you wish to store your backdrops."
        '
        'pnlTVImages
        '
        Me.pnlTVImages.BackColor = System.Drawing.Color.White
        Me.pnlTVImages.Controls.Add(Me.TabControl3)
        Me.pnlTVImages.Location = New System.Drawing.Point(1100, 650)
        Me.pnlTVImages.Name = "pnlTVImages"
        Me.pnlTVImages.Size = New System.Drawing.Size(750, 500)
        Me.pnlTVImages.TabIndex = 16
        Me.pnlTVImages.Visible = False
        '
        'TabControl3
        '
        Me.TabControl3.Controls.Add(Me.TabPage5)
        Me.TabControl3.Controls.Add(Me.TabPage6)
        Me.TabControl3.Controls.Add(Me.TabPage7)
        Me.TabControl3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.TabControl3.Location = New System.Drawing.Point(5, 6)
        Me.TabControl3.Name = "TabControl3"
        Me.TabControl3.SelectedIndex = 0
        Me.TabControl3.Size = New System.Drawing.Size(609, 397)
        Me.TabControl3.TabIndex = 0
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.gbAllSPosterOpts)
        Me.TabPage5.Controls.Add(Me.gbShowPosterOpts)
        Me.TabPage5.Controls.Add(Me.gbShowFanartOpts)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(601, 371)
        Me.TabPage5.TabIndex = 0
        Me.TabPage5.Text = "TV Show"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'gbAllSPosterOpts
        '
        Me.gbAllSPosterOpts.Controls.Add(Me.rbAllSPoster)
        Me.gbAllSPosterOpts.Controls.Add(Me.rbAllSBanner)
        Me.gbAllSPosterOpts.Controls.Add(Me.txtAllSPosterWidth)
        Me.gbAllSPosterOpts.Controls.Add(Me.txtAllSPosterHeight)
        Me.gbAllSPosterOpts.Controls.Add(Me.lblAllSPosterQual)
        Me.gbAllSPosterOpts.Controls.Add(Me.tbAllSPosterQual)
        Me.gbAllSPosterOpts.Controls.Add(Me.lblAllSPosterQ)
        Me.gbAllSPosterOpts.Controls.Add(Me.lblAllSPosterWidth)
        Me.gbAllSPosterOpts.Controls.Add(Me.lblAllSPosterHeight)
        Me.gbAllSPosterOpts.Controls.Add(Me.chkResizeAllSPoster)
        Me.gbAllSPosterOpts.Controls.Add(Me.lblAllSPosterSize)
        Me.gbAllSPosterOpts.Controls.Add(Me.cbAllSPosterSize)
        Me.gbAllSPosterOpts.Controls.Add(Me.chkOverwriteAllSPoster)
        Me.gbAllSPosterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbAllSPosterOpts.Location = New System.Drawing.Point(238, 5)
        Me.gbAllSPosterOpts.Name = "gbAllSPosterOpts"
        Me.gbAllSPosterOpts.Size = New System.Drawing.Size(228, 191)
        Me.gbAllSPosterOpts.TabIndex = 0
        Me.gbAllSPosterOpts.TabStop = False
        Me.gbAllSPosterOpts.Text = "All Season Posters"
        '
        'rbAllSPoster
        '
        Me.rbAllSPoster.AutoSize = True
        Me.rbAllSPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbAllSPoster.Location = New System.Drawing.Point(93, 30)
        Me.rbAllSPoster.Name = "rbAllSPoster"
        Me.rbAllSPoster.Size = New System.Drawing.Size(57, 17)
        Me.rbAllSPoster.TabIndex = 2
        Me.rbAllSPoster.TabStop = True
        Me.rbAllSPoster.Text = "Poster"
        Me.rbAllSPoster.UseVisualStyleBackColor = True
        '
        'rbAllSBanner
        '
        Me.rbAllSBanner.AutoSize = True
        Me.rbAllSBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbAllSBanner.Location = New System.Drawing.Point(8, 30)
        Me.rbAllSBanner.Name = "rbAllSBanner"
        Me.rbAllSBanner.Size = New System.Drawing.Size(62, 17)
        Me.rbAllSBanner.TabIndex = 1
        Me.rbAllSBanner.TabStop = True
        Me.rbAllSBanner.Text = "Banner"
        Me.rbAllSBanner.UseVisualStyleBackColor = True
        '
        'txtAllSPosterWidth
        '
        Me.txtAllSPosterWidth.Enabled = False
        Me.txtAllSPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAllSPosterWidth.Location = New System.Drawing.Point(71, 113)
        Me.txtAllSPosterWidth.Name = "txtAllSPosterWidth"
        Me.txtAllSPosterWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtAllSPosterWidth.TabIndex = 7
        '
        'txtAllSPosterHeight
        '
        Me.txtAllSPosterHeight.Enabled = False
        Me.txtAllSPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAllSPosterHeight.Location = New System.Drawing.Point(182, 113)
        Me.txtAllSPosterHeight.Name = "txtAllSPosterHeight"
        Me.txtAllSPosterHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtAllSPosterHeight.TabIndex = 9
        '
        'lblAllSPosterQual
        '
        Me.lblAllSPosterQual.AutoSize = True
        Me.lblAllSPosterQual.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblAllSPosterQual.Location = New System.Drawing.Point(183, 161)
        Me.lblAllSPosterQual.Name = "lblAllSPosterQual"
        Me.lblAllSPosterQual.Size = New System.Drawing.Size(29, 17)
        Me.lblAllSPosterQual.TabIndex = 12
        Me.lblAllSPosterQual.Text = "100"
        '
        'tbAllSPosterQual
        '
        Me.tbAllSPosterQual.AutoSize = False
        Me.tbAllSPosterQual.BackColor = System.Drawing.Color.White
        Me.tbAllSPosterQual.LargeChange = 10
        Me.tbAllSPosterQual.Location = New System.Drawing.Point(7, 154)
        Me.tbAllSPosterQual.Maximum = 100
        Me.tbAllSPosterQual.Name = "tbAllSPosterQual"
        Me.tbAllSPosterQual.Size = New System.Drawing.Size(179, 27)
        Me.tbAllSPosterQual.TabIndex = 11
        Me.tbAllSPosterQual.TickFrequency = 10
        Me.tbAllSPosterQual.Value = 100
        '
        'lblAllSPosterQ
        '
        Me.lblAllSPosterQ.AutoSize = True
        Me.lblAllSPosterQ.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAllSPosterQ.Location = New System.Drawing.Point(3, 141)
        Me.lblAllSPosterQ.Name = "lblAllSPosterQ"
        Me.lblAllSPosterQ.Size = New System.Drawing.Size(46, 13)
        Me.lblAllSPosterQ.TabIndex = 10
        Me.lblAllSPosterQ.Text = "Quality:"
        '
        'lblAllSPosterWidth
        '
        Me.lblAllSPosterWidth.AutoSize = True
        Me.lblAllSPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAllSPosterWidth.Location = New System.Drawing.Point(3, 117)
        Me.lblAllSPosterWidth.Name = "lblAllSPosterWidth"
        Me.lblAllSPosterWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblAllSPosterWidth.TabIndex = 6
        Me.lblAllSPosterWidth.Text = "Max Width:"
        '
        'lblAllSPosterHeight
        '
        Me.lblAllSPosterHeight.AutoSize = True
        Me.lblAllSPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAllSPosterHeight.Location = New System.Drawing.Point(112, 117)
        Me.lblAllSPosterHeight.Name = "lblAllSPosterHeight"
        Me.lblAllSPosterHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblAllSPosterHeight.TabIndex = 8
        Me.lblAllSPosterHeight.Text = "Max Height:"
        '
        'chkResizeAllSPoster
        '
        Me.chkResizeAllSPoster.AutoSize = True
        Me.chkResizeAllSPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkResizeAllSPoster.Location = New System.Drawing.Point(6, 92)
        Me.chkResizeAllSPoster.Name = "chkResizeAllSPoster"
        Me.chkResizeAllSPoster.Size = New System.Drawing.Size(133, 17)
        Me.chkResizeAllSPoster.TabIndex = 5
        Me.chkResizeAllSPoster.Text = "Automatically Resize:"
        Me.chkResizeAllSPoster.UseVisualStyleBackColor = True
        '
        'lblAllSPosterSize
        '
        Me.lblAllSPosterSize.AutoSize = True
        Me.lblAllSPosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAllSPosterSize.Location = New System.Drawing.Point(3, 14)
        Me.lblAllSPosterSize.Name = "lblAllSPosterSize"
        Me.lblAllSPosterSize.Size = New System.Drawing.Size(83, 13)
        Me.lblAllSPosterSize.TabIndex = 0
        Me.lblAllSPosterSize.Text = "Preferred Type:"
        '
        'cbAllSPosterSize
        '
        Me.cbAllSPosterSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbAllSPosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbAllSPosterSize.FormattingEnabled = True
        Me.cbAllSPosterSize.Location = New System.Drawing.Point(6, 52)
        Me.cbAllSPosterSize.Name = "cbAllSPosterSize"
        Me.cbAllSPosterSize.Size = New System.Drawing.Size(148, 21)
        Me.cbAllSPosterSize.TabIndex = 3
        '
        'chkOverwriteAllSPoster
        '
        Me.chkOverwriteAllSPoster.AutoSize = True
        Me.chkOverwriteAllSPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOverwriteAllSPoster.Location = New System.Drawing.Point(6, 76)
        Me.chkOverwriteAllSPoster.Name = "chkOverwriteAllSPoster"
        Me.chkOverwriteAllSPoster.Size = New System.Drawing.Size(119, 17)
        Me.chkOverwriteAllSPoster.TabIndex = 4
        Me.chkOverwriteAllSPoster.Text = "Overwrite Existing"
        Me.chkOverwriteAllSPoster.UseVisualStyleBackColor = True
        '
        'gbShowPosterOpts
        '
        Me.gbShowPosterOpts.Controls.Add(Me.rbPoster)
        Me.gbShowPosterOpts.Controls.Add(Me.rbBanner)
        Me.gbShowPosterOpts.Controls.Add(Me.txtShowPosterWidth)
        Me.gbShowPosterOpts.Controls.Add(Me.txtShowPosterHeight)
        Me.gbShowPosterOpts.Controls.Add(Me.lblShowPosterQual)
        Me.gbShowPosterOpts.Controls.Add(Me.tbShowPosterQual)
        Me.gbShowPosterOpts.Controls.Add(Me.lblShowPosterQ)
        Me.gbShowPosterOpts.Controls.Add(Me.lblShowPosterWidth)
        Me.gbShowPosterOpts.Controls.Add(Me.lblShowPosterHeight)
        Me.gbShowPosterOpts.Controls.Add(Me.chkResizeShowPoster)
        Me.gbShowPosterOpts.Controls.Add(Me.lblShowPosterSize)
        Me.gbShowPosterOpts.Controls.Add(Me.cbShowPosterSize)
        Me.gbShowPosterOpts.Controls.Add(Me.chkOverwriteShowPoster)
        Me.gbShowPosterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbShowPosterOpts.Location = New System.Drawing.Point(4, 5)
        Me.gbShowPosterOpts.Name = "gbShowPosterOpts"
        Me.gbShowPosterOpts.Size = New System.Drawing.Size(228, 191)
        Me.gbShowPosterOpts.TabIndex = 0
        Me.gbShowPosterOpts.TabStop = False
        Me.gbShowPosterOpts.Text = "Poster"
        '
        'rbPoster
        '
        Me.rbPoster.AutoSize = True
        Me.rbPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbPoster.Location = New System.Drawing.Point(93, 30)
        Me.rbPoster.Name = "rbPoster"
        Me.rbPoster.Size = New System.Drawing.Size(57, 17)
        Me.rbPoster.TabIndex = 2
        Me.rbPoster.TabStop = True
        Me.rbPoster.Text = "Poster"
        Me.rbPoster.UseVisualStyleBackColor = True
        '
        'rbBanner
        '
        Me.rbBanner.AutoSize = True
        Me.rbBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbBanner.Location = New System.Drawing.Point(8, 30)
        Me.rbBanner.Name = "rbBanner"
        Me.rbBanner.Size = New System.Drawing.Size(62, 17)
        Me.rbBanner.TabIndex = 1
        Me.rbBanner.TabStop = True
        Me.rbBanner.Text = "Banner"
        Me.rbBanner.UseVisualStyleBackColor = True
        '
        'txtShowPosterWidth
        '
        Me.txtShowPosterWidth.Enabled = False
        Me.txtShowPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShowPosterWidth.Location = New System.Drawing.Point(71, 113)
        Me.txtShowPosterWidth.Name = "txtShowPosterWidth"
        Me.txtShowPosterWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtShowPosterWidth.TabIndex = 7
        '
        'txtShowPosterHeight
        '
        Me.txtShowPosterHeight.Enabled = False
        Me.txtShowPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShowPosterHeight.Location = New System.Drawing.Point(182, 113)
        Me.txtShowPosterHeight.Name = "txtShowPosterHeight"
        Me.txtShowPosterHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtShowPosterHeight.TabIndex = 9
        '
        'lblShowPosterQual
        '
        Me.lblShowPosterQual.AutoSize = True
        Me.lblShowPosterQual.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblShowPosterQual.Location = New System.Drawing.Point(183, 161)
        Me.lblShowPosterQual.Name = "lblShowPosterQual"
        Me.lblShowPosterQual.Size = New System.Drawing.Size(29, 17)
        Me.lblShowPosterQual.TabIndex = 12
        Me.lblShowPosterQual.Text = "100"
        '
        'tbShowPosterQual
        '
        Me.tbShowPosterQual.AutoSize = False
        Me.tbShowPosterQual.BackColor = System.Drawing.Color.White
        Me.tbShowPosterQual.LargeChange = 10
        Me.tbShowPosterQual.Location = New System.Drawing.Point(7, 154)
        Me.tbShowPosterQual.Maximum = 100
        Me.tbShowPosterQual.Name = "tbShowPosterQual"
        Me.tbShowPosterQual.Size = New System.Drawing.Size(179, 27)
        Me.tbShowPosterQual.TabIndex = 11
        Me.tbShowPosterQual.TickFrequency = 10
        Me.tbShowPosterQual.Value = 100
        '
        'lblShowPosterQ
        '
        Me.lblShowPosterQ.AutoSize = True
        Me.lblShowPosterQ.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShowPosterQ.Location = New System.Drawing.Point(3, 141)
        Me.lblShowPosterQ.Name = "lblShowPosterQ"
        Me.lblShowPosterQ.Size = New System.Drawing.Size(46, 13)
        Me.lblShowPosterQ.TabIndex = 10
        Me.lblShowPosterQ.Text = "Quality:"
        '
        'lblShowPosterWidth
        '
        Me.lblShowPosterWidth.AutoSize = True
        Me.lblShowPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShowPosterWidth.Location = New System.Drawing.Point(3, 117)
        Me.lblShowPosterWidth.Name = "lblShowPosterWidth"
        Me.lblShowPosterWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblShowPosterWidth.TabIndex = 6
        Me.lblShowPosterWidth.Text = "Max Width:"
        '
        'lblShowPosterHeight
        '
        Me.lblShowPosterHeight.AutoSize = True
        Me.lblShowPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShowPosterHeight.Location = New System.Drawing.Point(112, 117)
        Me.lblShowPosterHeight.Name = "lblShowPosterHeight"
        Me.lblShowPosterHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblShowPosterHeight.TabIndex = 8
        Me.lblShowPosterHeight.Text = "Max Height:"
        '
        'chkResizeShowPoster
        '
        Me.chkResizeShowPoster.AutoSize = True
        Me.chkResizeShowPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkResizeShowPoster.Location = New System.Drawing.Point(6, 92)
        Me.chkResizeShowPoster.Name = "chkResizeShowPoster"
        Me.chkResizeShowPoster.Size = New System.Drawing.Size(133, 17)
        Me.chkResizeShowPoster.TabIndex = 5
        Me.chkResizeShowPoster.Text = "Automatically Resize:"
        Me.chkResizeShowPoster.UseVisualStyleBackColor = True
        '
        'lblShowPosterSize
        '
        Me.lblShowPosterSize.AutoSize = True
        Me.lblShowPosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShowPosterSize.Location = New System.Drawing.Point(3, 14)
        Me.lblShowPosterSize.Name = "lblShowPosterSize"
        Me.lblShowPosterSize.Size = New System.Drawing.Size(83, 13)
        Me.lblShowPosterSize.TabIndex = 0
        Me.lblShowPosterSize.Text = "Preferred Type:"
        '
        'cbShowPosterSize
        '
        Me.cbShowPosterSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbShowPosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbShowPosterSize.FormattingEnabled = True
        Me.cbShowPosterSize.Location = New System.Drawing.Point(6, 52)
        Me.cbShowPosterSize.Name = "cbShowPosterSize"
        Me.cbShowPosterSize.Size = New System.Drawing.Size(148, 21)
        Me.cbShowPosterSize.TabIndex = 3
        '
        'chkOverwriteShowPoster
        '
        Me.chkOverwriteShowPoster.AutoSize = True
        Me.chkOverwriteShowPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOverwriteShowPoster.Location = New System.Drawing.Point(6, 76)
        Me.chkOverwriteShowPoster.Name = "chkOverwriteShowPoster"
        Me.chkOverwriteShowPoster.Size = New System.Drawing.Size(119, 17)
        Me.chkOverwriteShowPoster.TabIndex = 4
        Me.chkOverwriteShowPoster.Text = "Overwrite Existing"
        Me.chkOverwriteShowPoster.UseVisualStyleBackColor = True
        '
        'gbShowFanartOpts
        '
        Me.gbShowFanartOpts.Controls.Add(Me.txtShowFanartWidth)
        Me.gbShowFanartOpts.Controls.Add(Me.txtShowFanartHeight)
        Me.gbShowFanartOpts.Controls.Add(Me.lblShowFanartQual)
        Me.gbShowFanartOpts.Controls.Add(Me.tbShowFanartQual)
        Me.gbShowFanartOpts.Controls.Add(Me.lblShowFanartQ)
        Me.gbShowFanartOpts.Controls.Add(Me.lblShowFanartWidth)
        Me.gbShowFanartOpts.Controls.Add(Me.lblShowFanartHeight)
        Me.gbShowFanartOpts.Controls.Add(Me.chkResizeShowFanart)
        Me.gbShowFanartOpts.Controls.Add(Me.cbShowFanartSize)
        Me.gbShowFanartOpts.Controls.Add(Me.lblShowFanartSize)
        Me.gbShowFanartOpts.Controls.Add(Me.chkOverwriteShowFanart)
        Me.gbShowFanartOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbShowFanartOpts.Location = New System.Drawing.Point(4, 199)
        Me.gbShowFanartOpts.Name = "gbShowFanartOpts"
        Me.gbShowFanartOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbShowFanartOpts.TabIndex = 1
        Me.gbShowFanartOpts.TabStop = False
        Me.gbShowFanartOpts.Text = "Fanart"
        '
        'txtShowFanartWidth
        '
        Me.txtShowFanartWidth.Enabled = False
        Me.txtShowFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShowFanartWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtShowFanartWidth.Name = "txtShowFanartWidth"
        Me.txtShowFanartWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtShowFanartWidth.TabIndex = 5
        '
        'txtShowFanartHeight
        '
        Me.txtShowFanartHeight.Enabled = False
        Me.txtShowFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShowFanartHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtShowFanartHeight.Name = "txtShowFanartHeight"
        Me.txtShowFanartHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtShowFanartHeight.TabIndex = 7
        '
        'lblShowFanartQual
        '
        Me.lblShowFanartQual.AutoSize = True
        Me.lblShowFanartQual.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblShowFanartQual.Location = New System.Drawing.Point(183, 138)
        Me.lblShowFanartQual.Name = "lblShowFanartQual"
        Me.lblShowFanartQual.Size = New System.Drawing.Size(29, 17)
        Me.lblShowFanartQual.TabIndex = 10
        Me.lblShowFanartQual.Text = "100"
        '
        'tbShowFanartQual
        '
        Me.tbShowFanartQual.AutoSize = False
        Me.tbShowFanartQual.BackColor = System.Drawing.Color.White
        Me.tbShowFanartQual.LargeChange = 10
        Me.tbShowFanartQual.Location = New System.Drawing.Point(7, 131)
        Me.tbShowFanartQual.Maximum = 100
        Me.tbShowFanartQual.Name = "tbShowFanartQual"
        Me.tbShowFanartQual.Size = New System.Drawing.Size(180, 27)
        Me.tbShowFanartQual.TabIndex = 9
        Me.tbShowFanartQual.TickFrequency = 10
        Me.tbShowFanartQual.Value = 100
        '
        'lblShowFanartQ
        '
        Me.lblShowFanartQ.AutoSize = True
        Me.lblShowFanartQ.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShowFanartQ.Location = New System.Drawing.Point(3, 118)
        Me.lblShowFanartQ.Name = "lblShowFanartQ"
        Me.lblShowFanartQ.Size = New System.Drawing.Size(46, 13)
        Me.lblShowFanartQ.TabIndex = 8
        Me.lblShowFanartQ.Text = "Quality:"
        '
        'lblShowFanartWidth
        '
        Me.lblShowFanartWidth.AutoSize = True
        Me.lblShowFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShowFanartWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblShowFanartWidth.Name = "lblShowFanartWidth"
        Me.lblShowFanartWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblShowFanartWidth.TabIndex = 4
        Me.lblShowFanartWidth.Text = "Max Width:"
        '
        'lblShowFanartHeight
        '
        Me.lblShowFanartHeight.AutoSize = True
        Me.lblShowFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShowFanartHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblShowFanartHeight.Name = "lblShowFanartHeight"
        Me.lblShowFanartHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblShowFanartHeight.TabIndex = 6
        Me.lblShowFanartHeight.Text = "Max Height:"
        '
        'chkResizeShowFanart
        '
        Me.chkResizeShowFanart.AutoSize = True
        Me.chkResizeShowFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkResizeShowFanart.Location = New System.Drawing.Point(6, 69)
        Me.chkResizeShowFanart.Name = "chkResizeShowFanart"
        Me.chkResizeShowFanart.Size = New System.Drawing.Size(133, 17)
        Me.chkResizeShowFanart.TabIndex = 3
        Me.chkResizeShowFanart.Text = "Automatically Resize:"
        Me.chkResizeShowFanart.UseVisualStyleBackColor = True
        '
        'cbShowFanartSize
        '
        Me.cbShowFanartSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbShowFanartSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbShowFanartSize.FormattingEnabled = True
        Me.cbShowFanartSize.Location = New System.Drawing.Point(6, 29)
        Me.cbShowFanartSize.Name = "cbShowFanartSize"
        Me.cbShowFanartSize.Size = New System.Drawing.Size(148, 21)
        Me.cbShowFanartSize.TabIndex = 1
        '
        'lblShowFanartSize
        '
        Me.lblShowFanartSize.AutoSize = True
        Me.lblShowFanartSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShowFanartSize.Location = New System.Drawing.Point(3, 14)
        Me.lblShowFanartSize.Name = "lblShowFanartSize"
        Me.lblShowFanartSize.Size = New System.Drawing.Size(80, 13)
        Me.lblShowFanartSize.TabIndex = 0
        Me.lblShowFanartSize.Text = "Preferred Size:"
        '
        'chkOverwriteShowFanart
        '
        Me.chkOverwriteShowFanart.AutoSize = True
        Me.chkOverwriteShowFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOverwriteShowFanart.Location = New System.Drawing.Point(6, 53)
        Me.chkOverwriteShowFanart.Name = "chkOverwriteShowFanart"
        Me.chkOverwriteShowFanart.Size = New System.Drawing.Size(119, 17)
        Me.chkOverwriteShowFanart.TabIndex = 2
        Me.chkOverwriteShowFanart.Text = "Overwrite Existing"
        Me.chkOverwriteShowFanart.UseVisualStyleBackColor = True
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.gbSeaPosterOpts)
        Me.TabPage6.Controls.Add(Me.gbSeaFanartOpts)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(601, 371)
        Me.TabPage6.TabIndex = 2
        Me.TabPage6.Text = "TV Season"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'gbSeaPosterOpts
        '
        Me.gbSeaPosterOpts.Controls.Add(Me.txtSeaPosterWidth)
        Me.gbSeaPosterOpts.Controls.Add(Me.txtSeaPosterHeight)
        Me.gbSeaPosterOpts.Controls.Add(Me.lblSeaPosterQual)
        Me.gbSeaPosterOpts.Controls.Add(Me.tbSeaPosterQual)
        Me.gbSeaPosterOpts.Controls.Add(Me.lblSeaPosterQ)
        Me.gbSeaPosterOpts.Controls.Add(Me.lblSeaPosterWidth)
        Me.gbSeaPosterOpts.Controls.Add(Me.lblSeaPosterHeight)
        Me.gbSeaPosterOpts.Controls.Add(Me.chkSeaResizePoster)
        Me.gbSeaPosterOpts.Controls.Add(Me.lblSeaPosterSize)
        Me.gbSeaPosterOpts.Controls.Add(Me.cbSeaPosterSize)
        Me.gbSeaPosterOpts.Controls.Add(Me.chkSeaOverwritePoster)
        Me.gbSeaPosterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbSeaPosterOpts.Location = New System.Drawing.Point(4, 5)
        Me.gbSeaPosterOpts.Name = "gbSeaPosterOpts"
        Me.gbSeaPosterOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbSeaPosterOpts.TabIndex = 0
        Me.gbSeaPosterOpts.TabStop = False
        Me.gbSeaPosterOpts.Text = "Poster"
        '
        'txtSeaPosterWidth
        '
        Me.txtSeaPosterWidth.Enabled = False
        Me.txtSeaPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeaPosterWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtSeaPosterWidth.Name = "txtSeaPosterWidth"
        Me.txtSeaPosterWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtSeaPosterWidth.TabIndex = 5
        '
        'txtSeaPosterHeight
        '
        Me.txtSeaPosterHeight.Enabled = False
        Me.txtSeaPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeaPosterHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtSeaPosterHeight.Name = "txtSeaPosterHeight"
        Me.txtSeaPosterHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtSeaPosterHeight.TabIndex = 7
        '
        'lblSeaPosterQual
        '
        Me.lblSeaPosterQual.AutoSize = True
        Me.lblSeaPosterQual.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeaPosterQual.Location = New System.Drawing.Point(183, 138)
        Me.lblSeaPosterQual.Name = "lblSeaPosterQual"
        Me.lblSeaPosterQual.Size = New System.Drawing.Size(29, 17)
        Me.lblSeaPosterQual.TabIndex = 10
        Me.lblSeaPosterQual.Text = "100"
        '
        'tbSeaPosterQual
        '
        Me.tbSeaPosterQual.AutoSize = False
        Me.tbSeaPosterQual.BackColor = System.Drawing.Color.White
        Me.tbSeaPosterQual.LargeChange = 10
        Me.tbSeaPosterQual.Location = New System.Drawing.Point(7, 131)
        Me.tbSeaPosterQual.Maximum = 100
        Me.tbSeaPosterQual.Name = "tbSeaPosterQual"
        Me.tbSeaPosterQual.Size = New System.Drawing.Size(179, 27)
        Me.tbSeaPosterQual.TabIndex = 9
        Me.tbSeaPosterQual.TickFrequency = 10
        Me.tbSeaPosterQual.Value = 100
        '
        'lblSeaPosterQ
        '
        Me.lblSeaPosterQ.AutoSize = True
        Me.lblSeaPosterQ.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeaPosterQ.Location = New System.Drawing.Point(3, 118)
        Me.lblSeaPosterQ.Name = "lblSeaPosterQ"
        Me.lblSeaPosterQ.Size = New System.Drawing.Size(46, 13)
        Me.lblSeaPosterQ.TabIndex = 8
        Me.lblSeaPosterQ.Text = "Quality:"
        '
        'lblSeaPosterWidth
        '
        Me.lblSeaPosterWidth.AutoSize = True
        Me.lblSeaPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeaPosterWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblSeaPosterWidth.Name = "lblSeaPosterWidth"
        Me.lblSeaPosterWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblSeaPosterWidth.TabIndex = 4
        Me.lblSeaPosterWidth.Text = "Max Width:"
        '
        'lblSeaPosterHeight
        '
        Me.lblSeaPosterHeight.AutoSize = True
        Me.lblSeaPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeaPosterHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblSeaPosterHeight.Name = "lblSeaPosterHeight"
        Me.lblSeaPosterHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblSeaPosterHeight.TabIndex = 6
        Me.lblSeaPosterHeight.Text = "Max Height:"
        '
        'chkSeaResizePoster
        '
        Me.chkSeaResizePoster.AutoSize = True
        Me.chkSeaResizePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSeaResizePoster.Location = New System.Drawing.Point(6, 69)
        Me.chkSeaResizePoster.Name = "chkSeaResizePoster"
        Me.chkSeaResizePoster.Size = New System.Drawing.Size(133, 17)
        Me.chkSeaResizePoster.TabIndex = 3
        Me.chkSeaResizePoster.Text = "Automatically Resize:"
        Me.chkSeaResizePoster.UseVisualStyleBackColor = True
        '
        'lblSeaPosterSize
        '
        Me.lblSeaPosterSize.AutoSize = True
        Me.lblSeaPosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeaPosterSize.Location = New System.Drawing.Point(3, 14)
        Me.lblSeaPosterSize.Name = "lblSeaPosterSize"
        Me.lblSeaPosterSize.Size = New System.Drawing.Size(83, 13)
        Me.lblSeaPosterSize.TabIndex = 0
        Me.lblSeaPosterSize.Text = "Preferred Type:"
        '
        'cbSeaPosterSize
        '
        Me.cbSeaPosterSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSeaPosterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSeaPosterSize.FormattingEnabled = True
        Me.cbSeaPosterSize.Location = New System.Drawing.Point(6, 29)
        Me.cbSeaPosterSize.Name = "cbSeaPosterSize"
        Me.cbSeaPosterSize.Size = New System.Drawing.Size(148, 21)
        Me.cbSeaPosterSize.TabIndex = 1
        '
        'chkSeaOverwritePoster
        '
        Me.chkSeaOverwritePoster.AutoSize = True
        Me.chkSeaOverwritePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSeaOverwritePoster.Location = New System.Drawing.Point(6, 53)
        Me.chkSeaOverwritePoster.Name = "chkSeaOverwritePoster"
        Me.chkSeaOverwritePoster.Size = New System.Drawing.Size(119, 17)
        Me.chkSeaOverwritePoster.TabIndex = 2
        Me.chkSeaOverwritePoster.Text = "Overwrite Existing"
        Me.chkSeaOverwritePoster.UseVisualStyleBackColor = True
        '
        'gbSeaFanartOpts
        '
        Me.gbSeaFanartOpts.Controls.Add(Me.txtSeaFanartWidth)
        Me.gbSeaFanartOpts.Controls.Add(Me.txtSeaFanartHeight)
        Me.gbSeaFanartOpts.Controls.Add(Me.lblSeaFanartQual)
        Me.gbSeaFanartOpts.Controls.Add(Me.tbSeaFanartQual)
        Me.gbSeaFanartOpts.Controls.Add(Me.lblSeaFanartQ)
        Me.gbSeaFanartOpts.Controls.Add(Me.lblSeaFanartWidth)
        Me.gbSeaFanartOpts.Controls.Add(Me.lblSeaFanartHeight)
        Me.gbSeaFanartOpts.Controls.Add(Me.chkSeaResizeFanart)
        Me.gbSeaFanartOpts.Controls.Add(Me.cbSeaFanartSize)
        Me.gbSeaFanartOpts.Controls.Add(Me.lblSeaFanartSize)
        Me.gbSeaFanartOpts.Controls.Add(Me.chkSeaOverwriteFanart)
        Me.gbSeaFanartOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbSeaFanartOpts.Location = New System.Drawing.Point(238, 5)
        Me.gbSeaFanartOpts.Name = "gbSeaFanartOpts"
        Me.gbSeaFanartOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbSeaFanartOpts.TabIndex = 1
        Me.gbSeaFanartOpts.TabStop = False
        Me.gbSeaFanartOpts.Text = "Fanart"
        '
        'txtSeaFanartWidth
        '
        Me.txtSeaFanartWidth.Enabled = False
        Me.txtSeaFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeaFanartWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtSeaFanartWidth.Name = "txtSeaFanartWidth"
        Me.txtSeaFanartWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtSeaFanartWidth.TabIndex = 5
        '
        'txtSeaFanartHeight
        '
        Me.txtSeaFanartHeight.Enabled = False
        Me.txtSeaFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeaFanartHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtSeaFanartHeight.Name = "txtSeaFanartHeight"
        Me.txtSeaFanartHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtSeaFanartHeight.TabIndex = 7
        '
        'lblSeaFanartQual
        '
        Me.lblSeaFanartQual.AutoSize = True
        Me.lblSeaFanartQual.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeaFanartQual.Location = New System.Drawing.Point(183, 138)
        Me.lblSeaFanartQual.Name = "lblSeaFanartQual"
        Me.lblSeaFanartQual.Size = New System.Drawing.Size(29, 17)
        Me.lblSeaFanartQual.TabIndex = 10
        Me.lblSeaFanartQual.Text = "100"
        '
        'tbSeaFanartQual
        '
        Me.tbSeaFanartQual.AutoSize = False
        Me.tbSeaFanartQual.BackColor = System.Drawing.Color.White
        Me.tbSeaFanartQual.LargeChange = 10
        Me.tbSeaFanartQual.Location = New System.Drawing.Point(7, 131)
        Me.tbSeaFanartQual.Maximum = 100
        Me.tbSeaFanartQual.Name = "tbSeaFanartQual"
        Me.tbSeaFanartQual.Size = New System.Drawing.Size(180, 27)
        Me.tbSeaFanartQual.TabIndex = 9
        Me.tbSeaFanartQual.TickFrequency = 10
        Me.tbSeaFanartQual.Value = 100
        '
        'lblSeaFanartQ
        '
        Me.lblSeaFanartQ.AutoSize = True
        Me.lblSeaFanartQ.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeaFanartQ.Location = New System.Drawing.Point(3, 118)
        Me.lblSeaFanartQ.Name = "lblSeaFanartQ"
        Me.lblSeaFanartQ.Size = New System.Drawing.Size(46, 13)
        Me.lblSeaFanartQ.TabIndex = 8
        Me.lblSeaFanartQ.Text = "Quality:"
        '
        'lblSeaFanartWidth
        '
        Me.lblSeaFanartWidth.AutoSize = True
        Me.lblSeaFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeaFanartWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblSeaFanartWidth.Name = "lblSeaFanartWidth"
        Me.lblSeaFanartWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblSeaFanartWidth.TabIndex = 4
        Me.lblSeaFanartWidth.Text = "Max Width:"
        '
        'lblSeaFanartHeight
        '
        Me.lblSeaFanartHeight.AutoSize = True
        Me.lblSeaFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeaFanartHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblSeaFanartHeight.Name = "lblSeaFanartHeight"
        Me.lblSeaFanartHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblSeaFanartHeight.TabIndex = 6
        Me.lblSeaFanartHeight.Text = "Max Height:"
        '
        'chkSeaResizeFanart
        '
        Me.chkSeaResizeFanart.AutoSize = True
        Me.chkSeaResizeFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSeaResizeFanart.Location = New System.Drawing.Point(6, 69)
        Me.chkSeaResizeFanart.Name = "chkSeaResizeFanart"
        Me.chkSeaResizeFanart.Size = New System.Drawing.Size(133, 17)
        Me.chkSeaResizeFanart.TabIndex = 3
        Me.chkSeaResizeFanart.Text = "Automatically Resize:"
        Me.chkSeaResizeFanart.UseVisualStyleBackColor = True
        '
        'cbSeaFanartSize
        '
        Me.cbSeaFanartSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSeaFanartSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSeaFanartSize.FormattingEnabled = True
        Me.cbSeaFanartSize.Location = New System.Drawing.Point(6, 29)
        Me.cbSeaFanartSize.Name = "cbSeaFanartSize"
        Me.cbSeaFanartSize.Size = New System.Drawing.Size(148, 21)
        Me.cbSeaFanartSize.TabIndex = 1
        '
        'lblSeaFanartSize
        '
        Me.lblSeaFanartSize.AutoSize = True
        Me.lblSeaFanartSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeaFanartSize.Location = New System.Drawing.Point(3, 14)
        Me.lblSeaFanartSize.Name = "lblSeaFanartSize"
        Me.lblSeaFanartSize.Size = New System.Drawing.Size(80, 13)
        Me.lblSeaFanartSize.TabIndex = 0
        Me.lblSeaFanartSize.Text = "Preferred Size:"
        '
        'chkSeaOverwriteFanart
        '
        Me.chkSeaOverwriteFanart.AutoSize = True
        Me.chkSeaOverwriteFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSeaOverwriteFanart.Location = New System.Drawing.Point(6, 53)
        Me.chkSeaOverwriteFanart.Name = "chkSeaOverwriteFanart"
        Me.chkSeaOverwriteFanart.Size = New System.Drawing.Size(119, 17)
        Me.chkSeaOverwriteFanart.TabIndex = 2
        Me.chkSeaOverwriteFanart.Text = "Overwrite Existing"
        Me.chkSeaOverwriteFanart.UseVisualStyleBackColor = True
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.gbEpPosterOpts)
        Me.TabPage7.Controls.Add(Me.gbEpFanartOpts)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage7.Size = New System.Drawing.Size(601, 371)
        Me.TabPage7.TabIndex = 1
        Me.TabPage7.Text = "TV Episode"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'gbEpPosterOpts
        '
        Me.gbEpPosterOpts.Controls.Add(Me.txtEpPosterWidth)
        Me.gbEpPosterOpts.Controls.Add(Me.txtEpPosterHeight)
        Me.gbEpPosterOpts.Controls.Add(Me.lblEpPosterQual)
        Me.gbEpPosterOpts.Controls.Add(Me.tbEpPosterQual)
        Me.gbEpPosterOpts.Controls.Add(Me.lblEpPosterQ)
        Me.gbEpPosterOpts.Controls.Add(Me.lblEpPosterWidth)
        Me.gbEpPosterOpts.Controls.Add(Me.lblEpPosterHeight)
        Me.gbEpPosterOpts.Controls.Add(Me.chkResizeEpPoster)
        Me.gbEpPosterOpts.Controls.Add(Me.chkOverwriteEpPoster)
        Me.gbEpPosterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbEpPosterOpts.Location = New System.Drawing.Point(4, 5)
        Me.gbEpPosterOpts.Name = "gbEpPosterOpts"
        Me.gbEpPosterOpts.Size = New System.Drawing.Size(228, 135)
        Me.gbEpPosterOpts.TabIndex = 0
        Me.gbEpPosterOpts.TabStop = False
        Me.gbEpPosterOpts.Text = "Poster"
        '
        'txtEpPosterWidth
        '
        Me.txtEpPosterWidth.Enabled = False
        Me.txtEpPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEpPosterWidth.Location = New System.Drawing.Point(71, 49)
        Me.txtEpPosterWidth.Name = "txtEpPosterWidth"
        Me.txtEpPosterWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtEpPosterWidth.TabIndex = 3
        '
        'txtEpPosterHeight
        '
        Me.txtEpPosterHeight.Enabled = False
        Me.txtEpPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEpPosterHeight.Location = New System.Drawing.Point(182, 49)
        Me.txtEpPosterHeight.Name = "txtEpPosterHeight"
        Me.txtEpPosterHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtEpPosterHeight.TabIndex = 5
        '
        'lblEpPosterQual
        '
        Me.lblEpPosterQual.AutoSize = True
        Me.lblEpPosterQual.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblEpPosterQual.Location = New System.Drawing.Point(181, 98)
        Me.lblEpPosterQual.Name = "lblEpPosterQual"
        Me.lblEpPosterQual.Size = New System.Drawing.Size(29, 17)
        Me.lblEpPosterQual.TabIndex = 8
        Me.lblEpPosterQual.Text = "100"
        '
        'tbEpPosterQual
        '
        Me.tbEpPosterQual.AutoSize = False
        Me.tbEpPosterQual.BackColor = System.Drawing.Color.White
        Me.tbEpPosterQual.LargeChange = 10
        Me.tbEpPosterQual.Location = New System.Drawing.Point(5, 91)
        Me.tbEpPosterQual.Maximum = 100
        Me.tbEpPosterQual.Name = "tbEpPosterQual"
        Me.tbEpPosterQual.Size = New System.Drawing.Size(179, 27)
        Me.tbEpPosterQual.TabIndex = 7
        Me.tbEpPosterQual.TickFrequency = 10
        Me.tbEpPosterQual.Value = 100
        '
        'lblEpPosterQ
        '
        Me.lblEpPosterQ.AutoSize = True
        Me.lblEpPosterQ.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEpPosterQ.Location = New System.Drawing.Point(1, 78)
        Me.lblEpPosterQ.Name = "lblEpPosterQ"
        Me.lblEpPosterQ.Size = New System.Drawing.Size(46, 13)
        Me.lblEpPosterQ.TabIndex = 6
        Me.lblEpPosterQ.Text = "Quality:"
        '
        'lblEpPosterWidth
        '
        Me.lblEpPosterWidth.AutoSize = True
        Me.lblEpPosterWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEpPosterWidth.Location = New System.Drawing.Point(3, 53)
        Me.lblEpPosterWidth.Name = "lblEpPosterWidth"
        Me.lblEpPosterWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblEpPosterWidth.TabIndex = 2
        Me.lblEpPosterWidth.Text = "Max Width:"
        '
        'lblEpPosterHeight
        '
        Me.lblEpPosterHeight.AutoSize = True
        Me.lblEpPosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEpPosterHeight.Location = New System.Drawing.Point(112, 53)
        Me.lblEpPosterHeight.Name = "lblEpPosterHeight"
        Me.lblEpPosterHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblEpPosterHeight.TabIndex = 4
        Me.lblEpPosterHeight.Text = "Max Height:"
        '
        'chkResizeEpPoster
        '
        Me.chkResizeEpPoster.AutoSize = True
        Me.chkResizeEpPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkResizeEpPoster.Location = New System.Drawing.Point(4, 30)
        Me.chkResizeEpPoster.Name = "chkResizeEpPoster"
        Me.chkResizeEpPoster.Size = New System.Drawing.Size(133, 17)
        Me.chkResizeEpPoster.TabIndex = 1
        Me.chkResizeEpPoster.Text = "Automatically Resize:"
        Me.chkResizeEpPoster.UseVisualStyleBackColor = True
        '
        'chkOverwriteEpPoster
        '
        Me.chkOverwriteEpPoster.AutoSize = True
        Me.chkOverwriteEpPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOverwriteEpPoster.Location = New System.Drawing.Point(4, 14)
        Me.chkOverwriteEpPoster.Name = "chkOverwriteEpPoster"
        Me.chkOverwriteEpPoster.Size = New System.Drawing.Size(119, 17)
        Me.chkOverwriteEpPoster.TabIndex = 0
        Me.chkOverwriteEpPoster.Text = "Overwrite Existing"
        Me.chkOverwriteEpPoster.UseVisualStyleBackColor = True
        '
        'gbEpFanartOpts
        '
        Me.gbEpFanartOpts.Controls.Add(Me.txtEpFanartWidth)
        Me.gbEpFanartOpts.Controls.Add(Me.txtEpFanartHeight)
        Me.gbEpFanartOpts.Controls.Add(Me.lblEpFanartQual)
        Me.gbEpFanartOpts.Controls.Add(Me.tbEpFanartQual)
        Me.gbEpFanartOpts.Controls.Add(Me.lblEpFanartQ)
        Me.gbEpFanartOpts.Controls.Add(Me.lblEpFanartWidth)
        Me.gbEpFanartOpts.Controls.Add(Me.lblEpFanartHeight)
        Me.gbEpFanartOpts.Controls.Add(Me.chkResizeEpFanart)
        Me.gbEpFanartOpts.Controls.Add(Me.cbEpFanartSize)
        Me.gbEpFanartOpts.Controls.Add(Me.lblEpFanartSize)
        Me.gbEpFanartOpts.Controls.Add(Me.chkOverwriteEpFanart)
        Me.gbEpFanartOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbEpFanartOpts.Location = New System.Drawing.Point(238, 5)
        Me.gbEpFanartOpts.Name = "gbEpFanartOpts"
        Me.gbEpFanartOpts.Size = New System.Drawing.Size(228, 167)
        Me.gbEpFanartOpts.TabIndex = 1
        Me.gbEpFanartOpts.TabStop = False
        Me.gbEpFanartOpts.Text = "Fanart"
        '
        'txtEpFanartWidth
        '
        Me.txtEpFanartWidth.Enabled = False
        Me.txtEpFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEpFanartWidth.Location = New System.Drawing.Point(71, 90)
        Me.txtEpFanartWidth.Name = "txtEpFanartWidth"
        Me.txtEpFanartWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtEpFanartWidth.TabIndex = 5
        '
        'txtEpFanartHeight
        '
        Me.txtEpFanartHeight.Enabled = False
        Me.txtEpFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEpFanartHeight.Location = New System.Drawing.Point(182, 90)
        Me.txtEpFanartHeight.Name = "txtEpFanartHeight"
        Me.txtEpFanartHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtEpFanartHeight.TabIndex = 7
        '
        'lblEpFanartQual
        '
        Me.lblEpFanartQual.AutoSize = True
        Me.lblEpFanartQual.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblEpFanartQual.Location = New System.Drawing.Point(183, 138)
        Me.lblEpFanartQual.Name = "lblEpFanartQual"
        Me.lblEpFanartQual.Size = New System.Drawing.Size(29, 17)
        Me.lblEpFanartQual.TabIndex = 10
        Me.lblEpFanartQual.Text = "100"
        '
        'tbEpFanartQual
        '
        Me.tbEpFanartQual.AutoSize = False
        Me.tbEpFanartQual.BackColor = System.Drawing.Color.White
        Me.tbEpFanartQual.LargeChange = 10
        Me.tbEpFanartQual.Location = New System.Drawing.Point(7, 131)
        Me.tbEpFanartQual.Maximum = 100
        Me.tbEpFanartQual.Name = "tbEpFanartQual"
        Me.tbEpFanartQual.Size = New System.Drawing.Size(180, 27)
        Me.tbEpFanartQual.TabIndex = 9
        Me.tbEpFanartQual.TickFrequency = 10
        Me.tbEpFanartQual.Value = 100
        '
        'lblEpFanartQ
        '
        Me.lblEpFanartQ.AutoSize = True
        Me.lblEpFanartQ.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEpFanartQ.Location = New System.Drawing.Point(3, 118)
        Me.lblEpFanartQ.Name = "lblEpFanartQ"
        Me.lblEpFanartQ.Size = New System.Drawing.Size(46, 13)
        Me.lblEpFanartQ.TabIndex = 8
        Me.lblEpFanartQ.Text = "Quality:"
        '
        'lblEpFanartWidth
        '
        Me.lblEpFanartWidth.AutoSize = True
        Me.lblEpFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEpFanartWidth.Location = New System.Drawing.Point(3, 94)
        Me.lblEpFanartWidth.Name = "lblEpFanartWidth"
        Me.lblEpFanartWidth.Size = New System.Drawing.Size(66, 13)
        Me.lblEpFanartWidth.TabIndex = 4
        Me.lblEpFanartWidth.Text = "Max Width:"
        '
        'lblEpFanartHeight
        '
        Me.lblEpFanartHeight.AutoSize = True
        Me.lblEpFanartHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEpFanartHeight.Location = New System.Drawing.Point(112, 94)
        Me.lblEpFanartHeight.Name = "lblEpFanartHeight"
        Me.lblEpFanartHeight.Size = New System.Drawing.Size(69, 13)
        Me.lblEpFanartHeight.TabIndex = 6
        Me.lblEpFanartHeight.Text = "Max Height:"
        '
        'chkResizeEpFanart
        '
        Me.chkResizeEpFanart.AutoSize = True
        Me.chkResizeEpFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkResizeEpFanart.Location = New System.Drawing.Point(6, 69)
        Me.chkResizeEpFanart.Name = "chkResizeEpFanart"
        Me.chkResizeEpFanart.Size = New System.Drawing.Size(133, 17)
        Me.chkResizeEpFanart.TabIndex = 3
        Me.chkResizeEpFanart.Text = "Automatically Resize:"
        Me.chkResizeEpFanart.UseVisualStyleBackColor = True
        '
        'cbEpFanartSize
        '
        Me.cbEpFanartSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEpFanartSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbEpFanartSize.FormattingEnabled = True
        Me.cbEpFanartSize.Location = New System.Drawing.Point(6, 29)
        Me.cbEpFanartSize.Name = "cbEpFanartSize"
        Me.cbEpFanartSize.Size = New System.Drawing.Size(148, 21)
        Me.cbEpFanartSize.TabIndex = 1
        '
        'lblEpFanartSize
        '
        Me.lblEpFanartSize.AutoSize = True
        Me.lblEpFanartSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEpFanartSize.Location = New System.Drawing.Point(3, 14)
        Me.lblEpFanartSize.Name = "lblEpFanartSize"
        Me.lblEpFanartSize.Size = New System.Drawing.Size(80, 13)
        Me.lblEpFanartSize.TabIndex = 0
        Me.lblEpFanartSize.Text = "Preferred Size:"
        '
        'chkOverwriteEpFanart
        '
        Me.chkOverwriteEpFanart.AutoSize = True
        Me.chkOverwriteEpFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOverwriteEpFanart.Location = New System.Drawing.Point(6, 53)
        Me.chkOverwriteEpFanart.Name = "chkOverwriteEpFanart"
        Me.chkOverwriteEpFanart.Size = New System.Drawing.Size(119, 17)
        Me.chkOverwriteEpFanart.TabIndex = 2
        Me.chkOverwriteEpFanart.Text = "Overwrite Existing"
        Me.chkOverwriteEpFanart.UseVisualStyleBackColor = True
        '
        'pnlTVScraper
        '
        Me.pnlTVScraper.BackColor = System.Drawing.Color.White
        Me.pnlTVScraper.Controls.Add(Me.gbTVScraperDuration)
        Me.pnlTVScraper.Controls.Add(Me.GroupBox32)
        Me.pnlTVScraper.Controls.Add(Me.GroupBox33)
        Me.pnlTVScraper.Controls.Add(Me.GroupBox31)
        Me.pnlTVScraper.Controls.Add(Me.gbTVScraperOptions)
        Me.pnlTVScraper.Location = New System.Drawing.Point(1150, 590)
        Me.pnlTVScraper.Name = "pnlTVScraper"
        Me.pnlTVScraper.Size = New System.Drawing.Size(750, 500)
        Me.pnlTVScraper.TabIndex = 19
        Me.pnlTVScraper.Visible = False
        '
        'gbTVScraperDuration
        '
        Me.gbTVScraperDuration.Controls.Add(Me.chkUseEPDuration)
        Me.gbTVScraperDuration.Controls.Add(Me.txtEPRuntimeFormat)
        Me.gbTVScraperDuration.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTVScraperDuration.Location = New System.Drawing.Point(6, 177)
        Me.gbTVScraperDuration.Name = "gbTVScraperDuration"
        Me.gbTVScraperDuration.Size = New System.Drawing.Size(200, 72)
        Me.gbTVScraperDuration.TabIndex = 4
        Me.gbTVScraperDuration.TabStop = False
        Me.gbTVScraperDuration.Text = "Duration Format"
        '
        'chkUseEPDuration
        '
        Me.chkUseEPDuration.AutoSize = True
        Me.chkUseEPDuration.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseEPDuration.Location = New System.Drawing.Point(9, 21)
        Me.chkUseEPDuration.Name = "chkUseEPDuration"
        Me.chkUseEPDuration.Size = New System.Drawing.Size(158, 17)
        Me.chkUseEPDuration.TabIndex = 1
        Me.chkUseEPDuration.Text = "Use Duration for Runtime"
        Me.chkUseEPDuration.UseVisualStyleBackColor = True
        '
        'txtEPRuntimeFormat
        '
        Me.txtEPRuntimeFormat.Location = New System.Drawing.Point(9, 44)
        Me.txtEPRuntimeFormat.Name = "txtEPRuntimeFormat"
        Me.txtEPRuntimeFormat.Size = New System.Drawing.Size(100, 22)
        Me.txtEPRuntimeFormat.TabIndex = 0
        '
        'GroupBox32
        '
        Me.GroupBox32.Controls.Add(Me.GroupBox35)
        Me.GroupBox32.Controls.Add(Me.GroupBox34)
        Me.GroupBox32.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox32.Location = New System.Drawing.Point(3, 255)
        Me.GroupBox32.Name = "GroupBox32"
        Me.GroupBox32.Size = New System.Drawing.Size(403, 114)
        Me.GroupBox32.TabIndex = 3
        Me.GroupBox32.TabStop = False
        Me.GroupBox32.Text = "Scraper Fields"
        '
        'GroupBox35
        '
        Me.GroupBox35.Controls.Add(Me.chkScraperShowRating)
        Me.GroupBox35.Controls.Add(Me.chkScraperShowActors)
        Me.GroupBox35.Controls.Add(Me.chkScraperShowStudio)
        Me.GroupBox35.Controls.Add(Me.chkScraperShowPremiered)
        Me.GroupBox35.Controls.Add(Me.chkScraperShowEGU)
        Me.GroupBox35.Controls.Add(Me.chkScraperShowMPAA)
        Me.GroupBox35.Controls.Add(Me.chkScraperShowPlot)
        Me.GroupBox35.Controls.Add(Me.chkScraperShowGenre)
        Me.GroupBox35.Controls.Add(Me.chkScraperShowTitle)
        Me.GroupBox35.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox35.Location = New System.Drawing.Point(3, 14)
        Me.GroupBox35.Name = "GroupBox35"
        Me.GroupBox35.Size = New System.Drawing.Size(213, 96)
        Me.GroupBox35.TabIndex = 0
        Me.GroupBox35.TabStop = False
        Me.GroupBox35.Text = "Show"
        '
        'chkScraperShowRating
        '
        Me.chkScraperShowRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowRating.Location = New System.Drawing.Point(130, 29)
        Me.chkScraperShowRating.Name = "chkScraperShowRating"
        Me.chkScraperShowRating.Size = New System.Drawing.Size(78, 17)
        Me.chkScraperShowRating.TabIndex = 6
        Me.chkScraperShowRating.Text = "Rating"
        Me.chkScraperShowRating.UseVisualStyleBackColor = True
        '
        'chkScraperShowActors
        '
        Me.chkScraperShowActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowActors.Location = New System.Drawing.Point(130, 61)
        Me.chkScraperShowActors.Name = "chkScraperShowActors"
        Me.chkScraperShowActors.Size = New System.Drawing.Size(78, 17)
        Me.chkScraperShowActors.TabIndex = 8
        Me.chkScraperShowActors.Text = "Actors"
        Me.chkScraperShowActors.UseVisualStyleBackColor = True
        '
        'chkScraperShowStudio
        '
        Me.chkScraperShowStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowStudio.Location = New System.Drawing.Point(130, 45)
        Me.chkScraperShowStudio.Name = "chkScraperShowStudio"
        Me.chkScraperShowStudio.Size = New System.Drawing.Size(78, 17)
        Me.chkScraperShowStudio.TabIndex = 7
        Me.chkScraperShowStudio.Text = "Studio"
        Me.chkScraperShowStudio.UseVisualStyleBackColor = True
        '
        'chkScraperShowPremiered
        '
        Me.chkScraperShowPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowPremiered.Location = New System.Drawing.Point(130, 13)
        Me.chkScraperShowPremiered.Name = "chkScraperShowPremiered"
        Me.chkScraperShowPremiered.Size = New System.Drawing.Size(78, 17)
        Me.chkScraperShowPremiered.TabIndex = 5
        Me.chkScraperShowPremiered.Text = "Premiered"
        Me.chkScraperShowPremiered.UseVisualStyleBackColor = True
        '
        'chkScraperShowEGU
        '
        Me.chkScraperShowEGU.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowEGU.Location = New System.Drawing.Point(6, 29)
        Me.chkScraperShowEGU.Name = "chkScraperShowEGU"
        Me.chkScraperShowEGU.Size = New System.Drawing.Size(118, 17)
        Me.chkScraperShowEGU.TabIndex = 1
        Me.chkScraperShowEGU.Text = "EpisodeGuideURL"
        Me.chkScraperShowEGU.UseVisualStyleBackColor = True
        '
        'chkScraperShowMPAA
        '
        Me.chkScraperShowMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowMPAA.Location = New System.Drawing.Point(6, 61)
        Me.chkScraperShowMPAA.Name = "chkScraperShowMPAA"
        Me.chkScraperShowMPAA.Size = New System.Drawing.Size(119, 17)
        Me.chkScraperShowMPAA.TabIndex = 3
        Me.chkScraperShowMPAA.Text = "MPAA"
        Me.chkScraperShowMPAA.UseVisualStyleBackColor = True
        '
        'chkScraperShowPlot
        '
        Me.chkScraperShowPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowPlot.Location = New System.Drawing.Point(6, 77)
        Me.chkScraperShowPlot.Name = "chkScraperShowPlot"
        Me.chkScraperShowPlot.Size = New System.Drawing.Size(119, 17)
        Me.chkScraperShowPlot.TabIndex = 4
        Me.chkScraperShowPlot.Text = "Plot"
        Me.chkScraperShowPlot.UseVisualStyleBackColor = True
        '
        'chkScraperShowGenre
        '
        Me.chkScraperShowGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowGenre.Location = New System.Drawing.Point(6, 45)
        Me.chkScraperShowGenre.Name = "chkScraperShowGenre"
        Me.chkScraperShowGenre.Size = New System.Drawing.Size(118, 17)
        Me.chkScraperShowGenre.TabIndex = 2
        Me.chkScraperShowGenre.Text = "Genre"
        Me.chkScraperShowGenre.UseVisualStyleBackColor = True
        '
        'chkScraperShowTitle
        '
        Me.chkScraperShowTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowTitle.Location = New System.Drawing.Point(6, 13)
        Me.chkScraperShowTitle.Name = "chkScraperShowTitle"
        Me.chkScraperShowTitle.Size = New System.Drawing.Size(118, 17)
        Me.chkScraperShowTitle.TabIndex = 0
        Me.chkScraperShowTitle.Text = "Title"
        Me.chkScraperShowTitle.UseVisualStyleBackColor = True
        '
        'GroupBox34
        '
        Me.GroupBox34.Controls.Add(Me.chkScraperEpActors)
        Me.GroupBox34.Controls.Add(Me.chkScraperEpCredits)
        Me.GroupBox34.Controls.Add(Me.chkScraperEpDirector)
        Me.GroupBox34.Controls.Add(Me.chkScraperEpPlot)
        Me.GroupBox34.Controls.Add(Me.chkScraperEpRating)
        Me.GroupBox34.Controls.Add(Me.chkScraperEpAired)
        Me.GroupBox34.Controls.Add(Me.chkScraperEpTitle)
        Me.GroupBox34.Controls.Add(Me.chkScraperEpEpisode)
        Me.GroupBox34.Controls.Add(Me.chkScraperEpSeason)
        Me.GroupBox34.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox34.Location = New System.Drawing.Point(219, 14)
        Me.GroupBox34.Name = "GroupBox34"
        Me.GroupBox34.Size = New System.Drawing.Size(181, 96)
        Me.GroupBox34.TabIndex = 1
        Me.GroupBox34.TabStop = False
        Me.GroupBox34.Text = "Episode"
        '
        'chkScraperEpActors
        '
        Me.chkScraperEpActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpActors.Location = New System.Drawing.Point(94, 60)
        Me.chkScraperEpActors.Name = "chkScraperEpActors"
        Me.chkScraperEpActors.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpActors.TabIndex = 0
        Me.chkScraperEpActors.Text = "Actors"
        Me.chkScraperEpActors.UseVisualStyleBackColor = True
        '
        'chkScraperEpCredits
        '
        Me.chkScraperEpCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpCredits.Location = New System.Drawing.Point(94, 44)
        Me.chkScraperEpCredits.Name = "chkScraperEpCredits"
        Me.chkScraperEpCredits.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpCredits.TabIndex = 8
        Me.chkScraperEpCredits.Text = "Credits"
        Me.chkScraperEpCredits.UseVisualStyleBackColor = True
        '
        'chkScraperEpDirector
        '
        Me.chkScraperEpDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpDirector.Location = New System.Drawing.Point(94, 28)
        Me.chkScraperEpDirector.Name = "chkScraperEpDirector"
        Me.chkScraperEpDirector.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpDirector.TabIndex = 7
        Me.chkScraperEpDirector.Text = "Director"
        Me.chkScraperEpDirector.UseVisualStyleBackColor = True
        '
        'chkScraperEpPlot
        '
        Me.chkScraperEpPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpPlot.Location = New System.Drawing.Point(94, 12)
        Me.chkScraperEpPlot.Name = "chkScraperEpPlot"
        Me.chkScraperEpPlot.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpPlot.TabIndex = 6
        Me.chkScraperEpPlot.Text = "Plot"
        Me.chkScraperEpPlot.UseVisualStyleBackColor = True
        '
        'chkScraperEpRating
        '
        Me.chkScraperEpRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpRating.Location = New System.Drawing.Point(6, 77)
        Me.chkScraperEpRating.Name = "chkScraperEpRating"
        Me.chkScraperEpRating.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpRating.TabIndex = 5
        Me.chkScraperEpRating.Text = "Rating"
        Me.chkScraperEpRating.UseVisualStyleBackColor = True
        '
        'chkScraperEpAired
        '
        Me.chkScraperEpAired.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpAired.Location = New System.Drawing.Point(6, 61)
        Me.chkScraperEpAired.Name = "chkScraperEpAired"
        Me.chkScraperEpAired.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpAired.TabIndex = 4
        Me.chkScraperEpAired.Text = "Aired"
        Me.chkScraperEpAired.UseVisualStyleBackColor = True
        '
        'chkScraperEpTitle
        '
        Me.chkScraperEpTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpTitle.Location = New System.Drawing.Point(6, 13)
        Me.chkScraperEpTitle.Name = "chkScraperEpTitle"
        Me.chkScraperEpTitle.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpTitle.TabIndex = 0
        Me.chkScraperEpTitle.Text = "Title"
        Me.chkScraperEpTitle.UseVisualStyleBackColor = True
        '
        'chkScraperEpEpisode
        '
        Me.chkScraperEpEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpEpisode.Location = New System.Drawing.Point(6, 45)
        Me.chkScraperEpEpisode.Name = "chkScraperEpEpisode"
        Me.chkScraperEpEpisode.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpEpisode.TabIndex = 3
        Me.chkScraperEpEpisode.Text = "Episode"
        Me.chkScraperEpEpisode.UseVisualStyleBackColor = True
        '
        'chkScraperEpSeason
        '
        Me.chkScraperEpSeason.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpSeason.Location = New System.Drawing.Point(6, 29)
        Me.chkScraperEpSeason.Name = "chkScraperEpSeason"
        Me.chkScraperEpSeason.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpSeason.TabIndex = 2
        Me.chkScraperEpSeason.Text = "Season"
        Me.chkScraperEpSeason.UseVisualStyleBackColor = True
        '
        'GroupBox33
        '
        Me.GroupBox33.Controls.Add(Me.gbEpLocks)
        Me.GroupBox33.Controls.Add(Me.gbShowLocks)
        Me.GroupBox33.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox33.Location = New System.Drawing.Point(208, 6)
        Me.GroupBox33.Name = "GroupBox33"
        Me.GroupBox33.Size = New System.Drawing.Size(191, 243)
        Me.GroupBox33.TabIndex = 1
        Me.GroupBox33.TabStop = False
        Me.GroupBox33.Text = "Global Locks"
        '
        'gbEpLocks
        '
        Me.gbEpLocks.Controls.Add(Me.chkEpLockTitle)
        Me.gbEpLocks.Controls.Add(Me.chkEpLockRating)
        Me.gbEpLocks.Controls.Add(Me.chkEpLockPlot)
        Me.gbEpLocks.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbEpLocks.Location = New System.Drawing.Point(5, 112)
        Me.gbEpLocks.Name = "gbEpLocks"
        Me.gbEpLocks.Size = New System.Drawing.Size(181, 66)
        Me.gbEpLocks.TabIndex = 1
        Me.gbEpLocks.TabStop = False
        Me.gbEpLocks.Text = "Episode"
        '
        'chkEpLockTitle
        '
        Me.chkEpLockTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEpLockTitle.Location = New System.Drawing.Point(6, 15)
        Me.chkEpLockTitle.Name = "chkEpLockTitle"
        Me.chkEpLockTitle.Size = New System.Drawing.Size(166, 17)
        Me.chkEpLockTitle.TabIndex = 0
        Me.chkEpLockTitle.Text = "Lock Title"
        Me.chkEpLockTitle.UseVisualStyleBackColor = True
        '
        'chkEpLockRating
        '
        Me.chkEpLockRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEpLockRating.Location = New System.Drawing.Point(6, 47)
        Me.chkEpLockRating.Name = "chkEpLockRating"
        Me.chkEpLockRating.Size = New System.Drawing.Size(168, 17)
        Me.chkEpLockRating.TabIndex = 2
        Me.chkEpLockRating.Text = "Lock Rating"
        Me.chkEpLockRating.UseVisualStyleBackColor = True
        '
        'chkEpLockPlot
        '
        Me.chkEpLockPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEpLockPlot.Location = New System.Drawing.Point(6, 31)
        Me.chkEpLockPlot.Name = "chkEpLockPlot"
        Me.chkEpLockPlot.Size = New System.Drawing.Size(166, 17)
        Me.chkEpLockPlot.TabIndex = 1
        Me.chkEpLockPlot.Text = "Lock Plot"
        Me.chkEpLockPlot.UseVisualStyleBackColor = True
        '
        'gbShowLocks
        '
        Me.gbShowLocks.Controls.Add(Me.chkShowLockPlot)
        Me.gbShowLocks.Controls.Add(Me.chkShowLockGenre)
        Me.gbShowLocks.Controls.Add(Me.chkShowLockStudio)
        Me.gbShowLocks.Controls.Add(Me.chkShowLockRating)
        Me.gbShowLocks.Controls.Add(Me.chkShowLockTitle)
        Me.gbShowLocks.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbShowLocks.Location = New System.Drawing.Point(5, 13)
        Me.gbShowLocks.Name = "gbShowLocks"
        Me.gbShowLocks.Size = New System.Drawing.Size(181, 96)
        Me.gbShowLocks.TabIndex = 0
        Me.gbShowLocks.TabStop = False
        Me.gbShowLocks.Text = "Show"
        '
        'chkShowLockPlot
        '
        Me.chkShowLockPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowLockPlot.Location = New System.Drawing.Point(6, 29)
        Me.chkShowLockPlot.Name = "chkShowLockPlot"
        Me.chkShowLockPlot.Size = New System.Drawing.Size(168, 17)
        Me.chkShowLockPlot.TabIndex = 1
        Me.chkShowLockPlot.Text = "Lock Plot"
        Me.chkShowLockPlot.UseVisualStyleBackColor = True
        '
        'chkShowLockGenre
        '
        Me.chkShowLockGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowLockGenre.Location = New System.Drawing.Point(6, 61)
        Me.chkShowLockGenre.Name = "chkShowLockGenre"
        Me.chkShowLockGenre.Size = New System.Drawing.Size(168, 17)
        Me.chkShowLockGenre.TabIndex = 3
        Me.chkShowLockGenre.Text = "Lock Genre"
        Me.chkShowLockGenre.UseVisualStyleBackColor = True
        '
        'chkShowLockStudio
        '
        Me.chkShowLockStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowLockStudio.Location = New System.Drawing.Point(6, 77)
        Me.chkShowLockStudio.Name = "chkShowLockStudio"
        Me.chkShowLockStudio.Size = New System.Drawing.Size(168, 17)
        Me.chkShowLockStudio.TabIndex = 4
        Me.chkShowLockStudio.Text = "Lock Studio"
        Me.chkShowLockStudio.UseVisualStyleBackColor = True
        '
        'chkShowLockRating
        '
        Me.chkShowLockRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowLockRating.Location = New System.Drawing.Point(6, 45)
        Me.chkShowLockRating.Name = "chkShowLockRating"
        Me.chkShowLockRating.Size = New System.Drawing.Size(168, 17)
        Me.chkShowLockRating.TabIndex = 2
        Me.chkShowLockRating.Text = "Lock Rating"
        Me.chkShowLockRating.UseVisualStyleBackColor = True
        '
        'chkShowLockTitle
        '
        Me.chkShowLockTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowLockTitle.Location = New System.Drawing.Point(6, 13)
        Me.chkShowLockTitle.Name = "chkShowLockTitle"
        Me.chkShowLockTitle.Size = New System.Drawing.Size(168, 17)
        Me.chkShowLockTitle.TabIndex = 0
        Me.chkShowLockTitle.Text = "Lock Title"
        Me.chkShowLockTitle.UseVisualStyleBackColor = True
        '
        'GroupBox31
        '
        Me.GroupBox31.Controls.Add(Me.gbTVMIDefaults)
        Me.GroupBox31.Controls.Add(Me.cboTVMetaDataOverlay)
        Me.GroupBox31.Controls.Add(Me.Label50)
        Me.GroupBox31.Controls.Add(Me.chkTVScanMetaData)
        Me.GroupBox31.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox31.Location = New System.Drawing.Point(403, 6)
        Me.GroupBox31.Name = "GroupBox31"
        Me.GroupBox31.Size = New System.Drawing.Size(208, 243)
        Me.GroupBox31.TabIndex = 2
        Me.GroupBox31.TabStop = False
        Me.GroupBox31.Text = "Meta Data"
        '
        'gbTVMIDefaults
        '
        Me.gbTVMIDefaults.Controls.Add(Me.lstTVMetaData)
        Me.gbTVMIDefaults.Controls.Add(Me.txtTVDefFIExt)
        Me.gbTVMIDefaults.Controls.Add(Me.Label49)
        Me.gbTVMIDefaults.Controls.Add(Me.btnRemoveTVMetaDataFT)
        Me.gbTVMIDefaults.Controls.Add(Me.btnEditTVMetaDataFT)
        Me.gbTVMIDefaults.Controls.Add(Me.btnNewTVMetaDataFT)
        Me.gbTVMIDefaults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTVMIDefaults.Location = New System.Drawing.Point(12, 93)
        Me.gbTVMIDefaults.Name = "gbTVMIDefaults"
        Me.gbTVMIDefaults.Size = New System.Drawing.Size(183, 144)
        Me.gbTVMIDefaults.TabIndex = 3
        Me.gbTVMIDefaults.TabStop = False
        Me.gbTVMIDefaults.Text = "Defaults by File Type"
        '
        'lstTVMetaData
        '
        Me.lstTVMetaData.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstTVMetaData.FormattingEnabled = True
        Me.lstTVMetaData.Location = New System.Drawing.Point(10, 16)
        Me.lstTVMetaData.Name = "lstTVMetaData"
        Me.lstTVMetaData.Size = New System.Drawing.Size(165, 95)
        Me.lstTVMetaData.TabIndex = 0
        '
        'txtTVDefFIExt
        '
        Me.txtTVDefFIExt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVDefFIExt.Location = New System.Drawing.Point(73, 116)
        Me.txtTVDefFIExt.Name = "txtTVDefFIExt"
        Me.txtTVDefFIExt.Size = New System.Drawing.Size(35, 22)
        Me.txtTVDefFIExt.TabIndex = 2
        '
        'Label49
        '
        Me.Label49.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(8, 116)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(66, 19)
        Me.Label49.TabIndex = 1
        Me.Label49.Text = "File Type"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnRemoveTVMetaDataFT
        '
        Me.btnRemoveTVMetaDataFT.Enabled = False
        Me.btnRemoveTVMetaDataFT.Image = CType(resources.GetObject("btnRemoveTVMetaDataFT.Image"), System.Drawing.Image)
        Me.btnRemoveTVMetaDataFT.Location = New System.Drawing.Point(153, 115)
        Me.btnRemoveTVMetaDataFT.Name = "btnRemoveTVMetaDataFT"
        Me.btnRemoveTVMetaDataFT.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveTVMetaDataFT.TabIndex = 4
        Me.btnRemoveTVMetaDataFT.UseVisualStyleBackColor = True
        '
        'btnEditTVMetaDataFT
        '
        Me.btnEditTVMetaDataFT.Enabled = False
        Me.btnEditTVMetaDataFT.Image = CType(resources.GetObject("btnEditTVMetaDataFT.Image"), System.Drawing.Image)
        Me.btnEditTVMetaDataFT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditTVMetaDataFT.Location = New System.Drawing.Point(130, 115)
        Me.btnEditTVMetaDataFT.Name = "btnEditTVMetaDataFT"
        Me.btnEditTVMetaDataFT.Size = New System.Drawing.Size(23, 23)
        Me.btnEditTVMetaDataFT.TabIndex = 3
        Me.btnEditTVMetaDataFT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditTVMetaDataFT.UseVisualStyleBackColor = True
        '
        'btnNewTVMetaDataFT
        '
        Me.btnNewTVMetaDataFT.Enabled = False
        Me.btnNewTVMetaDataFT.Image = CType(resources.GetObject("btnNewTVMetaDataFT.Image"), System.Drawing.Image)
        Me.btnNewTVMetaDataFT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNewTVMetaDataFT.Location = New System.Drawing.Point(108, 115)
        Me.btnNewTVMetaDataFT.Name = "btnNewTVMetaDataFT"
        Me.btnNewTVMetaDataFT.Size = New System.Drawing.Size(23, 23)
        Me.btnNewTVMetaDataFT.TabIndex = 29
        Me.btnNewTVMetaDataFT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNewTVMetaDataFT.UseVisualStyleBackColor = True
        '
        'cboTVMetaDataOverlay
        '
        Me.cboTVMetaDataOverlay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTVMetaDataOverlay.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cboTVMetaDataOverlay.FormattingEnabled = True
        Me.cboTVMetaDataOverlay.Location = New System.Drawing.Point(13, 62)
        Me.cboTVMetaDataOverlay.Name = "cboTVMetaDataOverlay"
        Me.cboTVMetaDataOverlay.Size = New System.Drawing.Size(182, 21)
        Me.cboTVMetaDataOverlay.Sorted = True
        Me.cboTVMetaDataOverlay.TabIndex = 2
        '
        'Label50
        '
        Me.Label50.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(4, 35)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(202, 29)
        Me.Label50.TabIndex = 1
        Me.Label50.Text = "Display Overlay if Video Contains an Audio Stream With the Following Language:"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'chkTVScanMetaData
        '
        Me.chkTVScanMetaData.AutoSize = True
        Me.chkTVScanMetaData.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVScanMetaData.Location = New System.Drawing.Point(5, 16)
        Me.chkTVScanMetaData.Name = "chkTVScanMetaData"
        Me.chkTVScanMetaData.Size = New System.Drawing.Size(106, 17)
        Me.chkTVScanMetaData.TabIndex = 0
        Me.chkTVScanMetaData.Text = "Scan Meta Data"
        Me.chkTVScanMetaData.UseVisualStyleBackColor = True
        '
        'gbTVScraperOptions
        '
        Me.gbTVScraperOptions.Controls.Add(Me.lblOrdering)
        Me.gbTVScraperOptions.Controls.Add(Me.cbOrdering)
        Me.gbTVScraperOptions.Controls.Add(Me.lblTVUpdate)
        Me.gbTVScraperOptions.Controls.Add(Me.cboTVUpdate)
        Me.gbTVScraperOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTVScraperOptions.Location = New System.Drawing.Point(5, 6)
        Me.gbTVScraperOptions.Name = "gbTVScraperOptions"
        Me.gbTVScraperOptions.Size = New System.Drawing.Size(200, 128)
        Me.gbTVScraperOptions.TabIndex = 0
        Me.gbTVScraperOptions.TabStop = False
        Me.gbTVScraperOptions.Text = "Options"
        '
        'lblOrdering
        '
        Me.lblOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblOrdering.Location = New System.Drawing.Point(10, 73)
        Me.lblOrdering.Name = "lblOrdering"
        Me.lblOrdering.Size = New System.Drawing.Size(177, 13)
        Me.lblOrdering.TabIndex = 7
        Me.lblOrdering.Text = "Default Episode Ordering:"
        Me.lblOrdering.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cbOrdering
        '
        Me.cbOrdering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbOrdering.FormattingEnabled = True
        Me.cbOrdering.Location = New System.Drawing.Point(15, 89)
        Me.cbOrdering.Name = "cbOrdering"
        Me.cbOrdering.Size = New System.Drawing.Size(166, 21)
        Me.cbOrdering.TabIndex = 8
        '
        'lblTVUpdate
        '
        Me.lblTVUpdate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTVUpdate.Location = New System.Drawing.Point(3, 18)
        Me.lblTVUpdate.Name = "lblTVUpdate"
        Me.lblTVUpdate.Size = New System.Drawing.Size(190, 31)
        Me.lblTVUpdate.TabIndex = 5
        Me.lblTVUpdate.Text = "Re-download Show Information Every:"
        Me.lblTVUpdate.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cboTVUpdate
        '
        Me.cboTVUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTVUpdate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cboTVUpdate.FormattingEnabled = True
        Me.cboTVUpdate.Location = New System.Drawing.Point(15, 49)
        Me.cboTVUpdate.Name = "cboTVUpdate"
        Me.cboTVUpdate.Size = New System.Drawing.Size(166, 21)
        Me.cboTVUpdate.TabIndex = 6
        '
        'gbOptions
        '
        Me.gbOptions.Controls.Add(Me.chkCertification)
        Me.gbOptions.Controls.Add(Me.chkTop250)
        Me.gbOptions.Controls.Add(Me.chkCountry)
        Me.gbOptions.Controls.Add(Me.txtGenreLimit)
        Me.gbOptions.Controls.Add(Me.lblLimit2)
        Me.gbOptions.Controls.Add(Me.txtActorLimit)
        Me.gbOptions.Controls.Add(Me.lblLimit)
        Me.gbOptions.Controls.Add(Me.chkCrew)
        Me.gbOptions.Controls.Add(Me.chkMusicBy)
        Me.gbOptions.Controls.Add(Me.chkProducers)
        Me.gbOptions.Controls.Add(Me.chkWriters)
        Me.gbOptions.Controls.Add(Me.chkStudio)
        Me.gbOptions.Controls.Add(Me.chkRuntime)
        Me.gbOptions.Controls.Add(Me.chkPlot)
        Me.gbOptions.Controls.Add(Me.chkOutline)
        Me.gbOptions.Controls.Add(Me.chkGenre)
        Me.gbOptions.Controls.Add(Me.chkDirector)
        Me.gbOptions.Controls.Add(Me.chkTagline)
        Me.gbOptions.Controls.Add(Me.chkCast)
        Me.gbOptions.Controls.Add(Me.chkVotes)
        Me.gbOptions.Controls.Add(Me.chkTrailer)
        Me.gbOptions.Controls.Add(Me.chkRating)
        Me.gbOptions.Controls.Add(Me.chkRelease)
        Me.gbOptions.Controls.Add(Me.chkMPAA)
        Me.gbOptions.Controls.Add(Me.chkYear)
        Me.gbOptions.Controls.Add(Me.chkTitle)
        Me.gbOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbOptions.Location = New System.Drawing.Point(8, 218)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(302, 179)
        Me.gbOptions.TabIndex = 67
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Scraper Fields - Global"
        '
        'chkCertification
        '
        Me.chkCertification.AutoSize = True
        Me.chkCertification.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCertification.Location = New System.Drawing.Point(6, 76)
        Me.chkCertification.Name = "chkCertification"
        Me.chkCertification.Size = New System.Drawing.Size(89, 17)
        Me.chkCertification.TabIndex = 24
        Me.chkCertification.Text = "Certification"
        Me.chkCertification.UseVisualStyleBackColor = True
        '
        'chkTop250
        '
        Me.chkTop250.AutoSize = True
        Me.chkTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTop250.Location = New System.Drawing.Point(205, 133)
        Me.chkTop250.Name = "chkTop250"
        Me.chkTop250.Size = New System.Drawing.Size(66, 17)
        Me.chkTop250.TabIndex = 23
        Me.chkTop250.Text = "Top 250"
        Me.chkTop250.UseVisualStyleBackColor = True
        '
        'chkCountry
        '
        Me.chkCountry.AutoSize = True
        Me.chkCountry.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCountry.Location = New System.Drawing.Point(205, 152)
        Me.chkCountry.Name = "chkCountry"
        Me.chkCountry.Size = New System.Drawing.Size(67, 17)
        Me.chkCountry.TabIndex = 25
        Me.chkCountry.Text = "Country"
        Me.chkCountry.UseVisualStyleBackColor = True
        '
        'txtGenreLimit
        '
        Me.txtGenreLimit.Enabled = False
        Me.txtGenreLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGenreLimit.Location = New System.Drawing.Point(255, 55)
        Me.txtGenreLimit.Name = "txtGenreLimit"
        Me.txtGenreLimit.Size = New System.Drawing.Size(39, 22)
        Me.txtGenreLimit.TabIndex = 21
        '
        'lblLimit2
        '
        Me.lblLimit2.AutoSize = True
        Me.lblLimit2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLimit2.Location = New System.Drawing.Point(223, 58)
        Me.lblLimit2.Name = "lblLimit2"
        Me.lblLimit2.Size = New System.Drawing.Size(34, 13)
        Me.lblLimit2.TabIndex = 22
        Me.lblLimit2.Text = "Limit:"
        Me.lblLimit2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtActorLimit
        '
        Me.txtActorLimit.Enabled = False
        Me.txtActorLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActorLimit.Location = New System.Drawing.Point(151, 112)
        Me.txtActorLimit.Name = "txtActorLimit"
        Me.txtActorLimit.Size = New System.Drawing.Size(39, 22)
        Me.txtActorLimit.TabIndex = 19
        '
        'lblLimit
        '
        Me.lblLimit.AutoSize = True
        Me.lblLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLimit.Location = New System.Drawing.Point(118, 115)
        Me.lblLimit.Name = "lblLimit"
        Me.lblLimit.Size = New System.Drawing.Size(34, 13)
        Me.lblLimit.TabIndex = 20
        Me.lblLimit.Text = "Limit:"
        Me.lblLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkCrew
        '
        Me.chkCrew.AutoSize = True
        Me.chkCrew.Enabled = False
        Me.chkCrew.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCrew.Location = New System.Drawing.Point(205, 114)
        Me.chkCrew.Name = "chkCrew"
        Me.chkCrew.Size = New System.Drawing.Size(85, 17)
        Me.chkCrew.TabIndex = 18
        Me.chkCrew.Text = "Other Crew"
        Me.chkCrew.UseVisualStyleBackColor = True
        '
        'chkMusicBy
        '
        Me.chkMusicBy.AutoSize = True
        Me.chkMusicBy.Enabled = False
        Me.chkMusicBy.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMusicBy.Location = New System.Drawing.Point(205, 95)
        Me.chkMusicBy.Name = "chkMusicBy"
        Me.chkMusicBy.Size = New System.Drawing.Size(71, 17)
        Me.chkMusicBy.TabIndex = 17
        Me.chkMusicBy.Text = "Music By"
        Me.chkMusicBy.UseVisualStyleBackColor = True
        '
        'chkProducers
        '
        Me.chkProducers.AutoSize = True
        Me.chkProducers.Enabled = False
        Me.chkProducers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkProducers.Location = New System.Drawing.Point(205, 19)
        Me.chkProducers.Name = "chkProducers"
        Me.chkProducers.Size = New System.Drawing.Size(77, 17)
        Me.chkProducers.TabIndex = 16
        Me.chkProducers.Text = "Producers"
        Me.chkProducers.UseVisualStyleBackColor = True
        '
        'chkWriters
        '
        Me.chkWriters.AutoSize = True
        Me.chkWriters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWriters.Location = New System.Drawing.Point(101, 152)
        Me.chkWriters.Name = "chkWriters"
        Me.chkWriters.Size = New System.Drawing.Size(63, 17)
        Me.chkWriters.TabIndex = 15
        Me.chkWriters.Text = "Writers"
        Me.chkWriters.UseVisualStyleBackColor = True
        '
        'chkStudio
        '
        Me.chkStudio.AutoSize = True
        Me.chkStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStudio.Location = New System.Drawing.Point(101, 19)
        Me.chkStudio.Name = "chkStudio"
        Me.chkStudio.Size = New System.Drawing.Size(60, 17)
        Me.chkStudio.TabIndex = 14
        Me.chkStudio.Text = "Studio"
        Me.chkStudio.UseVisualStyleBackColor = True
        '
        'chkRuntime
        '
        Me.chkRuntime.AutoSize = True
        Me.chkRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRuntime.Location = New System.Drawing.Point(6, 114)
        Me.chkRuntime.Name = "chkRuntime"
        Me.chkRuntime.Size = New System.Drawing.Size(69, 17)
        Me.chkRuntime.TabIndex = 13
        Me.chkRuntime.Text = "Runtime"
        Me.chkRuntime.UseVisualStyleBackColor = True
        '
        'chkPlot
        '
        Me.chkPlot.AutoSize = True
        Me.chkPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPlot.Location = New System.Drawing.Point(101, 75)
        Me.chkPlot.Name = "chkPlot"
        Me.chkPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkPlot.TabIndex = 12
        Me.chkPlot.Text = "Plot"
        Me.chkPlot.UseVisualStyleBackColor = True
        '
        'chkOutline
        '
        Me.chkOutline.AutoSize = True
        Me.chkOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOutline.Location = New System.Drawing.Point(101, 57)
        Me.chkOutline.Name = "chkOutline"
        Me.chkOutline.Size = New System.Drawing.Size(65, 17)
        Me.chkOutline.TabIndex = 11
        Me.chkOutline.Text = "Outline"
        Me.chkOutline.UseVisualStyleBackColor = True
        '
        'chkGenre
        '
        Me.chkGenre.AutoSize = True
        Me.chkGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGenre.Location = New System.Drawing.Point(205, 38)
        Me.chkGenre.Name = "chkGenre"
        Me.chkGenre.Size = New System.Drawing.Size(57, 17)
        Me.chkGenre.TabIndex = 10
        Me.chkGenre.Text = "Genre"
        Me.chkGenre.UseVisualStyleBackColor = True
        '
        'chkDirector
        '
        Me.chkDirector.AutoSize = True
        Me.chkDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDirector.Location = New System.Drawing.Point(101, 133)
        Me.chkDirector.Name = "chkDirector"
        Me.chkDirector.Size = New System.Drawing.Size(67, 17)
        Me.chkDirector.TabIndex = 9
        Me.chkDirector.Text = "Director"
        Me.chkDirector.UseVisualStyleBackColor = True
        '
        'chkTagline
        '
        Me.chkTagline.AutoSize = True
        Me.chkTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTagline.Location = New System.Drawing.Point(101, 38)
        Me.chkTagline.Name = "chkTagline"
        Me.chkTagline.Size = New System.Drawing.Size(63, 17)
        Me.chkTagline.TabIndex = 8
        Me.chkTagline.Text = "Tagline"
        Me.chkTagline.UseVisualStyleBackColor = True
        '
        'chkCast
        '
        Me.chkCast.AutoSize = True
        Me.chkCast.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCast.Location = New System.Drawing.Point(101, 95)
        Me.chkCast.Name = "chkCast"
        Me.chkCast.Size = New System.Drawing.Size(48, 17)
        Me.chkCast.TabIndex = 7
        Me.chkCast.Text = "Cast"
        Me.chkCast.UseVisualStyleBackColor = True
        '
        'chkVotes
        '
        Me.chkVotes.AutoSize = True
        Me.chkVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVotes.Location = New System.Drawing.Point(6, 152)
        Me.chkVotes.Name = "chkVotes"
        Me.chkVotes.Size = New System.Drawing.Size(55, 17)
        Me.chkVotes.TabIndex = 6
        Me.chkVotes.Text = "Votes"
        Me.chkVotes.UseVisualStyleBackColor = True
        '
        'chkTrailer
        '
        Me.chkTrailer.AutoSize = True
        Me.chkTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTrailer.Location = New System.Drawing.Point(205, 76)
        Me.chkTrailer.Name = "chkTrailer"
        Me.chkTrailer.Size = New System.Drawing.Size(57, 17)
        Me.chkTrailer.TabIndex = 5
        Me.chkTrailer.Text = "Trailer"
        Me.chkTrailer.UseVisualStyleBackColor = True
        '
        'chkRating
        '
        Me.chkRating.AutoSize = True
        Me.chkRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRating.Location = New System.Drawing.Point(6, 133)
        Me.chkRating.Name = "chkRating"
        Me.chkRating.Size = New System.Drawing.Size(60, 17)
        Me.chkRating.TabIndex = 4
        Me.chkRating.Text = "Rating"
        Me.chkRating.UseVisualStyleBackColor = True
        '
        'chkRelease
        '
        Me.chkRelease.AutoSize = True
        Me.chkRelease.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRelease.Location = New System.Drawing.Point(6, 95)
        Me.chkRelease.Name = "chkRelease"
        Me.chkRelease.Size = New System.Drawing.Size(92, 17)
        Me.chkRelease.TabIndex = 3
        Me.chkRelease.Text = "Release Date"
        Me.chkRelease.UseVisualStyleBackColor = True
        '
        'chkMPAA
        '
        Me.chkMPAA.AutoSize = True
        Me.chkMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMPAA.Location = New System.Drawing.Point(6, 57)
        Me.chkMPAA.Name = "chkMPAA"
        Me.chkMPAA.Size = New System.Drawing.Size(56, 17)
        Me.chkMPAA.TabIndex = 2
        Me.chkMPAA.Text = "MPAA"
        Me.chkMPAA.UseVisualStyleBackColor = True
        '
        'chkYear
        '
        Me.chkYear.AutoSize = True
        Me.chkYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkYear.Location = New System.Drawing.Point(6, 38)
        Me.chkYear.Name = "chkYear"
        Me.chkYear.Size = New System.Drawing.Size(47, 17)
        Me.chkYear.TabIndex = 1
        Me.chkYear.Text = "Year"
        Me.chkYear.UseVisualStyleBackColor = True
        '
        'chkTitle
        '
        Me.chkTitle.AutoSize = True
        Me.chkTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTitle.Location = New System.Drawing.Point(6, 19)
        Me.chkTitle.Name = "chkTitle"
        Me.chkTitle.Size = New System.Drawing.Size(47, 17)
        Me.chkTitle.TabIndex = 0
        Me.chkTitle.Text = "Title"
        Me.chkTitle.UseVisualStyleBackColor = True
        '
        'lblLimit3
        '
        Me.lblLimit3.AutoSize = True
        Me.lblLimit3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLimit3.Location = New System.Drawing.Point(203, 105)
        Me.lblLimit3.Name = "lblLimit3"
        Me.lblLimit3.Size = New System.Drawing.Size(34, 13)
        Me.lblLimit3.TabIndex = 70
        Me.lblLimit3.Text = "Limit:"
        Me.lblLimit3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOutlineLimit
        '
        Me.txtOutlineLimit.Enabled = False
        Me.txtOutlineLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOutlineLimit.Location = New System.Drawing.Point(240, 103)
        Me.txtOutlineLimit.Name = "txtOutlineLimit"
        Me.txtOutlineLimit.Size = New System.Drawing.Size(39, 22)
        Me.txtOutlineLimit.TabIndex = 69
        '
        'GroupBox26
        '
        Me.GroupBox26.Controls.Add(Me.GroupBox28)
        Me.GroupBox26.Controls.Add(Me.chkIFOScan)
        Me.GroupBox26.Controls.Add(Me.cbLanguages)
        Me.GroupBox26.Controls.Add(Me.Label31)
        Me.GroupBox26.Controls.Add(Me.gbRTFormat)
        Me.GroupBox26.Controls.Add(Me.chkScanMediaInfo)
        Me.GroupBox26.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox26.Location = New System.Drawing.Point(168, 6)
        Me.GroupBox26.Name = "GroupBox26"
        Me.GroupBox26.Size = New System.Drawing.Size(434, 176)
        Me.GroupBox26.TabIndex = 63
        Me.GroupBox26.TabStop = False
        Me.GroupBox26.Text = "Meta Data"
        '
        'GroupBox28
        '
        Me.GroupBox28.Controls.Add(Me.lstMetaData)
        Me.GroupBox28.Controls.Add(Me.txtDefFIExt)
        Me.GroupBox28.Controls.Add(Me.Label34)
        Me.GroupBox28.Controls.Add(Me.btnRemoveMetaDataFT)
        Me.GroupBox28.Controls.Add(Me.btnEditMetaDataFT)
        Me.GroupBox28.Controls.Add(Me.btnNewMetaDataFT)
        Me.GroupBox28.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox28.Location = New System.Drawing.Point(249, 10)
        Me.GroupBox28.Name = "GroupBox28"
        Me.GroupBox28.Size = New System.Drawing.Size(183, 144)
        Me.GroupBox28.TabIndex = 8
        Me.GroupBox28.TabStop = False
        Me.GroupBox28.Text = "Defaults by File Type"
        '
        'lstMetaData
        '
        Me.lstMetaData.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstMetaData.FormattingEnabled = True
        Me.lstMetaData.Location = New System.Drawing.Point(9, 15)
        Me.lstMetaData.Name = "lstMetaData"
        Me.lstMetaData.Size = New System.Drawing.Size(165, 95)
        Me.lstMetaData.TabIndex = 34
        '
        'txtDefFIExt
        '
        Me.txtDefFIExt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefFIExt.Location = New System.Drawing.Point(72, 115)
        Me.txtDefFIExt.Name = "txtDefFIExt"
        Me.txtDefFIExt.Size = New System.Drawing.Size(35, 22)
        Me.txtDefFIExt.TabIndex = 33
        '
        'Label34
        '
        Me.Label34.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(7, 118)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(63, 19)
        Me.Label34.TabIndex = 32
        Me.Label34.Text = "File Type:"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnRemoveMetaDataFT
        '
        Me.btnRemoveMetaDataFT.Enabled = False
        Me.btnRemoveMetaDataFT.Image = CType(resources.GetObject("btnRemoveMetaDataFT.Image"), System.Drawing.Image)
        Me.btnRemoveMetaDataFT.Location = New System.Drawing.Point(152, 114)
        Me.btnRemoveMetaDataFT.Name = "btnRemoveMetaDataFT"
        Me.btnRemoveMetaDataFT.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveMetaDataFT.TabIndex = 31
        Me.btnRemoveMetaDataFT.UseVisualStyleBackColor = True
        '
        'btnEditMetaDataFT
        '
        Me.btnEditMetaDataFT.Enabled = False
        Me.btnEditMetaDataFT.Image = CType(resources.GetObject("btnEditMetaDataFT.Image"), System.Drawing.Image)
        Me.btnEditMetaDataFT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditMetaDataFT.Location = New System.Drawing.Point(129, 114)
        Me.btnEditMetaDataFT.Name = "btnEditMetaDataFT"
        Me.btnEditMetaDataFT.Size = New System.Drawing.Size(23, 23)
        Me.btnEditMetaDataFT.TabIndex = 30
        Me.btnEditMetaDataFT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditMetaDataFT.UseVisualStyleBackColor = True
        '
        'btnNewMetaDataFT
        '
        Me.btnNewMetaDataFT.Enabled = False
        Me.btnNewMetaDataFT.Image = CType(resources.GetObject("btnNewMetaDataFT.Image"), System.Drawing.Image)
        Me.btnNewMetaDataFT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNewMetaDataFT.Location = New System.Drawing.Point(107, 114)
        Me.btnNewMetaDataFT.Name = "btnNewMetaDataFT"
        Me.btnNewMetaDataFT.Size = New System.Drawing.Size(23, 23)
        Me.btnNewMetaDataFT.TabIndex = 29
        Me.btnNewMetaDataFT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNewMetaDataFT.UseVisualStyleBackColor = True
        '
        'chkIFOScan
        '
        Me.chkIFOScan.AutoSize = True
        Me.chkIFOScan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIFOScan.Location = New System.Drawing.Point(6, 33)
        Me.chkIFOScan.Name = "chkIFOScan"
        Me.chkIFOScan.Size = New System.Drawing.Size(123, 17)
        Me.chkIFOScan.TabIndex = 18
        Me.chkIFOScan.Text = "Enable IFO Parsing"
        Me.chkIFOScan.UseVisualStyleBackColor = True
        '
        'cbLanguages
        '
        Me.cbLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLanguages.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbLanguages.FormattingEnabled = True
        Me.cbLanguages.Location = New System.Drawing.Point(38, 147)
        Me.cbLanguages.Name = "cbLanguages"
        Me.cbLanguages.Size = New System.Drawing.Size(174, 21)
        Me.cbLanguages.Sorted = True
        Me.cbLanguages.TabIndex = 17
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(2, 117)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(245, 29)
        Me.Label31.TabIndex = 16
        Me.Label31.Text = "Display Overlay if Video Contains an Audio Stream With the Following Language:"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'gbRTFormat
        '
        Me.gbRTFormat.Controls.Add(Me.Label51)
        Me.gbRTFormat.Controls.Add(Me.txtRuntimeFormat)
        Me.gbRTFormat.Controls.Add(Me.chkUseMIDuration)
        Me.gbRTFormat.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbRTFormat.Location = New System.Drawing.Point(6, 50)
        Me.gbRTFormat.Name = "gbRTFormat"
        Me.gbRTFormat.Size = New System.Drawing.Size(234, 64)
        Me.gbRTFormat.TabIndex = 9
        Me.gbRTFormat.TabStop = False
        Me.gbRTFormat.Text = "Duration Format"
        '
        'Label51
        '
        Me.Label51.Font = New System.Drawing.Font("Segoe UI", 7.0!)
        Me.Label51.Location = New System.Drawing.Point(160, 10)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(72, 50)
        Me.Label51.TabIndex = 23
        Me.Label51.Text = "<h>=Hours" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<m>=Minutes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<s>=Seconds"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRuntimeFormat
        '
        Me.txtRuntimeFormat.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRuntimeFormat.Location = New System.Drawing.Point(5, 34)
        Me.txtRuntimeFormat.Name = "txtRuntimeFormat"
        Me.txtRuntimeFormat.Size = New System.Drawing.Size(145, 22)
        Me.txtRuntimeFormat.TabIndex = 22
        '
        'chkUseMIDuration
        '
        Me.chkUseMIDuration.AutoSize = True
        Me.chkUseMIDuration.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseMIDuration.Location = New System.Drawing.Point(5, 15)
        Me.chkUseMIDuration.Name = "chkUseMIDuration"
        Me.chkUseMIDuration.Size = New System.Drawing.Size(158, 17)
        Me.chkUseMIDuration.TabIndex = 8
        Me.chkUseMIDuration.Text = "Use Duration for Runtime"
        Me.chkUseMIDuration.UseVisualStyleBackColor = True
        '
        'chkScanMediaInfo
        '
        Me.chkScanMediaInfo.AutoSize = True
        Me.chkScanMediaInfo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScanMediaInfo.Location = New System.Drawing.Point(6, 16)
        Me.chkScanMediaInfo.Name = "chkScanMediaInfo"
        Me.chkScanMediaInfo.Size = New System.Drawing.Size(106, 17)
        Me.chkScanMediaInfo.TabIndex = 7
        Me.chkScanMediaInfo.Text = "Scan Meta Data"
        Me.chkScanMediaInfo.UseVisualStyleBackColor = True
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.chkLockMPAA)
        Me.GroupBox10.Controls.Add(Me.chkLockLanguageA)
        Me.GroupBox10.Controls.Add(Me.chkLockLanguageV)
        Me.GroupBox10.Controls.Add(Me.chkLockOutline)
        Me.GroupBox10.Controls.Add(Me.chkLockPlot)
        Me.GroupBox10.Controls.Add(Me.chkLockTrailer)
        Me.GroupBox10.Controls.Add(Me.chkLockGenre)
        Me.GroupBox10.Controls.Add(Me.chkLockRealStudio)
        Me.GroupBox10.Controls.Add(Me.chkLockRating)
        Me.GroupBox10.Controls.Add(Me.chkLockTagline)
        Me.GroupBox10.Controls.Add(Me.chkLockTitle)
        Me.GroupBox10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox10.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(156, 206)
        Me.GroupBox10.TabIndex = 1
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Global Locks"
        '
        'chkLockMPAA
        '
        Me.chkLockMPAA.AutoSize = True
        Me.chkLockMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLockMPAA.Location = New System.Drawing.Point(6, 186)
        Me.chkLockMPAA.Name = "chkLockMPAA"
        Me.chkLockMPAA.Size = New System.Drawing.Size(149, 17)
        Me.chkLockMPAA.TabIndex = 49
        Me.chkLockMPAA.Text = "Lock MPAA/Certification"
        Me.chkLockMPAA.UseVisualStyleBackColor = True
        '
        'chkLockLanguageA
        '
        Me.chkLockLanguageA.AutoSize = True
        Me.chkLockLanguageA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLockLanguageA.Location = New System.Drawing.Point(6, 169)
        Me.chkLockLanguageA.Name = "chkLockLanguageA"
        Me.chkLockLanguageA.Size = New System.Drawing.Size(142, 17)
        Me.chkLockLanguageA.TabIndex = 48
        Me.chkLockLanguageA.Text = "Lock Language (audio)"
        Me.chkLockLanguageA.UseVisualStyleBackColor = True
        '
        'chkLockLanguageV
        '
        Me.chkLockLanguageV.AutoSize = True
        Me.chkLockLanguageV.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLockLanguageV.Location = New System.Drawing.Point(6, 152)
        Me.chkLockLanguageV.Name = "chkLockLanguageV"
        Me.chkLockLanguageV.Size = New System.Drawing.Size(140, 17)
        Me.chkLockLanguageV.TabIndex = 47
        Me.chkLockLanguageV.Text = "Lock Language (video)"
        Me.chkLockLanguageV.UseVisualStyleBackColor = True
        '
        'chkLockOutline
        '
        Me.chkLockOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLockOutline.Location = New System.Drawing.Point(6, 50)
        Me.chkLockOutline.Name = "chkLockOutline"
        Me.chkLockOutline.Size = New System.Drawing.Size(129, 17)
        Me.chkLockOutline.TabIndex = 1
        Me.chkLockOutline.Text = "Lock Outline"
        Me.chkLockOutline.UseVisualStyleBackColor = True
        '
        'chkLockPlot
        '
        Me.chkLockPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLockPlot.Location = New System.Drawing.Point(6, 33)
        Me.chkLockPlot.Name = "chkLockPlot"
        Me.chkLockPlot.Size = New System.Drawing.Size(129, 17)
        Me.chkLockPlot.TabIndex = 0
        Me.chkLockPlot.Text = "Lock Plot"
        Me.chkLockPlot.UseVisualStyleBackColor = True
        '
        'chkLockTrailer
        '
        Me.chkLockTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLockTrailer.Location = New System.Drawing.Point(6, 135)
        Me.chkLockTrailer.Name = "chkLockTrailer"
        Me.chkLockTrailer.Size = New System.Drawing.Size(129, 17)
        Me.chkLockTrailer.TabIndex = 46
        Me.chkLockTrailer.Text = "Lock Trailer"
        Me.chkLockTrailer.UseVisualStyleBackColor = True
        '
        'chkLockGenre
        '
        Me.chkLockGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLockGenre.Location = New System.Drawing.Point(6, 118)
        Me.chkLockGenre.Name = "chkLockGenre"
        Me.chkLockGenre.Size = New System.Drawing.Size(129, 17)
        Me.chkLockGenre.TabIndex = 7
        Me.chkLockGenre.Text = "Lock Genre"
        Me.chkLockGenre.UseVisualStyleBackColor = True
        '
        'chkLockRealStudio
        '
        Me.chkLockRealStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLockRealStudio.Location = New System.Drawing.Point(6, 101)
        Me.chkLockRealStudio.Name = "chkLockRealStudio"
        Me.chkLockRealStudio.Size = New System.Drawing.Size(129, 17)
        Me.chkLockRealStudio.TabIndex = 5
        Me.chkLockRealStudio.Text = "Lock Studio"
        Me.chkLockRealStudio.UseVisualStyleBackColor = True
        '
        'chkLockRating
        '
        Me.chkLockRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLockRating.Location = New System.Drawing.Point(6, 84)
        Me.chkLockRating.Name = "chkLockRating"
        Me.chkLockRating.Size = New System.Drawing.Size(129, 17)
        Me.chkLockRating.TabIndex = 4
        Me.chkLockRating.Text = "Lock Rating"
        Me.chkLockRating.UseVisualStyleBackColor = True
        '
        'chkLockTagline
        '
        Me.chkLockTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLockTagline.Location = New System.Drawing.Point(6, 67)
        Me.chkLockTagline.Name = "chkLockTagline"
        Me.chkLockTagline.Size = New System.Drawing.Size(129, 17)
        Me.chkLockTagline.TabIndex = 3
        Me.chkLockTagline.Text = "Lock Tagline"
        Me.chkLockTagline.UseVisualStyleBackColor = True
        '
        'chkLockTitle
        '
        Me.chkLockTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLockTitle.Location = New System.Drawing.Point(6, 16)
        Me.chkLockTitle.Name = "chkLockTitle"
        Me.chkLockTitle.Size = New System.Drawing.Size(129, 17)
        Me.chkLockTitle.TabIndex = 2
        Me.chkLockTitle.Text = "Lock Title"
        Me.chkLockTitle.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkOutlinePlotEnglishOverwrite)
        Me.GroupBox1.Controls.Add(Me.chkTitleFallback)
        Me.GroupBox1.Controls.Add(Me.lblLimit3)
        Me.GroupBox1.Controls.Add(Me.chkPlotForOutline)
        Me.GroupBox1.Controls.Add(Me.txtOutlineLimit)
        Me.GroupBox1.Controls.Add(Me.chkUseMPAAFSK)
        Me.GroupBox1.Controls.Add(Me.chkFullCast)
        Me.GroupBox1.Controls.Add(Me.chkForceTitle)
        Me.GroupBox1.Controls.Add(Me.cbForce)
        Me.GroupBox1.Controls.Add(Me.chkOnlyValueForCert)
        Me.GroupBox1.Controls.Add(Me.chkOutlineForPlot)
        Me.GroupBox1.Controls.Add(Me.chkCastWithImg)
        Me.GroupBox1.Controls.Add(Me.chkUseCertForMPAA)
        Me.GroupBox1.Controls.Add(Me.chkFullCrew)
        Me.GroupBox1.Controls.Add(Me.cbCert)
        Me.GroupBox1.Controls.Add(Me.chkCert)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(313, 187)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(289, 241)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Miscellaneous"
        '
        'chkOutlinePlotEnglishOverwrite
        '
        Me.chkOutlinePlotEnglishOverwrite.AutoSize = True
        Me.chkOutlinePlotEnglishOverwrite.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOutlinePlotEnglishOverwrite.Location = New System.Drawing.Point(6, 137)
        Me.chkOutlinePlotEnglishOverwrite.Name = "chkOutlinePlotEnglishOverwrite"
        Me.chkOutlinePlotEnglishOverwrite.Size = New System.Drawing.Size(207, 17)
        Me.chkOutlinePlotEnglishOverwrite.TabIndex = 72
        Me.chkOutlinePlotEnglishOverwrite.Text = "Only overwrite english plot/outline"
        Me.chkOutlinePlotEnglishOverwrite.UseVisualStyleBackColor = True
        '
        'chkTitleFallback
        '
        Me.chkTitleFallback.AutoSize = True
        Me.chkTitleFallback.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTitleFallback.Location = New System.Drawing.Point(6, 31)
        Me.chkTitleFallback.Name = "chkTitleFallback"
        Me.chkTitleFallback.Size = New System.Drawing.Size(153, 17)
        Me.chkTitleFallback.TabIndex = 71
        Me.chkTitleFallback.Text = "Worldwide-Title Fallback"
        Me.chkTitleFallback.UseVisualStyleBackColor = True
        '
        'chkPlotForOutline
        '
        Me.chkPlotForOutline.AutoSize = True
        Me.chkPlotForOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPlotForOutline.Location = New System.Drawing.Point(6, 88)
        Me.chkPlotForOutline.Name = "chkPlotForOutline"
        Me.chkPlotForOutline.Size = New System.Drawing.Size(228, 17)
        Me.chkPlotForOutline.TabIndex = 68
        Me.chkPlotForOutline.Text = "Use Plot  for Outline if Outline is Empty"
        Me.chkPlotForOutline.UseVisualStyleBackColor = True
        '
        'chkUseMPAAFSK
        '
        Me.chkUseMPAAFSK.AutoSize = True
        Me.chkUseMPAAFSK.Enabled = False
        Me.chkUseMPAAFSK.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseMPAAFSK.Location = New System.Drawing.Point(25, 218)
        Me.chkUseMPAAFSK.Name = "chkUseMPAAFSK"
        Me.chkUseMPAAFSK.Size = New System.Drawing.Size(213, 17)
        Me.chkUseMPAAFSK.TabIndex = 67
        Me.chkUseMPAAFSK.Text = "Use MPAA as Fallback for FSK Rating"
        Me.chkUseMPAAFSK.UseVisualStyleBackColor = True
        '
        'chkFullCast
        '
        Me.chkFullCast.AutoSize = True
        Me.chkFullCast.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFullCast.Location = New System.Drawing.Point(6, 50)
        Me.chkFullCast.Name = "chkFullCast"
        Me.chkFullCast.Size = New System.Drawing.Size(107, 17)
        Me.chkFullCast.TabIndex = 0
        Me.chkFullCast.Text = "Scrape Full Cast"
        Me.chkFullCast.UseVisualStyleBackColor = True
        '
        'chkForceTitle
        '
        Me.chkForceTitle.AutoSize = True
        Me.chkForceTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkForceTitle.Location = New System.Drawing.Point(6, 14)
        Me.chkForceTitle.Name = "chkForceTitle"
        Me.chkForceTitle.Size = New System.Drawing.Size(135, 17)
        Me.chkForceTitle.TabIndex = 64
        Me.chkForceTitle.Text = "Force Title Language:"
        Me.chkForceTitle.UseVisualStyleBackColor = True
        '
        'cbForce
        '
        Me.cbForce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbForce.Enabled = False
        Me.cbForce.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbForce.FormattingEnabled = True
        Me.cbForce.Location = New System.Drawing.Point(168, 12)
        Me.cbForce.Name = "cbForce"
        Me.cbForce.Size = New System.Drawing.Size(115, 21)
        Me.cbForce.Sorted = True
        Me.cbForce.TabIndex = 65
        '
        'chkOnlyValueForCert
        '
        Me.chkOnlyValueForCert.AutoSize = True
        Me.chkOnlyValueForCert.Enabled = False
        Me.chkOnlyValueForCert.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOnlyValueForCert.Location = New System.Drawing.Point(25, 202)
        Me.chkOnlyValueForCert.Name = "chkOnlyValueForCert"
        Me.chkOnlyValueForCert.Size = New System.Drawing.Size(168, 17)
        Me.chkOnlyValueForCert.TabIndex = 66
        Me.chkOnlyValueForCert.Text = "Only Save the Value to NFO"
        Me.chkOnlyValueForCert.UseVisualStyleBackColor = True
        '
        'chkOutlineForPlot
        '
        Me.chkOutlineForPlot.AutoSize = True
        Me.chkOutlineForPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOutlineForPlot.Location = New System.Drawing.Point(6, 114)
        Me.chkOutlineForPlot.Name = "chkOutlineForPlot"
        Me.chkOutlineForPlot.Size = New System.Drawing.Size(206, 17)
        Me.chkOutlineForPlot.TabIndex = 3
        Me.chkOutlineForPlot.Text = "Use Outline for Plot if Plot is Empty"
        Me.chkOutlineForPlot.UseVisualStyleBackColor = True
        '
        'chkCastWithImg
        '
        Me.chkCastWithImg.AutoSize = True
        Me.chkCastWithImg.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCastWithImg.Location = New System.Drawing.Point(6, 69)
        Me.chkCastWithImg.Name = "chkCastWithImg"
        Me.chkCastWithImg.Size = New System.Drawing.Size(189, 17)
        Me.chkCastWithImg.TabIndex = 1
        Me.chkCastWithImg.Text = "Scrape Only Actors With Images"
        Me.chkCastWithImg.UseVisualStyleBackColor = True
        '
        'chkUseCertForMPAA
        '
        Me.chkUseCertForMPAA.AutoSize = True
        Me.chkUseCertForMPAA.Enabled = False
        Me.chkUseCertForMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseCertForMPAA.Location = New System.Drawing.Point(6, 183)
        Me.chkUseCertForMPAA.Name = "chkUseCertForMPAA"
        Me.chkUseCertForMPAA.Size = New System.Drawing.Size(162, 17)
        Me.chkUseCertForMPAA.TabIndex = 6
        Me.chkUseCertForMPAA.Text = "Use Certification for MPAA"
        Me.chkUseCertForMPAA.UseVisualStyleBackColor = True
        '
        'chkFullCrew
        '
        Me.chkFullCrew.AutoSize = True
        Me.chkFullCrew.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFullCrew.Location = New System.Drawing.Point(148, 50)
        Me.chkFullCrew.Name = "chkFullCrew"
        Me.chkFullCrew.Size = New System.Drawing.Size(111, 17)
        Me.chkFullCrew.TabIndex = 2
        Me.chkFullCrew.Text = "Scrape Full Crew"
        Me.chkFullCrew.UseVisualStyleBackColor = True
        '
        'cbCert
        '
        Me.cbCert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCert.Enabled = False
        Me.cbCert.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbCert.FormattingEnabled = True
        Me.cbCert.Items.AddRange(New Object() {"Argentina", "Australia", "Belgium", "Brazil", "Canada", "Finland", "France", "Germany", "Hong Kong", "Hungary", "Iceland", "Ireland", "Netherlands", "New Zealand", "Peru", "Poland", "Portugal", "Serbia", "Singapore", "South Korea", "Spain", "Sweden", "Switzerland", "Turkey", "UK", "USA"})
        Me.cbCert.Location = New System.Drawing.Point(174, 165)
        Me.cbCert.Name = "cbCert"
        Me.cbCert.Size = New System.Drawing.Size(108, 21)
        Me.cbCert.Sorted = True
        Me.cbCert.TabIndex = 5
        '
        'chkCert
        '
        Me.chkCert.AutoSize = True
        Me.chkCert.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCert.Location = New System.Drawing.Point(6, 164)
        Me.chkCert.Name = "chkCert"
        Me.chkCert.Size = New System.Drawing.Size(168, 17)
        Me.chkCert.TabIndex = 4
        Me.chkCert.Text = "Use Certification Language:"
        Me.chkCert.UseVisualStyleBackColor = True
        '
        'pnlMovieScraper
        '
        Me.pnlMovieScraper.BackColor = System.Drawing.Color.White
        Me.pnlMovieScraper.Controls.Add(Me.GroupBox10)
        Me.pnlMovieScraper.Controls.Add(Me.gbOptions)
        Me.pnlMovieScraper.Controls.Add(Me.GroupBox1)
        Me.pnlMovieScraper.Controls.Add(Me.GroupBox26)
        Me.pnlMovieScraper.Location = New System.Drawing.Point(1325, 380)
        Me.pnlMovieScraper.Name = "pnlMovieScraper"
        Me.pnlMovieScraper.Size = New System.Drawing.Size(750, 500)
        Me.pnlMovieScraper.TabIndex = 14
        Me.pnlMovieScraper.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AllowMerge = False
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 64)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1008, 25)
        Me.ToolStrip1.Stretch = True
        Me.ToolStrip1.TabIndex = 4
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'pnlMain
        '
        Me.pnlMain.AutoScroll = True
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Location = New System.Drawing.Point(251, 147)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(752, 502)
        Me.pnlMain.TabIndex = 9
        '
        'gbHelp
        '
        Me.gbHelp.BackColor = System.Drawing.Color.White
        Me.gbHelp.Controls.Add(Me.PictureBox2)
        Me.gbHelp.Controls.Add(Me.lblHelp)
        Me.gbHelp.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbHelp.Location = New System.Drawing.Point(3, 3)
        Me.gbHelp.Name = "gbHelp"
        Me.gbHelp.Size = New System.Drawing.Size(628, 62)
        Me.gbHelp.TabIndex = 0
        Me.gbHelp.TabStop = False
        Me.gbHelp.Text = "     Help"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(6, -2)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'lblHelp
        '
        Me.lblHelp.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHelp.Location = New System.Drawing.Point(3, 14)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(738, 43)
        Me.lblHelp.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.gbHelp)
        Me.Panel2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(5, 657)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(752, 69)
        Me.Panel2.TabIndex = 8
        '
        'pnlMovieTrailers
        '
        Me.pnlMovieTrailers.BackColor = System.Drawing.Color.White
        Me.pnlMovieTrailers.Controls.Add(Me.GroupBox15)
        Me.pnlMovieTrailers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMovieTrailers.Location = New System.Drawing.Point(1125, 620)
        Me.pnlMovieTrailers.Name = "pnlMovieTrailers"
        Me.pnlMovieTrailers.Size = New System.Drawing.Size(750, 500)
        Me.pnlMovieTrailers.TabIndex = 21
        Me.pnlMovieTrailers.Visible = False
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.cbTrailerQuality)
        Me.GroupBox15.Controls.Add(Me.lblPreferredQuality)
        Me.GroupBox15.Controls.Add(Me.chkDeleteAllTrailers)
        Me.GroupBox15.Controls.Add(Me.chkOverwriteTrailer)
        Me.GroupBox15.Controls.Add(Me.chkDownloadTrailer)
        Me.GroupBox15.Location = New System.Drawing.Point(12, 11)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(183, 230)
        Me.GroupBox15.TabIndex = 1
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "Trailers"
        '
        'cbTrailerQuality
        '
        Me.cbTrailerQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTrailerQuality.Enabled = False
        Me.cbTrailerQuality.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbTrailerQuality.FormattingEnabled = True
        Me.cbTrailerQuality.Location = New System.Drawing.Point(25, 94)
        Me.cbTrailerQuality.Name = "cbTrailerQuality"
        Me.cbTrailerQuality.Size = New System.Drawing.Size(125, 21)
        Me.cbTrailerQuality.TabIndex = 7
        '
        'lblPreferredQuality
        '
        Me.lblPreferredQuality.AutoSize = True
        Me.lblPreferredQuality.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPreferredQuality.Location = New System.Drawing.Point(24, 78)
        Me.lblPreferredQuality.Name = "lblPreferredQuality"
        Me.lblPreferredQuality.Size = New System.Drawing.Size(96, 13)
        Me.lblPreferredQuality.TabIndex = 6
        Me.lblPreferredQuality.Text = "Preferred Quality:"
        '
        'chkDeleteAllTrailers
        '
        Me.chkDeleteAllTrailers.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkDeleteAllTrailers.Enabled = False
        Me.chkDeleteAllTrailers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDeleteAllTrailers.Location = New System.Drawing.Point(25, 57)
        Me.chkDeleteAllTrailers.Name = "chkDeleteAllTrailers"
        Me.chkDeleteAllTrailers.Size = New System.Drawing.Size(152, 27)
        Me.chkDeleteAllTrailers.TabIndex = 5
        Me.chkDeleteAllTrailers.Text = "Delete All Existing"
        Me.chkDeleteAllTrailers.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkDeleteAllTrailers.UseVisualStyleBackColor = True
        '
        'chkOverwriteTrailer
        '
        Me.chkOverwriteTrailer.AutoSize = True
        Me.chkOverwriteTrailer.Enabled = False
        Me.chkOverwriteTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOverwriteTrailer.Location = New System.Drawing.Point(25, 39)
        Me.chkOverwriteTrailer.Name = "chkOverwriteTrailer"
        Me.chkOverwriteTrailer.Size = New System.Drawing.Size(119, 17)
        Me.chkOverwriteTrailer.TabIndex = 4
        Me.chkOverwriteTrailer.Text = "Overwrite Existing"
        Me.chkOverwriteTrailer.UseVisualStyleBackColor = True
        '
        'chkDownloadTrailer
        '
        Me.chkDownloadTrailer.AutoSize = True
        Me.chkDownloadTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDownloadTrailer.Location = New System.Drawing.Point(12, 16)
        Me.chkDownloadTrailer.Name = "chkDownloadTrailer"
        Me.chkDownloadTrailer.Size = New System.Drawing.Size(140, 17)
        Me.chkDownloadTrailer.TabIndex = 0
        Me.chkDownloadTrailer.Text = "Enable Trailer Support"
        Me.chkDownloadTrailer.UseVisualStyleBackColor = True
        '
        'dlgSettings
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.pnlTVSources)
        Me.Controls.Add(Me.pnlMovieScraper)
        Me.Controls.Add(Me.pnlMovieSources)
        Me.Controls.Add(Me.pnlMovieGeneral)
        Me.Controls.Add(Me.pnlMovieImages)
        Me.Controls.Add(Me.pnlExtensions)
        Me.Controls.Add(Me.pnlTVGeneral)
        Me.Controls.Add(Me.pnlGeneral)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTVScraper)
        Me.Controls.Add(Me.pnlMovieTrailers)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlCurrent)
        Me.Controls.Add(Me.tvSettings)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.pnlTVImages)
        Me.Controls.Add(Me.pnlProxy)
        Me.DoubleBuffered = true
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "dlgSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.GroupBox4.ResumeLayout(false)
        Me.GroupBox4.PerformLayout
        Me.gb_DAEMON.ResumeLayout(false)
        Me.gb_DAEMON.PerformLayout
        Me.GroupBox29.ResumeLayout(false)
        Me.GroupBox29.PerformLayout
        Me.GroupBox3.ResumeLayout(false)
        Me.tcCleaner.ResumeLayout(false)
        Me.tpStandard.ResumeLayout(false)
        Me.tpStandard.PerformLayout
        Me.tpExpert.ResumeLayout(false)
        Me.tpExpert.PerformLayout
        Me.GroupBox12.ResumeLayout(false)
        Me.GroupBox12.PerformLayout
        Me.pnlMovieImages.ResumeLayout(false)
        Me.gbMovieImagesEFanarts.ResumeLayout(false)
        Me.gbMovieImagesEFanarts.PerformLayout
        CType(Me.tbMovieEFanartsQual,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbMovieImagesEThumbs.ResumeLayout(false)
        Me.gbMovieImagesEThumbs.PerformLayout
        CType(Me.tbMovieEThumbsQual,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox9.ResumeLayout(false)
        Me.gbMovieImagesFanart.ResumeLayout(false)
        Me.gbMovieImagesFanart.PerformLayout
        CType(Me.tbMovieFanartQual,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbMovieImagesPoster.ResumeLayout(false)
        Me.gbMovieImagesPoster.PerformLayout
        CType(Me.tbMoviePosterQual,System.ComponentModel.ISupportInitialize).EndInit
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.GroupBox25.ResumeLayout(false)
        Me.GroupBox25.PerformLayout
        Me.pnlTop.ResumeLayout(false)
        Me.pnlTop.PerformLayout
        CType(Me.pbTopLogo,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlGeneral.ResumeLayout(false)
        Me.gbInterface.ResumeLayout(false)
        Me.gbInterface.PerformLayout
        Me.pnlMovieGeneral.ResumeLayout(false)
        Me.GroupBox30.ResumeLayout(false)
        Me.GroupBox30.PerformLayout
        Me.gbGenreFilter.ResumeLayout(false)
        Me.gbFilters.ResumeLayout(false)
        Me.gbFilters.PerformLayout
        Me.GroupBox27.ResumeLayout(false)
        Me.pnlExtensions.ResumeLayout(false)
        Me.GroupBox22.ResumeLayout(false)
        Me.GroupBox22.PerformLayout
        Me.GroupBox18.ResumeLayout(false)
        Me.GroupBox18.PerformLayout
        Me.pnlProxy.ResumeLayout(false)
        Me.gbProxy.ResumeLayout(false)
        Me.gbProxy.PerformLayout
        Me.gbCreds.ResumeLayout(false)
        Me.gbCreds.PerformLayout
        Me.gbMovieBackdropsFolder.ResumeLayout(false)
        Me.gbMovieBackdropsFolder.PerformLayout
        Me.Panel1.ResumeLayout(false)
        CType(Me.pbCurrent,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlMovieSources.ResumeLayout(false)
        Me.gbMovieSetsFolder.ResumeLayout(false)
        Me.gbMovieSetsFolder.PerformLayout
        Me.gbMovieFileNaming.ResumeLayout(false)
        Me.tbcMovieFileNaming.ResumeLayout(false)
        Me.tpMovieFileNamingXBMC.ResumeLayout(false)
        Me.gbMovieXBMCOptional.ResumeLayout(false)
        Me.gbMovieXBMCOptional.PerformLayout
        Me.gbMovieEden.ResumeLayout(false)
        Me.gbMovieEden.PerformLayout
        Me.gbMovieFrodo.ResumeLayout(false)
        Me.gbMovieFrodo.PerformLayout
        Me.tpMovieFileNamingNMT.ResumeLayout(false)
        Me.gbMovieNMTOptional.ResumeLayout(false)
        Me.gbMovieNMTOptional.PerformLayout
        Me.gbMovieNMJ.ResumeLayout(false)
        Me.gbMovieNMJ.PerformLayout
        Me.gbMovieYAMJ.ResumeLayout(false)
        Me.gbMovieYAMJ.PerformLayout
        Me.tpMovieFileNamingExpert.ResumeLayout(false)
        Me.gbMovieExpert.ResumeLayout(false)
        Me.gbMovieExpert.PerformLayout
        Me.tbcMovieFileNamingExpert.ResumeLayout(false)
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
        Me.tbMovieFileNamingExpertVTS.ResumeLayout(false)
        Me.tbMovieFileNamingExpertVTS.PerformLayout
        Me.gbMovieExpertVTSOptionalSettings.ResumeLayout(false)
        Me.gbMovieExpertVTSOptionalSettings.PerformLayout
        Me.gbMovieExpertVTSOptionalImages.ResumeLayout(false)
        Me.gbMovieExpertVTSOptionalImages.PerformLayout
        Me.tbMovieFileNamingExpertBDMV.ResumeLayout(false)
        Me.tbMovieFileNamingExpertBDMV.PerformLayout
        Me.gbMovieExpertBDMVOptionalSettings.ResumeLayout(false)
        Me.gbMovieExpertBDMVOptionalSettings.PerformLayout
        Me.gbMovieExpertBDMVOptionalImages.ResumeLayout(false)
        Me.gbMovieExpertBDMVOptionalImages.PerformLayout
        Me.gbMovieMiscOptions.ResumeLayout(false)
        Me.gbMovieMiscOptions.PerformLayout
        Me.pnlTVGeneral.ResumeLayout(false)
        Me.gbTVListOptions.ResumeLayout(false)
        Me.gbTVListOptions.PerformLayout
        Me.gbEpisodeListOptions.ResumeLayout(false)
        Me.gbEpisodeListOptions.PerformLayout
        Me.gbSeasonListOptions.ResumeLayout(false)
        Me.gbSeasonListOptions.PerformLayout
        Me.gbShowListOptions.ResumeLayout(false)
        Me.gbShowListOptions.PerformLayout
        Me.gbEpFilter.ResumeLayout(false)
        Me.gbEpFilter.PerformLayout
        Me.gbTVMisc.ResumeLayout(false)
        Me.gbTVMisc.PerformLayout
        Me.gbShowFilter.ResumeLayout(false)
        Me.gbShowFilter.PerformLayout
        Me.pnlTVSources.ResumeLayout(false)
        Me.TabControl2.ResumeLayout(false)
        Me.TabPage3.ResumeLayout(false)
        Me.gbTVFileNaming.ResumeLayout(false)
        Me.tbcTVFileNaming.ResumeLayout(false)
        Me.tpTVFileNamingXBMC.ResumeLayout(false)
        Me.gbTVXBMCAdditional.ResumeLayout(false)
        Me.gbTVXBMCAdditional.PerformLayout
        Me.gbTVFrodo.ResumeLayout(false)
        Me.gbTVFrodo.PerformLayout
        Me.gbMiscTVSourceOpts.ResumeLayout(false)
        Me.gbMiscTVSourceOpts.PerformLayout
        Me.TabPage4.ResumeLayout(false)
        Me.gbShowRegex.ResumeLayout(false)
        Me.gbShowRegex.PerformLayout
        Me.pnlTVImages.ResumeLayout(false)
        Me.TabControl3.ResumeLayout(false)
        Me.TabPage5.ResumeLayout(false)
        Me.gbAllSPosterOpts.ResumeLayout(false)
        Me.gbAllSPosterOpts.PerformLayout
        CType(Me.tbAllSPosterQual,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbShowPosterOpts.ResumeLayout(false)
        Me.gbShowPosterOpts.PerformLayout
        CType(Me.tbShowPosterQual,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbShowFanartOpts.ResumeLayout(false)
        Me.gbShowFanartOpts.PerformLayout
        CType(Me.tbShowFanartQual,System.ComponentModel.ISupportInitialize).EndInit
        Me.TabPage6.ResumeLayout(false)
        Me.gbSeaPosterOpts.ResumeLayout(false)
        Me.gbSeaPosterOpts.PerformLayout
        CType(Me.tbSeaPosterQual,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbSeaFanartOpts.ResumeLayout(false)
        Me.gbSeaFanartOpts.PerformLayout
        CType(Me.tbSeaFanartQual,System.ComponentModel.ISupportInitialize).EndInit
        Me.TabPage7.ResumeLayout(false)
        Me.gbEpPosterOpts.ResumeLayout(false)
        Me.gbEpPosterOpts.PerformLayout
        CType(Me.tbEpPosterQual,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbEpFanartOpts.ResumeLayout(false)
        Me.gbEpFanartOpts.PerformLayout
        CType(Me.tbEpFanartQual,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlTVScraper.ResumeLayout(false)
        Me.gbTVScraperDuration.ResumeLayout(false)
        Me.gbTVScraperDuration.PerformLayout
        Me.GroupBox32.ResumeLayout(false)
        Me.GroupBox35.ResumeLayout(false)
        Me.GroupBox34.ResumeLayout(false)
        Me.GroupBox33.ResumeLayout(false)
        Me.gbEpLocks.ResumeLayout(false)
        Me.gbShowLocks.ResumeLayout(false)
        Me.GroupBox31.ResumeLayout(false)
        Me.GroupBox31.PerformLayout
        Me.gbTVMIDefaults.ResumeLayout(false)
        Me.gbTVMIDefaults.PerformLayout
        Me.gbTVScraperOptions.ResumeLayout(false)
        Me.gbOptions.ResumeLayout(false)
        Me.gbOptions.PerformLayout
        Me.GroupBox26.ResumeLayout(false)
        Me.GroupBox26.PerformLayout
        Me.GroupBox28.ResumeLayout(false)
        Me.GroupBox28.PerformLayout
        Me.gbRTFormat.ResumeLayout(false)
        Me.gbRTFormat.PerformLayout
        Me.GroupBox10.ResumeLayout(false)
        Me.GroupBox10.PerformLayout
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.pnlMovieScraper.ResumeLayout(false)
        Me.gbHelp.ResumeLayout(false)
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel2.ResumeLayout(false)
        Me.pnlMovieTrailers.ResumeLayout(false)
        Me.GroupBox15.ResumeLayout(false)
        Me.GroupBox15.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkCleanFolderJPG As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanFanartJPG As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanMovieTBNb As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanMovieTBN As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanMovieNFOb As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanMovieNFO As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanMovieFanartJPG As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkOverwriteNfo As System.Windows.Forms.CheckBox
    Friend WithEvents lvMovies As System.Windows.Forms.ListView
    Friend WithEvents colPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRecur As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkMovieOverwriteFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieOverwritePoster As System.Windows.Forms.CheckBox
    Friend WithEvents cbMovieFanartSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblFanartSize As System.Windows.Forms.Label
    Friend WithEvents lblPosterSize As System.Windows.Forms.Label
    Friend WithEvents cbMoviePosterSize As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieTrailerCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieInfoCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieFanartCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoviePosterCol As System.Windows.Forms.CheckBox
    Friend WithEvents btnMovieRem As System.Windows.Forms.Button
    Friend WithEvents btnMovieAddFolder As System.Windows.Forms.Button
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieSingleScrapeImages As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanMovieNameJPG As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanMovieJPG As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanPosterJPG As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanPosterTBN As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanDotFanartJPG As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents chkMarkNew As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieImagesFanart As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents chkMovieResizeFanart As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieFanartWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieFanartHeight As System.Windows.Forms.TextBox
    Friend WithEvents gbMovieImagesPoster As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents chkMovieResizePoster As System.Windows.Forms.CheckBox
    Friend WithEvents txtMoviePosterWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtMoviePosterHeight As System.Windows.Forms.TextBox
    Friend WithEvents ilSettings As System.Windows.Forms.ImageList
    Friend WithEvents tvSettings As System.Windows.Forms.TreeView
    Friend WithEvents pnlGeneral As System.Windows.Forms.Panel
    Friend WithEvents pnlProxy As System.Windows.Forms.Panel
    Friend WithEvents pnlMovieGeneral As System.Windows.Forms.Panel
    Friend WithEvents lblCurrent As System.Windows.Forms.Label
    Friend WithEvents pnlCurrent As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkCleanExtrathumbs As System.Windows.Forms.CheckBox
    Friend WithEvents pnlExtensions As System.Windows.Forms.Panel
    Friend WithEvents GroupBox18 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRemMovieExt As System.Windows.Forms.Button
    Friend WithEvents btnAddMovieExt As System.Windows.Forms.Button
    Friend WithEvents txtMovieExt As System.Windows.Forms.TextBox
    Friend WithEvents lstMovieExts As System.Windows.Forms.ListBox
    Friend WithEvents chkUpdates As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieBackdropsFolder As System.Windows.Forms.GroupBox
    Friend WithEvents txtBDPath As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseBackdrops As System.Windows.Forms.Button
    Friend WithEvents chkAutoBD As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieEThumbsCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSubCol As System.Windows.Forms.CheckBox
    Friend WithEvents pnlMovieSources As System.Windows.Forms.Panel
    Friend WithEvents lbGenre As System.Windows.Forms.CheckedListBox
    Friend WithEvents pnlMovieImages As System.Windows.Forms.Panel
    Friend WithEvents gbMovieMiscOptions As System.Windows.Forms.GroupBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtSkipLessThan As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents chkSkipStackedSizeCheck As System.Windows.Forms.CheckBox
    Friend WithEvents lblMoviePosterQual As System.Windows.Forms.Label
    Friend WithEvents tbMoviePosterQual As System.Windows.Forms.TrackBar
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblMovieFanartQual As System.Windows.Forms.Label
    Friend WithEvents tbMovieFanartQual As System.Windows.Forms.TrackBar
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents chkMovieFanartOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieNoSaveImagesToNfo As System.Windows.Forms.CheckBox
    Friend WithEvents tcCleaner As System.Windows.Forms.TabControl
    Friend WithEvents tpStandard As System.Windows.Forms.TabPage
    Friend WithEvents tpExpert As System.Windows.Forms.TabPage
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents btnRemoveWhitelist As System.Windows.Forms.Button
    Friend WithEvents btnAddWhitelist As System.Windows.Forms.Button
    Friend WithEvents txtWhitelist As System.Windows.Forms.TextBox
    Friend WithEvents lstWhitelist As System.Windows.Forms.ListBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents chkWhitelistVideo As System.Windows.Forms.CheckBox
    Friend WithEvents chkInfoPanelAnim As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowDims As System.Windows.Forms.CheckBox
    Friend WithEvents chkNoDisplayFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkNoDisplayPoster As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox22 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRemoveNoStack As System.Windows.Forms.Button
    Friend WithEvents btnAddNoStack As System.Windows.Forms.Button
    Friend WithEvents txtNoStack As System.Windows.Forms.TextBox
    Friend WithEvents lstNoStack As System.Windows.Forms.ListBox
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colFolder As System.Windows.Forms.ColumnHeader
    Friend WithEvents colSingle As System.Windows.Forms.ColumnHeader
    Friend WithEvents colID As System.Windows.Forms.ColumnHeader
    Friend WithEvents fbdBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnEditSource As System.Windows.Forms.Button
    Friend WithEvents chkShowGenresText As System.Windows.Forms.CheckBox
    Friend WithEvents chkDisplayYear As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox25 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRemoveToken As System.Windows.Forms.Button
    Friend WithEvents btnAddToken As System.Windows.Forms.Button
    Friend WithEvents txtSortToken As System.Windows.Forms.TextBox
    Friend WithEvents lstSortTokens As System.Windows.Forms.ListBox
    Friend WithEvents txtCheckTitleTol As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents chkCheckTitles As System.Windows.Forms.CheckBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents cbIntLang As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox27 As System.Windows.Forms.GroupBox
    Friend WithEvents chkMissingEThumbs As System.Windows.Forms.CheckBox
    Friend WithEvents chkMissingSubs As System.Windows.Forms.CheckBox
    Friend WithEvents chkMissingTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkMissingNFO As System.Windows.Forms.CheckBox
    Friend WithEvents chkMissingFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkMissingPoster As System.Windows.Forms.CheckBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents cbMovieTheme As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox29 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTips As System.Windows.Forms.ToolTip
    Friend WithEvents pnlTVSources As System.Windows.Forms.Panel
    Friend WithEvents btnEditTVSource As System.Windows.Forms.Button
    Friend WithEvents lvTVSources As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnRemTVSource As System.Windows.Forms.Button
    Friend WithEvents btnAddTVSource As System.Windows.Forms.Button
    Friend WithEvents chkCleanDB As System.Windows.Forms.CheckBox
    Friend WithEvents chkIgnoreLastScan As System.Windows.Forms.CheckBox
    Friend WithEvents pnlTVGeneral As System.Windows.Forms.Panel
    Friend WithEvents gbEpFilter As System.Windows.Forms.GroupBox
    Friend WithEvents btnEpFilterDown As System.Windows.Forms.Button
    Friend WithEvents btnEpFilterUp As System.Windows.Forms.Button
    Friend WithEvents chkEpProperCase As System.Windows.Forms.CheckBox
    Friend WithEvents btnRemoveEpFilter As System.Windows.Forms.Button
    Friend WithEvents btnAddEpFilter As System.Windows.Forms.Button
    Friend WithEvents txtEpFilter As System.Windows.Forms.TextBox
    Friend WithEvents lstEpFilters As System.Windows.Forms.ListBox
    Friend WithEvents gbShowFilter As System.Windows.Forms.GroupBox
    Friend WithEvents btnShowFilterDown As System.Windows.Forms.Button
    Friend WithEvents btnShowFilterUp As System.Windows.Forms.Button
    Friend WithEvents chkShowProperCase As System.Windows.Forms.CheckBox
    Friend WithEvents btnRemoveShowFilter As System.Windows.Forms.Button
    Friend WithEvents btnAddShowFilter As System.Windows.Forms.Button
    Friend WithEvents txtShowFilter As System.Windows.Forms.TextBox
    Friend WithEvents lstShowFilters As System.Windows.Forms.ListBox
    Friend WithEvents gbFilters As System.Windows.Forms.GroupBox
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents chkProperCase As System.Windows.Forms.CheckBox
    Friend WithEvents btnRemoveFilter As System.Windows.Forms.Button
    Friend WithEvents btnAddFilter As System.Windows.Forms.Button
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents lstFilters As System.Windows.Forms.ListBox
    Friend WithEvents gbGenreFilter As System.Windows.Forms.GroupBox
    Friend WithEvents gbMiscTVSourceOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVIgnoreLastScan As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVCleanDB As System.Windows.Forms.CheckBox
    Friend WithEvents gbInterface As System.Windows.Forms.GroupBox
    Friend WithEvents cbEpTheme As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbTVShowTheme As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents lvShowRegex As System.Windows.Forms.ListView
    Friend WithEvents colSeason As System.Windows.Forms.ColumnHeader
    Friend WithEvents colSeasonApply As System.Windows.Forms.ColumnHeader
    Friend WithEvents colEpisode As System.Windows.Forms.ColumnHeader
    Friend WithEvents colEpApply As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnAddShowRegex As System.Windows.Forms.Button
    Friend WithEvents btnEditShowRegex As System.Windows.Forms.Button
    Friend WithEvents btnRemoveShowRegex As System.Windows.Forms.Button
    Friend WithEvents txtSeasonRegex As System.Windows.Forms.TextBox
    Friend WithEvents cboEpRetrieve As System.Windows.Forms.ComboBox
    Friend WithEvents txtEpRegex As System.Windows.Forms.TextBox
    Friend WithEvents cboSeasonRetrieve As System.Windows.Forms.ComboBox
    Friend WithEvents lblSeasonMatch As System.Windows.Forms.Label
    Friend WithEvents gbShowRegex As System.Windows.Forms.GroupBox
    Friend WithEvents lblEpisodeRetrieve As System.Windows.Forms.Label
    Friend WithEvents lblSeasonRetrieve As System.Windows.Forms.Label
    Friend WithEvents lblEpisodeMatch As System.Windows.Forms.Label
    Friend WithEvents colRegID As System.Windows.Forms.ColumnHeader
    Friend WithEvents pnlTVImages As System.Windows.Forms.Panel
    Friend WithEvents gbShowFanartOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtShowFanartWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtShowFanartHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblShowFanartQual As System.Windows.Forms.Label
    Friend WithEvents tbShowFanartQual As System.Windows.Forms.TrackBar
    Friend WithEvents lblShowFanartQ As System.Windows.Forms.Label
    Friend WithEvents lblShowFanartWidth As System.Windows.Forms.Label
    Friend WithEvents lblShowFanartHeight As System.Windows.Forms.Label
    Friend WithEvents chkResizeShowFanart As System.Windows.Forms.CheckBox
    Friend WithEvents cbShowFanartSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblShowFanartSize As System.Windows.Forms.Label
    Friend WithEvents chkOverwriteShowFanart As System.Windows.Forms.CheckBox
    Friend WithEvents gbShowPosterOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtShowPosterWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtShowPosterHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblShowPosterQual As System.Windows.Forms.Label
    Friend WithEvents tbShowPosterQual As System.Windows.Forms.TrackBar
    Friend WithEvents lblShowPosterQ As System.Windows.Forms.Label
    Friend WithEvents lblShowPosterWidth As System.Windows.Forms.Label
    Friend WithEvents lblShowPosterHeight As System.Windows.Forms.Label
    Friend WithEvents chkResizeShowPoster As System.Windows.Forms.CheckBox
    Friend WithEvents lblShowPosterSize As System.Windows.Forms.Label
    Friend WithEvents cbShowPosterSize As System.Windows.Forms.ComboBox
    Friend WithEvents chkOverwriteShowPoster As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl3 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents pnlTVScraper As System.Windows.Forms.Panel
    Friend WithEvents cbRatingRegion As System.Windows.Forms.ComboBox
    Friend WithEvents gbTVMisc As System.Windows.Forms.GroupBox
    Friend WithEvents lblRatingRegion As System.Windows.Forms.Label
    Friend WithEvents gbTVListOptions As System.Windows.Forms.GroupBox
    Friend WithEvents gbEpisodeListOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkEpisodeNfoCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodeFanartCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodePosterCol As System.Windows.Forms.CheckBox
    Friend WithEvents gbSeasonListOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkSeasonFanartCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkSeasonPosterCol As System.Windows.Forms.CheckBox
    Friend WithEvents gbShowListOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkShowNfoCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowFanartCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowPosterCol As System.Windows.Forms.CheckBox
    Friend WithEvents gbEpPosterOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtEpPosterWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtEpPosterHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblEpPosterQual As System.Windows.Forms.Label
    Friend WithEvents tbEpPosterQual As System.Windows.Forms.TrackBar
    Friend WithEvents lblEpPosterQ As System.Windows.Forms.Label
    Friend WithEvents lblEpPosterWidth As System.Windows.Forms.Label
    Friend WithEvents lblEpPosterHeight As System.Windows.Forms.Label
    Friend WithEvents chkResizeEpPoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverwriteEpPoster As System.Windows.Forms.CheckBox
    Friend WithEvents gbEpFanartOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtEpFanartWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtEpFanartHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblEpFanartQual As System.Windows.Forms.Label
    Friend WithEvents tbEpFanartQual As System.Windows.Forms.TrackBar
    Friend WithEvents lblEpFanartQ As System.Windows.Forms.Label
    Friend WithEvents lblEpFanartWidth As System.Windows.Forms.Label
    Friend WithEvents lblEpFanartHeight As System.Windows.Forms.Label
    Friend WithEvents chkResizeEpFanart As System.Windows.Forms.CheckBox
    Friend WithEvents cbEpFanartSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblEpFanartSize As System.Windows.Forms.Label
    Friend WithEvents chkOverwriteEpFanart As System.Windows.Forms.CheckBox
    Friend WithEvents gbProxy As System.Windows.Forms.GroupBox
    Friend WithEvents gbCreds As System.Windows.Forms.GroupBox
    Friend WithEvents chkEnableCredentials As System.Windows.Forms.CheckBox
    Friend WithEvents lblProxyPort As System.Windows.Forms.Label
    Friend WithEvents lblProxyURI As System.Windows.Forms.Label
    Friend WithEvents txtProxyPort As System.Windows.Forms.TextBox
    Friend WithEvents txtProxyURI As System.Windows.Forms.TextBox
    Friend WithEvents chkEnableProxy As System.Windows.Forms.CheckBox
    Friend WithEvents txtProxyDomain As System.Windows.Forms.TextBox
    Friend WithEvents lblProxyDomain As System.Windows.Forms.Label
    Friend WithEvents txtProxyPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtProxyUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblProxyUN As System.Windows.Forms.Label
    Friend WithEvents lblProxyPW As System.Windows.Forms.Label
    Friend WithEvents chkSourceFromFolder As System.Windows.Forms.CheckBox
    Friend WithEvents chkSortBeforeScan As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVScraperOptions As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox26 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox28 As System.Windows.Forms.GroupBox
    Friend WithEvents lstMetaData As System.Windows.Forms.ListBox
    Friend WithEvents txtDefFIExt As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents btnRemoveMetaDataFT As System.Windows.Forms.Button
    Friend WithEvents btnEditMetaDataFT As System.Windows.Forms.Button
    Friend WithEvents btnNewMetaDataFT As System.Windows.Forms.Button
    Friend WithEvents chkIFOScan As System.Windows.Forms.CheckBox
    Friend WithEvents cbLanguages As System.Windows.Forms.ComboBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents gbRTFormat As System.Windows.Forms.GroupBox
    Friend WithEvents chkScanMediaInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseMIDuration As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents chkLockOutline As System.Windows.Forms.CheckBox
    Friend WithEvents chkLockPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkLockTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkLockGenre As System.Windows.Forms.CheckBox
    Friend WithEvents chkLockRealStudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkLockRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkLockTagline As System.Windows.Forms.CheckBox
    Friend WithEvents chkLockTitle As System.Windows.Forms.CheckBox
    Friend WithEvents gbOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkTop250 As System.Windows.Forms.CheckBox
    Friend WithEvents chkCountry As System.Windows.Forms.CheckBox
    Friend WithEvents txtGenreLimit As System.Windows.Forms.TextBox
    Friend WithEvents lblLimit2 As System.Windows.Forms.Label
    Friend WithEvents txtActorLimit As System.Windows.Forms.TextBox
    Friend WithEvents lblLimit As System.Windows.Forms.Label
    Friend WithEvents chkCrew As System.Windows.Forms.CheckBox
    Friend WithEvents chkMusicBy As System.Windows.Forms.CheckBox
    Friend WithEvents chkProducers As System.Windows.Forms.CheckBox
    Friend WithEvents chkWriters As System.Windows.Forms.CheckBox
    Friend WithEvents chkStudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkOutline As System.Windows.Forms.CheckBox
    Friend WithEvents chkGenre As System.Windows.Forms.CheckBox
    Friend WithEvents chkDirector As System.Windows.Forms.CheckBox
    Friend WithEvents chkTagline As System.Windows.Forms.CheckBox
    Friend WithEvents chkCast As System.Windows.Forms.CheckBox
    Friend WithEvents chkVotes As System.Windows.Forms.CheckBox
    Friend WithEvents chkTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkRelease As System.Windows.Forms.CheckBox
    Friend WithEvents chkMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkYear As System.Windows.Forms.CheckBox
    Friend WithEvents chkTitle As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbForce As System.Windows.Forms.ComboBox
    Friend WithEvents chkForceTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkOutlineForPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkCastWithImg As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseCertForMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkFullCast As System.Windows.Forms.CheckBox
    Friend WithEvents chkFullCrew As System.Windows.Forms.CheckBox
    Friend WithEvents cbCert As System.Windows.Forms.ComboBox
    Friend WithEvents chkCert As System.Windows.Forms.CheckBox
    Friend WithEvents pnlMovieScraper As System.Windows.Forms.Panel
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents gbSeaPosterOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtSeaPosterWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtSeaPosterHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblSeaPosterQual As System.Windows.Forms.Label
    Friend WithEvents tbSeaPosterQual As System.Windows.Forms.TrackBar
    Friend WithEvents lblSeaPosterQ As System.Windows.Forms.Label
    Friend WithEvents lblSeaPosterWidth As System.Windows.Forms.Label
    Friend WithEvents lblSeaPosterHeight As System.Windows.Forms.Label
    Friend WithEvents chkSeaResizePoster As System.Windows.Forms.CheckBox
    Friend WithEvents lblSeaPosterSize As System.Windows.Forms.Label
    Friend WithEvents cbSeaPosterSize As System.Windows.Forms.ComboBox
    Friend WithEvents chkSeaOverwritePoster As System.Windows.Forms.CheckBox
    Friend WithEvents gbSeaFanartOpts As System.Windows.Forms.GroupBox
    Friend WithEvents txtSeaFanartWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtSeaFanartHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblSeaFanartQual As System.Windows.Forms.Label
    Friend WithEvents tbSeaFanartQual As System.Windows.Forms.TrackBar
    Friend WithEvents lblSeaFanartQ As System.Windows.Forms.Label
    Friend WithEvents lblSeaFanartWidth As System.Windows.Forms.Label
    Friend WithEvents lblSeaFanartHeight As System.Windows.Forms.Label
    Friend WithEvents chkSeaResizeFanart As System.Windows.Forms.CheckBox
    Friend WithEvents cbSeaFanartSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblSeaFanartSize As System.Windows.Forms.Label
    Friend WithEvents chkSeaOverwriteFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVScanOrderModify As System.Windows.Forms.CheckBox
    Friend WithEvents chkScanOrderModify As System.Windows.Forms.CheckBox
    Friend WithEvents lblTVUpdate As System.Windows.Forms.Label
    Friend WithEvents cboTVUpdate As System.Windows.Forms.ComboBox
    Friend WithEvents chkNoFilterEpisode As System.Windows.Forms.CheckBox
    Friend WithEvents chkDisplayMissingEpisodes As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox31 As System.Windows.Forms.GroupBox
    Friend WithEvents gbTVMIDefaults As System.Windows.Forms.GroupBox
    Friend WithEvents lstTVMetaData As System.Windows.Forms.ListBox
    Friend WithEvents txtTVDefFIExt As System.Windows.Forms.TextBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents btnRemoveTVMetaDataFT As System.Windows.Forms.Button
    Friend WithEvents btnEditTVMetaDataFT As System.Windows.Forms.Button
    Friend WithEvents btnNewTVMetaDataFT As System.Windows.Forms.Button
    Friend WithEvents cboTVMetaDataOverlay As System.Windows.Forms.ComboBox
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents chkTVScanMetaData As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox32 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox33 As System.Windows.Forms.GroupBox
    Friend WithEvents chkEpLockPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowLockStudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpLockRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpLockTitle As System.Windows.Forms.CheckBox
    Friend WithEvents gbEpLocks As System.Windows.Forms.GroupBox
    Friend WithEvents gbShowLocks As System.Windows.Forms.GroupBox
    Friend WithEvents chkShowLockPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowLockGenre As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowLockRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowLockTitle As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox35 As System.Windows.Forms.GroupBox
    Friend WithEvents chkScraperShowEGU As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowGenre As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowTitle As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox34 As System.Windows.Forms.GroupBox
    Friend WithEvents chkScraperEpTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpEpisode As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpSeason As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowActors As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowStudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowPremiered As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpActors As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpCredits As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpDirector As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpAired As System.Windows.Forms.CheckBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents txtRuntimeFormat As System.Windows.Forms.TextBox
    Friend WithEvents chkCertification As System.Windows.Forms.CheckBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pbCurrent As System.Windows.Forms.PictureBox
    Friend WithEvents chkDisplayAllSeason As System.Windows.Forms.CheckBox
    Friend WithEvents gbHelp As System.Windows.Forms.GroupBox
    Friend WithEvents lblHelp As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents chkMarkNewShows As System.Windows.Forms.CheckBox
    Friend WithEvents chkMarkNewEpisodes As System.Windows.Forms.CheckBox
    Friend WithEvents chkOnlyValueForCert As System.Windows.Forms.CheckBox
    Friend WithEvents rbPoster As System.Windows.Forms.RadioButton
    Friend WithEvents rbBanner As System.Windows.Forms.RadioButton
    Friend WithEvents gbAllSPosterOpts As System.Windows.Forms.GroupBox
    Friend WithEvents rbAllSPoster As System.Windows.Forms.RadioButton
    Friend WithEvents rbAllSBanner As System.Windows.Forms.RadioButton
    Friend WithEvents txtAllSPosterWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtAllSPosterHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblAllSPosterQual As System.Windows.Forms.Label
    Friend WithEvents tbAllSPosterQual As System.Windows.Forms.TrackBar
    Friend WithEvents lblAllSPosterQ As System.Windows.Forms.Label
    Friend WithEvents lblAllSPosterWidth As System.Windows.Forms.Label
    Friend WithEvents lblAllSPosterHeight As System.Windows.Forms.Label
    Friend WithEvents chkResizeAllSPoster As System.Windows.Forms.CheckBox
    Friend WithEvents lblAllSPosterSize As System.Windows.Forms.Label
    Friend WithEvents cbAllSPosterSize As System.Windows.Forms.ComboBox
    Friend WithEvents chkOverwriteAllSPoster As System.Windows.Forms.CheckBox
    Friend WithEvents cbOrdering As System.Windows.Forms.ComboBox
    Friend WithEvents lblOrdering As System.Windows.Forms.Label
    Friend WithEvents btnResetShowFilters As System.Windows.Forms.Button
    Friend WithEvents btnResetEpFilter As System.Windows.Forms.Button
    Friend WithEvents btnResetMovieFilters As System.Windows.Forms.Button
    Friend WithEvents btnResetValidExts As System.Windows.Forms.Button
    Friend WithEvents btnResetShowRegex As System.Windows.Forms.Button
    Friend WithEvents btnRegexDown As System.Windows.Forms.Button
    Friend WithEvents btnRegexUp As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTVSkipLessThan As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnClearRegex As System.Windows.Forms.Button
    Friend WithEvents btnGetTVProfiles As System.Windows.Forms.Button
    Friend WithEvents chkAskCheckboxScrape As System.Windows.Forms.CheckBox
    Friend WithEvents chkClickScrape As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieScraperActorThumbs As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoviePosterOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkSpecialDateAdd As System.Windows.Forms.CheckBox
    Friend WithEvents chkLockLanguageV As System.Windows.Forms.CheckBox
    Friend WithEvents chkLockLanguageA As System.Windows.Forms.CheckBox
    Friend WithEvents chkLockMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseMPAAFSK As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox30 As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtIMDBURL As System.Windows.Forms.TextBox
    Friend WithEvents pnlMovieTrailers As System.Windows.Forms.Panel
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents cbTrailerQuality As System.Windows.Forms.ComboBox
    Friend WithEvents lblPreferredQuality As System.Windows.Forms.Label
    Friend WithEvents chkDeleteAllTrailers As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverwriteTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkDownloadTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents txtOutlineLimit As System.Windows.Forms.TextBox
    Friend WithEvents chkPlotForOutline As System.Windows.Forms.CheckBox
    Friend WithEvents lblLimit3 As System.Windows.Forms.Label
    Friend WithEvents chkNoDisplayFanartSmall As System.Windows.Forms.CheckBox
    Friend WithEvents chkImagesGlassOverlay As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieFileNaming As System.Windows.Forms.GroupBox
    Friend WithEvents tbcMovieFileNaming As System.Windows.Forms.TabControl
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
    Friend WithEvents gbTVScraperDuration As System.Windows.Forms.GroupBox
    Friend WithEvents chkUseEPDuration As System.Windows.Forms.CheckBox
    Friend WithEvents txtEPRuntimeFormat As System.Windows.Forms.TextBox
    Friend WithEvents chkMissingEFanarts As System.Windows.Forms.CheckBox
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
    Friend WithEvents gbMovieImagesEFanarts As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieEFanartsOnly As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieEFanartsWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieEFanartsHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieEFanartsQual As System.Windows.Forms.Label
    Friend WithEvents tbMovieEFanartsQual As System.Windows.Forms.TrackBar
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents chkMovieResizeEFanarts As System.Windows.Forms.CheckBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents cbMovieEFanartsSize As System.Windows.Forms.ComboBox
    Friend WithEvents chkMovieOverwriteEFanarts As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieImagesEThumbs As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieEThumbsOnly As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieEThumbsWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieEThumbsHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblMovieEThumbsQual As System.Windows.Forms.Label
    Friend WithEvents tbMovieEThumbsQual As System.Windows.Forms.TrackBar
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chkMovieResizeEThumbs As System.Windows.Forms.CheckBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cbMovieEThumbsSize As System.Windows.Forms.ComboBox
    Friend WithEvents chkMovieOverwriteEThumbs As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieXBMCOptional As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieXBMCTrailerFormat As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieWatchedCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieEFanartsCol As System.Windows.Forms.CheckBox
    Friend WithEvents chkTitleFallback As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieSetsFolder As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowseMoviesets As System.Windows.Forms.Button
    Friend WithEvents txtMoviesetsPath As System.Windows.Forms.TextBox
    Friend WithEvents gb_DAEMON As System.Windows.Forms.GroupBox
    Friend WithEvents bt_DAEMON_Programpath As System.Windows.Forms.Button
    Friend WithEvents txt_DAEMON_Programpath As System.Windows.Forms.TextBox
    Friend WithEvents lbl_DAEMON_Programpath As System.Windows.Forms.Label
    Friend WithEvents lbl_DAEMON_driveletter As System.Windows.Forms.Label
    Friend WithEvents cbo_DAEMON_driveletter As System.Windows.Forms.ComboBox
    Friend WithEvents fileBrowse As System.Windows.Forms.OpenFileDialog
    Friend WithEvents chkMovieUseExpert As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieExpert As System.Windows.Forms.GroupBox
    Friend WithEvents tbcMovieFileNamingExpert As System.Windows.Forms.TabControl
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
    Friend WithEvents tbMovieFileNamingExpertVTS As System.Windows.Forms.TabPage
    Friend WithEvents tbMovieFileNamingExpertBDMV As System.Windows.Forms.TabPage
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
    Friend WithEvents gbMovieNMTOptional As System.Windows.Forms.GroupBox
    Friend WithEvents btnMovieBrowseWatchedFiles As System.Windows.Forms.Button
    Friend WithEvents txtMovieYAMJWatchedFolder As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieYAMJWatchedFile As System.Windows.Forms.CheckBox
    Friend WithEvents chkOutlinePlotEnglishOverwrite As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVFileNaming As System.Windows.Forms.GroupBox
    Friend WithEvents tbcTVFileNaming As System.Windows.Forms.TabControl
    Friend WithEvents tpTVFileNamingXBMC As System.Windows.Forms.TabPage
    Friend WithEvents tpTVFileNamingNMT As System.Windows.Forms.TabPage
    Friend WithEvents tpTVFileNamingExpert As System.Windows.Forms.TabPage
    Friend WithEvents gbTVFrodo As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVSeasonPosterFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowBannerFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVUseFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVEpisodeActorThumbsFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVSeasonBannerFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVEpisodeFanartFrodo As System.Windows.Forms.CheckBox
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
    Friend WithEvents btnTVBrowseTVTheme As System.Windows.Forms.Button
End Class