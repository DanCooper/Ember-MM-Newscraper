<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgEditMovieSet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgEditMovieSet))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tblTop = New System.Windows.Forms.TableLayoutPanel()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.ofdLocalFiles = New System.Windows.Forms.OpenFileDialog()
        Me.tmrDelay = New System.Windows.Forms.Timer(Me.components)
        Me.tmrKeyBuffer = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearch_Movies = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSearch_Wait = New System.Windows.Forms.Timer(Me.components)
        Me.lblMovieSorting = New System.Windows.Forms.Label()
        Me.cbMovieSorting = New System.Windows.Forms.ComboBox()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnRescrape = New System.Windows.Forms.Button()
        Me.lblLanguage = New System.Windows.Forms.Label()
        Me.cbSourceLanguage = New System.Windows.Forms.ComboBox()
        Me.chkLocked = New System.Windows.Forms.CheckBox()
        Me.chkMarked = New System.Windows.Forms.CheckBox()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tcEdit = New System.Windows.Forms.TabControl()
        Me.tpDetails = New System.Windows.Forms.TabPage()
        Me.pnlDetails = New System.Windows.Forms.Panel()
        Me.tblDetails = New System.Windows.Forms.TableLayoutPanel()
        Me.txtCollectionID = New System.Windows.Forms.TextBox()
        Me.lblCollectionID = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.lblPlot = New System.Windows.Forms.Label()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.btnGetTMDbColID = New System.Windows.Forms.Button()
        Me.gbMovieAssignment = New System.Windows.Forms.GroupBox()
        Me.tblMovieAssignment = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvDatabaseList = New System.Windows.Forms.DataGridView()
        Me.txtSearchMovies = New System.Windows.Forms.TextBox()
        Me.lblMoviesInMovieset = New System.Windows.Forms.Label()
        Me.lblMoviesInDB = New System.Windows.Forms.Label()
        Me.lvMoviesInSet = New System.Windows.Forms.ListView()
        Me.colID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colOrdering = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovie = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnMovieRemove = New System.Windows.Forms.Button()
        Me.btnMovieDown = New System.Windows.Forms.Button()
        Me.btnMovieAdd = New System.Windows.Forms.Button()
        Me.btnMovieUp = New System.Windows.Forms.Button()
        Me.btnSearchMovie = New System.Windows.Forms.Button()
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
        Me.pnlDiscArt = New System.Windows.Forms.Panel()
        Me.tblDiscArt = New System.Windows.Forms.TableLayoutPanel()
        Me.pbDiscArt = New System.Windows.Forms.PictureBox()
        Me.lblDiscArt = New System.Windows.Forms.Label()
        Me.btnSetDiscArtLocal = New System.Windows.Forms.Button()
        Me.btnSetDiscArtScrape = New System.Windows.Forms.Button()
        Me.lblDiscArtSize = New System.Windows.Forms.Label()
        Me.btnSetDiscArtDL = New System.Windows.Forms.Button()
        Me.btnRemoveDiscArt = New System.Windows.Forms.Button()
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
        Me.pnlTop.SuspendLayout()
        Me.tblTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.tcEdit.SuspendLayout()
        Me.tpDetails.SuspendLayout()
        Me.pnlDetails.SuspendLayout()
        Me.tblDetails.SuspendLayout()
        Me.gbMovieAssignment.SuspendLayout()
        Me.tblMovieAssignment.SuspendLayout()
        CType(Me.dgvDatabaseList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpImages.SuspendLayout()
        Me.tblImages.SuspendLayout()
        Me.pnlPoster.SuspendLayout()
        Me.tblPoster.SuspendLayout()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDiscArt.SuspendLayout()
        Me.tblDiscArt.SuspendLayout()
        CType(Me.pbDiscArt, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.lblTopDetails.Size = New System.Drawing.Size(220, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Edit the details for the selected movieset."
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
        Me.lblTopTitle.Size = New System.Drawing.Size(172, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Edit MovieSet"
        '
        'tmrDelay
        '
        Me.tmrDelay.Interval = 250
        '
        'tmrKeyBuffer
        '
        Me.tmrKeyBuffer.Interval = 2000
        '
        'tmrSearch_Movies
        '
        Me.tmrSearch_Movies.Interval = 250
        '
        'tmrSearch_Wait
        '
        Me.tmrSearch_Wait.Interval = 250
        '
        'lblMovieSorting
        '
        Me.lblMovieSorting.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSorting.AutoSize = True
        Me.lblMovieSorting.Location = New System.Drawing.Point(249, 31)
        Me.lblMovieSorting.Name = "lblMovieSorting"
        Me.lblMovieSorting.Size = New System.Drawing.Size(97, 13)
        Me.lblMovieSorting.TabIndex = 9
        Me.lblMovieSorting.Text = "Movies sorted by:"
        Me.lblMovieSorting.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cbMovieSorting
        '
        Me.cbMovieSorting.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbMovieSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieSorting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbMovieSorting.FormattingEnabled = True
        Me.cbMovieSorting.Location = New System.Drawing.Point(352, 27)
        Me.cbMovieSorting.Name = "cbMovieSorting"
        Me.cbMovieSorting.Size = New System.Drawing.Size(92, 21)
        Me.cbMovieSorting.TabIndex = 10
        '
        'StatusStrip
        '
        Me.StatusStrip.Location = New System.Drawing.Point(0, 769)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1104, 22)
        Me.StatusStrip.TabIndex = 11
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 717)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1104, 52)
        Me.pnlBottom.TabIndex = 80
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 9
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnOK, 7, 1)
        Me.tblBottom.Controls.Add(Me.cbMovieSorting, 3, 1)
        Me.tblBottom.Controls.Add(Me.btnCancel, 8, 1)
        Me.tblBottom.Controls.Add(Me.btnRescrape, 5, 1)
        Me.tblBottom.Controls.Add(Me.lblLanguage, 0, 1)
        Me.tblBottom.Controls.Add(Me.cbSourceLanguage, 1, 1)
        Me.tblBottom.Controls.Add(Me.chkLocked, 0, 0)
        Me.tblBottom.Controls.Add(Me.chkMarked, 1, 0)
        Me.tblBottom.Controls.Add(Me.lblMovieSorting, 2, 1)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 2
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblBottom.Size = New System.Drawing.Size(1104, 52)
        Me.tblBottom.TabIndex = 78
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnOK.AutoSize = True
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnOK.Location = New System.Drawing.Point(946, 26)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(70, 23)
        Me.btnOK.TabIndex = 9
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnCancel.AutoSize = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(1022, 26)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(78, 23)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        '
        'btnRescrape
        '
        Me.btnRescrape.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnRescrape.AutoSize = True
        Me.btnRescrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRescrape.Image = CType(resources.GetObject("btnRescrape.Image"), System.Drawing.Image)
        Me.btnRescrape.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRescrape.Location = New System.Drawing.Point(646, 26)
        Me.btnRescrape.Name = "btnRescrape"
        Me.btnRescrape.Size = New System.Drawing.Size(98, 23)
        Me.btnRescrape.TabIndex = 7
        Me.btnRescrape.Text = "Re-scrape"
        Me.btnRescrape.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRescrape.UseVisualStyleBackColor = True
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
        'cbSourceLanguage
        '
        Me.cbSourceLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbSourceLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSourceLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSourceLanguage.Location = New System.Drawing.Point(71, 27)
        Me.cbSourceLanguage.Name = "cbSourceLanguage"
        Me.cbSourceLanguage.Size = New System.Drawing.Size(172, 21)
        Me.cbSourceLanguage.TabIndex = 2
        '
        'chkLocked
        '
        Me.chkLocked.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkLocked.AutoSize = True
        Me.chkLocked.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLocked.Location = New System.Drawing.Point(3, 3)
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
        Me.chkMarked.Location = New System.Drawing.Point(71, 3)
        Me.chkMarked.Name = "chkMarked"
        Me.chkMarked.Size = New System.Drawing.Size(65, 17)
        Me.chkMarked.TabIndex = 1
        Me.chkMarked.Text = "Marked"
        Me.chkMarked.UseVisualStyleBackColor = True
        '
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlMain.Controls.Add(Me.tcEdit)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1104, 661)
        Me.pnlMain.TabIndex = 81
        '
        'tcEdit
        '
        Me.tcEdit.Controls.Add(Me.tpDetails)
        Me.tcEdit.Controls.Add(Me.tpImages)
        Me.tcEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tcEdit.Location = New System.Drawing.Point(0, 0)
        Me.tcEdit.Name = "tcEdit"
        Me.tcEdit.SelectedIndex = 0
        Me.tcEdit.Size = New System.Drawing.Size(1104, 661)
        Me.tcEdit.TabIndex = 0
        '
        'tpDetails
        '
        Me.tpDetails.Controls.Add(Me.pnlDetails)
        Me.tpDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpDetails.Name = "tpDetails"
        Me.tpDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDetails.Size = New System.Drawing.Size(1096, 635)
        Me.tpDetails.TabIndex = 0
        Me.tpDetails.Text = "Details"
        Me.tpDetails.UseVisualStyleBackColor = True
        '
        'pnlDetails
        '
        Me.pnlDetails.AutoSize = True
        Me.pnlDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlDetails.Controls.Add(Me.tblDetails)
        Me.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetails.Location = New System.Drawing.Point(3, 3)
        Me.pnlDetails.Name = "pnlDetails"
        Me.pnlDetails.Size = New System.Drawing.Size(1090, 629)
        Me.pnlDetails.TabIndex = 79
        '
        'tblDetails
        '
        Me.tblDetails.AutoScroll = True
        Me.tblDetails.AutoSize = True
        Me.tblDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblDetails.ColumnCount = 3
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblDetails.Controls.Add(Me.txtCollectionID, 0, 3)
        Me.tblDetails.Controls.Add(Me.lblCollectionID, 0, 2)
        Me.tblDetails.Controls.Add(Me.lblTitle, 0, 0)
        Me.tblDetails.Controls.Add(Me.txtTitle, 0, 1)
        Me.tblDetails.Controls.Add(Me.lblPlot, 2, 0)
        Me.tblDetails.Controls.Add(Me.txtPlot, 2, 1)
        Me.tblDetails.Controls.Add(Me.btnGetTMDbColID, 1, 3)
        Me.tblDetails.Controls.Add(Me.gbMovieAssignment, 0, 4)
        Me.tblDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblDetails.Location = New System.Drawing.Point(0, 0)
        Me.tblDetails.Name = "tblDetails"
        Me.tblDetails.RowCount = 5
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.Size = New System.Drawing.Size(1090, 629)
        Me.tblDetails.TabIndex = 78
        '
        'txtCollectionID
        '
        Me.txtCollectionID.BackColor = System.Drawing.SystemColors.Window
        Me.txtCollectionID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCollectionID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtCollectionID.Location = New System.Drawing.Point(3, 57)
        Me.txtCollectionID.Name = "txtCollectionID"
        Me.txtCollectionID.Size = New System.Drawing.Size(79, 22)
        Me.txtCollectionID.TabIndex = 48
        '
        'lblCollectionID
        '
        Me.lblCollectionID.AutoSize = True
        Me.lblCollectionID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCollectionID.Location = New System.Drawing.Point(3, 41)
        Me.lblCollectionID.Name = "lblCollectionID"
        Me.lblCollectionID.Size = New System.Drawing.Size(76, 13)
        Me.lblCollectionID.TabIndex = 47
        Me.lblCollectionID.Text = "Collection ID:"
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
        'txtTitle
        '
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtTitle, 2)
        Me.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(3, 16)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(234, 22)
        Me.txtTitle.TabIndex = 0
        '
        'lblPlot
        '
        Me.lblPlot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblPlot.AutoSize = True
        Me.lblPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPlot.Location = New System.Drawing.Point(243, 0)
        Me.lblPlot.Name = "lblPlot"
        Me.lblPlot.Size = New System.Drawing.Size(30, 13)
        Me.lblPlot.TabIndex = 27
        Me.lblPlot.Text = "Plot:"
        '
        'txtPlot
        '
        Me.txtPlot.AcceptsReturn = True
        Me.txtPlot.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtPlot.Location = New System.Drawing.Point(243, 16)
        Me.txtPlot.Multiline = True
        Me.txtPlot.Name = "txtPlot"
        Me.tblDetails.SetRowSpan(Me.txtPlot, 3)
        Me.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlot.Size = New System.Drawing.Size(844, 65)
        Me.txtPlot.TabIndex = 10
        '
        'btnGetTMDbColID
        '
        Me.btnGetTMDbColID.AutoSize = True
        Me.btnGetTMDbColID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnGetTMDbColID.Location = New System.Drawing.Point(88, 57)
        Me.btnGetTMDbColID.Name = "btnGetTMDbColID"
        Me.btnGetTMDbColID.Size = New System.Drawing.Size(149, 24)
        Me.btnGetTMDbColID.TabIndex = 49
        Me.btnGetTMDbColID.Text = "Get TMDB Collection ID"
        Me.btnGetTMDbColID.UseVisualStyleBackColor = True
        '
        'gbMovieAssignment
        '
        Me.tblDetails.SetColumnSpan(Me.gbMovieAssignment, 3)
        Me.gbMovieAssignment.Controls.Add(Me.tblMovieAssignment)
        Me.gbMovieAssignment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieAssignment.Location = New System.Drawing.Point(3, 87)
        Me.gbMovieAssignment.Name = "gbMovieAssignment"
        Me.gbMovieAssignment.Size = New System.Drawing.Size(1084, 539)
        Me.gbMovieAssignment.TabIndex = 50
        Me.gbMovieAssignment.TabStop = False
        Me.gbMovieAssignment.Text = "Movie Assignment"
        '
        'tblMovieAssignment
        '
        Me.tblMovieAssignment.AutoSize = True
        Me.tblMovieAssignment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblMovieAssignment.ColumnCount = 8
        Me.tblMovieAssignment.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblMovieAssignment.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieAssignment.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieAssignment.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblMovieAssignment.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieAssignment.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieAssignment.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieAssignment.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieAssignment.Controls.Add(Me.dgvDatabaseList, 6, 1)
        Me.tblMovieAssignment.Controls.Add(Me.txtSearchMovies, 6, 2)
        Me.tblMovieAssignment.Controls.Add(Me.lblMoviesInMovieset, 0, 0)
        Me.tblMovieAssignment.Controls.Add(Me.lblMoviesInDB, 6, 0)
        Me.tblMovieAssignment.Controls.Add(Me.lvMoviesInSet, 0, 1)
        Me.tblMovieAssignment.Controls.Add(Me.btnMovieRemove, 4, 2)
        Me.tblMovieAssignment.Controls.Add(Me.btnMovieDown, 2, 2)
        Me.tblMovieAssignment.Controls.Add(Me.btnMovieAdd, 5, 1)
        Me.tblMovieAssignment.Controls.Add(Me.btnMovieUp, 1, 2)
        Me.tblMovieAssignment.Controls.Add(Me.btnSearchMovie, 7, 2)
        Me.tblMovieAssignment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieAssignment.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieAssignment.Name = "tblMovieAssignment"
        Me.tblMovieAssignment.RowCount = 3
        Me.tblMovieAssignment.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieAssignment.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMovieAssignment.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieAssignment.Size = New System.Drawing.Size(1078, 518)
        Me.tblMovieAssignment.TabIndex = 0
        '
        'dgvDatabaseList
        '
        Me.dgvDatabaseList.AllowUserToAddRows = False
        Me.dgvDatabaseList.AllowUserToDeleteRows = False
        Me.dgvDatabaseList.AllowUserToResizeColumns = False
        Me.dgvDatabaseList.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.dgvDatabaseList.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDatabaseList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvDatabaseList.BackgroundColor = System.Drawing.Color.White
        Me.dgvDatabaseList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvDatabaseList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvDatabaseList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvDatabaseList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.tblMovieAssignment.SetColumnSpan(Me.dgvDatabaseList, 2)
        Me.dgvDatabaseList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDatabaseList.GridColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvDatabaseList.Location = New System.Drawing.Point(635, 16)
        Me.dgvDatabaseList.Name = "dgvDatabaseList"
        Me.dgvDatabaseList.ReadOnly = True
        Me.dgvDatabaseList.RowHeadersVisible = False
        Me.dgvDatabaseList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDatabaseList.ShowCellErrors = False
        Me.dgvDatabaseList.ShowRowErrors = False
        Me.dgvDatabaseList.Size = New System.Drawing.Size(440, 470)
        Me.dgvDatabaseList.StandardTab = True
        Me.dgvDatabaseList.TabIndex = 50
        '
        'txtSearchMovies
        '
        Me.txtSearchMovies.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchMovies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSearchMovies.Location = New System.Drawing.Point(635, 492)
        Me.txtSearchMovies.Name = "txtSearchMovies"
        Me.txtSearchMovies.Size = New System.Drawing.Size(348, 22)
        Me.txtSearchMovies.TabIndex = 51
        '
        'lblMoviesInMovieset
        '
        Me.lblMoviesInMovieset.AutoSize = True
        Me.tblMovieAssignment.SetColumnSpan(Me.lblMoviesInMovieset, 5)
        Me.lblMoviesInMovieset.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMoviesInMovieset.Location = New System.Drawing.Point(3, 0)
        Me.lblMoviesInMovieset.Name = "lblMoviesInMovieset"
        Me.lblMoviesInMovieset.Size = New System.Drawing.Size(108, 13)
        Me.lblMoviesInMovieset.TabIndex = 29
        Me.lblMoviesInMovieset.Text = "Movies in Movieset:"
        '
        'lblMoviesInDB
        '
        Me.lblMoviesInDB.AutoSize = True
        Me.lblMoviesInDB.Enabled = False
        Me.lblMoviesInDB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMoviesInDB.Location = New System.Drawing.Point(635, 0)
        Me.lblMoviesInDB.Name = "lblMoviesInDB"
        Me.lblMoviesInDB.Size = New System.Drawing.Size(55, 13)
        Me.lblMoviesInDB.TabIndex = 39
        Me.lblMoviesInDB.Text = "Database"
        Me.lblMoviesInDB.Visible = False
        '
        'lvMoviesInSet
        '
        Me.lvMoviesInSet.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colID, Me.colOrdering, Me.colMovie})
        Me.tblMovieAssignment.SetColumnSpan(Me.lvMoviesInSet, 5)
        Me.lvMoviesInSet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvMoviesInSet.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvMoviesInSet.FullRowSelect = True
        Me.lvMoviesInSet.Location = New System.Drawing.Point(3, 16)
        Me.lvMoviesInSet.Name = "lvMoviesInSet"
        Me.lvMoviesInSet.Size = New System.Drawing.Size(597, 470)
        Me.lvMoviesInSet.TabIndex = 48
        Me.lvMoviesInSet.UseCompatibleStateImageBehavior = False
        Me.lvMoviesInSet.View = System.Windows.Forms.View.Details
        '
        'colID
        '
        Me.colID.Text = "ID"
        Me.colID.Width = 0
        '
        'colOrdering
        '
        Me.colOrdering.Text = "Ordering"
        Me.colOrdering.Width = 0
        '
        'colMovie
        '
        Me.colMovie.Text = "Movie"
        Me.colMovie.Width = 198
        '
        'btnMovieRemove
        '
        Me.btnMovieRemove.Enabled = False
        Me.btnMovieRemove.Image = CType(resources.GetObject("btnMovieRemove.Image"), System.Drawing.Image)
        Me.btnMovieRemove.Location = New System.Drawing.Point(577, 492)
        Me.btnMovieRemove.Name = "btnMovieRemove"
        Me.btnMovieRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieRemove.TabIndex = 35
        Me.btnMovieRemove.UseVisualStyleBackColor = True
        '
        'btnMovieDown
        '
        Me.btnMovieDown.Enabled = False
        Me.btnMovieDown.Image = CType(resources.GetObject("btnMovieDown.Image"), System.Drawing.Image)
        Me.btnMovieDown.Location = New System.Drawing.Point(290, 492)
        Me.btnMovieDown.Name = "btnMovieDown"
        Me.btnMovieDown.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieDown.TabIndex = 34
        Me.btnMovieDown.UseVisualStyleBackColor = True
        Me.btnMovieDown.Visible = False
        '
        'btnMovieAdd
        '
        Me.btnMovieAdd.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnMovieAdd.Enabled = False
        Me.btnMovieAdd.Image = CType(resources.GetObject("btnMovieAdd.Image"), System.Drawing.Image)
        Me.btnMovieAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieAdd.Location = New System.Drawing.Point(606, 239)
        Me.btnMovieAdd.Name = "btnMovieAdd"
        Me.btnMovieAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieAdd.TabIndex = 40
        Me.btnMovieAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieAdd.UseVisualStyleBackColor = True
        '
        'btnMovieUp
        '
        Me.btnMovieUp.Enabled = False
        Me.btnMovieUp.Image = CType(resources.GetObject("btnMovieUp.Image"), System.Drawing.Image)
        Me.btnMovieUp.Location = New System.Drawing.Point(261, 492)
        Me.btnMovieUp.Name = "btnMovieUp"
        Me.btnMovieUp.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieUp.TabIndex = 33
        Me.btnMovieUp.UseVisualStyleBackColor = True
        Me.btnMovieUp.Visible = False
        '
        'btnSearchMovie
        '
        Me.btnSearchMovie.AutoSize = True
        Me.btnSearchMovie.Location = New System.Drawing.Point(989, 492)
        Me.btnSearchMovie.Name = "btnSearchMovie"
        Me.btnSearchMovie.Size = New System.Drawing.Size(85, 23)
        Me.btnSearchMovie.TabIndex = 52
        Me.btnSearchMovie.Text = "Search Movie"
        Me.btnSearchMovie.UseVisualStyleBackColor = True
        '
        'tpImages
        '
        Me.tpImages.Controls.Add(Me.tblImages)
        Me.tpImages.Location = New System.Drawing.Point(4, 22)
        Me.tpImages.Name = "tpImages"
        Me.tpImages.Padding = New System.Windows.Forms.Padding(3)
        Me.tpImages.Size = New System.Drawing.Size(1096, 635)
        Me.tpImages.TabIndex = 16
        Me.tpImages.Text = "Images"
        Me.tpImages.UseVisualStyleBackColor = True
        '
        'tblImages
        '
        Me.tblImages.AutoScroll = True
        Me.tblImages.AutoSize = True
        Me.tblImages.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblImages.ColumnCount = 4
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.Controls.Add(Me.pnlPoster, 0, 0)
        Me.tblImages.Controls.Add(Me.pnlDiscArt, 2, 1)
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
        Me.tblImages.Size = New System.Drawing.Size(1090, 629)
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
        'pnlDiscArt
        '
        Me.pnlDiscArt.AutoSize = True
        Me.pnlDiscArt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlDiscArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDiscArt.Controls.Add(Me.tblDiscArt)
        Me.pnlDiscArt.Location = New System.Drawing.Point(543, 230)
        Me.pnlDiscArt.Name = "pnlDiscArt"
        Me.pnlDiscArt.Size = New System.Drawing.Size(264, 221)
        Me.pnlDiscArt.TabIndex = 5
        '
        'tblDiscArt
        '
        Me.tblDiscArt.AutoScroll = True
        Me.tblDiscArt.AutoSize = True
        Me.tblDiscArt.ColumnCount = 5
        Me.tblDiscArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDiscArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDiscArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDiscArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblDiscArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDiscArt.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblDiscArt.Controls.Add(Me.pbDiscArt, 0, 1)
        Me.tblDiscArt.Controls.Add(Me.lblDiscArt, 0, 0)
        Me.tblDiscArt.Controls.Add(Me.btnSetDiscArtLocal, 2, 3)
        Me.tblDiscArt.Controls.Add(Me.btnSetDiscArtScrape, 0, 3)
        Me.tblDiscArt.Controls.Add(Me.lblDiscArtSize, 0, 2)
        Me.tblDiscArt.Controls.Add(Me.btnSetDiscArtDL, 1, 3)
        Me.tblDiscArt.Controls.Add(Me.btnRemoveDiscArt, 4, 3)
        Me.tblDiscArt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblDiscArt.Location = New System.Drawing.Point(0, 0)
        Me.tblDiscArt.Name = "tblDiscArt"
        Me.tblDiscArt.RowCount = 4
        Me.tblDiscArt.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblDiscArt.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDiscArt.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblDiscArt.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDiscArt.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblDiscArt.Size = New System.Drawing.Size(262, 219)
        Me.tblDiscArt.TabIndex = 0
        '
        'pbDiscArt
        '
        Me.pbDiscArt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.pbDiscArt.BackColor = System.Drawing.Color.White
        Me.tblDiscArt.SetColumnSpan(Me.pbDiscArt, 5)
        Me.pbDiscArt.Location = New System.Drawing.Point(3, 23)
        Me.pbDiscArt.Name = "pbDiscArt"
        Me.pbDiscArt.Size = New System.Drawing.Size(256, 144)
        Me.pbDiscArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbDiscArt.TabIndex = 1
        Me.pbDiscArt.TabStop = False
        '
        'lblDiscArt
        '
        Me.lblDiscArt.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblDiscArt.AutoSize = True
        Me.tblDiscArt.SetColumnSpan(Me.lblDiscArt, 5)
        Me.lblDiscArt.Location = New System.Drawing.Point(109, 3)
        Me.lblDiscArt.Name = "lblDiscArt"
        Me.lblDiscArt.Size = New System.Drawing.Size(43, 13)
        Me.lblDiscArt.TabIndex = 2
        Me.lblDiscArt.Text = "DiscArt"
        '
        'btnSetDiscArtLocal
        '
        Me.btnSetDiscArtLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetDiscArtLocal.Image = CType(resources.GetObject("btnSetDiscArtLocal.Image"), System.Drawing.Image)
        Me.btnSetDiscArtLocal.Location = New System.Drawing.Point(61, 193)
        Me.btnSetDiscArtLocal.Name = "btnSetDiscArtLocal"
        Me.btnSetDiscArtLocal.Size = New System.Drawing.Size(23, 23)
        Me.btnSetDiscArtLocal.TabIndex = 2
        Me.btnSetDiscArtLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetDiscArtLocal.UseVisualStyleBackColor = True
        '
        'btnSetDiscArtScrape
        '
        Me.btnSetDiscArtScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetDiscArtScrape.Image = CType(resources.GetObject("btnSetDiscArtScrape.Image"), System.Drawing.Image)
        Me.btnSetDiscArtScrape.Location = New System.Drawing.Point(3, 193)
        Me.btnSetDiscArtScrape.Name = "btnSetDiscArtScrape"
        Me.btnSetDiscArtScrape.Size = New System.Drawing.Size(23, 23)
        Me.btnSetDiscArtScrape.TabIndex = 0
        Me.btnSetDiscArtScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetDiscArtScrape.UseVisualStyleBackColor = True
        '
        'lblDiscArtSize
        '
        Me.lblDiscArtSize.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblDiscArtSize.AutoSize = True
        Me.tblDiscArt.SetColumnSpan(Me.lblDiscArtSize, 5)
        Me.lblDiscArtSize.Location = New System.Drawing.Point(85, 173)
        Me.lblDiscArtSize.Name = "lblDiscArtSize"
        Me.lblDiscArtSize.Size = New System.Drawing.Size(92, 13)
        Me.lblDiscArtSize.TabIndex = 5
        Me.lblDiscArtSize.Text = "Size: (XXXXxXXXX)"
        Me.lblDiscArtSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblDiscArtSize.Visible = False
        '
        'btnSetDiscArtDL
        '
        Me.btnSetDiscArtDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetDiscArtDL.Image = CType(resources.GetObject("btnSetDiscArtDL.Image"), System.Drawing.Image)
        Me.btnSetDiscArtDL.Location = New System.Drawing.Point(32, 193)
        Me.btnSetDiscArtDL.Name = "btnSetDiscArtDL"
        Me.btnSetDiscArtDL.Size = New System.Drawing.Size(23, 23)
        Me.btnSetDiscArtDL.TabIndex = 1
        Me.btnSetDiscArtDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetDiscArtDL.UseVisualStyleBackColor = True
        '
        'btnRemoveDiscArt
        '
        Me.btnRemoveDiscArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveDiscArt.Image = CType(resources.GetObject("btnRemoveDiscArt.Image"), System.Drawing.Image)
        Me.btnRemoveDiscArt.Location = New System.Drawing.Point(236, 193)
        Me.btnRemoveDiscArt.Name = "btnRemoveDiscArt"
        Me.btnRemoveDiscArt.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveDiscArt.TabIndex = 3
        Me.btnRemoveDiscArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveDiscArt.UseVisualStyleBackColor = True
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
        'dlgEditMovieSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1104, 791)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.StatusStrip)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgEditMovieSet"
        Me.Text = "Edit Movie"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tblTop.ResumeLayout(False)
        Me.tblTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.tblBottom.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.tcEdit.ResumeLayout(False)
        Me.tpDetails.ResumeLayout(False)
        Me.tpDetails.PerformLayout()
        Me.pnlDetails.ResumeLayout(False)
        Me.pnlDetails.PerformLayout()
        Me.tblDetails.ResumeLayout(False)
        Me.tblDetails.PerformLayout()
        Me.gbMovieAssignment.ResumeLayout(False)
        Me.gbMovieAssignment.PerformLayout()
        Me.tblMovieAssignment.ResumeLayout(False)
        Me.tblMovieAssignment.PerformLayout()
        CType(Me.dgvDatabaseList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpImages.ResumeLayout(False)
        Me.tpImages.PerformLayout()
        Me.tblImages.ResumeLayout(False)
        Me.tblImages.PerformLayout()
        Me.pnlPoster.ResumeLayout(False)
        Me.pnlPoster.PerformLayout()
        Me.tblPoster.ResumeLayout(False)
        Me.tblPoster.PerformLayout()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDiscArt.ResumeLayout(False)
        Me.pnlDiscArt.PerformLayout()
        Me.tblDiscArt.ResumeLayout(False)
        Me.tblDiscArt.PerformLayout()
        CType(Me.pbDiscArt, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents ofdLocalFiles As System.Windows.Forms.OpenFileDialog
    Friend WithEvents tmrDelay As System.Windows.Forms.Timer
    Friend WithEvents tmrKeyBuffer As System.Windows.Forms.Timer
    Friend WithEvents tmrSearch_Movies As System.Windows.Forms.Timer
    Friend WithEvents tmrSearch_Wait As System.Windows.Forms.Timer
    Friend WithEvents lblMovieSorting As System.Windows.Forms.Label
    Friend WithEvents cbMovieSorting As System.Windows.Forms.ComboBox
    Friend WithEvents tblTop As TableLayoutPanel
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents tblBottom As TableLayoutPanel
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnRescrape As Button
    Friend WithEvents lblLanguage As Label
    Friend WithEvents cbSourceLanguage As ComboBox
    Friend WithEvents chkLocked As CheckBox
    Friend WithEvents chkMarked As CheckBox
    Friend WithEvents pnlMain As Panel
    Friend WithEvents tcEdit As TabControl
    Friend WithEvents tpDetails As TabPage
    Friend WithEvents pnlDetails As Panel
    Friend WithEvents tblDetails As TableLayoutPanel
    Friend WithEvents txtCollectionID As TextBox
    Friend WithEvents lblCollectionID As Label
    Friend WithEvents lblTitle As Label
    Friend WithEvents txtTitle As TextBox
    Friend WithEvents lblPlot As Label
    Friend WithEvents txtPlot As TextBox
    Friend WithEvents btnGetTMDbColID As Button
    Friend WithEvents gbMovieAssignment As GroupBox
    Friend WithEvents tblMovieAssignment As TableLayoutPanel
    Friend WithEvents dgvDatabaseList As DataGridView
    Friend WithEvents txtSearchMovies As TextBox
    Friend WithEvents lblMoviesInMovieset As Label
    Friend WithEvents lblMoviesInDB As Label
    Friend WithEvents lvMoviesInSet As ListView
    Friend WithEvents colID As ColumnHeader
    Friend WithEvents colOrdering As ColumnHeader
    Friend WithEvents colMovie As ColumnHeader
    Friend WithEvents btnMovieRemove As Button
    Friend WithEvents btnMovieDown As Button
    Friend WithEvents btnMovieAdd As Button
    Friend WithEvents btnMovieUp As Button
    Friend WithEvents btnSearchMovie As Button
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
    Friend WithEvents pnlDiscArt As Panel
    Friend WithEvents tblDiscArt As TableLayoutPanel
    Friend WithEvents pbDiscArt As PictureBox
    Friend WithEvents lblDiscArt As Label
    Friend WithEvents btnSetDiscArtLocal As Button
    Friend WithEvents btnSetDiscArtScrape As Button
    Friend WithEvents lblDiscArtSize As Label
    Friend WithEvents btnSetDiscArtDL As Button
    Friend WithEvents btnRemoveDiscArt As Button
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
End Class
