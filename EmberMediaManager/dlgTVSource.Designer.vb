<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgTVSource
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
        Me.pnlTVSource = New System.Windows.Forms.Panel()
        Me.lblSourceEpisodeSorting = New System.Windows.Forms.Label()
        Me.cbSourceEpisodeSorting = New System.Windows.Forms.ComboBox()
        Me.gbSourceOptions = New System.Windows.Forms.GroupBox()
        Me.chkExclude = New System.Windows.Forms.CheckBox()
        Me.lblSourceLanguage = New System.Windows.Forms.Label()
        Me.lblSourceOrdering = New System.Windows.Forms.Label()
        Me.cbSourceOrdering = New System.Windows.Forms.ComboBox()
        Me.cbSourceLanguage = New System.Windows.Forms.ComboBox()
        CType(Me.pbValid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTVSource.SuspendLayout()
        Me.gbSourceOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Enabled = False
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.OK_Button.Location = New System.Drawing.Point(283, 195)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Cancel_Button.Location = New System.Drawing.Point(356, 195)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'txtSourceName
        '
        Me.txtSourceName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSourceName.Location = New System.Drawing.Point(11, 25)
        Me.txtSourceName.Name = "txtSourceName"
        Me.txtSourceName.Size = New System.Drawing.Size(130, 22)
        Me.txtSourceName.TabIndex = 4
        '
        'lblSourceName
        '
        Me.lblSourceName.AutoSize = True
        Me.lblSourceName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSourceName.Location = New System.Drawing.Point(9, 10)
        Me.lblSourceName.Name = "lblSourceName"
        Me.lblSourceName.Size = New System.Drawing.Size(79, 13)
        Me.lblSourceName.TabIndex = 3
        Me.lblSourceName.Text = "Source Name:"
        '
        'lblSourcePath
        '
        Me.lblSourcePath.AutoSize = True
        Me.lblSourcePath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSourcePath.Location = New System.Drawing.Point(9, 55)
        Me.lblSourcePath.Name = "lblSourcePath"
        Me.lblSourcePath.Size = New System.Drawing.Size(72, 13)
        Me.lblSourcePath.TabIndex = 0
        Me.lblSourcePath.Text = "Source Path:"
        '
        'txtSourcePath
        '
        Me.txtSourcePath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSourcePath.Location = New System.Drawing.Point(11, 70)
        Me.txtSourcePath.Name = "txtSourcePath"
        Me.txtSourcePath.Size = New System.Drawing.Size(376, 22)
        Me.txtSourcePath.TabIndex = 1
        '
        'btnBrowse
        '
        Me.btnBrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(393, 69)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(26, 23)
        Me.btnBrowse.TabIndex = 2
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'pbValid
        '
        Me.pbValid.Image = Global.Ember_Media_Manager.My.Resources.Resources.invalid
        Me.pbValid.Location = New System.Drawing.Point(147, 27)
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
        'pnlTVSource
        '
        Me.pnlTVSource.BackColor = System.Drawing.Color.White
        Me.pnlTVSource.Controls.Add(Me.lblSourceEpisodeSorting)
        Me.pnlTVSource.Controls.Add(Me.cbSourceEpisodeSorting)
        Me.pnlTVSource.Controls.Add(Me.gbSourceOptions)
        Me.pnlTVSource.Controls.Add(Me.lblSourceLanguage)
        Me.pnlTVSource.Controls.Add(Me.lblSourceOrdering)
        Me.pnlTVSource.Controls.Add(Me.cbSourceOrdering)
        Me.pnlTVSource.Controls.Add(Me.cbSourceLanguage)
        Me.pnlTVSource.Controls.Add(Me.pbValid)
        Me.pnlTVSource.Controls.Add(Me.btnBrowse)
        Me.pnlTVSource.Controls.Add(Me.lblSourcePath)
        Me.pnlTVSource.Controls.Add(Me.txtSourcePath)
        Me.pnlTVSource.Controls.Add(Me.lblSourceName)
        Me.pnlTVSource.Controls.Add(Me.txtSourceName)
        Me.pnlTVSource.Location = New System.Drawing.Point(4, 4)
        Me.pnlTVSource.Name = "pnlTVSource"
        Me.pnlTVSource.Size = New System.Drawing.Size(427, 185)
        Me.pnlTVSource.TabIndex = 2
        '
        'lblSourceEpisodeSorting
        '
        Me.lblSourceEpisodeSorting.AutoSize = True
        Me.lblSourceEpisodeSorting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblSourceEpisodeSorting.Location = New System.Drawing.Point(9, 154)
        Me.lblSourceEpisodeSorting.Name = "lblSourceEpisodeSorting"
        Me.lblSourceEpisodeSorting.Size = New System.Drawing.Size(104, 13)
        Me.lblSourceEpisodeSorting.TabIndex = 14
        Me.lblSourceEpisodeSorting.Text = "Show Episodes by:"
        Me.lblSourceEpisodeSorting.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cbSourceEpisodeSorting
        '
        Me.cbSourceEpisodeSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSourceEpisodeSorting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSourceEpisodeSorting.FormattingEnabled = True
        Me.cbSourceEpisodeSorting.Location = New System.Drawing.Point(215, 151)
        Me.cbSourceEpisodeSorting.Name = "cbSourceEpisodeSorting"
        Me.cbSourceEpisodeSorting.Size = New System.Drawing.Size(172, 21)
        Me.cbSourceEpisodeSorting.TabIndex = 15
        '
        'gbSourceOptions
        '
        Me.gbSourceOptions.Controls.Add(Me.chkExclude)
        Me.gbSourceOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbSourceOptions.Location = New System.Drawing.Point(169, 8)
        Me.gbSourceOptions.Name = "gbSourceOptions"
        Me.gbSourceOptions.Size = New System.Drawing.Size(250, 53)
        Me.gbSourceOptions.TabIndex = 13
        Me.gbSourceOptions.TabStop = False
        Me.gbSourceOptions.Text = "Source Options"
        '
        'chkExclude
        '
        Me.chkExclude.AutoSize = True
        Me.chkExclude.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkExclude.Location = New System.Drawing.Point(6, 21)
        Me.chkExclude.Name = "chkExclude"
        Me.chkExclude.Size = New System.Drawing.Size(199, 17)
        Me.chkExclude.TabIndex = 3
        Me.chkExclude.Text = "Exclude path from library updates"
        Me.chkExclude.UseVisualStyleBackColor = True
        '
        'lblSourceLanguage
        '
        Me.lblSourceLanguage.AutoSize = True
        Me.lblSourceLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblSourceLanguage.Location = New System.Drawing.Point(9, 101)
        Me.lblSourceLanguage.Name = "lblSourceLanguage"
        Me.lblSourceLanguage.Size = New System.Drawing.Size(103, 13)
        Me.lblSourceLanguage.TabIndex = 12
        Me.lblSourceLanguage.Text = "Default Language:"
        Me.lblSourceLanguage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblSourceOrdering
        '
        Me.lblSourceOrdering.AutoSize = True
        Me.lblSourceOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblSourceOrdering.Location = New System.Drawing.Point(9, 127)
        Me.lblSourceOrdering.Name = "lblSourceOrdering"
        Me.lblSourceOrdering.Size = New System.Drawing.Size(141, 13)
        Me.lblSourceOrdering.TabIndex = 10
        Me.lblSourceOrdering.Text = "Default Episode Ordering:"
        Me.lblSourceOrdering.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cbSourceOrdering
        '
        Me.cbSourceOrdering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSourceOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSourceOrdering.FormattingEnabled = True
        Me.cbSourceOrdering.Location = New System.Drawing.Point(215, 124)
        Me.cbSourceOrdering.Name = "cbSourceOrdering"
        Me.cbSourceOrdering.Size = New System.Drawing.Size(172, 21)
        Me.cbSourceOrdering.TabIndex = 11
        '
        'cbSourceLanguage
        '
        Me.cbSourceLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSourceLanguage.Location = New System.Drawing.Point(215, 98)
        Me.cbSourceLanguage.Name = "cbSourceLanguage"
        Me.cbSourceLanguage.Size = New System.Drawing.Size(172, 21)
        Me.cbSourceLanguage.TabIndex = 9
        '
        'dlgTVSource
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 224)
        Me.Controls.Add(Me.pnlTVSource)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgTVSource"
        Me.ShowInTaskbar = False
        Me.Text = "TV Source"
        Me.TopMost = True
        CType(Me.pbValid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTVSource.ResumeLayout(False)
        Me.pnlTVSource.PerformLayout()
        Me.gbSourceOptions.ResumeLayout(False)
        Me.gbSourceOptions.PerformLayout()
        Me.ResumeLayout(False)

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
    Friend WithEvents pnlTVSource As System.Windows.Forms.Panel
    Friend WithEvents cbSourceLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents lblSourceOrdering As System.Windows.Forms.Label
    Friend WithEvents cbSourceOrdering As System.Windows.Forms.ComboBox
    Friend WithEvents lblSourceLanguage As System.Windows.Forms.Label
    Friend WithEvents gbSourceOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkExclude As System.Windows.Forms.CheckBox
    Friend WithEvents lblSourceEpisodeSorting As System.Windows.Forms.Label
    Friend WithEvents cbSourceEpisodeSorting As System.Windows.Forms.ComboBox

End Class
