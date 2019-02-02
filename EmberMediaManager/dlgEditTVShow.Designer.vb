<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgEditTVShow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgEditTVShow))
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
        Me.dgvCertifications = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnManual = New System.Windows.Forms.Button()
        Me.txtMPAA = New System.Windows.Forms.TextBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblCertifications = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.lbMPAA = New System.Windows.Forms.ListBox()
        Me.lblOriginalTitle = New System.Windows.Forms.Label()
        Me.lblMPAA = New System.Windows.Forms.Label()
        Me.txtOriginalTitle = New System.Windows.Forms.TextBox()
        Me.lblSortTilte = New System.Windows.Forms.Label()
        Me.txtSortTitle = New System.Windows.Forms.TextBox()
        Me.clbGenres = New System.Windows.Forms.CheckedListBox()
        Me.lvActors = New System.Windows.Forms.ListView()
        Me.colActorsID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colActorsName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colActorsRole = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colActorsThumb = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblActors = New System.Windows.Forms.Label()
        Me.lblCreators = New System.Windows.Forms.Label()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.lblPremiered = New System.Windows.Forms.Label()
        Me.dtpPremiered = New System.Windows.Forms.DateTimePicker()
        Me.lblRuntime = New System.Windows.Forms.Label()
        Me.txtRuntime = New System.Windows.Forms.TextBox()
        Me.lblGenres = New System.Windows.Forms.Label()
        Me.lblTags = New System.Windows.Forms.Label()
        Me.clbTags = New System.Windows.Forms.CheckedListBox()
        Me.dgvCreators = New System.Windows.Forms.DataGridView()
        Me.colCreditsName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvStudios = New System.Windows.Forms.DataGridView()
        Me.colStudiosName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblStudios = New System.Windows.Forms.Label()
        Me.lblCountries = New System.Windows.Forms.Label()
        Me.dgvCountries = New System.Windows.Forms.DataGridView()
        Me.colCountriesName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnActorsAdd = New System.Windows.Forms.Button()
        Me.btnActorsEdit = New System.Windows.Forms.Button()
        Me.btnActorsUp = New System.Windows.Forms.Button()
        Me.btnActorsDown = New System.Windows.Forms.Button()
        Me.btnActorsRemove = New System.Windows.Forms.Button()
        Me.lblUserRating = New System.Windows.Forms.Label()
        Me.txtUserRating = New System.Windows.Forms.TextBox()
        Me.chkWatched = New System.Windows.Forms.CheckBox()
        Me.dtpLastPlayed = New System.Windows.Forms.DateTimePicker()
        Me.lblRatings = New System.Windows.Forms.Label()
        Me.lvRatings = New System.Windows.Forms.ListView()
        Me.colRatingsName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRatingsValue = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRatingsVotes = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRatingsMax = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnRatingsAdd = New System.Windows.Forms.Button()
        Me.btnRatingsEdit = New System.Windows.Forms.Button()
        Me.btnRatingsRemove = New System.Windows.Forms.Button()
        Me.btnCertificationsAsMPAARating = New System.Windows.Forms.Button()
        Me.lblMPAADesc = New System.Windows.Forms.Label()
        Me.txtMPAADesc = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.lblPlot = New System.Windows.Forms.Label()
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
        Me.pnlPoster = New System.Windows.Forms.Panel()
        Me.tblPoster = New System.Windows.Forms.TableLayoutPanel()
        Me.pbPoster = New System.Windows.Forms.PictureBox()
        Me.lblPoster = New System.Windows.Forms.Label()
        Me.btnSetPosterLocal = New System.Windows.Forms.Button()
        Me.btnSetPosterScrape = New System.Windows.Forms.Button()
        Me.lblPosterSize = New System.Windows.Forms.Label()
        Me.btnSetPosterDL = New System.Windows.Forms.Button()
        Me.btnRemovePoster = New System.Windows.Forms.Button()
        Me.pnlCharacterArt = New System.Windows.Forms.Panel()
        Me.tblCharacterArt = New System.Windows.Forms.TableLayoutPanel()
        Me.pbCharacterArt = New System.Windows.Forms.PictureBox()
        Me.lblCharacterArt = New System.Windows.Forms.Label()
        Me.btnSetCharacterArtLocal = New System.Windows.Forms.Button()
        Me.btnSetCharacterArtScrape = New System.Windows.Forms.Button()
        Me.lblCharacterArtSize = New System.Windows.Forms.Label()
        Me.btnSetCharacterArtDL = New System.Windows.Forms.Button()
        Me.btnRemoveCharacterArt = New System.Windows.Forms.Button()
        Me.pnlClearLogo = New System.Windows.Forms.Panel()
        Me.tblClearLogo = New System.Windows.Forms.TableLayoutPanel()
        Me.pbClearLogo = New System.Windows.Forms.PictureBox()
        Me.lblClearLogo = New System.Windows.Forms.Label()
        Me.btnSetClearLogoLocal = New System.Windows.Forms.Button()
        Me.btnSetClearLogoScrape = New System.Windows.Forms.Button()
        Me.lblClearLogoSize = New System.Windows.Forms.Label()
        Me.btnSetClearLogoDL = New System.Windows.Forms.Button()
        Me.btnRemoveClearLogo = New System.Windows.Forms.Button()
        Me.pnlFanart = New System.Windows.Forms.Panel()
        Me.tblFanart = New System.Windows.Forms.TableLayoutPanel()
        Me.pbFanart = New System.Windows.Forms.PictureBox()
        Me.lblFanart = New System.Windows.Forms.Label()
        Me.btnSetFanartLocal = New System.Windows.Forms.Button()
        Me.btnSetFanartScrape = New System.Windows.Forms.Button()
        Me.lblFanartSize = New System.Windows.Forms.Label()
        Me.btnSetFanartDL = New System.Windows.Forms.Button()
        Me.btnRemoveFanart = New System.Windows.Forms.Button()
        Me.pnlLandscape = New System.Windows.Forms.Panel()
        Me.tblLandscape = New System.Windows.Forms.TableLayoutPanel()
        Me.pbLandscape = New System.Windows.Forms.PictureBox()
        Me.lblLandscape = New System.Windows.Forms.Label()
        Me.btnSetLandscapeLocal = New System.Windows.Forms.Button()
        Me.btnSetLandscapeScrape = New System.Windows.Forms.Button()
        Me.lblLandscapeSize = New System.Windows.Forms.Label()
        Me.btnSetLandscapeDL = New System.Windows.Forms.Button()
        Me.btnRemoveLandscape = New System.Windows.Forms.Button()
        Me.pnlBanner = New System.Windows.Forms.Panel()
        Me.tblBanner = New System.Windows.Forms.TableLayoutPanel()
        Me.pbBanner = New System.Windows.Forms.PictureBox()
        Me.lblBanner = New System.Windows.Forms.Label()
        Me.btnSetBannerLocal = New System.Windows.Forms.Button()
        Me.btnSetBannerScrape = New System.Windows.Forms.Button()
        Me.lblBannerSize = New System.Windows.Forms.Label()
        Me.btnSetBannerDL = New System.Windows.Forms.Button()
        Me.btnRemoveBanner = New System.Windows.Forms.Button()
        Me.pnlClearArt = New System.Windows.Forms.Panel()
        Me.tblClearArt = New System.Windows.Forms.TableLayoutPanel()
        Me.pbClearArt = New System.Windows.Forms.PictureBox()
        Me.lblClearArt = New System.Windows.Forms.Label()
        Me.btnSetClearArtLocal = New System.Windows.Forms.Button()
        Me.btnSetClearArtScrape = New System.Windows.Forms.Button()
        Me.lblClearArtSize = New System.Windows.Forms.Label()
        Me.btnSetClearArtDL = New System.Windows.Forms.Button()
        Me.btnRemoveClearArt = New System.Windows.Forms.Button()
        Me.pnlImagesRight = New System.Windows.Forms.Panel()
        Me.tblImagesRight = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlExtrafanarts = New System.Windows.Forms.Panel()
        Me.tblExtrafanarts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnExtrafanartsRemove = New System.Windows.Forms.Button()
        Me.lblExtrafanarts = New System.Windows.Forms.Label()
        Me.pnlExtrafanartsList = New System.Windows.Forms.Panel()
        Me.btnSetExtrafanartsLocal = New System.Windows.Forms.Button()
        Me.btnExtrafanartsRefresh = New System.Windows.Forms.Button()
        Me.btnSetExtrafanartsScrape = New System.Windows.Forms.Button()
        Me.btnSetExtrafanartsDL = New System.Windows.Forms.Button()
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
        CType(Me.dgvCreators, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvStudios, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCountries, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpOther.SuspendLayout()
        Me.tblOther.SuspendLayout()
        Me.gbTheme.SuspendLayout()
        Me.tblTheme.SuspendLayout()
        Me.tpImages.SuspendLayout()
        Me.tblImages.SuspendLayout()
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
        Me.pnlImagesRight.SuspendLayout()
        Me.tblImagesRight.SuspendLayout()
        Me.pnlExtrafanarts.SuspendLayout()
        Me.tblExtrafanarts.SuspendLayout()
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
        Me.pnlTop.Size = New System.Drawing.Size(1104, 56)
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
        Me.tblTop.Size = New System.Drawing.Size(1102, 54)
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
        Me.btnCancel.Location = New System.Drawing.Point(1025, 30)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(949, 30)
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
        Me.pnlBottom.Location = New System.Drawing.Point(0, 713)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1104, 56)
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
        Me.tblBottom.Size = New System.Drawing.Size(1104, 56)
        Me.tblBottom.TabIndex = 0
        '
        'btnChange
        '
        Me.btnChange.AutoSize = True
        Me.btnChange.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnChange.Image = CType(resources.GetObject("btnChange.Image"), System.Drawing.Image)
        Me.btnChange.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnChange.Location = New System.Drawing.Point(822, 30)
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
        Me.btnRescrape.Location = New System.Drawing.Point(718, 30)
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
        '
        'StatusStrip
        '
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(18, 18)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslFilename, Me.tsslSpring, Me.tsslStatus, Me.tspbStatus})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 769)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1104, 22)
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
        Me.tsslSpring.Size = New System.Drawing.Size(1034, 17)
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
        Me.pnlMain.Size = New System.Drawing.Size(1104, 657)
        Me.pnlMain.TabIndex = 78
        '
        'tcEdit
        '
        Me.tcEdit.Controls.Add(Me.tpDetails)
        Me.tcEdit.Controls.Add(Me.tpOther)
        Me.tcEdit.Controls.Add(Me.tpImages)
        Me.tcEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tcEdit.Location = New System.Drawing.Point(0, 0)
        Me.tcEdit.Name = "tcEdit"
        Me.tcEdit.SelectedIndex = 0
        Me.tcEdit.Size = New System.Drawing.Size(1104, 657)
        Me.tcEdit.TabIndex = 0
        '
        'tpDetails
        '
        Me.tpDetails.Controls.Add(Me.tblDetails)
        Me.tpDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpDetails.Name = "tpDetails"
        Me.tpDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDetails.Size = New System.Drawing.Size(1096, 631)
        Me.tpDetails.TabIndex = 0
        Me.tpDetails.Text = "Details"
        Me.tpDetails.UseVisualStyleBackColor = True
        '
        'tblDetails
        '
        Me.tblDetails.AutoScroll = True
        Me.tblDetails.AutoSize = True
        Me.tblDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblDetails.ColumnCount = 16
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.Controls.Add(Me.dgvCertifications, 6, 19)
        Me.tblDetails.Controls.Add(Me.btnManual, 12, 1)
        Me.tblDetails.Controls.Add(Me.txtMPAA, 7, 23)
        Me.tblDetails.Controls.Add(Me.lblTitle, 0, 0)
        Me.tblDetails.Controls.Add(Me.lblCertifications, 6, 18)
        Me.tblDetails.Controls.Add(Me.txtTitle, 0, 1)
        Me.tblDetails.Controls.Add(Me.lbMPAA, 7, 19)
        Me.tblDetails.Controls.Add(Me.lblOriginalTitle, 0, 2)
        Me.tblDetails.Controls.Add(Me.lblMPAA, 7, 18)
        Me.tblDetails.Controls.Add(Me.txtOriginalTitle, 0, 3)
        Me.tblDetails.Controls.Add(Me.lblSortTilte, 0, 4)
        Me.tblDetails.Controls.Add(Me.txtSortTitle, 0, 5)
        Me.tblDetails.Controls.Add(Me.clbGenres, 0, 14)
        Me.tblDetails.Controls.Add(Me.lvActors, 7, 14)
        Me.tblDetails.Controls.Add(Me.lblActors, 7, 13)
        Me.tblDetails.Controls.Add(Me.lblCreators, 6, 13)
        Me.tblDetails.Controls.Add(Me.txtPlot, 2, 1)
        Me.tblDetails.Controls.Add(Me.lblPremiered, 0, 6)
        Me.tblDetails.Controls.Add(Me.dtpPremiered, 0, 7)
        Me.tblDetails.Controls.Add(Me.lblRuntime, 0, 8)
        Me.tblDetails.Controls.Add(Me.txtRuntime, 0, 9)
        Me.tblDetails.Controls.Add(Me.lblGenres, 0, 13)
        Me.tblDetails.Controls.Add(Me.lblTags, 0, 15)
        Me.tblDetails.Controls.Add(Me.clbTags, 0, 16)
        Me.tblDetails.Controls.Add(Me.dgvCreators, 6, 14)
        Me.tblDetails.Controls.Add(Me.dgvStudios, 2, 16)
        Me.tblDetails.Controls.Add(Me.lblStudios, 2, 15)
        Me.tblDetails.Controls.Add(Me.lblCountries, 2, 13)
        Me.tblDetails.Controls.Add(Me.dgvCountries, 2, 14)
        Me.tblDetails.Controls.Add(Me.btnActorsAdd, 7, 17)
        Me.tblDetails.Controls.Add(Me.btnActorsEdit, 8, 17)
        Me.tblDetails.Controls.Add(Me.btnActorsUp, 10, 17)
        Me.tblDetails.Controls.Add(Me.btnActorsDown, 11, 17)
        Me.tblDetails.Controls.Add(Me.btnActorsRemove, 14, 17)
        Me.tblDetails.Controls.Add(Me.lblUserRating, 0, 18)
        Me.tblDetails.Controls.Add(Me.txtUserRating, 0, 19)
        Me.tblDetails.Controls.Add(Me.chkWatched, 0, 22)
        Me.tblDetails.Controls.Add(Me.dtpLastPlayed, 0, 23)
        Me.tblDetails.Controls.Add(Me.lblRatings, 2, 18)
        Me.tblDetails.Controls.Add(Me.lvRatings, 2, 19)
        Me.tblDetails.Controls.Add(Me.btnRatingsAdd, 2, 23)
        Me.tblDetails.Controls.Add(Me.btnRatingsEdit, 3, 23)
        Me.tblDetails.Controls.Add(Me.btnRatingsRemove, 5, 23)
        Me.tblDetails.Controls.Add(Me.btnCertificationsAsMPAARating, 6, 23)
        Me.tblDetails.Controls.Add(Me.lblMPAADesc, 10, 18)
        Me.tblDetails.Controls.Add(Me.txtMPAADesc, 10, 19)
        Me.tblDetails.Controls.Add(Me.lblStatus, 1, 8)
        Me.tblDetails.Controls.Add(Me.txtStatus, 1, 9)
        Me.tblDetails.Controls.Add(Me.lblPlot, 2, 0)
        Me.tblDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblDetails.Location = New System.Drawing.Point(3, 3)
        Me.tblDetails.Name = "tblDetails"
        Me.tblDetails.RowCount = 25
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
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.Size = New System.Drawing.Size(1090, 625)
        Me.tblDetails.TabIndex = 78
        '
        'dgvCertifications
        '
        Me.dgvCertifications.AllowUserToResizeColumns = False
        Me.dgvCertifications.AllowUserToResizeRows = False
        Me.dgvCertifications.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvCertifications.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvCertifications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCertifications.ColumnHeadersVisible = False
        Me.dgvCertifications.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1})
        Me.dgvCertifications.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCertifications.Location = New System.Drawing.Point(475, 470)
        Me.dgvCertifications.Name = "dgvCertifications"
        Me.dgvCertifications.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.tblDetails.SetRowSpan(Me.dgvCertifications, 4)
        Me.dgvCertifications.Size = New System.Drawing.Size(240, 106)
        Me.dgvCertifications.TabIndex = 35
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'btnManual
        '
        Me.tblDetails.SetColumnSpan(Me.btnManual, 3)
        Me.btnManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnManual.Enabled = False
        Me.btnManual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnManual.Location = New System.Drawing.Point(965, 16)
        Me.btnManual.Name = "btnManual"
        Me.btnManual.Size = New System.Drawing.Size(122, 23)
        Me.btnManual.TabIndex = 8
        Me.btnManual.Text = "Manual Edit"
        Me.btnManual.UseVisualStyleBackColor = True
        Me.btnManual.Visible = False
        '
        'txtMPAA
        '
        Me.txtMPAA.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtMPAA, 8)
        Me.txtMPAA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtMPAA.Location = New System.Drawing.Point(721, 582)
        Me.txtMPAA.Name = "txtMPAA"
        Me.txtMPAA.Size = New System.Drawing.Size(366, 22)
        Me.txtMPAA.TabIndex = 39
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTitle.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblTitle, 2)
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(3, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(31, 13)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Title:"
        '
        'lblCertifications
        '
        Me.lblCertifications.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCertifications.AutoSize = True
        Me.lblCertifications.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCertifications.Location = New System.Drawing.Point(475, 454)
        Me.lblCertifications.Name = "lblCertifications"
        Me.lblCertifications.Size = New System.Drawing.Size(78, 13)
        Me.lblCertifications.TabIndex = 45
        Me.lblCertifications.Text = "Certifications:"
        '
        'txtTitle
        '
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtTitle, 2)
        Me.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(3, 16)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(220, 22)
        Me.txtTitle.TabIndex = 0
        '
        'lbMPAA
        '
        Me.lbMPAA.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.lbMPAA, 3)
        Me.lbMPAA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbMPAA.FormattingEnabled = True
        Me.lbMPAA.Location = New System.Drawing.Point(721, 470)
        Me.lbMPAA.Name = "lbMPAA"
        Me.tblDetails.SetRowSpan(Me.lbMPAA, 4)
        Me.lbMPAA.Size = New System.Drawing.Size(180, 106)
        Me.lbMPAA.TabIndex = 37
        '
        'lblOriginalTitle
        '
        Me.lblOriginalTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOriginalTitle.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblOriginalTitle, 2)
        Me.lblOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblOriginalTitle.Location = New System.Drawing.Point(3, 42)
        Me.lblOriginalTitle.Name = "lblOriginalTitle"
        Me.lblOriginalTitle.Size = New System.Drawing.Size(76, 13)
        Me.lblOriginalTitle.TabIndex = 2
        Me.lblOriginalTitle.Text = "Original Title:"
        '
        'lblMPAA
        '
        Me.lblMPAA.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblMPAA.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblMPAA, 3)
        Me.lblMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMPAA.Location = New System.Drawing.Point(721, 454)
        Me.lblMPAA.Name = "lblMPAA"
        Me.lblMPAA.Size = New System.Drawing.Size(76, 13)
        Me.lblMPAA.TabIndex = 36
        Me.lblMPAA.Text = "MPAA Rating:"
        '
        'txtOriginalTitle
        '
        Me.txtOriginalTitle.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtOriginalTitle, 2)
        Me.txtOriginalTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtOriginalTitle.Location = New System.Drawing.Point(3, 58)
        Me.txtOriginalTitle.Name = "txtOriginalTitle"
        Me.txtOriginalTitle.Size = New System.Drawing.Size(220, 22)
        Me.txtOriginalTitle.TabIndex = 1
        '
        'lblSortTilte
        '
        Me.lblSortTilte.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSortTilte.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblSortTilte, 2)
        Me.lblSortTilte.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSortTilte.Location = New System.Drawing.Point(3, 83)
        Me.lblSortTilte.Name = "lblSortTilte"
        Me.lblSortTilte.Size = New System.Drawing.Size(55, 13)
        Me.lblSortTilte.TabIndex = 4
        Me.lblSortTilte.Text = "Sort Title:"
        '
        'txtSortTitle
        '
        Me.txtSortTitle.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtSortTitle, 2)
        Me.txtSortTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSortTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSortTitle.Location = New System.Drawing.Point(3, 99)
        Me.txtSortTitle.Name = "txtSortTitle"
        Me.txtSortTitle.Size = New System.Drawing.Size(220, 22)
        Me.txtSortTitle.TabIndex = 2
        '
        'clbGenres
        '
        Me.clbGenres.BackColor = System.Drawing.SystemColors.Window
        Me.clbGenres.CheckOnClick = True
        Me.tblDetails.SetColumnSpan(Me.clbGenres, 2)
        Me.clbGenres.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbGenres.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.clbGenres.FormattingEnabled = True
        Me.clbGenres.IntegralHeight = False
        Me.clbGenres.Location = New System.Drawing.Point(3, 222)
        Me.clbGenres.Name = "clbGenres"
        Me.clbGenres.Size = New System.Drawing.Size(220, 105)
        Me.clbGenres.TabIndex = 14
        '
        'lvActors
        '
        Me.lvActors.BackColor = System.Drawing.SystemColors.Window
        Me.lvActors.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colActorsID, Me.colActorsName, Me.colActorsRole, Me.colActorsThumb})
        Me.tblDetails.SetColumnSpan(Me.lvActors, 8)
        Me.lvActors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvActors.FullRowSelect = True
        Me.lvActors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvActors.Location = New System.Drawing.Point(721, 222)
        Me.lvActors.Name = "lvActors"
        Me.tblDetails.SetRowSpan(Me.lvActors, 3)
        Me.lvActors.Size = New System.Drawing.Size(366, 200)
        Me.lvActors.TabIndex = 20
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
        Me.colActorsName.Width = 130
        '
        'colActorsRole
        '
        Me.colActorsRole.Text = "Role"
        Me.colActorsRole.Width = 130
        '
        'colActorsThumb
        '
        Me.colActorsThumb.Text = "Thumb"
        Me.colActorsThumb.Width = 80
        '
        'lblActors
        '
        Me.lblActors.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblActors.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblActors, 8)
        Me.lblActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblActors.Location = New System.Drawing.Point(721, 206)
        Me.lblActors.Name = "lblActors"
        Me.lblActors.Size = New System.Drawing.Size(42, 13)
        Me.lblActors.TabIndex = 29
        Me.lblActors.Text = "Actors:"
        '
        'lblCreators
        '
        Me.lblCreators.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCreators.AutoSize = True
        Me.lblCreators.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCreators.Location = New System.Drawing.Point(475, 206)
        Me.lblCreators.Name = "lblCreators"
        Me.lblCreators.Size = New System.Drawing.Size(53, 13)
        Me.lblCreators.TabIndex = 40
        Me.lblCreators.Text = "Creators:"
        '
        'txtPlot
        '
        Me.txtPlot.AcceptsReturn = True
        Me.txtPlot.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtPlot, 10)
        Me.txtPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtPlot.Location = New System.Drawing.Point(229, 16)
        Me.txtPlot.Multiline = True
        Me.txtPlot.Name = "txtPlot"
        Me.tblDetails.SetRowSpan(Me.txtPlot, 9)
        Me.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlot.Size = New System.Drawing.Size(730, 187)
        Me.txtPlot.TabIndex = 10
        '
        'lblPremiered
        '
        Me.lblPremiered.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblPremiered.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblPremiered, 2)
        Me.lblPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPremiered.Location = New System.Drawing.Point(3, 124)
        Me.lblPremiered.Name = "lblPremiered"
        Me.lblPremiered.Size = New System.Drawing.Size(61, 13)
        Me.lblPremiered.TabIndex = 13
        Me.lblPremiered.Text = "Premiered:"
        '
        'dtpPremiered
        '
        Me.tblDetails.SetColumnSpan(Me.dtpPremiered, 2)
        Me.dtpPremiered.CustomFormat = "yyyy-dd-MM"
        Me.dtpPremiered.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpPremiered.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPremiered.Location = New System.Drawing.Point(3, 140)
        Me.dtpPremiered.Name = "dtpPremiered"
        Me.dtpPremiered.Size = New System.Drawing.Size(220, 22)
        Me.dtpPremiered.TabIndex = 4
        '
        'lblRuntime
        '
        Me.lblRuntime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRuntime.AutoSize = True
        Me.lblRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRuntime.Location = New System.Drawing.Point(3, 165)
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
        Me.txtRuntime.Location = New System.Drawing.Point(3, 181)
        Me.txtRuntime.Name = "txtRuntime"
        Me.txtRuntime.Size = New System.Drawing.Size(72, 22)
        Me.txtRuntime.TabIndex = 5
        '
        'lblGenres
        '
        Me.lblGenres.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblGenres.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblGenres, 2)
        Me.lblGenres.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblGenres.Location = New System.Drawing.Point(3, 206)
        Me.lblGenres.Name = "lblGenres"
        Me.lblGenres.Size = New System.Drawing.Size(46, 13)
        Me.lblGenres.TabIndex = 23
        Me.lblGenres.Text = "Genres:"
        '
        'lblTags
        '
        Me.lblTags.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTags.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblTags, 2)
        Me.lblTags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTags.Location = New System.Drawing.Point(3, 330)
        Me.lblTags.Name = "lblTags"
        Me.lblTags.Size = New System.Drawing.Size(32, 13)
        Me.lblTags.TabIndex = 23
        Me.lblTags.Text = "Tags:"
        '
        'clbTags
        '
        Me.clbTags.BackColor = System.Drawing.SystemColors.Window
        Me.clbTags.CheckOnClick = True
        Me.tblDetails.SetColumnSpan(Me.clbTags, 2)
        Me.clbTags.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbTags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.clbTags.FormattingEnabled = True
        Me.clbTags.IntegralHeight = False
        Me.clbTags.Location = New System.Drawing.Point(3, 346)
        Me.clbTags.Name = "clbTags"
        Me.tblDetails.SetRowSpan(Me.clbTags, 2)
        Me.clbTags.Size = New System.Drawing.Size(220, 105)
        Me.clbTags.TabIndex = 15
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
        Me.dgvCreators.Location = New System.Drawing.Point(475, 222)
        Me.dgvCreators.Name = "dgvCreators"
        Me.dgvCreators.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvCreators.Size = New System.Drawing.Size(240, 105)
        Me.dgvCreators.TabIndex = 18
        '
        'colCreditsName
        '
        Me.colCreditsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colCreditsName.HeaderText = "Name"
        Me.colCreditsName.Name = "colCreditsName"
        Me.colCreditsName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
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
        Me.tblDetails.SetColumnSpan(Me.dgvStudios, 4)
        Me.dgvStudios.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvStudios.Location = New System.Drawing.Point(229, 346)
        Me.dgvStudios.Name = "dgvStudios"
        Me.dgvStudios.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.tblDetails.SetRowSpan(Me.dgvStudios, 2)
        Me.dgvStudios.Size = New System.Drawing.Size(240, 105)
        Me.dgvStudios.TabIndex = 17
        '
        'colStudiosName
        '
        Me.colStudiosName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colStudiosName.HeaderText = "Name"
        Me.colStudiosName.Name = "colStudiosName"
        Me.colStudiosName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'lblStudios
        '
        Me.lblStudios.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStudios.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblStudios, 4)
        Me.lblStudios.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblStudios.Location = New System.Drawing.Point(229, 330)
        Me.lblStudios.Name = "lblStudios"
        Me.lblStudios.Size = New System.Drawing.Size(49, 13)
        Me.lblStudios.TabIndex = 42
        Me.lblStudios.Text = "Studios:"
        '
        'lblCountries
        '
        Me.lblCountries.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCountries.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblCountries, 4)
        Me.lblCountries.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCountries.Location = New System.Drawing.Point(229, 206)
        Me.lblCountries.Name = "lblCountries"
        Me.lblCountries.Size = New System.Drawing.Size(60, 13)
        Me.lblCountries.TabIndex = 11
        Me.lblCountries.Text = "Countries:"
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
        Me.tblDetails.SetColumnSpan(Me.dgvCountries, 4)
        Me.dgvCountries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCountries.Location = New System.Drawing.Point(229, 222)
        Me.dgvCountries.Name = "dgvCountries"
        Me.dgvCountries.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvCountries.Size = New System.Drawing.Size(240, 105)
        Me.dgvCountries.TabIndex = 16
        '
        'colCountriesName
        '
        Me.colCountriesName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colCountriesName.HeaderText = "Name"
        Me.colCountriesName.Name = "colCountriesName"
        Me.colCountriesName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'btnActorsAdd
        '
        Me.btnActorsAdd.Image = CType(resources.GetObject("btnActorsAdd.Image"), System.Drawing.Image)
        Me.btnActorsAdd.Location = New System.Drawing.Point(721, 428)
        Me.btnActorsAdd.Name = "btnActorsAdd"
        Me.btnActorsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsAdd.TabIndex = 21
        Me.btnActorsAdd.UseVisualStyleBackColor = True
        '
        'btnActorsEdit
        '
        Me.btnActorsEdit.Image = CType(resources.GetObject("btnActorsEdit.Image"), System.Drawing.Image)
        Me.btnActorsEdit.Location = New System.Drawing.Point(750, 428)
        Me.btnActorsEdit.Name = "btnActorsEdit"
        Me.btnActorsEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsEdit.TabIndex = 22
        Me.btnActorsEdit.UseVisualStyleBackColor = True
        '
        'btnActorsUp
        '
        Me.btnActorsUp.Image = CType(resources.GetObject("btnActorsUp.Image"), System.Drawing.Image)
        Me.btnActorsUp.Location = New System.Drawing.Point(907, 428)
        Me.btnActorsUp.Name = "btnActorsUp"
        Me.btnActorsUp.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsUp.TabIndex = 23
        Me.btnActorsUp.UseVisualStyleBackColor = True
        '
        'btnActorsDown
        '
        Me.btnActorsDown.Image = CType(resources.GetObject("btnActorsDown.Image"), System.Drawing.Image)
        Me.btnActorsDown.Location = New System.Drawing.Point(936, 428)
        Me.btnActorsDown.Name = "btnActorsDown"
        Me.btnActorsDown.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsDown.TabIndex = 24
        Me.btnActorsDown.UseVisualStyleBackColor = True
        '
        'btnActorsRemove
        '
        Me.btnActorsRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnActorsRemove.Image = CType(resources.GetObject("btnActorsRemove.Image"), System.Drawing.Image)
        Me.btnActorsRemove.Location = New System.Drawing.Point(1064, 428)
        Me.btnActorsRemove.Name = "btnActorsRemove"
        Me.btnActorsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsRemove.TabIndex = 25
        Me.btnActorsRemove.UseVisualStyleBackColor = True
        '
        'lblUserRating
        '
        Me.lblUserRating.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserRating.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblUserRating, 2)
        Me.lblUserRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblUserRating.Location = New System.Drawing.Point(3, 454)
        Me.lblUserRating.Name = "lblUserRating"
        Me.lblUserRating.Size = New System.Drawing.Size(70, 13)
        Me.lblUserRating.TabIndex = 19
        Me.lblUserRating.Text = "User Rating:"
        '
        'txtUserRating
        '
        Me.txtUserRating.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtUserRating, 2)
        Me.txtUserRating.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtUserRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtUserRating.Location = New System.Drawing.Point(3, 470)
        Me.txtUserRating.Name = "txtUserRating"
        Me.txtUserRating.Size = New System.Drawing.Size(220, 22)
        Me.txtUserRating.TabIndex = 27
        '
        'chkWatched
        '
        Me.chkWatched.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkWatched.AutoSize = True
        Me.chkWatched.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWatched.Location = New System.Drawing.Point(3, 559)
        Me.chkWatched.Name = "chkWatched"
        Me.chkWatched.Size = New System.Drawing.Size(72, 17)
        Me.chkWatched.TabIndex = 29
        Me.chkWatched.Text = "Watched"
        Me.chkWatched.UseVisualStyleBackColor = True
        '
        'dtpLastPlayed
        '
        Me.dtpLastPlayed.Checked = False
        Me.tblDetails.SetColumnSpan(Me.dtpLastPlayed, 2)
        Me.dtpLastPlayed.CustomFormat = "yyyy-dd-MM / HH:mm:ss"
        Me.dtpLastPlayed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpLastPlayed.Enabled = False
        Me.dtpLastPlayed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLastPlayed.Location = New System.Drawing.Point(3, 582)
        Me.dtpLastPlayed.Name = "dtpLastPlayed"
        Me.dtpLastPlayed.Size = New System.Drawing.Size(220, 22)
        Me.dtpLastPlayed.TabIndex = 30
        '
        'lblRatings
        '
        Me.lblRatings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRatings.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblRatings, 4)
        Me.lblRatings.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRatings.Location = New System.Drawing.Point(229, 454)
        Me.lblRatings.Name = "lblRatings"
        Me.lblRatings.Size = New System.Drawing.Size(49, 13)
        Me.lblRatings.TabIndex = 10
        Me.lblRatings.Text = "Ratings:"
        '
        'lvRatings
        '
        Me.lvRatings.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colRatingsName, Me.colRatingsValue, Me.colRatingsVotes, Me.colRatingsMax})
        Me.tblDetails.SetColumnSpan(Me.lvRatings, 4)
        Me.lvRatings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvRatings.FullRowSelect = True
        Me.lvRatings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvRatings.Location = New System.Drawing.Point(229, 470)
        Me.lvRatings.Name = "lvRatings"
        Me.tblDetails.SetRowSpan(Me.lvRatings, 4)
        Me.lvRatings.Size = New System.Drawing.Size(240, 106)
        Me.lvRatings.TabIndex = 31
        Me.lvRatings.UseCompatibleStateImageBehavior = False
        Me.lvRatings.View = System.Windows.Forms.View.Details
        '
        'colRatingsName
        '
        Me.colRatingsName.Text = "Name"
        Me.colRatingsName.Width = 65
        '
        'colRatingsValue
        '
        Me.colRatingsValue.Text = "Value"
        Me.colRatingsValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colRatingsValue.Width = 40
        '
        'colRatingsVotes
        '
        Me.colRatingsVotes.Text = "Votes"
        Me.colRatingsVotes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'colRatingsMax
        '
        Me.colRatingsMax.Text = "Max"
        Me.colRatingsMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colRatingsMax.Width = 35
        '
        'btnRatingsAdd
        '
        Me.btnRatingsAdd.Image = CType(resources.GetObject("btnRatingsAdd.Image"), System.Drawing.Image)
        Me.btnRatingsAdd.Location = New System.Drawing.Point(229, 582)
        Me.btnRatingsAdd.Name = "btnRatingsAdd"
        Me.btnRatingsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnRatingsAdd.TabIndex = 32
        Me.btnRatingsAdd.UseVisualStyleBackColor = True
        '
        'btnRatingsEdit
        '
        Me.btnRatingsEdit.Image = CType(resources.GetObject("btnRatingsEdit.Image"), System.Drawing.Image)
        Me.btnRatingsEdit.Location = New System.Drawing.Point(258, 582)
        Me.btnRatingsEdit.Name = "btnRatingsEdit"
        Me.btnRatingsEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnRatingsEdit.TabIndex = 33
        Me.btnRatingsEdit.UseVisualStyleBackColor = True
        '
        'btnRatingsRemove
        '
        Me.btnRatingsRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRatingsRemove.Image = CType(resources.GetObject("btnRatingsRemove.Image"), System.Drawing.Image)
        Me.btnRatingsRemove.Location = New System.Drawing.Point(446, 582)
        Me.btnRatingsRemove.Name = "btnRatingsRemove"
        Me.btnRatingsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnRatingsRemove.TabIndex = 34
        Me.btnRatingsRemove.UseVisualStyleBackColor = True
        '
        'btnCertificationsAsMPAARating
        '
        Me.btnCertificationsAsMPAARating.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCertificationsAsMPAARating.Image = CType(resources.GetObject("btnCertificationsAsMPAARating.Image"), System.Drawing.Image)
        Me.btnCertificationsAsMPAARating.Location = New System.Drawing.Point(692, 582)
        Me.btnCertificationsAsMPAARating.Name = "btnCertificationsAsMPAARating"
        Me.btnCertificationsAsMPAARating.Size = New System.Drawing.Size(23, 23)
        Me.btnCertificationsAsMPAARating.TabIndex = 36
        Me.btnCertificationsAsMPAARating.UseVisualStyleBackColor = True
        '
        'lblMPAADesc
        '
        Me.lblMPAADesc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblMPAADesc.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblMPAADesc, 5)
        Me.lblMPAADesc.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMPAADesc.Location = New System.Drawing.Point(907, 454)
        Me.lblMPAADesc.Name = "lblMPAADesc"
        Me.lblMPAADesc.Size = New System.Drawing.Size(138, 13)
        Me.lblMPAADesc.TabIndex = 38
        Me.lblMPAADesc.Text = "MPAA Rating Description:"
        '
        'txtMPAADesc
        '
        Me.txtMPAADesc.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtMPAADesc, 5)
        Me.txtMPAADesc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMPAADesc.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtMPAADesc.Location = New System.Drawing.Point(907, 470)
        Me.txtMPAADesc.Multiline = True
        Me.txtMPAADesc.Name = "txtMPAADesc"
        Me.tblDetails.SetRowSpan(Me.txtMPAADesc, 4)
        Me.txtMPAADesc.Size = New System.Drawing.Size(180, 106)
        Me.txtMPAADesc.TabIndex = 38
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(81, 165)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(42, 13)
        Me.lblStatus.TabIndex = 15
        Me.lblStatus.Text = "Status:"
        '
        'txtStatus
        '
        Me.txtStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtStatus.Location = New System.Drawing.Point(81, 181)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(142, 22)
        Me.txtStatus.TabIndex = 50
        '
        'lblPlot
        '
        Me.lblPlot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblPlot.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblPlot, 9)
        Me.lblPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPlot.Location = New System.Drawing.Point(229, 0)
        Me.lblPlot.Name = "lblPlot"
        Me.lblPlot.Size = New System.Drawing.Size(30, 13)
        Me.lblPlot.TabIndex = 27
        Me.lblPlot.Text = "Plot:"
        '
        'tpOther
        '
        Me.tpOther.Controls.Add(Me.tblOther)
        Me.tpOther.Location = New System.Drawing.Point(4, 22)
        Me.tpOther.Name = "tpOther"
        Me.tpOther.Size = New System.Drawing.Size(1096, 631)
        Me.tpOther.TabIndex = 17
        Me.tpOther.Text = "Other"
        Me.tpOther.UseVisualStyleBackColor = True
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
        Me.tblOther.Size = New System.Drawing.Size(1096, 631)
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
        Me.gbTheme.Size = New System.Drawing.Size(1093, 78)
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
        Me.tblTheme.Size = New System.Drawing.Size(1087, 57)
        Me.tblTheme.TabIndex = 59
        '
        'btnRemoveTheme
        '
        Me.btnRemoveTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveTheme.Image = CType(resources.GetObject("btnRemoveTheme.Image"), System.Drawing.Image)
        Me.btnRemoveTheme.Location = New System.Drawing.Point(1061, 31)
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
        Me.btnLocalThemePlay.Location = New System.Drawing.Point(1061, 3)
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
        Me.txtLocalTheme.Size = New System.Drawing.Size(1052, 22)
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
        Me.tpImages.Controls.Add(Me.tblImages)
        Me.tpImages.Controls.Add(Me.pnlImagesRight)
        Me.tpImages.Location = New System.Drawing.Point(4, 22)
        Me.tpImages.Name = "tpImages"
        Me.tpImages.Padding = New System.Windows.Forms.Padding(3)
        Me.tpImages.Size = New System.Drawing.Size(1096, 631)
        Me.tpImages.TabIndex = 16
        Me.tpImages.Text = "Images"
        Me.tpImages.UseVisualStyleBackColor = True
        '
        'tblImages
        '
        Me.tblImages.AutoScroll = True
        Me.tblImages.AutoSize = True
        Me.tblImages.ColumnCount = 4
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.Controls.Add(Me.pnlPoster, 0, 0)
        Me.tblImages.Controls.Add(Me.pnlCharacterArt, 2, 1)
        Me.tblImages.Controls.Add(Me.pnlClearLogo, 1, 1)
        Me.tblImages.Controls.Add(Me.pnlFanart, 1, 0)
        Me.tblImages.Controls.Add(Me.pnlLandscape, 2, 0)
        Me.tblImages.Controls.Add(Me.pnlBanner, 0, 2)
        Me.tblImages.Controls.Add(Me.pnlClearArt, 0, 1)
        Me.tblImages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImages.Location = New System.Drawing.Point(3, 3)
        Me.tblImages.Name = "tblImages"
        Me.tblImages.RowCount = 3
        Me.tblImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImages.Size = New System.Drawing.Size(881, 625)
        Me.tblImages.TabIndex = 2
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
        Me.tblPoster.ColumnCount = 5
        Me.tblPoster.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblPoster.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblPoster.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblPoster.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblPoster.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblPoster.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblPoster.Controls.Add(Me.pbPoster, 0, 1)
        Me.tblPoster.Controls.Add(Me.lblPoster, 0, 0)
        Me.tblPoster.Controls.Add(Me.btnSetPosterLocal, 2, 3)
        Me.tblPoster.Controls.Add(Me.btnSetPosterScrape, 0, 3)
        Me.tblPoster.Controls.Add(Me.lblPosterSize, 0, 2)
        Me.tblPoster.Controls.Add(Me.btnSetPosterDL, 1, 3)
        Me.tblPoster.Controls.Add(Me.btnRemovePoster, 4, 3)
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
        Me.tblPoster.SetColumnSpan(Me.pbPoster, 5)
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
        Me.tblPoster.SetColumnSpan(Me.lblPoster, 5)
        Me.lblPoster.Location = New System.Drawing.Point(111, 3)
        Me.lblPoster.Name = "lblPoster"
        Me.lblPoster.Size = New System.Drawing.Size(39, 13)
        Me.lblPoster.TabIndex = 2
        Me.lblPoster.Text = "Poster"
        '
        'btnSetPosterLocal
        '
        Me.btnSetPosterLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetPosterLocal.Image = CType(resources.GetObject("btnSetPosterLocal.Image"), System.Drawing.Image)
        Me.btnSetPosterLocal.Location = New System.Drawing.Point(61, 193)
        Me.btnSetPosterLocal.Name = "btnSetPosterLocal"
        Me.btnSetPosterLocal.Size = New System.Drawing.Size(23, 23)
        Me.btnSetPosterLocal.TabIndex = 2
        Me.btnSetPosterLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetPosterLocal.UseVisualStyleBackColor = True
        '
        'btnSetPosterScrape
        '
        Me.btnSetPosterScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetPosterScrape.Image = CType(resources.GetObject("btnSetPosterScrape.Image"), System.Drawing.Image)
        Me.btnSetPosterScrape.Location = New System.Drawing.Point(3, 193)
        Me.btnSetPosterScrape.Name = "btnSetPosterScrape"
        Me.btnSetPosterScrape.Size = New System.Drawing.Size(23, 23)
        Me.btnSetPosterScrape.TabIndex = 0
        Me.btnSetPosterScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetPosterScrape.UseVisualStyleBackColor = True
        '
        'lblPosterSize
        '
        Me.lblPosterSize.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPosterSize.AutoSize = True
        Me.tblPoster.SetColumnSpan(Me.lblPosterSize, 5)
        Me.lblPosterSize.Location = New System.Drawing.Point(85, 173)
        Me.lblPosterSize.Name = "lblPosterSize"
        Me.lblPosterSize.Size = New System.Drawing.Size(92, 13)
        Me.lblPosterSize.TabIndex = 5
        Me.lblPosterSize.Text = "Size: (XXXXxXXXX)"
        Me.lblPosterSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPosterSize.Visible = False
        '
        'btnSetPosterDL
        '
        Me.btnSetPosterDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetPosterDL.Image = CType(resources.GetObject("btnSetPosterDL.Image"), System.Drawing.Image)
        Me.btnSetPosterDL.Location = New System.Drawing.Point(32, 193)
        Me.btnSetPosterDL.Name = "btnSetPosterDL"
        Me.btnSetPosterDL.Size = New System.Drawing.Size(23, 23)
        Me.btnSetPosterDL.TabIndex = 1
        Me.btnSetPosterDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetPosterDL.UseVisualStyleBackColor = True
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
        'pnlCharacterArt
        '
        Me.pnlCharacterArt.AutoSize = True
        Me.pnlCharacterArt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlCharacterArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCharacterArt.Controls.Add(Me.tblCharacterArt)
        Me.pnlCharacterArt.Location = New System.Drawing.Point(543, 230)
        Me.pnlCharacterArt.Name = "pnlCharacterArt"
        Me.pnlCharacterArt.Size = New System.Drawing.Size(264, 221)
        Me.pnlCharacterArt.TabIndex = 5
        '
        'tblCharacterArt
        '
        Me.tblCharacterArt.AutoScroll = True
        Me.tblCharacterArt.AutoSize = True
        Me.tblCharacterArt.ColumnCount = 5
        Me.tblCharacterArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCharacterArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCharacterArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCharacterArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblCharacterArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCharacterArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblCharacterArt.Controls.Add(Me.pbCharacterArt, 0, 1)
        Me.tblCharacterArt.Controls.Add(Me.lblCharacterArt, 0, 0)
        Me.tblCharacterArt.Controls.Add(Me.btnSetCharacterArtLocal, 2, 3)
        Me.tblCharacterArt.Controls.Add(Me.btnSetCharacterArtScrape, 0, 3)
        Me.tblCharacterArt.Controls.Add(Me.lblCharacterArtSize, 0, 2)
        Me.tblCharacterArt.Controls.Add(Me.btnSetCharacterArtDL, 1, 3)
        Me.tblCharacterArt.Controls.Add(Me.btnRemoveCharacterArt, 4, 3)
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
        Me.tblCharacterArt.SetColumnSpan(Me.pbCharacterArt, 5)
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
        Me.tblCharacterArt.SetColumnSpan(Me.lblCharacterArt, 5)
        Me.lblCharacterArt.Location = New System.Drawing.Point(95, 3)
        Me.lblCharacterArt.Name = "lblCharacterArt"
        Me.lblCharacterArt.Size = New System.Drawing.Size(71, 13)
        Me.lblCharacterArt.TabIndex = 2
        Me.lblCharacterArt.Text = "CharacterArt"
        '
        'btnSetCharacterArtLocal
        '
        Me.btnSetCharacterArtLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetCharacterArtLocal.Image = CType(resources.GetObject("btnSetCharacterArtLocal.Image"), System.Drawing.Image)
        Me.btnSetCharacterArtLocal.Location = New System.Drawing.Point(61, 193)
        Me.btnSetCharacterArtLocal.Name = "btnSetCharacterArtLocal"
        Me.btnSetCharacterArtLocal.Size = New System.Drawing.Size(23, 23)
        Me.btnSetCharacterArtLocal.TabIndex = 2
        Me.btnSetCharacterArtLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetCharacterArtLocal.UseVisualStyleBackColor = True
        '
        'btnSetCharacterArtScrape
        '
        Me.btnSetCharacterArtScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetCharacterArtScrape.Image = CType(resources.GetObject("btnSetCharacterArtScrape.Image"), System.Drawing.Image)
        Me.btnSetCharacterArtScrape.Location = New System.Drawing.Point(3, 193)
        Me.btnSetCharacterArtScrape.Name = "btnSetCharacterArtScrape"
        Me.btnSetCharacterArtScrape.Size = New System.Drawing.Size(23, 23)
        Me.btnSetCharacterArtScrape.TabIndex = 0
        Me.btnSetCharacterArtScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetCharacterArtScrape.UseVisualStyleBackColor = True
        '
        'lblCharacterArtSize
        '
        Me.lblCharacterArtSize.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblCharacterArtSize.AutoSize = True
        Me.tblCharacterArt.SetColumnSpan(Me.lblCharacterArtSize, 5)
        Me.lblCharacterArtSize.Location = New System.Drawing.Point(85, 173)
        Me.lblCharacterArtSize.Name = "lblCharacterArtSize"
        Me.lblCharacterArtSize.Size = New System.Drawing.Size(92, 13)
        Me.lblCharacterArtSize.TabIndex = 5
        Me.lblCharacterArtSize.Text = "Size: (XXXXxXXXX)"
        Me.lblCharacterArtSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCharacterArtSize.Visible = False
        '
        'btnSetCharacterArtDL
        '
        Me.btnSetCharacterArtDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetCharacterArtDL.Image = CType(resources.GetObject("btnSetCharacterArtDL.Image"), System.Drawing.Image)
        Me.btnSetCharacterArtDL.Location = New System.Drawing.Point(32, 193)
        Me.btnSetCharacterArtDL.Name = "btnSetCharacterArtDL"
        Me.btnSetCharacterArtDL.Size = New System.Drawing.Size(23, 23)
        Me.btnSetCharacterArtDL.TabIndex = 1
        Me.btnSetCharacterArtDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetCharacterArtDL.UseVisualStyleBackColor = True
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
        'pnlClearLogo
        '
        Me.pnlClearLogo.AutoSize = True
        Me.pnlClearLogo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlClearLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlClearLogo.Controls.Add(Me.tblClearLogo)
        Me.pnlClearLogo.Location = New System.Drawing.Point(273, 230)
        Me.pnlClearLogo.Name = "pnlClearLogo"
        Me.pnlClearLogo.Size = New System.Drawing.Size(264, 221)
        Me.pnlClearLogo.TabIndex = 4
        '
        'tblClearLogo
        '
        Me.tblClearLogo.AutoScroll = True
        Me.tblClearLogo.AutoSize = True
        Me.tblClearLogo.ColumnCount = 5
        Me.tblClearLogo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearLogo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearLogo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearLogo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblClearLogo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearLogo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblClearLogo.Controls.Add(Me.pbClearLogo, 0, 1)
        Me.tblClearLogo.Controls.Add(Me.lblClearLogo, 0, 0)
        Me.tblClearLogo.Controls.Add(Me.btnSetClearLogoLocal, 2, 3)
        Me.tblClearLogo.Controls.Add(Me.btnSetClearLogoScrape, 0, 3)
        Me.tblClearLogo.Controls.Add(Me.lblClearLogoSize, 0, 2)
        Me.tblClearLogo.Controls.Add(Me.btnSetClearLogoDL, 1, 3)
        Me.tblClearLogo.Controls.Add(Me.btnRemoveClearLogo, 4, 3)
        Me.tblClearLogo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblClearLogo.Location = New System.Drawing.Point(0, 0)
        Me.tblClearLogo.Name = "tblClearLogo"
        Me.tblClearLogo.RowCount = 4
        Me.tblClearLogo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblClearLogo.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearLogo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblClearLogo.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblClearLogo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblClearLogo.Size = New System.Drawing.Size(262, 219)
        Me.tblClearLogo.TabIndex = 0
        '
        'pbClearLogo
        '
        Me.pbClearLogo.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pbClearLogo.BackColor = System.Drawing.Color.White
        Me.tblClearLogo.SetColumnSpan(Me.pbClearLogo, 5)
        Me.pbClearLogo.Location = New System.Drawing.Point(3, 23)
        Me.pbClearLogo.Name = "pbClearLogo"
        Me.pbClearLogo.Size = New System.Drawing.Size(256, 144)
        Me.pbClearLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbClearLogo.TabIndex = 1
        Me.pbClearLogo.TabStop = False
        '
        'lblClearLogo
        '
        Me.lblClearLogo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblClearLogo.AutoSize = True
        Me.tblClearLogo.SetColumnSpan(Me.lblClearLogo, 5)
        Me.lblClearLogo.Location = New System.Drawing.Point(101, 3)
        Me.lblClearLogo.Name = "lblClearLogo"
        Me.lblClearLogo.Size = New System.Drawing.Size(59, 13)
        Me.lblClearLogo.TabIndex = 2
        Me.lblClearLogo.Text = "ClearLogo"
        '
        'btnSetClearLogoLocal
        '
        Me.btnSetClearLogoLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearLogoLocal.Image = CType(resources.GetObject("btnSetClearLogoLocal.Image"), System.Drawing.Image)
        Me.btnSetClearLogoLocal.Location = New System.Drawing.Point(61, 193)
        Me.btnSetClearLogoLocal.Name = "btnSetClearLogoLocal"
        Me.btnSetClearLogoLocal.Size = New System.Drawing.Size(23, 23)
        Me.btnSetClearLogoLocal.TabIndex = 2
        Me.btnSetClearLogoLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearLogoLocal.UseVisualStyleBackColor = True
        '
        'btnSetClearLogoScrape
        '
        Me.btnSetClearLogoScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearLogoScrape.Image = CType(resources.GetObject("btnSetClearLogoScrape.Image"), System.Drawing.Image)
        Me.btnSetClearLogoScrape.Location = New System.Drawing.Point(3, 193)
        Me.btnSetClearLogoScrape.Name = "btnSetClearLogoScrape"
        Me.btnSetClearLogoScrape.Size = New System.Drawing.Size(23, 23)
        Me.btnSetClearLogoScrape.TabIndex = 0
        Me.btnSetClearLogoScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearLogoScrape.UseVisualStyleBackColor = True
        '
        'lblClearLogoSize
        '
        Me.lblClearLogoSize.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblClearLogoSize.AutoSize = True
        Me.tblClearLogo.SetColumnSpan(Me.lblClearLogoSize, 5)
        Me.lblClearLogoSize.Location = New System.Drawing.Point(85, 173)
        Me.lblClearLogoSize.Name = "lblClearLogoSize"
        Me.lblClearLogoSize.Size = New System.Drawing.Size(92, 13)
        Me.lblClearLogoSize.TabIndex = 5
        Me.lblClearLogoSize.Text = "Size: (XXXXxXXXX)"
        Me.lblClearLogoSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblClearLogoSize.Visible = False
        '
        'btnSetClearLogoDL
        '
        Me.btnSetClearLogoDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearLogoDL.Image = CType(resources.GetObject("btnSetClearLogoDL.Image"), System.Drawing.Image)
        Me.btnSetClearLogoDL.Location = New System.Drawing.Point(32, 193)
        Me.btnSetClearLogoDL.Name = "btnSetClearLogoDL"
        Me.btnSetClearLogoDL.Size = New System.Drawing.Size(23, 23)
        Me.btnSetClearLogoDL.TabIndex = 1
        Me.btnSetClearLogoDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearLogoDL.UseVisualStyleBackColor = True
        '
        'btnRemoveClearLogo
        '
        Me.btnRemoveClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveClearLogo.Image = CType(resources.GetObject("btnRemoveClearLogo.Image"), System.Drawing.Image)
        Me.btnRemoveClearLogo.Location = New System.Drawing.Point(236, 193)
        Me.btnRemoveClearLogo.Name = "btnRemoveClearLogo"
        Me.btnRemoveClearLogo.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveClearLogo.TabIndex = 3
        Me.btnRemoveClearLogo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveClearLogo.UseVisualStyleBackColor = True
        '
        'pnlFanart
        '
        Me.pnlFanart.AutoSize = True
        Me.pnlFanart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFanart.Controls.Add(Me.tblFanart)
        Me.pnlFanart.Location = New System.Drawing.Point(273, 3)
        Me.pnlFanart.Name = "pnlFanart"
        Me.pnlFanart.Size = New System.Drawing.Size(264, 221)
        Me.pnlFanart.TabIndex = 1
        '
        'tblFanart
        '
        Me.tblFanart.AutoScroll = True
        Me.tblFanart.AutoSize = True
        Me.tblFanart.ColumnCount = 5
        Me.tblFanart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFanart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFanart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFanart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblFanart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFanart.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFanart.Controls.Add(Me.pbFanart, 0, 1)
        Me.tblFanart.Controls.Add(Me.lblFanart, 0, 0)
        Me.tblFanart.Controls.Add(Me.btnSetFanartLocal, 2, 3)
        Me.tblFanart.Controls.Add(Me.btnSetFanartScrape, 0, 3)
        Me.tblFanart.Controls.Add(Me.lblFanartSize, 0, 2)
        Me.tblFanart.Controls.Add(Me.btnSetFanartDL, 1, 3)
        Me.tblFanart.Controls.Add(Me.btnRemoveFanart, 4, 3)
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
        Me.tblFanart.SetColumnSpan(Me.pbFanart, 5)
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
        Me.tblFanart.SetColumnSpan(Me.lblFanart, 5)
        Me.lblFanart.Location = New System.Drawing.Point(111, 3)
        Me.lblFanart.Name = "lblFanart"
        Me.lblFanart.Size = New System.Drawing.Size(40, 13)
        Me.lblFanart.TabIndex = 2
        Me.lblFanart.Text = "Fanart"
        '
        'btnSetFanartLocal
        '
        Me.btnSetFanartLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetFanartLocal.Image = CType(resources.GetObject("btnSetFanartLocal.Image"), System.Drawing.Image)
        Me.btnSetFanartLocal.Location = New System.Drawing.Point(61, 193)
        Me.btnSetFanartLocal.Name = "btnSetFanartLocal"
        Me.btnSetFanartLocal.Size = New System.Drawing.Size(23, 23)
        Me.btnSetFanartLocal.TabIndex = 2
        Me.btnSetFanartLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetFanartLocal.UseVisualStyleBackColor = True
        '
        'btnSetFanartScrape
        '
        Me.btnSetFanartScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetFanartScrape.Image = CType(resources.GetObject("btnSetFanartScrape.Image"), System.Drawing.Image)
        Me.btnSetFanartScrape.Location = New System.Drawing.Point(3, 193)
        Me.btnSetFanartScrape.Name = "btnSetFanartScrape"
        Me.btnSetFanartScrape.Size = New System.Drawing.Size(23, 23)
        Me.btnSetFanartScrape.TabIndex = 0
        Me.btnSetFanartScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetFanartScrape.UseVisualStyleBackColor = True
        '
        'lblFanartSize
        '
        Me.lblFanartSize.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblFanartSize.AutoSize = True
        Me.tblFanart.SetColumnSpan(Me.lblFanartSize, 5)
        Me.lblFanartSize.Location = New System.Drawing.Point(85, 173)
        Me.lblFanartSize.Name = "lblFanartSize"
        Me.lblFanartSize.Size = New System.Drawing.Size(92, 13)
        Me.lblFanartSize.TabIndex = 5
        Me.lblFanartSize.Text = "Size: (XXXXxXXXX)"
        Me.lblFanartSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblFanartSize.Visible = False
        '
        'btnSetFanartDL
        '
        Me.btnSetFanartDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetFanartDL.Image = CType(resources.GetObject("btnSetFanartDL.Image"), System.Drawing.Image)
        Me.btnSetFanartDL.Location = New System.Drawing.Point(32, 193)
        Me.btnSetFanartDL.Name = "btnSetFanartDL"
        Me.btnSetFanartDL.Size = New System.Drawing.Size(23, 23)
        Me.btnSetFanartDL.TabIndex = 1
        Me.btnSetFanartDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetFanartDL.UseVisualStyleBackColor = True
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
        'pnlLandscape
        '
        Me.pnlLandscape.AutoSize = True
        Me.pnlLandscape.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlLandscape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlLandscape.Controls.Add(Me.tblLandscape)
        Me.pnlLandscape.Location = New System.Drawing.Point(543, 3)
        Me.pnlLandscape.Name = "pnlLandscape"
        Me.pnlLandscape.Size = New System.Drawing.Size(264, 221)
        Me.pnlLandscape.TabIndex = 2
        '
        'tblLandscape
        '
        Me.tblLandscape.AutoScroll = True
        Me.tblLandscape.AutoSize = True
        Me.tblLandscape.ColumnCount = 5
        Me.tblLandscape.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblLandscape.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblLandscape.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblLandscape.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblLandscape.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblLandscape.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblLandscape.Controls.Add(Me.pbLandscape, 0, 1)
        Me.tblLandscape.Controls.Add(Me.lblLandscape, 0, 0)
        Me.tblLandscape.Controls.Add(Me.btnSetLandscapeLocal, 2, 3)
        Me.tblLandscape.Controls.Add(Me.btnSetLandscapeScrape, 0, 3)
        Me.tblLandscape.Controls.Add(Me.lblLandscapeSize, 0, 2)
        Me.tblLandscape.Controls.Add(Me.btnSetLandscapeDL, 1, 3)
        Me.tblLandscape.Controls.Add(Me.btnRemoveLandscape, 4, 3)
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
        Me.tblLandscape.SetColumnSpan(Me.pbLandscape, 5)
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
        Me.tblLandscape.SetColumnSpan(Me.lblLandscape, 5)
        Me.lblLandscape.Location = New System.Drawing.Point(100, 3)
        Me.lblLandscape.Name = "lblLandscape"
        Me.lblLandscape.Size = New System.Drawing.Size(61, 13)
        Me.lblLandscape.TabIndex = 2
        Me.lblLandscape.Text = "Landscape"
        '
        'btnSetLandscapeLocal
        '
        Me.btnSetLandscapeLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetLandscapeLocal.Image = CType(resources.GetObject("btnSetLandscapeLocal.Image"), System.Drawing.Image)
        Me.btnSetLandscapeLocal.Location = New System.Drawing.Point(61, 193)
        Me.btnSetLandscapeLocal.Name = "btnSetLandscapeLocal"
        Me.btnSetLandscapeLocal.Size = New System.Drawing.Size(23, 23)
        Me.btnSetLandscapeLocal.TabIndex = 2
        Me.btnSetLandscapeLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetLandscapeLocal.UseVisualStyleBackColor = True
        '
        'btnSetLandscapeScrape
        '
        Me.btnSetLandscapeScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetLandscapeScrape.Image = CType(resources.GetObject("btnSetLandscapeScrape.Image"), System.Drawing.Image)
        Me.btnSetLandscapeScrape.Location = New System.Drawing.Point(3, 193)
        Me.btnSetLandscapeScrape.Name = "btnSetLandscapeScrape"
        Me.btnSetLandscapeScrape.Size = New System.Drawing.Size(23, 23)
        Me.btnSetLandscapeScrape.TabIndex = 0
        Me.btnSetLandscapeScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetLandscapeScrape.UseVisualStyleBackColor = True
        '
        'lblLandscapeSize
        '
        Me.lblLandscapeSize.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblLandscapeSize.AutoSize = True
        Me.tblLandscape.SetColumnSpan(Me.lblLandscapeSize, 5)
        Me.lblLandscapeSize.Location = New System.Drawing.Point(85, 173)
        Me.lblLandscapeSize.Name = "lblLandscapeSize"
        Me.lblLandscapeSize.Size = New System.Drawing.Size(92, 13)
        Me.lblLandscapeSize.TabIndex = 5
        Me.lblLandscapeSize.Text = "Size: (XXXXxXXXX)"
        Me.lblLandscapeSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblLandscapeSize.Visible = False
        '
        'btnSetLandscapeDL
        '
        Me.btnSetLandscapeDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetLandscapeDL.Image = CType(resources.GetObject("btnSetLandscapeDL.Image"), System.Drawing.Image)
        Me.btnSetLandscapeDL.Location = New System.Drawing.Point(32, 193)
        Me.btnSetLandscapeDL.Name = "btnSetLandscapeDL"
        Me.btnSetLandscapeDL.Size = New System.Drawing.Size(23, 23)
        Me.btnSetLandscapeDL.TabIndex = 1
        Me.btnSetLandscapeDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetLandscapeDL.UseVisualStyleBackColor = True
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
        Me.tblBanner.ColumnCount = 5
        Me.tblBanner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBanner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBanner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBanner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBanner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBanner.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblBanner.Controls.Add(Me.pbBanner, 0, 1)
        Me.tblBanner.Controls.Add(Me.lblBanner, 0, 0)
        Me.tblBanner.Controls.Add(Me.btnSetBannerLocal, 2, 3)
        Me.tblBanner.Controls.Add(Me.btnSetBannerScrape, 0, 3)
        Me.tblBanner.Controls.Add(Me.lblBannerSize, 0, 2)
        Me.tblBanner.Controls.Add(Me.btnSetBannerDL, 1, 3)
        Me.tblBanner.Controls.Add(Me.btnRemoveBanner, 4, 3)
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
        Me.tblBanner.SetColumnSpan(Me.pbBanner, 5)
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
        Me.tblBanner.SetColumnSpan(Me.lblBanner, 5)
        Me.lblBanner.Location = New System.Drawing.Point(109, 3)
        Me.lblBanner.Name = "lblBanner"
        Me.lblBanner.Size = New System.Drawing.Size(44, 13)
        Me.lblBanner.TabIndex = 2
        Me.lblBanner.Text = "Banner"
        '
        'btnSetBannerLocal
        '
        Me.btnSetBannerLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetBannerLocal.Image = CType(resources.GetObject("btnSetBannerLocal.Image"), System.Drawing.Image)
        Me.btnSetBannerLocal.Location = New System.Drawing.Point(61, 97)
        Me.btnSetBannerLocal.Name = "btnSetBannerLocal"
        Me.btnSetBannerLocal.Size = New System.Drawing.Size(23, 23)
        Me.btnSetBannerLocal.TabIndex = 2
        Me.btnSetBannerLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetBannerLocal.UseVisualStyleBackColor = True
        '
        'btnSetBannerScrape
        '
        Me.btnSetBannerScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetBannerScrape.Image = CType(resources.GetObject("btnSetBannerScrape.Image"), System.Drawing.Image)
        Me.btnSetBannerScrape.Location = New System.Drawing.Point(3, 97)
        Me.btnSetBannerScrape.Name = "btnSetBannerScrape"
        Me.btnSetBannerScrape.Size = New System.Drawing.Size(23, 23)
        Me.btnSetBannerScrape.TabIndex = 0
        Me.btnSetBannerScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetBannerScrape.UseVisualStyleBackColor = True
        '
        'lblBannerSize
        '
        Me.lblBannerSize.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblBannerSize.AutoSize = True
        Me.tblBanner.SetColumnSpan(Me.lblBannerSize, 5)
        Me.lblBannerSize.Location = New System.Drawing.Point(85, 77)
        Me.lblBannerSize.Name = "lblBannerSize"
        Me.lblBannerSize.Size = New System.Drawing.Size(92, 13)
        Me.lblBannerSize.TabIndex = 5
        Me.lblBannerSize.Text = "Size: (XXXXxXXXX)"
        Me.lblBannerSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblBannerSize.Visible = False
        '
        'btnSetBannerDL
        '
        Me.btnSetBannerDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetBannerDL.Image = CType(resources.GetObject("btnSetBannerDL.Image"), System.Drawing.Image)
        Me.btnSetBannerDL.Location = New System.Drawing.Point(32, 97)
        Me.btnSetBannerDL.Name = "btnSetBannerDL"
        Me.btnSetBannerDL.Size = New System.Drawing.Size(23, 23)
        Me.btnSetBannerDL.TabIndex = 1
        Me.btnSetBannerDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetBannerDL.UseVisualStyleBackColor = True
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
        'pnlClearArt
        '
        Me.pnlClearArt.AutoSize = True
        Me.pnlClearArt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlClearArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlClearArt.Controls.Add(Me.tblClearArt)
        Me.pnlClearArt.Location = New System.Drawing.Point(3, 230)
        Me.pnlClearArt.Name = "pnlClearArt"
        Me.pnlClearArt.Size = New System.Drawing.Size(264, 221)
        Me.pnlClearArt.TabIndex = 2
        '
        'tblClearArt
        '
        Me.tblClearArt.AutoScroll = True
        Me.tblClearArt.AutoSize = True
        Me.tblClearArt.ColumnCount = 5
        Me.tblClearArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblClearArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblClearArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblClearArt.Controls.Add(Me.pbClearArt, 0, 1)
        Me.tblClearArt.Controls.Add(Me.lblClearArt, 0, 0)
        Me.tblClearArt.Controls.Add(Me.btnSetClearArtLocal, 2, 3)
        Me.tblClearArt.Controls.Add(Me.btnSetClearArtScrape, 0, 3)
        Me.tblClearArt.Controls.Add(Me.lblClearArtSize, 0, 2)
        Me.tblClearArt.Controls.Add(Me.btnSetClearArtDL, 1, 3)
        Me.tblClearArt.Controls.Add(Me.btnRemoveClearArt, 4, 3)
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
        Me.tblClearArt.SetColumnSpan(Me.pbClearArt, 5)
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
        Me.tblClearArt.SetColumnSpan(Me.lblClearArt, 5)
        Me.lblClearArt.Location = New System.Drawing.Point(107, 3)
        Me.lblClearArt.Name = "lblClearArt"
        Me.lblClearArt.Size = New System.Drawing.Size(48, 13)
        Me.lblClearArt.TabIndex = 2
        Me.lblClearArt.Text = "ClearArt"
        '
        'btnSetClearArtLocal
        '
        Me.btnSetClearArtLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearArtLocal.Image = CType(resources.GetObject("btnSetClearArtLocal.Image"), System.Drawing.Image)
        Me.btnSetClearArtLocal.Location = New System.Drawing.Point(61, 193)
        Me.btnSetClearArtLocal.Name = "btnSetClearArtLocal"
        Me.btnSetClearArtLocal.Size = New System.Drawing.Size(23, 23)
        Me.btnSetClearArtLocal.TabIndex = 2
        Me.btnSetClearArtLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearArtLocal.UseVisualStyleBackColor = True
        '
        'btnSetClearArtScrape
        '
        Me.btnSetClearArtScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearArtScrape.Image = CType(resources.GetObject("btnSetClearArtScrape.Image"), System.Drawing.Image)
        Me.btnSetClearArtScrape.Location = New System.Drawing.Point(3, 193)
        Me.btnSetClearArtScrape.Name = "btnSetClearArtScrape"
        Me.btnSetClearArtScrape.Size = New System.Drawing.Size(23, 23)
        Me.btnSetClearArtScrape.TabIndex = 0
        Me.btnSetClearArtScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearArtScrape.UseVisualStyleBackColor = True
        '
        'lblClearArtSize
        '
        Me.lblClearArtSize.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblClearArtSize.AutoSize = True
        Me.tblClearArt.SetColumnSpan(Me.lblClearArtSize, 5)
        Me.lblClearArtSize.Location = New System.Drawing.Point(85, 173)
        Me.lblClearArtSize.Name = "lblClearArtSize"
        Me.lblClearArtSize.Size = New System.Drawing.Size(92, 13)
        Me.lblClearArtSize.TabIndex = 5
        Me.lblClearArtSize.Text = "Size: (XXXXxXXXX)"
        Me.lblClearArtSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblClearArtSize.Visible = False
        '
        'btnSetClearArtDL
        '
        Me.btnSetClearArtDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearArtDL.Image = CType(resources.GetObject("btnSetClearArtDL.Image"), System.Drawing.Image)
        Me.btnSetClearArtDL.Location = New System.Drawing.Point(32, 193)
        Me.btnSetClearArtDL.Name = "btnSetClearArtDL"
        Me.btnSetClearArtDL.Size = New System.Drawing.Size(23, 23)
        Me.btnSetClearArtDL.TabIndex = 1
        Me.btnSetClearArtDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearArtDL.UseVisualStyleBackColor = True
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
        'pnlImagesRight
        '
        Me.pnlImagesRight.AutoSize = True
        Me.pnlImagesRight.Controls.Add(Me.tblImagesRight)
        Me.pnlImagesRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlImagesRight.Location = New System.Drawing.Point(884, 3)
        Me.pnlImagesRight.Name = "pnlImagesRight"
        Me.pnlImagesRight.Size = New System.Drawing.Size(209, 625)
        Me.pnlImagesRight.TabIndex = 3
        '
        'tblImagesRight
        '
        Me.tblImagesRight.AutoSize = True
        Me.tblImagesRight.ColumnCount = 1
        Me.tblImagesRight.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImagesRight.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImagesRight.Controls.Add(Me.pnlExtrafanarts, 0, 0)
        Me.tblImagesRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImagesRight.Location = New System.Drawing.Point(0, 0)
        Me.tblImagesRight.Name = "tblImagesRight"
        Me.tblImagesRight.RowCount = 2
        Me.tblImagesRight.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImagesRight.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImagesRight.Size = New System.Drawing.Size(209, 625)
        Me.tblImagesRight.TabIndex = 2
        '
        'pnlExtrafanarts
        '
        Me.pnlExtrafanarts.AutoSize = True
        Me.pnlExtrafanarts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlExtrafanarts.Controls.Add(Me.tblExtrafanarts)
        Me.pnlExtrafanarts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExtrafanarts.Location = New System.Drawing.Point(3, 3)
        Me.pnlExtrafanarts.Name = "pnlExtrafanarts"
        Me.pnlExtrafanarts.Size = New System.Drawing.Size(203, 579)
        Me.pnlExtrafanarts.TabIndex = 1
        '
        'tblExtrafanarts
        '
        Me.tblExtrafanarts.AutoSize = True
        Me.tblExtrafanarts.ColumnCount = 7
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblExtrafanarts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblExtrafanarts.Controls.Add(Me.btnExtrafanartsRemove, 6, 2)
        Me.tblExtrafanarts.Controls.Add(Me.lblExtrafanarts, 0, 0)
        Me.tblExtrafanarts.Controls.Add(Me.pnlExtrafanartsList, 0, 1)
        Me.tblExtrafanarts.Controls.Add(Me.btnSetExtrafanartsLocal, 2, 2)
        Me.tblExtrafanarts.Controls.Add(Me.btnExtrafanartsRefresh, 4, 2)
        Me.tblExtrafanarts.Controls.Add(Me.btnSetExtrafanartsScrape, 0, 2)
        Me.tblExtrafanarts.Controls.Add(Me.btnSetExtrafanartsDL, 1, 2)
        Me.tblExtrafanarts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblExtrafanarts.Location = New System.Drawing.Point(0, 0)
        Me.tblExtrafanarts.Name = "tblExtrafanarts"
        Me.tblExtrafanarts.RowCount = 4
        Me.tblExtrafanarts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblExtrafanarts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblExtrafanarts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblExtrafanarts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblExtrafanarts.Size = New System.Drawing.Size(201, 577)
        Me.tblExtrafanarts.TabIndex = 0
        '
        'btnExtrafanartsRemove
        '
        Me.btnExtrafanartsRemove.Enabled = False
        Me.btnExtrafanartsRemove.Image = CType(resources.GetObject("btnExtrafanartsRemove.Image"), System.Drawing.Image)
        Me.btnExtrafanartsRemove.Location = New System.Drawing.Point(175, 551)
        Me.btnExtrafanartsRemove.Name = "btnExtrafanartsRemove"
        Me.btnExtrafanartsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnExtrafanartsRemove.TabIndex = 3
        Me.btnExtrafanartsRemove.UseVisualStyleBackColor = True
        '
        'lblExtrafanarts
        '
        Me.lblExtrafanarts.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblExtrafanarts.AutoSize = True
        Me.tblExtrafanarts.SetColumnSpan(Me.lblExtrafanarts, 7)
        Me.lblExtrafanarts.Location = New System.Drawing.Point(66, 3)
        Me.lblExtrafanarts.Name = "lblExtrafanarts"
        Me.lblExtrafanarts.Size = New System.Drawing.Size(68, 13)
        Me.lblExtrafanarts.TabIndex = 2
        Me.lblExtrafanarts.Text = "Extrafanarts"
        '
        'pnlExtrafanartsList
        '
        Me.pnlExtrafanartsList.AutoScroll = True
        Me.tblExtrafanarts.SetColumnSpan(Me.pnlExtrafanartsList, 7)
        Me.pnlExtrafanartsList.Location = New System.Drawing.Point(0, 20)
        Me.pnlExtrafanartsList.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlExtrafanartsList.Name = "pnlExtrafanartsList"
        Me.pnlExtrafanartsList.Size = New System.Drawing.Size(200, 528)
        Me.pnlExtrafanartsList.TabIndex = 1
        '
        'btnSetExtrafanartsLocal
        '
        Me.btnSetExtrafanartsLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetExtrafanartsLocal.Image = CType(resources.GetObject("btnSetExtrafanartsLocal.Image"), System.Drawing.Image)
        Me.btnSetExtrafanartsLocal.Location = New System.Drawing.Point(61, 551)
        Me.btnSetExtrafanartsLocal.Name = "btnSetExtrafanartsLocal"
        Me.btnSetExtrafanartsLocal.Size = New System.Drawing.Size(23, 23)
        Me.btnSetExtrafanartsLocal.TabIndex = 2
        Me.btnSetExtrafanartsLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetExtrafanartsLocal.UseVisualStyleBackColor = True
        '
        'btnExtrafanartsRefresh
        '
        Me.btnExtrafanartsRefresh.Image = CType(resources.GetObject("btnExtrafanartsRefresh.Image"), System.Drawing.Image)
        Me.btnExtrafanartsRefresh.Location = New System.Drawing.Point(118, 551)
        Me.btnExtrafanartsRefresh.Name = "btnExtrafanartsRefresh"
        Me.btnExtrafanartsRefresh.Size = New System.Drawing.Size(23, 23)
        Me.btnExtrafanartsRefresh.TabIndex = 2
        Me.btnExtrafanartsRefresh.UseVisualStyleBackColor = True
        '
        'btnSetExtrafanartsScrape
        '
        Me.btnSetExtrafanartsScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetExtrafanartsScrape.Image = CType(resources.GetObject("btnSetExtrafanartsScrape.Image"), System.Drawing.Image)
        Me.btnSetExtrafanartsScrape.Location = New System.Drawing.Point(3, 551)
        Me.btnSetExtrafanartsScrape.Name = "btnSetExtrafanartsScrape"
        Me.btnSetExtrafanartsScrape.Size = New System.Drawing.Size(23, 23)
        Me.btnSetExtrafanartsScrape.TabIndex = 0
        Me.btnSetExtrafanartsScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetExtrafanartsScrape.UseVisualStyleBackColor = True
        '
        'btnSetExtrafanartsDL
        '
        Me.btnSetExtrafanartsDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetExtrafanartsDL.Image = CType(resources.GetObject("btnSetExtrafanartsDL.Image"), System.Drawing.Image)
        Me.btnSetExtrafanartsDL.Location = New System.Drawing.Point(32, 551)
        Me.btnSetExtrafanartsDL.Name = "btnSetExtrafanartsDL"
        Me.btnSetExtrafanartsDL.Size = New System.Drawing.Size(23, 23)
        Me.btnSetExtrafanartsDL.TabIndex = 1
        Me.btnSetExtrafanartsDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetExtrafanartsDL.UseVisualStyleBackColor = True
        '
        'dlgEditTVShow
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(1104, 791)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.StatusStrip)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgEditTVShow"
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
        CType(Me.dgvCreators, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvStudios, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCountries, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.pnlImagesRight.ResumeLayout(False)
        Me.pnlImagesRight.PerformLayout()
        Me.tblImagesRight.ResumeLayout(False)
        Me.tblImagesRight.PerformLayout()
        Me.pnlExtrafanarts.ResumeLayout(False)
        Me.pnlExtrafanarts.PerformLayout()
        Me.tblExtrafanarts.ResumeLayout(False)
        Me.tblExtrafanarts.PerformLayout()
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
    Friend WithEvents dgvCertifications As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents btnManual As Button
    Friend WithEvents txtMPAA As TextBox
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblCertifications As Label
    Friend WithEvents txtTitle As TextBox
    Friend WithEvents lbMPAA As ListBox
    Friend WithEvents lblOriginalTitle As Label
    Friend WithEvents lblMPAA As Label
    Friend WithEvents txtOriginalTitle As TextBox
    Friend WithEvents lblSortTilte As Label
    Friend WithEvents txtSortTitle As TextBox
    Friend WithEvents clbGenres As CheckedListBox
    Friend WithEvents lvActors As ListView
    Friend WithEvents colActorsID As ColumnHeader
    Friend WithEvents colActorsName As ColumnHeader
    Friend WithEvents colActorsRole As ColumnHeader
    Friend WithEvents colActorsThumb As ColumnHeader
    Friend WithEvents lblActors As Label
    Friend WithEvents lblPlot As Label
    Friend WithEvents lblCreators As Label
    Friend WithEvents txtPlot As TextBox
    Friend WithEvents lblPremiered As Label
    Friend WithEvents dtpPremiered As DateTimePicker
    Friend WithEvents lblRuntime As Label
    Friend WithEvents txtRuntime As TextBox
    Friend WithEvents lblGenres As Label
    Friend WithEvents lblTags As Label
    Friend WithEvents clbTags As CheckedListBox
    Friend WithEvents dgvCreators As DataGridView
    Friend WithEvents colCreditsName As DataGridViewTextBoxColumn
    Friend WithEvents dgvStudios As DataGridView
    Friend WithEvents colStudiosName As DataGridViewTextBoxColumn
    Friend WithEvents lblStudios As Label
    Friend WithEvents lblCountries As Label
    Friend WithEvents dgvCountries As DataGridView
    Friend WithEvents colCountriesName As DataGridViewTextBoxColumn
    Friend WithEvents btnActorsAdd As Button
    Friend WithEvents btnActorsEdit As Button
    Friend WithEvents btnActorsUp As Button
    Friend WithEvents btnActorsDown As Button
    Friend WithEvents btnActorsRemove As Button
    Friend WithEvents lblUserRating As Label
    Friend WithEvents txtUserRating As TextBox
    Friend WithEvents chkWatched As CheckBox
    Friend WithEvents dtpLastPlayed As DateTimePicker
    Friend WithEvents lblRatings As Label
    Friend WithEvents lvRatings As ListView
    Friend WithEvents colRatingsName As ColumnHeader
    Friend WithEvents colRatingsValue As ColumnHeader
    Friend WithEvents colRatingsVotes As ColumnHeader
    Friend WithEvents colRatingsMax As ColumnHeader
    Friend WithEvents btnRatingsAdd As Button
    Friend WithEvents btnRatingsEdit As Button
    Friend WithEvents btnRatingsRemove As Button
    Friend WithEvents btnCertificationsAsMPAARating As Button
    Friend WithEvents lblMPAADesc As Label
    Friend WithEvents txtMPAADesc As TextBox
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
    Friend WithEvents pnlPoster As Panel
    Friend WithEvents tblPoster As TableLayoutPanel
    Friend WithEvents pbPoster As PictureBox
    Friend WithEvents lblPoster As Label
    Friend WithEvents btnSetPosterLocal As Button
    Friend WithEvents btnSetPosterScrape As Button
    Friend WithEvents lblPosterSize As Label
    Friend WithEvents btnSetPosterDL As Button
    Friend WithEvents btnRemovePoster As Button
    Friend WithEvents pnlCharacterArt As Panel
    Friend WithEvents tblCharacterArt As TableLayoutPanel
    Friend WithEvents pbCharacterArt As PictureBox
    Friend WithEvents lblCharacterArt As Label
    Friend WithEvents btnSetCharacterArtLocal As Button
    Friend WithEvents btnSetCharacterArtScrape As Button
    Friend WithEvents lblCharacterArtSize As Label
    Friend WithEvents btnSetCharacterArtDL As Button
    Friend WithEvents btnRemoveCharacterArt As Button
    Friend WithEvents pnlClearLogo As Panel
    Friend WithEvents tblClearLogo As TableLayoutPanel
    Friend WithEvents pbClearLogo As PictureBox
    Friend WithEvents lblClearLogo As Label
    Friend WithEvents btnSetClearLogoLocal As Button
    Friend WithEvents btnSetClearLogoScrape As Button
    Friend WithEvents lblClearLogoSize As Label
    Friend WithEvents btnSetClearLogoDL As Button
    Friend WithEvents btnRemoveClearLogo As Button
    Friend WithEvents pnlFanart As Panel
    Friend WithEvents tblFanart As TableLayoutPanel
    Friend WithEvents pbFanart As PictureBox
    Friend WithEvents lblFanart As Label
    Friend WithEvents btnSetFanartLocal As Button
    Friend WithEvents btnSetFanartScrape As Button
    Friend WithEvents lblFanartSize As Label
    Friend WithEvents btnSetFanartDL As Button
    Friend WithEvents btnRemoveFanart As Button
    Friend WithEvents pnlLandscape As Panel
    Friend WithEvents tblLandscape As TableLayoutPanel
    Friend WithEvents pbLandscape As PictureBox
    Friend WithEvents lblLandscape As Label
    Friend WithEvents btnSetLandscapeLocal As Button
    Friend WithEvents btnSetLandscapeScrape As Button
    Friend WithEvents lblLandscapeSize As Label
    Friend WithEvents btnSetLandscapeDL As Button
    Friend WithEvents btnRemoveLandscape As Button
    Friend WithEvents pnlBanner As Panel
    Friend WithEvents tblBanner As TableLayoutPanel
    Friend WithEvents pbBanner As PictureBox
    Friend WithEvents lblBanner As Label
    Friend WithEvents btnSetBannerLocal As Button
    Friend WithEvents btnSetBannerScrape As Button
    Friend WithEvents lblBannerSize As Label
    Friend WithEvents btnSetBannerDL As Button
    Friend WithEvents btnRemoveBanner As Button
    Friend WithEvents pnlClearArt As Panel
    Friend WithEvents tblClearArt As TableLayoutPanel
    Friend WithEvents pbClearArt As PictureBox
    Friend WithEvents lblClearArt As Label
    Friend WithEvents btnSetClearArtLocal As Button
    Friend WithEvents btnSetClearArtScrape As Button
    Friend WithEvents lblClearArtSize As Label
    Friend WithEvents btnSetClearArtDL As Button
    Friend WithEvents btnRemoveClearArt As Button
    Friend WithEvents lblStatus As Label
    Friend WithEvents txtStatus As TextBox
    Friend WithEvents pnlImagesRight As Panel
    Friend WithEvents tblExtrafanarts As TableLayoutPanel
    Friend WithEvents btnExtrafanartsRemove As Button
    Friend WithEvents pnlExtrafanartsList As Panel
    Friend WithEvents btnSetExtrafanartsLocal As Button
    Friend WithEvents btnExtrafanartsRefresh As Button
    Friend WithEvents btnSetExtrafanartsScrape As Button
    Friend WithEvents btnSetExtrafanartsDL As Button
    Friend WithEvents lblExtrafanarts As Label
    Friend WithEvents pnlExtrafanarts As Panel
    Friend WithEvents tblImagesRight As TableLayoutPanel
End Class
