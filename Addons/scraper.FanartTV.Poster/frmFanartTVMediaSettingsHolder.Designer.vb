<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFanartTVMediaSettingsHolder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFanartTVMediaSettingsHolder))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.gbImages = New System.Windows.Forms.GroupBox()
        Me.chkScrapeLandscape = New System.Windows.Forms.CheckBox()
        Me.chkScrapeDiscArt = New System.Windows.Forms.CheckBox()
        Me.chkScrapeCharacterArt = New System.Windows.Forms.CheckBox()
        Me.chkScrapeClearArt = New System.Windows.Forms.CheckBox()
        Me.chkScrapeClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkScrapeBanner = New System.Windows.Forms.CheckBox()
        Me.chkScrapePoster = New System.Windows.Forms.CheckBox()
        Me.chkScrapeFanart = New System.Windows.Forms.CheckBox()
        Me.gbAPIKey = New System.Windows.Forms.GroupBox()
        Me.cbFANARTTVLanguage = New System.Windows.Forms.ComboBox()
        Me.lblFANARTTVPrefLanguage = New System.Windows.Forms.Label()
        Me.pbFANARTTV = New System.Windows.Forms.PictureBox()
        Me.lblAPIKey = New System.Windows.Forms.Label()
        Me.txtFANARTTVApiKey = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.cbEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.gbImages.SuspendLayout()
        Me.gbAPIKey.SuspendLayout()
        CType(Me.pbFANARTTV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.Controls.Add(Me.gbImages)
        Me.pnlSettings.Controls.Add(Me.gbAPIKey)
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
        Me.gbImages.Controls.Add(Me.chkScrapeLandscape)
        Me.gbImages.Controls.Add(Me.chkScrapeDiscArt)
        Me.gbImages.Controls.Add(Me.chkScrapeCharacterArt)
        Me.gbImages.Controls.Add(Me.chkScrapeClearArt)
        Me.gbImages.Controls.Add(Me.chkScrapeClearLogo)
        Me.gbImages.Controls.Add(Me.chkScrapeBanner)
        Me.gbImages.Controls.Add(Me.chkScrapePoster)
        Me.gbImages.Controls.Add(Me.chkScrapeFanart)
        Me.gbImages.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbImages.Location = New System.Drawing.Point(11, 139)
        Me.gbImages.Name = "gbImages"
        Me.gbImages.Size = New System.Drawing.Size(306, 116)
        Me.gbImages.TabIndex = 96
        Me.gbImages.TabStop = False
        Me.gbImages.Text = "Images"
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
        'gbAPIKey
        '
        Me.gbAPIKey.Controls.Add(Me.cbFANARTTVLanguage)
        Me.gbAPIKey.Controls.Add(Me.lblFANARTTVPrefLanguage)
        Me.gbAPIKey.Controls.Add(Me.pbFANARTTV)
        Me.gbAPIKey.Controls.Add(Me.lblAPIKey)
        Me.gbAPIKey.Controls.Add(Me.txtFANARTTVApiKey)
        Me.gbAPIKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbAPIKey.Location = New System.Drawing.Point(11, 31)
        Me.gbAPIKey.Name = "gbAPIKey"
        Me.gbAPIKey.Size = New System.Drawing.Size(513, 102)
        Me.gbAPIKey.TabIndex = 95
        Me.gbAPIKey.TabStop = False
        Me.gbAPIKey.Text = "Fanart.tv"
        '
        'cbFANARTTVLanguage
        '
        Me.cbFANARTTVLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFANARTTVLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cbFANARTTVLanguage.FormattingEnabled = True
        Me.cbFANARTTVLanguage.Items.AddRange(New Object() {"bg", "cs", "da", "de", "el", "en", "es", "fi", "fr", "he", "hu", "it", "nb", "nl", "no", "pl", "pt", "ru", "sk", "sv", "ta", "tr", "uk", "vi", "xx", "zh"})
        Me.cbFANARTTVLanguage.Location = New System.Drawing.Point(123, 65)
        Me.cbFANARTTVLanguage.Name = "cbFANARTTVLanguage"
        Me.cbFANARTTVLanguage.Size = New System.Drawing.Size(45, 21)
        Me.cbFANARTTVLanguage.TabIndex = 8
        '
        'lblFANARTTVPrefLanguage
        '
        Me.lblFANARTTVPrefLanguage.AutoSize = True
        Me.lblFANARTTVPrefLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblFANARTTVPrefLanguage.Location = New System.Drawing.Point(6, 68)
        Me.lblFANARTTVPrefLanguage.Name = "lblFANARTTVPrefLanguage"
        Me.lblFANARTTVPrefLanguage.Size = New System.Drawing.Size(111, 13)
        Me.lblFANARTTVPrefLanguage.TabIndex = 7
        Me.lblFANARTTVPrefLanguage.Text = "Preferred Language:"
        '
        'pbFANARTTV
        '
        Me.pbFANARTTV.Image = CType(resources.GetObject("pbFANARTTV.Image"), System.Drawing.Image)
        Me.pbFANARTTV.Location = New System.Drawing.Point(394, 32)
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
        Me.lblAPIKey.Size = New System.Drawing.Size(94, 13)
        Me.lblAPIKey.TabIndex = 0
        Me.lblAPIKey.Text = "Fanart.tv API Key:"
        '
        'txtFANARTTVApiKey
        '
        Me.txtFANARTTVApiKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFANARTTVApiKey.Location = New System.Drawing.Point(8, 32)
        Me.txtFANARTTVApiKey.Name = "txtFANARTTVApiKey"
        Me.txtFANARTTVApiKey.Size = New System.Drawing.Size(369, 22)
        Me.txtFANARTTVApiKey.TabIndex = 1
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
        'frmFanartTVMediaSettingsHolder
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
        Me.Name = "frmFanartTVMediaSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
        Me.pnlSettings.ResumeLayout(False)
        Me.gbImages.ResumeLayout(False)
        Me.gbImages.PerformLayout()
        Me.gbAPIKey.ResumeLayout(False)
        Me.gbAPIKey.PerformLayout()
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
    Friend WithEvents gbAPIKey As System.Windows.Forms.GroupBox
    Friend WithEvents pbFANARTTV As System.Windows.Forms.PictureBox
    Friend WithEvents lblAPIKey As System.Windows.Forms.Label
    Friend WithEvents txtFANARTTVApiKey As System.Windows.Forms.TextBox
    Friend WithEvents gbImages As System.Windows.Forms.GroupBox
    Friend WithEvents chkScrapePoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeFanart As System.Windows.Forms.CheckBox
    Friend WithEvents cbFANARTTVLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents lblFANARTTVPrefLanguage As System.Windows.Forms.Label
    Friend WithEvents chkScrapeLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeDiscArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeCharacterArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents chkScrapeBanner As System.Windows.Forms.CheckBox

End Class