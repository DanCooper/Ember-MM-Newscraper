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
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.lblScraperOrder = New System.Windows.Forms.Label()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbScraperFieldsOpts = New System.Windows.Forms.GroupBox()
        Me.tblScraperFieldsOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkActors = New System.Windows.Forms.CheckBox()
        Me.chkCertifications = New System.Windows.Forms.CheckBox()
        Me.chkCountries = New System.Windows.Forms.CheckBox()
        Me.chkGenres = New System.Windows.Forms.CheckBox()
        Me.chkDirectors = New System.Windows.Forms.CheckBox()
        Me.chkWriters = New System.Windows.Forms.CheckBox()
        Me.chkTop250 = New System.Windows.Forms.CheckBox()
        Me.chkTitle = New System.Windows.Forms.CheckBox()
        Me.chkTagline = New System.Windows.Forms.CheckBox()
        Me.chkStudiowithDistributors = New System.Windows.Forms.CheckBox()
        Me.chkStudios = New System.Windows.Forms.CheckBox()
        Me.chkRuntime = New System.Windows.Forms.CheckBox()
        Me.chkOutline = New System.Windows.Forms.CheckBox()
        Me.chkPlot = New System.Windows.Forms.CheckBox()
        Me.chkOriginalTitle = New System.Windows.Forms.CheckBox()
        Me.chkMPAA = New System.Windows.Forms.CheckBox()
        Me.chkMPAADescription = New System.Windows.Forms.CheckBox()
        Me.lblInfoParsing = New System.Windows.Forms.Label()
        Me.chkPremiered = New System.Windows.Forms.CheckBox()
        Me.chkRating = New System.Windows.Forms.CheckBox()
        Me.gbScraperOpts = New System.Windows.Forms.GroupBox()
        Me.tblScraperOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkPopularTitles = New System.Windows.Forms.CheckBox()
        Me.chkFallBackworldwide = New System.Windows.Forms.CheckBox()
        Me.lblForceTitleLanguage = New System.Windows.Forms.Label()
        Me.cbForceTitleLanguage = New System.Windows.Forms.ComboBox()
        Me.chkPartialTitles = New System.Windows.Forms.CheckBox()
        Me.chkTvTitles = New System.Windows.Forms.CheckBox()
        Me.chkVideoTitles = New System.Windows.Forms.CheckBox()
        Me.chkShortTitles = New System.Windows.Forms.CheckBox()
        Me.pnlSettingBottom = New System.Windows.Forms.Panel()
        Me.tblSettingsBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.pbIconBottom = New System.Windows.Forms.PictureBox()
        Me.lblInfoBottom = New System.Windows.Forms.Label()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
        Me.gbScraperFieldsOpts.SuspendLayout()
        Me.tblScraperFieldsOpts.SuspendLayout()
        Me.gbScraperOpts.SuspendLayout()
        Me.tblScraperOpts.SuspendLayout()
        Me.pnlSettingBottom.SuspendLayout()
        Me.tblSettingsBottom.SuspendLayout()
        CType(Me.pbIconBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.pnlSettingsTop.Size = New System.Drawing.Size(615, 29)
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
        Me.tblSettingsTop.Controls.Add(Me.lblScraperOrder, 2, 0)
        Me.tblSettingsTop.Controls.Add(Me.chkEnabled, 0, 0)
        Me.tblSettingsTop.Controls.Add(Me.btnUp, 3, 0)
        Me.tblSettingsTop.Controls.Add(Me.btnDown, 4, 0)
        Me.tblSettingsTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsTop.Name = "tblSettingsTop"
        Me.tblSettingsTop.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.tblSettingsTop.RowCount = 2
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.Size = New System.Drawing.Size(615, 29)
        Me.tblSettingsTop.TabIndex = 98
        '
        'lblScraperOrder
        '
        Me.lblScraperOrder.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblScraperOrder.AutoSize = True
        Me.lblScraperOrder.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScraperOrder.Location = New System.Drawing.Point(496, 8)
        Me.lblScraperOrder.Name = "lblScraperOrder"
        Me.lblScraperOrder.Size = New System.Drawing.Size(58, 12)
        Me.lblScraperOrder.TabIndex = 1
        Me.lblScraperOrder.Text = "Scraper order"
        '
        'btnUp
        '
        Me.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(560, 3)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(23, 23)
        Me.btnUp.TabIndex = 2
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'btnDown
        '
        Me.btnDown.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(589, 3)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(23, 23)
        Me.btnDown.TabIndex = 3
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.Controls.Add(Me.pnlSettingsMain)
        Me.pnlSettings.Controls.Add(Me.pnlSettingBottom)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsTop)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(615, 491)
        Me.pnlSettings.TabIndex = 0
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 29)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(615, 425)
        Me.pnlSettingsMain.TabIndex = 100
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
        Me.tblSettingsMain.Size = New System.Drawing.Size(615, 425)
        Me.tblSettingsMain.TabIndex = 98
        '
        'gbScraperFieldsOpts
        '
        Me.gbScraperFieldsOpts.AutoSize = True
        Me.gbScraperFieldsOpts.Controls.Add(Me.tblScraperFieldsOpts)
        Me.gbScraperFieldsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperFieldsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbScraperFieldsOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbScraperFieldsOpts.Name = "gbScraperFieldsOpts"
        Me.gbScraperFieldsOpts.Size = New System.Drawing.Size(506, 199)
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
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkActors, 0, 0)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkCertifications, 0, 1)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkCountries, 0, 2)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkGenres, 0, 5)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkDirectors, 0, 4)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkWriters, 0, 3)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkTop250, 3, 0)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkTitle, 2, 5)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkTagline, 2, 4)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkStudiowithDistributors, 2, 3)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkStudios, 2, 2)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkRuntime, 2, 1)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkOutline, 1, 4)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkPlot, 1, 3)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkOriginalTitle, 1, 2)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkMPAA, 1, 0)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkMPAADescription, 1, 1)
        Me.tblScraperFieldsOpts.Controls.Add(Me.lblInfoParsing, 0, 7)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkPremiered, 1, 5)
        Me.tblScraperFieldsOpts.Controls.Add(Me.chkRating, 2, 0)
        Me.tblScraperFieldsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperFieldsOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperFieldsOpts.Name = "tblScraperFieldsOpts"
        Me.tblScraperFieldsOpts.RowCount = 9
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.Size = New System.Drawing.Size(500, 178)
        Me.tblScraperFieldsOpts.TabIndex = 1
        '
        'chkActors
        '
        Me.chkActors.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkActors.AutoSize = True
        Me.chkActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkActors.Location = New System.Drawing.Point(3, 3)
        Me.chkActors.Name = "chkActors"
        Me.chkActors.Size = New System.Drawing.Size(58, 17)
        Me.chkActors.TabIndex = 12
        Me.chkActors.Text = "Actors"
        Me.chkActors.UseVisualStyleBackColor = True
        '
        'chkCertifications
        '
        Me.chkCertifications.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCertifications.AutoSize = True
        Me.chkCertifications.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCertifications.Location = New System.Drawing.Point(3, 26)
        Me.chkCertifications.Name = "chkCertifications"
        Me.chkCertifications.Size = New System.Drawing.Size(94, 17)
        Me.chkCertifications.TabIndex = 3
        Me.chkCertifications.Text = "Certifications"
        Me.chkCertifications.UseVisualStyleBackColor = True
        '
        'chkCountries
        '
        Me.chkCountries.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCountries.AutoSize = True
        Me.chkCountries.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCountries.Location = New System.Drawing.Point(3, 49)
        Me.chkCountries.Name = "chkCountries"
        Me.chkCountries.Size = New System.Drawing.Size(76, 17)
        Me.chkCountries.TabIndex = 18
        Me.chkCountries.Text = "Countries"
        Me.chkCountries.UseVisualStyleBackColor = True
        '
        'chkGenres
        '
        Me.chkGenres.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGenres.AutoSize = True
        Me.chkGenres.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGenres.Location = New System.Drawing.Point(3, 118)
        Me.chkGenres.Name = "chkGenres"
        Me.chkGenres.Size = New System.Drawing.Size(62, 17)
        Me.chkGenres.TabIndex = 15
        Me.chkGenres.Text = "Genres"
        Me.chkGenres.UseVisualStyleBackColor = True
        '
        'chkDirectors
        '
        Me.chkDirectors.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDirectors.AutoSize = True
        Me.chkDirectors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDirectors.Location = New System.Drawing.Point(3, 95)
        Me.chkDirectors.Name = "chkDirectors"
        Me.chkDirectors.Size = New System.Drawing.Size(72, 17)
        Me.chkDirectors.TabIndex = 13
        Me.chkDirectors.Text = "Directors"
        Me.chkDirectors.UseVisualStyleBackColor = True
        '
        'chkWriters
        '
        Me.chkWriters.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkWriters.AutoSize = True
        Me.chkWriters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWriters.Location = New System.Drawing.Point(3, 72)
        Me.chkWriters.Name = "chkWriters"
        Me.chkWriters.Size = New System.Drawing.Size(108, 17)
        Me.chkWriters.TabIndex = 14
        Me.chkWriters.Text = "Credits (Writers)"
        Me.chkWriters.UseVisualStyleBackColor = True
        '
        'chkTop250
        '
        Me.chkTop250.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTop250.AutoSize = True
        Me.chkTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTop250.Location = New System.Drawing.Point(431, 3)
        Me.chkTop250.Name = "chkTop250"
        Me.chkTop250.Size = New System.Drawing.Size(66, 17)
        Me.chkTop250.TabIndex = 16
        Me.chkTop250.Text = "Top 250"
        Me.chkTop250.UseVisualStyleBackColor = True
        '
        'chkTitle
        '
        Me.chkTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitle.AutoSize = True
        Me.chkTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTitle.Location = New System.Drawing.Point(277, 118)
        Me.chkTitle.Name = "chkTitle"
        Me.chkTitle.Size = New System.Drawing.Size(48, 17)
        Me.chkTitle.TabIndex = 0
        Me.chkTitle.Text = "Title"
        Me.chkTitle.UseVisualStyleBackColor = True
        '
        'chkTagline
        '
        Me.chkTagline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTagline.AutoSize = True
        Me.chkTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTagline.Location = New System.Drawing.Point(277, 95)
        Me.chkTagline.Name = "chkTagline"
        Me.chkTagline.Size = New System.Drawing.Size(63, 17)
        Me.chkTagline.TabIndex = 9
        Me.chkTagline.Text = "Tagline"
        Me.chkTagline.UseVisualStyleBackColor = True
        '
        'chkStudiowithDistributors
        '
        Me.chkStudiowithDistributors.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkStudiowithDistributors.AutoSize = True
        Me.chkStudiowithDistributors.Enabled = False
        Me.chkStudiowithDistributors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStudiowithDistributors.Location = New System.Drawing.Point(277, 72)
        Me.chkStudiowithDistributors.Name = "chkStudiowithDistributors"
        Me.chkStudiowithDistributors.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkStudiowithDistributors.Size = New System.Drawing.Size(148, 17)
        Me.chkStudiowithDistributors.TabIndex = 82
        Me.chkStudiowithDistributors.Text = "include Distributors"
        Me.chkStudiowithDistributors.UseVisualStyleBackColor = True
        '
        'chkStudios
        '
        Me.chkStudios.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkStudios.AutoSize = True
        Me.chkStudios.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStudios.Location = New System.Drawing.Point(277, 49)
        Me.chkStudios.Name = "chkStudios"
        Me.chkStudios.Size = New System.Drawing.Size(65, 17)
        Me.chkStudios.TabIndex = 8
        Me.chkStudios.Text = "Studios"
        Me.chkStudios.UseVisualStyleBackColor = True
        '
        'chkRuntime
        '
        Me.chkRuntime.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRuntime.AutoSize = True
        Me.chkRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRuntime.Location = New System.Drawing.Point(277, 26)
        Me.chkRuntime.Name = "chkRuntime"
        Me.chkRuntime.Size = New System.Drawing.Size(69, 17)
        Me.chkRuntime.TabIndex = 5
        Me.chkRuntime.Text = "Runtime"
        Me.chkRuntime.UseVisualStyleBackColor = True
        '
        'chkOutline
        '
        Me.chkOutline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkOutline.AutoSize = True
        Me.chkOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOutline.Location = New System.Drawing.Point(117, 95)
        Me.chkOutline.Name = "chkOutline"
        Me.chkOutline.Size = New System.Drawing.Size(88, 17)
        Me.chkOutline.TabIndex = 10
        Me.chkOutline.Text = "Plot Outline"
        Me.chkOutline.UseVisualStyleBackColor = True
        '
        'chkPlot
        '
        Me.chkPlot.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkPlot.AutoSize = True
        Me.chkPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPlot.Location = New System.Drawing.Point(117, 72)
        Me.chkPlot.Name = "chkPlot"
        Me.chkPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkPlot.TabIndex = 11
        Me.chkPlot.Text = "Plot"
        Me.chkPlot.UseVisualStyleBackColor = True
        '
        'chkOriginalTitle
        '
        Me.chkOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkOriginalTitle.AutoSize = True
        Me.chkOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOriginalTitle.Location = New System.Drawing.Point(117, 49)
        Me.chkOriginalTitle.Name = "chkOriginalTitle"
        Me.chkOriginalTitle.Size = New System.Drawing.Size(93, 17)
        Me.chkOriginalTitle.TabIndex = 81
        Me.chkOriginalTitle.Text = "Original Title"
        Me.chkOriginalTitle.UseVisualStyleBackColor = True
        '
        'chkMPAA
        '
        Me.chkMPAA.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMPAA.AutoSize = True
        Me.chkMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMPAA.Location = New System.Drawing.Point(117, 3)
        Me.chkMPAA.Name = "chkMPAA"
        Me.chkMPAA.Size = New System.Drawing.Size(55, 17)
        Me.chkMPAA.TabIndex = 80
        Me.chkMPAA.Text = "MPAA"
        Me.chkMPAA.UseVisualStyleBackColor = True
        '
        'chkMPAADescription
        '
        Me.chkMPAADescription.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMPAADescription.AutoSize = True
        Me.chkMPAADescription.Enabled = False
        Me.chkMPAADescription.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMPAADescription.Location = New System.Drawing.Point(117, 26)
        Me.chkMPAADescription.Name = "chkMPAADescription"
        Me.chkMPAADescription.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkMPAADescription.Size = New System.Drawing.Size(154, 17)
        Me.chkMPAADescription.TabIndex = 82
        Me.chkMPAADescription.Text = "include Description *"
        Me.chkMPAADescription.UseVisualStyleBackColor = True
        '
        'lblInfoParsing
        '
        Me.lblInfoParsing.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblInfoParsing.AutoSize = True
        Me.tblScraperFieldsOpts.SetColumnSpan(Me.lblInfoParsing, 4)
        Me.lblInfoParsing.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfoParsing.Location = New System.Drawing.Point(3, 161)
        Me.lblInfoParsing.Name = "lblInfoParsing"
        Me.lblInfoParsing.Size = New System.Drawing.Size(251, 13)
        Me.lblInfoParsing.TabIndex = 83
        Me.lblInfoParsing.Text = "* additional page(s) to parse, needs longer to scrape"
        '
        'chkPremiered
        '
        Me.chkPremiered.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkPremiered.AutoSize = True
        Me.chkPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPremiered.Location = New System.Drawing.Point(117, 118)
        Me.chkPremiered.Name = "chkPremiered"
        Me.chkPremiered.Size = New System.Drawing.Size(77, 17)
        Me.chkPremiered.TabIndex = 4
        Me.chkPremiered.Text = "Premiered"
        Me.chkPremiered.UseVisualStyleBackColor = True
        '
        'chkRating
        '
        Me.chkRating.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRating.AutoSize = True
        Me.chkRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRating.Location = New System.Drawing.Point(277, 3)
        Me.chkRating.Name = "chkRating"
        Me.chkRating.Size = New System.Drawing.Size(60, 17)
        Me.chkRating.TabIndex = 6
        Me.chkRating.Text = "Rating"
        Me.chkRating.UseVisualStyleBackColor = True
        '
        'gbScraperOpts
        '
        Me.gbScraperOpts.AutoSize = True
        Me.gbScraperOpts.Controls.Add(Me.tblScraperOpts)
        Me.gbScraperOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbScraperOpts.Location = New System.Drawing.Point(3, 208)
        Me.gbScraperOpts.Name = "gbScraperOpts"
        Me.gbScraperOpts.Size = New System.Drawing.Size(506, 140)
        Me.gbScraperOpts.TabIndex = 97
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
        Me.tblScraperOpts.Controls.Add(Me.chkPopularTitles, 0, 0)
        Me.tblScraperOpts.Controls.Add(Me.chkFallBackworldwide, 1, 1)
        Me.tblScraperOpts.Controls.Add(Me.lblForceTitleLanguage, 1, 0)
        Me.tblScraperOpts.Controls.Add(Me.cbForceTitleLanguage, 2, 0)
        Me.tblScraperOpts.Controls.Add(Me.chkPartialTitles, 0, 1)
        Me.tblScraperOpts.Controls.Add(Me.chkTvTitles, 0, 2)
        Me.tblScraperOpts.Controls.Add(Me.chkVideoTitles, 0, 3)
        Me.tblScraperOpts.Controls.Add(Me.chkShortTitles, 0, 4)
        Me.tblScraperOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperOpts.Name = "tblScraperOpts"
        Me.tblScraperOpts.RowCount = 5
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperOpts.Size = New System.Drawing.Size(500, 119)
        Me.tblScraperOpts.TabIndex = 1
        '
        'chkPopularTitles
        '
        Me.chkPopularTitles.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkPopularTitles.AutoSize = True
        Me.chkPopularTitles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPopularTitles.Location = New System.Drawing.Point(3, 5)
        Me.chkPopularTitles.Name = "chkPopularTitles"
        Me.chkPopularTitles.Size = New System.Drawing.Size(96, 17)
        Me.chkPopularTitles.TabIndex = 0
        Me.chkPopularTitles.Text = "Popular Titles"
        Me.chkPopularTitles.UseVisualStyleBackColor = True
        '
        'chkFallBackworldwide
        '
        Me.chkFallBackworldwide.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFallBackworldwide.AutoSize = True
        Me.tblScraperOpts.SetColumnSpan(Me.chkFallBackworldwide, 2)
        Me.chkFallBackworldwide.Enabled = False
        Me.chkFallBackworldwide.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFallBackworldwide.Location = New System.Drawing.Point(112, 30)
        Me.chkFallBackworldwide.Name = "chkFallBackworldwide"
        Me.chkFallBackworldwide.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkFallBackworldwide.Size = New System.Drawing.Size(189, 17)
        Me.chkFallBackworldwide.TabIndex = 78
        Me.chkFallBackworldwide.Text = "Fall back on worldwide title"
        Me.chkFallBackworldwide.UseVisualStyleBackColor = True
        '
        'lblForceTitleLanguage
        '
        Me.lblForceTitleLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblForceTitleLanguage.AutoSize = True
        Me.lblForceTitleLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblForceTitleLanguage.Location = New System.Drawing.Point(112, 7)
        Me.lblForceTitleLanguage.Name = "lblForceTitleLanguage"
        Me.lblForceTitleLanguage.Size = New System.Drawing.Size(117, 13)
        Me.lblForceTitleLanguage.TabIndex = 4
        Me.lblForceTitleLanguage.Text = "Force Title Language:"
        '
        'cbForceTitleLanguage
        '
        Me.cbForceTitleLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbForceTitleLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbForceTitleLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbForceTitleLanguage.FormattingEnabled = True
        Me.cbForceTitleLanguage.Items.AddRange(New Object() {"", "Argentina", "Australia", "Azerbaijan", "Belgium", "Brazil", "Bulgaria (Bulgarian title)", "Canada (English title)", "Canada (French title)", "Chile", "China (Mandarin title)", "Colombia", "Croatia", "Czech Republic", "Denmark", "Estonia", "Finland", "Finland (Swedish title)", "France", "Georgia", "Germany", "Greece", "Hong Kong", "Hong Kong (Cantonese title)", "Hong Kong (Mandarin title)", "Hungary", "Iceland", "India (Hindi title)", "Ireland", "Israel (Hebrew title)", "Italy", "Japan", "Japan (English title)", "Latvia", "Lithuania", "Mexico", "Netherlands", "New Zealand", "Panama", "Peru", "Poland", "Portugal", "Romania", "Russia", "Serbia", "Singapore", "Slovakia", "Slovenia", "South Korea", "Spain", "Sweden", "Switzerland", "Taiwan", "Turkey (Turkish title)", "UK", "Ukraine", "Uruguay", "USA", "Venezuela", "Vietnam"})
        Me.cbForceTitleLanguage.Location = New System.Drawing.Point(235, 3)
        Me.cbForceTitleLanguage.Name = "cbForceTitleLanguage"
        Me.cbForceTitleLanguage.Size = New System.Drawing.Size(131, 21)
        Me.cbForceTitleLanguage.Sorted = True
        Me.cbForceTitleLanguage.TabIndex = 77
        '
        'chkPartialTitles
        '
        Me.chkPartialTitles.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkPartialTitles.AutoSize = True
        Me.chkPartialTitles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPartialTitles.Location = New System.Drawing.Point(3, 30)
        Me.chkPartialTitles.Name = "chkPartialTitles"
        Me.chkPartialTitles.Size = New System.Drawing.Size(88, 17)
        Me.chkPartialTitles.TabIndex = 1
        Me.chkPartialTitles.Text = "Partial Titles"
        Me.chkPartialTitles.UseVisualStyleBackColor = True
        '
        'chkTvTitles
        '
        Me.chkTvTitles.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTvTitles.AutoSize = True
        Me.chkTvTitles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTvTitles.Location = New System.Drawing.Point(3, 53)
        Me.chkTvTitles.Name = "chkTvTitles"
        Me.chkTvTitles.Size = New System.Drawing.Size(103, 17)
        Me.chkTvTitles.TabIndex = 2
        Me.chkTvTitles.Text = "TV Movie Titles"
        Me.chkTvTitles.UseVisualStyleBackColor = True
        '
        'chkVideoTitles
        '
        Me.chkVideoTitles.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkVideoTitles.AutoSize = True
        Me.chkVideoTitles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVideoTitles.Location = New System.Drawing.Point(3, 76)
        Me.chkVideoTitles.Name = "chkVideoTitles"
        Me.chkVideoTitles.Size = New System.Drawing.Size(86, 17)
        Me.chkVideoTitles.TabIndex = 3
        Me.chkVideoTitles.Text = "Video Titles"
        Me.chkVideoTitles.UseVisualStyleBackColor = True
        '
        'chkShortTitles
        '
        Me.chkShortTitles.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkShortTitles.AutoSize = True
        Me.chkShortTitles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShortTitles.Location = New System.Drawing.Point(3, 99)
        Me.chkShortTitles.Name = "chkShortTitles"
        Me.chkShortTitles.Size = New System.Drawing.Size(84, 17)
        Me.chkShortTitles.TabIndex = 80
        Me.chkShortTitles.Text = "Short Titles"
        Me.chkShortTitles.UseVisualStyleBackColor = True
        '
        'pnlSettingBottom
        '
        Me.pnlSettingBottom.AutoSize = True
        Me.pnlSettingBottom.Controls.Add(Me.tblSettingsBottom)
        Me.pnlSettingBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlSettingBottom.Location = New System.Drawing.Point(0, 454)
        Me.pnlSettingBottom.Name = "pnlSettingBottom"
        Me.pnlSettingBottom.Size = New System.Drawing.Size(615, 37)
        Me.pnlSettingBottom.TabIndex = 99
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
        Me.tblSettingsBottom.Size = New System.Drawing.Size(615, 37)
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
        Me.ClientSize = New System.Drawing.Size(615, 491)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder_Movie"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
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
        Me.gbScraperOpts.ResumeLayout(False)
        Me.gbScraperOpts.PerformLayout()
        Me.tblScraperOpts.ResumeLayout(False)
        Me.tblScraperOpts.PerformLayout()
        Me.pnlSettingBottom.ResumeLayout(False)
        Me.pnlSettingBottom.PerformLayout()
        Me.tblSettingsBottom.ResumeLayout(False)
        Me.tblSettingsBottom.PerformLayout()
        CType(Me.pbIconBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents lblScraperOrder As System.Windows.Forms.Label
    Friend WithEvents lblInfoBottom As System.Windows.Forms.Label
    Friend WithEvents pbIconBottom As System.Windows.Forms.PictureBox
    Friend WithEvents gbScraperFieldsOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkCertifications As System.Windows.Forms.CheckBox
    Friend WithEvents chkCountries As System.Windows.Forms.CheckBox
    Friend WithEvents chkTop250 As System.Windows.Forms.CheckBox
    Friend WithEvents chkWriters As System.Windows.Forms.CheckBox
    Friend WithEvents chkStudios As System.Windows.Forms.CheckBox
    Friend WithEvents chkRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkOutline As System.Windows.Forms.CheckBox
    Friend WithEvents chkGenres As System.Windows.Forms.CheckBox
    Friend WithEvents chkDirectors As System.Windows.Forms.CheckBox
    Friend WithEvents chkTagline As System.Windows.Forms.CheckBox
    Friend WithEvents chkActors As System.Windows.Forms.CheckBox
    Friend WithEvents chkRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkPremiered As System.Windows.Forms.CheckBox
    Friend WithEvents chkTitle As System.Windows.Forms.CheckBox
    Friend WithEvents gbScraperOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkTvTitles As System.Windows.Forms.CheckBox
    Friend WithEvents chkPartialTitles As System.Windows.Forms.CheckBox
    Friend WithEvents chkPopularTitles As System.Windows.Forms.CheckBox
    Friend WithEvents chkVideoTitles As System.Windows.Forms.CheckBox
    Friend WithEvents chkShortTitles As System.Windows.Forms.CheckBox
    Friend WithEvents chkFallBackworldwide As System.Windows.Forms.CheckBox
    Friend WithEvents cbForceTitleLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents lblForceTitleLanguage As System.Windows.Forms.Label
    Friend WithEvents chkMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkOriginalTitle As System.Windows.Forms.CheckBox
    Friend WithEvents tblScraperFieldsOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblScraperOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingBottom As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents chkStudiowithDistributors As System.Windows.Forms.CheckBox
    Friend WithEvents chkMPAADescription As CheckBox
    Friend WithEvents lblInfoParsing As Label
End Class
