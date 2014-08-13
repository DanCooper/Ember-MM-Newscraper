<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTMDBMediaSettingsHolder_MovieSet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTMDBMediaSettingsHolder_MovieSet))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.gbScraper = New System.Windows.Forms.GroupBox()
        Me.chkGetBlankImages = New System.Windows.Forms.CheckBox()
        Me.chkPrefLanguageOnly = New System.Windows.Forms.CheckBox()
        Me.lblEMMAPI = New System.Windows.Forms.Label()
        Me.btnUnlockAPI = New System.Windows.Forms.Button()
        Me.pbTMDB = New System.Windows.Forms.PictureBox()
        Me.chkGetEnglishImages = New System.Windows.Forms.CheckBox()
        Me.cbPrefLanguage = New System.Windows.Forms.ComboBox()
        Me.lblPrefLanguage = New System.Windows.Forms.Label()
        Me.lblAPIKey = New System.Windows.Forms.Label()
        Me.txtApiKey = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.gbImages = New System.Windows.Forms.GroupBox()
        Me.chkScrapePoster = New System.Windows.Forms.CheckBox()
        Me.chkScrapeFanart = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.cbEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.gbScraper.SuspendLayout()
        CType(Me.pbTMDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbImages.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.Controls.Add(Me.gbScraper)
        Me.pnlSettings.Controls.Add(Me.Label1)
        Me.pnlSettings.Controls.Add(Me.PictureBox1)
        Me.pnlSettings.Controls.Add(Me.gbImages)
        Me.pnlSettings.Controls.Add(Me.Panel2)
        Me.pnlSettings.Location = New System.Drawing.Point(12, 4)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(617, 369)
        Me.pnlSettings.TabIndex = 0
        '
        'gbScraper
        '
        Me.gbScraper.Controls.Add(Me.chkGetBlankImages)
        Me.gbScraper.Controls.Add(Me.chkPrefLanguageOnly)
        Me.gbScraper.Controls.Add(Me.lblEMMAPI)
        Me.gbScraper.Controls.Add(Me.btnUnlockAPI)
        Me.gbScraper.Controls.Add(Me.pbTMDB)
        Me.gbScraper.Controls.Add(Me.chkGetEnglishImages)
        Me.gbScraper.Controls.Add(Me.cbPrefLanguage)
        Me.gbScraper.Controls.Add(Me.lblPrefLanguage)
        Me.gbScraper.Controls.Add(Me.lblAPIKey)
        Me.gbScraper.Controls.Add(Me.txtApiKey)
        Me.gbScraper.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbScraper.Location = New System.Drawing.Point(10, 31)
        Me.gbScraper.Name = "gbScraper"
        Me.gbScraper.Size = New System.Drawing.Size(600, 140)
        Me.gbScraper.TabIndex = 95
        Me.gbScraper.TabStop = False
        Me.gbScraper.Text = "TMDB"
        '
        'chkGetBlankImages
        '
        Me.chkGetBlankImages.AutoSize = True
        Me.chkGetBlankImages.Enabled = False
        Me.chkGetBlankImages.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGetBlankImages.Location = New System.Drawing.Point(212, 113)
        Me.chkGetBlankImages.Name = "chkGetBlankImages"
        Me.chkGetBlankImages.Size = New System.Drawing.Size(140, 17)
        Me.chkGetBlankImages.TabIndex = 16
        Me.chkGetBlankImages.Text = "Also Get Blank Images"
        Me.chkGetBlankImages.UseVisualStyleBackColor = True
        '
        'chkPrefLanguageOnly
        '
        Me.chkPrefLanguageOnly.AutoSize = True
        Me.chkPrefLanguageOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrefLanguageOnly.Location = New System.Drawing.Point(194, 67)
        Me.chkPrefLanguageOnly.Name = "chkPrefLanguageOnly"
        Me.chkPrefLanguageOnly.Size = New System.Drawing.Size(248, 17)
        Me.chkPrefLanguageOnly.TabIndex = 15
        Me.chkPrefLanguageOnly.Text = "Only Get Images for the Selected Language"
        Me.chkPrefLanguageOnly.UseVisualStyleBackColor = True
        '
        'lblEMMAPI
        '
        Me.lblEMMAPI.AutoSize = True
        Me.lblEMMAPI.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblEMMAPI.Location = New System.Drawing.Point(194, 39)
        Me.lblEMMAPI.Name = "lblEMMAPI"
        Me.lblEMMAPI.Size = New System.Drawing.Size(162, 13)
        Me.lblEMMAPI.TabIndex = 14
        Me.lblEMMAPI.Text = "Ember Media Manager API key"
        '
        'btnUnlockAPI
        '
        Me.btnUnlockAPI.Location = New System.Drawing.Point(9, 34)
        Me.btnUnlockAPI.Name = "btnUnlockAPI"
        Me.btnUnlockAPI.Size = New System.Drawing.Size(179, 23)
        Me.btnUnlockAPI.TabIndex = 13
        Me.btnUnlockAPI.Text = "Use my own API key"
        Me.btnUnlockAPI.UseVisualStyleBackColor = True
        '
        'pbTMDB
        '
        Me.pbTMDB.Image = CType(resources.GetObject("pbTMDB.Image"), System.Drawing.Image)
        Me.pbTMDB.Location = New System.Drawing.Point(574, 39)
        Me.pbTMDB.Name = "pbTMDB"
        Me.pbTMDB.Size = New System.Drawing.Size(16, 16)
        Me.pbTMDB.TabIndex = 5
        Me.pbTMDB.TabStop = False
        '
        'chkGetEnglishImages
        '
        Me.chkGetEnglishImages.AutoSize = True
        Me.chkGetEnglishImages.Enabled = False
        Me.chkGetEnglishImages.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGetEnglishImages.Location = New System.Drawing.Point(212, 90)
        Me.chkGetEnglishImages.Name = "chkGetEnglishImages"
        Me.chkGetEnglishImages.Size = New System.Drawing.Size(149, 17)
        Me.chkGetEnglishImages.TabIndex = 4
        Me.chkGetEnglishImages.Text = "Also Get English Images"
        Me.chkGetEnglishImages.UseVisualStyleBackColor = True
        '
        'cbPrefLanguage
        '
        Me.cbPrefLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPrefLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cbPrefLanguage.FormattingEnabled = True
        Me.cbPrefLanguage.Items.AddRange(New Object() {"bg", "cs", "da", "de", "el", "en", "es", "fi", "fr", "he", "hu", "it", "nb", "nl", "no", "pl", "pt", "ru", "sk", "sv", "ta", "tr", "uk", "vi", "xx", "zh"})
        Me.cbPrefLanguage.Location = New System.Drawing.Point(123, 65)
        Me.cbPrefLanguage.Name = "cbPrefLanguage"
        Me.cbPrefLanguage.Size = New System.Drawing.Size(45, 21)
        Me.cbPrefLanguage.TabIndex = 3
        '
        'lblPrefLanguage
        '
        Me.lblPrefLanguage.AutoSize = True
        Me.lblPrefLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblPrefLanguage.Location = New System.Drawing.Point(6, 68)
        Me.lblPrefLanguage.Name = "lblPrefLanguage"
        Me.lblPrefLanguage.Size = New System.Drawing.Size(111, 13)
        Me.lblPrefLanguage.TabIndex = 2
        Me.lblPrefLanguage.Text = "Preferred Language:"
        '
        'lblAPIKey
        '
        Me.lblAPIKey.AutoSize = True
        Me.lblAPIKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAPIKey.Location = New System.Drawing.Point(6, 18)
        Me.lblAPIKey.Name = "lblAPIKey"
        Me.lblAPIKey.Size = New System.Drawing.Size(79, 13)
        Me.lblAPIKey.TabIndex = 0
        Me.lblAPIKey.Text = "TMDB API Key:"
        '
        'txtApiKey
        '
        Me.txtApiKey.Enabled = False
        Me.txtApiKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApiKey.Location = New System.Drawing.Point(194, 35)
        Me.txtApiKey.Name = "txtApiKey"
        Me.txtApiKey.Size = New System.Drawing.Size(374, 22)
        Me.txtApiKey.TabIndex = 1
        Me.txtApiKey.Visible = False
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
        'gbImages
        '
        Me.gbImages.Controls.Add(Me.chkScrapePoster)
        Me.gbImages.Controls.Add(Me.chkScrapeFanart)
        Me.gbImages.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbImages.Location = New System.Drawing.Point(10, 177)
        Me.gbImages.Name = "gbImages"
        Me.gbImages.Size = New System.Drawing.Size(130, 72)
        Me.gbImages.TabIndex = 1
        Me.gbImages.TabStop = False
        Me.gbImages.Text = "Images"
        '
        'chkScrapePoster
        '
        Me.chkScrapePoster.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapePoster.Location = New System.Drawing.Point(6, 21)
        Me.chkScrapePoster.Name = "chkScrapePoster"
        Me.chkScrapePoster.Size = New System.Drawing.Size(114, 15)
        Me.chkScrapePoster.TabIndex = 0
        Me.chkScrapePoster.Text = "Get Posters"
        Me.chkScrapePoster.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapePoster.UseVisualStyleBackColor = True
        '
        'chkScrapeFanart
        '
        Me.chkScrapeFanart.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapeFanart.Location = New System.Drawing.Point(6, 44)
        Me.chkScrapeFanart.Name = "chkScrapeFanart"
        Me.chkScrapeFanart.Size = New System.Drawing.Size(84, 16)
        Me.chkScrapeFanart.TabIndex = 1
        Me.chkScrapeFanart.Text = "Get Fanart"
        Me.chkScrapeFanart.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkScrapeFanart.UseVisualStyleBackColor = True
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
        'frmTMDBMediaSettingsHolder_MovieSet
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
        Me.Name = "frmTMDBMediaSettingsHolder_MovieSet"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
        Me.pnlSettings.ResumeLayout(False)
        Me.gbScraper.ResumeLayout(False)
        Me.gbScraper.PerformLayout()
        CType(Me.pbTMDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbImages.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents chkScrapeFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapePoster As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cbEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents gbImages As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents gbScraper As System.Windows.Forms.GroupBox
    Friend WithEvents pbTMDB As System.Windows.Forms.PictureBox
    Friend WithEvents chkGetEnglishImages As System.Windows.Forms.CheckBox
    Friend WithEvents cbPrefLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents lblPrefLanguage As System.Windows.Forms.Label
    Friend WithEvents lblAPIKey As System.Windows.Forms.Label
    Friend WithEvents txtApiKey As System.Windows.Forms.TextBox
    Friend WithEvents lblEMMAPI As System.Windows.Forms.Label
    Friend WithEvents btnUnlockAPI As System.Windows.Forms.Button
    Friend WithEvents chkPrefLanguageOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkGetBlankImages As System.Windows.Forms.CheckBox

End Class