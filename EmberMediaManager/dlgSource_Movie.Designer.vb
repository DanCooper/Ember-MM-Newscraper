<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgSource_Movie
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
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtSourceName = New System.Windows.Forms.TextBox()
        Me.lblSourceName = New System.Windows.Forms.Label()
        Me.lblSourcePath = New System.Windows.Forms.Label()
        Me.txtSourcePath = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.gbSourceOptions = New System.Windows.Forms.GroupBox()
        Me.tblSourceOptions = New System.Windows.Forms.TableLayoutPanel()
        Me.chkScanRecursive = New System.Windows.Forms.CheckBox()
        Me.cbSourceLanguage = New System.Windows.Forms.ComboBox()
        Me.lblSourceLanguage = New System.Windows.Forms.Label()
        Me.chkExclude = New System.Windows.Forms.CheckBox()
        Me.chkGetYear = New System.Windows.Forms.CheckBox()
        Me.chkIsSingle = New System.Windows.Forms.CheckBox()
        Me.chkUseFolderName = New System.Windows.Forms.CheckBox()
        Me.pbValidSourceName = New System.Windows.Forms.PictureBox()
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.tmrWait = New System.Windows.Forms.Timer(Me.components)
        Me.tmrName = New System.Windows.Forms.Timer(Me.components)
        Me.tmrPathWait = New System.Windows.Forms.Timer(Me.components)
        Me.tmrPath = New System.Windows.Forms.Timer(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.pbValidSourcePath = New System.Windows.Forms.PictureBox()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.gbSourceOptions.SuspendLayout()
        Me.tblSourceOptions.SuspendLayout()
        CType(Me.pbValidSourceName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.tblMain.SuspendLayout()
        CType(Me.pbValidSourcePath, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Enabled = False
        Me.btnOK.Location = New System.Drawing.Point(318, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(67, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(391, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(67, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'txtSourceName
        '
        Me.txtSourceName.Location = New System.Drawing.Point(6, 26)
        Me.txtSourceName.Name = "txtSourceName"
        Me.txtSourceName.Size = New System.Drawing.Size(376, 22)
        Me.txtSourceName.TabIndex = 0
        '
        'lblSourceName
        '
        Me.lblSourceName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSourceName.AutoSize = True
        Me.lblSourceName.Location = New System.Drawing.Point(6, 6)
        Me.lblSourceName.Name = "lblSourceName"
        Me.lblSourceName.Size = New System.Drawing.Size(77, 13)
        Me.lblSourceName.TabIndex = 0
        Me.lblSourceName.Text = "Source Name:"
        '
        'lblSourcePath
        '
        Me.lblSourcePath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSourcePath.AutoSize = True
        Me.lblSourcePath.Location = New System.Drawing.Point(6, 54)
        Me.lblSourcePath.Name = "lblSourcePath"
        Me.lblSourcePath.Size = New System.Drawing.Size(71, 13)
        Me.lblSourcePath.TabIndex = 2
        Me.lblSourcePath.Text = "Source Path:"
        '
        'txtSourcePath
        '
        Me.txtSourcePath.Location = New System.Drawing.Point(6, 74)
        Me.txtSourcePath.Name = "txtSourcePath"
        Me.txtSourcePath.Size = New System.Drawing.Size(376, 22)
        Me.txtSourcePath.TabIndex = 1
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(410, 74)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(26, 23)
        Me.btnBrowse.TabIndex = 2
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'gbSourceOptions
        '
        Me.gbSourceOptions.AutoSize = True
        Me.tblMain.SetColumnSpan(Me.gbSourceOptions, 3)
        Me.gbSourceOptions.Controls.Add(Me.tblSourceOptions)
        Me.gbSourceOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSourceOptions.Location = New System.Drawing.Point(6, 103)
        Me.gbSourceOptions.Name = "gbSourceOptions"
        Me.gbSourceOptions.Size = New System.Drawing.Size(430, 163)
        Me.gbSourceOptions.TabIndex = 3
        Me.gbSourceOptions.TabStop = False
        Me.gbSourceOptions.Text = "Source Options"
        '
        'tblSourceOptions
        '
        Me.tblSourceOptions.AutoSize = True
        Me.tblSourceOptions.ColumnCount = 2
        Me.tblSourceOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSourceOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSourceOptions.Controls.Add(Me.chkScanRecursive, 0, 0)
        Me.tblSourceOptions.Controls.Add(Me.cbSourceLanguage, 1, 5)
        Me.tblSourceOptions.Controls.Add(Me.lblSourceLanguage, 0, 5)
        Me.tblSourceOptions.Controls.Add(Me.chkExclude, 0, 4)
        Me.tblSourceOptions.Controls.Add(Me.chkGetYear, 0, 3)
        Me.tblSourceOptions.Controls.Add(Me.chkIsSingle, 0, 1)
        Me.tblSourceOptions.Controls.Add(Me.chkUseFolderName, 0, 2)
        Me.tblSourceOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSourceOptions.Location = New System.Drawing.Point(3, 18)
        Me.tblSourceOptions.Name = "tblSourceOptions"
        Me.tblSourceOptions.RowCount = 7
        Me.tblSourceOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourceOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourceOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourceOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourceOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourceOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourceOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourceOptions.Size = New System.Drawing.Size(424, 142)
        Me.tblSourceOptions.TabIndex = 5
        '
        'chkScanRecursive
        '
        Me.chkScanRecursive.AutoSize = True
        Me.chkScanRecursive.Checked = True
        Me.chkScanRecursive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tblSourceOptions.SetColumnSpan(Me.chkScanRecursive, 2)
        Me.chkScanRecursive.Location = New System.Drawing.Point(3, 3)
        Me.chkScanRecursive.Name = "chkScanRecursive"
        Me.chkScanRecursive.Size = New System.Drawing.Size(109, 17)
        Me.chkScanRecursive.TabIndex = 0
        Me.chkScanRecursive.Text = "Scan Recursively"
        Me.chkScanRecursive.UseVisualStyleBackColor = True
        '
        'cbSourceLanguage
        '
        Me.cbSourceLanguage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbSourceLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSourceLanguage.Location = New System.Drawing.Point(111, 118)
        Me.cbSourceLanguage.Name = "cbSourceLanguage"
        Me.cbSourceLanguage.Size = New System.Drawing.Size(310, 21)
        Me.cbSourceLanguage.TabIndex = 5
        '
        'lblSourceLanguage
        '
        Me.lblSourceLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSourceLanguage.AutoSize = True
        Me.lblSourceLanguage.Location = New System.Drawing.Point(3, 122)
        Me.lblSourceLanguage.Name = "lblSourceLanguage"
        Me.lblSourceLanguage.Size = New System.Drawing.Size(102, 13)
        Me.lblSourceLanguage.TabIndex = 14
        Me.lblSourceLanguage.Text = "Default Language:"
        Me.lblSourceLanguage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'chkExclude
        '
        Me.chkExclude.AutoSize = True
        Me.tblSourceOptions.SetColumnSpan(Me.chkExclude, 2)
        Me.chkExclude.Location = New System.Drawing.Point(3, 95)
        Me.chkExclude.Name = "chkExclude"
        Me.chkExclude.Size = New System.Drawing.Size(199, 17)
        Me.chkExclude.TabIndex = 4
        Me.chkExclude.Text = "Exclude path from library updates"
        Me.chkExclude.UseVisualStyleBackColor = True
        '
        'chkGetYear
        '
        Me.chkGetYear.AutoSize = True
        Me.chkGetYear.Checked = True
        Me.chkGetYear.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tblSourceOptions.SetColumnSpan(Me.chkGetYear, 2)
        Me.chkGetYear.Location = New System.Drawing.Point(3, 72)
        Me.chkGetYear.Name = "chkGetYear"
        Me.chkGetYear.Size = New System.Drawing.Size(160, 17)
        Me.chkGetYear.TabIndex = 3
        Me.chkGetYear.Text = "Get year from folder name"
        Me.chkGetYear.UseVisualStyleBackColor = True
        '
        'chkIsSingle
        '
        Me.chkIsSingle.AutoSize = True
        Me.chkIsSingle.Checked = True
        Me.chkIsSingle.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tblSourceOptions.SetColumnSpan(Me.chkIsSingle, 2)
        Me.chkIsSingle.Location = New System.Drawing.Point(3, 26)
        Me.chkIsSingle.Name = "chkIsSingle"
        Me.chkIsSingle.Size = New System.Drawing.Size(180, 17)
        Me.chkIsSingle.TabIndex = 1
        Me.chkIsSingle.Text = "Movies are in separate folders"
        Me.chkIsSingle.UseVisualStyleBackColor = True
        '
        'chkUseFolderName
        '
        Me.chkUseFolderName.AutoSize = True
        Me.chkUseFolderName.Checked = True
        Me.chkUseFolderName.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tblSourceOptions.SetColumnSpan(Me.chkUseFolderName, 2)
        Me.chkUseFolderName.Location = New System.Drawing.Point(3, 49)
        Me.chkUseFolderName.Name = "chkUseFolderName"
        Me.chkUseFolderName.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkUseFolderName.Size = New System.Drawing.Size(220, 17)
        Me.chkUseFolderName.TabIndex = 2
        Me.chkUseFolderName.Text = "Use Folder Name for Initial Listing"
        Me.chkUseFolderName.UseVisualStyleBackColor = True
        '
        'pbValidSourceName
        '
        Me.pbValidSourceName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.pbValidSourceName.Image = Global.Ember_Media_Manager.My.Resources.Resources.invalid
        Me.pbValidSourceName.Location = New System.Drawing.Point(388, 29)
        Me.pbValidSourceName.Name = "pbValidSourceName"
        Me.pbValidSourceName.Size = New System.Drawing.Size(16, 16)
        Me.pbValidSourceName.TabIndex = 7
        Me.pbValidSourceName.TabStop = False
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
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(461, 291)
        Me.pnlMain.TabIndex = 2
        '
        'tblMain
        '
        Me.tblMain.AutoSize = True
        Me.tblMain.ColumnCount = 4
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.Controls.Add(Me.lblSourceName, 0, 0)
        Me.tblMain.Controls.Add(Me.gbSourceOptions, 0, 4)
        Me.tblMain.Controls.Add(Me.txtSourceName, 0, 1)
        Me.tblMain.Controls.Add(Me.lblSourcePath, 0, 2)
        Me.tblMain.Controls.Add(Me.btnBrowse, 2, 3)
        Me.tblMain.Controls.Add(Me.txtSourcePath, 0, 3)
        Me.tblMain.Controls.Add(Me.pbValidSourcePath, 1, 3)
        Me.tblMain.Controls.Add(Me.pbValidSourceName, 1, 1)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.Padding = New System.Windows.Forms.Padding(3)
        Me.tblMain.RowCount = 6
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.Size = New System.Drawing.Size(461, 291)
        Me.tblMain.TabIndex = 0
        '
        'pbValidSourcePath
        '
        Me.pbValidSourcePath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.pbValidSourcePath.Image = Global.Ember_Media_Manager.My.Resources.Resources.invalid
        Me.pbValidSourcePath.Location = New System.Drawing.Point(388, 77)
        Me.pbValidSourcePath.Name = "pbValidSourcePath"
        Me.pbValidSourcePath.Size = New System.Drawing.Size(16, 16)
        Me.pbValidSourcePath.TabIndex = 7
        Me.pbValidSourcePath.TabStop = False
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 291)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(461, 29)
        Me.pnlBottom.TabIndex = 4
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 3
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnCancel, 2, 0)
        Me.tblBottom.Controls.Add(Me.btnOK, 1, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.Size = New System.Drawing.Size(461, 29)
        Me.tblBottom.TabIndex = 0
        '
        'dlgSource_Movie
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(461, 320)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgSource_Movie"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Movie Source"
        Me.gbSourceOptions.ResumeLayout(False)
        Me.gbSourceOptions.PerformLayout()
        Me.tblSourceOptions.ResumeLayout(False)
        Me.tblSourceOptions.PerformLayout()
        CType(Me.pbValidSourceName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.tblMain.ResumeLayout(False)
        Me.tblMain.PerformLayout()
        CType(Me.pbValidSourcePath, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtSourceName As System.Windows.Forms.TextBox
    Friend WithEvents lblSourceName As System.Windows.Forms.Label
    Friend WithEvents lblSourcePath As System.Windows.Forms.Label
    Friend WithEvents txtSourcePath As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents gbSourceOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkUseFolderName As System.Windows.Forms.CheckBox
    Friend WithEvents chkScanRecursive As System.Windows.Forms.CheckBox
    Friend WithEvents chkIsSingle As System.Windows.Forms.CheckBox
    Friend WithEvents pbValidSourceName As System.Windows.Forms.PictureBox
    Friend WithEvents fbdBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents tmrWait As System.Windows.Forms.Timer
    Friend WithEvents tmrName As System.Windows.Forms.Timer
    Friend WithEvents tmrPathWait As System.Windows.Forms.Timer
    Friend WithEvents tmrPath As System.Windows.Forms.Timer
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents chkExclude As System.Windows.Forms.CheckBox
    Friend WithEvents chkGetYear As System.Windows.Forms.CheckBox
    Friend WithEvents lblSourceLanguage As System.Windows.Forms.Label
    Friend WithEvents cbSourceLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents tblBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblSourceOptions As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pbValidSourcePath As PictureBox
End Class
