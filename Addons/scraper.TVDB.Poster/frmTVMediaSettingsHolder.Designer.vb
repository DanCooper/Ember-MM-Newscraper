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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.GroupBox30 = New System.Windows.Forms.GroupBox()
        Me.pbTVDB = New System.Windows.Forms.PictureBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtTVDBApiKey = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblTVDBMirror = New System.Windows.Forms.Label()
        Me.txtTVDBMirror = New System.Windows.Forms.TextBox()
        Me.gbLanguage = New System.Windows.Forms.GroupBox()
        Me.lblTVLanguagePreferred = New System.Windows.Forms.Label()
        Me.btnTVLanguageFetch = New System.Windows.Forms.Button()
        Me.cbTVLanguage = New System.Windows.Forms.ComboBox()
        Me.chkGetEnglishImages = New System.Windows.Forms.CheckBox()
        Me.chkOnlyTVImagesLanguage = New System.Windows.Forms.CheckBox()
        Me.GroupBox32 = New System.Windows.Forms.GroupBox()
        Me.Panel1.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.GroupBox30.SuspendLayout()
        CType(Me.pbTVDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbLanguage.SuspendLayout()
        Me.GroupBox32.SuspendLayout()
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
        Me.pnlSettings.Controls.Add(Me.GroupBox32)
        Me.pnlSettings.Controls.Add(Me.GroupBox30)
        Me.pnlSettings.Controls.Add(Me.Label1)
        Me.pnlSettings.Controls.Add(Me.PictureBox1)
        Me.pnlSettings.Controls.Add(Me.Panel1)
        Me.pnlSettings.Location = New System.Drawing.Point(12, 1)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(617, 369)
        Me.pnlSettings.TabIndex = 0
        '
        'GroupBox30
        '
        Me.GroupBox30.Controls.Add(Me.gbLanguage)
        Me.GroupBox30.Controls.Add(Me.lblTVDBMirror)
        Me.GroupBox30.Controls.Add(Me.txtTVDBMirror)
        Me.GroupBox30.Controls.Add(Me.pbTVDB)
        Me.GroupBox30.Controls.Add(Me.Label18)
        Me.GroupBox30.Controls.Add(Me.txtTVDBApiKey)
        Me.GroupBox30.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox30.Location = New System.Drawing.Point(11, 31)
        Me.GroupBox30.Name = "GroupBox30"
        Me.GroupBox30.Size = New System.Drawing.Size(603, 119)
        Me.GroupBox30.TabIndex = 96
        Me.GroupBox30.TabStop = False
        Me.GroupBox30.Text = "TMDB"
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
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(6, 18)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(79, 13)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "TMDB API Key:"
        '
        'txtTVDBApiKey
        '
        Me.txtTVDBApiKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVDBApiKey.Location = New System.Drawing.Point(8, 32)
        Me.txtTVDBApiKey.Name = "txtTVDBApiKey"
        Me.txtTVDBApiKey.Size = New System.Drawing.Size(371, 22)
        Me.txtTVDBApiKey.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(37, 337)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(225, 31)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "These settings are specific to this module." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please refer to the global settings " & _
    "for more options."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 335)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(30, 31)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 96
        Me.PictureBox1.TabStop = False
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
        'gbLanguage
        '
        Me.gbLanguage.Controls.Add(Me.lblTVLanguagePreferred)
        Me.gbLanguage.Controls.Add(Me.btnTVLanguageFetch)
        Me.gbLanguage.Controls.Add(Me.cbTVLanguage)
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
        'btnTVLanguageFetch
        '
        Me.btnTVLanguageFetch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTVLanguageFetch.Location = New System.Drawing.Point(12, 68)
        Me.btnTVLanguageFetch.Name = "btnTVLanguageFetch"
        Me.btnTVLanguageFetch.Size = New System.Drawing.Size(166, 23)
        Me.btnTVLanguageFetch.TabIndex = 2
        Me.btnTVLanguageFetch.Text = "Fetch Available Languages"
        Me.btnTVLanguageFetch.UseVisualStyleBackColor = True
        '
        'cbTVLanguage
        '
        Me.cbTVLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbTVLanguage.Location = New System.Drawing.Point(12, 39)
        Me.cbTVLanguage.Name = "cbTVLanguage"
        Me.cbTVLanguage.Size = New System.Drawing.Size(166, 21)
        Me.cbTVLanguage.TabIndex = 1
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
        'GroupBox32
        '
        Me.GroupBox32.Controls.Add(Me.chkGetEnglishImages)
        Me.GroupBox32.Controls.Add(Me.chkOnlyTVImagesLanguage)
        Me.GroupBox32.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox32.Location = New System.Drawing.Point(10, 156)
        Me.GroupBox32.Name = "GroupBox32"
        Me.GroupBox32.Size = New System.Drawing.Size(403, 114)
        Me.GroupBox32.TabIndex = 77
        Me.GroupBox32.TabStop = False
        Me.GroupBox32.Text = "Scraper Fields"
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
        Me.GroupBox30.ResumeLayout(False)
        Me.GroupBox30.PerformLayout()
        CType(Me.pbTVDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbLanguage.ResumeLayout(False)
        Me.gbLanguage.PerformLayout()
        Me.GroupBox32.ResumeLayout(False)
        Me.GroupBox32.PerformLayout()
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
    Friend WithEvents GroupBox30 As System.Windows.Forms.GroupBox
    Friend WithEvents pbTVDB As System.Windows.Forms.PictureBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtTVDBApiKey As System.Windows.Forms.TextBox
    Friend WithEvents lblTVDBMirror As System.Windows.Forms.Label
    Friend WithEvents txtTVDBMirror As System.Windows.Forms.TextBox
    Friend WithEvents gbLanguage As System.Windows.Forms.GroupBox
    Friend WithEvents lblTVLanguagePreferred As System.Windows.Forms.Label
    Friend WithEvents btnTVLanguageFetch As System.Windows.Forms.Button
    Friend WithEvents cbTVLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents chkGetEnglishImages As System.Windows.Forms.CheckBox
    Friend WithEvents chkOnlyTVImagesLanguage As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox32 As System.Windows.Forms.GroupBox

End Class
