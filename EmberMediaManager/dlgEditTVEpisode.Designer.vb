<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgEditTVEpisode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgEditTVEpisode))
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Local Subtitles", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("1")
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.tsslFilename = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslSpring = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tspbStatus = New System.Windows.Forms.ToolStripProgressBar()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tcEdit = New System.Windows.Forms.TabControl()
        Me.tpDetails = New System.Windows.Forms.TabPage()
        Me.tblDetails = New System.Windows.Forms.TableLayoutPanel()
        Me.btnManual = New System.Windows.Forms.Button()
        Me.cbVideoSource = New System.Windows.Forms.ComboBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.lblOriginalTitle = New System.Windows.Forms.Label()
        Me.txtOriginalTitle = New System.Windows.Forms.TextBox()
        Me.lvActors = New System.Windows.Forms.ListView()
        Me.colActorsID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colActorsName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colActorsRole = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colActorsThumb = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblActors = New System.Windows.Forms.Label()
        Me.lblPlot = New System.Windows.Forms.Label()
        Me.lblCredits = New System.Windows.Forms.Label()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.dtpAired = New System.Windows.Forms.DateTimePicker()
        Me.txtRuntime = New System.Windows.Forms.TextBox()
        Me.lblVideoSource = New System.Windows.Forms.Label()
        Me.dgvCredits = New System.Windows.Forms.DataGridView()
        Me.colCreditsName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDirectors = New System.Windows.Forms.DataGridView()
        Me.colDirectorsName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblDirectors = New System.Windows.Forms.Label()
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
        Me.lblAired = New System.Windows.Forms.Label()
        Me.lblSeason = New System.Windows.Forms.Label()
        Me.lblEpisode = New System.Windows.Forms.Label()
        Me.txtSeason = New System.Windows.Forms.TextBox()
        Me.txtEpisode = New System.Windows.Forms.TextBox()
        Me.lblRuntime = New System.Windows.Forms.Label()
        Me.lblDisplaySeason = New System.Windows.Forms.Label()
        Me.txtDisplaySeason = New System.Windows.Forms.TextBox()
        Me.lblDisplayEpisode = New System.Windows.Forms.Label()
        Me.txtDisplayEpisode = New System.Windows.Forms.TextBox()
        Me.btnActorsAdd = New System.Windows.Forms.Button()
        Me.btnActorsEdit = New System.Windows.Forms.Button()
        Me.btnActorsUp = New System.Windows.Forms.Button()
        Me.btnActorsDown = New System.Windows.Forms.Button()
        Me.btnActorsRemove = New System.Windows.Forms.Button()
        Me.lblGuestStars = New System.Windows.Forms.Label()
        Me.lvGuestStars = New System.Windows.Forms.ListView()
        Me.colGuestStarsID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colGuestStarsName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colGuestStarsRole = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colGuestStarsThumb = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnGuestStarsAdd = New System.Windows.Forms.Button()
        Me.btnGuestStarsEdit = New System.Windows.Forms.Button()
        Me.btnGuestStarsUp = New System.Windows.Forms.Button()
        Me.btnGuestStarsDown = New System.Windows.Forms.Button()
        Me.btnGuestStarsRemove = New System.Windows.Forms.Button()
        Me.btnRatingsRemove = New System.Windows.Forms.Button()
        Me.lblDisplaySeasonEpisode = New System.Windows.Forms.Label()
        Me.tpOther = New System.Windows.Forms.TabPage()
        Me.tblOther = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMediaStub = New System.Windows.Forms.GroupBox()
        Me.tblMediaStub = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMediaStubTitle = New System.Windows.Forms.Label()
        Me.txtMediaStubMessage = New System.Windows.Forms.TextBox()
        Me.lblMediaStubMessage = New System.Windows.Forms.Label()
        Me.txtMediaStubTitle = New System.Windows.Forms.TextBox()
        Me.gbSubtitles = New System.Windows.Forms.GroupBox()
        Me.tblSubtitles = New System.Windows.Forms.TableLayoutPanel()
        Me.txtSubtitlesPreview = New System.Windows.Forms.TextBox()
        Me.btnSetSubtitleLocal = New System.Windows.Forms.Button()
        Me.btnRemoveSubtitle = New System.Windows.Forms.Button()
        Me.btnSetSubtitleDL = New System.Windows.Forms.Button()
        Me.lblSubtitlesPreview = New System.Windows.Forms.Label()
        Me.btnSetSubtitleScrape = New System.Windows.Forms.Button()
        Me.lvSubtitles = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
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
        Me.pnlFanart = New System.Windows.Forms.Panel()
        Me.tblFanart = New System.Windows.Forms.TableLayoutPanel()
        Me.pbFanart = New System.Windows.Forms.PictureBox()
        Me.lblFanart = New System.Windows.Forms.Label()
        Me.btnSetFanartLocal = New System.Windows.Forms.Button()
        Me.btnSetFanartScrape = New System.Windows.Forms.Button()
        Me.lblFanartSize = New System.Windows.Forms.Label()
        Me.btnSetFanartDL = New System.Windows.Forms.Button()
        Me.btnRemoveFanart = New System.Windows.Forms.Button()
        Me.tpFrameExtraction = New System.Windows.Forms.TabPage()
        Me.tblFrameExtraction = New System.Windows.Forms.TableLayoutPanel()
        Me.pbFrame = New System.Windows.Forms.PictureBox()
        Me.btnFrameSaveAsPoster = New System.Windows.Forms.Button()
        Me.tbFrame = New System.Windows.Forms.TrackBar()
        Me.btnFrameSaveAsFanart = New System.Windows.Forms.Button()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.btnFrameLoadVideo = New System.Windows.Forms.Button()
        Me.tpMetaData = New System.Windows.Forms.TabPage()
        Me.pnlFileInfo = New System.Windows.Forms.Panel()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnChange = New System.Windows.Forms.Button()
        Me.btnRescrape = New System.Windows.Forms.Button()
        Me.chkLocked = New System.Windows.Forms.CheckBox()
        Me.chkMarked = New System.Windows.Forms.CheckBox()
        Me.tmrDelay = New System.Windows.Forms.Timer(Me.components)
        Me.ofdLocalFiles = New System.Windows.Forms.OpenFileDialog()
        Me.pnlTop.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.tcEdit.SuspendLayout()
        Me.tpDetails.SuspendLayout()
        Me.tblDetails.SuspendLayout()
        CType(Me.dgvCredits, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDirectors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpOther.SuspendLayout()
        Me.tblOther.SuspendLayout()
        Me.gbMediaStub.SuspendLayout()
        Me.tblMediaStub.SuspendLayout()
        Me.gbSubtitles.SuspendLayout()
        Me.tblSubtitles.SuspendLayout()
        Me.tpImages.SuspendLayout()
        Me.tblImages.SuspendLayout()
        Me.pnlPoster.SuspendLayout()
        Me.tblPoster.SuspendLayout()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFanart.SuspendLayout()
        Me.tblFanart.SuspendLayout()
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpFrameExtraction.SuspendLayout()
        Me.tblFrameExtraction.SuspendLayout()
        CType(Me.pbFrame, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbFrame, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpMetaData.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.AutoSize = True
        Me.pnlTop.BackColor = System.Drawing.Color.LightSteelBlue
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.TableLayoutPanel1)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1104, 56)
        Me.pnlTop.TabIndex = 2
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.pbTopLogo, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTopDetails, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTopTitle, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1102, 54)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'pbTopLogo
        '
        Me.pbTopLogo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pbTopLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbTopLogo.ErrorImage = Nothing
        Me.pbTopLogo.Image = CType(resources.GetObject("pbTopLogo.Image"), System.Drawing.Image)
        Me.pbTopLogo.InitialImage = Nothing
        Me.pbTopLogo.Location = New System.Drawing.Point(3, 3)
        Me.pbTopLogo.Name = "pbTopLogo"
        Me.TableLayoutPanel1.SetRowSpan(Me.pbTopLogo, 2)
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
        Me.lblTopDetails.Size = New System.Drawing.Size(214, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Edit the details for the selected episode."
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
        Me.lblTopTitle.Size = New System.Drawing.Size(155, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Edit Episode"
        '
        'StatusStrip
        '
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(18, 18)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslFilename, Me.tsslSpring, Me.tsslStatus, Me.tspbStatus})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 769)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1104, 22)
        Me.StatusStrip.TabIndex = 79
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
        Me.pnlMain.Size = New System.Drawing.Size(1104, 684)
        Me.pnlMain.TabIndex = 80
        '
        'tcEdit
        '
        Me.tcEdit.Controls.Add(Me.tpDetails)
        Me.tcEdit.Controls.Add(Me.tpOther)
        Me.tcEdit.Controls.Add(Me.tpImages)
        Me.tcEdit.Controls.Add(Me.tpFrameExtraction)
        Me.tcEdit.Controls.Add(Me.tpMetaData)
        Me.tcEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tcEdit.Location = New System.Drawing.Point(0, 0)
        Me.tcEdit.Name = "tcEdit"
        Me.tcEdit.SelectedIndex = 0
        Me.tcEdit.Size = New System.Drawing.Size(1104, 684)
        Me.tcEdit.TabIndex = 0
        '
        'tpDetails
        '
        Me.tpDetails.Controls.Add(Me.tblDetails)
        Me.tpDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpDetails.Name = "tpDetails"
        Me.tpDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDetails.Size = New System.Drawing.Size(1096, 658)
        Me.tpDetails.TabIndex = 0
        Me.tpDetails.Text = "Details"
        Me.tpDetails.UseVisualStyleBackColor = True
        '
        'tblDetails
        '
        Me.tblDetails.AutoScroll = True
        Me.tblDetails.AutoSize = True
        Me.tblDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblDetails.ColumnCount = 17
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.Controls.Add(Me.btnManual, 15, 1)
        Me.tblDetails.Controls.Add(Me.cbVideoSource, 1, 10)
        Me.tblDetails.Controls.Add(Me.lblTitle, 0, 0)
        Me.tblDetails.Controls.Add(Me.txtTitle, 0, 1)
        Me.tblDetails.Controls.Add(Me.lblOriginalTitle, 0, 2)
        Me.tblDetails.Controls.Add(Me.txtOriginalTitle, 0, 3)
        Me.tblDetails.Controls.Add(Me.lvActors, 3, 15)
        Me.tblDetails.Controls.Add(Me.lblActors, 3, 14)
        Me.tblDetails.Controls.Add(Me.lblPlot, 3, 0)
        Me.tblDetails.Controls.Add(Me.lblCredits, 0, 14)
        Me.tblDetails.Controls.Add(Me.txtPlot, 3, 1)
        Me.tblDetails.Controls.Add(Me.dtpAired, 2, 5)
        Me.tblDetails.Controls.Add(Me.txtRuntime, 0, 10)
        Me.tblDetails.Controls.Add(Me.lblVideoSource, 1, 9)
        Me.tblDetails.Controls.Add(Me.dgvCredits, 0, 15)
        Me.tblDetails.Controls.Add(Me.dgvDirectors, 0, 17)
        Me.tblDetails.Controls.Add(Me.lblDirectors, 0, 16)
        Me.tblDetails.Controls.Add(Me.lblUserRating, 0, 19)
        Me.tblDetails.Controls.Add(Me.txtUserRating, 0, 20)
        Me.tblDetails.Controls.Add(Me.chkWatched, 0, 21)
        Me.tblDetails.Controls.Add(Me.dtpLastPlayed, 0, 22)
        Me.tblDetails.Controls.Add(Me.lblRatings, 3, 19)
        Me.tblDetails.Controls.Add(Me.lvRatings, 3, 20)
        Me.tblDetails.Controls.Add(Me.btnRatingsAdd, 3, 22)
        Me.tblDetails.Controls.Add(Me.btnRatingsEdit, 4, 22)
        Me.tblDetails.Controls.Add(Me.lblAired, 2, 4)
        Me.tblDetails.Controls.Add(Me.lblSeason, 0, 4)
        Me.tblDetails.Controls.Add(Me.lblEpisode, 1, 4)
        Me.tblDetails.Controls.Add(Me.txtSeason, 0, 5)
        Me.tblDetails.Controls.Add(Me.txtEpisode, 1, 5)
        Me.tblDetails.Controls.Add(Me.lblRuntime, 0, 9)
        Me.tblDetails.Controls.Add(Me.lblDisplaySeason, 0, 7)
        Me.tblDetails.Controls.Add(Me.txtDisplaySeason, 0, 8)
        Me.tblDetails.Controls.Add(Me.lblDisplayEpisode, 1, 7)
        Me.tblDetails.Controls.Add(Me.txtDisplayEpisode, 1, 8)
        Me.tblDetails.Controls.Add(Me.btnActorsAdd, 3, 18)
        Me.tblDetails.Controls.Add(Me.btnActorsEdit, 4, 18)
        Me.tblDetails.Controls.Add(Me.btnActorsUp, 6, 18)
        Me.tblDetails.Controls.Add(Me.btnActorsDown, 7, 18)
        Me.tblDetails.Controls.Add(Me.btnActorsRemove, 9, 18)
        Me.tblDetails.Controls.Add(Me.lblGuestStars, 10, 14)
        Me.tblDetails.Controls.Add(Me.lvGuestStars, 10, 15)
        Me.tblDetails.Controls.Add(Me.btnGuestStarsAdd, 10, 18)
        Me.tblDetails.Controls.Add(Me.btnGuestStarsEdit, 11, 18)
        Me.tblDetails.Controls.Add(Me.btnGuestStarsUp, 13, 18)
        Me.tblDetails.Controls.Add(Me.btnGuestStarsDown, 14, 18)
        Me.tblDetails.Controls.Add(Me.btnGuestStarsRemove, 16, 18)
        Me.tblDetails.Controls.Add(Me.btnRatingsRemove, 9, 22)
        Me.tblDetails.Controls.Add(Me.lblDisplaySeasonEpisode, 0, 6)
        Me.tblDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblDetails.Location = New System.Drawing.Point(3, 3)
        Me.tblDetails.Name = "tblDetails"
        Me.tblDetails.RowCount = 24
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
        Me.tblDetails.Size = New System.Drawing.Size(1090, 652)
        Me.tblDetails.TabIndex = 78
        '
        'btnManual
        '
        Me.tblDetails.SetColumnSpan(Me.btnManual, 2)
        Me.btnManual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnManual.Enabled = False
        Me.btnManual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnManual.Location = New System.Drawing.Point(930, 16)
        Me.btnManual.Name = "btnManual"
        Me.btnManual.Size = New System.Drawing.Size(157, 23)
        Me.btnManual.TabIndex = 8
        Me.btnManual.Text = "Manual Edit"
        Me.btnManual.UseVisualStyleBackColor = True
        Me.btnManual.Visible = False
        '
        'cbVideoSource
        '
        Me.tblDetails.SetColumnSpan(Me.cbVideoSource, 2)
        Me.cbVideoSource.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbVideoSource.FormattingEnabled = True
        Me.cbVideoSource.Location = New System.Drawing.Point(69, 194)
        Me.cbVideoSource.Name = "cbVideoSource"
        Me.cbVideoSource.Size = New System.Drawing.Size(198, 21)
        Me.cbVideoSource.TabIndex = 6
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTitle.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblTitle, 3)
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
        Me.tblDetails.SetColumnSpan(Me.txtTitle, 3)
        Me.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(3, 16)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(264, 22)
        Me.txtTitle.TabIndex = 0
        '
        'lblOriginalTitle
        '
        Me.lblOriginalTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOriginalTitle.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblOriginalTitle, 3)
        Me.lblOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblOriginalTitle.Location = New System.Drawing.Point(3, 42)
        Me.lblOriginalTitle.Name = "lblOriginalTitle"
        Me.lblOriginalTitle.Size = New System.Drawing.Size(76, 13)
        Me.lblOriginalTitle.TabIndex = 2
        Me.lblOriginalTitle.Text = "Original Title:"
        '
        'txtOriginalTitle
        '
        Me.txtOriginalTitle.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtOriginalTitle, 3)
        Me.txtOriginalTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtOriginalTitle.Location = New System.Drawing.Point(3, 58)
        Me.txtOriginalTitle.Name = "txtOriginalTitle"
        Me.txtOriginalTitle.Size = New System.Drawing.Size(264, 22)
        Me.txtOriginalTitle.TabIndex = 1
        '
        'lvActors
        '
        Me.lvActors.BackColor = System.Drawing.SystemColors.Window
        Me.lvActors.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colActorsID, Me.colActorsName, Me.colActorsRole, Me.colActorsThumb})
        Me.tblDetails.SetColumnSpan(Me.lvActors, 7)
        Me.lvActors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvActors.FullRowSelect = True
        Me.lvActors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvActors.Location = New System.Drawing.Point(273, 235)
        Me.lvActors.Name = "lvActors"
        Me.tblDetails.SetRowSpan(Me.lvActors, 3)
        Me.lvActors.Size = New System.Drawing.Size(403, 200)
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
        Me.tblDetails.SetColumnSpan(Me.lblActors, 7)
        Me.lblActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblActors.Location = New System.Drawing.Point(273, 219)
        Me.lblActors.Name = "lblActors"
        Me.lblActors.Size = New System.Drawing.Size(42, 13)
        Me.lblActors.TabIndex = 29
        Me.lblActors.Text = "Actors:"
        '
        'lblPlot
        '
        Me.lblPlot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblPlot.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblPlot, 13)
        Me.lblPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPlot.Location = New System.Drawing.Point(273, 0)
        Me.lblPlot.Name = "lblPlot"
        Me.lblPlot.Size = New System.Drawing.Size(30, 13)
        Me.lblPlot.TabIndex = 27
        Me.lblPlot.Text = "Plot:"
        '
        'lblCredits
        '
        Me.lblCredits.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCredits.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblCredits, 3)
        Me.lblCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCredits.Location = New System.Drawing.Point(3, 219)
        Me.lblCredits.Name = "lblCredits"
        Me.lblCredits.Size = New System.Drawing.Size(46, 13)
        Me.lblCredits.TabIndex = 40
        Me.lblCredits.Text = "Credits:"
        '
        'txtPlot
        '
        Me.txtPlot.AcceptsReturn = True
        Me.txtPlot.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtPlot, 12)
        Me.txtPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtPlot.Location = New System.Drawing.Point(273, 16)
        Me.txtPlot.Multiline = True
        Me.txtPlot.Name = "txtPlot"
        Me.tblDetails.SetRowSpan(Me.txtPlot, 10)
        Me.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlot.Size = New System.Drawing.Size(651, 200)
        Me.txtPlot.TabIndex = 10
        '
        'dtpAired
        '
        Me.dtpAired.CustomFormat = "yyyy-dd-MM"
        Me.dtpAired.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpAired.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpAired.Location = New System.Drawing.Point(135, 99)
        Me.dtpAired.Name = "dtpAired"
        Me.dtpAired.Size = New System.Drawing.Size(132, 22)
        Me.dtpAired.TabIndex = 4
        '
        'txtRuntime
        '
        Me.txtRuntime.BackColor = System.Drawing.SystemColors.Window
        Me.txtRuntime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtRuntime.Location = New System.Drawing.Point(3, 194)
        Me.txtRuntime.Name = "txtRuntime"
        Me.txtRuntime.Size = New System.Drawing.Size(60, 22)
        Me.txtRuntime.TabIndex = 5
        '
        'lblVideoSource
        '
        Me.lblVideoSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblVideoSource.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblVideoSource, 2)
        Me.lblVideoSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoSource.Location = New System.Drawing.Point(69, 178)
        Me.lblVideoSource.Name = "lblVideoSource"
        Me.lblVideoSource.Size = New System.Drawing.Size(78, 13)
        Me.lblVideoSource.TabIndex = 47
        Me.lblVideoSource.Text = "Video Source:"
        '
        'dgvCredits
        '
        Me.dgvCredits.AllowUserToResizeColumns = False
        Me.dgvCredits.AllowUserToResizeRows = False
        Me.dgvCredits.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvCredits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvCredits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCredits.ColumnHeadersVisible = False
        Me.dgvCredits.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCreditsName})
        Me.tblDetails.SetColumnSpan(Me.dgvCredits, 3)
        Me.dgvCredits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCredits.Location = New System.Drawing.Point(3, 235)
        Me.dgvCredits.Name = "dgvCredits"
        Me.dgvCredits.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvCredits.Size = New System.Drawing.Size(264, 105)
        Me.dgvCredits.TabIndex = 18
        '
        'colCreditsName
        '
        Me.colCreditsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colCreditsName.HeaderText = "Name"
        Me.colCreditsName.Name = "colCreditsName"
        Me.colCreditsName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'dgvDirectors
        '
        Me.dgvDirectors.AllowUserToResizeColumns = False
        Me.dgvDirectors.AllowUserToResizeRows = False
        Me.dgvDirectors.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvDirectors.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvDirectors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDirectors.ColumnHeadersVisible = False
        Me.dgvDirectors.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDirectorsName})
        Me.tblDetails.SetColumnSpan(Me.dgvDirectors, 3)
        Me.dgvDirectors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDirectors.Location = New System.Drawing.Point(3, 359)
        Me.dgvDirectors.Name = "dgvDirectors"
        Me.dgvDirectors.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.tblDetails.SetRowSpan(Me.dgvDirectors, 2)
        Me.dgvDirectors.Size = New System.Drawing.Size(264, 105)
        Me.dgvDirectors.TabIndex = 19
        '
        'colDirectorsName
        '
        Me.colDirectorsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDirectorsName.HeaderText = "Name"
        Me.colDirectorsName.Name = "colDirectorsName"
        Me.colDirectorsName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'lblDirectors
        '
        Me.lblDirectors.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDirectors.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblDirectors, 3)
        Me.lblDirectors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblDirectors.Location = New System.Drawing.Point(3, 343)
        Me.lblDirectors.Name = "lblDirectors"
        Me.lblDirectors.Size = New System.Drawing.Size(56, 13)
        Me.lblDirectors.TabIndex = 21
        Me.lblDirectors.Text = "Directors:"
        '
        'lblUserRating
        '
        Me.lblUserRating.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserRating.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblUserRating, 3)
        Me.lblUserRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblUserRating.Location = New System.Drawing.Point(3, 467)
        Me.lblUserRating.Name = "lblUserRating"
        Me.lblUserRating.Size = New System.Drawing.Size(70, 13)
        Me.lblUserRating.TabIndex = 19
        Me.lblUserRating.Text = "User Rating:"
        '
        'txtUserRating
        '
        Me.txtUserRating.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtUserRating, 3)
        Me.txtUserRating.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtUserRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtUserRating.Location = New System.Drawing.Point(3, 483)
        Me.txtUserRating.Name = "txtUserRating"
        Me.txtUserRating.Size = New System.Drawing.Size(264, 22)
        Me.txtUserRating.TabIndex = 27
        '
        'chkWatched
        '
        Me.chkWatched.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkWatched.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.chkWatched, 3)
        Me.chkWatched.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWatched.Location = New System.Drawing.Point(3, 572)
        Me.chkWatched.Name = "chkWatched"
        Me.chkWatched.Size = New System.Drawing.Size(72, 17)
        Me.chkWatched.TabIndex = 29
        Me.chkWatched.Text = "Watched"
        Me.chkWatched.UseVisualStyleBackColor = True
        '
        'dtpLastPlayed
        '
        Me.dtpLastPlayed.Checked = False
        Me.tblDetails.SetColumnSpan(Me.dtpLastPlayed, 3)
        Me.dtpLastPlayed.CustomFormat = "yyyy-dd-MM / HH:mm:ss"
        Me.dtpLastPlayed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpLastPlayed.Enabled = False
        Me.dtpLastPlayed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLastPlayed.Location = New System.Drawing.Point(3, 595)
        Me.dtpLastPlayed.Name = "dtpLastPlayed"
        Me.dtpLastPlayed.Size = New System.Drawing.Size(264, 22)
        Me.dtpLastPlayed.TabIndex = 30
        '
        'lblRatings
        '
        Me.lblRatings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRatings.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblRatings, 7)
        Me.lblRatings.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRatings.Location = New System.Drawing.Point(273, 467)
        Me.lblRatings.Name = "lblRatings"
        Me.lblRatings.Size = New System.Drawing.Size(49, 13)
        Me.lblRatings.TabIndex = 10
        Me.lblRatings.Text = "Ratings:"
        '
        'lvRatings
        '
        Me.lvRatings.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colRatingsName, Me.colRatingsValue, Me.colRatingsVotes, Me.colRatingsMax})
        Me.tblDetails.SetColumnSpan(Me.lvRatings, 7)
        Me.lvRatings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvRatings.FullRowSelect = True
        Me.lvRatings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvRatings.Location = New System.Drawing.Point(273, 483)
        Me.lvRatings.Name = "lvRatings"
        Me.tblDetails.SetRowSpan(Me.lvRatings, 2)
        Me.lvRatings.Size = New System.Drawing.Size(403, 106)
        Me.lvRatings.TabIndex = 31
        Me.lvRatings.UseCompatibleStateImageBehavior = False
        Me.lvRatings.View = System.Windows.Forms.View.Details
        '
        'colRatingsName
        '
        Me.colRatingsName.Text = "Name"
        Me.colRatingsName.Width = 150
        '
        'colRatingsValue
        '
        Me.colRatingsValue.Text = "Value"
        Me.colRatingsValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'colRatingsVotes
        '
        Me.colRatingsVotes.Text = "Votes"
        Me.colRatingsVotes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colRatingsVotes.Width = 80
        '
        'colRatingsMax
        '
        Me.colRatingsMax.Text = "Max"
        Me.colRatingsMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colRatingsMax.Width = 50
        '
        'btnRatingsAdd
        '
        Me.btnRatingsAdd.Image = CType(resources.GetObject("btnRatingsAdd.Image"), System.Drawing.Image)
        Me.btnRatingsAdd.Location = New System.Drawing.Point(273, 595)
        Me.btnRatingsAdd.Name = "btnRatingsAdd"
        Me.btnRatingsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnRatingsAdd.TabIndex = 32
        Me.btnRatingsAdd.UseVisualStyleBackColor = True
        '
        'btnRatingsEdit
        '
        Me.btnRatingsEdit.Image = CType(resources.GetObject("btnRatingsEdit.Image"), System.Drawing.Image)
        Me.btnRatingsEdit.Location = New System.Drawing.Point(302, 595)
        Me.btnRatingsEdit.Name = "btnRatingsEdit"
        Me.btnRatingsEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnRatingsEdit.TabIndex = 33
        Me.btnRatingsEdit.UseVisualStyleBackColor = True
        '
        'lblAired
        '
        Me.lblAired.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblAired.AutoSize = True
        Me.lblAired.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblAired.Location = New System.Drawing.Point(135, 83)
        Me.lblAired.Name = "lblAired"
        Me.lblAired.Size = New System.Drawing.Size(37, 13)
        Me.lblAired.TabIndex = 13
        Me.lblAired.Text = "Aired:"
        '
        'lblSeason
        '
        Me.lblSeason.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSeason.AutoSize = True
        Me.lblSeason.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSeason.Location = New System.Drawing.Point(3, 83)
        Me.lblSeason.Name = "lblSeason"
        Me.lblSeason.Size = New System.Drawing.Size(47, 13)
        Me.lblSeason.TabIndex = 13
        Me.lblSeason.Text = "Season:"
        '
        'lblEpisode
        '
        Me.lblEpisode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblEpisode.AutoSize = True
        Me.lblEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblEpisode.Location = New System.Drawing.Point(69, 83)
        Me.lblEpisode.Name = "lblEpisode"
        Me.lblEpisode.Size = New System.Drawing.Size(51, 13)
        Me.lblEpisode.TabIndex = 13
        Me.lblEpisode.Text = "Episode:"
        '
        'txtSeason
        '
        Me.txtSeason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSeason.Location = New System.Drawing.Point(3, 99)
        Me.txtSeason.Name = "txtSeason"
        Me.txtSeason.Size = New System.Drawing.Size(60, 22)
        Me.txtSeason.TabIndex = 50
        '
        'txtEpisode
        '
        Me.txtEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtEpisode.Location = New System.Drawing.Point(69, 99)
        Me.txtEpisode.Name = "txtEpisode"
        Me.txtEpisode.Size = New System.Drawing.Size(60, 22)
        Me.txtEpisode.TabIndex = 50
        '
        'lblRuntime
        '
        Me.lblRuntime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRuntime.AutoSize = True
        Me.lblRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRuntime.Location = New System.Drawing.Point(3, 178)
        Me.lblRuntime.Name = "lblRuntime"
        Me.lblRuntime.Size = New System.Drawing.Size(53, 13)
        Me.lblRuntime.TabIndex = 15
        Me.lblRuntime.Text = "Runtime:"
        '
        'lblDisplaySeason
        '
        Me.lblDisplaySeason.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDisplaySeason.AutoSize = True
        Me.lblDisplaySeason.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblDisplaySeason.Location = New System.Drawing.Point(3, 137)
        Me.lblDisplaySeason.Name = "lblDisplaySeason"
        Me.lblDisplaySeason.Size = New System.Drawing.Size(47, 13)
        Me.lblDisplaySeason.TabIndex = 13
        Me.lblDisplaySeason.Text = "Season:"
        '
        'txtDisplaySeason
        '
        Me.txtDisplaySeason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDisplaySeason.Location = New System.Drawing.Point(3, 153)
        Me.txtDisplaySeason.Name = "txtDisplaySeason"
        Me.txtDisplaySeason.Size = New System.Drawing.Size(60, 22)
        Me.txtDisplaySeason.TabIndex = 50
        '
        'lblDisplayEpisode
        '
        Me.lblDisplayEpisode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDisplayEpisode.AutoSize = True
        Me.lblDisplayEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblDisplayEpisode.Location = New System.Drawing.Point(69, 137)
        Me.lblDisplayEpisode.Name = "lblDisplayEpisode"
        Me.lblDisplayEpisode.Size = New System.Drawing.Size(51, 13)
        Me.lblDisplayEpisode.TabIndex = 13
        Me.lblDisplayEpisode.Text = "Episode:"
        '
        'txtDisplayEpisode
        '
        Me.txtDisplayEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDisplayEpisode.Location = New System.Drawing.Point(69, 153)
        Me.txtDisplayEpisode.Name = "txtDisplayEpisode"
        Me.txtDisplayEpisode.Size = New System.Drawing.Size(60, 22)
        Me.txtDisplayEpisode.TabIndex = 50
        '
        'btnActorsAdd
        '
        Me.btnActorsAdd.Image = CType(resources.GetObject("btnActorsAdd.Image"), System.Drawing.Image)
        Me.btnActorsAdd.Location = New System.Drawing.Point(273, 441)
        Me.btnActorsAdd.Name = "btnActorsAdd"
        Me.btnActorsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsAdd.TabIndex = 21
        Me.btnActorsAdd.UseVisualStyleBackColor = True
        '
        'btnActorsEdit
        '
        Me.btnActorsEdit.Image = CType(resources.GetObject("btnActorsEdit.Image"), System.Drawing.Image)
        Me.btnActorsEdit.Location = New System.Drawing.Point(302, 441)
        Me.btnActorsEdit.Name = "btnActorsEdit"
        Me.btnActorsEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsEdit.TabIndex = 22
        Me.btnActorsEdit.UseVisualStyleBackColor = True
        '
        'btnActorsUp
        '
        Me.btnActorsUp.Image = CType(resources.GetObject("btnActorsUp.Image"), System.Drawing.Image)
        Me.btnActorsUp.Location = New System.Drawing.Point(463, 441)
        Me.btnActorsUp.Name = "btnActorsUp"
        Me.btnActorsUp.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsUp.TabIndex = 23
        Me.btnActorsUp.UseVisualStyleBackColor = True
        '
        'btnActorsDown
        '
        Me.btnActorsDown.Image = CType(resources.GetObject("btnActorsDown.Image"), System.Drawing.Image)
        Me.btnActorsDown.Location = New System.Drawing.Point(492, 441)
        Me.btnActorsDown.Name = "btnActorsDown"
        Me.btnActorsDown.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsDown.TabIndex = 24
        Me.btnActorsDown.UseVisualStyleBackColor = True
        '
        'btnActorsRemove
        '
        Me.btnActorsRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnActorsRemove.Image = CType(resources.GetObject("btnActorsRemove.Image"), System.Drawing.Image)
        Me.btnActorsRemove.Location = New System.Drawing.Point(653, 441)
        Me.btnActorsRemove.Name = "btnActorsRemove"
        Me.btnActorsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsRemove.TabIndex = 25
        Me.btnActorsRemove.UseVisualStyleBackColor = True
        '
        'lblGuestStars
        '
        Me.lblGuestStars.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblGuestStars.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblGuestStars, 7)
        Me.lblGuestStars.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblGuestStars.Location = New System.Drawing.Point(682, 219)
        Me.lblGuestStars.Name = "lblGuestStars"
        Me.lblGuestStars.Size = New System.Drawing.Size(68, 13)
        Me.lblGuestStars.TabIndex = 29
        Me.lblGuestStars.Text = "Guest Stars:"
        '
        'lvGuestStars
        '
        Me.lvGuestStars.BackColor = System.Drawing.SystemColors.Window
        Me.lvGuestStars.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colGuestStarsID, Me.colGuestStarsName, Me.colGuestStarsRole, Me.colGuestStarsThumb})
        Me.tblDetails.SetColumnSpan(Me.lvGuestStars, 7)
        Me.lvGuestStars.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvGuestStars.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvGuestStars.FullRowSelect = True
        Me.lvGuestStars.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvGuestStars.Location = New System.Drawing.Point(682, 235)
        Me.lvGuestStars.Name = "lvGuestStars"
        Me.tblDetails.SetRowSpan(Me.lvGuestStars, 3)
        Me.lvGuestStars.Size = New System.Drawing.Size(405, 200)
        Me.lvGuestStars.TabIndex = 20
        Me.lvGuestStars.UseCompatibleStateImageBehavior = False
        Me.lvGuestStars.View = System.Windows.Forms.View.Details
        '
        'colGuestStarsID
        '
        Me.colGuestStarsID.Text = "ID"
        Me.colGuestStarsID.Width = 0
        '
        'colGuestStarsName
        '
        Me.colGuestStarsName.Text = "Name"
        Me.colGuestStarsName.Width = 130
        '
        'colGuestStarsRole
        '
        Me.colGuestStarsRole.Text = "Role"
        Me.colGuestStarsRole.Width = 130
        '
        'colGuestStarsThumb
        '
        Me.colGuestStarsThumb.Text = "Thumb"
        Me.colGuestStarsThumb.Width = 80
        '
        'btnGuestStarsAdd
        '
        Me.btnGuestStarsAdd.Image = CType(resources.GetObject("btnGuestStarsAdd.Image"), System.Drawing.Image)
        Me.btnGuestStarsAdd.Location = New System.Drawing.Point(682, 441)
        Me.btnGuestStarsAdd.Name = "btnGuestStarsAdd"
        Me.btnGuestStarsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnGuestStarsAdd.TabIndex = 21
        Me.btnGuestStarsAdd.UseVisualStyleBackColor = True
        '
        'btnGuestStarsEdit
        '
        Me.btnGuestStarsEdit.Image = CType(resources.GetObject("btnGuestStarsEdit.Image"), System.Drawing.Image)
        Me.btnGuestStarsEdit.Location = New System.Drawing.Point(711, 441)
        Me.btnGuestStarsEdit.Name = "btnGuestStarsEdit"
        Me.btnGuestStarsEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnGuestStarsEdit.TabIndex = 22
        Me.btnGuestStarsEdit.UseVisualStyleBackColor = True
        '
        'btnGuestStarsUp
        '
        Me.btnGuestStarsUp.Image = CType(resources.GetObject("btnGuestStarsUp.Image"), System.Drawing.Image)
        Me.btnGuestStarsUp.Location = New System.Drawing.Point(872, 441)
        Me.btnGuestStarsUp.Name = "btnGuestStarsUp"
        Me.btnGuestStarsUp.Size = New System.Drawing.Size(23, 23)
        Me.btnGuestStarsUp.TabIndex = 23
        Me.btnGuestStarsUp.UseVisualStyleBackColor = True
        '
        'btnGuestStarsDown
        '
        Me.btnGuestStarsDown.Image = CType(resources.GetObject("btnGuestStarsDown.Image"), System.Drawing.Image)
        Me.btnGuestStarsDown.Location = New System.Drawing.Point(901, 441)
        Me.btnGuestStarsDown.Name = "btnGuestStarsDown"
        Me.btnGuestStarsDown.Size = New System.Drawing.Size(23, 23)
        Me.btnGuestStarsDown.TabIndex = 24
        Me.btnGuestStarsDown.UseVisualStyleBackColor = True
        '
        'btnGuestStarsRemove
        '
        Me.btnGuestStarsRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGuestStarsRemove.Image = CType(resources.GetObject("btnGuestStarsRemove.Image"), System.Drawing.Image)
        Me.btnGuestStarsRemove.Location = New System.Drawing.Point(1064, 441)
        Me.btnGuestStarsRemove.Name = "btnGuestStarsRemove"
        Me.btnGuestStarsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnGuestStarsRemove.TabIndex = 25
        Me.btnGuestStarsRemove.UseVisualStyleBackColor = True
        '
        'btnRatingsRemove
        '
        Me.btnRatingsRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRatingsRemove.Image = CType(resources.GetObject("btnRatingsRemove.Image"), System.Drawing.Image)
        Me.btnRatingsRemove.Location = New System.Drawing.Point(653, 595)
        Me.btnRatingsRemove.Name = "btnRatingsRemove"
        Me.btnRatingsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnRatingsRemove.TabIndex = 34
        Me.btnRatingsRemove.UseVisualStyleBackColor = True
        '
        'lblDisplaySeasonEpisode
        '
        Me.lblDisplaySeasonEpisode.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblDisplaySeasonEpisode.AutoSize = True
        Me.tblDetails.SetColumnSpan(Me.lblDisplaySeasonEpisode, 2)
        Me.lblDisplaySeasonEpisode.Location = New System.Drawing.Point(44, 124)
        Me.lblDisplaySeasonEpisode.Name = "lblDisplaySeasonEpisode"
        Me.lblDisplaySeasonEpisode.Size = New System.Drawing.Size(44, 13)
        Me.lblDisplaySeasonEpisode.TabIndex = 51
        Me.lblDisplaySeasonEpisode.Text = "Display"
        '
        'tpOther
        '
        Me.tpOther.Controls.Add(Me.tblOther)
        Me.tpOther.Location = New System.Drawing.Point(4, 22)
        Me.tpOther.Name = "tpOther"
        Me.tpOther.Size = New System.Drawing.Size(1096, 658)
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
        Me.tblOther.Controls.Add(Me.gbMediaStub, 0, 0)
        Me.tblOther.Controls.Add(Me.gbSubtitles, 0, 1)
        Me.tblOther.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblOther.Location = New System.Drawing.Point(0, 0)
        Me.tblOther.Name = "tblOther"
        Me.tblOther.RowCount = 2
        Me.tblOther.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblOther.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblOther.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblOther.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblOther.Size = New System.Drawing.Size(1096, 658)
        Me.tblOther.TabIndex = 0
        '
        'gbMediaStub
        '
        Me.gbMediaStub.AutoSize = True
        Me.gbMediaStub.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbMediaStub.Controls.Add(Me.tblMediaStub)
        Me.gbMediaStub.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMediaStub.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMediaStub.Location = New System.Drawing.Point(3, 3)
        Me.gbMediaStub.Name = "gbMediaStub"
        Me.gbMediaStub.Size = New System.Drawing.Size(1093, 117)
        Me.gbMediaStub.TabIndex = 0
        Me.gbMediaStub.TabStop = False
        Me.gbMediaStub.Text = "MediaStub"
        '
        'tblMediaStub
        '
        Me.tblMediaStub.AutoSize = True
        Me.tblMediaStub.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblMediaStub.ColumnCount = 1
        Me.tblMediaStub.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaStub.Controls.Add(Me.lblMediaStubTitle, 0, 0)
        Me.tblMediaStub.Controls.Add(Me.txtMediaStubMessage, 0, 3)
        Me.tblMediaStub.Controls.Add(Me.lblMediaStubMessage, 0, 2)
        Me.tblMediaStub.Controls.Add(Me.txtMediaStubTitle, 0, 1)
        Me.tblMediaStub.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMediaStub.Location = New System.Drawing.Point(3, 18)
        Me.tblMediaStub.Name = "tblMediaStub"
        Me.tblMediaStub.RowCount = 4
        Me.tblMediaStub.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaStub.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaStub.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaStub.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaStub.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaStub.Size = New System.Drawing.Size(1087, 96)
        Me.tblMediaStub.TabIndex = 5
        '
        'lblMediaStubTitle
        '
        Me.lblMediaStubTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMediaStubTitle.AutoSize = True
        Me.lblMediaStubTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMediaStubTitle.Location = New System.Drawing.Point(3, 3)
        Me.lblMediaStubTitle.Name = "lblMediaStubTitle"
        Me.lblMediaStubTitle.Size = New System.Drawing.Size(31, 13)
        Me.lblMediaStubTitle.TabIndex = 2
        Me.lblMediaStubTitle.Text = "Title:"
        '
        'txtMediaStubMessage
        '
        Me.txtMediaStubMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMediaStubMessage.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMediaStubMessage.Location = New System.Drawing.Point(3, 71)
        Me.txtMediaStubMessage.Name = "txtMediaStubMessage"
        Me.txtMediaStubMessage.Size = New System.Drawing.Size(1081, 22)
        Me.txtMediaStubMessage.TabIndex = 1
        '
        'lblMediaStubMessage
        '
        Me.lblMediaStubMessage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMediaStubMessage.AutoSize = True
        Me.lblMediaStubMessage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMediaStubMessage.Location = New System.Drawing.Point(3, 51)
        Me.lblMediaStubMessage.Name = "lblMediaStubMessage"
        Me.lblMediaStubMessage.Size = New System.Drawing.Size(55, 13)
        Me.lblMediaStubMessage.TabIndex = 3
        Me.lblMediaStubMessage.Text = "Message:"
        '
        'txtMediaStubTitle
        '
        Me.txtMediaStubTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMediaStubTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMediaStubTitle.Location = New System.Drawing.Point(3, 23)
        Me.txtMediaStubTitle.Name = "txtMediaStubTitle"
        Me.txtMediaStubTitle.Size = New System.Drawing.Size(1081, 22)
        Me.txtMediaStubTitle.TabIndex = 0
        '
        'gbSubtitles
        '
        Me.gbSubtitles.AutoSize = True
        Me.gbSubtitles.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbSubtitles.Controls.Add(Me.tblSubtitles)
        Me.gbSubtitles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSubtitles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbSubtitles.Location = New System.Drawing.Point(3, 126)
        Me.gbSubtitles.Name = "gbSubtitles"
        Me.gbSubtitles.Size = New System.Drawing.Size(1093, 529)
        Me.gbSubtitles.TabIndex = 3
        Me.gbSubtitles.TabStop = False
        Me.gbSubtitles.Text = "Subtitles"
        '
        'tblSubtitles
        '
        Me.tblSubtitles.AutoSize = True
        Me.tblSubtitles.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblSubtitles.ColumnCount = 5
        Me.tblSubtitles.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSubtitles.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSubtitles.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSubtitles.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSubtitles.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSubtitles.Controls.Add(Me.txtSubtitlesPreview, 0, 3)
        Me.tblSubtitles.Controls.Add(Me.btnSetSubtitleLocal, 2, 1)
        Me.tblSubtitles.Controls.Add(Me.btnRemoveSubtitle, 4, 1)
        Me.tblSubtitles.Controls.Add(Me.btnSetSubtitleDL, 1, 1)
        Me.tblSubtitles.Controls.Add(Me.lblSubtitlesPreview, 0, 2)
        Me.tblSubtitles.Controls.Add(Me.btnSetSubtitleScrape, 0, 1)
        Me.tblSubtitles.Controls.Add(Me.lvSubtitles, 0, 0)
        Me.tblSubtitles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSubtitles.Location = New System.Drawing.Point(3, 18)
        Me.tblSubtitles.Name = "tblSubtitles"
        Me.tblSubtitles.RowCount = 4
        Me.tblSubtitles.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSubtitles.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSubtitles.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSubtitles.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSubtitles.Size = New System.Drawing.Size(1087, 508)
        Me.tblSubtitles.TabIndex = 0
        '
        'txtSubtitlesPreview
        '
        Me.tblSubtitles.SetColumnSpan(Me.txtSubtitlesPreview, 5)
        Me.txtSubtitlesPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSubtitlesPreview.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSubtitlesPreview.Location = New System.Drawing.Point(3, 178)
        Me.txtSubtitlesPreview.Multiline = True
        Me.txtSubtitlesPreview.Name = "txtSubtitlesPreview"
        Me.txtSubtitlesPreview.ReadOnly = True
        Me.txtSubtitlesPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSubtitlesPreview.Size = New System.Drawing.Size(1081, 327)
        Me.txtSubtitlesPreview.TabIndex = 5
        '
        'btnSetSubtitleLocal
        '
        Me.btnSetSubtitleLocal.Enabled = False
        Me.btnSetSubtitleLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetSubtitleLocal.Image = CType(resources.GetObject("btnSetSubtitleLocal.Image"), System.Drawing.Image)
        Me.btnSetSubtitleLocal.Location = New System.Drawing.Point(61, 129)
        Me.btnSetSubtitleLocal.Name = "btnSetSubtitleLocal"
        Me.btnSetSubtitleLocal.Size = New System.Drawing.Size(23, 23)
        Me.btnSetSubtitleLocal.TabIndex = 3
        Me.btnSetSubtitleLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSubtitleLocal.UseVisualStyleBackColor = True
        '
        'btnRemoveSubtitle
        '
        Me.btnRemoveSubtitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveSubtitle.Image = CType(resources.GetObject("btnRemoveSubtitle.Image"), System.Drawing.Image)
        Me.btnRemoveSubtitle.Location = New System.Drawing.Point(1061, 129)
        Me.btnRemoveSubtitle.Name = "btnRemoveSubtitle"
        Me.btnRemoveSubtitle.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveSubtitle.TabIndex = 4
        Me.btnRemoveSubtitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveSubtitle.UseVisualStyleBackColor = True
        '
        'btnSetSubtitleDL
        '
        Me.btnSetSubtitleDL.Enabled = False
        Me.btnSetSubtitleDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetSubtitleDL.Image = CType(resources.GetObject("btnSetSubtitleDL.Image"), System.Drawing.Image)
        Me.btnSetSubtitleDL.Location = New System.Drawing.Point(32, 129)
        Me.btnSetSubtitleDL.Name = "btnSetSubtitleDL"
        Me.btnSetSubtitleDL.Size = New System.Drawing.Size(23, 23)
        Me.btnSetSubtitleDL.TabIndex = 2
        Me.btnSetSubtitleDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSubtitleDL.UseVisualStyleBackColor = True
        '
        'lblSubtitlesPreview
        '
        Me.lblSubtitlesPreview.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSubtitlesPreview.AutoSize = True
        Me.tblSubtitles.SetColumnSpan(Me.lblSubtitlesPreview, 5)
        Me.lblSubtitlesPreview.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSubtitlesPreview.Location = New System.Drawing.Point(3, 158)
        Me.lblSubtitlesPreview.Name = "lblSubtitlesPreview"
        Me.lblSubtitlesPreview.Size = New System.Drawing.Size(49, 13)
        Me.lblSubtitlesPreview.TabIndex = 38
        Me.lblSubtitlesPreview.Text = "Preview:"
        '
        'btnSetSubtitleScrape
        '
        Me.btnSetSubtitleScrape.Enabled = False
        Me.btnSetSubtitleScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetSubtitleScrape.Image = CType(resources.GetObject("btnSetSubtitleScrape.Image"), System.Drawing.Image)
        Me.btnSetSubtitleScrape.Location = New System.Drawing.Point(3, 129)
        Me.btnSetSubtitleScrape.Name = "btnSetSubtitleScrape"
        Me.btnSetSubtitleScrape.Size = New System.Drawing.Size(23, 23)
        Me.btnSetSubtitleScrape.TabIndex = 1
        Me.btnSetSubtitleScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSubtitleScrape.UseVisualStyleBackColor = True
        '
        'lvSubtitles
        '
        Me.lvSubtitles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.tblSubtitles.SetColumnSpan(Me.lvSubtitles, 5)
        Me.lvSubtitles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvSubtitles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvSubtitles.FullRowSelect = True
        ListViewGroup2.Header = "Local Subtitles"
        ListViewGroup2.Name = "LocalSubtitles"
        Me.lvSubtitles.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup2})
        Me.lvSubtitles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        ListViewItem2.Group = ListViewGroup2
        Me.lvSubtitles.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem2})
        Me.lvSubtitles.Location = New System.Drawing.Point(3, 3)
        Me.lvSubtitles.MultiSelect = False
        Me.lvSubtitles.Name = "lvSubtitles"
        Me.lvSubtitles.Size = New System.Drawing.Size(1081, 120)
        Me.lvSubtitles.TabIndex = 0
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
        'tpImages
        '
        Me.tpImages.Controls.Add(Me.tblImages)
        Me.tpImages.Location = New System.Drawing.Point(4, 22)
        Me.tpImages.Name = "tpImages"
        Me.tpImages.Padding = New System.Windows.Forms.Padding(3)
        Me.tpImages.Size = New System.Drawing.Size(1096, 658)
        Me.tpImages.TabIndex = 16
        Me.tpImages.Text = "Images"
        Me.tpImages.UseVisualStyleBackColor = True
        '
        'tblImages
        '
        Me.tblImages.AutoScroll = True
        Me.tblImages.AutoSize = True
        Me.tblImages.ColumnCount = 2
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImages.Controls.Add(Me.pnlPoster, 0, 0)
        Me.tblImages.Controls.Add(Me.pnlFanart, 1, 0)
        Me.tblImages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImages.Location = New System.Drawing.Point(3, 3)
        Me.tblImages.Name = "tblImages"
        Me.tblImages.RowCount = 1
        Me.tblImages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImages.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 652.0!))
        Me.tblImages.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 652.0!))
        Me.tblImages.Size = New System.Drawing.Size(1090, 652)
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
        'tpFrameExtraction
        '
        Me.tpFrameExtraction.Controls.Add(Me.tblFrameExtraction)
        Me.tpFrameExtraction.Location = New System.Drawing.Point(4, 22)
        Me.tpFrameExtraction.Name = "tpFrameExtraction"
        Me.tpFrameExtraction.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFrameExtraction.Size = New System.Drawing.Size(1096, 658)
        Me.tpFrameExtraction.TabIndex = 3
        Me.tpFrameExtraction.Text = "Frame Extraction"
        Me.tpFrameExtraction.UseVisualStyleBackColor = True
        '
        'tblFrameExtraction
        '
        Me.tblFrameExtraction.AutoSize = True
        Me.tblFrameExtraction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblFrameExtraction.ColumnCount = 3
        Me.tblFrameExtraction.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblFrameExtraction.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFrameExtraction.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFrameExtraction.Controls.Add(Me.pbFrame, 0, 0)
        Me.tblFrameExtraction.Controls.Add(Me.btnFrameSaveAsPoster, 2, 3)
        Me.tblFrameExtraction.Controls.Add(Me.tbFrame, 0, 4)
        Me.tblFrameExtraction.Controls.Add(Me.btnFrameSaveAsFanart, 2, 2)
        Me.tblFrameExtraction.Controls.Add(Me.lblTime, 1, 4)
        Me.tblFrameExtraction.Controls.Add(Me.btnFrameLoadVideo, 2, 0)
        Me.tblFrameExtraction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFrameExtraction.Location = New System.Drawing.Point(3, 3)
        Me.tblFrameExtraction.Name = "tblFrameExtraction"
        Me.tblFrameExtraction.RowCount = 5
        Me.tblFrameExtraction.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFrameExtraction.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblFrameExtraction.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFrameExtraction.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFrameExtraction.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFrameExtraction.Size = New System.Drawing.Size(1090, 652)
        Me.tblFrameExtraction.TabIndex = 28
        '
        'pbFrame
        '
        Me.pbFrame.BackColor = System.Drawing.Color.DimGray
        Me.pbFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tblFrameExtraction.SetColumnSpan(Me.pbFrame, 2)
        Me.pbFrame.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbFrame.Location = New System.Drawing.Point(3, 3)
        Me.pbFrame.Name = "pbFrame"
        Me.tblFrameExtraction.SetRowSpan(Me.pbFrame, 4)
        Me.pbFrame.Size = New System.Drawing.Size(982, 613)
        Me.pbFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbFrame.TabIndex = 25
        Me.pbFrame.TabStop = False
        '
        'btnFrameSaveAsPoster
        '
        Me.btnFrameSaveAsPoster.Enabled = False
        Me.btnFrameSaveAsPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFrameSaveAsPoster.Image = CType(resources.GetObject("btnFrameSaveAsPoster.Image"), System.Drawing.Image)
        Me.btnFrameSaveAsPoster.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFrameSaveAsPoster.Location = New System.Drawing.Point(991, 533)
        Me.btnFrameSaveAsPoster.Name = "btnFrameSaveAsPoster"
        Me.btnFrameSaveAsPoster.Size = New System.Drawing.Size(96, 83)
        Me.btnFrameSaveAsPoster.TabIndex = 4
        Me.btnFrameSaveAsPoster.Text = "Save as Poster"
        Me.btnFrameSaveAsPoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnFrameSaveAsPoster.UseVisualStyleBackColor = True
        '
        'tbFrame
        '
        Me.tbFrame.AutoSize = False
        Me.tbFrame.BackColor = System.Drawing.Color.White
        Me.tbFrame.Cursor = System.Windows.Forms.Cursors.Default
        Me.tbFrame.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbFrame.Enabled = False
        Me.tbFrame.Location = New System.Drawing.Point(3, 622)
        Me.tbFrame.Name = "tbFrame"
        Me.tbFrame.Size = New System.Drawing.Size(917, 27)
        Me.tbFrame.TabIndex = 1
        Me.tbFrame.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'btnFrameSaveAsFanart
        '
        Me.btnFrameSaveAsFanart.Enabled = False
        Me.btnFrameSaveAsFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFrameSaveAsFanart.Image = CType(resources.GetObject("btnFrameSaveAsFanart.Image"), System.Drawing.Image)
        Me.btnFrameSaveAsFanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFrameSaveAsFanart.Location = New System.Drawing.Point(991, 444)
        Me.btnFrameSaveAsFanart.Name = "btnFrameSaveAsFanart"
        Me.btnFrameSaveAsFanart.Size = New System.Drawing.Size(96, 83)
        Me.btnFrameSaveAsFanart.TabIndex = 2
        Me.btnFrameSaveAsFanart.Text = "Save as Fanart"
        Me.btnFrameSaveAsFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnFrameSaveAsFanart.UseVisualStyleBackColor = True
        '
        'lblTime
        '
        Me.lblTime.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTime.Location = New System.Drawing.Point(926, 624)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(59, 23)
        Me.lblTime.TabIndex = 24
        Me.lblTime.Text = "00:00:00"
        Me.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnFrameLoadVideo
        '
        Me.btnFrameLoadVideo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFrameLoadVideo.Image = CType(resources.GetObject("btnFrameLoadVideo.Image"), System.Drawing.Image)
        Me.btnFrameLoadVideo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFrameLoadVideo.Location = New System.Drawing.Point(991, 3)
        Me.btnFrameLoadVideo.Name = "btnFrameLoadVideo"
        Me.btnFrameLoadVideo.Size = New System.Drawing.Size(96, 83)
        Me.btnFrameLoadVideo.TabIndex = 0
        Me.btnFrameLoadVideo.Text = "Load Video"
        Me.btnFrameLoadVideo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnFrameLoadVideo.UseVisualStyleBackColor = True
        '
        'tpMetaData
        '
        Me.tpMetaData.Controls.Add(Me.pnlFileInfo)
        Me.tpMetaData.Location = New System.Drawing.Point(4, 22)
        Me.tpMetaData.Name = "tpMetaData"
        Me.tpMetaData.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMetaData.Size = New System.Drawing.Size(1096, 658)
        Me.tpMetaData.TabIndex = 5
        Me.tpMetaData.Text = "Meta Data"
        Me.tpMetaData.UseVisualStyleBackColor = True
        '
        'pnlFileInfo
        '
        Me.pnlFileInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFileInfo.Location = New System.Drawing.Point(3, 3)
        Me.pnlFileInfo.Name = "pnlFileInfo"
        Me.pnlFileInfo.Size = New System.Drawing.Size(1090, 652)
        Me.pnlFileInfo.TabIndex = 0
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 740)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1104, 29)
        Me.pnlBottom.TabIndex = 81
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
        Me.tblBottom.Controls.Add(Me.btnOK, 6, 0)
        Me.tblBottom.Controls.Add(Me.btnCancel, 7, 0)
        Me.tblBottom.Controls.Add(Me.btnChange, 4, 0)
        Me.tblBottom.Controls.Add(Me.btnRescrape, 3, 0)
        Me.tblBottom.Controls.Add(Me.chkLocked, 0, 0)
        Me.tblBottom.Controls.Add(Me.chkMarked, 1, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.Size = New System.Drawing.Size(1104, 29)
        Me.tblBottom.TabIndex = 78
        '
        'btnOK
        '
        Me.btnOK.AutoSize = True
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnOK.Location = New System.Drawing.Point(949, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(70, 23)
        Me.btnOK.TabIndex = 9
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(1025, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(76, 23)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        '
        'btnChange
        '
        Me.btnChange.AutoSize = True
        Me.btnChange.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnChange.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnChange.Image = CType(resources.GetObject("btnChange.Image"), System.Drawing.Image)
        Me.btnChange.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnChange.Location = New System.Drawing.Point(541, 3)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(107, 23)
        Me.btnChange.TabIndex = 8
        Me.btnChange.Text = "Change Movie"
        Me.btnChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnChange.UseVisualStyleBackColor = True
        '
        'btnRescrape
        '
        Me.btnRescrape.AutoSize = True
        Me.btnRescrape.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRescrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRescrape.Image = CType(resources.GetObject("btnRescrape.Image"), System.Drawing.Image)
        Me.btnRescrape.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRescrape.Location = New System.Drawing.Point(437, 3)
        Me.btnRescrape.Name = "btnRescrape"
        Me.btnRescrape.Size = New System.Drawing.Size(98, 23)
        Me.btnRescrape.TabIndex = 7
        Me.btnRescrape.Text = "Re-scrape"
        Me.btnRescrape.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRescrape.UseVisualStyleBackColor = True
        '
        'chkLocked
        '
        Me.chkLocked.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkLocked.AutoSize = True
        Me.chkLocked.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLocked.Location = New System.Drawing.Point(3, 9)
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
        Me.chkMarked.Location = New System.Drawing.Point(71, 9)
        Me.chkMarked.Name = "chkMarked"
        Me.chkMarked.Size = New System.Drawing.Size(65, 17)
        Me.chkMarked.TabIndex = 1
        Me.chkMarked.Text = "Marked"
        Me.chkMarked.UseVisualStyleBackColor = True
        '
        'tmrDelay
        '
        Me.tmrDelay.Interval = 250
        '
        'dlgEditTVEpisode
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
        Me.Name = "dlgEditTVEpisode"
        Me.Text = "Edit Episode"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.tcEdit.ResumeLayout(False)
        Me.tpDetails.ResumeLayout(False)
        Me.tpDetails.PerformLayout()
        Me.tblDetails.ResumeLayout(False)
        Me.tblDetails.PerformLayout()
        CType(Me.dgvCredits, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDirectors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpOther.ResumeLayout(False)
        Me.tpOther.PerformLayout()
        Me.tblOther.ResumeLayout(False)
        Me.tblOther.PerformLayout()
        Me.gbMediaStub.ResumeLayout(False)
        Me.gbMediaStub.PerformLayout()
        Me.tblMediaStub.ResumeLayout(False)
        Me.tblMediaStub.PerformLayout()
        Me.gbSubtitles.ResumeLayout(False)
        Me.gbSubtitles.PerformLayout()
        Me.tblSubtitles.ResumeLayout(False)
        Me.tblSubtitles.PerformLayout()
        Me.tpImages.ResumeLayout(False)
        Me.tpImages.PerformLayout()
        Me.tblImages.ResumeLayout(False)
        Me.tblImages.PerformLayout()
        Me.pnlPoster.ResumeLayout(False)
        Me.pnlPoster.PerformLayout()
        Me.tblPoster.ResumeLayout(False)
        Me.tblPoster.PerformLayout()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFanart.ResumeLayout(False)
        Me.pnlFanart.PerformLayout()
        Me.tblFanart.ResumeLayout(False)
        Me.tblFanart.PerformLayout()
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpFrameExtraction.ResumeLayout(False)
        Me.tpFrameExtraction.PerformLayout()
        Me.tblFrameExtraction.ResumeLayout(False)
        CType(Me.pbFrame, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbFrame, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpMetaData.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.tblBottom.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents tsslFilename As ToolStripStatusLabel
    Friend WithEvents tsslSpring As ToolStripStatusLabel
    Friend WithEvents tsslStatus As ToolStripStatusLabel
    Friend WithEvents tspbStatus As ToolStripProgressBar
    Friend WithEvents pnlMain As Panel
    Friend WithEvents tcEdit As TabControl
    Friend WithEvents tpDetails As TabPage
    Friend WithEvents tblDetails As TableLayoutPanel
    Friend WithEvents btnManual As Button
    Friend WithEvents cbVideoSource As ComboBox
    Friend WithEvents lblTitle As Label
    Friend WithEvents txtTitle As TextBox
    Friend WithEvents lblOriginalTitle As Label
    Friend WithEvents txtOriginalTitle As TextBox
    Friend WithEvents lvActors As ListView
    Friend WithEvents colActorsID As ColumnHeader
    Friend WithEvents colActorsName As ColumnHeader
    Friend WithEvents colActorsRole As ColumnHeader
    Friend WithEvents colActorsThumb As ColumnHeader
    Friend WithEvents lblActors As Label
    Friend WithEvents lblPlot As Label
    Friend WithEvents lblCredits As Label
    Friend WithEvents txtPlot As TextBox
    Friend WithEvents lblAired As Label
    Friend WithEvents dtpAired As DateTimePicker
    Friend WithEvents lblRuntime As Label
    Friend WithEvents txtRuntime As TextBox
    Friend WithEvents lblVideoSource As Label
    Friend WithEvents dgvCredits As DataGridView
    Friend WithEvents colCreditsName As DataGridViewTextBoxColumn
    Friend WithEvents dgvDirectors As DataGridView
    Friend WithEvents colDirectorsName As DataGridViewTextBoxColumn
    Friend WithEvents lblDirectors As Label
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
    Friend WithEvents tpOther As TabPage
    Friend WithEvents tblOther As TableLayoutPanel
    Friend WithEvents gbMediaStub As GroupBox
    Friend WithEvents tblMediaStub As TableLayoutPanel
    Friend WithEvents lblMediaStubTitle As Label
    Friend WithEvents txtMediaStubMessage As TextBox
    Friend WithEvents lblMediaStubMessage As Label
    Friend WithEvents txtMediaStubTitle As TextBox
    Friend WithEvents gbSubtitles As GroupBox
    Friend WithEvents tblSubtitles As TableLayoutPanel
    Friend WithEvents txtSubtitlesPreview As TextBox
    Friend WithEvents btnSetSubtitleLocal As Button
    Friend WithEvents btnRemoveSubtitle As Button
    Friend WithEvents btnSetSubtitleDL As Button
    Friend WithEvents lblSubtitlesPreview As Label
    Friend WithEvents btnSetSubtitleScrape As Button
    Friend WithEvents lvSubtitles As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
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
    Friend WithEvents pnlFanart As Panel
    Friend WithEvents tblFanart As TableLayoutPanel
    Friend WithEvents pbFanart As PictureBox
    Friend WithEvents lblFanart As Label
    Friend WithEvents btnSetFanartLocal As Button
    Friend WithEvents btnSetFanartScrape As Button
    Friend WithEvents lblFanartSize As Label
    Friend WithEvents btnSetFanartDL As Button
    Friend WithEvents btnRemoveFanart As Button
    Friend WithEvents tpFrameExtraction As TabPage
    Friend WithEvents tblFrameExtraction As TableLayoutPanel
    Friend WithEvents pbFrame As PictureBox
    Friend WithEvents tbFrame As TrackBar
    Friend WithEvents btnFrameSaveAsFanart As Button
    Friend WithEvents lblTime As Label
    Friend WithEvents btnFrameLoadVideo As Button
    Friend WithEvents tpMetaData As TabPage
    Friend WithEvents pnlFileInfo As Panel
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents tblBottom As TableLayoutPanel
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnChange As Button
    Friend WithEvents btnRescrape As Button
    Friend WithEvents chkLocked As CheckBox
    Friend WithEvents chkMarked As CheckBox
    Friend WithEvents lblSeason As Label
    Friend WithEvents lblEpisode As Label
    Friend WithEvents txtSeason As TextBox
    Friend WithEvents txtEpisode As TextBox
    Friend WithEvents lblDisplaySeason As Label
    Friend WithEvents txtDisplaySeason As TextBox
    Friend WithEvents lblDisplayEpisode As Label
    Friend WithEvents txtDisplayEpisode As TextBox
    Friend WithEvents lblGuestStars As Label
    Friend WithEvents lvGuestStars As ListView
    Friend WithEvents colGuestStarsID As ColumnHeader
    Friend WithEvents colGuestStarsName As ColumnHeader
    Friend WithEvents colGuestStarsRole As ColumnHeader
    Friend WithEvents colGuestStarsThumb As ColumnHeader
    Friend WithEvents btnGuestStarsAdd As Button
    Friend WithEvents btnGuestStarsEdit As Button
    Friend WithEvents btnGuestStarsUp As Button
    Friend WithEvents btnGuestStarsDown As Button
    Friend WithEvents btnGuestStarsRemove As Button
    Friend WithEvents lblDisplaySeasonEpisode As Label
    Friend WithEvents tmrDelay As Timer
    Friend WithEvents btnFrameSaveAsPoster As Button
    Friend WithEvents ofdLocalFiles As OpenFileDialog
End Class
