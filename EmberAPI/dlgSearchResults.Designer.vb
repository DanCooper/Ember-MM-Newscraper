<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgSearchResults
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgSearchResults))
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pbPoster = New System.Windows.Forms.PictureBox()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.lblPremiered = New System.Windows.Forms.Label()
        Me.lblDirectors = New System.Windows.Forms.Label()
        Me.lblGenres = New System.Windows.Forms.Label()
        Me.txtUniqueId = New System.Windows.Forms.TextBox()
        Me.chkUniqueId = New System.Windows.Forms.CheckBox()
        Me.lblUniqueId = New System.Windows.Forms.Label()
        Me.lblPremieredHeader = New System.Windows.Forms.Label()
        Me.lblDirectorsHeader = New System.Windows.Forms.Label()
        Me.lblGenresHeader = New System.Windows.Forms.Label()
        Me.lblUniqueIdHeader = New System.Windows.Forms.Label()
        Me.lblPlotHeader = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnOpenFolder = New System.Windows.Forms.Button()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.ssStatus = New System.Windows.Forms.StatusStrip()
        Me.tsslSpring = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tspbStatus = New System.Windows.Forms.ToolStripProgressBar()
        Me.pnlMainRight = New System.Windows.Forms.Panel()
        Me.tblMainRight = New System.Windows.Forms.TableLayoutPanel()
        Me.lblOriginalTitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblTagline = New System.Windows.Forms.Label()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnNextScraper = New System.Windows.Forms.Button()
        Me.pnlMainLeft = New System.Windows.Forms.Panel()
        Me.tblMainLeft = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvSearchResults = New System.Windows.Forms.DataGridView()
        Me.colTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colYear = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlMainTop = New System.Windows.Forms.Panel()
        Me.tblMainTop = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ssStatus.SuspendLayout()
        Me.pnlMainRight.SuspendLayout()
        Me.tblMainRight.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.pnlMainLeft.SuspendLayout()
        Me.tblMainLeft.SuspendLayout()
        CType(Me.dgvSearchResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMainTop.SuspendLayout()
        Me.tblMainTop.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.tblMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOk
        '
        Me.btnOk.AutoSize = True
        Me.btnOk.Enabled = False
        Me.btnOk.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.Location = New System.Drawing.Point(841, 3)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(67, 23)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(914, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(67, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'pbPoster
        '
        Me.pbPoster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbPoster.Location = New System.Drawing.Point(3, 3)
        Me.pbPoster.Name = "pbPoster"
        Me.tblMainRight.SetRowSpan(Me.pbPoster, 9)
        Me.pbPoster.Size = New System.Drawing.Size(150, 225)
        Me.pbPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbPoster.TabIndex = 2
        Me.pbPoster.TabStop = False
        '
        'txtPlot
        '
        Me.txtPlot.BackColor = System.Drawing.SystemColors.Control
        Me.tblMainRight.SetColumnSpan(Me.txtPlot, 3)
        Me.txtPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlot.Location = New System.Drawing.Point(3, 254)
        Me.txtPlot.Multiline = True
        Me.txtPlot.Name = "txtPlot"
        Me.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlot.Size = New System.Drawing.Size(520, 148)
        Me.txtPlot.TabIndex = 22
        Me.txtPlot.TabStop = False
        '
        'lblPremiered
        '
        Me.lblPremiered.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPremiered.AutoSize = True
        Me.lblPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPremiered.Location = New System.Drawing.Point(227, 87)
        Me.lblPremiered.Name = "lblPremiered"
        Me.lblPremiered.Size = New System.Drawing.Size(31, 13)
        Me.lblPremiered.TabIndex = 13
        Me.lblPremiered.Text = "0000"
        Me.lblPremiered.UseMnemonic = False
        '
        'lblDirectors
        '
        Me.lblDirectors.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblDirectors.AutoSize = True
        Me.lblDirectors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDirectors.Location = New System.Drawing.Point(227, 107)
        Me.lblDirectors.Name = "lblDirectors"
        Me.lblDirectors.Size = New System.Drawing.Size(53, 13)
        Me.lblDirectors.TabIndex = 15
        Me.lblDirectors.Text = "Directors"
        Me.lblDirectors.UseMnemonic = False
        '
        'lblGenres
        '
        Me.lblGenres.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGenres.AutoSize = True
        Me.lblGenres.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGenres.Location = New System.Drawing.Point(227, 127)
        Me.lblGenres.Name = "lblGenres"
        Me.lblGenres.Size = New System.Drawing.Size(43, 13)
        Me.lblGenres.TabIndex = 17
        Me.lblGenres.Text = "Genres"
        Me.lblGenres.UseMnemonic = False
        '
        'txtUniqueId
        '
        Me.txtUniqueId.Enabled = False
        Me.txtUniqueId.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUniqueId.Location = New System.Drawing.Point(290, 31)
        Me.txtUniqueId.Name = "txtUniqueId"
        Me.txtUniqueId.Size = New System.Drawing.Size(80, 22)
        Me.txtUniqueId.TabIndex = 7
        '
        'chkUniqueId
        '
        Me.chkUniqueId.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkUniqueId.AutoSize = True
        Me.chkUniqueId.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUniqueId.Location = New System.Drawing.Point(3, 33)
        Me.chkUniqueId.Name = "chkUniqueId"
        Me.chkUniqueId.Size = New System.Drawing.Size(214, 17)
        Me.chkUniqueId.TabIndex = 6
        Me.chkUniqueId.Text = "Unique ID (IMDb, TMDb or TVDb ID):"
        Me.chkUniqueId.UseVisualStyleBackColor = True
        '
        'lblUniqueId
        '
        Me.lblUniqueId.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblUniqueId.AutoSize = True
        Me.lblUniqueId.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUniqueId.Location = New System.Drawing.Point(227, 147)
        Me.lblUniqueId.Name = "lblUniqueId"
        Me.lblUniqueId.Size = New System.Drawing.Size(59, 13)
        Me.lblUniqueId.TabIndex = 20
        Me.lblUniqueId.Text = "Unique ID"
        Me.lblUniqueId.UseMnemonic = False
        '
        'lblPremieredHeader
        '
        Me.lblPremieredHeader.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPremieredHeader.AutoSize = True
        Me.lblPremieredHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPremieredHeader.Location = New System.Drawing.Point(159, 87)
        Me.lblPremieredHeader.Name = "lblPremieredHeader"
        Me.lblPremieredHeader.Size = New System.Drawing.Size(61, 13)
        Me.lblPremieredHeader.TabIndex = 12
        Me.lblPremieredHeader.Text = "Premiered:"
        '
        'lblDirectorsHeader
        '
        Me.lblDirectorsHeader.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblDirectorsHeader.AutoSize = True
        Me.lblDirectorsHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblDirectorsHeader.Location = New System.Drawing.Point(159, 107)
        Me.lblDirectorsHeader.Name = "lblDirectorsHeader"
        Me.lblDirectorsHeader.Size = New System.Drawing.Size(56, 13)
        Me.lblDirectorsHeader.TabIndex = 14
        Me.lblDirectorsHeader.Text = "Directors:"
        '
        'lblGenresHeader
        '
        Me.lblGenresHeader.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGenresHeader.AutoSize = True
        Me.lblGenresHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblGenresHeader.Location = New System.Drawing.Point(159, 127)
        Me.lblGenresHeader.Name = "lblGenresHeader"
        Me.lblGenresHeader.Size = New System.Drawing.Size(46, 13)
        Me.lblGenresHeader.TabIndex = 16
        Me.lblGenresHeader.Text = "Genres:"
        '
        'lblUniqueIdHeader
        '
        Me.lblUniqueIdHeader.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblUniqueIdHeader.AutoSize = True
        Me.lblUniqueIdHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblUniqueIdHeader.Location = New System.Drawing.Point(159, 147)
        Me.lblUniqueIdHeader.Name = "lblUniqueIdHeader"
        Me.lblUniqueIdHeader.Size = New System.Drawing.Size(62, 13)
        Me.lblUniqueIdHeader.TabIndex = 19
        Me.lblUniqueIdHeader.Text = "Unique ID:"
        '
        'lblPlotHeader
        '
        Me.lblPlotHeader.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPlotHeader.AutoSize = True
        Me.tblMainRight.SetColumnSpan(Me.lblPlotHeader, 3)
        Me.lblPlotHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPlotHeader.Location = New System.Drawing.Point(3, 234)
        Me.lblPlotHeader.Name = "lblPlotHeader"
        Me.lblPlotHeader.Size = New System.Drawing.Size(31, 13)
        Me.lblPlotHeader.TabIndex = 21
        Me.lblPlotHeader.Text = "Plot:"
        '
        'btnSearch
        '
        Me.btnSearch.AutoSize = True
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(376, 3)
        Me.btnSearch.Name = "btnSearch"
        Me.tblMainLeft.SetRowSpan(Me.btnSearch, 2)
        Me.btnSearch.Size = New System.Drawing.Size(67, 50)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "Search"
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Location = New System.Drawing.Point(3, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(281, 22)
        Me.txtSearch.TabIndex = 2
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.Location = New System.Drawing.Point(952, 3)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.Size = New System.Drawing.Size(29, 23)
        Me.btnOpenFolder.TabIndex = 25
        Me.btnOpenFolder.Text = "..."
        Me.btnOpenFolder.UseVisualStyleBackColor = True
        '
        'txtFileName
        '
        Me.txtFileName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFileName.Enabled = False
        Me.txtFileName.Location = New System.Drawing.Point(3, 3)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(943, 22)
        Me.txtFileName.TabIndex = 24
        '
        'txtYear
        '
        Me.txtYear.Location = New System.Drawing.Point(290, 3)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(80, 22)
        Me.txtYear.TabIndex = 3
        '
        'ssStatus
        '
        Me.ssStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslSpring, Me.tsslStatus, Me.tspbStatus})
        Me.ssStatus.Location = New System.Drawing.Point(0, 469)
        Me.ssStatus.Name = "ssStatus"
        Me.ssStatus.Size = New System.Drawing.Size(984, 22)
        Me.ssStatus.TabIndex = 26
        '
        'tsslSpring
        '
        Me.tsslSpring.Name = "tsslSpring"
        Me.tsslSpring.Size = New System.Drawing.Size(828, 17)
        Me.tsslSpring.Spring = True
        '
        'tsslStatus
        '
        Me.tsslStatus.Name = "tsslStatus"
        Me.tsslStatus.Size = New System.Drawing.Size(39, 17)
        Me.tsslStatus.Text = "Status"
        Me.tsslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tspbStatus
        '
        Me.tspbStatus.Name = "tspbStatus"
        Me.tspbStatus.Size = New System.Drawing.Size(100, 16)
        Me.tspbStatus.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        '
        'pnlMainRight
        '
        Me.pnlMainRight.AutoSize = True
        Me.pnlMainRight.Controls.Add(Me.tblMainRight)
        Me.pnlMainRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMainRight.Location = New System.Drawing.Point(455, 3)
        Me.pnlMainRight.Name = "pnlMainRight"
        Me.pnlMainRight.Size = New System.Drawing.Size(526, 405)
        Me.pnlMainRight.TabIndex = 27
        '
        'tblMainRight
        '
        Me.tblMainRight.AutoSize = True
        Me.tblMainRight.ColumnCount = 3
        Me.tblMainRight.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainRight.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainRight.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainRight.Controls.Add(Me.pbPoster, 0, 0)
        Me.tblMainRight.Controls.Add(Me.lblDirectorsHeader, 1, 5)
        Me.tblMainRight.Controls.Add(Me.lblPremieredHeader, 1, 4)
        Me.tblMainRight.Controls.Add(Me.lblPremiered, 2, 4)
        Me.tblMainRight.Controls.Add(Me.lblDirectors, 2, 5)
        Me.tblMainRight.Controls.Add(Me.lblGenresHeader, 1, 6)
        Me.tblMainRight.Controls.Add(Me.lblGenres, 2, 6)
        Me.tblMainRight.Controls.Add(Me.lblUniqueIdHeader, 1, 7)
        Me.tblMainRight.Controls.Add(Me.lblUniqueId, 2, 7)
        Me.tblMainRight.Controls.Add(Me.txtPlot, 0, 10)
        Me.tblMainRight.Controls.Add(Me.lblPlotHeader, 0, 9)
        Me.tblMainRight.Controls.Add(Me.lblOriginalTitle, 1, 1)
        Me.tblMainRight.Controls.Add(Me.lblTitle, 1, 0)
        Me.tblMainRight.Controls.Add(Me.lblTagline, 1, 2)
        Me.tblMainRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMainRight.Location = New System.Drawing.Point(0, 0)
        Me.tblMainRight.Name = "tblMainRight"
        Me.tblMainRight.RowCount = 11
        Me.tblMainRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.tblMainRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMainRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMainRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMainRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMainRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMainRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMainRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMainRight.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMainRight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMainRight.Size = New System.Drawing.Size(526, 405)
        Me.tblMainRight.TabIndex = 0
        '
        'lblOriginalTitle
        '
        Me.lblOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblOriginalTitle.AutoSize = True
        Me.tblMainRight.SetColumnSpan(Me.lblOriginalTitle, 2)
        Me.lblOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblOriginalTitle.Location = New System.Drawing.Point(159, 27)
        Me.lblOriginalTitle.Name = "lblOriginalTitle"
        Me.lblOriginalTitle.Size = New System.Drawing.Size(74, 13)
        Me.lblOriginalTitle.TabIndex = 10
        Me.lblOriginalTitle.Text = "Original Title"
        Me.lblOriginalTitle.UseMnemonic = False
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTitle.AutoSize = True
        Me.tblMainRight.SetColumnSpan(Me.lblTitle, 2)
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(159, 3)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(76, 17)
        Me.lblTitle.TabIndex = 24
        Me.lblTitle.Text = "Title (Year)"
        '
        'lblTagline
        '
        Me.lblTagline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTagline.AutoSize = True
        Me.tblMainRight.SetColumnSpan(Me.lblTagline, 2)
        Me.lblTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTagline.Location = New System.Drawing.Point(159, 47)
        Me.lblTagline.Name = "lblTagline"
        Me.lblTagline.Size = New System.Drawing.Size(44, 13)
        Me.lblTagline.TabIndex = 10
        Me.lblTagline.Text = "Tagline"
        Me.lblTagline.UseMnemonic = False
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 440)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(984, 29)
        Me.pnlBottom.TabIndex = 28
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 5
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnOk, 3, 0)
        Me.tblBottom.Controls.Add(Me.btnCancel, 4, 0)
        Me.tblBottom.Controls.Add(Me.btnNextScraper, 1, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.Size = New System.Drawing.Size(984, 29)
        Me.tblBottom.TabIndex = 0
        '
        'btnNextScraper
        '
        Me.btnNextScraper.AutoSize = True
        Me.btnNextScraper.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNextScraper.Location = New System.Drawing.Point(734, 3)
        Me.btnNextScraper.Name = "btnNextScraper"
        Me.btnNextScraper.Size = New System.Drawing.Size(81, 23)
        Me.btnNextScraper.TabIndex = 0
        Me.btnNextScraper.Text = "Next Scraper"
        '
        'pnlMainLeft
        '
        Me.pnlMainLeft.AutoSize = True
        Me.pnlMainLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlMainLeft.Controls.Add(Me.tblMainLeft)
        Me.pnlMainLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMainLeft.Location = New System.Drawing.Point(3, 3)
        Me.pnlMainLeft.Name = "pnlMainLeft"
        Me.pnlMainLeft.Size = New System.Drawing.Size(446, 405)
        Me.pnlMainLeft.TabIndex = 29
        '
        'tblMainLeft
        '
        Me.tblMainLeft.AutoSize = True
        Me.tblMainLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblMainLeft.ColumnCount = 3
        Me.tblMainLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMainLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainLeft.Controls.Add(Me.btnSearch, 2, 0)
        Me.tblMainLeft.Controls.Add(Me.txtYear, 1, 0)
        Me.tblMainLeft.Controls.Add(Me.txtSearch, 0, 0)
        Me.tblMainLeft.Controls.Add(Me.dgvSearchResults, 0, 2)
        Me.tblMainLeft.Controls.Add(Me.chkUniqueId, 0, 1)
        Me.tblMainLeft.Controls.Add(Me.txtUniqueId, 1, 1)
        Me.tblMainLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMainLeft.Location = New System.Drawing.Point(0, 0)
        Me.tblMainLeft.Name = "tblMainLeft"
        Me.tblMainLeft.RowCount = 3
        Me.tblMainLeft.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainLeft.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMainLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMainLeft.Size = New System.Drawing.Size(446, 405)
        Me.tblMainLeft.TabIndex = 0
        '
        'dgvSearchResults
        '
        Me.dgvSearchResults.AllowUserToAddRows = False
        Me.dgvSearchResults.AllowUserToDeleteRows = False
        Me.dgvSearchResults.AllowUserToResizeColumns = False
        Me.dgvSearchResults.AllowUserToResizeRows = False
        Me.dgvSearchResults.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvSearchResults.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvSearchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSearchResults.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTitle, Me.colYear})
        Me.tblMainLeft.SetColumnSpan(Me.dgvSearchResults, 3)
        Me.dgvSearchResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSearchResults.Location = New System.Drawing.Point(3, 59)
        Me.dgvSearchResults.MultiSelect = False
        Me.dgvSearchResults.Name = "dgvSearchResults"
        Me.dgvSearchResults.ReadOnly = True
        Me.dgvSearchResults.RowHeadersVisible = False
        Me.dgvSearchResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvSearchResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSearchResults.Size = New System.Drawing.Size(440, 343)
        Me.dgvSearchResults.TabIndex = 9
        '
        'colTitle
        '
        Me.colTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colTitle.HeaderText = "Title"
        Me.colTitle.MinimumWidth = 50
        Me.colTitle.Name = "colTitle"
        Me.colTitle.ReadOnly = True
        '
        'colYear
        '
        Me.colYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colYear.HeaderText = "Year"
        Me.colYear.Name = "colYear"
        Me.colYear.ReadOnly = True
        Me.colYear.Width = 52
        '
        'pnlMainTop
        '
        Me.pnlMainTop.AutoSize = True
        Me.pnlMainTop.Controls.Add(Me.tblMainTop)
        Me.pnlMainTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMainTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlMainTop.Name = "pnlMainTop"
        Me.pnlMainTop.Size = New System.Drawing.Size(984, 29)
        Me.pnlMainTop.TabIndex = 30
        '
        'tblMainTop
        '
        Me.tblMainTop.AutoSize = True
        Me.tblMainTop.ColumnCount = 2
        Me.tblMainTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMainTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainTop.Controls.Add(Me.txtFileName, 0, 0)
        Me.tblMainTop.Controls.Add(Me.btnOpenFolder, 1, 0)
        Me.tblMainTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMainTop.Location = New System.Drawing.Point(0, 0)
        Me.tblMainTop.Name = "tblMainTop"
        Me.tblMainTop.RowCount = 1
        Me.tblMainTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainTop.Size = New System.Drawing.Size(984, 29)
        Me.tblMainTop.TabIndex = 0
        '
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 29)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(984, 411)
        Me.pnlMain.TabIndex = 31
        '
        'tblMain
        '
        Me.tblMain.AutoSize = True
        Me.tblMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblMain.ColumnCount = 2
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.Controls.Add(Me.pnlMainLeft, 0, 0)
        Me.tblMain.Controls.Add(Me.pnlMainRight, 1, 0)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.RowCount = 1
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.Size = New System.Drawing.Size(984, 411)
        Me.tblMain.TabIndex = 0
        '
        'dlgSearchResults
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(984, 491)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlMainTop)
        Me.Controls.Add(Me.ssStatus)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(598, 500)
        Me.Name = "dlgSearchResults"
        Me.Text = "Search Results"
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ssStatus.ResumeLayout(False)
        Me.ssStatus.PerformLayout()
        Me.pnlMainRight.ResumeLayout(False)
        Me.pnlMainRight.PerformLayout()
        Me.tblMainRight.ResumeLayout(False)
        Me.tblMainRight.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.tblBottom.PerformLayout()
        Me.pnlMainLeft.ResumeLayout(False)
        Me.pnlMainLeft.PerformLayout()
        Me.tblMainLeft.ResumeLayout(False)
        Me.tblMainLeft.PerformLayout()
        CType(Me.dgvSearchResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMainTop.ResumeLayout(False)
        Me.pnlMainTop.PerformLayout()
        Me.tblMainTop.ResumeLayout(False)
        Me.tblMainTop.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.tblMain.ResumeLayout(False)
        Me.tblMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pbPoster As System.Windows.Forms.PictureBox
    Friend WithEvents txtPlot As System.Windows.Forms.TextBox
    Friend WithEvents lblPremiered As System.Windows.Forms.Label
    Friend WithEvents lblDirectors As System.Windows.Forms.Label
    Friend WithEvents lblGenres As System.Windows.Forms.Label
    Friend WithEvents txtUniqueId As System.Windows.Forms.TextBox
    Friend WithEvents chkUniqueId As System.Windows.Forms.CheckBox
    Friend WithEvents lblUniqueId As System.Windows.Forms.Label
    Friend WithEvents lblPremieredHeader As System.Windows.Forms.Label
    Friend WithEvents lblDirectorsHeader As System.Windows.Forms.Label
    Friend WithEvents lblGenresHeader As System.Windows.Forms.Label
    Friend WithEvents lblUniqueIdHeader As System.Windows.Forms.Label
    Friend WithEvents lblPlotHeader As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnOpenFolder As System.Windows.Forms.Button
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents ssStatus As StatusStrip
    Friend WithEvents tsslStatus As ToolStripStatusLabel
    Friend WithEvents tspbStatus As ToolStripProgressBar
    Friend WithEvents tsslSpring As ToolStripStatusLabel
    Friend WithEvents pnlMainRight As Panel
    Friend WithEvents tblMainRight As TableLayoutPanel
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents tblBottom As TableLayoutPanel
    Friend WithEvents tblMainLeft As TableLayoutPanel
    Friend WithEvents pnlMainLeft As Panel
    Friend WithEvents pnlMainTop As Panel
    Friend WithEvents tblMainTop As TableLayoutPanel
    Friend WithEvents pnlMain As Panel
    Friend WithEvents tblMain As TableLayoutPanel
    Friend WithEvents lblOriginalTitle As Label
    Friend WithEvents dgvSearchResults As DataGridView
    Friend WithEvents colTitle As DataGridViewTextBoxColumn
    Friend WithEvents colYear As DataGridViewTextBoxColumn
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblTagline As Label
    Friend WithEvents btnNextScraper As Button
End Class
