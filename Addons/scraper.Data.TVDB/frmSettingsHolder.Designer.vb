<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettingsHolder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettingsHolder))
        Me.gbScraperOpts = New System.Windows.Forms.GroupBox()
        Me.tblScraperOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.pbApiKeyInfo = New System.Windows.Forms.PictureBox()
        Me.lblApiKey = New System.Windows.Forms.Label()
        Me.btnUnlockAPI = New System.Windows.Forms.Button()
        Me.txtApiKey = New System.Windows.Forms.TextBox()
        Me.lblEMMAPI = New System.Windows.Forms.Label()
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
        Me.gbScraperFieldsShow = New System.Windows.Forms.GroupBox()
        Me.tblScraperFieldsShow = New System.Windows.Forms.TableLayoutPanel()
        Me.chkScraperShowPlot = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowMPAA = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowGenres = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowEpisodeGuide = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowActors = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowPremiered = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowRating = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowRuntime = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowStudios = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowStatus = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowTitle = New System.Windows.Forms.CheckBox()
        Me.gbScraperFieldsEpisode = New System.Windows.Forms.GroupBox()
        Me.tblScraperFieldsEpisode = New System.Windows.Forms.TableLayoutPanel()
        Me.chkScraperEpisodeTitle = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpisodeAired = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpisodeActors = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpisodeRating = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpisodeCredits = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpisodeDirectors = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpisodeGuestStars = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpisodePlot = New System.Windows.Forms.CheckBox()
        Me.pnlSettingsBottom = New System.Windows.Forms.Panel()
        Me.tblSettingsBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.pbIconBottom = New System.Windows.Forms.PictureBox()
        Me.lblInfoBottom = New System.Windows.Forms.Label()
        Me.chkFallBackEng = New System.Windows.Forms.CheckBox()
        Me.gbScraperOpts.SuspendLayout()
        Me.tblScraperOpts.SuspendLayout()
        CType(Me.pbApiKeyInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
        Me.gbScraperFieldsOpts.SuspendLayout()
        Me.tblScraperFieldsOpts.SuspendLayout()
        Me.gbScraperFieldsShow.SuspendLayout()
        Me.tblScraperFieldsShow.SuspendLayout()
        Me.gbScraperFieldsEpisode.SuspendLayout()
        Me.tblScraperFieldsEpisode.SuspendLayout()
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
        Me.gbScraperOpts.Location = New System.Drawing.Point(3, 195)
        Me.gbScraperOpts.Name = "gbScraperOpts"
        Me.gbScraperOpts.Size = New System.Drawing.Size(432, 93)
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
        Me.tblScraperOpts.Controls.Add(Me.pbApiKeyInfo, 2, 1)
        Me.tblScraperOpts.Controls.Add(Me.lblApiKey, 0, 0)
        Me.tblScraperOpts.Controls.Add(Me.btnUnlockAPI, 0, 1)
        Me.tblScraperOpts.Controls.Add(Me.txtApiKey, 1, 1)
        Me.tblScraperOpts.Controls.Add(Me.lblEMMAPI, 1, 0)
        Me.tblScraperOpts.Controls.Add(Me.chkFallBackEng, 0, 2)
        Me.tblScraperOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperOpts.Name = "tblScraperOpts"
        Me.tblScraperOpts.RowCount = 4
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.Size = New System.Drawing.Size(426, 72)
        Me.tblScraperOpts.TabIndex = 98
        '
        'pbApiKeyInfo
        '
        Me.pbApiKeyInfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.pbApiKeyInfo.Image = CType(resources.GetObject("pbApiKeyInfo.Image"), System.Drawing.Image)
        Me.pbApiKeyInfo.Location = New System.Drawing.Point(407, 26)
        Me.pbApiKeyInfo.Name = "pbApiKeyInfo"
        Me.pbApiKeyInfo.Size = New System.Drawing.Size(16, 16)
        Me.pbApiKeyInfo.TabIndex = 5
        Me.pbApiKeyInfo.TabStop = False
        '
        'lblApiKey
        '
        Me.lblApiKey.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblApiKey.AutoSize = True
        Me.lblApiKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApiKey.Location = New System.Drawing.Point(3, 3)
        Me.lblApiKey.Name = "lblApiKey"
        Me.lblApiKey.Size = New System.Drawing.Size(77, 13)
        Me.lblApiKey.TabIndex = 0
        Me.lblApiKey.Text = "TVDB Api Key:"
        '
        'btnUnlockAPI
        '
        Me.btnUnlockAPI.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnUnlockAPI.Location = New System.Drawing.Point(3, 23)
        Me.btnUnlockAPI.Name = "btnUnlockAPI"
        Me.btnUnlockAPI.Size = New System.Drawing.Size(162, 23)
        Me.btnUnlockAPI.TabIndex = 11
        Me.btnUnlockAPI.Text = "Use my own API key"
        Me.btnUnlockAPI.UseVisualStyleBackColor = True
        '
        'txtApiKey
        '
        Me.txtApiKey.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtApiKey.Enabled = False
        Me.txtApiKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApiKey.Location = New System.Drawing.Point(171, 23)
        Me.txtApiKey.Name = "txtApiKey"
        Me.txtApiKey.Size = New System.Drawing.Size(230, 22)
        Me.txtApiKey.TabIndex = 1
        '
        'lblEMMAPI
        '
        Me.lblEMMAPI.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblEMMAPI.AutoSize = True
        Me.tblScraperOpts.SetColumnSpan(Me.lblEMMAPI, 2)
        Me.lblEMMAPI.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblEMMAPI.Location = New System.Drawing.Point(171, 3)
        Me.lblEMMAPI.Name = "lblEMMAPI"
        Me.lblEMMAPI.Size = New System.Drawing.Size(220, 13)
        Me.lblEMMAPI.TabIndex = 12
        Me.lblEMMAPI.Text = "Ember Media Manager Embedded API Key"
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
        Me.pnlSettingsTop.Size = New System.Drawing.Size(523, 29)
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
        Me.tblSettingsTop.Size = New System.Drawing.Size(523, 29)
        Me.tblSettingsTop.TabIndex = 98
        '
        'btnDown
        '
        Me.btnDown.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(497, 3)
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
        Me.lblScraperOrder.Location = New System.Drawing.Point(404, 8)
        Me.lblScraperOrder.Name = "lblScraperOrder"
        Me.lblScraperOrder.Size = New System.Drawing.Size(58, 12)
        Me.lblScraperOrder.TabIndex = 1
        Me.lblScraperOrder.Text = "Scraper order"
        '
        'btnUp
        '
        Me.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(468, 3)
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
        Me.pnlSettings.Size = New System.Drawing.Size(523, 440)
        Me.pnlSettings.TabIndex = 0
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 29)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(523, 374)
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
        Me.tblSettingsMain.Size = New System.Drawing.Size(523, 374)
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
        Me.gbScraperFieldsOpts.Size = New System.Drawing.Size(432, 186)
        Me.gbScraperFieldsOpts.TabIndex = 3
        Me.gbScraperFieldsOpts.TabStop = False
        Me.gbScraperFieldsOpts.Text = "Scraper Fields - Scraper specific"
        '
        'tblScraperFieldsOpts
        '
        Me.tblScraperFieldsOpts.AutoSize = True
        Me.tblScraperFieldsOpts.ColumnCount = 3
        Me.tblScraperFieldsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsOpts.Controls.Add(Me.gbScraperFieldsShow, 0, 0)
        Me.tblScraperFieldsOpts.Controls.Add(Me.gbScraperFieldsEpisode, 1, 0)
        Me.tblScraperFieldsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperFieldsOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperFieldsOpts.Name = "tblScraperFieldsOpts"
        Me.tblScraperFieldsOpts.RowCount = 2
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.Size = New System.Drawing.Size(426, 165)
        Me.tblScraperFieldsOpts.TabIndex = 99
        '
        'gbScraperFieldsShow
        '
        Me.gbScraperFieldsShow.AutoSize = True
        Me.gbScraperFieldsShow.Controls.Add(Me.tblScraperFieldsShow)
        Me.gbScraperFieldsShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperFieldsShow.Location = New System.Drawing.Point(3, 3)
        Me.gbScraperFieldsShow.Name = "gbScraperFieldsShow"
        Me.gbScraperFieldsShow.Size = New System.Drawing.Size(211, 159)
        Me.gbScraperFieldsShow.TabIndex = 0
        Me.gbScraperFieldsShow.TabStop = False
        Me.gbScraperFieldsShow.Text = "Show"
        '
        'tblScraperFieldsShow
        '
        Me.tblScraperFieldsShow.AutoSize = True
        Me.tblScraperFieldsShow.ColumnCount = 3
        Me.tblScraperFieldsShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowPlot, 0, 4)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowMPAA, 0, 3)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowGenres, 0, 2)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowEpisodeGuide, 0, 1)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowActors, 0, 0)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowPremiered, 0, 5)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowRating, 1, 0)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowRuntime, 1, 1)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowStudios, 1, 3)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowStatus, 1, 2)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowTitle, 1, 4)
        Me.tblScraperFieldsShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperFieldsShow.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperFieldsShow.Name = "tblScraperFieldsShow"
        Me.tblScraperFieldsShow.RowCount = 7
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.Size = New System.Drawing.Size(205, 138)
        Me.tblScraperFieldsShow.TabIndex = 0
        '
        'chkScraperShowPlot
        '
        Me.chkScraperShowPlot.AutoSize = True
        Me.chkScraperShowPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowPlot.Location = New System.Drawing.Point(3, 95)
        Me.chkScraperShowPlot.Name = "chkScraperShowPlot"
        Me.chkScraperShowPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkScraperShowPlot.TabIndex = 4
        Me.chkScraperShowPlot.Text = "Plot"
        Me.chkScraperShowPlot.UseVisualStyleBackColor = True
        '
        'chkScraperShowMPAA
        '
        Me.chkScraperShowMPAA.AutoSize = True
        Me.chkScraperShowMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowMPAA.Location = New System.Drawing.Point(3, 72)
        Me.chkScraperShowMPAA.Name = "chkScraperShowMPAA"
        Me.chkScraperShowMPAA.Size = New System.Drawing.Size(55, 17)
        Me.chkScraperShowMPAA.TabIndex = 3
        Me.chkScraperShowMPAA.Text = "MPAA"
        Me.chkScraperShowMPAA.UseVisualStyleBackColor = True
        '
        'chkScraperShowGenres
        '
        Me.chkScraperShowGenres.AutoSize = True
        Me.chkScraperShowGenres.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowGenres.Location = New System.Drawing.Point(3, 49)
        Me.chkScraperShowGenres.Name = "chkScraperShowGenres"
        Me.chkScraperShowGenres.Size = New System.Drawing.Size(62, 17)
        Me.chkScraperShowGenres.TabIndex = 2
        Me.chkScraperShowGenres.Text = "Genres"
        Me.chkScraperShowGenres.UseVisualStyleBackColor = True
        '
        'chkScraperShowEpisodeGuide
        '
        Me.chkScraperShowEpisodeGuide.AutoSize = True
        Me.chkScraperShowEpisodeGuide.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowEpisodeGuide.Location = New System.Drawing.Point(3, 26)
        Me.chkScraperShowEpisodeGuide.Name = "chkScraperShowEpisodeGuide"
        Me.chkScraperShowEpisodeGuide.Size = New System.Drawing.Size(124, 17)
        Me.chkScraperShowEpisodeGuide.TabIndex = 0
        Me.chkScraperShowEpisodeGuide.Text = "Episode Guide URL"
        Me.chkScraperShowEpisodeGuide.UseVisualStyleBackColor = True
        '
        'chkScraperShowActors
        '
        Me.chkScraperShowActors.AutoSize = True
        Me.chkScraperShowActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowActors.Location = New System.Drawing.Point(3, 3)
        Me.chkScraperShowActors.Name = "chkScraperShowActors"
        Me.chkScraperShowActors.Size = New System.Drawing.Size(58, 17)
        Me.chkScraperShowActors.TabIndex = 8
        Me.chkScraperShowActors.Text = "Actors"
        Me.chkScraperShowActors.UseVisualStyleBackColor = True
        '
        'chkScraperShowPremiered
        '
        Me.chkScraperShowPremiered.AutoSize = True
        Me.chkScraperShowPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowPremiered.Location = New System.Drawing.Point(3, 118)
        Me.chkScraperShowPremiered.Name = "chkScraperShowPremiered"
        Me.chkScraperShowPremiered.Size = New System.Drawing.Size(77, 17)
        Me.chkScraperShowPremiered.TabIndex = 5
        Me.chkScraperShowPremiered.Text = "Premiered"
        Me.chkScraperShowPremiered.UseVisualStyleBackColor = True
        '
        'chkScraperShowRating
        '
        Me.chkScraperShowRating.AutoSize = True
        Me.chkScraperShowRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowRating.Location = New System.Drawing.Point(133, 3)
        Me.chkScraperShowRating.Name = "chkScraperShowRating"
        Me.chkScraperShowRating.Size = New System.Drawing.Size(60, 17)
        Me.chkScraperShowRating.TabIndex = 6
        Me.chkScraperShowRating.Text = "Rating"
        Me.chkScraperShowRating.UseVisualStyleBackColor = True
        '
        'chkScraperShowRuntime
        '
        Me.chkScraperShowRuntime.AutoSize = True
        Me.chkScraperShowRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowRuntime.Location = New System.Drawing.Point(133, 26)
        Me.chkScraperShowRuntime.Name = "chkScraperShowRuntime"
        Me.chkScraperShowRuntime.Size = New System.Drawing.Size(69, 17)
        Me.chkScraperShowRuntime.TabIndex = 10
        Me.chkScraperShowRuntime.Text = "Runtime"
        Me.chkScraperShowRuntime.UseVisualStyleBackColor = True
        '
        'chkScraperShowStudios
        '
        Me.chkScraperShowStudios.AutoSize = True
        Me.chkScraperShowStudios.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowStudios.Location = New System.Drawing.Point(133, 72)
        Me.chkScraperShowStudios.Name = "chkScraperShowStudios"
        Me.chkScraperShowStudios.Size = New System.Drawing.Size(65, 17)
        Me.chkScraperShowStudios.TabIndex = 7
        Me.chkScraperShowStudios.Text = "Studios"
        Me.chkScraperShowStudios.UseVisualStyleBackColor = True
        '
        'chkScraperShowStatus
        '
        Me.chkScraperShowStatus.AutoSize = True
        Me.chkScraperShowStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowStatus.Location = New System.Drawing.Point(133, 49)
        Me.chkScraperShowStatus.Name = "chkScraperShowStatus"
        Me.chkScraperShowStatus.Size = New System.Drawing.Size(58, 17)
        Me.chkScraperShowStatus.TabIndex = 9
        Me.chkScraperShowStatus.Text = "Status"
        Me.chkScraperShowStatus.UseVisualStyleBackColor = True
        '
        'chkScraperShowTitle
        '
        Me.chkScraperShowTitle.AutoSize = True
        Me.chkScraperShowTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowTitle.Location = New System.Drawing.Point(133, 95)
        Me.chkScraperShowTitle.Name = "chkScraperShowTitle"
        Me.chkScraperShowTitle.Size = New System.Drawing.Size(47, 17)
        Me.chkScraperShowTitle.TabIndex = 0
        Me.chkScraperShowTitle.Text = "Title"
        Me.chkScraperShowTitle.UseVisualStyleBackColor = True
        '
        'gbScraperFieldsEpisode
        '
        Me.gbScraperFieldsEpisode.AutoSize = True
        Me.gbScraperFieldsEpisode.Controls.Add(Me.tblScraperFieldsEpisode)
        Me.gbScraperFieldsEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperFieldsEpisode.Location = New System.Drawing.Point(220, 3)
        Me.gbScraperFieldsEpisode.Name = "gbScraperFieldsEpisode"
        Me.gbScraperFieldsEpisode.Size = New System.Drawing.Size(174, 159)
        Me.gbScraperFieldsEpisode.TabIndex = 1
        Me.gbScraperFieldsEpisode.TabStop = False
        Me.gbScraperFieldsEpisode.Text = "Episode"
        '
        'tblScraperFieldsEpisode
        '
        Me.tblScraperFieldsEpisode.AutoSize = True
        Me.tblScraperFieldsEpisode.ColumnCount = 3
        Me.tblScraperFieldsEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodeTitle, 1, 3)
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodeAired, 0, 1)
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodeActors, 0, 0)
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodeRating, 1, 2)
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodeCredits, 0, 2)
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodeDirectors, 0, 3)
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodeGuestStars, 1, 0)
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodePlot, 1, 1)
        Me.tblScraperFieldsEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperFieldsEpisode.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperFieldsEpisode.Name = "tblScraperFieldsEpisode"
        Me.tblScraperFieldsEpisode.RowCount = 5
        Me.tblScraperFieldsEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperFieldsEpisode.Size = New System.Drawing.Size(168, 138)
        Me.tblScraperFieldsEpisode.TabIndex = 0
        '
        'chkScraperEpisodeTitle
        '
        Me.chkScraperEpisodeTitle.AutoSize = True
        Me.chkScraperEpisodeTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpisodeTitle.Location = New System.Drawing.Point(81, 72)
        Me.chkScraperEpisodeTitle.Name = "chkScraperEpisodeTitle"
        Me.chkScraperEpisodeTitle.Size = New System.Drawing.Size(47, 17)
        Me.chkScraperEpisodeTitle.TabIndex = 0
        Me.chkScraperEpisodeTitle.Text = "Title"
        Me.chkScraperEpisodeTitle.UseVisualStyleBackColor = True
        '
        'chkScraperEpisodeAired
        '
        Me.chkScraperEpisodeAired.AutoSize = True
        Me.chkScraperEpisodeAired.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpisodeAired.Location = New System.Drawing.Point(3, 26)
        Me.chkScraperEpisodeAired.Name = "chkScraperEpisodeAired"
        Me.chkScraperEpisodeAired.Size = New System.Drawing.Size(53, 17)
        Me.chkScraperEpisodeAired.TabIndex = 4
        Me.chkScraperEpisodeAired.Text = "Aired"
        Me.chkScraperEpisodeAired.UseVisualStyleBackColor = True
        '
        'chkScraperEpisodeActors
        '
        Me.chkScraperEpisodeActors.AutoSize = True
        Me.chkScraperEpisodeActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpisodeActors.Location = New System.Drawing.Point(3, 3)
        Me.chkScraperEpisodeActors.Name = "chkScraperEpisodeActors"
        Me.chkScraperEpisodeActors.Size = New System.Drawing.Size(58, 17)
        Me.chkScraperEpisodeActors.TabIndex = 0
        Me.chkScraperEpisodeActors.Text = "Actors"
        Me.chkScraperEpisodeActors.UseVisualStyleBackColor = True
        '
        'chkScraperEpisodeRating
        '
        Me.chkScraperEpisodeRating.AutoSize = True
        Me.chkScraperEpisodeRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpisodeRating.Location = New System.Drawing.Point(81, 49)
        Me.chkScraperEpisodeRating.Name = "chkScraperEpisodeRating"
        Me.chkScraperEpisodeRating.Size = New System.Drawing.Size(60, 17)
        Me.chkScraperEpisodeRating.TabIndex = 5
        Me.chkScraperEpisodeRating.Text = "Rating"
        Me.chkScraperEpisodeRating.UseVisualStyleBackColor = True
        '
        'chkScraperEpisodeCredits
        '
        Me.chkScraperEpisodeCredits.AutoSize = True
        Me.chkScraperEpisodeCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpisodeCredits.Location = New System.Drawing.Point(3, 49)
        Me.chkScraperEpisodeCredits.Name = "chkScraperEpisodeCredits"
        Me.chkScraperEpisodeCredits.Size = New System.Drawing.Size(62, 17)
        Me.chkScraperEpisodeCredits.TabIndex = 8
        Me.chkScraperEpisodeCredits.Text = "Credits"
        Me.chkScraperEpisodeCredits.UseVisualStyleBackColor = True
        '
        'chkScraperEpisodeDirectors
        '
        Me.chkScraperEpisodeDirectors.AutoSize = True
        Me.chkScraperEpisodeDirectors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpisodeDirectors.Location = New System.Drawing.Point(3, 72)
        Me.chkScraperEpisodeDirectors.Name = "chkScraperEpisodeDirectors"
        Me.chkScraperEpisodeDirectors.Size = New System.Drawing.Size(72, 17)
        Me.chkScraperEpisodeDirectors.TabIndex = 7
        Me.chkScraperEpisodeDirectors.Text = "Directors"
        Me.chkScraperEpisodeDirectors.UseVisualStyleBackColor = True
        '
        'chkScraperEpisodeGuestStars
        '
        Me.chkScraperEpisodeGuestStars.AutoSize = True
        Me.chkScraperEpisodeGuestStars.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkScraperEpisodeGuestStars.Location = New System.Drawing.Point(81, 3)
        Me.chkScraperEpisodeGuestStars.Name = "chkScraperEpisodeGuestStars"
        Me.chkScraperEpisodeGuestStars.Size = New System.Drawing.Size(84, 17)
        Me.chkScraperEpisodeGuestStars.TabIndex = 10
        Me.chkScraperEpisodeGuestStars.Text = "Guest Stars"
        Me.chkScraperEpisodeGuestStars.UseVisualStyleBackColor = True
        '
        'chkScraperEpisodePlot
        '
        Me.chkScraperEpisodePlot.AutoSize = True
        Me.chkScraperEpisodePlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpisodePlot.Location = New System.Drawing.Point(81, 26)
        Me.chkScraperEpisodePlot.Name = "chkScraperEpisodePlot"
        Me.chkScraperEpisodePlot.Size = New System.Drawing.Size(46, 17)
        Me.chkScraperEpisodePlot.TabIndex = 6
        Me.chkScraperEpisodePlot.Text = "Plot"
        Me.chkScraperEpisodePlot.UseVisualStyleBackColor = True
        '
        'pnlSettingsBottom
        '
        Me.pnlSettingsBottom.AutoSize = True
        Me.pnlSettingsBottom.Controls.Add(Me.tblSettingsBottom)
        Me.pnlSettingsBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlSettingsBottom.Location = New System.Drawing.Point(0, 403)
        Me.pnlSettingsBottom.Name = "pnlSettingsBottom"
        Me.pnlSettingsBottom.Size = New System.Drawing.Size(523, 37)
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
        Me.tblSettingsBottom.Size = New System.Drawing.Size(523, 37)
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
        'chkFallBackEng
        '
        Me.chkFallBackEng.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFallBackEng.AutoSize = True
        Me.tblScraperOpts.SetColumnSpan(Me.chkFallBackEng, 2)
        Me.chkFallBackEng.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFallBackEng.Location = New System.Drawing.Point(3, 52)
        Me.chkFallBackEng.Name = "chkFallBackEng"
        Me.chkFallBackEng.Size = New System.Drawing.Size(123, 17)
        Me.chkFallBackEng.TabIndex = 13
        Me.chkFallBackEng.Text = "Fallback to english"
        Me.chkFallBackEng.UseVisualStyleBackColor = True
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(523, 440)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
        Me.gbScraperOpts.ResumeLayout(False)
        Me.gbScraperOpts.PerformLayout()
        Me.tblScraperOpts.ResumeLayout(False)
        Me.tblScraperOpts.PerformLayout()
        CType(Me.pbApiKeyInfo, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.gbScraperFieldsShow.ResumeLayout(False)
        Me.gbScraperFieldsShow.PerformLayout()
        Me.tblScraperFieldsShow.ResumeLayout(False)
        Me.tblScraperFieldsShow.PerformLayout()
        Me.gbScraperFieldsEpisode.ResumeLayout(False)
        Me.gbScraperFieldsEpisode.PerformLayout()
        Me.tblScraperFieldsEpisode.ResumeLayout(False)
        Me.tblScraperFieldsEpisode.PerformLayout()
        Me.pnlSettingsBottom.ResumeLayout(False)
        Me.pnlSettingsBottom.PerformLayout()
        Me.tblSettingsBottom.ResumeLayout(False)
        Me.tblSettingsBottom.PerformLayout()
        CType(Me.pbIconBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gbScraperOpts As System.Windows.Forms.GroupBox
    Friend WithEvents lblApiKey As System.Windows.Forms.Label
    Friend WithEvents txtApiKey As System.Windows.Forms.TextBox
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents lblScraperOrder As System.Windows.Forms.Label
    Friend WithEvents lblInfoBottom As System.Windows.Forms.Label
    Friend WithEvents pbIconBottom As System.Windows.Forms.PictureBox
    Friend WithEvents gbScraperFieldsOpts As System.Windows.Forms.GroupBox
    Friend WithEvents pbApiKeyInfo As System.Windows.Forms.PictureBox
    Friend WithEvents lblEMMAPI As System.Windows.Forms.Label
    Friend WithEvents btnUnlockAPI As System.Windows.Forms.Button
    Friend WithEvents tblSettingsBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsBottom As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblScraperOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblScraperFieldsOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbScraperFieldsShow As System.Windows.Forms.GroupBox
    Friend WithEvents tblScraperFieldsShow As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkScraperShowTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowGenres As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowPremiered As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowStudios As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowActors As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowStatus As System.Windows.Forms.CheckBox
    Friend WithEvents gbScraperFieldsEpisode As System.Windows.Forms.GroupBox
    Friend WithEvents tblScraperFieldsEpisode As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkScraperEpisodeTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpisodePlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpisodeDirectors As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpisodeCredits As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpisodeActors As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpisodeAired As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpisodeRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperEpisodeGuestStars As System.Windows.Forms.CheckBox
    Friend WithEvents chkScraperShowEpisodeGuide As System.Windows.Forms.CheckBox
    Friend WithEvents chkFallBackEng As CheckBox
End Class
