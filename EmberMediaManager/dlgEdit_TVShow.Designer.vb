<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgEdit_TVShow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgEdit_TVShow))
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tblTop = New System.Windows.Forms.TableLayoutPanel()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ofdLocalFiles = New System.Windows.Forms.OpenFileDialog()
        Me.cbEpisodeOrdering = New System.Windows.Forms.ComboBox()
        Me.lblEpisodeOrdering = New System.Windows.Forms.Label()
        Me.cbEpisodeSorting = New System.Windows.Forms.ComboBox()
        Me.lblEpisodeSorting = New System.Windows.Forms.Label()
        Me.lblLanguage = New System.Windows.Forms.Label()
        Me.cbSourceLanguage = New System.Windows.Forms.ComboBox()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnChange = New System.Windows.Forms.Button()
        Me.btnRescrape = New System.Windows.Forms.Button()
        Me.chkLocked = New System.Windows.Forms.CheckBox()
        Me.chkMarked = New System.Windows.Forms.CheckBox()
        Me.chkMarkedCustom1 = New System.Windows.Forms.CheckBox()
        Me.chkMarkedCustom3 = New System.Windows.Forms.CheckBox()
        Me.chkMarkedCustom2 = New System.Windows.Forms.CheckBox()
        Me.chkMarkedCustom4 = New System.Windows.Forms.CheckBox()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.tsslFilename = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslSpring = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tspbStatus = New System.Windows.Forms.ToolStripProgressBar()
        Me.pnlMain = New System.Windows.Forms.Panel()
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
        Me.lblPlot = New System.Windows.Forms.Label()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.lblPremiered = New System.Windows.Forms.Label()
        Me.dtpPremiered = New System.Windows.Forms.DateTimePicker()
        Me.cbUserRating = New System.Windows.Forms.ComboBox()
        Me.lblRatings = New System.Windows.Forms.Label()
        Me.lblCertifications = New System.Windows.Forms.Label()
        Me.dgvCertifications = New System.Windows.Forms.DataGridView()
        Me.colCertificationsName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvRatings = New System.Windows.Forms.DataGridView()
        Me.colRatingsDefault = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colRatingsSource = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRatingsValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRatingsMax = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRatingsVotes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblMPAA = New System.Windows.Forms.Label()
        Me.lbMPAA = New System.Windows.Forms.ListBox()
        Me.btnCertificationsAsMPAARating = New System.Windows.Forms.Button()
        Me.lblMPAADesc = New System.Windows.Forms.Label()
        Me.txtMPAADescription = New System.Windows.Forms.TextBox()
        Me.txtMPAA = New EmberAPI.AdvancedControls.TextBox_with_Watermark()
        Me.lblUserRating = New System.Windows.Forms.Label()
        Me.lblRuntime = New System.Windows.Forms.Label()
        Me.txtRuntime = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.lblUserNote = New System.Windows.Forms.Label()
        Me.txtUserNote = New System.Windows.Forms.TextBox()
        Me.tpDetails2 = New System.Windows.Forms.TabPage()
        Me.tblDetails2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDateAdded = New System.Windows.Forms.Label()
        Me.dtpDateAdded_Date = New System.Windows.Forms.DateTimePicker()
        Me.chkWatched = New System.Windows.Forms.CheckBox()
        Me.dtpLastPlayed_Date = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateAdded_Time = New System.Windows.Forms.DateTimePicker()
        Me.dtpLastPlayed_Time = New System.Windows.Forms.DateTimePicker()
        Me.lblGenres = New System.Windows.Forms.Label()
        Me.lblUniqueIds = New System.Windows.Forms.Label()
        Me.dgvUniqueIds = New System.Windows.Forms.DataGridView()
        Me.colUniqueIdsDefault = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colUniqueIdsType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUniqueIdsValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbGenres = New System.Windows.Forms.ListBox()
        Me.btnGenres_Add = New System.Windows.Forms.Button()
        Me.btnGenres_Remove = New System.Windows.Forms.Button()
        Me.btnGenres_Up = New System.Windows.Forms.Button()
        Me.btnGenres_Down = New System.Windows.Forms.Button()
        Me.lblStudios = New System.Windows.Forms.Label()
        Me.dgvCountries = New System.Windows.Forms.DataGridView()
        Me.colCountriesName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblCountries = New System.Windows.Forms.Label()
        Me.dgvStudios = New System.Windows.Forms.DataGridView()
        Me.colStudiosName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblTags = New System.Windows.Forms.Label()
        Me.lbTags = New System.Windows.Forms.ListBox()
        Me.cbGenres = New System.Windows.Forms.ComboBox()
        Me.cbTags = New System.Windows.Forms.ComboBox()
        Me.btnTags_Add = New System.Windows.Forms.Button()
        Me.btnTags_Remove = New System.Windows.Forms.Button()
        Me.btnTags_Up = New System.Windows.Forms.Button()
        Me.btnTags_Down = New System.Windows.Forms.Button()
        Me.tpCastCrew = New System.Windows.Forms.TabPage()
        Me.tblCastCrew = New System.Windows.Forms.TableLayoutPanel()
        Me.lblActors = New System.Windows.Forms.Label()
        Me.lvActors = New System.Windows.Forms.ListView()
        Me.colActorsID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colActorsName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colActorsRole = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colActorsThumb = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnActorsAdd = New System.Windows.Forms.Button()
        Me.btnActorsRemove = New System.Windows.Forms.Button()
        Me.btnActorsEdit = New System.Windows.Forms.Button()
        Me.btnActorsDown = New System.Windows.Forms.Button()
        Me.btnActorsUp = New System.Windows.Forms.Button()
        Me.dgvCreators = New System.Windows.Forms.DataGridView()
        Me.colCreditsName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblCreators = New System.Windows.Forms.Label()
        Me.tpOther = New System.Windows.Forms.TabPage()
        Me.tblOther = New System.Windows.Forms.TableLayoutPanel()
        Me.gbTheme = New System.Windows.Forms.GroupBox()
        Me.tblTheme = New System.Windows.Forms.TableLayoutPanel()
        Me.btnRemoveTheme = New System.Windows.Forms.Button()
        Me.btnLocalThemePlay = New System.Windows.Forms.Button()
        Me.txtLocalTheme = New System.Windows.Forms.TextBox()
        Me.btnSetThemeLocal = New System.Windows.Forms.Button()
        Me.btnSetThemeScrape = New System.Windows.Forms.Button()
        Me.btnSetThemeDL = New System.Windows.Forms.Button()
        Me.tpImages = New System.Windows.Forms.TabPage()
        Me.tblImages = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlExtrafanarts = New System.Windows.Forms.Panel()
        Me.tblExtrafanarts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnRemoveExtrafanarts = New System.Windows.Forms.Button()
        Me.lblExtrafanarts = New System.Windows.Forms.Label()
        Me.pnlExtrafanartsList = New System.Windows.Forms.Panel()
        Me.btnLocalExtrafanarts = New System.Windows.Forms.Button()
        Me.btnRefreshExtrafanarts = New System.Windows.Forms.Button()
        Me.btnScrapeExtrafanarts = New System.Windows.Forms.Button()
        Me.btnDLExtrafanarts = New System.Windows.Forms.Button()
        Me.btnClipboardExtrafanarts = New System.Windows.Forms.Button()
        Me.pnlPoster = New System.Windows.Forms.Panel()
        Me.tblPoster = New System.Windows.Forms.TableLayoutPanel()
        Me.pbPoster = New System.Windows.Forms.PictureBox()
        Me.lblPoster = New System.Windows.Forms.Label()
        Me.btnLocalPoster = New System.Windows.Forms.Button()
        Me.btnScrapePoster = New System.Windows.Forms.Button()
        Me.lblSizePoster = New System.Windows.Forms.Label()
        Me.btnDLPoster = New System.Windows.Forms.Button()
        Me.btnRemovePoster = New System.Windows.Forms.Button()
        Me.btnClipboardPoster = New System.Windows.Forms.Button()
        Me.pnlCharacterArt = New System.Windows.Forms.Panel()
        Me.tblCharacterArt = New System.Windows.Forms.TableLayoutPanel()
        Me.pbCharacterArt = New System.Windows.Forms.PictureBox()
        Me.lblCharacterArt = New System.Windows.Forms.Label()
        Me.btnLocalCharacterArt = New System.Windows.Forms.Button()
        Me.btnScrapeCharacterArt = New System.Windows.Forms.Button()
        Me.lblSizeCharacterArt = New System.Windows.Forms.Label()
        Me.btnDLCharacterArt = New System.Windows.Forms.Button()
        Me.btnRemoveCharacterArt = New System.Windows.Forms.Button()
        Me.btnClipboardCharacterArt = New System.Windows.Forms.Button()
        Me.pnlClearLogo = New System.Windows.Forms.Panel()
        Me.tblClearLogo = New System.Windows.Forms.TableLayoutPanel()
        Me.pbClearLogo = New System.Windows.Forms.PictureBox()
        Me.lblClearLogo = New System.Windows.Forms.Label()
        Me.btnLocalClearLogo = New System.Windows.Forms.Button()
        Me.btnScrapeClearLogo = New System.Windows.Forms.Button()
        Me.lblSizeClearLogo = New System.Windows.Forms.Label()
        Me.btnDLClearLogo = New System.Windows.Forms.Button()
        Me.btnRemoveClearLogo = New System.Windows.Forms.Button()
        Me.btnClipboardClearLogo = New System.Windows.Forms.Button()
        Me.pnlFanart = New System.Windows.Forms.Panel()
        Me.tblFanart = New System.Windows.Forms.TableLayoutPanel()
        Me.pbFanart = New System.Windows.Forms.PictureBox()
        Me.lblFanart = New System.Windows.Forms.Label()
        Me.btnLocalFanart = New System.Windows.Forms.Button()
        Me.btnScrapeFanart = New System.Windows.Forms.Button()
        Me.lblSizeFanart = New System.Windows.Forms.Label()
        Me.btnDLFanart = New System.Windows.Forms.Button()
        Me.btnRemoveFanart = New System.Windows.Forms.Button()
        Me.btnClipboardFanart = New System.Windows.Forms.Button()
        Me.pnlLandscape = New System.Windows.Forms.Panel()
        Me.tblLandscape = New System.Windows.Forms.TableLayoutPanel()
        Me.pbLandscape = New System.Windows.Forms.PictureBox()
        Me.lblLandscape = New System.Windows.Forms.Label()
        Me.btnLocalLandscape = New System.Windows.Forms.Button()
        Me.btnScrapeLandscape = New System.Windows.Forms.Button()
        Me.lblSizeLandscape = New System.Windows.Forms.Label()
        Me.btnDLLandscape = New System.Windows.Forms.Button()
        Me.btnRemoveLandscape = New System.Windows.Forms.Button()
        Me.btnClipboardLandscape = New System.Windows.Forms.Button()
        Me.pnlBanner = New System.Windows.Forms.Panel()
        Me.tblBanner = New System.Windows.Forms.TableLayoutPanel()
        Me.pbBanner = New System.Windows.Forms.PictureBox()
        Me.lblBanner = New System.Windows.Forms.Label()
        Me.btnLocalBanner = New System.Windows.Forms.Button()
        Me.btnScrapeBanner = New System.Windows.Forms.Button()
        Me.lblSizeBanner = New System.Windows.Forms.Label()
        Me.btnDLBanner = New System.Windows.Forms.Button()
        Me.btnRemoveBanner = New System.Windows.Forms.Button()
        Me.btnClipboardBanner = New System.Windows.Forms.Button()
        Me.pnlClearArt = New System.Windows.Forms.Panel()
        Me.tblClearArt = New System.Windows.Forms.TableLayoutPanel()
        Me.pbClearArt = New System.Windows.Forms.PictureBox()
        Me.lblClearArt = New System.Windows.Forms.Label()
        Me.btnLocalClearArt = New System.Windows.Forms.Button()
        Me.btnScrapeClearArt = New System.Windows.Forms.Button()
        Me.lblSizeClearArt = New System.Windows.Forms.Label()
        Me.btnDLClearArt = New System.Windows.Forms.Button()
        Me.btnRemoveClearArt = New System.Windows.Forms.Button()
        Me.btnClipboardClearArt = New System.Windows.Forms.Button()
        Me.pnlKeyart = New System.Windows.Forms.Panel()
        Me.tblKeyart = New System.Windows.Forms.TableLayoutPanel()
        Me.pbKeyart = New System.Windows.Forms.PictureBox()
        Me.lblKeyart = New System.Windows.Forms.Label()
        Me.btnLocalKeyart = New System.Windows.Forms.Button()
        Me.btnScrapeKeyart = New System.Windows.Forms.Button()
        Me.lblSizeKeyart = New System.Windows.Forms.Label()
        Me.btnDLKeyart = New System.Windows.Forms.Button()
        Me.btnRemoveKeyart = New System.Windows.Forms.Button()
        Me.btnClipboardKeyart = New System.Windows.Forms.Button()
        Me.pnlImagesRight = New System.Windows.Forms.Panel()
        Me.tblImagesRight = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlTop.SuspendLayout()
        Me.tblTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.tcEdit.SuspendLayout()
        Me.tpDetails.SuspendLayout()
        Me.tblDetails.SuspendLayout()
        CType(Me.dgvCertifications, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvRatings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpDetails2.SuspendLayout()
        Me.tblDetails2.SuspendLayout()
        CType(Me.dgvUniqueIds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCountries, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvStudios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpCastCrew.SuspendLayout()
        Me.tblCastCrew.SuspendLayout()
        CType(Me.dgvCreators, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpOther.SuspendLayout()
        Me.tblOther.SuspendLayout()
        Me.gbTheme.SuspendLayout()
        Me.tblTheme.SuspendLayout()
        Me.tpImages.SuspendLayout()
        Me.tblImages.SuspendLayout()
        Me.pnlExtrafanarts.SuspendLayout()
        Me.tblExtrafanarts.SuspendLayout()
        Me.pnlPoster.SuspendLayout()
        Me.tblPoster.SuspendLayout()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCharacterArt.SuspendLayout()
        Me.tblCharacterArt.SuspendLayout()
        CType(Me.pbCharacterArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlClearLogo.SuspendLayout()
        Me.tblClearLogo.SuspendLayout()
        CType(Me.pbClearLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFanart.SuspendLayout()
        Me.tblFanart.SuspendLayout()
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLandscape.SuspendLayout()
        Me.tblLandscape.SuspendLayout()
        CType(Me.pbLandscape, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBanner.SuspendLayout()
        Me.tblBanner.SuspendLayout()
        CType(Me.pbBanner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlClearArt.SuspendLayout()
        Me.tblClearArt.SuspendLayout()
        CType(Me.pbClearArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlKeyart.SuspendLayout()
        Me.tblKeyart.SuspendLayout()
        CType(Me.pbKeyart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlImagesRight.SuspendLayout()
        Me.SuspendLayout()
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
        Me.pnlTop.Size = New System.Drawing.Size(1284, 56)
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
        Me.tblTop.Size = New System.Drawing.Size(1282, 54)
        Me.tblTop.TabIndex = 2
        '
        'pbTopLogo
        '
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
        Me.lblTopDetails.Size = New System.Drawing.Size(201, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Edit the details for the selected show."
        '
        'lblTopTitle
        '
        Me.lblTopTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(57, 0)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(127, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Edit Show"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(1205, 30)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Me.btnOK.Enabled = False
        Me.btnOK.Location = New System.Drawing.Point(1129, 30)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(70, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        '
        'cbEpisodeOrdering
        '
        Me.cbEpisodeOrdering.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbEpisodeOrdering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEpisodeOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbEpisodeOrdering.FormattingEnabled = True
        Me.cbEpisodeOrdering.Location = New System.Drawing.Point(532, 3)
        Me.cbEpisodeOrdering.Name = "cbEpisodeOrdering"
        Me.cbEpisodeOrdering.Size = New System.Drawing.Size(166, 21)
        Me.cbEpisodeOrdering.TabIndex = 5
        '
        'lblEpisodeOrdering
        '
        Me.lblEpisodeOrdering.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblEpisodeOrdering.AutoSize = True
        Me.lblEpisodeOrdering.Location = New System.Drawing.Point(423, 7)
        Me.lblEpisodeOrdering.Name = "lblEpisodeOrdering"
        Me.lblEpisodeOrdering.Size = New System.Drawing.Size(101, 13)
        Me.lblEpisodeOrdering.TabIndex = 4
        Me.lblEpisodeOrdering.Text = "Episode Ordering:"
        '
        'cbEpisodeSorting
        '
        Me.cbEpisodeSorting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbEpisodeSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEpisodeSorting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbEpisodeSorting.FormattingEnabled = True
        Me.cbEpisodeSorting.Location = New System.Drawing.Point(532, 32)
        Me.cbEpisodeSorting.Name = "cbEpisodeSorting"
        Me.cbEpisodeSorting.Size = New System.Drawing.Size(166, 21)
        Me.cbEpisodeSorting.TabIndex = 5
        '
        'lblEpisodeSorting
        '
        Me.lblEpisodeSorting.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblEpisodeSorting.AutoSize = True
        Me.lblEpisodeSorting.Location = New System.Drawing.Point(423, 35)
        Me.lblEpisodeSorting.Name = "lblEpisodeSorting"
        Me.lblEpisodeSorting.Size = New System.Drawing.Size(103, 13)
        Me.lblEpisodeSorting.TabIndex = 4
        Me.lblEpisodeSorting.Text = "Episode Sorted by:"
        Me.lblEpisodeSorting.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLanguage
        '
        Me.lblLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblLanguage.AutoSize = True
        Me.lblLanguage.Location = New System.Drawing.Point(3, 35)
        Me.lblLanguage.Name = "lblLanguage"
        Me.lblLanguage.Size = New System.Drawing.Size(61, 13)
        Me.lblLanguage.TabIndex = 4
        Me.lblLanguage.Text = "Language:"
        '
        'cbSourceLanguage
        '
        Me.cbSourceLanguage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbSourceLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSourceLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSourceLanguage.Location = New System.Drawing.Point(71, 32)
        Me.cbSourceLanguage.Name = "cbSourceLanguage"
        Me.cbSourceLanguage.Size = New System.Drawing.Size(172, 21)
        Me.cbSourceLanguage.TabIndex = 9
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 753)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1284, 56)
        Me.pnlBottom.TabIndex = 11
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 12
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnChange, 8, 1)
        Me.tblBottom.Controls.Add(Me.btnRescrape, 7, 1)
        Me.tblBottom.Controls.Add(Me.btnCancel, 11, 1)
        Me.tblBottom.Controls.Add(Me.chkLocked, 0, 0)
        Me.tblBottom.Controls.Add(Me.btnOK, 10, 1)
        Me.tblBottom.Controls.Add(Me.chkMarked, 1, 0)
        Me.tblBottom.Controls.Add(Me.cbEpisodeSorting, 5, 1)
        Me.tblBottom.Controls.Add(Me.lblEpisodeSorting, 4, 1)
        Me.tblBottom.Controls.Add(Me.cbEpisodeOrdering, 5, 0)
        Me.tblBottom.Controls.Add(Me.cbSourceLanguage, 1, 1)
        Me.tblBottom.Controls.Add(Me.chkMarkedCustom1, 2, 0)
        Me.tblBottom.Controls.Add(Me.lblEpisodeOrdering, 4, 0)
        Me.tblBottom.Controls.Add(Me.chkMarkedCustom3, 3, 0)
        Me.tblBottom.Controls.Add(Me.chkMarkedCustom2, 2, 1)
        Me.tblBottom.Controls.Add(Me.lblLanguage, 0, 1)
        Me.tblBottom.Controls.Add(Me.chkMarkedCustom4, 3, 1)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 2
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.Size = New System.Drawing.Size(1284, 56)
        Me.tblBottom.TabIndex = 0
        '
        'btnChange
        '
        Me.btnChange.AutoSize = True
        Me.btnChange.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnChange.Image = CType(resources.GetObject("btnChange.Image"), System.Drawing.Image)
        Me.btnChange.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnChange.Location = New System.Drawing.Point(912, 30)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(107, 23)
        Me.btnChange.TabIndex = 11
        Me.btnChange.Text = "Change TV Show"
        Me.btnChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnChange.UseVisualStyleBackColor = True
        '
        'btnRescrape
        '
        Me.btnRescrape.AutoSize = True
        Me.btnRescrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRescrape.Image = CType(resources.GetObject("btnRescrape.Image"), System.Drawing.Image)
        Me.btnRescrape.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRescrape.Location = New System.Drawing.Point(808, 30)
        Me.btnRescrape.Name = "btnRescrape"
        Me.btnRescrape.Size = New System.Drawing.Size(98, 23)
        Me.btnRescrape.TabIndex = 10
        Me.btnRescrape.Text = "Re-scrape"
        Me.btnRescrape.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRescrape.UseVisualStyleBackColor = True
        '
        'chkLocked
        '
        Me.chkLocked.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkLocked.AutoSize = True
        Me.chkLocked.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLocked.Location = New System.Drawing.Point(3, 7)
        Me.chkLocked.Name = "chkLocked"
        Me.chkLocked.Size = New System.Drawing.Size(62, 17)
        Me.chkLocked.TabIndex = 0
        Me.chkLocked.Text = "Locked"
        Me.chkLocked.UseVisualStyleBackColor = True
        '
        'chkMarked
        '
        Me.chkMarked.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkMarked.AutoSize = True
        Me.chkMarked.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarked.Location = New System.Drawing.Point(71, 7)
        Me.chkMarked.Name = "chkMarked"
        Me.chkMarked.Size = New System.Drawing.Size(65, 17)
        Me.chkMarked.TabIndex = 1
        Me.chkMarked.Text = "Marked"
        Me.chkMarked.UseVisualStyleBackColor = True
        '
        'chkMarkedCustom1
        '
        Me.chkMarkedCustom1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkMarkedCustom1.AutoSize = True
        Me.chkMarkedCustom1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarkedCustom1.Location = New System.Drawing.Point(249, 7)
        Me.chkMarkedCustom1.Name = "chkMarkedCustom1"
        Me.chkMarkedCustom1.Size = New System.Drawing.Size(81, 17)
        Me.chkMarkedCustom1.TabIndex = 3
        Me.chkMarkedCustom1.Text = "Custom #1"
        Me.chkMarkedCustom1.UseVisualStyleBackColor = True
        Me.chkMarkedCustom1.Visible = False
        '
        'chkMarkedCustom3
        '
        Me.chkMarkedCustom3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkMarkedCustom3.AutoSize = True
        Me.chkMarkedCustom3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarkedCustom3.Location = New System.Drawing.Point(336, 7)
        Me.chkMarkedCustom3.Name = "chkMarkedCustom3"
        Me.chkMarkedCustom3.Size = New System.Drawing.Size(81, 17)
        Me.chkMarkedCustom3.TabIndex = 5
        Me.chkMarkedCustom3.Text = "Custom #3"
        Me.chkMarkedCustom3.UseVisualStyleBackColor = True
        Me.chkMarkedCustom3.Visible = False
        '
        'chkMarkedCustom2
        '
        Me.chkMarkedCustom2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkMarkedCustom2.AutoSize = True
        Me.chkMarkedCustom2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarkedCustom2.Location = New System.Drawing.Point(249, 36)
        Me.chkMarkedCustom2.Name = "chkMarkedCustom2"
        Me.chkMarkedCustom2.Size = New System.Drawing.Size(81, 17)
        Me.chkMarkedCustom2.TabIndex = 4
        Me.chkMarkedCustom2.Text = "Custom #2"
        Me.chkMarkedCustom2.UseVisualStyleBackColor = True
        Me.chkMarkedCustom2.Visible = False
        '
        'chkMarkedCustom4
        '
        Me.chkMarkedCustom4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkMarkedCustom4.AutoSize = True
        Me.chkMarkedCustom4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarkedCustom4.Location = New System.Drawing.Point(336, 36)
        Me.chkMarkedCustom4.Name = "chkMarkedCustom4"
        Me.chkMarkedCustom4.Size = New System.Drawing.Size(81, 17)
        Me.chkMarkedCustom4.TabIndex = 6
        Me.chkMarkedCustom4.Text = "Custom #4"
        Me.chkMarkedCustom4.UseVisualStyleBackColor = True
        Me.chkMarkedCustom4.Visible = False
        '
        'StatusStrip
        '
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(18, 18)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslFilename, Me.tsslSpring, Me.tsslStatus, Me.tspbStatus})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 809)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1284, 22)
        Me.StatusStrip.TabIndex = 12
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'tsslFilename
        '
        Me.tsslFilename.Name = "tsslFilename"
        Me.tsslFilename.Size = New System.Drawing.Size(55, 17)
        Me.tsslFilename.Text = "Filename"
        '
        'tsslSpring
        '
        Me.tsslSpring.Name = "tsslSpring"
        Me.tsslSpring.Size = New System.Drawing.Size(1214, 17)
        Me.tsslSpring.Spring = True
        '
        'tsslStatus
        '
        Me.tsslStatus.Name = "tsslStatus"
        Me.tsslStatus.Size = New System.Drawing.Size(39, 17)
        Me.tsslStatus.Text = "Status"
        Me.tsslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.tsslStatus.Visible = False
        '
        'tspbStatus
        '
        Me.tspbStatus.Name = "tspbStatus"
        Me.tspbStatus.Size = New System.Drawing.Size(100, 16)
        Me.tspbStatus.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlMain.Controls.Add(Me.tcEdit)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1284, 697)
        Me.pnlMain.TabIndex = 78
        '
        'tcEdit
        '
        Me.tcEdit.Controls.Add(Me.tpDetails)
        Me.tcEdit.Controls.Add(Me.tpDetails2)
        Me.tcEdit.Controls.Add(Me.tpCastCrew)
        Me.tcEdit.Controls.Add(Me.tpOther)
        Me.tcEdit.Controls.Add(Me.tpImages)
        Me.tcEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcEdit.Location = New System.Drawing.Point(0, 0)
        Me.tcEdit.Name = "tcEdit"
        Me.tcEdit.SelectedIndex = 0
        Me.tcEdit.Size = New System.Drawing.Size(1284, 697)
        Me.tcEdit.TabIndex = 0
        '
        'tpDetails
        '
        Me.tpDetails.Controls.Add(Me.tblDetails)
        Me.tpDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpDetails.Name = "tpDetails"
        Me.tpDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDetails.Size = New System.Drawing.Size(1276, 671)
        Me.tpDetails.TabIndex = 0
        Me.tpDetails.Text = "Details"
        '
        'tblDetails
        '
        Me.tblDetails.AutoScroll = True
        Me.tblDetails.AutoSize = True
        Me.tblDetails.ColumnCount = 8
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.Controls.Add(Me.lblTitle, 0, 0)
        Me.tblDetails.Controls.Add(Me.txtTitle, 1, 0)
        Me.tblDetails.Controls.Add(Me.lblOriginalTitle, 0, 1)
        Me.tblDetails.Controls.Add(Me.txtOriginalTitle, 1, 1)
        Me.tblDetails.Controls.Add(Me.lblSortTilte, 0, 2)
        Me.tblDetails.Controls.Add(Me.txtSortTitle, 1, 2)
        Me.tblDetails.Controls.Add(Me.lblTagline, 0, 3)
        Me.tblDetails.Controls.Add(Me.txtTagline, 1, 3)
        Me.tblDetails.Controls.Add(Me.lblPlot, 0, 4)
        Me.tblDetails.Controls.Add(Me.txtPlot, 1, 4)
        Me.tblDetails.Controls.Add(Me.lblPremiered, 3, 0)
        Me.tblDetails.Controls.Add(Me.dtpPremiered, 4, 0)
        Me.tblDetails.Controls.Add(Me.cbUserRating, 4, 3)
        Me.tblDetails.Controls.Add(Me.lblRatings, 3, 4)
        Me.tblDetails.Controls.Add(Me.lblCertifications, 0, 6)
        Me.tblDetails.Controls.Add(Me.dgvCertifications, 1, 6)
        Me.tblDetails.Controls.Add(Me.dgvRatings, 4, 4)
        Me.tblDetails.Controls.Add(Me.lblMPAA, 3, 6)
        Me.tblDetails.Controls.Add(Me.lbMPAA, 4, 6)
        Me.tblDetails.Controls.Add(Me.btnCertificationsAsMPAARating, 3, 8)
        Me.tblDetails.Controls.Add(Me.lblMPAADesc, 6, 6)
        Me.tblDetails.Controls.Add(Me.txtMPAADescription, 6, 7)
        Me.tblDetails.Controls.Add(Me.txtMPAA, 4, 8)
        Me.tblDetails.Controls.Add(Me.lblUserRating, 3, 3)
        Me.tblDetails.Controls.Add(Me.lblRuntime, 3, 2)
        Me.tblDetails.Controls.Add(Me.txtRuntime, 4, 2)
        Me.tblDetails.Controls.Add(Me.lblStatus, 3, 1)
        Me.tblDetails.Controls.Add(Me.txtStatus, 4, 1)
        Me.tblDetails.Controls.Add(Me.lblUserNote, 0, 12)
        Me.tblDetails.Controls.Add(Me.txtUserNote, 1, 12)
        Me.tblDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblDetails.Location = New System.Drawing.Point(3, 3)
        Me.tblDetails.Name = "tblDetails"
        Me.tblDetails.RowCount = 15
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblDetails.Size = New System.Drawing.Size(1270, 665)
        Me.tblDetails.TabIndex = 78
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(49, 7)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(32, 13)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Title:"
        '
        'txtTitle
        '
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtTitle.Location = New System.Drawing.Point(87, 3)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(400, 22)
        Me.txtTitle.TabIndex = 0
        '
        'lblOriginalTitle
        '
        Me.lblOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblOriginalTitle.AutoSize = True
        Me.lblOriginalTitle.Location = New System.Drawing.Point(4, 35)
        Me.lblOriginalTitle.Name = "lblOriginalTitle"
        Me.lblOriginalTitle.Size = New System.Drawing.Size(77, 13)
        Me.lblOriginalTitle.TabIndex = 2
        Me.lblOriginalTitle.Text = "Original Title:"
        '
        'txtOriginalTitle
        '
        Me.txtOriginalTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtOriginalTitle.Location = New System.Drawing.Point(87, 31)
        Me.txtOriginalTitle.Name = "txtOriginalTitle"
        Me.txtOriginalTitle.Size = New System.Drawing.Size(400, 22)
        Me.txtOriginalTitle.TabIndex = 1
        '
        'lblSortTilte
        '
        Me.lblSortTilte.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblSortTilte.AutoSize = True
        Me.lblSortTilte.Location = New System.Drawing.Point(25, 63)
        Me.lblSortTilte.Name = "lblSortTilte"
        Me.lblSortTilte.Size = New System.Drawing.Size(56, 13)
        Me.lblSortTilte.TabIndex = 4
        Me.lblSortTilte.Text = "Sort Title:"
        '
        'txtSortTitle
        '
        Me.txtSortTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtSortTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSortTitle.Location = New System.Drawing.Point(87, 59)
        Me.txtSortTitle.Name = "txtSortTitle"
        Me.txtSortTitle.Size = New System.Drawing.Size(400, 22)
        Me.txtSortTitle.TabIndex = 2
        '
        'lblTagline
        '
        Me.lblTagline.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblTagline.AutoSize = True
        Me.lblTagline.Location = New System.Drawing.Point(34, 91)
        Me.lblTagline.Name = "lblTagline"
        Me.lblTagline.Size = New System.Drawing.Size(47, 13)
        Me.lblTagline.TabIndex = 6
        Me.lblTagline.Text = "Tagline:"
        '
        'txtTagline
        '
        Me.txtTagline.BackColor = System.Drawing.SystemColors.Window
        Me.txtTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTagline.Location = New System.Drawing.Point(87, 87)
        Me.txtTagline.Name = "txtTagline"
        Me.txtTagline.Size = New System.Drawing.Size(400, 22)
        Me.txtTagline.TabIndex = 3
        '
        'lblPlot
        '
        Me.lblPlot.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblPlot.AutoSize = True
        Me.lblPlot.Location = New System.Drawing.Point(51, 119)
        Me.lblPlot.Name = "lblPlot"
        Me.lblPlot.Size = New System.Drawing.Size(30, 13)
        Me.lblPlot.TabIndex = 27
        Me.lblPlot.Text = "Plot:"
        '
        'txtPlot
        '
        Me.txtPlot.AcceptsReturn = True
        Me.txtPlot.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtPlot.Location = New System.Drawing.Point(87, 115)
        Me.txtPlot.Multiline = True
        Me.txtPlot.Name = "txtPlot"
        Me.tblDetails.SetRowSpan(Me.txtPlot, 2)
        Me.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlot.Size = New System.Drawing.Size(400, 218)
        Me.txtPlot.TabIndex = 4
        '
        'lblPremiered
        '
        Me.lblPremiered.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblPremiered.AutoSize = True
        Me.lblPremiered.Location = New System.Drawing.Point(528, 7)
        Me.lblPremiered.Name = "lblPremiered"
        Me.lblPremiered.Size = New System.Drawing.Size(61, 13)
        Me.lblPremiered.TabIndex = 13
        Me.lblPremiered.Text = "Premiered:"
        '
        'dtpPremiered
        '
        Me.dtpPremiered.Checked = False
        Me.dtpPremiered.CustomFormat = ""
        Me.dtpPremiered.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpPremiered.Location = New System.Drawing.Point(595, 3)
        Me.dtpPremiered.Name = "dtpPremiered"
        Me.dtpPremiered.ShowCheckBox = True
        Me.dtpPremiered.Size = New System.Drawing.Size(120, 22)
        Me.dtpPremiered.TabIndex = 6
        Me.dtpPremiered.Value = New Date(2021, 1, 1, 0, 0, 0, 0)
        '
        'cbUserRating
        '
        Me.cbUserRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbUserRating.FormattingEnabled = True
        Me.cbUserRating.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cbUserRating.Location = New System.Drawing.Point(595, 87)
        Me.cbUserRating.Name = "cbUserRating"
        Me.cbUserRating.Size = New System.Drawing.Size(120, 21)
        Me.cbUserRating.TabIndex = 11
        '
        'lblRatings
        '
        Me.lblRatings.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblRatings.AutoSize = True
        Me.lblRatings.Location = New System.Drawing.Point(540, 119)
        Me.lblRatings.Name = "lblRatings"
        Me.lblRatings.Size = New System.Drawing.Size(49, 13)
        Me.lblRatings.TabIndex = 10
        Me.lblRatings.Text = "Ratings:"
        '
        'lblCertifications
        '
        Me.lblCertifications.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblCertifications.AutoSize = True
        Me.lblCertifications.Location = New System.Drawing.Point(3, 339)
        Me.lblCertifications.Name = "lblCertifications"
        Me.lblCertifications.Size = New System.Drawing.Size(78, 13)
        Me.lblCertifications.TabIndex = 45
        Me.lblCertifications.Text = "Certifications:"
        '
        'dgvCertifications
        '
        Me.dgvCertifications.AllowUserToResizeColumns = False
        Me.dgvCertifications.AllowUserToResizeRows = False
        Me.dgvCertifications.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvCertifications.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvCertifications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCertifications.ColumnHeadersVisible = False
        Me.dgvCertifications.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCertificationsName})
        Me.dgvCertifications.Location = New System.Drawing.Point(87, 339)
        Me.dgvCertifications.Name = "dgvCertifications"
        Me.dgvCertifications.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.tblDetails.SetRowSpan(Me.dgvCertifications, 3)
        Me.dgvCertifications.Size = New System.Drawing.Size(400, 136)
        Me.dgvCertifications.TabIndex = 16
        '
        'colCertificationsName
        '
        Me.colCertificationsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colCertificationsName.HeaderText = "Name"
        Me.colCertificationsName.Name = "colCertificationsName"
        Me.colCertificationsName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'dgvRatings
        '
        Me.dgvRatings.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvRatings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvRatings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRatings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRatingsDefault, Me.colRatingsSource, Me.colRatingsValue, Me.colRatingsMax, Me.colRatingsVotes})
        Me.tblDetails.SetColumnSpan(Me.dgvRatings, 3)
        Me.dgvRatings.Location = New System.Drawing.Point(595, 115)
        Me.dgvRatings.Name = "dgvRatings"
        Me.dgvRatings.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.tblDetails.SetRowSpan(Me.dgvRatings, 2)
        Me.dgvRatings.Size = New System.Drawing.Size(400, 218)
        Me.dgvRatings.TabIndex = 13
        '
        'colRatingsDefault
        '
        Me.colRatingsDefault.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colRatingsDefault.HeaderText = "Default"
        Me.colRatingsDefault.Name = "colRatingsDefault"
        Me.colRatingsDefault.Width = 51
        '
        'colRatingsSource
        '
        Me.colRatingsSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colRatingsSource.HeaderText = "Source"
        Me.colRatingsSource.Name = "colRatingsSource"
        '
        'colRatingsValue
        '
        Me.colRatingsValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colRatingsValue.DefaultCellStyle = DataGridViewCellStyle4
        Me.colRatingsValue.HeaderText = "Value"
        Me.colRatingsValue.Name = "colRatingsValue"
        Me.colRatingsValue.Width = 60
        '
        'colRatingsMax
        '
        Me.colRatingsMax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colRatingsMax.DefaultCellStyle = DataGridViewCellStyle5
        Me.colRatingsMax.HeaderText = "Max"
        Me.colRatingsMax.Name = "colRatingsMax"
        Me.colRatingsMax.Width = 53
        '
        'colRatingsVotes
        '
        Me.colRatingsVotes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colRatingsVotes.DefaultCellStyle = DataGridViewCellStyle6
        Me.colRatingsVotes.HeaderText = "Votes"
        Me.colRatingsVotes.Name = "colRatingsVotes"
        Me.colRatingsVotes.Width = 60
        '
        'lblMPAA
        '
        Me.lblMPAA.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblMPAA.AutoSize = True
        Me.lblMPAA.Location = New System.Drawing.Point(513, 339)
        Me.lblMPAA.Name = "lblMPAA"
        Me.lblMPAA.Size = New System.Drawing.Size(76, 13)
        Me.lblMPAA.TabIndex = 36
        Me.lblMPAA.Text = "MPAA Rating:"
        '
        'lbMPAA
        '
        Me.tblDetails.SetColumnSpan(Me.lbMPAA, 2)
        Me.lbMPAA.FormattingEnabled = True
        Me.lbMPAA.Location = New System.Drawing.Point(595, 339)
        Me.lbMPAA.Name = "lbMPAA"
        Me.tblDetails.SetRowSpan(Me.lbMPAA, 2)
        Me.lbMPAA.Size = New System.Drawing.Size(200, 108)
        Me.lbMPAA.TabIndex = 14
        '
        'btnCertificationsAsMPAARating
        '
        Me.btnCertificationsAsMPAARating.Image = CType(resources.GetObject("btnCertificationsAsMPAARating.Image"), System.Drawing.Image)
        Me.btnCertificationsAsMPAARating.Location = New System.Drawing.Point(513, 453)
        Me.btnCertificationsAsMPAARating.Name = "btnCertificationsAsMPAARating"
        Me.btnCertificationsAsMPAARating.Size = New System.Drawing.Size(23, 23)
        Me.btnCertificationsAsMPAARating.TabIndex = 36
        Me.btnCertificationsAsMPAARating.UseVisualStyleBackColor = True
        '
        'lblMPAADesc
        '
        Me.lblMPAADesc.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMPAADesc.AutoSize = True
        Me.lblMPAADesc.Location = New System.Drawing.Point(801, 339)
        Me.lblMPAADesc.Name = "lblMPAADesc"
        Me.lblMPAADesc.Size = New System.Drawing.Size(138, 13)
        Me.lblMPAADesc.TabIndex = 38
        Me.lblMPAADesc.Text = "MPAA Rating Description:"
        '
        'txtMPAADescription
        '
        Me.txtMPAADescription.Location = New System.Drawing.Point(801, 359)
        Me.txtMPAADescription.Multiline = True
        Me.txtMPAADescription.Name = "txtMPAADescription"
        Me.txtMPAADescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMPAADescription.Size = New System.Drawing.Size(194, 88)
        Me.txtMPAADescription.TabIndex = 15
        '
        'txtMPAA
        '
        Me.tblDetails.SetColumnSpan(Me.txtMPAA, 3)
        Me.txtMPAA.Location = New System.Drawing.Point(595, 453)
        Me.txtMPAA.Name = "txtMPAA"
        Me.txtMPAA.Size = New System.Drawing.Size(400, 22)
        Me.txtMPAA.TabIndex = 60
        Me.txtMPAA.WatermarkColor = System.Drawing.Color.Gray
        Me.txtMPAA.WatermarkText = "MPAA Rating + Description"
        '
        'lblUserRating
        '
        Me.lblUserRating.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblUserRating.AutoSize = True
        Me.lblUserRating.Location = New System.Drawing.Point(519, 91)
        Me.lblUserRating.Name = "lblUserRating"
        Me.lblUserRating.Size = New System.Drawing.Size(70, 13)
        Me.lblUserRating.TabIndex = 19
        Me.lblUserRating.Text = "User Rating:"
        '
        'lblRuntime
        '
        Me.lblRuntime.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblRuntime.AutoSize = True
        Me.lblRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRuntime.Location = New System.Drawing.Point(536, 63)
        Me.lblRuntime.Name = "lblRuntime"
        Me.lblRuntime.Size = New System.Drawing.Size(53, 13)
        Me.lblRuntime.TabIndex = 59
        Me.lblRuntime.Text = "Runtime:"
        '
        'txtRuntime
        '
        Me.txtRuntime.BackColor = System.Drawing.SystemColors.Window
        Me.txtRuntime.Location = New System.Drawing.Point(595, 59)
        Me.txtRuntime.Name = "txtRuntime"
        Me.txtRuntime.Size = New System.Drawing.Size(120, 22)
        Me.txtRuntime.TabIndex = 10
        Me.txtRuntime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(547, 35)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(42, 13)
        Me.lblStatus.TabIndex = 59
        Me.lblStatus.Text = "Status:"
        '
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus.Location = New System.Drawing.Point(595, 31)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(120, 22)
        Me.txtStatus.TabIndex = 10
        '
        'lblUserNote
        '
        Me.lblUserNote.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblUserNote.AutoSize = True
        Me.lblUserNote.Location = New System.Drawing.Point(46, 486)
        Me.lblUserNote.Name = "lblUserNote"
        Me.lblUserNote.Size = New System.Drawing.Size(35, 13)
        Me.lblUserNote.TabIndex = 45
        Me.lblUserNote.Text = "Note:"
        '
        'txtUserNote
        '
        Me.txtUserNote.Location = New System.Drawing.Point(87, 482)
        Me.txtUserNote.Name = "txtUserNote"
        Me.txtUserNote.Size = New System.Drawing.Size(400, 22)
        Me.txtUserNote.TabIndex = 62
        '
        'tpDetails2
        '
        Me.tpDetails2.Controls.Add(Me.tblDetails2)
        Me.tpDetails2.Location = New System.Drawing.Point(4, 22)
        Me.tpDetails2.Name = "tpDetails2"
        Me.tpDetails2.Size = New System.Drawing.Size(1276, 671)
        Me.tpDetails2.TabIndex = 18
        Me.tpDetails2.Text = "Details 2"
        '
        'tblDetails2
        '
        Me.tblDetails2.AutoSize = True
        Me.tblDetails2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblDetails2.ColumnCount = 7
        Me.tblDetails2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblDetails2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails2.Controls.Add(Me.lblDateAdded, 0, 0)
        Me.tblDetails2.Controls.Add(Me.dtpDateAdded_Date, 1, 0)
        Me.tblDetails2.Controls.Add(Me.chkWatched, 0, 1)
        Me.tblDetails2.Controls.Add(Me.dtpLastPlayed_Date, 1, 1)
        Me.tblDetails2.Controls.Add(Me.dtpDateAdded_Time, 2, 0)
        Me.tblDetails2.Controls.Add(Me.dtpLastPlayed_Time, 2, 1)
        Me.tblDetails2.Controls.Add(Me.lblGenres, 0, 4)
        Me.tblDetails2.Controls.Add(Me.lblUniqueIds, 0, 11)
        Me.tblDetails2.Controls.Add(Me.dgvUniqueIds, 1, 11)
        Me.tblDetails2.Controls.Add(Me.lbGenres, 1, 4)
        Me.tblDetails2.Controls.Add(Me.btnGenres_Add, 0, 5)
        Me.tblDetails2.Controls.Add(Me.btnGenres_Remove, 0, 6)
        Me.tblDetails2.Controls.Add(Me.btnGenres_Up, 0, 7)
        Me.tblDetails2.Controls.Add(Me.btnGenres_Down, 0, 8)
        Me.tblDetails2.Controls.Add(Me.lblStudios, 4, 2)
        Me.tblDetails2.Controls.Add(Me.dgvCountries, 1, 2)
        Me.tblDetails2.Controls.Add(Me.lblCountries, 0, 2)
        Me.tblDetails2.Controls.Add(Me.dgvStudios, 5, 2)
        Me.tblDetails2.Controls.Add(Me.lblTags, 4, 4)
        Me.tblDetails2.Controls.Add(Me.lbTags, 5, 4)
        Me.tblDetails2.Controls.Add(Me.cbGenres, 1, 10)
        Me.tblDetails2.Controls.Add(Me.cbTags, 5, 10)
        Me.tblDetails2.Controls.Add(Me.btnTags_Add, 4, 5)
        Me.tblDetails2.Controls.Add(Me.btnTags_Remove, 4, 6)
        Me.tblDetails2.Controls.Add(Me.btnTags_Up, 4, 7)
        Me.tblDetails2.Controls.Add(Me.btnTags_Down, 4, 8)
        Me.tblDetails2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblDetails2.Location = New System.Drawing.Point(0, 0)
        Me.tblDetails2.Name = "tblDetails2"
        Me.tblDetails2.RowCount = 16
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails2.Size = New System.Drawing.Size(1276, 671)
        Me.tblDetails2.TabIndex = 0
        '
        'lblDateAdded
        '
        Me.lblDateAdded.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblDateAdded.AutoSize = True
        Me.lblDateAdded.Location = New System.Drawing.Point(7, 7)
        Me.lblDateAdded.Name = "lblDateAdded"
        Me.lblDateAdded.Size = New System.Drawing.Size(68, 13)
        Me.lblDateAdded.TabIndex = 0
        Me.lblDateAdded.Text = "Date Added"
        Me.lblDateAdded.Visible = False
        '
        'dtpDateAdded_Date
        '
        Me.dtpDateAdded_Date.CustomFormat = ""
        Me.dtpDateAdded_Date.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateAdded_Date.Location = New System.Drawing.Point(81, 3)
        Me.dtpDateAdded_Date.Name = "dtpDateAdded_Date"
        Me.dtpDateAdded_Date.Size = New System.Drawing.Size(100, 22)
        Me.dtpDateAdded_Date.TabIndex = 31
        Me.dtpDateAdded_Date.Value = New Date(2021, 1, 1, 0, 0, 0, 0)
        Me.dtpDateAdded_Date.Visible = False
        '
        'chkWatched
        '
        Me.chkWatched.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.chkWatched.AutoSize = True
        Me.chkWatched.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkWatched.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWatched.Location = New System.Drawing.Point(3, 33)
        Me.chkWatched.Name = "chkWatched"
        Me.chkWatched.Size = New System.Drawing.Size(72, 17)
        Me.chkWatched.TabIndex = 30
        Me.chkWatched.Text = "Watched"
        Me.chkWatched.UseVisualStyleBackColor = True
        Me.chkWatched.Visible = False
        '
        'dtpLastPlayed_Date
        '
        Me.dtpLastPlayed_Date.CustomFormat = ""
        Me.dtpLastPlayed_Date.Enabled = False
        Me.dtpLastPlayed_Date.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpLastPlayed_Date.Location = New System.Drawing.Point(81, 31)
        Me.dtpLastPlayed_Date.Name = "dtpLastPlayed_Date"
        Me.dtpLastPlayed_Date.Size = New System.Drawing.Size(100, 22)
        Me.dtpLastPlayed_Date.TabIndex = 31
        Me.dtpLastPlayed_Date.Value = New Date(2021, 1, 1, 20, 0, 0, 0)
        Me.dtpLastPlayed_Date.Visible = False
        '
        'dtpDateAdded_Time
        '
        Me.dtpDateAdded_Time.CustomFormat = ""
        Me.dtpDateAdded_Time.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpDateAdded_Time.Location = New System.Drawing.Point(187, 3)
        Me.dtpDateAdded_Time.Name = "dtpDateAdded_Time"
        Me.dtpDateAdded_Time.ShowUpDown = True
        Me.dtpDateAdded_Time.Size = New System.Drawing.Size(100, 22)
        Me.dtpDateAdded_Time.TabIndex = 31
        Me.dtpDateAdded_Time.Value = New Date(2021, 2, 2, 20, 0, 0, 0)
        Me.dtpDateAdded_Time.Visible = False
        '
        'dtpLastPlayed_Time
        '
        Me.dtpLastPlayed_Time.CustomFormat = ""
        Me.dtpLastPlayed_Time.Enabled = False
        Me.dtpLastPlayed_Time.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpLastPlayed_Time.Location = New System.Drawing.Point(187, 31)
        Me.dtpLastPlayed_Time.Name = "dtpLastPlayed_Time"
        Me.dtpLastPlayed_Time.ShowUpDown = True
        Me.dtpLastPlayed_Time.Size = New System.Drawing.Size(100, 22)
        Me.dtpLastPlayed_Time.TabIndex = 31
        Me.dtpLastPlayed_Time.Value = New Date(2021, 2, 2, 20, 0, 0, 0)
        Me.dtpLastPlayed_Time.Visible = False
        '
        'lblGenres
        '
        Me.lblGenres.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblGenres.AutoSize = True
        Me.lblGenres.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblGenres.Location = New System.Drawing.Point(29, 219)
        Me.lblGenres.Name = "lblGenres"
        Me.lblGenres.Size = New System.Drawing.Size(46, 13)
        Me.lblGenres.TabIndex = 54
        Me.lblGenres.Text = "Genres:"
        '
        'lblUniqueIds
        '
        Me.lblUniqueIds.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblUniqueIds.AutoSize = True
        Me.lblUniqueIds.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblUniqueIds.Location = New System.Drawing.Point(8, 399)
        Me.lblUniqueIds.Name = "lblUniqueIds"
        Me.lblUniqueIds.Size = New System.Drawing.Size(67, 13)
        Me.lblUniqueIds.TabIndex = 54
        Me.lblUniqueIds.Text = "Unique IDs:"
        '
        'dgvUniqueIds
        '
        Me.dgvUniqueIds.AllowUserToResizeColumns = False
        Me.dgvUniqueIds.AllowUserToResizeRows = False
        Me.dgvUniqueIds.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvUniqueIds.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvUniqueIds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUniqueIds.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colUniqueIdsDefault, Me.colUniqueIdsType, Me.colUniqueIdsValue})
        Me.tblDetails2.SetColumnSpan(Me.dgvUniqueIds, 2)
        Me.dgvUniqueIds.Location = New System.Drawing.Point(81, 395)
        Me.dgvUniqueIds.Name = "dgvUniqueIds"
        Me.dgvUniqueIds.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.tblDetails2.SetRowSpan(Me.dgvUniqueIds, 2)
        Me.dgvUniqueIds.Size = New System.Drawing.Size(400, 140)
        Me.dgvUniqueIds.TabIndex = 60
        '
        'colUniqueIdsDefault
        '
        Me.colUniqueIdsDefault.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colUniqueIdsDefault.HeaderText = "Default"
        Me.colUniqueIdsDefault.Name = "colUniqueIdsDefault"
        Me.colUniqueIdsDefault.Width = 51
        '
        'colUniqueIdsType
        '
        Me.colUniqueIdsType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colUniqueIdsType.HeaderText = "Typ"
        Me.colUniqueIdsType.Name = "colUniqueIdsType"
        Me.colUniqueIdsType.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colUniqueIdsValue
        '
        Me.colUniqueIdsValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colUniqueIdsValue.HeaderText = "ID"
        Me.colUniqueIdsValue.Name = "colUniqueIdsValue"
        Me.colUniqueIdsValue.Width = 43
        '
        'lbGenres
        '
        Me.tblDetails2.SetColumnSpan(Me.lbGenres, 2)
        Me.lbGenres.FormattingEnabled = True
        Me.lbGenres.Location = New System.Drawing.Point(81, 215)
        Me.lbGenres.Name = "lbGenres"
        Me.tblDetails2.SetRowSpan(Me.lbGenres, 5)
        Me.lbGenres.Size = New System.Drawing.Size(400, 147)
        Me.lbGenres.TabIndex = 68
        '
        'btnGenres_Add
        '
        Me.btnGenres_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenres_Add.Image = CType(resources.GetObject("btnGenres_Add.Image"), System.Drawing.Image)
        Me.btnGenres_Add.Location = New System.Drawing.Point(52, 243)
        Me.btnGenres_Add.Name = "btnGenres_Add"
        Me.btnGenres_Add.Size = New System.Drawing.Size(23, 23)
        Me.btnGenres_Add.TabIndex = 64
        Me.btnGenres_Add.UseVisualStyleBackColor = True
        '
        'btnGenres_Remove
        '
        Me.btnGenres_Remove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenres_Remove.Image = CType(resources.GetObject("btnGenres_Remove.Image"), System.Drawing.Image)
        Me.btnGenres_Remove.Location = New System.Drawing.Point(52, 272)
        Me.btnGenres_Remove.Name = "btnGenres_Remove"
        Me.btnGenres_Remove.Size = New System.Drawing.Size(23, 23)
        Me.btnGenres_Remove.TabIndex = 65
        Me.btnGenres_Remove.UseVisualStyleBackColor = True
        '
        'btnGenres_Up
        '
        Me.btnGenres_Up.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenres_Up.Image = CType(resources.GetObject("btnGenres_Up.Image"), System.Drawing.Image)
        Me.btnGenres_Up.Location = New System.Drawing.Point(52, 301)
        Me.btnGenres_Up.Name = "btnGenres_Up"
        Me.btnGenres_Up.Size = New System.Drawing.Size(23, 23)
        Me.btnGenres_Up.TabIndex = 66
        Me.btnGenres_Up.UseVisualStyleBackColor = True
        '
        'btnGenres_Down
        '
        Me.btnGenres_Down.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenres_Down.Image = CType(resources.GetObject("btnGenres_Down.Image"), System.Drawing.Image)
        Me.btnGenres_Down.Location = New System.Drawing.Point(52, 330)
        Me.btnGenres_Down.Name = "btnGenres_Down"
        Me.btnGenres_Down.Size = New System.Drawing.Size(23, 23)
        Me.btnGenres_Down.TabIndex = 67
        Me.btnGenres_Down.UseVisualStyleBackColor = True
        '
        'lblStudios
        '
        Me.lblStudios.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblStudios.AutoSize = True
        Me.lblStudios.Location = New System.Drawing.Point(507, 59)
        Me.lblStudios.Name = "lblStudios"
        Me.lblStudios.Size = New System.Drawing.Size(49, 13)
        Me.lblStudios.TabIndex = 61
        Me.lblStudios.Text = "Studios:"
        '
        'dgvCountries
        '
        Me.dgvCountries.AllowUserToResizeColumns = False
        Me.dgvCountries.AllowUserToResizeRows = False
        Me.dgvCountries.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvCountries.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvCountries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCountries.ColumnHeadersVisible = False
        Me.dgvCountries.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCountriesName})
        Me.tblDetails2.SetColumnSpan(Me.dgvCountries, 2)
        Me.dgvCountries.Location = New System.Drawing.Point(81, 59)
        Me.dgvCountries.Name = "dgvCountries"
        Me.dgvCountries.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.tblDetails2.SetRowSpan(Me.dgvCountries, 2)
        Me.dgvCountries.Size = New System.Drawing.Size(400, 150)
        Me.dgvCountries.TabIndex = 60
        '
        'colCountriesName
        '
        Me.colCountriesName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colCountriesName.HeaderText = "Name"
        Me.colCountriesName.Name = "colCountriesName"
        Me.colCountriesName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'lblCountries
        '
        Me.lblCountries.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblCountries.AutoSize = True
        Me.lblCountries.Location = New System.Drawing.Point(15, 59)
        Me.lblCountries.Name = "lblCountries"
        Me.lblCountries.Size = New System.Drawing.Size(60, 13)
        Me.lblCountries.TabIndex = 59
        Me.lblCountries.Text = "Countries:"
        '
        'dgvStudios
        '
        Me.dgvStudios.AllowUserToResizeColumns = False
        Me.dgvStudios.AllowUserToResizeRows = False
        Me.dgvStudios.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvStudios.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvStudios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStudios.ColumnHeadersVisible = False
        Me.dgvStudios.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colStudiosName})
        Me.dgvStudios.Location = New System.Drawing.Point(562, 59)
        Me.dgvStudios.Name = "dgvStudios"
        Me.dgvStudios.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.tblDetails2.SetRowSpan(Me.dgvStudios, 2)
        Me.dgvStudios.Size = New System.Drawing.Size(400, 150)
        Me.dgvStudios.TabIndex = 62
        '
        'colStudiosName
        '
        Me.colStudiosName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colStudiosName.HeaderText = "Name"
        Me.colStudiosName.Name = "colStudiosName"
        Me.colStudiosName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'lblTags
        '
        Me.lblTags.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblTags.AutoSize = True
        Me.lblTags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTags.Location = New System.Drawing.Point(523, 219)
        Me.lblTags.Name = "lblTags"
        Me.lblTags.Size = New System.Drawing.Size(33, 13)
        Me.lblTags.TabIndex = 56
        Me.lblTags.Text = "Tags:"
        '
        'lbTags
        '
        Me.lbTags.FormattingEnabled = True
        Me.lbTags.Location = New System.Drawing.Point(562, 215)
        Me.lbTags.Name = "lbTags"
        Me.tblDetails2.SetRowSpan(Me.lbTags, 5)
        Me.lbTags.Size = New System.Drawing.Size(400, 147)
        Me.lbTags.TabIndex = 68
        '
        'cbGenres
        '
        Me.tblDetails2.SetColumnSpan(Me.cbGenres, 2)
        Me.cbGenres.FormattingEnabled = True
        Me.cbGenres.Location = New System.Drawing.Point(81, 368)
        Me.cbGenres.Name = "cbGenres"
        Me.cbGenres.Size = New System.Drawing.Size(400, 21)
        Me.cbGenres.TabIndex = 69
        '
        'cbTags
        '
        Me.cbTags.FormattingEnabled = True
        Me.cbTags.Location = New System.Drawing.Point(562, 368)
        Me.cbTags.Name = "cbTags"
        Me.cbTags.Size = New System.Drawing.Size(400, 21)
        Me.cbTags.TabIndex = 70
        '
        'btnTags_Add
        '
        Me.btnTags_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTags_Add.Image = CType(resources.GetObject("btnTags_Add.Image"), System.Drawing.Image)
        Me.btnTags_Add.Location = New System.Drawing.Point(533, 243)
        Me.btnTags_Add.Name = "btnTags_Add"
        Me.btnTags_Add.Size = New System.Drawing.Size(23, 23)
        Me.btnTags_Add.TabIndex = 64
        Me.btnTags_Add.UseVisualStyleBackColor = True
        '
        'btnTags_Remove
        '
        Me.btnTags_Remove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTags_Remove.Image = CType(resources.GetObject("btnTags_Remove.Image"), System.Drawing.Image)
        Me.btnTags_Remove.Location = New System.Drawing.Point(533, 272)
        Me.btnTags_Remove.Name = "btnTags_Remove"
        Me.btnTags_Remove.Size = New System.Drawing.Size(23, 23)
        Me.btnTags_Remove.TabIndex = 65
        Me.btnTags_Remove.UseVisualStyleBackColor = True
        '
        'btnTags_Up
        '
        Me.btnTags_Up.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTags_Up.Image = CType(resources.GetObject("btnTags_Up.Image"), System.Drawing.Image)
        Me.btnTags_Up.Location = New System.Drawing.Point(533, 301)
        Me.btnTags_Up.Name = "btnTags_Up"
        Me.btnTags_Up.Size = New System.Drawing.Size(23, 23)
        Me.btnTags_Up.TabIndex = 66
        Me.btnTags_Up.UseVisualStyleBackColor = True
        '
        'btnTags_Down
        '
        Me.btnTags_Down.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTags_Down.Image = CType(resources.GetObject("btnTags_Down.Image"), System.Drawing.Image)
        Me.btnTags_Down.Location = New System.Drawing.Point(533, 330)
        Me.btnTags_Down.Name = "btnTags_Down"
        Me.btnTags_Down.Size = New System.Drawing.Size(23, 23)
        Me.btnTags_Down.TabIndex = 67
        Me.btnTags_Down.UseVisualStyleBackColor = True
        '
        'tpCastCrew
        '
        Me.tpCastCrew.BackColor = System.Drawing.SystemColors.Control
        Me.tpCastCrew.Controls.Add(Me.tblCastCrew)
        Me.tpCastCrew.Location = New System.Drawing.Point(4, 22)
        Me.tpCastCrew.Name = "tpCastCrew"
        Me.tpCastCrew.Size = New System.Drawing.Size(1276, 671)
        Me.tpCastCrew.TabIndex = 19
        Me.tpCastCrew.Text = "Cast & Crew"
        '
        'tblCastCrew
        '
        Me.tblCastCrew.AutoSize = True
        Me.tblCastCrew.ColumnCount = 3
        Me.tblCastCrew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCastCrew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblCastCrew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblCastCrew.Controls.Add(Me.lblActors, 0, 0)
        Me.tblCastCrew.Controls.Add(Me.lvActors, 1, 0)
        Me.tblCastCrew.Controls.Add(Me.btnActorsAdd, 0, 1)
        Me.tblCastCrew.Controls.Add(Me.btnActorsRemove, 0, 2)
        Me.tblCastCrew.Controls.Add(Me.btnActorsEdit, 0, 5)
        Me.tblCastCrew.Controls.Add(Me.btnActorsDown, 0, 4)
        Me.tblCastCrew.Controls.Add(Me.btnActorsUp, 0, 3)
        Me.tblCastCrew.Controls.Add(Me.dgvCreators, 1, 7)
        Me.tblCastCrew.Controls.Add(Me.lblCreators, 0, 7)
        Me.tblCastCrew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblCastCrew.Location = New System.Drawing.Point(0, 0)
        Me.tblCastCrew.Name = "tblCastCrew"
        Me.tblCastCrew.RowCount = 9
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.tblCastCrew.Size = New System.Drawing.Size(1276, 671)
        Me.tblCastCrew.TabIndex = 0
        '
        'lblActors
        '
        Me.lblActors.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblActors.AutoSize = True
        Me.lblActors.Location = New System.Drawing.Point(14, 7)
        Me.lblActors.Name = "lblActors"
        Me.lblActors.Size = New System.Drawing.Size(42, 13)
        Me.lblActors.TabIndex = 30
        Me.lblActors.Text = "Actors:"
        '
        'lvActors
        '
        Me.lvActors.BackColor = System.Drawing.SystemColors.Window
        Me.lvActors.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colActorsID, Me.colActorsName, Me.colActorsRole, Me.colActorsThumb})
        Me.tblCastCrew.SetColumnSpan(Me.lvActors, 2)
        Me.lvActors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvActors.FullRowSelect = True
        Me.lvActors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvActors.HideSelection = False
        Me.lvActors.Location = New System.Drawing.Point(62, 3)
        Me.lvActors.Name = "lvActors"
        Me.tblCastCrew.SetRowSpan(Me.lvActors, 6)
        Me.lvActors.Size = New System.Drawing.Size(1211, 473)
        Me.lvActors.TabIndex = 31
        Me.lvActors.UseCompatibleStateImageBehavior = False
        Me.lvActors.View = System.Windows.Forms.View.Details
        '
        'colActorsID
        '
        Me.colActorsID.Text = "ID"
        Me.colActorsID.Width = 0
        '
        'colActorsName
        '
        Me.colActorsName.Text = "Name"
        Me.colActorsName.Width = 300
        '
        'colActorsRole
        '
        Me.colActorsRole.Text = "Role"
        Me.colActorsRole.Width = 300
        '
        'colActorsThumb
        '
        Me.colActorsThumb.Text = "Thumb"
        Me.colActorsThumb.Width = 500
        '
        'btnActorsAdd
        '
        Me.btnActorsAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnActorsAdd.Image = CType(resources.GetObject("btnActorsAdd.Image"), System.Drawing.Image)
        Me.btnActorsAdd.Location = New System.Drawing.Point(33, 31)
        Me.btnActorsAdd.Name = "btnActorsAdd"
        Me.btnActorsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsAdd.TabIndex = 32
        Me.btnActorsAdd.UseVisualStyleBackColor = True
        '
        'btnActorsRemove
        '
        Me.btnActorsRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnActorsRemove.Image = CType(resources.GetObject("btnActorsRemove.Image"), System.Drawing.Image)
        Me.btnActorsRemove.Location = New System.Drawing.Point(33, 60)
        Me.btnActorsRemove.Name = "btnActorsRemove"
        Me.btnActorsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsRemove.TabIndex = 36
        Me.btnActorsRemove.UseVisualStyleBackColor = True
        '
        'btnActorsEdit
        '
        Me.btnActorsEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnActorsEdit.Image = CType(resources.GetObject("btnActorsEdit.Image"), System.Drawing.Image)
        Me.btnActorsEdit.Location = New System.Drawing.Point(33, 147)
        Me.btnActorsEdit.Name = "btnActorsEdit"
        Me.btnActorsEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsEdit.TabIndex = 33
        Me.btnActorsEdit.UseVisualStyleBackColor = True
        '
        'btnActorsDown
        '
        Me.btnActorsDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnActorsDown.Image = CType(resources.GetObject("btnActorsDown.Image"), System.Drawing.Image)
        Me.btnActorsDown.Location = New System.Drawing.Point(33, 118)
        Me.btnActorsDown.Name = "btnActorsDown"
        Me.btnActorsDown.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsDown.TabIndex = 35
        Me.btnActorsDown.UseVisualStyleBackColor = True
        '
        'btnActorsUp
        '
        Me.btnActorsUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnActorsUp.Image = CType(resources.GetObject("btnActorsUp.Image"), System.Drawing.Image)
        Me.btnActorsUp.Location = New System.Drawing.Point(33, 89)
        Me.btnActorsUp.Name = "btnActorsUp"
        Me.btnActorsUp.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsUp.TabIndex = 34
        Me.btnActorsUp.UseVisualStyleBackColor = True
        '
        'dgvCreators
        '
        Me.dgvCreators.AllowUserToResizeColumns = False
        Me.dgvCreators.AllowUserToResizeRows = False
        Me.dgvCreators.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvCreators.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvCreators.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCreators.ColumnHeadersVisible = False
        Me.dgvCreators.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCreditsName})
        Me.dgvCreators.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCreators.Location = New System.Drawing.Point(62, 502)
        Me.dgvCreators.Name = "dgvCreators"
        Me.dgvCreators.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.tblCastCrew.SetRowSpan(Me.dgvCreators, 2)
        Me.dgvCreators.Size = New System.Drawing.Size(602, 166)
        Me.dgvCreators.TabIndex = 42
        '
        'colCreditsName
        '
        Me.colCreditsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colCreditsName.HeaderText = "Name"
        Me.colCreditsName.Name = "colCreditsName"
        Me.colCreditsName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'lblCreators
        '
        Me.lblCreators.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblCreators.AutoSize = True
        Me.lblCreators.Location = New System.Drawing.Point(3, 506)
        Me.lblCreators.Name = "lblCreators"
        Me.lblCreators.Size = New System.Drawing.Size(53, 13)
        Me.lblCreators.TabIndex = 41
        Me.lblCreators.Text = "Creators:"
        '
        'tpOther
        '
        Me.tpOther.BackColor = System.Drawing.SystemColors.Control
        Me.tpOther.Controls.Add(Me.tblOther)
        Me.tpOther.Location = New System.Drawing.Point(4, 22)
        Me.tpOther.Name = "tpOther"
        Me.tpOther.Size = New System.Drawing.Size(1276, 671)
        Me.tpOther.TabIndex = 17
        Me.tpOther.Text = "Other"
        '
        'tblOther
        '
        Me.tblOther.AutoSize = True
        Me.tblOther.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblOther.ColumnCount = 1
        Me.tblOther.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblOther.Controls.Add(Me.gbTheme, 0, 0)
        Me.tblOther.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblOther.Location = New System.Drawing.Point(0, 0)
        Me.tblOther.Name = "tblOther"
        Me.tblOther.RowCount = 2
        Me.tblOther.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblOther.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblOther.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblOther.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblOther.Size = New System.Drawing.Size(1276, 671)
        Me.tblOther.TabIndex = 0
        '
        'gbTheme
        '
        Me.gbTheme.AutoSize = True
        Me.gbTheme.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbTheme.Controls.Add(Me.tblTheme)
        Me.gbTheme.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTheme.Location = New System.Drawing.Point(3, 3)
        Me.gbTheme.Name = "gbTheme"
        Me.gbTheme.Size = New System.Drawing.Size(1270, 78)
        Me.gbTheme.TabIndex = 2
        Me.gbTheme.TabStop = False
        Me.gbTheme.Text = "Theme"
        '
        'tblTheme
        '
        Me.tblTheme.AutoSize = True
        Me.tblTheme.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblTheme.ColumnCount = 5
        Me.tblTheme.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTheme.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTheme.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTheme.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblTheme.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTheme.Controls.Add(Me.btnRemoveTheme, 4, 1)
        Me.tblTheme.Controls.Add(Me.btnLocalThemePlay, 4, 0)
        Me.tblTheme.Controls.Add(Me.txtLocalTheme, 0, 0)
        Me.tblTheme.Controls.Add(Me.btnSetThemeLocal, 2, 1)
        Me.tblTheme.Controls.Add(Me.btnSetThemeScrape, 0, 1)
        Me.tblTheme.Controls.Add(Me.btnSetThemeDL, 1, 1)
        Me.tblTheme.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTheme.Location = New System.Drawing.Point(3, 18)
        Me.tblTheme.Name = "tblTheme"
        Me.tblTheme.RowCount = 2
        Me.tblTheme.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTheme.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTheme.Size = New System.Drawing.Size(1264, 57)
        Me.tblTheme.TabIndex = 59
        '
        'btnRemoveTheme
        '
        Me.btnRemoveTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveTheme.Image = CType(resources.GetObject("btnRemoveTheme.Image"), System.Drawing.Image)
        Me.btnRemoveTheme.Location = New System.Drawing.Point(1238, 31)
        Me.btnRemoveTheme.Name = "btnRemoveTheme"
        Me.btnRemoveTheme.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveTheme.TabIndex = 5
        Me.btnRemoveTheme.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveTheme.UseVisualStyleBackColor = True
        '
        'btnLocalThemePlay
        '
        Me.btnLocalThemePlay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLocalThemePlay.Enabled = False
        Me.btnLocalThemePlay.Image = CType(resources.GetObject("btnLocalThemePlay.Image"), System.Drawing.Image)
        Me.btnLocalThemePlay.Location = New System.Drawing.Point(1238, 3)
        Me.btnLocalThemePlay.Name = "btnLocalThemePlay"
        Me.btnLocalThemePlay.Size = New System.Drawing.Size(23, 22)
        Me.btnLocalThemePlay.TabIndex = 1
        Me.btnLocalThemePlay.UseVisualStyleBackColor = True
        '
        'txtLocalTheme
        '
        Me.tblTheme.SetColumnSpan(Me.txtLocalTheme, 4)
        Me.txtLocalTheme.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLocalTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtLocalTheme.Location = New System.Drawing.Point(3, 3)
        Me.txtLocalTheme.Name = "txtLocalTheme"
        Me.txtLocalTheme.ReadOnly = True
        Me.txtLocalTheme.Size = New System.Drawing.Size(1229, 22)
        Me.txtLocalTheme.TabIndex = 0
        '
        'btnSetThemeLocal
        '
        Me.btnSetThemeLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetThemeLocal.Image = CType(resources.GetObject("btnSetThemeLocal.Image"), System.Drawing.Image)
        Me.btnSetThemeLocal.Location = New System.Drawing.Point(61, 31)
        Me.btnSetThemeLocal.Name = "btnSetThemeLocal"
        Me.btnSetThemeLocal.Size = New System.Drawing.Size(23, 23)
        Me.btnSetThemeLocal.TabIndex = 4
        Me.btnSetThemeLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetThemeLocal.UseVisualStyleBackColor = True
        '
        'btnSetThemeScrape
        '
        Me.btnSetThemeScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetThemeScrape.Image = CType(resources.GetObject("btnSetThemeScrape.Image"), System.Drawing.Image)
        Me.btnSetThemeScrape.Location = New System.Drawing.Point(3, 31)
        Me.btnSetThemeScrape.Name = "btnSetThemeScrape"
        Me.btnSetThemeScrape.Size = New System.Drawing.Size(23, 23)
        Me.btnSetThemeScrape.TabIndex = 2
        Me.btnSetThemeScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetThemeScrape.UseVisualStyleBackColor = True
        '
        'btnSetThemeDL
        '
        Me.btnSetThemeDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetThemeDL.Image = CType(resources.GetObject("btnSetThemeDL.Image"), System.Drawing.Image)
        Me.btnSetThemeDL.Location = New System.Drawing.Point(32, 31)
        Me.btnSetThemeDL.Name = "btnSetThemeDL"
        Me.btnSetThemeDL.Size = New System.Drawing.Size(23, 23)
        Me.btnSetThemeDL.TabIndex = 3
        Me.btnSetThemeDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetThemeDL.UseVisualStyleBackColor = True
        '
        'tpImages
        '
        Me.tpImages.BackColor = System.Drawing.SystemColors.Control
        Me.tpImages.Controls.Add(Me.tblImages)
        Me.tpImages.Controls.Add(Me.pnlImagesRight)
        Me.tpImages.Location = New System.Drawing.Point(4, 22)
        Me.tpImages.Name = "tpImages"
        Me.tpImages.Padding = New System.Windows.Forms.Padding(3)
        Me.tpImages.Size = New System.Drawing.Size(1276, 671)
        Me.tpImages.TabIndex = 16
        Me.tpImages.Text = "Images"
        '
        'tblImages
        '
        Me.tblImages.AutoScroll = True
        Me.tblImages.AutoSize = True
        Me.tblImages.ColumnCount = 6
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.Controls.Add(Me.pnlExtrafanarts, 3, 0)
        Me.tblImages.Controls.Add(Me.pnlPoster, 0, 0)
        Me.tblImages.Controls.Add(Me.pnlCharacterArt, 0, 1)
        Me.tblImages.Controls.Add(Me.pnlClearLogo, 1, 2)
        Me.tblImages.Controls.Add(Me.pnlFanart, 2, 0)
        Me.tblImages.Controls.Add(Me.pnlLandscape, 2, 1)
        Me.tblImages.Controls.Add(Me.pnlBanner, 0, 2)
        Me.tblImages.Controls.Add(Me.pnlClearArt, 1, 1)
        Me.tblImages.Controls.Add(Me.pnlKeyart, 1, 0)
        Me.tblImages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImages.Location = New System.Drawing.Point(3, 3)
        Me.tblImages.Name = "tblImages"
        Me.tblImages.RowCount = 4
        Me.tblImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImages.Size = New System.Drawing.Size(1267, 665)
        Me.tblImages.TabIndex = 2
        '
        'pnlExtrafanarts
        '
        Me.pnlExtrafanarts.AutoSize = True
        Me.pnlExtrafanarts.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlExtrafanarts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlExtrafanarts.Controls.Add(Me.tblExtrafanarts)
        Me.pnlExtrafanarts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExtrafanarts.Location = New System.Drawing.Point(813, 3)
        Me.pnlExtrafanarts.Name = "pnlExtrafanarts"
        Me.tblImages.SetRowSpan(Me.pnlExtrafanarts, 3)
        Me.pnlExtrafanarts.Size = New System.Drawing.Size(202, 631)
        Me.pnlExtrafanarts.TabIndex = 1
        '
        'tblExtrafanarts
        '
        Me.tblExtrafanarts.AutoSize = True
        Me.tblExtrafanarts.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblExtrafanarts.ColumnCount = 8
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblExtrafanarts.Controls.Add(Me.btnRemoveExtrafanarts, 7, 2)
        Me.tblExtrafanarts.Controls.Add(Me.lblExtrafanarts, 0, 0)
        Me.tblExtrafanarts.Controls.Add(Me.pnlExtrafanartsList, 0, 1)
        Me.tblExtrafanarts.Controls.Add(Me.btnLocalExtrafanarts, 2, 2)
        Me.tblExtrafanarts.Controls.Add(Me.btnRefreshExtrafanarts, 5, 2)
        Me.tblExtrafanarts.Controls.Add(Me.btnScrapeExtrafanarts, 0, 2)
        Me.tblExtrafanarts.Controls.Add(Me.btnDLExtrafanarts, 1, 2)
        Me.tblExtrafanarts.Controls.Add(Me.btnClipboardExtrafanarts, 3, 2)
        Me.tblExtrafanarts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblExtrafanarts.Location = New System.Drawing.Point(0, 0)
        Me.tblExtrafanarts.Name = "tblExtrafanarts"
        Me.tblExtrafanarts.RowCount = 3
        Me.tblExtrafanarts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblExtrafanarts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblExtrafanarts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblExtrafanarts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblExtrafanarts.Size = New System.Drawing.Size(200, 629)
        Me.tblExtrafanarts.TabIndex = 0
        '
        'btnRemoveExtrafanarts
        '
        Me.btnRemoveExtrafanarts.Enabled = False
        Me.btnRemoveExtrafanarts.Image = CType(resources.GetObject("btnRemoveExtrafanarts.Image"), System.Drawing.Image)
        Me.btnRemoveExtrafanarts.Location = New System.Drawing.Point(174, 603)
        Me.btnRemoveExtrafanarts.Name = "btnRemoveExtrafanarts"
        Me.btnRemoveExtrafanarts.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveExtrafanarts.TabIndex = 3
        Me.btnRemoveExtrafanarts.UseVisualStyleBackColor = True
        '
        'lblExtrafanarts
        '
        Me.lblExtrafanarts.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblExtrafanarts.AutoSize = True
        Me.tblExtrafanarts.SetColumnSpan(Me.lblExtrafanarts, 8)
        Me.lblExtrafanarts.Location = New System.Drawing.Point(66, 3)
        Me.lblExtrafanarts.Name = "lblExtrafanarts"
        Me.lblExtrafanarts.Size = New System.Drawing.Size(68, 13)
        Me.lblExtrafanarts.TabIndex = 2
        Me.lblExtrafanarts.Text = "Extrafanarts"
        '
        'pnlExtrafanartsList
        '
        Me.pnlExtrafanartsList.AutoScroll = True
        Me.tblExtrafanarts.SetColumnSpan(Me.pnlExtrafanartsList, 8)
        Me.pnlExtrafanartsList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExtrafanartsList.Location = New System.Drawing.Point(0, 20)
        Me.pnlExtrafanartsList.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlExtrafanartsList.Name = "pnlExtrafanartsList"
        Me.pnlExtrafanartsList.Size = New System.Drawing.Size(200, 580)
        Me.pnlExtrafanartsList.TabIndex = 1
        '
        'btnLocalExtrafanarts
        '
        Me.btnLocalExtrafanarts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnLocalExtrafanarts.Image = CType(resources.GetObject("btnLocalExtrafanarts.Image"), System.Drawing.Image)
        Me.btnLocalExtrafanarts.Location = New System.Drawing.Point(61, 603)
        Me.btnLocalExtrafanarts.Name = "btnLocalExtrafanarts"
        Me.btnLocalExtrafanarts.Size = New System.Drawing.Size(23, 23)
        Me.btnLocalExtrafanarts.TabIndex = 2
        Me.btnLocalExtrafanarts.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLocalExtrafanarts.UseVisualStyleBackColor = True
        '
        'btnRefreshExtrafanarts
        '
        Me.btnRefreshExtrafanarts.Image = CType(resources.GetObject("btnRefreshExtrafanarts.Image"), System.Drawing.Image)
        Me.btnRefreshExtrafanarts.Location = New System.Drawing.Point(132, 603)
        Me.btnRefreshExtrafanarts.Name = "btnRefreshExtrafanarts"
        Me.btnRefreshExtrafanarts.Size = New System.Drawing.Size(23, 23)
        Me.btnRefreshExtrafanarts.TabIndex = 2
        Me.btnRefreshExtrafanarts.UseVisualStyleBackColor = True
        '
        'btnScrapeExtrafanarts
        '
        Me.btnScrapeExtrafanarts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnScrapeExtrafanarts.Image = CType(resources.GetObject("btnScrapeExtrafanarts.Image"), System.Drawing.Image)
        Me.btnScrapeExtrafanarts.Location = New System.Drawing.Point(3, 603)
        Me.btnScrapeExtrafanarts.Name = "btnScrapeExtrafanarts"
        Me.btnScrapeExtrafanarts.Size = New System.Drawing.Size(23, 23)
        Me.btnScrapeExtrafanarts.TabIndex = 0
        Me.btnScrapeExtrafanarts.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnScrapeExtrafanarts.UseVisualStyleBackColor = True
        '
        'btnDLExtrafanarts
        '
        Me.btnDLExtrafanarts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnDLExtrafanarts.Image = CType(resources.GetObject("btnDLExtrafanarts.Image"), System.Drawing.Image)
        Me.btnDLExtrafanarts.Location = New System.Drawing.Point(32, 603)
        Me.btnDLExtrafanarts.Name = "btnDLExtrafanarts"
        Me.btnDLExtrafanarts.Size = New System.Drawing.Size(23, 23)
        Me.btnDLExtrafanarts.TabIndex = 1
        Me.btnDLExtrafanarts.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDLExtrafanarts.UseVisualStyleBackColor = True
        '
        'btnClipboardExtrafanarts
        '
        Me.btnClipboardExtrafanarts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClipboardExtrafanarts.Image = CType(resources.GetObject("btnClipboardExtrafanarts.Image"), System.Drawing.Image)
        Me.btnClipboardExtrafanarts.Location = New System.Drawing.Point(90, 603)
        Me.btnClipboardExtrafanarts.Name = "btnClipboardExtrafanarts"
        Me.btnClipboardExtrafanarts.Size = New System.Drawing.Size(23, 23)
        Me.btnClipboardExtrafanarts.TabIndex = 2
        Me.btnClipboardExtrafanarts.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClipboardExtrafanarts.UseVisualStyleBackColor = True
        '
        'pnlPoster
        '
        Me.pnlPoster.AutoSize = True
        Me.pnlPoster.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPoster.Controls.Add(Me.tblPoster)
        Me.pnlPoster.Location = New System.Drawing.Point(3, 3)
        Me.pnlPoster.Name = "pnlPoster"
        Me.pnlPoster.Size = New System.Drawing.Size(264, 221)
        Me.pnlPoster.TabIndex = 0
        '
        'tblPoster
        '
        Me.tblPoster.AutoScroll = True
        Me.tblPoster.AutoSize = True
        Me.tblPoster.ColumnCount = 6
        Me.tblPoster.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblPoster.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblPoster.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblPoster.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblPoster.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblPoster.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblPoster.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblPoster.Controls.Add(Me.pbPoster, 0, 1)
        Me.tblPoster.Controls.Add(Me.lblPoster, 0, 0)
        Me.tblPoster.Controls.Add(Me.btnLocalPoster, 2, 3)
        Me.tblPoster.Controls.Add(Me.btnScrapePoster, 0, 3)
        Me.tblPoster.Controls.Add(Me.lblSizePoster, 0, 2)
        Me.tblPoster.Controls.Add(Me.btnDLPoster, 1, 3)
        Me.tblPoster.Controls.Add(Me.btnRemovePoster, 5, 3)
        Me.tblPoster.Controls.Add(Me.btnClipboardPoster, 3, 3)
        Me.tblPoster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblPoster.Location = New System.Drawing.Point(0, 0)
        Me.tblPoster.Name = "tblPoster"
        Me.tblPoster.RowCount = 4
        Me.tblPoster.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblPoster.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblPoster.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblPoster.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblPoster.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblPoster.Size = New System.Drawing.Size(262, 219)
        Me.tblPoster.TabIndex = 0
        '
        'pbPoster
        '
        Me.pbPoster.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pbPoster.BackColor = System.Drawing.Color.White
        Me.tblPoster.SetColumnSpan(Me.pbPoster, 6)
        Me.pbPoster.Location = New System.Drawing.Point(3, 23)
        Me.pbPoster.Name = "pbPoster"
        Me.pbPoster.Size = New System.Drawing.Size(256, 144)
        Me.pbPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbPoster.TabIndex = 1
        Me.pbPoster.TabStop = False
        '
        'lblPoster
        '
        Me.lblPoster.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPoster.AutoSize = True
        Me.tblPoster.SetColumnSpan(Me.lblPoster, 6)
        Me.lblPoster.Location = New System.Drawing.Point(111, 3)
        Me.lblPoster.Name = "lblPoster"
        Me.lblPoster.Size = New System.Drawing.Size(39, 13)
        Me.lblPoster.TabIndex = 2
        Me.lblPoster.Text = "Poster"
        '
        'btnLocalPoster
        '
        Me.btnLocalPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnLocalPoster.Image = CType(resources.GetObject("btnLocalPoster.Image"), System.Drawing.Image)
        Me.btnLocalPoster.Location = New System.Drawing.Point(61, 193)
        Me.btnLocalPoster.Name = "btnLocalPoster"
        Me.btnLocalPoster.Size = New System.Drawing.Size(23, 23)
        Me.btnLocalPoster.TabIndex = 2
        Me.btnLocalPoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLocalPoster.UseVisualStyleBackColor = True
        '
        'btnScrapePoster
        '
        Me.btnScrapePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnScrapePoster.Image = CType(resources.GetObject("btnScrapePoster.Image"), System.Drawing.Image)
        Me.btnScrapePoster.Location = New System.Drawing.Point(3, 193)
        Me.btnScrapePoster.Name = "btnScrapePoster"
        Me.btnScrapePoster.Size = New System.Drawing.Size(23, 23)
        Me.btnScrapePoster.TabIndex = 0
        Me.btnScrapePoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnScrapePoster.UseVisualStyleBackColor = True
        '
        'lblSizePoster
        '
        Me.lblSizePoster.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblSizePoster.AutoSize = True
        Me.tblPoster.SetColumnSpan(Me.lblSizePoster, 6)
        Me.lblSizePoster.Location = New System.Drawing.Point(85, 173)
        Me.lblSizePoster.Name = "lblSizePoster"
        Me.lblSizePoster.Size = New System.Drawing.Size(92, 13)
        Me.lblSizePoster.TabIndex = 5
        Me.lblSizePoster.Text = "Size: (XXXXxXXXX)"
        Me.lblSizePoster.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSizePoster.Visible = False
        '
        'btnDLPoster
        '
        Me.btnDLPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnDLPoster.Image = CType(resources.GetObject("btnDLPoster.Image"), System.Drawing.Image)
        Me.btnDLPoster.Location = New System.Drawing.Point(32, 193)
        Me.btnDLPoster.Name = "btnDLPoster"
        Me.btnDLPoster.Size = New System.Drawing.Size(23, 23)
        Me.btnDLPoster.TabIndex = 1
        Me.btnDLPoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDLPoster.UseVisualStyleBackColor = True
        '
        'btnRemovePoster
        '
        Me.btnRemovePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemovePoster.Image = CType(resources.GetObject("btnRemovePoster.Image"), System.Drawing.Image)
        Me.btnRemovePoster.Location = New System.Drawing.Point(236, 193)
        Me.btnRemovePoster.Name = "btnRemovePoster"
        Me.btnRemovePoster.Size = New System.Drawing.Size(23, 23)
        Me.btnRemovePoster.TabIndex = 3
        Me.btnRemovePoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemovePoster.UseVisualStyleBackColor = True
        '
        'btnClipboardPoster
        '
        Me.btnClipboardPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClipboardPoster.Image = CType(resources.GetObject("btnClipboardPoster.Image"), System.Drawing.Image)
        Me.btnClipboardPoster.Location = New System.Drawing.Point(90, 193)
        Me.btnClipboardPoster.Name = "btnClipboardPoster"
        Me.btnClipboardPoster.Size = New System.Drawing.Size(23, 23)
        Me.btnClipboardPoster.TabIndex = 2
        Me.btnClipboardPoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClipboardPoster.UseVisualStyleBackColor = True
        '
        'pnlCharacterArt
        '
        Me.pnlCharacterArt.AutoSize = True
        Me.pnlCharacterArt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlCharacterArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCharacterArt.Controls.Add(Me.tblCharacterArt)
        Me.pnlCharacterArt.Location = New System.Drawing.Point(3, 230)
        Me.pnlCharacterArt.Name = "pnlCharacterArt"
        Me.pnlCharacterArt.Size = New System.Drawing.Size(264, 221)
        Me.pnlCharacterArt.TabIndex = 5
        '
        'tblCharacterArt
        '
        Me.tblCharacterArt.AutoScroll = True
        Me.tblCharacterArt.AutoSize = True
        Me.tblCharacterArt.ColumnCount = 6
        Me.tblCharacterArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCharacterArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCharacterArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCharacterArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCharacterArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblCharacterArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCharacterArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblCharacterArt.Controls.Add(Me.pbCharacterArt, 0, 1)
        Me.tblCharacterArt.Controls.Add(Me.lblCharacterArt, 0, 0)
        Me.tblCharacterArt.Controls.Add(Me.btnLocalCharacterArt, 2, 3)
        Me.tblCharacterArt.Controls.Add(Me.btnScrapeCharacterArt, 0, 3)
        Me.tblCharacterArt.Controls.Add(Me.lblSizeCharacterArt, 0, 2)
        Me.tblCharacterArt.Controls.Add(Me.btnDLCharacterArt, 1, 3)
        Me.tblCharacterArt.Controls.Add(Me.btnRemoveCharacterArt, 5, 3)
        Me.tblCharacterArt.Controls.Add(Me.btnClipboardCharacterArt, 3, 3)
        Me.tblCharacterArt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblCharacterArt.Location = New System.Drawing.Point(0, 0)
        Me.tblCharacterArt.Name = "tblCharacterArt"
        Me.tblCharacterArt.RowCount = 4
        Me.tblCharacterArt.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblCharacterArt.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCharacterArt.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblCharacterArt.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCharacterArt.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblCharacterArt.Size = New System.Drawing.Size(262, 219)
        Me.tblCharacterArt.TabIndex = 0
        '
        'pbCharacterArt
        '
        Me.pbCharacterArt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pbCharacterArt.BackColor = System.Drawing.Color.White
        Me.tblCharacterArt.SetColumnSpan(Me.pbCharacterArt, 6)
        Me.pbCharacterArt.Location = New System.Drawing.Point(3, 23)
        Me.pbCharacterArt.Name = "pbCharacterArt"
        Me.pbCharacterArt.Size = New System.Drawing.Size(256, 144)
        Me.pbCharacterArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbCharacterArt.TabIndex = 1
        Me.pbCharacterArt.TabStop = False
        '
        'lblCharacterArt
        '
        Me.lblCharacterArt.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblCharacterArt.AutoSize = True
        Me.tblCharacterArt.SetColumnSpan(Me.lblCharacterArt, 6)
        Me.lblCharacterArt.Location = New System.Drawing.Point(95, 3)
        Me.lblCharacterArt.Name = "lblCharacterArt"
        Me.lblCharacterArt.Size = New System.Drawing.Size(71, 13)
        Me.lblCharacterArt.TabIndex = 2
        Me.lblCharacterArt.Text = "CharacterArt"
        '
        'btnLocalCharacterArt
        '
        Me.btnLocalCharacterArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnLocalCharacterArt.Image = CType(resources.GetObject("btnLocalCharacterArt.Image"), System.Drawing.Image)
        Me.btnLocalCharacterArt.Location = New System.Drawing.Point(61, 193)
        Me.btnLocalCharacterArt.Name = "btnLocalCharacterArt"
        Me.btnLocalCharacterArt.Size = New System.Drawing.Size(23, 23)
        Me.btnLocalCharacterArt.TabIndex = 2
        Me.btnLocalCharacterArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLocalCharacterArt.UseVisualStyleBackColor = True
        '
        'btnScrapeCharacterArt
        '
        Me.btnScrapeCharacterArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnScrapeCharacterArt.Image = CType(resources.GetObject("btnScrapeCharacterArt.Image"), System.Drawing.Image)
        Me.btnScrapeCharacterArt.Location = New System.Drawing.Point(3, 193)
        Me.btnScrapeCharacterArt.Name = "btnScrapeCharacterArt"
        Me.btnScrapeCharacterArt.Size = New System.Drawing.Size(23, 23)
        Me.btnScrapeCharacterArt.TabIndex = 0
        Me.btnScrapeCharacterArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnScrapeCharacterArt.UseVisualStyleBackColor = True
        '
        'lblSizeCharacterArt
        '
        Me.lblSizeCharacterArt.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblSizeCharacterArt.AutoSize = True
        Me.tblCharacterArt.SetColumnSpan(Me.lblSizeCharacterArt, 6)
        Me.lblSizeCharacterArt.Location = New System.Drawing.Point(85, 173)
        Me.lblSizeCharacterArt.Name = "lblSizeCharacterArt"
        Me.lblSizeCharacterArt.Size = New System.Drawing.Size(92, 13)
        Me.lblSizeCharacterArt.TabIndex = 5
        Me.lblSizeCharacterArt.Text = "Size: (XXXXxXXXX)"
        Me.lblSizeCharacterArt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSizeCharacterArt.Visible = False
        '
        'btnDLCharacterArt
        '
        Me.btnDLCharacterArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnDLCharacterArt.Image = CType(resources.GetObject("btnDLCharacterArt.Image"), System.Drawing.Image)
        Me.btnDLCharacterArt.Location = New System.Drawing.Point(32, 193)
        Me.btnDLCharacterArt.Name = "btnDLCharacterArt"
        Me.btnDLCharacterArt.Size = New System.Drawing.Size(23, 23)
        Me.btnDLCharacterArt.TabIndex = 1
        Me.btnDLCharacterArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDLCharacterArt.UseVisualStyleBackColor = True
        '
        'btnRemoveCharacterArt
        '
        Me.btnRemoveCharacterArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveCharacterArt.Image = CType(resources.GetObject("btnRemoveCharacterArt.Image"), System.Drawing.Image)
        Me.btnRemoveCharacterArt.Location = New System.Drawing.Point(236, 193)
        Me.btnRemoveCharacterArt.Name = "btnRemoveCharacterArt"
        Me.btnRemoveCharacterArt.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveCharacterArt.TabIndex = 3
        Me.btnRemoveCharacterArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveCharacterArt.UseVisualStyleBackColor = True
        '
        'btnClipboardCharacterArt
        '
        Me.btnClipboardCharacterArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClipboardCharacterArt.Image = CType(resources.GetObject("btnClipboardCharacterArt.Image"), System.Drawing.Image)
        Me.btnClipboardCharacterArt.Location = New System.Drawing.Point(90, 193)
        Me.btnClipboardCharacterArt.Name = "btnClipboardCharacterArt"
        Me.btnClipboardCharacterArt.Size = New System.Drawing.Size(23, 23)
        Me.btnClipboardCharacterArt.TabIndex = 2
        Me.btnClipboardCharacterArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClipboardCharacterArt.UseVisualStyleBackColor = True
        '
        'pnlClearLogo
        '
        Me.pnlClearLogo.AutoSize = True
        Me.pnlClearLogo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlClearLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlClearLogo.Controls.Add(Me.tblClearLogo)
        Me.pnlClearLogo.Location = New System.Drawing.Point(273, 457)
        Me.pnlClearLogo.Name = "pnlClearLogo"
        Me.pnlClearLogo.Size = New System.Drawing.Size(264, 177)
        Me.pnlClearLogo.TabIndex = 4
        '
        'tblClearLogo
        '
        Me.tblClearLogo.AutoScroll = True
        Me.tblClearLogo.AutoSize = True
        Me.tblClearLogo.ColumnCount = 6
        Me.tblClearLogo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearLogo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearLogo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearLogo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearLogo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblClearLogo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearLogo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblClearLogo.Controls.Add(Me.pbClearLogo, 0, 1)
        Me.tblClearLogo.Controls.Add(Me.lblClearLogo, 0, 0)
        Me.tblClearLogo.Controls.Add(Me.btnLocalClearLogo, 2, 3)
        Me.tblClearLogo.Controls.Add(Me.btnScrapeClearLogo, 0, 3)
        Me.tblClearLogo.Controls.Add(Me.lblSizeClearLogo, 0, 2)
        Me.tblClearLogo.Controls.Add(Me.btnDLClearLogo, 1, 3)
        Me.tblClearLogo.Controls.Add(Me.btnRemoveClearLogo, 5, 3)
        Me.tblClearLogo.Controls.Add(Me.btnClipboardClearLogo, 3, 3)
        Me.tblClearLogo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblClearLogo.Location = New System.Drawing.Point(0, 0)
        Me.tblClearLogo.Name = "tblClearLogo"
        Me.tblClearLogo.RowCount = 4
        Me.tblClearLogo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblClearLogo.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearLogo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblClearLogo.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearLogo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblClearLogo.Size = New System.Drawing.Size(262, 175)
        Me.tblClearLogo.TabIndex = 0
        '
        'pbClearLogo
        '
        Me.pbClearLogo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pbClearLogo.BackColor = System.Drawing.Color.White
        Me.tblClearLogo.SetColumnSpan(Me.pbClearLogo, 6)
        Me.pbClearLogo.Location = New System.Drawing.Point(3, 23)
        Me.pbClearLogo.Name = "pbClearLogo"
        Me.pbClearLogo.Size = New System.Drawing.Size(256, 100)
        Me.pbClearLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbClearLogo.TabIndex = 1
        Me.pbClearLogo.TabStop = False
        '
        'lblClearLogo
        '
        Me.lblClearLogo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblClearLogo.AutoSize = True
        Me.tblClearLogo.SetColumnSpan(Me.lblClearLogo, 6)
        Me.lblClearLogo.Location = New System.Drawing.Point(101, 3)
        Me.lblClearLogo.Name = "lblClearLogo"
        Me.lblClearLogo.Size = New System.Drawing.Size(59, 13)
        Me.lblClearLogo.TabIndex = 2
        Me.lblClearLogo.Text = "ClearLogo"
        '
        'btnLocalClearLogo
        '
        Me.btnLocalClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnLocalClearLogo.Image = CType(resources.GetObject("btnLocalClearLogo.Image"), System.Drawing.Image)
        Me.btnLocalClearLogo.Location = New System.Drawing.Point(61, 149)
        Me.btnLocalClearLogo.Name = "btnLocalClearLogo"
        Me.btnLocalClearLogo.Size = New System.Drawing.Size(23, 23)
        Me.btnLocalClearLogo.TabIndex = 2
        Me.btnLocalClearLogo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLocalClearLogo.UseVisualStyleBackColor = True
        '
        'btnScrapeClearLogo
        '
        Me.btnScrapeClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnScrapeClearLogo.Image = CType(resources.GetObject("btnScrapeClearLogo.Image"), System.Drawing.Image)
        Me.btnScrapeClearLogo.Location = New System.Drawing.Point(3, 149)
        Me.btnScrapeClearLogo.Name = "btnScrapeClearLogo"
        Me.btnScrapeClearLogo.Size = New System.Drawing.Size(23, 23)
        Me.btnScrapeClearLogo.TabIndex = 0
        Me.btnScrapeClearLogo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnScrapeClearLogo.UseVisualStyleBackColor = True
        '
        'lblSizeClearLogo
        '
        Me.lblSizeClearLogo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblSizeClearLogo.AutoSize = True
        Me.tblClearLogo.SetColumnSpan(Me.lblSizeClearLogo, 6)
        Me.lblSizeClearLogo.Location = New System.Drawing.Point(85, 129)
        Me.lblSizeClearLogo.Name = "lblSizeClearLogo"
        Me.lblSizeClearLogo.Size = New System.Drawing.Size(92, 13)
        Me.lblSizeClearLogo.TabIndex = 5
        Me.lblSizeClearLogo.Text = "Size: (XXXXxXXXX)"
        Me.lblSizeClearLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSizeClearLogo.Visible = False
        '
        'btnDLClearLogo
        '
        Me.btnDLClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnDLClearLogo.Image = CType(resources.GetObject("btnDLClearLogo.Image"), System.Drawing.Image)
        Me.btnDLClearLogo.Location = New System.Drawing.Point(32, 149)
        Me.btnDLClearLogo.Name = "btnDLClearLogo"
        Me.btnDLClearLogo.Size = New System.Drawing.Size(23, 23)
        Me.btnDLClearLogo.TabIndex = 1
        Me.btnDLClearLogo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDLClearLogo.UseVisualStyleBackColor = True
        '
        'btnRemoveClearLogo
        '
        Me.btnRemoveClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveClearLogo.Image = CType(resources.GetObject("btnRemoveClearLogo.Image"), System.Drawing.Image)
        Me.btnRemoveClearLogo.Location = New System.Drawing.Point(236, 149)
        Me.btnRemoveClearLogo.Name = "btnRemoveClearLogo"
        Me.btnRemoveClearLogo.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveClearLogo.TabIndex = 3
        Me.btnRemoveClearLogo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveClearLogo.UseVisualStyleBackColor = True
        '
        'btnClipboardClearLogo
        '
        Me.btnClipboardClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClipboardClearLogo.Image = CType(resources.GetObject("btnClipboardClearLogo.Image"), System.Drawing.Image)
        Me.btnClipboardClearLogo.Location = New System.Drawing.Point(90, 149)
        Me.btnClipboardClearLogo.Name = "btnClipboardClearLogo"
        Me.btnClipboardClearLogo.Size = New System.Drawing.Size(23, 23)
        Me.btnClipboardClearLogo.TabIndex = 2
        Me.btnClipboardClearLogo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClipboardClearLogo.UseVisualStyleBackColor = True
        '
        'pnlFanart
        '
        Me.pnlFanart.AutoSize = True
        Me.pnlFanart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFanart.Controls.Add(Me.tblFanart)
        Me.pnlFanart.Location = New System.Drawing.Point(543, 3)
        Me.pnlFanart.Name = "pnlFanart"
        Me.pnlFanart.Size = New System.Drawing.Size(264, 221)
        Me.pnlFanart.TabIndex = 1
        '
        'tblFanart
        '
        Me.tblFanart.AutoScroll = True
        Me.tblFanart.AutoSize = True
        Me.tblFanart.ColumnCount = 6
        Me.tblFanart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFanart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFanart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFanart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFanart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblFanart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFanart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFanart.Controls.Add(Me.pbFanart, 0, 1)
        Me.tblFanart.Controls.Add(Me.lblFanart, 0, 0)
        Me.tblFanart.Controls.Add(Me.btnLocalFanart, 2, 3)
        Me.tblFanart.Controls.Add(Me.btnScrapeFanart, 0, 3)
        Me.tblFanart.Controls.Add(Me.lblSizeFanart, 0, 2)
        Me.tblFanart.Controls.Add(Me.btnDLFanart, 1, 3)
        Me.tblFanart.Controls.Add(Me.btnRemoveFanart, 5, 3)
        Me.tblFanart.Controls.Add(Me.btnClipboardFanart, 3, 3)
        Me.tblFanart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFanart.Location = New System.Drawing.Point(0, 0)
        Me.tblFanart.Name = "tblFanart"
        Me.tblFanart.RowCount = 4
        Me.tblFanart.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFanart.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFanart.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFanart.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFanart.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFanart.Size = New System.Drawing.Size(262, 219)
        Me.tblFanart.TabIndex = 0
        '
        'pbFanart
        '
        Me.pbFanart.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pbFanart.BackColor = System.Drawing.Color.White
        Me.tblFanart.SetColumnSpan(Me.pbFanart, 6)
        Me.pbFanart.Location = New System.Drawing.Point(3, 23)
        Me.pbFanart.Name = "pbFanart"
        Me.pbFanart.Size = New System.Drawing.Size(256, 144)
        Me.pbFanart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbFanart.TabIndex = 1
        Me.pbFanart.TabStop = False
        '
        'lblFanart
        '
        Me.lblFanart.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblFanart.AutoSize = True
        Me.tblFanart.SetColumnSpan(Me.lblFanart, 6)
        Me.lblFanart.Location = New System.Drawing.Point(111, 3)
        Me.lblFanart.Name = "lblFanart"
        Me.lblFanart.Size = New System.Drawing.Size(40, 13)
        Me.lblFanart.TabIndex = 2
        Me.lblFanart.Text = "Fanart"
        '
        'btnLocalFanart
        '
        Me.btnLocalFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnLocalFanart.Image = CType(resources.GetObject("btnLocalFanart.Image"), System.Drawing.Image)
        Me.btnLocalFanart.Location = New System.Drawing.Point(61, 193)
        Me.btnLocalFanart.Name = "btnLocalFanart"
        Me.btnLocalFanart.Size = New System.Drawing.Size(23, 23)
        Me.btnLocalFanart.TabIndex = 2
        Me.btnLocalFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLocalFanart.UseVisualStyleBackColor = True
        '
        'btnScrapeFanart
        '
        Me.btnScrapeFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnScrapeFanart.Image = CType(resources.GetObject("btnScrapeFanart.Image"), System.Drawing.Image)
        Me.btnScrapeFanart.Location = New System.Drawing.Point(3, 193)
        Me.btnScrapeFanart.Name = "btnScrapeFanart"
        Me.btnScrapeFanart.Size = New System.Drawing.Size(23, 23)
        Me.btnScrapeFanart.TabIndex = 0
        Me.btnScrapeFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnScrapeFanart.UseVisualStyleBackColor = True
        '
        'lblSizeFanart
        '
        Me.lblSizeFanart.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblSizeFanart.AutoSize = True
        Me.tblFanart.SetColumnSpan(Me.lblSizeFanart, 6)
        Me.lblSizeFanart.Location = New System.Drawing.Point(85, 173)
        Me.lblSizeFanart.Name = "lblSizeFanart"
        Me.lblSizeFanart.Size = New System.Drawing.Size(92, 13)
        Me.lblSizeFanart.TabIndex = 5
        Me.lblSizeFanart.Text = "Size: (XXXXxXXXX)"
        Me.lblSizeFanart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSizeFanart.Visible = False
        '
        'btnDLFanart
        '
        Me.btnDLFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnDLFanart.Image = CType(resources.GetObject("btnDLFanart.Image"), System.Drawing.Image)
        Me.btnDLFanart.Location = New System.Drawing.Point(32, 193)
        Me.btnDLFanart.Name = "btnDLFanart"
        Me.btnDLFanart.Size = New System.Drawing.Size(23, 23)
        Me.btnDLFanart.TabIndex = 1
        Me.btnDLFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDLFanart.UseVisualStyleBackColor = True
        '
        'btnRemoveFanart
        '
        Me.btnRemoveFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveFanart.Image = CType(resources.GetObject("btnRemoveFanart.Image"), System.Drawing.Image)
        Me.btnRemoveFanart.Location = New System.Drawing.Point(236, 193)
        Me.btnRemoveFanart.Name = "btnRemoveFanart"
        Me.btnRemoveFanart.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveFanart.TabIndex = 3
        Me.btnRemoveFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveFanart.UseVisualStyleBackColor = True
        '
        'btnClipboardFanart
        '
        Me.btnClipboardFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClipboardFanart.Image = CType(resources.GetObject("btnClipboardFanart.Image"), System.Drawing.Image)
        Me.btnClipboardFanart.Location = New System.Drawing.Point(90, 193)
        Me.btnClipboardFanart.Name = "btnClipboardFanart"
        Me.btnClipboardFanart.Size = New System.Drawing.Size(23, 23)
        Me.btnClipboardFanart.TabIndex = 2
        Me.btnClipboardFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClipboardFanart.UseVisualStyleBackColor = True
        '
        'pnlLandscape
        '
        Me.pnlLandscape.AutoSize = True
        Me.pnlLandscape.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlLandscape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlLandscape.Controls.Add(Me.tblLandscape)
        Me.pnlLandscape.Location = New System.Drawing.Point(543, 230)
        Me.pnlLandscape.Name = "pnlLandscape"
        Me.pnlLandscape.Size = New System.Drawing.Size(264, 221)
        Me.pnlLandscape.TabIndex = 2
        '
        'tblLandscape
        '
        Me.tblLandscape.AutoScroll = True
        Me.tblLandscape.AutoSize = True
        Me.tblLandscape.ColumnCount = 6
        Me.tblLandscape.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblLandscape.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblLandscape.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblLandscape.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblLandscape.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblLandscape.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblLandscape.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblLandscape.Controls.Add(Me.pbLandscape, 0, 1)
        Me.tblLandscape.Controls.Add(Me.lblLandscape, 0, 0)
        Me.tblLandscape.Controls.Add(Me.btnLocalLandscape, 2, 3)
        Me.tblLandscape.Controls.Add(Me.btnScrapeLandscape, 0, 3)
        Me.tblLandscape.Controls.Add(Me.lblSizeLandscape, 0, 2)
        Me.tblLandscape.Controls.Add(Me.btnDLLandscape, 1, 3)
        Me.tblLandscape.Controls.Add(Me.btnRemoveLandscape, 5, 3)
        Me.tblLandscape.Controls.Add(Me.btnClipboardLandscape, 3, 3)
        Me.tblLandscape.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblLandscape.Location = New System.Drawing.Point(0, 0)
        Me.tblLandscape.Name = "tblLandscape"
        Me.tblLandscape.RowCount = 4
        Me.tblLandscape.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblLandscape.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLandscape.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblLandscape.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLandscape.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblLandscape.Size = New System.Drawing.Size(262, 219)
        Me.tblLandscape.TabIndex = 0
        '
        'pbLandscape
        '
        Me.pbLandscape.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pbLandscape.BackColor = System.Drawing.Color.White
        Me.tblLandscape.SetColumnSpan(Me.pbLandscape, 6)
        Me.pbLandscape.Location = New System.Drawing.Point(3, 23)
        Me.pbLandscape.Name = "pbLandscape"
        Me.pbLandscape.Size = New System.Drawing.Size(256, 144)
        Me.pbLandscape.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbLandscape.TabIndex = 1
        Me.pbLandscape.TabStop = False
        '
        'lblLandscape
        '
        Me.lblLandscape.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblLandscape.AutoSize = True
        Me.tblLandscape.SetColumnSpan(Me.lblLandscape, 6)
        Me.lblLandscape.Location = New System.Drawing.Point(100, 3)
        Me.lblLandscape.Name = "lblLandscape"
        Me.lblLandscape.Size = New System.Drawing.Size(61, 13)
        Me.lblLandscape.TabIndex = 2
        Me.lblLandscape.Text = "Landscape"
        '
        'btnLocalLandscape
        '
        Me.btnLocalLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnLocalLandscape.Image = CType(resources.GetObject("btnLocalLandscape.Image"), System.Drawing.Image)
        Me.btnLocalLandscape.Location = New System.Drawing.Point(61, 193)
        Me.btnLocalLandscape.Name = "btnLocalLandscape"
        Me.btnLocalLandscape.Size = New System.Drawing.Size(23, 23)
        Me.btnLocalLandscape.TabIndex = 2
        Me.btnLocalLandscape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLocalLandscape.UseVisualStyleBackColor = True
        '
        'btnScrapeLandscape
        '
        Me.btnScrapeLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnScrapeLandscape.Image = CType(resources.GetObject("btnScrapeLandscape.Image"), System.Drawing.Image)
        Me.btnScrapeLandscape.Location = New System.Drawing.Point(3, 193)
        Me.btnScrapeLandscape.Name = "btnScrapeLandscape"
        Me.btnScrapeLandscape.Size = New System.Drawing.Size(23, 23)
        Me.btnScrapeLandscape.TabIndex = 0
        Me.btnScrapeLandscape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnScrapeLandscape.UseVisualStyleBackColor = True
        '
        'lblSizeLandscape
        '
        Me.lblSizeLandscape.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblSizeLandscape.AutoSize = True
        Me.tblLandscape.SetColumnSpan(Me.lblSizeLandscape, 6)
        Me.lblSizeLandscape.Location = New System.Drawing.Point(85, 173)
        Me.lblSizeLandscape.Name = "lblSizeLandscape"
        Me.lblSizeLandscape.Size = New System.Drawing.Size(92, 13)
        Me.lblSizeLandscape.TabIndex = 5
        Me.lblSizeLandscape.Text = "Size: (XXXXxXXXX)"
        Me.lblSizeLandscape.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSizeLandscape.Visible = False
        '
        'btnDLLandscape
        '
        Me.btnDLLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnDLLandscape.Image = CType(resources.GetObject("btnDLLandscape.Image"), System.Drawing.Image)
        Me.btnDLLandscape.Location = New System.Drawing.Point(32, 193)
        Me.btnDLLandscape.Name = "btnDLLandscape"
        Me.btnDLLandscape.Size = New System.Drawing.Size(23, 23)
        Me.btnDLLandscape.TabIndex = 1
        Me.btnDLLandscape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDLLandscape.UseVisualStyleBackColor = True
        '
        'btnRemoveLandscape
        '
        Me.btnRemoveLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveLandscape.Image = CType(resources.GetObject("btnRemoveLandscape.Image"), System.Drawing.Image)
        Me.btnRemoveLandscape.Location = New System.Drawing.Point(236, 193)
        Me.btnRemoveLandscape.Name = "btnRemoveLandscape"
        Me.btnRemoveLandscape.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveLandscape.TabIndex = 3
        Me.btnRemoveLandscape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveLandscape.UseVisualStyleBackColor = True
        '
        'btnClipboardLandscape
        '
        Me.btnClipboardLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClipboardLandscape.Image = CType(resources.GetObject("btnClipboardLandscape.Image"), System.Drawing.Image)
        Me.btnClipboardLandscape.Location = New System.Drawing.Point(90, 193)
        Me.btnClipboardLandscape.Name = "btnClipboardLandscape"
        Me.btnClipboardLandscape.Size = New System.Drawing.Size(23, 23)
        Me.btnClipboardLandscape.TabIndex = 2
        Me.btnClipboardLandscape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClipboardLandscape.UseVisualStyleBackColor = True
        '
        'pnlBanner
        '
        Me.pnlBanner.AutoSize = True
        Me.pnlBanner.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlBanner.Controls.Add(Me.tblBanner)
        Me.pnlBanner.Location = New System.Drawing.Point(3, 457)
        Me.pnlBanner.Name = "pnlBanner"
        Me.pnlBanner.Size = New System.Drawing.Size(264, 125)
        Me.pnlBanner.TabIndex = 6
        '
        'tblBanner
        '
        Me.tblBanner.AutoScroll = True
        Me.tblBanner.AutoSize = True
        Me.tblBanner.ColumnCount = 6
        Me.tblBanner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBanner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBanner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBanner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBanner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBanner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBanner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblBanner.Controls.Add(Me.pbBanner, 0, 1)
        Me.tblBanner.Controls.Add(Me.lblBanner, 0, 0)
        Me.tblBanner.Controls.Add(Me.btnLocalBanner, 2, 3)
        Me.tblBanner.Controls.Add(Me.btnScrapeBanner, 0, 3)
        Me.tblBanner.Controls.Add(Me.lblSizeBanner, 0, 2)
        Me.tblBanner.Controls.Add(Me.btnDLBanner, 1, 3)
        Me.tblBanner.Controls.Add(Me.btnRemoveBanner, 5, 3)
        Me.tblBanner.Controls.Add(Me.btnClipboardBanner, 3, 3)
        Me.tblBanner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBanner.Location = New System.Drawing.Point(0, 0)
        Me.tblBanner.Name = "tblBanner"
        Me.tblBanner.RowCount = 4
        Me.tblBanner.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblBanner.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBanner.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblBanner.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBanner.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblBanner.Size = New System.Drawing.Size(262, 123)
        Me.tblBanner.TabIndex = 0
        '
        'pbBanner
        '
        Me.pbBanner.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pbBanner.BackColor = System.Drawing.Color.White
        Me.tblBanner.SetColumnSpan(Me.pbBanner, 6)
        Me.pbBanner.Location = New System.Drawing.Point(3, 23)
        Me.pbBanner.Name = "pbBanner"
        Me.pbBanner.Size = New System.Drawing.Size(256, 48)
        Me.pbBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbBanner.TabIndex = 1
        Me.pbBanner.TabStop = False
        '
        'lblBanner
        '
        Me.lblBanner.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblBanner.AutoSize = True
        Me.tblBanner.SetColumnSpan(Me.lblBanner, 6)
        Me.lblBanner.Location = New System.Drawing.Point(109, 3)
        Me.lblBanner.Name = "lblBanner"
        Me.lblBanner.Size = New System.Drawing.Size(43, 13)
        Me.lblBanner.TabIndex = 2
        Me.lblBanner.Text = "Banner"
        '
        'btnLocalBanner
        '
        Me.btnLocalBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnLocalBanner.Image = CType(resources.GetObject("btnLocalBanner.Image"), System.Drawing.Image)
        Me.btnLocalBanner.Location = New System.Drawing.Point(61, 97)
        Me.btnLocalBanner.Name = "btnLocalBanner"
        Me.btnLocalBanner.Size = New System.Drawing.Size(23, 23)
        Me.btnLocalBanner.TabIndex = 2
        Me.btnLocalBanner.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLocalBanner.UseVisualStyleBackColor = True
        '
        'btnScrapeBanner
        '
        Me.btnScrapeBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnScrapeBanner.Image = CType(resources.GetObject("btnScrapeBanner.Image"), System.Drawing.Image)
        Me.btnScrapeBanner.Location = New System.Drawing.Point(3, 97)
        Me.btnScrapeBanner.Name = "btnScrapeBanner"
        Me.btnScrapeBanner.Size = New System.Drawing.Size(23, 23)
        Me.btnScrapeBanner.TabIndex = 0
        Me.btnScrapeBanner.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnScrapeBanner.UseVisualStyleBackColor = True
        '
        'lblSizeBanner
        '
        Me.lblSizeBanner.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblSizeBanner.AutoSize = True
        Me.tblBanner.SetColumnSpan(Me.lblSizeBanner, 6)
        Me.lblSizeBanner.Location = New System.Drawing.Point(85, 77)
        Me.lblSizeBanner.Name = "lblSizeBanner"
        Me.lblSizeBanner.Size = New System.Drawing.Size(92, 13)
        Me.lblSizeBanner.TabIndex = 5
        Me.lblSizeBanner.Text = "Size: (XXXXxXXXX)"
        Me.lblSizeBanner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSizeBanner.Visible = False
        '
        'btnDLBanner
        '
        Me.btnDLBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnDLBanner.Image = CType(resources.GetObject("btnDLBanner.Image"), System.Drawing.Image)
        Me.btnDLBanner.Location = New System.Drawing.Point(32, 97)
        Me.btnDLBanner.Name = "btnDLBanner"
        Me.btnDLBanner.Size = New System.Drawing.Size(23, 23)
        Me.btnDLBanner.TabIndex = 1
        Me.btnDLBanner.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDLBanner.UseVisualStyleBackColor = True
        '
        'btnRemoveBanner
        '
        Me.btnRemoveBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveBanner.Image = CType(resources.GetObject("btnRemoveBanner.Image"), System.Drawing.Image)
        Me.btnRemoveBanner.Location = New System.Drawing.Point(236, 97)
        Me.btnRemoveBanner.Name = "btnRemoveBanner"
        Me.btnRemoveBanner.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveBanner.TabIndex = 3
        Me.btnRemoveBanner.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveBanner.UseVisualStyleBackColor = True
        '
        'btnClipboardBanner
        '
        Me.btnClipboardBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClipboardBanner.Image = CType(resources.GetObject("btnClipboardBanner.Image"), System.Drawing.Image)
        Me.btnClipboardBanner.Location = New System.Drawing.Point(90, 97)
        Me.btnClipboardBanner.Name = "btnClipboardBanner"
        Me.btnClipboardBanner.Size = New System.Drawing.Size(23, 23)
        Me.btnClipboardBanner.TabIndex = 2
        Me.btnClipboardBanner.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClipboardBanner.UseVisualStyleBackColor = True
        '
        'pnlClearArt
        '
        Me.pnlClearArt.AutoSize = True
        Me.pnlClearArt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlClearArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlClearArt.Controls.Add(Me.tblClearArt)
        Me.pnlClearArt.Location = New System.Drawing.Point(273, 230)
        Me.pnlClearArt.Name = "pnlClearArt"
        Me.pnlClearArt.Size = New System.Drawing.Size(264, 221)
        Me.pnlClearArt.TabIndex = 2
        '
        'tblClearArt
        '
        Me.tblClearArt.AutoScroll = True
        Me.tblClearArt.AutoSize = True
        Me.tblClearArt.ColumnCount = 6
        Me.tblClearArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblClearArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblClearArt.Controls.Add(Me.pbClearArt, 0, 1)
        Me.tblClearArt.Controls.Add(Me.lblClearArt, 0, 0)
        Me.tblClearArt.Controls.Add(Me.btnLocalClearArt, 2, 3)
        Me.tblClearArt.Controls.Add(Me.btnScrapeClearArt, 0, 3)
        Me.tblClearArt.Controls.Add(Me.lblSizeClearArt, 0, 2)
        Me.tblClearArt.Controls.Add(Me.btnDLClearArt, 1, 3)
        Me.tblClearArt.Controls.Add(Me.btnRemoveClearArt, 5, 3)
        Me.tblClearArt.Controls.Add(Me.btnClipboardClearArt, 3, 3)
        Me.tblClearArt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblClearArt.Location = New System.Drawing.Point(0, 0)
        Me.tblClearArt.Name = "tblClearArt"
        Me.tblClearArt.RowCount = 4
        Me.tblClearArt.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblClearArt.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearArt.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblClearArt.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearArt.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblClearArt.Size = New System.Drawing.Size(262, 219)
        Me.tblClearArt.TabIndex = 0
        '
        'pbClearArt
        '
        Me.pbClearArt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pbClearArt.BackColor = System.Drawing.Color.White
        Me.tblClearArt.SetColumnSpan(Me.pbClearArt, 6)
        Me.pbClearArt.Location = New System.Drawing.Point(3, 23)
        Me.pbClearArt.Name = "pbClearArt"
        Me.pbClearArt.Size = New System.Drawing.Size(256, 144)
        Me.pbClearArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbClearArt.TabIndex = 1
        Me.pbClearArt.TabStop = False
        '
        'lblClearArt
        '
        Me.lblClearArt.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblClearArt.AutoSize = True
        Me.tblClearArt.SetColumnSpan(Me.lblClearArt, 6)
        Me.lblClearArt.Location = New System.Drawing.Point(107, 3)
        Me.lblClearArt.Name = "lblClearArt"
        Me.lblClearArt.Size = New System.Drawing.Size(48, 13)
        Me.lblClearArt.TabIndex = 2
        Me.lblClearArt.Text = "ClearArt"
        '
        'btnLocalClearArt
        '
        Me.btnLocalClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnLocalClearArt.Image = CType(resources.GetObject("btnLocalClearArt.Image"), System.Drawing.Image)
        Me.btnLocalClearArt.Location = New System.Drawing.Point(61, 193)
        Me.btnLocalClearArt.Name = "btnLocalClearArt"
        Me.btnLocalClearArt.Size = New System.Drawing.Size(23, 23)
        Me.btnLocalClearArt.TabIndex = 2
        Me.btnLocalClearArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLocalClearArt.UseVisualStyleBackColor = True
        '
        'btnScrapeClearArt
        '
        Me.btnScrapeClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnScrapeClearArt.Image = CType(resources.GetObject("btnScrapeClearArt.Image"), System.Drawing.Image)
        Me.btnScrapeClearArt.Location = New System.Drawing.Point(3, 193)
        Me.btnScrapeClearArt.Name = "btnScrapeClearArt"
        Me.btnScrapeClearArt.Size = New System.Drawing.Size(23, 23)
        Me.btnScrapeClearArt.TabIndex = 0
        Me.btnScrapeClearArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnScrapeClearArt.UseVisualStyleBackColor = True
        '
        'lblSizeClearArt
        '
        Me.lblSizeClearArt.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblSizeClearArt.AutoSize = True
        Me.tblClearArt.SetColumnSpan(Me.lblSizeClearArt, 6)
        Me.lblSizeClearArt.Location = New System.Drawing.Point(85, 173)
        Me.lblSizeClearArt.Name = "lblSizeClearArt"
        Me.lblSizeClearArt.Size = New System.Drawing.Size(92, 13)
        Me.lblSizeClearArt.TabIndex = 5
        Me.lblSizeClearArt.Text = "Size: (XXXXxXXXX)"
        Me.lblSizeClearArt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSizeClearArt.Visible = False
        '
        'btnDLClearArt
        '
        Me.btnDLClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnDLClearArt.Image = CType(resources.GetObject("btnDLClearArt.Image"), System.Drawing.Image)
        Me.btnDLClearArt.Location = New System.Drawing.Point(32, 193)
        Me.btnDLClearArt.Name = "btnDLClearArt"
        Me.btnDLClearArt.Size = New System.Drawing.Size(23, 23)
        Me.btnDLClearArt.TabIndex = 1
        Me.btnDLClearArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDLClearArt.UseVisualStyleBackColor = True
        '
        'btnRemoveClearArt
        '
        Me.btnRemoveClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveClearArt.Image = CType(resources.GetObject("btnRemoveClearArt.Image"), System.Drawing.Image)
        Me.btnRemoveClearArt.Location = New System.Drawing.Point(236, 193)
        Me.btnRemoveClearArt.Name = "btnRemoveClearArt"
        Me.btnRemoveClearArt.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveClearArt.TabIndex = 3
        Me.btnRemoveClearArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveClearArt.UseVisualStyleBackColor = True
        '
        'btnClipboardClearArt
        '
        Me.btnClipboardClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClipboardClearArt.Image = CType(resources.GetObject("btnClipboardClearArt.Image"), System.Drawing.Image)
        Me.btnClipboardClearArt.Location = New System.Drawing.Point(90, 193)
        Me.btnClipboardClearArt.Name = "btnClipboardClearArt"
        Me.btnClipboardClearArt.Size = New System.Drawing.Size(23, 23)
        Me.btnClipboardClearArt.TabIndex = 2
        Me.btnClipboardClearArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClipboardClearArt.UseVisualStyleBackColor = True
        '
        'pnlKeyart
        '
        Me.pnlKeyart.AutoSize = True
        Me.pnlKeyart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlKeyart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlKeyart.Controls.Add(Me.tblKeyart)
        Me.pnlKeyart.Location = New System.Drawing.Point(273, 3)
        Me.pnlKeyart.Name = "pnlKeyart"
        Me.pnlKeyart.Size = New System.Drawing.Size(264, 221)
        Me.pnlKeyart.TabIndex = 0
        '
        'tblKeyart
        '
        Me.tblKeyart.AutoScroll = True
        Me.tblKeyart.AutoSize = True
        Me.tblKeyart.ColumnCount = 6
        Me.tblKeyart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblKeyart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblKeyart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblKeyart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblKeyart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblKeyart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblKeyart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblKeyart.Controls.Add(Me.pbKeyart, 0, 1)
        Me.tblKeyart.Controls.Add(Me.lblKeyart, 0, 0)
        Me.tblKeyart.Controls.Add(Me.btnLocalKeyart, 2, 3)
        Me.tblKeyart.Controls.Add(Me.btnScrapeKeyart, 0, 3)
        Me.tblKeyart.Controls.Add(Me.lblSizeKeyart, 0, 2)
        Me.tblKeyart.Controls.Add(Me.btnDLKeyart, 1, 3)
        Me.tblKeyart.Controls.Add(Me.btnRemoveKeyart, 5, 3)
        Me.tblKeyart.Controls.Add(Me.btnClipboardKeyart, 3, 3)
        Me.tblKeyart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblKeyart.Location = New System.Drawing.Point(0, 0)
        Me.tblKeyart.Name = "tblKeyart"
        Me.tblKeyart.RowCount = 4
        Me.tblKeyart.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblKeyart.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblKeyart.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblKeyart.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblKeyart.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblKeyart.Size = New System.Drawing.Size(262, 219)
        Me.tblKeyart.TabIndex = 0
        '
        'pbKeyart
        '
        Me.pbKeyart.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pbKeyart.BackColor = System.Drawing.Color.White
        Me.tblKeyart.SetColumnSpan(Me.pbKeyart, 6)
        Me.pbKeyart.Location = New System.Drawing.Point(3, 23)
        Me.pbKeyart.Name = "pbKeyart"
        Me.pbKeyart.Size = New System.Drawing.Size(256, 144)
        Me.pbKeyart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbKeyart.TabIndex = 1
        Me.pbKeyart.TabStop = False
        '
        'lblKeyart
        '
        Me.lblKeyart.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblKeyart.AutoSize = True
        Me.tblKeyart.SetColumnSpan(Me.lblKeyart, 6)
        Me.lblKeyart.Location = New System.Drawing.Point(112, 3)
        Me.lblKeyart.Name = "lblKeyart"
        Me.lblKeyart.Size = New System.Drawing.Size(38, 13)
        Me.lblKeyart.TabIndex = 2
        Me.lblKeyart.Text = "Keyart"
        '
        'btnLocalKeyart
        '
        Me.btnLocalKeyart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnLocalKeyart.Image = CType(resources.GetObject("btnLocalKeyart.Image"), System.Drawing.Image)
        Me.btnLocalKeyart.Location = New System.Drawing.Point(61, 193)
        Me.btnLocalKeyart.Name = "btnLocalKeyart"
        Me.btnLocalKeyart.Size = New System.Drawing.Size(23, 23)
        Me.btnLocalKeyart.TabIndex = 2
        Me.btnLocalKeyart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLocalKeyart.UseVisualStyleBackColor = True
        '
        'btnScrapeKeyart
        '
        Me.btnScrapeKeyart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnScrapeKeyart.Image = CType(resources.GetObject("btnScrapeKeyart.Image"), System.Drawing.Image)
        Me.btnScrapeKeyart.Location = New System.Drawing.Point(3, 193)
        Me.btnScrapeKeyart.Name = "btnScrapeKeyart"
        Me.btnScrapeKeyart.Size = New System.Drawing.Size(23, 23)
        Me.btnScrapeKeyart.TabIndex = 0
        Me.btnScrapeKeyart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnScrapeKeyart.UseVisualStyleBackColor = True
        '
        'lblSizeKeyart
        '
        Me.lblSizeKeyart.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblSizeKeyart.AutoSize = True
        Me.tblKeyart.SetColumnSpan(Me.lblSizeKeyart, 6)
        Me.lblSizeKeyart.Location = New System.Drawing.Point(85, 173)
        Me.lblSizeKeyart.Name = "lblSizeKeyart"
        Me.lblSizeKeyart.Size = New System.Drawing.Size(92, 13)
        Me.lblSizeKeyart.TabIndex = 5
        Me.lblSizeKeyart.Text = "Size: (XXXXxXXXX)"
        Me.lblSizeKeyart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSizeKeyart.Visible = False
        '
        'btnDLKeyart
        '
        Me.btnDLKeyart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnDLKeyart.Image = CType(resources.GetObject("btnDLKeyart.Image"), System.Drawing.Image)
        Me.btnDLKeyart.Location = New System.Drawing.Point(32, 193)
        Me.btnDLKeyart.Name = "btnDLKeyart"
        Me.btnDLKeyart.Size = New System.Drawing.Size(23, 23)
        Me.btnDLKeyart.TabIndex = 1
        Me.btnDLKeyart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDLKeyart.UseVisualStyleBackColor = True
        '
        'btnRemoveKeyart
        '
        Me.btnRemoveKeyart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveKeyart.Image = CType(resources.GetObject("btnRemoveKeyart.Image"), System.Drawing.Image)
        Me.btnRemoveKeyart.Location = New System.Drawing.Point(236, 193)
        Me.btnRemoveKeyart.Name = "btnRemoveKeyart"
        Me.btnRemoveKeyart.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveKeyart.TabIndex = 3
        Me.btnRemoveKeyart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveKeyart.UseVisualStyleBackColor = True
        '
        'btnClipboardKeyart
        '
        Me.btnClipboardKeyart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClipboardKeyart.Image = CType(resources.GetObject("btnClipboardKeyart.Image"), System.Drawing.Image)
        Me.btnClipboardKeyart.Location = New System.Drawing.Point(90, 193)
        Me.btnClipboardKeyart.Name = "btnClipboardKeyart"
        Me.btnClipboardKeyart.Size = New System.Drawing.Size(23, 23)
        Me.btnClipboardKeyart.TabIndex = 2
        Me.btnClipboardKeyart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClipboardKeyart.UseVisualStyleBackColor = True
        '
        'pnlImagesRight
        '
        Me.pnlImagesRight.AutoSize = True
        Me.pnlImagesRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlImagesRight.Controls.Add(Me.tblImagesRight)
        Me.pnlImagesRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlImagesRight.Location = New System.Drawing.Point(1270, 3)
        Me.pnlImagesRight.Name = "pnlImagesRight"
        Me.pnlImagesRight.Size = New System.Drawing.Size(3, 665)
        Me.pnlImagesRight.TabIndex = 4
        '
        'tblImagesRight
        '
        Me.tblImagesRight.AutoSize = True
        Me.tblImagesRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblImagesRight.ColumnCount = 2
        Me.tblImagesRight.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImagesRight.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImagesRight.Location = New System.Drawing.Point(0, 0)
        Me.tblImagesRight.Name = "tblImagesRight"
        Me.tblImagesRight.RowCount = 1
        Me.tblImagesRight.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImagesRight.Size = New System.Drawing.Size(0, 0)
        Me.tblImagesRight.TabIndex = 2
        '
        'dlgEdit_TVShow
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(1284, 831)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.StatusStrip)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgEdit_TVShow"
        Me.Text = "Edit Show"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tblTop.ResumeLayout(False)
        Me.tblTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.tblBottom.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.tcEdit.ResumeLayout(False)
        Me.tpDetails.ResumeLayout(False)
        Me.tpDetails.PerformLayout()
        Me.tblDetails.ResumeLayout(False)
        Me.tblDetails.PerformLayout()
        CType(Me.dgvCertifications, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvRatings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpDetails2.ResumeLayout(False)
        Me.tpDetails2.PerformLayout()
        Me.tblDetails2.ResumeLayout(False)
        Me.tblDetails2.PerformLayout()
        CType(Me.dgvUniqueIds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCountries, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvStudios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpCastCrew.ResumeLayout(False)
        Me.tpCastCrew.PerformLayout()
        Me.tblCastCrew.ResumeLayout(False)
        Me.tblCastCrew.PerformLayout()
        CType(Me.dgvCreators, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpOther.ResumeLayout(False)
        Me.tpOther.PerformLayout()
        Me.tblOther.ResumeLayout(False)
        Me.tblOther.PerformLayout()
        Me.gbTheme.ResumeLayout(False)
        Me.gbTheme.PerformLayout()
        Me.tblTheme.ResumeLayout(False)
        Me.tblTheme.PerformLayout()
        Me.tpImages.ResumeLayout(False)
        Me.tpImages.PerformLayout()
        Me.tblImages.ResumeLayout(False)
        Me.tblImages.PerformLayout()
        Me.pnlExtrafanarts.ResumeLayout(False)
        Me.pnlExtrafanarts.PerformLayout()
        Me.tblExtrafanarts.ResumeLayout(False)
        Me.tblExtrafanarts.PerformLayout()
        Me.pnlPoster.ResumeLayout(False)
        Me.pnlPoster.PerformLayout()
        Me.tblPoster.ResumeLayout(False)
        Me.tblPoster.PerformLayout()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCharacterArt.ResumeLayout(False)
        Me.pnlCharacterArt.PerformLayout()
        Me.tblCharacterArt.ResumeLayout(False)
        Me.tblCharacterArt.PerformLayout()
        CType(Me.pbCharacterArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlClearLogo.ResumeLayout(False)
        Me.pnlClearLogo.PerformLayout()
        Me.tblClearLogo.ResumeLayout(False)
        Me.tblClearLogo.PerformLayout()
        CType(Me.pbClearLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFanart.ResumeLayout(False)
        Me.pnlFanart.PerformLayout()
        Me.tblFanart.ResumeLayout(False)
        Me.tblFanart.PerformLayout()
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLandscape.ResumeLayout(False)
        Me.pnlLandscape.PerformLayout()
        Me.tblLandscape.ResumeLayout(False)
        Me.tblLandscape.PerformLayout()
        CType(Me.pbLandscape, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBanner.ResumeLayout(False)
        Me.pnlBanner.PerformLayout()
        Me.tblBanner.ResumeLayout(False)
        Me.tblBanner.PerformLayout()
        CType(Me.pbBanner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlClearArt.ResumeLayout(False)
        Me.pnlClearArt.PerformLayout()
        Me.tblClearArt.ResumeLayout(False)
        Me.tblClearArt.PerformLayout()
        CType(Me.pbClearArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlKeyart.ResumeLayout(False)
        Me.pnlKeyart.PerformLayout()
        Me.tblKeyart.ResumeLayout(False)
        Me.tblKeyart.PerformLayout()
        CType(Me.pbKeyart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlImagesRight.ResumeLayout(False)
        Me.pnlImagesRight.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents ofdLocalFiles As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cbEpisodeOrdering As System.Windows.Forms.ComboBox
    Friend WithEvents lblEpisodeOrdering As System.Windows.Forms.Label
    Friend WithEvents cbEpisodeSorting As System.Windows.Forms.ComboBox
    Friend WithEvents lblEpisodeSorting As System.Windows.Forms.Label
    Friend WithEvents lblLanguage As System.Windows.Forms.Label
    Friend WithEvents cbSourceLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents tblTop As TableLayoutPanel
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents tblBottom As TableLayoutPanel
    Friend WithEvents chkLocked As CheckBox
    Friend WithEvents chkMarked As CheckBox
    Friend WithEvents chkMarkedCustom1 As CheckBox
    Friend WithEvents chkMarkedCustom3 As CheckBox
    Friend WithEvents chkMarkedCustom2 As CheckBox
    Friend WithEvents chkMarkedCustom4 As CheckBox
    Friend WithEvents btnRescrape As Button
    Friend WithEvents btnChange As Button
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents tsslFilename As ToolStripStatusLabel
    Friend WithEvents tsslSpring As ToolStripStatusLabel
    Friend WithEvents tsslStatus As ToolStripStatusLabel
    Friend WithEvents tspbStatus As ToolStripProgressBar
    Friend WithEvents pnlMain As Panel
    Friend WithEvents tcEdit As TabControl
    Friend WithEvents tpDetails As TabPage
    Friend WithEvents tblDetails As TableLayoutPanel
    Friend WithEvents lblTitle As Label
    Friend WithEvents txtTitle As TextBox
    Friend WithEvents lblOriginalTitle As Label
    Friend WithEvents txtOriginalTitle As TextBox
    Friend WithEvents lblSortTilte As Label
    Friend WithEvents txtSortTitle As TextBox
    Friend WithEvents lblTagline As Label
    Friend WithEvents txtTagline As TextBox
    Friend WithEvents lblPlot As Label
    Friend WithEvents txtPlot As TextBox
    Friend WithEvents lblPremiered As Label
    Friend WithEvents dtpPremiered As DateTimePicker
    Friend WithEvents cbUserRating As ComboBox
    Friend WithEvents lblRatings As Label
    Friend WithEvents lblCertifications As Label
    Friend WithEvents dgvCertifications As DataGridView
    Friend WithEvents dgvRatings As DataGridView
    Friend WithEvents colRatingsDefault As DataGridViewCheckBoxColumn
    Friend WithEvents colRatingsSource As DataGridViewTextBoxColumn
    Friend WithEvents colRatingsValue As DataGridViewTextBoxColumn
    Friend WithEvents colRatingsMax As DataGridViewTextBoxColumn
    Friend WithEvents colRatingsVotes As DataGridViewTextBoxColumn
    Friend WithEvents lblMPAA As Label
    Friend WithEvents lbMPAA As ListBox
    Friend WithEvents btnCertificationsAsMPAARating As Button
    Friend WithEvents lblMPAADesc As Label
    Friend WithEvents txtMPAADescription As TextBox
    Friend WithEvents txtMPAA As EmberAPI.AdvancedControls.TextBox_with_Watermark
    Friend WithEvents lblUserRating As Label
    Friend WithEvents lblRuntime As Label
    Friend WithEvents txtRuntime As TextBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents txtStatus As TextBox
    Friend WithEvents tpDetails2 As TabPage
    Friend WithEvents tblDetails2 As TableLayoutPanel
    Friend WithEvents lblDateAdded As Label
    Friend WithEvents dtpDateAdded_Date As DateTimePicker
    Friend WithEvents chkWatched As CheckBox
    Friend WithEvents dtpLastPlayed_Date As DateTimePicker
    Friend WithEvents lblCountries As Label
    Friend WithEvents dgvCountries As DataGridView
    Friend WithEvents colCountriesName As DataGridViewTextBoxColumn
    Friend WithEvents lblStudios As Label
    Friend WithEvents dgvStudios As DataGridView
    Friend WithEvents colStudiosName As DataGridViewTextBoxColumn
    Friend WithEvents lblTags As Label
    Friend WithEvents dtpDateAdded_Time As DateTimePicker
    Friend WithEvents dtpLastPlayed_Time As DateTimePicker
    Friend WithEvents lblGenres As Label
    Friend WithEvents lblUniqueIds As Label
    Friend WithEvents dgvUniqueIds As DataGridView
    Friend WithEvents colUniqueIdsDefault As DataGridViewCheckBoxColumn
    Friend WithEvents colUniqueIdsType As DataGridViewTextBoxColumn
    Friend WithEvents colUniqueIdsValue As DataGridViewTextBoxColumn
    Friend WithEvents tpCastCrew As TabPage
    Friend WithEvents tblCastCrew As TableLayoutPanel
    Friend WithEvents lblActors As Label
    Friend WithEvents lvActors As ListView
    Friend WithEvents colActorsID As ColumnHeader
    Friend WithEvents colActorsName As ColumnHeader
    Friend WithEvents colActorsRole As ColumnHeader
    Friend WithEvents colActorsThumb As ColumnHeader
    Friend WithEvents btnActorsAdd As Button
    Friend WithEvents btnActorsRemove As Button
    Friend WithEvents btnActorsEdit As Button
    Friend WithEvents btnActorsDown As Button
    Friend WithEvents btnActorsUp As Button
    Friend WithEvents dgvCreators As DataGridView
    Friend WithEvents colCreditsName As DataGridViewTextBoxColumn
    Friend WithEvents lblCreators As Label
    Friend WithEvents tpOther As TabPage
    Friend WithEvents tblOther As TableLayoutPanel
    Friend WithEvents gbTheme As GroupBox
    Friend WithEvents tblTheme As TableLayoutPanel
    Friend WithEvents btnRemoveTheme As Button
    Friend WithEvents btnLocalThemePlay As Button
    Friend WithEvents txtLocalTheme As TextBox
    Friend WithEvents btnSetThemeLocal As Button
    Friend WithEvents btnSetThemeScrape As Button
    Friend WithEvents btnSetThemeDL As Button
    Friend WithEvents tpImages As TabPage
    Friend WithEvents tblImages As TableLayoutPanel
    Friend WithEvents pnlExtrafanarts As Panel
    Friend WithEvents tblExtrafanarts As TableLayoutPanel
    Friend WithEvents btnRemoveExtrafanarts As Button
    Friend WithEvents lblExtrafanarts As Label
    Friend WithEvents pnlExtrafanartsList As Panel
    Friend WithEvents btnLocalExtrafanarts As Button
    Friend WithEvents btnRefreshExtrafanarts As Button
    Friend WithEvents btnScrapeExtrafanarts As Button
    Friend WithEvents btnDLExtrafanarts As Button
    Friend WithEvents btnClipboardExtrafanarts As Button
    Friend WithEvents pnlPoster As Panel
    Friend WithEvents tblPoster As TableLayoutPanel
    Friend WithEvents pbPoster As PictureBox
    Friend WithEvents lblPoster As Label
    Friend WithEvents btnLocalPoster As Button
    Friend WithEvents btnScrapePoster As Button
    Friend WithEvents lblSizePoster As Label
    Friend WithEvents btnDLPoster As Button
    Friend WithEvents btnRemovePoster As Button
    Friend WithEvents btnClipboardPoster As Button
    Friend WithEvents pnlCharacterArt As Panel
    Friend WithEvents tblCharacterArt As TableLayoutPanel
    Friend WithEvents pbCharacterArt As PictureBox
    Friend WithEvents lblCharacterArt As Label
    Friend WithEvents btnLocalCharacterArt As Button
    Friend WithEvents btnScrapeCharacterArt As Button
    Friend WithEvents lblSizeCharacterArt As Label
    Friend WithEvents btnDLCharacterArt As Button
    Friend WithEvents btnRemoveCharacterArt As Button
    Friend WithEvents btnClipboardCharacterArt As Button
    Friend WithEvents pnlClearLogo As Panel
    Friend WithEvents tblClearLogo As TableLayoutPanel
    Friend WithEvents pbClearLogo As PictureBox
    Friend WithEvents lblClearLogo As Label
    Friend WithEvents btnLocalClearLogo As Button
    Friend WithEvents btnScrapeClearLogo As Button
    Friend WithEvents lblSizeClearLogo As Label
    Friend WithEvents btnDLClearLogo As Button
    Friend WithEvents btnRemoveClearLogo As Button
    Friend WithEvents btnClipboardClearLogo As Button
    Friend WithEvents pnlFanart As Panel
    Friend WithEvents tblFanart As TableLayoutPanel
    Friend WithEvents pbFanart As PictureBox
    Friend WithEvents lblFanart As Label
    Friend WithEvents btnLocalFanart As Button
    Friend WithEvents btnScrapeFanart As Button
    Friend WithEvents lblSizeFanart As Label
    Friend WithEvents btnDLFanart As Button
    Friend WithEvents btnRemoveFanart As Button
    Friend WithEvents btnClipboardFanart As Button
    Friend WithEvents pnlLandscape As Panel
    Friend WithEvents tblLandscape As TableLayoutPanel
    Friend WithEvents pbLandscape As PictureBox
    Friend WithEvents lblLandscape As Label
    Friend WithEvents btnLocalLandscape As Button
    Friend WithEvents btnScrapeLandscape As Button
    Friend WithEvents lblSizeLandscape As Label
    Friend WithEvents btnDLLandscape As Button
    Friend WithEvents btnRemoveLandscape As Button
    Friend WithEvents btnClipboardLandscape As Button
    Friend WithEvents pnlBanner As Panel
    Friend WithEvents tblBanner As TableLayoutPanel
    Friend WithEvents pbBanner As PictureBox
    Friend WithEvents lblBanner As Label
    Friend WithEvents btnLocalBanner As Button
    Friend WithEvents btnScrapeBanner As Button
    Friend WithEvents lblSizeBanner As Label
    Friend WithEvents btnDLBanner As Button
    Friend WithEvents btnRemoveBanner As Button
    Friend WithEvents btnClipboardBanner As Button
    Friend WithEvents pnlClearArt As Panel
    Friend WithEvents tblClearArt As TableLayoutPanel
    Friend WithEvents pbClearArt As PictureBox
    Friend WithEvents lblClearArt As Label
    Friend WithEvents btnLocalClearArt As Button
    Friend WithEvents btnScrapeClearArt As Button
    Friend WithEvents lblSizeClearArt As Label
    Friend WithEvents btnDLClearArt As Button
    Friend WithEvents btnRemoveClearArt As Button
    Friend WithEvents btnClipboardClearArt As Button
    Friend WithEvents pnlKeyart As Panel
    Friend WithEvents tblKeyart As TableLayoutPanel
    Friend WithEvents pbKeyart As PictureBox
    Friend WithEvents lblKeyart As Label
    Friend WithEvents btnLocalKeyart As Button
    Friend WithEvents btnScrapeKeyart As Button
    Friend WithEvents lblSizeKeyart As Label
    Friend WithEvents btnDLKeyart As Button
    Friend WithEvents btnRemoveKeyart As Button
    Friend WithEvents btnClipboardKeyart As Button
    Friend WithEvents pnlImagesRight As Panel
    Friend WithEvents tblImagesRight As TableLayoutPanel
    Friend WithEvents lblUserNote As Label
    Friend WithEvents txtUserNote As TextBox
    Friend WithEvents lbGenres As ListBox
    Friend WithEvents btnGenres_Add As Button
    Friend WithEvents btnGenres_Remove As Button
    Friend WithEvents btnGenres_Up As Button
    Friend WithEvents btnGenres_Down As Button
    Friend WithEvents lbTags As ListBox
    Friend WithEvents cbGenres As ComboBox
    Friend WithEvents cbTags As ComboBox
    Friend WithEvents btnTags_Add As Button
    Friend WithEvents btnTags_Remove As Button
    Friend WithEvents btnTags_Up As Button
    Friend WithEvents btnTags_Down As Button
    Friend WithEvents colCertificationsName As DataGridViewTextBoxColumn
End Class
