<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgSourceTVShow
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
        Me.pbValid = New System.Windows.Forms.PictureBox()
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.tmrWait = New System.Windows.Forms.Timer(Me.components)
        Me.tmrName = New System.Windows.Forms.Timer(Me.components)
        Me.tmrPathWait = New System.Windows.Forms.Timer(Me.components)
        Me.tmrPath = New System.Windows.Forms.Timer(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbSourceOptions = New System.Windows.Forms.GroupBox()
        Me.tblSourceOptions = New System.Windows.Forms.TableLayoutPanel()
        Me.chkExclude = New System.Windows.Forms.CheckBox()
        Me.cbSourceEpisodeSorting = New System.Windows.Forms.ComboBox()
        Me.lblSourceLanguage = New System.Windows.Forms.Label()
        Me.lblSourceEpisodeSorting = New System.Windows.Forms.Label()
        Me.lblSourceOrdering = New System.Windows.Forms.Label()
        Me.cbSourceOrdering = New System.Windows.Forms.ComboBox()
        Me.cbSourceLanguage = New System.Windows.Forms.ComboBox()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.chkSingle = New System.Windows.Forms.CheckBox()
        CType(Me.pbValid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.tblMain.SuspendLayout()
        Me.gbSourceOptions.SuspendLayout()
        Me.tblSourceOptions.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Enabled = False
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.OK_Button.Location = New System.Drawing.Point(285, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 7
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Cancel_Button.Location = New System.Drawing.Point(358, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 8
        Me.Cancel_Button.Text = "Cancel"
        '
        'txtSourceName
        '
        Me.txtSourceName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSourceName.Location = New System.Drawing.Point(6, 26)
        Me.txtSourceName.Name = "txtSourceName"
        Me.txtSourceName.Size = New System.Drawing.Size(130, 22)
        Me.txtSourceName.TabIndex = 0
        '
        'lblSourceName
        '
        Me.lblSourceName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSourceName.AutoSize = True
        Me.lblSourceName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSourceName.Location = New System.Drawing.Point(6, 6)
        Me.lblSourceName.Name = "lblSourceName"
        Me.lblSourceName.Size = New System.Drawing.Size(79, 13)
        Me.lblSourceName.TabIndex = 3
        Me.lblSourceName.Text = "Source Name:"
        '
        'lblSourcePath
        '
        Me.lblSourcePath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSourcePath.AutoSize = True
        Me.lblSourcePath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSourcePath.Location = New System.Drawing.Point(6, 54)
        Me.lblSourcePath.Name = "lblSourcePath"
        Me.lblSourcePath.Size = New System.Drawing.Size(72, 13)
        Me.lblSourcePath.TabIndex = 0
        Me.lblSourcePath.Text = "Source Path:"
        '
        'txtSourcePath
        '
        Me.tblMain.SetColumnSpan(Me.txtSourcePath, 2)
        Me.txtSourcePath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSourcePath.Location = New System.Drawing.Point(6, 74)
        Me.txtSourcePath.Name = "txtSourcePath"
        Me.txtSourcePath.Size = New System.Drawing.Size(376, 22)
        Me.txtSourcePath.TabIndex = 1
        '
        'btnBrowse
        '
        Me.btnBrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(388, 74)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(26, 23)
        Me.btnBrowse.TabIndex = 2
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'pbValid
        '
        Me.pbValid.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.pbValid.Image = Global.Ember_Media_Manager.My.Resources.Resources.invalid
        Me.pbValid.Location = New System.Drawing.Point(142, 29)
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
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(428, 267)
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
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.Controls.Add(Me.lblSourceName, 0, 0)
        Me.tblMain.Controls.Add(Me.txtSourceName, 0, 1)
        Me.tblMain.Controls.Add(Me.pbValid, 1, 1)
        Me.tblMain.Controls.Add(Me.lblSourcePath, 0, 2)
        Me.tblMain.Controls.Add(Me.txtSourcePath, 0, 3)
        Me.tblMain.Controls.Add(Me.btnBrowse, 2, 3)
        Me.tblMain.Controls.Add(Me.gbSourceOptions, 0, 4)
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
        Me.tblMain.Size = New System.Drawing.Size(428, 267)
        Me.tblMain.TabIndex = 16
        '
        'gbSourceOptions
        '
        Me.gbSourceOptions.AutoSize = True
        Me.tblMain.SetColumnSpan(Me.gbSourceOptions, 3)
        Me.gbSourceOptions.Controls.Add(Me.tblSourceOptions)
        Me.gbSourceOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSourceOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbSourceOptions.Location = New System.Drawing.Point(6, 103)
        Me.gbSourceOptions.Name = "gbSourceOptions"
        Me.gbSourceOptions.Size = New System.Drawing.Size(408, 148)
        Me.gbSourceOptions.TabIndex = 13
        Me.gbSourceOptions.TabStop = False
        Me.gbSourceOptions.Text = "Source Options"
        '
        'tblSourceOptions
        '
        Me.tblSourceOptions.AutoSize = True
        Me.tblSourceOptions.ColumnCount = 3
        Me.tblSourceOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSourceOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSourceOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSourceOptions.Controls.Add(Me.chkExclude, 0, 1)
        Me.tblSourceOptions.Controls.Add(Me.cbSourceEpisodeSorting, 1, 4)
        Me.tblSourceOptions.Controls.Add(Me.lblSourceLanguage, 0, 2)
        Me.tblSourceOptions.Controls.Add(Me.lblSourceEpisodeSorting, 0, 4)
        Me.tblSourceOptions.Controls.Add(Me.lblSourceOrdering, 0, 3)
        Me.tblSourceOptions.Controls.Add(Me.cbSourceOrdering, 1, 3)
        Me.tblSourceOptions.Controls.Add(Me.cbSourceLanguage, 1, 2)
        Me.tblSourceOptions.Controls.Add(Me.chkSingle, 0, 0)
        Me.tblSourceOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSourceOptions.Location = New System.Drawing.Point(3, 18)
        Me.tblSourceOptions.Name = "tblSourceOptions"
        Me.tblSourceOptions.RowCount = 6
        Me.tblSourceOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourceOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourceOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourceOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourceOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourceOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourceOptions.Size = New System.Drawing.Size(402, 127)
        Me.tblSourceOptions.TabIndex = 4
        '
        'chkExclude
        '
        Me.chkExclude.AutoSize = True
        Me.tblSourceOptions.SetColumnSpan(Me.chkExclude, 2)
        Me.chkExclude.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkExclude.Location = New System.Drawing.Point(3, 26)
        Me.chkExclude.Name = "chkExclude"
        Me.chkExclude.Size = New System.Drawing.Size(199, 17)
        Me.chkExclude.TabIndex = 3
        Me.chkExclude.Text = "Exclude path from library updates"
        Me.chkExclude.UseVisualStyleBackColor = True
        '
        'cbSourceEpisodeSorting
        '
        Me.cbSourceEpisodeSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSourceEpisodeSorting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSourceEpisodeSorting.FormattingEnabled = True
        Me.cbSourceEpisodeSorting.Location = New System.Drawing.Point(151, 103)
        Me.cbSourceEpisodeSorting.Name = "cbSourceEpisodeSorting"
        Me.cbSourceEpisodeSorting.Size = New System.Drawing.Size(172, 21)
        Me.cbSourceEpisodeSorting.TabIndex = 6
        '
        'lblSourceLanguage
        '
        Me.lblSourceLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSourceLanguage.AutoSize = True
        Me.lblSourceLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSourceLanguage.Location = New System.Drawing.Point(3, 53)
        Me.lblSourceLanguage.Name = "lblSourceLanguage"
        Me.lblSourceLanguage.Size = New System.Drawing.Size(102, 13)
        Me.lblSourceLanguage.TabIndex = 12
        Me.lblSourceLanguage.Text = "Default Language:"
        Me.lblSourceLanguage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblSourceEpisodeSorting
        '
        Me.lblSourceEpisodeSorting.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSourceEpisodeSorting.AutoSize = True
        Me.lblSourceEpisodeSorting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSourceEpisodeSorting.Location = New System.Drawing.Point(3, 107)
        Me.lblSourceEpisodeSorting.Name = "lblSourceEpisodeSorting"
        Me.lblSourceEpisodeSorting.Size = New System.Drawing.Size(103, 13)
        Me.lblSourceEpisodeSorting.TabIndex = 14
        Me.lblSourceEpisodeSorting.Text = "Show Episodes by:"
        Me.lblSourceEpisodeSorting.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblSourceOrdering
        '
        Me.lblSourceOrdering.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSourceOrdering.AutoSize = True
        Me.lblSourceOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSourceOrdering.Location = New System.Drawing.Point(3, 80)
        Me.lblSourceOrdering.Name = "lblSourceOrdering"
        Me.lblSourceOrdering.Size = New System.Drawing.Size(142, 13)
        Me.lblSourceOrdering.TabIndex = 10
        Me.lblSourceOrdering.Text = "Default Episode Ordering:"
        Me.lblSourceOrdering.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cbSourceOrdering
        '
        Me.cbSourceOrdering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSourceOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSourceOrdering.FormattingEnabled = True
        Me.cbSourceOrdering.Location = New System.Drawing.Point(151, 76)
        Me.cbSourceOrdering.Name = "cbSourceOrdering"
        Me.cbSourceOrdering.Size = New System.Drawing.Size(172, 21)
        Me.cbSourceOrdering.TabIndex = 5
        '
        'cbSourceLanguage
        '
        Me.cbSourceLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSourceLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSourceLanguage.Location = New System.Drawing.Point(151, 49)
        Me.cbSourceLanguage.Name = "cbSourceLanguage"
        Me.cbSourceLanguage.Size = New System.Drawing.Size(172, 21)
        Me.cbSourceLanguage.TabIndex = 4
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 267)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(428, 29)
        Me.pnlBottom.TabIndex = 3
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 3
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.Cancel_Button, 2, 0)
        Me.tblBottom.Controls.Add(Me.OK_Button, 1, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblBottom.Size = New System.Drawing.Size(428, 29)
        Me.tblBottom.TabIndex = 0
        '
        'chkSingle
        '
        Me.chkSingle.AutoSize = True
        Me.tblSourceOptions.SetColumnSpan(Me.chkSingle, 2)
        Me.chkSingle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkSingle.Location = New System.Drawing.Point(3, 3)
        Me.chkSingle.Name = "chkSingle"
        Me.chkSingle.Size = New System.Drawing.Size(240, 17)
        Me.chkSingle.TabIndex = 15
        Me.chkSingle.Text = "Selected folder contains a single TV Show"
        Me.chkSingle.UseVisualStyleBackColor = True
        '
        'dlgSourceTVShow
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(428, 296)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgSourceTVShow"
        Me.ShowInTaskbar = False
        Me.Text = "TV Source"
        CType(Me.pbValid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.tblMain.ResumeLayout(False)
        Me.tblMain.PerformLayout()
        Me.gbSourceOptions.ResumeLayout(False)
        Me.gbSourceOptions.PerformLayout()
        Me.tblSourceOptions.ResumeLayout(False)
        Me.tblSourceOptions.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents txtSourceName As System.Windows.Forms.TextBox
    Friend WithEvents lblSourceName As System.Windows.Forms.Label
    Friend WithEvents lblSourcePath As System.Windows.Forms.Label
    Friend WithEvents txtSourcePath As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents pbValid As System.Windows.Forms.PictureBox
    Friend WithEvents fbdBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents tmrWait As System.Windows.Forms.Timer
    Friend WithEvents tmrName As System.Windows.Forms.Timer
    Friend WithEvents tmrPathWait As System.Windows.Forms.Timer
    Friend WithEvents tmrPath As System.Windows.Forms.Timer
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents cbSourceLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents lblSourceOrdering As System.Windows.Forms.Label
    Friend WithEvents cbSourceOrdering As System.Windows.Forms.ComboBox
    Friend WithEvents lblSourceLanguage As System.Windows.Forms.Label
    Friend WithEvents gbSourceOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkExclude As System.Windows.Forms.CheckBox
    Friend WithEvents lblSourceEpisodeSorting As System.Windows.Forms.Label
    Friend WithEvents cbSourceEpisodeSorting As System.Windows.Forms.ComboBox
    Friend WithEvents tblMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblSourceOptions As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents tblBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkSingle As CheckBox
End Class
