<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIMDBInfoSettingsHolder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIMDBInfoSettingsHolder))
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.cbEnabled = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.gbScraperOptions = New System.Windows.Forms.GroupBox()
        Me.lblForceTitleLanguage = New System.Windows.Forms.Label()
        Me.chkFallBackworldwide = New System.Windows.Forms.CheckBox()
        Me.cbForceTitleLanguage = New System.Windows.Forms.ComboBox()
        Me.chkVideoTitles = New System.Windows.Forms.CheckBox()
        Me.chkTvTitles = New System.Windows.Forms.CheckBox()
        Me.chkPartialTitles = New System.Windows.Forms.CheckBox()
        Me.chkPopularTitles = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.gbScraperFields = New System.Windows.Forms.GroupBox()
        Me.chkCertification = New System.Windows.Forms.CheckBox()
        Me.chkCountry = New System.Windows.Forms.CheckBox()
        Me.chkTop250 = New System.Windows.Forms.CheckBox()
        Me.chkCrew = New System.Windows.Forms.CheckBox()
        Me.chkMusicBy = New System.Windows.Forms.CheckBox()
        Me.chkProducers = New System.Windows.Forms.CheckBox()
        Me.chkWriters = New System.Windows.Forms.CheckBox()
        Me.chkStudio = New System.Windows.Forms.CheckBox()
        Me.chkRuntime = New System.Windows.Forms.CheckBox()
        Me.chkFullCrew = New System.Windows.Forms.CheckBox()
        Me.chkPlot = New System.Windows.Forms.CheckBox()
        Me.chkOutline = New System.Windows.Forms.CheckBox()
        Me.chkGenre = New System.Windows.Forms.CheckBox()
        Me.chkDirector = New System.Windows.Forms.CheckBox()
        Me.chkTagline = New System.Windows.Forms.CheckBox()
        Me.chkCast = New System.Windows.Forms.CheckBox()
        Me.chkVotes = New System.Windows.Forms.CheckBox()
        Me.chkTrailer = New System.Windows.Forms.CheckBox()
        Me.chkRating = New System.Windows.Forms.CheckBox()
        Me.chkRelease = New System.Windows.Forms.CheckBox()
        Me.chkYear = New System.Windows.Forms.CheckBox()
        Me.chkTitle = New System.Windows.Forms.CheckBox()
        Me.gbScraperFieldsCredits = New System.Windows.Forms.GroupBox()
        Me.Panel1.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.gbScraperOptions.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbScraperFields.SuspendLayout()
        Me.gbScraperFieldsCredits.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblVersion
        '
        Me.lblVersion.Location = New System.Drawing.Point(286, 393)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(90, 16)
        Me.lblVersion.TabIndex = 74
        Me.lblVersion.Text = "Version:"
        '
        'cbEnabled
        '
        Me.cbEnabled.AutoSize = True
        Me.cbEnabled.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbEnabled.Location = New System.Drawing.Point(10, 5)
        Me.cbEnabled.Name = "cbEnabled"
        Me.cbEnabled.Size = New System.Drawing.Size(68, 17)
        Me.cbEnabled.TabIndex = 0
        Me.cbEnabled.Text = "Enabled"
        Me.cbEnabled.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.btnDown)
        Me.Panel1.Controls.Add(Me.cbEnabled)
        Me.Panel1.Controls.Add(Me.btnUp)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1125, 25)
        Me.Panel1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(500, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Scraper order"
        '
        'btnDown
        '
        Me.btnDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(591, 1)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(23, 23)
        Me.btnDown.TabIndex = 3
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(566, 1)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(23, 23)
        Me.btnUp.TabIndex = 2
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'pnlSettings
        '
        Me.pnlSettings.Controls.Add(Me.gbScraperOptions)
        Me.pnlSettings.Controls.Add(Me.Label1)
        Me.pnlSettings.Controls.Add(Me.PictureBox1)
        Me.pnlSettings.Controls.Add(Me.Panel1)
        Me.pnlSettings.Controls.Add(Me.gbScraperFields)
        Me.pnlSettings.Location = New System.Drawing.Point(12, 4)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(617, 369)
        Me.pnlSettings.TabIndex = 0
        '
        'gbScraperOptions
        '
        Me.gbScraperOptions.Controls.Add(Me.lblForceTitleLanguage)
        Me.gbScraperOptions.Controls.Add(Me.chkFallBackworldwide)
        Me.gbScraperOptions.Controls.Add(Me.cbForceTitleLanguage)
        Me.gbScraperOptions.Controls.Add(Me.chkVideoTitles)
        Me.gbScraperOptions.Controls.Add(Me.chkTvTitles)
        Me.gbScraperOptions.Controls.Add(Me.chkPartialTitles)
        Me.gbScraperOptions.Controls.Add(Me.chkPopularTitles)
        Me.gbScraperOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbScraperOptions.Location = New System.Drawing.Point(10, 199)
        Me.gbScraperOptions.Name = "gbScraperOptions"
        Me.gbScraperOptions.Size = New System.Drawing.Size(589, 130)
        Me.gbScraperOptions.TabIndex = 97
        Me.gbScraperOptions.TabStop = False
        Me.gbScraperOptions.Text = "Scraper Options"
        '
        'lblForceTitleLanguage
        '
        Me.lblForceTitleLanguage.AutoSize = True
        Me.lblForceTitleLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblForceTitleLanguage.Location = New System.Drawing.Point(129, 22)
        Me.lblForceTitleLanguage.Name = "lblForceTitleLanguage"
        Me.lblForceTitleLanguage.Size = New System.Drawing.Size(116, 13)
        Me.lblForceTitleLanguage.TabIndex = 4
        Me.lblForceTitleLanguage.Text = "Force Title Language:"
        '
        'chkFallBackworldwide
        '
        Me.chkFallBackworldwide.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFallBackworldwide.Location = New System.Drawing.Point(132, 45)
        Me.chkFallBackworldwide.Name = "chkFallBackworldwide"
        Me.chkFallBackworldwide.Size = New System.Drawing.Size(293, 17)
        Me.chkFallBackworldwide.TabIndex = 78
        Me.chkFallBackworldwide.Text = "Fall back on worldwide title"
        Me.chkFallBackworldwide.UseVisualStyleBackColor = True
        '
        'cbForceTitleLanguage
        '
        Me.cbForceTitleLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbForceTitleLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbForceTitleLanguage.FormattingEnabled = True
        Me.cbForceTitleLanguage.Items.AddRange(New Object() {"", "Argentina", "Australia", "Belgium", "Brazil", "Canada: English title", "Canada: French title", "Denmark", "Finland", "France", "Germany", "Hong Kong", "Iceland", "Ireland", "Italy", "Netherlands", "New Zealand", "Peru", "Portugal", "Singapore", "South Korea", "Spain", "Sweden", "Switzerland", "UK", "USA"})
        Me.cbForceTitleLanguage.Location = New System.Drawing.Point(294, 18)
        Me.cbForceTitleLanguage.Name = "cbForceTitleLanguage"
        Me.cbForceTitleLanguage.Size = New System.Drawing.Size(131, 21)
        Me.cbForceTitleLanguage.Sorted = True
        Me.cbForceTitleLanguage.TabIndex = 77
        '
        'chkVideoTitles
        '
        Me.chkVideoTitles.AutoSize = True
        Me.chkVideoTitles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVideoTitles.Location = New System.Drawing.Point(6, 92)
        Me.chkVideoTitles.Name = "chkVideoTitles"
        Me.chkVideoTitles.Size = New System.Drawing.Size(85, 17)
        Me.chkVideoTitles.TabIndex = 3
        Me.chkVideoTitles.Text = "Video Titles"
        Me.chkVideoTitles.UseVisualStyleBackColor = True
        '
        'chkTvTitles
        '
        Me.chkTvTitles.AutoSize = True
        Me.chkTvTitles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTvTitles.Location = New System.Drawing.Point(7, 69)
        Me.chkTvTitles.Name = "chkTvTitles"
        Me.chkTvTitles.Size = New System.Drawing.Size(67, 17)
        Me.chkTvTitles.TabIndex = 2
        Me.chkTvTitles.Text = "TV Titles"
        Me.chkTvTitles.UseVisualStyleBackColor = True
        '
        'chkPartialTitles
        '
        Me.chkPartialTitles.AutoSize = True
        Me.chkPartialTitles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPartialTitles.Location = New System.Drawing.Point(7, 45)
        Me.chkPartialTitles.Name = "chkPartialTitles"
        Me.chkPartialTitles.Size = New System.Drawing.Size(87, 17)
        Me.chkPartialTitles.TabIndex = 1
        Me.chkPartialTitles.Text = "Partial Titles"
        Me.chkPartialTitles.UseVisualStyleBackColor = True
        '
        'chkPopularTitles
        '
        Me.chkPopularTitles.AutoSize = True
        Me.chkPopularTitles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPopularTitles.Location = New System.Drawing.Point(7, 22)
        Me.chkPopularTitles.Name = "chkPopularTitles"
        Me.chkPopularTitles.Size = New System.Drawing.Size(95, 17)
        Me.chkPopularTitles.TabIndex = 0
        Me.chkPopularTitles.Text = "Popular Titles"
        Me.chkPopularTitles.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(37, 337)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(225, 31)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "These settings are specific to this module." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please refer to the global settings " & _
    "for more options."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 335)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(30, 31)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 96
        Me.PictureBox1.TabStop = False
        '
        'gbScraperFields
        '
        Me.gbScraperFields.Controls.Add(Me.gbScraperFieldsCredits)
        Me.gbScraperFields.Controls.Add(Me.chkCertification)
        Me.gbScraperFields.Controls.Add(Me.chkCountry)
        Me.gbScraperFields.Controls.Add(Me.chkTop250)
        Me.gbScraperFields.Controls.Add(Me.chkWriters)
        Me.gbScraperFields.Controls.Add(Me.chkStudio)
        Me.gbScraperFields.Controls.Add(Me.chkRuntime)
        Me.gbScraperFields.Controls.Add(Me.chkPlot)
        Me.gbScraperFields.Controls.Add(Me.chkOutline)
        Me.gbScraperFields.Controls.Add(Me.chkGenre)
        Me.gbScraperFields.Controls.Add(Me.chkDirector)
        Me.gbScraperFields.Controls.Add(Me.chkTagline)
        Me.gbScraperFields.Controls.Add(Me.chkCast)
        Me.gbScraperFields.Controls.Add(Me.chkVotes)
        Me.gbScraperFields.Controls.Add(Me.chkTrailer)
        Me.gbScraperFields.Controls.Add(Me.chkRating)
        Me.gbScraperFields.Controls.Add(Me.chkRelease)
        Me.gbScraperFields.Controls.Add(Me.chkYear)
        Me.gbScraperFields.Controls.Add(Me.chkTitle)
        Me.gbScraperFields.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbScraperFields.Location = New System.Drawing.Point(11, 31)
        Me.gbScraperFields.Name = "gbScraperFields"
        Me.gbScraperFields.Size = New System.Drawing.Size(588, 161)
        Me.gbScraperFields.TabIndex = 3
        Me.gbScraperFields.TabStop = False
        Me.gbScraperFields.Text = "Scraper Fields"
        '
        'chkCertification
        '
        Me.chkCertification.AutoSize = True
        Me.chkCertification.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCertification.Location = New System.Drawing.Point(6, 53)
        Me.chkCertification.Name = "chkCertification"
        Me.chkCertification.Size = New System.Drawing.Size(123, 17)
        Me.chkCertification.TabIndex = 3
        Me.chkCertification.Text = "MPAA/Certification"
        Me.chkCertification.UseVisualStyleBackColor = True
        '
        'chkCountry
        '
        Me.chkCountry.AutoSize = True
        Me.chkCountry.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCountry.Location = New System.Drawing.Point(6, 70)
        Me.chkCountry.Name = "chkCountry"
        Me.chkCountry.Size = New System.Drawing.Size(67, 17)
        Me.chkCountry.TabIndex = 18
        Me.chkCountry.Text = "Country"
        Me.chkCountry.UseVisualStyleBackColor = True
        '
        'chkTop250
        '
        Me.chkTop250.AutoSize = True
        Me.chkTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTop250.Location = New System.Drawing.Point(131, 138)
        Me.chkTop250.Name = "chkTop250"
        Me.chkTop250.Size = New System.Drawing.Size(66, 17)
        Me.chkTop250.TabIndex = 16
        Me.chkTop250.Text = "Top 250"
        Me.chkTop250.UseVisualStyleBackColor = True
        '
        'chkCrew
        '
        Me.chkCrew.AutoSize = True
        Me.chkCrew.Enabled = False
        Me.chkCrew.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCrew.Location = New System.Drawing.Point(15, 38)
        Me.chkCrew.Name = "chkCrew"
        Me.chkCrew.Size = New System.Drawing.Size(85, 17)
        Me.chkCrew.TabIndex = 21
        Me.chkCrew.Text = "Other Crew"
        Me.chkCrew.UseVisualStyleBackColor = True
        '
        'chkMusicBy
        '
        Me.chkMusicBy.AutoSize = True
        Me.chkMusicBy.Enabled = False
        Me.chkMusicBy.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMusicBy.Location = New System.Drawing.Point(15, 55)
        Me.chkMusicBy.Name = "chkMusicBy"
        Me.chkMusicBy.Size = New System.Drawing.Size(71, 17)
        Me.chkMusicBy.TabIndex = 22
        Me.chkMusicBy.Text = "Music By"
        Me.chkMusicBy.UseVisualStyleBackColor = True
        '
        'chkProducers
        '
        Me.chkProducers.AutoSize = True
        Me.chkProducers.Enabled = False
        Me.chkProducers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkProducers.Location = New System.Drawing.Point(15, 72)
        Me.chkProducers.Name = "chkProducers"
        Me.chkProducers.Size = New System.Drawing.Size(77, 17)
        Me.chkProducers.TabIndex = 23
        Me.chkProducers.Text = "Producers"
        Me.chkProducers.UseVisualStyleBackColor = True
        '
        'chkWriters
        '
        Me.chkWriters.AutoSize = True
        Me.chkWriters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWriters.Location = New System.Drawing.Point(237, 36)
        Me.chkWriters.Name = "chkWriters"
        Me.chkWriters.Size = New System.Drawing.Size(105, 17)
        Me.chkWriters.TabIndex = 14
        Me.chkWriters.Text = "Credits(Writers)"
        Me.chkWriters.UseVisualStyleBackColor = True
        '
        'chkStudio
        '
        Me.chkStudio.AutoSize = True
        Me.chkStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStudio.Location = New System.Drawing.Point(131, 19)
        Me.chkStudio.Name = "chkStudio"
        Me.chkStudio.Size = New System.Drawing.Size(60, 17)
        Me.chkStudio.TabIndex = 8
        Me.chkStudio.Text = "Studio"
        Me.chkStudio.UseVisualStyleBackColor = True
        '
        'chkRuntime
        '
        Me.chkRuntime.AutoSize = True
        Me.chkRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRuntime.Location = New System.Drawing.Point(6, 104)
        Me.chkRuntime.Name = "chkRuntime"
        Me.chkRuntime.Size = New System.Drawing.Size(69, 17)
        Me.chkRuntime.TabIndex = 5
        Me.chkRuntime.Text = "Runtime"
        Me.chkRuntime.UseVisualStyleBackColor = True
        '
        'chkFullCrew
        '
        Me.chkFullCrew.AutoSize = True
        Me.chkFullCrew.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFullCrew.Location = New System.Drawing.Point(6, 21)
        Me.chkFullCrew.Name = "chkFullCrew"
        Me.chkFullCrew.Size = New System.Drawing.Size(111, 17)
        Me.chkFullCrew.TabIndex = 20
        Me.chkFullCrew.Text = "Scrape Full Crew"
        Me.chkFullCrew.UseVisualStyleBackColor = True
        '
        'chkPlot
        '
        Me.chkPlot.AutoSize = True
        Me.chkPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPlot.Location = New System.Drawing.Point(131, 70)
        Me.chkPlot.Name = "chkPlot"
        Me.chkPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkPlot.TabIndex = 11
        Me.chkPlot.Text = "Plot"
        Me.chkPlot.UseVisualStyleBackColor = True
        '
        'chkOutline
        '
        Me.chkOutline.AutoSize = True
        Me.chkOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOutline.Location = New System.Drawing.Point(131, 53)
        Me.chkOutline.Name = "chkOutline"
        Me.chkOutline.Size = New System.Drawing.Size(65, 17)
        Me.chkOutline.TabIndex = 10
        Me.chkOutline.Text = "Outline"
        Me.chkOutline.UseVisualStyleBackColor = True
        '
        'chkGenre
        '
        Me.chkGenre.AutoSize = True
        Me.chkGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGenre.Location = New System.Drawing.Point(131, 121)
        Me.chkGenre.Name = "chkGenre"
        Me.chkGenre.Size = New System.Drawing.Size(57, 17)
        Me.chkGenre.TabIndex = 15
        Me.chkGenre.Text = "Genre"
        Me.chkGenre.UseVisualStyleBackColor = True
        '
        'chkDirector
        '
        Me.chkDirector.AutoSize = True
        Me.chkDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDirector.Location = New System.Drawing.Point(131, 104)
        Me.chkDirector.Name = "chkDirector"
        Me.chkDirector.Size = New System.Drawing.Size(67, 17)
        Me.chkDirector.TabIndex = 13
        Me.chkDirector.Text = "Director"
        Me.chkDirector.UseVisualStyleBackColor = True
        '
        'chkTagline
        '
        Me.chkTagline.AutoSize = True
        Me.chkTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTagline.Location = New System.Drawing.Point(131, 36)
        Me.chkTagline.Name = "chkTagline"
        Me.chkTagline.Size = New System.Drawing.Size(63, 17)
        Me.chkTagline.TabIndex = 9
        Me.chkTagline.Text = "Tagline"
        Me.chkTagline.UseVisualStyleBackColor = True
        '
        'chkCast
        '
        Me.chkCast.AutoSize = True
        Me.chkCast.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCast.Location = New System.Drawing.Point(131, 87)
        Me.chkCast.Name = "chkCast"
        Me.chkCast.Size = New System.Drawing.Size(48, 17)
        Me.chkCast.TabIndex = 12
        Me.chkCast.Text = "Cast"
        Me.chkCast.UseVisualStyleBackColor = True
        '
        'chkVotes
        '
        Me.chkVotes.AutoSize = True
        Me.chkVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVotes.Location = New System.Drawing.Point(6, 138)
        Me.chkVotes.Name = "chkVotes"
        Me.chkVotes.Size = New System.Drawing.Size(86, 17)
        Me.chkVotes.TabIndex = 7
        Me.chkVotes.Text = "IMDB Votes"
        Me.chkVotes.UseVisualStyleBackColor = True
        '
        'chkTrailer
        '
        Me.chkTrailer.AutoSize = True
        Me.chkTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTrailer.Location = New System.Drawing.Point(237, 19)
        Me.chkTrailer.Name = "chkTrailer"
        Me.chkTrailer.Size = New System.Drawing.Size(57, 17)
        Me.chkTrailer.TabIndex = 17
        Me.chkTrailer.Text = "Trailer"
        Me.chkTrailer.UseVisualStyleBackColor = True
        '
        'chkRating
        '
        Me.chkRating.AutoSize = True
        Me.chkRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRating.Location = New System.Drawing.Point(6, 121)
        Me.chkRating.Name = "chkRating"
        Me.chkRating.Size = New System.Drawing.Size(91, 17)
        Me.chkRating.TabIndex = 6
        Me.chkRating.Text = "IMDB Rating"
        Me.chkRating.UseVisualStyleBackColor = True
        '
        'chkRelease
        '
        Me.chkRelease.AutoSize = True
        Me.chkRelease.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRelease.Location = New System.Drawing.Point(6, 87)
        Me.chkRelease.Name = "chkRelease"
        Me.chkRelease.Size = New System.Drawing.Size(92, 17)
        Me.chkRelease.TabIndex = 4
        Me.chkRelease.Text = "Release Date"
        Me.chkRelease.UseVisualStyleBackColor = True
        '
        'chkYear
        '
        Me.chkYear.AutoSize = True
        Me.chkYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkYear.Location = New System.Drawing.Point(6, 36)
        Me.chkYear.Name = "chkYear"
        Me.chkYear.Size = New System.Drawing.Size(47, 17)
        Me.chkYear.TabIndex = 1
        Me.chkYear.Text = "Year"
        Me.chkYear.UseVisualStyleBackColor = True
        '
        'chkTitle
        '
        Me.chkTitle.AutoSize = True
        Me.chkTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTitle.Location = New System.Drawing.Point(6, 19)
        Me.chkTitle.Name = "chkTitle"
        Me.chkTitle.Size = New System.Drawing.Size(47, 17)
        Me.chkTitle.TabIndex = 0
        Me.chkTitle.Text = "Title"
        Me.chkTitle.UseVisualStyleBackColor = True
        '
        'gbScraperFieldsCredits
        '
        Me.gbScraperFieldsCredits.Controls.Add(Me.chkFullCrew)
        Me.gbScraperFieldsCredits.Controls.Add(Me.chkProducers)
        Me.gbScraperFieldsCredits.Controls.Add(Me.chkMusicBy)
        Me.gbScraperFieldsCredits.Controls.Add(Me.chkCrew)
        Me.gbScraperFieldsCredits.Location = New System.Drawing.Point(255, 53)
        Me.gbScraperFieldsCredits.Name = "gbScraperFieldsCredits"
        Me.gbScraperFieldsCredits.Size = New System.Drawing.Size(145, 91)
        Me.gbScraperFieldsCredits.TabIndex = 79
        Me.gbScraperFieldsCredits.TabStop = False
        Me.gbScraperFieldsCredits.Text = "Credits"
        '
        'frmIMDBInfoSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(652, 388)
        Me.Controls.Add(Me.pnlSettings)
        Me.Controls.Add(Me.lblVersion)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIMDBInfoSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlSettings.ResumeLayout(False)
        Me.gbScraperOptions.ResumeLayout(False)
        Me.gbScraperOptions.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbScraperFields.ResumeLayout(False)
        Me.gbScraperFields.PerformLayout()
        Me.gbScraperFieldsCredits.ResumeLayout(False)
        Me.gbScraperFieldsCredits.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents cbEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents gbScraperFields As System.Windows.Forms.GroupBox
    Friend WithEvents chkCertification As System.Windows.Forms.CheckBox
    Friend WithEvents chkCountry As System.Windows.Forms.CheckBox
    Friend WithEvents chkTop250 As System.Windows.Forms.CheckBox
    Friend WithEvents chkCrew As System.Windows.Forms.CheckBox
    Friend WithEvents chkMusicBy As System.Windows.Forms.CheckBox
    Friend WithEvents chkFullCrew As System.Windows.Forms.CheckBox
    Friend WithEvents chkProducers As System.Windows.Forms.CheckBox
    Friend WithEvents chkWriters As System.Windows.Forms.CheckBox
    Friend WithEvents chkStudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkOutline As System.Windows.Forms.CheckBox
    Friend WithEvents chkGenre As System.Windows.Forms.CheckBox
    Friend WithEvents chkDirector As System.Windows.Forms.CheckBox
    Friend WithEvents chkTagline As System.Windows.Forms.CheckBox
    Friend WithEvents chkCast As System.Windows.Forms.CheckBox
    Friend WithEvents chkVotes As System.Windows.Forms.CheckBox
    Friend WithEvents chkTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkRelease As System.Windows.Forms.CheckBox
    Friend WithEvents chkYear As System.Windows.Forms.CheckBox
    Friend WithEvents chkTitle As System.Windows.Forms.CheckBox
    Friend WithEvents gbScraperOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkTvTitles As System.Windows.Forms.CheckBox
    Friend WithEvents chkPartialTitles As System.Windows.Forms.CheckBox
    Friend WithEvents chkPopularTitles As System.Windows.Forms.CheckBox
    Friend WithEvents chkVideoTitles As System.Windows.Forms.CheckBox
    Friend WithEvents chkFallBackworldwide As System.Windows.Forms.CheckBox
    Friend WithEvents cbForceTitleLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents lblForceTitleLanguage As System.Windows.Forms.Label
    Friend WithEvents gbScraperFieldsCredits As System.Windows.Forms.GroupBox

End Class
