<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTVInfoSettingsHolder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTVInfoSettingsHolder))
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblScrapeOrder = New System.Windows.Forms.Label()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.gbScraperFields = New System.Windows.Forms.GroupBox()
        Me.gbScraperFieldsShow = New System.Windows.Forms.GroupBox()
        Me.chkScraperShowVotes = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowRuntime = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowStatus = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowRating = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowActors = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowStudio = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowPremiered = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowEGU = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowMPAA = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowPlot = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowGenre = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowTitle = New System.Windows.Forms.CheckBox()
        Me.gbScraperFieldsEpisode = New System.Windows.Forms.GroupBox()
        Me.chkScraperEpGuestStars = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpVotes = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpActors = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpCredits = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpDirector = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpPlot = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpRating = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpAired = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpTitle = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpEpisode = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpSeason = New System.Windows.Forms.CheckBox()
        Me.gbTMDB = New System.Windows.Forms.GroupBox()
        Me.lblEMMAPI = New System.Windows.Forms.Label()
        Me.gbLanguage = New System.Windows.Forms.GroupBox()
        Me.lblTVLanguagePreferred = New System.Windows.Forms.Label()
        Me.cbTVScraperLanguage = New System.Windows.Forms.ComboBox()
        Me.btnUnlockAPI = New System.Windows.Forms.Button()
        Me.lblTVDBMirror = New System.Windows.Forms.Label()
        Me.txtTVDBMirror = New System.Windows.Forms.TextBox()
        Me.pbTVDB = New System.Windows.Forms.PictureBox()
        Me.lblTVDBApiKey = New System.Windows.Forms.Label()
        Me.txtApiKey = New System.Windows.Forms.TextBox()
        Me.lblModuleInfo = New System.Windows.Forms.Label()
        Me.pbModuleLogo = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.gbScraperFields.SuspendLayout()
        Me.gbScraperFieldsShow.SuspendLayout()
        Me.gbScraperFieldsEpisode.SuspendLayout()
        Me.gbTMDB.SuspendLayout()
        Me.gbLanguage.SuspendLayout()
        CType(Me.pbTVDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbModuleLogo, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'chkEnabled
        '
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Location = New System.Drawing.Point(10, 5)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(68, 17)
        Me.chkEnabled.TabIndex = 0
        Me.chkEnabled.Text = "Enabled"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Controls.Add(Me.lblScrapeOrder)
        Me.Panel1.Controls.Add(Me.btnDown)
        Me.Panel1.Controls.Add(Me.chkEnabled)
        Me.Panel1.Controls.Add(Me.btnUp)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1125, 25)
        Me.Panel1.TabIndex = 0
        '
        'lblScrapeOrder
        '
        Me.lblScrapeOrder.AutoSize = True
        Me.lblScrapeOrder.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScrapeOrder.Location = New System.Drawing.Point(500, 7)
        Me.lblScrapeOrder.Name = "lblScrapeOrder"
        Me.lblScrapeOrder.Size = New System.Drawing.Size(58, 12)
        Me.lblScrapeOrder.TabIndex = 1
        Me.lblScrapeOrder.Text = "Scraper order"
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
        Me.pnlSettings.Controls.Add(Me.gbScraperFields)
        Me.pnlSettings.Controls.Add(Me.gbTMDB)
        Me.pnlSettings.Controls.Add(Me.lblModuleInfo)
        Me.pnlSettings.Controls.Add(Me.pbModuleLogo)
        Me.pnlSettings.Controls.Add(Me.Panel1)
        Me.pnlSettings.Location = New System.Drawing.Point(12, 1)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(617, 369)
        Me.pnlSettings.TabIndex = 0
        '
        'gbScraperFields
        '
        Me.gbScraperFields.Controls.Add(Me.gbScraperFieldsShow)
        Me.gbScraperFields.Controls.Add(Me.gbScraperFieldsEpisode)
        Me.gbScraperFields.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbScraperFields.Location = New System.Drawing.Point(10, 156)
        Me.gbScraperFields.Name = "gbScraperFields"
        Me.gbScraperFields.Size = New System.Drawing.Size(403, 139)
        Me.gbScraperFields.TabIndex = 8
        Me.gbScraperFields.TabStop = False
        Me.gbScraperFields.Text = "Scraper Fields"
        '
        'gbScraperFieldsShow
        '
        Me.gbScraperFieldsShow.Controls.Add(Me.chkScraperShowVotes)
        Me.gbScraperFieldsShow.Controls.Add(Me.chkScraperShowRuntime)
        Me.gbScraperFieldsShow.Controls.Add(Me.chkScraperShowStatus)
        Me.gbScraperFieldsShow.Controls.Add(Me.chkScraperShowRating)
        Me.gbScraperFieldsShow.Controls.Add(Me.chkScraperShowActors)
        Me.gbScraperFieldsShow.Controls.Add(Me.chkScraperShowStudio)
        Me.gbScraperFieldsShow.Controls.Add(Me.chkScraperShowPremiered)
        Me.gbScraperFieldsShow.Controls.Add(Me.chkScraperShowEGU)
        Me.gbScraperFieldsShow.Controls.Add(Me.chkScraperShowMPAA)
        Me.gbScraperFieldsShow.Controls.Add(Me.chkScraperShowPlot)
        Me.gbScraperFieldsShow.Controls.Add(Me.chkScraperShowGenre)
        Me.gbScraperFieldsShow.Controls.Add(Me.chkScraperShowTitle)
        Me.gbScraperFieldsShow.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbScraperFieldsShow.Location = New System.Drawing.Point(3, 14)
        Me.gbScraperFieldsShow.Name = "gbScraperFieldsShow"
        Me.gbScraperFieldsShow.Size = New System.Drawing.Size(213, 115)
        Me.gbScraperFieldsShow.TabIndex = 0
        Me.gbScraperFieldsShow.TabStop = False
        Me.gbScraperFieldsShow.Text = "Show"
        '
        'chkScraperShowVotes
        '
        Me.chkScraperShowVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowVotes.Location = New System.Drawing.Point(129, 94)
        Me.chkScraperShowVotes.Name = "chkScraperShowVotes"
        Me.chkScraperShowVotes.Size = New System.Drawing.Size(78, 17)
        Me.chkScraperShowVotes.TabIndex = 11
        Me.chkScraperShowVotes.Text = "Votes"
        Me.chkScraperShowVotes.UseVisualStyleBackColor = True
        '
        'chkScraperShowRuntime
        '
        Me.chkScraperShowRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowRuntime.Location = New System.Drawing.Point(6, 94)
        Me.chkScraperShowRuntime.Name = "chkScraperShowRuntime"
        Me.chkScraperShowRuntime.Size = New System.Drawing.Size(78, 17)
        Me.chkScraperShowRuntime.TabIndex = 10
        Me.chkScraperShowRuntime.Text = "Runtime"
        Me.chkScraperShowRuntime.UseVisualStyleBackColor = True
        '
        'chkScraperShowStatus
        '
        Me.chkScraperShowStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowStatus.Location = New System.Drawing.Point(130, 77)
        Me.chkScraperShowStatus.Name = "chkScraperShowStatus"
        Me.chkScraperShowStatus.Size = New System.Drawing.Size(78, 17)
        Me.chkScraperShowStatus.TabIndex = 9
        Me.chkScraperShowStatus.Text = "Status"
        Me.chkScraperShowStatus.UseVisualStyleBackColor = True
        '
        'chkScraperShowRating
        '
        Me.chkScraperShowRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowRating.Location = New System.Drawing.Point(130, 29)
        Me.chkScraperShowRating.Name = "chkScraperShowRating"
        Me.chkScraperShowRating.Size = New System.Drawing.Size(78, 17)
        Me.chkScraperShowRating.TabIndex = 6
        Me.chkScraperShowRating.Text = "Rating"
        Me.chkScraperShowRating.UseVisualStyleBackColor = True
        '
        'chkScraperShowActors
        '
        Me.chkScraperShowActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowActors.Location = New System.Drawing.Point(130, 61)
        Me.chkScraperShowActors.Name = "chkScraperShowActors"
        Me.chkScraperShowActors.Size = New System.Drawing.Size(78, 17)
        Me.chkScraperShowActors.TabIndex = 8
        Me.chkScraperShowActors.Text = "Actors"
        Me.chkScraperShowActors.UseVisualStyleBackColor = True
        '
        'chkScraperShowStudio
        '
        Me.chkScraperShowStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowStudio.Location = New System.Drawing.Point(130, 45)
        Me.chkScraperShowStudio.Name = "chkScraperShowStudio"
        Me.chkScraperShowStudio.Size = New System.Drawing.Size(78, 17)
        Me.chkScraperShowStudio.TabIndex = 7
        Me.chkScraperShowStudio.Text = "Studio"
        Me.chkScraperShowStudio.UseVisualStyleBackColor = True
        '
        'chkScraperShowPremiered
        '
        Me.chkScraperShowPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowPremiered.Location = New System.Drawing.Point(130, 13)
        Me.chkScraperShowPremiered.Name = "chkScraperShowPremiered"
        Me.chkScraperShowPremiered.Size = New System.Drawing.Size(78, 17)
        Me.chkScraperShowPremiered.TabIndex = 5
        Me.chkScraperShowPremiered.Text = "Premiered"
        Me.chkScraperShowPremiered.UseVisualStyleBackColor = True
        '
        'chkScraperShowEGU
        '
        Me.chkScraperShowEGU.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowEGU.Location = New System.Drawing.Point(6, 29)
        Me.chkScraperShowEGU.Name = "chkScraperShowEGU"
        Me.chkScraperShowEGU.Size = New System.Drawing.Size(118, 17)
        Me.chkScraperShowEGU.TabIndex = 1
        Me.chkScraperShowEGU.Text = "EpisodeGuideURL"
        Me.chkScraperShowEGU.UseVisualStyleBackColor = True
        '
        'chkScraperShowMPAA
        '
        Me.chkScraperShowMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowMPAA.Location = New System.Drawing.Point(6, 61)
        Me.chkScraperShowMPAA.Name = "chkScraperShowMPAA"
        Me.chkScraperShowMPAA.Size = New System.Drawing.Size(119, 17)
        Me.chkScraperShowMPAA.TabIndex = 3
        Me.chkScraperShowMPAA.Text = "MPAA"
        Me.chkScraperShowMPAA.UseVisualStyleBackColor = True
        '
        'chkScraperShowPlot
        '
        Me.chkScraperShowPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowPlot.Location = New System.Drawing.Point(6, 77)
        Me.chkScraperShowPlot.Name = "chkScraperShowPlot"
        Me.chkScraperShowPlot.Size = New System.Drawing.Size(119, 17)
        Me.chkScraperShowPlot.TabIndex = 4
        Me.chkScraperShowPlot.Text = "Plot"
        Me.chkScraperShowPlot.UseVisualStyleBackColor = True
        '
        'chkScraperShowGenre
        '
        Me.chkScraperShowGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowGenre.Location = New System.Drawing.Point(6, 45)
        Me.chkScraperShowGenre.Name = "chkScraperShowGenre"
        Me.chkScraperShowGenre.Size = New System.Drawing.Size(118, 17)
        Me.chkScraperShowGenre.TabIndex = 2
        Me.chkScraperShowGenre.Text = "Genre"
        Me.chkScraperShowGenre.UseVisualStyleBackColor = True
        '
        'chkScraperShowTitle
        '
        Me.chkScraperShowTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowTitle.Location = New System.Drawing.Point(6, 13)
        Me.chkScraperShowTitle.Name = "chkScraperShowTitle"
        Me.chkScraperShowTitle.Size = New System.Drawing.Size(118, 17)
        Me.chkScraperShowTitle.TabIndex = 0
        Me.chkScraperShowTitle.Text = "Title"
        Me.chkScraperShowTitle.UseVisualStyleBackColor = True
        '
        'gbScraperFieldsEpisode
        '
        Me.gbScraperFieldsEpisode.Controls.Add(Me.chkScraperEpGuestStars)
        Me.gbScraperFieldsEpisode.Controls.Add(Me.chkScraperEpVotes)
        Me.gbScraperFieldsEpisode.Controls.Add(Me.chkScraperEpActors)
        Me.gbScraperFieldsEpisode.Controls.Add(Me.chkScraperEpCredits)
        Me.gbScraperFieldsEpisode.Controls.Add(Me.chkScraperEpDirector)
        Me.gbScraperFieldsEpisode.Controls.Add(Me.chkScraperEpPlot)
        Me.gbScraperFieldsEpisode.Controls.Add(Me.chkScraperEpRating)
        Me.gbScraperFieldsEpisode.Controls.Add(Me.chkScraperEpAired)
        Me.gbScraperFieldsEpisode.Controls.Add(Me.chkScraperEpTitle)
        Me.gbScraperFieldsEpisode.Controls.Add(Me.chkScraperEpEpisode)
        Me.gbScraperFieldsEpisode.Controls.Add(Me.chkScraperEpSeason)
        Me.gbScraperFieldsEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbScraperFieldsEpisode.Location = New System.Drawing.Point(219, 14)
        Me.gbScraperFieldsEpisode.Name = "gbScraperFieldsEpisode"
        Me.gbScraperFieldsEpisode.Size = New System.Drawing.Size(181, 115)
        Me.gbScraperFieldsEpisode.TabIndex = 1
        Me.gbScraperFieldsEpisode.TabStop = False
        Me.gbScraperFieldsEpisode.Text = "Episode"
        '
        'chkScraperEpGuestStars
        '
        Me.chkScraperEpGuestStars.AutoSize = True
        Me.chkScraperEpGuestStars.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkScraperEpGuestStars.Location = New System.Drawing.Point(6, 94)
        Me.chkScraperEpGuestStars.Name = "chkScraperEpGuestStars"
        Me.chkScraperEpGuestStars.Size = New System.Drawing.Size(84, 17)
        Me.chkScraperEpGuestStars.TabIndex = 10
        Me.chkScraperEpGuestStars.Text = "Guest Stars"
        Me.chkScraperEpGuestStars.UseVisualStyleBackColor = True
        '
        'chkScraperEpVotes
        '
        Me.chkScraperEpVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpVotes.Location = New System.Drawing.Point(94, 77)
        Me.chkScraperEpVotes.Name = "chkScraperEpVotes"
        Me.chkScraperEpVotes.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpVotes.TabIndex = 9
        Me.chkScraperEpVotes.Text = "Votes"
        Me.chkScraperEpVotes.UseVisualStyleBackColor = True
        '
        'chkScraperEpActors
        '
        Me.chkScraperEpActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpActors.Location = New System.Drawing.Point(94, 60)
        Me.chkScraperEpActors.Name = "chkScraperEpActors"
        Me.chkScraperEpActors.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpActors.TabIndex = 0
        Me.chkScraperEpActors.Text = "Actors"
        Me.chkScraperEpActors.UseVisualStyleBackColor = True
        '
        'chkScraperEpCredits
        '
        Me.chkScraperEpCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpCredits.Location = New System.Drawing.Point(94, 44)
        Me.chkScraperEpCredits.Name = "chkScraperEpCredits"
        Me.chkScraperEpCredits.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpCredits.TabIndex = 8
        Me.chkScraperEpCredits.Text = "Credits"
        Me.chkScraperEpCredits.UseVisualStyleBackColor = True
        '
        'chkScraperEpDirector
        '
        Me.chkScraperEpDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpDirector.Location = New System.Drawing.Point(94, 28)
        Me.chkScraperEpDirector.Name = "chkScraperEpDirector"
        Me.chkScraperEpDirector.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpDirector.TabIndex = 7
        Me.chkScraperEpDirector.Text = "Director"
        Me.chkScraperEpDirector.UseVisualStyleBackColor = True
        '
        'chkScraperEpPlot
        '
        Me.chkScraperEpPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpPlot.Location = New System.Drawing.Point(94, 12)
        Me.chkScraperEpPlot.Name = "chkScraperEpPlot"
        Me.chkScraperEpPlot.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpPlot.TabIndex = 6
        Me.chkScraperEpPlot.Text = "Plot"
        Me.chkScraperEpPlot.UseVisualStyleBackColor = True
        '
        'chkScraperEpRating
        '
        Me.chkScraperEpRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpRating.Location = New System.Drawing.Point(6, 77)
        Me.chkScraperEpRating.Name = "chkScraperEpRating"
        Me.chkScraperEpRating.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpRating.TabIndex = 5
        Me.chkScraperEpRating.Text = "Rating"
        Me.chkScraperEpRating.UseVisualStyleBackColor = True
        '
        'chkScraperEpAired
        '
        Me.chkScraperEpAired.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpAired.Location = New System.Drawing.Point(6, 61)
        Me.chkScraperEpAired.Name = "chkScraperEpAired"
        Me.chkScraperEpAired.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpAired.TabIndex = 4
        Me.chkScraperEpAired.Text = "Aired"
        Me.chkScraperEpAired.UseVisualStyleBackColor = True
        '
        'chkScraperEpTitle
        '
        Me.chkScraperEpTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpTitle.Location = New System.Drawing.Point(6, 13)
        Me.chkScraperEpTitle.Name = "chkScraperEpTitle"
        Me.chkScraperEpTitle.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpTitle.TabIndex = 0
        Me.chkScraperEpTitle.Text = "Title"
        Me.chkScraperEpTitle.UseVisualStyleBackColor = True
        '
        'chkScraperEpEpisode
        '
        Me.chkScraperEpEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpEpisode.Location = New System.Drawing.Point(6, 45)
        Me.chkScraperEpEpisode.Name = "chkScraperEpEpisode"
        Me.chkScraperEpEpisode.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpEpisode.TabIndex = 3
        Me.chkScraperEpEpisode.Text = "Episode"
        Me.chkScraperEpEpisode.UseVisualStyleBackColor = True
        '
        'chkScraperEpSeason
        '
        Me.chkScraperEpSeason.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpSeason.Location = New System.Drawing.Point(6, 29)
        Me.chkScraperEpSeason.Name = "chkScraperEpSeason"
        Me.chkScraperEpSeason.Size = New System.Drawing.Size(67, 17)
        Me.chkScraperEpSeason.TabIndex = 2
        Me.chkScraperEpSeason.Text = "Season"
        Me.chkScraperEpSeason.UseVisualStyleBackColor = True
        '
        'gbTMDB
        '
        Me.gbTMDB.Controls.Add(Me.lblEMMAPI)
        Me.gbTMDB.Controls.Add(Me.gbLanguage)
        Me.gbTMDB.Controls.Add(Me.btnUnlockAPI)
        Me.gbTMDB.Controls.Add(Me.lblTVDBMirror)
        Me.gbTMDB.Controls.Add(Me.txtTVDBMirror)
        Me.gbTMDB.Controls.Add(Me.pbTVDB)
        Me.gbTMDB.Controls.Add(Me.lblTVDBApiKey)
        Me.gbTMDB.Controls.Add(Me.txtApiKey)
        Me.gbTMDB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTMDB.Location = New System.Drawing.Point(11, 31)
        Me.gbTMDB.Name = "gbTMDB"
        Me.gbTMDB.Size = New System.Drawing.Size(603, 119)
        Me.gbTMDB.TabIndex = 97
        Me.gbTMDB.TabStop = False
        Me.gbTMDB.Text = "TMDB"
        '
        'lblEMMAPI
        '
        Me.lblEMMAPI.AutoSize = True
        Me.lblEMMAPI.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblEMMAPI.Location = New System.Drawing.Point(139, 18)
        Me.lblEMMAPI.Name = "lblEMMAPI"
        Me.lblEMMAPI.Size = New System.Drawing.Size(220, 13)
        Me.lblEMMAPI.TabIndex = 76
        Me.lblEMMAPI.Text = "Ember Media Manager Embedded API Key"
        '
        'gbLanguage
        '
        Me.gbLanguage.Controls.Add(Me.lblTVLanguagePreferred)
        Me.gbLanguage.Controls.Add(Me.cbTVScraperLanguage)
        Me.gbLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbLanguage.Location = New System.Drawing.Point(407, 13)
        Me.gbLanguage.Name = "gbLanguage"
        Me.gbLanguage.Size = New System.Drawing.Size(190, 100)
        Me.gbLanguage.TabIndex = 8
        Me.gbLanguage.TabStop = False
        Me.gbLanguage.Text = "Language"
        '
        'lblTVLanguagePreferred
        '
        Me.lblTVLanguagePreferred.AutoSize = True
        Me.lblTVLanguagePreferred.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTVLanguagePreferred.Location = New System.Drawing.Point(10, 24)
        Me.lblTVLanguagePreferred.Name = "lblTVLanguagePreferred"
        Me.lblTVLanguagePreferred.Size = New System.Drawing.Size(111, 13)
        Me.lblTVLanguagePreferred.TabIndex = 0
        Me.lblTVLanguagePreferred.Text = "Preferred Language:"
        '
        'cbTVScraperLanguage
        '
        Me.cbTVScraperLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVScraperLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbTVScraperLanguage.Location = New System.Drawing.Point(12, 39)
        Me.cbTVScraperLanguage.Name = "cbTVScraperLanguage"
        Me.cbTVScraperLanguage.Size = New System.Drawing.Size(166, 21)
        Me.cbTVScraperLanguage.TabIndex = 1
        '
        'btnUnlockAPI
        '
        Me.btnUnlockAPI.Location = New System.Drawing.Point(9, 32)
        Me.btnUnlockAPI.Name = "btnUnlockAPI"
        Me.btnUnlockAPI.Size = New System.Drawing.Size(127, 23)
        Me.btnUnlockAPI.TabIndex = 75
        Me.btnUnlockAPI.Text = "Use my own API key"
        Me.btnUnlockAPI.UseVisualStyleBackColor = True
        '
        'lblTVDBMirror
        '
        Me.lblTVDBMirror.AutoSize = True
        Me.lblTVDBMirror.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTVDBMirror.Location = New System.Drawing.Point(6, 57)
        Me.lblTVDBMirror.Name = "lblTVDBMirror"
        Me.lblTVDBMirror.Size = New System.Drawing.Size(72, 13)
        Me.lblTVDBMirror.TabIndex = 6
        Me.lblTVDBMirror.Text = "TVDB Mirror:"
        '
        'txtTVDBMirror
        '
        Me.txtTVDBMirror.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVDBMirror.Location = New System.Drawing.Point(8, 72)
        Me.txtTVDBMirror.Name = "txtTVDBMirror"
        Me.txtTVDBMirror.Size = New System.Drawing.Size(189, 22)
        Me.txtTVDBMirror.TabIndex = 7
        '
        'pbTVDB
        '
        Me.pbTVDB.Image = CType(resources.GetObject("pbTVDB.Image"), System.Drawing.Image)
        Me.pbTVDB.Location = New System.Drawing.Point(385, 34)
        Me.pbTVDB.Name = "pbTVDB"
        Me.pbTVDB.Size = New System.Drawing.Size(16, 16)
        Me.pbTVDB.TabIndex = 5
        Me.pbTVDB.TabStop = False
        '
        'lblTVDBApiKey
        '
        Me.lblTVDBApiKey.AutoSize = True
        Me.lblTVDBApiKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTVDBApiKey.Location = New System.Drawing.Point(6, 18)
        Me.lblTVDBApiKey.Name = "lblTVDBApiKey"
        Me.lblTVDBApiKey.Size = New System.Drawing.Size(76, 13)
        Me.lblTVDBApiKey.TabIndex = 0
        Me.lblTVDBApiKey.Text = "TVDB API Key:"
        '
        'txtApiKey
        '
        Me.txtApiKey.Enabled = False
        Me.txtApiKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApiKey.Location = New System.Drawing.Point(142, 32)
        Me.txtApiKey.Name = "txtApiKey"
        Me.txtApiKey.Size = New System.Drawing.Size(237, 22)
        Me.txtApiKey.TabIndex = 1
        '
        'lblModuleInfo
        '
        Me.lblModuleInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModuleInfo.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblModuleInfo.ForeColor = System.Drawing.Color.Blue
        Me.lblModuleInfo.Location = New System.Drawing.Point(37, 337)
        Me.lblModuleInfo.Name = "lblModuleInfo"
        Me.lblModuleInfo.Size = New System.Drawing.Size(225, 31)
        Me.lblModuleInfo.TabIndex = 1
        Me.lblModuleInfo.Text = "These settings are specific to this module." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please refer to the global settings " & _
    "for more options."
        Me.lblModuleInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pbModuleLogo
        '
        Me.pbModuleLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pbModuleLogo.Image = CType(resources.GetObject("pbModuleLogo.Image"), System.Drawing.Image)
        Me.pbModuleLogo.Location = New System.Drawing.Point(3, 335)
        Me.pbModuleLogo.Name = "pbModuleLogo"
        Me.pbModuleLogo.Size = New System.Drawing.Size(30, 31)
        Me.pbModuleLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbModuleLogo.TabIndex = 96
        Me.pbModuleLogo.TabStop = False
        '
        'frmTVInfoSettingsHolder
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
        Me.Name = "frmTVInfoSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlSettings.ResumeLayout(False)
        Me.gbScraperFields.ResumeLayout(False)
        Me.gbScraperFieldsShow.ResumeLayout(False)
        Me.gbScraperFieldsEpisode.ResumeLayout(False)
        Me.gbScraperFieldsEpisode.PerformLayout()
        Me.gbTMDB.ResumeLayout(False)
        Me.gbTMDB.PerformLayout()
        Me.gbLanguage.ResumeLayout(False)
        Me.gbLanguage.PerformLayout()
        CType(Me.pbTVDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbModuleLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents lblScrapeOrder As System.Windows.Forms.Label
    Friend WithEvents lblModuleInfo As System.Windows.Forms.Label
    Friend WithEvents pbModuleLogo As System.Windows.Forms.PictureBox
    Friend WithEvents gbTMDB As System.Windows.Forms.GroupBox
    Friend WithEvents pbTVDB As System.Windows.Forms.PictureBox
    Friend WithEvents lblTVDBApiKey As System.Windows.Forms.Label
    Friend WithEvents txtApiKey As System.Windows.Forms.TextBox
    Friend WithEvents lblTVDBMirror As System.Windows.Forms.Label
    Friend WithEvents txtTVDBMirror As System.Windows.Forms.TextBox
    Friend WithEvents gbScraperFields As System.Windows.Forms.GroupBox
    Friend WithEvents gbScraperFieldsShow As System.Windows.Forms.GroupBox
    Friend WithEvents chkScraperShowRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowActors As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowStudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowPremiered As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowEGU As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowGenre As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowTitle As System.Windows.Forms.CheckBox
    Friend WithEvents gbScraperFieldsEpisode As System.Windows.Forms.GroupBox
    Friend WithEvents chkScraperEpActors As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpCredits As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpDirector As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpAired As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpEpisode As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpSeason As System.Windows.Forms.CheckBox
    Friend WithEvents gbLanguage As System.Windows.Forms.GroupBox
    Friend WithEvents lblTVLanguagePreferred As System.Windows.Forms.Label
    Friend WithEvents cbTVScraperLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents chkScraperShowStatus As System.Windows.Forms.CheckBox
    Friend WithEvents lblEMMAPI As System.Windows.Forms.Label
    Friend WithEvents btnUnlockAPI As System.Windows.Forms.Button
    Friend WithEvents chkScraperShowRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowVotes As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpVotes As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpGuestStars As System.Windows.Forms.CheckBox

End Class
