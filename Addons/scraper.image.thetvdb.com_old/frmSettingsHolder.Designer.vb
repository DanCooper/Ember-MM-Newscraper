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
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbScraperImagesOpts = New System.Windows.Forms.GroupBox()
        Me.tblScraperImagesOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.gbScraperImagesSeason = New System.Windows.Forms.GroupBox()
        Me.tblScraperImagesSeason = New System.Windows.Forms.TableLayoutPanel()
        Me.chkScrapeSeasonBanner = New System.Windows.Forms.CheckBox()
        Me.chkScrapeSeasonPoster = New System.Windows.Forms.CheckBox()
        Me.gbScraperImagesTVShow = New System.Windows.Forms.GroupBox()
        Me.tblScraperImagesShow = New System.Windows.Forms.TableLayoutPanel()
        Me.chkScrapeShowBanner = New System.Windows.Forms.CheckBox()
        Me.chkScrapeShowFanart = New System.Windows.Forms.CheckBox()
        Me.chkScrapeShowPoster = New System.Windows.Forms.CheckBox()
        Me.gbScraperImagesEpisode = New System.Windows.Forms.GroupBox()
        Me.tblScraperImagesEpisode = New System.Windows.Forms.TableLayoutPanel()
        Me.chkScrapeEpisodePoster = New System.Windows.Forms.CheckBox()
        Me.gbScraperOpts = New System.Windows.Forms.GroupBox()
        Me.tblScraperOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.lblAPIKey = New System.Windows.Forms.Label()
        Me.pbApiKeyInfo = New System.Windows.Forms.PictureBox()
        Me.txtApiKey = New System.Windows.Forms.TextBox()
        Me.btnUnlockAPI = New System.Windows.Forms.Button()
        Me.lblEMMAPI = New System.Windows.Forms.Label()
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
        Me.gbScraperImagesSeason.SuspendLayout()
        Me.tblScraperImagesSeason.SuspendLayout()
        Me.gbScraperImagesTVShow.SuspendLayout()
        Me.tblScraperImagesShow.SuspendLayout()
        Me.gbScraperImagesEpisode.SuspendLayout()
        Me.tblScraperImagesEpisode.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(453, 289)
        Me.pnlSettings.TabIndex = 0
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 29)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(453, 223)
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
        Me.tblSettingsMain.Size = New System.Drawing.Size(453, 223)
        Me.tblSettingsMain.TabIndex = 0
        '
        'gbScraperImagesOpts
        '
        Me.gbScraperImagesOpts.AutoSize = True
        Me.gbScraperImagesOpts.Controls.Add(Me.tblScraperImagesOpts)
        Me.gbScraperImagesOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperImagesOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbScraperImagesOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbScraperImagesOpts.Name = "gbScraperImagesOpts"
        Me.gbScraperImagesOpts.Size = New System.Drawing.Size(432, 117)
        Me.gbScraperImagesOpts.TabIndex = 96
        Me.gbScraperImagesOpts.TabStop = False
        Me.gbScraperImagesOpts.Text = "Images - Scraper specific"
        '
        'tblScraperImagesOpts
        '
        Me.tblScraperImagesOpts.AutoSize = True
        Me.tblScraperImagesOpts.ColumnCount = 4
        Me.tblScraperImagesOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesOpts.Controls.Add(Me.gbScraperImagesSeason, 1, 0)
        Me.tblScraperImagesOpts.Controls.Add(Me.gbScraperImagesTVShow, 0, 0)
        Me.tblScraperImagesOpts.Controls.Add(Me.gbScraperImagesEpisode, 2, 0)
        Me.tblScraperImagesOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperImagesOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperImagesOpts.Name = "tblScraperImagesOpts"
        Me.tblScraperImagesOpts.RowCount = 2
        Me.tblScraperImagesOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesOpts.Size = New System.Drawing.Size(426, 96)
        Me.tblScraperImagesOpts.TabIndex = 11
        '
        'gbScraperImagesSeason
        '
        Me.gbScraperImagesSeason.AutoSize = True
        Me.gbScraperImagesSeason.Controls.Add(Me.tblScraperImagesSeason)
        Me.gbScraperImagesSeason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperImagesSeason.Location = New System.Drawing.Point(84, 3)
        Me.gbScraperImagesSeason.Name = "gbScraperImagesSeason"
        Me.gbScraperImagesSeason.Size = New System.Drawing.Size(75, 90)
        Me.gbScraperImagesSeason.TabIndex = 11
        Me.gbScraperImagesSeason.TabStop = False
        Me.gbScraperImagesSeason.Text = "Season"
        '
        'tblScraperImagesSeason
        '
        Me.tblScraperImagesSeason.AutoSize = True
        Me.tblScraperImagesSeason.ColumnCount = 3
        Me.tblScraperImagesSeason.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesSeason.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesSeason.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesSeason.Controls.Add(Me.chkScrapeSeasonBanner, 0, 0)
        Me.tblScraperImagesSeason.Controls.Add(Me.chkScrapeSeasonPoster, 0, 1)
        Me.tblScraperImagesSeason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperImagesSeason.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperImagesSeason.Name = "tblScraperImagesSeason"
        Me.tblScraperImagesSeason.RowCount = 3
        Me.tblScraperImagesSeason.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesSeason.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesSeason.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesSeason.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperImagesSeason.Size = New System.Drawing.Size(69, 69)
        Me.tblScraperImagesSeason.TabIndex = 98
        '
        'chkScrapeSeasonBanner
        '
        Me.chkScrapeSeasonBanner.AutoSize = True
        Me.chkScrapeSeasonBanner.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeSeasonBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeSeasonBanner.Location = New System.Drawing.Point(3, 3)
        Me.chkScrapeSeasonBanner.Name = "chkScrapeSeasonBanner"
        Me.chkScrapeSeasonBanner.Size = New System.Drawing.Size(63, 17)
        Me.chkScrapeSeasonBanner.TabIndex = 2
        Me.chkScrapeSeasonBanner.Text = "Banner"
        Me.chkScrapeSeasonBanner.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeSeasonBanner.UseVisualStyleBackColor = True
        '
        'chkScrapeSeasonPoster
        '
        Me.chkScrapeSeasonPoster.AutoSize = True
        Me.chkScrapeSeasonPoster.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeSeasonPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeSeasonPoster.Location = New System.Drawing.Point(3, 26)
        Me.chkScrapeSeasonPoster.Name = "chkScrapeSeasonPoster"
        Me.chkScrapeSeasonPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkScrapeSeasonPoster.TabIndex = 0
        Me.chkScrapeSeasonPoster.Text = "Poster"
        Me.chkScrapeSeasonPoster.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeSeasonPoster.UseVisualStyleBackColor = True
        '
        'gbScraperImagesTVShow
        '
        Me.gbScraperImagesTVShow.AutoSize = True
        Me.gbScraperImagesTVShow.Controls.Add(Me.tblScraperImagesShow)
        Me.gbScraperImagesTVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperImagesTVShow.Location = New System.Drawing.Point(3, 3)
        Me.gbScraperImagesTVShow.Name = "gbScraperImagesTVShow"
        Me.gbScraperImagesTVShow.Size = New System.Drawing.Size(75, 90)
        Me.gbScraperImagesTVShow.TabIndex = 10
        Me.gbScraperImagesTVShow.TabStop = False
        Me.gbScraperImagesTVShow.Text = "TV Show"
        '
        'tblScraperImagesShow
        '
        Me.tblScraperImagesShow.AutoSize = True
        Me.tblScraperImagesShow.ColumnCount = 3
        Me.tblScraperImagesShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesShow.Controls.Add(Me.chkScrapeShowBanner, 0, 0)
        Me.tblScraperImagesShow.Controls.Add(Me.chkScrapeShowFanart, 0, 1)
        Me.tblScraperImagesShow.Controls.Add(Me.chkScrapeShowPoster, 0, 2)
        Me.tblScraperImagesShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperImagesShow.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperImagesShow.Name = "tblScraperImagesShow"
        Me.tblScraperImagesShow.RowCount = 4
        Me.tblScraperImagesShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesShow.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperImagesShow.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperImagesShow.Size = New System.Drawing.Size(69, 69)
        Me.tblScraperImagesShow.TabIndex = 98
        '
        'chkScrapeShowBanner
        '
        Me.chkScrapeShowBanner.AutoSize = True
        Me.chkScrapeShowBanner.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeShowBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeShowBanner.Location = New System.Drawing.Point(3, 3)
        Me.chkScrapeShowBanner.Name = "chkScrapeShowBanner"
        Me.chkScrapeShowBanner.Size = New System.Drawing.Size(63, 17)
        Me.chkScrapeShowBanner.TabIndex = 2
        Me.chkScrapeShowBanner.Text = "Banner"
        Me.chkScrapeShowBanner.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeShowBanner.UseVisualStyleBackColor = True
        '
        'chkScrapeShowFanart
        '
        Me.chkScrapeShowFanart.AutoSize = True
        Me.chkScrapeShowFanart.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeShowFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeShowFanart.Location = New System.Drawing.Point(3, 26)
        Me.chkScrapeShowFanart.Name = "chkScrapeShowFanart"
        Me.chkScrapeShowFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkScrapeShowFanart.TabIndex = 1
        Me.chkScrapeShowFanart.Text = "Fanart"
        Me.chkScrapeShowFanart.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeShowFanart.UseVisualStyleBackColor = True
        '
        'chkScrapeShowPoster
        '
        Me.chkScrapeShowPoster.AutoSize = True
        Me.chkScrapeShowPoster.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeShowPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeShowPoster.Location = New System.Drawing.Point(3, 49)
        Me.chkScrapeShowPoster.Name = "chkScrapeShowPoster"
        Me.chkScrapeShowPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkScrapeShowPoster.TabIndex = 0
        Me.chkScrapeShowPoster.Text = "Poster"
        Me.chkScrapeShowPoster.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeShowPoster.UseVisualStyleBackColor = True
        '
        'gbScraperImagesEpisode
        '
        Me.gbScraperImagesEpisode.AutoSize = True
        Me.gbScraperImagesEpisode.Controls.Add(Me.tblScraperImagesEpisode)
        Me.gbScraperImagesEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperImagesEpisode.Location = New System.Drawing.Point(165, 3)
        Me.gbScraperImagesEpisode.Name = "gbScraperImagesEpisode"
        Me.gbScraperImagesEpisode.Size = New System.Drawing.Size(70, 90)
        Me.gbScraperImagesEpisode.TabIndex = 12
        Me.gbScraperImagesEpisode.TabStop = False
        Me.gbScraperImagesEpisode.Text = "Episode"
        '
        'tblScraperImagesEpisode
        '
        Me.tblScraperImagesEpisode.AutoSize = True
        Me.tblScraperImagesEpisode.ColumnCount = 2
        Me.tblScraperImagesEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperImagesEpisode.Controls.Add(Me.chkScrapeEpisodePoster, 0, 0)
        Me.tblScraperImagesEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperImagesEpisode.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperImagesEpisode.Name = "tblScraperImagesEpisode"
        Me.tblScraperImagesEpisode.RowCount = 2
        Me.tblScraperImagesEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperImagesEpisode.Size = New System.Drawing.Size(64, 69)
        Me.tblScraperImagesEpisode.TabIndex = 0
        '
        'chkScrapeEpisodePoster
        '
        Me.chkScrapeEpisodePoster.AutoSize = True
        Me.chkScrapeEpisodePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeEpisodePoster.Location = New System.Drawing.Point(3, 3)
        Me.chkScrapeEpisodePoster.Name = "chkScrapeEpisodePoster"
        Me.chkScrapeEpisodePoster.Size = New System.Drawing.Size(58, 17)
        Me.chkScrapeEpisodePoster.TabIndex = 0
        Me.chkScrapeEpisodePoster.Text = "Poster"
        Me.chkScrapeEpisodePoster.UseVisualStyleBackColor = True
        '
        'gbScraperOpts
        '
        Me.gbScraperOpts.AutoSize = True
        Me.gbScraperOpts.Controls.Add(Me.tblScraperOpts)
        Me.gbScraperOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbScraperOpts.Location = New System.Drawing.Point(3, 126)
        Me.gbScraperOpts.Name = "gbScraperOpts"
        Me.gbScraperOpts.Size = New System.Drawing.Size(432, 70)
        Me.gbScraperOpts.TabIndex = 95
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
        Me.tblScraperOpts.Controls.Add(Me.lblAPIKey, 0, 0)
        Me.tblScraperOpts.Controls.Add(Me.pbApiKeyInfo, 2, 1)
        Me.tblScraperOpts.Controls.Add(Me.txtApiKey, 1, 1)
        Me.tblScraperOpts.Controls.Add(Me.btnUnlockAPI, 0, 1)
        Me.tblScraperOpts.Controls.Add(Me.lblEMMAPI, 1, 0)
        Me.tblScraperOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperOpts.Name = "tblScraperOpts"
        Me.tblScraperOpts.RowCount = 3
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperOpts.Size = New System.Drawing.Size(426, 49)
        Me.tblScraperOpts.TabIndex = 98
        '
        'lblAPIKey
        '
        Me.lblAPIKey.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblAPIKey.AutoSize = True
        Me.lblAPIKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAPIKey.Location = New System.Drawing.Point(3, 3)
        Me.lblAPIKey.Name = "lblAPIKey"
        Me.lblAPIKey.Size = New System.Drawing.Size(76, 13)
        Me.lblAPIKey.TabIndex = 0
        Me.lblAPIKey.Text = "TVDB API Key:"
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
        'pnlSettingsBottom
        '
        Me.pnlSettingsBottom.AutoSize = True
        Me.pnlSettingsBottom.Controls.Add(Me.tblSettingsBottom)
        Me.pnlSettingsBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlSettingsBottom.Location = New System.Drawing.Point(0, 252)
        Me.pnlSettingsBottom.Name = "pnlSettingsBottom"
        Me.pnlSettingsBottom.Size = New System.Drawing.Size(453, 37)
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
        Me.tblSettingsBottom.Size = New System.Drawing.Size(453, 37)
        Me.tblSettingsBottom.TabIndex = 98
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
        Me.lblInfoBottom.Text = "These settings are specific to this module." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please refer to the global settings " & _
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
        Me.pnlSettingsTop.Size = New System.Drawing.Size(453, 29)
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
        Me.tblSettingsTop.Size = New System.Drawing.Size(453, 29)
        Me.tblSettingsTop.TabIndex = 99
        '
        'btnDown
        '
        Me.btnDown.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(427, 3)
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
        Me.lblScraperOrder.Location = New System.Drawing.Point(334, 8)
        Me.lblScraperOrder.Name = "lblScraperOrder"
        Me.lblScraperOrder.Size = New System.Drawing.Size(58, 12)
        Me.lblScraperOrder.TabIndex = 1
        Me.lblScraperOrder.Text = "Scraper order"
        '
        'btnUp
        '
        Me.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(398, 3)
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
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(453, 289)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
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
        Me.gbScraperImagesSeason.ResumeLayout(False)
        Me.gbScraperImagesSeason.PerformLayout()
        Me.tblScraperImagesSeason.ResumeLayout(False)
        Me.tblScraperImagesSeason.PerformLayout()
        Me.gbScraperImagesTVShow.ResumeLayout(False)
        Me.gbScraperImagesTVShow.PerformLayout()
        Me.tblScraperImagesShow.ResumeLayout(False)
        Me.tblScraperImagesShow.PerformLayout()
        Me.gbScraperImagesEpisode.ResumeLayout(False)
        Me.gbScraperImagesEpisode.PerformLayout()
        Me.tblScraperImagesEpisode.ResumeLayout(False)
        Me.tblScraperImagesEpisode.PerformLayout()
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
    Friend WithEvents chkScrapeShowPoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeShowFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeShowBanner As System.Windows.Forms.CheckBox
    Friend WithEvents tblSettingsBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsBottom As System.Windows.Forms.Panel
    Friend WithEvents tblScraperImagesShow As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblEMMAPI As System.Windows.Forms.Label
    Friend WithEvents btnUnlockAPI As System.Windows.Forms.Button
    Friend WithEvents tblScraperOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblScraperImagesOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbScraperImagesTVShow As System.Windows.Forms.GroupBox
    Friend WithEvents gbScraperImagesSeason As System.Windows.Forms.GroupBox
    Friend WithEvents tblScraperImagesSeason As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkScrapeSeasonBanner As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeSeasonPoster As System.Windows.Forms.CheckBox
    Friend WithEvents gbScraperImagesEpisode As System.Windows.Forms.GroupBox
    Friend WithEvents tblScraperImagesEpisode As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkScrapeEpisodePoster As System.Windows.Forms.CheckBox

End Class