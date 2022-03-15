<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgEdit_TVEpisode
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgEdit_TVEpisode))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Local Subtitles", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("1")
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tblTop = New System.Windows.Forms.TableLayoutPanel()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnChange = New System.Windows.Forms.Button()
        Me.btnRescrape = New System.Windows.Forms.Button()
        Me.chkLocked = New System.Windows.Forms.CheckBox()
        Me.chkMarked = New System.Windows.Forms.CheckBox()
        Me.ofdLocalFiles = New System.Windows.Forms.OpenFileDialog()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.tsslFilename = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslSpring = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tspbStatus = New System.Windows.Forms.ToolStripProgressBar()
        Me.tmrDelay = New System.Windows.Forms.Timer(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tcEdit = New System.Windows.Forms.TabControl()
        Me.tpDetails = New System.Windows.Forms.TabPage()
        Me.tblDetails = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDateAdded = New System.Windows.Forms.Label()
        Me.lblVideoSource = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.dtpDateAdded_Date = New System.Windows.Forms.DateTimePicker()
        Me.chkWatched = New System.Windows.Forms.CheckBox()
        Me.dtpLastPlayed_Date = New System.Windows.Forms.DateTimePicker()
        Me.lblSeason = New System.Windows.Forms.Label()
        Me.txtSeason = New System.Windows.Forms.TextBox()
        Me.lblPlot = New System.Windows.Forms.Label()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.lblAired = New System.Windows.Forms.Label()
        Me.dtpAired = New System.Windows.Forms.DateTimePicker()
        Me.lblRuntime = New System.Windows.Forms.Label()
        Me.txtRuntime = New System.Windows.Forms.TextBox()
        Me.lblUserRating = New System.Windows.Forms.Label()
        Me.lblUniqueIds = New System.Windows.Forms.Label()
        Me.dgvUniqueIds = New System.Windows.Forms.DataGridView()
        Me.colUniqueIdsDefault = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colUniqueIdsType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUniqueIdsValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cbUserRating = New System.Windows.Forms.ComboBox()
        Me.lblRatings = New System.Windows.Forms.Label()
        Me.dtpLastPlayed_Time = New System.Windows.Forms.DateTimePicker()
        Me.dgvRatings = New System.Windows.Forms.DataGridView()
        Me.colRatingsDefault = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colRatingsSource = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRatingsValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRatingsMax = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRatingsVotes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dtpDateAdded_Time = New System.Windows.Forms.DateTimePicker()
        Me.lblEpisode = New System.Windows.Forms.Label()
        Me.txtEpisode = New System.Windows.Forms.TextBox()
        Me.lblDisplaySeason = New System.Windows.Forms.Label()
        Me.lblDisplayEpisode = New System.Windows.Forms.Label()
        Me.txtDisplaySeason = New System.Windows.Forms.TextBox()
        Me.txtDisplayEpisode = New System.Windows.Forms.TextBox()
        Me.cbVideosource = New System.Windows.Forms.ComboBox()
        Me.lblUserNote = New System.Windows.Forms.Label()
        Me.txtUserNote = New System.Windows.Forms.TextBox()
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
        Me.dgvDirectors = New System.Windows.Forms.DataGridView()
        Me.colDirectorsName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvCredits = New System.Windows.Forms.DataGridView()
        Me.colCreditsName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblCredits = New System.Windows.Forms.Label()
        Me.lblDirectors = New System.Windows.Forms.Label()
        Me.lblGuestStars = New System.Windows.Forms.Label()
        Me.btnGuestStarsAdd = New System.Windows.Forms.Button()
        Me.btnGuestStarsRemove = New System.Windows.Forms.Button()
        Me.btnGuestStarsUp = New System.Windows.Forms.Button()
        Me.btnGuestStarsDown = New System.Windows.Forms.Button()
        Me.btnGuestStarsEdit = New System.Windows.Forms.Button()
        Me.lvGuestStars = New System.Windows.Forms.ListView()
        Me.colGuestStarsID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colGuestStarsName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colGuestStarsRole = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colGuestStarsThumb = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
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
        Me.colSubtitlesHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSubtitlesHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSubtitlesHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSubtitlesHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSubtitlesHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tpImages = New System.Windows.Forms.TabPage()
        Me.tblImages = New System.Windows.Forms.TableLayoutPanel()
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
        CType(Me.dgvUniqueIds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvRatings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpCastCrew.SuspendLayout()
        Me.tblCastCrew.SuspendLayout()
        CType(Me.dgvDirectors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCredits, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlTop.TabIndex = 3
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
        Me.pbTopLogo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pbTopLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbTopLogo.ErrorImage = Nothing
        Me.pbTopLogo.Image = CType(resources.GetObject("pbTopLogo.Image"), System.Drawing.Image)
        Me.pbTopLogo.InitialImage = Nothing
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
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 780)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1284, 29)
        Me.pnlBottom.TabIndex = 82
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
        Me.tblBottom.Size = New System.Drawing.Size(1284, 29)
        Me.tblBottom.TabIndex = 78
        '
        'btnOK
        '
        Me.btnOK.AutoSize = True
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOK.Enabled = False
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnOK.Location = New System.Drawing.Point(1129, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(70, 23)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(1205, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(76, 23)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        '
        'btnChange
        '
        Me.btnChange.AutoSize = True
        Me.btnChange.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnChange.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnChange.Image = CType(resources.GetObject("btnChange.Image"), System.Drawing.Image)
        Me.btnChange.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnChange.Location = New System.Drawing.Point(631, 3)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(107, 23)
        Me.btnChange.TabIndex = 3
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
        Me.btnRescrape.Location = New System.Drawing.Point(527, 3)
        Me.btnRescrape.Name = "btnRescrape"
        Me.btnRescrape.Size = New System.Drawing.Size(98, 23)
        Me.btnRescrape.TabIndex = 2
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
        'StatusStrip
        '
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(18, 18)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslFilename, Me.tsslSpring, Me.tsslStatus, Me.tspbStatus})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 809)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1284, 22)
        Me.StatusStrip.TabIndex = 83
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
        'tmrDelay
        '
        Me.tmrDelay.Interval = 250
        '
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlMain.Controls.Add(Me.tcEdit)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1284, 724)
        Me.pnlMain.TabIndex = 84
        '
        'tcEdit
        '
        Me.tcEdit.Controls.Add(Me.tpDetails)
        Me.tcEdit.Controls.Add(Me.tpCastCrew)
        Me.tcEdit.Controls.Add(Me.tpOther)
        Me.tcEdit.Controls.Add(Me.tpImages)
        Me.tcEdit.Controls.Add(Me.tpFrameExtraction)
        Me.tcEdit.Controls.Add(Me.tpMetaData)
        Me.tcEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tcEdit.Location = New System.Drawing.Point(0, 0)
        Me.tcEdit.Name = "tcEdit"
        Me.tcEdit.SelectedIndex = 0
        Me.tcEdit.Size = New System.Drawing.Size(1284, 724)
        Me.tcEdit.TabIndex = 0
        '
        'tpDetails
        '
        Me.tpDetails.BackColor = System.Drawing.SystemColors.Control
        Me.tpDetails.Controls.Add(Me.tblDetails)
        Me.tpDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpDetails.Name = "tpDetails"
        Me.tpDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDetails.Size = New System.Drawing.Size(1276, 698)
        Me.tpDetails.TabIndex = 0
        Me.tpDetails.Text = "Details"
        '
        'tblDetails
        '
        Me.tblDetails.AutoScroll = True
        Me.tblDetails.AutoSize = True
        Me.tblDetails.ColumnCount = 9
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblDetails.Controls.Add(Me.lblDateAdded, 0, 5)
        Me.tblDetails.Controls.Add(Me.lblVideoSource, 0, 7)
        Me.tblDetails.Controls.Add(Me.lblTitle, 0, 0)
        Me.tblDetails.Controls.Add(Me.txtTitle, 1, 0)
        Me.tblDetails.Controls.Add(Me.dtpDateAdded_Date, 1, 5)
        Me.tblDetails.Controls.Add(Me.chkWatched, 0, 6)
        Me.tblDetails.Controls.Add(Me.dtpLastPlayed_Date, 1, 6)
        Me.tblDetails.Controls.Add(Me.lblSeason, 0, 1)
        Me.tblDetails.Controls.Add(Me.txtSeason, 1, 1)
        Me.tblDetails.Controls.Add(Me.lblPlot, 0, 3)
        Me.tblDetails.Controls.Add(Me.txtPlot, 1, 3)
        Me.tblDetails.Controls.Add(Me.lblAired, 6, 0)
        Me.tblDetails.Controls.Add(Me.dtpAired, 7, 0)
        Me.tblDetails.Controls.Add(Me.lblRuntime, 6, 1)
        Me.tblDetails.Controls.Add(Me.txtRuntime, 7, 1)
        Me.tblDetails.Controls.Add(Me.lblUserRating, 6, 2)
        Me.tblDetails.Controls.Add(Me.lblUniqueIds, 6, 5)
        Me.tblDetails.Controls.Add(Me.dgvUniqueIds, 7, 5)
        Me.tblDetails.Controls.Add(Me.cbUserRating, 7, 2)
        Me.tblDetails.Controls.Add(Me.lblRatings, 6, 3)
        Me.tblDetails.Controls.Add(Me.dtpLastPlayed_Time, 2, 6)
        Me.tblDetails.Controls.Add(Me.dgvRatings, 7, 3)
        Me.tblDetails.Controls.Add(Me.dtpDateAdded_Time, 2, 5)
        Me.tblDetails.Controls.Add(Me.lblEpisode, 2, 1)
        Me.tblDetails.Controls.Add(Me.txtEpisode, 3, 1)
        Me.tblDetails.Controls.Add(Me.lblDisplaySeason, 0, 2)
        Me.tblDetails.Controls.Add(Me.lblDisplayEpisode, 2, 2)
        Me.tblDetails.Controls.Add(Me.txtDisplaySeason, 1, 2)
        Me.tblDetails.Controls.Add(Me.txtDisplayEpisode, 3, 2)
        Me.tblDetails.Controls.Add(Me.cbVideosource, 1, 7)
        Me.tblDetails.Controls.Add(Me.lblUserNote, 0, 8)
        Me.tblDetails.Controls.Add(Me.txtUserNote, 1, 8)
        Me.tblDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblDetails.Location = New System.Drawing.Point(3, 3)
        Me.tblDetails.Name = "tblDetails"
        Me.tblDetails.RowCount = 11
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDetails.Size = New System.Drawing.Size(1270, 692)
        Me.tblDetails.TabIndex = 78
        '
        'lblDateAdded
        '
        Me.lblDateAdded.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblDateAdded.AutoSize = True
        Me.lblDateAdded.Location = New System.Drawing.Point(22, 315)
        Me.lblDateAdded.Name = "lblDateAdded"
        Me.lblDateAdded.Size = New System.Drawing.Size(68, 13)
        Me.lblDateAdded.TabIndex = 0
        Me.lblDateAdded.Text = "Date Added"
        '
        'lblVideoSource
        '
        Me.lblVideoSource.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblVideoSource.AutoSize = True
        Me.lblVideoSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoSource.Location = New System.Drawing.Point(12, 371)
        Me.lblVideoSource.Name = "lblVideoSource"
        Me.lblVideoSource.Size = New System.Drawing.Size(78, 13)
        Me.lblVideoSource.TabIndex = 48
        Me.lblVideoSource.Text = "Video Source:"
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(58, 7)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(32, 13)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Title:"
        '
        'txtTitle
        '
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Window
        Me.tblDetails.SetColumnSpan(Me.txtTitle, 4)
        Me.txtTitle.Location = New System.Drawing.Point(96, 3)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(400, 22)
        Me.txtTitle.TabIndex = 0
        '
        'dtpDateAdded_Date
        '
        Me.dtpDateAdded_Date.CustomFormat = ""
        Me.dtpDateAdded_Date.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateAdded_Date.Location = New System.Drawing.Point(96, 311)
        Me.dtpDateAdded_Date.Name = "dtpDateAdded_Date"
        Me.dtpDateAdded_Date.Size = New System.Drawing.Size(100, 22)
        Me.dtpDateAdded_Date.TabIndex = 7
        Me.dtpDateAdded_Date.Value = New Date(2021, 1, 1, 0, 0, 0, 0)
        '
        'chkWatched
        '
        Me.chkWatched.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.chkWatched.AutoSize = True
        Me.chkWatched.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkWatched.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWatched.Location = New System.Drawing.Point(18, 341)
        Me.chkWatched.Name = "chkWatched"
        Me.chkWatched.Size = New System.Drawing.Size(72, 17)
        Me.chkWatched.TabIndex = 9
        Me.chkWatched.Text = "Watched"
        Me.chkWatched.UseVisualStyleBackColor = True
        '
        'dtpLastPlayed_Date
        '
        Me.dtpLastPlayed_Date.CustomFormat = ""
        Me.dtpLastPlayed_Date.Enabled = False
        Me.dtpLastPlayed_Date.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpLastPlayed_Date.Location = New System.Drawing.Point(96, 339)
        Me.dtpLastPlayed_Date.Name = "dtpLastPlayed_Date"
        Me.dtpLastPlayed_Date.Size = New System.Drawing.Size(100, 22)
        Me.dtpLastPlayed_Date.TabIndex = 10
        Me.dtpLastPlayed_Date.Value = New Date(2021, 1, 1, 20, 0, 0, 0)
        '
        'lblSeason
        '
        Me.lblSeason.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblSeason.AutoSize = True
        Me.lblSeason.Location = New System.Drawing.Point(43, 35)
        Me.lblSeason.Name = "lblSeason"
        Me.lblSeason.Size = New System.Drawing.Size(47, 13)
        Me.lblSeason.TabIndex = 4
        Me.lblSeason.Text = "Season:"
        '
        'txtSeason
        '
        Me.txtSeason.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeason.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSeason.Location = New System.Drawing.Point(96, 31)
        Me.txtSeason.Name = "txtSeason"
        Me.txtSeason.Size = New System.Drawing.Size(100, 22)
        Me.txtSeason.TabIndex = 2
        '
        'lblPlot
        '
        Me.lblPlot.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblPlot.AutoSize = True
        Me.lblPlot.Location = New System.Drawing.Point(60, 91)
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
        Me.txtPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtPlot.Location = New System.Drawing.Point(96, 87)
        Me.txtPlot.Multiline = True
        Me.txtPlot.Name = "txtPlot"
        Me.tblDetails.SetRowSpan(Me.txtPlot, 2)
        Me.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlot.Size = New System.Drawing.Size(400, 218)
        Me.txtPlot.TabIndex = 6
        '
        'lblAired
        '
        Me.lblAired.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblAired.AutoSize = True
        Me.lblAired.Location = New System.Drawing.Point(555, 7)
        Me.lblAired.Name = "lblAired"
        Me.lblAired.Size = New System.Drawing.Size(37, 13)
        Me.lblAired.TabIndex = 13
        Me.lblAired.Text = "Aired:"
        '
        'dtpAired
        '
        Me.dtpAired.Checked = False
        Me.dtpAired.CustomFormat = ""
        Me.dtpAired.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpAired.Location = New System.Drawing.Point(598, 3)
        Me.dtpAired.Name = "dtpAired"
        Me.dtpAired.ShowCheckBox = True
        Me.dtpAired.Size = New System.Drawing.Size(120, 22)
        Me.dtpAired.TabIndex = 13
        Me.dtpAired.Value = New Date(2021, 1, 1, 0, 0, 0, 0)
        '
        'lblRuntime
        '
        Me.lblRuntime.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblRuntime.AutoSize = True
        Me.lblRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRuntime.Location = New System.Drawing.Point(539, 35)
        Me.lblRuntime.Name = "lblRuntime"
        Me.lblRuntime.Size = New System.Drawing.Size(53, 13)
        Me.lblRuntime.TabIndex = 59
        Me.lblRuntime.Text = "Runtime:"
        '
        'txtRuntime
        '
        Me.txtRuntime.BackColor = System.Drawing.SystemColors.Window
        Me.txtRuntime.Location = New System.Drawing.Point(598, 31)
        Me.txtRuntime.Name = "txtRuntime"
        Me.txtRuntime.Size = New System.Drawing.Size(120, 22)
        Me.txtRuntime.TabIndex = 14
        Me.txtRuntime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblUserRating
        '
        Me.lblUserRating.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblUserRating.AutoSize = True
        Me.lblUserRating.Location = New System.Drawing.Point(522, 63)
        Me.lblUserRating.Name = "lblUserRating"
        Me.lblUserRating.Size = New System.Drawing.Size(70, 13)
        Me.lblUserRating.TabIndex = 19
        Me.lblUserRating.Text = "User Rating:"
        '
        'lblUniqueIds
        '
        Me.lblUniqueIds.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblUniqueIds.AutoSize = True
        Me.lblUniqueIds.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblUniqueIds.Location = New System.Drawing.Point(525, 315)
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
        Me.dgvUniqueIds.Location = New System.Drawing.Point(598, 311)
        Me.dgvUniqueIds.Name = "dgvUniqueIds"
        Me.dgvUniqueIds.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.tblDetails.SetRowSpan(Me.dgvUniqueIds, 5)
        Me.dgvUniqueIds.Size = New System.Drawing.Size(400, 218)
        Me.dgvUniqueIds.TabIndex = 17
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
        'cbUserRating
        '
        Me.cbUserRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbUserRating.FormattingEnabled = True
        Me.cbUserRating.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cbUserRating.Location = New System.Drawing.Point(598, 59)
        Me.cbUserRating.Name = "cbUserRating"
        Me.cbUserRating.Size = New System.Drawing.Size(120, 21)
        Me.cbUserRating.TabIndex = 15
        '
        'lblRatings
        '
        Me.lblRatings.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblRatings.AutoSize = True
        Me.lblRatings.Location = New System.Drawing.Point(543, 91)
        Me.lblRatings.Name = "lblRatings"
        Me.lblRatings.Size = New System.Drawing.Size(49, 13)
        Me.lblRatings.TabIndex = 10
        Me.lblRatings.Text = "Ratings:"
        '
        'dtpLastPlayed_Time
        '
        Me.dtpLastPlayed_Time.CustomFormat = ""
        Me.dtpLastPlayed_Time.Enabled = False
        Me.dtpLastPlayed_Time.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpLastPlayed_Time.Location = New System.Drawing.Point(202, 339)
        Me.dtpLastPlayed_Time.Name = "dtpLastPlayed_Time"
        Me.dtpLastPlayed_Time.ShowUpDown = True
        Me.dtpLastPlayed_Time.Size = New System.Drawing.Size(100, 22)
        Me.dtpLastPlayed_Time.TabIndex = 11
        Me.dtpLastPlayed_Time.Value = New Date(2021, 2, 2, 20, 0, 0, 0)
        '
        'dgvRatings
        '
        Me.dgvRatings.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvRatings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvRatings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRatings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRatingsDefault, Me.colRatingsSource, Me.colRatingsValue, Me.colRatingsMax, Me.colRatingsVotes})
        Me.dgvRatings.Location = New System.Drawing.Point(598, 87)
        Me.dgvRatings.Name = "dgvRatings"
        Me.dgvRatings.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.tblDetails.SetRowSpan(Me.dgvRatings, 2)
        Me.dgvRatings.Size = New System.Drawing.Size(400, 218)
        Me.dgvRatings.TabIndex = 16
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
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colRatingsValue.DefaultCellStyle = DataGridViewCellStyle1
        Me.colRatingsValue.HeaderText = "Value"
        Me.colRatingsValue.Name = "colRatingsValue"
        Me.colRatingsValue.Width = 60
        '
        'colRatingsMax
        '
        Me.colRatingsMax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colRatingsMax.DefaultCellStyle = DataGridViewCellStyle2
        Me.colRatingsMax.HeaderText = "Max"
        Me.colRatingsMax.Name = "colRatingsMax"
        Me.colRatingsMax.Width = 53
        '
        'colRatingsVotes
        '
        Me.colRatingsVotes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colRatingsVotes.DefaultCellStyle = DataGridViewCellStyle3
        Me.colRatingsVotes.HeaderText = "Votes"
        Me.colRatingsVotes.Name = "colRatingsVotes"
        Me.colRatingsVotes.Width = 60
        '
        'dtpDateAdded_Time
        '
        Me.dtpDateAdded_Time.CustomFormat = ""
        Me.dtpDateAdded_Time.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpDateAdded_Time.Location = New System.Drawing.Point(202, 311)
        Me.dtpDateAdded_Time.Name = "dtpDateAdded_Time"
        Me.dtpDateAdded_Time.ShowUpDown = True
        Me.dtpDateAdded_Time.Size = New System.Drawing.Size(100, 22)
        Me.dtpDateAdded_Time.TabIndex = 8
        Me.dtpDateAdded_Time.Value = New Date(2021, 2, 2, 20, 0, 0, 0)
        '
        'lblEpisode
        '
        Me.lblEpisode.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblEpisode.AutoSize = True
        Me.lblEpisode.Location = New System.Drawing.Point(251, 35)
        Me.lblEpisode.Name = "lblEpisode"
        Me.lblEpisode.Size = New System.Drawing.Size(51, 13)
        Me.lblEpisode.TabIndex = 4
        Me.lblEpisode.Text = "Episode:"
        '
        'txtEpisode
        '
        Me.txtEpisode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtEpisode.Location = New System.Drawing.Point(308, 31)
        Me.txtEpisode.Name = "txtEpisode"
        Me.txtEpisode.Size = New System.Drawing.Size(100, 22)
        Me.txtEpisode.TabIndex = 3
        '
        'lblDisplaySeason
        '
        Me.lblDisplaySeason.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblDisplaySeason.AutoSize = True
        Me.lblDisplaySeason.Location = New System.Drawing.Point(3, 63)
        Me.lblDisplaySeason.Name = "lblDisplaySeason"
        Me.lblDisplaySeason.Size = New System.Drawing.Size(87, 13)
        Me.lblDisplaySeason.TabIndex = 4
        Me.lblDisplaySeason.Text = "Display Season:"
        '
        'lblDisplayEpisode
        '
        Me.lblDisplayEpisode.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblDisplayEpisode.AutoSize = True
        Me.lblDisplayEpisode.Location = New System.Drawing.Point(211, 63)
        Me.lblDisplayEpisode.Name = "lblDisplayEpisode"
        Me.lblDisplayEpisode.Size = New System.Drawing.Size(91, 13)
        Me.lblDisplayEpisode.TabIndex = 4
        Me.lblDisplayEpisode.Text = "Display Episode:"
        '
        'txtDisplaySeason
        '
        Me.txtDisplaySeason.BackColor = System.Drawing.SystemColors.Window
        Me.txtDisplaySeason.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtDisplaySeason.Location = New System.Drawing.Point(96, 59)
        Me.txtDisplaySeason.Name = "txtDisplaySeason"
        Me.txtDisplaySeason.Size = New System.Drawing.Size(100, 22)
        Me.txtDisplaySeason.TabIndex = 4
        '
        'txtDisplayEpisode
        '
        Me.txtDisplayEpisode.BackColor = System.Drawing.SystemColors.Window
        Me.txtDisplayEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtDisplayEpisode.Location = New System.Drawing.Point(308, 59)
        Me.txtDisplayEpisode.Name = "txtDisplayEpisode"
        Me.txtDisplayEpisode.Size = New System.Drawing.Size(100, 22)
        Me.txtDisplayEpisode.TabIndex = 5
        '
        'cbVideosource
        '
        Me.tblDetails.SetColumnSpan(Me.cbVideosource, 2)
        Me.cbVideosource.FormattingEnabled = True
        Me.cbVideosource.Location = New System.Drawing.Point(96, 367)
        Me.cbVideosource.Name = "cbVideosource"
        Me.cbVideosource.Size = New System.Drawing.Size(203, 21)
        Me.cbVideosource.TabIndex = 12
        '
        'lblUserNote
        '
        Me.lblUserNote.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblUserNote.AutoSize = True
        Me.lblUserNote.Location = New System.Drawing.Point(55, 398)
        Me.lblUserNote.Name = "lblUserNote"
        Me.lblUserNote.Size = New System.Drawing.Size(35, 13)
        Me.lblUserNote.TabIndex = 45
        Me.lblUserNote.Text = "Note:"
        '
        'txtUserNote
        '
        Me.tblDetails.SetColumnSpan(Me.txtUserNote, 3)
        Me.txtUserNote.Location = New System.Drawing.Point(96, 394)
        Me.txtUserNote.Name = "txtUserNote"
        Me.txtUserNote.Size = New System.Drawing.Size(400, 22)
        Me.txtUserNote.TabIndex = 62
        '
        'tpCastCrew
        '
        Me.tpCastCrew.BackColor = System.Drawing.SystemColors.Control
        Me.tpCastCrew.Controls.Add(Me.tblCastCrew)
        Me.tpCastCrew.Location = New System.Drawing.Point(4, 22)
        Me.tpCastCrew.Name = "tpCastCrew"
        Me.tpCastCrew.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCastCrew.Size = New System.Drawing.Size(1276, 698)
        Me.tpCastCrew.TabIndex = 19
        Me.tpCastCrew.Text = "Cast & Crew"
        '
        'tblCastCrew
        '
        Me.tblCastCrew.AutoSize = True
        Me.tblCastCrew.ColumnCount = 5
        Me.tblCastCrew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCastCrew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblCastCrew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblCastCrew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCastCrew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblCastCrew.Controls.Add(Me.lblActors, 0, 0)
        Me.tblCastCrew.Controls.Add(Me.lvActors, 1, 0)
        Me.tblCastCrew.Controls.Add(Me.btnActorsAdd, 0, 1)
        Me.tblCastCrew.Controls.Add(Me.btnActorsRemove, 0, 2)
        Me.tblCastCrew.Controls.Add(Me.btnActorsEdit, 0, 5)
        Me.tblCastCrew.Controls.Add(Me.btnActorsDown, 0, 4)
        Me.tblCastCrew.Controls.Add(Me.btnActorsUp, 0, 3)
        Me.tblCastCrew.Controls.Add(Me.dgvDirectors, 4, 13)
        Me.tblCastCrew.Controls.Add(Me.dgvCredits, 1, 13)
        Me.tblCastCrew.Controls.Add(Me.lblCredits, 0, 13)
        Me.tblCastCrew.Controls.Add(Me.lblDirectors, 3, 13)
        Me.tblCastCrew.Controls.Add(Me.lblGuestStars, 0, 6)
        Me.tblCastCrew.Controls.Add(Me.btnGuestStarsAdd, 0, 7)
        Me.tblCastCrew.Controls.Add(Me.btnGuestStarsRemove, 0, 8)
        Me.tblCastCrew.Controls.Add(Me.btnGuestStarsUp, 0, 9)
        Me.tblCastCrew.Controls.Add(Me.btnGuestStarsDown, 0, 10)
        Me.tblCastCrew.Controls.Add(Me.btnGuestStarsEdit, 0, 11)
        Me.tblCastCrew.Controls.Add(Me.lvGuestStars, 1, 6)
        Me.tblCastCrew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblCastCrew.Location = New System.Drawing.Point(3, 3)
        Me.tblCastCrew.Name = "tblCastCrew"
        Me.tblCastCrew.RowCount = 15
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tblCastCrew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.tblCastCrew.Size = New System.Drawing.Size(1270, 692)
        Me.tblCastCrew.TabIndex = 0
        '
        'lblActors
        '
        Me.lblActors.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblActors.AutoSize = True
        Me.lblActors.Location = New System.Drawing.Point(29, 7)
        Me.lblActors.Name = "lblActors"
        Me.lblActors.Size = New System.Drawing.Size(42, 13)
        Me.lblActors.TabIndex = 30
        Me.lblActors.Text = "Actors:"
        '
        'lvActors
        '
        Me.lvActors.BackColor = System.Drawing.SystemColors.Window
        Me.lvActors.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colActorsID, Me.colActorsName, Me.colActorsRole, Me.colActorsThumb})
        Me.tblCastCrew.SetColumnSpan(Me.lvActors, 4)
        Me.lvActors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvActors.FullRowSelect = True
        Me.lvActors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvActors.HideSelection = False
        Me.lvActors.Location = New System.Drawing.Point(77, 3)
        Me.lvActors.Name = "lvActors"
        Me.tblCastCrew.SetRowSpan(Me.lvActors, 6)
        Me.lvActors.Size = New System.Drawing.Size(1190, 262)
        Me.lvActors.TabIndex = 5
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
        Me.btnActorsAdd.Location = New System.Drawing.Point(48, 31)
        Me.btnActorsAdd.Name = "btnActorsAdd"
        Me.btnActorsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsAdd.TabIndex = 0
        Me.btnActorsAdd.UseVisualStyleBackColor = True
        '
        'btnActorsRemove
        '
        Me.btnActorsRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnActorsRemove.Image = CType(resources.GetObject("btnActorsRemove.Image"), System.Drawing.Image)
        Me.btnActorsRemove.Location = New System.Drawing.Point(48, 60)
        Me.btnActorsRemove.Name = "btnActorsRemove"
        Me.btnActorsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsRemove.TabIndex = 1
        Me.btnActorsRemove.UseVisualStyleBackColor = True
        '
        'btnActorsEdit
        '
        Me.btnActorsEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnActorsEdit.Image = CType(resources.GetObject("btnActorsEdit.Image"), System.Drawing.Image)
        Me.btnActorsEdit.Location = New System.Drawing.Point(48, 147)
        Me.btnActorsEdit.Name = "btnActorsEdit"
        Me.btnActorsEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsEdit.TabIndex = 4
        Me.btnActorsEdit.UseVisualStyleBackColor = True
        '
        'btnActorsDown
        '
        Me.btnActorsDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnActorsDown.Image = CType(resources.GetObject("btnActorsDown.Image"), System.Drawing.Image)
        Me.btnActorsDown.Location = New System.Drawing.Point(48, 118)
        Me.btnActorsDown.Name = "btnActorsDown"
        Me.btnActorsDown.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsDown.TabIndex = 3
        Me.btnActorsDown.UseVisualStyleBackColor = True
        '
        'btnActorsUp
        '
        Me.btnActorsUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnActorsUp.Image = CType(resources.GetObject("btnActorsUp.Image"), System.Drawing.Image)
        Me.btnActorsUp.Location = New System.Drawing.Point(48, 89)
        Me.btnActorsUp.Name = "btnActorsUp"
        Me.btnActorsUp.Size = New System.Drawing.Size(23, 23)
        Me.btnActorsUp.TabIndex = 2
        Me.btnActorsUp.UseVisualStyleBackColor = True
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
        Me.dgvDirectors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDirectors.Location = New System.Drawing.Point(716, 559)
        Me.dgvDirectors.Name = "dgvDirectors"
        Me.dgvDirectors.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.tblCastCrew.SetRowSpan(Me.dgvDirectors, 2)
        Me.dgvDirectors.Size = New System.Drawing.Size(551, 130)
        Me.dgvDirectors.TabIndex = 13
        '
        'colDirectorsName
        '
        Me.colDirectorsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDirectorsName.HeaderText = "Name"
        Me.colDirectorsName.Name = "colDirectorsName"
        Me.colDirectorsName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
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
        Me.dgvCredits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCredits.Location = New System.Drawing.Point(77, 559)
        Me.dgvCredits.Name = "dgvCredits"
        Me.dgvCredits.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.tblCastCrew.SetRowSpan(Me.dgvCredits, 2)
        Me.dgvCredits.Size = New System.Drawing.Size(551, 130)
        Me.dgvCredits.TabIndex = 12
        '
        'colCreditsName
        '
        Me.colCreditsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colCreditsName.HeaderText = "Name"
        Me.colCreditsName.Name = "colCreditsName"
        Me.colCreditsName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'lblCredits
        '
        Me.lblCredits.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblCredits.AutoSize = True
        Me.lblCredits.Location = New System.Drawing.Point(25, 563)
        Me.lblCredits.Name = "lblCredits"
        Me.lblCredits.Size = New System.Drawing.Size(46, 13)
        Me.lblCredits.TabIndex = 41
        Me.lblCredits.Text = "Credits:"
        '
        'lblDirectors
        '
        Me.lblDirectors.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblDirectors.AutoSize = True
        Me.lblDirectors.Location = New System.Drawing.Point(654, 563)
        Me.lblDirectors.Name = "lblDirectors"
        Me.lblDirectors.Size = New System.Drawing.Size(56, 13)
        Me.lblDirectors.TabIndex = 43
        Me.lblDirectors.Text = "Directors:"
        '
        'lblGuestStars
        '
        Me.lblGuestStars.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblGuestStars.AutoSize = True
        Me.lblGuestStars.Location = New System.Drawing.Point(3, 275)
        Me.lblGuestStars.Name = "lblGuestStars"
        Me.lblGuestStars.Size = New System.Drawing.Size(68, 13)
        Me.lblGuestStars.TabIndex = 30
        Me.lblGuestStars.Text = "Guest Stars:"
        '
        'btnGuestStarsAdd
        '
        Me.btnGuestStarsAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGuestStarsAdd.Image = CType(resources.GetObject("btnGuestStarsAdd.Image"), System.Drawing.Image)
        Me.btnGuestStarsAdd.Location = New System.Drawing.Point(48, 299)
        Me.btnGuestStarsAdd.Name = "btnGuestStarsAdd"
        Me.btnGuestStarsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnGuestStarsAdd.TabIndex = 6
        Me.btnGuestStarsAdd.UseVisualStyleBackColor = True
        '
        'btnGuestStarsRemove
        '
        Me.btnGuestStarsRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGuestStarsRemove.Image = CType(resources.GetObject("btnGuestStarsRemove.Image"), System.Drawing.Image)
        Me.btnGuestStarsRemove.Location = New System.Drawing.Point(48, 328)
        Me.btnGuestStarsRemove.Name = "btnGuestStarsRemove"
        Me.btnGuestStarsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnGuestStarsRemove.TabIndex = 7
        Me.btnGuestStarsRemove.UseVisualStyleBackColor = True
        '
        'btnGuestStarsUp
        '
        Me.btnGuestStarsUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGuestStarsUp.Image = CType(resources.GetObject("btnGuestStarsUp.Image"), System.Drawing.Image)
        Me.btnGuestStarsUp.Location = New System.Drawing.Point(48, 357)
        Me.btnGuestStarsUp.Name = "btnGuestStarsUp"
        Me.btnGuestStarsUp.Size = New System.Drawing.Size(23, 23)
        Me.btnGuestStarsUp.TabIndex = 8
        Me.btnGuestStarsUp.UseVisualStyleBackColor = True
        '
        'btnGuestStarsDown
        '
        Me.btnGuestStarsDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGuestStarsDown.Image = CType(resources.GetObject("btnGuestStarsDown.Image"), System.Drawing.Image)
        Me.btnGuestStarsDown.Location = New System.Drawing.Point(48, 386)
        Me.btnGuestStarsDown.Name = "btnGuestStarsDown"
        Me.btnGuestStarsDown.Size = New System.Drawing.Size(23, 23)
        Me.btnGuestStarsDown.TabIndex = 9
        Me.btnGuestStarsDown.UseVisualStyleBackColor = True
        '
        'btnGuestStarsEdit
        '
        Me.btnGuestStarsEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGuestStarsEdit.Image = CType(resources.GetObject("btnGuestStarsEdit.Image"), System.Drawing.Image)
        Me.btnGuestStarsEdit.Location = New System.Drawing.Point(48, 415)
        Me.btnGuestStarsEdit.Name = "btnGuestStarsEdit"
        Me.btnGuestStarsEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnGuestStarsEdit.TabIndex = 10
        Me.btnGuestStarsEdit.UseVisualStyleBackColor = True
        '
        'lvGuestStars
        '
        Me.lvGuestStars.BackColor = System.Drawing.SystemColors.Window
        Me.lvGuestStars.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colGuestStarsID, Me.colGuestStarsName, Me.colGuestStarsRole, Me.colGuestStarsThumb})
        Me.tblCastCrew.SetColumnSpan(Me.lvGuestStars, 4)
        Me.lvGuestStars.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvGuestStars.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvGuestStars.FullRowSelect = True
        Me.lvGuestStars.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvGuestStars.HideSelection = False
        Me.lvGuestStars.Location = New System.Drawing.Point(77, 271)
        Me.lvGuestStars.Name = "lvGuestStars"
        Me.tblCastCrew.SetRowSpan(Me.lvGuestStars, 6)
        Me.lvGuestStars.Size = New System.Drawing.Size(1190, 262)
        Me.lvGuestStars.TabIndex = 11
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
        Me.colGuestStarsName.Width = 300
        '
        'colGuestStarsRole
        '
        Me.colGuestStarsRole.Text = "Role"
        Me.colGuestStarsRole.Width = 300
        '
        'colGuestStarsThumb
        '
        Me.colGuestStarsThumb.Text = "Thumb"
        Me.colGuestStarsThumb.Width = 500
        '
        'tpOther
        '
        Me.tpOther.BackColor = System.Drawing.SystemColors.Control
        Me.tpOther.Controls.Add(Me.tblOther)
        Me.tpOther.Location = New System.Drawing.Point(4, 22)
        Me.tpOther.Name = "tpOther"
        Me.tpOther.Size = New System.Drawing.Size(1276, 698)
        Me.tpOther.TabIndex = 17
        Me.tpOther.Text = "Other"
        '
        'tblOther
        '
        Me.tblOther.AutoSize = True
        Me.tblOther.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblOther.BackColor = System.Drawing.SystemColors.Control
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
        Me.tblOther.Size = New System.Drawing.Size(1276, 698)
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
        Me.gbMediaStub.Size = New System.Drawing.Size(1270, 117)
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
        Me.tblMediaStub.Size = New System.Drawing.Size(1264, 96)
        Me.tblMediaStub.TabIndex = 5
        '
        'lblMediaStubTitle
        '
        Me.lblMediaStubTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMediaStubTitle.AutoSize = True
        Me.lblMediaStubTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMediaStubTitle.Location = New System.Drawing.Point(3, 3)
        Me.lblMediaStubTitle.Name = "lblMediaStubTitle"
        Me.lblMediaStubTitle.Size = New System.Drawing.Size(32, 13)
        Me.lblMediaStubTitle.TabIndex = 2
        Me.lblMediaStubTitle.Text = "Title:"
        '
        'txtMediaStubMessage
        '
        Me.txtMediaStubMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMediaStubMessage.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMediaStubMessage.Location = New System.Drawing.Point(3, 71)
        Me.txtMediaStubMessage.Name = "txtMediaStubMessage"
        Me.txtMediaStubMessage.Size = New System.Drawing.Size(1258, 22)
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
        Me.txtMediaStubTitle.Size = New System.Drawing.Size(1258, 22)
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
        Me.gbSubtitles.Size = New System.Drawing.Size(1270, 569)
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
        Me.tblSubtitles.Size = New System.Drawing.Size(1264, 548)
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
        Me.txtSubtitlesPreview.Size = New System.Drawing.Size(1258, 367)
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
        Me.btnRemoveSubtitle.Location = New System.Drawing.Point(1238, 129)
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
        Me.lvSubtitles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colSubtitlesHeader1, Me.colSubtitlesHeader2, Me.colSubtitlesHeader3, Me.colSubtitlesHeader4, Me.colSubtitlesHeader6})
        Me.tblSubtitles.SetColumnSpan(Me.lvSubtitles, 5)
        Me.lvSubtitles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvSubtitles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvSubtitles.FullRowSelect = True
        ListViewGroup1.Header = "Local Subtitles"
        ListViewGroup1.Name = "LocalSubtitles"
        Me.lvSubtitles.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1})
        Me.lvSubtitles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvSubtitles.HideSelection = False
        ListViewItem1.Group = ListViewGroup1
        Me.lvSubtitles.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.lvSubtitles.Location = New System.Drawing.Point(3, 3)
        Me.lvSubtitles.MultiSelect = False
        Me.lvSubtitles.Name = "lvSubtitles"
        Me.lvSubtitles.Size = New System.Drawing.Size(1258, 120)
        Me.lvSubtitles.TabIndex = 0
        Me.lvSubtitles.UseCompatibleStateImageBehavior = False
        Me.lvSubtitles.View = System.Windows.Forms.View.Details
        '
        'colSubtitlesHeader1
        '
        Me.colSubtitlesHeader1.Width = 25
        '
        'colSubtitlesHeader2
        '
        Me.colSubtitlesHeader2.Width = 550
        '
        'colSubtitlesHeader3
        '
        Me.colSubtitlesHeader3.Width = 100
        '
        'tpImages
        '
        Me.tpImages.BackColor = System.Drawing.SystemColors.Control
        Me.tpImages.Controls.Add(Me.tblImages)
        Me.tpImages.Location = New System.Drawing.Point(4, 22)
        Me.tpImages.Name = "tpImages"
        Me.tpImages.Padding = New System.Windows.Forms.Padding(3)
        Me.tpImages.Size = New System.Drawing.Size(1276, 698)
        Me.tpImages.TabIndex = 16
        Me.tpImages.Text = "Images"
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
        Me.tblImages.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 692.0!))
        Me.tblImages.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 692.0!))
        Me.tblImages.Size = New System.Drawing.Size(1270, 692)
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
        Me.btnLocalPoster.TabIndex = 3
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
        Me.btnScrapePoster.TabIndex = 1
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
        Me.btnRemovePoster.TabIndex = 6
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
        Me.btnLocalFanart.TabIndex = 1
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
        Me.btnRemoveFanart.TabIndex = 2
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
        Me.btnClipboardFanart.TabIndex = 1
        Me.btnClipboardFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClipboardFanart.UseVisualStyleBackColor = True
        '
        'tpFrameExtraction
        '
        Me.tpFrameExtraction.BackColor = System.Drawing.SystemColors.Control
        Me.tpFrameExtraction.Controls.Add(Me.tblFrameExtraction)
        Me.tpFrameExtraction.Location = New System.Drawing.Point(4, 22)
        Me.tpFrameExtraction.Name = "tpFrameExtraction"
        Me.tpFrameExtraction.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFrameExtraction.Size = New System.Drawing.Size(1276, 698)
        Me.tpFrameExtraction.TabIndex = 3
        Me.tpFrameExtraction.Text = "Frame Extraction"
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
        Me.tblFrameExtraction.Size = New System.Drawing.Size(1270, 692)
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
        Me.pbFrame.Size = New System.Drawing.Size(1162, 653)
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
        Me.btnFrameSaveAsPoster.Location = New System.Drawing.Point(1171, 573)
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
        Me.tbFrame.Location = New System.Drawing.Point(3, 662)
        Me.tbFrame.Name = "tbFrame"
        Me.tbFrame.Size = New System.Drawing.Size(1097, 27)
        Me.tbFrame.TabIndex = 1
        Me.tbFrame.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'btnFrameSaveAsFanart
        '
        Me.btnFrameSaveAsFanart.Enabled = False
        Me.btnFrameSaveAsFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFrameSaveAsFanart.Image = CType(resources.GetObject("btnFrameSaveAsFanart.Image"), System.Drawing.Image)
        Me.btnFrameSaveAsFanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFrameSaveAsFanart.Location = New System.Drawing.Point(1171, 484)
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
        Me.lblTime.Location = New System.Drawing.Point(1106, 664)
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
        Me.btnFrameLoadVideo.Location = New System.Drawing.Point(1171, 3)
        Me.btnFrameLoadVideo.Name = "btnFrameLoadVideo"
        Me.btnFrameLoadVideo.Size = New System.Drawing.Size(96, 83)
        Me.btnFrameLoadVideo.TabIndex = 0
        Me.btnFrameLoadVideo.Text = "Load Video"
        Me.btnFrameLoadVideo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnFrameLoadVideo.UseVisualStyleBackColor = True
        '
        'tpMetaData
        '
        Me.tpMetaData.BackColor = System.Drawing.SystemColors.Control
        Me.tpMetaData.Controls.Add(Me.pnlFileInfo)
        Me.tpMetaData.Location = New System.Drawing.Point(4, 22)
        Me.tpMetaData.Name = "tpMetaData"
        Me.tpMetaData.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMetaData.Size = New System.Drawing.Size(1276, 698)
        Me.tpMetaData.TabIndex = 5
        Me.tpMetaData.Text = "Meta Data"
        '
        'pnlFileInfo
        '
        Me.pnlFileInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFileInfo.Location = New System.Drawing.Point(3, 3)
        Me.pnlFileInfo.Name = "pnlFileInfo"
        Me.pnlFileInfo.Size = New System.Drawing.Size(1270, 692)
        Me.pnlFileInfo.TabIndex = 0
        '
        'dlgEdit_TVEpisode
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(1284, 831)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.StatusStrip)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgEdit_TVEpisode"
        Me.Text = "Edit Episode"
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
        CType(Me.dgvUniqueIds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvRatings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpCastCrew.ResumeLayout(False)
        Me.tpCastCrew.PerformLayout()
        Me.tblCastCrew.ResumeLayout(False)
        Me.tblCastCrew.PerformLayout()
        CType(Me.dgvDirectors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCredits, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlTop As Panel
    Friend WithEvents tblTop As TableLayoutPanel
    Friend WithEvents pbTopLogo As PictureBox
    Friend WithEvents lblTopDetails As Label
    Friend WithEvents lblTopTitle As Label
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents tblBottom As TableLayoutPanel
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnChange As Button
    Friend WithEvents btnRescrape As Button
    Friend WithEvents chkLocked As CheckBox
    Friend WithEvents chkMarked As CheckBox
    Friend WithEvents ofdLocalFiles As OpenFileDialog
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents tsslFilename As ToolStripStatusLabel
    Friend WithEvents tsslSpring As ToolStripStatusLabel
    Friend WithEvents tsslStatus As ToolStripStatusLabel
    Friend WithEvents tspbStatus As ToolStripProgressBar
    Friend WithEvents tmrDelay As Timer
    Friend WithEvents pnlMain As Panel
    Friend WithEvents tcEdit As TabControl
    Friend WithEvents tpDetails As TabPage
    Friend WithEvents tblDetails As TableLayoutPanel
    Friend WithEvents lblDateAdded As Label
    Friend WithEvents lblVideoSource As Label
    Friend WithEvents lblTitle As Label
    Friend WithEvents txtTitle As TextBox
    Friend WithEvents dtpDateAdded_Date As DateTimePicker
    Friend WithEvents chkWatched As CheckBox
    Friend WithEvents dtpLastPlayed_Date As DateTimePicker
    Friend WithEvents lblSeason As Label
    Friend WithEvents txtSeason As TextBox
    Friend WithEvents lblPlot As Label
    Friend WithEvents txtPlot As TextBox
    Friend WithEvents lblAired As Label
    Friend WithEvents dtpAired As DateTimePicker
    Friend WithEvents lblRuntime As Label
    Friend WithEvents txtRuntime As TextBox
    Friend WithEvents lblUserRating As Label
    Friend WithEvents lblUniqueIds As Label
    Friend WithEvents dgvUniqueIds As DataGridView
    Friend WithEvents cbUserRating As ComboBox
    Friend WithEvents lblRatings As Label
    Friend WithEvents dtpLastPlayed_Time As DateTimePicker
    Friend WithEvents dgvRatings As DataGridView
    Friend WithEvents colRatingsDefault As DataGridViewCheckBoxColumn
    Friend WithEvents colRatingsSource As DataGridViewTextBoxColumn
    Friend WithEvents colRatingsValue As DataGridViewTextBoxColumn
    Friend WithEvents colRatingsMax As DataGridViewTextBoxColumn
    Friend WithEvents colRatingsVotes As DataGridViewTextBoxColumn
    Friend WithEvents dtpDateAdded_Time As DateTimePicker
    Friend WithEvents lblEpisode As Label
    Friend WithEvents txtEpisode As TextBox
    Friend WithEvents lblDisplaySeason As Label
    Friend WithEvents lblDisplayEpisode As Label
    Friend WithEvents txtDisplaySeason As TextBox
    Friend WithEvents txtDisplayEpisode As TextBox
    Friend WithEvents cbVideosource As ComboBox
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
    Friend WithEvents dgvDirectors As DataGridView
    Friend WithEvents colDirectorsName As DataGridViewTextBoxColumn
    Friend WithEvents dgvCredits As DataGridView
    Friend WithEvents colCreditsName As DataGridViewTextBoxColumn
    Friend WithEvents lblCredits As Label
    Friend WithEvents lblDirectors As Label
    Friend WithEvents lblGuestStars As Label
    Friend WithEvents btnGuestStarsAdd As Button
    Friend WithEvents btnGuestStarsRemove As Button
    Friend WithEvents btnGuestStarsUp As Button
    Friend WithEvents btnGuestStarsDown As Button
    Friend WithEvents btnGuestStarsEdit As Button
    Friend WithEvents lvGuestStars As ListView
    Friend WithEvents colGuestStarsID As ColumnHeader
    Friend WithEvents colGuestStarsName As ColumnHeader
    Friend WithEvents colGuestStarsRole As ColumnHeader
    Friend WithEvents colGuestStarsThumb As ColumnHeader
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
    Friend WithEvents colSubtitlesHeader1 As ColumnHeader
    Friend WithEvents colSubtitlesHeader2 As ColumnHeader
    Friend WithEvents colSubtitlesHeader3 As ColumnHeader
    Friend WithEvents colSubtitlesHeader4 As ColumnHeader
    Friend WithEvents colSubtitlesHeader6 As ColumnHeader
    Friend WithEvents tpImages As TabPage
    Friend WithEvents tblImages As TableLayoutPanel
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
    Friend WithEvents tpFrameExtraction As TabPage
    Friend WithEvents tblFrameExtraction As TableLayoutPanel
    Friend WithEvents pbFrame As PictureBox
    Friend WithEvents btnFrameSaveAsPoster As Button
    Friend WithEvents tbFrame As TrackBar
    Friend WithEvents btnFrameSaveAsFanart As Button
    Friend WithEvents lblTime As Label
    Friend WithEvents btnFrameLoadVideo As Button
    Friend WithEvents tpMetaData As TabPage
    Friend WithEvents pnlFileInfo As Panel
    Friend WithEvents colUniqueIdsDefault As DataGridViewCheckBoxColumn
    Friend WithEvents colUniqueIdsType As DataGridViewTextBoxColumn
    Friend WithEvents colUniqueIdsValue As DataGridViewTextBoxColumn
    Friend WithEvents lblUserNote As Label
    Friend WithEvents txtUserNote As TextBox
End Class
