﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTMDBInfoSettingsHolder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTMDBInfoSettingsHolder))
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.gbTMDBGlobalOpts = New System.Windows.Forms.GroupBox()
        Me.chkGetAdultItems = New System.Windows.Forms.CheckBox()
        Me.pbTMDBApiKeyInfo = New System.Windows.Forms.PictureBox()
        Me.chkFallBackEng = New System.Windows.Forms.CheckBox()
        Me.cbTMDBPrefLanguage = New System.Windows.Forms.ComboBox()
        Me.lblTMDBPrefLanguage = New System.Windows.Forms.Label()
        Me.lblTMDBApiKey = New System.Windows.Forms.Label()
        Me.txtTMDBApiKey = New System.Windows.Forms.TextBox()
        Me.cbEnabled = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblScraperOrder = New System.Windows.Forms.Label()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.gbTMDBScraperOpts = New System.Windows.Forms.GroupBox()
        Me.chkTMDBCleanPlotOutline = New System.Windows.Forms.CheckBox()
        Me.chkCrew = New System.Windows.Forms.CheckBox()
        Me.chkCountry = New System.Windows.Forms.CheckBox()
        Me.chkStudio = New System.Windows.Forms.CheckBox()
        Me.chkRuntime = New System.Windows.Forms.CheckBox()
        Me.chkPlot = New System.Windows.Forms.CheckBox()
        Me.chkGenre = New System.Windows.Forms.CheckBox()
        Me.chkTagline = New System.Windows.Forms.CheckBox()
        Me.chkCast = New System.Windows.Forms.CheckBox()
        Me.chkVotes = New System.Windows.Forms.CheckBox()
        Me.chkTrailer = New System.Windows.Forms.CheckBox()
        Me.chkRating = New System.Windows.Forms.CheckBox()
        Me.chkRelease = New System.Windows.Forms.CheckBox()
        Me.chkMPAA = New System.Windows.Forms.CheckBox()
        Me.chkYear = New System.Windows.Forms.CheckBox()
        Me.chkTitle = New System.Windows.Forms.CheckBox()
        Me.chkCollection = New System.Windows.Forms.CheckBox()
        Me.gbTMDBGlobalOpts.SuspendLayout()
        CType(Me.pbTMDBApiKeyInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTMDBScraperOpts.SuspendLayout()
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
        'gbTMDBGlobalOpts
        '
        Me.gbTMDBGlobalOpts.Controls.Add(Me.chkGetAdultItems)
        Me.gbTMDBGlobalOpts.Controls.Add(Me.pbTMDBApiKeyInfo)
        Me.gbTMDBGlobalOpts.Controls.Add(Me.chkFallBackEng)
        Me.gbTMDBGlobalOpts.Controls.Add(Me.cbTMDBPrefLanguage)
        Me.gbTMDBGlobalOpts.Controls.Add(Me.lblTMDBPrefLanguage)
        Me.gbTMDBGlobalOpts.Controls.Add(Me.lblTMDBApiKey)
        Me.gbTMDBGlobalOpts.Controls.Add(Me.txtTMDBApiKey)
        Me.gbTMDBGlobalOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTMDBGlobalOpts.Location = New System.Drawing.Point(11, 31)
        Me.gbTMDBGlobalOpts.Name = "gbTMDBGlobalOpts"
        Me.gbTMDBGlobalOpts.Size = New System.Drawing.Size(513, 102)
        Me.gbTMDBGlobalOpts.TabIndex = 1
        Me.gbTMDBGlobalOpts.TabStop = False
        Me.gbTMDBGlobalOpts.Text = "TMDB"
        '
        'chkGetAdultItems
        '
        Me.chkGetAdultItems.AutoSize = True
        Me.chkGetAdultItems.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkGetAdultItems.Location = New System.Drawing.Point(339, 67)
        Me.chkGetAdultItems.Name = "chkGetAdultItems"
        Me.chkGetAdultItems.Size = New System.Drawing.Size(125, 17)
        Me.chkGetAdultItems.TabIndex = 6
        Me.chkGetAdultItems.Text = "Include Adult Items"
        Me.chkGetAdultItems.UseVisualStyleBackColor = True
        '
        'pbTMDBApiKeyInfo
        '
        Me.pbTMDBApiKeyInfo.Image = CType(resources.GetObject("pbTMDBApiKeyInfo.Image"), System.Drawing.Image)
        Me.pbTMDBApiKeyInfo.Location = New System.Drawing.Point(491, 32)
        Me.pbTMDBApiKeyInfo.Name = "pbTMDBApiKeyInfo"
        Me.pbTMDBApiKeyInfo.Size = New System.Drawing.Size(16, 16)
        Me.pbTMDBApiKeyInfo.TabIndex = 5
        Me.pbTMDBApiKeyInfo.TabStop = False
        '
        'chkFallBackEng
        '
        Me.chkFallBackEng.AutoSize = True
        Me.chkFallBackEng.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFallBackEng.Location = New System.Drawing.Point(190, 67)
        Me.chkFallBackEng.Name = "chkFallBackEng"
        Me.chkFallBackEng.Size = New System.Drawing.Size(129, 17)
        Me.chkFallBackEng.TabIndex = 4
        Me.chkFallBackEng.Text = "Fall back on english"
        Me.chkFallBackEng.UseVisualStyleBackColor = True
        '
        'cbTMDBPrefLanguage
        '
        Me.cbTMDBPrefLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTMDBPrefLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cbTMDBPrefLanguage.FormattingEnabled = True
        Me.cbTMDBPrefLanguage.Items.AddRange(New Object() {"bg", "cs", "da", "de", "el", "en", "es", "fi", "fr", "he", "hu", "it", "nb", "nl", "no", "pl", "pt", "ru", "sk", "sv", "ta", "tr", "uk", "vi", "xx", "zh"})
        Me.cbTMDBPrefLanguage.Location = New System.Drawing.Point(123, 65)
        Me.cbTMDBPrefLanguage.Name = "cbTMDBPrefLanguage"
        Me.cbTMDBPrefLanguage.Size = New System.Drawing.Size(45, 21)
        Me.cbTMDBPrefLanguage.TabIndex = 3
        '
        'lblTMDBPrefLanguage
        '
        Me.lblTMDBPrefLanguage.AutoSize = True
        Me.lblTMDBPrefLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblTMDBPrefLanguage.Location = New System.Drawing.Point(6, 68)
        Me.lblTMDBPrefLanguage.Name = "lblTMDBPrefLanguage"
        Me.lblTMDBPrefLanguage.Size = New System.Drawing.Size(111, 13)
        Me.lblTMDBPrefLanguage.TabIndex = 2
        Me.lblTMDBPrefLanguage.Text = "Preferred Language:"
        '
        'lblTMDBApiKey
        '
        Me.lblTMDBApiKey.AutoSize = True
        Me.lblTMDBApiKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTMDBApiKey.Location = New System.Drawing.Point(6, 18)
        Me.lblTMDBApiKey.Name = "lblTMDBApiKey"
        Me.lblTMDBApiKey.Size = New System.Drawing.Size(79, 13)
        Me.lblTMDBApiKey.TabIndex = 0
        Me.lblTMDBApiKey.Text = "TMDB API Key:"
        '
        'txtTMDBApiKey
        '
        Me.txtTMDBApiKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTMDBApiKey.Location = New System.Drawing.Point(8, 32)
        Me.txtTMDBApiKey.Name = "txtTMDBApiKey"
        Me.txtTMDBApiKey.Size = New System.Drawing.Size(477, 22)
        Me.txtTMDBApiKey.TabIndex = 1
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
        Me.Panel1.Controls.Add(Me.lblScraperOrder)
        Me.Panel1.Controls.Add(Me.btnDown)
        Me.Panel1.Controls.Add(Me.cbEnabled)
        Me.Panel1.Controls.Add(Me.btnUp)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1125, 25)
        Me.Panel1.TabIndex = 0
        '
        'lblScraperOrder
        '
        Me.lblScraperOrder.AutoSize = True
        Me.lblScraperOrder.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScraperOrder.Location = New System.Drawing.Point(500, 7)
        Me.lblScraperOrder.Name = "lblScraperOrder"
        Me.lblScraperOrder.Size = New System.Drawing.Size(58, 12)
        Me.lblScraperOrder.TabIndex = 1
        Me.lblScraperOrder.Text = "Scraper order"
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
        Me.pnlSettings.Controls.Add(Me.Label1)
        Me.pnlSettings.Controls.Add(Me.PictureBox1)
        Me.pnlSettings.Controls.Add(Me.Panel1)
        Me.pnlSettings.Controls.Add(Me.gbTMDBGlobalOpts)
        Me.pnlSettings.Controls.Add(Me.gbTMDBScraperOpts)
        Me.pnlSettings.Location = New System.Drawing.Point(12, 4)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(617, 369)
        Me.pnlSettings.TabIndex = 0
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
        'gbTMDBScraperOpts
        '
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkCollection)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkTMDBCleanPlotOutline)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkCrew)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkCountry)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkStudio)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkRuntime)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkPlot)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkGenre)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkTagline)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkCast)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkVotes)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkTrailer)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkRating)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkRelease)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkMPAA)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkYear)
        Me.gbTMDBScraperOpts.Controls.Add(Me.chkTitle)
        Me.gbTMDBScraperOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTMDBScraperOpts.Location = New System.Drawing.Point(11, 139)
        Me.gbTMDBScraperOpts.Name = "gbTMDBScraperOpts"
        Me.gbTMDBScraperOpts.Size = New System.Drawing.Size(513, 150)
        Me.gbTMDBScraperOpts.TabIndex = 3
        Me.gbTMDBScraperOpts.TabStop = False
        Me.gbTMDBScraperOpts.Text = "Scraper Fields"
        '
        'chkTMDBCleanPlotOutline
        '
        Me.chkTMDBCleanPlotOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTMDBCleanPlotOutline.Location = New System.Drawing.Point(315, 36)
        Me.chkTMDBCleanPlotOutline.Name = "chkTMDBCleanPlotOutline"
        Me.chkTMDBCleanPlotOutline.Size = New System.Drawing.Size(194, 17)
        Me.chkTMDBCleanPlotOutline.TabIndex = 75
        Me.chkTMDBCleanPlotOutline.Text = "Clean Plot/Outline"
        Me.chkTMDBCleanPlotOutline.UseVisualStyleBackColor = True
        '
        'chkCrew
        '
        Me.chkCrew.AutoSize = True
        Me.chkCrew.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCrew.Location = New System.Drawing.Point(315, 19)
        Me.chkCrew.Name = "chkCrew"
        Me.chkCrew.Size = New System.Drawing.Size(52, 17)
        Me.chkCrew.TabIndex = 19
        Me.chkCrew.Text = "Crew"
        Me.chkCrew.UseVisualStyleBackColor = True
        '
        'chkCountry
        '
        Me.chkCountry.AutoSize = True
        Me.chkCountry.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCountry.Location = New System.Drawing.Point(173, 123)
        Me.chkCountry.Name = "chkCountry"
        Me.chkCountry.Size = New System.Drawing.Size(67, 17)
        Me.chkCountry.TabIndex = 18
        Me.chkCountry.Text = "Country"
        Me.chkCountry.UseVisualStyleBackColor = True
        '
        'chkStudio
        '
        Me.chkStudio.AutoSize = True
        Me.chkStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStudio.Location = New System.Drawing.Point(173, 19)
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
        Me.chkRuntime.Location = New System.Drawing.Point(6, 89)
        Me.chkRuntime.Name = "chkRuntime"
        Me.chkRuntime.Size = New System.Drawing.Size(69, 17)
        Me.chkRuntime.TabIndex = 5
        Me.chkRuntime.Text = "Runtime"
        Me.chkRuntime.UseVisualStyleBackColor = True
        '
        'chkPlot
        '
        Me.chkPlot.AutoSize = True
        Me.chkPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPlot.Location = New System.Drawing.Point(173, 53)
        Me.chkPlot.Name = "chkPlot"
        Me.chkPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkPlot.TabIndex = 10
        Me.chkPlot.Text = "Plot"
        Me.chkPlot.UseVisualStyleBackColor = True
        '
        'chkGenre
        '
        Me.chkGenre.AutoSize = True
        Me.chkGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGenre.Location = New System.Drawing.Point(173, 72)
        Me.chkGenre.Name = "chkGenre"
        Me.chkGenre.Size = New System.Drawing.Size(57, 17)
        Me.chkGenre.TabIndex = 15
        Me.chkGenre.Text = "Genre"
        Me.chkGenre.UseVisualStyleBackColor = True
        '
        'chkTagline
        '
        Me.chkTagline.AutoSize = True
        Me.chkTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTagline.Location = New System.Drawing.Point(173, 36)
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
        Me.chkCast.Location = New System.Drawing.Point(173, 89)
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
        Me.chkVotes.Location = New System.Drawing.Point(6, 123)
        Me.chkVotes.Name = "chkVotes"
        Me.chkVotes.Size = New System.Drawing.Size(55, 17)
        Me.chkVotes.TabIndex = 7
        Me.chkVotes.Text = "Votes"
        Me.chkVotes.UseVisualStyleBackColor = True
        '
        'chkTrailer
        '
        Me.chkTrailer.AutoSize = True
        Me.chkTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTrailer.Location = New System.Drawing.Point(173, 106)
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
        Me.chkRating.Location = New System.Drawing.Point(6, 106)
        Me.chkRating.Name = "chkRating"
        Me.chkRating.Size = New System.Drawing.Size(60, 17)
        Me.chkRating.TabIndex = 6
        Me.chkRating.Text = "Rating"
        Me.chkRating.UseVisualStyleBackColor = True
        '
        'chkRelease
        '
        Me.chkRelease.AutoSize = True
        Me.chkRelease.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRelease.Location = New System.Drawing.Point(6, 72)
        Me.chkRelease.Name = "chkRelease"
        Me.chkRelease.Size = New System.Drawing.Size(92, 17)
        Me.chkRelease.TabIndex = 4
        Me.chkRelease.Text = "Release Date"
        Me.chkRelease.UseVisualStyleBackColor = True
        '
        'chkMPAA
        '
        Me.chkMPAA.AutoSize = True
        Me.chkMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMPAA.Location = New System.Drawing.Point(6, 53)
        Me.chkMPAA.Name = "chkMPAA"
        Me.chkMPAA.Size = New System.Drawing.Size(56, 17)
        Me.chkMPAA.TabIndex = 2
        Me.chkMPAA.Text = "MPAA"
        Me.chkMPAA.UseVisualStyleBackColor = True
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
        'chkCollection
        '
        Me.chkCollection.AutoSize = True
        Me.chkCollection.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCollection.Location = New System.Drawing.Point(315, 53)
        Me.chkCollection.Name = "chkCollection"
        Me.chkCollection.Size = New System.Drawing.Size(78, 17)
        Me.chkCollection.TabIndex = 76
        Me.chkCollection.Text = "Collection"
        Me.chkCollection.UseVisualStyleBackColor = True
        '
        'frmTMDBInfoSettingsHolder
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
        Me.Name = "frmTMDBInfoSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
        Me.gbTMDBGlobalOpts.ResumeLayout(False)
        Me.gbTMDBGlobalOpts.PerformLayout()
        CType(Me.pbTMDBApiKeyInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlSettings.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTMDBScraperOpts.ResumeLayout(False)
        Me.gbTMDBScraperOpts.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents gbTMDBGlobalOpts As System.Windows.Forms.GroupBox
    Friend WithEvents lblTMDBApiKey As System.Windows.Forms.Label
    Friend WithEvents txtTMDBApiKey As System.Windows.Forms.TextBox
    Friend WithEvents cbEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents lblScraperOrder As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents gbTMDBScraperOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkCountry As System.Windows.Forms.CheckBox
    Friend WithEvents chkStudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkGenre As System.Windows.Forms.CheckBox
    Friend WithEvents chkTagline As System.Windows.Forms.CheckBox
    Friend WithEvents chkCast As System.Windows.Forms.CheckBox
    Friend WithEvents chkVotes As System.Windows.Forms.CheckBox
    Friend WithEvents chkTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkRelease As System.Windows.Forms.CheckBox
    Friend WithEvents chkMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkYear As System.Windows.Forms.CheckBox
    Friend WithEvents chkTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkFallBackEng As System.Windows.Forms.CheckBox
    Friend WithEvents cbTMDBPrefLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents lblTMDBPrefLanguage As System.Windows.Forms.Label
    Friend WithEvents chkCrew As System.Windows.Forms.CheckBox
    Friend WithEvents pbTMDBApiKeyInfo As System.Windows.Forms.PictureBox
    Friend WithEvents chkTMDBCleanPlotOutline As System.Windows.Forms.CheckBox
    Friend WithEvents chkGetAdultItems As System.Windows.Forms.CheckBox
    Friend WithEvents chkCollection As System.Windows.Forms.CheckBox

End Class
