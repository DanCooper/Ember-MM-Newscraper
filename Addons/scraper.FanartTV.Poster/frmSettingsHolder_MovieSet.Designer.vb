<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettingsHolder_MovieSet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettingsHolder_MovieSet))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbScraperImagesOpts = New System.Windows.Forms.GroupBox()
        Me.tblScraperImagesOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkScrapePoster = New System.Windows.Forms.CheckBox()
        Me.chkScrapeLandscape = New System.Windows.Forms.CheckBox()
        Me.chkScrapeClearLogoOnlyHD = New System.Windows.Forms.CheckBox()
        Me.chkScrapeDiscArt = New System.Windows.Forms.CheckBox()
        Me.chkScrapeFanart = New System.Windows.Forms.CheckBox()
        Me.chkScrapeClearArtOnlyHD = New System.Windows.Forms.CheckBox()
        Me.chkScrapeBanner = New System.Windows.Forms.CheckBox()
        Me.chkScrapeClearArt = New System.Windows.Forms.CheckBox()
        Me.chkScrapeClearLogo = New System.Windows.Forms.CheckBox()
        Me.gbScraperOpts = New System.Windows.Forms.GroupBox()
        Me.tblScraperOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.lblAPIHint = New System.Windows.Forms.Label()
        Me.btnUnlockAPI = New System.Windows.Forms.Button()
        Me.lblEMMAPI = New System.Windows.Forms.Label()
        Me.lblAPIKey = New System.Windows.Forms.Label()
        Me.txtApiKey = New System.Windows.Forms.TextBox()
        Me.pbApiKeyInfo = New System.Windows.Forms.PictureBox()
        Me.pnlSettingsBottom = New System.Windows.Forms.Panel()
        Me.tblSettingsBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.pbIconBottom = New System.Windows.Forms.PictureBox()
        Me.lblInfoBottom = New System.Windows.Forms.Label()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.lblScraperOrder = New System.Windows.Forms.Label()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
        Me.gbScraperImagesOpts.SuspendLayout()
        Me.tblScraperImagesOpts.SuspendLayout()
        Me.gbScraperOpts.SuspendLayout()
        Me.tblScraperOpts.SuspendLayout()
        CType(Me.pbApiKeyInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSettingsBottom.SuspendLayout()
        Me.tblSettingsBottom.SuspendLayout()
        CType(Me.pbIconBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(462, 422)
        Me.pnlSettings.TabIndex = 0
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 29)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(462, 356)
        Me.pnlSettingsMain.TabIndex = 98
        '
        'tblSettingsMain
        '
        Me.tblSettingsMain.AutoScroll = True
        Me.tblSettingsMain.AutoSize = True
        Me.tblSettingsMain.ColumnCount = 2
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.Controls.Add(Me.gbScraperImagesOpts, 0, 0)
        Me.tblSettingsMain.Controls.Add(Me.gbScraperOpts, 0, 1)
        Me.tblSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsMain.Name = "tblSettingsMain"
        Me.tblSettingsMain.RowCount = 3
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.Size = New System.Drawing.Size(462, 356)
        Me.tblSettingsMain.TabIndex = 99
        '
        'gbScraperImagesOpts
        '
        Me.gbScraperImagesOpts.AutoSize = True
        Me.gbScraperImagesOpts.Controls.Add(Me.tblScraperImagesOpts)
        Me.gbScraperImagesOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperImagesOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbScraperImagesOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbScraperImagesOpts.Name = "gbScraperImagesOpts"
        Me.gbScraperImagesOpts.Size = New System.Drawing.Size(432, 136)
        Me.gbScraperImagesOpts.TabIndex = 96
        Me.gbScraperImagesOpts.TabStop = False
        Me.gbScraperImagesOpts.Text = "Images - Scraper specific"
        '
        'tblScraperImagesOpts
        '
        Me.tblScraperImagesOpts.AutoSize = True
        Me.tblScraperImagesOpts.ColumnCount = 3
        Me.tblScraperImagesOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesOpts.Controls.Add(Me.chkScrapePoster, 1, 3)
        Me.tblScraperImagesOpts.Controls.Add(Me.chkScrapeLandscape, 1, 2)
        Me.tblScraperImagesOpts.Controls.Add(Me.chkScrapeClearLogoOnlyHD, 0, 4)
        Me.tblScraperImagesOpts.Controls.Add(Me.chkScrapeDiscArt, 1, 0)
        Me.tblScraperImagesOpts.Controls.Add(Me.chkScrapeFanart, 1, 1)
        Me.tblScraperImagesOpts.Controls.Add(Me.chkScrapeClearArtOnlyHD, 0, 2)
        Me.tblScraperImagesOpts.Controls.Add(Me.chkScrapeBanner, 0, 0)
        Me.tblScraperImagesOpts.Controls.Add(Me.chkScrapeClearArt, 0, 1)
        Me.tblScraperImagesOpts.Controls.Add(Me.chkScrapeClearLogo, 0, 3)
        Me.tblScraperImagesOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperImagesOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperImagesOpts.Name = "tblScraperImagesOpts"
        Me.tblScraperImagesOpts.RowCount = 6
        Me.tblScraperImagesOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesOpts.Size = New System.Drawing.Size(426, 115)
        Me.tblScraperImagesOpts.TabIndex = 98
        '
        'chkScrapePoster
        '
        Me.chkScrapePoster.AutoSize = True
        Me.chkScrapePoster.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapePoster.Location = New System.Drawing.Point(98, 72)
        Me.chkScrapePoster.Name = "chkScrapePoster"
        Me.chkScrapePoster.Size = New System.Drawing.Size(58, 17)
        Me.chkScrapePoster.TabIndex = 0
        Me.chkScrapePoster.Text = "Poster"
        Me.chkScrapePoster.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapePoster.UseVisualStyleBackColor = True
        '
        'chkScrapeLandscape
        '
        Me.chkScrapeLandscape.AutoSize = True
        Me.chkScrapeLandscape.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeLandscape.Location = New System.Drawing.Point(98, 49)
        Me.chkScrapeLandscape.Name = "chkScrapeLandscape"
        Me.chkScrapeLandscape.Size = New System.Drawing.Size(80, 17)
        Me.chkScrapeLandscape.TabIndex = 7
        Me.chkScrapeLandscape.Text = "Landscape"
        Me.chkScrapeLandscape.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeLandscape.UseVisualStyleBackColor = True
        '
        'chkScrapeClearLogoOnlyHD
        '
        Me.chkScrapeClearLogoOnlyHD.AutoSize = True
        Me.chkScrapeClearLogoOnlyHD.Enabled = False
        Me.chkScrapeClearLogoOnlyHD.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkScrapeClearLogoOnlyHD.Location = New System.Drawing.Point(3, 95)
        Me.chkScrapeClearLogoOnlyHD.Name = "chkScrapeClearLogoOnlyHD"
        Me.chkScrapeClearLogoOnlyHD.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkScrapeClearLogoOnlyHD.Size = New System.Drawing.Size(89, 17)
        Me.chkScrapeClearLogoOnlyHD.TabIndex = 8
        Me.chkScrapeClearLogoOnlyHD.Text = "Only HD"
        Me.chkScrapeClearLogoOnlyHD.UseVisualStyleBackColor = True
        '
        'chkScrapeDiscArt
        '
        Me.chkScrapeDiscArt.AutoSize = True
        Me.chkScrapeDiscArt.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeDiscArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeDiscArt.Location = New System.Drawing.Point(98, 3)
        Me.chkScrapeDiscArt.Name = "chkScrapeDiscArt"
        Me.chkScrapeDiscArt.Size = New System.Drawing.Size(62, 17)
        Me.chkScrapeDiscArt.TabIndex = 6
        Me.chkScrapeDiscArt.Text = "DiscArt"
        Me.chkScrapeDiscArt.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeDiscArt.UseVisualStyleBackColor = True
        '
        'chkScrapeFanart
        '
        Me.chkScrapeFanart.AutoSize = True
        Me.chkScrapeFanart.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeFanart.Location = New System.Drawing.Point(98, 26)
        Me.chkScrapeFanart.Name = "chkScrapeFanart"
        Me.chkScrapeFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkScrapeFanart.TabIndex = 1
        Me.chkScrapeFanart.Text = "Fanart"
        Me.chkScrapeFanart.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeFanart.UseVisualStyleBackColor = True
        '
        'chkScrapeClearArtOnlyHD
        '
        Me.chkScrapeClearArtOnlyHD.AutoSize = True
        Me.chkScrapeClearArtOnlyHD.Enabled = False
        Me.chkScrapeClearArtOnlyHD.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkScrapeClearArtOnlyHD.Location = New System.Drawing.Point(3, 49)
        Me.chkScrapeClearArtOnlyHD.Name = "chkScrapeClearArtOnlyHD"
        Me.chkScrapeClearArtOnlyHD.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkScrapeClearArtOnlyHD.Size = New System.Drawing.Size(89, 17)
        Me.chkScrapeClearArtOnlyHD.TabIndex = 9
        Me.chkScrapeClearArtOnlyHD.Text = "Only HD"
        Me.chkScrapeClearArtOnlyHD.UseVisualStyleBackColor = True
        '
        'chkScrapeBanner
        '
        Me.chkScrapeBanner.AutoSize = True
        Me.chkScrapeBanner.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeBanner.Location = New System.Drawing.Point(3, 3)
        Me.chkScrapeBanner.Name = "chkScrapeBanner"
        Me.chkScrapeBanner.Size = New System.Drawing.Size(63, 17)
        Me.chkScrapeBanner.TabIndex = 2
        Me.chkScrapeBanner.Text = "Banner"
        Me.chkScrapeBanner.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeBanner.UseVisualStyleBackColor = True
        '
        'chkScrapeClearArt
        '
        Me.chkScrapeClearArt.AutoSize = True
        Me.chkScrapeClearArt.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeClearArt.Location = New System.Drawing.Point(3, 26)
        Me.chkScrapeClearArt.Name = "chkScrapeClearArt"
        Me.chkScrapeClearArt.Size = New System.Drawing.Size(67, 17)
        Me.chkScrapeClearArt.TabIndex = 4
        Me.chkScrapeClearArt.Text = "ClearArt"
        Me.chkScrapeClearArt.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeClearArt.UseVisualStyleBackColor = True
        '
        'chkScrapeClearLogo
        '
        Me.chkScrapeClearLogo.AutoSize = True
        Me.chkScrapeClearLogo.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeClearLogo.Location = New System.Drawing.Point(3, 72)
        Me.chkScrapeClearLogo.Name = "chkScrapeClearLogo"
        Me.chkScrapeClearLogo.Size = New System.Drawing.Size(78, 17)
        Me.chkScrapeClearLogo.TabIndex = 3
        Me.chkScrapeClearLogo.Text = "ClearLogo"
        Me.chkScrapeClearLogo.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeClearLogo.UseVisualStyleBackColor = True
        '
        'gbScraperOpts
        '
        Me.gbScraperOpts.AutoSize = True
        Me.gbScraperOpts.Controls.Add(Me.tblScraperOpts)
        Me.gbScraperOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbScraperOpts.Location = New System.Drawing.Point(3, 145)
        Me.gbScraperOpts.Name = "gbScraperOpts"
        Me.gbScraperOpts.Size = New System.Drawing.Size(432, 110)
        Me.gbScraperOpts.TabIndex = 95
        Me.gbScraperOpts.TabStop = False
        Me.gbScraperOpts.Text = "Scraper Options"
        '
        'tblScraperOpts
        '
        Me.tblScraperOpts.AutoSize = True
        Me.tblScraperOpts.ColumnCount = 5
        Me.tblScraperOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperOpts.Controls.Add(Me.lblAPIHint, 0, 2)
        Me.tblScraperOpts.Controls.Add(Me.btnUnlockAPI, 0, 1)
        Me.tblScraperOpts.Controls.Add(Me.lblEMMAPI, 2, 0)
        Me.tblScraperOpts.Controls.Add(Me.lblAPIKey, 0, 0)
        Me.tblScraperOpts.Controls.Add(Me.txtApiKey, 2, 1)
        Me.tblScraperOpts.Controls.Add(Me.pbApiKeyInfo, 3, 1)
        Me.tblScraperOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperOpts.Name = "tblScraperOpts"
        Me.tblScraperOpts.RowCount = 4
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperOpts.Size = New System.Drawing.Size(426, 89)
        Me.tblScraperOpts.TabIndex = 98
        '
        'lblAPIHint
        '
        Me.lblAPIHint.AutoSize = True
        Me.tblScraperOpts.SetColumnSpan(Me.lblAPIHint, 4)
        Me.lblAPIHint.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblAPIHint.Location = New System.Drawing.Point(3, 49)
        Me.lblAPIHint.MaximumSize = New System.Drawing.Size(430, 0)
        Me.lblAPIHint.Name = "lblAPIHint"
        Me.lblAPIHint.Size = New System.Drawing.Size(407, 26)
        Me.lblAPIHint.TabIndex = 23
        Me.lblAPIHint.Text = "Using a Personal API Key reduces the time you have to wait for new images to show" &
    " up from 7 days to 48 hours."
        '
        'btnUnlockAPI
        '
        Me.btnUnlockAPI.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblScraperOpts.SetColumnSpan(Me.btnUnlockAPI, 2)
        Me.btnUnlockAPI.Location = New System.Drawing.Point(3, 23)
        Me.btnUnlockAPI.Name = "btnUnlockAPI"
        Me.btnUnlockAPI.Size = New System.Drawing.Size(162, 23)
        Me.btnUnlockAPI.TabIndex = 99
        Me.btnUnlockAPI.Text = "Use my own API key"
        Me.btnUnlockAPI.UseVisualStyleBackColor = True
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
        Me.lblEMMAPI.TabIndex = 99
        Me.lblEMMAPI.Text = "Ember Media Manager Embedded API Key"
        '
        'lblAPIKey
        '
        Me.lblAPIKey.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblAPIKey.AutoSize = True
        Me.tblScraperOpts.SetColumnSpan(Me.lblAPIKey, 2)
        Me.lblAPIKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAPIKey.Location = New System.Drawing.Point(3, 3)
        Me.lblAPIKey.Name = "lblAPIKey"
        Me.lblAPIKey.Size = New System.Drawing.Size(141, 13)
        Me.lblAPIKey.TabIndex = 0
        Me.lblAPIKey.Text = "Fanart.tv Personal API Key:"
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
        'pbApiKeyInfo
        '
        Me.pbApiKeyInfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.pbApiKeyInfo.Image = CType(resources.GetObject("pbApiKeyInfo.Image"), System.Drawing.Image)
        Me.pbApiKeyInfo.Location = New System.Drawing.Point(407, 26)
        Me.pbApiKeyInfo.Name = "pbApiKeyInfo"
        Me.pbApiKeyInfo.Size = New System.Drawing.Size(16, 16)
        Me.pbApiKeyInfo.TabIndex = 6
        Me.pbApiKeyInfo.TabStop = False
        '
        'pnlSettingsBottom
        '
        Me.pnlSettingsBottom.AutoSize = True
        Me.pnlSettingsBottom.Controls.Add(Me.tblSettingsBottom)
        Me.pnlSettingsBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlSettingsBottom.Location = New System.Drawing.Point(0, 385)
        Me.pnlSettingsBottom.Name = "pnlSettingsBottom"
        Me.pnlSettingsBottom.Size = New System.Drawing.Size(462, 37)
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
        Me.tblSettingsBottom.Size = New System.Drawing.Size(462, 37)
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
        Me.pbIconBottom.TabIndex = 94
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
        Me.lblInfoBottom.TabIndex = 3
        Me.lblInfoBottom.Text = "These settings are specific to this module." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please refer to the global settings " &
    "for more options."
        Me.lblInfoBottom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(462, 29)
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
        Me.tblSettingsTop.Size = New System.Drawing.Size(462, 29)
        Me.tblSettingsTop.TabIndex = 98
        '
        'btnDown
        '
        Me.btnDown.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(436, 3)
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
        Me.lblScraperOrder.Location = New System.Drawing.Point(343, 8)
        Me.lblScraperOrder.Name = "lblScraperOrder"
        Me.lblScraperOrder.Size = New System.Drawing.Size(58, 12)
        Me.lblScraperOrder.TabIndex = 1
        Me.lblScraperOrder.Text = "Scraper order"
        '
        'btnUp
        '
        Me.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(407, 3)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(23, 23)
        Me.btnUp.TabIndex = 2
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'chkEnabled
        '
        Me.chkEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Location = New System.Drawing.Point(8, 6)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(68, 17)
        Me.chkEnabled.TabIndex = 0
        Me.chkEnabled.Text = "Enabled"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'frmSettingsHolder_MovieSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(462, 422)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder_MovieSet"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.pnlSettingsMain.ResumeLayout(False)
        Me.pnlSettingsMain.PerformLayout()
        Me.tblSettingsMain.ResumeLayout(False)
        Me.tblSettingsMain.PerformLayout()
        Me.gbScraperImagesOpts.ResumeLayout(False)
        Me.gbScraperImagesOpts.PerformLayout()
        Me.tblScraperImagesOpts.ResumeLayout(False)
        Me.tblScraperImagesOpts.PerformLayout()
        Me.gbScraperOpts.ResumeLayout(False)
        Me.gbScraperOpts.PerformLayout()
        Me.tblScraperOpts.ResumeLayout(False)
        Me.tblScraperOpts.PerformLayout()
        CType(Me.pbApiKeyInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSettingsBottom.ResumeLayout(False)
        Me.pnlSettingsBottom.PerformLayout()
        Me.tblSettingsBottom.ResumeLayout(False)
        Me.tblSettingsBottom.PerformLayout()
        CType(Me.pbIconBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSettingsTop.ResumeLayout(False)
        Me.pnlSettingsTop.PerformLayout()
        Me.tblSettingsTop.ResumeLayout(False)
        Me.tblSettingsTop.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents lblScraperOrder As System.Windows.Forms.Label
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents lblInfoBottom As System.Windows.Forms.Label
    Friend WithEvents pbIconBottom As System.Windows.Forms.PictureBox
    Friend WithEvents gbScraperOpts As System.Windows.Forms.GroupBox
    Friend WithEvents pbApiKeyInfo As System.Windows.Forms.PictureBox
    Friend WithEvents lblAPIKey As System.Windows.Forms.Label
    Friend WithEvents txtApiKey As System.Windows.Forms.TextBox
    Friend WithEvents gbScraperImagesOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkScrapePoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeDiscArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeBanner As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeClearLogoOnlyHD As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeClearArtOnlyHD As System.Windows.Forms.CheckBox
    Friend WithEvents lblAPIHint As System.Windows.Forms.Label
    Friend WithEvents pnlSettingsBottom As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblScraperImagesOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblScraperOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblEMMAPI As System.Windows.Forms.Label
    Friend WithEvents btnUnlockAPI As System.Windows.Forms.Button
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel

End Class