<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTVMediaSettingsHolder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTVMediaSettingsHolder))
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.cbEnabled = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblScraperOrder = New System.Windows.Forms.Label()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.gbScraperFields = New System.Windows.Forms.GroupBox()
        Me.chkGetEnglishImages = New System.Windows.Forms.CheckBox()
        Me.chkOnlyTVImagesLanguage = New System.Windows.Forms.CheckBox()
        Me.gbTVDB = New System.Windows.Forms.GroupBox()
        Me.lblEMMAPI = New System.Windows.Forms.Label()
        Me.btnUnlockAPI = New System.Windows.Forms.Button()
        Me.gbLanguage = New System.Windows.Forms.GroupBox()
        Me.lblTVLanguagePreferred = New System.Windows.Forms.Label()
        Me.cbTVScraperLanguage = New System.Windows.Forms.ComboBox()
        Me.lblTVDBMirror = New System.Windows.Forms.Label()
        Me.txtTVDBMirror = New System.Windows.Forms.TextBox()
        Me.pbTVDB = New System.Windows.Forms.PictureBox()
        Me.lblTVDBApiKey = New System.Windows.Forms.Label()
        Me.txtTVDBApiKey = New System.Windows.Forms.TextBox()
        Me.lblModuleInfo = New System.Windows.Forms.Label()
        Me.pbModuleLogo = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.gbScraperFields.SuspendLayout()
        Me.gbTVDB.SuspendLayout()
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
        Me.pnlSettings.Controls.Add(Me.gbScraperFields)
        Me.pnlSettings.Controls.Add(Me.gbTVDB)
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
        Me.gbScraperFields.Controls.Add(Me.chkGetEnglishImages)
        Me.gbScraperFields.Controls.Add(Me.chkOnlyTVImagesLanguage)
        Me.gbScraperFields.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbScraperFields.Location = New System.Drawing.Point(10, 156)
        Me.gbScraperFields.Name = "gbScraperFields"
        Me.gbScraperFields.Size = New System.Drawing.Size(403, 114)
        Me.gbScraperFields.TabIndex = 77
        Me.gbScraperFields.TabStop = False
        Me.gbScraperFields.Text = "Scraper Fields"
        '
        'chkGetEnglishImages
        '
        Me.chkGetEnglishImages.AutoSize = True
        Me.chkGetEnglishImages.Enabled = False
        Me.chkGetEnglishImages.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkGetEnglishImages.Location = New System.Drawing.Point(19, 39)
        Me.chkGetEnglishImages.Name = "chkGetEnglishImages"
        Me.chkGetEnglishImages.Size = New System.Drawing.Size(149, 17)
        Me.chkGetEnglishImages.TabIndex = 76
        Me.chkGetEnglishImages.Text = "Also Get English Images"
        Me.chkGetEnglishImages.UseVisualStyleBackColor = True
        '
        'chkOnlyTVImagesLanguage
        '
        Me.chkOnlyTVImagesLanguage.AutoSize = True
        Me.chkOnlyTVImagesLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOnlyTVImagesLanguage.Location = New System.Drawing.Point(6, 21)
        Me.chkOnlyTVImagesLanguage.Name = "chkOnlyTVImagesLanguage"
        Me.chkOnlyTVImagesLanguage.Size = New System.Drawing.Size(248, 17)
        Me.chkOnlyTVImagesLanguage.TabIndex = 75
        Me.chkOnlyTVImagesLanguage.Text = "Only Get Images for the Selected Language"
        Me.chkOnlyTVImagesLanguage.UseVisualStyleBackColor = True
        '
        'gbTVDB
        '
        Me.gbTVDB.Controls.Add(Me.lblEMMAPI)
        Me.gbTVDB.Controls.Add(Me.btnUnlockAPI)
        Me.gbTVDB.Controls.Add(Me.gbLanguage)
        Me.gbTVDB.Controls.Add(Me.lblTVDBMirror)
        Me.gbTVDB.Controls.Add(Me.txtTVDBMirror)
        Me.gbTVDB.Controls.Add(Me.pbTVDB)
        Me.gbTVDB.Controls.Add(Me.lblTVDBApiKey)
        Me.gbTVDB.Controls.Add(Me.txtTVDBApiKey)
        Me.gbTVDB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTVDB.Location = New System.Drawing.Point(11, 31)
        Me.gbTVDB.Name = "gbTVDB"
        Me.gbTVDB.Size = New System.Drawing.Size(603, 119)
        Me.gbTVDB.TabIndex = 96
        Me.gbTVDB.TabStop = False
        Me.gbTVDB.Text = "TVDB"
        '
        'lblEMMAPI
        '
        Me.lblEMMAPI.AutoSize = True
        Me.lblEMMAPI.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblEMMAPI.Location = New System.Drawing.Point(142, 35)
        Me.lblEMMAPI.Name = "lblEMMAPI"
        Me.lblEMMAPI.Size = New System.Drawing.Size(142, 13)
        Me.lblEMMAPI.TabIndex = 78
        Me.lblEMMAPI.Text = "Ember Media Manager API"
        '
        'btnUnlockAPI
        '
        Me.btnUnlockAPI.Location = New System.Drawing.Point(9, 32)
        Me.btnUnlockAPI.Name = "btnUnlockAPI"
        Me.btnUnlockAPI.Size = New System.Drawing.Size(127, 23)
        Me.btnUnlockAPI.TabIndex = 77
        Me.btnUnlockAPI.Text = "Use my own API"
        Me.btnUnlockAPI.UseVisualStyleBackColor = True
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
        'txtTVDBApiKey
        '
        Me.txtTVDBApiKey.Enabled = False
        Me.txtTVDBApiKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVDBApiKey.Location = New System.Drawing.Point(145, 32)
        Me.txtTVDBApiKey.Name = "txtTVDBApiKey"
        Me.txtTVDBApiKey.Size = New System.Drawing.Size(234, 22)
        Me.txtTVDBApiKey.TabIndex = 1
        Me.txtTVDBApiKey.Visible = False
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
        Me.pbModuleLogo.InitialImage = CType(resources.GetObject("pbModuleLogo.InitialImage"), System.Drawing.Image)
        Me.pbModuleLogo.Location = New System.Drawing.Point(3, 335)
        Me.pbModuleLogo.Name = "pbModuleLogo"
        Me.pbModuleLogo.Size = New System.Drawing.Size(30, 31)
        Me.pbModuleLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbModuleLogo.TabIndex = 96
        Me.pbModuleLogo.TabStop = False
        '
        'frmTVMediaSettingsHolder
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
        Me.Name = "frmTVMediaSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlSettings.ResumeLayout(False)
        Me.gbScraperFields.ResumeLayout(False)
        Me.gbScraperFields.PerformLayout()
        Me.gbTVDB.ResumeLayout(False)
        Me.gbTVDB.PerformLayout()
        Me.gbLanguage.ResumeLayout(False)
        Me.gbLanguage.PerformLayout()
        CType(Me.pbTVDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbModuleLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents cbEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents lblScraperOrder As System.Windows.Forms.Label
    Friend WithEvents lblModuleInfo As System.Windows.Forms.Label
    Friend WithEvents pbModuleLogo As System.Windows.Forms.PictureBox
    Friend WithEvents gbTVDB As System.Windows.Forms.GroupBox
    Friend WithEvents pbTVDB As System.Windows.Forms.PictureBox
    Friend WithEvents lblTVDBApiKey As System.Windows.Forms.Label
    Friend WithEvents txtTVDBApiKey As System.Windows.Forms.TextBox
    Friend WithEvents lblTVDBMirror As System.Windows.Forms.Label
    Friend WithEvents txtTVDBMirror As System.Windows.Forms.TextBox
    Friend WithEvents gbLanguage As System.Windows.Forms.GroupBox
    Friend WithEvents lblTVLanguagePreferred As System.Windows.Forms.Label
    Friend WithEvents cbTVScraperLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents chkGetEnglishImages As System.Windows.Forms.CheckBox
    Friend WithEvents chkOnlyTVImagesLanguage As System.Windows.Forms.CheckBox
    Friend WithEvents gbScraperFields As System.Windows.Forms.GroupBox
    Friend WithEvents lblEMMAPI As System.Windows.Forms.Label
    Friend WithEvents btnUnlockAPI As System.Windows.Forms.Button

End Class
