<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFanartTVMediaSettingsHolder_Movie
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFanartTVMediaSettingsHolder_Movie))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.gbImages = New System.Windows.Forms.GroupBox()
        Me.chkScrapeClearArtOnlyHD = New System.Windows.Forms.CheckBox()
        Me.chkScrapeClearLogoOnlyHD = New System.Windows.Forms.CheckBox()
        Me.chkScrapeLandscape = New System.Windows.Forms.CheckBox()
        Me.chkScrapeDiscArt = New System.Windows.Forms.CheckBox()
        Me.chkScrapeCharacterArt = New System.Windows.Forms.CheckBox()
        Me.chkScrapeClearArt = New System.Windows.Forms.CheckBox()
        Me.chkScrapeClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkScrapeBanner = New System.Windows.Forms.CheckBox()
        Me.chkScrapePoster = New System.Windows.Forms.CheckBox()
        Me.chkScrapeFanart = New System.Windows.Forms.CheckBox()
        Me.gbScraper = New System.Windows.Forms.GroupBox()
        Me.lblAPIHint = New System.Windows.Forms.Label()
        Me.chkGetBlankImages = New System.Windows.Forms.CheckBox()
        Me.chkPrefLanguageOnly = New System.Windows.Forms.CheckBox()
        Me.chkGetEnglishImages = New System.Windows.Forms.CheckBox()
        Me.cbPrefLanguage = New System.Windows.Forms.ComboBox()
        Me.lblPrefLanguage = New System.Windows.Forms.Label()
        Me.pbFANARTTV = New System.Windows.Forms.PictureBox()
        Me.lblAPIKey = New System.Windows.Forms.Label()
        Me.txtApiKey = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.cbEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.gbImages.SuspendLayout()
        Me.gbScraper.SuspendLayout()
        CType(Me.pbFANARTTV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.Controls.Add(Me.gbImages)
        Me.pnlSettings.Controls.Add(Me.gbScraper)
        Me.pnlSettings.Controls.Add(Me.Label1)
        Me.pnlSettings.Controls.Add(Me.PictureBox1)
        Me.pnlSettings.Controls.Add(Me.Panel2)
        Me.pnlSettings.Location = New System.Drawing.Point(12, 4)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(617, 369)
        Me.pnlSettings.TabIndex = 0
        '
        'gbImages
        '
        Me.gbImages.Controls.Add(Me.chkScrapeClearArtOnlyHD)
        Me.gbImages.Controls.Add(Me.chkScrapeClearLogoOnlyHD)
        Me.gbImages.Controls.Add(Me.chkScrapeLandscape)
        Me.gbImages.Controls.Add(Me.chkScrapeDiscArt)
        Me.gbImages.Controls.Add(Me.chkScrapeCharacterArt)
        Me.gbImages.Controls.Add(Me.chkScrapeClearArt)
        Me.gbImages.Controls.Add(Me.chkScrapeClearLogo)
        Me.gbImages.Controls.Add(Me.chkScrapeBanner)
        Me.gbImages.Controls.Add(Me.chkScrapePoster)
        Me.gbImages.Controls.Add(Me.chkScrapeFanart)
        Me.gbImages.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbImages.Location = New System.Drawing.Point(10, 206)
        Me.gbImages.Name = "gbImages"
        Me.gbImages.Size = New System.Drawing.Size(410, 116)
        Me.gbImages.TabIndex = 96
        Me.gbImages.TabStop = False
        Me.gbImages.Text = "Images"
        '
        'chkScrapeClearArtOnlyHD
        '
        Me.chkScrapeClearArtOnlyHD.AutoSize = True
        Me.chkScrapeClearArtOnlyHD.Enabled = False
        Me.chkScrapeClearArtOnlyHD.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkScrapeClearArtOnlyHD.Location = New System.Drawing.Point(308, 44)
        Me.chkScrapeClearArtOnlyHD.Name = "chkScrapeClearArtOnlyHD"
        Me.chkScrapeClearArtOnlyHD.Size = New System.Drawing.Size(69, 17)
        Me.chkScrapeClearArtOnlyHD.TabIndex = 9
        Me.chkScrapeClearArtOnlyHD.Text = "Only HD"
        Me.chkScrapeClearArtOnlyHD.UseVisualStyleBackColor = True
        '
        'chkScrapeClearLogoOnlyHD
        '
        Me.chkScrapeClearLogoOnlyHD.AutoSize = True
        Me.chkScrapeClearLogoOnlyHD.Enabled = False
        Me.chkScrapeClearLogoOnlyHD.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkScrapeClearLogoOnlyHD.Location = New System.Drawing.Point(308, 67)
        Me.chkScrapeClearLogoOnlyHD.Name = "chkScrapeClearLogoOnlyHD"
        Me.chkScrapeClearLogoOnlyHD.Size = New System.Drawing.Size(69, 17)
        Me.chkScrapeClearLogoOnlyHD.TabIndex = 8
        Me.chkScrapeClearLogoOnlyHD.Text = "Only HD"
        Me.chkScrapeClearLogoOnlyHD.UseVisualStyleBackColor = True
        '
        'chkScrapeLandscape
        '
        Me.chkScrapeLandscape.AutoSize = True
        Me.chkScrapeLandscape.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeLandscape.Location = New System.Drawing.Point(6, 90)
        Me.chkScrapeLandscape.Name = "chkScrapeLandscape"
        Me.chkScrapeLandscape.Size = New System.Drawing.Size(101, 17)
        Me.chkScrapeLandscape.TabIndex = 7
        Me.chkScrapeLandscape.Text = "Get Landscape"
        Me.chkScrapeLandscape.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeLandscape.UseVisualStyleBackColor = True
        '
        'chkScrapeDiscArt
        '
        Me.chkScrapeDiscArt.AutoSize = True
        Me.chkScrapeDiscArt.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeDiscArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeDiscArt.Location = New System.Drawing.Point(152, 90)
        Me.chkScrapeDiscArt.Name = "chkScrapeDiscArt"
        Me.chkScrapeDiscArt.Size = New System.Drawing.Size(83, 17)
        Me.chkScrapeDiscArt.TabIndex = 6
        Me.chkScrapeDiscArt.Text = "Get DiscArt"
        Me.chkScrapeDiscArt.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeDiscArt.UseVisualStyleBackColor = True
        '
        'chkScrapeCharacterArt
        '
        Me.chkScrapeCharacterArt.AutoSize = True
        Me.chkScrapeCharacterArt.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeCharacterArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeCharacterArt.Location = New System.Drawing.Point(152, 21)
        Me.chkScrapeCharacterArt.Name = "chkScrapeCharacterArt"
        Me.chkScrapeCharacterArt.Size = New System.Drawing.Size(111, 17)
        Me.chkScrapeCharacterArt.TabIndex = 5
        Me.chkScrapeCharacterArt.Text = "Get CharacterArt"
        Me.chkScrapeCharacterArt.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeCharacterArt.UseVisualStyleBackColor = True
        '
        'chkScrapeClearArt
        '
        Me.chkScrapeClearArt.AutoSize = True
        Me.chkScrapeClearArt.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeClearArt.Location = New System.Drawing.Point(152, 44)
        Me.chkScrapeClearArt.Name = "chkScrapeClearArt"
        Me.chkScrapeClearArt.Size = New System.Drawing.Size(88, 17)
        Me.chkScrapeClearArt.TabIndex = 4
        Me.chkScrapeClearArt.Text = "Get ClearArt"
        Me.chkScrapeClearArt.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeClearArt.UseVisualStyleBackColor = True
        '
        'chkScrapeClearLogo
        '
        Me.chkScrapeClearLogo.AutoSize = True
        Me.chkScrapeClearLogo.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeClearLogo.Location = New System.Drawing.Point(152, 67)
        Me.chkScrapeClearLogo.Name = "chkScrapeClearLogo"
        Me.chkScrapeClearLogo.Size = New System.Drawing.Size(99, 17)
        Me.chkScrapeClearLogo.TabIndex = 3
        Me.chkScrapeClearLogo.Text = "Get ClearLogo"
        Me.chkScrapeClearLogo.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeClearLogo.UseVisualStyleBackColor = True
        '
        'chkScrapeBanner
        '
        Me.chkScrapeBanner.AutoSize = True
        Me.chkScrapeBanner.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeBanner.Location = New System.Drawing.Point(6, 67)
        Me.chkScrapeBanner.Name = "chkScrapeBanner"
        Me.chkScrapeBanner.Size = New System.Drawing.Size(84, 17)
        Me.chkScrapeBanner.TabIndex = 2
        Me.chkScrapeBanner.Text = "Get Banner"
        Me.chkScrapeBanner.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeBanner.UseVisualStyleBackColor = True
        '
        'chkScrapePoster
        '
        Me.chkScrapePoster.AutoSize = True
        Me.chkScrapePoster.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapePoster.Location = New System.Drawing.Point(6, 21)
        Me.chkScrapePoster.Name = "chkScrapePoster"
        Me.chkScrapePoster.Size = New System.Drawing.Size(79, 17)
        Me.chkScrapePoster.TabIndex = 0
        Me.chkScrapePoster.Text = "Get Poster"
        Me.chkScrapePoster.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapePoster.UseVisualStyleBackColor = True
        '
        'chkScrapeFanart
        '
        Me.chkScrapeFanart.AutoSize = True
        Me.chkScrapeFanart.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeFanart.Location = New System.Drawing.Point(6, 44)
        Me.chkScrapeFanart.Name = "chkScrapeFanart"
        Me.chkScrapeFanart.Size = New System.Drawing.Size(80, 17)
        Me.chkScrapeFanart.TabIndex = 1
        Me.chkScrapeFanart.Text = "Get Fanart"
        Me.chkScrapeFanart.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeFanart.UseVisualStyleBackColor = True
        '
        'gbScraper
        '
        Me.gbScraper.Controls.Add(Me.lblAPIHint)
        Me.gbScraper.Controls.Add(Me.chkGetBlankImages)
        Me.gbScraper.Controls.Add(Me.chkPrefLanguageOnly)
        Me.gbScraper.Controls.Add(Me.chkGetEnglishImages)
        Me.gbScraper.Controls.Add(Me.cbPrefLanguage)
        Me.gbScraper.Controls.Add(Me.lblPrefLanguage)
        Me.gbScraper.Controls.Add(Me.pbFANARTTV)
        Me.gbScraper.Controls.Add(Me.lblAPIKey)
        Me.gbScraper.Controls.Add(Me.txtApiKey)
        Me.gbScraper.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbScraper.Location = New System.Drawing.Point(10, 31)
        Me.gbScraper.Name = "gbScraper"
        Me.gbScraper.Size = New System.Drawing.Size(600, 169)
        Me.gbScraper.TabIndex = 95
        Me.gbScraper.TabStop = False
        Me.gbScraper.Text = "Fanart.tv"
        '
        'lblAPIHint
        '
        Me.lblAPIHint.AutoSize = True
        Me.lblAPIHint.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblAPIHint.Location = New System.Drawing.Point(6, 62)
        Me.lblAPIHint.Name = "lblAPIHint"
        Me.lblAPIHint.Size = New System.Drawing.Size(579, 13)
        Me.lblAPIHint.TabIndex = 24
        Me.lblAPIHint.Text = "Using a Personal API Key reduces the time you have to wait for new images to show" & _
    " up from 7 days to 48 hours."
        '
        'chkGetBlankImages
        '
        Me.chkGetBlankImages.AutoSize = True
        Me.chkGetBlankImages.Enabled = False
        Me.chkGetBlankImages.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGetBlankImages.Location = New System.Drawing.Point(212, 146)
        Me.chkGetBlankImages.Name = "chkGetBlankImages"
        Me.chkGetBlankImages.Size = New System.Drawing.Size(140, 17)
        Me.chkGetBlankImages.TabIndex = 19
        Me.chkGetBlankImages.Text = "Also Get Blank Images"
        Me.chkGetBlankImages.UseVisualStyleBackColor = True
        '
        'chkPrefLanguageOnly
        '
        Me.chkPrefLanguageOnly.AutoSize = True
        Me.chkPrefLanguageOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrefLanguageOnly.Location = New System.Drawing.Point(194, 100)
        Me.chkPrefLanguageOnly.Name = "chkPrefLanguageOnly"
        Me.chkPrefLanguageOnly.Size = New System.Drawing.Size(248, 17)
        Me.chkPrefLanguageOnly.TabIndex = 18
        Me.chkPrefLanguageOnly.Text = "Only Get Images for the Selected Language"
        Me.chkPrefLanguageOnly.UseVisualStyleBackColor = True
        '
        'chkGetEnglishImages
        '
        Me.chkGetEnglishImages.AutoSize = True
        Me.chkGetEnglishImages.Enabled = False
        Me.chkGetEnglishImages.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGetEnglishImages.Location = New System.Drawing.Point(212, 123)
        Me.chkGetEnglishImages.Name = "chkGetEnglishImages"
        Me.chkGetEnglishImages.Size = New System.Drawing.Size(149, 17)
        Me.chkGetEnglishImages.TabIndex = 17
        Me.chkGetEnglishImages.Text = "Also Get English Images"
        Me.chkGetEnglishImages.UseVisualStyleBackColor = True
        '
        'cbPrefLanguage
        '
        Me.cbPrefLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPrefLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cbPrefLanguage.FormattingEnabled = True
        Me.cbPrefLanguage.Items.AddRange(New Object() {"bg", "cs", "da", "de", "el", "en", "es", "fi", "fr", "he", "hu", "it", "nb", "nl", "no", "pl", "pt", "ru", "sk", "sv", "ta", "tr", "uk", "vi", "xx", "zh"})
        Me.cbPrefLanguage.Location = New System.Drawing.Point(123, 96)
        Me.cbPrefLanguage.Name = "cbPrefLanguage"
        Me.cbPrefLanguage.Size = New System.Drawing.Size(45, 21)
        Me.cbPrefLanguage.TabIndex = 8
        '
        'lblPrefLanguage
        '
        Me.lblPrefLanguage.AutoSize = True
        Me.lblPrefLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblPrefLanguage.Location = New System.Drawing.Point(6, 99)
        Me.lblPrefLanguage.Name = "lblPrefLanguage"
        Me.lblPrefLanguage.Size = New System.Drawing.Size(111, 13)
        Me.lblPrefLanguage.TabIndex = 7
        Me.lblPrefLanguage.Text = "Preferred Language:"
        '
        'pbFANARTTV
        '
        Me.pbFANARTTV.Image = CType(resources.GetObject("pbFANARTTV.Image"), System.Drawing.Image)
        Me.pbFANARTTV.Location = New System.Drawing.Point(569, 40)
        Me.pbFANARTTV.Name = "pbFANARTTV"
        Me.pbFANARTTV.Size = New System.Drawing.Size(16, 16)
        Me.pbFANARTTV.TabIndex = 6
        Me.pbFANARTTV.TabStop = False
        '
        'lblAPIKey
        '
        Me.lblAPIKey.AutoSize = True
        Me.lblAPIKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAPIKey.Location = New System.Drawing.Point(6, 18)
        Me.lblAPIKey.Name = "lblAPIKey"
        Me.lblAPIKey.Size = New System.Drawing.Size(141, 13)
        Me.lblAPIKey.TabIndex = 0
        Me.lblAPIKey.Text = "Fanart.tv Personal API Key:"
        '
        'txtApiKey
        '
        Me.txtApiKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApiKey.Location = New System.Drawing.Point(9, 37)
        Me.txtApiKey.Name = "txtApiKey"
        Me.txtApiKey.Size = New System.Drawing.Size(554, 22)
        Me.txtApiKey.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(37, 337)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(225, 31)
        Me.Label1.TabIndex = 3
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
        Me.PictureBox1.TabIndex = 94
        Me.PictureBox1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.btnDown)
        Me.Panel2.Controls.Add(Me.btnUp)
        Me.Panel2.Controls.Add(Me.cbEnabled)
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1125, 25)
        Me.Panel2.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(500, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 12)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Scraper order"
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
        'cbEnabled
        '
        Me.cbEnabled.AutoSize = True
        Me.cbEnabled.Location = New System.Drawing.Point(10, 5)
        Me.cbEnabled.Name = "cbEnabled"
        Me.cbEnabled.Size = New System.Drawing.Size(68, 17)
        Me.cbEnabled.TabIndex = 0
        Me.cbEnabled.Text = "Enabled"
        Me.cbEnabled.UseVisualStyleBackColor = True
        '
        'frmFanartTVMediaSettingsHolder_Movie
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(652, 388)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFanartTVMediaSettingsHolder_Movie"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
        Me.pnlSettings.ResumeLayout(False)
        Me.gbImages.ResumeLayout(False)
        Me.gbImages.PerformLayout()
        Me.gbScraper.ResumeLayout(False)
        Me.gbScraper.PerformLayout()
        CType(Me.pbFANARTTV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cbEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents gbScraper As System.Windows.Forms.GroupBox
    Friend WithEvents pbFANARTTV As System.Windows.Forms.PictureBox
    Friend WithEvents lblAPIKey As System.Windows.Forms.Label
    Friend WithEvents txtApiKey As System.Windows.Forms.TextBox
    Friend WithEvents gbImages As System.Windows.Forms.GroupBox
    Friend WithEvents chkScrapePoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeFanart As System.Windows.Forms.CheckBox
    Friend WithEvents cbPrefLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents lblPrefLanguage As System.Windows.Forms.Label
    Friend WithEvents chkScrapeLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeDiscArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeCharacterArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeBanner As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeClearLogoOnlyHD As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeClearArtOnlyHD As System.Windows.Forms.CheckBox
    Friend WithEvents chkGetBlankImages As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrefLanguageOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkGetEnglishImages As System.Windows.Forms.CheckBox
    Friend WithEvents lblAPIHint As System.Windows.Forms.Label

End Class