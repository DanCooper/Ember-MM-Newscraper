<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgEditMovie
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgEditMovie))
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Local Subtitles", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("1")
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tblTop = New System.Windows.Forms.TableLayoutPanel()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.tcEdit = New System.Windows.Forms.TabControl()
        Me.tpDetails = New System.Windows.Forms.TabPage()
        Me.tblDetails = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.lblOriginalTitle = New System.Windows.Forms.Label()
        Me.txtOriginalTitle = New System.Windows.Forms.TextBox()
        Me.lblSortTilte = New System.Windows.Forms.Label()
        Me.txtSortTitle = New System.Windows.Forms.TextBox()
        Me.lblTagline = New System.Windows.Forms.Label()
        Me.txtTagline = New System.Windows.Forms.TextBox()
        Me.lblOutline = New System.Windows.Forms.Label()
        Me.txtMPAA = New System.Windows.Forms.TextBox()
        Me.dtpLastPlayed = New System.Windows.Forms.DateTimePicker()
        Me.chkWatched = New System.Windows.Forms.CheckBox()
        Me.lblCountries = New System.Windows.Forms.Label()
        Me.lblVideoSource = New System.Windows.Forms.Label()
        Me.btnActorDown = New System.Windows.Forms.Button()
        Me.btnActorUp = New System.Windows.Forms.Button()
        Me.btnPlayTrailer = New System.Windows.Forms.Button()
        Me.btnDLTrailer = New System.Windows.Forms.Button()
        Me.clbGenres = New System.Windows.Forms.CheckedListBox()
        Me.btnStudio = New System.Windows.Forms.Button()
        Me.lblStudios = New System.Windows.Forms.Label()
        Me.lblTrailerURL = New System.Windows.Forms.Label()
        Me.txtTrailer = New System.Windows.Forms.TextBox()
        Me.lblReleaseDate = New System.Windows.Forms.Label()
        Me.lblCredits = New System.Windows.Forms.Label()
        Me.lblCertifications = New System.Windows.Forms.Label()
        Me.lblRuntime = New System.Windows.Forms.Label()
        Me.txtRuntime = New System.Windows.Forms.TextBox()
        Me.lblMPAADesc = New System.Windows.Forms.Label()
        Me.txtMPAADesc = New System.Windows.Forms.TextBox()
        Me.btnActorEdit = New System.Windows.Forms.Button()
        Me.btnActorAdd = New System.Windows.Forms.Button()
        Me.btnManual = New System.Windows.Forms.Button()
        Me.btnActorRemove = New System.Windows.Forms.Button()
        Me.lblActors = New System.Windows.Forms.Label()
        Me.lvActors = New System.Windows.Forms.ListView()
        Me.colID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRole = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colThumb = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lbMPAA = New System.Windows.Forms.ListBox()
        Me.lblGenres = New System.Windows.Forms.Label()
        Me.lblMPAA = New System.Windows.Forms.Label()
        Me.lblDirectors = New System.Windows.Forms.Label()
        Me.txtUserRating = New System.Windows.Forms.TextBox()
        Me.txtTop250 = New System.Windows.Forms.TextBox()
        Me.lblUserRating = New System.Windows.Forms.Label()
        Me.lblTop250 = New System.Windows.Forms.Label()
        Me.lblPlot = New System.Windows.Forms.Label()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.txtOutline = New System.Windows.Forms.TextBox()
        Me.lblRatings = New System.Windows.Forms.Label()
        Me.txtYear = New System.Windows.Forms.MaskedTextBox()
        Me.tpPoster = New System.Windows.Forms.TabPage()
        Me.btnSetPosterDL = New System.Windows.Forms.Button()
        Me.btnRemovePoster = New System.Windows.Forms.Button()
        Me.lblPosterSize = New System.Windows.Forms.Label()
        Me.btnSetPosterScrape = New System.Windows.Forms.Button()
        Me.btnSetPosterLocal = New System.Windows.Forms.Button()
        Me.pbPoster = New System.Windows.Forms.PictureBox()
        Me.tpBanner = New System.Windows.Forms.TabPage()
        Me.btnSetBannerDL = New System.Windows.Forms.Button()
        Me.btnRemoveBanner = New System.Windows.Forms.Button()
        Me.lblBannerSize = New System.Windows.Forms.Label()
        Me.btnSetBannerScrape = New System.Windows.Forms.Button()
        Me.btnSetBannerLocal = New System.Windows.Forms.Button()
        Me.pbBanner = New System.Windows.Forms.PictureBox()
        Me.tpLandscape = New System.Windows.Forms.TabPage()
        Me.btnSetLandscapeDL = New System.Windows.Forms.Button()
        Me.btnRemoveLandscape = New System.Windows.Forms.Button()
        Me.lblLandscapeSize = New System.Windows.Forms.Label()
        Me.btnSetLandscapeScrape = New System.Windows.Forms.Button()
        Me.btnSetLandscapeLocal = New System.Windows.Forms.Button()
        Me.pbLandscape = New System.Windows.Forms.PictureBox()
        Me.tpClearArt = New System.Windows.Forms.TabPage()
        Me.btnSetClearArtDL = New System.Windows.Forms.Button()
        Me.btnRemoveClearArt = New System.Windows.Forms.Button()
        Me.lblClearArtSize = New System.Windows.Forms.Label()
        Me.btnSetClearArtScrape = New System.Windows.Forms.Button()
        Me.btnSetClearArtLocal = New System.Windows.Forms.Button()
        Me.pbClearArt = New System.Windows.Forms.PictureBox()
        Me.tpClearLogo = New System.Windows.Forms.TabPage()
        Me.btnSetClearLogoDL = New System.Windows.Forms.Button()
        Me.btnRemoveClearLogo = New System.Windows.Forms.Button()
        Me.lblClearLogoSize = New System.Windows.Forms.Label()
        Me.btnSetClearLogoScrape = New System.Windows.Forms.Button()
        Me.btnSetClearLogoLocal = New System.Windows.Forms.Button()
        Me.pbClearLogo = New System.Windows.Forms.PictureBox()
        Me.tpDiscArt = New System.Windows.Forms.TabPage()
        Me.btnSetDiscArtDL = New System.Windows.Forms.Button()
        Me.btnRemoveDiscArt = New System.Windows.Forms.Button()
        Me.lblDiscArtSize = New System.Windows.Forms.Label()
        Me.btnSetDiscArtScrape = New System.Windows.Forms.Button()
        Me.btnSetDiscArtLocal = New System.Windows.Forms.Button()
        Me.pbDiscArt = New System.Windows.Forms.PictureBox()
        Me.tpFanart = New System.Windows.Forms.TabPage()
        Me.btnSetFanartDL = New System.Windows.Forms.Button()
        Me.btnRemoveFanart = New System.Windows.Forms.Button()
        Me.lblFanartSize = New System.Windows.Forms.Label()
        Me.btnSetFanartScrape = New System.Windows.Forms.Button()
        Me.btnSetFanartLocal = New System.Windows.Forms.Button()
        Me.pbFanart = New System.Windows.Forms.PictureBox()
        Me.tpExtrafanarts = New System.Windows.Forms.TabPage()
        Me.btnSetExtrafanartsScrape = New System.Windows.Forms.Button()
        Me.lblExtrafanartsSize = New System.Windows.Forms.Label()
        Me.pnlExtrafanarts = New System.Windows.Forms.Panel()
        Me.pnlExtrafanartsSetAsFanart = New System.Windows.Forms.Panel()
        Me.btnExtrafanartsSetAsFanart = New System.Windows.Forms.Button()
        Me.btnExtrafanartsRefresh = New System.Windows.Forms.Button()
        Me.btnExtrafanartsRemove = New System.Windows.Forms.Button()
        Me.pbExtrafanarts = New System.Windows.Forms.PictureBox()
        Me.tpExtrathumbs = New System.Windows.Forms.TabPage()
        Me.btnSetExtrathumbsScrape = New System.Windows.Forms.Button()
        Me.lblExtrathumbsSize = New System.Windows.Forms.Label()
        Me.pnlExtrathumbs = New System.Windows.Forms.Panel()
        Me.pnlExtrathumbsSetAsFanart = New System.Windows.Forms.Panel()
        Me.btnExtrathumbsSetAsFanart = New System.Windows.Forms.Button()
        Me.btnExtrathumbsRefresh = New System.Windows.Forms.Button()
        Me.btnExtrathumbsRemove = New System.Windows.Forms.Button()
        Me.btnExtrathumbsDown = New System.Windows.Forms.Button()
        Me.btnExtrathumbsUp = New System.Windows.Forms.Button()
        Me.pbExtrathumbs = New System.Windows.Forms.PictureBox()
        Me.tpFrameExtraction = New System.Windows.Forms.TabPage()
        Me.btnFrameSaveAsExtrafanart = New System.Windows.Forms.Button()
        Me.btnFrameSaveAsFanart = New System.Windows.Forms.Button()
        Me.btnFrameSaveAsExtrathumb = New System.Windows.Forms.Button()
        Me.pnlFrameProgress = New System.Windows.Forms.Panel()
        Me.lblExtractingFrame = New System.Windows.Forms.Label()
        Me.prbExtractingFrame = New System.Windows.Forms.ProgressBar()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.tbFrame = New System.Windows.Forms.TrackBar()
        Me.btnFrameLoad = New System.Windows.Forms.Button()
        Me.pbFrame = New System.Windows.Forms.PictureBox()
        Me.tpSubtitles = New System.Windows.Forms.TabPage()
        Me.lblSubtitlesPreview = New System.Windows.Forms.Label()
        Me.txtSubtitlesPreview = New System.Windows.Forms.TextBox()
        Me.lvSubtitles = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnRemoveSubtitle = New System.Windows.Forms.Button()
        Me.btnSetSubtitleDL = New System.Windows.Forms.Button()
        Me.btnSetSubtitleScrape = New System.Windows.Forms.Button()
        Me.btnSetSubtitleLocal = New System.Windows.Forms.Button()
        Me.tpTrailer = New System.Windows.Forms.TabPage()
        Me.btnLocalTrailerPlay = New System.Windows.Forms.Button()
        Me.txtLocalTrailer = New System.Windows.Forms.TextBox()
        Me.pnlTrailerPreview = New System.Windows.Forms.Panel()
        Me.pnlTrailerPreviewNoPlayer = New System.Windows.Forms.Panel()
        Me.tblTrailerPreviewNoPlayer = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTrailerPreviewNoPlayer = New System.Windows.Forms.Label()
        Me.btnSetTrailerDL = New System.Windows.Forms.Button()
        Me.btnRemoveTrailer = New System.Windows.Forms.Button()
        Me.btnSetTrailerScrape = New System.Windows.Forms.Button()
        Me.btnSetTrailerLocal = New System.Windows.Forms.Button()
        Me.tpTheme = New System.Windows.Forms.TabPage()
        Me.btnLocalThemePlay = New System.Windows.Forms.Button()
        Me.txtLocalTheme = New System.Windows.Forms.TextBox()
        Me.pnlThemePreview = New System.Windows.Forms.Panel()
        Me.pnlThemePreviewNoPlayer = New System.Windows.Forms.Panel()
        Me.tblThemePreviewNoPlayer = New System.Windows.Forms.TableLayoutPanel()
        Me.lblThemePreviewNoPlayer = New System.Windows.Forms.Label()
        Me.btnSetThemeDL = New System.Windows.Forms.Button()
        Me.btnRemoveTheme = New System.Windows.Forms.Button()
        Me.btnSetThemeScrape = New System.Windows.Forms.Button()
        Me.btnSetThemeLocal = New System.Windows.Forms.Button()
        Me.tpMetaData = New System.Windows.Forms.TabPage()
        Me.pnlFileInfo = New System.Windows.Forms.Panel()
        Me.tpMediaStub = New System.Windows.Forms.TabPage()
        Me.lblMediaStubMessage = New System.Windows.Forms.Label()
        Me.lblMediaStubTitle = New System.Windows.Forms.Label()
        Me.txtMediaStubMessage = New System.Windows.Forms.TextBox()
        Me.txtMediaStubTitle = New System.Windows.Forms.TextBox()
        Me.ofdLocalFiles = New System.Windows.Forms.OpenFileDialog()
        Me.btnRescrape = New System.Windows.Forms.Button()
        Me.btnChangeMovie = New System.Windows.Forms.Button()
        Me.tmrDelay = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.tsFilename = New System.Windows.Forms.ToolStripStatusLabel()
        Me.cbSourceLanguage = New System.Windows.Forms.ComboBox()
        Me.lblLanguage = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMarked = New System.Windows.Forms.CheckBox()
        Me.dtpReleaseDate = New System.Windows.Forms.DateTimePicker()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.chkLocked = New System.Windows.Forms.CheckBox()
        Me.lblTags = New System.Windows.Forms.Label()
        Me.clbTags = New System.Windows.Forms.CheckedListBox()
        Me.cbVideoSource = New System.Windows.Forms.ComboBox()
        Me.lblMovieSet = New System.Windows.Forms.Label()
        Me.dgvCredits = New System.Windows.Forms.DataGridView()
        Me.dgvStudios = New System.Windows.Forms.DataGridView()
        Me.dgvDirectors = New System.Windows.Forms.DataGridView()
        Me.dgvCountries = New System.Windows.Forms.DataGridView()
        Me.colCreditsName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDirectorsName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colStudiosName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCountriesName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlTop.SuspendLayout()
        Me.tblTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcEdit.SuspendLayout()
        Me.tpDetails.SuspendLayout()
        Me.tblDetails.SuspendLayout()
        Me.tpPoster.SuspendLayout()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpBanner.SuspendLayout()
        CType(Me.pbBanner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpLandscape.SuspendLayout()
        CType(Me.pbLandscape, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpClearArt.SuspendLayout()
        CType(Me.pbClearArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpClearLogo.SuspendLayout()
        CType(Me.pbClearLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpDiscArt.SuspendLayout()
        CType(Me.pbDiscArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpFanart.SuspendLayout()
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpExtrafanarts.SuspendLayout()
        Me.pnlExtrafanartsSetAsFanart.SuspendLayout()
        CType(Me.pbExtrafanarts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpExtrathumbs.SuspendLayout()
        Me.pnlExtrathumbsSetAsFanart.SuspendLayout()
        CType(Me.pbExtrathumbs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpFrameExtraction.SuspendLayout()
        Me.pnlFrameProgress.SuspendLayout()
        CType(Me.tbFrame, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFrame, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpSubtitles.SuspendLayout()
        Me.tpTrailer.SuspendLayout()
        Me.pnlTrailerPreview.SuspendLayout()
        Me.pnlTrailerPreviewNoPlayer.SuspendLayout()
        Me.tblTrailerPreviewNoPlayer.SuspendLayout()
        Me.tpTheme.SuspendLayout()
        Me.pnlThemePreview.SuspendLayout()
        Me.pnlThemePreviewNoPlayer.SuspendLayout()
        Me.tblThemePreviewNoPlayer.SuspendLayout()
        Me.tpMetaData.SuspendLayout()
        Me.tpMediaStub.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        CType(Me.dgvCredits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvStudios, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDirectors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCountries, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.AutoSize = True
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnOK.Location = New System.Drawing.Point(1778, 26)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(67, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(1851, 26)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(77, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'pnlTop
        '
        Me.pnlTop.AutoSize = True
        Me.pnlTop.BackColor = System.Drawing.Color.LightSteelBlue
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.tblTop)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1931, 56)
        Me.pnlTop.TabIndex = 2
        '
        'tblTop
        '
        Me.tblTop.AutoSize = True
        Me.tblTop.ColumnCount = 2
        Me.tblTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTop.Controls.Add(Me.pbTopLogo, 0, 0)
        Me.tblTop.Controls.Add(Me.lblTopDetails, 1, 1)
        Me.tblTop.Controls.Add(Me.lblTopTitle, 1, 0)
        Me.tblTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTop.Location = New System.Drawing.Point(0, 0)
        Me.tblTop.Name = "tblTop"
        Me.tblTop.RowCount = 2
        Me.tblTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTop.Size = New System.Drawing.Size(1929, 54)
        Me.tblTop.TabIndex = 2
        '
        'pbTopLogo
        '
        Me.pbTopLogo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pbTopLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbTopLogo.Image = CType(resources.GetObject("pbTopLogo.Image"), System.Drawing.Image)
        Me.pbTopLogo.Location = New System.Drawing.Point(3, 3)
        Me.pbTopLogo.Name = "pbTopLogo"
        Me.tblTop.SetRowSpan(Me.pbTopLogo, 2)
        Me.pbTopLogo.Size = New System.Drawing.Size(48, 48)
        Me.pbTopLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbTopLogo.TabIndex = 0
        Me.pbTopLogo.TabStop = False
        '
        'lblTopDetails
        '
        Me.lblTopDetails.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTopDetails.AutoSize = True
        Me.lblTopDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblTopDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopDetails.ForeColor = System.Drawing.Color.White
        Me.lblTopDetails.Location = New System.Drawing.Point(57, 36)
        Me.lblTopDetails.Name = "lblTopDetails"
        Me.lblTopDetails.Size = New System.Drawing.Size(205, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Edit the details for the selected movie."
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(57, 0)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(137, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Edit Movie"
        '
        'tcEdit
        '
        Me.tcEdit.Controls.Add(Me.tpDetails)
        Me.tcEdit.Controls.Add(Me.tpPoster)
        Me.tcEdit.Controls.Add(Me.tpBanner)
        Me.tcEdit.Controls.Add(Me.tpLandscape)
        Me.tcEdit.Controls.Add(Me.tpClearArt)
        Me.tcEdit.Controls.Add(Me.tpClearLogo)
        Me.tcEdit.Controls.Add(Me.tpDiscArt)
        Me.tcEdit.Controls.Add(Me.tpFanart)
        Me.tcEdit.Controls.Add(Me.tpExtrafanarts)
        Me.tcEdit.Controls.Add(Me.tpExtrathumbs)
        Me.tcEdit.Controls.Add(Me.tpFrameExtraction)
        Me.tcEdit.Controls.Add(Me.tpSubtitles)
        Me.tcEdit.Controls.Add(Me.tpTrailer)
        Me.tcEdit.Controls.Add(Me.tpTheme)
        Me.tcEdit.Controls.Add(Me.tpMetaData)
        Me.tcEdit.Controls.Add(Me.tpMediaStub)
        Me.tcEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tcEdit.Location = New System.Drawing.Point(0, 0)
        Me.tcEdit.Name = "tcEdit"
        Me.tcEdit.SelectedIndex = 0
        Me.tcEdit.Size = New System.Drawing.Size(1931, 631)
        Me.tcEdit.TabIndex = 3
        '
        'tpDetails
        '
        Me.tpDetails.Controls.Add(Me.tblDetails)
        Me.tpDetails.Controls.Add(Me.txtMPAA)
        Me.tpDetails.Controls.Add(Me.btnActorDown)
        Me.tpDetails.Controls.Add(Me.btnActorUp)
        Me.tpDetails.Controls.Add(Me.btnStudio)
        Me.tpDetails.Controls.Add(Me.lblCertifications)
        Me.tpDetails.Controls.Add(Me.lblMPAADesc)
        Me.tpDetails.Controls.Add(Me.txtMPAADesc)
        Me.tpDetails.Controls.Add(Me.btnActorEdit)
        Me.tpDetails.Controls.Add(Me.btnActorAdd)
        Me.tpDetails.Controls.Add(Me.btnManual)
        Me.tpDetails.Controls.Add(Me.btnActorRemove)
        Me.tpDetails.Controls.Add(Me.lblActors)
        Me.tpDetails.Controls.Add(Me.lvActors)
        Me.tpDetails.Controls.Add(Me.lbMPAA)
        Me.tpDetails.Controls.Add(Me.lblMPAA)
        Me.tpDetails.Controls.Add(Me.txtUserRating)
        Me.tpDetails.Controls.Add(Me.txtTop250)
        Me.tpDetails.Controls.Add(Me.lblUserRating)
        Me.tpDetails.Controls.Add(Me.lblTop250)
        Me.tpDetails.Controls.Add(Me.lblRatings)
        Me.tpDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpDetails.Name = "tpDetails"
        Me.tpDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDetails.Size = New System.Drawing.Size(1923, 605)
        Me.tpDetails.TabIndex = 0
        Me.tpDetails.Text = "Details"
        Me.tpDetails.UseVisualStyleBackColor = True
        '
        'tblDetails
        '
        Me.tblDetails.ColumnCount = 8
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 186.0!))
        Me.tblDetails.Controls.Add(Me.cbVideoSource, 1, 9)
        Me.tblDetails.Controls.Add(Me.lblTitle, 0, 0)
        Me.tblDetails.Controls.Add(Me.txtTitle, 0, 1)
        Me.tblDetails.Controls.Add(Me.lblOriginalTitle, 0, 2)
        Me.tblDetails.Controls.Add(Me.txtOriginalTitle, 0, 3)
        Me.tblDetails.Controls.Add(Me.lblCountries, 3, 14)
        Me.tblDetails.Controls.Add(Me.lblSortTilte, 0, 4)
        Me.tblDetails.Controls.Add(Me.txtSortTitle, 0, 5)
        Me.tblDetails.Controls.Add(Me.lblTagline, 2, 0)
        Me.tblDetails.Controls.Add(Me.txtTagline, 2, 1)
        Me.tblDetails.Controls.Add(Me.clbGenres, 0, 13)
        Me.tblDetails.Controls.Add(Me.lblStudios, 3, 12)
        Me.tblDetails.Controls.Add(Me.lblOutline, 2, 2)
        Me.tblDetails.Controls.Add(Me.txtOutline, 2, 3)
        Me.tblDetails.Controls.Add(Me.lblPlot, 3, 2)
        Me.tblDetails.Controls.Add(Me.lblCredits, 2, 12)
        Me.tblDetails.Controls.Add(Me.txtPlot, 3, 3)
        Me.tblDetails.Controls.Add(Me.lblYear, 0, 6)
        Me.tblDetails.Controls.Add(Me.txtYear, 0, 7)
        Me.tblDetails.Controls.Add(Me.lblReleaseDate, 1, 6)
        Me.tblDetails.Controls.Add(Me.dtpReleaseDate, 1, 7)
        Me.tblDetails.Controls.Add(Me.lblRuntime, 0, 8)
        Me.tblDetails.Controls.Add(Me.txtRuntime, 0, 9)
        Me.tblDetails.Controls.Add(Me.lblVideoSource, 1, 8)
        Me.tblDetails.Controls.Add(Me.lblGenres, 0, 12)
        Me.tblDetails.Controls.Add(Me.lblTags, 0, 14)
        Me.tblDetails.Controls.Add(Me.lblDirectors, 2, 14)
        Me.tblDetails.Controls.Add(Me.clbTags, 0, 15)
        Me.tblDetails.Controls.Add(Me.btnPlayTrailer, 6, 9)
        Me.tblDetails.Controls.Add(Me.btnDLTrailer, 5, 9)
        Me.tblDetails.Controls.Add(Me.lblTrailerURL, 2, 8)
        Me.tblDetails.Controls.Add(Me.lblMovieSet, 2, 10)
        Me.tblDetails.Controls.Add(Me.txtTrailer, 2, 9)
        Me.tblDetails.Controls.Add(Me.chkWatched, 0, 10)
        Me.tblDetails.Controls.Add(Me.dtpLastPlayed, 0, 11)
        Me.tblDetails.Controls.Add(Me.dgvCredits, 2, 13)
        Me.tblDetails.Controls.Add(Me.dgvStudios, 3, 13)
        Me.tblDetails.Controls.Add(Me.dgvDirectors, 2, 15)
        Me.tblDetails.Controls.Add(Me.dgvCountries, 3, 15)
        Me.tblDetails.Location = New System.Drawing.Point(8, 16)
        Me.tblDetails.Name = "tblDetails"
        Me.tblDetails.RowCount = 17
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblDetails.Size = New System.Drawing.Size(1069, 580)
        Me.tblDetails.TabIndex = 78
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblTitle, 2)
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(3, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(31, 13)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Title:"
        '
        'txtTitle
        '
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtTitle, 2)
        Me.txtTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(3, 16)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(220, 22)
        Me.txtTitle.TabIndex = 1
        '
        'lblOriginalTitle
        '
        Me.lblOriginalTitle.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblOriginalTitle, 2)
        Me.lblOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblOriginalTitle.Location = New System.Drawing.Point(3, 41)
        Me.lblOriginalTitle.Name = "lblOriginalTitle"
        Me.lblOriginalTitle.Size = New System.Drawing.Size(76, 13)
        Me.lblOriginalTitle.TabIndex = 2
        Me.lblOriginalTitle.Text = "Original Title:"
        '
        'txtOriginalTitle
        '
        Me.txtOriginalTitle.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtOriginalTitle, 2)
        Me.txtOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtOriginalTitle.Location = New System.Drawing.Point(3, 57)
        Me.txtOriginalTitle.Name = "txtOriginalTitle"
        Me.txtOriginalTitle.Size = New System.Drawing.Size(220, 22)
        Me.txtOriginalTitle.TabIndex = 3
        '
        'lblSortTilte
        '
        Me.lblSortTilte.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblSortTilte, 2)
        Me.lblSortTilte.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSortTilte.Location = New System.Drawing.Point(3, 82)
        Me.lblSortTilte.Name = "lblSortTilte"
        Me.lblSortTilte.Size = New System.Drawing.Size(55, 13)
        Me.lblSortTilte.TabIndex = 4
        Me.lblSortTilte.Text = "Sort Title:"
        '
        'txtSortTitle
        '
        Me.txtSortTitle.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtSortTitle, 2)
        Me.txtSortTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSortTitle.Location = New System.Drawing.Point(3, 98)
        Me.txtSortTitle.Name = "txtSortTitle"
        Me.txtSortTitle.Size = New System.Drawing.Size(220, 22)
        Me.txtSortTitle.TabIndex = 5
        '
        'lblTagline
        '
        Me.lblTagline.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblTagline, 5)
        Me.lblTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTagline.Location = New System.Drawing.Point(229, 0)
        Me.lblTagline.Name = "lblTagline"
        Me.lblTagline.Size = New System.Drawing.Size(46, 13)
        Me.lblTagline.TabIndex = 6
        Me.lblTagline.Text = "Tagline:"
        '
        'txtTagline
        '
        Me.txtTagline.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtTagline, 5)
        Me.txtTagline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTagline.Location = New System.Drawing.Point(229, 16)
        Me.txtTagline.Name = "txtTagline"
        Me.txtTagline.Size = New System.Drawing.Size(638, 22)
        Me.txtTagline.TabIndex = 7
        '
        'lblOutline
        '
        Me.lblOutline.AutoSize = True
        Me.lblOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblOutline.Location = New System.Drawing.Point(229, 41)
        Me.lblOutline.Name = "lblOutline"
        Me.lblOutline.Size = New System.Drawing.Size(49, 13)
        Me.lblOutline.TabIndex = 25
        Me.lblOutline.Text = "Outline:"
        '
        'txtMPAA
        '
        Me.txtMPAA.BackColor = System.Drawing.SystemColors.Window
        Me.txtMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtMPAA.Location = New System.Drawing.Point(1685, 471)
        Me.txtMPAA.Name = "txtMPAA"
        Me.txtMPAA.Size = New System.Drawing.Size(213, 22)
        Me.txtMPAA.TabIndex = 73
        '
        'dtpLastPlayed
        '
        Me.dtpLastPlayed.Checked = False
        Me.tblDetails.SetColumnSpan(Me.dtpLastPlayed, 2)
        Me.dtpLastPlayed.CustomFormat = "yyyy-dd-MM / hh:mm:ss"
        Me.dtpLastPlayed.Enabled = False
        Me.dtpLastPlayed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLastPlayed.Location = New System.Drawing.Point(3, 232)
        Me.dtpLastPlayed.Name = "dtpLastPlayed"
        Me.dtpLastPlayed.Size = New System.Drawing.Size(220, 22)
        Me.dtpLastPlayed.TabIndex = 77
        '
        'chkWatched
        '
        Me.chkWatched.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkWatched.AutoSize = True
        Me.chkWatched.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWatched.Location = New System.Drawing.Point(3, 209)
        Me.chkWatched.Name = "chkWatched"
        Me.chkWatched.Size = New System.Drawing.Size(72, 17)
        Me.chkWatched.TabIndex = 6
        Me.chkWatched.Text = "Watched"
        Me.chkWatched.UseVisualStyleBackColor = True
        '
        'lblCountries
        '
        Me.lblCountries.AutoSize = True
        Me.lblCountries.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCountries.Location = New System.Drawing.Point(475, 426)
        Me.lblCountries.Name = "lblCountries"
        Me.lblCountries.Size = New System.Drawing.Size(60, 13)
        Me.lblCountries.TabIndex = 11
        Me.lblCountries.Text = "Countries:"
        '
        'lblVideoSource
        '
        Me.lblVideoSource.AutoSize = True
        Me.lblVideoSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoSource.Location = New System.Drawing.Point(81, 164)
        Me.lblVideoSource.Name = "lblVideoSource"
        Me.lblVideoSource.Size = New System.Drawing.Size(78, 13)
        Me.lblVideoSource.TabIndex = 47
        Me.lblVideoSource.Text = "Video Source:"
        '
        'btnActorDown
        '
        Me.btnActorDown.Image = CType(resources.GetObject("btnActorDown.Image"), System.Drawing.Image)
        Me.btnActorDown.Location = New System.Drawing.Point(1480, 495)
        Me.btnActorDown.Name = "btnActorDown"
        Me.btnActorDown.Size = New System.Drawing.Size(23, 23)
        Me.btnActorDown.TabIndex = 34
        Me.btnActorDown.UseVisualStyleBackColor = True
        '
        'btnActorUp
        '
        Me.btnActorUp.Image = CType(resources.GetObject("btnActorUp.Image"), System.Drawing.Image)
        Me.btnActorUp.Location = New System.Drawing.Point(1456, 495)
        Me.btnActorUp.Name = "btnActorUp"
        Me.btnActorUp.Size = New System.Drawing.Size(23, 23)
        Me.btnActorUp.TabIndex = 33
        Me.btnActorUp.UseVisualStyleBackColor = True
        '
        'btnPlayTrailer
        '
        Me.btnPlayTrailer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPlayTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.Play_Icon
        Me.btnPlayTrailer.Location = New System.Drawing.Point(844, 180)
        Me.btnPlayTrailer.Name = "btnPlayTrailer"
        Me.btnPlayTrailer.Size = New System.Drawing.Size(23, 23)
        Me.btnPlayTrailer.TabIndex = 52
        Me.btnPlayTrailer.UseVisualStyleBackColor = True
        '
        'btnDLTrailer
        '
        Me.btnDLTrailer.Image = CType(resources.GetObject("btnDLTrailer.Image"), System.Drawing.Image)
        Me.btnDLTrailer.Location = New System.Drawing.Point(815, 180)
        Me.btnDLTrailer.Name = "btnDLTrailer"
        Me.btnDLTrailer.Size = New System.Drawing.Size(23, 23)
        Me.btnDLTrailer.TabIndex = 53
        Me.btnDLTrailer.UseVisualStyleBackColor = True
        '
        'clbGenres
        '
        Me.clbGenres.BackColor = System.Drawing.SystemColors.Window
        Me.clbGenres.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbGenres.CheckOnClick = True
        Me.tblDetails.SetColumnSpan(Me.clbGenres, 2)
        Me.clbGenres.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbGenres.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.clbGenres.FormattingEnabled = True
        Me.clbGenres.IntegralHeight = False
        Me.clbGenres.Location = New System.Drawing.Point(3, 273)
        Me.clbGenres.Name = "clbGenres"
        Me.clbGenres.Size = New System.Drawing.Size(220, 150)
        Me.clbGenres.TabIndex = 24
        '
        'btnStudio
        '
        Me.btnStudio.Image = CType(resources.GetObject("btnStudio.Image"), System.Drawing.Image)
        Me.btnStudio.Location = New System.Drawing.Point(1652, 573)
        Me.btnStudio.Name = "btnStudio"
        Me.btnStudio.Size = New System.Drawing.Size(23, 23)
        Me.btnStudio.TabIndex = 44
        Me.btnStudio.UseVisualStyleBackColor = True
        '
        'lblStudios
        '
        Me.lblStudios.AutoSize = True
        Me.lblStudios.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblStudios.Location = New System.Drawing.Point(475, 257)
        Me.lblStudios.Name = "lblStudios"
        Me.lblStudios.Size = New System.Drawing.Size(49, 13)
        Me.lblStudios.TabIndex = 42
        Me.lblStudios.Text = "Studios:"
        '
        'lblTrailerURL
        '
        Me.lblTrailerURL.AutoSize = True
        Me.lblTrailerURL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTrailerURL.Location = New System.Drawing.Point(229, 164)
        Me.lblTrailerURL.Name = "lblTrailerURL"
        Me.lblTrailerURL.Size = New System.Drawing.Size(63, 13)
        Me.lblTrailerURL.TabIndex = 49
        Me.lblTrailerURL.Text = "Trailer URL:"
        '
        'txtTrailer
        '
        Me.txtTrailer.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtTrailer, 2)
        Me.txtTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTrailer.Location = New System.Drawing.Point(229, 180)
        Me.txtTrailer.Name = "txtTrailer"
        Me.txtTrailer.Size = New System.Drawing.Size(560, 22)
        Me.txtTrailer.TabIndex = 50
        '
        'lblReleaseDate
        '
        Me.lblReleaseDate.AutoSize = True
        Me.lblReleaseDate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblReleaseDate.Location = New System.Drawing.Point(81, 123)
        Me.lblReleaseDate.Name = "lblReleaseDate"
        Me.lblReleaseDate.Size = New System.Drawing.Size(76, 13)
        Me.lblReleaseDate.TabIndex = 13
        Me.lblReleaseDate.Text = "Release Date:"
        '
        'lblCredits
        '
        Me.lblCredits.AutoSize = True
        Me.lblCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCredits.Location = New System.Drawing.Point(229, 257)
        Me.lblCredits.Name = "lblCredits"
        Me.lblCredits.Size = New System.Drawing.Size(46, 13)
        Me.lblCredits.TabIndex = 40
        Me.lblCredits.Text = "Credits:"
        '
        'lblCertifications
        '
        Me.lblCertifications.AutoSize = True
        Me.lblCertifications.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCertifications.Location = New System.Drawing.Point(1425, 249)
        Me.lblCertifications.Name = "lblCertifications"
        Me.lblCertifications.Size = New System.Drawing.Size(78, 13)
        Me.lblCertifications.TabIndex = 45
        Me.lblCertifications.Text = "Certifications:"
        '
        'lblRuntime
        '
        Me.lblRuntime.AutoSize = True
        Me.lblRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRuntime.Location = New System.Drawing.Point(3, 164)
        Me.lblRuntime.Name = "lblRuntime"
        Me.lblRuntime.Size = New System.Drawing.Size(53, 13)
        Me.lblRuntime.TabIndex = 15
        Me.lblRuntime.Text = "Runtime:"
        '
        'txtRuntime
        '
        Me.txtRuntime.BackColor = System.Drawing.SystemColors.Window
        Me.txtRuntime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtRuntime.Location = New System.Drawing.Point(3, 180)
        Me.txtRuntime.Name = "txtRuntime"
        Me.txtRuntime.Size = New System.Drawing.Size(72, 22)
        Me.txtRuntime.TabIndex = 16
        '
        'lblMPAADesc
        '
        Me.lblMPAADesc.AutoSize = True
        Me.lblMPAADesc.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMPAADesc.Location = New System.Drawing.Point(1683, 519)
        Me.lblMPAADesc.Name = "lblMPAADesc"
        Me.lblMPAADesc.Size = New System.Drawing.Size(138, 13)
        Me.lblMPAADesc.TabIndex = 38
        Me.lblMPAADesc.Text = "MPAA Rating Description:"
        '
        'txtMPAADesc
        '
        Me.txtMPAADesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtMPAADesc.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtMPAADesc.Location = New System.Drawing.Point(1685, 535)
        Me.txtMPAADesc.Multiline = True
        Me.txtMPAADesc.Name = "txtMPAADesc"
        Me.txtMPAADesc.Size = New System.Drawing.Size(213, 60)
        Me.txtMPAADesc.TabIndex = 39
        '
        'btnActorEdit
        '
        Me.btnActorEdit.Image = CType(resources.GetObject("btnActorEdit.Image"), System.Drawing.Image)
        Me.btnActorEdit.Location = New System.Drawing.Point(1296, 494)
        Me.btnActorEdit.Name = "btnActorEdit"
        Me.btnActorEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnActorEdit.TabIndex = 32
        Me.btnActorEdit.UseVisualStyleBackColor = True
        '
        'btnActorAdd
        '
        Me.btnActorAdd.Image = CType(resources.GetObject("btnActorAdd.Image"), System.Drawing.Image)
        Me.btnActorAdd.Location = New System.Drawing.Point(1267, 494)
        Me.btnActorAdd.Name = "btnActorAdd"
        Me.btnActorAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnActorAdd.TabIndex = 31
        Me.btnActorAdd.UseVisualStyleBackColor = True
        '
        'btnManual
        '
        Me.btnManual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnManual.Location = New System.Drawing.Point(1806, 264)
        Me.btnManual.Name = "btnManual"
        Me.btnManual.Size = New System.Drawing.Size(92, 23)
        Me.btnManual.TabIndex = 54
        Me.btnManual.Text = "Manual Edit"
        Me.btnManual.UseVisualStyleBackColor = True
        '
        'btnActorRemove
        '
        Me.btnActorRemove.Image = CType(resources.GetObject("btnActorRemove.Image"), System.Drawing.Image)
        Me.btnActorRemove.Location = New System.Drawing.Point(1652, 494)
        Me.btnActorRemove.Name = "btnActorRemove"
        Me.btnActorRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnActorRemove.TabIndex = 35
        Me.btnActorRemove.UseVisualStyleBackColor = True
        '
        'lblActors
        '
        Me.lblActors.AutoSize = True
        Me.lblActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblActors.Location = New System.Drawing.Point(1265, 332)
        Me.lblActors.Name = "lblActors"
        Me.lblActors.Size = New System.Drawing.Size(42, 13)
        Me.lblActors.TabIndex = 29
        Me.lblActors.Text = "Actors:"
        '
        'lvActors
        '
        Me.lvActors.BackColor = System.Drawing.SystemColors.Window
        Me.lvActors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvActors.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colID, Me.colName, Me.colRole, Me.colThumb})
        Me.lvActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvActors.FullRowSelect = True
        Me.lvActors.Location = New System.Drawing.Point(1267, 346)
        Me.lvActors.Name = "lvActors"
        Me.lvActors.Size = New System.Drawing.Size(408, 147)
        Me.lvActors.TabIndex = 30
        Me.lvActors.UseCompatibleStateImageBehavior = False
        Me.lvActors.View = System.Windows.Forms.View.Details
        '
        'colID
        '
        Me.colID.Text = "ID"
        Me.colID.Width = 0
        '
        'colName
        '
        Me.colName.Text = "Name"
        Me.colName.Width = 110
        '
        'colRole
        '
        Me.colRole.Text = "Role"
        Me.colRole.Width = 100
        '
        'colThumb
        '
        Me.colThumb.Text = "Thumb"
        Me.colThumb.Width = 174
        '
        'lbMPAA
        '
        Me.lbMPAA.BackColor = System.Drawing.SystemColors.Window
        Me.lbMPAA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbMPAA.FormattingEnabled = True
        Me.lbMPAA.Location = New System.Drawing.Point(1685, 346)
        Me.lbMPAA.Name = "lbMPAA"
        Me.lbMPAA.Size = New System.Drawing.Size(213, 106)
        Me.lbMPAA.TabIndex = 37
        '
        'lblGenres
        '
        Me.lblGenres.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGenres.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblGenres, 2)
        Me.lblGenres.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblGenres.Location = New System.Drawing.Point(3, 257)
        Me.lblGenres.Name = "lblGenres"
        Me.lblGenres.Size = New System.Drawing.Size(46, 13)
        Me.lblGenres.TabIndex = 23
        Me.lblGenres.Text = "Genres:"
        '
        'lblMPAA
        '
        Me.lblMPAA.AutoSize = True
        Me.lblMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMPAA.Location = New System.Drawing.Point(1683, 332)
        Me.lblMPAA.Name = "lblMPAA"
        Me.lblMPAA.Size = New System.Drawing.Size(76, 13)
        Me.lblMPAA.TabIndex = 36
        Me.lblMPAA.Text = "MPAA Rating:"
        '
        'lblDirectors
        '
        Me.lblDirectors.AutoSize = True
        Me.lblDirectors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblDirectors.Location = New System.Drawing.Point(229, 426)
        Me.lblDirectors.Name = "lblDirectors"
        Me.lblDirectors.Size = New System.Drawing.Size(56, 13)
        Me.lblDirectors.TabIndex = 21
        Me.lblDirectors.Text = "Directors:"
        '
        'txtUserRating
        '
        Me.txtUserRating.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtUserRating.Location = New System.Drawing.Point(1762, 178)
        Me.txtUserRating.Name = "txtUserRating"
        Me.txtUserRating.Size = New System.Drawing.Size(45, 22)
        Me.txtUserRating.TabIndex = 20
        '
        'txtTop250
        '
        Me.txtTop250.BackColor = System.Drawing.SystemColors.Window
        Me.txtTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTop250.Location = New System.Drawing.Point(1660, 151)
        Me.txtTop250.Name = "txtTop250"
        Me.txtTop250.Size = New System.Drawing.Size(45, 22)
        Me.txtTop250.TabIndex = 20
        '
        'lblUserRating
        '
        Me.lblUserRating.AutoSize = True
        Me.lblUserRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblUserRating.Location = New System.Drawing.Point(1748, 163)
        Me.lblUserRating.Name = "lblUserRating"
        Me.lblUserRating.Size = New System.Drawing.Size(70, 13)
        Me.lblUserRating.TabIndex = 19
        Me.lblUserRating.Text = "User Rating:"
        '
        'lblTop250
        '
        Me.lblTop250.AutoSize = True
        Me.lblTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTop250.Location = New System.Drawing.Point(1657, 136)
        Me.lblTop250.Name = "lblTop250"
        Me.lblTop250.Size = New System.Drawing.Size(49, 13)
        Me.lblTop250.TabIndex = 19
        Me.lblTop250.Text = "Top 250:"
        '
        'lblPlot
        '
        Me.lblPlot.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblPlot, 3)
        Me.lblPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPlot.Location = New System.Drawing.Point(475, 41)
        Me.lblPlot.Name = "lblPlot"
        Me.lblPlot.Size = New System.Drawing.Size(30, 13)
        Me.lblPlot.TabIndex = 27
        Me.lblPlot.Text = "Plot:"
        '
        'txtPlot
        '
        Me.txtPlot.AcceptsReturn = True
        Me.txtPlot.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtPlot, 4)
        Me.txtPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtPlot.Location = New System.Drawing.Point(475, 57)
        Me.txtPlot.Multiline = True
        Me.txtPlot.Name = "txtPlot"
        Me.tblDetails.SetRowSpan(Me.txtPlot, 5)
        Me.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlot.Size = New System.Drawing.Size(392, 104)
        Me.txtPlot.TabIndex = 28
        '
        'txtOutline
        '
        Me.txtOutline.AcceptsReturn = True
        Me.txtOutline.BackColor = System.Drawing.SystemColors.Window
        Me.txtOutline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtOutline.Location = New System.Drawing.Point(229, 57)
        Me.txtOutline.Multiline = True
        Me.txtOutline.Name = "txtOutline"
        Me.tblDetails.SetRowSpan(Me.txtOutline, 5)
        Me.txtOutline.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOutline.Size = New System.Drawing.Size(240, 104)
        Me.txtOutline.TabIndex = 26
        '
        'lblRatings
        '
        Me.lblRatings.AutoSize = True
        Me.lblRatings.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRatings.Location = New System.Drawing.Point(1748, 136)
        Me.lblRatings.Name = "lblRatings"
        Me.lblRatings.Size = New System.Drawing.Size(49, 13)
        Me.lblRatings.TabIndex = 10
        Me.lblRatings.Text = "Ratings:"
        '
        'txtYear
        '
        Me.txtYear.BackColor = System.Drawing.SystemColors.Window
        Me.txtYear.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtYear.Location = New System.Drawing.Point(3, 139)
        Me.txtYear.Mask = "####"
        Me.txtYear.Name = "txtYear"
        Me.txtYear.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.txtYear.Size = New System.Drawing.Size(72, 22)
        Me.txtYear.TabIndex = 9
        '
        'tpPoster
        '
        Me.tpPoster.Controls.Add(Me.btnSetPosterDL)
        Me.tpPoster.Controls.Add(Me.btnRemovePoster)
        Me.tpPoster.Controls.Add(Me.lblPosterSize)
        Me.tpPoster.Controls.Add(Me.btnSetPosterScrape)
        Me.tpPoster.Controls.Add(Me.btnSetPosterLocal)
        Me.tpPoster.Controls.Add(Me.pbPoster)
        Me.tpPoster.Location = New System.Drawing.Point(4, 22)
        Me.tpPoster.Name = "tpPoster"
        Me.tpPoster.Padding = New System.Windows.Forms.Padding(3)
        Me.tpPoster.Size = New System.Drawing.Size(1923, 605)
        Me.tpPoster.TabIndex = 1
        Me.tpPoster.Text = "Poster"
        Me.tpPoster.UseVisualStyleBackColor = True
        '
        'btnSetPosterDL
        '
        Me.btnSetPosterDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetPosterDL.Image = CType(resources.GetObject("btnSetPosterDL.Image"), System.Drawing.Image)
        Me.btnSetPosterDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetPosterDL.Location = New System.Drawing.Point(810, 184)
        Me.btnSetPosterDL.Name = "btnSetPosterDL"
        Me.btnSetPosterDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetPosterDL.TabIndex = 3
        Me.btnSetPosterDL.Text = "Download"
        Me.btnSetPosterDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetPosterDL.UseVisualStyleBackColor = True
        '
        'btnRemovePoster
        '
        Me.btnRemovePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemovePoster.Image = CType(resources.GetObject("btnRemovePoster.Image"), System.Drawing.Image)
        Me.btnRemovePoster.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemovePoster.Location = New System.Drawing.Point(810, 373)
        Me.btnRemovePoster.Name = "btnRemovePoster"
        Me.btnRemovePoster.Size = New System.Drawing.Size(96, 83)
        Me.btnRemovePoster.TabIndex = 4
        Me.btnRemovePoster.Text = "Remove"
        Me.btnRemovePoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemovePoster.UseVisualStyleBackColor = True
        '
        'lblPosterSize
        '
        Me.lblPosterSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPosterSize.Location = New System.Drawing.Point(8, 8)
        Me.lblPosterSize.Name = "lblPosterSize"
        Me.lblPosterSize.Size = New System.Drawing.Size(105, 23)
        Me.lblPosterSize.TabIndex = 0
        Me.lblPosterSize.Text = "Size: (XXXXxXXXX)"
        Me.lblPosterSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPosterSize.Visible = False
        '
        'btnSetPosterScrape
        '
        Me.btnSetPosterScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetPosterScrape.Image = CType(resources.GetObject("btnSetPosterScrape.Image"), System.Drawing.Image)
        Me.btnSetPosterScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetPosterScrape.Location = New System.Drawing.Point(810, 95)
        Me.btnSetPosterScrape.Name = "btnSetPosterScrape"
        Me.btnSetPosterScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetPosterScrape.TabIndex = 2
        Me.btnSetPosterScrape.Text = "Scrape"
        Me.btnSetPosterScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetPosterScrape.UseVisualStyleBackColor = True
        '
        'btnSetPosterLocal
        '
        Me.btnSetPosterLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetPosterLocal.Image = CType(resources.GetObject("btnSetPosterLocal.Image"), System.Drawing.Image)
        Me.btnSetPosterLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetPosterLocal.Location = New System.Drawing.Point(810, 6)
        Me.btnSetPosterLocal.Name = "btnSetPosterLocal"
        Me.btnSetPosterLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetPosterLocal.TabIndex = 1
        Me.btnSetPosterLocal.Text = "Local Browse"
        Me.btnSetPosterLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetPosterLocal.UseVisualStyleBackColor = True
        '
        'pbPoster
        '
        Me.pbPoster.BackColor = System.Drawing.Color.DimGray
        Me.pbPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbPoster.Location = New System.Drawing.Point(6, 6)
        Me.pbPoster.Name = "pbPoster"
        Me.pbPoster.Size = New System.Drawing.Size(800, 450)
        Me.pbPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbPoster.TabIndex = 0
        Me.pbPoster.TabStop = False
        '
        'tpBanner
        '
        Me.tpBanner.Controls.Add(Me.btnSetBannerDL)
        Me.tpBanner.Controls.Add(Me.btnRemoveBanner)
        Me.tpBanner.Controls.Add(Me.lblBannerSize)
        Me.tpBanner.Controls.Add(Me.btnSetBannerScrape)
        Me.tpBanner.Controls.Add(Me.btnSetBannerLocal)
        Me.tpBanner.Controls.Add(Me.pbBanner)
        Me.tpBanner.Location = New System.Drawing.Point(4, 22)
        Me.tpBanner.Name = "tpBanner"
        Me.tpBanner.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBanner.Size = New System.Drawing.Size(1923, 605)
        Me.tpBanner.TabIndex = 8
        Me.tpBanner.Text = "Banner"
        Me.tpBanner.UseVisualStyleBackColor = True
        '
        'btnSetBannerDL
        '
        Me.btnSetBannerDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetBannerDL.Image = CType(resources.GetObject("btnSetBannerDL.Image"), System.Drawing.Image)
        Me.btnSetBannerDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetBannerDL.Location = New System.Drawing.Point(810, 184)
        Me.btnSetBannerDL.Name = "btnSetBannerDL"
        Me.btnSetBannerDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetBannerDL.TabIndex = 9
        Me.btnSetBannerDL.Text = "Download"
        Me.btnSetBannerDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetBannerDL.UseVisualStyleBackColor = True
        '
        'btnRemoveBanner
        '
        Me.btnRemoveBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveBanner.Image = CType(resources.GetObject("btnRemoveBanner.Image"), System.Drawing.Image)
        Me.btnRemoveBanner.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveBanner.Location = New System.Drawing.Point(810, 373)
        Me.btnRemoveBanner.Name = "btnRemoveBanner"
        Me.btnRemoveBanner.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveBanner.TabIndex = 10
        Me.btnRemoveBanner.Text = "Remove"
        Me.btnRemoveBanner.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveBanner.UseVisualStyleBackColor = True
        '
        'lblBannerSize
        '
        Me.lblBannerSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBannerSize.Location = New System.Drawing.Point(8, 8)
        Me.lblBannerSize.Name = "lblBannerSize"
        Me.lblBannerSize.Size = New System.Drawing.Size(105, 23)
        Me.lblBannerSize.TabIndex = 5
        Me.lblBannerSize.Text = "Size: (XXXXxXXXX)"
        Me.lblBannerSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblBannerSize.Visible = False
        '
        'btnSetBannerScrape
        '
        Me.btnSetBannerScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetBannerScrape.Image = CType(resources.GetObject("btnSetBannerScrape.Image"), System.Drawing.Image)
        Me.btnSetBannerScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetBannerScrape.Location = New System.Drawing.Point(810, 95)
        Me.btnSetBannerScrape.Name = "btnSetBannerScrape"
        Me.btnSetBannerScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetBannerScrape.TabIndex = 8
        Me.btnSetBannerScrape.Text = "Scrape"
        Me.btnSetBannerScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetBannerScrape.UseVisualStyleBackColor = True
        '
        'btnSetBannerLocal
        '
        Me.btnSetBannerLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetBannerLocal.Image = CType(resources.GetObject("btnSetBannerLocal.Image"), System.Drawing.Image)
        Me.btnSetBannerLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetBannerLocal.Location = New System.Drawing.Point(810, 6)
        Me.btnSetBannerLocal.Name = "btnSetBannerLocal"
        Me.btnSetBannerLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetBannerLocal.TabIndex = 7
        Me.btnSetBannerLocal.Text = "Local Browse"
        Me.btnSetBannerLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetBannerLocal.UseVisualStyleBackColor = True
        '
        'pbBanner
        '
        Me.pbBanner.BackColor = System.Drawing.Color.DimGray
        Me.pbBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbBanner.Location = New System.Drawing.Point(6, 6)
        Me.pbBanner.Name = "pbBanner"
        Me.pbBanner.Size = New System.Drawing.Size(800, 450)
        Me.pbBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbBanner.TabIndex = 6
        Me.pbBanner.TabStop = False
        '
        'tpLandscape
        '
        Me.tpLandscape.Controls.Add(Me.btnSetLandscapeDL)
        Me.tpLandscape.Controls.Add(Me.btnRemoveLandscape)
        Me.tpLandscape.Controls.Add(Me.lblLandscapeSize)
        Me.tpLandscape.Controls.Add(Me.btnSetLandscapeScrape)
        Me.tpLandscape.Controls.Add(Me.btnSetLandscapeLocal)
        Me.tpLandscape.Controls.Add(Me.pbLandscape)
        Me.tpLandscape.Location = New System.Drawing.Point(4, 22)
        Me.tpLandscape.Name = "tpLandscape"
        Me.tpLandscape.Size = New System.Drawing.Size(1923, 605)
        Me.tpLandscape.TabIndex = 9
        Me.tpLandscape.Text = "Landscape"
        Me.tpLandscape.UseVisualStyleBackColor = True
        '
        'btnSetLandscapeDL
        '
        Me.btnSetLandscapeDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetLandscapeDL.Image = CType(resources.GetObject("btnSetLandscapeDL.Image"), System.Drawing.Image)
        Me.btnSetLandscapeDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetLandscapeDL.Location = New System.Drawing.Point(810, 184)
        Me.btnSetLandscapeDL.Name = "btnSetLandscapeDL"
        Me.btnSetLandscapeDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetLandscapeDL.TabIndex = 9
        Me.btnSetLandscapeDL.Text = "Download"
        Me.btnSetLandscapeDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetLandscapeDL.UseVisualStyleBackColor = True
        '
        'btnRemoveLandscape
        '
        Me.btnRemoveLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveLandscape.Image = CType(resources.GetObject("btnRemoveLandscape.Image"), System.Drawing.Image)
        Me.btnRemoveLandscape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveLandscape.Location = New System.Drawing.Point(810, 373)
        Me.btnRemoveLandscape.Name = "btnRemoveLandscape"
        Me.btnRemoveLandscape.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveLandscape.TabIndex = 10
        Me.btnRemoveLandscape.Text = "Remove"
        Me.btnRemoveLandscape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveLandscape.UseVisualStyleBackColor = True
        '
        'lblLandscapeSize
        '
        Me.lblLandscapeSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLandscapeSize.Location = New System.Drawing.Point(8, 8)
        Me.lblLandscapeSize.Name = "lblLandscapeSize"
        Me.lblLandscapeSize.Size = New System.Drawing.Size(105, 23)
        Me.lblLandscapeSize.TabIndex = 5
        Me.lblLandscapeSize.Text = "Size: (XXXXxXXXX)"
        Me.lblLandscapeSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblLandscapeSize.Visible = False
        '
        'btnSetLandscapeScrape
        '
        Me.btnSetLandscapeScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetLandscapeScrape.Image = CType(resources.GetObject("btnSetLandscapeScrape.Image"), System.Drawing.Image)
        Me.btnSetLandscapeScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetLandscapeScrape.Location = New System.Drawing.Point(810, 95)
        Me.btnSetLandscapeScrape.Name = "btnSetLandscapeScrape"
        Me.btnSetLandscapeScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetLandscapeScrape.TabIndex = 8
        Me.btnSetLandscapeScrape.Text = "Scrape"
        Me.btnSetLandscapeScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetLandscapeScrape.UseVisualStyleBackColor = True
        '
        'btnSetLandscapeLocal
        '
        Me.btnSetLandscapeLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetLandscapeLocal.Image = CType(resources.GetObject("btnSetLandscapeLocal.Image"), System.Drawing.Image)
        Me.btnSetLandscapeLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetLandscapeLocal.Location = New System.Drawing.Point(810, 6)
        Me.btnSetLandscapeLocal.Name = "btnSetLandscapeLocal"
        Me.btnSetLandscapeLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetLandscapeLocal.TabIndex = 7
        Me.btnSetLandscapeLocal.Text = "Local Browse"
        Me.btnSetLandscapeLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetLandscapeLocal.UseVisualStyleBackColor = True
        '
        'pbLandscape
        '
        Me.pbLandscape.BackColor = System.Drawing.Color.DimGray
        Me.pbLandscape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbLandscape.Location = New System.Drawing.Point(6, 6)
        Me.pbLandscape.Name = "pbLandscape"
        Me.pbLandscape.Size = New System.Drawing.Size(800, 450)
        Me.pbLandscape.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbLandscape.TabIndex = 6
        Me.pbLandscape.TabStop = False
        '
        'tpClearArt
        '
        Me.tpClearArt.Controls.Add(Me.btnSetClearArtDL)
        Me.tpClearArt.Controls.Add(Me.btnRemoveClearArt)
        Me.tpClearArt.Controls.Add(Me.lblClearArtSize)
        Me.tpClearArt.Controls.Add(Me.btnSetClearArtScrape)
        Me.tpClearArt.Controls.Add(Me.btnSetClearArtLocal)
        Me.tpClearArt.Controls.Add(Me.pbClearArt)
        Me.tpClearArt.Location = New System.Drawing.Point(4, 22)
        Me.tpClearArt.Name = "tpClearArt"
        Me.tpClearArt.Size = New System.Drawing.Size(1923, 605)
        Me.tpClearArt.TabIndex = 11
        Me.tpClearArt.Text = "ClearArt"
        Me.tpClearArt.UseVisualStyleBackColor = True
        '
        'btnSetClearArtDL
        '
        Me.btnSetClearArtDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearArtDL.Image = CType(resources.GetObject("btnSetClearArtDL.Image"), System.Drawing.Image)
        Me.btnSetClearArtDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetClearArtDL.Location = New System.Drawing.Point(810, 184)
        Me.btnSetClearArtDL.Name = "btnSetClearArtDL"
        Me.btnSetClearArtDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetClearArtDL.TabIndex = 9
        Me.btnSetClearArtDL.Text = "Download"
        Me.btnSetClearArtDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearArtDL.UseVisualStyleBackColor = True
        '
        'btnRemoveClearArt
        '
        Me.btnRemoveClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveClearArt.Image = CType(resources.GetObject("btnRemoveClearArt.Image"), System.Drawing.Image)
        Me.btnRemoveClearArt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveClearArt.Location = New System.Drawing.Point(810, 373)
        Me.btnRemoveClearArt.Name = "btnRemoveClearArt"
        Me.btnRemoveClearArt.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveClearArt.TabIndex = 10
        Me.btnRemoveClearArt.Text = "Remove"
        Me.btnRemoveClearArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveClearArt.UseVisualStyleBackColor = True
        '
        'lblClearArtSize
        '
        Me.lblClearArtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClearArtSize.Location = New System.Drawing.Point(8, 8)
        Me.lblClearArtSize.Name = "lblClearArtSize"
        Me.lblClearArtSize.Size = New System.Drawing.Size(105, 23)
        Me.lblClearArtSize.TabIndex = 5
        Me.lblClearArtSize.Text = "Size: (XXXXxXXXX)"
        Me.lblClearArtSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblClearArtSize.Visible = False
        '
        'btnSetClearArtScrape
        '
        Me.btnSetClearArtScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearArtScrape.Image = CType(resources.GetObject("btnSetClearArtScrape.Image"), System.Drawing.Image)
        Me.btnSetClearArtScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetClearArtScrape.Location = New System.Drawing.Point(810, 95)
        Me.btnSetClearArtScrape.Name = "btnSetClearArtScrape"
        Me.btnSetClearArtScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetClearArtScrape.TabIndex = 8
        Me.btnSetClearArtScrape.Text = "Scrape"
        Me.btnSetClearArtScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearArtScrape.UseVisualStyleBackColor = True
        '
        'btnSetClearArtLocal
        '
        Me.btnSetClearArtLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearArtLocal.Image = CType(resources.GetObject("btnSetClearArtLocal.Image"), System.Drawing.Image)
        Me.btnSetClearArtLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetClearArtLocal.Location = New System.Drawing.Point(810, 6)
        Me.btnSetClearArtLocal.Name = "btnSetClearArtLocal"
        Me.btnSetClearArtLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetClearArtLocal.TabIndex = 7
        Me.btnSetClearArtLocal.Text = "Local Browse"
        Me.btnSetClearArtLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearArtLocal.UseVisualStyleBackColor = True
        '
        'pbClearArt
        '
        Me.pbClearArt.BackColor = System.Drawing.Color.DimGray
        Me.pbClearArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbClearArt.Location = New System.Drawing.Point(6, 6)
        Me.pbClearArt.Name = "pbClearArt"
        Me.pbClearArt.Size = New System.Drawing.Size(800, 450)
        Me.pbClearArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbClearArt.TabIndex = 6
        Me.pbClearArt.TabStop = False
        '
        'tpClearLogo
        '
        Me.tpClearLogo.Controls.Add(Me.btnSetClearLogoDL)
        Me.tpClearLogo.Controls.Add(Me.btnRemoveClearLogo)
        Me.tpClearLogo.Controls.Add(Me.lblClearLogoSize)
        Me.tpClearLogo.Controls.Add(Me.btnSetClearLogoScrape)
        Me.tpClearLogo.Controls.Add(Me.btnSetClearLogoLocal)
        Me.tpClearLogo.Controls.Add(Me.pbClearLogo)
        Me.tpClearLogo.Location = New System.Drawing.Point(4, 22)
        Me.tpClearLogo.Name = "tpClearLogo"
        Me.tpClearLogo.Size = New System.Drawing.Size(1923, 605)
        Me.tpClearLogo.TabIndex = 12
        Me.tpClearLogo.Text = "ClearLogo"
        Me.tpClearLogo.UseVisualStyleBackColor = True
        '
        'btnSetClearLogoDL
        '
        Me.btnSetClearLogoDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearLogoDL.Image = CType(resources.GetObject("btnSetClearLogoDL.Image"), System.Drawing.Image)
        Me.btnSetClearLogoDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetClearLogoDL.Location = New System.Drawing.Point(810, 184)
        Me.btnSetClearLogoDL.Name = "btnSetClearLogoDL"
        Me.btnSetClearLogoDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetClearLogoDL.TabIndex = 9
        Me.btnSetClearLogoDL.Text = "Download"
        Me.btnSetClearLogoDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearLogoDL.UseVisualStyleBackColor = True
        '
        'btnRemoveClearLogo
        '
        Me.btnRemoveClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveClearLogo.Image = CType(resources.GetObject("btnRemoveClearLogo.Image"), System.Drawing.Image)
        Me.btnRemoveClearLogo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveClearLogo.Location = New System.Drawing.Point(810, 373)
        Me.btnRemoveClearLogo.Name = "btnRemoveClearLogo"
        Me.btnRemoveClearLogo.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveClearLogo.TabIndex = 10
        Me.btnRemoveClearLogo.Text = "Remove"
        Me.btnRemoveClearLogo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveClearLogo.UseVisualStyleBackColor = True
        '
        'lblClearLogoSize
        '
        Me.lblClearLogoSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClearLogoSize.Location = New System.Drawing.Point(8, 8)
        Me.lblClearLogoSize.Name = "lblClearLogoSize"
        Me.lblClearLogoSize.Size = New System.Drawing.Size(105, 23)
        Me.lblClearLogoSize.TabIndex = 5
        Me.lblClearLogoSize.Text = "Size: (XXXXxXXXX)"
        Me.lblClearLogoSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblClearLogoSize.Visible = False
        '
        'btnSetClearLogoScrape
        '
        Me.btnSetClearLogoScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearLogoScrape.Image = CType(resources.GetObject("btnSetClearLogoScrape.Image"), System.Drawing.Image)
        Me.btnSetClearLogoScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetClearLogoScrape.Location = New System.Drawing.Point(810, 95)
        Me.btnSetClearLogoScrape.Name = "btnSetClearLogoScrape"
        Me.btnSetClearLogoScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetClearLogoScrape.TabIndex = 8
        Me.btnSetClearLogoScrape.Text = "Scrape"
        Me.btnSetClearLogoScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearLogoScrape.UseVisualStyleBackColor = True
        '
        'btnSetClearLogoLocal
        '
        Me.btnSetClearLogoLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearLogoLocal.Image = CType(resources.GetObject("btnSetClearLogoLocal.Image"), System.Drawing.Image)
        Me.btnSetClearLogoLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetClearLogoLocal.Location = New System.Drawing.Point(810, 6)
        Me.btnSetClearLogoLocal.Name = "btnSetClearLogoLocal"
        Me.btnSetClearLogoLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetClearLogoLocal.TabIndex = 7
        Me.btnSetClearLogoLocal.Text = "Local Browse"
        Me.btnSetClearLogoLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearLogoLocal.UseVisualStyleBackColor = True
        '
        'pbClearLogo
        '
        Me.pbClearLogo.BackColor = System.Drawing.Color.DimGray
        Me.pbClearLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbClearLogo.Location = New System.Drawing.Point(6, 6)
        Me.pbClearLogo.Name = "pbClearLogo"
        Me.pbClearLogo.Size = New System.Drawing.Size(800, 450)
        Me.pbClearLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbClearLogo.TabIndex = 6
        Me.pbClearLogo.TabStop = False
        '
        'tpDiscArt
        '
        Me.tpDiscArt.Controls.Add(Me.btnSetDiscArtDL)
        Me.tpDiscArt.Controls.Add(Me.btnRemoveDiscArt)
        Me.tpDiscArt.Controls.Add(Me.lblDiscArtSize)
        Me.tpDiscArt.Controls.Add(Me.btnSetDiscArtScrape)
        Me.tpDiscArt.Controls.Add(Me.btnSetDiscArtLocal)
        Me.tpDiscArt.Controls.Add(Me.pbDiscArt)
        Me.tpDiscArt.Location = New System.Drawing.Point(4, 22)
        Me.tpDiscArt.Name = "tpDiscArt"
        Me.tpDiscArt.Size = New System.Drawing.Size(1923, 605)
        Me.tpDiscArt.TabIndex = 10
        Me.tpDiscArt.Text = "DiscArt"
        Me.tpDiscArt.UseVisualStyleBackColor = True
        '
        'btnSetDiscArtDL
        '
        Me.btnSetDiscArtDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetDiscArtDL.Image = CType(resources.GetObject("btnSetDiscArtDL.Image"), System.Drawing.Image)
        Me.btnSetDiscArtDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetDiscArtDL.Location = New System.Drawing.Point(810, 184)
        Me.btnSetDiscArtDL.Name = "btnSetDiscArtDL"
        Me.btnSetDiscArtDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetDiscArtDL.TabIndex = 9
        Me.btnSetDiscArtDL.Text = "Download"
        Me.btnSetDiscArtDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetDiscArtDL.UseVisualStyleBackColor = True
        '
        'btnRemoveDiscArt
        '
        Me.btnRemoveDiscArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveDiscArt.Image = CType(resources.GetObject("btnRemoveDiscArt.Image"), System.Drawing.Image)
        Me.btnRemoveDiscArt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveDiscArt.Location = New System.Drawing.Point(810, 373)
        Me.btnRemoveDiscArt.Name = "btnRemoveDiscArt"
        Me.btnRemoveDiscArt.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveDiscArt.TabIndex = 10
        Me.btnRemoveDiscArt.Text = "Remove"
        Me.btnRemoveDiscArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveDiscArt.UseVisualStyleBackColor = True
        '
        'lblDiscArtSize
        '
        Me.lblDiscArtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDiscArtSize.Location = New System.Drawing.Point(8, 8)
        Me.lblDiscArtSize.Name = "lblDiscArtSize"
        Me.lblDiscArtSize.Size = New System.Drawing.Size(105, 23)
        Me.lblDiscArtSize.TabIndex = 5
        Me.lblDiscArtSize.Text = "Size: (XXXXxXXXX)"
        Me.lblDiscArtSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblDiscArtSize.Visible = False
        '
        'btnSetDiscArtScrape
        '
        Me.btnSetDiscArtScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetDiscArtScrape.Image = CType(resources.GetObject("btnSetDiscArtScrape.Image"), System.Drawing.Image)
        Me.btnSetDiscArtScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetDiscArtScrape.Location = New System.Drawing.Point(810, 95)
        Me.btnSetDiscArtScrape.Name = "btnSetDiscArtScrape"
        Me.btnSetDiscArtScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetDiscArtScrape.TabIndex = 8
        Me.btnSetDiscArtScrape.Text = "Scrape"
        Me.btnSetDiscArtScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetDiscArtScrape.UseVisualStyleBackColor = True
        '
        'btnSetDiscArtLocal
        '
        Me.btnSetDiscArtLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetDiscArtLocal.Image = CType(resources.GetObject("btnSetDiscArtLocal.Image"), System.Drawing.Image)
        Me.btnSetDiscArtLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetDiscArtLocal.Location = New System.Drawing.Point(810, 6)
        Me.btnSetDiscArtLocal.Name = "btnSetDiscArtLocal"
        Me.btnSetDiscArtLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetDiscArtLocal.TabIndex = 7
        Me.btnSetDiscArtLocal.Text = "Local Browse"
        Me.btnSetDiscArtLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetDiscArtLocal.UseVisualStyleBackColor = True
        '
        'pbDiscArt
        '
        Me.pbDiscArt.BackColor = System.Drawing.Color.DimGray
        Me.pbDiscArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbDiscArt.Location = New System.Drawing.Point(6, 6)
        Me.pbDiscArt.Name = "pbDiscArt"
        Me.pbDiscArt.Size = New System.Drawing.Size(800, 450)
        Me.pbDiscArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbDiscArt.TabIndex = 6
        Me.pbDiscArt.TabStop = False
        '
        'tpFanart
        '
        Me.tpFanart.Controls.Add(Me.btnSetFanartDL)
        Me.tpFanart.Controls.Add(Me.btnRemoveFanart)
        Me.tpFanart.Controls.Add(Me.lblFanartSize)
        Me.tpFanart.Controls.Add(Me.btnSetFanartScrape)
        Me.tpFanart.Controls.Add(Me.btnSetFanartLocal)
        Me.tpFanart.Controls.Add(Me.pbFanart)
        Me.tpFanart.Location = New System.Drawing.Point(4, 22)
        Me.tpFanart.Name = "tpFanart"
        Me.tpFanart.Size = New System.Drawing.Size(1923, 605)
        Me.tpFanart.TabIndex = 2
        Me.tpFanart.Text = "Fanart"
        Me.tpFanart.UseVisualStyleBackColor = True
        '
        'btnSetFanartDL
        '
        Me.btnSetFanartDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetFanartDL.Image = CType(resources.GetObject("btnSetFanartDL.Image"), System.Drawing.Image)
        Me.btnSetFanartDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetFanartDL.Location = New System.Drawing.Point(810, 184)
        Me.btnSetFanartDL.Name = "btnSetFanartDL"
        Me.btnSetFanartDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetFanartDL.TabIndex = 3
        Me.btnSetFanartDL.Text = "Download"
        Me.btnSetFanartDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetFanartDL.UseVisualStyleBackColor = True
        '
        'btnRemoveFanart
        '
        Me.btnRemoveFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveFanart.Image = CType(resources.GetObject("btnRemoveFanart.Image"), System.Drawing.Image)
        Me.btnRemoveFanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveFanart.Location = New System.Drawing.Point(810, 373)
        Me.btnRemoveFanart.Name = "btnRemoveFanart"
        Me.btnRemoveFanart.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveFanart.TabIndex = 4
        Me.btnRemoveFanart.Text = "Remove"
        Me.btnRemoveFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveFanart.UseVisualStyleBackColor = True
        '
        'lblFanartSize
        '
        Me.lblFanartSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFanartSize.Location = New System.Drawing.Point(8, 8)
        Me.lblFanartSize.Name = "lblFanartSize"
        Me.lblFanartSize.Size = New System.Drawing.Size(105, 23)
        Me.lblFanartSize.TabIndex = 0
        Me.lblFanartSize.Text = "Size: (XXXXxXXXX)"
        Me.lblFanartSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblFanartSize.Visible = False
        '
        'btnSetFanartScrape
        '
        Me.btnSetFanartScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetFanartScrape.Image = CType(resources.GetObject("btnSetFanartScrape.Image"), System.Drawing.Image)
        Me.btnSetFanartScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetFanartScrape.Location = New System.Drawing.Point(810, 95)
        Me.btnSetFanartScrape.Name = "btnSetFanartScrape"
        Me.btnSetFanartScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetFanartScrape.TabIndex = 2
        Me.btnSetFanartScrape.Text = "Scrape"
        Me.btnSetFanartScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetFanartScrape.UseVisualStyleBackColor = True
        '
        'btnSetFanartLocal
        '
        Me.btnSetFanartLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetFanartLocal.Image = CType(resources.GetObject("btnSetFanartLocal.Image"), System.Drawing.Image)
        Me.btnSetFanartLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetFanartLocal.Location = New System.Drawing.Point(810, 6)
        Me.btnSetFanartLocal.Name = "btnSetFanartLocal"
        Me.btnSetFanartLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetFanartLocal.TabIndex = 1
        Me.btnSetFanartLocal.Text = "Local Browse"
        Me.btnSetFanartLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetFanartLocal.UseVisualStyleBackColor = True
        '
        'pbFanart
        '
        Me.pbFanart.BackColor = System.Drawing.Color.DimGray
        Me.pbFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbFanart.Location = New System.Drawing.Point(6, 6)
        Me.pbFanart.Name = "pbFanart"
        Me.pbFanart.Size = New System.Drawing.Size(800, 450)
        Me.pbFanart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbFanart.TabIndex = 1
        Me.pbFanart.TabStop = False
        '
        'tpExtrafanarts
        '
        Me.tpExtrafanarts.Controls.Add(Me.btnSetExtrafanartsScrape)
        Me.tpExtrafanarts.Controls.Add(Me.lblExtrafanartsSize)
        Me.tpExtrafanarts.Controls.Add(Me.pnlExtrafanarts)
        Me.tpExtrafanarts.Controls.Add(Me.pnlExtrafanartsSetAsFanart)
        Me.tpExtrafanarts.Controls.Add(Me.btnExtrafanartsRefresh)
        Me.tpExtrafanarts.Controls.Add(Me.btnExtrafanartsRemove)
        Me.tpExtrafanarts.Controls.Add(Me.pbExtrafanarts)
        Me.tpExtrafanarts.Location = New System.Drawing.Point(4, 22)
        Me.tpExtrafanarts.Name = "tpExtrafanarts"
        Me.tpExtrafanarts.Padding = New System.Windows.Forms.Padding(3)
        Me.tpExtrafanarts.Size = New System.Drawing.Size(1923, 605)
        Me.tpExtrafanarts.TabIndex = 6
        Me.tpExtrafanarts.Text = "Extrafanarts"
        Me.tpExtrafanarts.UseVisualStyleBackColor = True
        '
        'btnSetExtrafanartsScrape
        '
        Me.btnSetExtrafanartsScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetExtrafanartsScrape.Image = CType(resources.GetObject("btnSetExtrafanartsScrape.Image"), System.Drawing.Image)
        Me.btnSetExtrafanartsScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetExtrafanartsScrape.Location = New System.Drawing.Point(882, 95)
        Me.btnSetExtrafanartsScrape.Name = "btnSetExtrafanartsScrape"
        Me.btnSetExtrafanartsScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetExtrafanartsScrape.TabIndex = 17
        Me.btnSetExtrafanartsScrape.Text = "Scrape"
        Me.btnSetExtrafanartsScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetExtrafanartsScrape.UseVisualStyleBackColor = True
        '
        'lblExtrafanartsSize
        '
        Me.lblExtrafanartsSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblExtrafanartsSize.Location = New System.Drawing.Point(178, 10)
        Me.lblExtrafanartsSize.Name = "lblExtrafanartsSize"
        Me.lblExtrafanartsSize.Size = New System.Drawing.Size(105, 23)
        Me.lblExtrafanartsSize.TabIndex = 16
        Me.lblExtrafanartsSize.Text = "Size: (XXXXxXXXX)"
        Me.lblExtrafanartsSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblExtrafanartsSize.Visible = False
        '
        'pnlExtrafanarts
        '
        Me.pnlExtrafanarts.AutoScroll = True
        Me.pnlExtrafanarts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlExtrafanarts.Location = New System.Drawing.Point(5, 8)
        Me.pnlExtrafanarts.Name = "pnlExtrafanarts"
        Me.pnlExtrafanarts.Size = New System.Drawing.Size(165, 394)
        Me.pnlExtrafanarts.TabIndex = 15
        '
        'pnlExtrafanartsSetAsFanart
        '
        Me.pnlExtrafanartsSetAsFanart.BackColor = System.Drawing.Color.LightGray
        Me.pnlExtrafanartsSetAsFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlExtrafanartsSetAsFanart.Controls.Add(Me.btnExtrafanartsSetAsFanart)
        Me.pnlExtrafanartsSetAsFanart.Location = New System.Drawing.Point(767, 363)
        Me.pnlExtrafanartsSetAsFanart.Name = "pnlExtrafanartsSetAsFanart"
        Me.pnlExtrafanartsSetAsFanart.Size = New System.Drawing.Size(109, 39)
        Me.pnlExtrafanartsSetAsFanart.TabIndex = 14
        '
        'btnExtrafanartsSetAsFanart
        '
        Me.btnExtrafanartsSetAsFanart.Enabled = False
        Me.btnExtrafanartsSetAsFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnExtrafanartsSetAsFanart.Image = CType(resources.GetObject("btnExtrafanartsSetAsFanart.Image"), System.Drawing.Image)
        Me.btnExtrafanartsSetAsFanart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExtrafanartsSetAsFanart.Location = New System.Drawing.Point(2, 3)
        Me.btnExtrafanartsSetAsFanart.Name = "btnExtrafanartsSetAsFanart"
        Me.btnExtrafanartsSetAsFanart.Size = New System.Drawing.Size(103, 32)
        Me.btnExtrafanartsSetAsFanart.TabIndex = 0
        Me.btnExtrafanartsSetAsFanart.Text = "Set As Fanart"
        Me.btnExtrafanartsSetAsFanart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExtrafanartsSetAsFanart.UseVisualStyleBackColor = True
        '
        'btnExtrafanartsRefresh
        '
        Me.btnExtrafanartsRefresh.Image = CType(resources.GetObject("btnExtrafanartsRefresh.Image"), System.Drawing.Image)
        Me.btnExtrafanartsRefresh.Location = New System.Drawing.Point(5, 408)
        Me.btnExtrafanartsRefresh.Name = "btnExtrafanartsRefresh"
        Me.btnExtrafanartsRefresh.Size = New System.Drawing.Size(23, 23)
        Me.btnExtrafanartsRefresh.TabIndex = 12
        Me.btnExtrafanartsRefresh.UseVisualStyleBackColor = True
        '
        'btnExtrafanartsRemove
        '
        Me.btnExtrafanartsRemove.Image = CType(resources.GetObject("btnExtrafanartsRemove.Image"), System.Drawing.Image)
        Me.btnExtrafanartsRemove.Location = New System.Drawing.Point(147, 408)
        Me.btnExtrafanartsRemove.Name = "btnExtrafanartsRemove"
        Me.btnExtrafanartsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnExtrafanartsRemove.TabIndex = 13
        Me.btnExtrafanartsRemove.UseVisualStyleBackColor = True
        '
        'pbExtrafanarts
        '
        Me.pbExtrafanarts.BackColor = System.Drawing.Color.DimGray
        Me.pbExtrafanarts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbExtrafanarts.Location = New System.Drawing.Point(176, 8)
        Me.pbExtrafanarts.Name = "pbExtrafanarts"
        Me.pbExtrafanarts.Size = New System.Drawing.Size(700, 394)
        Me.pbExtrafanarts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbExtrafanarts.TabIndex = 10
        Me.pbExtrafanarts.TabStop = False
        '
        'tpExtrathumbs
        '
        Me.tpExtrathumbs.Controls.Add(Me.btnSetExtrathumbsScrape)
        Me.tpExtrathumbs.Controls.Add(Me.lblExtrathumbsSize)
        Me.tpExtrathumbs.Controls.Add(Me.pnlExtrathumbs)
        Me.tpExtrathumbs.Controls.Add(Me.pnlExtrathumbsSetAsFanart)
        Me.tpExtrathumbs.Controls.Add(Me.btnExtrathumbsRefresh)
        Me.tpExtrathumbs.Controls.Add(Me.btnExtrathumbsRemove)
        Me.tpExtrathumbs.Controls.Add(Me.btnExtrathumbsDown)
        Me.tpExtrathumbs.Controls.Add(Me.btnExtrathumbsUp)
        Me.tpExtrathumbs.Controls.Add(Me.pbExtrathumbs)
        Me.tpExtrathumbs.Location = New System.Drawing.Point(4, 22)
        Me.tpExtrathumbs.Name = "tpExtrathumbs"
        Me.tpExtrathumbs.Size = New System.Drawing.Size(1923, 605)
        Me.tpExtrathumbs.TabIndex = 4
        Me.tpExtrathumbs.Text = "Extrathumbs"
        Me.tpExtrathumbs.UseVisualStyleBackColor = True
        '
        'btnSetExtrathumbsScrape
        '
        Me.btnSetExtrathumbsScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetExtrathumbsScrape.Image = CType(resources.GetObject("btnSetExtrathumbsScrape.Image"), System.Drawing.Image)
        Me.btnSetExtrathumbsScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetExtrathumbsScrape.Location = New System.Drawing.Point(882, 95)
        Me.btnSetExtrathumbsScrape.Name = "btnSetExtrathumbsScrape"
        Me.btnSetExtrathumbsScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetExtrathumbsScrape.TabIndex = 18
        Me.btnSetExtrathumbsScrape.Text = "Scrape"
        Me.btnSetExtrathumbsScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetExtrathumbsScrape.UseVisualStyleBackColor = True
        '
        'lblExtrathumbsSize
        '
        Me.lblExtrathumbsSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblExtrathumbsSize.Location = New System.Drawing.Point(178, 10)
        Me.lblExtrathumbsSize.Name = "lblExtrathumbsSize"
        Me.lblExtrathumbsSize.Size = New System.Drawing.Size(105, 23)
        Me.lblExtrathumbsSize.TabIndex = 17
        Me.lblExtrathumbsSize.Text = "Size: (XXXXxXXXX)"
        Me.lblExtrathumbsSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblExtrathumbsSize.Visible = False
        '
        'pnlExtrathumbs
        '
        Me.pnlExtrathumbs.AutoScroll = True
        Me.pnlExtrathumbs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlExtrathumbs.Location = New System.Drawing.Point(5, 8)
        Me.pnlExtrathumbs.Name = "pnlExtrathumbs"
        Me.pnlExtrathumbs.Size = New System.Drawing.Size(165, 394)
        Me.pnlExtrathumbs.TabIndex = 7
        '
        'pnlExtrathumbsSetAsFanart
        '
        Me.pnlExtrathumbsSetAsFanart.BackColor = System.Drawing.Color.LightGray
        Me.pnlExtrathumbsSetAsFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlExtrathumbsSetAsFanart.Controls.Add(Me.btnExtrathumbsSetAsFanart)
        Me.pnlExtrathumbsSetAsFanart.Location = New System.Drawing.Point(767, 363)
        Me.pnlExtrathumbsSetAsFanart.Name = "pnlExtrathumbsSetAsFanart"
        Me.pnlExtrathumbsSetAsFanart.Size = New System.Drawing.Size(109, 39)
        Me.pnlExtrathumbsSetAsFanart.TabIndex = 6
        '
        'btnExtrathumbsSetAsFanart
        '
        Me.btnExtrathumbsSetAsFanart.Enabled = False
        Me.btnExtrathumbsSetAsFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnExtrathumbsSetAsFanart.Image = CType(resources.GetObject("btnExtrathumbsSetAsFanart.Image"), System.Drawing.Image)
        Me.btnExtrathumbsSetAsFanart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExtrathumbsSetAsFanart.Location = New System.Drawing.Point(2, 3)
        Me.btnExtrathumbsSetAsFanart.Name = "btnExtrathumbsSetAsFanart"
        Me.btnExtrathumbsSetAsFanart.Size = New System.Drawing.Size(103, 32)
        Me.btnExtrathumbsSetAsFanart.TabIndex = 0
        Me.btnExtrathumbsSetAsFanart.Text = "Set As Fanart"
        Me.btnExtrathumbsSetAsFanart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExtrathumbsSetAsFanart.UseVisualStyleBackColor = True
        '
        'btnExtrathumbsRefresh
        '
        Me.btnExtrathumbsRefresh.Image = CType(resources.GetObject("btnExtrathumbsRefresh.Image"), System.Drawing.Image)
        Me.btnExtrathumbsRefresh.Location = New System.Drawing.Point(5, 408)
        Me.btnExtrathumbsRefresh.Name = "btnExtrathumbsRefresh"
        Me.btnExtrathumbsRefresh.Size = New System.Drawing.Size(23, 23)
        Me.btnExtrathumbsRefresh.TabIndex = 4
        Me.btnExtrathumbsRefresh.UseVisualStyleBackColor = True
        '
        'btnExtrathumbsRemove
        '
        Me.btnExtrathumbsRemove.Image = CType(resources.GetObject("btnExtrathumbsRemove.Image"), System.Drawing.Image)
        Me.btnExtrathumbsRemove.Location = New System.Drawing.Point(147, 408)
        Me.btnExtrathumbsRemove.Name = "btnExtrathumbsRemove"
        Me.btnExtrathumbsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnExtrathumbsRemove.TabIndex = 5
        Me.btnExtrathumbsRemove.UseVisualStyleBackColor = True
        '
        'btnExtrathumbsDown
        '
        Me.btnExtrathumbsDown.Enabled = False
        Me.btnExtrathumbsDown.Image = CType(resources.GetObject("btnExtrathumbsDown.Image"), System.Drawing.Image)
        Me.btnExtrathumbsDown.Location = New System.Drawing.Point(88, 408)
        Me.btnExtrathumbsDown.Name = "btnExtrathumbsDown"
        Me.btnExtrathumbsDown.Size = New System.Drawing.Size(23, 23)
        Me.btnExtrathumbsDown.TabIndex = 3
        Me.btnExtrathumbsDown.UseVisualStyleBackColor = True
        '
        'btnExtrathumbsUp
        '
        Me.btnExtrathumbsUp.Enabled = False
        Me.btnExtrathumbsUp.Image = CType(resources.GetObject("btnExtrathumbsUp.Image"), System.Drawing.Image)
        Me.btnExtrathumbsUp.Location = New System.Drawing.Point(59, 408)
        Me.btnExtrathumbsUp.Name = "btnExtrathumbsUp"
        Me.btnExtrathumbsUp.Size = New System.Drawing.Size(23, 23)
        Me.btnExtrathumbsUp.TabIndex = 2
        Me.btnExtrathumbsUp.UseVisualStyleBackColor = True
        '
        'pbExtrathumbs
        '
        Me.pbExtrathumbs.BackColor = System.Drawing.Color.DimGray
        Me.pbExtrathumbs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbExtrathumbs.Location = New System.Drawing.Point(176, 8)
        Me.pbExtrathumbs.Name = "pbExtrathumbs"
        Me.pbExtrathumbs.Size = New System.Drawing.Size(700, 394)
        Me.pbExtrathumbs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbExtrathumbs.TabIndex = 2
        Me.pbExtrathumbs.TabStop = False
        '
        'tpFrameExtraction
        '
        Me.tpFrameExtraction.Controls.Add(Me.btnFrameSaveAsExtrafanart)
        Me.tpFrameExtraction.Controls.Add(Me.btnFrameSaveAsFanart)
        Me.tpFrameExtraction.Controls.Add(Me.btnFrameSaveAsExtrathumb)
        Me.tpFrameExtraction.Controls.Add(Me.pnlFrameProgress)
        Me.tpFrameExtraction.Controls.Add(Me.lblTime)
        Me.tpFrameExtraction.Controls.Add(Me.tbFrame)
        Me.tpFrameExtraction.Controls.Add(Me.btnFrameLoad)
        Me.tpFrameExtraction.Controls.Add(Me.pbFrame)
        Me.tpFrameExtraction.Location = New System.Drawing.Point(4, 22)
        Me.tpFrameExtraction.Name = "tpFrameExtraction"
        Me.tpFrameExtraction.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFrameExtraction.Size = New System.Drawing.Size(1923, 605)
        Me.tpFrameExtraction.TabIndex = 3
        Me.tpFrameExtraction.Text = "Frame Extraction"
        Me.tpFrameExtraction.UseVisualStyleBackColor = True
        '
        'btnFrameSaveAsExtrafanart
        '
        Me.btnFrameSaveAsExtrafanart.Enabled = False
        Me.btnFrameSaveAsExtrafanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFrameSaveAsExtrafanart.Image = CType(resources.GetObject("btnFrameSaveAsExtrafanart.Image"), System.Drawing.Image)
        Me.btnFrameSaveAsExtrafanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFrameSaveAsExtrafanart.Location = New System.Drawing.Point(810, 273)
        Me.btnFrameSaveAsExtrafanart.Name = "btnFrameSaveAsExtrafanart"
        Me.btnFrameSaveAsExtrafanart.Size = New System.Drawing.Size(96, 83)
        Me.btnFrameSaveAsExtrafanart.TabIndex = 27
        Me.btnFrameSaveAsExtrafanart.Text = "Save as Extrafanart"
        Me.btnFrameSaveAsExtrafanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnFrameSaveAsExtrafanart.UseVisualStyleBackColor = True
        '
        'btnFrameSaveAsFanart
        '
        Me.btnFrameSaveAsFanart.Enabled = False
        Me.btnFrameSaveAsFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFrameSaveAsFanart.Image = CType(resources.GetObject("btnFrameSaveAsFanart.Image"), System.Drawing.Image)
        Me.btnFrameSaveAsFanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFrameSaveAsFanart.Location = New System.Drawing.Point(810, 184)
        Me.btnFrameSaveAsFanart.Name = "btnFrameSaveAsFanart"
        Me.btnFrameSaveAsFanart.Size = New System.Drawing.Size(96, 83)
        Me.btnFrameSaveAsFanart.TabIndex = 26
        Me.btnFrameSaveAsFanart.Text = "Save as Fanart"
        Me.btnFrameSaveAsFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnFrameSaveAsFanart.UseVisualStyleBackColor = True
        '
        'btnFrameSaveAsExtrathumb
        '
        Me.btnFrameSaveAsExtrathumb.Enabled = False
        Me.btnFrameSaveAsExtrathumb.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFrameSaveAsExtrathumb.Image = CType(resources.GetObject("btnFrameSaveAsExtrathumb.Image"), System.Drawing.Image)
        Me.btnFrameSaveAsExtrathumb.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFrameSaveAsExtrathumb.Location = New System.Drawing.Point(810, 361)
        Me.btnFrameSaveAsExtrathumb.Name = "btnFrameSaveAsExtrathumb"
        Me.btnFrameSaveAsExtrathumb.Size = New System.Drawing.Size(96, 83)
        Me.btnFrameSaveAsExtrathumb.TabIndex = 22
        Me.btnFrameSaveAsExtrathumb.Text = "Save as Extrathumb"
        Me.btnFrameSaveAsExtrathumb.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnFrameSaveAsExtrathumb.UseVisualStyleBackColor = True
        '
        'pnlFrameProgress
        '
        Me.pnlFrameProgress.BackColor = System.Drawing.Color.White
        Me.pnlFrameProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFrameProgress.Controls.Add(Me.lblExtractingFrame)
        Me.pnlFrameProgress.Controls.Add(Me.prbExtractingFrame)
        Me.pnlFrameProgress.Location = New System.Drawing.Point(272, 184)
        Me.pnlFrameProgress.Name = "pnlFrameProgress"
        Me.pnlFrameProgress.Size = New System.Drawing.Size(252, 51)
        Me.pnlFrameProgress.TabIndex = 19
        Me.pnlFrameProgress.Visible = False
        '
        'lblExtractingFrame
        '
        Me.lblExtractingFrame.AutoSize = True
        Me.lblExtractingFrame.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblExtractingFrame.Location = New System.Drawing.Point(2, 7)
        Me.lblExtractingFrame.Name = "lblExtractingFrame"
        Me.lblExtractingFrame.Size = New System.Drawing.Size(103, 13)
        Me.lblExtractingFrame.TabIndex = 0
        Me.lblExtractingFrame.Text = "Extracting Frame..."
        '
        'prbExtractingFrame
        '
        Me.prbExtractingFrame.Location = New System.Drawing.Point(4, 26)
        Me.prbExtractingFrame.MarqueeAnimationSpeed = 25
        Me.prbExtractingFrame.Name = "prbExtractingFrame"
        Me.prbExtractingFrame.Size = New System.Drawing.Size(242, 16)
        Me.prbExtractingFrame.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.prbExtractingFrame.TabIndex = 1
        '
        'lblTime
        '
        Me.lblTime.Location = New System.Drawing.Point(747, 462)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(59, 23)
        Me.lblTime.TabIndex = 24
        Me.lblTime.Text = "00:00:00"
        Me.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbFrame
        '
        Me.tbFrame.AutoSize = False
        Me.tbFrame.BackColor = System.Drawing.Color.White
        Me.tbFrame.Cursor = System.Windows.Forms.Cursors.Default
        Me.tbFrame.Enabled = False
        Me.tbFrame.Location = New System.Drawing.Point(6, 462)
        Me.tbFrame.Name = "tbFrame"
        Me.tbFrame.Size = New System.Drawing.Size(735, 27)
        Me.tbFrame.TabIndex = 23
        Me.tbFrame.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'btnFrameLoad
        '
        Me.btnFrameLoad.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFrameLoad.Image = CType(resources.GetObject("btnFrameLoad.Image"), System.Drawing.Image)
        Me.btnFrameLoad.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFrameLoad.Location = New System.Drawing.Point(810, 6)
        Me.btnFrameLoad.Name = "btnFrameLoad"
        Me.btnFrameLoad.Size = New System.Drawing.Size(96, 83)
        Me.btnFrameLoad.TabIndex = 20
        Me.btnFrameLoad.Text = "Load Video"
        Me.btnFrameLoad.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnFrameLoad.UseVisualStyleBackColor = True
        '
        'pbFrame
        '
        Me.pbFrame.BackColor = System.Drawing.Color.DimGray
        Me.pbFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbFrame.Location = New System.Drawing.Point(6, 6)
        Me.pbFrame.Name = "pbFrame"
        Me.pbFrame.Size = New System.Drawing.Size(800, 450)
        Me.pbFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbFrame.TabIndex = 25
        Me.pbFrame.TabStop = False
        '
        'tpSubtitles
        '
        Me.tpSubtitles.Controls.Add(Me.lblSubtitlesPreview)
        Me.tpSubtitles.Controls.Add(Me.txtSubtitlesPreview)
        Me.tpSubtitles.Controls.Add(Me.lvSubtitles)
        Me.tpSubtitles.Controls.Add(Me.btnRemoveSubtitle)
        Me.tpSubtitles.Controls.Add(Me.btnSetSubtitleDL)
        Me.tpSubtitles.Controls.Add(Me.btnSetSubtitleScrape)
        Me.tpSubtitles.Controls.Add(Me.btnSetSubtitleLocal)
        Me.tpSubtitles.Location = New System.Drawing.Point(4, 22)
        Me.tpSubtitles.Name = "tpSubtitles"
        Me.tpSubtitles.Size = New System.Drawing.Size(1923, 605)
        Me.tpSubtitles.TabIndex = 15
        Me.tpSubtitles.Text = "Subtitles"
        Me.tpSubtitles.UseVisualStyleBackColor = True
        '
        'lblSubtitlesPreview
        '
        Me.lblSubtitlesPreview.AutoSize = True
        Me.lblSubtitlesPreview.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSubtitlesPreview.Location = New System.Drawing.Point(10, 295)
        Me.lblSubtitlesPreview.Name = "lblSubtitlesPreview"
        Me.lblSubtitlesPreview.Size = New System.Drawing.Size(51, 13)
        Me.lblSubtitlesPreview.TabIndex = 37
        Me.lblSubtitlesPreview.Text = "Preview:"
        '
        'txtSubtitlesPreview
        '
        Me.txtSubtitlesPreview.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSubtitlesPreview.Location = New System.Drawing.Point(6, 311)
        Me.txtSubtitlesPreview.Multiline = True
        Me.txtSubtitlesPreview.Name = "txtSubtitlesPreview"
        Me.txtSubtitlesPreview.ReadOnly = True
        Me.txtSubtitlesPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSubtitlesPreview.Size = New System.Drawing.Size(800, 145)
        Me.txtSubtitlesPreview.TabIndex = 11
        '
        'lvSubtitles
        '
        Me.lvSubtitles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvSubtitles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.lvSubtitles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvSubtitles.FullRowSelect = True
        ListViewGroup3.Header = "Local Subtitles"
        ListViewGroup3.Name = "LocalSubtitles"
        Me.lvSubtitles.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup3})
        Me.lvSubtitles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        ListViewItem3.Group = ListViewGroup3
        Me.lvSubtitles.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem3})
        Me.lvSubtitles.Location = New System.Drawing.Point(6, 6)
        Me.lvSubtitles.MultiSelect = False
        Me.lvSubtitles.Name = "lvSubtitles"
        Me.lvSubtitles.Size = New System.Drawing.Size(800, 261)
        Me.lvSubtitles.TabIndex = 10
        Me.lvSubtitles.UseCompatibleStateImageBehavior = False
        Me.lvSubtitles.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Width = 25
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Width = 550
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Width = 100
        '
        'btnRemoveSubtitle
        '
        Me.btnRemoveSubtitle.Enabled = False
        Me.btnRemoveSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveSubtitle.Image = CType(resources.GetObject("btnRemoveSubtitle.Image"), System.Drawing.Image)
        Me.btnRemoveSubtitle.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveSubtitle.Location = New System.Drawing.Point(810, 373)
        Me.btnRemoveSubtitle.Name = "btnRemoveSubtitle"
        Me.btnRemoveSubtitle.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveSubtitle.TabIndex = 9
        Me.btnRemoveSubtitle.Text = "Remove"
        Me.btnRemoveSubtitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveSubtitle.UseVisualStyleBackColor = True
        '
        'btnSetSubtitleDL
        '
        Me.btnSetSubtitleDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetSubtitleDL.Image = CType(resources.GetObject("btnSetSubtitleDL.Image"), System.Drawing.Image)
        Me.btnSetSubtitleDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSubtitleDL.Location = New System.Drawing.Point(810, 184)
        Me.btnSetSubtitleDL.Name = "btnSetSubtitleDL"
        Me.btnSetSubtitleDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSubtitleDL.TabIndex = 6
        Me.btnSetSubtitleDL.Text = "Download"
        Me.btnSetSubtitleDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSubtitleDL.UseVisualStyleBackColor = True
        '
        'btnSetSubtitleScrape
        '
        Me.btnSetSubtitleScrape.Enabled = False
        Me.btnSetSubtitleScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetSubtitleScrape.Image = CType(resources.GetObject("btnSetSubtitleScrape.Image"), System.Drawing.Image)
        Me.btnSetSubtitleScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSubtitleScrape.Location = New System.Drawing.Point(810, 95)
        Me.btnSetSubtitleScrape.Name = "btnSetSubtitleScrape"
        Me.btnSetSubtitleScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSubtitleScrape.TabIndex = 5
        Me.btnSetSubtitleScrape.Text = "Scrape"
        Me.btnSetSubtitleScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSubtitleScrape.UseVisualStyleBackColor = True
        '
        'btnSetSubtitleLocal
        '
        Me.btnSetSubtitleLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetSubtitleLocal.Image = CType(resources.GetObject("btnSetSubtitleLocal.Image"), System.Drawing.Image)
        Me.btnSetSubtitleLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSubtitleLocal.Location = New System.Drawing.Point(810, 6)
        Me.btnSetSubtitleLocal.Name = "btnSetSubtitleLocal"
        Me.btnSetSubtitleLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSubtitleLocal.TabIndex = 4
        Me.btnSetSubtitleLocal.Text = "Local Browse"
        Me.btnSetSubtitleLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSubtitleLocal.UseVisualStyleBackColor = True
        '
        'tpTrailer
        '
        Me.tpTrailer.Controls.Add(Me.btnLocalTrailerPlay)
        Me.tpTrailer.Controls.Add(Me.txtLocalTrailer)
        Me.tpTrailer.Controls.Add(Me.pnlTrailerPreview)
        Me.tpTrailer.Controls.Add(Me.btnSetTrailerDL)
        Me.tpTrailer.Controls.Add(Me.btnRemoveTrailer)
        Me.tpTrailer.Controls.Add(Me.btnSetTrailerScrape)
        Me.tpTrailer.Controls.Add(Me.btnSetTrailerLocal)
        Me.tpTrailer.Location = New System.Drawing.Point(4, 22)
        Me.tpTrailer.Name = "tpTrailer"
        Me.tpTrailer.Size = New System.Drawing.Size(1923, 605)
        Me.tpTrailer.TabIndex = 13
        Me.tpTrailer.Text = "Trailer"
        Me.tpTrailer.UseVisualStyleBackColor = True
        '
        'btnLocalTrailerPlay
        '
        Me.btnLocalTrailerPlay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLocalTrailerPlay.Enabled = False
        Me.btnLocalTrailerPlay.Image = Global.Ember_Media_Manager.My.Resources.Resources.Play_Icon
        Me.btnLocalTrailerPlay.Location = New System.Drawing.Point(783, 462)
        Me.btnLocalTrailerPlay.Name = "btnLocalTrailerPlay"
        Me.btnLocalTrailerPlay.Size = New System.Drawing.Size(23, 22)
        Me.btnLocalTrailerPlay.TabIndex = 53
        Me.btnLocalTrailerPlay.UseVisualStyleBackColor = True
        '
        'txtLocalTrailer
        '
        Me.txtLocalTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtLocalTrailer.Location = New System.Drawing.Point(6, 462)
        Me.txtLocalTrailer.Name = "txtLocalTrailer"
        Me.txtLocalTrailer.ReadOnly = True
        Me.txtLocalTrailer.Size = New System.Drawing.Size(771, 22)
        Me.txtLocalTrailer.TabIndex = 15
        '
        'pnlTrailerPreview
        '
        Me.pnlTrailerPreview.BackColor = System.Drawing.Color.DimGray
        Me.pnlTrailerPreview.Controls.Add(Me.pnlTrailerPreviewNoPlayer)
        Me.pnlTrailerPreview.Location = New System.Drawing.Point(6, 6)
        Me.pnlTrailerPreview.Name = "pnlTrailerPreview"
        Me.pnlTrailerPreview.Size = New System.Drawing.Size(800, 450)
        Me.pnlTrailerPreview.TabIndex = 13
        '
        'pnlTrailerPreviewNoPlayer
        '
        Me.pnlTrailerPreviewNoPlayer.BackColor = System.Drawing.Color.White
        Me.pnlTrailerPreviewNoPlayer.Controls.Add(Me.tblTrailerPreviewNoPlayer)
        Me.pnlTrailerPreviewNoPlayer.Location = New System.Drawing.Point(285, 203)
        Me.pnlTrailerPreviewNoPlayer.Name = "pnlTrailerPreviewNoPlayer"
        Me.pnlTrailerPreviewNoPlayer.Size = New System.Drawing.Size(242, 56)
        Me.pnlTrailerPreviewNoPlayer.TabIndex = 0
        '
        'tblTrailerPreviewNoPlayer
        '
        Me.tblTrailerPreviewNoPlayer.AutoSize = True
        Me.tblTrailerPreviewNoPlayer.ColumnCount = 1
        Me.tblTrailerPreviewNoPlayer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblTrailerPreviewNoPlayer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTrailerPreviewNoPlayer.Controls.Add(Me.lblTrailerPreviewNoPlayer, 0, 0)
        Me.tblTrailerPreviewNoPlayer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTrailerPreviewNoPlayer.Location = New System.Drawing.Point(0, 0)
        Me.tblTrailerPreviewNoPlayer.Name = "tblTrailerPreviewNoPlayer"
        Me.tblTrailerPreviewNoPlayer.RowCount = 1
        Me.tblTrailerPreviewNoPlayer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblTrailerPreviewNoPlayer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56.0!))
        Me.tblTrailerPreviewNoPlayer.Size = New System.Drawing.Size(242, 56)
        Me.tblTrailerPreviewNoPlayer.TabIndex = 0
        '
        'lblTrailerPreviewNoPlayer
        '
        Me.lblTrailerPreviewNoPlayer.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTrailerPreviewNoPlayer.AutoSize = True
        Me.lblTrailerPreviewNoPlayer.Location = New System.Drawing.Point(54, 21)
        Me.lblTrailerPreviewNoPlayer.Name = "lblTrailerPreviewNoPlayer"
        Me.lblTrailerPreviewNoPlayer.Size = New System.Drawing.Size(134, 13)
        Me.lblTrailerPreviewNoPlayer.TabIndex = 0
        Me.lblTrailerPreviewNoPlayer.Text = "no Media Player enabled"
        '
        'btnSetTrailerDL
        '
        Me.btnSetTrailerDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetTrailerDL.Image = CType(resources.GetObject("btnSetTrailerDL.Image"), System.Drawing.Image)
        Me.btnSetTrailerDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetTrailerDL.Location = New System.Drawing.Point(810, 184)
        Me.btnSetTrailerDL.Name = "btnSetTrailerDL"
        Me.btnSetTrailerDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetTrailerDL.TabIndex = 7
        Me.btnSetTrailerDL.Text = "Download"
        Me.btnSetTrailerDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetTrailerDL.UseVisualStyleBackColor = True
        '
        'btnRemoveTrailer
        '
        Me.btnRemoveTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveTrailer.Image = CType(resources.GetObject("btnRemoveTrailer.Image"), System.Drawing.Image)
        Me.btnRemoveTrailer.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveTrailer.Location = New System.Drawing.Point(810, 373)
        Me.btnRemoveTrailer.Name = "btnRemoveTrailer"
        Me.btnRemoveTrailer.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveTrailer.TabIndex = 8
        Me.btnRemoveTrailer.Text = "Remove"
        Me.btnRemoveTrailer.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveTrailer.UseVisualStyleBackColor = True
        '
        'btnSetTrailerScrape
        '
        Me.btnSetTrailerScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetTrailerScrape.Image = CType(resources.GetObject("btnSetTrailerScrape.Image"), System.Drawing.Image)
        Me.btnSetTrailerScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetTrailerScrape.Location = New System.Drawing.Point(810, 95)
        Me.btnSetTrailerScrape.Name = "btnSetTrailerScrape"
        Me.btnSetTrailerScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetTrailerScrape.TabIndex = 6
        Me.btnSetTrailerScrape.Text = "Scrape"
        Me.btnSetTrailerScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetTrailerScrape.UseVisualStyleBackColor = True
        '
        'btnSetTrailerLocal
        '
        Me.btnSetTrailerLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetTrailerLocal.Image = CType(resources.GetObject("btnSetTrailerLocal.Image"), System.Drawing.Image)
        Me.btnSetTrailerLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetTrailerLocal.Location = New System.Drawing.Point(810, 6)
        Me.btnSetTrailerLocal.Name = "btnSetTrailerLocal"
        Me.btnSetTrailerLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetTrailerLocal.TabIndex = 5
        Me.btnSetTrailerLocal.Text = "Local Browse"
        Me.btnSetTrailerLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetTrailerLocal.UseVisualStyleBackColor = True
        '
        'tpTheme
        '
        Me.tpTheme.Controls.Add(Me.btnLocalThemePlay)
        Me.tpTheme.Controls.Add(Me.txtLocalTheme)
        Me.tpTheme.Controls.Add(Me.pnlThemePreview)
        Me.tpTheme.Controls.Add(Me.btnSetThemeDL)
        Me.tpTheme.Controls.Add(Me.btnRemoveTheme)
        Me.tpTheme.Controls.Add(Me.btnSetThemeScrape)
        Me.tpTheme.Controls.Add(Me.btnSetThemeLocal)
        Me.tpTheme.Location = New System.Drawing.Point(4, 22)
        Me.tpTheme.Name = "tpTheme"
        Me.tpTheme.Size = New System.Drawing.Size(1923, 605)
        Me.tpTheme.TabIndex = 14
        Me.tpTheme.Text = "Theme"
        Me.tpTheme.UseVisualStyleBackColor = True
        '
        'btnLocalThemePlay
        '
        Me.btnLocalThemePlay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLocalThemePlay.Enabled = False
        Me.btnLocalThemePlay.Image = Global.Ember_Media_Manager.My.Resources.Resources.Play_Icon
        Me.btnLocalThemePlay.Location = New System.Drawing.Point(783, 462)
        Me.btnLocalThemePlay.Name = "btnLocalThemePlay"
        Me.btnLocalThemePlay.Size = New System.Drawing.Size(23, 22)
        Me.btnLocalThemePlay.TabIndex = 56
        Me.btnLocalThemePlay.UseVisualStyleBackColor = True
        '
        'txtLocalTheme
        '
        Me.txtLocalTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtLocalTheme.Location = New System.Drawing.Point(6, 462)
        Me.txtLocalTheme.Name = "txtLocalTheme"
        Me.txtLocalTheme.ReadOnly = True
        Me.txtLocalTheme.Size = New System.Drawing.Size(771, 22)
        Me.txtLocalTheme.TabIndex = 55
        '
        'pnlThemePreview
        '
        Me.pnlThemePreview.BackColor = System.Drawing.Color.DimGray
        Me.pnlThemePreview.Controls.Add(Me.pnlThemePreviewNoPlayer)
        Me.pnlThemePreview.Location = New System.Drawing.Point(6, 6)
        Me.pnlThemePreview.Name = "pnlThemePreview"
        Me.pnlThemePreview.Size = New System.Drawing.Size(800, 450)
        Me.pnlThemePreview.TabIndex = 14
        '
        'pnlThemePreviewNoPlayer
        '
        Me.pnlThemePreviewNoPlayer.BackColor = System.Drawing.Color.White
        Me.pnlThemePreviewNoPlayer.Controls.Add(Me.tblThemePreviewNoPlayer)
        Me.pnlThemePreviewNoPlayer.Location = New System.Drawing.Point(285, 203)
        Me.pnlThemePreviewNoPlayer.Name = "pnlThemePreviewNoPlayer"
        Me.pnlThemePreviewNoPlayer.Size = New System.Drawing.Size(242, 56)
        Me.pnlThemePreviewNoPlayer.TabIndex = 0
        '
        'tblThemePreviewNoPlayer
        '
        Me.tblThemePreviewNoPlayer.AutoSize = True
        Me.tblThemePreviewNoPlayer.ColumnCount = 1
        Me.tblThemePreviewNoPlayer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblThemePreviewNoPlayer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblThemePreviewNoPlayer.Controls.Add(Me.lblThemePreviewNoPlayer, 0, 0)
        Me.tblThemePreviewNoPlayer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblThemePreviewNoPlayer.Location = New System.Drawing.Point(0, 0)
        Me.tblThemePreviewNoPlayer.Name = "tblThemePreviewNoPlayer"
        Me.tblThemePreviewNoPlayer.RowCount = 1
        Me.tblThemePreviewNoPlayer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblThemePreviewNoPlayer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56.0!))
        Me.tblThemePreviewNoPlayer.Size = New System.Drawing.Size(242, 56)
        Me.tblThemePreviewNoPlayer.TabIndex = 0
        '
        'lblThemePreviewNoPlayer
        '
        Me.lblThemePreviewNoPlayer.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblThemePreviewNoPlayer.AutoSize = True
        Me.lblThemePreviewNoPlayer.Location = New System.Drawing.Point(54, 21)
        Me.lblThemePreviewNoPlayer.Name = "lblThemePreviewNoPlayer"
        Me.lblThemePreviewNoPlayer.Size = New System.Drawing.Size(134, 13)
        Me.lblThemePreviewNoPlayer.TabIndex = 0
        Me.lblThemePreviewNoPlayer.Text = "no Media Player enabled"
        '
        'btnSetThemeDL
        '
        Me.btnSetThemeDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetThemeDL.Image = CType(resources.GetObject("btnSetThemeDL.Image"), System.Drawing.Image)
        Me.btnSetThemeDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetThemeDL.Location = New System.Drawing.Point(810, 184)
        Me.btnSetThemeDL.Name = "btnSetThemeDL"
        Me.btnSetThemeDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetThemeDL.TabIndex = 7
        Me.btnSetThemeDL.Text = "Download"
        Me.btnSetThemeDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetThemeDL.UseVisualStyleBackColor = True
        '
        'btnRemoveTheme
        '
        Me.btnRemoveTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveTheme.Image = CType(resources.GetObject("btnRemoveTheme.Image"), System.Drawing.Image)
        Me.btnRemoveTheme.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveTheme.Location = New System.Drawing.Point(810, 373)
        Me.btnRemoveTheme.Name = "btnRemoveTheme"
        Me.btnRemoveTheme.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveTheme.TabIndex = 8
        Me.btnRemoveTheme.Text = "Remove"
        Me.btnRemoveTheme.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveTheme.UseVisualStyleBackColor = True
        '
        'btnSetThemeScrape
        '
        Me.btnSetThemeScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetThemeScrape.Image = CType(resources.GetObject("btnSetThemeScrape.Image"), System.Drawing.Image)
        Me.btnSetThemeScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetThemeScrape.Location = New System.Drawing.Point(810, 95)
        Me.btnSetThemeScrape.Name = "btnSetThemeScrape"
        Me.btnSetThemeScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetThemeScrape.TabIndex = 6
        Me.btnSetThemeScrape.Text = "Scrape"
        Me.btnSetThemeScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetThemeScrape.UseVisualStyleBackColor = True
        '
        'btnSetThemeLocal
        '
        Me.btnSetThemeLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetThemeLocal.Image = CType(resources.GetObject("btnSetThemeLocal.Image"), System.Drawing.Image)
        Me.btnSetThemeLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetThemeLocal.Location = New System.Drawing.Point(810, 6)
        Me.btnSetThemeLocal.Name = "btnSetThemeLocal"
        Me.btnSetThemeLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetThemeLocal.TabIndex = 5
        Me.btnSetThemeLocal.Text = "Local Browse"
        Me.btnSetThemeLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetThemeLocal.UseVisualStyleBackColor = True
        '
        'tpMetaData
        '
        Me.tpMetaData.Controls.Add(Me.pnlFileInfo)
        Me.tpMetaData.Location = New System.Drawing.Point(4, 22)
        Me.tpMetaData.Name = "tpMetaData"
        Me.tpMetaData.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMetaData.Size = New System.Drawing.Size(1923, 605)
        Me.tpMetaData.TabIndex = 5
        Me.tpMetaData.Text = "Meta Data"
        Me.tpMetaData.UseVisualStyleBackColor = True
        '
        'pnlFileInfo
        '
        Me.pnlFileInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFileInfo.Location = New System.Drawing.Point(3, 3)
        Me.pnlFileInfo.Name = "pnlFileInfo"
        Me.pnlFileInfo.Size = New System.Drawing.Size(1917, 599)
        Me.pnlFileInfo.TabIndex = 0
        '
        'tpMediaStub
        '
        Me.tpMediaStub.Controls.Add(Me.lblMediaStubMessage)
        Me.tpMediaStub.Controls.Add(Me.lblMediaStubTitle)
        Me.tpMediaStub.Controls.Add(Me.txtMediaStubMessage)
        Me.tpMediaStub.Controls.Add(Me.txtMediaStubTitle)
        Me.tpMediaStub.Location = New System.Drawing.Point(4, 22)
        Me.tpMediaStub.Name = "tpMediaStub"
        Me.tpMediaStub.Size = New System.Drawing.Size(1923, 605)
        Me.tpMediaStub.TabIndex = 7
        Me.tpMediaStub.Text = "Media Stub"
        Me.tpMediaStub.UseVisualStyleBackColor = True
        '
        'lblMediaStubMessage
        '
        Me.lblMediaStubMessage.AutoSize = True
        Me.lblMediaStubMessage.Location = New System.Drawing.Point(203, 231)
        Me.lblMediaStubMessage.Name = "lblMediaStubMessage"
        Me.lblMediaStubMessage.Size = New System.Drawing.Size(55, 13)
        Me.lblMediaStubMessage.TabIndex = 3
        Me.lblMediaStubMessage.Text = "Message:"
        '
        'lblMediaStubTitle
        '
        Me.lblMediaStubTitle.AutoSize = True
        Me.lblMediaStubTitle.Location = New System.Drawing.Point(203, 170)
        Me.lblMediaStubTitle.Name = "lblMediaStubTitle"
        Me.lblMediaStubTitle.Size = New System.Drawing.Size(31, 13)
        Me.lblMediaStubTitle.TabIndex = 2
        Me.lblMediaStubTitle.Text = "Title:"
        '
        'txtMediaStubMessage
        '
        Me.txtMediaStubMessage.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMediaStubMessage.Location = New System.Drawing.Point(206, 247)
        Me.txtMediaStubMessage.Name = "txtMediaStubMessage"
        Me.txtMediaStubMessage.Size = New System.Drawing.Size(472, 22)
        Me.txtMediaStubMessage.TabIndex = 1
        '
        'txtMediaStubTitle
        '
        Me.txtMediaStubTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMediaStubTitle.Location = New System.Drawing.Point(206, 186)
        Me.txtMediaStubTitle.Name = "txtMediaStubTitle"
        Me.txtMediaStubTitle.Size = New System.Drawing.Size(260, 22)
        Me.txtMediaStubTitle.TabIndex = 0
        '
        'btnRescrape
        '
        Me.btnRescrape.AutoSize = True
        Me.btnRescrape.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRescrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRescrape.Image = CType(resources.GetObject("btnRescrape.Image"), System.Drawing.Image)
        Me.btnRescrape.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRescrape.Location = New System.Drawing.Point(905, 26)
        Me.btnRescrape.Name = "btnRescrape"
        Me.btnRescrape.Size = New System.Drawing.Size(98, 23)
        Me.btnRescrape.TabIndex = 7
        Me.btnRescrape.Text = "Re-scrape"
        Me.btnRescrape.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRescrape.UseVisualStyleBackColor = True
        '
        'btnChangeMovie
        '
        Me.btnChangeMovie.AutoSize = True
        Me.btnChangeMovie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnChangeMovie.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnChangeMovie.Image = CType(resources.GetObject("btnChangeMovie.Image"), System.Drawing.Image)
        Me.btnChangeMovie.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnChangeMovie.Location = New System.Drawing.Point(1009, 26)
        Me.btnChangeMovie.Name = "btnChangeMovie"
        Me.btnChangeMovie.Size = New System.Drawing.Size(107, 23)
        Me.btnChangeMovie.TabIndex = 8
        Me.btnChangeMovie.Text = "Change Movie"
        Me.btnChangeMovie.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnChangeMovie.UseVisualStyleBackColor = True
        '
        'tmrDelay
        '
        Me.tmrDelay.Interval = 250
        '
        'StatusStrip
        '
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(18, 18)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsFilename})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 739)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1931, 22)
        Me.StatusStrip.SizingGrip = False
        Me.StatusStrip.TabIndex = 9
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'tsFilename
        '
        Me.tsFilename.Name = "tsFilename"
        Me.tsFilename.Size = New System.Drawing.Size(55, 17)
        Me.tsFilename.Text = "Filename"
        '
        'cbSourceLanguage
        '
        Me.cbSourceLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSourceLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSourceLanguage.Location = New System.Drawing.Point(71, 26)
        Me.cbSourceLanguage.Name = "cbSourceLanguage"
        Me.cbSourceLanguage.Size = New System.Drawing.Size(172, 21)
        Me.cbSourceLanguage.TabIndex = 76
        '
        'lblLanguage
        '
        Me.lblLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblLanguage.AutoSize = True
        Me.lblLanguage.Location = New System.Drawing.Point(3, 31)
        Me.lblLanguage.Name = "lblLanguage"
        Me.lblLanguage.Size = New System.Drawing.Size(61, 13)
        Me.lblLanguage.TabIndex = 75
        Me.lblLanguage.Text = "Language:"
        '
        'Timer1
        '
        Me.Timer1.Interval = 250
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.tcEdit)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1931, 631)
        Me.pnlMain.TabIndex = 78
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 687)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1931, 52)
        Me.pnlBottom.TabIndex = 79
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 8
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnOK, 6, 1)
        Me.tblBottom.Controls.Add(Me.btnCancel, 7, 1)
        Me.tblBottom.Controls.Add(Me.btnChangeMovie, 4, 1)
        Me.tblBottom.Controls.Add(Me.btnRescrape, 3, 1)
        Me.tblBottom.Controls.Add(Me.lblLanguage, 0, 1)
        Me.tblBottom.Controls.Add(Me.cbSourceLanguage, 1, 1)
        Me.tblBottom.Controls.Add(Me.chkLocked, 0, 0)
        Me.tblBottom.Controls.Add(Me.chkMarked, 1, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 2
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblBottom.Size = New System.Drawing.Size(1931, 52)
        Me.tblBottom.TabIndex = 78
        '
        'chkMarked
        '
        Me.chkMarked.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkMarked.AutoSize = True
        Me.chkMarked.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarked.Location = New System.Drawing.Point(71, 3)
        Me.chkMarked.Name = "chkMarked"
        Me.chkMarked.Size = New System.Drawing.Size(65, 17)
        Me.chkMarked.TabIndex = 5
        Me.chkMarked.Text = "Marked"
        Me.chkMarked.UseVisualStyleBackColor = True
        '
        'dtpReleaseDate
        '
        Me.dtpReleaseDate.CustomFormat = "yyyy-dd-MM"
        Me.dtpReleaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReleaseDate.Location = New System.Drawing.Point(81, 139)
        Me.dtpReleaseDate.Name = "dtpReleaseDate"
        Me.dtpReleaseDate.Size = New System.Drawing.Size(142, 22)
        Me.dtpReleaseDate.TabIndex = 29
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblYear.Location = New System.Drawing.Point(3, 123)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(30, 13)
        Me.lblYear.TabIndex = 8
        Me.lblYear.Text = "Year:"
        '
        'chkLocked
        '
        Me.chkLocked.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkLocked.AutoSize = True
        Me.chkLocked.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLocked.Location = New System.Drawing.Point(3, 3)
        Me.chkLocked.Name = "chkLocked"
        Me.chkLocked.Size = New System.Drawing.Size(62, 17)
        Me.chkLocked.TabIndex = 5
        Me.chkLocked.Text = "Locked"
        Me.chkLocked.UseVisualStyleBackColor = True
        '
        'lblTags
        '
        Me.lblTags.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTags.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblTags, 2)
        Me.lblTags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTags.Location = New System.Drawing.Point(3, 426)
        Me.lblTags.Name = "lblTags"
        Me.lblTags.Size = New System.Drawing.Size(32, 13)
        Me.lblTags.TabIndex = 23
        Me.lblTags.Text = "Tags:"
        '
        'clbTags
        '
        Me.clbTags.BackColor = System.Drawing.SystemColors.Window
        Me.clbTags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbTags.CheckOnClick = True
        Me.tblDetails.SetColumnSpan(Me.clbTags, 2)
        Me.clbTags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.clbTags.FormattingEnabled = True
        Me.clbTags.IntegralHeight = False
        Me.clbTags.Location = New System.Drawing.Point(3, 442)
        Me.clbTags.Name = "clbTags"
        Me.clbTags.Size = New System.Drawing.Size(220, 140)
        Me.clbTags.TabIndex = 24
        '
        'cbVideoSource
        '
        Me.cbVideoSource.FormattingEnabled = True
        Me.cbVideoSource.Location = New System.Drawing.Point(81, 180)
        Me.cbVideoSource.Name = "cbVideoSource"
        Me.cbVideoSource.Size = New System.Drawing.Size(142, 21)
        Me.cbVideoSource.TabIndex = 80
        '
        'lblMovieSet
        '
        Me.lblMovieSet.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSet.AutoSize = True
        Me.lblMovieSet.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieSet.Location = New System.Drawing.Point(229, 211)
        Me.lblMovieSet.Name = "lblMovieSet"
        Me.lblMovieSet.Size = New System.Drawing.Size(56, 13)
        Me.lblMovieSet.TabIndex = 49
        Me.lblMovieSet.Text = "Movieset:"
        '
        'dgvCredits
        '
        Me.dgvCredits.AllowUserToResizeColumns = False
        Me.dgvCredits.AllowUserToResizeRows = False
        Me.dgvCredits.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvCredits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCredits.ColumnHeadersVisible = False
        Me.dgvCredits.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCreditsName})
        Me.dgvCredits.Location = New System.Drawing.Point(229, 273)
        Me.dgvCredits.Name = "dgvCredits"
        Me.dgvCredits.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvCredits.Size = New System.Drawing.Size(240, 150)
        Me.dgvCredits.TabIndex = 82
        '
        'dgvStudios
        '
        Me.dgvStudios.AllowUserToResizeColumns = False
        Me.dgvStudios.AllowUserToResizeRows = False
        Me.dgvStudios.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvStudios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStudios.ColumnHeadersVisible = False
        Me.dgvStudios.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colStudiosName})
        Me.dgvStudios.Location = New System.Drawing.Point(475, 273)
        Me.dgvStudios.Name = "dgvStudios"
        Me.dgvStudios.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvStudios.Size = New System.Drawing.Size(240, 150)
        Me.dgvStudios.TabIndex = 82
        '
        'dgvDirectors
        '
        Me.dgvDirectors.AllowUserToResizeColumns = False
        Me.dgvDirectors.AllowUserToResizeRows = False
        Me.dgvDirectors.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvDirectors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDirectors.ColumnHeadersVisible = False
        Me.dgvDirectors.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDirectorsName})
        Me.dgvDirectors.Location = New System.Drawing.Point(229, 442)
        Me.dgvDirectors.Name = "dgvDirectors"
        Me.dgvDirectors.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvDirectors.Size = New System.Drawing.Size(240, 150)
        Me.dgvDirectors.TabIndex = 82
        '
        'dgvCountries
        '
        Me.dgvCountries.AllowUserToResizeColumns = False
        Me.dgvCountries.AllowUserToResizeRows = False
        Me.dgvCountries.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvCountries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCountries.ColumnHeadersVisible = False
        Me.dgvCountries.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCountriesName})
        Me.dgvCountries.Location = New System.Drawing.Point(475, 442)
        Me.dgvCountries.Name = "dgvCountries"
        Me.dgvCountries.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvCountries.Size = New System.Drawing.Size(240, 150)
        Me.dgvCountries.TabIndex = 82
        '
        'colCreditsName
        '
        Me.colCreditsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colCreditsName.HeaderText = "Name"
        Me.colCreditsName.Name = "colCreditsName"
        Me.colCreditsName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colDirectorsName
        '
        Me.colDirectorsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDirectorsName.HeaderText = "Name"
        Me.colDirectorsName.Name = "colDirectorsName"
        Me.colDirectorsName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colStudiosName
        '
        Me.colStudiosName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colStudiosName.HeaderText = "Name"
        Me.colStudiosName.Name = "colStudiosName"
        Me.colStudiosName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colCountriesName
        '
        Me.colCountriesName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colCountriesName.HeaderText = "Name"
        Me.colCountriesName.Name = "colCountriesName"
        Me.colCountriesName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'dlgEditMovie
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(1931, 761)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.StatusStrip)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgEditMovie"
        Me.Text = "Edit Movie"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tblTop.ResumeLayout(False)
        Me.tblTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcEdit.ResumeLayout(False)
        Me.tpDetails.ResumeLayout(False)
        Me.tpDetails.PerformLayout()
        Me.tblDetails.ResumeLayout(False)
        Me.tblDetails.PerformLayout()
        Me.tpPoster.ResumeLayout(False)
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpBanner.ResumeLayout(False)
        CType(Me.pbBanner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpLandscape.ResumeLayout(False)
        CType(Me.pbLandscape, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpClearArt.ResumeLayout(False)
        CType(Me.pbClearArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpClearLogo.ResumeLayout(False)
        CType(Me.pbClearLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpDiscArt.ResumeLayout(False)
        CType(Me.pbDiscArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpFanart.ResumeLayout(False)
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpExtrafanarts.ResumeLayout(False)
        Me.pnlExtrafanartsSetAsFanart.ResumeLayout(False)
        CType(Me.pbExtrafanarts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpExtrathumbs.ResumeLayout(False)
        Me.pnlExtrathumbsSetAsFanart.ResumeLayout(False)
        CType(Me.pbExtrathumbs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpFrameExtraction.ResumeLayout(False)
        Me.pnlFrameProgress.ResumeLayout(False)
        Me.pnlFrameProgress.PerformLayout()
        CType(Me.tbFrame, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFrame, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpSubtitles.ResumeLayout(False)
        Me.tpSubtitles.PerformLayout()
        Me.tpTrailer.ResumeLayout(False)
        Me.tpTrailer.PerformLayout()
        Me.pnlTrailerPreview.ResumeLayout(False)
        Me.pnlTrailerPreviewNoPlayer.ResumeLayout(False)
        Me.pnlTrailerPreviewNoPlayer.PerformLayout()
        Me.tblTrailerPreviewNoPlayer.ResumeLayout(False)
        Me.tblTrailerPreviewNoPlayer.PerformLayout()
        Me.tpTheme.ResumeLayout(False)
        Me.tpTheme.PerformLayout()
        Me.pnlThemePreview.ResumeLayout(False)
        Me.pnlThemePreviewNoPlayer.ResumeLayout(False)
        Me.pnlThemePreviewNoPlayer.PerformLayout()
        Me.tblThemePreviewNoPlayer.ResumeLayout(False)
        Me.tblThemePreviewNoPlayer.PerformLayout()
        Me.tpMetaData.ResumeLayout(False)
        Me.tpMediaStub.ResumeLayout(False)
        Me.tpMediaStub.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.tblBottom.PerformLayout()
        CType(Me.dgvCredits, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvStudios, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDirectors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCountries, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents tcEdit As System.Windows.Forms.TabControl
    Friend WithEvents tpDetails As System.Windows.Forms.TabPage
    Friend WithEvents lblMPAADesc As System.Windows.Forms.Label
    Friend WithEvents txtMPAADesc As System.Windows.Forms.TextBox
    Friend WithEvents btnActorEdit As System.Windows.Forms.Button
    Friend WithEvents btnActorAdd As System.Windows.Forms.Button
    Friend WithEvents btnManual As System.Windows.Forms.Button
    Friend WithEvents btnActorRemove As System.Windows.Forms.Button
    Friend WithEvents lblActors As System.Windows.Forms.Label
    Friend WithEvents lvActors As System.Windows.Forms.ListView
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRole As System.Windows.Forms.ColumnHeader
    Friend WithEvents colThumb As System.Windows.Forms.ColumnHeader
    Friend WithEvents lbMPAA As System.Windows.Forms.ListBox
    Friend WithEvents lblGenres As System.Windows.Forms.Label
    Friend WithEvents lblMPAA As System.Windows.Forms.Label
    Friend WithEvents lblDirectors As System.Windows.Forms.Label
    Friend WithEvents txtTop250 As System.Windows.Forms.TextBox
    Friend WithEvents lblTop250 As System.Windows.Forms.Label
    Friend WithEvents lblPlot As System.Windows.Forms.Label
    Friend WithEvents txtPlot As System.Windows.Forms.TextBox
    Friend WithEvents lblOutline As System.Windows.Forms.Label
    Friend WithEvents txtOutline As System.Windows.Forms.TextBox
    Friend WithEvents lblTagline As System.Windows.Forms.Label
    Friend WithEvents txtTagline As System.Windows.Forms.TextBox
    Friend WithEvents lblRatings As System.Windows.Forms.Label
    Friend WithEvents txtYear As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents tpPoster As System.Windows.Forms.TabPage
    Friend WithEvents tpFanart As System.Windows.Forms.TabPage
    Friend WithEvents btnSetPosterLocal As System.Windows.Forms.Button
    Friend WithEvents pbPoster As System.Windows.Forms.PictureBox
    Friend WithEvents btnSetFanartLocal As System.Windows.Forms.Button
    Friend WithEvents pbFanart As System.Windows.Forms.PictureBox
    Friend WithEvents ofdLocalFiles As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblRuntime As System.Windows.Forms.Label
    Friend WithEvents txtRuntime As System.Windows.Forms.TextBox
    Friend WithEvents lblReleaseDate As System.Windows.Forms.Label
    Friend WithEvents lblCredits As System.Windows.Forms.Label
    Friend WithEvents lblCertifications As System.Windows.Forms.Label
    Friend WithEvents lblTrailerURL As System.Windows.Forms.Label
    Friend WithEvents txtTrailer As System.Windows.Forms.TextBox
    Friend WithEvents btnSetPosterScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetFanartScrape As System.Windows.Forms.Button
    Friend WithEvents lblPosterSize As System.Windows.Forms.Label
    Friend WithEvents lblFanartSize As System.Windows.Forms.Label
    Friend WithEvents lblStudios As System.Windows.Forms.Label
    Friend WithEvents tpFrameExtraction As System.Windows.Forms.TabPage
    Friend WithEvents tpExtrathumbs As System.Windows.Forms.TabPage
    Friend WithEvents pbExtrathumbs As System.Windows.Forms.PictureBox
    Friend WithEvents btnExtrathumbsDown As System.Windows.Forms.Button
    Friend WithEvents btnExtrathumbsUp As System.Windows.Forms.Button
    Friend WithEvents btnExtrathumbsRemove As System.Windows.Forms.Button
    Friend WithEvents btnRescrape As System.Windows.Forms.Button
    Friend WithEvents btnChangeMovie As System.Windows.Forms.Button
    Friend WithEvents btnRemovePoster As System.Windows.Forms.Button
    Friend WithEvents btnRemoveFanart As System.Windows.Forms.Button
    Friend WithEvents btnExtrathumbsRefresh As System.Windows.Forms.Button
    Friend WithEvents btnStudio As System.Windows.Forms.Button
    Friend WithEvents clbGenres As System.Windows.Forms.CheckedListBox
    Friend WithEvents pnlExtrathumbsSetAsFanart As System.Windows.Forms.Panel
    Friend WithEvents btnExtrathumbsSetAsFanart As System.Windows.Forms.Button
    Friend WithEvents btnDLTrailer As System.Windows.Forms.Button
    Friend WithEvents btnPlayTrailer As System.Windows.Forms.Button
    Friend WithEvents btnSetPosterDL As System.Windows.Forms.Button
    Friend WithEvents btnSetFanartDL As System.Windows.Forms.Button
    Friend WithEvents tpMetaData As System.Windows.Forms.TabPage
    Friend WithEvents pnlFileInfo As System.Windows.Forms.Panel
    Friend WithEvents lblSortTilte As System.Windows.Forms.Label
    Friend WithEvents txtSortTitle As System.Windows.Forms.TextBox
    Friend WithEvents tmrDelay As System.Windows.Forms.Timer
    Friend WithEvents btnActorDown As System.Windows.Forms.Button
    Friend WithEvents btnActorUp As System.Windows.Forms.Button
    Friend WithEvents lblVideoSource As System.Windows.Forms.Label
    Friend WithEvents lblCountries As System.Windows.Forms.Label
    Friend WithEvents txtOriginalTitle As System.Windows.Forms.TextBox
    Friend WithEvents lblOriginalTitle As System.Windows.Forms.Label
    Friend WithEvents chkWatched As System.Windows.Forms.CheckBox
    Friend WithEvents tpExtrafanarts As System.Windows.Forms.TabPage
    Friend WithEvents pnlExtrafanartsSetAsFanart As System.Windows.Forms.Panel
    Friend WithEvents btnExtrafanartsSetAsFanart As System.Windows.Forms.Button
    Friend WithEvents btnExtrafanartsRefresh As System.Windows.Forms.Button
    Friend WithEvents btnExtrafanartsRemove As System.Windows.Forms.Button
    Friend WithEvents pbExtrafanarts As System.Windows.Forms.PictureBox
    Friend WithEvents pnlExtrathumbs As System.Windows.Forms.Panel
    Friend WithEvents pnlExtrafanarts As System.Windows.Forms.Panel
    Friend WithEvents lblExtrafanartsSize As System.Windows.Forms.Label
    Friend WithEvents lblExtrathumbsSize As System.Windows.Forms.Label
    Friend WithEvents tpMediaStub As System.Windows.Forms.TabPage
    Friend WithEvents lblMediaStubMessage As System.Windows.Forms.Label
    Friend WithEvents lblMediaStubTitle As System.Windows.Forms.Label
    Friend WithEvents txtMediaStubMessage As System.Windows.Forms.TextBox
    Friend WithEvents txtMediaStubTitle As System.Windows.Forms.TextBox
    Friend WithEvents tpBanner As System.Windows.Forms.TabPage
    Friend WithEvents btnSetBannerDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveBanner As System.Windows.Forms.Button
    Friend WithEvents lblBannerSize As System.Windows.Forms.Label
    Friend WithEvents btnSetBannerScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetBannerLocal As System.Windows.Forms.Button
    Friend WithEvents pbBanner As System.Windows.Forms.PictureBox
    Friend WithEvents tpLandscape As System.Windows.Forms.TabPage
    Friend WithEvents btnSetLandscapeDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveLandscape As System.Windows.Forms.Button
    Friend WithEvents lblLandscapeSize As System.Windows.Forms.Label
    Friend WithEvents btnSetLandscapeScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetLandscapeLocal As System.Windows.Forms.Button
    Friend WithEvents pbLandscape As System.Windows.Forms.PictureBox
    Friend WithEvents tpClearArt As System.Windows.Forms.TabPage
    Friend WithEvents btnSetClearArtDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveClearArt As System.Windows.Forms.Button
    Friend WithEvents lblClearArtSize As System.Windows.Forms.Label
    Friend WithEvents btnSetClearArtScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetClearArtLocal As System.Windows.Forms.Button
    Friend WithEvents pbClearArt As System.Windows.Forms.PictureBox
    Friend WithEvents tpClearLogo As System.Windows.Forms.TabPage
    Friend WithEvents btnSetClearLogoDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveClearLogo As System.Windows.Forms.Button
    Friend WithEvents lblClearLogoSize As System.Windows.Forms.Label
    Friend WithEvents btnSetClearLogoScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetClearLogoLocal As System.Windows.Forms.Button
    Friend WithEvents pbClearLogo As System.Windows.Forms.PictureBox
    Friend WithEvents tpDiscArt As System.Windows.Forms.TabPage
    Friend WithEvents btnSetDiscArtDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveDiscArt As System.Windows.Forms.Button
    Friend WithEvents lblDiscArtSize As System.Windows.Forms.Label
    Friend WithEvents btnSetDiscArtScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetDiscArtLocal As System.Windows.Forms.Button
    Friend WithEvents pbDiscArt As System.Windows.Forms.PictureBox
    Friend WithEvents tpTrailer As System.Windows.Forms.TabPage
    Friend WithEvents tpTheme As System.Windows.Forms.TabPage
    Friend WithEvents btnSetTrailerDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveTrailer As System.Windows.Forms.Button
    Friend WithEvents btnSetTrailerScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetTrailerLocal As System.Windows.Forms.Button
    Friend WithEvents btnSetThemeDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveTheme As System.Windows.Forms.Button
    Friend WithEvents btnSetThemeScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetThemeLocal As System.Windows.Forms.Button
    Friend WithEvents tpSubtitles As System.Windows.Forms.TabPage
    Friend WithEvents btnRemoveSubtitle As System.Windows.Forms.Button
    Friend WithEvents btnSetSubtitleDL As System.Windows.Forms.Button
    Friend WithEvents btnSetSubtitleScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetSubtitleLocal As System.Windows.Forms.Button
    Friend WithEvents lvSubtitles As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblSubtitlesPreview As System.Windows.Forms.Label
    Friend WithEvents txtSubtitlesPreview As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents tsFilename As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtMPAA As System.Windows.Forms.TextBox
    Friend WithEvents pnlTrailerPreview As System.Windows.Forms.Panel
    Friend WithEvents pnlTrailerPreviewNoPlayer As System.Windows.Forms.Panel
    Friend WithEvents tblTrailerPreviewNoPlayer As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblTrailerPreviewNoPlayer As System.Windows.Forms.Label
    Friend WithEvents pnlThemePreview As System.Windows.Forms.Panel
    Friend WithEvents pnlThemePreviewNoPlayer As System.Windows.Forms.Panel
    Friend WithEvents tblThemePreviewNoPlayer As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblThemePreviewNoPlayer As System.Windows.Forms.Label
    Friend WithEvents colID As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnSetExtrafanartsScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetExtrathumbsScrape As System.Windows.Forms.Button
    Friend WithEvents cbSourceLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents lblLanguage As System.Windows.Forms.Label
    Friend WithEvents btnLocalTrailerPlay As Button
    Friend WithEvents txtLocalTrailer As TextBox
    Friend WithEvents btnLocalThemePlay As Button
    Friend WithEvents txtLocalTheme As TextBox
    Friend WithEvents txtUserRating As TextBox
    Friend WithEvents lblUserRating As Label
    Friend WithEvents btnFrameSaveAsExtrafanart As Button
    Friend WithEvents btnFrameSaveAsFanart As Button
    Friend WithEvents btnFrameSaveAsExtrathumb As Button
    Friend WithEvents pnlFrameProgress As Panel
    Friend WithEvents lblExtractingFrame As Label
    Friend WithEvents prbExtractingFrame As ProgressBar
    Friend WithEvents lblTime As Label
    Friend WithEvents tbFrame As TrackBar
    Friend WithEvents btnFrameLoad As Button
    Friend WithEvents pbFrame As PictureBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents dtpLastPlayed As DateTimePicker
    Friend WithEvents tblTop As TableLayoutPanel
    Friend WithEvents pnlMain As Panel
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents tblBottom As TableLayoutPanel
    Friend WithEvents chkMarked As CheckBox
    Friend WithEvents tblDetails As TableLayoutPanel
    Friend WithEvents lblYear As Label
    Friend WithEvents dtpReleaseDate As DateTimePicker
    Friend WithEvents chkLocked As CheckBox
    Friend WithEvents lblTags As Label
    Friend WithEvents clbTags As CheckedListBox
    Friend WithEvents cbVideoSource As ComboBox
    Friend WithEvents lblMovieSet As Label
    Friend WithEvents dgvCredits As DataGridView
    Friend WithEvents colCreditsName As DataGridViewTextBoxColumn
    Friend WithEvents dgvStudios As DataGridView
    Friend WithEvents colStudiosName As DataGridViewTextBoxColumn
    Friend WithEvents dgvDirectors As DataGridView
    Friend WithEvents colDirectorsName As DataGridViewTextBoxColumn
    Friend WithEvents dgvCountries As DataGridView
    Friend WithEvents colCountriesName As DataGridViewTextBoxColumn
End Class
