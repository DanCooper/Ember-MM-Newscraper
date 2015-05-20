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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettingsHolder))
        Me.lvPaths = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblPath = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.btnPathBrowse = New System.Windows.Forms.Button()
        Me.btnPathRemove = New System.Windows.Forms.Button()
        Me.btnPathEdit = New System.Windows.Forms.Button()
        Me.btnPathNew = New System.Windows.Forms.Button()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbTeraCopy = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkTeraCopyEnable = New System.Windows.Forms.CheckBox()
        Me.lblTeraCopyPath = New System.Windows.Forms.Label()
        Me.txtTeraCopyPath = New System.Windows.Forms.TextBox()
        Me.btnTeraCopyPathBrowse = New System.Windows.Forms.Button()
        Me.lblTeraCopyLink = New System.Windows.Forms.LinkLabel()
        Me.ofdBrowse = New System.Windows.Forms.OpenFileDialog()
        Me.lblType = New System.Windows.Forms.Label()
        Me.cbType = New System.Windows.Forms.ComboBox()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
        Me.gbTeraCopy.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvPaths
        '
        Me.lvPaths.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.tblSettingsMain.SetColumnSpan(Me.lvPaths, 4)
        Me.lvPaths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvPaths.FullRowSelect = True
        Me.lvPaths.Location = New System.Drawing.Point(3, 3)
        Me.lvPaths.Name = "lvPaths"
        Me.lvPaths.Size = New System.Drawing.Size(499, 216)
        Me.lvPaths.TabIndex = 1
        Me.lvPaths.UseCompatibleStateImageBehavior = False
        Me.lvPaths.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Name"
        Me.ColumnHeader1.Width = 138
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Path"
        Me.ColumnHeader2.Width = 270
        '
        'lblName
        '
        Me.lblName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(3, 258)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(36, 13)
        Me.lblName.TabIndex = 5
        Me.lblName.Text = "Name"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPath
        '
        Me.lblPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPath.AutoSize = True
        Me.lblPath.Location = New System.Drawing.Point(3, 313)
        Me.lblPath.Name = "lblPath"
        Me.lblPath.Size = New System.Drawing.Size(30, 13)
        Me.lblPath.TabIndex = 7
        Me.lblPath.Text = "Path"
        Me.lblPath.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtName
        '
        Me.txtName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblSettingsMain.SetColumnSpan(Me.txtName, 2)
        Me.txtName.Location = New System.Drawing.Point(45, 254)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(121, 22)
        Me.txtName.TabIndex = 6
        '
        'txtPath
        '
        Me.txtPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblSettingsMain.SetColumnSpan(Me.txtPath, 2)
        Me.txtPath.Location = New System.Drawing.Point(45, 309)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(300, 22)
        Me.txtPath.TabIndex = 8
        '
        'btnPathBrowse
        '
        Me.btnPathBrowse.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnPathBrowse.Location = New System.Drawing.Point(348, 310)
        Me.btnPathBrowse.Margin = New System.Windows.Forms.Padding(0)
        Me.btnPathBrowse.Name = "btnPathBrowse"
        Me.btnPathBrowse.Size = New System.Drawing.Size(24, 20)
        Me.btnPathBrowse.TabIndex = 9
        Me.btnPathBrowse.Text = "..."
        Me.btnPathBrowse.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPathBrowse.UseVisualStyleBackColor = True
        '
        'btnPathRemove
        '
        Me.btnPathRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnPathRemove.Enabled = False
        Me.btnPathRemove.Image = CType(resources.GetObject("btnPathRemove.Image"), System.Drawing.Image)
        Me.btnPathRemove.Location = New System.Drawing.Point(479, 225)
        Me.btnPathRemove.Name = "btnPathRemove"
        Me.btnPathRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnPathRemove.TabIndex = 4
        Me.btnPathRemove.UseVisualStyleBackColor = True
        '
        'btnPathEdit
        '
        Me.btnPathEdit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnPathEdit.Enabled = False
        Me.btnPathEdit.Image = CType(resources.GetObject("btnPathEdit.Image"), System.Drawing.Image)
        Me.btnPathEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPathEdit.Location = New System.Drawing.Point(74, 225)
        Me.btnPathEdit.Name = "btnPathEdit"
        Me.btnPathEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnPathEdit.TabIndex = 3
        Me.btnPathEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPathEdit.UseVisualStyleBackColor = True
        '
        'btnPathNew
        '
        Me.btnPathNew.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnPathNew.Enabled = False
        Me.btnPathNew.Image = CType(resources.GetObject("btnPathNew.Image"), System.Drawing.Image)
        Me.btnPathNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPathNew.Location = New System.Drawing.Point(45, 225)
        Me.btnPathNew.Name = "btnPathNew"
        Me.btnPathNew.Size = New System.Drawing.Size(23, 23)
        Me.btnPathNew.TabIndex = 2
        Me.btnPathNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPathNew.UseVisualStyleBackColor = True
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(696, 23)
        Me.pnlSettingsTop.TabIndex = 0
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
        Me.tblSettingsTop.Size = New System.Drawing.Size(696, 23)
        Me.tblSettingsTop.TabIndex = 10
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
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.Controls.Add(Me.pnlSettingsMain)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsTop)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(696, 573)
        Me.pnlSettings.TabIndex = 0
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 23)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(696, 550)
        Me.pnlSettingsMain.TabIndex = 10
        '
        'tblSettingsMain
        '
        Me.tblSettingsMain.AutoScroll = True
        Me.tblSettingsMain.AutoSize = True
        Me.tblSettingsMain.ColumnCount = 5
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.Controls.Add(Me.lvPaths, 0, 0)
        Me.tblSettingsMain.Controls.Add(Me.lblPath, 0, 4)
        Me.tblSettingsMain.Controls.Add(Me.txtPath, 1, 4)
        Me.tblSettingsMain.Controls.Add(Me.lblName, 0, 2)
        Me.tblSettingsMain.Controls.Add(Me.txtName, 1, 2)
        Me.tblSettingsMain.Controls.Add(Me.btnPathRemove, 3, 1)
        Me.tblSettingsMain.Controls.Add(Me.btnPathEdit, 2, 1)
        Me.tblSettingsMain.Controls.Add(Me.btnPathNew, 1, 1)
        Me.tblSettingsMain.Controls.Add(Me.btnPathBrowse, 3, 4)
        Me.tblSettingsMain.Controls.Add(Me.gbTeraCopy, 0, 5)
        Me.tblSettingsMain.Controls.Add(Me.lblType, 0, 3)
        Me.tblSettingsMain.Controls.Add(Me.cbType, 1, 3)
        Me.tblSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsMain.Name = "tblSettingsMain"
        Me.tblSettingsMain.RowCount = 7
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.Size = New System.Drawing.Size(696, 550)
        Me.tblSettingsMain.TabIndex = 11
        '
        'gbTeraCopy
        '
        Me.gbTeraCopy.AutoSize = True
        Me.tblSettingsMain.SetColumnSpan(Me.gbTeraCopy, 4)
        Me.gbTeraCopy.Controls.Add(Me.TableLayoutPanel1)
        Me.gbTeraCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTeraCopy.Location = New System.Drawing.Point(3, 337)
        Me.gbTeraCopy.Name = "gbTeraCopy"
        Me.gbTeraCopy.Size = New System.Drawing.Size(499, 92)
        Me.gbTeraCopy.TabIndex = 10
        Me.gbTeraCopy.TabStop = False
        Me.gbTeraCopy.Text = "TeraCopy"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.chkTeraCopyEnable, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTeraCopyPath, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtTeraCopyPath, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnTeraCopyPathBrowse, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTeraCopyLink, 1, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 18)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(493, 71)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'chkTeraCopyEnable
        '
        Me.chkTeraCopyEnable.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTeraCopyEnable.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.chkTeraCopyEnable, 3)
        Me.chkTeraCopyEnable.Location = New System.Drawing.Point(3, 3)
        Me.chkTeraCopyEnable.Name = "chkTeraCopyEnable"
        Me.chkTeraCopyEnable.Size = New System.Drawing.Size(191, 17)
        Me.chkTeraCopyEnable.TabIndex = 0
        Me.chkTeraCopyEnable.Text = "Use TeraCopy to copy/move files"
        Me.chkTeraCopyEnable.UseVisualStyleBackColor = True
        '
        'lblTeraCopyPath
        '
        Me.lblTeraCopyPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTeraCopyPath.AutoSize = True
        Me.lblTeraCopyPath.Location = New System.Drawing.Point(3, 30)
        Me.lblTeraCopyPath.Name = "lblTeraCopyPath"
        Me.lblTeraCopyPath.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.lblTeraCopyPath.Size = New System.Drawing.Size(117, 13)
        Me.lblTeraCopyPath.TabIndex = 1
        Me.lblTeraCopyPath.Text = "Path to TeraCopy:"
        '
        'txtTeraCopyPath
        '
        Me.txtTeraCopyPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtTeraCopyPath.Enabled = False
        Me.txtTeraCopyPath.Location = New System.Drawing.Point(126, 26)
        Me.txtTeraCopyPath.Name = "txtTeraCopyPath"
        Me.txtTeraCopyPath.ReadOnly = True
        Me.txtTeraCopyPath.Size = New System.Drawing.Size(320, 22)
        Me.txtTeraCopyPath.TabIndex = 2
        '
        'btnTeraCopyPathBrowse
        '
        Me.btnTeraCopyPathBrowse.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTeraCopyPathBrowse.Enabled = False
        Me.btnTeraCopyPathBrowse.Location = New System.Drawing.Point(449, 27)
        Me.btnTeraCopyPathBrowse.Margin = New System.Windows.Forms.Padding(0)
        Me.btnTeraCopyPathBrowse.Name = "btnTeraCopyPathBrowse"
        Me.btnTeraCopyPathBrowse.Size = New System.Drawing.Size(24, 20)
        Me.btnTeraCopyPathBrowse.TabIndex = 10
        Me.btnTeraCopyPathBrowse.Text = "..."
        Me.btnTeraCopyPathBrowse.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnTeraCopyPathBrowse.UseVisualStyleBackColor = True
        '
        'lblTeraCopyLink
        '
        Me.lblTeraCopyLink.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTeraCopyLink.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lblTeraCopyLink, 2)
        Me.lblTeraCopyLink.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblTeraCopyLink.LinkArea = New System.Windows.Forms.LinkArea(13, 12)
        Me.lblTeraCopyLink.Location = New System.Drawing.Point(126, 51)
        Me.lblTeraCopyLink.Name = "lblTeraCopyLink"
        Me.lblTeraCopyLink.Size = New System.Drawing.Size(195, 20)
        Me.lblTeraCopyLink.TabIndex = 12
        Me.lblTeraCopyLink.TabStop = True
        Me.lblTeraCopyLink.Text = "supported by TeraCopy 2.0 and above"
        Me.lblTeraCopyLink.UseCompatibleTextRendering = True
        Me.lblTeraCopyLink.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'lblType
        '
        Me.lblType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblType.AutoSize = True
        Me.lblType.Location = New System.Drawing.Point(3, 286)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(30, 13)
        Me.lblType.TabIndex = 5
        Me.lblType.Text = "Type"
        Me.lblType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cbType
        '
        Me.tblSettingsMain.SetColumnSpan(Me.cbType, 2)
        Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbType.FormattingEnabled = True
        Me.cbType.Location = New System.Drawing.Point(45, 282)
        Me.cbType.Name = "cbType"
        Me.cbType.Size = New System.Drawing.Size(121, 21)
        Me.cbType.TabIndex = 11
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Type"
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(696, 573)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings for Media File Manager"
        Me.pnlSettingsTop.ResumeLayout(False)
        Me.pnlSettingsTop.PerformLayout()
        Me.tblSettingsTop.ResumeLayout(False)
        Me.tblSettingsTop.PerformLayout()
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.pnlSettingsMain.ResumeLayout(False)
        Me.pnlSettingsMain.PerformLayout()
        Me.tblSettingsMain.ResumeLayout(False)
        Me.tblSettingsMain.PerformLayout()
        Me.gbTeraCopy.ResumeLayout(False)
        Me.gbTeraCopy.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lvPaths As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblPath As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents btnPathBrowse As System.Windows.Forms.Button
    Friend WithEvents btnPathRemove As System.Windows.Forms.Button
    Friend WithEvents btnPathEdit As System.Windows.Forms.Button
    Friend WithEvents btnPathNew As System.Windows.Forms.Button
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents gbTeraCopy As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkTeraCopyEnable As System.Windows.Forms.CheckBox
    Friend WithEvents lblTeraCopyPath As System.Windows.Forms.Label
    Friend WithEvents txtTeraCopyPath As System.Windows.Forms.TextBox
    Friend WithEvents btnTeraCopyPathBrowse As System.Windows.Forms.Button
    Friend WithEvents lblTeraCopyLink As System.Windows.Forms.LinkLabel
    Friend WithEvents ofdBrowse As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents cbType As System.Windows.Forms.ComboBox

End Class
