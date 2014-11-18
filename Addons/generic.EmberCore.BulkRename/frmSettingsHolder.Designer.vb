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
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.lblTips = New System.Windows.Forms.Label()
        Me.gbRenamerPatterns = New System.Windows.Forms.GroupBox()
        Me.chkRenameSingle = New System.Windows.Forms.CheckBox()
        Me.chkRenameMulti = New System.Windows.Forms.CheckBox()
        Me.lblFilePattern = New System.Windows.Forms.Label()
        Me.lblFolderPattern = New System.Windows.Forms.Label()
        Me.txtFilePattern = New System.Windows.Forms.TextBox()
        Me.txtFolderPattern = New System.Windows.Forms.TextBox()
        Me.chkBulkRenamer = New System.Windows.Forms.CheckBox()
        Me.chkGenericModule = New System.Windows.Forms.CheckBox()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.tblRenamerPatterns = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlSettings.SuspendLayout()
        Me.gbRenamerPatterns.SuspendLayout()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
        Me.tblRenamerPatterns.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.pnlSettingsMain)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsTop)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(554, 379)
        Me.pnlSettings.TabIndex = 84
        '
        'lblTips
        '
        Me.lblTips.AutoSize = True
        Me.lblTips.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTips.Location = New System.Drawing.Point(302, 0)
        Me.lblTips.MaximumSize = New System.Drawing.Size(475, 360)
        Me.lblTips.Name = "lblTips"
        Me.tblSettingsMain.SetRowSpan(Me.lblTips, 4)
        Me.lblTips.Size = New System.Drawing.Size(42, 15)
        Me.lblTips.TabIndex = 4
        Me.lblTips.Text = "Label1"
        '
        'gbRenamerPatterns
        '
        Me.gbRenamerPatterns.AutoSize = True
        Me.gbRenamerPatterns.Controls.Add(Me.tblRenamerPatterns)
        Me.gbRenamerPatterns.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbRenamerPatterns.Location = New System.Drawing.Point(3, 49)
        Me.gbRenamerPatterns.Name = "gbRenamerPatterns"
        Me.gbRenamerPatterns.Size = New System.Drawing.Size(293, 163)
        Me.gbRenamerPatterns.TabIndex = 3
        Me.gbRenamerPatterns.TabStop = False
        Me.gbRenamerPatterns.Text = "Default Renaming Patterns"
        '
        'chkRenameSingle
        '
        Me.chkRenameSingle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRenameSingle.AutoSize = True
        Me.chkRenameSingle.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameSingle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRenameSingle.Location = New System.Drawing.Point(3, 122)
        Me.chkRenameSingle.Name = "chkRenameSingle"
        Me.chkRenameSingle.Size = New System.Drawing.Size(281, 17)
        Me.chkRenameSingle.TabIndex = 5
        Me.chkRenameSingle.Text = "Automatically Rename Files During Single-Scraper"
        Me.chkRenameSingle.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameSingle.UseVisualStyleBackColor = True
        '
        'chkRenameMulti
        '
        Me.chkRenameMulti.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRenameMulti.AutoSize = True
        Me.chkRenameMulti.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameMulti.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRenameMulti.Location = New System.Drawing.Point(3, 99)
        Me.chkRenameMulti.Name = "chkRenameMulti"
        Me.chkRenameMulti.Size = New System.Drawing.Size(276, 17)
        Me.chkRenameMulti.TabIndex = 4
        Me.chkRenameMulti.Text = "Automatically Rename Files During Multi-Scraper"
        Me.chkRenameMulti.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameMulti.UseVisualStyleBackColor = True
        '
        'lblFilePattern
        '
        Me.lblFilePattern.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilePattern.AutoSize = True
        Me.lblFilePattern.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilePattern.Location = New System.Drawing.Point(3, 51)
        Me.lblFilePattern.Name = "lblFilePattern"
        Me.lblFilePattern.Size = New System.Drawing.Size(70, 13)
        Me.lblFilePattern.TabIndex = 2
        Me.lblFilePattern.Text = "Files Pattern"
        '
        'lblFolderPattern
        '
        Me.lblFolderPattern.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFolderPattern.AutoSize = True
        Me.lblFolderPattern.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFolderPattern.Location = New System.Drawing.Point(3, 3)
        Me.lblFolderPattern.Name = "lblFolderPattern"
        Me.lblFolderPattern.Size = New System.Drawing.Size(85, 13)
        Me.lblFolderPattern.TabIndex = 0
        Me.lblFolderPattern.Text = "Folders Pattern"
        '
        'txtFilePattern
        '
        Me.txtFilePattern.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFilePattern.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePattern.Location = New System.Drawing.Point(3, 71)
        Me.txtFilePattern.Name = "txtFilePattern"
        Me.txtFilePattern.Size = New System.Drawing.Size(280, 22)
        Me.txtFilePattern.TabIndex = 3
        '
        'txtFolderPattern
        '
        Me.txtFolderPattern.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFolderPattern.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolderPattern.Location = New System.Drawing.Point(3, 23)
        Me.txtFolderPattern.Name = "txtFolderPattern"
        Me.txtFolderPattern.Size = New System.Drawing.Size(280, 22)
        Me.txtFolderPattern.TabIndex = 1
        '
        'chkBulkRenamer
        '
        Me.chkBulkRenamer.AutoSize = True
        Me.chkBulkRenamer.Location = New System.Drawing.Point(3, 3)
        Me.chkBulkRenamer.Name = "chkBulkRenamer"
        Me.chkBulkRenamer.Size = New System.Drawing.Size(160, 17)
        Me.chkBulkRenamer.TabIndex = 2
        Me.chkBulkRenamer.Text = "Enable Bulk Renamer Tool"
        Me.chkBulkRenamer.UseVisualStyleBackColor = True
        '
        'chkGenericModule
        '
        Me.chkGenericModule.AutoSize = True
        Me.chkGenericModule.Location = New System.Drawing.Point(3, 26)
        Me.chkGenericModule.Name = "chkGenericModule"
        Me.chkGenericModule.Size = New System.Drawing.Size(190, 17)
        Me.chkGenericModule.TabIndex = 1
        Me.chkGenericModule.Text = "Enable Generic Rename Module"
        Me.chkGenericModule.UseVisualStyleBackColor = True
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(554, 23)
        Me.pnlSettingsTop.TabIndex = 0
        '
        'chkEnabled
        '
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Location = New System.Drawing.Point(8, 3)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(68, 17)
        Me.chkEnabled.TabIndex = 0
        Me.chkEnabled.Text = "Enabled"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'tblSettingsTop
        '
        Me.tblSettingsTop.AutoSize = True
        Me.tblSettingsTop.ColumnCount = 2
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.Controls.Add(Me.chkEnabled, 0, 0)
        Me.tblSettingsTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsTop.Name = "tblSettingsTop"
        Me.tblSettingsTop.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.tblSettingsTop.RowCount = 2
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.Size = New System.Drawing.Size(554, 23)
        Me.tblSettingsTop.TabIndex = 5
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 23)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(554, 356)
        Me.pnlSettingsMain.TabIndex = 5
        '
        'tblSettingsMain
        '
        Me.tblSettingsMain.AutoScroll = True
        Me.tblSettingsMain.AutoSize = True
        Me.tblSettingsMain.ColumnCount = 3
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.Controls.Add(Me.chkBulkRenamer, 0, 0)
        Me.tblSettingsMain.Controls.Add(Me.chkGenericModule, 0, 1)
        Me.tblSettingsMain.Controls.Add(Me.gbRenamerPatterns, 0, 2)
        Me.tblSettingsMain.Controls.Add(Me.lblTips, 1, 0)
        Me.tblSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsMain.Name = "tblSettingsMain"
        Me.tblSettingsMain.RowCount = 5
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.Size = New System.Drawing.Size(554, 356)
        Me.tblSettingsMain.TabIndex = 6
        '
        'tblRenamerPatterns
        '
        Me.tblRenamerPatterns.AutoSize = True
        Me.tblRenamerPatterns.ColumnCount = 2
        Me.tblRenamerPatterns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRenamerPatterns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRenamerPatterns.Controls.Add(Me.chkRenameSingle, 0, 5)
        Me.tblRenamerPatterns.Controls.Add(Me.lblFolderPattern, 0, 0)
        Me.tblRenamerPatterns.Controls.Add(Me.chkRenameMulti, 0, 4)
        Me.tblRenamerPatterns.Controls.Add(Me.txtFolderPattern, 0, 1)
        Me.tblRenamerPatterns.Controls.Add(Me.txtFilePattern, 0, 3)
        Me.tblRenamerPatterns.Controls.Add(Me.lblFilePattern, 0, 2)
        Me.tblRenamerPatterns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblRenamerPatterns.Location = New System.Drawing.Point(3, 18)
        Me.tblRenamerPatterns.Name = "tblRenamerPatterns"
        Me.tblRenamerPatterns.RowCount = 7
        Me.tblRenamerPatterns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRenamerPatterns.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatterns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRenamerPatterns.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatterns.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatterns.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatterns.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatterns.Size = New System.Drawing.Size(287, 142)
        Me.tblRenamerPatterns.TabIndex = 7
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(554, 379)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmSettingsHolder"
        Me.Text = "frmSettingsHolder"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.gbRenamerPatterns.ResumeLayout(False)
        Me.gbRenamerPatterns.PerformLayout()
        Me.pnlSettingsTop.ResumeLayout(False)
        Me.pnlSettingsTop.PerformLayout()
        Me.tblSettingsTop.ResumeLayout(False)
        Me.tblSettingsTop.PerformLayout()
        Me.pnlSettingsMain.ResumeLayout(False)
        Me.pnlSettingsMain.PerformLayout()
        Me.tblSettingsMain.ResumeLayout(False)
        Me.tblSettingsMain.PerformLayout()
        Me.tblRenamerPatterns.ResumeLayout(False)
        Me.tblRenamerPatterns.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkBulkRenamer As System.Windows.Forms.CheckBox
    Friend WithEvents chkGenericModule As System.Windows.Forms.CheckBox
    Friend WithEvents gbRenamerPatterns As System.Windows.Forms.GroupBox
    Friend WithEvents chkRenameSingle As System.Windows.Forms.CheckBox
    Friend WithEvents chkRenameMulti As System.Windows.Forms.CheckBox
    Friend WithEvents lblFilePattern As System.Windows.Forms.Label
    Friend WithEvents lblFolderPattern As System.Windows.Forms.Label
    Friend WithEvents txtFilePattern As System.Windows.Forms.TextBox
    Friend WithEvents txtFolderPattern As System.Windows.Forms.TextBox
    Friend WithEvents lblTips As System.Windows.Forms.Label
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblRenamerPatterns As System.Windows.Forms.TableLayoutPanel
End Class
