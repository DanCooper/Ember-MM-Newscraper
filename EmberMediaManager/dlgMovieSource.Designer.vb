<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgMovieSource
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
        Me.components = New System.ComponentModel.Container()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.txtSourceName = New System.Windows.Forms.TextBox()
        Me.lblSourceName = New System.Windows.Forms.Label()
        Me.lblSourcePath = New System.Windows.Forms.Label()
        Me.txtSourcePath = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.gbSourceOptions = New System.Windows.Forms.GroupBox()
        Me.chkGetYear = New System.Windows.Forms.CheckBox()
        Me.chkExclude = New System.Windows.Forms.CheckBox()
        Me.chkSingle = New System.Windows.Forms.CheckBox()
        Me.chkUseFolderName = New System.Windows.Forms.CheckBox()
        Me.chkScanRecursive = New System.Windows.Forms.CheckBox()
        Me.pbValid = New System.Windows.Forms.PictureBox()
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.tmrWait = New System.Windows.Forms.Timer(Me.components)
        Me.tmrName = New System.Windows.Forms.Timer(Me.components)
        Me.tmrPathWait = New System.Windows.Forms.Timer(Me.components)
        Me.tmrPath = New System.Windows.Forms.Timer(Me.components)
        Me.lblHint = New System.Windows.Forms.Label()
        Me.pnlMovieSource = New System.Windows.Forms.Panel()
        Me.gbSourceOptions.SuspendLayout()
        CType(Me.pbValid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMovieSource.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Enabled = False
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.OK_Button.Location = New System.Drawing.Point(285, 191)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Cancel_Button.Location = New System.Drawing.Point(358, 191)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'txtSourceName
        '
        Me.txtSourceName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSourceName.Location = New System.Drawing.Point(12, 26)
        Me.txtSourceName.Name = "txtSourceName"
        Me.txtSourceName.Size = New System.Drawing.Size(130, 22)
        Me.txtSourceName.TabIndex = 1
        '
        'lblSourceName
        '
        Me.lblSourceName.AutoSize = True
        Me.lblSourceName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSourceName.Location = New System.Drawing.Point(10, 11)
        Me.lblSourceName.Name = "lblSourceName"
        Me.lblSourceName.Size = New System.Drawing.Size(79, 13)
        Me.lblSourceName.TabIndex = 0
        Me.lblSourceName.Text = "Source Name:"
        '
        'lblSourcePath
        '
        Me.lblSourcePath.AutoSize = True
        Me.lblSourcePath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSourcePath.Location = New System.Drawing.Point(11, 129)
        Me.lblSourcePath.Name = "lblSourcePath"
        Me.lblSourcePath.Size = New System.Drawing.Size(72, 13)
        Me.lblSourcePath.TabIndex = 2
        Me.lblSourcePath.Text = "Source Path:"
        '
        'txtSourcePath
        '
        Me.txtSourcePath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSourcePath.Location = New System.Drawing.Point(13, 144)
        Me.txtSourcePath.Name = "txtSourcePath"
        Me.txtSourcePath.Size = New System.Drawing.Size(376, 22)
        Me.txtSourcePath.TabIndex = 3
        '
        'btnBrowse
        '
        Me.btnBrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(398, 143)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(26, 23)
        Me.btnBrowse.TabIndex = 4
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'gbSourceOptions
        '
        Me.gbSourceOptions.Controls.Add(Me.chkGetYear)
        Me.gbSourceOptions.Controls.Add(Me.chkExclude)
        Me.gbSourceOptions.Controls.Add(Me.chkSingle)
        Me.gbSourceOptions.Controls.Add(Me.chkUseFolderName)
        Me.gbSourceOptions.Controls.Add(Me.chkScanRecursive)
        Me.gbSourceOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbSourceOptions.Location = New System.Drawing.Point(172, 5)
        Me.gbSourceOptions.Name = "gbSourceOptions"
        Me.gbSourceOptions.Size = New System.Drawing.Size(251, 132)
        Me.gbSourceOptions.TabIndex = 5
        Me.gbSourceOptions.TabStop = False
        Me.gbSourceOptions.Text = "Source Options"
        '
        'chkGetYear
        '
        Me.chkGetYear.AutoSize = True
        Me.chkGetYear.Checked = True
        Me.chkGetYear.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkGetYear.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkGetYear.Location = New System.Drawing.Point(6, 82)
        Me.chkGetYear.Name = "chkGetYear"
        Me.chkGetYear.Size = New System.Drawing.Size(160, 17)
        Me.chkGetYear.TabIndex = 4
        Me.chkGetYear.Text = "Get year from folder name"
        Me.chkGetYear.UseVisualStyleBackColor = True
        '
        'chkExclude
        '
        Me.chkExclude.AutoSize = True
        Me.chkExclude.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkExclude.Location = New System.Drawing.Point(6, 105)
        Me.chkExclude.Name = "chkExclude"
        Me.chkExclude.Size = New System.Drawing.Size(199, 17)
        Me.chkExclude.TabIndex = 3
        Me.chkExclude.Text = "Exclude path from library updates"
        Me.chkExclude.UseVisualStyleBackColor = True
        '
        'chkSingle
        '
        Me.chkSingle.AutoSize = True
        Me.chkSingle.Checked = True
        Me.chkSingle.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSingle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkSingle.Location = New System.Drawing.Point(6, 39)
        Me.chkSingle.Name = "chkSingle"
        Me.chkSingle.Size = New System.Drawing.Size(188, 17)
        Me.chkSingle.TabIndex = 1
        Me.chkSingle.Text = "Movies are in separate folders *"
        Me.chkSingle.UseVisualStyleBackColor = True
        '
        'chkUseFolderName
        '
        Me.chkUseFolderName.AutoSize = True
        Me.chkUseFolderName.Checked = True
        Me.chkUseFolderName.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUseFolderName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkUseFolderName.Location = New System.Drawing.Point(17, 59)
        Me.chkUseFolderName.Name = "chkUseFolderName"
        Me.chkUseFolderName.Size = New System.Drawing.Size(200, 17)
        Me.chkUseFolderName.TabIndex = 2
        Me.chkUseFolderName.Text = "Use Folder Name for Initial Listing"
        Me.chkUseFolderName.UseVisualStyleBackColor = True
        '
        'chkScanRecursive
        '
        Me.chkScanRecursive.AutoSize = True
        Me.chkScanRecursive.Checked = True
        Me.chkScanRecursive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkScanRecursive.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkScanRecursive.Location = New System.Drawing.Point(6, 19)
        Me.chkScanRecursive.Name = "chkScanRecursive"
        Me.chkScanRecursive.Size = New System.Drawing.Size(109, 17)
        Me.chkScanRecursive.TabIndex = 0
        Me.chkScanRecursive.Text = "Scan Recursively"
        Me.chkScanRecursive.UseVisualStyleBackColor = True
        '
        'pbValid
        '
        Me.pbValid.Image = Global.Ember_Media_Manager.My.Resources.Resources.invalid
        Me.pbValid.Location = New System.Drawing.Point(148, 28)
        Me.pbValid.Name = "pbValid"
        Me.pbValid.Size = New System.Drawing.Size(16, 16)
        Me.pbValid.TabIndex = 7
        Me.pbValid.TabStop = False
        '
        'fbdBrowse
        '
        Me.fbdBrowse.Description = "Select the parent folder for your movie folders/files."
        '
        'tmrWait
        '
        Me.tmrWait.Interval = 250
        '
        'tmrName
        '
        Me.tmrName.Interval = 250
        '
        'tmrPathWait
        '
        Me.tmrPathWait.Interval = 250
        '
        'tmrPath
        '
        Me.tmrPath.Interval = 250
        '
        'lblHint
        '
        Me.lblHint.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHint.Location = New System.Drawing.Point(-1, 191)
        Me.lblHint.Name = "lblHint"
        Me.lblHint.Size = New System.Drawing.Size(268, 24)
        Me.lblHint.TabIndex = 3
        Me.lblHint.Text = "* This MUST be enabled to use extrathumbs and file naming options like movie.nfo," & _
    " fanart.jpg, etc."
        Me.lblHint.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pnlMovieSource
        '
        Me.pnlMovieSource.BackColor = System.Drawing.Color.White
        Me.pnlMovieSource.Controls.Add(Me.pbValid)
        Me.pnlMovieSource.Controls.Add(Me.gbSourceOptions)
        Me.pnlMovieSource.Controls.Add(Me.btnBrowse)
        Me.pnlMovieSource.Controls.Add(Me.lblSourcePath)
        Me.pnlMovieSource.Controls.Add(Me.txtSourcePath)
        Me.pnlMovieSource.Controls.Add(Me.lblSourceName)
        Me.pnlMovieSource.Controls.Add(Me.txtSourceName)
        Me.pnlMovieSource.Location = New System.Drawing.Point(2, 3)
        Me.pnlMovieSource.Name = "pnlMovieSource"
        Me.pnlMovieSource.Size = New System.Drawing.Size(436, 182)
        Me.pnlMovieSource.TabIndex = 2
        '
        'dlgMovieSource
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(441, 221)
        Me.Controls.Add(Me.pnlMovieSource)
        Me.Controls.Add(Me.lblHint)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgMovieSource"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Movie Source"
        Me.gbSourceOptions.ResumeLayout(False)
        Me.gbSourceOptions.PerformLayout()
        CType(Me.pbValid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMovieSource.ResumeLayout(False)
        Me.pnlMovieSource.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents txtSourceName As System.Windows.Forms.TextBox
    Friend WithEvents lblSourceName As System.Windows.Forms.Label
    Friend WithEvents lblSourcePath As System.Windows.Forms.Label
    Friend WithEvents txtSourcePath As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents gbSourceOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkUseFolderName As System.Windows.Forms.CheckBox
    Friend WithEvents chkScanRecursive As System.Windows.Forms.CheckBox
    Friend WithEvents chkSingle As System.Windows.Forms.CheckBox
    Friend WithEvents pbValid As System.Windows.Forms.PictureBox
    Friend WithEvents fbdBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents tmrWait As System.Windows.Forms.Timer
    Friend WithEvents tmrName As System.Windows.Forms.Timer
    Friend WithEvents tmrPathWait As System.Windows.Forms.Timer
    Friend WithEvents tmrPath As System.Windows.Forms.Timer
    Friend WithEvents lblHint As System.Windows.Forms.Label
    Friend WithEvents pnlMovieSource As System.Windows.Forms.Panel
    Friend WithEvents chkExclude As System.Windows.Forms.CheckBox
    Friend WithEvents chkGetYear As System.Windows.Forms.CheckBox

End Class
