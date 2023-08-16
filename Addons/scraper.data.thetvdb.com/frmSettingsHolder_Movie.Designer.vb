<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettingsHolder_Movie
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettingsHolder_Movie))
        Me.gbScraperOpts = New System.Windows.Forms.GroupBox()
        Me.tblScraperOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.pbApiPinInfo = New System.Windows.Forms.PictureBox()
        Me.chkFallBackEng = New System.Windows.Forms.CheckBox()
        Me.txtApiPin = New System.Windows.Forms.TextBox()
        Me.lblApiPin = New System.Windows.Forms.Label()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.lblScraperOrder = New System.Windows.Forms.Label()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbScraperFieldsOpts = New System.Windows.Forms.GroupBox()
        Me.tblScraperFieldsOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkActors = New System.Windows.Forms.CheckBox()
        Me.chkCountries = New System.Windows.Forms.CheckBox()
        Me.chkOriginalTitle = New System.Windows.Forms.CheckBox()
        Me.chkGenres = New System.Windows.Forms.CheckBox()
        Me.chkDirectors = New System.Windows.Forms.CheckBox()
        Me.chkPlot = New System.Windows.Forms.CheckBox()
        Me.chkRuntime = New System.Windows.Forms.CheckBox()
        Me.chkStudios = New System.Windows.Forms.CheckBox()
        Me.chkTagline = New System.Windows.Forms.CheckBox()
        Me.chkTitle = New System.Windows.Forms.CheckBox()
        Me.chkWriters = New System.Windows.Forms.CheckBox()
        Me.chkTrailer = New System.Windows.Forms.CheckBox()
        Me.chkPremiered = New System.Windows.Forms.CheckBox()
        Me.chkCertifications = New System.Windows.Forms.CheckBox()
        Me.pnlSettingsBottom = New System.Windows.Forms.Panel()
        Me.tblSettingsBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.pbIconBottom = New System.Windows.Forms.PictureBox()
        Me.lblInfoBottom = New System.Windows.Forms.Label()
        Me.gbScraperOpts.SuspendLayout()
        Me.tblScraperOpts.SuspendLayout()
        CType(Me.pbApiPinInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
        Me.gbScraperFieldsOpts.SuspendLayout()
        Me.tblScraperFieldsOpts.SuspendLayout()
        Me.pnlSettingsBottom.SuspendLayout()
        Me.tblSettingsBottom.SuspendLayout()
        CType(Me.pbIconBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gbScraperOpts
        '
        Me.gbScraperOpts.AutoSize = True
        Me.gbScraperOpts.Controls.Add(Me.tblScraperOpts)
        Me.gbScraperOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbScraperOpts.Location = New System.Drawing.Point(3, 145)
        Me.gbScraperOpts.Name = "gbScraperOpts"
        Me.gbScraperOpts.Size = New System.Drawing.Size(346, 72)
        Me.gbScraperOpts.TabIndex = 1
        Me.gbScraperOpts.TabStop = False
        Me.gbScraperOpts.Text = "Scraper Options"
        '
        'tblScraperOpts
        '
        Me.tblScraperOpts.AutoSize = True
        Me.tblScraperOpts.ColumnCount = 4
        Me.tblScraperOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperOpts.Controls.Add(Me.pbApiPinInfo, 2, 0)
        Me.tblScraperOpts.Controls.Add(Me.chkFallBackEng, 0, 1)
        Me.tblScraperOpts.Controls.Add(Me.txtApiPin, 1, 0)
        Me.tblScraperOpts.Controls.Add(Me.lblApiPin, 0, 0)
        Me.tblScraperOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperOpts.Name = "tblScraperOpts"
        Me.tblScraperOpts.RowCount = 3
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.Size = New System.Drawing.Size(340, 51)
        Me.tblScraperOpts.TabIndex = 98
        '
        'pbApiPinInfo
        '
        Me.pbApiPinInfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.pbApiPinInfo.Image = CType(resources.GetObject("pbApiPinInfo.Image"), System.Drawing.Image)
        Me.pbApiPinInfo.Location = New System.Drawing.Point(321, 6)
        Me.pbApiPinInfo.Name = "pbApiPinInfo"
        Me.pbApiPinInfo.Size = New System.Drawing.Size(16, 16)
        Me.pbApiPinInfo.TabIndex = 5
        Me.pbApiPinInfo.TabStop = False
        '
        'chkFallBackEng
        '
        Me.chkFallBackEng.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFallBackEng.AutoSize = True
        Me.tblScraperOpts.SetColumnSpan(Me.chkFallBackEng, 3)
        Me.chkFallBackEng.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFallBackEng.Location = New System.Drawing.Point(3, 31)
        Me.chkFallBackEng.Name = "chkFallBackEng"
        Me.chkFallBackEng.Size = New System.Drawing.Size(123, 17)
        Me.chkFallBackEng.TabIndex = 4
        Me.chkFallBackEng.Text = "Fallback to english"
        Me.chkFallBackEng.UseVisualStyleBackColor = True
        '
        'txtApiPin
        '
        Me.txtApiPin.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtApiPin.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApiPin.Location = New System.Drawing.Point(85, 3)
        Me.txtApiPin.Name = "txtApiPin"
        Me.txtApiPin.Size = New System.Drawing.Size(230, 22)
        Me.txtApiPin.TabIndex = 1
        '
        'lblApiPin
        '
        Me.lblApiPin.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblApiPin.AutoSize = True
        Me.lblApiPin.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApiPin.Location = New System.Drawing.Point(3, 7)
        Me.lblApiPin.Name = "lblApiPin"
        Me.lblApiPin.Size = New System.Drawing.Size(76, 13)
        Me.lblApiPin.TabIndex = 0
        Me.lblApiPin.Text = "TVDb API Pin:"
        '
        'chkEnabled
        '
        Me.chkEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnabled.Location = New System.Drawing.Point(8, 6)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(68, 17)
        Me.chkEnabled.TabIndex = 0
        Me.chkEnabled.Text = "Enabled"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(473, 29)
        Me.pnlSettingsTop.TabIndex = 0
        '
        'tblSettingsTop
        '
        Me.tblSettingsTop.AutoSize = True
        Me.tblSettingsTop.ColumnCount = 5
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.Controls.Add(Me.btnDown, 4, 0)
        Me.tblSettingsTop.Controls.Add(Me.lblScraperOrder, 2, 0)
        Me.tblSettingsTop.Controls.Add(Me.btnUp, 3, 0)
        Me.tblSettingsTop.Controls.Add(Me.chkEnabled, 0, 0)
        Me.tblSettingsTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsTop.Name = "tblSettingsTop"
        Me.tblSettingsTop.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.tblSettingsTop.RowCount = 2
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.Size = New System.Drawing.Size(473, 29)
        Me.tblSettingsTop.TabIndex = 98
        '
        'btnDown
        '
        Me.btnDown.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(447, 3)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(23, 23)
        Me.btnDown.TabIndex = 3
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'lblScraperOrder
        '
        Me.lblScraperOrder.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblScraperOrder.AutoSize = True
        Me.lblScraperOrder.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScraperOrder.Location = New System.Drawing.Point(354, 8)
        Me.lblScraperOrder.Name = "lblScraperOrder"
        Me.lblScraperOrder.Size = New System.Drawing.Size(58, 12)
        Me.lblScraperOrder.TabIndex = 1
        Me.lblScraperOrder.Text = "Scraper order"
        '
        'btnUp
        '
        Me.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(418, 3)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(23, 23)
        Me.btnUp.TabIndex = 2
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.Controls.Add(Me.pnlSettingsMain)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsBottom)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsTop)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(473, 405)
        Me.pnlSettings.TabIndex = 0
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 29)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(473, 339)
        Me.pnlSettingsMain.TabIndex = 98
        '
        'tblSettingsMain
        '
        Me.tblSettingsMain.AutoScroll = True
        Me.tblSettingsMain.AutoSize = True
        Me.tblSettingsMain.ColumnCount = 2
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.Controls.Add(Me.gbScraperFieldsOpts, 0, 0)
        Me.tblSettingsMain.Controls.Add(Me.gbScraperOpts, 0, 1)
        Me.tblSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsMain.Name = "tblSettingsMain"
        Me.tblSettingsMain.RowCount = 3
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.Size = New System.Drawing.Size(473, 339)
        Me.tblSettingsMain.TabIndex = 0
        '
        'gbScraperFieldsOpts
        '
        Me.gbScraperFieldsOpts.AutoSize = True
        Me.gbScraperFieldsOpts.Controls.Add(Me.tblScraperFieldsOpts)
        Me.gbScraperFieldsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperFieldsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbScraperFieldsOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbScraperFieldsOpts.Name = "gbScraperFieldsOpts"
        Me.gbScraperFieldsOpts.Size = New System.Drawing.Size(346, 136)
        Me.gbScraperFieldsOpts.TabIndex = 3
        Me.gbScraperFieldsOpts.TabStop = False
        Me.gbScraperFieldsOpts.Text = "Scraper Fields - Scraper specific"
        '
        'tblScraperFieldsOpts
        '
        Me.tblScraperFieldsOpts.AutoSize = True
        Me.tblScraperFieldsOpts.ColumnCount = 4
        Me.tblScraperFieldsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkActors, 0, 0)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkOriginalTitle, 1, 0)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkPlot, 1, 1)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkTagline, 2, 0)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkTitle, 2, 1)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkWriters, 2, 3)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkTrailer, 2, 2)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkPremiered, 1, 2)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkCertifications, 0, 1)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkCountries, 0, 2)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkDirectors, 0, 3)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkGenres, 0, 4)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkRuntime, 1, 3)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkStudios, 1, 4)
        Me.tblScraperFieldsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperFieldsOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperFieldsOpts.Name = "tblScraperFieldsOpts"
        Me.tblScraperFieldsOpts.RowCount = 6
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.Size = New System.Drawing.Size(340, 115)
        Me.tblScraperFieldsOpts.TabIndex = 98
        '
        'chkActors
        '
        Me.chkActors.AutoSize = True
        Me.chkActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkActors.Location = New System.Drawing.Point(3, 3)
        Me.chkActors.Name = "chkActors"
        Me.chkActors.Size = New System.Drawing.Size(58, 17)
        Me.chkActors.TabIndex = 12
        Me.chkActors.Text = "Actors"
        Me.chkActors.UseVisualStyleBackColor = True
        '
        'chkCountries
        '
        Me.chkCountries.AutoSize = True
        Me.chkCountries.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCountries.Location = New System.Drawing.Point(3, 49)
        Me.chkCountries.Name = "chkCountries"
        Me.chkCountries.Size = New System.Drawing.Size(76, 17)
        Me.chkCountries.TabIndex = 18
        Me.chkCountries.Text = "Countries"
        Me.chkCountries.UseVisualStyleBackColor = True
        '
        'chkOriginalTitle
        '
        Me.chkOriginalTitle.AutoSize = True
        Me.chkOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOriginalTitle.Location = New System.Drawing.Point(103, 3)
        Me.chkOriginalTitle.Name = "chkOriginalTitle"
        Me.chkOriginalTitle.Size = New System.Drawing.Size(93, 17)
        Me.chkOriginalTitle.TabIndex = 78
        Me.chkOriginalTitle.Text = "Original Title"
        Me.chkOriginalTitle.UseVisualStyleBackColor = True
        '
        'chkGenres
        '
        Me.chkGenres.AutoSize = True
        Me.chkGenres.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGenres.Location = New System.Drawing.Point(3, 95)
        Me.chkGenres.Name = "chkGenres"
        Me.chkGenres.Size = New System.Drawing.Size(62, 17)
        Me.chkGenres.TabIndex = 15
        Me.chkGenres.Text = "Genres"
        Me.chkGenres.UseVisualStyleBackColor = True
        '
        'chkDirectors
        '
        Me.chkDirectors.AutoSize = True
        Me.chkDirectors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDirectors.Location = New System.Drawing.Point(3, 72)
        Me.chkDirectors.Name = "chkDirectors"
        Me.chkDirectors.Size = New System.Drawing.Size(72, 17)
        Me.chkDirectors.TabIndex = 19
        Me.chkDirectors.Text = "Directors"
        Me.chkDirectors.UseVisualStyleBackColor = True
        '
        'chkPlot
        '
        Me.chkPlot.AutoSize = True
        Me.chkPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPlot.Location = New System.Drawing.Point(103, 26)
        Me.chkPlot.Name = "chkPlot"
        Me.chkPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkPlot.TabIndex = 10
        Me.chkPlot.Text = "Plot"
        Me.chkPlot.UseVisualStyleBackColor = True
        '
        'chkRuntime
        '
        Me.chkRuntime.AutoSize = True
        Me.chkRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRuntime.Location = New System.Drawing.Point(103, 72)
        Me.chkRuntime.Name = "chkRuntime"
        Me.chkRuntime.Size = New System.Drawing.Size(69, 17)
        Me.chkRuntime.TabIndex = 5
        Me.chkRuntime.Text = "Runtime"
        Me.chkRuntime.UseVisualStyleBackColor = True
        '
        'chkStudios
        '
        Me.chkStudios.AutoSize = True
        Me.chkStudios.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStudios.Location = New System.Drawing.Point(103, 95)
        Me.chkStudios.Name = "chkStudios"
        Me.chkStudios.Size = New System.Drawing.Size(65, 17)
        Me.chkStudios.TabIndex = 8
        Me.chkStudios.Text = "Studios"
        Me.chkStudios.UseVisualStyleBackColor = True
        '
        'chkTagline
        '
        Me.chkTagline.AutoSize = True
        Me.chkTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTagline.Location = New System.Drawing.Point(202, 3)
        Me.chkTagline.Name = "chkTagline"
        Me.chkTagline.Size = New System.Drawing.Size(63, 17)
        Me.chkTagline.TabIndex = 9
        Me.chkTagline.Text = "Tagline"
        Me.chkTagline.UseVisualStyleBackColor = True
        '
        'chkTitle
        '
        Me.chkTitle.AutoSize = True
        Me.chkTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTitle.Location = New System.Drawing.Point(202, 26)
        Me.chkTitle.Name = "chkTitle"
        Me.chkTitle.Size = New System.Drawing.Size(48, 17)
        Me.chkTitle.TabIndex = 0
        Me.chkTitle.Text = "Title"
        Me.chkTitle.UseVisualStyleBackColor = True
        '
        'chkWriters
        '
        Me.chkWriters.AutoSize = True
        Me.chkWriters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWriters.Location = New System.Drawing.Point(202, 72)
        Me.chkWriters.Name = "chkWriters"
        Me.chkWriters.Size = New System.Drawing.Size(63, 17)
        Me.chkWriters.TabIndex = 77
        Me.chkWriters.Text = "Writers"
        Me.chkWriters.UseVisualStyleBackColor = True
        '
        'chkTrailer
        '
        Me.chkTrailer.AutoSize = True
        Me.chkTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTrailer.Location = New System.Drawing.Point(202, 49)
        Me.chkTrailer.Name = "chkTrailer"
        Me.chkTrailer.Size = New System.Drawing.Size(57, 17)
        Me.chkTrailer.TabIndex = 17
        Me.chkTrailer.Text = "Trailer"
        Me.chkTrailer.UseVisualStyleBackColor = True
        '
        'chkPremiered
        '
        Me.chkPremiered.AutoSize = True
        Me.chkPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPremiered.Location = New System.Drawing.Point(103, 49)
        Me.chkPremiered.Name = "chkPremiered"
        Me.chkPremiered.Size = New System.Drawing.Size(77, 17)
        Me.chkPremiered.TabIndex = 4
        Me.chkPremiered.Text = "Premiered"
        Me.chkPremiered.UseVisualStyleBackColor = True
        '
        'chkCertifications
        '
        Me.chkCertifications.AutoSize = True
        Me.chkCertifications.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCertifications.Location = New System.Drawing.Point(3, 26)
        Me.chkCertifications.Name = "chkCertifications"
        Me.chkCertifications.Size = New System.Drawing.Size(94, 17)
        Me.chkCertifications.TabIndex = 2
        Me.chkCertifications.Text = "Certifications"
        Me.chkCertifications.UseVisualStyleBackColor = True
        '
        'pnlSettingsBottom
        '
        Me.pnlSettingsBottom.AutoSize = True
        Me.pnlSettingsBottom.Controls.Add(Me.tblSettingsBottom)
        Me.pnlSettingsBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlSettingsBottom.Location = New System.Drawing.Point(0, 368)
        Me.pnlSettingsBottom.Name = "pnlSettingsBottom"
        Me.pnlSettingsBottom.Size = New System.Drawing.Size(473, 37)
        Me.pnlSettingsBottom.TabIndex = 97
        '
        'tblSettingsBottom
        '
        Me.tblSettingsBottom.AutoSize = True
        Me.tblSettingsBottom.ColumnCount = 3
        Me.tblSettingsBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsBottom.Controls.Add(Me.pbIconBottom, 0, 0)
        Me.tblSettingsBottom.Controls.Add(Me.lblInfoBottom, 1, 0)
        Me.tblSettingsBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsBottom.Name = "tblSettingsBottom"
        Me.tblSettingsBottom.RowCount = 2
        Me.tblSettingsBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsBottom.Size = New System.Drawing.Size(473, 37)
        Me.tblSettingsBottom.TabIndex = 0
        '
        'pbIconBottom
        '
        Me.pbIconBottom.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pbIconBottom.Image = CType(resources.GetObject("pbIconBottom.Image"), System.Drawing.Image)
        Me.pbIconBottom.Location = New System.Drawing.Point(3, 3)
        Me.pbIconBottom.Name = "pbIconBottom"
        Me.pbIconBottom.Size = New System.Drawing.Size(30, 31)
        Me.pbIconBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconBottom.TabIndex = 96
        Me.pbIconBottom.TabStop = False
        '
        'lblInfoBottom
        '
        Me.lblInfoBottom.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblInfoBottom.AutoSize = True
        Me.lblInfoBottom.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblInfoBottom.ForeColor = System.Drawing.Color.Blue
        Me.lblInfoBottom.Location = New System.Drawing.Point(39, 6)
        Me.lblInfoBottom.Name = "lblInfoBottom"
        Me.lblInfoBottom.Size = New System.Drawing.Size(205, 24)
        Me.lblInfoBottom.TabIndex = 4
        Me.lblInfoBottom.Text = "These settings are specific to this module." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please refer to the global settings " &
    "for more options."
        Me.lblInfoBottom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmSettingsHolder_Movie
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(473, 405)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder_Movie"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
        Me.gbScraperOpts.ResumeLayout(False)
        Me.gbScraperOpts.PerformLayout()
        Me.tblScraperOpts.ResumeLayout(False)
        Me.tblScraperOpts.PerformLayout()
        CType(Me.pbApiPinInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSettingsTop.ResumeLayout(False)
        Me.pnlSettingsTop.PerformLayout()
        Me.tblSettingsTop.ResumeLayout(False)
        Me.tblSettingsTop.PerformLayout()
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.pnlSettingsMain.ResumeLayout(False)
        Me.pnlSettingsMain.PerformLayout()
        Me.tblSettingsMain.ResumeLayout(False)
        Me.tblSettingsMain.PerformLayout()
        Me.gbScraperFieldsOpts.ResumeLayout(False)
        Me.gbScraperFieldsOpts.PerformLayout()
        Me.tblScraperFieldsOpts.ResumeLayout(False)
        Me.tblScraperFieldsOpts.PerformLayout()
        Me.pnlSettingsBottom.ResumeLayout(False)
        Me.pnlSettingsBottom.PerformLayout()
        Me.tblSettingsBottom.ResumeLayout(False)
        Me.tblSettingsBottom.PerformLayout()
        CType(Me.pbIconBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gbScraperOpts As System.Windows.Forms.GroupBox
    Friend WithEvents lblApiPin As System.Windows.Forms.Label
    Friend WithEvents txtApiPin As System.Windows.Forms.TextBox
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents lblScraperOrder As System.Windows.Forms.Label
    Friend WithEvents lblInfoBottom As System.Windows.Forms.Label
    Friend WithEvents pbIconBottom As System.Windows.Forms.PictureBox
    Friend WithEvents gbScraperFieldsOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkCountries As System.Windows.Forms.CheckBox
    Friend WithEvents chkStudios As System.Windows.Forms.CheckBox
    Friend WithEvents chkRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkGenres As System.Windows.Forms.CheckBox
    Friend WithEvents chkTagline As System.Windows.Forms.CheckBox
    Friend WithEvents chkActors As System.Windows.Forms.CheckBox
    Friend WithEvents chkTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkPremiered As System.Windows.Forms.CheckBox
    Friend WithEvents chkCertifications As System.Windows.Forms.CheckBox
    Friend WithEvents chkTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkFallBackEng As System.Windows.Forms.CheckBox
    Friend WithEvents chkDirectors As System.Windows.Forms.CheckBox
    Friend WithEvents pbApiPinInfo As System.Windows.Forms.PictureBox
    Friend WithEvents chkWriters As System.Windows.Forms.CheckBox
    Friend WithEvents chkOriginalTitle As System.Windows.Forms.CheckBox
    Friend WithEvents tblSettingsBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsBottom As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblScraperFieldsOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblScraperOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
End Class
