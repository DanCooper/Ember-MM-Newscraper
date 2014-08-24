<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgWizard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgWizard))
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.pnlWelcome = New System.Windows.Forms.Panel()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.cbIntLang = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlMovieSettings = New System.Windows.Forms.Panel()
        Me.gbMovieFileNaming = New System.Windows.Forms.GroupBox()
        Me.tbcMovieFileNaming = New System.Windows.Forms.TabControl()
        Me.tpMovieFileNamingXBMC = New System.Windows.Forms.TabPage()
        Me.gbMovieXBMCTheme = New System.Windows.Forms.GroupBox()
        Me.chkMovieXBMCThemeMovie = New System.Windows.Forms.CheckBox()
        Me.btnMovieXBMCThemeCustomPathBrowse = New System.Windows.Forms.Button()
        Me.chkMovieXBMCThemeSub = New System.Windows.Forms.CheckBox()
        Me.txtMovieXBMCThemeSubDir = New System.Windows.Forms.TextBox()
        Me.txtMovieXBMCThemeCustomPath = New System.Windows.Forms.TextBox()
        Me.chkMovieXBMCThemeCustom = New System.Windows.Forms.CheckBox()
        Me.chkMovieXBMCThemeEnable = New System.Windows.Forms.CheckBox()
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
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pnlMovieSource = New System.Windows.Forms.Panel()
        Me.lvMovies = New System.Windows.Forms.ListView()
        Me.colID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRecur = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colFolder = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSingle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnMovieRem = New System.Windows.Forms.Button()
        Me.btnMovieAddFolder = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlDone = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlTVShowSource = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnTVGeneralLangFetch = New System.Windows.Forms.Button()
        Me.cbTVGeneralLang = New System.Windows.Forms.ComboBox()
        Me.lvTVSources = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnTVRemoveSource = New System.Windows.Forms.Button()
        Me.btnTVAddSource = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlTVShowSettings = New System.Windows.Forms.Panel()
        Me.gbTVFileNaming = New System.Windows.Forms.GroupBox()
        Me.tcTVFileNaming = New System.Windows.Forms.TabControl()
        Me.tpTVFileNamingXBMC = New System.Windows.Forms.TabPage()
        Me.gbTVXBMCAdditional = New System.Windows.Forms.GroupBox()
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
        Me.tpTVFileNamingBoxee = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkTVSeasonPosterBoxee = New System.Windows.Forms.CheckBox()
        Me.chkTVShowBannerBoxee = New System.Windows.Forms.CheckBox()
        Me.chkTVEpisodePosterBoxee = New System.Windows.Forms.CheckBox()
        Me.chkTVShowFanartBoxee = New System.Windows.Forms.CheckBox()
        Me.chkTVShowPosterBoxee = New System.Windows.Forms.CheckBox()
        Me.chkTVUseBoxee = New System.Windows.Forms.CheckBox()
        Me.tpTVFileNamingExpert = New System.Windows.Forms.TabPage()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.pnlMovieSetSettings = New System.Windows.Forms.Panel()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.gbMovieSetFileNaming = New System.Windows.Forms.GroupBox()
        Me.tcMovieSetFileNaming = New System.Windows.Forms.TabControl()
        Me.tpMovieSetFileNamingXBMC = New System.Windows.Forms.TabPage()
        Me.pbMSAAInfo = New System.Windows.Forms.PictureBox()
        Me.gbMovieSetMSAAPath = New System.Windows.Forms.GroupBox()
        Me.btnMovieSetMSAAPathBrowse = New System.Windows.Forms.Button()
        Me.txtMovieSetMSAAPath = New System.Windows.Forms.TextBox()
        Me.gbMovieSetMSAA = New System.Windows.Forms.GroupBox()
        Me.chkMovieSetDiscArtMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetUseMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetLandscapeMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetBannerMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetClearArtMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetClearLogoMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetFanartMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetPosterMSAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetNFOMSAA = New System.Windows.Forms.CheckBox()
        Me.pnlWelcome.SuspendLayout()
        Me.pnlMovieSettings.SuspendLayout()
        Me.gbMovieFileNaming.SuspendLayout()
        Me.tbcMovieFileNaming.SuspendLayout()
        Me.tpMovieFileNamingXBMC.SuspendLayout()
        Me.gbMovieXBMCTheme.SuspendLayout()
        Me.gbMovieXBMCOptional.SuspendLayout()
        Me.gbMovieEden.SuspendLayout()
        Me.gbMovieFrodo.SuspendLayout()
        Me.tpMovieFileNamingNMT.SuspendLayout()
        Me.gbMovieNMTOptional.SuspendLayout()
        Me.gbMovieNMJ.SuspendLayout()
        Me.gbMovieYAMJ.SuspendLayout()
        Me.tpMovieFileNamingBoxee.SuspendLayout()
        Me.gbMovieBoxee.SuspendLayout()
        Me.tpMovieFileNamingExpert.SuspendLayout()
        Me.gbMovieExpert.SuspendLayout()
        Me.tbcMovieFileNamingExpert.SuspendLayout()
        Me.tpMovieFileNamingExpertSingle.SuspendLayout()
        Me.gbMovieExpertSingleOptionalSettings.SuspendLayout()
        Me.gbMovieExpertSingleOptionalImages.SuspendLayout()
        Me.tpMovieFileNamingExpertMulti.SuspendLayout()
        Me.gbMovieExpertMultiOptionalImages.SuspendLayout()
        Me.gbMovieExpertMultiOptionalSettings.SuspendLayout()
        Me.tpMovieFileNamingExpertVTS.SuspendLayout()
        Me.gbMovieExpertVTSOptionalSettings.SuspendLayout()
        Me.gbMovieExpertVTSOptionalImages.SuspendLayout()
        Me.tpMovieFileNamingExpertBDMV.SuspendLayout()
        Me.gbMovieExpertBDMVOptionalSettings.SuspendLayout()
        Me.gbMovieExpertBDMVOptionalImages.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMovieSource.SuspendLayout()
        Me.pnlDone.SuspendLayout()
        Me.pnlTVShowSource.SuspendLayout()
        Me.pnlTVShowSettings.SuspendLayout()
        Me.gbTVFileNaming.SuspendLayout()
        Me.tcTVFileNaming.SuspendLayout()
        Me.tpTVFileNamingXBMC.SuspendLayout()
        Me.gbTVXBMCAdditional.SuspendLayout()
        Me.gbTVFrodo.SuspendLayout()
        Me.tpTVFileNamingBoxee.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.pnlMovieSetSettings.SuspendLayout()
        Me.gbMovieSetFileNaming.SuspendLayout()
        Me.tcMovieSetFileNaming.SuspendLayout()
        Me.tpMovieSetFileNamingXBMC.SuspendLayout()
        CType(Me.pbMSAAInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMovieSetMSAAPath.SuspendLayout()
        Me.gbMovieSetMSAA.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.OK_Button.Enabled = False
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.OK_Button.Location = New System.Drawing.Point(556, 503)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Cancel_Button.Location = New System.Drawing.Point(629, 503)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'btnBack
        '
        Me.btnBack.Enabled = False
        Me.btnBack.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnBack.Location = New System.Drawing.Point(271, 503)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(67, 23)
        Me.btnBack.TabIndex = 3
        Me.btnBack.Text = "< Back"
        '
        'btnNext
        '
        Me.btnNext.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnNext.Location = New System.Drawing.Point(344, 503)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(67, 23)
        Me.btnNext.TabIndex = 2
        Me.btnNext.Text = "Next >"
        '
        'pnlWelcome
        '
        Me.pnlWelcome.BackColor = System.Drawing.Color.White
        Me.pnlWelcome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlWelcome.Controls.Add(Me.Label32)
        Me.pnlWelcome.Controls.Add(Me.cbIntLang)
        Me.pnlWelcome.Controls.Add(Me.Label2)
        Me.pnlWelcome.Controls.Add(Me.Label1)
        Me.pnlWelcome.Location = New System.Drawing.Point(166, 7)
        Me.pnlWelcome.Name = "pnlWelcome"
        Me.pnlWelcome.Size = New System.Drawing.Size(530, 490)
        Me.pnlWelcome.TabIndex = 4
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label32.Location = New System.Drawing.Point(203, 259)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(110, 13)
        Me.Label32.TabIndex = 2
        Me.Label32.Text = "Interface Language:"
        '
        'cbIntLang
        '
        Me.cbIntLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbIntLang.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbIntLang.FormattingEnabled = True
        Me.cbIntLang.Location = New System.Drawing.Point(181, 275)
        Me.cbIntLang.Name = "cbIntLang"
        Me.cbIntLang.Size = New System.Drawing.Size(148, 21)
        Me.cbIntLang.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label2.Location = New System.Drawing.Point(71, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(337, 201)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = resources.GetString("Label2.Text")
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.Location = New System.Drawing.Point(84, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(324, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Welcome to Ember Media Manager"
        '
        'pnlMovieSettings
        '
        Me.pnlMovieSettings.BackColor = System.Drawing.Color.White
        Me.pnlMovieSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMovieSettings.Controls.Add(Me.gbMovieFileNaming)
        Me.pnlMovieSettings.Controls.Add(Me.Label4)
        Me.pnlMovieSettings.Location = New System.Drawing.Point(1263, 7)
        Me.pnlMovieSettings.Name = "pnlMovieSettings"
        Me.pnlMovieSettings.Size = New System.Drawing.Size(530, 490)
        Me.pnlMovieSettings.TabIndex = 6
        Me.pnlMovieSettings.Visible = False
        '
        'gbMovieFileNaming
        '
        Me.gbMovieFileNaming.Controls.Add(Me.tbcMovieFileNaming)
        Me.gbMovieFileNaming.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieFileNaming.Location = New System.Drawing.Point(3, 97)
        Me.gbMovieFileNaming.Name = "gbMovieFileNaming"
        Me.gbMovieFileNaming.Size = New System.Drawing.Size(521, 384)
        Me.gbMovieFileNaming.TabIndex = 9
        Me.gbMovieFileNaming.TabStop = False
        Me.gbMovieFileNaming.Text = "File Naming"
        '
        'tbcMovieFileNaming
        '
        Me.tbcMovieFileNaming.Controls.Add(Me.tpMovieFileNamingXBMC)
        Me.tbcMovieFileNaming.Controls.Add(Me.tpMovieFileNamingNMT)
        Me.tbcMovieFileNaming.Controls.Add(Me.tpMovieFileNamingBoxee)
        Me.tbcMovieFileNaming.Controls.Add(Me.tpMovieFileNamingExpert)
        Me.tbcMovieFileNaming.Location = New System.Drawing.Point(6, 18)
        Me.tbcMovieFileNaming.Name = "tbcMovieFileNaming"
        Me.tbcMovieFileNaming.SelectedIndex = 0
        Me.tbcMovieFileNaming.Size = New System.Drawing.Size(513, 362)
        Me.tbcMovieFileNaming.TabIndex = 7
        '
        'tpMovieFileNamingXBMC
        '
        Me.tpMovieFileNamingXBMC.Controls.Add(Me.gbMovieXBMCTheme)
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
        Me.gbMovieXBMCTheme.Location = New System.Drawing.Point(238, 78)
        Me.gbMovieXBMCTheme.Name = "gbMovieXBMCTheme"
        Me.gbMovieXBMCTheme.Size = New System.Drawing.Size(261, 174)
        Me.gbMovieXBMCTheme.TabIndex = 4
        Me.gbMovieXBMCTheme.TabStop = False
        Me.gbMovieXBMCTheme.Text = "Theme Settings"
        '
        'chkMovieXBMCThemeMovie
        '
        Me.chkMovieXBMCThemeMovie.AutoSize = True
        Me.chkMovieXBMCThemeMovie.Enabled = False
        Me.chkMovieXBMCThemeMovie.Location = New System.Drawing.Point(7, 46)
        Me.chkMovieXBMCThemeMovie.Name = "chkMovieXBMCThemeMovie"
        Me.chkMovieXBMCThemeMovie.Size = New System.Drawing.Size(187, 17)
        Me.chkMovieXBMCThemeMovie.TabIndex = 6
        Me.chkMovieXBMCThemeMovie.Text = "Store themes in movie directory"
        Me.chkMovieXBMCThemeMovie.UseVisualStyleBackColor = True
        '
        'btnMovieXBMCThemeCustomPathBrowse
        '
        Me.btnMovieXBMCThemeCustomPathBrowse.Enabled = False
        Me.btnMovieXBMCThemeCustomPathBrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMovieXBMCThemeCustomPathBrowse.Location = New System.Drawing.Point(229, 93)
        Me.btnMovieXBMCThemeCustomPathBrowse.Name = "btnMovieXBMCThemeCustomPathBrowse"
        Me.btnMovieXBMCThemeCustomPathBrowse.Size = New System.Drawing.Size(25, 22)
        Me.btnMovieXBMCThemeCustomPathBrowse.TabIndex = 5
        Me.btnMovieXBMCThemeCustomPathBrowse.Text = "..."
        Me.btnMovieXBMCThemeCustomPathBrowse.UseVisualStyleBackColor = True
        '
        'chkMovieXBMCThemeSub
        '
        Me.chkMovieXBMCThemeSub.AutoSize = True
        Me.chkMovieXBMCThemeSub.Enabled = False
        Me.chkMovieXBMCThemeSub.Location = New System.Drawing.Point(7, 122)
        Me.chkMovieXBMCThemeSub.Name = "chkMovieXBMCThemeSub"
        Me.chkMovieXBMCThemeSub.Size = New System.Drawing.Size(181, 17)
        Me.chkMovieXBMCThemeSub.TabIndex = 4
        Me.chkMovieXBMCThemeSub.Text = "Store themes in sub directorys"
        Me.chkMovieXBMCThemeSub.UseVisualStyleBackColor = True
        '
        'txtMovieXBMCThemeSubDir
        '
        Me.txtMovieXBMCThemeSubDir.Enabled = False
        Me.txtMovieXBMCThemeSubDir.Location = New System.Drawing.Point(7, 145)
        Me.txtMovieXBMCThemeSubDir.Name = "txtMovieXBMCThemeSubDir"
        Me.txtMovieXBMCThemeSubDir.Size = New System.Drawing.Size(216, 22)
        Me.txtMovieXBMCThemeSubDir.TabIndex = 3
        '
        'txtMovieXBMCThemeCustomPath
        '
        Me.txtMovieXBMCThemeCustomPath.Enabled = False
        Me.txtMovieXBMCThemeCustomPath.Location = New System.Drawing.Point(7, 93)
        Me.txtMovieXBMCThemeCustomPath.Name = "txtMovieXBMCThemeCustomPath"
        Me.txtMovieXBMCThemeCustomPath.Size = New System.Drawing.Size(216, 22)
        Me.txtMovieXBMCThemeCustomPath.TabIndex = 2
        '
        'chkMovieXBMCThemeCustom
        '
        Me.chkMovieXBMCThemeCustom.AutoSize = True
        Me.chkMovieXBMCThemeCustom.Enabled = False
        Me.chkMovieXBMCThemeCustom.Location = New System.Drawing.Point(7, 69)
        Me.chkMovieXBMCThemeCustom.Name = "chkMovieXBMCThemeCustom"
        Me.chkMovieXBMCThemeCustom.Size = New System.Drawing.Size(182, 17)
        Me.chkMovieXBMCThemeCustom.TabIndex = 1
        Me.chkMovieXBMCThemeCustom.Text = "Store themes in a custom path"
        Me.chkMovieXBMCThemeCustom.UseVisualStyleBackColor = True
        '
        'chkMovieXBMCThemeEnable
        '
        Me.chkMovieXBMCThemeEnable.AutoSize = True
        Me.chkMovieXBMCThemeEnable.Enabled = False
        Me.chkMovieXBMCThemeEnable.Location = New System.Drawing.Point(7, 22)
        Me.chkMovieXBMCThemeEnable.Name = "chkMovieXBMCThemeEnable"
        Me.chkMovieXBMCThemeEnable.Size = New System.Drawing.Size(97, 17)
        Me.chkMovieXBMCThemeEnable.TabIndex = 0
        Me.chkMovieXBMCThemeEnable.Text = "Enable Theme"
        Me.chkMovieXBMCThemeEnable.UseVisualStyleBackColor = True
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
        Me.gbMovieNMTOptional.Controls.Add(Me.btnMovieYAMJWatchedFilesBrowse)
        Me.gbMovieNMTOptional.Controls.Add(Me.txtMovieYAMJWatchedFolder)
        Me.gbMovieNMTOptional.Controls.Add(Me.chkMovieYAMJWatchedFile)
        Me.gbMovieNMTOptional.Location = New System.Drawing.Point(238, 6)
        Me.gbMovieNMTOptional.Name = "gbMovieNMTOptional"
        Me.gbMovieNMTOptional.Size = New System.Drawing.Size(261, 84)
        Me.gbMovieNMTOptional.TabIndex = 18
        Me.gbMovieNMTOptional.TabStop = False
        Me.gbMovieNMTOptional.Text = "Optional Settings"
        '
        'btnMovieYAMJWatchedFilesBrowse
        '
        Me.btnMovieYAMJWatchedFilesBrowse.Enabled = False
        Me.btnMovieYAMJWatchedFilesBrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMovieYAMJWatchedFilesBrowse.Location = New System.Drawing.Point(230, 44)
        Me.btnMovieYAMJWatchedFilesBrowse.Name = "btnMovieYAMJWatchedFilesBrowse"
        Me.btnMovieYAMJWatchedFilesBrowse.Size = New System.Drawing.Size(25, 22)
        Me.btnMovieYAMJWatchedFilesBrowse.TabIndex = 2
        Me.btnMovieYAMJWatchedFilesBrowse.Text = "..."
        Me.btnMovieYAMJWatchedFilesBrowse.UseVisualStyleBackColor = True
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
        'tpMovieFileNamingBoxee
        '
        Me.tpMovieFileNamingBoxee.Controls.Add(Me.gbMovieBoxee)
        Me.tpMovieFileNamingBoxee.Location = New System.Drawing.Point(4, 22)
        Me.tpMovieFileNamingBoxee.Name = "tpMovieFileNamingBoxee"
        Me.tpMovieFileNamingBoxee.Size = New System.Drawing.Size(505, 336)
        Me.tpMovieFileNamingBoxee.TabIndex = 4
        Me.tpMovieFileNamingBoxee.Text = "Boxee"
        Me.tpMovieFileNamingBoxee.UseVisualStyleBackColor = True
        '
        'gbMovieBoxee
        '
        Me.gbMovieBoxee.Controls.Add(Me.chkMovieUseBoxee)
        Me.gbMovieBoxee.Controls.Add(Me.chkMovieFanartBoxee)
        Me.gbMovieBoxee.Controls.Add(Me.chkMoviePosterBoxee)
        Me.gbMovieBoxee.Controls.Add(Me.chkMovieNFOBoxee)
        Me.gbMovieBoxee.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieBoxee.Location = New System.Drawing.Point(6, 6)
        Me.gbMovieBoxee.Name = "gbMovieBoxee"
        Me.gbMovieBoxee.Size = New System.Drawing.Size(110, 324)
        Me.gbMovieBoxee.TabIndex = 5
        Me.gbMovieBoxee.TabStop = False
        Me.gbMovieBoxee.Text = "Boxee"
        '
        'chkMovieUseBoxee
        '
        Me.chkMovieUseBoxee.AutoSize = True
        Me.chkMovieUseBoxee.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieUseBoxee.Name = "chkMovieUseBoxee"
        Me.chkMovieUseBoxee.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieUseBoxee.TabIndex = 16
        Me.chkMovieUseBoxee.Text = "Use"
        Me.chkMovieUseBoxee.UseVisualStyleBackColor = True
        '
        'chkMovieFanartBoxee
        '
        Me.chkMovieFanartBoxee.AutoSize = True
        Me.chkMovieFanartBoxee.Enabled = False
        Me.chkMovieFanartBoxee.Location = New System.Drawing.Point(6, 90)
        Me.chkMovieFanartBoxee.Name = "chkMovieFanartBoxee"
        Me.chkMovieFanartBoxee.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieFanartBoxee.TabIndex = 13
        Me.chkMovieFanartBoxee.Text = "Fanart"
        Me.chkMovieFanartBoxee.UseVisualStyleBackColor = True
        '
        'chkMoviePosterBoxee
        '
        Me.chkMoviePosterBoxee.AutoSize = True
        Me.chkMoviePosterBoxee.Enabled = False
        Me.chkMoviePosterBoxee.Location = New System.Drawing.Point(6, 67)
        Me.chkMoviePosterBoxee.Name = "chkMoviePosterBoxee"
        Me.chkMoviePosterBoxee.Size = New System.Drawing.Size(58, 17)
        Me.chkMoviePosterBoxee.TabIndex = 12
        Me.chkMoviePosterBoxee.Text = "Poster"
        Me.chkMoviePosterBoxee.UseVisualStyleBackColor = True
        '
        'chkMovieNFOBoxee
        '
        Me.chkMovieNFOBoxee.AutoSize = True
        Me.chkMovieNFOBoxee.Enabled = False
        Me.chkMovieNFOBoxee.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieNFOBoxee.Name = "chkMovieNFOBoxee"
        Me.chkMovieNFOBoxee.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieNFOBoxee.TabIndex = 11
        Me.chkMovieNFOBoxee.Text = "NFO"
        Me.chkMovieNFOBoxee.UseVisualStyleBackColor = True
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
        Me.tbcMovieFileNamingExpert.Controls.Add(Me.tpMovieFileNamingExpertVTS)
        Me.tbcMovieFileNamingExpert.Controls.Add(Me.tpMovieFileNamingExpertBDMV)
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
        Me.tpMovieFileNamingExpertVTS.UseVisualStyleBackColor = True
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
        Me.tpMovieFileNamingExpertBDMV.UseVisualStyleBackColor = True
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
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(509, 93)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = resources.GetString("Label4.Text")
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Image = Global.Ember_Media_Manager.My.Resources.Resources.Wizard
        Me.PictureBox1.Location = New System.Drawing.Point(3, 7)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(157, 410)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'pnlMovieSource
        '
        Me.pnlMovieSource.BackColor = System.Drawing.Color.White
        Me.pnlMovieSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMovieSource.Controls.Add(Me.lvMovies)
        Me.pnlMovieSource.Controls.Add(Me.btnMovieRem)
        Me.pnlMovieSource.Controls.Add(Me.btnMovieAddFolder)
        Me.pnlMovieSource.Controls.Add(Me.Label3)
        Me.pnlMovieSource.Location = New System.Drawing.Point(717, 7)
        Me.pnlMovieSource.Name = "pnlMovieSource"
        Me.pnlMovieSource.Size = New System.Drawing.Size(530, 490)
        Me.pnlMovieSource.TabIndex = 5
        Me.pnlMovieSource.Visible = False
        '
        'lvMovies
        '
        Me.lvMovies.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colID, Me.colName, Me.colPath, Me.colRecur, Me.colFolder, Me.colSingle})
        Me.lvMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvMovies.FullRowSelect = True
        Me.lvMovies.HideSelection = False
        Me.lvMovies.Location = New System.Drawing.Point(6, 124)
        Me.lvMovies.Name = "lvMovies"
        Me.lvMovies.Size = New System.Drawing.Size(519, 105)
        Me.lvMovies.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvMovies.TabIndex = 1
        Me.lvMovies.UseCompatibleStateImageBehavior = False
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
        Me.colPath.Width = 220
        '
        'colRecur
        '
        Me.colRecur.Text = "Recursive"
        '
        'colFolder
        '
        Me.colFolder.Text = "Use Folder Name"
        '
        'colSingle
        '
        Me.colSingle.Text = "Single Video"
        '
        'btnMovieRem
        '
        Me.btnMovieRem.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMovieRem.Image = CType(resources.GetObject("btnMovieRem.Image"), System.Drawing.Image)
        Me.btnMovieRem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieRem.Location = New System.Drawing.Point(421, 235)
        Me.btnMovieRem.Name = "btnMovieRem"
        Me.btnMovieRem.Size = New System.Drawing.Size(104, 23)
        Me.btnMovieRem.TabIndex = 3
        Me.btnMovieRem.Text = "Remove"
        Me.btnMovieRem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieRem.UseVisualStyleBackColor = True
        '
        'btnMovieAddFolder
        '
        Me.btnMovieAddFolder.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMovieAddFolder.Image = CType(resources.GetObject("btnMovieAddFolder.Image"), System.Drawing.Image)
        Me.btnMovieAddFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieAddFolder.Location = New System.Drawing.Point(4, 236)
        Me.btnMovieAddFolder.Name = "btnMovieAddFolder"
        Me.btnMovieAddFolder.Size = New System.Drawing.Size(104, 23)
        Me.btnMovieAddFolder.TabIndex = 2
        Me.btnMovieAddFolder.Text = "Add Source"
        Me.btnMovieAddFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieAddFolder.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(519, 93)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "First, let's tell Ember Media Manager where to locate all your movies. You can ad" & _
    "d as many sources as you wish."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlDone
        '
        Me.pnlDone.BackColor = System.Drawing.Color.White
        Me.pnlDone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDone.Controls.Add(Me.Label8)
        Me.pnlDone.Controls.Add(Me.Label6)
        Me.pnlDone.Controls.Add(Me.Label7)
        Me.pnlDone.Location = New System.Drawing.Point(1263, 577)
        Me.pnlDone.Name = "pnlDone"
        Me.pnlDone.Size = New System.Drawing.Size(530, 490)
        Me.pnlDone.TabIndex = 9
        Me.pnlDone.Visible = False
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 117)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(518, 216)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = resources.GetString("Label8.Text")
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label6.Location = New System.Drawing.Point(84, 59)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(337, 46)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "That wasn't so hard was it?  As mentioned earlier, you can change these or any ot" & _
    "her options in the Settings dialog."
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label7.Location = New System.Drawing.Point(104, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(297, 50)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "That's it!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Ember Media Manager is Ready!"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlTVShowSource
        '
        Me.pnlTVShowSource.BackColor = System.Drawing.Color.White
        Me.pnlTVShowSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTVShowSource.Controls.Add(Me.Label10)
        Me.pnlTVShowSource.Controls.Add(Me.btnTVGeneralLangFetch)
        Me.pnlTVShowSource.Controls.Add(Me.cbTVGeneralLang)
        Me.pnlTVShowSource.Controls.Add(Me.lvTVSources)
        Me.pnlTVShowSource.Controls.Add(Me.btnTVRemoveSource)
        Me.pnlTVShowSource.Controls.Add(Me.btnTVAddSource)
        Me.pnlTVShowSource.Controls.Add(Me.Label9)
        Me.pnlTVShowSource.Location = New System.Drawing.Point(166, 577)
        Me.pnlTVShowSource.Name = "pnlTVShowSource"
        Me.pnlTVShowSource.Size = New System.Drawing.Size(530, 490)
        Me.pnlTVShowSource.TabIndex = 7
        Me.pnlTVShowSource.Visible = False
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label10.Location = New System.Drawing.Point(101, 278)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(332, 41)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "Now select the default language you would like Ember to look for when scraping TV" & _
    " Show items."
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnTVGeneralLangFetch
        '
        Me.btnTVGeneralLangFetch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTVGeneralLangFetch.Location = New System.Drawing.Point(184, 351)
        Me.btnTVGeneralLangFetch.Name = "btnTVGeneralLangFetch"
        Me.btnTVGeneralLangFetch.Size = New System.Drawing.Size(166, 23)
        Me.btnTVGeneralLangFetch.TabIndex = 8
        Me.btnTVGeneralLangFetch.Text = "Fetch Available Languages"
        Me.btnTVGeneralLangFetch.UseVisualStyleBackColor = True
        '
        'cbTVGeneralLang
        '
        Me.cbTVGeneralLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVGeneralLang.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbTVGeneralLang.Location = New System.Drawing.Point(184, 326)
        Me.cbTVGeneralLang.Name = "cbTVGeneralLang"
        Me.cbTVGeneralLang.Size = New System.Drawing.Size(166, 21)
        Me.cbTVGeneralLang.TabIndex = 9
        '
        'lvTVSources
        '
        Me.lvTVSources.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.lvTVSources.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvTVSources.FullRowSelect = True
        Me.lvTVSources.HideSelection = False
        Me.lvTVSources.Location = New System.Drawing.Point(6, 82)
        Me.lvTVSources.Name = "lvTVSources"
        Me.lvTVSources.Size = New System.Drawing.Size(519, 105)
        Me.lvTVSources.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvTVSources.TabIndex = 1
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
        Me.ColumnHeader3.Width = 261
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Language"
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Ordering"
        '
        'btnTVRemoveSource
        '
        Me.btnTVRemoveSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTVRemoveSource.Image = CType(resources.GetObject("btnTVRemoveSource.Image"), System.Drawing.Image)
        Me.btnTVRemoveSource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVRemoveSource.Location = New System.Drawing.Point(421, 194)
        Me.btnTVRemoveSource.Name = "btnTVRemoveSource"
        Me.btnTVRemoveSource.Size = New System.Drawing.Size(104, 23)
        Me.btnTVRemoveSource.TabIndex = 3
        Me.btnTVRemoveSource.Text = "Remove"
        Me.btnTVRemoveSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVRemoveSource.UseVisualStyleBackColor = True
        '
        'btnTVAddSource
        '
        Me.btnTVAddSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTVAddSource.Image = CType(resources.GetObject("btnTVAddSource.Image"), System.Drawing.Image)
        Me.btnTVAddSource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVAddSource.Location = New System.Drawing.Point(4, 194)
        Me.btnTVAddSource.Name = "btnTVAddSource"
        Me.btnTVAddSource.Size = New System.Drawing.Size(104, 23)
        Me.btnTVAddSource.TabIndex = 2
        Me.btnTVAddSource.Text = "Add Source"
        Me.btnTVAddSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVAddSource.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(522, 64)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Next, let's tell Ember Media Manager where to locate all your TV Shows. You can a" & _
    "dd as many sources as you wish."
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlTVShowSettings
        '
        Me.pnlTVShowSettings.BackColor = System.Drawing.Color.White
        Me.pnlTVShowSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTVShowSettings.Controls.Add(Me.gbTVFileNaming)
        Me.pnlTVShowSettings.Controls.Add(Me.Label11)
        Me.pnlTVShowSettings.Location = New System.Drawing.Point(717, 577)
        Me.pnlTVShowSettings.Name = "pnlTVShowSettings"
        Me.pnlTVShowSettings.Size = New System.Drawing.Size(530, 490)
        Me.pnlTVShowSettings.TabIndex = 8
        Me.pnlTVShowSettings.Visible = False
        '
        'gbTVFileNaming
        '
        Me.gbTVFileNaming.Controls.Add(Me.tcTVFileNaming)
        Me.gbTVFileNaming.Location = New System.Drawing.Point(3, 97)
        Me.gbTVFileNaming.Name = "gbTVFileNaming"
        Me.gbTVFileNaming.Size = New System.Drawing.Size(521, 384)
        Me.gbTVFileNaming.TabIndex = 7
        Me.gbTVFileNaming.TabStop = False
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
        Me.tcTVFileNaming.Size = New System.Drawing.Size(513, 362)
        Me.tcTVFileNaming.TabIndex = 0
        '
        'tpTVFileNamingXBMC
        '
        Me.tpTVFileNamingXBMC.Controls.Add(Me.gbTVXBMCAdditional)
        Me.tpTVFileNamingXBMC.Controls.Add(Me.gbTVFrodo)
        Me.tpTVFileNamingXBMC.Location = New System.Drawing.Point(4, 22)
        Me.tpTVFileNamingXBMC.Name = "tpTVFileNamingXBMC"
        Me.tpTVFileNamingXBMC.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTVFileNamingXBMC.Size = New System.Drawing.Size(505, 336)
        Me.tpTVFileNamingXBMC.TabIndex = 0
        Me.tpTVFileNamingXBMC.Text = "XBMC"
        Me.tpTVFileNamingXBMC.UseVisualStyleBackColor = True
        '
        'gbTVXBMCAdditional
        '
        Me.gbTVXBMCAdditional.Controls.Add(Me.btnTVShowTVThemeBrowse)
        Me.gbTVXBMCAdditional.Controls.Add(Me.txtTVShowTVThemeFolderXBMC)
        Me.gbTVXBMCAdditional.Controls.Add(Me.chkTVShowTVThemeXBMC)
        Me.gbTVXBMCAdditional.Controls.Add(Me.chkTVSeasonLandscapeXBMC)
        Me.gbTVXBMCAdditional.Controls.Add(Me.chkTVShowLandscapeXBMC)
        Me.gbTVXBMCAdditional.Controls.Add(Me.chkTVShowCharacterArtXBMC)
        Me.gbTVXBMCAdditional.Controls.Add(Me.chkTVShowClearArtXBMC)
        Me.gbTVXBMCAdditional.Controls.Add(Me.chkTVShowClearLogoXBMC)
        Me.gbTVXBMCAdditional.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gbTVXBMCAdditional.Location = New System.Drawing.Point(299, 6)
        Me.gbTVXBMCAdditional.Name = "gbTVXBMCAdditional"
        Me.gbTVXBMCAdditional.Size = New System.Drawing.Size(200, 199)
        Me.gbTVXBMCAdditional.TabIndex = 2
        Me.gbTVXBMCAdditional.TabStop = False
        Me.gbTVXBMCAdditional.Text = "Additional Stuff"
        '
        'btnTVShowTVThemeBrowse
        '
        Me.btnTVShowTVThemeBrowse.Enabled = False
        Me.btnTVShowTVThemeBrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTVShowTVThemeBrowse.Location = New System.Drawing.Point(169, 166)
        Me.btnTVShowTVThemeBrowse.Name = "btnTVShowTVThemeBrowse"
        Me.btnTVShowTVThemeBrowse.Size = New System.Drawing.Size(25, 22)
        Me.btnTVShowTVThemeBrowse.TabIndex = 7
        Me.btnTVShowTVThemeBrowse.Text = "..."
        Me.btnTVShowTVThemeBrowse.UseVisualStyleBackColor = True
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
        Me.chkTVEpisodeActorThumbsFrodo.Text = "Episode Actor Thumbs"
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
        Me.tpTVFileNamingNMT.Size = New System.Drawing.Size(505, 336)
        Me.tpTVFileNamingNMT.TabIndex = 1
        Me.tpTVFileNamingNMT.Text = "NMT"
        Me.tpTVFileNamingNMT.UseVisualStyleBackColor = True
        '
        'tpTVFileNamingBoxee
        '
        Me.tpTVFileNamingBoxee.Controls.Add(Me.GroupBox1)
        Me.tpTVFileNamingBoxee.Location = New System.Drawing.Point(4, 22)
        Me.tpTVFileNamingBoxee.Name = "tpTVFileNamingBoxee"
        Me.tpTVFileNamingBoxee.Size = New System.Drawing.Size(505, 336)
        Me.tpTVFileNamingBoxee.TabIndex = 3
        Me.tpTVFileNamingBoxee.Text = "Boxee"
        Me.tpTVFileNamingBoxee.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkTVSeasonPosterBoxee)
        Me.GroupBox1.Controls.Add(Me.chkTVShowBannerBoxee)
        Me.GroupBox1.Controls.Add(Me.chkTVEpisodePosterBoxee)
        Me.GroupBox1.Controls.Add(Me.chkTVShowFanartBoxee)
        Me.GroupBox1.Controls.Add(Me.chkTVShowPosterBoxee)
        Me.GroupBox1.Controls.Add(Me.chkTVUseBoxee)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(152, 289)
        Me.GroupBox1.TabIndex = 20
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Boxee"
        '
        'chkTVSeasonPosterBoxee
        '
        Me.chkTVSeasonPosterBoxee.AutoSize = True
        Me.chkTVSeasonPosterBoxee.Enabled = False
        Me.chkTVSeasonPosterBoxee.Location = New System.Drawing.Point(6, 113)
        Me.chkTVSeasonPosterBoxee.Name = "chkTVSeasonPosterBoxee"
        Me.chkTVSeasonPosterBoxee.Size = New System.Drawing.Size(98, 17)
        Me.chkTVSeasonPosterBoxee.TabIndex = 26
        Me.chkTVSeasonPosterBoxee.Text = "Season Poster"
        Me.chkTVSeasonPosterBoxee.UseVisualStyleBackColor = True
        '
        'chkTVShowBannerBoxee
        '
        Me.chkTVShowBannerBoxee.AutoSize = True
        Me.chkTVShowBannerBoxee.Enabled = False
        Me.chkTVShowBannerBoxee.Location = New System.Drawing.Point(6, 90)
        Me.chkTVShowBannerBoxee.Name = "chkTVShowBannerBoxee"
        Me.chkTVShowBannerBoxee.Size = New System.Drawing.Size(95, 17)
        Me.chkTVShowBannerBoxee.TabIndex = 25
        Me.chkTVShowBannerBoxee.Text = "Show Banner"
        Me.chkTVShowBannerBoxee.UseVisualStyleBackColor = True
        '
        'chkTVEpisodePosterBoxee
        '
        Me.chkTVEpisodePosterBoxee.AutoSize = True
        Me.chkTVEpisodePosterBoxee.Enabled = False
        Me.chkTVEpisodePosterBoxee.Location = New System.Drawing.Point(6, 136)
        Me.chkTVEpisodePosterBoxee.Name = "chkTVEpisodePosterBoxee"
        Me.chkTVEpisodePosterBoxee.Size = New System.Drawing.Size(102, 17)
        Me.chkTVEpisodePosterBoxee.TabIndex = 21
        Me.chkTVEpisodePosterBoxee.Text = "Episode Poster"
        Me.chkTVEpisodePosterBoxee.UseVisualStyleBackColor = True
        '
        'chkTVShowFanartBoxee
        '
        Me.chkTVShowFanartBoxee.AutoSize = True
        Me.chkTVShowFanartBoxee.Enabled = False
        Me.chkTVShowFanartBoxee.Location = New System.Drawing.Point(6, 67)
        Me.chkTVShowFanartBoxee.Name = "chkTVShowFanartBoxee"
        Me.chkTVShowFanartBoxee.Size = New System.Drawing.Size(91, 17)
        Me.chkTVShowFanartBoxee.TabIndex = 18
        Me.chkTVShowFanartBoxee.Text = "Show Fanart"
        Me.chkTVShowFanartBoxee.UseVisualStyleBackColor = True
        '
        'chkTVShowPosterBoxee
        '
        Me.chkTVShowPosterBoxee.AutoSize = True
        Me.chkTVShowPosterBoxee.Enabled = False
        Me.chkTVShowPosterBoxee.Location = New System.Drawing.Point(6, 44)
        Me.chkTVShowPosterBoxee.Name = "chkTVShowPosterBoxee"
        Me.chkTVShowPosterBoxee.Size = New System.Drawing.Size(90, 17)
        Me.chkTVShowPosterBoxee.TabIndex = 17
        Me.chkTVShowPosterBoxee.Text = "Show Poster"
        Me.chkTVShowPosterBoxee.UseVisualStyleBackColor = True
        '
        'chkTVUseBoxee
        '
        Me.chkTVUseBoxee.AutoSize = True
        Me.chkTVUseBoxee.Location = New System.Drawing.Point(6, 21)
        Me.chkTVUseBoxee.Name = "chkTVUseBoxee"
        Me.chkTVUseBoxee.Size = New System.Drawing.Size(45, 17)
        Me.chkTVUseBoxee.TabIndex = 16
        Me.chkTVUseBoxee.Text = "Use"
        Me.chkTVUseBoxee.UseVisualStyleBackColor = True
        '
        'tpTVFileNamingExpert
        '
        Me.tpTVFileNamingExpert.Location = New System.Drawing.Point(4, 22)
        Me.tpTVFileNamingExpert.Name = "tpTVFileNamingExpert"
        Me.tpTVFileNamingExpert.Size = New System.Drawing.Size(505, 336)
        Me.tpTVFileNamingExpert.TabIndex = 2
        Me.tpTVFileNamingExpert.Text = "Expert"
        Me.tpTVFileNamingExpert.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(522, 58)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = resources.GetString("Label11.Text")
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlMovieSetSettings
        '
        Me.pnlMovieSetSettings.BackColor = System.Drawing.Color.White
        Me.pnlMovieSetSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMovieSetSettings.Controls.Add(Me.gbMovieSetFileNaming)
        Me.pnlMovieSetSettings.Controls.Add(Me.Label48)
        Me.pnlMovieSetSettings.Location = New System.Drawing.Point(1799, 7)
        Me.pnlMovieSetSettings.Name = "pnlMovieSetSettings"
        Me.pnlMovieSetSettings.Size = New System.Drawing.Size(530, 490)
        Me.pnlMovieSetSettings.TabIndex = 10
        Me.pnlMovieSetSettings.Visible = False
        '
        'Label48
        '
        Me.Label48.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label48.Location = New System.Drawing.Point(9, 1)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(509, 93)
        Me.Label48.TabIndex = 0
        Me.Label48.Text = resources.GetString("Label48.Text")
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gbMovieSetFileNaming
        '
        Me.gbMovieSetFileNaming.Controls.Add(Me.tcMovieSetFileNaming)
        Me.gbMovieSetFileNaming.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieSetFileNaming.Location = New System.Drawing.Point(3, 97)
        Me.gbMovieSetFileNaming.Name = "gbMovieSetFileNaming"
        Me.gbMovieSetFileNaming.Size = New System.Drawing.Size(521, 384)
        Me.gbMovieSetFileNaming.TabIndex = 9
        Me.gbMovieSetFileNaming.TabStop = False
        Me.gbMovieSetFileNaming.Text = "File Naming"
        '
        'tcMovieSetFileNaming
        '
        Me.tcMovieSetFileNaming.Controls.Add(Me.tpMovieSetFileNamingXBMC)
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
        Me.tpMovieSetFileNamingXBMC.UseVisualStyleBackColor = True
        '
        'pbMSAAInfo
        '
        Me.pbMSAAInfo.Image = Global.Ember_Media_Manager.My.Resources.Resources.msaa
        Me.pbMSAAInfo.Location = New System.Drawing.Point(217, 74)
        Me.pbMSAAInfo.Name = "pbMSAAInfo"
        Me.pbMSAAInfo.Size = New System.Drawing.Size(250, 250)
        Me.pbMSAAInfo.TabIndex = 8
        Me.pbMSAAInfo.TabStop = False
        '
        'gbMovieSetMSAAPath
        '
        Me.gbMovieSetMSAAPath.Controls.Add(Me.btnMovieSetMSAAPathBrowse)
        Me.gbMovieSetMSAAPath.Controls.Add(Me.txtMovieSetMSAAPath)
        Me.gbMovieSetMSAAPath.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gbMovieSetMSAAPath.Location = New System.Drawing.Point(190, 6)
        Me.gbMovieSetMSAAPath.Name = "gbMovieSetMSAAPath"
        Me.gbMovieSetMSAAPath.Size = New System.Drawing.Size(307, 58)
        Me.gbMovieSetMSAAPath.TabIndex = 7
        Me.gbMovieSetMSAAPath.TabStop = False
        Me.gbMovieSetMSAAPath.Text = "MovieSet Artwork Folder"
        '
        'btnMovieSetMSAAPathBrowse
        '
        Me.btnMovieSetMSAAPathBrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMovieSetMSAAPathBrowse.Location = New System.Drawing.Point(276, 21)
        Me.btnMovieSetMSAAPathBrowse.Name = "btnMovieSetMSAAPathBrowse"
        Me.btnMovieSetMSAAPathBrowse.Size = New System.Drawing.Size(25, 22)
        Me.btnMovieSetMSAAPathBrowse.TabIndex = 1
        Me.btnMovieSetMSAAPathBrowse.Text = "..."
        Me.btnMovieSetMSAAPathBrowse.UseVisualStyleBackColor = True
        '
        'txtMovieSetMSAAPath
        '
        Me.txtMovieSetMSAAPath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieSetMSAAPath.Location = New System.Drawing.Point(6, 21)
        Me.txtMovieSetMSAAPath.Name = "txtMovieSetMSAAPath"
        Me.txtMovieSetMSAAPath.Size = New System.Drawing.Size(264, 22)
        Me.txtMovieSetMSAAPath.TabIndex = 0
        '
        'gbMovieSetMSAA
        '
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetDiscArtMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetUseMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetLandscapeMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetBannerMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetClearArtMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetClearLogoMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetFanartMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetPosterMSAA)
        Me.gbMovieSetMSAA.Controls.Add(Me.chkMovieSetNFOMSAA)
        Me.gbMovieSetMSAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieSetMSAA.Location = New System.Drawing.Point(6, 6)
        Me.gbMovieSetMSAA.Name = "gbMovieSetMSAA"
        Me.gbMovieSetMSAA.Size = New System.Drawing.Size(178, 324)
        Me.gbMovieSetMSAA.TabIndex = 0
        Me.gbMovieSetMSAA.TabStop = False
        Me.gbMovieSetMSAA.Text = "Movie Set Artwork Automator"
        '
        'chkMovieSetDiscArtMSAA
        '
        Me.chkMovieSetDiscArtMSAA.AutoSize = True
        Me.chkMovieSetDiscArtMSAA.Enabled = False
        Me.chkMovieSetDiscArtMSAA.Location = New System.Drawing.Point(6, 274)
        Me.chkMovieSetDiscArtMSAA.Name = "chkMovieSetDiscArtMSAA"
        Me.chkMovieSetDiscArtMSAA.Size = New System.Drawing.Size(62, 17)
        Me.chkMovieSetDiscArtMSAA.TabIndex = 11
        Me.chkMovieSetDiscArtMSAA.Text = "DiscArt"
        Me.chkMovieSetDiscArtMSAA.UseVisualStyleBackColor = True
        '
        'chkMovieSetUseMSAA
        '
        Me.chkMovieSetUseMSAA.AutoSize = True
        Me.chkMovieSetUseMSAA.Location = New System.Drawing.Point(6, 21)
        Me.chkMovieSetUseMSAA.Name = "chkMovieSetUseMSAA"
        Me.chkMovieSetUseMSAA.Size = New System.Drawing.Size(45, 17)
        Me.chkMovieSetUseMSAA.TabIndex = 10
        Me.chkMovieSetUseMSAA.Text = "Use"
        Me.chkMovieSetUseMSAA.UseVisualStyleBackColor = True
        '
        'chkMovieSetLandscapeMSAA
        '
        Me.chkMovieSetLandscapeMSAA.AutoSize = True
        Me.chkMovieSetLandscapeMSAA.Enabled = False
        Me.chkMovieSetLandscapeMSAA.Location = New System.Drawing.Point(6, 297)
        Me.chkMovieSetLandscapeMSAA.Name = "chkMovieSetLandscapeMSAA"
        Me.chkMovieSetLandscapeMSAA.Size = New System.Drawing.Size(80, 17)
        Me.chkMovieSetLandscapeMSAA.TabIndex = 9
        Me.chkMovieSetLandscapeMSAA.Text = "Landscape"
        Me.chkMovieSetLandscapeMSAA.UseVisualStyleBackColor = True
        '
        'chkMovieSetBannerMSAA
        '
        Me.chkMovieSetBannerMSAA.AutoSize = True
        Me.chkMovieSetBannerMSAA.Enabled = False
        Me.chkMovieSetBannerMSAA.Location = New System.Drawing.Point(6, 205)
        Me.chkMovieSetBannerMSAA.Name = "chkMovieSetBannerMSAA"
        Me.chkMovieSetBannerMSAA.Size = New System.Drawing.Size(63, 17)
        Me.chkMovieSetBannerMSAA.TabIndex = 8
        Me.chkMovieSetBannerMSAA.Text = "Banner"
        Me.chkMovieSetBannerMSAA.UseVisualStyleBackColor = True
        '
        'chkMovieSetClearArtMSAA
        '
        Me.chkMovieSetClearArtMSAA.AutoSize = True
        Me.chkMovieSetClearArtMSAA.Enabled = False
        Me.chkMovieSetClearArtMSAA.Location = New System.Drawing.Point(6, 251)
        Me.chkMovieSetClearArtMSAA.Name = "chkMovieSetClearArtMSAA"
        Me.chkMovieSetClearArtMSAA.Size = New System.Drawing.Size(67, 17)
        Me.chkMovieSetClearArtMSAA.TabIndex = 6
        Me.chkMovieSetClearArtMSAA.Text = "ClearArt"
        Me.chkMovieSetClearArtMSAA.UseVisualStyleBackColor = True
        '
        'chkMovieSetClearLogoMSAA
        '
        Me.chkMovieSetClearLogoMSAA.AutoSize = True
        Me.chkMovieSetClearLogoMSAA.Enabled = False
        Me.chkMovieSetClearLogoMSAA.Location = New System.Drawing.Point(6, 228)
        Me.chkMovieSetClearLogoMSAA.Name = "chkMovieSetClearLogoMSAA"
        Me.chkMovieSetClearLogoMSAA.Size = New System.Drawing.Size(78, 17)
        Me.chkMovieSetClearLogoMSAA.TabIndex = 5
        Me.chkMovieSetClearLogoMSAA.Text = "ClearLogo"
        Me.chkMovieSetClearLogoMSAA.UseVisualStyleBackColor = True
        '
        'chkMovieSetFanartMSAA
        '
        Me.chkMovieSetFanartMSAA.AutoSize = True
        Me.chkMovieSetFanartMSAA.Enabled = False
        Me.chkMovieSetFanartMSAA.Location = New System.Drawing.Point(6, 90)
        Me.chkMovieSetFanartMSAA.Name = "chkMovieSetFanartMSAA"
        Me.chkMovieSetFanartMSAA.Size = New System.Drawing.Size(59, 17)
        Me.chkMovieSetFanartMSAA.TabIndex = 2
        Me.chkMovieSetFanartMSAA.Text = "Fanart"
        Me.chkMovieSetFanartMSAA.UseVisualStyleBackColor = True
        '
        'chkMovieSetPosterMSAA
        '
        Me.chkMovieSetPosterMSAA.AutoSize = True
        Me.chkMovieSetPosterMSAA.Enabled = False
        Me.chkMovieSetPosterMSAA.Location = New System.Drawing.Point(6, 67)
        Me.chkMovieSetPosterMSAA.Name = "chkMovieSetPosterMSAA"
        Me.chkMovieSetPosterMSAA.Size = New System.Drawing.Size(58, 17)
        Me.chkMovieSetPosterMSAA.TabIndex = 1
        Me.chkMovieSetPosterMSAA.Text = "Poster"
        Me.chkMovieSetPosterMSAA.UseVisualStyleBackColor = True
        '
        'chkMovieSetNFOMSAA
        '
        Me.chkMovieSetNFOMSAA.AutoSize = True
        Me.chkMovieSetNFOMSAA.Enabled = False
        Me.chkMovieSetNFOMSAA.Location = New System.Drawing.Point(6, 44)
        Me.chkMovieSetNFOMSAA.Name = "chkMovieSetNFOMSAA"
        Me.chkMovieSetNFOMSAA.Size = New System.Drawing.Size(49, 17)
        Me.chkMovieSetNFOMSAA.TabIndex = 0
        Me.chkMovieSetNFOMSAA.Text = "NFO"
        Me.chkMovieSetNFOMSAA.UseVisualStyleBackColor = True
        '
        'dlgWizard
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(704, 531)
        Me.Controls.Add(Me.pnlMovieSetSettings)
        Me.Controls.Add(Me.pnlMovieSettings)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.pnlTVShowSource)
        Me.Controls.Add(Me.pnlMovieSource)
        Me.Controls.Add(Me.pnlDone)
        Me.Controls.Add(Me.pnlTVShowSettings)
        Me.Controls.Add(Me.pnlWelcome)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgWizard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Ember Startup Wizard"
        Me.TopMost = True
        Me.pnlWelcome.ResumeLayout(False)
        Me.pnlWelcome.PerformLayout()
        Me.pnlMovieSettings.ResumeLayout(False)
        Me.gbMovieFileNaming.ResumeLayout(False)
        Me.tbcMovieFileNaming.ResumeLayout(False)
        Me.tpMovieFileNamingXBMC.ResumeLayout(False)
        Me.gbMovieXBMCTheme.ResumeLayout(False)
        Me.gbMovieXBMCTheme.PerformLayout()
        Me.gbMovieXBMCOptional.ResumeLayout(False)
        Me.gbMovieXBMCOptional.PerformLayout()
        Me.gbMovieEden.ResumeLayout(False)
        Me.gbMovieEden.PerformLayout()
        Me.gbMovieFrodo.ResumeLayout(False)
        Me.gbMovieFrodo.PerformLayout()
        Me.tpMovieFileNamingNMT.ResumeLayout(False)
        Me.gbMovieNMTOptional.ResumeLayout(False)
        Me.gbMovieNMTOptional.PerformLayout()
        Me.gbMovieNMJ.ResumeLayout(False)
        Me.gbMovieNMJ.PerformLayout()
        Me.gbMovieYAMJ.ResumeLayout(False)
        Me.gbMovieYAMJ.PerformLayout()
        Me.tpMovieFileNamingBoxee.ResumeLayout(False)
        Me.gbMovieBoxee.ResumeLayout(False)
        Me.gbMovieBoxee.PerformLayout()
        Me.tpMovieFileNamingExpert.ResumeLayout(False)
        Me.gbMovieExpert.ResumeLayout(False)
        Me.gbMovieExpert.PerformLayout()
        Me.tbcMovieFileNamingExpert.ResumeLayout(False)
        Me.tpMovieFileNamingExpertSingle.ResumeLayout(False)
        Me.tpMovieFileNamingExpertSingle.PerformLayout()
        Me.gbMovieExpertSingleOptionalSettings.ResumeLayout(False)
        Me.gbMovieExpertSingleOptionalSettings.PerformLayout()
        Me.gbMovieExpertSingleOptionalImages.ResumeLayout(False)
        Me.gbMovieExpertSingleOptionalImages.PerformLayout()
        Me.tpMovieFileNamingExpertMulti.ResumeLayout(False)
        Me.tpMovieFileNamingExpertMulti.PerformLayout()
        Me.gbMovieExpertMultiOptionalImages.ResumeLayout(False)
        Me.gbMovieExpertMultiOptionalImages.PerformLayout()
        Me.gbMovieExpertMultiOptionalSettings.ResumeLayout(False)
        Me.gbMovieExpertMultiOptionalSettings.PerformLayout()
        Me.tpMovieFileNamingExpertVTS.ResumeLayout(False)
        Me.tpMovieFileNamingExpertVTS.PerformLayout()
        Me.gbMovieExpertVTSOptionalSettings.ResumeLayout(False)
        Me.gbMovieExpertVTSOptionalSettings.PerformLayout()
        Me.gbMovieExpertVTSOptionalImages.ResumeLayout(False)
        Me.gbMovieExpertVTSOptionalImages.PerformLayout()
        Me.tpMovieFileNamingExpertBDMV.ResumeLayout(False)
        Me.tpMovieFileNamingExpertBDMV.PerformLayout()
        Me.gbMovieExpertBDMVOptionalSettings.ResumeLayout(False)
        Me.gbMovieExpertBDMVOptionalSettings.PerformLayout()
        Me.gbMovieExpertBDMVOptionalImages.ResumeLayout(False)
        Me.gbMovieExpertBDMVOptionalImages.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMovieSource.ResumeLayout(False)
        Me.pnlDone.ResumeLayout(False)
        Me.pnlDone.PerformLayout()
        Me.pnlTVShowSource.ResumeLayout(False)
        Me.pnlTVShowSettings.ResumeLayout(False)
        Me.gbTVFileNaming.ResumeLayout(False)
        Me.tcTVFileNaming.ResumeLayout(False)
        Me.tpTVFileNamingXBMC.ResumeLayout(False)
        Me.gbTVXBMCAdditional.ResumeLayout(False)
        Me.gbTVXBMCAdditional.PerformLayout()
        Me.gbTVFrodo.ResumeLayout(False)
        Me.gbTVFrodo.PerformLayout()
        Me.tpTVFileNamingBoxee.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnlMovieSetSettings.ResumeLayout(False)
        Me.gbMovieSetFileNaming.ResumeLayout(False)
        Me.tcMovieSetFileNaming.ResumeLayout(False)
        Me.tpMovieSetFileNamingXBMC.ResumeLayout(False)
        CType(Me.pbMSAAInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMovieSetMSAAPath.ResumeLayout(False)
        Me.gbMovieSetMSAAPath.PerformLayout()
        Me.gbMovieSetMSAA.ResumeLayout(False)
        Me.gbMovieSetMSAA.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents pnlWelcome As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlMovieSource As System.Windows.Forms.Panel
    Friend WithEvents pnlMovieSettings As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnMovieRem As System.Windows.Forms.Button
    Friend WithEvents btnMovieAddFolder As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlDone As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lvMovies As System.Windows.Forms.ListView
    Friend WithEvents colID As System.Windows.Forms.ColumnHeader
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRecur As System.Windows.Forms.ColumnHeader
    Friend WithEvents colFolder As System.Windows.Forms.ColumnHeader
    Friend WithEvents colSingle As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents cbIntLang As System.Windows.Forms.ComboBox
    Friend WithEvents pnlTVShowSource As System.Windows.Forms.Panel
    Friend WithEvents btnTVRemoveSource As System.Windows.Forms.Button
    Friend WithEvents btnTVAddSource As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lvTVSources As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pnlTVShowSettings As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents gbMovieFileNaming As System.Windows.Forms.GroupBox
    Friend WithEvents tbcMovieFileNaming As System.Windows.Forms.TabControl
    Friend WithEvents tpMovieFileNamingXBMC As System.Windows.Forms.TabPage
    Friend WithEvents gbMovieXBMCOptional As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieXBMCProtectVTSBDMV As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieXBMCTrailerFormat As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieEden As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieExtrafanartsEden As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieExtrathumbsEden As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieUseEden As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieActorThumbsEden As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieTrailerEden As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieFanartEden As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoviePosterEden As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieNFOEden As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieFrodo As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieExtrafanartsFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieExtrathumbsFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieUseFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieLandscapeFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieBannerFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieDiscArtFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieClearArtFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieClearLogoFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieActorThumbsFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieTrailerFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieFanartFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoviePosterFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieNFOFrodo As System.Windows.Forms.CheckBox
    Friend WithEvents tpMovieFileNamingNMT As System.Windows.Forms.TabPage
    Friend WithEvents gbMovieNMTOptional As System.Windows.Forms.GroupBox
    Friend WithEvents btnMovieYAMJWatchedFilesBrowse As System.Windows.Forms.Button
    Friend WithEvents txtMovieYAMJWatchedFolder As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieYAMJWatchedFile As System.Windows.Forms.CheckBox
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
    Friend WithEvents tpMovieFileNamingExpert As System.Windows.Forms.TabPage
    Friend WithEvents gbMovieExpert As System.Windows.Forms.GroupBox
    Friend WithEvents tbcMovieFileNamingExpert As System.Windows.Forms.TabControl
    Friend WithEvents tpMovieFileNamingExpertSingle As System.Windows.Forms.TabPage
    Friend WithEvents gbMovieExpertSingleOptionalSettings As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieUnstackExpertSingle As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieStackExpertSingle As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieXBMCTrailerFormatExpertSingle As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieExpertSingleOptionalImages As System.Windows.Forms.GroupBox
    Friend WithEvents txtMovieActorThumbsExtExpertSingle As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieActorThumbsExpertSingle As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieExtrafanartsExpertSingle As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieExtrathumbsExpertSingle As System.Windows.Forms.CheckBox
    Friend WithEvents lblMovieClearArtExpertSingle As System.Windows.Forms.Label
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
    Friend WithEvents gbMovieExpertMultiOptionalImages As System.Windows.Forms.GroupBox
    Friend WithEvents txtMovieActorThumbsExtExpertMulti As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieActorThumbsExpertMulti As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieExpertMultiOptionalSettings As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieUnstackExpertMulti As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieStackExpertMulti As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieXBMCTrailerFormatExpertMulti As System.Windows.Forms.CheckBox
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
    Friend WithEvents tpMovieFileNamingExpertVTS As System.Windows.Forms.TabPage
    Friend WithEvents gbMovieExpertVTSOptionalSettings As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieRecognizeVTSExpertVTS As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieUseBaseDirectoryExpertVTS As System.Windows.Forms.CheckBox
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
    Friend WithEvents tpMovieFileNamingExpertBDMV As System.Windows.Forms.TabPage
    Friend WithEvents gbMovieExpertBDMVOptionalSettings As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieUseBaseDirectoryExpertBDMV As System.Windows.Forms.CheckBox
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
    Friend WithEvents chkMovieUseExpert As System.Windows.Forms.CheckBox
    Friend WithEvents gbTVFileNaming As System.Windows.Forms.GroupBox
    Friend WithEvents tcTVFileNaming As System.Windows.Forms.TabControl
    Friend WithEvents tpTVFileNamingXBMC As System.Windows.Forms.TabPage
    Friend WithEvents gbTVXBMCAdditional As System.Windows.Forms.GroupBox
    Friend WithEvents btnTVShowTVThemeBrowse As System.Windows.Forms.Button
    Friend WithEvents txtTVShowTVThemeFolderXBMC As System.Windows.Forms.TextBox
    Friend WithEvents chkTVShowTVThemeXBMC As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVSeasonLandscapeXBMC As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowLandscapeXBMC As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowCharacterArtXBMC As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowClearArtXBMC As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowClearLogoXBMC As System.Windows.Forms.CheckBox
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
    Friend WithEvents tpTVFileNamingNMT As System.Windows.Forms.TabPage
    Friend WithEvents tpTVFileNamingExpert As System.Windows.Forms.TabPage
    Friend WithEvents fbdBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnTVGeneralLangFetch As System.Windows.Forms.Button
    Friend WithEvents cbTVGeneralLang As System.Windows.Forms.ComboBox
    Friend WithEvents tpMovieFileNamingBoxee As System.Windows.Forms.TabPage
    Friend WithEvents gbMovieBoxee As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieUseBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieFanartBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoviePosterBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieNFOBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents gbMovieXBMCTheme As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieXBMCThemeMovie As System.Windows.Forms.CheckBox
    Friend WithEvents btnMovieXBMCThemeCustomPathBrowse As System.Windows.Forms.Button
    Friend WithEvents chkMovieXBMCThemeSub As System.Windows.Forms.CheckBox
    Friend WithEvents txtMovieXBMCThemeSubDir As System.Windows.Forms.TextBox
    Friend WithEvents txtMovieXBMCThemeCustomPath As System.Windows.Forms.TextBox
    Friend WithEvents chkMovieXBMCThemeCustom As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieXBMCThemeEnable As System.Windows.Forms.CheckBox
    Friend WithEvents tpTVFileNamingBoxee As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkTVSeasonPosterBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowBannerBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVEpisodePosterBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowFanartBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVShowPosterBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents chkTVUseBoxee As System.Windows.Forms.CheckBox
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pnlMovieSetSettings As System.Windows.Forms.Panel
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents gbMovieSetFileNaming As System.Windows.Forms.GroupBox
    Friend WithEvents tcMovieSetFileNaming As System.Windows.Forms.TabControl
    Friend WithEvents tpMovieSetFileNamingXBMC As System.Windows.Forms.TabPage
    Friend WithEvents pbMSAAInfo As System.Windows.Forms.PictureBox
    Friend WithEvents gbMovieSetMSAAPath As System.Windows.Forms.GroupBox
    Friend WithEvents btnMovieSetMSAAPathBrowse As System.Windows.Forms.Button
    Friend WithEvents txtMovieSetMSAAPath As System.Windows.Forms.TextBox
    Friend WithEvents gbMovieSetMSAA As System.Windows.Forms.GroupBox
    Friend WithEvents chkMovieSetDiscArtMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetUseMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetLandscapeMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetBannerMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetClearArtMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetClearLogoMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetFanartMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetPosterMSAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMovieSetNFOMSAA As System.Windows.Forms.CheckBox
End Class
